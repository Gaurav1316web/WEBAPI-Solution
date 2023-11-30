Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports System.IO
Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports common

Public Class RptVLCVehicleWeigmentRegister
    Inherits FrmMainTranScreen
    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        RadSplitButton1.Visible = MyBase.isExport
    End Sub

    Private Sub RptVLCVehicleWeigmentRegister_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        Reset()
    End Sub
    Sub Reset()
        txtFromDate.Value = clsCommon.GETSERVERDATE()
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtRoute.arrValueMember = Nothing
        txtVehicle.arrValueMember = Nothing
        gv.Columns.Clear()
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        PageSetupReport_ID = MyBase.Form_ID
        TemplateGridview = gv
        LoadData()
    End Sub
    Sub LoadData()
        Try
            If clsCommon.GetDateWithStartTime(txtFromDate.Value) > clsCommon.GetDateWithEndTime(txtToDate.Value) Then
                txtFromDate.Focus()
                Throw New Exception("From date can not be greater then to Date")
            End If
            gv.MasterTemplate.SummaryRowsBottom.Clear()
            Dim qry As String = ""
            Dim fromdate As String = ""
            Dim Todate As String = ""
            Dim whr As String = ""

            fromdate = clsCommon.myCDate(txtFromDate.Value, "dd/MM/yyyy")
            Todate = clsCommon.myCDate(txtToDate.Value, "dd/MM/yyyy")
            If txtRoute.arrValueMember IsNot Nothing AndAlso txtRoute.arrValueMember.Count > 0 Then
                whr = " and TSPL_MILK_GATE_ENTRY_IN.Route_Code in ( " + clsCommon.GetMulcallString(txtRoute.arrValueMember) + ")"
            End If

            If txtVehicle.arrValueMember IsNot Nothing AndAlso txtVehicle.arrValueMember.Count > 0 Then
                whr = whr + " and TSPL_MILK_GATE_ENTRY_IN.Vehicle_No in ( " + clsCommon.GetMulcallString(txtVehicle.arrValueMember) + ")"
            End If

            qry = " select TSPL_MILK_GATE_ENTRY_IN.Entry_Code,convert (varchar, FORMAT (TSPL_MILK_GATE_ENTRY_IN.Entry_Date,'dd/MM/yyyy HH:mm:ss' )) as Entry_Date_Time, TSPL_MILK_GATE_ENTRY_IN.Route_Code,TSPL_MCC_ROUTE_MASTER.Route_Name,TSPL_MILK_GATE_ENTRY_IN.Vehicle_No, TSPL_MILK_GATE_ENTRY_IN.Cans_Filled, TSPL_MILK_GATE_ENTRY_IN.Cans_Empty,TSPL_MILK_GATE_ENTRY_WEIGHTMENT_Temp.Weighment_Code, convert (varchar, FORMAT (TSPL_MILK_GATE_ENTRY_WEIGHTMENT_Temp.GW_Weighment_Date,'dd/MM/yyyy HH:mm:ss' )) as GW_Weighment_Date,convert (varchar, FORMAT (TSPL_MILK_GATE_ENTRY_WEIGHTMENT_Temp.TW_Weighment_Date,'dd/MM/yyyy HH:mm:ss' )) as TW_Weighment_Date,TSPL_MILK_GATE_ENTRY_WEIGHTMENT_Temp.Gross_Weight,TSPL_MILK_GATE_ENTRY_WEIGHTMENT_Temp.Tare_Weight,TSPL_MILK_GATE_ENTRY_WEIGHTMENT_Temp.Net_Weight from TSPL_MILK_GATE_ENTRY_IN " & _
                  " left outer join ( select * from TSPL_MILK_GATE_ENTRY_WEIGHTMENT where TSPL_MILK_GATE_ENTRY_WEIGHTMENT.GW_Status =1 and  TSPL_MILK_GATE_ENTRY_WEIGHTMENT.TW_Status =1) as TSPL_MILK_GATE_ENTRY_WEIGHTMENT_Temp on TSPL_MILK_GATE_ENTRY_WEIGHTMENT_Temp.Against_Gate_Entry_No= TSPL_MILK_GATE_ENTRY_IN.Entry_Code " & _
                  " left outer join  TSPL_MCC_ROUTE_MASTER  on TSPL_MCC_ROUTE_MASTER.Route_Code=TSPL_MILK_GATE_ENTRY_IN.Route_Code " & _
                  " where   TSPL_MILK_GATE_ENTRY_IN.Status=1 and Convert (date,TSPL_MILK_GATE_ENTRY_IN.Entry_Date,103) > = convert (date, '" + fromdate + "',103) " & _
                  " and Convert(date, TSPL_MILK_GATE_ENTRY_IN.Entry_Date,103) < = convert (date, '" + Todate + "',103)  " + whr + " "

            Dim dtgv As New DataTable
            dtgv = clsDBFuncationality.GetDataTable(qry)
            If dtgv IsNot Nothing And dtgv.Rows.Count > 0 Then
                gv.DataSource = Nothing
                gv.Rows.Clear()
                gv.Columns.Clear()
                gv.DataSource = dtgv
                gv.BestFitColumns()
                RadPageView1.SelectedPage = RadPageViewPage2
            End If
            If dtgv.Rows.Count <= 0 Then
                gv.DataSource = Nothing
                gv.Rows.Clear()
                gv.Columns.Clear()
                clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)
                Exit Sub
            End If
            ReStoreGridLayout()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadMenuItem1_Click(sender As Object, e As EventArgs) Handles RadMenuItem1.Click
        print(EnumExportTo.Excel)
    End Sub

    Sub print(ByVal exporter As EnumExportTo)
        Try
            If gv.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.RptVLCVehicleWeigmentRegister & "'"))

                If txtRoute.arrValueMember IsNot Nothing AndAlso txtRoute.arrValueMember.Count > 0 Then
                    arrHeader.Add("Route :" + clsCommon.GetMulcallStringWithComma(txtRoute.arrDispalyMember))
                End If

                If txtVehicle.arrValueMember IsNot Nothing AndAlso txtVehicle.arrValueMember.Count > 0 Then
                    arrHeader.Add("Vehicle :" + clsCommon.GetMulcallStringWithComma(txtVehicle.arrDispalyMember))
                End If

                If exporter = EnumExportTo.Excel Then
                    'Dim sfd As SaveFileDialog = New SaveFileDialog()
                    'Dim filePath As String
                    'sfd.FileName = Me.Text
                    'sfd.Filter = "Excel 97-2003 (*.xls) |*.xls;|Excel 2007 (*.xlsx)|*.xlsx;|CSV Files (*.csv) |*.csv"
                    'If sfd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                    '    filePath = sfd.FileName
                    'Else
                    '    Exit Sub
                    'End If
                    transportSql.applyExportTemplate(gv, PageSetupReport_ID)
                    transportSql.QuickExportToExcel(gv, "", Me.Text, , arrHeader)
                    'transportSql.exportdataChilRows(gv, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
                    'common.clsCommon.MyMessageBoxShow("Exported Successfully.")
                    'Process.Start(filePath)
                Else
                    transportSql.applyExportTemplate(gv, PageSetupReport_ID)
                    clsCommon.MyExportToPDF("VLC Vehicle Weigment Register Report", gv, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
                End If
            Else
                common.clsCommon.MyMessageBoxShow(Me, "No Data Found to Export.", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub
    Private Sub txtRoute__My_Click(sender As Object, e As EventArgs) Handles txtRoute._My_Click
        Dim qry As String = " select Route_Code as Code,Route_Name as Name from TSPL_MCC_ROUTE_MASTER "
        txtRoute.arrValueMember = clsCommon.ShowMultipleSelectForm("ROUTE", qry, "Code", "Name", txtRoute.arrValueMember, txtRoute.arrDispalyMember)
    End Sub
    Private Sub txtVehicle__My_Click(sender As Object, e As EventArgs) Handles txtVehicle._My_Click
        Dim qry As String = " select Number as Code,  Description as Name  from TSPL_VEHICLE_MASTER  "
        txtVehicle.arrValueMember = clsCommon.ShowMultipleSelectForm("VEHICLE", qry, "Code", "Name", txtVehicle.arrValueMember, txtVehicle.arrDispalyMember)
    End Sub

    Private Sub BtnReset_Click(sender As Object, e As EventArgs) Handles BtnReset.Click
        Reset()
    End Sub

    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub
    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(MyBase.Form_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(MyBase.Form_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
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
    Private Sub rmSaveLayout_Click(sender As Object, e As EventArgs) Handles rmSaveLayout.Click
        If clsCommon.myLen(MyBase.Form_ID) > 0 Then
            gv.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = MyBase.Form_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv.SaveLayout(obj.GridLayout)
            obj.GridColumns = gv.ColumnCount
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
            End If
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(MyBase.Form_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow("Layout Delete successfully", "Information")
    End Sub

    Private Sub RadMenuItem3_Click(sender As Object, e As EventArgs) Handles RadMenuItem3.Click
        print(EnumExportTo.PDF)
    End Sub
End Class
