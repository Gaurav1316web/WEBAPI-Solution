
Imports common
Imports System.Data
Imports System.Data.SqlClient
Public Class frmTemplateExpImp
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim userCode, companyCode, FormType As String
    Dim strCatStrPass As String = ""
    Dim seqNo As GridViewDecimalColumn
    Dim Column As GridViewTextBoxColumn
    'Public gvReport As RadGridView
    Public ListImpExpColumns1 As List(Of String)
    Public ListImpExpColumnsMandatory1 As List(Of String)
    Public dtReportType As DataTable
    Public Report_Type As String

#Region "Variable"
    Private isNewEntry As Boolean = False
    Private isInsideLoadData As Boolean = False
    Dim Qry As String
    Const colColumnName As String = "colColumnName"
    Const colColumnHeader As String = "colColumnHeader"
    Const colSeqNo As String = "colSeqNo"
    Const colIsMandatory As String = "colColumnIsMandatory"
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
    Public Sub Save()
        Try
            If AllowToSave() Then
                Dim obj As New clsTemplateExpImp()
                obj.Export_Code = txtCode.Value
                obj.Template_Name = Me.txtName.Text
                obj.Program_Code = txtProgram.Value
                'obj.Report_Type = "" 'ddlReportType.SelectedValue
                obj.Arr = New List(Of clsTemplateExpImpDetail)

                For Each grow As GridViewRowInfo In gvColumnTemp.Rows
                    Dim objTr As New clsTemplateExpImpDetail()
                    objTr.Export_Code = clsCommon.myCstr(Me.txtCode.Value)
                    objTr.Column_Name = clsCommon.myCstr(grow.Cells(colColumnName).Value)
                    objTr.Column_Header = clsCommon.myCstr(grow.Cells(colColumnHeader).Value)
                    objTr.Seq_No = clsCommon.myCdbl(grow.Cells(colSeqNo).Value)
                    objTr.IsMandatory = clsCommon.myCdbl(grow.Cells(colIsMandatory).Value)
                    obj.Arr.Add(objTr)
                Next
                clsTemplateExpImp.SaveData(obj, isNewEntry)
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
        Dim obj As New clsTemplateExpImp()
        obj = clsTemplateExpImp.GetData(strCode, txtProgram.Value, NavTyep) ' ddlReportType.SelectedValue

        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Export_Code) > 0) Then
            funReset()
            isNewEntry = False
            btnsave.Text = "Update"
            btndelete.Enabled = True
            txtCode.Value = obj.Export_Code
            txtName.Text = obj.Template_Name
            'ddlReportType.SelectedValue = obj.Report_Type
            txtCode.MyReadOnly = True
            LoadMainColumns()
            Dim ii As Int16 = 0
            If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                LoadGridColumns(gvColumnTemp, True)
                For Each objTr As clsTemplateExpImpDetail In obj.Arr
                    gvColumnTemp.Rows.AddNew()
                    ii = ii + 1
                    gvColumnTemp.Rows(gvColumnTemp.Rows.Count - 1).Cells(colColumnName).Value = objTr.Column_Name
                    gvColumnTemp.Rows(gvColumnTemp.Rows.Count - 1).Cells(colColumnHeader).Value = objTr.Column_Header
                    gvColumnTemp.Rows(gvColumnTemp.Rows.Count - 1).Cells(colSeqNo).Value = objTr.Seq_No
                    gvColumnTemp.Rows(gvColumnTemp.Rows.Count - 1).Cells(colIsMandatory).Value = objTr.IsMandatory
                Next
            End If
        End If
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
        If Me.gvColumnTemp.Rows.Count = 0 Then
            myMessages.blankValue("Second List is Empty")
            gvColumnTemp.Focus()
            Errorcontrol.SetError(gvColumnTemp, "Second List is Empty")
            Return False
        Else
            Errorcontrol.ResetError(gvColumnTemp)
        End If

        RefershLineNo()
        Return True
    End Function

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
                If (clsTemplateExpImp.DeleteData(txtCode.Value)) Then
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

    Private Sub frmTemplateExpImp_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'SetUserMgmtNew()
        LoadGridColumns(gvColumnsMain, False)
        LoadGridColumns(gvColumnTemp, True)
        'LoadReportType()
        isNewEntry = True
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update ")
        ' ButtonToolTip.SetToolTip(btnPost, "Press Alt+P for  Post")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D  for Delete ")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnNew, "Press Alt+N Adding New ")
        '  ButtonToolTip.SetToolTip(btnPrint, "Press Alt+R for Print Preview")
        funReset()
        loadDefault()
    End Sub
    'Sub LoadReportType()
    '    If dtReportType.Rows.Count > 0 AndAlso dtReportType.Columns.Count >= 1 Then
    '        ddlReportType.DataSource = dtReportType
    '        ddlReportType.ValueMember = dtReportType.Columns(0).ColumnName
    '        ddlReportType.DisplayMember = If(dtReportType.Columns.Count > 1, dtReportType.Columns(1).ColumnName, dtReportType.Columns(0).ColumnName)            
    '    End If
    '    If ddlReportType.Items.Count > 0 Then
    '        If clsCommon.myLen(Report_Type) > 0 Then
    '            ddlReportType.SelectedValue = Report_Type
    '            ddlReportType.Enabled = False
    '        Else
    '            ddlReportType.SelectedIndex = 0
    '        End If

    '    End If
    'End Sub
    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        funReset()
    End Sub

    Sub funReset()
        isNewEntry = True
        txtCode.MyReadOnly = False
        txtName.ReadOnly = False
        txtCode.Value = Nothing
        txtCode.Focus()
        txtName.Text = ""
        Me.gvColumnsMain.Rows.Clear()
        Me.gvColumnTemp.Rows.Clear()
        Me.gvColumnsMain.Rows.AddNew()
        LoadMainColumns()
        txtProgram.Value = FormType
        'lblProgramDesc.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Program_Name from tspl_program_Master where program_Code='" & FormType & "'"))
        fndUser.Value = objCommonVar.CurrentUserCode
        lblUserName.Text = clsUserMaster.GetName(fndUser.Value, Nothing)
        btnsave.Text = "Save"
        btnsave.Enabled = True
        btndelete.Enabled = False

    End Sub
    Sub loadDefault()
        Try
            Dim qry As String = "select Max(Export_Code) as Export_Code from TSPL_TEMPLATE_EXPIMP_HEAD where Program_Code='" & FormType & "' "
            Dim code As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
            If clsCommon.myLen(code) > 0 Then
                LoadData(code, Nothing)
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
            Dim str As String = "select count(*) from TSPL_TEMPLATE_EXPIMP_HEAD where Export_Code ='" + txtCode.Value + "' "
            Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
            If no = 0 AndAlso isButtonClicked = False Then
                txtCode.MyReadOnly = False
            Else
                txtCode.MyReadOnly = True
            End If
            If txtCode.MyReadOnly OrElse isButtonClicked Then
                '' finder query condition change by Panch Raj against Ticket No: KDI/11/05/18-000311
                txtCode.Value = clsTemplateExpImp.GetFinder(" TSPL_TEMPLATE_EXPIMP_HEAD.Program_Code='" + txtProgram.Value + "'", txtCode.Value, isButtonClicked)
                txtName.Text = clsTemplateExpImp.GetName(txtCode.Value, Nothing)
                LoadData(txtCode.Value, NavigatorType.Current)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub txtCode__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtCode._MYNavigator
        Try
            LoadData(txtCode.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub frmTemplateExpImp_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
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

    Sub LoadGridColumns(ByVal gv As RadGridView, ByVal TempGrid As Boolean)
        gv.DataSource = Nothing
        gv.Rows.Clear()
        gv.Columns.Clear()

        seqNo = New GridViewDecimalColumn()
        seqNo.FormatString = ""
        seqNo.HeaderText = "S.No."
        seqNo.Name = colSeqNo
        seqNo.Width = 60
        seqNo.ReadOnly = True
        seqNo.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv.Columns.Add(seqNo)

        Column = New GridViewTextBoxColumn()
        Column.FormatString = ""
        Column.HeaderText = "Column Name"
        Column.Name = colColumnName
        Column.Width = 100
        Column.ReadOnly = True
        Column.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv.Columns.Add(Column)

        Column = New GridViewTextBoxColumn()
        Column.FormatString = ""
        Column.HeaderText = "Column Header"
        Column.Name = colColumnHeader
        Column.Width = 150
        Column.ReadOnly = True
        Column.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv.Columns.Add(Column)

        If TempGrid = True Then
            seqNo = New GridViewDecimalColumn()
            seqNo.FormatString = ""
            seqNo.HeaderText = "Mandatory"
            seqNo.Name = colIsMandatory
            seqNo.Width = 30
            seqNo.ReadOnly = True
            seqNo.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
            gv.Columns.Add(seqNo)
        End If

    End Sub
    Sub LoadMainColumns()


        'Dim ListImpExpColumnsMandatory1 As List(Of String) = ListImpExpColumnsMandatory
        'Dim ListImpExpColumns1 As List(Of String) = ListImpExpColumns

        Me.gvColumnsMain.Rows.Clear()
        'For Each gcol As GridViewColumn In gvReport.Columns
        '    If gcol.IsVisible = True Then
        '        Me.gvColumnsMain.Rows.AddNew()
        '        Me.gvColumnsMain.Rows(gvColumnsMain.Rows.Count - 1).Cells(colSeqNo).Value = Me.gvColumnsMain.Rows.Count
        '        Me.gvColumnsMain.Rows(gvColumnsMain.Rows.Count - 1).Cells(colColumnName).Value = gcol.Name
        '        Me.gvColumnsMain.Rows(gvColumnsMain.Rows.Count - 1).Cells(colColumnHeader).Value = gcol.HeaderText
        '    End If

        'Next

        If ListImpExpColumns1 IsNot Nothing AndAlso ListImpExpColumnsMandatory1 IsNot Nothing AndAlso ListImpExpColumns1.Count > 0 AndAlso ListImpExpColumnsMandatory1.Count > 0 Then
            For Each field As String In ListImpExpColumns1
                If Not ListImpExpColumnsMandatory1.Contains(field) Then
                    Me.gvColumnsMain.Rows.AddNew()
                    Me.gvColumnsMain.Rows(gvColumnsMain.Rows.Count - 1).Cells(colSeqNo).Value = Me.gvColumnsMain.Rows.Count
                    Me.gvColumnsMain.Rows(gvColumnsMain.Rows.Count - 1).Cells(colColumnName).Value = field
                    Me.gvColumnsMain.Rows(gvColumnsMain.Rows.Count - 1).Cells(colColumnHeader).Value = field
                End If
            Next

            For Each field As String In ListImpExpColumnsMandatory1
                Me.gvColumnTemp.Rows.AddNew()
                Me.gvColumnTemp.Rows(gvColumnTemp.Rows.Count - 1).Cells(colSeqNo).Value = Me.gvColumnTemp.Rows.Count
                Me.gvColumnTemp.Rows(gvColumnTemp.Rows.Count - 1).Cells(colColumnName).Value = field
                Me.gvColumnTemp.Rows(gvColumnTemp.Rows.Count - 1).Cells(colColumnHeader).Value = field
                Me.gvColumnTemp.Rows(gvColumnTemp.Rows.Count - 1).Cells(colIsMandatory).Value = "1"
            Next
        End If

    End Sub

    Private Sub rdbNext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbNext.Click
        addColumn()
    End Sub
    Sub addColumn()
        Try

            If gvColumnsMain.CurrentRow.Index >= 0 Then
                '' check already exist
                For Each grow As GridViewRowInfo In gvColumnTemp.Rows
                    If clsCommon.CompairString(grow.Cells(colColumnName).Value, Me.gvColumnsMain.CurrentRow.Cells(colColumnName).Value) = CompairStringResult.Equal Then
                        clsCommon.MyMessageBoxShow("Column: " & Me.gvColumnsMain.CurrentRow.Cells(colColumnName).Value & " already added")
                        Exit Sub
                    End If
                Next
                Me.gvColumnTemp.Rows.AddNew()
                Me.gvColumnTemp.Rows(Me.gvColumnTemp.Rows.Count - 1).Cells(colSeqNo).Value = Me.gvColumnTemp.Rows.Count
                Me.gvColumnTemp.Rows(Me.gvColumnTemp.Rows.Count - 1).Cells(colColumnName).Value = Me.gvColumnsMain.CurrentRow.Cells(colColumnName).Value
                Me.gvColumnTemp.Rows(Me.gvColumnTemp.Rows.Count - 1).Cells(colColumnHeader).Value = Me.gvColumnsMain.CurrentRow.Cells(colColumnHeader).Value
                Me.gvColumnTemp.Rows(Me.gvColumnTemp.Rows.Count - 1).Cells(colIsMandatory).Value = "0"
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        If gvColumnTemp.CurrentRow.Index >= 0 AndAlso (Me.gvColumnTemp.CurrentRow.Cells(colIsMandatory).Value) = 0 Then
            Me.gvColumnsMain.Rows.AddNew()
            Me.gvColumnsMain.Rows(Me.gvColumnsMain.Rows.Count - 1).Cells(colColumnName).Value = Me.gvColumnTemp.CurrentRow.Cells(colColumnName).Value
            Me.gvColumnsMain.Rows(Me.gvColumnsMain.Rows.Count - 1).Cells(colColumnHeader).Value = Me.gvColumnTemp.CurrentRow.Cells(colColumnHeader).Value
            gvColumnTemp.Rows.RemoveAt(Me.gvColumnTemp.CurrentRow.Index)
            RefershLineNo()
        End If
    End Sub

    Private Sub RefershLineNo()
        For Each grow As GridViewRowInfo In gvColumnTemp.Rows
            grow.Cells(colSeqNo).Value = grow.Index + 1
        Next
        For Each grow As GridViewRowInfo In gvColumnsMain.Rows
            grow.Cells(colSeqNo).Value = grow.Index + 1
        Next
    End Sub


    Private Sub gvColumnsMain_DoubleClick(sender As Object, e As EventArgs) Handles gvColumnsMain.DoubleClick
        addColumn()
    End Sub

    Private Sub gvColumnTemp_UserDeletingRow(sender As Object, e As GridViewRowCancelEventArgs) Handles gvColumnTemp.UserDeletingRow
        If clsCommon.myCdbl(gvColumnTemp.CurrentRow.Cells(colIsMandatory).Value) = 1 Then
            common.clsCommon.MyMessageBoxShow(Me, "Mandatory column can't be deleted.", Me.Text)
            e.Cancel = True
        End If
    End Sub

    Private Sub gvColumnTemp_UserDeletedRow(sender As Object, e As GridViewRowEventArgs) Handles gvColumnTemp.UserDeletedRow
        RefershLineNo()
    End Sub
End Class
