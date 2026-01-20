'' Developped by Panch Raj on date:15-02-2018
Imports common
Imports System.Threading
Imports Telerik.WinControls.UI.Export
Imports Telerik.WinControls.UI.Export.ExcelML
Imports System.IO
Imports Microsoft.Office.Interop
Public Class rptGheeAndCattleFeedDeductionStatementReport
    Inherits FrmMainTranScreen
#Region "Varaibels"
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim Report_ID As String = ""
#End Region
    Private Sub rptGheeAndCattleFeedDeductionStatementReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        RadPageView1.SelectedPage = RadPageViewPage1
        reset()
        Dim dt As DataTable = clsDBFuncationality.GetDataTable("select Default_Location,TSPL_LOCATION_MASTER.LOCATION_DESC from TSPL_USER_MASTER left outer join TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.LOCATION_CODE= TSPL_USER_MASTER.Default_Location  where User_Code='" + objCommonVar.CurrentUserCode + "'")
        If dt.Rows.Count > 0 Then
            Dim arLoc As ArrayList = New ArrayList()
            Dim arLocName As ArrayList = New ArrayList()
            arLoc.Add(dt.Rows(0)("Default_Location"))
            arLocName.Add(dt.Rows(0)("LOCATION_DESC"))
            txtMCC.arrValueMember = arLoc
            txtMCC.arrDispalyMember = arLocName
        End If
    End Sub
    Sub GetReportID()
        Dim VarID As String = ""
        If rbtnMonthly.IsChecked Then
            VarID += "_MW"
        ElseIf rbtnQuarterly.IsChecked Then
            VarID += "_QW"
        ElseIf rbtnYearly.IsChecked Then
            VarID += "_YW"
        ElseIf rbtnCycle.IsChecked Then
            VarID += "_CY"
        ElseIf rbtnDateWise.IsChecked Then
            VarID += "_DW"
        End If
        gv1.VarID = VarID
        Report_ID = MyBase.Form_ID + "_" + VarID
    End Sub
    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
    End Sub

    Private Sub rptGheeAndCattleFeedDeductionStatementReport_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt And e.KeyCode = Keys.C Then
            Close()
        End If
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnGo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGo.Click
        Try
            GetReportID()
            gv1.EnableFiltering = True
            PageSetupReport_ID = MyBase.Form_ID
            TemplateGridview = gv1
            LoadData(False)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub LoadData(ByVal isPrint As Boolean)
        Try
            If rbtnQuarterly.IsChecked Then
                If txtFromDate.Value.Month <> 1 AndAlso txtFromDate.Value.Month <> 4 AndAlso txtFromDate.Value.Month <> 7 AndAlso txtFromDate.Value.Month <> 10 Then
                    clsCommon.MyMessageBoxShow(Me, "Quarter should be [01-Apr,01-Jul,01-Oct,01-Jan]", Me.Text)
                    Exit Sub
                End If
            ElseIf rbtnYearly.IsChecked Then
                If txtFromDate.Value.Month <> 4 Then
                    txtFromDate.Value = New DateTime(txtFromDate.Value.Year, 4, 1)
                End If
            End If
            If rbtnDateWise.IsChecked = False Then
                txtFromDate.Value = "01/" & DatePart(DateInterval.Month, txtFromDate.Value) & "/" & DatePart(DateInterval.Year, txtFromDate.Value)
            End If
            Dim whrcls As String = " and  trans_type='MCC' and TSPL_SD_SHIPMENT_HEAD.Status=1 and TSPL_DEDUCTION_MASTER.Ded_Grp_Code='DEDUCTION' and convert(date,TSPL_SD_SHIPMENT_HEAD.Document_Date,103) <='" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "' "
            If txtMCC.arrValueMember IsNot Nothing AndAlso txtMCC.arrValueMember.Count > 0 Then
                whrcls += " and TSPL_SD_SHIPMENT_HEAD.Sub_Location_Code in  (" + clsCommon.GetMulcallString(txtMCC.arrValueMember) + ")"
            End If
            If txtDeduction.arrValueMember IsNot Nothing Then
                whrcls += " and TSPL_SD_SHIPMENT_HEAD.Deduction in  (" + clsCommon.GetMulcallString(txtDeduction.arrValueMember) + ")"
            End If
            Dim Location As String = Nothing
            If txtMCC.arrValueMember IsNot Nothing AndAlso txtMCC.arrValueMember.Count > 0 Then
                If txtMCC.arrValueMember.Count > 1 Then
                    Location = "'" + clsDBFuncationality.getSingleValue(" select Location_Code from tspl_location_master where   IsMainPlant=1 ") + "'"
                Else
                    Location = clsCommon.GetMulcallString(txtMCC.arrValueMember)
                End If
            End If
            Dim ReportType As String = ""
            If rbtnMonthly.IsChecked Then
                ReportType = " Month " + clsCommon.myCstr(clsCommon.GetPrintDate(txtFromDate.Value, "MMM-yyyy")) + ""
            ElseIf rbtnQuarterly.IsChecked Then
                ReportType = "Quarter (" + clsCommon.myCstr(clsCommon.GetPrintDate(txtFromDate.Value, "MMM-yyyy")) + " To " + clsCommon.myCstr(clsCommon.GetPrintDate(txtToDate.Value, "MMM-yyyy")) + ")"
            ElseIf rbtnYearly.IsChecked Then
                ReportType = "Year (" + clsCommon.myCstr(clsCommon.GetPrintDate(txtFromDate.Value, "MMM-yyyy")) + " To " + clsCommon.myCstr(clsCommon.GetPrintDate(txtToDate.Value, "MMM-yyyy")) + ")"
            ElseIf rbtnDateWise.IsChecked Then
                ReportType = "Date " + clsCommon.myCstr(clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy")) + " To " + clsCommon.myCstr(clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + ")"
            ElseIf rbtnCycle.IsChecked Then
                ReportType = "Cycle " + clsCommon.myCstr(clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy")) + " To " + clsCommon.myCstr(clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + ")"
            End If
            Dim qry As String = " ,isnull(sum((TSPL_SD_SHIPMENT_HEAD.Total_Amt-isnull(TSPL_PAYMENT_PROCESS_MCC_SALE.Reduce_Deduc_Amt,0))  * (Case When Document_Date < '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' THEN 1.00 ELSE 0 end)),0)  AS [OPBal],
 isnull(sum (TSPL_SD_SHIPMENT_HEAD.Total_Amt * (CASE WHEN Document_Date >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' AND Document_Date <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and TSPL_SD_SHIPMENT_HEAD.Is_CashSale ='N' THEN 1.00 ELSE 0 end) ),0)  AS Credit_Amt,
 isnull(sum (TSPL_SD_SHIPMENT_HEAD.Total_Amt * (CASE WHEN Document_Date >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' AND Document_Date <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and TSPL_SD_SHIPMENT_HEAD.Is_CashSale ='Y' THEN 1.00 ELSE 0 end) ),0)  AS Cash_Amt,
isnull(sum ((isnull(TSPL_PAYMENT_PROCESS_MCC_SALE.Amount,0)-isnull(TSPL_PAYMENT_PROCESS_MCC_SALE.Reduce_Deduc_Amt,0))  * (CASE WHEN Document_Date >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' AND Document_Date <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "'  THEN 1.00 ELSE 0 end) ),0)  AS Amt_Ded
 from TSPL_SD_SHIPMENT_HEAD  left join TSPL_PAYMENT_PROCESS_MCC_SALE on TSPL_PAYMENT_PROCESS_MCC_SALE.Shipment_Doc_No=TSPL_SD_SHIPMENT_HEAD.Document_Code left outer join TSPL_DEDUCTION_MASTER on TSPL_DEDUCTION_MASTER.Code=TSPL_SD_SHIPMENT_HEAD.Deduction where 2=2 " + whrcls + ""
            Dim FinalQry As String = ""
            FinalQry = " select 1 as Grp, max(TSPL_DEDUCTION_MASTER.Description) as Item " & qry & " and IS_GHEE=0  group by TSPL_SD_SHIPMENT_HEAD.Deduction  " & Environment.NewLine & " UNION ALL  " & Environment.NewLine & " select 2 as Grp, 'Total' as Item " & qry & " and IS_GHEE=0  "
            FinalQry += "" & Environment.NewLine & " UNION ALL  " & Environment.NewLine & " select 3 as Grp, max(TSPL_DEDUCTION_MASTER.Description) as Item " & qry & " and IS_GHEE=1 group by TSPL_SD_SHIPMENT_HEAD.Deduction "
            FinalQry += "" & Environment.NewLine & " UNION ALL  " & Environment.NewLine & " select 4 as Grp,Item,sum(OPBal)OPBal,SUM(Credit_Amt)Credit_Amt,SUM(Cash_Amt)Cash_Amt,SUM(Amt_Ded)Amt_Ded FROM (select 'Grand Total' as Item " & qry & " and IS_GHEE=0 group by TSPL_SD_SHIPMENT_HEAD.Deduction "
            FinalQry += "" & Environment.NewLine & " UNION ALL  " & Environment.NewLine & " select 'Grand Total' as Item " & qry & " and IS_GHEE=1 group by TSPL_SD_SHIPMENT_HEAD.Deduction )Total group by Item "
            FinalQry = "  select " + IIf(isPrint, " TSPL_COMPANY_MASTER.Comp_Name,'" + objCommonVar.CurrentUser + "' as UserCode,'" + ReportType + "' as ReportType," + Location + " as Location, ", "") + " ROW_NUMBER() over (order by grp) AS SNo,Item,OPBal,Credit_Amt,Cash_Amt,Credit_Amt+Cash_Amt as Total,Amt_Ded,Cash_Amt as Depo_Amt,(OPBal+Credit_Amt+Cash_Amt)-(Amt_Ded+Cash_Amt) as Closing_Bal from  (  " + FinalQry + " ) xx  left outer join TSPL_COMPANY_MASTER on 1 =1 where OPBal >0 or Credit_Amt >0 or Cash_Amt>0  or Amt_Ded>0  order by grp "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(FinalQry)
            gv1.DataSource = Nothing
            gv1.Rows.Clear()
            gv1.Columns.Clear()
            If dt.Rows.Count > 0 Then
                gv1.DataSource = dt
                gv1.GroupDescriptors.Clear()
                gv1.MasterTemplate.BestFitColumns()
                gv1.EnableFiltering = True
                gv1.AllowAddNewRow = False
                gv1.AllowDeleteRow = False
                RadPageView1.SelectedPage = RadPageViewPage2
                SetGridFormation(isPrint)
                View()
                ReStoreGridLayout()
                EnableDisableCtrl(False)
                If isPrint Then
                    Dim frmCRV As New frmCrystalReportViewer()
                    frmCRV.funreport(MyBase.Form_ID, CrystalReportFolder.MilkProcurement, dt, "rptGheeAndCattleFeedDeductionStatement", "GHEE AND CATTLE FEED DEDUCTION STATEMENT ")
                    frmCRV = Nothing
                End If
            Else
                If isPrint Then
                    clsCommon.MyMessageBoxShow(Me, "No Data found to print", Me.Text)
                Else
                    clsCommon.MyMessageBoxShow(Me, "No Data found to Display", Me.Text)
                End If
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            btnGo.Enabled = True
        End Try
    End Sub
    Sub SetGridFormation(ByVal isPrint As Boolean)
        gv1.TableElement.TableHeaderHeight = 40
        gv1.MasterTemplate.ShowRowHeaderColumn = True
        For ii As Integer = 0 To gv1.Columns.Count - 1
            gv1.Columns(ii).ReadOnly = True
            gv1.Columns(ii).IsVisible = True
            gv1.Columns(ii).Width = 120
            gv1.Columns(ii).FormatString = "{0:n2}"
        Next
        gv1.ShowGroupPanel = False

        gv1.Columns("OPBal").HeaderText = "OB OutStanding To DCS"
        gv1.Columns("Credit_Amt").HeaderText = "CR"
        gv1.Columns("Cash_Amt").HeaderText = "Cash"
        gv1.Columns("Amt_Ded").HeaderText = "Amt.Ded. To DCS"
        gv1.Columns("Depo_Amt").HeaderText = "Depo. At Bank/Office"
        gv1.Columns("Closing_Bal").HeaderText = "C.B. OutStanding To DCS"
        gv1.Columns("SNo").Width = 50
        gv1.Columns("SNo").FormatString = ""
        Dim j As Integer = 0
        If isPrint Then
            gv1.Columns("Comp_Name").IsVisible = False
            gv1.Columns("ReportType").IsVisible = False
            gv1.Columns("UserCode").IsVisible = False
        End If
    End Sub

    Sub View()
        If gv1.Rows.Count > 0 Then
            Dim view As New ColumnGroupsViewDefinition()
            view.ColumnGroups.Add(New GridViewColumnGroup(""))
            view.ColumnGroups(0).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv1.Columns("SNo").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv1.Columns("Item").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv1.Columns("OPBal").Name)

            view.ColumnGroups.Add(New GridViewColumnGroup("SALE"))
            view.ColumnGroups(1).Rows.Add(New GridViewColumnGroupRow())

            view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv1.Columns("Credit_Amt").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv1.Columns("Cash_Amt").Name)

            view.ColumnGroups.Add(New GridViewColumnGroup(""))
            view.ColumnGroups(2).Rows.Add(New GridViewColumnGroupRow())

            view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv1.Columns("Total").Name)
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv1.Columns("Amt_Ded").Name)
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv1.Columns("Depo_Amt").Name)
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv1.Columns("Closing_Bal").Name)
            gv1.ViewDefinition = view
        End If
    End Sub
    Private Sub ReStoreGridLayout()
        Try
            Dim ReportID As String = PageSetupReport_ID
            If clsCommon.myLen(ReportID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(ReportID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gv1.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gv1.Columns.Count - 1 Step ii + 1
                        gv1.Columns(ii).IsVisible = False
                        gv1.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gv1.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            common.clsCommon.MyMessageBoxShow(Me, err.Message, Me.Text)
        End Try
    End Sub
    Private Sub btnExcel_Click(sender As Object, e As EventArgs) Handles btnExcel.Click
        Try
            If gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_code='" & clsUserMgtCode.rptGheeAndCattleFeedDeductionStatementReport & "'"))
                arrHeader.Add("Date : " & clsCommon.myCDate(txtFromDate.Value) + "  To " + clsCommon.myCDate(txtToDate.Value))

                If txtMCC.arrValueMember IsNot Nothing Then
                    arrHeader.Add("Location : " & clsCommon.GetMulcallString(txtMCC.arrValueMember) & "   Location Name :" & clsCommon.GetMulcallString(txtMCC.arrDispalyMember) & "")
                End If

                If txtDeduction.arrValueMember IsNot Nothing Then
                    arrHeader.Add("Deduction : " & clsCommon.GetMulcallString(txtDeduction.arrValueMember) & "   Deduction Name :" & clsCommon.GetMulcallString(txtDeduction.arrDispalyMember) & "")
                End If
                clsCommon.MyExportToExcel(Me.Text, gv1, arrHeader, Me.Text)
                clsCommon.MyMessageBoxShow(Me, "Export Successfully", Me.Text)
            Else
                clsCommon.MyMessageBoxShow(Me, "No data found to export", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnPDF_Click(sender As Object, e As EventArgs) Handles btnPDF.Click
        Try
            If gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Date : " & clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "  To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy"))
                If txtMCC.arrValueMember IsNot Nothing Then
                    arrHeader.Add("Location : " & clsCommon.GetMulcallString(txtMCC.arrValueMember) & "   Location Name :" & clsCommon.GetMulcallString(txtMCC.arrDispalyMember) & "")
                End If

                If txtDeduction.arrValueMember IsNot Nothing Then
                    arrHeader.Add("Deduction : " & clsCommon.GetMulcallString(txtDeduction.arrValueMember) & "   Deduction Name :" & clsCommon.GetMulcallString(txtDeduction.arrDispalyMember) & "")
                End If
                clsCommon.MyExportToPDF(Me.Text, gv1, arrHeader, Me.Text)
            Else
                clsCommon.MyMessageBoxShow(Me, "No data found to export", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub EnableDisableCtrl(ByVal val As Boolean)
        txtFromDate.Enabled = val
        txtToDate.Enabled = val
        RadGroupBox1.Enabled = val
        txtMCC.Enabled = val
        txtDeduction.Enabled = val
    End Sub
    Private Sub rmDeleteLayout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(PageSetupReport_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", Me.Text)
    End Sub

    Private Sub rmsaveLayout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmsaveLayout.Click
        Dim ReportID As String = PageSetupReport_ID
        If clsCommon.myLen(ReportID) > 0 Then
            gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = ReportID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv1.SaveLayout(obj.GridLayout)
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            obj.GridColumns = gv1.ColumnCount
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully", Me.Text)
            End If
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub txtMCC__My_Click(sender As Object, e As EventArgs) Handles txtMCC._My_Click, txtMCC.Click
        Dim qry As String = " select Location_Code as Code , Location_Desc AS Name from tspl_location_master "
        txtMCC.arrValueMember = clsCommon.ShowMultipleSelectForm("MCCFinder", qry, "Code", "Name", txtMCC.arrValueMember, txtMCC.arrDispalyMember)
    End Sub

    Private Sub txtDeduction__My_Click(sender As Object, e As EventArgs) Handles txtDeduction._My_Click, txtDeduction.Click
        Dim qry As String = " Select Code,Description from TSPL_DEDUCTION_MASTER where Ded_Grp_Code='DEDUCTION' "
        txtDeduction.arrValueMember = clsCommon.ShowMultipleSelectForm("Deduction", qry, "Code", "Description", txtDeduction.arrValueMember, txtDeduction.arrDispalyMember)
    End Sub
    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        reset()
    End Sub
    Sub reset()
        EnableDisableCtrl(True)
        gv1.DataSource = Nothing
        rbtnMonthly.IsChecked = True
        txtFromDate.Value = "01/" & DatePart(DateInterval.Month, clsCommon.GETSERVERDATE()) & "/" & DatePart(DateInterval.Year, clsCommon.GETSERVERDATE())
        txtToDate.Value = txtFromDate.Value.AddMonths(1).AddDays(-1)
        txtToDate.Enabled = False
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub
    Private Sub txtFromDate_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtFromDate.Validating
        If rbtnYearly.IsChecked Then
            If txtFromDate.Value.Month <> 4 Then
                txtFromDate.Value = New DateTime(txtFromDate.Value.Year, 4, 1)
            End If
        End If
        If rbtnDateWise.IsChecked = False Then
            txtFromDate.Value = "01/" & DatePart(DateInterval.Month, txtFromDate.Value) & "/" & DatePart(DateInterval.Year, txtFromDate.Value)
        End If
    End Sub

    Private Sub txtFromDate_Leave(sender As Object, e As EventArgs) Handles txtFromDate.Leave
        If rbtnQuarterly.IsChecked Then
            If txtFromDate.Value.Month <> 1 AndAlso txtFromDate.Value.Month <> 4 AndAlso txtFromDate.Value.Month <> 7 AndAlso txtFromDate.Value.Month <> 10 Then
                clsCommon.MyMessageBoxShow(Me, "Quarter should be [01-Apr,01-Jul,01-Oct,01-Jan]", Me.Text)
                Exit Sub
            End If
        ElseIf rbtnYearly.IsChecked Then
            If txtFromDate.Value.Month <> 4 Then
                txtFromDate.Value = New DateTime(txtFromDate.Value.Year, 4, 1)
            End If
        End If
        If rbtnDateWise.IsChecked = False Then
            txtFromDate.Value = "01/" & DatePart(DateInterval.Month, txtFromDate.Value) & "/" & DatePart(DateInterval.Year, txtFromDate.Value)
        End If
        SetToDate()
    End Sub
    Sub SetToDate()
        Try
            If rbtnMonthly.IsChecked Then
                txtToDate.Value = txtFromDate.Value.AddMonths(1).AddDays(-1)
            ElseIf rbtnQuarterly.IsChecked Then
                txtToDate.Value = txtFromDate.Value.AddMonths(3).AddDays(-1)
            ElseIf rbtnYearly.IsChecked Then
                txtToDate.Value = txtFromDate.Value.AddYears(1).AddDays(-1)
            ElseIf rbtnDateWise.IsChecked Then
                txtToDate.Enabled = True
            Else
                Dim PaymentCycleType As String = ""
                Dim PaymentCycleValue As Integer = 0
                If txtMCC.arrValueMember Is Nothing Then
                    Throw New Exception("Please select the Location first")
                    Exit Sub
                End If
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(" select TSPL_MCC_MASTER.Payment_Cycle,TSPL_PAYMENT_CYCLE_MASTER.PC_TYPE,TSPL_PAYMENT_CYCLE_MASTER.PC_VALUE  from TSPL_MCC_MASTER left outer join TSPL_PAYMENT_CYCLE_MASTER on TSPL_PAYMENT_CYCLE_MASTER.PC_CODE=TSPL_MCC_MASTER.Payment_Cycle   where TSPL_MCC_MASTER.MCC_Code =(select Location_Code  from TSPL_LOCATION_MASTER where Loc_Segment_Code in (" & clsCommon.GetMulcallString(txtMCC.arrValueMember) & ") and Location_Category='MCC' and Rejected_Type='N') ")
                If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                    Throw New Exception("No Payment Cycle found on current MCC/Location")
                    Exit Sub
                End If
                PaymentCycleType = clsCommon.myCstr(dt.Rows(0)("PC_TYPE"))
                PaymentCycleValue = clsCommon.myCdbl(dt.Rows(0)("PC_VALUE"))
                Dim dtCurr As DateTime = clsCommon.GETSERVERDATE()
                If clsCommon.CompairString(PaymentCycleType, "Day") = CompairStringResult.Equal Then
                    If txtFromDate.Value.Day Mod PaymentCycleValue <> 1 And (Not PaymentCycleValue = 1) Then
                        clsCommon.MyMessageBoxShow(Me, "Date can only be first day of month or at interval of " & PaymentCycleValue & " Day, Because MCC has payment Cycle of " & PaymentCycleValue & " Day ")
                        txtFromDate.Value = New Date(dtCurr.Year, dtCurr.Month, 1)
                        txtToDate.Value = txtFromDate.Value
                        Exit Sub
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
                        Throw New Exception("Date can only be first day of month, Because MCC has payment Cycle of Month Type")
                        txtFromDate.Value = "01/" & DatePart(DateInterval.Month, dtCurr) & "/" & DatePart(DateInterval.Year, dtCurr)
                        txtToDate.Value = "01/" & DatePart(DateInterval.Month, dtCurr) & "/" & DatePart(DateInterval.Year, dtCurr)
                        Exit Sub
                    End If
                    txtToDate.Value = DateAdd(DateInterval.Month, PaymentCycleValue, txtFromDate.Value)
                ElseIf clsCommon.CompairString(PaymentCycleType, "Year") = CompairStringResult.Equal Then
                    If clsCommon.myCdbl(clsCommon.GetPrintDate(txtFromDate.Value, "dd")) <> 1 Then
                        Throw New Exception("Date can only be first day of month, Because MCC has payment Cycle of Year Type")
                        txtFromDate.Value = "01/" & DatePart(DateInterval.Month, dtCurr) & "/" & DatePart(DateInterval.Year, dtCurr)
                        txtToDate.Value = "01/" & DatePart(DateInterval.Month, dtCurr) & "/" & DatePart(DateInterval.Year, dtCurr)
                        Exit Sub
                    End If
                    txtToDate.Value = DateAdd(DateInterval.Year, PaymentCycleValue, txtFromDate.Value)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        GetReportID()
        LoadData(True)
    End Sub

    Private Sub rbtnDateWise_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles rbtnDateWise.ToggleStateChanged, rbtnMonthly.ToggleStateChanged, rbtnYearly.ToggleStateChanged, rbtnQuarterly.ToggleStateChanged, rbtnCycle.ToggleStateChanged
        If rbtnQuarterly.IsChecked Then
            If txtFromDate.Value.Month <> 1 AndAlso txtFromDate.Value.Month <> 4 AndAlso txtFromDate.Value.Month <> 7 AndAlso txtFromDate.Value.Month <> 10 Then
            Else
                SetToDate()
            End If
            txtToDate.Enabled = False
        ElseIf rbtnYearly.IsChecked Then
            If txtFromDate.Value.Month <> 4 Then
                txtFromDate.Value = New DateTime(txtFromDate.Value.Year, 4, 1)
            End If
            SetToDate()
            txtToDate.Enabled = False
        ElseIf rbtnMonthly.IsChecked Then
                SetToDate()
            ElseIf rbtnDateWise.IsChecked Then
                txtToDate.Enabled = True
                txtToDate.Value = clsCommon.GETSERVERDATE()
            Else
                txtToDate.Enabled = False
        End If
    End Sub
End Class
