Imports common
Imports System.Data
Imports System.Data.SqlClient
Public Class frmMakeTempleteImportMP
    Inherits FrmMainTranScreen
#Region "Variable"
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim userCode, companyCode, FormType As String
    Dim strCatStrPass As String = ""
    Dim seqNo As GridViewDecimalColumn
    Dim Column As GridViewTextBoxColumn
    'Public gvReport As RadGridView
    'Public dtReportType As DataTable
    Public Report_Type As String
    Public arrColsOrginal As Dictionary(Of String, Boolean)
    Public dtColsExcel As DataTable
    Public IsDefaultValue As Boolean = False

    Private isNewEntry As Boolean = False
    Private isInsideLoadData As Boolean = False
    Dim Qry As String

    Const colSeqNo As String = "colSeqNo"
    Const colOrginal As String = "colOrginal"
    Const colMandatory As String = "Mandatory"
    Const colExcel As String = "colExcel"


    Dim Errorcontrol As clsErrorControl = New clsErrorControl()
#End Region

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        Save()
    End Sub
    Public Sub New(ByVal user As String, ByVal company As String, ByVal form_type As String)
        InitializeComponent()
        userCode = user
        companyCode = company
        FormType = form_type
    End Sub

    Private Sub frmExportTemplate_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadBlankGrid()
        isNewEntry = True
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D  for Delete ")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnNew, "Press Alt+N Adding New ")
        funReset()
        loadDefault()
    End Sub

    Sub funReset()
        isNewEntry = True
        txtCode.MyReadOnly = False
        txtName.ReadOnly = False
        txtCode.Value = Nothing
        txtCode.Focus()
        txtName.Text = ""
        LoadBlankGrid()
        txtProgram.Value = FormType
        fndUser.Value = objCommonVar.CurrentUserCode
        lblUserName.Text = clsUserMaster.GetName(fndUser.Value, Nothing)
        btnsave.Text = "Save"
        btnsave.Enabled = True
        btndelete.Enabled = False
    End Sub

    Sub LoadBlankGrid()
        gvColumnsMain.DataSource = Nothing
        gvColumnsMain.Rows.Clear()
        gvColumnsMain.Columns.Clear()

        seqNo = New GridViewDecimalColumn()
        seqNo.FormatString = ""
        seqNo.HeaderText = "SNo"
        seqNo.Name = colSeqNo
        seqNo.Width = 60
        seqNo.ReadOnly = True
        seqNo.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvColumnsMain.Columns.Add(seqNo)

        Column = New GridViewTextBoxColumn()
        Column.FormatString = ""
        Column.HeaderText = "Original Columns"
        Column.Name = colOrginal
        Column.Width = 250
        Column.ReadOnly = True
        Column.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvColumnsMain.MasterTemplate.Columns.Add(Column)


        Dim repoCheckBox As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoCheckBox.HeaderText = "Mandatory"
        repoCheckBox.Name = colMandatory
        repoCheckBox.ReadOnly = True
        repoCheckBox.IsVisible = True
        repoCheckBox.Width = 100
        repoCheckBox.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gvColumnsMain.MasterTemplate.Columns.Add(repoCheckBox)

        If IsDefaultValue Then
            Column = New GridViewTextBoxColumn()
            Column.FormatString = ""
            Column.HeaderText = "Default Value"
            Column.Name = colExcel
            Column.Width = 500
            Column.ReadOnly = False
            Column.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
            gvColumnsMain.MasterTemplate.Columns.Add(Column)
        Else
            Dim repoRowType As GridViewComboBoxColumn = New GridViewComboBoxColumn()
            repoRowType.FormatString = ""
            repoRowType.HeaderText = "Excel Columns"
            repoRowType.Name = colExcel
            repoRowType.Width = 250
            repoRowType.ReadOnly = False
            repoRowType.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
            repoRowType.DataSource = dtColsExcel
            repoRowType.ValueMember = "Code"
            repoRowType.DisplayMember = "Code"
            gvColumnsMain.MasterTemplate.Columns.Add(repoRowType)
        End If


        gvColumnsMain.ShowFilteringRow = True
        gvColumnsMain.EnableFiltering = True

    End Sub

    Function AllowToSave() As Boolean
        If clsCommon.myLen(txtName.Text) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Enter Template Name", Me.Text)
            txtName.Focus()
            txtName.Select()
            Errorcontrol.SetError(txtName, "Template Name")
            Return False
        Else
            Errorcontrol.ResetError(txtName)
        End If
        For ii As Integer = 0 To gvColumnsMain.Rows.Count - 1
            If clsCommon.myCBool(gvColumnsMain.Rows(ii).Cells(colMandatory).Value) Then
                If clsCommon.myLen(gvColumnsMain.Rows(ii).Cells(colExcel).Value) <= 0 Then
                    clsCommon.MyMessageBoxShow(Me, "Please Define Excel Column of " + clsCommon.myCstr(gvColumnsMain.Rows(ii).Cells(colOrginal).Value) + " At Row No " + clsCommon.myCstr(ii + 1), Me.Text)
                    Return False
                End If
            End If
            If Not IsDefaultValue Then
                If clsCommon.myLen(gvColumnsMain.Rows(ii).Cells(colExcel).Value) > 0 Then
                    For jj As Integer = ii + 1 To gvColumnsMain.Rows.Count - 1
                        If clsCommon.myLen(gvColumnsMain.Rows(jj).Cells(colExcel).Value) > 0 Then
                            If clsCommon.CompairString(clsCommon.myCstr(gvColumnsMain.Rows(ii).Cells(colExcel).Value), clsCommon.myCstr(gvColumnsMain.Rows(jj).Cells(colExcel).Value)) = CompairStringResult.Equal Then
                                clsCommon.MyMessageBoxShow(Me, "Excel Column " + clsCommon.myCstr(gvColumnsMain.Rows(ii).Cells(colExcel).Value) + " Repeated At Row No " + clsCommon.myCstr(ii + 1) + " and " + clsCommon.myCstr(jj + 1), Me.Text)
                                Return False
                            End If
                        End If
                    Next
                End If
            End If
        Next
        RefershLineNo()
        Return True
    End Function

    Public Sub Save()
        Try
            If AllowToSave() Then
                Dim obj As New clsExportTemplate()
                obj.Export_Code = txtCode.Value
                obj.Template_Name = Me.txtName.Text
                obj.Program_Code = txtProgram.Value
                obj.Report_Type = "" 'ddlReportType.SelectedValue
                obj.Is_Default_Value = IsDefaultValue
                obj.Arr = New List(Of clsExportTemplateDetail)

                For Each grow As GridViewRowInfo In gvColumnsMain.Rows
                    Dim objTr As New clsExportTemplateDetail()
                    objTr.Seq_No = clsCommon.myCdbl(grow.Cells(colSeqNo).Value)
                    objTr.Export_Code = clsCommon.myCstr(Me.txtCode.Value)
                    objTr.Column_Name = clsCommon.myCstr(grow.Cells(colOrginal).Value)
                    objTr.Column_Mandatory = clsCommon.myCBool(grow.Cells(colMandatory).Value)
                    objTr.Column_Header = clsCommon.myCstr(grow.Cells(colExcel).Value)
                    obj.Arr.Add(objTr)
                Next
                clsExportTemplate.SaveData(obj, isNewEntry)
                LoadData(obj.Export_Code, NavigatorType.Current)
                clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        txtCode.MyReadOnly = True
        btnsave.Enabled = True
        btndelete.Enabled = False
        Dim obj As New clsExportTemplate()
        obj = clsExportTemplate.GetData(strCode, txtProgram.Value, "", NavTyep) ' ddlReportType.SelectedValue
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Export_Code) > 0) Then
            funReset()
            isNewEntry = False
            btnsave.Text = "Update"
            btndelete.Enabled = True
            txtCode.Value = obj.Export_Code
            txtName.Text = obj.Template_Name
            txtCode.MyReadOnly = True
            LoadBlankGrid()
            Dim ii As Int16 = 0
            If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                For Each objTr As clsExportTemplateDetail In obj.Arr
                    gvColumnsMain.Rows.AddNew()
                    ii = ii + 1
                    gvColumnsMain.Rows(gvColumnsMain.Rows.Count - 1).Cells(colSeqNo).Value = objTr.Seq_No
                    gvColumnsMain.Rows(gvColumnsMain.Rows.Count - 1).Cells(colOrginal).Value = objTr.Column_Name
                    gvColumnsMain.Rows(gvColumnsMain.Rows.Count - 1).Cells(colMandatory).Value = objTr.Column_Mandatory
                    gvColumnsMain.Rows(gvColumnsMain.Rows.Count - 1).Cells(colExcel).Value = objTr.Column_Header
                Next
            End If
            If arrColsOrginal.Count <> obj.Arr.Count Then
                For Each key As String In arrColsOrginal.Keys
                    Dim flag As Boolean = False
                    For Each objTr As clsExportTemplateDetail In obj.Arr
                        If clsCommon.CompairString(objTr.Column_Name, key) = CompairStringResult.Equal Then
                            flag = True
                            Exit For
                        End If
                    Next
                    If Not flag Then
                        gvColumnsMain.Rows.AddNew()
                        gvColumnsMain.Rows(gvColumnsMain.Rows.Count - 1).Cells(colSeqNo).Value = gvColumnsMain.Rows.Count
                        gvColumnsMain.Rows(gvColumnsMain.Rows.Count - 1).Cells(colOrginal).Value = key
                        gvColumnsMain.Rows(gvColumnsMain.Rows.Count - 1).Cells(colMandatory).Value = arrColsOrginal(key)
                    End If
                Next

            End If
        End If
    End Sub



    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click
        DeleteData()
    End Sub

    Sub DeleteData()
        If clsCommon.myLen(txtCode.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "You Cannot Delete Record", Me.Text)
            Exit Sub
        End If
        funDelete()
    End Sub

    Sub funDelete()
        Try
            If (myMessages.deleteConfirm()) Then
                If (clsExportTemplate.DeleteData(txtCode.Value)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ", Me.Text)
                    funReset()
                End If
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try

    End Sub

    Private Sub txtCode_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCode.KeyPress
        If (e.KeyChar = Chr(39)) Then
            e.Handled = True
        End If
    End Sub



    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        funReset()
    End Sub


    Sub loadDefault()
        Try
            Dim flag As Boolean = False
            If IsDefaultValue Then
                Dim qry As String = "Select Max(Export_Code) As Export_Code from TSPL_EXPORT_TEMPLATE_HEAD where Program_Code='" & FormType & "' and Report_Type='" & Report_Type & "' and Is_Default_Value=1"
                Dim code As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
                If clsCommon.myLen(code) > 0 Then
                    LoadData(code, Nothing)
                Else
                    flag = True
                End If
            ElseIf clsCommon.myLen(strDocNoForOpen) > 0 Then
                Dim qry As String = "Select Max(Export_Code) As Export_Code from TSPL_EXPORT_TEMPLATE_HEAD where Program_Code='" & FormType & "' and Report_Type='" & Report_Type & "' and Is_Default_Value=0 and Created_By='" & objCommonVar.CurrentUserCode & "' and Export_Code='" + strDocNoForOpen + "'"
                Dim code As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
                If clsCommon.myLen(code) > 0 Then
                    LoadData(code, Nothing)
                End If
            Else
                flag = True
            End If
            If flag Then
                For Each key As String In arrColsOrginal.Keys
                    gvColumnsMain.Rows.AddNew()
                    gvColumnsMain.Rows(gvColumnsMain.Rows.Count - 1).Cells(colSeqNo).Value = gvColumnsMain.Rows.Count
                    gvColumnsMain.Rows(gvColumnsMain.Rows.Count - 1).Cells(colOrginal).Value = key
                    gvColumnsMain.Rows(gvColumnsMain.Rows.Count - 1).Cells(colMandatory).Value = arrColsOrginal(key)
                Next
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        funClose()
    End Sub

    Sub funClose()
        clsERPFuncationality.closeForm(Me)
    End Sub

    Private Sub txtCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtCode._MYValidating
        Try
            Dim str As String = "select count(*) from TSPL_EXPORT_TEMPLATE_HEAD where Export_Code ='" + txtCode.Value + "' and Is_Default_Value=" + clsCommon.myCstr(IIf(IsDefaultValue, "1", "0")) + " "
            Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
            If no = 0 AndAlso isButtonClicked = False Then
                txtCode.MyReadOnly = False
            Else
                txtCode.MyReadOnly = True
            End If
            If txtCode.MyReadOnly OrElse isButtonClicked Then
                txtCode.Value = clsExportTemplate.GetFinder(" TSPL_EXPORT_TEMPLATE_HEAD.Program_Code='" + txtProgram.Value + "' " & If(clsCommon.myLen(Report_Type) > 0, " and TSPL_EXPORT_TEMPLATE_HEAD.Report_Type='" & Report_Type & "' and TSPL_EXPORT_TEMPLATE_HEAD.Created_By='" & objCommonVar.CurrentUserCode & "' ", " and TSPL_EXPORT_TEMPLATE_HEAD.Created_By='" & objCommonVar.CurrentUserCode & "'") & "", txtCode.Value, isButtonClicked)
                txtName.Text = clsExportTemplate.GetName(txtCode.Value, Nothing)
                LoadData(txtCode.Value, NavigatorType.Current)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message)
        End Try

    End Sub

    Private Sub txtCode__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtCode._MYNavigator
        Try
            LoadData(txtCode.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnBrowse_Click(sender As Object, e As EventArgs) Handles btnBrowse.Click
        Try
            transportSql.exportdata(gvColumnsMain, "", Me.Text, False, Nothing, False, False, False)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadButton1_Click(sender As Object, e As EventArgs) Handles RadButton1.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Try
            If transportSql.importExcel(gv, gvColumnsMain.Columns(colSeqNo).HeaderText, gvColumnsMain.Columns(colOrginal).HeaderText, gvColumnsMain.Columns(colMandatory).HeaderText, gvColumnsMain.Columns(colExcel).HeaderText) Then
                Dim linno As Integer = 1
                Try
                    For ii As Integer = 0 To gvColumnsMain.Rows.Count - 1
                        For Each grow As GridViewRowInfo In gv.Rows
                            If clsCommon.CompairString(clsCommon.myCstr(grow.Cells(gvColumnsMain.Columns(colOrginal).HeaderText).Value), clsCommon.myCstr(gvColumnsMain.Rows(ii).Cells(colOrginal).Value)) = CompairStringResult.Equal Then
                                gvColumnsMain.Rows(ii).Cells(colExcel).Value = grow.Cells(gvColumnsMain.Columns(colExcel).HeaderText).Value
                            End If
                        Next
                    Next
                    common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
                Catch ex As Exception
                    myMessages.myExceptions(ex)
                End Try
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        Finally
            Me.Controls.Remove(gv)
        End Try
    End Sub

    Private Sub frmExportTemplate_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnNew.Enabled Then
            funReset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnsave.Enabled Then
            Save()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btndelete.Enabled Then
            DeleteData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            funClose()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            funReset()
        End If
    End Sub

    Private Sub RefershLineNo()
        For Each grow As GridViewRowInfo In gvColumnsMain.Rows
            grow.Cells(colSeqNo).Value = grow.Index + 1
        Next
    End Sub


End Class
