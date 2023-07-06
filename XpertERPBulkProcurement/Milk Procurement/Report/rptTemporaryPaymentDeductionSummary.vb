Imports common
Imports System.ComponentModel
Imports System.IO

'by Sanjay - Create new report 
Public Class rptTemporaryPaymentDeductionSummary
    Inherits FrmMainTranScreen
    Dim isLoad As Boolean = False
    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnExp.Visible = MyBase.isExport
    End Sub

    Private Sub RptInventoryMovement_Load(sender As Object, e As EventArgs) Handles Me.Load
        isLoad = True
        fromDate.Value = clsCommon.GETSERVERDATE()
        ToDate.Value = clsCommon.GETSERVERDATE()
        Reset()
        isLoad = False
    End Sub
    Sub Reset()
        EnableDisableControl(True)
        Gv1.DataSource = Nothing
        Gv1.Rows.Clear()
        Gv1.Columns.Clear()
        Gv1.MasterTemplate.SummaryRowsBottom.Clear()
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

    Private Sub EnableDisableControl(ByVal val As Boolean)
        RadGroupBox2.Enabled = val
        RadGroupBox3.Enabled = val
        txtDeduction.Enabled = val
        txtMCC.Enabled = val
        chkDCSWise.Enabled = val
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        PageSetupReport_ID = clsCommon.myCstr(MyBase.Form_ID)
        If clsCommon.CompairString(objCommonVar.CurrComp_Code1, "UDP") = CompairStringResult.Equal Then
            If CheckBox1.Checked Then
                PrintUDPOLD(False)
            Else
                PrintUDP(False)
            End If
        Else
            Print(False)
        End If

    End Sub

    Sub PrintUDP(ByVal isPrint As Boolean, Optional ByVal isPrerint As Boolean = False)
        Try
            Dim qry As String = ""
            Dim dt1 As New DataTable
            If rdbOldOutstanding.Checked Then ''1,2,3
                qry = "select top 1 From_Date,To_Date from TSPL_PAYMENT_CYCLE_GENERATED where From_Date<'" + clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") + "' order by MCC_Code,From_Date desc"
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                    Throw New Exception("Please Generate Payment Cycle")
                End If
                qry = GetBAseQeryUDP(clsCommon.myCDate(dt.Rows(0)("From_Date")), clsCommon.myCDate(dt.Rows(0)("To_Date")))
            ElseIf rdbOldCurrent.Checked OrElse rdbCurrentStanding.Checked = True Then
                qry = GetBAseQeryUDP(fromDate.Value, ToDate.Value)
            ElseIf chkWithOpening.Checked OrElse chkORD_CD.Checked Then
                Dim strDocNo As String = Nothing
                Dim strDocQry = "select Doc_No from TSPL_PAYMENT_PROCESS_head where convert(date,From_Date,103)>=convert(date,('" + fromDate.Value + "'),103) and convert(date,To_Date,103)<=convert(date,('" + ToDate.Value + "'),103)"
                Dim dtDocNo As DataTable = clsDBFuncationality.GetDataTable(strDocQry)
                If dtDocNo IsNot Nothing AndAlso dtDocNo.Rows.Count > 0 Then
                    Dim arr As New ArrayList
                    For Each dr As DataRow In dtDocNo.Rows
                        arr.Add(clsCommon.myCstr(dr("Doc_No")))
                    Next
                    strDocNo = clsCommon.GetMulcallString(arr)
                End If

                Dim strOldDocNo As String = Nothing
                If clsCommon.myLen(strDocNo) > 0 Then
                    strOldDocNo = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT TOP 1 Doc_No  from TSPL_PAYMENT_PROCESS_head where convert(date,To_Date,103)<=convert(date,('" + ToDate.Value + "'),103) AND TSPL_PAYMENT_PROCESS_head.Doc_No in (" + strDocNo + ") ORDER BY From_Date DESC"))
                Else
                    strDocNo = "''"
                    strOldDocNo = "''"
                End If

                Dim subMCCQry1 As String = Nothing
                Dim subMCCQry2 As String = Nothing
                If txtMCC.Value.Length > 0 Then
                    subMCCQry1 = " and TSPL_VLC_MASTER_HEAD.MCC='" + txtMCC.Value + "'"
                    'subMCCQry2 = " and TSPL_MULTIPLE_DEDUCTION_HEAD.MCC_Code='" + txtMCC.Value + "'"
                End If

                Dim subQryWhere As String = Nothing
                If txtDeduction.Value.Length > 0 Then
                    subQryWhere = "where Final.Ded_Code='" + txtDeduction.Value + "'"
                End If

                Dim subQry As String = Nothing
                Dim subDCSQry As String = Nothing
                If chkDCSWise.Checked = True Then
                    subDCSQry = ",Max(VSP_Uploader_Code) as DCSCode,Max(Vendor_NAME) As 'DCS Name'"
                    subQry = ",Final.Vendor_CODE"
                End If
                If chkWithOpening.Checked = True Then ''4
                    qry = "select case when isnull(Final.Type,'D')='D' then 'Deduction' when isnull(Final.Type,'')='A' then 'Addition' else '' end Type
                       ,Max(Ded_code+'-'+Ded_Desc) as DeductionName " + subDCSQry + ", sum(Amount) as [Amount] from ( 
                        select case when isnull(TSPL_MULTIPLE_DEDUCTION_HEAD.Trans_Type,'Deduction')='Addition' then 'A' else 'D' end Type
                        ,TSPL_VLC_MASTER_HEAD.VLC_CODE_VLC_Uploader as VSP_Uploader_Code 
                        ,TSPL_MULTIPLE_DEDUCTION_detail.Vendor_Code,TSPL_MULTIPLE_DEDUCTION_detail.Vendor_Name
                        ,TSPL_MULTIPLE_DEDUCTION_detail.DeductionCode as Ded_Code,TSPL_MULTIPLE_DEDUCTION_detail.Deduction_Desc as Ded_Desc
                        ,isnull(TSPL_MULTIPLE_DEDUCTION_detail.amount,0) as Amount
                        from TSPL_MULTIPLE_DEDUCTION_HEAD 
                        LEFT OUTER JOIN TSPL_MULTIPLE_DEDUCTION_DETAIL ON TSPL_MULTIPLE_DEDUCTION_HEAD.Document_No =TSPL_MULTIPLE_DEDUCTION_DETAIL.Document_No
                        left outer Join (select distinct TSPL_VLC_MASTER_HEAD.VSP_Code,TSPL_VLC_MASTER_HEAD.VLC_CODE_VLC_Uploader from TSPL_VLC_MASTER_HEAD) as TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code = TSPL_MULTIPLE_DEDUCTION_detail.Vendor_Code
                        where TSPL_MULTIPLE_DEDUCTION_HEAD.IsPosted=1
                        and TSPL_MULTIPLE_DEDUCTION_HEAD.IsOpening=1 and convert(date,Document_Date,103)>=convert(date,('" + fromDate.Value + "'),103) and convert(date,Document_Date,103)<=convert(date,('" + ToDate.Value + "'),103) " + subMCCQry1 + "
                        ) Final " + subQryWhere + " group by  Final.Ded_Code ,[Type]" + subQry + " order by [Type] desc"

                ElseIf chkORD_CD.Checked = True Then ''5
                    qry = "select case when isnull(Final.Type,'D')='D' then 'Deduction' when isnull(Final.Type,'')='A' then 'Addition' else '' end Type,Max(Ded_code+'-'+Ded_Desc) as DeductionName
                        " + subDCSQry + ", sum(Amount) as [Amount] from ( 
                        Select 'D' Type,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader as VSP_Uploader_Code,TSPL_PAYMENT_PROCESS_DEDUCTION.Vendor_CODE, TSPL_PAYMENT_PROCESS_DEDUCTION.Vendor_NAME, TSPL_PAYMENT_PROCESS_DEDUCTION.Ded_Code,TSPL_PAYMENT_PROCESS_DEDUCTION.Ded_Desc,TSPL_PAYMENT_PROCESS_DEDUCTION.Reduce_Deduc_Amt as Amount 
                        from TSPL_PAYMENT_PROCESS_DEDUCTION 
                        left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code =TSPL_PAYMENT_PROCESS_DEDUCTION.Vendor_CODE
                        where  TSPL_PAYMENT_PROCESS_DEDUCTION.Doc_No in ('" + strOldDocNo + "')  AND ISNULL(TSPL_PAYMENT_PROCESS_DEDUCTION.Reduce_Deduc_Amt,0)>0  
                        UNION ALL 
                        Select 'D' Type,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader as VSP_Uploader_Code,TSPL_PAYMENT_PROCESS_DEDUCTION.Vendor_CODE, TSPL_PAYMENT_PROCESS_DEDUCTION.Vendor_NAME, TSPL_PAYMENT_PROCESS_DEDUCTION.Ded_Code,TSPL_PAYMENT_PROCESS_DEDUCTION.Ded_Desc,TSPL_PAYMENT_PROCESS_DEDUCTION.Amount-TSPL_PAYMENT_PROCESS_DEDUCTION.Reduce_Deduc_Amt as Amount 
                        from TSPL_PAYMENT_PROCESS_DEDUCTION 
                        left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code =TSPL_PAYMENT_PROCESS_DEDUCTION.Vendor_CODE
                        where  TSPL_PAYMENT_PROCESS_DEDUCTION.Doc_No in (" + strDocNo + ") " + subMCCQry1 + "
                        ) Final " + subQryWhere + " group by  Final.Ded_Code ,[Type] " + subQry + " order by [Type] desc"
                End If
            End If
            dt1 = Nothing
            dt1 = clsDBFuncationality.GetDataTable(qry)
            Gv1.MasterTemplate.SummaryRowsBottom.Clear()
            Gv1.DataSource = Nothing
            Gv1.Rows.Clear()
            Gv1.Columns.Clear()
            Gv1.GroupDescriptors.Clear()
            Gv1.MasterTemplate.SummaryRowsBottom.Clear()
            Gv1.MasterView.Refresh()

            If dt1 Is Nothing OrElse dt1.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow("No Data Found to Display", Me.Text)
                Exit Sub
            Else
                Gv1.DataSource = dt1
                RadPageView1.SelectedPage = RadPageViewPage2
                SetGridFormatUDP()
                ReStoreGridLayout()
            End If
            EnableDisableControl(False)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Function GetBAseQeryUDP(ByVal fromDate As Date, ByVal ToDate As Date) As String
        Dim BaseQry As String = "select TSPL_MULTIPLE_DEDUCTION_HEAD.Document_No,TSPL_MULTIPLE_DEDUCTION_HEAD.Document_Date,TSPL_MULTIPLE_DEDUCTION_DETAIL.Against_Deduction_DocNo as AP_Invoice_No,TSPL_VENDOR_INVOICE_HEAD.Posting_Date as AP_Invoice_Date,TSPL_VENDOR_INVOICE_HEAD.Document_Type,TSPL_MULTIPLE_DEDUCTION_detail.DeductionCode,TSPL_MULTIPLE_DEDUCTION_detail.Vendor_Code,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader,TSPL_MULTIPLE_DEDUCTION_DETAIL.Amount,0 as Reduce_Deduc_Amt ,1 as RI
from TSPL_MULTIPLE_DEDUCTION_DETAIL
left outer join TSPL_MULTIPLE_DEDUCTION_HEAD on TSPL_MULTIPLE_DEDUCTION_HEAD.Document_No=TSPL_MULTIPLE_DEDUCTION_DETAIL.Document_No 
inner join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No=TSPL_MULTIPLE_DEDUCTION_DETAIL.Against_Deduction_DocNo
left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code=TSPL_VENDOR_INVOICE_HEAD.Vendor_Code
where 2=2 "
        If clsCommon.myLen(txtMCC.Value) > 0 Then
            BaseQry += " and TSPL_VLC_MASTER_HEAD.MCC='" + txtMCC.Value + "' "
        End If
        If clsCommon.myLen(txtDeduction.Value) > 0 Then
            BaseQry += " and TSPL_MULTIPLE_DEDUCTION_detail.DeductionCode='" + txtDeduction.Value + "'"
        End If
        BaseQry += "Union all
            select TSPL_PAYMENT_PROCESS_DEDUCTION.Doc_No as Document_No, TSPL_PAYMENT_PROCESS_HEAD.To_Date as Doc_Date,TSPL_PAYMENT_PROCESS_DEDUCTION.AP_Invoice_No ,TSPL_VENDOR_INVOICE_HEAD.Posting_Date as AP_Invoice_Date,TSPL_VENDOR_INVOICE_HEAD.Document_Type,TSPL_MULTIPLE_DEDUCTION_detail.DeductionCode,TSPL_PAYMENT_PROCESS_DEDUCTION.Vendor_CODE,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader,TSPL_PAYMENT_PROCESS_DEDUCTION.Amount,TSPL_PAYMENT_PROCESS_DEDUCTION.Reduce_Deduc_Amt,2 as RI
from TSPL_PAYMENT_PROCESS_DEDUCTION
inner join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No=TSPL_PAYMENT_PROCESS_DEDUCTION.AP_Invoice_No
left outer join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_HEAD.Doc_No=TSPL_PAYMENT_PROCESS_DEDUCTION.Doc_No
inner join TSPL_MULTIPLE_DEDUCTION_DETAIL on TSPL_MULTIPLE_DEDUCTION_DETAIL.Against_Deduction_DocNo=TSPL_PAYMENT_PROCESS_DEDUCTION.AP_Invoice_No 
left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code=TSPL_VENDOR_INVOICE_HEAD.Vendor_Code
where 2=2 "
        If clsCommon.myLen(txtMCC.Value) > 0 Then
            BaseQry += " and TSPL_VLC_MASTER_HEAD.MCC='" + txtMCC.Value + "' "
        End If
        If clsCommon.myLen(txtDeduction.Value) > 0 Then
            BaseQry += " and TSPL_MULTIPLE_DEDUCTION_detail.DeductionCode='" + txtDeduction.Value + "'"
        End If

        BaseQry += "Union all
select TSPL_PAYMENT_PROCESS_CREDIT_NOTE.Doc_No as Document_No, TSPL_PAYMENT_PROCESS_HEAD.To_Date as Doc_Date,TSPL_PAYMENT_PROCESS_CREDIT_NOTE.AP_Invoice_No,TSPL_VENDOR_INVOICE_HEAD.Posting_Date as AP_Invoice_Date,TSPL_VENDOR_INVOICE_HEAD.Document_Type,TSPL_MULTIPLE_DEDUCTION_detail.DeductionCode,TSPL_PAYMENT_PROCESS_CREDIT_NOTE.Vendor_CODE,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader,TSPL_PAYMENT_PROCESS_CREDIT_NOTE.Amount,0 as Reduce_Deduc_Amt,3 as RI
from TSPL_PAYMENT_PROCESS_CREDIT_NOTE
inner join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No=TSPL_PAYMENT_PROCESS_CREDIT_NOTE.AP_Invoice_No
left outer join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_HEAD.Doc_No=TSPL_PAYMENT_PROCESS_CREDIT_NOTE.Doc_No
inner join TSPL_MULTIPLE_DEDUCTION_DETAIL on TSPL_MULTIPLE_DEDUCTION_DETAIL.Against_Deduction_DocNo=TSPL_PAYMENT_PROCESS_CREDIT_NOTE.AP_Invoice_No 
left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code=TSPL_VENDOR_INVOICE_HEAD.Vendor_Code
where 2=2 "
        If clsCommon.myLen(txtMCC.Value) > 0 Then
            BaseQry += " and TSPL_VLC_MASTER_HEAD.MCC='" + txtMCC.Value + "' "
        End If
        If clsCommon.myLen(txtDeduction.Value) > 0 Then
            BaseQry += " and TSPL_MULTIPLE_DEDUCTION_detail.DeductionCode='" + txtDeduction.Value + "'"
        End If
        Dim qry As String = ""
        If chkDCSWise.Checked Then
            qry = "select case when Document_Type='D' then 'Deduction' else 'Addition'  end Type,DCSCode,[DCS Name] ,(DeductionCode+'-'+DeductionName) as DeductionName,(OP+Sale) as [Opening+Sale],AMTDeducted as [Amt Deducted],(OP+Sale-AMTDeducted) as [Balance Amount] from (
select Document_Type,xx.DeductionCode,max(TSPL_DEDUCTION_MASTER.Description) as DeductionName,TSPL_VENDOR_MASTER.Vendor_Code,max(VLC_Code_VLC_Uploader) as DCSCode,max(TSPL_VENDOR_MASTER.Vendor_Name) as [DCS Name]
,sum((Amount-Reduce_Deduc_Amt) * (case when  Document_Date<'" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(fromDate), "dd/MMM/yyyy hh:mm:ss tt") + "' then 1 else 0 end) * (case when RI=1 then 1 else -1 end)) as OP 
,sum(Amount * (case when  Document_Date>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(fromDate), "dd/MMM/yyyy hh:mm:ss tt") + "' and Document_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(ToDate), "dd/MMM/yyyy hh:mm:ss tt") + "' then 1 else 0 end) * (case when RI=1 then 1 else 0 end)) as Sale
,sum((Amount-Reduce_Deduc_Amt) * (case when Document_Date>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(fromDate), "dd/MMM/yyyy hh:mm:ss tt") + "' and Document_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(ToDate), "dd/MMM/yyyy hh:mm:ss tt") + "' then 1 else 0 end) * (case when RI=1 then 0 else 1 end)) as AMTDeducted 
from (" + BaseQry + ")xx
left outer join TSPL_DEDUCTION_MASTER on TSPL_DEDUCTION_MASTER.Code=xx.DeductionCode
left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=xx.Vendor_Code
group by Document_Type,DeductionCode,TSPL_VENDOR_MASTER.Vendor_Code
)xxx"
        Else
            qry = "select case when Document_Type='D' then 'Deduction' else 'Addition'  end Type ,(DeductionCode+'-'+DeductionName) as DeductionName,(OP+Sale) as [Opening+Sale],AMTDeducted as [Amt Deducted],(OP+Sale-AMTDeducted) as [Balance Amount] from (
select Document_Type,xx.DeductionCode,max(TSPL_DEDUCTION_MASTER.Description) as DeductionName
,sum((Amount-Reduce_Deduc_Amt) * (case when  Document_Date<'" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(fromDate), "dd/MMM/yyyy hh:mm:ss tt") + "' then 1 else 0 end) * (case when RI=1 then 1 else -1 end)) as OP 
,sum(Amount * (case when  Document_Date>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(fromDate), "dd/MMM/yyyy hh:mm:ss tt") + "' and Document_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(ToDate), "dd/MMM/yyyy hh:mm:ss tt") + "' then 1 else 0 end) * (case when RI=1 then 1 else 0 end)) as Sale
,sum((Amount-Reduce_Deduc_Amt) * (case when Document_Date>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(fromDate), "dd/MMM/yyyy hh:mm:ss tt") + "' and Document_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(ToDate), "dd/MMM/yyyy hh:mm:ss tt") + "' then 1 else 0 end) * (case when RI=1 then 0 else 1 end)) as AMTDeducted 
from (" + BaseQry + ")xx
left outer join TSPL_DEDUCTION_MASTER on TSPL_DEDUCTION_MASTER.Code=xx.DeductionCode
left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=xx.Vendor_Code
group by Document_Type,DeductionCode
)xxx"
        End If
        Return qry
    End Function
    Sub PrintUDPOLD(ByVal isPrint As Boolean, Optional ByVal isPrerint As Boolean = False)
        Try
            Dim dt1 As New DataTable
            Dim qry As String = Nothing
            'Dim strDocNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Doc_No from TSPL_PAYMENT_PROCESS_head where convert(date,From_Date,103)>=convert(date,('" + fromDate.Value + "'),103) and convert(date,To_Date,103)<=convert(date,('" + ToDate.Value + "'),103)"))
            Dim strDocNo As String = Nothing
            Dim strDocQry = "select Doc_No from TSPL_PAYMENT_PROCESS_head where convert(date,From_Date,103)>=convert(date,('" + fromDate.Value + "'),103) and convert(date,To_Date,103)<=convert(date,('" + ToDate.Value + "'),103)"
            Dim dtDocNo As DataTable = clsDBFuncationality.GetDataTable(strDocQry)
            If dtDocNo IsNot Nothing AndAlso dtDocNo.Rows.Count > 0 Then
                Dim arr As New ArrayList
                For Each dr As DataRow In dtDocNo.Rows
                    arr.Add(clsCommon.myCstr(dr("Doc_No")))
                Next
                strDocNo = clsCommon.GetMulcallString(arr)
            End If

            Dim strOldDocNo As String = Nothing
            If clsCommon.myLen(strDocNo) > 0 Then
                strOldDocNo = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT TOP 1 Doc_No  from TSPL_PAYMENT_PROCESS_head where convert(date,To_Date,103)<=convert(date,('" + ToDate.Value + "'),103) AND TSPL_PAYMENT_PROCESS_head.Doc_No in (" + strDocNo + ") ORDER BY From_Date DESC"))
            Else
                strDocNo = "''"
                strOldDocNo = "''"
            End If

            Dim subMCCQry1 As String = Nothing
            Dim subMCCQry2 As String = Nothing
            If txtMCC.Value.Length > 0 Then
                subMCCQry1 = " and TSPL_VLC_MASTER_HEAD.MCC='" + txtMCC.Value + "'"
                'subMCCQry2 = " and TSPL_MULTIPLE_DEDUCTION_HEAD.MCC_Code='" + txtMCC.Value + "'"
            End If

            Dim subQryWhere As String = Nothing
            If txtDeduction.Value.Length > 0 Then
                subQryWhere = "where Final.Ded_Code='" + txtDeduction.Value + "'"
            End If

            Dim subQry As String = Nothing
            Dim subDCSQry As String = Nothing
            If chkDCSWise.Checked = True Then
                subDCSQry = ",Max(VSP_Uploader_Code) as DCSCode,Max(Vendor_NAME) As 'DCS Name'"
                subQry = ",Final.Vendor_CODE"
            End If

            If rdbOldOutstanding.Checked Then ''1
                If clsCommon.GetDateWithStartTime(fromDate.Value) = clsCommon.GetDateWithStartTime("01/Apr/2023") Then
                    qry = "select case when isnull(Final.Type,'D')='D' then 'Deduction' when isnull(Final.Type,'')='A' then 'Addition' else '' end Type,Max(Ded_code+'-'+Ded_Desc) as DeductionName
                        " + subDCSQry + ",0 as [Opening+Sale],0 as [Amt Deducted], sum(Amount) as [Balance Amount] from ( 
                        Select 'D' Type,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader as VSP_Uploader_Code,TSPL_PAYMENT_PROCESS_DEDUCTION.Vendor_CODE, TSPL_PAYMENT_PROCESS_DEDUCTION.Vendor_NAME, TSPL_PAYMENT_PROCESS_DEDUCTION.Ded_Code,TSPL_PAYMENT_PROCESS_DEDUCTION.Ded_Desc 
                        ,isnull(TSPL_PAYMENT_PROCESS_DEDUCTION.Reduce_Deduc_Amt,0) as Amount
                        from TSPL_PAYMENT_PROCESS_DEDUCTION 
                        left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code =TSPL_PAYMENT_PROCESS_DEDUCTION.Vendor_CODE
                        Left Outer Join TSPL_MULTIPLE_DEDUCTION_HEAD on TSPL_MULTIPLE_DEDUCTION_HEAD.Document_No=TSPL_PAYMENT_PROCESS_DEDUCTION.Doc_No
                        where  TSPL_PAYMENT_PROCESS_DEDUCTION.Doc_No in ('" + strOldDocNo + "') " + subMCCQry1 + " and convert(date,TSPL_MULTIPLE_DEDUCTION_HEAD.Document_Date,103)<convert(date,('" + fromDate.Value + "'),103)
                                        union all
                        select case when isnull(TSPL_MULTIPLE_DEDUCTION_HEAD.Trans_Type,'Deduction')='Addition' then 'A' else 'D' end Type
                        ,TSPL_VLC_MASTER_HEAD.VLC_CODE_VLC_Uploader as VSP_Uploader_Code 
                        ,TSPL_MULTIPLE_DEDUCTION_detail.Vendor_Code,TSPL_MULTIPLE_DEDUCTION_detail.Vendor_Name
                        ,TSPL_MULTIPLE_DEDUCTION_detail.DeductionCode as Ded_Code,TSPL_MULTIPLE_DEDUCTION_detail.Deduction_Desc as Ded_Desc
                        ,isnull(TSPL_MULTIPLE_DEDUCTION_detail.amount,0) as Amount
                        from TSPL_MULTIPLE_DEDUCTION_HEAD 
                        LEFT OUTER JOIN TSPL_MULTIPLE_DEDUCTION_DETAIL ON TSPL_MULTIPLE_DEDUCTION_HEAD.Document_No =TSPL_MULTIPLE_DEDUCTION_DETAIL.Document_No
                        left outer Join (select distinct TSPL_VLC_MASTER_HEAD.VSP_Code,TSPL_VLC_MASTER_HEAD.VLC_CODE_VLC_Uploader,TSPL_VLC_MASTER_HEAD.MCC from TSPL_VLC_MASTER_HEAD) as TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code = TSPL_MULTIPLE_DEDUCTION_detail.Vendor_Code
                        where TSPL_MULTIPLE_DEDUCTION_HEAD.IsPosted=1 and TSPL_MULTIPLE_DEDUCTION_HEAD.IsOpening=1
                        and TSPL_MULTIPLE_DEDUCTION_HEAD.IsOpening=1 and convert(date,Document_Date,103)<convert(date,('" + fromDate.Value + "'),103) " + subMCCQry1 + "
                        ) Final " + subQryWhere + " group by  Final.Ded_Code ,[Type] " + subQry + " having sum(Amount)>0 order by [Type] desc"
                Else
                    qry = "select top 1 From_Date,To_Date from TSPL_PAYMENT_CYCLE_GENERATED where From_Date<'" + clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") + "' order by MCC_Code,From_Date desc"
                    Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                    If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                        Throw New Exception("Please Generate Payment Cycle")
                    End If
                    qry = GetBAseQeryUDPOLD(clsCommon.myCDate(dt.Rows(0)("From_Date")), clsCommon.myCDate(dt.Rows(0)("To_Date")), subDCSQry, subMCCQry1, subQryWhere, subQry)

                End If



            ElseIf rdbOldCurrent.Checked OrElse rdbCurrentStanding.Checked = True Then ''2
                qry = GetBAseQeryUDPOLD(fromDate.Value, ToDate.Value, subDCSQry, subMCCQry1, subQryWhere, subQry)


            ElseIf chkWithOpening.Checked = True Then ''4
                qry = "select case when isnull(Final.Type,'D')='D' then 'Deduction' when isnull(Final.Type,'')='A' then 'Addition' else '' end Type
                       ,Max(Ded_code+'-'+Ded_Desc) as DeductionName " + subDCSQry + ", sum(Amount) as [Amount] from ( 
                        select case when isnull(TSPL_MULTIPLE_DEDUCTION_HEAD.Trans_Type,'Deduction')='Addition' then 'A' else 'D' end Type
                        ,TSPL_VLC_MASTER_HEAD.VLC_CODE_VLC_Uploader as VSP_Uploader_Code 
                        ,TSPL_MULTIPLE_DEDUCTION_detail.Vendor_Code,TSPL_MULTIPLE_DEDUCTION_detail.Vendor_Name
                        ,TSPL_MULTIPLE_DEDUCTION_detail.DeductionCode as Ded_Code,TSPL_MULTIPLE_DEDUCTION_detail.Deduction_Desc as Ded_Desc
                        ,isnull(TSPL_MULTIPLE_DEDUCTION_detail.amount,0) as Amount
                        from TSPL_MULTIPLE_DEDUCTION_HEAD 
                        LEFT OUTER JOIN TSPL_MULTIPLE_DEDUCTION_DETAIL ON TSPL_MULTIPLE_DEDUCTION_HEAD.Document_No =TSPL_MULTIPLE_DEDUCTION_DETAIL.Document_No
                        left outer Join (select distinct TSPL_VLC_MASTER_HEAD.VSP_Code,TSPL_VLC_MASTER_HEAD.VLC_CODE_VLC_Uploader from TSPL_VLC_MASTER_HEAD) as TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code = TSPL_MULTIPLE_DEDUCTION_detail.Vendor_Code
                        where TSPL_MULTIPLE_DEDUCTION_HEAD.IsPosted=1
                        and TSPL_MULTIPLE_DEDUCTION_HEAD.IsOpening=1 and convert(date,Document_Date,103)>=convert(date,('" + fromDate.Value + "'),103) and convert(date,Document_Date,103)<=convert(date,('" + ToDate.Value + "'),103) " + subMCCQry1 + "
                        ) Final " + subQryWhere + " group by  Final.Ded_Code ,[Type]" + subQry + " order by [Type] desc"

            ElseIf chkORD_CD.Checked = True Then ''5
                qry = "select case when isnull(Final.Type,'D')='D' then 'Deduction' when isnull(Final.Type,'')='A' then 'Addition' else '' end Type,Max(Ded_code+'-'+Ded_Desc) as DeductionName
                        " + subDCSQry + ", sum(Amount) as [Amount] from ( 
                        Select 'D' Type,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader as VSP_Uploader_Code,TSPL_PAYMENT_PROCESS_DEDUCTION.Vendor_CODE, TSPL_PAYMENT_PROCESS_DEDUCTION.Vendor_NAME, TSPL_PAYMENT_PROCESS_DEDUCTION.Ded_Code,TSPL_PAYMENT_PROCESS_DEDUCTION.Ded_Desc,TSPL_PAYMENT_PROCESS_DEDUCTION.Reduce_Deduc_Amt as Amount 
                        from TSPL_PAYMENT_PROCESS_DEDUCTION 
                        left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code =TSPL_PAYMENT_PROCESS_DEDUCTION.Vendor_CODE
                        where  TSPL_PAYMENT_PROCESS_DEDUCTION.Doc_No in ('" + strOldDocNo + "')  AND ISNULL(TSPL_PAYMENT_PROCESS_DEDUCTION.Reduce_Deduc_Amt,0)>0  
                        UNION ALL 
                        Select 'D' Type,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader as VSP_Uploader_Code,TSPL_PAYMENT_PROCESS_DEDUCTION.Vendor_CODE, TSPL_PAYMENT_PROCESS_DEDUCTION.Vendor_NAME, TSPL_PAYMENT_PROCESS_DEDUCTION.Ded_Code,TSPL_PAYMENT_PROCESS_DEDUCTION.Ded_Desc,TSPL_PAYMENT_PROCESS_DEDUCTION.Amount-TSPL_PAYMENT_PROCESS_DEDUCTION.Reduce_Deduc_Amt as Amount 
                        from TSPL_PAYMENT_PROCESS_DEDUCTION 
                        left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code =TSPL_PAYMENT_PROCESS_DEDUCTION.Vendor_CODE
                        where  TSPL_PAYMENT_PROCESS_DEDUCTION.Doc_No in (" + strDocNo + ") " + subMCCQry1 + "
                        ) Final " + subQryWhere + " group by  Final.Ded_Code ,[Type] " + subQry + " order by [Type] desc"
            End If

            dt1 = Nothing
            dt1 = clsDBFuncationality.GetDataTable(qry)
            Gv1.MasterTemplate.SummaryRowsBottom.Clear()
            Gv1.DataSource = Nothing
            Gv1.Rows.Clear()
            Gv1.Columns.Clear()
            Gv1.GroupDescriptors.Clear()
            Gv1.MasterTemplate.SummaryRowsBottom.Clear()
            Gv1.MasterView.Refresh()

            If dt1 Is Nothing OrElse dt1.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow("No Data Found to Display", Me.Text)
                Exit Sub
            Else
                Gv1.DataSource = dt1
                RadPageView1.SelectedPage = RadPageViewPage2
                SetGridFormatUDP()
                ReStoreGridLayout()
            End If
            EnableDisableControl(False)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Function GetBAseQeryUDPOLD(ByVal fromDate As Date, ByVal ToDate As Date, ByVal subDCSQry As String, ByVal subMCCQry1 As String, ByVal subQryWhere As String, ByVal subQry As String) As String
        Dim strDocNo As String = Nothing
        Dim strDocQry = "select Doc_No from TSPL_PAYMENT_PROCESS_head where convert(date,From_Date,103)>=convert(date,('" + clsCommon.GetPrintDate(clsCommon.GetPrintDate(fromDate, "dd/MMM/yyyy"), "dd/MMM/yyyy") + "'),103) and convert(date,To_Date,103)<=convert(date,('" + clsCommon.GetPrintDate(ToDate, "dd/MMM/yyyy") + "'),103)"
        Dim dtDocNo As DataTable = clsDBFuncationality.GetDataTable(strDocQry)
        If dtDocNo IsNot Nothing AndAlso dtDocNo.Rows.Count > 0 Then
            Dim arr As New ArrayList
            For Each dr As DataRow In dtDocNo.Rows
                arr.Add(clsCommon.myCstr(dr("Doc_No")))
            Next
            strDocNo = clsCommon.GetMulcallString(arr)
        Else
            strDocNo = "''"
        End If
        If clsCommon.myLen(subMCCQry1) <= 0 Then
            subMCCQry1 = ""
        End If
        If clsCommon.myLen(subDCSQry) <= 0 Then
            subDCSQry = ""
        End If
        Dim strOldDocNo As String = Nothing
        Dim strOLDFromDate As String = ""
        Dim strOLDToDate As String = ""
        Dim qry As String = "SELECT TOP 1 Doc_No,From_Date,To_Date  from TSPL_PAYMENT_PROCESS_head where convert(date,To_Date,103)<convert(date,('" + clsCommon.GetPrintDate(fromDate, "dd/MMM/yyyy") + "'),103) ORDER BY From_Date DESC"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            strOldDocNo = clsCommon.myCstr(dt.Rows(0)("Doc_No"))
            strOLDFromDate = clsCommon.GetPrintDate(clsCommon.myCDate(dt.Rows(0)("From_Date")), "dd/MMM/yyyy")
            strOLDToDate = clsCommon.GetPrintDate(clsCommon.myCDate(dt.Rows(0)("To_Date")), "dd/MMM/yyyy")
        Else
            strOldDocNo = "''"
            strOLDFromDate = clsCommon.GetPrintDate(clsCommon.myCDate(fromDate.AddDays(-10)), "dd/MMM/yyyy")
            strOLDToDate = clsCommon.GetPrintDate(clsCommon.myCDate(fromDate.AddDays(-1)), "dd/MMM/yyyy")
        End If

        qry = "select case when isnull(Final.Type,'D')='D' then 'Deduction' when isnull(Final.Type,'')='A' then 'Addition' else '' end Type,Max(Ded_code+'-'+Ded_Desc) as DeductionName " + subDCSQry + ",sum(Amount) as 'Opening+Sale',Sum(ReDedctAmt) As 'Amt Deducted',sum(Amount-ReDedctAmt) As 'Balance Amount' from (                        
Select 'D' Type,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader as VSP_Uploader_Code,TSPL_PAYMENT_PROCESS_DEDUCTION.Vendor_CODE, TSPL_PAYMENT_PROCESS_DEDUCTION.Vendor_NAME, TSPL_PAYMENT_PROCESS_DEDUCTION.Ded_Code,TSPL_PAYMENT_PROCESS_DEDUCTION.Ded_Desc 
,0 as Amount ,(isnull(TSPL_PAYMENT_PROCESS_DEDUCTION.Amount,0)-isnull(TSPL_PAYMENT_PROCESS_DEDUCTION.Reduce_Deduc_Amt,0)) As ReDedctAmt
from TSPL_PAYMENT_PROCESS_DEDUCTION 
left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code =TSPL_PAYMENT_PROCESS_DEDUCTION.Vendor_CODE
Left Outer Join TSPL_MULTIPLE_DEDUCTION_HEAD on TSPL_MULTIPLE_DEDUCTION_HEAD.Document_No=TSPL_PAYMENT_PROCESS_DEDUCTION.Doc_No                       
where  TSPL_PAYMENT_PROCESS_DEDUCTION.Doc_No in (" + strDocNo + ") " + subMCCQry1 + " 
 union all
select case when isnull(TSPL_MULTIPLE_DEDUCTION_HEAD.Trans_Type,'Deduction')='Addition' then 'A' else 'D' end Type
,TSPL_VLC_MASTER_HEAD.VLC_CODE_VLC_Uploader as VSP_Uploader_Code 
,TSPL_MULTIPLE_DEDUCTION_detail.Vendor_Code,TSPL_MULTIPLE_DEDUCTION_detail.Vendor_Name
,TSPL_MULTIPLE_DEDUCTION_detail.DeductionCode as Ded_Code,TSPL_MULTIPLE_DEDUCTION_detail.Deduction_Desc as Ded_Desc
,isnull(TSPL_MULTIPLE_DEDUCTION_detail.amount,0) as Amount
,case when isnull(TSPL_MULTIPLE_DEDUCTION_HEAD.Trans_Type,'Deduction')='Addition' then IsNull(TSPL_MULTIPLE_DEDUCTION_DETAIL.Amount,0) else 0 end   as ReDedctAmt
from TSPL_MULTIPLE_DEDUCTION_HEAD 
LEFT OUTER JOIN TSPL_MULTIPLE_DEDUCTION_DETAIL ON TSPL_MULTIPLE_DEDUCTION_HEAD.Document_No =TSPL_MULTIPLE_DEDUCTION_DETAIL.Document_No
left outer Join (select distinct TSPL_VLC_MASTER_HEAD.VSP_Code,TSPL_VLC_MASTER_HEAD.VLC_CODE_VLC_Uploader,TSPL_VLC_MASTER_HEAD.MCC from TSPL_VLC_MASTER_HEAD) as TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code = TSPL_MULTIPLE_DEDUCTION_detail.Vendor_Code
where TSPL_MULTIPLE_DEDUCTION_HEAD.IsPosted=1 and TSPL_MULTIPLE_DEDUCTION_HEAD.IsOpening=0 
and convert(date,Document_Date,103)>=convert(date,('" + clsCommon.GetPrintDate(fromDate, "dd/MMM/yyyy") + "'),103) and convert(date,Document_Date,103)<=convert(date,('" + clsCommon.GetPrintDate(ToDate, "dd/MMM/yyyy") + "'),103)
Union All 
Select 'D' Type,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader as VSP_Uploader_Code,TSPL_PAYMENT_PROCESS_DEDUCTION.Vendor_CODE, TSPL_PAYMENT_PROCESS_DEDUCTION.Vendor_NAME, TSPL_PAYMENT_PROCESS_DEDUCTION.Ded_Code,TSPL_PAYMENT_PROCESS_DEDUCTION.Ded_Desc 
,isnull(TSPL_PAYMENT_PROCESS_DEDUCTION.Reduce_Deduc_Amt,0) as Amount,0 as ReDedctAmt
from TSPL_PAYMENT_PROCESS_DEDUCTION 
left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code =TSPL_PAYMENT_PROCESS_DEDUCTION.Vendor_CODE
Left Outer Join TSPL_MULTIPLE_DEDUCTION_HEAD on TSPL_MULTIPLE_DEDUCTION_HEAD.Document_No=TSPL_PAYMENT_PROCESS_DEDUCTION.Doc_No
where  TSPL_PAYMENT_PROCESS_DEDUCTION.Doc_No in ('" + strOldDocNo + "') " + subMCCQry1 + " 
union all
select case when isnull(TSPL_MULTIPLE_DEDUCTION_HEAD.Trans_Type,'Deduction')='Addition' then 'A' else 'D' end Type
,TSPL_VLC_MASTER_HEAD.VLC_CODE_VLC_Uploader as VSP_Uploader_Code 
,TSPL_MULTIPLE_DEDUCTION_detail.Vendor_Code,TSPL_MULTIPLE_DEDUCTION_detail.Vendor_Name
,TSPL_MULTIPLE_DEDUCTION_detail.DeductionCode as Ded_Code,TSPL_MULTIPLE_DEDUCTION_detail.Deduction_Desc as Ded_Desc
,isnull(TSPL_MULTIPLE_DEDUCTION_detail.amount,0) as Amount,0 as ReDedctAmt
from TSPL_MULTIPLE_DEDUCTION_HEAD 
LEFT OUTER JOIN TSPL_MULTIPLE_DEDUCTION_DETAIL ON TSPL_MULTIPLE_DEDUCTION_HEAD.Document_No =TSPL_MULTIPLE_DEDUCTION_DETAIL.Document_No
left outer Join (select distinct TSPL_VLC_MASTER_HEAD.VSP_Code,TSPL_VLC_MASTER_HEAD.VLC_CODE_VLC_Uploader,TSPL_VLC_MASTER_HEAD.MCC from TSPL_VLC_MASTER_HEAD) as TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code = TSPL_MULTIPLE_DEDUCTION_detail.Vendor_Code
where TSPL_MULTIPLE_DEDUCTION_HEAD.IsPosted=1 and TSPL_MULTIPLE_DEDUCTION_HEAD.IsOpening=1 "
        If clsCommon.myLen(strOLDFromDate) > 0 Then
            qry += " and convert(date,Document_Date,103)>=convert(date,('" + strOLDFromDate + "'),103) "
        End If
        If clsCommon.myLen(strOLDToDate) > 0 Then
            qry += " and convert(date,Document_Date,103)<=convert(date,('" + strOLDToDate + "'),103) "
        End If

        qry += subMCCQry1 + "
) Final " + subQryWhere + " group by  Final.Ded_Code ,[Type] " + subQry + " having sum(Amount)>0 order by [Type] desc"
        Return qry
    End Function
    Sub Print(ByVal isPrint As Boolean, Optional ByVal isPrerint As Boolean = False)
        Try

            Dim dt1 As New DataTable
            Dim qry As String = Nothing
            'Dim strDocNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Doc_No from TSPL_PAYMENT_PROCESS_head where convert(date,From_Date,103)>=convert(date,('" + fromDate.Value + "'),103) and convert(date,To_Date,103)<=convert(date,('" + ToDate.Value + "'),103)"))
            Dim strDocNo As String = Nothing
            Dim strDocQry = "select Doc_No from TSPL_PAYMENT_PROCESS_head where convert(date,From_Date,103)>=convert(date,('" + fromDate.Value + "'),103) and convert(date,To_Date,103)<=convert(date,('" + ToDate.Value + "'),103)"
            Dim dtDocNo As DataTable = clsDBFuncationality.GetDataTable(strDocQry)
            If dtDocNo IsNot Nothing AndAlso dtDocNo.Rows.Count > 0 Then
                Dim arr As New ArrayList
                For Each dr As DataRow In dtDocNo.Rows
                    arr.Add(clsCommon.myCstr(dr("Doc_No")))
                Next
                strDocNo = clsCommon.GetMulcallString(arr)
            End If

            Dim strOldDocNo As String = Nothing
            If clsCommon.myLen(strDocNo) > 0 Then
                strOldDocNo = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT TOP 1 Doc_No  from TSPL_PAYMENT_PROCESS_head where convert(date,To_Date,103)<=convert(date,('" + ToDate.Value + "'),103) AND TSPL_PAYMENT_PROCESS_head.Doc_No in (" + strDocNo + ") ORDER BY From_Date DESC"))
            Else
                strDocNo = "''"
                strOldDocNo = "''"
            End If

            Dim subMCCQry1 As String = Nothing
            Dim subMCCQry2 As String = Nothing
            If txtMCC.Value.Length > 0 Then
                subMCCQry1 = " and TSPL_VLC_MASTER_HEAD.MCC='" + txtMCC.Value + "'"
                'subMCCQry2 = " and TSPL_MULTIPLE_DEDUCTION_HEAD.MCC_Code='" + txtMCC.Value + "'"
            End If

            Dim subQryWhere As String = Nothing
            If txtDeduction.Value.Length > 0 Then
                subQryWhere = "where Final.Ded_Code='" + txtDeduction.Value + "'"
            End If

            Dim subQry As String = Nothing
            Dim subDCSQry As String = Nothing
            If chkDCSWise.Checked = True Then
                subDCSQry = ",Max(VSP_Uploader_Code) as DCSCode,Max(Vendor_NAME) As 'DCS Name'"
                subQry = ",Final.Vendor_CODE"
            End If

            If rdbOldOutstanding.Checked Then ''1
                qry = "select case when isnull(Final.Type,'D')='D' then 'Deduction' when isnull(Final.Type,'')='A' then 'Addition' else '' end Type,Max(Ded_code+'-'+Ded_Desc) as DeductionName
                        " + subDCSQry + ", sum(Amount) as [Amount] from ( 
                        Select 'D' Type,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader as VSP_Uploader_Code,TSPL_PAYMENT_PROCESS_DEDUCTION.Vendor_CODE, TSPL_PAYMENT_PROCESS_DEDUCTION.Vendor_NAME, TSPL_PAYMENT_PROCESS_DEDUCTION.Ded_Code,TSPL_PAYMENT_PROCESS_DEDUCTION.Ded_Desc 
                        ,isnull(TSPL_PAYMENT_PROCESS_DEDUCTION.Reduce_Deduc_Amt,0) as Amount
                        from TSPL_PAYMENT_PROCESS_DEDUCTION 
                        left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code =TSPL_PAYMENT_PROCESS_DEDUCTION.Vendor_CODE
                        where  TSPL_PAYMENT_PROCESS_DEDUCTION.Doc_No in ('" + strOldDocNo + "') " + subMCCQry1 + "
                                        union all
                        select case when isnull(TSPL_MULTIPLE_DEDUCTION_HEAD.Trans_Type,'Deduction')='Addition' then 'A' else 'D' end Type
                        ,TSPL_VLC_MASTER_HEAD.VLC_CODE_VLC_Uploader as VSP_Uploader_Code 
                        ,TSPL_MULTIPLE_DEDUCTION_detail.Vendor_Code,TSPL_MULTIPLE_DEDUCTION_detail.Vendor_Name
                        ,TSPL_MULTIPLE_DEDUCTION_detail.DeductionCode as Ded_Code,TSPL_MULTIPLE_DEDUCTION_detail.Deduction_Desc as Ded_Desc
                        ,isnull(TSPL_MULTIPLE_DEDUCTION_detail.amount,0) as Amount
                        from TSPL_MULTIPLE_DEDUCTION_HEAD 
                        LEFT OUTER JOIN TSPL_MULTIPLE_DEDUCTION_DETAIL ON TSPL_MULTIPLE_DEDUCTION_HEAD.Document_No =TSPL_MULTIPLE_DEDUCTION_DETAIL.Document_No
                        left outer Join (select distinct TSPL_VLC_MASTER_HEAD.VSP_Code,TSPL_VLC_MASTER_HEAD.VLC_CODE_VLC_Uploader,TSPL_VLC_MASTER_HEAD.MCC from TSPL_VLC_MASTER_HEAD) as TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code = TSPL_MULTIPLE_DEDUCTION_detail.Vendor_Code
                        where TSPL_MULTIPLE_DEDUCTION_HEAD.IsPosted=1 and TSPL_MULTIPLE_DEDUCTION_HEAD.IsOpening=1
                        and TSPL_MULTIPLE_DEDUCTION_HEAD.IsOpening=1 and convert(date,Document_Date,103)<convert(date,('" + fromDate.Value + "'),103) " + subMCCQry1 + "
                        ) Final " + subQryWhere + " group by  Final.Ded_Code ,[Type] " + subQry + " having sum(Amount)>0 order by [Type] desc"

            ElseIf rdbOldCurrent.Checked Then ''2
                qry = "select case when isnull(Final.Type,'D')='D' then 'Deduction' when isnull(Final.Type,'')='A' then 'Addition' else '' end Type
                       ,Max(Ded_code+'-'+Ded_Desc) as DeductionName " + subDCSQry + ", sum(Amount) as 'Opening+Sale',sum(Amount-ReDedctAmt) As 'Amt Deducted',Sum(ReDedctAmt) As 'Balance Amount' from ( 
                        select case when isnull(TSPL_MULTIPLE_DEDUCTION_HEAD.Trans_Type,'Deduction')='Addition' then 'A' else 'D' end Type
                        ,TSPL_VLC_MASTER_HEAD.VLC_CODE_VLC_Uploader as VSP_Uploader_Code 
                        ,TSPL_MULTIPLE_DEDUCTION_detail.Vendor_Code,TSPL_MULTIPLE_DEDUCTION_detail.Vendor_Name
                        ,TSPL_MULTIPLE_DEDUCTION_detail.DeductionCode as Ded_Code,TSPL_MULTIPLE_DEDUCTION_detail.Deduction_Desc as Ded_Desc
                        ,isnull(TSPL_MULTIPLE_DEDUCTION_detail.amount,0) as Amount,0 As ReDedctAmt
                        from TSPL_MULTIPLE_DEDUCTION_HEAD 
                        LEFT OUTER JOIN TSPL_MULTIPLE_DEDUCTION_DETAIL ON TSPL_MULTIPLE_DEDUCTION_HEAD.Document_No =TSPL_MULTIPLE_DEDUCTION_DETAIL.Document_No
                        left outer Join (select distinct TSPL_VLC_MASTER_HEAD.VSP_Code,TSPL_VLC_MASTER_HEAD.VLC_CODE_VLC_Uploader,TSPL_VLC_MASTER_HEAD.MCC from TSPL_VLC_MASTER_HEAD) as TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code = TSPL_MULTIPLE_DEDUCTION_detail.Vendor_Code
                        where TSPL_MULTIPLE_DEDUCTION_HEAD.IsPosted=1 " + subMCCQry1 + "
                        and TSPL_MULTIPLE_DEDUCTION_HEAD.IsOpening=1 and convert(date,Document_Date,103)<=convert(date,('" + ToDate.Value + "'),103) 
                        union all  
                        select 'D' Type,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader as VSP_Uploader_Code,TSPL_PAYMENT_PROCESS_DEDUCTION.Vendor_CODE, TSPL_PAYMENT_PROCESS_DEDUCTION.Vendor_NAME, TSPL_PAYMENT_PROCESS_DEDUCTION.Ded_Code,TSPL_PAYMENT_PROCESS_DEDUCTION.Ded_Desc
                        ,isnull(TSPL_PAYMENT_PROCESS_DEDUCTION.Amount,0) as Amount,IsNull(TSPL_PAYMENT_PROCESS_DEDUCTION.Reduce_Deduc_Amt,0) As ReDedctAmt from TSPL_PAYMENT_PROCESS_DEDUCTION 
                        left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code =TSPL_PAYMENT_PROCESS_DEDUCTION.Vendor_CODE
                        inner join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No=TSPL_PAYMENT_PROCESS_DEDUCTION.AP_Invoice_No
                        inner join TSPL_MULTIPLE_DEDUCTION_DETAIL on TSPL_MULTIPLE_DEDUCTION_DETAIL.Against_Deduction_DocNo=TSPL_VENDOR_INVOICE_HEAD.Document_No
                        inner join TSPL_MULTIPLE_DEDUCTION_HEAD on TSPL_MULTIPLE_DEDUCTION_HEAD.Document_No=TSPL_MULTIPLE_DEDUCTION_DETAIL.Document_No
                        where  TSPL_PAYMENT_PROCESS_DEDUCTION.Doc_No in (" + strDocNo + ") and TSPL_MULTIPLE_DEDUCTION_HEAD.IsOpening=0 " + subMCCQry1 + "
                        union all
                        select   'A' Type,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader as VSP_Uploader_Code,TSPL_PAYMENT_PROCESS_CREDIT_NOTE.Vendor_CODE,TSPL_PAYMENT_PROCESS_CREDIT_NOTE.Vendor_NAME, TSPL_MULTIPLE_DEDUCTION_DETAIL.DeductionCode as Ded_Code,TSPL_MULTIPLE_DEDUCTION_DETAIL.Deduction_Desc as Ded_Desc,isnull(TSPL_PAYMENT_PROCESS_CREDIT_NOTE.Amount,0) as Amount,0 As ReDedctAmt 
                        from TSPL_PAYMENT_PROCESS_CREDIT_NOTE
                        left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code =TSPL_PAYMENT_PROCESS_CREDIT_NOTE.Vendor_CODE
                        inner join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No=TSPL_PAYMENT_PROCESS_CREDIT_NOTE.AP_Invoice_No
                        inner join TSPL_MULTIPLE_DEDUCTION_DETAIL on TSPL_MULTIPLE_DEDUCTION_DETAIL.Against_Deduction_DocNo=TSPL_VENDOR_INVOICE_HEAD.Document_No
                        inner join TSPL_MULTIPLE_DEDUCTION_HEAD on TSPL_MULTIPLE_DEDUCTION_HEAD.Document_No=TSPL_MULTIPLE_DEDUCTION_DETAIL.Document_No
                        where  TSPL_PAYMENT_PROCESS_CREDIT_NOTE.Doc_No in (" + strDocNo + ") and TSPL_MULTIPLE_DEDUCTION_HEAD.IsOpening=0 " + subMCCQry1 + "
                        ) Final " + subQryWhere + " group by  Final.Ded_Code ,[Type] " + subQry + " having  sum(Amount)>0 order by [Type] desc "

            ElseIf rdbCurrentStanding.Checked = True Then  ''3
                qry = "select case when isnull(Final.Type,'D')='D' then 'Deduction' when isnull(Final.Type,'')='A' then 'Addition' else '' end Type
                       ,Max(Ded_code+'-'+Ded_Desc) as DeductionName " + subDCSQry + ", sum(Amount * RI) as [Amount] from (
                        select case when isnull(TSPL_MULTIPLE_DEDUCTION_HEAD.Trans_Type,'Deduction')='Addition' then 'A' else 'D' end Type
                        ,TSPL_VLC_MASTER_HEAD.VLC_CODE_VLC_Uploader as VSP_Uploader_Code 
                        ,TSPL_MULTIPLE_DEDUCTION_detail.Vendor_Code,TSPL_MULTIPLE_DEDUCTION_detail.Vendor_Name
                        ,TSPL_MULTIPLE_DEDUCTION_detail.DeductionCode as Ded_Code,TSPL_MULTIPLE_DEDUCTION_detail.Deduction_Desc as Ded_Desc
                        ,isnull(TSPL_MULTIPLE_DEDUCTION_detail.amount,0) as Amount,1 as RI
                        from TSPL_MULTIPLE_DEDUCTION_HEAD 
                        LEFT OUTER JOIN TSPL_MULTIPLE_DEDUCTION_DETAIL ON TSPL_MULTIPLE_DEDUCTION_HEAD.Document_No =TSPL_MULTIPLE_DEDUCTION_DETAIL.Document_No
                        left outer Join (select distinct TSPL_VLC_MASTER_HEAD.VSP_Code,TSPL_VLC_MASTER_HEAD.VLC_CODE_VLC_Uploader,TSPL_VLC_MASTER_HEAD.MCC from TSPL_VLC_MASTER_HEAD) as TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code = TSPL_MULTIPLE_DEDUCTION_detail.Vendor_Code
                        where TSPL_MULTIPLE_DEDUCTION_HEAD.IsPosted=1 and TSPL_MULTIPLE_DEDUCTION_HEAD.IsOpening=1 " + subMCCQry1 + "
                        and TSPL_MULTIPLE_DEDUCTION_HEAD.IsOpening=1 and convert(date,Document_Date,103)<convert(date,('" + fromDate.Value + "'),103)
                        union all
                        Select 'D' Type,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader as VSP_Uploader_Code,TSPL_PAYMENT_PROCESS_DEDUCTION.Vendor_CODE, TSPL_PAYMENT_PROCESS_DEDUCTION.Vendor_NAME, TSPL_PAYMENT_PROCESS_DEDUCTION.Ded_Code,TSPL_PAYMENT_PROCESS_DEDUCTION.Ded_Desc 
                        ,isnull(TSPL_PAYMENT_PROCESS_DEDUCTION.Reduce_Deduc_Amt,0) as Amount,1 as RI
                        from TSPL_PAYMENT_PROCESS_DEDUCTION 
                        left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code =TSPL_PAYMENT_PROCESS_DEDUCTION.Vendor_CODE
                        where  TSPL_PAYMENT_PROCESS_DEDUCTION.Doc_No in ('" + strOldDocNo + "') " + subMCCQry1 + "
                        union all
                        select 'D' Type,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader as VSP_Uploader_Code,TSPL_PAYMENT_PROCESS_DEDUCTION.Vendor_CODE, TSPL_PAYMENT_PROCESS_DEDUCTION.Vendor_NAME, TSPL_PAYMENT_PROCESS_DEDUCTION.Ded_Code,TSPL_PAYMENT_PROCESS_DEDUCTION.Ded_Desc 
                        ,isnull(TSPL_PAYMENT_PROCESS_DEDUCTION.Amount,0) as Amount,-1 as RI
                        from TSPL_PAYMENT_PROCESS_DEDUCTION 
                        left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code =TSPL_PAYMENT_PROCESS_DEDUCTION.Vendor_CODE
                        inner join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No=TSPL_PAYMENT_PROCESS_DEDUCTION.AP_Invoice_No
                        inner join TSPL_MULTIPLE_DEDUCTION_DETAIL on TSPL_MULTIPLE_DEDUCTION_DETAIL.Against_Deduction_DocNo=TSPL_VENDOR_INVOICE_HEAD.Document_No
                        inner join TSPL_MULTIPLE_DEDUCTION_HEAD on TSPL_MULTIPLE_DEDUCTION_HEAD.Document_No=TSPL_MULTIPLE_DEDUCTION_DETAIL.Document_No
                        where  TSPL_PAYMENT_PROCESS_DEDUCTION.Doc_No in (" + strDocNo + ") and TSPL_MULTIPLE_DEDUCTION_HEAD.IsOpening=0 " + subMCCQry1 + "
                        union all
                        select   'A' Type,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader as VSP_Uploader_Code,TSPL_PAYMENT_PROCESS_CREDIT_NOTE.Vendor_CODE,TSPL_PAYMENT_PROCESS_CREDIT_NOTE.Vendor_NAME, TSPL_MULTIPLE_DEDUCTION_DETAIL.DeductionCode as Ded_Code,TSPL_MULTIPLE_DEDUCTION_DETAIL.Deduction_Desc as Ded_Desc,isnull(TSPL_PAYMENT_PROCESS_CREDIT_NOTE.Amount,0) as Amount,-1 as RI 
                        from TSPL_PAYMENT_PROCESS_CREDIT_NOTE
                        left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code =TSPL_PAYMENT_PROCESS_CREDIT_NOTE.Vendor_CODE
                        inner join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No=TSPL_PAYMENT_PROCESS_CREDIT_NOTE.AP_Invoice_No
                        inner join TSPL_MULTIPLE_DEDUCTION_DETAIL on TSPL_MULTIPLE_DEDUCTION_DETAIL.Against_Deduction_DocNo=TSPL_VENDOR_INVOICE_HEAD.Document_No
                        inner join TSPL_MULTIPLE_DEDUCTION_HEAD on TSPL_MULTIPLE_DEDUCTION_HEAD.Document_No=TSPL_MULTIPLE_DEDUCTION_DETAIL.Document_No
                        where  TSPL_PAYMENT_PROCESS_CREDIT_NOTE.Doc_No in (" + strDocNo + ") and TSPL_MULTIPLE_DEDUCTION_HEAD.IsOpening=0 " + subMCCQry1 + "
                        ) Final " + subQryWhere + " group by  Final.Ded_Code" + subQry + "  ,[Type]  order by [Type] desc"

            ElseIf chkWithOpening.Checked = True Then ''4
                qry = "select case when isnull(Final.Type,'D')='D' then 'Deduction' when isnull(Final.Type,'')='A' then 'Addition' else '' end Type
                       ,Max(Ded_code+'-'+Ded_Desc) as DeductionName " + subDCSQry + ", sum(Amount) as [Amount] from ( 
                        select case when isnull(TSPL_MULTIPLE_DEDUCTION_HEAD.Trans_Type,'Deduction')='Addition' then 'A' else 'D' end Type
                        ,TSPL_VLC_MASTER_HEAD.VLC_CODE_VLC_Uploader as VSP_Uploader_Code 
                        ,TSPL_MULTIPLE_DEDUCTION_detail.Vendor_Code,TSPL_MULTIPLE_DEDUCTION_detail.Vendor_Name
                        ,TSPL_MULTIPLE_DEDUCTION_detail.DeductionCode as Ded_Code,TSPL_MULTIPLE_DEDUCTION_detail.Deduction_Desc as Ded_Desc
                        ,isnull(TSPL_MULTIPLE_DEDUCTION_detail.amount,0) as Amount
                        from TSPL_MULTIPLE_DEDUCTION_HEAD 
                        LEFT OUTER JOIN TSPL_MULTIPLE_DEDUCTION_DETAIL ON TSPL_MULTIPLE_DEDUCTION_HEAD.Document_No =TSPL_MULTIPLE_DEDUCTION_DETAIL.Document_No
                        left outer Join (select distinct TSPL_VLC_MASTER_HEAD.VSP_Code,TSPL_VLC_MASTER_HEAD.VLC_CODE_VLC_Uploader from TSPL_VLC_MASTER_HEAD) as TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code = TSPL_MULTIPLE_DEDUCTION_detail.Vendor_Code
                        where TSPL_MULTIPLE_DEDUCTION_HEAD.IsPosted=1
                        and TSPL_MULTIPLE_DEDUCTION_HEAD.IsOpening=1 and convert(date,Document_Date,103)>=convert(date,('" + fromDate.Value + "'),103) and convert(date,Document_Date,103)<=convert(date,('" + ToDate.Value + "'),103) " + subMCCQry1 + "
                        ) Final " + subQryWhere + " group by  Final.Ded_Code ,[Type]" + subQry + " order by [Type] desc"

            ElseIf chkORD_CD.Checked = True Then ''5
                qry = "select case when isnull(Final.Type,'D')='D' then 'Deduction' when isnull(Final.Type,'')='A' then 'Addition' else '' end Type,Max(Ded_code+'-'+Ded_Desc) as DeductionName
                        " + subDCSQry + ", sum(Amount) as [Amount] from ( 
                        Select 'D' Type,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader as VSP_Uploader_Code,TSPL_PAYMENT_PROCESS_DEDUCTION.Vendor_CODE, TSPL_PAYMENT_PROCESS_DEDUCTION.Vendor_NAME, TSPL_PAYMENT_PROCESS_DEDUCTION.Ded_Code,TSPL_PAYMENT_PROCESS_DEDUCTION.Ded_Desc,TSPL_PAYMENT_PROCESS_DEDUCTION.Reduce_Deduc_Amt as Amount 
                        from TSPL_PAYMENT_PROCESS_DEDUCTION 
                        left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code =TSPL_PAYMENT_PROCESS_DEDUCTION.Vendor_CODE
                        where  TSPL_PAYMENT_PROCESS_DEDUCTION.Doc_No in ('" + strOldDocNo + "')  AND ISNULL(TSPL_PAYMENT_PROCESS_DEDUCTION.Reduce_Deduc_Amt,0)>0  
                        UNION ALL 
                        Select 'D' Type,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader as VSP_Uploader_Code,TSPL_PAYMENT_PROCESS_DEDUCTION.Vendor_CODE, TSPL_PAYMENT_PROCESS_DEDUCTION.Vendor_NAME, TSPL_PAYMENT_PROCESS_DEDUCTION.Ded_Code,TSPL_PAYMENT_PROCESS_DEDUCTION.Ded_Desc,TSPL_PAYMENT_PROCESS_DEDUCTION.Amount-TSPL_PAYMENT_PROCESS_DEDUCTION.Reduce_Deduc_Amt as Amount 
                        from TSPL_PAYMENT_PROCESS_DEDUCTION 
                        left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code =TSPL_PAYMENT_PROCESS_DEDUCTION.Vendor_CODE
                        where  TSPL_PAYMENT_PROCESS_DEDUCTION.Doc_No in (" + strDocNo + ") " + subMCCQry1 + "
                        ) Final " + subQryWhere + " group by  Final.Ded_Code ,[Type] " + subQry + " order by [Type] desc"
            End If

            dt1 = Nothing
            dt1 = clsDBFuncationality.GetDataTable(qry)
            Gv1.MasterTemplate.SummaryRowsBottom.Clear()
            Gv1.DataSource = Nothing
            Gv1.Rows.Clear()
            Gv1.Columns.Clear()
            Gv1.GroupDescriptors.Clear()
            Gv1.MasterTemplate.SummaryRowsBottom.Clear()
            Gv1.MasterView.Refresh()

            If dt1 Is Nothing OrElse dt1.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow("No Data Found to Display", Me.Text)
                Exit Sub
            Else
                Gv1.DataSource = dt1
                RadPageView1.SelectedPage = RadPageViewPage2
                SetGridFormat()
                ReStoreGridLayout()
            End If
            EnableDisableControl(False)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Sub SetGridFormatUDP()
        Gv1.AutoExpandGroups = True
        Gv1.ShowGroupPanel = True
        Gv1.ShowRowHeaderColumn = False
        Gv1.AllowAddNewRow = False
        Gv1.AllowDeleteRow = False
        Gv1.EnableFiltering = True
        Gv1.ShowFilteringRow = True

        For ii As Integer = 0 To Gv1.Columns.Count - 1
            Gv1.Columns(ii).ReadOnly = True
            Gv1.Columns(ii).BestFit()
        Next

        Gv1.AutoSizeRows = True
        Gv1.BestFitColumns()
        Gv1.MasterTemplate.AutoExpandGroups = True

        Dim summaryRowItem As New GridViewSummaryRowItem()
        If rdbOldOutstanding.Checked OrElse rdbOldCurrent.Checked OrElse rdbCurrentStanding.Checked Then
            Gv1.Columns("Opening+Sale").FormatString = "{0:n2}"
            Dim item1 As New GridViewSummaryItem("Opening+Sale", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item1)
            Gv1.Columns("Opening+Sale").Width = 200
        Else
            Gv1.Columns("Amount").FormatString = "{0:n2}"
            Dim item1 As New GridViewSummaryItem("Amount", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item1)
            Gv1.Columns("Amount").Width = 200
        End If
        'Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

        If rdbOldOutstanding.Checked OrElse rdbOldCurrent.Checked OrElse rdbCurrentStanding.Checked Then
            Gv1.Columns("Amt Deducted").FormatString = "{0:n2}"
            Dim item2 As New GridViewSummaryItem("Amt Deducted", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item2)
            'Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
            Gv1.AutoSizeRows = True
            Gv1.BestFitColumns()
            Gv1.Columns("Amt Deducted").Width = 200
            Gv1.MasterTemplate.AutoExpandGroups = True

            Gv1.Columns("Balance Amount").FormatString = "{0:n2}"
            Dim item3 As New GridViewSummaryItem("Balance Amount", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item3)
            'Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
            Gv1.AutoSizeRows = True
            Gv1.BestFitColumns()
            Gv1.Columns("Balance Amount").Width = 200
            Gv1.MasterTemplate.AutoExpandGroups = True
        End If
        If rdbOldOutstanding.Checked OrElse rdbCurrentStanding.Checked Then
            Gv1.Columns("Amt Deducted").IsVisible = False
            Gv1.Columns("Opening+Sale").IsVisible = False
        End If
        Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        Gv1.GroupDescriptors.Add(New GridGroupByExpression("Type as Type format ""{0}: {1}"" Group By Type"))
        If chkDCSWise.Checked Then
            Gv1.GroupDescriptors.Add(New GridGroupByExpression("Type as Type format ""{0}: {1}"" Group By DeductionName"))
        End If
    End Sub

    Sub SetGridFormat()
        Gv1.AutoExpandGroups = True
        Gv1.ShowGroupPanel = True
        Gv1.ShowRowHeaderColumn = False
        Gv1.AllowAddNewRow = False
        Gv1.AllowDeleteRow = False
        Gv1.EnableFiltering = True
        Gv1.ShowFilteringRow = True

        For ii As Integer = 0 To Gv1.Columns.Count - 1
            Gv1.Columns(ii).ReadOnly = True
            Gv1.Columns(ii).BestFit()
        Next

        Gv1.AutoSizeRows = True
        Gv1.BestFitColumns()
        Gv1.MasterTemplate.AutoExpandGroups = True

        Dim summaryRowItem As New GridViewSummaryRowItem()
        If rdbOldCurrent.Checked Then
            Gv1.Columns("Opening+Sale").FormatString = "{0:n2}"
            Dim item1 As New GridViewSummaryItem("Opening+Sale", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item1)
            Gv1.Columns("Opening+Sale").Width = 200
        Else
            Gv1.Columns("Amount").FormatString = "{0:n2}"
            Dim item1 As New GridViewSummaryItem("Amount", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item1)
            Gv1.Columns("Amount").Width = 200
        End If
        'Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

        If rdbOldCurrent.Checked Then
            Gv1.Columns("Amt Deducted").FormatString = "{0:n2}"
            Dim item2 As New GridViewSummaryItem("Amt Deducted", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item2)
            'Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
            'Gv1.GroupDescriptors.Add(New GridGroupByExpression("Type as Type format ""{0}: {1}"" Group By Type"))
            Gv1.AutoSizeRows = True
            Gv1.BestFitColumns()
            Gv1.Columns("Amt Deducted").Width = 200
            Gv1.MasterTemplate.AutoExpandGroups = True

            Gv1.Columns("Balance Amount").FormatString = "{0:n2}"
            Dim item3 As New GridViewSummaryItem("Balance Amount", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item3)
            'Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
            'Gv1.GroupDescriptors.Add(New GridGroupByExpression("Type as Type format ""{0}: {1}"" Group By Type"))
            Gv1.AutoSizeRows = True
            Gv1.BestFitColumns()
            Gv1.Columns("Balance Amount").Width = 200
            Gv1.MasterTemplate.AutoExpandGroups = True
        End If
        Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        Gv1.GroupDescriptors.Add(New GridGroupByExpression("Type as Type format ""{0}: {1}"" Group By Type"))
        If chkDCSWise.Checked Then
            Gv1.GroupDescriptors.Add(New GridGroupByExpression("Type as Type format ""{0}: {1}"" Group By DeductionName"))
        End If
    End Sub
    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Reset()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub


    Private Sub ExportGrid(ByVal exporter As EnumExportTo)
        Try
            If Gv1.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow("No Data Found to Export", Me.Text)
                Exit Sub
            End If
            Dim StrReportName As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptTemporaryPaymentDeductionSummary & "'"))
            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("Name : " & StrReportName)
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy")) + " ")
            If exporter = EnumExportTo.Excel Then
                transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
                clsCommon.MyExportToExcelGrid(StrReportName, Gv1, arrHeader, Me.Text)
            Else
                transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
                ' clsCommon.MyExportToPDF(StrReportName, Gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)

                Dim style As New GridPrintStyle()
                'style.FitWidthMode = PrintFitWidthMode.FitPageWidth
                'style.PrintGrouping = True
                style.HeaderCellBackColor = Color.White
                style.GroupRowBackColor = Color.White
                style.SummaryCellBackColor = Color.White
                'style.PrintSummaries = False
                'style.PrintHeaderOnEachPage = True
                'style.PrintHiddenColumns = False
                Gv1.PrintStyle = style

                Dim doc As New clsMyPrintDocument()

                doc.Margins.Top = 50
                doc.Margins.Bottom = 50
                doc.Margins.Left = 50
                doc.Margins.Right = 50
                doc.HeaderHeight = 90
                doc.Landscape = True
                doc.AssociatedObject = Gv1

                'doc.DocumentName = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptTemporaryPaymentDeductionSummary & "'"))
                doc.DocumentName = objCommonVar.CurrentCompanyName
                'doc.MiddleHeader = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptTemporaryPaymentDeductionSummary & "'"))
                Dim strLocation As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select MCC_Name from TSPL_MCC_MASTER where MCC_Code='" + txtMCC.Value + "'"))
                doc.MiddleHeader = objCommonVar.CurrentCompanyName + Environment.NewLine
                If rdbOldOutstanding.Checked Then
                    doc.MiddleHeader += rdbOldOutstanding.Text
                ElseIf rdbOldCurrent.Checked Then
                    doc.MiddleHeader += rdbOldCurrent.Text
                ElseIf rdbCurrentStanding.Checked Then
                    doc.MiddleHeader += rdbCurrentStanding.Text
                ElseIf chkWithOpening.Checked Then
                    doc.MiddleHeader += chkWithOpening.Text
                ElseIf chkORD_CD.Checked Then
                    doc.MiddleHeader += chkORD_CD.Text
                End If
                doc.MiddleHeader += " Balance Report of" + " " + strLocation + " " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy")
                doc.HeaderFont = New Font("Segoe UI", 10, FontStyle.Bold)

                'doc.LeftUpperText = "Date Range: " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy")
                'doc.LeftUpperFont = New Font("Segoe UI", 8, FontStyle.Regular)

                doc.AssociatedObject = Gv1
                'doc.Print()
                doc.RightFooter = "Page [Page #] of [Total Pages]"

                Dim dialog As New RadPrintPreviewDialog
                dialog.Document = doc
                dialog.ToolMenu.Visible = True
                dialog.Show()
                doc = Nothing

            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK)
        End Try
    End Sub

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        ExportGrid(EnumExportTo.Excel)
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        ExportGrid(EnumExportTo.PDF)
    End Sub


    Private Sub rmsaveLayout_Click(sender As Object, e As EventArgs) Handles rmsaveLayout.Click
        If clsCommon.myLen(PageSetupReport_ID) > 0 Then
            Gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = PageSetupReport_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            Gv1.SaveLayout(obj.GridLayout)
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            obj.GridColumns = Gv1.ColumnCount
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
            End If
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(PageSetupReport_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow("Layout Delete successfully", "Information")
    End Sub

    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(PageSetupReport_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(PageSetupReport_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= Gv1.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To Gv1.Columns.Count - 1 Step ii + 1
                        Gv1.Columns(ii).IsVisible = False
                        Gv1.Columns(ii).VisibleInColumnChooser = True
                    Next
                    Gv1.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub

    Private Sub fromDate_Leave(sender As Object, e As EventArgs) Handles fromDate.Leave
        SetToDateNew()
    End Sub

    Private Sub fromDate_Validated(sender As Object, e As EventArgs) Handles fromDate.Validated
        SetToDateNew()
    End Sub

    Sub SetToDateNew()
        If Not isLoad Then
            Dim PaymentCycleType As String = ""
            Dim PaymentCycleValue As Integer = 0
            ' If Not isLoad Then

            Dim dt As DataTable = clsDBFuncationality.GetDataTable("select top 1 PC_VALUE,PC_TYPE from TSPL_PAYMENT_CYCLE_MASTER ")
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow("No Payment Cycle found on current MCC/Location")
                Exit Sub
            End If
            PaymentCycleType = clsCommon.myCstr(dt.Rows(0)("PC_TYPE"))
            PaymentCycleValue = clsCommon.myCdbl(dt.Rows(0)("PC_VALUE"))
            Dim dtCurr As DateTime = clsCommon.GETSERVERDATE()
            If clsCommon.CompairString(PaymentCycleType, "Day") = CompairStringResult.Equal Then
                If fromDate.Value.Day Mod PaymentCycleValue <> 1 And (Not PaymentCycleValue = 1) Then
                    clsCommon.MyMessageBoxShow("Date can only be first day of month or at interval of " & PaymentCycleValue & " Day, Because MCC has payment Cycle of " & PaymentCycleValue & " Day ")
                    fromDate.Value = New Date(dtCurr.Year, dtCurr.Month, 1)
                    ToDate.Value = fromDate.Value
                    Exit Sub
                End If
                ToDate.Value = fromDate.Value.AddDays(PaymentCycleValue - 1)

                If fromDate.Value.Month <> ToDate.Value.Month Then
                    ToDate.Value = New Date(fromDate.Value.Year, fromDate.Value.Month, 1).AddMonths(1).AddDays(-1)
                End If
                Dim dtNxtPay As DateTime = ToDate.Value.AddDays(Math.Ceiling(PaymentCycleValue / 2.0))
                If fromDate.Value.Month <> dtNxtPay.Month Then
                    ToDate.Value = New Date(fromDate.Value.Year, fromDate.Value.Month, 1).AddMonths(1).AddDays(-1)
                End If
            ElseIf clsCommon.CompairString(PaymentCycleType, "Month") = CompairStringResult.Equal Then
                If clsCommon.myCdbl(clsCommon.GetPrintDate(fromDate.Value, "dd")) <> 1 Then
                    clsCommon.MyMessageBoxShow("Date can only be first day of month, Because MCC has payment Cycle of Month Type")
                    fromDate.Value = "01/" & DatePart(DateInterval.Month, dtCurr) & "/" & DatePart(DateInterval.Year, dtCurr)
                    ToDate.Value = "01/" & DatePart(DateInterval.Month, dtCurr) & "/" & DatePart(DateInterval.Year, dtCurr)
                    Exit Sub
                End If
                ToDate.Value = DateAdd(DateInterval.Month, PaymentCycleValue, fromDate.Value)
            ElseIf clsCommon.CompairString(PaymentCycleType, "Year") = CompairStringResult.Equal Then
                If clsCommon.myCdbl(clsCommon.GetPrintDate(fromDate.Value, "dd")) <> 1 Then
                    clsCommon.MyMessageBoxShow("Date can only be first day of month, Because MCC has payment Cycle of Year Type")
                    fromDate.Value = "01/" & DatePart(DateInterval.Month, dtCurr) & "/" & DatePart(DateInterval.Year, dtCurr)
                    ToDate.Value = "01/" & DatePart(DateInterval.Month, dtCurr) & "/" & DatePart(DateInterval.Year, dtCurr)
                    Exit Sub
                End If
                ToDate.Value = DateAdd(DateInterval.Year, PaymentCycleValue, fromDate.Value)
            ElseIf clsCommon.CompairString(PaymentCycleType, "Week") = CompairStringResult.Equal Then
                Dim today As Date = fromDate.Value
                Dim dayDiff As Integer = today.DayOfWeek - IIf(PaymentCycleValue = 1, DayOfWeek.Sunday, IIf(PaymentCycleValue = 2, DayOfWeek.Monday, IIf(PaymentCycleValue = 3, DayOfWeek.Tuesday, IIf(PaymentCycleValue = 4, DayOfWeek.Wednesday, IIf(PaymentCycleValue = 5, DayOfWeek.Thursday, IIf(PaymentCycleValue = 6, DayOfWeek.Friday, DayOfWeek.Saturday))))))
                fromDate.Value = today.AddDays(-dayDiff)
                ToDate.Value = fromDate.Value.AddDays(6)
            End If
        End If
    End Sub

    Private Sub chkORD_CD_ToggleStateChanged(sender As Object, args As StateChangedEventArgs)
        If chkORD_CD.Checked = True AndAlso chkWithOpening.Checked = True Then
            chkWithOpening.Checked = False
        End If
    End Sub

    Private Sub chkWithOpening_ToggleStateChanged(sender As Object, args As StateChangedEventArgs)
        If chkWithOpening.Checked = True AndAlso chkORD_CD.Checked = True Then
            chkORD_CD.Checked = False
        End If
    End Sub

    Private Sub txtMCC__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtMCC._MYValidating
        Try
            Dim qry As String = "select MCC_Code as Code,MCC_NAME as Name,TSPL_MCC_MASTER.plant_code as [Plant Code],tspl_location_master.location_desc as [Plant Name] from TSPL_MCC_MASTER left join tspl_location_master on tspl_location_master.location_code=TSPL_MCC_MASTER.plant_code"
            txtMCC.Value = clsCommon.ShowSelectForm("vbaMccm", qry, "Code", "", txtMCC.Value, "Code", isButtonClicked)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text, MessageBoxButtons.OK)
        End Try
    End Sub

    Private Sub txtDeduction__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtDeduction._MYValidating
        Try
            Dim qry As String = "select Code,Description from TSPL_DEDUCTION_MASTER"
            txtDeduction.Value = clsCommon.ShowSelectForm("vbaMccm", qry, "Code", "Ded_Grp_Code='DEDUCTION'", txtDeduction.Value, "Description", isButtonClicked)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text, MessageBoxButtons.OK)
        End Try
    End Sub
End Class
