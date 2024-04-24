
Imports common
Imports XpertERPEngine
Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.WinControls.UI
Imports XpertERPEngineFine
Public Class frmBullMovement
    Inherits FrmMainTranScreen
    Dim isNewEntry As Boolean = True
    Dim ErrorControl As New clsErrorControl()
    Dim ButtonToolTip As ToolTip = New ToolTip()



    Private Sub frmBullMovement_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Dim coll As Dictionary(Of String, String)
        'coll = New Dictionary(Of String, String)()
        'coll.Add("Document_Code", "VARCHAR(30) NOT NULL PRIMARY KEY ")
        'coll.Add("Document_Date", "DateTime NOT NULL")
        'coll.Add("Bull_Code", "varchar(30) NULL REFERENCES TSPL_BULL_MASTER (Code)")
        'coll.Add("Bull_Movement_Type", "varchar(30) NULL REFERENCES TSPL_BULL_MOVEMENT_TYPE (Code)")
        'coll.Add("Bull_Shed", "varchar(30) NULL REFERENCES TSPL_BULL_SHED_MASTER (Code)")
        'coll.Add("Perid", "Varchar(50) NOT NULL ")
        'coll.Add("Status", "integer null")
        'coll.Add("Created_By", "varchar(12) NOT NULL REFERENCES TSPL_USER_MASTER (USER_CODE)")
        'coll.Add("Created_Date", "Datetime NOT NULL")
        'coll.Add("Modified_By", "varchar(12) NOT NULL REFERENCES TSPL_USER_MASTER (USER_CODE)")
        'coll.Add("Modified_Date", "Datetime NOT NULL")
        'coll.Add("Post_By", "VARCHAR(12) NULL REFERENCES TSPL_USER_MASTER(User_Code) ")
        'coll.Add("Post_Date", "DateTime NULL")
        'clsCommonFunctionality.CreateOrAlterTable("TSPL_BULL_MOVEMENT", coll)
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D  for Delete")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnnew, "Press Alt+N Adding New")
        funReset()
        LoadBullMovementType()
    End Sub
    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnsave.Visible = MyBase.isModifyFlag
        RadMenuItem1.Enabled = MyBase.isModifyFlag ' For Import
        RadMenuItem2.Enabled = MyBase.isModifyFlag ' For Export
        btndelete.Visible = MyBase.isDeleteFlag
    End Sub
    Sub funReset()
        isNewEntry = True
        txtCode.MyReadOnly = False
        txtCode.Value = Nothing
        txtCode.Focus()
        txtBullCode.Enabled = True
        txtBullCode.Value = ""
        LblMainLocation.Text = ""
        TxtShed.Enabled = True
        TxtShed.Value = ""
        LblShed.Text = ""
        txtPeriod.Text = ""
        cboBullMvmntType.SelectedValue = ""
        txtDate.Value = clsCommon.GETSERVERDATE()
        btnsave.Text = "Save"
    End Sub
    Sub LoadBullMovementType()
        Dim Whr = ""
        Dim dt As New DataTable()
        dt = clsBullMovementType.getBullMovementTypeQuery()
        cboBullMvmntType.DataSource = dt
        cboBullMvmntType.ValueMember = "Code"
        cboBullMvmntType.DisplayMember = "Name"
    End Sub

    Private Sub btnsave_Click(sender As Object, e As EventArgs) Handles btnsave.Click
        SaveData()
    End Sub

    Private Sub SaveData()
        Try
            If (AllowToSave()) Then
                If MyBase.isModifyonPasswordFlag Then
                    If clsPasswordCheckForMasters.CheckMasterPwd(clsUserMgtCode.frmBullMovement, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
                    Else
                        Return
                    End If
                End If
            End If
            Dim obj As New clsBullMovement()
            obj.Document_Code = txtCode.Value
            obj.Document_Date = txtDate.Value
            obj.Perid = txtPeriod.Text
            obj.Bull_Code = txtBullCode.Value
            obj.Bull_Shed = TxtShed.Value
            obj.Bull_Movement_Type = cboBullMvmntType.SelectedValue
            If IsNumeric(txtPeriod.Text) Then
                ' Convert the input to a number
                obj.Perid = clsCommon.myCdbl(txtPeriod.Text)
            Else
                ' Display an error message if the input is not a valid number
                clsCommon.MyMessageBoxShow("Please enter a valid number for Days.", Me.Text)
                txtPeriod.Focus()
                txtPeriod.Text = ""
                Exit Sub
            End If
            If (obj.SaveData(obj, isNewEntry)) Then
                clsCommon.MyMessageBoxShow(Me, "Data save successfully.", Me.Text)
                LoadData(obj.Document_Code, NavigatorType.Current)
            End If
            'End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Function AllowToSave() As Boolean
        Try
            If clsCommon.myLen(txtBullCode.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please Select Bull Code", Me.Text)
                txtBullCode.Focus()
                txtBullCode.Select()
                ErrorControl.SetError(txtBullCode, "Please Select Bull Code")
                Return False
            Else
                ErrorControl.ResetError(txtBullCode)
            End If
        Catch ex As Exception

        End Try
        Return True
    End Function

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Try
            isNewEntry = True
            btndelete.Enabled = False
            btnsave.Enabled = True
            btnsave.Text = "Update"
            txtCode.MyReadOnly = False
            Dim obj As clsBullMovement = clsBullMovement.GetData(strCode, NavTyep)
            If obj IsNot Nothing Then
                isNewEntry = False
                txtCode.Value = obj.Document_Code
                txtDate.Text = obj.Document_Date
                txtBullCode.Text = obj.Bull_Code
                TxtShed.Value = obj.Bull_Shed
                cboBullMvmntType.SelectedValue = obj.Bull_Movement_Type
                txtCode.MyReadOnly = True
                'btnsave.Text = "Update"
                '' btndelete.Enabled = True
                If obj.Status = ERPTransactionStatus.Approved Then
                    btnsave.Enabled = False
                    '' btnPost.Enabled = False
                    btndelete.Enabled = False
                End If
                UsLock1.Status = obj.Status
            Else
                'AddNew()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub txtBullCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtBullCode._MYValidating
        Dim qry As String = "select Code , Bull_Alia_Name as Bull_Name from TSPL_BULL_MASTER"
        txtBullCode.Value = clsCommon.ShowSelectForm("BULLFND", qry, "Code", "", txtBullCode.Value, "Code", isButtonClicked)

        If clsCommon.myLen(txtBullCode.Value) > 0 Then
            txtBullCode.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Bull_Alia_Name from TSPL_BULL_MASTER where Code='" + txtBullCode.Value + "'"))
        Else
            txtBullCode.Value = ""
            txtBullCode.Text = ""
        End If
    End Sub

    Private Sub TxtShed__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles TxtShed._MYValidating
        Dim qry As String = "select Code , Name  from TSPL_BULL_SHED_MASTER"
        TxtShed.Value = clsCommon.ShowSelectForm("BULLSHEDFND", qry, "Code", "", TxtShed.Value, "Code", isButtonClicked)

        If clsCommon.myLen(TxtShed.Value) > 0 Then
            TxtShed.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Name from TSPL_BULL_SHED_MASTER where Code='" + TxtShed.Value + "'"))
        Else
            TxtShed.Value = ""
            TxtShed.Text = ""
        End If
    End Sub

    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub btnnew_Click(sender As Object, e As EventArgs) Handles btnnew.Click
        funReset()
    End Sub

    Private Sub btndelete_Click(sender As Object, e As EventArgs) Handles btndelete.Click
        DeleteData()
    End Sub
    Private Sub DeleteData()
        Try
            If clsCommon.myLen(txtCode.Value) <= 0 Then
                txtCode.Focus()
                txtCode.Select()
                ErrorControl.SetError(txtCode, "Code not found to delete.")
                Throw New Exception("Code not found to delete")
            Else
                ErrorControl.ResetError(txtCode)
            End If

            If myMessages.deleteConfirm() Then
                If clsBullMovement.DeleteData(txtCode.Value) Then
                    myMessages.delete()
                    funReset()
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

End Class