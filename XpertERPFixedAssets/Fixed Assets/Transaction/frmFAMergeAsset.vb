Imports common
Imports System.IO
Imports Telerik.WinControls.UI
Imports XpertERPEngine
Imports Telerik.WinControls

Public Class frmFAMergeAsset
    Inherits FrmMainTranScreen
#Region "Variables"
    Dim i As Integer
    Private isCellValueChangedOpen As Boolean = False
    Private IsFormLoad As Boolean = False
    Private isNewEntry As Boolean = False
    Private isInsideLoadData As Boolean = False

    Const colLastAmount_AfterDep As String = "colLastAmount_AfterDep"
    Const colLineNo As String = "COLLNO"
    Const colAssetSpecificaion As String = "colAssetSpecificaion"
    Const colDepTaxRate As String = "colDepTaxRate"
    Const colBookDepType As String = "colBookDepType"
    Const colTaxDepType As String = "colTaxDepType"
    Const colAssetID As String = "colAssetID"
    Const colAssetName As String = "colAssetName"
    Const colTemplete As String = "colTemplete"
    Const colTempleteName As String = "colTempleteName"
    Const colAcquisitionDate As String = "colAcquisitionDate"
    Const colDepMethod As String = "colDepMethod"
    Const colDepMethodName As String = "colDepMethodName"
    Const colDepMethodTax As String = "colDepMethodTax"
    Const colDepMethodNameTax As String = "colDepMethodNameTax"
    Const colCategoryCode As String = "colCategoryCode"
    Const colCategoryName As String = "colCategoryName"
    Const colGroupCode As String = "colGroupCode"
    Const colGroupName As String = "colGroupName"
    Const colCostCenterCode As String = "colCostCenterCode"
    Const colCostCenterName As String = "colCostCenterName"
    Const colAccountSetCode As String = "colAccountSetCode"
    Const colAccountSetName As String = "colAccountSetName"
    Const colDepPeriodCode As String = "colDepPeriodCode"
    Const colDepPeriodName As String = "colDepPeriodName"
    Const colStartDate As String = "colStartDate"
    Const colDepRate As String = "colDepRate"
    Const ColBookEstimatedLife As String = "ColBookEstimatedLife"
    Const ColBookSourceValue As String = "ColBookSourceValue"
    Const ColBookSourceOriginalValue As String = "ColBookSourceOriginalValue"
    Const ColBookSalvageValue As String = "ColBookSalvageValue"
    Const ColBookSalvageRate As String = "ColBookSalvageRate"
    Const colNetAmt As String = "colNetAmt"

    Dim ButtonToolTip As ToolTip = New ToolTip()
#End Region

    Public Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.FAMergeAcquisitionEntry)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnPost.Visible = MyBase.isPostFlag
        btnDelete.Visible = MyBase.isDeleteFlag
        BtnPrint.Visible = MyBase.isPrintFlag
    End Sub

    Private Sub LoadType()
        ddlAcqType.DataSource = Nothing
        Dim qry As String = "select 'Direct' as Code,'Direct' as Name union all select 'Asset' as Code,'Asset' as Name union all select 'Assembled' as Code,'Assembled' as Name union all select 'Merge' as Code,'Merge' as Name"
        ddlAcqType.DataSource = clsDBFuncationality.GetDataTable(qry)
        ddlAcqType.ValueMember = "Code"
        ddlAcqType.DisplayMember = "Name"
    End Sub

    Private Sub FrmAPInvoiceEntry_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        RadPageView1.SelectedPage = RadPageViewPage1
        LoadType()
        LoadBlankGrid()

        IsFormLoad = True

        BlankAllControls()

        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for save record")
        ButtonToolTip.SetToolTip(btnPost, "Press Alt+P post record")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D delete record")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C close the window")
        ButtonToolTip.SetToolTip(btnAddNew, "Press Alt+N adding new record")

        IsFormLoad = False
        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
    End Sub

    Sub BlankAllControls()
        isNewEntry = True

        RadGroupBox1.Enabled = True
        rdbBook.IsChecked = True
        rdbTax.IsChecked = False

        txtDocNo.Value = ""
        txtDate.Value = clsCommon.GETSERVERDATE()
        dtpStartDate.Text = clsCommon.GETSERVERDATE()
        dtpStartDate.Checked = False
        ddlAcqType.SelectedValue = "Merge"
        txtAssetCode.Text = ""
        txtAssetDesc.Text = ""
        txtSpecification.Text = ""
        txtDesc.Text = ""
        txtRemarks.Text = ""
        txtLocation.Value = ""
        lblLocation.Text = ""
        fndTemplateCode.Value = ""
        lblTemplateDesc.Text = ""
        fndTemplateDetail.Value = ""
        fndTemplateDetail.Tag = Nothing
        lblNetAmt.Text = ""
        UsLock1.Status = ERPTransactionStatus.Pending

        gv1.Rows.Clear()
        gv1.Rows.AddNew()

        ddlAcqType.Enabled = True
        txtLocation.Enabled = True
        fndTemplateCode.Enabled = True

        btnSave.Text = "Save"
        btnSave.Enabled = True
        btnDelete.Enabled = False
        btnPost.Enabled = False
        btnChangeDepDetail.Enabled = False

        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

    Sub LoadBlankGrid()
        gv1.Rows.Clear()
        gv1.Columns.Clear()

        Dim repoLineNo As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoLineNo = New GridViewDecimalColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "S. No."
        repoLineNo.Name = colLineNo
        repoLineNo.Width = 50
        repoLineNo.ReadOnly = True
        repoLineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoLineNo.WrapText = True
        gv1.MasterTemplate.Columns.Add(repoLineNo)

        Dim repoAssetCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoAssetCode.FormatString = ""
        repoAssetCode.HeaderText = "Asset Code"
        repoAssetCode.Name = colAssetID
        repoAssetCode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoAssetCode.Width = 100
        ' repoAssetCode.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoAssetCode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoAssetCode.WrapText = True
        gv1.MasterTemplate.Columns.Add(repoAssetCode)

        Dim repoAssetName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoAssetName.FormatString = ""
        repoAssetName.HeaderText = "Asset Description"
        repoAssetName.Name = colAssetName
        repoAssetName.Width = 150
        repoAssetName.ReadOnly = True
        repoAssetName.WrapText = True
        gv1.MasterTemplate.Columns.Add(repoAssetName)

        Dim repoAssetSpecification As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoAssetSpecification.FormatString = ""
        repoAssetSpecification.HeaderText = "Asset Specification"
        repoAssetSpecification.Name = colAssetSpecificaion
        repoAssetSpecification.Width = 150
        repoAssetSpecification.ReadOnly = True
        repoAssetSpecification.WrapText = True
        gv1.MasterTemplate.Columns.Add(repoAssetSpecification)

        Dim repoTemplate As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTemplate.FormatString = ""
        repoTemplate.HeaderText = "Template Code"
        repoTemplate.Name = colTemplete
        repoTemplate.Width = 100
        repoTemplate.ReadOnly = True
        repoTemplate.IsVisible = True
        repoTemplate.WrapText = True
        gv1.MasterTemplate.Columns.Add(repoTemplate)

        Dim repoTempleteName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTempleteName.FormatString = ""
        repoTempleteName.HeaderText = "Templete"
        repoTempleteName.Name = colTempleteName
        repoTempleteName.Width = 150
        repoTempleteName.ReadOnly = True
        repoTempleteName.IsVisible = True
        repoTempleteName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        repoTempleteName.WrapText = True
        gv1.MasterTemplate.Columns.Add(repoTempleteName)

        Dim repobookDepType As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repobookDepType.FormatString = ""
        repobookDepType.HeaderText = "Book Dep. Type"
        repobookDepType.Name = colBookDepType
        repobookDepType.TextImageRelation = TextImageRelation.TextBeforeImage
        repobookDepType.Width = 100
        repobookDepType.ReadOnly = True
        repobookDepType.IsVisible = False
        repobookDepType.WrapText = True
        gv1.MasterTemplate.Columns.Add(repobookDepType)

        Dim repoTaxDepType As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTaxDepType.FormatString = ""
        repoTaxDepType.HeaderText = "Tax Dep.Type"
        repoTaxDepType.Name = colTaxDepType
        repoTaxDepType.TextImageRelation = TextImageRelation.TextBeforeImage
        repoTaxDepType.Width = 100
        repoTaxDepType.ReadOnly = True
        repoTaxDepType.IsVisible = False
        repoTaxDepType.WrapText = True
        gv1.MasterTemplate.Columns.Add(repoTaxDepType)

        Dim repoCategoryCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCategoryCode.FormatString = ""
        repoCategoryCode.HeaderText = "Category Code"
        repoCategoryCode.Name = colCategoryCode
        repoCategoryCode.Width = 100
        repoCategoryCode.ReadOnly = True
        repoCategoryCode.WrapText = True
        repoCategoryCode.IsVisible = True
        gv1.MasterTemplate.Columns.Add(repoCategoryCode)

        Dim repoCategoryName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCategoryName.FormatString = ""
        repoCategoryName.HeaderText = "Category"
        repoCategoryName.Name = colCategoryName
        repoCategoryName.Width = 150
        repoCategoryName.ReadOnly = True
        repoCategoryName.IsVisible = True
        repoCategoryName.WrapText = True
        repoCategoryName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.MasterTemplate.Columns.Add(repoCategoryName)

        Dim repoGroupCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoGroupCode.FormatString = ""
        repoGroupCode.HeaderText = "Group Code"
        repoGroupCode.Name = colGroupCode
        repoGroupCode.Width = 100
        repoGroupCode.ReadOnly = True
        repoGroupCode.IsVisible = True
        repoGroupCode.WrapText = True
        gv1.MasterTemplate.Columns.Add(repoGroupCode)

        Dim repoGroup As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoGroup.FormatString = ""
        repoGroup.HeaderText = "Group"
        repoGroup.Name = colGroupName
        repoGroup.Width = 150
        repoGroup.ReadOnly = True
        repoGroup.IsVisible = True
        repoGroup.WrapText = True
        repoGroup.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.MasterTemplate.Columns.Add(repoGroup)

        Dim repoCostCenterCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCostCenterCode.FormatString = ""
        repoCostCenterCode.HeaderText = "Cost Center Code"
        repoCostCenterCode.Name = colCostCenterCode
        repoCostCenterCode.Width = 100
        repoCostCenterCode.ReadOnly = True
        repoCostCenterCode.IsVisible = False
        repoCostCenterCode.WrapText = True
        gv1.MasterTemplate.Columns.Add(repoCostCenterCode)

        Dim repoCostCenterName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCostCenterName.FormatString = ""
        repoCostCenterName.HeaderText = "Cost Center"
        repoCostCenterName.Name = colCostCenterName
        repoCostCenterName.Width = 150
        repoCostCenterName.ReadOnly = True
        repoCostCenterName.IsVisible = False
        repoCostCenterName.WrapText = True
        repoCostCenterName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.MasterTemplate.Columns.Add(repoCostCenterName)

        Dim repoAccountSetCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoAccountSetCode.FormatString = ""
        repoAccountSetCode.HeaderText = "Account Set Code"
        repoAccountSetCode.Name = colAccountSetCode
        repoAccountSetCode.Width = 100
        repoAccountSetCode.ReadOnly = True
        repoAccountSetCode.IsVisible = True
        repoAccountSetCode.WrapText = True
        gv1.MasterTemplate.Columns.Add(repoAccountSetCode)

        Dim repoAccountSetName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoAccountSetName.FormatString = ""
        repoAccountSetName.HeaderText = "Account Set"
        repoAccountSetName.Name = colAccountSetName
        repoAccountSetName.Width = 150
        repoAccountSetName.ReadOnly = True
        repoAccountSetName.IsVisible = True
        repoAccountSetName.WrapText = True
        repoAccountSetName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.MasterTemplate.Columns.Add(repoAccountSetName)

        Dim repoAcquisitionDate As GridViewDateTimeColumn = New GridViewDateTimeColumn()
        repoAcquisitionDate.Format = DateTimePickerFormat.Custom
        repoAcquisitionDate.CustomFormat = "dd/MM/yyyy"
        repoAcquisitionDate.HeaderText = "Acquisition Date"
        repoAcquisitionDate.FormatString = "{0:d}"
        repoAcquisitionDate.Name = colAcquisitionDate
        repoAcquisitionDate.WrapText = True
        repoAcquisitionDate.ReadOnly = True
        repoAcquisitionDate.IsVisible = True
        repoAcquisitionDate.Width = 80
        gv1.MasterTemplate.Columns.Add(repoAcquisitionDate)

        Dim repoDepCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoDepCode.FormatString = ""
        repoDepCode.HeaderText = "Depreciation Method Code"
        repoDepCode.Name = colDepMethod
        repoDepCode.Width = 100
        repoDepCode.ReadOnly = True
        repoDepCode.IsVisible = True
        repoDepCode.WrapText = True
        gv1.MasterTemplate.Columns.Add(repoDepCode)

        Dim repoDepName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoDepName.FormatString = ""
        repoDepName.HeaderText = "Depreciation Method "
        repoDepName.Name = colDepMethodName
        repoDepName.Width = 150
        repoDepName.ReadOnly = True
        repoDepName.IsVisible = True
        repoDepName.WrapText = True
        repoDepName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.MasterTemplate.Columns.Add(repoDepName)

        Dim repoDepCodeTax As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoDepCodeTax.FormatString = ""
        repoDepCodeTax.HeaderText = "Depreciation Method Tax Code"
        repoDepCodeTax.Name = colDepMethodTax
        repoDepCodeTax.Width = 100
        repoDepCodeTax.ReadOnly = True
        repoDepCodeTax.IsVisible = True
        repoDepCodeTax.WrapText = True
        gv1.MasterTemplate.Columns.Add(repoDepCodeTax)

        Dim repoDepNametax As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoDepNametax.FormatString = ""
        repoDepNametax.HeaderText = "Depreciation Method Tax"
        repoDepNametax.Name = colDepMethodNameTax
        repoDepNametax.Width = 150
        repoDepNametax.ReadOnly = True
        repoDepNametax.IsVisible = True
        repoDepNametax.WrapText = True
        repoDepNametax.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.MasterTemplate.Columns.Add(repoDepNametax)

        Dim repoDepPeriodCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoDepPeriodCode.FormatString = ""
        repoDepPeriodCode.HeaderText = "Depreciation Period Code"
        repoDepPeriodCode.Name = colDepPeriodCode
        repoDepPeriodCode.Width = 100
        repoDepPeriodCode.ReadOnly = True
        repoDepPeriodCode.IsVisible = True
        repoDepPeriodCode.WrapText = True
        gv1.MasterTemplate.Columns.Add(repoDepPeriodCode)

        Dim repoDepPeriodName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoDepPeriodName.FormatString = ""
        repoDepPeriodName.HeaderText = "Depreciation Method "
        repoDepPeriodName.Name = colDepPeriodName
        repoDepPeriodName.Width = 150
        repoDepPeriodName.ReadOnly = True
        repoDepPeriodName.WrapText = True
        repoDepPeriodName.IsVisible = True
        repoDepPeriodName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.MasterTemplate.Columns.Add(repoDepPeriodName)


        Dim repoStartDate As GridViewDateTimeColumn = New GridViewDateTimeColumn()
        repoStartDate.Format = DateTimePickerFormat.Custom
        repoStartDate.CustomFormat = "dd/MM/yyyy"
        repoStartDate.HeaderText = "Start Date"
        repoStartDate.FormatString = "{0:d}"
        repoStartDate.Name = colStartDate
        repoStartDate.WrapText = True
        repoStartDate.ReadOnly = True
        repoStartDate.IsVisible = True
        repoStartDate.WrapText = True
        repoStartDate.Width = 80
        gv1.MasterTemplate.Columns.Add(repoStartDate)

        Dim repoDepTaxRate As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoDepTaxRate.FormatString = ""
        repoDepTaxRate.HeaderText = "Depreciation Tax Rate"
        repoDepTaxRate.Name = colDepTaxRate
        repoDepTaxRate.ReadOnly = True
        repoDepTaxRate.IsVisible = True
        repoDepTaxRate.Width = 80
        repoDepTaxRate.WrapText = True
        gv1.MasterTemplate.Columns.Add(repoDepTaxRate)

        Dim repoDepRate As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoDepRate.FormatString = ""
        repoDepRate.HeaderText = "Depreciation Rate"
        repoDepRate.Name = colDepRate
        repoDepRate.ReadOnly = True
        repoDepRate.IsVisible = True
        repoDepPeriodName.Width = 80
        repoDepPeriodName.WrapText = True
        gv1.MasterTemplate.Columns.Add(repoDepRate)

        Dim repoBookEstimatedLife As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoBookEstimatedLife.FormatString = ""
        repoBookEstimatedLife.HeaderText = "Book Estimated Life"
        repoBookEstimatedLife.Name = ColBookEstimatedLife
        repoBookEstimatedLife.ReadOnly = True
        repoBookEstimatedLife.IsVisible = False
        repoDepPeriodName.Width = 80
        repoDepPeriodName.WrapText = True
        gv1.MasterTemplate.Columns.Add(repoBookEstimatedLife)

        Dim repoBookSourceValue As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoBookSourceValue.FormatString = ""
        repoBookSourceValue.HeaderText = "Book Source Value"
        repoBookSourceValue.Name = ColBookSourceValue
        repoBookSourceValue.ReadOnly = True
        repoBookSourceValue.IsVisible = True
        repoBookSourceValue.WrapText = True
        repoDepPeriodName.Width = 80
        gv1.MasterTemplate.Columns.Add(repoBookSourceValue)


        Dim repoBookSourceOriginalValue As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoBookSourceOriginalValue.FormatString = ""
        repoBookSourceOriginalValue.HeaderText = "Book source Original Value"
        repoBookSourceOriginalValue.Name = ColBookSourceOriginalValue
        repoBookSourceOriginalValue.ReadOnly = True
        repoBookSourceOriginalValue.IsVisible = True
        repoBookSourceOriginalValue.Width = 80
        repoBookSourceOriginalValue.WrapText = True
        gv1.MasterTemplate.Columns.Add(repoBookSourceOriginalValue)


        Dim repoBookSalvageRate As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoBookSalvageRate.FormatString = ""
        repoBookSalvageRate.HeaderText = "Salvage %"
        repoBookSalvageRate.Name = ColBookSalvageRate
        repoBookSalvageRate.ReadOnly = True
        repoBookSalvageRate.IsVisible = True
        repoBookSalvageRate.WrapText = True
        repoBookSalvageRate.Width = 80
        gv1.MasterTemplate.Columns.Add(repoBookSalvageRate)

        Dim repoBookSalvageValue As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoBookSalvageValue.FormatString = ""
        repoBookSalvageValue.HeaderText = "Book Salvage Value"
        repoBookSalvageValue.Name = ColBookSalvageValue
        repoBookSalvageValue.ReadOnly = True
        repoBookSalvageValue.IsVisible = True
        repoBookSalvageValue.Width = 80
        repoBookSalvageValue.WrapText = True
        gv1.MasterTemplate.Columns.Add(repoBookSalvageValue)

        Dim repoNetAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoNetAmt = New GridViewDecimalColumn()
        repoNetAmt.FormatString = ""
        repoNetAmt.HeaderText = "Net Amount"
        repoNetAmt.Name = colNetAmt
        repoNetAmt.WrapText = True
        repoNetAmt.Width = 80
        repoNetAmt.WrapText = True
        repoNetAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoNetAmt.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoNetAmt)

        repoNetAmt = New GridViewDecimalColumn()
        repoNetAmt.FormatString = ""
        repoNetAmt.HeaderText = "After Dep. Net Amount"
        repoNetAmt.Name = colLastAmount_AfterDep
        repoNetAmt.WrapText = True
        repoNetAmt.Width = 80
        repoNetAmt.WrapText = True
        repoNetAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoNetAmt.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoNetAmt)

        gv1.AllowDeleteRow = True
        gv1.AllowAddNewRow = False
        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = True
        gv1.AllowRowReorder = True
        gv1.EnableSorting = False
        gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        gv1.TableElement.TableHeaderHeight = 40
        ReStoreGridLayout()
    End Sub

    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(MyBase.Form_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(Form_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
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

    Private Sub gv1_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellValueChanged
        Try
            If (Not isInsideLoadData) Then
                If Not isCellValueChangedOpen Then
                    isCellValueChangedOpen = True
                    If e.Column Is gv1.Columns(colAssetID) Then
                        OpenAssetList(False)
                    End If
                    isCellValueChangedOpen = False
                End If
            End If
        Catch ex As Exception
            isCellValueChangedOpen = False
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

#Region "Fill Asset Detail in Grid"
    Private Sub OpenAssetList(ByVal isButtonClick As Boolean)
        Dim qry As String = "select TSPL_ACQUISITION_head.Acquisition_Code as [Doc Code],TSPL_ACQUISITION_DETAIL.Asset_Code as [Code],TSPL_ACQUISITION_DETAIL.Asset_Name as [Asset Name],TSPL_ACQUISITION_DETAIL.Book_Source_Original_value as [Booking Value],TSPL_ACQUISITION_DETAIL.Book_Salvage_Rate as [Salvage Rate],TSPL_ACQUISITION_DETAIL.Book_Salvage_Value as [Salvage Value] from TSPL_ACQUISITION_head left outer join TSPL_ACQUISITION_DETAIL on TSPL_ACQUISITION_head.Acquisition_Code=TSPL_ACQUISITION_DETAIL.Acquisition_Code "
        gv1.CurrentRow.Cells(colAssetID).Value = clsCommon.myCstr(clsCommon.ShowSelectForm("MRGASTFND", qry, "Code", " TSPL_ACQUISITION_head.Status=1 and not exists (select 1 from TSPL_ACQUISITION_ASSET_MERGE_DETAIL where TSPL_ACQUISITION_ASSET_MERGE_DETAIL.old_asset_code=TSPL_ACQUISITION_DETAIL.Asset_Code) and  not exists (select 1 from TSPL_ASSET_SCRAP_DETAIL where TSPL_ASSET_SCRAP_DETAIL.Asset_Code=TSPL_ACQUISITION_DETAIL.Asset_Code) and 1 <> isnull((select case when Trans_Type='issue' then 1 else 0 end from TSPL_ASSET_ISSUE_RETURN where TSPL_ASSET_ISSUE_RETURN.Asset_id=TSPL_ACQUISITION_DETAIL.Asset_Code and TSPL_ASSET_ISSUE_RETURN.trans_date=(select max(trans_date) as xdate from TSPL_ASSET_ISSUE_RETURN where TSPL_ASSET_ISSUE_RETURN.Asset_id=TSPL_ACQUISITION_DETAIL.Asset_Code)),0) ", clsCommon.myCstr(gv1.CurrentRow.Cells(colAssetID).Value), "", isButtonClick)) 'and TSPL_ACQUISITION_head.Loc_Code='" + txtLocation.Value + "' and TSPL_ACQUISITION_head.Templete_Code='" + fndTemplateCode.Value + "'
        If clsCommon.myLen(gv1.CurrentRow.Cells(colAssetID).Value) > 0 Then
            FillGridData(gv1.CurrentRow.Index)
            ''==========for getting last net amt after dep. amount=================
            qry = clsAcquisitionDetail.GetAssetQuery()
            qry += " and  ACQ.Loc_Code= '" + txtLocation.Value + "' " + Environment.NewLine
            qry += " and  ACQD.Asset_Code ='" + clsCommon.myCstr(gv1.CurrentRow.Cells(colAssetID).Value) + "' " + Environment.NewLine

            If rdbBook.IsChecked Then
                qry = "select top 1 Book.Asset_Value from (" & qry & ") as Book "
            Else
                qry = "select top 1 Book.Asset_Value_Tax from (" & qry & ") as Book "
            End If
            gv1.CurrentRow.Cells(colLastAmount_AfterDep).Value = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))

            UpdateAllTotals()
        Else
            BlankCurrenRow(gv1.CurrentRow.Index)
        End If
    End Sub

    Private Sub FillGridData(ByVal intRow As Integer)
        Dim dt As New DataTable()
        Try
            Dim qry As String = "SELECT  TSPL_ACQUISITION_DETAIL.*,TSPL_ASSET_CATEGORY.Description as Category_Name,TSPL_ASSET_GROUP.Description as Group_Code_Name,TSPL_Dep_AccountSet.AcSet_Desc as AcSet_Code_Name,TSPL_FA_COST_CENTER_MASTER.CostCenter_Name as CostCenter_Name,TSPL_DEPRECIATION_METHOD.Description as Dep_Method_Name,TSPL_DEPRECIATION_PERIODS.period_Desc as Dep_Period_Name,TSPL_FA_TEMPLATE_MASTER.Template_Name as Templete_Name,TSPL_ITEM_MASTER.Item_Desc as Item_Name,DepMethodTax.Description as  Dep_Method_Tax_Name from TSPL_ACQUISITION_DETAIL " + Environment.NewLine
            qry += " left outer join TSPL_ASSET_CATEGORY on TSPL_ASSET_CATEGORY.Category_Code=TSPL_ACQUISITION_DETAIL.Category_code" + Environment.NewLine
            qry += " left outer join TSPL_ASSET_GROUP on TSPL_ASSET_GROUP.Group_Code=TSPL_ACQUISITION_DETAIL.Group_Code" + Environment.NewLine
            qry += " left outer join TSPL_Dep_AccountSet on TSPL_Dep_AccountSet.AcSet_Code=TSPL_ACQUISITION_DETAIL.AcSet_Code" + Environment.NewLine
            qry += " left outer join TSPL_FA_COST_CENTER_MASTER on TSPL_FA_COST_CENTER_MASTER.CostCenter_Code=TSPL_ACQUISITION_DETAIL.CostCenter_Code" + Environment.NewLine
            qry += " left outer join TSPL_DEPRECIATION_METHOD on TSPL_DEPRECIATION_METHOD.Code=TSPL_ACQUISITION_DETAIL.Dep_Method_Code" + Environment.NewLine
            qry += " left outer join TSPL_DEPRECIATION_METHOD as DepMethodTax on DepMethodTax.Code=TSPL_ACQUISITION_DETAIL.Dep_Method_Tax_Code" + Environment.NewLine
            qry += " left outer join TSPL_DEPRECIATION_PERIODS on TSPL_DEPRECIATION_PERIODS.period_Code=TSPL_ACQUISITION_DETAIL.Dep_Period_Code" + Environment.NewLine
            qry += " left outer join TSPL_FA_TEMPLATE_MASTER on TSPL_FA_TEMPLATE_MASTER.Template_Code=TSPL_ACQUISITION_DETAIL.Templete_Code" + Environment.NewLine
            qry += " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_ACQUISITION_DETAIL.Item_Code" + Environment.NewLine
            qry += " where TSPL_ACQUISITION_DETAIL.Asset_Code='" + clsCommon.myCstr(gv1.Rows(intRow).Cells(colAssetID).Value) + "' ORDER BY TSPL_ACQUISITION_DETAIL.SNo"
            dt = clsDBFuncationality.GetDataTable(qry)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                gv1.Rows(intRow).Cells(colAssetName).Value = clsCommon.myCstr(dt.Rows(0)("Asset_Name"))
                gv1.Rows(intRow).Cells(colAssetSpecificaion).Value = clsCommon.myCstr(dt.Rows(0)("asset_specification"))
                gv1.Rows(intRow).Cells(colDepMethod).Value = clsCommon.myCstr(dt.Rows(0)("dep_method_code"))
                gv1.Rows(intRow).Cells(colDepMethodName).Value = clsCommon.myCstr(dt.Rows(0)("Dep_Method_Name"))
                gv1.Rows(intRow).Cells(colDepMethodNameTax).Value = clsCommon.myCstr(dt.Rows(0)("Dep_Method_Tax_Name"))
                gv1.Rows(intRow).Cells(colDepMethodTax).Value = clsCommon.myCstr(dt.Rows(0)("Dep_Method_Tax_Code"))
                gv1.Rows(intRow).Cells(colDepPeriodCode).Value = clsCommon.myCstr(dt.Rows(0)("Dep_Period_Code"))
                gv1.Rows(intRow).Cells(colDepPeriodName).Value = clsCommon.myCstr(dt.Rows(0)("Dep_Period_Name"))
                gv1.Rows(intRow).Cells(colDepRate).Value = clsCommon.myCdbl(dt.Rows(0)("Dep_Rate"))
                gv1.Rows(intRow).Cells(colDepTaxRate).Value = clsCommon.myCdbl(dt.Rows(0)("Dep_Tax_Rate"))
                gv1.Rows(intRow).Cells(colBookDepType).Value = IIf(clsCommon.myCstr(dt.Rows(0)("Book_Dep_Type")) = "F", "Formula", "Manual")
                gv1.Rows(intRow).Cells(ColBookEstimatedLife).Value = clsCommon.myCdbl(dt.Rows(0)("Book_Estimated_Life"))
                gv1.Rows(intRow).Cells(ColBookSalvageRate).Value = clsCommon.myCdbl(dt.Rows(0)("Book_Salvage_Rate"))
                gv1.Rows(intRow).Cells(ColBookSalvageValue).Value = clsCommon.myCdbl(dt.Rows(0)("Book_Salvage_Value"))
                gv1.Rows(intRow).Cells(ColBookSourceOriginalValue).Value = clsCommon.myCdbl(dt.Rows(0)("Book_Source_Original_value"))
                gv1.Rows(intRow).Cells(ColBookSourceValue).Value = clsCommon.myCdbl(dt.Rows(0)("Book_Source_value"))
                gv1.Rows(intRow).Cells(colTemplete).Value = clsCommon.myCstr(dt.Rows(0)("Templete_Code"))
                gv1.Rows(intRow).Cells(colTempleteName).Value = clsCommon.myCstr(dt.Rows(0)("Templete_Name"))
                gv1.Rows(intRow).Cells(colTaxDepType).Value = IIf(clsCommon.myCstr(dt.Rows(0)("Tax_Dep_Type")) = "F", "Formula", "Manual")
                gv1.Rows(intRow).Cells(colCategoryCode).Value = clsCommon.myCstr(dt.Rows(0)("Category_code"))
                gv1.Rows(intRow).Cells(colCategoryName).Value = clsCommon.myCstr(dt.Rows(0)("Category_Name"))
                gv1.Rows(intRow).Cells(colCostCenterCode).Value = clsCommon.myCstr(dt.Rows(0)("CostCenter_Code"))
                gv1.Rows(intRow).Cells(colCostCenterName).Value = clsCommon.myCstr(dt.Rows(0)("CostCenter_Name"))
                gv1.Rows(intRow).Cells(colGroupCode).Value = clsCommon.myCstr(dt.Rows(0)("Group_Code"))
                gv1.Rows(intRow).Cells(colGroupName).Value = clsCommon.myCstr(dt.Rows(0)("Group_Code_Name"))
                gv1.Rows(intRow).Cells(colAccountSetCode).Value = clsCommon.myCstr(dt.Rows(0)("AcSet_Code"))
                gv1.Rows(intRow).Cells(colAccountSetName).Value = clsCommon.myCstr(dt.Rows(0)("AcSet_Code_Name"))
                gv1.Rows(intRow).Cells(colAcquisitionDate).Value = clsCommon.myCDate(dt.Rows(0)("Acqusition_Date"))
                gv1.Rows(intRow).Cells(colStartDate).Value = clsCommon.myCDate(dt.Rows(0)("Start_Date"))
                gv1.Rows(intRow).Cells(colNetAmt).Value = clsCommon.myCdbl(dt.Rows(0)("Item_Net_Amt"))
            Else
                BlankCurrenRow(intRow)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            dt = Nothing
        End Try
    End Sub

    Private Sub BlankCurrenRow(ByVal intRow As Integer)
        Try
            gv1.Rows(intRow).Cells(colLastAmount_AfterDep).Value = Nothing
            gv1.Rows(intRow).Cells(colAssetID).Value = Nothing
            gv1.Rows(intRow).Cells(colAssetName).Value = Nothing
            gv1.Rows(intRow).Cells(colAssetSpecificaion).Value = Nothing
            gv1.Rows(intRow).Cells(colDepMethod).Value = Nothing
            gv1.Rows(intRow).Cells(colDepMethodName).Value = Nothing
            gv1.Rows(intRow).Cells(colDepMethodNameTax).Value = Nothing
            gv1.Rows(intRow).Cells(colDepMethodTax).Value = Nothing
            gv1.Rows(intRow).Cells(colDepPeriodCode).Value = Nothing
            gv1.Rows(intRow).Cells(colDepPeriodName).Value = Nothing
            gv1.Rows(intRow).Cells(colDepRate).Value = Nothing
            gv1.Rows(intRow).Cells(colDepTaxRate).Value = Nothing
            gv1.Rows(intRow).Cells(colBookDepType).Value = Nothing
            gv1.Rows(intRow).Cells(ColBookEstimatedLife).Value = Nothing
            gv1.Rows(intRow).Cells(ColBookSalvageRate).Value = Nothing
            gv1.Rows(intRow).Cells(ColBookSalvageValue).Value = Nothing
            gv1.Rows(intRow).Cells(ColBookSourceOriginalValue).Value = Nothing
            gv1.Rows(intRow).Cells(ColBookSourceValue).Value = Nothing
            gv1.Rows(intRow).Cells(colTemplete).Value = Nothing
            gv1.Rows(intRow).Cells(colTempleteName).Value = Nothing
            gv1.Rows(intRow).Cells(colTaxDepType).Value = Nothing
            gv1.Rows(intRow).Cells(colCategoryCode).Value = Nothing
            gv1.Rows(intRow).Cells(colCategoryName).Value = Nothing
            gv1.Rows(intRow).Cells(colCostCenterCode).Value = Nothing
            gv1.Rows(intRow).Cells(colCostCenterName).Value = Nothing
            gv1.Rows(intRow).Cells(colGroupCode).Value = Nothing
            gv1.Rows(intRow).Cells(colGroupName).Value = Nothing
            gv1.Rows(intRow).Cells(colAccountSetCode).Value = Nothing
            gv1.Rows(intRow).Cells(colAccountSetName).Value = Nothing
            gv1.Rows(intRow).Cells(colAcquisitionDate).Value = Nothing
            gv1.Rows(intRow).Cells(colStartDate).Value = Nothing
            gv1.Rows(intRow).Cells(colNetAmt).Value = Nothing
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
#End Region

    Private Sub UpdateAllTotals()
        Dim dblTotAmt As Double = 0

        For ii As Integer = 0 To gv1.Rows.Count - 1
            dblTotAmt = dblTotAmt + clsCommon.myCdbl(gv1.Rows(ii).Cells(colLastAmount_AfterDep).Value)
        Next
        lblNetAmt.Text = dblTotAmt
    End Sub

    Private Sub btnAddNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddNew.Click
        BlankAllControls()
    End Sub

    Private Function AllowToSave() As Boolean
        '===============Preeti Gupta==================================
        If AllowFutureDateTransaction(txtDate.Value, Nothing) = False Then
            txtDate.Select()
            Return False
        End If
        '=======================================================
        If clsCommon.myLen(txtAssetDesc.Text) <= 0 Then
            txtAssetDesc.Focus()
            txtAssetDesc.Select()
            Throw New Exception("Please fill asset description.")
        End If

        If dtpStartDate.Checked = False Then
            dtpStartDate.Focus()
            dtpStartDate.Select()
            Throw New Exception("Please select asset start date.")
        End If

        If clsCommon.myLen(txtLocation.Value) <= 0 Then
            txtLocation.Focus()
            txtLocation.Select()
            Throw New Exception("Please select location detail.")
        End If

        If clsCommon.myLen(fndTemplateCode.Value) <= 0 Then
            fndTemplateCode.Focus()
            fndTemplateCode.Select()
            Throw New Exception("Please select template for asset.")
        End If

        UpdateAllTotals()

        Dim arrCount As New ArrayList()
        For Each grow As GridViewRowInfo In gv1.Rows
            grow.Cells(colLineNo).Value = grow.Index + 1
            If clsCommon.myLen(grow.Cells(colAssetID).Value) > 0 AndAlso Not arrCount.Contains(clsCommon.myCstr(grow.Cells(colAssetID).Value)) Then
                arrCount.Add(clsCommon.myCstr(grow.Cells(colAssetID).Value))
            End If
            If clsCommon.myLen(grow.Cells(colAssetID).Value) > 0 Then
                For i As Integer = grow.Index + 1 To gv1.Rows.Count - 1
                    If clsCommon.CompairString(clsCommon.myCstr(grow.Cells(colAssetID).Value), clsCommon.myCstr(gv1.Rows(i).Cells(colAssetID).Value)) = CompairStringResult.Equal Then
                        gv1.CurrentRow = gv1.Rows(i)
                        gv1.CurrentColumn = gv1.Columns(colAssetID)
                        Throw New Exception("Duplicate asset exist at row no. " + clsCommon.myCstr(grow.Index + 1) + " and " + clsCommon.myCstr(i + 1) + "")
                    End If
                Next
            End If
        Next

        If arrCount.Count <= 0 Then
            gv1.CurrentRow = gv1.Rows(0)
            gv1.CurrentColumn = gv1.Columns(colAssetID)
            Throw New Exception("Select atleast 1 asset for merging.")
        End If

        If fndTemplateDetail.Tag Is Nothing OrElse TryCast(fndTemplateDetail.Tag, clsFAMergeDetail) Is Nothing Then
            fndTemplateDetail.Focus()
            fndTemplateDetail.Select()
            Throw New Exception("Please select template detail.")
        End If

        Return True
    End Function

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        SavingData(False)
    End Sub

    Sub SavingData(ByVal ChekBtnPost As Boolean)
        If (SaveData(False, ChekBtnPost)) Then
            If ChekBtnPost = False Then
                common.clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
            End If
        End If
    End Sub

    Private Function SaveData(ByVal isDoAbandomentNo As Boolean, ByVal ChekBtnPost As Boolean) As Boolean
        Dim obj As New clsFAMergeHead()
        Dim objTr As New clsFAMergeDetail()
        Try
            If (AllowToSave()) Then
                obj = New clsFAMergeHead()

                obj.Acquisition_Type = ddlAcqType.Text
                obj.Acquisition_Code = txtDocNo.Value
                obj.Acquisition_Date = txtDate.Value
                obj.SRN_No = Nothing
                obj.Vendor_Code = Nothing
                obj.Vendor_Name = Nothing
                obj.Description = txtDesc.Text
                obj.Vendor_Invoice_No = Nothing
                obj.Remarks = txtRemarks.Text
                obj.On_Hold = False
                obj.Is_Visi_Type = False
                obj.Total_Amt = clsCommon.myCdbl(lblNetAmt.Text)
                obj.Total_Tax_Amt = 0 ' clsCommon.myCdbl(lblTaxAmt.Text)
                obj.Net_Amt = clsCommon.myCdbl(lblNetAmt.Text)
                obj.Loc_Code = txtLocation.Value
                obj.Tax_Group = Nothing
                obj.Templete_Code = fndTemplateCode.Value
                obj.IS_Assemble = False
                obj.Balance_Amt = clsCommon.myCdbl(lblNetAmt.Text)
                obj.Merge_Asset_Code = txtAssetCode.Text
                ''==

                obj.statusnewold = "NEW"

                ''=============================================================================
                obj.Arr = New List(Of clsFAMergeDetail)
                objTr = New clsFAMergeDetail()
                objTr.arrMerged_AssetCode = New List(Of clsFAMergeDetail)
                If fndTemplateDetail.Tag IsNot Nothing AndAlso TryCast(fndTemplateDetail.Tag, clsFAMergeDetail) IsNot Nothing AndAlso clsCommon.myLen(TryCast(fndTemplateDetail.Tag, clsFAMergeDetail).Asset_Name) > 0 Then
                    objTr = TryCast(fndTemplateDetail.Tag, clsFAMergeDetail)
                Else
                    objTr = Nothing
                End If
                For Each grow As GridViewRowInfo In gv1.Rows
                    Dim objN As New clsFAMergeDetail()
                    objN.Asset_Code = clsCommon.myCstr(grow.Cells(colAssetID).Value)
                    objN.Calc_Type = IIf(rdbBook.IsChecked, "B", "T")
                    objN.Net_Amt_After_Dep = clsCommon.myCdbl(grow.Cells(colLastAmount_AfterDep).Value)

                    If clsCommon.myLen(objN.Asset_Code) > 0 Then
                        objTr.arrMerged_AssetCode.Add(objN)
                    End If
                Next

                If clsCommon.myLen(objTr.Asset_Name) > 0 Or clsCommon.myLen(objTr.Asset_Code) > 0 Then
                    obj.Arr.Add(objTr)
                End If
                ''=============================================================================

                If (obj.Arr Is Nothing OrElse obj.Arr.Count <= 0) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Please Fill at list one Item", Me.Text)
                    Return False
                End If

                If obj.SaveData(obj, isNewEntry) Then
                    LoadData(obj.Acquisition_Code, NavigatorType.Current)
                    Return True
                End If
            Else
                Return False
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
        Return False
    End Function

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Dim obj As New clsFAMergeHead()
        Dim ob As New clsFAMergeDetail()
        Try
            BlankAllControls()

            isInsideLoadData = True
            obj = New clsFAMergeHead()
            obj = clsFAMergeHead.GetData(strCode, NavTyep)

            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Acquisition_Code) > 0) Then
                isNewEntry = False
                ddlAcqType.Enabled = False
                txtLocation.Enabled = False
                fndTemplateCode.Enabled = False
                RadGroupBox1.Enabled = False

                btnSave.Text = "Update"
                btnSave.Enabled = True
                btnPost.Enabled = True
                btnChangeDepDetail.Enabled = False
                btnDelete.Enabled = True
                If obj.Status = ERPTransactionStatus.Approved Then
                    btnSave.Enabled = False
                    btnPost.Enabled = False
                    btnChangeDepDetail.Enabled = True
                    btnDelete.Enabled = False
                End If


                UsLock1.Status = obj.Status
                ddlAcqType.Text = obj.Acquisition_Type
                txtDocNo.Value = obj.Acquisition_Code
                txtDate.Value = obj.Acquisition_Date
                txtDesc.Text = obj.Description
                lblNetAmt.Text = clsCommon.myFormat(obj.Net_Amt)
                txtLocation.Value = obj.Loc_Code
                fndTemplateCode.Value = obj.Templete_Code
                lblLocation.Text = clsLocation.GetName(obj.Loc_Code, Nothing)
                lblTemplateDesc.Text = ClsTemplateMaster.GetName(obj.Templete_Code, Nothing)

                If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                    For Each objTr As clsFAMergeDetail In obj.Arr
                        ob = New clsFAMergeDetail()

                        txtAssetCode.Text = objTr.Asset_Code
                        txtAssetDesc.Text = objTr.Asset_Name
                        txtSpecification.Text = objTr.Asset_Specification
                        dtpStartDate.Checked = True
                        dtpStartDate.Text = objTr.Start_Date

                        ob.Asset_Code = objTr.Asset_Code
                        ob.Asset_Name = objTr.Asset_Name
                        ob.Templete_Code = objTr.Templete_Code
                        ob.Templete_Name = objTr.Templete_Name
                        ob.Category_code = objTr.Category_code
                        ob.Category_Name = objTr.Category_Name
                        ob.Group_Code = objTr.Group_Code
                        ob.Group_Code_Name = objTr.Group_Code_Name
                        ob.AcSet_Code = objTr.AcSet_Code
                        ob.AcSet_Code_Name = objTr.AcSet_Code_Name
                        ob.CostCenter_Code = objTr.CostCenter_Code
                        ob.CostCenter_Name = objTr.CostCenter_Name
                        ob.Acqusition_Date = objTr.Acqusition_Date
                        ob.Dep_Method_Code = objTr.Dep_Method_Code
                        ob.Dep_Method_Name = objTr.Dep_Method_Name
                        ob.Dep_Method_Tax_Code = objTr.Dep_Method_Tax_Code
                        ob.Dep_Method_Tax_Name = objTr.Dep_Method_Tax_Name
                        ob.Dep_Period_Code = objTr.Dep_Period_Code
                        ob.Dep_Period_Name = objTr.Dep_Period_Name
                        ob.Start_Date = objTr.Start_Date

                        ob.Dep_Rate = objTr.Dep_Rate
                        ob.Book_Estimated_Life = objTr.Book_Estimated_Life
                        ob.Book_Source_value = objTr.Book_Source_value
                        ob.Book_Source_Original_value = objTr.Book_Source_Original_value

                        ob.Book_Salvage_Rate = objTr.Book_Salvage_Rate
                        ob.Book_Salvage_Value = objTr.Book_Salvage_Value
                        ob.Item_Net_Amt = objTr.Item_Net_Amt

                        ob.Asset_Specification = objTr.Asset_Specification
                        ob.Dep_Tax_Rate = objTr.Dep_Tax_Rate
                        ob.Book_Dep_Type = objTr.Book_Dep_Type
                        ob.Tax_Dep_Type = objTr.Tax_Dep_Type

                        ''========================grid data--------------------------
                        gv1.Rows.Clear()
                        gv1.Rows.AddNew()
                        If objTr.arrMerged_AssetCode IsNot Nothing AndAlso objTr.arrMerged_AssetCode.Count > 0 Then
                            For Each obb As clsFAMergeDetail In objTr.arrMerged_AssetCode
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colAssetID).Value = obb.Asset_Code
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = gv1.Rows.Count
                                FillGridData(gv1.Rows.Count - 1)
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colLastAmount_AfterDep).Value = obb.Net_Amt_After_Dep

                                If obb.Calc_Type = "B" Then
                                    rdbBook.IsChecked = True
                                    rdbTax.IsChecked = False
                                Else
                                    rdbBook.IsChecked = False
                                    rdbTax.IsChecked = True
                                End If
                                gv1.Rows.AddNew()
                            Next
                        End If
                        ''========================================================

                    Next

                    fndTemplateDetail.Tag = ob
                    fndTemplateDetail.Value = fndTemplateCode.Value
                Else
                    fndTemplateDetail.Tag = Nothing
                    fndTemplateDetail.Value = Nothing
                End If
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
        GC.Collect()
    End Sub

    Private Sub btnPost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPost.Click
        PostData()
    End Sub

    Sub PostData()
        Try
            If (myMessages.postConfirm()) Then
                SavingData(True)
                If (clsFAMergeHead.PostData(Form_ID, txtDocNo.Value, True, "NEW")) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Successfully Posted", Me.Text)
                    LoadData(txtDocNo.Value, NavigatorType.Current)
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
                If (clsFAMergeHead.DeleteData(txtDocNo.Value, txtAssetCode.Text)) Then
                    saveCancelLog(Reason, "Delete", Nothing)
                    common.clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ", Me.Text)
                    BlankAllControls()
                End If
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
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

    Private Sub txtDocNo__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtDocNo._MYNavigator
        Try
            Dim qst As String = "select count(*) from TSPL_ACQUISITION_HEAD where Acquisition_Code='" + txtDocNo.Value + "'"
            Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qst))
            If count = 0 Then
                txtDocNo.MyReadOnly = False
            Else
                txtDocNo.MyReadOnly = True
            End If
            LoadData(txtDocNo.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtDocNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtDocNo._MYValidating
        Dim qry As String = "select Acquisition_Code as Code,convert (varchar(10), Acquisition_Date,103) as Date, Net_Amt as Amount,case when Status='0' then 'Pending' else 'Approved' end as [Status],Acquisition_Type as [Acquisition Type]  from TSPL_ACQUISITION_HEAD"
        LoadData(clsCommon.ShowSelectForm("AcquiFndd", qry, "Code", " Acquisition_Type='Merge'", txtDocNo.Value, "TSPL_ACQUISITION_HEAD.Acquisition_Date desc", isButtonClicked), NavigatorType.Current)
    End Sub

    Private Sub FrmAPInvoiceEntry_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Try
            If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnAddNew.Enabled Then
                BlankAllControls()
            ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso btnSave.Enabled AndAlso MyBase.isModifyFlag Then
                SavingData(False)
            ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso btnPost.Enabled AndAlso MyBase.isPostFlag Then
                PostData()
            ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso btnDelete.Enabled AndAlso MyBase.isDeleteFlag Then
                DeleteData()
            ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnClose.Enabled Then
                CloseForm()
            End If

            If e.KeyCode = Keys.F2 AndAlso gv1.CurrentColumn IsNot Nothing AndAlso gv1.CurrentColumn Is gv1.Columns(colAssetID) Then
                isCellValueChangedOpen = True
                OpenAssetList(True)
                isCellValueChangedOpen = False
            End If
        Catch ex As Exception
            isCellValueChangedOpen = False
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub SaveLayout1()
        If clsCommon.myLen(MyBase.Form_ID) > 0 Then
            gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = MyBase.Form_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv1.SaveLayout(obj.GridLayout)
            obj.GridColumns = gv1.ColumnCount
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully", "Information", Me.Text)
            End If
            ''stuti regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Sub DeleteLayout()
        clsGridLayout.DeleteData(MyBase.Form_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", "Information", Me.Text)
    End Sub

    Sub DisableTemplateFields(ByVal frm As FrmAcquisitionEntryDetail)
        If clsCommon.CompairString(ddlAcqType.Text, "Merge") = CompairStringResult.Equal Or clsCommon.CompairString(ddlAcqType.Text, "Direct") = CompairStringResult.Equal Or clsCommon.CompairString(ddlAcqType.Text, "Assembled") = CompairStringResult.Equal Then
            frm.txtAssetSpecification.Enabled = True
            frm.txtItem.Enabled = False
            frm.txtTemplate.Enabled = False
            frm.txtCategory.Enabled = False
            frm.txtGroup.Enabled = True
            frm.txtCostCenter.Enabled = True
            frm.txtAccountSet.Enabled = False
            frm.txtAcquisitionDate.Enabled = True

            frm.txtDepMethod.Enabled = True
            frm.txtDepMethodTax.Enabled = True
            frm.txtDepPeriod.Enabled = True
            frm.txtStartDate.Enabled = True
            frm.txtDepRate.Enabled = True
            frm.txtDepTaxRate.Enabled = True
            frm.txtEstLife.Enabled = True
            frm.txtSourceOrgValue.Enabled = False
            frm.txtSourceValue.Enabled = False
            frm.txtSalvageRate.Enabled = True
            frm.txtSalvageValue.Enabled = True
            frm.txtNetValue.Enabled = False
            frm.txtTaxAmount.Enabled = False
        Else
            frm.txtAssetSpecification.Enabled = False
            frm.txtItem.Enabled = False
            frm.txtTemplate.Enabled = True
            frm.txtCategory.Enabled = False
            frm.txtGroup.Enabled = True
            frm.txtCostCenter.Enabled = False
            frm.txtAccountSet.Enabled = False
            frm.txtAcquisitionDate.Enabled = False
            frm.txtTaxAmount.Enabled = False
            frm.txtDepMethod.Enabled = False
            frm.txtDepMethodTax.Enabled = False
            frm.txtDepPeriod.Enabled = False
            frm.txtStartDate.Enabled = True
            frm.txtDepRate.Enabled = False
            frm.txtDepTaxRate.Enabled = False
            frm.txtEstLife.Enabled = True
            frm.txtSourceOrgValue.Enabled = False
            frm.txtSourceValue.Enabled = False
            frm.txtSalvageRate.Enabled = True
            frm.txtSalvageValue.Enabled = False
            frm.txtNetValue.Enabled = False
        End If

    End Sub

    Private Sub gv1_CurrentColumnChanged(sender As Object, e As CurrentColumnChangedEventArgs) Handles gv1.CurrentColumnChanged
        If gv1.RowCount > 0 Then
            Dim intCurrRow As Integer = gv1.CurrentRow.Index
            gv1.CurrentRow.Cells(colLineNo).Value = clsCommon.myCdbl(intCurrRow) + 1
            If intCurrRow = gv1.Rows.Count - 1 Then
                gv1.Rows.AddNew()
                gv1.CurrentRow = gv1.Rows(intCurrRow)
            End If
        End If
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
        Else
            lblNetAmt.Text = lblNetAmt.Text - clsCommon.myCdbl(gv1.CurrentRow.Cells(colLastAmount_AfterDep).Value)
        End If
    End Sub

    Private Sub txtLocation__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtLocation._MYValidating
        txtLocation.Value = clsLocation.getFinder("Location_Type='Physical'", txtLocation.Value, isButtonClicked)
        lblLocation.Text = clsLocation.GetName(txtLocation.Value, Nothing)
    End Sub

    Private Sub BtnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnPrint.Click
        Try
            If clsCommon.myLen(txtDocNo.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please Select a Acquisition First.", Me.Text)
                Return
            End If
            Dim frm As New frmCrystalReportViewer()
            Dim Qry As String = ""
            Qry += " SELECT '" + objCommonVar.CurrentCompanyName + "' as [Company], TSPL_ACQUISITION_HEAD.Acquisition_Code, TSPL_ACQUISITION_HEAD.Acquisition_Date, TSPL_ACQUISITION_HEAD.PO_No,  TSPL_ACQUISITION_HEAD.Description, TSPL_ACQUISITION_HEAD.Remarks,TSPL_LOCATION_MASTER.Location_Desc ,"
            Qry += " TSPL_VENDOR_MASTER.Vendor_Name,  "
            Qry += " TSPL_ACQUISITION_HEAD.tax1,TSPL_ACQUISITION_HEAD.tax1_amt,TSPL_ACQUISITION_HEAD.tax2,TSPL_ACQUISITION_HEAD.tax2_amt,TSPL_ACQUISITION_HEAD.tax3,TSPL_ACQUISITION_HEAD.tax3_amt,TSPL_ACQUISITION_HEAD.tax4,TSPL_ACQUISITION_HEAD.tax4_amt,TSPL_ACQUISITION_HEAD.tax5,TSPL_ACQUISITION_HEAD.tax5_amt,TSPL_ACQUISITION_HEAD.tax6,TSPL_ACQUISITION_HEAD.tax6_amt,TSPL_ACQUISITION_HEAD.tax7,TSPL_ACQUISITION_HEAD.tax7_amt,TSPL_ACQUISITION_HEAD.tax8,TSPL_ACQUISITION_HEAD.tax8_amt,TSPL_ACQUISITION_HEAD.tax9,TSPL_ACQUISITION_HEAD.tax9_amt,TSPL_ACQUISITION_HEAD.tax10,TSPL_ACQUISITION_HEAD.tax10_amt,"
            Qry += " TSPL_ACQUISITION_HEAD.total_amt, TSPL_ACQUISITION_HEAD.total_tax_amt, TSPL_ACQUISITION_HEAD.Net_Amt,"
            Qry += " TSPL_ACQUISITION_DETAIL.SNo, TSPL_ACQUISITION_DETAIL.Asset_Code, TSPL_ACQUISITION_DETAIL.Asset_Name, TSPL_ACQUISITION_DETAIL.Asset_Specification, TSPL_ACQUISITION_DETAIL.Dep_Rate, TSPL_ACQUISITION_DETAIL.Book_Source_value"
            Qry += " FROM TSPL_ACQUISITION_HEAD "
            Qry += " left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_ACQUISITION_HEAD.Vendor_Code "
            Qry += " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code =TSPL_ACQUISITION_HEAD.Loc_Code"
            Qry += " left outer join TSPL_ACQUISITION_DETAIL on TSPL_ACQUISITION_DETAIL.Acquisition_Code =TSPL_ACQUISITION_HEAD.Acquisition_Code"
            Qry += " where TSPL_ACQUISITION_HEAD.Acquisition_Code = '" + txtDocNo.Value + " '"

            Dim dt_final As DataTable = clsDBFuncationality.GetDataTable(Qry)
            If dt_final.Rows.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)
            Else
                frm.funreport(CrystalReportFolder.FixedAssets, dt_final, "frmFAMergeAssetReport", "Acquision Entry Report")
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnChangeDepDetail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnChangeDepDetail.Click
        Try
            If clsCommon.MyMessageBoxShow(Me, "Do you want to change Depreciation details" + Environment.NewLine + "Are you sure", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                Dim arr As New List(Of clsFAMergeDetail)()
                For Each grow As GridViewRowInfo In gv1.Rows
                    If clsCommon.myLen(grow.Cells(colAssetID).Value) Then
                        Dim objTr As New clsFAMergeDetail()
                        If clsAssetDepreciation.GetAssetDepCount(grow.Cells(colAssetID).Value, Nothing) > 0 Then
                            clsCommon.MyMessageBoxShow(Me, "Depreciation of Asset-" & grow.Cells(colAssetID).Value & " has been done. Can not change depereciation details for this Asset.")
                            Continue For
                        End If
                        objTr.Asset_Code = clsCommon.myCstr(grow.Cells(colAssetID).Value)
                        objTr.Dep_Method_Code = clsCommon.myCstr(grow.Cells(colDepMethod).Value)
                        objTr.Dep_Method_Tax_Code = clsCommon.myCstr(grow.Cells(colDepMethodTax).Value)
                        objTr.Dep_Period_Code = clsCommon.myCstr(grow.Cells(colDepPeriodCode).Value)
                        objTr.Dep_Rate = clsCommon.myCdbl(grow.Cells(colDepRate).Value)
                        objTr.Dep_Tax_Rate = clsCommon.myCdbl(grow.Cells(colDepTaxRate).Value)
                        objTr.Asset_Specification = clsCommon.myCstr(grow.Cells(colAssetSpecificaion).Value)
                        objTr.Asset_Name = clsCommon.myCstr(grow.Cells(colAssetName).Value)
                        objTr.Book_Salvage_Rate = clsCommon.myCstr(grow.Cells(ColBookSalvageRate).Value)
                        objTr.Book_Salvage_Value = clsCommon.myCstr(grow.Cells(ColBookSalvageValue).Value)
                        objTr.Book_Estimated_Life = clsCommon.myCstr(grow.Cells(ColBookEstimatedLife).Value)
                        objTr.Start_Date = clsCommon.myCDate(grow.Cells(colStartDate).Value)
                        arr.Add(objTr)
                    End If
                Next
                clsFAMergeDetail.UpdateDecpreciationData(txtDocNo.Value, arr)
                clsCommon.MyMessageBoxShow(Me, "Successfully changed the depreciation details", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub fndTemplateCode__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndTemplateCode._MYValidating
        Dim qry As String = " select template_code as Code,template_Name as Name,  category_code,group_code,Acset_code from TSPL_FA_TEMPLATE_MASTER "
        fndTemplateCode.Value = clsCommon.myCstr(clsCommon.ShowSelectForm("ACQDETTemplate", qry, "Code", "", fndTemplateCode.Value, "", isButtonClicked))
        If Not fndTemplateCode.Value Is Nothing Then
            If clsCommon.myLen(clsCommon.myCstr(fndTemplateCode.Value)) > 0 Then
                Me.lblTemplateDesc.Text = ClsTemplateMaster.GetData(fndTemplateCode.Value, NavigatorType.Current).template_Name
            End If
        End If


    End Sub

    Private Sub rmSaveLayout_Click(sender As Object, e As EventArgs) Handles rmSaveLayout.Click
        SaveLayout1()
    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        DeleteLayout()
    End Sub

    Private Sub fndTemplateDetail__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndTemplateDetail._MYValidating
        Dim ob As New clsFAMergeDetail
        Dim objTemp As New ClsTemplateMaster()
        Try
            If clsCommon.myLen(fndTemplateCode.Value) <= 0 Then
                fndTemplateCode.Focus()
                fndTemplateCode.Select()
                fndTemplateDetail.Value = ""
                RadMessageBox.Show("Please Select Template Code First")
                Exit Sub
            Else
                If clsAssetDepreciation.GetAssetDepCount(txtAssetCode.Text, Nothing) > 0 Then
                    clsCommon.MyMessageBoxShow(Me, "Depreciation of Asset-" & txtAssetCode.Text & " has been done. Can not change depereciation details for this Asset.")
                    Exit Sub
                End If

                If clsCommon.myLen(txtAssetDesc.Text) <= 0 Then
                    txtAssetDesc.Focus()
                    txtAssetDesc.Select()
                    fndTemplateDetail.Value = ""
                    RadMessageBox.Show("Please enter Asset Description")
                    Exit Sub
                End If

                If clsCommon.myCdbl(lblNetAmt.Text) <= 0 Then
                    gv1.Focus()
                    gv1.Select()
                    fndTemplateDetail.Value = ""
                    RadMessageBox.Show("Please select atleast one non-zero Asset for merging.")
                    Exit Sub
                End If

                Dim frm As New FrmAcquisitionEntryDetail()
                Dim Template_Code As String = fndTemplateCode.Value
                objTemp = New ClsTemplateMaster

                If clsCommon.myLen(Template_Code) > 0 Then
                    objTemp = ClsTemplateMaster.GetData(Template_Code, NavigatorType.Current, Nothing)
                End If

                DisableTemplateFields(frm)

                frm.isPostedTransaction = IIf(UsLock1.Status = ERPTransactionStatus.Approved, True, False)
                frm.obj = New clsAcquisitionDetail()
                frm.obj.Acqusition_Date = txtDate.Value
                frm.obj.Asset_Code = clsCommon.myCstr(txtAssetCode.Text)
                frm.obj.Asset_Name = clsCommon.myCstr(txtAssetDesc.Text)
                frm.obj.Templete_Code = Template_Code
                frm.obj.Templete_Name = objTemp.template_Name

                ''====================pick if any value is already tagged
                If fndTemplateDetail.Tag IsNot Nothing AndAlso TryCast(fndTemplateDetail.Tag, clsFAMergeDetail) IsNot Nothing AndAlso clsCommon.myLen(TryCast(fndTemplateDetail.Tag, clsFAMergeDetail).Asset_Name) > 0 Then
                    ob = TryCast(fndTemplateDetail.Tag, clsFAMergeDetail)
                Else
                    ob = Nothing
                End If
                ''=================================

                ''
                If ob IsNot Nothing AndAlso Not IsDBNull(ob.Start_Date) AndAlso IsDate(ob.Start_Date) Then
                    frm.obj.Start_Date = ob.Start_Date
                ElseIf dtpStartDate.Checked = True Then
                    frm.obj.Start_Date = dtpStartDate.Text
                Else
                    frm.obj.Start_Date = txtDate.Text
                End If
                ''
                If ob IsNot Nothing AndAlso clsCommon.myLen(ob.Category_code) > 0 Then
                    frm.obj.Category_code = ob.Category_code
                Else
                    frm.obj.Category_code = objTemp.category_code
                End If
                ''
                If ob IsNot Nothing AndAlso clsCommon.myLen(ob.Asset_Specification) > 0 Then
                    frm.obj.Asset_Specification = ob.Asset_Specification
                Else
                    frm.obj.Asset_Specification = txtSpecification.Text
                End If
                ''
                If ob IsNot Nothing AndAlso clsCommon.myLen(ob.Category_Name) > 0 Then
                    frm.obj.Category_Name = ob.Category_Name
                Else
                    frm.obj.Category_Name = objTemp.category_Description
                End If
                ''
                If ob IsNot Nothing AndAlso clsCommon.myLen(ob.CostCenter_Code) > 0 Then
                    frm.obj.CostCenter_Code = ob.CostCenter_Code
                Else
                    frm.obj.CostCenter_Code = objTemp.CostCenter_Code
                End If
                ''
                If ob IsNot Nothing AndAlso clsCommon.myLen(ob.CostCenter_Name) > 0 Then
                    frm.obj.CostCenter_Name = ob.CostCenter_Name
                Else
                    frm.obj.CostCenter_Name = objTemp.CostCenter_Description
                End If
                ''
                If ob IsNot Nothing AndAlso clsCommon.myLen(ob.Group_Code) > 0 Then
                    frm.obj.Group_Code = ob.Group_Code
                Else
                    frm.obj.Group_Code = objTemp.group_code
                End If
                ''
                If ob IsNot Nothing AndAlso clsCommon.myLen(ob.Group_Code_Name) > 0 Then
                    frm.obj.Group_Code_Name = ob.Group_Code_Name
                Else
                    frm.obj.Group_Code_Name = objTemp.group_Description
                End If
                ''
                If ob IsNot Nothing AndAlso clsCommon.myLen(ob.AcSet_Code) > 0 Then
                    frm.obj.AcSet_Code = ob.AcSet_Code
                Else
                    frm.obj.AcSet_Code = objTemp.Acset_code
                End If
                ''
                If ob IsNot Nothing AndAlso clsCommon.myLen(ob.AcSet_Code_Name) > 0 Then
                    frm.obj.AcSet_Code_Name = ob.AcSet_Code_Name
                Else
                    frm.obj.AcSet_Code_Name = objTemp.Acset_Description
                End If
                ''
                If ob IsNot Nothing AndAlso clsCommon.myLen(ob.Dep_Method_Code) > 0 Then
                    frm.obj.Dep_Method_Code = ob.Dep_Method_Code
                Else
                    frm.obj.Dep_Method_Code = objTemp.Dep_Method_Code
                End If
                ''
                If ob IsNot Nothing AndAlso clsCommon.myLen(ob.Dep_Method_Name) > 0 Then
                    frm.obj.Dep_Method_Name = ob.Dep_Method_Name
                Else
                    frm.obj.Dep_Method_Name = objTemp.Dep_Method_Name
                End If
                ''
                If ob IsNot Nothing AndAlso clsCommon.myLen(ob.Dep_Method_Tax_Code) > 0 Then
                    frm.obj.Dep_Method_Tax_Code = ob.Dep_Method_Tax_Code
                Else
                    frm.obj.Dep_Method_Tax_Code = objTemp.Dep_Method_Tax_Code
                End If
                ''
                If ob IsNot Nothing AndAlso clsCommon.myLen(ob.Dep_Method_Tax_Name) > 0 Then
                    frm.obj.Dep_Method_Tax_Name = ob.Dep_Method_Tax_Name
                Else
                    frm.obj.Dep_Method_Tax_Name = objTemp.Dep_Method_Tax_Name
                End If
                ''
                If ob IsNot Nothing AndAlso clsCommon.myLen(ob.Dep_Period_Code) > 0 Then
                    frm.obj.Dep_Period_Code = ob.Dep_Period_Code
                Else
                    frm.obj.Dep_Period_Code = objTemp.Dep_Period_Code
                End If
                ''
                If ob IsNot Nothing AndAlso clsCommon.myLen(ob.Dep_Period_Name) > 0 Then
                    frm.obj.Dep_Period_Name = ob.Dep_Period_Name
                Else
                    frm.obj.Dep_Period_Name = objTemp.Dep_Period_Name
                End If
                ''
                If ob IsNot Nothing AndAlso clsCommon.myLen(ob.Dep_Rate) > 0 Then
                    frm.obj.Dep_Rate = ob.Dep_Rate
                Else
                    frm.obj.Dep_Rate = objTemp.Dep_Rate
                End If
                ''
                If ob IsNot Nothing AndAlso clsCommon.myLen(ob.Book_Estimated_Life) > 0 Then
                    frm.obj.Book_Estimated_Life = ob.Book_Estimated_Life
                Else
                    frm.obj.Book_Estimated_Life = objTemp.Book_Estimated_Life
                End If
                ''
                If ob IsNot Nothing AndAlso clsCommon.myLen(ob.Dep_Tax_Rate) > 0 Then
                    frm.obj.Dep_Tax_Rate = ob.Dep_Tax_Rate
                Else
                    frm.obj.Dep_Tax_Rate = objTemp.Dep_Tax_Rate
                End If
                ''
                If ob IsNot Nothing AndAlso clsCommon.myLen(ob.Book_Source_value) > 0 Then
                    frm.obj.Book_Source_value = ob.Book_Source_value
                ElseIf clsCommon.myCdbl(lblNetAmt.Text) > 0 Then
                    frm.obj.Book_Source_value = lblNetAmt.Text
                Else
                    frm.obj.Book_Source_value = objTemp.Book_Source_value
                End If
                ''
                If ob IsNot Nothing AndAlso clsCommon.myLen(ob.Book_Source_Original_value) > 0 Then
                    frm.obj.Book_Source_Original_value = ob.Book_Source_Original_value
                ElseIf clsCommon.myCdbl(lblNetAmt.Text) > 0 Then
                    frm.obj.Book_Source_Original_value = lblNetAmt.Text
                Else
                    frm.obj.Book_Source_Original_value = objTemp.Book_Source_Original_value
                End If
                ''
                If ob IsNot Nothing AndAlso clsCommon.myLen(ob.Book_Salvage_Rate) > 0 Then
                    frm.obj.Book_Salvage_Rate = ob.Book_Salvage_Rate
                Else
                    frm.obj.Book_Salvage_Rate = objTemp.Book_Salvage_Rate
                End If
                ''
                If ob IsNot Nothing AndAlso clsCommon.myLen(ob.Book_Salvage_Value) > 0 Then
                    frm.obj.Book_Salvage_Value = ob.Book_Salvage_Value
                ElseIf clsCommon.myCdbl(lblNetAmt.Text) > 0 AndAlso clsCommon.myCdbl(frm.obj.Book_Salvage_Rate) > 0 Then
                    frm.obj.Book_Salvage_Value = System.Math.Round(clsCommon.myCdbl(lblNetAmt.Text) / clsCommon.myCdbl(frm.obj.Book_Salvage_Rate), 2)
                Else
                    frm.obj.Book_Salvage_Value = objTemp.Book_Salvage_Value
                End If
                ''
                If ob IsNot Nothing AndAlso clsCommon.myLen(ob.Total_Tax_Amt) > 0 Then
                    frm.obj.Total_Tax_Amt = ob.Total_Tax_Amt
                End If
                frm.obj.Item_Net_Amt = (frm.obj.Total_Tax_Amt + frm.obj.Book_Source_value)
                ''
                If ob IsNot Nothing AndAlso clsCommon.myLen(ob.Tax_Dep_Type) > 0 Then
                    frm.obj.Tax_Dep_Type = ob.Tax_Dep_Type
                Else
                    If objTemp.Tax_Dep_Type = "F" Then
                        frm.obj.Tax_Dep_Type = "Formula"
                    ElseIf objTemp.Tax_Dep_Type = "M" Then
                        frm.obj.Tax_Dep_Type = "Manual"
                    Else
                        frm.obj.Tax_Dep_Type = ""
                    End If
                End If
                ''
                If ob IsNot Nothing AndAlso clsCommon.myLen(ob.Book_Dep_Type) > 0 Then
                    frm.obj.Book_Dep_Type = ob.Book_Dep_Type
                Else
                    If objTemp.Book_Dep_Type = "F" Then
                        frm.obj.Book_Dep_Type = "Formula"
                    ElseIf objTemp.Book_Dep_Type = "M" Then
                        frm.obj.Book_Dep_Type = "Manual"
                    Else
                        frm.obj.Book_Dep_Type = ""
                    End If
                End If
                '''''''''''''''''''
                frm.ShowDialog()

                ob = New clsFAMergeDetail()
                If frm.obj IsNot Nothing Then
                    isCellValueChangedOpen = True
                    If UsLock1.Status = ERPTransactionStatus.Posted OrElse UsLock1.Status = ERPTransactionStatus.Approved Then
                        txtAssetCode.Text = frm.obj.Asset_Code
                    Else
                        txtAssetCode.Text = ""
                    End If

                    txtAssetDesc.Text = frm.obj.Asset_Name
                    txtSpecification.Text = frm.obj.Asset_Specification
                    dtpStartDate.Checked = True
                    dtpStartDate.Text = frm.obj.Start_Date

                    ob.Asset_Code = frm.obj.Asset_Code
                    ob.Asset_Name = frm.obj.Asset_Name
                    ob.Templete_Code = frm.obj.Templete_Code
                    ob.Templete_Name = frm.obj.Templete_Name
                    ob.Category_code = frm.obj.Category_code
                    ob.Category_Name = frm.obj.Category_Name
                    ob.CostCenter_Code = frm.obj.CostCenter_Code
                    ob.CostCenter_Name = frm.obj.CostCenter_Name
                    ob.Group_Code = frm.obj.Group_Code
                    ob.Group_Code_Name = frm.obj.Group_Code_Name
                    ob.AcSet_Code = frm.obj.AcSet_Code
                    ob.AcSet_Code_Name = frm.obj.AcSet_Code_Name
                    ob.Acqusition_Date = frm.obj.Acqusition_Date
                    ob.Dep_Method_Code = frm.obj.Dep_Method_Code
                    ob.Dep_Method_Name = frm.obj.Dep_Method_Name
                    ob.Dep_Method_Tax_Code = frm.obj.Dep_Method_Tax_Code
                    ob.Dep_Method_Tax_Name = frm.obj.Dep_Method_Tax_Name
                    ob.Dep_Period_Code = frm.obj.Dep_Period_Code
                    ob.Dep_Period_Name = frm.obj.Dep_Period_Name
                    ob.Start_Date = frm.obj.Start_Date
                    ob.Dep_Rate = frm.obj.Dep_Rate
                    ob.Book_Estimated_Life = frm.obj.Book_Estimated_Life
                    ob.Book_Source_value = frm.obj.Book_Source_value
                    ob.Book_Source_Original_value = frm.obj.Book_Source_Original_value
                    ob.Book_Salvage_Rate = frm.obj.Book_Salvage_Rate
                    ob.Book_Salvage_Value = frm.obj.Book_Salvage_Value
                    ob.Asset_Specification = frm.obj.Asset_Specification
                    ob.Dep_Tax_Rate = frm.obj.Dep_Tax_Rate
                    ob.Total_Tax_Amt = frm.obj.Total_Tax_Amt
                    ob.Item_Net_Amt = frm.obj.Item_Net_Amt
                    If clsCommon.CompairString(frm.obj.Tax_Dep_Type, "Formula") = CompairStringResult.Equal Then
                        ob.Tax_Dep_Type = frm.obj.Tax_Dep_Type
                    ElseIf clsCommon.CompairString(frm.obj.Tax_Dep_Type, "Manual") = CompairStringResult.Equal Then
                        ob.Tax_Dep_Type = frm.obj.Tax_Dep_Type
                    Else
                        Throw New Exception("Select Tax Rate Type(Formula,Manual)")
                    End If

                    If clsCommon.CompairString(frm.obj.Book_Dep_Type, "Formula") = CompairStringResult.Equal Then
                        ob.Book_Dep_Type = frm.obj.Book_Dep_Type
                    ElseIf clsCommon.CompairString(frm.obj.Book_Dep_Type, "Manual") = CompairStringResult.Equal Then
                        ob.Book_Dep_Type = frm.obj.Book_Dep_Type
                    Else
                        Throw New Exception("Select Book Rate Type(Formula,Manual)")
                    End If

                    fndTemplateDetail.Tag = ob
                    fndTemplateDetail.Value = fndTemplateCode.Value
                    isCellValueChangedOpen = False
                Else
                    fndTemplateDetail.Tag = Nothing
                    fndTemplateDetail.Value = Nothing
                End If ''array of tagged value

                UpdateAllTotals()
            End If ''template length

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub rdbBook_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles rdbBook.ToggleStateChanged, rdbTax.ToggleStateChanged
        If Not isInsideLoadData Then
            Dim qry As String = ""
            For Each grow As GridViewRowInfo In gv1.Rows
                If clsCommon.myLen(grow.Cells(colAssetID).Value) > 0 Then
                    ''==========for getting last net amt after dep. amount=================
                    qry = clsAcquisitionDetail.GetAssetQuery()
                    qry += " and  ACQ.Loc_Code= '" + txtLocation.Value + "' " + Environment.NewLine
                    qry += " and  ACQD.Asset_Code ='" + clsCommon.myCstr(gv1.CurrentRow.Cells(colAssetID).Value) + "' " + Environment.NewLine

                    If rdbBook.IsChecked Then
                        qry = "select top 1 Book.Asset_Value from (" & qry & ") as Book "
                    Else
                        qry = "select top 1 Book.Asset_Value_Tax from (" & qry & ") as Book "
                    End If
                    gv1.CurrentRow.Cells(colLastAmount_AfterDep).Value = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))

                    fndTemplateDetail.Value = ""
                    fndTemplateDetail.Tag = Nothing
                    UpdateAllTotals()
                End If
            Next ''
        End If
    End Sub
End Class
