Imports System.Data.SqlClient
Imports common
Imports System.Net.NetworkInformation

Public Class FrmPortSettings
    Inherits FrmMainTranScreen
    
    Dim sPort As clsSerialPort = Nothing

    Public isLoad As Boolean = True
    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
        GC.Collect()
    End Sub

    Sub reset()
        sPort = New clsSerialPort()
        sPort.SetBaudRate(ddlBaudRate)
        sPort.SetParityValues(ddlParity)
        sPort.SetStopBitValues(ddlStopBits)
        sPort.SetDataBits(ddlDataBits)
        'If isLoad Then
        clsPortSetting.GetDataFormType(CboDataForm)
        'isLoad = False
        'End If
        btnSave.Enabled = True
        btnSave.Text = "Save"
        btnDelete.Enabled = False
        'chkEkoProMachine.IsChecked = True
        'CboMachine.Text = "Everest New"
        If setPreviousAssignedValues(IIf(chkEkoProMachine.IsChecked, 0, 1)) Then
            btnSave.Text = "Update"
            btnDelete.Enabled = True
        End If
    End Sub
    Function setPreviousAssignedValues(ByVal mType As Integer) As Boolean
        Dim whrCls As String = String.Empty
        'If clsCommon.CompairString(CboMachine.Text, "kanha") = CompairStringResult.Equal Then
        '    whrCls = "k"
        'ElseIf clsCommon.CompairString(CboMachine.Text, "Prompt") = CompairStringResult.Equal Then
        '    whrCls = "P"
        'ElseIf clsCommon.CompairString(CboMachine.Text, "Delta") = CompairStringResult.Equal Then
        '    whrCls = "D"
        'ElseIf clsCommon.CompairString(CboMachine.Text, "Panasonic") = CompairStringResult.Equal Then
        '    whrCls = "B"
        'ElseIf clsCommon.CompairString(CboMachine.Text, "Supertech") = CompairStringResult.Equal Then
        '    whrCls = "S"
        'Else
        '    whrCls = "E"
        'End If
        Dim obj As clsPortSetting = clsPortSetting.getData(mType, CboMachine.Text)
        If obj IsNot Nothing Then
            'CboMachine.SelectedValue = obj.machinename
            CboDataForm.SelectedValue = obj.Data_Form
            ddlBaudRate.Text = obj.baud_rate
            ddlParity.Text = obj.parity
            ddlDataBits.Text = obj.data_bits
            ddlStopBits.Text = obj.stop_bits
            btnSave.Text = "Update"
            btnDelete.Enabled = True
            Return True
        Else
            obj = New clsPortSetting()
            Dim networkcard() As NetworkInterface = NetworkInterface.GetAllNetworkInterfaces()
            obj.ip_address = getMachineIP() 'System.Net.Dns.GetHostByName(System.Net.Dns.GetHostName()).AddressList(0).ToString()
            obj.mac_address = networkcard(0).GetPhysicalAddress.ToString()
            obj.machine_name = My.User.Name
            obj.Machine_Type = mType
            obj.machinename = whrCls
            obj.Data_Form = "T"
            If clsCommon.CompairString(obj.machinename, "K") = CompairStringResult.Equal And mType = 0 Then
                obj.Data_Form = "H"
                obj.baud_rate = "1200"
                obj.data_bits = "8"
                obj.stop_bits = "One"
                obj.parity = "Even"
            ElseIf clsCommon.CompairString(obj.machinename, "E") = CompairStringResult.Equal And mType = 0 Then
                obj.baud_rate = "2400"
                obj.data_bits = "8"
                obj.stop_bits = "One"
                obj.parity = "None"
            ElseIf clsCommon.CompairString(obj.machinename, "P") = CompairStringResult.Equal And mType = 1 Then
                obj.baud_rate = "2400"
                obj.data_bits = "8"
                obj.stop_bits = "One"
                obj.parity = "None"
            ElseIf clsCommon.CompairString(obj.machinename, "D") = CompairStringResult.Equal And mType = 1 Then
                obj.baud_rate = "2400"
                obj.data_bits = "8"
                obj.stop_bits = "One"
                obj.parity = "None"
            ElseIf clsCommon.CompairString(obj.machinename, "B") = CompairStringResult.Equal And mType = 1 Then
                obj.baud_rate = "2400"
                obj.data_bits = "8"
                obj.stop_bits = "One"
                obj.parity = "None"
            ElseIf clsCommon.CompairString(obj.machinename, "S") = CompairStringResult.Equal And mType = 1 Then
                obj.baud_rate = "9600"
                obj.data_bits = "8"
                obj.stop_bits = "One"
                obj.parity = "None"
            ElseIf clsCommon.CompairString(obj.machinename, "Z") = CompairStringResult.Equal And mType = 1 Then
                obj.baud_rate = "9600"
                obj.data_bits = "8"
                obj.stop_bits = "One"
                obj.parity = "None"
            Else
                obj.baud_rate = "2400"
                obj.data_bits = "8"
                obj.stop_bits = "1"
                obj.parity = "None"
            End If
            'CboMachine.SelectedValue = obj.machinename
            CboDataForm.SelectedValue = obj.Data_Form
            ddlBaudRate.Text = obj.baud_rate
            ddlParity.Text = obj.parity
            ddlDataBits.Text = obj.data_bits
            ddlStopBits.Text = obj.stop_bits
            btnSave.Text = "Save"
            btnDelete.Enabled = False
            Return False
        End If
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

    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.FrmPortSettings)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub FrmPortSettings_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        reset()
        If chkWeighingMachine.IsChecked Then
            clsPortSetting.GetWeighingMachineNames(CboMachine)
        Else
            clsPortSetting.GetMachineType(CboMachine)
        End If

        setPreviousAssignedValues(IIf(chkEkoProMachine.IsChecked, 0, 1))
        SetUserMgmtNew()
    End Sub

    Private Sub chkEkoProMachine_ToggleStateChanged(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkEkoProMachine.ToggleStateChanged
        If chkWeighingMachine.IsChecked Then
            clsPortSetting.GetWeighingMachineNames(CboMachine)
        Else
            clsPortSetting.GetMachineType(CboMachine)
        End If

        setPreviousAssignedValues(IIf(chkEkoProMachine.IsChecked, 0, 1))
    End Sub
    Function allowToSave() As Boolean
        Try
            If clsCommon.myLen(ddlBaudRate.Text) <= 0 Then
                Throw New Exception("Please select Baud Rate")
            End If
            If clsCommon.myLen(ddlDataBits.Text) <= 0 Then
                Throw New Exception("Please select Data Bits")
            End If
            If clsCommon.myLen(ddlStopBits.Text) <= 0 Then
                Throw New Exception("Please select Stop Bits")
            End If
            If clsCommon.myLen(ddlParity.Text) <= 0 Then
                Throw New Exception("Please select Parity")
            End If
            If clsCommon.myLen(CboDataForm.Text) <= 0 Then
                Throw New Exception("Please select Data Format")
            End If
            If clsCommon.myLen(CboMachine.Text) <= 0 Then
                Throw New Exception("Please select Machine")
            End If
            Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            Return False
        End Try
    End Function
    Sub save()

        If MyBase.isModifyonPasswordFlag Then
            If clsPasswordCheckForMasters.CheckMasterPwd(clsUserMgtCode.FrmPortSettings, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
            Else
                Return
            End If
        End If

        Dim trans As SqlTransaction = Nothing
        Dim networkcard() As NetworkInterface = NetworkInterface.GetAllNetworkInterfaces()
        Dim obj As clsPortSetting = Nothing
        Try
            obj = New clsPortSetting()
            If clsCommon.CompairString(btnSave.Text, "Save") = CompairStringResult.Equal Then
                obj.isNewEntry = True
            Else
                obj.isNewEntry = False
            End If
            trans = clsDBFuncationality.GetTransactin()
            Dim dt As Date = clsCommon.GETSERVERDATE(trans, "dd/MMM/yyyy")
            obj.machine_name = My.User.Name
            obj.ip_address = getMachineIP(trans) 'System.Net.Dns.GetHostByName(System.Net.Dns.GetHostName()).AddressList(0).ToString()
            obj.mac_address = networkcard(0).GetPhysicalAddress.ToString()
            obj.baud_rate = clsCommon.myCstr(ddlBaudRate.Text)
            obj.data_bits = clsCommon.myCstr(ddlDataBits.Text)
            obj.stop_bits = clsCommon.myCstr(ddlStopBits.Text)
            obj.parity = clsCommon.myCstr(ddlParity.Text)
            obj.modified_by = objCommonVar.CurrentUserCode
            obj.modified_date = clsCommon.GetPrintDate(dt, "dd/MM/yyyy")
            obj.comp_code = objCommonVar.CurrentCompanyCode
            If chkEkoProMachine.IsChecked Then
                obj.Machine_Type = 0
            Else
                obj.Machine_Type = 1
            End If
            '=================Rohit to Save Combo Machine Name and Setting================
            'If clsCommon.CompairString(CboMachine.Text, "kanha") = CompairStringResult.Equal Then
            '    obj.machinename = "k"
            'ElseIf clsCommon.CompairString(CboMachine.Text, "Prompt") = CompairStringResult.Equal Then
            '    obj.machinename = "P"
            'ElseIf clsCommon.CompairString(CboMachine.Text, "Delta") = CompairStringResult.Equal Then
            '    obj.machinename = "D"
            'ElseIf clsCommon.CompairString(CboMachine.Text, "Panasonic") = CompairStringResult.Equal Then
            '    obj.machinename = "B"
            'ElseIf clsCommon.CompairString(CboMachine.Text, "Supertech") = CompairStringResult.Equal Then
            '    obj.machinename = "S"
            'Else
            '    obj.machinename = "E"
            'End If
            'obj.machinename = IIf(LCase(Me.CboMachine.Text) = "kanha", "K", "E")
            obj.machinename = clsPortSetting.getMachineMakePrefix(CboMachine.Text, obj.Machine_Type)
            obj.Data_Form = clsPortSetting.getMachineDataFormat(CboMachine.Text)
            '=============================================================================
            If obj.isNewEntry Then
                obj.created_by = objCommonVar.CurrentUserCode
                obj.created_date = clsCommon.GetPrintDate(dt, "dd/MM/yyyy")
            End If
            If clsPortSetting.SaveData(obj, trans) Then
                trans.Commit()
                If clsCommon.CompairString(btnSave.Text, "Save") = CompairStringResult.Equal Then
                    clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                Else
                    clsCommon.MyMessageBoxShow(Me, "Data Updated Successfully", Me.Text)
                End If
                reset()
                Exit Sub
            End If
            Throw New Exception("Data Not Saved ")
        Catch ex As Exception
            btnSave.Text = "Save"
            btnDelete.Enabled = False
            trans.Rollback()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If allowToSave() Then save()
    End Sub

    Private Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Try
            If clsCommon.MyMessageBoxShow("Sure to Delete ? ", "Delete", MessageBoxButtons.YesNo, RadMessageIcon.Question, MessageBoxDefaultButton.Button1) = System.Windows.Forms.DialogResult.Yes Then
                Dim whrCls As String = clsPortSetting.getMachineMakePrefix(CboMachine.Text, IIf(chkEkoProMachine.IsChecked, 0, 1))
                'If clsCommon.CompairString(CboMachine.Text, "kanha") = CompairStringResult.Equal Then
                '    whrCls = "k"
                'ElseIf clsCommon.CompairString(CboMachine.Text, "Prompt") = CompairStringResult.Equal Then
                '    whrCls = "P"
                'ElseIf clsCommon.CompairString(CboMachine.Text, "Delta") = CompairStringResult.Equal Then
                '    whrCls = "D"
                'ElseIf clsCommon.CompairString(CboMachine.Text, "Panasonic") = CompairStringResult.Equal Then
                '    whrCls = "B"
                'ElseIf clsCommon.CompairString(CboMachine.Text, "Supertech") = CompairStringResult.Equal Then
                '    whrCls = "S"
                'Else
                '    whrCls = "E"
                'End If
                If clsPortSetting.deleteData(IIf(chkEkoProMachine.IsChecked, 0, 1), whrCls) Then
                    clsCommon.MyMessageBoxShow(Me, "Deleted Successfully", Me.Text)
                    reset()
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub CboMachine_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CboMachine.SelectedValueChanged
        setPreviousAssignedValues(IIf(chkEkoProMachine.IsChecked, 0, 1))
        'reset()
    End Sub

    Private Sub chkWeighingMachine_ToggleStateChanged(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkWeighingMachine.ToggleStateChanged
        If chkWeighingMachine.IsChecked Then
            clsPortSetting.GetWeighingMachineNames(CboMachine)
        Else
            clsPortSetting.GetMachineType(CboMachine)
        End If

        setPreviousAssignedValues(IIf(chkEkoProMachine.IsChecked, 0, 1))
    End Sub
End Class
