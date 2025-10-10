'' Developped by Panch Raj on date:15-02-2018
Imports common
Imports System.IO
Imports Microsoft.Office.Interop
Public Class rptPlantWiseAvailableStockQtyReport
    Inherits FrmMainTranScreen
#Region "Varaibels"
    Dim ButtonToolTip As ToolTip = New ToolTip()
#End Region
    Private Sub rptPlantWiseAvailableStockQtyReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
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
            txtLocation.arrValueMember = arLoc
            txtLocation.arrDispalyMember = arLocName
        End If
    End Sub
    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
    End Sub

    Private Sub rptPlantWiseAvailableStockQtyReport_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt And e.KeyCode = Keys.C Then
            Close()
        End If
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnGo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGo.Click
        Try
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
            If txtDate.Value.Month <> 1 AndAlso txtDate.Value.Month <> 4 AndAlso txtDate.Value.Month <> 7 AndAlso txtDate.Value.Month <> 10 Then
                clsCommon.MyMessageBoxShow(Me, "Quarter should be [01-Apr,01-Jul,01-Oct,01-Jan]", Me.Text)
                Exit Sub
            End If

            Dim whrcls As String = " and  trans_type='MCC' and TSPL_SD_SHIPMENT_HEAD.Status=1 and TSPL_DEDUCTION_MASTER.Ded_Grp_Code='DEDUCTION' and convert(date,TSPL_SD_SHIPMENT_HEAD.Document_Date,103) <='" + clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") + "' "
            If txtLocation.arrValueMember.Count > 0 AndAlso txtLocation.arrValueMember IsNot Nothing Then
                whrcls += " and TSPL_SD_SHIPMENT_HEAD.Sub_Location_Code in  (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ")"
            End If
            If txtItem.arrValueMember IsNot Nothing Then
                whrcls += " and TSPL_SD_SHIPMENT_HEAD.Deduction in  (" + clsCommon.GetMulcallString(txtItem.arrValueMember) + ")"
            End If
            Dim Location As String = Nothing
            If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                If txtLocation.arrValueMember.Count > 1 Then
                    Location = "'" + clsDBFuncationality.getSingleValue(" select Location_Code from tspl_location_master where   IsMainPlant=1 ") + "'"
                Else
                    Location = clsCommon.GetMulcallString(txtLocation.arrValueMember)
                End If
            End If
            Dim ReportType As String = ""

            ReportType = "Cycle " + clsCommon.myCstr(clsCommon.GetPrintDate(txtDate.Value, "dd/MM/yyyy")) + " To " + clsCommon.myCstr(clsCommon.GetPrintDate(txtDate.Value, "dd/MM/yyyy")) + ")"
            Dim qry As String = " ,isnull(sum((TSPL_SD_SHIPMENT_HEAD.Total_Amt-isnull(TSPL_PAYMENT_PROCESS_MCC_SALE.Reduce_Deduc_Amt,0))  * (Case When Document_Date < '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtDate.Value), "dd/MMM/yyyy hh:mm tt") + "' THEN 1.00 ELSE 0 end)),0)  AS [OPBal],
 isnull(sum (TSPL_SD_SHIPMENT_HEAD.Total_Amt * (CASE WHEN Document_Date >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtDate.Value), "dd/MMM/yyyy hh:mm tt") + "' AND Document_Date <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and TSPL_SD_SHIPMENT_HEAD.Is_CashSale ='N' THEN 1.00 ELSE 0 end) ),0)  AS Credit_Amt,
 isnull(sum (TSPL_SD_SHIPMENT_HEAD.Total_Amt * (CASE WHEN Document_Date >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtDate.Value), "dd/MMM/yyyy hh:mm tt") + "' AND Document_Date <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and TSPL_SD_SHIPMENT_HEAD.Is_CashSale ='Y' THEN 1.00 ELSE 0 end) ),0)  AS Cash_Amt,
isnull(sum ((isnull(TSPL_PAYMENT_PROCESS_MCC_SALE.Amount,0)-isnull(TSPL_PAYMENT_PROCESS_MCC_SALE.Reduce_Deduc_Amt,0))  * (CASE WHEN Document_Date >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtDate.Value), "dd/MMM/yyyy hh:mm tt") + "' AND Document_Date <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtDate.Value), "dd/MMM/yyyy hh:mm tt") + "'  THEN 1.00 ELSE 0 end) ),0)  AS Amt_Ded
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
                arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_code='" & clsUserMgtCode.rptPlantWiseAvailableStockQtyReport & "'"))
                arrHeader.Add("Date : " & clsCommon.myCDate(txtDate.Value))

                If txtLocation.arrValueMember IsNot Nothing Then
                    arrHeader.Add("Location : " & clsCommon.GetMulcallString(txtLocation.arrValueMember) & "   Location Name :" & clsCommon.GetMulcallString(txtLocation.arrDispalyMember) & "")
                End If

                If txtItem.arrValueMember IsNot Nothing Then
                    arrHeader.Add("Deduction : " & clsCommon.GetMulcallString(txtItem.arrValueMember) & "   Deduction Name :" & clsCommon.GetMulcallString(txtItem.arrDispalyMember) & "")
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
                arrHeader.Add("Date : " & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy"))
                If txtLocation.arrValueMember IsNot Nothing Then
                    arrHeader.Add("Location : " & clsCommon.GetMulcallString(txtLocation.arrValueMember) & "   Location Name :" & clsCommon.GetMulcallString(txtLocation.arrDispalyMember) & "")
                End If

                If txtItem.arrValueMember IsNot Nothing Then
                    arrHeader.Add("Deduction : " & clsCommon.GetMulcallString(txtItem.arrValueMember) & "   Deduction Name :" & clsCommon.GetMulcallString(txtItem.arrDispalyMember) & "")
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
        txtDate.Enabled = val
        RadGroupBox1.Enabled = val
        txtLocation.Enabled = val
        txtItem.Enabled = val
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

    Private Sub txtLocation__My_Click(sender As Object, e As EventArgs) Handles txtLocation.Click
        Dim qry As String = " select Location_Code as Code , Location_Desc AS Name from tspl_location_master "
        txtLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("LocFinder", qry, "Code", "Name", txtLocation.arrValueMember, txtLocation.arrDispalyMember)
    End Sub

    Private Sub txtItem__My_Click(sender As Object, e As EventArgs) Handles txtItem._My_Click
        Dim qry As String = " SELECT Item_Code AS Code,Item_Desc as [Item Description],Short_Description as [Short Description] FROM TSPL_ITEM_MASTER WHERE ITEM_TYPE = 'R' "
        txtItem.arrValueMember = clsCommon.ShowMultipleSelectForm("ItemFnd", qry, "Code", "Short Description", txtItem.arrValueMember, txtItem.arrDispalyMember)
    End Sub
    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        reset()
    End Sub
    Sub reset()
        EnableDisableCtrl(True)
        gv1.DataSource = Nothing
        txtDate.Value = clsCommon.GETSERVERDATE()
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub
    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        LoadData(True)
    End Sub
End Class
