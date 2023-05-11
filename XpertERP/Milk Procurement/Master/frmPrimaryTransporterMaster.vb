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

'created by --> Monika
'createddate --> 23/05/2014
'' updation by richa agarwal against ticket no BM00000006003 27/03/2015

Public Class FrmPrimaryTransporterMaster
    Inherits FrmMainTranScreen
    Dim userCode, companyCode As String
    Dim str As String
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim Errorcontrol As clsErrorControl = New clsErrorControl()
    Dim IsInsieLoadData As Boolean
    Dim Frm_Open As FrmMainTranScreen
    Dim isOneMCCOnePrimaryTranporter As Boolean = False
    Dim NotAllowDuplicatePANOnPrimaryTransporter As Boolean = False

    Const colDRMinutes As String = "colDRMinutes"
    Const colDRAmount As String = "colDRAmount"

    Sub LoadBlankGridDeduction()
        Try
            gvDeductionRange.Rows.Clear()
            gvDeductionRange.Columns.Clear()

            Dim repoNumBox As GridViewDecimalColumn = New GridViewDecimalColumn()
            repoNumBox.FormatString = "{0:n0}"
            repoNumBox.HeaderText = "Minutes"
            repoNumBox.Name = colDRMinutes
            repoNumBox.Width = 100
            repoNumBox.ReadOnly = False
            repoNumBox.Step = 0
            repoNumBox.ShowUpDownButtons = False
            repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
            gvDeductionRange.MasterTemplate.Columns.Add(repoNumBox)

            repoNumBox = New GridViewDecimalColumn()
            repoNumBox.FormatString = "{0:n3}"
            repoNumBox.HeaderText = "Deduction Amount"
            repoNumBox.Name = colDRAmount
            repoNumBox.Width = 300
            repoNumBox.ReadOnly = False
            repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
            repoNumBox.Step = 0
            repoNumBox.ShowUpDownButtons = False
            gvDeductionRange.MasterTemplate.Columns.Add(repoNumBox)

            gvDeductionRange.AllowDeleteRow = True
            gvDeductionRange.AllowAddNewRow = False
            gvDeductionRange.ShowGroupPanel = False
            gvDeductionRange.AllowColumnReorder = False
            gvDeductionRange.AllowRowReorder = False
            gvDeductionRange.EnableSorting = False
            gvDeductionRange.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
            gvDeductionRange.MasterTemplate.ShowRowHeaderColumn = False
            gvDeductionRange.AutoSizeRows = False
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Sub LoadBlankGvCheque()
        Try
            gvCheque.Rows.Clear()
            gvCheque.Columns.Clear()
            gvCheque.Columns.Add("COLCheck_No", "Cheque No")
            'gvCheque.Columns.Add("COLCheck_Date", "Cheque Date")

            Dim repoCode As GridViewDateTimeColumn = New GridViewDateTimeColumn()
            repoCode.FormatString = "{0:dd/MM/yyyy}"
            repoCode.HeaderText = "Cheque Date"
            repoCode.Name = "COLCheck_Date"
            repoCode.Width = 150
            repoCode.CustomFormat = "dd/MM/yyyy"
            gvCheque.MasterTemplate.Columns.Add(repoCode)

            gvCheque.Columns("COLCheck_No").Width = 100
            gvCheque.Columns("COLCheck_Date").Width = 150


            gvCheque.AllowAddNewRow = True
            gvCheque.MasterTemplate.AddNewRowPosition = SystemRowPosition.Bottom
            gvCheque.AllowEditRow = True
            gvCheque.AllowDeleteRow = True
            gvCheque.AllowRowResize = False
            gvCheque.AllowRowReorder = False
            gvCheque.AllowColumnResize = True
            gvCheque.AllowColumnChooser = False
            gvCheque.AllowAutoSizeColumns = False
            gvCheque.ShowGroupPanel = False
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
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

    Sub LoadAgreement()
        cmbagreemnt.DataSource = Nothing
        Dim dt As New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow = Nothing

        dr = dt.NewRow()
        dr("Code") = "NO"
        dr("Name") = "NO"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "YES"
        dr("Name") = "YES"
        dt.Rows.Add(dr)

        cmbagreemnt.DataSource = dt
        cmbagreemnt.DisplayMember = "Name"
        cmbagreemnt.ValueMember = "Code"
    End Sub

    Sub LoadSecurity()
        ' ''cmbsecurity.DataSource = Nothing
        ' ''Dim dt1 As New DataTable()
        ' ''dt1.Columns.Add("Code", GetType(String))
        ' ''dt1.Columns.Add("Name", GetType(String))

        ' ''Dim dr1 As DataRow = Nothing

        ' ''dr1 = dt1.NewRow()
        ' ''dr1("Code") = "NO"
        ' ''dr1("Name") = "NO"
        ' ''dt1.Rows.Add(dr1)

        ' ''dr1 = dt1.NewRow()
        ' ''dr1("Code") = "YES"
        ' ''dr1("Name") = "YES"
        ' ''dt1.Rows.Add(dr1)

        ' ''cmbsecurity.DataSource = dt1
        ' ''cmbsecurity.DisplayMember = "Name"
        ' ''cmbsecurity.ValueMember = "Code"
    End Sub

    Public Sub New(ByVal user As String, ByVal company As String)
        InitializeComponent()
        userCode = user
        companyCode = company
    End Sub

#Region "Page Load"
    Private Sub FrmPrimaryTransporterMaster_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load



        txtpan.CharacterCasing = CharacterCasing.Upper
        funreset()
        SetLength()
        pageCus.SelectedPage = RadPageViewPage1
        SetUserMgmtNew()
        ToolTipvendor.SetToolTip(btnnew, "New")
        fndvendorNo_text_changed()
        fndgroupcode_text_Changed()
        fndgroupcode_leave()
        chkInActive.Checked = False
        dtClosing.Enabled = False
        btndelete.Enabled = False
        btnsave.Enabled = True
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update Trasnaction")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnclear, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnnew, "Press Alt+N Adding New Trasnaction")
        txtcountrycode.Value = "INDIA"
        txtCountry.Text = "INDIA"
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
        End If
        txtvndrtype.Text = "PTM"
        chktrarns.Checked = True
        LoadAgreement()
        LoadSecurity()
        dtpStartDate.Value = clsCommon.GETSERVERDATE()
        dtpEndDate.Value = dtpStartDate.Value
        ''For Custom Fields
        LoadBlankGvCheque()
        LoadBlankGridDeduction()
        pageCus.Pages("pvpCustomFields").Item.Visibility = MyBase.customFieldTabProperty
        If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
            UcCustomFields1.Report_ID = MyBase.Form_ID
            UcCustomFields1.LoadCustomControls()
        End If
        ''End of For Custom Fields
        RadPageViewPage2.Item.Visibility = ElementVisibility.Collapsed
        RadPageViewPage5.Item.Visibility = ElementVisibility.Collapsed
        pvpCustomFields.Item.Visibility = ElementVisibility.Collapsed
        isOneMCCOnePrimaryTranporter = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.isOneMCCOnePrimaryTranporter, clsFixedParameterCode.isOneMCCOnePrimaryTranporter, Nothing)) > 0, True, False)
        NotAllowDuplicatePANOnPrimaryTransporter = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.NotAllowDuplicatePANOnPrimaryTransporter, clsFixedParameterCode.NotAllowDuplicatePANOnPrimaryTransporter, Nothing)) > 0, True, False)
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
        txtPhone1.MaxLength = 20
        txtPhone2.MaxLength = 20
        txtfax.MaxLength = 20
        txtEmail.MaxLength = 50
        txtWeb.MaxLength = 50
        txtContactName.MaxLength = 50
        txtContPhone.MaxLength = 20
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
        ''MyBase.SetUserMgmt(clsUserMgtCode.frmPrimaryTransporterMaster)
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow("Permission Denied")
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
                fndbankcode.Value = dr.Rows(0)("Bank_Code").ToString()
                'txtbankcodedes.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select bank_name from tspl_Vendor_bank_master where bank_code='" + fndbankcode.Value + "'"))
                'If clsCommon.myLen(txtbankcodedes.Text) <= 0 Then
                '    fndbankcode.Value = ""
                '    txtbankcodedes.Text = ""
                'End If

                If clsCommon.myLen(fndbankcode.Value) > 0 Then
                    Dim obj As clsVendorBankMaster = clsVendorBankMaster.GetData(fndbankcode.Value, NavigatorType.Current)
                    If obj Is Nothing Then
                        Exit Sub
                    End If
                    txtbankcodedes.Text = obj.Bank_Name
                    txtbankcountry.Text = obj.country_name
                    txtbankstate.Text = obj.state_name
                    txtbankcity.Text = obj.city_name
                    txtbranchcode.Value = obj.Branch_Code
                    txtbranchname.Text = obj.Branch_Name
                    txtifcicode.Text = obj.IFSC_Code
                Else
                    txtbankcodedes.Text = ""
                    txtbankcountry.Text = "India"
                    txtbankstate.Text = ""
                    txtbankcity.Text = ""
                    txtbranchname.Text = ""
                    txtaccno.Text = ""
                    txtifcicode.Text = ""
                    txtbranchcode.Value = ""
                    txtbranchname.Text = ""
                    txtifcicode.Text = ""
                End If

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
    Sub LoadBlankMCC_grid()
        Try
            gvMCC.Rows.Clear()
            gvMCC.Columns.Clear()
            gvMCC.Columns.Add("COLMCC_Code", "MCC Code")
            gvMCC.Columns.Add("COLMcc_Name", "MCC Name")
            gvMCC.Columns.Add("COLMccType", "MCC Type")

            Dim reponame1 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
            reponame1.FormatString = ""
            reponame1.Name = "COL_IsActive"
            reponame1.Width = 190
            reponame1.HeaderText = "Is Active"
            'reponame1.ReadOnly = True
            gvMCC.MasterTemplate.Columns.Add(reponame1)


            gvMCC.Columns("COLMCC_Code").Width = 150
            gvMCC.Columns("COLMcc_Name").Width = 200
            gvMCC.Columns("COLMccType").Width = 200
            gvMCC.Columns("COL_IsActive").Width = 50

            gvMCC.Columns("COLMcc_Name").ReadOnly = True
            gvMCC.Columns("COLMccType").ReadOnly = True
            gvMCC.Columns("COLMccType").IsVisible = False
            gvMCC.AllowAddNewRow = True
            gvMCC.MasterTemplate.AddNewRowPosition = SystemRowPosition.Bottom
            gvMCC.AllowEditRow = True
            gvMCC.AllowDeleteRow = True
            gvMCC.AllowRowResize = False
            gvMCC.AllowRowReorder = False
            gvMCC.AllowColumnResize = False
            gvMCC.AllowColumnChooser = False
            gvMCC.AllowAutoSizeColumns = True
            gvMCC.ShowGroupPanel = False
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    'Public Sub funfilfndcity()
    '    Try

    '        Dim strquery As String = "select City_Code ,city_Name from TSPL_City_MASTER where City_code = '" + txt.Value + "'"
    '        Dim dr As DataTable
    '        Dim strvalue As String
    '        dr = clsDBFuncationality.GetDataTable(strquery)
    '        If dr.Rows IsNot Nothing AndAlso dr.Rows.Count > 0 Then
    '            txtCity.Text = dr.Rows(0)("city_Name").ToString()
    '        End If
    '    Catch ex As Exception
    '        myMessages.myExceptions(ex)
    '    End Try
    'End Sub

    'It will fill the  controls if value exist in database according to fndTrmsCode
    Public Sub funfilfndterm()
        Try

            Dim strquery As String = "SELECT [Terms_Code] as [Terms Code],[Terms_Desc] as [Description] FROM [TSPL_TERMS_MASTER] where terms_code='" + fndTrmsCode.Value + "'"
            Dim dr As DataTable
            '  Dim strvalue As String
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

            Dim strquery As String = "select bank_code As [Bank Code],description  as [Description],City,State,Country from TSPL_Bank_MASTER where bank_code='" + fndbankcode.Value + "'"
            Dim dr As DataTable
            ' Dim strvalue As String
            dr = clsDBFuncationality.GetDataTable(strquery)
            If dr.Rows IsNot Nothing AndAlso dr.Rows.Count > 0 Then
                txtbankcodedes.Text = dr.Rows(0)("Description").ToString()
                txtbankcity.Text = clsCommon.myCstr(dr.Rows(0)("city"))
                txtbankstate.Text = clsCommon.myCstr(dr.Rows(0)("state"))
                txtbankcountry.Text = clsCommon.myCstr(dr.Rows(0)("country"))
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
            '  Dim strvalue As String
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
            strcmd = " SELECT TSPL_TAX_GROUP_DETAILS.Tax_Code as [Tax] FROM TSPL_TAX_GROUP_MASTER INNER JOIN TSPL_TAX_GROUP_DETAILS ON TSPL_TAX_GROUP_MASTER.Tax_Group_Code = TSPL_TAX_GROUP_DETAILS.Tax_Group_Code" & _
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
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Transporter")
        End Try
    End Sub
    'Thid funtion will fill all the fields on selecting the value fronm finder
    Public Sub funfill()
        Try
            'funreset()
            grdTax.Rows.Clear()
            Dim strCmd As String
            Dim myDs As DataSet
            IsInsieLoadData = True
            strCmd = " Select  Vendor_Name, Vendor_Group_Code,  Vendor_Group_Code_Desc,  Status ,OnHold  ,Convert(Date,Closing_Date,103) ,Add1 ,	Add2 ,Add3 ," &
                     "City_Code ,City_Code_Desc ,State ,Country ,	Phone1 ,Phone2 ,Fax,Email ,WebSite ,Contact_Person_Name ,Contact_Person_Phone ," &
                     "Contact_Person_Fax ,Contact_Person_Website ,Contact_Person_Email ,Terms_Code ,Terms_Code_Desc ,Vendor_Account ,Vendor_Account_Desc ," &
                     "Payment_Code,Payment_Code_Desc ,Ven_Type_Code ,Ven_Type_Desc ,Bank_Code ,Bank_Code_Desc ,Service_Tax_No ,Lst_No ,Tin_No ,	Credit_Limit ," &
                     "Tax_Group ,Tax_Group_Desc ,TAX1 ,TAX1_Rate ,TAX2,TAX2_Rate ,TAX3 ,TAX3_Rate ,TAX4 ,TAX4_Rate ,TAX5 ,TAX5_Rate ,TAX6 ,TAX6_Rate ," &
                     "TAX7 ,TAX7_Rate ,TAX8 ,TAX8_Rate ,TAX9 ,TAX9_Rate ,TAX10 ,TAX10_Rate ,Remarks1 ,Remarks2 ,Additional1 ,Additional2 ,Additional3,transporter,CST,ECC,Range,Collectorate,PAN,is_Gross_Receipt,Inter_branch,currency_code,franchise_yn,state_code,country_code,vsp_payee_name,zila,tehsil,branch_name,ifci_code,account_no,industry_type,industry_person,agreement,security_cheque,no_of_installment,amount_of_installment,branch_code,Account_Type,Start_date,End_Date,IsBlankCheque,Cheque_In_Favour_Of,Apply_Mult_Incentive,incentive_days,incentive,GSTRegistered,GSTEntity,GSTLastEntity,GSTFinalNo,Aadhar_No,Care_Of,SecChequeNoLac1,SecChequeNoRs100,Security_Amount,Security_Deduction_Amount from tspl_vendor_master where vendor_code='" + fndvendorNo.Value + "' and form_type='PTM'	"


            myDs = connectSql.RunSQLReturnDS(strCmd)
            Dim myDr As DataRow
            For Each myDr In myDs.Tables(0).Rows
                Me.txtvendorname.Text = myDr(0).ToString()
                Me.fndgroupcode.Value = myDr(1).ToString()
                Me.txtgroupdes.Text = myDr(2).ToString()

                cmbBlankCheque.Text = IIf(myDr("IsBlankCheque") = 1, "YES", "NO")

                chkMultIncentive.Checked = IIf(clsCommon.myCdbl(myDr("Apply_Mult_Incentive")) > 0, True, False)
                LoadIncentive(fndvendorNo.Value, Nothing)

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

                If (myDr(5).ToString()) = Nothing Then
                    Me.dtClosing.Value = System.DateTime.Now.Date
                Else
                    Me.dtClosing.Value = CDate(myDr(5).ToString())
                End If
                Me.txtAdd1.Text = myDr(6).ToString()
                Me.txtAdd2.Text = myDr(7).ToString()
                Me.txtAdd3.Text = myDr(8).ToString()
                Me.txtCity.Text = myDr(9).ToString()
                Me.txtState.Text = myDr(11).ToString()
                Me.txtCountry.Text = myDr(12).ToString()

                '----------------------------------------------------Monika 22/05/2014-----------------------
                txtcountrycode.Value = clsCommon.myCstr(myDr("country_code"))
                txtstatecode.Value = clsCommon.myCstr(myDr("state_code"))

                Dim check As String = clsDBFuncationality.getSingleValue("select case when isnull(GST_State_Code,'')='' then '' else GST_State_Code end as GST_State_Code from tspl_state_master where state_code='" & txtstatecode.Value & "'")
                If clsCommon.myLen(check) > 0 Then
                    txtGSTStateCode.Text = check
                End If

                txtEntity.Text = clsCommon.myCstr(myDr("GSTEntity"))
                MyTextBox2.Text = clsCommon.myCstr(myDr("GSTLastEntity"))
                Rchkregistered.Checked = IIf(clsCommon.myCstr(myDr("GSTRegistered").ToString()) = "1", True, False)
                txtGSTIN_No_final.Text = clsCommon.myCstr(myDr("GSTFinalNo"))


                '' done by Panch Raj
                FndIncentive.Value = clsCommon.myCstr(myDr("incentive"))
                GetIIncentiveDetails(False)

                '---------------------------------for old data getting code'-----------------------------
                If clsCommon.myLen(txtState.Text) > 0 AndAlso clsCommon.myLen(txtstatecode.Value) <= 0 Then
                    Dim qry As String = "select state_code from tspl_state_master where state_name like '%" + txtState.Text + "%'"
                    txtstatecode.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
                End If

                If clsCommon.myLen(txtCountry.Text) > 0 AndAlso clsCommon.myLen(txtcountrycode.Value) <= 0 Then
                    Dim qry As String = "select country_code from tspl_country_master where country_name like '%" + txtCountry.Text + "%'"
                    txtcountrycode.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
                End If

                txtzila.Text = clsCommon.myCstr(myDr("zila"))
                txttehsil.Text = clsCommon.myCstr(myDr("tehsil"))
                txtpayeename.Text = clsCommon.myCstr(myDr("vsp_payee_name"))
                txtbranchcode.Value = clsCommon.myCstr(myDr("branch_code"))
                ''richa agarwal 27/03/2015
                txtbranchname.Text = clsCommon.myCstr(myDr("branch_name"))
                ' txtbranchname.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select branch_name from tspl_bank_branch_master where branch_code='" + txtbranchcode.Value + "'"))
                ''---------------------------------
                txtifcicode.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select ifsc_code from tspl_bank_branch_master where branch_code='" + txtbranchcode.Value + "'"))
                txtaccno.Text = clsCommon.myCstr(myDr("account_no"))
                '---------------------------------------------------------------------------------

                Dim indtrytype As String = ""
                Dim indutrypersn As String = ""
                Dim agrmnt As String = ""

                indtrytype = clsCommon.myCstr(myDr("industry_type"))
                indutrypersn = clsCommon.myCstr(myDr("industry_person"))
                agrmnt = clsCommon.myCstr(myDr("agreement"))


                cmbagreemnt.SelectedValue = agrmnt

                If clsCommon.CompairString(cmbagreemnt.Text, "YES") = CompairStringResult.Equal Then
                    If myDr("Start_Date") IsNot DBNull.Value Then
                        dtpStartDate.Value = clsCommon.myCDate(myDr("Start_Date"))
                    End If
                    If myDr("End_Date") IsNot DBNull.Value Then
                        dtpEndDate.Value = clsCommon.myCDate(myDr("End_Date"))
                    End If

                    'If myDr("Start_Date")) isnot DBNull.value  AndAlso IsDBNull(myDr("End_Date")) = False Then
                    '    dtpStartDate.Value = clsCommon.myCDate(myDr("Start_Date"))
                    '    dtpEndDate.Value = clsCommon.myCDate(myDr("End_Date"))
                    'End If

                End If

                If clsCommon.CompairString(indtrytype, "Prop.") = CompairStringResult.Equal Then
                    rbtnprop.IsChecked = True
                    txtprop_name.Text = indutrypersn
                ElseIf clsCommon.CompairString(indtrytype, "Partnership") = CompairStringResult.Equal Then
                    rbtnpartnership.IsChecked = True
                    txtpartner_name.Text = indutrypersn
                ElseIf clsCommon.CompairString(indtrytype, "Public") = CompairStringResult.Equal Then
                    rbtnpublic.IsChecked = True
                    txtdirectr_name.Text = indutrypersn
                ElseIf clsCommon.CompairString(indtrytype, "Pvt") = CompairStringResult.Equal Then
                    rbtnpvt.IsChecked = True
                    txtdirectr_name.Text = indutrypersn
                End If
                txtChequeInFavour.Text = clsCommon.myCstr(myDr("Cheque_In_Favour_Of"))
                cmbsecurity.Text = clsCommon.myCstr(myDr("security_cheque"))
                txtnoofinstlmnt.Text = clsCommon.myCdbl(myDr("no_of_installment"))
                txtamtofinstlmnt.Text = clsCommon.myCdbl(myDr("amount_of_installment"))
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
                Me.fndbankcode.Value = myDr(31).ToString()
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
                txtCareOf.Text = clsCommon.myCstr(myDr("Care_Of"))
                txtAadharNo.Text = clsCommon.myCstr(myDr("Aadhar_No"))
                txtSecChequeLac1.Text = clsCommon.myCstr(myDr("SecChequeNoLac1"))
                txtSecChequeRs100.Text = clsCommon.myCstr(myDr("SecChequeNoRs100"))
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
                Me.txtGST_PanCode.Text = myDr(69).ToString()

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

                If clsCommon.CompairString(clsCommon.myCstr(myDr(39)), "") = CompairStringResult.Equal Then
                    If grdTax.Rows.Count < 1 Then
                        grdTax.Rows.AddNew()
                    End If

                    Me.grdTax.Rows(0).Cells(0).Value = myDr(39).ToString
                    Me.grdTax.Rows(0).Cells(1).Value = myDr(40).ToString
                Else
                    Return
                End If

                If clsCommon.CompairString(clsCommon.myCstr(myDr(41)), "") = CompairStringResult.Equal Then
                    If grdTax.Rows.Count <= 2 Then
                        grdTax.Rows.AddNew()
                    End If
                    Me.grdTax.Rows(1).Cells(0).Value = myDr(41).ToString
                    Me.grdTax.Rows(1).Cells(1).Value = myDr(42).ToString
                Else
                    Return
                End If

                If clsCommon.CompairString(clsCommon.myCstr(myDr(43)), "") = CompairStringResult.Equal Then
                    If grdTax.Rows.Count <= 3 Then
                        grdTax.Rows.AddNew()
                    End If
                    Me.grdTax.Rows(2).Cells(0).Value = myDr(43).ToString
                    Me.grdTax.Rows(2).Cells(1).Value = myDr(44).ToString
                Else
                    Return
                End If

                If clsCommon.CompairString(clsCommon.myCstr(myDr(45)), "") = CompairStringResult.Equal Then
                    If grdTax.Rows.Count <= 4 Then
                        grdTax.Rows.AddNew()
                    End If
                    Me.grdTax.Rows(3).Cells(0).Value = myDr(45).ToString
                    Me.grdTax.Rows(3).Cells(1).Value = myDr(46).ToString
                Else
                    Return
                End If

                If clsCommon.CompairString(clsCommon.myCstr(myDr(47)), "") = CompairStringResult.Equal Then
                    If grdTax.Rows.Count <= 5 Then
                        grdTax.Rows.AddNew()
                    End If
                    Me.grdTax.Rows(4).Cells(0).Value = myDr(47).ToString
                    Me.grdTax.Rows(4).Cells(1).Value = myDr(48).ToString
                Else
                    Return
                End If

                If clsCommon.CompairString(clsCommon.myCstr(myDr(49)), "") = CompairStringResult.Equal Then
                    If grdTax.Rows.Count <= 6 Then
                        grdTax.Rows.AddNew()
                    End If
                    Me.grdTax.Rows(5).Cells(0).Value = myDr(49).ToString
                    Me.grdTax.Rows(5).Cells(1).Value = myDr(50).ToString
                Else
                    Return
                End If

                If clsCommon.CompairString(clsCommon.myCstr(myDr(51)), "") = CompairStringResult.Equal Then
                    If grdTax.Rows.Count <= 7 Then
                        grdTax.Rows.AddNew()
                    End If
                    Me.grdTax.Rows(6).Cells(0).Value = myDr(51).ToString
                    Me.grdTax.Rows(6).Cells(1).Value = myDr(52).ToString
                Else
                    Return
                End If

                If clsCommon.CompairString(clsCommon.myCstr(myDr(53)), "") = CompairStringResult.Equal Then
                    If grdTax.Rows.Count <= 8 Then
                        grdTax.Rows.AddNew()
                    End If
                    Me.grdTax.Rows(7).Cells(0).Value = myDr(53).ToString
                    Me.grdTax.Rows(7).Cells(1).Value = myDr(54).ToString
                Else
                    Return
                End If

                If clsCommon.CompairString(clsCommon.myCstr(myDr(55)), "") = CompairStringResult.Equal Then
                    If grdTax.Rows.Count <= 9 Then
                        grdTax.Rows.AddNew()
                    End If
                    Me.grdTax.Rows(8).Cells(0).Value = myDr(55).ToString
                    Me.grdTax.Rows(8).Cells(1).Value = myDr(56).ToString
                Else
                    Return
                End If

                If clsCommon.CompairString(clsCommon.myCstr(myDr(57)), "") = CompairStringResult.Equal Then
                    If grdTax.Rows.Count <= 10 Then
                        grdTax.Rows.AddNew()
                    End If
                    Me.grdTax.Rows(9).Cells(0).Value = myDr(57).ToString()
                    Me.grdTax.Rows(9).Cells(1).Value = myDr(58).ToString
                Else
                    Return
                End If

                LoadAccount_Type()
                cmbAccount_Type.SelectedValue = clsCommon.myCstr(myDr("Account_Type"))


                TxtSecurityAmountEditable.Value = clsCommon.myCdbl(myDr("Security_Amount"))
                txtSecurityDeductedAmountEditable.Value = clsCommon.myCdbl(myDr("Security_Deduction_Amount"))


                'Me.txtRemarks1.Text = myDr(59).ToString()
                'Me.txtRemarks2.Text = myDr(60).ToString()
                'Me.txtAddInfo1.Text = myDr(61).ToString()
                'Me.txtAddInfo2.Text = myDr(62).ToString()
                'Me.txtAddInfo3.Text = myDr(63).ToString()
                LoadBlankMCC_grid()
                strCmd = "select * from Tspl_mcc_Transporter_Mapping tm left join tspl_MCC_Master mm on mm.mcc_Code=tm.Mcc_Code where tm.Transporter_CODE='" + fndvendorNo.Value + "'"
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(strCmd)
                If dt.Rows.Count > 0 Then
                    For i As Integer = 0 To dt.Rows.Count - 1
                        gvMCC.Rows.Add(dt.Rows(i).Item("MCC_CODE"), dt.Rows(i).Item("MCC_Name"), dt.Rows(i).Item("MCC_Type"), IIf(dt.Rows(i).Item("IS_Active") = "1", True, False))
                    Next
                End If
            Next
            IsInsieLoadData = False
            'btnsave.Text = "Update"
            'btnsave.Enabled = True
            'btndelete.Enabled = True

            UcAttachment1.LoadData(fndvendorNo.Value)
            LoadVisiDetail()
            LoadBlankPaymentEntry()
            TxtSecurityAmount.Text = 0
            txtSecurityDeductedAmount.Text = 0
            Dim objP As List(Of clsPayment_Detail_MCC) = clsPayment_Detail_MCC.GetPaymentData(fndvendorNo.Value)
            If objP IsNot Nothing AndAlso objP.Count > 0 Then
                For i As Integer = 0 To objP.Count - 1
                    GVPaymentEntry.Rows.Add(objP.Item(i).Payment_No, objP.Item(i).Payment_Date, objP.Item(i).Description, objP.Item(i).Bank_Name, objP.Item(i).Payment_Type, objP.Item(i).Bank_Charges, objP.Item(i).Vendor_Code, objP.Item(i).Vendor_Name, objP.Item(i).Cheque_No, objP.Item(i).Cheque_Date, objP.Item(i).Payment_Mode, objP.Item(i).Payment_Amount)
                    If clsCommon.myCdbl(objP.Item(i).Payment_Amount) >= 0 Then
                        TxtSecurityAmount.Text += clsCommon.myCdbl(objP.Item(i).Payment_Amount)
                    Else
                        txtSecurityDeductedAmount.Text -= clsCommon.myCdbl(objP.Item(i).Payment_Amount)
                    End If

                Next
                TxtTotalAmount.Text = clsCommon.myCdbl(TxtSecurityAmount.Text) - clsCommon.myCdbl(txtSecurityDeductedAmount.Text)
                GVPaymentEntry.BestFitColumns()
            End If

            LoadBlankGvCheque()
            LoadBlankGridDeduction()

            Dim arr As List(Of clsPTMDeductionRange) = clsPTMDeductionRange.GetData(fndvendorNo.Value)
            If arr IsNot Nothing AndAlso arr.Count > 0 Then
                For Each objtr As clsPTMDeductionRange In arr
                    gvDeductionRange.Rows.AddNew()
                    gvDeductionRange.Rows(gvDeductionRange.RowCount - 1).Cells(colDRMinutes).Value = objtr.Minutes
                    gvDeductionRange.Rows(gvDeductionRange.RowCount - 1).Cells(colDRAmount).Value = objtr.Amount
                Next
            End If
            gvDeductionRange.Rows.AddNew()

            If clsCommon.CompairString(cmbBlankCheque.Text, "YES") = CompairStringResult.Equal Then
                Dim objC As List(Of clsBlankChequeDetails) = clsBlankChequeDetails.LoadData(fndvendorNo.Value, Me.Form_ID)
                If objC IsNot Nothing AndAlso objC.Count > 0 Then
                    For i As Integer = 0 To objC.Count - 1
                        gvCheque.Rows.Add(objC.Item(i).Check_No, objC.Item(i).Check_date)
                    Next
                End If
            End If
            If clsCommon.myLen(fndbankcode.Value) > 0 Then
                Dim obj As clsVendorBankMaster = clsVendorBankMaster.GetData(fndbankcode.Value, NavigatorType.Current)
                If obj Is Nothing Then
                    Exit Sub
                End If
                txtbankcodedes.Text = obj.Bank_Name
                txtbankcountry.Text = obj.country_name
                txtbankstate.Text = obj.state_name
                txtbankcity.Text = obj.city_name
                'txtbranchcode.Value = obj.Branch_Code
                'txtbranchname.Text = obj.Branch_Name
                'txtifcicode.Text = obj.IFSC_Code
            Else
                txtbankcodedes.Text = ""
                txtbankcountry.Text = "India"
                txtbankstate.Text = ""
                txtbankcity.Text = ""
                ' txtbranchname.Text = ""
                txtaccno.Text = ""
                'txtifcicode.Text = ""
                'txtbranchcode.Value = ""
                'txtbranchname.Text = ""
                'txtifcicode.Text = ""
            End If

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Transporter")
        End Try
    End Sub

    Sub GeneratedVendorNo(ByVal trans As SqlTransaction)
        'Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If clsCommon.CompairString(btnsave.Text, "Save") = CompairStringResult.Equal Then
                fndvendorNo.Value = clsERPFuncationality.GetNextCode(trans, clsCommon.GETSERVERDATE(trans), clsDocType.PTMMASTER, "", "")
            End If
            'trans.Commit()
        Catch ex As Exception
            'trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Sub
    'For inserting the data in the database
    Public Sub funinsert()
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            GeneratedVendorNo(trans)

            Dim Registered As Integer = 0
            If Rchkregistered.Checked = True Then
                Registered = 1
            Else
                Registered = 0
                txtGST_PanCode.Text = ""
                txtEntity.Text = ""
                txtGSTStateCode.Text = ""
                txtGSTIN_No_final.Text = ""
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
            Dim indutrytype As String = ""
            Dim indutryperson As String = ""
            Dim agrremnt As String = ""
            Dim security As String = ""
            Dim noofinstallmnts As Decimal = Nothing
            Dim amtofinstlmnt As Decimal = Nothing

            security = clsCommon.myCstr(cmbsecurity.Text)
            noofinstallmnts = clsCommon.myCdbl(txtnoofinstlmnt.Text)
            amtofinstlmnt = clsCommon.myCdbl(txtamtofinstlmnt.Text)

            agrremnt = clsCommon.myCstr(cmbagreemnt.SelectedValue)

            If rbtnpartnership.IsChecked Then
                indutrytype = "Partnership"
                indutryperson = txtpartner_name.Text.Replace("'", "`")
            ElseIf rbtnprop.IsChecked Then
                indutrytype = "Prop."
                indutryperson = txtprop_name.Text.Replace("'", "`")
            ElseIf rbtnpublic.IsChecked Then
                indutrytype = "Public"
                indutryperson = txtdirectr_name.Text.Replace("'", "`")
            ElseIf rbtnpvt.IsChecked Then
                indutrytype = "Pvt"
                indutryperson = txtdirectr_name.Text.Replace("'", "`")
            End If


            Dim IsGrossReceipt As Integer = IIf(chkIsGrossReceipt.Checked, 1, 0)
            connectSql.RunSpTransaction(trans, "sp_TSPL_VENDOR_MASTER_INSERT", New SqlParameter("@Vendor_Code", fndvendorNo.Value), New SqlParameter("@Vendor_Name", txtvendorname.Text.ToString()), New SqlParameter("@Vendor_Group_Code", fndgroupcode.Value), New SqlParameter("Vendor_Group_Des", txtgroupdes.Text.ToString()), New SqlParameter("@Status", strStatus), New SqlParameter("@OnHold", strHold), New SqlParameter("@transporter", strtrans), New SqlParameter("@Closing_Date", Format(Me.dtClosing.Value, "dd/MM/yyyy")), New SqlParameter("@Add1", txtAdd1.Text.ToString()), New SqlParameter("@Add2", txtAdd2.Text.ToString()), New SqlParameter("@Add3", txtAdd3.Text.ToString()), New SqlParameter("@City_Code", txtCity.Text), New SqlParameter("@City_Des", txtCity.Text.ToString()), New SqlParameter("@State", txtState.Text.ToString()), New SqlParameter("@Country", txtCountry.Text.ToString()), New SqlParameter("@Phone1", txtPhone1.Text.ToString()), New SqlParameter("@Phone2", txtPhone2.Text.ToString()), New SqlParameter("@Fax", txtfax.Text.ToString()), New SqlParameter("@Email", txtEmail.Text.ToString()), New SqlParameter("@WebSite", txtWeb.Text.ToString()), New SqlParameter("@Contact_Person_Name", txtContactName.Text.ToString()), New SqlParameter("@Contact_Person_Phone", txtContPhone.Text.ToString()), New SqlParameter("@Contact_Person_Fax", txtContactFax.Text.ToString()), New SqlParameter("@Contact_Person_Website", txtContactWeb.Text.ToString()), New SqlParameter("@Contact_Person_Email", txtContactEmail.Text.ToString()), New SqlParameter("@Terms_Code", fndTrmsCode.Value), New SqlParameter("@Terms_Code_Des", txttermcodedes.Text.ToString()), New SqlParameter("@Vendor_Account", fndAccntSet.Value), New SqlParameter("@Vendor_Account_Set_Des", Me.fndAccntSet.Value), New SqlParameter("@Payment_Code", fndPayCode.Value), New SqlParameter("@Payment_Code_Des", txtpaymentcodedes.Text.ToString()), New SqlParameter("@Vendor_Type_Code", fndvendortype.Value), New SqlParameter("@Vendor_Type_Des", txtvendortypedes.Text.ToString()), New SqlParameter("@Bank_Code", fndbankcode.Value), New SqlParameter("@Bank_Code_Des", txtbankcodedes.Text.ToString()), New SqlParameter("@Service_Tax_No", Me.txtStaxNo.Text), New SqlParameter("@Lst_No", Me.txtLstNo.Text), New SqlParameter("@Tin_No", tin_no), New SqlParameter("@Credit_Limit", CrLimit), New SqlParameter("@Tax_Group", Me.fndTxGrp.Value), New SqlParameter("@Tax_Group_Des", txtTxGrp.Text.ToString()), New SqlParameter("@TAX1", strTax1), New SqlParameter("@TAX1_Rate", strTax1_Rate), New SqlParameter("@TAX2", strTax2), New SqlParameter("@TAX2_Rate", strTax2_Rate), New SqlParameter("@TAX3", strTax3), New SqlParameter("@TAX3_Rate", strTax3_Rate), New SqlParameter("@TAX4", strTax4), New SqlParameter("@TAX4_Rate", strTax4_Rate), New SqlParameter("@TAX5", strTax5), New SqlParameter("@TAX5_Rate", strTax5_Rate), New SqlParameter("@TAX6", strTax6), New SqlParameter("@TAX6_Rate", strTax6_Rate), New SqlParameter("@TAX7", strTax7), New SqlParameter("@TAX7_Rate", strTax7_Rate), New SqlParameter("@TAX8", strTax8), New SqlParameter("@TAX8_Rate", strTax8_Rate), New SqlParameter("@TAX9", strTax9), New SqlParameter("@TAX9_Rate", strTax9_Rate), New SqlParameter("@TAX10", strTax10), New SqlParameter("@TAX10_Rate", strTax10_Rate), New SqlParameter("@Remarks1", txtRemarks1.Text.ToString()), New SqlParameter("@Remarks2", txtRemarks2.Text.ToString()), New SqlParameter("@Additional1", txtAddInfo1.Text.ToString()), New SqlParameter("@Additional2", txtAddInfo2.Text.ToString()), New SqlParameter("@Additional3", txtAddInfo3.Text.ToString()), New SqlParameter("@cst", txtcst.Text.ToString()), New SqlParameter("@ecc", txtecc.Text.ToString()), New SqlParameter("@range", txtrange.Text.ToString()), New SqlParameter("@collectorate", txtcollect.Text.ToString()), New SqlParameter("@pan", txtpan.Text.ToString()), New SqlParameter("@Created_By", userCode), New SqlParameter("@Created_Date", connectSql.serverDate(trans)), New SqlParameter("@Modify_By", userCode), New SqlParameter("@Modify_Date", connectSql.serverDate(trans)), New SqlParameter("@Comp_Code", companyCode), New SqlParameter("@is_Gross_Receipt", IsGrossReceipt), New SqlParameter("@Inter_branch", strInterBranch))
            Dim strCmd11 As String = "Update TSPL_VENDOR_MASTER set franchise_yn='" & strTagAsFranchise & "',Form_Type='PTM',state_code='" & clsCommon.myCstr(txtstatecode.Value) & "',country_code='" & clsCommon.myCstr(txtcountrycode.Value) & "',vsp_payee_name='" & clsCommon.myCstr(txtpayeename.Text.Replace("'", "`")) & "',zila='" & clsCommon.myCstr(txtzila.Text.Replace("'", "`")) & "',tehsil='" & clsCommon.myCstr(txttehsil.Text.Replace("'", "`")) & "',branch_name='" & clsCommon.myCstr(txtbranchname.Text.Replace("'", "`")) & "',ifci_code='" & clsCommon.myCstr(txtbranchcode.Value) & "',account_no='" & clsCommon.myCstr(txtaccno.Text.Replace("'", "`")) & "',Industry_Type='" & indutrytype & "',Industry_Person='" & indutryperson & "',Agreement='" & agrremnt & "',security_cheque='" & security & "',no_of_installment='" & noofinstallmnts & "',amount_of_installment='" & amtofinstlmnt & "',branch_code='" & clsCommon.myCstr(txtbranchcode.Value) & "',IFSC_Code='" & clsCommon.myCstr(txtbranchcode.Value) & "',isBlankCheque=" & IIf(clsCommon.CompairString(cmbBlankCheque.Text, "YES") = CompairStringResult.Equal, 1, 0) & " where Vendor_Code='" & clsCommon.myCstr(fndvendorNo.Value) & "'"
            connectSql.RunSqlTransaction(trans, strCmd11)
            If clsCommon.CompairString(agrremnt, "NO") = CompairStringResult.Equal Then
            Else
                Dim strCmd111 As String = "Update TSPL_VENDOR_MASTER set  Start_Date='" & clsCommon.GetPrintDate(dtpStartDate.Value, "dd/MMM/yyyy") & "',End_Date='" & clsCommon.GetPrintDate(dtpEndDate.Value, "dd/MMM/yyyy") & "' where Vendor_Code='" & fndvendorNo.Value & "'"
                connectSql.RunSqlTransaction(trans, strCmd111)
            End If

            For i As Integer = 1 To grdTax.Rows.Count
                Dim strTax As String = Convert.ToString("Tax" & Convert.ToString(i))
                Dim Tax As String = grdTax.Rows(i - 1).Cells(0).Value
                Dim strTaxRate As String = Convert.ToString("Tax" & Convert.ToString(i) & "_Rate")
                Dim Tax_Rate As Decimal = Convert.ToDecimal(grdTax.Rows(i - 1).Cells(1).Value)
                Dim strCmd1 As String = "Update TSPL_VENDOR_MASTER set " & strTax & "='" & Tax & "'," & strTaxRate & "=" & Tax_Rate.ToString() & " , franchise_yn='" & strTagAsFranchise & "' where Vendor_Code='" & fndvendorNo.Value & "'"
                connectSql.RunSqlTransaction(trans, strCmd1)
            Next


            Dim streq1 As String = "Update TSPL_VENDOR_MASTER set Security_Amount='" + clsCommon.myCstr(TxtSecurityAmountEditable.Value) + "',Security_Deduction_Amount='" + clsCommon.myCstr(txtSecurityDeductedAmountEditable.Value) + "',GSTRegistered='" & Registered & "',GSTEntity='" & txtEntity.Text & "',GSTLastEntity='" & MyTextBox2.Text & "',GSTFinalNo='" & txtGSTIN_No_final.Text & "',GSTMiddle='" & txtFixxed.Text & "',Care_Of='" & clsCommon.myCstr(txtCareOf.Text) & "',Aadhar_No='" & clsCommon.myCstr(txtAadharNo.Text) & "',SecChequeNoLac1='" & clsCommon.myCstr(txtSecChequeLac1.Text) & "',SecChequeNoRs100='" & clsCommon.myCstr(txtSecChequeRs100.Text) & "' where Vendor_Code='" + fndvendorNo.Value + "'"
            clsDBFuncationality.ExecuteNonQuery(streq1, trans)

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
                strq = "Update TSPL_VENDOR_MASTER set CURRENCY_CODE='" & clsCommon.myCstr(Me.fndVendorCurrency.Value) & "',Form_Type='" & txtvndrtype.Text & "',state_code='" & txtstatecode.Value & "',country_code='" & txtcountrycode.Value & "' where Vendor_Code='" & fndvendorNo.Value & "'"
            Else
                strq = "Update TSPL_VENDOR_MASTER set CURRENCY_CODE='" + objCommonVar.BaseCurrencyCode + "',Form_Type='" & txtvndrtype.Text & "',state_code='" & txtstatecode.Value & "',country_code='" & txtcountrycode.Value & "' where Vendor_Code='" & fndvendorNo.Value & "'"
            End If
            clsDBFuncationality.ExecuteNonQuery(strq, trans)
            If Not IsNothing(Me.cmbAccount_Type.SelectedValue) Then
                strq = "Update TSPL_VENDOR_MASTER set Account_Type='" & clsCommon.myCstr(Me.cmbAccount_Type.SelectedValue) & "' where Vendor_Code='" & fndvendorNo.Value & "'"
                clsDBFuncationality.ExecuteNonQuery(strq, trans)
            End If
            'If clsCommon.myLen(txtChequeInFavour.Text) > 0 Then
            '    strq = "Update TSPL_VENDOR_MASTER set Cheque_In_Favour_Of='" & clsCommon.myCstr(txtChequeInFavour.Text) & "' where Vendor_Code='" + fndvendorNo.Value + "'"
            '    clsDBFuncationality.ExecuteNonQuery(strq, trans)
            'End If

            If clsCommon.myLen(txtChequeInFavour.Text) > 0 Then
                Dim streq As String = "Update TSPL_VENDOR_MASTER set Cheque_In_Favour_Of='" & clsCommon.myCstr(txtChequeInFavour.Text) & "' where Vendor_Code='" + fndvendorNo.Value + "'"
                clsDBFuncationality.ExecuteNonQuery(streq, trans)
            Else
                Dim streq As String = "Update TSPL_VENDOR_MASTER set Cheque_In_Favour_Of= '" + txtvendorname.Text + "'+'-' + '" + txtbankcodedes.Text + "' +'-' + '" + txtaccno.Text + "' where Vendor_Code='" + fndvendorNo.Value + "'"
                clsDBFuncationality.ExecuteNonQuery(streq, trans)
            End If
            '' end multi currency
            Dim objB As clsBlankChequeDetails = Nothing
            Dim arrObjB As List(Of clsBlankChequeDetails) = Nothing
            If clsCommon.CompairString(cmbBlankCheque.Text, "YES") = CompairStringResult.Equal Then

                If gvCheque.Rows.Count > 0 AndAlso clsCommon.myLen(gvCheque.Rows(0).Cells("COLCheck_No").Value) <= 0 Then
                    Throw New Exception("Please Fill Atleast One Cheque No")
                End If
                arrObjB = New List(Of clsBlankChequeDetails)
                For i As Integer = 0 To gvCheque.Rows.Count - 1
                    If gvCheque.Rows.Count > 0 AndAlso clsCommon.myLen(gvCheque.Rows(i).Cells("COLCheck_No").Value) > 0 AndAlso clsCommon.myLen(gvCheque.Rows(0).Cells("COLCheck_Date").Value) > 0 Then
                        objB = New clsBlankChequeDetails()
                        objB.Form_ID = Me.Form_ID
                        objB.Check_No = clsCommon.myCstr(gvCheque.Rows(i).Cells("COLCheck_No").Value)
                        objB.Check_date = clsCommon.myCDate(gvCheque.Rows(0).Cells("COLCheck_Date").Value)
                        objB.Prog_Code = fndvendorNo.Value
                        arrObjB.Add(objB)
                    End If
                Next
            End If

            If clsCommon.CompairString(cmbBlankCheque.Text, "YES") = CompairStringResult.Equal AndAlso (arrObjB Is Nothing Or arrObjB.Count <= 0) Then
                Throw New Exception("Please Fill Atleast One Cheque No")
            End If

            If arrObjB IsNot Nothing AndAlso arrObjB.Count > 0 Then
                clsBlankChequeDetails.SaveData(arrObjB, trans)
            End If
            SavdDeductionRange(trans)
            '' insert Incentives
            updateMultipleIncentive(fndvendorNo.Value, trans)

            btnsave.Text = "Update"
            btndelete.Enabled = True
            InsertMCC(trans)

            trans.Commit()
            myMessages.insert()
            UcAttachment1.SaveData(fndvendorNo.Value)
            'If userCode <> "ADMIN" Then
            '    If funSetUserAccess() = False Then Exit Sub
            'End If



        Catch ex As Exception
            trans.Rollback()
            myMessages.myExceptions(ex)
        End Try
    End Sub
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
    Sub SavdDeductionRange(ByVal trans As SqlTransaction)
        Dim arr As List(Of clsPTMDeductionRange) = Nothing
        If gvDeductionRange.Rows.Count > 0 Then
            arr = New List(Of clsPTMDeductionRange)()
            Dim obj As clsPTMDeductionRange
            For ii As Integer = 0 To gvDeductionRange.Rows.Count - 1
                obj = New clsPTMDeductionRange
                obj.Minutes = clsCommon.myCdbl(gvDeductionRange.Rows(ii).Cells(colDRMinutes).Value)
                obj.Amount = clsCommon.myCdbl(gvDeductionRange.Rows(ii).Cells(colDRAmount).Value)
                If obj.Minutes > 0 AndAlso obj.Amount > 0 Then
                    arr.Add(obj)
                End If
            Next
        End If
        clsPTMDeductionRange.SaveData(fndvendorNo.Value, arr, trans)
    End Sub

    'This function for updation
    Public Sub InsertMCC(ByVal trans As SqlTransaction)
        Try
            Dim sQuery As String = "Delete from TSPL_MCC_Transporter_MAPPING where Transporter_Code='" & fndvendorNo.Value & "'"
            clsDBFuncationality.ExecuteNonQuery(sQuery, trans)
            Dim arrMCC As New ArrayList
            Dim isMultiMCCExist As Integer = 0
            For Each row As GridViewRowInfo In gvMCC.Rows
                If clsCommon.myLen(row.Cells("COLMCC_Code").Value) > 0 Then
                    isMultiMCCExist += 1
                    sQuery = "insert into TSPL_MCC_Transporter_MAPPING values('" & clsCommon.myCstr(row.Cells("COLMCC_Code").Value) & "','" & fndvendorNo.Value & "','" & IIf(clsCommon.myCBool(row.Cells("COL_IsActive").Value) = True, "1", "0") & "')"
                    clsDBFuncationality.ExecuteNonQuery(sQuery, trans)
                    arrMCC.Add(clsCommon.myCstr(row.Cells("COLMCC_Code").Value))
                End If
            Next
            If isOneMCCOnePrimaryTranporter Then
                If isMultiMCCExist > 1 Then
                    Throw New Exception("Please select only one MCC")
                End If
                sQuery = " select MCC_CODE,Transporter_CODE from TSPL_MCC_Transporter_MAPPING where MCC_CODE in (" + clsCommon.GetMulcallString(arrMCC) + ") and Transporter_CODE not in ('" + fndvendorNo.Value + "') group by MCC_CODE,Transporter_CODE"
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(sQuery, trans)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    sQuery = "One MCC should Mapped With One Tranporter.But follwing combination exists "
                    For Each dr As DataRow In dt.Rows
                        sQuery += Environment.NewLine + " MCC- " + clsCommon.myCstr(dr("MCC_CODE")) + " Transporter- " + clsCommon.myCstr(dr("Transporter_CODE"))
                    Next
                    Throw New Exception(sQuery)
                End If
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Sub LoadAccount_Type()
        cmbAccount_Type.DataSource = Nothing
        Dim dt As New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow = Nothing

        dr = dt.NewRow()
        dr("Code") = "Sav"
        dr("Name") = "Saving"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Cur"
        dr("Name") = "Current"
        dt.Rows.Add(dr)


        cmbAccount_Type.DataSource = dt
        cmbAccount_Type.DisplayMember = "Name"
        cmbAccount_Type.ValueMember = "Code"
        cmbAccount_Type.SelectedValue = 0
    End Sub

    Public Sub funupdate()
        Dim trans As SqlTransaction = Nothing
        Try
            trans = clsDBFuncationality.GetTransactin()

            Dim Registered As Integer = 0
            If Rchkregistered.Checked = True Then
                Registered = 1
            Else
                Registered = 0
                txtGST_PanCode.Text = ""
                txtEntity.Text = ""
                txtGSTStateCode.Text = ""
                txtGSTIN_No_final.Text = ""
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


            'Dim Bal1 As Decimal
            Dim CrLimit As Decimal
            ' Dim OutComm As Decimal


            'Format(Me.dtClosing.Value, "dd/MM/yyyy")
            If txtCredit.Text = "" Then
                CrLimit = Convert.ToDecimal("0.00")
            Else
                CrLimit = Convert.ToDecimal(txtCredit.Text)
            End If
            Dim indutrytype As String = ""
            Dim indutryperson As String = ""
            Dim agrremnt As String = ""
            Dim security As String = ""
            Dim noofinstallmnts As Decimal = Nothing
            Dim amtofinstlmnt As Decimal = Nothing

            security = clsCommon.myCstr(cmbsecurity.Text)
            noofinstallmnts = clsCommon.myCdbl(txtnoofinstlmnt.Text)
            amtofinstlmnt = clsCommon.myCdbl(txtamtofinstlmnt.Text)

            agrremnt = clsCommon.myCstr(cmbagreemnt.SelectedValue)

            If rbtnpartnership.IsChecked Then
                indutrytype = "Partnership"
                indutryperson = txtpartner_name.Text.Replace("'", "`")
            ElseIf rbtnprop.IsChecked Then
                indutrytype = "Prop."
                indutryperson = txtprop_name.Text.Replace("'", "`")
            ElseIf rbtnpublic.IsChecked Then
                indutrytype = "Public"
                indutryperson = txtdirectr_name.Text.Replace("'", "`")
            ElseIf rbtnpvt.IsChecked Then
                indutrytype = "Pvt"
                indutryperson = txtdirectr_name.Text.Replace("'", "`")
            End If

            Dim IsGrossReceipt As Integer = IIf(chkIsGrossReceipt.Checked, 1, 0)
            clsDBFuncationality.SaveAStorePorcedure(trans, "sp_TSPL_VENDOR_MASTER_UPDATE", New SqlParameter("@Vendor_Code", fndvendorNo.Value), New SqlParameter("@Vendor_Name", txtvendorname.Text.ToString()), New SqlParameter("@Vendor_Group_Code", fndgroupcode.Value), New SqlParameter("Vendor_Group_Des", txtgroupdes.Text.ToString()), New SqlParameter("@Status", strStatus), New SqlParameter("@OnHold", strHold), New SqlParameter("@transporter", strtrans), New SqlParameter("@Closing_Date", closingdate), New SqlParameter("@Add1", txtAdd1.Text.ToString()), New SqlParameter("@Add2", txtAdd2.Text.ToString()), New SqlParameter("@Add3", txtAdd3.Text.ToString()), New SqlParameter("@City_Code", txtCity.Text), New SqlParameter("@City_Des", txtCity.Text.ToString()), New SqlParameter("@State", txtState.Text.ToString()), New SqlParameter("@Country", txtCountry.Text.ToString()), New SqlParameter("@Phone1", txtPhone1.Text.ToString()), New SqlParameter("@Phone2", txtPhone2.Text.ToString()), New SqlParameter("@Fax", txtfax.Text.ToString()), New SqlParameter("@Email", txtEmail.Text.ToString()), New SqlParameter("@WebSite", txtWeb.Text.ToString()), New SqlParameter("@Contact_Person_Name", txtContactName.Text.ToString()), New SqlParameter("@Contact_Person_Phone", txtContPhone.Text.ToString()), New SqlParameter("@Contact_Person_Fax", txtContactFax.Text.ToString()), New SqlParameter("@Contact_Person_Website", txtContactWeb.Text.ToString()), New SqlParameter("@Contact_Person_Email", txtContactEmail.Text.ToString()), New SqlParameter("@Terms_Code", fndTrmsCode.Value), New SqlParameter("@Terms_Code_Des", txttermcodedes.Text.ToString()), New SqlParameter("@Vendor_Account", fndAccntSet.Value), New SqlParameter("@Vendor_Account_Set_Des", fndAccntSet.Value), New SqlParameter("@Payment_Code", fndPayCode.Value), New SqlParameter("@Payment_Code_Des", txtpaymentcodedes.Text.ToString()), New SqlParameter("@Vendor_Type_Code", fndvendortype.Value), New SqlParameter("@Vendor_Type_Des", txtvendortypedes.Text.ToString()), New SqlParameter("@Bank_Code", fndbankcode.Value), New SqlParameter("@Bank_Code_Des", txtbankcodedes.Text.ToString()), New SqlParameter("@Service_Tax_No", Me.txtStaxNo.Text), New SqlParameter("@Lst_No", Me.txtLstNo.Text), New SqlParameter("@Tin_No", Me.txtTinNo.Text), New SqlParameter("@Credit_Limit", CrLimit), New SqlParameter("@Tax_Group", Me.fndTxGrp.Value), New SqlParameter("@Tax_Group_Des", txtTxGrp.Text.ToString()), New SqlParameter("@TAX1", strTax1), New SqlParameter("@TAX1_Rate", strTax1_Rate), New SqlParameter("@TAX2", strTax2), New SqlParameter("@TAX2_Rate", strTax2_Rate), New SqlParameter("@TAX3", strTax3), New SqlParameter("@TAX3_Rate", strTax3_Rate), New SqlParameter("@TAX4", strTax4), New SqlParameter("@TAX4_Rate", strTax4_Rate), New SqlParameter("@TAX5", strTax5), New SqlParameter("@TAX5_Rate", strTax5_Rate), New SqlParameter("@TAX6", strTax6), New SqlParameter("@TAX6_Rate", strTax6_Rate), New SqlParameter("@TAX7", strTax7), New SqlParameter("@TAX7_Rate", strTax7_Rate), New SqlParameter("@TAX8", strTax8), New SqlParameter("@TAX8_Rate", strTax8_Rate), New SqlParameter("@TAX9", strTax9), New SqlParameter("@TAX9_Rate", strTax9_Rate), New SqlParameter("@TAX10", strTax10), New SqlParameter("@TAX10_Rate", strTax10_Rate), New SqlParameter("@Remarks1", txtRemarks1.Text.ToString()), New SqlParameter("@Remarks2", txtRemarks2.Text.ToString()), New SqlParameter("@Additional1", txtAddInfo1.Text.ToString()), New SqlParameter("@Additional2", txtAddInfo2.Text.ToString()), New SqlParameter("@Additional3", txtAddInfo3.Text.ToString()), New SqlParameter("@cst", txtcst.Text.ToString()), New SqlParameter("@ecc", txtecc.Text.ToString()), New SqlParameter("@range", txtrange.Text.ToString()), New SqlParameter("@collectorate", txtcollect.Text.ToString()), New SqlParameter("@pan", txtpan.Text.ToString()), New SqlParameter("@Modify_By", userCode), New SqlParameter("@Modify_Date", connectSql.serverDate(trans)), New SqlParameter("@Comp_Code", companyCode), New SqlParameter("@is_Gross_Receipt", IsGrossReceipt), New SqlParameter("@InterBranch ", strInterBranch))
            Dim strCmd11 As String
            strCmd11 = "Update TSPL_VENDOR_MASTER set franchise_yn='" & strTagAsFranchise & "',Form_Type='PTM',state_code='" & clsCommon.myCstr(txtstatecode.Value) & "',country_code='" & clsCommon.myCstr(txtcountrycode.Value) & "',vsp_payee_name='" & clsCommon.myCstr(txtpayeename.Text.Replace("'", "`")) & "',zila='" & clsCommon.myCstr(txtzila.Text.Replace("'", "`")) & "',tehsil='" & clsCommon.myCstr(txttehsil.Text.Replace("'", "`")) & "',branch_name='" & clsCommon.myCstr(txtbranchname.Text.Replace("'", "`")) & "',ifci_code='" & clsCommon.myCstr(txtbranchcode.Value) & "',account_no='" & clsCommon.myCstr(txtaccno.Text.Replace("'", "`")) & "',Industry_Type='" & indutrytype & "',Industry_Person='" & indutryperson & "',Agreement='" & agrremnt & "',security_cheque='" & security & "',no_of_installment='" & noofinstallmnts & "',amount_of_installment='" & amtofinstlmnt & "',branch_code='" & clsCommon.myCstr(txtbranchcode.Value) & "',isBlankCheque=" & IIf(clsCommon.CompairString(cmbBlankCheque.Text, "YES") = CompairStringResult.Equal, 1, 0) & " where Vendor_Code='" & clsCommon.myCstr(fndvendorNo.Value) & "'"
            clsDBFuncationality.ExecuteNonQuery(strCmd11, trans)
            ''richa agarwal 27/03/2015
            strCmd11 = "Update TSPL_VENDOR_MASTER set franchise_yn='" & strTagAsFranchise & "',Form_Type='PTM',state_code='" & clsCommon.myCstr(txtstatecode.Value) & "',country_code='" & clsCommon.myCstr(txtcountrycode.Value) & "',vsp_payee_name='" & clsCommon.myCstr(txtpayeename.Text.Replace("'", "`")) & "',zila='" & clsCommon.myCstr(txtzila.Text.Replace("'", "`")) & "',tehsil='" & clsCommon.myCstr(txttehsil.Text.Replace("'", "`")) & "',branch_name='" & clsCommon.myCstr(txtbranchname.Text.Replace("'", "`")) & "',ifci_code='" & clsCommon.myCstr(txtbranchcode.Value) & "',account_no='" & clsCommon.myCstr(txtaccno.Text.Replace("'", "`")) & "',Industry_Type='" & indutrytype & "',Industry_Person='" & indutryperson & "',Agreement='" & agrremnt & "',security_cheque='" & security & "',no_of_installment='" & noofinstallmnts & "',amount_of_installment='" & amtofinstlmnt & "',branch_code='" & clsCommon.myCstr(txtbranchcode.Value) & "',IFSC_Code='" & clsCommon.myCstr(txtbranchcode.Value) & "',isBlankCheque=" & IIf(clsCommon.CompairString(cmbBlankCheque.Text, "YES") = CompairStringResult.Equal, 1, 0) & " where Vendor_Code='" & clsCommon.myCstr(fndvendorNo.Value) & "'"
            clsDBFuncationality.ExecuteNonQuery(strCmd11, trans)
            ''--------------------------
            If clsCommon.CompairString(agrremnt, "NO") = CompairStringResult.Equal Then
            Else
                Dim strCmd111 As String = "Update TSPL_VENDOR_MASTER set  Start_Date='" & clsCommon.GetPrintDate(dtpStartDate.Value, "dd/MMM/yyyy") & "',End_Date='" & clsCommon.GetPrintDate(dtpEndDate.Value, "dd/MMM/yyyy") & "'  where Vendor_Code='" & fndvendorNo.Value & "'"
                connectSql.RunSqlTransaction(trans, strCmd111)
            End If
            For i As Integer = 1 To grdTax.Rows.Count
                Dim strTax As String = Convert.ToString("Tax" & Convert.ToString(i))
                Dim Tax As String = grdTax.Rows(i - 1).Cells(0).Value
                Dim strTaxRate As String = Convert.ToString("Tax" & Convert.ToString(i) & "_Rate")
                ' Ticket No : BHA/26/06/18-000086 By prabhakar
                Dim chkTaxRate As String = grdTax.Rows(i - 1).Cells(1).Value
                Dim Tax_Rate As Decimal = 0.0
                If clsCommon.myLen(chkTaxRate) > 0 Then
                    Tax_Rate = Convert.ToDecimal(grdTax.Rows(i - 1).Cells(1).Value)
                End If
                Dim strCmd As String
                strCmd = "Update TSPL_VENDOR_MASTER set " & strTax & "='" & Tax & "'," & strTaxRate & "='" & Tax_Rate.ToString() & "' where Vendor_Code='" & fndvendorNo.Value & "'"
                clsDBFuncationality.ExecuteNonQuery(strCmd, trans)
            Next

            Dim streq As String = "Update TSPL_VENDOR_MASTER set Security_Amount='" + clsCommon.myCstr(TxtSecurityAmountEditable.Value) + "',Security_Deduction_Amount='" + clsCommon.myCstr(txtSecurityDeductedAmountEditable.Value) + "',GSTRegistered='" & Registered & "',GSTEntity='" & txtEntity.Text & "',GSTLastEntity='" & MyTextBox2.Text & "',GSTFinalNo='" & txtGSTIN_No_final.Text & "',GSTMiddle='" & txtFixxed.Text & "',Care_Of='" & clsCommon.myCstr(txtCareOf.Text) & "',Aadhar_No='" & clsCommon.myCstr(txtAadharNo.Text) & "',SecChequeNoLac1='" & clsCommon.myCstr(txtSecChequeLac1.Text) & "',SecChequeNoRs100='" & clsCommon.myCstr(txtSecChequeRs100.Text) & "' where Vendor_Code='" + fndvendorNo.Value + "'"
            clsDBFuncationality.ExecuteNonQuery(streq, trans)

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
                strq = "Update TSPL_VENDOR_MASTER set CURRENCY_CODE='" & clsCommon.myCstr(Me.fndVendorCurrency.Value) & "' where Vendor_Code='" & fndvendorNo.Value & "'"
            Else
                strq = "Update TSPL_VENDOR_MASTER set CURRENCY_CODE='" + objCommonVar.BaseCurrencyCode + "' where Vendor_Code='" & fndvendorNo.Value & "'"
            End If
            clsDBFuncationality.ExecuteNonQuery(strq, trans)
            If Not IsNothing(Me.cmbAccount_Type.SelectedValue) Then
                strq = "Update TSPL_VENDOR_MASTER set Account_Type='" & clsCommon.myCstr(Me.cmbAccount_Type.SelectedValue) & "' where Vendor_Code='" & fndvendorNo.Value & "'"
                clsDBFuncationality.ExecuteNonQuery(strq, trans)
            End If
            'If clsCommon.myLen(Me.txtChequeInFavour.Text) > 0 Then
            '    strq = "Update TSPL_VENDOR_MASTER set Cheque_In_Favour_Of ='" & clsCommon.myCstr(Me.txtChequeInFavour.Text) & "' where Vendor_Code='" + fndvendorNo.Value + "'"
            '    clsDBFuncationality.ExecuteNonQuery(strq, trans)
            'End If

            If clsCommon.myLen(txtChequeInFavour.Text) > 0 Then
                Dim streq1 As String = "Update TSPL_VENDOR_MASTER set Cheque_In_Favour_Of='" & clsCommon.myCstr(txtChequeInFavour.Text) & "' where Vendor_Code='" + fndvendorNo.Value + "'"
                clsDBFuncationality.ExecuteNonQuery(streq1, trans)
            Else
                Dim streq1 As String = "Update TSPL_VENDOR_MASTER set Cheque_In_Favour_Of= '" + txtvendorname.Text + "'+'-' + '" + txtbankcodedes.Text + "' +'-' + '" + txtaccno.Text + "' where Vendor_Code='" + fndvendorNo.Value + "'"
                clsDBFuncationality.ExecuteNonQuery(streq1, trans)
            End If
            Dim objB As clsBlankChequeDetails = Nothing
            Dim arrObjB As List(Of clsBlankChequeDetails) = Nothing
            If clsCommon.CompairString(cmbBlankCheque.Text, "YES") = CompairStringResult.Equal Then

                If gvCheque.Rows.Count > 0 AndAlso clsCommon.myLen(gvCheque.Rows(0).Cells("COLCheck_No").Value) <= 0 Then
                    Throw New Exception("Please Fill Atleast One Cheque No")
                End If
                arrObjB = New List(Of clsBlankChequeDetails)
                For i As Integer = 0 To gvCheque.Rows.Count - 1
                    If gvCheque.Rows.Count > 0 AndAlso clsCommon.myLen(gvCheque.Rows(i).Cells("COLCheck_No").Value) > 0 AndAlso clsCommon.myLen(gvCheque.Rows(0).Cells("COLCheck_Date").Value) > 0 Then
                        objB = New clsBlankChequeDetails()
                        objB.Form_ID = Me.Form_ID
                        objB.Check_No = clsCommon.myCstr(gvCheque.Rows(i).Cells("COLCheck_No").Value)
                        objB.Check_date = clsCommon.myCDate(gvCheque.Rows(0).Cells("COLCheck_Date").Value)
                        objB.Prog_Code = fndvendorNo.Value
                        arrObjB.Add(objB)
                    End If
                Next
            End If

            If clsCommon.CompairString(cmbBlankCheque.Text, "YES") = CompairStringResult.Equal AndAlso (arrObjB Is Nothing Or arrObjB.Count <= 0) Then
                Throw New Exception("Please Fill Atleast One Cheque No")
            End If


            If arrObjB IsNot Nothing AndAlso arrObjB.Count > 0 Then
                clsBlankChequeDetails.SaveData(arrObjB, trans)
            End If
            InsertMCC(trans)
            SavdDeductionRange(trans)
            '' update multiple incentive
            updateMultipleIncentive(fndvendorNo.Value, trans)
            '' end multi currency
            trans.Commit()
            UcAttachment1.SaveData(fndvendorNo.Value)
            myMessages.update()
        Catch ex As Exception
            trans.Rollback()
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Public Sub fundelete()
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim qst As String
            Dim dpt As String
            '------for payment screen---------
            qst = "select Vendor_Code from TSPL_PAYMENT_HEADER where Vendor_Code='" & fndvendorNo.Value & "'"
            dpt = clsDBFuncationality.getSingleValue(qst, trans)
            If clsCommon.myLen(dpt) > 0 AndAlso dpt IsNot Nothing Then
                Throw New Exception("This Transporter Cannot be deleted." & Environment.NewLine & "This is already in Process")
                Return
            End If
            '-----------for store recevied screen-------
            qst = "select Vendor_Code from TSPL_SRN_HEAD where Vendor_Code='" & fndvendorNo.Value & "'"
            dpt = clsDBFuncationality.getSingleValue(qst, trans)
            If clsCommon.myLen(dpt) > 0 AndAlso dpt IsNot Nothing Then
                Throw New Exception("This Transporter Cannot be deleted." & Environment.NewLine & "This is already in Process")
                Return
            End If
            qst = "select Vendor_Code from TSPL_PR_HEAD where Vendor_Code='" & fndvendorNo.Value & "'"
            dpt = clsDBFuncationality.getSingleValue(qst, trans)
            If clsCommon.myLen(dpt) > 0 AndAlso dpt IsNot Nothing Then
                Throw New Exception("This Transporter Cannot be deleted." & Environment.NewLine & "This is already in Process")
                Return
            End If
            clsPTMDeductionRange.DeleteData(fndvendorNo.Value, trans)
            connectSql.RunSpTransaction(trans, "sp_TSPL_VENDOR_MASTER_DELETE", New SqlParameter("@Vendor_Code", fndvendorNo.Value), New SqlParameter("@form_type", txtvndrtype.Text))
            clsCustomFieldValues.DeleteData(MyBase.Form_ID, fndvendorNo.Value, trans)

            myMessages.delete()
            btnsave.Text = "Save"
            btndelete.Enabled = False
            trans.Commit()
            Reset()
            funreset()
        Catch ex As Exception
            trans.Rollback()
            myMessages.myExceptions(ex)
        End Try
    End Sub
    'this function will reset all the fields for new entry
    Public Sub funreset()
        txtTranspoterCopy.Enabled = True
        txtTranspoterCopy.Value = ""
        chkMultIncentive.Checked = False
        txtIncentiveMult.Enabled = False
        FndIncentive.Value = Nothing
        LblIncentive.Text = ""

        TxtTotalAmount.Text = "0"
        UcAttachment1.Form_ID = Me.Form_ID
        UcAttachment1.BlankAllControls()
        rbtnpartnership.IsChecked = False
        rbtnprop.IsChecked = False
        rbtnpublic.IsChecked = False
        rbtnpvt.IsChecked = False
        txtpartner_name.Text = ""
        txtdirectr_name.Text = ""
        txtprop_name.Text = ""
        cmbagreemnt.SelectedValue = "NO"
        cmbsecurity.Text = "NO"
        txtnoofinstlmnt.Text = ""
        txtamtofinstlmnt.Text = ""
        txtChequeInFavour.Text = ""
        txtChequeInFavour.Enabled = True
        txtzila.Text = ""
        txttehsil.Text = ""
        txtpayeename.Text = ""
        txtbranchname.Text = ""
        txtifcicode.Text = ""
        txtaccno.Text = ""
        txtbankcountry.Text = "INDIA"
        txtbankstate.Text = ""
        txtbankcity.Text = ""
        txtcountrycode.Value = "INDIA"
        txtstatecode.Value = ""
        txtvndrtype.Text = "PTM"

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
        Me.txtCity.Text = ""
        Me.txtState.Text = ""
        Me.txtCountry.Text = "INDIA"
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
        chkInActive.Checked = False
        dtClosing.Enabled = False
        btnsave.Text = "Save"
        btndelete.Enabled = False
        chkTagAsFranchise.Checked = False
        txtvendorname.Focus()
        chktrarns.Checked = True
        chktrarns.Visible = False
        LoadAccount_Type()
        cmbAccount_Type.SelectedValue = 0
        gvMCC.Rows.Clear()
        gvMCC.Columns.Clear()
        IsInsieLoadData = False
        LoadBlankMCC_grid()
        dtpStartDate.Value = clsCommon.GETSERVERDATE()
        dtpEndDate.Value = dtpStartDate.Value
        ''For Custom Fields
        If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
            UcCustomFields1.BlankAllControls()
        End If
        ''End of For Custom Fields
        txtbranchcode.Value = ""
        txtaccno.ReadOnly = False
        txtpan.CharacterCasing = CharacterCasing.Upper
        LoadBlankPaymentEntry()
        TxtSecurityAmount.Text = "0"
        txtSecurityDeductedAmount.Text = "0"

        MyTextBox2.Text = ""
        txtEntity.Text = ""
        txtGSTIN_No_final.Text = ""
        txtGST_PanCode.Text = ""
        txtGSTStateCode.Text = ""
        txtAadharNo.Text = ""
        txtCareOf.Text = ""
        txtSecChequeLac1.Text = ""
        txtSecChequeRs100.Text = ""
        LoadBlankGvCheque()
        LoadBlankGridDeduction()
        LoadVisiDetail()
        gvDeductionRange.Rows.AddNew()

        TxtSecurityAmountEditable.Value = 0
        txtSecurityDeductedAmountEditable.Value = 0
    End Sub

    Public Sub funExport()
        Try
            Dim strCmd As String = "select count(*) from tspl_vendor_master where form_type='PTM'"
            Dim check As Integer = clsDBFuncationality.getSingleValue(strCmd)
            Dim whrCls As String = ""
            If check > 0 Then
                strCmd = " select Vendor_Code as [Transporter No],Vendor_Name as[Transporter Name],Add1 as [Address1],Add2 as [Address2]" &
                         ",Add3 as [Address3],Vendor_Group_Code as [Group Code]" &
                         ",Vendor_Group_Code_Desc as [Vendor Group Description],City_Code as [City]" &
                         ",State as [State],Country as [Country],Phone1 as [Phone Num1]" &
                         ",Phone2 as [Phone Num2],Fax as [Fax],Email as [Email Id],WebSite as [Website]" &
                         ",Terms_Code as [Terms Code],Terms_Code_Desc as [Terms Description],Vendor_Account as [Vendor Account]" &
                         ",Vendor_Account_Desc as [Vendor Account Description],Payment_Code as [Payment Code]" &
                         ",Payment_Code_Desc as [Paymnet Code Description],Bank_Code as [Bank Code]" &
                         ",Ven_Type_Code as [Vendor Type],Ven_Type_Desc as [Vendor Type Description]" &
                         ",Tax_Group as [Tax Group],Tax_Group_Desc as [Tax Group Description]" &
                         ",TAX1 as [Tax1],TAX1_Rate as [Tax1 Rate],TAX2 as [Tax2],TAX2_Rate as [Tax2 Rate],TAX3 as [Tax3]" &
                         ",TAX3_Rate as [Tax3 Rate],TAX4 as [Tax4],TAX4_Rate as [Tax4 Rate],TAX5 as [Tax5]" &
                         ",TAX5_Rate as [Tax5 Rate],TAX6 as [Tax6],TAX6_Rate as [Tax6 Rate],TAX7 as [Tax7]" &
                         ",TAX7_Rate as [Tax7 Rate], TAX8 as [Tax8],TAX8_Rate as [Tax8 Rate],TAX9 as [Tax9]" &
                         ",TAX9_Rate as [Tax9 Rate],TAX10 as [Tax10],TAX10_Rate as [Tax10 Rate]" &
                         ",Transporter as [Transporter]" &
                         ",Created_By as [Created By],Created_Date as [Created Date],Modify_By as [Modify By]" &
                         ",Modify_Date as [Modify Date],comp_code as [Company Code],CURRENCY_CODE as [Currency Code]" &
                         ",Collectorate as [Collectorate],PAN as [PAN],State_Code,Country_Code,Zila,Tehsil,VSP_payee_name as [Payee Name],Account_No,Industry_Type,(case when industry_type='Prop.' then industry_person else '' end) as [Prop Name],(case when industry_type='Partnership' then industry_person else '' end) as [Partner Name],(case when industry_type in ('Public','Pvt') then industry_person else '' end) as [Director Name],Agreement,Start_Date,End_Date,Security_Cheque,No_of_Installment,Amount_of_Installment,form_type,IFSC_Code,Branch_Name,CHEQUE_IN_FAVOUR_OF AS [Cheque in Favour of],Incentive,Apply_Mult_Incentive as [Multiple Incentive(0/1)] , GSTRegistered as [GST Register], GSTFinalNo as [GSTIN No],Care_Of,Aadhar_No,SecChequeNoLac1,SecChequeNoRs100  from TSPL_VENDOR_MASTER "
                whrCls = " and form_type='PTM' "
            Else

                whrCls = ""
                strCmd = "select '' as [Transporter No],'' as[Transporter Name],'' as [Address1],'' as [Address2]" &
                          ",'' as [Address3],'' as [Group Code]" &
                          ",'' as [Vendor Group Description],'' as [City]" &
                          ",'' as [State],'' as [Country],'' as [Phone Num1]" &
                          ",'' as [Phone Num2],'' as [Fax],'' as [Email Id],'' as [Website]" &
                          ",'' as [Terms Code],'' as [Terms Description],'' as [Vendor Account]" &
                           ",'' as [Vendor Account Description],'' as [Payment Code]" &
                           ",'' as [Paymnet Code Description],'' as [Bank Code]" &
                           ",'' as [Vendor Type],'' as [Vendor Type Description]" &
                           ",'' as [Tax Group],'' as [Tax Group Description]" &
                           ",'' as [Tax1],'' as [Tax1 Rate],'' as [Tax2],'' as [Tax2 Rate],'' as [Tax3]" &
                           ",'' as [Tax3 Rate],'' as [Tax4],'' as [Tax4 Rate],'' as [Tax5]" &
                           ",'' as [Tax5 Rate],'' as [Tax6],'' as [Tax6 Rate],'' as [Tax7]" &
                           ",'' as [Tax7 Rate], '' as [Tax8],'' as [Tax8 Rate],'' as [Tax9]" &
                           ",'' as [Tax9 Rate],'' as [Tax10],'' as [Tax10 Rate]" &
                           ",'' as [Transporter]" &
                           ",'' as [Created By],'' as [Created Date],'' as [Modify By]" &
                           ",'' as [Modify Date],'' as [Company Code],'' as  [Currency Code]" &
                           ",'' as [Collectorate],'' as [PAN],'' as State_Code,'' as Country_Code,'' as Zila,'' as Tehsil,'' as [Payee Name],'' as Account_No,'' as Industry_Type,'' as [Prop Name],'' as [Partner Name],'' as [Director Name],'' as Agreement,'' as Start_Date,'' as End_Date,'' as Security_Cheque,0 as No_of_Installment,0 as Amount_of_Installment,'PTM' as form_type,'' as IFSC_Code,'' as Branch_Name,'' as [Cheque in Favour of],0 as Deduction_Slab_1_Minute,0 as Deduction_Slab_1_Amount,0 as Deduction_Slab_2_Minute,0 as Deduction_Slab_2_Amount,0 as Deduction_Slab_3_Minute,0 as Deduction_Slab_3_Amount ,0 as Deduction_Slab_4_Minute,0 as Deduction_Slab_4_Amount,0 as Deduction_Slab_5_Minute,0 as Deduction_Slab_5_Amount,0 as Deduction_Slab_6_Minute,0 as Deduction_Slab_6_Amount ,0 as Deduction_Slab_7_Minute,0 as Deduction_Slab_7_Amount,0 as Deduction_Slab_8_Minute,0 as Deduction_Slab_8_Amount,0 as Deduction_Slab_9_Minute,0 as Deduction_Slab_9_Amount  ,0 as Deduction_Slab_10_Minute,0 as Deduction_Slab_10_Amount,'' as Incentive,'0' as Apply_Mult_Incentive as [Multiple Incentive(0/1)] , '' as [GST Register], '' as [GSTIN No],'' as Care_Of,'' as Aadhar_No,'' as SecChequeNoLac1,'' as SecChequeNoRs100 "
            End If
            ListImpExpColumnsMandatory = New List(Of String)({"Transporter No", "Transporter Name", "Group Code", "country_code", "state_code", "City", "Terms Code", "Vendor Account", "Bank Code", "Tax Group", "Industry_Type", "Prop Name", "Partner Name", "Director Name", "GST Register", "Pan"})
            ListImpExpColumnsSuperMandatory = New List(Of String)({"Transporter No"})
            transportSql.ExporttoExcel(strCmd, whrCls, "", Me, ListImpExpColumnsMandatory, ListImpExpColumnsSuperMandatory, MyBase.Form_ID)


        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Transporter")
        End Try

    End Sub

    Public Sub funImport()
        Dim GSTFinal As String = ""
        Dim Registered As Integer = 0
        Dim GSTEntity As String = ""
        Dim GSTLastEntity As String = ""
        Dim GSTMiddleEntity As String = ""
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim Count As String = ""
        If transportSql.importExcel(gv, "Transporter No", "Transporter Name", "Address1", "Address2", "Address3", "Group Code", "Vendor Group Description", "City", "State", "Country", "Phone Num1", "Phone Num2", "Fax", "Email Id", "Website", "Terms Code", "Terms Description", "Vendor Account", "Vendor Account Description", "Payment Code", "Paymnet Code Description", "Bank Code", "Vendor Type", "Vendor Type Description", "Tax Group", "Tax Group Description", "Tax1", "Tax1 Rate", "Tax2", "Tax2 Rate", "Tax3", "Tax3 Rate", "Tax4", "Tax4 Rate", "Tax5", "Tax5 Rate", "Tax6", "Tax6 Rate", "Tax7", "Tax7 Rate", "Tax8", "Tax8 Rate", "Tax9", "Tax9 Rate", "Tax10", "Tax10 Rate", "Transporter", "Created By", "Created Date", "Modify By", "Modify Date", "Company Code", "Currency Code", "Collectorate", "PAN", "State_Code", "Country_Code", "Zila", "Tehsil", "Payee Name", "Account_No", "Industry_Type", "Prop Name", "Partner Name", "Director Name", "Agreement", "Start_Date", "End_Date", "Security_Cheque", "No_of_Installment", "Amount_of_Installment", "form_type", "IFSC_Code", "Branch_Name", "Cheque in Favour of", "Incentive", "Multiple Incentive(0/1)", "GST Register", "GSTIN No", "Aadhar_No", "Care_Of", "SecChequeNoLac1", "SecChequeNoRs100") Then
            Dim trans As SqlTransaction = Nothing
            Try
                connectSql.OpenConnection()
                trans = clsDBFuncationality.GetTransactin()
                clsCommon.ProgressBarShow()
                Dim counter As Integer = 1

                For Each grow As GridViewRowInfo In gv.Rows
                    Count = clsCommon.myCstr(grow.Index + 2)
                    Dim strvendorNo As String = clsCommon.myCstr(grow.Cells("Transporter No").Value)
                    If strvendorNo.Length > 12 Then
                        Throw New Exception("Check the length of Transporter No.,See At Line No. " + clsCommon.myCstr(counter) + "")
                    End If

                    If String.IsNullOrEmpty(strvendorNo) Then
                        Throw New Exception("Transporter No. can not be blank,See At Line No. " + clsCommon.myCstr(counter) + "")
                    End If

                    Dim strvendorname1 As String = clsCommon.myCstr(grow.Cells("Transporter Name").Value)
                    Dim strvendorname As String = strvendorname1.Replace("'", "''")
                    If strvendorname.Length > 100 Then
                        Throw New Exception("Length of Transporter Name can not be greater than 100.,See At Line No. " + clsCommon.myCstr(counter) + "")
                    End If
                    If String.IsNullOrEmpty(strvendorname) Then
                        Throw New Exception("Transporter Name can not be blank,See At Line No. " + clsCommon.myCstr(counter) + "")
                    End If
                    Dim check As Integer = 0
                    Dim MultipleIncentive As Integer = clsCommon.myCdbl(grow.Cells("Multiple Incentive(0/1)").Value)
                       If String.IsNullOrEmpty(strvendorname) Then
                        Throw New Exception("VSP Name can not be blank,See At Line No. " + clsCommon.myCstr(counter) + "")
                    End If
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

                    Dim add1 As String = clsCommon.myCstr(grow.Cells("Address1").Value)
                    Dim add2 As String = clsCommon.myCstr(grow.Cells("Address2").Value)
                    Dim add3 As String = clsCommon.myCstr(grow.Cells("Address3").Value)
                    Dim closing_date As String
                    closing_date = System.DateTime.Now.Date
                    'If clsCommon.myCstr(grow.Cells("Closing Date").Value) = Nothing Then
                    '    closing_date = System.DateTime.Now.Date
                    'Else
                    '    closing_date = clsCommon.myCstr(grow.Cells("Closing Date").Value)
                    'End If



                    Dim strgroupCode As String = clsCommon.myCstr(grow.Cells("Group Code").Value)
                    If String.IsNullOrEmpty(strgroupCode) Then
                        Throw New Exception(" Group Code can not be blank")
                    End If
                    Dim i As Integer
                    Dim qry As String = "select Count(*)from TSPL_VENDOR_GROUP  where Ven_Group_Code ='" + strgroupCode + "'"
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
                    Dim citycode As String = clsCommon.myCstr(grow.Cells("City").Value)
                    Dim citycodedesc As String = citycode
                    Dim state As String = clsCommon.myCstr(grow.Cells("State").Value)
                    Dim country As String = clsCommon.myCstr(grow.Cells("Country").Value)

                    Dim statecode As String = clsCommon.myCstr(grow.Cells("state_code").Value)
                    Dim countrycode As String = clsCommon.myCstr(grow.Cells("country_code").Value)
                    'Dim check As Integer = 0

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

                    'If clsCommon.myLen(citycode) > 0 AndAlso clsCommon.myLen(statecode) > 0 Then
                    '    qry = "select count(*) from tspl_city_master where city_code='" + citycode + "' and state_code='" + statecode + "'"
                    '    check = clsDBFuncationality.getSingleValue(qry, trans)

                    '    If check <= 0 Then
                    '        Throw New Exception("City Code Does Not Exist,Please Make Its Master First,See At Line No. " + clsCommon.myCstr(counter) + "")
                    '    End If
                    'End If


                    Dim phonenum1 As String = clsCommon.myCstr(grow.Cells("Phone Num1").Value)
                    Dim phonenum2 As String = clsCommon.myCstr(grow.Cells("Phone Num2").Value)
                    Dim fax As String = clsCommon.myCstr(grow.Cells("Fax").Value)
                    Dim emailid As String = clsCommon.myCstr(grow.Cells("Email Id").Value)
                    Dim website As String = clsCommon.myCstr(grow.Cells("Website").Value)
                    Dim contct_person_name As String = "" 'clsCommon.myCstr(grow.Cells("Contact Person Name").Value)
                    Dim contct_perfson_phone As String = "" ' clsCommon.myCstr(grow.Cells("Contect Person Phone").Value)
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
                            Throw New Exception("Check the length of Payment Code")
                        End If
                    End If
                    Dim paymenttypedesc As String = clsCommon.myCstr(grow.Cells("Paymnet Code Description").Value)
                    Dim strbank As String = clsCommon.myCstr(grow.Cells("Bank Code").Value)

                    If String.IsNullOrEmpty(strbank) Then
                        Throw New Exception("Bank Code can not be blank,See At Line No. " + clsCommon.myCstr(counter) + "")
                    End If
                    Dim i5 As String

                    Dim qry7 As String = "select COUNT(*) from TSPL_Vendor_BANK_MASTER  where Bank_Code ='" + strbank + "'"
                    i5 = connectSql.RunScalar(trans, qry7)
                    If i5 = 0 Then
                        Throw New Exception("Bank Code Does Not Exist : " + strbank + ",See At Line No. " + clsCommon.myCstr(counter) + "")
                    End If
                    'If strbank.Length > 12 Then
                    '    Throw New Exception("Check the length of Bank Code,See At Line No. " + clsCommon.myCstr(counter) + "")
                    'End If

                    'Dim strbankdes As String = clsCommon.myCstr(grow.Cells("Bank Code Description").Value)
                    'If strbankdes.Length > 50 Then
                    '    Throw New Exception("Check the length of Bank Description,See At Line No. " + clsCommon.myCstr(counter) + "")
                    'End If

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
                    Dim strAadharNo As String = clsCommon.myCstr(grow.Cells("Aadhar_No").Value)
                    Dim strCareOf As String = clsCommon.myCstr(grow.Cells("Care_Of").Value)
                    Dim strSecChequeNoLac1 As String = clsCommon.myCstr(grow.Cells("SecChequeNoLac1").Value)
                    Dim strSecChequeNoRs100 As String = clsCommon.myCstr(grow.Cells("SecChequeNoRs100").Value)
                    If clsCommon.myLen(strAadharNo) > 0 Then
                        If clsCommon.myLen(strAadharNo) <> 12 Then
                            Throw New Exception("Aadhar No should be 12 character")
                        End If
                    End If
                    '------------------------------------------------------------------------

                    Dim zila As String = clsCommon.myCstr(grow.Cells("zila").Value)
                    Dim tehsil As String = clsCommon.myCstr(grow.Cells("tehsil").Value)
                    Dim payeename As String = clsCommon.myCstr(grow.Cells("Payee Name").Value)
                    Dim strbankdes As String = String.Empty
                    Dim branchname As String = String.Empty
                    Dim branchcode As String = String.Empty
                    Dim ifcicode As String = String.Empty

                    Dim objVb As clsVendorBankMaster = clsVendorBankMaster.GetData(strbank, NavigatorType.Current, trans)
                    If objVb IsNot Nothing Then
                        strbankdes = objVb.Bank_Name
                        'branchname = objVb.Branch_Name
                        'branchcode = objVb.Branch_Code
                        'ifcicode = objVb.IFSC_Code
                    End If
                    'If clsCommon.myLen(strbank) > 0 AndAlso clsCommon.myLen(branchcode) <= 0 Then
                    '    Throw New Exception("Branch code not exist for filled bank,please fill branch also")
                    'End If
                    ''richa agarwal 26/03/2015
                    ifcicode = clsCommon.myCstr(grow.Cells("IFSC_Code").Value)
                    If clsCommon.myLen(ifcicode) > 100 Then
                        Throw New Exception("IFSC Code should be max 100 character")
                    End If
                    If clsDBFuncationality.getSingleValue("Select count(*) from TSPL_Vendor_Bank_Branch_Details where Bank_IFSC_Code ='" + ifcicode + "'  and TSPL_Vendor_Bank_Branch_Details.Bank_Code='" & strbank & "' ", trans) <= 0 Then
                        Throw New Exception("IFSC Code Does Not Exist :  " + ifcicode + " for bank " + strbank + "  .Please make entry in vendor bank master.")
                    End If
                    branchname = clsCommon.myCstr(grow.Cells("Branch_Name").Value)
                    If clsCommon.myLen(branchname) > 100 Then
                        Throw New Exception("Branch Name should be max 100 character")
                    End If

                    If clsDBFuncationality.getSingleValue("Select count(*) from TSPL_Vendor_Bank_Branch_Details where Bank_IFSC_Code ='" + ifcicode + "'  and TSPL_Vendor_Bank_Branch_Details.Bank_Code='" & strbank & "' and TSPL_Vendor_Bank_Branch_Details.Branch_Name='" & branchname & "'", trans) <= 0 Then
                        Throw New Exception("Branch Name Does Not Exist : " + branchname + " for bank " + strbank + "  .Please make entry in vendor bank master.")
                    End If
                    branchcode = ifcicode
                    ''-------------------------

                    Dim Cheque_In_favour_of As String = clsCommon.myCstr(grow.Cells("Cheque in Favour of").Value)
                    Dim accno As String = clsCommon.myCstr(grow.Cells("Account_No").Value)
                    Dim Industry_Type As String = clsCommon.myCstr(grow.Cells("Industry_Type").Value)
                    Dim Industry_Person As String = ""
                    Dim Agreement As String = clsCommon.myCstr(grow.Cells("Agreement").Value)
                    Dim Start_Date As String = clsCommon.myCstr(grow.Cells("Start_Date").Value)

                    If clsCommon.CompairString(Agreement, "YES") = CompairStringResult.Equal AndAlso clsCommon.myLen(Start_Date) > 0 AndAlso IsDate(Start_Date) Then
                        Start_Date = clsCommon.GetPrintDate(clsCommon.myCDate(Start_Date), "dd/MMM/yyyy")
                    Else
                        Start_Date = ""
                    End If

                    Dim End_Date As String = clsCommon.myCstr(grow.Cells("End_Date").Value)

                    If clsCommon.CompairString(Agreement, "YES") = CompairStringResult.Equal AndAlso clsCommon.myLen(End_Date) > 0 AndAlso IsDate(End_Date) Then
                        End_Date = clsCommon.GetPrintDate(clsCommon.myCDate(End_Date), "dd/MMM/yyyy")
                    Else
                        End_Date = ""
                    End If


                    If clsCommon.myLen(Industry_Type) <= 0 Then
                        Throw New Exception("Please Mention Industry Type,See At Line No. " + clsCommon.myCstr(counter) + "")
                    End If

                    If clsCommon.CompairString(Industry_Type, "Prop.") = CompairStringResult.Equal Then
                        Industry_Person = clsCommon.myCstr(grow.Cells("Prop Name").Value)
                        If clsCommon.myLen(Industry_Person) <= 0 Then
                            Throw New Exception("Please Mention Prop. Person Name,See At Line No. " + clsCommon.myCstr(counter) + "")
                        End If
                    ElseIf clsCommon.CompairString(Industry_Type, "Partnership") = CompairStringResult.Equal Then
                        Industry_Person = clsCommon.myCstr(grow.Cells("Partner Name").Value)
                        If clsCommon.myLen(Industry_Person) <= 0 Then
                            Throw New Exception("Please Mention Partner Name,See At Line No. " + clsCommon.myCstr(counter) + "")
                        End If
                    ElseIf clsCommon.CompairString(Industry_Type, "Public") = CompairStringResult.Equal Then
                        Industry_Person = clsCommon.myCstr(grow.Cells("Director Name").Value)
                        If clsCommon.myLen(Industry_Person) <= 0 Then
                            Throw New Exception("Please Mention Director Name,See At Line No. " + clsCommon.myCstr(counter) + "")
                        End If
                    ElseIf clsCommon.CompairString(Industry_Type, "Pvt") = CompairStringResult.Equal Then
                        Industry_Person = clsCommon.myCstr(grow.Cells("Director Name").Value)
                        If clsCommon.myLen(Industry_Person) <= 0 Then
                            Throw New Exception("Please Mention Director Name,See At Line No. " + clsCommon.myCstr(counter) + "")
                        End If
                    End If
                    If clsCommon.myLen(clsCommon.myCstr(grow.Cells("PAN").Value)) > 0 AndAlso NotAllowDuplicatePANOnPrimaryTransporter = True Then
                        Dim isNewEntry As Boolean = clsCommon.myCBool(clsDBFuncationality.getSingleValue("select count(*) from TSPL_VENDOR_MASTER  where  vendor_code='" + strvendorNo + "'", trans))
                        Dim CodeWherePanExist As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   isnull(STUFF((SELECT distinct ',' + QUOTENAME(ISNULL(TSPL_VENDOR_MASTER.Vendor_Code,'')) as Alies_Name from TSPL_VENDOR_MASTER  where form_type='PTM'  and PAN = '" + clsCommon.myCstr(grow.Cells("PAN").Value) + "'  " + IIf(isNewEntry, " and vendor_code <> '" + strvendorNo + "' ", "") + " FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,''),'')", trans))
                        If clsCommon.myLen(CodeWherePanExist) > 0 Then
                            Throw New Exception("Enter PAN No Used in Transporter No : " + CodeWherePanExist + " At Line No. " + clsCommon.myCstr(counter) + "")
                        End If
                        Dim msg As String = clsERPFuncationality.CheckPanStructure(clsCommon.myCstr(grow.Cells("PAN").Value), strvendorname, trans)
                        If clsCommon.myLen(msg) > 0 Then
                            Throw New Exception(" " + msg + " for " + strvendorNo + " Transporter No At Line No. " + clsCommon.myCstr(counter) + "")
                        End If
                    End If

                    If clsCommon.myLen(clsCommon.myCstr(grow.Cells("Aadhar_No").Value)) > 0 Then
                        Dim isNewEntry As Boolean = clsCommon.myCBool(clsDBFuncationality.getSingleValue("select count(*) from TSPL_VENDOR_MASTER  where  vendor_code='" + strvendorNo + "'", trans))
                        Dim CodeWhereAadharExist As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   isnull(STUFF((SELECT distinct ',' + QUOTENAME(ISNULL(TSPL_VENDOR_MASTER.Vendor_Code,'')) as Alies_Name from TSPL_VENDOR_MASTER  where form_type='PTM'  and Aadhar_No = '" + clsCommon.myCstr(grow.Cells("Aadhar_No").Value) + "'  " + IIf(isNewEntry, " and vendor_code <> '" + strvendorNo + "' ", "") + " FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,''),'')", trans))
                        If clsCommon.myLen(CodeWhereAadharExist) > 0 Then
                            Throw New Exception("Duplicate Aadhar No Not Allow .Enter Aadhar No Used in Transporter No : " + CodeWhereAadharExist + " At Line No. " + clsCommon.myCstr(counter) + "")
                        End If
                    End If

                    ''-----GST Import Vedor Master
                    Dim GSTSate_COde As String = ""
                    Dim PanNo As String = ""

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
                    ''-----GST Import Vedor Master

                    Dim GSTQry As String = ""
                    Dim security As String = clsCommon.myCstr(grow.Cells("Security_Cheque").Value)
                    Dim noofinstlmnt As Decimal = clsCommon.myCdbl(grow.Cells("No_of_Installment").Value)
                    Dim amtofinstlmnt As Decimal = clsCommon.myCdbl(grow.Cells("Amount_of_Installment").Value)
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

                    'If clsCommon.CompairString(security, "NO") = CompairStringResult.Equal AndAlso (clsCommon.myCdbl(noofinstlmnt) <= 0 Or clsCommon.myCdbl(amtofinstlmnt) <= 0) Then
                    '    Throw New Exception("Please Fill No of Installment/Amount of Installment At Line No. " + clsCommon.myCstr(counter) + "")
                    'End If
                    '---------------------------------------------------------------------------

                    '---------------------------------------------------------------------------


                    Dim sql1 As String = "select count(*) from TSPL_VENDOR_MASTER  where  vendor_code='" + strvendorNo + "'"
                    Dim i2 As Integer = CInt(connectSql.RunScalar(trans, sql1))
                    If (i2 = 0) Then
                        Dim strcmd As String = "Insert into TSPL_VENDOR_MASTER (Vendor_Code,Vendor_Name,Add1,Add2,Add3,Closing_Date ,Vendor_Group_Code,Vendor_Group_Code_Desc,City_Code ,City_Code_Desc,State,Country,Phone1,Phone2,Fax,Email,WebSite,Terms_Code,Terms_Code_Desc ,Vendor_Account,Vendor_Account_Desc,Payment_Code,Payment_Code_Desc,Bank_Code ,Bank_Code_Desc,Ven_Type_Code ,Ven_Type_Desc,Tax_Group ,Tax_Group_Desc,TAX1,TAX1_Rate,TAX2,TAX2_Rate,TAX3,TAX3_Rate,TAX4,TAX4_Rate,TAX5,TAX5_Rate,TAX6,TAX6_Rate,TAX7,TAX7_Rate,TAX8,TAX8_Rate,TAX9,TAX9_Rate,TAX10,TAX10_Rate,Transporter,created_by,created_date,modify_by,modify_date,comp_code,Collectorate,PAN,Inter_branch,franchise_yn,Form_Type,state_code,country_code,vsp_payee_name,zila,tehsil,branch_name,ifci_code,account_no,Industry_Type,Industry_Person,Agreement,security_cheque,no_of_installment,amount_of_installment,branch_code,Start_Date,End_Date,IFSC_Code,Cheque_In_favour_of,CURRENCY_CODE,Aadhar_No,Care_Of,SecChequeNoLac1,SecChequeNoRs100) values('" & strvendorNo & "','" & strvendorname & "','" & add1 & "','" & add2 & "','" & add3 & "','" & closing_date & "','" & strgroupCode & "','" & strgroupDes & "','" & citycode & "','" & citycodedesc & "','" & state & "','" & country & "','" & phonenum1 & "','" & phonenum2 & "','" & fax & "','" & emailid & "','" & website & "','" & strtermcode & "','" & strtermdes & "','" & vendoracct & "','" & vendoracctdesc & "','" & paymenttype & "','" & paymenttypedesc & "','" & strbank & "','" & strbankdes & "','" & strvendortype & "','" & strvendortypedes & "','" & strTax & "','" & strtaxdes & "','" & grow.Cells("Tax1").Value.ToString() & "','" & clsCommon.myCdbl(grow.Cells("Tax1 Rate").Value.ToString()) & "','" & grow.Cells("Tax2").Value.ToString() & "','" & clsCommon.myCstr(clsCommon.myCdbl(grow.Cells("Tax2 Rate").Value)) & "','" & grow.Cells("Tax3").Value.ToString() & "','" & clsCommon.myCstr(clsCommon.myCdbl(grow.Cells("Tax3 Rate").Value)) & "','" & grow.Cells("Tax4").Value.ToString() & "','" & clsCommon.myCstr(clsCommon.myCdbl(grow.Cells("Tax4 Rate").Value)) & "','" & grow.Cells("Tax5").Value.ToString() & "','" & clsCommon.myCstr(clsCommon.myCdbl(grow.Cells("Tax5 Rate").Value)) & "','" & grow.Cells("Tax6").Value.ToString() & "','" & clsCommon.myCstr(clsCommon.myCdbl(grow.Cells("Tax6 Rate").Value)) & "','" & grow.Cells("Tax7").Value.ToString() & "','" & clsCommon.myCstr(clsCommon.myCdbl(grow.Cells("Tax7 Rate").Value)) & "','" & grow.Cells("Tax8").Value.ToString() & "','" & clsCommon.myCstr(clsCommon.myCdbl(grow.Cells("Tax8 Rate").Value)) & "','" & grow.Cells("Tax9").Value.ToString() & "','" & clsCommon.myCstr(clsCommon.myCdbl(grow.Cells("Tax9 Rate").Value)) & "','" & grow.Cells("Tax10").Value.ToString() & "','" & clsCommon.myCstr(clsCommon.myCdbl(grow.Cells("Tax10 Rate").Value)) & "','" & grow.Cells("Transporter").Value.ToString() & "','" & userCode & "','" & connectSql.serverDate(trans) & "','" & userCode & "','" & connectSql.serverDate(trans) & "','" & companyCode & "','" & grow.Cells("Collectorate").Value.ToString() & "','" & grow.Cells("PAN").Value.ToString() & "','" & interbranch & "','" & strTagAsFranchise & "','PTM','" & statecode & "','" & countrycode & "','" & payeename & "','" & zila & "','" & tehsil & "','" & branchname & "','" & ifcicode & "','" & accno & "','" & Industry_Type & "','" & Industry_Person & "','" & Agreement & "','" & security & "','" & noofinstlmnt & "','" & amtofinstlmnt & "','" & branchcode & "'," & IIf(clsCommon.myLen(Start_Date) > 0, "'" & Start_Date & "'", "null") & "," & IIf(clsCommon.myLen(End_Date) > 0, "'" & End_Date & "'", "null") & ",'" & branchcode & "','" & clsCommon.myCstr(grow.Cells("Cheque in Favour of").Value) & "','" & strCurrencyCode & "','" + strAadharNo + "','" + strCareOf + "','" + strSecChequeNoLac1 + "','" + strSecChequeNoRs100 + "')"
                        connectSql.RunSqlTransaction(trans, strcmd)

                        GSTQry = "Update TSPL_VENDOR_MASTER set GSTRegistered='" & Registered & "',GSTEntity='" & GSTEntity & "',GSTLastEntity='" & GSTLastEntity & "',GSTFinalNo='" & GSTFinal & "',GSTMiddle='" & GSTMiddleEntity & "' where Vendor_Code='" & strvendorNo & "'"
                        connectSql.RunSqlTransaction(trans, GSTQry)

                    Else
                        Dim strcmd As String = "Update  TSPL_VENDOR_MASTER set  Vendor_Name='" & strvendorname & "',add1='" & add1 & "',add2='" & add2 & "',add3='" & add3 & "',Closing_Date='" & closing_date & "',Vendor_Group_Code='" & strgroupCode & "',Vendor_Group_Code_Desc='" & strgroupDes & "',City_Code='" & citycode & "',City_Code_Desc='" & citycodedesc & "',State='" & state & "',Country='" & country & "',Phone1='" & phonenum1 & "',Phone2='" & phonenum2 & "',Fax='" & fax & "',Email='" & emailid & "',WebSite='" & website & "',Contact_Person_Name='" & contct_person_name & "',Contact_Person_Phone='" & contct_perfson_phone & "',Contact_Person_Fax='" & contct_person_fax & "',Contact_Person_Website='" & contct_person_website & "',Contact_Person_Email='" & contct_person_email & "',Terms_Code='" & strtermcode & "',Terms_Code_Desc='" & strtermdes & "' ,Vendor_Account='" & vendoracct & "',Vendor_Account_Desc='" & vendoracctdesc & "',Payment_Code='" & paymenttype & "',Payment_Code_Desc='" & paymenttypedesc & "',Bank_Code='" & strbank & "', Bank_Code_Desc='" & strbankdes & "',Ven_Type_Code='" & strvendortype & "',Ven_Type_Desc='" & strvendortypedes & "' ,Tax_Group='" & strTax & "',Tax_Group_Desc='" & strtaxdes & "' ,TAX1='" & grow.Cells("Tax1").Value.ToString() & "',TAX1_Rate='" & clsCommon.myCstr(clsCommon.myCdbl(grow.Cells("Tax1 Rate").Value)) & "',TAX2='" & grow.Cells("Tax2").Value.ToString() & "',TAX2_Rate='" & clsCommon.myCstr(clsCommon.myCdbl(grow.Cells("Tax2 Rate").Value)) & "',TAX3='" & grow.Cells("Tax3").Value.ToString() & "',TAX3_Rate='" & clsCommon.myCstr(clsCommon.myCdbl(grow.Cells("Tax3 Rate").Value)) & "',TAX4='" & grow.Cells("Tax4").Value.ToString() & "',TAX4_Rate='" & clsCommon.myCstr(clsCommon.myCdbl(grow.Cells("Tax4 Rate").Value)) & "',TAX5='" & grow.Cells("Tax5").Value.ToString() & "',TAX5_Rate='" & clsCommon.myCstr(clsCommon.myCdbl(grow.Cells("Tax5 Rate").Value)) & "',TAX6='" & grow.Cells("Tax6").Value.ToString() & "',TAX6_Rate='" & clsCommon.myCstr(clsCommon.myCdbl(grow.Cells("Tax6 Rate").Value)) & "',TAX7='" & grow.Cells("Tax7").Value.ToString() & "',TAX7_Rate='" & clsCommon.myCstr(clsCommon.myCdbl(grow.Cells("Tax7 Rate").Value)) & "',TAX8='" & grow.Cells("Tax8").Value.ToString() & "',TAX8_Rate='" & clsCommon.myCstr(clsCommon.myCdbl(grow.Cells("Tax8 Rate").Value)) & "',TAX9='" & grow.Cells("Tax9").Value.ToString() & "',TAX9_Rate='" & clsCommon.myCstr(clsCommon.myCdbl(grow.Cells("Tax9 Rate").Value)) & "',TAX10='" & grow.Cells("Tax10").Value.ToString() & "',TAX10_Rate='" & clsCommon.myCstr(clsCommon.myCdbl(grow.Cells("Tax10 Rate").Value)) & "',Transporter='" & grow.Cells("Transporter").Value.ToString() & "',modify_by='" & userCode & "',modify_date='" & connectSql.serverDate(trans) & "',comp_code='" & companyCode & "',Collectorate='" & grow.Cells("Collectorate").Value.ToString() & "',PAN='" & grow.Cells("PAN").Value.ToString() & "',Inter_Branch='" & interbranch & "', franchise_yn='" & strTagAsFranchise & "',form_type='PTM',state_code='" & statecode & "',country_code='" & countrycode & "',vsp_payee_name='" & payeename & "',zila='" & zila & "',tehsil='" & tehsil & "',branch_name='" & branchname & "',ifci_code='" & ifcicode & "',account_no='" & accno & "',Industry_Type='" & Industry_Type & "',Industry_Person='" & Industry_Person & "',Agreement='" & Agreement & "',security_cheque='" & security & "',no_of_installment='" & noofinstlmnt & "',amount_of_installment='" & amtofinstlmnt & "',branch_code='" & branchcode & "' , start_date=" & IIf(clsCommon.myLen(Start_Date) > 0, "'" & Start_Date & "'", "null") & ", End_Date=" & IIf(clsCommon.myLen(End_Date) > 0, "'" & End_Date & "'", "null") & ",IFSC_Code='" & branchcode & "',Cheque_In_favour_of='" & clsCommon.myCstr(grow.Cells("Cheque in Favour of").Value) & "',CURRENCY_CODE='" & strCurrencyCode & "',Aadhar_No='" + strAadharNo + "',Care_Of='" + strCareOf + "',SecChequeNoLac1='" + strSecChequeNoLac1 + "',SecChequeNoRs100='" + strSecChequeNoRs100 + "' where vendor_code='" & strvendorNo & "' and form_type='PTM'"
                        connectSql.RunSqlTransaction(trans, strcmd)

                        GSTQry = "Update TSPL_VENDOR_MASTER set GSTRegistered='" & Registered & "',GSTEntity='" & GSTEntity & "',GSTLastEntity='" & GSTLastEntity & "',GSTFinalNo='" & GSTFinal & "',GSTMiddle='" & GSTMiddleEntity & "'  where Vendor_Code='" & strvendorNo & "'"
                        connectSql.RunSqlTransaction(trans, GSTQry)

                    End If
                    counter += 1
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
            funreset()
            If clsCommon.myLen(strvalue) > 0 Then
                fndvendorNo.Value = strvalue
                funfill()
                btnsave.Text = "Update"
                btnsave.Enabled = True
                btndelete.Enabled = True
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
    'Sub fndcity_text_Changed()
    '    Try
    '        Dim str As String = "select City_Code,city_Name from TSPL_City_MASTER where City_code = '" + fndCity.Value + "'"
    '        Dim dr As DataTable
    '        Dim strvalue As String
    '        dr = clsDBFuncationality.GetDataTable(str)
    '        If dr.Rows IsNot Nothing AndAlso dr.Rows.Count > 0 Then

    '            strvalue = dr.Rows(0)("City_Code").ToString()
    '        End If
    '        If strvalue <> "" Then
    '            'funfilfndcity()
    '        Else
    '            txtCity.Text = ""

    '        End If
    '    Catch ex As Exception

    '        myMessages.myExceptions(ex)
    '    End Try
    'End Sub
    'Public Sub fndcity_text_Changed(ByVal sender As System.Object, ByVal e As System.EventArgs)

    'End Sub
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
                txtbankcodedes.Text = ""
                txtbranchname.Text = ""
                txtifcicode.Text = ""
                txtaccno.Text = ""
                txtbankcity.Text = ""
                txtbankcountry.Text = ""
                txtbankstate.Text = ""
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub
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
    'Sub fndCity_leave()
    '    If fndCity.Value = "" Then
    '    Else
    '        Try
    '            Dim strquery As String = "select City_Code ,city_Name from TSPL_City_MASTER where City_code = '" + fndCity.Value + "'"
    '            Dim dr As DataTable
    '            Dim strvalue As String
    '            dr = clsDBFuncationality.GetDataTable(strquery)
    '            If dr.Rows IsNot Nothing AndAlso dr.Rows.Count > 0 Then
    '                strvalue = dr.Rows(0)("City_Code").ToString()
    '            End If
    '            If strvalue <> "" Then
    '            Else : strquery = ""
    '                txtCity.Text = ""
    '                common.clsCommon.MyMessageBoxShow("This City does not exist in Master Table")
    '                fndCity.Value = ""
    '            End If
    '        Catch ex As Exception
    '            myMessages.myExceptions(ex)
    '        End Try
    '    End If
    'End Sub
    'Public Sub fndCity_leave(ByVal sender As System.Object, ByVal e As System.EventArgs)

    'End Sub
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
                    txtbankcodedes.Text = ""
                    common.clsCommon.MyMessageBoxShow("This Bank Code does not exist in Master Table")
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
    'Public Sub fndCity_key_press(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)

    '    If (e.KeyChar = Chr(39)) Then
    '        e.Handled = True
    '    End If
    'End Sub

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
    Private Sub txtPanNo_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtpan.Leave
        If txtpan.Text = "" Then
        Else
            Dim check As Match = Regex.Match(txtpan.Text, "[A-Z]{5}\d{4}[A-Z]{1}")
            If check.Success Then
                Errorcontrol.ResetError(txtpan)
            Else
                common.clsCommon.MyMessageBoxShow("Please Enter Valid PAN No.")
                txtpan.Text = ""
                'txtpan.Focus()
                'txtpan.Select()
                Errorcontrol.SetError(txtpan, "Please Enter Valid PAN No.")
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
    Private Sub txtPhone1_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPhone1.KeyPress
        If ((e.KeyChar >= Chr(48) And e.KeyChar <= Chr(57)) Or e.KeyChar = Chr(8) Or e.KeyChar = Chr(48) Or e.KeyChar = Chr(45)) Then
        Else
            e.Handled = True
        End If
    End Sub

    Private Sub txtPhone2_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPhone2.KeyPress
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

    Private Sub txtContPhone_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtContPhone.KeyPress
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
            If txtvendorname.Text = "" Then
                myMessages.blankValue("Please Fill Primary Transporter Name")
                pageCus.SelectedPage = RadPageViewPage1
                txtvendorname.Focus()
                txtvendorname.Select()

                Errorcontrol.SetError(txtvendorname, "Please Fill Primary Transporter Name")
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

            If fndTrmsCode.Value = "" Then
                myMessages.blankValue("Please Select Terms Code")
                pageCus.SelectedPage = RadPageViewPage4
                fndTrmsCode.Focus()
                fndTrmsCode.Select()

                Errorcontrol.SetError(fndTrmsCode, "Please Select Terms Code")
                Return
            Else
                Errorcontrol.ResetError(fndTrmsCode)
            End If

            If fndAccntSet.Value = "" Then
                myMessages.blankValue("Please Select Account Set")
                pageCus.SelectedPage = RadPageViewPage4
                fndAccntSet.Focus()
                fndAccntSet.Select()
                Errorcontrol.SetError(fndAccntSet, "Please Select Account Code")
                Return
            Else
                Errorcontrol.ResetError(fndAccntSet)
            End If

            If fndbankcode.Value = "" Then
                myMessages.blankValue("Please Select Bank Code")
                pageCus.SelectedPage = RadPageViewPage1
                fndbankcode.Focus()
                fndbankcode.Select()
                Errorcontrol.SetError(fndbankcode, "Please Select Bank Code")
                Return
            Else
                Errorcontrol.ResetError(fndbankcode)
            End If
            ''richa agarwal 27/03/2015
            If txtbranchcode.Value = "" Then
                myMessages.blankValue("Please Select IFSC Code")
                pageCus.SelectedPage = RadPageViewPage1
                txtbranchcode.Focus()
                txtbranchcode.Select()
                Errorcontrol.SetError(txtbranchcode, "Please Select IFSC Code")
                Return
            Else
                Errorcontrol.ResetError(txtbranchcode)
            End If
            ''------------------------
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

            For Each grow As GridViewRowInfo In gvMCC.Rows
                If clsCommon.myLen(grow.Cells("COLMCC_Name").Value) <= 0 Then
                    myMessages.blankValue("Please Fill Mcc Name")
                    pageCus.SelectedPage = RadPageViewPage6
                    gvMCC.Focus()
                    gvMCC.Select()
                    Errorcontrol.SetError(gvMCC, "Please Select Mcc Name")
                    Return
                Else
                    Errorcontrol.ResetError(gvMCC)
                End If
            Next
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



                'If clsCommon.CompairString(clsCommon.myCstr(fndVendorCurrency.Value), objCommonVar.BaseCurrencyCode) <> CompairStringResult.Equal Then

                '' when vendor currency is other than base currency of the company
                '' match account set currency with vendor currency
                Dim qry As String
                qry = "select CURRENCY_CODE from TSPL_VENDOR_ACCOUNT_SET where Acct_Set_Code='" & clsCommon.myCstr(Me.fndAccntSet.Value) & "' "
                Dim accCurrCode As String = clsDBFuncationality.getSingleValue(qry).ToString
                If clsCommon.CompairString(accCurrCode, clsCommon.myCstr(Me.fndVendorCurrency.Value)) <> CompairStringResult.Equal Then
                    clsCommon.MyMessageBoxShow("Account Set Currency and Vendor Currency must be same in case of Multicurrency.")
                    Exit Sub
                End If
                '' match tax Group currency with vendor currency
                qry = " select TSPL_TAX_GROUP_DETAILS.Tax_Code,coalesce(TSPL_TAX_MASTER.CURRENCY_CODE,'') as CURRENCY_CODE from TSPL_TAX_GROUP_DETAILS " & _
                      " inner join TSPL_TAX_GROUP_MASTER on TSPL_TAX_GROUP_DETAILS.Tax_Group_Code=TSPL_TAX_GROUP_MASTER.Tax_Group_Code " & _
                      " inner join TSPL_TAX_MASTER on TSPL_TAX_GROUP_DETAILS.Tax_Code=TSPL_TAX_MASTER.Tax_Code where TSPL_TAX_GROUP_MASTER.Tax_Group_Code='" & clsCommon.myCstr(Me.fndTxGrp.Value) & "' " & _
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


            'If clsCommon.myLen(txtpan.Text) <= 0 Then
            '    clsCommon.MyMessageBoxShow("Please Fill PAN No.")
            '    'pageCus.SelectedPage = RadPageViewPage1
            '    txtpan.Focus()
            '    txtpan.Select()
            '    Errorcontrol.SetError(txtpan, "Please Fill PAN No.")
            '    Exit Sub
            'Else
            '    Errorcontrol.ResetError(txtpan)
            'End If
            If clsCommon.myLen(clsCommon.myCstr(txtAadharNo.Text)) > 0 Then
                Dim isNewEntry As Boolean = clsCommon.myCBool(clsDBFuncationality.getSingleValue("select count(*) from TSPL_VENDOR_MASTER  where  vendor_code='" + fndvendorNo.Value + "'"))
                Dim CodeWhereAadharExist As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   isnull(STUFF((SELECT distinct ',' + QUOTENAME(ISNULL(TSPL_VENDOR_MASTER.Vendor_Code,'')) as Alies_Name from TSPL_VENDOR_MASTER  where form_type='PTM'  and Aadhar_No = '" + clsCommon.myCstr(txtAadharNo.Text) + "'  " + IIf(isNewEntry, " and vendor_code <> '" + fndvendorNo.Value + "' ", "") + " FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,''),'')"))
                If clsCommon.myLen(CodeWhereAadharExist) > 0 Then
                    Throw New Exception("Duplicate Aadhar No Not Allow .Enter Aadhar No Used in Transporter No : " + CodeWhereAadharExist + " ")
                End If
            End If

            If clsCommon.myLen(txtpan.Text) > 0 Then
                Dim qry As String = "select count(*) from tspl_vendor_master where PAN='" + clsCommon.myCstr(txtpan.Text) + "' and Form_Type='PTM'"
                Dim check As Integer = clsDBFuncationality.getSingleValue(qry)

                If clsCommon.CompairString(btnsave.Text, "Save") = CompairStringResult.Equal AndAlso check > 0 Then
                    clsCommon.MyMessageBoxShow("Pan No. You Entered Is Already Exist,Please Enter Valid,Unique Pan No.", Me.Text)
                    '   pageCus.SelectedPage = RadPageViewPage1
                    txtpan.Focus()
                    txtpan.Select()
                    Errorcontrol.SetError(txtpan, "Pan No. You Entered Is Already Exist,Please Enter Valid,Unique Pan No.")
                    Return
                Else
                    Errorcontrol.ResetError(txtpan)
                End If

                If clsCommon.CompairString(btnsave.Text, "Save") <> CompairStringResult.Equal AndAlso check > 1 Then
                    clsCommon.MyMessageBoxShow("Pan No. You Entered Is Already Exist,Please Enter Valid,Unique Pan No.", Me.Text)
                    '  pageCus.SelectedPage = RadPageViewPage1
                    txtpan.Focus()
                    txtpan.Select()
                    Errorcontrol.SetError(txtpan, "Pan No. You Entered Is Already Exist,Please Enter Valid,Unique Pan No.")
                    Return
                Else
                    Errorcontrol.ResetError(txtpan)
                End If
                '=============BM00000003721========
                Dim msg As String = clsERPFuncationality.CheckPanStructure(txtpan.Text.Trim(), txtvendorname.Text)

                If clsCommon.myLen(msg) > 0 Then
                    clsCommon.MyMessageBoxShow(msg, Me.Text)
                    'pageCus.SelectedPage = RadPageViewPage1
                    txtpan.Focus()
                    txtpan.Select()
                    Errorcontrol.SetError(txtpan, msg)
                    Exit Sub
                Else
                    Errorcontrol.ResetError(txtpan)
                End If
                '==============================================
            End If

            If clsCommon.myLen(txtAdd1.Text) <= 0 AndAlso clsCommon.myLen(txtAdd2.Text) <= 0 AndAlso clsCommon.myLen(txtAdd3.Text) <= 0 Then
                clsCommon.MyMessageBoxShow("Please Fill Address Of Primary Transporter Master")
                txtAdd1.Focus()
                txtAdd1.Select()
                Errorcontrol.SetError(txtAdd1, "Please Fill Address Of Primary Transporter Master")
                Errorcontrol.SetError(txtAdd2, "Please Fill Address Of Primary Transporter Master")
                Errorcontrol.SetError(txtAdd3, "Please Fill Address Of Primary Transporter Master")
                Exit Sub
            Else
                Errorcontrol.ResetError(txtAdd1)
                Errorcontrol.ResetError(txtAdd2)
                Errorcontrol.ResetError(txtAdd3)
            End If

            If clsCommon.myLen(txtcountrycode.Value) <= 0 Then
                clsCommon.MyMessageBoxShow("Please Select Country")
                txtcountrycode.Focus()
                txtcountrycode.Select()
                Errorcontrol.SetError(txtcountrycode, "Please Select Country")
                Exit Sub
            Else
                Errorcontrol.ResetError(txtcountrycode)
            End If

            If clsCommon.myLen(txtstatecode.Value) <= 0 Then
                clsCommon.MyMessageBoxShow("Please Select State")
                txtstatecode.Focus()
                txtstatecode.Select()
                Errorcontrol.SetError(txtstatecode, "Please Select State")
                Exit Sub
            Else
                Errorcontrol.ResetError(txtstatecode)
            End If

            If clsCommon.myLen(fndbankcode.Value) > 0 AndAlso clsCommon.myLen(txtpayeename.Text) <= 0 Then
                clsCommon.MyMessageBoxShow("Please Fill Payee Name For Selected Bank", Me.Text)
                txtpayeename.Focus()
                txtpayeename.Select()
                Errorcontrol.SetError(txtpayeename, "Please Fill Payee Name For Selected Bank")
                Return
            Else
                Errorcontrol.ResetError(txtpayeename)
            End If

            If clsCommon.myLen(fndbankcode.Value) > 0 AndAlso clsCommon.myLen(txtbranchcode.Value) <= 0 Then
                clsCommon.MyMessageBoxShow("Please select branch detail", Me.Text)
                txtbranchcode.Focus()
                txtbranchcode.Select()
                Errorcontrol.SetError(txtbranchcode, "Please select branch detail")
                Return
            Else
                Errorcontrol.ResetError(txtbranchcode)
            End If

            If rbtnpartnership.IsChecked = False AndAlso rbtnprop.IsChecked = False AndAlso rbtnpublic.IsChecked = False AndAlso rbtnpvt.IsChecked = False Then
                clsCommon.MyMessageBoxShow("Please Select Nature of Industry", Me.Text)
                Errorcontrol.SetError(RadGroupBox6, "Please Select Nature of Industry")
                Exit Sub
            Else
                Errorcontrol.ResetError(RadGroupBox6)
            End If

            If rbtnprop.IsChecked AndAlso clsCommon.myLen(txtprop_name.Text) <= 0 Then
                clsCommon.MyMessageBoxShow("Please Fill Prop. Name", Me.Text)
                txtprop_name.Focus()
                txtprop_name.Select()
                Errorcontrol.SetError(txtprop_name, "Please Fill Prop. Name")
                Exit Sub
            Else
                Errorcontrol.ResetError(txtprop_name)
            End If

            If rbtnpartnership.IsChecked AndAlso clsCommon.myLen(txtpartner_name.Text) <= 0 Then
                clsCommon.MyMessageBoxShow("Please Fill Partner Name", Me.Text)
                txtpartner_name.Focus()
                txtpartner_name.Select()
                Errorcontrol.SetError(txtpartner_name, "Please Fill Partner Name")
                Exit Sub
            Else
                Errorcontrol.ResetError(txtpartner_name)
            End If

            If (rbtnpublic.IsChecked Or rbtnpvt.IsChecked) AndAlso clsCommon.myLen(txtdirectr_name.Text) <= 0 Then
                clsCommon.MyMessageBoxShow("Please Fill Director Name", Me.Text)
                txtdirectr_name.Focus()
                txtdirectr_name.Select()
                Errorcontrol.SetError(txtdirectr_name, "Please Fill Director Name")
                Exit Sub
            Else
                Errorcontrol.ResetError(txtdirectr_name)
            End If

            If (clsCommon.CompairString(cmbagreemnt.SelectedValue, "YES") = CompairStringResult.Equal Or clsCommon.CompairString(cmbsecurity.Text, "YES") = CompairStringResult.Equal) AndAlso UcAttachment1.gv1.Rows.Count < 1 Then
                clsCommon.MyMessageBoxShow("Please Attached Document For Agreement/Security", Me.Text)
                UcAttachment1.gv1.Focus()
                UcAttachment1.gv1.Select()
                Errorcontrol.SetError(UcAttachment1.btnaddNewAttachment, "Please Attached Document For Agreement/Security")
                Return
            Else
                Errorcontrol.ResetError(UcAttachment1.btnaddNewAttachment)
            End If


            UcCustomFields1.AllowToSave()

            If MyBase.isModifyonPasswordFlag Then
                If clsPasswordCheckForMasters.CheckMasterPwd(clsUserMgtCode.frmPrimaryTransporterMaster, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
                Else
                    Return
                End If
            End If

            If clsCommon.myCdbl(Rchkregistered.Checked) > 0 Then
                Dim GSTFinal As String = clsCommon.myCstr(txtGSTStateCode.Text) + clsCommon.myCstr(txtGST_PanCode.Text) + clsCommon.myCstr(txtEntity.Text) + clsCommon.myCstr(txtFixxed.Text) + clsCommon.myCstr(MyTextBox2.Text)
                txtGSTIN_No_final.Text = GSTFinal
                clsERPFuncationality.ValidationGSTNO(txtGSTStateCode.Text, txtpan.Text, GSTFinal, Nothing)
            End If
            If txtSecurityDeductedAmountEditable.Value > TxtSecurityAmountEditable.Value Then
                clsCommon.MyMessageBoxShow("Security Monthly Deduction amount can't be more than Total security amount", Me.Text)
                Exit Sub
            End If
            If btnsave.Text = "Save" Then
                funinsert()
            ElseIf btnsave.Text = "Update" Then
                funupdate()
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub
    Private Sub btndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click
        DeleteData()
    End Sub
    Sub DeleteData()
        If fndvendorNo.Value = "" Then
            myMessages.blankValue("Transporter No.")
        ElseIf myMessages.deleteConfirm() Then
            fundelete()

        End If
    End Sub



    Private Sub btnnew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnnew.Click
        funreset()
        ''For Custom Fields
        If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
            UcCustomFields1.SetDefaultValues()
        End If
        ''End of For Custom Fields
    End Sub

    Private Sub MenuExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuExport.Click

    End Sub


    Private Sub RadMenuItem4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem4.Click
        Me.Close()
    End Sub
#End Region


    Private Sub FrmPrimaryTransporterMaster_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
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
            fndvendorNo.Value = clsVendorMaster.getFinder(" form_type='PTM'", fndvendorNo.Value, isButtonClicked)
            txtvendorname.Text = clsDBFuncationality.getSingleValue("Select group_desc from tspl_vendor_group where ven_group_code='" + fndvendorNo.Value + "'")
            If fndvendorNo.Value IsNot Nothing Then
                btndelete.Enabled = True
            Else
                btndelete.Enabled = False
            End If
            fndvendorNo_text_changed()
            txtTranspoterCopy.Enabled = False
        ElseIf fndvendorNo.MyReadOnly OrElse fndvendorNo.Value IsNot Nothing Then
            Dim qry As String = "Select * from TSPL_VENDOR_MASTER where Vendor_Code ='" + fndvendorNo.Value + "' and form_type='" + txtvndrtype.Text + "' "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt.Rows.Count <= 0 Then
                funreset()
            Else
                fndvendorNo_text_changed()
                txtTranspoterCopy.Enabled = False
            End If
        End If
    End Sub

    Private Sub fndgroupcode__MYOpenMasterForm(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndgroupcode._MYOpenMasterForm
        Frm_Open = New frmVendorGroup(objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode)
        Frm_Open.Show()
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

    'Private Sub fndCity__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean)
    '    If clsCommon.myLen(txtstatecode.Value) <= 0 Then
    '        clsCommon.MyMessageBoxShow("Please Select State Code First", Me.Text)
    '        txtstatecode.Focus()
    '        txtstatecode.Select()
    '        Errorcontrol.SetError(txtstatecode, "Please Select State Code First")
    '        Return
    '    Else
    '        Errorcontrol.ResetError(txtstatecode)
    '    End If
    '    '    If isButtonClicked Then
    '    'Dim qry As String = "SELECT [City_Code] as [CityCode],[City_Name] as [City Name]FROM [TSPL_CITY_MASTER] "
    '    'fndCity.Value = clsCommon.ShowSelectForm("CityFmfnd", qry, "CityCode", "", fndCity.Value, "", isButtonClicked)
    '    fndCity.Value = clsCityMaster.getFinder(" state_code='" + txtstatecode.Value + "'", fndCity.Value, isButtonClicked)
    '    txtCity.Text = clsDBFuncationality.getSingleValue("Select City_Name from TSPL_CITY_MASTER where City_Code='" + fndCity.Value + "' and state_code='" + txtstatecode.Value + "'")
    '    ''fndcity_text_Changed()
    '    ''fndCity_leave()
    '    '  End If
    'End Sub

    Private Sub fndvendorNo__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavigatorType As common.NavigatorType) Handles fndvendorNo._MYNavigator
        Dim qst As String = "select Vendor_Code as [Vendor Code],Vendor_Name as [Vendor Name],Vendor_Group_Code as [Vendor Group],(select case when Status='N' then 'Active' else 'In.Active' end ) as [Status] from TSPL_VENDOR_MASTER    where  2=2 and form_type='PTM'"
        Select Case NavigatorType
            Case NavigatorType.Current
            Case NavigatorType.Next
                qst += "and Vendor_Code in (select min(Vendor_Code) from TSPL_VENDOR_MASTER where Vendor_Code>'" + fndvendorNo.Value + "' and form_type='" + txtvndrtype.Text + "'  ) "
            Case NavigatorType.First
                qst += "and Vendor_Code in (select MIN(Vendor_Code) from TSPL_VENDOR_MASTER where form_type='" + txtvndrtype.Text + "')"
            Case NavigatorType.Last
                qst += "and Vendor_Code in (select Max(Vendor_Code) from TSPL_VENDOR_MASTER where form_type='" + txtvndrtype.Text + "' )"
            Case NavigatorType.Previous
                qst += "and Vendor_Code in (select max(Vendor_Code) from TSPL_VENDOR_MASTER where Vendor_Code<'" + fndvendorNo.Value + "' and form_type='" + txtvndrtype.Text + "'  )"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qst)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            fndvendorNo.Value = clsCommon.myCstr(dt.Rows(0)("Vendor Code"))
            txtvendorname.Text = clsCommon.myCstr(dt.Rows(0)("Vendor Name"))
        End If
        If fndvendorNo.Value IsNot Nothing Then
            btndelete.Enabled = True
        Else
            btndelete.Enabled = False
        End If
        fndvendorNo_text_changed()
        txtTranspoterCopy.Enabled = False
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

    Private Sub fndbankcode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndbankcode._MYValidating
        'fndbankcode.ConnectionString = connectSql.SqlCon()
        ''fndbankcode.Query = " select bank_code As [Bank Code],description  as [Description]from TSPL_Bank_MASTER "
        'fndbankcode.Query = clsERPFuncationality.glbankquery
        'fndbankcode.ValueToSelect = "Bank Code"
        'fndbankcode.Caption = "Bank Master"
        'fndbankcode.ValueToSelect1 = "Description"
        'Dim whrcls As String = ""
        '        Dim qry As String = clsERPFuncationality.glbankquery(whrcls)
        ' Dim qry As String = clsERPFuncationality.glbankquery(whrcls)
        'fndbankcode.Value = clsCommon.ShowSelectForm("fndbannk", qry, "Bank Code", whrcls, fndbankcode.Value, "", isButtonClicked)
        fndbankcode.Value = clsVendorBankMaster.GetFinder("", fndbankcode.Value, isButtonClicked)

        'If clsCommon.myLen(fndbankcode.Value) > 0 Then
        '    txtbankcodedes.Text = clsDBFuncationality.getSingleValue("Select Description from TSPL_BANK_MASTER where BANK_CODE='" + fndbankcode.Value + "'")
        '    txtbankcountry.Text = clsDBFuncationality.getSingleValue("Select country from TSPL_BANK_MASTER where BANK_CODE='" + fndbankcode.Value + "'")
        '    txtbankstate.Text = clsDBFuncationality.getSingleValue("Select state from TSPL_BANK_MASTER where BANK_CODE='" + fndbankcode.Value + "'")
        '    txtbankcity.Text = clsDBFuncationality.getSingleValue("Select city from TSPL_BANK_MASTER where BANK_CODE='" + fndbankcode.Value + "'")
        '    txtaccno.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select bankaccnumber from TSPL_BANK_MASTER where BANK_CODE='" + fndbankcode.Value + "'"))
        '    'txtbranchname.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select branch_name from TSPL_BANK_branch_MASTER where BANK_CODE='" + fndbankcode.Value + "'"))
        '    'txtifcicode.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select ifsc_code from TSPL_BANK_branch_MASTER where BANK_CODE='" + fndbankcode.Value + "'"))
        '    txtbranchcode.Value = ""
        '    txtbranchname.Text = ""
        '    txtifcicode.Text = ""
        'Else
        '    txtbankcodedes.Text = ""
        '    txtbankcountry.Text = ""
        '    txtbankstate.Text = ""
        '    txtbankcity.Text = ""
        '    txtbranchname.Text = ""
        '    txtaccno.Text = ""
        '    txtifcicode.Text = ""
        '    txtbranchcode.Value = ""
        '    txtbranchname.Text = ""
        '    txtifcicode.Text = ""
        'End If
        If clsCommon.myLen(fndbankcode.Value) > 0 Then
            Dim obj As clsVendorBankMaster = clsVendorBankMaster.GetData(fndbankcode.Value, NavigatorType.Current)
            If obj Is Nothing Then
                Exit Sub
            End If
            txtbankcodedes.Text = obj.Bank_Name
            txtbankcountry.Text = obj.country_name
            txtbankstate.Text = obj.state_name
            txtbankcity.Text = obj.city_name
            'txtbranchcode.Value = obj.Branch_Code
            'txtbranchname.Text = obj.Branch_Name
            'txtifcicode.Text = obj.IFSC_Code
        Else
            txtbankcodedes.Text = ""
            txtbankcountry.Text = "India"
            txtbankstate.Text = ""
            txtbankcity.Text = ""
            '   txtbranchname.Text = ""
            txtaccno.Text = ""
            'txtifcicode.Text = ""
            'txtbranchcode.Value = ""
            'txtbranchname.Text = ""
            'txtifcicode.Text = ""
        End If

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



        fndTxGrp.Value = clsCommon.ShowSelectForm("fndtxx", qry, "Code", whrcls, fndTxGrp.Value, "", isButtonClicked)
        txtTxGrp.Text = clsDBFuncationality.getSingleValue("Select Tax_Group_Desc from TSPL_TAX_GROUP_MASTER where TAX_Group_Code='" + fndTxGrp.Value + "'")
    End Sub

    Private Sub fndBaseCurrency__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndVendorCurrency._MYValidating
        Dim qry As String = "select CURRENCY_CODE AS Code, CURRENCY_NAME as Name from TSPL_CURRENCY_MASTER"
        fndVendorCurrency.Value = clsCommon.ShowSelectForm("CURRENCY_MASTER", qry, "Code", "", fndVendorCurrency.Value, "CURRENCY_CODE", isButtonClicked)

    End Sub

    Private Sub txtcountrycode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtcountrycode._MYValidating
        'Try
        '    Dim qry As String = "select country_code as Code,country_name as Country from tspl_country_master"
        '    txtcountrycode.Value = clsCommon.ShowSelectForm("CNTFND", qry, "Code", "", txtcountrycode.Value, "Code", isButtonClicked)

        '    If clsCommon.myLen(txtcountrycode.Value) > 0 Then
        '        txtCountry.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select country_name from tspl_country_master where country_code='" + txtcountrycode.Value + "'"))
        '        txtState.Text = ""
        '        txtCity.Text = ""
        '        txtCity.Text = ""
        '        txtstatecode.Value = ""
        '    Else
        '        txtState.Text = ""
        '        txtCity.Text = ""
        '        txtCity.Text = ""
        '        txtstatecode.Value = ""
        '        txtCountry.Text = ""
        '    End If
        'Catch ex As Exception
        '    clsCommon.MyMessageBoxShow(ex.Message)
        'End Try
    End Sub

    Private Sub txtstatecode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtstatecode._MYValidating
        'If clsCommon.myLen(txtcountrycode.Value) <= 0 Then
        '    clsCommon.MyMessageBoxShow("Please Select Country First", Me.Text)
        '    txtcountrycode.Focus()
        '    txtcountrycode.Select()
        '    Errorcontrol.SetError(txtcountrycode, "Please Select Country First")
        '    Return
        'Else
        '    Errorcontrol.ResetError(txtcountrycode)
        'End If

        Try
            Dim qry As String = "select State_code as Code,state_name as State from tspl_state_master"
            txtstatecode.Value = clsCommon.ShowSelectForm("STAFND", qry, "Code", "", txtstatecode.Value, "Code", isButtonClicked)

            If clsCommon.myLen(txtstatecode.Value) > 0 Then
                txtState.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select state_name from tspl_state_master where state_code='" + txtstatecode.Value + "'"))
                txtGSTStateCode.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select GST_State_Code from tspl_state_master where state_code='" + txtstatecode.Value + "'"))
                txtcountrycode.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select country_code from TSPL_state_MASTER where state_code='" + txtstatecode.Value + "' "))
                txtCountry.Text = clsCommon.myCstr(clsCountryMaster.GetName(txtcountrycode.Value, Nothing))
                txtcountrycode.Enabled = False
            Else
                txtState.Text = ""
                txtGSTStateCode.Text = ""
                txtcountrycode.Value = "INDIA"
                txtCountry.Text = "INDIA"
                txtcountrycode.Enabled = True
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try

    End Sub

    Private Sub txtWeb_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtWeb.Leave
        Try
            If clsCommon.myLen(txtWeb.Text) > 0 Then
                If Not txtWeb.Text.ToUpper().Contains("WWW.") Then
                    clsCommon.MyMessageBoxShow("Please Enter Valid Web Site Name", Me.Text)
                    'txtWeb.Focus()
                    'txtWeb.Select()
                    txtWeb.Text = ""
                    Errorcontrol.SetError(txtWeb, "Please Enter Valid Web Site Name")
                    Return
                Else
                    Errorcontrol.ResetError(txtWeb)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow("Please Enter Valid Web Site Name", Me.Text)
            'txtWeb.Focus()
            'txtWeb.Select()
            txtWeb.Text = ""
            Errorcontrol.SetError(txtWeb, "Please Enter Valid Web Site Name")
        End Try
    End Sub

    Private Sub rbtnprop_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtnprop.ToggleStateChanged
        txtprop_name.Enabled = False
        txtpartner_name.Enabled = False
        txtdirectr_name.Enabled = False
        txtprop_name.MendatroryField = False
        txtpartner_name.MendatroryField = False
        txtdirectr_name.MendatroryField = False
        txtprop_name.Text = ""
        txtpartner_name.Text = ""
        txtdirectr_name.Text = ""
        If rbtnprop.IsChecked Then
            txtprop_name.Enabled = True
            txtprop_name.MendatroryField = True
            txtpartner_name.Enabled = False
            txtdirectr_name.Enabled = False
            txtpartner_name.MendatroryField = False
            txtdirectr_name.MendatroryField = False
        End If
    End Sub

    Private Sub rbtnpartnership_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtnpartnership.ToggleStateChanged
        txtprop_name.Enabled = False
        txtpartner_name.Enabled = False
        txtdirectr_name.Enabled = False
        txtprop_name.MendatroryField = False
        txtpartner_name.MendatroryField = False
        txtdirectr_name.MendatroryField = False
        txtprop_name.Text = ""
        txtpartner_name.Text = ""
        txtdirectr_name.Text = ""
        If rbtnpartnership.IsChecked Then
            txtprop_name.Enabled = False
            txtprop_name.MendatroryField = False
            txtpartner_name.Enabled = True
            txtdirectr_name.Enabled = False
            txtpartner_name.MendatroryField = True
            txtdirectr_name.MendatroryField = False
        End If
    End Sub

    Private Sub rbtnpvt_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtnpvt.ToggleStateChanged
        txtprop_name.Enabled = False
        txtpartner_name.Enabled = False
        txtdirectr_name.Enabled = False
        txtprop_name.MendatroryField = False
        txtpartner_name.MendatroryField = False
        txtdirectr_name.MendatroryField = False
        txtprop_name.Text = ""
        txtpartner_name.Text = ""
        txtdirectr_name.Text = ""
        If rbtnpvt.IsChecked Then
            txtprop_name.Enabled = False
            txtprop_name.MendatroryField = False
            txtpartner_name.Enabled = False
            txtdirectr_name.Enabled = True
            txtpartner_name.MendatroryField = False
            txtdirectr_name.MendatroryField = True
        End If
    End Sub

    Private Sub rbtnpublic_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtnpublic.ToggleStateChanged
        txtprop_name.Enabled = False
        txtpartner_name.Enabled = False
        txtdirectr_name.Enabled = False
        txtprop_name.MendatroryField = False
        txtpartner_name.MendatroryField = False
        txtdirectr_name.MendatroryField = False
        txtprop_name.Text = ""
        txtpartner_name.Text = ""
        txtdirectr_name.Text = ""
        If rbtnpublic.IsChecked Then
            txtprop_name.Enabled = False
            txtprop_name.MendatroryField = False
            txtpartner_name.Enabled = False
            txtdirectr_name.Enabled = True
            txtpartner_name.MendatroryField = False
            txtdirectr_name.MendatroryField = True
        End If
    End Sub

    Private Sub cmbsecurity_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbsecurity.SelectedIndexChanged
        If clsCommon.CompairString(cmbsecurity.Text, "NO") = CompairStringResult.Equal Then
            txtnoofinstlmnt.Enabled = True
            txtamtofinstlmnt.Enabled = True
            txtnoofinstlmnt.MendatroryField = True
            txtamtofinstlmnt.MendatroryField = True
        Else
            txtnoofinstlmnt.Text = ""
            txtamtofinstlmnt.Text = ""
            txtnoofinstlmnt.Enabled = False
            txtamtofinstlmnt.Enabled = False
            txtnoofinstlmnt.MendatroryField = False
            txtamtofinstlmnt.MendatroryField = False
        End If
    End Sub

    Private Sub txtbranchcode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtbranchcode._MYValidating
        If clsCommon.myLen(fndbankcode.Value) > 0 Then
            Dim qry As String = "Select Bank_IFSC_Code as IFSCCode,Branch_Name from TSPL_Vendor_Bank_Branch_Details "
            txtbranchcode.Value = clsCommon.ShowSelectForm("FormIFSCCode", qry, "IFSCCode", " Bank_Code ='" & fndbankcode.Value & "' ", txtbranchcode.Value, "", isButtonClicked)
            txtbranchname.Text = clsDBFuncationality.getSingleValue("Select Branch_Name from TSPL_Vendor_Bank_Branch_Details where Bank_Code ='" & fndbankcode.Value & "' and Bank_IFSC_Code='" & txtbranchcode.Value & "' ")
        Else
            clsCommon.MyMessageBoxShow("Please select Bank Code first")
        End If
    End Sub

    Private Sub gvMCC_CellValueChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvMCC.CellValueChanged
        Try
            If Not IsInsieLoadData And e.Column Is gvMCC.Columns("COLMCC_Code") Then
                Dim sQuery As String = "select mm.MCC_Code,MCC_NAME,MCC_Type,Add1,Add2,Pin_code,Email,Telphone from tspl_mcc_master mm " _
                & " left join TSPL_MCC_Transporter_MAPPING tm  on mm.MCC_Code=tm.mcc_code"
                Dim str As String = clsCommon.ShowSelectForm("", sQuery, "MCC_Code", " tm.mcc_code is Null ", "MCC_Code")
                If str <> "" Then
                    IsInsieLoadData = True
                    gvMCC.CurrentRow.Cells("ColMCC_Code").Value = str
                    Dim objMCc As New clsMccMaster
                    objMCc = clsMccMaster.loadData(str, NavigatorType.Current, True, "", "")
                    If Not IsNothing(objMCc) Then
                        gvMCC.CurrentRow.Cells("ColMCC_Name").Value = objMCc.MCC_NAME
                        gvMCC.CurrentRow.Cells("COLMccType").Value = objMCc.MCC_Type
                        gvMCC.CurrentRow.Cells("COL_IsActive").Value = True
                    End If
                    IsInsieLoadData = False
                End If

            End If
        Catch ex As Exception
        End Try
    End Sub



    Private Sub txtvendorname_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtvendorname.TextChanged
        txtpayeename.Text = clsCommon.myCstr(txtvendorname.Text)
    End Sub

    Private Sub cmbagreemnt_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbagreemnt.SelectedValueChanged
        If clsCommon.CompairString(cmbagreemnt.Text, "NO") = CompairStringResult.Equal Then
            dtpStartDate.Enabled = False
            dtpEndDate.Enabled = False
        Else
            dtpStartDate.Enabled = True
            dtpEndDate.Enabled = True
        End If
    End Sub

    Private Sub cmbBlankCheque_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.Data.PositionChangedEventArgs) Handles cmbBlankCheque.SelectedIndexChanged
        If clsCommon.CompairString(cmbBlankCheque.Text, "NO") = CompairStringResult.Equal Then
            gvCheque.Enabled = False
        Else
            gvCheque.Enabled = True
            LoadBlankGvCheque()
        End If
    End Sub

    Private Sub txtstatecode__MYOpenMasterForm(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtstatecode._MYOpenMasterForm
        Frm_Open = New frmStateMaster()
        Frm_Open.SetUserMgmt(clsUserMgtCode.FrmStageMaster)
        Frm_Open.Show()
    End Sub

    Private Sub fndbankcode__MYOpenMasterForm(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndbankcode._MYOpenMasterForm
        Frm_Open = New FrmVendorBankMaster()
        Frm_Open.Show()
    End Sub

    Private Sub fndTrmsCode__MYOpenMasterForm(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndTrmsCode._MYOpenMasterForm

    End Sub

    Private Sub txtpan_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtpan.TextChanged
        Try
            If clsCommon.myLen(txtpan.Text) <= 0 Then
                Exit Sub
            End If
            Dim msg As String = clsERPFuncationality.CheckPanStructure(txtpan.Text.Trim(), txtvendorname.Text)
            txtGST_PanCode.Text = txtpan.Text
            If clsCommon.myLen(msg) > 0 Then
                pageCus.SelectedPage = RadPageViewPage1
                clsCommon.MyMessageBoxShow(msg, Me.Text)
                txtpan.Focus()
                txtpan.Select()
                Errorcontrol.SetError(txtpan, msg)
                Exit Sub
            Else
                Errorcontrol.ResetError(txtpan)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub


    Private Sub gvMCC_UserDeletingRow(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs) Handles gvMCC.UserDeletingRow
        Try
            If clsCommon.MyMessageBoxShow("Do You want to Delete Selected row.", "Delete Data", MessageBoxButtons.YesNo, RadMessageIcon.Question) = Windows.Forms.DialogResult.No Then
                e.Cancel = True
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub gvDeductionRange_CurrentColumnChanged(sender As Object, e As CurrentColumnChangedEventArgs) Handles gvDeductionRange.CurrentColumnChanged
        If gvDeductionRange.RowCount > 0 Then
            Dim intCurrRow As Integer = gvDeductionRange.CurrentRow.Index
            If intCurrRow = gvDeductionRange.Rows.Count - 1 Then
                gvDeductionRange.Rows.AddNew()
                gvDeductionRange.CurrentRow = gvDeductionRange.Rows(intCurrRow)
            End If
        End If
    End Sub

    Private Sub RadMenuItem3_Click(sender As Object, e As EventArgs) Handles RadMenuItem3.Click
        funImport()
    End Sub



    Private Sub RadMenuItem1_Click(sender As Object, e As EventArgs) Handles RadMenuItem1.Click
        funExport()
    End Sub

    Private Sub RadMenuItem2_Click(sender As Object, e As EventArgs) Handles RadMenuItem2.Click
        funExportDeduction()
    End Sub

    Public Sub funExportDeduction()
        Try
            Dim strCmd As String = "select count(*) from TSPL_PTM_DEDCUTION_RANGE "
            Dim check As Integer = clsDBFuncationality.getSingleValue(strCmd)
            Dim whrCls As String = ""
            If check > 0 Then
                strCmd = "select PTM_Code,Minutes,Amount from TSPL_PTM_DEDCUTION_RANGE"
            Else
                strCmd = "select '' as PTM_Code,'' as Minutes,'' as Amount"
            End If
            transportSql.ExporttoExcel(strCmd, whrCls, Me)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Transporter")
        End Try
    End Sub

    Private Sub RadMenuItem5_Click(sender As Object, e As EventArgs) Handles RadMenuItem5.Click
        funImportDeduction()
    End Sub

    Public Sub funImportDeduction()
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim Count As String = ""
        Dim coll As New Dictionary(Of String, List(Of clsPTMDeductionRange))
        If transportSql.importExcel(gv, "PTM_Code", "Minutes", "Amount") Then
            Try
                clsCommon.ProgressBarShow()
                Dim counter As Integer = 1
                For Each grow As GridViewRowInfo In gv.Rows
                    Count = clsCommon.myCstr(grow.Index + 2)
                    If clsCommon.myLen(grow.Cells("PTM_Code").Value) > 0 Then
                        Dim strvendorNo As String = clsCommon.myCstr(grow.Cells("PTM_Code").Value)
                        strvendorNo = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Vendor_Code from TSPL_VENDOR_MASTER  where  vendor_code='" + strvendorNo + "'"))
                        If clsCommon.myLen(strvendorNo) <= 0 Then
                            Throw New Exception("Not a valid Transporter No.,See At Line No. " + clsCommon.myCstr(counter) + "")
                        End If
                        Dim objtr As New clsPTMDeductionRange
                        objtr.PTM_Code = strvendorNo
                        objtr.Minutes = clsCommon.myCdbl(grow.Cells("Minutes").Value)
                        objtr.Amount = clsCommon.myCdbl(grow.Cells("Amount").Value)
                        If Not coll.ContainsKey(strvendorNo) Then
                            coll.Add(strvendorNo, New List(Of clsPTMDeductionRange))
                        End If
                        coll(strvendorNo).Add(objtr)
                    End If
                    counter += 1
                Next
                clsCommon.ProgressBarHide()
            Catch ex As Exception
                clsCommon.ProgressBarHide()
                clsCommon.MyMessageBoxShow("Line: " + Count + " - " + ex.Message)
            End Try

            Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
            Try
                clsCommon.ProgressBarShow()
                If coll IsNot Nothing AndAlso coll.Count > 0 Then
                    For Each key As String In coll.Keys
                        clsPTMDeductionRange.SaveData(key, coll(key), trans)
                    Next
                End If
                clsCommon.ProgressBarHide()
                trans.Commit()
            Catch ex As Exception
                trans.Rollback()
                clsCommon.ProgressBarHide()
                clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
            End Try
            common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
        End If
        Me.Controls.Remove(gv)
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
        'cmbincentive.DataSource = dt
        'cmbincentive.DisplayMember = "Name"
        'cmbincentive.ValueMember = "Code"
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

    Private Sub FndIncentive__MYOpenMasterForm(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles FndIncentive._MYOpenMasterForm
        Frm_Open = New frmIncentiveMaster
        Frm_Open.SetUserMgmt(clsUserMgtCode.frmIncentiveMaster)
        Frm_Open.Show()
    End Sub

    Private Sub FndIncentive__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles FndIncentive._MYValidating
        GetIIncentiveDetails(isButtonClicked)
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
    Private Sub ImportIncentiveDetails()
        Dim gvCharges As New RadGridView()
        Me.Controls.Add(gvCharges)
        Dim countDefaultUOM As Integer = 0
        If transportSql.importExcel(gvCharges, "Transporter", "Incentive Code") Then
            Dim isSaved As Boolean = True
            Dim currentdate As Date = Date.Today
            Dim trans As SqlTransaction = Nothing
            clsCommon.ProgressBarShow()
            Try
                trans = clsDBFuncationality.GetTransactin()
                For i As Integer = 0 To gvCharges.Rows.Count - 1
                    clsDBFuncationality.ExecuteNonQuery("DELETE FROM TSPL_VSP_INCENTIVE where Vendor_Code = '" & clsCommon.myCstr(gvCharges.Rows(i).Cells("Transporter").Value) & "'", trans)
                Next
                For Each grow As GridViewRowInfo In gvCharges.Rows
                    Dim LineNo As String = clsCommon.myCstr(grow.Index) + 2
                    Dim coll As New Hashtable()

                    Dim strVSPCode As String
                    Dim VSPCode As String = clsCommon.myCstr(grow.Cells("Transporter").Value)
                    If clsCommon.myLen(VSPCode) >= 0 Then
                        strVSPCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Vendor_Code from TSPL_VENDOR_MASTER Where Vendor_Code ='" + VSPCode + "' And Form_Type ='VSP'", trans))
                        If clsCommon.myLen(strVSPCode) <= 0 Then
                            Throw New Exception("Transporter '" + VSPCode + "' does not exist .Please make its master first at line no '" + LineNo + "'")
                        End If
                    Else
                        Throw New Exception("Please insert Transporter in at line no '" + LineNo + "' ")
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

    Private Sub radmnuIncentiveExport_Click(sender As Object, e As EventArgs) Handles radmnuIncentiveExport.Click
        Try
            Dim qry As String
            qry = "select count(*) from TSPL_VSP_INCENTIVE"
            If clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry)) > 0 Then
                qry = "select * from (SELECT VENDOR_CODE as [Transporter],INCENTIVE_CODE as [Incentive Code] FROM TSPL_VSP_INCENTIVE where VENDOR_CODE in (select Vendor_Code from TSPL_VENDOR_MASTER where form_type='PTM'))x"
            Else
                qry = "SELECT '' as [Transporter],'' as [Incentive Code] FROM TSPL_VSP_INCENTIVE"
            End If
            ListImpExpColumnsMandatory = New List(Of String)({"Transporter", "Incentive Code"})
            transportSql.ExporttoExcel(qry, "", "", Me, ListImpExpColumnsMandatory)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "UOM")
        End Try
    End Sub

    Private Sub radmnuIncentiveImport_Click(sender As Object, e As EventArgs) Handles radmnuIncentiveImport.Click
        Try
            ImportIncentiveDetails()
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
                clsCommon.MyMessageBoxShow("Select Transporter Code")
                Exit Sub
            End If
            clsERPFuncationality.ShowHistoryData(fndvendorNo.Value, "Vendor_Code", "TSPL_Vendor_MASTER")
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

    Private Sub txtTranspoterCopy__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtTranspoterCopy._MYValidating
        Try
            fndvendorNo.Value = clsVendorMaster.getFinder(" form_type='PTM'", fndvendorNo.Value, isButtonClicked)
            txtvendorname.Text = clsDBFuncationality.getSingleValue("Select group_desc from tspl_vendor_group where ven_group_code='" + fndvendorNo.Value + "'")
            fndvendorNo_text_changed()
            txtTranspoterCopy.Value = fndvendorNo.Value
            fndvendorNo.Value = ""
            txtvendorname.Text = ""
            txtpan.Text = ""
            btnsave.Text = "Save"
            btndelete.Enabled = False
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Public Sub LoadVisiDetail()
        Try
            Dim qryLoadVisi As String = "select  tspl_vsPasset_Head.Doc_No as [Doc Code],Doc_date as [Doc Date],Remarks,Comment,Emp_Name as [Request By]," _
            & " TSPL_VSPAsset_DETAIL.Item_Code as [Item Code], Item_desc as [Item Description],case when coalesce(Auto_Sr_No,'')='' then (case when isnull(tspl_vsPasset_Head.Doc_Type,'')='Issue' then TSPL_VSPAsset_DETAIL.Issued_Qty else TSPL_VSPAsset_DETAIL.Issued_Qty_againstret*-1 end) else 1 " _
            & " end as [Issued Qty],Auto_Sr_No as [Serial No],Unit_code as [Unit],TSPL_VSPAsset_DETAIL.Unit_Cost as [Cost],case when coalesce(Auto_Sr_No,'')='' " _
            & " then Amount  else TSPL_VSPAsset_DETAIL.Unit_Cost end as Amount from tspl_vsPasset_Head inner join TSPL_VSPAsset_DETAIL on " _
            & " tspl_vsPasset_Head.Doc_No=TSPL_VSPAsset_DETAIL.Doc_No  left join TSPL_EMPLOYEE_MASTER on EMP_CODE=Request_By Left join TSPL_SERIAL_ITEM on " _
            & " tspl_vsPasset_Head.Doc_No=TSPL_SERIAL_ITEM.Document_Code and TSPL_SERIAL_ITEM.Item_Code=TSPL_VSPAsset_DETAIL.Item_Code and Document_Type='VSPTRAN' where tspl_vsPasset_Head.Status='1' and " _
            & " Issue_To='" & fndvendorNo.Value & "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qryLoadVisi)
            GvAsset.DataSource = Nothing
            If dt IsNot Nothing And dt.Rows.Count > 0 Then
                GvAsset.DataSource = dt
                GvAsset.BestFitColumns()
                GvAsset.GroupDescriptors.Clear()
                GvAsset.ShowGroupPanel = False
                GvAsset.MasterTemplate.SummaryRowsBottom.Clear()
                For ii As Integer = 0 To GvAsset.Columns.Count - 1
                    GvAsset.Columns(ii).ReadOnly = True
                    GvAsset.Columns(ii).BestFit()
                Next
                GvAsset.EnableFiltering = True
                GvAsset.ShowFilteringRow = True
                GvAsset.AllowAddNewRow = False
                Dim summaryRowItem As New GridViewSummaryRowItem()
                Dim item1 As New GridViewSummaryItem("Issued Qty", "", GridAggregateFunction.Sum)
                summaryRowItem.Add(item1)
                GvAsset.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.ToString)
        End Try
    End Sub
End Class
