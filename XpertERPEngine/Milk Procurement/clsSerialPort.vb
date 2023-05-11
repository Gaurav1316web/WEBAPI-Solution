Imports System.Net.NetworkInformation
Imports System
Imports System.Text
Imports System.Drawing
Imports System.IO.Ports
Imports System.Windows.Forms
Imports common
Imports System.Data.SqlClient
Imports Comm64

Public Class clsSerialPort
#Region "Varaibles"
    Public objEkoPro As clsEkoPro = Nothing
    Public objWeighing As clsWeighingMachine = Nothing
    Public isWeighingMachine As Boolean = False
    Public isEkoProMachine As Boolean = False
    Public isDataReceived As Boolean = False
    Public MachineName As String = ""
    Public tmr As New Timer
    Public isEco2 As Boolean = False
    Private _Msg As String = String.Empty
    Private _baudRate As String = String.Empty
    Private _parity As String = String.Empty
    Private _stopBits As String = String.Empty
    Private _Data_Form As String = String.Empty
    Private _dataBits As String = String.Empty
    Private _portName As String = String.Empty
    Private _Input_String As String = String.Empty
    Private OldReading As String = String.Empty
    Private _IsAutoConfig As Boolean = False
    Private _isContinuous As Boolean = False
    Private _isCheckForZero As Boolean = False
    Private _StartStopSym As String = String.Empty
    Private _IntFrom As Integer = 0
    Private _IntNoOfChar As Integer = 0
    Private _FracFrom As Integer = 0
    Private _FracNoOfChar As Integer = 0
    Public _MachineCode As String = String.Empty
    Private _transType As TransmissionType
    Public _LblFat As common.Controls.MyLabel
    Public _LblSNF As common.Controls.MyLabel
    Public _LblProtein As common.Controls.MyLabel
    Public _LblWeight As common.Controls.MyLabel
    Public _LblAdulterant As common.Controls.MyLabel
    Public _LblConclusion As common.Controls.MyLabel
    Public _TextBox As common.Controls.MyTextBox
    Public _TextBoxRefresh As Boolean = False
    Private _fat As String
    Private _snf As String
    Private _Protein As String
    Private _Lactose As String
    Private _TotalSolids As String
    Private _FPD As String
    Private _TotalAcidity As String
    Private _Density As String
    Private _FFA As String
    Private _CitricAcids As String
    Private _Urea As String
    Private _Casein As String
    Private _Glucose As String
    Private _Conclusion As clsTempConclusion
    Private _Adulterant As clsTempAdulterant
    Private _weight As String
    Public _LblLactose As common.Controls.MyLabel
    Public _LblTotalSolids As common.Controls.MyLabel
    Public _LblFPD As common.Controls.MyLabel
    Public _LblTotalAcidity As common.Controls.MyLabel
    Public _LblDensity As common.Controls.MyLabel
    Public _LblFFA As common.Controls.MyLabel
    Public _LblCitricAcids As common.Controls.MyLabel
    Public _LblUrea As common.Controls.MyLabel
    Public _LblCasein As common.Controls.MyLabel
    Public _LblGlucose As common.Controls.MyLabel
    Private _hyperTerminal As String
    Private _type As MessageType
    Private MessageColor As Color() = {Color.Blue, Color.Green, Color.Black, Color.Orange, Color.Red}
    Private comPort As New SerialPort()
    Private comPort64 As New Comm64.Comm
    Private comPort64_2 As New Comm64.Comm
    Private write As Boolean = True
    Dim intcountr As Integer = 0
    Public isShowUtility As Boolean = False
#End Region

    Public Enum TransmissionType
        Text
        Hex
    End Enum

    Public Enum MessageType
        Incoming
        Outgoing
        Normal
        Warning
        [Error]
    End Enum
   
    Public Property BaudRate() As String
        Get
            Return _baudRate
        End Get
        Set(ByVal value As String)
            _baudRate = value
        End Set
    End Property

    Public Property Parity() As String
        Get
            Return _parity
        End Get
        Set(ByVal value As String)
            _parity = value
        End Set
    End Property

    Public Property StopBits() As String
        Get
            Return _stopBits
        End Get
        Set(ByVal value As String)
            _stopBits = value
        End Set
    End Property

    Public Property DataForm() As String
        Get
            Return _Data_Form
        End Get
        Set(ByVal value As String)
            _Data_Form = value
        End Set
    End Property

    Public Property DataBits() As String
        Get
            Return _dataBits
        End Get
        Set(ByVal value As String)
            _dataBits = value
        End Set
    End Property

    Public Property PortName() As String
        Get
            Return _portName
        End Get
        Set(ByVal value As String)
            _portName = value
        End Set
    End Property

    Public Property CurrentTransmissionType() As TransmissionType
        Get
            Return _transType
        End Get
        Set(ByVal value As TransmissionType)
            _transType = value
        End Set
    End Property

    Public Property DisplayFat() As common.Controls.MyLabel
        Get
            Return _LblFat
        End Get
        Set(ByVal value As common.Controls.MyLabel)
            _LblFat = value
        End Set
    End Property

    Public Property DisplaySNF() As common.Controls.MyLabel
        Get
            Return _LblSNF
        End Get
        Set(ByVal value As common.Controls.MyLabel)
            _LblSNF = value
        End Set
    End Property

    Public Property DisplayWeight() As common.Controls.MyLabel
        Get
            Return _LblWeight
        End Get
        Set(ByVal value As common.Controls.MyLabel)
            _LblWeight = value
        End Set
    End Property

    Public Property fat() As String
        Get
            Return _fat
        End Get
        Set(ByVal value As String)
            _fat = value
        End Set
    End Property

    Public Property snf() As String
        Get
            Return _snf
        End Get
        Set(ByVal value As String)
            _snf = value
        End Set
    End Property

    Public Property weight() As String
        Get
            Return _weight
        End Get
        Set(ByVal value As String)
            _weight = value
        End Set
    End Property

    Public Property Type() As MessageType
        Get
            Return _type
        End Get
        Set(ByVal value As MessageType)
            _type = value
        End Set
    End Property

    Public Sub New(ByVal baud As String, ByVal par As String, ByVal sBits As String, ByVal dBits As String, ByVal name As String, ByVal MachineName As String, ByVal DForm As String, ByRef lFat As common.Controls.MyLabel, ByRef lSnf As common.Controls.MyLabel)
        _baudRate = baud
        _parity = par
        _stopBits = sBits
        _dataBits = dBits
        _portName = name
        'AddHandler comPort.DataReceived, AddressOf comPort_DataReceived
        'If isEco2 Then
        '    AddHandler comPort64_2.OnComm, AddressOf comPort_2_DataReceived64
        'Else
        AddHandler comPort64.OnComm, AddressOf comPort_DataReceived64
        'End If
    End Sub

    Public Sub New(ByVal baud As String, ByVal par As String, ByVal sBits As String, ByVal dBits As String, ByVal name As String, ByVal MachineName As String, ByVal DForm As String, ByRef lWeight As common.Controls.MyLabel)
        _baudRate = baud
        _parity = par
        _stopBits = sBits
        _dataBits = dBits
        _portName = name
        _Data_Form = DForm

        'If isEco2 Then
        '    AddHandler comPort64_2.OnComm, AddressOf comPort_2_DataReceived64
        'Else
        AddHandler comPort64.OnComm, AddressOf comPort_DataReceived64
        'End If
    End Sub

    Public Sub New()
        _baudRate = String.Empty
        _parity = String.Empty
        _stopBits = String.Empty
        _dataBits = String.Empty
        _portName = "COM1"
        'AddHandler comPort.DataReceived, AddressOf comPort_DataReceived
        'If isEco2 Then
        '    AddHandler comPort64_2.OnComm, AddressOf comPort_2_DataReceived64
        'Else
        AddHandler comPort64.OnComm, AddressOf comPort_DataReceived64
        'End If
    End Sub

    <STAThread()> _
    Private Sub DisplayFATData(ByVal fat As String)
        Try
            _LblFat.Invoke(New EventHandler(AddressOf DoDisplayFAT))
        Catch ex As Exception

        End Try
    End Sub

    <STAThread()> _
    Private Sub DisplaySNFData(ByVal snf As String)
        Try
            _LblSNF.Invoke(New EventHandler(AddressOf DoDisplaySNF))
        Catch ex As Exception

        End Try
    End Sub

    <STAThread()> _
    Private Sub DisplayWeightData(ByVal weight As String)
        Try
            _LblWeight.Invoke(New EventHandler(AddressOf DoDisplayWeight))
        Catch ex As Exception

        End Try
    End Sub

    <STAThread()> _
    Private Sub DisplayTextBox(ByVal str As String)
        Try
            _Msg = str
            _TextBox.Invoke(New EventHandler(AddressOf DoTextBoxt))
        Catch ex As Exception

        End Try
    End Sub

    <STAThread()> _
    Private Sub DisplayAdulterantData()
        Try
            _LblAdulterant.Invoke(New EventHandler(AddressOf DoDisplayAdulterant))
        Catch ex As Exception

        End Try
    End Sub

    <STAThread()> _
    Private Sub DisplayConclutionData()
        Try
            _LblConclusion.Invoke(New EventHandler(AddressOf DoDisplayConclution))
        Catch ex As Exception

        End Try
    End Sub

    <STAThread()> _
    Private Sub DisplayProteinData()
        Try
            _LblProtein.Invoke(New EventHandler(AddressOf DoDisplayProtein))
        Catch ex As Exception

        End Try
    End Sub

    Private Sub DoTextBoxt(ByVal sender As Object, ByVal e As EventArgs)
        Try
            If _TextBox IsNot Nothing Then
                If _TextBoxRefresh Then
                    _TextBox.Text = _Msg
                Else
                    _TextBox.Text += Environment.NewLine + _Msg
                End If
                _TextBox.ScrollToCaret()
                _TextBox.Select()  'to Set Focus
                _TextBox.Select(_TextBox.Text.Length, 0) 'to set cursor at the end of textbox
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub DoDisplayAdulterant(ByVal sender As Object, ByVal e As EventArgs)
        Try
            _LblAdulterant.Text = _Adulterant.Water + IIf(clsCommon.myLen(_Adulterant.Water) > 0, " | ", "") + _Adulterant.Urea + IIf(clsCommon.myLen(_Adulterant.Urea) > 0, " | ", "") + _Adulterant.Maltodex + IIf(clsCommon.myLen(_Adulterant.Maltodex) > 0, " | ", "") + _Adulterant.AmmSulph + IIf(clsCommon.myLen(_Adulterant.AmmSulph) > 0, " | ", "") + _Adulterant.Sucrose
            _LblAdulterant.Tag = _Adulterant
        Catch ex As Exception
        End Try
    End Sub

    Private Sub DoDisplayConclution(ByVal sender As Object, ByVal e As EventArgs)
        Try
            _LblConclusion.Text = _Conclusion.Normal + IIf(clsCommon.myLen(_Conclusion.Normal) > 0, " | ", "") + _Conclusion.Sample
            _LblConclusion.Tag = _Conclusion
        Catch ex As Exception
        End Try
    End Sub

    Public Property DisplayConclution() As common.Controls.MyLabel
        Get
            Return _LblConclusion
        End Get
        Set(ByVal value As common.Controls.MyLabel)
            _LblConclusion = value
        End Set
    End Property

    Public Property DisplayAdulterant() As common.Controls.MyLabel
        Get
            Return _LblAdulterant
        End Get
        Set(ByVal value As common.Controls.MyLabel)
            _LblAdulterant = value
        End Set
    End Property

    Public Property DisplayProtein() As common.Controls.MyLabel
        Get
            Return _LblProtein
        End Get
        Set(ByVal value As common.Controls.MyLabel)
            _LblProtein = value
        End Set
    End Property

    Private Sub DoDisplayFAT(ByVal sender As Object, ByVal e As EventArgs)
        Try
            _LblFat.Text = _fat
        Catch ex As Exception

        End Try
    End Sub

    Private Sub DoDisplayProtein(ByVal sender As Object, ByVal e As EventArgs)
        Try
            _LblProtein.Text = _Protein
        Catch ex As Exception

        End Try
    End Sub

    Private Sub DoDisplaySNF(ByVal sender As Object, ByVal e As EventArgs)
        Try
            _LblSNF.Text = _snf
        Catch ex As Exception

        End Try
    End Sub

    Private Sub DoDisplayWeight(ByVal sender As Object, ByVal e As EventArgs)
        Try
            _LblWeight.Text = _weight
        Catch ex As Exception

        End Try
    End Sub

    <STAThread()> _
    Private Sub DisplayLactoseData()
        Try
            _LblLactose.Invoke(New EventHandler(AddressOf DoDisplayLactose))
        Catch ex As Exception
        End Try
    End Sub

    <STAThread()> _
    Private Sub DisplayTotalSolidsData()
        Try
            _LblTotalSolids.Invoke(New EventHandler(AddressOf DoDisplayTotalSolids))
        Catch ex As Exception
        End Try
    End Sub

    <STAThread()> _
    Private Sub DisplayFPDData()
        Try
            _LblFPD.Invoke(New EventHandler(AddressOf DoDisplayFPD))
        Catch ex As Exception
        End Try
    End Sub

    <STAThread()> _
    Private Sub DisplayTotalAcidityData()
        Try
            _LblTotalAcidity.Invoke(New EventHandler(AddressOf DoDisplayTotalAcidity))
        Catch ex As Exception
        End Try
    End Sub

    <STAThread()> _
    Private Sub DisplayDensityData()
        Try
            _LblDensity.Invoke(New EventHandler(AddressOf DoDisplayDensity))
        Catch ex As Exception
        End Try
    End Sub

    <STAThread()> _
    Private Sub DisplayFFAData()
        Try
            _LblFFA.Invoke(New EventHandler(AddressOf DoDisplayFFA))
        Catch ex As Exception
        End Try
    End Sub

    <STAThread()> _
    Private Sub DisplayCitricAcidsData()
        Try
            _LblCitricAcids.Invoke(New EventHandler(AddressOf DoDisplayCitricAcids))
        Catch ex As Exception
        End Try
    End Sub

    <STAThread()> _
    Private Sub DisplayUreaData()
        Try
            _LblUrea.Invoke(New EventHandler(AddressOf DoDisplayUrea))
        Catch ex As Exception
        End Try
    End Sub

    <STAThread()> _
    Private Sub DisplayCaseinData()
        Try
            _LblCasein.Invoke(New EventHandler(AddressOf DoDisplayCasein))
        Catch ex As Exception
        End Try
    End Sub

    <STAThread()> _
    Private Sub DisplayGlucoseData()
        Try
            _LblGlucose.Invoke(New EventHandler(AddressOf DoDisplayGlucose))
        Catch ex As Exception
        End Try
    End Sub

    Public Function OpenPort() As Boolean
        Try
            intcountr = 0
            If comPort64.PortOpen = True Then
                comPort64.PortOpen = False
            End If
            If comPort64_2.PortOpen = True Then
                comPort64_2.PortOpen = False
            End If
            OldReading = ""
            Dim qry As String = " select * from TSPL_MACHINE_INTEGRATION where code='" & _MachineCode & "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                _IsAutoConfig = True
                If clsCommon.myCdbl(dt.Rows(0)("isContinuousDataReading")) = 1 Then
                    _isContinuous = True
                    _StartStopSym = clsCommon.myCstr(dt.Rows(0)("StartStopCharacter"))
                Else
                    _isContinuous = False
                    _StartStopSym = ""
                End If
                _IntFrom = clsCommon.myCdbl(dt.Rows(0)("IntFromPos"))
                _IntNoOfChar = clsCommon.myCdbl(dt.Rows(0)("IntNoOfChar"))
                _FracFrom = clsCommon.myCdbl(dt.Rows(0)("FracFromPos"))
                _FracNoOfChar = clsCommon.myCdbl(dt.Rows(0)("FracNoOfChar"))
                _Input_String = clsCommon.myCstr(dt.Rows(0)("Input_String"))
                _isCheckForZero = IIf(clsCommon.myCdbl(dt.Rows(0)("Check_For_Zero")) = 1, True, False)
            Else
                _IsAutoConfig = False
            End If
            'If isEco2 Then
            '    comPort64_2.Settings = "" & Integer.Parse(_baudRate) & "," & DirectCast([Enum].Parse(GetType(Parity), _parity), Parity) & "," & Integer.Parse(_dataBits) & "," & DirectCast([Enum].Parse(GetType(StopBits), _stopBits), StopBits)
            '    If clsCommon.myLen(_portName) > 3 Then
            '        comPort64_2.CommPort = Microsoft.VisualBasic.Right(_portName, Microsoft.VisualBasic.Len(_portName) - 3)
            '    Else
            '        Throw New Exception("Please select a valid Port")
            '    End If

            '    If clsCommon.CompairString(_Data_Form, "H") = CompairStringResult.Equal Then
            '        CurrentTransmissionType = TransmissionType.Hex
            '    Else
            '        CurrentTransmissionType = TransmissionType.Text
            '    End If
            '    '            AddHandler comPort.DataReceived, AddressOf comPort_DataReceived
            '    AddHandler comPort64_2.OnComm, AddressOf comPort_2_DataReceived64
            '    comPort64_2.PortOpen = True
            '    If Not comPort64_2.PortOpen Then
            '        Throw New Exception("Port  " & _portName & " Could not Open")
            '    End If
            'Else
            Dim TParity As String = _parity
                If clsCommon.myLen(_parity) > 0 Then
                    TParity = _parity.Substring(0, 1)
                Else
                    TParity = "n"
                End If
                comPort64.Settings = "" & Integer.Parse(_baudRate) & "," & TParity & "," & Integer.Parse(_dataBits) & "," & DirectCast([Enum].Parse(GetType(StopBits), _stopBits), StopBits)
                'comPort64.Settings = "" & Integer.Parse(_baudRate) & "," & DirectCast([Enum].Parse(GetType(Parity), _parity), Parity) & "," & Integer.Parse(_dataBits) & "," & DirectCast([Enum].Parse(GetType(StopBits), _stopBits), StopBits)
                'comPort64.Settings = "1200,E,8,1"
                'clsCommon.MyMessageBoxShow(comPort64.Settings)
                If clsCommon.myLen(_portName) > 3 Then
                    comPort64.CommPort = Microsoft.VisualBasic.Right(_portName, Microsoft.VisualBasic.Len(_portName) - 3)
                Else
                    Throw New Exception("Please select a valid Port")
                End If

                If clsCommon.CompairString(_Data_Form, "H") = CompairStringResult.Equal Then
                    CurrentTransmissionType = TransmissionType.Hex
                Else
                    CurrentTransmissionType = TransmissionType.Text
                End If
                AddHandler comPort64.OnComm, AddressOf comPort_DataReceived64
                comPort64.PortOpen = True
                If clsCommon.myLen(_Input_String) > 0 Then
                    comPort64.Output = _Input_String
                End If
                If Not comPort64.PortOpen Then
                    Throw New Exception("Port  " & _portName & " Could not Open")
                End If
                'End If
                arrBy = Nothing
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Sub ClosePort()
        Try
            'If comPort.IsOpen Then
            '    _type = MessageType.Normal
            '    comPort.Close()
            'End If
            OldReading = ""
            If comPort64.PortOpen = True Then
                _type = MessageType.Normal
                comPort64.PortOpen = False
            End If
            If comPort64_2.PortOpen = True Then
                _type = MessageType.Normal
                comPort64_2.PortOpen = False
            End If
        Catch ex As Exception

        End Try
    End Sub

    Public Sub SetParityValues(ByVal obj As Object)
        Try
            DirectCast(obj, common.Controls.MyComboBox).Items.Clear()
            For Each str As String In [Enum].GetNames(GetType(Parity))
                DirectCast(obj, common.Controls.MyComboBox).Items.Add(str)
            Next
            If DirectCast(obj, common.Controls.MyComboBox).Items.Count > 0 Then
                DirectCast(obj, common.Controls.MyComboBox).SelectedIndex = 0
            End If
        Catch ex As Exception

        End Try
    End Sub

    Public Sub SetBaudRate(ByVal obj As Object)
        Try
            DirectCast(obj, common.Controls.MyComboBox).Items.Clear()
            For i As Integer = 1000 To 30000 Step 100
                DirectCast(obj, common.Controls.MyComboBox).Items.Add(i.ToString())
            Next
            DirectCast(obj, common.Controls.MyComboBox).Text = "2400"
        Catch ex As Exception

        End Try
    End Sub

    Public Sub SetStopBitValues(ByVal obj As Object)
        Try
            DirectCast(obj, common.Controls.MyComboBox).Items.Clear()
            For Each str As String In [Enum].GetNames(GetType(StopBits))
                DirectCast(obj, common.Controls.MyComboBox).Items.Add(str)
            Next
            If DirectCast(obj, common.Controls.MyComboBox).Items.Count > 0 Then
                DirectCast(obj, common.Controls.MyComboBox).SelectedIndex = 0
            End If
        Catch ex As Exception

        End Try
    End Sub

    Public Sub SetPortNameValues(ByVal obj As Object)
        Try
            DirectCast(obj, common.Controls.MyComboBox).Items.Clear()
            For Each str As String In SerialPort.GetPortNames()
                DirectCast(obj, common.Controls.MyComboBox).Items.Add(str)
            Next
            If DirectCast(obj, common.Controls.MyComboBox).Items.Count > 0 Then
                DirectCast(obj, common.Controls.MyComboBox).SelectedIndex = 0
            End If
        Catch ex As Exception

        End Try
    End Sub

    Public Sub SetDataBits(ByVal obj As Object)
        Try
            DirectCast(obj, common.Controls.MyComboBox).Items.Clear()
            DirectCast(obj, common.Controls.MyComboBox).Items.Add(7)
            DirectCast(obj, common.Controls.MyComboBox).Items.Add(8)
            DirectCast(obj, common.Controls.MyComboBox).Items.Add(9)
            DirectCast(obj, common.Controls.MyComboBox).Text = "8"
        Catch ex As Exception

        End Try
    End Sub

    Public Sub comPort_DataReceived(ByVal sender As Object, ByVal e As SerialDataReceivedEventArgs)

        Try
            If CurrentTransmissionType = TransmissionType.Text Then ' For Text Transmission type of machines
                Dim msg As String = comPort.ReadExisting()
                msg = msg.Trim()
                If Microsoft.VisualBasic.Len(msg) > 0 Then
                    If isEkoProMachine And clsCommon.CompairString(MachineName, "E") = CompairStringResult.Equal Then '' For Everest Old milk analyzer
                        If clsCommon.CompairString(Microsoft.VisualBasic.Left(msg, 1), "(") = CompairStringResult.Equal Then
                            _fat = IIf(clsCommon.myCdbl(Microsoft.VisualBasic.Mid(msg, 2, 2)) <= 9, "0" & clsCommon.myCdbl(Microsoft.VisualBasic.Mid(msg, 2, 2)), clsCommon.myCdbl(Microsoft.VisualBasic.Mid(msg, 2, 2))) & "." & IIf(clsCommon.myCdbl(Microsoft.VisualBasic.Mid(msg, 4, 2)) <= 9, clsCommon.myCdbl(Microsoft.VisualBasic.Mid(msg, 4, 2)) & "0", clsCommon.myCdbl(Microsoft.VisualBasic.Mid(msg, 4, 2)))
                            _snf = IIf(clsCommon.myCdbl(Microsoft.VisualBasic.Mid(msg, 6, 2)) <= 9, "0" & clsCommon.myCdbl(Microsoft.VisualBasic.Mid(msg, 6, 2)), clsCommon.myCdbl(Microsoft.VisualBasic.Mid(msg, 6, 2))) & "." & IIf(clsCommon.myCdbl(Microsoft.VisualBasic.Mid(msg, 8, 2)) <= 9, clsCommon.myCdbl(Microsoft.VisualBasic.Mid(msg, 8, 2)) & "0", clsCommon.myCdbl(Microsoft.VisualBasic.Mid(msg, 8, 2)))
                            If clsCommon.myCdbl(_fat) > 12 OrElse clsCommon.myCdbl(_snf) > 12 Then
                                Exit Sub
                            End If
                            If IsNumeric(_fat) AndAlso IsNumeric(_snf) Then
                                DisplayFATData(_fat)
                                DisplaySNFData(_snf)
                            End If
                        End If
                    ElseIf isEkoProMachine And clsCommon.CompairString(MachineName, "N") = CompairStringResult.Equal Then '' Final Checked For Everest New Mchine
                        If clsCommon.CompairString(Microsoft.VisualBasic.Left(msg, 1), "(") = CompairStringResult.Equal Then
                            ''_fat = IIf(clsCommon.myCdbl(Microsoft.VisualBasic.Mid(msg, 2, 2)) <= 9, "0" &  Microsoft.VisualBasic.Mid(msg, 2, 2), Microsoft.VisualBasic.Mid(msg, 2, 2)) & "." & IIf(clsCommon.myCdbl(Microsoft.VisualBasic.Mid(msg, 4, 2)) <= 9, Microsoft.VisualBasic.Mid(msg, 4, 2) & "0", Microsoft.VisualBasic.Mid(msg, 4, 2))
                            '_fat = Microsoft.VisualBasic.Mid(msg, 2, 2) & "." & Microsoft.VisualBasic.Mid(msg, 4, 2)
                            '_snf = IIf(clsCommon.myCdbl(Microsoft.VisualBasic.Mid(msg, 6, 2)) <= 9, "0" & Microsoft.VisualBasic.Mid(msg, 6, 2), Microsoft.VisualBasic.Mid(msg, 6, 2)) & "." & IIf(clsCommon.myCdbl(Microsoft.VisualBasic.Mid(msg, 8, 2)) <= 9, Microsoft.VisualBasic.Mid(msg, 8, 2) & "0", Microsoft.VisualBasic.Mid(msg, 8, 2))
                            '_snf = Microsoft.VisualBasic.Mid(msg, 6, 2) & "." & Microsoft.VisualBasic.Mid(msg, 8, 2)
                            _fat = IIf(clsCommon.myCdbl(Microsoft.VisualBasic.Mid(msg, 2, 2)) <= 9, "0" & clsCommon.myCdbl(Microsoft.VisualBasic.Mid(msg, 2, 2)), clsCommon.myCdbl(Microsoft.VisualBasic.Mid(msg, 2, 2))) & "." & IIf(clsCommon.myCdbl(Microsoft.VisualBasic.Mid(msg, 4, 2)) <= 9, clsCommon.myCdbl(Microsoft.VisualBasic.Mid(msg, 4, 2)) & "0", clsCommon.myCdbl(Microsoft.VisualBasic.Mid(msg, 4, 2)))
                            _snf = IIf(clsCommon.myCdbl(Microsoft.VisualBasic.Mid(msg, 6, 2)) <= 9, "0" & clsCommon.myCdbl(Microsoft.VisualBasic.Mid(msg, 6, 2)), clsCommon.myCdbl(Microsoft.VisualBasic.Mid(msg, 6, 2))) & "." & IIf(clsCommon.myCdbl(Microsoft.VisualBasic.Mid(msg, 8, 2)) <= 9, clsCommon.myCdbl(Microsoft.VisualBasic.Mid(msg, 8, 2)) & "0", clsCommon.myCdbl(Microsoft.VisualBasic.Mid(msg, 8, 2)))
                            If clsCommon.myCdbl(_fat) > 12 OrElse clsCommon.myCdbl(_snf) > 12 Then
                                Exit Sub
                            End If
                            If IsNumeric(_fat) AndAlso IsNumeric(_snf) Then
                                DisplayFATData(_fat)
                                DisplaySNFData(_snf)
                            End If
                        End If
                    ElseIf isWeighingMachine And clsCommon.CompairString(MachineName, "P") = CompairStringResult.Equal Then '' For Prompt
                        Dim IntPart As String = String.Empty
                        Dim FracPart As String = String.Empty
                        Try
                            IntPart = Microsoft.VisualBasic.Mid(msg, 2, 4)
                            IntPart = (IIf(clsCommon.myCdbl(IntPart) > 0 And clsCommon.myCdbl(IntPart) <= 9, "0" & clsCommon.myCdbl(IntPart), clsCommon.myCdbl(IntPart))).ToString
                        Catch ex As Exception
                        End Try

                        Try
                            FracPart = Microsoft.VisualBasic.Mid(msg, 6, 1) & "0"
                        Catch ex As Exception
                        End Try
                        _weight = IntPart & "." & FracPart
                        If IsNumeric(_weight) Then
                            DisplayWeightData(_weight)
                        End If
                    ElseIf isWeighingMachine And clsCommon.CompairString(MachineName, "X") = CompairStringResult.Equal Then '' For Prompt Ajmer
                        Dim IntPart As String = String.Empty
                        Dim FracPart As String = String.Empty
                        Try
                            IntPart = Microsoft.VisualBasic.Mid(msg, 2, 5)
                            IntPart = (IIf(clsCommon.myCdbl(IntPart) > 0 And clsCommon.myCdbl(IntPart) <= 9, "0" & clsCommon.myCdbl(IntPart), clsCommon.myCdbl(IntPart))).ToString
                        Catch ex As Exception
                        End Try

                        Try
                            FracPart = Microsoft.VisualBasic.Mid(msg, 7, 1) & "0"
                        Catch ex As Exception
                        End Try
                        _weight = IntPart & "." & FracPart
                        If IsNumeric(_weight) Then
                            DisplayWeightData(_weight)
                        End If
                    ElseIf isWeighingMachine And clsCommon.CompairString(MachineName, "D") = CompairStringResult.Equal Then '' For Delta
                        If clsCommon.CompairString(Microsoft.VisualBasic.Left(msg, 1), "[") = CompairStringResult.Equal Then
                            Dim IntPart As String = String.Empty
                            Dim FracPart As String = String.Empty
                            Try
                                IntPart = Microsoft.VisualBasic.Mid(msg, 2, 5)
                                IntPart = (IIf(clsCommon.myCdbl(IntPart) > 0 And clsCommon.myCdbl(IntPart) <= 9, "0" & clsCommon.myCdbl(IntPart), clsCommon.myCdbl(IntPart))).ToString
                            Catch ex As Exception
                            End Try

                            Try
                                FracPart = Microsoft.VisualBasic.Mid(msg, 7, 1) & "0"
                            Catch ex As Exception
                            End Try
                            ' _weight = (Math.Round(CDbl(Microsoft.VisualBasic.Mid(msg, 2, 5) & "." & Microsoft.VisualBasic.Mid(msg, 7, 1)), 1).ToString())
                            ' _weight = clsCommon.myCdbl(Microsoft.VisualBasic.Mid(msg, 2, 5)).ToString() & "." & clsCommon.myCdbl(Microsoft.VisualBasic.Mid(msg, 7, 1)).ToString
                            '_weight = Format(_weight, "00.00")
                            _weight = IntPart & "." & FracPart
                            If IsNumeric(_weight) Then
                                DisplayWeightData(_weight)
                            End If
                        End If
                    ElseIf isWeighingMachine And clsCommon.CompairString(MachineName, "B") = CompairStringResult.Equal Then '' For Panasonic
                        If clsCommon.CompairString(Microsoft.VisualBasic.Left(msg, 1), "[") = CompairStringResult.Equal Then
                            Dim IntPart As String = String.Empty
                            Dim FracPart As String = String.Empty
                            Try
                                IntPart = Microsoft.VisualBasic.Mid(msg, 2, 4)
                                IntPart = (IIf(clsCommon.myCdbl(IntPart) > 0 And clsCommon.myCdbl(IntPart) <= 9, "0" & clsCommon.myCdbl(IntPart), clsCommon.myCdbl(IntPart))).ToString
                            Catch ex As Exception
                            End Try

                            Try
                                FracPart = Microsoft.VisualBasic.Mid(msg, 6, 1) & "0"
                            Catch ex As Exception
                            End Try
                            _weight = IntPart & "." & FracPart
                            If IsNumeric(_weight) Then
                                DisplayWeightData(_weight)
                            End If
                        End If
                    ElseIf isWeighingMachine AndAlso (clsCommon.CompairString(MachineName, "U") = CompairStringResult.Equal) Then '' For Kothputli Supertech
                        _weight = (CDbl(Microsoft.VisualBasic.Mid(msg, 2, 6))).ToString()
                        If IsNumeric(_weight) Then
                            DisplayWeightData(_weight)
                        End If
                    ElseIf isWeighingMachine AndAlso (clsCommon.CompairString(MachineName, "S") = CompairStringResult.Equal OrElse clsCommon.CompairString(MachineName, "T") = CompairStringResult.Equal) Then '' For  Supertech
                        _weight = (CDbl(msg)).ToString()
                        If IsNumeric(_weight) Then
                            DisplayWeightData(_weight)
                        End If
                    ElseIf isWeighingMachine And clsCommon.CompairString(MachineName, "C") = CompairStringResult.Equal Then '' For Everest
                        Dim IntPart As String = String.Empty
                        Dim FracPart As String = String.Empty
                        Try
                            IntPart = Microsoft.VisualBasic.Mid(msg, 2, 3)
                            IntPart = (IIf(clsCommon.myCdbl(IntPart) > 0 And clsCommon.myCdbl(IntPart) <= 9, "0" & clsCommon.myCdbl(IntPart), clsCommon.myCdbl(IntPart))).ToString
                        Catch ex As Exception
                        End Try

                        Try
                            FracPart = Microsoft.VisualBasic.Mid(msg, 5, 1) & "0"
                        Catch ex As Exception
                        End Try
                        _weight = IntPart & "." & FracPart
                        '_weight = (Math.Round(CDbl(Microsoft.VisualBasic.Left(msg, 3) & "." & Microsoft.VisualBasic.Right(msg, 2)), 2)).ToString()
                        If IsNumeric(_weight) Then
                            DisplayWeightData(_weight)
                        End If
                    Else
                        _weight = (CDbl(msg)).ToString()
                        If IsNumeric(_weight) Then
                            DisplayWeightData(_weight)
                        End If
                    End If
                End If
            ElseIf CurrentTransmissionType = TransmissionType.Hex Then
                Dim msg(comPort.BytesToRead()) As Byte
                comPort.Read(msg, 0, comPort.BytesToRead())
                If msg.Length() > 0 Then
                    If isEkoProMachine And clsCommon.CompairString(MachineName, "K") = CompairStringResult.Equal Then
                        If clsCommon.myCdbl(msg(0).ToString("X") & "." & msg(1).ToString("X")) > 12 OrElse clsCommon.myCdbl(msg(2).ToString("X") & "." & msg(3).ToString("X")) > 12 Then
                            Exit Sub
                        End If
                        _fat = IIf(clsCommon.myCdbl(msg(0).ToString("X")) <= 9, "0" & msg(0).ToString("X"), msg(0).ToString("X")) & "." & IIf(clsCommon.myCdbl(msg(1).ToString("X")) <= 9, "0" & msg(1).ToString("X"), msg(1).ToString("X"))
                        _snf = IIf(clsCommon.myCdbl(msg(2).ToString("X")) <= 9, "0" & msg(2).ToString("X"), msg(2).ToString("X")) & "." & IIf(clsCommon.myCdbl(msg(3).ToString("X")) <= 9, "0" & msg(3).ToString("X"), msg(3).ToString("X"))
                        If IsNumeric(_fat) AndAlso IsNumeric(_snf) Then
                            DisplayFATData(_fat)
                            DisplaySNFData(_snf)
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub


    Public Sub comPort_DataReceived64()
        Try
            If Comm64.COMMEVENTS.comEvReceive Then

                If clsCommon.myLen(_Input_String) > 0 Then
                    comPort64.Output = _Input_String ''By this this event call in next time if required.
                End If
                If CurrentTransmissionType = TransmissionType.Text Then ' For Text Transmission type of machines
                    'clsCommon.MyMessageBoxShow(IIf(isWeighingMachine, "True", "False") + "  And " + MachineName)
                    Dim msg As String = comPort64.Input
                    If Microsoft.VisualBasic.Len(msg) > 0 Then
                        Try
                            If isShowUtility Then
                                DisplayTextBox(msg)
                            End If
                        Catch ex As Exception
                        End Try

                        If isEkoProMachine And clsCommon.CompairString(MachineName, "E") = CompairStringResult.Equal Then '' For Everest Old milk analyzer
                            If clsCommon.CompairString(Microsoft.VisualBasic.Left(msg, 1), "(") = CompairStringResult.Equal Then
                                'Reading
                                '(02010462153548130000000007011) Result- FAT 02.01,SNF 04.62
                                '(02510542181336940000000006300) Result- FAT 02.51,SNF 05.42
                                'By Balwinder on 11/12/2018 
                                '_fat = IIf(clsCommon.myCdbl(Microsoft.VisualBasic.Mid(msg, 2, 2)) <= 9, "0" & clsCommon.myCdbl(Microsoft.VisualBasic.Mid(msg, 2, 2)), clsCommon.myCdbl(Microsoft.VisualBasic.Mid(msg, 2, 2))) & "." & IIf(clsCommon.myCdbl(Microsoft.VisualBasic.Mid(msg, 4, 2)) <= 9, clsCommon.myCdbl(Microsoft.VisualBasic.Mid(msg, 4, 2)) & "0", clsCommon.myCdbl(Microsoft.VisualBasic.Mid(msg, 4, 2)))
                                '_snf = IIf(clsCommon.myCdbl(Microsoft.VisualBasic.Mid(msg, 6, 2)) <= 9, "0" & clsCommon.myCdbl(Microsoft.VisualBasic.Mid(msg, 6, 2)), clsCommon.myCdbl(Microsoft.VisualBasic.Mid(msg, 6, 2))) & "." & IIf(clsCommon.myCdbl(Microsoft.VisualBasic.Mid(msg, 8, 2)) <= 9, clsCommon.myCdbl(Microsoft.VisualBasic.Mid(msg, 8, 2)) & "0", clsCommon.myCdbl(Microsoft.VisualBasic.Mid(msg, 8, 2)))
                                _fat = IIf(clsCommon.myCdbl(Microsoft.VisualBasic.Mid(msg, 2, 2)) <= 9, "0" & clsCommon.myCdbl(Microsoft.VisualBasic.Mid(msg, 2, 2)), clsCommon.myCdbl(Microsoft.VisualBasic.Mid(msg, 2, 2))) & "." & clsCommon.myCstr(Microsoft.VisualBasic.Mid(msg, 4, 2))
                                _snf = IIf(clsCommon.myCdbl(Microsoft.VisualBasic.Mid(msg, 6, 2)) <= 9, "0" & clsCommon.myCdbl(Microsoft.VisualBasic.Mid(msg, 6, 2)), clsCommon.myCdbl(Microsoft.VisualBasic.Mid(msg, 6, 2))) & "." & clsCommon.myCstr(Microsoft.VisualBasic.Mid(msg, 8, 2))
                                If clsCommon.myCdbl(_fat) > 12 OrElse clsCommon.myCdbl(_snf) > 12 Then
                                    Exit Sub
                                End If
                                If IsNumeric(_fat) AndAlso IsNumeric(_snf) Then
                                    DisplayFATData(_fat)
                                    DisplaySNFData(_snf)
                                End If
                            End If
                        ElseIf isEkoProMachine And clsCommon.CompairString(MachineName, "F") = CompairStringResult.Equal Then '' For IndiFOSS on 16 Feb 2016 by balwinder
                            Try
                                Dim strBreak As String() = clsCommon.myCstr(OldReading + msg).Split(New String() {Environment.NewLine}, StringSplitOptions.None)
                                If strBreak.Length > 0 Then
                                    Dim strTemp As String = strBreak(0)
                                    Dim strTempBreak As String() = strTemp.Split(",")

                                    OldReading = strTemp
                                    If strTempBreak.Length = 12 Then
                                        _fat = strTempBreak(2)
                                        _snf = strTempBreak(3)

                                        _Protein = strTempBreak(4)

                                        _Adulterant = Nothing
                                        _Adulterant = New clsTempAdulterant()
                                        _Adulterant.Water = strTempBreak(5)
                                        _Adulterant.Urea = strTempBreak(6)
                                        _Adulterant.Maltodex = strTempBreak(7)
                                        _Adulterant.AmmSulph = strTempBreak(8)
                                        _Adulterant.Sucrose = strTempBreak(9)

                                        _Conclusion = Nothing
                                        _Conclusion = New clsTempConclusion()
                                        _Conclusion.Normal = strTempBreak(10)
                                        _Conclusion.Sample = strTempBreak(11)
                                        If IsNumeric(_fat) AndAlso IsNumeric(_snf) Then
                                            DisplayFATData(_fat)
                                            DisplaySNFData(_snf)
                                            DisplayProteinData()
                                            DisplayConclutionData()
                                            DisplayAdulterantData()
                                        End If
                                        OldReading = ""
                                    End If
                                    If strTempBreak.Length > 50 Then
                                        OldReading = ""
                                    End If

                                    strTemp = Nothing
                                    strTempBreak = Nothing
                                End If
                                strBreak = Nothing
                            Catch ex As Exception
                            End Try
                        ElseIf isEkoProMachine And clsCommon.CompairString(MachineName, "G") = CompairStringResult.Equal Then '' For IndiFOSSBIG on 25 Jun 2016 by balwinder
                            Try

                                Dim strBreak As String() = clsCommon.myCstr(OldReading + msg).Split(New String() {Environment.NewLine}, StringSplitOptions.None)
                                If strBreak.Length > 0 Then

                                    Dim strTemp As String = strBreak(0)
                                    Dim strTempBreak As String() = strTemp.Split(",")

                                    OldReading = strTemp
                                    If strTempBreak.Length = 13 Then

                                        _fat = strTempBreak(0)
                                        _snf = strTempBreak(1)
                                        _Protein = strTempBreak(2)
                                        _Lactose = strTempBreak(3)
                                        _TotalSolids = strTempBreak(4)
                                        _FPD = strTempBreak(5)
                                        _TotalAcidity = strTempBreak(6)
                                        _Density = strTempBreak(7)
                                        _FFA = strTempBreak(8)
                                        _CitricAcids = strTempBreak(9)
                                        _Urea = strTempBreak(10)
                                        _Casein = strTempBreak(11)
                                        _Glucose = strTempBreak(12)

                                        If IsNumeric(_fat) AndAlso IsNumeric(_snf) Then

                                            DisplayFATData(_fat)
                                            DisplaySNFData(_snf)
                                            DisplayProteinData()
                                            DisplayLactoseData()
                                            DisplayTotalSolidsData()
                                            DisplayFPDData()
                                            DisplayFFAData()
                                            DisplayGlucoseData()
                                            DisplayDensityData()
                                            DisplayTotalAcidityData()
                                            DisplayUreaData()
                                            DisplayCaseinData()
                                            DisplayCitricAcidsData()

                                        End If
                                        OldReading = ""
                                    End If
                                    If strTempBreak.Length > 50 Then
                                        OldReading = ""
                                    End If

                                    strTemp = Nothing
                                    strTempBreak = Nothing
                                End If
                                strBreak = Nothing
                            Catch ex As Exception
                            End Try
                        ElseIf isEkoProMachine And clsCommon.CompairString(MachineName, "N") = CompairStringResult.Equal Then '' Final Checked For Everest New Mchine
                            If clsCommon.CompairString(Microsoft.VisualBasic.Left(msg, 1), "(") = CompairStringResult.Equal Then
                                _fat = IIf(clsCommon.myCdbl(Microsoft.VisualBasic.Mid(msg, 2, 2)) <= 9, "0" & clsCommon.myCdbl(Microsoft.VisualBasic.Mid(msg, 2, 2)), clsCommon.myCdbl(Microsoft.VisualBasic.Mid(msg, 2, 2))) & "." & IIf(clsCommon.myCdbl(Microsoft.VisualBasic.Mid(msg, 4, 2)) <= 9, clsCommon.myCdbl(Microsoft.VisualBasic.Mid(msg, 4, 2)) & "0", clsCommon.myCdbl(Microsoft.VisualBasic.Mid(msg, 4, 2)))
                                _snf = IIf(clsCommon.myCdbl(Microsoft.VisualBasic.Mid(msg, 6, 2)) <= 9, "0" & clsCommon.myCdbl(Microsoft.VisualBasic.Mid(msg, 6, 2)), clsCommon.myCdbl(Microsoft.VisualBasic.Mid(msg, 6, 2))) & "." & IIf(clsCommon.myCdbl(Microsoft.VisualBasic.Mid(msg, 8, 2)) <= 9, clsCommon.myCdbl(Microsoft.VisualBasic.Mid(msg, 8, 2)) & "0", clsCommon.myCdbl(Microsoft.VisualBasic.Mid(msg, 8, 2)))
                                If clsCommon.myCdbl(_fat) > 12 OrElse clsCommon.myCdbl(_snf) > 12 Then
                                    Exit Sub
                                End If
                                If IsNumeric(_fat) AndAlso IsNumeric(_snf) Then
                                    DisplayFATData(_fat)
                                    DisplaySNFData(_snf)
                                End If
                            End If
                        ElseIf isEkoProMachine And clsCommon.CompairString(MachineName, "Y") = CompairStringResult.Equal Then '' For Benny by balwinder on 20/Apr/2017
                            Try
                                OldReading = OldReading + msg
                                If clsCommon.CompairString(Microsoft.VisualBasic.Left(OldReading, 1), "(") = CompairStringResult.Equal Then
                                    If OldReading.Contains(")") Then
                                        _fat = IIf(clsCommon.myCdbl(Microsoft.VisualBasic.Mid(OldReading, 2, 2)) <= 9, "0" & clsCommon.myCdbl(Microsoft.VisualBasic.Mid(OldReading, 2, 2)), clsCommon.myCdbl(Microsoft.VisualBasic.Mid(OldReading, 2, 2))) & "." & IIf(clsCommon.myCdbl(Microsoft.VisualBasic.Mid(OldReading, 4, 2)) <= 9, clsCommon.myCdbl(Microsoft.VisualBasic.Mid(OldReading, 4, 2)) & "0", clsCommon.myCdbl(Microsoft.VisualBasic.Mid(OldReading, 4, 2)))
                                        _snf = IIf(clsCommon.myCdbl(Microsoft.VisualBasic.Mid(OldReading, 6, 2)) <= 9, "0" & clsCommon.myCdbl(Microsoft.VisualBasic.Mid(OldReading, 6, 2)), clsCommon.myCdbl(Microsoft.VisualBasic.Mid(OldReading, 6, 2))) & "." & IIf(clsCommon.myCdbl(Microsoft.VisualBasic.Mid(OldReading, 8, 2)) <= 9, clsCommon.myCdbl(Microsoft.VisualBasic.Mid(OldReading, 8, 2)) & "0", clsCommon.myCdbl(Microsoft.VisualBasic.Mid(OldReading, 8, 2)))
                                        OldReading = ""
                                        If clsCommon.myCdbl(_fat) > 12 OrElse clsCommon.myCdbl(_snf) > 12 Then
                                            Exit Sub
                                        End If
                                        If IsNumeric(_fat) AndAlso IsNumeric(_snf) Then
                                            DisplayFATData(_fat)
                                            DisplaySNFData(_snf)
                                        End If
                                    End If
                                Else
                                    OldReading = ""
                                End If
                            Catch ex As Exception
                            End Try

                        ElseIf isEkoProMachine And clsCommon.CompairString(MachineName, "B4") = CompairStringResult.Equal Then '' For Benny4 by balwinder on 07/01/2019
                            Try
                                ''Reading  
                                ' Provisional Acknowledgement Slip

                                '          MILAN MILK FOOD LTD
                                '             SEHA BAGHEL 69

                                'Date: 07/01/19          Time: 00:33
                                'Code: ........................Name: ........................
                                'Milk Type:    Mix     Quantity:.........
                                'FAT:          5.1%    SNF:          7.9% 
                                'Rate (Rs.): ..........Amount:...........
                                Try
                                    OldReading += msg
                                    If OldReading.Contains("Amount:...........") OrElse OldReading.Contains("SSCounter") OrElse OldReading.Contains("CLR:") Then
                                        Dim cut_at As String = "FAT:"
                                        Dim stringSeparators() As String = {cut_at}
                                        Dim split = OldReading.Split(stringSeparators, 2, StringSplitOptions.RemoveEmptyEntries)
                                        _fat = split(1).Substring(9, 4)
                                        cut_at = "SNF:"
                                        stringSeparators = {cut_at}
                                        split = OldReading.Split(stringSeparators, 2, StringSplitOptions.RemoveEmptyEntries)
                                        _snf = split(1).Substring(9, 4)
                                        If IsNumeric(_fat) AndAlso IsNumeric(_snf) Then
                                            DisplayFATData(_fat)
                                            DisplaySNFData(_snf)
                                        End If
                                        OldReading = ""
                                    End If
                                Catch ex As Exception
                                End Try
                            Catch ex As Exception
                            End Try
                        ElseIf isWeighingMachine And clsCommon.CompairString(MachineName, "P") = CompairStringResult.Equal Then '' For Prompt
                            'To be Uncommented
                            Dim IntPart As String = String.Empty
                            Dim FracPart As String = String.Empty
                            Try
                                IntPart = Microsoft.VisualBasic.Mid(msg, 2, 4)
                                IntPart = (IIf(clsCommon.myCdbl(IntPart) > 0 And clsCommon.myCdbl(IntPart) <= 9, "0" & clsCommon.myCdbl(IntPart), clsCommon.myCdbl(IntPart))).ToString
                            Catch ex As Exception
                            End Try
                            Try
                                FracPart = Microsoft.VisualBasic.Mid(msg, 6, 1) & "0"
                            Catch ex As Exception
                            End Try
                            _weight = IntPart & "." & FracPart
                            If IsNumeric(_weight) Then
                                DisplayWeightData(_weight)
                            End If
                            'end of to be uncommented
                        ElseIf isWeighingMachine And clsCommon.CompairString(MachineName, "X") = CompairStringResult.Equal Then '' For Prompt Ajmer
                            Dim IntPart As String = String.Empty
                            Dim FracPart As String = String.Empty
                            Try
                                IntPart = Microsoft.VisualBasic.Mid(msg, 2, 5)
                                IntPart = (IIf(clsCommon.myCdbl(IntPart) > 0 And clsCommon.myCdbl(IntPart) <= 9, "0" & clsCommon.myCdbl(IntPart), clsCommon.myCdbl(IntPart))).ToString
                            Catch ex As Exception
                            End Try
                            Try
                                FracPart = Microsoft.VisualBasic.Mid(msg, 7, 1) & "0"
                            Catch ex As Exception
                            End Try
                            _weight = IntPart & "." & FracPart
                            If IsNumeric(_weight) Then
                                DisplayWeightData(_weight)
                            End If
                        ElseIf isWeighingMachine And clsCommon.CompairString(MachineName, "D") = CompairStringResult.Equal Then '' For Delta
                            If clsCommon.CompairString(Microsoft.VisualBasic.Left(msg, 1), "[") = CompairStringResult.Equal Then
                                Dim IntPart As String = String.Empty
                                Dim FracPart As String = String.Empty
                                Try
                                    IntPart = Microsoft.VisualBasic.Mid(msg, 2, 5)
                                    IntPart = (IIf(clsCommon.myCdbl(IntPart) > 0 And clsCommon.myCdbl(IntPart) <= 9, "0" & clsCommon.myCdbl(IntPart), clsCommon.myCdbl(IntPart))).ToString
                                Catch ex As Exception
                                End Try

                                Try
                                    FracPart = Microsoft.VisualBasic.Mid(msg, 7, 1) & "0"
                                Catch ex As Exception
                                End Try
                                _weight = IntPart & "." & FracPart
                                If IsNumeric(_weight) Then
                                    DisplayWeightData(_weight)
                                End If
                            End If
                        ElseIf isWeighingMachine And clsCommon.CompairString(MachineName, "B") = CompairStringResult.Equal Then '' For Panasonic
                            If clsCommon.CompairString(Microsoft.VisualBasic.Left(msg, 1), "[") = CompairStringResult.Equal Then
                                Dim IntPart As String = String.Empty
                                Dim FracPart As String = String.Empty
                                Try
                                    IntPart = Microsoft.VisualBasic.Mid(msg, 2, 4)
                                    IntPart = (IIf(clsCommon.myCdbl(IntPart) > 0 And clsCommon.myCdbl(IntPart) <= 9, "0" & clsCommon.myCdbl(IntPart), clsCommon.myCdbl(IntPart))).ToString
                                Catch ex As Exception
                                End Try

                                Try
                                    FracPart = Microsoft.VisualBasic.Mid(msg, 6, 1) & "0"
                                Catch ex As Exception
                                End Try
                                _weight = IntPart & "." & FracPart
                                If IsNumeric(_weight) Then
                                    DisplayWeightData(_weight)
                                End If
                            End If
                        ElseIf isWeighingMachine And clsCommon.CompairString(MachineName, "Z") = CompairStringResult.Equal Then '' For Smart EWS
                            If clsCommon.myLen(msg) >= 8 Then
                                If clsCommon.CompairString(Microsoft.VisualBasic.Mid(msg, 8, 1), "=") = CompairStringResult.Equal Then
                                    Dim IntPart As String = String.Empty
                                    Dim FracPart As String = String.Empty
                                    Try
                                        IntPart = Microsoft.VisualBasic.Mid(msg, 1, 5)
                                        IntPart = (IIf(clsCommon.myCdbl(IntPart) > 0 And clsCommon.myCdbl(IntPart) <= 9, "0" & clsCommon.myCdbl(IntPart), clsCommon.myCdbl(IntPart))).ToString
                                    Catch ex As Exception
                                    End Try

                                    Try
                                        FracPart = Microsoft.VisualBasic.Mid(msg, 7, 1) & "0"
                                    Catch ex As Exception
                                    End Try
                                    _weight = IntPart & "." & FracPart
                                    If IsNumeric(_weight) Then
                                        DisplayWeightData(_weight)
                                    End If
                                End If
                            End If
                        ElseIf isWeighingMachine And clsCommon.CompairString(MachineName, "Z1") = CompairStringResult.Equal Then '' For Smart EWS 
                            'N0010 0.3
                            '5=lt
                            OldReading += msg
                            If OldReading.Contains("=lt") Then
                                If OldReading.Contains("N") Then
                                    OldReading = Microsoft.VisualBasic.Mid(OldReading, 2, 7)
                                    If IsNumeric(OldReading) Then
                                        _weight = clsCommon.myCdbl(OldReading)
                                        DisplayWeightData(_weight)
                                    End If
                                End If
                                OldReading = ""
                            End If
                        ElseIf isWeighingMachine And clsCommon.CompairString(MachineName, "Z2") = CompairStringResult.Equal Then '' For Smart EWS 
                            'SAMPLE NO.:    0014
                            'ROUTE Code:    0013
                            'VILLAGE Code:  1317
                            'MILK Type:     0005
                            'CANS:        0001
                            'QUANTITY:   0038.1
                            OldReading += msg
                            If OldReading.Contains("QUANTITY:") Then
                                Dim strTempBreak As String() = OldReading.Split(":")
                                Dim temp As String = strTempBreak(strTempBreak.Length - 1)
                                If temp.Contains(".") Then
                                    strTempBreak = temp.Split(".")
                                    If strTempBreak.Length > 1 Then
                                        If clsCommon.myLen(strTempBreak(strTempBreak.Length - 1)) > 0 Then
                                            temp = temp.Trim()
                                            If IsNumeric(temp) Then
                                                _weight = clsCommon.myCdbl(temp)
                                                DisplayWeightData(_weight)
                                                OldReading = ""
                                            End If
                                        End If
                                    End If
                                End If
                            End If
                        ElseIf isWeighingMachine And clsCommon.CompairString(MachineName, "IT") = CompairStringResult.Equal Then '' For Intech
                            ''0811000= result is 1180
                            OldReading += msg
                            If OldReading.Contains("=") Then
                                Dim strTempBreak As String() = OldReading.Split("=")
                                If strTempBreak.Length > 0 Then
                                    Dim temp As String = ""
                                    If strTempBreak.Length > 1 Then
                                        OldReading = strTempBreak(strTempBreak.Length - 1)
                                        temp = strTempBreak(strTempBreak.Length - 2)
                                    End If
                                    temp = temp.Trim()
                                    temp = clsERPFuncationality.myReverseString(temp)
                                    If IsNumeric(temp) Then
                                        _weight = clsCommon.myCdbl(temp)
                                        DisplayWeightData(_weight)
                                    End If
                                End If
                            End If
                        ElseIf isWeighingMachine AndAlso (clsCommon.CompairString(MachineName, "U") = CompairStringResult.Equal) Then '' For Kothputli Supertech
                            _weight = (CDbl(Microsoft.VisualBasic.Mid(msg, 2, 6))).ToString()
                            If IsNumeric(_weight) Then
                                DisplayWeightData(_weight)
                            End If
                        ElseIf isWeighingMachine AndAlso (clsCommon.CompairString(MachineName, "S") = CompairStringResult.Equal OrElse clsCommon.CompairString(MachineName, "T") = CompairStringResult.Equal) Then '' For Supertech
                            _weight = (CDbl(msg)).ToString()
                            If IsNumeric(_weight) Then
                                DisplayWeightData(_weight)
                            End If
                        ElseIf isWeighingMachine And clsCommon.CompairString(MachineName, "C") = CompairStringResult.Equal Then '' For Everest
                            Dim IntPart As String = String.Empty
                            Dim FracPart As String = String.Empty
                            Try
                                IntPart = Microsoft.VisualBasic.Mid(msg, 2, 3)
                                IntPart = (IIf(clsCommon.myCdbl(IntPart) > 0 And clsCommon.myCdbl(IntPart) <= 9, "0" & clsCommon.myCdbl(IntPart), clsCommon.myCdbl(IntPart))).ToString
                            Catch ex As Exception
                            End Try

                            Try
                                FracPart = Microsoft.VisualBasic.Mid(msg, 5, 1) & "0"
                            Catch ex As Exception
                            End Try
                            _weight = IntPart & "." & FracPart
                            If IsNumeric(_weight) Then
                                DisplayWeightData(_weight)
                            End If
                        ElseIf isWeighingMachine And clsCommon.CompairString(MachineName, "EverestNew") = CompairStringResult.Equal Then '' For Everest New Format 00507,-00000
                            If msg.Substring(0, 1) = " " AndAlso clsCommon.myLen(OldReading) > 0 Then
                                Dim IntPart As String = String.Empty
                                Dim FracPart As String = String.Empty
                                Try
                                    IntPart = Microsoft.VisualBasic.Mid(OldReading, 2, 3)
                                    IntPart = (IIf(clsCommon.myCdbl(IntPart) > 0 And clsCommon.myCdbl(IntPart) <= 9, "0" & clsCommon.myCdbl(IntPart), clsCommon.myCdbl(IntPart))).ToString
                                Catch ex As Exception
                                End Try
                                Try
                                    FracPart = Microsoft.VisualBasic.Mid(OldReading, 5, 2) & "0"
                                Catch ex As Exception
                                End Try
                                _weight = IntPart & "." & FracPart
                                If IsNumeric(_weight) Then
                                    DisplayWeightData(_weight)
                                End If
                                OldReading = msg
                            ElseIf msg.Substring(0, 1) = "-" Then
                                OldReading = ""
                                _weight = "0.000"
                                DisplayWeightData(_weight)
                            Else
                                OldReading = OldReading + msg
                            End If
                        ElseIf isWeighingMachine And clsCommon.CompairString(MachineName, "Thanwla") = CompairStringResult.Equal Then '' For Thanwla Ticket no BM00000008591 on 22/Jan/2016 by Balwinder
                            ''By Balwinder on 22-Jan-2015  msg= start L0004.8=/.8=//
                            Dim IntPart As String = String.Empty
                            Dim FracPart As String = String.Empty
                            If clsCommon.myLen(msg) > 0 Then
                                If clsCommon.CompairString(msg(0), "L") = CompairStringResult.Equal AndAlso clsCommon.CompairString(msg(clsCommon.myLen(msg) - 1), "=") = CompairStringResult.Equal Then
                                    Try
                                        IntPart = Microsoft.VisualBasic.Mid(msg, _IntFrom, _IntNoOfChar)
                                        IntPart = (IIf(clsCommon.myCdbl(IntPart) > 0 And clsCommon.myCdbl(IntPart) <= 9, "0" & clsCommon.myCdbl(IntPart), clsCommon.myCdbl(IntPart))).ToString
                                    Catch ex As Exception
                                    End Try

                                    Try
                                        FracPart = Microsoft.VisualBasic.Mid(msg, _FracFrom, _FracNoOfChar) & "0"
                                    Catch ex As Exception
                                    End Try
                                    _weight = IntPart & "." & FracPart
                                    If IsNumeric(_weight) AndAlso clsCommon.myCdbl(_weight) > 0 Then
                                        DisplayWeightData(_weight)
                                    End If
                                End If
                            End If
                        ElseIf isWeighingMachine And clsCommon.CompairString(MachineName, "Thanwla New") = CompairStringResult.Equal Then
                            ''Balwinder on 18/02/2016
                            Dim IntPart As String = String.Empty
                            Dim FracPart As String = String.Empty
                            Try
                                OldReading += msg
                                If OldReading.Contains("=") Then
                                    Try
                                        IntPart = Microsoft.VisualBasic.Mid(OldReading, _IntFrom, _IntNoOfChar)
                                        IntPart = (IIf(clsCommon.myCdbl(IntPart) > 0 And clsCommon.myCdbl(IntPart) <= 9, "0" & clsCommon.myCdbl(IntPart), clsCommon.myCdbl(IntPart))).ToString
                                    Catch ex As Exception
                                    End Try
                                    Try
                                        FracPart = Microsoft.VisualBasic.Mid(OldReading, _FracFrom, _FracNoOfChar) & "0"
                                    Catch ex As Exception
                                    End Try
                                    _weight = IntPart & "." & FracPart
                                    If IsNumeric(_weight) AndAlso clsCommon.myCdbl(_weight) >= 0 Then
                                        DisplayWeightData(_weight)
                                    End If
                                    OldReading = ""
                                End If
                            Catch ex As Exception
                            End Try
                        ElseIf isWeighingMachine And clsCommon.CompairString(MachineName, "DSKNISAN") = CompairStringResult.Equal Then
                            ''Balwinder on 18/02/2016
                            Dim IntPart As String = String.Empty
                            Dim FracPart As String = String.Empty
                            Try
                                OldReading += msg
                                If OldReading.Contains("kg") Then
                                    Try
                                        IntPart = Microsoft.VisualBasic.Mid(OldReading, _IntFrom, _IntNoOfChar)
                                        IntPart = (IIf(clsCommon.myCdbl(IntPart) > 0 And clsCommon.myCdbl(IntPart) <= 9, "0" & clsCommon.myCdbl(IntPart), clsCommon.myCdbl(IntPart))).ToString
                                    Catch ex As Exception
                                    End Try
                                    Try
                                        FracPart = Microsoft.VisualBasic.Mid(OldReading, _FracFrom, _FracNoOfChar) & "0"
                                    Catch ex As Exception
                                    End Try
                                    _weight = IntPart & "." & FracPart
                                    If IsNumeric(_weight) AndAlso clsCommon.myCdbl(_weight) >= 0 Then
                                        DisplayWeightData(_weight)
                                    End If
                                    OldReading = ""
                                End If
                            Catch ex As Exception
                            End Try
                        ElseIf isWeighingMachine And clsCommon.CompairString(MachineName, "Essae") = CompairStringResult.Equal Then '' For Essae MPD on 18/Mar/2016 by Balwinder
                            ''msg=  x.x,xx.x,xxx.x and first character is space. Setting is faild due to no fixed as shown in example
                            If clsCommon.myLen(msg) > 0 Then
                                Try
                                    Dim reading As String = OldReading + msg.Trim().Replace("lt", "")
                                    If reading.Substring(reading.Length - 1, 1) = "." Then
                                        OldReading = reading
                                    Else
                                        reading = reading.Substring(1, clsCommon.myLen(reading) - 1)
                                        Dim strTempBreak As String() = reading.Split(".")
                                        If strTempBreak.Length = 2 Then
                                            If clsCommon.myLen(strTempBreak(0)) > 0 AndAlso clsCommon.myLen(strTempBreak(1)) > 0 Then
                                                _weight = reading
                                                DisplayWeightData(reading)
                                            End If
                                        End If
                                        OldReading = ""
                                    End If
                                Catch ex As Exception
                                End Try
                            End If
                        ElseIf isWeighingMachine And clsCommon.CompairString(MachineName, "Essae1") = CompairStringResult.Equal Then
                            ''msg=  x.x,xx.x,xxx.x and first character is space. Setting is faild due to no fixed as shown in example
                            If clsCommon.myLen(msg) > 0 Then
                                Try
                                    Dim reading As String = msg.Trim().Replace("lt", "")
                                    If reading.Substring(reading.Length - 2, 1) = "." Then
                                        Dim strTempBreak As String() = reading.Split(".")
                                        If strTempBreak.Length = 2 Then
                                            If clsCommon.myLen(strTempBreak(0)) > 0 AndAlso clsCommon.myLen(strTempBreak(1)) > 0 Then
                                                _weight = reading
                                                DisplayWeightData(reading)
                                            End If
                                        End If
                                    End If
                                Catch ex As Exception
                                End Try
                            End If
                        ElseIf isWeighingMachine And clsCommon.CompairString(MachineName, "Essae2") = CompairStringResult.Equal Then
                            ''SAMPLE Reading and result is 3.8,120.0
                            ''   14 3.
                            '8             29-12-2017
                            ' 17:02:25       
                            '1:
                            '3.7      3.8 OK

                            '   15 12
                            '0.0           29-12-2017
                            ' 17:04:32        1    11
                            '6.7    120.0 OK
                            If clsCommon.myLen(msg) > 0 Then
                                Try
                                    OldReading += msg
                                    If msg.Contains("OK") Then
                                        Try
                                            _weight = Microsoft.VisualBasic.Mid(OldReading, 60, 9).Trim()
                                            If IsNumeric(_weight) Then
                                                DisplayWeightData(_weight)
                                            End If
                                        Catch ex As Exception
                                        Finally
                                            OldReading = ""
                                        End Try
                                    End If
                                Catch ex As Exception
                                End Try
                            End If

                        ElseIf isWeighingMachine And clsCommon.CompairString(MachineName, "Essae3") = CompairStringResult.Equal Then
                            '19.9
                            '19.9
                            '19.9
                            '19.9

                            '19.9
                            '19.9
                            '19.9
                            '19.9
                            '19.
                            '9:

                            '19.5

                            '19.5

                            '19.5


                            '0.3

                            '0.3

                            If clsCommon.myLen(msg) > 0 Then
                                Try
                                    Dim reading As String = msg.Trim()
                                    Dim strBreak As String() = clsCommon.myCstr(reading).Split(New String() {Environment.NewLine}, StringSplitOptions.None)
                                    If strBreak.Length > 1 Then
                                        Dim strTemp As String = strBreak(1).Substring(1)
                                        _weight = clsCommon.myCdbl(strTemp)
                                        DisplayWeightData(_weight)
                                    End If
                                Catch ex As Exception
                                End Try
                            End If
                        ElseIf isWeighingMachine And clsCommon.CompairString(MachineName, "Essae4") = CompairStringResult.Equal Then
                            '19.5

                            '19.5

                            '19.5


                            '0.3

                            '0.3
                            If clsCommon.myLen(msg) > 0 Then
                                Try
                                    Dim reading As String = System.Text.RegularExpressions.Regex.Replace(msg.Trim(), "[^0-9.]", "")
                                    _weight = clsCommon.myCdbl(reading)
                                    If IsNumeric(_weight) Then
                                        DisplayWeightData(_weight)
                                    End If
                                Catch ex As Exception
                                End Try
                            End If
                        ElseIf isWeighingMachine And clsCommon.CompairString(MachineName, "Big Essae") = CompairStringResult.Equal Then '' For Essae MPD on 06/APR/2016 by Balwinder
                            If clsCommon.myLen(msg) > 0 Then
                                Try
                                    Dim reading As String = msg.Trim()
                                    Dim strBreak As String() = clsCommon.myCstr(reading).Split(New String() {" "}, StringSplitOptions.None)
                                    If strBreak.Length > 1 Then
                                        Dim strTemp As String = strBreak(1)
                                        _weight = clsCommon.myCdbl(strTemp)
                                        DisplayWeightData(_weight)
                                    End If
                                Catch ex As Exception
                                End Try
                            End If
                        ElseIf isWeighingMachine And clsCommon.CompairString(MachineName, "Essae Weighbridges") = CompairStringResult.Equal Then '' For Essae Weighbridge  MPD on 22/Nov/2016 by Balwinder
                            If clsCommon.myLen(msg) > 0 Then
                                Try
                                    If clsCommon.CompairString(msg, "40") = CompairStringResult.Equal Then
                                        Dim strTempBreak As String() = OldReading.Split(Environment.NewLine.ToCharArray())
                                        If strTempBreak.Length > 1 Then
                                            If clsCommon.myLen(strTempBreak(1)) > 0 Then
                                                _weight = clsCommon.myCdbl(strTempBreak(1))
                                                DisplayWeightData(_weight)
                                            End If
                                        End If
                                        OldReading = ""
                                    Else
                                        OldReading = OldReading + msg
                                        If OldReading.Contains(":") AndAlso OldReading.Contains("40") Then
                                            Dim strTempBreak As String() = OldReading.Split(Environment.NewLine.ToCharArray())
                                            If strTempBreak.Length > 2 Then
                                                If clsCommon.myLen(strTempBreak(1)) > 0 Then
                                                    _weight = clsCommon.myCdbl(strTempBreak(1))
                                                    DisplayWeightData(_weight)
                                                End If
                                            End If
                                            OldReading = ""
                                        End If
                                    End If
                                Catch ex As Exception
                                End Try
                            End If
                        ElseIf isWeighingMachine And clsCommon.CompairString(MachineName, "2 Essae Weighbridges") = CompairStringResult.Equal Then '' For Essae Weighbridge  MPD on 22/Nov/2016 by Balwinder
                            If clsCommon.myLen(msg) > 0 Then
                                Try
                                    OldReading = OldReading + msg
                                    If OldReading.Contains(":") AndAlso OldReading.Contains("40") Then
                                        Dim strTempBreak As String() = OldReading.Split(Environment.NewLine.ToCharArray())
                                        If strTempBreak.Length > 2 Then
                                            If clsCommon.myLen(strTempBreak(1)) > 0 Then
                                                _weight = clsCommon.myCdbl(strTempBreak(1))
                                                If _weight = 40 Then
                                                Else
                                                    DisplayWeightData(_weight)
                                                End If
                                            End If
                                        End If
                                        OldReading = ""
                                    End If
                                Catch ex As Exception
                                End Try
                            End If
                        ElseIf isWeighingMachine And clsCommon.CompairString(MachineName, "Mettler") = CompairStringResult.Equal Then '' For Mettler UDL on 10/Sep/2016 by Balwinder
                            If clsCommon.myLen(msg) > 0 Then
                                Try
                                    Dim reading As String = msg.Trim()
                                    Dim strBreak As String() = clsCommon.myCstr(reading).Split(New String() {" "}, StringSplitOptions.None)
                                    If strBreak.Length > 1 Then
                                        Dim strTemp As String = strBreak(1)
                                        If clsCommon.myLen(strTemp) = 12 Then
                                            strTemp = strTemp.Substring(0, 6)
                                            _weight = clsCommon.myCdbl(strTemp)
                                            DisplayWeightData(_weight)
                                        End If
                                    End If
                                Catch ex As Exception
                                End Try
                            End If
                        ElseIf isWeighingMachine And clsCommon.CompairString(MachineName, "Gold Weigh") = CompairStringResult.Equal Then
                            If clsCommon.myLen(msg) > 0 Then
                                Try
                                    OldReading += msg
                                    If OldReading.Contains("Kg") Then
                                        Dim reading As String = OldReading.Trim().Replace("Kg", "")
                                        If IsNumeric(reading) Then
                                            _weight = clsCommon.myCdbl(reading)
                                            DisplayWeightData(_weight)
                                        End If
                                        OldReading = ""
                                    End If
                                Catch ex As Exception
                                End Try
                            End If
                        ElseIf isWeighingMachine And clsCommon.CompairString(MachineName, "Combi") = CompairStringResult.Equal Then '' For Gold Weigh UDL on 10/Sep/2016 by Balwinder example 0004.9 Kg
                            ''3
                            ''37
                            ''0
                            ''k
                            ''g
                            If clsCommon.myLen(msg) > 0 Then
                                Try
                                    OldReading += msg
                                    If OldReading.Contains("kg") Then
                                        Dim reading As String = OldReading.Trim().Replace("kg", "")
                                        If IsNumeric(reading) Then
                                            _weight = clsCommon.myCdbl(reading)
                                            DisplayWeightData(_weight)
                                        End If
                                        OldReading = ""
                                    End If
                                Catch ex As Exception
                                End Try
                            End If
                        ElseIf isWeighingMachine And clsCommon.CompairString(MachineName, "KP GoldWeigh") = CompairStringResult.Equal Then '' For UDL on 30/Nov/2016 by Balwinder
                            ''msg=  [0005.1]  
                            If clsCommon.myLen(msg) > 0 Then
                                Try
                                    Dim reading As String = msg.Trim().Replace("[", "").Replace("]", "")
                                    If clsCommon.myCdbl(reading) >= 0 Then
                                        _weight = clsCommon.myCdbl(reading)
                                        DisplayWeightData(_weight)
                                    End If
                                Catch ex As Exception
                                End Try
                            End If
                        ElseIf isWeighingMachine And clsCommon.CompairString(MachineName, "Unknown") = CompairStringResult.Equal Then '' For Lacknow on 17/07/17 by Balwinder
                            If clsCommon.myLen(msg) > 0 Then
                                Try
                                    If OldReading.Contains("Wt:") Then
                                        OldReading += msg
                                        OldReading = Microsoft.VisualBasic.Mid(OldReading, 6, 7)
                                        _weight = clsCommon.myCdbl(OldReading)
                                        DisplayWeightData(_weight)
                                        OldReading = ""
                                    Else
                                        OldReading += msg
                                    End If
                                Catch ex As Exception
                                End Try
                            End If
                        ElseIf isWeighingMachine And clsCommon.CompairString(MachineName, "Digitron") = CompairStringResult.Equal Then '' For Milan on 10/01/2019 by balwinder
                            '0
                            '0
                            '0
                            '2
                            '.
                            '4
                            '0
                            If clsCommon.myLen(msg) > 0 Then
                                Try
                                    OldReading += msg
                                    If OldReading.Contains(".") Then
                                        Dim strBreak As String() = clsCommon.myCstr(OldReading).Split(New String() {"."}, StringSplitOptions.None)
                                        If strBreak.Count > 2 Then
                                            _weight = "0.00"
                                            OldReading = ""
                                        Else
                                            If clsCommon.myLen(strBreak(1)) = 2 Then
                                                _weight = OldReading.Trim()
                                                If IsNumeric(_weight) Then
                                                    _weight = clsCommon.myCdbl(_weight)
                                                    DisplayWeightData(_weight)
                                                End If
                                                OldReading = ""
                                            End If
                                        End If
                                    End If
                                Catch ex As Exception
                                End Try
                            End If
                        ElseIf isWeighingMachine And clsCommon.CompairString(MachineName, "AVR") = CompairStringResult.Equal Then
                            If clsCommon.myLen(msg) > 0 Then
                                Try
                                    ''
                                    ''   35110 kg  
                                    ''
                                    OldReading += msg
                                    If OldReading.Contains("kg") Then
                                        Try
                                            _weight = Microsoft.VisualBasic.Mid(OldReading, 5, 6)
                                        Catch ex As Exception
                                            _weight = "ABC"
                                        End Try
                                        If IsNumeric(_weight) Then
                                            DisplayWeightData(_weight)
                                        End If
                                        OldReading = ""
                                    End If
                                Catch ex As Exception
                                End Try
                            End If
                        ElseIf isWeighingMachine And clsCommon.CompairString(MachineName, "RKD") = CompairStringResult.Equal Then
                            If clsCommon.myLen(msg) > 0 Then
                                Try
                                    '    70gPH!v
                                    '    90iPH!x    90iPH!x
                                    '    90iPH!x
                                    '   130dPH!s
                                    _weight = Microsoft.VisualBasic.Mid(msg, 3, 6)
                                    _weight = _weight.Trim()
                                    If IsNumeric(_weight) Then
                                        _weight = clsCommon.myCdbl(_weight)
                                        DisplayWeightData(_weight)
                                    End If
                                Catch ex As Exception
                                End Try
                            End If
                        ElseIf isWeighingMachine AndAlso _IsAutoConfig Then '' For Auto Configuration
                            Dim IntPart As String = String.Empty
                            Dim FracPart As String = String.Empty
                            If _isContinuous = 1 Then
                                If clsCommon.myLen(msg) >= _FracFrom + IIf(_FracNoOfChar > 1, _FracNoOfChar, _FracNoOfChar + 1) Then
                                    If clsCommon.CompairString(Microsoft.VisualBasic.Mid(msg, _FracFrom + _FracNoOfChar, 1), _StartStopSym) = CompairStringResult.Equal Then
                                        Try
                                            IntPart = Microsoft.VisualBasic.Mid(msg, _IntFrom, _IntNoOfChar)
                                            IntPart = (IIf(clsCommon.myCdbl(IntPart) > 0 And clsCommon.myCdbl(IntPart) <= 9, "0" & clsCommon.myCdbl(IntPart), clsCommon.myCdbl(IntPart))).ToString
                                        Catch ex As Exception
                                        End Try

                                        Try
                                            FracPart = Microsoft.VisualBasic.Mid(msg, _FracFrom, _FracNoOfChar) & "0"
                                        Catch ex As Exception
                                        End Try
                                        _weight = IntPart & "." & FracPart
                                        If IsNumeric(_weight) AndAlso clsCommon.myCdbl(_weight) > 0 Then
                                            DisplayWeightData(_weight)
                                        End If
                                    End If
                                End If
                            Else
                                Try
                                    IntPart = Microsoft.VisualBasic.Mid(msg, _IntFrom, _IntNoOfChar)
                                    IntPart = (IIf(clsCommon.myCdbl(IntPart) > 0 And clsCommon.myCdbl(IntPart) <= 9, "0" & clsCommon.myCdbl(IntPart), clsCommon.myCdbl(IntPart))).ToString
                                Catch ex As Exception
                                End Try

                                Try
                                    FracPart = Microsoft.VisualBasic.Mid(msg, _FracFrom, _FracNoOfChar) & "0"
                                Catch ex As Exception
                                End Try
                                _weight = IntPart & "." & FracPart
                                If IsNumeric(_weight) AndAlso clsCommon.myCdbl(_weight) >= 0 Then
                                    If _isCheckForZero Then
                                        If clsCommon.myCdbl(_weight) >= 0 Then
                                            DisplayWeightData(_weight)
                                        End If
                                    Else
                                        If clsCommon.myCdbl(_weight) > 0 Then
                                            DisplayWeightData(_weight)
                                        End If
                                    End If
                                End If
                            End If
                        Else
                            _weight = (CDbl(msg)).ToString()
                            If IsNumeric(_weight) AndAlso clsCommon.myCdbl(_weight) > 0 Then
                                DisplayWeightData(_weight)
                            End If
                        End If
                    End If
                ElseIf CurrentTransmissionType = TransmissionType.Hex Then
                    Dim msg(comPort64.OutBufferCount) As Byte
                    msg = comPort64.readBytes(comPort64.OutBufferCount)
                    If msg.Length() > 0 Then
                        Try
                            If isShowUtility Then
                                Dim strReading As String = ""
                                For ii As Integer = 0 To msg.Length - 1
                                    strReading += msg(ii).ToString("X") + " "
                                Next
                                DisplayTextBox(strReading)
                            End If
                        Catch ex As Exception
                        End Try

                        If isEkoProMachine And clsCommon.CompairString(MachineName, "K") = CompairStringResult.Equal Then
                            If clsCommon.myCdbl(msg(0).ToString("X") & "." & msg(1).ToString("X")) > 12 OrElse clsCommon.myCdbl(msg(2).ToString("X") & "." & msg(3).ToString("X")) > 12 Then
                                Exit Sub
                            End If
                            _fat = IIf(clsCommon.myCdbl(msg(0).ToString("X")) <= 9, "0" & msg(0).ToString("X"), msg(0).ToString("X")) & "." & IIf(clsCommon.myCdbl(msg(1).ToString("X")) <= 9, "0" & msg(1).ToString("X"), msg(1).ToString("X"))
                            _snf = IIf(clsCommon.myCdbl(msg(2).ToString("X")) <= 9, "0" & msg(2).ToString("X"), msg(2).ToString("X")) & "." & IIf(clsCommon.myCdbl(msg(3).ToString("X")) <= 9, "0" & msg(3).ToString("X"), msg(3).ToString("X"))
                            If IsNumeric(_fat) AndAlso IsNumeric(_snf) Then
                                DisplayFATData(_fat)
                                DisplaySNFData(_snf)
                            End If
                        ElseIf isEkoProMachine And clsCommon.CompairString(MachineName, "EKB") = CompairStringResult.Equal Then
                            'Reading FAT 4.3 SNF 6.4
                            '4 30 6 40 20 72 30 2 
                            '1 0 81 0 3 80 15 0 
                            '5 0 0 0 0 0 0 0 
                            '0
                            intcountr += 1
                            If intcountr = 1 Then
                                If clsCommon.myCdbl(msg(0).ToString("X") & "." & msg(1).ToString("X")) > 12 OrElse clsCommon.myCdbl(msg(2).ToString("X") & "." & msg(3).ToString("X")) > 12 Then
                                    Exit Sub
                                End If
                                _fat = IIf(clsCommon.myCdbl(msg(0).ToString("X")) <= 9, "0" & msg(0).ToString("X"), msg(0).ToString("X")) & "." & IIf(clsCommon.myCdbl(msg(1).ToString("X")) <= 9, "0" & msg(1).ToString("X"), msg(1).ToString("X"))
                                _snf = IIf(clsCommon.myCdbl(msg(2).ToString("X")) <= 9, "0" & msg(2).ToString("X"), msg(2).ToString("X")) & "." & IIf(clsCommon.myCdbl(msg(3).ToString("X")) <= 9, "0" & msg(3).ToString("X"), msg(3).ToString("X"))
                                _fat = Math.Round(clsCommon.myCdbl(_fat), 1, MidpointRounding.ToEven)
                                _snf = Math.Round(clsCommon.myCdbl(_snf), 1, MidpointRounding.ToEven)
                                If IsNumeric(_fat) AndAlso IsNumeric(_snf) Then
                                    DisplayFATData(_fat)
                                    DisplaySNFData(_snf)
                                End If
                            ElseIf intcountr = 4 Then
                                intcountr = 0
                            End If
                        ElseIf isEkoProMachine And clsCommon.CompairString(MachineName, "EKB1") = CompairStringResult.Equal Then
                            Try
                                ''Reading (coming In Multiple lines)
                                ''1B 40 0A 20 20 20 20 20 50 72 6F 76 69 73 69 6F 6E 61 6C 20 41 63 6B 6E 6F 77 6C 65 64 67 65 6D 65 6E 74 20 53 6C 69 70 0D 0A 0D 0A 20 20 20 20 20 20 42 48 4F 4C 45 20 42 41 42 41 20 4D 49 4C 4B 20 46 4F 4F 44 20 50 56 54 20 4C 54 44 0D 0A 20 20 20 20 20 20 20 20 20 20 20 20 20 20 20 20 20 20 20 20 0D 0A 0D 0A 44 61 74 65 3A 20 31 33 2F 30 34 2F 32 31 20 20 20 20 20 20 20 20 20 20 54 69 6D 65 3A 20 31 39 3A 31 30 0D 0A 43 6F 64 65 3A 20 30 30 30 30 20 20 2E 2E 2E 2E 2E 2E 2E 2E 2E 2E 2E 2E 2E 2E 2E 2E 0D 0A 4E 61 6D 65 3A 20 2E 2E 2E 2E 2E 2E 2E 20 20 2E 2E 2E 2E 2E 2E 2E 0D 0A 4D 69 6C 6B 20 54 79 70 65 3A 20 20 20 20 4D 69 78 20 20 20 20 20 51 75 61 6E 74 69 74 79 3A 20 20 20 20 30 2E 30 30 0D 0A 46 41 54 3A 20 20 20 20 20 20 20 20 20 20 30 2E 38 25 20 20 20 20 53 4E 46 3A 20 20 20 20 20 20 20 20 20 31 32 2E 30 25 20 0D 0A 52 61 74 65 20 28 52 73 2E 29 3A 20 4E 2F 41 0D 0A 41 6D 6F 75 6E 74 3A 20 20 4E 2F 41 0D 0A 0D 0A 43 4C 52 3A 20 34 35 2E 39 20 0D 0A 0D 0A 0D 0A 53 68 69 66 74 3A 20 45 20 20 53 53 43 6F 75 6E 74 65 72 3A 20 33 32 0D 0A 0D 0A 0D 0A 0D 0A 0D 0A 0D 0A 0D 0A 
                                ''Result Convert hex to string
                                ' @
                                '     Provisional Acknowledgement Slip

                                '      BHOLE BABA MILK FOOD PVT LTD


                                'Date :  13/04/21          Time: 19:10
                                'Code: 0000  ................
                                'Name:                           ..............
                                'Milk Type:    Mix     Quantity:     0.00
                                'FAT:          0.8%    SNF:         12.0% 
                                'Rate(Rs.) : N/ A
                                'Amount:                         N/ A

                                'CLR: 45.9 


                                'Shift:                          E  SSCounter:  32
                                Dim strReading As String = ""
                                For ii As Integer = 0 To msg.Length - 1
                                    strReading += msg(ii).ToString("X") + " "
                                Next
                                OldReading += strReading
                                Dim strBreak As String() = clsCommon.myCstr(OldReading).Split(New String() {" "}, StringSplitOptions.None)
                                If strBreak.Count = 94 Then
                                    _fat = clsCommon.myCstr(clsCommon.myCdbl(strBreak(80).Replace("%", "")))
                                    _snf = clsCommon.myCstr(clsCommon.myCdbl(strBreak(93).Replace("%", "")))
                                    If IsNumeric(_fat) AndAlso IsNumeric(_snf) Then
                                        DisplayFATData(_fat)
                                        DisplaySNFData(_snf)
                                    End If
                                End If
                                If OldReading.Contains("SSCounter") Then
                                    OldReading = ""
                                End If
                            Catch ex As Exception
                            End Try
                        ElseIf isEkoProMachine And clsCommon.CompairString(MachineName, "B2") = CompairStringResult.Equal Then '' For Benny2 by balwinder on 04/01/2019
                            Try
                                ''Reading (coming In Multiple lines)
                                ''04 77 07 36 23 99 20 40 00 00 00 00 03 80 60 81 00 00 00 00 00 00 00 00 00 1B 40 0A 1D 61 04 20 20 20 20 20 50 72 6F 76 69 73 69 6F 6E 61 6C 20 41 63 6B 6E 6F 77 6C 65 64 67 65 6D 65 6E 74 20 53 6C 69 70 0D 0A 0D 0A 20 20 20 20 20 20 42 65 6E 6E 79 20 49 6D 70 65 78 20 50 72 69 76 61 74 65 20 4C 69 6D 69 74 65 64 0D 0A 20 20 20 20 20 20 20 20 20 20 20 20 20 20 20 4E 65 77 20 44 65 6C 68 69 0D 0A 0D 0A 44 61 74 65 3A 20 31 30 2F 31 31 2F 30 30 20 20 20 20 20 20 20 20 20 20 54 69 6D 65 3A 20 32 31 3A 35 36 0D 0A 43 6F 64 65 3A 20 2E 2E 2E 2E 2E 2E 2E 2E 2E 2E 0A 4D 69 6C 6B 20 54 79 70 65 3A 20 20 20 20 4D 69 78 20 20 20 20 20 51 75 61 6E 74 69 74 79 3A 2E 2E 2E 2E 2E 2E 2E 2E 2E 0D 0A 46 41 54 3A 20 20 20 20 20 20 20 20 20 34 2E 37 37 25 20 20 20 20 53 4E 46 3A 20 20 20 20 20 20 20 20 20 37 2E 33 36 25 0D 0A 52 61 74 65 20 28 52 73 2E 29 3A 20 2E 2E 2E 2E 2E 2E 2E 2E 2E 2E 41 6D 6F 75 6E 74 3A 2E 2E 2E 2E 2E 2E 2E 2E 2E 2E 2E 0A 0A 0A 0A 0A 0A 0A 
                                Dim strReading As String = ""
                                For ii As Integer = 0 To msg.Length - 1
                                    strReading += msg(ii).ToString("X") + " "
                                Next
                                OldReading += strReading
                                Dim strBreak As String() = clsCommon.myCstr(OldReading).Split(New String() {" "}, StringSplitOptions.None)
                                If strBreak.Count = 320 Then
                                    _fat = clsCommon.myCstr(clsCommon.myCdbl(strBreak(0))) + "." + strBreak(1).Substring(0, 1)
                                    _snf = clsCommon.myCstr(clsCommon.myCdbl(strBreak(2))) + "." + strBreak(3).Substring(0, 1)
                                    If IsNumeric(_fat) AndAlso IsNumeric(_snf) Then
                                        DisplayFATData(_fat)
                                        DisplaySNFData(_snf)
                                    End If
                                End If
                                If strBreak.Count >= 320 Then
                                    OldReading = ""
                                End If
                            Catch ex As Exception
                            End Try
                        ElseIf isEkoProMachine And clsCommon.CompairString(MachineName, "B3") = CompairStringResult.Equal Then '' For Benny3 by balwinder on 05/01/2019
                            Try
                                ''Reading (coming In Multiple lines)
                                ''04 77 07 36 23 99 20 40 00 00 00 00 03 80 60 81 00 00 00 00 00 00 00 00 00 1B 40 0A 1D 61 04 20 20 20 20 20 50 72 6F 76 69 73 69 6F 6E 61 6C 20 41 63 6B 6E 6F 77 6C 65 64 67 65 6D 65 6E 74 20 53 6C 69 70 0D 0A 0D 0A 20 20 20 20 20 20 42 65 6E 6E 79 20 49 6D 70 65 78 20 50 72 69 76 61 74 65 20 4C 69 6D 69 74 65 64 0D 0A 20 20 20 20 20 20 20 20 20 20 20 20 20 20 20 4E 65 77 20 44 65 6C 68 69 0D 0A 0D 0A 44 61 74 65 3A 20 31 30 2F 31 31 2F 30 30 20 20 20 20 20 20 20 20 20 20 54 69 6D 65 3A 20 32 31 3A 35 36 0D 0A 43 6F 64 65 3A 20 2E 2E 2E 2E 2E 2E 2E 2E 2E 2E 0A 4D 69 6C 6B 20 54 79 70 65 3A 20 20 20 20 4D 69 78 20 20 20 20 20 51 75 61 6E 74 69 74 79 3A 2E 2E 2E 2E 2E 2E 2E 2E 2E 0D 0A 46 41 54 3A 20 20 20 20 20 20 20 20 20 34 2E 37 37 25 20 20 20 20 53 4E 46 3A 20 20 20 20 20 20 20 20 20 37 2E 33 36 25 0D 0A 52 61 74 65 20 28 52 73 2E 29 3A 20 2E 2E 2E 2E 2E 2E 2E 2E 2E 2E 41 6D 6F 75 6E 74 3A 2E 2E 2E 2E 2E 2E 2E 2E 2E 2E 2E 0A 0A 0A 0A 0A 0A 0A 
                                Dim strReading As String = ""
                                For ii As Integer = 0 To msg.Length - 1
                                    strReading += msg(ii).ToString("X") + " "
                                Next
                                OldReading += strReading
                                Dim strBreak As String() = clsCommon.myCstr(OldReading).Split(New String() {" "}, StringSplitOptions.None)
                                If strBreak.Count = 73 Then
                                    _fat = clsCommon.myCstr(clsCommon.myCdbl(strBreak(0))) + "." + strBreak(1).Substring(0, 1)
                                    _snf = clsCommon.myCstr(clsCommon.myCdbl(strBreak(2))) + "." + strBreak(3).Substring(0, 1)
                                    If IsNumeric(_fat) AndAlso IsNumeric(_snf) Then
                                        DisplayFATData(_fat)
                                        DisplaySNFData(_snf)
                                    End If
                                End If
                                If strBreak.Count >= 73 Then
                                    OldReading = ""
                                End If
                            Catch ex As Exception
                            End Try
                        ElseIf isWeighingMachine And clsCommon.CompairString(MachineName, "SAR") = CompairStringResult.Equal Then
                            'Reading For 120 KG
                            '2
                            '31 68 
                            '1A 7F 20 20 31 32 30 3
                            If clsCommon.myCdbl((msg(0).ToString("X"))) = 2 Then
                                arrBy = Nothing
                            End If
                            If arrBy Is Nothing Then
                                arrBy = New List(Of String)
                            End If
                            Dim str1 As String = ""
                            For jj As Integer = 0 To msg.Length - 1
                                arrBy.Add(clsCommon.myCstr(msg(jj).ToString("X")))
                                str1 += clsCommon.myCstr(msg(jj).ToString("X"))
                            Next
                            If clsCommon.myCdbl((arrBy(arrBy.Count - 1))) = 3 Then
                                Try
                                    Dim str As String = ""
                                    If clsCommon.myCdbl(arrBy(arrBy.Count - 6)) > 30 Then
                                        str += clsCommon.myCstr(clsCommon.myCdbl(arrBy(arrBy.Count - 6)) - 30)
                                    Else
                                        str += "0"
                                    End If
                                    If clsCommon.myCdbl(arrBy(arrBy.Count - 5)) > 30 Then
                                        str += clsCommon.myCstr(clsCommon.myCdbl(arrBy(arrBy.Count - 5)) - 30)
                                    Else
                                        str += "0"
                                    End If
                                    If clsCommon.myCdbl(arrBy(arrBy.Count - 4)) > 30 Then
                                        str += clsCommon.myCstr(clsCommon.myCdbl(arrBy(arrBy.Count - 4)) - 30)
                                    Else
                                        str += "0"
                                    End If
                                    If clsCommon.myCdbl(arrBy(arrBy.Count - 3)) > 30 Then
                                        str += clsCommon.myCstr(clsCommon.myCdbl(arrBy(arrBy.Count - 3)) - 30)
                                    Else
                                        str += "0"
                                    End If
                                    If clsCommon.myCdbl(arrBy(arrBy.Count - 2)) > 30 Then
                                        str += clsCommon.myCstr(clsCommon.myCdbl(arrBy(arrBy.Count - 2)) - 30)
                                    Else
                                        str += "0"
                                    End If
                                    If IsNumeric(str) AndAlso clsCommon.myCdbl(str) >= 0 Then
                                        _weight = clsCommon.myCdbl(str)
                                        DisplayWeightData(_weight)
                                    End If
                                    arrBy = Nothing
                                Catch ex As Exception
                                End Try
                            End If
                        ElseIf isWeighingMachine And clsCommon.CompairString(MachineName, "USA") = CompairStringResult.Equal Then
                            'Reading For 16.7  
                            '2 20 30 
                            '30 30 
                            '31 
                            '36 
                            '37 3 D A 

                            If clsCommon.myCdbl((msg(0).ToString("X"))) = 2 Then
                                arrBy = Nothing
                            End If
                            If arrBy Is Nothing Then
                                arrBy = New List(Of String)
                            End If

                            For jj As Integer = 0 To msg.Length - 1
                                arrBy.Add(clsCommon.myCstr(msg(jj).ToString("X")))
                            Next
                            If clsCommon.CompairString(clsCommon.myCstr((arrBy(arrBy.Count - 1))), "A") = CompairStringResult.Equal Then
                                Try
                                    Dim str As String = ""
                                    If clsCommon.myCdbl(arrBy(arrBy.Count - 9)) > 30 Then
                                        str += clsCommon.myCstr(clsCommon.myCdbl(arrBy(arrBy.Count - 9)) - 30)
                                    Else
                                        str += "0"
                                    End If
                                    If clsCommon.myCdbl(arrBy(arrBy.Count - 8)) > 30 Then
                                        str += clsCommon.myCstr(clsCommon.myCdbl(arrBy(arrBy.Count - 8)) - 30)
                                    Else
                                        str += "0"
                                    End If
                                    If clsCommon.myCdbl(arrBy(arrBy.Count - 7)) > 30 Then
                                        str += clsCommon.myCstr(clsCommon.myCdbl(arrBy(arrBy.Count - 7)) - 30)
                                    Else
                                        str += "0"
                                    End If
                                    If clsCommon.myCdbl(arrBy(arrBy.Count - 6)) > 30 Then
                                        str += clsCommon.myCstr(clsCommon.myCdbl(arrBy(arrBy.Count - 6)) - 30)
                                    Else
                                        str += "0"
                                    End If
                                    If clsCommon.myCdbl(arrBy(arrBy.Count - 5)) > 30 Then
                                        str += clsCommon.myCstr(clsCommon.myCdbl(arrBy(arrBy.Count - 5)) - 30)
                                    Else
                                        str += "0"
                                    End If
                                    str += "."
                                    If clsCommon.myCdbl(arrBy(arrBy.Count - 4)) > 30 Then
                                        str += clsCommon.myCstr(clsCommon.myCdbl(arrBy(arrBy.Count - 4)) - 30)
                                    Else
                                        str += "0"
                                    End If
                                    If IsNumeric(str) AndAlso clsCommon.myCdbl(str) >= 0 Then
                                        'Convert KG reading To letter 
                                        Dim ltr As Decimal = Math.Round(clsCommon.myCdbl(str) / 1.03, 1, MidpointRounding.AwayFromZero)
                                        _weight = clsCommon.myCdbl(ltr)
                                        DisplayWeightData(_weight)
                                    End If
                                    arrBy = Nothing
                                Catch ex As Exception
                                End Try
                            End If

                        End If
                    End If
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub
   
    Dim arrBy As List(Of String) = Nothing

    'Public Sub comPort_2_DataReceived64()
    '    Try
    '        If Comm64.COMMEVENTS.comEvReceive Then
    '            If CurrentTransmissionType = TransmissionType.Text Then ' For Text Transmission type of machines
    '                Dim msg As String = comPort64_2.Input
    '                'msg = msg.Trim()
    '                If Microsoft.VisualBasic.Len(msg) > 0 Then
    '                    If isEkoProMachine And clsCommon.CompairString(MachineName, "E") = CompairStringResult.Equal Then '' For Everest Old milk analyzer
    '                        If clsCommon.CompairString(Microsoft.VisualBasic.Left(msg, 1), "(") = CompairStringResult.Equal Then
    '                            _fat = IIf(clsCommon.myCdbl(Microsoft.VisualBasic.Mid(msg, 2, 2)) <= 9, "0" & clsCommon.myCdbl(Microsoft.VisualBasic.Mid(msg, 2, 2)), clsCommon.myCdbl(Microsoft.VisualBasic.Mid(msg, 2, 2))) & "." & clsCommon.myCstr(Microsoft.VisualBasic.Mid(msg, 4, 2))
    '                            _snf = IIf(clsCommon.myCdbl(Microsoft.VisualBasic.Mid(msg, 6, 2)) <= 9, "0" & clsCommon.myCdbl(Microsoft.VisualBasic.Mid(msg, 6, 2)), clsCommon.myCdbl(Microsoft.VisualBasic.Mid(msg, 6, 2))) & "." & clsCommon.myCstr(Microsoft.VisualBasic.Mid(msg, 8, 2))
    '                            If clsCommon.myCdbl(_fat) > 12 OrElse clsCommon.myCdbl(_snf) > 12 Then
    '                                Exit Sub
    '                            End If
    '                            If IsNumeric(_fat) AndAlso IsNumeric(_snf) Then
    '                                DisplayFATData(_fat)
    '                                DisplaySNFData(_snf)
    '                            End If
    '                        End If

    '                    ElseIf isEkoProMachine And clsCommon.CompairString(MachineName, "F") = CompairStringResult.Equal Then '' For IndiFOSS on 16 Feb 2016 by balwinder
    '                        Try
    '                            Dim strBreak As String() = clsCommon.myCstr(OldReading + msg).Split(New String() {Environment.NewLine}, StringSplitOptions.None)
    '                            If strBreak.Length > 0 Then
    '                                Dim strTemp As String = strBreak(0)
    '                                Dim strTempBreak As String() = strTemp.Split(",")

    '                                OldReading = strTemp
    '                                If strTempBreak.Length = 12 Then
    '                                    _fat = strTempBreak(2)
    '                                    _snf = strTempBreak(3)

    '                                    _Protein = strTempBreak(4)

    '                                    _Adulterant = Nothing
    '                                    _Adulterant = New clsTempAdulterant()
    '                                    _Adulterant.Water = strTempBreak(5)
    '                                    _Adulterant.Urea = strTempBreak(6)
    '                                    _Adulterant.Maltodex = strTempBreak(7)
    '                                    _Adulterant.AmmSulph = strTempBreak(8)
    '                                    _Adulterant.Sucrose = strTempBreak(9)

    '                                    _Conclusion = Nothing
    '                                    _Conclusion = New clsTempConclusion()
    '                                    _Conclusion.Normal = strTempBreak(10)
    '                                    _Conclusion.Sample = strTempBreak(11)
    '                                    If IsNumeric(_fat) AndAlso IsNumeric(_snf) Then
    '                                        DisplayFATData(_fat)
    '                                        DisplaySNFData(_snf)
    '                                        DisplayProteinData()
    '                                        DisplayConclutionData()
    '                                        DisplayAdulterantData()
    '                                    End If
    '                                    OldReading = ""
    '                                End If
    '                                If strTempBreak.Length > 50 Then
    '                                    OldReading = ""
    '                                End If
    '                                strTemp = Nothing
    '                                strTempBreak = Nothing
    '                            End If
    '                            strBreak = Nothing
    '                        Catch ex As Exception
    '                        End Try
    '                    ElseIf isEkoProMachine And clsCommon.CompairString(MachineName, "N") = CompairStringResult.Equal Then '' Final Checked For Everest New Mchine
    '                        If clsCommon.CompairString(Microsoft.VisualBasic.Left(msg, 1), "(") = CompairStringResult.Equal Then
    '                            ''_fat = IIf(clsCommon.myCdbl(Microsoft.VisualBasic.Mid(msg, 2, 2)) <= 9, "0" &  Microsoft.VisualBasic.Mid(msg, 2, 2), Microsoft.VisualBasic.Mid(msg, 2, 2)) & "." & IIf(clsCommon.myCdbl(Microsoft.VisualBasic.Mid(msg, 4, 2)) <= 9, Microsoft.VisualBasic.Mid(msg, 4, 2) & "0", Microsoft.VisualBasic.Mid(msg, 4, 2))
    '                            '_fat = Microsoft.VisualBasic.Mid(msg, 2, 2) & "." & Microsoft.VisualBasic.Mid(msg, 4, 2)
    '                            '_snf = IIf(clsCommon.myCdbl(Microsoft.VisualBasic.Mid(msg, 6, 2)) <= 9, "0" & Microsoft.VisualBasic.Mid(msg, 6, 2), Microsoft.VisualBasic.Mid(msg, 6, 2)) & "." & IIf(clsCommon.myCdbl(Microsoft.VisualBasic.Mid(msg, 8, 2)) <= 9, Microsoft.VisualBasic.Mid(msg, 8, 2) & "0", Microsoft.VisualBasic.Mid(msg, 8, 2))
    '                            '_snf = Microsoft.VisualBasic.Mid(msg, 6, 2) & "." & Microsoft.VisualBasic.Mid(msg, 8, 2)
    '                            _fat = IIf(clsCommon.myCdbl(Microsoft.VisualBasic.Mid(msg, 2, 2)) <= 9, "0" & clsCommon.myCdbl(Microsoft.VisualBasic.Mid(msg, 2, 2)), clsCommon.myCdbl(Microsoft.VisualBasic.Mid(msg, 2, 2))) & "." & IIf(clsCommon.myCdbl(Microsoft.VisualBasic.Mid(msg, 4, 2)) <= 9, clsCommon.myCdbl(Microsoft.VisualBasic.Mid(msg, 4, 2)) & "0", clsCommon.myCdbl(Microsoft.VisualBasic.Mid(msg, 4, 2)))
    '                            _snf = IIf(clsCommon.myCdbl(Microsoft.VisualBasic.Mid(msg, 6, 2)) <= 9, "0" & clsCommon.myCdbl(Microsoft.VisualBasic.Mid(msg, 6, 2)), clsCommon.myCdbl(Microsoft.VisualBasic.Mid(msg, 6, 2))) & "." & IIf(clsCommon.myCdbl(Microsoft.VisualBasic.Mid(msg, 8, 2)) <= 9, clsCommon.myCdbl(Microsoft.VisualBasic.Mid(msg, 8, 2)) & "0", clsCommon.myCdbl(Microsoft.VisualBasic.Mid(msg, 8, 2)))
    '                            If clsCommon.myCdbl(_fat) > 12 OrElse clsCommon.myCdbl(_snf) > 12 Then
    '                                Exit Sub
    '                            End If
    '                            If IsNumeric(_fat) AndAlso IsNumeric(_snf) Then
    '                                DisplayFATData(_fat)
    '                                DisplaySNFData(_snf)
    '                            End If
    '                        End If
    '                    ElseIf isWeighingMachine And clsCommon.CompairString(MachineName, "P") = CompairStringResult.Equal Then '' For Prompt
    '                        'To be Uncommented
    '                        Dim IntPart As String = String.Empty
    '                        Dim FracPart As String = String.Empty
    '                        Try
    '                            IntPart = Microsoft.VisualBasic.Mid(msg, 2, 4)
    '                            IntPart = (IIf(clsCommon.myCdbl(IntPart) > 0 And clsCommon.myCdbl(IntPart) <= 9, "0" & clsCommon.myCdbl(IntPart), clsCommon.myCdbl(IntPart))).ToString
    '                        Catch ex As Exception
    '                        End Try

    '                        Try
    '                            FracPart = Microsoft.VisualBasic.Mid(msg, 6, 1) & "0"
    '                        Catch ex As Exception
    '                        End Try
    '                        _weight = IntPart & "." & FracPart
    '                        If IsNumeric(_weight) Then
    '                            DisplayWeightData(_weight)
    '                        End If

    '                        'end of to be uncommented



    '                        ' To be commented, dummy code for connecting Smart Weighning Machine at Goaba
    '                        'If clsCommon.CompairString(Microsoft.VisualBasic.Left(msg, 1), "N") = CompairStringResult.Equal And clsCommon.myLen(msg) > 1 Then
    '                        '    Dim IntPart As String = String.Empty
    '                        '    Dim FracPart As String = String.Empty
    '                        '    Try

    '                        '        IntPart = Microsoft.VisualBasic.Mid(msg, 2, 4)
    '                        '        IntPart = (IIf(clsCommon.myCdbl(IntPart) > 0 And clsCommon.myCdbl(IntPart) <= 9, "0" & clsCommon.myCdbl(IntPart), clsCommon.myCdbl(IntPart))).ToString

    '                        '    Catch ex As Exception
    '                        '    End Try

    '                        '    Try
    '                        '        FracPart = Microsoft.VisualBasic.Mid(msg, 7, 2) '& "0"
    '                        '    Catch ex As Exception
    '                        '    End Try
    '                        '    _weight = IntPart & "." & FracPart
    '                        '    If IsNumeric(_weight) Then
    '                        '        If clsCommon.myLen(IntPart) > 0 AndAlso clsCommon.myLen(FracPart) > 1 AndAlso clsCommon.myLen(_weight) > 0 Then
    '                        '            DisplayWeightData(_weight)
    '                        '        End If
    '                        '    End If
    '                        '    comPort64_2.sThreshold = True
    '                        'End If
    '                        ''end Of Code to be commented


    '                    ElseIf isWeighingMachine And clsCommon.CompairString(MachineName, "X") = CompairStringResult.Equal Then '' For Prompt Ajmer
    '                        Dim IntPart As String = String.Empty
    '                        Dim FracPart As String = String.Empty
    '                        Try
    '                            IntPart = Microsoft.VisualBasic.Mid(msg, 2, 5)
    '                            IntPart = (IIf(clsCommon.myCdbl(IntPart) > 0 And clsCommon.myCdbl(IntPart) <= 9, "0" & clsCommon.myCdbl(IntPart), clsCommon.myCdbl(IntPart))).ToString
    '                        Catch ex As Exception
    '                        End Try

    '                        Try
    '                            FracPart = Microsoft.VisualBasic.Mid(msg, 7, 1) & "0"
    '                        Catch ex As Exception
    '                        End Try
    '                        _weight = IntPart & "." & FracPart
    '                        If IsNumeric(_weight) Then
    '                            DisplayWeightData(_weight)
    '                        End If
    '                    ElseIf isWeighingMachine And clsCommon.CompairString(MachineName, "D") = CompairStringResult.Equal Then '' For Delta
    '                        If clsCommon.CompairString(Microsoft.VisualBasic.Left(msg, 1), "[") = CompairStringResult.Equal Then
    '                            Dim IntPart As String = String.Empty
    '                            Dim FracPart As String = String.Empty
    '                            Try
    '                                IntPart = Microsoft.VisualBasic.Mid(msg, 2, 5)
    '                                IntPart = (IIf(clsCommon.myCdbl(IntPart) > 0 And clsCommon.myCdbl(IntPart) <= 9, "0" & clsCommon.myCdbl(IntPart), clsCommon.myCdbl(IntPart))).ToString
    '                            Catch ex As Exception
    '                            End Try

    '                            Try
    '                                FracPart = Microsoft.VisualBasic.Mid(msg, 7, 1) & "0"
    '                            Catch ex As Exception
    '                            End Try
    '                            ' _weight = (Math.Round(CDbl(Microsoft.VisualBasic.Mid(msg, 2, 5) & "." & Microsoft.VisualBasic.Mid(msg, 7, 1)), 1).ToString())
    '                            ' _weight = clsCommon.myCdbl(Microsoft.VisualBasic.Mid(msg, 2, 5)).ToString() & "." & clsCommon.myCdbl(Microsoft.VisualBasic.Mid(msg, 7, 1)).ToString
    '                            '_weight = Format(_weight, "00.00")
    '                            _weight = IntPart & "." & FracPart
    '                            If IsNumeric(_weight) Then
    '                                DisplayWeightData(_weight)
    '                            End If
    '                        End If
    '                    ElseIf isWeighingMachine And clsCommon.CompairString(MachineName, "B") = CompairStringResult.Equal Then '' For Panasonic
    '                        If clsCommon.CompairString(Microsoft.VisualBasic.Left(msg, 1), "[") = CompairStringResult.Equal Then
    '                            Dim IntPart As String = String.Empty
    '                            Dim FracPart As String = String.Empty
    '                            Try
    '                                IntPart = Microsoft.VisualBasic.Mid(msg, 2, 4)
    '                                IntPart = (IIf(clsCommon.myCdbl(IntPart) > 0 And clsCommon.myCdbl(IntPart) <= 9, "0" & clsCommon.myCdbl(IntPart), clsCommon.myCdbl(IntPart))).ToString
    '                            Catch ex As Exception
    '                            End Try

    '                            Try
    '                                FracPart = Microsoft.VisualBasic.Mid(msg, 6, 1) & "0"
    '                            Catch ex As Exception
    '                            End Try
    '                            _weight = IntPart & "." & FracPart
    '                            If IsNumeric(_weight) Then
    '                                DisplayWeightData(_weight)
    '                            End If
    '                        End If
    '                    ElseIf isWeighingMachine And clsCommon.CompairString(MachineName, "Z") = CompairStringResult.Equal Then '' For Smart EWS
    '                        If clsCommon.myLen(msg) >= 8 Then
    '                            If clsCommon.CompairString(Microsoft.VisualBasic.Mid(msg, 8, 1), "=") = CompairStringResult.Equal Then
    '                                Dim IntPart As String = String.Empty
    '                                Dim FracPart As String = String.Empty
    '                                Try
    '                                    IntPart = Microsoft.VisualBasic.Mid(msg, 1, 5)
    '                                    IntPart = (IIf(clsCommon.myCdbl(IntPart) > 0 And clsCommon.myCdbl(IntPart) <= 9, "0" & clsCommon.myCdbl(IntPart), clsCommon.myCdbl(IntPart))).ToString
    '                                Catch ex As Exception
    '                                End Try

    '                                Try
    '                                    FracPart = Microsoft.VisualBasic.Mid(msg, 7, 1) & "0"
    '                                Catch ex As Exception
    '                                End Try
    '                                _weight = IntPart & "." & FracPart
    '                                If IsNumeric(_weight) Then
    '                                    DisplayWeightData(_weight)
    '                                End If
    '                                'Else
    '                                '    Dim IntPart As String = String.Empty
    '                                '    Dim FracPart As String = String.Empty
    '                                '    Try
    '                                '        IntPart = Microsoft.VisualBasic.Mid(msg, 2, 5)
    '                                '        IntPart = (IIf(clsCommon.myCdbl(IntPart) > 0 And clsCommon.myCdbl(IntPart) <= 9, "0" & clsCommon.myCdbl(IntPart), clsCommon.myCdbl(IntPart))).ToString
    '                                '    Catch ex As Exception
    '                                '    End Try

    '                                '    Try
    '                                '        FracPart = Microsoft.VisualBasic.Mid(msg, 8, 1) & "0"
    '                                '    Catch ex As Exception
    '                                '    End Try
    '                                '    _weight = IntPart & "." & FracPart
    '                                '    If IsNumeric(_weight) Then
    '                                '        DisplayWeightData(_weight)
    '                                '    End If
    '                            End If
    '                        End If

    '                    ElseIf isWeighingMachine AndAlso (clsCommon.CompairString(MachineName, "U") = CompairStringResult.Equal) Then '' For Kothputli Supertech
    '                        _weight = (CDbl(Microsoft.VisualBasic.Mid(msg, 2, 6))).ToString()
    '                        If IsNumeric(_weight) Then
    '                            DisplayWeightData(_weight)
    '                        End If
    '                    ElseIf isWeighingMachine AndAlso (clsCommon.CompairString(MachineName, "S") = CompairStringResult.Equal OrElse clsCommon.CompairString(MachineName, "T") = CompairStringResult.Equal) Then '' For Supertech
    '                        _weight = (CDbl(msg)).ToString()
    '                        If IsNumeric(_weight) Then
    '                            DisplayWeightData(_weight)
    '                        End If
    '                    ElseIf isWeighingMachine And clsCommon.CompairString(MachineName, "C") = CompairStringResult.Equal Then '' For Everest
    '                        Dim IntPart As String = String.Empty
    '                        Dim FracPart As String = String.Empty
    '                        Try
    '                            IntPart = Microsoft.VisualBasic.Mid(msg, 2, 3)
    '                            IntPart = (IIf(clsCommon.myCdbl(IntPart) > 0 And clsCommon.myCdbl(IntPart) <= 9, "0" & clsCommon.myCdbl(IntPart), clsCommon.myCdbl(IntPart))).ToString
    '                        Catch ex As Exception
    '                        End Try

    '                        Try
    '                            FracPart = Microsoft.VisualBasic.Mid(msg, 5, 1) & "0"
    '                        Catch ex As Exception
    '                        End Try
    '                        _weight = IntPart & "." & FracPart
    '                        '_weight = (Math.Round(CDbl(Microsoft.VisualBasic.Left(msg, 3) & "." & Microsoft.VisualBasic.Right(msg, 2)), 2)).ToString()
    '                        If IsNumeric(_weight) Then
    '                            DisplayWeightData(_weight)
    '                        End If
    '                    ElseIf isWeighingMachine AndAlso _IsAutoConfig Then '' For Auto Configuration
    '                        Dim IntPart As String = String.Empty
    '                        Dim FracPart As String = String.Empty
    '                        Try
    '                            IntPart = Microsoft.VisualBasic.Mid(msg, _IntFrom, _IntNoOfChar)
    '                            IntPart = (IIf(clsCommon.myCdbl(IntPart) > 0 And clsCommon.myCdbl(IntPart) <= 9, "0" & clsCommon.myCdbl(IntPart), clsCommon.myCdbl(IntPart))).ToString
    '                        Catch ex As Exception
    '                        End Try

    '                        Try
    '                            FracPart = Microsoft.VisualBasic.Mid(msg, _FracFrom, _FracNoOfChar) & "0"
    '                        Catch ex As Exception
    '                        End Try
    '                        _weight = IntPart & "." & FracPart
    '                        '_weight = (Math.Round(CDbl(Microsoft.VisualBasic.Left(msg, 3) & "." & Microsoft.VisualBasic.Right(msg, 2)), 2)).ToString()
    '                        If IsNumeric(_weight) AndAlso clsCommon.myCdbl(_weight) > 0 Then
    '                            DisplayWeightData(_weight)
    '                        End If

    '                    Else
    '                        _weight = (CDbl(msg)).ToString()
    '                        If IsNumeric(_weight) AndAlso clsCommon.myCdbl(_weight) > 0 Then
    '                            DisplayWeightData(_weight)
    '                        End If
    '                    End If
    '                End If
    '            ElseIf CurrentTransmissionType = TransmissionType.Hex Then
    '                Dim msg(comPort64_2.OutBufferCount) As Byte
    '                msg = comPort64_2.readBytes(comPort64_2.OutBufferCount)
    '                If msg.Length() > 0 Then
    '                    If isEkoProMachine And clsCommon.CompairString(MachineName, "K") = CompairStringResult.Equal Then
    '                        If clsCommon.myCdbl(msg(0).ToString("X") & "." & msg(1).ToString("X")) > 12 OrElse clsCommon.myCdbl(msg(2).ToString("X") & "." & msg(3).ToString("X")) > 12 Then
    '                            Exit Sub
    '                        End If
    '                        _fat = IIf(clsCommon.myCdbl(msg(0).ToString("X")) <= 9, "0" & msg(0).ToString("X"), msg(0).ToString("X")) & "." & IIf(clsCommon.myCdbl(msg(1).ToString("X")) <= 9, "0" & msg(1).ToString("X"), msg(1).ToString("X"))
    '                        _snf = IIf(clsCommon.myCdbl(msg(2).ToString("X")) <= 9, "0" & msg(2).ToString("X"), msg(2).ToString("X")) & "." & IIf(clsCommon.myCdbl(msg(3).ToString("X")) <= 9, "0" & msg(3).ToString("X"), msg(3).ToString("X"))
    '                        If IsNumeric(_fat) AndAlso IsNumeric(_snf) Then
    '                            DisplayFATData(_fat)
    '                            DisplaySNFData(_snf)
    '                        End If
    '                    End If
    '                End If
    '            End If
    '        End If
    '    Catch ex As Exception
    '    End Try
    'End Sub

    Private Sub DoDisplayLactose()
        Try
            _LblLactose.Text = _Lactose
        Catch ex As Exception

        End Try
    End Sub

    Private Sub DoDisplayTotalSolids()
        Try
            _LblTotalSolids.Text = _TotalSolids
        Catch ex As Exception

        End Try
    End Sub

    Private Sub DoDisplayFPD()
        Try
            _LblFPD.Text = _FPD
        Catch ex As Exception

        End Try
    End Sub

    Private Sub DoDisplayTotalAcidity()
        Try
            _LblTotalAcidity.Text = _TotalAcidity
        Catch ex As Exception

        End Try
    End Sub

    Private Sub DoDisplayDensity()
        Try
            _LblDensity.Text = _Density
        Catch ex As Exception

        End Try
    End Sub

    Private Sub DoDisplayFFA()
        Try
            _LblFFA.Text = _FFA
        Catch ex As Exception

        End Try
    End Sub

    Private Sub DoDisplayCitricAcids()
        Try
            _LblCitricAcids.Text = _CitricAcids
        Catch ex As Exception

        End Try
    End Sub

    Private Sub DoDisplayUrea()
        Try
            _LblUrea.Text = _Urea
        Catch ex As Exception

        End Try
    End Sub

    Private Sub DoDisplayCasein()
        Try
            _LblCasein.Text = _Casein
        Catch ex As Exception

        End Try
    End Sub

    Private Sub DoDisplayGlucose()
        Try
            _LblGlucose.Text = _Glucose
        Catch ex As Exception

        End Try
    End Sub

    Public Sub Comport_WriteData(ByVal InputString1 As String, ByVal InputString2 As String, ByVal InputString3 As String, ByVal InputString4 As String)
        Try
            If Comm64.COMMEVENTS.comEvSend Then
                Dim Buffer As Byte() = System.Text.Encoding.UTF8.GetBytes(Chr(&H2) + Chr(&H2) + InputString1 + Chr(&H12) + InputString2 + Chr(&H12) + InputString3 + Chr(&H12) + InputString4 + Chr(&H3) + Chr(&HA))
                comPort64.writeBytes(Buffer)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Public Sub Comport_WriteDataSuspend()
        Try
            If Comm64.COMMEVENTS.comEvSend Then
                Dim Buffer As Byte() = System.Text.Encoding.UTF8.GetBytes(Chr(&H2) + Chr(&H6) + Chr(&H6) + Chr(&H3) + Chr(&HA))
                comPort64.writeBytes(Buffer)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
    Public Sub Comport_WriteDataActive()
        Try
            If Comm64.COMMEVENTS.comEvSend Then
                Dim Buffer As Byte() = System.Text.Encoding.UTF8.GetBytes(Chr(&H2) + Chr(&H5) + Chr(&H6) + Chr(&H3) + Chr(&HA))
                comPort64.writeBytes(Buffer)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

End Class

Public Class clsWeighingMachine
    Public weight As Double = 0
    Public machineName As String = ""

    Public Function getData(ByVal comPort As String, ByVal bRate As String, ByVal Parity As String, ByVal dBits As String, ByVal sBits As String) As Double
        Try
            Dim objSerail As clsSerialPort = Nothing
            Dim obj As clsWeighingMachine = New clsWeighingMachine()
            objSerail = New clsSerialPort()
            objSerail.PortName = comPort
            objSerail.BaudRate = bRate
            objSerail.Parity = Parity
            objSerail.DataBits = dBits
            objSerail.StopBits = sBits
            objSerail.isWeighingMachine = True
            objSerail.isEkoProMachine = False


            If objSerail.OpenPort() Then

                Dim ts As DateTime = Now.AddSeconds(25)

                While Not objSerail.isDataReceived
                    If ts <= Now Then
                        clsCommon.MyMessageBoxShow("No Device Found Connected")
                        Return 0
                    End If
                End While
                obj = objSerail.objWeighing
                objSerail.isDataReceived = False
                Return obj.weight

            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
        Return 0
    End Function

    Public Function getData(ByVal comPort As String, ByVal bRate As String, ByVal Parity As String, ByVal dBits As String, ByVal sBits As String, ByVal machineName As String) As Double
        Try
            Dim objSerail As clsSerialPort = Nothing
            Dim obj As clsWeighingMachine = New clsWeighingMachine()
            objSerail = New clsSerialPort()
            objSerail.PortName = comPort
            objSerail.BaudRate = bRate
            objSerail.Parity = Parity
            objSerail.DataBits = dBits
            objSerail.StopBits = sBits
            objSerail.isWeighingMachine = True
            objSerail.isEkoProMachine = False


            If objSerail.OpenPort() Then

                Dim ts As DateTime = Now.AddSeconds(25)

                While Not objSerail.isDataReceived
                    If ts <= Now Then
                        clsCommon.MyMessageBoxShow("No Device Found Connected")
                        Return 0
                    End If
                End While
                obj = objSerail.objWeighing
                objSerail.isDataReceived = False
                Return obj.weight

            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
        Return 0
    End Function


    Public Function getData(ByVal comPort As String) As Double
        Dim Obj As clsPortSetting = clsPortSetting.getData(1, machineName)
        If Obj IsNot Nothing Then
            Return getData(comPort, Obj.baud_rate, Obj.parity, Obj.data_bits, Obj.stop_bits, Obj.Data_Form)
        Else
            'clsCommon.MyMessageBoxShow("Default Port Settings is Not Saved  For Your Machine to connect to Weighing Machine." & Environment.NewLine & "  Please Do it using Port setting Screen or Contact to administrator")
        End If
        Return 0
    End Function
    Public Function getData(ByVal comPort As String, ByVal brate As String) As Double
        Dim Obj As clsPortSetting = clsPortSetting.getData(1, machineName)
        If Obj IsNot Nothing Then
            Return getData(comPort, brate, Obj.parity, Obj.data_bits, Obj.stop_bits, Obj.Data_Form)
        Else
            clsCommon.MyMessageBoxShow("Default Port Settings is Not Saved  For Your Machine to connect to Weighing Machine." & Environment.NewLine & "  Please Do it using Port setting Screen or Contact to administrator")
        End If
        Return 0

    End Function
    Public Function getData(ByVal comPort As String, ByVal brate As String, ByVal parity As String) As Double
        Dim Obj As clsPortSetting = clsPortSetting.getData(1, machineName)
        If Obj IsNot Nothing Then
            Return getData(comPort, brate, parity, Obj.data_bits, Obj.stop_bits, Obj.Data_Form)
        Else
            clsCommon.MyMessageBoxShow("Default Port Settings is Not Saved  For Your Machine to connect to Weighing Machine." & Environment.NewLine & "  Please Do it using Port setting Screen or Contact to administrator")
        End If
        Return 0
    End Function
    Public Function getData(ByVal comPort As String, ByVal brate As String, ByVal parity As String, ByVal dbits As String) As Double
        Dim Obj As clsPortSetting = clsPortSetting.getData(1, machineName)
        If Obj IsNot Nothing Then
            Return getData(comPort, brate, parity, dbits, Obj.stop_bits, Obj.Data_Form)
        Else
            clsCommon.MyMessageBoxShow("Default Port Settings is Not Saved  For Your Machine to connect to Weighing Machine." & Environment.NewLine & "  Please Do it using Port setting Screen or Contact to administrator")
        End If
        Return 0
    End Function

End Class

Public Class clsPortSetting
    Public ip_address As String = String.Empty
    Public mac_address As String = String.Empty
    Public machine_name As String = String.Empty
    Public baud_rate As String = String.Empty
    Public data_bits As String = String.Empty
    Public stop_bits As String = String.Empty
    Public parity As String = String.Empty
    Public created_by As String = String.Empty
    Public created_date As String = String.Empty
    Public modified_by As String = String.Empty
    Public modified_date As String = String.Empty
    Public comp_code As String = String.Empty
    Public Machine_Type As Integer = 0
    Public isNewEntry As Boolean = False
    '============Rohit,Instructed by Ranjana Mam ===============
    Public Data_Form As String = String.Empty
    Public machinename As String = String.Empty
    '========================================================
    Public Shared Function getData(ByVal mtype As Integer) As clsPortSetting
        Dim obj As clsPortSetting = Nothing
        Dim networkcard() As NetworkInterface = NetworkInterface.GetAllNetworkInterfaces()
        'Dim qry As String = "select * from tspl_port_setting where mac_address='" & networkcard(0).GetPhysicalAddress.ToString() & "' and machine_name='" & My.User.Name & "' and ip_address='" & System.Net.Dns.GetHostByName(System.Net.Dns.GetHostName()).AddressList(0).ToString() & "' and machine_type='" & mtype & "'"
        Dim qry As String = "select * from tspl_port_setting where mac_address='" & networkcard(0).GetPhysicalAddress.ToString() & "' and machine_name='" & My.User.Name & "' and ip_address='" & clsPortSetting.getMachineIP() & "' and machine_type='" & mtype & "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            obj = New clsPortSetting()
            obj.machine_name = dt.Rows(0)("Machine_Name")
            obj.ip_address = dt.Rows(0)("IP_Address")
            obj.mac_address = dt.Rows(0)("MAC_Address")
            obj.baud_rate = dt.Rows(0)("baud_rate")
            obj.data_bits = dt.Rows(0)("data_bits")
            obj.stop_bits = dt.Rows(0)("stop_bits")
            obj.parity = dt.Rows(0)("parity")
            '==========Rohit==============
            obj.machinename = dt.Rows(0)("MachineName")
            obj.Data_Form = dt.Rows(0)("Data_Form")
            '===========================

        End If
        Return obj

    End Function

    Public Shared Function getData(ByVal mtype As Integer, ByVal MachineName As String) As clsPortSetting
        Dim obj As clsPortSetting = Nothing
        Dim networkcard() As NetworkInterface = NetworkInterface.GetAllNetworkInterfaces()
        Dim machinePrefix As String = String.Empty
        machinePrefix = clsPortSetting.getMachineMakePrefix(MachineName, mtype)
        MachineName = machinePrefix
        'Dim qry As String = "select * from tspl_port_setting where mac_address='" & networkcard(0).GetPhysicalAddress.ToString() & "' and machine_name='" & My.User.Name & "' and ip_address='" & System.Net.Dns.GetHostByName(System.Net.Dns.GetHostName()).AddressList(0).ToString() & "' and machine_type='" & mtype & "'  and MachineName='" & MachineName & "'"
        Dim qry As String = "select * from tspl_port_setting where  machine_type='" & mtype & "'  and MachineName='" & MachineName & "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            obj = New clsPortSetting()
            obj.machine_name = dt.Rows(0)("Machine_Name")
            obj.ip_address = dt.Rows(0)("IP_Address")
            obj.mac_address = dt.Rows(0)("MAC_Address")
            obj.baud_rate = dt.Rows(0)("baud_rate")
            obj.data_bits = dt.Rows(0)("data_bits")
            obj.stop_bits = dt.Rows(0)("stop_bits")
            obj.parity = dt.Rows(0)("parity")
            '==========Rohit==============
            obj.machinename = dt.Rows(0)("MachineName")
            obj.Data_Form = dt.Rows(0)("Data_Form")
            '===========================K-Kanha,P-Prompt,D-Delta,B-Panasonic,S-Supertec,E-Everest(Milk analyzer Old),N-Everest(Milk analyzer New),C-Everest(weighing) 

        End If
        Return obj
    End Function

    Public Shared Function getDataMachineWise(ByVal mtype As Integer, ByVal MachineName As String) As clsPortSetting
        Dim obj As clsPortSetting = Nothing
        Dim networkcard() As NetworkInterface = NetworkInterface.GetAllNetworkInterfaces()
        Dim qry As String = "select * from tspl_port_setting where mac_address='" & networkcard(0).GetPhysicalAddress.ToString() & "' " _
        & " and machine_name='" & My.User.Name & "' and ip_address='" & clsPortSetting.getMachineIP() & "' " _
        & " and machine_type='" & mtype & "' and machineName='" & MachineName & "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            obj = New clsPortSetting()
            obj.machine_name = dt.Rows(0)("Machine_Name")
            obj.ip_address = dt.Rows(0)("IP_Address")
            obj.mac_address = dt.Rows(0)("MAC_Address")
            obj.baud_rate = dt.Rows(0)("baud_rate")
            obj.data_bits = dt.Rows(0)("data_bits")
            obj.stop_bits = dt.Rows(0)("stop_bits")
            obj.parity = dt.Rows(0)("parity")
            '==========Rohit==============
            obj.machinename = dt.Rows(0)("MachineName")
            obj.Data_Form = dt.Rows(0)("Data_Form")
            '===========================
        End If
        Return obj

    End Function

    Public Shared Function GetMachineType(ByVal Cmb As common.Controls.MyComboBox)
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow = dt.NewRow()
        dr("Code") = "Y"
        dr("Name") = "Benny"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "B2"
        dr("Name") = "Benny2"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "B3"
        dr("Name") = "Benny3"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "B4"
        dr("Name") = "Benny4"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "EKB"
        dr("Name") = "EKOMILK Bond"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "EKB1"
        dr("Name") = "EKOMILK Bond1"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "E"
        dr("Name") = "Everest Old"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "N"
        dr("Name") = "Everest New"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "K"
        dr("Name") = "Kanha"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "F"
        dr("Name") = "indiFOSS"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "G"
        dr("Name") = "FOSS FT1"
        dt.Rows.Add(dr)

       

        Cmb.DataSource = dt
        Cmb.ValueMember = "Code"
        Cmb.DisplayMember = "Name"
        Cmb.SelectedValue = 0

        Return Cmb
    End Function

    Public Shared Function GetWeighingMachineNames(ByVal Cmb As common.Controls.MyComboBox)
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow = dt.NewRow()
        dr("Code") = "P"
        dr("Name") = "Prompt"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "D"
        dr("Name") = "Delta"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "B"
        dr("Name") = "Panasonic"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "S"
        dr("Name") = "Supertech"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "U"
        dr("Name") = "Kotputli Supertech"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "C"
        dr("Name") = "Everest"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "X"
        dr("Name") = "Prompt Ajmer"
        dt.Rows.Add(dr)


        dr = dt.NewRow()
        dr("Code") = "Y"
        dr("Name") = "Benny"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Z"
        dr("Name") = "Smart EWS"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Z1"
        dr("Name") = "1 Smart EWS"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Z2"
        dr("Name") = "2 Smart EWS"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "M"
        dr("Name") = "Mettler"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "G"
        dr("Name") = "Gold Weigh"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "L"
        dr("Name") = "Essae Weighbridges"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "A"
        dr("Name") = "KP GoldWeigh"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "AVR"
        dr("Name") = "Avery"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "SAR"
        dr("Name") = "Sartoriusintec"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "IT"
        dr("Name") = "Intech"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "RKD"
        dr("Name") = "RKD"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "CM"
        dr("Name") = "Combi"
        dt.Rows.Add(dr)

        If Not clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "KL") = CompairStringResult.Equal Then
            dr = dt.NewRow()
            dr("Code") = "USA"
            dr("Name") = "USHA"
            dt.Rows.Add(dr)
        End If

        Dim qry As String = "select * from TSPL_MACHINE_INTEGRATION where type=0"
        Dim dtt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dtt IsNot Nothing AndAlso dtt.Rows.Count > 0 Then
            For i As Integer = 0 To dtt.Rows.Count - 1
                dr = dt.NewRow()
                dr("Code") = dtt.Rows(i)("Code")
                dr("Name") = dtt.Rows(i)("Description")
                dt.Rows.Add(dr)
            Next
        End If
        Cmb.DataSource = dt
        Cmb.ValueMember = "Code"
        Cmb.DisplayMember = "Name"
        Cmb.SelectedValue = 0

        Return Cmb
    End Function

    Public Shared Function GetDataFormType(ByVal Cmb As common.Controls.MyComboBox)
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow = dt.NewRow()
        dr("Code") = "T"
        dr("Name") = "Text"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "H"
        dr("Name") = "Hexa-Decimal"
        dt.Rows.Add(dr)

        Cmb.DataSource = dt
        Cmb.ValueMember = "Code"
        Cmb.DisplayMember = "Name"
        Cmb.SelectedValue = 0

        Return Cmb

    End Function


    Public Shared Function SaveData(ByVal obj As clsPortSetting, ByVal trans As SqlTransaction) As Boolean
        Dim issaved As Boolean = True
        Try

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "baud_rate", obj.baud_rate)
            clsCommon.AddColumnsForChange(coll, "data_bits", obj.data_bits)
            clsCommon.AddColumnsForChange(coll, "stop_bits", obj.stop_bits)
            clsCommon.AddColumnsForChange(coll, "parity", obj.parity)
            clsCommon.AddColumnsForChange(coll, "ip_address", obj.ip_address)
            clsCommon.AddColumnsForChange(coll, "mac_address", obj.mac_address)
            clsCommon.AddColumnsForChange(coll, "machine_name", obj.machine_name)
            '===============Rohit===============================
            clsCommon.AddColumnsForChange(coll, "machinename", obj.machinename)
            clsCommon.AddColumnsForChange(coll, "Data_Form", obj.Data_Form)
            '=========================================================
            clsCommon.AddColumnsForChange(coll, "Machine_Type", obj.Machine_Type)
            clsCommon.AddColumnsForChange(coll, "modified_by", obj.modified_by)
            clsCommon.AddColumnsForChange(coll, "modified_date", obj.modified_date)
            clsCommon.AddColumnsForChange(coll, "comp_code", obj.comp_code)
            If obj.isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", obj.created_by)
                clsCommon.AddColumnsForChange(coll, "Created_Date", obj.created_date)
                issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, "tspl_port_setting", OMInsertOrUpdate.Insert, "", trans)
            Else
                'issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, "tspl_port_setting", OMInsertOrUpdate.Update, "tspl_port_setting.mac_address='" & obj.mac_address & "' and tspl_port_setting.machine_name='" & obj.machine_name & "' and tspl_port_setting.ip_address='" & obj.ip_address & "' and tspl_port_setting.machine_type='" & obj.Machine_Type & "' and tspl_port_setting.machineName='" & obj.machinename & "'", trans)
                issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, "tspl_port_setting", OMInsertOrUpdate.Update, " tspl_port_setting.machine_type='" & obj.Machine_Type & "' and tspl_port_setting.machineName='" & obj.machinename & "'", trans)
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
        Return issaved
    End Function


    Public Shared Function deleteData(ByVal mtype As Integer) As Boolean
        Try
            Dim isdeleted As Boolean = True
            Dim networkcard() As NetworkInterface = NetworkInterface.GetAllNetworkInterfaces()
            'Dim qry As String = "delete from tspl_port_setting where mac_address='" & networkcard(0).GetPhysicalAddress.ToString() & "' and machine_name='" & My.User.Name & "' and ip_address='" & System.Net.Dns.GetHostByName(System.Net.Dns.GetHostName()).AddressList(0).ToString() & "' and machine_type='" & mtype & "'"
            Dim qry As String = "delete from tspl_port_setting where mac_address='" & networkcard(0).GetPhysicalAddress.ToString() & "' and machine_name='" & My.User.Name & "' and ip_address='" & clsPortSetting.getMachineIP() & "' and machine_type='" & mtype & "'"
            isdeleted = isdeleted AndAlso clsDBFuncationality.ExecuteNonQuery(qry)
            Return isdeleted
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function
    Public Shared Function getMachineName(Optional trans As SqlTransaction = Nothing) As String
        Dim rValue As String = String.Empty
        rValue = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" SELECT  HOST_NAME() AS Client_Machin_Name", trans))
        Return rValue
    End Function

    Public Shared Function getMachineIP(Optional trans As SqlTransaction = Nothing) As String
        Dim rValue As String = String.Empty
        rValue = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" SELECT  CONNECTIONPROPERTY('client_net_address') AS CLIENT_IP_ADDRESS ", trans))
        Return rValue
    End Function
    Public Shared Function deleteData(ByVal mtype As Integer, ByVal MachineName As String) As Boolean
        Try
            Dim isdeleted As Boolean = True
            Dim networkcard() As NetworkInterface = NetworkInterface.GetAllNetworkInterfaces()
            'Dim qry As String = "delete from tspl_port_setting where mac_address='" & networkcard(0).GetPhysicalAddress.ToString() & "' and machine_name='" & My.User.Name & "' and ip_address='" & System.Net.Dns.GetHostByName(System.Net.Dns.GetHostName()).AddressList(0).ToString() & "' and machine_type='" & mtype & "' and MachineName='" & MachineName & "'"
            Dim qry As String = "delete from tspl_port_setting where  machine_type='" & mtype & "' and MachineName='" & MachineName & "'"
            isdeleted = isdeleted AndAlso clsDBFuncationality.ExecuteNonQuery(qry)
            Return isdeleted
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Shared Function getMachineMakePrefix(ByVal _machine_Name As String, ByVal machineType As Integer) As String
        'machineType -0 -Milk analyzer
        'machineType -1 -Weighing machine
        Dim rValue As String = ""
        If clsCommon.CompairString(_machine_Name, "kanha") = CompairStringResult.Equal And machineType = 0 Then
            rValue = "k"
        ElseIf clsCommon.CompairString(_machine_Name, "everest old") = CompairStringResult.Equal And machineType = 0 Then
            rValue = "E"
        ElseIf clsCommon.CompairString(_machine_Name, "everest new") = CompairStringResult.Equal And machineType = 0 Then
            rValue = "N"
        ElseIf clsCommon.CompairString(_machine_Name, "Prompt") = CompairStringResult.Equal And machineType = 1 Then
            rValue = "P"
        ElseIf clsCommon.CompairString(_machine_Name, "Delta") = CompairStringResult.Equal And machineType = 1 Then
            rValue = "D"
        ElseIf clsCommon.CompairString(_machine_Name, "Panasonic") = CompairStringResult.Equal And machineType = 1 Then
            rValue = "B"
        ElseIf clsCommon.CompairString(_machine_Name, "Supertech") = CompairStringResult.Equal And machineType = 1 Then
            rValue = "S"
        ElseIf clsCommon.CompairString(_machine_Name, "Kotputli Supertech") = CompairStringResult.Equal And machineType = 1 Then
            rValue = "U"
        ElseIf clsCommon.CompairString(_machine_Name, "Prompt Ajmer") = CompairStringResult.Equal And machineType = 1 Then
            rValue = "X"
        ElseIf clsCommon.CompairString(_machine_Name, "everest") = CompairStringResult.Equal And machineType = 1 Then
            rValue = "C"
        ElseIf clsCommon.CompairString(_machine_Name, "Benny") = CompairStringResult.Equal And machineType = 1 Then
            rValue = "Y"
        ElseIf clsCommon.CompairString(_machine_Name, "Benny") = CompairStringResult.Equal And machineType = 0 Then
            rValue = "Y"
        ElseIf clsCommon.CompairString(_machine_Name, "Smart EWS") = CompairStringResult.Equal And machineType = 1 Then
            rValue = "Z"
        ElseIf clsCommon.CompairString(_machine_Name, "1 Smart EWS") = CompairStringResult.Equal And machineType = 1 Then
            rValue = "Z1"
        ElseIf clsCommon.CompairString(_machine_Name, "2 Smart EWS") = CompairStringResult.Equal And machineType = 1 Then
            rValue = "Z2"
        ElseIf clsCommon.CompairString(_machine_Name, "Intech") = CompairStringResult.Equal And machineType = 1 Then
            rValue = "IT"
        ElseIf clsCommon.CompairString(_machine_Name, "indiFOSS") = CompairStringResult.Equal And machineType = 0 Then
            rValue = "F"
        ElseIf clsCommon.CompairString(_machine_Name, "FOSS FT1") = CompairStringResult.Equal And machineType = 0 Then
            rValue = "G"
        ElseIf clsCommon.CompairString(_machine_Name, "Mettler") = CompairStringResult.Equal And machineType = 0 Then
            rValue = "M"
        ElseIf clsCommon.CompairString(_machine_Name, "Gold Weigh") = CompairStringResult.Equal And machineType = 0 Then
            rValue = "G"
        ElseIf clsCommon.CompairString(_machine_Name, "Combi") = CompairStringResult.Equal And machineType = 0 Then
            rValue = "CM"
        ElseIf clsCommon.CompairString(_machine_Name, "Essae Weighbridges") = CompairStringResult.Equal And machineType = 0 Then
            rValue = "L"
        ElseIf clsCommon.CompairString(_machine_Name, "KP GoldWeigh") = CompairStringResult.Equal And machineType = 0 Then
            rValue = "A"
        ElseIf clsCommon.CompairString(_machine_Name, "Avery") = CompairStringResult.Equal And machineType = 1 Then
            rValue = "AVR"
        ElseIf clsCommon.CompairString(_machine_Name, "RKD") = CompairStringResult.Equal And machineType = 1 Then
            rValue = "RKD"
        ElseIf clsCommon.CompairString(_machine_Name, "EKOMILK Bond") = CompairStringResult.Equal And machineType = 0 Then
            rValue = "EKB"
        ElseIf clsCommon.CompairString(_machine_Name, "EKOMILK Bond1") = CompairStringResult.Equal And machineType = 0 Then
            rValue = "EKB1"
        ElseIf clsCommon.CompairString(_machine_Name, "Benny2") = CompairStringResult.Equal And machineType = 0 Then
            rValue = "B2"
        ElseIf clsCommon.CompairString(_machine_Name, "Benny3") = CompairStringResult.Equal And machineType = 0 Then
            rValue = "B3"
        ElseIf clsCommon.CompairString(_machine_Name, "Benny4") = CompairStringResult.Equal And machineType = 0 Then
            rValue = "B4"
        ElseIf clsCommon.CompairString(_machine_Name, "Sartoriusintec") = CompairStringResult.Equal And machineType = 1 Then
            rValue = "SAR"
        ElseIf clsCommon.CompairString(_machine_Name, "USHA") = CompairStringResult.Equal And machineType = 1 Then
            rValue = "USA"
        Else
            rValue = _machine_Name
        End If
        Return rValue
    End Function
    Public Shared Function getMachineDataFormat(ByVal _machine_Name As String) As String
        Dim rValue As String = ""
        If clsCommon.CompairString(_machine_Name, "kanha") = CompairStringResult.Equal Then
            rValue = "H"
        ElseIf clsCommon.CompairString(_machine_Name, "EKOMILK Bond") = CompairStringResult.Equal Then
            rValue = "H"
        ElseIf clsCommon.CompairString(_machine_Name, "Benny2") = CompairStringResult.Equal Then
            rValue = "H"
        ElseIf clsCommon.CompairString(_machine_Name, "Benny3") = CompairStringResult.Equal Then
            rValue = "H"
        ElseIf clsCommon.CompairString(_machine_Name, "Sartoriusintec") = CompairStringResult.Equal Then
            rValue = "H"
        ElseIf clsCommon.CompairString(_machine_Name, "USHA") = CompairStringResult.Equal Then
            rValue = "H"
        Else
            rValue = "T"
        End If
        Return rValue
    End Function
End Class

Public Class clsMachineIntegration
    Public Code As String = String.Empty
    Public Description As String = String.Empty
    Public Type As Integer = 0
    Public isContinuousDataReading As Integer = 0
    Public Input_String As String = ""
    Public DataSample As String = String.Empty
    Public StartStopCharacter As String = String.Empty
    Public IntFromPos As Integer = 0
    Public IntNoOfChar As Integer = 0
    Public FracFromPos As Integer = 0
    Public FracNoOfChar As Integer = 0
    Public Check_For_Zero As Boolean = False
    ' Table Used TSPL_MACHINE_INTEGRATION
    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = " select Code as MachineCode , Description as MachineName, case when type=1 then 'Analyzer' else 'EWS' end as MachineType from TSPL_MACHINE_INTEGRATION"
        str = clsCommon.ShowSelectForm("FndMachine", qry, "MachineCode", whrcls, curcode, "MachineCode", isButtonClicked)
        Return str
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsMachineIntegration
        Return GetData(strCode, NavType, Nothing)
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsMachineIntegration
        Dim obj As clsMachineIntegration = Nothing
        Try
            Dim qry As String = "select * from TSPL_MACHINE_INTEGRATION  where 2=2"
            Select Case NavType
                Case NavigatorType.First
                    qry += " and Code = (select MIN(Code) from TSPL_MACHINE_INTEGRATION)"
                Case NavigatorType.Last
                    qry += " and Code = (select Max(Code) from TSPL_MACHINE_INTEGRATION)"
                Case NavigatorType.Next
                    qry += " and Code = (select Min(Code) from TSPL_MACHINE_INTEGRATION where  Code>'" + strCode + "')"
                Case NavigatorType.Previous
                    qry += " and Code = (select Max(Code) from TSPL_MACHINE_INTEGRATION where Code<'" + strCode + "')"
                Case NavigatorType.Current
                    qry += " and Code = '" + strCode + "'"
            End Select
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj = New clsMachineIntegration()
                obj.Code = clsCommon.myCstr(dt.Rows(0)("Code"))
                obj.Description = clsCommon.myCstr(dt.Rows(0)("Description"))
                obj.Type = clsCommon.myCdbl(dt.Rows(0)("Type"))
                obj.isContinuousDataReading = clsCommon.myCdbl(dt.Rows(0)("isContinuousDataReading"))
                obj.DataSample = dt.Rows(0)("DataSample")
                obj.StartStopCharacter = clsCommon.myCstr(dt.Rows(0)("StartStopCharacter"))
                obj.IntFromPos = clsCommon.myCdbl(dt.Rows(0)("IntFromPos"))
                obj.IntNoOfChar = clsCommon.myCdbl(dt.Rows(0)("IntNoOfChar"))
                obj.FracFromPos = clsCommon.myCdbl(dt.Rows(0)("FracFromPos"))
                obj.FracNoOfChar = clsCommon.myCstr(dt.Rows(0)("FracNoOfChar"))
                obj.Input_String = clsCommon.myCstr(dt.Rows(0)("Input_String"))
                obj.Check_For_Zero = IIf(clsCommon.myCdbl(dt.Rows(0)("Check_For_Zero")) = 1, True, False)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return obj
    End Function
    Public Shared Function SaveData(ByVal obj As clsMachineIntegration, ByVal isNewEntry As Boolean, Optional trans As SqlTransaction = Nothing) As Boolean
        Dim isSaved As Boolean = True
        Try
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Description", obj.Description)
            clsCommon.AddColumnsForChange(coll, "Type", obj.Type)
            clsCommon.AddColumnsForChange(coll, "isContinuousDataReading", obj.isContinuousDataReading)
            clsCommon.AddColumnsForChange(coll, "DataSample", obj.DataSample)
            clsCommon.AddColumnsForChange(coll, "StartStopCharacter", obj.StartStopCharacter)
            clsCommon.AddColumnsForChange(coll, "IntFromPos", obj.IntFromPos)
            clsCommon.AddColumnsForChange(coll, "IntNoOfChar", obj.IntNoOfChar)
            clsCommon.AddColumnsForChange(coll, "FracFromPos", obj.FracFromPos)
            clsCommon.AddColumnsForChange(coll, "FracNoOfChar", obj.FracNoOfChar)
            clsCommon.AddColumnsForChange(coll, "Input_String", obj.Input_String)
            clsCommon.AddColumnsForChange(coll, "Check_For_Zero", IIf(obj.Check_For_Zero, 1, 0))
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
            If isNewEntry Then
                Dim qry As String = "select max(Code) from TSPL_MACHINE_INTEGRATION"
                obj.Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
                If clsCommon.myLen(obj.Code) <= 0 Then
                    obj.Code = "M00000000001"
                Else
                    obj.Code = clsCommon.incval(obj.Code)
                End If
                clsCommon.AddColumnsForChange(coll, "Code", obj.Code)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MACHINE_INTEGRATION", OMInsertOrUpdate.Insert, "", trans)
            Else
                clsCommon.AddColumnsForChange(coll, "Code", obj.Code)
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MACHINE_INTEGRATION", OMInsertOrUpdate.Update, "Code='" + obj.Code + "'", trans)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function
    Public Shared Function DeleteData(ByVal strCode As String, Optional trans As SqlTransaction = Nothing) As Boolean
        Dim isSaved As Boolean = False
        Try
            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Code not found to Delete")
            End If
            Dim qry As String = "delete from TSPL_MACHINE_INTEGRATION where Code='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function
End Class

Public Class clsTempConclusion
    Public Normal As String
    Public Sample As String
End Class

Public Class clsTempAdulterant
    Public Water As String
    Public Urea As String
    Public Maltodex As String
    Public AmmSulph As String
    Public Sucrose As String
End Class

Public Class clsAutoMachinePick
    Public Form_ID As String
    Public Machine As String
    Public Com_Port As String

    Public Function saveData(ByVal obj As clsAutoMachinePick) As Boolean
        Dim qry As String = "Select 1 from TSPL_Auto_Machine_Pick where Form_ID='" + obj.Form_ID + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

        Dim coll As New Hashtable()
        clsCommon.AddColumnsForChange(coll, "Com_Port", obj.Com_Port)
        clsCommon.AddColumnsForChange(coll, "Machine", obj.Machine)
        clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
        clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy hh:mm tt"))
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Auto_Machine_Pick", OMInsertOrUpdate.Update, "Form_ID='" + obj.Form_ID + "'")
        Else
            clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Form_ID", obj.Form_ID)
            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Auto_Machine_Pick", OMInsertOrUpdate.Insert, "")
        End If
        Return True
    End Function

    Public Shared Function GetData(ByVal strCode As String) As clsAutoMachinePick
        Dim obj As clsAutoMachinePick = Nothing
        Dim qry As String = "select TSPL_Auto_Machine_Pick.* from TSPL_Auto_Machine_Pick   where 2=2 "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsAutoMachinePick()
            obj.Form_ID = clsCommon.myCstr(dt.Rows(0)("Form_ID"))
            obj.Machine = clsCommon.myCstr(dt.Rows(0)("Machine"))
            obj.Com_Port = clsCommon.myCstr(dt.Rows(0)("Com_Port"))
        End If
        Return obj
    End Function
End Class

