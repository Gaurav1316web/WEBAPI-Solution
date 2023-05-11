' ----------------- Created By Anubhooti On 05-Aug-2014 Against -------------------- '
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Data.SqlClient
Imports Telerik.WinControls.UI
Imports Telerik.WinControls
Imports Telerik.WinControls.Data
Imports System.Text.RegularExpressions
Imports common
Imports XpertERPEngine


Public Class FrmQualificationMaster
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private isNewEntry As Boolean = False
    Dim userCode, companyCode As String
    Dim Errorcontrol As clsErrorControl = New clsErrorControl()


#Region "Functions"
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmQualificationMaster)
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow("Permission Denied")
            Me.Close()
            Exit Sub
        End If
        btnsave.Visible = MyBase.isModifyFlag
        btndelete.Visible = MyBase.isDeleteFlag
    End Sub

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Dim obj As ClsQualificationMaster = ClsQualificationMaster.GetData(strCode, NavTyep)
        If obj IsNot Nothing Then
            AddNew()
            isNewEntry = False
            txtcode.Value = obj.Qualification_Code
            txtdesp.Text = obj.Qualification_Name
            txtcode.MyReadOnly = True
            btnsave.Text = "Update"
            btndelete.Enabled = True
        End If
    End Sub

    Sub SaveData()
        'Dim trans As SqlTransaction = Nothing
        Try
            If AllowToSave() Then
                'trans = clsDBFuncationality.GetTransactin
                Dim obj As New ClsQualificationMaster()
                obj.Qualification_Code = txtcode.Value
                obj.Qualification_Name = txtdesp.Text
                Dim qry As Integer = clsDBFuncationality.getSingleValue("select count(Qualification_Code) from TSPL_HR_QUALIFICATION_MASTER where Qualification_Code='" + obj.Qualification_Code + "'")
                If (qry = 0) Then
                    isNewEntry = True
                Else
                    isNewEntry = False
                End If
                If (ClsQualificationMaster.SaveData(obj, isNewEntry)) Then
                    'trans.Commit()
                    clsCommon.MyMessageBoxShow("Data saved Successfully", Me.Text)
                    LoadData(obj.Qualification_Code, NavigatorType.Current)
                    btnsave.Text = "Update"
                    btndelete.Enabled = True
                Else
                    btnsave.Text = "Save"
                    btndelete.Enabled = False
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
            'trans.Rollback()
        End Try
    End Sub

    Private Function AllowToSave() As Boolean
        Try
            If clsCommon.myLen(clsCommon.myCstr(txtcode.Value)) <= 0 Or clsCommon.myLen(clsCommon.myCstr(txtcode.Value)) > 30 Then
                myMessages.blankValue("Code")

                txtcode.Focus()
                txtcode.Select()
                Errorcontrol.SetError(txtcode, "Code")
                Return False
            Else
                Errorcontrol.ResetError(txtcode)
            End If
            If clsCommon.myLen(clsCommon.myCstr(txtdesp.Text)) <= 0 Or clsCommon.myLen(clsCommon.myCstr(txtdesp.Text)) > 150 Then
                myMessages.blankValue("Description")

                txtdesp.Focus()
                txtdesp.Select()
                Errorcontrol.SetError(txtdesp, "Description")
                Return False
            Else
                Errorcontrol.ResetError(txtdesp)
            End If
            Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
        Return True
    End Function
    Private Sub DeleteData()
        Try
            If clsCommon.myLen(txtCode.Value) <= 0 Then
                Throw New Exception("Code not found to delete")
            End If
            If clsCommon.MyMessageBoxShow("are you sure? do you want to delete this Code ('" + txtcode.Value + "')", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then

                Dim qry As String = "DELETE FROM TSPL_HR_QUALIFICATION_MASTER WHERE Qualification_Code='" + txtcode.Value + "'"
                clsDBFuncationality.ExecuteNonQuery(qry)
                clsCommon.MyMessageBoxShow("Deleted Successfully", Me.Text)
                AddNew()
            End If
        Catch ex As Exception
            If (clsCommon.CompairString(clsCommon.myCstr(ex.Message), "Code not found to delete") <> CompairStringResult.Equal) Then
                clsCommon.MyMessageBoxShow("Current Code is in use")
            Else
                clsCommon.MyMessageBoxShow(ex.Message)
            End If
        End Try
    End Sub
    Sub AddNew()
        txtCode.Value = ""
        txtdesp.Text = ""
        txtcode.MyReadOnly = False
        txtcode.Focus()
        btnsave.Text = "Save"
        btndelete.Enabled = False
    End Sub
#End Region

    
    Private Sub FrmQualificationMaster_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnsave.Enabled Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btndelete.Enabled Then
            DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Close()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            AddNew()
        End If
    End Sub

    Private Sub FrmQualificationMaster_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        SetUserMgmtNew()
        isNewEntry = True
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D  for Delete ")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnnew, "Press Alt+N Adding New ")
        AddNew()
    End Sub

    Private Sub btnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btndelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btndelete.Click
        DeleteData()
    End Sub

    Private Sub btnnew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnnew.Click
        AddNew()
    End Sub

    Private Sub btnsave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsave.Click
        SaveData()
    End Sub

    Private Sub txtcode__MYNavigator(ByVal sender As Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtcode._MYNavigator
        Try
            LoadData(txtcode.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

   
    Private Sub txtcode__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtcode._MYValidating
        Dim str As String = "select count(*) from TSPL_HR_QUALIFICATION_MASTER where Qualification_Code ='" + txtcode.Value + "' "
        Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
        If no = 0 AndAlso isButtonClicked = False Then
            txtcode.MyReadOnly = False
        Else
            txtcode.MyReadOnly = True
        End If

        If txtcode.MyReadOnly OrElse isButtonClicked Then
            Dim qry As String = ""
            qry = "Select Qualification_Code As [Code],Qualification_Name As [Qualification Name] from TSPL_HR_QUALIFICATION_MASTER"
            txtcode.Value = clsCommon.ShowSelectForm("QUA", qry, "Code", "", txtcode.Value, "TSPL_HR_QUALIFICATION_MASTER.Qualification_Code", isButtonClicked)
            If clsCommon.myLen(txtcode.Value) > 0 Then
                Dim objOT As ClsQualificationMaster
                objOT = ClsQualificationMaster.GetData(txtcode.Value, NavigatorType.Current)
                If Not objOT Is Nothing Then
                    LoadData(txtcode.Value, NavigatorType.Current)
                End If
            Else
                AddNew()
            End If
        End If
    End Sub

    
   
    Private Sub RMImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RMImport.Click
        Dim gv As New RadGridView()
        Dim IsNewEntry As Boolean
        Me.Controls.Add(gv)
        If transportSql.importExcel(gv, "Code", "Description") Then
            Dim linno As Integer = 1
            Try
                For Each grow As GridViewRowInfo In gv.Rows

                    Dim obj As New ClsQualificationMaster()
                    Dim strCode As String = clsCommon.myCstr(grow.Cells("Code").Value)
                    Dim strDescription As String = clsCommon.myCstr(grow.Cells("Description").Value)
                    linno += 1
                    If clsCommon.myLen(strCode) <= 0 Then
                        Throw New Exception("Code should not be left blank" + clsCommon.myCstr(linno) + ".")
                    End If


                    If clsCommon.myLen(strDescription) <= 0 Then
                        Throw New Exception("Description should not be left blank" + clsCommon.myCstr(linno) + ".")
                    End If

                    If clsCommon.myLen(strCode) > 0 AndAlso clsDBFuncationality.getSingleValue("Select count(*) from TSPL_HR_QUALIFICATION_MASTER where Qualification_Code='" + strCode + "' ") > 0 Then
                        IsNewEntry = False
                    Else
                        IsNewEntry = True

                    End If

                    obj.Qualification_Code = strCode
                    obj.Qualification_Name = strDescription
                    ClsQualificationMaster.SaveData(obj, IsNewEntry)

                Next
                'trans.Commit()
                common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                'trans.Rollback()
                myMessages.myExceptions(ex)
            End Try
        End If
        Me.Controls.Remove(gv)
    End Sub

    Private Sub RMExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RMExport.Click
        Dim str As String
        str = "select Qualification_Code as [Code],Qualification_Name As [Description]  from TSPL_HR_QUALIFICATION_MASTER "
        transportSql.ExporttoExcel(str, Me)
    End Sub
End Class
