Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Data.SqlClient
Imports Telerik.WinControls.UI
Imports XpertERPEngine
Imports Telerik.WinControls
Imports Telerik.WinControls.Data
Imports System.Text.RegularExpressions
Imports Excel = Microsoft.Office.Interop.Excel
Imports common
'created by --> Vipin
'createddate --> 22/05/2011
'modifiedby --> Vipin
'Modified date -->03/06/2011
'Tables Used --> tspl_Transport_master

Public Class frmTransportMaster
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim formtype As String = Nothing
    Dim userCode, companyCode As String
    Dim strUserMgtValPass As String = ""
    Public Sub New(ByVal user As String, ByVal company As String, ByVal formid As String)
        InitializeComponent()
        userCode = user
        companyCode = company
        formtype = formid
    End Sub
#Region "Variables"
    Dim sql As String
    Dim ds As DataSet
    Dim dr As SqlDataReader
    Dim tableName As String = "TSPL_TAX_MASTER"
    Dim tableCode As String = "Tax_Code"
    Dim codePrefix As String = "TAX"
#End Region
    'Main Form Load
    Public Sub SetLength()
        fndtrans.MyMaxLength = 12
        txtname.MaxLength = 50
        txtadd1.MaxLength = 50
        txtadd2.MaxLength = 50
        txtcity.MaxLength = 12
        txtstate.MaxLength = 50
        txtpan.MaxLength = 30
        txtphone.MaxLength = 20
        txtemail.MaxLength = 50
        txtpin.MaxLength = 6
    End Sub
    Private Sub TransportMaster_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetLength()
        'globalFunc.mandatoryText(fndtrans.Value)
        SetUserMgmtNew()
        text_changed()
        fnd_leave()
        'ToolTiptrans.SetToolTip(btnnew, "Reset")
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update ")
        'ButtonToolTip.SetToolTip(btnPost, "Press Alt+P Post Trasnaction")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D Delete ")
        ButtonToolTip.SetToolTip(btnclear, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnnew, "Press Alt+N Adding New ")
        'ButtonToolTip.SetToolTip(btnPrint, "Press Alt+R for Print Preview")
        'fndtrans.Value.MaxLength = 12
        '  AddHandler fndtrans.ValueChanged, AddressOf text_changed
        ' AddHandler fndtrans.Value.Leave, AddressOf fnd_leave
        'AddHandler fndtrans.Value.KeyPress, AddressOf key_press
        '   fndtrans.Value.CharacterCasing = CharacterCasing.Upper
        btndelete.Enabled = False
        btnsave.Enabled = True
        'If userCode <> "ADMIN" Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If
        If formtype = clsUserMgtCode.transportMaster Then
            btndelete.Visible = False
            btnsave.Visible = False
        End If
    End Sub
    Private Sub SetUserMgmtNew()
        If clsCommon.CompairString(formtype, clsUserMgtCode.transportMaster) = CompairStringResult.Equal Then
            'MyBase.SetUserMgmt(clsUserMgtCode.transportMaster)
            strUserMgtValPass = clsUserMgtCode.transportMaster
        ElseIf clsCommon.CompairString(formtype, clsUserMgtCode.transportMasterVendor) = CompairStringResult.Equal Then
            'MyBase.SetUserMgmt(clsUserMgtCode.transportMasterVendor)
            strUserMgtValPass = clsUserMgtCode.transportMasterVendor
        End If

        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow("Permission Denied")
            Me.Close()
            Exit Sub
        End If
        btnsave.Visible = MyBase.isModifyFlag
        '--------richa Ticket no. BM00000003014 03/07/2014 to enable/disable import/export option acc. to user mgmt setting -----------
        If btnsave.Visible = True Then
            transimport.Enabled = True
            transexport.Enabled = True
        Else
            transimport.Enabled = False
            transexport.Enabled = False
        End If
        '--------------------------------------------------
        'btnPost.Visible = MyBase.isPostFlag
        btndelete.Visible = MyBase.isDeleteFlag
    End Sub
    'Keypress validation on finder and converting lower case to upper case
    Public Sub key_press(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)

        If (e.KeyChar = Chr(39)) Then
            e.Handled = True
        End If
    End Sub
    'It will fill all controls in screen if find any existing data in table 
    Sub text_changed()
        Dim str As String = "select Transport_Id from TSPL_Transport_MASTER where Transport_Id='" + fndtrans.Value + "'"

        Dim trs As String = fndtrans.Tag
        Dim strvalue As String = clsDBFuncationality.getSingleValue(str)
        
        If strvalue <> "" Then
            funfill()

        Else
            Dim str1 As String = "select Vendor_Code ,Vendor_Name  from TSPL_VENDOR_MASTER where Vendor_Code='" + fndtrans.Value + "' and transporter='Y' "
            '  Dim dr1 As SqlDataReader
            Dim chk As String = ""
            Dim des As String = ""

            Dim dt As DataTable
            dt = clsDBFuncationality.GetDataTable(str1)
            If dt.Rows.Count > 0 Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    chk = dt.Rows(i)("Vendor_Code")
                    des = dt.Rows(i)("Vendor_Name")
                Next

            End If
            

            If chk <> "" Then
                txtname.Text = des
                txtcity.Text = ""
                txtstate.Text = ""
                txtpin.Text = ""
                txtpan.Text = ""
                txtphone.Text = ""
                txtemail.Text = ""
                txtadd1.Text = ""
                txtadd2.Text = ""
                btnsave.Text = "Save"
                btndelete.Enabled = False

            Else
                txtname.Text = ""
                txtcity.Text = ""
                txtstate.Text = ""
                txtpin.Text = ""
                txtpan.Text = ""
                txtphone.Text = ""
                txtemail.Text = ""
                txtadd1.Text = ""
                txtadd2.Text = ""
                btnsave.Text = "Save"
                btndelete.Enabled = False
            End If
        End If

    End Sub
    Public Sub text_changed(ByVal sender As System.Object, ByVal e As System.EventArgs)
       
    End Sub
    'it will check the data existance in table on leave of fndtrans
    Sub fnd_leave()
        Try
            Dim strquery As String = "select Vendor_Code  from TSPL_VENDOR_MASTER where  Vendor_Code='" + fndtrans.Value + "' and transporter='Y' "

            Dim strvalue As String = clsDBFuncationality.getSingleValue(strquery)
           
            If strvalue <> "" Or fndtrans.Value = "" Then
            Else : strvalue = ""
                common.clsCommon.MyMessageBoxShow("This Transporter does not exist in Master Table", Me.Text)
                fndtrans.Value = ""
                txtname.Text = ""
            End If
        Catch ex As Exception

            myMessages.myExceptions(ex)
        End Try
    End Sub
 
    'For validation of email format 
    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click

        SaveData()
    End Sub
    Sub SaveData()
        Try

            Dim check As Match = Regex.Match(txtemail.Text, "\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*")
            If (IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowAutoGenerateDocNoInMaster, clsFixedParameterCode.AllowAutoGenerateDocNoInMaster, Nothing)) = "1", True, False) = False) AndAlso fndtrans.Value = "" Then
                myMessages.blankValue("Transporter Code")
                fndtrans.Focus()
            ElseIf txtname.Text = "" Then
                myMessages.blankValue("Transporter Name")
                txtname.Focus()
            ElseIf check.Success = False And txtemail.Text <> "" Then
                common.clsCommon.MyMessageBoxShow("Please insert the proper format of e-mail address", Me.Text)
                txtemail.Text = ""
                txtemail.Focus()
            Else
                If clsCommon.myLen(strUserMgtValPass) > 0 Then
                    If MyBase.isModifyonPasswordFlag Then
                        If clsPasswordCheckForMasters.CheckMasterPwd(strUserMgtValPass, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
                        Else
                            Return
                        End If
                    End If
                End If

                If btnsave.Text = "Save" Then
                    funinsert()
                ElseIf btnsave.Text = "Update" Then
                    funupdate()
                End If
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub

    'Funtion for insertion of data
    Public Sub funinsert()
        Try
            If (IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowAutoGenerateDocNoInMaster, clsFixedParameterCode.AllowAutoGenerateDocNoInMaster, Nothing)) = "1", True, False)) Then
                Dim ChkNewEntry As String = clsDBFuncationality.getSingleValue("Select count(*) from TSPL_TRANSPORT_MASTER where Transport_Id='" & fndtrans.Value & "'")
                If ChkNewEntry = 0 Then
                    fndtrans.Value = clsERPFuncationality.GetNextCode(Nothing, clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy"), clsDocType.TransportMaster, "", "")
                    If clsCommon.myLen(fndtrans.Value) <= 0 Then
                        Throw New Exception("Error in Code Generation")
                    End If
                End If
            End If
            connectSql.RunSp("sp_transportmaster_insert", New SqlParameter("@transid", fndtrans.Value), New SqlParameter("@transname", txtname.Text.ToString()), New SqlParameter("@city", txtcity.Text.ToString()), New SqlParameter("@state", txtstate.Text.ToString()), New SqlParameter("@pincode", txtpin.Text.ToString()), New SqlParameter("@panno", txtpan.Text.ToString()), New SqlParameter("@phone", txtphone.Text.ToString()), New SqlParameter("@add1", txtadd1.Text.ToString()), New SqlParameter("@add2", txtadd2.Text.ToString()), New SqlParameter("@email", txtemail.Text.ToString()), New SqlParameter("@createdby", userCode), New SqlParameter("@createddate", connectSql.serverDate()), New SqlParameter("@modifiedby", userCode), New SqlParameter("@modifieddate", connectSql.serverDate()), New SqlParameter("@compcode", companyCode))
            myMessages.insert()
            btnsave.Text = "Update"
            btndelete.Enabled = True
            'If userCode <> "ADMIN" Then
            '    If funSetUserAccess() = False Then Exit Sub
            'End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub
    'It will fill all controls in screen if find any existing data in table 
    Public Sub funfill()
        Try
            Dim str As String = "select Transporter_Name,city,state,pincode,panno,Phone,Add1,Add2,Email from TSPL_TRANSPORT_MASTER where Transport_Id='" + fndtrans.Value + "'"
            Dim dt As DataTable
            dt = clsDBFuncationality.GetDataTable(str)
            If dt.Rows.Count > 0 Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    txtname.Text = dt.Rows(i)("Transporter_Name")
                    txtcity.Text = dt.Rows(i)("city")
                    txtstate.Text = dt.Rows(i)("state")
                    txtpin.Text = dt.Rows(i)("pincode")
                    txtpan.Text = dt.Rows(i)("panno")
                    txtphone.Text = dt.Rows(i)("Phone")
                    txtadd1.Text = dt.Rows(i)("Add1")
                    txtadd2.Text = dt.Rows(i)("Add2")
                    txtemail.Text = dt.Rows(i)("Email")
                Next
            End If
            btnsave.Enabled = True
            btndelete.Enabled = True
            btnsave.Text = "Update"
            'If userCode <> "ADMIN" Then
            '    If funSetUserAccess() = False Then Exit Sub
            'End If
        Catch ex As Exception

            myMessages.myExceptions(ex)
        End Try
    End Sub
    'Funtion for updation  of data
    Public Sub funupdate()
        Try
            Dim currentdate As Date = Date.Today
            connectSql.RunSp("sp_transportmaster_update", New SqlParameter("@transid", fndtrans.Value), New SqlParameter("@transname", txtname.Text.ToString()), New SqlParameter("@city", txtcity.Text.ToString()), New SqlParameter("@state", txtstate.Text.ToString()), New SqlParameter("@pincode", txtpin.Text.ToString()), New SqlParameter("@panno", txtpan.Text.ToString()), New SqlParameter("@phone", txtphone.Text.ToString()), New SqlParameter("@add1", txtadd1.Text.ToString()), New SqlParameter("@add2", txtadd2.Text.ToString()), New SqlParameter("@email", txtemail.Text.ToString()), New SqlParameter("@modifiedby", userCode), New SqlParameter("@modifieddate", connectSql.serverDate()), New SqlParameter("@compcode", companyCode))
            myMessages.update()
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub
    'Function for deletion of data
    Public Sub fundelete()
        Try
            Dim currentdate As Date = Date.Today
            connectSql.RunSp("sp_transportmaster_delete", New SqlParameter("@transid", fndtrans.Value.ToString()))
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub
    'Delete funtion call on delete button
    Private Sub btndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click
        DeleteData()
    End Sub
    Sub DeleteData()
        If btndelete.Enabled = True Then
            If fndtrans.Value = "" Then
                myMessages.blankValue("Transporter Code")
            ElseIf myMessages.deleteConfirm() Then
                fundelete()
                myMessages.delete()
                btnsave.Text = "Save"
                btndelete.Enabled = False
            End If
        Else
            common.clsCommon.MyMessageBoxShow("You Can Not delete Record")
        End If
    End Sub
    'It will reset all the controls in screens
    Public Sub funreset()
        fndtrans.Value = ""
        fndtrans.MyReadOnly = False
        txtname.Text = ""
        txtcity.Text = ""
        txtstate.Text = ""
        txtpin.Text = ""
        txtpan.Text = ""
        txtphone.Text = ""
        txtemail.Text = ""
        txtadd1.Text = ""
        txtadd2.Text = ""
        btnsave.Text = "Save"
        btndelete.Enabled = False
    End Sub

    Private Sub btnnew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnnew.Click
        funreset()
    End Sub
    'closing of current window form
    Private Sub btnclear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclear.Click

        Me.Close()
    End Sub

    Private Sub txtphone_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtphone.KeyPress
        If ((e.KeyChar >= Chr(48) And e.KeyChar <= Chr(57)) Or e.KeyChar = Chr(8) Or e.KeyChar = Chr(48) Or e.KeyChar = Chr(45)) Then
        Else
            e.Handled = True
        End If
    End Sub
    Private Sub fndtrans_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'fndtrans.ConnectionString = connectSql.SqlCon()
        'fndtrans.Query = " select transport_id As [Transporter Code],transporter_name  as [Transporter Name]from TSPL_TRANSPORT_MASTER "
        'fndtrans.ValueToSelect = "Transporter Code"
        'fndtrans.Caption = "Transport Master"
        'fndtrans.ValueToSelect1 = "Transporter Name"

        'fndtrans.Query = "select Vendor_Code as [Vendor Code],Vendor_Name as [Vendor Name] from TSPL_VENDOR_MASTER where transporter='Y' "
        'fndtrans.ConnectionString = connectSql.SqlCon()
        'fndtrans.Caption = "Vendor Master"
        'fndtrans.ValueToSelect = "Vendor Code"
        'fndtrans.ValueToSelect1 = "Vendor Name"

    End Sub
    'For Export functionality 
    Private Sub transexport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles transexport.Click
        sql = "select transport_id As [Transporter Code],transporter_name As[Transporter Name],city as [City],state as [State],pincode as [Pin Code],panno as [PAN NO],phone as [Phone],add1 As [Address1],add2 as [Address2],email As [Email] from tspl_transport_Master"
        transportSql.ExporttoExcel(sql, Me)
    End Sub

    'For Import functionality 
    Private Sub transimport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles transimport.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim currentdate As Date = Date.Today
        If transportSql.importExcel(gv, "Transporter Code", "Transporter Name", "City", "State", "Pin Code", "PAN NO", "Phone", "Address1", "Address2", "Email") Then
            Dim trans As SqlTransaction = Nothing
            Try
                trans = clsDBFuncationality.GetTransactin()
                clsCommon.ProgressBarShow()
                Dim LineNo As String
                For Each grow As GridViewRowInfo In gv.Rows
                    LineNo = clsCommon.myCstr(grow.Index + 2)

                    Dim strid As String = clsCommon.myCstr(grow.Cells(0).Value)
                    If clsCommon.myLen(strid) > 0 Then
                        strid = clsDBFuncationality.getSingleValue("Select Vendor_Code from TSPL_VENDOR_MASTER Where Vendor_Code='" + strid + "'", trans)
                        If clsCommon.myLen(strid) <= 0 Then
                            Throw New Exception("Transport Id at line '" + LineNo + "' does not exist in Vendor Master.")
                        End If
                    Else
                        Throw New Exception("Transport Id can not be blank at line " + LineNo + ".")
                    End If
                    
                    Dim strname As String = clsDBFuncationality.getSingleValue("Select Vendor_Name from TSPL_VENDOR_MASTER Where Vendor_Code='" + strid + "'", trans)
                    Dim strcity As String = clsCommon.myCstr(grow.Cells(2).Value)
                    If clsCommon.myLen(strcity) > 20 Then
                        Throw New Exception("Check the lenght of City")
                    End If

                    Dim strstate As String = clsCommon.myCstr(grow.Cells(3).Value)
                    If clsCommon.myLen(strstate) > 20 Then
                        Throw New Exception("Check the lenght of State")
                    End If

                    Dim strpin As String = clsCommon.myCstr(grow.Cells(4).Value)
                    If clsCommon.myLen(strpin) > 20 Then
                        Throw New Exception("Check the lenght of Pin Code")
                    End If

                    Dim strpan As String = clsCommon.myCstr(grow.Cells(5).Value)
                    If clsCommon.myLen(strpan) > 20 Then
                        Throw New Exception("Check the lenght of PAN No")
                    End If

                    Dim strphone As String = clsCommon.myCstr(grow.Cells(6).Value)
                    If clsCommon.myLen(strphone) > 10 Then
                        Throw New Exception("Check the lenght of Phone")
                    End If

                    Dim stradd1 As String = clsCommon.myCstr(grow.Cells(7).Value)
                    If clsCommon.myLen(stradd1) > 50 Then
                        Throw New Exception("Check the lenght of Address1")
                    End If

                    Dim stradd2 As String = clsCommon.myCstr(grow.Cells(8).Value)
                    If clsCommon.myLen(stradd2) > 50 Then
                        Throw New Exception("Check the lenght of Address2")
                    End If

                    Dim stremail As String = clsCommon.myCstr(grow.Cells(9).Value)
                    If clsCommon.myLen(stremail) > 50 Then
                        Throw New Exception("Check the lenght of Email")
                    End If


                    Dim check As Match = Regex.Match(stremail, "\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*")
                    If (check.Success = False) And stremail <> "" Then
                        Throw New Exception(" Check the Email format")
                    End If

                    Dim sql1 As String = "select count(*) from tspl_Transport_master where transport_id='" + strid + "'"
                    Dim i As Integer = CInt(connectSql.RunScalar(trans, sql1))
                    If (i = 0) Then
                        connectSql.RunSpTransaction(trans, "sp_TransportMaster_insert", New SqlParameter("@transid", strid), New SqlParameter("@transname", strname), New SqlParameter("@city", strcity), New SqlParameter("@state", strstate), New SqlParameter("@pincode", strpin), New SqlParameter("@panno", strpan), New SqlParameter("@phone", strphone), New SqlParameter("@add1", stradd1), New SqlParameter("@add2", stradd2), New SqlParameter("@email", stremail), New SqlParameter("@createdby", userCode), New SqlParameter("@createddate", connectSql.serverDate(trans)), New SqlParameter("@modifiedby", userCode), New SqlParameter("@modifieddate", connectSql.serverDate(trans)), New SqlParameter("@compcode", companyCode))
                    Else
                        connectSql.RunSpTransaction(trans, "sp_TransportMaster_update", New SqlParameter("@transid", strid), New SqlParameter("@transname", strname), New SqlParameter("@city", strcity), New SqlParameter("@state", strstate), New SqlParameter("@pincode", strpin), New SqlParameter("@panno", strpan), New SqlParameter("@phone", strphone), New SqlParameter("@add1", stradd1), New SqlParameter("@add2", stradd2), New SqlParameter("@email", stremail), New SqlParameter("@modifiedby", userCode), New SqlParameter("@modifieddate", connectSql.serverDate(trans)), New SqlParameter("@compcode", companyCode))
                    End If
                    clsDBFuncationality.ExecuteNonQuery("Update TSPL_VENDOR_MASTER Set Transporter = 'Y',CURRENCY_CODE='" & clsCommon.myCstr(objCommonVar.BaseCurrencyCode) & "' Where Vendor_Code='" + strid + "'", trans)
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
    'For closing current screen by menu strip Close
    Private Sub transclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles transclose.Click
        Me.Close()
    End Sub
    'This will check the authorization of user to access the screen.If authorize then it will allow user to access the screen.
    'Private Function funSetUserAccess() As Boolean
    '    Try

    '        Dim strRights As String
    '        Dim strTemp() As String
    '        Dim strProgCode = "TRANSP-M"
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
    '            btnsave.Enabled = False
    '        End If
    '        If strTemp(2) = "0" Then 'Grant modify access
    '            btndelete.Enabled = False
    '        End If

    '        funSetUserAccess = True
    '    Catch er As Exception
    '        myMessages.myExceptions(er)
    '    End Try
    'End Function


    Private Sub menuprint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuprint.Click
        'Dim frm As New frmTransportMasterRpt()
        'frm.Show()

    End Sub

    Private Sub frmTransportMaster_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If formtype <> clsUserMgtCode.transportMaster Then
            If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnsave.Enabled Then
                SaveData()
            ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag Then
                'PostData()
            ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btndelete.Enabled Then
                DeleteData()
            ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
                Close()
            ElseIf e.Alt And e.KeyCode = Keys.N Then
                funreset()
            End If
        Else
            If e.Alt And e.KeyCode = Keys.N Then
                funreset()
            End If
        End If
    End Sub

    Private Sub fndtrans__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndtrans._MYValidating

        Dim str As String = "select count(*) from TSPL_VENDOR_MASTER where Vendor_Code ='" + fndtrans.Value + "' "
        Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
        If no = 0 Then
            fndtrans.MyReadOnly = False
        Else
            fndtrans.MyReadOnly = True
        End If
        'If fndtrans.MyReadOnly OrElse isButtonClicked Then
        'Dim qry As String = "select Vendor_Code as [VendorCode],Vendor_Name as [Vendor Name] from TSPL_VENDOR_MASTER  "
        'fndtrans.Value = clsCommon.ShowSelectForm("TransCodeMastrID", qry, "VendorCode", " transporter='Y'", fndtrans.Value, "", isButtonClicked)
        fndtrans.Value = clsVendorMaster.getFinder(" transporter='Y'", fndtrans.Value, isButtonClicked)
        If fndtrans.Value IsNot Nothing Then
            btndelete.Enabled = True
        Else
            btndelete.Enabled = False
        End If

        'End If
        text_changed()
        fnd_leave()


    End Sub

    Private Sub fndtrans__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavigatorType As common.NavigatorType) Handles fndtrans._MYNavigator
        Dim qst As String = "select Vendor_Code as [Vendor Code],Vendor_Name as [Vendor Name] from TSPL_VENDOR_MASTER  where  2=2 "
        Select Case NavigatorType
            Case NavigatorType.Current
            Case NavigatorType.Next
                qst += "and Vendor_Code in (select min(Vendor_Code) from TSPL_VENDOR_MASTER where Vendor_Code>'" + fndtrans.Value + "' and transporter='Y'   ) "
            Case NavigatorType.First
                qst += "and Vendor_Code in (select MIN(Vendor_Code) from TSPL_VENDOR_MASTER where transporter='Y'  )"
            Case NavigatorType.Last
                qst += "and Vendor_Code in (select Max(Vendor_Code) from TSPL_VENDOR_MASTER where transporter='Y'  )"
            Case NavigatorType.Previous
                qst += "and Vendor_Code in (select max(Vendor_Code) from TSPL_VENDOR_MASTER where Vendor_Code<'" + fndtrans.Value + "' and transporter='Y'   )"
        End Select
        ' fun_gridfill()
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qst)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            fndtrans.Value = clsCommon.myCstr(dt.Rows(0)("Vendor Code"))
            txtname.Text = clsCommon.myCstr(dt.Rows(0)("Vendor Name"))
        End If
        If fndtrans.Value IsNot Nothing Then
            btndelete.Enabled = True
        Else
            btndelete.Enabled = False

        End If
        text_changed()
    End Sub
End Class
