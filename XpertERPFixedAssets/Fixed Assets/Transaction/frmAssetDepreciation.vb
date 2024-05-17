Imports common
Imports System.Data.SqlClient
Imports Telerik.WinControls.UI
Imports XpertERPEngine
Imports Telerik.WinControls

Public Class FrmAssetDepreciation
    Inherits FrmMainTranScreen

#Region "Variables"
    Const colManualDep As String = "colManualDep"
    Const colSelect As String = "colSelect"
    Const colDate As String = "colDate"
    Const colAssetCode As String = "colAssetCode"
    Const colAssetName As String = "colAssetName"
    Const colDepMethodCode As String = "colDepMethodCode"
    Const colDepMethod As String = "colDepMethod"
    Const colPeriodCode As String = "colPeriodCode"
    Const colPeriod As String = "colPeriod"
    Const colDepFormula As String = "colDepFormula"
    Const colAmtBeforeDep As String = "colAmtBeforeDep"
    Const colDepRate As String = "colDepRate"
    Const colRoundOffRate As String = "colRoundOffRate"
    Const colDepAmount As String = "colDepAmount"
    Const colPrevDepAmount As String = "colPrevDepAmount"
    Const colTotDepAmount As String = "colTotDepAmount"
    Const colAssetValue As String = "colAssetValue"
    Const colSourceOrgValue As String = "colSourceOrgValue"
    Const colEstimatedLife As String = "colEstimatedLife"
    Const colSalvageValue As String = "colSalvageValue"
    Const colAmtBeforeDepTax As String = "colAmtBeforeDepTax"
    Const colDepFormulaTax As String = "colDepFormulaTax"
    Const colDepRateTax As String = "colDepRateTax"
    Const colPrevDepAmountTax As String = "colPrevDepAmountTax"
    Const colTotDepAmountTax As String = "colTotDepAmountTax"
    Const colDepAmountTax As String = "colDepAmountTax"
    Const colAssetValueTax As String = "colAssetValueTax"
    Const colDepMethodTaxCode As String = "colDepMethodTaxCode"
    Const colDepMethodTax As String = "colDepMethodTax"
    Const colIsPermDep As String = "colIsPermDep"

    '' new columns added
    Const colOpening_Value As String = "colOpening_Value"
    Const colOpening_Value_Tax As String = "colOpening_Value_Tax"
    Const colWork_Expense As String = "colWork_Expense"
    Const colAssemble_Cost As String = "colAssemble_Cost"
    Const colDisposable_Value As String = "colDisposable_Value"
    Const colAccumulated_Dep As String = "colAccumulated_Dep"
    Const colLocation_Code As String = "colLocation_Code"
    Const colLocation_Desc As String = "colLocation_Desc"
    Const colAsset_Group_Code As String = "colAsset_Group_Code"
    Const colAsset_Group_Desc As String = "colAsset_Group_Desc"
    Const colAsset_Category_Code As String = "colAsset_Category_Code"
    Const colAsset_Category_Desc As String = "colAsset_Category_Desc"

    Const colIs_Reverse_Dep As String = "colIs_Reverse_Dep"
    Const colReverseDate As String = "colReverseDate"
    Const colTax_Recoverable As String = "colTax_Recoverable"
    Const colTax_Non_Recoverable As String = "colTax_Non_Recoverable"
    '==============Added by preeti gupta[20/02/2017]=============
    Dim AllowRoundInFixedAsset As String = Nothing
    Dim AllowDecimalInFixedAsset As Integer = 0
    Dim ReadOnlyTemplateFieldsOnAcqusition As Boolean = False
    Dim ERPStartDate As String

    Dim ITPartialFADays As Integer
    Dim ITRateMulFA As Decimal
    Dim depreciationCalculation As String



#End Region
    Private Sub FrmAssetDepreciation_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            ERPStartDate = clsCommon.myCDate(objCommonVar.ERPStartDate)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, "Invalid ERP Start Date", Me.Text)
            Me.Close()
        End Try
        '============Added by preeti gupta=============================
        '' if below setting is on then 
        '' 1. Rename FA Template Master to Asset Category
        '' 2. Rename Asset Category Master to Asset Group
        '' 3. Rename Asset Group Master to Sub Group Master
        ReadOnlyTemplateFieldsOnAcqusition = If(clsFixedParameter.GetData(clsFixedParameterCode.ReadOnlyTemplateFieldsOnAcqusition, clsFixedParameterType.ReadOnlyTemplateFieldsOnAcqusition, Nothing) = 1, True, False)
        AllowRoundInFixedAsset = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowRoundInFixedAsset, clsFixedParameterCode.AllowRoundInFixedAsset, Nothing))
        AllowDecimalInFixedAsset = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowDecimalInFixedAsset, clsFixedParameterCode.AllowDecimalInFixedAsset, Nothing))

        '==============================================================
        ITPartialFADays = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.PartialFADepDays, clsFixedParameterCode.PartialFADepDays, Nothing))
        ITRateMulFA = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.RateMultPartialFADepDays, clsFixedParameterCode.RateMultPartialFADepDays, Nothing))
        depreciationCalculation = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.DepreciationCalculationMethod, clsFixedParameterCode.DepreciationCalculationMethod, Nothing))


        SetUserMgmtNew()
        txtTransactionDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = txtTransactionDate.Value
        txtToDate.Value = txtTransactionDate.Value
        RadPageView1.SelectedPage = RadPageViewPage1
        chkReverseTempDep.Checked = False
        chkReverseTempDep.Visible = False
        If clsCommon.myLen(Me.Tag) > 0 Then
            'LoadDataNew()
        End If
    End Sub

    Sub LoadBlankGrid()

        gv1.Rows.Clear()
        gv1.Columns.Clear()

        Dim repoSelect As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoSelect.HeaderText = "Manual Depriciation"
        repoSelect.Name = colManualDep
        repoSelect.ReadOnly = True
        repoSelect.IsVisible = False
        repoSelect.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoSelect)

        repoSelect = New GridViewCheckBoxColumn()
        repoSelect.HeaderText = " "
        repoSelect.Name = colSelect
        repoSelect.ReadOnly = True
        repoSelect.IsVisible = False
        repoSelect.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoSelect)

        Dim repoDate As GridViewDateTimeColumn = New GridViewDateTimeColumn()
        repoDate.Format = DateTimePickerFormat.Custom
        repoDate.CustomFormat = "dd-MM-yyyy"
        repoDate.HeaderText = "Date"
        repoDate.FormatString = "{0:d}"
        repoDate.Name = colDate
        repoDate.WrapText = True
        repoDate.ReadOnly = True
        repoDate.Width = 80
        gv1.MasterTemplate.Columns.Add(repoDate)

        '' grid columns 
        Dim RepoCol As GridViewTextBoxColumn

        RepoCol = New GridViewTextBoxColumn()
        RepoCol.FormatString = ""
        RepoCol.HeaderText = "Asset Code"
        RepoCol.Name = colAssetCode
        RepoCol.Width = 100
        RepoCol.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(RepoCol)

        'Dim repoAsset As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        RepoCol = New GridViewTextBoxColumn()
        RepoCol.FormatString = ""
        RepoCol.HeaderText = "Asset Desc"
        RepoCol.Name = colAssetName
        RepoCol.Width = 100
        RepoCol.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(RepoCol)

        '' Location 
        'Dim repoAssetLocation As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        RepoCol = New GridViewTextBoxColumn()
        RepoCol.FormatString = ""
        RepoCol.HeaderText = "Location Code"
        RepoCol.Name = colLocation_Code
        RepoCol.Width = 100
        RepoCol.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(RepoCol)

        'Dim repoAssetLocationDesc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        RepoCol = New GridViewTextBoxColumn()
        RepoCol.FormatString = ""
        RepoCol.HeaderText = "Location Desc"
        RepoCol.Name = colLocation_Desc
        RepoCol.Width = 100
        RepoCol.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(RepoCol)

        'Dim repoAssetGroup As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        RepoCol = New GridViewTextBoxColumn()
        RepoCol.FormatString = ""
        RepoCol.HeaderText = If(ReadOnlyTemplateFieldsOnAcqusition = True, "Sub Group Code", "Group Code")
        RepoCol.Name = colAsset_Group_Code
        RepoCol.Width = 100
        RepoCol.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(RepoCol)

        'Dim repoAssetGroupDesc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        RepoCol = New GridViewTextBoxColumn()
        RepoCol.FormatString = ""
        RepoCol.HeaderText = If(ReadOnlyTemplateFieldsOnAcqusition = True, "Sub Group Name", "Group Desc")
        RepoCol.Name = colAsset_Group_Desc
        RepoCol.Width = 100
        RepoCol.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(RepoCol)

        '' category
        'Dim repoAssetGroup As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        RepoCol = New GridViewTextBoxColumn()
        RepoCol.FormatString = ""
        RepoCol.HeaderText = If(ReadOnlyTemplateFieldsOnAcqusition = True, "Asset Group Code", "Category Code")
        RepoCol.Name = colAsset_Category_Code
        RepoCol.Width = 100
        RepoCol.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(RepoCol)

        'Dim repoAssetGroupDesc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        RepoCol = New GridViewTextBoxColumn()
        RepoCol.FormatString = ""
        RepoCol.HeaderText = If(ReadOnlyTemplateFieldsOnAcqusition = True, "Asset Group Name", "Category Desc")
        RepoCol.Name = colAsset_Category_Desc
        RepoCol.Width = 100
        RepoCol.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(RepoCol)


        'Dim repoDepMethodCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        RepoCol = New GridViewTextBoxColumn()
        RepoCol.FormatString = ""
        RepoCol.HeaderText = "Method Code"
        RepoCol.Name = colDepMethodCode
        RepoCol.Width = 100
        RepoCol.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(RepoCol)

        'Dim repoDepMethod As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        RepoCol = New GridViewTextBoxColumn()
        RepoCol.FormatString = ""
        RepoCol.HeaderText = "Method"
        RepoCol.Name = colDepMethod
        RepoCol.Width = 150
        RepoCol.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(RepoCol)


        'Dim repoPeriodCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        RepoCol = New GridViewTextBoxColumn()
        RepoCol.FormatString = ""
        RepoCol.HeaderText = "Period Code"
        RepoCol.Name = colPeriodCode
        RepoCol.Width = 100
        RepoCol.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(RepoCol)

        'Dim repoPeriod As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        RepoCol = New GridViewTextBoxColumn()
        RepoCol.FormatString = ""
        RepoCol.HeaderText = "Period"
        RepoCol.Name = colPeriod
        RepoCol.Width = 150
        RepoCol.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(RepoCol)


        'Dim repoFormula As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        RepoCol = New GridViewTextBoxColumn()
        RepoCol.FormatString = ""
        RepoCol.HeaderText = "Formula"
        RepoCol.Name = colDepFormula
        RepoCol.Width = 150
        RepoCol.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(RepoCol)

        Dim repoColDec As GridViewDecimalColumn

        'Dim repoEstLife As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoColDec = New GridViewDecimalColumn()
        repoColDec.FormatString = ""
        repoColDec.HeaderText = "Estimated life"
        repoColDec.Name = colEstimatedLife
        repoColDec.Width = 100
        repoColDec.ReadOnly = True
        repoColDec.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoColDec)

        'Dim repoSalvageValue As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoColDec = New GridViewDecimalColumn()
        repoColDec.FormatString = ""
        repoColDec.HeaderText = "Book Salvage Value"
        repoColDec.Name = colSalvageValue
        repoColDec.Width = 100
        repoColDec.ReadOnly = True
        repoColDec.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoColDec)

        repoColDec = New GridViewDecimalColumn()
        repoColDec.FormatString = ""
        repoColDec.HeaderText = "Original Value"
        repoColDec.Name = colSourceOrgValue
        repoColDec.Width = 100
        repoColDec.ReadOnly = True
        repoColDec.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoColDec)

        '' Opening Value
        repoColDec = New GridViewDecimalColumn()
        repoColDec.FormatString = ""
        repoColDec.HeaderText = "Opening Value"
        repoColDec.Name = colOpening_Value
        repoColDec.Width = 100
        repoColDec.ReadOnly = True
        repoColDec.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoColDec)

        '' Work Expense
        repoColDec = New GridViewDecimalColumn()
        repoColDec.FormatString = ""
        repoColDec.HeaderText = "Work Expense"
        repoColDec.Name = colWork_Expense
        repoColDec.Width = 100
        repoColDec.ReadOnly = True
        repoColDec.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoColDec)

        '' Work Expense
        repoColDec = New GridViewDecimalColumn()
        repoColDec.FormatString = ""
        repoColDec.HeaderText = "Assemble Cost"
        repoColDec.Name = colAssemble_Cost
        repoColDec.Width = 100
        repoColDec.ReadOnly = True
        repoColDec.IsVisible = False
        repoColDec.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoColDec)

        '' Disposable Value
        repoColDec = New GridViewDecimalColumn()
        repoColDec.FormatString = ""
        repoColDec.HeaderText = "Sale/Disposable Value"
        repoColDec.Name = colDisposable_Value
        repoColDec.Width = 100
        repoColDec.ReadOnly = True
        repoColDec.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoColDec)

        'Dim repoBaseAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoColDec = New GridViewDecimalColumn()
        repoColDec.FormatString = ""
        repoColDec.HeaderText = "Base Amount"
        repoColDec.Name = colAmtBeforeDep
        repoColDec.Width = 100
        repoColDec.ReadOnly = True
        repoColDec.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoColDec)

        'Dim repoRate As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoColDec = New GridViewDecimalColumn()
        repoColDec.FormatString = ""
        repoColDec.HeaderText = "Depreciation Rate"
        repoColDec.Name = colDepRate
        repoColDec.Width = 100
        repoColDec.ReadOnly = True
        repoColDec.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoColDec)

        repoColDec = New GridViewDecimalColumn()
        repoColDec.FormatString = ""
        repoColDec.HeaderText = "roundOffRate"
        repoColDec.Name = colRoundOffRate
        repoColDec.Width = 100
        repoColDec.ReadOnly = True
        repoColDec.IsVisible = False
        repoColDec.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoColDec)
        'Dim repoDepAmt As GridViewDecimalColumn = New GridViewDecimalColumn()

        repoColDec = New GridViewDecimalColumn()
        repoColDec.FormatString = ""
        repoColDec.HeaderText = "Prev. Temp Depreciation"
        repoColDec.Name = colPrevDepAmount
        repoColDec.Width = 100
        repoColDec.ReadOnly = True
        repoColDec.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoColDec.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoColDec)

        repoColDec = New GridViewDecimalColumn()
        repoColDec.FormatString = ""
        repoColDec.HeaderText = "Depreciation Amount"
        repoColDec.Name = colDepAmount
        repoColDec.Width = 100
        repoColDec.ReadOnly = False
        repoColDec.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoColDec)

        repoColDec = New GridViewDecimalColumn()
        repoColDec.FormatString = ""
        repoColDec.HeaderText = "Asset Value"
        repoColDec.Name = colAssetValue
        repoColDec.Width = 100
        repoColDec.ReadOnly = True
        repoColDec.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoColDec)


        '//Tax
        '' Opening Value
        repoColDec = New GridViewDecimalColumn()
        repoColDec.FormatString = ""
        repoColDec.HeaderText = "Opening Value Tax"
        repoColDec.Name = colOpening_Value_Tax
        repoColDec.Width = 100
        repoColDec.ReadOnly = True
        repoColDec.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoColDec)

        'Dim repoBaseAmtTax As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoColDec = New GridViewDecimalColumn()
        repoColDec.FormatString = ""
        repoColDec.HeaderText = "Base Amount Tax"
        repoColDec.Name = colAmtBeforeDepTax
        repoColDec.Width = 100
        repoColDec.ReadOnly = True
        repoColDec.IsVisible = False
        repoColDec.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoColDec)

        'Dim repoRateTax As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoColDec = New GridViewDecimalColumn()
        repoColDec.FormatString = ""
        repoColDec.HeaderText = "Depreciation Rate Tax"
        repoColDec.Name = colDepRateTax
        repoColDec.Width = 100
        repoColDec.ReadOnly = True
        repoColDec.IsVisible = False
        repoColDec.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoColDec)

        'Dim repoDepAmtTax As GridViewDecimalColumn = New GridViewDecimalColumn()

        repoColDec = New GridViewDecimalColumn()
        repoColDec.FormatString = ""
        repoColDec.HeaderText = "Prev. Depreciation Tax"
        repoColDec.Name = colPrevDepAmountTax
        repoColDec.Width = 100
        repoColDec.ReadOnly = True
        repoColDec.IsVisible = False
        repoColDec.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoColDec)

        repoColDec = New GridViewDecimalColumn()
        repoColDec.FormatString = ""
        repoColDec.HeaderText = "Depreciation Amount Tax"
        repoColDec.Name = colDepAmountTax
        repoColDec.Width = 100
        repoColDec.ReadOnly = True
        repoColDec.IsVisible = False
        repoColDec.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoColDec)


        repoColDec = New GridViewDecimalColumn()
        repoColDec.FormatString = ""
        repoColDec.HeaderText = "Asset Value Tax"
        repoColDec.Name = colAssetValueTax
        repoColDec.Width = 100
        repoColDec.ReadOnly = True
        repoColDec.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoColDec)

        'Dim repoFormulaTax As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        RepoCol = New GridViewTextBoxColumn
        RepoCol.FormatString = ""
        RepoCol.HeaderText = "Formula Tax"
        RepoCol.Name = colDepFormulaTax
        RepoCol.Width = 150
        RepoCol.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(RepoCol)

        'Dim repoDepMethodTaxCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        RepoCol = New GridViewTextBoxColumn
        RepoCol.FormatString = ""
        RepoCol.HeaderText = "Method Tax Code"
        RepoCol.Name = colDepMethodTaxCode
        RepoCol.Width = 100
        RepoCol.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(RepoCol)

        'Dim repoDepMethodTax As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        RepoCol = New GridViewTextBoxColumn
        RepoCol.FormatString = ""
        RepoCol.HeaderText = "Method Tax"
        RepoCol.Name = colDepMethodTax
        RepoCol.Width = 150
        RepoCol.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(RepoCol)
        '//End TAx

        'Dim repoIsPermDep As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        RepoCol = New GridViewTextBoxColumn
        RepoCol.FormatString = ""
        RepoCol.HeaderText = "Is Permanent"
        RepoCol.Name = colIsPermDep
        RepoCol.Width = 100
        RepoCol.ReadOnly = True
        RepoCol.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(RepoCol)

        RepoCol = New GridViewTextBoxColumn
        RepoCol.FormatString = ""
        RepoCol.HeaderText = "Is Reverse"
        RepoCol.Name = colIs_Reverse_Dep
        RepoCol.Width = 100
        RepoCol.ReadOnly = True
        RepoCol.IsVisible = False
        RepoCol.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(RepoCol)

        repoDate = New GridViewDateTimeColumn
        repoDate.Format = DateTimePickerFormat.Custom
        repoDate.CustomFormat = "dd-MM-yyyy"
        repoDate.HeaderText = "Reverse Date"
        repoDate.FormatString = "{0:d}"
        repoDate.Name = colReverseDate
        repoDate.WrapText = True
        repoDate.ReadOnly = True
        repoDate.IsVisible = False
        repoDate.Width = 80
        gv1.MasterTemplate.Columns.Add(repoDate)

        repoColDec = New GridViewDecimalColumn()
        repoColDec.FormatString = ""
        repoColDec.HeaderText = "Tax Recoverable"
        repoColDec.Name = colTax_Recoverable
        repoColDec.Width = 100
        repoColDec.ReadOnly = True
        repoColDec.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoColDec)

        '' Opening Value
        repoColDec = New GridViewDecimalColumn()
        repoColDec.FormatString = ""
        repoColDec.HeaderText = "Tax Non Recoverable"
        repoColDec.Name = colTax_Non_Recoverable
        repoColDec.Width = 100
        repoColDec.ReadOnly = True
        repoColDec.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoColDec)

        gv1.AllowDeleteRow = True
        gv1.AllowAddNewRow = False
        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = False
        gv1.AllowRowReorder = False
        gv1.EnableSorting = False
        gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        gv1.TableElement.TableHeaderHeight = 40
    End Sub

    Private Sub FrmAssetDepreciation_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F12 Then
            Dim frm As New FrmPWD(Nothing)
            frm.strType = "SIRC"
            frm.strCode = "SIReversAndCreate"
            frm.ShowDialog()
            If frm.isPasswordCorrect Then
                btnReverse.Visible = True
                'CheckBox1.Visible = True
            End If
        End If
    End Sub

    Private Sub RadButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadButton2.Click
        Me.Close()
    End Sub

    Public Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Try
            Dim Msg As String = "Load Depreciation Data"
            If chkAutoProcess.Checked Then
                Msg = "Processed Depreciation Data"
            End If
            If clsCommon.MyMessageBoxShow(Me, Msg + Environment.NewLine + "Are You sure", Me.Text, MessageBoxButtons.YesNo) = DialogResult.Yes Then
                LoadDataNew(chkAutoProcess.Checked)
                clsCommon.MyMessageBoxShow(Me, "Successfully " + Msg, Me.Text)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnProcess_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProcess.Click
        Try
            If clsCommon.MyMessageBoxShow(Me, "Processed Depreciation Data" + Environment.NewLine + "Are You sure", Me.Text, MessageBoxButtons.YesNo) = DialogResult.Yes Then
                LoadDataNew(True)
                clsCommon.MyMessageBoxShow(Me, "Depreciation Processed Successfully", Me.Text)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub LoadDataNew(ByVal isAutoProcess As Boolean)
        Dim Arr As List(Of clsDepreciationCalculation) = Nothing
        If clsAssetDepreciation.GetDepreciationCal(Arr, isAutoProcess, txtTransactionDate.Value, chkReverseTempDep.Checked, txtLocation.arrValueMember, txtAsset.arrValueMember, txtGroup.arrValueMember, txtCostCenter.arrValueMember, txtCategory.arrValueMember) Then
            If Not isAutoProcess Then
                LoadBlankGrid()
                For Each obj As clsDepreciationCalculation In Arr
                    gv1.Rows.AddNew()
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colSelect).Value = True
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDate).Value = obj.colDate
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colAssetCode).Value = obj.colAssetCode
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colAssetName).Value = obj.colAssetName
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colAssetName).Tag = obj.ColAsset_Disposal_Code
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colLocation_Code).Value = obj.colLocation_Code
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colLocation_Desc).Value = obj.colLocation_Desc
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colAsset_Group_Code).Value = obj.colAsset_Group_Code
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colAsset_Group_Desc).Value = obj.colAsset_Group_Desc
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colAsset_Category_Code).Value = obj.colAsset_Category_Code
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colAsset_Category_Desc).Value = obj.colAsset_Category_Desc
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDepMethodCode).Value = obj.colDepMethodCode
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDepMethod).Value = obj.colDepMethod
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colPeriodCode).Value = obj.colPeriodCode
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colPeriod).Value = obj.colPeriod
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colSalvageValue).Value = obj.colSalvageValue
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colSourceOrgValue).Value = obj.colSourceOrgValue
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colEstimatedLife).Value = obj.colEstimatedLife
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colOpening_Value).Value = obj.colOpening_Value
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colWork_Expense).Value = obj.colWork_Expense
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colAssemble_Cost).Value = obj.colAssemble_Cost
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colOpening_Value_Tax).Value = obj.colOpening_Value_Tax
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTax_Non_Recoverable).Value = obj.colTax_Non_Recoverable
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTax_Recoverable).Value = obj.colTax_Recoverable
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDepFormula).Value = obj.colDepFormula
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colAmtBeforeDep).Value = obj.colAmtBeforeDep
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDepRate).Value = obj.colDepRate
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colRoundOffRate).Value = obj.colRoundOffRate
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDepAmount).Value = obj.colDepAmount
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colPrevDepAmount).Value = obj.colPrevDepAmount
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colAssetValue).Value = obj.colAssetValue
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colIsPermDep).Value = obj.colIsPermDep
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colManualDep).Value = obj.colManualDep
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colIs_Reverse_Dep).Value = obj.colIs_Reverse_Dep
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colReverseDate).Value = obj.colReverse_Date
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colIs_Reverse_Dep).Value = obj.colIs_Reverse_Dep
                Next
                RadPageView1.SelectedPage = RadPageViewPage4
            End If
        End If
    End Sub
    'Sub LoadData()
    '    Try
    '        Dim adjustment As Double
    '        txtTransactionDate.Value = clsCommon.GetDateWithEndTime(txtTransactionDate.Value)
    '        LoadBlankGrid()
    '        Dim DepQry As String = clsAssetDepreciation.GetAssetDepQuery()
    '        Dim qry As String = "select xxx.*,Dep.Temp_Dep_Amount,Dep.Temp_Dep_Amount_Tax,xxxTemp.Temp_Doc_Date,FiscalPeriodRun1,FiscalPeriodRun2,FiscalPeriodRun3,FiscalPeriodRun4,FiscalPeriodRun5,FiscalPeriodRun6,FiscalPeriodRun7,FiscalPeriodRun8,FiscalPeriodRun9,FiscalPeriodRun10,FiscalPeriodRun11,FiscalPeriodRun12,TSPL_DEPRECIATION_METHOD.Description as MethodName,TSPL_DEPRECIATION_METHOD.Formula,TSPL_DEPRECIATION_PERIODS.period_Desc,DepMethodTax.Formula as Formula_Tax,DepMethodTax.Description as MethodNameTax,FiscalPeriodRunPerm1,FiscalPeriodRunPerm2,FiscalPeriodRunPerm3,FiscalPeriodRunPerm4,FiscalPeriodRunPerm5,FiscalPeriodRunPerm6,FiscalPeriodRunPerm7,FiscalPeriodRunPerm8,FiscalPeriodRunPerm9,FiscalPeriodRunPerm10,FiscalPeriodRunPerm11,FiscalPeriodRunPerm12,loc.Location_Desc,AG.Description as Group_Desc,AC.Description as Cat_Desc from (" + Environment.NewLine
    '        qry += " select Asset_Code,Asset_Name,Dep_Method_Code,Dep_Period_Code,0 as RI,Start_Date as Document_Date, isnull(TSPL_ACQUISITION_DETAIL.Book_Source_value,0)+isnull(TSPL_ACQUISITION_DETAIL.Tax_Non_Recoverable,0)-isnull(TSPL_ACQUISITION_DETAIL.Depreciated_Value,0) as Asset_Value,TSPL_ACQUISITION_DETAIL.Dep_Rate,TSPL_ACQUISITION_HEAD.Loc_Code,TSPL_ACQUISITION_DETAIL.Group_Code,TSPL_ACQUISITION_DETAIL.Category_code,TSPL_ACQUISITION_DETAIL.CostCenter_Code,isnull(TSPL_ACQUISITION_DETAIL.Book_Source_Original_value,0)+isnull(TSPL_ACQUISITION_DETAIL.Tax_Non_Recoverable,0)-isnull(TSPL_ACQUISITION_DETAIL.Depreciated_Value,0) as Book_Source_Original_value ,TSPL_ACQUISITION_DETAIL.Book_Estimated_Life,  isnull(TSPL_ACQUISITION_DETAIL.Book_Source_value,0)+isnull(TSPL_ACQUISITION_DETAIL.Tax_Non_Recoverable,0) as Asset_value_Tax, TSPL_ACQUISITION_DETAIL.Dep_Tax_Rate,TSPL_ACQUISITION_DETAIL.Dep_Method_Tax_Code,TSPL_ACQUISITION_DETAIL.Book_Salvage_Value,TSPL_ACQUISITION_DETAIL.Start_Date,TSPL_ACQUISITION_DETAIL.Book_Salvage_Rate ,TSPL_ACQUISITION_DETAIL.Tax_Non_Recoverable ,TSPL_ACQUISITION_DETAIL.Tax_Recoverable  from TSPL_ACQUISITION_DETAIL" + Environment.NewLine
    '        qry += " left outer join TSPL_ACQUISITION_HEAD on TSPL_ACQUISITION_HEAD.Acquisition_Code=TSPL_ACQUISITION_DETAIL.Acquisition_Code" + Environment.NewLine
    '        qry += " where TSPL_ACQUISITION_HEAD.Status=1 and isnull(TSPL_ACQUISITION_DETAIL.asset_merged,0)<>1 and not exists (select 1 from TSPL_ASSET_DEPRECIATION where TSPL_ASSET_DEPRECIATION.Asset_Code=TSPL_ACQUISITION_DETAIL.Asset_Code )   " + Environment.NewLine
    '        qry += " union all " + Environment.NewLine
    '        qry += " select TSPL_ASSET_DEPRECIATION.Asset_Code,TSPL_ACQUISITION_DETAIL.Asset_Name,TSPL_ASSET_DEPRECIATION.Dep_Method_Code,TSPL_ASSET_DEPRECIATION.Dep_Period_Code,1 as RI,TSPL_ASSET_DEPRECIATION.Document_Date,TSPL_ASSET_DEPRECIATION.Asset_value,TSPL_ACQUISITION_DETAIL.Dep_Rate,TSPL_ACQUISITION_HEAD.Loc_Code,TSPL_ACQUISITION_DETAIL.Group_Code,TSPL_ACQUISITION_DETAIL.Category_code,TSPL_ACQUISITION_DETAIL.CostCenter_Code,isnull(TSPL_ACQUISITION_DETAIL.Book_Source_Original_value,0)+isnull(TSPL_ACQUISITION_DETAIL.Tax_Non_Recoverable,0)-isnull(TSPL_ACQUISITION_DETAIL.Depreciated_Value,0) as Book_Source_Original_value,TSPL_ACQUISITION_DETAIL.Book_Estimated_Life, TSPL_ASSET_DEPRECIATION.Asset_value_Tax, TSPL_ACQUISITION_DETAIL.Dep_Tax_Rate,TSPL_ACQUISITION_DETAIL.Dep_Method_Tax_Code,TSPL_ACQUISITION_DETAIL.Book_Salvage_Value,TSPL_ACQUISITION_DETAIL.Start_Date,TSPL_ACQUISITION_DETAIL.Book_Salvage_Rate ,TSPL_ACQUISITION_DETAIL.Tax_Non_Recoverable ,TSPL_ACQUISITION_DETAIL.Tax_Recoverable  from TSPL_ASSET_DEPRECIATION" + Environment.NewLine
    '        qry += " left outer join TSPL_ACQUISITION_DETAIL on TSPL_ACQUISITION_DETAIL.Asset_Code=TSPL_ASSET_DEPRECIATION.Asset_Code " + Environment.NewLine
    '        qry += " left outer join TSPL_ACQUISITION_HEAD on TSPL_ACQUISITION_HEAD.Acquisition_Code=TSPL_ACQUISITION_DETAIL.Acquisition_Code" + Environment.NewLine
    '        qry += " Where Document_Code =(select top 1 innTable.Document_Code from TSPL_ASSET_DEPRECIATION as innTable where innTable.Asset_Code=TSPL_ASSET_DEPRECIATION.Asset_Code  order by innTable.Document_Date desc) and isnull(TSPL_ACQUISITION_DETAIL.asset_merged,0)<>1 " + Environment.NewLine
    '        qry += " )xxx" + Environment.NewLine
    '        qry += " left join (" &
    '               " select Acquisition_Code,Asset_Code,Start_Date as Temp_Doc_Date from TSPL_ACQUISITION_DETAIL where  not exists (select 1 from TSPL_ASSET_DEPRECIATION " &
    '               " where TSPL_ASSET_DEPRECIATION.Asset_Code=TSPL_ACQUISITION_DETAIL.Asset_Code ) and isnull(TSPL_ACQUISITION_DETAIL.asset_merged,0)<>1 " &
    '               " union all " &
    '               " select Document_Code,Asset_Code,Document_Date as Temp_Doc_Date from TSPL_ASSET_DEPRECIATION Where Document_Code =(select top 1 innTable.Document_Code " &
    '               " from TSPL_ASSET_DEPRECIATION as innTable where innTable.Asset_Code=TSPL_ASSET_DEPRECIATION.Asset_Code order by innTable.Document_Date desc)) " &
    '               " xxxTemp on xxx.Asset_Code=xxxTemp.Asset_Code "
    '        qry += " left outer join TSPL_DEPRECIATION_PERIODS on TSPL_DEPRECIATION_PERIODS.period_Code=xxx.Dep_Period_Code" + Environment.NewLine
    '        qry += " left outer join TSPL_DEPRECIATION_METHOD on TSPL_DEPRECIATION_METHOD.Code=xxx.Dep_Method_Code" + Environment.NewLine
    '        qry += " left outer join TSPL_DEPRECIATION_METHOD as DepMethodTax on DepMethodTax.Code=Dep_Method_Tax_Code" &
    '               " left join TSPL_LOCATION_MASTER Loc on xxx.Loc_Code=Loc.Location_Code" &
    '               " left join TSPL_ASSET_GROUP AG on xxx.Group_Code=AG.Group_Code " &
    '               " left join TSPL_ASSET_CATEGORY AC on xxx.Category_code=ac.Category_Code " &
    '               " left join (" & DepQry & ") as Dep on xxx.Asset_Code=Dep.Asset_Code " &
    '               " where 2=2 "
    '        If txtCategory.arrValueMember IsNot Nothing AndAlso txtCategory.arrValueMember.Count > 0 Then
    '            qry += " and xxx.Category_code in (" + clsCommon.GetMulcallString(txtCategory.arrValueMember) + ")"
    '        End If
    '        If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
    '            qry += " and xxx.Loc_Code in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ")"
    '        End If
    '        If txtCostCenter.arrValueMember IsNot Nothing AndAlso txtCostCenter.arrValueMember.Count > 0 Then
    '            qry += " and xxx.CostCenter_Code in (" + clsCommon.GetMulcallString(txtCostCenter.arrValueMember) + ")"
    '        End If
    '        If txtGroup.arrValueMember IsNot Nothing AndAlso txtGroup.arrValueMember.Count > 0 Then
    '            qry += " and xxx.Group_Code in (" + clsCommon.GetMulcallString(txtGroup.arrValueMember) + ")"
    '        End If
    '        If txtAsset.arrValueMember IsNot Nothing AndAlso txtAsset.arrValueMember.Count > 0 Then
    '            qry += " and xxx.Asset_Code in (" + clsCommon.GetMulcallString(txtAsset.arrValueMember) + ")"
    '        End If
    '        Dim PermDepFlag As Boolean = False
    '        Dim Asset_Code As String = ""
    '        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
    '        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
    '            RadPageView1.SelectedPage = RadPageViewPage4
    '            EnableDisableCtrl(False)
    '            clsCommon.ProgressBarPercentShow()
    '            Dim CurrIndx As Integer = 0
    '            Dim Total As Integer = dt.Rows.Count
    '            For Each dr As DataRow In dt.Rows
    '                Try
    '                    If chkAutoProcess.Checked AndAlso gv1.Rows.Count > 0 Then
    '                        If clsCommon.myLen(Asset_Code) > 0 Then
    '                            If Not clsCommon.CompairString(Asset_Code, clsCommon.myCstr(dr("Asset_Code"))) = CompairStringResult.Equal Then
    '                                ProcessData()
    '                                LoadBlankGrid()
    '                            End If
    '                        End If
    '                    End If

    '                    If clsCommon.myLen(Asset_Code) <= 0 Then
    '                        PermDepFlag = False
    '                        Asset_Code = clsCommon.myCstr(dr("Asset_Code"))
    '                    Else
    '                        If clsCommon.CompairString(clsCommon.myCstr(dr("Asset_Code")), Asset_Code) = CompairStringResult.Equal Then
    '                            If PermDepFlag Then
    '                                Continue For
    '                            End If
    '                        Else
    '                            PermDepFlag = False
    '                            Asset_Code = clsCommon.myCstr(dr("Asset_Code"))
    '                        End If
    '                    End If
    '                    CurrIndx += 1
    '                    clsCommon.ProgressBarPercentUpdate(((CurrIndx) * 100 / (Total)), "Asset [" + Asset_Code + "]" & clsCommon.myCstr(CurrIndx) & "/" & clsCommon.myCstr(Total) & "")

    '                    Dim dblTaxRecoverable As Double = clsCommon.myCdbl(dr("Tax_Recoverable"))
    '                    Dim dblTaxNonRecoverable As Double = clsCommon.myCdbl(dr("Tax_Non_Recoverable"))

    '                    Dim dblPrevDep As Double = clsCommon.myCdbl(dr("Temp_Dep_Amount"))
    '                    Dim dblBaseAmt As Double = clsCommon.myCdbl(dr("Asset_Value"))

    '                    Dim dblDepRate As Double = clsCommon.myCdbl(dr("Dep_Rate"))
    '                    Dim dblPrevDepTax As Double = clsCommon.myCdbl(dr("Temp_Dep_Amount_Tax"))
    '                    Dim dblBaseAmtTax As Double = clsCommon.myCdbl(dr("Asset_value_Tax"))
    '                    Dim dblDepRateTax As Double = clsCommon.myCdbl(dr("Dep_Tax_Rate"))
    '                    Dim depStartDate As Date = clsCommon.GetDateWithEndTime(dr.Item("Start_Date"))
    '                    Dim Book_Salvage_Value As Double = clsCommon.myCdbl(dr("Book_Salvage_Value"))

    '                    If dblBaseAmt <= Book_Salvage_Value Then
    '                        Continue For
    '                    End If
    '                    dblDepRate = dblDepRate
    '                    dblDepRateTax = dblDepRateTax

    '                    qry = "  select SUM(1) from TSPL_ASSET_DEPRECIATION where Asset_Code='" + clsCommon.myCstr(dr("Asset_Code")) + "' "
    '                    Dim intDepCount As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
    '                    Dim dtStartDate As Date = clsCommon.GetDateWithEndTime(clsCommon.myCDate(dr("Document_Date")))
    '                    Dim dtStartDate1 As Date = clsCommon.GetDateWithEndTime(clsCommon.myCDate(dr("Document_Date")))
    '                    Dim dtPermDepDate As Date = clsCommon.GetDateWithEndTime(clsCommon.myCDate(dr("Document_Date")))
    '                    Dim LastTempDepDate As Date = clsCommon.GetDateWithEndTime(clsCommon.myCDate(dr("Temp_Doc_Date")))
    '                    Dim dtEndDate As Date = clsCommon.GetDateWithEndTime(depStartDate).AddYears(clsCommon.myCdbl(dr("Book_Estimated_Life"))).AddDays(-1)
    '                    Dim dtDepData As DataTable = GetAssetLastDepData(dr.Item("Asset_Code"))
    '                    Dim dtEndMonthDate As Date = clsCommon.GetDateWithEndTime(New Date(dtEndDate.Year, dtEndDate.Month, 1).AddMonths(1).AddDays(-1))
    '                    If intDepCount > 0 Then
    '                        dtPermDepDate = clsCommon.GetDateWithEndTime(dtDepData.Rows(0).Item("Perm_Doc_Date"))
    '                        dtPermDepDate = LastTempDepDate.AddDays(1)

    '                        dtStartDate = dtStartDate.AddDays(1)
    '                        dtStartDate1 = dtStartDate1.AddDays(1)
    '                    Else
    '                        dtStartDate = dtStartDate
    '                        dtStartDate1 = dtStartDate1
    '                    End If
    '                    qry = "select DateDiff(d,start_date,end_date) as dd,Fiscal_Name,Start_Date,End_Date from TSPL_Fiscal_Year_Master where convert(date,'" & dtStartDate & "',103) >= convert(date,start_date,103) and convert(date,'" & dtStartDate & "',103) <= convert(date,end_date,103)"
    '                    Dim dtFisalYEar As DataTable = clsDBFuncationality.GetDataTable(qry)
    '                    If dtFisalYEar Is Nothing OrElse dtFisalYEar.Rows.Count <= 0 Then
    '                        Throw New Exception("Fiscal Year Not exist where Date [" + clsCommon.GetPrintDate(dtStartDate, "dd/MM/yyyy") + "] Exist")
    '                    End If
    '                    clsCommon.ProgressBarPercentUpdate(((CurrIndx) * 100 / (Total)), "Asset [" + Asset_Code + "] [" + clsCommon.myCstr(dtFisalYEar.Rows(0)("Fiscal_Name")) + "]" & clsCommon.myCstr(CurrIndx) & "/" & clsCommon.myCstr(Total) & "")

    '                    qry = " select TSPL_ASSET_SCRAP_HEAD.Document_No,TSPL_ASSET_SCRAP_HEAD.Document_Date,TSPL_ASSET_SCRAP_HEAD.Status,TSPL_ASSET_SCRAP_HEAD.Loc_Code from TSPL_ASSET_SCRAP_HEAD where  TSPL_ASSET_SCRAP_HEAD.Document_No in ( select TSPL_ASSET_SCRAP_DETAIL.Document_No from TSPL_ASSET_SCRAP_DETAIL where TSPL_ASSET_SCRAP_DETAIL.Asset_Code='" + Asset_Code + "')"
    '                    Dim dtDisposalEntry As DataTable = clsDBFuncationality.GetDataTable(qry)
    '                    If dtDisposalEntry IsNot Nothing AndAlso dtDisposalEntry.Rows.Count > 0 Then
    '                        If clsCommon.GetDateWithStartTime(clsCommon.myCDate(dtFisalYEar.Rows(0)("Start_Date"))) <= clsCommon.myCDate(dtDisposalEntry.Rows(0)("Document_Date")) AndAlso
    '                                clsCommon.GetDateWithEndTime(clsCommon.myCDate(dtFisalYEar.Rows(0)("End_Date"))) >= clsCommon.myCDate(dtDisposalEntry.Rows(0)("Document_Date")) Then
    '                            If dtDisposalEntry.Rows(0)("Status") = 1 Then
    '                                ''Now Create Dep Enry of Disposal Entry
    '                                qry = " select 1 from TSPL_ASSET_DEPRECIATION where Asset_Code='" + Asset_Code + "' and len(isnull(Asset_Disposal_Code,''))>0 "
    '                                Dim dtCheck As DataTable = clsDBFuncationality.GetDataTable(qry)
    '                                If dtCheck Is Nothing OrElse dtCheck.Rows.Count <= 0 Then
    '                                    Dim tran As SqlTransaction = clsDBFuncationality.GetTransactin()
    '                                    Try
    '                                        Dim objProcess As New clsDepreciationCalculation()
    '                                        objProcess.GetDepreciationProcess(clsCommon.myCDate(dtDisposalEntry.Rows(0)("Document_Date")), clsCommon.myCstr(dtDisposalEntry.Rows(0)("Document_No")), False, clsCommon.myCstr(dtDisposalEntry.Rows(0)("Loc_Code")), Asset_Code, tran, False)
    '                                        tran.Commit()
    '                                    Catch ex As Exception
    '                                        tran.Rollback()
    '                                        Throw New Exception("Error While Create Dep Entry of Disposal " + Environment.NewLine + ex.Message)
    '                                    End Try
    '                                End If
    '                            End If
    '                            Continue For
    '                        End If
    '                        If clsCommon.GetDateWithEndTime(clsCommon.myCDate(dtFisalYEar.Rows(0)("End_Date"))) >= clsCommon.myCDate(dtDisposalEntry.Rows(0)("Document_Date")) Then
    '                            Continue For ''Do Not calcualte Dep of Disposal Entry
    '                        End If
    '                    End If

    '                    If Book_Salvage_Value <= 0 Then
    '                        Continue For ''Do Not calcualte Dep if salves Value is Zero
    '                    End If

    '                    Dim DaysInYear As Integer = clsCommon.myCDecimal(dtFisalYEar.Rows(0)("dd")) + 1
    '                    Dim dclTotalDep As Decimal = GetAssetDepreciationAmt(clsCommon.myCstr(dr("Asset_Code")), dtStartDate)
    '                    While (txtTransactionDate.Value >= dtStartDate AndAlso dblBaseAmt > Book_Salvage_Value AndAlso (dtStartDate.AddMonths(1).AddDays(-1)) <= dtEndMonthDate)
    '                        dblBaseAmt = clsCommon.myCdbl(dr.Item("Book_Source_Original_value"))
    '                        dblBaseAmtTax = clsCommon.myCdbl(dr.Item("Book_Source_Original_value"))
    '                        Dim dclMaxDepAmt As Decimal = (dblBaseAmt - dclTotalDep - Book_Salvage_Value)
    '                        Dim dclMaxDepAmtflag As Boolean = False
    '                        If (dblBaseAmt - dclTotalDep) <= Book_Salvage_Value Then
    '                            Exit While
    '                        End If
    '                        If clsCommon.CompairString(clsCommon.myCstr(dr("FiscalPeriodRunPerm" + clsCommon.myCstr(dtStartDate.Month) + "")), "YES") = CompairStringResult.Equal Then
    '                            PermDepFlag = True
    '                        Else
    '                            If PermDepFlag Then
    '                                Continue For
    '                            End If
    '                        End If
    '                        If clsCommon.CompairString(clsCommon.myCstr(dr("FiscalPeriodRun" + clsCommon.myCstr(dtStartDate.Month) + "")), "YES") = CompairStringResult.Equal Or clsCommon.CompairString(clsCommon.myCstr(dr("FiscalPeriodRunPerm" + clsCommon.myCstr(dtStartDate.Month) + "")), "YES") = CompairStringResult.Equal Then
    '                            Dim dtForChecFrom As Date = New Date(dtStartDate.Year, dtStartDate.Month, 1)
    '                            dtForChecFrom = clsCommon.GetDateWithEndTime(dtForChecFrom)
    '                            Dim dtForChecTo As Date = dtForChecFrom.AddMonths(1)
    '                            dtForChecTo = clsCommon.GetDateWithEndTime(dtForChecTo.AddDays(-1))
    '                            Dim DocDate As Date = dtForChecTo
    '                            dtForChecTo = IIf(dtForChecTo < dtEndDate, dtForChecTo, dtEndDate)
    '                            If ITPartialFADays > clsCommon.myFormat(DateDiff(DateInterval.Day, depStartDate, DocDate)) Then
    '                                dblDepRateTax = dblDepRateTax * ITRateMulFA
    '                            End If

    '                            qry = "select 1 from TSPL_ASSET_DEPRECIATION where Asset_Code='" + clsCommon.myCstr(dr("Asset_Code")) + "' and  Document_Date>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(dtForChecFrom), "dd/MMM/yyyy hh:mm tt") + "' and Document_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(dtForChecTo), "dd/MMM/yyyy hh:mm tt") + "' AND coalesce(TSPL_ASSET_DEPRECIATION.is_Permanent,'NO')='YES'"
    '                            Dim isRecordExist As DataTable = clsDBFuncationality.GetDataTable(qry)
    '                            If isRecordExist Is Nothing OrElse isRecordExist.Rows.Count <= 0 Then
    '                                qry = "select sum(Dep_Amount) as Dep_Amount,sum(Dep_Amount_Tax) as Dep_Amount_Tax from (
    '                            select Dep_Amount,Dep_Amount_Tax from TSPL_ASSET_DEPRECIATION where Asset_Code='" + clsCommon.myCstr(dr("Asset_Code")) + "' and Document_Date<(select Start_Date from TSPL_Fiscal_Year_Master where Start_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(dtForChecFrom), "dd/MMM/yyyy hh:mm tt") + "' and End_Date>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(dtForChecFrom), "dd/MMM/yyyy hh:mm tt") + "')
    '                            union all
    '                            select -1*Dep_Amount as Dep_Amount,-1*Dep_Amount_Tax as Dep_Amount_Tax from TSPL_ASSET_DEPRECIATION_ADJUSTMENT where Asset_Code='" + clsCommon.myCstr(dr("Asset_Code")) + "' and Adjustment_Date <= '" + clsCommon.GetPrintDate(dtForChecFrom, "dd/MMM/yyyy") + "'
    '                            )xx"
    '                                Dim dtPreFiscayYearDep As DataTable = clsDBFuncationality.GetDataTable(qry, Nothing)

    '                                qry = "select SUM(Net_Amt) as Expense,sum(case when convert(Date, Document_Date,103)<='" & clsCommon.GetPrintDate(dtForChecTo, "dd/MMM/yyyy") & "' then Net_Amt else 0 end) as Current_Expense from (select Asset_Code,Document_Date,Net_Amt from  TSPL_ASSET_WORK_HEAD where Status=1 and Asset_Code='" + clsCommon.myCstr(dr("Asset_Code")) + "'  and Document_Code not in (select Document_Code from TSPL_ASSET_ASSEMBLE_DETAIL where Distribute='Y' and Asset_Code ='" + clsCommon.myCstr(dr("Asset_Code")) + "')" &
    '                                " ) AS TSPL_ASSET_WORK_HEAD "
    '                                Dim dtExp As DataTable = clsDBFuncationality.GetDataTable(qry, Nothing)
    '                                Dim dblExpence As Double = clsCommon.myCdbl(dtExp.Rows(0).Item("Expense"))
    '                                Dim dblCurrExpence As Double = clsCommon.myCdbl(dtExp.Rows(0).Item("Current_Expense"))
    '                                dtStartDate1 = clsCommon.GetDateWithEndTime(dtForChecTo.AddDays(1))
    '                                dblBaseAmt += dblCurrExpence
    '                                dblBaseAmtTax += dblCurrExpence
    '                                If dtPreFiscayYearDep IsNot Nothing AndAlso dtPreFiscayYearDep.Rows.Count > 0 Then
    '                                    dblBaseAmt -= clsCommon.myCdbl(dtPreFiscayYearDep.Rows(0)("Dep_Amount"))
    '                                    dblBaseAmtTax -= clsCommon.myCdbl(dtPreFiscayYearDep.Rows(0)("Dep_Amount_Tax"))
    '                                End If

    '                                qry = "select Dep_Rate from TSPL_FA_BOOK_MASTER  Where  TSPL_FA_BOOK_MASTER.Book_Code = '" + clsCommon.myCstr(dr("Group_Code")) + "' and start_Date<='" + clsCommon.GetPrintDate(DocDate, "dd/MMM/yyyy") + "'"
    '                                Dim dtRate As DataTable = clsDBFuncationality.GetDataTable(qry)
    '                                If dtRate IsNot Nothing AndAlso dtRate.Rows.Count > 0 Then
    '                                    dblDepRate = clsCommon.myCstr(dtRate.Rows(0)("Dep_Rate"))
    '                                End If
    '                                gv1.Rows.AddNew()
    '                                gv1.Rows(gv1.Rows.Count - 1).Cells(colSelect).Value = True
    '                                gv1.Rows(gv1.Rows.Count - 1).Cells(colDate).Value = DocDate
    '                                gv1.Rows(gv1.Rows.Count - 1).Cells(colAssetCode).Value = clsCommon.myCstr(dr("Asset_Code"))
    '                                gv1.Rows(gv1.Rows.Count - 1).Cells(colAssetName).Value = clsCommon.myCstr(dr("Asset_Name"))
    '                                gv1.Rows(gv1.Rows.Count - 1).Cells(colLocation_Code).Value = clsCommon.myCstr(dr("Loc_Code"))
    '                                gv1.Rows(gv1.Rows.Count - 1).Cells(colLocation_Desc).Value = clsCommon.myCstr(dr("Location_Desc"))
    '                                gv1.Rows(gv1.Rows.Count - 1).Cells(colAsset_Group_Code).Value = clsCommon.myCstr(dr("Group_Code"))
    '                                gv1.Rows(gv1.Rows.Count - 1).Cells(colAsset_Group_Desc).Value = clsCommon.myCstr(dr("Group_Desc"))
    '                                gv1.Rows(gv1.Rows.Count - 1).Cells(colAsset_Category_Code).Value = clsCommon.myCstr(dr("Category_Code"))
    '                                gv1.Rows(gv1.Rows.Count - 1).Cells(colAsset_Category_Desc).Value = clsCommon.myCstr(dr("Cat_Desc"))
    '                                gv1.Rows(gv1.Rows.Count - 1).Cells(colDepMethodCode).Value = clsCommon.myCstr(dr("Dep_Method_Code"))
    '                                gv1.Rows(gv1.Rows.Count - 1).Cells(colDepMethod).Value = clsCommon.myCstr(dr("MethodName"))
    '                                gv1.Rows(gv1.Rows.Count - 1).Cells(colPeriodCode).Value = clsCommon.myCstr(dr("Dep_Period_Code"))
    '                                gv1.Rows(gv1.Rows.Count - 1).Cells(colPeriod).Value = clsCommon.myCstr(dr("period_Desc"))
    '                                gv1.Rows(gv1.Rows.Count - 1).Cells(colSalvageValue).Value = clsCommon.myCdbl(dr("Book_Salvage_Value"))
    '                                gv1.Rows(gv1.Rows.Count - 1).Cells(colSourceOrgValue).Value = clsCommon.myCdbl(dr("Book_Source_Original_value"))
    '                                gv1.Rows(gv1.Rows.Count - 1).Cells(colEstimatedLife).Value = clsCommon.myCdbl(dr("Book_Estimated_Life"))
    '                                gv1.Rows(gv1.Rows.Count - 1).Cells(colOpening_Value).Value = clsCommon.myCdbl(dr("Asset_Value"))
    '                                gv1.Rows(gv1.Rows.Count - 1).Cells(colWork_Expense).Value = dblCurrExpence
    '                                gv1.Rows(gv1.Rows.Count - 1).Cells(colAssemble_Cost).Value = 0
    '                                gv1.Rows(gv1.Rows.Count - 1).Cells(colOpening_Value_Tax).Value = clsCommon.myCdbl(dr("Asset_Value_Tax"))
    '                                gv1.Rows(gv1.Rows.Count - 1).Cells(colTax_Non_Recoverable).Value = clsCommon.myCdbl(dr("Tax_Non_Recoverable"))
    '                                gv1.Rows(gv1.Rows.Count - 1).Cells(colTax_Recoverable).Value = clsCommon.myCdbl(dr("Tax_Recoverable"))
    '                                Dim strFormula As String = clsCommon.myCstr(dr("Formula"))
    '                                If strFormula.Contains("^") = True Then
    '                                    Throw New Exception("Formula for depreciation method " & gv1.Rows(gv1.Rows.Count - 1).Cells(colDepMethodCode).Value & " contains invalid charecter(^)")
    '                                End If
    '                                Dim str As String = clsCommon.myFormat(dblBaseAmt).Replace(",", "")
    '                                gv1.Rows(gv1.Rows.Count - 1).Cells(colDepFormula).Value = strFormula
    '                                strFormula = strFormula.Replace(clsDepreciationParameter.BNV, "(" & clsCommon.myCstr(dblBaseAmt) & "/1.0)")
    '                                strFormula = strFormula.Replace(clsDepreciationParameter.BEY, "(" & clsCommon.myCstr(clsCommon.myCdbl(dr("Book_Estimated_Life"))) & "/1.0)")
    '                                strFormula = strFormula.Replace(clsDepreciationParameter.BSV, "(" & clsCommon.myCstr(clsCommon.myCdbl(dr("Book_Salvage_value")) / IIf(clsCommon.myCdbl(dr("Book_Source_Original_value")) = 0, IIf(dblBaseAmt = 0, 1, dblBaseAmt), clsCommon.myCdbl(dr("Book_Source_Original_value"))) * dblBaseAmt) & "/1.0)")
    '                                strFormula = strFormula.Replace(clsDepreciationParameter.BSR, "(" & clsCommon.myCstr(clsCommon.myCdbl(dr("Book_Salvage_Rate")) / IIf(clsCommon.myCdbl(dr("Book_Source_Original_value")) = 0, IIf(dblBaseAmt = 0, 1, dblBaseAmt), clsCommon.myCdbl(dr("Book_Source_Original_value"))) * dblBaseAmt) & "/1.0)")
    '                                strFormula = strFormula.Replace(clsDepreciationParameter.BDT, "(" & clsCommon.myCstr(intDepCount) & "/1.0)")
    '                                strFormula = strFormula.Replace(clsDepreciationParameter.BRT, "(" & clsCommon.myCstr(dblDepRate) & "/1.0)")
    '                                strFormula = strFormula.Replace(clsDepreciationParameter.BCLD, "(" & clsCommon.myCstr(DateDiff(DateInterval.Day, dtPermDepDate.AddDays(-1), dtForChecTo)) & "/1.0)")
    '                                Dim CheckFormulaType As String = clsDBFuncationality.getSingleValue("select type from TSPL_DEPRECIATION_METHOD where code ='" & gv1.Rows(gv1.Rows.Count - 1).Cells(colDepMethodCode).Value & "'")
    '                                Dim dblDepAmt As Double = 0
    '                                Dim dblDifference As Double = 0
    '                                Dim originalDepRate As Double = 0
    '                                If clsCommon.CompairString(CheckFormulaType, "Formula") = CompairStringResult.Equal Then
    '                                    dblDepAmt = Math.Round(clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select  " + strFormula + "")), 3, MidpointRounding.ToEven)
    '                                ElseIf clsCommon.CompairString(CheckFormulaType, "Rate") = CompairStringResult.Equal Then
    '                                    dblDepRate = Math.Round(clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select  " + strFormula + "")), 3, MidpointRounding.ToEven)
    '                                    dblDepAmt = dblBaseAmt * dblDepRate / 100
    '                                    originalDepRate = dblDepRate
    '                                Else
    '                                    dblDepAmt = Math.Round(clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select  " + strFormula + "")), 3, MidpointRounding.ToEven)
    '                                End If


    '                                If clsCommon.CompairString(clsCommon.myCstr(depreciationCalculation), "DL") = CompairStringResult.Equal Then
    '                                    dblDepAmt = dblDepAmt * (DateDiff(DateInterval.Day, dtPermDepDate.AddDays(-1), dtForChecTo)) / DaysInYear
    '                                ElseIf clsCommon.CompairString(clsCommon.myCstr(depreciationCalculation), "MT") = CompairStringResult.Equal Then
    '                                    If clsCommon.CompairString(clsCommon.myCstr(dr("FiscalPeriodRunPerm" + clsCommon.myCstr(dtStartDate.Month) + "")), "YES") = CompairStringResult.Equal Then
    '                                        dblDepAmt = dblDepAmt * DateDiff(DateInterval.Month, dtPermDepDate.AddDays(1), dtForChecTo.AddDays(1)) / 12.0
    '                                    Else
    '                                        dblDepAmt = dblDepAmt * DateDiff(DateInterval.Month, LastTempDepDate.AddDays(1), dtForChecTo.AddDays(1)) / 12.0
    '                                    End If
    '                                ElseIf clsCommon.CompairString(clsCommon.myCstr(depreciationCalculation), "QTL") = CompairStringResult.Equal Then
    '                                    If clsCommon.CompairString(clsCommon.myCstr(dr("FiscalPeriodRunPerm" + clsCommon.myCstr(dtStartDate.Month) + "")), "YES") = CompairStringResult.Equal Then
    '                                        dblDepAmt = dblDepAmt * DateDiff(DateInterval.Quarter, dtPermDepDate.AddDays(1), dtForChecTo.AddDays(1)) / 4.0
    '                                    Else
    '                                        dblDepAmt = dblDepAmt * DateDiff(DateInterval.Quarter, LastTempDepDate.AddDays(1), dtForChecTo.AddDays(1)) / 4.0
    '                                    End If
    '                                ElseIf clsCommon.CompairString(clsCommon.myCstr(depreciationCalculation), "YRY") = CompairStringResult.Equal Then
    '                                    If clsCommon.CompairString(clsCommon.myCstr(dr("FiscalPeriodRunPerm" + clsCommon.myCstr(dtStartDate.Month) + "")), "YES") = CompairStringResult.Equal Then
    '                                        dblDepAmt = dblDepAmt * DateDiff(DateInterval.Year, dtPermDepDate, dtForChecTo.AddDays(1)) / 1.0
    '                                    Else
    '                                        dblDepAmt = dblDepAmt * DateDiff(DateInterval.Year, LastTempDepDate, dtForChecTo.AddDays(1)) / 1.0
    '                                    End If
    '                                End If
    '                                gv1.Rows(gv1.Rows.Count - 1).Cells(colAmtBeforeDep).Value = dblBaseAmt
    '                                If clsCommon.myCdbl(dr("Dep_Rate")) = 0 Then
    '                                    If dblBaseAmt = 0 Then
    '                                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDepRate).Value = 0
    '                                    Else
    '                                        If clsCommon.CompairString(CheckFormulaType, "Formula") = CompairStringResult.Equal Then
    '                                            Dim FrmulBaseAmount As Double = (dblDepAmt / dblBaseAmt) * 100
    '                                            gv1.Rows(gv1.Rows.Count - 1).Cells(colDepRate).Value = Math.Round(FrmulBaseAmount, 3, MidpointRounding.ToEven)
    '                                        Else
    '                                            gv1.Rows(gv1.Rows.Count - 1).Cells(colDepRate).Value = dblDepRate
    '                                        End If
    '                                    End If
    '                                Else
    '                                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDepRate).Value = dblDepRate
    '                                End If
    '                                If dblDepAmt < 0 Then
    '                                    dblDepAmt = 0
    '                                End If
    '                                If dblDepAmt > dblBaseAmt Then
    '                                    dblDepAmt = dblBaseAmt
    '                                End If
    '                                If dblDepAmt > dclMaxDepAmt Then
    '                                    dblDepAmt = dclMaxDepAmt
    '                                    dclMaxDepAmtflag = True
    '                                End If
    '                                adjustment = Math.Pow(10, AllowDecimalInFixedAsset)
    '                                If clsCommon.CompairString(clsCommon.myCstr(AllowRoundInFixedAsset), "Upper") = CompairStringResult.Equal Then
    '                                    dblDepAmt = Math.Ceiling(dblDepAmt * adjustment) / adjustment
    '                                ElseIf clsCommon.CompairString(clsCommon.myCstr(AllowRoundInFixedAsset), "Round") = CompairStringResult.Equal Then
    '                                    dblDepAmt = Math.Round(dblDepAmt, AllowDecimalInFixedAsset)
    '                                ElseIf clsCommon.CompairString(clsCommon.myCstr(AllowRoundInFixedAsset), "Lower") = CompairStringResult.Equal Then
    '                                    dblDepAmt = Math.Floor(dblDepAmt * adjustment) / adjustment
    '                                Else
    '                                    dblDepAmt = Math.Round(dblDepAmt, 2, MidpointRounding.ToEven)
    '                                End If
    '                                If clsCommon.CompairString(clsCommon.myCstr(AllowRoundInFixedAsset), "Upper") = CompairStringResult.Equal Then
    '                                    dblDepRate = Math.Ceiling(dblDepRate * adjustment) / adjustment
    '                                ElseIf clsCommon.CompairString(clsCommon.myCstr(AllowRoundInFixedAsset), "Round") = CompairStringResult.Equal Then
    '                                    dblDepRate = Math.Round(dblDepRate, AllowDecimalInFixedAsset)
    '                                ElseIf clsCommon.CompairString(clsCommon.myCstr(AllowRoundInFixedAsset), "Lower") = CompairStringResult.Equal Then
    '                                    dblDepRate = Math.Floor(dblDepRate * adjustment) / adjustment
    '                                Else
    '                                    dblDepRate = Math.Round(dblDepRate, 2, MidpointRounding.ToEven)
    '                                End If
    '                                dblDifference = originalDepRate - dblDepRate
    '                                gv1.Rows(gv1.Rows.Count - 1).Cells(colRoundOffRate).Value = dblDifference
    '                                gv1.Rows(gv1.Rows.Count - 1).Cells(colDepAmount).Value = dblDepAmt
    '                                gv1.Rows(gv1.Rows.Count - 1).Cells(colPrevDepAmount).Value = dblPrevDep
    '                                If clsCommon.CompairString(clsCommon.myCstr(dr("FiscalPeriodRunPerm" + clsCommon.myCstr(dtStartDate.Month) + "")), "YES") = CompairStringResult.Equal Then
    '                                    gv1.Rows(gv1.Rows.Count - 1).Cells(colAssetValue).Value = dblBaseAmt - dblDepAmt
    '                                Else
    '                                    gv1.Rows(gv1.Rows.Count - 1).Cells(colAssetValue).Value = dblBaseAmt - dblDepAmt
    '                                End If

    '                                dblBaseAmt -= dblDepAmt

    '                                If False Then ''clsCommon.myLen(dr("Dep_Method_Tax_Code")) > 0
    '                                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDepMethodTaxCode).Value = clsCommon.myCstr(dr("Dep_Method_Tax_Code"))
    '                                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDepMethodTax).Value = clsCommon.myCstr(dr("MethodNameTax"))
    '                                    strFormula = clsCommon.myCstr(dr("Formula_Tax"))
    '                                    If strFormula.Contains("^") = True Then
    '                                        Throw New Exception("Formula for depreciation method " & gv1.Rows(gv1.Rows.Count - 1).Cells(colDepMethodCode).Value & " contains invalid charecter(^)")
    '                                    End If
    '                                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDepFormulaTax).Value = strFormula
    '                                    strFormula = strFormula.Replace(clsDepreciationParameter.BNV, "(" & clsCommon.myCstr(dblBaseAmtTax) & "/1.0)")
    '                                    strFormula = strFormula.Replace(clsDepreciationParameter.BEY, "(" & clsCommon.myCstr(clsCommon.myCdbl(dr("Book_Estimated_Life"))) & "/1.0)")
    '                                    strFormula = strFormula.Replace(clsDepreciationParameter.BSV, "(" & clsCommon.myCstr(clsCommon.myCdbl(dr("Book_Salvage_Value"))) & "/1.0)")
    '                                    strFormula = strFormula.Replace(clsDepreciationParameter.BSR, "(" & clsCommon.myCstr(clsCommon.myCdbl(dr("Book_Salvage_Rate"))) & "/1.0)")
    '                                    strFormula = strFormula.Replace(clsDepreciationParameter.BDT, "(" & clsCommon.myCstr(intDepCount) & "/1.0)")
    '                                    strFormula = strFormula.Replace(clsDepreciationParameter.BRT, "(" & clsCommon.myCstr(dblDepRateTax) & "/1.0)")
    '                                    strFormula = strFormula.Replace(clsDepreciationParameter.BCLD, "(" & clsCommon.myCstr(DateDiff(DateInterval.Day, dtPermDepDate.AddDays(-1), dtForChecTo)) & "/1.0)")
    '                                    If clsCommon.CompairString(CheckFormulaType, "Formula") = CompairStringResult.Equal Then
    '                                        dblDepAmt = Math.Round(clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select  " + strFormula + "")), 3, MidpointRounding.ToEven)
    '                                    ElseIf clsCommon.CompairString(CheckFormulaType, "Rate") = CompairStringResult.Equal Then
    '                                        dblDepRateTax = Math.Round(clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select  " + strFormula + "")), 3, MidpointRounding.ToEven)
    '                                        dblDepAmt = dblBaseAmt * dblDepRate / 100
    '                                        originalDepRate = dblDepRateTax
    '                                    Else
    '                                        dblDepAmt = Math.Round(clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select  " + strFormula + "")), 3, MidpointRounding.ToEven)
    '                                    End If
    '                                    adjustment = Math.Pow(10, AllowDecimalInFixedAsset)
    '                                    If clsCommon.CompairString(clsCommon.myCstr(AllowRoundInFixedAsset), "Upper") = CompairStringResult.Equal Then
    '                                        dblDepAmt = Math.Ceiling(dblDepAmt * adjustment) / adjustment
    '                                    ElseIf clsCommon.CompairString(clsCommon.myCstr(AllowRoundInFixedAsset), "Round") = CompairStringResult.Equal Then
    '                                        dblDepAmt = Math.Round(dblDepAmt, AllowDecimalInFixedAsset)
    '                                    ElseIf clsCommon.CompairString(clsCommon.myCstr(AllowRoundInFixedAsset), "Lower") = CompairStringResult.Equal Then
    '                                        dblDepAmt = Math.Floor(dblDepAmt * adjustment) / adjustment
    '                                    Else
    '                                        dblDepAmt = Math.Round(dblDepAmt, 2, MidpointRounding.ToEven)
    '                                    End If

    '                                    If clsCommon.CompairString(clsCommon.myCstr(AllowRoundInFixedAsset), "Upper") = CompairStringResult.Equal Then
    '                                        dblDepRateTax = Math.Ceiling(dblDepRateTax * adjustment) / adjustment
    '                                    ElseIf clsCommon.CompairString(clsCommon.myCstr(AllowRoundInFixedAsset), "Round") = CompairStringResult.Equal Then
    '                                        dblDepRateTax = Math.Round(dblDepRateTax, AllowDecimalInFixedAsset)
    '                                    ElseIf clsCommon.CompairString(clsCommon.myCstr(AllowRoundInFixedAsset), "Lower") = CompairStringResult.Equal Then
    '                                        dblDepRateTax = Math.Floor(dblDepRateTax * adjustment) / adjustment
    '                                    Else
    '                                        dblDepRateTax = Math.Round(dblDepRateTax, 2, MidpointRounding.ToEven)
    '                                    End If
    '                                    If clsCommon.CompairString(clsCommon.myCstr(depreciationCalculation), "DL") = CompairStringResult.Equal Then
    '                                        If clsCommon.GetDateWithEndTime(dtPermDepDate) = depStartDate Then
    '                                            dblDepAmt = dblDepAmt * (DateDiff(DateInterval.Day, dtPermDepDate.AddDays(-1), dtForChecTo)) / DaysInYear '365.0
    '                                        Else
    '                                            dblDepAmt = dblDepAmt * DateDiff(DateInterval.Day, dtPermDepDate, dtForChecTo) / DaysInYear '365.0
    '                                        End If
    '                                    ElseIf clsCommon.CompairString(clsCommon.myCstr(depreciationCalculation), "MT") = CompairStringResult.Equal Then
    '                                        If clsCommon.CompairString(clsCommon.myCstr(dr("FiscalPeriodRunPerm" + clsCommon.myCstr(dtStartDate.Month) + "")), "YES") = CompairStringResult.Equal Then
    '                                            dblDepAmt = dblDepAmt * DateDiff(DateInterval.Month, dtPermDepDate.AddDays(1), dtForChecTo.AddDays(1)) / 12.0
    '                                        Else
    '                                            dblDepAmt = dblDepAmt * DateDiff(DateInterval.Month, LastTempDepDate.AddDays(1), dtForChecTo.AddDays(1)) / 12.0
    '                                        End If
    '                                    ElseIf clsCommon.CompairString(clsCommon.myCstr(depreciationCalculation), "QTL") = CompairStringResult.Equal Then
    '                                        If clsCommon.CompairString(clsCommon.myCstr(dr("FiscalPeriodRunPerm" + clsCommon.myCstr(dtStartDate.Month) + "")), "YES") = CompairStringResult.Equal Then
    '                                            dblDepAmt = dblDepAmt * DateDiff(DateInterval.Quarter, dtPermDepDate.AddDays(1), dtForChecTo.AddDays(1)) / 4.0
    '                                        Else
    '                                            dblDepAmt = dblDepAmt * DateDiff(DateInterval.Quarter, LastTempDepDate.AddDays(1), dtForChecTo.AddDays(1)) / 4.0
    '                                        End If
    '                                    ElseIf clsCommon.CompairString(clsCommon.myCstr(depreciationCalculation), "YRY") = CompairStringResult.Equal Then
    '                                        If clsCommon.CompairString(clsCommon.myCstr(dr("FiscalPeriodRunPerm" + clsCommon.myCstr(dtStartDate.Month) + "")), "YES") = CompairStringResult.Equal Then
    '                                            dblDepAmt = dblDepAmt * DateDiff(DateInterval.Year, dtPermDepDate, dtForChecTo.AddDays(1)) / 1.0
    '                                        Else
    '                                            dblDepAmt = dblDepAmt * DateDiff(DateInterval.Year, LastTempDepDate, dtForChecTo.AddDays(1)) / 1.0
    '                                        End If
    '                                    End If
    '                                    gv1.Rows(gv1.Rows.Count - 1).Cells(colAmtBeforeDepTax).Value = dblBaseAmtTax
    '                                    If clsCommon.myCdbl(dr("Dep_Tax_Rate")) = 0 Then
    '                                        If dblBaseAmtTax = 0 Then
    '                                            gv1.Rows(gv1.Rows.Count - 1).Cells(colDepRateTax).Value = 0
    '                                        Else
    '                                            If clsCommon.CompairString(CheckFormulaType, "Formula") = CompairStringResult.Equal Then
    '                                                Dim FrmulBaseAmount As Double = (dblDepAmt / dblBaseAmtTax) * 100
    '                                                gv1.Rows(gv1.Rows.Count - 1).Cells(colDepRateTax).Value = Math.Round(FrmulBaseAmount, 3, MidpointRounding.ToEven)
    '                                            Else
    '                                                gv1.Rows(gv1.Rows.Count - 1).Cells(colDepRateTax).Value = dblDepRateTax
    '                                            End If
    '                                        End If
    '                                    Else
    '                                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDepRateTax).Value = dblDepRateTax
    '                                    End If
    '                                    If dblDepAmt < 0 Then
    '                                        dblDepAmt = 0
    '                                    End If
    '                                    If dblDepAmt > dblBaseAmtTax Then
    '                                        dblDepAmt = dblBaseAmtTax
    '                                    End If

    '                                    adjustment = Math.Pow(10, AllowDecimalInFixedAsset)

    '                                    If clsCommon.CompairString(clsCommon.myCstr(AllowRoundInFixedAsset), "Upper") = CompairStringResult.Equal Then
    '                                        dblDepAmt = Math.Ceiling(dblDepAmt * adjustment) / adjustment
    '                                    ElseIf clsCommon.CompairString(clsCommon.myCstr(AllowRoundInFixedAsset), "Round") = CompairStringResult.Equal Then
    '                                        dblDepAmt = Math.Round(dblDepAmt, AllowDecimalInFixedAsset)
    '                                    ElseIf clsCommon.CompairString(clsCommon.myCstr(AllowRoundInFixedAsset), "Lower") = CompairStringResult.Equal Then
    '                                        dblDepAmt = Math.Floor(dblDepAmt * adjustment) / adjustment
    '                                    Else
    '                                        dblDepAmt = Math.Round(dblDepAmt, 2, MidpointRounding.ToEven)
    '                                    End If
    '                                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDepAmountTax).Value = dblDepAmt
    '                                    gv1.Rows(gv1.Rows.Count - 1).Cells(colPrevDepAmountTax).Value = dblPrevDepTax
    '                                    If clsCommon.CompairString(clsCommon.myCstr(dr("FiscalPeriodRunPerm" + clsCommon.myCstr(dtStartDate.Month) + "")), "YES") = CompairStringResult.Equal Then
    '                                        gv1.Rows(gv1.Rows.Count - 1).Cells(colAssetValueTax).Value = dblBaseAmtTax - dblDepAmt
    '                                    Else
    '                                        gv1.Rows(gv1.Rows.Count - 1).Cells(colAssetValueTax).Value = dblBaseAmtTax - dblDepAmt
    '                                    End If
    '                                    dblBaseAmtTax -= dblDepAmt
    '                                    If clsCommon.myCdbl(gv1.Rows(gv1.Rows.Count - 1).Cells(colAssetValueTax).Value) < 0 Then
    '                                        gv1.Rows(gv1.Rows.Count - 1).Cells(colAssetValueTax).Value = 0
    '                                    End If
    '                                End If
    '                                If clsCommon.CompairString(clsCommon.myCstr(dr("FiscalPeriodRunPerm" + clsCommon.myCstr(dtStartDate.Month) + "")), "YES") = CompairStringResult.Equal Then
    '                                    gv1.Rows(gv1.Rows.Count - 1).Cells(colIsPermDep).Value = "YES"
    '                                Else
    '                                    gv1.Rows(gv1.Rows.Count - 1).Cells(colIsPermDep).Value = "NO"
    '                                End If
    '                                gv1.Rows(gv1.Rows.Count - 1).Cells(colManualDep).Value = False
    '                                Try
    '                                    If dr("Start_Date") IsNot DBNull.Value Then
    '                                        If clsCommon.GetDateWithStartTime(dr("Start_Date")) < clsCommon.GetDateWithStartTime(ERPStartDate) Then
    '                                            gv1.Rows(gv1.Rows.Count - 1).Cells(colManualDep).Value = True
    '                                        End If
    '                                    End If
    '                                Catch ex As Exception
    '                                End Try
    '                                intDepCount += 1
    '                                dclTotalDep += dblDepAmt
    '                            End If
    '                            dtPermDepDate = DocDate.AddDays(1)
    '                        End If
    '                        If clsCommon.CompairString(clsCommon.myCstr(dr("FiscalPeriodRunPerm" + clsCommon.myCstr(dtStartDate.Month) + "")), "NO") = CompairStringResult.Equal Then
    '                            If chkReverseTempDep.Checked Then
    '                                If (gv1.Rows.Count - 1) < 0 Then
    '                                    dtStartDate = dtStartDate.AddMonths(1)
    '                                    Continue While
    '                                End If
    '                                gv1.Rows(gv1.Rows.Count - 1).Cells(colIs_Reverse_Dep).Value = 1
    '                                gv1.Rows(gv1.Rows.Count - 1).Cells(colReverseDate).Value = clsCommon.myCDate(gv1.Rows(gv1.Rows.Count - 1).Cells(colDate).Value).AddDays(1)
    '                            Else
    '                                If (gv1.Rows.Count - 1) < 0 Then
    '                                    dtStartDate = dtStartDate.AddMonths(1)
    '                                    Continue While
    '                                End If
    '                                gv1.Rows(gv1.Rows.Count - 1).Cells(colIs_Reverse_Dep).Value = 0
    '                                gv1.Rows(gv1.Rows.Count - 1).Cells(colReverseDate).ReadOnly = True
    '                            End If
    '                        End If
    '                        dtStartDate = dtStartDate.AddMonths(1)
    '                        If dclMaxDepAmtflag Then
    '                            Continue For
    '                        End If
    '                    End While
    '                Catch ex As Exception
    '                    clsCommon.ProgressBarPercentHide()
    '                    Throw New Exception(ex.Message)
    '                End Try
    '            Next
    '            If chkAutoProcess.Checked AndAlso gv1.Rows.Count > 0 Then
    '                ProcessData()
    '                LoadBlankGrid()
    '                clsCommon.ProgressBarPercentHide()
    '                clsCommon.MyMessageBoxShow(Me, "Processes successfully", Me.Text)
    '            Else
    '                clsCommon.ProgressBarPercentHide()
    '            End If
    '        End If
    '    Catch ex As Exception
    '        clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
    '    End Try
    'End Sub

    Function ProcessData() As Boolean
        Dim arr As New List(Of clsAssetDepreciation)
        For ii As Integer = 0 To gv1.Rows.Count - 1
            If clsCommon.myCBool(gv1.Rows(ii).Cells(colSelect).Value) Then
                Dim obj As New clsAssetDepreciation()
                obj.Document_Date = clsCommon.myCDate(gv1.Rows(ii).Cells(colDate).Value)
                obj.Asset_code = clsCommon.myCstr(gv1.Rows(ii).Cells(colAssetCode).Value)
                obj.Asset_Disposal_Code = clsCommon.myCstr(gv1.Rows(ii).Cells(colAssetName).Tag)
                obj.Dep_Method_Code = clsCommon.myCstr(gv1.Rows(ii).Cells(colDepMethodCode).Value)
                obj.Dep_Period_Code = clsCommon.myCstr(gv1.Rows(ii).Cells(colPeriodCode).Value)
                obj.Value_Before_Depreciation = clsCommon.myCdbl(gv1.Rows(ii).Cells(colAmtBeforeDep).Value)
                obj.Asset_value = clsCommon.myCdbl(gv1.Rows(ii).Cells(colAssetValue).Value)
                obj.Dep_Amount = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDepAmount).Value)
                obj.DepRate = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDepRate).Value)
                obj.RoundOffRate = clsCommon.myCdbl(gv1.Rows(ii).Cells(colRoundOffRate).Value)
                obj.Dep_Method_Tax_Code = clsCommon.myCstr(gv1.Rows(ii).Cells(colDepMethodTaxCode).Value)
                obj.Value_Before_Depreciation_Tax = clsCommon.myCdbl(gv1.Rows(ii).Cells(colAmtBeforeDepTax).Value)
                obj.Asset_value_Tax = clsCommon.myCdbl(gv1.Rows(ii).Cells(colAssetValueTax).Value)
                obj.Dep_Amount_Tax = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDepAmountTax).Value)
                obj.DepRateTax = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDepRateTax).Value)
                obj.Is_Permanent = clsCommon.myCstr(gv1.Rows(ii).Cells(colIsPermDep).Value)
                obj.Opening_Value = clsCommon.myCdbl(gv1.Rows(ii).Cells(colOpening_Value).Value)
                obj.Opening_Value_Tax = clsCommon.myCdbl(gv1.Rows(ii).Cells(colOpening_Value_Tax).Value)
                obj.Work_Expense = clsCommon.myCdbl(gv1.Rows(ii).Cells(colWork_Expense).Value)
                obj.Assemble_Cost = clsCommon.myCdbl(gv1.Rows(ii).Cells(colAssemble_Cost).Value)
                obj.Disposable_Value = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDisposable_Value).Value)
                'obj.Accumulated_Dep = clsCommon.myCdbl(gv1.Rows(ii).Cells(colAccumulated_Dep).Value)
                obj.Location_Code = clsCommon.myCstr(gv1.Rows(ii).Cells(colLocation_Code).Value)
                obj.Asset_Group_Code = clsCommon.myCstr(gv1.Rows(ii).Cells(colAsset_Group_Code).Value)
                obj.Asset_Category_Code = clsCommon.myCstr(gv1.Rows(ii).Cells(colAsset_Category_Code).Value)
                '' reverse functionalities
                obj.Is_Reverse_Dep = clsCommon.myCdbl(gv1.Rows(ii).Cells(colIs_Reverse_Dep).Value)
                If obj.Is_Reverse_Dep = 1 Then
                    obj.Reverse_Date = clsCommon.myCDate(gv1.Rows(ii).Cells(colReverseDate).Value)
                Else
                    obj.Reverse_Date = Nothing
                End If
                obj.Tax_Non_Recoverable = clsCommon.myCdbl(gv1.Rows(ii).Cells(colTax_Non_Recoverable).Value)
                obj.Tax_Recoverable = clsCommon.myCdbl(gv1.Rows(ii).Cells(colTax_Recoverable).Value)
                arr.Add(obj)
            End If
        Next
        If arr Is Nothing OrElse arr.Count <= 0 Then
            Throw New Exception("No Asset found to depreciate")
        End If
        clsAssetDepreciation.SaveData(arr)
        Return True
    End Function

    Private Function GetAssetDepreciationAmt(ByVal AssetCode As String, ByVal TransEndDate As Date) As Decimal
        'Dim qry As String = "select sum(Dep_Amount) as Dep_Amount,sum(Dep_Amount_Tax) as Dep_Amount_Tax from TSPL_ASSET_DEPRECIATION where Asset_Code='" + AssetCode + "'"

        Dim qry As String = "select sum(Dep_Amount) as Dep_Amount,sum(Dep_Amount_Tax) as Dep_Amount_Tax from (
select Asset_Code, Dep_Amount,Dep_Amount_Tax from TSPL_ASSET_DEPRECIATION where Asset_Code='" + AssetCode + "'
union all
select Asset_Code,-1* Dep_Amount as Dep_Amount,-1*Dep_Amount_Tax as Dep_Amount_Tax from TSPL_ASSET_DEPRECIATION_ADJUSTMENT where Asset_Code='" + AssetCode + "' and Adjustment_Date <= '" + clsCommon.GetPrintDate(TransEndDate, "dd/MMM/yyyy") + "'
)xx"
        Return clsCommon.myCDecimal(clsDBFuncationality.getSingleValue(qry))

    End Function

    Function GetAssetLastDepData(ByVal Asset_Code As String) As DataTable
        Dim qry As String = " select Max(Perm_Doc_Date) as Perm_Doc_Date,max(Temp_Doc_Date) as Temp_Doc_Date,min(Perm_Asset_Value) as Perm_Asset_Value, " &
                            " min(Temp_Asset_Value) as Temp_Asset_Value," &
                            " min(Perm_Asset_Value_Tax) as Perm_Asset_Value_Tax,min(Temp_Asset_Value_Tax) as Temp_Asset_Value_Tax from ( " &
                            " Select max(Document_Date) as Perm_Doc_Date," &
                            " max(case when Is_Permanent='No' then Document_Date else null end) as Temp_Doc_Date, " &
                            " case when  datepart(month,max( Document_Date))=3 then (max(Value_Before_Depreciation)-sum(Dep_Amount)) else min(Value_Before_Depreciation) end as Perm_Asset_Value, " &
                            " min(case when Is_Permanent='No' then Asset_value  end) as Temp_Asset_Value, " &
                            " min(case when Is_Permanent='Yes' then Asset_value_Tax end) as Perm_Asset_Value_Tax," &
                            " min(case when Is_Permanent='No' then Asset_value_Tax end) as Temp_Asset_Value_Tax " &
                            " from TSPL_ASSET_DEPRECIATION where Asset_Code='" & Asset_Code & "'" &
                            " union all " &
                            " select Acqusition_Date as Perm_Doc_Date,Null as Temp_Doc_Date,isnull(Book_Source_value,0)+isnull(Tax_Non_Recoverable ,0)  as Perm_Asset_Value,Null as Temp_Asset_Value, " &
                            " isnull(Book_Source_value,0)+isnull(Tax_Non_Recoverable ,0)  as Perm_Asset_Value_Tax, " &
                            " Null as Temp_Asset_Value_Tax  from TSPL_ACQUISITION_DETAIL where Asset_Code='" & Asset_Code & "' and Book_Source_Original_value>Book_Source_value " &
                            " and Book_Source_value>Book_Salvage_Value) as Tab"

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        Return dt
    End Function
    Function GetTotalTempDep(ByVal dr As DataRow) As Integer
        Dim totalTempDep As Integer = 0
        For intLoop As Integer = 1 To 12
            If clsCommon.CompairString(dr.Item("FiscalPeriodRun" & intLoop), "YES") = CompairStringResult.Equal Then
                totalTempDep = totalTempDep + 1
            End If
        Next
        Return totalTempDep
    End Function

    Function GetTotalPermDep(ByVal dr As DataRow) As Integer
        Dim totalPermDep As Integer = 0
        For intLoop As Integer = 1 To 12
            If clsCommon.CompairString(dr.Item("FiscalPeriodRunPerm" & intLoop), "YES") = CompairStringResult.Equal Then
                totalPermDep = totalPermDep + 1
            End If
        Next
        Return totalPermDep
    End Function

    Private Sub LoadSavedData()
        gv2.DataSource = Nothing
        gv2.Rows.Clear()
        gv2.Columns.Clear()
        Dim qry As String = "select TSPL_ASSET_DEPRECIATION.Document_Code,CONVERT(varchar, TSPL_ASSET_DEPRECIATION.Document_Date,103) as Document_Date,TSPL_ASSET_DEPRECIATION.Asset_Code,"
        qry += " TSPL_ACQUISITION_DETAIL.Asset_Name,TSPL_ACQUISITION_DETAIL.Asset_Specification,TSPL_ACQUISITION_HEAD.Loc_Code as [Location Code],loc.Location_Desc as [Location Desc],TSPL_ACQUISITION_DETAIL.Group_Code as [Group Code],AG.Description as [Group Desc],TSPL_ACQUISITION_DETAIL.Category_Code as [Category Code],AC.Description as [Category Desc],TSPL_ACQUISITION_DETAIL.Dep_Method_Code,TSPL_DEPRECIATION_METHOD.Description as Dep_method_Name,"
        qry += " TSPL_ACQUISITION_DETAIL.Dep_Method_Tax_Code,DepMethodTax.Description as Dep_method_Tax_Name,TSPL_ASSET_DEPRECIATION.Opening_Value,TSPL_ASSET_DEPRECIATION.Opening_Value_Tax,TSPL_ASSET_DEPRECIATION.Work_Expense,TSPL_ASSET_DEPRECIATION.Assemble_Cost,TSPL_ASSET_DEPRECIATION.Assemble_Cost,TSPL_ASSET_DEPRECIATION.Disposable_Value,TSPL_ASSET_DEPRECIATION.Value_Before_Depreciation,TSPL_ASSET_DEPRECIATION.Dep_Amount,TSPL_ASSET_DEPRECIATION.DepRate,TSPL_ASSET_DEPRECIATION.Asset_value ,TSPL_ASSET_DEPRECIATION.Value_Before_Depreciation_Tax,TSPL_ASSET_DEPRECIATION.Dep_Amount_Tax,TSPL_ASSET_DEPRECIATION.DepRateTax ,TSPL_ASSET_DEPRECIATION.Asset_value_Tax,TSPL_ASSET_DEPRECIATION.Is_Reverse_Dep,TSPL_ASSET_DEPRECIATION.Reverse_Date"
        qry += " from TSPL_ASSET_DEPRECIATION "
        qry += " left outer join TSPL_ACQUISITION_DETAIL on TSPL_ACQUISITION_DETAIL.Asset_Code=TSPL_ASSET_DEPRECIATION.Asset_Code and isnull(TSPL_ACQUISITION_DETAIL.asset_merged,0)<>1 "
        qry += " left outer join TSPL_ACQUISITION_HEAD on TSPL_ACQUISITION_HEAD.Acquisition_Code=TSPL_ACQUISITION_DETAIL.Acquisition_Code" + Environment.NewLine
        qry += " left outer join TSPL_DEPRECIATION_METHOD on TSPL_DEPRECIATION_METHOD.Code=TSPL_ASSET_DEPRECIATION.Dep_Method_Code"
        qry += " left outer join TSPL_DEPRECIATION_METHOD as DepMethodTax on DepMethodTax.Code=TSPL_ASSET_DEPRECIATION.Dep_Method_Tax_Code" &
               " left join TSPL_LOCATION_MASTER Loc on TSPL_ACQUISITION_HEAD.Loc_Code=Loc.Location_Code" &
               " left join TSPL_ASSET_GROUP AG on TSPL_ACQUISITION_DETAIL.Group_Code=AG.Group_Code " &
               " left join TSPL_ASSET_CATEGORY AC on TSPL_ACQUISITION_DETAIL.Category_code=ac.Category_Code " &
               " where 2=2 "

        If chkCreateDate.Checked Then
            qry += " and TSPL_ASSET_DEPRECIATION.Created_Date>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and TSPL_ASSET_DEPRECIATION.Created_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "'"
        Else
            qry += " and TSPL_ASSET_DEPRECIATION.Document_Date>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and TSPL_ASSET_DEPRECIATION.Document_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "'"
        End If

        If txtCategory.arrValueMember IsNot Nothing AndAlso txtCategory.arrValueMember.Count > 0 Then
            qry += " and TSPL_ACQUISITION_DETAIL.Category_code in (" + clsCommon.GetMulcallString(txtCategory.arrValueMember) + ")"
        End If
        If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
            qry += " and TSPL_ACQUISITION_HEAD.Loc_Code in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ")"
        End If
        If txtCostCenter.arrValueMember IsNot Nothing AndAlso txtCostCenter.arrValueMember.Count > 0 Then
            qry += " and TSPL_ACQUISITION_DETAIL.CostCenter_Code in (" + clsCommon.GetMulcallString(txtCostCenter.arrValueMember) + ")"
        End If
        If txtGroup.arrValueMember IsNot Nothing AndAlso txtGroup.arrValueMember.Count > 0 Then
            qry += " and TSPL_ACQUISITION_DETAIL.Group_Code in (" + clsCommon.GetMulcallString(txtGroup.arrValueMember) + ")"
        End If
        If txtAsset.arrValueMember IsNot Nothing AndAlso txtAsset.arrValueMember.Count > 0 Then
            qry += " and TSPL_ASSET_DEPRECIATION.Asset_Code in (" + clsCommon.GetMulcallString(txtAsset.arrValueMember) + ")"
        End If

        qry += " Order by TSPL_ASSET_DEPRECIATION.Document_Date"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        gv2.DataSource = Nothing
        gv2.Columns.Clear()
        gv2.Rows.Clear()
        gv2.GroupDescriptors.Clear()
        gv2.MasterTemplate.SummaryRowsBottom.Clear()
        gv2.DataSource = dt
        SetGridFormationOFGV1()
    End Sub

    Sub SetGridFormationOFGV1()
        gv2.TableElement.TableHeaderHeight = 40
        gv2.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gv2.Columns.Count - 1
            gv2.Columns(ii).ReadOnly = True
            gv2.Columns(ii).IsVisible = False
        Next
        gv2.Columns("Document_Code").IsVisible = True
        gv2.Columns("Document_Code").Width = 100
        gv2.Columns("Document_Code").HeaderText = "Document No"

        gv2.Columns("Document_Date").IsVisible = True
        gv2.Columns("Document_Date").Width = 80
        gv2.Columns("Document_Date").HeaderText = "Document Date"

        gv2.Columns("Asset_Code").IsVisible = True
        gv2.Columns("Asset_Code").Width = 100
        gv2.Columns("Asset_Code").HeaderText = "Asset Code"

        gv2.Columns("Asset_Name").IsVisible = True
        gv2.Columns("Asset_Name").Width = 150
        gv2.Columns("Asset_Name").HeaderText = "Asset Description"

        gv2.Columns("Asset_Specification").IsVisible = True
        gv2.Columns("Asset_Specification").Width = 150
        gv2.Columns("Asset_Specification").HeaderText = "Asset Specification"

        gv2.Columns("Location Code").IsVisible = True
        gv2.Columns("Location Code").Width = 100
        gv2.Columns("Location Code").HeaderText = "Location Code"

        gv2.Columns("Location Desc").IsVisible = True
        gv2.Columns("Location Desc").Width = 100
        gv2.Columns("Location Desc").HeaderText = "Location Desc"

        gv2.Columns("Group Code").IsVisible = True
        gv2.Columns("Group Code").Width = 100
        gv2.Columns("Group Code").HeaderText = If(ReadOnlyTemplateFieldsOnAcqusition = True, "Sub Group Code", "Group Code")

        gv2.Columns("Group Desc").IsVisible = True
        gv2.Columns("Group Desc").Width = 100
        gv2.Columns("Group Desc").HeaderText = If(ReadOnlyTemplateFieldsOnAcqusition = True, "Sub Group Name", "Group Desc")

        gv2.Columns("Category Code").IsVisible = True
        gv2.Columns("Category Code").Width = 100
        gv2.Columns("Category Code").HeaderText = If(ReadOnlyTemplateFieldsOnAcqusition = True, "Asset Group Code", "Category Code")

        gv2.Columns("Category Desc").IsVisible = True
        gv2.Columns("Category Desc").Width = 100
        gv2.Columns("Category Desc").HeaderText = If(ReadOnlyTemplateFieldsOnAcqusition = True, "Asset Group Name", "Category Desc")

        gv2.Columns("Dep_Method_Code").IsVisible = True
        gv2.Columns("Dep_Method_Code").Width = 100
        gv2.Columns("Dep_Method_Code").HeaderText = "Depreciation Method Code"

        gv2.Columns("Dep_method_Name").IsVisible = True
        gv2.Columns("Dep_method_Name").Width = 150
        gv2.Columns("Dep_method_Name").HeaderText = "Depreciation Method"

        gv2.Columns("Opening_Value").IsVisible = True
        gv2.Columns("Opening_Value").Width = 100
        gv2.Columns("Opening_Value").HeaderText = "Opening Value"

        gv2.Columns("Opening_Value_Tax").IsVisible = True
        gv2.Columns("Opening_Value_Tax").Width = 100
        gv2.Columns("Opening_Value_Tax").HeaderText = "Opening Value Tax"

        gv2.Columns("Work_Expense").IsVisible = True
        gv2.Columns("Work_Expense").Width = 100
        gv2.Columns("Work_Expense").HeaderText = "Work Expense"

        gv2.Columns("Assemble_Cost").IsVisible = True
        gv2.Columns("Assemble_Cost").Width = 100
        gv2.Columns("Assemble_Cost").HeaderText = "Assemble Cost"

        gv2.Columns("Value_Before_Depreciation").IsVisible = True
        gv2.Columns("Value_Before_Depreciation").Width = 100
        gv2.Columns("Value_Before_Depreciation").HeaderText = "Base Value"

        gv2.Columns("Dep_Amount").IsVisible = True
        gv2.Columns("Dep_Amount").Width = 100
        gv2.Columns("Dep_Amount").HeaderText = "Depreciation Amount"

        gv2.Columns("Asset_value").IsVisible = True
        gv2.Columns("Asset_value").Width = 100
        gv2.Columns("Asset_value").HeaderText = "Asset Value"

        gv2.Columns("Dep_Method_Tax_Code").IsVisible = True
        gv2.Columns("Dep_Method_Tax_Code").Width = 100
        gv2.Columns("Dep_Method_Tax_Code").HeaderText = "Depreciation Method Tax Code"

        gv2.Columns("Dep_method_Tax_Name").IsVisible = True
        gv2.Columns("Dep_method_Tax_Name").Width = 150
        gv2.Columns("Dep_method_Tax_Name").HeaderText = "Depreciation Method Tax"

        gv2.Columns("Value_Before_Depreciation_Tax").IsVisible = True
        gv2.Columns("Value_Before_Depreciation_Tax").Width = 100
        gv2.Columns("Value_Before_Depreciation_Tax").HeaderText = "Base Value Tax"

        gv2.Columns("Dep_Amount_Tax").IsVisible = True
        gv2.Columns("Dep_Amount_Tax").Width = 100
        gv2.Columns("Dep_Amount_Tax").HeaderText = "Depreciation Amount Tax"

        gv2.Columns("Asset_value_Tax").IsVisible = True
        gv2.Columns("Asset_value_Tax").Width = 100
        gv2.Columns("Asset_value_Tax").HeaderText = "Asset Value Tax"

        gv2.Columns("Is_Reverse_Dep").IsVisible = True
        gv2.Columns("Is_Reverse_Dep").Width = 100
        gv2.Columns("Is_Reverse_Dep").HeaderText = "Is Reverse"

        gv2.Columns("Reverse_Date").IsVisible = True
        gv2.Columns("Reverse_Date").Width = 100
        gv2.Columns("Reverse_Date").HeaderText = "Reverse Date"

        gv2.Columns("DepRate").IsVisible = True
        gv2.Columns("DepRate").Width = 100
        gv2.Columns("DepRate").HeaderText = "Dep Rate"

        gv2.Columns("DepRateTax").IsVisible = True
        gv2.Columns("DepRateTax").Width = 100
        gv2.Columns("DepRateTax").HeaderText = "Dep Rate Tax"


        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim item1 As New GridViewSummaryItem("Value_Before_Depreciation", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)
        Dim item2 As New GridViewSummaryItem("Dep_Amount", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item2)
        Dim item3 As New GridViewSummaryItem("Asset_value", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item3)
        Dim item4 As New GridViewSummaryItem("Value_Before_Depreciation_Tax", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item4)
        Dim item5 As New GridViewSummaryItem("Dep_Amount_Tax", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item5)
        Dim item6 As New GridViewSummaryItem("Asset_value_Tax", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item6)
        Dim item7 As New GridViewSummaryItem("DepRate", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item7)
        Dim item8 As New GridViewSummaryItem("DepRateTax", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item8)


        gv2.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        gv2.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
        gv2.ShowFilteringRow = True
        gv2.EnableFiltering = True
        gv2.AllowDeleteRow = True
        gv2.AllowAddNewRow = False
        gv2.ShowGroupPanel = False
        gv2.AllowColumnReorder = True
        gv2.AllowRowReorder = True
        gv2.EnableSorting = True
        gv2.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv2.MasterTemplate.ShowRowHeaderColumn = False
        gv2.TableElement.TableHeaderHeight = 40
    End Sub

    Private Sub EnableDisableCtrl(ByVal val As Boolean)
        txtTransactionDate.Enabled = val
        txtFromDate.Enabled = val
        txtToDate.Enabled = val
        txtGroup.Enabled = val
        txtLocation.Enabled = val
        txtCostCenter.Enabled = val
        txtCategory.Enabled = val
        txtAsset.Enabled = val
        chkCreateDate.Enabled = val
    End Sub

    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        gv1.Rows.Clear()
        gv1.Columns.Clear()
        gv1.DataSource = Nothing

        gv2.DataSource = Nothing
        gv2.Columns.Clear()
        gv2.Rows.Clear()
        gv2.GroupDescriptors.Clear()
        gv2.MasterTemplate.SummaryRowsBottom.Clear()

        EnableDisableCtrl(True)

    End Sub

    Private Sub gv2_ViewCellFormatting(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CellFormattingEventArgs) Handles gv2.ViewCellFormatting
        If TypeOf e.CellElement Is Telerik.WinControls.UI.GridSummaryCellElement Then
            e.CellElement.TextAlignment = ContentAlignment.MiddleRight
        End If
    End Sub

    Private Sub RadMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem1.Click
        print(EnumExportTo.Excel)
    End Sub

    Private Sub RadMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem2.Click
        print(EnumExportTo.PDF)
    End Sub

    Sub print(ByVal exporter As EnumExportTo)
        Try
            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add(IIf(chkCreateDate.Checked, "Document Create", "Document") + " Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy"))
            Dim strLoca As String = ""
            If txtAsset.arrDispalyMember IsNot Nothing AndAlso txtAsset.arrDispalyMember.Count > 0 Then
                strLoca = ""
                For Each Str As String In txtAsset.arrDispalyMember
                    If clsCommon.myLen(strLoca) > 0 Then
                        strLoca += ", "
                    End If
                    strLoca += Str
                Next
                arrHeader.Add("Asset : " + strLoca)
            End If
            'If txtlocca.arrDispalyMember IsNot Nothing AndAlso txtAsset.arrDispalyMember.Count > 0 Then
            '    strLoca = ""
            '    For Each Str As String In txtAsset.arrDispalyMember
            '        If clsCommon.myLen(strLoca) > 0 Then
            '            strLoca += ", "
            '        End If
            '        strLoca += Str
            '    Next
            '    arrHeader.Add("Asset : " + strLoca)
            'End If
            'If rbtnLocationSelect.IsChecked Then
            '    strLoca = ""
            '    For Each Str As String In cbgLocation.CheckedDisplayMember
            '        If clsCommon.myLen(strLoca) > 0 Then
            '            strLoca += ", "
            '        End If
            '        strLoca += Str
            '    Next
            '    arrHeader.Add("Location : " + strLoca)
            'End If
            'If rbtnCostCenterSelect.IsChecked Then
            '    strLoca = ""
            '    For Each Str As String In cbgCostCenter.CheckedDisplayMember
            '        If clsCommon.myLen(strLoca) > 0 Then
            '            strLoca += ", "
            '        End If
            '        strLoca += Str
            '    Next
            '    arrHeader.Add("Cost Center : " + strLoca)
            'End If
            'If rbtnGroupSelect.IsChecked Then
            '    strLoca = ""
            '    For Each Str As String In cbgGroup.CheckedDisplayMember
            '        If clsCommon.myLen(strLoca) > 0 Then
            '            strLoca += ", "
            '        End If
            '        strLoca += Str
            '    Next
            '    arrHeader.Add("Group : " + strLoca)
            'End If
            'If rbtnCategorySelect.IsChecked Then
            '    strLoca = ""
            '    For Each Str As String In cbgCategory.CheckedDisplayMember
            '        If clsCommon.myLen(strLoca) > 0 Then
            '            strLoca += ", "
            '        End If
            '        strLoca += Str
            '    Next
            '    arrHeader.Add("Category : " + strLoca)
            'End If
            If exporter = EnumExportTo.Excel Then
                transportSql.QuickExportToExcel(gv2, "", Me.Text, , arrHeader)
                'clsCommon.MyExportToExcelGrid("Asset Depreciation", gv2, arrHeader, Me.Text)
            Else
                clsCommon.MyExportToPDF("Asset Depreciation", gv2, arrHeader, Me.Text, True)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnReverse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReverse.Click
        Try
            If gv2.RowCount > 0 Then
                If clsCommon.MyMessageBoxShow(Me, "Do you want to Reverse the current Document" + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                    '' reason for reverse
                    Dim Reason As String = ""
                    Dim frm As New FrmFreeTxtBox1
                    frm.Text = "Remarks for Reverse"
                    frm.ShowDialog()
                    If clsCommon.myLen(frm.strRmks) <= 0 Then
                        Exit Sub
                    Else
                        Reason = frm.strRmks
                    End If

                    Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
                    Try
                        For ii As Integer = 0 To gv2.RowCount - 1
                            clsAssetDepreciation.RevereAndDelete(Form_ID, "Reverse And Recreate", clsCommon.myCstr(gv2.Rows(ii).Cells("Document_Code").Value), trans)
                        Next
                        trans.Commit()
                        clsCommon.MyMessageBoxShow(Me, "Task done Successfully", Me.Text)
                        LoadSavedData()
                    Catch ex As Exception
                        trans.Rollback()
                        Throw New Exception(ex.Message)
                    End Try
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub gv1_CellFormatting(sender As Object, e As CellFormattingEventArgs) Handles gv1.CellFormatting
        Try
            If e.Column.Index >= 0 Then
                If e.Column Is gv1.Columns(colDepAmount) Then
                    gv1.CurrentRow.Cells(colDepAmount).ReadOnly = Not clsCommon.myCBool(gv1.CurrentRow.Cells(colManualDep).Value)
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub RadButton1_Click(sender As Object, e As EventArgs) Handles RadButton1.Click
        Try
            LoadSavedData()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub chkAutoProcess_CheckedChanged(sender As Object, e As EventArgs) Handles chkAutoProcess.CheckedChanged
        btnProcess.Visible = Not chkAutoProcess.Checked
    End Sub

    Private Sub txtAsset__My_Click(sender As Object, e As EventArgs) Handles txtAsset._My_Click
        Dim qry As String = " select Asset_Code,Asset_Name,Asset_Specification,convert(varchar, TSPL_ACQUISITION_DETAIL.Start_Date,103) as [Use Date], TSPL_ACQUISITION_HEAD.Acquisition_Code, convert (varchar, TSPL_ACQUISITION_HEAD.Acquisition_Date,103) as Acquisition_Date from TSPL_ACQUISITION_DETAIL left outer join TSPL_ACQUISITION_HEAD on TSPL_ACQUISITION_HEAD.Acquisition_Code=TSPL_ACQUISITION_DETAIL.Acquisition_Code where TSPL_ACQUISITION_HEAD.Status=1 and isnull(TSPL_ACQUISITION_DETAIL.Asset_Merged,0)<>1 " ''and  not exists (select 1 from TSPL_ASSET_SCRAP_DETAIL where Asset_Code=TSPL_ACQUISITION_DETAIL.Asset_Code)
        txtAsset.arrValueMember = clsCommon.ShowMultipleSelectForm("AssDep@ca", qry, "Asset_Code", "Asset_Name", txtAsset.arrValueMember, txtAsset.arrDispalyMember)
    End Sub

    Private Sub txtCostCenter__My_Click(sender As Object, e As EventArgs) Handles txtCostCenter._My_Click
        Dim qry As String = "select CostCenter_Code,CostCenter_Name from TSPL_FA_COST_CENTER_MASTER"
        txtCostCenter.arrValueMember = clsCommon.ShowMultipleSelectForm("CosCen@ca", qry, "CostCenter_Code", "CostCenter_Name", txtCostCenter.arrValueMember, txtCostCenter.arrDispalyMember)

    End Sub

    Private Sub txtGroup__My_Click(sender As Object, e As EventArgs) Handles txtGroup._My_Click
        Dim qry As String = "select Group_Code,Description from TSPL_ASSET_GROUP"
        txtGroup.arrValueMember = clsCommon.ShowMultipleSelectForm("Group@ca", qry, "Group_Code", "Description", txtGroup.arrValueMember, txtGroup.arrDispalyMember)

    End Sub

    Private Sub txtLocation__My_Click(sender As Object, e As EventArgs) Handles txtLocation._My_Click
        Dim qry As String = "select Location_Code,Location_Desc from TSPL_LOCATION_MASTER where Location_Type='Physical'"
        txtLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("Loc@ca", qry, "Location_Code", "Location_Desc", txtLocation.arrValueMember, txtLocation.arrDispalyMember)

    End Sub

    Private Sub txtCategory__My_Click(sender As Object, e As EventArgs) Handles txtCategory._My_Click
        Dim qry As String = " select Category_Code,Description from TSPL_ASSET_CATEGORY"
        txtCategory.arrValueMember = clsCommon.ShowMultipleSelectForm("Cate@ca", qry, "Category_Code", "Description", txtCategory.arrValueMember, txtCategory.arrDispalyMember)
    End Sub
End Class
