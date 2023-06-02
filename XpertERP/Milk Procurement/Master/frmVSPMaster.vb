'=======BM00000007843============
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
Imports XpertERPHRandPayroll
Imports XpertERPCommanServices

Public Class frmVSPMaster
    Inherits FrmMainTranScreen
    Dim userCode, companyCode As String
    Dim str As String
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim Errorcontrol As clsErrorControl = New clsErrorControl()
    Dim checkPan As New System.Text.RegularExpressions.Regex("^([A-Z]){5}([0-9]){4}([A-Z]){1}")
    Dim IsInsieLoadData As Boolean
    Dim Frm_Open As FrmMainTranScreen
    Dim FixVSPEMP As Integer = 0
    Dim AllowVSPMasterAutoPrefix As Integer = 0
    Dim EnableBankFromMaster As Boolean
    Const colEMPSno As String = "colEMPSno"
    Const colEMPSlab As String = "colEMPSlab"
    Const colEMPValue As String = "colEMPValue"
    Dim TIPRateMix As Double = 0
    Dim TIPRateCow As Double = 0
    Dim TIPRateBuffalo As Double = 0
    Dim DefaultCustomerGroupCodeforVSP As String = ""
    Dim DefaultVendorGroupCodeforVSP As String = ""
    Dim isLoadCopy As Boolean = False
    Dim Allow_Reg_PDCS_CLUSTER_2ndBank_MCC_VLCVSPMaster As Boolean = False
    Public Sub New(ByVal user As String, ByVal company As String)
        InitializeComponent()
        userCode = user
        companyCode = company
    End Sub

#Region "Page Load"
    Private Sub frmVSPMaster_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        AllowVSPMasterAutoPrefix = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowVSPMasterAutoPrefix, clsFixedParameterCode.AllowVSPMasterAutoPrefix, Nothing))
        FixVSPEMP = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.FixVSPEMP, clsFixedParameterCode.FixVSPEMP, Nothing))
        EnableBankFromMaster = IIf(clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Description from TSPL_FIXED_PARAMETER where Code='" & clsFixedParameterCode.EnableBankFromMaster & "'")) = 0, False, True)

        TIPRateMix = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.TIPRateMix, clsFixedParameterCode.TIPRateMix, Nothing))
        TIPRateCow = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.TIPRateCow, clsFixedParameterCode.TIPRateCow, Nothing))
        TIPRateBuffalo = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.TIPRateBuffalo, clsFixedParameterCode.TIPRateBuffalo, Nothing))
        DefaultCustomerGroupCodeforVSP = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.DefaultCustomerGroupCodeforVSP, clsFixedParameterCode.DefaultCustomerGroupCodeforVSP, Nothing))
        DefaultVendorGroupCodeforVSP = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.DefaultVendorGroupCodeforVSP, clsFixedParameterCode.DefaultVendorGroupCodeforVSP, Nothing))
        Allow_Reg_PDCS_CLUSTER_2ndBank_MCC_VLCVSPMaster = IIf(clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Description from TSPL_FIXED_PARAMETER where Code='" & clsFixedParameterCode.Allow_Reg_PDCS_CLUSTER_2ndBank_MCC_VLCVSPMaster & "'")) = 0, False, True)
        funreset()
        SetLength()
        pageCus.SelectedPage = RadPageViewPage1
        SetUserMgmtNew()
        ToolTipvendor.SetToolTip(btnnew, "New")
        '' Anubhooti 4-Aug-2014 BM00000003319
        LoadAccountType()
        ''
        fndvendorNo_text_changed()
        fndgroupcode_text_Changed()
        fndcity_text_Changed()

        fndgroupcode_leave()
        fndCity_leave()
        LoadEMPType()

        chkInActive.Checked = False
        dtClosing.Enabled = False
        btndelete.Enabled = False
        btnsave.Enabled = True

        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update Trasnaction")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnclear, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnnew, "Press Alt+N Adding New Trasnaction")
        txtChequeInFavour.Enabled = True

        '' check for multi currency
        If objCommonVar.IsDemoERP = True Then
            If CheckMultiCurrency(Nothing) = True Then
                lblBaseCurrency.Visible = True
                Me.fndVendorCurrency.Visible = True
            Else
                lblBaseCurrency.Visible = False
                Me.fndVendorCurrency.Visible = False

            End If
        Else
            lblBaseCurrency.Visible = False
            fndVendorCurrency.Visible = False
            fndVendorCurrency.Value = objCommonVar.BaseCurrencyCode
        End If

        If EnableBankFromMaster = True Then
            findfndbankcode.Visible = True
            findfndbankcode.Value = ""
            findTxtIFSCCode.Visible = True
            findTxtIFSCCode.Value = ""
            txtBankCode.Visible = True
            txtBankCode.Value = ""
            txtBankBranchCode.Visible = True
            txtBankBranchCode.Value = ""
            fndbankcode.Visible = False
            TxtIFSCCode.Visible = False
            texttxtBankCode.Visible = False
            texttxtBankBranchCode.Visible = False
            TxtBankName.ReadOnly = True
            findfndbankcode2.Visible = True
            findfndbankcode2.Value = ""
        Else
            fndbankcode.Visible = True
            TxtIFSCCode.Visible = True
            texttxtBankCode.Visible = True
            texttxtBankBranchCode.Visible = True
            findfndbankcode.Visible = False
            findfndbankcode.Value = ""
            findTxtIFSCCode.Visible = False
            findTxtIFSCCode.Value = ""
            txtBankCode.Visible = False
            txtBankCode.Value = ""
            txtBankBranchCode.Visible = False
            txtBankBranchCode.Value = ""
            TxtBankName.ReadOnly = False
            findfndbankcode2.Visible = False
            findfndbankcode2.Value = ""
        End If

        '---------------------------------
        txtvndrtype.Text = "VSP"
        LoadVSPPayment()
        LoadIncentive()
        '---------------------------------------------------------

        ''For Custom Fields
        pageCus.Pages("pvpCustomFields").Item.Visibility = MyBase.customFieldTabProperty
        If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
            UcCustomFields1.Report_ID = MyBase.Form_ID
            UcCustomFields1.LoadCustomControls()
        End If
        ''End of For Custom Fields

        RadPageViewPage2.Item.Visibility = ElementVisibility.Collapsed
        'RadPageViewPage5.Item.Visibility = ElementVisibility.Collapsed
        ' pvpCustomFields.Item.Visibility = ElementVisibility.Collapsed
        txtcountrycode.Value = clsDBFuncationality.getSingleValue("select country_Code from tspl_Country_Master where Country_Code='INDIA'") '""
        txtCountry.Text = clsDBFuncationality.getSingleValue("select country_Name from tspl_Country_Master where Country_Code='INDIA'") '""
        Pg_Bank.Text = "Joint Bank" & Environment.NewLine & " & " & "Security Details"
        RadPageViewPage6.Text = "Head Load and" & Environment.NewLine & " & " & "Own Asset Details"
        If objCommonVar.GSTApplicable Then
            Pg_GST.Enabled = True
        Else
            Pg_GST.Enabled = False
        End If
        funSetDefaultData()

        chkRegistered.Visible = Allow_Reg_PDCS_CLUSTER_2ndBank_MCC_VLCVSPMaster
        chkPDCS.Visible = Allow_Reg_PDCS_CLUSTER_2ndBank_MCC_VLCVSPMaster
        chkCLUSTER.Visible = Allow_Reg_PDCS_CLUSTER_2ndBank_MCC_VLCVSPMaster
        lblStartDate.Visible = Allow_Reg_PDCS_CLUSTER_2ndBank_MCC_VLCVSPMaster
        txtStartDate.Visible = Allow_Reg_PDCS_CLUSTER_2ndBank_MCC_VLCVSPMaster
        chkOwnBMC.Visible = Allow_Reg_PDCS_CLUSTER_2ndBank_MCC_VLCVSPMaster
        lblOwnMCC.Visible = Allow_Reg_PDCS_CLUSTER_2ndBank_MCC_VLCVSPMaster
        txtMCCOwnBMC.Visible = Allow_Reg_PDCS_CLUSTER_2ndBank_MCC_VLCVSPMaster
        lblMCCOwnBMC.Visible = Allow_Reg_PDCS_CLUSTER_2ndBank_MCC_VLCVSPMaster
        gbBank2Details.Visible = Allow_Reg_PDCS_CLUSTER_2ndBank_MCC_VLCVSPMaster
        lblMCCOwnBMC.Visible = Allow_Reg_PDCS_CLUSTER_2ndBank_MCC_VLCVSPMaster
        txtSupervisiorRP.Visible = Allow_Reg_PDCS_CLUSTER_2ndBank_MCC_VLCVSPMaster
        lblSupervisiorRP.Visible = Allow_Reg_PDCS_CLUSTER_2ndBank_MCC_VLCVSPMaster
    End Sub
    Function CheckMultiCurrency(ByVal trans As SqlTransaction) As Boolean
        Dim strq As String
        strq = "select * from tspl_module_currency_mapping where comp_code='" + objCommonVar.CurrentCompanyCode + "' and module_code='" & Me.Module_Code & "'"
        Dim dt As DataTable
        dt = clsDBFuncationality.GetDataTable(strq, trans)
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
        txtvendorname.MaxLength = 50
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
        txtbankcodedes.MaxLength = 50
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

    End Sub
    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.frmVSPMaster)
        If Not (MyBase.isReadFlag) Then

            If MDI.blnShowAllMenu = False Then
                common.clsCommon.MyMessageBoxShow("Permission Denied")
            Else
                Throw New Exception("Can't Access in demo version. " + Environment.NewLine + " For any queries/details, contact tecxpert@tecxpert.in. ")
            End If

            Me.Close()
            Exit Sub
        End If
        btnsave.Visible = MyBase.isModifyFlag
        'btnPost.Visible = MyBase.isPostFlag
        btndelete.Visible = MyBase.isDeleteFlag
        If Not (MyBase.isReadFlag) Then
            MenuExport.Visibility = ElementVisibility.Collapsed
        End If
        If Not (MyBase.isModifyFlag) Then
            MenuImport.Visibility = ElementVisibility.Collapsed
        End If
    End Sub
#End Region

#Region "Function"

    '' Anubhooti 4-Aug-2014 BM00000003319
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
    'It will fill the  controls if value exist in database according to fndgroupcode
    Public Sub funfillfndGroupCode()
        Try
            Dim strquery As String = "select group_desc,tax_Group_Code,Acct_Set_code,Terms_COde,Bank_Code ,payment_code from Tspl_vendor_group where ven_group_code='" + fndgroupcode.Value + "'"
            Dim dr As DataTable
            'Dim strvalue As String
            dr = clsDBFuncationality.GetDataTable(strquery)
            If dr.Rows IsNot Nothing AndAlso dr.Rows.Count > 0 Then
                txtgroupdes.Text = dr.Rows(0)("group_desc").ToString()
                fndTxGrp.Value = dr.Rows(0)("tax_Group_Code").ToString()
                txtTxGrp.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select tax_code_desc from tspl_tax_master where tax_code='" + clsCommon.myCstr(fndTxGrp.Value) + "'"))
                If clsCommon.myLen(txtTxGrp.Text) <= 0 Then
                    fndTxGrp.Value = ""
                    txtTxGrp.Text = ""
                End If
                fndAccntSet.Value = dr.Rows(0)("Acct_Set_code").ToString()
                txtaccsetdes.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select acct_set_desc from TSPL_VENDOR_ACCOUNT_SET where acct_set_code='" + clsCommon.myCstr(fndAccntSet.Value) + "'"))
                If clsCommon.myLen(txtaccsetdes.Text) <= 0 Then
                    fndAccntSet.Value = ""
                    txtaccsetdes.Text = ""
                End If
                fndTrmsCode.Value = dr.Rows(0)("Terms_COde").ToString()
                txttermcodedes.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Terms_Desc from TSPL_TERMS_MASTER where Terms_Code='" + clsCommon.myCstr(fndTrmsCode.Value) + "'"))
                If clsCommon.myLen(txttermcodedes.Text) <= 0 Then
                    fndTrmsCode.Value = ""
                    txttermcodedes.Text = ""
                End If
                fndbankcode.Text = dr.Rows(0)("Bank_Code").ToString()
                txtbankcodedes.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select description from tspl_bank_master where bank_code='" + fndbankcode.Text + "'"))
                'If clsCommon.myLen(txtbankcodedes.Text) <= 0 Then
                '    fndbankcode.Value = ""
                '    txtbankcodedes.Text = ""
                'End If
                fndPayCode.Value = dr.Rows(0)("payment_code").ToString()
                txtpaymentcodedes.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select payment_desc from tspl_payment_code where payment_code='" + clsCommon.myCstr(fndPayCode.Value) + "'"))
                If clsCommon.myLen(txtpaymentcodedes.Text) <= 0 Then
                    fndPayCode.Value = ""
                    txtpaymentcodedes.Text = ""
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
            ' Dim strvalue As String
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
            ' Dim strvalue As String
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
            'Dim strvalue As String
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

            Dim strquery As String = "select bank_code As [Bank Code],description  as [Description]from TSPL_Bank_MASTER where bank_code='" + fndbankcode.Text + "'"
            Dim dr As DataTable
            'Dim strvalue As String
            dr = clsDBFuncationality.GetDataTable(strquery)
            If dr.Rows IsNot Nothing AndAlso dr.Rows.Count > 0 Then
                txtbankcodedes.Text = dr.Rows(0)("Description").ToString()
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub
    Public Sub funfillbranch()
        Try
            If clsCommon.myLen(fndbankcode.Text) > 0 Then
                TxtBankBranch.Text = clsDBFuncationality.getSingleValue("Select Branch_Name from TSPL_Vendor_Bank_Branch_Details where Bank_Code ='" & fndbankcode.Text & "' and Bank_IFSC_Code='" & TxtIFSCCode.Text & "' ")
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

            strcmd = " SELECT TSPL_TAX_GROUP_DETAILS.Tax_Code as [Tax] FROM TSPL_TAX_GROUP_MASTER INNER JOIN TSPL_TAX_GROUP_DETAILS ON TSPL_TAX_GROUP_MASTER.Tax_Group_Code = TSPL_TAX_GROUP_DETAILS.Tax_Group_Code where  TSPL_TAX_GROUP_DETAILS.Tax_Group_Type='P' and TSPL_TAX_GROUP_MASTER.Tax_Group_Type='P' and  TSPL_TAX_GROUP_MASTER.Tax_Group_Code='" + fndTxGrp.Value + "' ORDER BY Trans_Code "
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


    Sub LoadBlankBankGuaranteeGrid()
        Try
            gvBankG.Rows.Clear()
            gvBankG.Columns.Clear()
            gvBankG.Columns.Add("COLBankNO", "Bank Guarantee NO")
            gvBankG.Columns.Add("COLBankDate", "Date")
            gvBankG.Columns.Add("COLDesc", "Description")
            gvBankG.Columns.Add("COLGuaranteeType", "Bank Guarantee Type")
            gvBankG.Columns.Add("COLStartDate", "Start Date")
            gvBankG.Columns.Add("COLEndDate", "End Date")
            gvBankG.Columns.Add("COLExtEndDate", "Extended End Date")
            gvBankG.Columns.Add("COLAmount", "Amount")
            gvBankG.Columns.Add("COLremarks", "Remarks")


            gvBankG.Columns("COLBankNO").Width = 100
            gvBankG.Columns("COLBankDate").Width = 100
            gvBankG.Columns("COLDesc").Width = 100
            gvBankG.Columns("COLStartDate").Width = 100
            gvBankG.Columns("COLEndDate").Width = 100
            gvBankG.Columns("COLExtEndDate").Width = 100
            gvBankG.Columns("COLAmount").Width = 100
            gvBankG.Columns("COLremarks").Width = 100
            gvBankG.Columns("COLGuaranteeType").Width = 100

            gvBankG.AllowAddNewRow = False
            gvBankG.AllowEditRow = False
            gvBankG.AllowDeleteRow = False
            gvBankG.AllowRowResize = False
            gvBankG.AllowRowReorder = False
            gvBankG.AllowColumnResize = False
            gvBankG.AllowColumnChooser = False
            gvBankG.AllowAutoSizeColumns = True
            gvBankG.ShowGroupPanel = False
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    'Thid funtion will fill all the fields on selecting the value fronm finder
    Public Sub funfill()
        Try
            TxtSecurityAmount.Text = 0
            txtSecurityDeductedAmount.Text = 0
            TxtBankRefundedAmount.Text = 0
            TxtGuranteeAmount.Text = 0
            grdTax.Rows.Clear()
            Dim myDs As DataSet
            myDs = connectSql.RunSQLReturnDS(clsVendorMaster.getQueryForVSPMaster(fndvendorNo.Value))
            Dim myDr As DataRow
            For Each myDr In myDs.Tables(0).Rows
                chk_isblacklist.Checked = IIf(clsCommon.myCstr(myDr("Is_Blacklist").ToString()) = "1", True, False)
                chkMultIncentive.Checked = IIf(clsCommon.myCdbl(myDr("Apply_Mult_Incentive")) > 0, True, False)
                LoadIncentive(fndvendorNo.Value, Nothing)
                Me.txtvendorname.Text = myDr(0).ToString()
                Me.fndgroupcode.Value = myDr(1).ToString()
                Me.txtgroupdes.Text = myDr(2).ToString()

                '==================================================
                Dim strRegistered_PDCS_CLUSTER As String = clsCommon.myCstr(myDr("Registered_PDCS_CLUSTER"))
                If clsCommon.CompairString(strRegistered_PDCS_CLUSTER, "Registered") = CompairStringResult.Equal Then
                    chkRegistered.Checked = True
                ElseIf clsCommon.CompairString(strRegistered_PDCS_CLUSTER, "PDCS") = CompairStringResult.Equal Then
                    chkPDCS.Checked = True
                ElseIf clsCommon.CompairString(strRegistered_PDCS_CLUSTER, "CLUSTER") = CompairStringResult.Equal Then
                    chkCLUSTER.Checked = True
                Else
                    chkRegistered.Checked = False
                    chkPDCS.Checked = False
                    chkCLUSTER.Checked = False
                End If
                If clsCommon.myLen(clsCommon.myCstr(myDr("StartDate"))) > 0 Then
                    txtStartDate.Value = clsCommon.myCDate((myDr("StartDate")))
                Else
                    txtStartDate.Value = Nothing
                End If
                chkOwnBMC.Checked = clsfrmVLCMaster.IsOwnBMCByVSPCode(fndvendorNo.Value, Nothing) 'clsCommon.myCBool(clsCommon.myCdbl(myDr("isOwnBMC")))
                If chkOwnBMC.Checked = True Then
                    txtMCCOwnBMC.Value = clsfrmVLCMaster.OwnBMCCode(fndvendorNo.Value, Nothing) 'clsCommon.myCstr(myDr("MCCOwnBMC"))
                    lblMCCOwnBMC.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select MCC_NAME from tspl_MCC_MASTER where MCC_Code = '" + txtMCCOwnBMC.Value + "'"))
                End If
                txtvendornameHindi.Text = clsCommon.myCstr(myDr("Vendor_name_Hindi"))
                findfndbankcode2.Value = clsCommon.myCstr(myDr("BankCode2"))
                fndbankcode2.Text = clsCommon.myCstr(myDr("BankCode2"))
                'txtbankcodedes2.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select MCC_NAME from tspl_MCC_MASTER where MCC_Code = '" + txtMCCOwnBMC.Value + "'"))
                txtCredit2.Text = clsCommon.myCdbl(myDr("Credit2"))
                txtIFSCCode2.Text = clsCommon.myCstr(myDr("IFSCCode2"))
                TxtAccNo2.Text = clsCommon.myCstr(myDr("AccNo2"))
                cmbAccountType2.Text = clsCommon.myCstr(myDr("AccountType2"))
                TxtBankName2.Text = clsCommon.myCstr(myDr("BankBranch2"))
                TxtSecurityCharges2.Text = clsCommon.myCstr(myDr("SecurityCharges2"))
                findTxtIFSCCode2.Value = clsCommon.myCstr(myDr("IFSCCode2"))
                txtBankBranch2.Text = clsCommon.myCstr(myDr("BankBranch2"))
                txtSupervisiorRP.Value = clsCommon.myCstr(myDr("SupervisorOrRP"))
                lblSupervisiorRPName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select Emp_Name from TSPL_EMPLOYEE_MASTER where EMP_CODE = '" + txtSupervisiorRP.Value + "' "))
                '==================================================
                txtCompanyBank.Value = clsCommon.myCstr(myDr("Company_Bank"))
                lblCompanyBank.Text = clsBankMaster.GetName(clsCommon.myCstr(myDr("Company_Bank")))
                '=============Added By Rohit================
                txtMonthlyRent.Value = clsCommon.myCdbl(myDr("Monthly_Rent")) ''ERO/23/05/19-000612 by balwinder on 24/05/2019
                LoadCharges()
                UcAttachment1.LoadData(fndvendorNo.Value)
                LoadVisiDetail()
                LoadBlankBankGuaranteeGrid()
                LoadBlankPaymentEntry()
                Dim objB As List(Of clsBankGuaranteeMaster) = clsMccMaster.GetDataBankGuarantee(fndvendorNo.Value, NavigatorType.Current)
                If Not IsNothing(objB) Then
                    TxtGuranteeAmount.Text = 0
                    For i As Integer = 0 To objB.Count - 1
                        gvBankG.Rows.Add(objB.Item(i).code, objB.Item(i).docdate, objB.Item(i).desc, objB.Item(i).Bank_Guarantee_Type, objB.Item(i).strtdate, objB.Item(i).enddate, objB.Item(i).extnddate, objB.Item(i).amount, objB.Item(i).remarks)
                        If clsCommon.CompairString(objB.Item(i).Bank_Guarantee_Type, "Receiving") = CompairStringResult.Equal Then
                            TxtGuranteeAmount.Text += clsCommon.myCdbl(objB.Item(i).amount)
                        Else
                            TxtBankRefundedAmount.Text += clsCommon.myCdbl(objB.Item(i).amount)
                        End If

                    Next
                    gvBankG.BestFitColumns()
                End If

                TxtCreditLimitBasedOnMilkReceipt.Value = clsCommon.myCdbl(myDr("Credit_Limit_On_Milk_Receipt_Per"))
                If TxtCreditLimitBasedOnMilkReceipt.Value > 0 Then
                    chkCreditLimitBasedOnMilkReceipt.Checked = True
                Else
                    chkCreditLimitBasedOnMilkReceipt.Checked = False
                    TxtCreditLimitBasedOnMilkReceipt.Value = Nothing
                End If
                chkbuyerfilereturnlasttwoyear.Checked = IIf(clsCommon.myCstr(myDr("Isbuyerfilereturninlasttwoyears").ToString()) = "1", True, False)
                chkTCSTDSamountgreater50KpreviousYear.Checked = IIf(clsCommon.myCstr(myDr("IsTCS_TDSamountgreaterthan50KpreviousYear").ToString()) = "1", True, False)

                ChkHeadLoad.Checked = IIf(clsCommon.myCstr(myDr("Is_Head_Load")) = "T", True, False)
                chkHoldPaymentProcess.Checked = clsCommon.myCdbl(myDr("is_Hold_Payment_Process")) = 1
                chkInactiveInMilkModule.Checked = clsCommon.myCdbl(myDr("Is_Inactive_In_Milk_Procurement")) = 1

                ChkOwnAsset.Checked = IIf(clsCommon.myCstr(myDr("Is_Own_Asset")) = "T", True, False)
                txtRateHeadLoad.Text = clsCommon.myCdbl(myDr("Rate_Head_Load"))
                txtDistanceKMHeadLoad.value = clsCommon.myCdbl(myDr("DistanceKM_Head_Load"))
                ChkIsMP.Checked = IIf(clsCommon.myLen(clsCommon.myCstr(myDr("MP_code"))) > 0, True, False)
                FndMPCode.Value = clsCommon.myCstr(myDr("MP_code"))
                LblMPName.Text = clsCommon.myCstr(myDr("MP_name"))
                TxtPinCode.Text = clsCommon.myCstr(myDr("Pin_Code"))
                TxtStandardSec_Amt.Text = clsCommon.myCdbl(myDr("Standard_Security_Amount"))
                CmbHeadLoadServiceBasis.SelectedValue = clsCommon.myCstr(myDr("Service_Basis_Head_Load"))
                TxtRateOwnAsset.Text = clsCommon.myCdbl(myDr("Rate_Own_Asset"))
                CmbOwnAssetServiceBasis.SelectedValue = clsCommon.myCstr(myDr("Service_Basis_Own_Asset"))

                txtCareOf.Text = clsCommon.myCstr(myDr("Care_Of"))
                txtAadharNo.Text = clsCommon.myCstr(myDr("Aadhar_No"))
                txtSecChequeLac1.Text = clsCommon.myCstr(myDr("SecChequeNoLac1"))
                txtSecChequeRs100.Text = clsCommon.myCstr(myDr("SecChequeNoRs100"))
                If EnableBankFromMaster = True Then
                    txtBankCode.Value = clsCommon.myCstr(myDr("Joint_Bank_Code"))
                    txtBankBranchCode.Value = clsCommon.myCstr(myDr("Joint_Branch_Name"))
                Else
                    texttxtBankCode.Text = clsCommon.myCstr(myDr("Joint_Bank_Code"))
                    texttxtBankBranchCode.Text = clsCommon.myCstr(myDr("Joint_Branch_Name"))
                End If
                GetJointBankDetails(False)
                txtIFCICode.Text = clsCommon.myCstr(myDr("Joint_IFSC_Code"))
                txtAccountNo.Text = clsCommon.myCstr(myDr("Joint_Account_no"))
                If clsCommon.myLen(myDr("End_Date")) > 0 Then
                    txtexpir_date.Value = clsCommon.myCstr(myDr("End_Date"))
                End If
                If clsCommon.myLen(myDr("Start_Date")) > 0 Then
                    txtagrmnt_date.Value = clsCommon.myCstr(myDr("Start_Date"))
                End If

                cmbagreemnt.SelectedValue = clsCommon.myCstr(myDr("Agreement"))
                fndpaymentCycle.Value = clsCommon.myCstr(myDr("PC_COde"))
                GetPaymentCycleData(False)

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
                End If


                '==========================Payment Details=============================================
                Dim objPayment_Detail_MCC As List(Of clsPayment_Detail_MCC)
                objPayment_Detail_MCC = clsPayment_Detail_MCC.GetPaymentData(fndvendorNo.Value)
                If objPayment_Detail_MCC.Count > 0 Then

                    For i As Integer = 0 To objPayment_Detail_MCC.Count - 1
                        GVPaymentEntry.Rows.Add(objPayment_Detail_MCC.Item(i).Payment_No, objPayment_Detail_MCC.Item(i).Payment_Date, objPayment_Detail_MCC.Item(i).Description, objPayment_Detail_MCC.Item(i).Bank_Name, objPayment_Detail_MCC.Item(i).Payment_Type, objPayment_Detail_MCC.Item(i).Bank_Charges, objPayment_Detail_MCC.Item(i).Vendor_Code, objPayment_Detail_MCC.Item(i).Vendor_Name, objPayment_Detail_MCC.Item(i).Cheque_No, objPayment_Detail_MCC.Item(i).Cheque_Date, objPayment_Detail_MCC.Item(i).Payment_Mode, objPayment_Detail_MCC.Item(i).Payment_Amount)
                        If clsCommon.myCdbl(objPayment_Detail_MCC.Item(i).Payment_Amount) >= 0 Then
                            TxtSecurityAmount.Text += clsCommon.myCdbl(objPayment_Detail_MCC.Item(i).Payment_Amount)
                        Else
                            txtSecurityDeductedAmount.Text -= clsCommon.myCdbl(objPayment_Detail_MCC.Item(i).Payment_Amount)
                        End If

                    Next
                    GVPaymentEntry.BestFitColumns()
                End If
                '=======================================================================================
                GettotalAmount()
                '==============================================
                Dim strStatus As String = myDr(3).ToString()
                If strStatus = "N" Then
                    chkInActive.Checked = False
                ElseIf strStatus = "Y" Then
                    chkInActive.Checked = True
                End If

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

                If clsCommon.CompairString(clsCommon.myCstr(myDr("is_drip_saver")), "Y") = CompairStringResult.Equal Then
                    ChkIsDripSaver.Checked = True
                Else
                    ChkIsDripSaver.Checked = False
                End If

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
                Me.txtState.Text = myDr(11).ToString()
                Me.txtCountry.Text = myDr(12).ToString()

                '----------------------------------------------------Monika 22/05/2014-----------------------
                txtcountrycode.Value = clsCommon.myCstr(myDr("country_code"))
                txtstatecode.Value = clsCommon.myCstr(myDr("state_code"))
                '---------------------------------for old data getting code'-----------------------------

                If objCommonVar.GSTApplicable Then
                    Dim check As String = clsDBFuncationality.getSingleValue("select case when isnull(GST_State_Code,'')='' then '' else GST_State_Code end as GST_State_Code from tspl_state_master where state_code='" & txtstatecode.Value & "'")
                    If clsCommon.myLen(check) > 0 Then
                        txtGSTStateCode.Text = check
                    End If

                    txtEntity.Text = clsCommon.myCstr(myDr("GSTEntity"))
                    MyTextBox2.Text = clsCommon.myCstr(myDr("GSTLastEntity"))
                    Rchkregistered.Checked = IIf(clsCommon.myCstr(myDr("GSTRegistered").ToString()) = "1", True, False)
                    txtGSTIN_No_final.Text = clsCommon.myCstr(myDr("GSTFinalNo"))
                End If

                numCorrectionFat.Text = clsCommon.myCdbl(myDr("CorrectionFat"))
                numCorrectionSNF.Text = clsCommon.myCdbl(myDr("CorrectionSNF"))

                If clsCommon.myLen(txtState.Text) > 0 AndAlso clsCommon.myLen(txtstatecode.Value) <= 0 Then
                    Dim qry As String = "select state_code from tspl_state_master where state_name like '%" + txtState.Text + "%'"
                    txtstatecode.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
                End If

                If clsCommon.myLen(txtCountry.Text) > 0 AndAlso clsCommon.myLen(txtcountrycode.Value) <= 0 Then
                    Dim qry As String = "select country_code from tspl_country_master where country_name like '%" + txtCountry.Text + "%'"
                    txtcountrycode.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
                End If



                'CmbNature.SelectedValue = clsCommon.myCstr(myDr("Nature"))
                txtcommpers.Text = clsCommon.myCstr(myDr("commision_pers"))
                txtpaymnt_cmsn.Text = clsCommon.myCstr(myDr("payment_commision_pers"))
                'cmbincentive.SelectedValue = clsCommon.myCstr(myDr("incentive"))
                FndIncentive.Value = clsCommon.myCstr(myDr("incentive"))
                GetIIncentiveDetails(False)
                txtno_days.Text = clsCommon.myCstr(myDr("incentive_days"))
                cmbvsppayment.SelectedValue = clsCommon.myCstr(myDr("vsp_payment"))
                txtpayeename.Text = clsCommon.myCstr(myDr("vsp_payee_name"))
                cmbservc_type.Text = clsCommon.myCstr(myDr("Service_Charge_Type"))
                '====shivani
                txtChequeInFavour.Text = clsCommon.myCstr(myDr("Cheque_In_Favour_Of"))
                '====
                txtjointname.Text = clsCommon.myCstr(myDr("Joint_Name")) 'fndVendorCOde.Value = clsCommon.myCstr(myDr("Joint_Name"))
                TxtSecurityCharges.Text = clsCommon.myCdbl(myDr("Security_Amount"))
                If clsCommon.myLen(myDr("Billing_Date")) > 0 Then
                    DtpBillingDate.Value = clsCommon.myCDate(myDr("Billing_Date"))
                    DtpEndBillingDate.Value = DtpBillingDate.Value.AddDays(2)
                End If

                TxtAmCU.Text = clsCommon.myCstr(myDr("AMCU"))
                TxtAmc_Charge.Text = clsCommon.myCdbl(myDr("AMC_Charge"))
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
                'Me.fndbankcode.Text = myDr(31).ToString()
                'GetBankDetails(False)
                'funfillbank()
                'funfillbranch()
                Me.txtbankcodedes.Text = myDr(32).ToString()
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
                chkIsGrossReceipt.Checked = IIf(clsCommon.myCdbl(myDr("is_Gross_Receipt")) = 1, True, False)
                txtVSPSecurityDeduction.Value = clsCommon.myCdbl(myDr("Security_Deduction_Amount"))
                txtVSPInterestPer.Value = clsCommon.myCdbl(myDr("Interest_Per"))
                txtVSPMinimumInterest.Value = clsCommon.myCdbl(myDr("Minimum_Interest"))
                txtServiceCharge.Value = clsCommon.myCdbl(myDr("Service_Charge_Per_Unit"))
                ''For Custom Fields
                If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
                    UcCustomFields1.BlankAllControls()
                    UcCustomFields1.LoadData(fndvendorNo.Value)
                End If
                ''End of For Custom Fields


                '' multicurrency
                Me.fndVendorCurrency.Value = myDr("currency_code").ToString()
                '' end multicurrency

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
                Me.txtpan.Text = myDr(69).ToString()

                If objCommonVar.GSTApplicable Then
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

                txtTxGrp.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select tax_code_desc from tspl_tax_master where tax_code='" + clsCommon.myCstr(fndTxGrp.Value) + "'"))
                txtaccsetdes.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select acct_set_desc from TSPL_VENDOR_ACCOUNT_SET where acct_set_code='" + clsCommon.myCstr(fndAccntSet.Value) + "'"))
                txttermcodedes.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Terms_Desc from TSPL_TERMS_MASTER where Terms_Code='" + clsCommon.myCstr(fndTrmsCode.Value) + "'"))
                txtpaymentcodedes.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select payment_desc from tspl_payment_code where payment_code='" + clsCommon.myCstr(fndPayCode.Value) + "'"))
                ' txtjointname.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select vendor_name from tspl_vendor_master where vendor_code='" + clsCommon.myCstr(fndVendorCOde.Value) + "'"))

                '' Anubhooti 1-Aug-2014 BM00000003318
                Me.TxtBankName.Text = myDr("Bank_Name").ToString()
                Me.TxtBankBranch.Text = myDr("Branch_Name").ToString()
                Me.TxtAccNo.Text = myDr("Account_No").ToString()
                'Me.TxtIFSCCode.Text = myDr("IFSC_Code").ToString()

                If EnableBankFromMaster = True Then
                    findfndbankcode.Value = myDr(31).ToString()
                    findTxtIFSCCode.Value = myDr("IFSC_Code").ToString()
                Else
                    Me.fndbankcode.Text = myDr(31).ToString()
                    Me.TxtIFSCCode.Text = myDr("IFSC_Code").ToString()
                End If

                If clsCommon.myLen(myDr("Account_Type")) > 0 Then
                    Me.cmbAccountType.SelectedValue = clsCommon.myCstr(myDr("Account_Type"))
                Else
                    Me.cmbAccountType.SelectedValue = clsCommon.myCstr("Cur")
                End If
                ''
                cboEMPType.SelectedValue = clsCommon.myCstr(myDr("EMP_Type"))
                txtFixedAmount.Value = clsCommon.myCdbl(myDr("EMP_Fixed_Amount"))
                txtHandlingChargesPer.Value = clsCommon.myCdbl(myDr("Handling_Charges_Per"))
                loadBlankGridEMP()
                gvEMP.Rows(0).Cells(colEMPSno).Value = 1
                gvEMP.Rows(0).Cells(colEMPSlab).Value = clsCommon.myCdbl(myDr("Actual_charges_Slab"))
                gvEMP.Rows(0).Cells(colEMPValue).Value = clsCommon.myCdbl(myDr("Actual_charges"))
                gvEMP.Rows(1).Cells(colEMPSno).Value = 2
                gvEMP.Rows(1).Cells(colEMPSlab).Value = clsCommon.myCdbl(myDr("Actual_charges_Slab2"))
                gvEMP.Rows(1).Cells(colEMPValue).Value = clsCommon.myCdbl(myDr("Actual_charges2"))
                gvEMP.Rows(2).Cells(colEMPSno).Value = 3
                gvEMP.Rows(2).Cells(colEMPSlab).Value = clsCommon.myCdbl(myDr("Actual_charges_Slab3"))
                gvEMP.Rows(2).Cells(colEMPValue).Value = clsCommon.myCdbl(myDr("Actual_charges3"))
                gvEMP.Rows(3).Cells(colEMPSno).Value = 4
                gvEMP.Rows(3).Cells(colEMPSlab).Value = clsCommon.myCdbl(myDr("Actual_charges_Slab4"))
                gvEMP.Rows(3).Cells(colEMPValue).Value = clsCommon.myCdbl(myDr("Actual_charges4"))
                gvEMP.Rows(4).Cells(colEMPSno).Value = 5
                gvEMP.Rows(4).Cells(colEMPSlab).Value = clsCommon.myCdbl(myDr("Actual_charges_Slab5"))
                gvEMP.Rows(4).Cells(colEMPValue).Value = clsCommon.myCdbl(myDr("Actual_charges5"))
                txtTIPBuffalo.Value = clsCommon.myCdbl(myDr("TIP_Buffalo"))
                txtTIPCow.Value = clsCommon.myCdbl(myDr("TIP_Cow"))
                txtTIPMix.Value = clsCommon.myCdbl(myDr("TIP_Mix"))
                'Sanjay
                Dim TempCust_Group_Code As String = clsDBFuncationality.getSingleValue("select isnull(Cust_Group_Code,'') as Cust_Group_Code from tspl_customer_master where CUSTOMER_FORM_TYPE='VSP' AND cust_code='" + fndvendorNo.Value + "'")
                If clsCommon.myLen(TempCust_Group_Code) > 0 Then
                    chkCreateCustomerAlso.Checked = True
                    fndCusgrp.Value = clsCommon.myCstr(TempCust_Group_Code)
                Else
                    chkCreateCustomerAlso.Checked = False
                    fndCusgrp.Value = Nothing
                End If
                'Sanjay

                If clsCommon.CompairString(clsCommon.myCstr(myDr(39)), "") <> CompairStringResult.Equal Then
                    If grdTax.Rows.Count < 1 Then
                        grdTax.Rows.AddNew()
                    End If

                    Me.grdTax.Rows(0).Cells(0).Value = myDr(39).ToString
                    Me.grdTax.Rows(0).Cells(1).Value = myDr(40).ToString
                Else
                    Return
                End If

                If clsCommon.CompairString(myDr(41), "") <> CompairStringResult.Equal Then
                    If grdTax.Rows.Count <= 2 Then
                        grdTax.Rows.AddNew()
                    End If
                    Me.grdTax.Rows(1).Cells(0).Value = myDr(41).ToString
                    Me.grdTax.Rows(1).Cells(1).Value = myDr(42).ToString
                Else
                    Return
                End If

                If clsCommon.CompairString(myDr(43), "") <> CompairStringResult.Equal Then
                    If grdTax.Rows.Count <= 3 Then
                        grdTax.Rows.AddNew()
                    End If
                    Me.grdTax.Rows(2).Cells(0).Value = myDr(43).ToString
                    Me.grdTax.Rows(2).Cells(1).Value = myDr(44).ToString
                Else
                    Return
                End If

                If clsCommon.CompairString(myDr(45), "") <> CompairStringResult.Equal Then
                    If grdTax.Rows.Count <= 4 Then
                        grdTax.Rows.AddNew()
                    End If
                    Me.grdTax.Rows(3).Cells(0).Value = myDr(45).ToString
                    Me.grdTax.Rows(3).Cells(1).Value = myDr(46).ToString
                Else
                    Return
                End If

                If clsCommon.CompairString(myDr(47), "") <> CompairStringResult.Equal Then
                    If grdTax.Rows.Count <= 5 Then
                        grdTax.Rows.AddNew()
                    End If
                    Me.grdTax.Rows(4).Cells(0).Value = myDr(47).ToString
                    Me.grdTax.Rows(4).Cells(1).Value = myDr(48).ToString
                Else
                    Return
                End If

                If clsCommon.CompairString(myDr(49), "") <> CompairStringResult.Equal Then
                    If grdTax.Rows.Count <= 6 Then
                        grdTax.Rows.AddNew()
                    End If
                    Me.grdTax.Rows(5).Cells(0).Value = myDr(49).ToString
                    Me.grdTax.Rows(5).Cells(1).Value = myDr(50).ToString
                Else
                    Return
                End If

                If clsCommon.CompairString(myDr(51), "") <> CompairStringResult.Equal Then
                    If grdTax.Rows.Count <= 7 Then
                        grdTax.Rows.AddNew()
                    End If
                    Me.grdTax.Rows(6).Cells(0).Value = myDr(51).ToString
                    Me.grdTax.Rows(6).Cells(1).Value = myDr(52).ToString
                Else
                    Return
                End If

                If clsCommon.CompairString(myDr(53), "") <> CompairStringResult.Equal Then
                    If grdTax.Rows.Count <= 8 Then
                        grdTax.Rows.AddNew()
                    End If
                    Me.grdTax.Rows(7).Cells(0).Value = myDr(53).ToString
                    Me.grdTax.Rows(7).Cells(1).Value = myDr(54).ToString
                Else
                    Return
                End If

                If clsCommon.CompairString(myDr(55), "") <> CompairStringResult.Equal Then
                    If grdTax.Rows.Count <= 9 Then
                        grdTax.Rows.AddNew()
                    End If
                    Me.grdTax.Rows(8).Cells(0).Value = myDr(55).ToString
                    Me.grdTax.Rows(8).Cells(1).Value = myDr(56).ToString
                Else
                    Return
                End If

                If clsCommon.CompairString(myDr(57), "") <> CompairStringResult.Equal Then
                    If grdTax.Rows.Count <= 10 Then
                        grdTax.Rows.AddNew()
                    End If
                    Me.grdTax.Rows(9).Cells(0).Value = myDr(57).ToString()
                    Me.grdTax.Rows(9).Cells(1).Value = myDr(58).ToString
                Else
                    Return
                End If
               
            Next
            'btnsave.Text = "Update"
            'btnsave.Enabled = True
            'btndelete.Enabled = True

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

    Sub LoadBlankPaymentEntry()
        Try
            GVPaymentEntry.Rows.Clear()
            GVPaymentEntry.Columns.Clear()
            GVPaymentEntry.Columns.Add("COLPaymentNO", "Payment NO")
            GVPaymentEntry.Columns.Add("COLPaymentDate", "Date")
            GVPaymentEntry.Columns.Add("COLDesc", "Description")
            GVPaymentEntry.Columns.Add("COLBankName", "Bank Name")
            GVPaymentEntry.Columns.Add("COLPaymentType", "Payment Type")
            GVPaymentEntry.Columns.Add("COLBankCharges", "Bank Charges")
            GVPaymentEntry.Columns.Add("COLVendorCode", "Vendor Code")
            GVPaymentEntry.Columns.Add("COLVendorName", "Vendor Name")
            GVPaymentEntry.Columns.Add("COLChequeNo", "Cheque No")
            GVPaymentEntry.Columns.Add("COLChequeDate", "Cheque Date")
            GVPaymentEntry.Columns.Add("COLpaymentMode", "Payment Mode")
            GVPaymentEntry.Columns.Add("COLPaymentAmount", "Payment Amount")


            GVPaymentEntry.Columns("COLPaymentNO").Width = 100
            GVPaymentEntry.Columns("COLPaymentDate").Width = 100
            GVPaymentEntry.Columns("COLDesc").Width = 100
            GVPaymentEntry.Columns("COLBankName").Width = 100
            GVPaymentEntry.Columns("COLPaymentType").Width = 100
            GVPaymentEntry.Columns("COLBankCharges").Width = 100
            GVPaymentEntry.Columns("COLVendorCode").Width = 100
            GVPaymentEntry.Columns("COLVendorName").Width = 100
            GVPaymentEntry.Columns("COLChequeNo").Width = 100
            GVPaymentEntry.Columns("COLChequeDate").Width = 100
            GVPaymentEntry.Columns("COLpaymentMode").Width = 80
            GVPaymentEntry.Columns("COLPaymentAmount").Width = 100

            GVPaymentEntry.AllowAddNewRow = False
            GVPaymentEntry.AllowEditRow = False
            GVPaymentEntry.AllowDeleteRow = False
            GVPaymentEntry.AllowRowResize = False
            GVPaymentEntry.AllowRowReorder = False
            GVPaymentEntry.AllowColumnResize = False
            GVPaymentEntry.AllowColumnChooser = False
            GVPaymentEntry.AllowAutoSizeColumns = True
            GVPaymentEntry.ShowGroupPanel = False
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Sub GenerateVoucherNo(ByVal trans As SqlTransaction)

        Try
            fndvendorNo.Value = clsERPFuncationality.GetNextCode(trans, clsCommon.GETSERVERDATE(trans), clsDocType.VSPMASTER, clsDocTransactionType.Registered, "")
            'trans.Commit()
        Catch ex As Exception
            'trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Sub
    'For inserting the data in the database
    Public Sub funinsert()

        If chkCreateCustomerAlso.Checked = True Then
            If fndCusgrp.Value = "" Then
                myMessages.blankValue("Please Select Customer Group Code")
                pageCus.SelectedPage = RadPageViewPage1
                fndCusgrp.Focus()
                fndCusgrp.Select()
                Return
            End If
        End If
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If AllowVSPMasterAutoPrefix = 1 Then
                GenerateVoucherNo(trans)
            End If
            Dim Registered As Integer = 0
            If objCommonVar.GSTApplicable Then
                If Rchkregistered.Checked = True Then
                    Registered = 1
                Else
                    Registered = 0
                    txtGST_PanCode.Text = ""
                    txtEntity.Text = ""
                    txtGSTStateCode.Text = ""
                    txtGSTIN_No_final.Text = ""
                End If

            End If


            Dim strStatus As String = ""
            If chkInActive.Checked = True Then
                strStatus = "Y"                    '******* for:In-Active ********
            ElseIf chkInActive.Checked = False Then
                strStatus = "N"                    '******* for:Active ******** 
            End If

            Dim strInterBranch As String = ""
            If chkInterBranch.Checked = True Then
                strInterBranch = "Y"
            Else
                strInterBranch = "N"
            End If

            Dim strTagAsFranchise As String
            If chkTagAsFranchise.Checked = True Then
                strTagAsFranchise = "Y"  '''''   for taging vendor as franchise
            Else
                strTagAsFranchise = "N" '''''   for untaging vendor as franchise
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
            'Dim OutComm As Decimal

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

            Dim srvc_type As String = clsCommon.myCstr(cmbservc_type.Text)
            Dim joint_name As String = clsCommon.myCstr(txtjointname.Text.Replace("'", "`")) 'clsCommon.myCstr(fndVendorCOde.Value)


            Dim IsGrossReceipt As Integer = IIf(chkIsGrossReceipt.Checked, 1, 0)
            connectSql.RunSpTransaction(trans, "sp_TSPL_VENDOR_MASTER_INSERT", New SqlParameter("@Vendor_Code", fndvendorNo.Value), New SqlParameter("@Vendor_Name", txtvendorname.Text.ToString()), New SqlParameter("@Vendor_Group_Code", fndgroupcode.Value), New SqlParameter("Vendor_Group_Des", txtgroupdes.Text.ToString()), New SqlParameter("@Status", strStatus), New SqlParameter("@OnHold", strHold), New SqlParameter("@transporter", strtrans), New SqlParameter("@Closing_Date", Format(Me.dtClosing.Value, "dd/MM/yyyy")), New SqlParameter("@Add1", txtAdd1.Text.ToString()), New SqlParameter("@Add2", txtAdd2.Text.ToString()), New SqlParameter("@Add3", txtAdd3.Text.ToString()), New SqlParameter("@City_Code", fndCity.Value), New SqlParameter("@City_Des", txtCity.Text.ToString()), New SqlParameter("@State", txtState.Text.ToString()), New SqlParameter("@Country", txtCountry.Text.ToString()), New SqlParameter("@Phone1", txtPhone1.Text.ToString()), New SqlParameter("@Phone2", txtPhone2.Text.ToString()), New SqlParameter("@Fax", txtfax.Text.ToString()), New SqlParameter("@Email", txtEmail.Text.ToString()), New SqlParameter("@WebSite", txtWeb.Text.ToString()), New SqlParameter("@Contact_Person_Name", txtContactName.Text.ToString()), New SqlParameter("@Contact_Person_Phone", txtContPhone.Text.ToString()), New SqlParameter("@Contact_Person_Fax", txtContactFax.Text.ToString()), New SqlParameter("@Contact_Person_Website", txtContactWeb.Text.ToString()), New SqlParameter("@Contact_Person_Email", txtContactEmail.Text.ToString()), New SqlParameter("@Terms_Code", fndTrmsCode.Value), New SqlParameter("@Terms_Code_Des", txttermcodedes.Text.ToString()), New SqlParameter("@Vendor_Account", fndAccntSet.Value), New SqlParameter("@Vendor_Account_Set_Des", Me.fndAccntSet.Value), New SqlParameter("@Payment_Code", fndPayCode.Value), New SqlParameter("@Payment_Code_Des", txtpaymentcodedes.Text.ToString()), New SqlParameter("@Vendor_Type_Code", fndvendortype.Value), New SqlParameter("@Vendor_Type_Des", txtvendortypedes.Text.ToString()), New SqlParameter("@Bank_Code", IIf(EnableBankFromMaster = True, findfndbankcode.Value, fndbankcode.Text)), New SqlParameter("@Bank_Code_Des", txtbankcodedes.Text.ToString()), New SqlParameter("@Service_Tax_No", Me.txtStaxNo.Text), New SqlParameter("@Lst_No", Me.txtLstNo.Text), New SqlParameter("@Tin_No", tin_no), New SqlParameter("@Credit_Limit", CrLimit), New SqlParameter("@Tax_Group", Me.fndTxGrp.Value), New SqlParameter("@Tax_Group_Des", txtTxGrp.Text.ToString()), New SqlParameter("@TAX1", strTax1), New SqlParameter("@TAX1_Rate", strTax1_Rate), New SqlParameter("@TAX2", strTax2), New SqlParameter("@TAX2_Rate", strTax2_Rate), New SqlParameter("@TAX3", strTax3), New SqlParameter("@TAX3_Rate", strTax3_Rate), New SqlParameter("@TAX4", strTax4), New SqlParameter("@TAX4_Rate", strTax4_Rate), New SqlParameter("@TAX5", strTax5), New SqlParameter("@TAX5_Rate", strTax5_Rate), New SqlParameter("@TAX6", strTax6), New SqlParameter("@TAX6_Rate", strTax6_Rate), New SqlParameter("@TAX7", strTax7), New SqlParameter("@TAX7_Rate", strTax7_Rate), New SqlParameter("@TAX8", strTax8), New SqlParameter("@TAX8_Rate", strTax8_Rate), New SqlParameter("@TAX9", strTax9), New SqlParameter("@TAX9_Rate", strTax9_Rate), New SqlParameter("@TAX10", strTax10), New SqlParameter("@TAX10_Rate", strTax10_Rate), New SqlParameter("@Remarks1", txtRemarks1.Text.ToString()), New SqlParameter("@Remarks2", txtRemarks2.Text.ToString()), New SqlParameter("@Additional1", txtAddInfo1.Text.ToString()), New SqlParameter("@Additional2", txtAddInfo2.Text.ToString()), New SqlParameter("@Additional3", txtAddInfo3.Text.ToString()), New SqlParameter("@cst", txtcst.Text.ToString()), New SqlParameter("@ecc", txtecc.Text.ToString()), New SqlParameter("@range", txtrange.Text.ToString()), New SqlParameter("@collectorate", txtcollect.Text.ToString()), New SqlParameter("@pan", txtpan.Text.ToString()), New SqlParameter("@Created_By", userCode), New SqlParameter("@Created_Date", connectSql.serverDate(trans)), New SqlParameter("@Modify_By", userCode), New SqlParameter("@Modify_Date", connectSql.serverDate(trans)), New SqlParameter("@Comp_Code", companyCode), New SqlParameter("@is_Gross_Receipt", IsGrossReceipt), New SqlParameter("@Inter_branch", strInterBranch), New SqlParameter("@Branch_Name ", TxtBankBranch.Text.ToString()), New SqlParameter("@Account_No ", TxtAccNo.Text.ToString()), New SqlParameter("@Bank_Name ", TxtBankName.Text.ToString()), New SqlParameter("@IFSC_Code ", IIf(EnableBankFromMaster = True, findTxtIFSCCode.Value, TxtIFSCCode.Text.ToString())), New SqlParameter("@Account_Type ", clsCommon.myCstr(cmbAccountType.SelectedValue)))
            clsDBFuncationality.ExecuteNonQuery(GetUpdateQry(strTagAsFranchise, joint_name, srvc_type), trans)

            Dim strCmd11 As String = "Update TSPL_VENDOR_MASTER set "
            If clsCommon.myLen(txtCompanyBank.Value) > 0 Then
                strCmd11 += " Company_Bank='" + txtCompanyBank.Value + "'"
            Else
                strCmd11 += " Company_Bank= null "
            End If
            strCmd11 += ",Is_Inactive_In_Milk_Procurement ='" + IIf(chkInactiveInMilkModule.Checked, "1", "0") + "', is_Hold_Payment_Process='" + IIf(chkHoldPaymentProcess.Checked, "1", "0") + "',Is_Blacklist='" + clsCommon.myCstr(IIf(clsCommon.myCBool(chk_isblacklist.Checked) = True, 1, 0)) + "',Isbuyerfilereturninlasttwoyears='" + clsCommon.myCstr(IIf(clsCommon.myCBool(chkbuyerfilereturnlasttwoyear.Checked) = True, 1, 0)) + "',IsTCS_TDSamountgreaterthan50KpreviousYear='" + clsCommon.myCstr(IIf(clsCommon.myCBool(chkTCSTDSamountgreater50KpreviousYear.Checked) = True, 1, 0)) + "' where Vendor_Code='" + fndvendorNo.Value + "'"
            connectSql.RunSqlTransaction(trans, strCmd11)

            For i As Integer = 1 To grdTax.Rows.Count
                Dim strTax As String = Convert.ToString("Tax" & Convert.ToString(i))
                Dim Tax As String = grdTax.Rows(i - 1).Cells(0).Value
                Dim strTaxRate As String = Convert.ToString("Tax" & Convert.ToString(i) & "_Rate")
                Dim Tax_Rate As Decimal = Convert.ToDecimal(grdTax.Rows(i - 1).Cells(1).Value)
                Dim strCmd1 As String = "Update TSPL_VENDOR_MASTER set " + strTax + "='" + Tax + "'," + strTaxRate + "=" + Tax_Rate.ToString() + " franchise_yn='" + strTagAsFranchise + "' where Vendor_Code='" + fndvendorNo.Value + "'"
                connectSql.RunSqlTransaction(trans, strCmd1)
            Next


            If objCommonVar.GSTApplicable = True Then
                Dim streq As String = "Update TSPL_VENDOR_MASTER set GSTRegistered='" & Registered & "',GSTEntity='" & txtEntity.Text & "',GSTLastEntity='" & MyTextBox2.Text & "',GSTFinalNo='" & txtGSTIN_No_final.Text & "',GSTMiddle='" & txtFixxed.Text & "' where Vendor_Code='" + fndvendorNo.Value + "'"
                clsDBFuncationality.ExecuteNonQuery(streq, trans)
            End If


            ''For Custom Fields
            Dim arrCustomFields As List(Of clsCustomFieldValues) = New List(Of clsCustomFieldValues)
            If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
                UcCustomFields1.GetData(arrCustomFields)
            End If

            clsCustomFieldValues.SaveData(MyBase.Form_ID, fndvendorNo.Value, arrCustomFields, trans)
            ''End of For Custom Fields


            '' multicurrency
            Dim strq As String
            If Me.fndVendorCurrency.Visible = True Then
                strq = "Update TSPL_VENDOR_MASTER set CURRENCY_CODE='" & clsCommon.myCstr(Me.fndVendorCurrency.Value) & "',Form_Type='" + txtvndrtype.Text + "',state_code='" + txtstatecode.Value + "',country_code='" + txtcountrycode.Value + "' where Vendor_Code='" + fndvendorNo.Value + "'"
            Else
                strq = "Update TSPL_VENDOR_MASTER set CURRENCY_CODE='" & objCommonVar.BaseCurrencyCode & "',Form_Type='" + txtvndrtype.Text + "',state_code='" + txtstatecode.Value + "',country_code='" + txtcountrycode.Value + "' where Vendor_Code='" + fndvendorNo.Value + "'"
            End If
            clsDBFuncationality.ExecuteNonQuery(strq, trans)

            If clsCommon.myCdbl(Me.TxtSecurityCharges.Text) > 0 Then
                strq = "Update TSPL_VENDOR_MASTER set Security_Amount='" & clsCommon.myCdbl(Me.TxtSecurityCharges.Text) & "' where Vendor_Code='" + fndvendorNo.Value + "'"
                clsDBFuncationality.ExecuteNonQuery(strq, trans)
            End If
            If clsCommon.myLen(Me.TxtAmCU.Text) > 0 Then
                strq = "Update TSPL_VENDOR_MASTER set AMCU='" & clsCommon.myCstr(Me.TxtAmCU.Text) & "' where Vendor_Code='" + fndvendorNo.Value + "'"
                clsDBFuncationality.ExecuteNonQuery(strq, trans)
            End If
            If clsCommon.myCdbl(Me.TxtAmc_Charge.Text) > 0 Then
                strq = "Update TSPL_VENDOR_MASTER set AMC_Charge ='" & clsCommon.myCstr(Me.TxtAmc_Charge.Text) & "' where Vendor_Code='" + fndvendorNo.Value + "'"
                clsDBFuncationality.ExecuteNonQuery(strq, trans)
            End If

            If clsCommon.myLen(clsCommon.myCstr(Me.FndMPCode.Value)) > 0 Then
                strq = "Update TSPL_VENDOR_MASTER set MP_code='" & clsCommon.myCstr(FndMPCode.Value) & "',MP_Name='" & clsCommon.myCstr(LblMPName.Text) & "' where Vendor_Code='" + fndvendorNo.Value + "'"
                clsDBFuncationality.ExecuteNonQuery(strq, trans)
            End If
            If clsCommon.myLen(Me.txtjointname.Text) > 0 Then
                If EnableBankFromMaster = True Then
                    strq = "Update TSPL_VENDOR_MASTER set Joint_Bank_Code ='" & clsCommon.myCstr(Me.txtBankCode.Value) & "',Joint_Account_No ='" & clsCommon.myCstr(Me.txtAccountNo.Text) & "' where Vendor_Code='" + fndvendorNo.Value + "'"
                Else
                    strq = "Update TSPL_VENDOR_MASTER set Joint_Bank_Code ='" & clsCommon.myCstr(Me.texttxtBankCode.Text) & "',Joint_Account_No ='" & clsCommon.myCstr(Me.txtAccountNo.Text) & "' where Vendor_Code='" + fndvendorNo.Value + "'"
                End If
                clsDBFuncationality.ExecuteNonQuery(strq, trans)
            End If
            If clsCommon.myCstr(Me.cmbagreemnt.Text) = "YES" Then
                strq = "Update TSPL_VENDOR_MASTER set Agreement ='" & clsCommon.myCstr(Me.cmbagreemnt.Text) & "',Start_Date =COnvert(date,'" & clsCommon.myCstr(Me.txtagrmnt_date.Value) & "',103),End_Date =COnvert(date,'" & clsCommon.myCstr(Me.txtexpir_date.Value) & "',103) where Vendor_Code='" + fndvendorNo.Value + "'"
                clsDBFuncationality.ExecuteNonQuery(strq, trans)
            End If
            If clsCommon.myLen(Me.fndpaymentCycle.Value) > 0 Then
                strq = "Update TSPL_VENDOR_MASTER set PC_CODE ='" & clsCommon.myCstr(Me.fndpaymentCycle.Value) & "' where Vendor_Code='" + fndvendorNo.Value + "'"
                clsDBFuncationality.ExecuteNonQuery(strq, trans)
            End If
            If clsCommon.myLen(txtAliesName.Text) > 0 Then
                Dim AliesQry As String = "Update TSPL_VENDOR_MASTER set Alies_Name='" & clsCommon.myCstr(txtAliesName.Text) & "' where Vendor_Code='" + fndvendorNo.Value + "'"
                clsDBFuncationality.ExecuteNonQuery(AliesQry, trans)
            End If
            '==========shivani
            If clsCommon.myLen(txtChequeInFavour.Text) > 0 Then
                Dim streq As String = "Update TSPL_VENDOR_MASTER set Cheque_In_Favour_Of='" & clsCommon.myCstr(txtChequeInFavour.Text) & "' where Vendor_Code='" + fndvendorNo.Value + "'"
                clsDBFuncationality.ExecuteNonQuery(streq, trans)
            Else
                Dim streq As String = "Update TSPL_VENDOR_MASTER set Cheque_In_Favour_Of= '" + txtvendorname.Text + "'+'-' + '" + TxtBankName.Text + "' +'-' + '" + TxtAccNo.Text + "' where Vendor_Code='" + fndvendorNo.Value + "'"
                clsDBFuncationality.ExecuteNonQuery(streq, trans)
            End If
            'If clsCommon.myLen(txtChequeInFavour.Text) > 0 Then
            '    strq = "Update TSPL_VENDOR_MASTER set Cheque_In_Favour_Of='" & clsCommon.myCstr(txtChequeInFavour.Text) & "' where Vendor_Code='" + fndvendorNo.Value + "'"
            '    clsDBFuncationality.ExecuteNonQuery(strq, trans)
            'End If

            Dim CorrectionFatSNF As String = "update TSPL_VENDOR_MASTER set Care_Of='" & clsCommon.myCstr(txtCareOf.Text) & "',Aadhar_No='" & clsCommon.myCstr(txtAadharNo.Text) & "',TIP_Buffalo='" & clsCommon.myCstr(txtTIPBuffalo.Value) & "',TIP_Cow='" & clsCommon.myCdbl(txtTIPCow.Value) & "',TIP_Mix='" & clsCommon.myCdbl(txtTIPMix.Value) & "', CorrectionFat='" & clsCommon.myCdbl(numCorrectionFat.Text) & "' , CorrectionSNF='" & clsCommon.myCdbl(numCorrectionSNF.Text) & "',Credit_Limit_On_Milk_Receipt_Per='" + clsCommon.myCstr(IIf(chkCreditLimitBasedOnMilkReceipt.Checked, TxtCreditLimitBasedOnMilkReceipt.Value, -1)) + "',SecChequeNoLac1='" & clsCommon.myCstr(txtSecChequeLac1.Text) & "',SecChequeNoRs100='" & clsCommon.myCstr(txtSecChequeRs100.Text) & "' where Vendor_Code='" & fndvendorNo.Value & "'"
            clsDBFuncationality.ExecuteNonQuery(CorrectionFatSNF, trans)
            Dim QryVSPNameHindi As String = " update TSPL_VENDOR_MASTER set Vendor_name_Hindi = N'" + txtvendornameHindi.Text + "' where Vendor_Code='" & fndvendorNo.Value & "' "
            clsDBFuncationality.ExecuteNonQuery(QryVSPNameHindi, trans)
            '' end multi currency
            '' insert Incentives
            updateMultipleIncentive(fndvendorNo.Value, trans)
            If chkCreateCustomerAlso.Checked = True Then
                If CreateCustomer(True, trans) = False Then
                    Exit Sub
                End If
            End If

            ''richa add tds info section 
            Dim IS_TDS_App As Integer
            If ChkIsTDSApp.Checked = True Then
                IS_TDS_App = 1
            ElseIf ChkIsTDSApp.Checked = False Then
                IS_TDS_App = 0
            End If
            Dim State As String
            If clsCommon.myLen(clsCommon.myCstr(txttdsstate.Value)) > 0 Then
                State = "'" & clsCommon.myCstr(txttdsstate.Value) & "'"
            Else
                State = "NULL"
            End If

            Dim TDSBranch As String = ""

            If clsCommon.myLen(clsCommon.myCstr(fndbranchnew.Value)) > 0 Then
                TDSBranch = "'" & clsCommon.myCstr(fndbranchnew.Value) & "'"
            Else
                TDSBranch = "Null"
            End If
            Dim PartyDetailsQry As String
            Dim DBEntry As Double
            Dim TDSQry As String
            DBEntry = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select COUNT(*) As Row From TSPL_TDS_VENDOR_DETAILS Where Vendor_Code ='" & clsCommon.myCstr(fndvendorNo.Value) & "'", trans))
            If ChkIsTDSApp.Checked = True Then
                TDSQry = "Update TSPL_VENDOR_MASTER set Is_TDS_Applicable= " & IS_TDS_App & ",TDS_State_Code=" & State & ",TDS_Status= '" & clsCommon.myCstr(ddlstatus.Text) & "', TDS_Vendor_Type= '" & clsCommon.myCstr(ddlventype.Text) & "', Deduction_Code= '" & clsCommon.myCstr(fnddeducNew.Value) & "',TDS_Branch_Code=" & TDSBranch & " where Vendor_Code='" + fndvendorNo.Value + "'"
                If DBEntry = 0 Then
                    clsDBFuncationality.SaveAStorePorcedure(trans, "SP_TDS_VENDOR_DETAILS_INSERT", New SqlParameter("@Vendor_Code", fndvendorNo.Value), New SqlParameter("@Nature_Of_Deduction", clsCommon.myCstr(fnddeducNew.Value)), New SqlParameter("@State_Code", State.Replace("'", "")), New SqlParameter("@Pan", clsCommon.myCstr(txtpan.Text)), New SqlParameter("@Vendor_Type", clsCommon.myCstr(ddlventype.Text)), New SqlParameter("@status", clsCommon.myCstr(ddlstatus.Text)), New SqlParameter("@Branch_Code", TDSBranch.Replace("'", "")), New SqlParameter("@Inactive", "N"), New SqlParameter("@createdby", userCode), New SqlParameter("@createddate", connectSql.serverDate(trans)), New SqlParameter("@modifiedby", userCode), New SqlParameter("@modifieddate", connectSql.serverDate(trans)), New SqlParameter("@compcode", companyCode))
                    PartyDetailsQry = "UPDATE TSPL_TDS_VENDOR_DETAILS SET State_Code=" & State & ",Branch_Code=" & TDSBranch & " where Vendor_Code='" + fndvendorNo.Value + "'"
                Else
                    PartyDetailsQry = "UPDATE TSPL_TDS_VENDOR_DETAILS SET Nature_Of_Deduction='" & clsCommon.myCstr(fnddeducNew.Value) & "',State_Code=" & State & ",Vendor_TYpe='" & clsCommon.myCstr(ddlventype.Text) & "',Status='" & clsCommon.myCstr(ddlstatus.Text) & "',Branch_Code=" & TDSBranch & ",Pan='" & clsCommon.myCstr(txtpan.Text) & "' where Vendor_Code='" + fndvendorNo.Value + "'"
                End If
            Else
                TDSQry = "Update TSPL_VENDOR_MASTER set Is_TDS_Applicable= " & IS_TDS_App & ",TDS_State_Code=null, TDS_Status= null, TDS_Vendor_Type= null, Deduction_Code= null,TDS_Branch_Code=null where Vendor_Code='" + fndvendorNo.Value + "'"
                PartyDetailsQry = "UPDATE TSPL_TDS_VENDOR_DETAILS SET Nature_Of_Deduction=null,State_Code=null,Vendor_TYpe='Individual',Status='Resident',Branch_Code='' where Vendor_Code='" + fndvendorNo.Value + "'"
            End If
            connectSql.RunSqlTransaction(trans, TDSQry)
            If clsCommon.myLen(PartyDetailsQry) > 0 Then
                connectSql.RunSqlTransaction(trans, PartyDetailsQry)
            End If
            ''---------------------



            myMessages.insert()
            btnsave.Text = "Update"
            btndelete.Enabled = True
            trans.Commit()
            UcAttachment1.SaveData(fndvendorNo.Value)
            'If userCode <> "ADMIN" Then
            '    If funSetUserAccess() = False Then Exit Sub
            'End If


        Catch ex As Exception
            trans.Rollback()
            myMessages.myExceptions(ex)
        End Try
    End Sub

    'Sanjay
    Private Function CreateCustomer(ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim obj As New clsCustomerMaster()
            obj.Cust_Code = clsCommon.myCstr(fndvendorNo.Value)
            obj.Customer_Name = clsCommon.myCstr(txtvendorname.Text)
            obj.Customer_Name_Hindi = clsCommon.myCstr(txtvendornameHindi.Text)
            obj.Alies_Name = clsCommon.myCstr(txtAliesName.Text)
            obj.Add1 = clsCommon.myCstr(txtAdd1.Text)
            obj.Add2 = clsCommon.myCstr(txtAdd2.Text)
            obj.Add3 = clsCommon.myCstr(txtAdd3.Text)
            obj.City_Code = clsCommon.myCstr(fndCity.Value)
            obj.State = clsCommon.myCstr(txtstatecode.Value)
            obj.Country = clsCommon.myCstr(txtcountrycode.Value)
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
            obj.CUSTOMER_FORM_TYPE = "VSP"
            Dim strCmd1 As String = " SELECT Cust_Group_Code as [Customer Gruop Code],Cust_Group_Desc as [Description]," & _
              " Tax_Group as [Tax Group],Cust_Account as [Account Set],Terms_Code as [Terms Code] FROM [TSPL_CUSTOMER_GROUP_MASTER] where Cust_Group_Code='" + fndCusgrp.Value + "' "
            Dim myDs As DataSet = connectSql.RunSQLReturnDS(trans, strCmd1)
            If myDs.Tables(0).Rows.Count > 0 Then
                Dim row As DataRow = myDs.Tables(0).Rows(0)
                obj.Cust_Group_Code = clsCommon.myCstr(row(0).ToString().Trim())
                obj.Tax_Group = clsCommon.myCstr(row(2).ToString().Trim())
                obj.Cust_Account = clsCommon.myCstr(row(3).ToString().Trim())
                obj.Terms_Code = clsCommon.myCstr(row(4).ToString().Trim())
            End If

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

            obj.PIN_NO = clsCommon.myCstr(TxtPinCode.Text)
            obj.Remarks1 = clsCommon.myCstr(txtRemarks1.Text)
            obj.Remarks2 = clsCommon.myCstr(txtRemarks2.Text)
            obj.Additional1 = clsCommon.myCstr(txtAddInfo1.Text)
            obj.Additional2 = clsCommon.myCstr(txtAddInfo2.Text)
            obj.Additional3 = clsCommon.myCstr(txtAddInfo3.Text)
            obj.OutLet_Commossion = clsCommon.myCdbl(0)
            obj.Balance_ToDate = 0
            obj.Credit_Limit = clsCommon.myCdbl(txtCredit.Text)
            obj.CST = clsCommon.myCstr(txtcst.Text)
            obj.ECC = clsCommon.myCstr(txtecc.Text)
            obj.Range = clsCommon.myCstr(txtrange.Text)
            obj.Collectorate = clsCommon.myCstr(txtcollect.Text)
            obj.PAN = clsCommon.myCstr(txtpan.Text)
            obj.Credit_Customer = "N"

            obj.LastInvoice_No = Nothing
            obj.LastInvoice_Date = Nothing
            obj.Inter_Branch = "N"

            obj.IsDistributor = "N"

            obj.prntcustyn = "N"

            obj.CSA_Type = "N"
            obj.ManualCustomer = "N"
            obj.CURRENCY_CODE = fndVendorCurrency.Value
            obj.Comp_Code = objCommonVar.CurrentCompanyCode

            Dim arrDBName As New List(Of String)
            arrDBName.Add(clsCommon.myCstr(objCommonVar.CurrDatabase))

            If Rchkregistered.Checked = True Then
                obj.GSTNO = clsCommon.myCstr(txtGSTIN_No_final.Text)
                obj.GSTEntity = clsCommon.myCstr(txtEntity.Text)
                obj.GSTBlank = clsCommon.myCstr(txtFixxed.Text)
                obj.GSTDigit = clsCommon.myCstr(MyTextBox2.Text)
                obj.GST_Registered = 1
            End If

            obj.SaveData(obj, obj.ArrVisi, isNewEntry, arrDBName, trans)

            'Customer Vendor mapping
            Dim ii As Integer = clsDBFuncationality.getSingleValue("Select COUNT(*) from TSPL_CUSTOMER_VENDOR_MAPPING WHERE cust_code='" + fndvendorNo.Value + "'", trans)
            If ii = 0 Then
                Dim qry As String = "insert into TSPL_CUSTOMER_VENDOR_MAPPING values('" + fndvendorNo.Value + "','" + fndvendorNo.Value + "') "
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
            End If

        Catch ex As Exception
            Return False
            myMessages.myExceptions(ex)
        End Try
        Return True
    End Function

    Public Function updateMultipleIncentive(ByVal Vendor_Code As String, ByVal trans As SqlTransaction) As Boolean
        Dim strq As String = ""
        Try
            strq = "delete from TSPL_VSP_INCENTIVE where VENDOR_CODE='" & Vendor_Code & "'"
            clsDBFuncationality.ExecuteNonQuery(strq, trans)
            If chkMultIncentive.Checked Then
                Dim arrList As New ArrayList
                arrList = txtIncentiveMult.arrValueMember
                For Each Incentive As String In arrList
                    strq = "insert into TSPL_VSP_INCENTIVE(VENDOR_CODE,INCENTIVE_CODE) Values('" & Vendor_Code & "','" & Incentive & "')"
                    clsDBFuncationality.ExecuteNonQuery(strq, trans)
                Next
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Sub LoadIncentive(ByVal Vendor_Code As String, ByVal trans As SqlTransaction)
        Dim strq As String = ""
        Try
            '' get already selected data
            strq = "select Vendor_Code,INCENTIVE_CODE from TSPL_VSP_INCENTIVE where Vendor_Code='" & fndvendorNo.Value & "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(strq)
            Dim arr As New ArrayList
            For Each dr As DataRow In dt.Rows
                arr.Add(clsCommon.myCstr(dr.Item("INCENTIVE_CODE")))
            Next
            txtIncentiveMult.arrValueMember = arr
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Sub
    'This function for updation
    Public Sub funupdate()
        If chkCreateCustomerAlso.Checked = True Then
            If fndCusgrp.Value = "" Then
                myMessages.blankValue("Please Select Customer Group Code")
                pageCus.SelectedPage = RadPageViewPage1
                fndCusgrp.Focus()
                fndCusgrp.Select()
                Return
            End If
        End If
        Dim trans As SqlTransaction = Nothing
        Try
            trans = clsDBFuncationality.GetTransactin()

            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, fndvendorNo.Value, "TSPL_VENDOR_MASTER", "Vendor_Code", trans)

            Dim Registered As Integer = 0
            If objCommonVar.GSTApplicable Then
                If Rchkregistered.Checked = True Then
                    Registered = 1
                Else
                    Registered = 0
                    txtGST_PanCode.Text = ""
                    txtEntity.Text = ""
                    txtGSTStateCode.Text = ""
                    txtGSTIN_No_final.Text = ""
                End If

            End If


            Dim closingdate As String = Nothing
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

            Dim strHold As String = ""
            If chkHold.Checked = True Then
                strHold = "Y"                      '******* for:Hold ******** 
            ElseIf chkHold.Checked = False Then
                strHold = "N"                      '******* for:Remove Hold ********
            End If

            Dim strInterBranch As String = ""
            If chkInterBranch.Checked = True Then
                strInterBranch = "Y"
            Else
                strInterBranch = "N"
            End If

            Dim strtrans As String = ""
            If chktrarns.Checked = True Then
                strtrans = "Y"                      '******* for:Transporter type ******** 
            ElseIf chktrarns.Checked = False Then
                strtrans = "N"
                Dim ii As Integer = clsDBFuncationality.getSingleValue("Select COUNT(*) from tspl_transport_Master WHERE Transport_Id='" + fndvendorNo.Value + "'", trans)
                If ii > 0 Then
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


            Dim CrLimit As Decimal
            If txtCredit.Text = "" Then
                CrLimit = Convert.ToDecimal("0.00")
            Else
                CrLimit = Convert.ToDecimal(txtCredit.Text)
            End If

            Dim srvc_type As String = clsCommon.myCstr(cmbservc_type.Text)
            Dim joint_name As String = clsCommon.myCstr(txtjointname.Text.Replace("'", "`")) 'clsCommon.myCstr(fndVendorCOde.Value)

            Dim IsGrossReceipt As Integer = IIf(chkIsGrossReceipt.Checked, 1, 0)
            clsDBFuncationality.SaveAStorePorcedure(trans, "sp_TSPL_VENDOR_MASTER_UPDATE", New SqlParameter("@Vendor_Code", fndvendorNo.Value), New SqlParameter("@Vendor_Name", txtvendorname.Text.ToString()), New SqlParameter("@Vendor_Group_Code", fndgroupcode.Value), New SqlParameter("Vendor_Group_Des", txtgroupdes.Text.ToString()), New SqlParameter("@Status", strStatus), New SqlParameter("@OnHold", strHold), New SqlParameter("@transporter", strtrans), New SqlParameter("@Closing_Date", closingdate), New SqlParameter("@Add1", txtAdd1.Text.ToString()), New SqlParameter("@Add2", txtAdd2.Text.ToString()), New SqlParameter("@Add3", txtAdd3.Text.ToString()), New SqlParameter("@City_Code", fndCity.Value), New SqlParameter("@City_Des", txtCity.Text.ToString()), New SqlParameter("@State", txtState.Text.ToString()), New SqlParameter("@Country", txtCountry.Text.ToString()), New SqlParameter("@Phone1", txtPhone1.Text.ToString()), New SqlParameter("@Phone2", txtPhone2.Text.ToString()), New SqlParameter("@Fax", txtfax.Text.ToString()), New SqlParameter("@Email", txtEmail.Text.ToString()), New SqlParameter("@WebSite", txtWeb.Text.ToString()), New SqlParameter("@Contact_Person_Name", txtContactName.Text.ToString()), New SqlParameter("@Contact_Person_Phone", txtContPhone.Text.ToString()), New SqlParameter("@Contact_Person_Fax", txtContactFax.Text.ToString()), New SqlParameter("@Contact_Person_Website", txtContactWeb.Text.ToString()), New SqlParameter("@Contact_Person_Email", txtContactEmail.Text.ToString()), New SqlParameter("@Terms_Code", fndTrmsCode.Value), New SqlParameter("@Terms_Code_Des", txttermcodedes.Text.ToString()), New SqlParameter("@Vendor_Account", fndAccntSet.Value), New SqlParameter("@Vendor_Account_Set_Des", fndAccntSet.Value), New SqlParameter("@Payment_Code", fndPayCode.Value), New SqlParameter("@Payment_Code_Des", txtpaymentcodedes.Text.ToString()), New SqlParameter("@Vendor_Type_Code", fndvendortype.Value), New SqlParameter("@Vendor_Type_Des", txtvendortypedes.Text.ToString()), New SqlParameter("@Bank_Code", IIf(EnableBankFromMaster = True, findfndbankcode.Value, fndbankcode.Text)), New SqlParameter("@Bank_Code_Des", txtbankcodedes.Text.ToString()), New SqlParameter("@Service_Tax_No", Me.txtStaxNo.Text), New SqlParameter("@Lst_No", Me.txtLstNo.Text), New SqlParameter("@Tin_No", Me.txtTinNo.Text), New SqlParameter("@Credit_Limit", CrLimit), New SqlParameter("@Tax_Group", Me.fndTxGrp.Value), New SqlParameter("@Tax_Group_Des", txtTxGrp.Text.ToString()), New SqlParameter("@TAX1", strTax1), New SqlParameter("@TAX1_Rate", strTax1_Rate), New SqlParameter("@TAX2", strTax2), New SqlParameter("@TAX2_Rate", strTax2_Rate), New SqlParameter("@TAX3", strTax3), New SqlParameter("@TAX3_Rate", strTax3_Rate), New SqlParameter("@TAX4", strTax4), New SqlParameter("@TAX4_Rate", strTax4_Rate), New SqlParameter("@TAX5", strTax5), New SqlParameter("@TAX5_Rate", strTax5_Rate), New SqlParameter("@TAX6", strTax6), New SqlParameter("@TAX6_Rate", strTax6_Rate), New SqlParameter("@TAX7", strTax7), New SqlParameter("@TAX7_Rate", strTax7_Rate), New SqlParameter("@TAX8", strTax8), New SqlParameter("@TAX8_Rate", strTax8_Rate), New SqlParameter("@TAX9", strTax9), New SqlParameter("@TAX9_Rate", strTax9_Rate), New SqlParameter("@TAX10", strTax10), New SqlParameter("@TAX10_Rate", strTax10_Rate), New SqlParameter("@Remarks1", txtRemarks1.Text.ToString()), New SqlParameter("@Remarks2", txtRemarks2.Text.ToString()), New SqlParameter("@Additional1", txtAddInfo1.Text.ToString()), New SqlParameter("@Additional2", txtAddInfo2.Text.ToString()), New SqlParameter("@Additional3", txtAddInfo3.Text.ToString()), New SqlParameter("@cst", txtcst.Text.ToString()), New SqlParameter("@ecc", txtecc.Text.ToString()), New SqlParameter("@range", txtrange.Text.ToString()), New SqlParameter("@collectorate", txtcollect.Text.ToString()), New SqlParameter("@pan", txtpan.Text.ToString()), New SqlParameter("@Modify_By", userCode), New SqlParameter("@Modify_Date", connectSql.serverDate(trans)), New SqlParameter("@Comp_Code", companyCode), New SqlParameter("@is_Gross_Receipt", IsGrossReceipt), New SqlParameter("@InterBranch ", strInterBranch), New SqlParameter("@Branch_Name ", TxtBankBranch.Text.ToString()), New SqlParameter("@Account_No ", TxtAccNo.Text.ToString()), New SqlParameter("@Bank_Name ", TxtBankName.Text.ToString()), New SqlParameter("@IFSC_Code ", IIf(EnableBankFromMaster = True, findTxtIFSCCode.Value, TxtIFSCCode.Text.ToString())), New SqlParameter("@Account_Type ", clsCommon.myCstr(cmbAccountType.SelectedValue)))
            'Dim strCmd11 As String
            clsDBFuncationality.ExecuteNonQuery(GetUpdateQry(strTagAsFranchise, joint_name, srvc_type), trans)


            Dim strCmd11 As String = "Update TSPL_VENDOR_MASTER set  "
            If clsCommon.myLen(txtCompanyBank.Value) > 0 Then
                strCmd11 += "Company_Bank= '" + txtCompanyBank.Value + "'"
            Else
                strCmd11 += "Company_Bank= null "
            End If
            strCmd11 += ",Is_Inactive_In_Milk_Procurement ='" + IIf(chkInactiveInMilkModule.Checked, "1", "0") + "', is_Hold_Payment_Process='" + IIf(chkHoldPaymentProcess.Checked, "1", "0") + "',Is_Blacklist='" + clsCommon.myCstr(IIf(clsCommon.myCBool(chk_isblacklist.Checked) = True, 1, 0)) + "',Isbuyerfilereturninlasttwoyears='" + clsCommon.myCstr(IIf(clsCommon.myCBool(chkbuyerfilereturnlasttwoyear.Checked) = True, 1, 0)) + "',IsTCS_TDSamountgreaterthan50KpreviousYear='" + clsCommon.myCstr(IIf(clsCommon.myCBool(chkTCSTDSamountgreater50KpreviousYear.Checked) = True, 1, 0)) + "' where Vendor_Code='" + fndvendorNo.Value + "'"
            connectSql.RunSqlTransaction(trans, strCmd11)

            For i As Integer = 1 To grdTax.Rows.Count
                Dim strTax As String = Convert.ToString("Tax" & Convert.ToString(i))
                Dim Tax As String = grdTax.Rows(i - 1).Cells(0).Value
                Dim strTaxRate As String = Convert.ToString("Tax" & Convert.ToString(i) & "_Rate")
                Dim Tax_Rate As Decimal = Convert.ToDecimal(grdTax.Rows(i - 1).Cells(1).Value)
                Dim strCmd As String
                strCmd = "Update TSPL_VENDOR_MASTER set " + strTax + "='" + Tax + "'," + strTaxRate + "='" + Tax_Rate.ToString() + "' where Vendor_Code='" + fndvendorNo.Value + "'"
                clsDBFuncationality.ExecuteNonQuery(strCmd, trans)
            Next

            If objCommonVar.GSTApplicable = True Then
                Dim streq As String = "Update TSPL_VENDOR_MASTER set GSTRegistered='" & Registered & "',GSTEntity='" & txtEntity.Text & "',GSTLastEntity='" & MyTextBox2.Text & "',GSTFinalNo='" & txtGSTIN_No_final.Text & "',GSTMiddle='" & txtFixxed.Text & "' where Vendor_Code='" + fndvendorNo.Value + "'"
                clsDBFuncationality.ExecuteNonQuery(streq, trans)
            End If

            ''For Custom Fields
            Dim arrCustomFields As List(Of clsCustomFieldValues) = New List(Of clsCustomFieldValues)
            If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
                UcCustomFields1.GetData(arrCustomFields)
            End If

            clsCustomFieldValues.SaveData(MyBase.Form_ID, fndvendorNo.Value, arrCustomFields, trans)
            ''End of For Custom Fields


            '' multicurrency
            Dim strq As String
            If Me.fndVendorCurrency.Visible = True Then
                strq = "Update TSPL_VENDOR_MASTER set CURRENCY_CODE='" & clsCommon.myCstr(Me.fndVendorCurrency.Value) & "',Form_Type='" + txtvndrtype.Text + "',state_code='" + txtstatecode.Value + "',country_code='" + txtcountrycode.Value + "' where Vendor_Code='" + fndvendorNo.Value + "'"
            Else
                strq = "Update TSPL_VENDOR_MASTER set CURRENCY_CODE='" & objCommonVar.BaseCurrencyCode & "',Form_Type='" + txtvndrtype.Text + "',state_code='" + txtstatecode.Value + "',country_code='" + txtcountrycode.Value + "' where Vendor_Code='" + fndvendorNo.Value + "'"
            End If
            clsDBFuncationality.ExecuteNonQuery(strq, trans)

            If clsCommon.myCdbl(Me.TxtSecurityCharges.Text) > 0 Then
                strq = "Update TSPL_VENDOR_MASTER set Security_Amount='" & clsCommon.myCdbl(Me.TxtSecurityCharges.Text) & "' where Vendor_Code='" + fndvendorNo.Value + "'"
                clsDBFuncationality.ExecuteNonQuery(strq, trans)
            End If

            If clsCommon.myLen(clsCommon.myCstr(Me.FndMPCode.Value)) > 0 Then
                strq = "Update TSPL_VENDOR_MASTER set MP_code='" & clsCommon.myCstr(FndMPCode.Value) & "',MP_Name='" & clsCommon.myCstr(LblMPName.Text) & "' where Vendor_Code='" + fndvendorNo.Value + "'"
                clsDBFuncationality.ExecuteNonQuery(strq, trans)
            End If
            If clsCommon.myLen(Me.TxtAmCU.Text) > 0 Then
                strq = "Update TSPL_VENDOR_MASTER set AMCU='" & clsCommon.myCstr(Me.TxtAmCU.Text) & "' where Vendor_Code='" + fndvendorNo.Value + "'"
                clsDBFuncationality.ExecuteNonQuery(strq, trans)
            End If
            If clsCommon.myCdbl(Me.TxtAmc_Charge.Text) > 0 Then
                strq = "Update TSPL_VENDOR_MASTER set AMC_Charge ='" & clsCommon.myCstr(Me.TxtAmc_Charge.Text) & "' where Vendor_Code='" + fndvendorNo.Value + "'"
                clsDBFuncationality.ExecuteNonQuery(strq, trans)
            End If
            If clsCommon.myLen(Me.txtjointname.Text) > 0 Then
                If EnableBankFromMaster = True Then
                    strq = "Update TSPL_VENDOR_MASTER set Joint_Bank_Code ='" & clsCommon.myCstr(Me.txtBankCode.Value) & "',Joint_Account_No ='" & clsCommon.myCstr(Me.txtAccountNo.Text) & "' where Vendor_Code='" + fndvendorNo.Value + "'"
                Else
                    strq = "Update TSPL_VENDOR_MASTER set Joint_Bank_Code ='" & clsCommon.myCstr(Me.texttxtBankCode.Text) & "',Joint_Account_No ='" & clsCommon.myCstr(Me.txtAccountNo.Text) & "' where Vendor_Code='" + fndvendorNo.Value + "'"
                End If
                clsDBFuncationality.ExecuteNonQuery(strq, trans)
            End If
            If clsCommon.myCstr(Me.cmbagreemnt.Text) = "YES" Then
                strq = "Update TSPL_VENDOR_MASTER set Agreement ='" & clsCommon.myCstr(Me.cmbagreemnt.Text) & "',Start_Date =COnvert(date,'" & clsCommon.myCstr(Me.txtagrmnt_date.Value) & "',103),End_Date =COnvert(date,'" & clsCommon.myCstr(Me.txtexpir_date.Value) & "',103) where Vendor_Code='" + fndvendorNo.Value + "'"
                clsDBFuncationality.ExecuteNonQuery(strq, trans)
            End If
            If clsCommon.myLen(Me.fndpaymentCycle.Value) > 0 Then
                strq = "Update TSPL_VENDOR_MASTER set PC_CODE ='" & clsCommon.myCstr(Me.fndpaymentCycle.Value) & "' where Vendor_Code='" + fndvendorNo.Value + "'"
                clsDBFuncationality.ExecuteNonQuery(strq, trans)
            End If
            If clsCommon.myLen(txtChequeInFavour.Text) > 0 Then
                Dim streq As String = "Update TSPL_VENDOR_MASTER set Cheque_In_Favour_Of='" & clsCommon.myCstr(txtChequeInFavour.Text) & "' where Vendor_Code='" + fndvendorNo.Value + "'"
                clsDBFuncationality.ExecuteNonQuery(streq, trans)
            Else
                Dim streq As String = "Update TSPL_VENDOR_MASTER set Cheque_In_Favour_Of= '" + txtvendorname.Text + "'+'-' + '" + TxtBankName.Text + "' +'-' + '" + TxtAccNo.Text + "' where Vendor_Code='" + fndvendorNo.Value + "'"
                clsDBFuncationality.ExecuteNonQuery(streq, trans)
            End If
            'If clsCommon.myLen(Me.txtChequeInFavour.Text) > 0 Then
            '    strq = "Update TSPL_VENDOR_MASTER set Cheque_In_Favour_Of ='" & clsCommon.myCstr(Me.txtChequeInFavour.Text) & "' where Vendor_Code='" + fndvendorNo.Value + "'"
            '    clsDBFuncationality.ExecuteNonQuery(strq, trans)
            'End If

            Dim CorrectionFatSNF As String = "update TSPL_VENDOR_MASTER set Care_Of='" & clsCommon.myCstr(txtCareOf.Text) & "',Aadhar_No='" & clsCommon.myCstr(txtAadharNo.Text) & "',TIP_Buffalo='" & clsCommon.myCstr(txtTIPBuffalo.Value) & "',TIP_Cow='" & clsCommon.myCdbl(txtTIPCow.Value) & "',TIP_Mix='" & clsCommon.myCdbl(txtTIPMix.Value) & "', CorrectionFat='" & clsCommon.myCdbl(numCorrectionFat.Text) & "' , CorrectionSNF='" & clsCommon.myCdbl(numCorrectionSNF.Text) & "',Credit_Limit_On_Milk_Receipt_Per='" + clsCommon.myCstr(IIf(chkCreditLimitBasedOnMilkReceipt.Checked, TxtCreditLimitBasedOnMilkReceipt.Value, -1)) + "',SecChequeNoLac1='" & clsCommon.myCstr(txtSecChequeLac1.Text) & "',SecChequeNoRs100='" & clsCommon.myCstr(txtSecChequeRs100.Text) & "' where Vendor_Code='" & fndvendorNo.Value & "'"
            clsDBFuncationality.ExecuteNonQuery(CorrectionFatSNF, trans)

            '' end multi currency
            '' update multiple incentive
            updateMultipleIncentive(fndvendorNo.Value, trans)


            ''richa add tds info section 
            Dim IS_TDS_App As Integer
            If ChkIsTDSApp.Checked = True Then
                IS_TDS_App = 1
            ElseIf ChkIsTDSApp.Checked = False Then
                IS_TDS_App = 0
            End If
            Dim State As String
            If clsCommon.myLen(clsCommon.myCstr(txttdsstate.Value)) > 0 Then
                State = "'" & clsCommon.myCstr(txttdsstate.Value) & "'"
            Else
                State = "NULL"
            End If

            Dim TDSBranch As String = ""

            If clsCommon.myLen(clsCommon.myCstr(fndbranchnew.Value)) > 0 Then
                TDSBranch = "'" & clsCommon.myCstr(fndbranchnew.Value) & "'"
            Else
                TDSBranch = "Null"
            End If
            Dim PartyDetailsQry As String
            Dim DBEntry As Double
            Dim TDSQry As String
            DBEntry = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select COUNT(*) As Row From TSPL_TDS_VENDOR_DETAILS Where Vendor_Code ='" & clsCommon.myCstr(fndvendorNo.Value) & "'", trans))
            If ChkIsTDSApp.Checked = True Then
                TDSQry = "Update TSPL_VENDOR_MASTER set Is_TDS_Applicable= " & IS_TDS_App & ",TDS_State_Code=" & State & ",TDS_Status= '" & clsCommon.myCstr(ddlstatus.Text) & "', TDS_Vendor_Type= '" & clsCommon.myCstr(ddlventype.Text) & "', Deduction_Code= '" & clsCommon.myCstr(fnddeducNew.Value) & "',TDS_Branch_Code=" & TDSBranch & " where Vendor_Code='" + fndvendorNo.Value + "'"
                If DBEntry = 0 Then
                    clsDBFuncationality.SaveAStorePorcedure(trans, "SP_TDS_VENDOR_DETAILS_INSERT", New SqlParameter("@Vendor_Code", fndvendorNo.Value), New SqlParameter("@Nature_Of_Deduction", clsCommon.myCstr(fnddeducNew.Value)), New SqlParameter("@State_Code", State.Replace("'", "")), New SqlParameter("@Pan", clsCommon.myCstr(txtpan.Text)), New SqlParameter("@Vendor_Type", clsCommon.myCstr(ddlventype.Text)), New SqlParameter("@status", clsCommon.myCstr(ddlstatus.Text)), New SqlParameter("@Branch_Code", TDSBranch.Replace("'", "")), New SqlParameter("@Inactive", "N"), New SqlParameter("@createdby", userCode), New SqlParameter("@createddate", connectSql.serverDate(trans)), New SqlParameter("@modifiedby", userCode), New SqlParameter("@modifieddate", connectSql.serverDate(trans)), New SqlParameter("@compcode", companyCode))
                    PartyDetailsQry = "UPDATE TSPL_TDS_VENDOR_DETAILS SET State_Code=" & State & ",Branch_Code=" & TDSBranch & " where Vendor_Code='" + fndvendorNo.Value + "'"
                Else
                    PartyDetailsQry = "UPDATE TSPL_TDS_VENDOR_DETAILS SET Nature_Of_Deduction='" & clsCommon.myCstr(fnddeducNew.Value) & "',State_Code=" & State & ",Vendor_TYpe='" & clsCommon.myCstr(ddlventype.Text) & "',Status='" & clsCommon.myCstr(ddlstatus.Text) & "',Branch_Code=" & TDSBranch & ",Pan='" & clsCommon.myCstr(txtpan.Text) & "' where Vendor_Code='" + fndvendorNo.Value + "'"
                End If
            Else
                TDSQry = "Update TSPL_VENDOR_MASTER set Is_TDS_Applicable= " & IS_TDS_App & ",TDS_State_Code=null, TDS_Status= null, TDS_Vendor_Type= null, Deduction_Code= null,TDS_Branch_Code=null where Vendor_Code='" + fndvendorNo.Value + "'"
                PartyDetailsQry = "UPDATE TSPL_TDS_VENDOR_DETAILS SET Nature_Of_Deduction=null,State_Code=null,Vendor_TYpe='Individual',Status='Resident',Branch_Code='' where Vendor_Code='" + fndvendorNo.Value + "'"
            End If
            connectSql.RunSqlTransaction(trans, TDSQry)
            If clsCommon.myLen(PartyDetailsQry) > 0 Then
                connectSql.RunSqlTransaction(trans, PartyDetailsQry)
            End If
            ''---------------------



            If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*) from tspl_customer_master where CUSTOMER_FORM_TYPE='VSP' and cust_code='" + fndvendorNo.Value + "'", trans)) > 0 Then
                If CreateCustomer(False, trans) = False Then
                    Exit Sub
                End If
            Else
                If chkCreateCustomerAlso.Checked = True Then
                    If CreateCustomer(True, trans) = False Then
                        Exit Sub
                    End If
                End If
            End If

            Dim QryVSPNameHindi As String = " update TSPL_VENDOR_MASTER set Vendor_name_Hindi = N'" + txtvendornameHindi.Text + "' where Vendor_Code='" & fndvendorNo.Value & "' "
            clsDBFuncationality.ExecuteNonQuery(QryVSPNameHindi, trans)

            trans.Commit()

            UcAttachment1.SaveData(fndvendorNo.Value)
            myMessages.update()
        Catch ex As Exception
            trans.Rollback()
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Customer")
        End Try
    End Sub

    Function GetUpdateQry(ByVal strTagAsFranchise As String, ByVal joint_name As String, ByVal srvc_type As String) As String

        Dim strCmd11 As String = "Update TSPL_VENDOR_MASTER set  Monthly_Rent='" + clsCommon.myCstr(txtMonthlyRent.Value) + "', Nature='E',Billing_Date='" & clsCommon.GetPrintDate(DtpBillingDate.Value, "dd-MMM-yyyy") & "', franchise_yn='" + strTagAsFranchise + "',Service_charges='0',commision_pers='" + clsCommon.myCstr(clsCommon.myCdbl(txtcommpers.Text)) + "',incentive='" + clsCommon.myCstr(FndIncentive.Value) + "',incentive_days='" + clsCommon.myCstr(clsCommon.myCdbl(txtno_days.Text)) + "',vsp_payment='" + cmbvsppayment.SelectedValue + "',VSP_Payee_Name='" + txtpayeename.Text + "',Service_Charge_Type='" + srvc_type + "',Joint_Name='" + joint_name + "',payment_commision_pers='" + clsCommon.myCstr(clsCommon.myCdbl("payment_commision_pers")) + "',Is_Head_Load='" & IIf(ChkHeadLoad.Checked = True, "T", "F") & "',Rate_Head_Load='" & clsCommon.myCdbl(txtRateHeadLoad.Text) & "',DistanceKM_Head_Load='" & txtDistanceKMHeadLoad.Value & "',Standard_Security_Amount='" & clsCommon.myCdbl(TxtStandardSec_Amt.Text) & "',Service_Basis_Head_Load='" & CmbHeadLoadServiceBasis.SelectedValue & "',Is_Own_Asset='" & IIf(ChkOwnAsset.Checked = True, "T", "F") & "',Rate_Own_Asset='" & clsCommon.myCdbl(TxtRateOwnAsset.Text) & "',Service_Basis_Own_Asset='" & CmbOwnAssetServiceBasis.SelectedValue & "',Pin_Code='" & clsCommon.myCstr(TxtPinCode.Text) & "',is_drip_saver='" & IIf(ChkIsDripSaver.Checked, "Y", "") & "',Joint_Branch_Name='" & IIf(EnableBankFromMaster = True, txtBankBranchCode.Value, texttxtBankBranchCode.Text) & "',Joint_IFSC_Code='" & txtIFCICode.Text & "'" +
            ",EMP_Type='" + clsCommon.myCstr(cboEMPType.SelectedValue) + "',Apply_Mult_Incentive=" & IIf(chkMultIncentive.Checked = True, 1, 0) & ""
        If clsCommon.CompairString(clsCommon.myCstr(cboEMPType.SelectedValue), "FAFP") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(cboEMPType.SelectedValue), "FASWP") = CompairStringResult.Equal Then
            strCmd11 += " ,EMP_Fixed_Amount='" + clsCommon.myCstr(txtFixedAmount.Value) + "'"
        Else
            strCmd11 += " ,EMP_Fixed_Amount='0'"
        End If
        strCmd11 += ",Security_Deduction_Amount='" + clsCommon.myCstr(txtVSPSecurityDeduction.Value) + "',Handling_Charges_Per='" + clsCommon.myCstr(txtHandlingChargesPer.Value) + "',Interest_Per='" + clsCommon.myCstr(txtVSPInterestPer.Value) + "',Minimum_Interest='" + clsCommon.myCstr(txtVSPMinimumInterest.Value) + "',Service_Charge_Per_Unit='" + clsCommon.myCstr(txtServiceCharge.Value) + "' "
        For ii As Integer = 0 To 4
            Dim skipQuery As Boolean = False
            Dim str As String = IIf(ii > 0, clsCommon.myCstr(ii + 1), "")
            If clsCommon.CompairString(clsCommon.myCstr(cboEMPType.SelectedValue), "FP") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(cboEMPType.SelectedValue), "FAFP") = CompairStringResult.Equal Then
                If ii > 0 Then
                    strCmd11 += ", Actual_charges_Slab" & str & " ='0', Actual_charges" & str & " ='0'"
                    skipQuery = True
                End If
            End If
            If Not skipQuery Then
                If clsCommon.CompairString(clsCommon.myCstr(cboEMPType.SelectedValue), "SWP") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(cboEMPType.SelectedValue), "FASWP") = CompairStringResult.Equal Then
                    strCmd11 += ", Actual_charges_Slab" & str & " ='" & clsCommon.myCdbl(gvEMP.Rows(ii).Cells(colEMPSlab).Value) & "'"
                Else
                    strCmd11 += ", Actual_charges_Slab" & str & " ='0'"
                End If
                strCmd11 += ", Actual_charges" & str & " ='" & clsCommon.myCdbl(gvEMP.Rows(ii).Cells(colEMPValue).Value) & "'"
            End If
        Next
        strCmd11 += " where Vendor_Code='" + fndvendorNo.Value + "'"
        Return strCmd11
    End Function
    Public Sub fundelete()
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim qst As String
            Dim dpt As String
            '------for payment screen---------
            qst = "select Vendor_Code from TSPL_PAYMENT_HEADER where Vendor_Code='" + fndvendorNo.Value + "'"
            dpt = clsDBFuncationality.getSingleValue(qst, trans)
            If clsCommon.myLen(dpt) > 0 AndAlso dpt IsNot Nothing Then
                Throw New Exception("This Customer Cannot be deleted." + Environment.NewLine + "This is already in Process")
                'Return
            End If
            '-----------for store recevied screen-------
            qst = "select Vendor_Code from TSPL_SRN_HEAD where Vendor_Code='" + fndvendorNo.Value + "'"
            dpt = clsDBFuncationality.getSingleValue(qst, trans)
            If clsCommon.myLen(dpt) > 0 AndAlso dpt IsNot Nothing Then
                Throw New Exception("This Customer Cannot be deleted." + Environment.NewLine + "This is already in Process")
                Return
            End If
            qst = "select Vendor_Code from TSPL_PR_HEAD where Vendor_Code='" + fndvendorNo.Value + "'"
            dpt = clsDBFuncationality.getSingleValue(qst, trans)
            If clsCommon.myLen(dpt) > 0 AndAlso dpt IsNot Nothing Then
                Throw New Exception("This Customer Cannot be deleted." + Environment.NewLine + "This is already in Process")
                Return
            End If

            connectSql.RunSpTransaction(trans, "sp_TSPL_VENDOR_MASTER_DELETE", New SqlParameter("@Vendor_Code", fndvendorNo.Value), New SqlParameter("@form_type", txtvndrtype.Text))
            connectSql.RunSpTransaction(trans, "sp_TSPL_CUSTOMER_MASTER_DELETE", New SqlParameter("@Cust_Code", fndvendorNo.Value), New SqlParameter("@CUSTOMER_FORM_TYPE", "VSP"))
            clsCustomFieldValues.DeleteData(MyBase.Form_ID, fndvendorNo.Value, trans)


            btnsave.Text = "Save"
            btndelete.Enabled = False
            trans.Commit()
            myMessages.delete()
            Reset()
            funreset()
        Catch ex As Exception
            trans.Rollback()
            myMessages.myExceptions(ex)
        End Try
    End Sub

    Sub LoadBlank_Charges_Grid()
        Try
            gvCharges.Rows.Clear()
            gvCharges.Columns.Clear()
            gvCharges.Columns.Add("COLChargeCode", "Charge Code")
            gvCharges.Columns.Add("COLChargeDESC", "Description")
            ' gvCharges.Columns.Add("COLGLCODE", "GL Code")
            'gvCharges.Columns.Add("COLGLDESC", "GL Desc")
            gvCharges.Columns.Add("COLRate", "EMP")

            gvCharges.Columns("COLChargeCode").Width = 100
            gvCharges.Columns("COLChargeDESC").Width = 200
            'gvCharges.Columns("COLGLCODE").Width = 100
            gvCharges.Columns("COLRate").Width = 100
            'gvCharges.Columns("COLGLDESC").Width = 200

            gvCharges.Columns("COLChargeDESC").ReadOnly = True
            'gvCharges.Columns("COLGLCODE").ReadOnly = True
            'gvCharges.Columns("COLGLDESC").ReadOnly = True

            gvCharges.AllowAddNewRow = True
            gvCharges.MasterTemplate.AddNewRowPosition = SystemRowPosition.Bottom
            gvCharges.AllowEditRow = True
            gvCharges.AllowDeleteRow = True
            gvCharges.AllowRowResize = False
            gvCharges.AllowRowReorder = False
            gvCharges.AllowColumnResize = True
            gvCharges.AllowColumnChooser = False
            gvCharges.AllowAutoSizeColumns = False
            gvCharges.ShowGroupPanel = False
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Sub LoadVSPPayment()
        Dim dt As New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow = Nothing

        'dr = dt.NewRow()
        'dr("Code") = ""
        'dr("Name") = "Select"
        'dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Self"
        dr("Name") = "Self"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Different"
        dr("Name") = "Different"
        dt.Rows.Add(dr)

        'cmbvsppayment.DataSource = Nothing
        cmbvsppayment.DataSource = dt
        cmbvsppayment.DisplayMember = "Name"
        cmbvsppayment.ValueMember = "Code"
        cmbvsppayment.SelectedValue = "Different"
        If clsCommon.CompairString(cmbvsppayment.SelectedValue, "Different") = CompairStringResult.Equal Then
            'txtpayeename.Enabled = True
            txtjointname.Enabled = True
            'fndVendorCOde.Enabled = True
            Grpjoint.Enabled = True

            If EnableBankFromMaster = True Then
                texttxtBankCode.Visible = False
                texttxtBankBranchCode.Visible = False
                txtBankCode.Visible = True
                txtBankBranchCode.Visible = True
            Else
                texttxtBankCode.Visible = True
                texttxtBankBranchCode.Visible = True
                txtBankCode.Visible = False
                txtBankBranchCode.Visible = False
            End If

        Else
            ' txtpayeename.Text = ""
            txtpayeename.Enabled = False
            txtjointname.Text = ""
            txtjointname.Enabled = False
            txtBankBranchName.Text = Nothing
            txtBankBranchCode.Text = Nothing
            txtBankBranchName.Text = Nothing
            fndBankCity.Text = Nothing
            txtBankCityName.Text = Nothing
            fndBankState.Value = Nothing
            txtBankStateName.Text = Nothing
            TxtIFSCCode.Text = ""
            Grpjoint.Enabled = False
            txtpayeename.Text = txtvendorname.Text
            'fndVendorCOde.Value = ""
            'fndVendorCOde.Enabled = False
        End If

    End Sub

    Sub LoadEMPType()
        Dim dt As New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow = Nothing

        dr = dt.NewRow()
        dr("Code") = "FP"
        dr("Name") = "Fixed Percentage"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "SWP"
        dr("Name") = "Slab Wise Percentage"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "FAFP"
        dr("Name") = "Fixed Amount + Fixed Percentage"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "FASWP"
        dr("Name") = "Fixed Amount + Slab Wise Percentage"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "FPSP"
        dr("Name") = "Fixed Percentage + Standard Price"
        dt.Rows.Add(dr)

        cboEMPType.DataSource = dt
        cboEMPType.DisplayMember = "Name"
        cboEMPType.ValueMember = "Code"
    End Sub

    Sub LoadIncentive()
        Dim dt As New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow = Nothing

        dr = dt.NewRow()
        dr("Code") = ""
        dr("Name") = "Select"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Slab"
        dr("Name") = "Slab Wise"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Qty"
        dr("Name") = "Qty Wise"
        dt.Rows.Add(dr)

        'cmbincentive.DataSource = Nothing
        cmbincentive.DataSource = dt
        cmbincentive.DisplayMember = "Name"
        cmbincentive.ValueMember = "Code"
    End Sub


    Sub Load_KG_and_LTr(ByVal Cmb As common.Controls.MyComboBox)
        Load_KG_and_LTr(Cmb, False)
    End Sub
    Sub Load_KG_and_LTr(ByVal Cmb As common.Controls.MyComboBox, ByVal isFATSNFWeight As Boolean)
        Dim dt As New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow = Nothing
        dr = dt.NewRow()
        dr("Code") = "K"
        dr("Name") = "KG"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "L"
        dr("Name") = "LTR"
        dt.Rows.Add(dr)

        If isFATSNFWeight Then
            dr = dt.NewRow()
            dr("Code") = "W"
            dr("Name") = "FAT/SNF KG"
            dt.Rows.Add(dr)
        End If

        Cmb.DataSource = dt
        Cmb.DisplayMember = "Name"
        Cmb.ValueMember = "Code"
    End Sub


    'this function will reset all the fields for new entry
    Public Sub funreset()
        chkTCSTDSamountgreater50KpreviousYear.Checked = False
        chkbuyerfilereturnlasttwoyear.Checked = False
        chk_isblacklist.Checked = False
        IsInsieLoadData = False
        chkMultIncentive.Checked = False
        txtIncentiveMult.Enabled = False
        txtAccountNo.Text = ""
        gvCharges.Rows.Clear()
        cmbservc_type.Text = "Select"
        txtjointname.Text = ""
        txtno_days.Enabled = False
        txtpayeename.Enabled = False
        txtMonthlyRent.Value = 0
        chkHoldPaymentProcess.Checked = False
        cmbagreemnt.Text = "NO"
        FndMPCode.Value = Nothing
        LblMPName.Text = Nothing
        TxtPinCode.Text = Nothing
        ChkIsMP.Checked = False
        TxtJointBankName.Text = Nothing
        txtagrmnt_date.Text = clsCommon.GETSERVERDATE()
        txtexpir_date.Text = clsCommon.GETSERVERDATE()
        fndpaymentCycle.Value = Nothing
        TxtPaymentCycle.Text = Nothing
        txtBankCode.Value = Nothing
        ChkIsDripSaver.Checked = False
        txtcommpers.Text = ""
        txtpaymnt_cmsn.Text = ""
        cmbincentive.SelectedValue = ""
        txtno_days.Text = ""
        cmbvsppayment.SelectedValue = ""
        txtpayeename.Text = ""
        txtChequeInFavour.Enabled = True
        txtcountrycode.Value = clsDBFuncationality.getSingleValue("select country_Code from tspl_Country_Master where Country_Code='INDIA'") '""
        txtCountry.Text = clsDBFuncationality.getSingleValue("select country_Name from tspl_Country_Master where Country_Code='INDIA'") '""
        txtstatecode.Value = ""
        txtvndrtype.Text = "VSP"
        'fndVendorCOde.Value = ""
        txtvendornameHindi.Text = ""
        Me.fndvendorNo.Value = ""
        Me.txtvendortypedes.Text = ""
        Me.txtvendorname.Text = ""
        Me.fndgroupcode.Value = ""
        Me.txtgroupdes.Text = ""
        chkHold.Checked = False
        chkInActive.Checked = False
        chktrarns.Checked = False
        Me.dtClosing.Value = connectSql.serverDate()
        Me.txtAdd1.Text = ""
        Me.txtAdd2.Text = ""
        Me.txtAdd3.Text = ""
        Me.fndCity.Value = ""
        Me.txtCity.Text = ""
        Me.txtState.Text = ""
        ' Me.txtCountry.Text = ""
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
        Me.fndbankcode.Text = ""
        Me.txtbankcodedes.Text = ""
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
        Me.chkInterBranch.Checked = False
        Me.grdTax.DataSource = Nothing
        Me.grdTax.Rows.Clear()
        Me.fndVendorCurrency.Value = Nothing
        '' Anubhooti 4-Aug-2014 BM00000003319
        cmbAccountType.SelectedValue = "Cur"
        TxtBankBranch.Text = ""
        TxtBankName.Text = ""
        TxtIFSCCode.Text = ""
        TxtAccNo.Text = ""
        TxtBankName.Text = ""
        txtBankBranchCode.Value = ""
        txtBankBranchName.Text = ""
        fndBankCity.Value = ""
        txtBankCityName.Text = ""
        fndBankState.Value = ""
        txtBankStateName.Text = ""
        txtIFCICode.Text = ""
        TxtSecurityAmount.Text = 0
        txtSecurityDeductedAmount.Text = 0
        txtChequeInFavour.Text = ""
        chkInActive.Checked = False
        dtClosing.Enabled = False
        findfndbankcode.Value = ""
        findTxtIFSCCode.Value = ""
        texttxtBankCode.Text = ""
        texttxtBankBranchCode.Text = ""
        btnsave.Text = "Save"
        btndelete.Enabled = False
        chkTagAsFranchise.Checked = False
        txtvendorname.Focus()
        TxtSecurityCharges.Text = Nothing
        DtpBillingDate.Value = clsCommon.GETSERVERDATE()
        DtpEndBillingDate.Value = DtpBillingDate.Value.AddDays(2)
        chktrarns.Visible = False
        TxtAmCU.Text = Nothing
        TxtAmc_Charge.Text = Nothing
        txtBankBranchName.Text = Nothing
        txtBankBranchCode.Text = Nothing
        txtBankBranchName.Text = Nothing
        fndBankCity.Text = Nothing
        txtBankCityName.Text = Nothing
        fndBankState.Value = Nothing
        txtBankStateName.Text = Nothing
        txtIFCICode.Text = Nothing
        cmbvsppayment.SelectedValue = "Different"
        If clsCommon.CompairString(cmbvsppayment.SelectedValue, "Different") = CompairStringResult.Equal Then
            Grpjoint.Enabled = True
            If EnableBankFromMaster = True Then
                texttxtBankCode.Visible = False
                texttxtBankBranchCode.Visible = False
                txtBankCode.Visible = True
                txtBankBranchCode.Visible = True
            Else
                texttxtBankCode.Visible = True
                texttxtBankBranchCode.Visible = True
                txtBankCode.Visible = False
                txtBankBranchCode.Visible = False
            End If
        Else
            Grpjoint.Enabled = False
        End If
        FndIncentive.Value = Nothing
        LblIncentive.Text = ""
        ChkHeadLoad.Checked = False
        txtRateHeadLoad.Text = Nothing
        txtDistanceKMHeadLoad.Text = Nothing
        TxtStandardSec_Amt.Text = Nothing
        CmbHeadLoadServiceBasis.SelectedValue = -1
        ChkOwnAsset.Checked = False
        TxtRateOwnAsset.Text = Nothing
        CmbOwnAssetServiceBasis.SelectedValue = -1

        LoadBlank_Charges_Grid()
        'LoadVisi()
        LoadVisiDetail()
        Load_KG_and_LTr(CmbHeadLoadServiceBasis, True)
        Load_KG_and_LTr(CmbOwnAssetServiceBasis)
        LoadBlankPaymentEntry()
        LoadBlankBankGuaranteeGrid()
        gvBankG.Rows.Clear()
        GVPaymentEntry.Rows.Clear()

        UcAttachment1.Form_ID = Me.Form_ID
        UcAttachment1.BlankAllControls()
        ''For Custom Fields
        If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
            UcCustomFields1.BlankAllControls()
        End If
        ''End of For Custom Fields
        loadBlankGridEMP()
        txtFixedAmount.Value = 0
        txtHandlingChargesPer.Value = 0
        txtVSPSecurityDeduction.Value = 0
        txtVSPInterestPer.Value = 0
        txtVSPMinimumInterest.Value = 0
        txtServiceCharge.Value = 0

        MyTextBox2.Text = ""
        txtEntity.Text = ""
        txtGSTIN_No_final.Text = ""
        txtGST_PanCode.Text = ""
        txtGSTStateCode.Text = ""

        cboEMPType.Enabled = True
        cmbservc_type.Enabled = True
        gvEMP.Enabled = True

        If FixVSPEMP > 0 Then
            cboEMPType.SelectedValue = "FP"
            cmbservc_type.Text = "%(Percentage)"
            gvEMP.Rows(0).Cells(colEMPValue).Value = FixVSPEMP

            cboEMPType.Enabled = False
            cmbservc_type.Enabled = False
            gvEMP.Enabled = False
        End If
        numCorrectionFat.Text = ""
        numCorrectionSNF.Text = ""
        TxtCreditLimitBasedOnMilkReceipt.Text = Nothing
        chkCreditLimitBasedOnMilkReceipt.Checked = False
        chkCreateCustomerAlso.Checked = False
        fndCusgrp.Value = Nothing

        txtTIPBuffalo.Value = 0
        txtTIPCow.Value = 0
        txtTIPMix.Value = 0
        fndVSPCopy.Value = ""
        txtAadharNo.Text = ""
        txtCareOf.Text = ""
        funSetDefaultData()
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
        txtSecChequeLac1.Text = ""
        txtSecChequeRs100.Text = ""


        chkRegistered.Checked = False
        chkPDCS.Checked = False
        chkCLUSTER.Checked = False
        txtStartDate.Value = clsCommon.GETSERVERDATE()
        chkOwnBMC.Checked = False
        txtMCCOwnBMC.Value = ""
        lblMCCOwnBMC.Text = ""
        findfndbankcode2.Value = ""
        txtbankcodedes2.Text = ""
        txtCredit2.Text = 0
        txtIFSCCode2.Text = ""
        TxtAccNo2.Text = ""
        'cmbAccountType2
        TxtBankName2.Text = ""
        txtBankBranch2.Text = ""
        TxtSecurityCharges2.Text = ""
        fndbankcode2.Text = ""
        txtIFSCCode2.Text = ""
        findTxtIFSCCode2.Value = ""
        txtSupervisiorRP.Value = ""
        lblSupervisiorRPName.Text = ""
        txtCompanyBank.Value = ""
        lblCompanyBank.Text = ""
    End Sub

    Private Sub funSetDefaultData()
        txtstatecode.Value = clsDBFuncationality.getSingleValue("select state from TSPL_COMPANY_MASTER where comp_code='" + objCommonVar.CurrentCompanyCode + "'")
        fndgroupcode.Value = DefaultVendorGroupCodeforVSP
        If clsCommon.myLen(DefaultVendorGroupCodeforVSP) > 0 Then
            Me.txtgroupdes.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(Group_Desc,'') as tt from TSPL_VENDOR_GROUP where Ven_Group_Code='" + DefaultVendorGroupCodeforVSP + "'"))
        Else
            Me.txtgroupdes.Text = ""
        End If
        Me.txtState.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select TSPL_STATE_MASTER.STATE_NAME from TSPL_STATE_MASTER left join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.State=TSPL_STATE_MASTER.STATE_CODE where TSPL_COMPANY_MASTER.comp_code='" + objCommonVar.CurrentCompanyCode + "'"))
        Me.fndTxGrp.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(Tax_Group_Code,'') as aa from TSPL_TAX_GROUP_MASTER where Tax_Group_Type='P' and Is_Tax_Exempted=1"))
        Me.txtTxGrp.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(Tax_Group_Desc,'') as aa from TSPL_TAX_GROUP_MASTER where Tax_Group_Type='P' and Is_Tax_Exempted=1"))
        fndCusgrp.Value = DefaultCustomerGroupCodeforVSP
        txtTIPBuffalo.Value = TIPRateBuffalo
        txtTIPCow.Value = TIPRateCow
        txtTIPMix.Value = TIPRateMix
    End Sub

    Sub loadBlankGridEMP()
        Try
            gvEMP.Rows.Clear()
            gvEMP.Columns.Clear()

            Dim repoLineNo As GridViewDecimalColumn = New GridViewDecimalColumn()
            repoLineNo = New GridViewDecimalColumn()
            repoLineNo.FormatString = ""
            repoLineNo.HeaderText = "SNo"
            repoLineNo.Name = colEMPSno
            repoLineNo.Width = 50
            repoLineNo.ReadOnly = True
            repoLineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
            gvEMP.MasterTemplate.Columns.Add(repoLineNo) '0

            Dim repoNum As GridViewDecimalColumn = New GridViewDecimalColumn()
            repoNum.FormatString = ""
            repoNum.HeaderText = "Slab"
            repoNum.Name = colEMPSlab
            repoNum.Width = 200
            repoNum.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
            repoNum.ReadOnly = False
            repoNum.ShowUpDownButtons = False
            repoNum.Step = 0
            repoNum.Minimum = 0
            gvEMP.MasterTemplate.Columns.Add(repoNum)

            repoNum = New GridViewDecimalColumn()
            repoNum.FormatString = ""
            repoNum.HeaderText = "Value"
            repoNum.Name = colEMPValue
            repoNum.Width = 200
            repoNum.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
            repoNum.ReadOnly = False
            repoNum.ShowUpDownButtons = False
            repoNum.Step = 0
            repoNum.Minimum = 0
            gvEMP.MasterTemplate.Columns.Add(repoNum)

            gvEMP.AllowAddNewRow = False
            gvEMP.ShowGroupPanel = False
            gvEMP.AllowColumnReorder = False
            gvEMP.AllowRowReorder = False
            gvEMP.EnableSorting = False
            gvEMP.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
            gvEMP.MasterTemplate.ShowRowHeaderColumn = False
            gvEMP.TableElement.TableHeaderHeight = 40

            For ii As Integer = 1 To 5
                gvEMP.Rows.AddNew()
                gvEMP.Rows(ii - 1).Cells(colEMPSno).Value = ii
            Next

            SetEMPColumns()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Public Sub GettotalAmount()
        TxtTotalAmount.Text = clsCommon.myCdbl(TxtSecurityAmount.Text) + clsCommon.myCdbl(TxtGuranteeAmount.Text) - clsCommon.myCdbl(txtSecurityDeductedAmount.Text) - clsCommon.myCdbl(TxtBankRefundedAmount.Text)
    End Sub

#End Region

#Region "Event"
    Sub fndvendorNo_text_changed()
        Try
            Dim str As String = "select vendor_code from TSPL_VENDOR_MASTER where vendor_code = '" + fndvendorNo.Value + "' and form_type='" + txtvndrtype.Text + "'"
            Dim dr As DataTable
            Dim strvalue As String = ""
            dr = clsDBFuncationality.GetDataTable(str)
            If dr.Rows IsNot Nothing AndAlso dr.Rows.Count > 0 Then
                strvalue = dr.Rows(0)("vendor_code").ToString()
            End If
            If strvalue <> "" Then
                funfill()
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

                txtno_days.Text = ""
                cmbincentive.SelectedValue = ""
                cmbvsppayment.SelectedValue = ""
                txtpayeename.Text = ""
                txtcommpers.Text = ""
                txtpaymnt_cmsn.Text = ""
                txtcountrycode.Value = ""
                txtstatecode.Value = ""
                Me.txtvendorname.Text = ""
                Me.fndgroupcode.Value = ""
                Me.txtgroupdes.Text = ""
                chkHold.Checked = False
                chkInActive.Checked = False
                chktrarns.Checked = False
                chkTagAsFranchise.Checked = False
                chkHoldPaymentProcess.Checked = False
                Me.dtClosing.Value = clsCommon.GETSERVERDATE()
                Me.txtAdd1.Text = ""
                Me.txtAdd2.Text = ""
                Me.txtAdd3.Text = ""
                Me.fndCity.Value = ""
                Me.txtCity.Text = ""
                Me.txtState.Text = ""
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
                Me.fndbankcode.Text = ""
                Me.txtbankcodedes.Text = ""
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
                Me.grdTax.DataSource = Nothing
                Me.grdTax.Rows.Clear()
                If AllowVSPMasterAutoPrefix = 1 Then
                    fndvendorNo.Value = ""
                End If
                btnsave.Text = "Save"
                btndelete.Enabled = False


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
    'Public Sub fndbankcode_text_Changed(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Try
    '        Dim strquery As String = "select bank_code As [Bank Code],description  as [Description]from TSPL_Bank_MASTER where bank_code='" + fndbankcode.Value + "'"
    '        Dim dr As DataTable
    '        Dim strvalue As String = ""
    '        dr = clsDBFuncationality.GetDataTable(strquery)
    '        If dr.Rows IsNot Nothing AndAlso dr.Rows.Count > 0 Then
    '            strvalue = dr.Rows(0)("Bank Code").ToString()
    '        End If
    '        If strvalue <> "" Then
    '            funfillbank()
    '        Else
    '            txtbankcodedes.Text = ""
    '        End If
    '    Catch ex As Exception
    '        myMessages.myExceptions(ex)
    '    End Try
    'End Sub
    'This will check the existence of value in databse on text change property of finder,if it exist then it will call funfill() otherwise it will blank the fields
    Public Sub fndTxGrp_text_Changed(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try


            Dim strquery As String = " SELECT TSPL_TAX_GROUP_DETAILS.Tax_Code as [Tax] FROM TSPL_TAX_GROUP_MASTER INNER JOIN TSPL_TAX_GROUP_DETAILS ON TSPL_TAX_GROUP_MASTER.Tax_Group_Code = TSPL_TAX_GROUP_DETAILS.Tax_Group_Code" & _
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
                    common.clsCommon.MyMessageBoxShow("This Group Code does not exist in Master Table")
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
                    common.clsCommon.MyMessageBoxShow("This City does not exist in Master Table")
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
                    common.clsCommon.MyMessageBoxShow("This Term Code does not exist in Master Table")
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
                Dim strquery As String = "SELECT  [Acct_Set_Code] as [Account Set Code],[Acct_Set_Desc] as [Description] FROM [TSPL_VENDOR_ACCOUNT_SET]where Acct_Set_Code ='" + fndAccntSet.Value + "'"
                Dim dr As DataTable
                Dim strvalue As String = ""
                dr = clsDBFuncationality.GetDataTable(strquery)
                If dr.Rows IsNot Nothing AndAlso dr.Rows.Count > 0 Then
                    strvalue = dr.Rows(0)("Account Set Code").ToString()
                End If
                If strvalue <> "" Then
                Else : strquery = ""
                    txtaccsetdes.Text = ""
                    common.clsCommon.MyMessageBoxShow("This Account does not exist in Master Table")
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
                    common.clsCommon.MyMessageBoxShow("This Payment Code does not exist in Master Table")
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
                    common.clsCommon.MyMessageBoxShow("This Vendor Type does not exist in Master Table")
                    fndvendortype.Value = ""
                End If
            Catch ex As Exception
                myMessages.myExceptions(ex)
            End Try
        End If
    End Sub
    'This will check the existence of value in databse on leave property of finder,if it exist then it will fill, otherwise it will blank the fields
    'Public Sub fndbankcode_leave(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    If fndbankcode.Value = "" Then
    '    Else
    '        Try
    '            Dim strquery As String = "select bank_code As [Bank Code],description  as [Description]from TSPL_Bank_MASTER where bank_code='" + fndbankcode.Value + "'"
    '            Dim dr As DataTable
    '            Dim strvalue As String = ""
    '            dr = clsDBFuncationality.GetDataTable(strquery)
    '            If dr.Rows IsNot Nothing AndAlso dr.Rows.Count > 0 Then
    '                strvalue = dr.Rows(0)("Bank Code").ToString()
    '            End If
    '            If strvalue <> "" Then
    '            Else : strquery = ""
    '                txtbankcodedes.Text = ""
    '                common.clsCommon.MyMessageBoxShow("This Bank Code does not exist in Master Table")
    '                fndbankcode.Value = ""
    '            End If
    '        Catch ex As Exception
    '            myMessages.myExceptions(ex)
    '        End Try
    '    End If
    'End Sub
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
                    common.clsCommon.MyMessageBoxShow("This Tax Group does not exist in Master Table")
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
    'Public Sub fndbankcode_key_press(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)

    '    If (e.KeyChar = Chr(39)) Then
    '        e.Handled = True
    '    End If
    'End Sub

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
                Errorcontrol.ResetError(txtEmail)
            Else
                common.clsCommon.MyMessageBoxShow("Please Enter the proper format of e-mail address")
                txtEmail.Text = ""
                txtEmail.Focus()
                txtEmail.Select()
                Errorcontrol.SetError(txtEmail, "Please Enter the proper format of e-mail address")
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
                common.clsCommon.MyMessageBoxShow("Please Enter the proper format of e-mail address")
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

    Private Sub chkInActive_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkInActive.ToggleStateChanged
        If chkInActive.Checked = True Then
            dtClosing.Enabled = True
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
        SaveData()
    End Sub
    Sub SaveData()
        Try
            If clsCommon.CompairString(btnsave.Text, "Update") = CompairStringResult.Equal Then
                If MyBase.isUpdateFlag = False Then
                    clsCommon.MyMessageBoxShow("Don't have permission to update VSP Master.")
                    Return
                End If
            End If

            If AllowVSPMasterAutoPrefix = 0 Then
                If fndvendorNo.Value = "" Then
                    myMessages.blankValue("Please Fill VSP Code")
                    pageCus.SelectedPage = RadPageViewPage1
                    fndvendorNo.Focus()
                    fndvendorNo.Select()
                    Errorcontrol.SetError(fndvendorNo, "Please Fill VSP Code")
                    Return
                Else
                    Errorcontrol.ResetError(fndvendorNo)
                End If

            End If
            If txtvendorname.Text = "" Then
                myMessages.blankValue("Please Fill VSP Name")
                pageCus.SelectedPage = RadPageViewPage1
                txtvendorname.Focus()
                txtvendorname.Select()
                Errorcontrol.SetError(txtvendorname, "Please Fill VSP Name")
                Return
            Else
                Errorcontrol.ResetError(txtvendorname)
            End If

            If fndgroupcode.Value = "" Then
                myMessages.blankValue("Please Select Group Code")
                pageCus.SelectedPage = RadPageViewPage1
                fndgroupcode.Focus()
                fndgroupcode.Select()
                Errorcontrol.SetError(fndgroupcode, "Please Select Group Code")
                Return
            Else
                Errorcontrol.ResetError(fndgroupcode)
            End If

            If clsCommon.myLen(txtAdd1.Text) <= 0 AndAlso clsCommon.myLen(txtAdd2.Text) <= 0 AndAlso clsCommon.myLen(txtAdd3.Text) <= 0 Then
                myMessages.blankValue("Please Fill Address")
                pageCus.SelectedPage = RadPageViewPage1
                txtAdd1.Focus()
                txtAdd2.Select()
                Errorcontrol.SetError(txtAdd1, "Please Fill Address")
                Errorcontrol.SetError(txtAdd2, "Please Fill Address")
                Errorcontrol.SetError(txtAdd3, "Please Fill Address")
                Return
            Else
                Errorcontrol.ResetError(txtAdd1)
                Errorcontrol.ResetError(txtAdd2)
                Errorcontrol.ResetError(txtAdd3)
            End If

            If ChkIsMP.Checked Then
                If clsCommon.myLen(FndMPCode.Value) <= 0 Then
                    myMessages.blankValue("Please Fill MP Code")
                    pageCus.SelectedPage = RadPageViewPage2
                    FndMPCode.Focus()
                    FndMPCode.Select()
                    Errorcontrol.SetError(FndMPCode, "Please Fill MP Code")
                    Return
                Else
                    Errorcontrol.ResetError(FndMPCode)
                End If
            End If

            If clsCommon.myLen(txtcountrycode) <= 0 Then
                clsCommon.MyMessageBoxShow("Please Select Country", Me.Text)
                pageCus.SelectedPage = RadPageViewPage1
                txtcountrycode.Select()
                txtcountrycode.Focus()
                Errorcontrol.SetError(txtcountrycode, "Please Select Country")
                Return
            Else
                Errorcontrol.ResetError(txtcountrycode)
            End If

            If clsCommon.myLen(txtstatecode) <= 0 Then
                clsCommon.MyMessageBoxShow("Please Select State", Me.Text)
                pageCus.SelectedPage = RadPageViewPage1
                txtstatecode.Select()
                txtstatecode.Focus()
                Errorcontrol.SetError(txtstatecode, "Please Select State")
                Return
            Else
                Errorcontrol.ResetError(txtstatecode)
            End If

            If clsCommon.myLen(txtCity) <= 0 Then
                clsCommon.MyMessageBoxShow("Please Select City", Me.Text)
                pageCus.SelectedPage = RadPageViewPage1
                txtCity.Select()
                txtCity.Focus()
                Errorcontrol.SetError(txtcountrycode, "Please Select City")
                Return
            Else
                Errorcontrol.ResetError(txtCity)
            End If
            ' Modify By : Prabhakar Ref Ticket : BM00000010125
            If clsCommon.myLen(TxtPinCode.Text) > 0 Then
                If clsCommon.myLen(TxtPinCode.Text) <> 6 Then
                    pageCus.SelectedPage = RadPageViewPage1
                    clsCommon.MyMessageBoxShow("Invalid Pin Code.Please Enter Pin Code 6 Digit", Me.Text)
                    Errorcontrol.SetError(TxtPinCode, "Invalid Pin Code.Please Enter Pin Code 6 Digit")
                    Return
                Else
                    Errorcontrol.ResetError(TxtPinCode)
                End If
            Else
                Errorcontrol.ResetError(TxtPinCode)

            End If






            'If clsCommon.myLen(txtpaymnt_cmsn.Text) <= 0 Then
            '    clsCommon.MyMessageBoxShow("Please Fill Payment Commision Percentage", Me.Text)
            '    pageCus.SelectedPage = RadPageViewPage1
            '    txtpaymnt_cmsn.Focus()
            '    txtpaymnt_cmsn.Select()
            '    Errorcontrol.SetError(txtpaymnt_cmsn, "Please Fill Payment Service Percentage")
            '    Return
            'Else
            '    Errorcontrol.ResetError(txtpaymnt_cmsn)
            'End If

            If cmbvsppayment.SelectedValue = "" Then
                clsCommon.MyMessageBoxShow("Please Select VSP Payment Value", Me.Text)
                pageCus.SelectedPage = RadPageViewPage1
                cmbvsppayment.Select()
                Errorcontrol.SetError(cmbvsppayment, "Please Select VSP Payment Value")
                Return
            Else
                Errorcontrol.ResetError(cmbvsppayment)
            End If

            If fndTrmsCode.Value = "" Then
                myMessages.blankValue("Please Fill Terms Code")
                pageCus.SelectedPage = RadPageViewPage4
                fndTrmsCode.Focus()
                fndTrmsCode.Select()
                Errorcontrol.SetError(fndTrmsCode, "Please Fill Terms Code")
                Return
            Else
                Errorcontrol.ResetError(fndTrmsCode)
            End If

            If fndAccntSet.Value = "" Then
                myMessages.blankValue("Please Select Account Set")
                pageCus.SelectedPage = RadPageViewPage4
                fndAccntSet.Focus()
                fndAccntSet.Select()
                Errorcontrol.SetError(fndAccntSet, "Please Select Account Set")
                Return
            Else
                Errorcontrol.ResetError(fndAccntSet)
            End If

            If EnableBankFromMaster = True Then
                If clsCommon.myLen(findfndbankcode.Value) = 0 Then
                    myMessages.blankValue("Please Enter Bank Code")
                    pageCus.SelectedPage = RadPageViewPage4
                    findfndbankcode.Focus()
                    findfndbankcode.Select()
                    Errorcontrol.SetError(findfndbankcode, "Please Enter Bank Code")
                    Return
                Else
                    Errorcontrol.ResetError(findfndbankcode)
                End If

                If clsCommon.myLen(findTxtIFSCCode.Value) = 0 Then
                    myMessages.blankValue("Please Enter IFSC Code")
                    pageCus.SelectedPage = RadPageViewPage4
                    findTxtIFSCCode.Focus()
                    findTxtIFSCCode.Select()
                    Errorcontrol.SetError(findTxtIFSCCode, "Please Enter IFSC Code")
                    Return
                Else
                    Errorcontrol.ResetError(findTxtIFSCCode)
                End If

            Else
                If clsCommon.myLen(fndbankcode.Text) = 0 Then
                    myMessages.blankValue("Please Enter Bank Code")
                    pageCus.SelectedPage = RadPageViewPage4
                    fndbankcode.Focus()
                    fndbankcode.Select()
                    Errorcontrol.SetError(fndbankcode, "Please Enter Bank Code")
                    Return
                Else
                    Errorcontrol.ResetError(fndbankcode)
                End If

                ''richa agarwal 27/03/2015
                If clsCommon.myLen(TxtIFSCCode.Text) = 0 Then
                    myMessages.blankValue("Please Enter IFSC Code")
                    pageCus.SelectedPage = RadPageViewPage4
                    TxtIFSCCode.Focus()
                    TxtIFSCCode.Select()
                    Errorcontrol.SetError(TxtIFSCCode, "Please Enter IFSC Code")
                    Return
                Else
                    Errorcontrol.ResetError(TxtIFSCCode)
                End If
                ''--------------------
            End If
            If fndTxGrp.Value = "" Then
                myMessages.blankValue("Please Select Tax Group")
                pageCus.SelectedPage = RadPageViewPage4
                fndTxGrp.Focus()
                fndTxGrp.Select()
                Errorcontrol.SetError(fndTxGrp, "Please Select Tax Group")
                Return
            Else
                Errorcontrol.ResetError(fndTxGrp)
            End If


            '' validation for multicurrency
            fndVendorCurrency.Value = IIf(objCommonVar.IsMultiCurrencyCompany = False, objCommonVar.BaseCurrencyCode, clsCommon.myCstr(fndVendorCurrency.Value))
            If clsCommon.myLen(clsCommon.myCstr(fndVendorCurrency.Value)) > 0 Then
                If clsCommon.myLen(Me.fndAccntSet.Value) <= 0 Then
                    clsCommon.MyMessageBoxShow("Select Account Set.")
                    Me.fndAccntSet.Focus()
                    Exit Sub
                End If

                If clsCommon.myLen(Me.fndTxGrp.Value) <= 0 Then
                    clsCommon.MyMessageBoxShow("Select Tax Group.")
                    Me.fndTxGrp.Focus()
                    Exit Sub
                End If
                Dim qry As String
                qry = "select CURRENCY_CODE from TSPL_VENDOR_ACCOUNT_SET where Acct_Set_Code='" & clsCommon.myCstr(Me.fndAccntSet.Value) & "' "
                Dim accCurrCode As String = clsDBFuncationality.getSingleValue(qry).ToString
                If clsCommon.CompairString(accCurrCode, clsCommon.myCstr(Me.fndVendorCurrency.Value)) <> CompairStringResult.Equal Then
                    clsCommon.MyMessageBoxShow("Account Set Currency and Vendor Currency must be same in case of Multicurrency.")
                    Exit Sub
                End If
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


            If clsCommon.CompairString(cmbincentive.SelectedValue, "Qty") = CompairStringResult.Equal AndAlso (clsCommon.myLen(txtno_days.Text) <= 0 Or clsCommon.myCdbl(txtno_days.Text) <= 0) Then
                clsCommon.MyMessageBoxShow("Please Enter No.Of Days For Incentive", Me.Text)
                txtno_days.Focus()
                txtno_days.Select()
                Errorcontrol.SetError(txtno_days, "Please Enter No.Of Days For Incentive")
                Return
            Else
                Errorcontrol.ResetError(txtno_days)
            End If

            If clsCommon.CompairString(cmbvsppayment.SelectedValue, "Different") = CompairStringResult.Equal AndAlso clsCommon.myLen(txtpayeename.Text) <= 0 Then
                clsCommon.MyMessageBoxShow("Please Enter VSP Payment Payee Name", Me.Text)
                txtpayeename.Focus()
                txtpayeename.Select()
                Errorcontrol.SetError(txtpayeename, "Please Enter VSP Payment Payee Name")
                Return
            Else
                Errorcontrol.ResetError(txtpayeename)
            End If

            If clsCommon.myLen(txtpan.Text) > 0 Then
                Dim qry As String = "select count(*) from tspl_vendor_master where PAN='" + clsCommon.myCstr(txtpan.Text) + "' and Form_Type='VSP'"
                Dim check As Integer = clsDBFuncationality.getSingleValue(qry)

                If clsCommon.CompairString(btnsave.Text, "Save") = CompairStringResult.Equal AndAlso check > 0 Then
                    clsCommon.MyMessageBoxShow("Pan No. You Entered Is Already Exist,Please Enter Valid,Unique Pan No.", Me.Text)
                    pageCus.SelectedPage = RadPageViewPage4
                    txtpan.Focus()
                    txtpan.Select()
                    Errorcontrol.SetError(txtpan, "Pan No. You Entered Is Already Exist,Please Enter Valid,Unique Pan No.")
                    Return
                Else
                    Errorcontrol.ResetError(txtpan)
                End If

                If clsCommon.CompairString(btnsave.Text, "Save") <> CompairStringResult.Equal AndAlso check > 1 Then
                    clsCommon.MyMessageBoxShow("Pan No. You Entered Is Already Exist,Please Enter Valid,Unique Pan No.", Me.Text)
                    pageCus.SelectedPage = RadPageViewPage4
                    txtpan.Focus()
                    txtpan.Select()
                    Errorcontrol.SetError(txtpan, "Pan No. You Entered Is Already Exist,Please Enter Valid,Unique Pan No.")
                    Return
                Else
                    Errorcontrol.ResetError(txtpan)
                End If

                If clsCommon.myLen(txtpan.Text) > 0 Then
                    If Not checkPan.IsMatch(txtpan.Text) Then
                        txtpan.Focus()
                        Throw New Exception("Please check ! PAN numbers need to have 5 characters followed by 4 numbers then a final character")
                    End If
                End If
            End If
            If clsCommon.myLen(txtcountrycode.Value) <= 0 Then
                clsCommon.MyMessageBoxShow("Please Select Country", Me.Text)
                pageCus.SelectedPage = RadPageViewPage1
                txtcountrycode.Focus()
                txtcountrycode.Select()
                Errorcontrol.SetError(txtCountry, "Please Select Country")
                Return
            Else
                Errorcontrol.ResetError(txtCountry)
            End If

            If clsCommon.myLen(txtstatecode.Value) <= 0 Then
                clsCommon.MyMessageBoxShow("Please Select State", Me.Text)
                pageCus.SelectedPage = RadPageViewPage1
                txtstatecode.Focus()
                txtstatecode.Select()
                Errorcontrol.SetError(txtState, "Please Select State")
                Return
            Else
                Errorcontrol.ResetError(txtState)
            End If


            If clsCommon.myLen(fndCity.Value) <= 0 Then
                clsCommon.MyMessageBoxShow("Please Select City", Me.Text)
                pageCus.SelectedPage = RadPageViewPage1
                fndCity.Focus()
                fndCity.Select()
                Errorcontrol.SetError(txtCity, "Please Select City")
                Return
            Else
                Errorcontrol.ResetError(txtCity)
            End If
            Dim totcharge As Decimal = 0
            For Each row As GridViewRowInfo In gvCharges.Rows
                totcharge += row.Cells("COLRate").Value
            Next
            If txtvendorname.Text = "" Then
                myMessages.blankValue("Please Fill VSP Name")
                pageCus.SelectedPage = RadPageViewPage1
                txtvendorname.Focus()
                txtvendorname.Select()
                Errorcontrol.SetError(txtvendorname, "Please Fill VSP Name")
                Return
            Else
                Errorcontrol.ResetError(txtvendorname)
            End If
            If clsCommon.myLen(txtjointname.Text) <= 0 And clsCommon.myCstr(cmbvsppayment.Text) = "Different" Then
                clsCommon.MyMessageBoxShow("Please Fill Joint Name", Me.Text)
                pageCus.SelectedPage = RadPageViewPage1
                txtjointname.Focus()
                txtjointname.Select()
                Errorcontrol.SetError(txtjointname, "Please Fill Joint Name")
                Return
            Else
                Errorcontrol.ResetError(txtjointname)
            End If
            If EnableBankFromMaster = True Then
                If clsCommon.myLen(txtjointname.Text) > 0 And clsCommon.myLen(txtBankCode.Value) <= 0 Then
                    clsCommon.MyMessageBoxShow("Please Fill Joint Bank Details", Me.Text)
                    pageCus.SelectedPage = RadPageViewPage1
                    txtBankCode.Focus()
                    txtBankCode.Select()
                    Errorcontrol.SetError(txtBankCode, "Please Fill Joint Bank Details")
                    Return
                Else
                    Errorcontrol.ResetError(txtBankCode)
                End If
            Else
                If clsCommon.myLen(txtjointname.Text) > 0 And clsCommon.myLen(texttxtBankCode.Text) <= 0 Then
                    clsCommon.MyMessageBoxShow("Please Fill Joint Bank Details", Me.Text)
                    pageCus.SelectedPage = RadPageViewPage1
                    txtBankCode.Focus()
                    txtBankCode.Select()
                    Errorcontrol.SetError(texttxtBankCode, "Please Fill Joint Bank Details")
                    Return
                Else
                    Errorcontrol.ResetError(texttxtBankCode)
                End If
            End If
            If clsCommon.myLen(txtRateHeadLoad.Text) <= 0 And ChkHeadLoad.Checked = True Then
                clsCommon.MyMessageBoxShow("Please Fill Head Load Rate", Me.Text)
                pageCus.SelectedPage = RadPageViewPage2
                txtRateHeadLoad.Focus()
                txtRateHeadLoad.Select()
                Errorcontrol.SetError(txtRateHeadLoad, "Please Fill Head Load Rate")
                Return
            Else
                Errorcontrol.ResetError(txtRateHeadLoad)
            End If
            If clsCommon.myLen(TxtRateOwnAsset.Text) <= 0 And ChkOwnAsset.Checked = True Then
                clsCommon.MyMessageBoxShow("Please Fill Own Asset Rate", Me.Text)
                pageCus.SelectedPage = RadPageViewPage2
                TxtRateOwnAsset.Focus()
                TxtRateOwnAsset.Select()
                Errorcontrol.SetError(TxtRateOwnAsset, "Please Fill Own Asset Rate")
                Return
            Else
                Errorcontrol.ResetError(TxtRateOwnAsset)
            End If


            Dim qryNatureDed As String = ""
            If ChkIsTDSApp.Checked = True Then
                If clsCommon.myLen(fnddeducNew.Value) > 0 Then
                    qryNatureDed = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Non_PAN_No From TSPL_TDS_DEDUCTION_HEAD where Deduction_Code ='" & clsCommon.myCstr(fnddeducNew.Value) & "'"))
                End If

                If clsCommon.myLen(fnddeducNew.Value) <= 0 Then
                    pageCus.SelectedPage = RadPageViewPage5
                    fnddeducNew.Focus()
                    fnddeducNew.Select()
                    Errorcontrol.SetError(fnddeducNew, "Nature of Deduction can't be left blank")
                    Return
                Else
                    Errorcontrol.ResetError(fnddeducNew)
                End If

                If clsCommon.CompairString(qryNatureDed.ToUpper().Trim(), "Y") = CompairStringResult.Equal AndAlso clsCommon.myLen(txtpan.Text) > 0 Then
                    txtpan.Focus()
                    txtpan.Select()
                    Errorcontrol.SetError(txtpan, "You can not make this entry with Non PAN nature of deduction as PAN No exists.")
                    Return
                Else
                    Errorcontrol.ResetError(txtpan)
                End If
            End If


            If clsCommon.myLen(txtAadharNo.Text) > 0 Then
                If clsCommon.myLen(txtAadharNo.Text) <> 12 Then
                    pageCus.SelectedPage = RadPageViewPage4
                    clsCommon.MyMessageBoxShow("Invalid Aadhar No.Please Enter Aadhar No 12 Digit", Me.Text)
                    Errorcontrol.SetError(txtAadharNo, "Invalid Aadhar No.Please Enter Aadhar No 12 Digit")
                    Return
                Else
                    Errorcontrol.ResetError(txtAadharNo)
                End If
            Else
                Errorcontrol.ResetError(txtAadharNo)

            End If

            If clsCommon.CompairString("Select", cmbservc_type.Text) = CompairStringResult.Equal Then
                For ii As Integer = 0 To 4
                    If clsCommon.myCdbl(gvEMP.Rows(ii).Cells(colEMPValue).Value) > 0 Then
                        Throw New Exception("Please select service basis on EMP Tab")
                    End If
                Next
            End If
            If chkCreditLimitBasedOnMilkReceipt.Checked Then
                If TxtCreditLimitBasedOnMilkReceipt.Value < 0 OrElse TxtCreditLimitBasedOnMilkReceipt.Value > 100 Then
                    TxtCreditLimitBasedOnMilkReceipt.Focus()
                    Throw New Exception("Credit Limit Based% range should be (0-100)")
                End If
            End If



            If txtHandlingChargesPer.Value < 0 OrElse txtHandlingChargesPer.Value > 100 Then
                pageCus.SelectedPage = RadPageViewPage7
                txtHandlingChargesPer.Focus()
                Throw New Exception("Handling Charges(%) range should be (0-100)")
            End If

            If clsCommon.CompairString(clsCommon.myCstr(cboEMPType.SelectedValue), "FP") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(cboEMPType.SelectedValue), "FPSP") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(cboEMPType.SelectedValue), "FAFP") = CompairStringResult.Equal Then
                If clsCommon.myCdbl(gvEMP.Rows(0).Cells(colEMPValue).Value) < 0 Then
                    pageCus.SelectedPage = RadPageViewPage7
                    Throw New Exception("Please provide value of Emp at row No 1")
                End If
                If clsCommon.CompairString(clsCommon.myCstr(cboEMPType.SelectedValue), "FAFP") = CompairStringResult.Equal Then
                    If txtFixedAmount.Value <= 0 Then
                        pageCus.SelectedPage = RadPageViewPage7
                        Throw New Exception("Please provide value of Fill fixed amount")
                    End If
                End If

                If clsCommon.CompairString(clsCommon.myCstr(cboEMPType.SelectedValue), "FPSP") = CompairStringResult.Equal Then
                    If Not (clsCommon.CompairString(cmbservc_type.Text, "Rate/Kg") = CompairStringResult.Equal OrElse clsCommon.CompairString(cmbservc_type.Text, "Rate/Ltr") = CompairStringResult.Equal) Then
                        cmbservc_type.Focus()
                        Throw New Exception("For EMP Type- FPSP(Fixed Percent + Standard Price) ," + cmbservc_type.MyLinkLable1.Text + " should be Rate/Kg or Rate/Ltr")
                    End If
                End If
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboEMPType.SelectedValue), "SWP") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(cboEMPType.SelectedValue), "FASWP") = CompairStringResult.Equal Then
                Dim countZero As Integer = 0
                For ii As Integer = 4 To 0 Step -1
                    If clsCommon.myCdbl(gvEMP.Rows(ii).Cells(colEMPSlab).Value) = 0 Then
                        countZero += 1
                    Else
                        Exit For
                    End If
                Next
                Dim previousSlab As Double = 0
                For ii As Integer = 0 To 4 - countZero
                    If clsCommon.myCdbl(gvEMP.Rows(ii).Cells(colEMPValue).Value) <= 0 Then
                        pageCus.SelectedPage = RadPageViewPage7
                        Throw New Exception("Please Fill emp value at row No " + clsCommon.myCstr(ii + 1))
                    End If
                    If previousSlab >= clsCommon.myCdbl(gvEMP.Rows(ii).Cells(colEMPSlab).Value) Then
                        pageCus.SelectedPage = RadPageViewPage7
                        Throw New Exception("Please Fill slab in increase order at row no " + clsCommon.myCstr(ii + 1))
                    End If
                    previousSlab = clsCommon.myCdbl(gvEMP.Rows(ii).Cells(colEMPSlab).Value)
                Next
                If clsCommon.CompairString(clsCommon.myCstr(cboEMPType.SelectedValue), "FASWP") = CompairStringResult.Equal Then
                    If txtFixedAmount.Value <= 0 Then
                        pageCus.SelectedPage = RadPageViewPage7
                        Throw New Exception("Please provide value of Fill fixed amount")
                    End If
                End If
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboEMPType.SelectedValue), "FAFP") = CompairStringResult.Equal Then
                If clsCommon.myCdbl(gvEMP.Rows(0).Cells(colEMPValue).Value) < 0 Then
                    pageCus.SelectedPage = RadPageViewPage7
                    Throw New Exception("Please provide value of Emp at row No 1")
                End If
                If txtFixedAmount.Value <= 0 Then
                    pageCus.SelectedPage = RadPageViewPage7
                    Throw New Exception("Please provide value of Fill fixed amount")
                End If
            Else
            End If

            UcCustomFields1.AllowToSave()


            If MyBase.isModifyonPasswordFlag Then
                If clsPasswordCheckForMasters.CheckMasterPwd(clsUserMgtCode.frmVSPMaster, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
                Else
                    Return
                End If
            End If


            If objCommonVar.GSTApplicable Then
                If clsCommon.myCdbl(Rchkregistered.Checked) > 0 Then
                    Dim GSTFinal As String = clsCommon.myCstr(txtGSTStateCode.Text) + clsCommon.myCstr(txtGST_PanCode.Text) + clsCommon.myCstr(txtEntity.Text) + clsCommon.myCstr(txtFixxed.Text) + clsCommon.myCstr(MyTextBox2.Text)
                    txtGSTIN_No_final.Text = GSTFinal
                    clsERPFuncationality.ValidationGSTNO(txtGSTStateCode.Text, txtpan.Text, GSTFinal, Nothing)
                End If
            End If



            If btnsave.Text = "Save" Then
                funinsert()
                funInsertCharges()
                isLoadCopy = False
            ElseIf btnsave.Text = "Update" Then
                funupdate()
                funInsertCharges()
                isLoadCopy = False
            End If


        Catch ex As Exception
            myMessages.myExceptions(ex)

        End Try
    End Sub

    Public Sub funInsertCharges()
        Dim obj As clsVendorMaster = Nothing
        Dim ObjV As New List(Of clsVendorMaster)
        For Each row As GridViewRowInfo In gvCharges.Rows
            If clsCommon.myLen(row.Cells("COLChargeCode").Value) > 0 Then
                obj = New clsVendorMaster
                obj.Charge_CODE = clsCommon.myCstr(row.Cells("COLChargeCode").Value)
                obj.Rate = clsCommon.myCdbl(row.Cells("COLRate").Value)
                obj.Updated_date = clsCommon.GETSERVERDATE()
                obj.VSP_CODE = clsCommon.myCstr(fndvendorNo.Value)
                ObjV.Add(obj)
            End If
        Next
        If ObjV.Count > 0 Then
            clsVendorMaster.Save_VSP_Data(obj.VSP_CODE, ObjV, Nothing)
        End If
    End Sub


    Public Sub LoadCharges()
        gvCharges.Rows.Clear()
        IsInsieLoadData = True
        'Dim obj As clsVendorMaster
        Dim ObjV As New List(Of clsVendorMaster)
        ObjV = clsVendorMaster.GetChargesData(fndvendorNo.Value, NavigatorType.Current)
        If Not IsNothing(ObjV) AndAlso ObjV.Count > 0 Then
            For Each obj1 As clsVendorMaster In ObjV
                gvCharges.Rows.AddNew()
                gvCharges.Rows(gvCharges.Rows.Count - 1).Cells("COlChargeCode").Value = obj1.Charge_CODE
                gvCharges.Rows(gvCharges.Rows.Count - 1).Cells("COlChargeDesc").Value = obj1.Charge_Desc
                ' gvCharges.Rows(gvCharges.Rows.Count - 1).Cells("COlGLCode").Value = obj1.GL_CODE
                'gvCharges.Rows(gvCharges.Rows.Count - 1).Cells("COlGLDesc").Value = obj1.GL_DESC
                gvCharges.Rows(gvCharges.Rows.Count - 1).Cells("COlRate").Value = obj1.Rate
            Next
        Else
            gvCharges.Rows.AddNew()
        End If
        IsInsieLoadData = False
    End Sub

    Private Sub btndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click
        DeleteData()
    End Sub
    Sub DeleteData()
        If fndvendorNo.Value = "" Then
            myMessages.blankValue("VSP No.")
        ElseIf myMessages.deleteConfirm() Then
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
        'funExport()
    End Sub

    Private Sub MenuImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuImport.Click
        'funImport()
    End Sub
    Private Sub RadMenuItem4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem4.Click
        Me.Close()
    End Sub
#End Region


    Private Sub frmVSPMaster_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
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
    End Sub

    Private Sub fndvendorNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndvendorNo._MYValidating
        If isButtonClicked Then
            'Dim qry As String = "select Vendor_Code as [Vendor Code],Vendor_Name as [Vendor Name],Vendor_Group_Code as [Vendor Group],(select case when Status='N' then 'Active' else 'In Active' end ) as [Status] from TSPL_VENDOR_MASTER "

            'fndvendorNo.Value = clsCommon.ShowSelectForm("fmVedrNofnd", qry, "Vendor Code", "", fndvendorNo.Value, "", isButtonClicked)
            fndvendorNo.Value = clsVendorMaster.getFinder(" form_type='VSP'", fndvendorNo.Value, isButtonClicked)

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
                txtaccsetdes.Text = ""
                txtvendorname.Text = ""
                txtvendortypedes.Text = ""
                txtWeb.Text = ""
                txtTxGrp.Text = ""
                txtTinNo.Text = ""
                LblIncentive.Text = ""
                FndIncentive.Value = Nothing
                txttermcodedes.Text = ""
                txtStaxNo.Text = ""
                txtState.Text = ""
                txtRemarks2.Text = ""
                txtRemarks1.Text = ""
                txtrange.Text = ""
                txtPhone2.Text = "(+__)__________"
                txtPhone1.Text = "(+__)__________"
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
                txtContPhone.Text = "(+__)__________"
                txtContactWeb.Text = ""
                txtContactName.Text = ""
                txtContactFax.Text = ""
                txtAdd1.Text = ""
                txtAdd2.Text = ""
                txtAdd3.Text = ""
                txtAddInfo1.Text = ""
                txtAddInfo2.Text = ""
                txtAddInfo3.Text = ""
                txtbankcodedes.Text = ""
                txtCity.Text = ""
                txtcollect.Text = ""
                txtContactEmail.Text = ""
                chkHold.Checked = False
                chkInActive.Checked = False
                chkInterBranch.Checked = False
                chkTagAsFranchise.Checked = False
                chkIsGrossReceipt.Checked = False
                chktrarns.Checked = False
                fndgroupcode.Value = Nothing
                fndCity.Value = Nothing
                ChkHeadLoad.Checked = False
                txtRateHeadLoad.Text = Nothing
                txtDistanceKMHeadLoad.Text = Nothing
                TxtStandardSec_Amt.Text = Nothing
                CmbHeadLoadServiceBasis.SelectedValue = -1
                ChkOwnAsset.Checked = False
                TxtRateOwnAsset.Text = Nothing
                CmbOwnAssetServiceBasis.SelectedValue = -1
                If AllowVSPMasterAutoPrefix = 1 Then
                    fndvendorNo.Value = ""
                End If
                btnsave.Text = "Save"
            Else
                fndvendorNo_text_changed()
            End If
        End If
    End Sub

    Private Sub fndgroupcode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndgroupcode._MYValidating
        ' If isButtonClicked Then
        'Dim qry As String = "SELECT ven_Group_Code as [VendorGroupCode],Group_Desc as [Description],Tax_Group_Code as [Tax Group],Acct_Set_Code as [Account Set],Terms_Code as [Terms Code] FROM [TSPL_VENDOR_GROUP]  "
        'fndgroupcode.Value = clsCommon.ShowSelectForm("grcodefmfnd", qry, "VendorGroupCode", "", fndgroupcode.Value, "", isButtonClicked)
        fndgroupcode.Value = clsVendorGroupMaster.getFinder("", fndgroupcode.Value, isButtonClicked)
        'txtvendorname.Text = clsDBFuncationality.getSingleValue("Select group_desc from tspl_vendor_group where ven_group_code='" + fndgroupcode.Value + "'")
        fndgroupcode_text_Changed()
        fndgroupcode_leave()
        '   End If
    End Sub

    Private Sub fndCity__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndCity._MYValidating

        fndCity.Value = clsCityMaster.getFinder("", fndCity.Value, isButtonClicked)
        'txtCity.Text = clsDBFuncationality.getSingleValue("Select City_Name from TSPL_CITY_MASTER where City_Code='" + fndCity.Value + "' and state_code='" + txtstatecode.Value + "'")
        If clsCommon.myLen(fndCity.Value) > 0 Then
            Dim obj As clsCityMaster = clsCityMaster.GetData(fndCity.Value, NavigatorType.Current)
            If Not IsNothing(obj) Then
                txtCity.Text = obj.City_Name
                txtstatecode.Value = obj.STATE_CODE
                txtState.Text = obj.STATE_NAME
                txtcountrycode.Value = clsDBFuncationality.getSingleValue("select country_code from tspl_State_master where state_Code='" & txtstatecode.Value & "'")
                txtCountry.Text = clsDBFuncationality.getSingleValue("select country_name from tspl_Country_master where Country_Code='" & txtcountrycode.Value & "'")
            End If
            'txtCityName.Text = clsDBFuncationality.getSingleValue("select  city_name from tspl_city_master where city_code='" & fndCity.Value & "'")
        Else
            txtCity.Text = ""
            fndCity.Value = ""
        End If

    End Sub

    Private Sub fndvendorNo__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavigatorType As common.NavigatorType) Handles fndvendorNo._MYNavigator
        Dim qst As String = "select Vendor_Code as [Vendor Code],Vendor_Name as [Vendor Name],Vendor_Group_Code as [Vendor Group],(select case when Status='N' then 'Active' else 'In.Active' end ) as [Status] from TSPL_VENDOR_MASTER    where  2=2 and form_type='VSP'"
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

    'Private Sub fndbankcode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean)
    '    'fndbankcode.ConnectionString = connectSql.SqlCon()
    '    ''fndbankcode.Query = " select bank_code As [Bank Code],description  as [Description]from TSPL_Bank_MASTER "
    '    'fndbankcode.Query = clsERPFuncationality.glbankquery
    '    'fndbankcode.ValueToSelect = "Bank Code"
    '    'fndbankcode.Caption = "Bank Master"
    '    'fndbankcode.ValueToSelect1 = "Description"
    '    GetBankDetails(isButtonClicked)
    '    'Dim whrcls As String = ""
    '    'Dim qry As String = clsERPFuncationality.glbankquery(whrcls)
    '    'fndbankcode.Value = clsCommon.ShowSelectForm("fndbannk", qry, "Bank Code", whrcls, fndbankcode.Value, "", isButtonClicked)
    '    'txtbankcodedes.Text = clsDBFuncationality.getSingleValue("Select Description from TSPL_BANK_MASTER where BANK_CODE='" + fndbankcode.Value + "'")

    'End Sub

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
                txtState.Text = ""
                txtCity.Text = ""
                fndCity.Value = ""
                txtstatecode.Value = ""
            Else
                txtState.Text = ""
                txtCity.Text = ""
                fndCity.Value = ""
                txtCountry.Text = ""
                txtstatecode.Value = ""
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub txtstatecode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtstatecode._MYValidating
        If clsCommon.myLen(txtcountrycode.Value) <= 0 Then
            clsCommon.MyMessageBoxShow("Please Select Country First", Me.Text)
            txtcountrycode.Focus()
            txtcountrycode.Select()
            Return
        End If

        Try
            Dim qry As String = "select State_code as Code,state_name as State from tspl_state_master"
            txtstatecode.Value = clsCommon.ShowSelectForm("STAFND", qry, "Code", " country_code='" + txtcountrycode.Value + "'", txtcountrycode.Value, "Code", isButtonClicked)

            If clsCommon.myLen(txtstatecode.Value) > 0 Then
                txtState.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select state_name from tspl_state_master where state_code='" + txtstatecode.Value + "'"))
                txtGSTStateCode.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select GST_State_Code from tspl_state_master where state_code='" + txtstatecode.Value + "'"))
                txtCity.Text = ""
                fndCity.Value = ""
            Else
                txtState.Text = ""
                txtGSTStateCode.Text = ""
                txtCity.Text = ""
                fndCity.Value = ""
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try

    End Sub



    Private Sub txtcommpers_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtcommpers.Validating
        'Try
        '    Convert.ToDecimal(txtcommpers.Text)
        '    Errorcontrol.ResetError(txtcommpers)
        'Catch ex As Exception
        '    clsCommon.MyMessageBoxShow("Commision Percentage Should Be Numeric", Me.Text)
        '    txtcommpers.Text = "0"
        '    txtcommpers.Focus()
        '    txtcommpers.Select()
        '    Errorcontrol.SetError(txtcommpers, "Commision Percentage Should Be Numeric")
        'End Try
    End Sub

    Private Sub txtno_days_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtno_days.Validating
        Try
            Convert.ToDecimal(txtno_days.Text)
            Errorcontrol.ResetError(txtno_days)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow("No. Of Days Should Be Numeric", Me.Text)
            txtno_days.Text = "0"
            txtno_days.Focus()
            txtno_days.Select()
            Errorcontrol.SetError(txtno_days, "No. Of Days Should Be Numeric")
        End Try
    End Sub



    Private Sub txtpayeename_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtpayeename.Validating
        If clsCommon.myLen(txtpayeename.Text) > 0 Then
            txtpayeename.Text = txtpayeename.Text.Replace("'", "`")
        End If
    End Sub

    Private Sub cmbincentive_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbincentive.SelectedValueChanged
        Try
            txtno_days.Text = "0"
            txtno_days.Enabled = False
            If clsCommon.CompairString(cmbincentive.SelectedValue, "Qty") = CompairStringResult.Equal Then
                txtno_days.Enabled = True
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub cmbvsppayment_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbvsppayment.SelectedValueChanged
        Try

            If clsCommon.CompairString(cmbvsppayment.SelectedValue, "Different") = CompairStringResult.Equal Then
                'txtpayeename.Enabled = True
                txtjointname.Enabled = True
                'fndVendorCOde.Enabled = True
                Grpjoint.Enabled = True
                If EnableBankFromMaster = True Then
                    texttxtBankCode.Visible = False
                    texttxtBankBranchCode.Visible = False
                    txtBankCode.Visible = True
                    txtBankBranchCode.Visible = True
                Else
                    texttxtBankCode.Visible = True
                    texttxtBankBranchCode.Visible = True
                    txtBankCode.Visible = False
                    txtBankBranchCode.Visible = False
                End If

            Else
                texttxtBankCode.Text = ""
                texttxtBankBranchCode.Text = ""
                txtBankCode.Value = Nothing
                txtBankBranchCode.Value = Nothing
                ' txtpayeename.Text = ""
                txtpayeename.Enabled = False
                txtjointname.Text = ""
                txtjointname.Enabled = False
                txtBankBranchName.Text = Nothing
                txtBankBranchCode.Text = Nothing
                txtBankBranchName.Text = Nothing
                fndBankCity.Text = Nothing
                txtBankCityName.Text = Nothing
                fndBankState.Value = Nothing
                txtBankStateName.Text = Nothing
                TxtIFSCCode.Text = ""
                Grpjoint.Enabled = False
                txtpayeename.Text = txtvendorname.Text
                'fndVendorCOde.Value = ""
                'fndVendorCOde.Enabled = False
            End If
        Catch ex As Exception
        End Try
    End Sub


    Private Sub txtWeb_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtWeb.Leave
        Try
            If clsCommon.myLen(txtWeb.Text) > 0 Then
                If Not txtWeb.Text.ToUpper().Contains("WWW.") Then
                    clsCommon.MyMessageBoxShow("Please Enter Web Site In Proper Format.", Me.Text)
                    txtWeb.Focus()
                    txtWeb.Select()
                    txtWeb.Text = ""
                    Errorcontrol.SetError(txtWeb, "Please Enter Web Site In Proper Format.")
                    Return
                Else
                    Errorcontrol.ResetError(txtWeb)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow("Please Enter Web Site In Proper Format.", Me.Text)
            txtWeb.Focus()
            txtWeb.Select()
            txtWeb.Text = ""
            Errorcontrol.SetError(txtWeb, "Please Enter Web Site In Proper Format.")
        End Try
    End Sub

    Private Sub txtpaymnt_cmsn_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtpaymnt_cmsn.Validating
        'Try
        '    Convert.ToDecimal(txtpaymnt_cmsn.Text)
        '    Errorcontrol.ResetError(txtpaymnt_cmsn)
        'Catch ex As Exception
        '    clsCommon.MyMessageBoxShow("Payment Commision Percentage Should Be Numeric", Me.Text)
        '    txtpaymnt_cmsn.Text = "0"
        '    txtpaymnt_cmsn.Focus()
        '    txtpaymnt_cmsn.Select()
        '    Errorcontrol.SetError(txtpaymnt_cmsn, "Payment Commision Percentage Should Be Numeric")
        'End Try
    End Sub



    'Private Sub fndVendorCOde__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean)
    '    Dim qry As String = "select Vendor_Code as Code,Vendor_Name as Name,Terms_Code as [Term Code] ,Terms_Code_Desc as [Term Description] ,Tax_Group as [Tax Group],Tax_Group_Desc as [Tax Group Description],Add1 as [Address 1],Add2 as [Address 2],Add3 as [Address 3],City_Code_Desc as City,State,Country  from TSPL_VENDOR_MASTER"
    '    fndVendorCOde.Value = clsCommon.ShowSelectForm("POVFND", qry, "Code", "", fndVendorCOde.Value, "Code", isButtonClicked)
    '    ''lblVendorName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Vendor_Name from TSPL_VENDOR_MASTER where Vendor_Code='" + txtVendorNo.Value + "'"))

    '    qry = "select  Vendor_Name from TSPL_VENDOR_MASTER where Vendor_Code ='" + fndVendorCOde.Value + "'"
    '    Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
    '    If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
    '        txtjointname.Text = clsCommon.myCstr(dt.Rows(0)("Vendor_Name"))
    '    Else
    '        txtjointname.Text = ""
    '    End If
    'End Sub

    Private Sub gvCharges_CellValueChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvCharges.CellValueChanged
        Try
            If Not IsInsieLoadData And e.Column Is gvCharges.Columns("COLChargeCode") Then
                Dim str As String = clsItemchargecategorymaster.getFinder("", "", True)
                If str <> "" Then
                    IsInsieLoadData = True
                    gvCharges.CurrentRow.Cells("COLChargeCode").Value = str
                    Dim objCategory As New clsItemchargecategorymaster
                    objCategory = clsItemchargecategorymaster.GetData(str, NavigatorType.Current)
                    If Not IsNothing(objCategory) Then
                        gvCharges.CurrentRow.Cells("COLChargeCode").Value = objCategory.chrcategorycode
                        gvCharges.CurrentRow.Cells("COLChargeDesc").Value = objCategory.chrcategorydesc
                        'gvCharges.CurrentRow.Cells("ColGLCode").Value = objCategory.glacccode
                        'gvCharges.CurrentRow.Cells("ColGLdesc").Value = objCategory.glaccdesc
                    End If
                    IsInsieLoadData = False
                End If

            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.ToString)
        End Try
    End Sub

    Public Sub LoadVisiDetail()
        Try
            ''Inner join TSPL_RGP_DETAIL rd on TSPL_VISI_MASTER.serial_no=rd.serial_no inner join TSPL_RGP_HEAD rh on rh.Doc_Type='Disp' and rh.RGP_No=rd.RGP_No 
            Dim qryLoadVisi As String = "select  tspl_vsPasset_Head.Doc_No as [Doc Code],Doc_date as [Doc Date],Remarks,Comment,Emp_Name as [Request By]," _
            & " TSPL_VSPAsset_DETAIL.Item_Code as [Item Code], Item_desc as [Item Description],case when coalesce(Auto_Sr_No,'')='' then (case when isnull(tspl_vsPasset_Head.Doc_Type,'')='Issue' then TSPL_VSPAsset_DETAIL.Issued_Qty else TSPL_VSPAsset_DETAIL.Issued_Qty_againstret*-1 end) else 1 " _
            & " end as [Issued Qty],Auto_Sr_No as [Serial No],Unit_code as [Unit],TSPL_VSPAsset_DETAIL.Unit_Cost as [Cost],case when coalesce(Auto_Sr_No,'')='' " _
            & " then Amount  else TSPL_VSPAsset_DETAIL.Unit_Cost end as Amount from tspl_vsPasset_Head inner join TSPL_VSPAsset_DETAIL on " _
            & " tspl_vsPasset_Head.Doc_No=TSPL_VSPAsset_DETAIL.Doc_No  left join TSPL_EMPLOYEE_MASTER on EMP_CODE=Request_By Left join TSPL_SERIAL_ITEM on " _
            & " tspl_vsPasset_Head.Doc_No=TSPL_SERIAL_ITEM.Document_Code and TSPL_SERIAL_ITEM.Item_Code=TSPL_VSPAsset_DETAIL.Item_Code and Document_Type='VSPTRAN' where tspl_vsPasset_Head.Status='1' and " _
            & " Issue_To='" & fndvendorNo.Value & "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qryLoadVisi)
            'GvAsset.DataSource = dt
            ' FormatGrid()
            'GvAsset.BestFitColumns()
            'GvAsset.ReadOnly = True
            ''------------------Code Ends Here----------------------------------
            GvAsset.DataSource = Nothing
            If dt IsNot Nothing And dt.Rows.Count > 0 Then
                GvAsset.DataSource = dt
                GvAsset.BestFitColumns()
                'GvAsset.ShowFilteringRow = True
                GvAsset.GroupDescriptors.Clear()
                GvAsset.ShowGroupPanel = False
                GvAsset.MasterTemplate.SummaryRowsBottom.Clear()
                For ii As Integer = 0 To GvAsset.Columns.Count - 1
                    GvAsset.Columns(ii).ReadOnly = True
                    GvAsset.Columns(ii).BestFit()
                Next
                'GvAsset.AutoSizeRows = True
                GvAsset.EnableFiltering = True
                GvAsset.ShowFilteringRow = True
                GvAsset.AllowAddNewRow = False
                Dim summaryRowItem As New GridViewSummaryRowItem()
                Dim item1 As New GridViewSummaryItem("Issued Qty", "", GridAggregateFunction.Sum)
                summaryRowItem.Add(item1)
                GvAsset.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
            End If


        Catch ex As Exception

        End Try
    End Sub

    Private Function GetItemType(Optional ByVal istype As Boolean = False) As DataTable
        Dim dt As New DataTable()
        If istype = True Then
            dt.Columns.Add("Code", GetType(String))

            Dim dr As DataRow = dt.NewRow()
            dr("Code") = "New"
            dt.Rows.Add(dr)

            dr = dt.NewRow()
            dr("Code") = "Refurnished"
            dt.Rows.Add(dr)
        Else
            dt.Columns.Add("Code", GetType(String))

            Dim dr As DataRow = dt.NewRow()
            dr("Code") = "Yes"
            dt.Rows.Add(dr)

            dr = dt.NewRow()
            dr("Code") = "No"
            dt.Rows.Add(dr)
        End If

        Return dt
    End Function

    Private Sub DtpBillingDate_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles DtpBillingDate.Leave
        Try
            DtpEndBillingDate.Value = DtpBillingDate.Value.AddDays(2)
        Catch ex As Exception
        End Try
    End Sub



    Private Sub txtjointname_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtjointname.Leave
        'If Not A Matching Format Entered
        If Not Regex.Match(txtjointname.Text, "^[a-zA-Z ]*$", RegexOptions.IgnorePatternWhitespace).Success Then 'Only Letters
            clsCommon.MyMessageBoxShow("Please Enter Alphabetic Characters Only!") 'Inform User
            txtjointname.Focus() 'Return Focus
            txtjointname.Clear() 'Clear TextBox
            txtpayeename.Text = txtvendorname.Text
        Else
            'txtpayeename.Text = txtvendorname.Text & " and " & txtjointname.Text
            txtpayeename.Text = txtvendorname.Text & IIf(clsCommon.myLen(txtjointname.Text) > 0, " and " & txtjointname.Text, "")
        End If
    End Sub



    Private Sub txtBankCode__MYOpenMasterForm(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtBankCode._MYOpenMasterForm
        Frm_Open = New FrmVendorBankMaster
        Frm_Open.SetUserMgmt(clsUserMgtCode.VendorBankMaster)
        Frm_Open.Show()
    End Sub

    Private Sub txtBankCode__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtBankCode._MYValidating
        GetJointBankDetails(isButtonClicked)
    End Sub

    Sub GetJointBankDetails(ByVal isBUttonclicked As Boolean)
        'If clsCommon.myLen(txtBankCode.Value) > 0 Then
        If isBUttonclicked Then
            txtBankCode.Value = clsVendorBankMaster.GetFinder(IIf(clsCommon.myLen(txtBankCode.Value) > 0, "bank_code='" & txtBankCode.Value & "'", ""), txtBankCode.Value, isBUttonclicked)
        End If
        If clsCommon.myLen(txtBankCode.Value) > 0 Then
            Dim obj As clsVendorBankMaster
            obj = clsVendorBankMaster.GetData(txtBankCode.Value, NavigatorType.Current)
            If Not IsNothing(obj) Then
                TxtJointBankName.Text = obj.Bank_Name
                'txtBankBranchCode.Value = obj.Branch_Code
                txtBankBranchName.Text = obj.Branch_Name
                fndBankCity.Value = obj.city_code
                txtBankCityName.Text = obj.city_name
                fndBankState.Value = obj.state_code
                txtBankStateName.Text = obj.state_name
                'txtIFCICode.Text = obj.IFSC_Code
            End If
        Else
            txtBankBranchName.Text = ""
        End If
        'Else
        'If isBUttonclicked Then
        '    clsCommon.MyMessageBoxShow("Please Select Bank First")
        'End If
        'txtBankCode.Focus()
        'End If
    End Sub

    Sub GetBankDetails(ByVal isBUttonclicked As Boolean)
        'If clsCommon.myLen(txtBankCode.Value) > 0 Then
        If isBUttonclicked Then
            findfndbankcode.Value = clsVendorBankMaster.GetFinder("", findfndbankcode.Value, isBUttonclicked)
        End If
        If clsCommon.myLen(findfndbankcode.Value) > 0 Then
            Dim obj As clsVendorBankMaster
            obj = clsVendorBankMaster.GetData(findfndbankcode.Value, NavigatorType.Current)
            If Not IsNothing(obj) Then
                txtbankcodedes.Text = obj.Bank_Name
                TxtBankName.Text = obj.Bank_Name
                ' txtBankBranchCode.Value = obj.Branch_Code
                TxtBankBranch.Text = obj.Branch_Name
                'fndBankCity.Value = obj.city_code
                'txtBankCityName.Text = obj.city_name
                'fndBankState.Value = obj.state_code
                'txtBankStateName.Text = obj.state_name
                'TxtIFSCCode.Value = obj.IFSC_Code
            End If
        Else
            txtBankBranchName.Text = ""
        End If
        'Else
        'If isBUttonclicked Then
        '    clsCommon.MyMessageBoxShow("Please Select Bank First")
        'End If
        'txtBankCode.Focus()
        'End If
    End Sub

    Private Sub cmbagreemnt_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.Data.PositionChangedEventArgs) Handles cmbagreemnt.SelectedIndexChanged
        If clsCommon.CompairString(cmbagreemnt.Text, "YES") = CompairStringResult.Equal Then
            txtagrmnt_date.Enabled = True
            txtexpir_date.Enabled = True
            txtagrmnt_date.MendatroryField = True
            txtexpir_date.MendatroryField = True
        Else
            txtagrmnt_date.Enabled = False
            txtexpir_date.Enabled = False
            txtagrmnt_date.MendatroryField = False
            txtexpir_date.MendatroryField = False
            txtagrmnt_date.Text = clsCommon.GETSERVERDATE()
            txtexpir_date.Text = clsCommon.GETSERVERDATE()
        End If
    End Sub

    Private Sub txtvendorname_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtvendorname.Leave
        txtpayeename.Text = txtvendorname.Text & IIf(clsCommon.myLen(txtjointname.Text) > 0, " and " & txtjointname.Text, "")
    End Sub

    Private Sub fndpaymentCycle__MYOpenMasterForm(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndpaymentCycle._MYOpenMasterForm
        Frm_Open = New frmPaymentCycleMaster
        Frm_Open.SetUserMgmt(clsUserMgtCode.frmPaymentCycleMaster)
        Frm_Open.Show()
    End Sub

    Private Sub fndpaymentCycle__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndpaymentCycle._MYValidating
        Try
            GetPaymentCycleData(isButtonClicked)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.ToString)
        End Try
    End Sub

    Sub GetPaymentCycleData(ByVal isButtonClicked As Boolean)
        If isButtonClicked Then
            fndpaymentCycle.Value = clsPaymentCycleMaster.getFinder(IIf(clsCommon.myLen(fndpaymentCycle.Value) > 0, "TSPL_PAYMENT_CYCLE_MASTER.PC_CODE='" & fndpaymentCycle.Value & "'", ""), fndpaymentCycle.Value, isButtonClicked)
        End If
        If clsCommon.myLen(fndpaymentCycle.Value) > 0 Then
            Dim obj As clsPaymentCycleMaster = clsPaymentCycleMaster.GetData(fndpaymentCycle.Value, NavigatorType.Current)
            TxtPaymentCycle.Text = obj.Description
        End If
    End Sub

    Sub GetIIncentiveDetails(ByVal isBUttonclicked As Boolean)
        'If clsCommon.myLen(txtBankCode.Value) > 0 Then
        If isBUttonclicked Then
            FndIncentive.Value = clsIncentiveMaster.GetFinder("", FndIncentive.Value, isBUttonclicked)
        End If
        If clsCommon.myLen(FndIncentive.Value) > 0 Then
            Dim obj As clsIncentiveMaster
            obj = clsIncentiveMaster.GetData(FndIncentive.Value, NavigatorType.Current)
            If Not IsNothing(obj) Then
                LblIncentive.Text = obj.DESCRIPTION
            End If
        Else
            LblIncentive.Text = ""
            FndIncentive.Value = Nothing
        End If
        'Else
        'If isBUttonclicked Then
        '    clsCommon.MyMessageBoxShow("Please Select Bank First")
        'End If
        'txtBankCode.Focus()
        'End If
    End Sub

    Private Sub FndIncentive__MYOpenMasterForm(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles FndIncentive._MYOpenMasterForm
        Frm_Open = New frmIncentiveMaster
        Frm_Open.SetUserMgmt(clsUserMgtCode.frmIncentiveMaster)
        Frm_Open.Show()
    End Sub

    Private Sub FndIncentive__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles FndIncentive._MYValidating
        GetIIncentiveDetails(isButtonClicked)
    End Sub

    Private Sub fndCity__MYOpenMasterForm(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndCity._MYOpenMasterForm
        Frm_Open = New frmCityMaster(objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode)
        Frm_Open.SetUserMgmt(clsUserMgtCode.cityMaster)
        Frm_Open.Show()
    End Sub

    Private Sub txtstatecode__MYOpenMasterForm(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtstatecode._MYOpenMasterForm
        Frm_Open = New frmStateMaster
        Frm_Open.SetUserMgmt(clsUserMgtCode.frmStateMaster)
        Frm_Open.Show()
    End Sub

    Private Sub txtcountrycode__MYOpenMasterForm(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtcountrycode._MYOpenMasterForm
        Frm_Open = New frmCountryMaster
        Frm_Open.SetUserMgmt(clsUserMgtCode.frmCountryMaster)
        Frm_Open.Show()
    End Sub


    Private Sub ImportChargeDetails()
        Dim gvCharges As New RadGridView()
        Me.Controls.Add(gvCharges)
        Dim countDefaultUOM As Integer = 0
        If transportSql.importExcel(gvCharges, "VSP Code", "Charge Code", "Description", "EMP") Then
            Dim isSaved As Boolean = True
            Dim currentdate As Date = Date.Today
            Dim trans As SqlTransaction = Nothing
            clsCommon.ProgressBarShow()
            Try
                trans = clsDBFuncationality.GetTransactin()
                For i As Integer = 0 To gvCharges.Rows.Count - 1
                    clsDBFuncationality.ExecuteNonQuery("DELETE FROM TSPL_MCC_VSP_ChargeCategory_MAPPING where VSP_CODE = '" & clsCommon.myCstr(gvCharges.Rows(i).Cells("VSP CODE").Value) & "'", trans)
                Next
                For Each grow As GridViewRowInfo In gvCharges.Rows
                    Dim LineNo As String = clsCommon.myCstr(grow.Index) + 2
                    Dim coll As New Hashtable()

                    Dim strVSPCode As String
                    Dim VSPCode As String = clsCommon.myCstr(grow.Cells("VSP Code").Value)
                    If clsCommon.myLen(VSPCode) >= 0 Then
                        strVSPCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Vendor_Code from TSPL_VENDOR_MASTER Where Vendor_Code ='" + VSPCode + "' And Form_Type ='VSP'", trans))
                        If clsCommon.myLen(strVSPCode) <= 0 Then
                            Throw New Exception("VSP Code '" + VSPCode + "' does not exist .Please make its master first at line no '" + LineNo + "'")
                        End If
                    Else
                        Throw New Exception("Please insert VSP code in at line no '" + LineNo + "' ")
                    End If

                    Dim strChargeC As String
                    Dim ChargeCode As String = clsCommon.myCstr(grow.Cells("Charge Code").Value)
                    If clsCommon.myLen(ChargeCode) > 0 Then
                        strChargeC = clsDBFuncationality.getSingleValue("Select Charge_Cat_Code from tspl_charge_category Where Charge_Cat_Code='" + ChargeCode + "'", trans)
                        If clsCommon.CompairString(strChargeC, ChargeCode) = CompairStringResult.Equal Then
                        Else
                            Throw New Exception("Charge code '" + ChargeCode + "' at line no '" + LineNo + "' does  not exist")
                        End If
                    Else
                        Throw New Exception("Please insert charge code at Line No '" + LineNo + "' ")
                    End If


                    'For i As Integer = 0 To gv1.Rows.Count - 1
                    '    If clsCommon.myLen(gv1.Rows(i).Cells("UOM").Value) > 0 Then
                    '        Dim UOM As String = clsCommon.myCstr(gv1.Rows(i).Cells("UOM").Value)
                    '        Dim FirstItemCode As String = clsCommon.myCstr(gv1.Rows(i).Cells("Item Code").Value)
                    '        For j As Integer = i + 1 To gv1.Rows.Count - 1
                    '            Dim SecondUOM As String = clsCommon.myCstr(gv1.Rows(j).Cells("UOM").Value)
                    '            Dim SecondItemCode As String = clsCommon.myCstr(gv1.Rows(j).Cells("Item Code").Value)
                    '            If clsCommon.CompairString(UOM, SecondUOM) = CompairStringResult.Equal And clsCommon.CompairString(FirstItemCode, SecondItemCode) = CompairStringResult.Equal Then
                    '                Throw New Exception("Please check ! duplicate UOM between Line No '" + clsCommon.myCstr(i + 1) + "' and '" + clsCommon.myCstr(j + 1) + "'")
                    '            End If
                    '        Next
                    '    End If
                    'Next
                    ''
                    Dim totcharge As Decimal = 0
                    Dim DBRate As Decimal = 0
                    For Each row As GridViewRowInfo In gvCharges.Rows
                        If clsCommon.CompairString(strVSPCode, clsCommon.myCstr(row.Cells("VSP Code").Value)) = CompairStringResult.Equal Then
                            If clsCommon.myLen(row.Cells("EMP").Value) > 0 Then
                                totcharge += row.Cells("EMP").Value
                            End If
                        End If


                    Next
                    DBRate = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select Service_charges - Actual_charges AS Rate From tspl_vendor_master where form_type='vsp' And Vendor_Code ='" & strVSPCode & "'", trans))
                    If totcharge > 0 Then

                        If (DBRate) >= 0 And (DBRate) <> totcharge Then
                            Throw New Exception("Please fill charges correctly for VSP (" & strVSPCode & ").It's EMP total should be [" & (DBRate) & "] at line no '" + LineNo + "'")
                        End If
                    Else
                        Throw New Exception("Please fill charges correctly for VSP (" & strVSPCode & ").It's EMP total should be [" & (DBRate) & "] at line no '" + LineNo + "'")
                    End If

                    If clsCommon.myLen(grow.Cells("EMP").Value) > 0 Then
                        If Not IsNumeric(grow.Cells("EMP").Value) Then
                            Throw New Exception("Please insert decimal data in EMP at Line No '" + LineNo + "' ")
                        Else
                            clsCommon.AddColumnsForChange(coll, "Rate", clsCommon.myCdbl(grow.Cells("EMP").Value))
                        End If
                    Else
                        clsCommon.AddColumnsForChange(coll, "Rate", clsCommon.myCdbl(grow.Cells("EMP").Value))
                    End If

                    'clsDBFuncationality.ExecuteNonQuery("Delete From TSPL_MCC_VSP_ChargeCategory_MAPPING where VSP_Code='" & strVSPCode & "'", trans)
                    'Dim Count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select Count(*) from TSPL_MCC_VSP_ChargeCategory_MAPPING Where VSP_Code='" + strVSPCode + "' AND Charge_Code='" & strChargeC & "'", trans))
                    clsCommon.AddColumnsForChange(coll, "VSP_CODE", strVSPCode)
                    clsCommon.AddColumnsForChange(coll, "Charge_CODE", strChargeC)
                    clsCommon.AddColumnsForChange(coll, "Updated_date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd-MMM-yyyy hh:mm:ss tt"))
                    'If Count <= 0 Then
                    'clsCommon.AddColumnsForChange(coll, "VSP_CODE", strVSPCode)
                    'clsCommon.AddColumnsForChange(coll, "Charge_CODE", strChargeC)
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MCC_VSP_ChargeCategory_MAPPING", OMInsertOrUpdate.Insert, "", trans)
                    'Else
                    'Dim whrClas As String = "VSP_Code = '" + strVSPCode + "' AND Charge_Code='" & strChargeC & "'"
                    ''clsCommon.AddColumnsForChange(coll, "VSP_CODE", strVSPCode)
                    ''clsCommon.AddColumnsForChange(coll, "Charge_CODE", strChargeC)
                    'isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MCC_VSP_ChargeCategory_MAPPING", OMInsertOrUpdate.Update, whrClas, trans)
                    'End If
                Next
                If isSaved Then

                    clsCommon.ProgressBarHide()
                    trans.Commit()
                    RadMessageBox.Show("Data Imported Successfully.")
                Else
                    Throw New Exception("Error in Import")
                End If
            Catch ex As Exception
                clsCommon.ProgressBarHide()
                trans.Rollback()
                RadMessageBox.Show(ex.Message)
            Finally
                Me.Controls.Remove(gvCharges)
            End Try
        End If
    End Sub

    Private Sub rmChargesDetails_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmChargesDetails.Click
        Try
            Dim qry As String
            qry = "Select VSP_CODE AS [VSP Code],Charge_CODE AS [Charge Code],isnull(TSPL_Charge_Category.Description,'') As Description,Rate AS EMP From TSPL_MCC_VSP_ChargeCategory_MAPPING LEFT OUTER JOIN TSPL_Charge_Category  on TSPL_Charge_Category.Charge_Cat_Code=TSPL_MCC_VSP_ChargeCategory_MAPPING.Charge_CODE"
            ListImpExpColumnsMandatory = New List(Of String)({"VSP Code", "Charge Code"})
            ListImpExpColumnsSuperMandatory = New List(Of String)({"VSP Code"})
            transportSql.ExporttoExcel(qry, "", "", Me, ListImpExpColumnsMandatory, ListImpExpColumnsSuperMandatory, MyBase.Form_ID + "ChargesDetails")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "UOM")
        End Try
    End Sub

    Private Sub rmVSPDetail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmVSPDetail.Click
        funImport()
    End Sub

    Private Sub rmChargesDetail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmChargesDetail.Click
        Try
            ImportChargeDetails()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub rmVSPDetails_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmVSPDetails.Click
        funExport()
    End Sub

    Private Sub gvBankG_CellDoubleClicK(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvBankG.CellDoubleClick
        'Try
        '    If clsCommon.myLen(gvBankG.CurrentRow.Cells("COLBankNO").Value) > 0 Then
        '        clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FrmBankGuaranteeMaster1, gvBankG.CurrentRow.Cells("COLBankNO").Value)
        '    End If
        'Catch ex As Exception
        '    clsCommon.MyMessageBoxShow(ex.ToString)
        'End Try
    End Sub

    Private Sub GVPaymentEntry_CellDoubleClicK(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles GVPaymentEntry.CellDoubleClick
        Try
            If clsCommon.myLen(GVPaymentEntry.CurrentRow.Cells("COLPaymentNO").Value) > 0 Then
                clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.PaymentEntryNew, GVPaymentEntry.CurrentRow.Cells("COLPaymentNO").Value)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.ToString)
        End Try
    End Sub

    Private Sub ChkHeadLoad_CheckStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkHeadLoad.CheckStateChanged
        Try
            If ChkHeadLoad.Checked = True Then
                GrpHeadLoad.Enabled = True
            Else
                GrpHeadLoad.Enabled = False
                txtRateHeadLoad.Text = Nothing
                txtDistanceKMHeadLoad.Text = Nothing
                CmbHeadLoadServiceBasis.SelectedValue = 0
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.ToString)
        End Try
    End Sub

    Private Sub chkismp_CheckStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkIsMP.CheckStateChanged
        Try
            If ChkIsMP.Checked = True Then
                GrpMP.Enabled = True
            Else
                GrpMP.Enabled = False
                FndMPCode.Value = Nothing
                LblMPName.Text = Nothing
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.ToString)
        End Try
    End Sub

    Private Sub ChkOwnAsset_CheckStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkOwnAsset.CheckStateChanged
        Try
            If ChkOwnAsset.Checked = True Then
                GrpOwnAsset.Enabled = True
            Else
                GrpOwnAsset.Enabled = False
                TxtRateOwnAsset.Text = Nothing
                CmbOwnAssetServiceBasis.SelectedValue = 0
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.ToString)
        End Try
    End Sub

    Private Sub fndMPCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles FndMPCode._MYValidating
        Try
            Dim whrcls As String = ""
            Dim qry As String = " select tspl_mp_master.MP_Code as [Code] ,tspl_mp_master.MP_Name as [MP Name] ,tspl_mp_master.VLC_Code as [VLC Code] ,tspl_mp_master.Village_Code as [Village Code] ,tspl_mp_master.Father_Name as [Father Name] ,tspl_mp_master.Add1 as [Address1] ,tspl_mp_master.Add2 as [Address2] ,tspl_mp_master.Zila as [Zila] ,tspl_mp_master.Tehsil as [Tehsil] ,tspl_mp_master.City_code as [City Code] ,tspl_mp_master.State_Code as [State Code] ,tspl_mp_master.Country_code as [Country Code] ,tspl_mp_master.Pin_code as [Pin Code] ,tspl_mp_master.Telphone as [Telphone] ,tspl_mp_master.Email as [Email] ,tspl_mp_master.Fax as [Fax] ,tspl_mp_master.DOB as [Date Of Birth] ,tspl_mp_master.Education as [Education] ,tspl_mp_master.Land_Holding as [Land Holding] ,tspl_mp_master.No_Of_Buffaloes as [No Of Buffaloes] ,tspl_mp_master.No_Of_Cows as [No Of Cows] ,tspl_mp_master.No_Of_breedable_milk_animal as [No Of Breedable Milk Animal] ,tspl_mp_master.Milk_production as [Total Milk Production] ,tspl_mp_master.Milk_Home_consumption as [Total Milk Home Consumption] ,tspl_mp_master.Milk_For_sale as [Remaining Milk For Sale] ,tspl_mp_master.PayeeName as [Payee Name] ,tspl_mp_master.BankName as [Bank Name] ,tspl_mp_master.BankBranch as [Bank Branch] ,tspl_mp_master.BankCityCode as [Bank City Code] ,tspl_mp_master.BankStateCode as [Bank State Code] ,tspl_mp_master.IFCICode as [IFCI Code] ,tspl_mp_master.AccountNO as [Account No] ,tspl_mp_master.Created_By as [Created By] ,tspl_mp_master.Created_Date as [Created Date] ,tspl_mp_master.Modified_By as [Modified By] ,tspl_mp_master.Modified_Date as [Modified Date] ,tspl_mp_master.Comp_Code as [Company Code],tspl_mp_master.Mp_code_Vlc_uploader as [MP Code VLC Uploder]  From tspl_mp_master "
            ' str = clsCommon.ShowSelectForm("FNDMPMST", qry, "Code", whrcls, "", "Code", isButtonClicked)
            Dim dr As DataRow = clsCommon.ShowSelectFormForRow("FNDMPCD", qry, FndMPCode.Value, "Code")
            If Not IsNothing(dr) Then 'clsCommon.myLen(fndMPCode.Value) > 0 Then
                FndMPCode.Value = dr("Code") 'clsMpMaster.getFinder("", FndMPCode.Value, isButtonClicked)
                LblMPName.Text = dr("MP Name")
                txtvendorname.Text = LblMPName.Text
                'LblMPName.Text = clsMpMaster.getMPNameOnMPCodeForVLCUplader(clsCommon.myCstr(FndMPCode.Value), Nothing)
            Else
                LblMPName.Text = Nothing
                FndMPCode.Value = Nothing
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub FndMPCode__MYOpenMasterForm(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles FndMPCode._MYOpenMasterForm
        Frm_Open = New FrmMPMaster(objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode)
        Frm_Open.SetUserMgmt(clsUserMgtCode.frmMPMaster)
        Frm_Open.Show()
    End Sub

    Public Sub Save_Transport_Data()
        Dim str As String = "select count(*) from tspl_Transport_master where Transport_Id='" & clsCommon.myCstr(fndvendorNo.Value) & "'"
        Dim check As Integer = clsDBFuncationality.getSingleValue(str)
        If check > 0 Then
            funupdateTransport()
        Else
            funinsertTransport()
        End If
    End Sub

    'Funtion for insertion of data
    Public Sub funinsertTransport()
        Try

            connectSql.RunSp("sp_transportmaster_insert", New SqlParameter("@transid", fndvendorNo.Value), New SqlParameter("@transname", txtvendorname.Text.ToString()), New SqlParameter("@city", txtCity.Text.ToString()), New SqlParameter("@state", txtState.Text.ToString()), New SqlParameter("@pincode", TxtPinCode.Text.ToString()), New SqlParameter("@panno", txtpan.Text.ToString()), New SqlParameter("@phone", txtPhone1.Text.ToString()), New SqlParameter("@add1", txtAdd1.Text.ToString()), New SqlParameter("@add2", txtAdd2.Text.ToString()), New SqlParameter("@email", txtEmail.Text.ToString()), New SqlParameter("@createdby", userCode), New SqlParameter("@createddate", connectSql.serverDate()), New SqlParameter("@modifiedby", userCode), New SqlParameter("@modifieddate", connectSql.serverDate()), New SqlParameter("@compcode", companyCode))
            ' myMessages.insert()
            btnsave.Text = "Update"
            btndelete.Enabled = True
            'If userCode <> "ADMIN" Then
            '    If funSetUserAccess() = False Then Exit Sub
            'End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub

    'Funtion for updation  of data
    Public Sub funupdateTransport()
        Try
            Dim currentdate As Date = Date.Today
            connectSql.RunSp("sp_transportmaster_update", New SqlParameter("@transid", fndvendorNo.Value), New SqlParameter("@transname", txtvendorname.Text.ToString()), New SqlParameter("@city", txtCity.Text.ToString()), New SqlParameter("@state", txtState.Text.ToString()), New SqlParameter("@pincode", TxtPinCode.Text.ToString()), New SqlParameter("@panno", txtpan.Text.ToString()), New SqlParameter("@phone", txtPhone1.Text.ToString()), New SqlParameter("@add1", txtAdd1.Text.ToString()), New SqlParameter("@add2", txtAdd2.Text.ToString()), New SqlParameter("@email", txtEmail.Text.ToString()), New SqlParameter("@modifiedby", userCode), New SqlParameter("@modifieddate", connectSql.serverDate()), New SqlParameter("@compcode", companyCode))
            ' myMessages.update()
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub

    'Private Sub TxtIFSCCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean)
    '    If clsCommon.myLen(fndbankcode.Value) > 0 Then
    '        Dim qry As String = "Select Bank_IFSC_Code as IFSCCode,Branch_Name  from TSPL_Vendor_Bank_Branch_Details "
    '        TxtIFSCCode.Value = clsCommon.ShowSelectForm("FormIFSCCode", qry, "IFSCCode", " Bank_Code ='" & fndbankcode.Value & "' ", TxtIFSCCode.Value, "", isButtonClicked)
    '        TxtBankBranch.Text = clsDBFuncationality.getSingleValue("Select Branch_Name from TSPL_Vendor_Bank_Branch_Details where Bank_Code ='" & fndbankcode.Value & "' and Bank_IFSC_Code='" & TxtIFSCCode.Value & "' ")
    '    Else
    '        clsCommon.MyMessageBoxShow("Please select Bank Code first")
    '    End If
    'End Sub

    Private Sub txtBankBranchCode__MYOpenMasterForm(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtBankBranchCode._MYOpenMasterForm
        If clsCommon.myLen(IIf(EnableBankFromMaster = True, findfndbankcode.Value, fndbankcode.Text)) > 0 Then
            Dim frm As New FrmVendorBankMaster '= Nothing
            frm.SetUserMgmt(clsUserMgtCode.VendorBankMaster)
            frm.BankCode = IIf(EnableBankFromMaster = True, findfndbankcode.Value, fndbankcode.Text)
            frm.WindowState = FormWindowState.Maximized
            frm.Show()
        Else
            clsCommon.MyMessageBoxShow("Please select bank Code")
        End If

    End Sub

    Private Sub txtBankBranchCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtBankBranchCode._MYValidating
        If clsCommon.myLen(IIf(EnableBankFromMaster = True, findfndbankcode.Value, fndbankcode.Text)) > 0 Then
            Dim qry As String = "Select Bank_IFSC_Code as IFSCCode,Branch_Name  from TSPL_Vendor_Bank_Branch_Details "
            txtBankBranchCode.Value = clsCommon.ShowSelectForm("FormBankBranch", qry, "Branch_Name", " Bank_Code ='" & IIf(EnableBankFromMaster = True, findfndbankcode.Value, fndbankcode.Text) & "' ", txtBankBranchCode.Value, "", isButtonClicked)
            txtIFCICode.Text = clsDBFuncationality.getSingleValue("Select Bank_IFSC_Code from TSPL_Vendor_Bank_Branch_Details where Bank_Code ='" & IIf(EnableBankFromMaster = True, findfndbankcode.Value, fndbankcode.Text) & "' and Branch_Name='" & txtBankBranchCode.Value & "' ")
        Else
            clsCommon.MyMessageBoxShow("Please select bank Code")
        End If
    End Sub

    Private Sub cboEMPType_SelectedValueChanged(sender As Object, e As EventArgs) Handles cboEMPType.SelectedValueChanged
        SetEMPColumns()
    End Sub

    Sub SetEMPColumns()
        Try
            txtFixedAmount.Visible = False
            MyLabel20.Visible = False
            If clsCommon.CompairString(clsCommon.myCstr(cboEMPType.SelectedValue), "FAFP") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(cboEMPType.SelectedValue), "FASWP") = CompairStringResult.Equal Then
                txtFixedAmount.Visible = True
                MyLabel20.Visible = True
            End If
            gvEMP.Columns(colEMPSlab).IsVisible = False
            If clsCommon.CompairString(clsCommon.myCstr(cboEMPType.SelectedValue), "SWP") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(cboEMPType.SelectedValue), "FASWP") = CompairStringResult.Equal Then
                gvEMP.Columns(colEMPSlab).IsVisible = True
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub gvEMP_CellFormatting(sender As Object, e As CellFormattingEventArgs) Handles gvEMP.CellFormatting
        If e.RowIndex >= 0 Then
            If e.Column Is gvEMP.Columns(colEMPSlab) Then
                gvEMP.CurrentRow.Cells(colEMPSlab).ReadOnly = True
                If clsCommon.CompairString(clsCommon.myCstr(cboEMPType.SelectedValue), "SWP") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(cboEMPType.SelectedValue), "FASWP") = CompairStringResult.Equal Then
                    gvEMP.CurrentRow.Cells(colEMPSlab).ReadOnly = False
                ElseIf clsCommon.CompairString(clsCommon.myCstr(cboEMPType.SelectedValue), "FP") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(cboEMPType.SelectedValue), "FPSP") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(cboEMPType.SelectedValue), "FAFP") = CompairStringResult.Equal Then
                    If e.RowIndex = 0 Then
                        gvEMP.CurrentRow.Cells(colEMPSlab).ReadOnly = False
                    End If
                End If
            ElseIf e.Column Is gvEMP.Columns(colEMPValue) Then
                gvEMP.CurrentRow.Cells(colEMPValue).ReadOnly = True
                If clsCommon.CompairString(clsCommon.myCstr(cboEMPType.SelectedValue), "FP") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(cboEMPType.SelectedValue), "FPSP") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(cboEMPType.SelectedValue), "FAFP") = CompairStringResult.Equal Then
                    If e.RowIndex = 0 Then
                        gvEMP.CurrentRow.Cells(colEMPValue).ReadOnly = False
                    End If
                ElseIf clsCommon.CompairString(clsCommon.myCstr(cboEMPType.SelectedValue), "SWP") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(cboEMPType.SelectedValue), "FASWP") = CompairStringResult.Equal Then
                    gvEMP.CurrentRow.Cells(colEMPValue).ReadOnly = False
                End If

            End If
        End If
    End Sub

    Public Sub funExport()
        Try
            Dim strCmd As String = "select count(*) from tspl_vendor_master where form_type='VSP'"
            Dim check As Integer = clsDBFuncationality.getSingleValue(strCmd)
            Dim whrCls As String = ""
            ''richa agarwal add FAt/SNF KG in Head Load Unit against Ticket No.MIL/17/01/19-000031
            'Ticket No-ERO/14/10/19-001055
            If check > 0 Then
                strCmd = "select Vendor_Code as [VSP No],Vendor_Name as[VSP Name],Add1 as [Address1],Add2 as [Address2]" &
                          ",Add3 as [Address3],Pin_Code as [Pin Code],Vendor_Group_Code as [Group Code]" &
                          ",Vendor_Group_Code_Desc as [Vendor Group Description],City_Code as [City Code]" &
                          ",City_Code_Desc as [City Code Description],State as [State],Country as [Country],Phone1 as [Phone Num1]" &
                          ",Phone2 as [Phone Num2],Fax as [Fax],Email as [Email Id],WebSite as [Website]" &
                          ",Terms_Code as [Terms Code],Terms_Code_Desc as [Terms Description],Vendor_Account as [Vendor Account]" &
                           ",Vendor_Account_Desc as [Vendor Account Description],Payment_Code as [Payment Code]" &
                           ",Payment_Code_Desc as [Paymnet Code Description],Bank_Code as [Bank Code],Bank_Code_Desc as [Bank Code Description]" &
                           ",Ven_Type_Code as [Vendor Type],Ven_Type_Desc as [Vendor Type Description]" &
                           ",Tax_Group as [Tax Group],Tax_Group_Desc as [Tax Group Description]" &
                           ",TAX1 as [Tax1],TAX1_Rate as [Tax1 Rate],TAX2 as [Tax2],TAX2_Rate as [Tax2 Rate],TAX3 as [Tax3]" &
                           ",TAX3_Rate as [Tax3 Rate],TAX4 as [Tax4],TAX4_Rate as [Tax4 Rate],TAX5 as [Tax5]" &
                           ",TAX5_Rate as [Tax5 Rate],TAX6 as [Tax6],TAX6_Rate as [Tax6 Rate],TAX7 as [Tax7]" &
                           ",TAX7_Rate as [Tax7 Rate], TAX8 as [Tax8],TAX8_Rate as [Tax8 Rate],TAX9 as [Tax9]" &
                           ",TAX9_Rate as [Tax9 Rate],TAX10 as [Tax10],TAX10_Rate as [Tax10 Rate]" &
                           ",Transporter as [Transporter]" &
                           ",Created_By as [Created By],Created_Date as [Created Date],Modify_By as [Modify By]" &
                           ",Modify_Date as [Modify Date],comp_code as [Company Code]" &
                           ",Collectorate as [Collectorate],PAN as [PAN],State_Code,Country_Code,PC_Code AS [Payment Cycle]," _
                           & " incentive,Joint_Name,case when Joint_Name <>'' then Joint_bank_Code else null end As [Name of Bank],case when Joint_Name <>'' then " _
                           & " Joint_Account_No else null end As [Bank Account No],Service_Charge_Type,commision_pers,payment_commision_pers,incentive_days,vsp_payment," _
                           & " VSP_Payee_Name,form_type, Branch_Name,Account_No,Bank_Name,IFSC_Code ,Account_Type,Agreement,Start_Date As [Agreement Date]," _
                           & " End_Date As [Expiry Date],MP_code as [MP Code],MP_Name as [MP Name],IS_DRIP_SAVER AS [Drip Saver],Joint_Branch_Name as" _
                           & " [Joint Branch Name],Joint_IFSC_Code as [Joint IFSC Code],convert (varchar,Rate_Head_Load) as [Head Load Rate],convert (varchar,DistanceKM_Head_Load) as [Head Load Distance KM] ,Rate_Own_Asset as [Own Asset Rate]," _
                           & " case when Service_Basis_Head_Load='K' then 'KG' when Service_Basis_Head_Load='L' then 'LTR' when Service_Basis_Head_Load='W' then 'FAT/SNF KG'  end as [Head Load Unit],case when Service_Basis_Own_Asset='K' then 'KG' when Service_Basis_Own_Asset='L' then 'LTR' end as [Own Asset Unit],CHEQUE_IN_FAVOUR_OF AS [Cheque in Favour of],EMP_Type as [EMPType (FP/SWP/FAFP/FASWP/FPSP)],EMP_Fixed_Amount,Actual_charges_Slab as [EMP Slab 1],Actual_charges [EMP Slab 1 Value],Actual_charges_Slab2 as [EMP Slab 2],Actual_charges2 [EMP Slab 2 Value],Actual_charges_Slab3 as [EMP Slab 3],Actual_charges3 [EMP Slab 3 Value],Actual_charges_Slab4 as [EMP Slab 4],Actual_charges4 [EMP Slab 4 Value],Actual_charges_Slab5 as [EMP Slab 5],Actual_charges5 [EMP Slab 5 Value],Apply_Mult_Incentive as [Multiple Incentive(0/1)],Security_Deduction_Amount,Interest_Per,Minimum_Interest,(case when Is_Blacklist='1' then 'Y' else 'N' end) as [Is Blacklist],Service_Charge_Per_Unit,CorrectionFat as [Adjusted Fat],CorrectionSNF as [Adjusted SNF] "

                If objCommonVar.GSTApplicable Then
                    strCmd += " , GSTRegistered as [GST Register], GSTFinalNo as [GSTIN No]"
                End If

                'strCmd += ",Handling_Charges_Per as [Handling Charges %] , TSPL_VENDOR_MASTER.CURRENCY_CODE  as [Currency Code],case when Credit_Limit_On_Milk_Receipt_Per<0 then 'N' else 'Y' end as [Apply Credit Limit Based on Milk Receipt(Y/N)],case when Credit_Limit_On_Milk_Receipt_Per<0 then 0 else Credit_Limit_On_Milk_Receipt_Per end as [Credit Limit %],Monthly_Rent as [Monthly Rent],(select case when CUST_CODE is null then 'N' ELSE 'Y' END from TSPL_CUSTOMER_MASTER where TSPL_CUSTOMER_MASTER.CUST_CODE=TSPL_VENDOR_MASTER.VENDOR_CODE) as [Create Customer(Y/N)],(select TSPL_CUSTOMER_MASTER.Cust_Group_Code from TSPL_CUSTOMER_MASTER where TSPL_CUSTOMER_MASTER.CUST_CODE=TSPL_VENDOR_MASTER.VENDOR_CODE) as [Customer Group Code] from TSPL_VENDOR_MASTER "
                'strCmd += ",Handling_Charges_Per as [Handling Charges %] , TSPL_VENDOR_MASTER.CURRENCY_CODE  as [Currency Code],case when Credit_Limit_On_Milk_Receipt_Per<0 then 'N' else 'Y' end as [Apply Credit Limit Based on Milk Receipt(Y/N)],case when Credit_Limit_On_Milk_Receipt_Per<0 then 0 else Credit_Limit_On_Milk_Receipt_Per end as [Credit Limit %],Monthly_Rent as [Monthly Rent],(select case when TSPL_CUSTOMER_VENDOR_MAPPING.CUST_CODE is null then 'N' ELSE 'Y' END from TSPL_CUSTOMER_VENDOR_MAPPING where TSPL_CUSTOMER_VENDOR_MAPPING.VENDOR_CODE=TSPL_VENDOR_MASTER.VENDOR_CODE) as [Create Customer(Y/N)],(select TSPL_CUSTOMER_MASTER.Cust_Group_Code from TSPL_CUSTOMER_MASTER left join TSPL_CUSTOMER_VENDOR_MAPPING on TSPL_CUSTOMER_MASTER.CUST_CODE=TSPL_CUSTOMER_VENDOR_MAPPING.CUST_CODE where TSPL_CUSTOMER_VENDOR_MAPPING.VENDOR_CODE=TSPL_VENDOR_MASTER.VENDOR_CODE) as [Customer Group Code] from TSPL_VENDOR_MASTER "
                strCmd += ",Handling_Charges_Per as [Handling Charges %] , TSPL_VENDOR_MASTER.CURRENCY_CODE  as [Currency Code],case when Credit_Limit_On_Milk_Receipt_Per<0 then 'N' else 'Y' end as [Apply Credit Limit Based on Milk Receipt(Y/N)],case when Credit_Limit_On_Milk_Receipt_Per<0 then 0 else Credit_Limit_On_Milk_Receipt_Per end as [Credit Limit %],Monthly_Rent as [Monthly Rent],(select case when CUST_CODE is null then 'N' ELSE 'Y' END from TSPL_CUSTOMER_MASTER where TSPL_CUSTOMER_MASTER.CUST_CODE=TSPL_VENDOR_MASTER.VENDOR_CODE and CUSTOMER_FORM_TYPE='VSP') as [Create Customer(Y/N)],(select TSPL_CUSTOMER_MASTER.Cust_Group_Code from TSPL_CUSTOMER_MASTER where TSPL_CUSTOMER_MASTER.CUST_CODE=TSPL_VENDOR_MASTER.VENDOR_CODE and CUSTOMER_FORM_TYPE='VSP') as [Customer Group Code],TIP_Buffalo as [TIP Buffalo],TIP_Cow as [TIP Cow],TIP_Mix as [TIP Mix],Care_Of,Aadhar_No, case when  isnull (Isbuyerfilereturninlasttwoyears,0) = 1 then 'Yes' else 'No' end as [buyer file return in last two years] , case when  isnull ( IsTCS_TDSamountgreaterthan50KpreviousYear,0) = 1 then 'Yes' else 'No' end as [TCS/TDS amount is greater than 50K in previous Year],Is_TDS_Applicable As [Is TDS Applicable],TDS_State_Code As [TDS State Code],TDS_Status As [TDS Status],TDS_Vendor_Type As [TDS Vendor Type],Deduction_Code As [Deduction Code],TDS_Branch_Code As [TDS Branch Code],SecChequeNoLac1,SecChequeNoRs100,TSPL_VENDOR_MASTER.Company_Bank as [CompanyBank]  from TSPL_VENDOR_MASTER "
                whrCls = " and form_type='VSP'"
            Else
                whrCls = ""
                strCmd = "select '' as [VSP No],'' as[VSP Name],'' as [Address1],'' as [Address2]" &
                          ",'' as [Address3],''  as [Pin Code],'' as [Closing Date],'' as [Group Code]" &
                          ",'' as [Vendor Group Description],'' as [City Code]" &
                          ",'' as [City Code Description],'' as [State],'' as [Country],'' as [Phone Num1]" &
                          ",'' as [Phone Num2],'' as [Fax],'' as [Email Id],'' as [Website]" &
                          ",'' as [Terms Code],'' as [Terms Description],'' as [Vendor Account]" &
                           ",'' as [Vendor Account Description],'' as [Payment Code]" &
                           ",'' as [Paymnet Code Description],'' as [Bank Code],'' as [Bank Code Description]" &
                           ",'' as [Vendor Type],'' as [Vendor Type Description]" &
                           ",'' as [Tax Group],'' as [Tax Group Description]" &
                           ",'' as [Tax1],'' as [Tax1 Rate],'' as [Tax2],'' as [Tax2 Rate],'' as [Tax3]" &
                           ",'' as [Tax3 Rate],'' as [Tax4],'' as [Tax4 Rate],'' as [Tax5]" &
                           ",'' as [Tax5 Rate],'' as [Tax6],'' as [Tax6 Rate],'' as [Tax7]" &
                           ",'' as [Tax7 Rate], '' as [Tax8],'' as [Tax8 Rate],'' as [Tax9]" &
                           ",'' as [Tax9 Rate],'' as [Tax10],'' as [Tax10 Rate]" &
                           ",'' as [Transporter]" &
                           ",'' as [Created By],'' as [Created Date],'' as [Modify By]" &
                           ",'' as [Modify Date],'' as [Company Code]" &
                           ",'' as [Collectorate],'' as [PAN],'' as State_Code,'' as Country_Code,'' As [Payment Cycle],'' as incentive," _
                           & " '' As [Name of Bank],'' As [Bank Account No],'' as Service_Charge_Type,'' as commision_pers,'' as payment_commision_pers,'' as incentive_days," _
                           & " '' as vsp_payment,'' as VSP_Payee_Name,'' as Joint_Name,'VSP' as form_type,'' AS Branch_Name,'' As Account_No,'' As Bank_Name," _
                           & " '' As IFSC_Code ,'' As Account_Type,'' As [Agreement Date],'' As Start_Date,'' As [Expiry Date],'' as [MP Code]," _
                           & " '' as [MP Name],'' AS [Drip Saver],'' as [Joint Branch Name],'' as [Joint IFSC Code],'' as [Head Load Rate],'' as [Head Load Distance KM],'' as [Own Asset Rate],'' as [Head Load Unit],'' as [Own Asset Unit],'' as [Cheque in Favour of],'' as [EMPType (FP/SWP/FAFP/FASWP/FPSP)],'' as EMP_Fixed_Amount,'' as [EMP Slab 1],'' as [EMP Slab 1 Value],'' as [EMP Slab 2],'' as [EMP Slab 2 Value],'' as [EMP Slab 3],'' as [EMP Slab 3 Value],'' as [EMP Slab 4],'' as [EMP Slab 4 Value],'' as [EMP Slab 5],'' as  [EMP Slab 5 Value],'0' as [Multiple Incentive(0/1)],0 as Security_Deduction_Amount,0 as Interest_Per,0 as Minimum_Interest,'N' as [Is Blacklist],0 as Service_Charge_Per_Unit,'' as [Adjusted Fat],'' as [Adjusted SNF] "

                If objCommonVar.GSTApplicable Then
                    strCmd += " , '' as [GST Register], '' as [GSTIN No]"
                End If
                strCmd += ",'' as [Handling Charges %], '' as [Currency Code],'' as [Apply Credit Limit Based on Milk Receipt(Y/N)],'' as [Credit Limit %],0 as [Monthly Rent],'' as [Create Customer(Y/N)],'' as [Customer Group Code],0 as [TIP Buffalo],0 as [TIP Cow],0 as [TIP Mix],'' as Care_Of,'' as Aadhar_No, 0 as [buyer file return in last two years] , 0 as [TCS/TDS amount is greater than 50K in previous Year],0 As [Is TDS Applicable],'' As [TDS State Code],'' As [TDS Status],'' As [TDS Vendor Type],'' As [Deduction Code],'' As [TDS Branch Code],'' as SecChequeNoLac1,'' as SecChequeNoRs100,'' as CompanyBank "
            End If
            ListImpExpColumnsMandatory = New List(Of String)({"VSP No", "VSP Name", "Group Code", "country_code", "state_code", "City Code", "Service_Charge_Type", "Terms Code", "Vendor Account", "Bank Code", "Tax Group", "IFSC_Code", "Agreement Date", "Expiry Date", "EMPType (FP/SWP/FAFP/FASWP/FPSP)", "EMP_Fixed_Amount", "GST Register", "Pan"})
            ListImpExpColumnsSuperMandatory = New List(Of String)({"VSP No"})
            transportSql.ExporttoExcel(strCmd, whrCls, "", Me, ListImpExpColumnsMandatory, ListImpExpColumnsSuperMandatory, MyBase.Form_ID)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Customer")
        End Try

    End Sub

    Public Sub funImport()

        Dim GSTFinal As String = ""
        Dim Registered As Integer = 0
        Dim GSTEntity As String = ""
        Dim GSTLastEntity As String = ""
        Dim GSTMiddleEntity As String = ""
        Dim PartyDetailsQry As String = ""
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim Count As String = """"


        Dim inputs() As String = {}

        If objCommonVar.GSTApplicable Then
            inputs = {"VSP No", "VSP Name", "Address1", "Address2", "Address3", "Group Code", "Vendor Group Description", "City Code", "City Code Description", "State", "Country", "Phone Num1", "Phone Num2", "Fax", "Email Id", "Website", "Terms Code", "Terms Description", "Vendor Account", "Vendor Account Description", "Payment Code", "Paymnet Code Description", "Bank Code", "Bank Code Description", "Vendor Type", "Vendor Type Description", "Tax Group", "Tax Group Description", "Tax1", "Tax1 Rate", "Tax2", "Tax2 Rate", "Tax3", "Tax3 Rate", "Tax4", "Tax4 Rate", "Tax5", "Tax5 Rate", "Tax6", "Tax6 Rate", "Tax7", "Tax7 Rate", "Tax8", "Tax8 Rate", "Tax9", "Tax9 Rate", "Tax10", "Tax10 Rate", "Transporter", "Created By", "Created Date", "Modify By", "Modify Date", "Company Code", "Collectorate", "PAN", "State_Code", "Country_Code", "Payment Cycle", "incentive", "Name of Bank", "Bank Account No", "Service_Charge_Type", "commision_pers", "payment_commision_pers", "incentive_days", "vsp_payment", "VSP_Payee_Name", "Joint_Name", "form_type", "Branch_Name", "Account_No", "Bank_Name", "IFSC_Code", "Account_Type", "Agreement", "Agreement Date", "Expiry Date", "MP Code", "MP Name", "Pin Code", "Drip Saver", "Joint Branch Name", "Joint IFSC Code", "Head Load Rate", "Head Load Distance KM", "Own Asset Rate", "Cheque in Favour of", "EMPType (FP/SWP/FAFP/FASWP/FPSP)", "EMP_Fixed_Amount", "EMP Slab 1", "EMP Slab 1 Value", "EMP Slab 2", "EMP Slab 2 Value", "EMP Slab 3", "EMP Slab 3 Value", "EMP Slab 4", "EMP Slab 4 Value", "EMP Slab 5", "EMP Slab 5 Value", "Multiple Incentive(0/1)", "Security_Deduction_Amount", "Interest_Per", "Minimum_Interest", "Is Blacklist", "Service_Charge_Per_Unit", "GST Register", "GSTIN No", "Adjusted Fat", "Adjusted SNF", "Handling Charges %", "Currency Code", "Apply Credit Limit Based on Milk Receipt(Y/N)", "Credit Limit %", "Monthly Rent", "Create Customer(Y/N)", "Customer Group Code", "TIP Buffalo", "TIP Cow", "TIP Mix", "Aadhar_No", "Care_Of", "buyer file return in last two years", "TCS/TDS amount is greater than 50K in previous Year", "Is TDS Applicable", "TDS State Code", "TDS Status", "TDS Vendor Type", "Deduction Code", "TDS Branch Code", "SecChequeNoLac1", "SecChequeNoRs100", "CompanyBank"}

        Else
            inputs = {"VSP No", "VSP Name", "Address1", "Address2", "Address3", "Group Code", "Vendor Group Description", "City Code", "City Code Description", "State", "Country", "Phone Num1", "Phone Num2", "Fax", "Email Id", "Website", "Terms Code", "Terms Description", "Vendor Account", "Vendor Account Description", "Payment Code", "Paymnet Code Description", "Bank Code", "Bank Code Description", "Vendor Type", "Vendor Type Description", "Tax Group", "Tax Group Description", "Tax1", "Tax1 Rate", "Tax2", "Tax2 Rate", "Tax3", "Tax3 Rate", "Tax4", "Tax4 Rate", "Tax5", "Tax5 Rate", "Tax6", "Tax6 Rate", "Tax7", "Tax7 Rate", "Tax8", "Tax8 Rate", "Tax9", "Tax9 Rate", "Tax10", "Tax10 Rate", "Transporter", "Created By", "Created Date", "Modify By", "Modify Date", "Company Code", "Collectorate", "PAN", "State_Code", "Country_Code", "Payment Cycle", "incentive", "Name of Bank", "Bank Account No", "Service_Charge_Type", "commision_pers", "payment_commision_pers", "incentive_days", "vsp_payment", "VSP_Payee_Name", "Joint_Name", "form_type", "Branch_Name", "Account_No", "Bank_Name", "IFSC_Code", "Account_Type", "Agreement", "Agreement Date", "Expiry Date", "MP Code", "MP Name", "Pin Code", "Drip Saver", "Joint Branch Name", "Joint IFSC Code", "Head Load Rate", "Head Load Distance KM", "Own Asset Rate", "Cheque in Favour of", "EMPType (FP/SWP/FAFP/FASWP/FPSP)", "EMP_Fixed_Amount", "EMP Slab 1", "EMP Slab 1 Value", "EMP Slab 2", "EMP Slab 2 Value", "EMP Slab 3", "EMP Slab 3 Value", "EMP Slab 4", "EMP Slab 4 Value", "EMP Slab 5", "EMP Slab 5 Value", "Multiple Incentive(0/1)", "Security_Deduction_Amount", "Interest_Per", "Minimum_Interest", "Is Blacklist", "Service_Charge_Per_Unit", "Adjusted Fat", "Adjusted SNF", "Handling Charges %", "Currency Code", "Apply Credit Limit Based on Milk Receipt(Y/N)", "Credit Limit %", "Monthly Rent", "Create Customer(Y/N)", "Customer Group Code", "TIP Buffalo", "TIP Cow", "TIP Mix", "Aadhar_No", "Care_Of", "buyer file return in last two years", "TCS/TDS amount is greater than 50K in previous Year", "Is TDS Applicable", "TDS State Code", "TDS Status", "TDS Vendor Type", "Deduction Code", "TDS Branch Code", "SecChequeNoLac1", "SecChequeNoRs100", "CompanyBank"}
        End If

        Dim Strs As List(Of String) = New List(Of String)(inputs)

        If transportSql.importExcel(gv, Strs.ToArray()) Then
            Dim trans As SqlTransaction = Nothing
            Try
                connectSql.OpenConnection()
                trans = clsDBFuncationality.GetTransactin()
                clsCommon.ProgressBarShow()
                Dim counter As Integer = 1
                Dim IsBlacklisted As Integer = 0

                For Each grow As GridViewRowInfo In gv.Rows
                    Count = clsCommon.myCstr(grow.Index + 2)

                    Dim IsBlacklist As String = clsCommon.myCstr(grow.Cells("Is Blacklist").Value)

                    Dim strvendorNo As String = clsCommon.myCstr(grow.Cells("VSP No").Value)
                    If clsCommon.myLen(strvendorNo) > 0 Then
                        If strvendorNo.Length > 12 Then
                            Throw New Exception("Check the length of VSP No.,See At Line No. " + clsCommon.myCstr(counter) + "")
                        End If

                        If String.IsNullOrEmpty(strvendorNo) Then
                            Throw New Exception("VSP No. can not be blank,See At Line No. " + clsCommon.myCstr(counter) + "")
                        End If
                    End If

                    If clsDBFuncationality.getSingleValue("select count(*) from TSPL_VENDOR_MASTER  where  vendor_code='" + strvendorNo + "'", trans) > 0 Then
                        If MyBase.isUpdateFlag = False Then
                            Throw New Exception("Don't have permission to update VSP Master.")
                        End If
                    End If

                    Dim strvendorname1 As String = clsCommon.myCstr(grow.Cells("VSP Name").Value)
                    Dim strvendorname As String = strvendorname1.Replace("'", "''")
                    If strvendorname.Length > 100 Then
                        Throw New Exception("Length of VSP Name can not be greater than 100.,See At Line No. " + clsCommon.myCstr(counter) + "")
                    End If
                    Dim MultipleIncentive As Integer = clsCommon.myCdbl(grow.Cells("Multiple Incentive(0/1)").Value)
                    If String.IsNullOrEmpty(strvendorname) Then
                        Throw New Exception("VSP Name can not be blank,See At Line No. " + clsCommon.myCstr(counter) + "")
                    End If

                    Dim add1 As String = clsCommon.myCstr(grow.Cells("Address1").Value)
                    Dim add2 As String = clsCommon.myCstr(grow.Cells("Address2").Value)
                    Dim add3 As String = clsCommon.myCstr(grow.Cells("Address3").Value)
                    Dim Pin_Code As String = clsCommon.myCstr(grow.Cells("Pin Code").Value)
                    Dim closing_date As String = System.DateTime.Now.Date
                    Dim strgroupCode As String = clsCommon.myCstr(grow.Cells("Group Code").Value)
                    If String.IsNullOrEmpty(strgroupCode) Then
                        Throw New Exception(" Group Code can not be blank,See At Line No. " + clsCommon.myCstr(counter) + "")
                    End If
                    Dim i As Integer
                    Dim qry As String = "select Count(*) from TSPL_VENDOR_GROUP  where Ven_Group_Code ='" + strgroupCode + "'"
                    i = connectSql.RunScalar(trans, qry)
                    If i = 0 Then
                        Throw New Exception("Vendor Group Code does not exist : " + strgroupCode + ",See At Line No. " + clsCommon.myCstr(counter) + "")
                    Else
                    End If
                    If strgroupCode.Length > 12 Then
                        Throw New Exception("Check the length of Group Code,See At Line No. " + clsCommon.myCstr(counter) + "")
                    End If

                    Dim strgroupDes As String = grow.Cells("Vendor Group Description").Value.ToString()
                    If strgroupDes.Length > 50 Then
                        Throw New Exception("Check the length of Group Code Description,See At Line No. " + clsCommon.myCstr(counter) + "")
                    End If
                    Dim citycode As String = clsCommon.myCstr(grow.Cells("City Code").Value)
                    Dim citycodedesc As String = clsCommon.myCstr(grow.Cells("City Code Description").Value)
                    Dim state As String = clsCommon.myCstr(grow.Cells("State").Value)
                    Dim country As String = clsCommon.myCstr(grow.Cells("Country").Value)

                    Dim statecode As String = clsCommon.myCstr(grow.Cells("state_code").Value)
                    Dim countrycode As String = clsCommon.myCstr(grow.Cells("country_code").Value)
                    Dim check As Integer = 0

                    If clsCommon.myLen(countrycode) <= 0 Then
                        Throw New Exception("Please Fill Country,See At Line No. " + clsCommon.myCstr(counter) + "")
                    End If
                    If clsCommon.myLen(statecode) <= 0 Then
                        Throw New Exception("Please Fill State,See At Line No. " + clsCommon.myCstr(counter) + "")
                    End If
                    If clsCommon.myLen(citycode) <= 0 Then
                        Throw New Exception("Please Fill City,See At Line No. " + clsCommon.myCstr(counter) + "")
                    End If

                    If clsCommon.myLen(countrycode) > 0 Then
                        qry = "select count(*) from tspl_country_master where country_code='" + countrycode + "'"
                        check = clsDBFuncationality.getSingleValue(qry, trans)

                        If check <= 0 Then
                            Throw New Exception("Country Code Does Not Exist,Please Make Country Master,See At Line No. " + clsCommon.myCstr(counter) + "")
                        End If
                    End If
                    If clsCommon.myLen(statecode) > 0 AndAlso clsCommon.myLen(countrycode) > 0 Then
                        qry = "select count(*) from tspl_state_master where country_code='" + countrycode + "' and state_code='" + statecode + "'"
                        check = clsDBFuncationality.getSingleValue(qry, trans)

                        If check <= 0 Then
                            Throw New Exception("State Code Does Not Exist,Please Make Its Master First,See At Line No. " + clsCommon.myCstr(counter) + "")
                        End If
                    End If

                    If clsCommon.myLen(citycode) > 0 AndAlso clsCommon.myLen(statecode) > 0 Then
                        qry = "select count(*) from tspl_city_master where city_code='" + citycode + "' and state_code='" + statecode + "'"
                        check = clsDBFuncationality.getSingleValue(qry, trans)

                        If check <= 0 Then
                            Throw New Exception("City Code Does Not Exist,Please Make Its Master First,See At Line No. " + clsCommon.myCstr(counter) + "")
                        End If
                    End If
                    ''Comment becuase EMP is removed againt ticke no BM00000009629 and actual_charges is now emp charges
                    'Dim srvccharge As Decimal = clsCommon.myCdbl(grow.Cells("EMP").Value)
                    ' '' Anubhooti 28-Oct-2014
                    'Dim Actual_charges As Decimal = clsCommon.myCdbl(grow.Cells("Actual_charges").Value)
                    'If clsCommon.myLen(Actual_charges) <= 0 Or clsCommon.myCdbl(Actual_charges) <= 0 Then
                    '    Throw New Exception("Please fill actual charges and it should be greater than 0,see at line no. " + clsCommon.myCstr(counter) + "")
                    'End If
                    'If Actual_charges > srvccharge Then
                    '    Throw New Exception("Please fill actual charge less than EMP ,see at line no. " + clsCommon.myCstr(counter) + "")
                    'End If
                    'Dim DBChargeRate As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select SUM(Rate) As Rate from TSPL_MCC_VSP_ChargeCategory_MAPPING group by VSP_CODE having VSP_CODE ='" & strvendorNo & "'", trans))
                    'Dim Diff As Double = srvccharge - Actual_charges
                    'If DBChargeRate > 0 Then
                    '    If Diff <> DBChargeRate And Diff <= 0 Then
                    '        qry = "update TSPL_MCC_VSP_ChargeCategory_MAPPING set rate=0,Updated_date='" & clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt") & "' where  VSP_CODE ='" & strvendorNo & "'"
                    '        clsDBFuncationality.getSingleValue(qry, trans)
                    '    End If
                    'End If

                    Dim PC_Code As String = clsCommon.myCstr(grow.Cells("Payment Cycle").Value)
                    If clsCommon.myLen(PC_Code) > 0 Then
                        qry = "select count(*) from TSPL_PAYMENT_CYCLE_MASTER where PC_CODE='" + PC_Code + "'"
                        check = clsDBFuncationality.getSingleValue(qry, trans)
                        If check <= 0 Then
                            Throw New Exception("Payment cycle does not exist,please make its master first,see at line no. " + clsCommon.myCstr(counter) + "")
                        End If
                        PC_Code = "'" & PC_Code & "'"
                    Else
                        PC_Code = "NULL"
                    End If

                    Dim MP_Code As String = clsCommon.myCstr(grow.Cells("MP Code").Value)
                    Dim MP_name As String = String.Empty
                    If clsCommon.myLen(MP_Code) > 0 Then
                        qry = "select count(*) from TSPL_MP_MASTER where MP_Code='" + MP_Code + "'"
                        check = clsDBFuncationality.getSingleValue(qry, trans)
                        If check <= 0 Then
                            Throw New Exception("MP Code does not exist,please make its master first,see at line no. " + clsCommon.myCstr(counter) + "")
                        End If
                        MP_Code = "'" & MP_Code & "'"
                        MP_name = clsDBFuncationality.getSingleValue("select MP_Name from TSPL_MP_MASTER where MP_Code=" & MP_Code & "", trans)
                    Else
                        MP_Code = "NULL"
                    End If
                    ''
                    Dim srvctype As String = clsCommon.myCstr(grow.Cells("Service_Charge_Type").Value)
                    Dim commsn As Decimal = clsCommon.myCdbl(grow.Cells("commision_pers").Value)
                    Dim paymnt_commsn As Decimal = clsCommon.myCdbl(grow.Cells("payment_commision_pers").Value)
                    Dim incentv As String = clsCommon.myCstr(grow.Cells("incentive").Value).Replace("'", "`")
                    If clsCommon.myLen(incentv) > 0 Then
                        Dim qryincentive As String = "select count(*) from TSPL_INCENTIVE_MASTER_HEAD where INCENTIVE_CODE='" + incentv + "'"
                        check = clsDBFuncationality.getSingleValue(qryincentive, trans)
                        If check <= 0 Then
                            Throw New Exception("Incentive does not exist,please make its master first,see at line no. " + clsCommon.myCstr(counter) + "")
                        End If
                    End If
                    If MultipleIncentive = 1 Then
                        incentv = ""
                    End If
                    Dim noofdays As Decimal = clsCommon.myCdbl(grow.Cells("incentive_days").Value)
                    Dim vsppaymnt As String = clsCommon.myCstr(grow.Cells("vsp_payment").Value).Replace("'", "`")
                    Dim payeename As String = clsCommon.myCstr(grow.Cells("vsp_payee_name").Value).Replace("'", "`")
                    Dim jointname As String = clsCommon.myCstr(grow.Cells("Joint_Name").Value).Replace("'", "`")

                    If clsCommon.CompairString(vsppaymnt, "Different") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(vsppaymnt, "Self") <> CompairStringResult.Equal Then
                        Throw New Exception("Fill Self/Different in vsp payment at line no. " + clsCommon.myCstr(counter) + "")
                    End If
                    If clsCommon.CompairString(vsppaymnt, "Different") = CompairStringResult.Equal AndAlso clsCommon.myLen(jointname) <= 0 Then
                        Throw New Exception("Please Fill Joint Name At Line No. " + clsCommon.myCstr(counter) + "")
                    ElseIf clsCommon.CompairString(vsppaymnt, "Different") <> CompairStringResult.Equal Then
                        payeename = ""
                        jointname = ""
                    End If
                    '' Anubhooti 28-Oct-2014
                    Dim NameOfBank As String = ""
                    Dim AccountNo As String = ""
                    If clsCommon.CompairString(vsppaymnt, "Different") = CompairStringResult.Equal Then
                        payeename = strvendorname & IIf(clsCommon.myLen(jointname) > 0, " and " & jointname, "")
                        NameOfBank = clsCommon.myCstr(grow.Cells("Name of Bank").Value).Replace("'", "`")
                        If clsCommon.myLen(NameOfBank) > 0 Then
                            Dim qrybank As String = "select count(*) from TSPL_Vendor_Bank_MASTER where Bank_Code='" + NameOfBank + "'"
                            check = clsDBFuncationality.getSingleValue(qrybank, trans)
                            If check <= 0 Then
                                Throw New Exception("Name of Bank does not exist,please make its master first,see at line no. " + clsCommon.myCstr(counter) + "")
                            End If
                        Else
                            Throw New Exception("Name of bank can not be left blank,see at line no. " + clsCommon.myCstr(counter) + "")
                        End If
                        AccountNo = clsCommon.myCstr(grow.Cells("Bank Account No").Value).Replace("'", "`")
                        If clsCommon.myLen(AccountNo) > 30 Then
                            Throw New Exception("Length of bank account no. should not be more than 30,see at line no. " + clsCommon.myCstr(counter) + "")
                        End If
                    ElseIf clsCommon.CompairString(vsppaymnt, "Self") = CompairStringResult.Equal Then
                        payeename = strvendorname
                    End If
                    ''
                    Dim Joint_Branch_Name As String = String.Empty
                    Dim Joint_IFSC_Code As String = String.Empty
                    Joint_Branch_Name = clsCommon.myCstr(grow.Cells("Joint Branch Name").Value)
                    Joint_IFSC_Code = clsCommon.myCstr(grow.Cells("Joint IFSC Code").Value)

                    If clsCommon.myLen(srvctype) <= 0 Then
                        Throw New Exception("Please Fill Service Type(Select,%(Percentage),Rate/Kg,Rate/Ltr) At Line No. " + clsCommon.myCstr(counter) + "")
                    End If

                    If clsCommon.CompairString(srvctype, "Select") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(srvctype, "%(Percentage)") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(srvctype, "Rate/Kg") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(srvctype, "Rate/Ltr") <> CompairStringResult.Equal Then
                        Throw New Exception("Filled Service Type Should Be Any One From Select,%(Percentage),Rate/Kg,Rate/Ltr At Line No. " + clsCommon.myCstr(counter) + "")
                    End If

                    Dim phonenum1 As String = clsCommon.myCstr(grow.Cells("Phone Num1").Value)
                    Dim phonenum2 As String = clsCommon.myCstr(grow.Cells("Phone Num2").Value)
                    Dim fax As String = clsCommon.myCstr(grow.Cells("Fax").Value)
                    Dim emailid As String = clsCommon.myCstr(grow.Cells("Email Id").Value)
                    Dim website As String = clsCommon.myCstr(grow.Cells("Website").Value)
                    Dim contct_person_name As String = "" 'clsCommon.myCstr(grow.Cells("Contact Person Name").Value)
                    Dim contct_perfson_phone As String = "" 'clsCommon.myCstr(grow.Cells("Contect Person Phone").Value)
                    Dim contct_person_fax As String = "" 'clsCommon.myCstr(grow.Cells("Contact Person Fax").Value)
                    Dim contct_person_website As String = "" 'clsCommon.myCstr(grow.Cells("Contact Person Website").Value)
                    Dim contct_person_email As String = "" 'clsCommon.myCstr(grow.Cells("Contact Person Email").Value)
                    Dim strtermcode As String = clsCommon.myCstr(grow.Cells("Terms Code").Value)
                    If String.IsNullOrEmpty(strtermcode) Then
                        Throw New Exception(" Terms Code can not be blank,See At Line No. " + clsCommon.myCstr(counter) + "")
                    End If
                    Dim i1 As Integer
                    Dim qry1 As String = "select count(*) from tspl_terms_master where terms_code='" + strtermcode + "'"
                    i1 = connectSql.RunScalar(trans, qry1)
                    If i1 = 0 Then
                        Throw New Exception("Terms Code Does not exist : " + strtermcode + ",See At Line No. " + clsCommon.myCstr(counter) + "")
                    End If

                    If strtermcode.Length > 12 Then
                        Throw New Exception("Check the length of Terms Code,See At Line No. " + clsCommon.myCstr(counter) + "")
                    End If

                    Dim strtermdes As String = clsCommon.myCstr(grow.Cells("Terms Description").Value)
                    If strtermdes.Length > 50 Then
                        Throw New Exception("Check the length of Term Description,See At Line No. " + clsCommon.myCstr(counter) + "")
                    End If
                    Dim vendoracct As String = clsCommon.myCstr(grow.Cells("Vendor Account").Value)
                    If String.IsNullOrEmpty(vendoracct) Then
                        Throw New Exception(" Vendor Account can not be blank,See At Line No. " + clsCommon.myCstr(counter) + "")
                    End If
                    Dim i3 As String

                    Dim qry3 As String = "select COUNT(*) from TSPL_VENDOR_ACCOUNT_SET where Acct_Set_Code ='" + vendoracct + "'"
                    i3 = connectSql.RunScalar(trans, qry3)
                    If i3 = 0 Then
                        Throw New Exception("Vendor Account Does Not Exist : " + vendoracct + ",See At Line No. " + clsCommon.myCstr(counter) + "")
                    End If
                    If vendoracct.Length > 12 Then
                        Throw New Exception("Check the length of Vendor Account Set Code,See At Line No. " + clsCommon.myCstr(counter) + "")
                    End If

                    Dim vendoracctdesc As String = clsCommon.myCstr(grow.Cells("Vendor Account Description").Value)

                    Dim paymenttype As String = clsCommon.myCstr(grow.Cells("Payment Code").Value)
                    Dim i4 As String
                    If Not String.IsNullOrEmpty(paymenttype) Then
                        Dim qry5 As String = "select COUNT(*) from TSPL_PAYMENT_CODE  where Payment_Code ='" + paymenttype + "'"
                        i4 = connectSql.RunScalar(trans, qry5)
                        If i4 = 0 Then
                            Throw New Exception("Payment Code Does Not Exist : " + paymenttype + ",See At Line No. " + clsCommon.myCstr(counter) + "")
                        End If
                        If paymenttype.Length > 12 Then
                            Throw New Exception("Check the length of Payment Code,See At Line No. " + clsCommon.myCstr(counter) + "")
                        End If
                    End If
                    Dim paymenttypedesc As String = clsCommon.myCstr(grow.Cells("Paymnet Code Description").Value)
                    Dim strbank As String = clsCommon.myCstr(grow.Cells("Bank Code").Value)

                    If String.IsNullOrEmpty(strbank) Then
                        Throw New Exception("Bank Code can not be blank,See At Line No. " + clsCommon.myCstr(counter) + "")
                    End If

                    If EnableBankFromMaster = True Then
                        Dim i5 As String
                        Dim qry7 As String = "select COUNT(*) from tspl_vendor_bank_master  where Bank_Code ='" + strbank + "'"
                        i5 = connectSql.RunScalar(trans, qry7)
                        If i5 = 0 Then
                            Throw New Exception("Bank code does not exist : " + strbank + ",see at line no. " + clsCommon.myCstr(counter) + "")
                        End If
                    End If
                    If strbank.Length > 30 Then
                        Throw New Exception("Check the length of bank code,see at line no. " + clsCommon.myCstr(counter) + "")
                    End If

                    Dim Cheque_In_favour_of As String = clsCommon.myCstr(grow.Cells("Cheque in Favour of").Value)
                    Dim Head_Load_Amt As Decimal = clsCommon.myCdbl(grow.Cells("Head Load Rate").Value)
                    Dim Head_Load_DistanceKM As Decimal = clsCommon.myCdbl(grow.Cells("Head Load Distance KM").Value)
                    Dim Own_Asset_Amt As Decimal = clsCommon.myCdbl(grow.Cells("Own Asset Rate").Value)
                    Dim Service_Basis_Head_Load = clsCommon.myCstr(grow.Cells("Head Load Unit").Value)
                    If Head_Load_Amt <= 0 Then
                        '   Service_Basis_Head_Load = ""
                    End If

                    If Not String.IsNullOrEmpty(Service_Basis_Head_Load) Then
                        ''richa agarwal add FAt/SNF KG in Head Load Unit against Ticket No.MIL/17/01/19-000031
                        If clsCommon.CompairString(Service_Basis_Head_Load, "FAT/SNF KG") = CompairStringResult.Equal Then
                            Service_Basis_Head_Load = "W"
                        Else
                            Dim qry5 As String = "select COUNT(*) from TSPL_Unit_MasTer  where Unit_Code ='" & Service_Basis_Head_Load & "'"
                            'Dim qry5 As String = "select COUNT(*) from TSPL_Unit_MasTer  where Unit_Code ='" & IIf(Service_Basis_Head_Load = "K", "KG", "LTR") & "'"
                            i4 = connectSql.RunScalar(trans, qry5)
                            If i4 = 0 Then
                                Throw New Exception("Head Load Unit Does Not Exist : " + Service_Basis_Head_Load + ",See At Line No. " + clsCommon.myCstr(counter) + "")
                            End If
                            If clsCommon.CompairString(Service_Basis_Head_Load, "KG") = CompairStringResult.Equal Then
                                Service_Basis_Head_Load = "K"
                            Else
                                Service_Basis_Head_Load = "L"
                            End If
                        End If

                    End If
                    Dim Service_Basis_Own_Asset = clsCommon.myCstr(grow.Cells("Own Asset Unit").Value)
                    If Own_Asset_Amt <= 0 Then
                        Service_Basis_Own_Asset = ""
                    End If
                    If Not String.IsNullOrEmpty(Service_Basis_Own_Asset) Then
                        Dim qry5 As String = "select COUNT(*) from TSPL_Unit_MasTer  where Unit_Code ='" & IIf(Service_Basis_Own_Asset = "K", "KG", "LTR") & "'"
                        i4 = connectSql.RunScalar(trans, qry5)
                        If i4 = 0 Then
                            Throw New Exception("Own Asset Unit Does Not Exist : " + Service_Basis_Own_Asset + ",See At Line No. " + clsCommon.myCstr(counter) + "")
                        End If
                    End If
                    Dim strbankdes As String = clsCommon.myCstr(grow.Cells("Bank Code Description").Value)
                    If strbankdes.Length > 50 Then
                        Throw New Exception("Check the length of Bank Description,See At Line No. " + clsCommon.myCstr(counter) + "")
                    End If

                    Dim strvendortype As String = clsCommon.myCstr(grow.Cells("Vendor Type").Value)
                    Dim strvendortypedes As String = grow.Cells("Vendor Type Description").Value.ToString()
                    If strvendortype.Length > 12 Then
                        Throw New Exception("Check the length of Vendor Type,See At Line No. " + clsCommon.myCstr(counter) + " ")
                    End If
                    If strvendortypedes.Length > 50 Then
                        Throw New Exception("Check the length of Vendor Type Description,See At Line No. " + clsCommon.myCstr(counter) + "")
                    End If
                    Dim strTax As String = clsCommon.myCstr(grow.Cells("Tax Group").Value)
                    If String.IsNullOrEmpty(strTax) Then
                        Throw New Exception(" Tax Group can not be blank,See At Line No. " + clsCommon.myCstr(counter) + "")
                    End If
                    Dim i6 As String
                    Dim qry9 As String = "select COUNT(*) from  TSPL_TAX_GROUP_MASTER   where tax_group_Code ='" + strTax + "'"
                    i6 = connectSql.RunScalar(trans, qry9)
                    If i6 = 0 Then
                        Throw New Exception("Tax Group Code Does Not Exist : " + strTax + ",See At Line No. " + clsCommon.myCstr(counter) + "")
                    End If
                    If strTax.Length > 12 Then
                        Throw New Exception("Check the length of Tax Group Code,See At Line No. " + clsCommon.myCstr(counter) + "")
                    End If

                    Dim strtaxdes As String = grow.Cells("Tax Group Description").Value.ToString()
                    If strtaxdes.Length > 50 Then
                        Throw New Exception("Check the length of Tax Description,See At Line No. " + clsCommon.myCstr(counter) + "")
                    End If
                    Dim interbranch As String = "" 'grow.Cells("Inter Branch").Value.ToString()
                    If interbranch.Length > 1 Then
                        Throw New Exception("Check the length of Inter Branch,See At Line No. " + clsCommon.myCstr(counter) + "")
                    ElseIf String.IsNullOrEmpty(interbranch) Then
                        interbranch = "N"
                    End If

                    Dim strTagAsFranchise As String = "" ' grow.Cells("Tagged as Franchise").Value.ToString()
                    If strTagAsFranchise.Length > 1 Then
                        Throw New Exception("Check the length of Tagged as Franchise,See At Line No. " + clsCommon.myCstr(counter) + "")
                    ElseIf String.IsNullOrEmpty(strTagAsFranchise) Then
                        strTagAsFranchise = "N"
                    End If

                    Dim strAccNo As String = clsCommon.myCstr(grow.Cells("Account_No").Value)
                    If clsCommon.myLen(strAccNo) > 50 Then
                        Throw New Exception("Account No. should be max 50 character.")
                    End If

                    Dim strBName As String = clsCommon.myCstr(grow.Cells("Bank_Name").Value)
                    If clsCommon.myLen(strBName) > 50 Then
                        Throw New Exception("Bank Name should be max 50 character.")
                    End If
                    Dim strAadharNo As String = clsCommon.myCstr(grow.Cells("Aadhar_No").Value)
                    Dim strCareOf As String = clsCommon.myCstr(grow.Cells("Care_Of").Value)
                    Dim strSecChequeNoLac1 As String = clsCommon.myCstr(grow.Cells("SecChequeNoLac1").Value)
                    Dim strSecChequeNoRs100 As String = clsCommon.myCstr(grow.Cells("SecChequeNoRs100").Value)

                    If clsCommon.myLen(strAadharNo) > 0 Then
                        If clsCommon.myLen(strAadharNo) <> 12 Then
                            Throw New Exception("Aadhar No should be 12 character")
                        End If
                    End If
                    ''richa agarwal 26/03/2015
                    Dim strIFSCCode As String = clsCommon.myCstr(grow.Cells("IFSC_Code").Value)
                    If clsCommon.myLen(strIFSCCode) > 100 Then
                        Throw New Exception("IFSC Code should be max 100 character")
                    End If
                    If String.IsNullOrEmpty(strIFSCCode) Then
                        Throw New Exception("IFSC Code can not be blank,See At Line No. " + clsCommon.myCstr(counter) + "")
                    End If
                    If EnableBankFromMaster = True Then
                        If clsDBFuncationality.getSingleValue("Select count(*) from TSPL_Vendor_Bank_Branch_Details where Bank_IFSC_Code ='" + strIFSCCode + "'  and TSPL_Vendor_Bank_Branch_Details.Bank_Code='" & strbank & "' ", trans) <= 0 Then
                            Throw New Exception("IFSC Code Does Not Exist :  " + strIFSCCode + " for bank " + strbank + "  .Please make entry in vendor bank master.")
                        End If
                    End If
                    Dim strBrachName As String = clsCommon.myCstr(grow.Cells("Branch_Name").Value)
                    If clsCommon.myLen(strBrachName) > 100 Then
                        Throw New Exception("Branch Name should be max 100 character")
                    End If
                    If EnableBankFromMaster = True Then
                        If clsDBFuncationality.getSingleValue("Select count(*) from TSPL_Vendor_Bank_Branch_Details where Bank_IFSC_Code ='" + strIFSCCode + "'  and TSPL_Vendor_Bank_Branch_Details.Bank_Code='" & strbank & "' and TSPL_Vendor_Bank_Branch_Details.Branch_Name='" & strBrachName & "'", trans) <= 0 Then
                            Throw New Exception("Branch Name Does Not Exist : " + strBrachName + " for bank " + strbank + "  .Please make entry in vendor bank master.")
                        End If
                    End If
                    ''-------------------------

                    If clsCommon.myLen(Joint_IFSC_Code) > 100 Then
                        Throw New Exception("Joint IFSC Code should be max 50 character")
                    End If
                    If clsCommon.myLen(Joint_Branch_Name) > 100 Then
                        Throw New Exception("Joint Branch Name should be max 50 character")
                    End If
                    If EnableBankFromMaster = True Then
                        If clsCommon.CompairString(vsppaymnt, "Different") = CompairStringResult.Equal Then
                            If clsDBFuncationality.getSingleValue("Select count(*) from TSPL_Vendor_Bank_Branch_Details where Bank_IFSC_Code ='" + Joint_IFSC_Code + "'  and TSPL_Vendor_Bank_Branch_Details.Bank_Code='" & NameOfBank & "' ", trans) <= 0 Then
                                Throw New Exception("Joint IFSC Code Does Not Exist :  " + Joint_IFSC_Code + " for bank " + NameOfBank + "  .Please make entry in vendor bank master.")
                            End If

                            If clsDBFuncationality.getSingleValue("Select count(*) from TSPL_Vendor_Bank_Branch_Details where Bank_IFSC_Code ='" + Joint_IFSC_Code + "'  and TSPL_Vendor_Bank_Branch_Details.Bank_Code='" & NameOfBank & "' and TSPL_Vendor_Bank_Branch_Details.Branch_Name='" & Joint_Branch_Name & "'", trans) <= 0 Then
                                Throw New Exception("Joint Branch Name Does Not Exist : " + Joint_Branch_Name + " for bank " + NameOfBank + "  .Please make entry in vendor bank master.")
                            End If

                        End If
                    End If
                    Dim strAccType As String = clsCommon.myCstr(grow.Cells("Account_Type").Value)
                    If (String.IsNullOrEmpty(strAccType)) Or clsCommon.myLen(strAccType) > 10 Then
                        Throw New Exception("Length of Account Type should be max. 10 character .")
                    End If

                    If clsCommon.myLen(strbank) > 0 Then
                        Dim obj As clsVendorBankMaster
                        obj = clsVendorBankMaster.GetData(strbank, NavigatorType.Current, trans)
                        If Not IsNothing(obj) Then
                            strBName = obj.Bank_Name
                            strbankdes = obj.Bank_Name
                        End If
                    Else
                        strBName = ""
                        strbankdes = ""
                    End If
                    If clsCommon.myLen(strAccType) > 0 Then
                        If clsCommon.CompairString(strAccType, "Cur") = CompairStringResult.Equal Or clsCommon.CompairString(strAccType, "Cre") = CompairStringResult.Equal Or clsCommon.CompairString(strAccType, "Oth") = CompairStringResult.Equal Or clsCommon.CompairString(strAccType, "Sav") = CompairStringResult.Equal Or clsCommon.CompairString(strAccType, "Cas") = CompairStringResult.Equal Or clsCommon.CompairString(strAccType, "Loa") = CompairStringResult.Equal Then
                        Else
                            Throw New Exception("Account Type For should be amoung 'Cur','Cas','Sav','Cre','Loa','Oth'.")
                        End If
                    End If
                    'nature is always E due against ticket no BM00000009629 by balwinder
                    'Dim strNature As String = "E" 
                    'If clsCommon.myLen(strNature) > 0 Then
                    '    If clsCommon.CompairString(strNature, "E") = CompairStringResult.Equal Or clsCommon.CompairString(strNature, "C") = CompairStringResult.Equal Then
                    '    Else
                    '        Throw New Exception("Nature should be amoung 'E','C'.")
                    '    End If
                    '    If clsCommon.CompairString(strNature, "E") = CompairStringResult.Equal Then
                    '        commsn = Actual_charges
                    '    ElseIf clsCommon.CompairString(strNature, "C") = CompairStringResult.Equal Then
                    '        paymnt_commsn = Actual_charges
                    '    End If
                    'Else
                    '    Throw New Exception("Nature can not be left blank.")
                    'End If

                    Dim strAgreement As String = clsCommon.myCstr(grow.Cells("Agreement").Value)
                    Dim strAgreementDate As String = clsCommon.myCstr(grow.Cells("Agreement Date").Value)
                    Dim strExpiryDate As String = clsCommon.myCstr(grow.Cells("Expiry Date").Value)
                    If clsCommon.CompairString(strAgreement, "Yes") = CompairStringResult.Equal Then
                        If clsCommon.myLen(strAgreementDate) <= 0 Then
                            Throw New Exception("Agreement date can not be left blank")
                        End If
                        If clsCommon.myLen(strExpiryDate) <= 0 Then
                            Throw New Exception("Expiry date can not be left blank")
                        End If
                        Try
                            Convert.ToDateTime(strAgreementDate)
                        Catch exx As Exception
                            Throw New Exception("Agreement date should be in proper date format")
                        End Try
                        Try
                            Convert.ToDateTime(strExpiryDate)
                        Catch exx As Exception
                            Throw New Exception("Expiry date should be in proper date format")
                        End Try
                    End If

                    If clsCommon.CompairString(clsCommon.myCstr(IsBlacklist), "Y") = CompairStringResult.Equal Then
                        IsBlacklisted = 1
                    ElseIf clsCommon.CompairString(clsCommon.myCstr(IsBlacklist), "N") = CompairStringResult.Equal Then
                        IsBlacklisted = 0
                    Else
                        Throw New Exception("Fill 'Is Blacklist' in 'Y/N' format")
                    End If

                    Dim is_drip_saver As String = clsCommon.myCstr(grow.Cells("Drip saver").Value)
                    Dim sql1 As String = ""
                    Dim i2 As Integer = 0


                    If AllowVSPMasterAutoPrefix = 1 Then
                        If clsCommon.myLen(strvendorNo) > 0 Then
                            sql1 = "select count(*) from TSPL_VENDOR_MASTER  where  vendor_code='" + strvendorNo + "'"
                            i2 = CInt(connectSql.RunScalar(trans, sql1))
                            If i2 = 0 Then
                                Throw New Exception("Please enter valid vendor no to update.For new vendor auto prefix is ON.")
                            End If
                        Else
                            strvendorNo = (clsERPFuncationality.GetNextCode(trans, clsCommon.GETSERVERDATE(trans), clsDocType.VSPMASTER, clsDocTransactionType.Registered, ""))
                        End If
                    Else
                        sql1 = "select count(*) from TSPL_VENDOR_MASTER  where  vendor_code='" + strvendorNo + "'"
                        i2 = CInt(connectSql.RunScalar(trans, sql1))
                    End If
                    If clsCommon.myLen(clsCommon.myCstr(grow.Cells("PAN").Value)) > 0 Then
                        qry = "select count(*) from tspl_vendor_master where PAN='" + clsCommon.myCstr(grow.Cells("PAN").Value) + "' and Form_Type='VSP' and vendor_Code<>'" & clsCommon.myCstr(grow.Cells("VSP No").Value) & "'"
                        check = clsDBFuncationality.getSingleValue(qry, trans)

                        If check > 0 Then
                            clsCommon.ProgressBarHide()
                            trans.Rollback()
                            clsCommon.MyMessageBoxShow("Pan No. You Entered Is Already Exist,Please Enter Valid,Unique Pan No.,See At Line No. " + clsCommon.myCstr(counter) + "", Me.Text)
                            pageCus.SelectedPage = RadPageViewPage4
                            txtpan.Focus()
                            txtpan.Select()
                            Errorcontrol.SetError(txtpan, "Pan No. You Entered Is Already Exist,Please Enter Valid,Unique Pan No.,See At Line No. " + clsCommon.myCstr(counter) + "")
                            Return
                        Else
                            Errorcontrol.ResetError(txtpan)
                        End If

                        If clsCommon.myLen(clsCommon.myCstr(grow.Cells("PAN").Value)) > 0 Then
                            If Not checkPan.IsMatch(clsCommon.myCstr(grow.Cells("PAN").Value)) Then
                                clsCommon.ProgressBarHide()
                                Throw New Exception("Please check ! PAN numbers need to have 5 characters followed by 4 numbers then a final character,See At Line No. " + clsCommon.myCstr(counter) + "")
                            End If
                        End If
                    End If

                    ''Slab Work
                    Dim strEMPType As String = clsCommon.myCstr(grow.Cells("EMPType (FP/SWP/FAFP/FASWP/FPSP)").Value)
                    If clsCommon.myLen(strEMPType) <= 0 Then
                        Throw New Exception("Please provide emp type.See At Line No. " + clsCommon.myCstr(counter) + "")
                    End If
                    If Not (clsCommon.CompairString(strEMPType, "FP") = CompairStringResult.Equal OrElse clsCommon.CompairString(strEMPType, "FPSP") = CompairStringResult.Equal OrElse clsCommon.CompairString(strEMPType, "SWP") = CompairStringResult.Equal OrElse clsCommon.CompairString(strEMPType, "FAFP") = CompairStringResult.Equal OrElse clsCommon.CompairString(strEMPType, "FASWP") = CompairStringResult.Equal) Then
                        Throw New Exception("Emp type should be FP/FPSP/SWP/FAFP/FASWP .See At Line No. " + clsCommon.myCstr(counter) + "")
                    End If

                    If clsCommon.CompairString(strEMPType, "FPSP") = CompairStringResult.Equal Then
                        If Not (clsCommon.CompairString(srvctype, "Rate/Kg") = CompairStringResult.Equal OrElse clsCommon.CompairString(srvctype, "Rate/Ltr") = CompairStringResult.Equal) Then
                            cmbservc_type.Focus()
                            Throw New Exception("For EMP Type- FPSP(Fixed Percent + Standard Price) ,Service Basis should be 'Rate/Kg' or 'Rate/Ltr'")
                        End If
                    End If


                    Dim dblFixedAmount As Double = clsCommon.myCdbl(grow.Cells("EMP_Fixed_Amount").Value)
                    If clsCommon.CompairString(strEMPType, "FAFP") = CompairStringResult.Equal OrElse clsCommon.CompairString(strEMPType, "FASWP") = CompairStringResult.Equal Then
                        If dblFixedAmount <= 0 Then
                            Throw New Exception("Please provide EMP Fixed Amount .See At Line No. " + clsCommon.myCstr(counter) + "")
                        End If
                    End If


                    Dim EMPSlab1, EMPSlab1Value, EMPSlab2, EMPSlab2Value, EMPSlab3, EMPSlab3Value, EMPSlab4, EMPSlab4Value, EMPSlab5, EMPSlab5Value As New Double
                    If clsCommon.CompairString(strEMPType, "FP") = CompairStringResult.Equal OrElse clsCommon.CompairString(strEMPType, "FPSP") = CompairStringResult.Equal OrElse clsCommon.CompairString(strEMPType, "FAFP") = CompairStringResult.Equal Then
                        EMPSlab1Value = clsCommon.myCdbl(grow.Cells("EMP Slab 1 Value").Value)
                    Else
                        EMPSlab1 = clsCommon.myCdbl(grow.Cells("EMP Slab 1").Value)
                        EMPSlab1Value = clsCommon.myCdbl(grow.Cells("EMP Slab 1 Value").Value)
                        EMPSlab2 = clsCommon.myCdbl(grow.Cells("EMP Slab 2").Value)
                        EMPSlab2Value = clsCommon.myCdbl(grow.Cells("EMP Slab 2 Value").Value)
                        EMPSlab3 = clsCommon.myCdbl(grow.Cells("EMP Slab 3").Value)
                        EMPSlab3Value = clsCommon.myCdbl(grow.Cells("EMP Slab 3 Value").Value)
                        EMPSlab4 = clsCommon.myCdbl(grow.Cells("EMP Slab 4").Value)
                        EMPSlab4Value = clsCommon.myCdbl(grow.Cells("EMP Slab 4 Value").Value)
                        EMPSlab5 = clsCommon.myCdbl(grow.Cells("EMP Slab 5").Value)
                        EMPSlab5Value = clsCommon.myCdbl(grow.Cells("EMP Slab 5 Value").Value)
                    End If
                    If FixVSPEMP > 0 Then
                        strEMPType = "FP"
                        dblFixedAmount = 0
                        srvctype = "%(Percentage)"
                        EMPSlab1 = 0
                        EMPSlab1Value = FixVSPEMP
                        EMPSlab2 = 0
                        EMPSlab2Value = 0
                        EMPSlab3 = 0
                        EMPSlab3Value = 0
                        EMPSlab4 = 0
                        EMPSlab4Value = 0
                        EMPSlab5 = 0
                        EMPSlab5Value = 0
                    End If

                    ''End of Slab Work

                    If clsCommon.CompairString(srvctype, "Select") = CompairStringResult.Equal Then
                        If EMPSlab1Value > 0 OrElse EMPSlab2Value > 0 OrElse EMPSlab3Value > 0 OrElse EMPSlab4Value > 0 OrElse EMPSlab5Value > 0 Then
                            Throw New Exception("Filled Service Type Should Be Any One From %(Percentage),Rate/Kg,Rate/Ltr At Line No. " + clsCommon.myCstr(counter) + "")
                        End If
                    End If

                    Dim CorrectionFat As Decimal = clsCommon.myCdbl(grow.Cells("Adjusted Fat").Value)

                    Dim CorrectionSNF As Decimal = clsCommon.myCdbl(grow.Cells("Adjusted SNF").Value)



                    ''-----GST Import Vedor Master
                    Dim GSTSate_COde As String = ""
                    Dim PanNo As String = ""
                    If objCommonVar.GSTApplicable Then
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

                        GSTFinal = ""
                        GSTEntity = ""
                        GSTMiddleEntity = ""
                        GSTLastEntity = ""

                        If Registered = 1 Then

                            PanNo = clsCommon.myCstr(grow.Cells("Pan").Value)

                            If clsCommon.myLen(PanNo) <= 0 Then
                                Throw New Exception("Fill Pan Number.")
                            End If
                            Dim check1 As String = clsDBFuncationality.getSingleValue("select case when isnull(GST_State_Code,'')='' then '' else GST_State_Code end as GST_State_Code from tspl_state_master where state_code='" & statecode & "'", trans)
                            If clsCommon.myLen(check1) > 0 Then
                                GSTSate_COde = check1
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
                    ''-----GST Import Vedor Master

                    Dim GSTQry As String = ""
                    Dim strSecurityDeductionAmount As String = clsCommon.myCstr(clsCommon.myCdbl(grow.Cells("Security_Deduction_Amount").Value))
                    Dim strInterestPer As String = clsCommon.myCstr(clsCommon.myCdbl(grow.Cells("Interest_Per").Value))
                    Dim strMinimumInterest As String = clsCommon.myCstr(clsCommon.myCdbl(grow.Cells("Minimum_Interest").Value))
                    Dim strServiceChargePerUnit As String = clsCommon.myCstr(clsCommon.myCdbl(grow.Cells("Service_Charge_Per_Unit").Value))
                    Dim dclHandlingChargesPer As Decimal = clsCommon.myCdbl(grow.Cells("Handling Charges %").Value)
                    If dclHandlingChargesPer < 0 OrElse dclHandlingChargesPer > 100 Then
                        Throw New Exception("Handling Charges(%) range should be (0-100)")
                    End If
                    'Currency Code


                    Dim dclCreditLimitPer As Decimal = 0
                    If clsCommon.CompairString(clsCommon.myCstr(grow.Cells("Apply Credit Limit Based on Milk Receipt(Y/N)").Value), "Y") = CompairStringResult.Equal Then
                        dclCreditLimitPer = clsCommon.myCdbl(grow.Cells("Credit Limit %").Value)
                        If dclCreditLimitPer < 0 OrElse dclCreditLimitPer > 100 Then
                            Throw New Exception("Credit Limit(%) range should be (0-100)")
                        End If
                    Else
                        dclCreditLimitPer = -1
                    End If

                    If dclHandlingChargesPer < 0 OrElse dclHandlingChargesPer > 100 Then
                        Throw New Exception("Handling Charges(%) range should be (0-100)")
                    End If

                    ' Currency Code start
                    Dim strCurrencyCode As String = IIf(objCommonVar.IsMultiCurrencyCompany = False, objCommonVar.BaseCurrencyCode, clsCommon.myCstr(grow.Cells("Currency Code").Value))
                    If CheckMultiCurrency(trans) = True Then
                        Dim chkCurrency As Boolean = clsCommon.myCBool(clsDBFuncationality.getSingleValue(" select count(*) from TSPL_CURRENCY_MASTER where CURRENCY_CODE = '" + strCurrencyCode + "'", trans))
                        If chkCurrency = False Then
                            Throw New Exception("Invalid Currency Code,See At Line No. " + clsCommon.myCstr(counter) + "")
                        End If
                    Else
                        If clsCommon.myLen(strCurrencyCode) > 0 Then
                            If clsCommon.CompairString(strCurrencyCode, clsCommon.myCstr(objCommonVar.BaseCurrencyCode)) <> CompairStringResult.Equal Then
                                Throw New Exception("Please First Apply in [Module Currency Mapping] Screen for this Module,See At Line No. " + clsCommon.myCstr(counter) + "")
                            End If
                        Else
                            strCurrencyCode = clsCommon.myCstr(objCommonVar.BaseCurrencyCode)
                        End If
                    End If
                    If ValidationMultiCurrencyForImport(strCurrencyCode, vendoracct, strTax, clsCommon.myCstr(counter), trans) = False Then
                        trans.Rollback()
                        clsCommon.ProgressBarHide()
                        Exit Sub
                    End If
                    ' Currency Code End

                    'Customer Group Code sanjay
                    Dim strCustomerGroupCode As String = clsCommon.myCstr(grow.Cells("Customer Group Code").Value)
                    If clsCommon.CompairString(clsCommon.myCstr(grow.Cells("Create Customer(Y/N)").Value), "Y") = CompairStringResult.Equal Then
                        Dim chkCustomerGroupCode As Boolean = clsCommon.myCBool(clsDBFuncationality.getSingleValue(" select count(*) from TSPL_CUSTOMER_GROUP_MASTER where CUST_GROUP_CODE = '" + strCustomerGroupCode + "'", trans))
                        If chkCustomerGroupCode = False Then
                            Throw New Exception("Invalid/Blank Customer Group Code,See At Line No. " + clsCommon.myCstr(counter) + "")
                        End If
                    End If
                    'Customer Group Code

                    Dim strCompanyBank As String = clsCommon.myCstr(grow.Cells("CompanyBank").Value)
                    If clsCommon.myLen(strCompanyBank) > 0 Then
                        strCompanyBank = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select bank_code from TSPL_BANK_MASTER where bank_code= '" + strCompanyBank + "'", trans))
                        If clsCommon.myLen(strCompanyBank) <= 0 Then
                            Throw New Exception("Invalid Company Bank [" + clsCommon.myCstr(grow.Cells("CompanyBank").Value) + "],See At Line No. " + clsCommon.myCstr(counter) + "")
                        End If
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
                    Dim IsTaxApp As String = clsCommon.myCstr(grow.Cells("Is TDS Applicable").Value)
                    If clsCommon.myLen(IsTaxApp) > 0 Then
                        If clsCommon.CompairString(IsTaxApp, "0") = CompairStringResult.Equal Or clsCommon.CompairString(IsTaxApp, "1") = CompairStringResult.Equal Then
                            If clsVendorMaster.checkisIDSapplicable(strgroupCode, trans) AndAlso (clsCommon.CompairString(IsTaxApp, "0") = CompairStringResult.Equal) Then
                                Throw New Exception("Is TDS Applicable must be '1' for Vendor group '" + strgroupCode + "'")
                            End If
                        Else
                            Throw New Exception("Is TDS Applicable should be amoung '0','1'")
                        End If
                    Else
                        Throw New Exception("Is TDS Applicable should be amoung '0','1'")
                    End If


                    Dim strTDSStateCode As String = clsCommon.myCstr(grow.Cells("TDS State Code").Value)
                    If clsCommon.myLen(IsTaxApp) > 0 Then
                        If clsCommon.CompairString(IsTaxApp, "1") = CompairStringResult.Equal Then
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
                        ElseIf clsCommon.CompairString(IsTaxApp, "0") = CompairStringResult.Equal Then
                            strTDSStateCode = "Null"
                        End If
                    End If

                    Dim strTDSState As String = clsCommon.myCstr(grow.Cells("TDS Status").Value)
                    If clsCommon.myLen(IsTaxApp) > 0 Then
                        If clsCommon.CompairString(IsTaxApp, "1") = CompairStringResult.Equal Then
                            If clsCommon.CompairString(strTDSState, "Resident") = CompairStringResult.Equal Or clsCommon.CompairString(strTDSState, "Non Resident") = CompairStringResult.Equal Then
                            Else
                                Throw New Exception("TDS Status should be amoung 'Resident','Non Resident'")
                            End If
                        ElseIf clsCommon.CompairString(IsTaxApp, "0") = CompairStringResult.Equal Then
                            strTDSState = ""
                        End If
                    End If

                    Dim strTDSVenType As String = clsCommon.myCstr(grow.Cells("TDS Vendor Type").Value)
                    If clsCommon.myLen(IsTaxApp) > 0 Then
                        If clsCommon.CompairString(IsTaxApp, "1") = CompairStringResult.Equal Then
                            If clsCommon.CompairString(strTDSVenType, "Individual") = CompairStringResult.Equal Or clsCommon.CompairString(strTDSVenType, "Undevided Family") = CompairStringResult.Equal Or clsCommon.CompairString(strTDSVenType, "Partnership Firm") = CompairStringResult.Equal Or clsCommon.CompairString(strTDSVenType, "Domestic Company") = CompairStringResult.Equal Or clsCommon.CompairString(strTDSVenType, "Co-Operative Society") = CompairStringResult.Equal Or clsCommon.CompairString(strTDSVenType, "Local Authority") = CompairStringResult.Equal Then
                            Else
                                Throw New Exception("TDS Vendor Type should be amoung 'Individual','Undevided Family','Partnership Firm','Domestic Company','Co-Operative Society','Local Authority'")
                            End If
                        ElseIf clsCommon.CompairString(IsTaxApp, "0") = CompairStringResult.Equal Then
                            strTDSVenType = ""
                        End If
                    End If

                    Dim strDedCode As String = clsCommon.myCstr(grow.Cells("Deduction Code").Value)
                    If clsCommon.myLen(IsTaxApp) > 0 Then
                        If clsCommon.CompairString(IsTaxApp, "1") = CompairStringResult.Equal Then
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
                        ElseIf clsCommon.CompairString(IsTaxApp, "0") = CompairStringResult.Equal Then
                            strDedCode = "Null"
                        End If
                    End If

                    Dim strTDSBranch As String = clsCommon.myCstr(grow.Cells("TDS Branch Code").Value)
                    If clsCommon.myLen(IsTaxApp) > 0 Then
                        If clsCommon.CompairString(IsTaxApp, "1") = CompairStringResult.Equal Then
                            If clsCommon.myLen(strTDSBranch) > 0 Then
                                Dim qryTDSBranch As String = "select Count(*) As Row from TSPL_TDS_BRANCH_MASTER where Branch_Code='" & strTDSBranch & "'"
                                Dim checkBranch As Integer = clsDBFuncationality.getSingleValue(qryTDSBranch, trans)
                                If checkBranch <= 0 Then
                                    Throw New Exception("Filled TDS branch code does not exist" + Environment.NewLine + ".First make the entry for TDS branch code")
                                End If
                                strTDSBranch = "'" & strTDSBranch & "'"
                            Else
                                strTDSBranch = "Null"
                            End If

                        ElseIf clsCommon.CompairString(IsTaxApp, "0") = CompairStringResult.Equal Then
                            strTDSBranch = "Null"
                        End If
                    End If
                    If clsCommon.CompairString(IsTaxApp, "1") = CompairStringResult.Equal Then
                        Dim qryNatureDed As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Non_PAN_No From TSPL_TDS_DEDUCTION_HEAD where Deduction_Code =" & strDedCode & "", trans))
                        If clsCommon.CompairString(qryNatureDed.ToUpper().Trim(), "Y") = CompairStringResult.Equal AndAlso clsCommon.myLen(clsCommon.myCstr(grow.Cells("PAN").Value)) > 0 Then
                            Throw New Exception("You can not make this entry with Non PAN nature of deduction as PAN No exists.")
                        End If
                    End If
                    If clsCommon.myLen(clsCommon.myCstr(grow.Cells("State_Code").Value)) > 0 AndAlso clsCommon.myLen(clsCommon.myCstr(grow.Cells("TDS State Code").Value)) > 0 Then
                        If clsCommon.CompairString(clsCommon.myCstr(grow.Cells("State_Code").Value), clsCommon.myCstr(grow.Cells("TDS State Code").Value)) <> CompairStringResult.Equal Then
                            Throw New Exception("State code and TDS state code should be same.")
                        End If
                    End If
                    Dim StateCodeCommon As String
                    If clsCommon.myLen(clsCommon.myCstr(grow.Cells("State_Code").Value)) > 0 Then
                        StateCodeCommon = "'" & clsCommon.myCstr(grow.Cells("State_Code").Value) & "'"
                    ElseIf clsCommon.myLen(clsCommon.myCstr(grow.Cells("TDS State Code").Value)) > 0 Then
                        StateCodeCommon = "'" & clsCommon.myCstr(grow.Cells("TDS State Code").Value) & "'"
                    Else
                        StateCodeCommon = "NULL"
                    End If

                    If (i2 = 0) Then
                        Dim strcmd As String = ""
                        If clsCommon.CompairString(strAgreement, "Yes") = CompairStringResult.Equal Then
                            strcmd = "Insert into TSPL_VENDOR_MASTER (Vendor_Code,Vendor_Name,Add1,Add2,Add3,Closing_Date ,Vendor_Group_Code,Vendor_Group_Code_Desc,City_Code ,City_Code_Desc,State,Country,Phone1,Phone2,Fax,Email,WebSite,Terms_Code,Terms_Code_Desc ,Vendor_Account,Vendor_Account_Desc,Payment_Code,Payment_Code_Desc,Bank_Code ,Bank_Code_Desc,Tax_Group ,Tax_Group_Desc,TAX1,TAX1_Rate,TAX2,TAX2_Rate,TAX3,TAX3_Rate,TAX4,TAX4_Rate,TAX5,TAX5_Rate,TAX6,TAX6_Rate,TAX7,TAX7_Rate,TAX8,TAX8_Rate,TAX9,TAX9_Rate,TAX10,TAX10_Rate,Transporter,created_by,created_date,modify_by,modify_date,comp_code,PAN,Inter_branch,franchise_yn,form_type,state_code,country_code,commision_pers,incentive,incentive_days,vsp_payment,vsp_payee_name,Joint_Name,Service_Charge_Type,payment_commision_pers,Branch_Name,Account_No,Bank_Name,IFSC_Code,Account_Type,Agreement ,Start_Date,End_Date ,Joint_bank_Code,Joint_Account_No,PC_CODE,Nature,Mp_code,Mp_Name,is_Drip_saver,Joint_Branch_Name,Joint_IFSC_Code,Rate_Head_Load,DistanceKM_Head_Load,Is_Head_Load,Rate_Own_Asset,is_Own_Asset,Service_basis_Head_Load,Service_basis_Own_Asset,Cheque_in_Favour_of,Status,OnHold,EMP_Type,EMP_Fixed_Amount,Actual_charges_Slab,Actual_charges,Actual_charges_Slab2 ,Actual_charges2 ,Actual_charges_Slab3 ,Actual_charges3 ,Actual_charges_Slab4 ,Actual_charges4 ,Actual_charges_Slab5 ,Actual_charges5 ,Apply_Mult_Incentive,Security_Deduction_Amount,Interest_Per,Minimum_Interest,Is_Blacklist,Service_Charge_Per_Unit,Aadhar_No,Care_Of,SecChequeNoLac1,SecChequeNoRs100 ) values ('" + strvendorNo + "','" + strvendorname + "','" + add1 + "','" + add2 + "','" + add3 + "','" + closing_date + "','" + strgroupCode + "','" + strgroupDes + "','" + citycode + "','" + citycodedesc + "','" + state + "','" + country + "','" + phonenum1 + "','" + phonenum2 + "','" + fax + "','" + emailid + "','" + website + "','" + strtermcode + "','" + strtermdes + "','" + vendoracct + "','" + vendoracctdesc + "','" + paymenttype + "','" + paymenttypedesc + "','" + strbank + "','" + strbankdes + "','" + strTax + "','" + strtaxdes + "','" + clsCommon.myCstr(grow.Cells("Tax1").Value.ToString()) + "','" + clsCommon.myCstr(clsCommon.myCdbl(grow.Cells("Tax1 Rate").Value.ToString())) + "','" + clsCommon.myCstr(grow.Cells("Tax2").Value.ToString()) + "','" + clsCommon.myCstr(clsCommon.myCdbl(grow.Cells("Tax2 Rate").Value)) + "','" + clsCommon.myCstr(grow.Cells("Tax3").Value.ToString()) + "','" + clsCommon.myCstr(clsCommon.myCdbl(grow.Cells("Tax3 Rate").Value)) + "','" + clsCommon.myCstr(grow.Cells("Tax4").Value.ToString()) + "','" + clsCommon.myCstr(clsCommon.myCdbl(grow.Cells("Tax4 Rate").Value)) + "','" + clsCommon.myCstr(grow.Cells("Tax5").Value.ToString()) + "','" + clsCommon.myCstr(clsCommon.myCdbl(grow.Cells("Tax5 Rate").Value)) + "','" + grow.Cells("Tax6").Value.ToString() + "','" + clsCommon.myCstr(clsCommon.myCdbl(grow.Cells("Tax6 Rate").Value)) + "','" + clsCommon.myCstr(grow.Cells("Tax7").Value.ToString()) + "','" + clsCommon.myCstr(clsCommon.myCdbl(grow.Cells("Tax7 Rate").Value)) + "','" + clsCommon.myCstr(grow.Cells("Tax8").Value.ToString()) + "','" + clsCommon.myCstr(clsCommon.myCdbl(grow.Cells("Tax8 Rate").Value)) + "','" + clsCommon.myCstr(grow.Cells("Tax9").Value.ToString()) + "','" + clsCommon.myCstr(clsCommon.myCdbl(grow.Cells("Tax9 Rate").Value)) + "','" + clsCommon.myCstr(grow.Cells("Tax10").Value.ToString()) + "','" + clsCommon.myCstr(clsCommon.myCdbl(grow.Cells("Tax10 Rate").Value)) + "','" + clsCommon.myCstr(grow.Cells("Transporter").Value.ToString()) + "','" + userCode + "','" + connectSql.serverDate(trans) + "','" + userCode + "','" + connectSql.serverDate(trans) + "','" + companyCode + "','" + clsCommon.myCstr(grow.Cells("PAN").Value.ToString()) + "','" + interbranch + "','" + strTagAsFranchise + "','VSP','" + statecode + "','" + countrycode + "','" + clsCommon.myCstr(commsn) + "','" + clsCommon.myCstr(incentv) + "','" + clsCommon.myCstr(noofdays) + "','" + clsCommon.myCstr(vsppaymnt) + "','" + clsCommon.myCstr(payeename) + "','" + jointname + "','" + srvctype + "','" + clsCommon.myCstr(paymnt_commsn) + "','" + strBrachName + "','" + strAccNo + "','" + strBName + "','" + strIFSCCode + "','" + strAccType + "','" + strAgreement.ToUpper().Trim() + "','" & strAgreementDate & "','" & strExpiryDate & "','" + NameOfBank + "','" + AccountNo + "'," + PC_Code + ",'E'," + MP_Code + ",'" + MP_name + "','" & is_drip_saver & "','" & Joint_Branch_Name & "','" & Joint_IFSC_Code & "','" & Head_Load_Amt & "','" & Head_Load_DistanceKM & "','" & IIf(Head_Load_Amt > 0, "T", "F") & "','" & Own_Asset_Amt & "','" & IIf(Own_Asset_Amt > 0, "T", "F") & "','" & IIf(Service_Basis_Head_Load.ToString.Contains("K"), "K", IIf(Service_Basis_Head_Load.ToString.Contains("L"), "L", "")) & "','" & IIf(Service_Basis_Own_Asset.ToString.Contains("K"), "K", IIf(Service_Basis_Own_Asset.ToString.Contains("L"), "L", "")) & "','" & Cheque_In_favour_of & "','N','N','" + strEMPType + "', '" + clsCommon.myCstr(dblFixedAmount) + "','" + clsCommon.myCstr(EMPSlab1) + "','" + clsCommon.myCstr(EMPSlab1Value) + "','" + clsCommon.myCstr(EMPSlab2) + "', '" + clsCommon.myCstr(EMPSlab2Value) + "','" + clsCommon.myCstr(EMPSlab3) + "','" + clsCommon.myCstr(EMPSlab3Value) + "','" + clsCommon.myCstr(EMPSlab4) + "','" + clsCommon.myCstr(EMPSlab4Value) + "', '" + clsCommon.myCstr(EMPSlab5) + "', '" + clsCommon.myCstr(EMPSlab5Value) + "'," & MultipleIncentive & "," + strSecurityDeductionAmount + "," + strInterestPer + "," + strMinimumInterest + ",'" + clsCommon.myCstr(IsBlacklisted) + "','" + strServiceChargePerUnit + "','" + strAadharNo + "','" + strCareOf + "','" + strSecChequeNoLac1 + "','" + strSecChequeNoRs100 + "')"
                            connectSql.RunSqlTransaction(trans, "Update  TSPL_VENDOR_MASTER set Start_Date=Convert(date,'" & strAgreementDate & "',103),End_Date =Convert(date,'" & strExpiryDate & "',103),Currency_Code='" & objCommonVar.BaseCurrencyCode & "' where vendor_code='" + strvendorNo + "' and form_type='VSP'")

                        Else
                            strAgreementDate = "NULL"
                            strExpiryDate = "NULL"
                            strcmd = "Insert into TSPL_VENDOR_MASTER (Vendor_Code,Vendor_Name,Add1,Add2,Add3,Closing_Date ,Vendor_Group_Code,Vendor_Group_Code_Desc,City_Code ,City_Code_Desc,State,Country,Phone1,Phone2,Fax,Email,WebSite,Terms_Code,Terms_Code_Desc ,Vendor_Account,Vendor_Account_Desc,Payment_Code,Payment_Code_Desc,Bank_Code ,Bank_Code_Desc,Tax_Group ,Tax_Group_Desc,TAX1,TAX1_Rate,TAX2,TAX2_Rate,TAX3,TAX3_Rate,TAX4,TAX4_Rate,TAX5,TAX5_Rate,TAX6,TAX6_Rate,TAX7,TAX7_Rate,TAX8,TAX8_Rate,TAX9,TAX9_Rate,TAX10,TAX10_Rate,Transporter,created_by,created_date,modify_by,modify_date,comp_code,PAN,Inter_branch,franchise_yn,form_type,state_code,country_code,commision_pers,incentive,incentive_days,vsp_payment,vsp_payee_name,Joint_Name,Service_Charge_Type,payment_commision_pers,Branch_Name,Account_No,Bank_Name,IFSC_Code,Account_Type,Agreement ,Start_Date,End_Date ,Joint_bank_Code,Joint_Account_No,PC_CODE,Nature,Mp_code,Mp_Name,is_Drip_saver,Joint_Branch_Name,Joint_IFSC_Code,Rate_Head_Load,DistanceKM_Head_Load,Is_Head_Load,Rate_Own_Asset,is_Own_Asset,Service_basis_Head_Load,Service_basis_Own_Asset,Cheque_in_Favour_of,Status,OnHold,EMP_Type,EMP_Fixed_Amount,Actual_charges_Slab,Actual_charges,Actual_charges_Slab2 ,Actual_charges2 ,Actual_charges_Slab3 ,Actual_charges3 ,Actual_charges_Slab4 ,Actual_charges4 ,Actual_charges_Slab5 ,Actual_charges5 ,Apply_Mult_Incentive,Security_Deduction_Amount,Interest_Per,Minimum_Interest,Is_Blacklist,Service_Charge_Per_Unit,Currency_Code,Aadhar_No,Care_Of,SecChequeNoLac1,SecChequeNoRs100 ) values ('" + strvendorNo + "','" + strvendorname + "','" + add1 + "','" + add2 + "','" + add3 + "','" + closing_date + "','" + strgroupCode + "','" + strgroupDes + "','" + citycode + "','" + citycodedesc + "','" + state + "','" + country + "','" + phonenum1 + "','" + phonenum2 + "','" + fax + "','" + emailid + "','" + website + "','" + strtermcode + "','" + strtermdes + "','" + vendoracct + "','" + vendoracctdesc + "','" + paymenttype + "','" + paymenttypedesc + "','" + strbank + "','" + strbankdes + "','" + strTax + "','" + strtaxdes + "','" + clsCommon.myCstr(grow.Cells("Tax1").Value.ToString()) + "','" + clsCommon.myCstr(clsCommon.myCdbl(grow.Cells("Tax1 Rate").Value.ToString())) + "','" + clsCommon.myCstr(grow.Cells("Tax2").Value.ToString()) + "','" + clsCommon.myCstr(clsCommon.myCdbl(grow.Cells("Tax2 Rate").Value)) + "','" + clsCommon.myCstr(grow.Cells("Tax3").Value.ToString()) + "','" + clsCommon.myCstr(clsCommon.myCdbl(grow.Cells("Tax3 Rate").Value)) + "','" + clsCommon.myCstr(grow.Cells("Tax4").Value.ToString()) + "','" + clsCommon.myCstr(clsCommon.myCdbl(grow.Cells("Tax4 Rate").Value)) + "','" + clsCommon.myCstr(grow.Cells("Tax5").Value.ToString()) + "','" + clsCommon.myCstr(clsCommon.myCdbl(grow.Cells("Tax5 Rate").Value)) + "','" + grow.Cells("Tax6").Value.ToString() + "','" + clsCommon.myCstr(clsCommon.myCdbl(grow.Cells("Tax6 Rate").Value)) + "','" + clsCommon.myCstr(grow.Cells("Tax7").Value.ToString()) + "','" + clsCommon.myCstr(clsCommon.myCdbl(grow.Cells("Tax7 Rate").Value)) + "','" + clsCommon.myCstr(grow.Cells("Tax8").Value.ToString()) + "','" + clsCommon.myCstr(clsCommon.myCdbl(grow.Cells("Tax8 Rate").Value)) + "','" + clsCommon.myCstr(grow.Cells("Tax9").Value.ToString()) + "','" + clsCommon.myCstr(clsCommon.myCdbl(grow.Cells("Tax9 Rate").Value)) + "','" + clsCommon.myCstr(grow.Cells("Tax10").Value.ToString()) + "','" + clsCommon.myCstr(clsCommon.myCdbl(grow.Cells("Tax10 Rate").Value)) + "','" + clsCommon.myCstr(grow.Cells("Transporter").Value.ToString()) + "','" + userCode + "','" + connectSql.serverDate(trans) + "','" + userCode + "','" + connectSql.serverDate(trans) + "','" + companyCode + "','" + clsCommon.myCstr(grow.Cells("PAN").Value.ToString()) + "','" + interbranch + "','" + strTagAsFranchise + "','VSP','" + statecode + "','" + countrycode + "','" + clsCommon.myCstr(commsn) + "','" + clsCommon.myCstr(incentv) + "','" + clsCommon.myCstr(noofdays) + "','" + clsCommon.myCstr(vsppaymnt) + "','" + clsCommon.myCstr(payeename) + "','" + jointname + "','" + srvctype + "','" + clsCommon.myCstr(paymnt_commsn) + "','" + strBrachName + "','" + strAccNo + "','" + strBName + "','" + strIFSCCode + "','" + strAccType + "','" + strAgreement.ToUpper().Trim() + "'," & strAgreementDate & "," & strExpiryDate & ",'" + NameOfBank + "','" + AccountNo + "'," + PC_Code + ",'E'," + MP_Code + ",'" + MP_name + "','" & is_drip_saver & "','" & Joint_Branch_Name & "','" & Joint_IFSC_Code & "','" & Head_Load_Amt & "','" & Head_Load_DistanceKM & "','" & IIf(Head_Load_Amt > 0, "T", "F") & "','" & Own_Asset_Amt & "','" & IIf(Own_Asset_Amt > 0, "T", "F") & "','" & IIf(Service_Basis_Head_Load.ToString.Contains("K"), "K", IIf(Service_Basis_Head_Load.ToString.Contains("L"), "L", "")) & "','" & IIf(Service_Basis_Own_Asset.ToString.Contains("K"), "K", IIf(Service_Basis_Own_Asset.ToString.Contains("L"), "L", "")) & "','" & Cheque_In_favour_of & "','N','N','" + strEMPType + "', '" + clsCommon.myCstr(dblFixedAmount) + "','" + clsCommon.myCstr(EMPSlab1) + "','" + clsCommon.myCstr(EMPSlab1Value) + "','" + clsCommon.myCstr(EMPSlab2) + "', '" + clsCommon.myCstr(EMPSlab2Value) + "','" + clsCommon.myCstr(EMPSlab3) + "','" + clsCommon.myCstr(EMPSlab3Value) + "','" + clsCommon.myCstr(EMPSlab4) + "','" + clsCommon.myCstr(EMPSlab4Value) + "', '" + clsCommon.myCstr(EMPSlab5) + "', '" + clsCommon.myCstr(EMPSlab5Value) + "'," & MultipleIncentive & "," + strSecurityDeductionAmount + "," + strInterestPer + "," + strMinimumInterest + ",'" + clsCommon.myCstr(IsBlacklisted) + "','" + strServiceChargePerUnit + "','" & objCommonVar.BaseCurrencyCode & "','" + strAadharNo + "','" + strCareOf + "','" + strSecChequeNoLac1 + "','" + strSecChequeNoRs100 + "')"
                        End If

                        connectSql.RunSqlTransaction(trans, strcmd)


                        If objCommonVar.GSTApplicable Then
                            GSTQry = "Update TSPL_VENDOR_MASTER set GSTRegistered='" & Registered & "',GSTEntity='" & GSTEntity & "',GSTLastEntity='" & GSTLastEntity & "',GSTFinalNo='" & GSTFinal & "',GSTMiddle='" & GSTMiddleEntity & "' where Vendor_Code='" & strvendorNo & "'"
                            connectSql.RunSqlTransaction(trans, GSTQry)
                        End If
                        ''richa  UDL/02/07/21-001039
                        Dim DBEntry As Double
                        DBEntry = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select COUNT(*) As Row From TSPL_TDS_VENDOR_DETAILS Where Vendor_Code ='" & clsCommon.myCstr(strvendorNo) & "'", trans))
                        If clsCommon.CompairString(IsTaxApp, "1") = CompairStringResult.Equal Then
                            If DBEntry = 0 Then
                                clsDBFuncationality.SaveAStorePorcedure(trans, "SP_TDS_VENDOR_DETAILS_INSERT", New SqlParameter("@Vendor_Code", strvendorNo), New SqlParameter("@Nature_Of_Deduction", strDedCode.Replace("'", "")), New SqlParameter("@State_Code", StateCodeCommon.Replace("'", "")), New SqlParameter("@Pan", clsCommon.myCstr(grow.Cells("PAN").Value)), New SqlParameter("@Vendor_Type", strTDSVenType.Replace("'", "")), New SqlParameter("@status", strTDSState), New SqlParameter("@Branch_Code", strTDSBranch.Replace("'", "")), New SqlParameter("@Inactive", "N"), New SqlParameter("@createdby", userCode), New SqlParameter("@createddate", connectSql.serverDate(trans)), New SqlParameter("@modifiedby", userCode), New SqlParameter("@modifieddate", connectSql.serverDate(trans)), New SqlParameter("@compcode", companyCode))
                            Else
                                PartyDetailsQry = "UPDATE TSPL_TDS_VENDOR_DETAILS SET  Nature_Of_Deduction=" & strDedCode & ",State_Code=" & StateCodeCommon & ",Vendor_TYpe='" & strTDSVenType & "',Status='" & strTDSState & "',Branch_Code=" & strTDSBranch & ",Pan='" & clsCommon.myCstr(grow.Cells("PAN").Value) & "' where Vendor_Code='" + strvendorNo + "'"
                                connectSql.RunSqlTransaction(trans, PartyDetailsQry)
                            End If
                        Else
                            PartyDetailsQry = "UPDATE TSPL_TDS_VENDOR_DETAILS SET Nature_Of_Deduction=NULL,State_Code=NULL,Vendor_TYpe='Individual',Status='Resident',Branch_Code=NULL,Pan='" & clsCommon.myCstr(grow.Cells("PAN").Value) & "' where Vendor_Code='" + strvendorNo + "'"
                        End If


                    Else
                        clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strvendorNo, "TSPL_VENDOR_MASTER", "Vendor_Code", trans)
                        Dim strcmd As String
                        ''richa agarwal add FAt/SNF KG in Head Load Unit against Ticket No.MIL/17/01/19-000031
                        If clsCommon.CompairString(strAgreement, "Yes") = CompairStringResult.Equal Then
                            strcmd = "Update  TSPL_VENDOR_MASTER set  Vendor_Name='" + strvendorname + "',add1='" + add1 + "',add2='" + add2 + "',add3='" + add3 + "',Pin_Code='" & Pin_Code & "',Closing_Date='" + closing_date + "',Vendor_Group_Code='" + strgroupCode + "',Vendor_Group_Code_Desc='" + strgroupDes + "',City_Code='" + citycode + "',City_Code_Desc='" + citycodedesc + "',State='" + state + "',Country='" + country + "',Phone1='" + phonenum1 + "',Phone2='" + phonenum2 + "',Fax='" + fax + "',Email='" + emailid + "',WebSite='" + website + "',Terms_Code='" + strtermcode + "',Terms_Code_Desc='" + strtermdes + "' ,Vendor_Account='" + vendoracct + "',Vendor_Account_Desc='" + vendoracctdesc + "',Payment_Code='" + paymenttype + "',Payment_Code_Desc='" + paymenttypedesc + "',Bank_Code='" + strbank + "', Bank_Code_Desc='" + strbankdes + "',Ven_Type_Code='" + strvendortype + "',Ven_Type_Desc='" + strvendortypedes + "' ,Tax_Group='" + strTax + "',Tax_Group_Desc='" + strtaxdes + "' ,TAX1='" + grow.Cells("Tax1").Value.ToString() + "',TAX1_Rate='" + clsCommon.myCstr(clsCommon.myCdbl(grow.Cells("Tax1 Rate").Value)) + "',TAX2='" + grow.Cells("Tax2").Value.ToString() + "',TAX2_Rate='" + clsCommon.myCstr(clsCommon.myCdbl(grow.Cells("Tax2 Rate").Value)) + "',TAX3='" + grow.Cells("Tax3").Value.ToString() + "',TAX3_Rate='" + clsCommon.myCstr(clsCommon.myCdbl(grow.Cells("Tax3 Rate").Value)) + "',TAX4='" + grow.Cells("Tax4").Value.ToString() + "',TAX4_Rate='" + clsCommon.myCstr(clsCommon.myCdbl(grow.Cells("Tax4 Rate").Value)) + "',TAX5='" + grow.Cells("Tax5").Value.ToString() + "',TAX5_Rate='" + clsCommon.myCstr(clsCommon.myCdbl(grow.Cells("Tax5 Rate").Value)) + "',TAX6='" + grow.Cells("Tax6").Value.ToString() + "',TAX6_Rate='" + clsCommon.myCstr(clsCommon.myCdbl(grow.Cells("Tax6 Rate").Value)) + "',TAX7='" + grow.Cells("Tax7").Value.ToString() + "',TAX7_Rate='" + clsCommon.myCstr(clsCommon.myCdbl(grow.Cells("Tax7 Rate").Value)) + "',TAX8='" + grow.Cells("Tax8").Value.ToString() + "',TAX8_Rate='" + clsCommon.myCstr(clsCommon.myCdbl(grow.Cells("Tax8 Rate").Value)) + "',TAX9='" + grow.Cells("Tax9").Value.ToString() + "',TAX9_Rate='" + clsCommon.myCstr(clsCommon.myCdbl(grow.Cells("Tax9 Rate").Value)) + "',TAX10='" + grow.Cells("Tax10").Value.ToString() + "',TAX10_Rate='" + clsCommon.myCstr(clsCommon.myCdbl(grow.Cells("Tax10 Rate").Value)) + "',Transporter='" + grow.Cells("Transporter").Value.ToString() + "',modify_by='" + userCode + "',modify_date='" + connectSql.serverDate(trans) + "',comp_code='" + companyCode + "',PAN='" + grow.Cells("PAN").Value.ToString() + "',Inter_Branch='" + interbranch + "', franchise_yn='" + strTagAsFranchise + "',form_type='VSP',state_code='" + statecode + "',country_code='" + countrycode + "',commision_pers='" + clsCommon.myCstr(commsn) + "',incentive='" + clsCommon.myCstr(incentv) + "',incentive_days='" + clsCommon.myCstr(noofdays) + "',vsp_payment='" + clsCommon.myCstr(vsppaymnt) + "',vsp_payee_name='" + clsCommon.myCstr(payeename) + "',Joint_Name='" + jointname + "',Service_Charge_Type='" + srvctype + "',payment_commision_pers='" + clsCommon.myCstr(paymnt_commsn) + "',Branch_Name='" + strBrachName + "',Account_No='" + strAccNo + "',Bank_Name='" + strBName + "',IFSC_Code='" + strIFSCCode + "',Account_Type='" + strAccType + "',Agreement ='" + strAgreement.ToUpper().Trim() + "',Start_Date=Convert(date,'" & strAgreementDate & "',103),End_Date =Convert(date,'" & strExpiryDate & "',103),Joint_bank_Code='" + NameOfBank + "',Joint_Account_No='" + AccountNo + "',PC_CODE=" + PC_Code + ",Nature='E',MP_code=" + MP_Code + ",MP_Name='" + MP_name + "',is_Drip_saver='" & is_drip_saver & "',Joint_IFSC_Code='" & Joint_IFSC_Code & "',Joint_Branch_Name='" & Joint_Branch_Name & "',Rate_Head_Load=" & clsCommon.myCdbl(Head_Load_Amt) & ",DistanceKM_Head_Load=" & clsCommon.myCdbl(Head_Load_DistanceKM) & ",Rate_Own_Asset=" & clsCommon.myCdbl(Own_Asset_Amt) & ",is_Head_Load='" & IIf(Head_Load_Amt > 0, "T", "F") & "',is_Own_Asset='" & IIf(Own_Asset_Amt > 0, "T", "F") & "',Service_Basis_Head_Load='" & IIf(Service_Basis_Head_Load.ToString.Contains("K"), "K", IIf(Service_Basis_Head_Load.ToString.Contains("L"), "L", IIf(Service_Basis_Head_Load.ToString.Contains("W"), "W", ""))) & "',service_basis_Own_Asset='" & IIf(Service_Basis_Own_Asset.ToString.Contains("K"), "K", IIf(Service_Basis_Own_Asset.ToString.Contains("L"), "L", "")) & "',Cheque_in_Favour_of='" & Cheque_In_favour_of & "',Status='N',Onhold='N'  ,EMP_Type='" + strEMPType + "',EMP_Fixed_Amount= '" + clsCommon.myCstr(dblFixedAmount) + "',Actual_charges_Slab='" + clsCommon.myCstr(EMPSlab1) + "',Actual_charges='" + clsCommon.myCstr(EMPSlab1Value) + "',Actual_charges_Slab2 ='" + clsCommon.myCstr(EMPSlab2) + "',Actual_charges2 = '" + clsCommon.myCstr(EMPSlab2Value) + "',Actual_charges_Slab3 ='" + clsCommon.myCstr(EMPSlab3) + "',Actual_charges3 ='" + clsCommon.myCstr(EMPSlab3Value) + "',Actual_charges_Slab4 ='" + clsCommon.myCstr(EMPSlab4) + "',Actual_charges4='" + clsCommon.myCstr(EMPSlab4Value) + "', Actual_charges_Slab5 ='" + clsCommon.myCstr(EMPSlab5) + "', Actual_charges5 ='" + clsCommon.myCstr(EMPSlab5Value) + "',Apply_Mult_Incentive=" & MultipleIncentive & ",Security_Deduction_Amount=" + strSecurityDeductionAmount + ",Interest_Per=" + strInterestPer + ",Minimum_Interest=" + strMinimumInterest + ",Is_Blacklist='" & clsCommon.myCstr(IsBlacklisted) & "',Service_Charge_Per_Unit='" + strServiceChargePerUnit + "',Currency_Code='" & objCommonVar.BaseCurrencyCode & "',Aadhar_No='" + strAadharNo + "',Care_Of='" + strCareOf + "',SecChequeNoLac1='" + strSecChequeNoLac1 + "',SecChequeNoRs100='" + strSecChequeNoRs100 + "' where vendor_code='" + strvendorNo + "' and form_type='VSP'"
                        Else
                            strcmd = "Update  TSPL_VENDOR_MASTER set  Vendor_Name='" + strvendorname + "',add1='" + add1 + "',add2='" + add2 + "',add3='" + add3 + "',Pin_Code='" & Pin_Code & "',Closing_Date='" + closing_date + "',Vendor_Group_Code='" + strgroupCode + "',Vendor_Group_Code_Desc='" + strgroupDes + "',City_Code='" + citycode + "',City_Code_Desc='" + citycodedesc + "',State='" + state + "',Country='" + country + "',Phone1='" + phonenum1 + "',Phone2='" + phonenum2 + "',Fax='" + fax + "',Email='" + emailid + "',WebSite='" + website + "',Terms_Code='" + strtermcode + "',Terms_Code_Desc='" + strtermdes + "' ,Vendor_Account='" + vendoracct + "',Vendor_Account_Desc='" + vendoracctdesc + "',Payment_Code='" + paymenttype + "',Payment_Code_Desc='" + paymenttypedesc + "',Bank_Code='" + strbank + "', Bank_Code_Desc='" + strbankdes + "',Ven_Type_Code='" + strvendortype + "',Ven_Type_Desc='" + strvendortypedes + "' ,Tax_Group='" + strTax + "',Tax_Group_Desc='" + strtaxdes + "' ,TAX1='" + grow.Cells("Tax1").Value.ToString() + "',TAX1_Rate='" + clsCommon.myCstr(clsCommon.myCdbl(grow.Cells("Tax1 Rate").Value)) + "',TAX2='" + grow.Cells("Tax2").Value.ToString() + "',TAX2_Rate='" + clsCommon.myCstr(clsCommon.myCdbl(grow.Cells("Tax2 Rate").Value)) + "',TAX3='" + grow.Cells("Tax3").Value.ToString() + "',TAX3_Rate='" + clsCommon.myCstr(clsCommon.myCdbl(grow.Cells("Tax3 Rate").Value)) + "',TAX4='" + grow.Cells("Tax4").Value.ToString() + "',TAX4_Rate='" + clsCommon.myCstr(clsCommon.myCdbl(grow.Cells("Tax4 Rate").Value)) + "',TAX5='" + grow.Cells("Tax5").Value.ToString() + "',TAX5_Rate='" + clsCommon.myCstr(clsCommon.myCdbl(grow.Cells("Tax5 Rate").Value)) + "',TAX6='" + grow.Cells("Tax6").Value.ToString() + "',TAX6_Rate='" + clsCommon.myCstr(clsCommon.myCdbl(grow.Cells("Tax6 Rate").Value)) + "',TAX7='" + grow.Cells("Tax7").Value.ToString() + "',TAX7_Rate='" + clsCommon.myCstr(clsCommon.myCdbl(grow.Cells("Tax7 Rate").Value)) + "',TAX8='" + grow.Cells("Tax8").Value.ToString() + "',TAX8_Rate='" + clsCommon.myCstr(clsCommon.myCdbl(grow.Cells("Tax8 Rate").Value)) + "',TAX9='" + grow.Cells("Tax9").Value.ToString() + "',TAX9_Rate='" + clsCommon.myCstr(clsCommon.myCdbl(grow.Cells("Tax9 Rate").Value)) + "',TAX10='" + grow.Cells("Tax10").Value.ToString() + "',TAX10_Rate='" + clsCommon.myCstr(clsCommon.myCdbl(grow.Cells("Tax10 Rate").Value)) + "',Transporter='" + grow.Cells("Transporter").Value.ToString() + "',modify_by='" + userCode + "',modify_date='" + connectSql.serverDate(trans) + "',comp_code='" + companyCode + "',PAN='" + grow.Cells("PAN").Value.ToString() + "',Inter_Branch='" + interbranch + "', franchise_yn='" + strTagAsFranchise + "',form_type='VSP',state_code='" + statecode + "',country_code='" + countrycode + "',commision_pers='" + clsCommon.myCstr(commsn) + "',incentive='" + clsCommon.myCstr(incentv) + "',incentive_days='" + clsCommon.myCstr(noofdays) + "',vsp_payment='" + clsCommon.myCstr(vsppaymnt) + "',vsp_payee_name='" + clsCommon.myCstr(payeename) + "',Joint_Name='" + jointname + "',Service_Charge_Type='" + srvctype + "',payment_commision_pers='" + clsCommon.myCstr(paymnt_commsn) + "',Branch_Name='" + strBrachName + "',Account_No='" + strAccNo + "',Bank_Name='" + strBName + "',IFSC_Code='" + strIFSCCode + "',Account_Type='" + strAccType + "',Agreement ='" + strAgreement.ToUpper().Trim() + "',Start_Date=NULL,End_Date =NULL,Joint_bank_Code='" + NameOfBank + "',Joint_Account_No='" + AccountNo + "',PC_CODE=" + PC_Code + ",Nature='E',MP_code=" + MP_Code + ",MP_Name='" + MP_name + "',is_Drip_saver='" & is_drip_saver & "',Joint_IFSC_Code='" & Joint_IFSC_Code & "',Joint_Branch_Name='" & Joint_Branch_Name & "',Rate_Head_Load=" & clsCommon.myCdbl(Head_Load_Amt) & ",DistanceKM_Head_Load=" & clsCommon.myCdbl(Head_Load_DistanceKM) & ",Rate_Own_Asset=" & clsCommon.myCdbl(Own_Asset_Amt) & ",is_Head_Load='" & IIf(Head_Load_Amt > 0, "T", "F") & "',is_Own_Asset='" & IIf(Own_Asset_Amt > 0, "T", "F") & "',Service_Basis_Head_Load='" & IIf(Service_Basis_Head_Load.ToString.Contains("K"), "K", IIf(Service_Basis_Head_Load.ToString.Contains("L"), "L", IIf(Service_Basis_Head_Load.ToString.Contains("W"), "W", ""))) & "',service_basis_Own_Asset='" & IIf(Service_Basis_Own_Asset.ToString.Contains("K"), "K", IIf(Service_Basis_Own_Asset.ToString.Contains("L"), "L", "")) & "',Cheque_in_Favour_of='" & Cheque_In_favour_of & "',Status='N',Onhold='N'  ,EMP_Type='" + strEMPType + "',EMP_Fixed_Amount= '" + clsCommon.myCstr(dblFixedAmount) + "',Actual_charges_Slab='" + clsCommon.myCstr(EMPSlab1) + "',Actual_charges='" + clsCommon.myCstr(EMPSlab1Value) + "',Actual_charges_Slab2 ='" + clsCommon.myCstr(EMPSlab2) + "',Actual_charges2 = '" + clsCommon.myCstr(EMPSlab2Value) + "',Actual_charges_Slab3 ='" + clsCommon.myCstr(EMPSlab3) + "',Actual_charges3 ='" + clsCommon.myCstr(EMPSlab3Value) + "',Actual_charges_Slab4 ='" + clsCommon.myCstr(EMPSlab4) + "',Actual_charges4='" + clsCommon.myCstr(EMPSlab4Value) + "', Actual_charges_Slab5 ='" + clsCommon.myCstr(EMPSlab5) + "', Actual_charges5 ='" + clsCommon.myCstr(EMPSlab5Value) + "',Apply_Mult_Incentive=" & MultipleIncentive & ",Security_Deduction_Amount=" + strSecurityDeductionAmount + ",Interest_Per=" + strInterestPer + ",Minimum_Interest=" + strMinimumInterest + ",Is_Blacklist='" & clsCommon.myCstr(IsBlacklisted) & "',Service_Charge_Per_Unit='" + strServiceChargePerUnit + "',Currency_Code='" & objCommonVar.BaseCurrencyCode & "',Aadhar_No='" + strAadharNo + "',Care_Of='" + strCareOf + "',SecChequeNoLac1='" + strSecChequeNoLac1 + "',SecChequeNoRs100='" + strSecChequeNoRs100 + "' where vendor_code='" + strvendorNo + "' and form_type='VSP'"
                        End If
                        connectSql.RunSqlTransaction(trans, strcmd)

                        If objCommonVar.GSTApplicable Then
                            GSTQry = "Update TSPL_VENDOR_MASTER set GSTRegistered='" & Registered & "',GSTEntity='" & GSTEntity & "',GSTLastEntity='" & GSTLastEntity & "',GSTFinalNo='" & GSTFinal & "',GSTMiddle='" & GSTMiddleEntity & "' where Vendor_Code='" & strvendorNo & "'"
                            connectSql.RunSqlTransaction(trans, GSTQry)
                        End If

                        Dim DBEntry As Double
                        DBEntry = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select COUNT(*) As Row From TSPL_TDS_VENDOR_DETAILS Where Vendor_Code ='" & clsCommon.myCstr(strvendorNo) & "'", trans))
                        If clsCommon.CompairString(IsTaxApp, "1") = CompairStringResult.Equal Then
                            If DBEntry = 0 Then
                                clsDBFuncationality.SaveAStorePorcedure(trans, "SP_TDS_VENDOR_DETAILS_INSERT", New SqlParameter("@Vendor_Code", strvendorNo), New SqlParameter("@Nature_Of_Deduction", strDedCode.Replace("'", "")), New SqlParameter("@State_Code", StateCodeCommon.Replace("'", "")), New SqlParameter("@Pan", clsCommon.myCstr(grow.Cells("PAN").Value)), New SqlParameter("@Vendor_Type", strTDSVenType.Replace("'", "")), New SqlParameter("@status", strTDSState), New SqlParameter("@Branch_Code", strTDSBranch.Replace("'", "")), New SqlParameter("@Inactive", "N"), New SqlParameter("@createdby", userCode), New SqlParameter("@createddate", connectSql.serverDate(trans)), New SqlParameter("@modifiedby", userCode), New SqlParameter("@modifieddate", connectSql.serverDate(trans)), New SqlParameter("@compcode", companyCode))
                            Else
                                PartyDetailsQry = "UPDATE TSPL_TDS_VENDOR_DETAILS SET Nature_Of_Deduction=" & strDedCode & ",State_Code=" & StateCodeCommon & ",Vendor_TYpe='" & strTDSVenType & "',Status='" & strTDSState & "',Branch_Code=" & strTDSBranch & ",Pan='" & clsCommon.myCstr(grow.Cells("PAN").Value) & "' where Vendor_Code='" + strvendorNo + "'"
                                connectSql.RunSqlTransaction(trans, PartyDetailsQry)
                            End If
                        Else
                            PartyDetailsQry = "UPDATE TSPL_TDS_VENDOR_DETAILS SET  Nature_Of_Deduction=NULL,State_Code=NULL,Vendor_TYpe='Individual',Status='Resident',Branch_Code=NULL,Pan='" & clsCommon.myCstr(grow.Cells("PAN").Value) & "' where Vendor_Code='" + strvendorNo + "'"
                            connectSql.RunSqlTransaction(trans, PartyDetailsQry)
                        End If



                    End If
                    connectSql.RunSqlTransaction(trans, "UPDATE TSPL_VENDOR_MASTER SET Isbuyerfilereturninlasttwoyears=" + buyerfilereturninlasttwoyears + " , IsTCS_TDSamountgreaterthan50KpreviousYear = " + TCSTDSamountisgreaterthan50KinpreviousYear + ",Is_TDS_Applicable='" + IsTaxApp + "' ,TDS_State_Code =" + StateCodeCommon + ",TDS_Status='" + strTDSState + "' ,TDS_Vendor_Type ='" + strTDSVenType + "',Deduction_Code= " + strDedCode + ",TDS_Branch_Code=" + strTDSBranch + " where Vendor_Code='" + strvendorNo + "'")



                    Dim CorrectionFatSNF As String = "update TSPL_VENDOR_MASTER set "
                    If clsCommon.myLen(strCompanyBank) > 0 Then
                        qry += " Company_Bank ='" + strCompanyBank + "'"
                        CorrectionFatSNF += " Company_Bank ='" + strCompanyBank + "'"
                    Else
                        qry += " Company_Bank = null "
                        CorrectionFatSNF += " Company_Bank = null "
                    End If
                    CorrectionFatSNF += " , TIP_Buffalo='" & clsCommon.myCstr(clsCommon.myCdbl(grow.Cells("TIP Buffalo").Value)) & "',TIP_Cow='" & clsCommon.myCstr(clsCommon.myCdbl(grow.Cells("TIP Cow").Value)) & "',TIP_Mix='" & clsCommon.myCstr(clsCommon.myCdbl(grow.Cells("TIP Mix").Value)) & "',Monthly_Rent='" + clsCommon.myCstr(clsCommon.myCdbl(grow.Cells("Monthly Rent").Value)) + "', Handling_Charges_Per='" + clsCommon.myCstr(dclHandlingChargesPer) + "' , CorrectionFat='" & clsCommon.myCdbl(CorrectionFat) & "' , CorrectionSNF='" & clsCommon.myCdbl(CorrectionSNF) & "' , CURRENCY_CODE = '" & strCurrencyCode & "',Credit_Limit_On_Milk_Receipt_Per='" + clsCommon.myCstr(dclCreditLimitPer) + "' where Vendor_Code='" & strvendorNo & "'"
                    connectSql.RunSqlTransaction(trans, CorrectionFatSNF)

                    'sanjay
                    If clsCommon.CompairString(clsCommon.myCstr(grow.Cells("Create Customer(Y/N)").Value), "Y") = CompairStringResult.Equal Then
                        Dim obj As New clsCustomerMaster()
                        obj.Cust_Code = clsCommon.myCstr(strvendorNo)
                        obj.Customer_Name = clsCommon.myCstr(strvendorname)
                        obj.Alies_Name = ""
                        obj.Add1 = clsCommon.myCstr(add1)
                        obj.Add2 = clsCommon.myCstr(add2)
                        obj.Add3 = clsCommon.myCstr(add3)
                        obj.City_Code = clsCommon.myCstr(citycode)
                        obj.State = clsCommon.myCstr(state)
                        obj.Country = clsCommon.myCstr(country)
                        obj.Phone1 = clsCommon.myCstr(phonenum1)
                        obj.Phone2 = clsCommon.myCstr(phonenum2)
                        obj.Fax = clsCommon.myCstr(fax)
                        obj.Email = clsCommon.myCstr(emailid)
                        obj.WebSite = clsCommon.myCstr(website)
                        obj.Contact_Person_Name = ""
                        obj.Contact_Person_Phone = ""
                        obj.Contact_Person_Fax = ""
                        obj.Contact_Person_Email = ""
                        obj.Contact_Person_Website = ""
                        obj.CUSTOMER_FORM_TYPE = "VSP"
                        Dim strCmd1 As String = " SELECT Cust_Group_Code as [Customer Gruop Code],Cust_Group_Desc as [Description]," &
                          " Tax_Group as [Tax Group],Cust_Account as [Account Set],Terms_Code as [Terms Code] FROM [TSPL_CUSTOMER_GROUP_MASTER] where Cust_Group_Code='" + strCustomerGroupCode + "' "
                        Dim myDs As DataSet = connectSql.RunSQLReturnDS(trans, strCmd1)
                        If myDs.Tables(0).Rows.Count > 0 Then
                            Dim row As DataRow = myDs.Tables(0).Rows(0)
                            obj.Cust_Group_Code = clsCommon.myCstr(row(0).ToString().Trim())
                            obj.Tax_Group = clsCommon.myCstr(row(2).ToString().Trim())
                            obj.Cust_Account = clsCommon.myCstr(row(3).ToString().Trim())
                            obj.Terms_Code = clsCommon.myCstr(row(4).ToString().Trim())
                        End If

                        obj.TAX1 = clsCommon.myCstr(grow.Cells("Tax1").Value.ToString())
                        obj.TAX1_Rate = clsCommon.myCdbl(grow.Cells("Tax1 Rate").Value)
                        obj.TAX2 = clsCommon.myCstr(grow.Cells("Tax2").Value.ToString())
                        obj.TAX2_Rate = clsCommon.myCdbl(grow.Cells("Tax2 Rate").Value)
                        obj.TAX3 = clsCommon.myCstr(grow.Cells("Tax3").Value.ToString())
                        obj.TAX3_Rate = clsCommon.myCdbl(grow.Cells("Tax3 Rate").Value)
                        obj.TAX4 = clsCommon.myCstr(grow.Cells("Tax4").Value.ToString())
                        obj.TAX4_Rate = clsCommon.myCdbl(grow.Cells("Tax4 Rate").Value)
                        obj.TAX5 = clsCommon.myCstr(grow.Cells("Tax5").Value.ToString())
                        obj.TAX5_Rate = clsCommon.myCdbl(grow.Cells("Tax5 Rate").Value)
                        obj.TAX6 = clsCommon.myCstr(grow.Cells("Tax6").Value.ToString())
                        obj.TAX6_Rate = clsCommon.myCdbl(grow.Cells("Tax6 Rate").Value)
                        obj.TAX7 = clsCommon.myCstr(grow.Cells("Tax7").Value.ToString())
                        obj.TAX7_Rate = clsCommon.myCdbl(grow.Cells("Tax7 Rate").Value)
                        obj.TAX8 = clsCommon.myCstr(grow.Cells("Tax8").Value.ToString())
                        obj.TAX8_Rate = clsCommon.myCdbl(grow.Cells("Tax8 Rate").Value)
                        obj.TAX9 = clsCommon.myCstr(grow.Cells("Tax9").Value.ToString())
                        obj.TAX9_Rate = clsCommon.myCdbl(grow.Cells("Tax9 Rate").Value)
                        obj.TAX10 = clsCommon.myCstr(grow.Cells("Tax10").Value.ToString())
                        obj.TAX10_Rate = clsCommon.myCdbl(grow.Cells("Tax10 Rate").Value)

                        obj.Payment_Code = clsCommon.myCstr(paymenttype)
                        obj.Service_Tax_No = ""
                        obj.Tin_No = ""
                        obj.Lst_No = ""

                        obj.PIN_NO = clsCommon.myCstr(Pin_Code)
                        obj.Remarks1 = ""
                        obj.Remarks2 = ""
                        obj.Additional1 = ""
                        obj.Additional2 = ""
                        obj.Additional3 = ""
                        obj.OutLet_Commossion = clsCommon.myCdbl(0)
                        obj.Balance_ToDate = 0
                        obj.Credit_Limit = clsCommon.myCdbl(dclCreditLimitPer)
                        obj.CST = ""
                        obj.ECC = ""
                        obj.Range = ""
                        obj.Collectorate = clsCommon.myCstr(clsCommon.myCstr(grow.Cells("Collectorate").Value.ToString()))
                        obj.PAN = grow.Cells("PAN").Value.ToString()
                        obj.Credit_Customer = "N"

                        obj.LastInvoice_No = Nothing
                        obj.LastInvoice_Date = Nothing
                        obj.Inter_Branch = "N"

                        obj.IsDistributor = "N"

                        obj.prntcustyn = "N"

                        obj.CSA_Type = "N"
                        obj.ManualCustomer = "N"

                        obj.Comp_Code = objCommonVar.CurrentCompanyCode
                        obj.CURRENCY_CODE = clsCommon.myCstr(strCurrencyCode)
                        Dim arrDBName As New List(Of String)
                        arrDBName.Add(clsCommon.myCstr(objCommonVar.CurrDatabase))


                        obj.GSTNO = clsCommon.myCstr(GSTFinal)
                        obj.GSTEntity = clsCommon.myCstr(GSTEntity)
                        obj.GSTBlank = clsCommon.myCstr(GSTMiddleEntity)
                        obj.GSTDigit = clsCommon.myCstr(GSTLastEntity)
                        obj.GST_Registered = Registered
                        obj.Arr_CrateAccount = New List(Of clsCustomerCrateAccounting)
                        Dim ii2 As Integer = clsDBFuncationality.getSingleValue("select count(*) from tspl_customer_master where CUSTOMER_FORM_TYPE='VSP' and cust_code='" + strvendorNo + "'", trans)
                        If obj.SaveData(obj, obj.ArrVisi, IIf(ii2 = 0, True, False), arrDBName, trans) = False Then
                            clsCommon.ProgressBarHide()
                            clsCommon.MyMessageBoxShow("Error in Create Customer,See At Line No. " + clsCommon.myCstr(counter) + "")
                            Exit Sub
                        End If

                        'Customer Vendor mapping
                        Dim ii As Integer = clsDBFuncationality.getSingleValue("Select COUNT(*) from TSPL_CUSTOMER_VENDOR_MAPPING WHERE cust_code='" + strvendorNo + "'", trans)
                        If ii = 0 Then
                            Dim qry12 As String = "insert into TSPL_CUSTOMER_VENDOR_MAPPING values('" + strvendorNo + "','" + strvendorNo + "') "
                            clsDBFuncationality.ExecuteNonQuery(qry12, trans)
                        End If
                    End If


                    counter += 1
                    clsCommon.ProgressBarUpdate("Imported Receords  : " & counter & "/" & gv.Rows.Count)
                Next
                trans.Commit()
                clsCommon.ProgressBarHide()
                common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                trans.Rollback()
                clsCommon.ProgressBarHide()
                clsCommon.MyMessageBoxShow("Line: " + Count + " - " + ex.Message)

            End Try

        End If
        Me.Controls.Remove(gv)
    End Sub

    Private Sub chkMultIncentive_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkMultIncentive.ToggleStateChanged
        If chkMultIncentive.Checked Then
            txtIncentiveMult.Enabled = True
            FndIncentive.Enabled = False
            FndIncentive.Value = ""
        Else
            txtIncentiveMult.Enabled = False
            txtIncentiveMult.arrValueMember = Nothing
            FndIncentive.Enabled = True
        End If
    End Sub

    Private Sub txtIncentiveMult__My_Click(sender As Object, e As EventArgs) Handles txtIncentiveMult._My_Click
        Dim qry As String = " select INCENTIVE_CODE as Code,DESCRIPTION as Name,INCENTIVE_DATE as Date,INCENTIVE_TYPE as IncentiveType,SCHEME_FOR as [Scheme For],Calc_Type as [Calculation Type],Rate_Type as [Rate Type],Qty_Type as [Quantity Type] from TSPL_INCENTIVE_MASTER_HEAD "
        '' get already selected data
        Dim qrySel As String = "select Vendor_Code,INCENTIVE_CODE from TSPL_VSP_INCENTIVE where Vendor_Code='" & fndvendorNo.Value & "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qrySel)
        Dim arr As New ArrayList
        For Each dr As DataRow In dt.Rows
            arr.Add(clsCommon.myCstr(dr.Item("INCENTIVE_CODE")))
        Next
        If txtIncentiveMult.arrValueMember IsNot Nothing AndAlso txtIncentiveMult.arrValueMember.Count <= 0 Then
            txtIncentiveMult.arrValueMember = arr
        End If
        txtIncentiveMult.arrValueMember = clsCommon.ShowMultipleSelectForm("IncenMulSel", qry, "Code", "Name", txtIncentiveMult.arrValueMember, txtIncentiveMult.arrDispalyMember)

    End Sub

    Private Sub RadMenuItem1_Click(sender As Object, e As EventArgs) Handles RadMenuItem1.Click
        Try
            Dim qry As String
            qry = "select count(*) from TSPL_VSP_INCENTIVE"
            If clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry)) > 0 Then
                qry = "SELECT VENDOR_CODE as [VSP Code],INCENTIVE_CODE as [Incentive Code] FROM TSPL_VSP_INCENTIVE where VENDOR_CODE in (select Vendor_Code from TSPL_VENDOR_MASTER where form_type='VSP')"
            Else
                qry = "SELECT '' as [VSP Code],'' as [Incentive Code] FROM TSPL_VSP_INCENTIVE"
            End If
            ListImpExpColumnsMandatory = New List(Of String)({"VSP Code", "Incentive Code"})
            ListImpExpColumnsSuperMandatory = New List(Of String)({"VSP Code", "Incentive Code"})
            transportSql.ExporttoExcel(qry, "", "", Me, ListImpExpColumnsMandatory, ListImpExpColumnsSuperMandatory, MyBase.Form_ID + "INCENTIVE")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "UOM")
        End Try
    End Sub

    Private Sub RadMenuItem2_Click(sender As Object, e As EventArgs) Handles RadMenuItem2.Click
        Try
            ImportIncentiveDetails()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Private Sub ImportIncentiveDetails()
        Dim gvCharges As New RadGridView()
        Me.Controls.Add(gvCharges)
        Dim countDefaultUOM As Integer = 0
        If transportSql.importExcel(gvCharges, "VSP Code", "Incentive Code") Then
            Dim isSaved As Boolean = True
            Dim currentdate As Date = Date.Today
            Dim trans As SqlTransaction = Nothing
            clsCommon.ProgressBarShow()
            Try
                trans = clsDBFuncationality.GetTransactin()
                For i As Integer = 0 To gvCharges.Rows.Count - 1
                    clsDBFuncationality.ExecuteNonQuery("DELETE FROM TSPL_VSP_INCENTIVE where Vendor_Code = '" & clsCommon.myCstr(gvCharges.Rows(i).Cells("VSP CODE").Value) & "'", trans)
                Next
                For Each grow As GridViewRowInfo In gvCharges.Rows
                    Dim LineNo As String = clsCommon.myCstr(grow.Index) + 2
                    Dim coll As New Hashtable()

                    Dim strVSPCode As String
                    Dim VSPCode As String = clsCommon.myCstr(grow.Cells("VSP Code").Value)
                    If clsCommon.myLen(VSPCode) >= 0 Then
                        strVSPCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Vendor_Code from TSPL_VENDOR_MASTER Where Vendor_Code ='" + VSPCode + "' And Form_Type ='VSP'", trans))
                        If clsCommon.myLen(strVSPCode) <= 0 Then
                            Throw New Exception("VSP Code '" + VSPCode + "' does not exist .Please make its master first at line no '" + LineNo + "'")
                        End If
                    Else
                        Throw New Exception("Please insert VSP code in at line no '" + LineNo + "' ")
                    End If

                    Dim strChargeC As String
                    Dim ChargeCode As String = clsCommon.myCstr(grow.Cells("Incentive Code").Value)
                    If clsCommon.myLen(ChargeCode) > 0 Then
                        strChargeC = clsDBFuncationality.getSingleValue("Select INCENTIVE_CODE from TSPL_INCENTIVE_MASTER_HEAD Where INCENTIVE_CODE='" + ChargeCode + "'", trans)
                        If clsCommon.CompairString(strChargeC, ChargeCode) = CompairStringResult.Equal Then
                        Else
                            Throw New Exception("INCENTIVE CODE '" + ChargeCode + "' at line no '" + LineNo + "' does  not exist")
                        End If
                    Else
                        Throw New Exception("Please insert INCENTIVE CODE at Line No '" + LineNo + "' ")
                    End If

                    clsCommon.AddColumnsForChange(coll, "Vendor_Code", strVSPCode)
                    clsCommon.AddColumnsForChange(coll, "INCENTIVE_CODE", ChargeCode)

                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_VSP_INCENTIVE", OMInsertOrUpdate.Insert, "", trans)

                Next
                If isSaved Then

                    clsCommon.ProgressBarHide()
                    trans.Commit()
                    RadMessageBox.Show("Data Imported Successfully.")
                Else
                    Throw New Exception("Error in Import")
                End If
            Catch ex As Exception
                clsCommon.ProgressBarHide()
                trans.Rollback()
                RadMessageBox.Show(ex.Message)
            Finally
                Me.Controls.Remove(gvCharges)
            End Try
        End If
    End Sub

    Private Sub TxtPinCode_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtPinCode.KeyPress
        If Not Char.IsNumber(e.KeyChar) AndAlso Not e.KeyChar = Chr(Keys.Back) Then e.Handled = True
    End Sub

    Private Sub txtpan_TextChanged(sender As Object, e As EventArgs) Handles txtpan.TextChanged
        Try
            If clsCommon.myLen(txtpan.Text) <= 0 Then
                Exit Sub
            End If

            Dim msg As String = clsERPFuncationality.CheckPanStructure(txtpan.Text, txtvendorname.Text)
            txtGST_PanCode.Text = txtpan.Text
            If clsCommon.myLen(msg) > 0 Then
                pageCus.SelectedPage = RadPageViewPage1
                clsCommon.MyMessageBoxShow(msg, Me.Text)
                txtpan.Focus()
                txtpan.Select()
                Return
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub Rchkregistered_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles Rchkregistered.ToggleStateChanged
        Try
            If Rchkregistered.Checked Then
                txtGSTIN_No_final.Enabled = False
                txtEntity.Enabled = True
                MyTextBox2.Enabled = True
                txtGST_PanCode.Text = txtpan.Text
                If clsCommon.myLen(txtstatecode.Value) > 0 Then
                    txtGSTStateCode.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select GST_State_Code from tspl_state_master where state_code='" + txtstatecode.Value + "'"))
                End If
            Else
                txtEntity.Enabled = False
                MyTextBox2.Enabled = False
                txtGST_PanCode.Text = ""
                txtEntity.Text = ""
                txtGSTStateCode.Text = ""
                txtGSTIN_No_final.Text = ""
                MyTextBox2.Text = ""
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message())
        End Try
    End Sub

    Private Sub btnHistory_Click(sender As Object, e As EventArgs) Handles btnHistory.Click
        Try
            If clsCommon.myLen(fndvendorNo.Value) <= 0 Then
                clsCommon.MyMessageBoxShow("Select VSP Code")
                Exit Sub
            End If
            clsERPFuncationalityOLD.ShowHistoryData(fndvendorNo.Value, "Vendor_Code", "TSPL_Vendor_MASTER")
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
    Function ValidationMultiCurrencyForImport(ByVal strVendorCurrency As String, ByVal strVendorAccountSet As String, ByVal strTaxGroup As String, ByVal strlineNo As String, ByVal trans As SqlTransaction) As Boolean
        '' validation for multicurrency
        If clsCommon.myLen(clsCommon.myCstr(strVendorCurrency)) > 0 Then
            Dim qry As String
            qry = "select CURRENCY_CODE from TSPL_VENDOR_ACCOUNT_SET where Acct_Set_Code='" & clsCommon.myCstr(strVendorAccountSet) & "' "
            Dim accCurrCode As String = clsDBFuncationality.getSingleValue(qry, trans).ToString
            If clsCommon.CompairString(accCurrCode, clsCommon.myCstr(strVendorCurrency)) <> CompairStringResult.Equal Then
                clsCommon.MyMessageBoxShow("Account Set Currency and Vendor Currency must be same in case of Multicurrency. See At Line No :" + strlineNo) ',See At Line No.
                Return False
            End If
            '' match tax Group currency with vendor currency
            qry = " select TSPL_TAX_GROUP_DETAILS.Tax_Code,coalesce(TSPL_TAX_MASTER.CURRENCY_CODE,'') as CURRENCY_CODE from TSPL_TAX_GROUP_DETAILS " & _
                  " inner join TSPL_TAX_GROUP_MASTER on TSPL_TAX_GROUP_DETAILS.Tax_Group_Code=TSPL_TAX_GROUP_MASTER.Tax_Group_Code " & _
                  " inner join TSPL_TAX_MASTER on TSPL_TAX_GROUP_DETAILS.Tax_Code=TSPL_TAX_MASTER.Tax_Code where TSPL_TAX_GROUP_MASTER.Tax_Group_Code='" & clsCommon.myCstr(strTaxGroup) & "' " & _
                  " and coalesce(TSPL_TAX_MASTER.CURRENCY_CODE,'')<>'" & clsCommon.myCstr(strVendorCurrency) & "'"
            Dim dt As DataTable
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            Dim taxCode As String = ""
            For Each dr As DataRow In dt.Rows
                If dt.Rows.IndexOf(dr) = 0 Then
                    taxCode = dr.Item("Tax_Code")
                Else
                    taxCode = taxCode & "," & dr.Item("Tax_Code")
                End If
            Next
            If clsCommon.myLen(taxCode) > 0 Then
                clsCommon.MyMessageBoxShow("Tax Code '" & taxCode & "' in Tax Group " & clsCommon.myCstr(strTaxGroup) & " are created for currency other than " & clsCommon.myCstr(strVendorCurrency) & " .See At Line No :" + strlineNo)
                Return False
            End If
            'End If
            Return True
        End If
    End Function

    Private Sub chkCreditLimitBasedOnMilkReceipt_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkCreditLimitBasedOnMilkReceipt.ToggleStateChanged
        TxtCreditLimitBasedOnMilkReceipt.Enabled = chkCreditLimitBasedOnMilkReceipt.Checked
    End Sub

    Private Sub fndCusgrp__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndCusgrp._MYValidating
        Dim qry As String = " SELECT Cust_Group_Code as [CustomerGruopCode],Cust_Group_Desc as [Description]," & _
                    " Tax_Group as [Tax Group],Cust_Account as [Account Set],Terms_Code as [Terms Code] FROM [TSPL_CUSTOMER_GROUP_MASTER] "
        fndCusgrp.Value = clsCommon.ShowSelectForm("CUSGRP_CODE1", qry, "CustomerGruopCode", "", fndCusgrp.Value, "", isButtonClicked)
    End Sub

  
    Private Sub findfndbankcode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles findfndbankcode._MYValidating
        'fndbankcode.ConnectionString = connectSql.SqlCon()
        ''fndbankcode.Query = " select bank_code As [Bank Code],description  as [Description]from TSPL_Bank_MASTER "
        'fndbankcode.Query = clsERPFuncationality.glbankquery
        'fndbankcode.ValueToSelect = "Bank Code"
        'fndbankcode.Caption = "Bank Master"
        'fndbankcode.ValueToSelect1 = "Description"
        GetBankDetails(isButtonClicked)
        'Dim whrcls As String = ""
        'Dim qry As String = clsERPFuncationality.glbankquery(whrcls)
        'fndbankcode.Value = clsCommon.ShowSelectForm("fndbannk", qry, "Bank Code", whrcls, fndbankcode.Value, "", isButtonClicked)
        'txtbankcodedes.Text = clsDBFuncationality.getSingleValue("Select Description from TSPL_BANK_MASTER where BANK_CODE='" + fndbankcode.Value + "'")

    End Sub

    Private Sub findTxtIFSCCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles findTxtIFSCCode._MYValidating
        If clsCommon.myLen(findfndbankcode.Value) > 0 Then
            Dim qry As String = "Select Bank_IFSC_Code as IFSCCode,Branch_Name  from TSPL_Vendor_Bank_Branch_Details "
            findTxtIFSCCode.Value = clsCommon.ShowSelectForm("FormIFSCCode", qry, "IFSCCode", " Bank_Code ='" & findfndbankcode.Value & "' ", findTxtIFSCCode.Value, "", isButtonClicked)
            TxtBankBranch.Text = clsDBFuncationality.getSingleValue("Select Branch_Name from TSPL_Vendor_Bank_Branch_Details where Bank_Code ='" & findfndbankcode.Value & "' and Bank_IFSC_Code='" & findTxtIFSCCode.Value & "' ")
        Else
            clsCommon.MyMessageBoxShow("Please select Bank Code first")
        End If
    End Sub
    Private Sub fndVSPCopy__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndVSPCopy._MYValidating
        Try
            isLoadCopy = True
            fndVSPCopy.Value = clsVendorMaster.getFinder(" form_type='VSP'", fndVSPCopy.Value, isButtonClicked)
            If clsCommon.myLen(fndVSPCopy.Value) > 0 Then
                fndvendorNo.Value = fndVSPCopy.Value
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

    Private Sub ChkIsTDSApp_CheckStateChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkIsTDSApp.CheckStateChanged
        If ChkIsTDSApp.Checked = True Then
            GrpTDS.Enabled = True
            fnddeducNew.MendatroryField = True
            'fndbranchnew.MendatroryField = True
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
            'fndbranchnew.MendatroryField = False
        End If
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
    'Public Sub ToHindiInput()
    '    Try
    '        Dim CName As String = ""

    '        For Each lang As InputLanguage In InputLanguage.InstalledInputLanguages
    '            CName = lang.Culture.EnglishName.ToString()

    '            If CName.StartsWith("Hindi") Then
    '                InputLanguage.CurrentInputLanguage = lang
    '            End If
    '        Next
    '    Catch ex As Exception

    '    End Try

    'End Sub

    Private Sub txtvendornameHindi_Leave(sender As Object, e As EventArgs) Handles txtvendornameHindi.Leave
        clsMccMaster.ToEnglishInput()
    End Sub

    Private Sub txtCompanyBank__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtCompanyBank._MYValidating
        Try
            txtCompanyBank.Value = clsBankMaster.getFinder("", txtCompanyBank.Value, isButtonClicked)
            lblCompanyBank.Text = clsBankMaster.GetName(txtCompanyBank.Value)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtvendornameHindi_Enter(sender As Object, e As EventArgs) Handles txtvendornameHindi.Enter
        clsMccMaster.ToHindiInput()
    End Sub

    'Public Sub ToEnglishInput()
    '    Try
    '        Dim CName As String = ""

    '        For Each lang As InputLanguage In InputLanguage.InstalledInputLanguages
    '            CName = lang.Culture.EnglishName.ToString()

    '            If CName.StartsWith("English") Then
    '                InputLanguage.CurrentInputLanguage = lang
    '            End If
    '        Next
    '    Catch ex As Exception

    '    End Try

    'End Sub



End Class