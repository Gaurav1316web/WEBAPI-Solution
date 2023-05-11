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
Imports XpertERPEngine

Public Class FrmTemplateMaster
    Inherits FrmMainTranScreen
    Dim Qry As String
    Dim userCode, companyCode As String
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private isNewEntry As Boolean = False
    Dim CheckDDLType As Boolean = False
    Dim CheckDTType As Boolean = False
    Dim ShowCapexCodeandSubCode As Boolean = False
    Dim ReadOnlyTemplateFieldsOnAcqusition As Boolean = False
    Dim AllowAssetBookChangeInTemplate As Boolean = False

#Region "Finder"


    Private Sub fndCategory__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean)
        Qry = "Select Category_Code as [Code], Description, Is_Default as [Default], Convert(VARCHAR,Last_Maintained_Date, 103) as [Last Maintained], Inactive from TSPL_ASSET_CATEGORY "
        fndCategory.Value = clsCommon.ShowSelectForm("Asset", Qry, "Code", "", fndCategory.Value, "", isButtonClicked)
        txtCategory.Text = clsDBFuncationality.getSingleValue("Select Description from TSPL_ASSET_CATEGORY where Category_Code='" + fndCategory.Value + "' ")
    End Sub

    Private Sub fndGroup__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean)
        Qry = "Select Group_Code as [Code], Description, Is_Default as [Default], Convert(VARCHAR,Last_Maintained_Date, 103) as [Last Maintained], Inactive from TSPL_ASSET_GROUP "
        fndGroup.Value = clsCommon.ShowSelectForm("Selector", Qry, "Code", "", fndGroup.Value, "", isButtonClicked)
        txtGroup.Text = clsDBFuncationality.getSingleValue("Select Description from TSPL_ASSET_GROUP where Group_Code='" + fndGroup.Value + "' ")

    End Sub

    Private Sub fndAccountSet__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean)
        Qry = "Select AcSet_Code as [Code], AcSet_Desc as [Description] from TSPL_Dep_AccountSet"
        fndAccountSet.Value = clsCommon.ShowSelectForm("Setder", Qry, "Code", "", fndAccountSet.Value, "Code", isButtonClicked)
        txtAccountSet.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select AcSet_Desc from TSPL_Dep_AccountSet WHERE AcSet_Code ='" + fndAccountSet.Value + "'"))

    End Sub

    Private Sub fndCostCenter__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean)
        Dim qry As String = "select CostCenter_Code as Code,CostCenter_name as Name from  TSPL_FA_COST_CENTER_MASTER"
        fndCostCenter.Value = clsCommon.ShowSelectForm("STER", qry, "Code", "", fndCostCenter.Value, "", isButtonClicked)
        txtCostCenter.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select CostCenter_name from TSPL_FA_COST_CENTER_MASTER WHERE CostCenter_Code ='" + fndCostCenter.Value + "'"))

    End Sub
#End Region

    Private Sub fndTemplateCode__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles fndTemplateCode._MYNavigator

        Try
            LoadData(fndTemplateCode.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub fndTemplateCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndTemplateCode._MYValidating
        Try
            Dim str As String = "select count(*) from TSPL_FA_TEMPLATE_MASTER where template_code ='" + fndTemplateCode.Value + "' "
            Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
            If no = 0 AndAlso isButtonClicked = False Then
                fndTemplateCode.MyReadOnly = False
            Else
                fndTemplateCode.MyReadOnly = True
            End If
            If fndTemplateCode.MyReadOnly OrElse isButtonClicked Then
                Dim qry As String = " select template_code as Code,  category_code,group_code,Acset_code from TSPL_FA_TEMPLATE_MASTER "
                fndTemplateCode.Value = clsCommon.ShowSelectForm("FA_TEMPLATE", qry, "Code", "", fndTemplateCode.Value, "", isButtonClicked)
                If fndTemplateCode.Value <> "" Then
                    LoadData(fndTemplateCode.Value, NavigatorType.Current)
                Else
                    Reset()
                End If
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message())
        End Try
    End Sub
#Region "Functions"
    Sub funDelete()
        Try
            If (myMessages.deleteConfirm()) Then
                If (ClsTemplateMaster.DeleteData(fndTemplateCode.Value)) Then
                    common.clsCommon.MyMessageBoxShow("Data Deleted Successfully ")
                    Reset()
                End If
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try

    End Sub
    Public Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.Template)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        rdbtnsave.Visible = MyBase.isModifyFlag
        'btnPost.Visible = MyBase.isPostFlag
        rdbtndelete.Visible = MyBase.isDeleteFlag
    End Sub

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Dim obj As ClsTemplateMaster = ClsTemplateMaster.GetData(strCode, NavTyep)
        If obj IsNot Nothing Then
            isNewEntry = False
            'RadPageView1.SelectedPage = RadPageViewPage1
            fndTemplateCode.Value = obj.template_code
            txttemplateName.Text = obj.template_Name
            fndCategory.Value = obj.category_code
            fndGroup.Value = obj.group_code
            fndAccountSet.Value = obj.Acset_code
            fndCostCenter.Value = obj.CostCenter_Code
            txtAssetBook.Value = obj.FA_Book_Code
            lblAssetBookDesc.Text = clsAssetBookMaster.GetName(txtAssetBook.Value, Nothing)
            'txtCategory.Text = clsDBFuncationality.getSingleValue("Select Description from TSPL_ASSET_CATEGORY where Category_Code='" + fndCategory.Value + "' ")
            'txtGroup.Text = clsDBFuncationality.getSingleValue("Select Description from TSPL_ASSET_GROUP where Group_Code='" + fndGroup.Value + "' ")
            'txtAccountSet.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select AcSet_Desc from TSPL_Dep_AccountSet WHERE AcSet_Code ='" + fndAccountSet.Value + "'"))
            'txtCostCenter.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select CostCenter_name from TSPL_FA_COST_CENTER_MASTER WHERE CostCenter_Code ='" + fndCostCenter.Value + "'"))
            txtCategory.Text = obj.category_Description
            txtGroup.Text = obj.group_Description
            txtAccountSet.Text = obj.Acset_Description
            txtCostCenter.Text = obj.CostCenter_Description

            '' Anubhooti 21-July-2014
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
            txtnetvalue.Text = obj.Book_Net_Value


            fndcapexcode.Value = obj.Capex_Code
            If clsCommon.myLen(Me.fndcapexcode.Value) > 0 Then
                lbl_capexcode.Text = clsCapexMaster.GetName(Me.fndcapexcode.Value, Nothing)
            End If
            fndcapexsubcode.Value = obj.CapexSub_Code
            If clsCommon.myLen(Me.fndcapexsubcode.Value) > 0 Then
                lbl_capexsubcode.Text = clsCapexBudget.GetName(Me.fndcapexsubcode.Value, Nothing)
                lbl_Amount.Text = clsCapexBudget.GetBudget(Me.fndcapexsubcode.Value, Nothing)
                lbl_AmountWithTol.Text = clsCapexBudget.GetBudgetWithTolerence(Me.fndcapexsubcode.Value, Nothing)
                lbl_BalAmount.Text = clsCapexBudget.GetReBudget(Me.fndcapexsubcode.Value, "", Nothing, "AC-Capex")
                lblBalAmountWithTolerenace.Text = clsCapexBudget.GetReBudgetWithTolerence(Me.fndcapexsubcode.Value, "", Nothing, "AC-Capex")
            End If

            fndTemplateCode.MyReadOnly = True
        End If
    End Sub
    Sub Reset()
        '' if below setting is on then 
        '' 1. Rename FA Template Master to Asset Category
        '' 2. Rename Asset Category Master to Asset Group
        '' 3. Rename Asset Group Master to Sub Group Master
        ReadOnlyTemplateFieldsOnAcqusition = If(clsFixedParameter.GetData(clsFixedParameterCode.ReadOnlyTemplateFieldsOnAcqusition, clsFixedParameterType.ReadOnlyTemplateFieldsOnAcqusition, Nothing) = 1, True, False)
        If ReadOnlyTemplateFieldsOnAcqusition Then
            Me.Text = "Asset Category Master"
            lblAssetCategory.Text = "Asset Group"
            lblAssetGroup.Text = "Asset Sub Group"
        End If
        RadPageView1.SelectedPage = RadPageViewPage1
        LoadDepType()
        LoadTaxDepType()
        fndTemplateCode.Value = ""
        fndCategory.Value = ""
        fndGroup.Value = ""
        fndAccountSet.Value = ""
        fndCostCenter.Value = ""
        txtCategory.Text = ""
        txtGroup.Text = ""
        txtAccountSet.Text = ""
        txtCostCenter.Text = ""
        txtDepMethod.Value = ""
        lblDepMethod.Text = ""
        txtDepMethodTax.Value = ""
        lblDepMethodTax.Text = ""
        txtDepPeriod.Value = ""
        lblDepPeriod.Text = ""
        txtDepRate.Text = ""
        txtDepTaxRate.Text = ""
        txtEstLife.Text = ""
        txtSourceValue.Text = ""
        txtSourceOrgValue.Text = ""
        txtSalvageValue.Text = ""
        fndTemplateCode.MyReadOnly = False
        txttemplateName.Text = ""
        txtStartDate.Value = clsCommon.GETSERVERDATE
        txtnetvalue.Text = ""
        txtDepRate.ReadOnly = True
        txtDepTaxRate.ReadOnly = True
        fndcapexsubcode.Value = ""
        fndcapexcode.Value = ""
        lbl_capexsubcode.Text = ""
        lbl_capexcode.Text = ""
        lbl_Amount.Text = ""
        ShowCapexCodeandSubCode = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.ShowOptionforSelectingCapexForFA, clsFixedParameterCode.ShowOptionforSelectingCapexForFA, Nothing)) = "1", True, False))
        If ShowCapexCodeandSubCode = True Then
            RadPageViewPage2.Enabled = True
        Else
            RadPageViewPage2.Enabled = False
        End If
    End Sub
    Sub SaveData()

        If MyBase.isModifyonPasswordFlag Then
            If clsPasswordCheckForMasters.CheckMasterPwd(clsUserMgtCode.Template, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
            Else
                Return
            End If
        End If
        If AllowToSave() Then
            Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
            Try
                Dim obj As New ClsTemplateMaster()
                obj.template_code = fndTemplateCode.Value
                obj.template_Name = txttemplateName.Text
                obj.category_code = fndCategory.Value
                obj.group_code = fndGroup.Value
                obj.Acset_code = fndAccountSet.Value
                obj.CostCenter_Code = fndCostCenter.Value
                obj.FA_Book_Code = txtAssetBook.Value
                '' Anubhooti 21-July-2014
                obj.Dep_Method_Code = txtDepMethod.Value
                obj.Dep_Method_Name = lblDepMethod.Text
                obj.Dep_Method_Tax_Code = txtDepMethodTax.Value
                obj.Dep_Method_Tax_Name = lblDepMethodTax.Text
                obj.Dep_Period_Code = txtDepPeriod.Value
                obj.Dep_Period_Name = lblDepPeriod.Text
                obj.Start_Date = txtStartDate.Value
                obj.Dep_Rate = txtDepRate.Value
                obj.Dep_Tax_Rate = txtDepTaxRate.Value
                obj.Book_Estimated_Life = txtEstLife.Value
                obj.Book_Source_value = txtSourceValue.Value
                obj.Book_Source_Original_value = txtSourceOrgValue.Value
                obj.Book_Salvage_Value = txtSalvageValue.Value
                obj.Book_Salvage_Rate = txtSalvageRate.Value
                obj.Book_Net_Value = txtnetvalue.Value
                obj.Book_Dep_Type = cboDepType.SelectedValue
                obj.Tax_Dep_Type = cboTaxDepType.SelectedValue
                obj.Capex_Code = fndcapexcode.Value
                obj.CapexSub_Code = fndcapexsubcode.Value
                Dim qry As Integer = clsDBFuncationality.getSingleValue("select count(template_code) from TSPL_FA_TEMPLATE_MASTER where template_code='" + obj.template_code + "'", trans)
                If (qry = 0) Then
                    isNewEntry = True
                Else
                    isNewEntry = False
                End If
                If (ClsTemplateMaster.SaveData(obj, isNewEntry, trans)) Then
                    trans.Commit()
                    clsCommon.MyMessageBoxShow("Data saved Successfully", Me.Text)
                    LoadData(obj.template_code, NavigatorType.Current)

                End If
            Catch ex As Exception
                trans.Rollback()
                clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
            End Try
        End If
    End Sub
    Private Function AllowToSave() As Boolean
        If (IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowAutoGenerateDocNoInMaster, clsFixedParameterCode.AllowAutoGenerateDocNoInMaster, Nothing)) = "1", True, False) = False) Then
            If clsCommon.myLen(clsCommon.myCstr(fndTemplateCode.Value)) <= 0 Then
                fndTemplateCode.Focus()
                Throw New Exception("Please Fill Template Code")
            End If
        End If
        If clsCommon.myLen(clsCommon.myCstr(txttemplateName.Text)) <= 0 Then
            fndCostCenter.Focus()
            Throw New Exception("Please Fill Template Name")
        End If
        'If clsCommon.myLen(clsCommon.myCstr(fndCostCenter.Value)) <= 0 Then
        '    fndCostCenter.Focus()
        '    Throw New Exception("Please Fill Cost Center")
        'End If
        If clsCommon.myLen(clsCommon.myCstr(fndAccountSet.Value)) <= 0 Then
            fndAccountSet.Focus()
            Throw New Exception("Please Fill AccountSet Code")
        End If
        If clsCommon.myLen(clsCommon.myCstr(fndCategory.Value)) <= 0 Then
            fndCategory.Focus()
            Throw New Exception("Please Fill Category Code")
        End If
        If clsCommon.myLen(clsCommon.myCstr(fndGroup.Value)) <= 0 Then
            fndGroup.Focus()
            Throw New Exception("Please Fill Group Code")
        End If
        If txtSalvageRate.Value > 100 Then
            txtSalvageRate.Focus()
            Throw New Exception("Salvage % can not be greater than 100. ")
        End If
        If cboDepType.SelectedValue <> "F" Then

            If clsCommon.myCdbl(txtDepRate.Text) = 0 Then
                txtDepRate.Focus()
                Throw New Exception("Please Fill Depriciation  Rate")
            End If
        End If
        If cboTaxDepType.SelectedValue <> "F" Then
            If clsCommon.myCdbl(txtDepTaxRate.Text) = 0 Then
                txtDepTaxRate.Focus()
                Throw New Exception("Please Fill Depriciation Tax Rate")
            End If
        End If
        Return True
    End Function
#End Region
#Region " Events"


    Private Sub rdbtnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbtnsave.Click
        SaveData()
    End Sub

    Private Sub rdbtndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbtndelete.Click
        funDelete()
    End Sub

    Private Sub rdbtnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbtnclose.Click
        Me.Close()
    End Sub

    Private Sub rdbtnnew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbtnnew.Click
        Reset()
    End Sub

    Private Sub FrmTemplateMaster_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso rdbtnnew.Enabled Then
            Reset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso rdbtnsave.Enabled Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso rdbtndelete.Enabled Then
            funDelete()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            Reset()
        End If
    End Sub


    Private Sub FrmTemplateMaster_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        SetUserMgmtNew() '       
        Reset()
        AllowAssetBookChangeInTemplate = If(clsFixedParameter.GetData(clsFixedParameterCode.AllowAssetBookChangeInTemplate, clsFixedParameterType.AllowAssetBookChangeInTemplate, Nothing) = 1, True, False)
        If AllowAssetBookChangeInTemplate Then
            txtAssetBook.Enabled = True
            lblAssetBookDesc.Enabled = True
            cboDepType.Enabled = True
            txtEstLife.Enabled = True
            txtnetvalue.Enabled = True
            txtSalvageRate.Enabled = True
            txtSalvageValue.Enabled = True
            txtSourceOrgValue.Enabled = True
            txtSourceValue.Enabled = True
            txtDepMethod.Enabled = True
            txtDepMethodTax.Enabled = True
            txtDepPeriod.Enabled = True
            txtDepRate.Enabled = True
            txtDepTaxRate.Enabled = True
            cboTaxDepType.Enabled = True
            txtStartDate.Enabled = True
        Else

            lblAssetBookDesc.Enabled = False
            cboDepType.Enabled = False
            txtEstLife.Enabled = False
            txtnetvalue.Enabled = False
            txtSalvageRate.Enabled = False
            txtSalvageValue.Enabled = False
            txtSourceOrgValue.Enabled = False
            txtSourceValue.Enabled = False
            txtDepMethod.Enabled = False
            txtDepMethodTax.Enabled = False
            txtDepPeriod.Enabled = False
            txtDepRate.Enabled = False
            txtDepTaxRate.Enabled = False
            cboTaxDepType.Enabled = False
            txtStartDate.Enabled = False
        End If
    End Sub

    'Public Sub SetUserMgmtNew()
    '    ''MyBase.SetUserMgmt(clsUserMgtCode.Template)
    '    If Not (MyBase.isReadFlag) Then
    '        common.clsCommon.MyMessageBoxShow("Permission Denied")
    '        Me.Close()
    '        Exit Sub
    '    End If
    '    rdbtnsave.Visible = MyBase.isModifyFlag
    '    rdbtndelete.Visible = MyBase.isDeleteFlag
    'End Sub

#End Region

    Private Sub txtAssetBook__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtAssetBook._MYValidating
        'Dim qry As String = " select Code,Description from TSPL_DEPRECIATION_METHOD "
        'txtDepMethod.Value = clsCommon.ShowSelectForm("ACQDETDepMethod", qry, "Code", "", txtDepMethod.Value, "", isButtonClicked)
        txtAssetBook.Value = clsAssetBookMaster.getFinder("", txtAssetBook.Value, isButtonClicked)
        Dim obj As New clsAssetBookMaster()
        obj = clsAssetBookMaster.GetData(txtAssetBook.Value, NavigatorType.Current)
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Book_Code) > 0) Then
            txtAssetBook.Value = obj.Book_Code
            lblAssetBookDesc.Text = obj.Book_Name
            cboDepType.SelectedValue = obj.Book_Dep_Type
            txtEstLife.Text = obj.Book_Estimated_Life
            txtnetvalue.Text = obj.Book_Net_Value
            txtSalvageRate.Text = obj.Book_Salvage_Rate
            txtSalvageValue.Text = obj.Book_Salvage_Value
            txtSourceOrgValue.Text = obj.Book_Source_Original_value
            txtSourceValue.Text = obj.Book_Source_value
            txtDepMethod.Value = obj.Dep_Method_Code
            txtDepMethodTax.Value = obj.Dep_Method_Tax_Code
            txtDepPeriod.Value = obj.Dep_Period_Code
            txtDepRate.Value = obj.Dep_Rate
            txtDepTaxRate.Value = obj.Dep_Tax_Rate
            cboTaxDepType.SelectedValue = obj.Tax_Dep_Type
            txtStartDate.Value = obj.Start_Date

        Else
            txtAssetBook.Value = ""
            lblAssetBookDesc.Text = ""
            cboDepType.SelectedValue = ""
            txtEstLife.Text = ""
            txtnetvalue.Text = ""
            txtSalvageRate.Text = ""
            txtSalvageValue.Text = ""
            txtSourceOrgValue.Text = ""
            txtSourceValue.Text = ""
            txtDepMethod.Value = ""
            txtDepMethodTax.Value = ""
            txtDepPeriod.Value = ""
            txtDepRate.Value = 0
            txtDepTaxRate.Value = 0
            cboTaxDepType.SelectedValue = ""


        End If
    End Sub
    Private Sub txtDepMethod__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtDepMethod._MYValidating
        Dim qry As String = " select Code,Description from TSPL_DEPRECIATION_METHOD "
        txtDepMethod.Value = clsCommon.ShowSelectForm("ACQDETDepMethod", qry, "Code", "", txtDepMethod.Value, "", isButtonClicked)
        lblDepMethod.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description from TSPL_DEPRECIATION_METHOD where Code='" + txtDepMethod.Value + "'"))
    End Sub

    Private Sub txtDepMethodTax__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtDepMethodTax._MYValidating
        Dim qry As String = " select Code,Description from TSPL_DEPRECIATION_METHOD "
        txtDepMethodTax.Value = clsCommon.ShowSelectForm("ACQDETDepTMethod", qry, "Code", "", txtDepMethodTax.Value, "", isButtonClicked)
        lblDepMethodTax.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description from TSPL_DEPRECIATION_METHOD where Code='" + txtDepMethodTax.Value + "'"))
    End Sub

    Private Sub txtDepPeriod__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtDepPeriod._MYValidating
        Dim qry As String = " select period_Code as Code,period_Desc as Description from TSPL_DEPRECIATION_PERIODS "
        txtDepPeriod.Value = clsCommon.ShowSelectForm("ACQDETDepPeriod", qry, "Code", "", txtDepPeriod.Value, "", isButtonClicked)
        lblDepPeriod.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  period_Desc from TSPL_DEPRECIATION_PERIODS where period_Code='" + txtDepPeriod.Value + "'"))
    End Sub

    Private Sub fndCategory__MYValidating_1(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndCategory._MYValidating
        Dim qry As String = "Select Category_Code as [Code], Description, Is_Default as [Default], Convert(VARCHAR,Last_Maintained_Date, 103) as [Last Maintained], Inactive from TSPL_ASSET_CATEGORY "
        fndCategory.Value = clsCommon.ShowSelectForm("AssetCategorySelector", qry, "Code", "", fndCategory.Value, "", isButtonClicked)
        txtCategory.Text = clsDBFuncationality.getSingleValue("Select Description from TSPL_ASSET_CATEGORY where Category_Code='" + fndCategory.Value + "' ")
        If clsCommon.myLen(fndCategory.Value) > 0 Then
            Dim qry1 As String = "Select AcSet_Code as [Code], AcSet_Desc as [Description] from TSPL_ASSET_CATEGORY left join TSPL_Dep_AccountSet on TSPL_Dep_AccountSet.AcSet_Code =TSPL_ASSET_CATEGORY.Default_Account_Set where Category_Code ='" & fndCategory.Value & "' "
            fndAccountSet.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry1))
            txtAccountSet.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select AcSet_Desc from TSPL_Dep_AccountSet WHERE AcSet_Code ='" + fndAccountSet.Value + "'"))
            fndGroup.Value = Nothing
            txtGroup.Text = ""
        End If
    End Sub

    Private Sub fndGroup__MYValidating_1(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndGroup._MYValidating
        'If clsCommon.myLen(fndCategory.Value) > 0 Then
        Dim qry As String = "select Group_Code as Code,Description,Category_Code as [Category Code] from TSPL_ASSET_GROUP "
        Dim Whrcls As String = "" ''" Category_Code='" & fndCategory.Value & "'"
        If clsCommon.myLen(fndCategory.Value) > 0 Then
            Whrcls = " Category_Code='" & fndCategory.Value & "'"
        End If

        fndGroup.Value = clsCommon.ShowSelectForm("AssetCategoryAQD", qry, "Code", Whrcls, fndGroup.Value, "", isButtonClicked)
        txtGroup.Text = clsDBFuncationality.getSingleValue("Select Description from TSPL_ASSET_GROUP where Group_Code='" + fndGroup.Value + "' ")
        If clsCommon.myLen(fndCategory.Value) <= 0 Then
            qry = "select Category_Code from TSPL_ASSET_GROUP where Group_Code='" & fndGroup.Value & "'"
            fndCategory.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, Nothing))
            txtCategory.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description from TSPL_ASSET_CATEGORY where Category_Code='" & fndCategory.Value & "'"))
            fndAccountSet.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Default_Account_Set from TSPL_ASSET_CATEGORY where Category_Code='" & fndCategory.Value & "'"))
            txtAccountSet.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select AcSet_Desc from TSPL_Dep_AccountSet WHERE AcSet_Code ='" & fndAccountSet.Value & "'"))
        End If
        'Else
        'clsCommon.MyMessageBoxShow("Select Category first.")
        'End If

    End Sub

    Private Sub fndAccountSet__MYValidating_1(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndAccountSet._MYValidating

        Dim qry As String = "Select AcSet_Code as [Code], AcSet_Desc as [Description] from TSPL_Dep_AccountSet"
        fndAccountSet.Value = clsCommon.ShowSelectForm("AccSetFinderACDE", qry, "Code", "", fndAccountSet.Value, "Code", isButtonClicked)
        txtAccountSet.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select AcSet_Desc from TSPL_Dep_AccountSet WHERE AcSet_Code ='" + fndAccountSet.Value + "'"))
    End Sub

    Private Sub fndCostCenter__MYValidating_1(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndCostCenter._MYValidating
        Dim qry As String = "select CostCenter_Code as Code,CostCenter_name as Name from  TSPL_FA_COST_CENTER_MASTER"
        fndCostCenter.Value = clsCommon.ShowSelectForm("CostCenterACQDEt", qry, "Code", "", fndCostCenter.Value, "", isButtonClicked)
        txtCostCenter.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select CostCenter_name from TSPL_FA_COST_CENTER_MASTER WHERE CostCenter_Code ='" + fndCostCenter.Value + "'"))
    End Sub

    Private Sub txtSalvageRate_TextChanged(sender As Object, e As EventArgs) Handles txtSalvageRate.TextChanged
        'txtSalvageValue.Text = 0
        txtSalvageValue.Text = txtSourceValue.Value * txtSalvageRate.Value / 100
    End Sub

    'Private Sub txtSalvageValue_TextChanged(sender As Object, e As EventArgs) Handles txtSalvageValue.TextChanged
    '    txtSalvageRate.Text = 0
    'End Sub
    Public Sub LoadDepType()
        'If CheckDDLType = False Then
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

            cboDepType.DataSource = dt
            cboDepType.ValueMember = "Code"
            cboDepType.DisplayMember = "Name"

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        'End If

    End Sub

    Public Sub LoadTaxDepType()
        'If CheckDTType = False Then
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
            dr("code") = "M"
            dr("Name") = "Manual"
            dt.Rows.Add(dr)

            cboTaxDepType.DataSource = dt
            cboTaxDepType.ValueMember = "Code"
            cboTaxDepType.DisplayMember = "Name"

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

        'End If
    End Sub

    Private Sub txtSourceValue_TextChanged(sender As Object, e As EventArgs) Handles txtSourceValue.TextChanged
        If (clsCommon.myCdbl(txtSourceValue.Text)) > 0 Then
            txtnetvalue.Text = txtSourceValue.Text
            txtSalvageValue.Text = txtSourceValue.Value * txtSalvageRate.Value / 100
        End If
    End Sub

    Private Sub cboDepType_SelectedIndexChanged(sender As Object, e As UI.Data.PositionChangedEventArgs) Handles cboDepType.SelectedIndexChanged
        'If CheckDDLType = True Then
        If (clsCommon.myCstr(cboDepType.SelectedValue)) = "F" Then
            txtDepRate.Text = 0
            txtDepRate.ReadOnly = True
        Else
            txtDepRate.ReadOnly = False
        End If

        'End If

    End Sub
    Private Sub cboTaxDepType_SelectedIndexChanged(sender As Object, e As UI.Data.PositionChangedEventArgs) Handles cboTaxDepType.SelectedIndexChanged
        'If CheckDTType = True Then
        If (clsCommon.myCstr(cboTaxDepType.SelectedValue)) = "F" Then
            txtDepTaxRate.Text = 0
            txtDepTaxRate.ReadOnly = True
        Else
            txtDepTaxRate.ReadOnly = False
        End If

        'End If
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
                lbl_BalAmount.Text = clsCapexBudget.GetReBudget(Me.fndcapexsubcode.Value, "", Nothing)
                lblBalAmountWithTolerenace.Text = clsCapexBudget.GetReBudgetWithTolerence(Me.fndcapexsubcode.Value, "", Nothing)
                If clsCommon.myLen(Me.fndcapexcode.Value) > 0 Then
                    lbl_capexcode.Text = clsCapexMaster.GetName(Me.fndcapexcode.Value, Nothing)
                End If
            Else
                lbl_capexsubcode.Text = ""
                fndcapexcode.Value = ""
                lbl_Amount.Text = ""
                lbl_AmountWithTol.Text = ""
                lbl_BalAmount.Text = ""
                lblBalAmountWithTolerenace.Text = ""
                lbl_capexcode.Text = ""

            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try

    End Sub


End Class
