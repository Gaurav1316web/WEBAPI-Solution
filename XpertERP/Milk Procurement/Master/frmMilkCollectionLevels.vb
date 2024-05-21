Imports common
Imports System.Data.SqlClient

Public Class frmMilkCollectionLevels
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Public Code As String
    Public CreateNewTransaction As Boolean = False
    Public isNewEntry As Boolean = True

    Private Sub FrmSourceTaskMaster_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")

        If clsCommon.myLen(Code) > 0 Then
            LoadData(Code, NavigatorType.Current)
        End If
        If CreateNewTransaction Then
            AddNew()
        End If
    End Sub
    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.frmMilkCollectionLevels)
        'If Not (MyBase.isReadFlag) Then
        '    common.clsCommon.MyMessageBoxShow("Permission Denied")
        '    Me.Close()
        '    Exit Sub
        'End If
        'btnsave.Visible = MyBase.isModifyFlag
        'btndelete.Visible = MyBase.isDeleteFlag
    End Sub


    Private Sub AddNew()
        isNewEntry = True
        txtCode.Value = ""
        txtAccdescription.Text = ""
        txtParentCode.Value = Nothing
        lblHigherLevelDesgName.Text = ""

    End Sub

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        AddNew()

       
        Dim obj As clsMilkCollectionLevels = clsMilkCollectionLevels.GetData(strCode, NavTyep)
        If obj IsNot Nothing Then
            isNewEntry = False
            txtCode.Value = obj.LEVEL_CODE
            txtAccdescription.Text = obj.DESCRIPTION
            txtParentCode.Value = obj.PARENT_LEVEL_CODE
            lblHigherLevelDesgName.Text = obj.PARENT_LEVEL_CODE_Desc


        End If
    End Sub
    Sub DeleteData()
        Try
            If clsCommon.myLen(txtCode.Value) <= 0 Then
                Throw New Exception("User Code not found to delete")
            End If
            If clsCommon.MyMessageBoxShow("Delete the Current User." + Environment.NewLine + "Are you sure", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                Dim qry As String = "delete from TSPL_MilkCollectionLevels where LEVEL_CODE='" + txtCode.Value + "'"
                clsDBFuncationality.ExecuteNonQuery(qry)
                clsCommon.MyMessageBoxShow(Me, "Successfully Deleted", Me.Text)
                AddNew()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtCode__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtCode._MYNavigator
        LoadData(txtCode.Value, NavType)
    End Sub

    Private Sub txtCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtCode._MYValidating
        If txtCode.MyReadOnly OrElse isButtonClicked Then
            Dim qry As String = "select LEVEL_CODE as Code,Description as Name, Parent_Level_Code as 'Parent Level Code' from TSPL_MilkCollectionLevels  "
            LoadData(clsCommon.ShowSelectForm("TSPL_MilkCollectionLevels", qry, "Code", "", txtCode.Value, "Code", isButtonClicked, "TSPL_MilkCollectionLevels.Created_Date"), NavigatorType.Current)
        End If
      
    End Sub


    Private Sub btnAddNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddNew.Click
        AddNew()
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        SaveData()
    End Sub
    Sub SaveData()
        Try
            If AllowToSave() Then
                Dim obj As New clsMilkCollectionLevels()
                obj.LEVEL_CODE = clsCommon.myCstr(txtCode.Value)
                obj.DESCRIPTION = txtAccdescription.Text
                obj.PARENT_LEVEL_CODE = clsCommon.myCstr(txtParentCode.Value)


                Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
                Try
                    If clsMilkCollectionLevels.SaveData(obj, isNewEntry, trans) Then
                        trans.Commit()
                        clsCommon.MyMessageBoxShow(Me, "Data saved successfully", Me.Text)
                        LoadData(obj.LEVEL_CODE, NavigatorType.Current)
                    End If
                Catch ex As Exception
                    trans.Rollback()
                    Throw New Exception(ex.Message)
                End Try

            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Function AllowToSave() As Boolean

        If (clsCommon.myLen(txtCode.Value) <= 0) Then
            clsCommon.MyMessageBoxShow(Me, "Please select Code", Me.Text)
            Return False
        End If
        If (clsCommon.myLen(txtAccdescription.Text) <= 0) Then
            clsCommon.MyMessageBoxShow(Me, "Description is blank.", Me.Text)
            Return False
        End If

        Return True
    End Function

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        DeleteData()
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        CloseForm()
    End Sub
    Sub CloseForm()
        Me.Close()
    End Sub


    Private Sub FrmSourceTaskMaster_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnSave.Enabled Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btnDelete.Enabled Then
            DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnClose.Enabled Then
            CloseForm()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnAddNew.Enabled Then
            AddNew()
        End If
    End Sub

    Private Sub RadButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUser.Click
        Dim frm As New frmMilkCollectionLevels()
        frm.Show()
    End Sub

    Private Sub txtHigherLevelDesg__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtParentCode._MYValidating

        Dim qry As String = "select LEVEL_CODE as Code,Description  from TSPL_MilkCollectionLevels "
        Dim wherclause As String = "" '" LEVEL=(select MAX(level) from TSPL_Hierarchy_Master where cast(substring(Level,6,2) as integer)<substring('" & Me.cboLevel.SelectedValue.ToString & "',6,2))"
        txtParentCode.Value = clsCommon.ShowSelectForm("TSPL_MilkCollectionLevels2", qry, "Code", wherclause, txtParentCode.Value, "", isButtonClicked)

        qry = "select Description  from TSPL_MilkCollectionLevels  where LEVEL_CODE  ='" + txtCode.Value + "'"
        lblHigherLevelDesgName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
    End Sub
End Class
