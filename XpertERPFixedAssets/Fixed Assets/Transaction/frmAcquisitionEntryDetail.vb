'--Updation By--[Pankaj Kumar Chaudhary]-agaist ticket no-[BM00000001771]
'===============BM00000003593========add balance on item finder========================
Imports common
Imports Telerik.WinControls.UI
Imports Telerik.WinControls
Imports XpertERPEngine

Public Class FrmAcquisitionEntryDetail
    Public obj As clsAcquisitionDetail = Nothing
    Public isPostedTransaction As Boolean = False
    Dim ShowCapexCodeandSubCode As Boolean = False
    Dim ReadOnlyTemplateFieldsOnAcqusition As Boolean = False
    Sub ReadonlyTaxNBook()
        If (clsCommon.myCstr(cboDepType.Text)) = "Formula" Then
            txtDepRate.Text = 0
            txtDepRate.ReadOnly = True
        Else
            txtDepRate.ReadOnly = False
        End If
        If (clsCommon.myCstr(cboTaxDepType.Text)) = "Formula" Then
            txtDepTaxRate.Text = 0
            txtDepTaxRate.ReadOnly = True
        Else
            txtDepTaxRate.ReadOnly = False
        End If
    End Sub

    Private Sub FrmAcquisitionEntryDetail_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        '' if below setting is on then 
        '' 1. Rename FA Template Master to Asset Category
        '' 2. Rename Asset Category Master to Asset Group
        '' 3. Rename Asset Group Master to Sub Group Master
        ReadOnlyTemplateFieldsOnAcqusition = If(clsFixedParameter.GetData(clsFixedParameterCode.ReadOnlyTemplateFieldsOnAcqusition, clsFixedParameterType.ReadOnlyTemplateFieldsOnAcqusition, Nothing) = 1, True, False)
        If ReadOnlyTemplateFieldsOnAcqusition Then
            lblTemplateCode.Text = "Asset Category"
            lblCategoryCode.Text = "Asset Group"
            lblGroupCode.Text = "Sub Group Code"
        End If
        'Dim objtr As New clsAcquisitionHead()
        'If objtr.Acquisition_Type = "Direct" Then
        '    txtItem.Enabled = False

        'End If
        txtAcquisitionDate.Value = clsCommon.GETSERVERDATE()
        txtStartDate.Value = txtAcquisitionDate.Value
        RadPageView1.SelectedPage = RadPageViewPage1
        LoadDepType()
        LoadTaxDepType()
        If obj IsNot Nothing Then

            lblAsset.Text = obj.Asset_Code
            lblAssetDesc.Text = obj.Asset_Name
            txtTemplate.Value = obj.Templete_Code
            lblTemplate.Text = obj.Templete_Name
            txtCategory.Value = obj.Category_code
            lblCategory.Text = obj.Category_Name
            txtCostCenter.Value = obj.CostCenter_Code
            lblCostCenter.Text = obj.CostCenter_Name
            txtGroup.Value = obj.Group_Code
            lblGroup.Text = obj.Group_Code_Name
            txtAccountSet.Value = obj.AcSet_Code
            lblAccountSet.Text = obj.AcSet_Code_Name
            If obj.Acqusition_Date.Year > 1 Then
                txtAcquisitionDate.Value = obj.Acqusition_Date
            End If

            txtDepMethod.Value = obj.Dep_Method_Code
            lblDepMethod.Text = obj.Dep_Method_Name
            txtDepPeriod.Value = obj.Dep_Period_Code
            lblDepPeriod.Text = obj.Dep_Period_Name
            txtDepMethodTax.Value = obj.Dep_Method_Tax_Code
            lblDepMethodTax.Text = obj.Dep_Method_Tax_Name
            If obj.Start_Date.Year > 1 Then
                txtStartDate.Value = obj.Start_Date
            End If

            txtStartDate.Checked = obj.Put_To_Use
            txtDepRate.Value = obj.Dep_Rate
            txtEstLife.Value = obj.Book_Estimated_Life
            txtSourceValue.Value = obj.Book_Source_value
            txtSourceOrgValue.Value = obj.Book_Source_Original_value

            txtSalvageRate.Value = obj.Book_Salvage_Rate
            If obj.Book_Salvage_Rate > 0 Then
                txtSalvageValue.Value = obj.Book_Source_value * obj.Book_Salvage_Rate / 100
            Else
                txtSalvageValue.Value = obj.Book_Salvage_Value
            End If

            txtAssetSpecification.Text = obj.Asset_Specification
            txtItem.Value = obj.Item_Code
            lblItem.Text = obj.Item_Name
            txtDepTaxRate.Value = obj.Dep_Tax_Rate
            txtTaxAmount.Value = obj.Total_Tax_Amt

            txtNetValue.Value = obj.Item_Net_Amt
            'If obj.Book_Dep_Type = "Formula" Then
            cboDepType.Text = obj.Book_Dep_Type
            '    txtDepRate.ReadOnly = False
            'Else
            '    cboDepType.Text = obj.Book_Dep_Type
            '    txtDepRate.ReadOnly = True
            'End If
            'If obj.Tax_Dep_Type = "Formula" Then
            cboTaxDepType.Text = obj.Tax_Dep_Type
            '    txtDepTaxRate.ReadOnly = False
            'Else
            '    cboTaxDepType.SelectedValue = obj.Tax_Dep_Type
            '    txtDepTaxRate.ReadOnly = True
            'End If
            fndcapexcode.Value = obj.Capex_Code
            If clsCommon.myLen(Me.fndcapexcode.Value) > 0 Then
                lbl_capexcode.Text = clsCapexMaster.GetName(Me.fndcapexcode.Value, Nothing)
            End If
            fndcapexsubcode.Value = obj.CapexSub_Code
            If clsCommon.myLen(Me.fndcapexsubcode.Value) > 0 Then
                lbl_capexsubcode.Text = clsCapexBudget.GetName(Me.fndcapexsubcode.Value, Nothing)
                lbl_Amount.Text = clsCapexBudget.GetBudget(Me.fndcapexsubcode.Value, Nothing)
                lbl_AmountWithTol.Text = clsCapexBudget.GetBudgetWithTolerence(Me.fndcapexsubcode.Value, Nothing)
                lblBalanceAmount.Text = clsCapexBudget.GetReBudget(Me.fndcapexsubcode.Value, obj.Acquisition_Code, Nothing, "AC-Capex")
                lblBalAmountWithTol.Text = clsCapexBudget.GetReBudgetWithTolerence(Me.fndcapexsubcode.Value, obj.Acquisition_Code, Nothing, "AC-Capex")
            End If



        End If

        If isPostedTransaction Then
            ''txtAssetSpecification.Enabled = True
            txtItem.Enabled = False
            txtTemplate.Enabled = False
            txtCategory.Enabled = False
            txtGroup.Enabled = False
            txtCostCenter.Enabled = False
            txtAccountSet.Enabled = False
            txtAcquisitionDate.Enabled = False

            'txtStartDate.Enabled = False
            txtSourceOrgValue.Enabled = False
            txtSourceValue.Enabled = False
            txtSalvageValue.Enabled = False
            txtEstLife.Enabled = False
        End If

        '===============================Added by preeti gupta========================================================
        ShowCapexCodeandSubCode = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.ShowOptionforSelectingCapexForFA, clsFixedParameterCode.ShowOptionforSelectingCapexForFA, Nothing)) = "1", True, False))
        '============================================================END==================================================================
        '' check for template ready only setting
        Dim ReadOnlyTemplateFiled As Boolean = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.ReadOnlyTemplateFieldsOnAcqusition, clsFixedParameterCode.ReadOnlyTemplateFieldsOnAcqusition, Nothing)) = "1", True, False))
        If ReadOnlyTemplateFiled Then
            readOnlyTemplateFields()
        End If

        If ShowCapexCodeandSubCode = True Then
            RadPageViewPage2.Enabled = True
        Else
            RadPageViewPage2.Enabled = False
        End If

        ReadonlyTaxNBook()
    End Sub
    Sub readOnlyTemplateFields()
        txtAssetSpecification.ReadOnly = True
        txtItem.Enabled = False
        txtTemplate.Enabled = False
        txtCategory.Enabled = False
        txtGroup.Enabled = False
        txtAccountSet.Enabled = False
        txtAcquisitionDate.ReadOnly = True
        txtCostCenter.Enabled = False
        txtDepMethod.Enabled = False
        txtDepMethodTax.Enabled = False
        txtDepPeriod.Enabled = False
        'txtStartDate.ReadOnly = True
        txtDepRate.ReadOnly = True
        txtDepTaxRate.ReadOnly = True
        txtEstLife.ReadOnly = True
        txtSourceOrgValue.ReadOnly = True
        txtSourceValue.ReadOnly = True
        txtTaxAmount.ReadOnly = True
        txtNetValue.ReadOnly = True
        txtSalvageRate.ReadOnly = True
        cboDepType.ReadOnly = True
        cboTaxDepType.ReadOnly = True
        txtSalvageValue.ReadOnly = True
        fndcapexsubcode.Enabled = False
        fndcapexcode.Enabled = False
    End Sub

    Private Sub txtTemplate__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtTemplate._MYValidating
        Dim qry As String = " select template_code as Code,template_Name as Name,  category_code,group_code,Acset_code,CostCenter_Code from TSPL_FA_TEMPLATE_MASTER "
        txtTemplate.Value = clsCommon.ShowSelectForm("ACQDETTemplate", qry, "Code", "", txtTemplate.Value, "", isButtonClicked)
        Dim obj As ClsTemplateMaster = ClsTemplateMaster.GetData(txtTemplate.Value, NavigatorType.Current)
        If obj IsNot Nothing AndAlso clsCommon.myLen(obj.category_code) > 0 Then
            lblTemplate.Text = obj.template_Name
            txtCategory.Value = obj.category_code
            lblCategory.Text = obj.category_Description
            txtGroup.Value = obj.group_code
            lblGroup.Text = obj.group_Description
            txtCostCenter.Value = obj.CostCenter_Code
            lblCostCenter.Text = obj.CostCenter_Description
            txtAccountSet.Value = obj.Acset_code
            lblAccountSet.Text = obj.Acset_Description
            txtDepMethod.Value = obj.Dep_Method_Code
            lblDepMethod.Text = obj.Dep_Method_Name
            txtDepMethodTax.Value = obj.Dep_Method_Tax_Code
            lblDepMethodTax.Text = obj.Dep_Method_Tax_Name
            txtDepPeriod.Value = obj.Dep_Period_Code
            lblDepPeriod.Text = obj.Dep_Period_Name
            txtStartDate.Value = obj.Start_Date
            txtDepRate.Value = obj.Dep_Rate
            txtEstLife.Value = obj.Book_Estimated_Life
            txtSourceValue.Value = obj.Book_Source_value
            txtSourceOrgValue.Value = obj.Book_Source_Original_value
            txtSalvageValue.Value = obj.Book_Salvage_Value
            txtSalvageRate.Value = obj.Book_Salvage_Rate
            If obj.Book_Dep_Type = "F" Then
                cboDepType.Text = "Formula"
            ElseIf obj.Book_Dep_Type = "M" Then
                cboDepType.Text = "Manual"
            Else
                cboDepType.Text = ""
            End If
            If obj.Tax_Dep_Type = "F" Then
                cboTaxDepType.Text = "Formula"
            ElseIf obj.Tax_Dep_Type = "M" Then
                cboTaxDepType.Text = "Manual"
            Else
                cboTaxDepType.Text = ""
            End If
            txtDepTaxRate.Text = obj.Dep_Tax_Rate
            txtNetValue.Text = obj.Book_Net_Value
        End If

    End Sub

    Private Sub txtDepMethod__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtDepMethod._MYValidating
        Dim qry As String = " select Code,Description from TSPL_DEPRECIATION_METHOD "
        txtDepMethod.Value = clsCommon.ShowSelectForm("ACQDETDepMethod", qry, "Code", "", txtDepMethod.Value, "", isButtonClicked)
        lblDepMethod.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description from TSPL_DEPRECIATION_METHOD where Code='" + txtDepMethod.Value + "'"))

    End Sub

    Private Sub txtDepPeriod__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtDepPeriod._MYValidating
        Dim qry As String = " select period_Code as Code,period_Desc as Description from TSPL_DEPRECIATION_PERIODS "
        txtDepPeriod.Value = clsCommon.ShowSelectForm("ACQDETDepPeriod", qry, "Code", "", txtDepPeriod.Value, "", isButtonClicked)
        lblDepPeriod.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  period_Desc from TSPL_DEPRECIATION_PERIODS where period_Code='" + txtDepPeriod.Value + "'"))
    End Sub

    Private Sub txtCategory__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtCategory._MYValidating
        Dim qry As String = "Select Category_Code as [Code], Description, Is_Default as [Default], Convert(VARCHAR,Last_Maintained_Date, 103) as [Last Maintained], Inactive from TSPL_ASSET_CATEGORY "
        txtCategory.Value = clsCommon.ShowSelectForm("AssetCategorySelector", qry, "Code", "", txtCategory.Value, "", isButtonClicked)
        lblCategory.Text = clsDBFuncationality.getSingleValue("Select Description from TSPL_ASSET_CATEGORY where Category_Code='" + txtCategory.Value + "' ")
    End Sub

    Private Sub txtGroup__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtGroup._MYValidating
        If clsCommon.myLen(txtCategory.Value) > 0 Then
            Dim qry As String = "select Group_Code as Code,Description from TSPL_ASSET_GROUP "
            Dim Whrcls As String = " Category_Code='" & txtCategory.Value & "'"
            txtGroup.Value = clsCommon.ShowSelectForm("AssetCategoryAQD", qry, "Code", Whrcls, txtGroup.Value, "", isButtonClicked)
            lblGroup.Text = clsDBFuncationality.getSingleValue("Select Description from TSPL_ASSET_GROUP where Group_Code='" + txtGroup.Value + "' ")
        Else
            clsCommon.MyMessageBoxShow(Me, "Select Category first.", Me.Text)
        End If
    End Sub

    Private Sub txtCostCenter__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtCostCenter._MYValidating
        Dim qry As String = "select CostCenter_Code as Code,CostCenter_name as Name from  TSPL_FA_COST_CENTER_MASTER"
        txtCostCenter.Value = clsCommon.ShowSelectForm("CostCenterACQDEt", qry, "Code", "", txtCostCenter.Value, "", isButtonClicked)
        lblCostCenter.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select CostCenter_name from TSPL_FA_COST_CENTER_MASTER WHERE CostCenter_Code ='" + txtCostCenter.Value + "'"))
    End Sub

    Private Sub txtAccountSet__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtAccountSet._MYValidating
        Dim qry As String = "Select AcSet_Code as [Code], AcSet_Desc as [Description] from TSPL_Dep_AccountSet"
        txtAccountSet.Value = clsCommon.ShowSelectForm("AccSetFinderACDE", qry, "Code", "", txtAccountSet.Value, "Code", isButtonClicked)
        txtAccountSet.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select AcSet_Desc from TSPL_Dep_AccountSet WHERE AcSet_Code ='" + txtAccountSet.Value + "'"))
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click

        'obj = Nothing
        Me.Close()
    End Sub

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click

        If txtStartDate.Value.Date < txtAcquisitionDate.Value.Date Then
            clsCommon.MyMessageBoxShow(Me, "Start date can not less than Acquisition Date.", Me.Text)
            Exit Sub
        End If
        If txtSalvageRate.Value < 0 Or txtSalvageRate.Value >= 100 Then
            clsCommon.MyMessageBoxShow(Me, "Solvage % must be between 0 and 100(>=0 and <100).", Me.Text)
            Exit Sub
        End If
        If txtEstLife.Text <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Please fill Estimate life", Me.Text)
            Exit Sub
        End If
        obj = New clsAcquisitionDetail()
        obj.Asset_Code = lblAsset.Text
        obj.Asset_Name = lblAssetDesc.Text
        obj.Templete_Code = txtTemplate.Value
        obj.Templete_Name = lblTemplate.Text
        obj.Category_code = txtCategory.Value
        obj.Category_Name = lblCategory.Text
        obj.CostCenter_Code = txtCostCenter.Value
        obj.CostCenter_Name = lblCostCenter.Text
        obj.Group_Code = txtGroup.Value
        obj.Group_Code_Name = lblGroup.Text
        obj.AcSet_Code = txtAccountSet.Value
        obj.AcSet_Code_Name = lblAccountSet.Text
        obj.Acqusition_Date = txtAcquisitionDate.Value
        obj.Dep_Method_Code = txtDepMethod.Value
        obj.Dep_Method_Name = lblDepMethod.Text
        obj.Dep_Method_Tax_Code = txtDepMethodTax.Value
        obj.Dep_Method_Tax_Name = lblDepMethodTax.Text
        obj.Dep_Period_Code = txtDepPeriod.Value
        obj.Dep_Period_Name = lblDepPeriod.Text
        obj.Put_To_Use = txtStartDate.Checked
        obj.Start_Date = txtStartDate.Value
        obj.Dep_Rate = txtDepRate.Value
        obj.Book_Estimated_Life = txtEstLife.Value
        obj.Book_Source_value = txtSourceValue.Value
        obj.Book_Source_Original_value = txtSourceOrgValue.Value
        obj.Book_Salvage_Rate = txtSalvageRate.Value
        obj.Book_Salvage_Value = txtSalvageValue.Value
        obj.Asset_Specification = txtAssetSpecification.Text
        obj.Item_Code = txtItem.Value
        obj.Item_Name = lblItem.Text
        obj.Dep_Tax_Rate = txtDepTaxRate.Value
        obj.Total_Tax_Amt = txtTaxAmount.Value
        obj.Item_Net_Amt = txtNetValue.Value
        obj.Tax_Dep_Type = cboTaxDepType.Text
        obj.Book_Dep_Type = cboDepType.Text
        obj.Capex_Code = fndcapexcode.Value
        obj.CapexSub_Code = fndcapexsubcode.Value
        If clsCommon.myLen(obj.CapexSub_Code) > 0 Then
            obj.IsCapex = 1
        End If
        Me.Close()
    End Sub
    Public Sub LoadDepType()
        Try
            Dim dt As New DataTable()
            dt.Columns.Add("code", GetType(String))
            dt.Columns.Add("Name", GetType(String))
            Dim dr As DataRow = Nothing

            dr = dt.NewRow()
            dr("code") = "F"
            dr("Name") = "Formula"
            dt.Rows.Add(dr)

            dr = dt.NewRow()
            dr("code") = "M"
            dr("Name") = "Manual"
            dt.Rows.Add(dr)

            cboDepType.DataSource = dt
            cboDepType.DisplayMember = "name"
            cboDepType.ValueMember = "Code"
        Catch ex As Exception
        End Try
    End Sub

    Public Sub LoadTaxDepType()
        Try
            Dim dt As New DataTable()
            dt.Columns.Add("Code", GetType(String))
            dt.Columns.Add("Name", GetType(String))
            Dim dr As DataRow = Nothing

            dr = dt.NewRow()
            dr("Code") = "F"
            dr("Name") = "Formula"
            dt.Rows.Add(dr)

            dr = dt.NewRow()
            dr("Code") = "M"
            dr("Name") = "Manual"
            dt.Rows.Add(dr)

            cboTaxDepType.DataSource = dt
            cboTaxDepType.ValueMember = "Code"
            cboTaxDepType.DisplayMember = "Name"

        Catch ex As Exception
        End Try
    End Sub

    Private Sub txtItem__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtItem._MYValidating
        'Dim obj As clsItemMaster = clsItemMaster.FinderForItem(txtItem.Value, "A", True, isButtonClicked, "", "")
        Dim Icode As String = clsItemMaster.getFinder(" tspl_item_master.item_type='A'", txtItem.Value, isButtonClicked)
        If Icode IsNot Nothing AndAlso clsCommon.myLen(Icode) > 0 Then
            txtItem.Value = Icode
            lblItem.Text = clsItemMaster.GetItemName(Icode, Nothing)
        Else
            txtItem.Value = ""
            lblItem.Text = ""
        End If
    End Sub


    Private Sub txtSourceOrgValue_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs)
        txtSourceValue.Value = txtSourceOrgValue.Value
    End Sub

    Private Sub txtDepMethodTax__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtDepMethodTax._MYValidating
        Dim qry As String = " select Code,Description from TSPL_DEPRECIATION_METHOD "
        txtDepMethodTax.Value = clsCommon.ShowSelectForm("ACQDETDepTMethod", qry, "Code", "", txtDepMethodTax.Value, "", isButtonClicked)
        lblDepMethodTax.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description from TSPL_DEPRECIATION_METHOD where Code='" + txtDepMethodTax.Value + "'"))
    End Sub

    Private Sub txtSalvageRate_TextChanged(sender As Object, e As EventArgs) Handles txtSalvageRate.TextChanged, txtNetValue.TextChanged
        txtSalvageValue.Text = txtSourceValue.Value * txtSalvageRate.Value / 100
    End Sub

    'Private Sub txtSalvageValue_TextChanged(sender As Object, e As EventArgs) Handles txtSalvageValue.TextChanged
    '    txtSalvageRate.Text = 0
    'End Sub
    Private Sub txtSourceValue_TextChanged(sender As Object, e As EventArgs) Handles txtSourceValue.TextChanged
        If (clsCommon.myCdbl(txtSourceValue.Text)) > 0 Then
            txtNetValue.Text = txtSourceValue.Text
        End If
    End Sub

    Private Sub txtTaxAmount_TextChanged(sender As Object, e As EventArgs) Handles txtTaxAmount.TextChanged
        If (clsCommon.myCdbl(txtTaxAmount.Text)) > 0 Then
            txtNetValue.Text = (clsCommon.myCdbl(txtSourceValue.Text) + clsCommon.myCdbl(txtTaxAmount.Text))
        End If
    End Sub

    Private Sub cboDepType_SelectedIndexChanged(sender As Object, e As UI.Data.PositionChangedEventArgs) Handles cboDepType.SelectedIndexChanged
        If (clsCommon.myCstr(cboDepType.Text)) = "Formula" Then
            txtDepRate.Text = 0
            txtDepRate.ReadOnly = True
        Else
            txtDepRate.ReadOnly = False
        End If
    End Sub

    Private Sub cboTaxDepType_SelectedIndexChanged(sender As Object, e As UI.Data.PositionChangedEventArgs) Handles cboTaxDepType.SelectedIndexChanged
        If (clsCommon.myCstr(cboTaxDepType.Text)) = "Formula" Then
            txtDepTaxRate.Text = 0
            txtDepTaxRate.ReadOnly = True
        Else
            txtDepTaxRate.ReadOnly = False
        End If
    End Sub

    Private Sub fndcapexsubcode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndcapexsubcode._MYValidating
        Try
            lbl_capexcode.Text = ""
            fndcapexcode.Value = ""
            Me.fndcapexsubcode.Value = clsCapexBudget.getFinder("", fndcapexsubcode.Value, isButtonClicked)
            If clsCommon.myLen(Me.fndcapexsubcode.Value) > 0 Then
                lbl_capexsubcode.Text = clsCapexBudget.GetName(Me.fndcapexsubcode.Value, Nothing)
                fndcapexcode.Value = clsCapexBudget.GetCapexCode(Me.fndcapexsubcode.Value, Nothing)
                lbl_Amount.Text = clsCapexBudget.GetBudget(Me.fndcapexsubcode.Value, Nothing)
                lbl_AmountWithTol.Text = clsCapexBudget.GetBudgetWithTolerence(Me.fndcapexsubcode.Value, Nothing)
                lblBalanceAmount.Text = clsCapexBudget.GetReBudget(Me.fndcapexsubcode.Value, "", Nothing)
                lblBalAmountWithTol.Text = clsCapexBudget.GetReBudgetWithTolerence(Me.fndcapexsubcode.Value, "", Nothing)
                If clsCommon.myLen(Me.fndcapexcode.Value) > 0 Then
                    lbl_capexcode.Text = clsCapexMaster.GetName(Me.fndcapexcode.Value, Nothing)
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub fndcapexcode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndcapexcode._MYValidating

    End Sub

    Private Sub MyLabel17_Click(sender As Object, e As EventArgs) Handles MyLabel17.Click

    End Sub
End Class
