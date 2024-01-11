Imports common
Imports Telerik.WinControls
Imports Telerik.WinControls.UI
Imports System.Data.SqlClient

Public Class FrmBalanceSheetPerforma1
    Inherits FrmMainTranScreen
#Region "Variables"
    Const SNo As String = "SNo"
    Const Main_Particular As String = "Main Particular"
    Const Particular As String = "Particular"
    Const ColMainGroupCode As String = "Group Code"
    Const Type As String = "Type"
    Const Group_Name As String = "Group Name"
    Const Note As String = "Note"
    Const ColGLMainAccount As String = "ColGLMainAccount"
    Private isInsideLoadData As Boolean = False
    Private objlist As New List(Of clsBalanceSheetPerforma)
    Dim settSelectGLInBalanceSheetPerforma As Boolean = False
    Dim strPAndLGroupCode As String
    Dim strPAndLGroupDesc As String
#End Region

    Private Sub FrmBalanceSheetPerforma1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        strPAndLGroupCode = clsFixedParameter.GetData(clsFixedParameterType.BalanceSheetProftAndLossGroupCode, clsFixedParameterCode.BalanceSheetProftAndLossGroupCode, Nothing)
        strPAndLGroupDesc = clsFixedParameter.GetData(clsFixedParameterType.BalanceSheetProftAndLossGroupDesc, clsFixedParameterCode.BalanceSheetProftAndLossGroupDesc, Nothing)

        settSelectGLInBalanceSheetPerforma = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.SelectGLInBalanceSheetPerforma, clsFixedParameterCode.SelectGLInBalanceSheetPerforma, Nothing)) > 0)
        RadLabel12.Visible = settSelectGLInBalanceSheetPerforma
        SetUserMgmtNew()
        gv2.AllowDeleteRow = True
        gv2.AllowAddNewRow = False
        gv2.ShowGroupPanel = False
        gv2.AllowColumnReorder = False
        gv2.AllowRowReorder = True
        gv2.EnableSorting = False
        gv2.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv2.MasterTemplate.ShowRowHeaderColumn = False
        LoadBlankGrid()
        LoadData()
        gv2.Rows.AddNew()
        If clsCommon.myLen(gv2.Rows(0).Cells(ColMainGroupCode).Value) <= 0 Then
            btnDelete.Enabled = False
        End If
    End Sub

    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.frmBalanceSheetPerforma)
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow(Me, "Permission Denied", Me.Text)
            Me.Close()
            Exit Sub
        End If
        btnSave.Visible = MyBase.isModifyFlag
        '--------richa Ticket no. BM00000003014 02/07/2014 to enable/disable import/export option acc. to user mgmt setting -----------
        If btnSave.Visible = True Then
            RadMenuItem4.Enabled = True
            Export.Enabled = True
        Else
            RadMenuItem4.Enabled = False
            Export.Enabled = False
        End If
        '--------------------------------------------------
        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        SavingData()
    End Sub

    Sub SavingData()

        Try
            Dim arr As New List(Of clsBalanceSheetPerforma)
            If AllowToSave() = True Then

                If MyBase.isModifyonPasswordFlag Then
                    If clsPasswordCheckForMasters.CheckMasterPwd(clsUserMgtCode.frmBalanceSheetPerforma, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
                    Else
                        Return
                    End If
                End If
                arr = New List(Of clsBalanceSheetPerforma)
                For ii As Integer = 0 To gv2.Rows.Count - 1
                    Dim obj As New clsBalanceSheetPerforma()
                    obj.SNo = clsCommon.myCdbl(gv2.Rows(ii).Cells(SNo).Value)
                    obj.MainParticular = clsCommon.myCstr(gv2.Rows(ii).Cells(Main_Particular).Value)
                    obj.Particular = clsCommon.myCstr(gv2.Rows(ii).Cells(Particular).Value)
                    obj.GroupCode = clsCommon.myCstr(gv2.Rows(ii).Cells(ColMainGroupCode).Value)
                    obj.Type = clsCommon.myCstr(gv2.Rows(ii).Cells(Type).Value)
                    obj.GroupName = clsCommon.myCstr(gv2.Rows(ii).Cells(Group_Name).Value)
                    obj.Note = clsCommon.myCstr(gv2.Rows(ii).Cells(Note).Value)
                    If settSelectGLInBalanceSheetPerforma Then
                        If clsCommon.myLen(obj.GroupCode) > 0 Then
                            obj.arrGLMainAccount = TryCast(gv2.Rows(ii).Cells(ColGLMainAccount).Tag, ArrayList)
                            If obj.arrGLMainAccount Is Nothing OrElse obj.arrGLMainAccount.Count <= 0 Then
                                If Not clsCommon.CompairString(strPAndLGroupCode, obj.GroupCode) = CompairStringResult.Equal Then
                                    Throw New Exception("Please select GL Main Accounts for Group Code [" + obj.GroupCode + "] at Sno" + clsCommon.myCstr(obj.SNo))
                                End If
                            End If
                        End If
                    End If
                    If clsCommon.myLen(obj.MainParticular) > 0 AndAlso clsCommon.myLen(obj.Particular) > 0 AndAlso clsCommon.myLen(obj.GroupCode) > 0 AndAlso clsCommon.myLen(obj.Type) > 0 Then
                        arr.Add(obj)
                    End If
                Next
                If clsBalanceSheetPerforma.SaveData(arr) = True Then
                    common.clsCommon.MyMessageBoxShow(Me, "Data Saved Sucsessfully", Me.Text)
                    btnDelete.Enabled = True

                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Public Function AllowToSave() As Boolean
        For ii As Integer = 0 To gv2.Rows.Count - 1
            If gv2.Rows(ii).Cells(SNo).Value IsNot Nothing Then

                If clsCommon.myCstr(gv2.Rows(ii).Cells("Main Particular").Value) Is Nothing OrElse clsCommon.myLen(gv2.Rows(ii).Cells(Main_Particular).Value) <= 0 Then
                    common.clsCommon.MyMessageBoxShow(Me, "Please Fill the Main Particular", Me.Text)
                    Return False
                ElseIf clsCommon.myCstr(gv2.Rows(ii).Cells("Particular").Value) Is Nothing OrElse clsCommon.myLen(gv2.Rows(ii).Cells(Particular).Value) <= 0 Then
                    common.clsCommon.MyMessageBoxShow(Me, "Please Fill the Particular", Me.Text)
                    Return False
                ElseIf clsCommon.myCstr(gv2.Rows(ii).Cells("Type").Value) Is Nothing OrElse clsCommon.myLen(gv2.Rows(ii).Cells("Type").Value) <= 0 Then
                    common.clsCommon.MyMessageBoxShow(Me, "Please Select the Type", Me.Text)
                    Return False
                ElseIf clsCommon.myCstr(gv2.Rows(ii).Cells("Group Code").Value) Is Nothing OrElse clsCommon.myLen(gv2.Rows(ii).Cells(ColMainGroupCode).Value) <= 0 Then
                    common.clsCommon.MyMessageBoxShow(Me, "Please Fill the Group Code", Me.Text)
                    Return False
                End If
            End If
        Next
        For ii As Integer = 0 To gv2.Rows.Count - 1
            For jj As Integer = ii + 1 To gv2.Rows.Count - 1
                If clsCommon.CompairString(gv2.Rows(ii).Cells(ColMainGroupCode).Value, gv2.Rows(jj).Cells(ColMainGroupCode).Value) = CompairStringResult.Equal Then
                    common.clsCommon.MyMessageBoxShow(Me, "This Group Already Exist", Me.Text)
                    gv2.CurrentRow.Cells(ColMainGroupCode).Value = ""
                    Return False
                End If
                If Not clsCommon.CompairString(gv2.Rows(ii).Cells(Main_Particular).Value, gv2.Rows(jj).Cells(Main_Particular).Value) = CompairStringResult.Equal Then
                    Dim varMain As String = gv2.Rows(jj).Cells(Main_Particular).Value
                    For pp As Integer = jj + 1 To gv2.Rows.Count - 1
                        If clsCommon.CompairString(varMain, gv2.Rows(pp).Cells(Main_Particular).Value) <> CompairStringResult.Equal Then
                            If clsCommon.CompairString(gv2.Rows(ii).Cells(Main_Particular).Value, gv2.Rows(pp).Cells(Main_Particular).Value) = CompairStringResult.Equal Then
                                common.clsCommon.MyMessageBoxShow(Me, "The Main Particular are same at SNo " + clsCommon.myCstr(gv2.Rows(ii).Cells(SNo).Value) + " And " + clsCommon.myCstr(gv2.Rows(pp).Cells(SNo).Value))
                                Return False
                                'Continue For
                            End If
                        End If
                    Next
                End If
            Next

        Next


        Return True
    End Function

    Sub LoadBlankGrid()
        gv2.Rows.Clear()
        gv2.Columns.Clear()
        gv2.AllowAddNewRow = False


        Dim repoLineNo As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoLineNo = New GridViewDecimalColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "SNo"
        repoLineNo.Name = SNo
        repoLineNo.Width = 50
        repoLineNo.ReadOnly = True
        repoLineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv2.MasterTemplate.Columns.Add(repoLineNo)


        Dim repoMain_Particular As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoMain_Particular.FormatString = ""
        repoMain_Particular.HeaderText = "Main Particular"
        repoMain_Particular.Width = 100
        repoMain_Particular.Name = Main_Particular
        repoMain_Particular.ReadOnly = False
        gv2.MasterTemplate.Columns.Add(repoMain_Particular)


        Dim repoParticular As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoParticular.FormatString = ""
        repoParticular.HeaderText = "Particular"
        repoParticular.Width = 100
        repoParticular.Name = Particular
        repoParticular.ReadOnly = False
        gv2.MasterTemplate.Columns.Add(repoParticular)

        Dim repoGroupCod As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoGroupCod.FormatString = ""
        repoGroupCod.HeaderText = "Group Code"
        repoGroupCod.Width = 100
        repoGroupCod.Name = ColMainGroupCode
        repoGroupCod.HeaderImage = My.Resources.search4
        repoGroupCod.TextImageRelation = TextImageRelation.TextBeforeImage
        repoGroupCod.ReadOnly = False
        gv2.MasterTemplate.Columns.Add(repoGroupCod)

        Dim repoNote As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoNote.FormatString = ""
        repoNote.HeaderText = "Note"
        repoNote.Width = 100
        repoNote.Name = Note
        repoNote.ReadOnly = False
        gv2.MasterTemplate.Columns.Add(repoNote)

        Dim repoGroupName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoGroupName.FormatString = ""
        repoGroupName.HeaderText = "Group Name"
        repoGroupName.Width = 200
        repoGroupName.Name = Group_Name
        repoGroupName.ReadOnly = False
        gv2.MasterTemplate.Columns.Add(repoGroupName)


        Dim repoType As GridViewComboBoxColumn = New GridViewComboBoxColumn()
        repoType.FormatString = ""
        repoType.HeaderText = "Type"
        repoType.Width = 100
        repoType.Name = Type
        repoType.ReadOnly = False
        repoType.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        repoType.DataSource = GetItemType()
        repoType.ValueMember = "Code"
        repoType.DisplayMember = "Code"
        gv2.MasterTemplate.Columns.Add(repoType)

        repoGroupName = New GridViewTextBoxColumn()
        repoGroupName.FormatString = ""
        repoGroupName.HeaderText = "GL Main Account"
        repoGroupName.Width = 100
        repoGroupName.Name = ColGLMainAccount
        repoGroupName.ReadOnly = True
        repoGroupName.IsVisible = settSelectGLInBalanceSheetPerforma
        gv2.MasterTemplate.Columns.Add(repoGroupName)
    End Sub

    Public Shared Function GetItemType() As DataTable
        Dim dt As New DataTable()
        dt.Columns.Add("Code", GetType(String))

        Dim dr As DataRow = dt.NewRow()
        dr("Code") = "Add"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Subtract"
        dt.Rows.Add(dr)

        Return dt
    End Function

    Private Sub gv2_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv2.CellValueChanged
        If Not isInsideLoadData Then


            If e.Column Is gv2.Columns(Main_Particular) OrElse e.Column Is gv2.Columns(Particular) OrElse e.Column Is gv2.Columns(ColMainGroupCode) OrElse e.Column Is gv2.Columns(Type) Then
                If e.Column Is gv2.Columns(ColMainGroupCode) Then
                    Dim qry As String = ""
                    qry = " select account_Main_group_code as [AccountGroupsCode],account_Main_group_desc as [Description] from TSPL_ACCOUNT_MAIN_GROUPS "
                    gv2.CurrentRow.Cells(ColMainGroupCode).Value = clsCommon.ShowSelectForm("BSPGroupCode", qry, "AccountGroupsCode", "", clsCommon.myCstr(gv2.CurrentRow.Cells(ColMainGroupCode).Value), "", False)

                    If clsCommon.myLen(strPAndLGroupCode) > 0 AndAlso clsCommon.CompairString(strPAndLGroupCode, clsCommon.myCstr(gv2.CurrentRow.Cells(ColMainGroupCode).Value)) = CompairStringResult.Equal Then
                        gv2.CurrentRow.Cells(Group_Name).Value = strPAndLGroupDesc
                    Else
                        gv2.CurrentRow.Cells(Group_Name).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select account_mAIN_group_desc from tspl_account_MAIN_groups where account_mAIN_group_code='" + gv2.CurrentRow.Cells(ColMainGroupCode).Value + "'"))
                    End If
                End If
                IndexChange()
            End If
        End If
    End Sub

    Sub IndexChange()
        Try
            Dim intCurrRow As Integer = gv2.CurrentRow.Index
            gv2.CurrentRow.Cells(SNo).Value = clsCommon.myCdbl(intCurrRow + 1)
            If intCurrRow = gv2.Rows.Count - 1 Then
                gv2.Rows.AddNew()
                gv2.CurrentRow = gv2.Rows(intCurrRow)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Sub LoadData()
        isInsideLoadData = True
        Dim Counter As Integer = 0
        LoadBlankGrid()
        Dim arr As List(Of clsBalanceSheetPerforma) = clsBalanceSheetPerforma.GetData()
        If arr IsNot Nothing AndAlso arr.Count > 0 Then
            For Each obj As clsBalanceSheetPerforma In arr
                gv2.Rows.AddNew()
                gv2.Rows(Counter).Cells(SNo).Value = obj.SNo
                gv2.Rows(Counter).Cells(Main_Particular).Value = obj.MainParticular
                gv2.Rows(Counter).Cells(Particular).Value = obj.Particular
                gv2.Rows(Counter).Cells(ColMainGroupCode).Value = obj.GroupCode
                gv2.Rows(Counter).Cells(Group_Name).Value = obj.GroupName
                gv2.Rows(Counter).Cells(Note).Value = obj.Note
                If clsCommon.myCdbl(obj.Type) = 1 Then
                    obj.Type = "Add"
                Else
                    obj.Type = "Subtract"
                End If
                gv2.Rows(Counter).Cells(Type).Value = obj.Type
                gv2.Rows(Counter).Cells(ColGLMainAccount).Tag = obj.arrGLMainAccount
                gv2.Rows(Counter).Cells(ColGLMainAccount).Value = clsCommon.GetMulcallStringWithComma(obj.arrGLMainAccount)
                Counter += 1
            Next
        End If
        isInsideLoadData = False
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub gv2_UserDeletingRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs) Handles gv2.UserDeletingRow
        If common.clsCommon.MyMessageBoxShow(Me, "Do you want to Delete the Current Row." + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
            e.Cancel = True
        End If
    End Sub

    Private Sub gv2_UserDeletedRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowEventArgs) Handles gv2.UserDeletedRow
        For ii As Integer = 1 To gv2.Rows.Count - 1
            gv2.Rows(ii - 1).Cells(SNo).Value = ii
        Next
    End Sub

    Private Sub FrmBalanceSheetPerforma1_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.F2 AndAlso (gv2.CurrentCell IsNot Nothing OrElse clsCommon.myLen(gv2.CurrentRow.Cells(ColMainGroupCode).Value) <= 0) Then
            isInsideLoadData = False
            If gv2.CurrentColumn Is gv2.Columns(ColMainGroupCode) Then
                gv2.CurrentColumn = gv2.Columns(Group_Name)
                gv2.CurrentColumn = gv2.Columns(ColMainGroupCode)
                gv2.CurrentRow.Cells(ColMainGroupCode).Value = "."
            End If
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnSave.Enabled Then
            SavingData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btnDelete.Enabled Then
            DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnclose.Enabled Then
            Close()
        End If
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        DeleteData()
    End Sub

    Sub DeleteData()
        If common.clsCommon.MyMessageBoxShow(Me, "Do You Really Want to Delete All Data", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
            clsBalanceSheetPerforma.DeleteData()
            LoadBlankGrid()
            gv2.Rows.AddNew()
            common.clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfull", Me.Text)
            btnDelete.Enabled = False
        End If
    End Sub

    Private Sub FunExportToExcel(ByVal exporter As EnumExportTo)
        Try
            Dim Sql As String = "Select S_No as [S No],Main_Particular as [Main Particular], Particular ,Group_Code as [Group Code],Group_Name as [Group Name],Note as [Note],Type from tspl_balance_sheet_performa"
            transportSql.ExporttoExcel(Sql, Me)  
        Catch ex As Exception
            clsCommon.ProgressBarHide()
            Throw New Exception(ex.Message)
       
        End Try
    End Sub

    Private Sub btnExpoertToExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Export.Click
        FunExportToExcel(EnumExportTo.Excel)
    End Sub

    Private Sub RadMenuItem4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem4.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        If transportSql.importExcel(gv, "S No", "Main Particular", "Particular", "Group Code", "Group Name", "Note", "Type") Then
            'Dim trans As SqlTransaction
            Dim obj As clsBalanceSheetPerforma
            Dim arr As New List(Of clsBalanceSheetPerforma)

            Try

                Dim LineNo As String = "0"
                clsCommon.ProgressBarShow()

                For Each grow As GridViewRowInfo In gv.Rows

                    LineNo = clsCommon.myCstr(grow.Index + 2)
                    obj = New clsBalanceSheetPerforma



                    Dim strv_id As String = clsCommon.myCstr(grow.Cells("S No").Value)
                    If String.IsNullOrEmpty(strv_id) Or clsCommon.myLen(strv_id) > 12 Then
                        Throw New Exception("Line " + LineNo + " : S_No has incorrect values")
                    End If
                    obj.SNo = strv_id

                    Dim strMain_Particular As String = clsCommon.myCstr(grow.Cells("Main Particular").Value)
                    If clsCommon.myLen(strMain_Particular) > 50 Then
                        Throw New Exception("Line " + LineNo + " : Main_Particular Can not be greater than 50")
                    End If
                    obj.MainParticular = strMain_Particular

                    Dim strParticular As String = clsCommon.myCstr(grow.Cells("Particular").Value)
                    If clsCommon.myLen(strParticular) > 50 Then
                        Throw New Exception("Line " + LineNo + " : Particular can not be greater than 50")
                    End If
                    obj.Particular = strParticular

                    Dim strGroup_Code As String = clsCommon.myCstr(grow.Cells("Group Code").Value)
                    If clsCommon.myLen(strGroup_Code) > 50 Then
                        Throw New Exception("Line " + LineNo + " : Description Can not be greater than 50")
                    End If

                    obj.GroupCode = strGroup_Code


                    Dim Count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*) from tspl_account_groups where  Account_Group_Code='" + strGroup_Code + "'"))
                    If Count <= 0 Then
                        Throw New Exception("Line " + LineNo + " : This  '" + strGroup_Code + "'  group code does not exist")
                    End If

                    Dim strGroup_Name As String = clsCommon.myCstr(grow.Cells("Group Name").Value)
                    If clsCommon.myLen(strGroup_Name) > 50 Then
                        Throw New Exception("Line " + LineNo + " : Group_Name  not be blank")
                    End If
                    obj.GroupName = strGroup_Name

                    Dim strNote As String = clsCommon.myCstr(grow.Cells("Note").Value)
                    If clsCommon.myLen(strNote) > 50 Then
                        Throw New Exception("Line " + LineNo + " :Note can not be blank")
                    End If
                    obj.Note = strNote

                    Dim strType As String = clsCommon.myCstr(grow.Cells("Type").Value)
                    If strType = "1" Then
                        obj.Type = "Add"
                    ElseIf strType = "-1" Then
                        obj.Type = "subtract"
                    Else
                        Throw New Exception("Line " + LineNo + " : Type can not be blank")

                    End If
                    arr.Add(obj)
                Next

                clsBalanceSheetPerforma.SaveData(arr)
                clsCommon.ProgressBarHide()
                common.clsCommon.MyMessageBoxShow(Me, "Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)

            Catch ex As Exception
                clsCommon.ProgressBarHide()

                myMessages.myExceptions(ex)
            Finally
                clsCommon.ProgressBarHide()
            End Try
        End If
        Me.Controls.Remove(gv)
    End Sub

    Private Sub gv2_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles gv2.CellDoubleClick
        Try
            Dim qry As String
            Dim arrGLMainAccount As ArrayList = Nothing
            If e.Column Is gv2.Columns(ColGLMainAccount) Then
                If clsCommon.myLen(gv2.CurrentRow.Cells(ColMainGroupCode).Value) > 0 AndAlso settSelectGLInBalanceSheetPerforma Then
                    arrGLMainAccount = TryCast(gv2.CurrentRow.Cells(ColGLMainAccount).Tag, ArrayList)
                    qry = "select Main_GL_Account as Code,Main_GL_Account_desc as Name,TSPL_ACCOUNT_MAIN_GL_ACCOUNT.Sub_Group_Code as SubGroupCode, TSPL_ACCOUNT_SUB_GROUPS.Account_Sub_Group_Desc as SubGroupName,TSPL_ACCOUNT_SUB_GROUPS.Account_Group_Code as GroupCode,TSPL_ACCOUNT_GROUPS.Account_Group_Desc as GroupName " + Environment.NewLine + _
                    "from TSPL_ACCOUNT_MAIN_GL_ACCOUNT" + Environment.NewLine + _
                    "left outer join TSPL_ACCOUNT_SUB_GROUPS on TSPL_ACCOUNT_SUB_GROUPS.Account_Sub_Group_Code=TSPL_ACCOUNT_MAIN_GL_ACCOUNT.Sub_Group_Code" + Environment.NewLine + _
                    "left outer join TSPL_ACCOUNT_GROUPS on TSPL_ACCOUNT_GROUPS.Account_Group_Code=TSPL_ACCOUNT_Sub_GROUPS.Account_Group_Code" + Environment.NewLine + _
                    "where TSPL_ACCOUNT_GROUPS.Account_Main_Group_Code in ('" + clsCommon.myCstr(gv2.CurrentRow.Cells(ColMainGroupCode).Value) + "')"
                    arrGLMainAccount = clsCommon.ShowMultipleSelectForm(False, "@BSPglmain", qry, "Code", "Name", arrGLMainAccount, Nothing)
                    gv2.CurrentRow.Cells(ColGLMainAccount).Tag = arrGLMainAccount
                    gv2.CurrentRow.Cells(ColGLMainAccount).Value = clsCommon.GetMulcallStringWithComma(arrGLMainAccount)
                End If
            End If
            arrGLMainAccount = Nothing
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class
