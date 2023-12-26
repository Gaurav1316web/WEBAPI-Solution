Imports System
Imports System.Data.SqlClient
Imports common

Public Class FrmAdditionalCharges
    Inherits FrmMainTranScreen

    Dim dt As New DataTable()
    Dim obj As New clsAdditionalCharge()
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private isNewEntry As Boolean = False
    Dim QRY As String
    Dim userCode, companyCode As String
    Private isInsideLoadData As Boolean = False
    Dim GstApplicable As Boolean = False
    Private ApplyNoGSTCreditIndependentlyOnVendorServiceCharge As Boolean = False

    Public Sub New(ByVal user As String, ByVal company As String)
        InitializeComponent()
        userCode = user
        companyCode = company
    End Sub
    Private Sub SetUserMgmtNew()
        '' Anubhooti 31-July-2014 BM00000003131
        'MyBase.SetUserMgmt(clsUserMgtCode.FrmAdditionalCharges)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
            'Me.Close()
            'Exit Function
        End If
        btnreset.Visible = MyBase.isModifyFlag
        btnSave.Visible = MyBase.isModifyFlag
        '--------richa Ticket no. BM00000003014 03/07/2014 to enable/disable import/export option acc. to user mgmt setting -----------
        If btnSave.Visible = True Then
            RadMenuItem2.Enabled = True
            RadMenuItem3.Enabled = True
        Else
            RadMenuItem2.Enabled = False
            RadMenuItem3.Enabled = False
        End If
        '--------------------------------------------------
        '         btnclose.Visible = MyBase.isDeleteFlag
        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub
    Public Sub SetLength()
        fndCode.MyMaxLength = 30
        txtdesc.MaxLength = 500
        txtGlAccDesc.MaxLength = 100
    End Sub

    Private Sub FrmAdditionalCharges_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnreset, "Press Alt+N Adding New Trasnaction")
        fndCode.MyReadOnly = True
        fndCode.MyMaxLength = 30
        ApplyNoGSTCreditIndependentlyOnVendorServiceCharge = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ApplyNoGSTCreditIndependentlyOnVendorServiceCharge, clsFixedParameterCode.ApplyNoGSTCreditIndependentlyOnVendorServiceCharge, Nothing)) = 1, True, False)
        AddNew()
        GstApplicable = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.GSTApplicable, clsFixedParameterCode.GSTApplicable, Nothing)) = "1", True, False))
        RdbGST.Enabled = GstApplicable
        'If objCommonVar.CurrentUserCode <> "ADMIN" Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If
    End Sub
    Sub BlankAllControls()
        fndCode.Value = Nothing
        txtdesc.Text = ""
        txtGLAccount.Value = Nothing
        txtGlAccDesc.Text = ""
        fndSACcode.Value = Nothing
        chkRCM.Checked = False
        chkInsurance.Checked = False
        chkServiceType.Checked = False
        lblSacCodeDesc.Text = ""
    End Sub
    Function AllowToSave() As Boolean
        If clsCommon.myLen(fndCode.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "Please Enter Code", Me.Text)
            fndCode.Focus()
            Return False
        End If

        If clsCommon.myLen(txtGLAccount.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow("Me,Please select Account", Me.Text)
            txtGLAccount.Focus()
            Return False
        End If

        If GstApplicable Then
            If chkServiceType.Checked = True Then
                If clsCommon.myLen(fndSACcode.Value) <= 0 Then '
                    common.clsCommon.MyMessageBoxShow(Me, "Please select SAC Code", Me.Text)
                    fndSACcode.Focus()
                    Return False
                End If
            End If
        End If
        Return True
    End Function
    Sub SaveData()
        If clsCommon.myLen(fndCode.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow(Me, " Account Code can not be left blank ", Me.Text)
            fndCode.Focus()
            Exit Sub
        End If
        Try
            If (AllowToSave()) Then
                If MyBase.isModifyonPasswordFlag Then
                    If clsPasswordCheckForMasters.CheckMasterPwd(clsUserMgtCode.FrmAdditionalCharges, clsCommon.myCstr(companyCode)) Then
                    Else
                        Return
                    End If
                End If

                Dim obj As New clsAdditionalCharge()
                obj.Code = (fndCode.Value).ToString
                obj.desc = clsCommon.myCstr(txtdesc.Text)
                obj.Account_Code = clsCommon.myCstr(txtGLAccount.Value)
                obj.Account_Description = clsCommon.myCstr(txtGlAccDesc.Text)
                If chkFreight.Checked = True Then
                    obj.freight = "Y"
                Else
                    obj.freight = "N"
                End If
                Dim qry As String = "select COUNT(*) from TSPL_Additional_Charges where FreightCharges not in ('" & obj.freight & "')"
                Dim Count As Integer = clsDBFuncationality.getSingleValue(qry)
                If Count = 1 AndAlso clsCommon.myCstr(obj.freight) = "Y" Then
                    common.clsCommon.MyMessageBoxShow("Freight Charges has already Added on another Additional Code")
                    Exit Sub
                End If

                obj.specification = txtspecification.Text.Replace("'", "`")

                If clsCommon.myLen(obj.specification) > 1000 Then
                    obj.specification = obj.specification.Substring(0, 1000)
                End If
                obj.Is_RoundOff = chkRoundoff.Checked
                obj.abtment = clsCommon.myCdbl(txtabtmnt.Value)
                obj.Reverse_Charge_Per = txtReverseChargePer.Value
                If GstApplicable Then
                    If chkServiceType.Checked = True Then
                        obj.ServiceType = "Y"
                        obj.RCM = chkRCM.Checked
                    Else
                        obj.ServiceType = "N"
                    End If

                    obj.NO_GST_Credit = chkNoGSTCredit.Checked
                    obj.SACCode = clsCommon.myCstr(fndSACcode.Value)
                End If
                obj.Is_Insurance = chkInsurance.Checked
                If (obj.SaveData(obj, isNewEntry)) Then
                    If btnSave.Text = "Save" Then
                        common.clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                        btnSave.Text = "Update"
                    Else
                        common.clsCommon.MyMessageBoxShow(Me, "Data Updated Successfully", Me.Text)
                    End If

                    LoadData(obj.Code, NavigatorType.Current)
                Else
                    common.clsCommon.MyMessageBoxShow("This '" & obj.Code & "' already exist ")
                End If
            End If


        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        SaveData()
    End Sub
    Sub LoadData(ByVal Code As String, ByVal navType As common.NavigatorType)
        Try
            btnSave.Enabled = True
            btnDelete.Enabled = True
            isInsideLoadData = True
            isNewEntry = False

            BlankAllControls()
            fndCode.MyReadOnly = True
            Dim obj As New clsAdditionalCharge()
            obj = clsAdditionalCharge.GetData(Code, navType)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Code) > 0) Then
                fndCode.Value = obj.Code
                txtdesc.Text = obj.desc
                txtGLAccount.Value = obj.Account_Code
                txtGlAccDesc.Text = obj.Account_Description
                If obj.freight = "Y" Then
                    chkFreight.Checked = True
                ElseIf obj.freight = "N" Then
                    chkFreight.Checked = False
                End If
                chkRCM.Checked = obj.RCM
                chkNoGSTCredit.Checked = obj.NO_GST_Credit
                If chkRCM.Checked = True Or ApplyNoGSTCreditIndependentlyOnVendorServiceCharge = True Then
                    chkNoGSTCredit.Enabled = True
                Else
                    chkNoGSTCredit.Enabled = False
                End If
                chkRoundoff.Checked = obj.Is_RoundOff
                chkInsurance.Checked = obj.Is_Insurance
                txtabtmnt.Value = obj.abtment
                txtReverseChargePer.Value = obj.Reverse_Charge_Per
                txtspecification.Text = obj.specification
                If obj.ServiceType = "Y" Then
                    chkServiceType.Checked = True
                ElseIf obj.ServiceType = "N" Then
                    chkServiceType.Checked = False
                End If
                fndSACcode.Value = obj.SACCode
                lblSacCodeDesc.Text = obj.SAC_Description

                btnSave.Text = "Update"
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isInsideLoadData = False
        End Try
    End Sub
    Sub DeleteData()
        If clsCommon.myLen(fndCode.Value) <= 0 Then
            Exit Sub
        End If
        Try
            If (myMessages.deleteConfirm()) Then
                If (clsAdditionalCharge.DeleteData(fndCode.Value)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully", Me.Text)
                    AddNew()
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        DeleteData()
    End Sub
    Sub CloseForm()
        Me.Close()
    End Sub
    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        CloseForm()
    End Sub
    Private Sub btnreset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnreset.Click
        AddNew()
    End Sub
    Sub AddNew()
        BlankAllControls()
        fndCode.Focus()
        fndCode.MyReadOnly = False
        isNewEntry = True
        btnSave.Text = "Save"
        btnSave.Enabled = True
        btnDelete.Enabled = True
        chkFreight.Checked = False
        txtabtmnt.Value = 0
        txtspecification.Text = ""
        txtReverseChargePer.Value = 0
        chkRCM.Checked = False
        chkRCM.Enabled = True
        chkInsurance.Checked = False
        chkNoGSTCredit.Checked = False
        If ApplyNoGSTCreditIndependentlyOnVendorServiceCharge Then
            chkNoGSTCredit.Enabled = True
        Else
            chkNoGSTCredit.Enabled = False
        End If
        chkRoundoff.Checked = False
    End Sub
    Private Sub fndCode__MYValidating_1(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndCode._MYValidating
        Dim qst As String = "select count(*) from tspl_Additional_Charges where Code='" + fndCode.Value + "'"
        Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qst))
        If count = 0 Then
            fndCode.MyReadOnly = False
        Else
            fndCode.MyReadOnly = True
        End If
        If fndCode.MyReadOnly Or isButtonClicked Then
            fndCode.Value = clsAdditionalCharge.getFinder("", fndCode.Value, isButtonClicked)
            LoadData(fndCode.Value, NavigatorType.Current)
        End If
    End Sub
    Private Sub fndCode__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles fndCode._MYNavigator
        Try
            LoadData(fndCode.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub



    Private Sub fndCode_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles fndCode.KeyPress
        If (e.KeyChar = Chr(39)) Then
            e.Handled = True
        End If
    End Sub
    ' For Import Data Excel Sheet to Database table
    Public Sub Import()

        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim currentdate As Date = Date.Today
        Dim StrVar() As String = {}
        If GstApplicable Then
            StrVar = {"Code", "Description", "Account_Code", "Account_Description", "Freight Charges", "Abatement", "Specification", "Reverse_Charge_Per", "Insurance(Y/N)", "Service_Type", "SAC_Code", "RCM", "NO GST Credit"}
        Else
            StrVar = {"Code", "Description", "Account_Code", "Account_Description", "Freight Charges", "Abatement", "Specification", "Reverse_Charge_Per", "Insurance(Y/N)"}
        End If
        Dim strvarlist As List(Of String) = New List(Of String)(StrVar)
        If transportSql.importExcel(gv, strvarlist.ToArray()) Then
            Dim trans As SqlTransaction = Nothing
            Try
                connectSql.OpenConnection()
                trans = clsDBFuncationality.GetTransactin()
                clsCommon.ProgressBarShow()
                Dim rowNumber As Integer = 0
                For Each grow As GridViewRowInfo In gv.Rows
                    rowNumber = rowNumber + 1
                    Dim ServiceType As String = ""
                    Dim SACCode As String = ""
                    Dim Code As String = clsCommon.myCstr(grow.Cells("Code").Value)
                    Dim Desc As String = clsCommon.myCstr(grow.Cells("Description").Value)
                    Dim straccount As String = clsCommon.myCstr(grow.Cells("Account_Code").Value)
                    Dim straccdesc As String = clsCommon.myCstr(grow.Cells("Account_Description").Value)
                    Dim freight As String = clsCommon.myCstr(grow.Cells("Freight Charges").Value)
                    Dim abtment As Decimal = clsCommon.myCdbl(grow.Cells("abatement").Value)
                    Dim Reverse_Charge_Per As String = clsCommon.myCstr(clsCommon.myCdbl(grow.Cells("Reverse_Charge_Per").Value))
                    Dim specification As String = clsCommon.myCstr(grow.Cells("specification").Value)
                    Dim strRCM As String = "0"
                    Dim strNOGSTCredit As String = "0"

                    Dim Insurance As String = clsCommon.myCstr(grow.Cells("Insurance(Y/N)").Value)
                    If clsCommon.CompairString(Insurance, "Y") = CompairStringResult.Equal Then
                        Insurance = "1"
                    Else
                        Insurance = "0"
                    End If

                    If GstApplicable Then
                        ServiceType = clsCommon.myCstr(grow.Cells("Service_Type").Value)
                        SACCode = clsCommon.myCstr(grow.Cells("SAC_Code").Value)
                        strRCM = clsCommon.myCstr(grow.Cells("RCM").Value)
                        strNOGSTCredit = clsCommon.myCstr(grow.Cells("NO GST Credit").Value)
                        If clsCommon.CompairString(ServiceType, "Y") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(ServiceType, "N") <> CompairStringResult.Equal Then
                            Throw New Exception("Service Type should be Y or N at Row No " + clsCommon.myCstr(rowNumber))
                        End If
                        If clsCommon.CompairString(strRCM, "Y") = CompairStringResult.Equal Then
                            If ServiceType = "Y" Then
                                strRCM = "1"
                            Else
                                Throw New Exception("Service Type Should be 'Y' for RCM. When RCM value equal to 'Y' at Row No " + clsCommon.myCstr(rowNumber))
                            End If
                        ElseIf clsCommon.CompairString(strRCM, "N") = CompairStringResult.Equal Then
                            strRCM = "0"
                        Else
                            Throw New Exception("RCM should be Y/N at Row No " + clsCommon.myCstr(rowNumber))
                        End If



                        '*****************************************
                        If clsCommon.CompairString(strNOGSTCredit, "Y") = CompairStringResult.Equal Then
                            If strRCM = "1" Then
                                strNOGSTCredit = "1"
                            Else
                                Throw New Exception("RCM Should be 'Y' for [NO GST Credit]. When [NO GST Credit] value equal to 'Y' at Row No " + clsCommon.myCstr(rowNumber))
                            End If

                        ElseIf clsCommon.CompairString(strNOGSTCredit, "N") = CompairStringResult.Equal Then
                            strNOGSTCredit = "0"
                        Else
                            Throw New Exception("[NO GST Credit] should be Y/N at Row No " + clsCommon.myCstr(rowNumber))
                        End If
                        '******************************************

                    End If

                    If clsCommon.myLen(specification) > 1000 Then
                        specification = specification.Substring(0, 1000)
                    End If

                    Dim dt As DataTable = clsDBFuncationality.GetDataTable("select Description from tspl_gl_accounts where Account_Code='" + straccount + "'", trans)
                    If dt.Rows.Count <= 0 Then
                        Throw New Exception("Account does not exist at Row No " + clsCommon.myCstr(rowNumber))
                    Else
                        For Each row As DataRow In dt.Rows
                            If (straccdesc.ToUpper() <> row("Description").ToString().ToUpper()) Then
                                Throw New Exception("Account description not exist for this account at Row No" + clsCommon.myCstr(rowNumber))
                            End If
                        Next
                    End If


                    If (String.IsNullOrEmpty(Code)) Or clsCommon.myLen(Code) > 30 Then
                        Throw New Exception("Code can not be blank or greather than 30 length at Row No " + clsCommon.myCstr(rowNumber))
                    End If

                    If (String.IsNullOrEmpty(Desc)) Or clsCommon.myLen(Desc) > 500 Then
                        Throw New Exception(" Description can not be blank or greather than 500 length at Row No " + clsCommon.myCstr(rowNumber))
                    End If
                    If (String.IsNullOrEmpty(straccount)) Or clsCommon.myLen(straccount) > 50 Then
                        Throw New Exception("Account can not be blank or greather than 50 length at Row No " + clsCommon.myCstr(rowNumber))
                    End If

                    If (String.IsNullOrEmpty(straccdesc)) Or clsCommon.myLen(straccdesc) > 100 Then
                        Throw New Exception("Account Description can not be blank or greather than 100 length at Row No " + clsCommon.myCstr(rowNumber))
                    End If
                    If clsCommon.myLen(freight) > 1 Then
                        Throw New Exception("Freight Charges should be Y or N at Row No " + clsCommon.myCstr(rowNumber))
                    End If
                    If GstApplicable Then
                        If clsCommon.CompairString(ServiceType, "Y") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(ServiceType, "N") <> CompairStringResult.Equal Then
                            Throw New Exception("Service_Type should be Y or N at Row No " + clsCommon.myCstr(rowNumber))
                        End If
                        If ServiceType = "Y" Then
                            If clsCommon.myLen(SACCode) <= 0 Then
                                Throw New Exception("SAC Code is mandatory if Service_Type is Y at Row No " + clsCommon.myCstr(rowNumber))
                            End If

                        End If
                        If clsCommon.myLen(SACCode) > 0 Then
                            Dim dt1 As DataTable = clsDBFuncationality.GetDataTable("select Code from TSPL_SAC_MASTER where Code='" + SACCode + "'", trans)
                            If dt1.Rows.Count <= 0 Then
                                Throw New Exception("SAC Code does not exist at Row No " + clsCommon.myCstr(rowNumber))
                            End If
                        End If
                    End If
                    Dim sql1 As String = "select count(*) from tspl_Additional_Charges where Code='" + Code + "'"
                    Dim i As Integer = CInt(connectSql.RunScalar(trans, sql1))
                    If (i = 0) Then
                        QRY = "INSERT Into tspl_Additional_Charges values('" + Code + "','" + Desc + "','" + userCode + "','" + connectSql.serverDate(trans) + "','" + userCode + "','" + connectSql.serverDate(trans) + "','" + companyCode + "','" + straccount + "','" + straccdesc + "','" & freight & "','" & abtment & "','" & specification & "','" & Reverse_Charge_Per & "','" & ServiceType & "','" & SACCode & "','" + strRCM + "','" + strNOGSTCredit + "','" + Insurance + "')"
                        connectSql.RunSqlTransaction(trans, QRY)
                    Else
                        QRY = "UPDATE tspl_Additional_Charges set Code='" + Code + "', description='" + Desc + "',Account_Code='" + straccount + "',Account_Description='" + straccdesc + "' ,Modified_By='" + userCode + "', Modified_Date='" + connectSql.serverDate(trans) + "', Comp_Code='" + companyCode + "',FreightCharges='" & freight & "',abatement='" & abtment & "',specification='" + specification + "',Reverse_Charge_Per='" + Reverse_Charge_Per + "' ,Service_Type='" + ServiceType + "',SAC_Code='" + SACCode + "',RCM='" + strRCM + "',NO_GST_Credit = '" + strNOGSTCredit + "',Is_Insurance='" + Insurance + "'  WHERE Code='" + Code + "'"
                        connectSql.RunSqlTransaction(trans, QRY)
                    End If
                    clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, clsCommon.myCstr(Code), "tspl_Additional_Charges", "CODE", trans)
                Next
                trans.Commit()
                clsCommon.ProgressBarHide()
                common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                trans.Rollback()
                clsCommon.ProgressBarHide()
                myMessages.myExceptions(ex)

            End Try

        End If


        Me.Controls.Remove(gv)
    End Sub
    ' For expport data database to table
    Public Sub Export()
        Dim str As String
        str = "select Code As [Code],description as [Description],Account_Code,Account_Description,FreightCharges as [Freight Charges],Abatement,Specification,Reverse_Charge_Per , (case when isnull( Is_Insurance,0)=1 then 'Y' else 'N' end) as [Insurance(Y/N)] "
        If GstApplicable Then
            str += ",Service_Type,SAC_Code,case when RCM=1 then 'Y' else 'N' end as RCM, case when NO_GST_Credit=1 then 'Y' else 'N' end as [NO GST Credit] "
        End If
        str += " from tspl_Additional_Charges"

        ListImpExpColumnsMandatory = New List(Of String)({"Code", "Description", "Account_Code", "Account_Description"})
        ListImpExpColumnsSuperMandatory = New List(Of String)({"Code"})
        transportSql.ExporttoExcel(str, "", "", Me, ListImpExpColumnsMandatory, ListImpExpColumnsSuperMandatory, MyBase.Form_ID)
    End Sub
    Private Sub FrmAdditionalCharges_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso MyBase.isModifyFlag Then
            AddNew()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnSave.Enabled Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btnDelete.Enabled Then
            DeleteData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            CloseForm()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            AddNew()
        End If
    End Sub

    Private Sub RadMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem2.Click
        Import()
    End Sub
    Private Sub RadMenuItem4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem4.Click
        CloseForm()
    End Sub

    Private Sub menuAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuAll.Click
        Export()
    End Sub

    Private Sub MenuCriteria_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuCriteria.Click
        Dim frmsetCriteria As New FrmExportSetCriteria()
        frmsetCriteria.Show()
    End Sub

    Private Sub txtGLAccount__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtGLAccount._MYValidating
        Dim qry As String = "select Account_Code as [Account],Description from tspl_gl_accounts"
        'txtGLAccount.Value = clsCommon.ShowSelectForm("DisountMasterAccount", qry, "Account", "", txtGLAccount.Value, "Account", isButtonClicked)
        txtGLAccount.Value = clsGLAccount.getFinder("", txtGLAccount.Value, isButtonClicked)
        ''lblVendorName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Vendor_Name from TSPL_VENDOR_MASTER where Vendor_Code='" + txtVendorNo.Value + "'"))
        qry = "select Description from tspl_gl_accounts where Account_Code ='" + txtGLAccount.Value + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            txtGlAccDesc.Text = clsCommon.myCstr(dt.Rows(0)("Description"))
        Else
            txtGlAccDesc.Text = ""
        End If
    End Sub


    '---------------------Added By -----Pankaj Kumar-------------on--29/03/2012-------------
    'This will check the authorization of user to access the screen.If authorize then it will allow user to access the screen.
    'Private Function funSetUserAccess() As Boolean
    '    Try
    '        Dim strRights As String
    '        Dim strTemp() As String
    '        Dim strProgCode = "ADSNL-CHRG"
    '        strRights = enuUserRights.enuRead & "," & enuUserRights.enuModify & "," & enuUserRights.enuDelete
    '        strRights = modUserMgt.funGetPermissions(strRights, strProgCode)
    '        strTemp = Split(strRights, ",")
    '        If strTemp(0) = "0" Then
    '            MsgBox("Permission Denied", MsgBoxStyle.Critical, Me.Text)
    '            funSetUserAccess = False
    '            blnRead = False
    '            Me.Close()
    '            Exit Function
    '        Else
    '            blnRead = True
    '        End If
    '        If strTemp(1) = "0" Then 'Grant modify access
    '            btnSave.Enabled = False
    '        End If
    '        If strTemp(2) = "0" Then 'Grant modify access
    '            btnDelete.Enabled = False
    '        End If

    '        funSetUserAccess = True
    '    Catch er As Exception
    '        myMessages.myExceptions(er)
    '    End Try
    '    '-----------------------------------Code Ends Here-------------------------------
    'End Function

    Private Sub fndSACcode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndSACcode._MYValidating
        Dim qry As String = "select Code,Description from TSPL_SAC_MASTER"
        fndSACcode.Value = ClsSACMaster.getFinder("", fndSACcode.Value, isButtonClicked)

        qry = "select Code,Description from TSPL_SAC_MASTER where Code ='" + fndSACcode.Value + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            lblSacCodeDesc.Text = clsCommon.myCstr(dt.Rows(0)("Description"))
        Else
            lblSacCodeDesc.Text = ""
        End If
    End Sub

    Private Sub chkServiceType_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkServiceType.ToggleStateChanged
        If chkServiceType.Checked = True Then
            fndSACcode.Enabled = True
            chkRCM.Enabled = True
        Else
            fndSACcode.Enabled = False
            chkRCM.Enabled = False
            chkRCM.Checked = False
        End If

    End Sub

    Private Sub chkRCM_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkRCM.ToggleStateChanged
        If (chkRCM.Checked = True AndAlso chkRCM.Enabled = True) OrElse ApplyNoGSTCreditIndependentlyOnVendorServiceCharge = True Then
            chkNoGSTCredit.Enabled = True
        Else
            chkNoGSTCredit.Checked = False
            If ApplyNoGSTCreditIndependentlyOnVendorServiceCharge = True Then
                chkNoGSTCredit.Enabled = True
            Else
                chkNoGSTCredit.Enabled = False
            End If

        End If
    End Sub

    
    Private Sub btnHistory_Click(sender As Object, e As EventArgs) Handles btnHistory.Click
        Try
            If clsCommon.myLen(fndCode.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Select Additional Charges Code", Me.Text)
                Exit Sub
            End If
            clsERPFuncationalityOLD.ShowHistoryData(fndCode.Value, "Code", "tspl_Additional_Charges")
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
End Class
