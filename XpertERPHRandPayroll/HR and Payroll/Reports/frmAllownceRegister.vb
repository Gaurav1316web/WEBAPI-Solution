'--16/07/2013--form Add By- Pradeep Sharma ---------
'' Anubhooti(2-July-2014) Added Export Permission Against BM00000003016 ''''''''
'' Anubhooti(11-July-2014) Added Export (Clubed)Button BM00000003137 ''''''''

Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports XpertERPEngine
Imports Telerik.WinControls.UI

Public Class frmAllownceRegister
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Const ReportID As String = "AllownceRegister"

#Region "Variable"
    Private isInsideLoadData As Boolean = False
    Dim Qry As String
    Dim DT As DataTable
    Dim DT_Details As DataTable

#End Region
    Sub LoadData()
        Try

            If clsCommon.myLen(txtFromPP.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please fill the From pay period First. ", Me.Text)
                txtFromPP.Focus()
                Exit Sub
            End If
            If clsCommon.myLen(txtTopp.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please fill the To pay period First. ", Me.Text)
                txtTopp.Focus()
                Exit Sub
            End If

            txtFromPP.MyReadOnly = True
            isInsideLoadData = True
            btnGenrate.Enabled = False
            'DT = clsAllowanceDetails.GetRegisterDT(txtFromPP.Value, txtTopp.Value).Copy()
            DT_Details = clsAllowanceDetails.GetRegisterDTDetailed(txtFromPP.Value, txtTopp.Value, fndLocationCode.Value, fndDivisionCode.Value)
            'DT.AcceptChanges()
            'DT_Details.AcceptChanges()
            'SetupMasterForAutoGenerateHierarchy()
            gv1.DataSource = DT_Details
            gv1.MasterTemplate.SummaryRowsBottom.Clear()
            Dim summaryRowItem As New GridViewSummaryRowItem()
            Dim item1 As New GridViewSummaryItem("Allowance Amount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item1)
            gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
            gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
            gv1.MasterTemplate.BestFitColumns()
            btnGenrate.Enabled = True
            ReStoreGridLayout()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            btnGenrate.Enabled = True
        End Try
    End Sub

    Private Sub frmAllownceRegister_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnGenrate, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnNew, "Press Alt+N Adding New ")
    End Sub

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmAllownceRegister)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
        '' Anubhooti(2-July-2014) Added Export Permission Against BM00000003016 ''''''''
        'btnExpoExl.Visible = MyBase.isExport
        'btnExpoPDF.Visible = MyBase.isExport
        btnExport.Visible = MyBase.isExport
        btnGenrate.Visible = MyBase.isModifyFlag
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        funReset()
    End Sub

    Sub funReset()
        txtFromPP.MyReadOnly = False
        txtFromPP.Value = Nothing
        txtFromPP.Focus()
        lblFrompp.Text = ""
        txtTopp.Value = Nothing
        lblTopp.Text = ""
        btnGenrate.Enabled = True
        gv1.DataSource = Nothing
        gv1.Rows.Clear()
        gv1.Columns.Clear()
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        funClose()
    End Sub

    Sub funClose()
        Me.Close()
    End Sub

    Private Sub txtCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtFromPP._MYValidating
        Dim qry As String = "SELECT PAY_PERIOD_CODE AS 'Code',(DATEDIFF(DAY,date_from,date_to)+1) as 'Total days', " _
            & " PAY_PERIOD_NAME as 'Pay Period Name' FROM TSPL_PAYPERIOD_MASTER  "
        txtFromPP.Value = clsCommon.ShowSelectForm("TSPL_PAYPERIOD_MASTER", qry, "Code", "POSTED=1 AND FREEZED=0", txtFromPP.Value, "", isButtonClicked)
        lblFrompp.Text = clsPayPeriodMaster.GetName(txtFromPP.Value, Nothing)
    End Sub

    Private Sub txtCode_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If (e.KeyChar = Chr(39)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub frmAllownceRegister_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt And e.KeyCode = Keys.C Then
            funClose()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            funReset()
        End If
    End Sub

    Private Sub btnGenrate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenrate.Click
        PageSetupReport_ID = ReportID
        TemplateGridview = gv1
        LoadData()
    End Sub
    Private Sub RadMenuItemSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItemSave.Click
        If clsCommon.myLen(PageSetupReport_ID) > 0 Then
            gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = PageSetupReport_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv1.SaveLayout(obj.GridLayout)
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            obj.GridColumns = gv1.ColumnCount
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully", Me.Text)
            End If

            ''richa agarwal regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
            ''---------------
        End If
    End Sub
    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(PageSetupReport_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(PageSetupReport_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
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
        clsGridLayout.DeleteData(PageSetupReport_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", Me.Text)
    End Sub

    Private Sub btnExpoExl_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim arr As New List(Of String)()
        arr.Add(objCommonVar.CurrentCompanyName)
        arr.Add("Allowance Report")
        arr.Add("as on :-" + clsCommon.GETSERVERDATE() + " ")
        'clsCommon.MyExportToExcel("Salary Register", gv1, arr, "Salary Register")
        clsCommon.MyExportToExcelGrid("Allowance Register", gv1, arr, "Allowance Register", False)

    End Sub

    Private Sub btnExpoPDF_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim arr As New List(Of String)()
        arr.Add(objCommonVar.CurrentCompanyName)
        arr.Add("Allowance Report")
        arr.Add("as on :-" + clsCommon.GETSERVERDATE() + " ")
        clsCommon.MyExportToPDF("Allowance Register", gv1, arr, "Allowance Register", False)

    End Sub

#Region "grid operations"

    Private Sub SetupMasterForAutoGenerateHierarchy()
        Using Me.gv1.DeferRefresh()
            Me.gv1.AutoGenerateHierarchy = True
            Me.gv1.MasterTemplate.Reset()
            Me.gv1.TableElement.RowHeight = 20
            Me.gv1.DataSource = DT
            Me.gv1.MasterTemplate.Columns("ALLOWANCE_CODE").HeaderText = "ALLOWANCE CODE"
            Me.gv1.MasterTemplate.Columns("ALLOWANCE_DATE").HeaderText = "ALLOWANCE DATE"
            Me.gv1.MasterTemplate.Columns("ALLOWANCE_DATE").FormatString = "{0:  dd/MMM/yyyy}"
            Me.gv1.MasterTemplate.Columns("ALLOWANCE_AMOUNT").HeaderText = "ALLOWANCE AMOUNT"
            Me.gv1.MasterTemplate.Columns("ALLOWANCE_REMARKS").HeaderText = "REMARKS"
            Me.gv1.MasterTemplate.Columns("PAY_PERIOD_CODE").HeaderText = "PAY PERIOD CODE"
            Me.gv1.MasterTemplate.Columns("PAY_PERIOD_NAME").HeaderText = "PAY PERIOD NAME"
            Me.gv1.MasterTemplate.Columns("EMP_CODE").HeaderText = "EMPLOYEE CODE"
            Me.gv1.MasterTemplate.Columns("Emp_Name").HeaderText = "EMPLOYEE NAME"
            Me.gv1.MasterTemplate.Columns("ALLOWANCE_BY").HeaderText = "ALLOWANCE BY CODE"
            Me.gv1.MasterTemplate.Columns("ALLOWANCE_BY_NAME").HeaderText = "ALLOWANCE BY NAME"
            Me.gv1.MasterTemplate.Columns("POSTED").HeaderText = "IS APPROVED"
            Me.gv1.MasterTemplate.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill

            Dim template As New GridViewTemplate()
            template.DataSource = DT_Details
            Me.gv1.Templates.Add(template)
            template.AllowAddNewRow = False
            template.Columns("ALLOWANCE_CODE").HeaderText = "ALLOWANCE CODE"
            template.Columns("PAY_HEAD_CODE").HeaderText = "PAY HEAD CODE"
            template.Columns("PAY_HEAD_NAME").HeaderText = "PAY HEAD NAME"
            template.Columns("EMP_CODE").HeaderText = "EMPLOYEE CODE"
            template.Columns("Emp_Name").HeaderText = "EMPLOYEE NAME"
            template.Columns("ALLOWANCE_AMOUNT").HeaderText = "ALLOWANCE AMOUNT"
            template.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill
            template.ReadOnly = True

            Dim relation As New GridViewRelation(gv1.MasterTemplate, template)
            relation.RelationName = "ALLOWANCE_PRO"
            relation.ParentColumnNames.Add("ALLOWANCE_CODE")
            relation.ChildColumnNames.Add("ALLOWANCE_CODE")
            Me.gv1.Relations.Add(relation)

            'gv1.EnableCustomFiltering = True
            'gv1.EnableCustomGrouping = True
            'gv1.EnableCustomSorting = True
            'gv1.EnableFiltering = True
            'gv1.EnableGrouping = True
            'gv1.EnableSorting = True
            'gv1.ShowFilteringRow = True
            'gv1.EnableFiltering = True
        End Using
    End Sub

#End Region

    Private Sub txtTopp__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtTopp._MYValidating
        Dim qry As String = "SELECT PAY_PERIOD_CODE AS 'Code',(DATEDIFF(DAY,date_from,date_to)+1) as 'Total days', " _
            & " PAY_PERIOD_NAME as 'Pay Period Name' FROM TSPL_PAYPERIOD_MASTER  "
        txtTopp.Value = clsCommon.ShowSelectForm("TSPL_PAYPERIOD_MASTER", qry, "Code", "POSTED=1 AND FREEZED=0", txtTopp.Value, "", isButtonClicked)
        lblTopp.Text = clsPayPeriodMaster.GetName(txtTopp.Value, Nothing)
    End Sub

    Private Sub btnExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcel.Click
        'Dim arr As New List(Of String)()
        'arr.Add(objCommonVar.CurrentCompanyName)
        'arr.Add("Allowance Report")
        'arr.Add("as on :-" + clsCommon.GETSERVERDATE() + " ")
        ''clsCommon.MyExportToExcel("Salary Register", gv1, arr, "Salary Register")
        'If gv1.Rows.Count <= 0 Then
        '    gv1.Focus()
        '    clsCommon.MyMessageBoxShow("Data not found.")
        'Else
        '    clsCommon.MyExportToExcelGrid("Allowance Register", gv1, arr, "Allowance Register", False)
        'End If
        ExportGrid(EnumExportTo.Excel)
    End Sub

    Private Sub btnPDF_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPDF.Click
        'Dim arr As New List(Of String)()
        'arr.Add(objCommonVar.CurrentCompanyName)
        'arr.Add("Allowance Report")
        'arr.Add("as on :-" + clsCommon.GETSERVERDATE() + " ")
        'clsCommon.MyExportToPDF("Allowance Register", gv1, arr, "Allowance Register", False)
        ExportGrid(EnumExportTo.PDF)
    End Sub
    Private Sub fndLocationCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndLocationCode._MYValidating
        fndLocationCode.Value = clsLocation.getFinder("Location_Type='Physical'", Me.fndLocationCode.Value, isButtonClicked)
        'fndLocationCode.Value = clsLocation.getFinder("Location_Type='Physical' and IsMainPlant='1' and Loc_Segment_Code in (" + objCommonVar.strCurrUserLocationsSegment + ")", Me.fndLocationCode.Value, isButtonClicked)
        If clsCommon.myLen(fndLocationCode.Value) > 0 Then
            lblLocationName.Text = clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" & fndLocationCode.Value & "'")
        Else
            lblLocationName.Text = ""
        End If
    End Sub

    Private Sub fndDivisionCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndDivisionCode._MYValidating
        Dim qry As String = "select DEVISION_CODE as Code, DEVISION_NAME as Name, DESCRIPTION as Description from TSPL_DEVISION_MASTER"
        fndDivisionCode.Value = clsCommon.ShowSelectForm("DEVISION_MASTER", qry, "Code", "", fndDivisionCode.Value, "DEVISION_CODE", isButtonClicked)
        If clsCommon.myLen(fndDivisionCode.Value) > 0 Then
            lblDivisionName.Text = clsDBFuncationality.getSingleValue("select DEVISION_NAME from TSPL_DEVISION_MASTER where DEVISION_CODE='" & fndDivisionCode.Value & "'")
        Else
            lblDivisionName.Text = ""
        End If
    End Sub

    Private Sub ExportGrid(ByVal exporter As EnumExportTo)
        Try
            If gv1.Rows.Count > 0 Then
                ' Tcket No:BM00000007602  by Panch Raj
                Dim arrHeader As List(Of String) = New List(Of String)()

                'arrHeader.Add("Date :" + clsCommon.GETSERVERDATE() + " ")
                arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.frmAllownceRegister & "'"))
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add(("From Pay Period: " + txtFromPP.Value) + " To Pay Period " + txtTopp.Value)


                If fndLocationCode.Value IsNot Nothing AndAlso fndLocationCode.Value.Count > 0 Then
                    arrHeader.Add("Location : " + lblLocationName.Text)
                End If
                If fndDivisionCode.Value IsNot Nothing AndAlso fndDivisionCode.Value.Count > 0 Then
                    arrHeader.Add("Division : " + lblDivisionName.Text)
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
                    transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
                    transportSql.QuickExportToExcel(gv1, "", Me.Text, , arrHeader)
                    'transportSql.exportdataChilRows(gv1, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
                    'common.clsCommon.MyMessageBoxShow("Exported Successfully.")
                    'Process.Start(filePath)
                Else
                    transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
                    clsCommon.MyExportToPDF("Allowance Register", gv1, arrHeader, "Allowance Register", PageSetupReport_ID, objCommonVar.CurrentUserCode)
                End If
            Else
                common.clsCommon.MyMessageBoxShow(Me, "No Data Found to Export.", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class
