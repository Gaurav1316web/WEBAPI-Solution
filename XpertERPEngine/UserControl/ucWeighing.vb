Public Class ucWeighing
#Region "Varibales"
    Dim _form_ID As String
    Dim objSerial As New clsSerialPort
    Delegate Sub My_Weight_Changed(ByVal Val As String)
    Event _MYWeightChanged As My_Weight_Changed
    Public isSkipHighSecurity As Boolean = False
    Dim SettingHighSecurityOnWeighingIntegratedScreen As Boolean = False
    Dim SettingStableSeconds As Integer = 5
    Dim SettingWeightTolerance As Decimal = 5
    Dim isZeroOccured As Boolean = True
    Dim arrReading As List(Of Double)
#End Region

    Public Sub LoadPortAndMachine()
        Try
            objSerial.SetPortNameValues(cboComPort)
        Catch ex As Exception
        End Try

        Try
            clsPortSetting.GetWeighingMachineNames(CboMachine)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub BtnStart_Click(sender As Object, e As EventArgs) Handles BtnStart.Click
        StartOrStop(True)
    End Sub

    Public Sub StartOrStop(ByVal isShowMsg As Boolean)
        Try
            If clsCommon.CompairString(BtnStart.Text, "Start") = CompairStringResult.Equal Then
                SetSerialData()
                BtnStart.Text = "Stop"
                cboComPort.Enabled = False
                CboMachine.Enabled = False
                SettingHighSecurityOnWeighingIntegratedScreen = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AddHighSecurityOnWeighingIntegratedScreen, clsFixedParameterCode.AddHighSecurityOnWeighingIntegratedScreen, Nothing)) = 1, True, False)
                SettingHighSecurityOnWeighingIntegratedScreen = SettingHighSecurityOnWeighingIntegratedScreen AndAlso Not isSkipHighSecurity
                If SettingHighSecurityOnWeighingIntegratedScreen Then
                    Timer2.Enabled = SettingHighSecurityOnWeighingIntegratedScreen
                    SettingStableSeconds = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.HighSecurityStableSeconds, clsFixedParameterCode.HighSecurityStableSeconds, Nothing))
                    SettingWeightTolerance = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.HighSecurityWeightTolerance, clsFixedParameterCode.HighSecurityWeightTolerance, Nothing))
                    isZeroOccured = True
                    arrReading = New List(Of Double)
                End If

            Else
                CloseCOMPORT()
            End If
        Catch ex As Exception
            cboComPort.Enabled = True
            CboMachine.Enabled = True
            BtnStart.Text = "Start"
            lblWeight.Text = "00.00"
            Timer2.Enabled = False
            objSerial.ClosePort()
            If isShowMsg Then
                clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            End If
        End Try
    End Sub

    Public Sub CloseCOMPORT()
        Try
            cboComPort.Enabled = True
            CboMachine.Enabled = True
            BtnStart.Text = "Start"
            lblWeight.Text = "00.00"
            objSerial.ClosePort()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub SetSerialData()
        Try
            Dim obj As clsPortSetting = clsPortSetting.getData(1, CboMachine.Text)
            If obj Is Nothing Then
                Throw New Exception("No setting found for device")
            End If
            objSerial.BaudRate = obj.baud_rate
            objSerial.Parity = obj.parity
            objSerial.PortName = cboComPort.Text
            objSerial.StopBits = obj.stop_bits
            objSerial.DataBits = obj.data_bits
            objSerial.DataForm = obj.Data_Form
            objSerial.isEkoProMachine = False
            objSerial.isWeighingMachine = True
            objSerial._LblFat = Nothing
            objSerial._LblSNF = Nothing
            objSerial._LblWeight = lblWeight
            objSerial._MachineCode = CboMachine.SelectedValue
            objSerial.MachineName = clsPortSetting.getMachineMakePrefix(CboMachine.Text, 1)
            objSerial.OpenPort()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub MyLabel5_DoubleClick(sender As Object, e As EventArgs) Handles MyLabel5.DoubleClick
        Dim dbpwd As String = clsDBFuncationality.getSingleValue("select Description from TSPL_FIXED_PARAMETER where Code='AllowMilkReceiptAfterSettingsisOn' and TYPE='AllowMilkReceiptAfterSettingsisOn'")
        Dim pwd As New FrmPWD(Nothing)
        pwd.strCode = "AllowMilkReceiptAfterSettingsisOn"
        pwd.strType = "AllowMilkReceiptAfterSettingsisOn"
        pwd.ShowDialog()
        If pwd.isPasswordCorrect Then
            txtManualCheck.Visible = True
            RadButton1.Visible = True
            RadButton2.Visible = True
        End If
    End Sub

    Private Sub lblWeight_TextChanged(sender As Object, e As EventArgs) Handles lblWeight.TextChanged
        RaiseEvent _MYWeightChanged(lblWeight.Text)
    End Sub

    Private Sub RadButton1_Click(sender As Object, e As EventArgs) Handles RadButton1.Click
        lblWeight.Text = clsCommon.myFormat(txtManualCheck.Value)
    End Sub

    Private Sub RadButton2_Click(sender As Object, e As EventArgs) Handles RadButton2.Click
        Timer1.Enabled = Not Timer1.Enabled
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        lblWeight.Text = clsCommon.myFormat(clsCommon.myCdbl(lblWeight.Text) + 0.01)
    End Sub

    Public Property Machine() As String
        Get
            Return clsCommon.myCstr(CboMachine.SelectedValue)
        End Get

        Set(value As String)
            CboMachine.SelectedValue = value
        End Set
    End Property

    Public Property form_ID() As String
        Get
            Return _form_ID
        End Get

        Set(value As String)
            _form_ID = value
        End Set
    End Property

    Public Property Port() As String
        Get
            Return clsCommon.myCstr(cboComPort.Text)
        End Get

        Set(value As String)
            cboComPort.Text = value
        End Set
    End Property

    Private Sub RadButton3_Click(sender As Object, e As EventArgs) Handles RadButton3.Click
        Try
            If clsCommon.myLen(_form_ID) <= 0 Then
                Throw New Exception("Please provide Form ID")
            End If
            If clsCommon.myLen(CboMachine.SelectedValue) <= 0 Then
                Throw New Exception("Please select Machine")
            End If
            If clsCommon.myLen(cboComPort.Text) <= 0 Then
                Throw New Exception("Please select Port")
            End If

            Dim obj As New clsAutoMachinePick
            obj.Form_ID = _form_ID
            obj.Machine = clsCommon.myCstr(CboMachine.SelectedValue)
            obj.Com_Port = clsCommon.myCstr(cboComPort.Text)
            obj.saveData(obj)
            clsCommon.MyMessageBoxShow(Me, "Setting saved successfully", Me.Text)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Public Sub LoadSettingAndStart()
        Try
            If clsCommon.myLen(_form_ID) > 0 Then
                Dim obj As clsAutoMachinePick = clsAutoMachinePick.GetData(_form_ID)
                If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Form_ID) > 0 Then
                    CboMachine.SelectedValue = obj.Machine
                    cboComPort.Text = obj.Com_Port
                    If objCommonVar.AutoStartReading Then
                        StartOrStop(False)
                    End If
                End If
            End If
        Catch ex As Exception
            'clsCommon.MyMessageBoxShow(me,ex.Message,me.text)
        End Try
    End Sub

    Public Property LiveReading() As Double
        Get
            If SettingHighSecurityOnWeighingIntegratedScreen Then
                If SettingStableSeconds > 0 Then
                    If arrReading.Count < SettingStableSeconds Then
                        Throw New Exception("Reading should be stable for " + clsCommon.myCstr(SettingStableSeconds) + " Seconds.")
                    End If
                    Dim firstReading As Double = arrReading(0)
                    For ii As Integer = 0 To arrReading.Count - 1
                        If firstReading <> arrReading(ii) Then
                            Throw New Exception("Reading is not stable")
                        End If
                    Next
                End If
                If SettingWeightTolerance >= 0 Then
                    If isZeroOccured Then
                        isZeroOccured = False
                    Else
                        Throw New Exception("Improper minimum weight")
                    End If
                End If
            End If
            Return clsCommon.myCdbl(lblWeight.Text)
        End Get

        Set(ByVal Value As Double)
            lblWeight.Text = clsCommon.myCstr(Value)
        End Set
    End Property

    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        If arrReading.Count > 0 Then
            While arrReading.Count >= SettingStableSeconds
                arrReading.RemoveAt(0)
            End While
        End If
        arrReading.Add(clsCommon.myCdbl(lblWeight.Text))
        If SettingWeightTolerance >= 0 Then
            If Not isZeroOccured Then
                If clsCommon.myCdbl(lblWeight.Text) <= SettingWeightTolerance Then
                    isZeroOccured = True
                End If
            End If
        End If
    End Sub
End Class
