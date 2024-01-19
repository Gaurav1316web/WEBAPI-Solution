'Developed By -BibhuPrasad Parida
'Database - TSPLERP
'Table -tspl_ship_To_location
'Start Date -
'End Date -
'-20/12/2012:11:00AM--Updation By--Pankaj Kumar--Applied Validations
'-Updation By--[Pankaj Kumar Chaudhary]--Against Ticket No--[BM00000002442]
'--preeti gupta-ticket no-[BM00000003128]


Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.WinControls.UI
Imports System.Text.RegularExpressions
Imports common


Public Class FrmShipToLocationDetails
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim EnableAutoDocNoShipToLocation As Integer = 0


#Region "Constructors"
    Public Sub New(ByVal user As String, ByVal company As String, ByVal strstt As String, ByVal strcusn As String, ByVal strcusnam As String)
        InitializeComponent()
        userCode = user
        companyCode = company
        strShipToType = strstt
        strCustomerNo = strcusn
        strCustomerName = strcusnam

    End Sub
    Public Sub New(ByVal usrcd As String, ByVal comcd As String)
        userCode = usrcd
        companyCode = comcd
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Public Sub New(ByVal user As String, ByVal company As String, ByVal strstt As String, ByVal strcusn As String, ByVal strcusnam As String, ByVal dt As DataTable)
        InitializeComponent()
        userCode = user
        companyCode = company
        strShipToType = strstt
        strCustomerNo = strcusn
        strCustomerName = strcusnam
        If dt.Rows.Count <> 0 Then
            txtShipToLocation.Text = dt.Rows(0)(0).ToString()
            txtShipToLocationDesc.Text = dt.Rows(0)(1).ToString()
            txtAddress1.Text = dt.Rows(0)(2).ToString()
            txtAddress22.Text = dt.Rows(0)(3).ToString()
            txtAddress3.Text = dt.Rows(0)(4).ToString()
            txtAddress4.Text = dt.Rows(0)(5).ToString()
            txtCity.Value = dt.Rows(0)(6).ToString()
            txtState.Value = dt.Rows(0)(7).ToString()
            txtPostalCode.Text = dt.Rows(0)(8).ToString()
            txtCountry.Value = dt.Rows(0)(9).ToString()
            txtTelephone.Text = dt.Rows(0)(10).ToString()
            txtEmail.Text = dt.Rows(0)(11).ToString()
            btnAdd.Text = "Update"
            btnDelete.Enabled = True
        End If

    End Sub


    Public Sub New(ByVal stlshiptloc As String, ByVal stlshiptdesc As String, ByVal stradd1 As String, ByVal stradd2 As String, ByVal stradd3 As String, ByVal stradd4 As String, ByVal strcity As String, ByVal strstate As String, ByVal strpostalcode As String, ByVal strcountry As String, ByVal strtelephone As String, ByVal stremail As String)
        InitializeComponent()
    End Sub
#End Region
#Region "Variables"
    Dim GstApplicable As Boolean = False
    Dim userCode, companyCode As String
    Dim strShipToType As String
    Dim strCustomerNo As String
    Dim strCustomerName As String
    Dim sql As String
    Dim ds As DataSet
    Dim dr As DataTable
    Dim tableName As String = "TSPL_TAX_MASTER"
    Dim tableCode As String = "Tax_Code"
    Dim codePrefix As String = "TAX"
    Dim objstr As String = "Tecxpert Software Pvt Ltd."
    Dim dt As Date = Date.Today

#End Region
#Region "Page Load"
    Private Sub SetUserMgmtNew()

        ''MyBase.SetUserMgmt("SHIP_LOC_D")
        'MyBase.SetUserMgmt(clsUserMgtCode.frmShipToLocationDetails)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
            Me.Close()
            Exit Sub
        End If
        btnAdd.Visible = MyBase.isModifyFlag
        '--------richa Ticket no. BM00000003014 02/07/2014 to enable/disable import/export option acc. to user mgmt setting -----------
        If btnAdd.Visible = True Then
            menuImport1.Enabled = True
            menuExport.Enabled = True
        Else
            menuImport1.Enabled = False
            menuExport.Enabled = False
        End If
        '--------------------------------------------------
        'btnPost.Visible = MyBase.isPostFlag
        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub
   
    Private Sub FrmShipToLocationDetails_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        EnableAutoDocNoShipToLocation = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.EnableAutoDocNoShipToLocation, clsFixedParameterCode.EnableAutoDocNoShipToLocation, Nothing))
        If EnableAutoDocNoShipToLocation = 1 Then
            txtShipToLocation.Enabled = False
        End If
        SetUserMgmtNew()
        'fndCustomer.Text = strCustomerNo
        'txtCustomerName.Text = strCustomerName
        If fndCustomer.Text <> "" Or txtShipToLocation.Text <> "" Then
            btnDelete.Enabled = True
        Else
            btnDelete.Enabled = False
        End If
        ButtonToolTip.SetToolTip(btnAdd, "Press Alt+S for Save/Update Trasnaction")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnNew, "Press Alt+N Adding New Trasnaction")
        ApplyReadOnly()
        ValidateLength()
        resetshipdetail()
        LoadLocation(txtShipToLocation.Text, txtcustomer.Value)
        '----------For Custom Fields----------
        RadPageView1.Pages("pvpCustomFields").Item.Visibility = MyBase.customFieldTabProperty
        If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
            UcCustomFields1.Report_ID = MyBase.Form_ID

            UcCustomFields1.LoadCustomControls()
        End If
        '---------End of For Custom Fields----
        ''================Sanjeet (30/05/2017)=======================
        GstApplicable = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.GSTApplicable, clsFixedParameterCode.GSTApplicable, Nothing)) = "1", True, False))
        If GstApplicable Then
            grpboxGST.Enabled = True
            txtGstDefaultValue.Text = "Z"
        Else
            grpboxGST.Enabled = False
        End If
        ''==========================================================

    End Sub

    Private Sub ApplyReadOnly()
        fndCustomer.ReadOnly = True
        txtCustomerName.ReadOnly = True
    End Sub
    Private Sub ValidateLength()
        txtAddress1.MaxLength = 50
        txtAddress2.MaxLength = 50
        txtAddress3.MaxLength = 50
        txtAddress4.MaxLength = 50
        'txtCity.MaxLength = 12
        'txtState.MaxLength = 50
        txtPostalCode.MaxLength = 6
        'txtCountry.MaxLength = 50
        txtTelephone.MaxLength = 12
        txtEmail.MaxLength = 50
        txtContactFax.MaxLength = 20
        'txtContPhone.MaxLength = 12
        txtContactEmail.MaxLength = 50
        txtContactName.MaxLength = 50
        txtContactWeb.MaxLength = 50

    End Sub
#End Region
    
#Region "TextChanged Event"

    Private Sub txtCustomer_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

        LoadData()
    End Sub
    Sub LoadData()
        ''txtCustomerName.Text = fndCustomer.Tag

        'If fndCustomer.Text <> "" Then
        '    Dim str As String = clsDBFuncationality.getSingleValue("Select Cust_Code from TSPL_CUSTOMER_MASTER  where Cust_Code='" + fndCustomer.Text + "'")
        '    If str = fndCustomer.Text Then

        '        'fndShipToLocation.ConnectionString = connectSql.SqlCon()
        '        'fndShipToLocation.Query = "select Ship_To_Code as [Ship To Location],Ship_To_Desc as [Description] from TSPL_SHIP_TO_LOCATION  where Ship_To_Type_Code='" + fndCustomer.Text+ "'"
        '        'txtShipToLocation.Text ToSelect = "Ship To Location"
        '        'txtShipToLocation.Text ToSelect1 = "Description"
        '        'fndShipToLocation.Caption = "Ship To Location Details"
        '    Else
        '        ' common.clsCommon.MyMessageBoxShow("Customer does not exist.")
        '    End If

        'Else

        'End If


        'btnOpen.Enabled = False
        'btnDelete.Enabled = False
        If txtcustomer.Value <> "" Then
            'dr = connectSql.RunSqlReturnDR("Select Ship_To_Type_Code from tspl_ship_To_location  where Ship_To_Type_Code='" + fndCustomer.Value + "'")
            sql = "Select Ship_To_Type_Code from tspl_ship_To_location  where Ship_To_Type_Code='" + txtcustomer.Value + "'"
            Dim str As String = ""

            Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(sql)
            If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
                For Each dr As DataRow In dt1.Rows
                    str = dr(0).ToString().ToUpper()
                Next
            End If

            If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
                MasterTemplate.ReadOnly = True
            Else
                MasterTemplate.ReadOnly = False
                MasterTemplate.CurrentRow.Cells(1).ReadOnly = True
                MasterTemplate.CurrentRow.Cells(2).ReadOnly = True
                MasterTemplate.CurrentRow.Cells(3).ReadOnly = True
                MasterTemplate.CurrentRow.Cells(4).ReadOnly = True
                MasterTemplate.CurrentRow.Cells(5).ReadOnly = True
                MasterTemplate.CurrentRow.Cells(6).ReadOnly = True
                MasterTemplate.CurrentRow.Cells(7).ReadOnly = True
                MasterTemplate.CurrentRow.Cells(8).ReadOnly = True
                MasterTemplate.CurrentRow.Cells(9).ReadOnly = True
                MasterTemplate.CurrentRow.Cells(10).ReadOnly = True
                MasterTemplate.CurrentRow.Cells(11).ReadOnly = True
                ' MasterTemplate.CurrentRow.Cells(12).ReadOnly = True
            End If
            If str <> txtcustomer.Value Then
                ' txtCustomerName.Text = ""
                MasterTemplate.DataSource = Nothing
                MasterTemplate.AllowAddNewRow = True

                '  Dim dinfo As GridViewRowInfo = dgShipToLocation.Rows.AddNew
                For i As Integer = 0 To 1
                    MasterTemplate.AllowAddNewRow = True
                Next
            Else
                funFill()
            End If
        ElseIf fndCustomer.Text = "" Then
            txtCustomerName.Text = ""
            MasterTemplate.DataSource = Nothing
            MasterTemplate.AllowAddNewRow = True
        End If

    End Sub
    Private Sub txtShipToLocation_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Loadship()
    End Sub
    Sub Loadship()
        'txtShipToLocationDesc.Text = fndShipToLocation.Tag

        If txtShipToLocation.Text <> "" Then
            Dim str As String
            str = clsDBFuncationality.getSingleValue("Select Ship_To_Code from tspl_ship_To_location  where Ship_To_Code='" + txtShipToLocation.Text + "'")



            If str <> txtShipToLocation.Text Then
                txtShipToLocationDesc.Text = ""
                btnAdd.Text = "Save"
                btnDelete.Enabled = False
            Else
                funFill()
            End If
        End If
    End Sub
#End Region
#Region "KeyPress Event"

    Private Sub txtShipToLocation_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If Microsoft.VisualBasic.Asc(e.KeyChar) = 39 Then
            e.Handled = True
        End If
    End Sub

    Private Sub fndCustomer_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If Microsoft.VisualBasic.Asc(e.KeyChar) = 39 Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtPostalCode_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPostalCode.KeyPress
        If IsNumeric(e.KeyChar) = True Then
            e.Handled = False
        ElseIf Microsoft.VisualBasic.Asc(e.KeyChar) = 8 Then
        Else
            e.Handled = True
        End If
    End Sub
#End Region
#Region "ButtonClick Event"
    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        If myMessages.deleteConfirm() Then
            funDelete()
        Else
        End If
    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        SaveData()
    End Sub

    Sub SaveData()
        Try
            If MyBase.isModifyonPasswordFlag Then
                If clsPasswordCheckForMasters.CheckMasterPwd(clsUserMgtCode.frmShipToLocationDetails, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
                Else
                    Return
                End If
            End If
            If GstApplicable Then
                If chkGstRegistered.Checked Then
                    If clsCommon.myLen(txtpan.Text) <= 0 Then
                        clsCommon.MyMessageBoxShow(Me, "Please Enter Customer Pan No.", Me.Text)
                        txtpan.Focus()
                        txtpan.Select()
                        Exit Sub
                    End If
                    txtGstInNo.Text = clsCommon.myCstr(txtGSTstateCode.Text) + clsCommon.myCstr(txtGstPanNo.Text) + clsCommon.myCstr(txtGstEntityNo.Text) + clsCommon.myCstr(txtGstDefaultValue.Text) + clsCommon.myCstr(txtGstLastNo.Text)
                    Dim StrMsg As String = clsERPFuncationality.ValidationGSTNO(txtGSTstateCode.Text, txtGstPanNo.Text, clsCommon.myCstr(txtGstInNo.Text), Nothing)
                    If clsCommon.myCstr(StrMsg) = "False" Then
                        Exit Sub
                    End If
                End If
            End If

            If btnAdd.Text = "Save" Then
                If fndCustomer.Text = "" Then
                    myMessages.blankValue("Customer Code")
                    Exit Sub
                ElseIf txtShipToLocation.Text = "" And EnableAutoDocNoShipToLocation = 0 Then
                    myMessages.blankValue("Ship To Location")
                    txtShipToLocation.Focus()
                    Exit Sub
                ElseIf fndLocation.Value = "" And EnableAutoDocNoShipToLocation = 1 Then
                    myMessages.blankValue("Location")
                    fndLocation.Focus()
                    Exit Sub
                End If
                If funInsert() Then
                    ''For Custom Fields
                    Dim arrCustomFields As New List(Of clsCustomFieldValues)
                    If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
                        UcCustomFields1.GetData(arrCustomFields)
                    End If
                    clsCustomFieldValues.SaveData(MyBase.Form_ID, txtShipToLocation.Text, arrCustomFields, Nothing)
                    InsertLocations(txtShipToLocation.Text, cbgLocation.CheckedValue, txtcustomer.Value)
                    '''''' end of custom field
                    funFill()
                    resetshipdetail()
                    myMessages.insert()
                End If
            ElseIf btnAdd.Text = "Update" Then
                If txtShipToLocation.Text = "" And EnableAutoDocNoShipToLocation = 0 Then
                    myMessages.blankValue("Ship To Location")
                    txtShipToLocation.Focus()
                    Exit Sub
                ElseIf fndLocation.Value = "" And EnableAutoDocNoShipToLocation = 1 Then
                    myMessages.blankValue("Location")
                    fndLocation.Focus()
                    Exit Sub
                End If
                    If funUpdate() Then
                        ''For Custom Fields
                        Dim arrCustomFields As New List(Of clsCustomFieldValues)
                        If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
                            UcCustomFields1.GetData(arrCustomFields)
                        End If
                        clsCustomFieldValues.SaveData(MyBase.Form_ID, txtShipToLocation.Text, arrCustomFields, Nothing)
                    InsertLocations(txtShipToLocation.Text, cbgLocation.CheckedValue, txtcustomer.Value)
                    '''''' end of custom field
                    funFill()
                        resetshipdetail()
                        myMessages.update()
                    End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        funReset()
        txtShipToLocation.Focus()
    End Sub
    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub menuClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuClose.Click
        Me.Close()
    End Sub

#End Region
#Region "Methods"
    ''insert Ship To Location Details
    Private Function funInsert() As Boolean
        Dim check As Match = Regex.Match(txtEmail.Text, "\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*")

        If check.Success = False And txtEmail.Text <> "" Then
            common.clsCommon.MyMessageBoxShow(Me, "Please insert the proper format of e-mail address", Me.Text)
            txtEmail.Text = ""
            txtEmail.Focus()
            Return False
        Else
            Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
            Try
                Dim strShipCode As String = Nothing
                If EnableAutoDocNoShipToLocation = 1 Then
                    strShipCode = clsERPFuncationality.GetNextCode(trans, clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"), clsDocType.ShipToLocation, "", fndLocation.Value, False, True)
                    txtShipToLocation.Text = strShipCode
                End If
                connectSql.RunSpTransaction(trans, "sp_TSPL_SHIP_TO_LOCATION_insert", New SqlParameter("@ShipToTypeCode", fndCustomer.Text), New SqlParameter("@ShipToTypeDesc", txtCustomerName.Text), New SqlParameter("@ShipToType", "S"), New SqlParameter("@ShipToCode", txtShipToLocation.Text), New SqlParameter("@ShipToDesc", txtShipToLocationDesc.Text), New SqlParameter("@Add1", txtAddress1.Text), New SqlParameter("@Add2", txtAddress22.Text), New SqlParameter("@Add3", txtAddress3.Text), New SqlParameter("@Add4", txtAddress4.Text), New SqlParameter("@CityCode", txtCity.Value), New SqlParameter("@State", txtState.Value), New SqlParameter("@PinCode", txtPostalCode.Text), New SqlParameter("@Country", txtCountry.Value), New SqlParameter("@Telephone", txtTelephone.Text), New SqlParameter("@Email", txtEmail.Text), New SqlParameter("@Createdby", userCode), New SqlParameter("@Createddate", connectSql.serverDate(trans)), New SqlParameter("@Modifiedby", userCode), New SqlParameter("@Modifieddate", connectSql.serverDate(trans)), New SqlParameter("@CompCode", companyCode), New SqlParameter("@Tin_No", clsCommon.myCstr(txtTinNo.Text)), New SqlParameter("@CST_No", clsCommon.myCstr(txtCSTNo.Text)), New SqlParameter("@Loc_Code", clsCommon.myCstr(fndLocation.Value)))
                Dim strRegion_Type As String = ""
                Dim intGSTRegistered As Integer = 0
                Dim intGSTCOMPOSITION As Integer = 0
                Dim intOtherForPAN As Integer = 0
                If rbtnDomestic.IsChecked Then
                    strRegion_Type = "D"
                End If
                If rbtnForeign.IsChecked Then
                    strRegion_Type = "F"
                End If
                If chkGstRegistered.Checked Then
                    intGSTRegistered = 1
                End If
                If chkComposition.Checked Then
                    intGSTCOMPOSITION = 1
                End If
                If ChkOther.Checked = True Then
                    intOtherForPAN = 1
                Else
                    intOtherForPAN = 0
                End If

                'done by priti KDI/24/04/18-000273
                Dim qry = "update TSPL_SHIP_TO_LOCATION set Contact_Person_Name='" & clsCommon.myCstr(txtContactName.Text) & "', " & _
                        "Contact_Person_Fax='" & clsCommon.myCstr(txtContactFax.Text) & "',Contact_Person_Email='" & clsCommon.myCstr(txtContactEmail.Text) & "', " & _
                        "Contact_Person_Phone='" & clsCommon.myCstr(txtContPhone.Text) & "',Contact_Person_Website='" & clsCommon.myCstr(txtContactWeb.Text) & "', " & _
                        "[VehicleNo ]='" & clsCommon.myCstr(txtVehicleNo.Text) & "',[Driver_Name ]='" & clsCommon.myCstr(txtDriverFinder.Value) & "',[Driver_Mobile_No ]='" & clsCommon.myCstr(txtDriverMobileNo.Text) & "', " & _
                        "GSTNO='" & clsCommon.myCstr(txtGstInNo.Text) & "',GSTEntity='" & clsCommon.myCstr(txtGstEntityNo.Text) & "', " & _
                        "GSTBlank='" & clsCommon.myCstr(txtGstDefaultValue.Text) & "',GSTDigit='" & clsCommon.myCstr(txtGstLastNo.Text) & "', " & _
                        "Region_Type='" & strRegion_Type & "',GST_Registered='" & intGSTRegistered & "',GST_COMPOSITION='" & intGSTCOMPOSITION & "', " & _
                        "Pan='" & txtpan.Text & "',Other_For_PAN='" & intOtherForPAN & "',Add3='" & txtAddress3.Text & "'" & _
                        "where Ship_To_Type_Code='" & fndCustomer.Text & "' and Ship_To_Code='" & txtShipToLocation.Text & "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
                trans.Commit()

                Return True

            Catch ex As Exception
                trans.Rollback()
                clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
                Return False
            End Try
        End If

    End Function
    ''update Ship To Location Details
    Private Function funUpdate() As Boolean
        Dim check As Match = Regex.Match(txtEmail.Text, "\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*")

        If check.Success = False And txtEmail.Text <> "" Then

            common.clsCommon.MyMessageBoxShow(Me, "Please insert the proper format of e-mail address", Me.Text)
            txtEmail.Text = ""
            txtEmail.Focus()
            Return False
            'ElseIf txtEmail.Text = "" Then
            '    connectSql.RunSp("sp_TSPL_SHIP_TO_LOCATION_update", New SqlParameter("@ShipToTypeCode",  fndCustomer.Text), New SqlParameter("@ShipToTypeDesc", txtCustomerName.Text), New SqlParameter("@ShipToType", strShipToType), New SqlParameter("@ShipToCode", txtShipToLocation.Text ), New SqlParameter("@ShipToDesc", txtShipToLocationDesc.Text), New SqlParameter("@Add1", txtAddress1.Text), New SqlParameter("@Add2", txtAddress22.Text), New SqlParameter("@Add3", txtAddress3.Text), New SqlParameter("@Add4", txtAddress4.Text), New SqlParameter("@CityCode", txtCity.Text), New SqlParameter("@State", txtState.Text), New SqlParameter("@PinCode", txtPostalCode.Text), New SqlParameter("@Country", txtCountry.Text), New SqlParameter("@Telephone", txtTelephone.Text), New SqlParameter("@Email", txtEmail.Text), New SqlParameter("@Createdby", userCode), New SqlParameter("@Createddate", connectSql.serverDate()), New SqlParameter("@Modifiedby", userCode), New SqlParameter("@Modifieddate", connectSql.serverDate()), New SqlParameter("@CompCode", companyCode))
            '    myMessages.update()
        Else
            Try
                connectSql.RunSp("sp_TSPL_SHIP_TO_LOCATION_update", New SqlParameter("@ShipToTypeCode", fndCustomer.Text), New SqlParameter("@ShipToTypeDesc", txtCustomerName.Text), New SqlParameter("@ShipToType", "S"), New SqlParameter("@ShipToCode", txtShipToLocation.Text), New SqlParameter("@ShipToDesc", txtShipToLocationDesc.Text), New SqlParameter("@Add1", txtAddress1.Text), New SqlParameter("@Add2", txtAddress22.Text), New SqlParameter("@Add3", txtAddress3.Text), New SqlParameter("@Add4", txtAddress4.Text), New SqlParameter("@CityCode", txtCity.Value), New SqlParameter("@State", txtState.Value), New SqlParameter("@PinCode", txtPostalCode.Text), New SqlParameter("@Country", txtCountry.Value), New SqlParameter("@Telephone", txtTelephone.Text), New SqlParameter("@Email", txtEmail.Text), New SqlParameter("@Createdby", userCode), New SqlParameter("@Createddate", connectSql.serverDate()), New SqlParameter("@Modifiedby", userCode), New SqlParameter("@Modifieddate", connectSql.serverDate()), New SqlParameter("@CompCode", companyCode), New SqlParameter("@Tin_No", clsCommon.myCstr(txtTinNo.Text)), New SqlParameter("@CST_No", clsCommon.myCstr(txtCSTNo.Text)), New SqlParameter("@Loc_Code", clsCommon.myCstr(fndLocation.Value)))
                Dim strRegion_Type As String = ""
                Dim intGSTRegistered As Integer = 0
                Dim intGSTCOMPOSITION As Integer = 0
                Dim intOtherForPAN As Integer = 0
                If rbtnDomestic.IsChecked Then
                    strRegion_Type = "D"
                End If
                If rbtnForeign.IsChecked Then
                    strRegion_Type = "F"
                End If
                If chkGstRegistered.Checked Then
                    intGSTRegistered = 1
                End If
                If chkComposition.Checked Then
                    intGSTCOMPOSITION = 1
                End If
                If ChkOther.Checked = True Then
                    intOtherForPAN = 1
                Else
                    intOtherForPAN = 0
                End If

                Dim qry = "update TSPL_SHIP_TO_LOCATION set Contact_Person_Name='" & clsCommon.myCstr(txtContactName.Text) & "', " & _
                          "Contact_Person_Fax='" & clsCommon.myCstr(txtContactFax.Text) & "',Contact_Person_Email='" & clsCommon.myCstr(txtContactEmail.Text) & "', " & _
                          "Contact_Person_Phone='" & clsCommon.myCstr(txtContPhone.Text) & "',Contact_Person_Website='" & clsCommon.myCstr(txtContactWeb.Text) & "', " & _
                          "[VehicleNo ]='" & clsCommon.myCstr(txtVehicleNo.Text) & "',[Driver_Name ]='" & clsCommon.myCstr(txtDriverFinder.Value) & "',[Driver_Mobile_No ]='" & clsCommon.myCstr(txtDriverMobileNo.Text) & "', " & _
                          "GSTNO='" & clsCommon.myCstr(txtGstInNo.Text) & "',GSTEntity='" & clsCommon.myCstr(txtGstEntityNo.Text) & "', " & _
                          "GSTBlank='" & clsCommon.myCstr(txtGstDefaultValue.Text) & "',GSTDigit='" & clsCommon.myCstr(txtGstLastNo.Text) & "', " & _
                          "Region_Type='" & strRegion_Type & "',GST_Registered='" & intGSTRegistered & "',GST_COMPOSITION='" & intGSTCOMPOSITION & "', " & _
                          "Pan='" & txtpan.Text & "',Other_For_PAN='" & intOtherForPAN & "'" & _
                          "where Ship_To_Type_Code='" & fndCustomer.Text & "' and Ship_To_Code='" & txtShipToLocation.Text & "'"
                clsDBFuncationality.ExecuteNonQuery(qry)

                Return True
            Catch ex As Exception

                myMessages.myExceptions(ex)
                Return False
            End Try
        End If

    End Function
    ''delete Ship To Location Details
    Private Sub funDelete()
        Try
            If txtShipToLocation.Text = "" Then
                clsCommon.MyMessageBoxShow(Me, "Empty Ship To Code", Me.Text)
                Exit Sub
            End If
            connectSql.RunSp("sp_TSPL_SHIP_TO_LOCATION_delete", New SqlParameter("@ShipToCode", txtShipToLocation.Text), New SqlParameter("@ShipToTypeCode", fndCustomer.Text))
            If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
                clsCustomFieldValues.DeleteData(MyBase.Form_ID, txtShipToLocation.Text, Nothing)
            End If
            funFill()
            resetshipdetail()
            myMessages.delete()
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
        btnDelete.Enabled = False
        btnAdd.Text = "Save"
    End Sub
    ''fill Ship To Location Details
    Private Sub funFill()
        'Dim dt As DataTable
        'dt = clsDBFuncationality.GetDataTable(" select Add1,Add2,Add3,Add4,City_Code,State,Pin_Code,Country,Telphone,Email from TSPL_SHIP_TO_LOCATION  where Ship_To_Type_Code ='" + strCustomerNo + "' and Ship_To_Code  ='" + txtShipToLocation.Text  + "' ")
        'If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
        '    For Each row As DataRow In dt.Rows
        '        txtAddress1.Text = row(0).ToString()
        '        txtAddress22.Text = row(1).ToString()
        '        txtAddress3.Text = row(2).ToString()
        '        txtAddress4.Text = row(3).ToString()
        '        txtCity.Text = row(4).ToString()
        '        txtState.Text = row(5).ToString()
        '        txtPostalCode.Text = row(6).ToString()
        '        txtCountry.Text = row(7).ToString()
        '        txtTelephone.Text = row(8).ToString()
        '        txtEmail.Text = row(9).ToString()
        '    Next
        'End If
        'btnAdd.Text = "Update"
        'btnAdd.Enabled = True

        ''If userCode <> "ADMIN" Then
        ''    If funSetUserAccess() = False Then Exit Sub
        ''End If

        MasterTemplate.AutoGenerateColumns = False
        MasterTemplate.DataSource = Nothing
        ds = connectSql.RunSQLReturnDS("select Ship_To_Code,Ship_To_Desc,Add1,Add2,Add3,Add4,City_Code,State,Pin_Code,Country,Telphone,Email, Tin_No, CST_No,Loc_Code,Contact_Person_Name,Contact_Person_Fax,Contact_Person_Email,Contact_Person_Phone,Contact_Person_Website,VehicleNo ,Driver_Name ,Driver_Mobile_No,GSTNO,GSTEntity,GSTBlank,GSTDigit,Region_Type,GST_Registered,GST_COMPOSITION,Other_For_PAN,PAN from TSPL_SHIP_TO_LOCATION  where Ship_To_Type_Code='" + txtcustomer.Value + "'")
        MasterTemplate.DataSource = ds.Tables(0)
        MasterTemplate.Columns(0).Width = 80
        MasterTemplate.Columns(1).Width = 80
        MasterTemplate.Columns(2).Width = 80
        MasterTemplate.Columns(3).Width = 80
        MasterTemplate.Columns(4).Width = 80
        MasterTemplate.Columns(5).Width = 80
        MasterTemplate.Columns(6).Width = 80
        MasterTemplate.Columns(7).Width = 80
        MasterTemplate.Columns(8).Width = 80
        MasterTemplate.Columns(9).Width = 80
        MasterTemplate.Columns(10).Width = 80
        MasterTemplate.Columns(11).Width = 80
        'MasterTemplate.Columns(14).Width = 80
        MasterTemplate.Columns(0).FieldName = "Ship_To_Code"
        MasterTemplate.Columns(1).FieldName = "Ship_To_Desc"
        MasterTemplate.Columns(2).FieldName = "Add1"
        MasterTemplate.Columns(3).FieldName = "Add2"
        MasterTemplate.Columns(4).FieldName = "Add3"
        MasterTemplate.Columns(5).FieldName = "Add4"
        MasterTemplate.Columns(6).FieldName = "City_Code"
        MasterTemplate.Columns(7).FieldName = "State"
        MasterTemplate.Columns(8).FieldName = "Pin_Code"
        MasterTemplate.Columns(9).FieldName = "Country"
        MasterTemplate.Columns(10).FieldName = "Telphone"
        MasterTemplate.Columns(11).FieldName = "Email"
        MasterTemplate.Columns("colTinNo").FieldName = "Tin_No"
        MasterTemplate.Columns("colCST").FieldName = "CST_No"
        MasterTemplate.Columns(14).FieldName = "Loc_Code"
        MasterTemplate.Columns(15).FieldName = "Contact_Person_Name"
        MasterTemplate.Columns(16).FieldName = "Contact_Person_Fax"
        MasterTemplate.Columns(17).FieldName = "Contact_Person_Email"
        MasterTemplate.Columns(18).FieldName = "Contact_Person_Phone"
        MasterTemplate.Columns(19).FieldName = "Contact_Person_Website"
        MasterTemplate.Columns(20).FieldName = "VehicleNo"
        MasterTemplate.Columns(21).FieldName = "Driver_Name"
        MasterTemplate.Columns(22).FieldName = "Driver_Mobile_No"
        MasterTemplate.Columns(23).FieldName = "PAN"
        MasterTemplate.Columns(24).FieldName = "Other_For_PAN"

        MasterTemplate.Columns(25).FieldName = "GSTNO"
        MasterTemplate.Columns(26).FieldName = "GSTEntity"
        MasterTemplate.Columns(27).FieldName = "GSTBlank"
        MasterTemplate.Columns(28).FieldName = "GSTDigit"
        MasterTemplate.Columns(29).FieldName = "Region_Type"
        MasterTemplate.Columns(30).FieldName = "GST_Registered"
        MasterTemplate.Columns(31).FieldName = "GST_COMPOSITION"

       

    

        'MasterTemplate.ReadOnly = True
        ' MasterTemplate.AllowEditRow = False

        'dr = connectSql.RunSqlReturnDR("select Ship_To_Type_Code from TSPL_SHIP_TO_LOCATION  where Ship_To_Type_Code='" + fndCustomer.Value + "'")
        sql = "select Ship_To_Type_Code from TSPL_SHIP_TO_LOCATION  where Ship_To_Type_Code='" + txtcustomer.Value + "'"


        Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(sql)
        If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
            'btnOpen.Enabled = True
            'btnNew.Enabled = True
            'btnDelete.Enabled = True
        Else
            'btnOpen.Enabled = False
            'btnNew.Enabled = True
            'btnDelete.Enabled = False
        End If


        'If userCode <> "ADMIN" Then
        ' If funSetUserAccess() = False Then Exit Sub
        'End If

    End Sub
    Private Sub txtpan_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtpan.TextChanged
        Try
            If clsCommon.myLen(txtpan.Text) <= 0 Then
                Exit Sub
            End If
            If ChkOther.Checked = False Then
                Dim msg As String = clsERPFuncationality.CheckPanStructure(txtpan.Text.Trim(), txtCustomerName.Text)
                If clsCommon.myLen(msg) > 0 Then
                    clsCommon.MyMessageBoxShow(Me, msg, Me.Text)
                    txtpan.Focus()
                    txtpan.Select()
                End If
                If GstApplicable Then
                    txtGstPanNo.Text = txtpan.Text.Trim()
                End If

            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    ''reset Ship To Location Details
    Private Sub funReset()
        txtGSTstateCode.Text = ""
        txtGstPanNo.Text = ""
        txtGstEntityNo.Text = ""
        txtGstDefaultValue.Text = "Z"
        txtGstLastNo.Text = ""
        txtGstInNo.Text = ""
        rbtnDomestic.IsChecked = False
        rbtnForeign.IsChecked = False
        chkGstRegistered.Checked = False
        chkComposition.Checked = False
        txtpan.Text = ""
        txtContactName.Text = ""
        txtContPhone.Text = "(+__)__________"
        txtContactFax.Text = ""
        txtContactWeb.Text = ""
        txtContactEmail.Text = ""
        txtDriverMobileNo.Text = "(+__)__________"
        txtDriverFinder.Value = ""
        txtVehicleNo.Text = ""
        txtShipToLocation.Text = ""
        txtShipToLocationDesc.Text = ""
        txtAddress1.Text = ""
        txtAddress22.Text = ""
        txtAddress3.Text = ""
        txtAddress4.Text = ""
        txtCity.Value = ""
        txtState.Value = ""
        txtPostalCode.Text = ""
        txtCountry.Value = ""
        TxtCountryName.Text = ""
        txtStateName.Text = ""
        txtCityName.Text = ""
        txtTelephone.Text = ""
        txtEmail.Text = ""
        txtTinNo.Text = ""
        txtCSTNo.Text = ""
        btnAdd.Text = "Save"
        btnAdd.Enabled = True
        btnDelete.Enabled = False
        'txtShipToLocation.Focus()
        fndLocation.Value = ""
        lblLocationDesc.Text = ""
        Dim LocationArr As New ArrayList
        cbgLocation.CheckedValue = LocationArr
    End Sub
#End Region
#Region "Finder Load"
    Private Sub txtShipToLocation_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'txtShipToLocation.ConnectionString = connectSql.SqlCon()
        'txtShipToLocation.Query = "select Ship_To_Code as [Ship To Location],Ship_To_Desc as [Description] from TSPL_SHIP_TO_LOCATION  where Ship_To_Type_Code='" + strCustomerNo + "'"
        'txtShipToLocation.Text ToSelect = "Ship To Location"
        'txtShipToLocation.Text ToSelect1 = "Description"
        'txtShipToLocation.Caption = "Ship To Location Details"
    End Sub
    Private Sub fndCustomer_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'fndCustomer.ConnectionString = connectSql.SqlCon()
        'fndCustomer.Query = "select Cust_Code as [Customer Code],Customer_Name as [Customer Name] from TSPL_CUSTOMER_MASTER "
        'fndCustomer.TextToSelect = "Customer Code"
        'fndCustomer.TextToSelect1 = "Customer Name"
        'fndCustomer.Caption = "Customer Details"
    End Sub
#End Region
#Region "Finder Leave Event"
    Private Sub fndCustomer_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If fndCustomer.Text <> "" Then
            Dim s As String = clsDBFuncationality.getSingleValue("select Cust_Code  from TSPL_CUSTOMER_MASTER where Cust_Code='" + fndCustomer.Text + "'")
            If s <> fndCustomer.Text Then
                common.clsCommon.MyMessageBoxShow(Me, "Customer doesn't exist.", Me.Text)
                fndCustomer.Text = ""
                txtCustomerName.Text = ""
                funReset()
                fndCustomer.Focus()
            Else

            End If
        Else
        End If
    End Sub
#End Region
#Region "Import/Export "

    Private Sub menuExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuExport.Click
        sql = "select Ship_To_Type_Code,Ship_To_Type_Desc,Ship_To_Code,Ship_To_Desc,Ship_To_Type, Add1,Add2,Add3,Add4,City_Code,State,Pin_Code,Country,Telphone,Email, Tin_No, CST_No,Loc_Code from TSPL_SHIP_TO_LOCATION "
        If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
            transportSql.ExporttoExcelWithCustomField(sql, "", "", Me, MyBase.Form_ID)
        Else
            ListImpExpColumnsMandatory = New List(Of String)({"Ship_To_Type_Code", "Ship_To_Code"})
            ListImpExpColumnsSuperMandatory = New List(Of String)({"Ship_To_Type_Code", "Ship_To_Code"})
            transportSql.ExporttoExcel(sql, "", "", Me, ListImpExpColumnsMandatory, ListImpExpColumnsSuperMandatory, MyBase.Form_ID)
        End If

    End Sub

    Private Sub menuImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuImport.Click

    End Sub
#End Region
#Region "Print"
    Private Sub menuPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuPrint.Click
        Dim stl As New FrmShipToLocationReport_vb()
        stl.ShowDialog()
    End Sub
#End Region

    Private Sub FrmShipToLocationDetails_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Control And e.KeyCode.ToString() = "S" Then

            ' funInsert()
        End If
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnAdd.Enabled Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag Then
            'PostData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btnDelete.Enabled Then
            funDelete()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Close()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            funReset()
            txtShipToLocation.Focus()
        End If
    End Sub
    Private Sub menuImport1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuImport1.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        '''''''''''' retrieving associated custom field list
        Dim drr As DataTable
        Dim qry1 As String = "select TSPL_CUSTOM_FIELD_HEAD.Name,TSPL_CUSTOM_FIELD_HEAD.Code,TSPL_CUSTOM_FIELD_MAPPING.Program_Code  from TSPL_CUSTOM_FIELD_HEAD left outer join TSPL_CUSTOM_FIELD_MAPPING on TSPL_CUSTOM_FIELD_MAPPING.Custom_Field_Code =TSPL_CUSTOM_FIELD_HEAD.Code WHERE PROGRAM_CODE='" & MyBase.Form_ID & "'"
        drr = clsDBFuncationality.GetDataTable(qry1)
        Dim str(0 To ((18 + drr.Rows.Count) - 1)) As String
        'Ship_To_Code	Ship_To_Desc	Ship_To_Type_Code	Ship_To_Type_Desc	Ship_To_Type	Add1	Add2	Add3	Add4	City_Code	State	Pin_Code	Country	Telphone	Email

        str(0) = "Ship_To_Code"
        str(1) = "Ship_To_Desc"
        str(2) = "Ship_To_Type_Code"
        str(3) = "Ship_To_Type_Desc"
        str(4) = "Ship_To_Type"
        str(5) = "Add1"
        str(6) = "Add2"
        str(7) = "Add3"
        str(8) = "Add4"
        str(9) = "City_Code"
        str(10) = "State"
        str(11) = "Pin_Code"
        str(12) = "Country"
        str(13) = "Telphone"
        str(14) = "Email"
        str(15) = "Tin_No"
        str(16) = "CST_No"
        str(17) = "Loc_Code"
        Dim i As Integer = 18
        For Each row As DataRow In drr.Rows
            str(i) = row(0).ToString
            i = i + 1
        Next
        Dim rownum As Integer = 0
        If transportSql.importExcel(gv, str) Then
            clsCommon.ProgressBarShow()
            Dim trans As SqlTransaction = Nothing
            Try
                'connectSql.OpenConnection()
                trans = clsDBFuncationality.GetTransactin()
                For Each grow As GridViewRowInfo In gv.Rows
                    rownum = rownum + 1
                    Dim strCCode As String
                    Dim strCName As String
                    Dim strStType As String
                    Dim strLCode As String
                    Dim strLDesc As String
                    Dim strAdd1 As String
                    Dim strAdd2 As String
                    Dim strAdd3 As String
                    Dim strAdd4 As String
                    Dim strCity As String
                    Dim strStProvince As String
                    Dim strPCode As String
                    Dim strCountry As String
                    Dim strTelephone As String
                    Dim strEmail As String
                    If grow.Cells(2).Value.ToString() = String.Empty Then
                        myMessages.blankValue("Customer Code")
                        'trans.Rollback()
                        Exit Sub
                    ElseIf grow.Cells(2).Value.ToString().Length > 12 Then
                        Throw New Exception("Customer Code be greater than 12 length")
                        'trans.Rollback()
                        Exit Sub
                    Else
                        strCCode = grow.Cells(2).Value.ToString().ToUpper()
                    End If
                    If grow.Cells(3).Value.ToString().Length > 50 Then
                        Throw New Exception("Customer Name cannot be greater than 50 length")
                        'trans.Rollback()
                        Exit Sub
                    Else
                        strCName = grow.Cells(3).Value.ToString()
                    End If
                    strStType = grow.Cells(4).Value.ToString()

                    If strStType = "S" Then
                        strStType = "S"
                    Else
                        Throw New Exception("ShiP To Type must be S.")

                    End If
                    If grow.Cells(0).Value.ToString() = String.Empty Then
                        Throw New Exception("Location Code")
                        'trans.Rollback()
                        Exit Sub
                    ElseIf grow.Cells(0).Value.ToString().Length > 12 Then
                        Throw New Exception("Location Code be greater than 12 length")
                        'trans.Rollback()
                        Exit Sub
                    Else
                        strLCode = grow.Cells(0).Value.ToString().ToUpper()
                    End If

                    If grow.Cells(1).Value.ToString().Length > 50 Then
                        Throw New Exception("Location Description cannot be greater than 50 length")
                        'trans.Rollback()
                        Exit Sub
                    Else
                        strLDesc = grow.Cells(1).Value.ToString()
                    End If


                    If grow.Cells(5).Value.ToString().Length > 50 Then
                        Throw New Exception("Address1 cannot be greater than 50 length")
                        'trans.Rollback()
                        Exit Sub
                    Else
                        strAdd1 = grow.Cells(5).Value.ToString()
                    End If
                    If grow.Cells(6).Value.ToString().Length > 50 Then
                        Throw New Exception("Address2 cannot be greater than 50 length")
                        'trans.Rollback()
                        Exit Sub
                    Else
                        strAdd2 = grow.Cells(6).Value.ToString()
                    End If
                    If grow.Cells(7).Value.ToString().Length > 50 Then
                        Throw New Exception("Address3 cannot be greater than 50 length")
                        'trans.Rollback()
                        Exit Sub
                    Else
                        strAdd3 = grow.Cells(7).Value.ToString()
                    End If
                    If grow.Cells(8).Value.ToString().Length > 50 Then
                        Throw New Exception("Address4 cannot be greater than 50 length")
                        'trans.Rollback()
                        Exit Sub
                    Else
                        strAdd4 = grow.Cells(8).Value.ToString()
                    End If
                    If grow.Cells(9).Value.ToString().Length > 50 Then
                        Throw New Exception("City cannot be greater than 50 length")
                        'trans.Rollback()
                        Exit Sub
                    Else
                        strCity = grow.Cells(9).Value.ToString()
                    End If
                    If grow.Cells(10).Value.ToString().Length > 50 Then
                        Throw New Exception("State/Province cannot be greater than 50 length")
                        'trans.Rollback()
                        Exit Sub
                    Else
                        strStProvince = grow.Cells(10).Value.ToString()
                    End If
                    If grow.Cells(11).Value.ToString().Length > 9 Then
                        Throw New Exception("Postal Code cannot be greater than 9 length")
                        'trans.Rollback()
                        Exit Sub
                    ElseIf Not IsNumeric(grow.Cells(11).Value) Then
                        Throw New Exception("Char value not allowed in Postal Code.")
                        'trans.Rollback()
                        Exit Sub
                    Else
                        strPCode = grow.Cells(11).Value.ToString()
                    End If
                    If grow.Cells(12).Value.ToString().Length > 50 Then
                        Throw New Exception("Country cannot be greater than 50 length")
                        'trans.Rollback()
                        Exit Sub
                    Else
                        strCountry = grow.Cells(12).Value.ToString()
                    End If
                    If grow.Cells(13).Value.ToString().Length > 50 Then
                        Throw New Exception("Telephone cannot be greater than 50 length")
                        'trans.Rollback()
                        Exit Sub
                    Else
                        strTelephone = grow.Cells(13).Value.ToString()
                    End If
                    If grow.Cells(14).Value.ToString().Length > 50 Then
                        Throw New Exception("Email cannot be greater than 50 length")
                        'trans.Rollback()
                        Exit Sub
                    ElseIf grow.Cells(14).Value.ToString <> String.Empty Then
                        strEmail = grow.Cells(14).Value.ToString()
                        Dim re As Regex = New Regex("\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*")
                        If Not re.IsMatch(strEmail) Then
                            Throw New Exception("Email has some incorrect values")
                            'trans.Rollback()
                            Exit Sub
                        End If
                    Else
                        strEmail = grow.Cells(14).Value.ToString()
                    End If
                    If clsCommon.myLen(grow.Cells("Tin_No").Value) > 20 Then
                        Throw New Exception("Length of Tin No can not be greater than 20")
                        Exit Sub
                    End If
                    If clsCommon.myLen(grow.Cells("CST_No").Value) > 20 Then
                        Throw New Exception("Length of CST No can not be greater than 20")
                        Exit Sub
                    End If
                    If clsCommon.myLen(grow.Cells("Loc_Code").Value) > 0 Then
                        If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*) from TSPL_LOCATION_MASTER where Location_Code='" & clsCommon.myCstr(grow.Cells("Loc_Code").Value) & "'", trans)) = 0 Then
                            Throw New Exception("Invalid Location Code")
                            Exit Sub
                        End If
                    End If
                    Dim sql1 As String = "select COUNT(*) from TSPL_SHIP_TO_LOCATION  where Ship_To_Type_Code='" + strCCode + "' and Ship_To_Code='" + strLCode + "'"
                    i = clsDBFuncationality.getSingleValue(sql1, trans)
                    'connectSql.OpenConnection()
                    'trans = clsDBFuncationality.GetTransactin()
                    Dim dt = connectSql.serverDate(trans)
                    If (i = 0) Then
                        connectSql.RunSpTransaction(trans, "sp_TSPL_SHIP_TO_LOCATION_insert", New SqlParameter("@ShipToTypeCode", strCCode), New SqlParameter("@ShipToTypeDesc", strCName), New SqlParameter("@ShipToType", strStType), New SqlParameter("@ShipToCode", strLCode), New SqlParameter("@ShipToDesc", strLDesc), New SqlParameter("@Add1", strAdd1), New SqlParameter("@Add2", strAdd2), New SqlParameter("@Add3", strAdd3), New SqlParameter("@Add4", strAdd4), New SqlParameter("@CityCode", strCity), New SqlParameter("@State", strStProvince), New SqlParameter("@PinCode", strPCode), New SqlParameter("@Country", strCountry), New SqlParameter("@Telephone", strTelephone), New SqlParameter("@Email", strEmail), New SqlParameter("@Createdby", userCode), New SqlParameter("@Createddate", dt), New SqlParameter("@Modifiedby", userCode), New SqlParameter("@Modifieddate", dt), New SqlParameter("@CompCode", companyCode), New SqlParameter("@Tin_No", "" + clsCommon.myCstr(grow.Cells("Tin_No").Value) + ""), New SqlParameter("@CST_No", "" + clsCommon.myCstr(grow.Cells("CST_No").Value) + ""), New SqlParameter("@Loc_Code", "" + clsCommon.myCstr(grow.Cells("Loc_Code").Value) + ""))
                    Else
                        connectSql.RunSpTransaction(trans, "sp_TSPL_SHIP_TO_LOCATION_update", New SqlParameter("@ShipToTypeCode", strCCode), New SqlParameter("@ShipToTypeDesc", strCName), New SqlParameter("@ShipToType", strStType), New SqlParameter("@ShipToCode", strLCode), New SqlParameter("@ShipToDesc", strLDesc), New SqlParameter("@Add1", strAdd1), New SqlParameter("@Add2", strAdd2), New SqlParameter("@Add3", strAdd3), New SqlParameter("@Add4", strAdd4), New SqlParameter("@CityCode", strCity), New SqlParameter("@State", strStProvince), New SqlParameter("@PinCode", strPCode), New SqlParameter("@Country", strCountry), New SqlParameter("@Telephone", strTelephone), New SqlParameter("@Email", strEmail), New SqlParameter("@Createdby", userCode), New SqlParameter("@Createddate", dt), New SqlParameter("@Modifiedby", userCode), New SqlParameter("@Modifieddate", dt), New SqlParameter("@CompCode", companyCode), New SqlParameter("@Tin_No", "" + clsCommon.myCstr(grow.Cells("Tin_No").Value) + ""), New SqlParameter("@CST_No", "" + clsCommon.myCstr(grow.Cells("CST_No").Value) + ""), New SqlParameter("@Loc_Code", "" + clsCommon.myCstr(grow.Cells("Loc_Code").Value) + ""))
                    End If
                    'trans.Commit()
                    ''''''''''''''  retrieving associated custom field data from shee for table
                    'connectSql.OpenConnection()
                    'trans = clsDBFuncationality.GetTransactin()
                    Dim j As Integer = 15
                    For Each row As DataRow In drr.Rows
                        sql1 = "select COUNT(*) from TSPL_CUSTOM_FIELD_VALUES  where transaction_Code='" + strLCode + "' and program_code='" & MyBase.Form_ID & "' and custom_field_code='" & row(1).ToString & "'"
                        i = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(sql1, trans))
                        If i = 0 Then
                            sql1 = "insert into tspl_custom_field_values values('" & MyBase.Form_ID & "','" & strLCode & "','" & row(1).ToString & "','" & grow.Cells(j).Value & "','0')"
                        Else
                            sql1 = "update tspl_custom_field_values set value='" & grow.Cells(j).Value.ToString & "' where transaction_Code='" + strLCode + "' and program_code='" & MyBase.Form_ID & "' and custom_field_code='" & row(1).ToString & "'"

                        End If
                        clsDBFuncationality.ExecuteNonQuery(sql1, trans)
                        j = j + 1
                    Next
                    ''''''''''''''''End of custom field data retrieval
                Next
                trans.Commit()

                clsCommon.ProgressBarHide()
                common.clsCommon.MyMessageBoxShow(Me, "Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                clsCommon.ProgressBarHide()
                clsCommon.MyMessageBoxShow(Me, ex.Message & " At Row number: " & rownum)
                trans.Rollback()

            End Try

        End If
        Me.Controls.Remove(gv)
    End Sub
    'Private Sub fndCustomer__MYValidating_1(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean)
    '    If fndCustomer.MyReadOnly OrElse isButtonClicked Then
    '        Dim qry As String = "select Cust_Code as [Customer Code],Customer_Name as [Customer Name] from TSPL_CUSTOMER_MASTER "
    '        fndCustomer.Text= clsCommon.ShowSelectForm("CUST_Code", qry, "Customer Code", "", fndCustomer.Text, "", isButtonClicked)
    '        txtCustomerName.Text = clsDBFuncationality.getSingleValue("Select Cust_Code from TSPL_CUSTOMER_MASTER  where Cust_Code='" + fndCustomer.Text+ "'")
    '        LoadData()
    '    End If
    'Loadship()
    'End Sub

    'Private Sub fndCustomer__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType)
    '    Dim qst As String = "select Cust_Code as [Customer Code],Customer_Name as [Customer Name] from TSPL_CUSTOMER_MASTER where 2=2 "
    '    Select Case NavType
    '        Case NavigatorType.Current
    '            qst += " and TSPL_CUSTOMER_MASTER .Cust_Code in ('" + fndCustomer.Text+ "')"
    '        Case NavigatorType.Next
    '            qst += " and TSPL_CUSTOMER_MASTER .Cust_Code in (select min(Cust_Code ) from TSPL_CUSTOMER_MASTER where Cust_Code  >'" + fndCustomer.Text+ "')"
    '        Case NavigatorType.First
    '            qst += " and TSPL_CUSTOMER_MASTER .Cust_Code in (select MIN(Cust_Code ) from TSPL_CUSTOMER_MASTER)"

    '        Case NavigatorType.Last
    '            qst += " and TSPL_CUSTOMER_MASTER .Cust_Code in (select Max(Cust_Code ) from TSPL_CUSTOMER_MASTER)"
    '        Case NavigatorType.Previous
    '            qst += " and TSPL_CUSTOMER_MASTER .Cust_Code in (select Max(Cust_Code ) from TSPL_CUSTOMER_MASTER where Cust_Code  <'" + fndCustomer.Text+ "')"
    '    End Select
    '    Dim dt As DataTable = clsDBFuncationality.GetDataTable(qst)
    '    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
    '        fndCustomer.Text= clsCommon.myCstr(dt.Rows(0)("Customer Code"))
    '        txtCustomerName.Text = clsCommon.myCstr(dt.Rows(0)("Customer Name"))
    '    End If
    '    LoadData()
    '    Loadship()
    'End Sub
    Private Sub txtShipToLocation__MYValidating_1(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean)
        Dim str As String = "select count(*) from TSPL_SHIP_TO_LOCATION   where  Ship_To_Code ='" + txtShipToLocation.Text + "' "

        Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))



        Dim qry As String = "select Ship_To_Code as [ShipToLocation],Ship_To_Desc as [Description] from TSPL_SHIP_TO_LOCATION   "
        txtShipToLocation.Text = clsCommon.ShowSelectForm("SHIP_COD", qry, "ShipToLocation", "  Ship_To_Type_Code = '" + strCustomerNo + "'", txtShipToLocation.Text, "", isButtonClicked)
        txtShipToLocationDesc.Text = clsDBFuncationality.getSingleValue("Select Ship_To_Desc from tspl_ship_To_location  where  Ship_To_Code='" + txtShipToLocation.Text + "'")
        Loadship()
        txtShipToLocationDesc.Text = clsDBFuncationality.getSingleValue("Select Ship_To_Desc from tspl_ship_To_location  where  Ship_To_Code='" + txtShipToLocation.Text + "'")

    End Sub
    Private Sub fndShipToLocation__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType)
        Dim qst As String = "select Ship_To_Code as [ShipToLocation],Ship_To_Desc as [Description] from TSPL_SHIP_TO_LOCATION where 2=2  "
        Select Case NavType
            Case NavigatorType.Current
                qst += " and TSPL_SHIP_TO_LOCATION .Ship_To_Code in ('" + txtShipToLocation.Text + "') and TSPL_SHIP_TO_LOCATION .Ship_To_Type_Code='" + fndCustomer.Text + "' "
            Case NavigatorType.Next
                qst += " and TSPL_SHIP_TO_LOCATION .Ship_To_Code in (select min(Ship_To_Code ) from TSPL_SHIP_TO_LOCATION where Ship_To_Code  >'" + txtShipToLocation.Text + "' and TSPL_SHIP_TO_LOCATION .Ship_To_Type_Code='" + fndCustomer.Text + "')"
            Case NavigatorType.First
                qst += " and TSPL_SHIP_TO_LOCATION .Ship_To_Code in (select MIN(Ship_To_Code ) from TSPL_SHIP_TO_LOCATION) and TSPL_SHIP_TO_LOCATION .Ship_To_Type_Code='" + fndCustomer.Text + "'"

            Case NavigatorType.Last
                qst += " and TSPL_SHIP_TO_LOCATION .Ship_To_Code in (select Max(Ship_To_Code ) from TSPL_SHIP_TO_LOCATION) and TSPL_SHIP_TO_LOCATION .Ship_To_Type_Code='" + fndCustomer.Text + "'"
            Case NavigatorType.Previous
                qst += " and TSPL_SHIP_TO_LOCATION .Ship_To_Code in (select Max(Ship_To_Code )  from TSPL_SHIP_TO_LOCATION where Ship_To_Code  <'" + txtShipToLocation.Text + "' and TSPL_SHIP_TO_LOCATION .Ship_To_Type_Code='" + fndCustomer.Text + "') "
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qst)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            txtShipToLocation.Text = clsCommon.myCstr(dt.Rows(0)("ShipToLocation"))
            txtShipToLocationDesc.Text = clsCommon.myCstr(dt.Rows(0)("Description"))
        End If
        '  LoadData()
        Loadship()
        txtShipToLocationDesc.Text = clsDBFuncationality.getSingleValue("Select Ship_To_Desc from tspl_ship_To_location  where  Ship_To_Code='" + txtShipToLocation.Text + "'")
    End Sub

    Private Sub fndShipToLocation_Load_1(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub txtTelephone_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtTelephone.KeyPress
        If IsNumeric(e.KeyChar) = True Then
            e.Handled = False
        ElseIf Microsoft.VisualBasic.Asc(e.KeyChar) = 8 Then
        Else
            e.Handled = True
        End If
    End Sub



    Private Sub txtcustomer__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtcustomer._MYValidating
        Dim str As String = "select count(*) from TSPL_CUSTOMER_MASTER where  Cust_Code ='" + txtcustomer.Value + "' "

        Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))

        If no = 0 Then
            txtcustomer.MyReadOnly = False
        Else
            txtcustomer.MyReadOnly = True
        End If
        If txtcustomer.MyReadOnly OrElse isButtonClicked Then
            'Dim qry As String = "select Cust_Code as Code,Customer_Name as [Customer Name] from TSPL_CUSTOMER_MASTER "
            'txtcustomer.Value = clsCommon.ShowSelectForm("fmCUST_CODE", qry, "Code", "", txtcustomer.Value, "", isButtonClicked)
            txtcustomer.Value = clsCustomerMaster.getFinder("", txtcustomer.Value, isButtonClicked)
            txtcustomerdesc.Text = clsDBFuncationality.getSingleValue("select Customer_Name  from TSPL_CUSTOMER_MASTER where Cust_Code ='" + txtcustomer.Value + "'")
            LoadData()
        End If
        If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*) from tspl_customer_master where cust_code='" & txtcustomer.Value & "'")) = 0 Then
            clsCommon.MyMessageBoxShow(Me, "Customer Not Found", Me.Text)
            txtcustomer.Value = ""
            txtcustomerdesc.Text = ""
            LoadData()
            MasterTemplate.DataSource = Nothing
            txtcustomer.Focus()
        End If
        resetshipdetail()

    End Sub

    Private Sub txtcustomer__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtcustomer._MYNavigator
        Dim qst As String = "select Cust_Code as [Customer Code],Customer_Name as [Customer Name] from TSPL_CUSTOMER_MASTER where 2=2 "
        Select Case NavType
            Case NavigatorType.Current
                qst += " and TSPL_CUSTOMER_MASTER .Cust_Code in ('" + txtcustomer.Value + "')"
            Case NavigatorType.Next
                qst += " and TSPL_CUSTOMER_MASTER .Cust_Code in (select min(Cust_Code ) from TSPL_CUSTOMER_MASTER where Cust_Code  >'" + txtcustomer.Value + "')"
            Case NavigatorType.First
                qst += " and TSPL_CUSTOMER_MASTER .Cust_Code in (select MIN(Cust_Code ) from TSPL_CUSTOMER_MASTER)"

            Case NavigatorType.Last
                qst += " and TSPL_CUSTOMER_MASTER .Cust_Code in (select Max(Cust_Code ) from TSPL_CUSTOMER_MASTER)"
            Case NavigatorType.Previous
                qst += " and TSPL_CUSTOMER_MASTER .Cust_Code in (select Max(Cust_Code ) from TSPL_CUSTOMER_MASTER where Cust_Code  <'" + txtcustomer.Value + "')"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qst)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            txtcustomer.Value = clsCommon.myCstr(dt.Rows(0)("Customer Code"))
            txtcustomerdesc.Text = clsCommon.myCstr(dt.Rows(0)("Customer Name"))
            resetshipdetail()
        End If
        LoadData()
    End Sub

    Private Sub MasterTemplate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MasterTemplate.Click

    End Sub
    Sub resetshipdetail()
        txtTinNo.Text = ""
        txtCSTNo.Text = ""
        txtGSTstateCode.Text = ""
        txtGstPanNo.Text = ""
        txtGstEntityNo.Text = ""
        txtGstDefaultValue.Text = "Z"
        txtGstLastNo.Text = ""
        txtGstInNo.Text = ""
        rbtnDomestic.IsChecked = False
        rbtnForeign.IsChecked = False
        chkGstRegistered.Checked = False
        chkComposition.Checked = False
        txtpan.Text = ""
        txtContactName.Text = ""
        txtContPhone.Text = "(+__)__________"
        txtContactFax.Text = ""
        txtContactWeb.Text = ""
        txtContactEmail.Text = ""
        txtDriverMobileNo.Text = "(+__)__________"
        txtDriverFinder.Value = ""
        txtVehicleNo.Text = ""

        fndCustomer.Text = txtcustomer.Value

        txtCustomerName.Text = txtcustomerdesc.Text
        txtShipToLocation.Text = ""
        txtShipToLocationDesc.Text = ""
        txtAddress1.Text = ""
        txtAddress22.Text = ""
        txtAddress3.Text = ""
        txtAddress4.Text = ""
        txtCity.Value = ""
        TxtCountryName.Text = ""
        txtStateName.Text = ""
        txtCityName.Text = ""
        txtState.Value = ""
        txtPostalCode.Text = ""
        txtCountry.Value = ""
        txtTelephone.Text = ""
        txtEmail.Text = ""
        fndLocation.Value = ""
        lblLocationDesc.Text = ""
        btnDelete.Enabled = False
        btnAdd.Text = "Save"
        txtcustomer.MyReadOnly = False
        If MyBase.customFieldTabProperty = ElementVisibility.Visible Then

            UcCustomFields1.BlankAllControls()
        End If
        LoadLocation(txtShipToLocation.Text, txtcustomer.Value)
    End Sub
    Private Sub MasterTemplate_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles MasterTemplate.DoubleClick
        Dim rno As Integer = MasterTemplate.CurrentRow.Index

        If rno >= 0 Then
            FunDoubleClick(rno)
            'fndCustomer.Text = txtcustomer.Value
            'txtCustomerName.Text = txtcustomerdesc.Text
            'txtShipToLocation.Text = MasterTemplate.CurrentRow.Cells(0).Value
            'txtShipToLocationDesc.Text = MasterTemplate.CurrentRow.Cells(1).Value
            'txtAddress1.Text = MasterTemplate.CurrentRow.Cells(2).Value
            'txtAddress22.Text = MasterTemplate.CurrentRow.Cells(3).Value
            'txtAddress3.Text = MasterTemplate.CurrentRow.Cells(4).Value
            'txtAddress4.Text = MasterTemplate.CurrentRow.Cells(5).Value
            'txtCity.Value = MasterTemplate.CurrentRow.Cells(6).Value
            'txtState.Value = MasterTemplate.CurrentRow.Cells(7).Value
            'txtCountry.Value = MasterTemplate.CurrentRow.Cells(9).Value
            'txtCityName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT [City_Name] FROM [TSPL_CITY_MASTER] where [City_Code]='" + txtCity.Value + "'"))
            'txtStateName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  STATE_NAME  from TSPL_State_MASTER where STATE_CODE ='" + txtState.Value + "'"))
            'TxtCountryName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select country_name from tspl_country_master where country_code='" + txtCountry.Value + "'"))

            'txtPostalCode.Text = MasterTemplate.CurrentRow.Cells(8).Value

            'txtTelephone.Text = MasterTemplate.CurrentRow.Cells(10).Value
            'txtEmail.Text = clsCommon.myCstr(MasterTemplate.CurrentRow.Cells(11).Value)
            'txtTinNo.Text = clsCommon.myCstr(MasterTemplate.CurrentRow.Cells(12).Value)
            'txtCSTNo.Text = clsCommon.myCstr(MasterTemplate.CurrentRow.Cells(13).Value)
            'fndLocation.Value = clsCommon.myCstr(MasterTemplate.CurrentRow.Cells(14).Value)
            'lblLocationDesc.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select location_desc from TSPL_LOCATION_MASTER where Location_Code='" & clsCommon.myCstr(fndLocation.Value) & "'"))
            'txtContactName.Text = clsCommon.myCstr(MasterTemplate.CurrentRow.Cells(15).Value)
            'txtContactFax.Text = clsCommon.myCstr(MasterTemplate.CurrentRow.Cells(16).Value)
            'txtContactEmail.Text = clsCommon.myCstr(MasterTemplate.CurrentRow.Cells(17).Value)
            'txtContPhone.Text = clsCommon.myCstr(MasterTemplate.CurrentRow.Cells(18).Value)
            'txtContactWeb.Text = clsCommon.myCstr(MasterTemplate.CurrentRow.Cells(19).Value)
            'txtVehicleNo.Text = clsCommon.myCstr(MasterTemplate.CurrentRow.Cells(20).Value)
            'txtDriverFinder.Value = clsCommon.myCstr(MasterTemplate.CurrentRow.Cells(21).Value)
            'txtDriverMobileNo.Text = clsCommon.myCstr(MasterTemplate.CurrentRow.Cells(22).Value)

            'txtpan.Text = clsCommon.myCstr(MasterTemplate.CurrentRow.Cells(23).Value)
            'ChkOther.Checked = IIf(clsCommon.myCstr(MasterTemplate.CurrentRow.Cells(24).Value) = "1", True, False)

            ''========priti(GST Detail (13/03/2018)=====================
            'txtGstInNo.Text = clsCommon.myCstr(MasterTemplate.CurrentRow.Cells(25).Value)
            'txtGstEntityNo.Text = clsCommon.myCstr(MasterTemplate.CurrentRow.Cells(26).Value)
            'txtGstDefaultValue.Text = clsCommon.myCstr(MasterTemplate.CurrentRow.Cells(27).Value)
            'txtGstLastNo.Text = clsCommon.myCstr(MasterTemplate.CurrentRow.Cells(28).Value)
            'If clsCommon.myCstr(MasterTemplate.CurrentRow.Cells(29).Value) = "D" Then
            '    rbtnDomestic.IsChecked = True
            '    rbtnForeign.IsChecked = False
            'End If
            'If clsCommon.myCstr(MasterTemplate.CurrentRow.Cells(29).Value) = "F" Then
            '    rbtnForeign.IsChecked = True
            '    rbtnDomestic.IsChecked = False
            'End If
            'If clsCommon.myCstr(MasterTemplate.CurrentRow.Cells(30).Value) = "1" Then
            '    chkGstRegistered.Checked = True
            'Else
            '    chkGstRegistered.Checked = False
            'End If
            'If clsCommon.myCstr(MasterTemplate.CurrentRow.Cells(31).Value) = "1" Then
            '    chkComposition.Checked = True
            'Else
            '    chkComposition.Checked = False
            'End If
            ''==========================================================================

            'txtGSTstateCode.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  GST_STATE_Code  from TSPL_State_MASTER where STATE_CODE ='" + txtState.Value + "'"))
            'txtGstPanNo.Text = txtpan.Text

            ''For Custom Fields
            'If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
            '    UcCustomFields1.LoadData(txtShipToLocation.Text)
            'End If
            'LoadLocation(txtShipToLocation.Text, txtcustomer.Value)
            ''End of For Custom Fields
            'btnDelete.Enabled = True
            'btnAdd.Text = "Update"
        End If
    End Sub

    Private Sub FunDoubleClick(ByVal RowIndex As Integer)
        Try
            fndCustomer.Text = txtcustomer.Value
            txtCustomerName.Text = txtcustomerdesc.Text
            txtShipToLocation.Text = MasterTemplate.Rows(RowIndex).Cells(0).Value
            txtShipToLocationDesc.Text = MasterTemplate.Rows(RowIndex).Cells(1).Value
            txtAddress1.Text = MasterTemplate.Rows(RowIndex).Cells(2).Value
            txtAddress22.Text = MasterTemplate.Rows(RowIndex).Cells(3).Value
            txtAddress3.Text = MasterTemplate.Rows(RowIndex).Cells(4).Value
            txtAddress4.Text = MasterTemplate.Rows(RowIndex).Cells(5).Value
            txtCity.Value = MasterTemplate.Rows(RowIndex).Cells(6).Value
            txtState.Value = MasterTemplate.Rows(RowIndex).Cells(7).Value
            txtCountry.Value = MasterTemplate.Rows(RowIndex).Cells(9).Value
            txtCityName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT [City_Name] FROM [TSPL_CITY_MASTER] where [City_Code]='" + txtCity.Value + "'"))
            txtStateName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  STATE_NAME  from TSPL_State_MASTER where STATE_CODE ='" + txtState.Value + "'"))
            TxtCountryName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select country_name from tspl_country_master where country_code='" + txtCountry.Value + "'"))

            txtPostalCode.Text = MasterTemplate.Rows(RowIndex).Cells(8).Value

            txtTelephone.Text = MasterTemplate.Rows(RowIndex).Cells(10).Value
            txtEmail.Text = clsCommon.myCstr(MasterTemplate.Rows(RowIndex).Cells(11).Value)
            txtTinNo.Text = clsCommon.myCstr(MasterTemplate.Rows(RowIndex).Cells(12).Value)
            txtCSTNo.Text = clsCommon.myCstr(MasterTemplate.Rows(RowIndex).Cells(13).Value)
            fndLocation.Value = clsCommon.myCstr(MasterTemplate.Rows(RowIndex).Cells(14).Value)
            lblLocationDesc.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select location_desc from TSPL_LOCATION_MASTER where Location_Code='" & clsCommon.myCstr(fndLocation.Value) & "'"))
            txtContactName.Text = clsCommon.myCstr(MasterTemplate.Rows(RowIndex).Cells(15).Value)
            txtContactFax.Text = clsCommon.myCstr(MasterTemplate.Rows(RowIndex).Cells(16).Value)
            txtContactEmail.Text = clsCommon.myCstr(MasterTemplate.Rows(RowIndex).Cells(17).Value)
            txtContPhone.Text = clsCommon.myCstr(MasterTemplate.Rows(RowIndex).Cells(18).Value)
            txtContactWeb.Text = clsCommon.myCstr(MasterTemplate.Rows(RowIndex).Cells(19).Value)
            txtVehicleNo.Text = clsCommon.myCstr(MasterTemplate.Rows(RowIndex).Cells(20).Value)
            txtDriverFinder.Value = clsCommon.myCstr(MasterTemplate.Rows(RowIndex).Cells(21).Value)
            txtDriverMobileNo.Text = clsCommon.myCstr(MasterTemplate.Rows(RowIndex).Cells(22).Value)

            txtpan.Text = clsCommon.myCstr(MasterTemplate.Rows(RowIndex).Cells(23).Value)
            ChkOther.Checked = IIf(clsCommon.myCstr(MasterTemplate.Rows(RowIndex).Cells(24).Value) = "1", True, False)

            ''========priti(GST Detail (13/03/2018)=====================
            txtGstInNo.Text = clsCommon.myCstr(MasterTemplate.Rows(RowIndex).Cells(25).Value)
            txtGstEntityNo.Text = clsCommon.myCstr(MasterTemplate.Rows(RowIndex).Cells(26).Value)
            txtGstDefaultValue.Text = clsCommon.myCstr(MasterTemplate.Rows(RowIndex).Cells(27).Value)
            txtGstLastNo.Text = clsCommon.myCstr(MasterTemplate.Rows(RowIndex).Cells(28).Value)
            If clsCommon.myCstr(MasterTemplate.Rows(RowIndex).Cells(29).Value) = "D" Then
                rbtnDomestic.IsChecked = True
                rbtnForeign.IsChecked = False
            End If
            If clsCommon.myCstr(MasterTemplate.Rows(RowIndex).Cells(29).Value) = "F" Then
                rbtnForeign.IsChecked = True
                rbtnDomestic.IsChecked = False
            End If
            If clsCommon.myCstr(MasterTemplate.Rows(RowIndex).Cells(30).Value) = "1" Then
                chkGstRegistered.Checked = True
            Else
                chkGstRegistered.Checked = False
            End If
            If clsCommon.myCstr(MasterTemplate.Rows(RowIndex).Cells(31).Value) = "1" Then
                chkComposition.Checked = True
            Else
                chkComposition.Checked = False
            End If
            '==========================================================================

            txtGSTstateCode.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  GST_STATE_Code  from TSPL_State_MASTER where STATE_CODE ='" + txtState.Value + "'"))
            txtGstPanNo.Text = txtpan.Text

            ''For Custom Fields
            If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
                UcCustomFields1.LoadData(txtShipToLocation.Text)
            End If
            LoadLocation(txtShipToLocation.Text, txtcustomer.Value)
            ''End of For Custom Fields
            btnDelete.Enabled = True
            btnAdd.Text = "Update"
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub fndLocation__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndLocation._MYValidating
        fndLocation.Value = clsLocation.getFinder("Location_Type <>'Logical'", fndLocation.Value, isButtonClicked)
        lblLocationDesc.Text = clsDBFuncationality.getSingleValue("select location_desc from TSPL_LOCATION_MASTER where Location_Code='" & clsCommon.myCstr(fndLocation.Value) & "'")

    End Sub
    Sub LoadLocation(ByVal Ship_To_Code As String, ByVal Cust_Code As String)
        Dim qry As String = " select Location_Code,Location_Desc from TSPL_LOCATION_MASTER "
        'Dim qry As String = " select Segment_code as Code, Description  from TSPL_GL_SEGMENT_CODE where Seg_No='7' "
        cbgLocation.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgLocation.ValueMember = "Location_Code"
        cbgLocation.DisplayMember = "Location_Desc"
        'If Ship_To_Code = "" Then
        '    Exit Sub
        'End If
        If clsCommon.myLen(Ship_To_Code) <= 0 OrElse clsCommon.myLen(Cust_Code) <= 0 Then
            Exit Sub
        End If
        qry = " select Loc_Code from TSPL_SHIP_TO_LOCATION_LOCATIONS where Ship_To_Code='" + Ship_To_Code + "' AND Cust_Code='" + Cust_Code + "'"
        'Dim qry As String = " select Segment_code as Code, Description  from TSPL_GL_SEGMENT_CODE where Seg_No='7' "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        Dim LocationArr As New ArrayList

        For i As Integer = 0 To dt.Rows.Count - 1
            LocationArr.Add(dt.Rows(i)("Loc_Code").ToString())
        Next
        cbgLocation.CheckedValue = LocationArr

    End Sub
    Public Sub InsertLocations(ByVal Ship_To_Code As String, ByVal location As ArrayList, ByVal Cust_Code As String)
        Dim qry As String = "delete from TSPL_SHIP_TO_LOCATION_LOCATIONS where Ship_To_Code='" + Ship_To_Code + "' and Cust_Code='" + Cust_Code + "'"
        clsDBFuncationality.ExecuteNonQuery(qry)
        For Each objLoc As String In location
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Ship_To_Code", Ship_To_Code)
            clsCommon.AddColumnsForChange(coll, "Cust_Code", Cust_Code)
            clsCommon.AddColumnsForChange(coll, "Loc_Code", objLoc)
            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SHIP_TO_LOCATION_LOCATIONS", OMInsertOrUpdate.Insert, "")
        Next
    End Sub
    Private Sub fndCountry__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtCountry._MYValidating
        Try
            Dim qry As String = "select country_code as Code,country_name as Country from tspl_country_master"
            txtCountry.Value = clsCommon.ShowSelectForm("CNTFND", qry, "Code", "", txtCountry.Value, "Code", isButtonClicked)

            If clsCommon.myLen(txtCountry.Value) > 0 Then
                TxtCountryName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select country_name from tspl_country_master where country_code='" + txtCountry.Value + "'"))
            Else
                TxtCountryName.Text = ""
                txtState.Value = ""
                txtStateName.Text = ""
                'txtCity.Text = ""
                txtCity.Value = ""
                txtCityName.Text = ""
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub fndstate__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtState._MYValidating
        'Dim qry As String = "select state_code as [StateCode],state_name as [State Name] from TSPL_TDS_STATE_MASTER"
        'fndstate.Value = clsCommon.ShowSelectForm("FNDSTATE_CODE", qry, "StateCode", "", fndstate.Value, "", isButtonClicked)
        txtState.Value = clsStateMaster.getFinder("", txtState.Value, isButtonClicked)
        txtStateName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  STATE_NAME  from TSPL_State_MASTER where STATE_CODE ='" + txtState.Value + "'"))
        If GstApplicable Then
            txtGSTstateCode.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  GST_STATE_Code  from TSPL_State_MASTER where STATE_CODE ='" + txtState.Value + "'"))
        End If
    End Sub
    Private Sub fndCity__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtCity._MYValidating

        'Dim qry As String = "SELECT [City_Code] as [CityCode],[City_Name] as [City Name]FROM [TSPL_CITY_MASTER]"
        'fndCity.Value = clsCommon.ShowSelectForm("FNDCITY_CODE", qry, "CityCode", "", fndCity.Value, "", isButtonClicked)
        txtCity.Value = clsCityMaster.getFinder("", txtCity.Value, isButtonClicked)
        txtCityName.Text = clsDBFuncationality.getSingleValue("SELECT [City_Name] FROM [TSPL_CITY_MASTER] where [City_Code]='" + txtCity.Value + "' ")

    End Sub
    Private Sub txtDriverFinder__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtDriverFinder._MYValidating
        Dim Whrcls As String = Nothing
        Try
            ' Changed By : Prabhakar Ticket Ref : BM00000010132
            Dim Qry As String = "select User_Code as 'Code',User_Name as 'Name' from tspl_USER_MASTER"
            Whrcls = " Login_Type is null and EmployeeCode is not null "
            txtDriverFinder.Value = clsCustomerMaster.getDriverFinder(Whrcls, txtDriverFinder.Value, isButtonClicked)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtShipToLocation_Leave(sender As Object, e As EventArgs) Handles txtShipToLocation.Leave
        'Dim qry As String = ""
        'qry = " select count(1) from TSPL_SHIP_TO_LOCATION where Ship_To_Code='" + txtShipToLocation.Text + "'"
        'If clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry)) > 0 Then
        '    qry = "select Ship_To_Type_Code from TSPL_SHIP_TO_LOCATION where Ship_To_Code='" + txtShipToLocation.Text + "'"
        '    txtcustomer.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
        '    LoadData()
        '    LoadLocation(txtShipToLocation.Text)
        '    btnDelete.Enabled = True
        '    btnAdd.Text = "Update"
        'End If

        Dim qry As String = ""
        qry = " select count(1) from TSPL_SHIP_TO_LOCATION where Ship_To_Code='" + txtShipToLocation.Text + "' and Ship_To_Type_Code='" + txtcustomer.Value + "'"
        If clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry)) > 0 Then
            Dim rno As Integer = -1
            If MasterTemplate.Rows.Count > 0 Then
                For ii As Integer = 0 To MasterTemplate.Rows.Count - 1
                    If clsCommon.CompairString(MasterTemplate.Rows(ii).Cells(0).Value, txtShipToLocation.Text) = CompairStringResult.Equal Then
                        rno = ii
                    End If
                Next
            End If
            If rno >= 0 Then
                FunDoubleClick(rno)
            End If
            LoadLocation(txtShipToLocation.Text, txtcustomer.Value)
            btnDelete.Enabled = True
            btnAdd.Text = "Update"
        Else
            Dim TempShipToLocation As String = clsCommon.myCstr(txtShipToLocation.Text)
            funReset()
            If clsCommon.myLen(TempShipToLocation) > 0 Then
                txtShipToLocation.Text = TempShipToLocation
            End If
        End If

    End Sub
End Class
