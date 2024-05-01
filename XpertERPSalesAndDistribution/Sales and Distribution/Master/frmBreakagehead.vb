
'--Developed By -Pankaj Kumar Chaudhary
'--Database - TSPLERP
'--Table - tspl_Breakage_Head
'--Start Date -20/10/2011
'--End Date -
Imports common
Imports System.Data.SqlClient
Imports XpertERPEngine
Imports Telerik.WinControls.UI

Public Class FrmBreakagehead
    Inherits FrmMainTranScreen
#Region "Variables"
    Dim Qry As String
    Dim dt As DataTable
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private isNewEntry As Boolean = False
    Private isInsideLoadData As Boolean = False
#End Region

#Region "Events"

    Private Sub FrmFrmBreakagehead_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        'If Not clsCommon.CompairString(objCommonVar.CurrentUserCode, "ADMIN") = CompairStringResult.Equal Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If
        'TxtCategoryCode.Value.CharacterCasing = CharacterCasing.Upper
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnAddNew, "Press Alt+N Adding New Trasnaction")
        TxtBreakageType.MyReadOnly = True
        TxtBreakageType.MyMaxLength = 30    ''''Validates The Length Of Finder(txtBreakageType)
        tbDescription.MaxLength = 200       ''''Validates The Length Of TextBox(tbDescription)

        'If objCommonVar.CurrentUserCode <> "ADMIN" Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If


    End Sub

    Private Sub btnAddNew_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddNew.Click
        AddNew()
    End Sub

    Private Sub btnSave_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        SaveData()
    End Sub

    Private Sub btnDelete_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        DeleteData()
    End Sub

    Private Sub btnClose_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        CloseForm()
    End Sub

    ''''Finder(txtBreakageType) 
    Private Sub TxtBreakageType__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles TxtBreakageType._MYValidating

        If TxtBreakageType.MyReadOnly OrElse isButtonClicked Then
            Dim qry As String = "select TSPL_BREAKAGE_HEAD.Breakage_Type as [BreakageType],TSPL_BREAKAGE_HEAD.Description as [Description] from TSPL_BREAKAGE_HEAD "
            TxtBreakageType.Value = clsCommon.ShowSelectForm("BreakageYpeSelect", qry, "BreakageType", "", TxtBreakageType.Value, "BreakageType", isButtonClicked)
            tbDescription.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select TSPL_BREAKAGE_HEAD.Description from TSPL_BREAKAGE_HEAD where Breakage_Type='" + TxtBreakageType.Value + "'"))
        End If
        Dim str As String = "select count(*) from TSPL_BREAKAGE_HEAD where TSPL_BREAKAGE_HEAD.Breakage_Type ='" + TxtBreakageType.Value + "' "
        Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
        If no = 0 Then
            TxtBreakageType.MyReadOnly = False
        Else
            TxtBreakageType.MyReadOnly = True
        End If
    End Sub

    Private Sub TxtBreakageType__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles TxtBreakageType._MYNavigator
        Try
            LoadData(TxtBreakageType.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    ''Validation("Collon" is not to be Typed )
    Private Sub TxtBreakageType_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TxtBreakageType.KeyPress
        If (e.KeyChar = Chr(39)) Then
            e.Handled = True
        End If
    End Sub


    Private Sub FrmBreakagehead_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnAddNew.Enabled Then
            AddNew()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnSave.Enabled Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btnDelete.Enabled Then
            DeleteData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            CloseForm()
        End If
    End Sub

    Private Sub RadMenuItemImport_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItemImport.Click
        Import()
    End Sub

    Private Sub RadMenuItemExportt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItemExportt.Click
        Export()
    End Sub
#End Region

#Region "Methods"
    Sub BlankAllControls()
        TxtBreakageType.Value = Nothing
        tbDescription.Text = ""
    End Sub

    Sub AddNew()
        BlankAllControls()
        TxtBreakageType.Focus()
        TxtBreakageType.MyReadOnly = False
        isNewEntry = True
        btnSave.Text = "Save"
        btnSave.Enabled = True
        'btnPost.Enabled = True
        btnDelete.Enabled = True

    End Sub

    Sub SaveData()
        Try
            If (AllowToSave()) Then
                Dim obj As New clsBreakageHead()
                obj.Breakage_Type = (TxtBreakageType.Value).ToString
                obj.Description = tbDescription.Text
                If (obj.SaveData(obj, isNewEntry)) Then
                    common.clsCommon.MyMessageBoxShow("Data Saved Successfully")
                    LoadData(obj.Breakage_Type, NavigatorType.Current)
                Else
                    common.clsCommon.MyMessageBoxShow("Record Updated Successfully")
                    LoadData(obj.Breakage_Type, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub DeleteData()
        Try
            If (myMessages.deleteConfirm()) Then
                If (clsBreakageHead.DeleteData(TxtBreakageType.Value)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ", Me.Text)
                    AddNew()
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub LoadData(ByVal Breakage_Type As String, ByVal navType As common.NavigatorType)
        Try
            btnSave.Enabled = True
            'btnPost.Enabled = True
            btnDelete.Enabled = True
            isInsideLoadData = True
            isNewEntry = False
            btnSave.Text = "Update"
            BlankAllControls()
            TxtBreakageType.MyReadOnly = True

            Dim obj As New clsBreakageHead()
            obj = clsBreakageHead.GetData(Breakage_Type, navType)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Breakage_Type) > 0) Then
                TxtBreakageType.Value = obj.Breakage_Type
                tbDescription.Text = obj.Description
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isInsideLoadData = False
        End Try
    End Sub

    Function AllowToSave() As Boolean
        If (IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowAutoGenerateDocNoInMaster, clsFixedParameterCode.AllowAutoGenerateDocNoInMaster, Nothing)) = "1", True, False) = False) Then
            If clsCommon.myLen(TxtBreakageType.Value) <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please Enter Breakage Type")
                TxtBreakageType.Focus()
                Return False
            End If
        End If
        Return True
    End Function


    Sub CloseForm()
        Me.Close()
    End Sub
#End Region

#Region "Import/Export"
    Public Sub Import()
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim currentdate As Date = Date.Today
        If transportSql.importExcel(gv, "Breakage Type", "Description") Then
            Dim trans As SqlTransaction = Nothing
            clsCommon.ProgressBarShow()
            Try
                connectSql.OpenConnection()
                trans = clsDBFuncationality.GetTransactin()
                For Each grow As GridViewRowInfo In gv.Rows
                    Dim strBreakageType As String = clsCommon.myCstr(grow.Cells(0).Value)
                    Dim strDescription As String = clsCommon.myCstr(grow.Cells(1).Value)

                    If (String.IsNullOrEmpty(strDescription)) Or strDescription.Length > 200 Then
                        Throw New Exception("Sub Description can not be blank or incorrect")
                    End If

                    Dim sql1 As String = "select count(*) from TSPL_BREAKAGE_HEAD where Breakage_Type='" + strBreakageType + "'"
                    Dim i As Integer = CInt(connectSql.RunScalar(trans, sql1))
                    If (i = 0) Then
                        Qry = "INSERT Into TSPL_BREAKAGE_HEAD values('" + strBreakageType + "','" + strDescription + "', '" + objCommonVar.CurrentUser + "','" + connectSql.serverDate(trans) + "','" + objCommonVar.CurrentUser + "','" + connectSql.serverDate(trans) + "','" + objCommonVar.CurrentCompanyCode + "')"
                        connectSql.RunSqlTransaction(trans, Qry)
                        'connectSql.RunSpTransaction(trans, "sp_DesignationMaster_insert", New SqlParameter("@Category Code", strCtgryCode), New SqlParameter("@Category Name", strCtgryName), New SqlParameter("@createdby", userCode), New SqlParameter("@createddate", connectSql.serverDate()), New SqlParameter("@modifiedby", userCode), New SqlParameter("@modifieddate", connectSql.serverDate()), New SqlParameter("@compcode", companyCode))
                    Else
                        Qry = "UPDATE TSPL_BREAKAGE_HEAD set Breakage_Type='" + strBreakageType + "', Description='" + strDescription + "', Modify_By='" + objCommonVar.CurrentUser + "', Modify_Date='" + connectSql.serverDate(trans) + "', Comp_Code='" + objCommonVar.CurrentCompanyCode + "' WHERE Breakage_Type='" + strBreakageType + "'"
                        connectSql.RunSqlTransaction(trans, Qry)
                        'connectSql.RunSpTransaction(trans, "sp_DesignationMaster_update", New SqlParameter("@CAtegory Code", strCtgryCode), New SqlParameter("@Category Name", strCtgryName), New SqlParameter("@modifiedby", userCode), New SqlParameter("@modifieddate", connectSql.serverDate()), New SqlParameter("@compcode", companyCode))

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


    Public Sub Export()
        Dim str As String
        str = "select TSPL_BREAKAGE_HEAD.Breakage_Type As [Breakage Type],TSPL_BREAKAGE_HEAD.Description as [Description] from TSPL_BREAKAGE_HEAD"
        transportSql.ExporttoExcel(str, Me)
    End Sub
#End Region


    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.mbtnBreakageHead1)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
        btnSave.Visible = MyBase.isModifyFlag
        'btnPost.Visible = MyBase.isPostFlag
        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    'This will check the authorization of user to access the screen.If authorize then it will allow user to access the screen.
    'Private Function funSetUserAccess() As Boolean
    '    Try

    '        Dim strRights As String
    '        Dim strTemp() As String
    '        Dim strProgCode = "BREAKAGE"
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
    '            btnSave.Enabled = False
    '        End If
    '        If strTemp(2) = "0" Then 'Grant modify access
    '            btnDelete.Enabled = False
    '        End If

    '        funSetUserAccess = True
    '    Catch er As Exception
    '        myMessages.myExceptions(er)
    '    End Try
    'End Function



End Class

