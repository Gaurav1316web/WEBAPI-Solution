Imports System.IO
Imports common


'by Sanjay - Create new report 
Public Class rptTemporaryPaymentDeductionSummary
    Inherits FrmMainTranScreen
    Dim isLoad As Boolean = False
    Dim dtPrint As DataTable = Nothing
    Dim AreaWiseBilling As Boolean = False
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
        AreaWiseBilling = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AreaWiseBilling, clsFixedParameterCode.AreaWiseBilling, Nothing)) = 1)
        fndArea.Visible = AreaWiseBilling
        MyLabel2.Visible = AreaWiseBilling
    End Sub
    Sub Reset()
        EnableDisableControl(True)
        Gv1.DataSource = Nothing
        Gv1.Rows.Clear()
        Gv1.Columns.Clear()
        Gv1.MasterTemplate.SummaryRowsBottom.Clear()
        RadPageView1.SelectedPage = RadPageViewPage1
        rbtnAll.Checked = True
        txtMCC.Value = ""
        fndArea.Value = ""
        'fndMultDCS.arrValueMember = Nothing
    End Sub

    Private Sub EnableDisableControl(ByVal val As Boolean)
        RadGroupBox2.Enabled = val
        RadGroupBox3.Enabled = val
        txtDeduction.Enabled = val
        txtMCC.Enabled = val
        chkDCSWise.Enabled = val
        RadGroupBox1.Enabled = val
        fndMultDCS.Enabled = val
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        btnPrint.Text = Nothing
        PageSetupReport_ID = clsCommon.myCstr(MyBase.Form_ID)
        GetReportID()
        If chkCurrntCycle.Checked Then
            Print(False)
            ' PrintChkwiseData(False)
            Exit Sub
        End If
        If rdbDocumentWise.Checked Then
            DocumentWiseQuery()
        ElseIf rdbDocumentWiseDetail.Checked Then
            DocumentWiseDetailQuery()
        ElseIf clsCommon.CompairString(objCommonVar.CurrComp_Code1, "UDP") = CompairStringResult.Equal OrElse
                clsCommon.CompairString(objCommonVar.CurrComp_Code1, "ALW") = CompairStringResult.Equal OrElse
                clsCommon.CompairString(objCommonVar.CurrComp_Code1, "CHT") = CompairStringResult.Equal OrElse
            clsCommon.CompairString(objCommonVar.CurrComp_Code1, "JPR") = CompairStringResult.Equal OrElse
             clsCommon.CompairString(objCommonVar.CurrComp_Code1, "RJS") = CompairStringResult.Equal Then
            PrintUDP(False)
        Else
            Print(False)

        End If
        'If clsCommon.CompairString(objCommonVar.CurrComp_Code1, "UDP") = CompairStringResult.Equal OrElse
        '        clsCommon.CompairString(objCommonVar.CurrComp_Code1, "ALW") = CompairStringResult.Equal OrElse
        '        clsCommon.CompairString(objCommonVar.CurrComp_Code1, "CHT") = CompairStringResult.Equal OrElse
        '    clsCommon.CompairString(objCommonVar.CurrComp_Code1, "JPR") = CompairStringResult.Equal OrElse
        '     clsCommon.CompairString(objCommonVar.CurrComp_Code1, "RJS") = CompairStringResult.Equal Then
        '    PrintUDP(False)
        'Else
        '    Print(False)
        'End If
        btnPrint.Text = "Print"
    End Sub

    Sub DocumentWiseDetailQuery()
        Try
            Dim qry As String = " WITH BASE AS
(
    SELECT
        AP_Invoice_No,
		Document_type,
         (CONVERT(varchar(20), AP_Invoice_Date, 103)) AS AP_Invoice_Date ,
        ISNULL(Description,'') AS DeductionName,
        VLC_Code_VLC_Uploader AS DCSCode,
        Document_Total,
        (Amount - Reduce_Deduc_Amt) AS ConsumedAmt
    FROM
    (
        /* -------- Vendor Deduction -------- */
        SELECT
            D.AP_Invoice_No,
            V.Posting_Date AS AP_Invoice_Date,
            V.Document_Total,
            D.Ded_Desc AS Description,
            VLC.VLC_Code_VLC_Uploader,
            D.Amount,
            D.Reduce_Deduc_Amt,
            H.To_Date AS Document_Date,
			v.Document_Type
        FROM TSPL_PAYMENT_PROCESS_DEDUCTION D
        LEFT JOIN TSPL_VENDOR_INVOICE_HEAD V
            ON V.Document_No = D.AP_Invoice_No
        LEFT JOIN TSPL_PAYMENT_PROCESS_HEAD H
            ON H.Doc_No = D.Doc_No
        LEFT JOIN TSPL_VLC_MASTER_HEAD VLC
            ON VLC.VSP_Code = V.Vendor_Code

        UNION ALL

        /* -------- MCC Sale Deduction -------- */
        SELECT
            M.AR_Invoice_No,
            C.Posting_Date,
            C.Document_Total,
            DM.Description,
            VLC.VLC_Code_VLC_Uploader,
            M.Amount,
            M.Reduce_Deduc_Amt,
            H.To_Date,
			c.Document_Type
        FROM TSPL_PAYMENT_PROCESS_MCC_SALE M
        LEFT JOIN TSPL_SD_SHIPMENT_HEAD S
            ON S.Document_Code = M.Shipment_Doc_No
        LEFT JOIN TSPL_Customer_Invoice_Head C
            ON C.Document_No = M.AR_Invoice_No
        LEFT JOIN TSPL_PAYMENT_PROCESS_HEAD H
            ON H.Doc_No = M.Doc_No
        LEFT JOIN TSPL_CUSTOMER_VENDOR_MAPPING CVM
            ON CVM.Cust_Code = S.Customer_Code
        LEFT JOIN TSPL_VLC_MASTER_HEAD VLC
            ON VLC.VSP_Code = CVM.Vendor_Code
        LEFT JOIN TSPL_DEDUCTION_MASTER DM
            ON DM.Code = S.Deduction
    ) X
    WHERE
        X.Document_Date BETWEEN '" & clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy ") & "' AND '" & clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy ") & "' "
            If fndMultDCS.arrValueMember IsNot Nothing AndAlso fndMultDCS.arrValueMember.Count > 0 Then
                qry += " and X.VLC_Code_VLC_Uploader In(" + clsCommon.GetMulcallString(fndMultDCS.arrDispalyMember) + ")" + Environment.NewLine
            End If
            qry += " )

SELECT
    AP_Invoice_No,
	case when Document_type='D' then 'Deduction' else'Addition' end as Document_type,
    AP_Invoice_Date,
    DeductionName,
    DCSCode,
    Document_Total,
    ConsumedAmt,
    FinalBal
FROM
(
    /* -------- Detail Rows -------- */
    SELECT
        AP_Invoice_No,
		Document_type,
        AP_Invoice_Date,
        DeductionName,
        DCSCode,
        Document_Total,
        ConsumedAmt,
        NULL AS FinalBal,
        1 AS SortOrder
    FROM BASE

    UNION ALL

    /* -------- Balance Row per AP Invoice -------- */
    SELECT
        AP_Invoice_No,
			max(Document_type)Document_type,
        NULL AS AP_Invoice_Date,
	        'BALANCE' AS DeductionName,
        NULL AS DCSCode,
        MAX(Document_Total) - SUM(ConsumedAmt) as Document_Total,
        NULL,
        MAX(Document_Total) - SUM(ConsumedAmt) AS FinalBal,
        2 AS SortOrder
    FROM BASE
    GROUP BY AP_Invoice_No
) F
ORDER BY
    AP_Invoice_No,
    SortOrder,
    AP_Invoice_Date; "

            Dim Dtdetail As DataTable = Nothing
            Dtdetail = clsDBFuncationality.GetDataTable(qry)
            Gv1.DataSource = Nothing
            Gv1.Rows.Clear()
            Gv1.Columns.Clear()
            Gv1.GroupDescriptors.Clear()
            Gv1.MasterView.Refresh()
            Gv1.GroupDescriptors.Clear()
            Gv1.EnableFiltering = True
            Gv1.MasterTemplate.SummaryRowsBottom.Clear()
            If Dtdetail.Rows.Count > 0 Then
                Gv1.DataSource = Dtdetail
                Gv1.BestFitColumns()
                SetGridFormationDetail()
                ReStoreGridLayout()
                Gv1.MasterTemplate.AutoExpandGroups = True
                RadPageView1.SelectedPage = RadPageViewPage2
                Gv1.BestFitColumns()
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data found to Display", Me.Text)
                Exit Sub
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub DocumentWiseQuery()
        Try
            Dim Qry As String = ""
            Dim dt As New DataTable

            Qry = "  Select *,(Document_Total-ConsumedAmt)FinalBal from ( Select ROW_NUMBER() OVER (ORDER BY AP_Invoice_No)SNo,AP_Invoice_No,(CONVERT(varchar(20), AP_Invoice_Date, 103))AP_Invoice_Date,Isnull((Description),'') as DeductionName,
                            VLC_Code_VLC_Uploader as DCSCode,Document_Total,Balance_Amt,Amount,Reduce_Deduc_Amt,(Amount-Reduce_Deduc_Amt) as ConsumedAmt
                            from (
                            select TSPL_PAYMENT_PROCESS_DEDUCTION.Doc_No as Document_No, TSPL_PAYMENT_PROCESS_HEAD.To_Date as Document_Date,TSPL_PAYMENT_PROCESS_DEDUCTION.AP_Invoice_No ,
                            TSPL_VENDOR_INVOICE_HEAD.Posting_Date as AP_Invoice_Date,TSPL_VENDOR_INVOICE_HEAD.Document_Total,TSPL_VENDOR_INVOICE_HEAD.Balance_Amt,TSPL_VENDOR_INVOICE_HEAD.Document_Type,
                            TSPL_PAYMENT_PROCESS_DEDUCTION.Ded_Code as DeductionCode,TSPL_PAYMENT_PROCESS_DEDUCTION.Ded_Desc as Description,
                            TSPL_PAYMENT_PROCESS_DEDUCTION.Vendor_CODE,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader,TSPL_PAYMENT_PROCESS_DEDUCTION.Amount,
                            TSPL_PAYMENT_PROCESS_DEDUCTION.Reduce_Deduc_Amt,2 as RI,TSPL_VLC_MASTER_HEAD.Active   
                            from TSPL_PAYMENT_PROCESS_DEDUCTION
                            left join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No=TSPL_PAYMENT_PROCESS_DEDUCTION.AP_Invoice_No
                            left  join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_HEAD.Doc_No=TSPL_PAYMENT_PROCESS_DEDUCTION.Doc_No
                            left join TSPL_MULTIPLE_DEDUCTION_DETAIL on TSPL_MULTIPLE_DEDUCTION_DETAIL.Against_Deduction_DocNo=TSPL_PAYMENT_PROCESS_DEDUCTION.AP_Invoice_No 
                            left join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code=TSPL_VENDOR_INVOICE_HEAD.Vendor_Code
                            left join tspl_dcs_addition_deduction on tspl_dcs_addition_deduction.code=TSPL_PAYMENT_PROCESS_DEDUCTION.Ded_Code
                            where 2=2  

                            Union all

                            select TSPL_PAYMENT_PROCESS_MCC_SALE.Doc_No as Document_No, TSPL_PAYMENT_PROCESS_HEAD.To_Date as Document_Date,
                            TSPL_PAYMENT_PROCESS_MCC_SALE.AR_Invoice_No as AP_Invoice_No ,TSPL_Customer_Invoice_Head.Posting_Date as AP_Invoice_Date,
                            TSPL_Customer_Invoice_Head.Document_Total,TSPL_Customer_Invoice_Head.Balance_Amt,'D' as Document_Type,
                            TSPL_SD_SHIPMENT_HEAD.Deduction as DeductionCode,TSPL_DEDUCTION_MASTER.description,
                            TSPL_CUSTOMER_VENDOR_MAPPING.Vendor_Code,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader,TSPL_PAYMENT_PROCESS_MCC_SALE.Amount,TSPL_PAYMENT_PROCESS_MCC_SALE.Reduce_Deduc_Amt,
                            5 as RI,TSPL_VLC_MASTER_HEAD.Active
                            from TSPL_PAYMENT_PROCESS_MCC_SALE
                            left outer join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_Code=TSPL_PAYMENT_PROCESS_MCC_SALE.Shipment_Doc_No
                            left outer join TSPL_Customer_Invoice_Head on TSPL_Customer_Invoice_Head.Document_No=TSPL_PAYMENT_PROCESS_MCC_SALE.AR_Invoice_No
                            left outer join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_HEAD.Doc_No=TSPL_PAYMENT_PROCESS_MCC_SALE.Doc_No
                            left outer join TSPL_CUSTOMER_VENDOR_MAPPING on TSPL_CUSTOMER_VENDOR_MAPPING.Cust_Code=TSPL_SD_SHIPMENT_HEAD.Customer_Code
                            left join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code=TSPL_CUSTOMER_VENDOR_MAPPING.Vendor_Code
                            left  join TSPL_DEDUCTION_MASTER on TSPL_DEDUCTION_MASTER.Code=TSPL_SD_SHIPMENT_HEAD.Deduction
                            where 2=2 )  XX
                            left  join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=xx.Vendor_Code 
                            Where  XX.Document_Date>='" & clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy ") & "' 
                            and XX.Document_Date<='" & clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy ") & "'  "

            If fndMultDCS.arrValueMember IsNot Nothing AndAlso fndMultDCS.arrValueMember.Count > 0 Then
                Qry += " and XX.VLC_Code_VLC_Uploader In(" + clsCommon.GetMulcallString(fndMultDCS.arrValueMember) + ")" + Environment.NewLine
            End If
            Qry += " )XX "

            dt = clsDBFuncationality.GetDataTable(Qry)
            Gv1.DataSource = Nothing
            Gv1.Rows.Clear()
            Gv1.Columns.Clear()
            Gv1.GroupDescriptors.Clear()
            Gv1.MasterView.Refresh()
            Gv1.GroupDescriptors.Clear()
            Gv1.EnableFiltering = True
            Gv1.MasterTemplate.SummaryRowsBottom.Clear()
            If dt.Rows.Count > 0 Then
                Gv1.DataSource = dt
                Gv1.BestFitColumns()
                SetGridFormation()
                ReStoreGridLayout()
                Gv1.MasterTemplate.AutoExpandGroups = True
                RadPageView1.SelectedPage = RadPageViewPage2
                Gv1.BestFitColumns()
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data found to Display", Me.Text)
                Exit Sub
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub SetGridFormationDetail()
        Try

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

            'Gv1.Columns("Reduce_Deduc_Amt").HeaderText = "Reduce Deduc Amt"
            'Gv1.Columns("Reduce_Deduc_Amt").IsVisible = False
            'Gv1.Columns("Reduce_Deduc_Amt").VisibleInColumnChooser = True
            Gv1.Columns("AP_Invoice_No").HeaderText = "AP Invoice No"
            Gv1.Columns("Document_type").HeaderText = "Document type"
            Gv1.Columns("AP_Invoice_Date").HeaderText = "AP Invoice Date"
            Gv1.Columns("DeductionName").HeaderText = "Deduction"
            Gv1.Columns("DCSCode").HeaderText = "DCS Code"
            Gv1.Columns("Document_Total").HeaderText = "Document Total"
            'Gv1.Columns("Balance_Amt").HeaderText = "Balance Amt"
            'Gv1.Columns("Amount").HeaderText = "Amount"
            Gv1.Columns("ConsumedAmt").IsVisible = False
            Gv1.Columns("ConsumedAmt").HeaderText = "Consumed Amt"
            Gv1.Columns("FinalBal").IsVisible = False
            Gv1.Columns("FinalBal").HeaderText = "Final Balance"

            Gv1.ShowGroupPanel = True
            Gv1.MasterTemplate.AutoExpandGroups = True

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub SetGridFormation()
        Try


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

            Gv1.Columns("Reduce_Deduc_Amt").HeaderText = "Reduce Deduc Amt"
            Gv1.Columns("Reduce_Deduc_Amt").IsVisible = False
            Gv1.Columns("Reduce_Deduc_Amt").VisibleInColumnChooser = True
            Gv1.Columns("AP_Invoice_No").HeaderText = "AP Invoice No"
            Gv1.Columns("AP_Invoice_Date").HeaderText = "AP Invoice Date"
            Gv1.Columns("DeductionName").HeaderText = "Deduction"
            Gv1.Columns("DCSCode").HeaderText = "DCS Code"
            Gv1.Columns("Document_Total").HeaderText = "Document Total"
            Gv1.Columns("Balance_Amt").HeaderText = "Balance Amt"
            Gv1.Columns("Amount").HeaderText = "Amount"
            Gv1.Columns("ConsumedAmt").HeaderText = "Consumed Amt"
            Gv1.Columns("FinalBal").HeaderText = "Final Balance"

            Gv1.ShowGroupPanel = True
            Gv1.MasterTemplate.AutoExpandGroups = True

            'Dim summaryRowItem As New GridViewSummaryRowItem()
            'Dim index As Integer = 7
            'For ii As Integer = index To Gv1.Columns.Count - 1
            '    'If clsCommon.CompairString(gvData.Columns(ii).Name, "Zone_Code") <> CompairStringResult.Equal Then
            '    summaryRowItem.Add(New GridViewSummaryItem(Gv1.Columns(ii).Name, "{0:F2}", GridAggregateFunction.Sum))
            '    'End If
            'Next
            'Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
            'Gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub GetReportID()
        Dim VarID As String = ""
        If chkDCSWise.Checked Then
            VarID += "_DW"
        End If

        If rdbOldOutstanding.Checked Then
            VarID += "_O"
        ElseIf rdbOldCurrent.Checked Then
            VarID += "_CD"
        ElseIf rdbCurrentStanding.Checked Then
            VarID += "_CO"
        ElseIf chkWithOpening.Checked Then
            VarID += "_OO"
        ElseIf chkORD_CD.Checked Then
            VarID += "_RD"
        End If

        If rbtnActive.Checked Then
            VarID += "_A"
        ElseIf rbtnInActive.Checked Then
            VarID += "_I"
        ElseIf rbtnAll.Checked Then
            VarID += "_AL"
        End If
        Gv1.VarID = VarID
    End Sub

    Sub PrintUDP(ByVal isPrint As Boolean, Optional ByVal isPrerint As Boolean = False)
        Try
            Dim qry As String = ""
            Dim dt1 As New DataTable

            Dim whrActiveInactive As String = Nothing
            If rbtnActive.Checked Then
                whrActiveInactive = " And TSPL_VLC_MASTER_HEAD.Active=1 "
                'whrActiveInactive = " And xxx.Active=1 "
            ElseIf rbtnInActive.Checked Then
                whrActiveInactive = " And TSPL_VLC_MASTER_HEAD.Active=0 "
                'whrActiveInactive = " And xxx.Active=0 "
            Else
                whrActiveInactive = Nothing
            End If

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
                        select case when isnull(TSPL_MULTIPLE_DEDUCTION_DETAIL.Trans_Type,'Deduction')='Addition' then 'A' else 'D' end Type
                        ,TSPL_VLC_MASTER_HEAD.VLC_CODE_VLC_Uploader as VSP_Uploader_Code 
                        ,TSPL_MULTIPLE_DEDUCTION_detail.Vendor_Code,TSPL_MULTIPLE_DEDUCTION_detail.Vendor_Name
                        ,TSPL_MULTIPLE_DEDUCTION_detail.DeductionCode as Ded_Code,TSPL_MULTIPLE_DEDUCTION_detail.Deduction_Desc as Ded_Desc
                        ,isnull(TSPL_MULTIPLE_DEDUCTION_detail.amount,0) as Amount
                        from TSPL_MULTIPLE_DEDUCTION_HEAD 
                        LEFT OUTER JOIN TSPL_MULTIPLE_DEDUCTION_DETAIL ON TSPL_MULTIPLE_DEDUCTION_HEAD.Document_No =TSPL_MULTIPLE_DEDUCTION_DETAIL.Document_No
                        left outer Join (select distinct TSPL_VLC_MASTER_HEAD.VSP_Code,TSPL_VLC_MASTER_HEAD.VLC_CODE_VLC_Uploader,TSPL_VLC_MASTER_HEAD.Active from TSPL_VLC_MASTER_HEAD) as TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code = TSPL_MULTIPLE_DEDUCTION_detail.Vendor_Code
                        where TSPL_MULTIPLE_DEDUCTION_HEAD.IsPosted=1 " + whrActiveInactive + "
                        and TSPL_MULTIPLE_DEDUCTION_HEAD.IsOpening=1 and convert(date,Document_Date,103)>=convert(date,('" + fromDate.Value + "'),103) and convert(date,Document_Date,103)<=convert(date,('" + ToDate.Value + "'),103) " + subMCCQry1 + "
                        ) Final " + subQryWhere + " group by  Final.Ded_Code ,[Type]" + subQry + " order by [Type] desc"

                ElseIf chkORD_CD.Checked = True Then ''5
                    qry = "select case when isnull(Final.Type,'D')='D' then 'Deduction' when isnull(Final.Type,'')='A' then 'Addition' else '' end Type,Max(Ded_code+'-'+Ded_Desc) as DeductionName
                        " + subDCSQry + ", sum(Amount) as [Amount] from ( 
                        Select 'D' Type,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader as VSP_Uploader_Code,TSPL_PAYMENT_PROCESS_DEDUCTION.Vendor_CODE, TSPL_PAYMENT_PROCESS_DEDUCTION.Vendor_NAME, TSPL_PAYMENT_PROCESS_DEDUCTION.Ded_Code,TSPL_PAYMENT_PROCESS_DEDUCTION.Ded_Desc,TSPL_PAYMENT_PROCESS_DEDUCTION.Reduce_Deduc_Amt as Amount 
                        from TSPL_PAYMENT_PROCESS_DEDUCTION 
                        left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code =TSPL_PAYMENT_PROCESS_DEDUCTION.Vendor_CODE
                        where  TSPL_PAYMENT_PROCESS_DEDUCTION.Doc_No in ('" + strOldDocNo + "')  AND ISNULL(TSPL_PAYMENT_PROCESS_DEDUCTION.Reduce_Deduc_Amt,0)>0  " + whrActiveInactive + "
                        UNION ALL 
                        Select 'D' Type,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader as VSP_Uploader_Code,TSPL_PAYMENT_PROCESS_DEDUCTION.Vendor_CODE, TSPL_PAYMENT_PROCESS_DEDUCTION.Vendor_NAME, TSPL_PAYMENT_PROCESS_DEDUCTION.Ded_Code,TSPL_PAYMENT_PROCESS_DEDUCTION.Ded_Desc,TSPL_PAYMENT_PROCESS_DEDUCTION.Amount-TSPL_PAYMENT_PROCESS_DEDUCTION.Reduce_Deduc_Amt as Amount 
                        from TSPL_PAYMENT_PROCESS_DEDUCTION 
                        left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code =TSPL_PAYMENT_PROCESS_DEDUCTION.Vendor_CODE
                        where  TSPL_PAYMENT_PROCESS_DEDUCTION.Doc_No in (" + strDocNo + ") " + subMCCQry1 + "" + whrActiveInactive + "
                        ) Final " + subQryWhere + " group by  Final.Ded_Code ,[Type] " + subQry + " order by [Type] desc"
                End If
            End If
            If btnPrint.Text = "Print" Then
                dtPrint = clsDBFuncationality.GetDataTable(qry)
                Dim frmCRV As New frmCrystalReportViewer()
                frmCRV.funreport(MyBase.Form_ID, CrystalReportFolder.MilkProcurement, dtPrint, "rptTempPayDedctSummaryList", "TP Print")
                frmCRV = Nothing
            Else
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
                    clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                    Exit Sub
                Else
                    Gv1.DataSource = dt1
                    RadPageView1.SelectedPage = RadPageViewPage2
                    SetGridFormatUDP()
                    ReStoreGridLayout()
                End If
                EnableDisableControl(False)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Function GetBAseQeryUDP(ByVal fromDate As Date, ByVal ToDate As Date) As String
        Dim whrActiveInactive As String = Nothing
        Dim BaseQry As String = "select TSPL_MULTIPLE_DEDUCTION_HEAD.Document_No,TSPL_MULTIPLE_DEDUCTION_HEAD.Document_Date,TSPL_MULTIPLE_DEDUCTION_DETAIL.Against_Deduction_DocNo as AP_Invoice_No,
TSPL_VENDOR_INVOICE_HEAD.Posting_Date as AP_Invoice_Date,TSPL_VENDOR_INVOICE_HEAD.Document_Type,TSPL_MULTIPLE_DEDUCTION_detail.DeductionCode,
TSPL_MULTIPLE_DEDUCTION_detail.Vendor_Code,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader,TSPL_MULTIPLE_DEDUCTION_DETAIL.Amount,0 as Reduce_Deduc_Amt ,1 as RI,TSPL_VLC_MASTER_HEAD.Active
from TSPL_MULTIPLE_DEDUCTION_DETAIL
left  join TSPL_MULTIPLE_DEDUCTION_HEAD on TSPL_MULTIPLE_DEDUCTION_HEAD.Document_No=TSPL_MULTIPLE_DEDUCTION_DETAIL.Document_No 
left join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No=TSPL_MULTIPLE_DEDUCTION_DETAIL.Against_Deduction_DocNo
left  join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code=TSPL_VENDOR_INVOICE_HEAD.Vendor_Code
where 2=2 "


        BaseQry += "Union all
select TSPL_PAYMENT_PROCESS_DEDUCTION.Doc_No as Document_No, TSPL_PAYMENT_PROCESS_HEAD.To_Date as Doc_Date,TSPL_PAYMENT_PROCESS_DEDUCTION.AP_Invoice_No ,
TSPL_VENDOR_INVOICE_HEAD.Posting_Date as AP_Invoice_Date,TSPL_VENDOR_INVOICE_HEAD.Document_Type,TSPL_MULTIPLE_DEDUCTION_detail.DeductionCode,
TSPL_PAYMENT_PROCESS_DEDUCTION.Vendor_CODE,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader,TSPL_PAYMENT_PROCESS_DEDUCTION.Amount,
TSPL_PAYMENT_PROCESS_DEDUCTION.Reduce_Deduc_Amt,2 as RI,TSPL_VLC_MASTER_HEAD.Active   
from TSPL_PAYMENT_PROCESS_DEDUCTION
left join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No=TSPL_PAYMENT_PROCESS_DEDUCTION.AP_Invoice_No
left  join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_HEAD.Doc_No=TSPL_PAYMENT_PROCESS_DEDUCTION.Doc_No
left join TSPL_MULTIPLE_DEDUCTION_DETAIL on TSPL_MULTIPLE_DEDUCTION_DETAIL.Against_Deduction_DocNo=TSPL_PAYMENT_PROCESS_DEDUCTION.AP_Invoice_No 
left join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code=TSPL_VENDOR_INVOICE_HEAD.Vendor_Code
where 2=2  and TSPL_MULTIPLE_DEDUCTION_detail.DeductionCode is not null   
Union all
select TSPL_PAYMENT_PROCESS_CREDIT_NOTE.Doc_No as Document_No, TSPL_PAYMENT_PROCESS_HEAD.To_Date as Doc_Date,TSPL_PAYMENT_PROCESS_CREDIT_NOTE.AP_Invoice_No,
TSPL_VENDOR_INVOICE_HEAD.Posting_Date as AP_Invoice_Date,TSPL_VENDOR_INVOICE_HEAD.Document_Type,TSPL_MULTIPLE_DEDUCTION_detail.DeductionCode,
TSPL_PAYMENT_PROCESS_CREDIT_NOTE.Vendor_CODE,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader,TSPL_PAYMENT_PROCESS_CREDIT_NOTE.Amount,0 as Reduce_Deduc_Amt,3 as RI,TSPL_VLC_MASTER_HEAD.Active
from TSPL_PAYMENT_PROCESS_CREDIT_NOTE
left join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No=TSPL_PAYMENT_PROCESS_CREDIT_NOTE.AP_Invoice_No
left  join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_HEAD.Doc_No=TSPL_PAYMENT_PROCESS_CREDIT_NOTE.Doc_No
left join TSPL_MULTIPLE_DEDUCTION_DETAIL on TSPL_MULTIPLE_DEDUCTION_DETAIL.Against_Deduction_DocNo=TSPL_PAYMENT_PROCESS_CREDIT_NOTE.AP_Invoice_No 
left join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code=TSPL_VENDOR_INVOICE_HEAD.Vendor_Code
where 2=2 and TSPL_MULTIPLE_DEDUCTION_detail.DeductionCode is not null 
union all 
select TSPL_SD_SHIPMENT_HEAD.Document_Code as Document_No,TSPL_SD_SHIPMENT_HEAD.Document_Date,TSPL_Customer_Invoice_Head.Document_No as AP_Invoice_No ,TSPL_Customer_Invoice_Head.Posting_Date as AP_Invoice_Date,'D' as Document_Type,TSPL_SD_SHIPMENT_HEAD.Deduction as DeductionCode,TSPL_VLC_MASTER_HEAD.VSP_Code as Vendor_Code ,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader ,(TSPL_SD_SHIPMENT_HEAD.Total_Amt - isnull(TSPL_SD_SHIPMENT_HEAD.TotalSubsidyAmt,0)) as Amount,0 as Reduce_Deduc_Amt ,4 as RI,TSPL_VLC_MASTER_HEAD.Active 
from  TSPL_SD_SHIPMENT_HEAD 
left outer join TSPL_CUSTOMER_VENDOR_MAPPING on TSPL_CUSTOMER_VENDOR_MAPPING.Cust_Code=TSPL_SD_SHIPMENT_HEAD.Customer_Code
left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code = TSPL_CUSTOMER_VENDOR_MAPPING.Vendor_Code
left outer join TSPL_DEDUCTION_MASTER on TSPL_DEDUCTION_MASTER.Code = TSPL_SD_SHIPMENT_HEAD.Deduction 
left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code= TSPL_VLC_MASTER_HEAD.VSP_Code
left outer join TSPL_CUSTOMER_INVOICE_HEAD on TSPL_Customer_Invoice_Head.Against_Sale_No=TSPL_SD_SHIPMENT_HEAD.Sale_Invoice_No
where  TSPL_SD_SHIPMENT_HEAD.Trans_Type='MCC' and TSPL_SD_SHIPMENT_HEAD.is_cashsale='N'  and TSPL_SD_SHIPMENT_HEAD.Status=1
union all
select TSPL_PAYMENT_PROCESS_MCC_SALE.Doc_No as Document_No, TSPL_PAYMENT_PROCESS_HEAD.To_Date as Doc_Date,TSPL_PAYMENT_PROCESS_MCC_SALE.AR_Invoice_No as AP_Invoice_No ,TSPL_Customer_Invoice_Head.Posting_Date as AP_Invoice_Date,'D' as Document_Type,TSPL_SD_SHIPMENT_HEAD.Deduction as DeductionCode,TSPL_CUSTOMER_VENDOR_MAPPING.Vendor_Code,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader,TSPL_PAYMENT_PROCESS_MCC_SALE.Amount,TSPL_PAYMENT_PROCESS_MCC_SALE.Reduce_Deduc_Amt,5 as RI,TSPL_VLC_MASTER_HEAD.Active
from TSPL_PAYMENT_PROCESS_MCC_SALE
left outer join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_Code=TSPL_PAYMENT_PROCESS_MCC_SALE.Shipment_Doc_No
left outer join TSPL_Customer_Invoice_Head on TSPL_Customer_Invoice_Head.Document_No=TSPL_PAYMENT_PROCESS_MCC_SALE.AR_Invoice_No
left outer join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_HEAD.Doc_No=TSPL_PAYMENT_PROCESS_MCC_SALE.Doc_No
left outer join TSPL_CUSTOMER_VENDOR_MAPPING on TSPL_CUSTOMER_VENDOR_MAPPING.Cust_Code=TSPL_SD_SHIPMENT_HEAD.Customer_Code
left join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code=TSPL_CUSTOMER_VENDOR_MAPPING.Vendor_Code
where 2=2 

union all
Select '' as Document_No,TSPL_VENDOR_INVOICE_HEAD.Posting_Date as Doc_Date,TSPL_VENDOR_INVOICE_HEAD.Document_No as AP_Invoice_No,TSPL_VENDOR_INVOICE_HEAD.Posting_Date as AP_Invoice_Date,TSPL_VENDOR_INVOICE_HEAD.Document_Type,TSPL_VENDOR_INVOICE_DETAIL.DeductionCode,TSPL_VENDOR_INVOICE_HEAD.Vendor_Code,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader,TSPL_VENDOR_INVOICE_HEAD.Document_Total as Amount,0 as Reduce_Deduc_Amt,6 as RI,TSPL_VLC_MASTER_HEAD.Active from TSPL_VENDOR_INVOICE_DETAIL
LEFT OUTER JOIN TSPL_VENDOR_INVOICE_HEAD ON TSPL_VENDOR_INVOICE_HEAD.Document_No=TSPL_VENDOR_INVOICE_DETAIL.Document_No
left outer join TSPL_VLC_MASTER_HEAD ON TSPL_VLC_MASTER_HEAD.VSP_Code=TSPL_VENDOR_INVOICE_HEAD.Vendor_Code
WHERE 2=2  and  RefDocType In('CAP-MSN-CDCS','CAP-MSN','CAP-OMSN','SUS-ADJD','SUS-ADJC')

union all
Select TSPL_PAYMENT_PROCESS_CREDIT_NOTE.Doc_No as Document_No, TSPL_PAYMENT_PROCESS_HEAD.To_Date as Doc_Date,TSPL_PAYMENT_PROCESS_CREDIT_NOTE.AP_Invoice_No,
TSPL_VENDOR_INVOICE_HEAD.Posting_Date as AP_Invoice_Date,TSPL_VENDOR_INVOICE_HEAD.Document_Type,TSPL_VENDOR_INVOICE_DETAIL.DeductionCode,
TSPL_PAYMENT_PROCESS_CREDIT_NOTE.Vendor_CODE,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader,TSPL_PAYMENT_PROCESS_CREDIT_NOTE.Amount,0 as Reduce_Deduc_Amt,7 as RI,TSPL_VLC_MASTER_HEAD.Active 
from TSPL_PAYMENT_PROCESS_CREDIT_NOTE
left join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No=TSPL_PAYMENT_PROCESS_CREDIT_NOTE.AP_Invoice_No
left join TSPL_VENDOR_INVOICE_DETAIL on TSPL_VENDOR_INVOICE_DETAIL.Document_No=TSPL_VENDOR_INVOICE_HEAD.Document_No
left  join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_HEAD.Doc_No=TSPL_PAYMENT_PROCESS_CREDIT_NOTE.Doc_No
left join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code=TSPL_VENDOR_INVOICE_HEAD.Vendor_Code
where 2=2  and RefDocType In('CAP-MSN-CDCS','CAP-MSN','CAP-OMSN','SUS-ADJD','SUS-ADJC')

union all
Select TSPL_PAYMENT_PROCESS_DEDUCTION.Doc_No as Document_No, TSPL_PAYMENT_PROCESS_HEAD.To_Date as Doc_Date,TSPL_PAYMENT_PROCESS_DEDUCTION.AP_Invoice_No ,
TSPL_VENDOR_INVOICE_HEAD.Posting_Date as AP_Invoice_Date,TSPL_VENDOR_INVOICE_HEAD.Document_Type,TSPL_VENDOR_INVOICE_DETAIL.DeductionCode,
TSPL_PAYMENT_PROCESS_DEDUCTION.Vendor_CODE,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader,TSPL_PAYMENT_PROCESS_DEDUCTION.Amount,
TSPL_PAYMENT_PROCESS_DEDUCTION.Reduce_Deduc_Amt,8 as RI,TSPL_VLC_MASTER_HEAD.Active 
from TSPL_PAYMENT_PROCESS_DEDUCTION
left join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No=TSPL_PAYMENT_PROCESS_DEDUCTION.AP_Invoice_No
left join TSPL_VENDOR_INVOICE_DETAIL on TSPL_VENDOR_INVOICE_DETAIL.Document_No=TSPL_VENDOR_INVOICE_HEAD.Document_No
left  join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_HEAD.Doc_No=TSPL_PAYMENT_PROCESS_DEDUCTION.Doc_No
left join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code=TSPL_VENDOR_INVOICE_HEAD.Vendor_Code
where 2=2 and RefDocType In('CAP-MSN-CDCS','CAP-MSN','CAP-OMSN','SUS-ADJD','SUS-ADJC') 

union all

select TSPL_PAYMENT_PROCESS_MCC_SALE_RETURN.Doc_No as Document_No, TSPL_PAYMENT_PROCESS_HEAD.To_Date as Doc_Date,TSPL_PAYMENT_PROCESS_MCC_SALE_RETURN.AR_Invoice_No as AP_Invoice_No ,TSPL_Customer_Invoice_Head.Posting_Date as AP_Invoice_Date,'D' as Document_Type,TSPL_SD_SHIPMENT_HEAD.Deduction as DeductionCode,TSPL_CUSTOMER_VENDOR_MAPPING.Vendor_Code,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader,TSPL_PAYMENT_PROCESS_MCC_SALE_RETURN.Amount,TSPL_PAYMENT_PROCESS_MCC_SALE_RETURN.Reduce_Deduc_Amt,8 as RI,TSPL_VLC_MASTER_HEAD.Active
from TSPL_PAYMENT_PROCESS_MCC_SALE_RETURN
left outer join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_Code=TSPL_PAYMENT_PROCESS_MCC_SALE_RETURN.Shipment_Doc_No
left outer join TSPL_Customer_Invoice_Head on TSPL_Customer_Invoice_Head.Document_No=TSPL_PAYMENT_PROCESS_MCC_SALE_RETURN.AR_Invoice_No
left outer join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_HEAD.Doc_No=TSPL_PAYMENT_PROCESS_MCC_SALE_RETURN.Doc_No
left outer join TSPL_CUSTOMER_VENDOR_MAPPING on TSPL_CUSTOMER_VENDOR_MAPPING.Cust_Code=TSPL_SD_SHIPMENT_HEAD.Customer_Code
left join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code=TSPL_CUSTOMER_VENDOR_MAPPING.Vendor_Code
where 2=2 
"

        Dim qry As String = ""
        If chkDCSWise.Checked Then
            Dim subQry As String = Nothing
            If btnPrint.Text = "Print" Then
                subQry = "select '" + clsCommon.myCstr(objCommonVar.CurrentUser) + "' As PrintedBy,'" + clsCommon.GetPrintDate(fromDate) + "' As FromDate,'" + clsCommon.GetPrintDate(ToDate) + "' As ToDate,(Select Comp_Name From TSPL_COMPANY_MASTER Where Comp_Code1='" + clsCommon.myCstr(objCommonVar.CurrComp_Code1) + "') As Comp_Name,(Select City_Code From TSPL_COMPANY_MASTER Where Comp_Code1='" + clsCommon.myCstr(objCommonVar.CurrComp_Code1) + "') As City_Code, "
            Else
                subQry = "Select "
            End If
            qry = subQry + " case when Document_Type='D' then 'Deduction' else 'Addition'  end Type,DCSCode,[DCS Name] ,(DeductionCode+'-'+DeductionName) as DeductionName,cast((OP+Sale) as  decimal(18,2)) as [Opening+Sale],AMTDeducted as [Amt Deducted],cast((OP+Sale-AMTDeducted) as decimal(18,2)) as [Balance Amount],Active from (
select Document_Type,xx.DeductionCode,max(TSPL_DEDUCTION_MASTER.Description) as DeductionName,TSPL_VENDOR_MASTER.Vendor_Code,max(VLC_Code_VLC_Uploader) as DCSCode,max(TSPL_VENDOR_MASTER.Vendor_Name) as [DCS Name]
,sum((Amount-Reduce_Deduc_Amt) * (case when  Document_Date<'" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(fromDate), "dd/MMM/yyyy hh:mm:ss tt") + "' then 1 else 0 end) * (case when RI=1 or RI=4 or RI=6 or RI=8 then 1 else -1 end)) as OP 
,sum(Amount * (case when  Document_Date>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(fromDate), "dd/MMM/yyyy hh:mm:ss tt") + "' and Document_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(ToDate), "dd/MMM/yyyy hh:mm:ss tt") + "' then 1 else 0 end) * (case when (RI=1 or RI=4 or RI=6 or RI=8) then 1 else 0 end)) as Sale
,sum((Amount-Reduce_Deduc_Amt) * (case when Document_Date>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(fromDate), "dd/MMM/yyyy hh:mm:ss tt") + "' and Document_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(ToDate), "dd/MMM/yyyy hh:mm:ss tt") + "' then 1 else 0 end) * (case when (RI=1 or RI=4 or RI=6 or RI=8) then 0 else 1 end)) as AMTDeducted ,max(Active)Active
from (" + BaseQry + ")xx
left  join TSPL_DEDUCTION_MASTER on TSPL_DEDUCTION_MASTER.Code=xx.DeductionCode
left  join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=xx.Vendor_Code "
            If clsCommon.CompairString(objCommonVar.CurrComp_Code1, "JPR") = CompairStringResult.Equal Then
                qry += " where 2=2 and TSPL_DEDUCTION_MASTER.Code !='PDP' "
            End If
            qry += " group by Document_Type,DeductionCode,TSPL_VENDOR_MASTER.Vendor_Code
)xxx  where 2=2 "
            If clsCommon.myLen(txtMCC.Value) > 0 Then
                qry += " and TSPL_VLC_MASTER_HEAD.MCC='" + txtMCC.Value + "' "
            End If
            If clsCommon.myLen(txtDeduction.Value) > 0 Then
                qry += " and TSPL_MULTIPLE_DEDUCTION_detail.DeductionCode='" + txtDeduction.Value + "'"
            End If
            If rbtnActive.Checked Then
                'whrActiveInactive = " And TSPL_VLC_MASTER_HEAD.Active=1 "
                qry += " And xxx.Active=1 "
            ElseIf rbtnInActive.Checked Then
                'whrActiveInactive = " And TSPL_VLC_MASTER_HEAD.Active=0 "
                qry += " And xxx.Active=0 "
            Else
                qry += Nothing
            End If


            'where (OP+Sale-AMTDeducted)>0"
        Else
            qry = "select case when Document_Type='D' then 'Deduction' else 'Addition'  end Type ,(DeductionCode+'-'+DeductionName) as DeductionName,(OP+Sale) as [Opening+Sale],AMTDeducted as [Amt Deducted],(OP+Sale-AMTDeducted) as [Balance Amount],Active from (
select Document_Type,xx.DeductionCode,max(TSPL_DEDUCTION_MASTER.Description) as DeductionName
,sum((Amount-Reduce_Deduc_Amt) * (case when  Document_Date<'" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(fromDate), "dd/MMM/yyyy hh:mm:ss tt") + "' then 1 else 0 end) * (case when RI=1 or RI=4 or RI=6 or RI=8 then 1 else -1 end)) as OP 
,sum(Amount * (case when  Document_Date>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(fromDate), "dd/MMM/yyyy hh:mm:ss tt") + "' and Document_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(ToDate), "dd/MMM/yyyy hh:mm:ss tt") + "' then 1 else 0 end) * (case when RI=1 or RI=4 or RI=6 or RI=8 then 1 else 0 end)) as Sale
,sum((Amount-Reduce_Deduc_Amt) * (case when Document_Date>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(fromDate), "dd/MMM/yyyy hh:mm:ss tt") + "' and Document_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(ToDate), "dd/MMM/yyyy hh:mm:ss tt") + "' then 1 else 0 end) * (case when RI=1 or RI=4 or RI=6 or RI=8 then 0 else 1 end)) as AMTDeducted ,max(Active)Active
from (" + BaseQry + ")xx
left  join TSPL_DEDUCTION_MASTER on TSPL_DEDUCTION_MASTER.Code=xx.DeductionCode
left  join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=xx.Vendor_Code "

            If clsCommon.CompairString(objCommonVar.CurrComp_Code1, "JPR") = CompairStringResult.Equal Then
                qry += " where 2=2 and TSPL_DEDUCTION_MASTER.Code !='PDP' "
            End If

            qry += " group by Document_Type,DeductionCode
)xxx   where 2=2 "
            If clsCommon.myLen(txtMCC.Value) > 0 Then
                qry += " And TSPL_VLC_MASTER_HEAD.MCC ='" + txtMCC.Value + "' "
            End If
            If clsCommon.myLen(txtDeduction.Value) > 0 Then
                qry += " and TSPL_MULTIPLE_DEDUCTION_detail.DeductionCode='" + txtDeduction.Value + "'"
            End If
            If rbtnActive.Checked Then
                'whrActiveInactive = " And TSPL_VLC_MASTER_HEAD.Active=1 "
                qry += " And xxx.Active=1 "
            ElseIf rbtnInActive.Checked Then
                'whrActiveInactive = " And TSPL_VLC_MASTER_HEAD.Active=0 "
                qry += " And xxx.Active=0 "
            Else
                qry += Nothing
            End If

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
                        select case when isnull(TSPL_MULTIPLE_DEDUCTION_DETAIL.Trans_Type,'Deduction')='Addition' then 'A' else 'D' end Type
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
                        select case when isnull(TSPL_MULTIPLE_DEDUCTION_DETAIL.Trans_Type,'Deduction')='Addition' then 'A' else 'D' end Type
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
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub
            Else
                Gv1.DataSource = dt1
                RadPageView1.SelectedPage = RadPageViewPage2
                SetGridFormatUDP()
                ReStoreGridLayout()
            End If
            EnableDisableControl(False)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
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
select case when isnull(TSPL_MULTIPLE_DEDUCTION_DETAIL.Trans_Type,'Deduction')='Addition' then 'A' else 'D' end Type
,TSPL_VLC_MASTER_HEAD.VLC_CODE_VLC_Uploader as VSP_Uploader_Code 
,TSPL_MULTIPLE_DEDUCTION_detail.Vendor_Code,TSPL_MULTIPLE_DEDUCTION_detail.Vendor_Name
,TSPL_MULTIPLE_DEDUCTION_detail.DeductionCode as Ded_Code,TSPL_MULTIPLE_DEDUCTION_detail.Deduction_Desc as Ded_Desc
,isnull(TSPL_MULTIPLE_DEDUCTION_detail.amount,0) as Amount
,case when isnull(TSPL_MULTIPLE_DEDUCTION_DETAIL.Trans_Type,'Deduction')='Addition' then IsNull(TSPL_MULTIPLE_DEDUCTION_DETAIL.Amount,0) else 0 end   as ReDedctAmt
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
select case when isnull(TSPL_MULTIPLE_DEDUCTION_DETAIL.Trans_Type,'Deduction')='Addition' then 'A' else 'D' end Type
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
            Dim subQryWhere As String = Nothing
            Dim MCCWhere As String = Nothing
            Dim subMCCQry1 As String = Nothing
            Dim subMCCQry2 As String = Nothing
            Dim subAreaQry As String = Nothing
            If fndArea.Value.Length > 0 Then
                subAreaQry = " and tSPL_MCC_MASTER.Area_Location_Code='" + fndArea.Value + "'"
            End If
            If txtMCC.Value.Length > 0 Then
                subMCCQry1 = " and TSPL_VLC_MASTER_HEAD.MCC='" + txtMCC.Value + "'"
                'subMCCQry2 = " and TSPL_MULTIPLE_DEDUCTION_HEAD.MCC_Code='" + txtMCC.Value + "'"

                If txtDeduction.Value.Length > 0 Then
                    subQryWhere = "where Final.Ded_Code='" + txtDeduction.Value + "' AND Final.Amount <> 0"
                Else
                    MCCWhere = " where Final.Amount <> 0 "
                End If
            End If

            'Dim subQryWhere As String = Nothing
            If txtDeduction.Value.Length > 0 AndAlso txtMCC.Value.Length <= 0 Then
                subQryWhere = "where Final.Ded_Code='" + txtDeduction.Value + "' AND Final.Amount <> 0"
            End If

            Dim subQry As String = Nothing
            Dim subDCSQry As String = Nothing
            Dim subQrderBy As String = Nothing
            If chkDCSWise.Checked = True Then
                subDCSQry = ",Max(VSP_Uploader_Code) as DCSCode,Max(Vendor_NAME) As 'DCS Name'"
                subQry = ",Final.Vendor_CODE,Final.VSP_Uploader_Code"
                subQrderBy = "Convert(int,VSP_Uploader_Code),"
            End If

            Dim whrActiveInactive As String = Nothing
            If rbtnActive.Checked Then
                whrActiveInactive = " And TSPL_VLC_MASTER_HEAD.Active=1 "
            ElseIf rbtnInActive.Checked Then
                whrActiveInactive = " And TSPL_VLC_MASTER_HEAD.Active=0 "
            Else
                whrActiveInactive = Nothing
            End If

            If rdbOldOutstanding.Checked Then ''1
                qry = "select case when isnull(Final.Type,'D')='D' then 'Deduction' when isnull(Final.Type,'')='A' then 'Addition' else '' end Type,Max(Ded_code+'-'+Ded_Desc) as DeductionName
                        " + subDCSQry + ", sum(Amount) as [Amount] from ( 
                        Select 'D' Type,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader as VSP_Uploader_Code,TSPL_PAYMENT_PROCESS_DEDUCTION.Vendor_CODE, TSPL_PAYMENT_PROCESS_DEDUCTION.Vendor_NAME, TSPL_PAYMENT_PROCESS_DEDUCTION.Ded_Code,TSPL_PAYMENT_PROCESS_DEDUCTION.Ded_Desc 
                        ,isnull(TSPL_PAYMENT_PROCESS_DEDUCTION.Reduce_Deduc_Amt,0) as Amount
                        from TSPL_PAYMENT_PROCESS_DEDUCTION 
                        left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code =TSPL_PAYMENT_PROCESS_DEDUCTION.Vendor_CODE"
                If AreaWiseBilling Then
                    qry += " left outer join tSPL_MCC_MASTER on tSPL_MCC_MASTER.MCC_Code=TSPL_VLC_MASTER_HEAD.MCC
						left outer join tspl_location_master on tspl_location_master.Location_Code=tSPL_MCC_MASTER.Area_Location_Code"
                End If
                qry += " where  TSPL_PAYMENT_PROCESS_DEDUCTION.Doc_No in ('" + strOldDocNo + "') " + whrActiveInactive + ""
                If AreaWiseBilling = True Then
                    qry += subAreaQry
                Else
                    qry += subMCCQry1
                End If

                qry += "              union all
                        select case when isnull(TSPL_MULTIPLE_DEDUCTION_DETAIL.Trans_Type,'Deduction')='Addition' then 'A' else 'D' end Type
                        ,TSPL_VLC_MASTER_HEAD.VLC_CODE_VLC_Uploader as VSP_Uploader_Code 
                        ,TSPL_MULTIPLE_DEDUCTION_detail.Vendor_Code,TSPL_MULTIPLE_DEDUCTION_detail.Vendor_Name
                        ,TSPL_MULTIPLE_DEDUCTION_detail.DeductionCode as Ded_Code,TSPL_MULTIPLE_DEDUCTION_detail.Deduction_Desc as Ded_Desc
                        ,isnull(TSPL_MULTIPLE_DEDUCTION_detail.amount,0) as Amount
                        from TSPL_MULTIPLE_DEDUCTION_HEAD 
                        LEFT OUTER JOIN TSPL_MULTIPLE_DEDUCTION_DETAIL ON TSPL_MULTIPLE_DEDUCTION_HEAD.Document_No =TSPL_MULTIPLE_DEDUCTION_DETAIL.Document_No
                        left outer Join (select distinct TSPL_VLC_MASTER_HEAD.VSP_Code,TSPL_VLC_MASTER_HEAD.VLC_CODE_VLC_Uploader,TSPL_VLC_MASTER_HEAD.MCC,TSPL_VLC_MASTER_HEAD.Active from TSPL_VLC_MASTER_HEAD) as TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code = TSPL_MULTIPLE_DEDUCTION_detail.Vendor_Code"
                If AreaWiseBilling Then
                    qry += " left outer join tSPL_MCC_MASTER on tSPL_MCC_MASTER.MCC_Code=TSPL_VLC_MASTER_HEAD.MCC
						left outer join tspl_location_master on tspl_location_master.Location_Code=tSPL_MCC_MASTER.Area_Location_Code"
                End If
                qry += "  where TSPL_MULTIPLE_DEDUCTION_HEAD.IsPosted=1 and TSPL_MULTIPLE_DEDUCTION_HEAD.IsOpening=1 " + whrActiveInactive + "
                        and TSPL_MULTIPLE_DEDUCTION_HEAD.IsOpening=1 and convert(date,Document_Date,103)<convert(date,('" + fromDate.Value + "'),103)"
                If AreaWiseBilling = True Then
                    qry += subAreaQry
                Else
                    qry += subMCCQry1
                End If
                qry += " union all select 'D'Type ,Customer_Code as VSP_Uploader_Code,TSPL_VLC_MASTER_HEAD.VSP_Code as Vendor_CODE,TSPL_VLC_MASTER_HEAD.VLC_Name as Vendor_NAME,TSPL_DEDUCTION_MASTER.Code as Ded_Code,TSPL_DEDUCTION_MASTER.Description as Ded_Desc,TSPL_SD_SHIPMENT_DETAIL.Item_Net_Amt as Amount
                    from TSPL_SD_SHIPMENT_DETAIL
                    left outer join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_Code = TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE
                    left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code = TSPL_SD_SHIPMENT_HEAD.Customer_Code
                    left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_SD_SHIPMENT_DETAIL.Item_Code
                    left outer join TSPL_DEDUCTION_MASTER on TSPL_DEDUCTION_MASTER.Code = TSPL_ITEM_MASTER.Deduction 
                    left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code= TSPL_VLC_MASTER_HEAD.VSP_Code"
                If AreaWiseBilling Then
                    qry += " left outer join tSPL_MCC_MASTER on tSPL_MCC_MASTER.MCC_Code=TSPL_VLC_MASTER_HEAD.MCC
						left outer join tspl_location_master on tspl_location_master.Location_Code=tSPL_MCC_MASTER.Area_Location_Code"
                End If
                qry += " where TSPL_SD_SHIPMENT_HEAD.Is_CashSale='N' and convert(date,TSPL_SD_SHIPMENT_HEAD.Document_Date,103)>=convert(date,('" + fromDate.Value + "'),103) and
                convert(date,TSPL_SD_SHIPMENT_HEAD.Document_Date,103) <= convert(date,('" + ToDate.Value + "'),103) and TSPL_VENDOR_MASTER.Vendor_Group_Code='DCS' "
                If AreaWiseBilling = True Then
                    qry += subAreaQry
                Else
                    qry += subMCCQry1
                End If

                qry += "  ) Final " + subQryWhere + "group by  Final.Ded_Code ,[Type] " + subQry + " having sum(Amount)>0 order by [Type] desc"

            ElseIf rdbOldCurrent.Checked Then ''2
                Dim subNewQry As String = Nothing
                If btnPrint.Text = "Print" Then
                    subNewQry = "select '" + clsCommon.myCstr(objCommonVar.CurrentUser) + "' As PrintedBy,'" + clsCommon.GetPrintDate(fromDate.Value) + "' As FromDate,'" + clsCommon.GetPrintDate(ToDate.Value) + "' As ToDate,(Select Comp_Name From TSPL_COMPANY_MASTER Where Comp_Code1='" + clsCommon.myCstr(objCommonVar.CurrComp_Code1) + "') As Comp_Name,(Select City_Code From TSPL_COMPANY_MASTER Where Comp_Code1='" + clsCommon.myCstr(objCommonVar.CurrComp_Code1) + "') As City_Code, "
                Else
                    subNewQry = "Select "
                End If
                qry = subNewQry + " case when isnull(Final.Type,'D')='D' then 'Deduction' when isnull(Final.Type,'')='A' then 'Addition' else '' end Type
                       ,Max(Ded_code+'-'+Ded_Desc) as DeductionName " + subDCSQry + ", sum(Amount) as 'Opening+Sale',sum(Amount-ReDedctAmt) As 'Amt Deducted',Sum(ReDedctAmt) As 'Balance Amount',max(zone_code)zone_code from ( 
                        select case when isnull(TSPL_MULTIPLE_DEDUCTION_DETAIL.Trans_Type,'Deduction')='Addition' then 'A' else 'D' end Type
                        ,TSPL_VLC_MASTER_HEAD.VLC_CODE_VLC_Uploader as VSP_Uploader_Code 
                        ,TSPL_MULTIPLE_DEDUCTION_detail.Vendor_Code,TSPL_MULTIPLE_DEDUCTION_detail.Vendor_Name
                        ,TSPL_MULTIPLE_DEDUCTION_detail.DeductionCode as Ded_Code,TSPL_MULTIPLE_DEDUCTION_detail.Deduction_Desc as Ded_Desc
                        ,isnull(TSPL_MULTIPLE_DEDUCTION_detail.amount,0) as Amount,0 As ReDedctAmt,TSPL_VENDOR_MASTER.Zone_Code as Zone_code
                        from TSPL_MULTIPLE_DEDUCTION_HEAD 
                        LEFT OUTER JOIN TSPL_MULTIPLE_DEDUCTION_DETAIL ON TSPL_MULTIPLE_DEDUCTION_HEAD.Document_No =TSPL_MULTIPLE_DEDUCTION_DETAIL.Document_No
                        left outer Join (select distinct TSPL_VLC_MASTER_HEAD.VSP_Code,TSPL_VLC_MASTER_HEAD.VLC_CODE_VLC_Uploader,TSPL_VLC_MASTER_HEAD.MCC,TSPL_VLC_MASTER_HEAD.Active from TSPL_VLC_MASTER_HEAD) as TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code = TSPL_MULTIPLE_DEDUCTION_detail.Vendor_Code
						 left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code= TSPL_VLC_MASTER_HEAD.VSP_Code"
                If AreaWiseBilling Then
                    qry += " left outer join tSPL_MCC_MASTER on tSPL_MCC_MASTER.MCC_Code=TSPL_VLC_MASTER_HEAD.MCC
						     left outer join tspl_location_master on tspl_location_master.Location_Code=tSPL_MCC_MASTER.Area_Location_Code"
                End If
                qry += "  where TSPL_MULTIPLE_DEDUCTION_HEAD.IsPosted = 1 " + whrActiveInactive + ""
                If AreaWiseBilling = True Then
                    qry += subAreaQry
                Else
                    qry += subMCCQry1
                End If
                qry += " And TSPL_MULTIPLE_DEDUCTION_HEAD.IsOpening=1 And convert(Date, Document_Date, 103)<=convert(date,('" + ToDate.Value + "'),103) 
                        union all  
                        Select 'D' Type,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader as VSP_Uploader_Code,TSPL_PAYMENT_PROCESS_DEDUCTION.Vendor_CODE, TSPL_PAYMENT_PROCESS_DEDUCTION.Vendor_NAME, TSPL_PAYMENT_PROCESS_DEDUCTION.Ded_Code,TSPL_PAYMENT_PROCESS_DEDUCTION.Ded_Desc
                        ,isnull(TSPL_PAYMENT_PROCESS_DEDUCTION.Amount,0) as Amount,IsNull(TSPL_PAYMENT_PROCESS_DEDUCTION.Reduce_Deduc_Amt,0) As ReDedctAmt,TSPL_VENDOR_MASTER.Zone_Code as Zone_code from TSPL_PAYMENT_PROCESS_DEDUCTION 
                        Left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code =TSPL_PAYMENT_PROCESS_DEDUCTION.Vendor_CODE
                        inner Join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No=TSPL_PAYMENT_PROCESS_DEDUCTION.AP_Invoice_No
								 left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code= TSPL_VENDOR_INVOICE_HEAD.Vendor_Code 
                        
inner Join TSPL_MULTIPLE_DEDUCTION_DETAIL on TSPL_MULTIPLE_DEDUCTION_DETAIL.Against_Deduction_DocNo=TSPL_VENDOR_INVOICE_HEAD.Document_No
                        inner Join TSPL_MULTIPLE_DEDUCTION_HEAD on TSPL_MULTIPLE_DEDUCTION_HEAD.Document_No=TSPL_MULTIPLE_DEDUCTION_DETAIL.Document_No"
                If AreaWiseBilling Then
                    qry += " left outer join tSPL_MCC_MASTER on tSPL_MCC_MASTER.MCC_Code=TSPL_VLC_MASTER_HEAD.MCC
						     Left outer join tspl_location_master on tspl_location_master.Location_Code=tSPL_MCC_MASTER.Area_Location_Code"
                End If
                qry += "  where  TSPL_PAYMENT_PROCESS_DEDUCTION.Doc_No in (" + strDocNo + ") And TSPL_MULTIPLE_DEDUCTION_HEAD.IsOpening=0 " + whrActiveInactive + ""
                If AreaWiseBilling = True Then
                    qry += subAreaQry
                Else
                    qry += subMCCQry1
                End If
                qry += " union all
                        Select 'A' Type,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader as VSP_Uploader_Code,TSPL_PAYMENT_PROCESS_CREDIT_NOTE.Vendor_CODE,TSPL_PAYMENT_PROCESS_CREDIT_NOTE.Vendor_NAME, TSPL_MULTIPLE_DEDUCTION_DETAIL.DeductionCode as Ded_Code,TSPL_MULTIPLE_DEDUCTION_DETAIL.Deduction_Desc as Ded_Desc,isnull(TSPL_PAYMENT_PROCESS_CREDIT_NOTE.Amount,0) as Amount,0 As ReDedctAmt,TSPL_VENDOR_MASTER.Zone_Code as Zone_code 
                        From TSPL_PAYMENT_PROCESS_CREDIT_NOTE
                        Left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code =TSPL_PAYMENT_PROCESS_CREDIT_NOTE.Vendor_CODE
                        inner Join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No=TSPL_PAYMENT_PROCESS_CREDIT_NOTE.AP_Invoice_No
                        inner Join TSPL_MULTIPLE_DEDUCTION_DETAIL on TSPL_MULTIPLE_DEDUCTION_DETAIL.Against_Deduction_DocNo=TSPL_VENDOR_INVOICE_HEAD.Document_No
						   left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code= TSPL_VENDOR_INVOICE_HEAD.Vendor_Code  

                        inner Join TSPL_MULTIPLE_DEDUCTION_HEAD on TSPL_MULTIPLE_DEDUCTION_HEAD.Document_No=TSPL_MULTIPLE_DEDUCTION_DETAIL.Document_No"
                If AreaWiseBilling Then
                    qry += " left outer join tSPL_MCC_MASTER on tSPL_MCC_MASTER.MCC_Code=TSPL_VLC_MASTER_HEAD.MCC
						     Left outer join tspl_location_master on tspl_location_master.Location_Code=tSPL_MCC_MASTER.Area_Location_Code"
                End If

                qry += "  where  TSPL_PAYMENT_PROCESS_CREDIT_NOTE.Doc_No in (" + strDocNo + ") And TSPL_MULTIPLE_DEDUCTION_HEAD.IsOpening=0 " + whrActiveInactive + ""
                If AreaWiseBilling = True Then
                    qry += subAreaQry
                Else
                    qry += subMCCQry1
                End If
                qry += " union all select 'D' Type ,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader as VSP_Uploader_Code,TSPL_VLC_MASTER_HEAD.VSP_Code as Vendor_CODE,TSPL_VLC_MASTER_HEAD.VLC_Name as Vendor_NAME,TSPL_DEDUCTION_MASTER.Code as Ded_Code,TSPL_DEDUCTION_MASTER.Description as Ded_Desc,TSPL_PAYMENT_PROCESS_MCC_SALE.AMOUNT as Amount,TSPL_PAYMENT_PROCESS_MCC_SALE.Reduce_Deduc_Amt As ReDedctAmt,TSPL_VENDOR_MASTER.Zone_Code
                        from TSPL_PAYMENT_PROCESS_MCC_SALE 
                        left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code =TSPL_PAYMENT_PROCESS_MCC_SALE.Customer_CODE
                        left  join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.document_code=TSPL_PAYMENT_PROCESS_MCC_SALE.shipment_doc_no
                        left join TSPL_SD_SHIPMENT_detail on TSPL_SD_SHIPMENT_detail.document_code=TSPL_PAYMENT_PROCESS_MCC_SALE.shipment_doc_no and TSPL_SD_SHIPMENT_detail.Line_No=1
                        left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_SD_SHIPMENT_detail.Item_Code
                        left outer join TSPL_DEDUCTION_MASTER  on TSPL_DEDUCTION_MASTER.Code=TSPL_SD_SHIPMENT_HEAD.Deduction
                        left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code= TSPL_VLC_MASTER_HEAD.VSP_Code "
                If AreaWiseBilling Then
                    qry += " left outer join tSPL_MCC_MASTER on tSPL_MCC_MASTER.MCC_Code=TSPL_VLC_MASTER_HEAD.MCC
						     Left outer join tspl_location_master on tspl_location_master.Location_Code=tSPL_MCC_MASTER.Area_Location_Code"
                End If
                qry += " where TSPL_PAYMENT_PROCESS_MCC_SALE.Doc_No in (" + strDocNo + ")      and TSPL_SD_SHIPMENT_HEAD.Status=1 and TSPL_VENDOR_MASTER.Vendor_Group_Code='DCS' " + whrActiveInactive + ""
                'convert(date,TSPL_SD_SHIPMENT_HEAD.Document_Date,103)>=convert(date,('" + fromDate.Value + "'),103) and
                'convert(date,TSPL_SD_SHIPMENT_HEAD.Document_Date,103) <= convert(date,('" + ToDate.Value + "'),103)  


                If AreaWiseBilling = True Then
                    qry += subAreaQry
                Else
                    qry += subMCCQry1
                End If

                If clsCommon.CompairString(objCommonVar.CurrComp_Code1, "CHU") = CompairStringResult.Equal Then
                    qry += " ) Final " + subQryWhere + " group by  Final.Ded_Code ,[Type] " + subQry + " having  sum(Amount)>0 order by Final.Ded_Code, " + subQrderBy + " [Type] desc "
                Else
                    qry += " ) Final " + subQryWhere + " group by  Final.Ded_Code ,[Type] " + subQry + " having  sum(Amount)>0 order by " + subQrderBy + " [Type] desc "
                End If

            ElseIf rdbCurrentStanding.Checked = True Then  ''3
                Dim subNewQry As String = Nothing
                If btnPrint.Text = "Print" Then
                    subNewQry = "select '" + clsCommon.myCstr(objCommonVar.CurrentUser) + "' As PrintedBy,'" + clsCommon.GetPrintDate(fromDate.Value) + "' As FromDate,'" + clsCommon.GetPrintDate(ToDate.Value) + "' As ToDate,(Select Comp_Name From TSPL_COMPANY_MASTER Where Comp_Code1='" + clsCommon.myCstr(objCommonVar.CurrComp_Code1) + "') As Comp_Name,(Select City_Code From TSPL_COMPANY_MASTER Where Comp_Code1='" + clsCommon.myCstr(objCommonVar.CurrComp_Code1) + "') As City_Code,Document_Date, "
                Else
                    subNewQry = "Select "
                End If
                qry = subNewQry + " case when isnull(Final.Type,'D')='D' then 'Deduction' when isnull(Final.Type,'')='A' then 'Addition' else '' end Type
                       ,Max(Ded_code+'-'+Ded_Desc) as DeductionName " + subDCSQry + ", sum(Amount * RI) as [Amount] from (
                        select '' AS Document_Date,case when isnull(TSPL_MULTIPLE_DEDUCTION_DETAIL.Trans_Type,'Deduction')='Addition' then 'A' else 'D' end Type
                        ,TSPL_VLC_MASTER_HEAD.VLC_CODE_VLC_Uploader as VSP_Uploader_Code 
                        ,TSPL_MULTIPLE_DEDUCTION_detail.Vendor_Code,TSPL_MULTIPLE_DEDUCTION_detail.Vendor_Name
                        ,TSPL_MULTIPLE_DEDUCTION_detail.DeductionCode as Ded_Code,TSPL_MULTIPLE_DEDUCTION_detail.Deduction_Desc as Ded_Desc
                        ,isnull(TSPL_MULTIPLE_DEDUCTION_detail.amount,0) as Amount,1 as RI
                        from TSPL_MULTIPLE_DEDUCTION_HEAD 
                        LEFT OUTER JOIN TSPL_MULTIPLE_DEDUCTION_DETAIL ON TSPL_MULTIPLE_DEDUCTION_HEAD.Document_No =TSPL_MULTIPLE_DEDUCTION_DETAIL.Document_No
                        left outer Join (select distinct TSPL_VLC_MASTER_HEAD.VSP_Code,TSPL_VLC_MASTER_HEAD.VLC_CODE_VLC_Uploader,TSPL_VLC_MASTER_HEAD.MCC,TSPL_VLC_MASTER_HEAD.Active from TSPL_VLC_MASTER_HEAD) as TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code = TSPL_MULTIPLE_DEDUCTION_detail.Vendor_Code"
                If AreaWiseBilling Then
                    qry += " left outer join tSPL_MCC_MASTER on tSPL_MCC_MASTER.MCC_Code=TSPL_VLC_MASTER_HEAD.MCC
						     Left outer join tspl_location_master on tspl_location_master.Location_Code=tSPL_MCC_MASTER.Area_Location_Code"
                End If
                qry += " where TSPL_MULTIPLE_DEDUCTION_HEAD.IsPosted=1 and TSPL_MULTIPLE_DEDUCTION_HEAD.IsOpening=1  "
                If AreaWiseBilling = True Then
                    qry += subAreaQry
                Else
                    qry += subMCCQry1
                End If

                qry += " " + whrActiveInactive + " and TSPL_MULTIPLE_DEDUCTION_HEAD.IsOpening=1 and convert(date,Document_Date,103)<convert(date,('" + fromDate.Value + "'),103)
                        union all
                        Select '' AS Document_Date,'D' Type,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader as VSP_Uploader_Code,TSPL_PAYMENT_PROCESS_DEDUCTION.Vendor_CODE, TSPL_PAYMENT_PROCESS_DEDUCTION.Vendor_NAME, TSPL_PAYMENT_PROCESS_DEDUCTION.Ded_Code,TSPL_PAYMENT_PROCESS_DEDUCTION.Ded_Desc 
                        ,isnull(TSPL_PAYMENT_PROCESS_DEDUCTION.Reduce_Deduc_Amt,0) as Amount,1 as RI
                        from TSPL_PAYMENT_PROCESS_DEDUCTION 
                        left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code =TSPL_PAYMENT_PROCESS_DEDUCTION.Vendor_CODE"
                If AreaWiseBilling Then
                    qry += " left outer join tSPL_MCC_MASTER on tSPL_MCC_MASTER.MCC_Code=TSPL_VLC_MASTER_HEAD.MCC
						     Left outer join tspl_location_master on tspl_location_master.Location_Code=tSPL_MCC_MASTER.Area_Location_Code"
                End If
                qry += " where  TSPL_PAYMENT_PROCESS_DEDUCTION.Doc_No in ('" + strOldDocNo + "')  "
                If AreaWiseBilling = True Then
                    qry += subAreaQry
                Else
                    qry += subMCCQry1
                End If
                qry += " " + whrActiveInactive + "
union all
                        Select  convert(DATE, TSPL_MULTIPLE_DEDUCTION_HEAD.Document_Date, 103) ,'D' Type,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader as VSP_Uploader_Code,TSPL_PAYMENT_PROCESS_DEDUCTION.Vendor_CODE, TSPL_PAYMENT_PROCESS_DEDUCTION.Vendor_NAME, TSPL_PAYMENT_PROCESS_DEDUCTION.Ded_Code,TSPL_PAYMENT_PROCESS_DEDUCTION.Ded_Desc 
                        ,isnull(TSPL_PAYMENT_PROCESS_DEDUCTION.Amount,0) as Amount,-1 as RI
                        from TSPL_PAYMENT_PROCESS_DEDUCTION 
                        left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code =TSPL_PAYMENT_PROCESS_DEDUCTION.Vendor_CODE
                        inner join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No=TSPL_PAYMENT_PROCESS_DEDUCTION.AP_Invoice_No
                        inner join TSPL_MULTIPLE_DEDUCTION_DETAIL on TSPL_MULTIPLE_DEDUCTION_DETAIL.Against_Deduction_DocNo=TSPL_VENDOR_INVOICE_HEAD.Document_No
                        inner join TSPL_MULTIPLE_DEDUCTION_HEAD on TSPL_MULTIPLE_DEDUCTION_HEAD.Document_No=TSPL_MULTIPLE_DEDUCTION_DETAIL.Document_No"
                If AreaWiseBilling Then
                    qry += " left outer join tSPL_MCC_MASTER on tSPL_MCC_MASTER.MCC_Code=TSPL_VLC_MASTER_HEAD.MCC
						     Left outer join tspl_location_master on tspl_location_master.Location_Code=tSPL_MCC_MASTER.Area_Location_Code"
                End If

                qry += "  where  TSPL_PAYMENT_PROCESS_DEDUCTION.Doc_No in (" + strDocNo + ") and TSPL_MULTIPLE_DEDUCTION_HEAD.IsOpening=0 "
                If AreaWiseBilling = True Then
                    qry += subAreaQry
                Else
                    qry += subMCCQry1
                End If

                qry += " " + whrActiveInactive + " 
union all
                        select  '' AS Document_Date, 'A' Type,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader as VSP_Uploader_Code,TSPL_PAYMENT_PROCESS_CREDIT_NOTE.Vendor_CODE,TSPL_PAYMENT_PROCESS_CREDIT_NOTE.Vendor_NAME, TSPL_MULTIPLE_DEDUCTION_DETAIL.DeductionCode as Ded_Code,TSPL_MULTIPLE_DEDUCTION_DETAIL.Deduction_Desc as Ded_Desc,isnull(TSPL_PAYMENT_PROCESS_CREDIT_NOTE.Amount,0) as Amount,-1 as RI 
                        from TSPL_PAYMENT_PROCESS_CREDIT_NOTE
                        left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code =TSPL_PAYMENT_PROCESS_CREDIT_NOTE.Vendor_CODE
                        inner join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No=TSPL_PAYMENT_PROCESS_CREDIT_NOTE.AP_Invoice_No
                        inner join TSPL_MULTIPLE_DEDUCTION_DETAIL on TSPL_MULTIPLE_DEDUCTION_DETAIL.Against_Deduction_DocNo=TSPL_VENDOR_INVOICE_HEAD.Document_No
                        inner join TSPL_MULTIPLE_DEDUCTION_HEAD on TSPL_MULTIPLE_DEDUCTION_HEAD.Document_No=TSPL_MULTIPLE_DEDUCTION_DETAIL.Document_No"
                If AreaWiseBilling Then
                    qry += " left outer join tSPL_MCC_MASTER on tSPL_MCC_MASTER.MCC_Code=TSPL_VLC_MASTER_HEAD.MCC
						     Left outer join tspl_location_master on tspl_location_master.Location_Code=tSPL_MCC_MASTER.Area_Location_Code"
                End If
                qry += "  where  TSPL_PAYMENT_PROCESS_CREDIT_NOTE.Doc_No in (" + strDocNo + ") and TSPL_MULTIPLE_DEDUCTION_HEAD.IsOpening=0  "
                If AreaWiseBilling = True Then
                    qry += subAreaQry
                Else
                    qry += subMCCQry1
                End If
                qry += " " + whrActiveInactive + " "

                qry += " union all
                select  '' AS Document_Date, 'D' Type,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader as VSP_Uploader_Code,TSPL_VLC_MASTER_HEAD.VSP_Code as Vendor_CODE,
                TSPL_VLC_MASTER_HEAD.VLC_Name as Vendor_NAME, TSPL_DEDUCTION_MASTER.Code as Ded_Code,TSPL_DEDUCTION_MASTER.Description as Ded_Desc,
                isnull(TSPL_PAYMENT_PROCESS_MCC_SALE.Amount,0) as Amount,-1 as RI 
                from TSPL_PAYMENT_PROCESS_MCC_SALE
                left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code =TSPL_PAYMENT_PROCESS_MCC_SALE.Customer_CODE
                left  join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.document_code=TSPL_PAYMENT_PROCESS_MCC_SALE.shipment_doc_no
                left join TSPL_SD_SHIPMENT_detail on TSPL_SD_SHIPMENT_detail.document_code=TSPL_PAYMENT_PROCESS_MCC_SALE.shipment_doc_no and TSPL_SD_SHIPMENT_detail.Line_No=1
                left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_SD_SHIPMENT_detail.Item_Code
                left outer join TSPL_DEDUCTION_MASTER  on TSPL_DEDUCTION_MASTER.Code=TSPL_SD_SHIPMENT_HEAD.Deduction
                left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code= TSPL_VLC_MASTER_HEAD.VSP_Code
                where TSPL_PAYMENT_PROCESS_MCC_SALE.Doc_No in(" + strDocNo + ")  and TSPL_SD_SHIPMENT_HEAD.Status=1 and TSPL_VENDOR_MASTER.Vendor_Group_Code='DCS' "

                If AreaWiseBilling = True Then
                    qry += subAreaQry
                Else
                    qry += subMCCQry1
                End If
                qry += " " + whrActiveInactive + " "

                qry += " ) Final " + subQryWhere + " " + MCCWhere + "  group by  Final.Ded_Code  " + subQry + "  ,[Type], Document_Date  order by [Type] desc"

            ElseIf chkWithOpening.Checked = True Then ''4
                qry = "select case when isnull(Final.Type,'D')='D' then 'Deduction' when isnull(Final.Type,'')='A' then 'Addition' else '' end Type
                       ,Max(Ded_code+'-'+Ded_Desc) as DeductionName " + subDCSQry + ", sum(Amount) as [Amount] from ( 
                        select case when isnull(TSPL_MULTIPLE_DEDUCTION_DETAIL.Trans_Type,'Deduction')='Addition' then 'A' else 'D' end Type
                        ,TSPL_VLC_MASTER_HEAD.VLC_CODE_VLC_Uploader as VSP_Uploader_Code 
                        ,TSPL_MULTIPLE_DEDUCTION_detail.Vendor_Code,TSPL_MULTIPLE_DEDUCTION_detail.Vendor_Name
                        ,TSPL_MULTIPLE_DEDUCTION_detail.DeductionCode as Ded_Code,TSPL_MULTIPLE_DEDUCTION_detail.Deduction_Desc as Ded_Desc
                        ,isnull(TSPL_MULTIPLE_DEDUCTION_detail.amount,0) as Amount
                        from TSPL_MULTIPLE_DEDUCTION_HEAD 
                        LEFT OUTER JOIN TSPL_MULTIPLE_DEDUCTION_DETAIL ON TSPL_MULTIPLE_DEDUCTION_HEAD.Document_No =TSPL_MULTIPLE_DEDUCTION_DETAIL.Document_No

                        left outer Join (select distinct TSPL_VLC_MASTER_HEAD.VSP_Code,TSPL_VLC_MASTER_HEAD.VLC_CODE_VLC_Uploader,TSPL_VLC_MASTER_HEAD.Active,TSPL_VLC_MASTER_HEAD.mcc from TSPL_VLC_MASTER_HEAD) as TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code = TSPL_MULTIPLE_DEDUCTION_detail.Vendor_Code"
                If AreaWiseBilling Then
                    qry += " left outer join tSPL_MCC_MASTER on tSPL_MCC_MASTER.MCC_Code=TSPL_VLC_MASTER_HEAD.MCC
						     Left outer join tspl_location_master on tspl_location_master.Location_Code=tSPL_MCC_MASTER.Area_Location_Code"
                End If
                qry += " where TSPL_MULTIPLE_DEDUCTION_HEAD.IsPosted=1
                        and TSPL_MULTIPLE_DEDUCTION_HEAD.IsOpening=1 and convert(date,Document_Date,103)>=convert(date,('" + fromDate.Value + "'),103) and convert(date,Document_Date,103)<=convert(date,('" + ToDate.Value + "'),103) " + whrActiveInactive + " "
                If AreaWiseBilling = True Then
                    qry += subAreaQry
                Else
                    qry += subMCCQry1
                End If


                qry += "  ) Final " + subQryWhere + " group by  Final.Ded_Code ,[Type]" + subQry + " order by [Type] desc"

            ElseIf chkORD_CD.Checked = True Then ''5
                qry = "select case when isnull(Final.Type,'D')='D' then 'Deduction' when isnull(Final.Type,'')='A' then 'Addition' else '' end Type,Max(Ded_code+'-'+Ded_Desc) as DeductionName
                        " + subDCSQry + ", sum(Amount) as [Amount] from ( 
                        Select 'D' Type,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader as VSP_Uploader_Code,TSPL_PAYMENT_PROCESS_DEDUCTION.Vendor_CODE, TSPL_PAYMENT_PROCESS_DEDUCTION.Vendor_NAME, TSPL_PAYMENT_PROCESS_DEDUCTION.Ded_Code,TSPL_PAYMENT_PROCESS_DEDUCTION.Ded_Desc,TSPL_PAYMENT_PROCESS_DEDUCTION.Reduce_Deduc_Amt as Amount 
                        from TSPL_PAYMENT_PROCESS_DEDUCTION 
                        left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code =TSPL_PAYMENT_PROCESS_DEDUCTION.Vendor_CODE
                        where  TSPL_PAYMENT_PROCESS_DEDUCTION.Doc_No in ('" + strOldDocNo + "')  AND ISNULL(TSPL_PAYMENT_PROCESS_DEDUCTION.Reduce_Deduc_Amt,0)>0  " + whrActiveInactive + "
                        UNION ALL 
                        Select 'D' Type,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader as VSP_Uploader_Code,TSPL_PAYMENT_PROCESS_DEDUCTION.Vendor_CODE, TSPL_PAYMENT_PROCESS_DEDUCTION.Vendor_NAME, TSPL_PAYMENT_PROCESS_DEDUCTION.Ded_Code,TSPL_PAYMENT_PROCESS_DEDUCTION.Ded_Desc,TSPL_PAYMENT_PROCESS_DEDUCTION.Amount-TSPL_PAYMENT_PROCESS_DEDUCTION.Reduce_Deduc_Amt as Amount 
                        from TSPL_PAYMENT_PROCESS_DEDUCTION 
                        left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code =TSPL_PAYMENT_PROCESS_DEDUCTION.Vendor_CODE"
                If AreaWiseBilling Then
                    qry += " left outer join tSPL_MCC_MASTER on tSPL_MCC_MASTER.MCC_Code=TSPL_VLC_MASTER_HEAD.MCC
						     Left outer join tspl_location_master on tspl_location_master.Location_Code=tSPL_MCC_MASTER.Area_Location_Code"
                End If
                qry += "  where  TSPL_PAYMENT_PROCESS_DEDUCTION.Doc_No in (" + strDocNo + ") " + whrActiveInactive + " "
                If AreaWiseBilling = True Then
                    qry += subAreaQry
                Else
                    qry += subMCCQry1
                End If

                qry += "  ) Final " + subQryWhere + " group by  Final.Ded_Code ,[Type] " + subQry + " order by [Type] desc"
            End If

            If btnPrint.Text = "Print" Then
                dtPrint = clsDBFuncationality.GetDataTable(qry)
                Dim frmCRV As New frmCrystalReportViewer()
                If clsCommon.CompairString(objCommonVar.CurrComp_Code1, "JPR") = CompairStringResult.Equal Then
                    frmCRV.funreport(MyBase.Form_ID, CrystalReportFolder.MilkProcurement, dtPrint, "crptTmpPayDedSummaryJPR", "TP Print")
                    'frmCRV.funreport(CrystalReportFolder.MilkProcurement, dtPrint, "rptTempPayDedctSummaryList", "TP Print")
                ElseIf clsCommon.CompairString(objCommonVar.CurrComp_Code1, "CHU") = CompairStringResult.Equal Then
                    frmCRV.funreport(MyBase.Form_ID, CrystalReportFolder.MilkProcurement, dtPrint, "crptTmpPayDedSummaryCHU", "TP Print")
                Else
                    clsCommon.MyMessageBoxShow(Me, "This print is not intended for you", Me.Text)
                End If
                frmCRV = Nothing
            Else
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
                    clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                    Exit Sub
                Else
                    Gv1.DataSource = dt1
                    RadPageView1.SelectedPage = RadPageViewPage2
                    SetGridFormat()
                    ReStoreGridLayout()
                End If
                EnableDisableControl(False)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
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
            Gv1.Columns("Active").IsVisible = False
        End If
        If rdbOldCurrent.Checked Then
            Gv1.Columns("Active").IsVisible = False
        End If
        Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        Gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
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
            Gv1.Columns("DeductionName").HeaderText = "Deduction Name"
            Gv1.Columns("Amt Deducted").HeaderText = "Amt Deducted/Paid"
            Gv1.Columns("Zone_Code").HeaderText = "Zone Code"
            Gv1.Columns("Zone_Code").IsVisible = True
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
        Gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
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
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Export", Me.Text)
                Exit Sub
            End If
            Dim StrReportName As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptTemporaryPaymentDeductionSummary & "'"))
            Dim arrHeader As List(Of String) = New List(Of String)()
            If rdbOldOutstanding.Checked = True Then
                arrHeader.Add("Name : Old Outstanding")
            ElseIf rdbCurrentStanding.Checked = True Then
                arrHeader.Add("Name : Current Outstanding")
            ElseIf rdbOldOutstanding.Checked = True Then
                arrHeader.Add("Name : Current Opening + Deduction")
            ElseIf chkWithOpening.Checked = True Then
                arrHeader.Add("Name : Only Opening(Add/Ded)")
            ElseIf chkORD_CD.Checked = True Then
                arrHeader.Add("Name : Old Reduce Deduction + Current Deduction")
            End If
            'arrHeader.Add("Name : " & StrReportName)
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
                doc.Landscape = False
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
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, "Error", MessageBoxButtons.OK)
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
                'common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully",  Me.Text)
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully", Me.Text)
            End If
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(PageSetupReport_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", Me.Text)
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
        If rdbDocumentWise.Checked Then
        ElseIf rdbDocumentWiseDetail.Checked Then
        Else
            SetToDateNew()
        End If

    End Sub

    Private Sub fromDate_Validated(sender As Object, e As EventArgs) Handles fromDate.Validated
        If rdbDocumentWise.Checked Then
        ElseIf rdbDocumentWiseDetail.Checked Then
        Else
            SetToDateNew()
        End If
        'SetToDateNew()
    End Sub

    Sub SetToDateNew()
        If Not isLoad Then
            Dim PaymentCycleType As String = ""
            Dim PaymentCycleValue As Integer = 0
            ' If Not isLoad Then

            Dim dt As DataTable = clsDBFuncationality.GetDataTable("select top 1 PC_VALUE,PC_TYPE from TSPL_PAYMENT_CYCLE_MASTER ")
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "No Payment Cycle found on current MCC/Location", Me.Text)
                Exit Sub
            End If
            PaymentCycleType = clsCommon.myCstr(dt.Rows(0)("PC_TYPE"))
            PaymentCycleValue = clsCommon.myCdbl(dt.Rows(0)("PC_VALUE"))
            Dim dtCurr As DateTime = clsCommon.GETSERVERDATE()
            If clsCommon.CompairString(PaymentCycleType, "Day") = CompairStringResult.Equal Then
                If fromDate.Value.Day Mod PaymentCycleValue <> 1 And (Not PaymentCycleValue = 1) Then
                    clsCommon.MyMessageBoxShow(Me, "Date can only be first day of month or at interval of " & PaymentCycleValue & " Day, Because MCC has payment Cycle of " & PaymentCycleValue & " Day ")
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
                    clsCommon.MyMessageBoxShow(Me, "Date can only be first day of month, Because MCC has payment Cycle of Month Type", Me.Text)
                    fromDate.Value = "01/" & DatePart(DateInterval.Month, dtCurr) & "/" & DatePart(DateInterval.Year, dtCurr)
                    ToDate.Value = "01/" & DatePart(DateInterval.Month, dtCurr) & "/" & DatePart(DateInterval.Year, dtCurr)
                    Exit Sub
                End If
                ToDate.Value = DateAdd(DateInterval.Month, PaymentCycleValue, fromDate.Value)
            ElseIf clsCommon.CompairString(PaymentCycleType, "Year") = CompairStringResult.Equal Then
                If clsCommon.myCdbl(clsCommon.GetPrintDate(fromDate.Value, "dd")) <> 1 Then
                    clsCommon.MyMessageBoxShow(Me, "Date can only be first day of month, Because MCC has payment Cycle of Year Type", Me.Text)
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

    Private Sub rdbOldCurrent_Click(sender As Object, e As EventArgs) Handles rdbOldCurrent.Click
        Try
            If rdbOldCurrent.Checked Then
                btnPrint.Visible = True
            Else
                btnPrint.Visible = False
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text, MessageBoxButtons.OK)
        End Try
    End Sub

    Private Sub rdbCurrentStanding_Click(sender As Object, e As EventArgs) Handles rdbCurrentStanding.Click
        Try
            If rdbCurrentStanding.Checked Then
                btnPrint.Visible = True
            Else
                btnPrint.Visible = False
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text, MessageBoxButtons.OK)
        End Try
    End Sub

    Private Sub rdbOldOutstanding_Click(sender As Object, e As EventArgs) Handles rdbOldOutstanding.Click
        Try
            If rdbOldOutstanding.Checked Then
                btnPrint.Visible = False
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text, MessageBoxButtons.OK)
        End Try
    End Sub

    Private Sub chkWithOpening_Click(sender As Object, e As EventArgs) Handles chkWithOpening.Click
        Try
            If chkWithOpening.Checked Then
                btnPrint.Visible = False
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text, MessageBoxButtons.OK)
        End Try
    End Sub

    Private Sub chkORD_CD_Click(sender As Object, e As EventArgs) Handles chkORD_CD.Click
        Try
            If chkORD_CD.Checked Then
                btnPrint.Visible = False
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text, MessageBoxButtons.OK)
        End Try
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Try
            If chkDCSWise.Checked Then
                PageSetupReport_ID = clsCommon.myCstr(MyBase.Form_ID)
                If clsCommon.CompairString(objCommonVar.CurrComp_Code1, "UDP") = CompairStringResult.Equal OrElse
                     clsCommon.CompairString(objCommonVar.CurrComp_Code1, "CHT") = CompairStringResult.Equal OrElse
                    clsCommon.CompairString(objCommonVar.CurrComp_Code1, "JPR") = CompairStringResult.Equal OrElse
                    clsCommon.CompairString(objCommonVar.CurrComp_Code1, "RJS") = CompairStringResult.Equal Then
                    PrintUDP(False)
                Else
                    Print(False)
                End If
            Else
                common.clsCommon.MyMessageBoxShow(Me, "Check DCS Wise", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text, MessageBoxButtons.OK)
        End Try
    End Sub

    Private Sub fndArea__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndArea._MYValidating
        Try
            Dim sQuery As String = " Select TSPL_LOCATION_MASTER.Location_Code as Code ,  TSPL_LOCATION_MASTER.Location_Desc, Type from TSPL_LOCATION_MASTER "
            fndArea.Value = clsCommon.ShowSelectForm("Location@Plant@Master", sQuery, "Code", "TSPL_LOCATION_MASTER.Type <> 'PLANT' OR TSPL_LOCATION_MASTER.Location_Category <> 'Mcc'", fndArea.Value, "Code", isButtonClicked)

            'Dim Mcc As New String
            Dim dt As New DataTable
            Dim query As String = "select MCC_NAME from TSPL_MCC_MASTER  WHERE Area_Location_Code='" + fndArea.Value + "'"
            dt = Nothing
            dt = clsDBFuncationality.GetDataTable(query)

            'For i As Integer = 0 To dt.Rows.Count - 1
            '    Mcc.Add(dt.Rows(i)("MCC_NAME"))
            '    'arrMCCMapped.Add(dt.Rows(i)("MCC_NAME").ToString())
            'Next
            'txtMCC.Value = Mcc
            'txtMCC.arrValueMember

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.ToString)
        End Try
    End Sub

    Sub PrintChkwiseData(ByVal isPrint As Boolean, Optional ByVal isPrerint As Boolean = False)
        Try
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

            Dim StrVSPCODE As String = " Select Distinct VSP_CODE from TSPL_PAYMENT_PROCESS_DETAIL where Doc_No In (" + strDocNo + ")
 "
            Dim dtvspcode As DataTable = clsDBFuncationality.GetDataTable(StrVSPCODE)
            If dtvspcode IsNot Nothing AndAlso dtvspcode.Rows.Count > 0 Then
                Dim arr As New ArrayList
                For Each dr As DataRow In dtvspcode.Rows
                    arr.Add(clsCommon.myCstr(dr("VSP_CODE")))
                Next
                StrVSPCODE = clsCommon.GetMulcallString(arr)
            End If

            Dim qry As String = ""
            Dim dt1 As New DataTable

            Dim whrActiveInactive As String = Nothing
            If rbtnActive.Checked Then
                whrActiveInactive = " And TSPL_VLC_MASTER_HEAD.Active=1 "
                'whrActiveInactive = " And xxx.Active=1 "
            ElseIf rbtnInActive.Checked Then
                whrActiveInactive = " And TSPL_VLC_MASTER_HEAD.Active=0 "
                'whrActiveInactive = " And xxx.Active=0 "
            Else
                whrActiveInactive = Nothing
            End If

            If rdbOldOutstanding.Checked Then ''1,2,3
                qry = "select top 1 From_Date,To_Date from TSPL_PAYMENT_CYCLE_GENERATED where From_Date<'" + clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") + "' order by MCC_Code,From_Date desc"
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                    Throw New Exception("Please Generate Payment Cycle")
                End If
                qry = GetBAseQeryforcycle(clsCommon.myCDate(dt.Rows(0)("From_Date")), clsCommon.myCDate(dt.Rows(0)("To_Date")), StrVSPCODE, strDocNo)
            ElseIf rdbOldCurrent.Checked OrElse rdbCurrentStanding.Checked = True Then
                qry = GetBAseQeryforcycle(fromDate.Value, ToDate.Value, StrVSPCODE, strDocNo)
            ElseIf chkWithOpening.Checked OrElse chkORD_CD.Checked Then
                'Dim strDocNo As String = Nothing
                'Dim strDocQry = "select Doc_No from TSPL_PAYMENT_PROCESS_head where convert(date,From_Date,103)>=convert(date,('" + fromDate.Value + "'),103) and convert(date,To_Date,103)<=convert(date,('" + ToDate.Value + "'),103)"
                'Dim dtDocNo As DataTable = clsDBFuncationality.GetDataTable(strDocQry)
                'If dtDocNo IsNot Nothing AndAlso dtDocNo.Rows.Count > 0 Then
                '    Dim arr As New ArrayList
                '    For Each dr As DataRow In dtDocNo.Rows
                '        arr.Add(clsCommon.myCstr(dr("Doc_No")))
                '    Next
                '    strDocNo = clsCommon.GetMulcallString(arr)
                'End If

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
                        select case when isnull(TSPL_MULTIPLE_DEDUCTION_DETAIL.Trans_Type,'Deduction')='Addition' then 'A' else 'D' end Type
                        ,TSPL_VLC_MASTER_HEAD.VLC_CODE_VLC_Uploader as VSP_Uploader_Code 
                        ,TSPL_MULTIPLE_DEDUCTION_detail.Vendor_Code,TSPL_MULTIPLE_DEDUCTION_detail.Vendor_Name
                        ,TSPL_MULTIPLE_DEDUCTION_detail.DeductionCode as Ded_Code,TSPL_MULTIPLE_DEDUCTION_detail.Deduction_Desc as Ded_Desc
                        ,isnull(TSPL_MULTIPLE_DEDUCTION_detail.amount,0) as Amount
                        from TSPL_MULTIPLE_DEDUCTION_HEAD 
                        LEFT OUTER JOIN TSPL_MULTIPLE_DEDUCTION_DETAIL ON TSPL_MULTIPLE_DEDUCTION_HEAD.Document_No =TSPL_MULTIPLE_DEDUCTION_DETAIL.Document_No
                        left outer Join (select distinct TSPL_VLC_MASTER_HEAD.VSP_Code,TSPL_VLC_MASTER_HEAD.VLC_CODE_VLC_Uploader,TSPL_VLC_MASTER_HEAD.Active from TSPL_VLC_MASTER_HEAD) as TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code = TSPL_MULTIPLE_DEDUCTION_detail.Vendor_Code
                        where TSPL_MULTIPLE_DEDUCTION_HEAD.IsPosted=1 " + whrActiveInactive + "
                        and TSPL_MULTIPLE_DEDUCTION_HEAD.IsOpening=1 and convert(date,Document_Date,103)>=convert(date,('" + fromDate.Value + "'),103) and convert(date,Document_Date,103)<=convert(date,('" + ToDate.Value + "'),103) " + subMCCQry1 + "
                        ) Final " + subQryWhere + " group by  Final.Ded_Code ,[Type]" + subQry + " order by [Type] desc"

                ElseIf chkORD_CD.Checked = True Then ''5
                    qry = "select case when isnull(Final.Type,'D')='D' then 'Deduction' when isnull(Final.Type,'')='A' then 'Addition' else '' end Type,Max(Ded_code+'-'+Ded_Desc) as DeductionName
                        " + subDCSQry + ", sum(Amount) as [Amount] from ( 
                        Select 'D' Type,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader as VSP_Uploader_Code,TSPL_PAYMENT_PROCESS_DEDUCTION.Vendor_CODE, TSPL_PAYMENT_PROCESS_DEDUCTION.Vendor_NAME, TSPL_PAYMENT_PROCESS_DEDUCTION.Ded_Code,TSPL_PAYMENT_PROCESS_DEDUCTION.Ded_Desc,TSPL_PAYMENT_PROCESS_DEDUCTION.Reduce_Deduc_Amt as Amount 
                        from TSPL_PAYMENT_PROCESS_DEDUCTION 
                        left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code =TSPL_PAYMENT_PROCESS_DEDUCTION.Vendor_CODE
                        where  TSPL_PAYMENT_PROCESS_DEDUCTION.Doc_No in ('" + strOldDocNo + "')  AND ISNULL(TSPL_PAYMENT_PROCESS_DEDUCTION.Reduce_Deduc_Amt,0)>0  " + whrActiveInactive + "
                        UNION ALL 
                        Select 'D' Type,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader as VSP_Uploader_Code,TSPL_PAYMENT_PROCESS_DEDUCTION.Vendor_CODE, TSPL_PAYMENT_PROCESS_DEDUCTION.Vendor_NAME, TSPL_PAYMENT_PROCESS_DEDUCTION.Ded_Code,TSPL_PAYMENT_PROCESS_DEDUCTION.Ded_Desc,TSPL_PAYMENT_PROCESS_DEDUCTION.Amount-TSPL_PAYMENT_PROCESS_DEDUCTION.Reduce_Deduc_Amt as Amount 
                        from TSPL_PAYMENT_PROCESS_DEDUCTION 
                        left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code =TSPL_PAYMENT_PROCESS_DEDUCTION.Vendor_CODE
                        where  TSPL_PAYMENT_PROCESS_DEDUCTION.Doc_No in (" + strDocNo + ") " + subMCCQry1 + "" + whrActiveInactive + "
                        ) Final " + subQryWhere + " group by  Final.Ded_Code ,[Type] " + subQry + " order by [Type] desc"
                End If
            End If
            If btnPrint.Text = "Print" Then
                dtPrint = clsDBFuncationality.GetDataTable(qry)
                Dim frmCRV As New frmCrystalReportViewer()
                frmCRV.funreport(MyBase.Form_ID, CrystalReportFolder.MilkProcurement, dtPrint, "rptTempPayDedctSummaryList", "TP Print")
                frmCRV = Nothing
            Else
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
                    clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                    Exit Sub
                Else
                    Gv1.DataSource = dt1
                    RadPageView1.SelectedPage = RadPageViewPage2
                    SetGridFormatUDP()
                    ReStoreGridLayout()
                End If
                EnableDisableControl(False)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Function GetBAseQeryforcycle(ByVal fromDate As Date, ByVal ToDate As Date, ByVal strVSPCode As String, ByVal strDocNo As String) As String
        Dim whrActiveInactive As String = Nothing
        Dim BaseQry As String = "select TSPL_MULTIPLE_DEDUCTION_HEAD.Document_No,TSPL_MULTIPLE_DEDUCTION_HEAD.Document_Date,TSPL_MULTIPLE_DEDUCTION_DETAIL.Against_Deduction_DocNo as AP_Invoice_No,
TSPL_VENDOR_INVOICE_HEAD.Posting_Date as AP_Invoice_Date,TSPL_VENDOR_INVOICE_HEAD.Document_Type,TSPL_MULTIPLE_DEDUCTION_detail.DeductionCode,
TSPL_MULTIPLE_DEDUCTION_detail.Vendor_Code,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader,TSPL_MULTIPLE_DEDUCTION_DETAIL.Amount,0 as Reduce_Deduc_Amt ,1 as RI,TSPL_VLC_MASTER_HEAD.Active
from TSPL_MULTIPLE_DEDUCTION_DETAIL
left  join TSPL_MULTIPLE_DEDUCTION_HEAD on TSPL_MULTIPLE_DEDUCTION_HEAD.Document_No=TSPL_MULTIPLE_DEDUCTION_DETAIL.Document_No 
left join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No=TSPL_MULTIPLE_DEDUCTION_DETAIL.Against_Deduction_DocNo
left  join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code=TSPL_VENDOR_INVOICE_HEAD.Vendor_Code
where 2=2  and TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader In (" + strVSPCode + ")"


        BaseQry += "Union all
select TSPL_PAYMENT_PROCESS_DEDUCTION.Doc_No as Document_No, TSPL_PAYMENT_PROCESS_HEAD.To_Date as Doc_Date,TSPL_PAYMENT_PROCESS_DEDUCTION.AP_Invoice_No ,
TSPL_VENDOR_INVOICE_HEAD.Posting_Date as AP_Invoice_Date,TSPL_VENDOR_INVOICE_HEAD.Document_Type,TSPL_MULTIPLE_DEDUCTION_detail.DeductionCode,
TSPL_PAYMENT_PROCESS_DEDUCTION.Vendor_CODE,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader,TSPL_PAYMENT_PROCESS_DEDUCTION.Amount,
TSPL_PAYMENT_PROCESS_DEDUCTION.Reduce_Deduc_Amt,2 as RI,TSPL_VLC_MASTER_HEAD.Active   
from TSPL_PAYMENT_PROCESS_DEDUCTION
left join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No=TSPL_PAYMENT_PROCESS_DEDUCTION.AP_Invoice_No
left  join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_HEAD.Doc_No=TSPL_PAYMENT_PROCESS_DEDUCTION.Doc_No
left join TSPL_MULTIPLE_DEDUCTION_DETAIL on TSPL_MULTIPLE_DEDUCTION_DETAIL.Against_Deduction_DocNo=TSPL_PAYMENT_PROCESS_DEDUCTION.AP_Invoice_No 
left join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code=TSPL_VENDOR_INVOICE_HEAD.Vendor_Code
where 2=2  and TSPL_MULTIPLE_DEDUCTION_detail.DeductionCode is not null  and TSPL_PAYMENT_PROCESS_DEDUCTION.Doc_No In (" + strDocNo + ")
Union all
select TSPL_PAYMENT_PROCESS_CREDIT_NOTE.Doc_No as Document_No, TSPL_PAYMENT_PROCESS_HEAD.To_Date as Doc_Date,TSPL_PAYMENT_PROCESS_CREDIT_NOTE.AP_Invoice_No,
TSPL_VENDOR_INVOICE_HEAD.Posting_Date as AP_Invoice_Date,TSPL_VENDOR_INVOICE_HEAD.Document_Type,TSPL_MULTIPLE_DEDUCTION_detail.DeductionCode,
TSPL_PAYMENT_PROCESS_CREDIT_NOTE.Vendor_CODE,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader,TSPL_PAYMENT_PROCESS_CREDIT_NOTE.Amount,0 as Reduce_Deduc_Amt,3 as RI,TSPL_VLC_MASTER_HEAD.Active
from TSPL_PAYMENT_PROCESS_CREDIT_NOTE
left join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No=TSPL_PAYMENT_PROCESS_CREDIT_NOTE.AP_Invoice_No
left  join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_HEAD.Doc_No=TSPL_PAYMENT_PROCESS_CREDIT_NOTE.Doc_No
left join TSPL_MULTIPLE_DEDUCTION_DETAIL on TSPL_MULTIPLE_DEDUCTION_DETAIL.Against_Deduction_DocNo=TSPL_PAYMENT_PROCESS_CREDIT_NOTE.AP_Invoice_No 
left join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code=TSPL_VENDOR_INVOICE_HEAD.Vendor_Code
where 2=2 and TSPL_MULTIPLE_DEDUCTION_detail.DeductionCode is not null and TSPL_PAYMENT_PROCESS_CREDIT_NOTE.Doc_No In (" + strDocNo + ")
union all 
select TSPL_SD_SHIPMENT_HEAD.Document_Code as Document_No,TSPL_SD_SHIPMENT_HEAD.Document_Date,TSPL_Customer_Invoice_Head.Document_No as AP_Invoice_No ,TSPL_Customer_Invoice_Head.Posting_Date as AP_Invoice_Date,'D' as Document_Type,TSPL_SD_SHIPMENT_HEAD.Deduction as DeductionCode,TSPL_VLC_MASTER_HEAD.VSP_Code as Vendor_Code ,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader ,TSPL_SD_SHIPMENT_HEAD.Total_Amt as Amount,0 as Reduce_Deduc_Amt ,4 as RI,TSPL_VLC_MASTER_HEAD.Active 
from  TSPL_SD_SHIPMENT_HEAD 
left outer join TSPL_CUSTOMER_VENDOR_MAPPING on TSPL_CUSTOMER_VENDOR_MAPPING.Cust_Code=TSPL_SD_SHIPMENT_HEAD.Customer_Code
left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code = TSPL_CUSTOMER_VENDOR_MAPPING.Vendor_Code
left outer join TSPL_DEDUCTION_MASTER on TSPL_DEDUCTION_MASTER.Code = TSPL_SD_SHIPMENT_HEAD.Deduction 
left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code= TSPL_VLC_MASTER_HEAD.VSP_Code
left outer join TSPL_CUSTOMER_INVOICE_HEAD on TSPL_Customer_Invoice_Head.Against_Sale_No=TSPL_SD_SHIPMENT_HEAD.Sale_Invoice_No
where  TSPL_SD_SHIPMENT_HEAD.Trans_Type='MCC' and TSPL_SD_SHIPMENT_HEAD.is_cashsale='N'  and TSPL_SD_SHIPMENT_HEAD.Status=1 
and TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader In (" + strVSPCode + ")
union all
select TSPL_PAYMENT_PROCESS_MCC_SALE.Doc_No as Document_No, TSPL_PAYMENT_PROCESS_HEAD.To_Date as Doc_Date,TSPL_PAYMENT_PROCESS_MCC_SALE.AR_Invoice_No as AP_Invoice_No ,TSPL_Customer_Invoice_Head.Posting_Date as AP_Invoice_Date,'D' as Document_Type,TSPL_SD_SHIPMENT_HEAD.Deduction as DeductionCode,TSPL_CUSTOMER_VENDOR_MAPPING.Vendor_Code,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader,TSPL_PAYMENT_PROCESS_MCC_SALE.Amount,TSPL_PAYMENT_PROCESS_MCC_SALE.Reduce_Deduc_Amt,5 as RI,TSPL_VLC_MASTER_HEAD.Active
from TSPL_PAYMENT_PROCESS_MCC_SALE
left outer join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_Code=TSPL_PAYMENT_PROCESS_MCC_SALE.Shipment_Doc_No
left outer join TSPL_Customer_Invoice_Head on TSPL_Customer_Invoice_Head.Document_No=TSPL_PAYMENT_PROCESS_MCC_SALE.AR_Invoice_No
left outer join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_HEAD.Doc_No=TSPL_PAYMENT_PROCESS_MCC_SALE.Doc_No
left outer join TSPL_CUSTOMER_VENDOR_MAPPING on TSPL_CUSTOMER_VENDOR_MAPPING.Cust_Code=TSPL_SD_SHIPMENT_HEAD.Customer_Code
left join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code=TSPL_CUSTOMER_VENDOR_MAPPING.Vendor_Code
where 2=2 and TSPL_PAYMENT_PROCESS_MCC_SALE.Doc_No In (" + strDocNo + ")

union all
Select '' as Document_No,'' as Doc_Date,TSPL_VENDOR_INVOICE_HEAD.Document_No as AP_Invoice_No,TSPL_VENDOR_INVOICE_HEAD.Posting_Date as AP_Invoice_Date,TSPL_VENDOR_INVOICE_HEAD.Document_Type,TSPL_VENDOR_INVOICE_DETAIL.DeductionCode,TSPL_VENDOR_INVOICE_HEAD.Vendor_Code,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader,TSPL_VENDOR_INVOICE_HEAD.Document_Total as Amount,0 as Reduce_Deduc_Amt,6 as RI,TSPL_VLC_MASTER_HEAD.Active from TSPL_VENDOR_INVOICE_DETAIL
LEFT OUTER JOIN TSPL_VENDOR_INVOICE_HEAD ON TSPL_VENDOR_INVOICE_HEAD.Document_No=TSPL_VENDOR_INVOICE_DETAIL.Document_No
left outer join TSPL_VLC_MASTER_HEAD ON TSPL_VLC_MASTER_HEAD.VSP_Code=TSPL_VENDOR_INVOICE_HEAD.Vendor_Code
WHERE 2=2  and  RefDocType In('CAP-MSN-CDCS','CAP-MSN','CAP-OMSN') 

union all
Select TSPL_PAYMENT_PROCESS_CREDIT_NOTE.Doc_No as Document_No, TSPL_PAYMENT_PROCESS_HEAD.To_Date as Doc_Date,TSPL_PAYMENT_PROCESS_CREDIT_NOTE.AP_Invoice_No,
TSPL_VENDOR_INVOICE_HEAD.Posting_Date as AP_Invoice_Date,TSPL_VENDOR_INVOICE_HEAD.Document_Type,TSPL_VENDOR_INVOICE_DETAIL.DeductionCode,
TSPL_PAYMENT_PROCESS_CREDIT_NOTE.Vendor_CODE,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader,TSPL_PAYMENT_PROCESS_CREDIT_NOTE.Amount,0 as Reduce_Deduc_Amt,7 as RI,TSPL_VLC_MASTER_HEAD.Active 
from TSPL_PAYMENT_PROCESS_CREDIT_NOTE
left join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No=TSPL_PAYMENT_PROCESS_CREDIT_NOTE.AP_Invoice_No
left join TSPL_VENDOR_INVOICE_DETAIL on TSPL_VENDOR_INVOICE_DETAIL.Document_No=TSPL_VENDOR_INVOICE_HEAD.Document_No
left  join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_HEAD.Doc_No=TSPL_PAYMENT_PROCESS_CREDIT_NOTE.Doc_No
left join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code=TSPL_VENDOR_INVOICE_HEAD.Vendor_Code
where 2=2  and RefDocType In('CAP-MSN-CDCS','CAP-MSN','CAP-OMSN')

union all
Select TSPL_PAYMENT_PROCESS_DEDUCTION.Doc_No as Document_No, TSPL_PAYMENT_PROCESS_HEAD.To_Date as Doc_Date,TSPL_PAYMENT_PROCESS_DEDUCTION.AP_Invoice_No ,
TSPL_VENDOR_INVOICE_HEAD.Posting_Date as AP_Invoice_Date,TSPL_VENDOR_INVOICE_HEAD.Document_Type,TSPL_VENDOR_INVOICE_DETAIL.DeductionCode,
TSPL_PAYMENT_PROCESS_DEDUCTION.Vendor_CODE,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader,TSPL_PAYMENT_PROCESS_DEDUCTION.Amount,
TSPL_PAYMENT_PROCESS_DEDUCTION.Reduce_Deduc_Amt,8 as RI,TSPL_VLC_MASTER_HEAD.Active 
from TSPL_PAYMENT_PROCESS_DEDUCTION
left join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No=TSPL_PAYMENT_PROCESS_DEDUCTION.AP_Invoice_No
left join TSPL_VENDOR_INVOICE_DETAIL on TSPL_VENDOR_INVOICE_DETAIL.Document_No=TSPL_VENDOR_INVOICE_HEAD.Document_No
left  join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_HEAD.Doc_No=TSPL_PAYMENT_PROCESS_DEDUCTION.Doc_No
left join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code=TSPL_VENDOR_INVOICE_HEAD.Vendor_Code
where 2=2 and RefDocType In('CAP-MSN-CDCS','CAP-MSN','CAP-OMSN') "

        Dim qry As String = ""
        If chkDCSWise.Checked Then
            Dim subQry As String = Nothing
            If btnPrint.Text = "Print" Then
                subQry = "select '" + clsCommon.myCstr(objCommonVar.CurrentUser) + "' As PrintedBy,'" + clsCommon.GetPrintDate(fromDate) + "' As FromDate,'" + clsCommon.GetPrintDate(ToDate) + "' As ToDate,(Select Comp_Name From TSPL_COMPANY_MASTER Where Comp_Code1='" + clsCommon.myCstr(objCommonVar.CurrComp_Code1) + "') As Comp_Name,(Select City_Code From TSPL_COMPANY_MASTER Where Comp_Code1='" + clsCommon.myCstr(objCommonVar.CurrComp_Code1) + "') As City_Code, "
            Else
                subQry = "Select "
            End If
            qry = subQry + " case when Document_Type='D' then 'Deduction' else 'Addition'  end Type,DCSCode,[DCS Name] ,(DeductionCode+'-'+DeductionName) as DeductionName,cast((OP+Sale) as  decimal(18,2)) as [Opening+Sale],AMTDeducted as [Amt Deducted],cast((OP+Sale-AMTDeducted) as decimal(18,2)) as [Balance Amount],Active from (
select Document_Type,xx.DeductionCode,max(TSPL_DEDUCTION_MASTER.Description) as DeductionName,TSPL_VENDOR_MASTER.Vendor_Code,max(VLC_Code_VLC_Uploader) as DCSCode,max(TSPL_VENDOR_MASTER.Vendor_Name) as [DCS Name]
,sum((Amount-Reduce_Deduc_Amt) * (case when  Document_Date<'" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(fromDate), "dd/MMM/yyyy hh:mm:ss tt") + "' then 1 else 0 end) * (case when RI=1 or RI=4 or RI=6 then 1 else -1 end)) as OP 
,sum(Amount * (case when  Document_Date>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(fromDate), "dd/MMM/yyyy hh:mm:ss tt") + "' and Document_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(ToDate), "dd/MMM/yyyy hh:mm:ss tt") + "' then 1 else 0 end) * (case when (RI=1 or RI=4 or RI=6) then 1 else 0 end)) as Sale
,sum((Amount-Reduce_Deduc_Amt) * (case when Document_Date>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(fromDate), "dd/MMM/yyyy hh:mm:ss tt") + "' and Document_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(ToDate), "dd/MMM/yyyy hh:mm:ss tt") + "' then 1 else 0 end) * (case when (RI=1 or RI=4 or RI=6) then 0 else 1 end)) as AMTDeducted ,max(Active)Active
from (" + BaseQry + ")xx
left  join TSPL_DEDUCTION_MASTER on TSPL_DEDUCTION_MASTER.Code=xx.DeductionCode
left  join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=xx.Vendor_Code "
            If clsCommon.CompairString(objCommonVar.CurrComp_Code1, "JPR") = CompairStringResult.Equal Then
                qry += " where 2=2 and TSPL_DEDUCTION_MASTER.Code !='PDP' "
            End If
            qry += " group by Document_Type,DeductionCode,TSPL_VENDOR_MASTER.Vendor_Code
)xxx  where 2=2 "
            If clsCommon.myLen(txtMCC.Value) > 0 Then
                qry += " and TSPL_VLC_MASTER_HEAD.MCC='" + txtMCC.Value + "' "
            End If
            If clsCommon.myLen(txtDeduction.Value) > 0 Then
                qry += " and TSPL_MULTIPLE_DEDUCTION_detail.DeductionCode='" + txtDeduction.Value + "'"
            End If
            If rbtnActive.Checked Then
                'whrActiveInactive = " And TSPL_VLC_MASTER_HEAD.Active=1 "
                qry += " And xxx.Active=1 "
            ElseIf rbtnInActive.Checked Then
                'whrActiveInactive = " And TSPL_VLC_MASTER_HEAD.Active=0 "
                qry += " And xxx.Active=0 "
            Else
                qry += Nothing
            End If


            'where (OP+Sale-AMTDeducted)>0"
        Else
            qry = "select case when Document_Type='D' then 'Deduction' else 'Addition'  end Type ,(DeductionCode+'-'+DeductionName) as DeductionName,(OP+Sale) as [Opening+Sale],AMTDeducted as [Amt Deducted],(OP+Sale-AMTDeducted) as [Balance Amount],Active from (
select Document_Type,xx.DeductionCode,max(TSPL_DEDUCTION_MASTER.Description) as DeductionName
,sum((Amount-Reduce_Deduc_Amt) * (case when  Document_Date<'" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(fromDate), "dd/MMM/yyyy hh:mm:ss tt") + "' then 1 else 0 end) * (case when RI=1 or RI=4 or RI=6 then 1 else -1 end)) as OP 
,sum(Amount * (case when  Document_Date>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(fromDate), "dd/MMM/yyyy hh:mm:ss tt") + "' and Document_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(ToDate), "dd/MMM/yyyy hh:mm:ss tt") + "' then 1 else 0 end) * (case when RI=1 or RI=4 or RI=6 then 1 else 0 end)) as Sale
,sum((Amount-Reduce_Deduc_Amt) * (case when Document_Date>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(fromDate), "dd/MMM/yyyy hh:mm:ss tt") + "' and Document_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(ToDate), "dd/MMM/yyyy hh:mm:ss tt") + "' then 1 else 0 end) * (case when RI=1 or RI=4 or RI=6 then 0 else 1 end)) as AMTDeducted ,max(Active)Active
from (" + BaseQry + ")xx
left  join TSPL_DEDUCTION_MASTER on TSPL_DEDUCTION_MASTER.Code=xx.DeductionCode
left  join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=xx.Vendor_Code "

            If clsCommon.CompairString(objCommonVar.CurrComp_Code1, "JPR") = CompairStringResult.Equal Then
                qry += " where 2=2 and TSPL_DEDUCTION_MASTER.Code !='PDP' "
            End If

            qry += " group by Document_Type,DeductionCode
)xxx   where 2=2 "
            If clsCommon.myLen(txtMCC.Value) > 0 Then
                qry += " And TSPL_VLC_MASTER_HEAD.MCC ='" + txtMCC.Value + "' "
            End If
            If clsCommon.myLen(txtDeduction.Value) > 0 Then
                qry += " and TSPL_MULTIPLE_DEDUCTION_detail.DeductionCode='" + txtDeduction.Value + "'"
            End If
            If rbtnActive.Checked Then
                'whrActiveInactive = " And TSPL_VLC_MASTER_HEAD.Active=1 "
                qry += " And xxx.Active=1 "
            ElseIf rbtnInActive.Checked Then
                'whrActiveInactive = " And TSPL_VLC_MASTER_HEAD.Active=0 "
                qry += " And xxx.Active=0 "
            Else
                qry += Nothing
            End If

        End If
        Return qry
    End Function

    Private Sub rdbDocumentWise_CheckedChanged(sender As Object, e As EventArgs) Handles rdbDocumentWise.CheckedChanged
        If rdbDocumentWise.Checked Then
            MyLabel3.Visible = True
            fndMultDCS.Visible = True
            RadGroupBox1.Visible = False
            fndArea.Visible = False
            MyLabel4.Visible = False
            txtMCC.Visible = False
            MyLabel1.Visible = False
            txtDeduction.Visible = False
            chkDCSWise.Visible = False
            chkCurrntCycle.Visible = False
            ToDate.ReadOnly = False

        Else
            MyLabel3.Visible = False
            fndMultDCS.Visible = False
            RadGroupBox1.Visible = True
            MyLabel4.Visible = True
            txtMCC.Visible = True
            MyLabel1.Visible = True
            txtDeduction.Visible = True
            chkDCSWise.Visible = True
            chkCurrntCycle.Visible = True
            ToDate.ReadOnly = True

        End If
    End Sub

    Private Sub rdbDocumentWiseDetail_CheckedChanged(sender As Object, e As EventArgs) Handles rdbDocumentWiseDetail.CheckedChanged
        If rdbDocumentWiseDetail.Checked Then
            MyLabel3.Visible = True
            fndMultDCS.Visible = True
            RadGroupBox1.Visible = False
            fndArea.Visible = False
            MyLabel4.Visible = False
            txtMCC.Visible = False
            MyLabel1.Visible = False
            txtDeduction.Visible = False
            chkDCSWise.Visible = False
            chkCurrntCycle.Visible = False
            ToDate.ReadOnly = False
        Else
            MyLabel3.Visible = False
            fndMultDCS.Visible = False
            RadGroupBox1.Visible = True
            MyLabel4.Visible = True
            txtMCC.Visible = True
            MyLabel1.Visible = True
            txtDeduction.Visible = True
            chkDCSWise.Visible = True
            chkCurrntCycle.Visible = True
            ToDate.ReadOnly = True
        End If
    End Sub

    Private Sub fndMultDCS__My_Click(sender As Object, e As EventArgs) Handles fndMultDCS._My_Click
        Try
            Dim qry As String = " select VSP_Code as Code,VLC_Code_VLC_Uploader as Name,VLC_Name,VLC_Code from TSPL_VLC_MASTER_HEAD  "
            'If TxtCustomerType.arrValueMember IsNot Nothing AndAlso TxtCustomerType.arrValueMember.Count > 0 Then
            '    qry += " Where Cust_Type_Code In (" + clsCommon.GetMulcallString(TxtCustomerType.arrValueMember) + ")"
            'End If
            fndMultDCS.arrValueMember = clsCommon.ShowMultipleSelectForm("CustMulSel", qry, "Code", "Name", fndMultDCS.arrValueMember, fndMultDCS.arrDispalyMember)

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class
