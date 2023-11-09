'--Developed By -Pankaj Kumar Chaudhary
'--Database - TSPLERP
'--Table - tspl_Item_Sub_Category,tspl_Item_Category
'--Start Date -19/10/2011
'--End Date -
Imports common
Imports System.Data.SqlClient

Public Class FrmItemSubCategory
    Inherits FrmMainTranScreen
#Region "Variables"
    Dim Qry As String
    Dim dt As DataTable
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private isNewEntry As Boolean = False
    Private isInsideLoadData As Boolean = False
    Dim UserCode, CompanyCode As String
#End Region

    Public Sub New(ByVal user As String, ByVal company As String)
        InitializeComponent()
        userCode = user
        companyCode = company
    End Sub
#Region "Events"
    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.mbtnItemSubCategory)
        If Not (MyBase.isReadFlag) Then
            '--------richa Ticket no. BM00000003121 15/07/2014 
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag
        '--------richa Ticket no. BM00000003014 02/07/2014 to enable/disable import/export option acc. to user mgmt setting -----------
        If btnSave.Visible = True Then
            RadMenuItemImport.Enabled = True
            RadMenuItemExport1.Enabled = True
        Else
            RadMenuItemImport.Enabled = False
            RadMenuItemExport1.Enabled = False
        End If
        '--------------------------------------------------
        'btnPost.Visible = MyBase.isPostFlag
        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub
    Private Sub FrmItemSubCategory_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        'If Not clsCommon.CompairString(objCommonVar.CurrentUserCode, "ADMIN") = CompairStringResult.Equal Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If
        'TxtCategoryCode.Value.CharacterCasing = CharacterCasing.Upper
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnAddNew, "Press Alt+N Adding New Trasnaction")
        TxtSubCategoryCode.MyReadOnly = True
        TxtSubCategoryCode.MyMaxLength = 30     ''''Validates The Length Of Finder(txtSubCategoryCode)
        tbDescription.MaxLength = 200           ''''Validates The Length Of TextBox(tbDescription)
        txtCategory.MyReadOnly = True
        lblDescription.BorderVisible = True
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

    Private Sub TxtSubCategoryCode__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles TxtSubCategoryCode._MYNavigator
        Try
            LoadData(TxtSubCategoryCode.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message)
        End Try
    End Sub

    ''''Finder(txtSubCategoryCode)
    Private Sub TxtSubCategoryCode_MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles TxtSubCategoryCode._MYValidating
        Dim str As String = "select count(*) from TSPL_Item_Sub_Category where Sub_Category_Code ='" + TxtSubCategoryCode.Value + "' "
        Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
        If no = 0 Then
            TxtSubCategoryCode.MyReadOnly = False
        Else
            TxtSubCategoryCode.MyReadOnly = True
        End If
        If TxtSubCategoryCode.MyReadOnly OrElse isButtonClicked Then
            Dim qry As String = "select Sub_Category_Code as [SubCategoryCode],Category_Code as [CategoryCode], Description as [Description]  from TSPL_Item_Sub_Category "
            TxtSubCategoryCode.Value = clsCommon.ShowSelectForm("SubCatSelector", qry, "SubCategoryCode", "", TxtSubCategoryCode.Value, "SubCategoryCode", isButtonClicked)
            'FillData()
            LoadData(TxtSubCategoryCode.Value, NavigatorType.Current)
        End If
    End Sub

    Public Sub FillData()
        txtCategory.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Category_Code from tspl_Item_Sub_category where Sub_category_code='" + TxtSubCategoryCode.Value + "'"))
        tbDescription.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description from tspl_Item_Sub_category where Sub_category_code='" + TxtSubCategoryCode.Value + "'"))
        lblDescription.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Category_Name from tspl_Item_Category Where TSPL_ITEM_CATEGORY.Category_Code='" + txtCategory.Value + "'"))
        btnSave.Text = "Update"
        TxtSubCategoryCode.MyReadOnly = True
    End Sub

    ''''Finder(txtCategory) 
    Private Sub txtCategory__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtCategory._MYValidating
        Dim qry As String = "select Category_Code as [Code],Category_Name as [Description] from TSPL_Item_Category"
        txtCategory.Value = clsCommon.ShowSelectForm("Category Code", qry, "Code", "", txtCategory.Value, "Code", isButtonClicked)
        lblDescription.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Category_Name from TSPL_Item_Category where Category_Code='" + txtCategory.Value + "'"))
    End Sub

    ''''Validates The Finder(Sub Category Code)
    Private Sub TxtSubCategoryCode_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TxtSubCategoryCode.KeyPress
        If (e.KeyChar = Chr(39)) Then
            e.Handled = True
        End If

    End Sub

    Private Sub RadMenuItemImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItemImport.Click
        Import()
    End Sub

    Private Sub RadMenuItemExport1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItemExport1.Click
        Export()
    End Sub

    Private Sub FrmItemSubCategory1_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
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

#End Region

#Region "Methods"

    Sub BlankAllControls()
        TxtSubCategoryCode.Value = Nothing
        tbDescription.Text = ""
        txtCategory.Value = ""
        lblDescription.Text = ""
    End Sub

    Sub AddNew()
        BlankAllControls()
        TxtSubCategoryCode.Focus()
        TxtSubCategoryCode.MyReadOnly = False
        isNewEntry = True
        btnSave.Text = "Save"
        btnSave.Enabled = True
        'btnPost.Enabled = True
        btnDelete.Enabled = True

    End Sub

    Function AllowToSave() As Boolean
        If (IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowAutoGenerateDocNoInMaster, clsFixedParameterCode.AllowAutoGenerateDocNoInMaster, Nothing)) = "1", True, False) = False) Then
            If clsCommon.myLen(TxtSubCategoryCode.Value) <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Please Enter Sub Category Code")
                TxtSubCategoryCode.Focus()
                Return False
            End If
        End If

        If clsCommon.myLen(txtCategory.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "Please Select Category Code")
            txtCategory.Focus()
            Return False
        End If

        Dim dt As DataTable = clsDBFuncationality.GetDataTable("Select * From TSPL_ITEM_SUB_CATEGORY Where Sub_Category_Code='" + TxtSubCategoryCode.Value + "'")
        If dt.Rows.Count > 0 And isNewEntry Then
            common.clsCommon.MyMessageBoxShow(Me, "This Code has been already added")
            TxtSubCategoryCode.Focus()
            Return False
        End If

        Return True
    End Function

    Sub SaveData()
        Try
            If (AllowToSave()) Then

                If MyBase.isModifyonPasswordFlag Then
                    If clsPasswordCheckForMasters.CheckMasterPwd(clsUserMgtCode.mbtnItemSubCategory, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
                    Else
                        Return
                    End If
                End If
                Dim obj As New clsItemSubCategory()
                obj.Sub_Category_Code = (TxtSubCategoryCode.Value).ToString
                obj.Description = tbDescription.Text
                obj.Category_Code = txtCategory.Value
                If (obj.SaveData(obj, isNewEntry)) And btnSave.Text = "Save" Then
                    common.clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully")
                    LoadData(obj.Sub_Category_Code, NavigatorType.Current)
                Else
                    common.clsCommon.MyMessageBoxShow(Me, "Record Updated Successfully")
                    LoadData(obj.Sub_Category_Code, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message)
        End Try
    End Sub

    Sub DeleteData()
        Try
            If (myMessages.deleteConfirm()) Then
                If (clsItemSubCategory.DeleteData(TxtSubCategoryCode.Value)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ")
                    AddNew()
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message)
        End Try
    End Sub

    Sub LoadData(ByVal Sub_Category_Code As String, ByVal navType As common.NavigatorType)
        Try
            btnSave.Enabled = True
            'btnPost.Enabled = True
            btnDelete.Enabled = True
            isInsideLoadData = True
            isNewEntry = False
            btnSave.Text = "Update"
            BlankAllControls()
            TxtSubCategoryCode.MyReadOnly = True

            Dim obj As New clsItemSubCategory()
            obj = clsItemSubCategory.GetData(Sub_Category_Code, navType)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Category_Code) > 0) Then
                TxtSubCategoryCode.Value = obj.Sub_Category_Code
                tbDescription.Text = obj.Description
                txtCategory.Value = obj.Category_Code
                lblDescription.Text = obj.Category_Name
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message)
        Finally
            isInsideLoadData = False
        End Try
    End Sub

    Sub CloseForm()
        Me.Close()
    End Sub

#End Region

#Region "Import/Export"
    Public Sub Import()
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim currentdate As Date = Date.Today
        If transportSql.importExcel(gv, "Sub Category Code", "Category Code", "Description") Then
            Dim trans As SqlTransaction = Nothing
            Try
                'connectSql.OpenConnection()
                trans = clsDBFuncationality.GetTransactin()
                clsCommon.ProgressBarShow()
                For Each grow As GridViewRowInfo In gv.Rows
                    Dim LineNo As String = clsCommon.myCstr(grow.Index + 2)
                    Dim strSubCategoryCode As String = clsCommon.myCstr(grow.Cells(0).Value)
                    Dim strCategoryCode As String = clsCommon.myCstr(grow.Cells(1).Value)
                    Dim strDescription As String = clsCommon.myCstr(grow.Cells(2).Value)

                    If (String.IsNullOrEmpty(strDescription)) Or clsCommon.myLen(strSubCategoryCode) > 30 Then
                        Throw New Exception("Sub Category Code can not be blank or incorrect at Line No '" + LineNo + "'")
                    End If

                    Dim categoryCode As String
                    If clsCommon.myLen(strCategoryCode) > 0 Then
                        categoryCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Category_Code from TSPL_Item_Category Where Category_Code='" + strCategoryCode + "'", trans))
                        If Not clsCommon.CompairString(strCategoryCode, categoryCode) = CompairStringResult.Equal Then
                            Throw New Exception("Category Code '" + strCategoryCode + "' at Line No '" + LineNo + "' Does Not Exist In Master")
                        End If
                    Else
                        Throw New Exception(" Category Code can not be blank at Line No '" + LineNo + "'")
                    End If

                    If (String.IsNullOrEmpty(strDescription)) Or clsCommon.myLen(strDescription) > 200 Then
                        Throw New Exception(" Description can not be blank or incorrect At Line No '" + LineNo + "'")
                    End If

                    Dim sql1 As String = "select count(*) from TSPL_Item_Sub_Category where Sub_Category_Code='" + strSubCategoryCode + "'"
                    Dim i As Integer = CInt(clsDBFuncationality.getSingleValue(sql1, trans))
                    If (i = 0) Then
                        Qry = "INSERT Into TSPL_Item_Sub_Category values('" + strSubCategoryCode + "','" + strCategoryCode + "', '" + strDescription + "','" + UserCode + "','" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy") + "','" + UserCode + "','" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy") + "','" + CompanyCode + "')"
                        clsDBFuncationality.ExecuteNonQuery(Qry, trans)
                        'connectSql.RunSqlTransaction(trans, Qry)
                        'connectSql.RunSpTransaction(trans, "sp_DesignationMaster_insert", New SqlParameter("@Category Code", strCtgryCode), New SqlParameter("@Category Name", strCtgryName), New SqlParameter("@createdby", userCode), New SqlParameter("@createddate", connectSql.serverDate()), New SqlParameter("@modifiedby", userCode), New SqlParameter("@modifieddate", connectSql.serverDate()), New SqlParameter("@compcode", companyCode))
                    Else
                        Qry = "UPDATE TSPL_Item_Sub_Category set Sub_Category_Code='" + strSubCategoryCode + "', Category_Code='" + strCategoryCode + "', Description='" + strDescription + "', Modify_By='" + UserCode + "', Modify_Date='" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy") + "', Comp_Code='" + CompanyCode + "' WHERE Sub_Category_Code='" + strSubCategoryCode + "'"
                        clsDBFuncationality.ExecuteNonQuery(Qry, trans)
                        'connectSql.RunSqlTransaction(trans, Qry)
                        'connectSql.RunSpTransaction(trans, "sp_DesignationMaster_update", New SqlParameter("@CAtegory Code", strCtgryCode), New SqlParameter("@Category Name", strCtgryName), New SqlParameter("@modifiedby", userCode), New SqlParameter("@modifieddate", connectSql.serverDate()), New SqlParameter("@compcode", companyCode))

                    End If
                Next
                trans.Commit()
                clsCommon.ProgressBarHide()
                common.clsCommon.MyMessageBoxShow(Me, "Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
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
        str = "select TSPL_ITEM_SUB_CATEGORY.Sub_Category_Code As [Sub Category Code],TSPL_ITEM_SUB_CATEGORY.Category_Code as [Category Code], TSPL_ITEM_SUB_CATEGORY.Description as [Description] from TSPL_Item_Sub_Category"
        transportSql.ExporttoExcel(str, Me)
    End Sub
#End Region
    

    'Private Function funSetUserAccess() As Boolean
    '    Try
    '        btnSave.Visible = True
    '        btnDelete.Visible = True
    '        'btnPost.Visible = True

    '        Dim strRights As String
    '        Dim strTemp() As String
    '        Dim strProgCode = "ITM-SUB-CAT"
    '        strRights = enuUserRights.enuRead & "," & enuUserRights.enuModify & "," & enuUserRights.enuDelete & "," & enuUserRights.enuAuthorised
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
    '            btnSave.Visible = False
    '        End If
    '        If strTemp(2) = "0" Then 'Grant modify access
    '            btnDelete.Visible = False
    '        End If
    '        If strTemp(3) = "0" Then 'Grant Authorize access
    '            'btnPost.Visible = False
    '        End If
    '        funSetUserAccess = True
    '    Catch er As Exception

    '    End Try
    'End Function

    Private Sub RadMenuItemExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItemExit.Click
        CloseForm()
    End Sub
    
End Class
