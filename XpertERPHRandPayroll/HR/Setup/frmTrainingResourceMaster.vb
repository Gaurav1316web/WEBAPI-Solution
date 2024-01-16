Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.WinControls
Imports common
Imports XpertERPEngine
Imports Telerik.WinControls.UI

Public Class frmTrainingResourceMaster

    Dim isnewentry As Boolean
    Dim tb_name As String = Nothing
    Dim ButtonToolTip As ToolTip = New ToolTip()

    Function AllowTosave() As Boolean
        If clsCommon.myLen(txt_Code.Value) <= 0 Then
            txt_Code.Focus()
            Throw New Exception("Code cannot be left blank")
        End If
        If clsCommon.myLen(txt_Name.Text) <= 0 Then
            txt_Name.Focus()
            Throw New Exception("Name cannot be left blank")
        End If

        'If txt_Code.Value = "" Then
        '    MessageBox.Show("Code cannot be blank")
        '    Return False
        'ElseIf txt_Name.Text = "" Then

        '    MessageBox.Show("Name cannot be blank")
        '    Return False
        'End If
        Return True
    End Function
    Sub SaveData()
        'Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If AllowTosave() Then
                Dim obj As New clsTrainingMaster

                obj.tb_Name = tb_name

                obj.Code = txt_Code.Value
                obj.Name = txt_Name.Text

                If (clsTrainingMaster.SaveData(obj, isnewentry)) Then

                    clsCommon.MyMessageBoxShow(Me, "Data saved Successfully", Me.Text)
                    LoadData(obj.Code, NavigatorType.Current)
                End If
            End If




        Catch ex As Exception
            'trans.Rollback()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub butnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btnsave.Click
        SaveData()

    End Sub
    Sub DeleteData()

        Try
            If clsCommon.myLen(txt_Code.Value) <= 0 Then
                Throw New Exception("Code not found to delete")
            End If
            If clsCommon.MyMessageBoxShow("Are you sure? Do you want to Delete this Code ('" + txt_Code.Value + "')", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                Dim obj As New clsTrainingMaster
                If obj.DeleteData(txt_Code.Value, tb_name) Then
                    clsCommon.MyMessageBoxShow(Me, "Deleted Successfully", Me.Text)
                    ResetData()
                End If
            End If
        Catch ex As Exception
            If (clsCommon.CompairString(clsCommon.myCstr(ex.Message), "Code not found to delete") <> CompairStringResult.Equal) Then
                clsCommon.MyMessageBoxShow(Me, "Current Code is in use", Me.Text)
            Else
                clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            End If
        End Try


    End Sub
    Sub ResetData()
        txt_Code.Value = ""
        txt_Name.Text = ""
        BtnDelete.Enabled = False
        Btnsave.Text = "Save"
    End Sub
    Private Sub butnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnDelete.Click
        DeleteData()
        ResetData()
    End Sub

    Private Sub butnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnClose.Click
        Me.Close()
    End Sub

    Sub LoadData(ByVal obj As String, ByVal NavigatorType As NavigatorType)
        Btnsave.Text = "Save"
        Dim cls As clsTrainingMaster = clsTrainingMaster.GetData(obj, NavigatorType, tb_name)

        If cls IsNot Nothing Then
            txt_Code.Value = cls.Code
            txt_Name.Text = cls.Name

            Btnsave.Text = "Update"
            BtnDelete.Enabled = True
            isnewentry = False
        End If


    End Sub

    Private Sub txt_Code__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txt_Code._MYNavigator
        LoadData(txt_Code.Value, NavType)
        Btnsave.Text = "Update"
    End Sub

    Private Sub txt_Code__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txt_Code._MYValidating
        Dim qry As String
        If isButtonClicked Then
            qry = "select Code,Name from " & tb_name & ""
            txt_Code.Value = clsCommon.ShowSelectForm("id", qry, "Code", "", txt_Code.Value, " " & tb_name & ".code ", isButtonClicked)
            If clsCommon.myLen(txt_Code.Value) > 0 Then
                LoadData(txt_Code.Value, NavigatorType.Current)

            End If
        End If
    End Sub

    Private Sub btnnew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnnew.Click
        ResetData()
    End Sub

    Private Sub RMImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RMImport.Click
        Dim gv As New RadGridView()
        Dim IsNewEntry As Boolean
        'Dim trans As SqlTransaction
        Me.Controls.Add(gv)
        If transportSql.importExcel(gv, "Code", "Name") Then
            Dim linno As Integer = 1
            Try
                'trans = clsDBFuncationality.GetTransactin()
                'connectSql.OpenConnection()
                For Each grow As GridViewRowInfo In gv.Rows

                    Dim obj As New clsTrainingMaster()
                    Dim strCode As String = clsCommon.myCstr(grow.Cells("Code").Value)
                    If clsCommon.myLen(strCode) <= 0 Then
                        Throw New Exception("Code should not be left blank" + clsCommon.myCstr(linno) + ".")
                    End If

                    Dim strName As String = clsCommon.myCstr(grow.Cells("Name").Value)
                    If clsCommon.myLen(strName) <= 0 Then
                        Throw New Exception("Name should not be left blank" + clsCommon.myCstr(linno) + ".")
                    End If
                    obj.Code = strCode
                    obj.Name = strName
                    obj.tb_Name = tb_name
                    clsTrainingMaster.SaveData(obj, IsNewEntry)
                    linno += 1
                Next
                'trans.Commit()
                common.clsCommon.MyMessageBoxShow(Me, "Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                'trans.Rollback()
                myMessages.myExceptions(ex)
            End Try
        End If
        Me.Controls.Remove(gv)
    End Sub

    Private Sub RMExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RMExport.Click
        Dim str As String
        str = "select Code as [Code],Name As [Name]  from " & tb_name & " "
        transportSql.ExporttoExcel(str, Me)
    End Sub

    Private Sub frmHRResourceMaster_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso Btnsave.Enabled Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso BtnDelete.Enabled Then

            DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Close()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            ResetData()
        End If
    End Sub

    Private Sub frmHRResourceMaster_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            ';If MyBase.Text = "Training Master" Then
            tb_name = "Tspl_Training_Resource_Master"
            'End If
            SetUserMgmtNew()
            isnewentry = True
            ButtonToolTip.SetToolTip(Btnsave, "Press Alt+S for Save/Update ")
            ButtonToolTip.SetToolTip(BtnDelete, "Press Alt+D  for Delete ")
            ButtonToolTip.SetToolTip(BtnClose, "Press Alt+C Close the Window")
            ButtonToolTip.SetToolTip(btnnew, "Press Alt+N Adding New ")
            ResetData()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmSourceTypeMaster)
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow(Me, "Permission Denied", Me.Text)
            Me.Close()
            Exit Sub
        End If
        Btnsave.Visible = MyBase.isModifyFlag
        BtnDelete.Visible = MyBase.isDeleteFlag
    End Sub


End Class
