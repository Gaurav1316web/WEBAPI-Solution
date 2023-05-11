'Created By---> Mayank
'Created Date--->15/june/2011
'Modified By--> Mayank
'Last Modify Date-->
'Tables Used-->TSPL_TDS_STATE_MASTER.
Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Data.OleDb
Imports System.Drawing
Imports System.IO
Imports System.Data.SqlClient
Imports Telerik.WinControls.UI
Imports Telerik.WinControls.Data
Imports Telerik.Data
Imports Telerik.WinControls.Enumerations
Imports Telerik.WinControls
Imports System.Text.RegularExpressions
Imports Telerik.WinControls.UI.Export
Imports Telerik.WinControls.UI.Export.ExportToExcelML
Imports Excel = Microsoft.Office.Interop.Excel
Imports System.Globalization
Imports System.Threading
Imports common
Imports XpertERPEngine
'--preeti gupta..ticket no.[BM00000003134]
Public Class frmStateCode
    Inherits FrmMainTranScreen

    Dim userCode, companyCode As String
    Public Sub New(ByVal user As String, ByVal company As String)
        InitializeComponent()
        userCode = user
        companyCode = company
    End Sub
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.StateCode)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
            Me.Close()
        End If
        rbtnSave.Visible = MyBase.isModifyFlag
        '--------richa Ticket no. BM00000003014 02/07/2014 to enable/disable import/export option acc. to user mgmt setting -----------
        If rbtnSave.Visible = True Then
            RadMenuItemImport.Enabled = True
            RadMenuItemExport.Enabled = True
        Else
            RadMenuItemImport.Enabled = False
            RadMenuItemExport.Enabled = False
        End If
        '--------------------------------------------------
        'btnPost.Visible = MyBase.isPostFlag
        rbtnDelete.Visible = MyBase.isDeleteFlag
    End Sub
    Private Sub frmStateCode_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso rbtnSave.Enabled Then
            SaveData()
            'ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag Then
            '    PostData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso rbtnDelete.Enabled Then
            DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Close()
        End If
    End Sub
    Private Sub FrmStateCode_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        'globalFunc.mandatoryText(fndStateCode.txtValue)
        'AddHandler fndStateCode.txtValue.TextChanged, AddressOf text_changed
        'AddHandler fndStateCode.txtValue.KeyPress, AddressOf fndStateCode_KeyPress
        FndStateCodeNew.MyCharacterCasing = CharacterCasing.Upper
        rbtnDelete.Enabled = False
        'If userCode <> "ADMIN" Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If
    End Sub

    'It Is Used To Fill The state Code In fndStateCode from TSPL_TDS_STATE_MASTER
    'Private Sub fndStateCode_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles fndStateCode.Load
    '    fndStateCode.ConnectionString = connectSql.SqlCon()
    '    fndStateCode.Query = "select state_Code as [state Code],State_Name as [State Name] from TSPL_TDS_STATE_MASTER"
    '    fndStateCode.Caption = "State Code"
    '    fndStateCode.ValueToSelect = "state Code"
    '    fndStateCode.ValueToSelect1 = "State Name"
    'End Sub
    '' Added By Abhishek Kumar as on 4 June 2012

    Private Sub FndStateCodeNew__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles FndStateCodeNew._MYNavigator
        Dim qry As String = "select state_Code as [Code],State_Name as [State Name] from TSPL_TDS_STATE_MASTER where 2=2"
        Select Case NavType
            Case NavigatorType.Current
                qry += " and TSPL_TDS_STATE_MASTER   .state_Code in ('" + FndStateCodeNew.Value + "')"
            Case NavigatorType.Next
                qry += " and TSPL_TDS_STATE_MASTER   .state_Code in (select min(state_Code) from TSPL_TDS_STATE_MASTER  where state_Code >'" + FndStateCodeNew.Value + "')"
            Case NavigatorType.First
                qry += " and TSPL_TDS_STATE_MASTER  .state_Code in (select MIN(state_Code) from TSPL_TDS_STATE_MASTER )"

            Case NavigatorType.Last
                qry += " and TSPL_TDS_STATE_MASTER   .state_Code in (select Max(state_Code) from TSPL_TDS_STATE_MASTER )"
            Case NavigatorType.Previous
                qry += " and TSPL_TDS_STATE_MASTER  .state_Code in (select Max(state_Code ) from TSPL_TDS_STATE_MASTER  where state_Code <'" + FndStateCodeNew.Value + "')"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            FndStateCodeNew.Value = clsCommon.myCstr(dt.Rows(0)("Code"))
        End If
        Loaddata()
    End Sub

    Private Sub FndStateCodeNew__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles FndStateCodeNew._MYValidating
        Dim Qry As String = " Select Count(*) From TSPL_TDS_STATE_MASTER where state_Code='" & FndStateCodeNew.Value & "'"
        Dim Count As String = clsDBFuncationality.getSingleValue(Qry)
        If Count = 0 Then
            FndStateCodeNew.MyReadOnly = False
        Else
            FndStateCodeNew.MyReadOnly = True
        End If
        If FndStateCodeNew.MyReadOnly Or isButtonClicked Then

            'Dim qry1 As String = "select state_Code as [Code],State_Name as [State Name] from TSPL_TDS_STATE_MASTER"
            'FndStateCodeNew.Value = clsCommon.ShowSelectForm("PersonCodestate", qry1, "Code", "", FndStateCodeNew.Value, "Code", isButtonClicked)
            FndStateCodeNew.Value = clsStateCode.getFinder("", FndStateCodeNew.Value, isButtonClicked)
            FndStateCodeNew.MyMaxLength = 12
            Loaddata()
        End If
    End Sub

    Private Sub FndStateCodeNew_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles FndStateCodeNew.KeyPress
        If Microsoft.VisualBasic.Asc(e.KeyChar) = 39 Then
            e.Handled = True
        End If
    End Sub
    'It Validate User To Press The Keys 
    ''Private Sub fndStateCode_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)

    ''    If Microsoft.VisualBasic.Asc(e.KeyChar) = 39 Then
    ''        e.Handled = True
    ''    End If
    ''End Sub
    'It Is Used To Save And Update All Record.
    Private Sub rbtnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtnSave.Click
        SaveData()
    End Sub
    Sub SaveData()
        If (IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowAutoGenerateDocNoInMaster, clsFixedParameterCode.AllowAutoGenerateDocNoInMaster, Nothing)) = "1", True, False) = False) AndAlso FndStateCodeNew.Value = "" Then
             myMessages.blankValue("State Code")
            FndStateCodeNew.Focus()
        ElseIf rbtnSave.Text = "Save" Then
            funinsert()
        Else
            funUpdate()
        End If
    End Sub
    'It Is Used To Delete The Record From Table.
    Private Sub rbtnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtnDelete.Click
        DeleteData()
    End Sub
    Sub DeleteData()
        If FndStateCodeNew.Value = "" Then
            myMessages.blankValue("State Code")
        ElseIf myMessages.deleteConfirm() Then
            fundelete()
            rbtnSave.Text = "Save"
            rbtnDelete.Enabled = False
        End If
    End Sub
    'This is Insert Function Used To Insert Values In Table.
    Private Sub funinsert()
        Try
            If (IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowAutoGenerateDocNoInMaster, clsFixedParameterCode.AllowAutoGenerateDocNoInMaster, Nothing)) = "1", True, False)) Then
                Dim ChkNewEntry As String = clsDBFuncationality.getSingleValue("Select count(*) from TSPL_TDS_STATE_MASTER where State_Code='" & FndStateCodeNew.Value & "'")
                If ChkNewEntry = 0 Then
                    FndStateCodeNew.Value = clsERPFuncationality.GetNextCode(Nothing, clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy"), clsDocType.StateCode, "", "")
                    If clsCommon.myLen(FndStateCodeNew.Value) <= 0 Then
                        Throw New Exception("Error in Code Generation")
                    End If
                End If
            End If
            connectSql.RunSp("SP_TSPL_TDS_STATE_MASTER_INSERT", New SqlParameter("@State_Code", FndStateCodeNew.Value), New SqlParameter("@State_Name", txtStateName.Text), New SqlParameter("@Created_By", userCode), New SqlParameter("@Created_Date", connectSql.serverDate()), New SqlParameter("@Modify_By", userCode), New SqlParameter("@Modify_Date", connectSql.serverDate()), New SqlParameter("@Comp_Code", companyCode))
            myMessages.insert()
            rbtnSave.Text = "Update"
            rbtnDelete.Enabled = True
            'If userCode <> "ADMIN" Then
            '    If funSetUserAccess() = False Then Exit Sub
            'End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub
    'This is Update Function Used To Update Records In Table.
    Private Sub funUpdate()
        Try
            connectSql.RunSp("SP_TSPL_TDS_STATE_MASTER_UPDATE", New SqlParameter("@State_Code", FndStateCodeNew.Value), New SqlParameter("@State_Name", txtStateName.Text), New SqlParameter("@Modify_By", userCode), New SqlParameter("@Modify_Date", connectSql.serverDate()), New SqlParameter("@Comp_Code", companyCode))
            myMessages.update()
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try

    End Sub
    'This is Delete Function Used To Delete Records From Table.
    Private Sub fundelete()
        Try
            connectSql.RunSp("SP_TSPL_TDS_STATE_MASTER_Delete", New SqlParameter("@State_Code", FndStateCodeNew.Value))
            myMessages.delete()
        Catch ex As Exception

        End Try
    End Sub
    'It is Used To Clear All Fields Of Current Windows Form
    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        FndStateCodeNew.Value = ""
        FndStateCodeNew.MyReadOnly = False
        txtStateName.Text = ""
        rbtnSave.Text = "Save"
        rbtnDelete.Enabled = False
    End Sub
    'It Is Used To Close The Current Windows Form
    Private Sub rbtnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtnClose.Click
        Me.Close()
    End Sub
    'It Is Used To Fill Or Clear All Fields of Current Windows Form Bassed On State Code(fndStateCode).
    'Public Sub text_changed(ByVal sender As System.Object, ByVal e As System.EventArgs)

    'End Sub

    Public Sub Loaddata()
        Try
            Dim strStateCode As String = "select State_Code from TSPL_TDS_STATE_MASTER where State_Code='" + FndStateCodeNew.Value + "'"
            'Dim dr As sqldatareader 
            ' dr = connectSql.RunSqlReturnDR(strStateCode)
            Dim strvalue As String = clsDBFuncationality.getSingleValue(strStateCode)

            'If dr.Read() Then
            '    strvalue = dr(0).ToString()
            'End If

            If (strvalue <> "") Then
                funfill()
            Else
                txtStateName.Text = ""
                rbtnSave.Text = "Save"
                rbtnDelete.Enabled = False
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub
    'This is Funfill Function Used To Fill All Fields of Current Windows Form.
    Private Sub funfill()
        Try
            Dim strQuery As String = "select State_Name from TSPL_TDS_STATE_MASTER where State_Code='" + FndStateCodeNew.Value + "'"
            'Dim dr As SqlDataReader
            'dr = connectSql.RunSqlReturnDR(strQuery)
            'If dr.Read() Then
            ' txtStateName.Text = dr(0).ToString()
            txtStateName.Text = clsDBFuncationality.getSingleValue(strQuery)
            If (clsCommon.myCstr(txtStateName.Text) <> "") Then
                rbtnDelete.Enabled = True
                rbtnSave.Text = "Update"
                'If userCode <> "ADMIN" Then
                '    If funSetUserAccess() = False Then Exit Sub
                'End If
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub
    'It Is Used To Export The Records From Table.
    Private Sub RadMenuItemExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItemExport.Click
        Dim strSql As String = "select State_Code as [State Code], State_Name as [State Name] from TSPL_TDS_STATE_MASTER"
        transportSql.ExporttoExcel(strSql, Me)
    End Sub
    'It Is Used To Import The Records From Table.
    Private Sub RadMenuItemImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItemImport.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        If transportSql.importExcel(gv, "State Code", "State Name") Then
            Dim trans As SqlTransaction = Nothing
            Try
                connectSql.OpenConnection()
                trans = clsDBFuncationality.GetTransactin()
                clsCommon.ProgressBarShow()
                For Each grow As GridViewRowInfo In gv.Rows
                    Dim strStateCode As String = clsCommon.myCstr(grow.Cells(0).Value)
                    If String.IsNullOrEmpty(strStateCode) Or clsCommon.myLen(strStateCode) > 12 Then
                        Throw New Exception("State Code Can not be left Blank or size can not be grater than 12")
                    End If

                    Dim strStateName As String = clsCommon.myCstr(grow.Cells(1).Value)
                    If clsCommon.myLen(strStateName) > 50 Then
                        Throw New Exception("State Name Can not be grater than 50")
                    End If

                    Dim strquery As String = "select count(*) from TSPL_TDS_STATE_MASTER where State_Code='" + strStateCode + "'"
                    Dim i As Integer = CInt(connectSql.RunScalar(trans, strquery))
                    If (i = 0) Then
                        connectSql.RunSpTransaction(trans, "SP_TSPL_TDS_STATE_MASTER_INSERT", New SqlParameter("@State_Code", strStateCode), New SqlParameter("@State_Name", strStateName), New SqlParameter("@Created_By", userCode), New SqlParameter("@Created_Date", connectSql.serverDate(trans)), New SqlParameter("@Modify_By", userCode), New SqlParameter("@Modify_Date", connectSql.serverDate(trans)), New SqlParameter("@Comp_Code", companyCode))
                    Else
                        connectSql.RunSpTransaction(trans, "SP_TSPL_TDS_STATE_MASTER_UPDATE", New SqlParameter("@State_Code", strStateCode), New SqlParameter("@State_Name", strStateName), New SqlParameter("@Modify_By", userCode), New SqlParameter("@Modify_Date", connectSql.serverDate(trans)), New SqlParameter("@Comp_Code", companyCode))
                    End If
                Next
                trans.Commit()
                clsCommon.ProgressBarHide()
                common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                trans.Rollback()
                clsCommon.ProgressBarHide()
                myMessages.myExceptions(ex)

            End Try
        End If
        Me.Controls.Remove(gv)
    End Sub
    'It Is Used To Close The Current Windows Form.
    Private Sub RadMenuItemClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItemClose.Click
        Me.Close()
    End Sub
    ' It Is Used To Give The Authority To User,To Access This Form(It Is Bassed On Mapping)
    'Private Function funSetUserAccess() As Boolean
    '    Try
    '        Dim strRights As String
    '        Dim strTemp() As String
    '        Dim strProgCode = "STATE-M"
    '        strRights = enuUserRights.enuRead & "," & enuUserRights.enuModify & "," & enuUserRights.enuDelete
    '        strRights = modUserMgt.funGetPermissions(strRights, strProgCode)
    '        strTemp = Split(strRights, ",")
    '        If strTemp(0) = "0" Then
    '            MsgBox("Permission Denied", MsgBoxStyle.Critical, Me.Text)
    '            funSetUserAccess = False
    '            blnRead = False
    '            Me.Close()
    '            Exit Function
    '        Else
    '            blnRead = True
    '        End If
    '        If strTemp(1) = "0" Then 'Grant modify access
    '            rbtnSave.Enabled = False
    '        End If
    '        If strTemp(2) = "0" Then 'Grant modify access
    '            rbtnDelete.Enabled = False
    '        End If

    '        funSetUserAccess = True
    '    Catch er As Exception

    '    End Try
    'End Function

  
End Class
