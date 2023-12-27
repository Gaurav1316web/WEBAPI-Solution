Imports common
Imports System.Data.SqlClient
''---Preeti Gupta ticket no-BM00000003239--
Public Class FrmBankPermission
    Inherits FrmMainTranScreen

    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.FrmBankPermission)
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow("Permission Denied")
            Me.Close()
            Exit Sub
        End If
        FrmMainTranScreen.bankPermission()
    End Sub
    Private Sub FrmBankPermission_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        SetUserMgmtNew()
        BankCode()
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save")
        ButtonToolTip.SetToolTip(btnRefresh, "Press Alt+E for Refresh")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+R for Reset")
        'Dim bankcode As String = bankPermission()
        'Dim strWhrclas As String = " TSPL_BANK_MASTER.bank_code in (" + bankcode + ")"
    End Sub
    Public Sub BankCode()
        Dim qry As String = "select BANK_CODE as Code ,DESCRIPTION  from TSPL_BANK_MASTER   "
        cbgBankCode.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgBankCode.ValueMember = "Code"
        cbgBankCode.DisplayMember = "DESCRIPTION"
    End Sub


    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Reset()
    End Sub
    Public Sub Reset()
        cbgBankCode.UnCheckedAll()
        fndUser.Value = ""
    End Sub


    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Save()
    End Sub
    Public Sub Save()
        Dim arrbank As New List(Of clsBankPermission)
        Dim obj As clsBankPermission = Nothing
        Dim UserCode As New List(Of String)
        Dim i As Integer = 0
        Dim j As Integer = 0
        If (clsCommon.myLen(fndUser.Value) <= 0) Then
            common.clsCommon.MyMessageBoxShow(Me, "Please select user ", Me.Text)
            fndUser.Focus()
            Return
        End If
        If ((cbgBankCode.CheckedValue.Count) <= 0) Then
            common.clsCommon.MyMessageBoxShow(Me, "Please select atlist one Bank code ", Me.Text)
            cbgBankCode.Focus()
            Return
        End If
        If cbgBankCode.CheckedValue.Count > 0 Then
            For i = 0 To cbgBankCode.CheckedValue.Count - 1
                obj = New clsBankPermission()
                obj.Usercode = clsCommon.myCstr(fndUser.Value)
                obj.BankCode = clsCommon.myCstr(cbgBankCode.CheckedValue(i))
                arrbank.Add(obj)
            Next
            obj.SaveData(UserCode, arrbank)
            common.clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)

        End If
    End Sub


    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click

        Refreshsub()
    End Sub
    Public Sub Refreshsub()
        If (clsCommon.myLen(fndUser.Value) <= 0) Then
            common.clsCommon.MyMessageBoxShow(Me, "Please select user ", Me.Text)
            fndUser.Focus()
            Return
        End If

        Dim isget As Boolean = True
        Dim qry As String = "select Bank_Code  from tspl_user_bank_mapping where Item_Code = '" + fndUser.Value + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

        Dim arr As New ArrayList()
        For Each dr As DataRow In dt.Rows
            arr.Add(dr("Bank_Code").ToString())
        Next
        cbgBankCode.CheckedValue = arr

    End Sub
    Private Sub FrmBankPermission_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.Control AndAlso e.KeyCode = Keys.T Then

        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnSave.Enabled Then
            Save()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.E Then
            Refreshsub()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        ElseIf e.Alt And e.KeyCode = Keys.R Then
            Reset()
        End If
    End Sub

    Private Sub txtState__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndUser._MYValidating

        Dim qry As String = "select User_Code as [user Code] ,User_Name as [User name] from tspl_user_master"
        fndUser.Value = clsCommon.ShowSelectForm("User", qry, "user Code", "", fndUser.Value, "User_Code", isButtonClicked)
        'fndUser.Value = clsStateMaster.getFinder("", fndUser.Value, isButtonClicked)
    End Sub

    Private Sub chkBankAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs)

    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim qry As String = "delete from tspl_user_bank_mapping where Item_Code='" + fndUser.Value + "'"
        clsDBFuncationality.ExecuteNonQuery(qry)
        clsCommon.MyMessageBoxShow(Me, "Delete Successfuly", Me.Text)
        Reset()
    End Sub
End Class
