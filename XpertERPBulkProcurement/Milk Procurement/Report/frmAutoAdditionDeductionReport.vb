Imports common
Imports System.ComponentModel
Imports System.IO

Public Class frmAutoAdditionDeductionReport
    Inherits FrmMainTranScreen

    Dim dt As DataTable = Nothing
    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnExp.Visible = MyBase.isExport
    End Sub
    Private Sub rptMilkBillProcurementSummary_Load(sender As Object, e As EventArgs) Handles Me.Load
        Reset()
    End Sub


    Sub Reset()
        txtFromDate.Value = clsCommon.GETSERVERDATE()
        txtToDate.Value = clsCommon.GETSERVERDATE()
        btnGo.Enabled = True
        Gv1.DataSource = Nothing
        Gv1.Rows.Clear()
        Gv1.Columns.Clear()
        Gv1.MasterTemplate.SummaryRowsBottom.Clear()
        dt = Nothing
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub
    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Print(False)
    End Sub
    Sub Print(ByVal isPrint As Boolean)
        Try
            'If SetToDate() Then
            Gv1.MasterTemplate.SummaryRowsBottom.Clear()
            Gv1.DataSource = Nothing
            Gv1.Rows.Clear()
            Gv1.Columns.Clear()
            Gv1.GroupDescriptors.Clear()
            Gv1.MasterTemplate.SummaryRowsBottom.Clear()
            Gv1.MasterView.Refresh()
            PageSetupReport_ID = clsCommon.myCstr(MyBase.Form_ID)
            TemplateGridview = Gv1
            Dim Qry As String = "select TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader as [DCS Code]
                                    ,TSPL_VLC_MASTER_HEAD.VSP_Code as [Code]
                                    ,CASE WHEN TSPL_DCS_ADDITION_DEDUCTION.Applicable_DCS_Type=0 THEN 'All'
                                    WHEN TSPL_DCS_ADDITION_DEDUCTION.Applicable_DCS_Type=1 THEN 'DCS'
                                    WHEN TSPL_DCS_ADDITION_DEDUCTION.Applicable_DCS_Type=2 THEN 'PDCS'
                                    WHEN TSPL_DCS_ADDITION_DEDUCTION.Applicable_DCS_Type=3 THEN 'BMC'
                                    WHEN TSPL_DCS_ADDITION_DEDUCTION.Applicable_DCS_Type=4 THEN 'Cluster'
                                    END AS [DCS Type]
                                    ,case when TSPL_VLC_MASTER_HEAD.isOwnBMC=1 then 'Yes' else 'No' end as [Is Own BMC]
                                    ,TSPL_VENDOR_INVOICE_HEAD.Description
                                    ,case when TSPL_DCS_ADDITION_DEDUCTION.Applicable_On=1 then 'Amount'
                                    when TSPL_DCS_ADDITION_DEDUCTION.Applicable_On=0 then 'Quantity' else '' end as [Apply On]
                                    ,case when TSPL_DCS_ADDITION_DEDUCTION.Applicable_Type=1 then 'Percentage'
                                    when TSPL_DCS_ADDITION_DEDUCTION.Applicable_Type=0 then 'Rate' else '' end as [Apply Type]
                                    ,Applicable_Value as [Formula]
                                    ,CASE when TSPL_DCS_ADDITION_DEDUCTION.Applicable_On=1 and TSPL_DCS_ADDITION_DEDUCTION.Applicable_Type=1 then
                                    cast((TSPL_VENDOR_INVOICE_DETAIL.Total_Amount*100)/TSPL_DCS_ADDITION_DEDUCTION.Applicable_Value as decimal(18,2)) 
                                     when TSPL_DCS_ADDITION_DEDUCTION.Applicable_On=0 and TSPL_DCS_ADDITION_DEDUCTION.Applicable_Type=0 then
                                    cast(TSPL_VENDOR_INVOICE_DETAIL.Total_Amount/TSPL_DCS_ADDITION_DEDUCTION.Applicable_Value as decimal(18,2)) 
                                    else 0 end AS [Base Amount/Quantity]                                    
                                    ,TSPL_VENDOR_INVOICE_DETAIL.Total_Amount As [Addition/Deduction Amount]
                                    ,TSPL_DCS_ADDITION_DEDUCTION.Description As [Addition/Deduction Description]
                                     from TSPL_VENDOR_INVOICE_DETAIL
                                    LEFT OUTER JOIN TSPL_VENDOR_INVOICE_HEAD ON TSPL_VENDOR_INVOICE_DETAIL.Document_No=TSPL_VENDOR_INVOICE_HEAD.Document_No
                                    LEFT OUTER JOIN TSPL_DCS_ADDITION_DEDUCTION ON TSPL_DCS_ADDITION_DEDUCTION.CODE=ISNULL(TSPL_VENDOR_INVOICE_DETAIL.DCS_Addition_Deduction,'')
                                    left outer join TSPL_VLC_MASTER_HEAD on VSP_Code=TSPL_VENDOR_INVOICE_HEAD.Vendor_Code
                                    WHERE ISNULL(TSPL_VENDOR_INVOICE_DETAIL.DCS_Addition_Deduction,'')<>'' and CONVERT(date,TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_Date,103)>='" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "' and CONVERT(date,TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_Date,103)<='" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "' "

            If rbtnAddition.IsChecked = True Then
                Qry += " and TSPL_DCS_ADDITION_DEDUCTION.Nature_Type=0 "
            ElseIf rbtnDeduction.IsChecked = True Then
                Qry += " and TSPL_DCS_ADDITION_DEDUCTION.Nature_Type=1 "
            End If

            If txtMultiMCC.arrValueMember IsNot Nothing AndAlso txtMultiMCC.arrValueMember.Count > 0 Then
                Qry += "  and TSPL_VLC_MASTER_HEAD.MCC in (" + clsCommon.GetMulcallString(txtMultiMCC.arrValueMember) + ")"
            End If
            If TxtMultiDCS.arrValueMember IsNot Nothing AndAlso TxtMultiDCS.arrValueMember.Count > 0 Then
                Qry += "and TSPL_VLC_MASTER_HEAD.VSP_Code in (" + clsCommon.GetMulcallString(TxtMultiDCS.arrValueMember) + ")"
            End If

            If TxtMultiDeduction.arrValueMember IsNot Nothing AndAlso TxtMultiDeduction.arrValueMember.Count > 0 Then
                Qry += " and TSPL_DCS_ADDITION_DEDUCTION.Code in (" + clsCommon.GetMulcallString(TxtMultiDeduction.arrValueMember) + ")"
            End If


            dt = Nothing
            dt = clsDBFuncationality.GetDataTable(Qry)
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                Throw New Exception("No Data Found to Display")
            End If


            Gv1.DataSource = dt
            RadPageView1.SelectedPage = RadPageViewPage2
            SetGridFormat(Gv1)
            EnableDisaableControls(False)
            ReStoreGridLayout()
            'End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message)
        End Try
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

    Sub SetGridFormat(ByRef Gv1 As RadGridView)
        Gv1.ShowGroupPanel = True
        Gv1.ShowRowHeaderColumn = False
        Gv1.AllowAddNewRow = False
        Gv1.AllowDeleteRow = False
        Gv1.EnableFiltering = True
        Gv1.ShowFilteringRow = True
        Gv1.MasterTemplate.SummaryRowsBottom.Clear()

        For ii As Integer = 0 To Gv1.Columns.Count - 1
            Gv1.Columns(ii).ReadOnly = True
            Gv1.Columns(ii).BestFit()
        Next
        Gv1.Columns("Base Amount/Quantity").FormatString = "{0:n2}"
        Gv1.Columns("Addition/Deduction Amount").FormatString = "{0:n2}"
        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim BaseAmount As New GridViewSummaryItem("Base Amount/Quantity", "{0:n2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(BaseAmount)
        Dim Amount As New GridViewSummaryItem("Addition/Deduction Amount", "{0:n2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(Amount)
        Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

        Gv1.AutoSizeRows = False
        Gv1.BestFitColumns()
    End Sub
    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        EnableDisaableControls(True)
    End Sub
    Sub EnableDisaableControls(ByVal flag As Boolean)
        txtFromDate.Enabled = flag
        txtToDate.Enabled = flag
        GroupBox1.Enabled = flag
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
            Dim strHeading As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.frmAutoAdditionDeductionReport & "'"))

            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Report Name : " + strHeading)
            arrHeader.Add("Date Range from : " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy"))

            transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
            If exporter = EnumExportTo.Excel Then
                'transportSql.QuickExportToExcel(Gv1, "", Me.Text,, arrHeader)
                transportSql.exportdata(Gv1, "", Me.Text, , arrHeader, False, True)
            Else
                clsCommon.MyExportToPDF(strHeading, Gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
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

    Private Sub txtFromDate_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtFromDate.Validating
        'SetToDate()
    End Sub
    Private Sub txtFromDate_Leave(sender As Object, e As EventArgs) Handles txtFromDate.Leave
        'SetToDate()
    End Sub
    Function SetToDate() As Boolean
        Try
            Dim PaymentCycleType As String = ""
            Dim PaymentCycleValue As Integer = 0
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(" select Payment_Cycle,PC_TYPE,PC_VALUE from ( select TSPL_PAYMENT_CYCLE_MASTER.PC_VALUE as Payment_Cycle,
 TSPL_PAYMENT_CYCLE_MASTER.PC_TYPE,TSPL_PAYMENT_CYCLE_MASTER.PC_VALUE  from TSPL_PAYMENT_CYCLE_MASTER  where 
 TSPL_PAYMENT_CYCLE_MASTER.IsDefault=1 ) xx group by Payment_Cycle,PC_TYPE,PC_VALUE")
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                Throw New Exception("No Payment Cycle found on current MCC/Location")
            End If
            If dt.Rows.Count > 1 Then
                Throw New Exception("Selected MCC's Payment Cycle Should be Same")
            End If
            'If SettMPIncentiveEntryApplyMonthly Then
            '    txtFromDate.Value = New Date(txtFromDate.Value.Year, txtFromDate.Value.Month, 1)
            '    txtToDate.Value = txtFromDate.Value.AddMonths(1).AddDays(-1)
            'Else
            PaymentCycleType = clsCommon.myCstr(dt.Rows(0)("PC_TYPE"))
            PaymentCycleValue = clsCommon.myCdbl(dt.Rows(0)("PC_VALUE"))
            Dim dtCurr As DateTime = clsCommon.GETSERVERDATE()
            If clsCommon.CompairString(PaymentCycleType, "Day") = CompairStringResult.Equal Then
                If txtFromDate.Value.Day Mod PaymentCycleValue <> 1 And (Not PaymentCycleValue = 1) Then
                    txtFromDate.Value = New Date(dtCurr.Year, dtCurr.Month, 1)
                    txtToDate.Value = txtFromDate.Value
                    Throw New Exception("Date can only be first day of month or at interval of " & PaymentCycleValue & " Day, Because MCC has payment Cycle of " & PaymentCycleValue & " Day ")
                End If
                txtToDate.Value = txtFromDate.Value.AddDays(PaymentCycleValue - 1)

                If txtFromDate.Value.Month <> txtToDate.Value.Month Then
                    txtToDate.Value = New Date(txtFromDate.Value.Year, txtFromDate.Value.Month, 1).AddMonths(1).AddDays(-1)
                End If
                Dim dtNxtPay As DateTime = txtToDate.Value.AddDays(Math.Ceiling(PaymentCycleValue / 2.0))
                If txtFromDate.Value.Month <> dtNxtPay.Month Then
                    txtToDate.Value = New Date(txtFromDate.Value.Year, txtFromDate.Value.Month, 1).AddMonths(1).AddDays(-1)
                End If
            ElseIf clsCommon.CompairString(PaymentCycleType, "Month") = CompairStringResult.Equal Then
                If clsCommon.myCdbl(clsCommon.GetPrintDate(txtFromDate.Value, "dd")) <> 1 Then

                    txtFromDate.Value = "01/" & DatePart(DateInterval.Month, dtCurr) & "/" & DatePart(DateInterval.Year, dtCurr)
                    txtToDate.Value = "01/" & DatePart(DateInterval.Month, dtCurr) & "/" & DatePart(DateInterval.Year, dtCurr)
                    Throw New Exception("Date can only be first day of month, Because MCC has payment Cycle of Month Type")
                End If
                txtToDate.Value = DateAdd(DateInterval.Month, PaymentCycleValue, txtFromDate.Value)
            ElseIf clsCommon.CompairString(PaymentCycleType, "Year") = CompairStringResult.Equal Then
                If clsCommon.myCdbl(clsCommon.GetPrintDate(txtFromDate.Value, "dd")) <> 1 Then
                    txtFromDate.Value = "01/" & DatePart(DateInterval.Month, dtCurr) & "/" & DatePart(DateInterval.Year, dtCurr)
                    txtToDate.Value = "01/" & DatePart(DateInterval.Month, dtCurr) & "/" & DatePart(DateInterval.Year, dtCurr)
                    Throw New Exception("Date can only be first day of month, Because MCC has payment Cycle of Year Type")
                End If
                txtToDate.Value = DateAdd(DateInterval.Year, PaymentCycleValue, txtFromDate.Value)
            ElseIf clsCommon.CompairString(PaymentCycleType, "Week") = CompairStringResult.Equal Then
                Dim today As Date = txtFromDate.Value
                Dim dayDiff As Integer = today.DayOfWeek - IIf(PaymentCycleValue = 1, DayOfWeek.Sunday, IIf(PaymentCycleValue = 2, DayOfWeek.Monday, IIf(PaymentCycleValue = 3, DayOfWeek.Tuesday, IIf(PaymentCycleValue = 4, DayOfWeek.Wednesday, IIf(PaymentCycleValue = 5, DayOfWeek.Thursday, IIf(PaymentCycleValue = 6, DayOfWeek.Friday, DayOfWeek.Saturday))))))
                txtFromDate.Value = today.AddDays(-dayDiff)
                txtToDate.Value = txtFromDate.Value.AddDays(6)
            End If
            'End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            Return False
        End Try
        Return True
    End Function

    Private Sub rmsaveLayout_Click(sender As Object, e As EventArgs) Handles rmsaveLayout.Click
        Try
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
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        Try
            clsGridLayout.DeleteData(PageSetupReport_ID, objCommonVar.CurrentUserCode)
            common.clsCommon.MyMessageBoxShow("Layout Delete successfully", "Information")
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtMultiMCC_My_Click(sender As Object, e As EventArgs) Handles txtMultiMCC._My_Click
        Dim qry As String = "select MCC_Code,MCC_NAME,TSPL_MCC_MASTER.plant_code as [Plant Code],tspl_location_master.location_desc as [Plant Name] from TSPL_MCC_MASTER left join tspl_location_master on tspl_location_master.location_code=TSPL_MCC_MASTER.plant_code where 2=2 "
        txtMultiMCC.arrValueMember = clsCommon.ShowMultipleSelectForm("MCC@VMPIFSC", qry, "MCC_Code", "MCC_NAME", txtMultiMCC.arrValueMember, txtMultiMCC.arrDispalyMember)
        RefreshDCS()
    End Sub

    Private Sub TxtMultiDeduction_My_Click(sender As Object, e As EventArgs) Handles TxtMultiDeduction._My_Click
        Dim qry As String = " select TSPL_DCS_ADDITION_DEDUCTION.Code, TSPL_DCS_ADDITION_DEDUCTION.Description from TSPL_DCS_ADDITION_DEDUCTION"
        TxtMultiDeduction.arrValueMember = clsCommon.ShowMultipleSelectForm("DEDMulSel", qry, "Code", "Description", TxtMultiDeduction.arrValueMember, TxtMultiDeduction.arrDispalyMember)
    End Sub

    Sub RefreshDCS()
        If TxtMultiDCS.arrValueMember IsNot Nothing AndAlso TxtMultiDCS.arrValueMember.Count > 0 Then
            Dim qry As String = "select VSP_Code from TSPL_VLC_MASTER_HEAD where  VSP_Code in (" + clsCommon.GetMulcallString(TxtMultiDCS.arrValueMember) + ")  and MCC in (" + clsCommon.GetMulcallString(txtMultiMCC.arrValueMember) + ")  "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            TxtMultiDCS.arrValueMember = Nothing
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim arr As New ArrayList
                For Each dr As DataRow In dt.Rows
                    arr.Add(clsCommon.myCstr(dr("VSP_Code")))
                Next
                TxtMultiDCS.arrValueMember = arr
            End If
        End If
    End Sub

    Private Sub TxtMultiDCS_My_Click(sender As Object, e As EventArgs) Handles TxtMultiDCS._My_Click
        Try
            Dim qry As String = "select TSPL_VLC_MASTER_HEAD.VSP_Code,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader,TSPL_VLC_MASTER_HEAD.Route_Code,TSPL_MCC_ROUTE_MASTER.Route_Name from TSPL_VLC_MASTER_HEAD left outer join TSPL_MCC_ROUTE_MASTER on TSPL_MCC_ROUTE_MASTER.Route_Code=TSPL_VLC_MASTER_HEAD.Route_Code where 2=2 "
            If txtMultiMCC.arrValueMember IsNot Nothing AndAlso txtMultiMCC.arrValueMember.Count > 0 Then
                qry += " and TSPL_VLC_MASTER_HEAD.MCC in (" + clsCommon.GetMulcallString(txtMultiMCC.arrValueMember) + ") "
            End If
            TxtMultiDCS.arrValueMember = clsCommon.ShowMultipleSelectForm("VLC@VMPIFSC", qry, "VSP_Code", "VLC_Code_VLC_Uploader", TxtMultiDCS.arrValueMember, TxtMultiDCS.arrDispalyMember)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Try
            Dim Qry As String = Nothing
            Dim Qry1 As String = Nothing
            Dim Qry2 As String = Nothing

            ''Dim Qry3 As String = Nothing
            If txtMultiMCC.arrValueMember IsNot Nothing AndAlso txtMultiMCC.arrValueMember.Count > 0 Then
                Qry1 = "  and TSPL_VLC_MASTER_HEAD.MCC in (" + clsCommon.GetMulcallString(txtMultiMCC.arrValueMember) + ")"
            End If

            If TxtMultiDeduction.arrValueMember IsNot Nothing AndAlso TxtMultiDeduction.arrValueMember.Count > 0 Then
                Qry2 = " and TSPL_DCS_ADDITION_DEDUCTION.Code in (" + clsCommon.GetMulcallString(TxtMultiDeduction.arrValueMember) + ")"
            End If



            Qry = "select round(row_number() over(order by(cast(TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader as integer))),0) as SNo, cast(TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader as integer) as [DCS Code] 
                                    ,max(TSPL_VLC_MASTER_HEAD.VSP_Code) as [Code]
									,max(TSPL_VLC_MASTER_HEAD.VLC_Name) as [Vender Name]
                                     ,max(TSPL_MCC_MASTER.MCC_Name) as Area
									 ,max(TSPL_COMPANY_MASTER.Regn_No) as Regn_No
									 ,max(TSPL_COMPANY_MASTER.Phone1 ) as Phone1
									 ,max(TSPL_COMPANY_MASTER.Comp_Name) as Comp_Name
									 ,max(MILK_SRN_DETAIL.AMOUNT) AS SRN_AMOUNT        
									 ,max(MILK_RECEIPT_DETAIL.ACC_WEIGHT)as ACC_WEIGHT
                                    ,sum(TSPL_VENDOR_INVOICE_DETAIL.Total_Amount) As [Addition/Deduction Amount]
                                    ,max(TSPL_DCS_ADDITION_DEDUCTION.Description) As [Addition/Deduction Description]
									,'" + txtFromDate.Value + "' As FromDate ,'" + txtToDate.Value + "' As ToDate
                                     from TSPL_VENDOR_INVOICE_DETAIL
                                    LEFT OUTER JOIN TSPL_VENDOR_INVOICE_HEAD ON TSPL_VENDOR_INVOICE_DETAIL.Document_No=TSPL_VENDOR_INVOICE_HEAD.Document_No
                                    LEFT OUTER JOIN TSPL_DCS_ADDITION_DEDUCTION ON TSPL_DCS_ADDITION_DEDUCTION.CODE=ISNULL(TSPL_VENDOR_INVOICE_DETAIL.DCS_Addition_Deduction,'')
                                    left outer join TSPL_VLC_MASTER_HEAD on VSP_Code=TSPL_VENDOR_INVOICE_HEAD.Vendor_Code
									 left outer  join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code=TSPL_VLC_MASTER_HEAD.MCC
									 left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code=TSPL_VLC_MASTER_HEAD.comp_code
									 LEFT OUTER JOIN (select  TSPL_MILK_SRN_HEAD.VSP_CODE,sum(TSPL_MILK_SRN_DETAIL.AMOUNT) as 'amount' from TSPL_MILK_SRN_HEAD 
									left outer join TSPL_MILK_SRN_DETAIL on TSPL_MILK_SRN_DETAIL.DOC_CODE=TSPL_MILK_SRN_HEAD.DOC_CODE
									where 
									convert(date,TSPL_MILK_SRN_HEAD.DOC_DATE,103)>='" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "'  and CONVERT(date,TSPL_MILK_SRN_HEAD.DOC_DATE,103)<='" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "'  
									group by TSPL_MILK_SRN_HEAD.VSP_CODE) MILK_SRN_DETAIL ON MILK_SRN_DETAIL.VSP_CODE=TSPL_VLC_MASTER_HEAD.VSP_Code
									LEFT OUTER JOIN (select  TSPL_MILK_RECEIPT_DETAIL.VSP_CODE,sum(TSPL_MILK_RECEIPT_DETAIL.ACC_WEIGHT) as 'ACC_WEIGHT' from TSPL_MILK_RECEIPT_DETAIL 
									where 
									convert(date,TSPL_MILK_RECEIPT_DETAIL.DOC_DATE,103)>='" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "'  and CONVERT(date,TSPL_MILK_RECEIPT_DETAIL.DOC_DATE,103)<='" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "'  
									group by TSPL_MILK_RECEIPT_DETAIL.VSP_CODE) MILK_RECEIPT_DETAIL ON MILK_RECEIPT_DETAIL.VSP_CODE=TSPL_VLC_MASTER_HEAD.VSP_Code
                                    WHERE ISNULL(TSPL_VENDOR_INVOICE_DETAIL.DCS_Addition_Deduction,'')<>'' and CONVERT(date,TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_Date,103)>='" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "' and CONVERT(date,TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_Date,103)<='" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "' " + Qry1 + Qry2 + " group by TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader
									order by [DCS Code] asc"



            Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(Qry)
            If dt1.Rows.Count > 0 Then
                Dim frmCRV As New frmCrystalReportViewer()
                frmCRV.funreport(CrystalReportFolder.MilkProcurement, dt1, "crptAutoAdditionDeductionNewGNG", "AutoPrint")
                frmCRV = Nothing
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message)
        End Try
    End Sub
End Class
