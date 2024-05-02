'---preeti Gupta---Ticket No.-BM00000003015--01/07/2014
'' updation by richa agarwal against ticket no. BM00000006631,BM00000006641
Imports System.Data.SqlClient
Imports System.IO
Imports common
Imports System
Imports XpertERPEngine
Imports Telerik.WinControls.UI
Imports Telerik.WinControls
Imports Telerik.WinControls.Data


Public Class FrmAPInvoiceEntryTDS
    Inherits FrmMainTranScreen

#Region "Variables"
    Const ReportID As String = "APInvoiceGridTDS"
    Public strAPInvoice As String = Nothing
    Private isCellValueChangedOpen As Boolean = False
    Private isCellValueChangedTaxOpen As Boolean = False
    Private objRemittance As clsRemittance
    Dim iStxtTaxGroup_TxtChangedComplete As Boolean = True
    Private isNewEntry As Boolean = False
    Private isInsideLoadData As Boolean = False
    Const colLineNo As String = "LNO"
    Const colDedCode As String = "colDedCode"
    Const colDedName As String = "colDedName"
    Const colDedSection As String = "colDedSection"
    Const colACCode As String = "NAME"
    Const colACName As String = "QTY"
    Const colAmt As String = "AMT"
    Const colDocRefAmount As String = "colDocRefAmount"
    Const colTDSper As String = "colTDSper"
    Const colDisPer As String = "DISPER"
    Const colDisAmt As String = "DISAMT"
    Const colAmtAfterDis As String = "AMTAFTERDIS"
    Const colLandedAmt As String = "LANDEDAMT"
    Const colTax1 As String = "COLTAX1"
    Const colTaxBaseAmt1 As String = "COLTAXBASEAMT1"
    Const colTaxRate1 As String = "COLTAXRATE1"
    Const colTaxAmt1 As String = "COLTAXAMT1"
    Const colIsTaxable1 As String = "ISTAXABLE1"
    Const colIsSurTax1 As String = "ISSURTAX1"
    Const colSurTaxCode1 As String = "SURTAXCODE1"
    Const colIsExcisable1 As String = "ISEXCISABLE1"

    Const colTax2 As String = "COLTAX2"
    Const colTaxBaseAmt2 As String = "COLTAXBASEAMT2"
    Const colTaxRate2 As String = "COLTAXRATE2"
    Const colTaxAmt2 As String = "COLTAXAMT2"
    Const colIsTaxable2 As String = "ISTAXABLE2"
    Const colIsSurTax2 As String = "ISSURTAX2"
    Const colSurTaxCode2 As String = "SURTAXCODE2"
    Const colIsExcisable2 As String = "ISEXCISABLE2"

    Const colTax3 As String = "COLTAX3"
    Const colTaxBaseAmt3 As String = "COLTAXBASEAMT3"
    Const colTaxRate3 As String = "COLTAXRATE3"
    Const colTaxAmt3 As String = "COLTAXAMT3"
    Const colIsTaxable3 As String = "ISTAXABLE3"
    Const colIsSurTax3 As String = "ISSURTAX3"
    Const colSurTaxCode3 As String = "SURTAXCODE3"
    Const colIsExcisable3 As String = "ISEXCISABLE3"

    Const colTax4 As String = "COLTAX4"
    Const colTaxBaseAmt4 As String = "COLTAXBASEAMT4"
    Const colTaxRate4 As String = "COLTAXRATE4"
    Const colTaxAmt4 As String = "COLTAXAMT4"
    Const colIsTaxable4 As String = "ISTAXABLE4"
    Const colIsSurTax4 As String = "ISSURTAX4"
    Const colSurTaxCode4 As String = "SURTAXCODE4"
    Const colIsExcisable4 As String = "ISEXCISABLE4"

    Const colTax5 As String = "COLTAX5"
    Const colTaxBaseAmt5 As String = "COLTAXBASEAMT5"
    Const colTaxRate5 As String = "COLTAXRATE5"
    Const colTaxAmt5 As String = "COLTAXAMT5"
    Const colIsTaxable5 As String = "ISTAXABLE5"
    Const colIsSurTax5 As String = "ISSURTAX5"
    Const colSurTaxCode5 As String = "SURTAXCODE5"
    Const colIsExcisable5 As String = "ISEXCISABLE5"

    Const colTax6 As String = "COLTAX6"
    Const colTaxBaseAmt6 As String = "COLTAXBASEAMT6"
    Const colTaxRate6 As String = "COLTAXRATE6"
    Const colTaxAmt6 As String = "COLTAXAMT6"
    Const colIsTaxable6 As String = "ISTAXABLE6"
    Const colIsSurTax6 As String = "ISSURTAX6"
    Const colSurTaxCode6 As String = "SURTAXCODE6"
    Const colIsExcisable6 As String = "ISEXCISABLE6"

    Const colTax7 As String = "COLTAX7"
    Const colTaxBaseAmt7 As String = "COLTAXBASEAMT7"
    Const colTaxRate7 As String = "COLTAXRATE7"
    Const colTaxAmt7 As String = "COLTAXAMT7"
    Const colIsTaxable7 As String = "ISTAXABLE7"
    Const colIsSurTax7 As String = "ISSURTAX7"
    Const colSurTaxCode7 As String = "SURTAXCODE7"
    Const colIsExcisable7 As String = "ISEXCISABLE7"

    Const colTax8 As String = "COLTAX8"
    Const colTaxBaseAmt8 As String = "COLTAXBASEAMT8"
    Const colTaxRate8 As String = "COLTAXRATE8"
    Const colTaxAmt8 As String = "COLTAXAMT8"
    Const colIsTaxable8 As String = "ISTAXABLE8"
    Const colIsSurTax8 As String = "ISSURTAX8"
    Const colSurTaxCode8 As String = "SURTAXCODE8"
    Const colIsExcisable8 As String = "ISEXCISABLE8"

    Const colTax9 As String = "COLTAX9"
    Const colTaxBaseAmt9 As String = "COLTAXBASEAMT9"
    Const colTaxRate9 As String = "COLTAXRATE9"
    Const colTaxAmt9 As String = "COLTAXAMT9"
    Const colIsTaxable9 As String = "ISTAXABLE9"
    Const colIsSurTax9 As String = "ISSURTAX9"
    Const colSurTaxCode9 As String = "SURTAXCODE9"
    Const colIsExcisable9 As String = "ISEXCISABLE9"

    Const colTax10 As String = "COLTAX10"
    Const colTaxBaseAmt10 As String = "COLTAXBASEAMT10"
    Const colTaxRate10 As String = "COLTAXRATE10"
    Const colTaxAmt10 As String = "COLTAXAMT10"
    Const colIsTaxable10 As String = "ISTAXABLE10"
    Const colIsSurTax10 As String = "ISSURTAX10"
    Const colSurTaxCode10 As String = "SURTAXCODE10"
    Const colIsExcisable10 As String = "ISEXCISABLE10"

    Const colTotTaxAmt As String = "TAXAMT"
    Const colAmtAfterTax As String = "AMTAFTERTAX"
    Const colIsUnclaimedTax As String = "ISUNCLAIMEDTAX"
    Const colDocType As String = "DOCTYPE"
    Const colDocNo As String = "DOCNO"
    Const colRemarks As String = "REMARKS"

    Const colTTaxAutCode As String = "TAXAUTCODE"
    Const colTTaxAutName As String = "TAXAUTNAME"
    'Const colTIsTaxable As String = "ISTAXABLE"
    Const colTTaxRate As String = "TAXRATE"
    'Const colTIsSurTax As String = "ISSURTAX"
    'Const colTSurTaxCode As String = "SURTAXCODE"
    Const colTBaseAmt As String = "TAXBASEAMT"
    Const colTTaxAmt As String = "TAXAMT"

    Const colAChgCode As String = "COLACCODE"
    Const colAChgName As String = "COLACNAME"
    Const colRefDocNo As String = "colRefDocNo"
    Const colAChgAmount As String = "COLACAMOUNT"

    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim repoAdChagCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
    Dim repoAdChagName As GridViewTextBoxColumn = New GridViewTextBoxColumn()

    Public Const DocTypeSaleInvoice As String = "Sale Invoice"
    Public Const DocTypeSalesReturn As String = "Sale Return"
    Public Const DocTypeLO As String = "Loadout"
    Public Const DocTypeLI As String = "Loadin"
    Public Const DocTypeSRN As String = "SRN"
    Public Const DocTypeAdjustment As String = "Adjustment"
    Public Const DocTypeTransfer As String = "Transfer"
    Private SettingAutoRoundOffSeprateAccountOnVendorTransaction As Boolean = False

#End Region

    Public Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.mbtnAPInvoiceEntryTDS)
        '--preeti gupta--ticket no-[BM00000003179]
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnPost.Visible = MyBase.isPostFlag
        btnDelete.Visible = MyBase.isDeleteFlag
        btnPrint.Visible = MyBase.isPrintFlag
        'If MyBase.isReverse Then
        '    btnReverse.Enabled = True
        'Else
        '    btnReverse.Enabled = False

        'End If
        btnReverse.Visible = False
    End Sub

    Private Sub FrmAPInvoiceEntry_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        SettingAutoRoundOffSeprateAccountOnVendorTransaction = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AutoRoundOffSeprateAccountOnVendorTransaction, clsFixedParameterCode.AutoRoundOffSeprateAccountOnVendorTransaction, Nothing)) = 1)
        Setlength()
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction")
        ButtonToolTip.SetToolTip(btnPost, "Press Alt+P Post Trasnaction")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnAddNew, "Press Alt+N Adding New Trasnaction")
        ButtonToolTip.SetToolTip(btnPrint, "Press Ctrl+P for Print ")
        ButtonToolTip.SetToolTip(btnViewTDSDetails, "Press Alt+V View TDS Details")

        RadPageView1.SelectedPage = RadPageViewPage1
        RadPageViewPage2.Enabled = False
        RadPageViewPage3.Enabled = False

        LoadBlankGridGL()
        LoadBlankGridTax()
        LoadRefDocumenType()
        LoadInvoiceType()
        AddNew()

        If clsCommon.myLen(strAPInvoice) > 0 Then
            LoadData(strAPInvoice)
        End If
        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag))
        End If
        btnPrint.Visible = False
    End Sub

    Public Sub Setlength()
        txtDesc.MaxLength = 250
        txtOrderNo.MaxLength = 30
        txtPONo.MaxLength = 30
        txtVendorInvoiceNo.MaxLength = 30
    End Sub

    Private Sub txtChangeVendorNo()
        If Not isInsideLoadData Then
            Dim qry As String = "select  Vendor_Code,Vendor_Name,Terms_Code,Terms_Code_Desc ,Vendor_Account ,Tax_Group,Tax_Group_Desc from TSPL_VENDOR_MASTER where Vendor_Code ='" + clsCommon.myCstr(TxtVendorNo.Value) + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                lblVendorName.Text = clsCommon.myCstr(dt.Rows(0)("Vendor_Name"))
                TxtVendorNo.Value = clsCommon.myCstr(dt.Rows(0)("Vendor_Code"))
                txtTermCode.Value = clsCommon.myCstr(dt.Rows(0)("Terms_Code"))
                lblTermName.Text = clsCommon.myCstr(dt.Rows(0)("Terms_Code_Desc"))
                txtACSet.Value = clsCommon.myCstr(dt.Rows(0)("Vendor_Account"))
                txtTaxGroup.Value = clsCommon.myCstr(dt.Rows(0)("Tax_Group"))
                lblTaxGrpName.Text = clsCommon.myCstr(dt.Rows(0)("Tax_Group_Desc"))
            Else
                TxtVendorNo.Value = ""
                lblVendorName.Text = ""
                txtTermCode.Value = ""
                lblTermName.Text = ""
                txtACSet.Value = ""
                txtTaxGroup.Value = ""
                lblTaxGrpName.Text = ""
            End If

            SetVendorTDSDetails()

            Dim objVendor As clsTDSVendorDetails = clsTDSVendorDetails.GetData(TxtVendorNo.Value)
            If objVendor IsNot Nothing Then
                qry = "select 1 from TSPL_PAYMENT_HEADER where Payment_Type='AV' and (TSPL_PAYMENT_HEADER.Posted='1' or TSPL_PAYMENT_HEADER.Posted='P') and  Vendor_Code='" + TxtVendorNo.Value + "' and isnull(Payment_Amount,0)-isnull(Total_Applied_Amount,0)>0"
                dt = clsDBFuncationality.GetDataTable(qry)
                If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                    ' If (common.clsCommon.MyMessageBoxShow("Advance TDS payment found.Do you want to Apply TDS on this Document", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No) Then
                    btnViewTDSDetails.Enabled = False
                    objRemittance = New clsRemittance()
                    objRemittance.IsApplyTDS = False
                    objRemittance.Deduction_Code = ""
                    objRemittance.TDS_Per = 0
                    objRemittance.Surcharge_Per = 0
                    objRemittance.Edu_Cess_Per = 0
                    objRemittance.Sec_Educess_Per = 0
                    UpdateTDSAmount()
                    'Else
                    '    btnViewTDSDetails.Enabled = True
                    'End If
                End If
                If True Then
                End If
            End If
        End If
    End Sub

    Sub SetVendorTDSDetails()
        btnViewTDSDetails.Enabled = False
        objRemittance = Nothing
        Dim objVendor As clsTDSVendorDetails = clsTDSVendorDetails.GetData(TxtVendorNo.Value)
        If objVendor IsNot Nothing Then
            btnViewTDSDetails.Enabled = True
            Dim objDedDetails As clsTDSDeductionDetails = clsTDSDeductionDetails.GetApplicableTDRate(objVendor.Nature_Of_Deduction, clsCommon.myCdbl(lblTotRAmt.Text), Nothing, False, TxtVendorNo.Value)
            If (objDedDetails IsNot Nothing) Then
                objRemittance = New clsRemittance()
                objRemittance.Branch_Code = objVendor.Branch_Code
                objRemittance.Deduction_Code = objVendor.Nature_Of_Deduction
                objRemittance.TDS_Per = objDedDetails.TDS
                objRemittance.Surcharge_Per = objDedDetails.Surcharge
                objRemittance.Edu_Cess_Per = objDedDetails.Educess
                objRemittance.Sec_Educess_Per = objDedDetails.Seceducess
                objRemittance.IsTDSOverride = False
                If isNewEntry Then
                    objRemittance.IsApplyTDS = True
                Else
                    objRemittance.IsApplyTDS = clsPIRemittance.IsTDSApplied(txtDocNo.Value)
                End If
                objRemittance.Section_Code = objVendor.TDSSection
                objRemittance.Section_Description = objVendor.TDSSectionDescription
                objRemittance.Select_By = objVendor.VendorTypeCode

                Dim qry As String = "select Year_Name from TSPL_TDS_FINANCIAL_YEAR where convert(date,'" + txtDate.Value + "',103)>=  convert(date,From_Date,103)  and convert(date,'" + txtDate.Value + "',103)<=convert(date,To_Date,103) "
                objRemittance.Fiscal_Year = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
                objRemittance.Quarter = "First"

            End If
        End If
    End Sub

    Sub UpdateTDSAmount()
        If (objRemittance Is Nothing) Then
            SetVendorTDSDetails()
        Else
            Dim objDedDetails As clsTDSDeductionDetails = clsTDSDeductionDetails.GetApplicableTDRate(objRemittance.Deduction_Code, clsCommon.myCdbl(lblTotRAmt.Text), Nothing, False, TxtVendorNo.Value)
            If (objDedDetails IsNot Nothing AndAlso objRemittance.IsApplyTDS) Then
                objRemittance.TDS_Per = objDedDetails.TDS
                objRemittance.Surcharge_Per = objDedDetails.Surcharge
                objRemittance.Edu_Cess_Per = objDedDetails.Educess
                objRemittance.Sec_Educess_Per = objDedDetails.Seceducess
            End If
        End If
        If (objRemittance IsNot Nothing) Then
            objRemittance.Vendor_Code = TxtVendorNo.Value
            objRemittance.Vendor_Name = lblVendorName.Text
            objRemittance.Document_Date = txtDate.Value
            objRemittance.Document_Type = clsCommon.myCstr(cboDocType.SelectedValue)
            objRemittance.Document_Amount = clsCommon.myCdbl(lblTotRAmt.Text)
            objRemittance.Calculated_TDS_Base = clsCommon.myCdbl(lblTotRAmt.Text)
            If Not objRemittance.IsTDSOverride Then
                objRemittance.Actual_TDS_Base = clsCommon.myCdbl(lblTotRAmt.Text)
            End If

            objRemittance.Calculated_TDS = (objRemittance.Calculated_TDS_Base * objRemittance.TDS_Per) / 100
            objRemittance.Actual_TDS = (objRemittance.Actual_TDS_Base * objRemittance.TDS_Per) / 100

            objRemittance.Calculated_Surcharge = (objRemittance.Calculated_TDS_Base * objRemittance.Surcharge_Per) / 100
            objRemittance.Actual_Surcharge = (objRemittance.Actual_TDS_Base * objRemittance.Surcharge_Per) / 100

            objRemittance.Calculated_Edu_Cess = (objRemittance.Calculated_TDS_Base * objRemittance.Edu_Cess_Per) / 100
            objRemittance.Actual_Edu_Cess = (objRemittance.Actual_TDS_Base * objRemittance.Edu_Cess_Per) / 100

            objRemittance.Calculated_Sec_Educess = (objRemittance.Calculated_TDS_Base * objRemittance.Sec_Educess_Per) / 100
            objRemittance.Actual_Sec_Educess = (objRemittance.Actual_TDS_Base * objRemittance.Sec_Educess_Per) / 100

            objRemittance.Calculated_Total_TDS = objRemittance.Calculated_TDS + objRemittance.Calculated_Surcharge + objRemittance.Calculated_Edu_Cess + objRemittance.Calculated_Sec_Educess
            objRemittance.Actual_Total_TDS = objRemittance.Actual_TDS + objRemittance.Actual_Surcharge + objRemittance.Actual_Edu_Cess + objRemittance.Actual_Sec_Educess
        End If
    End Sub

    Private Sub txtTaxGroup_TxtChanged()
        If Not isInsideLoadData AndAlso iStxtTaxGroup_TxtChangedComplete Then
            iStxtTaxGroup_TxtChangedComplete = False
            LoadBlankGridTax()
            Dim qry As String = "select TSPL_TAX_GROUP_DETAILS.Tax_Group_Code ,TSPL_TAX_GROUP_MASTER.Tax_Group_Desc,Tax_Code,Tax_Code_Desc,Surtax,Surtax_Tax_Code,(select Tax_Rate from TSPL_TAX_RATES WHERE Tax_Rate_Code=1 AND Tax_Code=TSPL_TAX_GROUP_DETAILS.Tax_Code and TSPL_TAX_RATES.Tax_Type='P') AS TaxRate,Taxable from TSPL_TAX_GROUP_DETAILS left outer join TSPL_TAX_GROUP_MASTER on TSPL_TAX_GROUP_MASTER.Tax_Group_Code=TSPL_TAX_GROUP_DETAILS.Tax_Group_Code where TSPL_TAX_GROUP_DETAILS.Tax_Group_Code='" + txtTaxGroup.Value + "' and TSPL_TAX_GROUP_MASTER.Tax_Group_Type='P' and TSPL_TAX_GROUP_DETAILS.Tax_Group_Type='P' order by Trans_Code"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                If (dt.Rows.Count > 10) Then
                    MessageBox.Show("Can't Handle More than 10 Tax Types in a Group")
                    Return
                End If
                Dim ii As Integer = 0
                txtTaxGroup.Value = clsCommon.myCstr(dt.Rows(0)("Tax_Group_Code"))
                lblTaxGrpName.Text = clsCommon.myCstr(dt.Rows(0)("Tax_Group_Desc"))
                For Each dr As DataRow In dt.Rows
                    gv2.Rows.AddNew()
                    gv2.Rows(ii).Cells(colTTaxAutCode).Value = clsCommon.myCstr(dr("Tax_Code"))
                    gv2.Rows(ii).Cells(colTTaxAutName).Value = clsCommon.myCstr(dr("Tax_Code_Desc"))
                    If rbtnTaxCalAutomatic.IsChecked Then
                        gv2.Rows(ii).Cells(colTTaxRate).Value = clsCommon.myCdbl(dr("TaxRate"))
                        'gv2.Rows(ii).Cells(colTIsSurTax).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Surtax")), "Y") = CompairStringResult.Equal)
                        'gv2.Rows(ii).Cells(colTIsTaxable).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Taxable")), "Y") = CompairStringResult.Equal)
                        'gv2.Rows(ii).Cells(colTSurTaxCode).Value = clsCommon.myCstr(dr("Surtax_Tax_Code"))
                    ElseIf rbtnTaxCalManual.IsChecked Then
                        gv2.Rows(ii).Cells(colTTaxRate).Value = Nothing
                        'gv2.Rows(ii).Cells(colTIsSurTax).Value = Nothing
                        'gv2.Rows(ii).Cells(colTIsTaxable).Value = Nothing
                        'gv2.Rows(ii).Cells(colTSurTaxCode).Value = Nothing
                    End If
                    ii = ii + 1
                Next
                SetitemWiseTaxSetting(True, False)
            Else
                lblTaxGrpName.Text = ""
            End If

            For ii As Integer = 0 To gv1.Rows.Count - 1
                UpdateCurrentRowWihtRowNo(ii)
            Next
            UpdateAllTotals()
            iStxtTaxGroup_TxtChangedComplete = True
        End If
    End Sub

    Private Sub txtTermCode_TxtChanged()

        If Not isInsideLoadData Then
            Dim qry As String = "select Terms_Code,Terms_Desc,No_Days from TSPL_TERMS_MASTER where Terms_Code='" + clsCommon.myCstr(txtTermCode.Value) + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                'txtTermCode.SelectedValue = clsCommon.myCstr(dt.Rows(0)("Terms_Code"))
                txtTermCode.Value = clsCommon.myCstr(dt.Rows(0)("Terms_Code"))
                lblTermName.Text = clsCommon.myCstr(dt.Rows(0)("Terms_Desc"))
                txtDueDate.Value = txtDate.Value.AddDays(clsCommon.myCdbl(dt.Rows(0)("No_Days")))
            Else
                lblTermName.Text = ""
            End If
        End If
    End Sub

    Sub LoadInvoiceType()
        Dim dt As New DataTable
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))
        Dim dr As DataRow = dt.NewRow()
        'dr("Code") = "I"
        'dr("Name") = "Invoice"
        'dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "D"
        dr("Name") = "Debit Note"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "C"
        dr("Name") = "Credit Note"
        dt.Rows.Add(dr)

        cboDocType.DataSource = dt
        cboDocType.ValueMember = "Code"
        cboDocType.DisplayMember = "Name"

    End Sub

    '''' added by priti to add RefDoctype dropdown

    Sub LoadRefDocumenType()
        Dim dt As New DataTable
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))
        Dim dr As DataRow = dt.NewRow()
        dr("Code") = ""
        dr("Name") = "Select"
        dt.Rows.Add(dr)

        '' Removed SRN No. and Work Order options as refrence 
        'dr = dt.NewRow()
        'dr("Code") = "S"
        'dr("Name") = "SRN No"
        'dt.Rows.Add(dr)

        ''dr = dt.NewRow()
        ''dr("Code") = "LO"
        ''dr("Name") = "Loadout"
        ''dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "AP"
        dr("Name") = "AP Invoice"
        dt.Rows.Add(dr)

        'dr = dt.NewRow()
        'dr("Code") = "WO"
        'dr("Name") = "Work Order"
        'dt.Rows.Add(dr)

        ''richa agarwal 
        dr = dt.NewRow()
        dr("Code") = "A"
        dr("Name") = "Advance"
        dt.Rows.Add(dr)
        ''---------------------

        cmbRefType.DataSource = dt
        cmbRefType.ValueMember = "Code"
        cmbRefType.DisplayMember = "Name"

    End Sub

    '''' ends here

    Sub BlankAllControls()
        UsLock1.Status = ERPTransactionStatus.Pending
        txtDocNo.Value = ""
        txtDesc.Text = ""
        chkOnHold.Checked = False
        TxtVendorNo.Value = ""
        lblVendorName.Text = ""
        txtACSet.Value = ""
        txtDate.Value = clsCommon.GETSERVERDATE()
        cboDocType.SelectedIndex = 0
        txtPONo.Text = ""
        txtOrderNo.Text = ""
        txtTaxGroup.Value = ""
        lblTaxGrpName.Text = ""
        txtTermCode.Value = ""
        lblTermName.Text = ""
        txtDueDate.Value = txtDate.Value
        lblAmtWithDiscount.Text = ""
        lblDiscountAmt.Text = ""
        lblAmtAfterDiscount.Text = ""
        lblTaxAmt.Text = ""
        lblRoundOff.Text = ""
        lblTotRAmt.Text = ""
        lblTotRAmt1.Text = ""
        txtVendorInvoiceNo.Text = ""
        txtVendorInvDatre.Value = txtDate.Value
        cmbRefType.SelectedIndex = 0
        txtRefDocNo.Value = ""
        rbtnTaxCalAutomatic.IsChecked = True
        lblTotEmptyAmt.Text = ""
    End Sub

    Private Sub TxtVendorNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles TxtVendorNo._MYValidating
        Try
            If txtlocation.Value = "" Then
                If clsCommon.myLen(TxtVendorNo.Value) > 0 Then
                    common.clsCommon.MyMessageBoxShow(Me, "Please first select Location", Me.Text)
                    TxtVendorNo.Value = ""
                    txtlocation.Focus()
                    Exit Sub
                End If
            End If
            Dim Qry As String = clsERPFuncationality.glvendorqueryNew
            Dim whrclas As String = " exists(select 1 from TSPL_TDS_VENDOR_DETAILS where TSPL_TDS_VENDOR_DETAILS.Vendor_Code=m.Vendor_Code ) AND  m.Status='N'"
            TxtVendorNo.Value = clsCommon.ShowSelectForm("VendSelectfnd", Qry, "Code", whrclas, TxtVendorNo.Value, "Code", isButtonClicked)
            txtChangeVendorNo()
            txtTermCode_TxtChanged()
            FillVendorDetails()
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub TxtVendorNo_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TxtVendorNo.KeyPress
        If (e.KeyChar = Chr(39)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtACSet__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtACSet._MYValidating
        Dim Qry As String = "select Acct_Set_Code as Code ,Acct_Set_Desc as Description from TSPL_VENDOR_ACCOUNT_SET"
        txtACSet.Value = clsCommon.ShowSelectForm("AccFiltrFND", Qry, "Code", "", txtACSet.Value, "Code", isButtonClicked)

    End Sub

    Private Sub txtACSet_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtACSet.KeyPress
        If (e.KeyChar = Chr(39)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtTaxGroup__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtTaxGroup._MYValidating
        Dim Qry As String = "select Tax_Group_Code as Code,Tax_Group_Desc as Description from TSPL_TAX_GROUP_MASTER "
        txtTaxGroup.Value = clsCommon.ShowSelectForm("TaxGrpSFND", Qry, "Code", " Tax_Group_Type='P'", txtTaxGroup.Value, "Code", isButtonClicked)
        txtTaxGroup_TxtChanged()
    End Sub

    Private Sub txtTaxGroup_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtTaxGroup.KeyPress
        If (e.KeyChar = Chr(39)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtTermCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtTermCode._MYValidating
        Dim Qry As String = "select Terms_Code as Code,Terms_Desc as Description,No_Days as [No Of Days] from TSPL_TERMS_MASTER"
        txtTermCode.Value = clsCommon.ShowSelectForm("TermCodeFNDD", Qry, "Code", "", txtTermCode.Value, "Code", isButtonClicked)
        txtTermCode_TxtChanged()
    End Sub

    Private Sub txtTermCode_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtTermCode.KeyPress
        If (e.KeyChar = Chr(39)) Then
            e.Handled = True
        End If
    End Sub

    Private Function GetDocType() As DataTable
        Dim dt As New DataTable()
        dt.Columns.Add("Code", GetType(String))

        Dim dr As DataRow = dt.NewRow()
        dr("Code") = ""
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = DocTypeSaleInvoice
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = DocTypeSalesReturn
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = DocTypeLO
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = DocTypeLI
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = DocTypeSRN
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = DocTypeAdjustment
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = DocTypeTransfer
        dt.Rows.Add(dr)

        Return dt
    End Function

    Sub LoadBlankGridGL()
        gv1.Rows.Clear()
        gv1.Columns.Clear()

        Dim repoLineNo As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoLineNo = New GridViewDecimalColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "Line No"
        repoLineNo.Name = colLineNo
        repoLineNo.Width = 50
        repoLineNo.ReadOnly = True
        repoLineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoLineNo)


        repoAdChagCode = New GridViewTextBoxColumn()
        repoAdChagCode.FormatString = ""
        repoAdChagCode.HeaderText = "Additional Charges"
        repoAdChagCode.Name = colAChgCode
        repoAdChagCode.HeaderImage = My.Resources.Resources.search4
        repoAdChagCode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoAdChagCode.Width = 150
        repoAdChagCode.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoAdChagCode)

        repoAdChagName = New GridViewTextBoxColumn()
        repoAdChagName.FormatString = ""
        repoAdChagName.HeaderText = "Additional Charges Description"
        repoAdChagName.Name = colAChgName
        repoAdChagName.Width = 150
        repoAdChagName.ReadOnly = True
        repoAdChagName.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoAdChagName)

        ''richa agarwal 
        Dim repoRefDocCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoRefDocCode.FormatString = ""
        repoRefDocCode.HeaderText = "Reference Document No"
        repoRefDocCode.Name = colRefDocNo
        repoRefDocCode.Width = 150
        repoRefDocCode.ReadOnly = False
        repoRefDocCode.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoRefDocCode)
        ''------------------

        Dim repoDedCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoDedCode.FormatString = ""
        repoDedCode.HeaderText = "Deduction Code"
        repoDedCode.Name = colDedCode
        repoDedCode.HeaderImage = My.Resources.Resources.search4
        repoDedCode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoDedCode.Width = 150
        gv1.MasterTemplate.Columns.Add(repoDedCode)

        Dim repoDedName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoDedName.FormatString = ""
        repoDedName.HeaderText = "Deduction"
        repoDedName.Name = colDedName
        repoDedName.Width = 100
        repoDedName.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoDedName)

        Dim repoDedSection As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoDedSection.FormatString = ""
        repoDedSection.HeaderText = "Section"
        repoDedSection.Name = colDedSection
        repoDedSection.Width = 100
        repoDedSection.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoDedSection)

        Dim repoAcCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoAcCode.FormatString = ""
        repoAcCode.HeaderText = "GL Account"
        repoAcCode.Name = colACCode
        'repoAcCode.HeaderImage = Global.ERP.My.Resources.Resources.search4
        'repoAcCode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoAcCode.Width = 150
        repoAcCode.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoAcCode)

        Dim repoACName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoACName.FormatString = ""
        repoACName.HeaderText = "Account Description"
        repoACName.Name = colACName
        repoACName.Width = 150
        repoACName.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoACName)


        ''richa agarwal 
        Dim repoRefDocAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoRefDocAmt = New GridViewDecimalColumn()
        repoRefDocAmt.FormatString = ""
        repoRefDocAmt.HeaderText = "Doc./Taxable Amount"
        repoRefDocAmt.Name = colDocRefAmount
        repoRefDocAmt.Width = 80
        repoRefDocAmt.IsVisible = False
        repoRefDocAmt.ReadOnly = True
        repoRefDocAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoRefDocAmt)

        Dim repoTaxPer As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxPer = New GridViewDecimalColumn()
        repoTaxPer.FormatString = ""
        repoTaxPer.HeaderText = "TDS %"
        repoTaxPer.Name = colTDSper
        repoTaxPer.Width = 80
        repoTaxPer.IsVisible = False
        repoTaxPer.ReadOnly = True
        repoTaxPer.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxPer)
        ''------------------

        Dim repoAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAmt = New GridViewDecimalColumn()
        repoAmt.FormatString = ""
        repoAmt.HeaderText = "Amount"
        repoAmt.Name = colAmt
        repoAmt.Width = 80
        repoAmt.ReadOnly = False
        'repoAmt.Minimum = 0
        repoAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoAmt)

        Dim repoDisPer As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoDisPer = New GridViewDecimalColumn()
        repoDisPer.FormatString = ""
        repoDisPer.HeaderText = "Discount %"
        repoDisPer.Minimum = 0
        repoDisPer.Maximum = 100
        repoDisPer.Name = colDisPer
        repoDisPer.Width = 100
        repoDisPer.IsVisible = False
        repoDisPer.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoDisPer)

        Dim repoDisAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoDisAmt = New GridViewDecimalColumn()
        repoDisAmt.FormatString = ""
        repoDisAmt.HeaderText = "Discount Amount"
        repoDisAmt.Name = colDisAmt
        repoDisAmt.Width = 100
        repoDisAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        ' repoDisAmt.VisibleInColumnChooser = False
        repoDisAmt.ReadOnly = True
        repoDisAmt.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoDisAmt)

        Dim repoAmtAfterDis As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAmtAfterDis = New GridViewDecimalColumn()
        repoAmtAfterDis.FormatString = ""
        repoAmtAfterDis.HeaderText = "Amount After Discount"
        repoAmtAfterDis.Name = colAmtAfterDis
        repoAmtAfterDis.Width = 100
        repoAmtAfterDis.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        ' repoAmtAfterDis.VisibleInColumnChooser = False
        repoAmtAfterDis.ReadOnly = True
        repoAmtAfterDis.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoAmtAfterDis)


        Dim repoLandedamt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoLandedamt.FormatString = ""
        repoLandedamt.HeaderText = "Landed Amount"
        repoLandedamt.Name = colLandedAmt
        repoLandedamt.Width = 100
        repoLandedamt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        ' repoLandedamt.VisibleInColumnChooser = False
        repoLandedamt.IsVisible = False
        repoLandedamt.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoLandedamt)

        Dim repoTax1 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTax1.FormatString = ""
        repoTax1.HeaderText = "Tax 1"
        repoTax1.Name = colTax1
        repoTax1.ReadOnly = True
        repoTax1.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoTax1)

        Dim repoTaxBaseAmt1 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxBaseAmt1.FormatString = ""
        repoTaxBaseAmt1.HeaderText = "Tax Base Amount 1"
        repoTaxBaseAmt1.Name = colTaxBaseAmt1
        repoTaxBaseAmt1.ReadOnly = True
        repoTaxBaseAmt1.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoTaxBaseAmt1)

        Dim repoTaxRate1 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate1 = New GridViewDecimalColumn()
        repoTaxRate1.FormatString = ""
        repoTaxRate1.HeaderText = "Tax Rate 1"
        repoTaxRate1.Name = colTaxRate1
        repoTaxRate1.IsVisible = False
        repoTaxRate1.ReadOnly = True
        repoTaxRate1.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxRate1)

        Dim repoTaxAmt1 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxAmt1 = New GridViewDecimalColumn()
        repoTaxAmt1.FormatString = ""
        repoTaxAmt1.HeaderText = "Tax Amt 1"
        repoTaxAmt1.Name = colTaxAmt1
        repoTaxAmt1.IsVisible = False
        repoTaxAmt1.ReadOnly = True
        repoTaxAmt1.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxAmt1)

        Dim repoIsSurTax1 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsSurTax1.HeaderText = "Is Surtax 1"
        repoIsSurTax1.Name = colIsSurTax1
        repoIsSurTax1.ReadOnly = True
        repoIsSurTax1.IsVisible = False
        repoIsSurTax1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsSurTax1)

        Dim repoSurTaxCode1 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSurTaxCode1.FormatString = ""
        repoSurTaxCode1.HeaderText = "Surtax 1"
        repoSurTaxCode1.Name = colSurTaxCode1
        repoSurTaxCode1.ReadOnly = True
        repoSurTaxCode1.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoSurTaxCode1)

        Dim repoIsTaxable1 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsTaxable1.HeaderText = "Is Taxable 1"
        repoIsTaxable1.Name = colIsTaxable1
        repoIsTaxable1.ReadOnly = True
        repoIsTaxable1.IsVisible = False
        repoIsTaxable1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsTaxable1)

        Dim repoIsExcisable1 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsExcisable1.HeaderText = "Is Excisable 1"
        repoIsExcisable1.Name = colIsExcisable1
        repoIsExcisable1.ReadOnly = True
        repoIsExcisable1.IsVisible = False
        repoIsExcisable1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsExcisable1)

        Dim repoTax2 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTax2.FormatString = ""
        repoTax2.HeaderText = "Tax 2"
        repoTax2.Name = colTax2
        repoTax2.ReadOnly = True
        repoTax2.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoTax2)

        Dim repoTaxBaseAmt2 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxBaseAmt2.FormatString = ""
        repoTaxBaseAmt2.HeaderText = "Tax Base Amount 2"
        repoTaxBaseAmt2.Name = colTaxBaseAmt2
        repoTaxBaseAmt2.ReadOnly = True
        repoTaxBaseAmt2.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoTaxBaseAmt2)

        Dim repoTaxRate2 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate2 = New GridViewDecimalColumn()
        repoTaxRate2.FormatString = ""
        repoTaxRate2.HeaderText = "Tax Rate 2"
        repoTaxRate2.Name = colTaxRate2
        repoTaxRate2.IsVisible = False
        repoTaxRate2.ReadOnly = True
        repoTaxRate2.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxRate2)

        Dim repoTaxAmt2 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxAmt2 = New GridViewDecimalColumn()
        repoTaxAmt2.FormatString = ""
        repoTaxAmt2.HeaderText = "Tax Amt 2"
        repoTaxAmt2.Name = colTaxAmt2
        repoTaxAmt2.IsVisible = False
        repoTaxAmt2.ReadOnly = True
        repoTaxAmt2.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxAmt2)

        Dim repoIsSurTax2 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsSurTax2.HeaderText = "Is Surtax 2"
        repoIsSurTax2.Name = colIsSurTax2
        repoIsSurTax2.ReadOnly = True
        repoIsSurTax2.IsVisible = False
        repoIsSurTax2.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsSurTax2)

        Dim repoSurTaxCode2 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSurTaxCode2.FormatString = ""
        repoSurTaxCode2.HeaderText = "Surtax 2"
        repoSurTaxCode2.Name = colSurTaxCode2
        repoSurTaxCode2.ReadOnly = True
        repoSurTaxCode2.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoSurTaxCode2)

        Dim repoIsTaxable2 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsTaxable2.HeaderText = "Is Taxable 2"
        repoIsTaxable2.Name = colIsTaxable2
        repoIsTaxable2.ReadOnly = True
        repoIsTaxable2.IsVisible = False
        repoIsTaxable2.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsTaxable2)


        Dim repoIsExcisable2 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsExcisable2.HeaderText = "Is Excisable 2"
        repoIsExcisable2.Name = colIsExcisable2
        repoIsExcisable2.ReadOnly = True
        repoIsExcisable2.IsVisible = False
        repoIsExcisable2.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsExcisable2)

        Dim repoTax3 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTax3.FormatString = ""
        repoTax3.HeaderText = "Tax 3"
        repoTax3.Name = colTax3
        repoTax3.ReadOnly = True
        repoTax3.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoTax3)

        Dim repoTaxBaseAmt3 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxBaseAmt3.FormatString = ""
        repoTaxBaseAmt3.HeaderText = "Tax Base Amount 3"
        repoTaxBaseAmt3.Name = colTaxBaseAmt3
        repoTaxBaseAmt3.ReadOnly = True
        repoTaxBaseAmt3.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoTaxBaseAmt3)

        Dim repoTaxRate3 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate3 = New GridViewDecimalColumn()
        repoTaxRate3.FormatString = ""
        repoTaxRate3.HeaderText = "Tax Rate 3"
        repoTaxRate3.Name = colTaxRate3
        repoTaxRate3.IsVisible = False
        repoTaxRate3.ReadOnly = True
        repoTaxRate3.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxRate3)

        Dim repoTaxAmt3 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxAmt3 = New GridViewDecimalColumn()
        repoTaxAmt3.FormatString = ""
        repoTaxAmt3.HeaderText = "Tax Amt 3"
        repoTaxAmt3.Name = colTaxAmt3
        repoTaxAmt3.IsVisible = False
        repoTaxAmt3.ReadOnly = True
        repoTaxAmt3.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxAmt3)

        Dim repoIsSurTax3 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsSurTax3.HeaderText = "Is Surtax 3"
        repoIsSurTax3.Name = colIsSurTax3
        repoIsSurTax3.ReadOnly = True
        repoIsSurTax3.IsVisible = False
        repoIsSurTax3.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsSurTax3)

        Dim repoSurTaxCode3 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSurTaxCode3.FormatString = ""
        repoSurTaxCode3.HeaderText = "Surtax 3"
        repoSurTaxCode3.Name = colSurTaxCode3
        repoSurTaxCode3.ReadOnly = True
        repoSurTaxCode3.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoSurTaxCode3)

        Dim repoIsTaxable3 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsTaxable3.HeaderText = "Is Taxable 3"
        repoIsTaxable3.Name = colIsTaxable3
        repoIsTaxable3.ReadOnly = True
        repoIsTaxable3.IsVisible = False
        repoIsTaxable3.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsTaxable3)


        Dim repoIsExcisable3 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsExcisable3.HeaderText = "Is Excisable 3"
        repoIsExcisable3.Name = colIsExcisable3
        repoIsExcisable3.ReadOnly = True
        repoIsExcisable3.IsVisible = False
        repoIsExcisable3.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsExcisable3)

        Dim repoTax4 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTax4.FormatString = ""
        repoTax4.HeaderText = "Tax 4"
        repoTax4.Name = colTax4
        repoTax4.ReadOnly = True
        repoTax4.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoTax4)

        Dim repoTaxBaseAmt4 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxBaseAmt4.FormatString = ""
        repoTaxBaseAmt4.HeaderText = "Tax Base Amount 4"
        repoTaxBaseAmt4.Name = colTaxBaseAmt4
        repoTaxBaseAmt4.ReadOnly = True
        repoTaxBaseAmt4.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoTaxBaseAmt4)

        Dim repoTaxRate4 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate4 = New GridViewDecimalColumn()
        repoTaxRate4.FormatString = ""
        repoTaxRate4.HeaderText = "Tax Rate 4"
        repoTaxRate4.Name = colTaxRate4
        repoTaxRate4.IsVisible = False
        repoTaxRate4.ReadOnly = True
        repoTaxRate4.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxRate4)

        Dim repoTaxAmt4 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxAmt4 = New GridViewDecimalColumn()
        repoTaxAmt4.FormatString = ""
        repoTaxAmt4.HeaderText = "Tax Amt 4"
        repoTaxAmt4.Name = colTaxAmt4
        repoTaxAmt4.IsVisible = False
        repoTaxAmt4.ReadOnly = True
        repoTaxAmt4.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxAmt4)

        Dim repoIsSurTax4 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsSurTax4.HeaderText = "Is Surtax 4"
        repoIsSurTax4.Name = colIsSurTax4
        repoIsSurTax4.ReadOnly = True
        repoIsSurTax4.IsVisible = False
        repoIsSurTax4.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsSurTax4)

        Dim repoSurTaxCode4 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSurTaxCode4.FormatString = ""
        repoSurTaxCode4.HeaderText = "Surtax 4"
        repoSurTaxCode4.Name = colSurTaxCode4
        repoSurTaxCode4.ReadOnly = True
        repoSurTaxCode4.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoSurTaxCode4)

        Dim repoIsTaxable4 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsTaxable4.HeaderText = "Is Taxable 4"
        repoIsTaxable4.Name = colIsTaxable4
        repoIsTaxable4.ReadOnly = True
        repoIsTaxable4.IsVisible = False
        repoIsTaxable4.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsTaxable4)


        Dim repoIsExcisable4 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsExcisable4.HeaderText = "Is Excisable 4"
        repoIsExcisable4.Name = colIsExcisable4
        repoIsExcisable4.ReadOnly = True
        repoIsExcisable4.IsVisible = False
        repoIsExcisable4.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsExcisable4)

        Dim repoTax5 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTax5.FormatString = ""
        repoTax5.HeaderText = "Tax 5"
        repoTax5.Name = colTax5
        repoTax5.ReadOnly = True
        repoTax5.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoTax5)

        Dim repoTaxBaseAmt5 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxBaseAmt5.FormatString = ""
        repoTaxBaseAmt5.HeaderText = "Tax Base Amount 5"
        repoTaxBaseAmt5.Name = colTaxBaseAmt5
        repoTaxBaseAmt5.ReadOnly = True
        repoTaxBaseAmt5.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoTaxBaseAmt5)

        Dim repoTaxRate5 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate5 = New GridViewDecimalColumn()
        repoTaxRate5.FormatString = ""
        repoTaxRate5.HeaderText = "Tax Rate 5"
        repoTaxRate5.Name = colTaxRate5
        repoTaxRate5.IsVisible = False
        repoTaxRate5.ReadOnly = True
        repoTaxRate5.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxRate5)

        Dim repoTaxAmt5 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxAmt5 = New GridViewDecimalColumn()
        repoTaxAmt5.FormatString = ""
        repoTaxAmt5.HeaderText = "Tax Amt 5"
        repoTaxAmt5.Name = colTaxAmt5
        repoTaxAmt5.IsVisible = False
        repoTaxAmt3.ReadOnly = True
        repoTaxAmt5.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxAmt5)

        Dim repoIsSurTax5 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsSurTax5.HeaderText = "Is Surtax 5"
        repoIsSurTax5.Name = colIsSurTax5
        repoIsSurTax5.ReadOnly = True
        repoIsSurTax5.IsVisible = False
        repoIsSurTax5.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsSurTax5)

        Dim repoSurTaxCode5 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSurTaxCode5.FormatString = ""
        repoSurTaxCode5.HeaderText = "Surtax 5"
        repoSurTaxCode5.Name = colSurTaxCode5
        repoSurTaxCode5.ReadOnly = True
        repoSurTaxCode5.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoSurTaxCode5)

        Dim repoIsTaxable5 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsTaxable5.HeaderText = "Is Taxable 5"
        repoIsTaxable5.Name = colIsTaxable5
        repoIsTaxable5.ReadOnly = True
        repoIsTaxable5.IsVisible = False
        repoIsTaxable5.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsTaxable5)


        Dim repoIsExcisable5 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsExcisable5.HeaderText = "Is Excisable 5"
        repoIsExcisable5.Name = colIsExcisable5
        repoIsExcisable5.ReadOnly = True
        repoIsExcisable5.IsVisible = False
        repoIsExcisable5.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsExcisable5)


        Dim repoTax6 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTax6.FormatString = ""
        repoTax6.HeaderText = "Tax 6"
        repoTax6.Name = colTax6
        repoTax6.ReadOnly = True
        repoTax6.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoTax6)

        Dim repoTaxBaseAmt6 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxBaseAmt6.FormatString = ""
        repoTaxBaseAmt6.HeaderText = "Tax Base Amount 6"
        repoTaxBaseAmt6.Name = colTaxBaseAmt6
        repoTaxBaseAmt6.ReadOnly = True
        repoTaxBaseAmt6.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoTaxBaseAmt6)

        Dim repoTaxRate6 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate6 = New GridViewDecimalColumn()
        repoTaxRate6.FormatString = ""
        repoTaxRate6.HeaderText = "Tax Rate 6"
        repoTaxRate6.Name = colTaxRate6
        repoTaxRate6.IsVisible = False
        repoTaxRate6.ReadOnly = True
        repoTaxRate6.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxRate6)

        Dim repoTaxAmt6 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxAmt6 = New GridViewDecimalColumn()
        repoTaxAmt6.FormatString = ""
        repoTaxAmt6.HeaderText = "Tax Amt 6"
        repoTaxAmt6.Name = colTaxAmt6
        repoTaxAmt6.IsVisible = False
        repoTaxAmt6.ReadOnly = True
        repoTaxAmt6.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxAmt6)

        Dim repoIsSurTax6 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsSurTax6.HeaderText = "Is Surtax 6"
        repoIsSurTax6.Name = colIsSurTax6
        repoIsSurTax6.ReadOnly = True
        repoIsSurTax6.IsVisible = False
        repoIsSurTax6.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsSurTax6)

        Dim repoSurTaxCode6 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSurTaxCode6.FormatString = ""
        repoSurTaxCode6.HeaderText = "Surtax 6"
        repoSurTaxCode6.Name = colSurTaxCode6
        repoSurTaxCode6.ReadOnly = True
        repoSurTaxCode6.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoSurTaxCode6)

        Dim repoIsTaxable6 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsTaxable6.HeaderText = "Is Taxable 6"
        repoIsTaxable6.Name = colIsTaxable6
        repoIsTaxable6.ReadOnly = True
        repoIsTaxable6.IsVisible = False
        repoIsTaxable6.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsTaxable6)


        Dim repoIsExcisable6 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsExcisable6.HeaderText = "Is Excisable 6"
        repoIsExcisable6.Name = colIsExcisable6
        repoIsExcisable6.ReadOnly = True
        repoIsExcisable6.IsVisible = False
        repoIsExcisable6.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsExcisable6)

        Dim repoTax7 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTax7.FormatString = ""
        repoTax7.HeaderText = "Tax 7"
        repoTax7.Name = colTax7
        repoTax7.ReadOnly = True
        repoTax7.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoTax7)

        Dim repoTaxBaseAmt7 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxBaseAmt7.FormatString = ""
        repoTaxBaseAmt7.HeaderText = "Tax Base Amount 7"
        repoTaxBaseAmt7.Name = colTaxBaseAmt7
        repoTaxBaseAmt7.ReadOnly = True
        repoTaxBaseAmt7.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoTaxBaseAmt7)

        Dim repoTaxRate7 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate7 = New GridViewDecimalColumn()
        repoTaxRate7.FormatString = ""
        repoTaxRate7.HeaderText = "Tax Rate 7"
        repoTaxRate7.Name = colTaxRate7
        repoTaxRate7.IsVisible = False
        repoTaxRate7.ReadOnly = True
        repoTaxRate7.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxRate7)

        Dim repoTaxAmt7 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxAmt7 = New GridViewDecimalColumn()
        repoTaxAmt7.FormatString = ""
        repoTaxAmt7.HeaderText = "Tax Amt 7"
        repoTaxAmt7.Name = colTaxAmt7
        repoTaxAmt7.IsVisible = False
        repoTaxAmt7.ReadOnly = True
        repoTaxAmt7.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxAmt7)

        Dim repoIsSurTax7 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsSurTax7.HeaderText = "Is Surtax 7"
        repoIsSurTax7.Name = colIsSurTax7
        repoIsSurTax7.ReadOnly = True
        repoIsSurTax7.IsVisible = False
        repoIsSurTax7.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsSurTax7)

        Dim repoSurTaxCode7 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSurTaxCode7.FormatString = ""
        repoSurTaxCode7.HeaderText = "Surtax 7"
        repoSurTaxCode7.Name = colSurTaxCode7
        repoSurTaxCode7.ReadOnly = True
        repoSurTaxCode7.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoSurTaxCode7)

        Dim repoIsTaxable7 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsTaxable7.HeaderText = "Is Taxable 7"
        repoIsTaxable7.Name = colIsTaxable7
        repoIsTaxable7.ReadOnly = True
        repoIsTaxable7.IsVisible = False
        repoIsTaxable7.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsTaxable7)

        Dim repoIsExcisable7 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsExcisable7.HeaderText = "Is Excisable 7"
        repoIsExcisable7.Name = colIsExcisable7
        repoIsExcisable7.ReadOnly = True
        repoIsExcisable7.IsVisible = False
        repoIsExcisable7.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsExcisable7)

        Dim repoTax8 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTax8.FormatString = ""
        repoTax8.HeaderText = "Tax 8"
        repoTax8.Name = colTax8
        repoTax8.ReadOnly = True
        repoTax8.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoTax8)

        Dim repoTaxBaseAmt8 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxBaseAmt8.FormatString = ""
        repoTaxBaseAmt8.HeaderText = "Tax Base Amount 8"
        repoTaxBaseAmt8.Name = colTaxBaseAmt8
        repoTaxBaseAmt8.ReadOnly = True
        repoTaxBaseAmt8.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoTaxBaseAmt8)

        Dim repoTaxRate8 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate8 = New GridViewDecimalColumn()
        repoTaxRate8.FormatString = ""
        repoTaxRate8.HeaderText = "Tax Rate 8"
        repoTaxRate8.Name = colTaxRate8
        repoTaxRate8.IsVisible = False
        repoTaxRate8.ReadOnly = True
        repoTaxRate8.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxRate8)

        Dim repoTaxAmt8 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxAmt8 = New GridViewDecimalColumn()
        repoTaxAmt8.FormatString = ""
        repoTaxAmt8.HeaderText = "Tax Amt 8"
        repoTaxAmt8.Name = colTaxAmt8
        repoTaxAmt8.IsVisible = False
        repoTaxAmt8.ReadOnly = True
        repoTaxAmt8.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxAmt8)

        Dim repoIsSurTax8 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsSurTax8.HeaderText = "Is Surtax 8"
        repoIsSurTax8.Name = colIsSurTax8
        repoIsSurTax8.ReadOnly = True
        repoIsSurTax8.IsVisible = False
        repoIsSurTax8.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsSurTax8)

        Dim repoSurTaxCode8 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSurTaxCode8.FormatString = ""
        repoSurTaxCode8.HeaderText = "Surtax 8"
        repoSurTaxCode8.Name = colSurTaxCode8
        repoSurTaxCode8.ReadOnly = True
        repoSurTaxCode8.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoSurTaxCode8)

        Dim repoIsTaxable8 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsTaxable8.HeaderText = "Is Taxable 8"
        repoIsTaxable8.Name = colIsTaxable8
        repoIsTaxable8.ReadOnly = True
        repoIsTaxable8.IsVisible = False
        repoIsTaxable8.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsTaxable8)

        Dim repoIsExcisable8 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsExcisable8.HeaderText = "Is Excisable 8"
        repoIsExcisable8.Name = colIsExcisable8
        repoIsExcisable8.ReadOnly = True
        repoIsExcisable8.IsVisible = False
        repoIsExcisable8.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsExcisable8)

        Dim repoTax9 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTax9.FormatString = ""
        repoTax9.HeaderText = "Tax 9"
        repoTax9.Name = colTax9
        repoTax9.ReadOnly = True
        repoTax9.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoTax9)

        Dim repoTaxBaseAmt9 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxBaseAmt9.FormatString = ""
        repoTaxBaseAmt9.HeaderText = "Tax Base Amount 9"
        repoTaxBaseAmt9.Name = colTaxBaseAmt9
        repoTaxBaseAmt9.ReadOnly = True
        repoTaxBaseAmt9.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoTaxBaseAmt9)

        Dim repoTaxRate9 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate9 = New GridViewDecimalColumn()
        repoTaxRate9.FormatString = ""
        repoTaxRate9.HeaderText = "Tax Rate 9"
        repoTaxRate9.Name = colTaxRate9
        repoTaxRate9.IsVisible = False
        repoTaxRate9.ReadOnly = True
        repoTaxRate9.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxRate9)

        Dim repoTaxAmt9 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxAmt9 = New GridViewDecimalColumn()
        repoTaxAmt9.FormatString = ""
        repoTaxAmt9.HeaderText = "Tax Amt 9"
        repoTaxAmt9.Name = colTaxAmt9
        repoTaxAmt9.IsVisible = False
        repoTaxAmt9.ReadOnly = True
        repoTaxAmt9.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxAmt9)

        Dim repoIsSurTax9 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsSurTax9.HeaderText = "Is Surtax 9"
        repoIsSurTax9.Name = colIsSurTax9
        repoIsSurTax9.ReadOnly = True
        repoIsSurTax9.IsVisible = False
        repoIsSurTax9.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsSurTax9)

        Dim repoSurTaxCode9 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSurTaxCode9.FormatString = ""
        repoSurTaxCode9.HeaderText = "Surtax 9"
        repoSurTaxCode9.Name = colSurTaxCode9
        repoSurTaxCode9.ReadOnly = True
        repoSurTaxCode9.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoSurTaxCode9)

        Dim repoIsTaxable9 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsTaxable9.HeaderText = "Is Taxable 9"
        repoIsTaxable9.Name = colIsTaxable9
        repoIsTaxable9.ReadOnly = True
        repoIsTaxable9.IsVisible = False
        repoIsTaxable9.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsTaxable9)

        Dim repoIsExcisable9 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsExcisable9.HeaderText = "Is Excisable 9"
        repoIsExcisable9.Name = colIsExcisable9
        repoIsExcisable9.ReadOnly = True
        repoIsExcisable9.IsVisible = False
        repoIsExcisable9.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsExcisable9)

        Dim repoTax10 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTax10.FormatString = ""
        repoTax10.HeaderText = "Tax 10"
        repoTax10.Name = colTax10
        repoTax10.ReadOnly = True
        repoTax10.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoTax10)

        Dim repoTaxBaseAmt10 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxBaseAmt10.FormatString = ""
        repoTaxBaseAmt10.HeaderText = "Tax Base Amount 10"
        repoTaxBaseAmt10.Name = colTaxBaseAmt10
        repoTaxBaseAmt10.ReadOnly = True
        repoTaxBaseAmt10.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoTaxBaseAmt10)

        Dim repoTaxRate10 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate10 = New GridViewDecimalColumn()
        repoTaxRate10.FormatString = ""
        repoTaxRate10.HeaderText = "Tax Rate 10"
        repoTaxRate10.Name = colTaxRate10
        repoTaxRate10.IsVisible = False
        repoTaxRate10.ReadOnly = True
        repoTaxRate10.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxRate10)

        Dim repoTaxAmt10 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxAmt10 = New GridViewDecimalColumn()
        repoTaxAmt10.FormatString = ""
        repoTaxAmt10.HeaderText = "Tax Amt 10"
        repoTaxAmt10.Name = colTaxAmt10
        repoTaxAmt10.IsVisible = False
        repoTaxAmt10.ReadOnly = True
        repoTaxAmt10.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxAmt10)

        Dim repoIsSurTax10 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsSurTax10.HeaderText = "Is Surtax 10"
        repoIsSurTax10.Name = colIsSurTax10
        repoIsSurTax10.ReadOnly = True
        repoIsSurTax10.IsVisible = False
        repoIsSurTax10.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsSurTax10)

        Dim repoSurTaxCode10 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSurTaxCode10.FormatString = ""
        repoSurTaxCode10.HeaderText = "Surtax 10"
        repoSurTaxCode10.Name = colSurTaxCode10
        repoSurTaxCode10.ReadOnly = True
        repoSurTaxCode10.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoSurTaxCode10)

        Dim repoIsTaxable10 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsTaxable10.HeaderText = "Is Taxable 10"
        repoIsTaxable10.Name = colIsTaxable10
        repoIsTaxable10.ReadOnly = True
        repoIsTaxable10.IsVisible = False
        repoIsTaxable10.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsTaxable10)

        Dim repoIsExcisable10 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsExcisable10.HeaderText = "Is Excisable 10"
        repoIsExcisable10.Name = colIsExcisable10
        repoIsExcisable10.ReadOnly = True
        repoIsExcisable10.IsVisible = False
        repoIsExcisable10.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsExcisable10)


        Dim repoTotTaxAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTotTaxAmt = New GridViewDecimalColumn()
        repoTotTaxAmt.FormatString = ""
        repoTotTaxAmt.HeaderText = "Tax Amount"
        repoTotTaxAmt.Name = colTotTaxAmt
        repoTotTaxAmt.Width = 100
        repoTotTaxAmt.ReadOnly = True
        repoTotTaxAmt.IsVisible = False
        repoTotTaxAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTotTaxAmt)

        Dim repoAmtAfterTax As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAmtAfterTax = New GridViewDecimalColumn()
        repoAmtAfterTax.FormatString = ""
        repoAmtAfterTax.HeaderText = "Included Tax Amount"
        repoAmtAfterTax.Name = colAmtAfterTax
        repoAmtAfterTax.Width = 120
        repoAmtAfterTax.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoAmtAfterTax.ReadOnly = True
        repoAmtAfterTax.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoAmtAfterTax)

        Dim repoIsUnclaimedTax As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsUnclaimedTax.HeaderText = "Unclaimed Tax"
        repoIsUnclaimedTax.Name = colIsUnclaimedTax
        repoIsUnclaimedTax.Width = 90
        repoIsUnclaimedTax.ReadOnly = False
        repoIsUnclaimedTax.IsVisible = False
        repoIsUnclaimedTax.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsUnclaimedTax)

        Dim repoDocType As GridViewComboBoxColumn = New GridViewComboBoxColumn()
        repoDocType.FormatString = ""
        repoDocType.HeaderText = "Document Type"
        repoDocType.Name = colDocType
        repoDocType.Width = 100
        repoDocType.ReadOnly = False
        repoDocType.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        repoDocType.DataSource = GetDocType()
        repoDocType.ValueMember = "Code"
        repoDocType.DisplayMember = "Code"
        gv1.MasterTemplate.Columns.Add(repoDocType)

        Dim repoInviceNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoInviceNo.FormatString = ""
        repoInviceNo.HeaderText = "Document No"
        repoInviceNo.Name = colDocNo
        repoInviceNo.HeaderImage = My.Resources.Resources.search4
        repoInviceNo.TextImageRelation = TextImageRelation.TextBeforeImage
        repoInviceNo.Width = 100
        gv1.MasterTemplate.Columns.Add(repoInviceNo)

        Dim repoRemarks As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoRemarks.FormatString = ""
        repoRemarks.HeaderText = "Remarks"
        repoRemarks.Name = colRemarks
        repoRemarks.IsVisible = True
        repoInviceNo.Width = 100
        gv1.MasterTemplate.Columns.Add(repoRemarks)

        gv1.AllowDeleteRow = True
        gv1.AllowAddNewRow = False
        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = False
        gv1.AllowRowReorder = False
        gv1.EnableSorting = False
        gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        ReStoreGridLayout()
    End Sub

    Sub LoadBlankGridTax()
        gv2.Rows.Clear()
        gv2.Columns.Clear()

        Dim repoTaxAuthCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTaxAuthCode.FormatString = ""
        repoTaxAuthCode.HeaderText = "Tax Authority Code"
        repoTaxAuthCode.Name = colTTaxAutCode
        repoTaxAuthCode.Width = 150
        repoTaxAuthCode.ReadOnly = True
        gv2.MasterTemplate.Columns.Add(repoTaxAuthCode)

        Dim repoTaxAuthName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTaxAuthName.FormatString = ""
        repoTaxAuthName.HeaderText = "Tax Authority"
        repoTaxAuthName.Name = colTTaxAutName
        repoTaxAuthName.Width = 200
        repoTaxAuthName.ReadOnly = True
        gv2.MasterTemplate.Columns.Add(repoTaxAuthName)

        Dim repoTaxRate As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate.FormatString = ""
        repoTaxRate.HeaderText = "Tax Rate"
        repoTaxRate.Name = colTTaxRate
        repoTaxRate.Width = 100
        repoTaxRate.ReadOnly = False
        repoTaxRate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv2.MasterTemplate.Columns.Add(repoTaxRate)

        'Dim repoIsSurTax As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        'repoIsSurTax.HeaderText = "Is Surtax"
        'repoIsSurTax.Name = colTIsSurTax
        'repoIsSurTax.Width = 80
        'repoIsSurTax.ReadOnly = True
        'repoIsSurTax.IsVisible = False
        'repoIsSurTax.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        'gv2.MasterTemplate.Columns.Add(repoIsSurTax)

        'Dim repoSurTaxCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        'repoSurTaxCode.FormatString = ""
        'repoSurTaxCode.HeaderText = "Surtax"
        'repoSurTaxCode.Name = colTSurTaxCode
        'repoSurTaxCode.Width = 100
        'repoSurTaxCode.ReadOnly = True
        'repoSurTaxCode.IsVisible = False
        'gv2.MasterTemplate.Columns.Add(repoSurTaxCode)

        'Dim repoIsTaxable As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        'repoIsTaxable.HeaderText = "Is Taxable"
        'repoIsTaxable.Name = colTIsTaxable
        'repoIsTaxable.Width = 80
        'repoIsTaxable.ReadOnly = True
        'repoIsTaxable.IsVisible = False
        'repoIsTaxable.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        'gv2.MasterTemplate.Columns.Add(repoIsTaxable)

        Dim repoTaxBaseAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxBaseAmt.FormatString = ""
        repoTaxBaseAmt.HeaderText = "Base Amount"
        repoTaxBaseAmt.Name = colTBaseAmt
        repoTaxBaseAmt.Width = 100
        repoTaxBaseAmt.ReadOnly = True
        repoTaxBaseAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv2.MasterTemplate.Columns.Add(repoTaxBaseAmt)


        Dim repoTaxAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxAmt.FormatString = ""
        repoTaxAmt.HeaderText = "Tax Amount"
        repoTaxAmt.Name = colTTaxAmt
        ''repoTaxAmt.Minimum = 0
        repoTaxAmt.Width = 100
        repoTaxAmt.ReadOnly = False
        repoTaxAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv2.MasterTemplate.Columns.Add(repoTaxAmt)


        gv2.AllowAddNewRow = False
        gv2.ShowGroupPanel = False
        gv2.AllowColumnReorder = False
        gv2.AllowRowReorder = False
        gv2.EnableSorting = False
        gv2.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv2.MasterTemplate.ShowRowHeaderColumn = False

    End Sub

    Private Sub gv1_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellValueChanged
        Try

            If (Not isInsideLoadData) Then
                If Not isCellValueChangedOpen Then
                    If ((clsCommon.CompairString(e.Column.Name, colAmt) = CompairStringResult.Equal) OrElse (clsCommon.CompairString(e.Column.Name, colDisPer) = CompairStringResult.Equal) OrElse (clsCommon.CompairString(e.Column.Name, colIsUnclaimedTax) = CompairStringResult.Equal) OrElse (clsCommon.CompairString(e.Column.Name, colDedCode) = CompairStringResult.Equal) OrElse (clsCommon.CompairString(e.Column.Name, colAChgCode) = CompairStringResult.Equal) OrElse (clsCommon.CompairString(e.Column.Name, colDocNo) = CompairStringResult.Equal) OrElse (clsCommon.CompairString(e.Column.Name, colRefDocNo) = CompairStringResult.Equal) OrElse (clsCommon.CompairString(e.Column.Name, colDocRefAmount) = CompairStringResult.Equal)) Then

                        isCellValueChangedOpen = True
                        If ((clsCommon.CompairString(e.Column.Name, colAmt) = CompairStringResult.Equal) OrElse (clsCommon.CompairString(e.Column.Name, colDisPer) = CompairStringResult.Equal) OrElse (clsCommon.CompairString(e.Column.Name, colIsUnclaimedTax) = CompairStringResult.Equal) OrElse (clsCommon.CompairString(e.Column.Name, colDocRefAmount) = CompairStringResult.Equal)) Then
                            If (clsCommon.CompairString(e.Column.Name, colDocRefAmount) = CompairStringResult.Equal) Then
                                gv1.CurrentRow.Cells(colAmt).Value = clsCommon.myCdbl(gv1.CurrentRow.Cells(colDocRefAmount).Value) * clsCommon.myCdbl(gv1.CurrentRow.Cells(colTDSper).Value) / 100
                            End If
                            If rbtnTaxCalAutomatic.IsChecked Then
                                UpdateCurrentRowWihtRowNo(gv1.CurrentRow.Index)
                            ElseIf rbtnTaxCalManual.IsChecked Then
                                For ii As Integer = 0 To gv1.Rows.Count - 1
                                    UpdateCurrentRowWihtRowNo(ii)
                                Next
                            End If
                            UpdateAllTotals()
                        ElseIf (clsCommon.CompairString(e.Column.Name, colDedCode) = CompairStringResult.Equal) Then
                            OpenGLAccount(False)

                        ElseIf (clsCommon.CompairString(e.Column.Name, colAChgCode) = CompairStringResult.Equal) Then
                            OpenAdditionCharges(False)

                        ElseIf (clsCommon.CompairString(e.Column.Name, colDocNo) = CompairStringResult.Equal) Then
                            OpenInvoiceNo(False)
                            ''richa agarwal
                        ElseIf (clsCommon.CompairString(e.Column.Name, colRefDocNo) = CompairStringResult.Equal) Then
                            OpenPaymentEntryNo(False)
                        End If
                        ''setGridFocus()

                    End If
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isCellValueChangedOpen = False
        End Try
    End Sub

    Private Sub ApplyQuickMode()
        If chkQuickMode.Checked Then
            If gv1.RowCount - 1 > gv1.CurrentRow.Index Then
                If clsCommon.myLen(gv1.Rows(gv1.RowCount - 1).Cells(colACCode).Value) <= 0 AndAlso clsCommon.myLen(gv1.CurrentRow.Cells(colACCode).Value) > 0 Then
                    gv1.Rows(gv1.RowCount - 1).Cells(colACCode).Value = clsCommon.myCstr(gv1.CurrentRow.Cells(colACCode).Value)
                    gv1.Rows(gv1.RowCount - 1).Cells(colACName).Value = clsCommon.myCstr(gv1.CurrentRow.Cells(colACName).Value)
                End If
            End If
        End If
    End Sub

    Private Sub setGridFocus()
        Dim intCurrRow As Integer = gv1.CurrentRow.Index
        gv1.CurrentRow.Cells(colLineNo).Value = clsCommon.myCdbl(intCurrRow + 1)
        If intCurrRow = gv1.Rows.Count - 1 Then
            gv1.Rows.AddNew()
            If chkQuickMode.Checked Then
                gv1.CurrentRow.Cells(colACCode).Value = gv1.Rows(intCurrRow).Cells(colACCode).Value
                gv1.CurrentRow.Cells(colACName).Value = gv1.Rows(intCurrRow).Cells(colACName).Value
            End If
            gv1.CurrentRow = gv1.Rows(intCurrRow)
        End If
        If clsCommon.myLen(gv1.CurrentRow.Cells(colACCode).Value) > 0 Then
            If gv1.CurrentColumn Is gv1.Columns(colACCode) Then
                gv1.CurrentRow = gv1.Rows(intCurrRow)
                gv1.CurrentColumn = gv1.Columns(colAmt)
            ElseIf gv1.CurrentColumn Is gv1.Columns(colAmt) Then
                gv1.CurrentRow = gv1.Rows(intCurrRow)
                gv1.CurrentColumn = gv1.Columns(colDisPer)
            ElseIf gv1.CurrentColumn Is gv1.Columns(colDisPer) Then
                gv1.CurrentRow = gv1.Rows(intCurrRow + 1)
                gv1.CurrentColumn = gv1.Columns(colACCode)
            End If
        End If

    End Sub

    Private Sub OpenGLAccount(ByVal isButtonClick As Boolean)
        If clsCommon.myLen(txtlocation.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "Please first select Location", Me.Text)
            gv1.CurrentRow.Cells(colDedCode).Value = ""
            Exit Sub
        End If
        Dim qry As String = "select TSPL_TDS_DEDUCTION_HEAD.Deduction_Code as Code,TSPL_TDS_DEDUCTION_HEAD.Description,TSPL_TDS_DEDUCTION_HEAD.TDS_Section as [TDS Section],TSPL_TDS_DEDUCTION_HEAD.Gl_Account as [Account Code],TSPL_GL_ACCOUNTS.Description as Account   "
        qry += "  from TSPL_TDS_DEDUCTION_HEAD "
        qry += "  inner join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code=TSPL_TDS_DEDUCTION_HEAD.Gl_Account"
        Dim whrcls As String = "1=1 " '"TSPL_GL_ACCOUNTS.Account_Seg_Code7='" + txtlocation.Value + "'"
        ''richa agarwal 
        If clsCommon.CompairString(cmbRefType.SelectedValue, "A") = CompairStringResult.Equal Then
            If clsCommon.myLen(TxtVendorNo.Value) > 0 Then
                whrcls += " and TSPL_TDS_DEDUCTION_HEAD.Deduction_Code in (Select isnull(Nature_Of_Deduction,'')  from TSPL_TDS_VENDOR_DETAILS where Vendor_Code ='" & TxtVendorNo.Value & "')"
            End If
        End If
        ''changed by Panch Raj against Ticket No: BM00000008151    
        gv1.CurrentRow.Cells(colDedCode).Value = clsCommon.ShowSelectForm("NatDedFinderAP", qry, "Code", whrcls, clsCommon.myCstr(gv1.CurrentRow.Cells(colDedCode).Value), "", isButtonClick)
        If clsCommon.myLen(gv1.CurrentRow.Cells(colDedCode).Value) > 0 Then
            qry = qry + " where " + whrcls + " and TSPL_TDS_DEDUCTION_HEAD.Deduction_Code='" + clsCommon.myCstr(gv1.CurrentRow.Cells(colDedCode).Value) + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

            gv1.CurrentRow.Cells(colDedName).Value = clsCommon.myCstr(dt.Rows(0)("Description"))
            gv1.CurrentRow.Cells(colDedSection).Value = clsCommon.myCstr(dt.Rows(0)("TDS Section"))
            gv1.CurrentRow.Cells(colACCode).Value = clsERPFuncationality.ChangeGLAccountLocationSegment(clsCommon.myCstr(dt.Rows(0)("Account Code")), txtlocation.Value, True, True, Nothing)
            gv1.CurrentRow.Cells(colACName).Value = clsGLAccount.GetName(gv1.CurrentRow.Cells(colACCode).Value)
            ''RICHA 13 sEP,2017
            Dim objDedDetails As clsTDSDeductionDetails = clsTDSDeductionDetails.GetApplicableTDRate(clsCommon.myCstr(gv1.CurrentRow.Cells(colDedCode).Value), clsCommon.myCdbl(gv1.CurrentRow.Cells(colDocRefAmount).Value), Nothing, False, TxtVendorNo.Value)
            If (objDedDetails IsNot Nothing) Then
                gv1.CurrentRow.Cells(colTDSper).Value = objDedDetails.TDS
                gv1.CurrentRow.Cells(colAmt).Value = clsCommon.myCdbl(gv1.CurrentRow.Cells(colDocRefAmount).Value) * objDedDetails.TDS / 100
            End If
        Else
            gv1.CurrentRow.Cells(colDedName).Value = ""
            gv1.CurrentRow.Cells(colDedSection).Value = ""
            gv1.CurrentRow.Cells(colACCode).Value = ""
            gv1.CurrentRow.Cells(colACName).Value = ""
        End If



        txtlocation.Enabled = False
        SetitemWiseTaxSetting(True, True)
    End Sub
    ''richa agarwal
    Private Sub OpenPaymentEntryNo(ByVal isButtonClick As Boolean)
        If clsCommon.myLen(TxtVendorNo.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "Please select Vendor", Me.Text)
            gv1.CurrentRow.Cells(colRefDocNo).Value = ""
            Exit Sub
        End If
        If clsCommon.myLen(txtlocation.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "Please first select Location", Me.Text)
            gv1.CurrentRow.Cells(colDedCode).Value = ""
            Exit Sub
        End If
        Dim qry As String = ""
        Dim whrcls As String = ""
        If clsCommon.CompairString(cmbRefType.SelectedValue, "A") = CompairStringResult.Equal Then
            qry = "select Payment_No as Code,CONVERT(VARCHAR,Payment_Date,103) as Date,Vendor_Code as [Vendor code],Vendor_Name as [Vendor Name],Location_Code as [Location Code],Location_Description as [Location Name],Payment_Amount from TSPL_PAYMENT_HEADER " & _
             " where 1=1 and TDS_Amount =0 and Posted =1 and Payment_Type='AV' and Vendor_Code='" & TxtVendorNo.Value & "' and Payment_No not in (  Select isnull(TSPL_VENDOR_INVOICE_DETAIL.AgainstPayment_No,'')  from TSPL_VENDOR_INVOICE_HEAD Left Outer Join TSPL_VENDOR_INVOICE_DETAIL on TSPL_VENDOR_INVOICE_HEAD.Document_No =TSPL_VENDOR_INVOICE_DETAIL.Document_No  where TSPL_VENDOR_INVOICE_HEAD.RefDocType='A')"
            Dim dr As DataRow = clsCommon.ShowSelectFormForRow("ADVANCEAP", qry)
            If dr IsNot Nothing Then
                gv1.CurrentRow.Cells(colRefDocNo).Value = clsCommon.myCstr(dr("Code"))
                gv1.CurrentRow.Cells(colDocRefAmount).Value = clsCommon.myCdbl(dr("Payment_Amount"))

            Else
                gv1.CurrentRow.Cells(colRefDocNo).Value = ""
                gv1.CurrentRow.Cells(colDocRefAmount).Value = 0
            End If
            If clsCommon.myLen(gv1.CurrentRow.Cells(colRefDocNo).Value) > 0 Then
                qry = "select TSPL_TDS_DEDUCTION_HEAD.Deduction_Code as Code,TSPL_TDS_DEDUCTION_HEAD.Description,TSPL_TDS_DEDUCTION_HEAD.TDS_Section as [TDS Section],TSPL_TDS_DEDUCTION_HEAD.Gl_Account as [Account Code],TSPL_GL_ACCOUNTS.Description as Account   " & _
            "  from TSPL_TDS_DEDUCTION_HEAD  inner join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code=TSPL_TDS_DEDUCTION_HEAD.Gl_Account where 1=1 " & _
             "  and TSPL_TDS_DEDUCTION_HEAD.Deduction_Code in (Select isnull(Nature_Of_Deduction,'')  from TSPL_TDS_VENDOR_DETAILS where Vendor_Code ='" & TxtVendorNo.Value & "')"
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    gv1.CurrentRow.Cells(colDedCode).Value = clsCommon.myCstr(dt.Rows(0)("Code"))
                    gv1.CurrentRow.Cells(colDedName).Value = clsCommon.myCstr(dt.Rows(0)("Description"))
                    gv1.CurrentRow.Cells(colDedSection).Value = clsCommon.myCstr(dt.Rows(0)("TDS Section"))
                    gv1.CurrentRow.Cells(colACCode).Value = clsERPFuncationality.ChangeGLAccountLocationSegment(clsCommon.myCstr(dt.Rows(0)("Account Code")), txtlocation.Value, Nothing)
                    gv1.CurrentRow.Cells(colACName).Value = clsGLAccount.GetName(gv1.CurrentRow.Cells(colACCode).Value)
                    Dim objDedDetails As clsTDSDeductionDetails = clsTDSDeductionDetails.GetApplicableTDRate(clsCommon.myCstr(gv1.CurrentRow.Cells(colDedCode).Value), clsCommon.myCdbl(gv1.CurrentRow.Cells(colDocRefAmount).Value), Nothing, False, TxtVendorNo.Value)
                    If (objDedDetails IsNot Nothing) Then
                        gv1.CurrentRow.Cells(colTDSper).Value = objDedDetails.TDS
                        gv1.CurrentRow.Cells(colAmt).Value = clsCommon.myCdbl(gv1.CurrentRow.Cells(colDocRefAmount).Value) * objDedDetails.TDS / 100
                    End If

                Else
                    gv1.CurrentRow.Cells(colDedName).Value = ""
                    gv1.CurrentRow.Cells(colDedName).Value = ""
                    gv1.CurrentRow.Cells(colDedSection).Value = ""
                    gv1.CurrentRow.Cells(colACCode).Value = ""
                    gv1.CurrentRow.Cells(colACName).Value = ""
                    gv1.CurrentRow.Cells(colTDSper).Value = 0
                    gv1.CurrentRow.Cells(colAmt).Value = 0
                End If

            End If
            gv1.Columns(colAmt).ReadOnly = True
            txtlocation.Enabled = False
            SetitemWiseTaxSetting(True, True)
        End If

    End Sub
    ''---------------------------


    Private Sub OpenInvoiceNo(ByVal isButtonClick As Boolean)
        If clsCommon.myLen(TxtVendorNo.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "Please select Vendor", Me.Text)
            gv1.CurrentRow.Cells(colDocNo).Value = ""
            Exit Sub
        End If
        Dim qry As String = ""
        Dim whrcls As String = ""
        If clsCommon.CompairString(DocTypeSaleInvoice, clsCommon.myCstr(gv1.CurrentRow.Cells(colDocType).Value)) = CompairStringResult.Equal Then
            qry = "select TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No as Code , CONVERT(varchar(10), TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103) as Date,TSPL_SALE_INVOICE_HEAD.Cust_Code as [Customer Code],TSPL_SALE_INVOICE_HEAD.Cust_Name as Customer"
            qry += " from TSPL_SALE_INVOICE_HEAD "
            qry += " inner join TSPL_VEHICLE_MASTER on TSPL_VEHICLE_MASTER.Vehicle_Id=TSPL_SALE_INVOICE_HEAD.Vehicle_Code"
            qry += " inner join TSPL_TRANSPORT_MASTER on TSPL_TRANSPORT_MASTER.Transport_Id=TSPL_VEHICLE_MASTER.Transport_Id"
            whrcls = "TSPL_VEHICLE_MASTER.Transport_Id='" + TxtVendorNo.Value + "' and not exists (select 1 from TSPL_VENDOR_INVOICE_DETAIL where TSPL_VENDOR_INVOICE_DETAIL.Invoice_No=TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No AND TSPL_VENDOR_INVOICE_DETAIL.Document_No NOT IN('" + txtDocNo.Value + "')) and TSPL_SALE_INVOICE_HEAD.Is_Post='Y'"
            gv1.CurrentRow.Cells(colDocNo).Value = clsCommon.ShowSelectForm("APInvoiceFiND", qry, "Code", whrcls, clsCommon.myCstr(gv1.CurrentRow.Cells(colDocNo).Value), "", isButtonClick)
        ElseIf clsCommon.CompairString(DocTypeSalesReturn, clsCommon.myCstr(gv1.CurrentRow.Cells(colDocType).Value)) = CompairStringResult.Equal Then
            qry = " select TSPL_SALE_RETURN_HEAD.Sale_Return_No as Code , CONVERT(varchar(10), TSPL_SALE_RETURN_HEAD.Sale_Return_Date,103) as Date,TSPL_SALE_RETURN_HEAD.Cust_Code as [Customer Code],TSPL_SALE_RETURN_HEAD.Cust_Name as Customer,TSPL_VEHICLE_MASTER.Transport_Id "
            qry += " from TSPL_SALE_RETURN_HEAD "
            qry += " inner join TSPL_SALE_INVOICE_HEAD  on TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No= TSPL_SALE_RETURN_HEAD.Invoice_No"
            qry += " inner join TSPL_VEHICLE_MASTER on TSPL_VEHICLE_MASTER.Vehicle_Id=TSPL_SALE_INVOICE_HEAD.Vehicle_Code "
            qry += " inner join TSPL_TRANSPORT_MASTER on TSPL_TRANSPORT_MASTER.Transport_Id=TSPL_VEHICLE_MASTER.Transport_Id"
            whrcls = " TSPL_VEHICLE_MASTER.Transport_Id='" + TxtVendorNo.Value + "' and "
            whrcls += " not exists (select 1 from TSPL_VENDOR_INVOICE_DETAIL where TSPL_VENDOR_INVOICE_DETAIL.Invoice_No=TSPL_SALE_RETURN_HEAD.Sale_Return_No AND TSPL_VENDOR_INVOICE_DETAIL.Document_No NOT IN('" + txtDocNo.Value + "')) and TSPL_SALE_RETURN_HEAD.Is_Post='Y'"
            gv1.CurrentRow.Cells(colDocNo).Value = clsCommon.ShowSelectForm("APInceFinderSR", qry, "Code", whrcls, clsCommon.myCstr(gv1.CurrentRow.Cells(colDocNo).Value), "", isButtonClick)
        ElseIf clsCommon.CompairString(DocTypeLO, clsCommon.myCstr(gv1.CurrentRow.Cells(colDocType).Value)) = CompairStringResult.Equal Then
            qry = " select TSPL_TRANSFER_HEAD.Transfer_No as Code , CONVERT(varchar(10), TSPL_TRANSFER_HEAD.Transfer_Date,103) as Date ,TSPL_VEHICLE_MASTER.Transport_Id "
            qry += " from TSPL_TRANSFER_HEAD"
            qry += " inner join TSPL_VEHICLE_MASTER on TSPL_VEHICLE_MASTER.Vehicle_Id=TSPL_TRANSFER_HEAD.Vehicle_Code "
            qry += " inner join TSPL_TRANSPORT_MASTER on TSPL_TRANSPORT_MASTER.Transport_Id=TSPL_VEHICLE_MASTER.Transport_Id  "
            whrcls = "TSPL_VEHICLE_MASTER.Transport_Id='" + TxtVendorNo.Value + "' and "
            whrcls += " not exists (select 1 from TSPL_VENDOR_INVOICE_DETAIL where TSPL_VENDOR_INVOICE_DETAIL.Invoice_No=TSPL_TRANSFER_HEAD.Transfer_No AND TSPL_VENDOR_INVOICE_DETAIL.Document_No NOT IN('" + txtDocNo.Value + "')) and TSPL_TRANSFER_HEAD.Post='Y' and TSPL_TRANSFER_HEAD.Transfer_Type='LO'  "
            gv1.CurrentRow.Cells(colDocNo).Value = clsCommon.ShowSelectForm("APInvoiceFinderLO", qry, "Code", whrcls, clsCommon.myCstr(gv1.CurrentRow.Cells(colDocNo).Value), "", isButtonClick)
        ElseIf clsCommon.CompairString(DocTypeLI, clsCommon.myCstr(gv1.CurrentRow.Cells(colDocType).Value)) = CompairStringResult.Equal Then
            qry = " select TSPL_TRANSFER_HEAD.Transfer_No as Code , CONVERT(varchar(10), TSPL_TRANSFER_HEAD.Transfer_Date,103) as Date ,TSPL_VEHICLE_MASTER.Transport_Id "
            qry += " from TSPL_TRANSFER_HEAD"
            qry += " inner join TSPL_VEHICLE_MASTER on TSPL_VEHICLE_MASTER.Vehicle_Id=TSPL_TRANSFER_HEAD.Vehicle_Code "
            qry += " inner join TSPL_TRANSPORT_MASTER on TSPL_TRANSPORT_MASTER.Transport_Id=TSPL_VEHICLE_MASTER.Transport_Id  "
            whrcls = "TSPL_VEHICLE_MASTER.Transport_Id='" + TxtVendorNo.Value + "' and "
            whrcls += " not exists (select 1 from TSPL_VENDOR_INVOICE_DETAIL where TSPL_VENDOR_INVOICE_DETAIL.Invoice_No=TSPL_TRANSFER_HEAD.Transfer_No AND TSPL_VENDOR_INVOICE_DETAIL.Document_No NOT IN('" + txtDocNo.Value + "')) and TSPL_TRANSFER_HEAD.Post='Y' and TSPL_TRANSFER_HEAD.Transfer_Type='LI'  "
            gv1.CurrentRow.Cells(colDocNo).Value = clsCommon.ShowSelectForm("APInvoiceFinderLI", qry, "Code", whrcls, clsCommon.myCstr(gv1.CurrentRow.Cells(colDocNo).Value), "", isButtonClick)
        ElseIf clsCommon.CompairString(DocTypeTransfer, clsCommon.myCstr(gv1.CurrentRow.Cells(colDocType).Value)) = CompairStringResult.Equal Then
            qry = "select TSPL_IssueReturn_HEAD.Doc_No as Code , CONVERT(varchar(10), TSPL_IssueReturn_HEAD.Doc_Date,103) as Date  "
            qry += " from TSPL_IssueReturn_HEAD    "
            whrcls = " not exists (select 1 from TSPL_VENDOR_INVOICE_DETAIL where TSPL_VENDOR_INVOICE_DETAIL.Invoice_No=TSPL_IssueReturn_HEAD.Doc_No AND TSPL_VENDOR_INVOICE_DETAIL.Document_No NOT IN('" + txtDocNo.Value + "')) and TSPL_IssueReturn_HEAD.Status='1' and TSPL_IssueReturn_HEAD.Doc_Type='Transfer'  "
            gv1.CurrentRow.Cells(colDocNo).Value = clsCommon.ShowSelectForm("APInvoiceFinderTR", qry, "Code", whrcls, clsCommon.myCstr(gv1.CurrentRow.Cells(colDocNo).Value), "", isButtonClick)
        ElseIf clsCommon.CompairString(DocTypeAdjustment, clsCommon.myCstr(gv1.CurrentRow.Cells(colDocType).Value)) = CompairStringResult.Equal Then
            qry = "  select TSPL_ADJUSTMENT_HEADER.Adjustment_No as Code , CONVERT(varchar(10), TSPL_ADJUSTMENT_HEADER.Adjustment_Date,103) as Date,TSPL_ADJUSTMENT_HEADER.Customer_CODE as [Customer Code],TSPL_ADJUSTMENT_HEADER.Customer_NAME as Customer,TSPL_VEHICLE_MASTER.Transport_Id "
            qry += " from TSPL_ADJUSTMENT_HEADER"
            qry += " inner join TSPL_VEHICLE_MASTER on TSPL_VEHICLE_MASTER.Vehicle_Id=TSPL_ADJUSTMENT_HEADER.Vehicle_Code "
            qry += " inner join TSPL_TRANSPORT_MASTER on TSPL_TRANSPORT_MASTER.Transport_Id=TSPL_VEHICLE_MASTER.Transport_Id "
            whrcls = " TSPL_VEHICLE_MASTER.Transport_Id='" + TxtVendorNo.Value + "' and "
            whrcls += " not exists (select 1 from TSPL_VENDOR_INVOICE_DETAIL where TSPL_VENDOR_INVOICE_DETAIL.Invoice_No=TSPL_ADJUSTMENT_HEADER.Adjustment_No AND TSPL_VENDOR_INVOICE_DETAIL.Document_No NOT IN('" + txtDocNo.Value + "')) and TSPL_ADJUSTMENT_HEADER.Posted='Y'  and TSPL_ADJUSTMENT_HEADER.ItemType='E' "
            gv1.CurrentRow.Cells(colDocNo).Value = clsCommon.ShowSelectForm("APInvoiceFinderAd", qry, "Code", whrcls, clsCommon.myCstr(gv1.CurrentRow.Cells(colDocNo).Value), "", isButtonClick)
        ElseIf clsCommon.CompairString(DocTypeSRN, clsCommon.myCstr(gv1.CurrentRow.Cells(colDocType).Value)) = CompairStringResult.Equal Then
            qry = "select TSPL_SRN_HEAD.SRN_No as Code , CONVERT(varchar(10), TSPL_SRN_HEAD.SRN_Date,103) as Date,TSPL_SRN_HEAD.Vendor_Code as [Vendor Code],TSPL_SRN_HEAD.Vendor_Name as Vendor from TSPL_SRN_HEAD  "
            whrcls = "not exists (select 1 from TSPL_VENDOR_INVOICE_DETAIL where TSPL_VENDOR_INVOICE_DETAIL.Invoice_No=TSPL_SRN_HEAD.SRN_No AND TSPL_VENDOR_INVOICE_DETAIL.Document_No NOT IN('" + txtDocNo.Value + "')) and TSPL_SRN_HEAD.Status=1   "
            gv1.CurrentRow.Cells(colDocNo).Value = clsCommon.ShowSelectForm("APInvoiceFinderSRN", qry, "Code", whrcls, clsCommon.myCstr(gv1.CurrentRow.Cells(colDocNo).Value), "", isButtonClick)
        End If

    End Sub

    Private Sub OpenAdditionCharges(ByVal isButtonClick As Boolean)
        Try
            Dim obj As clsAdditionalCharge = clsAdditionalCharge.getFinder(clsCommon.myCstr(gv1.CurrentRow.Cells(colAChgCode).Value), isButtonClick)
            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Code) > 0 Then
                gv1.CurrentRow.Cells(colAChgCode).Value = obj.Code
                gv1.CurrentRow.Cells(colAChgName).Value = obj.desc
                If clsCommon.CompairString(cmbRefType.SelectedValue, "S") = CompairStringResult.Equal Then
                    obj.Account_Code = clsERPFuncationality.ChangeGLAccountLocationSegment(obj.Account_Code, txtlocation.Value)
                End If
                gv1.CurrentRow.Cells(colACCode).Value = obj.Account_Code
                gv1.CurrentRow.Cells(colACName).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Description from TSPL_GL_ACCOUNTS WHere Account_Code='" + obj.Account_Code + "'"))
            Else
                gv1.CurrentRow.Cells(colAChgCode).Value = ""
                gv1.CurrentRow.Cells(colAChgName).Value = ""
            End If
        Catch ex As Exception
            gv1.CurrentRow.Cells(colAChgCode).Value = ""
            gv1.CurrentRow.Cells(colAChgName).Value = ""
            gv1.CurrentRow.Cells(colACCode).Value = ""
            gv1.CurrentRow.Cells(colACName).Value = ""
            Throw New Exception(ex.Message)
        End Try

    End Sub

    Private Sub gv1_UserAddedRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowEventArgs) Handles gv1.UserAddedRow
        For i As Integer = 0 To gv1.Rows.Count - 1
            gv1.Rows(0).Cells(0).Value = 1
            If i <> 0 Then
                gv1.Rows(i).Cells(colLineNo).Value = i + 1
            End If
        Next
    End Sub

    Private Sub BlankTaxDetailsCurrentRow()
        For ii As Integer = 1 To 10
            Dim strII As String = clsCommon.myCstr(ii)
            gv1.CurrentRow.Cells(clsCommon.myCstr("colTax" + strII)).Value = Nothing
            gv1.CurrentRow.Cells(clsCommon.myCstr("colTaxRate" + strII)).Value = Nothing
            gv1.CurrentRow.Cells(clsCommon.myCstr("colTaxAmt" + strII)).Value = Nothing
        Next
    End Sub

    Private Function GetCurrentRowTotalTaxAmt() As Double
        Dim dblTotTax As Double = 0
        For ii As Integer = 1 To 10
            Dim strii As String = clsCommon.myCstr(ii)
            dblTotTax = dblTotTax + clsCommon.myCdbl(gv1.CurrentRow.Cells(clsCommon.myCstr("colTaxAmt" + strii)).Value)
        Next
        Return dblTotTax
    End Function

    Private Function GetCurrentRowSurTaxAmt(ByVal IntRowNo As Integer, ByVal intEndCol As Integer, ByVal strSurTaxCode As String) As Double
        For ii As Integer = 1 To intEndCol
            Dim strii As String = clsCommon.myCstr(ii)
            If IntRowNo < 0 Then
                If clsCommon.CompairString(strSurTaxCode, clsCommon.myCstr(gv1.CurrentRow.Cells(clsCommon.myCstr("colTax" + strii)).Value)) = CompairStringResult.Equal Then
                    Return clsCommon.myCdbl(gv1.CurrentRow.Cells(clsCommon.myCstr("colTaxAmt" + strii)).Value)
                End If
            Else
                If clsCommon.CompairString(strSurTaxCode, clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("colTax" + strii)).Value)) = CompairStringResult.Equal Then
                    Return clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("colTaxAmt" + strii)).Value)
                End If
            End If
        Next
        Return 0
    End Function

    Private Function GetCurrentRowOtherTaxAmt(ByVal IntRowNo As Integer, ByVal intEndCol As Integer, ByVal arrTaxableAuth As List(Of String)) As Double
        Dim dblRet As Double = 0
        For Each strTaxAuth As String In arrTaxableAuth
            For ii As Integer = 1 To intEndCol
                Dim strii As String = clsCommon.myCstr(ii)
                If IntRowNo < 0 Then
                    If clsCommon.CompairString(strTaxAuth, clsCommon.myCstr(gv1.CurrentRow.Cells(clsCommon.myCstr("colTax" + strii)).Value)) = CompairStringResult.Equal Then
                        dblRet = dblRet + clsCommon.myCdbl(gv1.CurrentRow.Cells(clsCommon.myCstr("colTaxAmt" + strii)).Value)
                    End If
                Else
                    If clsCommon.CompairString(strTaxAuth, clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("colTax" + strii)).Value)) = CompairStringResult.Equal Then
                        dblRet = dblRet + clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("colTaxAmt" + strii)).Value)
                    End If
                End If
            Next
        Next
        Return dblRet
    End Function

    Private Sub UpdateCurrentRowWihtRowNo(ByVal intRowNo As Integer)
        Dim arrTaxableAuth As New List(Of String)
        Dim dblAmt As Double = clsCommon.myCdbl(gv1.Rows(intRowNo).Cells(colAmt).Value)
        Dim dblDisPer As Double = clsCommon.myCdbl(gv1.Rows(intRowNo).Cells(colDisPer).Value)
        Dim dblDisAmt As Double = (dblAmt * dblDisPer) / 100
        Dim dblAmtAfterDis As Double = dblAmt - dblDisAmt
        Dim isUnClaimedTax As Boolean = clsCommon.myCBool(gv1.Rows(intRowNo).Cells(colIsUnclaimedTax).Value)
        'BlankTaxDetailsCurrentRowWihtRowNo(intRowNo)

        'Dim ii As String = "1"
        For ii As Integer = 1 To 10
            Dim Strii As String = clsCommon.myCstr(ii)
            If rbtnTaxCalAutomatic.IsChecked Then
                Dim strTaxCode As String = clsCommon.myCstr(gv1.Rows(intRowNo).Cells(clsCommon.myCstr("COLTAX" + Strii)).Value)
                If clsCommon.myLen(strTaxCode) > 0 Then
                    Dim dblTaxRate As Double = clsCommon.myCdbl(gv1.Rows(intRowNo).Cells(clsCommon.myCstr("COLTAXRATE" + Strii)).Value)
                    Dim IsSurTax As Boolean = clsCommon.myCBool(gv1.Rows(intRowNo).Cells(clsCommon.myCstr("ISSURTAX" + Strii)).Value)
                    Dim strSurTaxCode As String = clsCommon.myCstr(gv1.Rows(intRowNo).Cells(clsCommon.myCstr("SURTAXCODE" + Strii)).Value)
                    Dim IsTaxable As Boolean = clsCommon.myCBool(gv1.Rows(intRowNo).Cells(clsCommon.myCstr("ISTAXABLE" + Strii)).Value)
                    ''Dim IsExcisable As Boolean = clsCommon.myCBool(gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("ISEXCISABLE" + Strii)).Value)
                    Dim dblBaseAmt As Double = 0
                    Dim dblTaxAmt As Double = 0
                    If IsSurTax Then
                        Dim dblSurTaxAmt As Double = GetCurrentRowSurTaxAmt(intRowNo, ii, strSurTaxCode)
                        dblBaseAmt = dblSurTaxAmt
                    Else
                        Dim dblOtherTaxAmt As Double = 0
                        ''If IsTaxable Then
                        dblOtherTaxAmt = GetCurrentRowOtherTaxAmt(intRowNo, Strii, arrTaxableAuth)
                        ''End If
                        ''If IsExcisable Then
                        ''    dblBaseAmt = (dblAssessableAmt + dblOtherTaxAmt)
                        ''Else
                        dblBaseAmt = (dblAmtAfterDis + dblOtherTaxAmt)
                        ''End If
                    End If
                    gv1.Rows(intRowNo).Cells(clsCommon.myCstr("COLTAXBASEAMT" + Strii)).Value = Math.Round(dblBaseAmt, 2)
                    dblTaxAmt = (dblBaseAmt * dblTaxRate) / 100
                    gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTaxAmt" + Strii)).Value = Math.Round(dblTaxAmt, 2)
                    If IsTaxable AndAlso Not arrTaxableAuth.Contains(strTaxCode.ToUpper()) Then
                        arrTaxableAuth.Add(strTaxCode.ToUpper())
                    End If
                Else
                    gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTax" + Strii)).Value = Nothing
                    gv1.Rows(intRowNo).Cells(clsCommon.myCstr("COLTAXBASEAMT" + Strii)).Value = Nothing
                    gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTaxRate" + Strii)).Value = Nothing
                    gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTaxAmt" + Strii)).Value = Nothing
                    gv1.Rows(intRowNo).Cells(clsCommon.myCstr("ISSURTAX" + Strii)).Value = Nothing
                    gv1.Rows(intRowNo).Cells(clsCommon.myCstr("SURTAXCODE" + Strii)).Value = Nothing
                    gv1.Rows(intRowNo).Cells(clsCommon.myCstr("ISTAXABLE" + Strii)).Value = Nothing
                    gv1.Rows(intRowNo).Cells(clsCommon.myCstr("ISEXCISABLE" + Strii)).Value = Nothing
                End If
            ElseIf rbtnTaxCalManual.IsChecked Then
                If gv2.Rows.Count >= ii Then
                    Dim dblTaxAmt As Double = clsCommon.myCdbl(gv2.Rows(ii - 1).Cells(colTTaxAmt).Value)
                    Dim dblTaxRate As Double = clsCommon.myCdbl(gv2.Rows(ii - 1).Cells(colTTaxRate).Value)
                    Dim dblCurrRowAmt As Double = clsCommon.myCdbl(gv1.Rows(clsCommon.myCdbl(intRowNo)).Cells(colAmt).Value)
                    Dim dblTotAmt As Double = 0
                    For jj As Integer = 0 To gv1.Rows.Count - 1
                        dblTotAmt += clsCommon.myCdbl(gv1.Rows(jj).Cells(colAmt).Value)
                    Next
                    Dim dblCurrCalTax As Double = 0
                    If dblTotAmt <> 0 Then
                        dblCurrCalTax = Math.Round(clsCommon.myCdbl(dblTaxAmt * dblCurrRowAmt / dblTotAmt), 2, MidpointRounding.ToEven)
                    End If
                    gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTaxAmt" + Strii)).Value = dblCurrCalTax
                    gv1.Rows(intRowNo).Cells(clsCommon.myCstr("COLTAXRATE" + Strii)).Value = dblTaxRate
                End If
            End If
        Next

        Dim dblTotTaxAmt As Double = GetCurrentRowTotalTaxAmtWihtRowNo(intRowNo)
        Dim dblAmtAfterTax As Double = dblAmtAfterDis + dblTotTaxAmt
        gv1.Rows(intRowNo).Cells(colDisAmt).Value = dblDisAmt
        gv1.Rows(intRowNo).Cells(colAmtAfterDis).Value = dblAmtAfterDis
        gv1.Rows(intRowNo).Cells(colTotTaxAmt).Value = dblTotTaxAmt
        gv1.Rows(intRowNo).Cells(colAmtAfterTax).Value = dblAmtAfterTax
    End Sub

    Private Function GetCurrentRowOtherTaxAmtWihtRowNo(ByVal intRowNo As Integer, ByVal intEndCol As Integer, ByVal arrTaxableAuth As List(Of String)) As Double
        Dim dblRet As Double = 0
        For Each strTaxAuth As String In arrTaxableAuth
            For ii As Integer = 1 To intEndCol
                Dim strii As String = clsCommon.myCstr(ii)
                If clsCommon.CompairString(strTaxAuth, clsCommon.myCstr(gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTax" + strii)).Value)) = CompairStringResult.Equal Then
                    dblRet = dblRet + clsCommon.myCdbl(gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTaxAmt" + strii)).Value)
                End If
            Next
        Next
        Return dblRet
    End Function

    Private Sub BlankTaxDetailsCurrentRowWihtRowNo(ByVal intRowNo As Integer)
        For ii As Integer = 1 To 10
            Dim strII As String = clsCommon.myCstr(ii)
            gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTax" + strII)).Value = Nothing
            gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTaxRate" + strII)).Value = Nothing
            gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTaxAmt" + strII)).Value = Nothing
        Next
    End Sub

    Private Function GetCurrentRowSurTaxAmtWihtRowNo(ByVal intRowNo As Integer, ByVal intEndCol As Integer, ByVal strSurTaxCode As String) As Double
        For ii As Integer = 1 To intEndCol
            Dim strii As String = clsCommon.myCstr(ii)
            If clsCommon.CompairString(strSurTaxCode, clsCommon.myCstr(gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTax" + strii)).Value)) = CompairStringResult.Equal Then
                Return clsCommon.myCdbl(gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTaxAmt" + strii)).Value)
            End If
        Next
        Return 0
    End Function

    Private Function GetCurrentRowTotalTaxAmtWihtRowNo(ByVal intRowNo As Integer) As Double
        Dim dblTotTax As Double = 0
        For ii As Integer = 1 To 10
            Dim strii As String = clsCommon.myCstr(ii)
            dblTotTax = dblTotTax + clsCommon.myCdbl(gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTaxAmt" + strii)).Value)
        Next
        Return dblTotTax
    End Function

    Private Sub UpdateAllTotals()
        Dim dblTotAmt As Double = 0
        Dim dblTotDisAmt As Double = 0
        Dim dblAmtAfterDis As Double = 0

        Dim dblTaxBaseAmt1 As Double = 0
        Dim dblTaxBaseAmt2 As Double = 0
        Dim dblTaxBaseAmt3 As Double = 0
        Dim dblTaxBaseAmt4 As Double = 0
        Dim dblTaxBaseAmt5 As Double = 0
        Dim dblTaxBaseAmt6 As Double = 0
        Dim dblTaxBaseAmt7 As Double = 0
        Dim dblTaxBaseAmt8 As Double = 0
        Dim dblTaxBaseAmt9 As Double = 0
        Dim dblTaxBaseAmt10 As Double = 0


        Dim dblTaxAmt1 As Double = 0
        Dim dblTaxAmt2 As Double = 0
        Dim dblTaxAmt3 As Double = 0
        Dim dblTaxAmt4 As Double = 0
        Dim dblTaxAmt5 As Double = 0
        Dim dblTaxAmt6 As Double = 0
        Dim dblTaxAmt7 As Double = 0
        Dim dblTaxAmt8 As Double = 0
        Dim dblTaxAmt9 As Double = 0
        Dim dblTaxAmt10 As Double = 0
        Dim dblTaxTotAmt As Double = 0
        Dim dblNetAmt As Double = 0
        Dim dblLandedAmt As Double = 0
        For ii As Integer = 0 To gv1.Rows.Count - 1
            If (clsCommon.myLen(gv1.Rows(ii).Cells(colACCode).Value) > 0) Then
                dblTotAmt = dblTotAmt + clsCommon.myCdbl(gv1.Rows(ii).Cells(colAmt).Value)
                dblTotDisAmt = dblTotDisAmt + clsCommon.myCdbl(gv1.Rows(ii).Cells(colDisAmt).Value)
                dblAmtAfterDis = dblAmtAfterDis + clsCommon.myCdbl(gv1.Rows(ii).Cells(colAmtAfterDis).Value)
                dblTaxAmt1 = dblTaxAmt1 + clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxAmt1).Value)
                dblTaxAmt2 = dblTaxAmt2 + clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxAmt2).Value)
                dblTaxAmt3 = dblTaxAmt3 + clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxAmt3).Value)
                dblTaxAmt4 = dblTaxAmt4 + clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxAmt4).Value)
                dblTaxAmt5 = dblTaxAmt5 + clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxAmt5).Value)
                dblTaxAmt6 = dblTaxAmt6 + clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxAmt6).Value)
                dblTaxAmt7 = dblTaxAmt7 + clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxAmt7).Value)
                dblTaxAmt8 = dblTaxAmt8 + clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxAmt8).Value)
                dblTaxAmt9 = dblTaxAmt9 + clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxAmt9).Value)
                dblTaxAmt10 = dblTaxAmt10 + clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxAmt10).Value)

                dblTaxBaseAmt1 = dblTaxBaseAmt1 + clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxBaseAmt1).Value)
                dblTaxBaseAmt2 = dblTaxBaseAmt2 + clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxBaseAmt2).Value)
                dblTaxBaseAmt3 = dblTaxBaseAmt3 + clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxBaseAmt3).Value)
                dblTaxBaseAmt4 = dblTaxBaseAmt4 + clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxBaseAmt4).Value)
                dblTaxBaseAmt5 = dblTaxBaseAmt5 + clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxBaseAmt5).Value)
                dblTaxBaseAmt6 = dblTaxBaseAmt6 + clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxBaseAmt6).Value)
                dblTaxBaseAmt7 = dblTaxBaseAmt7 + clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxBaseAmt7).Value)
                dblTaxBaseAmt8 = dblTaxBaseAmt8 + clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxBaseAmt8).Value)
                dblTaxBaseAmt9 = dblTaxBaseAmt9 + clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxBaseAmt9).Value)
                dblTaxBaseAmt10 = dblTaxBaseAmt10 + clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxBaseAmt10).Value)

                dblTaxTotAmt = dblTaxTotAmt + clsCommon.myCdbl(gv1.Rows(ii).Cells(colTotTaxAmt).Value)
                dblNetAmt = dblNetAmt + clsCommon.myCdbl(gv1.Rows(ii).Cells(colAmtAfterTax).Value)
                dblLandedAmt += clsCommon.myCdbl(gv1.Rows(ii).Cells(colLandedAmt).Value)
            End If
        Next

        If rbtnTaxCalAutomatic.IsChecked Then
            For ii As Integer = 1 To gv2.Rows.Count
                Select Case (ii)
                    Case 1
                        gv2.Rows(ii - 1).Cells(colTTaxAmt).Value = Math.Round(dblTaxAmt1, 2)
                        gv2.Rows(ii - 1).Cells(colTBaseAmt).Value = Math.Round(dblTaxBaseAmt1, 2)
                        If dblTaxBaseAmt1 <> 0 Then
                            gv2.Rows(ii - 1).Cells(colTTaxRate).Value = Math.Round((dblTaxAmt1 * 100) / dblTaxBaseAmt1, 2)
                        Else
                            gv2.Rows(ii - 1).Cells(colTTaxRate).Value = 0
                        End If
                    Case 2
                        gv2.Rows(ii - 1).Cells(colTTaxAmt).Value = Math.Round(dblTaxAmt2, 2)
                        gv2.Rows(ii - 1).Cells(colTBaseAmt).Value = Math.Round(dblTaxBaseAmt2, 2)
                        If dblTaxBaseAmt2 <> 0 Then
                            gv2.Rows(ii - 1).Cells(colTTaxRate).Value = Math.Round((dblTaxAmt2 * 100) / dblTaxBaseAmt2, 2)
                        Else
                            gv2.Rows(ii - 1).Cells(colTTaxRate).Value = 0
                        End If
                    Case 3
                        gv2.Rows(ii - 1).Cells(colTTaxAmt).Value = Math.Round(dblTaxAmt3, 2)
                        gv2.Rows(ii - 1).Cells(colTBaseAmt).Value = Math.Round(dblTaxBaseAmt3, 2)
                        If dblTaxBaseAmt3 <> 0 Then
                            gv2.Rows(ii - 1).Cells(colTTaxRate).Value = Math.Round((dblTaxAmt3 * 100) / dblTaxBaseAmt3, 2)
                        Else
                            gv2.Rows(ii - 1).Cells(colTTaxRate).Value = 0
                        End If
                    Case 4
                        gv2.Rows(ii - 1).Cells(colTTaxAmt).Value = Math.Round(dblTaxAmt4, 2)
                        gv2.Rows(ii - 1).Cells(colTBaseAmt).Value = Math.Round(dblTaxBaseAmt4, 2)
                        If dblTaxBaseAmt4 <> 0 Then
                            gv2.Rows(ii - 1).Cells(colTTaxRate).Value = Math.Round((dblTaxAmt4 * 100) / dblTaxBaseAmt4, 2)
                        Else
                            gv2.Rows(ii - 1).Cells(colTTaxRate).Value = 0
                        End If
                    Case 5
                        gv2.Rows(ii - 1).Cells(colTTaxAmt).Value = Math.Round(dblTaxAmt5, 2)
                        gv2.Rows(ii - 1).Cells(colTBaseAmt).Value = Math.Round(dblTaxBaseAmt5, 2)
                        If dblTaxBaseAmt5 <> 0 Then
                            gv2.Rows(ii - 1).Cells(colTTaxRate).Value = Math.Round((dblTaxAmt5 * 100) / dblTaxBaseAmt5, 2)
                        Else
                            gv2.Rows(ii - 1).Cells(colTTaxRate).Value = 0
                        End If
                    Case 6
                        gv2.Rows(ii - 1).Cells(colTTaxAmt).Value = Math.Round(dblTaxAmt6, 2)
                        gv2.Rows(ii - 1).Cells(colTBaseAmt).Value = Math.Round(dblTaxBaseAmt6, 2)
                        If dblTaxBaseAmt6 <> 0 Then
                            gv2.Rows(ii - 1).Cells(colTTaxRate).Value = Math.Round((dblTaxAmt6 * 100) / dblTaxBaseAmt6, 2)
                        Else
                            gv2.Rows(ii - 1).Cells(colTTaxRate).Value = 0
                        End If
                    Case 7
                        gv2.Rows(ii - 1).Cells(colTTaxAmt).Value = Math.Round(dblTaxAmt7, 2)
                        gv2.Rows(ii - 1).Cells(colTBaseAmt).Value = Math.Round(dblTaxBaseAmt7, 2)
                        If dblTaxBaseAmt7 <> 0 Then
                            gv2.Rows(ii - 1).Cells(colTTaxRate).Value = Math.Round((dblTaxAmt7 * 100) / dblTaxBaseAmt7, 2)
                        Else
                            gv2.Rows(ii - 1).Cells(colTTaxRate).Value = 0
                        End If
                    Case 8
                        gv2.Rows(ii - 1).Cells(colTTaxAmt).Value = Math.Round(dblTaxAmt8, 2)
                        gv2.Rows(ii - 1).Cells(colTBaseAmt).Value = Math.Round(dblTaxBaseAmt8, 2)
                        If dblTaxBaseAmt8 <> 0 Then
                            gv2.Rows(ii - 1).Cells(colTTaxRate).Value = Math.Round((dblTaxAmt8 * 100) / dblTaxBaseAmt8, 2)
                        Else
                            gv2.Rows(ii - 1).Cells(colTTaxRate).Value = 0
                        End If
                    Case 9
                        gv2.Rows(ii - 1).Cells(colTTaxAmt).Value = Math.Round(dblTaxAmt9, 2)
                        gv2.Rows(ii - 1).Cells(colTBaseAmt).Value = Math.Round(dblTaxBaseAmt9, 2)
                        If dblTaxBaseAmt9 <> 0 Then
                            gv2.Rows(ii - 1).Cells(colTTaxRate).Value = Math.Round((dblTaxAmt9 * 100) / dblTaxBaseAmt9, 2)
                        Else
                            gv2.Rows(ii - 1).Cells(colTTaxRate).Value = 0
                        End If
                    Case 10
                        gv2.Rows(ii - 1).Cells(colTTaxAmt).Value = Math.Round(dblTaxAmt10, 2)
                        gv2.Rows(ii - 1).Cells(colTBaseAmt).Value = Math.Round(dblTaxBaseAmt10, 2)
                        If dblTaxBaseAmt10 <> 0 Then
                            gv2.Rows(ii - 1).Cells(colTTaxRate).Value = Math.Round((dblTaxAmt10 * 100) / dblTaxBaseAmt10, 2)
                        Else
                            gv2.Rows(ii - 1).Cells(colTTaxRate).Value = 0
                        End If
                End Select
            Next
        End If

        Dim dblACAmount As Double = 0
        For ii As Integer = 0 To gvAC.Rows.Count - 1
            If (clsCommon.myLen(gvAC.Rows(ii).Cells(colAChgCode).Value) > 0) Then
                dblACAmount = dblACAmount + clsCommon.myCdbl(gvAC.Rows(ii).Cells(colAChgAmount).Value)
            End If
        Next

        lblAmtWithDiscount.Text = clsCommon.myFormat(dblTotAmt)
        lblDiscountAmt.Text = clsCommon.myFormat(dblTotDisAmt)
        lblAmtAfterDiscount.Text = clsCommon.myFormat(dblAmtAfterDis)
        lblTaxAmt.Text = clsCommon.myFormat(dblTaxTotAmt)

        lblAddCharges.Text = clsCommon.myFormat(dblACAmount)
        lblAddCharges1.Text = clsCommon.myFormat(dblACAmount)
        lblLandedAmt.Text = clsCommon.myFormat(dblLandedAmt)
        dblNetAmt = dblNetAmt + dblACAmount + dblLandedAmt
        Dim dclROAmt As Decimal = 0
        If SettingAutoRoundOffSeprateAccountOnVendorTransaction Then
            dclROAmt = Math.Round(dblNetAmt, 0, MidpointRounding.AwayFromZero) - dblNetAmt
            dblNetAmt = Math.Round(dblNetAmt, 0, MidpointRounding.AwayFromZero)
        End If
        lblRoundOff.Text = clsCommon.myFormat(dclROAmt)
        lblTotRAmt.Text = clsCommon.myFormat(dblNetAmt)
        lblTotRAmt1.Text = clsCommon.myFormat(dblNetAmt)
    End Sub

    Private Function GetBaseTaxAmount(ByVal intEndCol As Integer, ByVal strSurTaxCode As String) As Double

        For ii As Integer = 0 To intEndCol
            If clsCommon.CompairString(strSurTaxCode, clsCommon.myCstr(gv2.Rows(ii).Cells(colTTaxAutCode).Value)) = CompairStringResult.Equal Then
                Return clsCommon.myCdbl(clsCommon.myCstr(gv2.Rows(ii).Cells(colTTaxAmt).Value))
            End If
        Next
        Return 0
    End Function

    Private Sub btnAddNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddNew.Click
        AddNew()

    End Sub

    Sub AddNew()
        txtlocation.Value = ""
        txtlocation.Enabled = True
        BlankAllControls()
        LoadBlankGridGL()
        LoadBlankGridTax()
        LoadBlankGridAC()
        'cboDocType.Enabled = False
        isNewEntry = True
        btnSave.Text = "Save"
        txtDocNo.MyReadOnly = False
        btnViewTDSDetails.Enabled = False
        txtDate.Enabled = True
        btnSave.Enabled = True
        btnPost.Enabled = True
        btnDelete.Enabled = True
        gv1.Rows.AddNew()
        gvAC.Rows.AddNew()
        cmbRefType.SelectedIndex = 1
        cmbRefType.SelectedIndex = 0
        btnPrint.Visible = True
        ''richa agarwal
        txtRefDocNo.Visible = True
        txtPONo.Enabled = True
        txtVendorInvoiceNo.Enabled = True
        RadLabel15.Visible = True
        ''--------------
        FillVendorDetails()
    End Sub

    Function AllowToSave() As Boolean
        If AllowFutureDateTransaction(txtDate.Value, Nothing) = False Then
            txtDate.Focus()
            Return False
        End If

        ' If clsCommon.myLen(obj.Posting_Date) > 0 Then
        If btnSave.Text = "Update" Then
            Dim strchk As String = "select Posting_Date from TSPL_VENDOR_INVOICE_HEAD where Document_No='" + txtDocNo.Value + "'"
            Dim chkpost As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strchk))
            If clsCommon.myLen(chkpost) > 0 Then
                clsCommon.MyMessageBoxShow(Me, "Transaction already posted", Me.Text)
                Return False
            End If
        End If
        For ii As Integer = 0 To gv1.Rows.Count - 1
            UpdateCurrentRowWihtRowNo(ii)
        Next

        UpdateAllTotals()
        If Not objRemittance Is Nothing Then
            UpdateTDSAmount()
        End If

        If clsCommon.myLen(txtlocation.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "Please first select Location", Me.Text)
            txtlocation.Focus()
            Return False
        End If
        ' '' Added BY abhishek as on 1 Dec 2012 For Lock Transaction Location Wise
        'Dim LocSeg As String = txtlocation.Value
        'Dim qry1 As String = " select Description  from TSPL_GL_SEGMENT_CODE  where Seg_No ='7' and  Segment_code ='" + LocSeg + "'"
        'Dim LocSegName As String = clsDBFuncationality.getSingleValue(qry1)

        'Dim LockDate As String = clsERPFuncationality.ValidateLocationSegment(objCommonVar.CurrentCompanyCode, "Payables", "AP Invoice Entry", LocSeg, clsCommon.GetPrintDate(txtDate.Value, "dd/MM/yyyy"))
        'If clsCommon.myLen(LockDate) > 0 Then
        '    common.clsCommon.MyMessageBoxShow("Transaction is Locked For Location " + LocSegName + " from " + LockDate + "")
        '    Return False


        'End If

        ''Code Ends Here



        If clsCommon.myLen(TxtVendorNo.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "Please select Vendor", Me.Text)
            TxtVendorNo.Focus()
            Return False
        End If
        If clsCommon.myLen(txtACSet.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "Please select Vendor Account Set", Me.Text)
            txtACSet.Focus()
            Return False
        End If
        If clsCommon.CompairString(cboDocType.SelectedValue, "D") <> CompairStringResult.Equal Then
            If clsCommon.myLen(txtVendorInvoiceNo.Text) <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Please Enter Vendor Invoice No", Me.Text)
                txtVendorInvoiceNo.Focus()
                Return False
            End If
        End If

        'If clsCommon.myLen(txtTaxGroup.Value) <= 0 Then
        '    common.clsCommon.MyMessageBoxShow("Please select Tax Group")
        '    txtTaxGroup.Focus()
        '    Return False
        'End If
        If clsCommon.myLen(txtVendorInvoiceNo.Text) > 0 Then
            Dim dt As DataTable = clsDBFuncationality.GetDataTable("select Document_No from TSPL_VENDOR_INVOICE_HEAD where Vendor_Invoice_No='" + txtVendorInvoiceNo.Text + "' and Vendor_Code='" + TxtVendorNo.Value + "' and Document_No not in('" + txtDocNo.Value + "')")
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                common.clsCommon.MyMessageBoxShow("Vendor Invoice No:" + txtVendorInvoiceNo.Text + " Already used in Document No: " + clsCommon.myCstr(dt.Rows(0)("Document_No")))
                txtVendorInvoiceNo.Focus()
                Return False
            End If
        End If
        If clsCommon.GetDateWithStartTime(txtVendorInvDatre.Value) > clsCommon.GetDateWithEndTime(txtDate.Value) Then
            common.clsCommon.MyMessageBoxShow(Me, "Vendor Invoice Date can't be Greate then Document Date", Me.Text)
            Return False
        End If
        If clsCommon.myLen(cmbRefType.SelectedValue) > 0 AndAlso clsCommon.myLen(txtRefDocNo.Value) <= 0 AndAlso clsCommon.CompairString(cmbRefType.SelectedValue, "A") <> CompairStringResult.Equal Then

            common.clsCommon.MyMessageBoxShow(Me, "Please select Ref Document No", Me.Text)
            txtRefDocNo.Focus()
            Return False
        End If
        'For ii As Integer = 0 To gv1.Rows.Count - 1
        '    Dim strIIGLAccount As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colACCode).Value)
        '    Dim strIIGLAccountName As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colACName).Value)
        '    For jj As Integer = 0 To gv1.Rows.Count - 1
        '        If (ii = jj) Then
        '            Continue For
        '        End If
        '        If (clsCommon.CompairString(strIIGLAccount, clsCommon.myCstr(gv1.Rows(jj).Cells(colACCode).Value)) = CompairStringResult.Equal) Then
        '            common.clsCommon.MyMessageBoxShow("Already selected GL Account " + strIIGLAccount.Trim() + "( " + strIIGLAccountName.Trim() + " ) At Line No: " + clsCommon.myCstr(clsCommon.myCdbl(ii + 1)) + " and  " + clsCommon.myCstr(clsCommon.myCdbl(jj + 1)) + "")
        '            Return False
        '        End If
        '    Next
        'Next
        ''Dim arrInvNo As New List(Of String)
        ''For ii As Integer = 0 To gv1.Rows.Count - 1
        ''    Dim strInvNo As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colDocNo).Value)
        ''    If clsCommon.myLen(strInvNo) > 0 Then
        ''        If arrInvNo.Contains(strInvNo) Then
        ''            common.clsCommon.MyMessageBoxShow("Invoice No " + strInvNo + ".Repeated at row no" + clsCommon.myCstr(ii + 1))
        ''            Return False
        ''        End If
        ''        arrInvNo.Add(strInvNo)
        ''    End If
        ''Next

        ''''-------- Added By Abhishek for Row By Row check If Doc type and Doc No HAs same value as on 28 june 2012 
        Dim l As Integer
        Dim k As Integer
        For k = 0 To gv1.Rows.Count - 1
            ''richa agarwal
            If clsCommon.CompairString(cmbRefType.SelectedValue, "A") = CompairStringResult.Equal Then
                If clsCommon.myLen(gv1.Rows(l).Cells(colRefDocNo).Value) <= 0 Then
                    common.clsCommon.MyMessageBoxShow(Me, "Please Select Ref Document No.", Me.Text)
                    Return False
                End If
            End If
            ''---------------
            If clsCommon.myLen(gv1.Rows(k).Cells(colDocType).Value) > 0 AndAlso clsCommon.myLen(gv1.Rows(k).Cells(colDocNo).Value) > 0 Then
                Dim DocType As String = clsCommon.myCstr(gv1.Rows(k).Cells(colDocType).Value)
                Dim DocNo As String = clsCommon.myCstr(gv1.Rows(k).Cells(colDocNo).Value)
                ' Dim PaydocNo As String = clsCommon.myCstr(gv1.Rows(k).Cells(colRefDocNo).Value)
                For l = k + 1 To gv1.Rows.Count - 1
                    Dim NextDocType As String = clsCommon.myCstr(gv1.Rows(l).Cells(colDocType).Value)
                    Dim NextDocNo As String = clsCommon.myCstr(gv1.Rows(l).Cells(colDocNo).Value)
                    '  Dim NextPaydocNo As String = clsCommon.myCstr(gv1.Rows(l).Cells(colRefDocNo).Value)
                    If DocType = NextDocType AndAlso DocNo = NextDocNo Then
                        common.clsCommon.MyMessageBoxShow(" DocType : " + DocType + "  and  DocNo : " + DocNo + " Should not be same in next row ")
                        Return False
                        'ElseIf PaydocNo = NextPaydocNo Then
                        '    common.clsCommon.MyMessageBoxShow(" Ref Document No. " + DocNo + " Should not be same in next row ")
                        '    Return False
                    Else
                        Continue For
                    End If
                Next

            End If
        Next
        For ii As Integer = 0 To gvAC.Rows.Count - 1
            If clsCommon.myLen(gvAC.Rows(ii).Cells(colAChgCode).Value) > 0 Then
                For jj As Integer = 0 To gvAC.Rows.Count - 1
                    If ii = jj Then
                        Continue For
                    End If
                    If clsCommon.CompairString(clsCommon.myCstr(gvAC.Rows(ii).Cells(colAChgCode).Value), clsCommon.myCstr(gvAC.Rows(jj).Cells(colAChgCode).Value)) = CompairStringResult.Equal Then
                        common.clsCommon.MyMessageBoxShow(Me, "Additional Charges: " + clsCommon.myCstr(gvAC.Rows(ii).Cells(colAChgCode).Value) + "Repeated at Row No " + clsCommon.myCstr(ii + 1) + " and " + clsCommon.myCstr(jj + 1) + "")
                        Return False
                    End If
                Next
            End If
        Next
        Dim isFirstTime As Boolean = True
        Dim strFirstLocSeg As String = ""
        For ii As Integer = 0 To gv1.Rows.Count - 1
            Dim strACode As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colACCode).Value)
            If clsCommon.myLen(strACode) > 0 Then
                If (isFirstTime) Then
                    strFirstLocSeg = strACode.Substring(strACode.Length - 3, 3)
                    isFirstTime = False
                End If
                Dim strCurrLocSeg As String = strACode.Substring(strACode.Length - 3, 3)
                If Not clsCommon.CompairString(strCurrLocSeg, strFirstLocSeg) = CompairStringResult.Equal Then
                    common.clsCommon.MyMessageBoxShow(Me, "Location segment should be same for all the GL Accounts", Me.Text)
                    Return False
                End If
            End If
        Next
        If clsCommon.CompairString(clsCommon.myCstr(cmbRefType.SelectedValue), "AP") = CompairStringResult.Equal Then
            If clsCommon.CompairString(clsCommon.myCstr(cboDocType.SelectedValue), "I") = CompairStringResult.Equal Then
                common.clsCommon.MyMessageBoxShow(Me, "Can't Create AP Invoice against AP Invoice", Me.Text)
                Return False
            End If
            Dim qry As String = "select Vendor_Code from TSPL_VENDOR_INVOICE_HEAD where Document_No='" + txtRefDocNo.Value + "' "
            Dim strRefVendorCode As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))

            If Not clsCommon.CompairString(strRefVendorCode, TxtVendorNo.Value) = CompairStringResult.Equal Then
                common.clsCommon.MyMessageBoxShow(Me, "Reference Document's Vendor:" + strRefVendorCode + " and Document Vendor" + TxtVendorNo.Value, Me.Text)
                Return False
            End If

            Dim dblDocAmt As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select Amount_Less_Discount from TSPL_VENDOR_INVOICE_HEAD where Document_No ='" & clsCommon.myCstr(txtRefDocNo.Value) & "'"))
            If clsCommon.myCdbl(gv1.Rows(0).Cells(colDocRefAmount).Value) > dblDocAmt Then
                common.clsCommon.MyMessageBoxShow(Me, "Taxable Amount cannot be greater than " & dblDocAmt, Me.Text)
                Return False
            End If


        End If
        Return True
    End Function

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        SaveData()

    End Sub

    Sub SaveData()
        Try
            If (AllowToSave()) Then
                Dim obj As New clsVedorInvoiceHead()

                obj.Document_No = txtDocNo.Value
                obj.Invoice_Entry_Date = txtDate.Value
                obj.Vendor_Code = TxtVendorNo.Value
                obj.Vendor_Name = lblVendorName.Text
                obj.Vendor_Invoice_No = txtVendorInvoiceNo.Text
                obj.Vendor_Invoice_Date = txtVendorInvDatre.Value
                obj.Account_Set = txtACSet.Value
                obj.Document_Type = clsCommon.myCstr(cboDocType.SelectedValue)
                obj.PO_Number = txtPONo.Text
                '---------added by  usha
                obj.loc_code = txtlocation.Value
                '---------end
                ''added by priti
                obj.RefDocType = clsCommon.myCstr(cmbRefType.SelectedValue)
                obj.RefDocNo = txtRefDocNo.Value
                '' priti ends here
                obj.Order_No = txtOrderNo.Text
                obj.Total_Tax = clsCommon.myCdbl(lblTaxAmt.Text)

                obj.On_Hold = chkOnHold.Checked
                obj.Description = txtDesc.Text
                obj.Tax_Group = txtTaxGroup.Value
                If (gv2.Rows.Count > 0) Then
                    obj.TAX1 = clsCommon.myCstr(gv2.Rows(0).Cells(colTTaxAutCode).Value)

                    obj.TAX1_Rate = clsCommon.myCdbl(gv2.Rows(0).Cells(colTTaxRate).Value)
                    obj.Tax1_BAmount = clsCommon.myCdbl(gv2.Rows(0).Cells(colTBaseAmt).Value)
                    obj.TAX1_Amt = clsCommon.myCdbl(gv2.Rows(0).Cells(colTTaxAmt).Value)
                End If
                If (gv2.Rows.Count > 1) Then
                    obj.TAX2 = clsCommon.myCstr(gv2.Rows(1).Cells(colTTaxAutCode).Value)

                    obj.TAX2_Rate = clsCommon.myCdbl(gv2.Rows(1).Cells(colTTaxRate).Value)
                    obj.Tax2_BAmount = clsCommon.myCdbl(gv2.Rows(1).Cells(colTBaseAmt).Value)
                    obj.TAX2_Amt = clsCommon.myCdbl(gv2.Rows(1).Cells(colTTaxAmt).Value)
                End If
                If (gv2.Rows.Count > 2) Then
                    obj.TAX3 = clsCommon.myCstr(gv2.Rows(2).Cells(colTTaxAutCode).Value)

                    obj.TAX3_Rate = clsCommon.myCdbl(gv2.Rows(2).Cells(colTTaxRate).Value)
                    obj.Tax3_BAmount = clsCommon.myCdbl(gv2.Rows(2).Cells(colTBaseAmt).Value)
                    obj.TAX3_Amt = clsCommon.myCdbl(gv2.Rows(2).Cells(colTTaxAmt).Value)
                End If
                If (gv2.Rows.Count > 3) Then
                    obj.TAX4 = clsCommon.myCstr(gv2.Rows(3).Cells(colTTaxAutCode).Value)

                    obj.TAX4_Rate = clsCommon.myCdbl(gv2.Rows(3).Cells(colTTaxRate).Value)
                    obj.Tax4_BAmount = clsCommon.myCdbl(gv2.Rows(3).Cells(colTBaseAmt).Value)
                    obj.TAX4_Amt = clsCommon.myCdbl(gv2.Rows(3).Cells(colTTaxAmt).Value)
                End If
                If (gv2.Rows.Count > 4) Then
                    obj.TAX5 = clsCommon.myCstr(gv2.Rows(4).Cells(colTTaxAutCode).Value)

                    obj.TAX5_Rate = clsCommon.myCdbl(gv2.Rows(4).Cells(colTTaxRate).Value)
                    obj.Tax5_BAmount = clsCommon.myCdbl(gv2.Rows(4).Cells(colTBaseAmt).Value)
                    obj.TAX5_Amt = clsCommon.myCdbl(gv2.Rows(4).Cells(colTTaxAmt).Value)
                End If
                If (gv2.Rows.Count > 5) Then
                    obj.TAX6 = clsCommon.myCstr(gv2.Rows(5).Cells(colTTaxAutCode).Value)

                    obj.TAX6_Rate = clsCommon.myCdbl(gv2.Rows(5).Cells(colTTaxRate).Value)
                    obj.Tax6_BAmount = clsCommon.myCdbl(gv2.Rows(5).Cells(colTBaseAmt).Value)
                    obj.TAX6_Amt = clsCommon.myCdbl(gv2.Rows(5).Cells(colTTaxAmt).Value)
                End If
                If (gv2.Rows.Count > 6) Then
                    obj.TAX7 = clsCommon.myCstr(gv2.Rows(6).Cells(colTTaxAutCode).Value)

                    obj.TAX7_Rate = clsCommon.myCdbl(gv2.Rows(6).Cells(colTTaxRate).Value)
                    obj.Tax7_BAmount = clsCommon.myCdbl(gv2.Rows(6).Cells(colTBaseAmt).Value)
                    obj.TAX7_Amt = clsCommon.myCdbl(gv2.Rows(6).Cells(colTTaxAmt).Value)
                End If
                If (gv2.Rows.Count > 7) Then
                    obj.TAX8 = clsCommon.myCstr(gv2.Rows(7).Cells(colTTaxAutCode).Value)

                    obj.TAX8_Rate = clsCommon.myCdbl(gv2.Rows(7).Cells(colTTaxRate).Value)
                    obj.Tax8_BAmount = clsCommon.myCdbl(gv2.Rows(7).Cells(colTBaseAmt).Value)
                    obj.TAX8_Amt = clsCommon.myCdbl(gv2.Rows(7).Cells(colTTaxAmt).Value)
                End If
                If (gv2.Rows.Count > 8) Then
                    obj.TAX9 = clsCommon.myCstr(gv2.Rows(8).Cells(colTTaxAutCode).Value)

                    obj.TAX9_Rate = clsCommon.myCdbl(gv2.Rows(8).Cells(colTTaxRate).Value)
                    obj.Tax9_BAmount = clsCommon.myCdbl(gv2.Rows(8).Cells(colTBaseAmt).Value)
                    obj.TAX9_Amt = clsCommon.myCdbl(gv2.Rows(8).Cells(colTTaxAmt).Value)
                End If
                If (gv2.Rows.Count > 9) Then
                    obj.TAX10 = clsCommon.myCstr(gv2.Rows(9).Cells(colTTaxAutCode).Value)

                    obj.TAX10_Rate = clsCommon.myCdbl(gv2.Rows(9).Cells(colTTaxRate).Value)
                    obj.Tax10_BAmount = clsCommon.myCdbl(gv2.Rows(9).Cells(colTBaseAmt).Value)
                    obj.TAX10_Amt = clsCommon.myCdbl(gv2.Rows(9).Cells(colTTaxAmt).Value)
                End If
                obj.is_For_TDS = 1
                obj.Terms_Code = txtTermCode.Value
                obj.Terms_Description = lblTermName.Text
                obj.Due_Date = txtDueDate.Value
                obj.Discount_Base = clsCommon.myCdbl(lblAmtWithDiscount.Text)
                obj.Discount_Amount = clsCommon.myCdbl(lblDiscountAmt.Text)
                obj.Amount_Less_Discount = clsCommon.myCdbl(lblAmtAfterDiscount.Text)
                obj.RoundOffAmount = clsCommon.myCdbl(lblRoundOff.Text)
                obj.Document_Total = clsCommon.myCdbl(lblTotRAmt.Text)
                obj.Balance_Amt = clsCommon.myCdbl(lblTotRAmt.Text)
                obj.Total_Landed_Amt = clsCommon.myCdbl(lblLandedAmt.Text)
                If rbtnTaxCalAutomatic.IsChecked Then
                    obj.Tax_Calculation_Type = EnumTaxCalucationType.Automatic
                ElseIf rbtnTaxCalManual.IsChecked Then
                    obj.Tax_Calculation_Type = EnumTaxCalucationType.Mannual
                End If

                Dim dt As DataTable = clsDBFuncationality.GetDataTable("select Payable_Account,Discount_Account from TSPL_VENDOR_ACCOUNT_SET where Acct_Set_Code='" + txtACSet.Value + "'")

                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    obj.Vendor_Control_AC = clsCommon.myCstr(dt.Rows(0)("Payable_Account"))
                    If clsCommon.myCdbl(lblDiscountAmt.Text) > 0 Then
                        obj.Discount_GL_AC = clsCommon.myCstr(dt.Rows(0)("Discount_Account"))
                    End If
                End If

                If (gvAC.Rows.Count > 0) Then
                    If clsCommon.myLen(gvAC.Rows(0).Cells(colAChgCode).Value) > 0 Then
                        obj.Add_Charge_Code1 = clsCommon.myCstr(gvAC.Rows(0).Cells(colAChgCode).Value)
                        obj.Add_Charge_Name1 = clsCommon.myCstr(gvAC.Rows(0).Cells(colAChgName).Value)
                        obj.Add_Charge_Amt1 = clsCommon.myCdbl(gvAC.Rows(0).Cells(colAChgAmount).Value)
                    End If
                End If
                If (gvAC.Rows.Count > 1) Then
                    If clsCommon.myLen(gvAC.Rows(1).Cells(colAChgCode).Value) > 0 Then
                        obj.Add_Charge_Code2 = clsCommon.myCstr(gvAC.Rows(1).Cells(colAChgCode).Value)
                        obj.Add_Charge_Name2 = clsCommon.myCstr(gvAC.Rows(1).Cells(colAChgName).Value)
                        obj.Add_Charge_Amt2 = clsCommon.myCdbl(gvAC.Rows(1).Cells(colAChgAmount).Value)
                    End If
                End If
                If (gvAC.Rows.Count > 2) Then
                    If clsCommon.myLen(gvAC.Rows(2).Cells(colAChgCode).Value) > 0 Then
                        obj.Add_Charge_Code3 = clsCommon.myCstr(gvAC.Rows(2).Cells(colAChgCode).Value)
                        obj.Add_Charge_Name3 = clsCommon.myCstr(gvAC.Rows(2).Cells(colAChgName).Value)
                        obj.Add_Charge_Amt3 = clsCommon.myCdbl(gvAC.Rows(2).Cells(colAChgAmount).Value)
                    End If
                End If
                If (gvAC.Rows.Count > 3) Then
                    If clsCommon.myLen(gvAC.Rows(3).Cells(colAChgCode).Value) > 0 Then
                        obj.Add_Charge_Code4 = clsCommon.myCstr(gvAC.Rows(3).Cells(colAChgCode).Value)
                        obj.Add_Charge_Name4 = clsCommon.myCstr(gvAC.Rows(3).Cells(colAChgName).Value)
                        obj.Add_Charge_Amt4 = clsCommon.myCdbl(gvAC.Rows(3).Cells(colAChgAmount).Value)
                    End If
                End If
                If (gvAC.Rows.Count > 4) Then
                    If clsCommon.myLen(gvAC.Rows(4).Cells(colAChgCode).Value) > 0 Then
                        obj.Add_Charge_Code5 = clsCommon.myCstr(gvAC.Rows(4).Cells(colAChgCode).Value)
                        obj.Add_Charge_Name5 = clsCommon.myCstr(gvAC.Rows(4).Cells(colAChgName).Value)
                        obj.Add_Charge_Amt5 = clsCommon.myCdbl(gvAC.Rows(4).Cells(colAChgAmount).Value)
                    End If
                End If
                If (gvAC.Rows.Count > 5) Then
                    If clsCommon.myLen(gvAC.Rows(5).Cells(colAChgCode).Value) > 0 Then
                        obj.Add_Charge_Code6 = clsCommon.myCstr(gvAC.Rows(5).Cells(colAChgCode).Value)
                        obj.Add_Charge_Name6 = clsCommon.myCstr(gvAC.Rows(5).Cells(colAChgName).Value)
                        obj.Add_Charge_Amt6 = clsCommon.myCdbl(gvAC.Rows(5).Cells(colAChgAmount).Value)
                    End If
                End If
                If (gvAC.Rows.Count > 6) Then
                    If clsCommon.myLen(gvAC.Rows(6).Cells(colAChgCode).Value) > 0 Then
                        obj.Add_Charge_Code7 = clsCommon.myCstr(gvAC.Rows(6).Cells(colAChgCode).Value)
                        obj.Add_Charge_Name7 = clsCommon.myCstr(gvAC.Rows(6).Cells(colAChgName).Value)
                        obj.Add_Charge_Amt7 = clsCommon.myCdbl(gvAC.Rows(6).Cells(colAChgAmount).Value)
                    End If
                End If
                If (gvAC.Rows.Count > 7) Then
                    If clsCommon.myLen(gvAC.Rows(7).Cells(colAChgCode).Value) > 0 Then
                        obj.Add_Charge_Code8 = clsCommon.myCstr(gvAC.Rows(7).Cells(colAChgCode).Value)
                        obj.Add_Charge_Name8 = clsCommon.myCstr(gvAC.Rows(7).Cells(colAChgName).Value)
                        obj.Add_Charge_Amt8 = clsCommon.myCdbl(gvAC.Rows(7).Cells(colAChgAmount).Value)
                    End If
                End If
                If (gvAC.Rows.Count > 8) Then
                    If clsCommon.myLen(gvAC.Rows(8).Cells(colAChgCode).Value) > 0 Then
                        obj.Add_Charge_Code9 = clsCommon.myCstr(gvAC.Rows(8).Cells(colAChgCode).Value)
                        obj.Add_Charge_Name9 = clsCommon.myCstr(gvAC.Rows(8).Cells(colAChgName).Value)
                        obj.Add_Charge_Amt9 = clsCommon.myCdbl(gvAC.Rows(8).Cells(colAChgAmount).Value)
                    End If
                End If
                If (gvAC.Rows.Count > 9) Then
                    If clsCommon.myLen(gvAC.Rows(9).Cells(colAChgCode).Value) > 0 Then
                        obj.Add_Charge_Code10 = clsCommon.myCstr(gvAC.Rows(9).Cells(colAChgCode).Value)
                        obj.Add_Charge_Name10 = clsCommon.myCstr(gvAC.Rows(9).Cells(colAChgName).Value)
                        obj.Add_Charge_Amt10 = clsCommon.myCdbl(gvAC.Rows(9).Cells(colAChgAmount).Value)
                    End If
                End If
                obj.Total_Add_Charge = clsCommon.myCdbl(lblAddCharges.Text)
                obj.Empty_Amount = clsCommon.myCdbl(lblTotEmptyAmt.Text)
                obj.Arr = New List(Of clsVedorInvoiceDetail)
                For Each grow As GridViewRowInfo In gv1.Rows
                    Dim objTr As New clsVedorInvoiceDetail()
                    objTr.Detail_Line_No = 1
                    ''---------richa agarwal
                    If clsCommon.CompairString(cmbRefType.SelectedValue, "A") = CompairStringResult.Equal Then
                        objTr.AgainstPayment_No = clsCommon.myCstr(grow.Cells(colRefDocNo).Value)
                        objTr.Payment_Amount = clsCommon.myCdbl(grow.Cells(colDocRefAmount).Value)
                        objTr.TDS_Per = clsCommon.myCdbl(grow.Cells(colTDSper).Value)
                    Else
                        objTr.Payment_Amount = clsCommon.myCdbl(grow.Cells(colDocRefAmount).Value)
                        objTr.TDS_Per = clsCommon.myCdbl(grow.Cells(colTDSper).Value)
                    End If
                    ''----------------------------
                    objTr.Deduction_Code = clsCommon.myCstr(grow.Cells(colDedCode).Value)
                    objTr.GL_Account_Code = clsCommon.myCstr(grow.Cells(colACCode).Value)
                    objTr.GL_Account_Desc = clsCommon.myCstr(grow.Cells(colACName).Value)
                    objTr.Amount = clsCommon.myCdbl(grow.Cells(colAmt).Value)
                    objTr.Landed_Amount = clsCommon.myCdbl(grow.Cells(colLandedAmt).Value)
                    objTr.Discount_Per = clsCommon.myCdbl(grow.Cells(colDisPer).Value)
                    objTr.Discount = clsCommon.myCdbl(grow.Cells(colDisAmt).Value)
                    objTr.Amount_less_Discount = clsCommon.myCdbl(grow.Cells(colAmtAfterDis).Value)
                    objTr.TAX1 = clsCommon.myCstr(grow.Cells(colTax1).Value)
                    objTr.TAX1_Rate = clsCommon.myCdbl(grow.Cells(colTaxRate1).Value)
                    objTr.TAX1_Amt = clsCommon.myCdbl(grow.Cells(colTaxAmt1).Value)
                    objTr.TAX2 = clsCommon.myCstr(grow.Cells(colTax2).Value)
                    objTr.TAX2_Rate = clsCommon.myCdbl(grow.Cells(colTaxRate2).Value)
                    objTr.TAX2_Amt = clsCommon.myCdbl(grow.Cells(colTaxAmt2).Value)
                    objTr.TAX3 = clsCommon.myCstr(grow.Cells(colTax3).Value)
                    objTr.TAX3_Rate = clsCommon.myCdbl(grow.Cells(colTaxRate3).Value)
                    objTr.TAX3_Amt = clsCommon.myCdbl(grow.Cells(colTaxAmt3).Value)
                    objTr.TAX4 = clsCommon.myCstr(grow.Cells(colTax4).Value)
                    objTr.TAX4_Rate = clsCommon.myCdbl(grow.Cells(colTaxRate4).Value)
                    objTr.TAX4_Amt = clsCommon.myCdbl(grow.Cells(colTaxAmt4).Value)
                    objTr.TAX5 = clsCommon.myCstr(grow.Cells(colTax5).Value)
                    objTr.TAX5_Rate = clsCommon.myCdbl(grow.Cells(colTaxRate5).Value)
                    objTr.TAX5_Amt = clsCommon.myCdbl(grow.Cells(colTaxAmt5).Value)
                    objTr.TAX6 = clsCommon.myCstr(grow.Cells(colTax6).Value)
                    objTr.TAX6_Rate = clsCommon.myCdbl(grow.Cells(colTaxRate6).Value)
                    objTr.TAX6_Amt = clsCommon.myCdbl(grow.Cells(colTaxAmt6).Value)
                    objTr.TAX7 = clsCommon.myCstr(grow.Cells(colTax7).Value)
                    objTr.TAX7_Rate = clsCommon.myCdbl(grow.Cells(colTaxRate7).Value)
                    objTr.TAX7_Amt = clsCommon.myCdbl(grow.Cells(colTaxAmt7).Value)
                    objTr.TAX8 = clsCommon.myCstr(grow.Cells(colTax8).Value)
                    objTr.TAX8_Rate = clsCommon.myCdbl(grow.Cells(colTaxRate8).Value)
                    objTr.TAX8_Amt = clsCommon.myCdbl(grow.Cells(colTaxAmt8).Value)
                    objTr.TAX9 = clsCommon.myCstr(grow.Cells(colTax9).Value)
                    objTr.TAX9_Rate = clsCommon.myCdbl(grow.Cells(colTaxRate9).Value)
                    objTr.TAX9_Amt = clsCommon.myCdbl(grow.Cells(colTaxAmt9).Value)
                    objTr.TAX10 = clsCommon.myCstr(grow.Cells(colTax10).Value)
                    objTr.TAX10_Rate = clsCommon.myCdbl(grow.Cells(colTaxRate10).Value)
                    objTr.TAX10_Amt = clsCommon.myCdbl(grow.Cells(colTaxAmt10).Value)
                    objTr.Total_Tax = clsCommon.myCdbl(grow.Cells(colTotTaxAmt).Value)
                    objTr.Total_Amount = clsCommon.myCdbl(grow.Cells(colAmtAfterTax).Value)
                    objTr.AddChargeCode = clsCommon.myCstr(grow.Cells(colAChgCode).Value)
                    objTr.AddChargeDesc = clsCommon.myCstr(grow.Cells(colAChgName).Value)
                    objTr.is_Unclaimed_Tax = clsCommon.myCBool(grow.Cells(colIsUnclaimedTax).Value)
                    objTr.Remarks = clsCommon.myCstr(grow.Cells(colRemarks).Value)
                    objTr.Invoice_Type = clsCommon.myCstr(grow.Cells(colDocType).Value)
                    objTr.Invoice_No = clsCommon.myCstr(grow.Cells(colDocNo).Value)
                    If (clsCommon.myLen(objTr.GL_Account_Code) > 0) And objTr.Amount <> 0 Then
                        obj.Arr.Add(objTr)
                    End If
                Next

                'If objRemittance IsNot Nothing Then
                objRemittance = New clsRemittance()

                Dim objVendor As clsTDSVendorDetails = clsTDSVendorDetails.GetData(TxtVendorNo.Value)
                objRemittance.Branch_Code = objVendor.Branch_Code
                objRemittance.Select_By = objVendor.VendorTypeCode
                objRemittance.Deduction_Code = clsCommon.myCstr(gv1.Rows(0).Cells(colDedCode).Value)
                objRemittance.Section_Code = clsCommon.myCstr(gv1.Rows(0).Cells(colDedSection).Value)
                objRemittance.Section_Description = clsTDSSection.GetName(objRemittance.Section_Code)
                Dim qry As String = "select Year_Name from TSPL_TDS_FINANCIAL_YEAR where convert(date,'" + txtDate.Value + "',103)>=  convert(date,From_Date,103)  and convert(date,'" + txtDate.Value + "',103)<=convert(date,To_Date,103) "
                objRemittance.Fiscal_Year = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
                objRemittance.Quarter = "First"

                objRemittance.Vendor_Code = TxtVendorNo.Value
                objRemittance.Vendor_Name = lblVendorName.Text
                objRemittance.Document_Date = txtDate.Value
                objRemittance.Document_Type = clsCommon.myCstr(cboDocType.SelectedValue)
                objRemittance.Document_Amount = clsCommon.myCdbl(lblTotRAmt.Text)
                objRemittance.Calculated_TDS_Base = clsCommon.myCdbl(lblTotRAmt.Text)
                objRemittance.IsTDSOverride = False
                objRemittance.Actual_TDS_Base = clsCommon.myCdbl(lblTotRAmt.Text)
                objRemittance.Calculated_TDS = clsCommon.myCdbl(lblTotRAmt.Text) ''(objRemittance.Calculated_TDS_Base * objRemittance.TDS_Per) / 100
                objRemittance.Actual_TDS = clsCommon.myCdbl(lblTotRAmt.Text) '' (objRemittance.Actual_TDS_Base * objRemittance.TDS_Per) / 100
                objRemittance.Calculated_Surcharge = 0 '' (objRemittance.Calculated_TDS_Base * objRemittance.Surcharge_Per) / 100
                objRemittance.Actual_Surcharge = 0 ' (objRemittance.Actual_TDS_Base * objRemittance.Surcharge_Per) / 100
                objRemittance.Calculated_Edu_Cess = 0 ' (objRemittance.Calculated_TDS_Base * objRemittance.Edu_Cess_Per) / 100
                objRemittance.Actual_Edu_Cess = 0 ' (objRemittance.Actual_TDS_Base * objRemittance.Edu_Cess_Per) / 100
                objRemittance.Calculated_Sec_Educess = 0 ' (objRemittance.Calculated_TDS_Base * objRemittance.Sec_Educess_Per) / 100
                objRemittance.Actual_Sec_Educess = 0 ' (objRemittance.Actual_TDS_Base * objRemittance.Sec_Educess_Per) / 100
                objRemittance.Calculated_Total_TDS = objRemittance.Calculated_TDS + objRemittance.Calculated_Surcharge + objRemittance.Calculated_Edu_Cess + objRemittance.Calculated_Sec_Educess
                objRemittance.Actual_Total_TDS = objRemittance.Actual_TDS + objRemittance.Actual_Surcharge + objRemittance.Actual_Edu_Cess + objRemittance.Actual_Sec_Educess


                obj.RemittanceObject = objRemittance
                obj.RemittanceObject.Vendor_Invoice_No = txtVendorInvoiceNo.Text
                obj.TDS_Base_Actual_Amount = objRemittance.Actual_TDS_Base
                obj.TDS_Base_Calculated_Amount = objRemittance.Calculated_TDS_Base
                obj.TDS_Percentage = objRemittance.TDS_Per
                obj.TDS_Actual_Amount = objRemittance.Actual_Total_TDS
                obj.TDS_Calculated_Amount = objRemittance.Calculated_Total_TDS
                obj.Nature_of_deduction = objRemittance.Deduction_Code
                obj.Branch_Code = objRemittance.Branch_Code
                obj.Balance_Amt = clsCommon.myCdbl(lblTotRAmt.Text) - objRemittance.Actual_Total_TDS
                obj.Section_Code = objRemittance.Section_Code


                'End If
                If (obj.Arr Is Nothing OrElse obj.Arr.Count <= 0) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Please Fill at list one GL Acount", Me.Text)
                    Return
                End If
                If (obj.SaveData(obj, isNewEntry)) Then
                    txtDocNo.Value = obj.Document_No
                    common.clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                    LoadData(obj.Document_No)
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub LoadData(ByVal strDocumentNo As String)
        Try
            txtlocation.Enabled = False
            btnSave.Enabled = True
            btnPost.Enabled = True
            btnDelete.Enabled = True
            btnViewTDSDetails.Enabled = False
            objRemittance = Nothing
            'cboDocType.Enabled = False
            txtDate.Enabled = False
            isInsideLoadData = True
            isNewEntry = False
            btnSave.Text = "Update"
            txtDocNo.MyReadOnly = True
            BlankAllControls()
            LoadBlankGridGL()
            LoadBlankGridTax()
            LoadBlankGridAC()
            Dim obj As New clsVedorInvoiceHead()
            obj = clsVedorInvoiceHead.GetData(strDocumentNo, "")
            btnPrint.Visible = True
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Document_No) > 0) Then
                If (obj.RemittanceObject IsNot Nothing) Then
                    objRemittance = New clsRemittance()
                    objRemittance = obj.RemittanceObject
                    btnViewTDSDetails.Enabled = True
                End If

                If clsCommon.myLen(obj.Posting_Date) > 0 Then
                    btnSave.Enabled = False
                    btnPost.Enabled = False
                    btnDelete.Enabled = False
                    UsLock1.Status = ERPTransactionStatus.Approved
                End If
                txtDocNo.Value = obj.Document_No
                txtDate.Value = obj.Invoice_Entry_Date
                TxtVendorNo.Value = obj.Vendor_Code
                '--------------Enables/Disables--TDS button------------------------
                Dim objVendor As clsTDSVendorDetails = clsTDSVendorDetails.GetData(obj.Vendor_Code)
                If objVendor IsNot Nothing Then
                    btnViewTDSDetails.Enabled = True
                Else
                    btnViewTDSDetails.Enabled = False
                End If
                '------------------------------------------------------------------
                txtlocation.Value = obj.loc_code
                'txtVendorNo.SelectedValue = obj.Vendor_Code
                '===============================update by richa agarwal 3 July,2018 ticket no. KDI/02/07/18-000383 pick vendor name from vendor master table instead of transaction table
                lblVendorName.Text = clsVendorMaster.GetName(obj.Vendor_Code, Nothing)
                txtVendorInvoiceNo.Text = obj.Vendor_Invoice_No
                txtVendorInvDatre.Value = obj.Vendor_Invoice_Date
                txtACSet.Value = obj.Account_Set
                'txtACSet.SelectedValue = obj.Account_Set
                cboDocType.SelectedValue = obj.Document_Type
                txtPONo.Text = obj.PO_Number
                '' priti starts here
                cmbRefType.SelectedValue = obj.RefDocType
                txtRefDocNo.Value = obj.RefDocNo
                '' priti ends here
                txtOrderNo.Text = obj.Order_No


                chkOnHold.Checked = obj.On_Hold
                txtDesc.Text = obj.Description
                txtTaxGroup.Value = obj.Tax_Group

                Dim objTaxGrpMaster As New clsTaxGroupMaster()
                objTaxGrpMaster = objTaxGrpMaster.GetDataForPurchase(obj.Tax_Group)
                If (objTaxGrpMaster IsNot Nothing) Then
                    lblTaxGrpName.Text = objTaxGrpMaster.Tax_Group_Desc
                End If
                txtTaxGroup.Value = obj.Tax_Group
                txtTermCode.Value = obj.Terms_Code
                'txtTermCode.SelectedValue = obj.Terms_Code
                lblTermName.Text = obj.Terms_Description
                txtDueDate.Value = obj.Due_Date
                lblAmtWithDiscount.Text = clsCommon.myFormat(obj.Discount_Base)
                lblDiscountAmt.Text = clsCommon.myFormat(obj.Discount_Amount)
                lblAmtAfterDiscount.Text = clsCommon.myFormat(obj.Amount_Less_Discount)
                lblTaxAmt.Text = clsCommon.myFormat(obj.Total_Tax)
                lblRoundOff.Text = clsCommon.myFormat(obj.RoundOffAmount)
                lblTotRAmt.Text = clsCommon.myFormat(obj.Document_Total)
                lblTotRAmt1.Text = clsCommon.myFormat(obj.Document_Total)
                lblTotEmptyAmt.Text = clsCommon.myFormat(obj.Empty_Amount)
                If obj.Tax_Calculation_Type = EnumTaxCalucationType.Automatic Then
                    rbtnTaxCalAutomatic.IsChecked = True
                ElseIf obj.Tax_Calculation_Type = EnumTaxCalucationType.Mannual Then
                    rbtnTaxCalManual.IsChecked = True
                End If
                lblLandedAmt.Text = clsCommon.myFormat(obj.Total_Landed_Amt)
                If (clsCommon.myLen(obj.TAX1) > 0) Then
                    gv2.Rows.AddNew()
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutCode).Value = obj.TAX1
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value = obj.TAX1_Rate
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTBaseAmt).Value = obj.Tax1_BAmount
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAmt).Value = obj.TAX1_Amt
                    If (objTaxGrpMaster IsNot Nothing AndAlso objTaxGrpMaster.Arr IsNot Nothing AndAlso objTaxGrpMaster.Arr.Count > 0) Then
                        For Each objTaxGrpTr As clsTaxGroupDetail In objTaxGrpMaster.Arr
                            If (clsCommon.CompairString(objTaxGrpTr.Tax_Code, obj.TAX1) = CompairStringResult.Equal) Then
                                gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutName).Value = objTaxGrpTr.Tax_Code_Desc
                                'gv2.Rows(gv2.Rows.Count - 1).Cells(colTIsSurTax).Value = objTaxGrpTr.Surtax
                                'gv2.Rows(gv2.Rows.Count - 1).Cells(colTIsTaxable).Value = objTaxGrpTr.Taxable
                                'gv2.Rows(gv2.Rows.Count - 1).Cells(colTSurTaxCode).Value = objTaxGrpTr.Surtax_Tax_Code
                                Exit For
                            End If
                        Next
                    End If
                End If
                If (clsCommon.myLen(obj.TAX2) > 0) Then
                    gv2.Rows.AddNew()
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutCode).Value = obj.TAX2
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value = obj.TAX2_Rate
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTBaseAmt).Value = obj.Tax2_BAmount
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAmt).Value = obj.TAX2_Amt
                    If (objTaxGrpMaster IsNot Nothing AndAlso objTaxGrpMaster.Arr IsNot Nothing AndAlso objTaxGrpMaster.Arr.Count > 0) Then
                        For Each objTaxGrpTr As clsTaxGroupDetail In objTaxGrpMaster.Arr
                            If (clsCommon.CompairString(objTaxGrpTr.Tax_Code, obj.TAX2) = CompairStringResult.Equal) Then
                                gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutName).Value = objTaxGrpTr.Tax_Code_Desc
                                'gv2.Rows(gv2.Rows.Count - 1).Cells(colTIsSurTax).Value = objTaxGrpTr.Surtax
                                'gv2.Rows(gv2.Rows.Count - 1).Cells(colTIsTaxable).Value = objTaxGrpTr.Taxable
                                'gv2.Rows(gv2.Rows.Count - 1).Cells(colTSurTaxCode).Value = objTaxGrpTr.Surtax_Tax_Code
                                Exit For
                            End If
                        Next
                    End If
                End If
                If (clsCommon.myLen(obj.TAX3) > 0) Then
                    gv2.Rows.AddNew()
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutCode).Value = obj.TAX3
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value = obj.TAX3_Rate
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTBaseAmt).Value = obj.Tax3_BAmount
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAmt).Value = obj.TAX3_Amt
                    If (objTaxGrpMaster IsNot Nothing AndAlso objTaxGrpMaster.Arr IsNot Nothing AndAlso objTaxGrpMaster.Arr.Count > 0) Then
                        For Each objTaxGrpTr As clsTaxGroupDetail In objTaxGrpMaster.Arr
                            If (clsCommon.CompairString(objTaxGrpTr.Tax_Code, obj.TAX3) = CompairStringResult.Equal) Then
                                gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutName).Value = objTaxGrpTr.Tax_Code_Desc
                                'gv2.Rows(gv2.Rows.Count - 1).Cells(colTIsSurTax).Value = objTaxGrpTr.Surtax
                                'gv2.Rows(gv2.Rows.Count - 1).Cells(colTIsTaxable).Value = objTaxGrpTr.Taxable
                                'gv2.Rows(gv2.Rows.Count - 1).Cells(colTSurTaxCode).Value = objTaxGrpTr.Surtax_Tax_Code
                                Exit For
                            End If
                        Next
                    End If
                End If
                If (clsCommon.myLen(obj.TAX4) > 0) Then
                    gv2.Rows.AddNew()
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutCode).Value = obj.TAX4
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value = obj.TAX4_Rate
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTBaseAmt).Value = obj.Tax4_BAmount
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAmt).Value = obj.TAX4_Amt
                    If (objTaxGrpMaster IsNot Nothing AndAlso objTaxGrpMaster.Arr IsNot Nothing AndAlso objTaxGrpMaster.Arr.Count > 0) Then
                        For Each objTaxGrpTr As clsTaxGroupDetail In objTaxGrpMaster.Arr
                            If (clsCommon.CompairString(objTaxGrpTr.Tax_Code, obj.TAX4) = CompairStringResult.Equal) Then
                                gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutName).Value = objTaxGrpTr.Tax_Code_Desc
                                'gv2.Rows(gv2.Rows.Count - 1).Cells(colTIsSurTax).Value = objTaxGrpTr.Surtax
                                'gv2.Rows(gv2.Rows.Count - 1).Cells(colTIsTaxable).Value = objTaxGrpTr.Taxable
                                'gv2.Rows(gv2.Rows.Count - 1).Cells(colTSurTaxCode).Value = objTaxGrpTr.Surtax_Tax_Code
                                Exit For
                            End If
                        Next
                    End If
                End If
                If (clsCommon.myLen(obj.TAX5) > 0) Then
                    gv2.Rows.AddNew()
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutCode).Value = obj.TAX5
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value = obj.TAX5_Rate
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTBaseAmt).Value = obj.Tax5_BAmount
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAmt).Value = obj.TAX5_Amt
                    If (objTaxGrpMaster IsNot Nothing AndAlso objTaxGrpMaster.Arr IsNot Nothing AndAlso objTaxGrpMaster.Arr.Count > 0) Then
                        For Each objTaxGrpTr As clsTaxGroupDetail In objTaxGrpMaster.Arr
                            If (clsCommon.CompairString(objTaxGrpTr.Tax_Code, obj.TAX5) = CompairStringResult.Equal) Then
                                gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutName).Value = objTaxGrpTr.Tax_Code_Desc
                                'gv2.Rows(gv2.Rows.Count - 1).Cells(colTIsSurTax).Value = objTaxGrpTr.Surtax
                                'gv2.Rows(gv2.Rows.Count - 1).Cells(colTIsTaxable).Value = objTaxGrpTr.Taxable
                                'gv2.Rows(gv2.Rows.Count - 1).Cells(colTSurTaxCode).Value = objTaxGrpTr.Surtax_Tax_Code
                                Exit For
                            End If
                        Next
                    End If
                End If
                If (clsCommon.myLen(obj.TAX6) > 0) Then
                    gv2.Rows.AddNew()
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutCode).Value = obj.TAX6
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value = obj.TAX6_Rate
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTBaseAmt).Value = obj.Tax6_BAmount
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAmt).Value = obj.TAX6_Amt
                    If (objTaxGrpMaster IsNot Nothing AndAlso objTaxGrpMaster.Arr IsNot Nothing AndAlso objTaxGrpMaster.Arr.Count > 0) Then
                        For Each objTaxGrpTr As clsTaxGroupDetail In objTaxGrpMaster.Arr
                            If (clsCommon.CompairString(objTaxGrpTr.Tax_Code, obj.TAX6) = CompairStringResult.Equal) Then
                                gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutName).Value = objTaxGrpTr.Tax_Code_Desc
                                'gv2.Rows(gv2.Rows.Count - 1).Cells(colTIsSurTax).Value = objTaxGrpTr.Surtax
                                'gv2.Rows(gv2.Rows.Count - 1).Cells(colTIsTaxable).Value = objTaxGrpTr.Taxable
                                'gv2.Rows(gv2.Rows.Count - 1).Cells(colTSurTaxCode).Value = objTaxGrpTr.Surtax_Tax_Code
                                Exit For
                            End If
                        Next
                    End If
                End If
                If (clsCommon.myLen(obj.TAX7) > 0) Then
                    gv2.Rows.AddNew()
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutCode).Value = obj.TAX7
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value = obj.TAX7_Rate
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTBaseAmt).Value = obj.Tax7_BAmount
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAmt).Value = obj.TAX7_Amt
                    If (objTaxGrpMaster IsNot Nothing AndAlso objTaxGrpMaster.Arr IsNot Nothing AndAlso objTaxGrpMaster.Arr.Count > 0) Then
                        For Each objTaxGrpTr As clsTaxGroupDetail In objTaxGrpMaster.Arr
                            If (clsCommon.CompairString(objTaxGrpTr.Tax_Code, obj.TAX7) = CompairStringResult.Equal) Then
                                gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutName).Value = objTaxGrpTr.Tax_Code_Desc
                                'gv2.Rows(gv2.Rows.Count - 1).Cells(colTIsSurTax).Value = objTaxGrpTr.Surtax
                                'gv2.Rows(gv2.Rows.Count - 1).Cells(colTIsTaxable).Value = objTaxGrpTr.Taxable
                                'gv2.Rows(gv2.Rows.Count - 1).Cells(colTSurTaxCode).Value = objTaxGrpTr.Surtax_Tax_Code
                                Exit For
                            End If
                        Next
                    End If
                End If
                If (clsCommon.myLen(obj.TAX8) > 0) Then
                    gv2.Rows.AddNew()
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutCode).Value = obj.TAX8
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value = obj.TAX8_Rate
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTBaseAmt).Value = obj.Tax8_BAmount
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAmt).Value = obj.TAX8_Amt
                    If (objTaxGrpMaster IsNot Nothing AndAlso objTaxGrpMaster.Arr IsNot Nothing AndAlso objTaxGrpMaster.Arr.Count > 0) Then
                        For Each objTaxGrpTr As clsTaxGroupDetail In objTaxGrpMaster.Arr
                            If (clsCommon.CompairString(objTaxGrpTr.Tax_Code, obj.TAX8) = CompairStringResult.Equal) Then
                                gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutName).Value = objTaxGrpTr.Tax_Code_Desc
                                'gv2.Rows(gv2.Rows.Count - 1).Cells(colTIsSurTax).Value = objTaxGrpTr.Surtax
                                'gv2.Rows(gv2.Rows.Count - 1).Cells(colTIsTaxable).Value = objTaxGrpTr.Taxable
                                'gv2.Rows(gv2.Rows.Count - 1).Cells(colTSurTaxCode).Value = objTaxGrpTr.Surtax_Tax_Code
                                Exit For
                            End If
                        Next
                    End If
                End If
                If (clsCommon.myLen(obj.TAX9) > 0) Then
                    gv2.Rows.AddNew()
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutCode).Value = obj.TAX9
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value = obj.TAX9_Rate
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTBaseAmt).Value = obj.Tax9_BAmount
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAmt).Value = obj.TAX9_Amt
                    If (objTaxGrpMaster IsNot Nothing AndAlso objTaxGrpMaster.Arr IsNot Nothing AndAlso objTaxGrpMaster.Arr.Count > 0) Then
                        For Each objTaxGrpTr As clsTaxGroupDetail In objTaxGrpMaster.Arr
                            If (clsCommon.CompairString(objTaxGrpTr.Tax_Code, obj.TAX9) = CompairStringResult.Equal) Then
                                gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutName).Value = objTaxGrpTr.Tax_Code_Desc
                                'gv2.Rows(gv2.Rows.Count - 1).Cells(colTIsSurTax).Value = objTaxGrpTr.Surtax
                                'gv2.Rows(gv2.Rows.Count - 1).Cells(colTIsTaxable).Value = objTaxGrpTr.Taxable
                                'gv2.Rows(gv2.Rows.Count - 1).Cells(colTSurTaxCode).Value = objTaxGrpTr.Surtax_Tax_Code
                                Exit For
                            End If
                        Next
                    End If
                End If
                If (clsCommon.myLen(obj.TAX10) > 0) Then
                    gv2.Rows.AddNew()
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutCode).Value = obj.TAX10
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value = obj.TAX10_Rate
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTBaseAmt).Value = obj.Tax10_BAmount
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAmt).Value = obj.TAX10_Amt
                    If (objTaxGrpMaster IsNot Nothing AndAlso objTaxGrpMaster.Arr IsNot Nothing AndAlso objTaxGrpMaster.Arr.Count > 0) Then
                        For Each objTaxGrpTr As clsTaxGroupDetail In objTaxGrpMaster.Arr
                            If (clsCommon.CompairString(objTaxGrpTr.Tax_Code, obj.TAX10) = CompairStringResult.Equal) Then
                                gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutName).Value = objTaxGrpTr.Tax_Code_Desc
                                'gv2.Rows(gv2.Rows.Count - 1).Cells(colTIsSurTax).Value = objTaxGrpTr.Surtax
                                'gv2.Rows(gv2.Rows.Count - 1).Cells(colTIsTaxable).Value = objTaxGrpTr.Taxable
                                'gv2.Rows(gv2.Rows.Count - 1).Cells(colTSurTaxCode).Value = objTaxGrpTr.Surtax_Tax_Code
                                Exit For
                            End If
                        Next
                    End If
                End If


                If (clsCommon.myLen(obj.Add_Charge_Code1) > 0) Then
                    gvAC.Rows.AddNew()
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colAChgCode).Value = obj.Add_Charge_Code1
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colAChgName).Value = obj.Add_Charge_Name1
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colAChgAmount).Value = obj.Add_Charge_Amt1
                End If
                If (clsCommon.myLen(obj.Add_Charge_Code2) > 0) Then
                    gvAC.Rows.AddNew()
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colAChgCode).Value = obj.Add_Charge_Code2
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colAChgName).Value = obj.Add_Charge_Name2
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colAChgAmount).Value = obj.Add_Charge_Amt2
                End If
                If (clsCommon.myLen(obj.Add_Charge_Code3) > 0) Then
                    gvAC.Rows.AddNew()
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colAChgCode).Value = obj.Add_Charge_Code3
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colAChgName).Value = obj.Add_Charge_Name3
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colAChgAmount).Value = obj.Add_Charge_Amt3
                End If
                If (clsCommon.myLen(obj.Add_Charge_Code4) > 0) Then
                    gvAC.Rows.AddNew()
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colAChgCode).Value = obj.Add_Charge_Code4
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colAChgName).Value = obj.Add_Charge_Name4
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colAChgAmount).Value = obj.Add_Charge_Amt4
                End If
                If (clsCommon.myLen(obj.Add_Charge_Code5) > 0) Then
                    gvAC.Rows.AddNew()
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colAChgCode).Value = obj.Add_Charge_Code5
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colAChgName).Value = obj.Add_Charge_Name5
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colAChgAmount).Value = obj.Add_Charge_Amt5
                End If
                If (clsCommon.myLen(obj.Add_Charge_Code6) > 0) Then
                    gvAC.Rows.AddNew()
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colAChgCode).Value = obj.Add_Charge_Code6
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colAChgName).Value = obj.Add_Charge_Name6
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colAChgAmount).Value = obj.Add_Charge_Amt6
                End If
                If (clsCommon.myLen(obj.Add_Charge_Code7) > 0) Then
                    gvAC.Rows.AddNew()
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colAChgCode).Value = obj.Add_Charge_Code7
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colAChgName).Value = obj.Add_Charge_Name7
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colAChgAmount).Value = obj.Add_Charge_Amt7
                End If
                If (clsCommon.myLen(obj.Add_Charge_Code8) > 0) Then
                    gvAC.Rows.AddNew()
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colAChgCode).Value = obj.Add_Charge_Code8
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colAChgName).Value = obj.Add_Charge_Name8
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colAChgAmount).Value = obj.Add_Charge_Amt8
                End If
                If (clsCommon.myLen(obj.Add_Charge_Code9) > 0) Then
                    gvAC.Rows.AddNew()
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colAChgCode).Value = obj.Add_Charge_Code9
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colAChgName).Value = obj.Add_Charge_Name9
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colAChgAmount).Value = obj.Add_Charge_Amt9
                End If
                If (clsCommon.myLen(obj.Add_Charge_Code10) > 0) Then
                    gvAC.Rows.AddNew()
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colAChgCode).Value = obj.Add_Charge_Code10
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colAChgName).Value = obj.Add_Charge_Name10
                    gvAC.Rows(gvAC.Rows.Count - 1).Cells(colAChgAmount).Value = obj.Add_Charge_Amt10
                End If

                lblAddCharges.Text = clsCommon.myFormat(obj.Total_Add_Charge)
                lblAddCharges1.Text = clsCommon.myFormat(obj.Total_Add_Charge)

                If clsCommon.myLen(obj.Against_POInvoice_No) > 0 OrElse clsCommon.myLen(obj.Against_PurchaseReturn_No) > 0 Then
                    btnPrint.Visible = False
                End If


                For Each objTr As clsVedorInvoiceDetail In obj.Arr
                    gv1.Rows.AddNew()
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = objTr.Detail_Line_No
                    ''---------richa agarwal
                    If clsCommon.CompairString(cmbRefType.SelectedValue, "A") = CompairStringResult.Equal Then
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colRefDocNo).Value = objTr.AgainstPayment_No
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDocRefAmount).Value = objTr.Payment_Amount
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTDSper).Value = objTr.TDS_Per
                    Else
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDocRefAmount).Value = objTr.Payment_Amount
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTDSper).Value = objTr.TDS_Per
                    End If
                    ''----------------------------
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDedCode).Value = objTr.Deduction_Code
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDedName).Value = objTr.Deduction_Name
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDedSection).Value = objTr.Deduction_Section

                    gv1.Rows(gv1.Rows.Count - 1).Cells(colACCode).Value = objTr.GL_Account_Code
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colACName).Value = objTr.GL_Account_Desc
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colAmt).Value = objTr.Amount
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colLandedAmt).Value = objTr.Landed_Amount
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDisPer).Value = objTr.Discount_Per
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDisAmt).Value = objTr.Discount
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colAmtAfterDis).Value = objTr.Amount_less_Discount
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTax1).Value = objTr.TAX1
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate1).Value = objTr.TAX1_Rate
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt1).Value = objTr.TAX1_Amt
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTax2).Value = objTr.TAX2
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate2).Value = objTr.TAX2_Rate
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt2).Value = objTr.TAX2_Amt
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTax3).Value = objTr.TAX3
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate3).Value = objTr.TAX3_Rate
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt3).Value = objTr.TAX3_Amt
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTax4).Value = objTr.TAX4
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate4).Value = objTr.TAX4_Rate
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt4).Value = objTr.TAX4_Amt
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTax5).Value = objTr.TAX5
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate5).Value = objTr.TAX5_Rate
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt5).Value = objTr.TAX5_Amt
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTax6).Value = objTr.TAX6
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate6).Value = objTr.TAX6_Rate
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt6).Value = objTr.TAX6_Amt
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTax7).Value = objTr.TAX7
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate7).Value = objTr.TAX7_Rate
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt7).Value = objTr.TAX7_Amt
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTax8).Value = objTr.TAX8
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate8).Value = objTr.TAX8_Rate
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt8).Value = objTr.TAX8_Amt
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTax9).Value = objTr.TAX9
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate9).Value = objTr.TAX9_Rate
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt9).Value = objTr.TAX9_Amt
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTax10).Value = objTr.TAX10
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate10).Value = objTr.TAX10_Rate
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt10).Value = objTr.TAX10_Amt
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTotTaxAmt).Value = objTr.Total_Tax
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colAmtAfterTax).Value = objTr.Total_Amount

                    gv1.Rows(gv1.Rows.Count - 1).Cells(colAChgCode).Value = objTr.AddChargeCode
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colAChgName).Value = objTr.AddChargeDesc
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colIsUnclaimedTax).Value = objTr.is_Unclaimed_Tax
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colRemarks).Value = objTr.Remarks
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDocNo).Value = objTr.Invoice_No
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDocType).Value = objTr.Invoice_Type
                Next
                'If clsCommon.myLen(obj.Posting_Date) <= 0 Then
                '    gv1.Rows.AddNew()
                '    gvAC.Rows.AddNew()
                'End If
                ''richa agarwal 
                EnabledisableinAdvance()
                If clsCommon.CompairString(cmbRefType.SelectedValue, "A") = CompairStringResult.Equal OrElse clsCommon.CompairString(cmbRefType.SelectedValue, "AP") = CompairStringResult.Equal Then
                    gv1.Columns(colAmt).ReadOnly = True
                Else
                    gv1.Columns(colAmt).ReadOnly = False
                End If
                ''-----------------------
                SetitemWiseTaxOnlySetting()
                FillVendorDetails()
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isInsideLoadData = False
        End Try
    End Sub

    Private Sub RadButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        CloseForm()
    End Sub

    Sub CloseForm()
        Me.Close()
    End Sub

    Private Sub gv2_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gv2.DoubleClick
        Try
            If rbtnTaxCalAutomatic.IsChecked Then
                Dim qry As String = "select Tax_Rate_Code as [Rate Code],Tax_Rate_Desc as [Rate Description],Tax_Rate as [Rate] from TSPL_TAX_RATES "
                Dim dblNewRate As Double = clsCommon.myCdbl(clsCommon.ShowSelectForm("FndVendorTaxRateIO", qry, "Rate", "Tax_Code='" + clsCommon.myCstr(gv2.CurrentRow.Cells(colTTaxAutCode).Value) + "' and Tax_Type='P'", "", "", True))
                Dim intRowNo As Integer = gv2.CurrentRow.Index
                If gv1.RowCount > 0 AndAlso intRowNo >= 0 Then
                    Dim strII As String = clsCommon.myCstr(intRowNo + 1)
                    For ii As Integer = 0 To gv1.Rows.Count - 1
                        gv1.Rows(ii).Cells("COLTAXRATE" + strII).Value = dblNewRate
                    Next
                End If
                For ii As Integer = 0 To gv1.Rows.Count - 1
                    UpdateCurrentRowWihtRowNo(ii)
                Next
                UpdateAllTotals()
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub btnViewTDSDetails_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnViewTDSDetails.Click
        ViewTDS()
    End Sub

    Sub ViewTDS()
        Try
            Dim frm As New FrmViewTDS()
            UpdateTDSAmount()
            frm.ObjIn = objRemittance
            frm.ShowDialog()
            'If (frm.ObjReturn IsNot Nothing) Then
            objRemittance = frm.ObjReturn
            'End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnPost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPost.Click
        PostData()
    End Sub

    Sub PostData()
        Try
            If (myMessages.postConfirm()) Then

                'For Each gr As GridViewRowInfo In gv2.Rows
                '    Dim strTaxCode As String = clsCommon.myCstr(gr.Cells(colTTaxAutCode).Value)
                '    Dim dblTaxRate As Double = clsCommon.myCdbl(gr.Cells(colTTaxRate).Value)
                '    Dim IsSurTax As Boolean = clsCommon.myCBool(gr.Cells(colTIsSurTax).Value)
                '    Dim strSurTaxCode As String = clsCommon.myCstr(gr.Cells(colTSurTaxCode).Value)
                '    Dim IsTaxable As Boolean = clsCommon.myCBool(gr.Cells(colTIsTaxable).Value)

                'Next


                If (clsVedorInvoiceHead.PostData("", txtDocNo.Value, txtRefDocNo.Value)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Data Posted Successfully ", Me.Text)
                    LoadData(txtDocNo.Value)
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        DeleteData()
    End Sub

    Sub DeleteData()
        Try
            Dim Reason As String = ""
            If (myMessages.deleteConfirm()) Then
                If clsCancelLog.CheckForReasonOnDelete() Then
                    '' REASON FOR DELETE 
                    Dim frm As New FrmFreeTxtBox1
                    frm.Text = "Remarks for Delete"
                    frm.ShowDialog()
                    If clsCommon.myLen(frm.strRmks) <= 0 Then
                        Exit Sub
                    Else
                        Reason = frm.strRmks
                    End If
                End If
                If (clsVedorInvoiceHead.DeleteData(txtDocNo.Value)) Then
                    saveCancelLog(Reason, "Delete", Nothing)
                    common.clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ", Me.Text)
                    AddNew()
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub
    Function saveCancelLog(ByVal Reason As String, ByVal Activity_Type As String, Optional ByVal trans As System.Data.SqlClient.SqlTransaction = Nothing) As Boolean
        Dim obj As New clsCancelLog
        obj.Program_Code = Form_ID
        obj.DOCUMENT_NO = clsCommon.myCstr(Me.txtDocNo.Value)
        obj.REASON = Reason
        obj.ACTIVITY_TYPE = Activity_Type
        Return clsCancelLog.SaveData(obj, True, trans)
    End Function

    Private Sub gv1_CellEditorInitialized(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellEditorInitialized
        If TypeOf Me.gv1.CurrentColumn Is GridViewMultiComboBoxColumn Then
            Dim editor As RadMultiColumnComboBoxElement = DirectCast(Me.gv1.ActiveEditor, RadMultiColumnComboBoxElement)
            editor.AutoSizeDropDownToBestFit = True
            editor.EditorControl.MasterTemplate.BestFitColumns()
            editor.DropDownStyle = RadDropDownStyle.DropDown
            editor.AutoFilter = True
            If editor.EditorControl.MasterTemplate.FilterDescriptors.Count = 0 Then
                Dim autoFilter As FilterDescriptor = New FilterDescriptor("Name", FilterOperator.StartsWith, "")
                autoFilter.IsFilterEditor = True
                editor.EditorControl.FilterDescriptors.Add(autoFilter)
            End If
        End If
    End Sub

    Private Sub txtDocNo__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtDocNo._MYNavigator
        Try
            '----------Added By--Pankaj Kumar-----For GL Security-----31/08/2012
            Dim Arrloc As New ArrayList
            Dim ArrAcc As New ArrayList
            clsERPFuncationality.GlLOCandACCArray(Arrloc, ArrAcc)
            Dim WhrCls As String = ""
            If clsCommon.CompairString(objCommonVar.CurrentUserCode, "Admin") = CompairStringResult.Equal Then
                WhrCls = " and TSPL_VENDOR_INVOICE_HEAD.is_For_TDS=1"
            Else
                WhrCls = " and TSPL_VENDOR_INVOICE_HEAD.is_For_TDS=1 AND substring(TSPL_VENDOR_INVOICE_DETAIL.GL_Account_Code,(len(TSPL_VENDOR_INVOICE_DETAIL.GL_Account_Code)-2),3) IN (" + clsCommon.GetMulcallString(Arrloc) + ") OR TSPL_VENDOR_INVOICE_DETAIL.GL_Account_Code IN (" + clsCommon.GetMulcallString(ArrAcc) + ")"
            End If
            '-------------------------Code Ends Here------------------
            Dim qry As String = "select Document_No from TSPL_VENDOR_INVOICE_HEAD where Document_No="
            Select Case NavType
                Case NavigatorType.First
                    qry += "(select MIN(TSPL_VENDOR_INVOICE_HEAD.Document_No) from TSPL_VENDOR_INVOICE_HEAD left outer join TSPL_VENDOR_INVOICE_DETAIL on TSPL_VENDOR_INVOICE_DETAIL.Document_No=TSPL_VENDOR_INVOICE_HEAD.Document_No and TSPL_VENDOR_INVOICE_DETAIL.Detail_Line_No=1 where 2=2  " + WhrCls + ")"
                Case NavigatorType.Last
                    qry += "(select Max(TSPL_VENDOR_INVOICE_HEAD.Document_No) from TSPL_VENDOR_INVOICE_HEAD left outer join TSPL_VENDOR_INVOICE_DETAIL on TSPL_VENDOR_INVOICE_DETAIL.Document_No=TSPL_VENDOR_INVOICE_HEAD.Document_No and TSPL_VENDOR_INVOICE_DETAIL.Detail_Line_No=1 where 2=2  " + WhrCls + ")"
                Case NavigatorType.Next
                    qry += "(select Min(TSPL_VENDOR_INVOICE_HEAD.Document_No) from TSPL_VENDOR_INVOICE_HEAD left outer join TSPL_VENDOR_INVOICE_DETAIL on TSPL_VENDOR_INVOICE_DETAIL.Document_No=TSPL_VENDOR_INVOICE_HEAD.Document_No and TSPL_VENDOR_INVOICE_DETAIL.Detail_Line_No=1 where TSPL_VENDOR_INVOICE_HEAD.Document_No>'" + txtDocNo.Value + "' " + WhrCls + ")"
                Case NavigatorType.Previous
                    qry += "(select Max(TSPL_VENDOR_INVOICE_HEAD.Document_No) from TSPL_VENDOR_INVOICE_HEAD left outer join TSPL_VENDOR_INVOICE_DETAIL on TSPL_VENDOR_INVOICE_DETAIL.Document_No=TSPL_VENDOR_INVOICE_HEAD.Document_No and TSPL_VENDOR_INVOICE_DETAIL.Detail_Line_No=1 where TSPL_VENDOR_INVOICE_HEAD.Document_No<'" + txtDocNo.Value + "' " + WhrCls + ")"
            End Select
            LoadData(clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry)))
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtDocNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtDocNo._MYValidating
        ' done by priti KDI/04/07/18-000387 for updating vendor name from master
        Dim qry As String = "select TSPL_VENDOR_INVOICE_HEAD.Document_No as DocumentNo,Invoice_Entry_Date as Date,TSPL_VENDOR_INVOICE_HEAD.Vendor_Code as [Vendor Code],TSPL_VENDOR_MASTER.Vendor_Name as [Vendor Name],ISNULL(TSPL_VENDOR_MASTER.alies_name,'') As [Alies Name],Vendor_Invoice_No as [Vendor Invoice No],Against_POInvoice_No as [PO Invoice No],Vendor_Invoice_Date as [Vendor Invoice Date],(case when len(Posting_Date) is null then 'UnPosted' else 'Posted' end) as [Status],Account_Set as AccountSet,Against_PurchaseReturn_No as [PO Return No],TSPL_VENDOR_INVOICE_HEAD.Against_Salary_Generation_Code as [Against Salary Generation] from TSPL_VENDOR_INVOICE_HEAD left outer join TSPL_VENDOR_INVOICE_DETAIL on TSPL_VENDOR_INVOICE_DETAIL.Document_No=TSPL_VENDOR_INVOICE_HEAD.Document_No and TSPL_VENDOR_INVOICE_DETAIL.Detail_Line_No=1   "
        '' Anubhooti 12-Mar-2015 (Fetch Alies Name On Vendor Finder)
        qry += " LEFT OUTER JOIN TSPL_VENDOR_MASTER ON TSPL_VENDOR_MASTER.Vendor_Code = TSPL_VENDOR_INVOICE_HEAD.Vendor_Code "
        '----------Added By--Pankaj Kumar-----For GL Security-----31/08/2012
        Dim Arrloc As New ArrayList
        Dim ArrAcc As New ArrayList
        clsERPFuncationality.GlLOCandACCArray(Arrloc, ArrAcc)
        Dim WhrCls As String = ""
        If clsCommon.CompairString(objCommonVar.CurrentUserCode, "Admin") = CompairStringResult.Equal Then
            WhrCls = " TSPL_VENDOR_INVOICE_HEAD.is_For_TDS=1 "
        Else
            WhrCls = " TSPL_VENDOR_INVOICE_HEAD.is_For_TDS=1 and ( substring(TSPL_VENDOR_INVOICE_DETAIL.GL_Account_Code,(len(TSPL_VENDOR_INVOICE_DETAIL.GL_Account_Code)-2),3) IN (" + clsCommon.GetMulcallString(Arrloc) + ") OR TSPL_VENDOR_INVOICE_DETAIL.GL_Account_Code IN (" + clsCommon.GetMulcallString(ArrAcc) + "))"
        End If
        '-------------------------Code Ends Here------------------
        LoadData(clsCommon.ShowSelectForm("APInvcSLCtr", qry, "DocumentNo", WhrCls, txtDocNo.Value, "DocumentNo", isButtonClicked))
    End Sub

    Private Sub txtDocNo_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtDocNo.KeyPress
        If (e.KeyChar = Chr(39)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        PrintData()

    End Sub

    Sub PrintData()
        If clsCommon.myLen(txtDocNo.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "Document No not found to print", Me.Text)
        End If
        Dim Arr As New ArrayList
        Arr.Add(txtDocNo.Value)
        frmRptAPInvoice.PrintData("", "", True, Arr, False, Nothing, False, Nothing)
    End Sub

    Private Sub FrmAPInvoiceEntry_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.F2 AndAlso gv1.CurrentCell IsNot Nothing Then
            If clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentCell.ColumnInfo.Name), colDedCode) = CompairStringResult.Equal Then
                OpenGLAccount(True)
            ElseIf clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentCell.ColumnInfo.Name), colAChgCode) = CompairStringResult.Equal Then
                OpenAdditionCharges(True)
            ElseIf clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentCell.ColumnInfo.Name), colDocNo) = CompairStringResult.Equal Then
                OpenInvoiceNo(True)
            End If
        ElseIf e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnAddNew.Enabled Then
            AddNew()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnSave.Enabled Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag AndAlso btnPost.Enabled Then
            PostData()
        ElseIf e.Control AndAlso e.KeyCode = Keys.P AndAlso btnPrint.Enabled Then
            PrintData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.V AndAlso MyBase.isPostFlag Then
            ViewTDS()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso btnDelete.Enabled Then
            DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnClose.Enabled Then
            CloseForm()
        ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F12 Then
            If MyBase.isReverse Then

                Dim frm As New FrmPWD(Nothing)
                frm.strType = "SIRC"
                frm.strCode = "SIReversAndCreate"
                frm.ShowDialog()
                If frm.isPasswordCorrect Then
                    btnReverse.Visible = True
                End If
            Else
                clsCommon.MyMessageBoxShow(Me, "You are not authorized to perform this action.", Me.Text, MessageBoxButtons.OK, Telerik.WinControls.RadMessageIcon.Error)
                'MessageBox.Show("You are not authorized to perform this action.", "Unauthorized Access", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        End If
    End Sub

    Private Sub gv1_CellBeginEdit(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellCancelEventArgs) Handles gv1.CellBeginEdit
        If TypeOf Me.gv1.CurrentColumn Is GridViewTextBoxColumn Then
            Dim editor As RadTextBoxEditor = DirectCast(Me.gv1.ActiveEditor, RadTextBoxEditor)
            Dim editorElement As RadTextBoxElement = DirectCast(editor.EditorElement, RadTextBoxElement)
            If e.ColumnIndex = 1 Then
                AddHandler editorElement.KeyDown, AddressOf key_down
            End If
        End If
    End Sub

    Private Sub key_down(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        ''If e.KeyCode = Keys.F2 Then
        ''    OpenGLAccount(True)
        ''    gv1.EndEdit()
        ''End If
    End Sub

    Private Sub gv1_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles gv1.KeyDown
        ''If e.KeyCode = Keys.F2 Then
        ''    If clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentCell.ColumnInfo.Name), colACCode) = CompairStringResult.Equal Then
        ''        OpenGLAccount(True)
        ''    End If
        ''End If
    End Sub

    Private Sub MyComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.Data.PositionChangedEventArgs) Handles cmbRefType.SelectedIndexChanged
        txtRefDocNo.Value = ""
        txtRefDocNo.Enabled = False
        repoAdChagCode.IsVisible = False
        repoAdChagName.IsVisible = False
        If clsCommon.myLen(cmbRefType.SelectedValue) > 0 Then
            txtRefDocNo.Enabled = True
        End If
        If clsCommon.CompairString(clsCommon.myCstr(cmbRefType.SelectedValue), "S") = CompairStringResult.Equal Then
            repoAdChagCode.IsVisible = True
            repoAdChagName.IsVisible = True
        End If
        EnabledisableinAdvance()
    End Sub
    ''richa agarwal 
    Sub EnabledisableinAdvance()
        If clsCommon.CompairString(clsCommon.myCstr(cmbRefType.SelectedValue), "A") = CompairStringResult.Equal Then
            gv1.Columns(colRefDocNo).IsVisible = True
            gv1.Columns(colDocRefAmount).IsVisible = True
            gv1.Columns(colTDSper).IsVisible = True
            txtPONo.Text = ""
            txtVendorInvoiceNo.Text = ""
            txtRefDocNo.Visible = False
            gv1.Columns(colDocRefAmount).ReadOnly = True
            txtPONo.Enabled = False
            txtVendorInvDatre.Enabled = False
            txtVendorInvoiceNo.Enabled = False
            RadLabel15.Visible = False
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cmbRefType.SelectedValue), "AP") = CompairStringResult.Equal Then
            gv1.Columns(colDocRefAmount).ReadOnly = False
            gv1.Columns(colDocRefAmount).IsVisible = True
            gv1.Columns(colTDSper).IsVisible = True
            gv1.Columns(colAmt).ReadOnly = True
        Else
            gv1.Columns(colRefDocNo).IsVisible = False
            gv1.Columns(colDocRefAmount).IsVisible = False
            gv1.Columns(colTDSper).IsVisible = False
            gv1.Columns(colDocRefAmount).ReadOnly = True
            gv1.Columns(colAmt).ReadOnly = False
            txtRefDocNo.Visible = True
            txtPONo.Enabled = True
            txtVendorInvDatre.Enabled = True
            txtVendorInvoiceNo.Enabled = True
            RadLabel15.Visible = True
        End If
    End Sub
    ''----------------------------
    Private Sub gv1_CellFormatting(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CellFormattingEventArgs) Handles gv1.CellFormatting
        Try

            If e.Column.Index >= 0 Then
                If e.Column Is gv1.Columns(colACCode) Then
                    If clsCommon.myLen(gv1.CurrentRow.Cells(colAChgCode).Value) > 0 Then
                        gv1.CurrentRow.Cells(colACCode).ReadOnly = True
                    Else
                        gv1.CurrentRow.Cells(colACCode).ReadOnly = False
                    End If
                ElseIf e.Column Is gv1.Columns(colIsUnclaimedTax) Then
                    If rbtnTaxCalManual.IsChecked Then
                        gv1.CurrentRow.Cells(colIsUnclaimedTax).ReadOnly = True
                    Else
                        gv1.CurrentRow.Cells(colIsUnclaimedTax).ReadOnly = False
                    End If
                End If
            End If

            'Dim cell As GridDataCellElement = TryCast(e.CellElement, GridDataCellElement)
            'cell.GradientStyle = GradientStyles.Solid
            'cell.BackColor = Color.FromArgb(243, 181, 51)
        Catch ex As Exception
            'common.clsCommon.MyMessageBoxShow(me,ex.Message,me.text)
        End Try

    End Sub

    Private Sub gv1_UserDeletedRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowEventArgs) Handles gv1.UserDeletedRow
        UpdateAllTotals()
        For ii As Integer = 1 To gv1.Rows.Count
            gv1.Rows(ii - 1).Cells(colLineNo).Value = ii
        Next
    End Sub

    Private Sub gv1_UserDeletingRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs) Handles gv1.UserDeletingRow
        If common.clsCommon.MyMessageBoxShow(Me, "Delete The Current Row." + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
            e.Cancel = True
        End If
    End Sub

    Private Sub txtDocNo_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDocNo.Load

    End Sub

    Private Sub txtRefDocNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtRefDocNo._MYValidating
        Try
            If clsCommon.myLen(cmbRefType.SelectedValue) > 0 Then
                If clsCommon.CompairString(clsCommon.myCstr(cmbRefType.SelectedValue), "S") = CompairStringResult.Equal Then
                    Dim qry As String = "select SRN_No as Code,SRN_Date,Vendor_Code,Vendor_Name from TSPL_SRN_HEAD"
                    txtRefDocNo.Value = clsCommon.ShowSelectForm("APINVSRNFND", qry, "Code", "Status=1", txtRefDocNo.Value, "Code", isButtonClicked)
                    ''ElseIf clsCommon.CompairString(clsCommon.myCstr(cmbRefType.SelectedValue), "LO") = CompairStringResult.Equal Then
                    ''    Dim qry As String = "select Shipment_No as Loadout,Shipment_Date as [Loadout Date],Cust_Name as Customer from TSPL_SHIPMENT_MASTER "
                    ''    txtRefDocNo.Value = clsCommon.ShowSelectForm("APINVLO", qry, "Loadout", "Is_Post='Y' and Shipment_No not in(select RefDocNo from TSPL_VENDOR_INVOICE_HEAD where RefDocType='LO' and Document_No not in('" + txtDocNo.Value + "'))", txtRefDocNo.Value, "Loadout", isButtonClicked)
                ElseIf clsCommon.CompairString(clsCommon.myCstr(cmbRefType.SelectedValue), "AP") = CompairStringResult.Equal Then
                    Dim qry As String = "select Document_No as Code,Invoice_Entry_Date as Date,Vendor_Code as [Vendor Code],Vendor_Name as Vendor,Vendor_Invoice_No as [Vendor Invoice No],Vendor_Invoice_Date as [Vendor Invoice Date] from TSPL_VENDOR_INVOICE_HEAD "
                    Dim whrclas As String = "Document_Type='I' and LEN(Posting_Date)>0 and Vendor_Code='" + TxtVendorNo.Value + "'"
                    txtRefDocNo.Value = clsCommon.ShowSelectForm("APINVAPInv", qry, "Code", whrclas, txtRefDocNo.Value, "Code", isButtonClicked)
                    gv1.Rows(0).Cells(colDocRefAmount).Value = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select Amount_Less_Discount from TSPL_VENDOR_INVOICE_HEAD where Document_No ='" & clsCommon.myCstr(txtRefDocNo.Value) & "'"))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(cmbRefType.SelectedValue), "WO") = CompairStringResult.Equal Then
                    Dim qry As String = "select PurchaseOrder_No as Code,PurchaseOrder_Date as Date,Vendor_Code as [Vendor code],Vendor_Name as [Vendor Name] from TSPL_PURCHASE_ORDER_HEAD"
                    Dim whrclas As String = "Status='1'"
                    txtRefDocNo.Value = clsCommon.ShowSelectForm("WORKORDER", qry, "Code", whrclas, txtRefDocNo.Value, "Code", isButtonClicked)
                    ''richa agarwal
                ElseIf clsCommon.CompairString(clsCommon.myCstr(cmbRefType.SelectedValue), "A") = CompairStringResult.Equal Then
                    Dim qry As String = "select Payment_No as Code,CONVERT(VARCHAR,Payment_Date,103) as Date,Vendor_Code as [Vendor code],Vendor_Name as [Vendor Name],Location_Code as [Location Code],Location_Description as [Location Name],Payment_Amount from TSPL_PAYMENT_HEADER "
                    Dim whrclas As String = " TDS_Amount =0 and Posted =1 and Payment_Type='AV' and Payment_No not in ( Select RefDocNo from TSPL_VENDOR_INVOICE_HEAD where RefDocType='A')"
                    txtRefDocNo.Value = clsCommon.ShowSelectForm("ADVANCE", qry, "Code", whrclas, txtRefDocNo.Value, "Code", isButtonClicked)

                End If

                ''If clsCommon.myLen(txtRefDocNo.Value) <= 0 Then
                ''    common.clsCommon.MyMessageBoxShow("Please Select 'Reference Document Number'")
                ''End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub LoadBlankGridAC()
        gvAC.Rows.Clear()
        gvAC.Columns.Clear()

        Dim repoACCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoACCode.FormatString = ""
        repoACCode.HeaderText = "Addition Charges Code"
        repoACCode.Name = colAChgCode
        repoACCode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoACCode.Width = 150
        repoACCode.ReadOnly = False
        gvAC.MasterTemplate.Columns.Add(repoACCode)

        Dim repoACName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoACName.FormatString = ""
        repoACName.HeaderText = "Addition Charges Description"
        repoACName.Name = colAChgName
        repoACName.Width = 300
        repoACName.ReadOnly = True
        gvAC.MasterTemplate.Columns.Add(repoACName)

        Dim repoACAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoACAmt.FormatString = ""
        repoACAmt.HeaderText = "Amount"
        repoACAmt.Name = colAChgAmount
        repoACAmt.Width = 100
        repoACAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoACAmt.ReadOnly = False
        gvAC.MasterTemplate.Columns.Add(repoACAmt)

        gvAC.AllowAddNewRow = False
        gvAC.ShowGroupPanel = False
        gvAC.AllowColumnReorder = True
        gvAC.AllowRowReorder = False
        gvAC.EnableSorting = False
        gvAC.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gvAC.MasterTemplate.ShowRowHeaderColumn = False
        gvAC.TableElement.TableHeaderHeight = 40
        ReStoreGridLayout()
    End Sub

    Private Sub gvAC_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvAC.CellValueChanged
        Try
            If (Not isInsideLoadData) Then
                If Not isCellValueChangedOpen Then
                    isCellValueChangedOpen = True
                    If e.Column Is gvAC.Columns(colAChgAmount) Then

                        UpdateAllTotals()
                    ElseIf e.Column Is gvAC.Columns(colAChgCode) Then
                        Dim obj As clsAdditionalCharge = clsAdditionalCharge.getFinder(clsCommon.myCstr(gvAC.CurrentRow.Cells(colAChgCode).Value), False)
                        If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Code) > 0 Then
                            gvAC.CurrentRow.Cells(colAChgCode).Value = obj.Code
                            gvAC.CurrentRow.Cells(colAChgName).Value = obj.desc
                        Else
                            gvAC.CurrentRow.Cells(colAChgCode).Value = ""
                            gvAC.CurrentRow.Cells(colAChgName).Value = ""
                            gvAC.CurrentRow.Cells(colAChgAmount).Value = 0
                        End If
                    End If
                End If
                setGridFocusAC()
                isCellValueChangedOpen = False
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub setGridFocusAC()
        Try
            Dim intCurrRow As Integer = gvAC.CurrentRow.Index
            If intCurrRow = gvAC.Rows.Count - 1 AndAlso gvAC.Rows.Count <= 10 Then
                gvAC.Rows.AddNew()
                gvAC.CurrentRow = gvAC.Rows(intCurrRow)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnDrillDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDrillDown.Click
        Dim strPoInvcNo As String = connectSql.RunScalar("select PI_No  from TSPL_PI_HEAD where Vendor_Invoice_No ='" + txtVendorInvoiceNo.Text + "' and Vendor_Code ='" + TxtVendorNo.Value + "'")
        clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnPurchaseInvoice, strPoInvcNo)
    End Sub

    Private Sub rbtnTaxCalAutomatic_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtnTaxCalAutomatic.ToggleStateChanged, rbtnTaxCalManual.ToggleStateChanged
        If Not isInsideLoadData Then
            If rbtnTaxCalAutomatic.IsChecked Then
                txtTaxGroup_TxtChanged()
            ElseIf rbtnTaxCalManual.IsChecked Then
                For intRowNo As Integer = 0 To gv2.Rows.Count - 1
                    gv2.Rows(intRowNo).Cells(colTTaxRate).Value = Nothing
                    gv2.Rows(intRowNo).Cells(colTTaxAmt).Value = Nothing
                    gv2.Rows(intRowNo).Cells(colTBaseAmt).Value = Nothing
                Next
                For intRowNo As Integer = 0 To gv1.Rows.Count - 1
                    For ii As Integer = 1 To 10
                        Dim strII As String = clsCommon.myCstr(ii)
                        gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTaxRate" + strII)).Value = Nothing
                        gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTaxAmt" + strII)).Value = Nothing
                    Next
                    gv1.Rows(intRowNo).Cells(colIsUnclaimedTax).Value = False
                Next
            End If
        End If
    End Sub

    Private Sub gv2_CellFormatting(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CellFormattingEventArgs) Handles gv2.CellFormatting
        Try
            If e.Column.Index >= 0 Then
                If (e.Column Is gv2.Columns(colTTaxAmt)) Then
                    gv2.CurrentRow.Cells(colTTaxAmt).ReadOnly = rbtnTaxCalAutomatic.IsChecked
                ElseIf (e.Column Is gv2.Columns(colTTaxRate)) Then
                    gv2.CurrentRow.Cells(colTTaxRate).ReadOnly = rbtnTaxCalAutomatic.IsChecked
                End If

                'Dim cell As GridDataCellElement = TryCast(e.CellElement, GridDataCellElement)
                'cell.GradientStyle = GradientStyles.Solid
                'cell.BackColor = Color.FromArgb(243, 181, 51)
            End If
        Catch ex As Exception
            'common.clsCommon.MyMessageBoxShow(me,ex.Message,me.text)
        End Try
    End Sub

    Private Sub gv2_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv2.CellValueChanged
        Try
            If (Not isInsideLoadData) Then
                If Not isCellValueChangedTaxOpen Then
                    isCellValueChangedTaxOpen = True
                    If (e.Column Is (gv2.Columns(colTTaxAmt)) AndAlso rbtnTaxCalManual.IsChecked) Then
                        For ii As Integer = 0 To gv1.Rows.Count - 1
                            UpdateCurrentRowWihtRowNo(ii)
                        Next
                        UpdateAllTotals()
                    End If
                    isCellValueChangedTaxOpen = False
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub gv1_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gv1.DoubleClick
        ''Try
        ''    If gv1.CurrentColumn Is gv1.Columns(colTotTaxAmt) AndAlso rbtnTaxCalAutomatic.IsChecked Then
        ''        Dim frm As New FrmPOItemTaxDetails()
        ''        frm.strLineNo = clsCommon.myCstr(gv1.CurrentRow.Cells(colLineNo).Value)
        ''        frm.strItemCode = clsCommon.myCstr(gv1.CurrentRow.Cells(colACCode).Value)
        ''        frm.strItemName = clsCommon.myCstr(gv1.CurrentRow.Cells(colACName).Value)
        ''        frm.dblTotTax = clsCommon.myCdbl(gv1.CurrentRow.Cells(colTotTaxAmt).Value)
        ''        frm.dblAmtAfterDis = clsCommon.myCdbl(gv1.CurrentRow.Cells(colAmtAfterDis).Value)
        ''        If clsCommon.myLen(frm.strItemCode) > 0 Then
        ''            frm.ArrIn = New List(Of clsTempItemTaxDetails)
        ''            For ii As Integer = 1 To 10
        ''                Dim strii As String = clsCommon.myCstr(ii)
        ''                Dim obj As New clsTempItemTaxDetails()
        ''                obj.AuthorityCode = clsCommon.myCstr(gv1.CurrentRow.Cells("COLTAX" + strii).Value)
        ''                If clsCommon.myLen(obj.AuthorityCode) > 0 Then
        ''                    obj.AuthorityName = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Tax_Code_Desc from TSPL_TAX_MASTER WHERE Tax_Code='" + obj.AuthorityCode + "'"))
        ''                    obj.Rate = clsCommon.myCdbl(gv1.CurrentRow.Cells("COLTAXRATE" + strii).Value)
        ''                    obj.BaseAmt = clsCommon.myCdbl(gv1.CurrentRow.Cells("COLTAXBASEAMT" + strii).Value)
        ''                    obj.TaxAmt = clsCommon.myCdbl(gv1.CurrentRow.Cells("COLTAXAMT" + strii).Value)
        ''                    obj.isSurTax = clsCommon.myCBool(gv1.CurrentRow.Cells("ISSURTAX" + strii).Value)
        ''                    obj.SurTax = clsCommon.myCstr(gv1.CurrentRow.Cells("SURTAXCODE" + strii).Value)
        ''                    obj.IsTaxable = clsCommon.myCBool(gv1.CurrentRow.Cells("ISTAXABLE" + strii).Value)
        ''                    frm.ArrIn.Add(obj)
        ''                End If
        ''            Next

        ''            frm.ShowDialog()
        ''            If frm.ArrOut IsNot Nothing AndAlso frm.ArrOut.Count > 0 Then
        ''                BlankTaxDetailsCurrentRowWihtRowNo(gv1.CurrentRow.Index)
        ''                For ii As Integer = 0 To frm.ArrOut.Count - 1
        ''                    Dim strii As String = clsCommon.myCstr(ii + 1)
        ''                    gv1.CurrentRow.Cells("COLTAX" + strii).Value = frm.ArrOut(ii).AuthorityCode
        ''                    gv1.CurrentRow.Cells("COLTAXRATE" + strii).Value = frm.ArrOut(ii).Rate
        ''                    gv1.CurrentRow.Cells("COLTAXBASEAMT" + strii).Value = frm.ArrOut(ii).BaseAmt
        ''                    gv1.CurrentRow.Cells("COLTAXAMT" + strii).Value = frm.ArrOut(ii).TaxAmt
        ''                    gv1.CurrentRow.Cells("ISSURTAX" + strii).Value = frm.ArrOut(ii).isSurTax
        ''                    gv1.CurrentRow.Cells("SURTAXCODE" + strii).Value = frm.ArrOut(ii).SurTax
        ''                    gv1.CurrentRow.Cells("ISTAXABLE" + strii).Value = frm.ArrOut(ii).IsTaxable
        ''                Next
        ''                gv1.CurrentRow.Cells(colTotTaxAmt).Value = frm.dblTotTax
        ''                UpdateAllTotals()
        ''            End If
        ''        End If
        ''    End If
        ''Catch ex As Exception
        ''    common.clsCommon.MyMessageBoxShow(ex.Message)
        ''End Try
    End Sub

    Private Sub gv1_CurrentColumnChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gv1.CurrentColumnChanged
        'If gv1.RowCount > 0 Then
        '    Dim intCurrRow As Integer = gv1.CurrentRow.Index
        '    gv1.CurrentRow.Cells(colLineNo).Value = clsCommon.myCdbl(intCurrRow + 1)
        '    If intCurrRow = gv1.Rows.Count - 1 Then
        '        gv1.Rows.AddNew()

        '        gv1.CurrentRow = gv1.Rows(intCurrRow)
        '    End If
        '    ApplyQuickMode()
        'End If
    End Sub

    Private Sub RadMenuItem4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim Sql As String = "select '' as [Date], '' as Vendor, 0 as Amount,'' as LocSegment"
        transportSql.ExporttoExcel(Sql, Me)
    End Sub

    Private Sub RadMenuItem5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim type As String = "I"
        funfillimport(type)
    End Sub
    '----------------------For Credit Note---------------------
    Private Sub RadMenuItem6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim type As String = "C"
        funfillimport(type)
    End Sub
    '----------------------For Debit Note------------------------

    Private Sub RadMenuItem7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim type As String = "D"
        funfillimport(type)
    End Sub

    Sub funfillimport(ByRef type As String)
        Dim BalanceType As String = type
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim Counter As Integer = 0
        Dim qry As String = "select top 1 Account_Code,Description from TSPL_GL_ACCOUNTS where LEN(Account_Code)>6 order by Account_Code"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        Dim strACCode As String = clsCommon.myCstr(dt.Rows(0)("Account_Code"))
        Dim strACName As String = clsCommon.myCstr(dt.Rows(0)("Description"))
        If transportSql.importExcel(gv, "Date", "Vendor", "Amount", "LocSegment") Then
            Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
            clsCommon.ProgressBarShow()
            Try
                For Each grow As GridViewRowInfo In gv.Rows
                    Dim strVendor As String = clsCommon.myCstr(grow.Cells("Vendor").Value)
                    Dim dblAmt As Double = clsCommon.myCdbl(grow.Cells("Amount").Value)
                    Dim strDate As String = clsCommon.GetPrintDate(grow.Cells("Date").Value, "dd/MM/yyyy")
                    Dim segment As String = clsCommon.myCstr(grow.Cells("LocSegment").Value)

                    If clsCommon.myLen(strVendor) <= 0 Then
                        Throw New Exception("Vendor not found")
                    End If
                    If clsCommon.myLen(grow.Cells("Date").Value) <= 0 Then
                        Throw New Exception("Transactin Date is not found")
                    End If
                    If dblAmt <= 0 Then
                        Throw New Exception("Amount not found")
                    End If
                    Counter += 1
                    Dim obj As New clsVedorInvoiceHead()
                    'obj.Document_No = txtDocNo.Value
                    obj.Invoice_Entry_Date = strDate
                    obj.Vendor_Code = strVendor
                    obj.loc_code = segment
                    Dim objVendor As clsVendorMaster = clsVendorMaster.GetData(strVendor, trans)
                    obj.Vendor_Name = objVendor.Vendor_Name
                    obj.Vendor_Invoice_No = "Opening Balance " + clsCommon.myCstr(Counter)
                    obj.Vendor_Invoice_Date = strDate
                    obj.Account_Set = objVendor.Vendor_Account
                    'obj.Document_Type = "I"
                    obj.Document_Type = BalanceType
                    obj.Order_No = "C" ''if it is C then can't be put in GL entry

                    obj.On_Hold = False

                    obj.Tax_Group = "NILL"

                    obj.Due_Date = strDate
                    obj.Discount_Base = dblAmt
                    obj.Discount_Amount = 0
                    obj.Amount_Less_Discount = dblAmt
                    obj.Document_Total = dblAmt
                    obj.Balance_Amt = dblAmt


                    obj.Tax_Calculation_Type = EnumTaxCalucationType.Automatic


                    dt = clsDBFuncationality.GetDataTable("select Payable_Account,Discount_Account from TSPL_VENDOR_ACCOUNT_SET where Acct_Set_Code='" + obj.Account_Set + "'", trans)

                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                        obj.Vendor_Control_AC = clsCommon.myCstr(dt.Rows(0)("Payable_Account"))
                        If clsCommon.myCdbl(obj.Discount_Amount) > 0 Then
                            obj.Discount_GL_AC = clsCommon.myCstr(dt.Rows(0)("Discount_Account"))
                        End If
                    End If


                    obj.Total_Add_Charge = 0



                    obj.Arr = New List(Of clsVedorInvoiceDetail)
                    ''For Each grow As GridViewRowInfo In gv1.Rows
                    Dim objTr As New clsVedorInvoiceDetail()
                    objTr.Detail_Line_No = 1





                    '---------------------------------------------------

                    'For retriving Control Account for Vendor

                    'Dim segment As String = clsCommon.myCstr(grow.Cells("LocSegment").Value)

                    Dim qry1 As String = "select TSPL_VENDOR_ACCOUNT_SET.Payable_Account ,tspl_gl_accounts.Description   from TSPL_VENDOR_MASTER   " & _
                                        " left outer join TSPL_VENDOR_ACCOUNT_SET  on TSPL_VENDOR_ACCOUNT_SET.Acct_Set_Code=TSPL_VENDOR_MASTER.Vendor_Account  " & _
                                       " left outer join tspl_gl_accounts on  TSPL_VENDOR_ACCOUNT_SET.Payable_Account=tspl_gl_accounts.Account_Code " & _
                                        " where TSPL_VENDOR_MASTER.Vendor_Code='" + strVendor + "' "




                    Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qry1, trans)
                    Dim strACName1 As String
                    Dim strACCode1 As String
                    If dt1.Rows.Count > 0 Then
                        strACCode1 = clsCommon.myCstr(dt1.Rows(0)("Payable_Account"))
                        strACName1 = clsCommon.myCstr(dt1.Rows(0)("Description"))
                    Else
                        Throw New Exception("Account does not exist for Vendor '" + strVendor + "'")
                    End If


                    '-------------For  Segment code overwriting
                    ' Dim acccontrol As String
                    'Dim acccontdesc As String

                    Dim AccountFinal As String = strACCode1.Substring(0, (clsCommon.myLen(strACCode1) - 4)) + "-" + segment

                    Dim qryAcc As String = "select Account_Code,Description from TSPL_GL_ACCOUNTS where Account_Code='" + AccountFinal + "'"
                    Dim dtfinal As DataTable = clsDBFuncationality.GetDataTable(qryAcc, trans)
                    Dim FinalAcc As String
                    Dim FinalDesc As String
                    If dtfinal.Rows.Count > 0 Then
                        FinalAcc = clsCommon.myCstr(dtfinal.Rows(0)("Account_Code"))
                        FinalDesc = clsCommon.myCstr(dtfinal.Rows(0)("Description"))
                    Else
                        Throw New Exception("Account does not exist for Vendor '" + strVendor + "'")
                    End If

                    objTr.GL_Account_Code = FinalAcc
                    objTr.GL_Account_Desc = FinalDesc
                    '---------------------------------------------------




                    'objTr.GL_Account_Code = strACCode
                    'objTr.GL_Account_Desc = strACName
                    objTr.Amount = dblAmt
                    objTr.Discount_Per = 0
                    objTr.Discount = 0
                    objTr.Amount_less_Discount = dblAmt

                    objTr.Total_Tax = 0
                    objTr.Total_Amount = dblAmt

                    objTr.is_Unclaimed_Tax = False ''clsCommon.myCBool(grow.Cells(colIsUnclaimedTax).Value)

                    If (clsCommon.myLen(objTr.GL_Account_Code) > 0) And objTr.Amount <> 0 Then
                        obj.Arr.Add(objTr)
                    End If

                    obj.SaveData(obj, True, trans)
                Next

                clsCommon.ProgressBarHide()
                trans.Commit()
                common.clsCommon.MyMessageBoxShow(Me, "Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                clsCommon.ProgressBarHide()
                trans.Rollback()
                common.clsCommon.MyMessageBoxShow(Me, "Error at Rowno " + clsCommon.myCstr(Counter) + Environment.NewLine + ex.Message, Me.Text, MessageBoxButtons.OK, RadMessageIcon.Error)
            Finally
                clsCommon.ProgressBarHide()
                Me.Controls.Remove(gv)
            End Try
        End If
    End Sub

    Private Sub txtVendorInvoiceNo_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtVendorInvoiceNo.TextChanged
        'If txtlocation.Value = "" Then
        '    common.clsCommon.MyMessageBoxShow("Please first select Location")
        '    txtlocation.Focus()
        '    Return
        'End If
    End Sub

    Private Sub txtlocation__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtlocation._MYValidating
        Try
            'Dim qry As String = "select Segment_code as Code ,Description  from TSPL_GL_SEGMENT_CODE "
            Dim qry As String = "select distinct(Segment_code) as Code ,Description  from TSPL_GL_SEGMENT_CODE left outer join TSPL_LOCATION_MASTER on TSPL_GL_SEGMENT_CODE .Segment_code =TSPL_LOCATION_MASTER .Loc_Segment_Code "
            Dim WhrCls As String = "Seg_No = '7'"
            If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                WhrCls += "  and  TSPL_LOCATION_MASTER.Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
            End If
            txtlocation.Value = clsCommon.ShowSelectForm("GLsegmentcode", qry, "Code", WhrCls, txtlocation.Value, "Code", isButtonClicked)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub mbtnExportApTransaction_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim Sql As String = "select 'dd-MMM-yyyy' as [Date], '' as Vendor,'' as VendorInvoiceNo,'I or D or C' as Type,'' as LocSegment,'' as TaxGroup,'' as GLAccount1,0 as GLAmount1,'' as GLAccount2,0 as GLAmount2,'' as GLAccount3,0 as GLAmount3,'' as GLAccount4,0 as GLAmount4,'' as GLAccount5,0 as GLAmount5"
        transportSql.ExporttoExcel(Sql, Me)
    End Sub

    Private Sub mbtnImportApTransaction_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'TransactionHeadCalculation()
    End Sub

    Private Sub RadMenuItem8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem8.Click
        If clsCommon.myLen(ReportID) > 0 Then
            gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = ReportID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv1.SaveLayout(obj.GridLayout)
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            obj.GridColumns = gv1.ColumnCount
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully", Me.Text)
            End If

            ''richa agarwal regarding memory leakage

            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
            obj = Nothing
            ''---------------
        End If
    End Sub

    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(ReportID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(ReportID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gv1.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gv1.Columns.Count - 1 Step ii + 1
                        gv1.Columns(ii).IsVisible = False
                        gv1.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gv1.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub

    Private Sub RadMenuItem9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem9.Click
        clsGridLayout.DeleteData(ReportID, objCommonVar.CurrentUserCode)
    End Sub

    Private Sub gv1_CellDoubleClick(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellDoubleClick
        Try
            If e.Column Is gv1.Columns(colTotTaxAmt) AndAlso rbtnTaxCalAutomatic.IsChecked AndAlso Not clsCommon.myCBool(e.Row.Cells(colIsUnclaimedTax).Value) Then
                Dim frm As New FrmPOItemTaxDetails()
                frm.strLineNo = clsCommon.myCstr(gv1.CurrentRow.Cells(colLineNo).Value)
                frm.strItemCode = clsCommon.myCstr(gv1.CurrentRow.Cells(colACCode).Value)
                frm.strItemName = clsCommon.myCstr(gv1.CurrentRow.Cells(colACName).Value)
                frm.dblTotTax = clsCommon.myCdbl(gv1.CurrentRow.Cells(colTotTaxAmt).Value)
                frm.dblAmtAfterDis = clsCommon.myCdbl(gv1.CurrentRow.Cells(colAmtAfterDis).Value)
                If clsCommon.myLen(frm.strItemCode) > 0 Then
                    frm.ArrIn = New List(Of clsTempItemTaxDetails)
                    For ii As Integer = 1 To 10
                        Dim strii As String = clsCommon.myCstr(ii)
                        Dim obj As New clsTempItemTaxDetails()
                        obj.AuthorityCode = clsCommon.myCstr(gv1.CurrentRow.Cells("COLTAX" + strii).Value)
                        If clsCommon.myLen(obj.AuthorityCode) > 0 Then
                            obj.AuthorityName = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Tax_Code_Desc from TSPL_TAX_MASTER WHERE Tax_Code='" + obj.AuthorityCode + "'"))
                            obj.Rate = clsCommon.myCdbl(gv1.CurrentRow.Cells("COLTAXRATE" + strii).Value)
                            obj.BaseAmt = clsCommon.myCdbl(gv1.CurrentRow.Cells("COLTAXBASEAMT" + strii).Value)
                            obj.TaxAmt = clsCommon.myCdbl(gv1.CurrentRow.Cells("COLTAXAMT" + strii).Value)
                            obj.isSurTax = clsCommon.myCBool(gv1.CurrentRow.Cells("ISSURTAX" + strii).Value)
                            obj.SurTax = clsCommon.myCstr(gv1.CurrentRow.Cells("SURTAXCODE" + strii).Value)
                            obj.IsTaxable = clsCommon.myCBool(gv1.CurrentRow.Cells("ISTAXABLE" + strii).Value)
                            frm.ArrIn.Add(obj)
                        End If
                    Next

                    frm.ShowDialog()
                    If frm.ArrOut IsNot Nothing AndAlso frm.ArrOut.Count > 0 Then
                        BlankTaxDetailsCurrentRowWihtRowNo(gv1.CurrentRow.Index)
                        For ii As Integer = 0 To frm.ArrOut.Count - 1
                            Dim strii As String = clsCommon.myCstr(ii + 1)
                            gv1.CurrentRow.Cells("COLTAX" + strii).Value = frm.ArrOut(ii).AuthorityCode
                            gv1.CurrentRow.Cells("COLTAXRATE" + strii).Value = frm.ArrOut(ii).Rate
                            gv1.CurrentRow.Cells("COLTAXBASEAMT" + strii).Value = frm.ArrOut(ii).BaseAmt
                            gv1.CurrentRow.Cells("COLTAXAMT" + strii).Value = frm.ArrOut(ii).TaxAmt
                            gv1.CurrentRow.Cells("ISSURTAX" + strii).Value = frm.ArrOut(ii).isSurTax
                            gv1.CurrentRow.Cells("SURTAXCODE" + strii).Value = frm.ArrOut(ii).SurTax
                            gv1.CurrentRow.Cells("ISTAXABLE" + strii).Value = frm.ArrOut(ii).IsTaxable
                        Next
                        gv1.CurrentRow.Cells(colTotTaxAmt).Value = frm.dblTotTax
                        UpdateAllTotals()
                    End If
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub BlankTaxDetails(ByVal intRowNo As Integer)
        For ii As Integer = 1 To 10
            Dim strII As String = clsCommon.myCstr(ii)
            If intRowNo < 0 Then
                gv1.CurrentRow.Cells(clsCommon.myCstr("colTax" + strII)).Value = Nothing
                gv1.CurrentRow.Cells(clsCommon.myCstr("COLTAXBASEAMT" + strII)).Value = Nothing
                gv1.CurrentRow.Cells(clsCommon.myCstr("colTaxRate" + strII)).Value = Nothing
                gv1.CurrentRow.Cells(clsCommon.myCstr("colTaxAmt" + strII)).Value = Nothing
                gv1.CurrentRow.Cells(clsCommon.myCstr("ISTAXABLE" + strII)).Value = Nothing
                gv1.CurrentRow.Cells(clsCommon.myCstr("ISSURTAX" + strII)).Value = Nothing
                gv1.CurrentRow.Cells(clsCommon.myCstr("SURTAXCODE" + strII)).Value = Nothing
            Else
                gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTax" + strII)).Value = Nothing
                gv1.Rows(intRowNo).Cells(clsCommon.myCstr("COLTAXBASEAMT" + strII)).Value = Nothing
                gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTaxRate" + strII)).Value = Nothing
                gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTaxAmt" + strII)).Value = Nothing
                gv1.Rows(intRowNo).Cells(clsCommon.myCstr("ISTAXABLE" + strII)).Value = Nothing
                gv1.Rows(intRowNo).Cells(clsCommon.myCstr("ISSURTAX" + strII)).Value = Nothing
                gv1.Rows(intRowNo).Cells(clsCommon.myCstr("SURTAXCODE" + strII)).Value = Nothing
            End If
        Next
    End Sub


    Sub SetitemWiseTaxSetting(ByVal isChangeRate As Boolean, ByVal isForCurrentRow As Boolean)
        ''Dim qry As String = "select TSPL_TAX_GROUP_DETAILS.Tax_Group_Code ,TSPL_TAX_GROUP_MASTER.Tax_Group_Desc,Tax_Code,Tax_Code_Desc,Surtax,Surtax_Tax_Code,(select Tax_Rate from TSPL_TAX_RATES WHERE Tax_Rate_Code=1 AND Tax_Code=TSPL_TAX_GROUP_DETAILS.Tax_Code and TSPL_TAX_RATES.Tax_Type='P') AS TaxRate,Taxable from TSPL_TAX_GROUP_DETAILS left outer join TSPL_TAX_GROUP_MASTER on TSPL_TAX_GROUP_MASTER.Tax_Group_Code=TSPL_TAX_GROUP_DETAILS.Tax_Group_Code where TSPL_TAX_GROUP_DETAILS.Tax_Group_Code='" + txtTaxGroup.Value + "' and TSPL_TAX_GROUP_MASTER.Tax_Group_Type='P' and TSPL_TAX_GROUP_DETAILS.Tax_Group_Type='P' order by Trans_Code"
        Dim dt As DataTable = clsTaxGroupMaster.GetTaxDetails(txtTaxGroup.Value)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            If isForCurrentRow Then
                BlankTaxDetails(gv1.CurrentRow.Index)
                If clsCommon.myLen(gv1.CurrentRow.Cells(colACCode)) > 0 Then
                    Dim ii As Integer = 1
                    For Each dr As DataRow In dt.Rows
                        Dim strII As String = clsCommon.myCstr(ii)
                        gv1.CurrentRow.Cells(clsCommon.myCstr("colTax" + strII)).Value = clsCommon.myCstr(dr("Tax_Code"))
                        If isChangeRate Then
                            gv1.CurrentRow.Cells(clsCommon.myCstr("colTaxRate" + strII)).Value = clsCommon.myCdbl(dr("TaxRate"))
                        End If
                        gv1.CurrentRow.Cells(clsCommon.myCstr("ISTAXABLE" + strII)).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Taxable")), "Y") = CompairStringResult.Equal)
                        gv1.CurrentRow.Cells(clsCommon.myCstr("ISSURTAX" + strII)).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Surtax")), "Y") = CompairStringResult.Equal)
                        gv1.CurrentRow.Cells(clsCommon.myCstr("SURTAXCODE" + strII)).Value = clsCommon.myCstr(dr("Surtax_Tax_Code"))
                        gv1.CurrentRow.Cells(clsCommon.myCstr("ISEXCISABLE" + strII)).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Excisable")), "Y") = CompairStringResult.Equal)
                        ii = ii + 1
                    Next
                End If
            Else
                For intRowNo As Integer = 0 To gv1.Rows.Count - 1
                    BlankTaxDetails(intRowNo)
                    If clsCommon.myLen(gv1.Rows(intRowNo).Cells(colACCode)) > 0 Then
                        Dim ii As Integer = 1
                        For Each dr As DataRow In dt.Rows
                            Dim strII As String = clsCommon.myCstr(ii)
                            gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTax" + strII)).Value = clsCommon.myCstr(dr("Tax_Code"))
                            If isChangeRate Then
                                gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTaxRate" + strII)).Value = clsCommon.myCdbl(dr("TaxRate"))
                            End If
                            gv1.Rows(intRowNo).Cells(clsCommon.myCstr("ISTAXABLE" + strII)).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Taxable")), "Y") = CompairStringResult.Equal)
                            gv1.Rows(intRowNo).Cells(clsCommon.myCstr("ISSURTAX" + strII)).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Surtax")), "Y") = CompairStringResult.Equal)
                            gv1.Rows(intRowNo).Cells(clsCommon.myCstr("SURTAXCODE" + strII)).Value = clsCommon.myCstr(dr("Surtax_Tax_Code"))
                            gv1.Rows(intRowNo).Cells(clsCommon.myCstr("ISEXCISABLE" + strII)).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Excisable")), "Y") = CompairStringResult.Equal)
                            ii = ii + 1
                        Next
                    End If
                Next
            End If
        End If
    End Sub

    Sub SetitemWiseTaxOnlySetting()
        Dim dt As DataTable = clsTaxGroupMaster.GetTaxDetails(txtTaxGroup.Value)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            For intRowNo As Integer = 0 To gv1.Rows.Count - 1
                If clsCommon.myLen(gv1.Rows(intRowNo).Cells(colACCode)) > 0 Then
                    Dim ii As Integer = 1
                    For Each dr As DataRow In dt.Rows
                        Dim strII As String = clsCommon.myCstr(ii)
                        gv1.Rows(intRowNo).Cells(clsCommon.myCstr("ISTAXABLE" + strII)).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Taxable")), "Y") = CompairStringResult.Equal)
                        gv1.Rows(intRowNo).Cells(clsCommon.myCstr("ISSURTAX" + strII)).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Surtax")), "Y") = CompairStringResult.Equal)
                        gv1.Rows(intRowNo).Cells(clsCommon.myCstr("SURTAXCODE" + strII)).Value = clsCommon.myCstr(dr("Surtax_Tax_Code"))
                        gv1.Rows(intRowNo).Cells(clsCommon.myCstr("ISEXCISABLE" + strII)).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Excisable")), "Y") = CompairStringResult.Equal)
                        ii = ii + 1
                    Next
                End If
            Next
        End If
    End Sub


    Private Sub btnReverse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReverse.Click
        Try
            If common.clsCommon.MyMessageBoxShow(Me, "Reverse and Unpost the Current Document" + Environment.NewLine + "Are you sure", Me.Text, MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
                '' REASON FOR DELETE 
                Dim Reason As String = ""
                Dim frm As New FrmFreeTxtBox1
                frm.Text = "Remarks for Reverse"
                frm.ShowDialog()
                If clsCommon.myLen(frm.strRmks) <= 0 Then
                    Exit Sub
                Else
                    Reason = frm.strRmks
                End If
                If clsVedorInvoiceHead.ReverseAndUnpost(txtDocNo.Value) Then
                    saveCancelLog(Reason, "Reverse And Recreate", Nothing)
                    common.clsCommon.MyMessageBoxShow(Me, "Successfully Reversed and Recreated", Me.Text)
                    LoadData(txtDocNo.Value)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Public Sub FillVendorDetails()
        lblRegisterOrUnregister.Text = clsVendorMaster.GetVendorRegisterORNonRegister(TxtVendorNo.Value, Nothing)
        lblGstinNo.Text = clsVendorMaster.GetVendorGSTINNo(TxtVendorNo.Value, Nothing)
    End Sub
End Class
