Public Class TypeOfItemMaster
    Inherits FrmMainTranScreen
#Region "Variables"
    Dim isNewEntry As Boolean = False
#End Region
    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnDelete.Visible = MyBase.isDeleteFlag

    End Sub
    Private Sub TypeOfItemMaster_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        AddNew()
    End Sub

    Private Sub TypeOfItemMaster_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnSave.Enabled Then
            btnSave.PerformClick()
        End If
    End Sub

    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        AddNew()
    End Sub
    Private Sub AddNew()
        isNewEntry = True
        txtCode.Value = ""
        txtName.Text = ""
    End Sub
    Private Function AllowToSave() As Boolean
        Try
            If clsCommon.myLen(txtCode.Value) <= 0 Then
                Throw New Exception("Please Enter Code")
            End If
            If clsCommon.myLen(txtName.Text) <= 0 Then
                Throw New Exception("Please Enter Name")
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            If AllowToSave() Then
                Dim Obj As clsTypeofItem = New clsTypeofItem
                Obj.Code = txtCode.Value
                Obj.Name = txtName.Text
                If Obj.SaveData(Obj, isNewEntry) Then
                    clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully")
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadButton1_Click(sender As Object, e As EventArgs) Handles RadButton1.Click
        Me.Close()
    End Sub

    Private Sub txtCode__MYNavigator(sender As Object, e As EventArgs, NavType As NavigatorType) Handles txtCode._MYNavigator
        LoadData(txtCode.Value, NavType)
    End Sub

    Private Sub txtCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtCode._MYValidating
        Try
            Dim str As String = "select count(*) from TSPL_Type_Of_Item "
            Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
            If no = 0 Then
                txtCode.MyReadOnly = False
            Else
                txtCode.MyReadOnly = True
            End If
            Dim qry As String = "select Code as Code,Name from TSPL_Type_Of_Item "

            LoadData(clsCommon.ShowSelectForm("Type_Ofitemfnd", qry, "Code", "", txtCode.Value, "Code", isButtonClicked), NavigatorType.Current)

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub LoadData(ByVal strCode As String, ByVal NavType As NavigatorType)
        Try
            Dim obj As clsTypeofItem = clsTypeofItem.GetData(strCode, NavType)
            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Code) > 0 Then
                AddNew()
                isNewEntry = False
                txtCode.Value = obj.Code
                txtName.Text = obj.Name
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Try
            If (myMessages.deleteConfirm()) Then
                clsTypeofItem.DeleteData(txtCode.Value)
                common.clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ", Me.Text)
                AddNew()
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class