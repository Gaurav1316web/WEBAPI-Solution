Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports XpertERPEngine
Imports Telerik.WinControls.UI
Public Class frmDCSFinancialHead
    Inherits FrmMainTranScreen

#Region "Variable"
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private isNewEntry As Boolean = False
    Private isInsideLoadData As Boolean = False
    Dim Qry As String
#End Region

    Private Sub frmJWPriceCodeMaster_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        LoadType()
        LoadSubType()
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D  for Delete")
        ButtonToolTip.SetToolTip(rdbtnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(rdbtnreset, "Press Alt+N Adding New")
        funReset()
    End Sub

    Sub LoadType()
        isInsideLoadData = True

        cboType.DataSource = clsDCSFinancialHead.GetHeadType()
        cboType.ValueMember = "Code"
        cboType.DisplayMember = "Name"

        isInsideLoadData = False
    End Sub

    Sub LoadSubType()
        isInsideLoadData = True

        cboSubType.DataSource = clsDCSFinancialHead.GetHeadSubType(clsCommon.myCstr(cboType.SelectedValue))
        cboSubType.ValueMember = "Code"
        cboSubType.DisplayMember = "Name"

        isInsideLoadData = False
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        Save()
    End Sub
    Public Sub Save()
        Try
            If (AllowToSave()) Then
                Dim obj As New clsDCSFinancialHead()
                obj.Code = txtCode.Value

                obj.Description = txtDescription.Text
                obj.Type = clsCommon.myCstr(cboType.SelectedValue)
                obj.Sub_Type = clsCommon.myCstr(cboSubType.SelectedValue)
                obj.Parent_Head = txtParentCode.Value
                obj.SNo = txtSNO.Value
                If obj.SaveData(obj, isNewEntry) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                    LoadData(obj.Code, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Try
            isInsideLoadData = True
            funReset()
            Dim obj As clsDCSFinancialHead = clsDCSFinancialHead.GetData(strCode, NavTyep)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Code) > 0) Then
                btnsave.Enabled = True
                isNewEntry = False
                txtCode.Value = obj.Code
                txtDescription.Text = obj.Description
                cboType.SelectedValue = obj.Type
                LoadSubType()
                cboSubType.SelectedValue = obj.Sub_Type
                txtParentCode.Value = obj.Parent_Head
                txtSNO.Value = obj.SNo
                btnsave.Text = "Update"
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isInsideLoadData = False
        End Try
    End Sub
    Function AllowToSave() As Boolean
        If txtSNO.Value <= 0 Then
            myMessages.blankValue("SNo")
            txtSNO.Focus()
            Return False
        End If

        If clsCommon.myLen(txtDescription.Text) <= 0 Then
            myMessages.blankValue("Description")
            txtDescription.Focus()
            Return False
        End If
        If clsCommon.myLen(cboType.SelectedValue) <= 0 Then
            myMessages.blankValue("Type")
            cboType.Focus()
            Return False
        End If

        If clsCommon.myLen(cboSubType.SelectedValue) <= 0 Then
            myMessages.blankValue("Sub Type")
            cboSubType.Focus()
            Return False
        End If

        Return True
    End Function
    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        DeleteData()
    End Sub
    Sub DeleteData()
        If clsCommon.myLen(txtCode.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "You Cannot Delete Record", Me.Text)
            Exit Sub
        End If
        funDelete()
    End Sub
    Sub funDelete()
        Try
            If (myMessages.deleteConfirm()) Then
                If (clsDCSFinancialHead.DeleteData(txtCode.Value)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ", Me.Text)
                    funReset()
                End If
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try

    End Sub
    Private Sub txtCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtCode._MYValidating
        Dim str As String = "select count(*) from TSPL_DCS_FINANCIAL_HEAD where Code ='" + txtCode.Value + "' "
        Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
        If no = 0 AndAlso isButtonClicked = False Then
            txtCode.MyReadOnly = False
            isButtonClicked = True
        Else
            txtCode.MyReadOnly = True
        End If
        If txtCode.MyReadOnly OrElse isButtonClicked Then
            txtCode.Value = clsDCSFinancialHead.getFinder("", txtCode.Value, isButtonClicked)
            If txtCode.Value <> "" Then
                LoadData(txtCode.Value, NavigatorType.Current)
            Else
                funReset()
            End If
        End If
    End Sub
    Private Sub txtCode_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If (e.KeyChar = Chr(39)) Then
            e.Handled = True
        End If
    End Sub
    Private Sub txtCode__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtCode._MYNavigator
        Try
            LoadData(txtCode.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub funReset()
        isNewEntry = True
        txtCode.MyReadOnly = False
        txtCode.Focus()
        btnsave.Text = "Save"
        btnsave.Enabled = True
        btnDelete.Enabled = True
        cboType.SelectedValue = ""
        cboSubType.SelectedValue = ""
        txtParentCode.Value = ""
        txtSNO.Text = ""
        txtCode.Value = Nothing
        txtDescription.Text = Nothing
    End Sub
    Private Sub frmHSNMaster_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso rdbtnreset.Enabled Then
            funReset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnsave.Enabled Then
            Save()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btnDelete.Enabled Then
            DeleteData()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            funReset()

        End If
    End Sub
    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnsave.Visible = MyBase.isModifyFlag
        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub
    Private Sub rdbtnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbtnclose.Click
        Me.Close()
    End Sub
    Private Sub rdbtnreset_Click(sender As Object, e As EventArgs) Handles rdbtnreset.Click
        funReset()
    End Sub





    Private Sub RadButton1_Click(sender As Object, e As EventArgs) Handles RadButton1.Click
        Try
            txtCode.Value = clsDCSFinancialHead.getFinder("", txtCode.Value, True)
            If clsCommon.myLen(txtCode.Value) > 0 Then
                LoadData(txtCode.Value, NavigatorType.Current)
                isNewEntry = True
                txtCode.Value = ""
                btnDelete.Enabled = False
                btnsave.Enabled = True
                btnsave.Text = "Save"
            Else
                Reset()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub






    Private Sub txtParentCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtParentCode._MYValidating
        txtParentCode.Value = clsDCSFinancialHead.getFinder("", txtParentCode.Value, isButtonClicked)
    End Sub

    Private Sub cboType_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles cboType.Validating
        LoadSubType()
    End Sub
End Class