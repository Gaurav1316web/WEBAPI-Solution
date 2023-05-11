Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports System.IO
Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports common

Public Class rptMCCRouteTimeTable
    Inherits FrmMainTranScreen
    Dim qry As String
    Dim ds As New DataSet()
    Dim ButtonToolTip As ToolTip = New ToolTip()

    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
    End Sub
    Private Sub rptMCCRouteTimeTable_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            SetUserMgmtNew()
            Reset()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        PageSetupReport_ID = MyBase.Form_ID
        TemplateGridview = gv
        LoadData()
    End Sub
    Sub Reset()
        txtFromDate.Value = clsCommon.GETSERVERDATE()
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtRoute.arrValueMember = Nothing
        txtMCC.arrValueMember = Nothing
        txtTransporter.arrValueMember = Nothing
        txtVehicleNo.arrValueMember = Nothing
        LoadShiftFrom()
        LoadShiftTo()
        gv.Columns.Clear()
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

    Sub LoadShiftFrom()
        Dim dt As DataTable = New DataTable
        dt.Columns.Add("Code")
        dt.Columns.Add("Shift")

        Dim dr As DataRow = dt.NewRow
        dr("Code") = "M"
        dr("Shift") = "Morning"
        dt.Rows.Add(dr)

        dr = dt.NewRow
        dr("Code") = "E"
        dr("Shift") = "Evening"
        dt.Rows.Add(dr)

        txtFromShift.DataSource = dt
        txtFromShift.ValueMember = "Code"
    End Sub

    Sub LoadShiftTo()
        Dim dt As DataTable = New DataTable
        dt.Columns.Add("Code")
        dt.Columns.Add("Shift")

        Dim dr As DataRow = dt.NewRow
        dr("Code") = "M"
        dr("Shift") = "Morning"
        dt.Rows.Add(dr)

        dr = dt.NewRow
        dr("Code") = "E"
        dr("Shift") = "Evening"
        dt.Rows.Add(dr)

        txtToShift.DataSource = dt
        txtToShift.ValueMember = "Code"
    End Sub


    Public Sub SetGrdProperties()
        gv.AllowAddNewRow = False
        gv.AllowDeleteRow = False
        gv.ShowGroupPanel = False
        gv.ReadOnly = False
        gv.BestFitColumns()
    End Sub
    Private Sub Set_GridView_Format()
        Try

            gv.TableElement.TableHeaderHeight = 45
            gv.MasterTemplate.ShowRowHeaderColumn = True
            For Each col As GridViewColumn In gv.Columns
                col.ReadOnly = True
            Next

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub


    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Reset()
    End Sub
    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub
    Sub LoadData()
        Try
            Dim qry As String = ""
            Dim dt As DataTable = Nothing
            'Sanjay Comment BHA/26/06/18-000085 Change in Report
            'qry = "select TSPL_MCC_ROUTE_MASTER.Route_Code as [Route Code],convert(varchar(12),TSPL_MILK_GATE_ENTRY_IN.Entry_Date,103) as [Date],TSPL_MILK_GATE_ENTRY_IN.Entry_Shift as [Shift],case when TSPL_MILK_GATE_ENTRY_IN.Entry_Shift = 'M' then convert(varchar(20),TSPL_MCC_ROUTE_MASTER.MCC_Reaching_Time_M,100) else convert(varchar(20),TSPL_MCC_ROUTE_MASTER.MCC_Reaching_Time_E,100) end as [Time Table Time],convert(varchar,TSPL_MILK_GATE_ENTRY_IN.Entry_Date,8) as [Gate Entry Time],convert(varchar,TSPL_MILK_GATE_ENTRY_WEIGHTMENT.GW_Weighment_Date,8) as [Dumping Time],convert(varchar,TSPL_MILK_GATE_ENTRY_OUT.Gate_Out_Date,8) as [Gate Out Time]"
            'qry += " from TSPL_MCC_ROUTE_MASTER"
            'qry += " left outer join TSPL_MILK_GATE_ENTRY_IN on TSPL_MILK_GATE_ENTRY_IN.Route_Code=tspl_mcc_route_master.route_code"
            'qry += " left outer join TSPL_MILK_GATE_ENTRY_WEIGHTMENT on TSPL_MILK_GATE_ENTRY_WEIGHTMENT.Against_Gate_Entry_No=TSPL_MILK_GATE_ENTRY_IN.Entry_Code"
            'qry += " left outer join TSPL_MILK_GATE_ENTRY_OUT on TSPL_MILK_GATE_ENTRY_OUT.Against_Gate_Entry_No= TSPL_MILK_GATE_ENTRY_in.Entry_Code"
            'qry += " where 2=2"
            'If txtRoute.arrValueMember IsNot Nothing AndAlso txtRoute.arrValueMember.Count > 0 Then
            '    qry += " and TSPL_MCC_ROUTE_MASTER.Route_Code in (" + clsCommon.GetMulcallString(txtRoute.arrValueMember) + ") " + Environment.NewLine
            'End If
            'qry += " and convert(date,TSPL_MILK_GATE_ENTRY_IN.Entry_Date,103)>=convert(date,'" & clsCommon.myCDate(txtFromDate.Value, "dd/MM/yyyy") & "',103) and convert(date,TSPL_MILK_GATE_ENTRY_IN.Entry_Date,103)<=convert(date,'" & clsCommon.myCDate(txtToDate.Value, "dd/MM/yyyy") & "',103)"

            qry = "select ROW_NUMBER() OVER( ORDER BY TSPL_MILK_GATE_ENTRY_IN.Entry_Date ASC) AS [S No], convert(varchar(12),TSPL_MILK_GATE_ENTRY_IN.Entry_Date,103) as [Date] "
            qry += " ,Case When TSPL_MILK_GATE_ENTRY_IN.Entry_Shift = 'M' Then 'Morning' Else 'Evening' End As [Shift] "
            qry += " ,TSPL_MILK_GATE_ENTRY_IN.Vehicle_No as [Vehicle No],tspl_vendor_master.vendor_name as [Transporter Name] "
            qry += " ,TSPL_MILK_GATE_ENTRY_IN.Entry_Code as [Gate Entry No] ,TSPL_MILK_GATE_ENTRY_IN.Cans_Filled as [Filled Can In] "
            qry += " ,TSPL_MILK_GATE_ENTRY_IN.Cans_Empty as [Empty Can In] "
            qry += " ,case when TSPL_MILK_GATE_ENTRY_IN.Entry_Shift = 'M' then CONVERT(varchar(15),CAST(TSPL_MCC_ROUTE_MASTER.MCC_Reaching_Time_M AS TIME),100) "
            qry += " else CONVERT(varchar(15),CAST(TSPL_MCC_ROUTE_MASTER.MCC_Reaching_Time_E AS TIME),100) end as [Reaching Time] "
            qry += " ,CONVERT(varchar(15),CAST(TSPL_MILK_GATE_ENTRY_IN.Entry_Date AS TIME),100) as [Reached Time] "
            qry += " ,TSPL_MILK_GATE_ENTRY_IN.Penalty_Amount as [Calculated Deduction Amount] "
            qry += " ,(case when isnull(TSPL_MILK_GATE_ENTRY_IN.Penalty_Status,0)=1 then TSPL_MILK_GATE_ENTRY_IN.Penalty_Amount else 0 end) as [Applied Deduction Amount] "
            qry += " ,TSPL_MILK_GATE_ENTRY_OUT.Gate_Out_Code as [Gate Out No] "
            qry += " ,TSPL_MILK_GATE_ENTRY_OUT.Cans_Filled as [Filled Can Out] "
            qry += " ,TSPL_MILK_GATE_ENTRY_OUT.Cans_Empty as [Empty Can Out] "
            qry += " from  TSPL_MILK_GATE_ENTRY_IN left outer join TSPL_MCC_ROUTE_MASTER on TSPL_MILK_GATE_ENTRY_IN.Route_Code=tspl_mcc_route_master.route_code "
            qry += " left outer join TSPL_MILK_GATE_ENTRY_OUT on TSPL_MILK_GATE_ENTRY_OUT.Against_Gate_Entry_No= TSPL_MILK_GATE_ENTRY_in.Entry_Code "
            qry += " left outer join TSPL_Primary_Vehicle_Master on TSPL_Primary_Vehicle_Master.Vehicle_Code=TSPL_MILK_GATE_ENTRY_IN.Vehicle_No "
            qry += " left outer join tspl_vendor_master on tspl_vendor_master.vendor_code=TSPL_Primary_Vehicle_Master.vendor_code and tspl_vendor_master.form_type='PTM' "
            qry += " where 2=2"

            If txtMCC.arrValueMember IsNot Nothing AndAlso txtMCC.arrValueMember.Count > 0 Then
                qry += "and TSPL_MILK_GATE_ENTRY_IN.MCC_CODE  IN (" + clsCommon.GetMulcallString(txtMCC.arrValueMember) + ") " + Environment.NewLine
            End If
            If txtRoute.arrValueMember IsNot Nothing AndAlso txtRoute.arrValueMember.Count > 0 Then
                qry += " and TSPL_MILK_GATE_ENTRY_IN.Route_Code in (" + clsCommon.GetMulcallString(txtRoute.arrValueMember) + ")  " + Environment.NewLine
            End If
            If txtVehicleNo.arrValueMember IsNot Nothing AndAlso txtVehicleNo.arrValueMember.Count > 0 Then
                qry += "  and TSPL_MILK_GATE_ENTRY_IN.Vehicle_No in (" + clsCommon.GetMulcallString(txtVehicleNo.arrValueMember) + ") " + Environment.NewLine
            End If
            If txtTransporter.arrValueMember IsNot Nothing AndAlso txtTransporter.arrValueMember.Count > 0 Then
                qry += " and TSPL_MILK_GATE_ENTRY_IN.Transporter_Code in ( " + clsCommon.GetMulcallString(txtTransporter.arrValueMember) + ") " + Environment.NewLine
            End If

            qry += " and Cast(TSPL_MILK_GATE_ENTRY_IN.Entry_Date as Date) >='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy") + "' and Cast(TSPL_MILK_GATE_ENTRY_IN.Entry_Date as Date) <='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy") + "'"
            If clsCommon.CompairString(txtFromShift.Text, "E") = CompairStringResult.Equal Then
                qry += " and 2=( case when Cast(TSPL_MILK_GATE_ENTRY_IN.Entry_Date as Date) >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy") + "' and Cast(TSPL_MILK_GATE_ENTRY_IN.Entry_Date as Date) <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtFromDate.Value), "dd/MMM/yyyy") + "' and TSPL_MILK_GATE_ENTRY_IN.Entry_Shift='M' then 3 else 2 end  )"
            End If
            If clsCommon.CompairString(txtToShift.Text, "M") = CompairStringResult.Equal Then
                qry += " and 2=( case when Cast(TSPL_MILK_GATE_ENTRY_IN.Entry_Date as Date) >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtToDate.Value), "dd/MMM/yyyy") + "' and Cast(TSPL_MILK_GATE_ENTRY_IN.Entry_Date as Date) <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy") + "' and TSPL_MILK_GATE_ENTRY_IN.Entry_Shift='E' then 3 else 2 end  )"
            End If

            qry += " order by Cast(TSPL_MILK_GATE_ENTRY_IN.Entry_Date as Date),TSPL_MILK_GATE_ENTRY_IN.Entry_Code "

            dt = clsDBFuncationality.GetDataTable(qry)
            gv.DataSource = Nothing
            gv.Columns.Clear()
            gv.Rows.Clear()
            gv.GroupDescriptors.Clear()
            gv.MasterTemplate.SummaryRowsBottom.Clear()
            gv.EnableFiltering = True

            If dt Is Nothing OrElse dt.Rows.Count > 0 Then
                gv.DataSource = dt
                gv.BestFitColumns()
                SetGrdProperties()
                Set_GridView_Format()
                ReStoreGridLayout()
                RadPageView1.SelectedPage = RadPageViewPage2
            Else
                common.clsCommon.MyMessageBoxShow("No Data Found to Display", Me.Text)
                Exit Sub
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Private Sub txtRoute__My_Click(sender As Object, e As EventArgs) Handles txtRoute._My_Click
        Dim qry As String = " SELECT  DISTINCT Route_Code as Code,Route_Name as Name FROM TSPL_MCC_ROUTE_MASTER "

        txtRoute.arrValueMember = clsCommon.ShowMultipleSelectForm("RouteMulSel", qry, "Code", "Name", txtRoute.arrValueMember, txtRoute.arrDispalyMember)
    End Sub
    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(MyBase.Form_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(Form_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gv.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gv.Columns.Count - 1 Step ii + 1
                        gv.Columns(ii).IsVisible = False
                        gv.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gv.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub

    Private Sub txtMCC__My_Click(sender As Object, e As EventArgs) Handles txtMCC._My_Click
        Dim qry As String = "select MCC_Code,MCC_NAME from TSPL_MCC_MASTER"
        txtMCC.arrValueMember = clsCommon.ShowMultipleSelectForm("PCUTMCC", qry, "MCC_Code", "MCC_NAME", txtMCC.arrValueMember, txtMCC.arrDispalyMember)
    End Sub

    Private Sub txtTransporter__My_Click(sender As Object, e As EventArgs) Handles txtTransporter._My_Click
        Try
            Dim qry As String = "select Vendor_Code as [Transporter Code],Vendor_Name as [Transporter Name] from TSPL_VENDOR_MASTER  where  2=2 and form_type='PTM' "
            txtTransporter.arrValueMember = clsCommon.ShowMultipleSelectForm("RWTT_VeCode", qry, "Transporter Code", "Transporter Name", txtTransporter.arrValueMember, txtTransporter.arrDispalyMember)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub txtVehicleNo__My_Click(sender As Object, e As EventArgs) Handles txtVehicleNo._My_Click
        Try
            Dim qry As String = "select TSPL_Primary_Vehicle_Master.vehicle_code as Code,TSPL_Primary_Vehicle_Master.Description from TSPL_Primary_Vehicle_Master left outer join tspl_vendor_master on tspl_vendor_master.vendor_code=TSPL_Primary_Vehicle_Master.vendor_code and tspl_vendor_master.form_type='PTM' "
            txtVehicleNo.arrValueMember = clsCommon.ShowMultipleSelectForm("RWT_VehiCode", qry, "Code", "Description", txtVehicleNo.arrValueMember, txtVehicleNo.arrDispalyMember)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub


    Private Sub rmSaveLayout_Click(sender As Object, e As EventArgs) Handles rmSaveLayout.Click
        Try
            If clsCommon.myLen(PageSetupReport_ID) > 0 Then
                gv.MasterTemplate.FilterDescriptors.Clear()
                Dim obj As New clsGridLayout()
                obj.ReportID = PageSetupReport_ID
                obj.UserID = objCommonVar.CurrentUserCode
                obj.GridLayout = New MemoryStream()
                gv.SaveLayout(obj.GridLayout)
                obj.GridColumns = gv.ColumnCount
                obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                If obj.SaveData() Then
                    common.clsCommon.MyMessageBoxShow("Layout Saved Successfully", Me.Text)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        Try
            clsGridLayout.DeleteData(PageSetupReport_ID, objCommonVar.CurrentUserCode)
            common.clsCommon.MyMessageBoxShow("Layout Delete successfully", Me.Text)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        Print(EnumExportTo.Excel)
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        Print(EnumExportTo.PDF)
    End Sub
    Sub print(ByVal exporter As EnumExportTo)
        Try
            If gv.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Company : " + objCommonVar.CurrentCompanyName)
                arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptMCCRouteTimeTable & "'"))
                arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")


                If txtMCC.arrDispalyMember IsNot Nothing AndAlso txtMCC.arrDispalyMember.Count > 0 Then
                    arrHeader.Add("MCC : " + clsCommon.GetMulcallStringWithComma(txtMCC.arrDispalyMember))
                End If
                If txtRoute.arrDispalyMember IsNot Nothing AndAlso txtRoute.arrDispalyMember.Count > 0 Then
                    arrHeader.Add("Route : " + clsCommon.GetMulcallStringWithComma(txtRoute.arrDispalyMember))
                End If
                If txtTransporter.arrDispalyMember IsNot Nothing AndAlso txtTransporter.arrDispalyMember.Count > 0 Then
                    arrHeader.Add("Transporter : " + clsCommon.GetMulcallStringWithComma(txtTransporter.arrDispalyMember))
                End If
                If txtVehicleNo.arrDispalyMember IsNot Nothing AndAlso txtVehicleNo.arrDispalyMember.Count > 0 Then
                    arrHeader.Add("Vehicle No : " + clsCommon.GetMulcallStringWithComma(txtVehicleNo.arrValueMember))
                End If
                If exporter = EnumExportTo.Excel Then
                    transportSql.applyExportTemplate(gv, PageSetupReport_ID)
                    clsCommon.MyExportToExcelGrid("Milk Route Time Report ", gv, arrHeader, Me.Text)
                Else
                    transportSql.applyExportTemplate(gv, PageSetupReport_ID)
                    clsCommon.MyExportToPDF("Milk Route Time Report ", gv, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
                End If
            Else
                common.clsCommon.MyMessageBoxShow("No Data Found to Export.", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub
End Class
