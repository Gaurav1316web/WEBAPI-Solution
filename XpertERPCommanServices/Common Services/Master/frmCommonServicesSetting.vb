Imports common
Imports System.Data.SqlClient
Imports common.Controls
Public Class frmCommonServicesSetting
    Inherits FrmMainTranScreen
#Region "Variables"
    Dim userCode, companyCode As String
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private isNewEntry As Boolean = False
#End Region

    Private Sub FrmSaleSetting_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        isNewEntry = True
        ButtonToolTip.SetToolTip(rdbtnsave, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(rdbtnclose, "Press Alt+C Close the Window")
        txtIdleTimeInMnt.Enabled = False
        SetControlsTag()
        LoadData()
    End Sub

    Sub SetControlsTag()
        chkGLACAccordingTaxRate.Tag1 = clsFixedParameterCode.GLACAccordingToTaxRate
        chkGLACAccordingTaxRate.Tag = clsFixedParameterType.GLACAccordingToTaxRate

        chkShowTaxRateColumnonTransaction.Tag1 = clsFixedParameterCode.ShowTaxRateColumnOnTransaction
        chkShowTaxRateColumnonTransaction.Tag = clsFixedParameterType.ShowTaxRateColumnOnTransaction
        '' Anubhooti 03-Sep-2014 BM00000003437 (Setting For Sub Account in Bank Master)
        ChkAllowToUseSubAcc.Tag1 = clsFixedParameterCode.AllowToUseSubAccount
        ChkAllowToUseSubAcc.Tag = clsFixedParameterType.AllowToUseSubAccount
        ''
        '' Balwinder 29-Sep-2014 BM00000003808
        chkBrachnAccounting.Tag1 = clsFixedParameterCode.ApplyBrachAccounting
        chkBrachnAccounting.Tag = clsFixedParameterType.ApplyBrachAccounting
        ''
        '' Anubhooti 16-Dec-2014 BM00000004959 (Setting For Bank Transfer(Withdrawal/Receipt/Both))
        ChkInTransit.Tag1 = clsFixedParameterCode.InTransitFeatureIsRequired
        ChkInTransit.Tag = clsFixedParameterType.InTransitFeatureIsRequired

        gbPermissionSettingForTransactionWithBank.Tag = clsFixedParameterType.PermissionSettingForTransactionWithBank
    End Sub

    Private Sub SetUserMgmtNew()
        '' Anubhooti 31-July-2014 BM00000003131
        'MyBase.SetUserMgmt(clsUserMgtCode.CommonServicesSetting)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
            'Me.Close()
            'Exit Function
        End If
        rdbtnsave.Visible = MyBase.isModifyFlag
        'btnPost.Visible = MyBase.isPostFlag

    End Sub

    Sub LoadData()
        Try
            For Each ctrl As Control In RadGroupBox1.Controls
                If ctrl.GetType Is GetType(MyCheckBox) Then
                    Dim chkBox As MyCheckBox = TryCast(ctrl, MyCheckBox)
                    If clsCommon.myLen(chkBox.Tag) >= 0 AndAlso clsCommon.myLen(chkBox.Tag1) >= 0 Then
                        chkBox.Checked = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(chkBox.Tag, chkBox.Tag1, Nothing)) = 1, True, False)
                    End If
                End If
            Next
            If clsCommon.myCdbl(clsFixedParameter.GetData(gbPermissionSettingForTransactionWithBank.Tag, gbPermissionSettingForTransactionWithBank.Tag, Nothing)) = 1 Then
                rbtnGLSecurity.IsChecked = True
            ElseIf clsCommon.myCdbl(clsFixedParameter.GetData(gbPermissionSettingForTransactionWithBank.Tag, gbPermissionSettingForTransactionWithBank.Tag, Nothing)) = 2 Then
                rbtnBankPermission.IsChecked = True
            Else
                rbtnNone.IsChecked = True
            End If
            
            If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.Idle, clsFixedParameterCode.isIdleTimerOn, Nothing)) = 0 Then
                chkIdleTimer.Checked = False
                txtIdleTimeInMnt.Text = ""
            Else
                chkIdleTimer.Checked = True
                txtIdleTimeInMnt.Text = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.Idle, clsFixedParameterCode.idleTime, Nothing))
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub SaveData()
        If MyBase.isModifyonPasswordFlag Then
            If clsPasswordCheckForMasters.CheckMasterPwd(clsUserMgtCode.CommonServicesSetting, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
            Else
                Return
            End If
        End If

        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try

            For Each ctrl As Control In RadGroupBox1.Controls
                If ctrl.GetType Is GetType(MyCheckBox) Then
                    Dim chkBox As MyCheckBox = TryCast(ctrl, MyCheckBox)
                    If clsCommon.myLen(chkBox.Tag) >= 0 AndAlso clsCommon.myLen(chkBox.Tag1) >= 0 Then
                        clsFixedParameter.UpdateData(chkBox.Tag, chkBox.Tag1, IIf(chkBox.Checked, "1", "0"), trans)
                    End If
                End If
            Next
            If rbtnGLSecurity.IsChecked Then
                clsFixedParameter.UpdateData(gbPermissionSettingForTransactionWithBank.Tag, gbPermissionSettingForTransactionWithBank.Tag, "1", trans)
            ElseIf rbtnBankPermission.IsChecked Then
                clsFixedParameter.UpdateData(gbPermissionSettingForTransactionWithBank.Tag, gbPermissionSettingForTransactionWithBank.Tag, "2", trans)
            Else
                clsFixedParameter.UpdateData(gbPermissionSettingForTransactionWithBank.Tag, gbPermissionSettingForTransactionWithBank.Tag, "0", trans)
            End If

            If chkIdleTimer.Checked Then
                If clsCommon.myCdbl(txtIdleTimeInMnt.Text) <= 0 Then
                    Throw New Exception("Idle time in Minute, Must not be negative or Zero or Alphanumeric Value")
                End If
                If Not IsNumeric(txtIdleTimeInMnt.Text) Then
                    Throw New Exception("Please numeric value in idle time in minute")
                End If
                clsFixedParameter.UpdateData(clsFixedParameterType.Idle, clsFixedParameterCode.isIdleTimerOn, "1", trans)
                clsFixedParameter.UpdateData(clsFixedParameterType.Idle, clsFixedParameterCode.idleTime, clsCommon.myCdbl(txtIdleTimeInMnt.Text), trans)
            Else
                clsFixedParameter.UpdateData(clsFixedParameterType.Idle, clsFixedParameterCode.isIdleTimerOn, "0", trans)
                clsFixedParameter.UpdateData(clsFixedParameterType.Idle, clsFixedParameterCode.idleTime, 0, trans)
            End If

            trans.Commit()
            clsCommon.MyMessageBoxShow(Me, "Data saved Successfully", Me.Text)

        Catch ex As Exception
            trans.Rollback()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub frmPurchaseSettings_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso rdbtnsave.Enabled Then
            SaveData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            Reset()
        End If
    End Sub

    Private Sub rdbtnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbtnclose.Click
        Me.Close()
    End Sub

    Private Sub rdbtnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbtnsave.Click
        SaveData()
    End Sub

    
    Private Sub chkIdleTimer_ToggleStateChanged(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkIdleTimer.ToggleStateChanged
        txtIdleTimeInMnt.Enabled = chkIdleTimer.Checked
        If txtIdleTimeInMnt.Enabled Then
        Else
            txtIdleTimeInMnt.Text = ""
        End If
    End Sub
End Class
