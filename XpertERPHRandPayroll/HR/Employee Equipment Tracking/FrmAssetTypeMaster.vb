'Created By Anand @BM00000001154
'Created On Date-15/11/2013
'Teable Created TSPL_Asset_Type_Master
'Class Created-ClsAssetType
Imports common
Imports System.Data.SqlClient
Imports XpertERPEngine
Public Class FrmAssetTypeMaster
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private isNewEntry As Boolean = False
    Public Code As String

    Private Sub FrmAssetTypeMaster_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        LoadData(Code, NavigatorType.Current)

        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D Delete ")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
    End Sub
    Public Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.AssetTypeMain)
        'If Not (MyBase.isReadFlag) Then
        '    common.clsCommon.MyMessageBoxShow("Permission Denied")
        '    Me.Close()
        '    Exit Function
        'End If
        'btnsave.Visible = MyBase.isModifyFlag
        'btndelete.Visible = MyBase.isDeleteFlag
    End Sub
    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        'AddNew()
        Dim obj As ClsAssetType = ClsAssetType.GetData(strCode, NavTyep)
        If obj IsNot Nothing Then
            isNewEntry = False
            txtAssetCode.Value = obj.Asset_Type_Code
            txtDescription.Text = obj.Asset_Type_Description
            ddlAssetType.Text = obj.Asset_Type
            txtAssetCode.MyReadOnly = True
            Me.btnSave.Text = "Update"
        Else
            AddNew()
        End If
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        SaveData()
    End Sub
    Sub SaveData()
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If AllowToSave() Then
                Dim obj As New ClsAssetType()
                obj.Asset_Type_Code = txtAssetCode.Value
                obj.Asset_Type_Description = txtDescription.Text
                obj.Asset_Type = ddlAssetType.Text


                Dim qry As Integer = clsDBFuncationality.getSingleValue("select count(Asset_Type_Code) from TSPL_Asset_Type_Master where Asset_Type_Code='" + obj.Asset_Type_Code + "'", trans)
                If (qry = 0) Then
                    isNewEntry = True
                Else
                    isNewEntry = False

                End If
                If (ClsAssetType.SaveData(obj, isNewEntry, trans)) Then
                    trans.Commit()
                    clsCommon.MyMessageBoxShow("Data saved Successfully", Me.Text)
                    LoadData(obj.Asset_Type_Code, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            trans.Rollback()
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub
    Private Function AllowToSave() As Boolean
        If clsCommon.myLen(clsCommon.myCstr(txtAssetCode.Value)) <= 0 Then
            txtAssetCode.Focus()
            Throw New Exception("Please Fill Code")
        End If

        If clsCommon.myLen(clsCommon.myCstr(txtDescription.Text)) <= 0 Then
            txtDescription.Focus()
            Throw New Exception("Please Fill  Name")
        End If

        If clsCommon.myLen(clsCommon.myCstr(ddlAssetType.Text)) <= 0 Then
            ddlAssetType.Focus()
            Throw New Exception("Please Fill Asset Type")
        End If
        Return True
    End Function

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        DeleteData()
    End Sub
    Private Sub DeleteData()
        Try
            If clsCommon.myLen(txtAssetCode.Value) <= 0 Then
                Throw New Exception("  Code not found to delete")

            End If
            If clsCommon.MyMessageBoxShow("Do you want to delete Code '" + txtAssetCode.Value + "'", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then

                Dim qry As String = "delete from TSPL_Asset_Type_Master where Asset_Type_Code='" + txtAssetCode.Value + "'"
                clsDBFuncationality.ExecuteNonQuery(qry)
                clsCommon.MyMessageBoxShow("Successfully Deleted", Me.Text)
                AddNew()
            End If
        Catch ex As Exception
            If (clsCommon.CompairString(clsCommon.myCstr(ex.Message), " Code not found to delete") <> CompairStringResult.Equal) Then
                clsCommon.MyMessageBoxShow("Current  Code is in use")

            Else
                clsCommon.MyMessageBoxShow(ex.Message)
            End If
        End Try
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub txtAssetCode__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtAssetCode._MYNavigator
        LoadData(txtAssetCode.Value, NavType)
    End Sub

    Private Sub txtAssetCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtAssetCode._MYValidating
        If txtAssetCode.MyReadOnly OrElse isButtonClicked Then
            Dim qry As String = "select Asset_Type_Code as Code,Asset_Type_Description as Description ,Asset_Type  from  TSPL_Asset_Type_Master"
            Dim whrClas As String = " Comp_Code='" + objCommonVar.CurrentCompanyCode + "' "

            txtAssetCode.Value = clsCommon.ShowSelectForm("ASST_Type", qry, "Code", whrClas, txtAssetCode.Value, "", isButtonClicked)
            LoadData(txtAssetCode.Value, NavigatorType.Current)
        End If
    End Sub

    Private Sub btnAddNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddNew.Click
        AddNew()
    End Sub
    Sub AddNew()
        txtAssetCode.Value = ""
        txtDescription.Text = ""
        ddlAssetType.Text = ""

        txtAssetCode.MyReadOnly = False

    End Sub

    Private Sub FrmAssetTypeMaster_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag Then
            DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.A Then
            AddNew()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        End If
    End Sub

    Private Sub SplitContainer1_Panel1_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles SplitContainer1.Panel1.Paint

    End Sub
End Class
