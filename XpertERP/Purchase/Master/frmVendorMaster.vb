Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Data.SqlClient
Imports Telerik.WinControls.UI
Imports Telerik.WinControls
Imports Telerik.WinControls.Data
Imports System.Text.RegularExpressions

Imports common
'created by --> Vipin
'createddate --> 10/06/2011
'modifiedby --> Vipin
'Modified date -->14/06/2011
'Tables Used --> tspl_Vendor_master
'--Updation By--[Pankaj Kumar Chaudhary]--Against Ticket no---[BM00000000747]
'--------------BM00000003305-----------------------------------24/07/2014
'--BM00000003542(attachment tab),BM00000009844
'---changes by shivani,,,ticket no.[BM00000003433]
'=======================BM00000003721----------BM00000003474----------
''richa agarwal ticket no. BM00000005874,BM00000007109
'' work done as ticket no. MIL/13/02/19-000041
'Ticket ERO/19/11/19-001119,Sanjay map Employee
Public Class frmVendorMaster
    Inherits FrmMainTranScreen
    Dim userCode, companyCode As String
    Dim str As String
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Const CatcolCode As String = "CatcolCode"
    Const CatcolCodeDesc As String = "CatcolCodeDesc"
    Const CatcolValue As String = "CatcolValue"
    Const CatcolValueDesc As String = "CatcolValueDesc"
    Dim isInsideLoadData As Boolean = False
    Dim isCellValueChangedOpen As Boolean = False
    Public is_For_Chilling_Vendor As Boolean = False
    Dim checkPan As New System.Text.RegularExpressions.Regex("^([a-zA-Z]){5}([0-9]){4}([a-zA-Z]){1}?$")
    Dim IndustryType As String = ""
    Dim vendorDict As Dictionary(Of String, String) = Nothing
    Dim AllowGSTApplicable As Boolean = False
    Dim isAllowPanNoValidation As Boolean = False
    Dim isLoadCopy As Boolean = False
    Dim EnableBankFromMaster As Boolean = False
    Dim SettEnableTDSforServiceVendorSeparately As Boolean = False
    Dim DoNotCheckAnyValidationOnVendorInactive As Boolean = False
    Dim OneTimeCheck As Boolean = False

    Public Sub New(ByVal user As String, ByVal company As String)
        InitializeComponent()
        userCode = user
        companyCode = company
    End Sub


#Region "Page Load"
    Private Sub frmVendorMaster_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        EnableBankFromMaster = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.EnableBankFromMaster, clsFixedParameterCode.EnableBankFromMaster, Nothing)) = 1, True, False)
        SettEnableTDSforServiceVendorSeparately = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.EnableTDSforServiceVendorSeparately, clsFixedParameterCode.EnableTDSforServiceVendorSeparately, Nothing)) = 1)
        DoNotCheckAnyValidationOnVendorInactive = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.DoNotCheckAnyValidationOnVendorInactive, clsFixedParameterCode.DoNotCheckAnyValidationOnVendorInactive, Nothing)) = 1, True, False)
        GrpTDSService.Visible = SettEnableTDSforServiceVendorSeparately
        SetLength()
        pageCus.SelectedPage = RadPageViewPage1
        SetUserMgmtNew()

        '' Anubhooti 1-Aug-2014
        LoadAccountType()
        LoadVendorType()
        LoadVendorTypeCHA()
        loadindustrytype()
        funreset()
        txtChequeInFavour.Enabled = True
        txtCHA_Amount.Enabled = True
        txtCHA_Type.Enabled = True
        ''
        'globalFunc.mandatoryText(fndvendorNo.txtValue, txtvendorname, fndgroupcode.txtValue, fndTrmsCode.txtValue, fndAccntSet.txtValue, fndbankcode.txtValue, fndTxGrp.txtValue, txtTinNo)
        ToolTipvendor.SetToolTip(btnnew, "New")
        '  fndvendorNo.txtValue.MaxLength = 12
        fndvendorNo_text_changed()
        fndgroupcode_text_Changed()
        fndcity_text_Changed()
        ' AddHandler fndvendorNo.ValueChanged, AddressOf fndvendorNo_text_changed
        '     AddHandler fndgroupcode.ValueChanged, AddressOf fndgroupcode_text_Changed
        '  AddHandler fndCity.ValueChanged, AddressOf fndcity_text_Changed
        'AddHandler fndTrmsCode.txtValue.TextChanged, AddressOf fndTrmsCode_text_Changed
        'AddHandler fndAccntSet.txtValue.TextChanged, AddressOf fndAccntSet_text_Changed
        'AddHandler fndPayCode1.txtValue.TextChanged, AddressOf fndPayCode_text_Changed
        'AddHandler fndvendortype1.txtValue.TextChanged, AddressOf fndvendortype_text_Changed
        'AddHandler fndbankcode.txtValue.TextChanged, AddressOf fndbankcode_text_Changed
        'AddHandler fndTxGrp.txtValue.TextChanged, AddressOf fndTxGrp_text_Changed

        'AddHandler fndvendorNo.txtValue.KeyPress, AddressOf fndvendorNo_key_press
        '  AddHandler fndgroupcode.txtValue.KeyPress, AddressOf fndgroupcode_key_press
        'AddHandler fndCity.txtValue.KeyPress, AddressOf fndCity_key_press
        'AddHandler fndTrmsCode.txtValue.KeyPress, AddressOf fndTermsCode_key_press
        'AddHandler fndAccntSet.txtValue.KeyPress, AddressOf fndAccntSet_key_press
        'AddHandler fndPayCode.txtValue.KeyPress, AddressOf fndPayCode_key_press
        'AddHandler fndvendortype.txtValue.KeyPress, AddressOf fndvendortype_key_press
        'AddHandler fndbankcode.txtValue.KeyPress, AddressOf fndbankcode_key_press
        'AddHandler fndTxGrp.txtValue.KeyPress, AddressOf fndTxGrp_key_press
        fndgroupcode_leave()
        fndCity_leave()
        '   AddHandler fndvendorNo.txtValue.Leave, AddressOf fndvendorNo_leave
        '  AddHandler fndgroupcode.txtValue.Leave, AddressOf fndgroupcode_leave
        ' AddHandler fndCity.txtValue.Leave, AddressOf fndCity_leave
        'AddHandler fndTrmsCode.txtValue.Leave, AddressOf fndTrmsCode_leave
        'AddHandler fndAccntSet.txtValue.Leave, AddressOf fndAccntSet_leave
        'AddHandler fndPayCode1.txtValue.Leave, AddressOf fndPayCode_leave
        'AddHandler fndvendortype1.txtValue.Leave, AddressOf fndvendortype_leave
        'AddHandler fndbankcode.txtValue.Leave, AddressOf fndbankcode_leave
        'AddHandler fndTxGrp.txtValue.Leave, AddressOf fndTxGrp_leave
        '  fndvendorNo.txtValue.CharacterCasing = CharacterCasing.Upper
        'fndgroupcode.txtValue.CharacterCasing = CharacterCasing.Upper
        '  fndCity.txtValue.CharacterCasing = CharacterCasing.Upper
        'fndTrmsCode.txtValue.CharacterCasing = CharacterCasing.Upper
        'fndAccntSet.txtValue.CharacterCasing = CharacterCasing.Upper
        'fndPayCode1.txtValue.CharacterCasing = CharacterCasing.Upper
        'fndvendortype1.txtValue.CharacterCasing = CharacterCasing.Upper
        'fndbankcode.txtValue.CharacterCasing = CharacterCasing.Upper
        'fndTxGrp.txtValue.CharacterCasing = CharacterCasing.Upper

        '  fndvendorNo.txtValue.MaxLength = 12
        ' fndgroupcode.txtValue.MaxLength = 12
        'fndCity.txtValue.MaxLength = 12
        'fndTrmsCode.txtValue.MaxLength = 12
        'fndAccntSet.txtValue.MaxLength = 12
        'fndvendortype1.txtValue.MaxLength = 12
        'fndbankcode.txtValue.MaxLength = 12
        'fndPayCode1.txtValue.MaxLength = 12
        'fndTxGrp.txtValue.MaxLength = 12

        chkInActive.Checked = False
        dtClosing.Enabled = False
        btndelete.Enabled = False
        btnsave.Enabled = True
        chkVendorInvoiceNo.Checked = False
        chkOEM.Checked = False
        'If userCode <> "ADMIN" Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update Trasnaction")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnclear, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnnew, "Press Alt+N Adding New Trasnaction")


        '' check for multi currency
        If objCommonVar.IsDemoERP = True Then
            If CheckMultiCurrency() = True Then
                lblBaseCurrency.Visible = True
                Me.fndVendorCurrency.Visible = True
            Else
                lblBaseCurrency.Visible = False
                Me.fndVendorCurrency.Visible = False

            End If
        Else
            lblBaseCurrency.Visible = False
            fndVendorCurrency.Visible = False
        End If

        txtvndrtype.Text = "ALL"

        RadPageViewPage3.Item.Visibility = ElementVisibility.Collapsed
        If MDI.IsVendor_NLevel = "YES" Then
            RadPageViewPage3.Item.Visibility = ElementVisibility.Visible
        End If
        ''For Custom Fields
        pageCus.Pages("pvpCustomFields").Item.Visibility = MyBase.customFieldTabProperty
        If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
            UcCustomFields1.Report_ID = MyBase.Form_ID
            UcCustomFields1.LoadCustomControls()
        End If
        ''End of For Custom Fields

        '' Anubhooti 01-Sep-2014
        VendorName()
        ''
        '' Anubhooti 15-Jan-2014 (Auto Industry)
        IndustryType = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.INDUSTRYTYPE, clsFixedParameterCode.INDUSTRYTYPE, Nothing))
        If clsCommon.CompairString(IndustryType, "A") = CompairStringResult.Equal Then
            GrpBoxTC.Visible = True
        Else
            GrpBoxTC.Visible = False
        End If
        AllowGSTApplicable = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.GSTApplicable, clsFixedParameterCode.GSTApplicable, Nothing)) = 1, True, False)
        If AllowGSTApplicable = True Then
            RadPageViewPage7.Enabled = True
        Else
            RadPageViewPage7.Enabled = False
        End If
        isAllowPanNoValidation = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowPanNoValidation, clsFixedParameterCode.AllowPanNoValidation, Nothing)) = 1, True, False)

        ' For dril down open 
        If clsCommon.myLen(Me.Tag) > 0 Then
            fndvendorNo.Value = clsCommon.myCstr(Me.Tag)
            fndvendorNo_text_changed()
        End If
        If clsCommon.CompairString(objCommonVar.CurrComp_Code1, "RCDFCF") = CompairStringResult.Equal Then
            InActiveCF.Visible = True
            InActiveCF.Checked = False
        Else
            InActiveCF.Visible = False
        End If
    End Sub
    Function CheckMultiCurrency() As Boolean
        Dim strq As String
        strq = "select * from tspl_module_currency_mapping where comp_code='" + objCommonVar.CurrentCompanyCode + "' and module_code='" & Me.Module_Code & "'"
        Dim dt As DataTable
        dt = clsDBFuncationality.GetDataTable(strq)
        If dt.Rows.Count > 0 Then
            If dt.Rows(0).Item("Apply") = True Then
                Return True
            Else
                Return False
            End If
        Else
            Return False
        End If
    End Function
    Public Sub SetLength()
        fndvendorNo.MyMaxLength = 12
        txtvendorname.MaxLength = 200
        txtAdd1.MaxLength = 50
        txtAdd2.MaxLength = 50
        txtAdd3.MaxLength = 50
        txtgroupdes.MaxLength = 50
        'txtPhone1.MaxLength = 20
        'txtPhone2.MaxLength = 20
        txtfax.MaxLength = 20
        txtEmail.MaxLength = 50
        txtWeb.MaxLength = 50
        txtContactName.MaxLength = 50
        'txtContPhone.MaxLength = 20
        txtContactFax.MaxLength = 20
        txtContactWeb.MaxLength = 50
        txtContactEmail.MaxLength = 50
        txttermcodedes.MaxLength = 50
        txtvendortypedes.MaxLength = 50
        txtpaymentcodedes.MaxLength = 50
        TxtBankName.MaxLength = 50
        txtTxGrp.MaxLength = 50
        txtStaxNo.MaxLength = 50
        txtTinNo.MaxLength = 50
        txtLstNo.MaxLength = 50
        txtRemarks1.MaxLength = 200
        txtRemarks2.MaxLength = 200
        txtAddInfo1.MaxLength = 50
        txtAddInfo2.MaxLength = 50
        txtAddInfo3.MaxLength = 50
        txtCredit.MaxLength = 9
        txtcollect.MaxLength = 30
        txtpan.MaxLength = 30
        txtrange.MaxLength = 30
        txtecc.MaxLength = 30
        txtcst.MaxLength = 30

        TxtBankName.MaxLength = 50
        'TxtBankBranch.MaxLength = 150
        TxtAccNo.MaxLength = 50

    End Sub
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt("VENDOR-M")
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
            Me.Close()
            Exit Sub
        End If
        btnsave.Visible = MyBase.isModifyFlag
        '--------richa Ticket no. BM00000003014 03/07/2014 to enable/disable import/export option acc. to user mgmt setting -----------
        If btnsave.Visible = True Then
            MenuImport.Enabled = True
            MenuExport.Enabled = True
        Else
            MenuImport.Enabled = False
            MenuExport.Enabled = False
        End If
        '--------------------------------------------------
        'btnPost.Visible = MyBase.isPostFlag
        btndelete.Visible = MyBase.isDeleteFlag
    End Sub
#End Region

#Region "Function"

    'stuti  Ticket No-BM00000009451
    Private Sub loadindustrytype()
        Try
            Dim strquery As String = "select 'MME' as Code,'MME' as Name union all select 'SSI' as Code,'Small Scale Industry' as Name union all select 'Others' as Code,'Others' as Name"
            Dim dr As DataTable
            dr = clsDBFuncationality.GetDataTable(strquery)
            If dr.Rows IsNot Nothing AndAlso dr.Rows.Count > 0 Then
                ddl_industrytype.DataSource = dr
                ddl_industrytype.DisplayMember = "Name"
                ddl_industrytype.ValueMember = "Code"
                ddl_industrytype.SelectedValue = "Others"
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub

    'It will fill the  controls if value exist in database according to fndgroupcode
    Public Sub funfillfndGroupCode()
        Try

            Dim strquery As String = "select group_desc,tax_Group_Code,Acct_Set_code,Terms_COde,Bank_Code ,payment_code from Tspl_vendor_group where ven_group_code='" + fndgroupcode.Value + "'"

            Dim dr As DataTable
            ' Dim strvalue As String
            dr = clsDBFuncationality.GetDataTable(strquery)
            If dr.Rows IsNot Nothing AndAlso dr.Rows.Count > 0 Then
                txtgroupdes.Text = dr.Rows(0)("group_desc").ToString()
                fndTxGrp.Value = dr.Rows(0)("tax_Group_Code").ToString()
                fndAccntSet.Value = dr.Rows(0)("Acct_Set_code").ToString()
                fndTrmsCode.Value = dr.Rows(0)("Terms_COde").ToString()
                fndbankcode.Value = dr.Rows(0)("Bank_Code").ToString()
                fndPayCode.Value = dr.Rows(0)("payment_code").ToString()
                If clsCommon.myLen(clsCommon.myCstr(dr.Rows(0)("Bank_Code"))) > 0 Then
                    TxtBankName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select ISNULL(Bank_Name,'') AS [Bank_Name] From TSPL_VENDOR_BANK_MASTER Where Bank_Code ='" & clsCommon.myCstr(dr.Rows(0)("Bank_Code")) & "'"))
                Else
                    TxtBankName.Text = ""
                End If

            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub
    'It will fill the  controls if value exist in database according to fndCity
    Public Sub funfilfndcity()
        Try

            Dim strquery As String = "select City_Code ,city_Name from TSPL_City_MASTER where City_code = '" + fndCity.Value + "'"
            Dim dr As DataTable
            ' Dim strvalue As String
            dr = clsDBFuncationality.GetDataTable(strquery)
            If dr.Rows IsNot Nothing AndAlso dr.Rows.Count > 0 Then
                txtCity.Text = dr.Rows(0)("city_Name").ToString()
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub

    'It will fill the  controls if value exist in database according to fndTrmsCode
    Public Sub funfilfndterm()
        Try

            Dim strquery As String = "SELECT [Terms_Code] as [Terms Code],[Terms_Desc] as [Description] FROM [TSPL_TERMS_MASTER] where terms_code='" + fndTrmsCode.Value + "'"
            Dim dr As DataTable
            ' Dim strvalue As String
            dr = clsDBFuncationality.GetDataTable(strquery)
            If dr.Rows IsNot Nothing AndAlso dr.Rows.Count > 0 Then
                txttermcodedes.Text = dr.Rows(0)("Description").ToString()
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub

    'It will fill the  controls if value exist in database according to fndAccntSet
    Public Sub funfillfndACCSet()
        Try

            Dim strquery As String = "SELECT  [Acct_Set_Code] as [Account Set Code],[Acct_Set_Desc] as [Description] FROM [TSPL_VENDOR_ACCOUNT_SET] where Acct_Set_Code ='" + fndAccntSet.Value + "'"
            Dim dr As DataTable
            'Dim strvalue As String
            dr = clsDBFuncationality.GetDataTable(strquery)
            If dr.Rows IsNot Nothing AndAlso dr.Rows.Count > 0 Then

                txtaccsetdes.Text = dr.Rows(0)("Description").ToString()
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub

    'It will fill the  controls if value exist in database according to fndPayCode
    Public Sub funfillfndPay()
        Try

            Dim strquery As String = "  SELECT [Payment_Code] as [Payment Code],[payment_Desc] as [Description] FROM [TSPL_PAYMENT_CODE] where payment_code='" + fndPayCode.Value + "'"
            Dim dr As DataTable
            'Dim strvalue As String
            dr = clsDBFuncationality.GetDataTable(strquery)
            If dr.Rows IsNot Nothing AndAlso dr.Rows.Count > 0 Then

                txtpaymentcodedes.Text = dr.Rows(0)("Description").ToString()
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub

    'It will fill the  controls if value exist in database according to fndvendortype
    Public Sub funfillfndventype()
        Try

            Dim strquery As String = "SELECT  [ven_Type_Code] as [Vendor Type Code],[ven_Type_Desc] as [Description]FROM [TSPL_VENDOR_TYPE_MASTER] where ven_type_code='" + fndvendortype.Value + "'"
            Dim dr As DataTable
            '  Dim strvalue As String
            dr = clsDBFuncationality.GetDataTable(strquery)
            If dr.Rows IsNot Nothing AndAlso dr.Rows.Count > 0 Then
                txtvendortypedes.Text = dr.Rows(0)("Description").ToString()
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub

    'It will fill the  controls if value exist in database according to fndbankcode
    Public Sub funfillbank()
        Try

            Dim strquery As String = "select bank_code As [Bank Code],description  as [Description]from TSPL_Bank_MASTER where bank_code='" + fndbankcode.Value + "'"
            Dim dr As DataTable
            ' Dim strvalue As String
            dr = clsDBFuncationality.GetDataTable(strquery)
            If dr.Rows IsNot Nothing AndAlso dr.Rows.Count > 0 Then
                TxtBankName.Text = dr.Rows(0)("Description").ToString()
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub

    'It will fill the  controls if value exist in database according to fndTxGrp
    Public Sub funfillfndtaxgrp()
        Try

            Dim strquery As String = "SELECT [Tax_Group_Code] AS [Tax Group Code],[Tax_Group_Desc] as [Description] FROM [TSPL_TAX_GROUP_MASTER] where Tax_Group_Code='" + fndTxGrp.Value + "' and Tax_Group_Type='P'"
            Dim dr As DataTable
            ' Dim strvalue As String
            dr = clsDBFuncationality.GetDataTable(strquery)
            If dr.Rows IsNot Nothing AndAlso dr.Rows.Count > 0 Then
                txtTxGrp.Text = dr.Rows(0)("Description").ToString()
                grdTax.DataSource = Nothing
                grdTax.Rows.Clear()
                fnTaxGrp()
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub
    'This is for filling the gridview according to tax group
    Public Sub fnTaxGrp()
        Try
            Dim strcmd As String
            Dim myDs As DataSet
            Dim i As Integer
            ' strcmd = " SELECT TSPL_TAX_GROUP_DETAILS.Tax_Code as [Tax] FROM TSPL_TAX_GROUP_MASTER INNER JOIN TSPL_TAX_GROUP_DETAILS ON TSPL_TAX_GROUP_MASTER.Tax_Group_Code = TSPL_TAX_GROUP_DETAILS.Tax_Group_Code" & _
            '" where TSPL_TAX_GROUP_MASTER.Tax_Group_Code='" + fndTxGrp.txtValue.Text + "' and  TSPL_TAX_GROUP_MASTER.Tax_Group_Type='P'and TSPL_TAX_GROUP_MASTER.Tax_Group_Type='P'"
            strcmd = " SELECT TSPL_TAX_GROUP_DETAILS.Tax_Code as [Tax] FROM TSPL_TAX_GROUP_MASTER INNER JOIN TSPL_TAX_GROUP_DETAILS ON TSPL_TAX_GROUP_MASTER.Tax_Group_Code = TSPL_TAX_GROUP_DETAILS.Tax_Group_Code" &
                     " where  TSPL_TAX_GROUP_DETAILS.Tax_Group_Type='P' and TSPL_TAX_GROUP_MASTER.Tax_Group_Type='P' and  TSPL_TAX_GROUP_MASTER.Tax_Group_Code='" + fndTxGrp.Value + "' ORDER BY Trans_Code "
            myDs = connectSql.RunSQLReturnDS(strcmd)
            If myDs.Tables(0).Rows.Count > 0 Then
                Dim Dr As DataRow
                i = 0
                For Each Dr In myDs.Tables(0).Rows
                    Dim r As GridViewRowInfo = grdTax.Rows.AddNew()
                    r.Cells(0).Value = Dr(0).ToString()
                Next
            End If
            grdTax.AllowAddNewRow = False
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Customer")
        End Try
    End Sub
    'Thid funtion will fill all the fields on selecting the value fronm finder
    Public Sub funfill(Optional ByVal VCode As String = "")
        Try
            Dim Is_TDS_App As String = ""
            grdTax.Rows.Clear()
            Dim strCmd As String
            Dim myDs As DataSet
            '' Anubhooti 26-Aug-2014 BM00000003619 (Only Fetches Pin_Code)
            '' Anubhooti 1-Aug-2104 (Fetches Five Columns)
            If VCode = "" Then
                strCmd = " Select Vendor_Name, Vendor_Group_Code,  Vendor_Group_Code_Desc,  Status ,OnHold  ,Convert(Date,Closing_Date,103) ,Add1 ,	Add2 ,Add3 ," &
                     "City_Code ,City_Code_Desc ,State ,Country ,	Phone1 ,Phone2 ,Fax,Email ,WebSite ,Contact_Person_Name ,Contact_Person_Phone ," &
                     "Contact_Person_Fax ,Contact_Person_Website ,Contact_Person_Email ,Terms_Code ,Terms_Code_Desc ,Vendor_Account ,Vendor_Account_Desc ," &
                     "Payment_Code,Payment_Code_Desc ,Ven_Type_Code ,Ven_Type_Desc ,Bank_Code ,Bank_Code_Desc ,Service_Tax_No ,Lst_No ,Tin_No ,	Credit_Limit," &
                     "Tax_Group ,Tax_Group_Desc ,TAX1 ,TAX1_Rate ,TAX2,TAX2_Rate ,TAX3 ,TAX3_Rate ,TAX4 ,TAX4_Rate ,TAX5 ,TAX5_Rate ,TAX6 ,TAX6_Rate ," &
                     "TAX7 ,TAX7_Rate ,TAX8 ,TAX8_Rate ,TAX9 ,TAX9_Rate ,TAX10 ,TAX10_Rate ,Remarks1 ,Remarks2 ,Additional1 ,Additional2 ,Additional3,transporter,CST,ECC,Range,Collectorate,PAN,is_Gross_Receipt,Inter_branch,currency_code,franchise_yn,state_code,country_code,Parent_Vendor_Code,Is_Parent_Vendor,Category_Struct_Code,branch_code,Branch_Name,Account_No,Bank_Name,IFSC_Code,Account_Type,Vendor_Type,Pin_Code,Is_Chilling_Vendor,Is_TDS_Applicable,TDS_Branch_Code,Deduction_Code,TDS_Vendor_Type,TDS_Status,TDS_State_Code,csa_type,ISNULL(Alies_Name,'') As [Alies Name],ISNULL(Vendor_Type_CHA,'') As Vendor_Type_CHA,IsVendorInvoiceNo,cha_doc_no,Is_TC_Certified,TC_Certified,ISNULL(Other_For_Pan,0) AS Other_For_Pan,Cheque_In_Favour_Of,tspl_vendor_master.PC_CODE,tspl_vendor_master.Vendor_Distance,tspl_vendor_master.Industry_Type,tspl_vendor_master.oldname,tspl_vendor_master.SSI_No,tspl_vendor_master.Is_Blacklist,weight,JWPriceCode,IsEmployee,isHighClass , Bulk_ROUTE_NO,EMP_CODE,Registration_No,TSPL_VENDOR_MASTER.IsTCSnotApplicable,TSPL_VENDOR_MASTER.Isbuyerfilereturninlasttwoyears,TSPL_VENDOR_MASTER.IsTCS_TDSamountgreaterthan50KpreviousYear,Deduction_Code_Service,TDS_State_Code_Service,TDS_Branch_Code_Service,TDS_Vendor_Type_Service,TDS_Status_Service,TSPL_VENDOR_MASTER.IsAllowSkipPurchaseQC,TSPL_VENDOR_MASTER.OEM,TSPL_VENDOR_MASTER.Is_Provisional,Is_Default_Grower,In_Active_CF  from tspl_vendor_master where vendor_code='" + fndvendorNo.Value + "' " 'and form_type='ALL'	"
            Else
                strCmd = " Select Vendor_Name, Vendor_Group_Code,  Vendor_Group_Code_Desc,  Status ,OnHold  ,Convert(Date,Closing_Date,103) ,Add1 ,	Add2 ,Add3 ," &
                     "City_Code ,City_Code_Desc ,State ,Country ,	Phone1 ,Phone2 ,Fax,Email ,WebSite ,Contact_Person_Name ,Contact_Person_Phone ," &
                     "Contact_Person_Fax ,Contact_Person_Website ,Contact_Person_Email ,Terms_Code ,Terms_Code_Desc ,Vendor_Account ,Vendor_Account_Desc ," &
                     "Payment_Code,Payment_Code_Desc ,Ven_Type_Code ,Ven_Type_Desc ,Bank_Code ,Bank_Code_Desc ,Service_Tax_No ,Lst_No ,Tin_No ,	Credit_Limit ," &
                     "Tax_Group ,Tax_Group_Desc ,TAX1 ,TAX1_Rate ,TAX2,TAX2_Rate ,TAX3 ,TAX3_Rate ,TAX4 ,TAX4_Rate ,TAX5 ,TAX5_Rate ,TAX6 ,TAX6_Rate ," &
                     "TAX7 ,TAX7_Rate ,TAX8 ,TAX8_Rate ,TAX9 ,TAX9_Rate ,TAX10 ,TAX10_Rate ,Remarks1 ,Remarks2 ,Additional1 ,Additional2 ,Additional3,transporter,CST,ECC,Range,Collectorate,PAN,is_Gross_Receipt,Inter_branch,currency_code,franchise_yn,state_code,country_code,Parent_Vendor_Code,Is_Parent_Vendor,Category_Struct_Code,branch_code,Branch_Name,Account_No,Bank_Name,IFSC_Code,Account_Type,Vendor_Type,Pin_Code,Vendor_Code,Is_Chilling_Vendor,Is_TDS_Applicable,TDS_Branch_Code,Deduction_Code,TDS_Vendor_Type,TDS_Status,TDS_State_Code,csa_type,ISNULL(Alies_Name,'') As [Alies Name],ISNULL(Vendor_Type_CHA,'') As Vendor_Type_CHA,IsVendorInvoiceNo,cha_doc_no,Is_TC_Certified,TC_Certified,ISNULL(Other_For_Pan,0) AS Other_For_Pan,Cheque_In_Favour_Of,tspl_vendor_master.PC_CODE,tspl_vendor_master.Vendor_Distance,tspl_vendor_master.Industry_Type,tspl_vendor_master.oldname,tspl_vendor_master.SSI_No,tspl_vendor_master.Is_Blacklist,DFOption,BusinessCondition,GSTRegistered,GST_Composition_scheme,GSTEntity,GSTLastEntity,GSTFinalNo,weight,JWPriceCode,IsEmployee, isHighClass , Bulk_ROUTE_NO,EMP_CODE,Registration_No,TSPL_VENDOR_MASTER.IsTCSnotApplicable,TSPL_VENDOR_MASTER.Isbuyerfilereturninlasttwoyears,TSPL_VENDOR_MASTER.IsTCS_TDSamountgreaterthan50KpreviousYear,Deduction_Code_Service,TDS_State_Code_Service,TDS_Branch_Code_Service,TDS_Vendor_Type_Service,TDS_Status_Service,TSPL_VENDOR_MASTER.IsAllowSkipPurchaseQC,TSPL_VENDOR_MASTER.OEM,TSPL_VENDOR_MASTER.Is_Provisional,Is_Default_Grower,In_Active_CF  from TSPL_VENDOR_MASTER where vendor_code='" + VCode + "'" ' and form_type='ALL'	"
            End If



            myDs = connectSql.RunSQLReturnDS(strCmd)
            Dim myDr As DataRow
            For Each myDr In myDs.Tables(0).Rows
                fndVendorReg.Value = clsCommon.myCstr(myDr("Registration_No"))
                txt_ssino.Text = clsCommon.myCstr(myDr("SSI_No"))
                chk_isblacklist.Checked = IIf(clsCommon.myCstr(myDr("Is_Blacklist").ToString()) = "1", True, False)
                chkIsEmployee.Checked = IIf(clsCommon.myCstr(myDr("IsEmployee").ToString()) = "1", True, False)
                EmployeeFind.Value = clsCommon.myCstr(myDr("EMP_CODE"))
                txtCategoryStructureCode.Value = clsCommon.myCstr(myDr("Category_Struct_Code"))
                lblCategoryStructureCode.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select description from TSPL_ITEM_CATEGORY_STRUCTURE where item_category_struct_code='" + txtCategoryStructureCode.Value + "' and isnull(form_type,'item')='vendor'"))

                'chkcsa.Checked = clsCommon.myCBool(IIf(clsCommon.myCstr(myDr("csa_type")) = "Y", True, False))
                If clsCommon.CompairString(clsCommon.myCstr(myDr("CSA_Type")), "Y") = CompairStringResult.Equal Then
                    CmbVenType.SelectedValue = "CSA"
                ElseIf clsCommon.CompairString(clsCommon.myCstr(myDr("Is_Chilling_vendor")), "1") = CompairStringResult.Equal Then
                    CmbVenType.SelectedValue = "CV"
                Else
                    CmbVenType.SelectedValue = clsCommon.myCstr(myDr("Vendor_Type_CHA"))
                End If

                Me.txtvendorname.Text = myDr(0).ToString()
                Me.fndgroupcode.Value = myDr(1).ToString()
                Me.txtgroupdes.Text = myDr(2).ToString()

                Dim strStatus As String = myDr(3).ToString()
                If strStatus = "N" Then
                    chkInActive.Checked = False
                ElseIf strStatus = "Y" Then
                    chkInActive.Checked = True
                End If

                'IsVendorInvoiceNo In_Active_CF
                Dim IsVendorInvoiceNo As Integer = 0
                IsVendorInvoiceNo = clsCommon.myCdbl(myDr("IsVendorInvoiceNo"))
                If IsVendorInvoiceNo = 1 Then
                    chkVendorInvoiceNo.Checked = True
                Else
                    chkVendorInvoiceNo.Checked = False
                End If
                Dim InActive As String
                InActive = clsCommon.myCstr(myDr("In_Active_CF"))
                If InActive = "Y" Then
                    InActiveCF.Checked = True
                Else
                    InActiveCF.Checked = False
                End If
                chkOEM.Checked = (clsCommon.myCDecimal(myDr("OEM")) = 1)
                chkProvisional.Checked = IIf(clsCommon.myCdbl(myDr("Is_Provisional")) = 1, True, False)
                chkDefaultGrower.Checked = IIf(clsCommon.myCdbl(myDr("Is_Default_Grower")) = 1, True, False)
                Dim strHold As String = myDr(4).ToString()
                If strHold = "N" Then
                    chkHold.Checked = False
                ElseIf strHold = "Y" Then
                    chkHold.Checked = True
                End If

                If clsCommon.CompairString(clsCommon.myCstr(myDr("Transporter")), "Y") = CompairStringResult.Equal Then
                    chktrarns.Checked = True
                Else
                    chktrarns.Checked = False
                End If
                '====shivani
                txtChequeInFavour.Text = clsCommon.myCstr(myDr("Cheque_In_Favour_Of"))
                '====
                If (myDr(5).ToString()) = Nothing Then
                    Me.dtClosing.Value = System.DateTime.Now.Date
                Else
                    Me.dtClosing.Value = CDate(myDr(5).ToString())
                End If
                Me.txtAdd1.Text = myDr(6).ToString()
                Me.txtAdd2.Text = myDr(7).ToString()
                Me.txtAdd3.Text = myDr(8).ToString()
                Me.fndCity.Value = myDr(9).ToString()
                Me.txtCity.Text = myDr(10).ToString()
                Me.LblState.Text = clsStateMaster.GetName(clsCommon.myCstr(myDr("State_Code")))
                '' Anubhooti 26-Aug-2014 BM00000003619
                Me.txtPinCode.Text = myDr("Pin_Code").ToString()

                fndCHA_Code.Value = clsCommon.myCstr(myDr("cha_doc_no"))
                txtCHA_Type.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select (case when cha_type='ICD' then 'Dry Port-ICD(Inland Container Depot)' else case when cha_type='ISD' then 'Sea Port' else case when cha_type='THC' then 'Terminal Handling Charges at ICD& Sea Port' else case when cha_type='IHC' then 'Inland Haulage Charges at ICD& Sea Port' else case when cha_type='OTH' then 'Other' else 'None' end end end end end) as type_cha from tspl_cha_charge_master where doc_no='" + fndCHA_Code.Value + "'"))
                txtCHA_Amount.Text = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select amount from tspl_cha_charge_master where doc_no='" + fndCHA_Code.Value + "'"))


                '----------------------------------------------------Monika 22/05/2014-----------------------
                txtcountrycode.Value = clsCommon.myCstr(myDr("country_code"))
                txtCountry.Text = clsCountryMaster.GetName(txtcountrycode.Value, Nothing)
                txtState.Value = clsCommon.myCstr(myDr("state_code"))

                If AllowGSTApplicable = True Then
                    Dim check As String = clsDBFuncationality.getSingleValue("select case when isnull(GST_State_Code,'')='' then '' else GST_State_Code end as GST_State_Code from tspl_state_master where state_code='" & txtState.Value & "'")
                    If clsCommon.myLen(check) > 0 Then
                        txtGSTStateCode.Text = check
                    End If

                    txtEntity.Text = clsCommon.myCstr(myDr("GSTEntity"))
                    MyTextBox2.Text = clsCommon.myCstr(myDr("GSTLastEntity"))
                    Rchkregistered.Checked = IIf(clsCommon.myCstr(myDr("GSTRegistered").ToString()) = "1", True, False)
                    RchkCompscheme.Checked = IIf(clsCommon.myCstr(myDr("GST_Composition_scheme").ToString()) = "1", True, False)
                    txtGSTIN_No_final.Text = clsCommon.myCstr(myDr("GSTFinalNo"))
                    Dim Doption As String = clsCommon.myCstr(myDr("DFOption"))
                    If clsCommon.myCstr(Doption) = "Foreign" Then
                        rbtnForeign.IsChecked = True
                    Else
                        rbtnDomestic.IsChecked = True
                    End If
                    rdrpbusiness.SelectedValue = clsCommon.myCstr(myDr("BusinessCondition"))
                End If

                If clsCommon.CompairString(clsCommon.myCstr(myDr("Is_Parent_Vendor")), "1") = CompairStringResult.Equal Then
                    chkparentvendor.Checked = True
                    fndparent.Value = ""
                    txtparentname.Text = ""
                Else
                    chkparentvendor.Checked = False
                    fndparent.Value = clsCommon.myCstr(myDr("Parent_Vendor_Code"))
                    If clsCommon.myLen(fndparent.Value) > 0 Then
                        txtparentname.Text = clsCommon.myCstr(clsVendorMaster.GetName(fndparent.Value, Nothing))
                    End If
                End If
                chkparentvendorstatus()
                '---------------------------------for old data getting code'-----------------------------
                If clsCommon.myLen(LblState.Text) > 0 AndAlso clsCommon.myLen(txtState.Value) <= 0 Then
                    Dim qry As String = "select state_code from tspl_state_master where state_name like '%" + LblState.Text + "%'"
                    txtState.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
                End If

                If clsCommon.myLen(txtCountry.Text) > 0 AndAlso clsCommon.myLen(txtcountrycode.Value) <= 0 Then
                    Dim qry As String = "select country_code from tspl_country_master where country_name like '%" + txtCountry.Text + "%'"
                    txtcountrycode.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
                End If
                '---------------------------------------------------------------------------------

                Me.txtPhone1.Text = myDr(13).ToString()
                Me.txtPhone2.Text = myDr(14).ToString()
                Me.txtfax.Text = myDr(15).ToString()
                Me.txtEmail.Text = myDr(16).ToString()
                Me.txtWeb.Text = myDr(17).ToString()
                Me.txtContactName.Text = myDr(18).ToString()
                Me.txtContPhone.Text = myDr(19).ToString()
                Me.txtContactFax.Text = myDr(20).ToString()
                Me.txtContactWeb.Text = myDr(21).ToString()
                Me.txtContactEmail.Text = myDr(22).ToString()
                Me.fndTrmsCode.Value = myDr(23).ToString()
                Me.txttermcodedes.Text = myDr(24).ToString()
                Me.fndAccntSet.Value = myDr(25).ToString()
                Me.txtaccsetdes.Text = myDr(26).ToString()
                Me.fndPayCode.Value = myDr(27).ToString()
                Me.txtpaymentcodedes.Text = myDr(28).ToString()
                Me.fndvendortype.Value = myDr(29).ToString()
                Me.txtvendortypedes.Text = myDr(30).ToString()
                Me.fndbankcode.Value = myDr("Bank_Code").ToString()
                'Me.txtbankcodedes.Text = myDr(32).ToString()
                Me.txtStaxNo.Text = myDr(33).ToString()
                Me.txtLstNo.Text = myDr(34).ToString()
                Me.txtTinNo.Text = myDr(35).ToString()
                Me.txtCredit.Text = myDr(36).ToString()
                Me.fndTxGrp.Value = myDr(37).ToString()
                Me.txtTxGrp.Text = myDr(38).ToString()
                Me.txtRemarks1.Text = myDr(59).ToString()
                Me.txtRemarks2.Text = myDr(60).ToString()
                Me.txtAddInfo1.Text = myDr(61).ToString()
                Me.txtAddInfo2.Text = myDr(62).ToString()
                Me.txtAddInfo3.Text = myDr(63).ToString()

                txtWeight.Text = clsCommon.myCdbl(myDr("Weight"))

                txtJWPriceCode.Value = clsCommon.myCstr(myDr("JWPriceCode"))

                '' Anubhooti 10-Oct-2014 BM00000004198
                'chkIsGrossReceipt.Checked = IIf(clsCommon.myCdbl(myDr("is_Gross_Receipt")) = 1, True, False)
                Me.ddl_industrytype.SelectedValue = clsCommon.myCstr(myDr("Industry_Type").ToString())
                If clsCommon.CompairString(ddl_industrytype.SelectedValue, "") = CompairStringResult.Equal Then
                    Me.ddl_industrytype.SelectedValue = "Others"
                End If
                ''For Custom Fields
                If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
                    UcCustomFields1.BlankAllControls()
                    UcCustomFields1.LoadData(fndvendorNo.Value)
                End If
                ''End of For Custom Fields


                chkTCSNotApplicable.Checked = (clsCommon.myCdbl(myDr("IsTCSnotApplicable")) > 0)
                chkbuyerfilereturnlasttwoyear.Checked = IIf(clsCommon.myCstr(myDr("Isbuyerfilereturninlasttwoyears").ToString()) = "1", True, False)
                chkTCSTDSamountgreater50KpreviousYear.Checked = IIf(clsCommon.myCstr(myDr("IsTCS_TDSamountgreaterthan50KpreviousYear").ToString()) = "1", True, False)
                chkIsAllowSkipPurchaseQC.Checked = IIf(clsCommon.myCstr(myDr("IsAllowSkipPurchaseQC").ToString()) = "1", True, False)

                '' multicurrency
                Me.fndVendorCurrency.Value = myDr("currency_code").ToString()
                '' end multicurrency
                fndpaymentCycle.Value = myDr("PC_CODE").ToString
                lblpaymentCycle.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select DESCRIPTION from TSPL_PAYMENT_CYCLE_MASTER where PC_CODE='" + fndpaymentCycle.Value + "'"))
                txtVendorDistance.Text = myDr("Vendor_Distance").ToString
                Dim strtran As String = myDr(64).ToString()
                If strtran = "N" Then
                    chktrarns.Checked = False
                ElseIf strtran = "Y" Then
                    chktrarns.Checked = True
                ElseIf strtran = "" Then
                    chktrarns.Checked = False
                End If
                Me.txtcst.Text = myDr(65).ToString()
                Me.txtecc.Text = myDr(66).ToString()
                Me.txtrange.Text = myDr(67).ToString()
                Me.txtcollect.Text = myDr(68).ToString()
                ChkOther.Checked = IIf(clsCommon.myCstr(myDr("Other_For_PAN")) = "1", True, False)
                Me.txtpan.Text = myDr(69).ToString()
                If AllowGSTApplicable = True Then
                    Me.txtGST_PanCode.Text = myDr(69).ToString()
                End If
                Dim interbranch As String = myDr("Inter_Branch").ToString()
                If interbranch = "Y" Then
                    chkInterBranch.Checked = True
                ElseIf interbranch = "N" Then
                    chkInterBranch.Checked = False
                End If
                Dim strtagasfranchise As String = myDr("franchise_yn").ToString()
                If strtagasfranchise = "Y" Then
                    chkTagAsFranchise.Checked = True
                ElseIf interbranch = "N" Then
                    chkTagAsFranchise.Checked = False
                End If

                '' Anubhooti 1-Aug-2014 BM00000003318
                Me.TxtBankName.Text = myDr("Bank_Name").ToString()
                ''richa agarwal 24/03/2015
                ' Me.TxtBankBranch.Text = myDr("Branch_Name").ToString()
                Me.TxtBankBranch.Value = myDr("branch_code").ToString()
                Me.txtbranchname.Text = myDr("Branch_Name").ToString()
                ''-------
                Me.TxtAccNo.Text = myDr("Account_No").ToString()
                Me.TxtIFSCCode.Text = myDr("IFSC_Code").ToString()
                If clsCommon.myLen(myDr("Account_Type")) > 0 Then
                    Me.cmbAccountType.SelectedValue = clsCommon.myCstr(myDr("Account_Type"))
                Else
                    Me.cmbAccountType.SelectedValue = clsCommon.myCstr("Cur")
                End If
                '' Anubhooti 4-Aug-2014 BM00000003368 
                If clsCommon.myLen(myDr("Vendor_Type")) > 0 Then
                    Me.cmbTypeOfVen.SelectedValue = clsCommon.myCstr(myDr("Vendor_Type"))
                Else
                    Me.cmbTypeOfVen.SelectedValue = clsCommon.myCstr("N")
                End If
                '' Anubhooti 01-Sep-2014
                fndvendorNo.Value = clsCommon.myCstr(myDr("Vendor_Code"))
                ChkIsTDSApp.Checked = IIf(clsCommon.myCstr(myDr("Is_TDS_Applicable")) = "1", True, False)

                If ChkIsTDSApp.Checked = True Then
                    GrpTDS.Enabled = True
                    Me.fnddeducNew.Value = clsCommon.myCstr(myDr("Deduction_Code"))
                    If clsCommon.myLen(fnddeducNew.Value) > 0 Then
                        lblNatureOfDed.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  description  from TSPL_TDS_DEDUCTION_HEAD where deduction_code ='" & clsCommon.myCstr(fnddeducNew.Value) & "'"))
                    End If
                    Me.txttdsstate.Value = clsCommon.myCstr(myDr("TDS_State_Code"))
                    If clsCommon.myLen(txttdsstate.Value) > 0 Then
                        lblStateName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  State_Name from TSPL_STATE_MASTER where State_Code='" & clsCommon.myCstr(txttdsstate.Value) & "'"))
                    End If
                    Me.fndbranchnew.Value = clsCommon.myCstr(myDr("TDS_Branch_Code"))
                    If clsCommon.myLen(fndbranchnew.Value) > 0 Then
                        LblBranchName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  Branch_Name from  TSPL_TDS_BRANCH_MASTER where Branch_Code='" & clsCommon.myCstr(fndbranchnew.Value) & "'"))
                    End If
                    Me.ddlventype.Text = clsCommon.myCstr(myDr("TDS_Vendor_Type"))
                    Me.ddlstatus.Text = clsCommon.myCstr(myDr("TDS_Status"))



                    GrpTDSService.Enabled = True
                    Me.fnddeducNewService.Value = clsCommon.myCstr(myDr("Deduction_Code_Service"))
                    If clsCommon.myLen(fnddeducNewService.Value) > 0 Then
                        lblNatureOfDedService.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  description  from TSPL_TDS_DEDUCTION_HEAD where deduction_code ='" & clsCommon.myCstr(fnddeducNewService.Value) & "'"))
                    End If
                    Me.txttdsstate.Value = clsCommon.myCstr(myDr("TDS_State_Code_Service"))
                    If clsCommon.myLen(txttdsstateService.Value) > 0 Then
                        lblStateNameService.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  State_Name from TSPL_STATE_MASTER where State_Code='" & clsCommon.myCstr(txttdsstateService.Value) & "'"))
                    End If
                    Me.fndbranchnewService.Value = clsCommon.myCstr(myDr("TDS_Branch_Code_Service"))
                    If clsCommon.myLen(fndbranchnewService.Value) > 0 Then
                        LblBranchNameService.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  Branch_Name from  TSPL_TDS_BRANCH_MASTER where Branch_Code='" & clsCommon.myCstr(fndbranchnewService.Value) & "'"))
                    End If
                    Me.ddlventypeService.Text = clsCommon.myCstr(myDr("TDS_Vendor_Type_Service"))
                    Me.ddlstatusService.Text = clsCommon.myCstr(myDr("TDS_Status_Service"))

                Else
                    GrpTDS.Enabled = False
                    Me.fnddeducNew.Value = ""
                    Me.fndbranchnew.Value = ""
                    txttdsstate.Value = ""
                    lblStateName.Text = ""
                    lblNatureOfDed.Text = ""
                    LblBranchName.Text = ""
                    ddlventype.Text = "Individual"
                    ddlstatus.Text = "Resident"

                    Me.fnddeducNewService.Value = ""
                    Me.fndbranchnewService.Value = ""
                    txttdsstateService.Value = ""
                    lblStateNameService.Text = ""
                    lblNatureOfDedService.Text = ""
                    LblBranchNameService.Text = ""
                    ddlventypeService.Text = "Individual"
                    ddlstatusService.Text = "Resident"
                End If

                '' Anubhooti 24-Sep-2014 (Bank Details From Vendor Bank Master)
                If EnableBankFromMaster = True Then
                    If clsCommon.myLen(fndbankcode.Value) > 0 Then
                        Dim obj As clsVendorBankMaster = clsVendorBankMaster.GetData(fndbankcode.Value, NavigatorType.Current)
                        If obj Is Nothing Then
                            Exit Sub
                        End If
                        TxtBankName.Text = obj.Bank_Name
                        txtbankcountry.Text = obj.country_name
                        txtbankstate.Text = obj.state_name
                        txtbankcity.Text = obj.city_name
                        'TxtBankBranch.Value = obj.Branch_Code
                        'txtbranchname.Text = obj.Branch_Name
                        'TxtIFSCCode.Text = obj.IFSC_Code
                    Else
                        TxtBankName.Text = ""
                        txtbankcountry.Text = "India"
                        txtbankstate.Text = ""
                        txtbankcity.Text = ""
                        '  txtbranchname.Text = ""
                        TxtAccNo.Text = ""
                        ' TxtIFSCCode.Text = ""
                        '  TxtBankBranch.Value = ""
                        ' txtbranchname.Text = ""
                        ' TxtIFSCCode.Text = ""
                    End If
                End If

                ''
                '' Anubhooti 06-Oct-2014 BM00000003996
                Me.txtAliesName.Text = clsCommon.myCstr(myDr("Alies Name"))
                '' Anubhooti 15-Jan-2014 (Auto Industry)
                If clsCommon.CompairString(IndustryType, "A") = CompairStringResult.Equal Then
                    GrpBoxTC.Visible = True
                    If clsCommon.CompairString(clsCommon.myCstr(myDr("Is_TC_Certified")), "1") = CompairStringResult.Equal Then
                        ChkTCCertified.Checked = True
                        TxtTCCertified.Text = clsCommon.myCstr(myDr("TC_Certified"))
                    Else
                        ChkTCCertified.Checked = False
                        TxtTCCertified.Text = ""
                    End If
                Else
                    GrpBoxTC.Visible = False
                End If

                TxtOldname.Text = clsCommon.myCstr(myDr("OldName"))
                '================Rohit=======================
                'ChkChillingVendor.Checked = IIf(clsCommon.myCstr(myDr("Is_Chilling_Vendor")) = "1", True, False)
                If clsCommon.CompairString(clsCommon.myCstr(myDr(39)), "") <> CompairStringResult.Equal Then
                    If grdTax.Rows.Count < 1 Then
                        grdTax.Rows.AddNew()
                    End If

                    Me.grdTax.Rows(0).Cells(0).Value = myDr(39).ToString
                    Me.grdTax.Rows(0).Cells(1).Value = myDr(40).ToString
                End If

                If clsCommon.CompairString(clsCommon.myCstr(myDr(41)), "") <> CompairStringResult.Equal Then
                    If grdTax.Rows.Count <= 2 Then
                        grdTax.Rows.AddNew()
                    End If
                    Me.grdTax.Rows(1).Cells(0).Value = myDr(41).ToString
                    Me.grdTax.Rows(1).Cells(1).Value = myDr(42).ToString
                End If

                If clsCommon.CompairString(clsCommon.myCstr(myDr(43)), "") <> CompairStringResult.Equal Then
                    If grdTax.Rows.Count <= 3 Then
                        grdTax.Rows.AddNew()
                    End If
                    Me.grdTax.Rows(2).Cells(0).Value = myDr(43).ToString
                    Me.grdTax.Rows(2).Cells(1).Value = myDr(44).ToString
                Else
                    ' Return
                End If

                If clsCommon.CompairString(clsCommon.myCstr(myDr(45)), "") <> CompairStringResult.Equal Then
                    If grdTax.Rows.Count <= 4 Then
                        grdTax.Rows.AddNew()
                    End If
                    Me.grdTax.Rows(3).Cells(0).Value = myDr(45).ToString
                    Me.grdTax.Rows(3).Cells(1).Value = myDr(46).ToString
                Else
                    'Return
                End If

                If clsCommon.CompairString(clsCommon.myCstr(myDr(47)), "") <> CompairStringResult.Equal Then
                    If grdTax.Rows.Count <= 5 Then
                        grdTax.Rows.AddNew()
                    End If
                    Me.grdTax.Rows(4).Cells(0).Value = myDr(47).ToString
                    Me.grdTax.Rows(4).Cells(1).Value = myDr(48).ToString
                Else
                    'Return
                End If

                If clsCommon.CompairString(clsCommon.myCstr(myDr(49)), "") <> CompairStringResult.Equal Then
                    If grdTax.Rows.Count <= 6 Then
                        grdTax.Rows.AddNew()
                    End If
                    Me.grdTax.Rows(5).Cells(0).Value = myDr(49).ToString
                    Me.grdTax.Rows(5).Cells(1).Value = myDr(50).ToString
                Else
                    'Return
                End If

                If clsCommon.CompairString(clsCommon.myCstr(myDr(51)), "") <> CompairStringResult.Equal Then
                    If grdTax.Rows.Count <= 7 Then
                        grdTax.Rows.AddNew()
                    End If
                    Me.grdTax.Rows(6).Cells(0).Value = myDr(51).ToString
                    Me.grdTax.Rows(6).Cells(1).Value = myDr(52).ToString
                Else
                    'Return
                End If

                If clsCommon.CompairString(clsCommon.myCstr(myDr(53)), "") <> CompairStringResult.Equal Then
                    If grdTax.Rows.Count <= 8 Then
                        grdTax.Rows.AddNew()
                    End If
                    Me.grdTax.Rows(7).Cells(0).Value = myDr(53).ToString
                    Me.grdTax.Rows(7).Cells(1).Value = myDr(54).ToString
                Else
                    'Return
                End If

                If clsCommon.CompairString(clsCommon.myCstr(myDr(55)), "") <> CompairStringResult.Equal Then
                    If grdTax.Rows.Count <= 9 Then
                        grdTax.Rows.AddNew()
                    End If
                    Me.grdTax.Rows(8).Cells(0).Value = myDr(55).ToString
                    Me.grdTax.Rows(8).Cells(1).Value = myDr(56).ToString
                Else
                    'Return
                End If

                If clsCommon.CompairString(clsCommon.myCstr(myDr(57)), "") <> CompairStringResult.Equal Then
                    If grdTax.Rows.Count <= 10 Then
                        grdTax.Rows.AddNew()
                    End If
                    Me.grdTax.Rows(9).Cells(0).Value = myDr(57).ToString()
                    Me.grdTax.Rows(9).Cells(1).Value = myDr(58).ToString
                Else
                    'Return
                End If

                'Me.txtRemarks1.Text = myDr(59).ToString()
                'Me.txtRemarks2.Text = myDr(60).ToString()
                'Me.txtAddInfo1.Text = myDr(61).ToString()
                'Me.txtAddInfo2.Text = myDr(62).ToString()
                'Me.txtAddInfo3.Text = myDr(63).ToString()
                Dim strIsHighClass As String = myDr("isHighClass").ToString()
                If strIsHighClass = "0" Then
                    chkHighClass.Checked = False
                ElseIf strIsHighClass = "1" Then
                    chkHighClass.Checked = True
                End If
                fndBulkRouteCode.Value = clsCommon.myCstr(myDr("Bulk_ROUTE_NO").ToString())
                lblBulkRouteNo.Text = clsDBFuncationality.getSingleValue("Select ROUTE_NAME from TSPL_BULK_ROUTE_MASTER where ROUTE_NO='" + fndBulkRouteCode.Value + "'")

            Next
            'btnsave.Text = "Update"
            'btnsave.Enabled = True
            'btndelete.Enabled = True
            LoadBlankGridCat()
            If clsCommon.myLen(txtCategoryStructureCode.Value) > 0 Then
                FunFillCategory()
            End If

            If clsCommon.CompairString(CmbVenType.SelectedValue, "CHA") = CompairStringResult.Equal Then
                fndCHA_Code.Enabled = True
            End If

            UcAttachment1.LoadData(fndvendorNo.Value)

            If clsVendorMaster.checkisIDSapplicable(fndgroupcode.Value, Nothing) Then
                ChkIsTDSApp.Checked = True
                ChkIsTDSApp.Enabled = False
            Else
                ChkIsTDSApp.Enabled = True
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Customer")
        End Try
    End Sub

    Sub FunFillCategory()
        Try
            isInsideLoadData = True

            Dim qry As String = "select TSPL_VENDOR_CATEGORY_MASTER.*,TSPL_ITEM_CATEGORY_LEVEL.description as cat_desc,TSPL_ITEM_CATEGORY_LEVEL_VALUES.description from TSPL_VENDOR_CATEGORY_MASTER left outer join TSPL_ITEM_CATEGORY_LEVEL on TSPL_ITEM_CATEGORY_LEVEL.item_category_code=TSPL_VENDOR_CATEGORY_MASTER.Category_Code and isnull(TSPL_ITEM_CATEGORY_LEVEL.form_type,'Item')='vendor' left outer join TSPL_ITEM_CATEGORY_LEVEL_VALUES on isnull(TSPL_ITEM_CATEGORY_LEVEL_VALUES.form_type,'Item')='vendor' and TSPL_ITEM_CATEGORY_LEVEL_VALUES.form_type=TSPL_ITEM_CATEGORY_LEVEL.form_type and TSPL_ITEM_CATEGORY_LEVEL.item_category_code=TSPL_ITEM_CATEGORY_LEVEL_VALUES.item_category_code and TSPL_ITEM_CATEGORY_LEVEL_VALUES.code=TSPL_VENDOR_CATEGORY_MASTER.Category_Code_Values and TSPL_ITEM_CATEGORY_LEVEL_VALUES.item_category_code=TSPL_VENDOR_CATEGORY_MASTER.Category_Code where TSPL_VENDOR_CATEGORY_MASTER.vendor_code='" + fndvendorNo.Value + "' and TSPL_VENDOR_CATEGORY_MASTER.Category_Struct_Code='" + txtCategoryStructureCode.Value + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

            gvCategory.Rows.Clear()

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    gvCategory.Rows.AddNew()
                    gvCategory.Rows(gvCategory.Rows.Count - 1).Cells(CatcolCode).Value = clsCommon.myCstr(dr("Category_Code"))
                    gvCategory.Rows(gvCategory.Rows.Count - 1).Cells(CatcolCodeDesc).Value = clsCommon.myCstr(dr("cat_desc"))
                    gvCategory.Rows(gvCategory.Rows.Count - 1).Cells(CatcolValue).Value = clsCommon.myCstr(dr("Category_Code_Values"))
                    gvCategory.Rows(gvCategory.Rows.Count - 1).Cells(CatcolValueDesc).Value = clsCommon.myCstr(dr("description"))
                Next
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        isInsideLoadData = False
    End Sub


    'For inserting the data in the database
    Public Sub funinsert(ByVal trans As SqlTransaction)
        'Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try

            Dim strStatus As String = ""
            If chkInActive.Checked = True Then
                strStatus = "Y"                    '******* for:In-Active ********
            ElseIf chkInActive.Checked = False Then
                strStatus = "N"                    '******* for:Active ******** 
            End If

            Dim strInterBranch As String
            If chkInterBranch.Checked = True Then
                strInterBranch = "Y"
            Else
                strInterBranch = "N"
            End If

            Dim IsVendorInvoiceNo As Integer = 0
            If chkVendorInvoiceNo.Checked Then
                IsVendorInvoiceNo = 1
            Else
                IsVendorInvoiceNo = 0
            End If


            Dim strTagAsFranchise As String
            If chkTagAsFranchise.Checked = True Then
                strTagAsFranchise = "Y"  '''''   for taging vendor as franchise
            Else
                strTagAsFranchise = "N" '''''   for untaging vendor as franchise
            End If

            Dim strchkInActiveCF As String = ""
            If InActiveCF.Checked = True Then
                strchkInActiveCF = "Y"  '''''   Actice for Cattle Feed
            Else
                strchkInActiveCF = "N" '''''  InActive for Cattle Feed
            End If

            Dim strHold As String = ""
            If chkHold.Checked = True Then
                strHold = "Y"                      '******* for:Hold ******** 
            ElseIf chkHold.Checked = False Then
                strHold = "N"                      '******* for:Remove Hold ********
            End If


            Dim strtrans As String = ""
            If chktrarns.Checked = True Then
                strtrans = "Y"                      '******* for:Transporter type ******** 
            ElseIf chktrarns.Checked = False Then
                strtrans = "N"                      '******* for:Remove Non Transporter type ********
            End If

            Dim strTax1 As String = ""
            Dim strTax1_Rate As Decimal = 0.0

            Dim strTax2 As String = ""
            Dim strTax2_Rate As Decimal = 0.0

            Dim strTax3 As String = ""
            Dim strTax3_Rate As Decimal = 0.0

            Dim strTax4 As String = ""
            Dim strTax4_Rate As Decimal = 0.0

            Dim strTax5 As String = ""
            Dim strTax5_Rate As Decimal = 0.0

            Dim strTax6 As String = ""
            Dim strTax6_Rate As Decimal = 0.0

            Dim strTax7 As String = ""
            Dim strTax7_Rate As Decimal = 0.0

            Dim strTax8 As String = ""
            Dim strTax8_Rate As Decimal = 0.0

            Dim strTax9 As String = ""
            Dim strTax9_Rate As Decimal = 0.0

            Dim strTax10 As String = ""
            Dim strTax10_Rate As Decimal = 0.0

            ' Dim Bal1 As Decimal
            Dim CrLimit As Decimal
            ' Dim OutComm As Decimal

            If txtCredit.Text = "" Then
                CrLimit = Convert.ToDecimal("0.00")
            Else
                CrLimit = Convert.ToDecimal(txtCredit.Text)
            End If
            Dim tin_no As Object
            tin_no = txtTinNo.Text
            'If Len(txtTinNo.Text) > 0 Then
            'Else
            '    common.clsCommon.MyMessageBoxShow("Tin_No can not be left blank ")
            '    pageCus.SelectedPage = RadPageViewPage4
            '    txtTinNo.Focus()
            '    Exit Sub
            'End If

            Dim IS_TDS_App As Integer
            If ChkIsTDSApp.Checked = True Then
                IS_TDS_App = 1
            ElseIf ChkIsTDSApp.Checked = False Then
                IS_TDS_App = 0
            End If

            '' Anubhooti 26-Aug-2014 BM00000003619 (Only Added Pin_Code In Update Query After Procedure Exection)
            '' Anubhooti 10-Oct-2014 BM00000004198 (Removed Gross Receipt)
            Dim IsGrossReceipt As Integer
            'Dim IsGrossReceipt As Integer = IIf(chkIsGrossReceipt.Checked, 1, 0)
            '' Anubhooti 01-Sep-2014 AutoGeneratedCode By VName
            Dim AllowAutoVCode As String = ""
            Dim AllowAutoVCodeForAllCompany As String = ""
            Dim AutoVendorCode As String = ""
            AllowAutoVCode = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AutoGeneratedVendorCode, clsFixedParameterCode.AutoGeneratedVendorCode, trans))
            AllowAutoVCodeForAllCompany = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AutoGeneratedVendorCodeForAllCompany, clsFixedParameterCode.AutoGeneratedVendorCodeForAllCompany, trans))
            If clsCommon.CompairString(AllowAutoVCode, "1") = CompairStringResult.Equal AndAlso clsCommon.myLen(fndvendorNo.Value.Trim()) <= 0 Then
                If clsCommon.CompairString(AllowAutoVCodeForAllCompany, "1") = CompairStringResult.Equal Then
                    AutoVendorCode = clsERPFuncationality.GetNextCode(trans, clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"), clsDocType.VendorMaster, "", "")
                    fndvendorNo.Value = AutoVendorCode
                Else
                    AutoVendorCode = clsERPFuncationality.GetVendorNextCode("TSPL_VENDOR_MASTER", "Vendor_Name", txtvendorname.Text, trans)
                    fndvendorNo.Value = AutoVendorCode

                End If

            Else
                fndvendorNo.Value = fndvendorNo.Value
            End If


            Dim TDSQry As String = ""
            Dim QryToGetOutAmt As String = ""
            Dim OutStandAmt As Double = 0

            QryToGetOutAmt = "Select Vendor_Code, MAX(Vendor_Name) as Vendor_Name, SUM([Due Amount]) as [Due Amount] from (" &
            " select xxx.Vendor_Code, TSPL_VENDOR_MASTER.Vendor_Name, xxx.DocNo as [Document Id], Case when Document_Type IN ('D','AV','OA') then [Document_Total]*-1 Else [Document_Total] End as [Due Amount], xxx.DocDate as [Document Date], case when Document_Type='I' then 'IN' when Document_Type='PY' then 'PY' when Document_Type='D' then 'DR' when Document_Type='C' then 'CR' when Document_Type='AV' then 'AV' when Document_Type='OA' then 'OA'when Document_Type='PY' then 'PY' when Document_Type='RC' then 'RC' end as [Document_Type] FROM (   " &
            " select Vendor_code, Document_No as DocNo , Document_Type , COnvert(Date,Invoice_Entry_Date, 103) as DocDate , (Case When TSPL_VENDOR_INVOICE_HEAD.is_For_TDS=1 Then TSPL_VENDOR_INVOICE_HEAD.Document_Total Else TSPL_VENDOR_INVOICE_HEAD.Document_Total-TDS_Actual_Amount-ISNULL((Select Balance_Amt from TSPL_VENDOR_INVOICE_HEAD as VH Where VH.Vendor_Code=TSPL_VENDOR_INVOICE_HEAD.Vendor_Code AND VH.Vendor_Invoice_No=TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_No AND VH.Document_Type IN ('D','C') AND ISNULL(VH.Against_PurchaseReturn_No,'')<>''),0) End)-ISNULL((Select SUM(Applied_Amount) from TSPL_PAYMENT_DETAIL LEFT OUTER JOIN TSPL_PAYMENT_HEADER ON TSPL_PAYMENT_HEADER.Payment_No=TSPL_PAYMENT_DETAIL.Payment_No Where TSPL_PAYMENT_HEADER.Posted=1 AND TSPL_PAYMENT_HEADER.IsChkReverse<>'Y' AND TSPL_PAYMENT_DETAIL.Document_No=TSPL_VENDOR_INVOICE_HEAD.Document_No AND  CONVERT(DATE,TSPL_PAYMENT_HEADER.Payment_Date,103)<=CONVERT(DATE,'02/09/2014',103)),0) as [Document_Total], TSPL_VENDOR_INVOICE_HEAD.Comp_Code, TSPL_VENDOR_INVOICE_HEAD.Loc_Code AS Location  from TSPL_VENDOR_INVOICE_HEAD  where ISNULL(tspl_vendor_invoice_head.Posting_Date,'')<> '' And (Len(ISNULL(Against_POInvoice_No, ''))<=0 AND Len(ISNULL(Against_PurchaseReturn_No, ''))<=0) or Document_Type='I'  and TSPL_VENDOR_INVOICE_HEAD.Posting_Date <> ''   " &
            " UNION ALL  " &
            " select  Vendor_code, TSPL_PAYMENT_HEADER.Payment_No as DocNo ,TSPL_PAYMENT_HEADER.Payment_Type as Document_Type , Convert(Date,Payment_Date, 103) as DocDate , Payment_Amount+TDS_Amount-isnull((case when TSPL_PAYMENT_HEADER.Payment_Type in('AV','OA','RC') then (select Amount from TSPL_BANK_REVERSE where Source_Type='AP' and Document_No=TSPL_PAYMENT_HEADER.Payment_No AND TSPL_BANK_REVERSE.Post<>'N' AND TSPL_BANK_REVERSE.Reversal_Date<=Convert(Date,'02/09/2014', 103)) else 0 end ),0) as [Document_Total], TSPL_PAYMENT_HEADER.Comp_Code, RIGHT(TSPL_BANK_MASTER.BANKACC , 3) as Location  from TSPL_PAYMENT_HEADER  LEFT OUTER JOIN TSPL_BANK_MASTER ON TSPL_PAYMENT_HEADER.Bank_Code= TSPL_BANK_MASTER.BANK_CODE where TSPL_PAYMENT_HEADER.Payment_Type NOT IN ('PY','MI') And (Posted='P' or Posted='1')    " &
            " UNION ALL  " &
            " select  VC_Code as Vendor_code, Document_No as DocNo, case when Amount_Type='Cr' then 'D' else  'C' end as Document_Type,CONVERT(Date,TSPL_VCGL_Head.Document_Date,103) as DocDate, Amount as [Document_Total], TSPL_VCGL_Head.Comp_Code ,  TSPL_VCGL_Head.Location_Segment as Location  from TSPL_VCGL_Head  where Document_Type='v' and TSPL_VCGL_Head.Status='1'    " &
            " UNION ALL  " &
            " select  TSPL_VCGL_Detail.VCGL_Code  as Vendor_code, TSPL_VCGL_Detail.Document_No as DocNo, case when TSPL_VCGL_Detail.Dr_Amount=0.0 then 'C' when TSPL_VCGL_Detail.Cr_Amount=0.0 then 'D' end as Document_Type, CONVERT(Date,TSPL_VCGL_Head.Document_Date,103) as DocDate, case when TSPL_VCGL_Detail.Dr_Amount=0.0 then TSPL_VCGL_Detail.Cr_Amount when TSPL_VCGL_Detail.Cr_Amount=0.0 then TSPL_VCGL_Detail.Dr_Amount end as [Document_Total], TSPL_VCGL_Head.Comp_Code ,  TSPL_VCGL_Head.Location_Segment as Location from  TSPL_VCGL_Detail  left outer join  TSPL_VCGL_Head on TSPL_VCGL_Head.Document_No=TSPL_VCGL_Detail.Document_No    where Row_Type='Vendor'  and TSPL_VCGL_Head.Status='1'   " &
            " ) xxx Left Outer Join TSPL_COMPANY_MASTER ON xxx.Comp_Code=TSPL_COMPANY_MASTER.Comp_Code LEFT OUTER JOIN TSPL_VENDOR_MASTER ON xxx.Vendor_Code=TSPL_VENDOR_MASTER.Vendor_Code where Document_Total<>0  and Convert(Date, DocDate, 103) <=Convert(Date,'02/09/2014', 103)  and Document_Type  in ('I','C','D','AV','OA','P','RC' )  " &
            " ) FINAL where Vendor_Code='" + fndvendorNo.Value + "'  Group By Vendor_Code"
            Dim dt As DataTable
            dt = clsDBFuncationality.GetDataTable(QryToGetOutAmt, trans)
            If dt.Rows IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                OutStandAmt = clsCommon.myCdbl(dt.Rows(0)("Due Amount").ToString())
            End If
            If DoNotCheckAnyValidationOnVendorInactive = True Then
            Else
                If OutStandAmt > 0 AndAlso chkInActive.Checked = True Then
                    Throw New Exception("You can not make this vendor Inactive because it has outstanding amount")
                End If
            End If
            '' Anubhooti 11-Oct-2014 BM00000004240
            Dim VenCHA As String = ""
            Dim csatype As String = ""
            Dim chillingven As String = "0"

            If clsCommon.CompairString(CmbVenType.SelectedValue, "CSA") = CompairStringResult.Equal Then
                VenCHA = clsCommon.myCstr(CmbVenType.SelectedValue)
                csatype = "Y"
            ElseIf clsCommon.CompairString(CmbVenType.SelectedValue, "CHA") = CompairStringResult.Equal Then
                VenCHA = clsCommon.myCstr(CmbVenType.SelectedValue)
            ElseIf clsCommon.CompairString(CmbVenType.SelectedValue, "CV") = CompairStringResult.Equal Then
                VenCHA = clsCommon.myCstr(CmbVenType.SelectedValue)
                chillingven = "1"
            Else
                '' Anubhooti 17-Nov-2014 BM00000004655
                VenCHA = clsCommon.myCstr(CmbVenType.SelectedValue)
            End If
            'If clsCommon.myLen(clsCommon.myCstr(CmbVenType.SelectedValue)) > 0 Then
            '    VenCHA = "'" & clsCommon.myCstr(CmbVenType.SelectedValue) & "'"
            'Else
            '    VenCHA = "Null"
            'End If
            '' Anubhooti 23-Sep-2014 (Only Added TDS_Status,TDS_Vendor_Type,Deduction_Code,TDS_Branch_Code In Update Query)

            'Dim csatype As String = clsCommon.myCstr(IIf(chkcsa.Checked = True, "Y", "N"))
            Dim Other_For_Pan As Integer = 0
            If ChkOther.Checked = True Then
                Other_For_Pan = 1
            Else
                Other_For_Pan = 0
            End If
            Dim DFoption As String = Nothing
            Dim Registered As Integer = 0
            Dim GST_Composition_scheme As Integer = 0
            If AllowGSTApplicable = True Then
                If rbtnDomestic.IsChecked Then
                    DFoption = "Domestic"
                Else
                    DFoption = "Foreign"
                End If
                If Rchkregistered.Checked = True Then
                    Registered = 1
                Else
                    Registered = 0
                    ''update 28/06/2017 Parteek
                    txtGST_PanCode.Text = ""
                    txtEntity.Text = ""
                    txtGSTStateCode.Text = ""
                    txtGSTIN_No_final.Text = ""
                    ''end
                End If
                If RchkCompscheme.Checked Then
                    GST_Composition_scheme = 1
                Else
                    GST_Composition_scheme = 0
                End If
            End If

            connectSql.RunSpTransaction(trans, "sp_TSPL_VENDOR_MASTER_INSERT", New SqlParameter("@Vendor_Code", fndvendorNo.Value), New SqlParameter("@Vendor_Name", txtvendorname.Text.ToString()), New SqlParameter("@Vendor_Group_Code", fndgroupcode.Value), New SqlParameter("Vendor_Group_Des", txtgroupdes.Text.ToString()), New SqlParameter("@Status", strStatus), New SqlParameter("@In_Active_CF", strchkInActiveCF), New SqlParameter("@OnHold", strHold), New SqlParameter("@transporter", strtrans), New SqlParameter("@Closing_Date", Format(Me.dtClosing.Value, "dd/MM/yyyy")), New SqlParameter("@Add1", txtAdd1.Text.ToString()), New SqlParameter("@Add2", txtAdd2.Text.ToString()), New SqlParameter("@Add3", txtAdd3.Text.ToString()), New SqlParameter("@City_Code", fndCity.Value), New SqlParameter("@City_Des", txtCity.Text.ToString()), New SqlParameter("@State", lblStateName.Text.ToString()), New SqlParameter("@Country", txtCountry.Text.ToString()), New SqlParameter("@Phone1", txtPhone1.Text.ToString()), New SqlParameter("@Phone2", txtPhone2.Text.ToString()), New SqlParameter("@Fax", txtfax.Text.ToString()), New SqlParameter("@Email", txtEmail.Text.ToString()), New SqlParameter("@WebSite", txtWeb.Text.ToString()), New SqlParameter("@Contact_Person_Name", txtContactName.Text.ToString()), New SqlParameter("@Contact_Person_Phone", txtContPhone.Text.ToString()), New SqlParameter("@Contact_Person_Fax", txtContactFax.Text.ToString()), New SqlParameter("@Contact_Person_Website", txtContactWeb.Text.ToString()), New SqlParameter("@Contact_Person_Email", txtContactEmail.Text.ToString()), New SqlParameter("@Terms_Code", fndTrmsCode.Value), New SqlParameter("@Terms_Code_Des", txttermcodedes.Text.ToString()), New SqlParameter("@Vendor_Account", fndAccntSet.Value), New SqlParameter("@Vendor_Account_Set_Des", Me.fndAccntSet.Value), New SqlParameter("@Payment_Code", fndPayCode.Value), New SqlParameter("@Payment_Code_Des", txtpaymentcodedes.Text.ToString()), New SqlParameter("@Vendor_Type_Code", fndvendortype.Value), New SqlParameter("@Vendor_Type_Des", txtvendortypedes.Text.ToString()), New SqlParameter("@Bank_Code", fndbankcode.Value), New SqlParameter("@Bank_Code_Des", TxtBankName.Text.ToString()), New SqlParameter("@Service_Tax_No", Me.txtStaxNo.Text), New SqlParameter("@Lst_No", Me.txtLstNo.Text), New SqlParameter("@Tin_No", tin_no), New SqlParameter("@Credit_Limit", CrLimit), New SqlParameter("@Tax_Group", Me.fndTxGrp.Value), New SqlParameter("@Tax_Group_Des", txtTxGrp.Text.ToString()), New SqlParameter("@TAX1", strTax1), New SqlParameter("@TAX1_Rate", strTax1_Rate), New SqlParameter("@TAX2", strTax2), New SqlParameter("@TAX2_Rate", strTax2_Rate), New SqlParameter("@TAX3", strTax3), New SqlParameter("@TAX3_Rate", strTax3_Rate), New SqlParameter("@TAX4", strTax4), New SqlParameter("@TAX4_Rate", strTax4_Rate), New SqlParameter("@TAX5", strTax5), New SqlParameter("@TAX5_Rate", strTax5_Rate), New SqlParameter("@TAX6", strTax6), New SqlParameter("@TAX6_Rate", strTax6_Rate), New SqlParameter("@TAX7", strTax7), New SqlParameter("@TAX7_Rate", strTax7_Rate), New SqlParameter("@TAX8", strTax8), New SqlParameter("@TAX8_Rate", strTax8_Rate), New SqlParameter("@TAX9", strTax9), New SqlParameter("@TAX9_Rate", strTax9_Rate), New SqlParameter("@TAX10", strTax10), New SqlParameter("@TAX10_Rate", strTax10_Rate), New SqlParameter("@Remarks1", txtRemarks1.Text.ToString()), New SqlParameter("@Remarks2", txtRemarks2.Text.ToString()), New SqlParameter("@Additional1", txtAddInfo1.Text.ToString()), New SqlParameter("@Additional2", txtAddInfo2.Text.ToString()), New SqlParameter("@Additional3", txtAddInfo3.Text.ToString()), New SqlParameter("@cst", txtcst.Text.ToString()), New SqlParameter("@ecc", txtecc.Text.ToString()), New SqlParameter("@range", txtrange.Text.ToString()), New SqlParameter("@collectorate", txtcollect.Text.ToString()), New SqlParameter("@pan", txtpan.Text.ToString()), New SqlParameter("@Created_By", userCode), New SqlParameter("@Created_Date", connectSql.serverDate(trans)), New SqlParameter("@Modify_By", userCode), New SqlParameter("@Modify_Date", connectSql.serverDate(trans)), New SqlParameter("@Comp_Code", companyCode), New SqlParameter("@is_Gross_Receipt", IsGrossReceipt), New SqlParameter("@Inter_branch", strInterBranch), New SqlParameter("@Branch_Name ", TxtBankBranch.Text.ToString()), New SqlParameter("@Account_No ", TxtAccNo.Text.ToString()), New SqlParameter("@Bank_Name ", TxtBankName.Text.ToString()), New SqlParameter("@IFSC_Code ", TxtBankBranch.Value.ToString()), New SqlParameter("@Account_Type ", clsCommon.myCstr(cmbAccountType.SelectedValue)), New SqlParameter("@Vendor_Type ", clsCommon.myCstr(cmbTypeOfVen.SelectedValue)))
            Dim State As String
            If clsCommon.myLen(clsCommon.myCstr(txttdsstate.Value)) > 0 Then
                State = "'" & clsCommon.myCstr(txttdsstate.Value) & "'"
            ElseIf clsCommon.myLen(clsCommon.myCstr(txtState.Value)) > 0 Then
                State = "'" & clsCommon.myCstr(txtState.Value) & "'"
            Else
                State = "NULL"
            End If

            updateRemainingColmns(trans, IS_TDS_App, State, DFoption, Registered, GST_Composition_scheme, VenCHA, IsVendorInvoiceNo, Other_For_Pan, chillingven, csatype, strTagAsFranchise)









            If clsCommon.myLen(txtCategoryStructureCode.Value) > 0 Then
                SaveVendorCategory()
            End If
            'myMessages.insert()
            btnsave.Text = "Update"
            btndelete.Enabled = True

            UcAttachment1.SaveData(fndvendorNo.Value, False, trans)
            'If userCode <> "ADMIN" Then
            '    If funSetUserAccess() = False Then Exit Sub
            'End If
            isLoadCopy = False
            '' Anubhooti 01-Sep-2014 Call LoadData
            'fndvendorNo_text_changed(AutoVendorCode)
        Catch ex As Exception

            'myMessages.myExceptions(ex)
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Public Sub SaveVendorCategory()
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try

            Dim isSaved As Boolean = True
            Dim qry As String = "delete from TSPL_VENDOR_CATEGORY_MASTER where Vendor_code='" + clsCommon.myCstr(fndvendorNo.Value) + "' and Category_Struct_Code='" + clsCommon.myCstr(txtCategoryStructureCode.Value) + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            For Each grow As GridViewRowInfo In gvCategory.Rows
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Vendor_code", clsCommon.myCstr(fndvendorNo.Value))
                clsCommon.AddColumnsForChange(coll, "Category_Struct_Code", clsCommon.myCstr(txtCategoryStructureCode.Value))
                clsCommon.AddColumnsForChange(coll, "Category_Code", clsCommon.myCstr(grow.Cells(CatcolCode).Value))
                clsCommon.AddColumnsForChange(coll, "Category_Code_Values", clsCommon.myCstr(grow.Cells(CatcolValue).Value))

                If clsCommon.myLen(grow.Cells(CatcolCode).Value) > 0 Then
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_VENDOR_CATEGORY_MASTER", OMInsertOrUpdate.Insert, "", trans)
                End If
            Next

            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Sub
    'This function for updation
    Public Sub funupdate(ByVal trans As SqlTransaction)
        'Dim trans As SqlTransaction = Nothing
        Try
            'trans = clsDBFuncationality.GetTransactin()
            '' Anubhooti 17-Nov-2014 BM00000004655
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, fndvendorNo.Value, "TSPL_VENDOR_MASTER", "Vendor_Code", trans)

            Dim closingdate As String = ""
            Dim strStatus As String = ""
            If chkInActive.Checked = True Then
                strStatus = "Y"                    '******* for:In-Active ********
                closingdate = Format(dtClosing.Value, "dd/MM/yyyy")
            ElseIf chkInActive.Checked = False Then
                strStatus = "N"                    '******* for:Active ******** 
                closingdate = connectSql.serverDate(trans)
            End If

            Dim strTagAsFranchise As String
            If chkTagAsFranchise.Checked = True Then
                strTagAsFranchise = "Y"  '''''   for taging vendor as franchise
            Else
                strTagAsFranchise = "N" '''''   for untaging vendor as franchise
            End If

            Dim strchkInActiveCF As String = ""
            If InActiveCF.Checked = True Then
                strchkInActiveCF = "Y"  '''''   Actice for Cattle Feed
            Else
                strchkInActiveCF = "N" '''''  InActive for Cattle Feed
            End If

            Dim strHold As String = ""
            If chkHold.Checked = True Then
                strHold = "Y"                      '******* for:Hold ******** 
            ElseIf chkHold.Checked = False Then
                strHold = "N"                      '******* for:Remove Hold ********
            End If

            Dim IsVendorInvoiceNo As Integer = 0
            If chkVendorInvoiceNo.Checked Then
                IsVendorInvoiceNo = 1
            Else
                IsVendorInvoiceNo = 0
            End If


            Dim strInterBranch As String
            If chkInterBranch.Checked = True Then
                strInterBranch = "Y"
            Else
                strInterBranch = "N"
            End If

            Dim strtrans As String = ""
            Dim IsTransportor As String = String.Empty
            IsTransportor = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Transporter  From TSPL_VENDOR_MASTER Where Vendor_Code ='" + fndvendorNo.Value + "'", trans))
            If chktrarns.Checked = True Then
                strtrans = "Y"
            ElseIf chktrarns.Checked = False Then
                strtrans = "N"
                If clsCommon.CompairString(IsTransportor, "Y") = CompairStringResult.Equal Then
                    If common.clsCommon.MyMessageBoxShow("Do you want to delete this Vendor from Transport Master?", "", MessageBoxButtons.YesNo, RadMessageIcon.Question) = DialogResult.Yes Then
                        strtrans = "N"                      '******* for:Remove Non Transporter type ********
                        clsDBFuncationality.ExecuteNonQuery("Delete from tspl_transport_Master Where Transport_Id='" + fndvendorNo.Value + "'", trans)
                    Else
                        strtrans = "Y"
                    End If
                End If
            End If


            Dim strTax1 As String = ""
            Dim strTax1_Rate As Decimal = 0.0

            Dim strTax2 As String = ""
            Dim strTax2_Rate As Decimal = 0.0

            Dim strTax3 As String = ""
            Dim strTax3_Rate As Decimal = 0.0

            Dim strTax4 As String = ""
            Dim strTax4_Rate As Decimal = 0.0

            Dim strTax5 As String = ""
            Dim strTax5_Rate As Decimal = 0.0

            Dim strTax6 As String = ""
            Dim strTax6_Rate As Decimal = 0.0

            Dim strTax7 As String = ""
            Dim strTax7_Rate As Decimal = 0.0

            Dim strTax8 As String = ""
            Dim strTax8_Rate As Decimal = 0.0

            Dim strTax9 As String = ""
            Dim strTax9_Rate As Decimal = 0.0

            Dim strTax10 As String = ""
            Dim strTax10_Rate As Decimal = 0.0


            'Dim Bal1 As Decimal
            Dim CrLimit As Decimal
            'Dim OutComm As Decimal

            Dim IS_TDS_App As Integer
            If ChkIsTDSApp.Checked = True Then
                IS_TDS_App = 1
            ElseIf ChkIsTDSApp.Checked = False Then
                IS_TDS_App = 0
            End If

            'Format(Me.dtClosing.Value, "dd/MM/yyyy")
            If txtCredit.Text = "" Then
                CrLimit = Convert.ToDecimal("0.00")
            Else
                CrLimit = Convert.ToDecimal(txtCredit.Text)
            End If

            Dim TDSQry As String = ""
            '' Anubhooti 26-Aug-2014 BM00000003619 (Only Added Pin_Code In Update Query After Procedure Exection)
            '' Anubhooti 10-Oct-2014 BM00000004198 (Removed Gross Receipt)
            Dim IsGrossReceipt As Integer
            'Dim IsGrossReceipt As Integer = IIf(chkIsGrossReceipt.Checked, 1, 0)
            '' Anubhooti 1-Aug-2104 (Added Four Columns)

            '' ******************* Check Outstanding Amount Of vendor *************
            '' Anubhooti 01-Sep-2014 BM00000003425
            Dim QryToGetOutAmt As String = ""
            Dim OutStandAmt As Double = 0

            QryToGetOutAmt = "Select Vendor_Code, MAX(Vendor_Name) as Vendor_Name, SUM([Due Amount]) as [Due Amount] from (" &
            " select xxx.Vendor_Code, TSPL_VENDOR_MASTER.Vendor_Name, xxx.DocNo as [Document Id], Case when Document_Type IN ('D','AV','OA') then [Document_Total]*-1 Else [Document_Total] End as [Due Amount], xxx.DocDate as [Document Date], case when Document_Type='I' then 'IN' when Document_Type='PY' then 'PY' when Document_Type='D' then 'DR' when Document_Type='C' then 'CR' when Document_Type='AV' then 'AV' when Document_Type='OA' then 'OA'when Document_Type='PY' then 'PY' when Document_Type='RC' then 'RC' end as [Document_Type] FROM (   " &
            " select Vendor_code, Document_No as DocNo , Document_Type , COnvert(Date,Invoice_Entry_Date, 103) as DocDate , (Case When TSPL_VENDOR_INVOICE_HEAD.is_For_TDS=1 Then TSPL_VENDOR_INVOICE_HEAD.Document_Total Else TSPL_VENDOR_INVOICE_HEAD.Document_Total-TDS_Actual_Amount-ISNULL((Select Balance_Amt from TSPL_VENDOR_INVOICE_HEAD as VH Where VH.Vendor_Code=TSPL_VENDOR_INVOICE_HEAD.Vendor_Code AND VH.Vendor_Invoice_No=TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_No AND VH.Document_Type IN ('D','C') AND ISNULL(VH.Against_PurchaseReturn_No,'')<>''),0) End)-ISNULL((Select SUM(Applied_Amount) from TSPL_PAYMENT_DETAIL LEFT OUTER JOIN TSPL_PAYMENT_HEADER ON TSPL_PAYMENT_HEADER.Payment_No=TSPL_PAYMENT_DETAIL.Payment_No Where TSPL_PAYMENT_HEADER.Posted=1 AND TSPL_PAYMENT_HEADER.IsChkReverse<>'Y' AND TSPL_PAYMENT_DETAIL.Document_No=TSPL_VENDOR_INVOICE_HEAD.Document_No AND  CONVERT(DATE,TSPL_PAYMENT_HEADER.Payment_Date,103)<=CONVERT(DATE,'02/09/2014',103)),0) as [Document_Total], TSPL_VENDOR_INVOICE_HEAD.Comp_Code, TSPL_VENDOR_INVOICE_HEAD.Loc_Code AS Location  from TSPL_VENDOR_INVOICE_HEAD  where ISNULL(tspl_vendor_invoice_head.Posting_Date,'')<> '' And (Len(ISNULL(Against_POInvoice_No, ''))<=0 AND Len(ISNULL(Against_PurchaseReturn_No, ''))<=0) or Document_Type='I'  and TSPL_VENDOR_INVOICE_HEAD.Posting_Date <> ''   " &
            " UNION ALL  " &
            " select  Vendor_code, TSPL_PAYMENT_HEADER.Payment_No as DocNo ,TSPL_PAYMENT_HEADER.Payment_Type as Document_Type , Convert(Date,Payment_Date, 103) as DocDate , Payment_Amount+TDS_Amount-isnull((case when TSPL_PAYMENT_HEADER.Payment_Type in('AV','OA','RC') then (select Amount from TSPL_BANK_REVERSE where Source_Type='AP' and Document_No=TSPL_PAYMENT_HEADER.Payment_No AND TSPL_BANK_REVERSE.Post<>'N' AND TSPL_BANK_REVERSE.Reversal_Date<=Convert(Date,'02/09/2014', 103)) else 0 end ),0) as [Document_Total], TSPL_PAYMENT_HEADER.Comp_Code, RIGHT(TSPL_BANK_MASTER.BANKACC , 3) as Location  from TSPL_PAYMENT_HEADER  LEFT OUTER JOIN TSPL_BANK_MASTER ON TSPL_PAYMENT_HEADER.Bank_Code= TSPL_BANK_MASTER.BANK_CODE where TSPL_PAYMENT_HEADER.Payment_Type NOT IN ('PY','MI') And (Posted='P' or Posted='1')    " &
            " UNION ALL   " &
            " select  VC_Code as Vendor_code, Document_No as DocNo, case when Amount_Type='Cr' then 'D' else  'C' end as Document_Type,CONVERT(Date,TSPL_VCGL_Head.Document_Date,103) as DocDate, Amount as [Document_Total], TSPL_VCGL_Head.Comp_Code ,  TSPL_VCGL_Head.Location_Segment as Location  from TSPL_VCGL_Head  where Document_Type='v' and TSPL_VCGL_Head.Status='1'    " &
            " UNION ALL   " &
            " select  TSPL_VCGL_Detail.VCGL_Code  as Vendor_code, TSPL_VCGL_Detail.Document_No as DocNo, case when TSPL_VCGL_Detail.Dr_Amount=0.0 then 'C' when TSPL_VCGL_Detail.Cr_Amount=0.0 then 'D' end as Document_Type, CONVERT(Date,TSPL_VCGL_Head.Document_Date,103) as DocDate, case when TSPL_VCGL_Detail.Dr_Amount=0.0 then TSPL_VCGL_Detail.Cr_Amount when TSPL_VCGL_Detail.Cr_Amount=0.0 then TSPL_VCGL_Detail.Dr_Amount end as [Document_Total], TSPL_VCGL_Head.Comp_Code ,  TSPL_VCGL_Head.Location_Segment as Location from  TSPL_VCGL_Detail  left outer join  TSPL_VCGL_Head on TSPL_VCGL_Head.Document_No=TSPL_VCGL_Detail.Document_No    where Row_Type='Vendor'  and TSPL_VCGL_Head.Status='1'   " &
            " ) xxx Left Outer Join TSPL_COMPANY_MASTER ON xxx.Comp_Code=TSPL_COMPANY_MASTER.Comp_Code LEFT OUTER JOIN TSPL_VENDOR_MASTER ON xxx.Vendor_Code=TSPL_VENDOR_MASTER.Vendor_Code where Document_Total<>0  and Convert(Date, DocDate, 103) <=Convert(Date,'02/09/2014', 103)  and Document_Type  in ('I','C','D','AV','OA','P','RC' )  " &
            " ) FINAL where Vendor_Code='" + fndvendorNo.Value + "'  Group By Vendor_Code"
            Dim dt As DataTable
            dt = clsDBFuncationality.GetDataTable(QryToGetOutAmt, trans)
            If dt.Rows IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                OutStandAmt = clsCommon.myCdbl(dt.Rows(0)("Due Amount").ToString())
            End If
            If DoNotCheckAnyValidationOnVendorInactive = True Then
            Else
                If OutStandAmt > 0 AndAlso chkInActive.Checked = True Then
                    Throw New Exception("You can not make this vendor Inactive because it has outstanding amount")
                End If
            End If
            '' ******************* Check Outstanding Amount Of vendor *************
            '' Anubhooti 11-Oct-2014 BM00000004240
            Dim VenCHA As String = ""
            Dim csatype As String = "N"
            Dim chillingven As String = "0"

            If clsCommon.CompairString(CmbVenType.SelectedValue, "CSA") = CompairStringResult.Equal Then
                VenCHA = clsCommon.myCstr(CmbVenType.SelectedValue)
                csatype = "Y"
            ElseIf clsCommon.CompairString(CmbVenType.SelectedValue, "CHA") = CompairStringResult.Equal Then
                VenCHA = clsCommon.myCstr(CmbVenType.SelectedValue)
            ElseIf clsCommon.CompairString(CmbVenType.SelectedValue, "CV") = CompairStringResult.Equal Then
                VenCHA = clsCommon.myCstr(CmbVenType.SelectedValue)
                chillingven = "1"
            Else
                VenCHA = clsCommon.myCstr(CmbVenType.SelectedValue)
            End If

            Dim State As String
            If clsCommon.myLen(clsCommon.myCstr(txttdsstate.Value)) > 0 Then
                State = "'" & clsCommon.myCstr(txttdsstate.Value) & "'"
            ElseIf clsCommon.myLen(clsCommon.myCstr(txtState.Value)) > 0 Then
                State = "'" & clsCommon.myCstr(txtState.Value) & "'"
            Else
                State = "NULL"
            End If
            Dim Other_For_Pan As Integer = 0
            If ChkOther.Checked = True Then
                Other_For_Pan = 1
            Else
                Other_For_Pan = 0
            End If

            Dim DFoption As String = Nothing
            Dim Registered As Integer = 0
            Dim GST_Composition_scheme As Integer = 0
            If AllowGSTApplicable = True Then
                If rbtnDomestic.IsChecked Then
                    DFoption = "Domestic"
                Else
                    DFoption = "Foreign"
                End If
                If Rchkregistered.Checked = True Then
                    Registered = 1
                Else
                    Registered = 0
                End If
                If RchkCompscheme.Checked Then
                    GST_Composition_scheme = 1
                Else
                    GST_Composition_scheme = 0
                End If
            End If


            clsDBFuncationality.SaveAStorePorcedure(trans, "sp_TSPL_VENDOR_MASTER_UPDATE", New SqlParameter("@Vendor_Code", fndvendorNo.Value), New SqlParameter("@Vendor_Name", txtvendorname.Text.ToString()), New SqlParameter("@Vendor_Group_Code", fndgroupcode.Value), New SqlParameter("Vendor_Group_Des", txtgroupdes.Text.ToString()), New SqlParameter("@Status", strStatus), New SqlParameter("@In_Active_CF", strchkInActiveCF), New SqlParameter("@OnHold", strHold), New SqlParameter("@transporter", strtrans), New SqlParameter("@Closing_Date", closingdate), New SqlParameter("@Add1", txtAdd1.Text.ToString()), New SqlParameter("@Add2", txtAdd2.Text.ToString()), New SqlParameter("@Add3", txtAdd3.Text.ToString()), New SqlParameter("@City_Code", fndCity.Value), New SqlParameter("@City_Des", txtCity.Text.ToString()), New SqlParameter("@State", lblStateName.Text.ToString()), New SqlParameter("@Country", txtCountry.Text.ToString()), New SqlParameter("@Phone1", txtPhone1.Text.ToString()), New SqlParameter("@Phone2", txtPhone2.Text.ToString()), New SqlParameter("@Fax", txtfax.Text.ToString()), New SqlParameter("@Email", txtEmail.Text.ToString()), New SqlParameter("@WebSite", txtWeb.Text.ToString()), New SqlParameter("@Contact_Person_Name", txtContactName.Text.ToString()), New SqlParameter("@Contact_Person_Phone", txtContPhone.Text.ToString()), New SqlParameter("@Contact_Person_Fax", txtContactFax.Text.ToString()), New SqlParameter("@Contact_Person_Website", txtContactWeb.Text.ToString()), New SqlParameter("@Contact_Person_Email", txtContactEmail.Text.ToString()), New SqlParameter("@Terms_Code", fndTrmsCode.Value), New SqlParameter("@Terms_Code_Des", txttermcodedes.Text.ToString()), New SqlParameter("@Vendor_Account", fndAccntSet.Value), New SqlParameter("@Vendor_Account_Set_Des", fndAccntSet.Value), New SqlParameter("@Payment_Code", fndPayCode.Value), New SqlParameter("@Payment_Code_Des", txtpaymentcodedes.Text.ToString()), New SqlParameter("@Vendor_Type_Code", fndvendortype.Value), New SqlParameter("@Vendor_Type_Des", txtvendortypedes.Text.ToString()), New SqlParameter("@Bank_Code", fndbankcode.Value), New SqlParameter("@Bank_Code_Des", TxtBankName.Text.ToString()), New SqlParameter("@Service_Tax_No", Me.txtStaxNo.Text), New SqlParameter("@Lst_No", Me.txtLstNo.Text), New SqlParameter("@Tin_No", Me.txtTinNo.Text), New SqlParameter("@Credit_Limit", CrLimit), New SqlParameter("@Tax_Group", Me.fndTxGrp.Value), New SqlParameter("@Tax_Group_Des", txtTxGrp.Text.ToString()), New SqlParameter("@TAX1", strTax1), New SqlParameter("@TAX1_Rate", strTax1_Rate), New SqlParameter("@TAX2", strTax2), New SqlParameter("@TAX2_Rate", strTax2_Rate), New SqlParameter("@TAX3", strTax3), New SqlParameter("@TAX3_Rate", strTax3_Rate), New SqlParameter("@TAX4", strTax4), New SqlParameter("@TAX4_Rate", strTax4_Rate), New SqlParameter("@TAX5", strTax5), New SqlParameter("@TAX5_Rate", strTax5_Rate), New SqlParameter("@TAX6", strTax6), New SqlParameter("@TAX6_Rate", strTax6_Rate), New SqlParameter("@TAX7", strTax7), New SqlParameter("@TAX7_Rate", strTax7_Rate), New SqlParameter("@TAX8", strTax8), New SqlParameter("@TAX8_Rate", strTax8_Rate), New SqlParameter("@TAX9", strTax9), New SqlParameter("@TAX9_Rate", strTax9_Rate), New SqlParameter("@TAX10", strTax10), New SqlParameter("@TAX10_Rate", strTax10_Rate), New SqlParameter("@Remarks1", txtRemarks1.Text.ToString()), New SqlParameter("@Remarks2", txtRemarks2.Text.ToString()), New SqlParameter("@Additional1", txtAddInfo1.Text.ToString()), New SqlParameter("@Additional2", txtAddInfo2.Text.ToString()), New SqlParameter("@Additional3", txtAddInfo3.Text.ToString()), New SqlParameter("@cst", txtcst.Text.ToString()), New SqlParameter("@ecc", txtecc.Text.ToString()), New SqlParameter("@range", txtrange.Text.ToString()), New SqlParameter("@collectorate", txtcollect.Text.ToString()), New SqlParameter("@pan", txtpan.Text.ToString()), New SqlParameter("@Modify_By", userCode), New SqlParameter("@Modify_Date", connectSql.serverDate(trans)), New SqlParameter("@Comp_Code", companyCode), New SqlParameter("@is_Gross_Receipt", IsGrossReceipt), New SqlParameter("@InterBranch ", strInterBranch), New SqlParameter("@Branch_Name ", TxtBankBranch.Text.ToString()), New SqlParameter("@Account_No ", TxtAccNo.Text.ToString()), New SqlParameter("@Bank_Name ", TxtBankName.Text.ToString()), New SqlParameter("@IFSC_Code ", TxtBankBranch.Value.ToString()), New SqlParameter("@Account_Type ", clsCommon.myCstr(cmbAccountType.SelectedValue)), New SqlParameter("@Vendor_Type ", clsCommon.myCstr(cmbTypeOfVen.SelectedValue)))




            updateRemainingColmns(trans, IS_TDS_App, State, DFoption, Registered, GST_Composition_scheme, VenCHA, IsVendorInvoiceNo, Other_For_Pan, chillingven, csatype, strTagAsFranchise)








            'trans.Commit()

            If clsCommon.myLen(txtCategoryStructureCode.Value) > 0 Then
                SaveVendorCategory()
            End If

            'myMessages.update()

            UcAttachment1.SaveData(fndvendorNo.Value, False, trans)
            isLoadCopy = False
            ' fndvendorNo_text_changed(fndvendorNo.Value)
        Catch ex As Exception
            'trans.Rollback()
            'myMessages.myExceptions(ex)
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Sub updateRemainingColmns(ByVal trans As SqlTransaction, ByVal IS_TDS_App As Integer, ByVal State As String, ByVal DFoption As String, ByVal Registered As Integer, ByVal GST_Composition_scheme As Integer, ByVal VenCHA As String, ByVal IsVendorInvoiceNo As Integer, ByVal Other_For_Pan As Integer, ByVal chillingven As String, ByVal csatype As String, ByVal strTagAsFranchise As String)

        If (clsCommon.myCBool(chkDefaultGrower.Checked) = True) Then
            Dim qry As String = "update TSPL_VENDOR_MASTER set Is_Default_Grower=0 where Is_Default_Grower=1"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
        End If

        Dim strCmd11 As String
        strCmd11 = "Update TSPL_VENDOR_MASTER set IsTCSnotApplicable='" + IIf(chkTCSNotApplicable.Checked, "1", "0") + "', SSI_No='" + clsCommon.myCstr(txt_ssino.Text) + "',Is_Blacklist='" + clsCommon.myCstr(IIf(clsCommon.myCBool(chk_isblacklist.Checked) = True, 1, 0)) + "',IsEmployee='" + clsCommon.myCstr(IIf(clsCommon.myCBool(chkIsEmployee.Checked) = True, 1, 0)) + "', OldName='" & clsCommon.myCstr(TxtOldname.Text) & "', Industry_Type='" & clsCommon.myCstr(ddl_industrytype.SelectedValue.ToString()) & "', Vendor_Distance='" & clsCommon.myCstr(txtVendorDistance.Text) & "',PC_CODE=" & IIf(clsCommon.myLen(fndpaymentCycle.Value) <= 0, "null", "'" & fndpaymentCycle.Value & "'") + ",CHA_DOC_NO='" & clsCommon.myCstr(fndCHA_Code.Value) & "',branch_code='" & clsCommon.myCstr(TxtBankBranch.Value) & "',Branch_Name='" & clsCommon.myCstr(txtbranchname.Text) & "',Pin_Code='" + txtPinCode.Text + "',  franchise_yn='" & strTagAsFranchise & "',Form_Type='" + txtvndrtype.Text + "',state_code=" + State + ",country_code='" + txtcountrycode.Value + "',Is_Parent_Vendor='" + clsCommon.myCstr(IIf(chkparentvendor.Checked, 1, 0)) + "',Parent_Vendor_Code='" + clsCommon.myCstr(fndparent.Value) + "',Category_Struct_Code='" + clsCommon.myCstr(txtCategoryStructureCode.Value) + "',Is_Chilling_Vendor='" + chillingven + "',csa_type='" + csatype + "',Vendor_Type_CHA='" & VenCHA & "',IsVendorInvoiceNo=" & IsVendorInvoiceNo & ",Other_For_Pan=" & clsCommon.myCstr(Other_For_Pan) & ",Isbuyerfilereturninlasttwoyears='" + clsCommon.myCstr(IIf(clsCommon.myCBool(chkbuyerfilereturnlasttwoyear.Checked) = True, 1, 0)) + "',IsTCS_TDSamountgreaterthan50KpreviousYear='" + clsCommon.myCstr(IIf(clsCommon.myCBool(chkTCSTDSamountgreater50KpreviousYear.Checked) = True, 1, 0)) + "',IsAllowSkipPurchaseQC='" + clsCommon.myCstr(IIf(clsCommon.myCBool(chkIsAllowSkipPurchaseQC.Checked) = True, 1, 0)) + "',Is_Provisional='" + clsCommon.myCstr(IIf(clsCommon.myCBool(chkProvisional.Checked) = True, 1, 0)) + "',Is_Default_Grower='" + clsCommon.myCstr(IIf(clsCommon.myCBool(chkDefaultGrower.Checked) = True, 1, 0)) + "'  where Vendor_Code='" + fndvendorNo.Value + "'"
        clsDBFuncationality.ExecuteNonQuery(strCmd11, trans)
        Dim TDSState As String = ""

        If clsCommon.myLen(clsCommon.myCstr(txttdsstate.Value)) > 0 Then
            TDSState = "'" & clsCommon.myCstr(txttdsstate.Value) & "'"
        Else
            TDSState = "Null"
        End If
        Dim TDSBranch As String = ""
        If clsCommon.myLen(clsCommon.myCstr(fndbranchnew.Value)) > 0 Then
            TDSBranch = "'" & clsCommon.myCstr(fndbranchnew.Value) & "'"
        Else
            TDSBranch = "Null"
        End If

        Dim TDSBranchService As String = ""
        If clsCommon.myLen(clsCommon.myCstr(fndbranchnewService.Value)) > 0 Then
            TDSBranchService = "'" & clsCommon.myCstr(fndbranchnewService.Value) & "'"
        Else
            TDSBranchService = "Null"
        End If

        Dim PartyDetailsQry As String
        Dim DBEntry As Double
        Dim TDSQry As String

        Dim StateService As String
        If clsCommon.myLen(clsCommon.myCstr(txttdsstateService.Value)) > 0 Then
            StateService = "'" & clsCommon.myCstr(txttdsstateService.Value) & "'"
        ElseIf clsCommon.myLen(clsCommon.myCstr(txtState.Value)) > 0 Then
            StateService = "'" & clsCommon.myCstr(txtState.Value) & "'"
        Else
            StateService = "NULL"
        End If

        DBEntry = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select COUNT(*) As Row From TSPL_TDS_VENDOR_DETAILS Where Vendor_Code ='" & clsCommon.myCstr(fndvendorNo.Value) & "'", trans))
        If ChkIsTDSApp.Checked = True Then
            Dim strDeductionCodeService As String = "Null"
            If clsCommon.myLen(fnddeducNewService.Value) > 0 Then
                strDeductionCodeService = "'" & clsCommon.myCstr(fnddeducNewService.Value) & "'"
            End If

            TDSQry = "Update TSPL_VENDOR_MASTER set Is_TDS_Applicable= " & IS_TDS_App & ",TDS_State_Code=" & State & ",TDS_Status= '" & clsCommon.myCstr(ddlstatus.Text) & "', TDS_Vendor_Type= '" & clsCommon.myCstr(ddlventype.Text) & "', Deduction_Code= '" & clsCommon.myCstr(fnddeducNew.Value) & "',TDS_Branch_Code=" & TDSBranch & " " +
            ",TDS_State_Code_Service=" & StateService & ",TDS_Status_Service= '" & clsCommon.myCstr(ddlstatusService.Text) & "', TDS_Vendor_Type_Service= '" & clsCommon.myCstr(ddlventypeService.Text) & "', Deduction_Code_Service= " + strDeductionCodeService + ",TDS_Branch_Code_Service=" & TDSBranchService & " " +
            " where Vendor_Code='" + fndvendorNo.Value + "'"

            If DBEntry = 0 Then
                clsDBFuncationality.SaveAStorePorcedure(trans, "SP_TDS_VENDOR_DETAILS_INSERT", New SqlParameter("@Vendor_Code", fndvendorNo.Value), New SqlParameter("@Nature_Of_Deduction", clsCommon.myCstr(fnddeducNew.Value)), New SqlParameter("@State_Code", State.Replace("'", "")), New SqlParameter("@Pan", clsCommon.myCstr(txtpan.Text)), New SqlParameter("@Vendor_Type", clsCommon.myCstr(ddlventype.Text)), New SqlParameter("@status", clsCommon.myCstr(ddlstatus.Text)), New SqlParameter("@Branch_Code", TDSBranch.Replace("'", "")), New SqlParameter("@Inactive", "N"), New SqlParameter("@createdby", userCode), New SqlParameter("@createddate", connectSql.serverDate(trans)), New SqlParameter("@modifiedby", userCode), New SqlParameter("@modifieddate", connectSql.serverDate(trans)), New SqlParameter("@compcode", companyCode))
                PartyDetailsQry = "UPDATE TSPL_TDS_VENDOR_DETAILS SET State_Code=" & State & ",Branch_Code=" & TDSBranch & " where Vendor_Code='" + fndvendorNo.Value + "'"
            Else
                PartyDetailsQry = "UPDATE TSPL_TDS_VENDOR_DETAILS SET Nature_Of_Deduction='" & clsCommon.myCstr(fnddeducNew.Value) & "',State_Code=" & State & ",Vendor_TYpe='" & clsCommon.myCstr(ddlventype.Text) & "',Status='" & clsCommon.myCstr(ddlstatus.Text) & "',Branch_Code=" & TDSBranch & ",Pan='" & clsCommon.myCstr(txtpan.Text) & "' where Vendor_Code='" + fndvendorNo.Value + "'"
            End If
        Else
            TDSQry = "Update TSPL_VENDOR_MASTER set Is_TDS_Applicable= " & IS_TDS_App & ",TDS_State_Code=null, TDS_Status= null, TDS_Vendor_Type= null, Deduction_Code= null,TDS_Branch_Code=null " +
            ",TDS_State_Code_Service=null,TDS_Status_Service= null,TDS_Vendor_Type_Service= null, Deduction_Code_Service= null,TDS_Branch_Code_Service=null " +
            "where Vendor_Code='" + fndvendorNo.Value + "'"
            PartyDetailsQry = "UPDATE TSPL_TDS_VENDOR_DETAILS SET Nature_Of_Deduction=null,State_Code=null,Vendor_TYpe='Individual',Status='Resident',Branch_Code='' where Vendor_Code='" + fndvendorNo.Value + "'"
        End If
        connectSql.RunSqlTransaction(trans, TDSQry)
        If clsCommon.myLen(PartyDetailsQry) > 0 Then
            connectSql.RunSqlTransaction(trans, PartyDetailsQry)
        End If

        For i As Integer = 1 To grdTax.Rows.Count
            Dim strTax As String = Convert.ToString("Tax" & Convert.ToString(i))
            Dim Tax As String = grdTax.Rows(i - 1).Cells(0).Value
            Dim strTaxRate As String = Convert.ToString("Tax" & Convert.ToString(i) & "_Rate")
            Dim Tax_Rate As Decimal = Convert.ToDecimal(grdTax.Rows(i - 1).Cells(1).Value)
            Dim strCmd As String
            strCmd = "Update TSPL_VENDOR_MASTER set " + strTax + "='" + Tax + "'," + strTaxRate + "='" + Tax_Rate.ToString() + "' where Vendor_Code='" + fndvendorNo.Value + "'"
            clsDBFuncationality.ExecuteNonQuery(strCmd, trans)
        Next

        '' Anubhooti 06-Oct-2014 BM00000003996
        If clsCommon.myLen(txtAliesName.Text) > 0 Then
            Dim AliesQry As String = "Update TSPL_VENDOR_MASTER set Alies_Name='" & clsCommon.myCstr(txtAliesName.Text) & "' where Vendor_Code='" + fndvendorNo.Value + "'"
            clsDBFuncationality.ExecuteNonQuery(AliesQry, trans)
        Else
            Dim AliesQry1 As String = "Update TSPL_VENDOR_MASTER set Alies_Name='' where Vendor_Code='" + fndvendorNo.Value + "'"
            clsDBFuncationality.ExecuteNonQuery(AliesQry1, trans)
        End If
        ''
        '' Anubhooti 06-Jan-2014 (Auto TC Certified)
        If ChkTCCertified.Checked = True Then
            Dim TCCertifiedQry As String = "Update TSPL_VENDOR_MASTER set Is_TC_Certified=1, TC_Certified='" & clsCommon.myCstr(TxtTCCertified.Text) & "' where Vendor_Code='" + fndvendorNo.Value + "'"
            clsDBFuncationality.ExecuteNonQuery(TCCertifiedQry, trans)
        Else
            Dim TCCertifiedQry1 As String = "Update TSPL_VENDOR_MASTER set Is_TC_Certified=0, TC_Certified=NULL where Vendor_Code='" + fndvendorNo.Value + "'"
            clsDBFuncationality.ExecuteNonQuery(TCCertifiedQry1, trans)
        End If
        'If clsCommon.myLen(Me.txtChequeInFavour.Text) > 0 Then
        '    Dim streq As String = "Update TSPL_VENDOR_MASTER set Cheque_In_Favour_Of ='" & clsCommon.myCstr(Me.txtChequeInFavour.Text) & "' where Vendor_Code='" + fndvendorNo.Value + "'"
        '    clsDBFuncationality.ExecuteNonQuery(streq, trans)
        'End If
        If clsCommon.myLen(txtChequeInFavour.Text) > 0 Then
            Dim streq As String = "Update TSPL_VENDOR_MASTER set Cheque_In_Favour_Of='" & clsCommon.myCstr(txtChequeInFavour.Text) & "' where Vendor_Code='" + fndvendorNo.Value + "'"
            clsDBFuncationality.ExecuteNonQuery(streq, trans)
        Else
            Dim streq As String = "Update TSPL_VENDOR_MASTER set Cheque_In_Favour_Of= '" + txtvendorname.Text + "'+'-' + '" + TxtBankName.Text + "' +'-' + '" + TxtAccNo.Text + "' where Vendor_Code='" + fndvendorNo.Value + "'"
            clsDBFuncationality.ExecuteNonQuery(streq, trans)
        End If

        ''---GST Added
        If AllowGSTApplicable = True Then
            Dim streq As String = "Update TSPL_VENDOR_MASTER set DFOption= '" + DFoption + "' , BusinessCondition='" & rdrpbusiness.SelectedValue & "',GSTRegistered='" & Registered & "',GST_Composition_scheme='" & GST_Composition_scheme & "',GSTEntity='" & txtEntity.Text & "',GSTLastEntity='" & MyTextBox2.Text & "',GSTFinalNo='" & txtGSTIN_No_final.Text & "',GSTMiddle='" & txtFixxed.Text & "' where Vendor_Code='" + fndvendorNo.Value + "'"
            clsDBFuncationality.ExecuteNonQuery(streq, trans)
        End If
        ''----ENd

        ''---- Added by Parteek Weight
        Dim strqry As String = ""
        strqry = " update TSPL_VENDOR_MASTER set OEM=" + IIf(chkOEM.Checked, "1", "0") + " ,Weight='" & clsCommon.myCdbl(txtWeight.Text) & "', JWPriceCode='" & txtJWPriceCode.Value & "' where Vendor_Code='" + fndvendorNo.Value + "'"
        clsDBFuncationality.ExecuteNonQuery(strqry, trans)
        ''-------End

        ''

        ''For Custom Fields
        Dim arrCustomFields As List(Of clsCustomFieldValues) = New List(Of clsCustomFieldValues)
        If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
            UcCustomFields1.GetData(arrCustomFields)
        End If

        clsCustomFieldValues.SaveData(MyBase.Form_ID, fndvendorNo.Value, arrCustomFields, trans)
        ''End of For Custom Fields


        '' Anubhooti 26-Aug-2014 (Error Handling When Currency Code is visible)
        '' multicurrency
        Dim strq As String
        If Me.fndVendorCurrency.Visible = True Then
            If clsCommon.myLen(fndVendorCurrency.Value) > 0 Then
                strq = "Update TSPL_VENDOR_MASTER set CURRENCY_CODE='" & clsCommon.myCstr(Me.fndVendorCurrency.Value) & "' where Vendor_Code='" + fndvendorNo.Value + "'"
            Else
                strq = "Update TSPL_VENDOR_MASTER set CURRENCY_CODE=null where Vendor_Code='" + fndvendorNo.Value + "'"
            End If
        Else
            strq = "Update TSPL_VENDOR_MASTER set CURRENCY_CODE=null where Vendor_Code='" + fndvendorNo.Value + "'"
        End If
        clsDBFuncationality.ExecuteNonQuery(strq, trans)

        If objCommonVar.IsMultiCurrencyCompany = False Then
            clsDBFuncationality.ExecuteNonQuery("Update TSPL_VENDOR_MASTER set CURRENCY_CODE='" + objCommonVar.BaseCurrencyCode + "' where Vendor_Code='" + fndvendorNo.Value + "'", trans)
        End If

        '' end multi currency

        ' ticket : BHA/21/06/18-000071************************
        Dim qryforHighclass As String = ""
        If chkHighClass.Checked = True Then
            qryforHighclass = " Update TSPL_VENDOR_MASTER set isHighClass = 1 where Vendor_Code='" + fndvendorNo.Value + "' "
        ElseIf chkHighClass.Checked = False Then
            qryforHighclass = " Update TSPL_VENDOR_MASTER set isHighClass = 0 where Vendor_Code='" + fndvendorNo.Value + "' "
        End If
        clsDBFuncationality.ExecuteNonQuery(qryforHighclass, trans)
        Dim qryforBulkRouteNo As String = ""
        If clsCommon.myLen(fndBulkRouteCode.Value) > 0 Then

            qryforBulkRouteNo = " Update TSPL_VENDOR_MASTER set  Bulk_ROUTE_NO = '" + clsCommon.myCstr(fndBulkRouteCode.Value) + "'  where Vendor_Code='" + fndvendorNo.Value + "' "
            clsDBFuncationality.ExecuteNonQuery(qryforBulkRouteNo, trans)
        Else
            qryforBulkRouteNo = " Update TSPL_VENDOR_MASTER set  Bulk_ROUTE_NO = NULL  where Vendor_Code='" + fndvendorNo.Value + "' "
            clsDBFuncationality.ExecuteNonQuery(qryforBulkRouteNo, trans)
        End If


        Dim qryforEmp As String = ""
        If chkIsEmployee.Checked = True AndAlso clsCommon.myLen(EmployeeFind.Value) > 0 Then
            qryforEmp = " Update TSPL_VENDOR_MASTER set EMP_CODE ='" + EmployeeFind.Value + "' where Vendor_Code='" + fndvendorNo.Value + "'"
        Else
            qryforEmp = " Update TSPL_VENDOR_MASTER set EMP_CODE = NULL where Vendor_Code='" + fndvendorNo.Value + "'"
        End If
        clsDBFuncationality.ExecuteNonQuery(qryforEmp, trans)

        Dim QryRegistration As String = ""
        If clsCommon.myLen(fndVendorReg.Value) > 0 Then
            QryRegistration = " Update TSPL_VENDOR_MASTER set  Registration_No = '" + clsCommon.myCstr(fndVendorReg.Value) + "'  where Vendor_Code='" + fndvendorNo.Value + "' "
            clsDBFuncationality.ExecuteNonQuery(QryRegistration, trans)
        Else
            QryRegistration = " Update TSPL_VENDOR_MASTER set  Registration_No = NULL  where Vendor_Code='" + fndvendorNo.Value + "' "
            clsDBFuncationality.ExecuteNonQuery(QryRegistration, trans)
        End If

        '****************************************************
    End Sub


    Public Sub fundelete()
        Try
            Dim qst As String
            Dim dpt As String
            '------for payment screen---------
            qst = "select Vendor_Code from TSPL_PAYMENT_HEADER where Vendor_Code='" + fndvendorNo.Value + "'"
            dpt = clsDBFuncationality.getSingleValue(qst)
            If clsCommon.myLen(dpt) > 0 AndAlso dpt IsNot Nothing Then
                common.clsCommon.MyMessageBoxShow("This Customer Cannot be deleted." + Environment.NewLine + "This is already in Process")
                Return
            End If
            '-----------for store recevied screen-------
            qst = "select Vendor_Code from TSPL_SRN_HEAD where Vendor_Code='" + fndvendorNo.Value + "'"
            dpt = clsDBFuncationality.getSingleValue(qst)
            If clsCommon.myLen(dpt) > 0 AndAlso dpt IsNot Nothing Then
                common.clsCommon.MyMessageBoxShow("This Customer Cannot be deleted." + Environment.NewLine + "This is already in Process")
                Return
            End If
            qst = "select Vendor_Code from TSPL_PR_HEAD where Vendor_Code='" + fndvendorNo.Value + "'"
            dpt = clsDBFuncationality.getSingleValue(qst)
            If clsCommon.myLen(dpt) > 0 AndAlso dpt IsNot Nothing Then
                common.clsCommon.MyMessageBoxShow("This Customer Cannot be deleted." + Environment.NewLine + "This is already in Process")
                Return
            End If

            connectSql.RunSp("sp_TSPL_VENDOR_MASTER_DELETE", New SqlParameter("@Vendor_Code", fndvendorNo.Value), New SqlParameter("@form_type", txtvndrtype.Text))
            clsCustomFieldValues.DeleteData(MyBase.Form_ID, fndvendorNo.Value, Nothing)

            qst = "delete from TSPL_vendor_category_master where Vendor_Code='" + fndvendorNo.Value + "'"
            clsDBFuncationality.ExecuteNonQuery(qst)

            myMessages.delete()
            btnsave.Text = "Save"
            btndelete.Enabled = False
            funreset()
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub
    'this function will reset all the fields for new entry
    Public Sub funreset()
        'chkcsa.Checked = False
        chkIsAllowSkipPurchaseQC.Checked = False
        chkTCSTDSamountgreater50KpreviousYear.Checked = False
        chkbuyerfilereturnlasttwoyear.Checked = False
        chk_isblacklist.Checked = False
        chkIsEmployee.Checked = False
        txt_ssino.Text = ""
        ddl_industrytype.SelectedValue = "Others"
        txtVendorDistance.Text = ""
        fndpaymentCycle.Value = ""
        lblpaymentCycle.Text = ""
        fndCHA_Code.Enabled = False
        fndCHA_Code.Value = ""
        txtCHA_Amount.Text = ""
        txtCHA_Type.Text = ""
        txtChequeInFavour.Enabled = True
        txtCategoryStructureCode.Value = ""
        lblCategoryStructureCode.Text = ""
        LoadBlankGridCat()
        chkVendorInvoiceNo.Checked = False
        chkOEM.Checked = False
        chkparentvendor.Checked = False
        txtparentname.Text = ""
        fndparent.Value = ""
        fndparent.Enabled = True
        fndparent.MendatroryField = True
        txtcountrycode.Value = ""
        txttdsstate.Value = ""
        txtvndrtype.Text = "ALL"
        txtChequeInFavour.Text = ""
        Me.fndvendorNo.Value = ""
        Me.txtvendortypedes.Text = ""
        Me.txtvendorname.Text = ""
        Me.fndgroupcode.Value = ""
        Me.txtgroupdes.Text = ""
        chkHold.Checked = False
        chkInActive.Checked = False
        InActiveCF.Checked = False
        chktrarns.Checked = False
        Me.dtClosing.Value = connectSql.serverDate()
        Me.txtAdd1.Text = ""
        Me.txtAdd2.Text = ""
        Me.txtAdd3.Text = ""
        Me.fndCity.Value = ""
        Me.txtCity.Text = ""
        Me.lblStateName.Text = ""
        '' Anubhooti 16-Aug-2014
        Me.txtPinCode.Text = ""
        Me.txtCountry.Text = ""
        Me.txtPhone1.Text = "(+__)__________"
        Me.txtPhone2.Text = "(+__)__________"
        Me.txtfax.Text = ""
        Me.txtEmail.Text = ""
        Me.txtWeb.Text = ""
        Me.txtContactName.Text = ""
        Me.txtContPhone.Text = "(+__)__________"
        Me.txtContactFax.Text = ""
        Me.txtContactWeb.Text = ""
        Me.txtContactEmail.Text = ""
        Me.fndTrmsCode.Value = ""
        Me.txttermcodedes.Text = ""
        Me.fndAccntSet.Value = ""
        Me.txtaccsetdes.Text = ""
        Me.fndPayCode.Value = ""
        Me.txtpaymentcodedes.Text = ""
        Me.fndvendortype.Value = ""
        Me.txtvendortypedes.Text = ""
        Me.fndbankcode.Value = ""
        'Me.txtbankcodedes.Text = ""
        Me.TxtBankName.Text = ""
        Me.txtbranchname.Text = ""
        Me.TxtBankBranch.Value = ""
        Me.txtbankcountry.Text = ""
        Me.txtbankstate.Text = ""
        Me.txtbankcity.Text = ""
        Me.txtStaxNo.Text = ""
        Me.txtLstNo.Text = ""
        Me.txtTinNo.Text = ""
        Me.txtCredit.Text = "0.00"
        Me.txtRemarks1.Text = ""
        Me.txtRemarks2.Text = ""
        Me.txtAddInfo1.Text = ""
        Me.txtAddInfo2.Text = ""
        Me.txtAddInfo3.Text = ""
        Me.fndTxGrp.Value = ""
        Me.txtTxGrp.Text = ""
        Me.txtcst.Text = ""
        Me.txtecc.Text = ""
        Me.txtrange.Text = ""
        Me.txtcollect.Text = ""
        Me.txtpan.Text = ""
        txtAliesName.Text = ""
        TxtOldname.Text = ""
        Me.chkInterBranch.Checked = False
        Me.grdTax.DataSource = Nothing
        Me.grdTax.Rows.Clear()
        Me.fndVendorCurrency.Value = Nothing
        '' Anubhooti 1-Aug-2014
        cmbAccountType.SelectedValue = "Cur"
        TxtBankBranch.Text = ""
        TxtBankName.Text = ""
        TxtIFSCCode.Text = ""
        TxtAccNo.Text = ""
        cmbTypeOfVen.SelectedValue = "N"
        If is_For_Chilling_Vendor = True Then
            ' ChkChillingVendor.Checked = False
            CmbVenType.SelectedValue = "CV"
            CmbVenType.Enabled = False
        Else
            CmbVenType.SelectedValue = ""
        End If
        ChkIsTDSApp.Checked = False
        ChkIsTDSApp.Enabled = True
        GrpTDS.Enabled = False
        fnddeducNew.Value = ""
        lblNatureOfDed.Text = ""
        txttdsstate.Value = ""
        lblStateName.Text = ""
        fndbranchnew.Value = ""
        LblBranchName.Text = ""
        ddlventype.Text = "Individual"
        ddlstatus.Text = "Resident"
        chkProvisional.Checked = False
        chkDefaultGrower.Checked = False

        fnddeducNewService.Value = ""
        lblNatureOfDedService.Text = ""
        txttdsstateService.Value = ""
        lblStateNameService.Text = ""
        fndbranchnewService.Value = ""
        LblBranchNameService.Text = ""
        ddlventypeService.Text = "Individual"
        ddlstatusService.Text = "Resident"

        chkInActive.Checked = False
        dtClosing.Enabled = False
        TxtTCCertified.Text = ""
        ChkTCCertified.Checked = False
        btnsave.Text = "Save"
        btndelete.Enabled = False
        chkTagAsFranchise.Checked = False
        txtvendorname.Focus()
        ChkOther.Checked = False
        ChkOther.Visible = True
        chkTCSNotApplicable.Checked = False
        UcAttachment1.Form_ID = Me.Form_ID
        UcAttachment1.BlankAllControls()
        ''For Custom Fields
        If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
            UcCustomFields1.BlankAllControls()
        End If
        VendorName()
        ''End of For Custom Fields
        LoadType()
        MyTextBox2.Text = ""
        txtEntity.Text = ""
        txtGSTIN_No_final.Text = ""
        txtGST_PanCode.Text = ""
        txtGSTStateCode.Text = ""
        txtState.Value = ""
        LblState.Text = ""
        txtWeight.Text = ""
        txtJWPriceCode.Value = ""
        fndBulkRouteCode.Value = ""
        lblBulkRouteNo.Text = ""
        chkHighClass.Checked = False
        EmployeeFind.Value = ""
        fndVendorReg.Value = ""
        If EnableBankFromMaster = True Then
            TxtBankName.ReadOnly = True
            txtbranchname.ReadOnly = True
        Else
            TxtBankName.ReadOnly = False
            txtbranchname.ReadOnly = False
        End If
    End Sub

    Public Sub funExport()
        Try
            Dim strCmd As String
            strCmd = "select Vendor_Code as [Vendor No],Vendor_Name as[Vendor Name],Alies_Name AS [Alias Name],Vendor_Type_CHA As [Vendor Type CHA],Add1 as [Address1],Add2 as [Address2]" &
                      ",Add3 as [Address3],Closing_Date as [Closing Date],Vendor_Group_Code as [Group Code]" &
                      ",Vendor_Group_Code_Desc as [Vendor Group Description],City_Code as [City Code]" &
                      ",City_Code_Desc as [City Code Description],State_Code, Country_Code, Phone1 as [Phone Num1]" &
                      ",Phone2 as [Phone Num2],Fax as [Fax],Email as [Email Id],WebSite as [Website],Contact_Person_Name as [Contact Person Name]" &
                      ",Contact_Person_Phone as [Contect Person Phone],Contact_Person_Fax as [Contact Person Fax]" &
                       ",Contact_Person_Website as [Contact Person Website],Contact_Person_Email as [Contact Person Email]" &
                      ",Terms_Code as [Terms Code],Terms_Code_Desc as [Terms Description],Vendor_Account as [Vendor Account]" &
                       ",Vendor_Account_Desc as [Vendor Account Description],Payment_Code as [Payment Code]" &
                       ",Payment_Code_Desc as [Paymnet Code Description],Bank_Code as [Bank Code],Bank_Code_Desc as [Bank Code Description],branch_code As [IFSC Code],Branch_Name AS [Branch Name]" &
                       ",Ven_Type_Code as [Vendor Type],Ven_Type_Desc as [Vendor Type Description]" &
                       ",Tax_Group as [Tax Group],Tax_Group_Desc as [Tax Group Description]" &
                       ",TAX1 as [Tax1],TAX1_Rate as [Tax1 Rate],TAX2 as [Tax2],TAX2_Rate as [Tax2 Rate],TAX3 as [Tax3]" &
                       ",TAX3_Rate as [Tax3 Rate],TAX4 as [Tax4],TAX4_Rate as [Tax4 Rate],TAX5 as [Tax5]" &
                       ",TAX5_Rate as [Tax5 Rate],TAX6 as [Tax6],TAX6_Rate as [Tax6 Rate],TAX7 as [Tax7]" &
                       ",TAX7_Rate as [Tax7 Rate], TAX8 as [Tax8],TAX8_Rate as [Tax8 Rate],TAX9 as [Tax9]" &
                       ",TAX9_Rate as [Tax9 Rate],TAX10 as [Tax10],TAX10_Rate as [Tax10 Rate],Service_Tax_No as [Service Tax No]" &
                       ",Tin_No as [Tin No],Lst_No as [List No],Status as [Status],OnHold as [On Hold],Transporter as [Transporter]" &
                       ",Remarks1 as [Remarks],Remarks2 as [Remarks2],Additional1 as [Additional1],Additional2 as [Additional2]" &
                       ",Additional3 as [Additional3],Created_By as [Created By],Created_Date as [Created Date],Modify_By as [Modify By]" &
                       ",Modify_Date as [Modify Date],comp_code as [Company Code],CST as [CST],ECC as [Ecc],Range as [Range]" &
                       ",Collectorate as [Collectorate],PAN as [PAN] ,inter_Branch as [Inter Branch], franchise_yn as [Tagged as Franchise],Is_Parent_Vendor,Parent_Vendor_Code,form_type,Category_Struct_Code,Account_No,Account_Type,Vendor_Type,Pin_Code,Is_TDS_Applicable As [Is TDS Applicable],TDS_State_Code As [TDS State Code],TDS_Status As [TDS Status],TDS_Vendor_Type As [TDS Vendor Type],Deduction_Code As [Deduction Code],TDS_Branch_Code As [TDS Branch Code],TDS_State_Code_Service As [Service TDS State Code],TDS_Status_Service As [Service TDS Status],TDS_Vendor_Type_Service As [Service TDS Vendor Type],Deduction_Code_Service As [Service Deduction Code],TDS_Branch_Code_Service As [Service TDS Branch Code],case when isnull(IsVendorInvoiceNo,0)=0 then 'N' else 'Y' end as [Vendor Invoice No(Y/N)],CHA_DOC_NO,Is_TC_Certified AS [Is TC Certified],TC_Certified AS [TC Certified],CHEQUE_IN_FAVOUR_OF AS [Cheque in Favour of],TSPL_VENDOR_MASTER.CURRENCY_CODE as [CURRENCY CODE],PC_CODE as [Payment Cycle],Vendor_Distance as [Vendor Distance],OldName as [Old Name],SSI_No as [SSI No],(case when Is_Blacklist='1' then 'Y' else 'N' end) as [Is Blacklist],(case when IsEmployee='1' then 'Y' else 'N' end) as [Is Employee],ISNULL(EMP_CODE,'') as [Employee Code],Weight"
            If AllowGSTApplicable = True Then
                strCmd += " ,DFOption as 'Domestic Foreign', BusinessCondition as 'Business Condition', GSTRegistered as 'GST Register', GST_Composition_scheme as 'GST Composition Scheme', GSTFinalNo as 'GSTIN No'"
            End If

            strCmd += " ,isHighClass as [High Class] , Bulk_ROUTE_NO as [Bulk Route No], case when  isnull ( Isbuyerfilereturninlasttwoyears,0) = 1 then 'Yes' else 'No' end as [buyer file return in last two years], case when  isnull (IsTCS_TDSamountgreaterthan50KpreviousYear,0) = 1 then 'Yes' else 'No' end as [TCS/TDS amount is greater than 50K in previous Year], case when  isnull ( IsAllowSkipPurchaseQC,0) = 1 then 'Yes' else 'No' end as [Allow Skip Purchase QC] from TSPL_VENDOR_MASTER "

            Dim whrCls = " and (form_type='ALL' or form_type='VSP') "

            ListImpExpColumnsMandatory = New List(Of String)({"Vendor No", "Vendor Name", "Group Code", "state_code", "Terms Code", "Vendor Account", "Tax Group", "Is_Parent_Vendor", "Account_Type", "Vendor_Type", "Is TDS Applicable", "Is TC Certified", "GST Register", "Domestic Foreign", "Pan"})
            ListImpExpColumnsSuperMandatory = New List(Of String)({"Vendor No"})
            transportSql.ExporttoExcel(strCmd, whrCls, "", Me, ListImpExpColumnsMandatory, ListImpExpColumnsSuperMandatory, MyBase.Form_ID)


        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Customer")
        End Try

    End Sub

    Public Sub funImport()
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim strbankdes As String
        Dim Count As String = ""
        Dim AllowAutoVCode As String = ""
        Dim AllowAutoVCodeForAllCompany As String = ""
        Dim qryNatureDed As String = ""
        Dim strBrachName As String
        Dim strBName As String
        Dim strIFSCCode As String = String.Empty
        Dim strBranchCode As String = String.Empty
        Dim strAliesName As String = ""
        Dim strVenType As String = ""
        Dim Str_Vendor As String = ""
        Dim GSTFinal As String = ""
        Dim DFoption As String = Nothing
        Dim Registered As Integer = 0
        Dim GST_Composition_scheme As Integer = 0
        Dim BusinessCondition As String = ""
        Dim GSTEntity As String = ""
        Dim GSTLastEntity As String = ""
        Dim GSTMiddleEntity As String = ""

        Dim inputs() As String = {}

        If AllowGSTApplicable = True Then
            inputs = {"Vendor No", "Vendor Name", "Alias Name", "Vendor Type CHA", "Address1", "Address2", "Address3", "Closing Date", "Group Code", "Vendor Group Description", "City Code", "City Code Description", "State_Code", "Country_Code", "Phone Num1", "Phone Num2", "Fax", "Email Id", "Website", "Contact Person Name", "Contect Person Phone", "Contact Person Fax", "Contact Person Website", "Contact Person Email", "Terms Code", "Terms Description", "Vendor Account", "Vendor Account Description", "Payment Code", "Paymnet Code Description", "Bank Code", "Bank Code Description", "IFSC Code", "Branch Name", "Vendor Type", "Vendor Type Description", "Tax Group", "Tax Group Description", "Tax1", "Tax1 Rate", "Tax2", "Tax2 Rate", "Tax3", "Tax3 Rate", "Tax4", "Tax4 Rate", "Tax5", "Tax5 Rate", "Tax6", "Tax6 Rate", "Tax7", "Tax7 Rate", "Tax8", "Tax8 Rate", "Tax9", "Tax9 Rate", "Tax10", "Tax10 Rate", "Service Tax No", "Tin No", "List No", "Status", "On Hold", "Transporter", "Remarks", "Remarks2", "Additional1", "Additional2", "Additional3", "Created By", "Created Date", "Modify By", "Modify Date", "Company Code", "CST", "Ecc", "Range", "Collectorate", "PAN", "Inter Branch", "Tagged as Franchise", "Is_Parent_Vendor", "Parent_Vendor_Code", "form_type", "Category_Struct_Code", "Account_No", "Account_Type", "Vendor_Type", "Pin_Code", "Is TDS Applicable", "TDS State Code", "TDS Status", "TDS Vendor Type", "Deduction Code", "TDS Branch Code", "Service TDS State Code", "Service TDS Status", "Service TDS Vendor Type", "Service Deduction Code", "Service TDS Branch Code", "Vendor Invoice No(Y/N)", "CHA_DOC_NO", "Is TC Certified", "TC Certified", "Cheque in Favour of", "CURRENCY CODE", "Payment Cycle", "Vendor Distance", "Old Name", "SSI No", "Is Blacklist", "Is Employee", "Employee Code", "Domestic Foreign", "Business Condition", "GST Register", "GST Composition Scheme", "GSTIN No", "Weight", "High Class", "Bulk Route No", "buyer file return in last two years", "TCS/TDS amount is greater than 50K in previous Year", "Allow Skip Purchase QC"}
        Else
            inputs = {"Vendor No", "Vendor Name", "Alias Name", "Vendor Type CHA", "Address1", "Address2", "Address3", "Closing Date", "Group Code", "Vendor Group Description", "City Code", "City Code Description", "State_Code", "Country_Code", "Phone Num1", "Phone Num2", "Fax", "Email Id", "Website", "Contact Person Name", "Contect Person Phone", "Contact Person Fax", "Contact Person Website", "Contact Person Email", "Terms Code", "Terms Description", "Vendor Account", "Vendor Account Description", "Payment Code", "Paymnet Code Description", "Bank Code", "Bank Code Description", "IFSC Code", "Branch Name", "Vendor Type", "Vendor Type Description", "Tax Group", "Tax Group Description", "Tax1", "Tax1 Rate", "Tax2", "Tax2 Rate", "Tax3", "Tax3 Rate", "Tax4", "Tax4 Rate", "Tax5", "Tax5 Rate", "Tax6", "Tax6 Rate", "Tax7", "Tax7 Rate", "Tax8", "Tax8 Rate", "Tax9", "Tax9 Rate", "Tax10", "Tax10 Rate", "Service Tax No", "Tin No", "List No", "Status", "On Hold", "Transporter", "Remarks", "Remarks2", "Additional1", "Additional2", "Additional3", "Created By", "Created Date", "Modify By", "Modify Date", "Company Code", "CST", "Ecc", "Range", "Collectorate", "PAN", "Inter Branch", "Tagged as Franchise", "Is_Parent_Vendor", "Parent_Vendor_Code", "form_type", "Category_Struct_Code", "Account_No", "Account_Type", "Vendor_Type", "Pin_Code", "Is TDS Applicable", "TDS State Code", "TDS Status", "TDS Vendor Type", "Deduction Code", "TDS Branch Code", "Service TDS State Code", "Service TDS Status", "Service TDS Vendor Type", "Service Deduction Code", "Service TDS Branch Code", "Vendor Invoice No(Y/N)", "CHA_DOC_NO", "Is TC Certified", "TC Certified", "Cheque in Favour of", "CURRENCY CODE", "Payment Cycle", "Vendor Distance", "Old Name", "SSI No", "Is Blacklist", "Is Employee", "Employee Code", "Weight", "High Class", "Bulk Route No"}
        End If

        Dim Strs As List(Of String) = New List(Of String)(inputs)


        If transportSql.importExcel(gv, Strs.ToArray()) Then
            Dim trans As SqlTransaction = Nothing
            Dim linno As Integer = 0
            Dim IsBlacklisted As Integer = 0
            Dim IsEmployee As Integer = 0
            Dim strbank As String = String.Empty
            Try
                connectSql.OpenConnection()
                trans = clsDBFuncationality.GetTransactin()
                clsCommon.ProgressBarPercentShow()
                For Each grow As GridViewRowInfo In gv.Rows
                    Count = clsCommon.myCstr(grow.Index + 2)
                    linno += 1
                    clsCommon.ProgressBarPercentUpdate((linno * 100) / gv.RowCount - 1, "Importing " + clsCommon.myCstr(linno) + "/" + clsCommon.myCstr(gv.RowCount - 1))
                    Dim IsBlacklist As String = clsCommon.myCstr(grow.Cells("Is Blacklist").Value)
                    Dim IsEmployeeD As String = clsCommon.myCstr(grow.Cells("Is Employee").Value)
                    Dim EmployeeCode As String = clsCommon.myCstr(grow.Cells("Employee Code").Value)
                    Dim strSSIno As String = clsCommon.myCstr(grow.Cells("SSI No").Value)

                    Dim strvendorNo As String = clsCommon.myCstr(grow.Cells("Vendor No").Value)
                    AllowAutoVCode = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AutoGeneratedVendorCode, clsFixedParameterCode.AutoGeneratedVendorCode, trans))
                    If clsCommon.CompairString(AllowAutoVCode, "0") = CompairStringResult.Equal Then
                        If strvendorNo.Length > 12 Then
                            Throw New Exception("Check the lenght of Vendor No.")
                        End If

                        If String.IsNullOrEmpty(strvendorNo) Then
                            Throw New Exception("Vendor No. can not be blank")
                        End If
                    Else

                    End If


                    Dim strvendorname1 As String = clsCommon.myCstr(grow.Cells("Vendor Name").Value)
                    Dim strvendorname As String = strvendorname1.Replace("'", "''")
                    If strvendorname.Length > 100 Then
                        Throw New Exception("Length of Vendor Name can not be greater than 100.")
                    End If
                    If String.IsNullOrEmpty(strvendorname) Then
                        Throw New Exception("Vendor Name can not be blank")
                    End If
                    '' Anubhooti 06-Oct-2014 BM00000003996
                    strAliesName = clsCommon.myCstr(grow.Cells("Alias Name").Value)
                    If clsCommon.myLen(strAliesName) > 200 Then
                        Throw New Exception("Alias Name should be max 200 character")
                    End If
                    ''
                    ''richa agarwal 28 Sep, 2016
                    Dim stroldname As String = clsCommon.myCstr(grow.Cells("Old Name").Value)
                    If clsCommon.myLen(stroldname) > 50 Then
                        Throw New Exception("Old Name should be max 50 character")
                    End If
                    ''---------------
                    Dim CSAType As String = "N"
                    Dim ChillingVen As String = "0"

                    Dim isVendorInvoiceNo As Integer = 0
                    'Vendor Invoice No(Y/N)
                    If clsCommon.CompairString(clsCommon.myCstr(grow.Cells("Vendor Invoice No(Y/N)").Value).Trim().ToUpper(), "Y") = CompairStringResult.Equal Then
                        isVendorInvoiceNo = 1
                    Else
                        isVendorInvoiceNo = 0
                    End If
                    strVenType = clsCommon.myCstr(grow.Cells("Vendor Type CHA").Value).Trim().ToUpper()
                    If clsCommon.myLen(strVenType) > 0 Then
                        If clsCommon.CompairString(strVenType, "BROKER") = CompairStringResult.Equal OrElse clsCommon.CompairString(strVenType, "CSA") = CompairStringResult.Equal Or clsCommon.CompairString(strVenType, "CHA") = CompairStringResult.Equal Or clsCommon.CompairString(strVenType, "CV") = CompairStringResult.Equal Or clsCommon.CompairString(strVenType, "A") = CompairStringResult.Equal Or clsCommon.CompairString(strVenType, "O") = CompairStringResult.Equal Or clsCommon.CompairString(strVenType, "M") = CompairStringResult.Equal Or clsCommon.CompairString(strVenType, "J") = CompairStringResult.Equal Then 'clsCommon.CompairString(strVenType, "COM") = CompairStringResult.Equal OrElse
                            If clsCommon.CompairString(strVenType, "CSA") = CompairStringResult.Equal Then
                                CSAType = "Y"
                            ElseIf clsCommon.CompairString(strVenType, "CV") = CompairStringResult.Equal Then
                                ChillingVen = "1"
                            End If
                        Else
                            Throw New Exception("Vendor Type CHA should be any one from 'CSA','CHA','BROKER','CV','A','O','M','J'")
                        End If
                    End If

                    Dim CHA_DOC_NO As String = ""
                    CHA_DOC_NO = clsCommon.myCstr(grow.Cells("CHA_DOC_NO").Value)

                    If clsCommon.CompairString(strVenType, "CHA") = CompairStringResult.Equal AndAlso clsCommon.myLen(CHA_DOC_NO) <= 0 Then
                        Throw New Exception("Fill CHA Charge Code for vendor type CHA at line no. " + clsCommon.myCstr(grow.Index + 1) + "")
                    End If

                    If clsCommon.myLen(CHA_DOC_NO) > 0 Then
                        Dim xco As Integer = clsDBFuncationality.getSingleValue("select count(*) from TSPL_CHA_CHARGE_MASTER where doc_no='" + CHA_DOC_NO + "'", trans)
                        If xco <= 0 Then
                            Throw New Exception("Fill CHA Charge Code for vendor type CHA not found at line no. " + clsCommon.myCstr(grow.Index + 1) + "")
                        End If
                    End If
                    Dim Cheque_In_favour_of As String = clsCommon.myCstr(grow.Cells("Cheque in Favour of").Value)
                    Dim add1 As String = clsCommon.myCstr(grow.Cells("Address1").Value)
                    Dim add2 As String = clsCommon.myCstr(grow.Cells("Address2").Value)
                    Dim add3 As String = clsCommon.myCstr(grow.Cells("Address3").Value)
                    Dim closing_date As String
                    If clsCommon.myCstr(grow.Cells("Closing Date").Value) = Nothing Then
                        closing_date = System.DateTime.Now.Date
                    Else
                        closing_date = clsCommon.myCstr(grow.Cells("Closing Date").Value)
                    End If



                    Dim strgroupCode As String = clsCommon.myCstr(grow.Cells("Group Code").Value)
                    If String.IsNullOrEmpty(strgroupCode) Then
                        Throw New Exception(" Group Code can not be blank")
                    End If
                    Dim i As Integer
                    Dim qry As String = "select Count(*)from TSPL_VENDOR_GROUP  where Ven_Group_Code ='" + strgroupCode + "'"
                    i = connectSql.RunScalar(trans, qry)
                    If i = 0 Then
                        Throw New Exception("Vendor Group Code does not exist : " + strgroupCode + "")
                    Else
                    End If
                    If strgroupCode.Length > 12 Then
                        Throw New Exception("Check the lenght of Group Code")
                    End If
                    '' 
                    Dim strGrpBankCode As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Bank_Code from Tspl_vendor_group where ven_group_code='" + strgroupCode + "'", trans))
                    ''

                    Dim strgroupDes As String = grow.Cells("Vendor Group Description").Value.ToString()
                    If strgroupDes.Length > 50 Then
                        Throw New Exception("Check the lenght of Group Code Description")
                    End If
                    Dim citycode As String = clsCommon.myCstr(grow.Cells("City Code").Value)
                    Dim citycodedesc As String = clsCommon.myCstr(grow.Cells("City Code Description").Value)

                    Dim Weight As Integer = clsCommon.myCdbl(grow.Cells("Weight").Value)

                    Dim statecode As String = clsCommon.myCstr(grow.Cells("state_code").Value)
                    If clsCommon.myLen(statecode) <= 0 Then
                        Throw New Exception("Filled state code ")
                    End If
                    If clsCommon.myLen(statecode) > 0 Then
                        Dim qryState As String = "select Count(*) As Row from TSPL_STATE_MASTER where State_Code ='" & statecode & "'"
                        Dim checkState As Integer = clsDBFuncationality.getSingleValue(qryState, trans)
                        If checkState <= 0 Then
                            Throw New Exception("Filled state code does not exist" + Environment.NewLine + ".First make the entry for state code")
                        End If
                        'strTDSStateCode = "'" & strTDSStateCode & "'"
                    End If
                    Dim state As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select State_Name from TSPL_STATE_MASTER WHERE State_Code='" & statecode & "'", trans))
                    Dim countrycode As String = clsCommon.myCstr(grow.Cells("country_code").Value)
                    Dim country As String = clsCountryMaster.GetName(countrycode, trans)

                    Dim phonenum1 As String = clsCommon.myCstr(grow.Cells("Phone Num1").Value)
                    Dim phonenum2 As String = clsCommon.myCstr(grow.Cells("Phone Num2").Value)
                    Dim fax As String = clsCommon.myCstr(grow.Cells("Fax").Value)
                    Dim emailid As String = clsCommon.myCstr(grow.Cells("Email Id").Value)
                    Dim website As String = clsCommon.myCstr(grow.Cells("Website").Value)
                    Dim contct_person_name As String = clsCommon.myCstr(grow.Cells("Contact Person Name").Value)
                    Dim contct_perfson_phone As String = clsCommon.myCstr(grow.Cells("Contect Person Phone").Value)
                    Dim contct_person_fax As String = clsCommon.myCstr(grow.Cells("Contact Person Fax").Value)
                    Dim contct_person_website As String = clsCommon.myCstr(grow.Cells("Contact Person Website").Value)
                    Dim contct_person_email As String = clsCommon.myCstr(grow.Cells("Contact Person Email").Value)
                    Dim strtermcode As String = clsCommon.myCstr(grow.Cells("Terms Code").Value)
                    If String.IsNullOrEmpty(strtermcode) Then
                        Throw New Exception(" Terms Code can not be blank")
                    End If
                    Dim i1 As Integer
                    Dim qry1 As String = "select count(*) from tspl_terms_master where terms_code='" + strtermcode + "'"
                    i1 = connectSql.RunScalar(trans, qry1)
                    If i1 = 0 Then
                        Throw New Exception("Terms Code Does not exist : " + strtermcode + "")
                    End If

                    If strtermcode.Length > 12 Then
                        Throw New Exception("Check the length of Terms Code")
                    End If

                    Dim strtermdes As String = clsCommon.myCstr(grow.Cells("Terms Description").Value)
                    If strtermdes.Length > 50 Then
                        Throw New Exception("Check the length of Term Description")
                    End If
                    Dim vendoracct As String = clsCommon.myCstr(grow.Cells("Vendor Account").Value)
                    If String.IsNullOrEmpty(vendoracct) Then
                        Throw New Exception(" Vendor Account can not be blank")
                    End If
                    Dim i3 As String

                    Dim qry3 As String = "select COUNT(*) from TSPL_VENDOR_ACCOUNT_SET where Acct_Set_Code ='" + vendoracct + "'"
                    i3 = connectSql.RunScalar(trans, qry3)
                    If i3 = 0 Then
                        Throw New Exception("Vendor Account Does Not Exist : " + vendoracct + "")
                    End If
                    If vendoracct.Length > 12 Then
                        Throw New Exception("Check the lenght of Vendor Account Set Code")
                    End If

                    Dim vendoracctdesc As String = clsCommon.myCstr(grow.Cells("Vendor Account Description").Value)

                    Dim paymenttype As String = clsCommon.myCstr(grow.Cells("Payment Code").Value)
                    Dim i4 As String
                    If Not String.IsNullOrEmpty(paymenttype) Then
                        Dim qry5 As String = "select COUNT(*) from TSPL_PAYMENT_CODE  where Payment_Code ='" + paymenttype + "'"
                        i4 = connectSql.RunScalar(trans, qry5)
                        If i4 = 0 Then
                            Throw New Exception("Payment Code Does Not Exist : " + paymenttype + "")
                        End If
                        If paymenttype.Length > 12 Then
                            Throw New Exception("Check the lenght of Payment Code")
                        End If
                    End If
                    Dim paymenttypedesc As String = clsCommon.myCstr(grow.Cells("Paymnet Code Description").Value)
                    ' If clsCommon.myLen(clsCommon.myCstr(grow.Cells("Bank Code").Value)) > 0 Then
                    strbank = clsCommon.myCstr(grow.Cells("Bank Code").Value)
                    'End If


                    'If String.IsNullOrEmpty(strbank) Then
                    '    Throw New Exception("Bank Code can not be blank")
                    'End If
                    Dim i5 As String
                    '' Anubhooti 26-Sep-2014
                    'Dim qry7 As String = "select COUNT(*) from TSPL_BANK_MASTER  where Bank_Code ='" + strbank + "'"
                    Dim qry7 As String = "select COUNT(*) from TSPL_Vendor_Bank_MASTER  where Bank_Code ='" + strbank + "'"
                    i5 = connectSql.RunScalar(trans, qry7)
                    If i5 = 0 Then
                        Throw New Exception("Bank Code Does Not Exist : " + strbank + ".Please make entry in vendor bank master.")
                    End If
                    If strbank.Length > 30 Then
                        Throw New Exception("Check the lenght of Bank Code")
                    End If

                    'Dim strbankdes As String = clsCommon.myCstr(grow.Cells("Bank Code Description").Value)
                    'If strbankdes.Length > 50 Then
                    '    Throw New Exception("Check the lenght of Bank Description")
                    'End If

                    If clsCommon.myLen(strbank) > 0 Then
                        strbank = strbank
                    Else
                        strbank = strGrpBankCode
                    End If

                    Dim strvendortype As String = clsCommon.myCstr(grow.Cells("Vendor Type").Value)
                    Dim strvendortypedes As String = grow.Cells("Vendor Type Description").Value.ToString()
                    If strvendortype.Length > 12 Then
                        Throw New Exception("Check the lenght of Vendor Type ")
                    End If
                    If strvendortypedes.Length > 50 Then
                        Throw New Exception("Check the lenght of Vendor Type Description")
                    End If
                    Dim strTax As String = clsCommon.myCstr(grow.Cells("Tax Group").Value)
                    If String.IsNullOrEmpty(strTax) Then
                        Throw New Exception(" Tax Group can not be blank")
                    End If
                    Dim i6 As String
                    Dim qry9 As String = "select COUNT(*) from  TSPL_TAX_GROUP_MASTER   where tax_group_Code ='" + strTax + "'"
                    i6 = connectSql.RunScalar(trans, qry9)
                    If i6 = 0 Then
                        Throw New Exception("Tax Group Code Does Not Exist : " + strTax + "")
                    End If
                    If strTax.Length > 12 Then
                        Throw New Exception("Check the lenght of Tax Group Code")
                    End If

                    Dim strtaxdes As String = grow.Cells("Tax Group Description").Value.ToString()
                    If strtaxdes.Length > 50 Then
                        Throw New Exception("Check the lenght of Tax Description")
                    End If
                    Dim interbranch As String = grow.Cells("Inter Branch").Value.ToString()
                    If interbranch.Length > 1 Then
                        Throw New Exception("Check the lenght of Inter Branch")
                    ElseIf String.IsNullOrEmpty(interbranch) Then
                        interbranch = "N"
                    End If

                    Dim strTagAsFranchise As String = grow.Cells("Tagged as Franchise").Value.ToString()
                    If strTagAsFranchise.Length > 1 Then
                        Throw New Exception("Check the lenght of Tagged as Franchise")
                    ElseIf String.IsNullOrEmpty(strTagAsFranchise) Then
                        strTagAsFranchise = "N"
                    End If

                    '--------------------------------------------
                    Dim parent_vendor_code As String = clsCommon.myCstr(grow.Cells("Parent_Vendor_Code").Value)
                    Dim isparent As String = clsCommon.myCstr(grow.Cells("Is_Parent_Vendor").Value)

                    If clsCommon.myLen(isparent) <= 0 Then
                        Throw New Exception("Please Fill Parent Status,If Is_Parent_Vendor Then Put '1' Else Put '0'")
                    ElseIf clsCommon.CompairString(isparent, "1") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(isparent, "0") <> CompairStringResult.Equal Then
                        Throw New Exception("Please Fill Parent Status,If Is_Parent_Vendor Then Put '1' Else Put '0'")
                    End If

                    If clsCommon.CompairString(isparent, "0") = CompairStringResult.Equal AndAlso clsCommon.myLen(parent_vendor_code) <= 0 Then
                        Throw New Exception("Please Fill Parent Vendor Code")
                    ElseIf clsCommon.CompairString(isparent, "0") = CompairStringResult.Equal AndAlso clsCommon.myLen(parent_vendor_code) > 0 Then
                        qry = "select count(*) from tspl_vendor_master where vendor_code='" + parent_vendor_code + "' and is_parent_vendor='1'"
                        Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)

                        If check <= 0 Then
                            Throw New Exception("Filled Parent Vendor Code Does Not Exist," + Environment.NewLine + "First Make The Entry For Parent Vendor Then" + Environment.NewLine + "Choose It As Parent of Child Vendor")
                        End If
                    End If
                    '----------------------------------------------------------------

                    Dim cat_struct_code As String = ""
                    cat_struct_code = clsCommon.myCstr(grow.Cells("Category_Struct_Code").Value)

                    If clsCommon.myLen(cat_struct_code) > 0 Then
                        qry = "select count(*) from TSPL_ITEM_CATEGORY_STRUCTURE where item_category_struct_code='" + cat_struct_code + "' and isnull(form_type,'item')='vendor'"
                        Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)

                        If check <= 0 Then
                            Throw New Exception("First create Location category structure and mapped levels")
                        End If
                    End If
                    '' Anubhooti 1-Aug-2014

                    'Dim strBrachName As String = clsCommon.myCstr(grow.Cells("Branch_Name").Value)
                    'If clsCommon.myLen(strBrachName) > 150 Then
                    '    Throw New Exception("Branch Name should be max 150 character")
                    'End If

                    Dim strAccNo As String = clsCommon.myCstr(grow.Cells("Account_No").Value)
                    If clsCommon.myLen(strAccNo) > 50 Then
                        Throw New Exception("Account No. should be max 50 character")
                    End If

                    'Dim strBName As String = clsCommon.myCstr(grow.Cells("Bank_Name").Value)
                    'If clsCommon.myLen(strBName) > 50 Then
                    '    Throw New Exception("Bank Name should be max 50 character")
                    'End If
                    ''richa agarwal 26/03/2015
                    ' BM00000007855 10-Sep-2015
                    strIFSCCode = clsCommon.myCstr(grow.Cells("IFSC Code").Value)
                    strBrachName = clsCommon.myCstr(grow.Cells("Branch Name").Value)
                    If clsCommon.myLen(strIFSCCode) > 100 Then
                        Throw New Exception("IFSC Code should be max 100 character")
                    End If
                    If clsCommon.myLen(strbank) > 0 Then
                        If clsCommon.myLen(strIFSCCode) > 0 AndAlso clsCommon.myLen(strBrachName) <= 0 Then
                            If clsCommon.myLen(strbank) > 0 AndAlso clsDBFuncationality.getSingleValue("Select count(*) from TSPL_Vendor_Bank_Branch_Details where Bank_IFSC_Code ='" + strIFSCCode + "'  and TSPL_Vendor_Bank_Branch_Details.Bank_Code='" & strbank & "' ", trans) <= 0 Then
                                Throw New Exception("IFSC Code Does Not Exist :  " + strIFSCCode + " for bank " + strbank + "  .Please make entry in vendor bank master.")
                            Else
                                strBrachName = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select ISNULL(TSPL_Vendor_Bank_Branch_Details.Branch_Name,'') AS Branch_Name from TSPL_Vendor_Bank_Branch_Details where Bank_IFSC_Code ='" + strIFSCCode + "'  and TSPL_Vendor_Bank_Branch_Details.Bank_Code='" & strbank & "' ", trans))
                            End If
                        End If
                        If clsCommon.myLen(strIFSCCode) <= 0 AndAlso clsCommon.myLen(strBrachName) > 0 Then
                            If clsCommon.myLen(strbank) > 0 AndAlso clsDBFuncationality.getSingleValue("Select count(*) from TSPL_Vendor_Bank_Branch_Details where TSPL_Vendor_Bank_Branch_Details.Bank_Code='" & strbank & "' and TSPL_Vendor_Bank_Branch_Details.Branch_Name='" & strBrachName & "'", trans) <= 0 Then
                                Throw New Exception("Branch Name Does Not Exist : " + strBrachName + " for bank " + strbank + "  .Please make entry in vendor bank master.")
                            Else
                                strIFSCCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select ISNULL(TSPL_Vendor_Bank_Branch_Details.Bank_IFSC_Code,'') AS Branch_Name from TSPL_Vendor_Bank_Branch_Details where TSPL_Vendor_Bank_Branch_Details.Bank_Code='" & strbank & "' and TSPL_Vendor_Bank_Branch_Details.Branch_Name='" & strBrachName & "' ", trans))
                            End If
                        End If
                        If clsCommon.myLen(strIFSCCode) > 0 AndAlso clsCommon.myLen(strBrachName) > 0 Then
                            If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select count(*) from TSPL_Vendor_Bank_Branch_Details where Bank_IFSC_Code ='" + strIFSCCode + "'  and TSPL_Vendor_Bank_Branch_Details.Bank_Code='" & strbank & "' ", trans)) <= 0 Then
                                Throw New Exception("IFSC Code Does Not Exist :  " + strIFSCCode + " for bank " + strbank + "  .Please make entry in vendor bank master.")
                            End If
                            If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select count(*) from TSPL_Vendor_Bank_Branch_Details where TSPL_Vendor_Bank_Branch_Details.Bank_Code='" & strbank & "' and TSPL_Vendor_Bank_Branch_Details.Branch_Name='" & strBrachName & "'", trans)) <= 0 Then
                                Throw New Exception("Branch Name Does Not Exist : " + strBrachName + " for bank " + strbank + "  .Please make entry in vendor bank master.")
                            End If
                            strBranchCode = strIFSCCode
                            strBrachName = strBrachName
                        End If
                        If clsCommon.myLen(strIFSCCode) <= 0 AndAlso clsCommon.myLen(strBrachName) <= 0 Then
                            strBranchCode = ""
                            strBrachName = ""
                        End If
                    End If
                    'strBranchCode = strIFSCCode
                    'strBrachName = clsCommon.myCstr(grow.Cells("Branch Name").Value)
                    'If clsCommon.myLen(strBrachName) > 100 Then
                    '    Throw New Exception("Branch Name should be max 100 character")
                    'End If
                    'If clsCommon.myLen(strbank) > 0 AndAlso clsDBFuncationality.getSingleValue("Select count(*) from TSPL_Vendor_Bank_Branch_Details where Bank_IFSC_Code ='" + strIFSCCode + "'  and TSPL_Vendor_Bank_Branch_Details.Bank_Code='" & strbank & "' and TSPL_Vendor_Bank_Branch_Details.Branch_Name='" & strBrachName & "'", trans) <= 0 Then
                    '    Throw New Exception("Branch Name Does Not Exist : " + strBrachName + " for bank " + strbank + "  .Please make entry in vendor bank master.")
                    'End If
                    ''-------------------------

                    '' Anubhooti 24-Sep-2014 (Bank Details From Vendor Bank Master)
                    If clsCommon.myLen(strbank) > 0 Then
                        Dim obj As clsVendorBankMaster = clsVendorBankMaster.GetData(strbank, NavigatorType.Current, trans)
                        If obj Is Nothing Then
                            Exit Sub
                        End If
                        strbankdes = obj.Bank_Name
                        strBName = obj.Bank_Name
                        'txtbankcountry.Text = obj.country_name
                        'txtbankstate.Text = obj.state_name
                        'txtbankcity.Text = obj.city_name
                        'richa Agarwal 23/03/2015
                        'strBranchCode = obj.Branch_Code
                        'strBrachName = obj.Branch_Name
                        'strIFSCCode = obj.IFSC_Code
                        ''--------------------
                    Else
                        strbankdes = ""
                        'txtbankcountry.Text = "India"
                        'strBrachName = ""
                        'strBranchCode = ""
                        strBName = ""
                        'strIFSCCode = ""
                    End If
                    ''

                    Dim strAccType As String = clsCommon.myCstr(grow.Cells("Account_Type").Value)
                    If (String.IsNullOrEmpty(strAccType)) Or clsCommon.myLen(strAccType) > 10 Then
                        Throw New Exception("Length of Account Type should be max. 10 character")
                    End If

                    If clsCommon.myLen(strAccType) > 0 Then
                        If clsCommon.CompairString(strAccType, "Cur") = CompairStringResult.Equal Or clsCommon.CompairString(strAccType, "Cre") = CompairStringResult.Equal Or clsCommon.CompairString(strAccType, "Oth") = CompairStringResult.Equal Or clsCommon.CompairString(strAccType, "Sav") = CompairStringResult.Equal Or clsCommon.CompairString(strAccType, "Cas") = CompairStringResult.Equal Or clsCommon.CompairString(strAccType, "Loa") = CompairStringResult.Equal Then
                        Else
                            Throw New Exception("Account Type For should be amoung 'Cur','Cas','Sav','Cre','Loa','Oth'")
                        End If
                    End If

                    Dim strTypeOfVen As String = clsCommon.myCstr(grow.Cells("Vendor_Type").Value)
                    If (String.IsNullOrEmpty(strTypeOfVen)) Or clsCommon.myLen(strTypeOfVen) > 10 Then
                        Throw New Exception("Length of Type Of Vendor should be max. 10 character")
                    End If

                    If clsCommon.myLen(strTypeOfVen) > 0 Then
                        If clsCommon.CompairString(strTypeOfVen, "A") = CompairStringResult.Equal Or clsCommon.CompairString(strTypeOfVen, "B") = CompairStringResult.Equal Or clsCommon.CompairString(strTypeOfVen, "O") = CompairStringResult.Equal Or clsCommon.CompairString(strTypeOfVen, "N") = CompairStringResult.Equal Then
                        Else
                            Throw New Exception("Type Of Vendor should be amoung 'A','B','O','N'")
                        End If
                    End If
                    ''
                    '' Anubhooti 26-Aug-2014 BM00000003619 (Add Pin_Code In Update And Insert Command)
                    Dim strPin_Code As String = clsCommon.myCstr(grow.Cells("Pin_Code").Value)
                    If clsCommon.myLen(strPin_Code) > 0 Then
                        If clsCommon.myLen(strPin_Code) > 6 Then
                            Throw New Exception("Length of Pin Code should be max. 6 character")
                        End If
                    End If

                    ''
                    '' Anubhooti 24-Sep-2014
                    Dim IsTDSApp As String = clsCommon.myCstr(grow.Cells("Is TDS Applicable").Value)
                    If clsCommon.myLen(IsTDSApp) > 0 Then
                        If clsCommon.CompairString(IsTDSApp, "0") = CompairStringResult.Equal Or clsCommon.CompairString(IsTDSApp, "1") = CompairStringResult.Equal Then
                            If clsVendorMaster.checkisIDSapplicable(strgroupCode, trans) AndAlso (clsCommon.CompairString(IsTDSApp, "0") = CompairStringResult.Equal) Then
                                Throw New Exception("Is TDS Applicable must be '1' for Vendor group '" + strgroupCode + "'")
                            End If
                        Else
                            Throw New Exception("Is TDS Applicable should be amoung '0','1'")
                        End If
                    Else
                        Throw New Exception("Is TDS Applicable should be amoung '0','1'")
                    End If


                    Dim strTDSStateCode As String = clsCommon.myCstr(grow.Cells("TDS State Code").Value)
                    If clsCommon.myLen(IsTDSApp) > 0 Then
                        If clsCommon.CompairString(IsTDSApp, "1") = CompairStringResult.Equal Then
                            If clsCommon.myLen(strTDSStateCode) > 0 Then
                                Dim qryTDSState As String = "select Count(*) As Row from TSPL_STATE_MASTER where State_Code ='" & strTDSStateCode & "'"
                                Dim checkTDSState As Integer = clsDBFuncationality.getSingleValue(qryTDSState, trans)
                                If checkTDSState <= 0 Then
                                    Throw New Exception("Filled TDS state code does not exist" + Environment.NewLine + ".First make the entry for state code")
                                End If
                                strTDSStateCode = "'" & strTDSStateCode & "'"
                            Else
                                strTDSStateCode = "Null"
                            End If
                        ElseIf clsCommon.CompairString(IsTDSApp, "0") = CompairStringResult.Equal Then
                            strTDSStateCode = "Null"
                        End If
                    End If

                    Dim strTDSState As String = clsCommon.myCstr(grow.Cells("TDS Status").Value)
                    If clsCommon.myLen(IsTDSApp) > 0 Then
                        If clsCommon.CompairString(IsTDSApp, "1") = CompairStringResult.Equal Then
                            If clsCommon.CompairString(strTDSState, "Resident") = CompairStringResult.Equal Or clsCommon.CompairString(strTDSState, "Non Resident") = CompairStringResult.Equal Then
                            Else
                                Throw New Exception("TDS Status should be amoung 'Resident','Non Resident'")
                            End If
                        ElseIf clsCommon.CompairString(IsTDSApp, "0") = CompairStringResult.Equal Then
                            strTDSState = ""
                        End If
                    End If

                    Dim strTDSVenType As String = clsCommon.myCstr(grow.Cells("TDS Vendor Type").Value)
                    If clsCommon.myLen(IsTDSApp) > 0 Then
                        If clsCommon.CompairString(IsTDSApp, "1") = CompairStringResult.Equal Then
                            If clsCommon.CompairString(strTDSVenType, "Individual") = CompairStringResult.Equal Or clsCommon.CompairString(strTDSVenType, "Undevided Family") = CompairStringResult.Equal Or clsCommon.CompairString(strTDSVenType, "Partnership Firm") = CompairStringResult.Equal Or clsCommon.CompairString(strTDSVenType, "Domestic Company") = CompairStringResult.Equal Or clsCommon.CompairString(strTDSVenType, "Co-Operative Society") = CompairStringResult.Equal Or clsCommon.CompairString(strTDSVenType, "Local Authority") = CompairStringResult.Equal Then
                            Else
                                Throw New Exception("TDS Vendor Type should be amoung 'Individual','Undevided Family','Partnership Firm','Domestic Company','Co-Operative Society','Local Authority'")
                            End If
                        ElseIf clsCommon.CompairString(IsTDSApp, "0") = CompairStringResult.Equal Then
                            strTDSVenType = ""
                        End If
                    End If

                    Dim strDedCode As String = clsCommon.myCstr(grow.Cells("Deduction Code").Value)
                    If clsCommon.myLen(IsTDSApp) > 0 Then
                        If clsCommon.CompairString(IsTDSApp, "1") = CompairStringResult.Equal Then
                            If clsCommon.myLen(strDedCode) > 0 Then
                                Dim qryTDSDed As String = "select Count(*) As Row from TSPL_TDS_DEDUCTION_HEAD Where deduction_code='" & strDedCode & "'"
                                Dim checkDed As Integer = clsDBFuncationality.getSingleValue(qryTDSDed, trans)
                                If checkDed <= 0 Then
                                    Throw New Exception("Filled deduction code does not exist" + Environment.NewLine + ".First make the entry for deduction code")
                                End If
                            Else
                                Throw New Exception("Deduction code can not be left blank")
                            End If
                            strDedCode = "'" & strDedCode & "'"
                        ElseIf clsCommon.CompairString(IsTDSApp, "0") = CompairStringResult.Equal Then
                            strDedCode = "Null"
                        End If
                    End If

                    Dim strTDSBranch As String = clsCommon.myCstr(grow.Cells("TDS Branch Code").Value)
                    If clsCommon.myLen(IsTDSApp) > 0 Then
                        If clsCommon.CompairString(IsTDSApp, "1") = CompairStringResult.Equal Then
                            If clsCommon.myLen(strTDSBranch) > 0 Then
                                Dim qryTDSBranch As String = "select Count(*) As Row from TSPL_TDS_BRANCH_MASTER where Branch_Code='" & strTDSBranch & "'"
                                Dim checkBranch As Integer = clsDBFuncationality.getSingleValue(qryTDSBranch, trans)
                                If checkBranch <= 0 Then
                                    Throw New Exception("Filled TDS branch code does not exist" + Environment.NewLine + ".First make the entry for TDS branch code")
                                End If
                                strTDSBranch = "'" & strTDSBranch & "'"
                            Else
                                'Throw New Exception("TDS Branch code can not be left blank ")
                                strTDSBranch = "Null"
                            End If

                        ElseIf clsCommon.CompairString(IsTDSApp, "0") = CompairStringResult.Equal Then
                            strTDSBranch = "Null"
                        End If
                    End If
                    If clsCommon.CompairString(IsTDSApp, "1") = CompairStringResult.Equal Then
                        qryNatureDed = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Non_PAN_No From TSPL_TDS_DEDUCTION_HEAD where Deduction_Code =" & strDedCode & "", trans))
                        If clsCommon.CompairString(qryNatureDed.ToUpper().Trim(), "Y") = CompairStringResult.Equal AndAlso clsCommon.myLen(clsCommon.myCstr(grow.Cells("PAN").Value)) > 0 Then
                            Throw New Exception("You can not make this entry with Non PAN nature of deduction as PAN No exists.")
                        End If
                    End If



                    Dim strTDSStateCodeService As String = clsCommon.myCstr(grow.Cells("Service TDS State Code").Value)
                    If clsCommon.myLen(IsTDSApp) > 0 Then
                        If clsCommon.CompairString(IsTDSApp, "1") = CompairStringResult.Equal Then
                            If clsCommon.myLen(strTDSStateCodeService) > 0 Then
                                Dim qryTDSState As String = "select Count(*) As Row from TSPL_STATE_MASTER where State_Code ='" & strTDSStateCodeService & "'"
                                Dim checkTDSState As Integer = clsDBFuncationality.getSingleValue(qryTDSState, trans)
                                If checkTDSState <= 0 Then
                                    Throw New Exception("Filled service TDS state code does not exist" + Environment.NewLine + ".First make the entry for state code")
                                End If
                                strTDSStateCodeService = "'" & strTDSStateCodeService & "'"
                            Else
                                strTDSStateCodeService = "Null"
                            End If
                        ElseIf clsCommon.CompairString(IsTDSApp, "0") = CompairStringResult.Equal Then
                            strTDSStateCodeService = "Null"
                        End If
                    End If

                    Dim strTDSStateService As String = clsCommon.myCstr(grow.Cells("Service TDS Status").Value)
                    If clsCommon.myLen(IsTDSApp) > 0 Then
                        If clsCommon.CompairString(IsTDSApp, "1") = CompairStringResult.Equal Then
                            If clsCommon.CompairString(strTDSStateService, "Resident") = CompairStringResult.Equal Or clsCommon.CompairString(strTDSStateService, "Non Resident") = CompairStringResult.Equal Then
                            Else
                                Throw New Exception("service TDS Status should be amoung 'Resident','Non Resident'")
                            End If
                        ElseIf clsCommon.CompairString(IsTDSApp, "0") = CompairStringResult.Equal Then
                            strTDSStateService = ""
                        End If
                    End If

                    Dim strTDSVenTypeService As String = clsCommon.myCstr(grow.Cells("Service TDS Vendor Type").Value)
                    If clsCommon.myLen(IsTDSApp) > 0 Then
                        If clsCommon.CompairString(IsTDSApp, "1") = CompairStringResult.Equal Then
                            If clsCommon.CompairString(strTDSVenTypeService, "Individual") = CompairStringResult.Equal Or clsCommon.CompairString(strTDSVenTypeService, "Undevided Family") = CompairStringResult.Equal Or clsCommon.CompairString(strTDSVenTypeService, "Partnership Firm") = CompairStringResult.Equal Or clsCommon.CompairString(strTDSVenTypeService, "Domestic Company") = CompairStringResult.Equal Or clsCommon.CompairString(strTDSVenTypeService, "Co-Operative Society") = CompairStringResult.Equal Or clsCommon.CompairString(strTDSVenTypeService, "Local Authority") = CompairStringResult.Equal Then
                            Else
                                Throw New Exception("TDS Vendor Type should be amoung 'Individual','Undevided Family','Partnership Firm','Domestic Company','Co-Operative Society','Local Authority'")
                            End If
                        ElseIf clsCommon.CompairString(IsTDSApp, "0") = CompairStringResult.Equal Then
                            strTDSVenTypeService = ""
                        End If
                    End If

                    Dim strDedCodeService As String = clsCommon.myCstr(grow.Cells("Deduction Code").Value)
                    If clsCommon.myLen(IsTDSApp) > 0 Then
                        If clsCommon.CompairString(IsTDSApp, "1") = CompairStringResult.Equal Then
                            If clsCommon.myLen(strDedCodeService) > 0 Then
                                Dim qryTDSDed As String = "select Count(*) As Row from TSPL_TDS_DEDUCTION_HEAD Where deduction_code='" & strDedCodeService & "'"
                                Dim checkDed As Integer = clsDBFuncationality.getSingleValue(qryTDSDed, trans)
                                If checkDed <= 0 Then
                                    Throw New Exception("Filled Service deduction code does not exist" + Environment.NewLine + ".First make the entry for deduction code")
                                End If
                            Else
                                Throw New Exception("Service Deduction code can not be left blank")
                            End If
                            strDedCodeService = "'" & strDedCodeService & "'"
                        ElseIf clsCommon.CompairString(IsTDSApp, "0") = CompairStringResult.Equal Then
                            strDedCodeService = "Null"
                        End If
                    End If

                    Dim strTDSBranchService As String = clsCommon.myCstr(grow.Cells("Service TDS Branch Code").Value)
                    If clsCommon.myLen(IsTDSApp) > 0 Then
                        If clsCommon.CompairString(IsTDSApp, "1") = CompairStringResult.Equal Then
                            If clsCommon.myLen(strTDSBranchService) > 0 Then
                                Dim qryTDSBranch As String = "select Count(*) As Row from TSPL_TDS_BRANCH_MASTER where Branch_Code='" & strTDSBranchService & "'"
                                Dim checkBranch As Integer = clsDBFuncationality.getSingleValue(qryTDSBranch, trans)
                                If checkBranch <= 0 Then
                                    Throw New Exception("Filled Service TDS branch code does not exist" + Environment.NewLine + ".First make the entry for TDS branch code")
                                End If
                                strTDSBranchService = "'" & strTDSBranchService & "'"
                            Else
                                'Throw New Exception("TDS Branch code can not be left blank ")
                                strTDSBranchService = "Null"
                            End If

                        ElseIf clsCommon.CompairString(IsTDSApp, "0") = CompairStringResult.Equal Then
                            strTDSBranchService = "Null"
                        End If
                    End If








                    ''
                    '' Anubhooti 20-Oct-2014 BM00000004198
                    If clsCommon.myLen(clsCommon.myCstr(grow.Cells("State_Code").Value)) > 0 AndAlso clsCommon.myLen(clsCommon.myCstr(grow.Cells("TDS State Code").Value)) > 0 Then
                        If clsCommon.CompairString(clsCommon.myCstr(grow.Cells("State_Code").Value), clsCommon.myCstr(grow.Cells("TDS State Code").Value)) <> CompairStringResult.Equal Then
                            Throw New Exception("State code and TDS state code should be same.")
                        End If
                    End If
                    '' Anubhooti 01-Nov-2104 (State_Code and TDS_State_Code must have same value)
                    Dim StateCodeCommon As String
                    If clsCommon.myLen(clsCommon.myCstr(grow.Cells("State_Code").Value)) > 0 Then
                        StateCodeCommon = "'" & clsCommon.myCstr(grow.Cells("State_Code").Value) & "'"
                    ElseIf clsCommon.myLen(clsCommon.myCstr(grow.Cells("TDS State Code").Value)) > 0 Then
                        StateCodeCommon = "'" & clsCommon.myCstr(grow.Cells("TDS State Code").Value) & "'"
                    Else
                        StateCodeCommon = "NULL"
                    End If

                    Dim CURRENCYCode As String
                    Dim CURRENCY_CODE As String = clsCommon.myCstr(grow.Cells("CURRENCY CODE").Value)

                    If clsCommon.myLen(CURRENCY_CODE) <= 0 Then
                        CURRENCY_CODE = clsCommon.myCstr(objCommonVar.BaseCurrencyCode)
                    End If

                    If clsCommon.myLen(CURRENCY_CODE) > 0 Then
                        CURRENCYCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select CURRENCY_CODE  from TSPL_CURRENCY_MASTER Where CURRENCY_CODE='" + CURRENCY_CODE + "'", trans))
                        If clsCommon.CompairString(CURRENCY_CODE, CURRENCYCode) = CompairStringResult.Equal Then
                            '' when vendor currency is other than base currency of the company
                            '' match account set currency with vendor currency
                            'Dim qry As String
                            qry = "select CURRENCY_CODE from TSPL_VENDOR_ACCOUNT_SET where Acct_Set_Code='" & clsCommon.myCstr(vendoracct) & "' "
                            Dim accCurrCode As String = clsDBFuncationality.getSingleValue(qry, trans).ToString
                            If clsCommon.CompairString(accCurrCode, clsCommon.myCstr(CURRENCY_CODE)) <> CompairStringResult.Equal Then
                                clsCommon.MyMessageBoxShow("Account Set Currency and Vendor Currency must be same in case of Multicurrency.")
                                Exit Sub
                            End If
                            '' match tax Group currency with vendor currency
                            qry = " select TSPL_TAX_GROUP_DETAILS.Tax_Code,coalesce(TSPL_TAX_MASTER.CURRENCY_CODE,'') as CURRENCY_CODE from TSPL_TAX_GROUP_DETAILS " &
                                  " inner join TSPL_TAX_GROUP_MASTER on TSPL_TAX_GROUP_DETAILS.Tax_Group_Code=TSPL_TAX_GROUP_MASTER.Tax_Group_Code " &
                                  " inner join TSPL_TAX_MASTER on TSPL_TAX_GROUP_DETAILS.Tax_Code=TSPL_TAX_MASTER.Tax_Code where TSPL_TAX_GROUP_MASTER.Tax_Group_Code='" & clsCommon.myCstr(strTax) & "' " &
                                  " and coalesce(TSPL_TAX_MASTER.CURRENCY_CODE,'')<>'" & clsCommon.myCstr(CURRENCY_CODE) & "'"
                            Dim dt1 As DataTable
                            dt1 = clsDBFuncationality.GetDataTable(qry, trans)
                            Dim taxCode As String = ""
                            For Each dr As DataRow In dt1.Rows
                                If dt1.Rows.IndexOf(dr) = 0 Then
                                    taxCode = dr.Item("Tax_Code")
                                Else
                                    taxCode = taxCode & "," & dr.Item("Tax_Code")
                                End If
                            Next
                            If clsCommon.myLen(taxCode) > 0 Then
                                clsCommon.MyMessageBoxShow("Tax Code '" & taxCode & "' in Tax Group " & clsCommon.myCstr(strTax) & " are created for currency other than " & clsCommon.myCstr(CURRENCY_CODE) & " .")
                                Exit Sub
                            End If
                            'End If
                        Else
                            Throw New Exception("This Currency Code Does not Exist in Currency Master")
                        End If
                    End If
                    Dim PC_CODE As String = clsCommon.myCstr(grow.Cells("Payment Cycle").Value)
                    If clsCommon.myLen(PC_CODE) > 0 Then
                        Dim PaymentCycle As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select COUNT(*) from  TSPL_PAYMENT_CYCLE_MASTER   where PC_CODE ='" + PC_CODE + "'", trans))
                        If PaymentCycle = 0 Then
                            Throw New Exception("Payment Cycle Does Not Exist : " + strTax + "")
                        End If
                    End If
                    Dim Vendor_Distance As String = clsCommon.myCstr(grow.Cells("Vendor Distance").Value)
                    '' Anubhooti 02-Sep-2014 Duplication of vendor desp
                    'Dim dt1 As DataTable
                    'dt1 = clsDBFuncationality.GetDataTable("Select * From TSPL_VENDOR_MASTER Where (((ISNULL( ECC,'')='" & clsCommon.myCstr(grow.Cells("Ecc").Value).Trim() & "' and LEN(ISNULL( ECC,'')) > 0))  or ((ISNULL(Email,'')='" & emailid.Trim() & "' ANd ISNULL(Email,'')<>'' )) or ((ISNULL(Tin_No,'')='" & grow.Cells("Tin No").Value.ToString().Trim() & "' AND ISNULL(Tin_No,'')<>'' )) or ((ISNULL(Contact_Person_Email,'')='" & contct_person_email.Trim() & "' ANd ISNULL(Contact_Person_Email,'')<>'' )) ) and (TSPL_VENDOR_MASTER.Vendor_Code not in ('" & strvendorNo & "'))", trans)
                    'If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
                    '    clsCommon.ProgressBarHide()
                    '    If common.clsCommon.MyMessageBoxShow("Vendor (" & strvendorNo & ") exists with same vendor description.Do you still want to continue ", Me.Text, MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
                    '        clsCommon.ProgressBarShow()
                    '    Else
                    '        clsCommon.ProgressBarShow()
                    '        Continue For
                    '    End If
                    'End If
                    '' Anubhooti 06-Jan-2014 (Auto TC Certificate)
                    Dim Is_TC_Certi As String = ""
                    Dim TC_Certi As String = ""
                    If clsCommon.CompairString(IndustryType, "A") = CompairStringResult.Equal Then
                        Is_TC_Certi = clsCommon.myCstr(grow.Cells("Is TC Certified").Value)
                        TC_Certi = clsCommon.myCstr(grow.Cells("TC Certified").Value)
                        If clsCommon.myLen(Is_TC_Certi) > 0 Then
                            If clsCommon.CompairString(Is_TC_Certi, "0") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(Is_TC_Certi, "1") <> CompairStringResult.Equal > 0 Then
                                Throw New Exception("Please check ! Is TC certified should be '1' or '0'")
                            End If
                        Else
                            Throw New Exception("Please check ! Is TC certified should be '1' or '0'")
                        End If
                        If clsCommon.CompairString(Is_TC_Certi, "1") = CompairStringResult.Equal AndAlso clsCommon.myLen(TC_Certi) <= 0 Then
                            Throw New Exception("Please fill TC certificate")
                        End If
                        If clsCommon.CompairString(Is_TC_Certi, "0") = CompairStringResult.Equal AndAlso clsCommon.myLen(TC_Certi) > 0 Then
                            Throw New Exception("Please check ! Is TC certified should be '1'")
                        End If
                        If clsCommon.myLen(TC_Certi) > 50 Then
                            Throw New Exception("Please check ! TC certified should not be more than 50 characters.")
                        End If
                    End If

                    If clsCommon.CompairString(clsCommon.myCstr(IsBlacklist), "Y") = CompairStringResult.Equal Then
                        IsBlacklisted = 1
                    ElseIf clsCommon.CompairString(clsCommon.myCstr(IsBlacklist), "N") = CompairStringResult.Equal Then
                        IsBlacklisted = 0
                    Else
                        Throw New Exception("Fill 'Is Blacklist' in 'Y/N' format")
                    End If

                    If clsCommon.CompairString(clsCommon.myCstr(IsEmployeeD), "Y") = CompairStringResult.Equal Then
                        IsEmployee = 1
                        If clsCommon.myLen(EmployeeCode) > 0 Then
                            Dim StrEmpExist As Integer = 0
                            StrEmpExist = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*) as c from TSPL_EMPLOYEE_MASTER where isnull(EMP_CODE,'')='" + clsCommon.myCstr(EmployeeCode) + "'", trans))
                            If StrEmpExist <= 0 Then
                                Throw New Exception("Wrong Employee code.")
                            End If
                        End If
                    ElseIf clsCommon.CompairString(clsCommon.myCstr(IsEmployeeD), "N") = CompairStringResult.Equal Then
                        IsEmployee = 0
                    Else
                        Throw New Exception("Fill 'Is Employee' in 'Y/N' format")
                    End If

                    Dim PanNo As String = ""
                    Dim GSTSate_COde As String = ""
                    ''-----GST Import Vedor Master
                    If AllowGSTApplicable = True Then
                        Registered = clsCommon.myCstr(grow.Cells("GST Register").Value)
                        If clsCommon.myLen(Registered) > 0 Then
                            If Registered = 1 Then
                                Registered = 1
                            Else
                                Registered = 0
                            End If
                        Else
                            Throw New Exception("Fill GST Registered")
                        End If
                        DFoption = clsCommon.myCstr(grow.Cells("Domestic Foreign").Value)
                        If clsCommon.myLen(DFoption) > 0 Then
                            If DFoption = "Domestic" Then
                                DFoption = "Domestic"
                            Else
                                DFoption = "Foreign"
                            End If
                        Else
                            Throw New Exception("Fill Option Foreign/Domestic")
                        End If
                        BusinessCondition = clsCommon.myCstr(grow.Cells("Business Condition").Value)

                        '   GSTMiddleEntity = clsCommon.myCstr(grow.Cells("GST Middle Entity").Value)
                        '' changed by Panch Raj as per discussion with Ranjana Mam

                        GST_Composition_scheme = clsCommon.myCstr(grow.Cells("GST Composition Scheme").Value)
                        If GST_Composition_scheme = 1 Then
                            GST_Composition_scheme = 1
                        Else
                            GST_Composition_scheme = 0
                        End If

                        GSTFinal = ""
                        GSTEntity = ""
                        GSTMiddleEntity = ""
                        GSTLastEntity = ""

                        If Registered = 1 Then
                            'GSTEntity = clsCommon.myCstr(grow.Cells("GST First Entity").Value)
                            'If clsCommon.myLen(GSTEntity) <= 0 Then
                            '    Throw New Exception("Fill GST First Entry Number.")
                            'End If
                            'GSTLastEntity = clsCommon.myCstr(grow.Cells("GST Last Entity").Value)
                            'If clsCommon.myLen(GSTLastEntity) <= 0 Then
                            '    Throw New Exception("Fill GST Last Digit Number.")
                            'End If
                            'GST_Composition_scheme = clsCommon.myCstr(grow.Cells("GST Composition Scheme").Value)
                            'If GST_Composition_scheme = 1 Then
                            '    GST_Composition_scheme = 1
                            'Else
                            '    GST_Composition_scheme = 0
                            'End If
                            PanNo = clsCommon.myCstr(grow.Cells("Pan").Value)

                            If clsCommon.myLen(PanNo) <= 0 Then
                                Throw New Exception("Fill Pan Number.")
                            End If
                            Dim check As String = clsDBFuncationality.getSingleValue("select case when isnull(GST_State_Code,'')='' then '' else GST_State_Code end as GST_State_Code from tspl_state_master where state_code='" & statecode & "'", trans)
                            If clsCommon.myLen(check) > 0 Then
                                GSTSate_COde = check
                            Else
                                Throw New Exception("Mapped GST State Code in State Master")
                            End If
                            GSTFinal = clsCommon.myCstr(grow.Cells("GSTIN No").Value)
                            Dim StrMsg As String = clsERPFuncationality.ValidationGSTNO(GSTSate_COde, PanNo, GSTFinal, trans)
                            If clsCommon.myCstr(StrMsg) = "False" Then
                                Exit Sub
                            End If
                            GSTEntity = GSTFinal.Trim().Substring(12, 1)
                            GSTMiddleEntity = GSTFinal.Trim().Substring(13, 1)
                            GSTLastEntity = GSTFinal.Trim().Substring(14, 1)
                        End If
                    End If

                    ''---End

                    ''
                    '*************************************************************
                    Dim isHighClass As String = clsCommon.myCstr(grow.Cells("High Class").Value)
                    If clsCommon.CompairString(clsCommon.myCstr(isHighClass), "Y") = CompairStringResult.Equal Or clsCommon.CompairString(clsCommon.myCstr(isHighClass), "1") = CompairStringResult.Equal Then
                        isHighClass = 1
                    ElseIf clsCommon.CompairString(clsCommon.myCstr(IsEmployeeD), "N") = CompairStringResult.Equal Or clsCommon.CompairString(clsCommon.myCstr(isHighClass), "0") = CompairStringResult.Equal Then
                        isHighClass = 0
                    Else
                        Throw New Exception("Fill 'High Class' in 'Y/N/0/1' format")
                    End If

                    '===================== buyer file return in last two years, TCS/TDS amount is greater than 50K in previous Year
                    Dim buyerfilereturninlasttwoyears As String = clsCommon.myCstr(grow.Cells("buyer file return in last two years").Value)
                    If clsCommon.CompairString(buyerfilereturninlasttwoyears, "Y") = CompairStringResult.Equal OrElse clsCommon.CompairString(buyerfilereturninlasttwoyears, "Yes") = CompairStringResult.Equal OrElse clsCommon.CompairString(buyerfilereturninlasttwoyears, "1") = CompairStringResult.Equal Then
                        buyerfilereturninlasttwoyears = 1
                    ElseIf clsCommon.CompairString(buyerfilereturninlasttwoyears, "N") = CompairStringResult.Equal OrElse clsCommon.CompairString(buyerfilereturninlasttwoyears, "No") = CompairStringResult.Equal OrElse clsCommon.CompairString(buyerfilereturninlasttwoyears, "0") = CompairStringResult.Equal OrElse clsCommon.CompairString(buyerfilereturninlasttwoyears, "") = CompairStringResult.Equal Then
                        buyerfilereturninlasttwoyears = 0
                    Else
                        Throw New Exception("Please Enter Only Y/Yes/1 or N/No/0 as [buyer file return in last two years] Field")
                    End If
                    Dim TCSTDSamountisgreaterthan50KinpreviousYear As String = clsCommon.myCstr(grow.Cells("TCS/TDS amount is greater than 50K in previous Year").Value)
                    If clsCommon.CompairString(TCSTDSamountisgreaterthan50KinpreviousYear, "Y") = CompairStringResult.Equal OrElse clsCommon.CompairString(TCSTDSamountisgreaterthan50KinpreviousYear, "Yes") = CompairStringResult.Equal OrElse clsCommon.CompairString(TCSTDSamountisgreaterthan50KinpreviousYear, "1") = CompairStringResult.Equal Then
                        TCSTDSamountisgreaterthan50KinpreviousYear = 1
                    ElseIf clsCommon.CompairString(TCSTDSamountisgreaterthan50KinpreviousYear, "N") = CompairStringResult.Equal OrElse clsCommon.CompairString(TCSTDSamountisgreaterthan50KinpreviousYear, "No") = CompairStringResult.Equal OrElse clsCommon.CompairString(TCSTDSamountisgreaterthan50KinpreviousYear, "0") = CompairStringResult.Equal OrElse clsCommon.CompairString(TCSTDSamountisgreaterthan50KinpreviousYear, "") = CompairStringResult.Equal Then
                        TCSTDSamountisgreaterthan50KinpreviousYear = 0
                    Else
                        Throw New Exception("Please Enter Only Y/Yes/1 or N/No/0 as [TCS/TDS amount is greater than 50K in previous Year] Field")
                    End If

                    '=====================
                    Dim IsAllowSkipPurchaseQC As String = clsCommon.myCstr(grow.Cells("Allow Skip Purchase QC").Value)
                    If clsCommon.CompairString(IsAllowSkipPurchaseQC, "Y") = CompairStringResult.Equal OrElse clsCommon.CompairString(IsAllowSkipPurchaseQC, "Yes") = CompairStringResult.Equal OrElse clsCommon.CompairString(IsAllowSkipPurchaseQC, "1") = CompairStringResult.Equal Then
                        IsAllowSkipPurchaseQC = 1
                    ElseIf clsCommon.CompairString(IsAllowSkipPurchaseQC, "N") = CompairStringResult.Equal OrElse clsCommon.CompairString(IsAllowSkipPurchaseQC, "No") = CompairStringResult.Equal OrElse clsCommon.CompairString(IsAllowSkipPurchaseQC, "0") = CompairStringResult.Equal OrElse clsCommon.CompairString(IsAllowSkipPurchaseQC, "") = CompairStringResult.Equal Then
                        IsAllowSkipPurchaseQC = 0
                    Else
                        Throw New Exception("Please Enter Only Y/Yes/1 or N/No/0 as [Allow Skip Purchase QC] Field")
                    End If
                    Dim isPendingInvoice As Boolean = clsCommon.myCBool(clsDBFuncationality.getSingleValue("select count (*) from TSPL_BULK_MILK_PURCHASE_INVOICE_head where Vendor_Code = '" + strvendorNo + "' and isPosted = 0", trans))
                    Dim BeforeHighclassValue As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select isHighClass from TSPL_VENDOR_MASTER where Vendor_Code = '" + strvendorNo + "' ", trans))
                    Dim AfterHighclassValue As Integer = isHighClass
                    Dim invoiceNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select STUFF((Select ',['+Doc_No+']'  from (select TSPL_BULK_MILK_PURCHASE_INVOICE_head.Doc_No from TSPL_BULK_MILK_PURCHASE_INVOICE_head  where Vendor_Code = '" + strvendorNo + "' and isPosted = 0 ) XXX For XML Path('')),1,1,'') ", trans))
                    If BeforeHighclassValue <> AfterHighclassValue AndAlso isPendingInvoice = True Then
                        Throw New Exception("You can change hight Class after Post all Pending document (" + invoiceNo + ") of Bulk Milk Purchase Invoice against " + strvendorNo + " Vendor.")

                    End If

                    '======================

                    Dim strBulkRouteNo As String = clsCommon.myCstr(grow.Cells("Bulk Route No").Value)
                    If clsCommon.myLen(strBulkRouteNo) > 0 Then
                        Dim chkBulkRoute As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*) from TSPL_BULK_ROUTE_MASTER where Route_No ='" + strBulkRouteNo + "'", trans))
                        If chkBulkRoute <= 0 Then
                            Throw New Exception("Invalid Bulk Route No ")
                        End If
                    Else
                        strBulkRouteNo = ""
                    End If
                    '**************************************************************

                    Dim QryToGetOutAmt As String = ""
                    Dim OutStandAmt As Double = 0

                    QryToGetOutAmt = "Select Vendor_Code, MAX(Vendor_Name) as Vendor_Name, SUM([Due Amount]) as [Due Amount] from (" &
                    " select xxx.Vendor_Code, TSPL_VENDOR_MASTER.Vendor_Name, xxx.DocNo as [Document Id], Case when Document_Type IN ('D','AV','OA') then [Document_Total]*-1 Else [Document_Total] End as [Due Amount], xxx.DocDate as [Document Date], case when Document_Type='I' then 'IN' when Document_Type='PY' then 'PY' when Document_Type='D' then 'DR' when Document_Type='C' then 'CR' when Document_Type='AV' then 'AV' when Document_Type='OA' then 'OA'when Document_Type='PY' then 'PY' when Document_Type='RC' then 'RC' end as [Document_Type] FROM (   " &
                    " select Vendor_code, Document_No as DocNo , Document_Type , COnvert(Date,Invoice_Entry_Date, 103) as DocDate , (Case When TSPL_VENDOR_INVOICE_HEAD.is_For_TDS=1 Then TSPL_VENDOR_INVOICE_HEAD.Document_Total Else TSPL_VENDOR_INVOICE_HEAD.Document_Total-TDS_Actual_Amount-ISNULL((Select Balance_Amt from TSPL_VENDOR_INVOICE_HEAD as VH Where VH.Vendor_Code=TSPL_VENDOR_INVOICE_HEAD.Vendor_Code AND VH.Vendor_Invoice_No=TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_No AND VH.Document_Type IN ('D','C') AND ISNULL(VH.Against_PurchaseReturn_No,'')<>''),0) End)-ISNULL((Select SUM(Applied_Amount) from TSPL_PAYMENT_DETAIL LEFT OUTER JOIN TSPL_PAYMENT_HEADER ON TSPL_PAYMENT_HEADER.Payment_No=TSPL_PAYMENT_DETAIL.Payment_No Where TSPL_PAYMENT_HEADER.Posted=1 AND TSPL_PAYMENT_HEADER.IsChkReverse<>'Y' AND TSPL_PAYMENT_DETAIL.Document_No=TSPL_VENDOR_INVOICE_HEAD.Document_No AND  CONVERT(DATE,TSPL_PAYMENT_HEADER.Payment_Date,103)<=CONVERT(DATE,'02/09/2014',103)),0) as [Document_Total], TSPL_VENDOR_INVOICE_HEAD.Comp_Code, TSPL_VENDOR_INVOICE_HEAD.Loc_Code AS Location  from TSPL_VENDOR_INVOICE_HEAD  where ISNULL(tspl_vendor_invoice_head.Posting_Date,'')<> '' And (Len(ISNULL(Against_POInvoice_No, ''))<=0 AND Len(ISNULL(Against_PurchaseReturn_No, ''))<=0) or Document_Type='I'  and TSPL_VENDOR_INVOICE_HEAD.Posting_Date <> ''   " &
                    " UNION ALL  " &
                    " select  Vendor_code, TSPL_PAYMENT_HEADER.Payment_No as DocNo ,TSPL_PAYMENT_HEADER.Payment_Type as Document_Type , Convert(Date,Payment_Date, 103) as DocDate , Payment_Amount+TDS_Amount-isnull((case when TSPL_PAYMENT_HEADER.Payment_Type in('AV','OA','RC') then (select Amount from TSPL_BANK_REVERSE where Source_Type='AP' and Document_No=TSPL_PAYMENT_HEADER.Payment_No AND TSPL_BANK_REVERSE.Post<>'N' AND TSPL_BANK_REVERSE.Reversal_Date<=Convert(Date,'02/09/2014', 103)) else 0 end ),0) as [Document_Total], TSPL_PAYMENT_HEADER.Comp_Code, RIGHT(TSPL_BANK_MASTER.BANKACC , 3) as Location  from TSPL_PAYMENT_HEADER  LEFT OUTER JOIN TSPL_BANK_MASTER ON TSPL_PAYMENT_HEADER.Bank_Code= TSPL_BANK_MASTER.BANK_CODE where TSPL_PAYMENT_HEADER.Payment_Type NOT IN ('PY','MI') And (Posted='P' or Posted='1')    " &
                    " UNION ALL  " &
                    " select  VC_Code as Vendor_code, Document_No as DocNo, case when Amount_Type='Cr' then 'D' else  'C' end as Document_Type,CONVERT(Date,TSPL_VCGL_Head.Document_Date,103) as DocDate, Amount as [Document_Total], TSPL_VCGL_Head.Comp_Code ,  TSPL_VCGL_Head.Location_Segment as Location  from TSPL_VCGL_Head  where Document_Type='v' and TSPL_VCGL_Head.Status='1'    " &
                    " UNION ALL  " &
                    " select  TSPL_VCGL_Detail.VCGL_Code  as Vendor_code, TSPL_VCGL_Detail.Document_No as DocNo, case when TSPL_VCGL_Detail.Dr_Amount=0.0 then 'C' when TSPL_VCGL_Detail.Cr_Amount=0.0 then 'D' end as Document_Type, CONVERT(Date,TSPL_VCGL_Head.Document_Date,103) as DocDate, case when TSPL_VCGL_Detail.Dr_Amount=0.0 then TSPL_VCGL_Detail.Cr_Amount when TSPL_VCGL_Detail.Cr_Amount=0.0 then TSPL_VCGL_Detail.Dr_Amount end as [Document_Total], TSPL_VCGL_Head.Comp_Code ,  TSPL_VCGL_Head.Location_Segment as Location from  TSPL_VCGL_Detail  left outer join  TSPL_VCGL_Head on TSPL_VCGL_Head.Document_No=TSPL_VCGL_Detail.Document_No    where Row_Type='Vendor'  and TSPL_VCGL_Head.Status='1'   " &
                    " ) xxx Left Outer Join TSPL_COMPANY_MASTER ON xxx.Comp_Code=TSPL_COMPANY_MASTER.Comp_Code LEFT OUTER JOIN TSPL_VENDOR_MASTER ON xxx.Vendor_Code=TSPL_VENDOR_MASTER.Vendor_Code where Document_Total<>0  and Convert(Date, DocDate, 103) <=Convert(Date,'02/09/2014', 103)  and Document_Type  in ('I','C','D','AV','OA','P','RC' )  " &
                    " ) FINAL where Vendor_Code='" + strvendorNo + "'  Group By Vendor_Code"
                    Dim dt As DataTable
                    dt = clsDBFuncationality.GetDataTable(QryToGetOutAmt, trans)
                    If dt.Rows IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                        OutStandAmt = clsCommon.myCdbl(dt.Rows(0)("Due Amount").ToString())
                    End If
                    If OutStandAmt > 0 AndAlso clsCommon.CompairString(grow.Cells("Status").Value.ToString().ToUpper().Trim(), "Y") = CompairStringResult.Equal Then
                        Throw New Exception("You can not make this vendor Inactive because it has outstanding amount")
                    End If
                    ''
                    Dim PartyDetailsQry As String = ""
                    Dim TCCertiQry As String = ""
                    Dim GSTQry As String = ""
                    Dim strqry As String = ""

                    Dim sql1 As String = "select count(*) from TSPL_VENDOR_MASTER  where  vendor_code='" + strvendorNo + "'"
                    Dim i2 As Integer = CInt(connectSql.RunScalar(trans, sql1))
                    If (i2 = 0) Then

                        ''Map Employee
                        If IsEmployee = 1 AndAlso clsCommon.myLen(EmployeeCode) > 0 Then
                            Dim StrEmpExist As Integer = 0
                            StrEmpExist = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*) as c from tspl_vendor_master where isnull(EMP_CODE,'')='" + clsCommon.myCstr(EmployeeCode) + "'", trans))
                            If StrEmpExist > 0 Then
                                Throw New Exception("Employee already map with another vendor.")
                            End If
                        End If

                        ''Anubhooti 02-Sep-2014 AutoGenerate
                        AllowAutoVCode = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AutoGeneratedVendorCode, clsFixedParameterCode.AutoGeneratedVendorCode, trans))
                        AllowAutoVCodeForAllCompany = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AutoGeneratedVendorCodeForAllCompany, clsFixedParameterCode.AutoGeneratedVendorCodeForAllCompany, trans))
                        If clsCommon.CompairString(AllowAutoVCode, "1") = CompairStringResult.Equal AndAlso clsCommon.myLen(strvendorNo.Trim()) <= 0 Then
                            If clsCommon.CompairString(AllowAutoVCodeForAllCompany, "1") = CompairStringResult.Equal Then
                                strvendorNo = clsERPFuncationality.GetNextCode(trans, clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"), clsDocType.VendorMaster, "", "")
                            Else
                                strvendorNo = clsERPFuncationality.GetVendorNextCode("TSPL_VENDOR_MASTER", "Vendor_Name", strvendorname, trans)

                            End If


                        Else
                            strvendorNo = strvendorNo
                        End If

                        Dim strcmd As String = "Insert into TSPL_VENDOR_MASTER (Vendor_Code,Vendor_Name,Add1,Add2,Add3,Closing_Date ,Vendor_Group_Code,Vendor_Group_Code_Desc,City_Code ,City_Code_Desc,State,Country,Phone1,Phone2,Fax,Email,WebSite,Contact_Person_Name,Contact_Person_Phone ,Contact_Person_Fax ,Contact_Person_Website,Contact_Person_Email,Terms_Code,Terms_Code_Desc ,Vendor_Account,Vendor_Account_Desc,Payment_Code,Payment_Code_Desc,Bank_Code ,Bank_Code_Desc,Ven_Type_Code ,Ven_Type_Desc,Tax_Group ,Tax_Group_Desc,TAX1,TAX1_Rate,TAX2,TAX2_Rate,TAX3,TAX3_Rate,TAX4,TAX4_Rate,TAX5,TAX5_Rate,TAX6,TAX6_Rate,TAX7,TAX7_Rate,TAX8,TAX8_Rate,TAX9,TAX9_Rate,TAX10,TAX10_Rate,Service_Tax_No,Tin_No,Lst_No,Status,OnHold,Transporter,Remarks1,Remarks2,Additional1,Additional2,Additional3,created_by,created_date,modify_by,modify_date,comp_code,CST,ECC,Range,Collectorate,PAN,Inter_branch,franchise_yn,Form_Type,state_code,country_code,Is_Parent_Vendor,Parent_Vendor_Code,Category_Struct_Code,Branch_Name,Account_No,Bank_Name,IFSC_Code,Account_Type,vendor_type,Pin_Code,Is_TDS_Applicable ,TDS_State_Code ,TDS_Status ,TDS_Vendor_Type ,Deduction_Code ,TDS_Branch_Code,TDS_State_Code_Service ,TDS_Status_Service ,TDS_Vendor_Type_Service ,Deduction_Code_Service ,TDS_Branch_Code_Service,branch_code,Alies_Name,Vendor_Type_CHA,csa_type,Is_Chilling_Vendor,isVendorInvoiceNo,CHA_DOC_NO,Cheque_In_Favour_Of,CURRENCY_CODE,PC_CODE,OldName,SSI_No,Is_Blacklist,IsEmployee) values('" + strvendorNo + "','" + strvendorname + "','" + add1 + "','" + add2 + "','" + add3 + "','" + closing_date + "','" + strgroupCode + "','" + strgroupDes + "','" + citycode + "','" + citycodedesc + "','" + state + "','" + country + "','" + phonenum1 + "','" + phonenum2 + "','" + fax + "','" + emailid + "','" + website + "','" + contct_person_name + "','" + contct_perfson_phone + "','" + contct_person_fax + "','" + contct_person_website + "','" + contct_person_email + "','" + strtermcode + "','" + strtermdes + "','" + vendoracct + "','" + vendoracctdesc + "','" + paymenttype + "','" + paymenttypedesc + "','" + strbank + "','" + strbankdes + "','" + strvendortype + "','" + strvendortypedes + "','" + strTax + "','" + strtaxdes + "','" + grow.Cells("Tax1").Value.ToString() + "','" + clsCommon.myCstr(clsCommon.myCdbl(grow.Cells("Tax1 Rate").Value)) + "','" + grow.Cells("Tax2").Value.ToString() + "','" + clsCommon.myCstr(clsCommon.myCdbl(grow.Cells("Tax2 Rate").Value)) + "','" + grow.Cells("Tax3").Value.ToString() + "','" + clsCommon.myCstr(clsCommon.myCdbl(grow.Cells("Tax3 Rate").Value)) + "','" + grow.Cells("Tax4").Value.ToString() + "','" + clsCommon.myCstr(clsCommon.myCdbl(grow.Cells("Tax4 Rate").Value)) + "','" + grow.Cells("Tax5").Value.ToString() + "','" + clsCommon.myCstr(clsCommon.myCdbl(grow.Cells("Tax5 Rate").Value)) + "','" + grow.Cells("Tax6").Value.ToString() + "','" + clsCommon.myCstr(clsCommon.myCdbl(grow.Cells("Tax6 Rate").Value)) + "','" + grow.Cells("Tax7").Value.ToString() + "','" + clsCommon.myCstr(clsCommon.myCdbl(grow.Cells("Tax7 Rate").Value)) + "','" + grow.Cells("Tax8").Value.ToString() + "','" + clsCommon.myCstr(clsCommon.myCdbl(grow.Cells("Tax8 Rate").Value)) + "','" + grow.Cells("Tax9").Value.ToString() + "','" + clsCommon.myCstr(clsCommon.myCdbl(grow.Cells("Tax9 Rate").Value)) + "','" + grow.Cells("Tax10").Value.ToString() + "','" + clsCommon.myCstr(clsCommon.myCdbl(grow.Cells("Tax10 Rate").Value)) + "','" + grow.Cells("Service Tax No").Value.ToString() + "','" + grow.Cells("Tin No").Value.ToString() + "','" + grow.Cells("List No").Value.ToString() + "','" + grow.Cells("Status").Value.ToString() + "','" + grow.Cells("On Hold").Value.ToString() + "','" + grow.Cells("Transporter").Value.ToString() + "','" + grow.Cells("Remarks").Value.ToString() + "','" + grow.Cells("Remarks2").Value.ToString() + "','" + grow.Cells("Additional1").Value.ToString() + "','" + grow.Cells("Additional2").Value.ToString() + "','" + grow.Cells("Additional3").Value.ToString() + "','" + userCode + "','" + connectSql.serverDate(trans) + "','" + userCode + "','" + connectSql.serverDate(trans) + "','" + companyCode + "','" + grow.Cells("CST").Value.ToString() + "','" + grow.Cells("Ecc").Value.ToString() + "','" + grow.Cells("Range").Value.ToString() + "','" + grow.Cells("Collectorate").Value.ToString() + "','" + grow.Cells("PAN").Value.ToString() + "','" + interbranch + "','" + strTagAsFranchise + "','ALL'," + StateCodeCommon + ",'" + countrycode + "','" + isparent + "','" + parent_vendor_code + "','" + cat_struct_code + "','" + strBrachName + "','" + strAccNo + "','" + strBName + "','" + strIFSCCode + "','" + strAccType + "','" + strTypeOfVen + "','" + strPin_Code + "','" + IsTDSApp + "'," + StateCodeCommon + ",'" + strTDSState + "','" + strTDSVenType + "'," + strDedCode + "," + strTDSBranch + ", " + StateCodeCommon + ",'" + strTDSStateService + "','" + strTDSVenTypeService + "'," + strDedCodeService + "," + strTDSBranchService + ",'" + strBranchCode + "','" + strAliesName + "','" + strVenType + "','" + CSAType + "','" + ChillingVen + "'," & isVendorInvoiceNo & ",'" + CHA_DOC_NO + "','" + Cheque_In_favour_of + "','" & CURRENCY_CODE & "'," + IIf(clsCommon.myLen(PC_CODE) <= 0, "null", "'" & PC_CODE & "'") + ",'" & stroldname & "','" & strSSIno & "','" & clsCommon.myCstr(IsBlacklisted) & "', '" & clsCommon.myCstr(IsEmployee) & "')"
                        connectSql.RunSqlTransaction(trans, strcmd)
                        Dim DBEntry As Double
                        DBEntry = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select COUNT(*) As Row From TSPL_TDS_VENDOR_DETAILS Where Vendor_Code ='" & clsCommon.myCstr(fndvendorNo.Value) & "'", trans))
                        If clsCommon.CompairString(IsTDSApp, "1") = CompairStringResult.Equal Then
                            If DBEntry = 0 Then
                                clsDBFuncationality.SaveAStorePorcedure(trans, "SP_TDS_VENDOR_DETAILS_INSERT", New SqlParameter("@Vendor_Code", strvendorNo), New SqlParameter("@Nature_Of_Deduction", strDedCode.Replace("'", "")), New SqlParameter("@State_Code", StateCodeCommon.Replace("'", "")), New SqlParameter("@Pan", clsCommon.myCstr(grow.Cells("PAN").Value)), New SqlParameter("@Vendor_Type", strTDSVenType.Replace("'", "")), New SqlParameter("@status", strTDSState), New SqlParameter("@Branch_Code", strTDSBranch.Replace("'", "")), New SqlParameter("@Inactive", "N"), New SqlParameter("@createdby", userCode), New SqlParameter("@createddate", connectSql.serverDate(trans)), New SqlParameter("@modifiedby", userCode), New SqlParameter("@modifieddate", connectSql.serverDate(trans)), New SqlParameter("@compcode", companyCode))
                            Else
                                PartyDetailsQry = "UPDATE TSPL_TDS_VENDOR_DETAILS SET Vendor_Distance='" & Vendor_Distance & "',PC_CODE=(case when " & clsCommon.myLen(PC_CODE) & ">0 then '" & PC_CODE & "' else null end), Nature_Of_Deduction=" & strDedCode & ",State_Code=" & StateCodeCommon & ",Vendor_TYpe='" & strTDSVenType & "',Status='" & strTDSState & "',Branch_Code=" & strTDSBranch & ",Pan='" & clsCommon.myCstr(grow.Cells("PAN").Value) & "' where Vendor_Code='" + strvendorNo + "'"
                                connectSql.RunSqlTransaction(trans, PartyDetailsQry)
                            End If
                        Else
                            PartyDetailsQry = "UPDATE TSPL_TDS_VENDOR_DETAILS SET Vendor_Distance='" & Vendor_Distance & "',PC_CODE=(case when " & clsCommon.myLen(PC_CODE) & ">0 then '" & PC_CODE & "' else null end), Nature_Of_Deduction=NULL,State_Code=NULL,Vendor_TYpe='Individual',Status='Resident',Branch_Code=NULL,Pan='" & clsCommon.myCstr(grow.Cells("PAN").Value) & "' where Vendor_Code='" + fndvendorNo.Value + "'"
                        End If

                        ''----Qry Run Insert
                        If AllowGSTApplicable = True Then
                            GSTQry = "Update TSPL_VENDOR_MASTER set DFOption='" & DFoption & "',BusinessCondition='" & BusinessCondition & "',GSTRegistered='" & Registered & "',GST_Composition_scheme=" & GST_Composition_scheme & ",GSTEntity='" & GSTEntity & "',GSTLastEntity='" & GSTLastEntity & "',GSTFinalNo='" & GSTFinal & "',GSTMiddle='" & GSTMiddleEntity & "' where Vendor_Code='" & strvendorNo & "'"
                            connectSql.RunSqlTransaction(trans, GSTQry)
                        End If
                        ''--End

                        strqry = " update TSPL_VENDOR_MASTER set Weight='" & Weight & "' where Vendor_Code='" + strvendorNo + "'"
                        clsDBFuncationality.ExecuteNonQuery(strqry, trans)

                        If clsCommon.CompairString(Is_TC_Certi, "1") = CompairStringResult.Equal AndAlso clsCommon.CompairString(IndustryType, "A") = CompairStringResult.Equal Then
                            TCCertiQry = "UPDATE TSPL_VENDOR_MASTER SET TC_Certified='" & TC_Certi & "',Is_TC_Certified='" & Is_TC_Certi & "' where Vendor_Code='" + strvendorNo + "'"
                        Else
                            TCCertiQry = "UPDATE TSPL_VENDOR_MASTER SET TC_Certified=NULL,Is_TC_Certified='0' where Vendor_Code='" + strvendorNo + "'"
                        End If
                        connectSql.RunSqlTransaction(trans, TCCertiQry)

                        If IsEmployee = 1 AndAlso clsCommon.myLen(EmployeeCode) > 0 Then
                            Dim qryforEmp As String = ""
                            qryforEmp = " Update TSPL_VENDOR_MASTER set EMP_CODE ='" + clsCommon.myCstr(EmployeeCode) + "' where Vendor_Code='" + strvendorNo + "'"
                            clsDBFuncationality.ExecuteNonQuery(qryforEmp, trans)
                        End If

                    Else

                        clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strvendorNo, "TSPL_VENDOR_MASTER", "Vendor_Code", trans)

                        ''Map Employee
                        If IsEmployee = 1 AndAlso clsCommon.myLen(EmployeeCode) > 0 Then
                            Dim StrEmpExist As Integer = 0
                            StrEmpExist = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*) as c from tspl_vendor_master where Vendor_Code<>'" + strvendorNo + "' and isnull(EMP_CODE,'')='" + clsCommon.myCstr(EmployeeCode) + "'", trans))
                            If StrEmpExist > 0 Then
                                Throw New Exception("Employee already map with another vendor.")
                            End If
                            Dim qryforEmp As String = ""
                            qryforEmp = " Update TSPL_VENDOR_MASTER set EMP_CODE ='" + clsCommon.myCstr(EmployeeCode) + "' where Vendor_Code='" + strvendorNo + "'"
                            clsDBFuncationality.ExecuteNonQuery(qryforEmp, trans)
                        Else
                            Dim qryforEmp As String = ""
                            qryforEmp = " Update TSPL_VENDOR_MASTER set EMP_CODE = NULL where Vendor_Code='" + strvendorNo + "'"
                            clsDBFuncationality.ExecuteNonQuery(qryforEmp, trans)
                        End If


                        Dim strcmd As String = "Update  TSPL_VENDOR_MASTER set Vendor_Distance='" & Vendor_Distance & "',PC_CODE=(case when " & clsCommon.myLen(PC_CODE) & ">0 then '" & PC_CODE & "' else null end), CURRENCY_CODE='" & CURRENCY_CODE & "', Cheque_In_Favour_Of='" + Cheque_In_favour_of + "' ,CHA_DOC_NO='" + CHA_DOC_NO + "',Vendor_Name='" + strvendorname + "',add1='" + add1 + "',add2='" + add2 + "',add3='" + add3 + "',Closing_Date='" + closing_date + "',Vendor_Group_Code='" + strgroupCode + "',Vendor_Group_Code_Desc='" + strgroupDes + "',City_Code='" + citycode + "',City_Code_Desc='" + citycodedesc + "',State='" + state + "',Country='" + country + "',Phone1='" + phonenum1 + "',Phone2='" + phonenum2 + "',Fax='" + fax + "',Email='" + emailid + "',WebSite='" + website + "',Contact_Person_Name='" + contct_person_name + "',Contact_Person_Phone='" + contct_perfson_phone + "',Contact_Person_Fax='" + contct_person_fax + "',Contact_Person_Website='" + contct_person_website + "',Contact_Person_Email='" + contct_person_email + "',Terms_Code='" + strtermcode + "',Terms_Code_Desc='" + strtermdes + "' ,Vendor_Account='" + vendoracct + "',Vendor_Account_Desc='" + vendoracctdesc + "',Payment_Code='" + paymenttype + "',Payment_Code_Desc='" + paymenttypedesc + "',Bank_Code='" + strbank + "', Bank_Code_Desc='" + strbankdes + "',Ven_Type_Code='" + strvendortype + "',Ven_Type_Desc='" + strvendortypedes + "' ,Tax_Group='" + strTax + "',Tax_Group_Desc='" + strtaxdes + "' ,TAX1='" + grow.Cells("Tax1").Value.ToString() + "',TAX1_Rate='" + clsCommon.myCstr(clsCommon.myCdbl(grow.Cells("Tax1 Rate").Value)) + "',TAX2='" + grow.Cells("Tax2").Value.ToString() + "',TAX2_Rate='" + clsCommon.myCstr(clsCommon.myCdbl(grow.Cells("Tax2 Rate").Value)) + "',TAX3='" + grow.Cells("Tax3").Value.ToString() + "',TAX3_Rate='" + clsCommon.myCstr(clsCommon.myCdbl(grow.Cells("Tax3 Rate").Value)) + "',TAX4='" + grow.Cells("Tax4").Value.ToString() + "',TAX4_Rate='" + clsCommon.myCstr(clsCommon.myCdbl(grow.Cells("Tax4 Rate").Value)) + "',TAX5='" + grow.Cells("Tax5").Value.ToString() + "',TAX5_Rate='" + clsCommon.myCstr(clsCommon.myCdbl(grow.Cells("Tax5 Rate").Value)) + "',TAX6='" + grow.Cells("Tax6").Value.ToString() + "',TAX6_Rate='" + clsCommon.myCstr(clsCommon.myCdbl(grow.Cells("Tax6 Rate").Value)) + "',TAX7='" + grow.Cells("Tax7").Value.ToString() + "',TAX7_Rate='" + clsCommon.myCstr(clsCommon.myCdbl(grow.Cells("Tax7 Rate").Value)) + "',TAX8='" + grow.Cells("Tax8").Value.ToString() + "',TAX8_Rate='" + clsCommon.myCstr(clsCommon.myCdbl(grow.Cells("Tax8 Rate").Value)) + "',TAX9='" + grow.Cells("Tax9").Value.ToString() + "',TAX9_Rate='" + clsCommon.myCstr(clsCommon.myCdbl(grow.Cells("Tax9 Rate").Value)) + "',TAX10='" + grow.Cells("Tax10").Value.ToString() + "',TAX10_Rate='" + clsCommon.myCstr(clsCommon.myCdbl(grow.Cells("Tax10 Rate").Value)) + "',Service_Tax_No='" + grow.Cells("Service Tax No").Value.ToString() + "',Tin_No='" + grow.Cells("Tin No").Value.ToString() + "',Lst_No='" + grow.Cells("List No").Value.ToString() + "',Status='" + grow.Cells("Status").Value.ToString() + "',OnHold='" + grow.Cells("On Hold").Value.ToString() + "',Transporter='" + grow.Cells("Transporter").Value.ToString() + "',Remarks1='" + grow.Cells("Remarks").Value.ToString() + "',Remarks2='" + grow.Cells("Remarks2").Value.ToString() + "',Additional1='" + grow.Cells("Additional1").Value.ToString() + "',Additional2='" + grow.Cells("Additional2").Value.ToString() + "',Additional3='" + grow.Cells("Additional3").Value.ToString() + "',modify_by='" + userCode + "',modify_date='" + connectSql.serverDate(trans) + "',comp_code='" + companyCode + "',CST='" + grow.Cells("CST").Value.ToString() + "',ECC='" + grow.Cells("Ecc").Value.ToString() + "',Range='" + grow.Cells("Range").Value.ToString() + "',Collectorate='" + grow.Cells("Collectorate").Value.ToString() + "',PAN='" + grow.Cells("PAN").Value.ToString() + "',Inter_Branch='" + interbranch + "', franchise_yn='" + strTagAsFranchise + "',form_type='ALL',state_code=" + StateCodeCommon + ",country_code='" + countrycode + "',Is_Parent_Vendor='" + isparent + "',Parent_Vendor_Code='" + parent_vendor_code + "',Category_Struct_Code='" + cat_struct_code + "',Branch_Name='" + strBrachName + "',Account_No='" + strAccNo + "',Bank_Name='" + strBName + "',IFSC_Code='" + strIFSCCode + "',Account_Type='" + strAccType + "',Vendor_Type='" + strTypeOfVen + "',Pin_Code='" + strPin_Code + "',Is_TDS_Applicable='" + IsTDSApp + "' ,TDS_State_Code =" + StateCodeCommon + ",TDS_Status='" + strTDSState + "' ,TDS_Vendor_Type ='" + strTDSVenType + "',Deduction_Code= " + strDedCode + ",TDS_Branch_Code=" + strTDSBranch + ",TDS_State_Code_Service =" + StateCodeCommon + ",TDS_Status_Service='" + strTDSStateService + "' ,TDS_Vendor_Type_Service ='" + strTDSVenTypeService + "',Deduction_Code_Service= " + strDedCodeService + ",TDS_Branch_Code_Service=" + strTDSBranchService + ",Branch_Code='" + strBranchCode + "',Alies_Name='" + strAliesName + "',Vendor_Type_CHA='" + strVenType + "',csa_type='" + CSAType + "',Is_Chilling_Vendor='" + ChillingVen + "',isVendorInvoiceNo=" & isVendorInvoiceNo & ",OldName='" & stroldname & "',SSI_No='" & strSSIno & "',Is_Blacklist='" & clsCommon.myCstr(IsBlacklisted) & "',IsEmployee='" & clsCommon.myCstr(IsEmployee) & "'  where vendor_code='" + strvendorNo + "' " ' and form_type='ALL'"
                        If clsCommon.CompairString(IsTDSApp, "1") = CompairStringResult.Equal Then
                            PartyDetailsQry = "UPDATE TSPL_TDS_VENDOR_DETAILS SET Nature_Of_Deduction=" & strDedCode & ",State_Code=" & StateCodeCommon & ",Vendor_TYpe='" & strTDSVenType & "',Status='" & strTDSState & "',Branch_Code=" & strTDSBranch & ",Pan='" & clsCommon.myCstr(grow.Cells("PAN").Value) & "' where Vendor_Code='" + strvendorNo + "'"
                        Else
                            PartyDetailsQry = "UPDATE TSPL_TDS_VENDOR_DETAILS SET Nature_Of_Deduction=NULL,State_Code=NULL,Vendor_TYpe='Individual',Status='Resident',Branch_Code=NULL,Pan='" & clsCommon.myCstr(grow.Cells("PAN").Value) & "' where Vendor_Code='" + fndvendorNo.Value + "'"
                        End If

                        connectSql.RunSqlTransaction(trans, strcmd)
                        connectSql.RunSqlTransaction(trans, PartyDetailsQry)

                        ''----Qry Run Insert
                        If AllowGSTApplicable = True Then
                            GSTQry = "Update TSPL_VENDOR_MASTER set DFOption='" & DFoption & "',BusinessCondition='" & BusinessCondition & "',GSTRegistered='" & Registered & "',GST_Composition_scheme=" & GST_Composition_scheme & ",GSTEntity='" & GSTEntity & "',GSTLastEntity='" & GSTLastEntity & "',GSTFinalNo='" & GSTFinal & "',GSTMiddle='" & GSTMiddleEntity & "' where Vendor_Code='" & strvendorNo & "'"
                            connectSql.RunSqlTransaction(trans, GSTQry)
                        End If
                        ''--End

                        strqry = " update TSPL_VENDOR_MASTER set Weight='" & Weight & "' where Vendor_Code='" + strvendorNo + "'"
                        clsDBFuncationality.ExecuteNonQuery(strqry, trans)

                        If clsCommon.CompairString(Is_TC_Certi, "1") = CompairStringResult.Equal AndAlso clsCommon.CompairString(IndustryType, "A") = CompairStringResult.Equal Then
                            TCCertiQry = "UPDATE TSPL_VENDOR_MASTER SET TC_Certified='" & TC_Certi & "',Is_TC_Certified='" & Is_TC_Certi & "' where Vendor_Code='" + strvendorNo + "'"
                        Else
                            TCCertiQry = "UPDATE TSPL_VENDOR_MASTER SET TC_Certified=NULL,Is_TC_Certified='0' where Vendor_Code='" + strvendorNo + "'"
                        End If
                        connectSql.RunSqlTransaction(trans, TCCertiQry)
                    End If
                    If clsCommon.CompairString(clsCommon.myLen(clsCommon.myCstr(grow.Cells("Transporter").Value.ToString())), "1") = CompairStringResult.Equal Then
                        Str_Vendor = IIf(Str_Vendor = "", strvendorNo, Str_Vendor & "," & strvendorNo)
                    End If
                    Dim qryHighClass As String = ""
                    If isHighClass = "1" Then
                        qryHighClass = "UPDATE TSPL_VENDOR_MASTER SET isHighClass=1 where Vendor_Code='" + strvendorNo + "'"
                    Else
                        qryHighClass = "UPDATE TSPL_VENDOR_MASTER SET isHighClass=0 where Vendor_Code='" + strvendorNo + "'"
                    End If
                    connectSql.RunSqlTransaction(trans, qryHighClass)
                    Dim qryBulkRouteCode As String = ""
                    If clsCommon.myLen(strBulkRouteNo) > 0 Then
                        qryBulkRouteCode = "UPDATE TSPL_VENDOR_MASTER SET Bulk_ROUTE_NO='" + strBulkRouteNo + "' where Vendor_Code='" + strvendorNo + "'"
                        connectSql.RunSqlTransaction(trans, qryBulkRouteCode)
                    Else
                        qryBulkRouteCode = "UPDATE TSPL_VENDOR_MASTER SET Bulk_ROUTE_NO=NULL where Vendor_Code='" + strvendorNo + "'"
                        connectSql.RunSqlTransaction(trans, qryBulkRouteCode)
                    End If

                    ' buyerfilereturninlasttwoyears , TCSTDSamountisgreaterthan50KinpreviousYear
                    connectSql.RunSqlTransaction(trans, "UPDATE TSPL_VENDOR_MASTER SET Isbuyerfilereturninlasttwoyears=" + buyerfilereturninlasttwoyears + " , IsTCS_TDSamountgreaterthan50KpreviousYear = " + TCSTDSamountisgreaterthan50KinpreviousYear + " where Vendor_Code='" + strvendorNo + "'")
                    connectSql.RunSqlTransaction(trans, "UPDATE TSPL_VENDOR_MASTER SET IsAllowSkipPurchaseQC = " + IsAllowSkipPurchaseQC + " where Vendor_Code='" + strvendorNo + "'")

                Next
                trans.Commit()
                If clsCommon.myLen(Str_Vendor) > 0 Then
                    Save_Transport_Data(Nothing, Str_Vendor)
                End If
                clsCommon.ProgressBarPercentHide()
                common.clsCommon.MyMessageBoxShow(Me, "Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                trans.Rollback()
                clsCommon.ProgressBarPercentHide()
                clsCommon.MyMessageBoxShow("Line: " + Count + " - " + ex.Message)
            End Try

        End If
        Me.Controls.Remove(gv)
    End Sub

    '' Anubhooti 01-Sep-2014
    Public Sub VendorName()
        Try
            Dim col As New AutoCompleteStringCollection
            Dim strquery As String = "Select Vendor_Name  From TSPL_VENDOR_MASTER "
            Dim ds As DataTable
            ' Dim strvalue As String
            ds = clsDBFuncationality.GetDataTable(strquery)
            Dim comp As Integer
            For comp = 0 To ds.Rows.Count - 1
                col.Add(ds.Rows(comp).Item(0))

            Next
            txtvendorname.AutoCompleteSource = AutoCompleteSource.CustomSource
            txtvendorname.AutoCompleteCustomSource = col
            txtvendorname.AutoCompleteMode = AutoCompleteMode.Suggest

        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub
#End Region

#Region "Event"
    '' Anubhooti 01-Sep-2014 Parameter VCode
    Sub fndvendorNo_text_changed(Optional ByVal VCode As String = "")
        Try
            Dim str As String = ""
            If VCode = "" Then
                str = "select vendor_code from TSPL_VENDOR_MASTER where vendor_code = '" + fndvendorNo.Value + "' " 'and form_type='" + txtvndrtype.Text + "'"
            Else
                str = "select vendor_code from TSPL_VENDOR_MASTER where vendor_code = '" + VCode + "' " 'and form_type='" + txtvndrtype.Text + "'"
            End If


            Dim dr As DataTable
            Dim strvalue As String = ""
            dr = clsDBFuncationality.GetDataTable(str)
            If dr.Rows IsNot Nothing AndAlso dr.Rows.Count > 0 Then
                strvalue = dr.Rows(0)("vendor_code").ToString()
            End If
            If strvalue <> "" Then
                funfill(strvalue)
                If isLoadCopy = True Then
                    btnsave.Text = "Save"
                    btnsave.Enabled = True
                    btndelete.Enabled = False
                Else
                    btnsave.Text = "Update"
                    btnsave.Enabled = True
                    btndelete.Enabled = True
                End If
                'If userCode <> "ADMIN" Then
                '    If funSetUserAccess() = False Then Exit Sub
                'End If

            Else
                'chkcsa.Checked = False
                txtcountrycode.Value = ""
                txttdsstate.Value = ""
                Me.txtvendorname.Text = ""
                Me.fndgroupcode.Value = ""
                Me.txtgroupdes.Text = ""
                chkHold.Checked = False
                chkInActive.Checked = False
                chktrarns.Checked = False
                chkTagAsFranchise.Checked = False
                Me.dtClosing.Value = clsCommon.GETSERVERDATE()
                Me.txtAdd1.Text = ""
                Me.txtAdd2.Text = ""
                Me.txtAdd3.Text = ""
                Me.fndCity.Value = ""
                Me.txtCity.Text = ""
                Me.lblStateName.Text = ""
                Me.txtCountry.Text = ""
                Me.txtPhone1.Text = ""
                Me.txtPhone2.Text = ""
                Me.txtfax.Text = ""
                Me.txtEmail.Text = ""
                Me.txtWeb.Text = ""
                Me.txtContactName.Text = ""
                Me.txtContPhone.Text = ""
                Me.txtContactFax.Text = ""
                Me.txtContactWeb.Text = ""
                Me.txtContactEmail.Text = ""
                Me.fndTrmsCode.Value = ""
                Me.txttermcodedes.Text = ""
                Me.fndAccntSet.Value = ""
                Me.txtaccsetdes.Text = ""
                Me.fndPayCode.Value = ""
                Me.txtpaymentcodedes.Text = ""
                Me.fndvendortype.Value = ""
                Me.txtvendortypedes.Text = ""
                Me.fndbankcode.Value = ""
                ' Me.txtbankcodedes.Text = ""
                TxtBankName.Text = ""
                Me.txtStaxNo.Text = ""
                Me.txtLstNo.Text = ""
                Me.txtTinNo.Text = ""
                Me.txtCredit.Text = "0.00"
                Me.fndTxGrp.Value = ""
                Me.txtTxGrp.Text = ""
                Me.txtRemarks1.Text = ""
                Me.txtRemarks2.Text = ""
                Me.txtAddInfo1.Text = ""
                Me.txtAddInfo2.Text = ""
                Me.txtAddInfo3.Text = ""
                Me.txtcst.Text = ""
                Me.txtecc.Text = ""
                Me.txtrange.Text = ""
                Me.txtcollect.Text = ""
                Me.txtpan.Text = ""
                cmbAccountType.SelectedValue = "Cur"
                TxtBankBranch.Text = ""
                TxtBankName.Text = ""
                TxtIFSCCode.Text = ""
                TxtAccNo.Text = ""
                txtAliesName.Text = ""
                TxtOldname.Text = ""
                cmbTypeOfVen.SelectedValue = "N"
                chkVendorInvoiceNo.Checked = False
                chkOEM.Checked = False
                If is_For_Chilling_Vendor = False Then
                    CmbVenType.SelectedValue = ""
                End If
                Me.grdTax.DataSource = Nothing
                Me.grdTax.Rows.Clear()
                btnsave.Text = "Save"
                btndelete.Enabled = False
                fndBulkRouteCode.Value = ""
                lblBulkRouteNo.Text = ""
                chkHighClass.Checked = False
                EmployeeFind.Value = ""
                fndVendorReg.Value = ""
            End If
        Catch ex As Exception

            myMessages.myExceptions(ex)
        End Try
    End Sub
    Public Sub fndvendorNo_text_changed(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub
    'This will check the existence of value in databse on text change property of finder,if it exist then it will call funfill() otherwise it will blank the fields
    Sub fndgroupcode_text_Changed()
        Try
            Dim strquery As String = "select ven_Group_code,group_desc  from Tspl_vendor_group where ven_group_code='" + fndgroupcode.Value + "'"
            Dim dr As DataTable
            Dim strvalue As String = ""
            dr = clsDBFuncationality.GetDataTable(strquery)
            If dr.Rows IsNot Nothing AndAlso dr.Rows.Count > 0 Then

                strvalue = dr.Rows(0)("ven_Group_code").ToString()
            End If
            If strvalue <> "" Then
                funfillfndGroupCode()
            Else
                txtgroupdes.Text = ""
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub
    Public Sub fndgroupcode_text_Changed(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub


    'This will check the existence of value in databse on text change property of finder,if it exist then it will call funfill() otherwise it will blank the fields
    Sub fndcity_text_Changed()
        Try
            Dim str As String = "select City_Code,city_Name from TSPL_City_MASTER where City_code = '" + fndCity.Value + "'"
            Dim dr As DataTable
            Dim strvalue As String = ""
            dr = clsDBFuncationality.GetDataTable(str)
            If dr.Rows IsNot Nothing AndAlso dr.Rows.Count > 0 Then

                strvalue = dr.Rows(0)("City_Code").ToString()
            End If
            If strvalue <> "" Then
                funfilfndcity()
            Else
                txtCity.Text = ""

            End If
        Catch ex As Exception

            myMessages.myExceptions(ex)
        End Try
    End Sub
    Public Sub fndcity_text_Changed(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub
    'This will check the existence of value in databse on text change property of finder,if it exist then it will call funfill() otherwise it will blank the fields
    Public Sub fndTrmsCode_text_Changed(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim str As String = "SELECT [Terms_Code] as [Terms Code],[Terms_Desc] as [Description] FROM [TSPL_TERMS_MASTER] where terms_code='" + fndTrmsCode.Value + "'"
            Dim dr As DataTable
            Dim strvalue As String = ""
            dr = clsDBFuncationality.GetDataTable(str)
            If dr.Rows IsNot Nothing AndAlso dr.Rows.Count > 0 Then
                strvalue = dr.Rows(0)("Terms Code").ToString()
            End If
            If strvalue <> "" Then
                funfilfndterm()
            Else
                txttermcodedes.Text = ""

            End If
        Catch ex As Exception

            myMessages.myExceptions(ex)
        End Try
    End Sub
    'This will check the existence of value in databse on text change property of finder,if it exist then it will call funfill() otherwise it will blank the fields
    Public Sub fndAccntSet_text_Changed(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim str As String = "SELECT  [Acct_Set_Code] as [Account Set Code],[Acct_Set_Desc] as [Description] FROM [TSPL_VENDOR_ACCOUNT_SET] where Acct_Set_Code ='" + fndAccntSet.Value + "'"
            Dim dr As DataTable
            Dim strvalue As String = ""
            dr = clsDBFuncationality.GetDataTable(str)
            If dr.Rows IsNot Nothing AndAlso dr.Rows.Count > 0 Then
                strvalue = dr.Rows(0)("Account Set Code").ToString()
            End If
            If strvalue <> "" Then
                funfillfndACCSet()
            Else
                txtaccsetdes.Text = ""

            End If
        Catch ex As Exception

            myMessages.myExceptions(ex)
        End Try
    End Sub
    'This will check the existence of value in databse on text change property of finder,if it exist then it will call funfill() otherwise it will blank the fields
    Public Sub fndPayCode_text_Changed(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim str As String = "  SELECT [Payment_Code] as [Payment Code],[payment_Desc] as [Description] FROM [TSPL_PAYMENT_CODE] where payment_code='" + fndPayCode.Value + "'"
            Dim dr As DataTable
            Dim strvalue As String = ""
            dr = clsDBFuncationality.GetDataTable(str)
            If dr.Rows IsNot Nothing AndAlso dr.Rows.Count > 0 Then
                strvalue = dr.Rows(0)("Payment Code").ToString()
            End If
            If strvalue <> "" Then
                funfillfndPay()
            Else
                txtpaymentcodedes.Text = ""

            End If
        Catch ex As Exception

            myMessages.myExceptions(ex)
        End Try
    End Sub
    'This will check the existence of value in databse on text change property of finder,if it exist then it will call funfill() otherwise it will blank the fields
    Public Sub fndvendortype_text_Changed(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim str As String = "SELECT  [ven_Type_Code] as [Vendor Type Code],[ven_Type_Desc] as [Description]FROM [TSPL_VENDOR_TYPE_MASTER] where ven_type_code='" + fndvendortype.Value + "'"
            Dim dr As DataTable
            Dim strvalue As String = ""
            dr = clsDBFuncationality.GetDataTable(str)
            If dr.Rows IsNot Nothing AndAlso dr.Rows.Count > 0 Then
                strvalue = dr.Rows(0)("Vendor Type Code").ToString()
            End If
            If strvalue <> "" Then
                funfillfndventype()
            Else
                txtvendortypedes.Text = ""
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub
    'This will check the existence of value in databse on text change property of finder,if it exist then it will call funfill() otherwise it will blank the fields
    Public Sub fndbankcode_text_Changed(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim strquery As String = "select bank_code As [Bank Code],description  as [Description]from TSPL_Bank_MASTER where bank_code='" + fndbankcode.Value + "'"
            Dim dr As DataTable
            Dim strvalue As String = ""
            dr = clsDBFuncationality.GetDataTable(strquery)
            If dr.Rows IsNot Nothing AndAlso dr.Rows.Count > 0 Then
                strvalue = dr.Rows(0)("Bank Code").ToString()
            End If
            If strvalue <> "" Then
                funfillbank()
            Else
                'txtbankcodedes.Text = ""
                TxtBankName.Text = ""
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub
    'This will check the existence of value in databse on text change property of finder,if it exist then it will call funfill() otherwise it will blank the fields
    Public Sub fndTxGrp_text_Changed(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try

            Dim strquery As String = " SELECT TSPL_TAX_GROUP_DETAILS.Tax_Code as [Tax] FROM TSPL_TAX_GROUP_MASTER INNER JOIN TSPL_TAX_GROUP_DETAILS ON TSPL_TAX_GROUP_MASTER.Tax_Group_Code = TSPL_TAX_GROUP_DETAILS.Tax_Group_Code" &
                      " where TSPL_TAX_GROUP_MASTER.Tax_Group_Code='" + fndTxGrp.Value + "' and TSPL_TAX_GROUP_MASTER.Tax_Group_Type='P'"
            Dim dr As DataTable
            Dim strvalue As String = ""
            dr = clsDBFuncationality.GetDataTable(strquery)
            If dr.Rows IsNot Nothing AndAlso dr.Rows.Count > 0 Then
                strvalue = dr.Rows(0)("Tax").ToString()
            End If
            If strvalue <> "" Then
                funfillfndtaxgrp()
            Else
                txtTxGrp.Text = ""
                Me.grdTax.DataSource = Nothing
                Me.grdTax.Rows.Clear()

            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub
    'This will check the existence of value in databse on leave property of finder,if it exist then it will fill, otherwise it will blank the fields
    Sub fndgroupcode_leave()
        If fndgroupcode.Value = "" Then
        Else
            Try
                Dim strquery As String = "select ven_Group_code,group_desc  from Tspl_vendor_group where ven_group_code='" + fndgroupcode.Value + "'"
                Dim dr As DataTable
                Dim strvalue As String = ""
                dr = clsDBFuncationality.GetDataTable(strquery)
                If dr.Rows IsNot Nothing AndAlso dr.Rows.Count > 0 Then
                    strvalue = dr.Rows(0)("ven_Group_code").ToString()
                End If
                If strvalue <> "" Then
                Else : strquery = ""
                    txtgroupdes.Text = ""
                    common.clsCommon.MyMessageBoxShow(Me, "This Group Code does not exist in Master Table", Me.Text)
                    fndgroupcode.Value = ""
                End If
            Catch ex As Exception
                myMessages.myExceptions(ex)
            End Try
        End If
    End Sub
    Public Sub fndgroupcode_leave(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub
    'This will check the existence of value in databse on leave property of finder,if it exist then it will fill, otherwise it will blank the fields
    Sub fndCity_leave()
        If fndCity.Value = "" Then
        Else
            Try
                Dim strquery As String = "select City_Code ,city_Name from TSPL_City_MASTER where City_code = '" + fndCity.Value + "'"
                Dim dr As DataTable
                Dim strvalue As String = ""
                dr = clsDBFuncationality.GetDataTable(strquery)
                If dr.Rows IsNot Nothing AndAlso dr.Rows.Count > 0 Then
                    strvalue = dr.Rows(0)("City_Code").ToString()
                End If
                If strvalue <> "" Then
                Else : strquery = ""
                    txtCity.Text = ""
                    common.clsCommon.MyMessageBoxShow(Me, "This City does not exist in Master Table", Me.Text)
                    fndCity.Value = ""
                End If
            Catch ex As Exception
                myMessages.myExceptions(ex)
            End Try
        End If
    End Sub
    Public Sub fndCity_leave(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub
    'This will check the existence of value in databse on leave property of finder,if it exist then it will fill, otherwise it will blank the fields
    Public Sub fndTrmsCode_leave(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If fndTrmsCode.Value = "" Then
        Else
            Try
                Dim strquery As String = "SELECT [Terms_Code] as [Terms Code],[Terms_Desc] as [Description] FROM [TSPL_TERMS_MASTER] where terms_code='" + fndTrmsCode.Value + "'"
                Dim dr As DataTable
                Dim strvalue As String = ""
                dr = clsDBFuncationality.GetDataTable(strquery)
                If dr.Rows IsNot Nothing AndAlso dr.Rows.Count > 0 Then
                    strvalue = dr.Rows(0)("Terms Code").ToString()
                End If
                If strvalue <> "" Then
                Else : strquery = ""
                    txttermcodedes.Text = ""
                    common.clsCommon.MyMessageBoxShow(Me, "This Term Code does not exist in Master Table", Me.Text)
                    fndTrmsCode.Value = ""
                End If
            Catch ex As Exception
                myMessages.myExceptions(ex)
            End Try
        End If
    End Sub
    'This will check the existence of value in databse on leave property of finder,if it exist then it will fill, otherwise it will blank the fields
    Public Sub fndAccntSet_leave(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If fndAccntSet.Value = "" Then
        Else
            Try
                Dim strquery As String = "SELECT [Acct_Set_Code] as [Account Set Code],[Acct_Set_Desc] as [Description] FROM [TSPL_VENDOR_ACCOUNT_SET]where Acct_Set_Code ='" + fndAccntSet.Value + "'"
                Dim dr As DataTable
                Dim strvalue As String = ""
                dr = clsDBFuncationality.GetDataTable(strquery)
                If dr.Rows IsNot Nothing AndAlso dr.Rows.Count > 0 Then
                    strvalue = dr.Rows(0)("Account Set Code").ToString()
                End If
                If strvalue <> "" Then
                Else : strquery = ""
                    txtaccsetdes.Text = ""
                    common.clsCommon.MyMessageBoxShow(Me, "This Account does not exist in Master Table", Me.Text)
                    fndAccntSet.Value = ""
                End If
            Catch ex As Exception
                myMessages.myExceptions(ex)
            End Try
        End If
    End Sub
    'This will check the existence of value in databse on leave property of finder,if it exist then it will fill, otherwise it will blank the fields
    Public Sub fndPayCode_leave(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If fndPayCode.Value = "" Then
        Else
            Try
                Dim strquery As String = "  SELECT [Payment_Code] as [Payment Code],[payment_Desc] as [Description] FROM [TSPL_PAYMENT_CODE] where payment_code='" + fndPayCode.Value + "'"
                Dim dr As DataTable
                Dim strvalue As String = ""
                dr = clsDBFuncationality.GetDataTable(strquery)
                If dr.Rows IsNot Nothing AndAlso dr.Rows.Count > 0 Then
                    strvalue = dr.Rows(0)("Payment Code").ToString()
                End If
                If strvalue <> "" Then
                Else : strquery = ""
                    txtpaymentcodedes.Text = ""
                    common.clsCommon.MyMessageBoxShow(Me, "This Payment Code does not exist in Master Table", Me.Text)
                    fndPayCode.Value = ""
                End If
            Catch ex As Exception
                myMessages.myExceptions(ex)
            End Try
        End If
    End Sub
    'This will check the existence of value in databse on leave property of finder,if it exist then it will fill, otherwise it will blank the fields
    Public Sub fndvendortype_leave(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If fndvendortype.Value = "" Then
        Else
            Try
                Dim strquery As String = "SELECT  [ven_Type_Code] as [Vendor Type Code],[ven_Type_Desc] as [Description]FROM [TSPL_VENDOR_TYPE_MASTER] where ven_type_code='" + fndvendortype.Value + "'"
                Dim dr As DataTable
                Dim strvalue As String = ""
                dr = clsDBFuncationality.GetDataTable(strquery)
                If dr.Rows IsNot Nothing AndAlso dr.Rows.Count > 0 Then
                    strvalue = dr.Rows(0)("Vendor Type Code").ToString()
                End If
                If strvalue <> "" Then
                Else : strquery = ""
                    txtvendortypedes.Text = ""
                    common.clsCommon.MyMessageBoxShow(Me, "This Vendor Type does not exist in Master Table", Me.Text)
                    fndvendortype.Value = ""
                End If
            Catch ex As Exception
                myMessages.myExceptions(ex)
            End Try
        End If
    End Sub
    'This will check the existence of value in databse on leave property of finder,if it exist then it will fill, otherwise it will blank the fields
    Public Sub fndbankcode_leave(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If fndbankcode.Value = "" Then
        Else
            Try
                Dim strquery As String = "select bank_code As [Bank Code],description  as [Description]from TSPL_Bank_MASTER where bank_code='" + fndbankcode.Value + "'"
                Dim dr As DataTable
                Dim strvalue As String = ""
                dr = clsDBFuncationality.GetDataTable(strquery)
                If dr.Rows IsNot Nothing AndAlso dr.Rows.Count > 0 Then
                    strvalue = dr.Rows(0)("Bank Code").ToString()
                End If
                If strvalue <> "" Then
                Else : strquery = ""
                    TxtBankName.Text = ""
                    common.clsCommon.MyMessageBoxShow(Me, "This Bank Code does not exist in Master Table", Me.Text)
                    fndbankcode.Value = ""
                End If
            Catch ex As Exception
                myMessages.myExceptions(ex)
            End Try
        End If
    End Sub
    'This will check the existence of value in databse on leave property of finder,if it exist then it will fill, otherwise it will blank the fields
    Public Sub fndTxGrp_leave(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If fndTxGrp.Value = "" Then
        Else
            Try
                Dim strquery As String = "SELECT [Tax_Group_Code] AS [Tax Group Code],[Tax_Group_Desc] as [Description] FROM [TSPL_TAX_GROUP_MASTER] where Tax_Group_Code='" + fndTxGrp.Value + "'"
                Dim dr As DataTable
                Dim strvalue As String = ""
                dr = clsDBFuncationality.GetDataTable(strquery)
                If dr.Rows IsNot Nothing AndAlso dr.Rows.Count > 0 Then
                    strvalue = dr.Rows(0)("Tax Group Code").ToString()
                End If
                If strvalue <> "" Then
                Else : strquery = ""
                    txtTxGrp.Text = ""
                    common.clsCommon.MyMessageBoxShow(Me, "This Tax Group does not exist in Master Table", Me.Text)
                    fndTxGrp.Value = ""
                    Me.grdTax.DataSource = Nothing
                    Me.grdTax.Rows.Clear()
                End If
            Catch ex As Exception
                myMessages.myExceptions(ex)
            End Try
        End If
    End Sub

    'Keypress validation on finder and converting lower case to upper case
    Public Sub fndvendorNo_key_press(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)

        'If (e.KeyChar = Chr(39)) Then
        '    e.Handled = True
        'End If
    End Sub

    'Keypress validation on finder and converting lower case to upper case
    Public Sub fndgroupcode_key_press(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)

        If (e.KeyChar = Chr(39)) Then
            e.Handled = True
        End If
    End Sub

    'Keypress validation on finder and converting lower case to upper case
    Public Sub fndCity_key_press(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)

        If (e.KeyChar = Chr(39)) Then
            e.Handled = True
        End If
    End Sub

    'Keypress validation on finder and converting lower case to upper case
    Public Sub fndTermsCode_key_press(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)

        If (e.KeyChar = Chr(39)) Then
            e.Handled = True
        End If
    End Sub

    'Keypress validation on finder and converting lower case to upper case
    Public Sub fndAccntSet_key_press(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)

        If (e.KeyChar = Chr(39)) Then
            e.Handled = True
        End If
    End Sub

    'Keypress validation on finder and converting lower case to upper case
    Public Sub fndPayCode_key_press(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)

        If (e.KeyChar = Chr(39)) Then
            e.Handled = True
        End If
    End Sub

    'Keypress validation on finder and converting lower case to upper case
    Public Sub fndvendortype_key_press(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)

        If (e.KeyChar = Chr(39)) Then
            e.Handled = True
        End If
    End Sub
    'Keypress validation on finder and converting lower case to upper case
    Public Sub fndbankcode_key_press(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)

        If (e.KeyChar = Chr(39)) Then
            e.Handled = True
        End If
    End Sub

    Public Sub fndTxGrp_key_press(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)

        If (e.KeyChar = Chr(39)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub grdTax_EditorRequired(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.EditorRequiredEventArgs) Handles grdTax.EditorRequired
        'Dim str As String = "select Tax_Rate as [Tax Rate] from TSPL_TAX_RATES where Tax_Code = '" + grdTax.CurrentRow.Cells(0).Value + "'"
        Dim str As String = "select Tax_Rate as [Tax Rate] from TSPL_TAX_RATES where Tax_Code = '" + grdTax.CurrentRow.Cells(0).Value + "' and tax_type='P'"
        Dim gvMultiComboColum As GridViewComboBoxColumn = TryCast(grdTax.Columns(1), GridViewComboBoxColumn)
        Dim myDs As DataSet
        myDs = connectSql.RunSQLReturnDS(str)
        gvMultiComboColum.DataSource = myDs.Tables(0)
        gvMultiComboColum.ValueMember = "Tax Rate"

    End Sub
    'Validation in credit limit text box
    Private Sub txtCredit_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCredit.KeyPress
        If ((e.KeyChar >= Chr(48) And e.KeyChar <= Chr(57)) Or e.KeyChar = Chr(8) Or e.KeyChar = Chr(48)) Then
        Else
            e.Handled = True
        End If
    End Sub
    'Email Validation
    Private Sub txtEmail_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtEmail.Leave
        If txtEmail.Text = "" Then
        Else
            Dim check As Match = Regex.Match(txtEmail.Text, "\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*")
            If check.Success Then
            Else
                common.clsCommon.MyMessageBoxShow(Me, "Please Enter the proper format of e-mail address", Me.Text)
                txtEmail.Text = ""
                txtEmail.Focus()
            End If
        End If
    End Sub
    Private Sub txtContactEmail_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtContactEmail.Leave
        If txtContactEmail.Text = "" Then
            Return
        Else
            Dim check As Match = Regex.Match(txtContactEmail.Text, "\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*")
            If check.Success Then
            Else
                common.clsCommon.MyMessageBoxShow(Me, "Please Enter the proper format of e-mail address", Me.Text)
                txtContactEmail.Text = ""
                txtContactEmail.Focus()
            End If
        End If
    End Sub
    'Numerics Validation---------------------------------------------
    Private Sub txtPhone1_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If ((e.KeyChar >= Chr(48) And e.KeyChar <= Chr(57)) Or e.KeyChar = Chr(8) Or e.KeyChar = Chr(48) Or e.KeyChar = Chr(45)) Then
        Else
            e.Handled = True
        End If
    End Sub

    Private Sub txtPhone2_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If ((e.KeyChar >= Chr(48) And e.KeyChar <= Chr(57)) Or e.KeyChar = Chr(8) Or e.KeyChar = Chr(48) Or e.KeyChar = Chr(45)) Then
        Else
            e.Handled = True
        End If
    End Sub

    Private Sub txtfax_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtfax.KeyPress
        If ((e.KeyChar >= Chr(48) And e.KeyChar <= Chr(57)) Or e.KeyChar = Chr(8) Or e.KeyChar = Chr(48) Or e.KeyChar = Chr(45)) Then
        Else
            e.Handled = True
        End If
    End Sub

    Private Sub txtContactFax_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtContactFax.KeyPress
        If ((e.KeyChar >= Chr(48) And e.KeyChar <= Chr(57)) Or e.KeyChar = Chr(8) Or e.KeyChar = Chr(48) Or e.KeyChar = Chr(45)) Then
        Else
            e.Handled = True
        End If
    End Sub

    Private Sub txtContPhone_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If ((e.KeyChar >= Chr(48) And e.KeyChar <= Chr(57)) Or e.KeyChar = Chr(8) Or e.KeyChar = Chr(48) Or e.KeyChar = Chr(45)) Then
        Else
            e.Handled = True
        End If
    End Sub
#End Region

#Region "Finder"

    Private Sub fndvendorNo_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'fndvendorNo.Query = "select Vendor_Code as [Vendor Code],Vendor_Name as [Vendor Name],Vendor_Group_Code as [Vendor Group],(select case when Status='N' then 'Active' else 'In.Active' end ) as [Status] from TSPL_VENDOR_MASTER  "
        'fndvendorNo.ConnectionString = connectSql.SqlCon()
        'fndvendorNo.Caption = "Vendor"
        'fndvendorNo.ValueToSelect = "Vendor Code"
        'fndvendorNo.ValueToSelect1 = "Vendor Name"
    End Sub

    Private Sub fndgroupcode_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)

        'fndgroupcode.Query = " SELECT ven_Group_Code as [Vendor Group Code],Group_Desc as [Description],Tax_Group_Code as [Tax Group],Acct_Set_Code as [Account Set],Terms_Code as [Terms Code] FROM [TSPL_VENDOR_GROUP] "
        'fndgroupcode.ConnectionString = connectSql.SqlCon()
        'fndgroupcode.Caption = "Vendor Group"
        'fndgroupcode.ValueToSelect = "Vendor Group Code"
        'fndgroupcode.ValueToSelect1 = "Description"
    End Sub

    Private Sub fndCity_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'fndCity.Query = "SELECT [City_Code] as [City Code],[City_Name] as [City Name]FROM [TSPL_CITY_MASTER]"
        'fndCity.ConnectionString = connectSql.SqlCon()
        'fndCity.Caption = "City"
        'fndCity.ValueToSelect = "City Code"
        'fndCity.ValueToSelect1 = "City Name"
    End Sub
    'Private Sub fndTrmsCode_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    fndTrmsCode.Query = "SELECT [Terms_Code] as [Terms Code],[Terms_Desc] as [Description],[No_Days] as [No. of Days] FROM [TSPL_TERMS_MASTER]"
    '    fndTrmsCode.ConnectionString = connectSql.SqlCon()
    '    fndTrmsCode.Caption = "Payments Terms"
    '    fndTrmsCode.ValueToSelect = "Terms Code"
    '    fndTrmsCode.ValueToSelect1 = "Description"
    'End Sub

    'Private Sub fndAccntSet_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    fndAccntSet.Query = "SELECT  [Acct_Set_Code] as [Account Set Code],[Acct_Set_Desc] as [Description] FROM [TSPL_VENDOR_ACCOUNT_SET]"
    '    fndAccntSet.ConnectionString = connectSql.SqlCon()
    '    fndAccntSet.Caption = "Vendor Account Set"
    '    fndAccntSet.ValueToSelect = "Account Set Code"
    '    fndAccntSet.ValueToSelect1 = "Description"
    'End Sub

    'Private Sub fndPayCode_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    fndPayCode1.Query = "  SELECT [Payment_Code] as [Payment Code],[payment_Desc] as [Description] FROM [TSPL_PAYMENT_CODE]"
    '    fndPayCode1.ConnectionString = connectSql.SqlCon()
    '    fndPayCode1.Caption = "Payment Code"
    '    fndPayCode1.ValueToSelect = "Payment Code"
    '    fndPayCode1.ValueToSelect1 = "Description"
    'End Sub

    'Private Sub fndvendortype_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    fndvendortype1.Query = "SELECT  [ven_Type_Code] as [Vendor Type Code],[ven_Type_Desc] as [Description]FROM [TSPL_VENDOR_TYPE_MASTER]"
    '    fndvendortype1.ConnectionString = connectSql.SqlCon()
    '    fndvendortype1.Caption = "Vendor Type"
    '    fndvendortype1.ValueToSelect = "Vendor Type Code"
    '    fndvendortype1.ValueToSelect1 = "Description"
    'End Sub

    'Private Sub fndbankcode_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    fndbankcode.ConnectionString = connectSql.SqlCon()
    '    'fndbankcode.Query = " select bank_code As [Bank Code],description  as [Description]from TSPL_Bank_MASTER "
    '    fndbankcode.Query = clsERPFuncationality.glbankquery
    '    fndbankcode.ValueToSelect = "Bank Code"
    '    fndbankcode.Caption = "Bank Master"
    '    fndbankcode.ValueToSelect1 = "Description"
    'End Sub

    'Private Sub fndTxGrp_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    'fndTxGrp.Query = "SELECT [Tax_Group_Code] AS [Tax Group Code],[Tax_Group_Desc] as [Description]," & _
    '    '   " (select case when [Tax_Group_Type]='S' then 'Sale' else 'Purchase' end) as [Type] FROM [TSPL_TAX_GROUP_MASTER] where Tax_Group_Type='S'"
    '    fndTxGrp.Query = clsERPFuncationality.UserAvailableTaxGroup + " and  M.Tax_Group_Type='P'"
    '    fndTxGrp.ConnectionString = connectSql.SqlCon()
    '    fndTxGrp.Caption = "Tax Group"
    '    fndTxGrp.ValueToSelect = "Code"
    '    fndTxGrp.ValueToSelect1 = "Description"
    'End Sub

    Private Sub Validate_ActivationStatus()

        Dim qryVendorStatus As String = Nothing
        Dim dt As DataTable = Nothing
        Try
            vendorDict = New Dictionary(Of String, String)
            If clsCommon.myLen(fndvendorNo.Value) > 0 Then
                qryVendorStatus = " SELECT DISTINCT TSPL_VENDOR_MASTER.Vendor_Code, TSPL_PURCHASE_ORDER_HEAD.Vendor_Code AS 'TSPL_PURCHASE_ORDER_HEAD.Vendor_Code', COALESCE(TSPL_GRN_HEAD.Vendor_Code, '') AS 'TSPL_GRN_HEAD.Vendor_Code', COALESCE(TSPL_SRN_HEAD.Vendor_Code, '') AS 'TSPL_SRN_HEAD.Vendor_Code', COALESCE(TSPL_PI_HEAD.Vendor_Code, '') AS 'TSPL_PI_HEAD.Vendor_Code', COALESCE(TSPL_PR_HEAD.Vendor_Code, '') AS 'TSPL_PR_HEAD.Vendor_Code' FROM TSPL_PURCHASE_ORDER_HEAD LEFT JOIN TSPL_GRN_HEAD ON TSPL_PURCHASE_ORDER_HEAD.Vendor_Code = TSPL_GRN_HEAD.Vendor_Code LEFT JOIN TSPL_SRN_HEAD ON TSPL_GRN_HEAD.Vendor_Code = TSPL_SRN_HEAD.Vendor_Code LEFT JOIN TSPL_PI_HEAD LEFT JOIN TSPL_PR_HEAD ON TSPL_PI_HEAD.Vendor_Code = TSPL_PR_HEAD.Vendor_Code ON TSPL_PI_HEAD.Vendor_Code = TSPL_SRN_HEAD.Vendor_Code LEFT JOIN TSPL_VENDOR_MASTER ON  TSPL_PURCHASE_ORDER_HEAD.Vendor_Code = TSPL_VENDOR_MASTER.Vendor_Code WHERE 1 = 1 AND TSPL_VENDOR_MASTER.status = 'N' and TSPL_VENDOR_MASTER.Vendor_Code in ('" + fndvendorNo.Value + "') ORDER BY TSPL_VENDOR_MASTER.Vendor_Code ; "
                dt = clsDBFuncationality.GetDataTable(qryVendorStatus)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    For Each row As DataRow In dt.Rows
                        vendorDict.Add("TSPL_PURCHASE_ORDER_HEAD.Vendor_Code", row("TSPL_PURCHASE_ORDER_HEAD.Vendor_Code"))
                        vendorDict.Add("TSPL_GRN_HEAD.Vendor_Code", row("TSPL_GRN_HEAD.Vendor_Code"))
                        vendorDict.Add("TSPL_SRN_HEAD.Vendor_Code", row("TSPL_SRN_HEAD.Vendor_Code"))
                        vendorDict.Add("TSPL_PI_HEAD.Vendor_Code", row("TSPL_PI_HEAD.Vendor_Code"))
                        vendorDict.Add("TSPL_PR_HEAD.Vendor_Code", row("TSPL_PR_HEAD.Vendor_Code"))
                    Next
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub
    Private Function findVendorCount() As Integer
        Validate_ActivationStatus()
        Dim countOfVendorFound As Integer = 0
        Dim value As String = Nothing
        Try
            For Each keyName As String In vendorDict.Keys
                If (vendorDict.TryGetValue(keyName, value)) Then
                    If value = fndvendorNo.Value Then
                        countOfVendorFound = countOfVendorFound + 1
                    End If
                End If
            Next
        Catch ex As Exception
        End Try
        Return countOfVendorFound
    End Function

    Private Sub chkInActive_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkInActive.ToggleStateChanged
        If chkInActive.Checked = True Then
            dtClosing.Enabled = True
            If DoNotCheckAnyValidationOnVendorInactive = True Then
            Else
                If findVendorCount() >= 1 Then
                    clsCommon.MyMessageBoxShow("Vendor : " + fndvendorNo.Value + " is already in used, so cannot make it inactive.", Me.Text)
                    chkInActive.Checked = False
                End If
            End If
        ElseIf chkInActive.Checked = False Then
            dtClosing.Enabled = False
            dtClosing.Value = connectSql.serverDate()
        End If
    End Sub

    'This will check the authorization of user to access the screen.If authorize then it will allow user to access the screen.
    'Private Function funSetUserAccess() As Boolean
    '    Try

    '        Dim strRights As String
    '        Dim strTemp() As String
    '        Dim strProgCode = "VENDOR-M"
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
#End Region

#Region "Button Click"
    Private Sub btnclear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclear.Click
        Me.Close()
    End Sub

    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        If btnsave.Text = "Update" AndAlso OneTimeCheck = False Then
            Dim frm As New FrmPWD(Nothing)
            frm.strType = clsFixedParameterType.Transactionupdate
            frm.strCode = clsFixedParameterCode.VendorMaster
            frm.ShowDialog()
            If frm.isPasswordCorrect Then
                ShowRemarks()
                OneTimeCheck = True
            End If
        ElseIf btnsave.Text = "Update" AndAlso OneTimeCheck Then
            ShowRemarks()
        Else
            SaveData()
        End If

    End Sub
    Sub SaveData()
        Dim trans As SqlTransaction = Nothing

        Try
            '' Anubhooti 01-Sep-2014
            Dim AllowAutoVCode As String = ""
            AllowAutoVCode = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AutoGeneratedVendorCode, clsFixedParameterCode.AutoGeneratedVendorCode, Nothing))
            If clsCommon.CompairString(AllowAutoVCode, "0") = CompairStringResult.Equal AndAlso fndvendorNo.Value = "" Then
                myMessages.blankValue(Me, "Vendor No.", Me.Text)
                fndvendorNo.Focus()
            ElseIf txtvendorname.Text = "" Then
                myMessages.blankValue(Me, "Vendor Name", Me.Text)
                txtvendorname.Focus()
            ElseIf fndgroupcode.Value = "" Then
                myMessages.blankValue(Me, "Group Code", Me.Text)
                fndgroupcode.Focus()
            ElseIf fndTrmsCode.Value = "" Then
                myMessages.blankValue(Me, "Terms Code", Me.Text)
                fndTrmsCode.Focus()
            ElseIf fndAccntSet.Value = "" Then
                myMessages.blankValue(Me, "Account Set", Me.Text)
                fndAccntSet.Focus()
            Else
                '' Anubhooti 02-Sep-2014 Duplication of vendor desp
                Dim dt1 As DataTable
                dt1 = clsDBFuncationality.GetDataTable("Select * From TSPL_VENDOR_MASTER Where (((ISNULL( ECC,'')='" & txtecc.Text.Trim() & "' and LEN(ISNULL( ECC,'')) > 0))  or ((ISNULL(Email,'')='" & txtEmail.Text.Trim() & "' ANd ISNULL(Email,'')<>'' )) or ((ISNULL(Tin_No,'')='" & txtTinNo.Text.Trim() & "' AND ISNULL(Tin_No,'')<>'' )) or ((ISNULL(Contact_Person_Email,'')='" & txtContactEmail.Text.Trim() & "' ANd ISNULL(Contact_Person_Email,'')<>'' )) ) and (TSPL_VENDOR_MASTER.Vendor_Code not in ('" & fndvendorNo.Value.Trim() & "'))")
                If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
                    If common.clsCommon.MyMessageBoxShow("Vendor exists with same vendor description.Do you still want to continue ", Me.Text, MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
                    Else
                        Exit Sub
                    End If
                End If
                '' Anubhooti 20-Oct-2014 BM00000004198
                If clsCommon.myLen(txttdsstate.Value) > 0 AndAlso clsCommon.myLen(txtState.Value) > 0 Then
                    If clsCommon.CompairString(clsCommon.myCstr(txtState.Value), clsCommon.myCstr(txttdsstate.Value)) <> CompairStringResult.Equal Then
                        pageCus.SelectedPage = RadPageViewPage5
                        txttdsstate.Focus()
                        clsCommon.MyMessageBoxShow("State and TDS state should be same.")
                        Exit Sub
                    End If
                End If

                '' Anubhooti 23-Sep-2014
                Dim qryNatureDed As String = ""
                If ChkIsTDSApp.Checked = True Then
                    If clsCommon.myLen(fnddeducNew.Value) > 0 Then
                        qryNatureDed = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Non_PAN_No From TSPL_TDS_DEDUCTION_HEAD where Deduction_Code ='" & clsCommon.myCstr(fnddeducNew.Value) & "'"))
                    End If
                    If clsCommon.myLen(fnddeducNew.Value) <= 0 Then
                        pageCus.SelectedPage = RadPageViewPage5
                        fnddeducNew.Focus()
                        clsCommon.MyMessageBoxShow(Me, "Nature of Deduction can't be left blank", Me.Text)
                        Exit Sub
                    ElseIf clsCommon.CompairString(qryNatureDed.ToUpper().Trim(), "Y") = CompairStringResult.Equal AndAlso clsCommon.myLen(txtpan.Text) > 0 Then
                        txtpan.Focus()
                        clsCommon.MyMessageBoxShow(Me, "You can not make this entry with Non PAN nature of deduction as PAN No exists.", Me.Text)
                        Exit Sub
                    End If
                    If SettEnableTDSforServiceVendorSeparately Then
                        If clsCommon.myLen(fnddeducNewService.Value) > 0 Then
                            qryNatureDed = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Non_PAN_No From TSPL_TDS_DEDUCTION_HEAD where Deduction_Code ='" & clsCommon.myCstr(fnddeducNewService.Value) & "'"))
                        End If
                        If clsCommon.myLen(fnddeducNewService.Value) <= 0 Then
                            pageCus.SelectedPage = RadPageViewPage5
                            fnddeducNewService.Focus()
                            clsCommon.MyMessageBoxShow(Me, "Service Nature of Deduction can't be left blank", Me.Text)
                            Exit Sub
                        ElseIf clsCommon.CompairString(qryNatureDed.ToUpper().Trim(), "Y") = CompairStringResult.Equal AndAlso clsCommon.myLen(txtpan.Text) > 0 Then
                            txtpan.Focus()
                            clsCommon.MyMessageBoxShow(Me, "You can not make this entry with Non PAN nature of deduction as PAN No exists.", Me.Text)
                            Exit Sub
                        End If
                    End If

                End If
                ''
                '' validation for multicurrency
                fndVendorCurrency.Value = IIf(objCommonVar.IsMultiCurrencyCompany = False, objCommonVar.BaseCurrencyCode, clsCommon.myCstr(fndVendorCurrency.Value))
                If clsCommon.myLen(clsCommon.myCstr(fndVendorCurrency.Value)) > 0 Then
                    If clsCommon.myLen(Me.fndAccntSet.Value) <= 0 Then
                        clsCommon.MyMessageBoxShow(Me, "Select Account Set.", Me.Text)
                        Me.fndAccntSet.Focus()
                        Exit Sub
                    End If

                    If clsCommon.myLen(Me.fndTxGrp.Value) <= 0 Then
                        clsCommon.MyMessageBoxShow(Me, "Select Tax Group.", Me.Text)
                        Me.fndTxGrp.Focus()
                        Exit Sub
                    End If
                    'If clsCommon.CompairString(clsCommon.myCstr(fndVendorCurrency.Value), objCommonVar.BaseCurrencyCode) <> CompairStringResult.Equal Then
                    '' when vendor currency is other than base currency of the company
                    '' match account set currency with vendor currency
                    Dim qry As String
                    qry = "select CURRENCY_CODE from TSPL_VENDOR_ACCOUNT_SET where Acct_Set_Code='" & clsCommon.myCstr(Me.fndAccntSet.Value) & "' "
                    Dim accCurrCode As String = clsDBFuncationality.getSingleValue(qry).ToString
                    If clsCommon.CompairString(accCurrCode, clsCommon.myCstr(Me.fndVendorCurrency.Value)) <> CompairStringResult.Equal Then
                        clsCommon.MyMessageBoxShow(Me, "Account Set Currency and Vendor Currency must be same in case of Multicurrency.", Me.Text)
                        Exit Sub
                    End If
                    '' match tax Group currency with vendor currency
                    qry = " select TSPL_TAX_GROUP_DETAILS.Tax_Code,coalesce(TSPL_TAX_MASTER.CURRENCY_CODE,'') as CURRENCY_CODE from TSPL_TAX_GROUP_DETAILS " &
                          " inner join TSPL_TAX_GROUP_MASTER on TSPL_TAX_GROUP_DETAILS.Tax_Group_Code=TSPL_TAX_GROUP_MASTER.Tax_Group_Code " &
                          " inner join TSPL_TAX_MASTER on TSPL_TAX_GROUP_DETAILS.Tax_Code=TSPL_TAX_MASTER.Tax_Code where TSPL_TAX_GROUP_MASTER.Tax_Group_Code='" & clsCommon.myCstr(Me.fndTxGrp.Value) & "' " &
                          " and coalesce(TSPL_TAX_MASTER.CURRENCY_CODE,'')<>'" & clsCommon.myCstr(Me.fndVendorCurrency.Value) & "'"
                    Dim dt As DataTable
                    dt = clsDBFuncationality.GetDataTable(qry)
                    Dim taxCode As String = ""
                    For Each dr As DataRow In dt.Rows
                        If dt.Rows.IndexOf(dr) = 0 Then
                            taxCode = dr.Item("Tax_Code")
                        Else
                            taxCode = taxCode & "," & dr.Item("Tax_Code")
                        End If
                    Next
                    If clsCommon.myLen(taxCode) > 0 Then
                        clsCommon.MyMessageBoxShow("Tax Code '" & taxCode & "' in Tax Group " & clsCommon.myCstr(Me.fndTxGrp.Value) & " are created for currency other than " & clsCommon.myCstr(Me.fndVendorCurrency.Value) & " .")
                        Exit Sub
                    End If
                    'End If
                End If

                If Not chkparentvendor.Checked AndAlso clsCommon.myLen(fndparent.Value) <= 0 Then
                    clsCommon.MyMessageBoxShow(Me, "Please Select Parent Vendor Code", Me.Text)
                    fndparent.Focus()
                    fndparent.Select()
                    Return
                End If

                '' Anubhooti 06-Jan-2014 (Auto TC Certified)
                If ChkTCCertified.Checked = True AndAlso clsCommon.myLen(TxtTCCertified.Text) <= 0 Then
                    clsCommon.MyMessageBoxShow(Me, "TC certified can not be left blank.", Me.Text)
                    TxtTCCertified.Focus()
                    TxtTCCertified.Select()
                    Return
                End If
                ''
                If clsCommon.myLen(txtCategoryStructureCode.Value) > 0 Then
                    If clsCommon.myLen(gvCategory.Rows(0).Cells(0).Value) <= 0 Then
                        pageCus.SelectedPage = RadPageViewPage3
                        clsCommon.MyMessageBoxShow("First mapped Location category values with Location category structure", Me.Text)
                        gvCategory.Focus()
                        gvCategory.Select()
                        Return
                    End If

                    For Each grow As GridViewRowInfo In gvCategory.Rows
                        If clsCommon.myLen(grow.Cells(CatcolCode).Value) > 0 AndAlso clsCommon.myLen(grow.Cells(CatcolValue).Value) <= 0 Then
                            pageCus.SelectedPage = RadPageViewPage3
                            clsCommon.MyMessageBoxShow(Me, "Please select category values", Me.Text)
                            gvCategory.Focus()
                            gvCategory.Select()
                            Return
                        End If
                    Next
                End If

                If clsCommon.myLen(txtpan.Text) > 0 Then
                    If clsCommon.myLen(txtpan.Text) < 10 Then
                        pageCus.SelectedPage = RadPageViewPage4
                        clsCommon.MyMessageBoxShow(Me, "PAN number should have max. 10 length.", Me.Text)
                        txtpan.Focus()
                        txtpan.Select()
                        Return
                    End If
                    If ChkOther.Checked = False Then
                        If isAllowPanNoValidation = True Then
                            Dim msg As String = clsERPFuncationality.CheckPanStructure(txtpan.Text, txtvendorname.Text)

                            If clsCommon.myLen(msg) > 0 Then
                                pageCus.SelectedPage = RadPageViewPage4
                                clsCommon.MyMessageBoxShow(Me, msg, Me.Text)
                                txtpan.Focus()
                                txtpan.Select()
                                Return
                            End If
                        End If
                    End If
                End If

                ' Modify By : Prabhakar Ref Ticket : BM00000010125
                If clsCommon.myLen(txtPinCode.Text) > 0 Then
                    If clsCommon.myLen(txtPinCode.Text) <> 6 Then
                        pageCus.SelectedPage = RadPageViewPage1
                        clsCommon.MyMessageBoxShow(Me, "Invalid Pin Code.Please Enter Pin Code 6 Digit", Me.Text)
                        txtPinCode.Focus()
                        txtPinCode.Select()
                        Return
                    End If
                End If

                '-----Parteek Added
                If clsCommon.myLen(txtState.Value) <= 0 Then
                    clsCommon.MyMessageBoxShow(Me, "Select State Code", Me.Text)
                    txtState.Focus()
                    txtState.Select()
                    Return
                End If

                '---- End
                If clsCommon.CompairString(CmbVenType.SelectedValue, "CHA") = CompairStringResult.Equal AndAlso clsCommon.myLen(fndCHA_Code.Value) <= 0 Then
                    pageCus.SelectedPage = RadPageViewPage1
                    clsCommon.MyMessageBoxShow(Me, "Select CHA Detail for vendor.", Me.Text)
                    fndCHA_Code.Focus()
                    fndCHA_Code.Select()
                    Return
                End If

                UcCustomFields1.AllowToSave()

                If MyBase.isModifyonPasswordFlag Then
                    If clsPasswordCheckForMasters.CheckMasterPwd(clsUserMgtCode.vendormaster, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
                    Else
                        Return
                    End If
                End If

                If AllowGSTApplicable = True Then
                    If clsCommon.myCdbl(Rchkregistered.Checked) > 0 Then
                        If clsCommon.CompairString(rdrpbusiness.SelectedValue, "Select") = CompairStringResult.Equal Then
                            clsCommon.MyMessageBoxShow(Me, "Select Business Condition", Me.Text)
                            rdrpbusiness.Focus()
                            Return
                        End If

                        Dim GSTFinal As String = clsCommon.myCstr(txtGSTStateCode.Text) + clsCommon.myCstr(txtGST_PanCode.Text) + clsCommon.myCstr(txtEntity.Text) + clsCommon.myCstr(txtFixxed.Text) + clsCommon.myCstr(MyTextBox2.Text)
                        txtGSTIN_No_final.Text = GSTFinal
                        clsERPFuncationality.ValidationGSTNO(txtGSTStateCode.Text, txtpan.Text, GSTFinal, Nothing)
                    End If
                End If

                'Map Employee
                If btnsave.Text = "Save" Then
                    If chkIsEmployee.Checked = True AndAlso clsCommon.myLen(EmployeeFind.Value) > 0 Then
                        Dim StrEmpExist As Integer = 0
                        StrEmpExist = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*) as c from tspl_vendor_master where isnull(EMP_CODE,'')='" + EmployeeFind.Value + "'"))
                        If StrEmpExist > 0 Then
                            common.clsCommon.MyMessageBoxShow(Me, "Employee already map with another vendor.", Me.Text)
                            EmployeeFind.Focus()
                            EmployeeFind.Select()
                            Return
                        End If
                    End If
                ElseIf btnsave.Text = "Update" Then
                    If chkIsEmployee.Checked = True AndAlso clsCommon.myLen(EmployeeFind.Value) > 0 Then
                        Dim StrEmpExist As Integer = 0
                        StrEmpExist = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*) as c from tspl_vendor_master where Vendor_Code<>'" + fndvendorNo.Value + "' and isnull(EMP_CODE,'')='" + EmployeeFind.Value + "'"))
                        If StrEmpExist > 0 Then
                            common.clsCommon.MyMessageBoxShow("Employee already map with another vendor.", Me.Text)
                            EmployeeFind.Focus()
                            EmployeeFind.Select()
                            Return
                        End If
                    End If

                    Dim isPendingInvoice As Boolean = clsCommon.myCBool(clsDBFuncationality.getSingleValue("select count (*) from TSPL_BULK_MILK_PURCHASE_INVOICE_head where Vendor_Code = '" + fndvendorNo.Value + "' and isPosted = 0"))
                    Dim BeforeHighclassValue As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select isHighClass from TSPL_VENDOR_MASTER where Vendor_Code = '" + fndvendorNo.Value + "' "))
                    Dim invoiceNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select STUFF((Select ',['+Doc_No+']'  from (select TSPL_BULK_MILK_PURCHASE_INVOICE_head.Doc_No from TSPL_BULK_MILK_PURCHASE_INVOICE_head  where Vendor_Code = '" + fndvendorNo.Value + "' and isPosted = 0 ) XXX For XML Path('')),1,1,'') "))
                    Dim AfterHighclassValue As Integer = 0 ' IIf(chkHighClass.Checked, True, , 0)
                    If chkHighClass.Checked = True Then
                        AfterHighclassValue = 1
                    End If
                    If BeforeHighclassValue <> AfterHighclassValue AndAlso isPendingInvoice = True Then
                        common.clsCommon.MyMessageBoxShow("You can change high Class after Post all Pending document (" + invoiceNo + ") of Bulk Milk Purchase Invoice against " + fndvendorNo.Value + " Vendor.", Me.Text)
                        Return
                    End If
                End If

                trans = clsDBFuncationality.GetTransactin()

                If btnsave.Text = "Save" Then
                    funinsert(trans)
                ElseIf btnsave.Text = "Update" Then
                    funupdate(trans)
                End If


            End If
            If chktrarns.Checked Then
                Save_Transport_Data(trans)
            End If
            trans.Commit()
            clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
            fndvendorNo_text_changed(fndvendorNo.Value)

        Catch ex As Exception
            'trans.Rollback()
            ' myMessages.myExceptions(ex)
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub btndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click
        DeleteData()
    End Sub
    Sub DeleteData()
        If fndvendorNo.Value = "" Then
            myMessages.blankValue(Me, "Vendor No.", Me.Text)
        ElseIf myMessages.deleteConfirm() Then
            fundeleteTransport()
            fundelete()
        End If
    End Sub

    Private Sub btnnew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnnew.Click
        funreset()
        isLoadCopy = False
        ''For Custom Fields
        If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
            UcCustomFields1.SetDefaultValues()
        End If
        ''End of For Custom Fields
    End Sub

    Private Sub MenuExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuExport.Click
        funExport()
    End Sub

    Private Sub MenuImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuImport.Click
        funImport()
    End Sub
    Private Sub RadMenuItem4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem4.Click
        Me.Close()
    End Sub
#End Region

    Public Sub LoadType()
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow = dt.NewRow()
        dr("Code") = "Select"
        dr("Name") = "Select"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Individual"
        dr("Name") = "Individual"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Partnership"
        dr("Name") = "Partnership"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Firm"
        dr("Name") = "Firm"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "HUF"
        dr("Name") = "HUF"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Company"
        dr("Name") = "Company"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "AOP"
        dr("Name") = "AOP"
        dt.Rows.Add(dr)

        rdrpbusiness.DataSource = dt
        rdrpbusiness.ValueMember = "Code"
        rdrpbusiness.DisplayMember = "Name"

    End Sub

    Private Sub frmVendorMaster_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag Then
            btnsave.PerformClick()
            'SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag Then
            'PostData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag Then
            DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Close()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            funreset()
        End If
    End Sub

    Private Sub fndvendorNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndvendorNo._MYValidating
        If isButtonClicked Then
            'Dim qry As String = "select Vendor_Code as [Vendor Code],Vendor_Name as [Vendor Name],Vendor_Group_Code as [Vendor Group],(select case when Status='N' then 'Active' else 'In Active' end ) as [Status] from TSPL_VENDOR_MASTER "

            'fndvendorNo.Value = clsCommon.ShowSelectForm("fmVedrNofnd", qry, "Vendor Code", "", fndvendorNo.Value, "", isButtonClicked)
            ''fndvendorNo.Value = clsVendorMaster.getFinder(" form_type='ALL' or  form_type='VSP'", fndvendorNo.Value, isButtonClicked)
            fndvendorNo.Value = clsVendorMaster.getFinder(" form_type='ALL'", fndvendorNo.Value, isButtonClicked) ''Do not show VSP Type as asked by amit sir and tested by bhavna by balwinder on 2015/01/07
            'fndvendorNo.Value = clsVendorMaster.("", fndvendorNo.Value, isButtonClicked)
            txtvendorname.Text = clsDBFuncationality.getSingleValue("Select group_desc from tspl_vendor_group where ven_group_code='" + fndvendorNo.Value + "'")
            If fndvendorNo.Value IsNot Nothing Then
                btndelete.Enabled = True
            Else
                btndelete.Enabled = False
            End If
            fndvendorNo_text_changed()
        ElseIf fndvendorNo.MyReadOnly OrElse fndvendorNo.Value IsNot Nothing Then
            Dim qry As String = "Select * from TSPL_VENDOR_MASTER where Vendor_Code ='" + fndvendorNo.Value + "' and form_type='" + txtvndrtype.Text + "' "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt.Rows.Count <= 0 Then
                'chkcsa.Checked = False
                txtaccsetdes.Text = ""
                txtvendorname.Text = ""
                txtvendortypedes.Text = ""
                txtWeb.Text = ""
                txtTxGrp.Text = ""
                txtTinNo.Text = ""
                txttermcodedes.Text = ""
                txtStaxNo.Text = ""
                lblStateName.Text = ""
                txtRemarks2.Text = ""
                txtRemarks1.Text = ""
                txtrange.Text = ""
                txtPhone2.Text = ""
                txtPhone1.Text = ""
                txtpaymentcodedes.Text = ""
                txtpan.Text = ""
                txtLstNo.Text = ""
                txtfax.Text = ""
                txtgroupdes.Text = ""
                txtEmail.Text = ""
                txtecc.Text = ""
                txtcst.Text = ""
                txtCredit.Text = ""
                txtCountry.Text = ""
                txtContPhone.Text = ""
                txtContactWeb.Text = ""
                txtContactName.Text = ""
                txtContactFax.Text = ""
                txtAdd1.Text = ""
                txtAdd2.Text = ""
                txtAdd3.Text = ""
                txtAddInfo1.Text = ""
                txtAddInfo2.Text = ""
                txtAddInfo3.Text = ""
                TxtBankName.Text = ""
                txtCity.Text = ""
                txtcollect.Text = ""
                txtContactEmail.Text = ""
                txtAliesName.Text = ""
                TxtOldname.Text = ""
                chkHold.Checked = False
                chkInActive.Checked = False
                InActiveCF.Checked = False
                chkInterBranch.Checked = False
                chkTagAsFranchise.Checked = False
                chkVendorInvoiceNo.Checked = False
                chkOEM.Checked = False
                chkProvisional.Checked = False
                chkDefaultGrower.Checked = False
                '' Anubhooti 10-Oct-2014 BM00000004198 (Removed Gross Receipt)
                'chkIsGrossReceipt.Checked = False
                chktrarns.Checked = False
                fndgroupcode.Value = Nothing
                fndCity.Value = Nothing
                EmployeeFind.Value = Nothing
                fndVendorReg.Value = Nothing
                btnsave.Text = "Save"
            Else
                fndvendorNo_text_changed()
            End If
        End If
    End Sub

    Private Sub fndpaymentCycle__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndpaymentCycle._MYValidating
        Try
            fndpaymentCycle.Value = clsPaymentCycleMaster.getFinder("", fndpaymentCycle.Value, isButtonClicked)
            If clsCommon.myLen(fndpaymentCycle.Value) > 0 Then
                lblpaymentCycle.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select DESCRIPTION from TSPL_PAYMENT_CYCLE_MASTER where PC_CODE='" + fndpaymentCycle.Value + "'"))
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.ToString)
        End Try
    End Sub

    Private Sub fndgroupcode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndgroupcode._MYValidating
        ' If isButtonClicked Then
        'Dim qry As String = "SELECT ven_Group_Code as [VendorGroupCode],Group_Desc as [Description],Tax_Group_Code as [Tax Group],Acct_Set_Code as [Account Set],Terms_Code as [Terms Code] FROM [TSPL_VENDOR_GROUP]  "
        'fndgroupcode.Value = clsCommon.ShowSelectForm("grcodefmfnd", qry, "VendorGroupCode", "", fndgroupcode.Value, "", isButtonClicked)
        fndgroupcode.Value = clsVendorGroupMaster.getFinder("", fndgroupcode.Value, isButtonClicked)
        'txtvendorname.Text = clsDBFuncationality.getSingleValue("Select group_desc from tspl_vendor_group where ven_group_code='" + fndgroupcode.Value + "'")
        fndgroupcode_text_Changed()
        fndgroupcode_leave()
        If clsVendorMaster.checkisIDSapplicable(fndgroupcode.Value, Nothing) Then
            ChkIsTDSApp.Checked = True
            ChkIsTDSApp.Enabled = False
        Else
            ChkIsTDSApp.Enabled = True
        End If
        '   End If
    End Sub
    Private Sub txtJWPriceCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtJWPriceCode._MYValidating
        Try
            Dim qry As String = "SELECT Code,[Description] as [Description] FROM TSPL_JOB_OUTWARD_PRICE_MASTER "
            txtJWPriceCode.Value = clsCommon.ShowSelectForm("JWPrice", qry, "Code", "", txtJWPriceCode.Value, "", isButtonClicked)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub fndCity__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndCity._MYValidating
        'If clsCommon.myLen(txtState.Value) <= 0 Then
        '    clsCommon.MyMessageBoxShow("Please Select State Code First", Me.Text)
        '    txtState.Focus()
        '    txtState.Select()
        '    Return
        'End If
        '    If isButtonClicked Then
        'Dim qry As String = "SELECT [City_Code] as [CityCode],[City_Name] as [City Name]FROM [TSPL_CITY_MASTER] "
        'fndCity.Value = clsCommon.ShowSelectForm("CityFmfnd", qry, "CityCode", "", fndCity.Value, "", isButtonClicked)
        fndCity.Value = clsCityMaster.getFinder("", fndCity.Value, isButtonClicked)
        If clsCommon.myLen(fndCity.Value) > 0 Then
            Dim obj As clsCityMaster = clsCityMaster.GetData(fndCity.Value, NavigatorType.Current)
            If Not IsNothing(obj) Then
                txtCity.Text = obj.City_Name
                txtState.Value = obj.STATE_CODE
                LblState.Text = obj.STATE_NAME
                txtcountrycode.Value = clsDBFuncationality.getSingleValue("select country_code from tspl_State_master where state_Code='" & txtState.Value & "'")
                txtCountry.Text = clsDBFuncationality.getSingleValue("select country_name from tspl_Country_master where Country_Code='" & txtcountrycode.Value & "'")
            End If
            'txtCity.Text = clsDBFuncationality.getSingleValue("Select City_Name from TSPL_CITY_MASTER where City_Code='" + fndCity.Value + "' and state_code='" + txtState.Value + "'")

        Else
            txtCity.Text = ""
            fndCity.Value = ""
        End If
        ''fndcity_text_Changed()
        ''fndCity_leave()
        '  End If
    End Sub

    Private Sub fndvendorNo__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavigatorType As common.NavigatorType) Handles fndvendorNo._MYNavigator
        Dim qst As String = "select Vendor_Code as [Vendor Code],Vendor_Name as [Vendor Name],Vendor_Group_Code as [Vendor Group],(select case when Status='N' then 'Active' else 'In.Active' end ) as [Status] from TSPL_VENDOR_MASTER    where  2=2 and (form_type='ALL' or form_type='VSP')"
        Select Case NavigatorType
            Case NavigatorType.Current
                '  qst += "and assign_to='" + txtassign.Value + "' "
                ' qst += "and job_code in ('" + txtcode1.Value + "')"
            Case NavigatorType.Next
                qst += "and Vendor_Code in (select min(Vendor_Code) from TSPL_VENDOR_MASTER where Vendor_Code>'" + fndvendorNo.Value + "' and form_type='" + txtvndrtype.Text + "'  ) "
            Case NavigatorType.First
                qst += "and Vendor_Code in (select MIN(Vendor_Code) from TSPL_VENDOR_MASTER where form_type='" + txtvndrtype.Text + "')"
            Case NavigatorType.Last
                qst += "and Vendor_Code in (select Max(Vendor_Code) from TSPL_VENDOR_MASTER where form_type='" + txtvndrtype.Text + "' )"
            Case NavigatorType.Previous
                qst += "and Vendor_Code in (select max(Vendor_Code) from TSPL_VENDOR_MASTER where Vendor_Code<'" + fndvendorNo.Value + "' and form_type='" + txtvndrtype.Text + "'  )"
        End Select
        ' fun_gridfill()
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qst)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            fndvendorNo.Value = clsCommon.myCstr(dt.Rows(0)("Vendor Code"))
            txtvendorname.Text = clsCommon.myCstr(dt.Rows(0)("Vendor Name"))
        End If
        'TextChanged()
        If fndvendorNo.Value IsNot Nothing Then
            btndelete.Enabled = True
        Else
            btndelete.Enabled = False

        End If
        fndvendorNo_text_changed()
    End Sub

    Private Sub fndTrmsCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndTrmsCode._MYValidating
        'fndTrmsCode.Query = "SELECT [Terms_Code] as [Terms Code],[Terms_Desc] as [Description],[No_Days] as [No. of Days] FROM [TSPL_TERMS_MASTER]"
        'fndTrmsCode.ConnectionString = connectSql.SqlCon()
        'fndTrmsCode.Caption = "Payments Terms"
        'fndTrmsCode.ValueToSelect = "Terms Code"
        'fndTrmsCode.ValueToSelect1 = "Description"

        Dim qry As String = "SELECT [Terms_Code] as [Terms Code],[Terms_Desc] as [Description],[No_Days] as [No. of Days] FROM [TSPL_TERMS_MASTER]"
        fndTrmsCode.Value = clsCommon.ShowSelectForm("fndtrms", qry, "Terms Code", "", fndTrmsCode.Value, "", isButtonClicked)

        txttermcodedes.Text = clsDBFuncationality.getSingleValue("Select Terms_Desc from TSPL_TERMS_MASTER where Terms_Code='" + fndTrmsCode.Value + "'")

    End Sub

    Private Sub fndAccntSet__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndAccntSet._MYValidating
        'fndAccntSet.Query = "SELECT  [Acct_Set_Code] as [Account Set Code],[Acct_Set_Desc] as [Description] FROM [TSPL_VENDOR_ACCOUNT_SET]"
        'fndAccntSet.ConnectionString = connectSql.SqlCon()
        'fndAccntSet.Caption = "Vendor Account Set"
        'fndAccntSet.ValueToSelect = "Account Set Code"
        'fndAccntSet.ValueToSelect1 = "Description"

        'Dim qry As String = "SELECT  [Acct_Set_Code] as [Account Set Code],[Acct_Set_Desc] as [Description] FROM [TSPL_VENDOR_ACCOUNT_SET]"
        'fndAccntSet.Value = clsCommon.ShowSelectForm("fndacccnt", qry, "Account Set Code", "", fndAccntSet.Value, "", isButtonClicked)
        fndAccntSet.Value = clsVendorAccountSet.getFinder("", fndAccntSet.Value, isButtonClicked)
        txtaccsetdes.Text = clsDBFuncationality.getSingleValue("Select Acct_Set_Desc from TSPL_VENDOR_ACCOUNT_SET where Acct_Set_Code='" + fndAccntSet.Value + "'")
        fndVendorCurrency.Value = clsDBFuncationality.getSingleValue("Select COALESCE(CURRENCY_CODE,'') AS CURRENCY_CODE from TSPL_VENDOR_ACCOUNT_SET where Acct_Set_Code='" + fndAccntSet.Value + "'")
    End Sub

    Private Sub fndPayCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndPayCode._MYValidating
        'fndPayCode1.Query = "  SELECT [Payment_Code] as [Payment Code],[payment_Desc] as [Description] FROM [TSPL_PAYMENT_CODE]"
        'fndPayCode1.ConnectionString = connectSql.SqlCon()
        'fndPayCode1.Caption = "Payment Code"
        'fndPayCode1.ValueToSelect = "Payment Code"
        'fndPayCode1.ValueToSelect1 = "Description"

        'Dim qry As String = "  SELECT [Payment_Code] as [Payment Code],[payment_Desc] as [Description] FROM [TSPL_PAYMENT_CODE]"
        'fndPayCode.Value = clsCommon.ShowSelectForm("fndpayy", qry, "Payment Code", "", fndPayCode.Value, "", isButtonClicked)
        fndPayCode.Value = clsPaymentCode.getFinder("", fndPayCode.Value, isButtonClicked)
        txtpaymentcodedes.Text = clsDBFuncationality.getSingleValue("Select payment_Desc from TSPL_PAYMENT_CODE where Payment_Code='" + fndPayCode.Value + "'")

    End Sub

    Private Sub fndvendortype__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndvendortype._MYValidating
        'fndvendortype1.Query = "SELECT  [ven_Type_Code] as [Vendor Type Code],[ven_Type_Desc] as [Description]FROM [TSPL_VENDOR_TYPE_MASTER]"
        'fndvendortype1.ConnectionString = connectSql.SqlCon()
        'fndvendortype1.Caption = "Vendor Type"
        'fndvendortype1.ValueToSelect = "Vendor Type Code"
        'fndvendortype1.ValueToSelect1 = "Description"

        Dim qry As String = "SELECT  [ven_Type_Code] as [Vendor Type Code],[ven_Type_Desc] as [Description]FROM [TSPL_VENDOR_TYPE_MASTER]"
        fndvendortype.Value = clsCommon.ShowSelectForm("fndvrn", qry, "Vendor Type Code", "", fndvendortype.Value, "", isButtonClicked)

        txtvendortypedes.Text = clsDBFuncationality.getSingleValue("Select ven_Type_Desc from TSPL_VENDOR_TYPE_MASTER where ven_Type_Code='" + fndvendortype.Value + "'")
    End Sub

    Private Sub fndbankcode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean)
        'fndbankcode.ConnectionString = connectSql.SqlCon()
        ''fndbankcode.Query = " select bank_code As [Bank Code],description  as [Description]from TSPL_Bank_MASTER "
        'fndbankcode.Query = clsERPFuncationality.glbankquery
        'fndbankcode.ValueToSelect = "Bank Code"
        'fndbankcode.Caption = "Bank Master"
        'fndbankcode.ValueToSelect1 = "Description"
        Dim whrcls As String = ""
        Dim qry As String = clsERPFuncationality.glbankquery(whrcls)
        'fndbankcode.Value = clsCommon.ShowSelectForm("fndbannk", qry, "Bank Code", whrcls, fndbankcode.Value, "", isButtonClicked)
        'txtbankcodedes.Text = clsDBFuncationality.getSingleValue("Select Description from TSPL_BANK_MASTER where BANK_CODE='" + fndbankcode.Value + "'")

    End Sub

    Private Sub fndTxGrp__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndTxGrp._MYValidating
        'fndTxGrp.Query = clsERPFuncationality.UserAvailableTaxGroup + " and  M.Tax_Group_Type='P'"
        'fndTxGrp.ConnectionString = connectSql.SqlCon()
        'fndTxGrp.Caption = "Tax Group"
        'fndTxGrp.ValueToSelect = "Code"
        'fndTxGrp.ValueToSelect1 = "Description"
        Dim whrcls As String = ""
        Dim qry As String = clsERPFuncationality.UserAvailableTaxGroup(whrcls) + " and  M.Tax_Group_Type='P'"
        If (whrcls <> "") Then
            whrcls = whrcls + " and  M.Tax_Group_Type='P'"
        Else
            whrcls = " M.Tax_Group_Type='P'"
        End If
        If AllowGSTApplicable = True Then
            whrcls += " and Active=1"
        End If


        fndTxGrp.Value = clsCommon.ShowSelectForm("fndtxx", qry, "Code", whrcls, fndTxGrp.Value, "", isButtonClicked)
        txtTxGrp.Text = clsDBFuncationality.getSingleValue("Select Tax_Group_Desc from TSPL_TAX_GROUP_MASTER where TAX_Group_Code='" + fndTxGrp.Value + "'")
    End Sub

    Private Sub fndBaseCurrency__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndVendorCurrency._MYValidating
        Dim qry As String = "select CURRENCY_CODE AS Code, CURRENCY_NAME as Name from TSPL_CURRENCY_MASTER"
        fndVendorCurrency.Value = clsCommon.ShowSelectForm("CURRENCY_MASTER", qry, "Code", "", fndVendorCurrency.Value, "CURRENCY_CODE", isButtonClicked)

    End Sub

    Private Sub txtcountrycode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtcountrycode._MYValidating
        Try
            Dim qry As String = "select country_code as Code,country_name as Country from tspl_country_master"
            txtcountrycode.Value = clsCommon.ShowSelectForm("CNTFND", qry, "Code", "", txtcountrycode.Value, "Code", isButtonClicked)

            If clsCommon.myLen(txtcountrycode.Value) > 0 Then
                txtCountry.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select country_name from tspl_country_master where country_code='" + txtcountrycode.Value + "'"))
                LblState.Text = ""
                txtState.Value = ""
                txtCity.Text = ""
                fndCity.Value = ""
            Else
                txtCountry.Text = ""
                LblState.Text = ""
                txtState.Value = ""
                txtCity.Text = ""
                fndCity.Value = ""
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtstatecode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean)
        If clsCommon.myLen(txtcountrycode.Value) <= 0 Then
            clsCommon.MyMessageBoxShow("Please Select Country First", Me.Text)
            txtcountrycode.Focus()
            txtcountrycode.Select()
            Return
        End If

        Try
            Dim qry As String = "select State_code as Code,state_name as State from tspl_state_master"
            txttdsstate.Value = clsCommon.ShowSelectForm("STAFND", qry, "Code", " country_code='" + txtcountrycode.Value + "'", txtcountrycode.Value, "Code", isButtonClicked)

            If clsCommon.myLen(txttdsstate.Value) > 0 Then
                lblStateName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select state_name from tspl_state_master where state_code='" + txttdsstate.Value + "'"))
            Else
                lblStateName.Text = ""
                txttdsstate.Value = ""
                txtCity.Text = ""
                fndCity.Value = ""
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Sub chkparentvendorstatus()
        If chkparentvendor.Checked Then
            fndparent.Enabled = False
            txtparentname.Enabled = False
            fndparent.MendatroryField = False
            fndparent.Value = ""
            txtparentname.Text = ""
        Else
            fndparent.Enabled = True
            txtparentname.Enabled = True
            fndparent.MendatroryField = True
        End If
    End Sub

    Private Sub chkparentvendor_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkparentvendor.ToggleStateChanged
        chkparentvendorstatus()
    End Sub

    Private Sub fndparent__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndparent._MYValidating
        fndparent.Value = clsCommon.myCstr(clsVendorMaster.getFinder(" tspl_vendor_master.Is_Parent_Vendor='1'", fndparent.Value, isButtonClicked))

        If clsCommon.myLen(fndparent.Value) > 0 Then
            txtparentname.Text = clsCommon.myCstr(clsVendorMaster.GetName(fndparent.Value, Nothing))
        Else
            txtparentname.Text = ""
        End If
    End Sub

    Private Sub txtCategoryStructureCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtCategoryStructureCode._MYValidating
        Dim qry As String = "select ITEM_CATEGORY_STRUCT_CODE as Code,DESCRIPTION from TSPL_ITEM_CATEGORY_STRUCTURE"
        txtCategoryStructureCode.Value = clsCommon.ShowSelectForm("ITEMMASTERCATSTRU", qry, "Code", " isnull(form_type,'item')='vendor'", txtCategoryStructureCode.Value, "Code", isButtonClicked)
        LoadBlankGridCat()
        If clsCommon.myLen(txtCategoryStructureCode.Value) > 0 Then
            lblCategoryStructureCode.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select DESCRIPTION from TSPL_ITEM_CATEGORY_STRUCTURE where ITEM_CATEGORY_STRUCT_CODE='" + txtCategoryStructureCode.Value + "' and isnull(form_type,'Item')='vendor'"))

            qry = "select TSPL_ITEM_CATEGORY_STRUCT_DETAIL.ITEM_CATEGORY_CODE ,TSPL_ITEM_CATEGORY_LEVEL.DESCRIPTION  "
            qry += " from TSPL_ITEM_CATEGORY_STRUCT_DETAIL"
            qry += " left outer join TSPL_ITEM_CATEGORY_LEVEL on  TSPL_ITEM_CATEGORY_LEVEL.ITEM_CATEGORY_CODE=TSPL_ITEM_CATEGORY_STRUCT_DETAIL.ITEM_CATEGORY_CODE and TSPL_ITEM_CATEGORY_LEVEL.form_type=TSPL_ITEM_CATEGORY_STRUCT_DETAIL.form_type"
            qry += " where TSPL_ITEM_CATEGORY_STRUCT_DETAIL.ITEM_CATEGORY_STRUCT_CODE='" + txtCategoryStructureCode.Value + "' and isnull(TSPL_ITEM_CATEGORY_STRUCT_DETAIL.form_type,'Item')='vendor' order by TSPL_ITEM_CATEGORY_STRUCT_DETAIL.CATEGORY_LEVEL"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    gvCategory.Rows.AddNew()
                    gvCategory.Rows(gvCategory.RowCount - 1).Cells(CatcolCode).Value = clsCommon.myCstr(dr("ITEM_CATEGORY_CODE"))
                    gvCategory.Rows(gvCategory.RowCount - 1).Cells(CatcolCodeDesc).Value = clsCommon.myCstr(dr("DESCRIPTION"))
                Next
            End If
        Else
            lblCategoryStructureCode.Text = ""
        End If
    End Sub

    Sub LoadBlankGridCat()
        gvCategory.Rows.Clear()
        gvCategory.Columns.Clear()

        Dim repoCatCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCatCode.FormatString = ""
        repoCatCode.HeaderText = "Category Code"
        repoCatCode.Name = CatcolCode
        repoCatCode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoCatCode.Width = 100L
        gvCategory.MasterTemplate.Columns.Add(repoCatCode)

        Dim repoCatCodeDesc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCatCodeDesc.FormatString = ""
        repoCatCodeDesc.HeaderText = "Category Description"
        repoCatCodeDesc.Name = CatcolCodeDesc

        repoCatCodeDesc.Width = 200
        gvCategory.MasterTemplate.Columns.Add(repoCatCodeDesc)

        Dim repoCatValue As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCatValue.FormatString = ""
        repoCatValue.HeaderText = "Category Value"
        repoCatValue.Name = CatcolValue
        repoCatValue.Width = 100
        repoCatValue.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoCatValue.TextImageRelation = TextImageRelation.TextBeforeImage
        repoCatValue.ReadOnly = False
        gvCategory.MasterTemplate.Columns.Add(repoCatValue)

        Dim repoCatValueDesc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCatValueDesc.FormatString = ""
        repoCatValueDesc.HeaderText = "Category Value Description"
        repoCatValueDesc.Name = CatcolValueDesc
        repoCatValueDesc.Width = 200
        repoCatValueDesc.ReadOnly = True
        gvCategory.MasterTemplate.Columns.Add(repoCatValueDesc)

        gvCategory.AllowAddNewRow = False
        gvCategory.ShowGroupPanel = False
        gvCategory.AllowColumnReorder = False
        gvCategory.AllowRowReorder = False
        gvCategory.EnableSorting = False
        gvCategory.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gvCategory.MasterTemplate.ShowRowHeaderColumn = False
    End Sub

    Sub OpenCatValueList(ByVal isButtonClick As Boolean)

        gvCategory.CurrentRow.Cells(CatcolValueDesc).Value = ""
        Dim qry As String = " select CODE,DESCRIPTION from TSPL_ITEM_CATEGORY_LEVEL_VALUES"
        gvCategory.CurrentRow.Cells(CatcolValue).Value = clsCommon.ShowSelectForm("itemMAsCatFind", qry, "Code", " ITEM_CATEGORY_CODE='" + clsCommon.myCstr(gvCategory.CurrentRow.Cells(CatcolCode).Value) + "' and isnull(form_type,'Item')='vendor'", clsCommon.myCstr(gvCategory.CurrentRow.Cells(CatcolValue).Value), "Code", isButtonClick)
        qry = "select DESCRIPTION from TSPL_ITEM_CATEGORY_LEVEL_VALUES where ITEM_CATEGORY_CODE='" + clsCommon.myCstr(gvCategory.CurrentRow.Cells(CatcolCode).Value) + "' and CODE='" + clsCommon.myCstr(gvCategory.CurrentRow.Cells(CatcolValue).Value) + "' and isnull(form_type,'Item')='vendor'"
        gvCategory.CurrentRow.Cells(CatcolValueDesc).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
    End Sub

    Private Sub gvCategory_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvCategory.CellValueChanged
        Try
            If (Not isInsideLoadData) Then
                If Not isCellValueChangedOpen Then
                    isCellValueChangedOpen = True
                    If e.Column Is gvCategory.Columns(CatcolValue) Then
                        OpenCatValueList(False)
                    End If
                    isCellValueChangedOpen = False
                End If
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    '' Anubhooti 1-Aug-2014
    Sub LoadAccountType()
        Dim dt As New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        dt.Rows.Add("Cur", "Current")
        dt.Rows.Add("Sav", "Saving")
        dt.Rows.Add("Cas", "Cash")
        dt.Rows.Add("Cre", "Credit")
        dt.Rows.Add("Loa", "Loan")
        dt.Rows.Add("Oth", "Others")

        cmbAccountType.DataSource = dt
        cmbAccountType.DisplayMember = "Name"
        cmbAccountType.ValueMember = "Code"
    End Sub

    '' Anubhooti 20-Oct-2014 BM00000004240
    Sub LoadVendorTypeCHA()
        Dim dt As New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        dt.Rows.Add("", "Select")
        dt.Rows.Add("CSA", "CSA")
        dt.Rows.Add("CHA", "CHA")
        dt.Rows.Add("BROKER", "BROKER")
        dt.Rows.Add("CV", "Chilling Vendor")
        'dt.Rows.Add("COM", "Commission Type")
        '' Anubhooti 17-Nov-2014 BM00000004655
        dt.Rows.Add("A", "Agent")
        dt.Rows.Add("O", "Other")
        dt.Rows.Add("M", "Milk")
        dt.Rows.Add("J", "Job Order Milk")

        CmbVenType.DataSource = dt
        CmbVenType.DisplayMember = "Name"
        CmbVenType.ValueMember = "Code"
    End Sub
    '' Anubhooti 4-Aug-2014 BM00000003368
    Sub LoadVendorType()
        Dim dt As New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))
        dt.Rows.Add("N", "None")
        dt.Rows.Add("A", "A")
        dt.Rows.Add("B", "B")
        dt.Rows.Add("O", "Others")

        cmbTypeOfVen.DataSource = dt
        cmbTypeOfVen.DisplayMember = "Name"
        cmbTypeOfVen.ValueMember = "Code"
    End Sub

    Private Sub fnddeducNew__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fnddeducNew._MYValidating
        Dim query As String = "select deduction_code As [Code],description  as [Description] from TSPL_TDS_DEDUCTION_HEAD"
        fnddeducNew.Value = clsCommon.ShowSelectForm("DeductionCodevald", query, "Code", "", fnddeducNew.Value, "Code", isButtonClicked)
        Dim desc As String = "select  description  from TSPL_TDS_DEDUCTION_HEAD where deduction_code ='" & fnddeducNew.Value & "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(desc)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            lblNatureOfDed.Text = clsCommon.myCstr(dt.Rows(0)("description"))
        Else
            lblNatureOfDed.Text = ""
        End If
    End Sub

    Private Sub fnddeducNewServiceService__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fnddeducNewService._MYValidating
        Dim query As String = "select deduction_code As [Code],description  as [Description] from TSPL_TDS_DEDUCTION_HEAD"
        fnddeducNewService.Value = clsCommon.ShowSelectForm("DeductionCodevald", query, "Code", "", fnddeducNewService.Value, "Code", isButtonClicked)
        Dim desc As String = "select  description  from TSPL_TDS_DEDUCTION_HEAD where deduction_code ='" & fnddeducNewService.Value & "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(desc)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            lblNatureOfDedService.Text = clsCommon.myCstr(dt.Rows(0)("description"))
        Else
            lblNatureOfDedService.Text = ""
        End If
    End Sub


    Private Sub ChkIsTDSApp_CheckStateChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkIsTDSApp.CheckStateChanged
        If ChkIsTDSApp.Checked = True Then
            GrpTDS.Enabled = True
            fnddeducNew.MendatroryField = True
            'fndbranchnew.MendatroryField = True

            GrpTDSService.Enabled = True
        Else
            GrpTDS.Enabled = False
            fnddeducNew.Value = ""
            lblNatureOfDed.Text = ""
            fndbranchnew.Value = ""
            LblBranchName.Text = ""
            txttdsstate.Value = ""
            lblStateName.Text = ""
            ddlventype.Text = "Individual"
            ddlstatus.Text = "Resident"
            fnddeducNew.MendatroryField = False


            GrpTDSService.Enabled = False
            fnddeducNewService.Value = ""
            lblNatureOfDedService.Text = ""
            fndbranchnewService.Value = ""
            LblBranchNameService.Text = ""
            txttdsstateService.Value = ""
            lblStateNameService.Text = ""
            ddlventypeService.Text = "Individual"
            ddlstatusService.Text = "Resident"
            fnddeducNewService.MendatroryField = False
        End If
    End Sub

    Private Sub fndbranchnew__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndbranchnew._MYValidating
        Dim query As String = "select Branch_Code as [Code],Branch_Name as [Branch Name], State_Code as [State Code],State_Name as [State Name] from TSPL_TDS_BRANCH_MASTER"
        fndbranchnew.Value = clsCommon.ShowSelectForm("BranchCodevald", query, "Code", "", fndbranchnew.Value, "Code", isButtonClicked)
        Dim desc As String = "select  Branch_Name from  TSPL_TDS_BRANCH_MASTER where Branch_Code='" & fndbranchnew.Value & "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(desc)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            LblBranchName.Text = clsCommon.myCstr(dt.Rows(0)("Branch_Name"))
        Else
            LblBranchName.Text = ""
        End If
    End Sub

    Private Sub fndbranchnewService__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndbranchnewService._MYValidating
        Dim query As String = "select Branch_Code as [Code],Branch_Name as [Branch Name], State_Code as [State Code],State_Name as [State Name] from TSPL_TDS_BRANCH_MASTER"
        fndbranchnewService.Value = clsCommon.ShowSelectForm("BranchCodevald", query, "Code", "", fndbranchnewService.Value, "Code", isButtonClicked)
        Dim desc As String = "select  Branch_Name from  TSPL_TDS_BRANCH_MASTER where Branch_Code='" & fndbranchnewService.Value & "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(desc)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            LblBranchNameService.Text = clsCommon.myCstr(dt.Rows(0)("Branch_Name"))
        Else
            LblBranchNameService.Text = ""
        End If
    End Sub

    Private Sub txtState__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtState._MYValidating
        If clsCommon.myLen(txtcountrycode.Value) <= 0 Then
            clsCommon.MyMessageBoxShow("Please Select Country First", Me.Text)
            txtcountrycode.Focus()
            txtcountrycode.Select()
            Return
        End If

        Try
            Dim qry As String = "select State_code as Code,state_name as State from tspl_state_master"
            '======update by preeti gupta against ticket no [BM00000008670]
            txtState.Value = clsCommon.ShowSelectForm("STAFND", qry, "Code", " country_code='" + txtcountrycode.Value + "'", txtState.Value, "Code", isButtonClicked)

            If clsCommon.myLen(txtState.Value) > 0 Then
                LblState.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select state_name from tspl_state_master where state_code='" + txtState.Value + "'"))
                txtGSTStateCode.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select GST_State_Code from tspl_state_master where state_code='" + txtState.Value + "'"))
            Else
                LblState.Text = ""
                txtState.Value = ""

                txtCity.Text = ""
                fndCity.Value = ""
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txttdsstate__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txttdsstate._MYValidating
        Dim query As String = "select State_Code as [Code],State_Name as [State Name] from tspl_state_master"
        txttdsstate.Value = clsCommon.ShowSelectForm("StateCodefrm", query, "Code", "", txttdsstate.Value, "Code", isButtonClicked)
        If clsCommon.myLen(txttdsstate.Value) > 0 Then
            Dim desc As String = "select  State_Name from tspl_state_master where State_Code='" & txttdsstate.Value & "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(desc)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                lblStateName.Text = clsCommon.myCstr(dt.Rows(0)("State_Name"))
            Else
                lblStateName.Text = ""
            End If
        Else
            lblStateName.Text = ""
        End If

    End Sub

    Private Sub txttdsstateService__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txttdsstateService._MYValidating
        Dim query As String = "select State_Code as [Code],State_Name as [State Name] from tspl_state_master"
        txttdsstateService.Value = clsCommon.ShowSelectForm("StateCodefrm", query, "Code", "", txttdsstateService.Value, "Code", isButtonClicked)
        If clsCommon.myLen(txttdsstateService.Value) > 0 Then
            Dim desc As String = "select  State_Name from tspl_state_master where State_Code='" & txttdsstateService.Value & "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(desc)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                lblStateNameService.Text = clsCommon.myCstr(dt.Rows(0)("State_Name"))
            Else
                lblStateNameService.Text = ""
            End If
        Else
            lblStateNameService.Text = ""
        End If

    End Sub

    Private Sub fndbankcode__MYValidating_1(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndbankcode._MYValidating
        ' BM00000007800 Resloved Error
        Dim whrcls As String = ""
        If EnableBankFromMaster = True Then
            Dim qry As String = clsCommon.myCstr(clsERPFuncationality.glbankquery())
            fndbankcode.Value = clsVendorBankMaster.GetFinder("", fndbankcode.Value, isButtonClicked)
            If clsCommon.myLen(fndbankcode.Value) > 0 Then
                Dim obj As clsVendorBankMaster = clsVendorBankMaster.GetData(fndbankcode.Value, NavigatorType.Current)
                If obj Is Nothing Then
                    Exit Sub
                End If
                TxtBankName.Text = obj.Bank_Name
                txtbankcountry.Text = obj.country_name
                txtbankstate.Text = obj.state_name
                txtbankcity.Text = obj.city_name
                ''richa agarwal 24/03/2015
                'TxtBankBranch.Value = obj.Branch_Code
                'txtbranchname.Text = obj.Branch_Name
                'TxtIFSCCode.Text = obj.IFSC_Code
                ''--------------
            Else
                TxtBankName.Text = ""
                txtbankcountry.Text = "India"
                txtbankstate.Text = ""
                txtbankcity.Text = ""
                txtbranchname.Text = ""
                TxtAccNo.Text = ""
                TxtIFSCCode.Text = ""
                ''richa agarwal 24/03/2015
                'TxtBankBranch.Value = ""
                'txtbranchname.Text = ""
                'TxtIFSCCode.Text = ""
                ''-----------------
            End If
        End If

    End Sub

    Private Sub RadPageViewPage4_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles RadPageViewPage4.Paint

    End Sub

    Private Sub txtpan_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtpan.TextChanged
        Try
            If clsCommon.myLen(txtpan.Text) <= 0 Then
                Exit Sub
            End If
            If ChkOther.Checked = False Then
                Dim msg As String = clsERPFuncationality.CheckPanStructure(txtpan.Text, txtvendorname.Text)
                txtGST_PanCode.Text = txtpan.Text
                If isAllowPanNoValidation = True Then
                    If clsCommon.myLen(msg) > 0 Then
                        pageCus.SelectedPage = RadPageViewPage4
                        clsCommon.MyMessageBoxShow(msg, Me.Text)
                        txtpan.Focus()
                        txtpan.Select()
                        Return
                    End If
                End If
            Else
                txtGST_PanCode.Text = txtpan.Text
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub CmbVenType_SelectedValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmbVenType.SelectedValueChanged
        If clsCommon.myLen(CmbVenType.SelectedValue) > 0 AndAlso clsCommon.CompairString(clsCommon.myCstr(CmbVenType.SelectedValue), "CHA") = CompairStringResult.Equal Then
            fndCHA_Code.Enabled = True
        Else
            fndCHA_Code.Enabled = False
            fndCHA_Code.Value = ""
            txtCHA_Amount.Text = ""
            txtCHA_Type.Text = ""
        End If
    End Sub

    Private Sub fndCHA_Code__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndCHA_Code._MYValidating
        fndCHA_Code.Value = clsCHAChargeMaster.GetFinder("", fndCHA_Code.Value, isButtonClicked)
        txtCHA_Type.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select distinct (select ','+(case when cha_type='ICD' then 'Dry Port-ICD(Inland Container Depot)' else case when cha_type='ISD' then 'Sea Port' else case when cha_type='THC' then 'Terminal Handling Charges at ICD& Sea Port' else case when cha_type='IHC' then 'Inland Haulage Charges at ICD& Sea Port' else case when cha_type='OTH' then 'Other' else 'None' end end end end end) from tspl_cha_charge_master where doc_no='" + fndCHA_Code.Value + "' for xml path(''))"))
        txtCHA_Amount.Text = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select amount from tspl_cha_charge_master where doc_no='" + fndCHA_Code.Value + "'"))
    End Sub
    '' Anubhooti 06-Jan-2014
    Private Sub ChkTCCertified_CheckStateChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkTCCertified.CheckStateChanged
        If ChkTCCertified.Checked = True Then
            TxtTCCertified.Enabled = True
            TxtTCCertified.MendatroryField = True
        Else
            TxtTCCertified.Enabled = False
            TxtTCCertified.MendatroryField = False
        End If
    End Sub

    Private Sub TxtBankBranch__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles TxtBankBranch._MYValidating
        If EnableBankFromMaster = True Then
            If clsCommon.myLen(fndbankcode.Value) > 0 Then
                Dim qry As String = "Select Bank_IFSC_Code as IFSCCode,Branch_Name from TSPL_Vendor_Bank_Branch_Details "
                TxtBankBranch.Value = clsCommon.ShowSelectForm("FormIFSCCode", qry, "IFSCCode", " Bank_Code ='" & fndbankcode.Value & "' ", TxtBankBranch.Value, "", isButtonClicked)
                txtbranchname.Text = clsDBFuncationality.getSingleValue("Select Branch_Name from TSPL_Vendor_Bank_Branch_Details where Bank_Code ='" & fndbankcode.Value & "' and Bank_IFSC_Code='" & TxtBankBranch.Value & "' ")
            Else
                clsCommon.MyMessageBoxShow("Please select Bank Code first")
            End If
        End If
    End Sub

    Public Sub Save_Transport_Data(ByVal trans As SqlTransaction, Optional ByVal str_vendor As String = Nothing)
        If clsCommon.myLen(str_vendor) <= 0 Then
            Dim str As String = "select count(*) from tspl_Transport_master where Transport_Id='" & clsCommon.myCstr(fndvendorNo.Value) & "'"
            Dim check As Integer = clsDBFuncationality.getSingleValue(str, trans)
            If check > 0 Then
                funupdateTransport(trans)
            Else
                funinsertTransport(trans)
            End If
        Else
            For Each str_cc As String In Regex.Split(str_vendor, ",")
                funfill(str_cc)
                Dim str As String = "select count(*) from tspl_Transport_master where Transport_Id='" & clsCommon.myCstr(fndvendorNo.Value) & "'"
                Dim check As Integer = clsDBFuncationality.getSingleValue(str, trans)
                If check > 0 Then
                    funupdateTransport(trans)
                Else
                    funinsertTransport(trans)
                End If
                funreset()
            Next
        End If

    End Sub

    'Funtion for insertion of data
    Public Sub funinsertTransport(ByVal trans As SqlTransaction)
        Try


            'connectSql.RunSpTransaction(trans, "sp_transportmaster_insert", New SqlParameter("@transid", fndvendorNo.Value), New SqlParameter("@transname", txtvendorname.Text.ToString()), New SqlParameter("@city", txtCity.Text.ToString()), New SqlParameter("@state", LblState.Text.ToString()), New SqlParameter("@pincode", txtPinCode.Text.ToString()), New SqlParameter("@panno", txtpan.Text.ToString()), New SqlParameter("@phone", txtPhone1.Text.ToString()), New SqlParameter("@add1", txtAdd1.Text.ToString()), New SqlParameter("@add2", txtAdd2.Text.ToString()), New SqlParameter("@email", txtEmail.Text.ToString()), New SqlParameter("@createdby", userCode), New SqlParameter("@createddate", connectSql.serverDate()), New SqlParameter("@modifiedby", userCode), New SqlParameter("@modifieddate", connectSql.serverDate(trans)), New SqlParameter("@compcode", companyCode))
            ' myMessages.insert()
            Dim obj As New clsTRANSPORT_MASTER
            obj.Transport_Id = fndvendorNo.Value
            obj.Transporter_Name = txtvendorname.Text
            obj.city = clsCommon.myCstr(txtCity.Text)
            obj.state = clsCommon.myCstr(txtState.Text)
            obj.pincode = clsCommon.myCstr(txtPinCode.Text)
            obj.panno = clsCommon.myCstr(txtpan.Text)
            obj.Phone = clsCommon.myCstr(txtPhone1.Text)
            obj.Add1 = clsCommon.myCstr(txtAdd1.Text)
            obj.Add2 = clsCommon.myCstr(txtAdd2.Text)
            obj.Email = clsCommon.myCstr(txtEmail.Text)
            If clsTRANSPORT_MASTER.SaveData(obj, True, trans) Then
                SaveasCustomer(trans, True)
                btnsave.Text = "Update"
                btndelete.Enabled = True
            End If


            'If userCode <> "ADMIN" Then
            '    If funSetUserAccess() = False Then Exit Sub
            'End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub

    'Funtion for updation  of data
    Public Sub funupdateTransport(ByVal trans As SqlTransaction)
        Try
            'Dim currentdate As Date = Date.Today
            'connectSql.RunSpTransaction(trans, "sp_transportmaster_update", New SqlParameter("@transid", fndvendorNo.Value), New SqlParameter("@transname", txtvendorname.Text.ToString()), New SqlParameter("@city", txtCity.Text.ToString()), New SqlParameter("@state", LblState.Text.ToString()), New SqlParameter("@pincode", txtPinCode.Text.ToString()), New SqlParameter("@panno", txtpan.Text.ToString()), New SqlParameter("@phone", txtPhone1.Text.ToString()), New SqlParameter("@add1", txtAdd1.Text.ToString()), New SqlParameter("@add2", txtAdd2.Text.ToString()), New SqlParameter("@email", txtEmail.Text.ToString()), New SqlParameter("@modifiedby", userCode), New SqlParameter("@modifieddate", connectSql.serverDate(trans)), New SqlParameter("@compcode", companyCode))
            Dim obj As New clsTRANSPORT_MASTER
            obj.Transport_Id = fndvendorNo.Value
            obj.Transporter_Name = txtvendorname.Text
            obj.city = clsCommon.myCstr(txtCity.Text)
            obj.state = clsCommon.myCstr(txtState.Text)
            obj.pincode = clsCommon.myCstr(txtPinCode.Text)
            obj.panno = clsCommon.myCstr(txtpan.Text)
            obj.Phone = clsCommon.myCstr(txtPhone1.Text)
            obj.Add1 = clsCommon.myCstr(txtAdd1.Text)
            obj.Add2 = clsCommon.myCstr(txtAdd2.Text)
            obj.Email = clsCommon.myCstr(txtEmail.Text)
            If clsTRANSPORT_MASTER.SaveData(obj, False, trans) Then
                SaveasCustomer(trans, False)

            End If
            'SaveasCustomer(trans, False)
            ' myMessages.update()
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub

    'Function for deletion of data
    Public Sub fundeleteTransport()
        Try
            Dim currentdate As Date = Date.Today
            connectSql.RunSp("sp_transportmaster_delete", New SqlParameter("@transid", fndvendorNo.Value.ToString()))
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub

    '' against ticket no BM00000005875 by richa agarwal
    Private Sub fndbankcode__MYOpenMasterForm(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndbankcode._MYOpenMasterForm
        Dim frmVBM As New FrmVendorBankMaster()
        frmVBM.SetUserMgmt(clsUserMgtCode.VendorBankMaster)
        frmVBM.ShowDialog()
        fndbankcode.Value = frmVBM.BAnkCodeValue
        If clsCommon.myLen(fndbankcode.Value) > 0 Then
            Dim dt As DataTable = clsDBFuncationality.GetDataTable("Select TSPL_Vendor_Bank_MASTER.Bank_Name,TSPL_CITY_MASTER.City_Name ,TSPL_STATE_MASTER.STATE_NAME,TSPL_COUNTRY_MASTER.COUNTRY_NAME   from TSPL_Vendor_Bank_MASTER left outer join TSPL_COUNTRY_MASTER on TSPL_COUNTRY_MASTER.COUNTRY_CODE =TSPL_Vendor_Bank_MASTER.Country_Code left outer join TSPL_STATE_MASTER on TSPL_STATE_MASTER.STATE_CODE  =TSPL_Vendor_Bank_MASTER.State_Code  left outer join TSPL_CITY_MASTER  on TSPL_CITY_MASTER.City_Code  =TSPL_Vendor_Bank_MASTER.City_Code  where TSPL_Vendor_Bank_MASTER.Bank_Code ='" & fndbankcode.Value & "' ")
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                TxtBankName.Text = clsCommon.myCstr(dt.Rows(0)("Bank_Name"))
                txtbankcountry.Text = clsCommon.myCstr(dt.Rows(0)("COUNTRY_NAME"))
                txtbankstate.Text = clsCommon.myCstr(dt.Rows(0)("STATE_NAME"))
                txtbankcity.Text = clsCommon.myCstr(dt.Rows(0)("City_Name"))
                TxtBankBranch.Value = ""
                txtbranchname.Text = ""
            Else
                TxtBankName.Text = ""
                txtbankcountry.Text = ""
                txtbankstate.Text = ""
                txtbankcity.Text = ""
                TxtBankBranch.Value = ""
                txtbranchname.Text = ""
            End If
        End If
    End Sub
    ' Modify By : Prabhakar Ref Ticket : BM00000010125
    Private Sub txtPinCode_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPinCode.KeyPress
        If Not Char.IsNumber(e.KeyChar) AndAlso Not e.KeyChar = Chr(Keys.Back) Then e.Handled = True
    End Sub

    Private Sub Rchkregistered_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles Rchkregistered.ToggleStateChanged
        Try
            If Rchkregistered.Checked Then
                RchkCompscheme.Enabled = True
                txtGSTIN_No_final.Enabled = False
                txtEntity.Enabled = True
                MyTextBox2.Enabled = True
                txtGST_PanCode.Text = txtpan.Text
                If clsCommon.myLen(txtState.Value) > 0 Then
                    txtGSTStateCode.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select GST_State_Code from tspl_state_master where state_code='" + txtState.Value + "'"))
                End If
            Else
                '' changed by Panch Raj
                RchkCompscheme.Enabled = True
                txtEntity.Enabled = False
                MyTextBox2.Enabled = False
                ''update 28/06/2017 Parteek
                txtGST_PanCode.Text = ""
                txtEntity.Text = ""
                txtGSTStateCode.Text = ""
                txtGSTIN_No_final.Text = ""
                MyTextBox2.Text = ""
                ''end
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message())
        End Try
    End Sub

    Private Sub btnHistory_Click(sender As Object, e As EventArgs) Handles btnHistory.Click
        Try
            If clsCommon.myLen(fndvendorNo.Value) <= 0 Then
                clsCommon.MyMessageBoxShow("Select Vendor")
                Exit Sub
            End If
            clsERPFuncationalityOLD.ShowHistoryData(fndvendorNo.Value, "Vendor_Code", "TSPL_VENDOR_MASTER")
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Sub

    Private Sub fndBulkRouteCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndBulkRouteCode._MYValidating
        Dim qry As String = "SELECT  [ROUTE_NO] as [Route No],[ROUTE_NAME] as [Route Name] FROM [TSPL_BULK_ROUTE_MASTER]"
        fndBulkRouteCode.Value = clsCommon.ShowSelectForm("fndBulk@Route", qry, "Route No", "", fndBulkRouteCode.Value, "", isButtonClicked)
        lblBulkRouteNo.Text = clsDBFuncationality.getSingleValue("Select ROUTE_NAME from TSPL_BULK_ROUTE_MASTER where ROUTE_NO='" + fndBulkRouteCode.Value + "'")
    End Sub

    Private Sub EmployeeFind__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles EmployeeFind._MYValidating
        EmployeeFind.Value = clsEmployeeMaster.getFinder("", EmployeeFind.Value, isButtonClicked)
    End Sub

    Private Sub FndVendorReg__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndVendorReg._MYValidating
        'If clsCommon.myLen(fndvendorNo.Value) <= 0 Then
        Dim Whre As String = "Is_VendorRegApproved=1 and not exists (select 1 from TSPL_VENDOR_MASTER where TSPL_VENDOR_MASTER.vendor_code<>'" & fndvendorNo.Value & "' and TSPL_VENDOR_MASTER.Registration_No=TSPL_VENDORREGISTRATION_MASTER.Registration_No)"
        fndVendorReg.Value = clsVendorReg.getFinder(Whre, fndVendorReg.Value, isButtonClicked)
        If clsCommon.myLen(fndVendorReg.Value) > 0 Then
            Dim obj As clsVendorReg = clsVendorReg.GetData(fndVendorReg.Value, NavigatorType.Current)
            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Code) > 0 Then
                txtvendorname.Text = obj.Name
                txtAliesName.Text = obj.Name
                txtAdd1.Text = obj.Address1
                txtAdd2.Text = obj.Address2
                txtPhone1.Text = obj.Phone_No
                txtfax.Text = obj.Fax_No
                txtContactName.Text = obj.Contact_Person_Name
                txtContPhone.Text = obj.Contact_Person_Phone_No
            End If
        End If
        'End If
    End Sub

    Private Sub RadButton1_Click(sender As Object, e As EventArgs) Handles RadButton1.Click
        Try
            isLoadCopy = True
            Dim strVendor As String = clsVendorMaster.getFinder(" form_type='ALL'", "", True)
            If clsCommon.myLen(strVendor) > 0 Then
                fndvendorNo.Value = strVendor
                fndvendorNo_text_changed()
                fndvendorNo.Value = ""
                txtvendorname.Text = ""
            Else
                funreset()
                isLoadCopy = False
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub

    Private Sub ShowRemarks()
        Try
            Dim Reason As String = ""
            Dim frm As New FrmFreeTxtBox1
            frm.Text = "Remarks for Update"
            frm.ShowDialog()
            If clsCommon.myLen(frm.strRmks) <= 0 Then
                Exit Sub
            Else
                Reason = frm.strRmks
            End If
            SaveData()
            saveCancelLog(Reason, "Updated", Nothing)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Function saveCancelLog(ByVal Reason As String, ByVal Activity_Type As String, Optional ByVal trans As System.Data.SqlClient.SqlTransaction = Nothing) As Boolean
        Dim obj As New clsCancelLog
        obj.Program_Code = Form_ID
        obj.DOCUMENT_NO = clsCommon.myCstr(Me.fndvendorNo.Value)
        obj.REASON = Reason
        obj.ACTIVITY_TYPE = Activity_Type
        Return clsCancelLog.SaveData(obj, True, trans)
    End Function

    Private Sub RadCheckBox1_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles InActiveCF.ToggleStateChanged

    End Sub

    Public Sub SaveasCustomer(ByVal trans As SqlTransaction, ByVal isNewEntry As Boolean)
        Try
            Dim obj As New clsCustomerMaster()
            obj.Cust_Code = fndvendorNo.Value
            obj.Customer_Name = txtvendorname.Text
            obj.Alies_Name = clsCommon.myCstr(txtAliesName.Text)
            If isNewEntry = True Then
                obj.Created_By = objCommonVar.CurrentUserCode
            Else
                obj.Modify_By = objCommonVar.CurrentUserCode
            End If
            If chktrarns.Checked Then
                obj.Form_Type = "TPT"
                obj.CUSTOMER_FORM_TYPE = "ALL"
            Else
                obj.Form_Type = ""
                obj.CUSTOMER_FORM_TYPE = ""
            End If

            obj.Add1 = clsCommon.myCstr(txtAdd1.Text)
            obj.Add2 = clsCommon.myCstr(txtAdd2.Text)
            obj.Add3 = clsCommon.myCstr(txtAdd3.Text)
            obj.City_Code = clsCommon.myCstr(fndCity.Value)
            obj.State = clsCommon.myCstr(txtState.Value)
            obj.Country = clsCommon.myCstr(txtcountrycode.Value)
            obj.PIN_NO = clsCommon.myCstr(txtPinCode.Text)

            If chkTCSNotApplicable.Checked = True Then
                obj.IsTCSnotApplicable = 1
            Else
                obj.IsTCSnotApplicable = 0
            End If
            obj.Phone1 = clsCommon.myCstr(txtPhone1.Text)
            obj.Phone2 = clsCommon.myCstr(txtPhone2.Text)
            obj.Fax = clsCommon.myCstr(txtfax.Text)
            obj.Email = clsCommon.myCstr(txtEmail.Text)
            obj.WebSite = clsCommon.myCstr(txtWeb.Text)
            obj.Contact_Person_Name = clsCommon.myCstr(txtContactName.Text)
            obj.Contact_Person_Phone = clsCommon.myCstr(txtContPhone.Text)
            obj.Contact_Person_Fax = clsCommon.myCstr(txtContactFax.Text)
            obj.Contact_Person_Email = clsCommon.myCstr(txtContactEmail.Text)
            obj.Contact_Person_Website = clsCommon.myCstr(txtContactWeb.Text)
            obj.Terms_Code = clsCommon.myCstr(fndTrmsCode.Value)
            obj.Cust_Account = clsCommon.myCstr(fndAccntSet.Value)
            obj.Tax_Group = clsCommon.myCstr(fndTxGrp.Value)
            If (grdTax.Rows.Count > 0) Then
                obj.TAX1 = clsCommon.myCstr(grdTax.Rows(0).Cells(0).Value)
                obj.TAX1_Rate = clsCommon.myCdbl(grdTax.Rows(0).Cells(1).Value)
            End If
            If (grdTax.Rows.Count > 1) Then
                obj.TAX2 = clsCommon.myCstr(grdTax.Rows(1).Cells(0).Value)
                obj.TAX2_Rate = clsCommon.myCdbl(grdTax.Rows(1).Cells(1).Value)
            End If
            If (grdTax.Rows.Count > 2) Then
                obj.TAX3 = clsCommon.myCstr(grdTax.Rows(2).Cells(0).Value)
                obj.TAX3_Rate = clsCommon.myCdbl(grdTax.Rows(2).Cells(1).Value)
            End If
            If (grdTax.Rows.Count > 3) Then
                obj.TAX4 = clsCommon.myCstr(grdTax.Rows(3).Cells(0).Value)
                obj.TAX4_Rate = clsCommon.myCdbl(grdTax.Rows(3).Cells(1).Value)
            End If
            If (grdTax.Rows.Count > 4) Then
                obj.TAX5 = clsCommon.myCstr(grdTax.Rows(4).Cells(0).Value)
                obj.TAX5_Rate = clsCommon.myCdbl(grdTax.Rows(4).Cells(1).Value)
            End If
            If (grdTax.Rows.Count > 5) Then
                obj.TAX6 = clsCommon.myCstr(grdTax.Rows(5).Cells(0).Value)
                obj.TAX6_Rate = clsCommon.myCdbl(grdTax.Rows(5).Cells(1).Value)
            End If
            If (grdTax.Rows.Count > 6) Then
                obj.TAX7 = clsCommon.myCstr(grdTax.Rows(6).Cells(0).Value)
                obj.TAX7_Rate = clsCommon.myCdbl(grdTax.Rows(6).Cells(1).Value)
            End If
            If (grdTax.Rows.Count > 7) Then
                obj.TAX8 = clsCommon.myCstr(grdTax.Rows(7).Cells(0).Value)
                obj.TAX8_Rate = clsCommon.myCdbl(grdTax.Rows(7).Cells(1).Value)
            End If
            If (grdTax.Rows.Count > 8) Then
                obj.TAX9 = clsCommon.myCstr(grdTax.Rows(8).Cells(0).Value)
                obj.TAX9_Rate = clsCommon.myCdbl(grdTax.Rows(8).Cells(1).Value)
            End If
            If (grdTax.Rows.Count > 9) Then
                obj.TAX10 = clsCommon.myCstr(grdTax.Rows(9).Cells(0).Value)
                obj.TAX10_Rate = clsCommon.myCdbl(grdTax.Rows(9).Cells(1).Value)
            End If
            obj.Payment_Code = clsCommon.myCstr(fndPayCode.Value)
            obj.Service_Tax_No = clsCommon.myCstr(txtStaxNo.Text)
            obj.Tin_No = clsCommon.myCstr(txtTinNo.Text)
            obj.Lst_No = clsCommon.myCstr(txtLstNo.Text)
            '============
            obj.Remarks1 = clsCommon.myCstr(txtRemarks1.Text)
            obj.Remarks2 = clsCommon.myCstr(txtRemarks2.Text)
            obj.Additional1 = clsCommon.myCstr(txtAddInfo1.Text)
            obj.Additional2 = clsCommon.myCstr(txtAddInfo2.Text)
            obj.Additional3 = clsCommon.myCstr(txtAddInfo3.Text)
            obj.OutLet_Commossion = clsCommon.myCdbl(0) '--default 0
            obj.Balance_ToDate = 0                      '--Default 0
            obj.Credit_Limit = clsCommon.myCdbl(txtCredit.Text)
            obj.CST = clsCommon.myCstr(txtcst.Text)
            obj.ECC = clsCommon.myCstr(txtecc.Text)
            obj.Range = clsCommon.myCstr(txtrange.Text)
            obj.Collectorate = clsCommon.myCstr(txtcollect.Text)
            obj.PAN = clsCommon.myCstr(txtpan.Text)
            obj.Cust_Group_Code = clsCommon.myCstr(fndgroupcode.Value)
            Dim IsSave As Boolean = obj.SaveData(obj, Nothing, isNewEntry, trans)
            If isNewEntry AndAlso IsSave Then
                Dim qry As String = "insert into TSPL_CUSTOMER_VENDOR_MAPPING values('" + clsCommon.myCstr(fndvendorNo.Value) + "','" + clsCommon.myCstr(fndvendorNo.Value) + "')"

                clsDBFuncationality.ExecuteNonQuery(qry, trans)

            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Sub
End Class