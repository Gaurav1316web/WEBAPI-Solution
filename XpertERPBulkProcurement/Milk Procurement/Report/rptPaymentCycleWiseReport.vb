Imports common
Imports System.ComponentModel
Imports System.IO

Public Class rptPaymentCycleWiseReport
    Inherits FrmMainTranScreen
    Dim isLoad As Boolean = True
    Dim MultipleFinderFillAuto As Boolean = False
    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
    End Sub

    Private Sub rptMilkAnalysis_Load(sender As Object, e As EventArgs) Handles Me.Load
        isLoad = True
        RadGroupBox3.Visible = True
        fromDate.Value = "01/" & DatePart(DateInterval.Month, clsCommon.GETSERVERDATE()) & "/" & DatePart(DateInterval.Year, clsCommon.GETSERVERDATE())
        ToDate.Value = "15/" & DatePart(DateInterval.Month, clsCommon.GETSERVERDATE()) & "/" & DatePart(DateInterval.Year, clsCommon.GETSERVERDATE())
        Reset()
        btnPrint.Visible = True
        FillAllMCCDefault()
        RadGroupBox1.Visible = True
        fun_gridfill()
        isLoad = False
    End Sub
    Private Sub fun_gridfill()
        dgv_Groupmapping.AutoGenerateColumns = False
        Try
            Dim strQuery As String = "select Doc_No,concat(convert(varchar,From_Date,103),' - ',convert(varchar,To_Date,103)) as Description from TSPL_PAYMENT_PROCESS_HEAD"
            transportSql.FillGridView(strQuery, dgv_Groupmapping)
            dgv_Groupmapping.Columns(0).FieldName = "Doc_No"
            dgv_Groupmapping.Columns(1).FieldName = "Description"
            dgv_Groupmapping.Select()
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message.ToString(), Me.Text)
        End Try
    End Sub
    Sub Reset()

        'Gv1.DataSource = Nothing
        'Gv1.Rows.Clear()
        'Gv1.Columns.Clear()
        'Gv1.MasterTemplate.SummaryRowsBottom.Clear()
        gv1.DataSource = Nothing
        EnableDisableCtrl(True)
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub
    Sub EnableDisableCtrl(ByVal val As Boolean)
        txtRoute.Enabled = val
        txtBank.Enabled = val
        txtDCS.Enabled = val
        txtMCC_BMC.Enabled = val
        RadGroupBox1.Enabled = val
        chkShowData.Enabled = val
        RadGroupBox2.Enabled = val
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs)
        Print(False)
    End Sub
    Sub Print(ByVal isPrint As Boolean, Optional ByVal isPrerint As Boolean = False)
        Try
            Dim arr As ArrayList = Nothing
            Dim strDocumentCode As String = ""
            Dim qry = "select  Doc_No from TSPL_PAYMENT_PROCESS_HEAD where convert (date,TSPL_PAYMENT_PROCESS_HEAD.From_Date,103) >= convert (date, '" + fromDate.Value + "',103) and convert (date,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103) <= convert (date, '" + ToDate.Value + "',103)  " ' and isPosted = 1
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                arr = New ArrayList()
                For Each dr As DataRow In dt.Rows
                    arr.Add(clsCommon.myCstr(dr("Doc_No")))
                Next
                strDocumentCode = clsCommon.GetMulcallString(arr)
            End If
            Dim strDCS As String = ""
            If txtDCS.arrValueMember IsNot Nothing AndAlso txtDCS.arrValueMember.Count > 0 Then
                strDCS = clsCommon.GetMulcallString(txtDCS.arrValueMember)
            End If
            Dim strRoute As String = ""
            If txtRoute.arrValueMember IsNot Nothing AndAlso txtRoute.arrValueMember.Count > 0 Then
                strRoute = clsCommon.GetMulcallString(txtRoute.arrValueMember)
            End If
            Dim strLocaton As String = ""
            If txtMCC_BMC.arrValueMember IsNot Nothing AndAlso txtMCC_BMC.arrValueMember.Count > 0 Then
                strLocaton = clsCommon.GetMulcallString(txtMCC_BMC.arrValueMember)
            End If
            Dim strBank As String = ""
            If txtBank.arrValueMember IsNot Nothing AndAlso txtBank.arrValueMember.Count > 0 Then
                strBank = clsCommon.GetMulcallString(txtBank.arrValueMember)
            End If
            Dim strHold As String = ""
            If rdbHold.Checked = True Then
                strHold = 1
            ElseIf rdbUnhold.Checked = True Then
                strHold = 0
            End If

            If chkPaymentSummary.Checked = True Then
                strDocumentCode = ""
                For i As Integer = 0 To dgv_Groupmapping.Rows.Count - 1
                    If CBool(dgv_Groupmapping.Rows(i).Cells(2).Value = True) Then
                        If clsCommon.myLen(strDocumentCode) > 0 Then
                            strDocumentCode += ","
                        End If
                        strDocumentCode += "'" + clsCommon.myCstr(dgv_Groupmapping.Rows(i).Cells(0).Value) + "'"
                    End If
                Next

            End If

            If clsCommon.myLen(strDocumentCode) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Data Not Found!", Me.Text)
                Return
            End If

            If chkPaymentSummary.Checked = True Then

                Dim strQry As String = "select min(From_Date) as From_Date,max(To_Date) as To_Date
                                        from TSPL_PAYMENT_PROCESS_HEAD where Doc_No in (" + strDocumentCode + ")"

                Dim PaymentProcessDeatil As DataTable = clsDBFuncationality.GetDataTable(strQry)
                clsPaymentProcessHead.PaymentProcessDrCrPrint(strDocumentCode, clsCommon.myCstr(PaymentProcessDeatil.Rows(0).Item("From_Date")), clsCommon.myCstr(PaymentProcessDeatil.Rows(0).Item("To_Date")), strLocaton, strDCS, strRoute, "", "", "")

            ElseIf chkShowData.Checked = True Then
                Dim query As String = ""
                If rbtnHeadLoad.IsChecked = True Then
                    query = "select VLC_Code_VLC_Uploader as [VSP Uploader Code],VSP_CODE as [VSP Code],Vendor_Name AS [VSP Name],MAX(Head_Load_Amount) AS [Amount]
                    from ("
                    query += clsCommon.myCstr(clsPaymentProcessHead.Load_Report_Paymnet_RCDF(strDocumentCode, fromDate.Text, ToDate.Text, strLocaton, strDCS, strRoute, strBank, strHold, True))
                    query += " )xx GROUP BY VLC_Code_VLC_Uploader,VSP_CODE,Vendor_Name"
                ElseIf rbtnOutstanding.IsChecked = True Then
                    query = "select Final.VSP_Uploader_Code as [VSP Uploader Code], Final.Vendor_CODE as [VSP Code], Vendor_NAME as [VSP Name], sum(Amount) as [Amount] from (
                            select TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader as VSP_Uploader_Code,TSPL_PAYMENT_PROCESS_DEDUCTION.Vendor_CODE, TSPL_PAYMENT_PROCESS_DEDUCTION.Vendor_NAME, TSPL_PAYMENT_PROCESS_DEDUCTION.Ded_Code,TSPL_PAYMENT_PROCESS_DEDUCTION.Ded_Desc,TSPL_PAYMENT_PROCESS_DEDUCTION.Reduce_Deduc_Amt as Amount 
                            from TSPL_PAYMENT_PROCESS_DEDUCTION 
                            left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code =TSPL_PAYMENT_PROCESS_DEDUCTION.Vendor_CODE
                            where  TSPL_PAYMENT_PROCESS_DEDUCTION.Doc_No in (" + strDocumentCode + ") 
                            ) Final group by  final.VSP_Uploader_Code, Final.Vendor_CODE, Vendor_NAME, Final.Ded_Code having sum(Amount)>0"
                End If

                Dim dt2 As DataTable = clsDBFuncationality.GetDataTable(query)
                If dt2 IsNot Nothing And dt2.Rows.Count > 0 Then
                    gv1.DataSource = Nothing
                    gv1.Columns.Clear()
                    gv1.Rows.Clear()
                    gv1.GroupDescriptors.Clear()
                    gv1.MasterTemplate.SummaryRowsBottom.Clear()
                    gv1.ShowGroupPanel = True
                    gv1.EnableFiltering = True
                    RadPageView1.SelectedPage = RadPageViewPage2
                    gv1.DataSource = dt2
                    SetGridFormat()
                Else
                    clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)
                    Exit Sub
                End If
            Else
                clsPaymentProcessHead.Load_Report_Paymnet_RCDF(strDocumentCode, fromDate.Text, ToDate.Text, strLocaton, strDCS, strRoute, strBank, strHold, False) ' clsCommon.GetMulcallString(mfndMcc.arrValueMember)
            End If
            EnableDisableCtrl(False)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub SetGridFormat()
        gv1.ShowGroupPanel = True
        gv1.ShowRowHeaderColumn = False
        gv1.AllowAddNewRow = False
        gv1.AllowDeleteRow = False
        gv1.EnableFiltering = True
        gv1.ShowFilteringRow = True
        gv1.MasterTemplate.SummaryRowsBottom.Clear()
        Dim summaryRowItem As New GridViewSummaryRowItem()
        For ii As Integer = 0 To gv1.Columns.Count - 1
            gv1.Columns(ii).ReadOnly = True
            gv1.Columns(ii).BestFit()
        Next
        Dim item1 As New GridViewSummaryItem("Amount", "{0:n2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)
        gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        gv1.AutoSizeRows = False
        gv1.BestFitColumns()
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Reset()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub TxtMCC__My_Click(sender As Object, e As EventArgs) Handles txtRoute._My_Click
        Dim qry As String = " select TSPL_BULK_ROUTE_MASTER.ROUTE_NO as Code , ROUTE_NAME as Name from TSPL_BULK_ROUTE_MASTER "
        txtRoute.arrValueMember = clsCommon.ShowMultipleSelectForm("PCWR@Route@MFinder", qry, "Code", "Name", txtRoute.arrValueMember, txtRoute.arrDispalyMember)
    End Sub

    Sub SetToDate()
        If Not isLoad Then

            Dim PaymentCycleType As String = ""
            Dim PaymentCycleValue As Integer = 0

            'If MultipleFinderFillAuto = True Then
            If mfndMcc.arrValueMember Is Nothing OrElse mfndMcc.arrValueMember.Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Data Not Found", Me.Text)
                Exit Sub
            End If
            'End If
            Dim strMCCcode = ""
            'If MultipleFinderFillAuto Then
            strMCCcode = " location_Code in ( " + clsCommon.GetMulcallString(mfndMcc.arrValueMember) + ")  "
            'End If

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(" select TSPL_MCC_MASTER.Payment_Cycle,TSPL_PAYMENT_CYCLE_MASTER.PC_TYPE,TSPL_PAYMENT_CYCLE_MASTER.PC_VALUE  from TSPL_MCC_MASTER left outer join TSPL_PAYMENT_CYCLE_MASTER on TSPL_PAYMENT_CYCLE_MASTER.PC_CODE=TSPL_MCC_MASTER.Payment_Cycle   where TSPL_MCC_MASTER.MCC_Code  in (select Location_Code  from TSPL_LOCATION_MASTER where " + strMCCcode + " and Location_Category='MCC' and Rejected_Type='N') ")
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "No Payment Cycle found on current MCC/Location", Me.Text)
                Exit Sub
            End If
            PaymentCycleType = clsCommon.myCstr(dt.Rows(0)("PC_TYPE"))
            PaymentCycleValue = clsCommon.myCdbl(dt.Rows(0)("PC_VALUE"))
            Dim dtCurr As DateTime = clsCommon.GETSERVERDATE()
            If clsCommon.CompairString(PaymentCycleType, "Day") = CompairStringResult.Equal Then
                If fromDate.Value.Day Mod PaymentCycleValue <> 1 And (Not PaymentCycleValue = 1) Then
                    clsCommon.MyMessageBoxShow(Me, "Date can only be first day of month or at interval of " & PaymentCycleValue & " Day, Because MCC has payment Cycle of " & PaymentCycleValue & " Day ", Me.Text)
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
            ' End If
            'If clsCommon.myLen(txtMCC.Text) > 0 Then
            '    txtPaymentCycleNo.Text = clsGenratePaymentCycles.GetPaymentCycleNo(txtMCC.Text, dtpToDate.Value)
            '    txtFiscalYear.Text = clsGenratePaymentCycles.GetPaymentFiscalCode(txtMCC.Text, dtpToDate.Value)
            'Else
            '    txtPaymentCycleNo.Text = clsGenratePaymentCycles.GetPaymentCycleNo(fndLoc.Value, dtpToDate.Value)
            '    txtFiscalYear.Text = clsGenratePaymentCycles.GetPaymentFiscalCode(fndLoc.Value, dtpToDate.Value)
            'End If
        End If
    End Sub

    Private Sub fromDate_Leave(sender As Object, e As EventArgs) Handles fromDate.Leave
        'If MultipleFinderFillAuto = True Then
        SetToDate()
        'End If
    End Sub

    Private Sub fromDate_Validating(sender As Object, e As CancelEventArgs) Handles fromDate.Validating
        'If MultipleFinderFillAuto = True Then
        SetToDate()
        'End If
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Try
            Print(False, True)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub mfndMcc__My_Click(sender As Object, e As EventArgs) Handles mfndMcc._My_Click
        Try
            Dim whrCls As String = " 1=1 "
            If Not clsMccMaster.isCurrentUserHO() Then
                If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                    whrCls += " and  Location_Code in (" & objCommonVar.strCurrUserLocations & ")  "
                End If
            End If

            whrCls = whrCls & " and   Rejected_Type='N' and Location_Category='MCC'"
            Dim qry As String = " select Location_Code as [Code],Location_Desc as [Description],Loc_Segment_Code as [LocationSegmentCode],Add1,Add2,Add3,Add4,City_Code as [City Code],State,Pin_Code as [Pin Code],Country,Hoadd1,hoadd2,Telphone,Email,Location_Type as [Location Type],Loc_Status as [Location Status],Status_Date as [Status Date],Excisable,Type,Purchase_Tax_Group as [Purchase Tax Group],Sales_Tax_Group as [Sales Tax Group],Ecc_Number as [ECC Number],Registration_Number as [Registration Number],Commissionerate as [Commission Rate],Range_Code as [Range Code],Range_Name as [Range Name],Range_Address as [Range Address],Division_Code as [Division Code],Division_Name as [Division Name],Division_Address as [Division Address],Created_By as [Created By],Created_Date as [Created Date],Modify_By as [Modify By],Modify_Date as [Modify Date],Comp_code as [Company Code],TIN_No as [TIN No],TAN_No as [TAN No],TCAN_No as [TCAN No],Service_Tax_Reg_No as [Service Tax Registration No],DutyPaid as [Duty Paid],Purchase_Tax_GroupIS as [Purchase Tax Group Inter State],Sales_Tax_GroupIS as [Sales Tax Group Inter State],Stock_Transfer_Filled_Ac as [Stock Transfer Filled Account],Stock_Transfer_Empty_Ac as [Stock Transfer Empty Account],GIT_Location as [GIT Location],GIT_Type as [GIT Type],Rejected_Type as [Rejected Type],Rejected_Location as [Rejected Location],CSA_Type as [CSA Type],Cust_Code as [Cust Code],CST_No as [CST No],Phone1,Phone2,stock_transfer_ac as [Stock Tranfer A/C],Loss_Ac as [Loss A/C]  from TSPL_Location_MASTER "
            If clsCommon.myLen(whrCls) > 0 Then
                qry += " where " + whrCls
            End If
            mfndMcc.arrValueMember = clsCommon.ShowMultipleSelectForm("MULMCC@PaymentProcessCYCLEREPOT", qry, "Code", "", mfndMcc.arrValueMember, Nothing)

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Public Sub FillAllMCCDefault()
        Try
            Dim arr As ArrayList = Nothing
            Dim qry As String = ""
            Dim whrCls As String = " 1=1 "
            If Not clsMccMaster.isCurrentUserHO() Then
                If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                    whrCls += " and  Location_Code in (" & objCommonVar.strCurrUserLocations & ")  "
                End If
            End If

            whrCls = whrCls & " and   Rejected_Type='N' and Location_Category='MCC'"
            qry = " select Location_Code as [Code],Location_Desc as [Description],Loc_Segment_Code as [LocationSegmentCode],Add1,Add2,Add3,Add4,City_Code as [City Code],State,Pin_Code as [Pin Code],Country,Hoadd1,hoadd2,Telphone,Email,Location_Type as [Location Type],Loc_Status as [Location Status],Status_Date as [Status Date],Excisable,Type,Purchase_Tax_Group as [Purchase Tax Group],Sales_Tax_Group as [Sales Tax Group],Ecc_Number as [ECC Number],Registration_Number as [Registration Number],Commissionerate as [Commission Rate],Range_Code as [Range Code],Range_Name as [Range Name],Range_Address as [Range Address],Division_Code as [Division Code],Division_Name as [Division Name],Division_Address as [Division Address],Created_By as [Created By],Created_Date as [Created Date],Modify_By as [Modify By],Modify_Date as [Modify Date],Comp_code as [Company Code],TIN_No as [TIN No],TAN_No as [TAN No],TCAN_No as [TCAN No],Service_Tax_Reg_No as [Service Tax Registration No],DutyPaid as [Duty Paid],Purchase_Tax_GroupIS as [Purchase Tax Group Inter State],Sales_Tax_GroupIS as [Sales Tax Group Inter State],Stock_Transfer_Filled_Ac as [Stock Transfer Filled Account],Stock_Transfer_Empty_Ac as [Stock Transfer Empty Account],GIT_Location as [GIT Location],GIT_Type as [GIT Type],Rejected_Type as [Rejected Type],Rejected_Location as [Rejected Location],CSA_Type as [CSA Type],Cust_Code as [Cust Code],CST_No as [CST No],Phone1,Phone2,stock_transfer_ac as [Stock Tranfer A/C],Loss_Ac as [Loss A/C]  from TSPL_Location_MASTER "
            If clsCommon.myLen(whrCls) > 0 Then
                qry += " where " + whrCls
            End If
            Dim dtMCC As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dtMCC IsNot Nothing AndAlso dtMCC.Rows.Count > 0 Then
                arr = New ArrayList()
                For Each dr As DataRow In dtMCC.Rows
                    arr.Add(clsCommon.myCstr(dr("Code")))
                Next
                mfndMcc.arrValueMember = arr
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtDCS__My_Click(sender As Object, e As EventArgs) Handles txtDCS._My_Click
        Try
            Dim qry As String = " select Vendor_Code  as Code, Vendor_Name as Name from TSPL_VENDOR_MASTER  "
            txtDCS.arrValueMember = clsCommon.ShowMultipleSelectForm("PCWR@DCS@MFinder", qry, "Code", "Name", txtDCS.arrValueMember, txtDCS.arrDispalyMember)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtBank__My_Click(sender As Object, e As EventArgs) Handles txtBank._My_Click
        Try
            Dim qry As String = " select BANK_CODE as Code , DESCRIPTION as Name from TSPL_BANK_MASTER  "
            txtBank.arrValueMember = clsCommon.ShowMultipleSelectForm("PCWR@BANK@MFinder", qry, "Code", "Name", txtBank.arrValueMember, txtBank.arrDispalyMember)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtMCC_BMC__My_Click(sender As Object, e As EventArgs) Handles txtMCC_BMC._My_Click
        Try
            Dim qry As String = " select Location_Code as [Code],Location_Desc as Name from TSPL_Location_MASTER where Rejected_Type='N' and Location_Category='MCC' "
            txtMCC_BMC.arrValueMember = clsCommon.ShowMultipleSelectForm("MULMCC@Finder@PaymentCycleReport", qry, "Code", "", txtMCC_BMC.arrValueMember, Nothing)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        ExportGrid(EnumExportTo.Excel)
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        ExportGrid(EnumExportTo.PDF)
    End Sub

    Private Sub ExportGrid(ByVal exporter As EnumExportTo)
        Try
            If gv1.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Export", Me.Text)
                Exit Sub
            End If
            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy")) + " ")
            If txtMCC_BMC.arrDispalyMember IsNot Nothing AndAlso txtMCC_BMC.arrDispalyMember.Count > 0 Then
                arrHeader.Add("MCC/BMC : " + clsCommon.GetMulcallStringWithComma(txtMCC_BMC.arrDispalyMember))
            End If
            If txtRoute.arrDispalyMember IsNot Nothing AndAlso txtRoute.arrDispalyMember.Count > 0 Then
                arrHeader.Add("Route : " + clsCommon.GetMulcallStringWithComma(txtRoute.arrDispalyMember))
            End If
            If txtDCS.arrDispalyMember IsNot Nothing AndAlso txtDCS.arrDispalyMember.Count > 0 Then
                arrHeader.Add("DCS : " + clsCommon.GetMulcallStringWithComma(txtDCS.arrDispalyMember))
            End If
            If txtBank.arrDispalyMember IsNot Nothing AndAlso txtBank.arrDispalyMember.Count > 0 Then
                arrHeader.Add("Bank : " + clsCommon.GetMulcallStringWithComma(txtBank.arrDispalyMember))
            End If
            If exporter = EnumExportTo.Excel Then
                clsCommon.MyExportToExcelGrid(Me.Text, gv1, arrHeader, Me.Text)
            Else
                clsCommon.MyExportToPDF(Me.Text, gv1, arrHeader, Me.Text, Me.Text, objCommonVar.CurrentUserCode)
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, "Error", MessageBoxButtons.OK)
        End Try
    End Sub

End Class
