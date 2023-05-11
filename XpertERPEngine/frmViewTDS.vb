Imports common
Public Class FrmViewTDS
    Inherits FrmMainTranScreen
    Public ObjIn As clsRemittance
    Public ObjReturn As clsRemittance
    Dim isInSideLoadData As Boolean = False
    Public isCanceButtonPressed As Boolean = False
    Public isForService As Boolean = False
    Public strVendorCode As String = ""

    Private Sub FrmViewTDS_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        RadGroupBox1.Enabled = False
        RadGroupBox2.Enabled = False
        RadGroupBox3.Enabled = False

        isInSideLoadData = True
        'SetUserMgmtNew()
        LoadQuarterData()
        'AddHandler txtNatureOfDeduction.txtValue.TextChanged, AddressOf txtNatureOfDed_TxtChanged
        'AddHandler txtBranchCode.txtValue.TextChanged, AddressOf txtBranchCode_TxtChanged
        'AddHandler txtFiscalYear.txtValue.TextChanged, AddressOf txtFiscalYear_TxtChanged

        'AddHandler txtNatureOfDeduction.txtValue.Leave, AddressOf txtNatureOfDed_Leave
        'AddHandler txtBranchCode.txtValue.Leave, AddressOf txtBranchCode_Leave
        'AddHandler txtFiscalYear.txtValue.Leave, AddressOf txtFiscalYear_Leave
        chkApplyTDS.Checked = False
        If (ObjIn IsNot Nothing) Then
            txtNatureOfDeduction.Value = ObjIn.Deduction_Code
            txtNatureOfDeduction.Value = ObjIn.Deduction_Code
            txtBrachCode.Value = ObjIn.Branch_Code
            txtBrachCode.Value = ObjIn.Branch_Code
            txtFiscalYear.Value = ObjIn.Fiscal_Year
            txtFiscalYear.Value = ObjIn.Fiscal_Year
            cboQuarter.SelectedValue = ObjIn.Quarter
            ChkOverrideTDS.Checked = ObjIn.IsTDSOverride
            txtTDSBaseCal.Value = Math.Round(ObjIn.Calculated_TDS_Base, 2)
            txtTDSBaseAct.Value = Math.Round(ObjIn.Actual_TDS_Base, 2)
            txtTDSPerCal.Value = Math.Round(ObjIn.TDS_Per, 2)
            txtTDSPerAct.Value = Math.Round(ObjIn.TDS_Per, 2)
            txtTDSAmtCal.Value = Math.Round(ObjIn.Calculated_TDS, 2)
            txtTDSAmtAct.Value = Math.Round(ObjIn.Actual_TDS, 2)
            txtSurchargePerCal.Value = Math.Round(ObjIn.Surcharge_Per, 2)
            txtSurchargePerAct.Value = Math.Round(ObjIn.Surcharge_Per, 2)
            txtSurchargeAmtCal.Value = Math.Round(ObjIn.Calculated_Surcharge, 2)
            txtSurchargeAmtAct.Value = Math.Round(ObjIn.Actual_Surcharge, 2)
            txtEcessPerCal.Value = Math.Round(ObjIn.Edu_Cess_Per, 2)
            txtEcessPerAct.Value = Math.Round(ObjIn.Edu_Cess_Per, 2)
            txtEcessAmtCal.Value = Math.Round(ObjIn.Calculated_Edu_Cess, 2)
            txtEcessAmtAct.Value = Math.Round(ObjIn.Actual_Edu_Cess, 2)

            txtHCessPerCal.Value = Math.Round(ObjIn.Sec_Educess_Per, 2)
            txtHCessPerAct.Value = Math.Round(ObjIn.Sec_Educess_Per, 2)
            txtHCessAmtCal.Value = Math.Round(ObjIn.Calculated_Sec_Educess, 2)
            txtHCessAmtAct.Value = Math.Round(ObjIn.Actual_Sec_Educess, 2)

            txtTotTDSCal.Value = Math.Round(ObjIn.Calculated_Total_TDS, 2)
            txtTotTDSAct.Value = Math.Round(ObjIn.Actual_Total_TDS, 2)
            chkApplyTDS.Checked = ObjIn.IsApplyTDS
            If (Not ObjIn.IsApplyTDS) Then
                'RadGroupBox2.Enabled = chkApplyTDS.Checked
                ChkOverrideTDS.Enabled = chkApplyTDS.Checked
                If Not chkApplyTDS.Checked Then
                    ChkOverrideTDS.Checked = False
                    txtNatureOfDeduction.Value = ""
                    txtNatureOfDeduction.Value = ""
                    lblNatureOf.Text = ""
                End If
            End If
            ApplyTDS()
        End If
        isInSideLoadData = False
    End Sub
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmViewTDS)
        'If Not (MyBase.isReadFlag) Then
        '    common.clsCommon.MyMessageBoxShow("Permission Denied")
        '    Me.Close()
        '    Exit Sub
        'End If
        'btnSave.Visible = MyBase.isModifyFlag
        'btnPost.Visible = MyBase.isPostFlag
        'btnDelete.Visible = MyBase.isDeleteFlag
    End Sub

   

    'Private Sub txtBranchCode_TxtChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Dim qry As String = "select  Branch_Code ,Branch_Name from TSPL_TDS_BRANCH_MASTER where Branch_Code='" + txtBrachCode.Value + "'"
    '    Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
    '    If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
    '        txtBrachCode.Value = clsCommon.myCstr(dt.Rows(0)("Branch_Code"))
    '        txtBranchCode.txtValue.Text = clsCommon.myCstr(dt.Rows(0)("Branch_Code"))
    '        lblBranchCode.Text = clsCommon.myCstr(dt.Rows(0)("Branch_Name"))
    '    Else
    '        lblBranchCode.Text = ""
    '    End If
    'End Sub

    'Private Sub txtFiscalYear_TxtChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Dim qry As String = "select Year_Name from TSPL_TDS_FINANCIAL_YEAR where Year_Name='" + txtFiscalYear.Value + "'"
    '    Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
    '    If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
    '        txtFiscalYear.Value = clsCommon.myCstr(dt.Rows(0)("Year_Name"))
    '        txtFiscalYear.txtValue.Text = clsCommon.myCstr(dt.Rows(0)("Year_Name"))
    '    Else
    '        'lblTermName.Text = ""
    '    End If
    'End Sub

    'Private Sub txtNatureOfDed_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    If (clsCommon.myLen(txtNatureOfDeduction.Value) > 0) Then
    '        Dim qry As String = "select 1 from TSPL_TDS_DEDUCTION_HEAD where Deduction_Code='" + txtNatureOfDeduction.Value + "'"
    '        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
    '        If (dt Is Nothing OrElse dt.Rows.Count <= 0) Then
    '            common.clsCommon.MyMessageBoxShow("Nature of Deduction is Not Exist")
    '            txtNatureOfDeduction.Value = ""
    '            txtNatureOfDeduction.Value = ""
    '            txtNatureOfDeduction.Focus()
    '        End If
    '    End If
    'End Sub

    'Private Sub txtBranchCode_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    If (clsCommon.myLen(txtBrachCode.Value) > 0) Then
    '        Dim qry As String = "select 1 from TSPL_TDS_BRANCH_MASTER where Branch_Code='" + txtBrachCode.Value + "'"
    '        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
    '        If (dt Is Nothing OrElse dt.Rows.Count <= 0) Then
    '            common.clsCommon.MyMessageBoxShow("Branch code is Not Exist")
    '            txtBrachCode.Value = ""
    '            txtBranchCode.txtValue.Text = ""
    '            txtBranchCode.Focus()
    '        End If
    '    End If
    'End Sub

    'Private Sub txtFiscalYear_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    If (clsCommon.myLen(txtFiscalYear.Value) > 0) Then
    '        Dim qry As String = "select 1 from TSPL_TDS_FINANCIAL_YEAR where Year_Name='" + txtFiscalYear.Value + "'"
    '        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
    '        If (dt Is Nothing OrElse dt.Rows.Count <= 0) Then
    '            common.clsCommon.MyMessageBoxShow("Fisacal year Not Exist")
    '            txtFiscalYear.Value = ""
    '            txtFiscalYear.txtValue.Text = ""
    '            txtFiscalYear.Focus()
    '        End If
    '    End If
    'End Sub

    Sub LoadQuarterData()
        Dim dt As New DataTable
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))
        Dim dr As DataRow = dt.NewRow()
        dr("Code") = "First"
        dr("Name") = "First"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Second"
        dr("Name") = "Second"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Third"
        dr("Name") = "Third"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Fourth"
        dr("Name") = "Fourth"
        dt.Rows.Add(dr)

        cboQuarter.DataSource = dt
        cboQuarter.ValueMember = "Code"
        cboQuarter.DisplayMember = "Name"

    End Sub

    Private Sub ChkOverrideTDS_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles ChkOverrideTDS.ToggleStateChanged
        txtTDSBaseAct.ReadOnly = Not ChkOverrideTDS.Checked

        If Not ChkOverrideTDS.Checked Then
            txtTDSBaseAct.Value = txtTDSBaseCal.Value
            CalculateActualTDS()
        End If

    End Sub

    Private Sub chkApplyTDS_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkApplyTDS.ToggleStateChanged
        ApplyTDS()
    End Sub

    Private Sub ApplyTDS()
        'RadGroupBox2.Enabled = chkApplyTDS.Checked
        ChkOverrideTDS.Enabled = chkApplyTDS.Checked
        If Not chkApplyTDS.Checked Then
            ChkOverrideTDS.Checked = False
            txtNatureOfDeduction.Value = ""
            txtNatureOfDeduction.Value = ""
            lblNatureOf.Text = ""
            txtTDSPerCal.Value = 0
            txtTDSPerAct.Value = 0

            txtSurchargePerCal.Value = 0
            txtSurchargePerAct.Value = 0

            txtEcessPerCal.Value = 0
            txtEcessPerAct.Value = 0

            txtHCessPerCal.Value = 0
            txtHCessPerAct.Value = 0

            CalculateActualTDS()
        End If
    End Sub

    'Private Sub txtNatureOfDed_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Try
    '        txtNatureOfDeduction.ConnectionString = connectSql.SqlCon()
    '        txtNatureOfDeduction.Query = "select Deduction_Code,Description from TSPL_TDS_DEDUCTION_HEAD where Comp_Code='" + objCommonVar.CurrentCompanyCode + "'"
    '        txtNatureOfDeduction.ValueToSelect = "Deduction_Code"
    '        txtNatureOfDeduction.Caption = "Description"
    '        txtNatureOfDeduction.ValueToSelect1 = "Deduction_Code"
    '    Catch ex As Exception
    '        common.clsCommon.MyMessageBoxShow(ex.Message)
    '    End Try
    'End Sub

    'Private Sub txtBranchCode_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Try
    '        txtBranchCode.ConnectionString = connectSql.SqlCon()
    '        txtBranchCode.Query = "select Branch_Code as Code,Branch_Name as Name from TSPL_TDS_BRANCH_MASTER where Comp_Code='" + objCommonVar.CurrentCompanyCode + "'"
    '        txtBranchCode.ValueToSelect = "Code"
    '        txtBranchCode.Caption = "Code"
    '        txtBranchCode.ValueToSelect1 = "Code"
    '    Catch ex As Exception
    '        common.clsCommon.MyMessageBoxShow(ex.Message)
    '    End Try
    'End Sub

    'Private Sub txtFiscalYear_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Try
    '        txtFiscalYear.ConnectionString = connectSql.SqlCon()
    '        txtFiscalYear.Query = "select Year_Name as [Fiscal Year],From_Date as [From Date],To_Date as [To Date] from TSPL_TDS_FINANCIAL_YEAR where Comp_Code='" + objCommonVar.CurrentCompanyCode + "'"
    '        txtFiscalYear.ValueToSelect = "Fiscal Year"
    '        txtFiscalYear.Caption = "Fiscal Year"
    '        txtFiscalYear.ValueToSelect1 = "Fiscal Year"
    '    Catch ex As Exception
    '        common.clsCommon.MyMessageBoxShow(ex.Message)
    '    End Try
    'End Sub

    Private Sub RadButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'ObjReturn = Nothing
        isCanceButtonPressed = True
        Me.Close()
    End Sub

    Private Sub txtTDSBaseAct_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtTDSBaseAct.Validating
        CalculateActualTDS()

    End Sub

    Sub CalculateActualTDS()
        txtTDSAmtAct.Value = Math.Round((txtTDSBaseAct.Value * txtTDSPerAct.Value) / 100, 2)
        txtTDSAmtCal.Value = Math.Round((txtTDSBaseCal.Value * txtTDSPerCal.Value) / 100, 2)
        txtSurchargeAmtAct.Value = Math.Round((txtTDSBaseAct.Value * txtSurchargePerAct.Value) / 100, 2)
        txtSurchargeAmtCal.Value = Math.Round((txtTDSBaseCal.Value * txtSurchargePerCal.Value) / 100, 2)
        txtEcessAmtAct.Value = Math.Round((txtTDSBaseAct.Value * txtEcessPerAct.Value) / 100, 2)
        txtEcessAmtCal.Value = Math.Round((txtTDSBaseCal.Value * txtEcessPerCal.Value) / 100, 2)
        txtHCessAmtAct.Value = Math.Round((txtTDSBaseAct.Value * txtHCessPerAct.Value) / 100, 2)
        txtHCessAmtCal.Value = Math.Round((txtTDSBaseCal.Value * txtHCessPerCal.Value) / 100, 2)
        txtTotTDSAct.Value = Math.Round((txtTDSAmtAct.Value + txtSurchargeAmtAct.Value + txtEcessAmtAct.Value + txtHCessAmtAct.Value), 2)
        txtTotTDSCal.Value = Math.Round((txtTDSAmtCal.Value + txtSurchargeAmtCal.Value + txtEcessAmtCal.Value + txtHCessAmtCal.Value), 2)
    End Sub

    Private Sub btnOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOk.Click
        If Not chkApplyTDS.Checked Then
            ObjReturn = Nothing
            Me.Close()
            Exit Sub
        End If
        If (chkApplyTDS.Checked AndAlso clsCommon.myLen(txtNatureOfDeduction.Value) <= 0) Then
            common.clsCommon.MyMessageBoxShow("Please select Deduction Code")
            txtNatureOfDeduction.Focus()
            Return
        End If
        ObjReturn = New clsRemittance()
        ObjReturn = ObjIn
        ObjReturn.Deduction_Code = txtNatureOfDeduction.Value
        ObjReturn.Branch_Code = txtBrachCode.Value
        ObjReturn.Fiscal_Year = txtFiscalYear.Value
        ObjReturn.Quarter = clsCommon.myCstr(cboQuarter.SelectedValue)
        ObjReturn.IsTDSOverride = ChkOverrideTDS.Checked

        ObjReturn.Calculated_TDS_Base = txtTDSBaseCal.Value
        ObjReturn.Actual_TDS_Base = txtTDSBaseAct.Value

        ObjReturn.TDS_Per = txtTDSPerCal.Value
        ObjReturn.TDS_Per = txtTDSPerAct.Value
        ObjReturn.Calculated_TDS = txtTDSAmtCal.Value
        ObjReturn.Actual_TDS = txtTDSAmtAct.Value

        ObjReturn.Surcharge_Per = txtSurchargePerCal.Value
        ObjReturn.Surcharge_Per = txtSurchargePerAct.Value
        ObjReturn.Calculated_Surcharge = txtSurchargeAmtCal.Value
        ObjReturn.Actual_Surcharge = txtSurchargeAmtAct.Value

        ObjReturn.Edu_Cess_Per = txtEcessPerCal.Value
        ObjReturn.Edu_Cess_Per = txtEcessPerAct.Value
        ObjReturn.Calculated_Edu_Cess = txtEcessAmtCal.Value
        ObjReturn.Actual_Edu_Cess = txtEcessAmtAct.Value

        ObjReturn.Sec_Educess_Per = txtHCessPerCal.Value
        ObjReturn.Sec_Educess_Per = txtHCessPerAct.Value
        ObjReturn.Calculated_Sec_Educess = txtHCessAmtCal.Value
        ObjReturn.Actual_Sec_Educess = txtHCessAmtAct.Value

        ObjReturn.Calculated_Total_TDS = txtTotTDSCal.Value
        ObjReturn.Actual_Total_TDS = txtTotTDSAct.Value

        ObjReturn.IsApplyTDS = chkApplyTDS.Checked
        ObjReturn.IsTDSOverride = ChkOverrideTDS.Checked
        Me.Close()
    End Sub

    Private Sub RadButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadButton1.Click
        Dim frm As New FrmPWD(Nothing)
        frm.strCode = clsFixedParameterCode.ViewTDSPwd
        frm.strType = clsFixedParameterType.ViewTDSPwd
        frm.ShowDialog()
        If frm.isPasswordCorrect Then
            RadGroupBox1.Enabled = True
            RadGroupBox2.Enabled = True
            RadGroupBox3.Enabled = True
            ChkOverrideTDS.Checked = True
        End If
       
    End Sub

    
    Private Sub txtNatureOfDeduction__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtNatureOfDeduction._MYValidating
        Dim qry As String = "select Deduction_Code as Code,Description from TSPL_TDS_DEDUCTION_HEAD   "
        Dim WhrCls As String = " Comp_Code='" + objCommonVar.CurrentCompanyCode + "'"
        txtNatureOfDeduction.Value = clsCommon.ShowSelectForm("NatOfDedVTDS", qry, "Code", WhrCls, txtNatureOfDeduction.Value, "Code", isButtonClicked)
        txtNatureOfDed_TxtChanged()
    End Sub

    Private Sub txtNatureOfDed_TxtChanged()
        Dim qry As String = "select Deduction_Code,Description from TSPL_TDS_DEDUCTION_HEAD  where Deduction_Code='" + txtNatureOfDeduction.Value + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            txtNatureOfDeduction.Value = clsCommon.myCstr(dt.Rows(0)("Deduction_Code"))
            txtNatureOfDeduction.Value = clsCommon.myCstr(dt.Rows(0)("Deduction_Code"))
            lblNatureOf.Text = clsCommon.myCstr(dt.Rows(0)("Description"))
            If Not isInSideLoadData Then
                Dim objDedDetails As clsTDSDeductionDetails = clsTDSDeductionDetails.GetApplicableTDRate(txtNatureOfDeduction.Value, clsCommon.myCdbl(ObjIn.Document_Amount), Nothing, isForService, strVendorCode)
                If (objDedDetails IsNot Nothing) Then

                    txtTDSPerCal.Value = objDedDetails.TDS
                    txtTDSPerAct.Value = objDedDetails.TDS

                    txtSurchargePerCal.Value = objDedDetails.Surcharge
                    txtSurchargePerAct.Value = objDedDetails.Surcharge

                    txtEcessPerCal.Value = objDedDetails.Educess
                    txtEcessPerAct.Value = objDedDetails.Educess

                    txtHCessPerCal.Value = objDedDetails.Seceducess
                    txtHCessPerAct.Value = objDedDetails.Seceducess

                    CalculateActualTDS()
                End If
            End If
        Else
            lblNatureOf.Text = ""
        End If
    End Sub

    Private Sub txtBrachCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtBrachCode._MYValidating
        Dim qry As String = "select Branch_Code as Code,Branch_Name as Name from TSPL_TDS_BRANCH_MASTER   "
        Dim WhrCls As String = " Comp_Code='" + objCommonVar.CurrentCompanyCode + "'"
        txtBrachCode.Value = clsCommon.ShowSelectForm("BrachVTDS", qry, "Code", WhrCls, txtBrachCode.Value, "Code", isButtonClicked)
        lblBrachCode.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Branch_Name  from TSPL_TDS_BRANCH_MASTER where Branch_Code ='" + txtBrachCode.Value + "'"))
    End Sub

    Private Sub txtFiscalYear__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtFiscalYear._MYValidating
        Dim qry As String = "select Year_Name as Code,From_Date as [From Date],To_Date as [To Date] from TSPL_TDS_FINANCIAL_YEAR "
        Dim WhrCls As String = " Comp_Code='" + objCommonVar.CurrentCompanyCode + "'"
        txtFiscalYear.Value = clsCommon.ShowSelectForm("FyearVTDS", qry, "Code", WhrCls, txtFiscalYear.Value, "Code", isButtonClicked)
    End Sub
End Class
