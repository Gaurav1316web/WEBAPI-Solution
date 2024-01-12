Imports common
Imports System.Data.SqlClient
Imports XpertERPEngine
Public Class FrmAssetSubCategoryMaster
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private isNewEntry As Boolean = False
    Dim qry As String
    Public Code As String

    Private Sub FrmAssetTypeMaster_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        LoadData(Code, NavigatorType.Current)

        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D Delete ")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
    End Sub
    Public Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.AssetSubCategory)
        'If Not (MyBase.isReadFlag) Then
        '    common.clsCommon.MyMessageBoxShow("Permission Denied")
        '    Me.Close()
        '    Exit Function
        'End If
        'btnSave.Visible = MyBase.isModifyFlag
        'btnDelete.Visible = MyBase.isDeleteFlag
    End Sub
    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        'AddNew()
        Dim obj As ClsAssetSubCategory = ClsAssetSubCategory.GetData(strCode, NavTyep)
        If obj IsNot Nothing Then
            isNewEntry = False
            txtCode.Value = obj.Code
            txtDescription.Text = obj.Description
            txtCategory.Value = obj.Category
            txtCode.MyReadOnly = True
            Me.btnSave.Text = "Update"
        Else
            AddNew()
        End If
    End Sub
    Sub AddNew()
        txtCode.Value = ""
        txtDescription.Text = ""
        txtCategory.Value = ""


    End Sub
    Sub SaveData()
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If AllowToSave() Then
                Dim obj As New ClsAssetSubCategory()
                obj.Code = txtCode.Value
                obj.Description = txtDescription.Text
                obj.Category = txtCategory.Value
                Dim qry As Integer = clsDBFuncationality.getSingleValue("select count(Code) from TSPL_Asset_SubCategory_Master where Code='" + obj.Code + "'", trans)
                If (qry = 0) Then
                    isNewEntry = True
                Else
                    isNewEntry = False

                End If
                If (ClsAssetSubCategory.SaveData(obj, isNewEntry, trans)) Then
                    trans.Commit()
                    clsCommon.MyMessageBoxShow(Me, "Data saved Successfully", Me.Text)
                    LoadData(obj.Code, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            trans.Rollback()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Function AllowToSave() As Boolean
        If clsCommon.myLen(clsCommon.myCstr(txtDescription.Text)) <= 0 Then
            txtDescription.Focus()
            Throw New Exception("Please Fill  Description")
        End If

        If clsCommon.myLen(clsCommon.myCstr(txtCategory.Value)) <= 0 Then
            txtCategory.Focus()
            Throw New Exception("Please Fill Asset Category")
        End If
        Return True
    End Function

    Private Sub DeleteData()
        Try
            If clsCommon.myLen(txtCode.Value) <= 0 Then
                Throw New Exception("  Code not found to delete")

            End If
            If clsCommon.MyMessageBoxShow(Me, "Do you want to delete Code '" + txtCode.Value + "'", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then

                Dim qry As String = "delete from TSPL_Asset_SubCategory_Master where Code='" + txtCode.Value + "'"
                clsDBFuncationality.ExecuteNonQuery(qry)
                clsCommon.MyMessageBoxShow(Me, "Successfully Deleted", Me.Text)
                AddNew()
            End If
        Catch ex As Exception
            If (clsCommon.CompairString(clsCommon.myCstr(ex.Message), " Code not found to delete") <> CompairStringResult.Equal) Then
                clsCommon.MyMessageBoxShow(Me, "Current  Code is in use", Me.Text)

            Else
                clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            End If
        End Try
    End Sub



    Private Sub txtCode__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtCode._MYNavigator
        LoadData(txtCode.Value, NavType)
    End Sub

    Private Sub txtCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtCode._MYValidating
        If txtCode.MyReadOnly OrElse isButtonClicked Then
            Dim qry As String = "select Code ,Description ,Category  from  TSPL_Asset_SubCategory_Master"
            Dim whrClas As String = " CompCode='" + objCommonVar.CurrentCompanyCode + "' "

            txtCode.Value = clsCommon.ShowSelectForm("ASST_SUBCATEGORY", qry, "Code", whrClas, txtCode.Value, "", isButtonClicked)
            LoadData(txtCode.Value, NavigatorType.Current)
        End If
    End Sub

    Private Sub txtCategory__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtCategory._MYValidating
        qry = "select Asset_Type_Code as code,Asset_Type_Description  as Description,Asset_Type  as AssetType from TSPL_Asset_Type_Master "
        Dim whrClas As String = " Comp_Code='" + objCommonVar.CurrentCompanyCode + "' "
        txtCategory.Value = clsCommon.ShowSelectForm("Asset_Type", qry, "Code", whrClas, txtCategory.Value, "", isButtonClicked)
    End Sub

    Private Sub RadButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        SaveData()
    End Sub

    Private Sub btnDelete_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        DeleteData()
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
End Class
