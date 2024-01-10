'--12/12/2018--form Add By- Sanjay Gupta
Imports common
Imports System.Data
Imports System.Data.SqlClient
Public Class frmCreateTemplate
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    'Dim userCode, companyCode, FormType As String
    'Dim strCatStrPass As String = ""
    'Dim seqNo As GridViewDecimalColumn
    'Dim Column As GridViewTextBoxColumn
    'Public gvReport As RadGridView
    'Public dtReportType As DataTable
    'Public Report_Type As String
    Public ReportId As String
    Public StrGridLayout As String
    Public ColumnCount As Integer
    Dim dt As DataTable

#Region "Variable"
    'Private isNewEntry As Boolean = False
    Private isInsideLoadData As Boolean = False
    Dim Qry As String
    'Const colColumnName As String = "colColumnName"
    'Const colColumnHeader As String = "colColumnHeader"
    'Const colSeqNo As String = "colSeqNo"


    Dim Errorcontrol As clsErrorControl = New clsErrorControl()
#End Region

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        Save()
    End Sub
    Public Sub New() 'ByVal Report_Id As String, ByVal StrGridview As String, ByVal ColumnCount As Integer'ByVal user As String, ByVal company As String, ByVal form_type As String)
        InitializeComponent()
        'userCode = user
        'companyCode = company
        'ReportId = Report_Id

    End Sub
    Public Sub Save()
        Try
            If AllowToSave() Then
                Dim obj As New clsManageTemplate()
                obj.ReportId = clsCommon.myCstr(ReportId)
                obj.TemplateName = clsCommon.myCstr(txtName.Text)
                obj.GridLayout = clsCommon.myCstr(StrGridLayout)
                obj.GridColumns = clsCommon.myCdbl(ColumnCount)
                'obj.Arr = New List(Of clsExportTemplateDetail)

                'For Each grow As GridViewRowInfo In gvColumnTemp.Rows
                '    Dim objTr As New clsExportTemplateDetail()
                '    objTr.Export_Code = clsCommon.myCstr(Me.txtCode.Value)
                '    objTr.Column_Name = clsCommon.myCstr(grow.Cells(colColumnName).Value)
                '    objTr.Column_Header = clsCommon.myCstr(grow.Cells(colColumnHeader).Value)
                '    objTr.Seq_No = clsCommon.myCdbl(grow.Cells(colSeqNo).Value)
                '    obj.Arr.Add(objTr)
                'Next
                clsManageTemplate.SaveData(obj)
                'LoadData(obj.Export_Code, NavigatorType.Current)
                clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                funClose()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        'txtCode.MyReadOnly = True
        'btnsave.Enabled = True
        'btndelete.Enabled = False
        'Dim obj As New clsExportTemplate()
        'obj = clsExportTemplate.GetData(strCode, txtProgram.Value, ddlReportType.SelectedValue, NavTyep)
        LoadTemplate()
        ' If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Export_Code) > 0) Then
        'funReset()
        'isNewEntry = False
        'btnsave.Text = "Update"
        'btndelete.Enabled = True
        'txtCode.Value = obj.Export_Code
        'txtName.Text = obj.Template_Name
        'ddlReportType.SelectedValue = obj.Report_Type
        'txtCode.MyReadOnly = True
        'LoadMainColumns(gvReport)
        'Dim ii As Int16 = 0
        'If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
        '    LoadGridColumns(gvColumnTemp)
        '    For Each objTr As clsExportTemplateDetail In obj.Arr
        '        gvColumnTemp.Rows.AddNew()
        '        ii = ii + 1
        '        gvColumnTemp.Rows(gvColumnTemp.Rows.Count - 1).Cells(colColumnName).Value = objTr.Column_Name
        '        gvColumnTemp.Rows(gvColumnTemp.Rows.Count - 1).Cells(colColumnHeader).Value = objTr.Column_Header
        '        gvColumnTemp.Rows(gvColumnTemp.Rows.Count - 1).Cells(colSeqNo).Value = objTr.Seq_No
        '    Next
        'End If
        'End If
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
        'If Me.gvColumnTemp.Rows.Count = 0 Then
        '    myMessages.blankValue("Second List is Empty")
        '    gvColumnTemp.Focus()
        '    Errorcontrol.SetError(gvColumnTemp, "Second List is Empty")
        '    Return False
        'Else
        '    Errorcontrol.ResetError(gvColumnTemp)
        'End If

        'RefershLineNo()
        Return True
    End Function

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click
        DeleteData()
    End Sub

    Sub DeleteData()
        If clsCommon.myLen(txtName.Text) <= 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "Select Template for Delete", Me.Text)
            Exit Sub
        End If
        funDelete()
        txtName.Text = ""
        isInsideLoadData = True
        LoadTemplate()
        isInsideLoadData = False
    End Sub

    Sub funDelete()
        Try
            If (myMessages.deleteConfirm()) Then
                If (clsManageTemplate.DeleteData(txtReportId.Text, txtName.Text)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ", Me.Text)
                    LoadTemplate()
                End If
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try

    End Sub

    Private Sub txtCode_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If (e.KeyChar = Chr(39)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub frmCreateTemplate_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'SetUserMgmtNew()
        'LoadGridColumns(gvColumnsMain)
        'LoadGridColumns(gvColumnTemp)
        'LoadReportType()
        'isNewEntry = True
        txtReportId.Text = ReportId
        txtUserId.Text = objCommonVar.CurrentUserCode
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update ")
        ' ButtonToolTip.SetToolTip(btnPost, "Press Alt+P for  Post")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D  for Delete ")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        isInsideLoadData = True
        LoadTemplate()
        isInsideLoadData = False
        'ButtonToolTip.SetToolTip(btnNew, "Press Alt+N Adding New ")
        '  ButtonToolTip.SetToolTip(btnPrint, "Press Alt+R for Print Preview")
        'funReset()
        'loadDefault()
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

    Sub LoadTemplate()
        dt = New DataTable
        dt = clsDBFuncationality.GetDataTable("select '' as TemplateName union select TemplateName from TSPL_MANAGE_TEMPLATE where ReportId='" & ReportId & "' and UserID='" & objCommonVar.CurrentUserCode & "'")
        ddlTemplate.DataSource = dt
        ddlTemplate.ValueMember = "TemplateName"
        ddlTemplate.DisplayMember = "TemplateName"
        ddlTemplate.SelectedValue = ""
    End Sub
   

    'Sub loadDefault()
    '    Try
    '        Dim qry As String = "select Max(Export_Code) as Export_Code from TSPL_EXPORT_TEMPLATE_HEAD where Program_Code='" & FormType & "' and Report_Type='" & Report_Type & "' and Created_By='" & objCommonVar.CurrentUserCode & "'"
    '        Dim code As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
    '        If clsCommon.myLen(code) > 0 Then
    '            LoadData(code, Nothing)
    '        End If
    '    Catch ex As Exception

    '    End Try
    'End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        funClose()
    End Sub

    Sub funClose()
        clsERPFuncationality.closeForm(Me)
    End Sub


    Private Sub frmCreateTemplate_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
       If e.Alt AndAlso e.KeyCode = Keys.S Then
            Save()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D Then
            DeleteData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            funClose()
        End If
    End Sub

    Private Sub ddlTemplate_SelectedIndexChanged(sender As Object, e As UI.Data.PositionChangedEventArgs) Handles ddlTemplate.SelectedIndexChanged
        If isInsideLoadData = False Then
            txtName.Text = clsCommon.myCstr(ddlTemplate.SelectedValue)
        End If
    End Sub
End Class
