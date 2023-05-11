'--29/07/2013--form Add By- Pradeep Sharma ---------
'' Anubhooti(3-July-2014) Added Export Permission Against BM00000003016 ''''''''
'' Anubhooti(11-July-2014) Added Export (Clubed)Button BM00000003137 ''''''''

Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports XpertERPEngine
Imports Telerik.WinControls.UI

Public Class frmVoucherPaymentsRegister
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Const ReportID As String = "PaymentVoucherRegister"
#Region "Variable"
    Private isInsideLoadData As Boolean = False
    Dim Qry As String
    Dim DT As DataTable
    Dim DT_Details As DataTable
#End Region
    Sub LoadData()
        Try
            If clsCommon.myLen(txtCode.Value) > 0 Then


                gv1.DataSource = Nothing
                gv1.GroupDescriptors.Clear()
                gv1.MasterTemplate.SummaryRowsBottom.Clear()
                gv1.Rows.Clear()
                gv1.Columns.Clear()
                DT = clsSalaryGeneration.GetVoucherPaymentReportData(txtCode.Value)

                Using Me.gv1.DeferRefresh()
                    Me.gv1.DataSource = DT
                    Dim summaryRowItem As New GridViewSummaryRowItem()
                    gv1.BestFitColumns()
                    For Each dr As DataColumn In DT.Columns
                        If clsCommon.CompairString(dr.ColumnName, "EMP_CODE") = CompairStringResult.Equal Then
                            gv1.Columns("EMP_CODE").IsVisible = True
                            gv1.Columns("EMP_CODE").Width = 150
                            gv1.Columns("EMP_CODE").HeaderText = "Employee Id"
                        ElseIf clsCommon.CompairString(dr.ColumnName, "EMPLOYEE_NAME") = CompairStringResult.Equal Then
                            gv1.Columns("EMPLOYEE_NAME").IsVisible = True
                            gv1.Columns("EMPLOYEE_NAME").Width = 200
                            gv1.Columns("EMPLOYEE_NAME").HeaderText = "Name"
                        ElseIf clsCommon.CompairString(dr.ColumnName, "DESIGNATION") = CompairStringResult.Equal Then
                            gv1.Columns("DESIGNATION").IsVisible = True
                            gv1.Columns("DESIGNATION").Width = 150
                            gv1.Columns("DESIGNATION").HeaderText = "Designation "
                        ElseIf clsCommon.CompairString(dr.ColumnName, "DEPARTMENT") = CompairStringResult.Equal Then
                            gv1.Columns("DEPARTMENT").IsVisible = True
                            gv1.Columns("DEPARTMENT").Width = 150
                            gv1.Columns("DEPARTMENT").HeaderText = "DEPARTMENT"
                        Else
                            gv1.Columns(dr.ColumnName).IsVisible = True
                            gv1.Columns(dr.ColumnName).Width = 100
                            Dim item1 As New GridViewSummaryItem(dr.ColumnName, "{0:F2}", GridAggregateFunction.Sum)
                            summaryRowItem.Add(item1)
                        End If
                    Next
                    gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
                    ReStoreGridLayout()
                End Using
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub frmVoucherPaymentsRegister_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()

    End Sub

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmVoucherPaymentsRegister)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        ' Preeti Gupta Added Export Permission  ''''''''

        btnExport.Visible = MyBase.isExport
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        funClose()
    End Sub

    Sub funClose()
        Me.Close()
    End Sub
    Private Sub frmVoucherPaymentsRegister_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt And e.KeyCode = Keys.C Then
            funClose()
        End If
    End Sub

    Private Sub RadMenuItemSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItemSave.Click
        If clsCommon.myLen(ReportID) > 0 Then
            gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = ReportID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv1.SaveLayout(obj.GridLayout)
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            obj.GridColumns = gv1.ColumnCount
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
            End If

            ''richa agarwal regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
            ''---------------
        End If
    End Sub
    Private Sub ReStoreGridLayout()
        Try
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
            MessageBox.Show(err.Message)
        End Try
    End Sub

    Private Sub RadMenuItemDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItemDelete.Click
        clsGridLayout.DeleteData(ReportID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow("Layout delete successfully", "Information")
    End Sub

    Private Sub btnExpoExl_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim arr As New List(Of String)()
        arr.Add(objCommonVar.CurrentCompanyName)
        arr.Add("Employee Voucher Payment Register ")
        arr.Add("as on :-" + clsCommon.GETSERVERDATE() + " ")
        'clsCommon.MyExportToExcel("Employee Voucher Payment ", gv1, arr, "Employee Voucher Payment")
        clsCommon.MyExportToExcelGrid("Employee Voucher Payment", gv1, arr, "Employee Voucher Payment", False)
    End Sub

    Private Sub btnExpoPDF_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim arr As New List(Of String)()
        arr.Add(objCommonVar.CurrentCompanyName)
        arr.Add("Employee Voucher Payment")
        arr.Add("as on :-" + clsCommon.GETSERVERDATE() + " ")
        clsCommon.MyExportToPDF("Employee Voucher Payment", gv1, arr, "Employee Register", False)
    End Sub

    Private Sub txtCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtCode._MYValidating
        Dim qry As String = "SELECT PAY_PERIOD_CODE AS 'Code',(DATEDIFF(DAY,date_from,date_to)+1) as 'Total days', " _
            & " PAY_PERIOD_NAME as 'Pay Period Name' FROM TSPL_PAYPERIOD_MASTER  "
        txtCode.Value = clsCommon.ShowSelectForm("TSPL_PAYPERIOD_MASTER", qry, "Code", "POSTED=1 AND FREEZED=0", txtCode.Value, "", isButtonClicked)
        lblPayPeriodName.Text = clsPayPeriodMaster.GetName(txtCode.Value, Nothing)
    End Sub
    Private Sub btnGenrate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenrate.Click
        PageSetupReport_ID = ReportID
        TemplateGridview = gv1
        LoadData()
    End Sub

    Private Sub btnExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcel.Click
        'Dim arr As New List(Of String)()
        'arr.Add(objCommonVar.CurrentCompanyName)
        'arr.Add("Employee Voucher Payment Register ")
        'arr.Add("as on :-" + clsCommon.GETSERVERDATE() + " ")
        ''clsCommon.MyExportToExcel("Employee Voucher Payment ", gv1, arr, "Employee Voucher Payment")
        'If gv1.Rows.Count <= 0 Then
        '    gv1.Focus()
        '    clsCommon.MyMessageBoxShow("Data not found.")
        'Else
        '    clsCommon.MyExportToExcelGrid("Employee Voucher Payment", gv1, arr, "Employee Voucher Payment", False)
        'End If
        ExportGrid(EnumExportTo.Excel)
    End Sub

    Private Sub btnPDF_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPDF.Click
        'Dim arr As New List(Of String)()
        'arr.Add(objCommonVar.CurrentCompanyName)
        'arr.Add("Employee Voucher Payment")
        'arr.Add("as on :-" + clsCommon.GETSERVERDATE() + " ")
        'clsCommon.MyExportToPDF("Employee Voucher Payment", gv1, arr, "Employee Register", False)
        ExportGrid(EnumExportTo.PDF)
    End Sub
    '==========Added by Preeti GUpta============
    Private Sub ExportGrid(ByVal exporter As EnumExportTo)
        Try
            If gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.frmVoucherPaymentsRegister & "'"))
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                'arrHeader.Add("Date :" + clsCommon.GETSERVERDATE() + " ")
                arrHeader.Add("Pay Period: " + lblPayPeriodName.Text)
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
                    transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
                    transportSql.QuickExportToExcel(gv1, "", Me.Text, , arrHeader)
                    'transportSql.exportdataChilRows(gv1, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
                    'common.clsCommon.MyMessageBoxShow("Exported Successfully.")
                    'Process.Start(filePath)
                Else
                    transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
                    clsCommon.MyExportToPDF("Employee Voucher Payment", gv1, arrHeader, "Employee Register", PageSetupReport_ID, objCommonVar.CurrentUserCode)
                End If
            Else
                common.clsCommon.MyMessageBoxShow("No Data Found to Export.", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
End Class
