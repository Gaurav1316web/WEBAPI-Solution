Imports common
Imports System.Data.SqlClient

Public Class FrmSchemeMasterDairy
    Inherits FrmMainTranScreen
#Region "Variables"
    Dim CombineExportImportOnSchemeMaster As Boolean = False
    Dim AllowSchemeItems As Boolean = False
    Const colLineNo As String = "LineNo"
    Const colICode As String = "ItemCode"
    Const colIName As String = "ItemName"
    Const colSchemeItemStrCode As String = "colSchemeItemStrCode"
    Const colSchemeItemStrDesc As String = "colSchemeItemStrDesc"
    Const colUOM As String = "UnitCode"
    Const colPriceDate As String = "PriceDate"
    Const colQty As String = "Quantity"
    Const colMRP As String = "MRP"
    Const colRemarks As String = "Remarks"
    Const colISelect As String = "ISelect"
    Const colMainICode As String = "colMainICode"
    Const colMainIName As String = "colMainIName"
    Const colMainIStructureCode As String = "colMainIStructureCode"
    Const colMainIStructureDesc As String = "colMainIStructureDesc"
    Const colMainIUnit As String = "colMainIUnit"
    Const colMainIQty As String = "colMainIQty"
    Const colMainIAmt As String = "colMainAmt"
    Const colCashPer As String = "colCashPer"
    Const colCashAmt As String = "colCashAmt"
    Const colMaxLimit As String = "colMaxLimit"
    Const colIncrementValue As String = "colIncrementValue"

    Const colSelect As String = "Select"
    Const colCustCode As String = "Customer Code"
    Const colCustName As String = "CustName"


    Const colInLineNo As String = "LineNo"
    Const colFromRange As String = "FromRange"
    Const colToRange As String = "ToRange"
    Const colRangeUom As String = "RangeUom"
    Const colIncentiveUom As String = "IncentiveUom"
    Const colCashDisAmt As String = "colCashDisAmt"
    Dim isCellValueChangedOpen As Boolean = False

    Private isInsideLoadData As Boolean = False
    Private isFromLoad As Boolean = False
    Dim dt As DataTable
    Dim qry As String
    Dim CurrentDate As DateTime = clsCommon.GETSERVERDATE()

    Public Const colRange As String = "colRange"
    Public Const colRate As String = "colRate"
    Public Const colTo As String = "colTo"
    Dim isNewEntry As Boolean = True
    Dim SlabChangeFlag As Boolean = False
    Dim IsMRPLoad As Boolean = False
    Private KeyDownisRunning As Boolean = False
    Dim strMsg As String = ""
#End Region

    Private Sub FrmSecondaryCustomer_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnSave.Enabled Then
            SaveData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.R Then
            Reset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso btnDelete.Enabled Then
            DeleteData(fndScheme.Value)
        End If
    End Sub

    Private Sub FrmSchemeMasterNew_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            RadGroupBox9.Enabled = True
            gvTS.Enabled = True
            isFromLoad = True
            Dim ButtonToolTip As ToolTip = New ToolTip()
            SetUserMgmtNew()
            AllowSchemeItems = IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowSchemeItemCondONSchemeMaster, clsFixedParameterCode.AllowSchemeItemCondONSchemeMaster, Nothing)) = "1", True, False)
            ValidateLength()
            LoadTypes()
            LoadQuantitiveType()
            LoadCriteria()
            LoadBlankItemGrid()
            gvItem.Rows.AddNew()
            LoadBlankCustomerGrid()
            loadBlankGridRange()
            Reset()
            '----------For Custom Fields----------
            RadPageView1.Pages("pvpCustomFields").Item.Visibility = MyBase.customFieldTabProperty
            If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
                UcCustomFields1.Report_ID = MyBase.Form_ID
                UcCustomFields1.LoadCustomControls()
            End If
            '---------End of For Custom Fields----
            isFromLoad = False
            txtAmount.Enabled = False
            txtPercentage.Enabled = False
            dtpInactive.Enabled = False
            '= KUNAL > TICKET : BM00000009590 > DATE : 20-09-2016
            If objCommonVar.IsDemoERP Then
                UcAttachment1.Form_ID = MyBase.Form_ID
                RadPageView1.Pages("pgAttachmentSchMst").Item.Visibility = ElementVisibility.Visible
            Else
                RadPageView1.Pages("pgAttachmentSchMst").Item.Visibility = ElementVisibility.Collapsed
            End If
            RadPageView1.SelectedPage = RadPageViewPage1
            RadPageView1.Pages("VolumeSlab").Item.Visibility = ElementVisibility.Collapsed
            RadPageView1.Pages("RadPageViewPage4").Item.Visibility = ElementVisibility.Collapsed
            gvTS2.ReadOnly = True
            btn_Apply.Visible = False
        Catch ex As Exception
            Throw New Exception(ex.Message) ''clsCommon.MyMessageBoxShow(ex.Message) comment it because after getting permission denied exception, it still opened the screen due to showmessage propety,so chenage it into throw exception
        End Try

    End Sub

    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.frmSchemeMasterNew)
        If Not (MyBase.isReadFlag) Then
            '--------richa Ticket no. BM00000003121 15/07/2014 
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag
        CombineExportImportOnSchemeMaster = IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.CombineExportImportOnSchemeMaster, clsFixedParameterCode.CombineExportImportOnSchemeMaster, Nothing)) = "1", True, False)

        '--------richa Ticket no. BM00000003014 02/07/2014 to enable/disable import/export option acc. to user mgmt setting -----------
        If btnSave.Visible = True Then
            rmiImport.Enabled = True
            rmiExport.Enabled = True
            rmWholeSheetExport.Enabled = True
            rmImportWholeSheet.Enabled = True
        Else
            rmiImport.Enabled = False
            rmiExport.Enabled = False
            rmWholeSheetExport.Enabled = False
            rmImportWholeSheet.Enabled = False
        End If
        '--------------------------------------------------

        If CombineExportImportOnSchemeMaster Then
            rmWholeSheetExport.Visibility = ElementVisibility.Visible
            rmImportWholeSheet.Visibility = ElementVisibility.Visible
        Else
            rmWholeSheetExport.Visibility = ElementVisibility.Collapsed
            rmImportWholeSheet.Visibility = ElementVisibility.Collapsed
        End If
        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub ValidateLength()
        txtDesc.MaxLength = 200
        txtComment.MaxLength = 200
    End Sub

    Sub LoadBlankItemGrid()
        gvItem.Rows.Clear()
        gvItem.Columns.Clear()

        Dim repoLineNo As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "Line No"
        repoLineNo.Name = colLineNo
        repoLineNo.Width = 50
        repoLineNo.ReadOnly = True
        repoLineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvItem.MasterTemplate.Columns.Add(repoLineNo)

        Dim repoISelect As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoISelect.FormatString = ""
        repoISelect.HeaderText = "Select"
        repoISelect.Name = colISelect
        repoISelect.Width = 100
        repoISelect.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gvItem.MasterTemplate.Columns.Add(repoISelect)

        Dim repoMainICode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoMainICode.FormatString = ""
        repoMainICode.HeaderText = "Main Item Code"
        repoMainICode.Name = colMainICode
        repoMainICode.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoMainICode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoMainICode.Width = 100
        gvItem.MasterTemplate.Columns.Add(repoMainICode)

        Dim repoMainIName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoMainIName.FormatString = ""
        repoMainIName.HeaderText = "Main Item Desc"
        repoMainIName.Name = colMainIName
        repoMainIName.Width = 200
        repoMainIName.ReadOnly = True
        gvItem.MasterTemplate.Columns.Add(repoMainIName)

        Dim repoMainIStrCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoMainIStrCode.FormatString = ""
        repoMainIStrCode.HeaderText = "Main Item Structure Code"
        repoMainIStrCode.Name = colMainIStructureCode
        repoMainIStrCode.Width = 60
        repoMainIStrCode.ReadOnly = True
        gvItem.MasterTemplate.Columns.Add(repoMainIStrCode)

        Dim repoMainIStrDesc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoMainIStrDesc.FormatString = ""
        repoMainIStrDesc.HeaderText = "Main Item Structure Desc"
        repoMainIStrDesc.Name = colMainIStructureDesc
        repoMainIStrDesc.Width = 120
        repoMainIStrDesc.ReadOnly = True
        gvItem.MasterTemplate.Columns.Add(repoMainIStrDesc)

        Dim repoMainUnitCode As GridViewComboBoxColumn = New GridViewComboBoxColumn()
        repoMainUnitCode.HeaderText = "Main Unit Code"
        repoMainUnitCode.Name = colMainIUnit
        repoMainUnitCode.Width = 80
        repoMainUnitCode.ReadOnly = False
        gvItem.MasterTemplate.Columns.Add(repoMainUnitCode)

        Dim repoMainQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoMainQty.FormatString = ""
        repoMainQty.HeaderText = "Main Qty"
        repoMainQty.Name = colMainIQty
        repoMainQty.Width = 80
        repoMainQty.Minimum = 0
        repoMainQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvItem.MasterTemplate.Columns.Add(repoMainQty)

        Dim repoMainAmount As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoMainAmount.FormatString = ""
        repoMainAmount.HeaderText = "Amount"
        repoMainAmount.Name = colMainIAmt
        repoMainAmount.Width = 80
        repoMainAmount.Minimum = 0
        repoMainAmount.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvItem.MasterTemplate.Columns.Add(repoMainAmount)


        Dim repoICode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoICode.FormatString = ""
        repoICode.HeaderText = "Scheme Item Code"
        repoICode.Name = colICode
        repoICode.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoICode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoICode.Width = 100
        gvItem.MasterTemplate.Columns.Add(repoICode)

        Dim repoIName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoIName.FormatString = ""
        repoIName.HeaderText = "Scheme Item Description"
        repoIName.Name = colIName
        repoIName.Width = 200
        repoIName.ReadOnly = True
        gvItem.MasterTemplate.Columns.Add(repoIName)

        Dim repoSchemeIStrCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSchemeIStrCode.FormatString = ""
        repoSchemeIStrCode.HeaderText = "Scheme Item Structure Code"
        repoSchemeIStrCode.Name = colSchemeItemStrCode
        repoSchemeIStrCode.Width = 60
        repoSchemeIStrCode.ReadOnly = True
        gvItem.MasterTemplate.Columns.Add(repoSchemeIStrCode)

        Dim repoSchemeIStrDesc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSchemeIStrDesc.FormatString = ""
        repoSchemeIStrDesc.HeaderText = "Scheme Item Structure Desc"
        repoSchemeIStrDesc.Name = colSchemeItemStrDesc
        repoSchemeIStrDesc.Width = 120
        repoSchemeIStrDesc.ReadOnly = True
        gvItem.MasterTemplate.Columns.Add(repoSchemeIStrDesc)

        Dim repoUnitCode As GridViewComboBoxColumn = New GridViewComboBoxColumn()
        repoUnitCode.HeaderText = "Scheme Unit Code"
        repoUnitCode.Name = colUOM
        repoUnitCode.Width = 80
        repoUnitCode.ReadOnly = False
        gvItem.MasterTemplate.Columns.Add(repoUnitCode)


        Dim repoPriceDate As GridViewComboBoxColumn = New GridViewComboBoxColumn()
        repoPriceDate.FormatString = ""
        repoPriceDate.HeaderText = "Price Date"
        repoPriceDate.Name = colPriceDate
        repoPriceDate.Width = 100
        repoPriceDate.IsVisible = False
        gvItem.MasterTemplate.Columns.Add(repoPriceDate)

        Dim repoReqQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoReqQty.FormatString = ""
        repoReqQty.HeaderText = "Scheme Qty"
        repoReqQty.Name = colQty
        repoReqQty.Width = 80
        repoReqQty.Minimum = 0
        repoReqQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvItem.MasterTemplate.Columns.Add(repoReqQty)

        Dim repoCashPer As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoCashPer.FormatString = ""
        repoCashPer.HeaderText = "Cash %"
        repoCashPer.Name = colCashPer
        repoCashPer.Width = 80
        'repoCashPer.Minimum = 0
        repoCashPer.ShowUpDownButtons = False
        repoCashPer.Step = 0
        repoCashPer.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvItem.MasterTemplate.Columns.Add(repoCashPer)

        Dim repoCashAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoCashAmt.FormatString = ""
        repoCashAmt.HeaderText = "Cash Amt"
        repoCashAmt.Name = colCashAmt
        repoCashAmt.Width = 80
        'repoCashAmt.Minimum = 0
        repoCashAmt.ShowUpDownButtons = False
        repoCashAmt.Step = 0
        repoCashAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvItem.MasterTemplate.Columns.Add(repoCashAmt)


        Dim repoMRP As GridViewComboBoxColumn = New GridViewComboBoxColumn()
        repoMRP.FormatString = ""
        repoMRP.HeaderText = "MRP"
        repoMRP.Name = colMRP
        repoMRP.Width = 80
        repoMRP.IsVisible = False
        gvItem.MasterTemplate.Columns.Add(repoMRP)

        Dim repoRemark As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoRemark.FormatString = ""
        repoRemark.HeaderText = "Remark"
        repoRemark.Name = colRemarks
        repoRemark.Width = 80
        gvItem.MasterTemplate.Columns.Add(repoRemark)

        repoReqQty = New GridViewDecimalColumn()
        repoReqQty.FormatString = ""
        repoReqQty.HeaderText = "Max Limit"
        repoReqQty.Name = colMaxLimit
        repoReqQty.Width = 80
        repoReqQty.Minimum = 0
        repoReqQty.IsVisible = False
        repoReqQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvItem.MasterTemplate.Columns.Add(repoReqQty)

        repoReqQty = New GridViewDecimalColumn()
        repoReqQty.FormatString = ""
        repoReqQty.HeaderText = "Increment Value"
        repoReqQty.Name = colIncrementValue
        repoReqQty.Width = 80
        repoReqQty.Minimum = 0
        repoReqQty.IsVisible = False
        repoReqQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvItem.MasterTemplate.Columns.Add(repoReqQty)

        gvItem.AllowDeleteRow = True
        gvItem.AllowAddNewRow = False
        gvItem.ShowGroupPanel = False
        gvItem.AllowColumnReorder = False
        gvItem.AllowRowReorder = False
        gvItem.EnableSorting = False
        gvItem.ShowFilteringRow = True
        gvItem.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gvItem.MasterTemplate.ShowRowHeaderColumn = False
        gvItem.TableElement.TableHeaderHeight = 40

       
    End Sub

    Sub setFixedColumns()
        If clsCommon.CompairString(ddlType.SelectedValue, "Fixed") = CompairStringResult.Equal Then
            For ii As Integer = 0 To gvItem.ColumnCount - 1
                If (clsCommon.CompairString(gvItem.Columns(ii).Name, colLineNo) = CompairStringResult.Equal _
                    OrElse clsCommon.CompairString(gvItem.Columns(ii).Name, colICode) = CompairStringResult.Equal _
                    OrElse clsCommon.CompairString(gvItem.Columns(ii).Name, colIName) = CompairStringResult.Equal _
                    OrElse clsCommon.CompairString(gvItem.Columns(ii).Name, colUOM) = CompairStringResult.Equal _
                    OrElse clsCommon.CompairString(gvItem.Columns(ii).Name, colQty) = CompairStringResult.Equal _
                    OrElse clsCommon.CompairString(gvItem.Columns(ii).Name, colRemarks) = CompairStringResult.Equal) Then
                    gvItem.Columns(ii).IsVisible = True
                Else
                    gvItem.Columns(ii).IsVisible = False
                End If
            Next
        End If
    End Sub

    Sub LoadBlankCustomerGrid()
        gvCustomer.DataSource = Nothing
        gvCustomer.Rows.Clear()
        gvCustomer.Columns.Clear()

        Dim repoLineNo As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "Line No"
        repoLineNo.Name = colLineNo
        repoLineNo.Width = 100
        repoLineNo.ReadOnly = True
        repoLineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvCustomer.MasterTemplate.Columns.Add(repoLineNo)

        Dim repoSelect As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoSelect.FormatString = ""
        repoSelect.HeaderText = "Select"
        repoSelect.Name = colSelect
        repoSelect.Width = 100
        repoSelect.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gvCustomer.MasterTemplate.Columns.Add(repoSelect)

        Dim repoCCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCCode.FormatString = ""
        repoCCode.HeaderText = "Customer Code"
        repoCCode.Name = colCustCode
        repoCCode.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoCCode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoCCode.Width = 150
        gvCustomer.MasterTemplate.Columns.Add(repoCCode)

        Dim repoCName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCName.FormatString = ""
        repoCName.HeaderText = "Customer Name"
        repoCName.Name = colCustName
        repoCName.Width = 300
        repoCName.ReadOnly = True
        gvCustomer.MasterTemplate.Columns.Add(repoCName)

        gvCustomer.AllowDeleteRow = True
        gvCustomer.AllowAddNewRow = False
        gvCustomer.ShowGroupPanel = False
        gvCustomer.AllowColumnReorder = False
        gvCustomer.AllowRowReorder = False
        gvCustomer.EnableSorting = False
        gvCustomer.ShowFilteringRow = True
        gvCustomer.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gvCustomer.MasterTemplate.ShowRowHeaderColumn = False
        gvCustomer.TableElement.TableHeaderHeight = 40
    End Sub

    Sub LoadTypes()
        dt = New DataTable
        dt.Columns.Add("Code", GetType(String))
        dt.Rows.Add("Quantitive")
        dt.Rows.Add("Cash")
        dt.Rows.Add("Volume")
        dt.Rows.Add("VolumeSlab")
        dt.Rows.Add("MaxLimit")
        dt.Rows.Add("Discount")
        dt.Rows.Add("Target")
        dt.Rows.Add("Structure")
        dt.Rows.Add("Fixed")
        dt.Rows.Add("VolumeSlab_Cash")
        ddlType.DataSource = dt
        ddlType.ValueMember = "Code"
        ddlType.DisplayMember = "Code"
    End Sub

    Sub LoadQuantitiveType()
        dt = New DataTable
        dt.Columns.Add("Code", GetType(Integer))
        dt.Columns.Add("Name", GetType(String))
        dt.Rows.Add(0, "None")
        dt.Rows.Add(1, "Max Limit")
        dt.Rows.Add(2, "Increment Value")
        cboQuantitiveType.DataSource = dt
        cboQuantitiveType.ValueMember = "Code"
        cboQuantitiveType.DisplayMember = "Name"
    End Sub

    Sub LoadTargetTypes()
        dt = New DataTable
        dt.Columns.Add("Code", GetType(String))
        dt.Rows.Add("Quantity")
        dt.Rows.Add("Amount")
        ddlTargetType.DataSource = dt
        ddlTargetType.ValueMember = "Code"
        ddlTargetType.DisplayMember = "Code"
    End Sub

    Sub LoadCriteria()
        dt = New DataTable
        dt.Columns.Add("Code", GetType(String))
        dt.Rows.Add("Customer Group")
        dt.Rows.Add("Customer Category")
        dt.Rows.Add("Customer")
        dt.Rows.Add("State")
        ddlCriteria.DataSource = dt
        ddlCriteria.ValueMember = "Code"
        ddlCriteria.DisplayMember = "Code"
    End Sub

    Private Sub txtMainItem__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtMainItemGrp._MYValidating
        qry = "select  distinct CSA_TYPE as  Code from TSPL_ITEM_MASTER "
        Dim whrCls = "(CSA_TYPE <> '' and CSA_TYPE <> 'None')"
        txtMainItemGrp.Value = clsCommon.ShowSelectForm("mainItemFinder@SCHMMD", qry, "Code", whrCls, txtMainItemGrp.Value, "", isButtonClicked)
        FillMainItemGrid()
    End Sub

    Private Sub txtUnitCode__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtUnitCode._MYValidating
        qry = "select Unit_Code as Code,Unit_Desc as Description from TSPL_UNIT_MASTER "
        Dim whrCls As String = " unit_code in (select uom_code from TSPL_ITEM_UOM_DETAIL where item_code in (select item_code from tspl_item_master where csa_type='" + txtMainItemGrp.Value + "'))"
        txtUnitCode.Value = clsCommon.ShowSelectForm("UOMFinder@SCHMMD", qry, "Code", whrCls, txtUnitCode.Value, "", isButtonClicked)
    End Sub

    Private Sub txtCriteria__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtCriteria._MYValidating
        If clsCommon.CompairString(ddlCriteria.SelectedValue, "Customer Group") = CompairStringResult.Equal Then
            qry = "Select Cust_Group_Code as Code, Cust_Group_Desc as Description from TSPL_CUSTOMER_GROUP_MASTER"
            txtCriteria.Value = clsCommon.ShowSelectForm("CriteriaFinder@SCHMMD", qry, "Code", "", txtCriteria.Value, "", isButtonClicked)
            lblCriteria.Text = clsDBFuncationality.getSingleValue("Select Cust_Group_Desc from TSPL_CUSTOMER_GROUP_MASTER WHERE Cust_Group_Code ='" + txtCriteria.Value + "'")
        ElseIf clsCommon.CompairString(ddlCriteria.SelectedValue, "Customer Category") = CompairStringResult.Equal Then
            qry = "Select CUST_CATEGORY_CODE as Code, CUST_CATEGORY_DESC as Description from TSPL_CUSTOMER_CATEGORY_MASTER"
            txtCriteria.Value = clsCommon.ShowSelectForm("CriteriaFinder@SCHMMD", qry, "Code", "", txtCriteria.Value, "", isButtonClicked)
            lblCriteria.Text = clsDBFuncationality.getSingleValue("Select CUST_CATEGORY_DESC from TSPL_CUSTOMER_CATEGORY_MASTER WHERE CUST_CATEGORY_CODE ='" + txtCriteria.Value + "'")
        ElseIf clsCommon.CompairString(ddlCriteria.SelectedValue, "State") = CompairStringResult.Equal Then
            qry = "select distinct TSPL_CUSTOMER_MASTER.State as Code,TSPL_STATE_MASTER.STATE_NAME as Description from TSPL_CUSTOMER_MASTER left outer join TSPL_STATE_MASTER on TSPL_CUSTOMER_MASTER.State=TSPL_STATE_MASTER.STATE_CODE  "
            txtCriteria.Value = clsCommon.ShowSelectForm("CriteriaFinder@SCHMMD", qry, "Code", "isnull(State,'') <> ''", txtCriteria.Value, "", isButtonClicked)
            lblCriteria.Text = clsDBFuncationality.getSingleValue("Select STATE_NAME from TSPL_STATE_MASTER WHERE STATE_CODE ='" + txtCriteria.Value + "'")
        End If
        FillCustomerGrid(txtCriteria.Value)
    End Sub

    Private Sub fillMRP()
        Try
            IsMRPLoad = True
            qry = "select distinct Item_Basic_Net  from TSPL_ITEM_PRICE_MASTER where Item_Code = '" + txtMainItemGrp.Value + "' AND Uom='" + txtUnitCode.Value + "'"
            ddlmrp.DataSource = clsDBFuncationality.GetDataTable(qry)
            ddlmrp.ValueMember = "Item_Basic_Net"
            ddlmrp.DisplayMember = "Item_Basic_Net"
            IsMRPLoad = False
            FillBasicPrice()
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub

    Private Sub ddlCriteria_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.Data.PositionChangedEventArgs) Handles ddlCriteria.SelectedIndexChanged
        Try
            If Not isFromLoad Then
                txtCriteria.Value = ""
                lblCriteria.Text = ""
                If clsCommon.CompairString(ddlCriteria.SelectedValue, "Customer Group") = CompairStringResult.Equal OrElse clsCommon.CompairString(ddlCriteria.SelectedValue, "State") = CompairStringResult.Equal OrElse clsCommon.CompairString(ddlCriteria.SelectedValue, "Customer Category") = CompairStringResult.Equal Then
                    txtCriteria.Enabled = True
                    LoadBlankCustomerGrid()
                    ''RICHA AGARWAL 13 DEC,2016
                    gvCustomer.Rows.AddNew()
                    LoadBeneficiaryaccToCriteria()
                    ''--------------
                Else
                    txtCriteria.Enabled = False
                    LoadBlankCustomerGrid()
                    gvCustomer.Rows.AddNew()
                    ''RICHA AGARWAL 13 DEC,2016
                    LoadBeneficiaryaccToCriteria()
                    ''--------------
                End If
                loadBlankGridRange()
                gvTS.Rows.AddNew()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    ''RICHA AGARWAL 13 DEC,2016
    Sub LoadBeneficiaryaccToCriteria()
        If clsCommon.CompairString(ddlCriteria.SelectedValue, "Customer Group") = CompairStringResult.Equal Then
            qry = "Select Cast(Case When ISNULL(XXX.Cust_Code,'')<>'' Then 1 Else 0 End as Bit) As [Select], XXX.Scheme_Code, TSPL_CUSTOMER_MASTER.Cust_Code, TSPL_CUSTOMER_MASTER.Customer_Name from TSPL_CUSTOMER_MASTER LEFT OUTER JOIN ( Select TSPL_SCHEME_BENEFICIARY.Cust_Code, TSPL_SCHEME_BENEFICIARY.Scheme_Code from TSPL_SCHEME_BENEFICIARY WHERE Scheme_Code='" + clsCommon.myCstr(fndScheme.Value) + "') XXX ON TSPL_CUSTOMER_MASTER.Cust_Code=XXX.Cust_Code WHERE TSPL_CUSTOMER_MASTER.Cust_Group_Code='" + clsCommon.myCstr(ddlCriteria.SelectedValue) + "'"
        ElseIf clsCommon.CompairString(ddlCriteria.SelectedValue, "Customer Category") = CompairStringResult.Equal Then
            qry = "Select Cast(Case When ISNULL(XXX.Cust_Code,'')<>'' Then 1 Else 0 End as Bit) As [Select], XXX.Scheme_Code, TSPL_CUSTOMER_MASTER.Cust_Code, TSPL_CUSTOMER_MASTER.Customer_Name from TSPL_CUSTOMER_MASTER LEFT OUTER JOIN ( Select TSPL_SCHEME_BENEFICIARY.Cust_Code, TSPL_SCHEME_BENEFICIARY.Scheme_Code from TSPL_SCHEME_BENEFICIARY WHERE Scheme_Code='" + clsCommon.myCstr(fndScheme.Value) + "') XXX ON TSPL_CUSTOMER_MASTER.Cust_Code=XXX.Cust_Code WHERE TSPL_CUSTOMER_MASTER.Cust_Category_Code='" + clsCommon.myCstr(ddlCriteria.SelectedValue) + "'"
        Else
            qry = "Select Cast(1 as bit) as [Select], TSPL_SCHEME_BENEFICIARY.Scheme_Code, TSPL_SCHEME_BENEFICIARY.Cust_Code, TSPL_CUSTOMER_MASTER.Customer_Name from TSPL_SCHEME_BENEFICIARY LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SCHEME_BENEFICIARY.Cust_Code WHERE TSPL_SCHEME_BENEFICIARY.Scheme_Code = '" + clsCommon.myCstr(fndScheme.Value) + "'"
        End If

        dt = New DataTable()
        dt = clsDBFuncationality.GetDataTable(qry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            Dim LineNo As Integer = 0
            For Each dr As DataRow In dt.Rows
                LineNo += 1
                gvCustomer.CurrentRow.Cells(colLineNo).Value = LineNo
                gvCustomer.CurrentRow.Cells(colSelect).Value = clsCommon.myCdbl(dr("Select"))
                gvCustomer.CurrentRow.Cells(colCustCode).Value = clsCommon.myCstr(dr("Cust_Code"))
                gvCustomer.CurrentRow.Cells(colCustName).Value = clsCommon.myCstr(dr("Customer_Name"))
                gvCustomer.Rows.AddNew()
            Next
        End If
    End Sub
    ''--------------------

    Private Sub ddlType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.Data.PositionChangedEventArgs) Handles ddlType.SelectedIndexChanged
        Try
            If isInsideLoadData = False Then
                txtAmount.Text = ""
                txtPercentage.Text = ""
                LoadBlankItemGrid()
                If Not isFromLoad Then
                    GrpQuantiiveType.Visible = False
                    btn_Apply.Visible = False
                    cboQuantitiveType.SelectedValue = 0
                    If clsCommon.CompairString(ddlType.SelectedValue, "Quantitive") = CompairStringResult.Equal OrElse clsCommon.CompairString(ddlType.SelectedValue, "Cash") = CompairStringResult.Equal OrElse clsCommon.CompairString(ddlType.SelectedValue, "Discount") = CompairStringResult.Equal Then
                        If clsCommon.CompairString(ddlType.SelectedValue, "Quantitive") = CompairStringResult.Equal Then
                            GrpQuantiiveType.Visible = True
                        End If
                        FillMainItemGrid()
                        txtUnitCode.Visible = False
                        txtQty.Visible = False
                        lblUnit.Visible = False
                        lblQty.Visible = False

                        MyLabel1.Visible = False
                        txtPercentage.Visible = False
                        txtPercentage.Enabled = False
                        RadLabel10.Visible = False
                        txtAmount.Visible = False
                        txtAmount.Enabled = False
                        If clsCommon.CompairString(ddlType.SelectedValue, "Cash") = CompairStringResult.Equal Then
                            btn_Apply.Visible = True
                            lblUnit.Visible = True
                            txtUnitCode.Visible = True
                            txtUnitCode.Enabled = True
                            lblQty.Visible = True
                            txtQty.Visible = True
                            txtQty.Enabled = True
                            MyLabel1.Visible = True
                            txtPercentage.Visible = True
                            txtPercentage.Enabled = True
                            RadLabel10.Visible = True
                            txtAmount.Visible = True
                            txtAmount.Enabled = True
                        End If
                    ElseIf clsCommon.CompairString(ddlType.SelectedValue, "Volume") = CompairStringResult.Equal Then
                        FillMainItemGrid()

                        txtUnitCode.Visible = True
                        txtQty.Visible = True
                        lblUnit.Visible = True
                        lblQty.Visible = True

                        MyLabel1.Visible = False
                        txtPercentage.Visible = False
                        txtPercentage.Enabled = False
                        RadLabel10.Visible = False
                        txtAmount.Visible = False
                        txtAmount.Enabled = False
                    ElseIf clsCommon.CompairString(ddlType.SelectedValue, "MaxLimit") = CompairStringResult.Equal Then
                        LoadBlankItemGrid()
                        FillMainItemGrid()
                        txtUnitCode.Visible = False
                        txtQty.Visible = False
                        lblUnit.Visible = False
                        lblQty.Visible = False
                        txtFromDate.Visible = True
                        txtToDate.Visible = True
                        lblFromDate.Visible = True
                        lblToDate.Visible = True

                        MyLabel1.Visible = False
                        txtPercentage.Visible = False
                        txtPercentage.Enabled = False
                        RadLabel10.Visible = False
                        txtAmount.Visible = False
                        txtAmount.Enabled = False
                    ElseIf clsCommon.CompairString(ddlType.SelectedValue, "VolumeSlab") = CompairStringResult.Equal Then
                        LoadBlankItemGrid()
                        loadBlankVolumeslabGrid()
                        RadPageView1.Pages("VolumeSlab").Item.Visibility = ElementVisibility.Visible
                        RadPageView1.SelectedPage = VolumeSlab
                        gvVolumeSlab.Rows.AddNew()
                    ElseIf clsCommon.CompairString(ddlType.SelectedValue, "Fixed") = CompairStringResult.Equal Then
                        LoadBlankItemGrid()
                        setFixedColumns()
                        gvItem.Rows.AddNew()
                        txtUnitCode.Visible = False
                        txtQty.Visible = False
                        lblUnit.Visible = False
                        lblQty.Visible = False
                        txtFromDate.Visible = True
                        txtToDate.Visible = True
                        lblFromDate.Visible = True
                        lblToDate.Visible = True
                        GrpQuantiiveType.Visible = False

                        MyLabel1.Visible = False
                        txtPercentage.Visible = False
                        txtPercentage.Enabled = False
                        RadLabel10.Visible = False
                        txtAmount.Visible = False
                        txtAmount.Enabled = False
                    ElseIf clsCommon.CompairString(ddlType.SelectedValue, "VolumeSlab_Cash") = CompairStringResult.Equal Then
                        LoadBlankItemGrid()
                        LoadBlankVolumeSlabCashDisGrid()
                        FillMainItemGrid()
                        RadPageView1.Pages("RadPageViewPage4").Item.Visibility = ElementVisibility.Visible
                        RadPageView1.SelectedPage = RadPageViewPage4
                        'gvVolumeSlab.Rows.AddNew()
                    End If
                    If clsCommon.CompairString(ddlType.SelectedValue, "Quantitive") = CompairStringResult.Equal Then
                    ElseIf clsCommon.CompairString(ddlType.SelectedValue, "Cash") = CompairStringResult.Equal Then
                    ElseIf clsCommon.CompairString(ddlType.SelectedValue, "Structure") = CompairStringResult.Equal Then
                        SplitContainer3.Panel2Collapsed = True
                    End If
                    If clsCommon.CompairString(clsCommon.myCstr(ddlType.SelectedValue), "Target") = CompairStringResult.Equal Then
                        FillMainItemGrid()

                        txtUnitCode.Visible = False
                        txtQty.Visible = False
                        lblUnit.Visible = False
                        lblQty.Visible = False

                        MyLabel1.Visible = False
                        txtPercentage.Visible = False
                        txtPercentage.Enabled = False
                        RadLabel10.Visible = False
                        txtAmount.Visible = False
                        txtAmount.Enabled = False

                        lblTargetType.Visible = True
                        ddlTargetType.Visible = True
                        LoadTargetTypes()
                    Else
                        lblTargetType.Visible = False
                        ddlTargetType.Visible = False

                    End If
                End If
            End If
            SplitContainer3.Panel2Collapsed = False
            If clsCommon.CompairString(clsCommon.myCstr(ddlType.SelectedValue), "Structure") = CompairStringResult.Equal Then
                SplitContainer3.Panel2Collapsed = True
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub ddlmrp_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.Data.PositionChangedEventArgs) Handles ddlmrp.SelectedIndexChanged
        If Not IsMRPLoad Then
            FillBasicPrice()
        End If
    End Sub

    Private Sub FillBasicPrice()
        If clsCommon.myLen(ddlmrp.SelectedValue) > 0 Then
            qry = "select distinct Item_Basic_Price  from TSPL_ITEM_PRICE_MASTER where Item_Code = '" + txtMainItemGrp.Value + "' AND Uom='" + txtUnitCode.Value + "' AND Item_Basic_Net='" + clsCommon.myCstr(ddlmrp.SelectedValue) + "'"
            ddlBasicPrice.DataSource = clsDBFuncationality.GetDataTable(qry)
            ddlBasicPrice.ValueMember = "Item_Basic_Price"
            ddlBasicPrice.DisplayMember = "Item_Basic_Price"
        End If
    End Sub

    Private Sub FillCustomerGrid(ByVal strCriteria As String)
        Try
            gvCustomer.DataSource = Nothing
            gvCustomer.Rows.Clear()
            gvCustomer.Columns.Clear()
            If clsCommon.CompairString(ddlCriteria.SelectedValue, "Customer Group") = CompairStringResult.Equal Then
                qry = "Select ROW_NUMBER() Over (order by Cust_Code) as [LineNo], Cast(1 as bit) as [Select], Cust_Code as [Customer Code], Customer_Name as [Customer Name] from TSPL_CUSTOMER_MASTER WHERE Status='N' and Cust_Group_Code='" + strCriteria + "'"
                LoadCustomerData(clsDBFuncationality.GetDataTable(qry))
            ElseIf clsCommon.CompairString(ddlCriteria.SelectedValue, "Customer Category") = CompairStringResult.Equal Then
                qry = "select ROW_NUMBER() Over (order by Cust_Code) as [LineNo], Cast(1 as bit) as [Select], Cust_Code as [Customer Code], Customer_Name as [Customer Name] from TSPL_CUSTOMER_MASTER WHERE Status='N' and Cust_Category_Code='" + strCriteria + "'"
                LoadCustomerData(clsDBFuncationality.GetDataTable(qry))
            ElseIf clsCommon.CompairString(ddlCriteria.SelectedValue, "State") = CompairStringResult.Equal Then
                qry = "select ROW_NUMBER() Over (order by Cust_Code) as [LineNo], Cast(1 as bit) as [Select], Cust_Code as [Customer Code], Customer_Name as [Customer Name] from TSPL_CUSTOMER_MASTER WHERE Status='N' and State='" + strCriteria + "'"
                LoadCustomerData(clsDBFuncationality.GetDataTable(qry))
            Else
            End If
            If gvCustomer.Rows.Count > 0 Then
                gvCustomer.Columns("LineNo").Width = 100
                gvCustomer.Columns("Select").Width = 100
                gvCustomer.Columns("Customer Code").Width = 150
                gvCustomer.Columns("Customer Name").Width = 300
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub FillMainItemGrid()
        Try
            LoadBlankItemGrid()
            If clsCommon.myLen(txtMainItemGrp.Value) > 0 Then
                If clsCommon.CompairString(ddlType.SelectedValue, "Discount") = CompairStringResult.Equal Then
                    qry = "Select ROW_NUMBER() Over (order by TSPL_ITEM_MASTER.Item_Code) as [LineNo], 1 as [Select], TSPL_ITEM_MASTER.Item_Code as Code, Item_Desc as Description,tspl_item_uom_detail.UOM_Code as Unit,TSPL_ITEM_MASTER.Structure_Code,TSPL_STRUCTURE_MASTER.Structure_Descq from " & _
                        "TSPL_ITEM_MASTER left outer join tspl_item_uom_detail on " & _
               "TSPL_ITEM_MASTER.item_code=tspl_item_uom_detail.item_code and tspl_item_uom_detail.Default_UOM=1 left outer join TSPL_STRUCTURE_MASTER on TSPL_ITEM_MASTER.Structure_Code=TSPL_STRUCTURE_MASTER.Structure_Code  where  CSA_TYPE='" & txtMainItemGrp.Value & "'"

                    If AllowSchemeItems Then
                        qry += " and coalesce(is_scheme_item,0)=0 "
                    End If

                Else
                    qry = "Select ROW_NUMBER() Over (order by Item_Code) as [LineNo], 1 as [Select], Item_Code as Code, Item_Desc as Description,TSPL_ITEM_MASTER.Structure_Code,TSPL_STRUCTURE_MASTER.Structure_Descq from TSPL_ITEM_MASTER left outer join TSPL_STRUCTURE_MASTER on TSPL_ITEM_MASTER.Structure_Code=TSPL_STRUCTURE_MASTER.Structure_Code  where  CSA_TYPE='" & txtMainItemGrp.Value & "'"
                    If AllowSchemeItems Then
                        qry += " and coalesce(is_scheme_item,0)=0 "
                    End If
                End If
                dt = New DataTable()
                dt = clsDBFuncationality.GetDataTable(qry)
                If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                    For Each dr As DataRow In dt.Rows
                        gvItem.Rows.AddNew()
                        gvItem.Rows(gvItem.Rows.Count - 1).Cells(colLineNo).Value = clsCommon.myCdbl(dr("LineNo"))
                        If clsCommon.myCstr(dr("Select")) = "1" Then
                            gvItem.Rows(gvItem.Rows.Count - 1).Cells(colISelect).Value = True
                        Else
                            gvItem.Rows(gvItem.Rows.Count - 1).Cells(colISelect).Value = False
                        End If
                        gvItem.Rows(gvItem.Rows.Count - 1).Cells(colMainICode).Value = clsCommon.myCstr(dr("Code"))
                        gvItem.Rows(gvItem.Rows.Count - 1).Cells(colMainIName).Value = clsCommon.myCstr(dr("Description"))
                        If clsCommon.CompairString(ddlType.SelectedValue, "Discount") = CompairStringResult.Equal Then
                            gvItem.Rows(gvItem.Rows.Count - 1).Cells(colMainIUnit).Value = clsCommon.myCstr(dr("Unit"))
                            gvItem.Rows(gvItem.Rows.Count - 1).Cells(colMainIQty).Value = "1"
                        End If
                        gvItem.Rows(gvItem.Rows.Count - 1).Cells(colMainIStructureCode).Value = clsCommon.myCstr(dr("Structure_Code"))
                        gvItem.Rows(gvItem.Rows.Count - 1).Cells(colMainIStructureDesc).Value = clsCommon.myCstr(dr("Structure_Descq"))
                    Next

                    btnItemSelect.Text = "UnSelect All"

                    gvItem.ShowFilteringRow = True
                End If

            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub LoadCustomerData(ByVal dt As DataTable)
        Try
            gvCustomer.DataSource = dt
            If dt.Rows.Count > 0 Then
                btnSelect.Text = "UnSelect All"
            Else
                btnSelect.Text = "Select All"
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub fndScheme__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndScheme._MYValidating
        fndScheme.Value = clsSchemeMasterDairy.getFinder("", fndScheme.Value, isButtonClicked)
        LoadData(fndScheme.Value, NavigatorType.Current)
    End Sub

    Private Sub fndScheme__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles fndScheme._MYNavigator
        Try
            qry = "select count(*) from TSPL_SCHEME_MASTER_NEW where Scheme_Code='" + fndScheme.Value + "'"
            Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
            If count = 0 Then
                fndScheme.MyReadOnly = False
            Else
                fndScheme.MyReadOnly = True
            End If
            LoadData(fndScheme.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Sub LoadData(ByVal strSecCustomerCode As String, ByVal NavType As NavigatorType)
        Try
            Reset()

            Dim obj As New clsSchemeMasterDairy
            obj = clsSchemeMasterDairy.GetData(strSecCustomerCode, NavType)
            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Scheme_Code) > 0 Then
                isInsideLoadData = True
                txtFromDate.Value = obj.MaxlimitStart_Date
                txtToDate.Value = obj.MaxlimitEnd_Date
                fndScheme.Value = obj.Scheme_Code
                txtDesc.Text = obj.Scheme_Desc
                dtpScheme.Value = obj.Start_Date
                If clsCommon.CompairString(obj.Scheme_Type, "VS_CASH") = CompairStringResult.Equal Then
                    ddlType.SelectedValue = "VolumeSlab_Cash"
                Else
                    ddlType.SelectedValue = obj.Scheme_Type
                End If

                ' txtMainItemGrp.Value = "NotMenditory"
                txtMainItemGrp.Value = obj.Item_Code
                lblMainItemDesc.Text = obj.Item_Desc
                txtUnitCode.Value = obj.Unit_Code
                lblUnitCodeDesc.Text = obj.Unit_Desc
                txtQty.Text = obj.Item_Qty
                fillMRP()
                '' Anubhooti 07-Oct-2014 BM00000004181 (MRP value should be fetched using selectedvalue not from text property)
                ddlmrp.SelectedValue = obj.MRP
                ddlBasicPrice.Text = obj.Basic_Price
                txtPercentage.Text = obj.Percentage
                txtAmount.Text = obj.Amount
                chkInactive.Checked = IIf(clsCommon.CompairString(obj.Status, "InActive") = CompairStringResult.Equal, True, False)
                If chkInactive.Checked Then
                    dtpInactive.Value = obj.End_Date
                    dtpInactive.Enabled = True
                Else
                    dtpInactive.Enabled = False
                End If
                ddlCriteria.SelectedValue = obj.Criteria
                txtCriteria.Value = obj.Criteria_Code
                lblCriteria.Text = obj.Criteria_Desc
                txtComment.Text = obj.Comments
                txtStrcutCode.Value = obj.Structure_Code
                txtstructUnit.Value = obj.Structure_Unit
                lblStructDesc.Text = clsDBFuncationality.getSingleValue("Select Structure_Descq from tspl_structure_master WHERE Structure_Code ='" + txtStrcutCode.Value + "'")
                lblStructUnit.Text = clsDBFuncationality.getSingleValue("Select unit_desc from TSPL_UNIT_MASTER WHERE unit_code ='" + txtstructUnit.Value + "'")

                If (obj.Target_Sub_Type <> "") Then
                    lblTargetType.Visible = True
                    ddlTargetType.Visible = True
                    ddlTargetType.SelectedValue = obj.Target_Sub_Type
                End If
                If clsCommon.CompairString(ddlType.SelectedValue, "Quantitive") = CompairStringResult.Equal OrElse clsCommon.CompairString(ddlType.SelectedValue, "Discount") = CompairStringResult.Equal Then

                ElseIf clsCommon.CompairString(ddlType.SelectedValue, "Cash") = CompairStringResult.Equal Then
                    btn_Apply.Visible = True
                    lblUnit.Visible = True
                    txtUnitCode.Visible = True
                    txtUnitCode.Enabled = True
                    lblQty.Visible = True
                    txtQty.Visible = True
                    txtQty.Enabled = True
                    MyLabel1.Visible = True
                    txtPercentage.Visible = True
                    txtPercentage.Enabled = True
                    RadLabel10.Visible = True
                    txtAmount.Visible = True
                    txtAmount.Enabled = True
                ElseIf clsCommon.CompairString(ddlType.SelectedValue, "Volume") = CompairStringResult.Equal Then
                    txtUnitCode.Visible = True
                    txtQty.Visible = True
                    lblUnit.Visible = True
                    lblQty.Visible = True
                ElseIf clsCommon.CompairString(ddlType.SelectedValue, "MaxLimit") = CompairStringResult.Equal Then
                    txtFromDate.Visible = True
                    txtToDate.Visible = True
                    lblFromDate.Visible = True
                    lblToDate.Visible = True
                End If
                ''For Custom Fields

                If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
                    UcCustomFields1.GetData(obj.arrCustomFields)
                End If
                ''End of For Custom Fields
                'KUNAL > TICKET : BM00000010252 : UDL > DATE : 28 -11 -2016

                cboQuantitiveType.SelectedValue = obj.Quantitive_Type
                txtQuantitiveStructureCode.arrValueMember = obj.ArrQuantitiveStructureCode
                txtQuantitiveStructureMainQty.Value = obj.Quantitive_Type_Structure_Main_Qty
                txtQuantitiveStructureMainUOM.Value = obj.Quantitive_Type_Structure_Main_UOM
                txtQuantitiveStructureFreeICode.Value = obj.Quantitive_Type_Structure_Free_Item
                txtQuantitiveStructureFreeQty.Value = obj.Quantitive_Type_Structure_Free_Qty
                txtQuantitiveStructureFreeUOM.Value = obj.Quantitive_Type_Structure_Free_UOM

                Dim LineNo As Integer = 0
                LoadBlankCustomerGrid()
                loadBlankGridRange()
                For Each objDTL As clsSchemeDetailDairy In obj.ArrDTL

                    LineNo += 1

                    gvItem.CurrentRow.Cells(colLineNo).Value = LineNo
                    gvItem.CurrentRow.Cells(colISelect).Value = IIf(objDTL.ColSelect = 1, True, False)
                    gvItem.CurrentRow.Cells(colICode).Value = objDTL.Item_Code
                    gvItem.CurrentRow.Cells(colIName).Value = objDTL.Item_Desc
                    gvItem.CurrentRow.Cells(colQty).Value = objDTL.Qty
                    gvItem.CurrentRow.Cells(colUOM).Value = objDTL.Unit_Code
                    gvItem.CurrentRow.Cells(colMRP).Value = objDTL.MRP
                    gvItem.CurrentRow.Cells(colPriceDate).Value = objDTL.Price_Date
                    gvItem.CurrentRow.Cells(colRemarks).Value = objDTL.Remarks
                    gvItem.CurrentRow.Cells(colMainICode).Value = objDTL.MainItem_Code
                    gvItem.CurrentRow.Cells(colMainIName).Value = objDTL.MainItem_Desc
                    gvItem.CurrentRow.Cells(colMainIQty).Value = objDTL.MainQty
                    gvItem.CurrentRow.Cells(colMainIUnit).Value = objDTL.MainUnit_Code
                    gvItem.CurrentRow.Cells(colMainIAmt).Value = objDTL.Amount
                    gvItem.CurrentRow.Cells(colCashAmt).Value = objDTL.CashDisc_Amount
                    gvItem.CurrentRow.Cells(colCashPer).Value = objDTL.CasdDisc_Percentage
                    gvItem.CurrentRow.Cells(colMainIStructureCode).Value = objDTL.MainItemStrCode
                    gvItem.CurrentRow.Cells(colMainIStructureDesc).Value = objDTL.MainItemStrDesc
                    gvItem.CurrentRow.Cells(colSchemeItemStrCode).Value = objDTL.SchemeItemStrCode
                    gvItem.CurrentRow.Cells(colSchemeItemStrDesc).Value = objDTL.SchemeItemStrDesc

                    gvItem.CurrentRow.Cells(colMaxLimit).Value = objDTL.Max_Limit
                    gvItem.CurrentRow.Cells(colIncrementValue).Value = objDTL.Increment_Value

                    gvItem.Rows.AddNew()
                Next
                setFixedColumns()
                gvItem.Rows.RemoveAt(LineNo)
                gvItem.AllowAddNewRow = False
                LineNo = 0
                For Each objSchmBen As clsSchemeBenificiaryDairy In obj.ArrSchmBen
                    gvCustomer.Rows.AddNew()
                    LineNo += 1
                    gvCustomer.CurrentRow.Cells(colLineNo).Value = LineNo
                    gvCustomer.CurrentRow.Cells(colSelect).Value = objSchmBen.check
                    gvCustomer.CurrentRow.Cells(colCustCode).Value = objSchmBen.Cust_Code
                    gvCustomer.CurrentRow.Cells(colCustName).Value = objSchmBen.Customer_Name
                Next

                chkSlabWise.Checked = obj.Apply_Slab
                ChkQuantativeSch.Checked = obj.Quantative_Scheme_In_Slab
                If obj.Apply_Slab AndAlso obj.Quantative_Scheme_In_Slab AndAlso obj.arrSLAB IsNot Nothing AndAlso obj.arrSLAB.Count > 0 Then
                    For Each objts As clsSchemeMasterDairySlab In obj.arrSLAB
                        gvTS2.Rows.AddNew()
                        gvTS2.Rows(gvTS2.Rows.Count - 1).Cells(colRange).Value = objts.Min_Range
                        If gvTS2.Rows.Count > 1 Then
                            gvTS2.Rows(gvTS2.Rows.Count - 2).Cells(colTo).Value = clsCommon.myCdbl(clsCommon.myCdbl(objts.Min_Range) - 1)
                        End If
                        gvTS2.Rows(gvTS2.Rows.Count - 1).Cells(colRate).Value = objts.Value
                    Next
                    If gvTS2.Rows.Count > 1 Then
                        gvTS2.Rows(gvTS2.Rows.Count - 1).Cells(colTo).Value = 5000
                    End If

                    For Each objQts As clsSchemeMasterDairyQuantativeSlab In obj.arrQuantativeSLAB
                        gvTS.Rows.AddNew()
                        gvTS.Rows(gvTS.Rows.Count - 1).Cells(colRange).Value = objQts.Min_Range
                        If gvTS.Rows.Count > 1 Then
                            gvTS.Rows(gvTS.Rows.Count - 2).Cells(colTo).Value = clsCommon.myCdbl(clsCommon.myCdbl(objQts.Min_Range) - 1)
                        End If
                        gvTS.Rows(gvTS.Rows.Count - 1).Cells(colRate).Value = objQts.Value
                    Next
                    If gvTS.Rows.Count > 1 Then
                        gvTS.Rows(gvTS.Rows.Count - 1).Cells(colTo).Value = 5000
                    End If

                ElseIf obj.Apply_Slab AndAlso obj.arrSLAB IsNot Nothing AndAlso obj.arrSLAB.Count > 0 Then

                    For Each objts As clsSchemeMasterDairySlab In obj.arrSLAB
                        gvTS.Rows.AddNew()
                        gvTS.Rows(gvTS.Rows.Count - 1).Cells(colRange).Value = objts.Min_Range
                        If gvTS.Rows.Count > 1 Then
                            gvTS.Rows(gvTS.Rows.Count - 2).Cells(colTo).Value = clsCommon.myCdbl(clsCommon.myCdbl(objts.Min_Range) - 1)
                        End If
                        gvTS.Rows(gvTS.Rows.Count - 1).Cells(colRate).Value = objts.Value
                    Next
                    If gvTS.Rows.Count > 1 Then
                        gvTS.Rows(gvTS.Rows.Count - 1).Cells(colTo).Value = 5000
                    End If
                End If

                txtQuantum.Value = CInt(obj.Quantum)

                If clsCommon.CompairString(ddlType.SelectedValue, "VolumeSlab") = CompairStringResult.Equal Then
                    loadBlankVolumeslabGrid()
                    LoadBeneficiaryaccToCriteria()

                    For Each objVS As clsSchemeMasterVolumeSlab In obj.arrVolumeSLAB
                        gvVolumeSlab.Rows.AddNew()
                        gvVolumeSlab.Rows(gvVolumeSlab.Rows.Count - 1).Cells(colRange).Value = objVS.Min_Range
                        gvVolumeSlab.Rows(gvVolumeSlab.Rows.Count - 1).Cells(colTo).Value = objVS.Max_Range
                        gvVolumeSlab.Rows(gvVolumeSlab.Rows.Count - 1).Cells(colICode).Value = objVS.Item_Code
                        gvVolumeSlab.Rows(gvVolumeSlab.Rows.Count - 1).Cells(colIName).Value = clsItemMaster.GetItemName(objVS.Item_Code, Nothing)

                        gvVolumeSlab.Rows(gvVolumeSlab.Rows.Count - 1).Cells(colUOM).Value = objVS.Unit_Code
                        gvVolumeSlab.Rows(gvVolumeSlab.Rows.Count - 1).Cells(colQty).Value = objVS.Qty
                    Next
                End If

                txtRangeUnit.Value = obj.CASHDISVOL_RANGE_UOM
                TxtCashDisunit.Value = obj.CASHDISVOL_UOM
                lblCashDisunit.Text = clsDBFuncationality.getSingleValue("Select unit_desc from TSPL_UNIT_MASTER WHERE unit_code ='" + TxtCashDisunit.Value + "'")
                LineNo = 0
                For Each objIncentiveDetail As clsCashDiscountVolumneDetail In obj.ArrCashDiscountVolumneDetails
                    LineNo += 1
                    gvCashDisGrid.CurrentRow.Cells(colInLineNo).Value = LineNo
                    gvCashDisGrid.CurrentRow.Cells(colFromRange).Value = objIncentiveDetail.FROM_RANGE
                    gvCashDisGrid.CurrentRow.Cells(colToRange).Value = objIncentiveDetail.TO_RANGE
                    gvCashDisGrid.CurrentRow.Cells(colRangeUom).Value = obj.CASHDISVOL_RANGE_UOM
                    gvCashDisGrid.CurrentRow.Cells(colCashDisAmt).Value = objIncentiveDetail.Discount
                    gvCashDisGrid.CurrentRow.Cells(colIncentiveUom).Value = obj.CASHDISVOL_UOM
                    gvCashDisGrid.Rows.AddNew()
                Next

                txtItemSturcture.arrValueMember = obj.ArrCashDisSturctureMapping



                isNewEntry = False
                btnSave.Text = "Update"
                isInsideLoadData = False

                '= KUNAL > TICKET : BM00000009590 > DATE : 20-09-2016 ==============
                If strSecCustomerCode IsNot Nothing AndAlso strSecCustomerCode.Length > 0 Then
                    UcAttachment1.LoadData(strSecCustomerCode)
                End If

                gvTS.Rows.AddNew()
            Else
                isNewEntry = True
                btnSave.Text = "Save"
            End If
            If clsCommon.CompairString(ddlType.SelectedValue, "VolumeSlab") = CompairStringResult.Equal Then
                LoadBlankItemGrid()
                RadPageView1.Pages("VolumeSlab").Item.Visibility = ElementVisibility.Visible
                RadPageView1.SelectedPage = VolumeSlab
            ElseIf clsCommon.CompairString(ddlType.SelectedValue, "VolumeSlab_Cash") = CompairStringResult.Equal Then
                LoadBlankItemGrid()
                RadPageView1.Pages("RadPageViewPage4").Item.Visibility = ElementVisibility.Visible
                RadPageView1.SelectedPage = RadPageViewPage4
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            SaveData()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Function AllowToSave() As Boolean
        Dim linno As Integer = 0
        If clsCommon.myLen(txtDesc.Text) <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please enter Scheme Description")
            txtDesc.Focus()
            Return False
        ElseIf clsCommon.CompairString(ddlType.SelectedValue, "Quantitive") = CompairStringResult.Equal Then

        ElseIf clsCommon.CompairString(ddlType.SelectedValue, "Volume") = CompairStringResult.Equal Then
            If clsCommon.myLen(txtUnitCode.Value) = 0 Then
                common.clsCommon.MyMessageBoxShow("Please enter Unit.")
                txtAmount.Focus()
                Return False
            ElseIf clsCommon.myCdbl(txtQty.Text) <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please enter Qty.")
                txtAmount.Focus()
                Return False
            End If
        ElseIf clsCommon.CompairString(ddlType.SelectedValue, "VolumeSlab") = CompairStringResult.Equal Then
            If clsCommon.myLen(txtStrcutCode.Value) = 0 Then
                common.clsCommon.MyMessageBoxShow("Please enter Structure Code.")
                txtStrcutCode.Focus()
                Return False
            ElseIf clsCommon.myLen(txtstructUnit.Value) = 0 Then
                common.clsCommon.MyMessageBoxShow("Please enter Structure Unit.")
                txtAmount.Focus()
                Return False
            End If
        ElseIf clsCommon.CompairString(ddlType.SelectedValue, "Cash") = CompairStringResult.Equal Then

        ElseIf clsCommon.CompairString(ddlType.SelectedValue, "Discount") = CompairStringResult.Equal Then

        ElseIf txtFromDate.Value.Date > txtToDate.Value.Date Then
            common.clsCommon.MyMessageBoxShow("To date can not be before than from date.")
            txtToDate.Focus()
            Return False


        ElseIf dtpScheme.Value.Date > dtpInactive.Value.Date Then
            common.clsCommon.MyMessageBoxShow("inactive date can not be before than scheme date.")
            dtpInactive.Focus()
            Return False
        End If
        For ii As Integer = 0 To gvItem.Rows.Count - 1
            Dim strMainICode As String = clsCommon.myCstr(gvItem.Rows(ii).Cells(colMainICode).Value)
            Dim strMainIName As String = clsCommon.myCstr(gvItem.Rows(ii).Cells(colMainIName).Value)
            Dim strMainUnit As String = clsCommon.myCstr(gvItem.Rows(ii).Cells(colMainIUnit).Value)
            Dim dblMainQty As String = clsCommon.myCdbl(gvItem.Rows(ii).Cells(colMainIQty).Value)
            Dim strICode As String = clsCommon.myCstr(gvItem.Rows(ii).Cells(colICode).Value)
            Dim strIName As String = clsCommon.myCstr(gvItem.Rows(ii).Cells(colIName).Value)
            Dim strUnit As String = clsCommon.myCstr(gvItem.Rows(ii).Cells(colUOM).Value)
            Dim dblQty As String = clsCommon.myCdbl(gvItem.Rows(ii).Cells(colQty).Value)
            Dim dblCashQty As String = clsCommon.myCdbl(gvItem.Rows(ii).Cells(colCashPer).Value)
            Dim dblCashAmt As String = clsCommon.myCdbl(gvItem.Rows(ii).Cells(colCashAmt).Value)
            Dim strMainIAmt As String = clsCommon.myCstr(gvItem.Rows(ii).Cells(colMainIAmt).Value)
            If clsCommon.myCBool(gvItem.Rows(ii).Cells(colISelect).Value) = True Then
                If clsCommon.CompairString(ddlType.SelectedValue, "Quantitive") = CompairStringResult.Equal Then
                    If clsCommon.myLen(strMainUnit) = 0 Then
                        common.clsCommon.MyMessageBoxShow("Please select Main item unit  for " + strMainIName + ". At Line No" + clsCommon.myCstr(ii + 1))
                        Return False
                    ElseIf clsCommon.myCdbl(dblMainQty) = 0 Then
                        common.clsCommon.MyMessageBoxShow("Please select Main item  qty for " + strMainIName + ". At Line No" + clsCommon.myCstr(ii + 1))
                        Return False
                    ElseIf clsCommon.myLen(strICode) = 0 Then
                        common.clsCommon.MyMessageBoxShow("Please select Scheme item for " + strMainIName + ". At Line No" + clsCommon.myCstr(ii + 1))
                        Return False
                    ElseIf clsCommon.myLen(strUnit) = 0 Then
                        common.clsCommon.MyMessageBoxShow("Please select Scheme item unit for " + strMainIName + ". At Line No" + clsCommon.myCstr(ii + 1))
                        Return False
                    ElseIf clsCommon.myCdbl(dblQty) = 0 Then
                        common.clsCommon.MyMessageBoxShow("Please select Scheme item qty for " + strMainIName + ". At Line No" + clsCommon.myCstr(ii + 1))
                        Return False
                    End If
                    If clsCommon.myCdbl(cboQuantitiveType.SelectedValue) = 1 Then
                        If clsCommon.myCdbl(gvItem.Rows(ii).Cells(colMaxLimit).Value) <= 0 Then
                            common.clsCommon.MyMessageBoxShow("Please Enter Max Limit of item " + strMainIName + ". At Line No" + clsCommon.myCstr(ii + 1))
                            Return False
                        End If
                    ElseIf clsCommon.myCdbl(cboQuantitiveType.SelectedValue) = 2 Then
                        If clsCommon.myCdbl(gvItem.Rows(ii).Cells(colIncrementValue).Value) <= 0 Then
                            common.clsCommon.MyMessageBoxShow("Please Enter Increment Value of item " + strMainIName + ". At Line No" + clsCommon.myCstr(ii + 1))
                            Return False
                        End If
                    Else
                        gvItem.Rows(ii).Cells(colMaxLimit).Value = 0
                        gvItem.Rows(ii).Cells(colIncrementValue).Value = 0
                    End If

                ElseIf clsCommon.CompairString(ddlType.SelectedValue, "Volume") = CompairStringResult.Equal Then
                    If clsCommon.myLen(strICode) = 0 Then
                        common.clsCommon.MyMessageBoxShow("Please select Scheme item for " + strMainIName + ". At Line No" + clsCommon.myCstr(ii + 1))
                        Return False
                    ElseIf clsCommon.myLen(strUnit) = 0 Then
                        common.clsCommon.MyMessageBoxShow("Please select Scheme item unit for " + strMainIName + ". At Line No" + clsCommon.myCstr(ii + 1))
                        Return False
                    ElseIf clsCommon.myCdbl(dblQty) = 0 Then
                        common.clsCommon.MyMessageBoxShow("Please select Scheme item qty for " + strMainIName + ". At Line No" + clsCommon.myCstr(ii + 1))
                        Return False
                    End If
                ElseIf clsCommon.CompairString(ddlType.SelectedValue, "Maxlimit") = CompairStringResult.Equal Then

                    If clsCommon.myLen(strMainUnit) = 0 Then
                        common.clsCommon.MyMessageBoxShow("Please select Main item unit  for " + strMainIName + ". At Line No" + clsCommon.myCstr(ii + 1))
                        Return False
                    ElseIf clsCommon.myCdbl(dblMainQty) = 0 Then
                        common.clsCommon.MyMessageBoxShow("Please select Main item  qty for " + strMainIName + ". At Line No" + clsCommon.myCstr(ii + 1))
                        Return False

                    End If

                ElseIf clsCommon.CompairString(ddlType.SelectedValue, "Cash") = CompairStringResult.Equal Then
                    'OrElse clsCommon.CompairString(ddlType.SelectedValue, "Discount") = CompairStringResult.Equal
                    If clsCommon.myLen(strMainUnit) = 0 Then
                        common.clsCommon.MyMessageBoxShow("Please select Main item unit  for " + strMainIName + ". At Line No" + clsCommon.myCstr(ii + 1))
                        Return False
                    ElseIf clsCommon.myCdbl(dblMainQty) = 0 Then
                        common.clsCommon.MyMessageBoxShow("Please select Main item  qty for " + strMainIName + ". At Line No" + clsCommon.myCstr(ii + 1))
                        Return False
                    ElseIf clsCommon.myCdbl(dblCashQty) <= 0 AndAlso clsCommon.myCdbl(dblCashAmt) <= 0 Then
                        common.clsCommon.MyMessageBoxShow("Please enter Cash % or Cash amt for " + strMainIName + ". At Line No" + clsCommon.myCstr(ii + 1))
                        Return False
                    End If


                    Dim UOMEXIST As Integer = 0
                    UOMEXIST = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(1) as xx from TSPL_ITEM_UOM_DETAIL where Item_Code = '" + strMainICode + "' and UOM_Code = '" + strMainUnit + "'"))
                    If UOMEXIST = 0 Then
                        strMsg = strMsg & strMainICode & " - " & strMainUnit & Environment.NewLine
                    End If

                ElseIf clsCommon.CompairString(ddlType.SelectedValue, "Target") = CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(ddlTargetType.SelectedValue), "Quantity") = CompairStringResult.Equal Then
                    If clsCommon.myLen(strMainUnit) = 0 Then
                        common.clsCommon.MyMessageBoxShow("Please select Main item unit  for " + strMainIName + ". At Line No" + clsCommon.myCstr(ii + 1))
                        Return False
                    ElseIf clsCommon.myCdbl(dblMainQty) = 0 Then
                        common.clsCommon.MyMessageBoxShow("Please select Main item  qty for " + strMainIName + ". At Line No" + clsCommon.myCstr(ii + 1))
                        Return False
                    ElseIf clsCommon.myCdbl(dblCashQty) <= 0 AndAlso clsCommon.myCdbl(dblCashAmt) <= 0 Then
                        common.clsCommon.MyMessageBoxShow("Please enter Cash % or Cash amt for " + strMainIName + ". At Line No" + clsCommon.myCstr(ii + 1))
                        Return False

                    End If

                ElseIf clsCommon.CompairString(ddlType.SelectedValue, "Target") = CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(ddlTargetType.SelectedValue), "Amount") = CompairStringResult.Equal Then
                    If clsCommon.myLen(strMainIAmt) = 0 OrElse clsCommon.CompairString(clsCommon.myCstr(strMainIAmt), "0") = CompairStringResult.Equal Then
                        common.clsCommon.MyMessageBoxShow("Please enter Amount  for " + strMainIAmt + ". At Line No" + clsCommon.myCstr(ii + 1))
                        Return False
                    ElseIf clsCommon.myCdbl(dblCashQty) <= 0 AndAlso clsCommon.myCdbl(dblCashAmt) <= 0 Then
                        common.clsCommon.MyMessageBoxShow("Please enter Cash % or Cash amt for " + strMainIName + ". At Line No" + clsCommon.myCstr(ii + 1))
                        Return False

                    End If

                End If

                Dim intCountCustomer As Integer = 0
                For kk As Integer = 0 To gvCustomer.Rows.Count - 1
                    Dim strCustomer As String = clsCommon.myCstr(gvCustomer.Rows(kk).Cells(colCustCode).Value)
                    If clsCommon.myLen(strCustomer) > 0 AndAlso gvCustomer.Rows(kk).Cells(colSelect).Value = True Then
                        Dim strSchemeExist As String = ("SELECT count(*) from TSPL_SCHEME_MASTER_NEW inner join TSPL_SCHEME_Detail_NEW on TSPL_SCHEME_MASTER_NEW.Scheme_Code=TSPL_SCHEME_Detail_NEW.Scheme_Code inner join TSPL_SCHEME_BENEFICIARY on TSPL_SCHEME_MASTER_NEW.Scheme_Code=" & _
                                                      " TSPL_SCHEME_BENEFICIARY.Scheme_Code WHERE TSPL_SCHEME_MASTER_NEW.Scheme_Code<>'" + clsCommon.myCstr(fndScheme.Value) + "' and Scheme_Type='" & ddlType.SelectedValue & "' AND TSPL_SCHEME_MASTER_NEW.Item_Code='" & txtMainItemGrp.Value & "' and Status='Active' " & _
                                                      " and MaxlimitStart_Date < = '" & clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") & "' and MaxlimitEnd_Date >= '" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") & "' and TSPL_SCHEME_BENEFICIARY.Cust_Code='" & strCustomer & "' and TSPL_SCHEME_Detail_NEW.MainItem_Code='" & strMainICode & "'")
                        Dim SchemeExist As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strSchemeExist))
                        If clsCommon.CompairString(clsCommon.myCstr(SchemeExist), "0") <> CompairStringResult.Equal AndAlso clsCommon.myLen(SchemeExist) > 0 Then
                            common.clsCommon.MyMessageBoxShow("" & ddlType.SelectedValue & " already created for From Date: " & clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") & " To Date: " & clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") & " and Item Group : " & txtMainItemGrp.Value & " and Main Item Code:" & strMainICode & " and Customer : " & strCustomer & ". First In-Active Scheme Type then create new scheme.")
                            Return False
                        End If

                        intCountCustomer += 1
                        'Dim intCount As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select 1 as count from TSPL_SCHEME_MASTER_NEW where Item_Code='" & txtMainItemGrp.Value & "' and Start_Date= '" & clsCommon.GetPrintDate(dtpScheme.Value, "dd/MMM/yyyy") & "'  and Scheme_Type='" & ddlType.SelectedValue & "' and Scheme_Code not in ('" & fndScheme.Value & "')"))
                        Dim strSchemeCode As String = clsDBFuncationality.getSingleValue("select top 1 TSPL_SCHEME_MASTER_NEW.Scheme_Code  from TSPL_SCHEME_MASTER_NEW  " & _
                         "left outer join TSPL_SCHEME_DETAIL_NEW on TSPL_SCHEME_MASTER_NEW.Scheme_Code=TSPL_SCHEME_DETAIL_NEW.Scheme_Code   left outer join " & _
                         "TSPL_SCHEME_BENEFICIARY on TSPL_SCHEME_DETAIL_NEW.Scheme_Code=TSPL_SCHEME_BENEFICIARY.Scheme_Code " & _
                         "where TSPL_SCHEME_DETAIL_NEW.MainItem_Code='" & strMainICode & "' and MainUnit_Code='" & strMainUnit & "'  " & _
                         "and TSPL_SCHEME_MASTER_NEW.Start_Date='" & clsCommon.GetPrintDate(dtpScheme.Value, "dd/MMM/yyyy") & "' and " & _
                         "Scheme_Type='" & ddlType.SelectedValue & "' and Cust_Code='" & strCustomer & "' and " & _
                         "TSPL_SCHEME_MASTER_NEW.Scheme_Code not in ('" & fndScheme.Value & "')")
                        If clsCommon.myLen(strSchemeCode) > 0 Then
                            common.clsCommon.MyMessageBoxShow("" & strSchemeCode & " already created for Date: " & clsCommon.GetPrintDate(dtpScheme.Value, "dd/MMM/yyyy") & " and Item : " & strMainICode & "  and Customer : " & strCustomer & ".")
                            Return False
                        End If

                    End If
                Next


                For jj As Integer = 0 To gvItem.Rows.Count - 1
                    Dim strInnerMainICode As String = clsCommon.myCstr(gvItem.Rows(jj).Cells(colMainICode).Value)
                    Dim strInnerMainUnit As String = clsCommon.myCstr(gvItem.Rows(jj).Cells(colMainIUnit).Value)
                    Dim strInnerICode As String = clsCommon.myCstr(gvItem.Rows(jj).Cells(colICode).Value)
                    Dim strInnerUnit As String = clsCommon.myCstr(gvItem.Rows(jj).Cells(colUOM).Value)
                    If jj = ii Then
                        Continue For
                    End If
                    If clsCommon.myCBool(gvItem.Rows(ii).Cells(colISelect).Value) = True Then
                        If clsCommon.CompairString(ddlType.SelectedValue, "Quantitive") = CompairStringResult.Equal OrElse clsCommon.CompairString(ddlType.SelectedValue, "Volume") = CompairStringResult.Equal Then
                            If clsCommon.CompairString(strMainICode, strInnerMainICode) = CompairStringResult.Equal AndAlso clsCommon.CompairString(strMainUnit, strInnerMainUnit) = CompairStringResult.Equal AndAlso clsCommon.CompairString(strICode, strInnerICode) = CompairStringResult.Equal AndAlso clsCommon.CompairString(strUnit, strInnerUnit) = CompairStringResult.Equal Then
                                Dim Msg As String = "Same scheme Exist at Row No " + clsCommon.myCstr(ii + 1) + " And " + clsCommon.myCstr(jj + 1)
                                common.clsCommon.MyMessageBoxShow(Msg)
                                Return False
                            End If
                        ElseIf clsCommon.CompairString(ddlType.SelectedValue, "Cash") = CompairStringResult.Equal OrElse clsCommon.CompairString(ddlType.SelectedValue, "Discount") = CompairStringResult.Equal Then
                            If clsCommon.CompairString(strMainICode, strInnerMainICode) = CompairStringResult.Equal AndAlso clsCommon.CompairString(strMainUnit, strInnerMainUnit) = CompairStringResult.Equal Then
                                Dim Msg As String = "Same scheme Exist at Row No " + clsCommon.myCstr(ii + 1) + " And " + clsCommon.myCstr(jj + 1)
                                common.clsCommon.MyMessageBoxShow(Msg)
                                Return False
                            End If
                        End If
                    End If

                Next
            End If
        Next

        If clsCommon.CompairString(ddlType.SelectedValue, "VolumeSlab") = CompairStringResult.Equal Then
            Dim Count As Integer = 0
            For intCount As Integer = 0 To gvVolumeSlab.Rows.Count - 1
                Dim strSCode As String = clsCommon.myCstr(gvVolumeSlab.Rows(intCount).Cells(colICode).Value)
                Dim strSName As String = clsCommon.myCstr(gvVolumeSlab.Rows(intCount).Cells(colIName).Value)
                Dim strSUnit As String = clsCommon.myCstr(gvVolumeSlab.Rows(intCount).Cells(colUOM).Value)
                Dim dblMinRange As String = clsCommon.myCstr(gvVolumeSlab.Rows(intCount).Cells(colRange).Value)
                Dim dblMaxRange As String = clsCommon.myCstr(gvVolumeSlab.Rows(intCount).Cells(colTo).Value)
                Dim dblSQty As Double = clsCommon.myCdbl(gvVolumeSlab.Rows(intCount).Cells(colQty).Value)

                If clsCommon.myCdbl(dblMinRange) > 0 AndAlso clsCommon.myCdbl(dblMaxRange) > 0 Then
                    Count += 1
                    If clsCommon.myLen(strSCode) = 0 Then
                        common.clsCommon.MyMessageBoxShow("Please select Item for Range  " + dblMinRange + " To  " + dblMaxRange + " . At Line No" + clsCommon.myCstr(intCount + 1), Me.Text)
                        Return False
                    ElseIf clsCommon.myLen(strSUnit) = 0 Then
                        common.clsCommon.MyMessageBoxShow("Please select Unit for Range  " + dblMinRange + " To  " + dblMaxRange + " . At Line No" + clsCommon.myCstr(intCount + 1), Me.Text)
                        Return False
                    ElseIf dblSQty <= 0 Then
                        common.clsCommon.MyMessageBoxShow("Please enter Qty for Range  " + dblMinRange + " To  " + dblMaxRange + " . At Line No" + clsCommon.myCstr(intCount + 1), Me.Text)
                        Return False
                    ElseIf clsCommon.myCdbl(dblMinRange) > clsCommon.myCdbl(dblMaxRange) Then
                        common.clsCommon.MyMessageBoxShow("Max Range should be greater than Min Range  " + dblMinRange + " To  " + dblMaxRange + " . At Line No" + clsCommon.myCstr(intCount + 1), Me.Text)
                        Return False
                    End If
                    If Count > 1 Then
                        Dim dblFirstMaxRange As String = clsCommon.myCstr(gvVolumeSlab.Rows(intCount - 1).Cells(colTo).Value)
                        If clsCommon.myCdbl(dblMinRange) < clsCommon.myCdbl(dblFirstMaxRange) Then
                            common.clsCommon.MyMessageBoxShow("From Range should be greater than Previos Max Range  " + dblMinRange + " To  " + dblMaxRange + " . At Line No" + clsCommon.myCstr(intCount + 1), Me.Text)
                            Return False
                        End If
                    End If
                End If
            Next
            If Count = 0 Then
                common.clsCommon.MyMessageBoxShow("Please select atleast one range for scheme volume slab ", Me.Text)
                Return False
            End If
        End If

        If clsCommon.CompairString(ddlType.SelectedValue, "Structure") = CompairStringResult.Equal Then
            If txtQuantitiveStructureCode.arrValueMember Is Nothing OrElse txtQuantitiveStructureCode.arrValueMember.Count <= 0 Then
                txtQuantitiveStructureCode.Focus()
                Throw New Exception("First select at lease one structure code")
            End If
            If txtQuantitiveStructureMainQty.Value <= 0 Then
                txtQuantitiveStructureMainQty.Focus()
                Throw New Exception("Please enter main Qty")
            End If
            If clsCommon.myLen(txtQuantitiveStructureMainUOM.Value) <= 0 Then
                txtQuantitiveStructureMainUOM.Focus()
                Throw New Exception("Please enter main Uom")
            End If
            If clsCommon.myLen(txtQuantitiveStructureFreeICode.Value) <= 0 Then
                txtQuantitiveStructureFreeICode.Focus()
                Throw New Exception("Please enter Free Item Code")
            End If
            If txtQuantitiveStructureFreeQty.Value <= 0 Then
                txtQuantitiveStructureFreeQty.Focus()
                Throw New Exception("Please enter Free Quantity")
            End If
            If clsCommon.myLen(txtQuantitiveStructureFreeUOM.Value) <= 0 Then
                txtQuantitiveStructureFreeUOM.Focus()
                Throw New Exception("Please enter Free Uom")
            End If
        End If
        If clsCommon.CompairString(ddlType.SelectedValue, "VolumeSlab_Cash") = CompairStringResult.Equal Then

            If clsCommon.myLen(txtRangeUnit.Value) <= 0 Then
                txtRangeUnit.Focus()
                Throw New Exception("Please select Range UOM")
            End If


            If clsCommon.myLen(TxtCashDisunit.Value) <= 0 Then
                TxtCashDisunit.Focus()
                Throw New Exception("Please select Unit")
            End If
            If txtItemSturcture.arrValueMember Is Nothing OrElse txtItemSturcture.arrValueMember.Count <= 0 Then
                txtItemSturcture.Focus()
                Throw New Exception("Please select at least one Item Structure")
            End If

            If clsCommon.myLen(gvCashDisGrid.Rows(0).Cells(colToRange).Value) <= 0 AndAlso clsCommon.myLen(gvCashDisGrid.Rows(0).Cells(colFromRange).Value) <= 0 Then
                Throw New Exception("Please Enter Atleast one SLAB in grid.")
            End If
            If gvCashDisGrid.Rows.Count > 0 Then
                        For ii As Integer = 0 To gvCashDisGrid.RowCount - 1
                            If clsCommon.myCdbl(gvCashDisGrid.Rows(ii).Cells(colToRange).Value) > 0 AndAlso clsCommon.myCdbl(gvCashDisGrid.Rows(ii).Cells(colFromRange).Value) > 0 Then
                        If clsCommon.myCdbl(gvCashDisGrid.Rows(ii).Cells(colToRange).Value) <= clsCommon.myCdbl(gvCashDisGrid.Rows(ii).Cells(colFromRange).Value) Then
                            Throw New Exception("To Range should be greater  then From Range . At Line No: " + clsCommon.myCstr(clsCommon.myCdbl(ii + 1)) + " ")
                        End If
                        If clsCommon.myLen(gvCashDisGrid.Rows(ii).Cells(colCashDisAmt).Value) <= 0 Then
                            Throw New Exception("Cash Discount Amt is Mandatory. At Line No: " + clsCommon.myCstr(clsCommon.myCdbl(ii + 1)) + " ")
                        End If
                    End If
                        Next
                    End If
        End If

        Return True
    End Function

    Sub SaveData()
        Try
            strMsg = ""
            If (AllowToSave()) Then
                If clsCommon.myLen(strMsg) > 0 Then
                    strMsg = "Wrong UOM -" & strMsg
                    clsCommon.MyMessageBoxShow(strMsg)
                    Return
                End If
                If MyBase.isModifyonPasswordFlag Then
                    If clsPasswordCheckForMasters.CheckMasterPwd(clsUserMgtCode.frmSchemeMasterNew, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
                    Else
                        Return
                    End If
                End If

                Dim obj As New clsSchemeMasterDairy()
                obj.MaxlimitStart_Date = txtFromDate.Value
                obj.MaxlimitEnd_Date = txtToDate.Value
                obj.Scheme_Code = clsCommon.myCstr(fndScheme.Value)
                obj.Scheme_Desc = clsCommon.myCstr(txtDesc.Text)
                obj.Start_Date = clsCommon.myCDate(dtpScheme.Value)
                If clsCommon.CompairString(ddlType.SelectedValue, "VolumeSlab_Cash") = CompairStringResult.Equal Then
                    obj.Scheme_Type = "VS_CASH"
                Else
                    obj.Scheme_Type = clsCommon.myCstr(ddlType.SelectedValue)
                End If

                'obj.Item_Code = clsCommon.myCstr("NotManditory")
                obj.Item_Code = clsCommon.myCstr(txtMainItemGrp.Value)
                obj.Unit_Code = clsCommon.myCstr(txtUnitCode.Value)
                obj.Item_Qty = clsCommon.myCdbl(txtQty.Text)
                obj.MRP = clsCommon.myCdbl(ddlmrp.SelectedValue)
                obj.Basic_Price = clsCommon.myCdbl(ddlBasicPrice.SelectedValue)
                obj.Percentage = clsCommon.myCdbl(txtPercentage.Text)
                obj.Amount = clsCommon.myCdbl(txtAmount.Text)
                obj.Status = IIf(chkInactive.Checked, "InActive", "Active")
                obj.Target_Sub_Type = clsCommon.myCstr(ddlTargetType.SelectedValue)
                If chkInactive.Checked Then
                    obj.End_Date = clsCommon.myCDate(dtpInactive.Value)
                End If
                obj.Criteria = ddlCriteria.SelectedValue
                obj.Criteria_Code = clsCommon.myCstr(txtCriteria.Value)
                obj.Comments = clsCommon.myCstr(txtComment.Text)
                obj.Structure_Code = clsCommon.myCstr(txtStrcutCode.Value)
                obj.Structure_Unit = clsCommon.myCstr(txtstructUnit.Value)
                obj.ArrDTL = New List(Of clsSchemeDetailDairy)

                ''For Custom Fields
                obj.Form_ID = MyBase.Form_ID
                obj.arrCustomFields = New List(Of clsCustomFieldValues)
                If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
                    UcCustomFields1.GetData(obj.arrCustomFields)
                End If
                ''End of For Custom Fields

                '============================Scheme Item Information==============================
                Dim Count As Integer = 0
                If clsCommon.CompairString(ddlType.SelectedValue, "Quantitive") = CompairStringResult.Equal Then
                    obj.Quantitive_Type = cboQuantitiveType.SelectedValue
                ElseIf clsCommon.CompairString(ddlType.SelectedValue, "Structure") = CompairStringResult.Equal Then
                    obj.ArrQuantitiveStructureCode = txtQuantitiveStructureCode.arrValueMember
                    obj.Quantitive_Type_Structure_Main_Qty = txtQuantitiveStructureMainQty.Value
                    obj.Quantitive_Type_Structure_Main_UOM = txtQuantitiveStructureMainUOM.Value
                    obj.Quantitive_Type_Structure_Free_Item = txtQuantitiveStructureFreeICode.Value
                    obj.Quantitive_Type_Structure_Free_Qty = txtQuantitiveStructureFreeQty.Value
                    obj.Quantitive_Type_Structure_Free_UOM = txtQuantitiveStructureFreeUOM.Value
                End If


                obj.ArrDTL = New List(Of clsSchemeDetailDairy)
                For Each grow As GridViewRowInfo In gvItem.Rows
                    If (clsCommon.myLen(grow.Cells(colMainICode).Value) > 0 AndAlso clsCommon.myCBool(grow.Cells(colISelect).Value) = True) OrElse (clsCommon.myLen(grow.Cells(colICode).Value) > 0 AndAlso (clsCommon.CompairString(ddlType.SelectedValue, "MaxLimit") = CompairStringResult.Equal OrElse clsCommon.CompairString(ddlType.SelectedValue, "Fixed") = CompairStringResult.Equal)) Then
                        Dim objTr As New clsSchemeDetailDairy()
                        objTr.Scheme_Code = clsCommon.myCstr(fndScheme.Value)
                        objTr.Item_Code = clsCommon.myCstr(grow.Cells(colICode).Value)
                        objTr.Qty = clsCommon.myCdbl(grow.Cells(colQty).Value)
                        objTr.Unit_Code = clsCommon.myCstr(grow.Cells(colUOM).Value)
                        objTr.MRP = clsCommon.myCdbl(grow.Cells(colMRP).Value)
                        objTr.Price_Date = grow.Cells(colPriceDate).Value
                        objTr.Remarks = clsCommon.myCstr(grow.Cells(colRemarks).Value)
                        objTr.MainItem_Code = clsCommon.myCstr(grow.Cells(colMainICode).Value)
                        If clsCommon.CompairString(ddlType.SelectedValue, "Volume") = CompairStringResult.Equal Then
                            objTr.MainQty = txtQty.Value
                            objTr.MainUnit_Code = txtUnitCode.Value
                        Else
                            objTr.MainQty = clsCommon.myCdbl(grow.Cells(colMainIQty).Value)
                            objTr.MainUnit_Code = clsCommon.myCstr(grow.Cells(colMainIUnit).Value)
                        End If

                        objTr.CashDisc_Amount = clsCommon.myCdbl(grow.Cells(colCashAmt).Value)
                        objTr.CasdDisc_Percentage = clsCommon.myCdbl(grow.Cells(colCashPer).Value)
                        objTr.Amount = clsCommon.myCdbl(grow.Cells(colMainIAmt).Value)
                        If clsCommon.CompairString(ddlType.SelectedValue, "Quantitive") = CompairStringResult.Equal Then
                            If clsCommon.myCdbl(cboQuantitiveType.SelectedValue) = 1 Then
                                objTr.Max_Limit = clsCommon.myCdbl(grow.Cells(colMaxLimit).Value)
                                If objTr.Max_Limit <= 0 Then
                                    Throw New Exception("Please define +ve max limit at row No " + clsCommon.myCstr(grow.Index + 1))
                                End If
                            ElseIf clsCommon.myCdbl(cboQuantitiveType.SelectedValue) = 2 Then
                                objTr.Increment_Value = clsCommon.myCdbl(grow.Cells(colIncrementValue).Value)
                                If objTr.Increment_Value <= 0 Then
                                    Throw New Exception("Please define +ve Increment Value at row No " + clsCommon.myCstr(grow.Index + 1))
                                End If
                            End If
                        End If

                        obj.ArrDTL.Add(objTr)
                        Count += 1
                    End If
                Next
                If Not (clsCommon.CompairString(ddlType.SelectedValue, "VolumeSlab") = CompairStringResult.Equal OrElse clsCommon.CompairString(ddlType.SelectedValue, "Structure") = CompairStringResult.Equal OrElse clsCommon.CompairString(ddlType.SelectedValue, "VolumeSlab_Cash") = CompairStringResult.Equal) Then
                    If (obj.ArrDTL Is Nothing OrElse obj.ArrDTL.Count <= 0) Then
                        Throw New Exception("Please Fill at list one Item.")
                    End If
                End If
                'End If

                '-----------------Customer Information-----------------
                Count = 0
                obj.ArrSchmBen = New List(Of clsSchemeBenificiaryDairy)
                For Each grow As GridViewRowInfo In gvCustomer.Rows
                    If grow.Cells(colSelect).Value = True Then
                        Dim objTr As New clsSchemeBenificiaryDairy()
                        objTr.Scheme_Code = clsCommon.myCstr(fndScheme.Value)
                        objTr.Cust_Code = clsCommon.myCstr(grow.Cells(colCustCode).Value)
                        obj.ArrSchmBen.Add(objTr)
                        Count += 1
                    End If
                Next

                obj.Apply_Slab = chkSlabWise.Checked
                obj.Quantative_Scheme_In_Slab = ChkQuantativeSch.Checked
                obj.Quantum = CInt(txtQuantum.Value)
                If chkSlabWise.Checked And ChkQuantativeSch.Checked Then

                    obj.arrSLAB = New List(Of clsSchemeMasterDairySlab)
                    For ii As Integer = 0 To gvTS2.RowCount - 1
                        Dim objTS As New clsSchemeMasterDairySlab
                        objTS.Min_Range = clsCommon.myCdbl(gvTS2.Rows(ii).Cells(colRange).Value)
                        objTS.Value = clsCommon.myCdbl(gvTS2.Rows(ii).Cells(colRate).Value)
                        If objTS.Min_Range > 0 Then
                            obj.arrSLAB.Add(objTS)
                        End If
                    Next
                    If obj.arrSLAB.Count <= 0 Then
                        Throw New Exception("Please define at leats one row for slab and Apply then.")
                    End If

                    obj.arrQuantativeSLAB = New List(Of clsSchemeMasterDairyQuantativeSlab)
                    For ii As Integer = 0 To gvTS.RowCount - 1
                        Dim objTS1 As New clsSchemeMasterDairyQuantativeSlab
                        objTS1.Min_Range = clsCommon.myCdbl(gvTS.Rows(ii).Cells(colRange).Value)
                        objTS1.Value = clsCommon.myCdbl(gvTS.Rows(ii).Cells(colRate).Value)
                        If objTS1.Min_Range > 0 Then
                            obj.arrQuantativeSLAB.Add(objTS1)
                        End If
                    Next
                    If obj.arrQuantativeSLAB.Count <= 0 Then
                        Throw New Exception("Please define at least one row for slab and Apply then.")
                    End If

                ElseIf chkSlabWise.Checked Then
                    obj.arrSLAB = New List(Of clsSchemeMasterDairySlab)
                    For ii As Integer = 0 To gvTS.RowCount - 1
                        Dim objTS As New clsSchemeMasterDairySlab
                        objTS.Min_Range = clsCommon.myCdbl(gvTS.Rows(ii).Cells(colRange).Value)
                        objTS.Value = clsCommon.myCdbl(gvTS.Rows(ii).Cells(colRate).Value)
                        If objTS.Min_Range > 0 Then
                            obj.arrSLAB.Add(objTS)
                        End If
                    Next
                    If obj.arrSLAB.Count <= 0 Then
                        Throw New Exception("Please define at leats one row for slab")
                    End If
                End If
                If clsCommon.CompairString(ddlType.SelectedValue, "VolumeSlab") = CompairStringResult.Equal Then
                    obj.arrVolumeSLAB = New List(Of clsSchemeMasterVolumeSlab)
                    For ii As Integer = 0 To gvVolumeSlab.RowCount - 1
                        Dim dblMinRange As Double = clsCommon.myCdbl(gvVolumeSlab.Rows(ii).Cells(colRange).Value)
                        Dim dblMaxRange As Double = clsCommon.myCdbl(gvVolumeSlab.Rows(ii).Cells(colTo).Value)
                        If clsCommon.myCdbl(dblMinRange) > 0 AndAlso clsCommon.myCdbl(dblMaxRange) > 0 Then
                            Dim objVS As New clsSchemeMasterVolumeSlab
                            objVS.Min_Range = clsCommon.myCdbl(gvVolumeSlab.Rows(ii).Cells(colRange).Value)
                            objVS.Max_Range = clsCommon.myCdbl(gvVolumeSlab.Rows(ii).Cells(colTo).Value)
                            objVS.Item_Code = clsCommon.myCstr(gvVolumeSlab.Rows(ii).Cells(colICode).Value)
                            objVS.Unit_Code = clsCommon.myCstr(gvVolumeSlab.Rows(ii).Cells(colUOM).Value)
                            objVS.Qty = clsCommon.myCdbl(gvVolumeSlab.Rows(ii).Cells(colQty).Value)
                            If objVS.Min_Range > 0 Then
                                obj.arrVolumeSLAB.Add(objVS)
                            End If
                        End If
                    Next
                    If obj.arrVolumeSLAB.Count <= 0 Then
                        Throw New Exception("Please define atleast one row for Volume slab")
                    End If
                End If
                '==================Detail Section Ends Here=======================

                obj.ArrCashDisSturctureMapping = txtItemSturcture.arrValueMember

                obj.CASHDISVOL_RANGE_UOM = txtRangeUnit.Value
                obj.CASHDISVOL_UOM = TxtCashDisunit.Value
                obj.ArrCashDiscountVolumneDetails = New List(Of clsCashDiscountVolumneDetail)
                For Each grow As GridViewRowInfo In gvCashDisGrid.Rows
                    If clsCommon.myLen(grow.Cells(colFromRange).Value) > 0 AndAlso clsCommon.myLen(grow.Cells(colToRange).Value) > 0 Then
                        Dim objTr As New clsCashDiscountVolumneDetail()
                        objTr.SNO = clsCommon.myCdbl(grow.Cells(colInLineNo).Value)
                        objTr.Scheme_Code = obj.Scheme_Code
                        objTr.FROM_RANGE = clsCommon.myCdbl(grow.Cells(colFromRange).Value)
                        objTr.TO_RANGE = clsCommon.myCdbl(grow.Cells(colToRange).Value)
                        objTr.Discount = clsCommon.myCdbl(grow.Cells(colCashDisAmt).Value)
                        obj.ArrCashDiscountVolumneDetails.Add(objTr)
                    End If
                Next


                Dim isSaved As Boolean = False
                isSaved = obj.SaveData(obj, isNewEntry)

                '= KUNAL > TICKET : BM00000009590 > DATE : 20-09-2016 =============
                If isSaved Then
                    If UcAttachment1.AllowToSave() Then
                        If obj.Scheme_Code IsNot Nothing AndAlso obj.Scheme_Code.Length > 0 Then
                            UcAttachment1.SaveData(obj.Scheme_Code)
                        End If
                    End If
                End If
                LoadData(obj.Scheme_Code, NavigatorType.Current)
                isNewEntry = False
                clsCommon.MyMessageBoxShow("Data Saved Successfully.")
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        DeleteData(fndScheme.Value)
    End Sub

    Private Sub DeleteData(ByVal strSchemeCode As String)
        If clsCommon.myLen(strSchemeCode) = 0 Then
            clsCommon.MyMessageBoxShow("No Scheme found to delete.")
        End If
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If clsCommon.myLen(strSchemeCode) > 0 Then
                If clsSchemeMasterDairy.fundelete(strSchemeCode, trans) Then
                    trans.Commit()
                    clsCommon.MyMessageBoxShow("Data deleted successfully.")
                    Reset()
                End If
            Else
                clsCommon.MyMessageBoxShow("No Scheme found to delete.")
            End If
        Catch ex As Exception
            trans.Rollback()
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Reset()
        gvTS.Rows.AddNew()
    End Sub

    Private Sub Reset()
        RadPageView1.SelectedPage = RadPageViewPage1
        txtStrcutCode.Value = ""
        lblStructDesc.Text = ""
        txtstructUnit.Value = ""
        lblStructUnit.Text = ""
        txtRangeUnit.Value = ""
        TxtCashDisunit.Value = ""
        lblCashDisunit.Text = ""
        txtItemSturcture.arrValueMember = Nothing
        loadBlankVolumeslabGrid()
        LoadBlankVolumeSlabCashDisGrid()
        txtUnitCode.Visible = False
        txtQty.Visible = False
        lblUnit.Visible = False
        lblQty.Visible = False
        lblTargetType.Visible = False
        ddlTargetType.Visible = False
        ddlType.SelectedValue = "Cash"
        ddlType.SelectedValue = "Quantitive"
        txtFromDate.Value = clsCommon.GETSERVERDATE
        txtToDate.Value = clsCommon.GETSERVERDATE
        fndScheme.Value = ""
        txtDesc.Text = ""
        txtMainItemGrp.Value = ""
        lblMainItemDesc.Text = ""
        txtQty.Text = ""
        txtUnitCode.Value = ""
        lblUnitCodeDesc.Text = ""
        ddlmrp.DataSource = Nothing
        ddlBasicPrice.DataSource = Nothing
        txtComment.Text = ""
        txtCriteria.Value = ""
        lblCriteria.Text = ""
        LoadBlankItemGrid()
        gvItem.Rows.AddNew()
        txtPercentage.Enabled = False
        txtAmount.Enabled = False
        LoadBlankCustomerGrid()
        loadBlankGridRange()
        gvCustomer.DataSource = Nothing
        chkInactive.Checked = False
        dtpInactive.Enabled = False
        dtpScheme.Value = CurrentDate
        dtpInactive.Value = CurrentDate
        txtPercentage.Text = ""
        txtAmount.Text = ""
        txtComment.Text = ""
        btnSelect.Text = "Select All"
        btnSave.Text = "Save"
        isNewEntry = True
        chkSlabWise.Checked = False
        ChkQuantativeSch.Checked = False
        If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
            UcCustomFields1.SetDefaultValues()
        End If
        isCellValueChangedOpen = False
        '= KUNAL > TICKET : BM00000009590 > DATE : 20-09-2016 =
        UcAttachment1.BlankAllControls()
        gvTS2.Rows.Clear()
        ChkQuantativeSch.Checked = False
        txtQuantum.Value = 0
        cboQuantitiveType.SelectedValue = 0
        txtQuantitiveStructureMainQty.Value = 0
        txtQuantitiveStructureMainUOM.Value = ""
        txtQuantitiveStructureFreeICode.Value = ""
        txtQuantitiveStructureFreeQty.Value = 0
        txtQuantitiveStructureFreeUOM.Value = ""
        txtQuantitiveStructureCode.arrValueMember = Nothing
        btn_Apply.Visible = False
        strMsg = ""
        RadPageView1.Pages("RadPageViewPage4").Item.Visibility = ElementVisibility.Collapsed
    End Sub

    Private Sub rmiClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmiClose.Click
        Me.Close()
    End Sub

    Private Sub gvItem_CellEditorInitialized(sender As Object, e As GridViewCellEventArgs) Handles gvItem.CellEditorInitialized
        If KeyDownisRunning = True Then Exit Sub
        Dim GseEditor As GridSpinEditor = TryCast(e.ActiveEditor, GridSpinEditor)
        If Not GseEditor Is Nothing Then
            Dim element As GridSpinEditorElement = TryCast(GseEditor.EditorElement, GridSpinEditorElement)
            element.InterceptArrowKeys = False
            RemoveHandler element.KeyDown, AddressOf element_KeyDown
            AddHandler element.KeyDown, AddressOf element_KeyDown
        End If
    End Sub

    Private Sub gvItem_CellFormatting(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.CellFormattingEventArgs) Handles gvItem.CellFormatting
        If KeyDownisRunning = True Then Exit Sub
        If clsCommon.CompairString(ddlType.SelectedValue, "Volume") = CompairStringResult.Equal Then
            gvItem.CurrentRow.Cells(colMainIQty).ReadOnly = True
            gvItem.CurrentRow.Cells(colMainIUnit).ReadOnly = True
            gvItem.CurrentRow.Cells(colCashPer).ReadOnly = True
            gvItem.CurrentRow.Cells(colCashAmt).ReadOnly = True
            gvItem.CurrentRow.Cells(colICode).ReadOnly = False
            gvItem.CurrentRow.Cells(colQty).ReadOnly = False
            gvItem.CurrentRow.Cells(colUOM).ReadOnly = False
            gvItem.Columns(colMainIAmt).IsVisible = False
        ElseIf clsCommon.CompairString(ddlType.SelectedValue, "MaxLimit") = CompairStringResult.Equal Then
            gvItem.CurrentRow.Cells(colMainIQty).ReadOnly = False
            gvItem.CurrentRow.Cells(colMainIUnit).ReadOnly = False
            gvItem.CurrentRow.Cells(colCashPer).ReadOnly = True
            gvItem.CurrentRow.Cells(colCashAmt).ReadOnly = True
            gvItem.CurrentRow.Cells(colICode).ReadOnly = False
            gvItem.CurrentRow.Cells(colQty).ReadOnly = False
            gvItem.CurrentRow.Cells(colUOM).ReadOnly = False
            gvItem.Columns(colMainIAmt).IsVisible = False
            gvItem.Columns(colICode).IsVisible = False
            gvItem.Columns(colIName).IsVisible = False
            gvItem.Columns(colUOM).IsVisible = False
            gvItem.Columns(colPriceDate).IsVisible = False
            gvItem.Columns(colQty).IsVisible = False
            gvItem.Columns(colCashPer).IsVisible = False
            gvItem.Columns(colCashAmt).IsVisible = False
            gvItem.Columns(colMRP).IsVisible = False
        ElseIf clsCommon.CompairString(ddlType.SelectedValue, "Cash") = CompairStringResult.Equal Then
            gvItem.CurrentRow.Cells(colMainIQty).ReadOnly = False
            'gvItem.CurrentRow.Cells(colMainIQty).Value = 1
            gvItem.CurrentRow.Cells(colMainIUnit).ReadOnly = False
            gvItem.CurrentRow.Cells(colCashPer).ReadOnly = False
            gvItem.CurrentRow.Cells(colCashAmt).ReadOnly = False
            gvItem.CurrentRow.Cells(colICode).ReadOnly = True
            gvItem.CurrentRow.Cells(colQty).ReadOnly = True
            gvItem.CurrentRow.Cells(colUOM).ReadOnly = True
            gvItem.Columns(colMainIAmt).IsVisible = False
            'gvItem.CurrentRow.Cells(colICode). = False
            'gvItem.CurrentRow.Cells(colQty).IsVisible = False
            'gvItem.CurrentRow.Cells(colUOM).IsVisible = False

            gvItem.Columns(colMainIStructureCode).IsVisible = False
            gvItem.Columns(colMainIStructureDesc).IsVisible = False
            gvItem.Columns(colICode).IsVisible = False
            gvItem.Columns(colIName).IsVisible = False
            gvItem.Columns(colSchemeItemStrCode).IsVisible = False
            gvItem.Columns(colSchemeItemStrDesc).IsVisible = False
            gvItem.Columns(colUOM).IsVisible = False
            gvItem.Columns(colPriceDate).IsVisible = False
            gvItem.Columns(colQty).IsVisible = False
            gvItem.Columns(colMRP).IsVisible = False
            gvItem.Columns(colMaxLimit).IsVisible = False
            gvItem.Columns(colIncrementValue).IsVisible = False

        ElseIf clsCommon.CompairString(ddlType.SelectedValue, "Discount") = CompairStringResult.Equal Then
            gvItem.CurrentRow.Cells(colMainIQty).ReadOnly = False
            'gvItem.CurrentRow.Cells(colMainIQty).Value = 1
            gvItem.CurrentRow.Cells(colMainIUnit).ReadOnly = False
            gvItem.CurrentRow.Cells(colCashPer).ReadOnly = False
            gvItem.CurrentRow.Cells(colCashAmt).ReadOnly = False
            gvItem.Columns(colICode).IsVisible = False
            gvItem.Columns(colQty).IsVisible = False
            gvItem.Columns(colUOM).IsVisible = False
            gvItem.Columns(colMainIAmt).IsVisible = False
        ElseIf clsCommon.CompairString(ddlType.SelectedValue, "Target") = CompairStringResult.Equal AndAlso clsCommon.CompairString(ddlTargetType.SelectedValue, "Amount") = CompairStringResult.Equal Then
            gvItem.Columns(colMainIAmt).IsVisible = True
            gvItem.Columns(colMainIQty).IsVisible = False
            gvItem.Columns(colMainIUnit).IsVisible = False
            gvItem.Columns(colICode).IsVisible = False
            gvItem.Columns(colIName).IsVisible = False
            gvItem.Columns(colUOM).IsVisible = False
            gvItem.Columns(colPriceDate).IsVisible = False
            gvItem.Columns(colQty).IsVisible = False
            gvItem.CurrentRow.Cells(colCashPer).ReadOnly = False
            gvItem.CurrentRow.Cells(colCashAmt).ReadOnly = False

        ElseIf clsCommon.CompairString(ddlType.SelectedValue, "Target") = CompairStringResult.Equal AndAlso clsCommon.CompairString(ddlTargetType.SelectedValue, "Quantity") = CompairStringResult.Equal Then
            gvItem.Columns(colMainIAmt).IsVisible = False
            gvItem.Columns(colMainIQty).IsVisible = True
            gvItem.Columns(colMainIUnit).IsVisible = True
            gvItem.Columns(colICode).IsVisible = False
            gvItem.Columns(colIName).IsVisible = False
            gvItem.Columns(colUOM).IsVisible = False
            gvItem.Columns(colPriceDate).IsVisible = False
            gvItem.Columns(colQty).IsVisible = False
            gvItem.CurrentRow.Cells(colCashPer).ReadOnly = False
            gvItem.CurrentRow.Cells(colCashAmt).ReadOnly = False
        ElseIf clsCommon.CompairString(ddlType.SelectedValue, "Quantitive") = CompairStringResult.Equal Then
            gvItem.CurrentRow.Cells(colMaxLimit).ReadOnly = True
            gvItem.CurrentRow.Cells(colIncrementValue).ReadOnly = True
            If clsCommon.myCdbl(cboQuantitiveType.SelectedValue) = 1 Then
                gvItem.CurrentRow.Cells(colMaxLimit).ReadOnly = False
            ElseIf clsCommon.myCdbl(cboQuantitiveType.SelectedValue) = 2 Then
                gvItem.CurrentRow.Cells(colIncrementValue).ReadOnly = False
            End If

        Else
            gvItem.Columns(colMainIAmt).IsVisible = False
            gvItem.CurrentRow.Cells(colCashPer).ReadOnly = True
            gvItem.CurrentRow.Cells(colCashAmt).ReadOnly = True
            gvItem.CurrentRow.Cells(colMainIQty).ReadOnly = False
            gvItem.CurrentRow.Cells(colMainIUnit).ReadOnly = False
            gvItem.CurrentRow.Cells(colICode).ReadOnly = False
            gvItem.CurrentRow.Cells(colQty).ReadOnly = False
            gvItem.CurrentRow.Cells(colUOM).ReadOnly = False
        End If
    End Sub

    Private Sub gvItem_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvItem.CellValueChanged
        If gvItem.CurrentRow.Index >= 0 Then
            Dim MainFreshType As String = ""
            Dim MainMilkType As String = ""
            Dim MainProdType As Double = 0
            Dim Whr As String = ""
            '' Anubhooti 07-Oct-2014 BM00000004181 (Fresh/Milk Item Should fill fresh/Milk Items in grid else if item is not both fresh/milk then it will be consider as product type)
            If clsCommon.myLen(gvItem.CurrentRow.Cells(colICode).Value) > 0 Then
                MainFreshType = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select ISNULL(Is_FreshItem,'') As Is_FreshItem From TSPL_ITEM_MASTER Where Item_Code ='" & clsCommon.myCstr(txtMainItemGrp.Value) & "'"))
                If clsCommon.CompairString(MainFreshType, "1") = CompairStringResult.Equal Then
                    Whr = " ISNULL(Is_FreshItem,'')=1"
                End If
                MainMilkType = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select ISNULL(Product_Type,'') As Product_Type  From TSPL_ITEM_MASTER Where Item_Code ='" & clsCommon.myCstr(txtMainItemGrp.Value) & "'"))
                If clsCommon.CompairString(MainMilkType, "MI") = CompairStringResult.Equal Then
                    Whr = " ISNULL(Product_Type,'') ='MI'"
                End If
                MainProdType = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select COUNT(*)  From TSPL_ITEM_MASTER Where Item_Code ='" & clsCommon.myCstr(txtMainItemGrp.Value) & "' AND (ISNULL(Is_FreshItem,'')<>1 AND ISNULL(Product_Type,'') <>'MI')"))
                If MainProdType > 0 Then
                    Whr = " ISNULL(Is_FreshItem,'')<>1 AND ISNULL(Product_Type,'') <>'MI'"
                End If
            End If
            If (Not isInsideLoadData) Then
                If e.Column Is gvItem.Columns(colMainICode) Then
                    Whr += " CSA_TYPE='" & txtMainItemGrp.Value & "'"
                    qry = "Select Item_Code as Code, Item_Desc as Description from TSPL_ITEM_MASTER "

                    If AllowSchemeItems AndAlso clsCommon.myLen(Whr) > 0 Then
                        Whr += " and coalesce(is_scheme_item,0)=0 "
                    ElseIf AllowSchemeItems AndAlso clsCommon.myLen(Whr) <= 0 Then
                        Whr += " coalesce(is_scheme_item,0)=0 "
                    End If


                    gvItem.CurrentRow.Cells(colMainICode).Value = clsCommon.ShowSelectForm("MainCodeFind@SMD", qry, "Code", Whr, gvItem.CurrentRow.Cells(colMainICode).Value, "Code", False)
                    gvItem.CurrentRow.Cells(colMainIName).Value = clsItemMaster.GetItemName(gvItem.CurrentRow.Cells(colMainICode).Value, Nothing)
                End If
                If e.Column Is gvItem.Columns(colICode) Then
                    qry = "Select Item_Code as Code, Item_Desc as Description from TSPL_ITEM_MASTER"

                    If AllowSchemeItems AndAlso clsCommon.myLen(Whr) > 0 Then
                        Whr += " and coalesce(is_scheme_item,0)=1 "
                    ElseIf AllowSchemeItems AndAlso clsCommon.myLen(Whr) <= 0 Then
                        ' Whr += " coalesce(is_scheme_item,0)=1 "
                    End If

                    gvItem.CurrentRow.Cells(colICode).Value = clsCommon.ShowSelectForm("ItemCodefind@SMD", qry, "Code", Whr, gvItem.CurrentRow.Cells(colICode).Value, "Code", False)
                    gvItem.CurrentRow.Cells(colIName).Value = clsItemMaster.GetItemName(gvItem.CurrentRow.Cells(colICode).Value, Nothing)
                    qry = "Select TSPL_ITEM_MASTER.Structure_Code,TSPL_STRUCTURE_MASTER.Structure_Descq  from TSPL_ITEM_MASTER " & _
                        "left outer join TSPL_STRUCTURE_MASTER on TSPL_ITEM_MASTER.Structure_Code=TSPL_STRUCTURE_MASTER.Structure_Code where Item_Code='" & clsCommon.myCstr(gvItem.CurrentRow.Cells(colICode).Value) & "' "
                    Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                    If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                        gvItem.CurrentRow.Cells(colSchemeItemStrCode).Value = clsCommon.myCstr(dt.Rows(0)("Structure_Code"))
                        gvItem.CurrentRow.Cells(colSchemeItemStrDesc).Value = clsCommon.myCstr(dt.Rows(0)("Structure_Descq"))
                    End If
                End If
                If clsCommon.CompairString(ddlType.SelectedValue, "Cash") = CompairStringResult.Equal AndAlso e.Column Is gvItem.Columns(colCashPer) Then
                    If clsCommon.myCdbl(gvItem.CurrentRow.Cells(colCashPer).Value) > 0 AndAlso clsCommon.myCdbl(gvItem.CurrentRow.Cells(colCashAmt).Value) > 0 Then
                        gvItem.CurrentRow.Cells(colCashPer).Value = 0
                        clsCommon.MyMessageBoxShow("Please enter either cash percentage or cash amount.")
                    End If
                End If
                If clsCommon.CompairString(ddlType.SelectedValue, "Cash") = CompairStringResult.Equal AndAlso e.Column Is gvItem.Columns(colCashAmt) Then
                    If clsCommon.myCdbl(gvItem.CurrentRow.Cells(colCashPer).Value) > 0 AndAlso clsCommon.myCdbl(gvItem.CurrentRow.Cells(colCashAmt).Value) > 0 Then
                        gvItem.CurrentRow.Cells(colCashAmt).Value = 0
                        clsCommon.MyMessageBoxShow("Please enter either cash percentage or cash amount.")
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub gvItem_CurrentColumnChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gvItem.CurrentColumnChanged
        If gvItem.RowCount > 0 Then
            Dim intCurrRow As Integer = gvItem.CurrentRow.Index
            If intCurrRow >= 0 Then
                gvItem.CurrentRow.Cells(colLineNo).Value = clsCommon.myCdbl(intCurrRow + 1)
                If intCurrRow = gvItem.Rows.Count - 1 Then
                    gvItem.Rows.AddNew()
                    gvItem.CurrentRow = gvItem.Rows(intCurrRow)
                End If
            End If
        End If
    End Sub

    Private Sub gvCustomer_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvCustomer.CellValueChanged
        If (Not isInsideLoadData) Then
            If e.Column Is gvCustomer.Columns(colCustCode) And gvCustomer.CurrentRow.Index >= 0 Then
                qry = "Select Cust_Code as Code, Customer_Name as Name from TSPL_CUSTOMER_MASTER"
                gvCustomer.CurrentRow.Cells(colCustCode).Value = clsCommon.ShowSelectForm("CustFinder@SMD", qry, "Code", "Status='N'", gvCustomer.CurrentRow.Cells(colCustCode).Value, "Code", False)
                gvCustomer.CurrentRow.Cells(colCustName).Value = clsCustomerMaster.GetName(gvCustomer.CurrentRow.Cells(colCustCode).Value, Nothing)
                If clsCommon.myLen(gvCustomer.CurrentRow.Cells(colCustCode).Value) > 0 Then
                    gvCustomer.CurrentRow.Cells(colSelect).Value = True
                Else
                    gvCustomer.CurrentRow.Cells(colSelect).Value = False
                End If
            End If
        End If
    End Sub

    Private Sub gvCustomer_CurrentColumnChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gvCustomer.CurrentColumnChanged
        If gvCustomer.RowCount > 0 Then
            Dim intCurrRow As Integer = gvCustomer.CurrentRow.Index
            If intCurrRow >= 0 Then
                gvCustomer.CurrentRow.Cells(colLineNo).Value = clsCommon.myCdbl(intCurrRow + 1)
                If intCurrRow = gvCustomer.Rows.Count - 1 Then
                    gvCustomer.Rows.AddNew()
                    gvCustomer.CurrentRow = gvCustomer.Rows(intCurrRow)
                End If
            End If
        End If
    End Sub

    Private Sub btnSelect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelect.Click
        If clsCommon.CompairString(btnSelect.Text, "Select All") = CompairStringResult.Equal Then
            For Each grow As GridViewRowInfo In gvCustomer.Rows
                grow.Cells(colSelect).Value = True
            Next
            btnSelect.Text = "UnSelect All"
        Else
            For Each grow As GridViewRowInfo In gvCustomer.Rows
                grow.Cells(colSelect).Value = False
            Next
            btnSelect.Text = "Select All"
        End If
    End Sub

    Private Sub gvItem_EditorRequired(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.EditorRequiredEventArgs) Handles gvItem.EditorRequired
        Try
            Dim mrp As GridViewComboBoxColumn = TryCast(gvItem.Columns(colMRP), GridViewComboBoxColumn)
            Dim unit As GridViewComboBoxColumn = TryCast(gvItem.Columns(colUOM), GridViewComboBoxColumn)
            Dim Mainunit As GridViewComboBoxColumn = TryCast(gvItem.Columns(colMainIUnit), GridViewComboBoxColumn)
            Dim priceDate As GridViewComboBoxColumn = TryCast(gvItem.Columns(colPriceDate), GridViewComboBoxColumn)
            Dim ds As New DataSet()
            If gvItem.CurrentColumn.Name = colUOM Then
                qry = "select UOM_Code as Unit from TSPL_ITEM_UOM_DETAIL where Item_Code = '" + gvItem.CurrentRow.Cells(colICode).Value + "'"
                unit.DataSource = clsDBFuncationality.GetDataTable(qry)
                unit.ValueMember = "Unit"
            ElseIf gvItem.CurrentColumn.Name = colMainIUnit Then
                qry = "select UOM_Code as Unit from TSPL_ITEM_UOM_DETAIL where Item_Code = '" + gvItem.CurrentRow.Cells(colMainICode).Value + "'"
                Mainunit.DataSource = clsDBFuncationality.GetDataTable(qry)
                Mainunit.ValueMember = "Unit"
            ElseIf gvItem.CurrentColumn.Name = colPriceDate Then
                qry = "select distinct convert(varchar(10), Start_date,103) as Start_Date  from TSPL_ITEM_PRICE_MASTER where Item_Code = '" + gvItem.CurrentRow.Cells(colICode).Value + "' AND Uom='" + gvItem.CurrentRow.Cells(colUOM).Value + "'"
                priceDate.DataSource = clsDBFuncationality.GetDataTable(qry)
                priceDate.ValueMember = "Start_Date"
            ElseIf gvItem.CurrentColumn.Name = colMRP Then
                qry = "select distinct Item_Basic_Net  from TSPL_ITEM_PRICE_MASTER where Item_Code = '" + gvItem.CurrentRow.Cells(colICode).Value + "' AND Start_Date='" + Format(CDate(gvItem.CurrentRow.Cells(colPriceDate).Value), "MM/dd/yyyy") + "' AND Uom='" + gvItem.CurrentRow.Cells(colUOM).Value + "'"
                mrp.DataSource = clsDBFuncationality.GetDataTable(qry)
                mrp.ValueMember = "Item_Basic_Net"
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try

    End Sub

    Private Sub txtPercentage_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPercentage.Leave
        If clsCommon.myCdbl(txtPercentage.Text) > 0 Then
            txtAmount.Text = ""
        End If
    End Sub

    Private Sub txtAmount_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAmount.Leave
        If clsCommon.myCdbl(txtAmount.Text) > 0 Then
            txtPercentage.Text = ""
        End If
    End Sub

    Private Sub chkInactive_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkInactive.ToggleStateChanged
        dtpInactive.Enabled = chkInactive.Checked
    End Sub

    Private Sub export_main_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles export_main.Click
        Try
            qry = "select count(*) from TSPL_SCHEME_MASTER_NEW"
            Dim check As Integer = clsDBFuncationality.getSingleValue(qry)

            If check > 0 Then
                qry = "select TSPL_SCHEME_MASTER_NEW.Scheme_Code as [Scheme Code],TSPL_SCHEME_MASTER_NEW.Scheme_Desc as [Description] ,TSPL_SCHEME_MASTER_NEW.Start_Date as [Date],TSPL_SCHEME_MASTER_NEW.Status as [Inactive] ,TSPL_SCHEME_MASTER_NEW.end_date as [InActive Date],TSPL_SCHEME_MASTER_NEW.Item_Code as [Item Group],TSPL_SCHEME_MASTER_NEW.Scheme_Type as [Scheme type] ,TSPL_SCHEME_MASTER_NEW.Comments as [Comments],TSPL_SCHEME_MASTER_NEW.Criteria as [Criteria],TSPL_SCHEME_MASTER_NEW.Criteria_Code as [Criteris Code]  from TSPL_SCHEME_MASTER_NEW"
            Else
                qry = "select '' as [Scheme Code],'' as [Description] ,'' as [Date],'' as [Inactive] ,'' as [InActive Date],'' as [Item Group],'' as [Scheme type] ,'' as [Comments],'' as [Criteria],'' as [Criteris Code]  from TSPL_SCHEME_MASTER_NEW"
            End If
            transportSql.ExporttoExcel(qry, Me)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub export_beneficial_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles export_beneficial.Click
        Try
            qry = "select count(*) from TSPL_SCHEME_MASTER_NEW"
            Dim check As Integer = clsDBFuncationality.getSingleValue(qry)

            If check > 0 Then
                qry = "select TSPL_SCHEME_DETAIL_NEW.Scheme_Code as [Scheme Code] ,TSPL_SCHEME_DETAIL_NEW.MainItem_Code as [Main Item Code],TSPL_ITEM_MASTER.Item_Desc as [Main Item Desc],TSPL_SCHEME_DETAIL_NEW.MainUnit_Code as [Main Unit Code],isnull(TSPL_SCHEME_DETAIL_NEW.MainQty,0) as [Main Qty],TSPL_SCHEME_DETAIL_NEW.Item_code as [Scheme Item],TSPL_SCHEME_DETAIL_NEW.Unit_Code as [Unit Code],isnull(TSPL_SCHEME_DETAIL_NEW.Qty,0) as [Scheme Qty],isnull(TSPL_SCHEME_DETAIL_NEW.CasdDisc_Percentage,0) as [Cash per],isnull(TSPL_SCHEME_DETAIL_NEW.CashDisc_Amount,0) as [Cash Amount] ,TSPL_SCHEME_DETAIL_NEW.Remarks as [Remarks] from TSPL_SCHEME_DETAIL_NEW left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code= TSPL_SCHEME_DETAIL_NEW.MainItem_Code "
            Else
                qry = "select '' as [Scheme Code] ,'' as [Main Item Code],'' as [Main Unit Code],0 as [Main Qty],0 as [Cash per],0 as [Cash Amount] ,'' as [Remarks] from TSPL_SCHEME_DETAIL_NEW"
            End If
            transportSql.ExporttoExcel(qry, Me)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub Export_Criteria_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Export_Criteria.Click

        Try
            qry = "select count(*) from TSPL_SCHEME_BENEFICIARY"
            Dim check As Integer = clsDBFuncationality.getSingleValue(qry)

            If check > 0 Then
                qry = "select TSPL_SCHEME_BENEFICIARY.Scheme_Code as [Scheme Code], TSPL_SCHEME_MASTER_NEW.Criteria as[Criteria],TSPL_SCHEME_MASTER_NEW.Criteria_Code as [Criteria Code],TSPL_SCHEME_BENEFICIARY.Cust_Code as [Customer Code],TSPL_CUSTOMER_MASTER.Customer_Name as 'Customer Name' from TSPL_SCHEME_BENEFICIARY left outer join TSPL_SCHEME_MASTER_NEW on TSPL_SCHEME_MASTER_NEW.scheme_code=TSPL_SCHEME_BENEFICIARY.scheme_code left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SCHEME_BENEFICIARY.Cust_Code"
            Else
                qry = "select '' as [Scheme Code],'' as [Criteria],'' as [Criteria Code], '' as [Customer Code] from TSPL_SCHEME_BENEFICIARY"
            End If
            transportSql.ExporttoExcel(qry, Me)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try

    End Sub

    Private Sub Import_Main_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Import_Main.Click
        Try
            Dim gv As New RadGridView()
            Me.Controls.Add(gv)
            Dim isSaved As Boolean = True
            Dim counter As Integer = 0
            If transportSql.importExcel(gv, "Scheme Code", "Description", "Date", "Inactive", "InActive Date", "Item Group", "Scheme type", "Comments", "Criteria", "Criteris Code") Then
                clsCommon.ProgressBarShow()
                Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
                Try

                    Dim Scheme_Code As String = Nothing
                    Dim Scheme_Desc As String = Nothing
                    Dim Start_Date As Date = Nothing
                    Dim End_Date As Date = Nothing
                    Dim Scheme_Type As String = Nothing
                    Dim Item_Group As String = Nothing
                    Dim Unit_Code As String = Nothing

                    Dim Critria As String = Nothing
                    Dim Critria_code As String = Nothing
                    Dim Item_Qty As Double = 0.0
                    Dim MRP As Double = 0.0
                    Dim Basic_Price As Double = 0.0
                    Dim Amount As Double = 0.0
                    Dim Percentage As Double = 0.0
                    Dim Status As String = Nothing
                    Dim comnt As String = ""


                    For Each grow As GridViewRowInfo In gv.Rows

                        Scheme_Code = clsCommon.myCstr(grow.Cells("Scheme Code").Value)

                        If clsCommon.myLen(Scheme_Code) <= 0 Then
                            Throw New Exception("Please Fill Scheme Code")
                        End If
                        If clsCommon.myLen(Scheme_Code) >= 12 Then
                            Throw New Exception("Please enter only 12 character in Scheme code")
                        End If
                        Scheme_Desc = clsCommon.myCstr(grow.Cells("Description").Value)
                        If clsCommon.myLen(Scheme_Desc) >= 200 Then
                            Throw New Exception("Please enter only 200 character in description")
                        End If


                        Start_Date = clsCommon.GetPrintDate(clsCommon.myCDate(grow.Cells("Date").Value), "dd/MMM/yyyy")

                        Status = clsCommon.myCstr(grow.Cells("Inactive").Value)
                        If clsCommon.myCstr(grow.Cells("Inactive").Value) <> "Active" And clsCommon.myCstr(grow.Cells("Inactive").Value) <> "InActive" Then 'clsCommon.myLen(Status) <= 0 Then
                            Throw New Exception("Please Fill Status(Active/InActive)")
                        End If

                        If clsCommon.CompairString(Status, "InActive") = CompairStringResult.Equal And clsCommon.myLen(grow.Cells("InActive Date").Value) <= 0 Then
                            Throw New Exception("Please Fill InActive Date")
                        End If

                        If clsCommon.CompairString(Status, "InActive") = CompairStringResult.Equal Then
                            End_Date = clsCommon.GetPrintDate(clsCommon.myCDate(grow.Cells("InActive Date").Value), "dd/MMM/yyyy")
                        End If

                        'If clsCommon.myLen(End_Date) <= 0 Then
                        '    Throw New Exception("Please Fill Inactive Date")
                        'End If
                        'If clsCommon.CompairString(Status, "InActive") = CompairStringResult.Equal AndAlso clsCommon.myLen(End_Date) <= 0 Then
                        '    Throw New Exception("Please Fill InActive Date")
                        'End If

                        Item_Group = clsCommon.myCstr(grow.Cells("Item Group").Value)

                        If clsCommon.myLen(Item_Group) <= 0 Then
                            Throw New Exception("Please Fill Item Group")
                        End If

                        If clsCommon.myLen(Item_Group) > 0 Then
                            Dim qry As String = "select  CSA_TYPE from TSPL_ITEM_MASTER where (CSA_TYPE <> '' and CSA_TYPE <> 'None'  and CSA_TYPE ='" & Item_Group & "')"
                            Item_Group = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
                        End If
                        If clsCommon.myLen(Item_Group) <= 0 Then
                            Throw New Exception("Item Group ('" + Item_Group + "') does not exist in Item Master . Please make it entry first.")
                        End If

                        Scheme_Type = clsCommon.myCstr(grow.Cells("Scheme type").Value)
                        If clsCommon.CompairString(Scheme_Type, "Cash") <> CompairStringResult.Equal And clsCommon.CompairString(Scheme_Type, "Volume") <> CompairStringResult.Equal And clsCommon.CompairString(Scheme_Type, "MaxLimit") <> CompairStringResult.Equal And clsCommon.CompairString(Scheme_Type, "Discount") <> CompairStringResult.Equal And clsCommon.CompairString(Scheme_Type, "Quantitive") <> CompairStringResult.Equal Then
                            Throw New Exception("Please enter only Cash/Volume/MaxLimit/Discount/Quantitive in Current Scheme Type")
                        End If
                        comnt = clsCommon.myCstr(grow.Cells("comments").Value)
                        Critria = clsCommon.myCstr(grow.Cells("Criteria").Value)
                        Critria_code = clsCommon.myCstr(grow.Cells("Criteris Code").Value)

                        Dim check As Integer = clsDBFuncationality.getSingleValue("select count(*) from TSPL_SCHEME_MASTER_NEW WHERE Scheme_Code ='" + Scheme_Code + "'", trans)


                        Dim coll As New Hashtable()
                        clsCommon.AddColumnsForChange(coll, "Scheme_Code", Scheme_Code)
                        clsCommon.AddColumnsForChange(coll, "Scheme_Desc", Scheme_Desc)
                        clsCommon.AddColumnsForChange(coll, "Start_Date", clsCommon.GetPrintDate(Start_Date, "dd/MMM/yyyy"))
                        clsCommon.AddColumnsForChange(coll, "Status", Status)
                        If clsCommon.CompairString(Status, "InActive") = CompairStringResult.Equal Then
                            clsCommon.AddColumnsForChange(coll, "End_Date", clsCommon.GetPrintDate(End_Date, "dd/MMM/yyyy"))
                        Else
                            clsCommon.AddColumnsForChange(coll, "End_Date", "", True)
                        End If
                        clsCommon.AddColumnsForChange(coll, "Item_Code", Item_Group)
                        clsCommon.AddColumnsForChange(coll, "Scheme_Type", Scheme_Type)
                        clsCommon.AddColumnsForChange(coll, "Comments", comnt)
                        clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
                        clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
                        clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                        clsCommon.AddColumnsForChange(coll, "criteria", Critria)
                        clsCommon.AddColumnsForChange(coll, "criteria_code", Critria_code)
                        clsCommon.AddColumnsForChange(coll, "Unit_Code", Unit_Code)
                        clsCommon.AddColumnsForChange(coll, "Item_Qty", Item_Qty)
                        clsCommon.AddColumnsForChange(coll, "MRP", MRP)
                        clsCommon.AddColumnsForChange(coll, "Basic_Price", Basic_Price)
                        clsCommon.AddColumnsForChange(coll, "Amount", Amount)
                        clsCommon.AddColumnsForChange(coll, "Percentage", Percentage)
                        clsCommon.AddColumnsForChange(coll, "MaxlimitEnd_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                        clsCommon.AddColumnsForChange(coll, "MaxlimitStart_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))

                        If check <= 0 Then
                            clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                            clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                            isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SCHEME_MASTER_NEW", OMInsertOrUpdate.Insert, "", trans)
                        Else
                            isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SCHEME_MASTER_NEW", OMInsertOrUpdate.Update, " TSPL_SCHEME_MASTER_NEW.scheme_code='" + Scheme_Code + "'", trans)
                        End If
                        counter += 1
                    Next
                    trans.Commit()

                    clsCommon.ProgressBarHide()
                    common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
                Catch ex As Exception
                    trans.Rollback()
                    clsCommon.ProgressBarHide()
                    clsCommon.MyMessageBoxShow(ex.Message & " At Line No : " & counter + 1)
                End Try
            End If
            Me.Controls.Remove(gv)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub import_beneficial_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles import_beneficial.Click
        Try
            Dim gv As New RadGridView()
            Me.Controls.Add(gv)
            Dim issaved As Boolean = True
            If transportSql.importExcel(gv, "Scheme Code", "Main Item Code", "Main Unit Code", "Main Qty", "Scheme Item", "Unit Code", "Scheme Qty", "Cash per", "Cash Amount", "Remarks") Then
                clsCommon.ProgressBarShow()
                Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
                Try

                    Dim Scheme_Code As String = Nothing
                    Dim desc As String = Nothing
                    Dim item_code As String = Nothing
                    Dim Main_item_code As String = Nothing
                    Dim item_desc As String = Nothing
                    Dim qty As Decimal = Nothing
                    Dim Main_qty As Decimal = Nothing
                    Dim Cash_per As Decimal = Nothing
                    Dim Cash_Amount As Decimal = Nothing
                    Dim unit_code As String = Nothing
                    Dim Main_unit_code As String = Nothing
                    Dim unit_desc As String = Nothing
                    Dim MRP As Decimal = Nothing
                    Dim Price_date As Date = Nothing
                    Dim basic_price As Decimal = Nothing
                    Dim remarks As String = Nothing
                    Dim counter As Integer = 0
                    Dim Scheme_Item As String = Nothing
                    Dim Scheme_Unit As String = Nothing
                    Dim Scheme_Qty As Decimal = Nothing

                    For Each grow As GridViewRowInfo In gv.Rows
                        Scheme_Code = clsCommon.myCstr(grow.Cells("Scheme Code").Value)
                        If clsCommon.myLen(Scheme_Code) > 0 Then
                            Dim qry As String = "select scheme_code from TSPL_SCHEME_MASTER_NEW where Scheme_Code='" + Scheme_Code + "'"
                            Scheme_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
                            If clsCommon.myLen(Scheme_Code) <= 0 Then
                                Throw New Exception("Please Fill Scheme Code For Scheme [" + Scheme_Code + "] in header part")
                            End If
                        End If

                        Main_item_code = clsCommon.myCstr(grow.Cells("Main Item Code").Value)
                        If clsCommon.myLen(Main_item_code) <= 0 Then
                            Throw New Exception("Please Fill Main Item Code")
                        End If

                        Main_unit_code = clsCommon.myCstr(grow.Cells("Main Unit Code").Value)
                        If clsCommon.myLen(Main_unit_code) <= 0 Then
                            Throw New Exception("Please Fill Unit Code/Unit")
                        End If

                        If clsCommon.myLen(Main_unit_code) > 0 Then
                            qry = "select unit_code from tspl_unit_master where unit_code='" + unit_code + "'"
                            unit_code = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
                        End If
                        If clsCommon.myLen(Main_unit_code) <= 0 Then
                            Throw New Exception("Please Make Unit Master First")
                        End If
                        '' Scheme Item Added in Import
                        Scheme_Item = clsCommon.myCstr(grow.Cells("Scheme Item").Value)
                        If clsCommon.myLen(Scheme_Item) <= 0 Then
                            Throw New Exception("Please Fill Scheme Item Code")
                        End If

                        Scheme_Unit = clsCommon.myCstr(grow.Cells("Unit Code").Value)
                        If clsCommon.myLen(Scheme_Unit) <= 0 Then
                            Throw New Exception("Please Fill Unit Code/Unit")
                        End If

                        If clsCommon.myLen(Scheme_Unit) > 0 Then
                            qry = "select unit_code from tspl_unit_master where unit_code='" + Scheme_Unit + "'"
                            unit_code = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
                        End If
                        If clsCommon.myLen(Scheme_Unit) <= 0 Then
                            Throw New Exception("Please Make Unit Master First")
                        End If

                        Scheme_Qty = clsCommon.myCdbl(grow.Cells("Scheme Qty").Value)

                        'qty = clsCommon.myCdbl(grow.Cells("qty").Value)
                        'MRP = clsCommon.myCdbl(grow.Cells("mrp").Value)
                        'basic_price = clsCommon.myCdbl(grow.Cells("basic price").Value)
                        remarks = clsCommon.myCstr(grow.Cells("remarks").Value)
                        'Price_date = clsCommon.myCDate(grow.Cells("price date").Value)
                        Main_qty = clsCommon.myCdbl(grow.Cells("Main Qty").Value)
                        Cash_per = clsCommon.myCdbl(grow.Cells("Cash per").Value)
                        Cash_Amount = clsCommon.myCdbl(grow.Cells("Cash Amount").Value)
                        If clsCommon.myCdbl(Cash_per) <> 0 AndAlso clsCommon.myCdbl(Cash_Amount) <> 0 Then
                            Throw New Exception("Please enter Cash % or Cash amt for " + item_code + "")
                        End If

                        'If counter = 0 Then
                        Dim check As Integer = clsDBFuncationality.getSingleValue("select count(*) from TSPL_SCHEME_DETAIL_NEW WHERE Scheme_Code ='" + Scheme_Code + "' and MainItem_Code='" + Main_item_code + "'", trans)
                        'End If

                        Dim coll As New Hashtable()
                        clsCommon.AddColumnsForChange(coll, "scheme_code", Scheme_Code)
                        clsCommon.AddColumnsForChange(coll, "Item_Code", Scheme_Item)
                        clsCommon.AddColumnsForChange(coll, "MainItem_Code", Main_item_code)
                        clsCommon.AddColumnsForChange(coll, "qty", Scheme_Qty)
                        clsCommon.AddColumnsForChange(coll, "Unit_Code", Scheme_Unit)
                        clsCommon.AddColumnsForChange(coll, "MainUnit_Code", Main_unit_code)
                        clsCommon.AddColumnsForChange(coll, "mrp", MRP)
                        clsCommon.AddColumnsForChange(coll, "price_date", clsCommon.GetPrintDate(Price_date, "dd/MMM/yyyy"))
                        clsCommon.AddColumnsForChange(coll, "basic_price", basic_price)
                        clsCommon.AddColumnsForChange(coll, "remarks", remarks)
                        clsCommon.AddColumnsForChange(coll, "MainQty", Main_qty)
                        clsCommon.AddColumnsForChange(coll, "CasdDisc_Percentage", Cash_per)
                        clsCommon.AddColumnsForChange(coll, "CashDisc_Amount", Cash_Amount)


                        If check <= 0 Then
                            issaved = issaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SCHEME_DETAIL_NEW", OMInsertOrUpdate.Insert, "", trans)
                        Else
                            issaved = issaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SCHEME_DETAIL_NEW", OMInsertOrUpdate.Update, " TSPL_SCHEME_DETAIL_NEW.scheme_code='" + Scheme_Code + "' and TSPL_SCHEME_DETAIL_NEW.item_code='" + item_code + "'", trans)
                        End If

                        counter += 1
                    Next
                    trans.Commit()
                    clsCommon.ProgressBarHide()
                    common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
                Catch ex As Exception
                    trans.Rollback()
                    clsCommon.ProgressBarHide()
                    Throw New Exception(ex.Message)
                End Try
            End If
            Me.Controls.Remove(gv)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub import_criteria_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles import_criteria.Click

        Try
            Dim gv As New RadGridView()
            Me.Controls.Add(gv)
            Dim issaved As Boolean = True
            If transportSql.importExcel(gv, "Scheme Code", "Criteria", "Criteria Code", "Customer Code") Then
                clsCommon.ProgressBarShow()
                Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
                Try

                    Dim Scheme_Code As String = Nothing
                    Dim desc As String = Nothing
                    Dim code As String = Nothing
                    Dim criteria As String = Nothing
                    Dim cust_code As String = Nothing
                    Dim cust_name As String = Nothing
                    Dim add1 As String = Nothing
                    Dim add2 As String = Nothing
                    Dim add3 As String = Nothing
                    Dim counter As Integer = 0

                    For Each grow As GridViewRowInfo In gv.Rows
                        Scheme_Code = clsCommon.myCstr(grow.Cells("Scheme Code").Value)
                        If clsCommon.myLen(Scheme_Code) > 0 Then
                            Dim qry As String = "select scheme_code from TSPL_SCHEME_MASTER_NEW where Scheme_Code='" + Scheme_Code + "'"
                            Scheme_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
                            If clsCommon.myLen(Scheme_Code) <= 0 Then
                                Throw New Exception("Please Fill Scheme Code For Scheme [" + Scheme_Code + "] in header part")
                            End If
                        End If


                        cust_code = clsCommon.myCstr(grow.Cells("Customer Code").Value)

                        If clsCommon.myLen(cust_code) <= 0 Then
                            Throw New Exception("Please Fill Customer")
                        End If

                        If clsCommon.myLen(cust_code) > 0 Then
                            qry = "select cust_code from tspl_customer_master where cust_code='" + cust_code + "'" ' and (add1+' '+add2+' '+add3)=(" + add1 + "+' '+" + add2 + "+' '+" + add3 + ")"
                            cust_code = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
                        End If
                        If clsCommon.myLen(cust_code) <= 0 Then
                            Throw New Exception("Please Fill Customer Code Of Customer [" + cust_code + "],Or Make Its Master First")
                        End If


                        criteria = clsCommon.myCstr(grow.Cells("Criteria").Value)

                        If clsCommon.myLen(criteria) <= 0 Then
                            Throw New Exception("Please Fill Criteria(Customer/Customer Group)")
                        End If
                        code = clsCommon.myCstr(grow.Cells("Criteria Code").Value)


                        'If counter = 0 Then
                        Dim check As Integer = clsDBFuncationality.getSingleValue("select count(*) from TSPL_SCHEME_BENEFICIARY WHERE Scheme_Code ='" + Scheme_Code + "' and cust_code='" + cust_code + "'", trans)
                        'End If

                        Dim coll As New Hashtable()
                        clsCommon.AddColumnsForChange(coll, "scheme_code", Scheme_Code)
                        clsCommon.AddColumnsForChange(coll, "cust_code", cust_code)

                        If check <= 0 Then
                            issaved = issaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SCHEME_BENEFICIARY", OMInsertOrUpdate.Insert, "", trans)
                        Else
                            issaved = issaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SCHEME_BENEFICIARY", OMInsertOrUpdate.Update, " TSPL_SCHEME_BENEFICIARY.Scheme_Code ='" + Scheme_Code + "' and TSPL_SCHEME_BENEFICIARY.cust_code='" + cust_code + "'", trans)
                        End If

                        qry = "update TSPL_SCHEME_MASTER_NEW set Criteria='" + criteria + "',Criteria_Code='" + code + "' where scheme_code='" + Scheme_Code + "'"
                        clsDBFuncationality.ExecuteNonQuery(qry, trans)

                        counter += 1
                    Next
                    trans.Commit()
                    clsCommon.ProgressBarHide()
                    common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
                Catch ex As Exception
                    trans.Rollback()
                    clsCommon.ProgressBarHide()
                    Throw New Exception(ex.Message)
                End Try
            End If
            Me.Controls.Remove(gv)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub btnItemSelect_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnItemSelect.Click
        If clsCommon.CompairString(btnItemSelect.Text, "Select All") = CompairStringResult.Equal Then
            For Each grow As GridViewRowInfo In gvItem.Rows
                grow.Cells(colISelect).Value = True
            Next
            btnItemSelect.Text = "UnSelect All"
        Else
            For Each grow As GridViewRowInfo In gvItem.Rows
                grow.Cells(colISelect).Value = False
            Next
            btnItemSelect.Text = "Select All"
        End If
    End Sub

    Private Sub element_KeyDown(sender As Object, e As KeyEventArgs)
        KeyDownisRunning = True
        Select Case e.KeyData

            Case Keys.Up

                gvItem.GridNavigator.SelectPreviousRow(1)

            Case Keys.Down

                gvItem.GridNavigator.SelectNextRow(1)

            Case Keys.Left

                gvItem.GridNavigator.SelectPreviousColumn()

            Case Keys.Right

                gvItem.GridNavigator.SelectNextColumn()


                'Case Keys.Enter

                '    gvItem.GridNavigator.SelectNextRow(1)

        End Select
        KeyDownisRunning = False
    End Sub

    Private Sub ddlTargetType_SelectedIndexChanged(sender As Object, e As UI.Data.PositionChangedEventArgs) Handles ddlTargetType.SelectedIndexChanged
        Try
            txtUnitCode.Visible = False
            txtQty.Visible = False
            lblUnit.Visible = False
            lblQty.Visible = False

            If clsCommon.CompairString(clsCommon.myCstr(ddlTargetType.SelectedValue), "Quantity") = CompairStringResult.Equal Then
                gvItem.Columns(colMainIAmt).IsVisible = False
                gvItem.Columns(colMainIQty).IsVisible = True
                gvItem.Columns(colMainIUnit).IsVisible = True
                gvItem.Columns(colICode).IsVisible = False
                gvItem.Columns(colIName).IsVisible = False
                gvItem.Columns(colUOM).IsVisible = False
                gvItem.Columns(colPriceDate).IsVisible = False
                gvItem.Columns(colQty).IsVisible = False
                gvItem.CurrentRow.Cells(colCashPer).ReadOnly = False
                gvItem.CurrentRow.Cells(colCashAmt).ReadOnly = False


            ElseIf clsCommon.CompairString(clsCommon.myCstr(ddlTargetType.SelectedValue), "Amount") = CompairStringResult.Equal Then
                gvItem.Columns(colMainIAmt).IsVisible = True
                gvItem.Columns(colMainIQty).IsVisible = False
                gvItem.Columns(colMainIUnit).IsVisible = False
                gvItem.Columns(colICode).IsVisible = False
                gvItem.Columns(colIName).IsVisible = False
                gvItem.Columns(colUOM).IsVisible = False
                gvItem.Columns(colPriceDate).IsVisible = False
                gvItem.Columns(colQty).IsVisible = False
                gvItem.CurrentRow.Cells(colCashPer).ReadOnly = False
                gvItem.CurrentRow.Cells(colCashAmt).ReadOnly = False


            Else
                'txtUnitCode.Visible = False
                'txtQty.Visible = False
                'lblUnit.Visible = False
                'lblQty.Visible = False
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub rmWholeSheetExport_Click(sender As Object, e As EventArgs) Handles rmWholeSheetExport.Click
        Try
            Dim qry As String = " select tspl_scheme_master_New.Scheme_Code,tspl_scheme_master_New.Scheme_Desc,TSPL_SCHEME_BENEFICIARY.Cust_Code,TSPL_CUSTOMER_MASTER.Customer_Name,tspl_scheme_master_New.Item_Code as Item_Group,tspl_scheme_detail_new.MainItem_Code,tspl_item_master.Item_Desc,tspl_scheme_detail_new.MainUnit_Code as Unit_Code,tspl_scheme_detail_new.MainQty,tspl_scheme_master_new.Scheme_Type,CONVERT(varchar, tspl_scheme_master_New.MaxlimitStart_Date,103) as Start_Date,CONVERT(varchar, tspl_scheme_master_New.MaxlimitEnd_Date,103) as End_Date, case when  TSPL_SCHEME_DETAIL_NEW.CashDisc_Amount >0 OR TSPL_SCHEME_DETAIL_NEW.CashDisc_Amount <0 then TSPL_SCHEME_DETAIL_NEW.CashDisc_Amount else TSPL_SCHEME_DETAIL_NEW.CasdDisc_Percentage end as 'Discount',case when TSPL_SCHEME_DETAIL_NEW.CasdDisc_Percentage >0 then 'By Percentage ' else 'By Value' end as Discount_Type from tspl_scheme_master_New left outer join TSPL_SCHEME_BENEFICIARY on TSPL_SCHEME_BENEFICIARY.Scheme_Code=tspl_scheme_master_New.Scheme_Code left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SCHEME_BENEFICIARY.Cust_Code left outer join tspl_scheme_detail_new on tspl_scheme_detail_new.Scheme_Code=tspl_scheme_master_New.Scheme_Code left outer join tspl_item_master on tspl_item_master.Item_Code=tspl_scheme_detail_new.MainItem_Code  "
            transportSql.ExporttoExcel(qry, " and tspl_scheme_master_new.Scheme_Type='Discount'", Me)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub rmImportWholeSheet_Click(sender As Object, e As EventArgs) Handles rmImportWholeSheet.Click
        ''import whole sheet
        Try
            Dim gv As New RadGridView()
            Me.Controls.Add(gv)
            Dim issaved As Boolean = True
            If transportSql.importExcel(gv, "Scheme_Code", "Scheme_Desc", "Cust_Code", "Customer_Name", "MainItem_Code", "Item_Desc", "Unit_Code", "MainQty", "Scheme_Type", "Start_Date", "End_Date", "Discount", "Discount_Type") Then
                clsCommon.ProgressBarShow()
                Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
                Try
                    Dim Scheme_Code As String = Nothing
                    Dim Scheme_Desc As String = Nothing
                    Dim Cust_Code As String = Nothing
                    Dim Customer_Name As String = Nothing
                    Dim MainItem_Code As String = Nothing
                    Dim Item_Desc As String = Nothing
                    Dim Unit_Code As String = Nothing
                    Dim MainQty As Decimal = Nothing
                    ' Dim CasdDisc_Percentage = 0
                    'Dim CashDisc_Amount = 0
                    Dim Discount = 0
                    Dim Scheme_Type As String = Nothing
                    Dim Start_Date As Date = Nothing
                    Dim End_Date As Date = Nothing
                    Dim Discount_Type As String = Nothing
                    Dim counter As Integer = 0
                    Dim Item_Qty As Decimal = 0
                    Dim Status As String = Nothing
                    Dim Item_Group As String = Nothing

                    For Each grow As GridViewRowInfo In gv.Rows
                        Scheme_Code = clsCommon.myCstr(grow.Cells("Scheme_Code").Value)
                        If clsCommon.myLen(Scheme_Code) <= 0 Then
                            Throw New Exception("Please Fill Scheme Code For Scheme [" + Scheme_Code + "] in header part")
                        End If
                        '------------------------------- For Item Group ------------------------------------------------------------
                        Item_Group = clsCommon.myCstr(grow.Cells("Item_Group").Value)
                        If clsCommon.myLen(Item_Group) > 0 Then
                            Dim qry1 As String = "select  distinct CSA_TYPE  from TSPL_ITEM_MASTER where (CSA_TYPE <> '' and CSA_TYPE <> 'None') and CSA_TYPE='" + Item_Group + "' "
                            Item_Group = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry1, trans))
                            If clsCommon.myLen(Item_Group) <= 0 Then
                                Throw New Exception("Item Group invalid For Scheme [" + Scheme_Code + "] in header part")
                            End If
                        Else
                            Throw New Exception("Please Fill Item Group For Scheme [" + Scheme_Code + "] in header part")
                        End If
                        '-----------------------------------------------------------------------------------------------------------
                        Cust_Code = clsCommon.myCstr(grow.Cells("Cust_Code").Value)
                        If clsCommon.myLen(Cust_Code) > 0 Then
                            Dim qry As String = "select Cust_Code from TSPL_CUSTOMER_MASTER where Cust_Code='" + Cust_Code + "'"
                            Cust_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
                            If clsCommon.myLen(Cust_Code) <= 0 Then
                                Throw New Exception("Customer Code invalid For Scheme [" + Scheme_Code + "] in header part")
                            End If
                        Else
                            Throw New Exception("Please Fill Customer Code For Scheme [" + Scheme_Code + "] in header part")
                        End If
                        '------------------------------------------------------------------------------------------------------------
                        MainItem_Code = clsCommon.myCstr(grow.Cells("MainItem_Code").Value)
                        If clsCommon.myLen(MainItem_Code) > 0 Then
                            Dim qry As String = "select Item_Code from tspl_item_master where Item_Code='" + MainItem_Code + "'"
                            MainItem_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
                            If clsCommon.myLen(MainItem_Code) <= 0 Then
                                Throw New Exception("Item Code invalid For Scheme [" + Scheme_Code + "] with Item code  [" + MainItem_Code + "] in header part")
                            End If
                        Else
                            Throw New Exception("Please Fill Item Code For Scheme [" + Scheme_Code + "] in header part")
                        End If

                        Unit_Code = clsCommon.myCstr(grow.Cells("Unit_Code").Value)
                        If clsCommon.myLen(Unit_Code) > 0 Then
                            Dim qry As String = "select Unit_Code from tspl_unit_master where Unit_Code='" + Unit_Code + "'"
                            Dim UnitCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
                            If clsCommon.myLen(UnitCode) <= 0 Then
                                Throw New Exception("Unit Code invalid For Scheme [" + Scheme_Code + "] with Unit code  [" + UnitCode + "] in header part")
                            End If
                        Else
                            Throw New Exception("Please Fill Unit Code For Scheme [" + Scheme_Code + "] in header part")
                        End If
                        '-------------------------------------------------------------------------------------------------------------
                        MainQty = clsCommon.myCstr(grow.Cells("MainQty").Value)
                        If clsCommon.myLen(MainItem_Code) <= 0 Then
                            MainQty = 1
                        End If
                        '--------------------------------------------------------------------------------------------------------------
                        Discount_Type = clsCommon.myCstr(grow.Cells("Discount_Type").Value)
                        If clsCommon.myLen(Discount_Type) > 0 Then
                            If Discount_Type.ToUpper() <> "BY VALUE" Then
                                If Discount_Type.ToUpper <> "BY PERCENTAGE" Then
                                    Throw New Exception("Ddiscount Type invalid For Scheme [" + Scheme_Code + "] with Item code  [" + MainItem_Code + "] in header part")
                                End If

                            End If
                        End If
                        '---------------------------------------------------------------------------------------------------------------
                        Start_Date = clsCommon.myCstr(grow.Cells("Start_Date").Value)
                        If clsCommon.myLen(Start_Date) > 0 AndAlso IsNothing(Start_Date) = False AndAlso IsDBNull(Start_Date) = False Then
                            Start_Date = clsCommon.myCstr(clsCommon.GetPrintDate(grow.Cells("Start_Date").Value, "dd/MMM/yyyy"))
                        Else
                            Throw New Exception(" Please Fill Scheme Start Date For Scheme [" + Scheme_Code + "] with Item code  [" + MainItem_Code + "] in header part")
                        End If
                        '----------------------------------------------------------------------------------------------------------------
                        End_Date = clsCommon.myCstr(grow.Cells("End_Date").Value)
                        If clsCommon.myLen(End_Date) > 0 AndAlso IsNothing(End_Date) = False AndAlso IsDBNull(End_Date) = False Then
                            End_Date = clsCommon.myCstr(clsCommon.GetPrintDate(grow.Cells("End_Date").Value, "dd/MMM/yyyy"))
                        Else
                            Throw New Exception(" Please Fill Scheme End Date For Scheme [" + Scheme_Code + "] with Item code  [" + MainItem_Code + "] in header part")
                        End If
                        '------------------------------------------------------------------------------------------------------------------
                        Scheme_Type = "Discount"
                        Discount = clsCommon.myCdbl(grow.Cells("Discount").Value)
                        Scheme_Desc = clsCommon.myCstr(grow.Cells("Scheme_Desc").Value)
                        '------------------------------------------------------------------------------------------------------------------
                        Item_Qty = 0

                        Status = "Active"
                        Dim collSchemeMaster As New Hashtable()
                        clsCommon.AddColumnsForChange(collSchemeMaster, "Scheme_Code", Scheme_Code)
                        clsCommon.AddColumnsForChange(collSchemeMaster, "Scheme_Desc", Scheme_Desc)
                        clsCommon.AddColumnsForChange(collSchemeMaster, "Item_Code", Item_Group)
                        clsCommon.AddColumnsForChange(collSchemeMaster, "Unit_Code", Unit_Code)
                        clsCommon.AddColumnsForChange(collSchemeMaster, "Scheme_Type", Scheme_Type)
                        clsCommon.AddColumnsForChange(collSchemeMaster, "Start_Date", clsCommon.myCstr(clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy")))
                        clsCommon.AddColumnsForChange(collSchemeMaster, "MaxlimitStart_Date", clsCommon.GetPrintDate(Start_Date, "dd/MMM/yyyy"))
                        clsCommon.AddColumnsForChange(collSchemeMaster, "MaxlimitEnd_Date", clsCommon.GetPrintDate(End_Date, "dd/MMM/yyyy"))
                        clsCommon.AddColumnsForChange(collSchemeMaster, "Item_Qty", Item_Qty)
                        clsCommon.AddColumnsForChange(collSchemeMaster, "comp_code", objCommonVar.CurrentCompanyCode)
                        clsCommon.AddColumnsForChange(collSchemeMaster, "Modify_By", objCommonVar.CurrentUserCode)
                        clsCommon.AddColumnsForChange(collSchemeMaster, "Modify_Date", clsCommon.myCstr(clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt")))
                        clsCommon.AddColumnsForChange(collSchemeMaster, "Status", Status)
                        clsCommon.AddColumnsForChange(collSchemeMaster, "Criteria_Code", "")
                        clsCommon.AddColumnsForChange(collSchemeMaster, "Criteria", "Customer")
                        Dim checkExcution As Integer = 0
                        Dim check As Integer = clsDBFuncationality.getSingleValue("select count(*) from tspl_scheme_master_new WHERE Scheme_Code ='" + Scheme_Code + "' ", trans)
                        If check <= 0 Then
                            clsCommon.AddColumnsForChange(collSchemeMaster, "Created_By", objCommonVar.CurrentUserCode)
                            clsCommon.AddColumnsForChange(collSchemeMaster, "Created_Date", clsCommon.myCstr(clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt")))
                            issaved = issaved AndAlso clsCommonFunctionality.UpdateDataTable(collSchemeMaster, "tspl_scheme_master_new", OMInsertOrUpdate.Insert, "", trans)
                            checkExcution = checkExcution + 1
                        Else
                            checkExcution = checkExcution + 1
                            check = clsDBFuncationality.getSingleValue("select count(*) from tspl_scheme_master_new WHERE Scheme_Code ='" + Scheme_Code + "' and Item_Code='" + MainItem_Code + "' ", trans)
                            If check > 0 Then

                                issaved = issaved AndAlso clsCommonFunctionality.UpdateDataTable(collSchemeMaster, "tspl_scheme_master_new", OMInsertOrUpdate.Update, " tspl_scheme_master_new.scheme_code='" + Scheme_Code + "' and tspl_scheme_master_new.item_code='" + MainItem_Code + "'", trans)
                            End If

                        End If
                        If checkExcution = 1 Then
                            Dim collBeneficiary As New Hashtable()
                            clsCommon.AddColumnsForChange(collBeneficiary, "Scheme_Code", Scheme_Code)
                            clsCommon.AddColumnsForChange(collBeneficiary, "Cust_Code", Cust_Code)
                            check = clsDBFuncationality.getSingleValue("select count(*) from TSPL_SCHEME_BENEFICIARY WHERE Scheme_Code ='" + Scheme_Code + "' and Cust_Code='" + Cust_Code + "'", trans)
                            If check <= 0 Then
                                checkExcution = checkExcution + 1
                                issaved = issaved AndAlso clsCommonFunctionality.UpdateDataTable(collBeneficiary, "TSPL_SCHEME_BENEFICIARY", OMInsertOrUpdate.Insert, "", trans)
                            Else
                                checkExcution = checkExcution + 1
                                issaved = issaved AndAlso clsCommonFunctionality.UpdateDataTable(collBeneficiary, "TSPL_SCHEME_BENEFICIARY", OMInsertOrUpdate.Update, " TSPL_SCHEME_BENEFICIARY.scheme_code='" + Scheme_Code + "' and TSPL_SCHEME_BENEFICIARY.Cust_Code='" + Cust_Code + "'", trans)
                            End If
                        End If
                        If checkExcution = 2 Then
                            Dim collSchemeDetails As New Hashtable()
                            clsCommon.AddColumnsForChange(collSchemeDetails, "Scheme_Code", Scheme_Code)
                            clsCommon.AddColumnsForChange(collSchemeDetails, "MainItem_code", MainItem_Code)
                            clsCommon.AddColumnsForChange(collSchemeDetails, "MainQty", MainQty)
                            clsCommon.AddColumnsForChange(collSchemeDetails, "MainUnit_Code", Unit_Code)
                            If Discount_Type.ToUpper() = "BY PERCENTAGE" Then
                                clsCommon.AddColumnsForChange(collSchemeDetails, "CasdDisc_Percentage", Discount)
                            End If
                            If Discount_Type.ToUpper() = "BY VALUE" Then

                                clsCommon.AddColumnsForChange(collSchemeDetails, "CashDisc_Amount", Discount)
                            End If
                            check = clsDBFuncationality.getSingleValue("select count(*) from TSPL_SCHEME_DETAIL_NEW WHERE Scheme_Code ='" + Scheme_Code + "' and TSPL_SCHEME_DETAIL_NEW.MainItem_code='" + MainItem_Code + "'", trans)
                            If check <= 0 Then

                                issaved = issaved AndAlso clsCommonFunctionality.UpdateDataTable(collSchemeDetails, "TSPL_SCHEME_DETAIL_NEW", OMInsertOrUpdate.Insert, "", trans)
                            Else
                                issaved = issaved AndAlso clsCommonFunctionality.UpdateDataTable(collSchemeDetails, "TSPL_SCHEME_DETAIL_NEW", OMInsertOrUpdate.Update, " TSPL_SCHEME_DETAIL_NEW.scheme_code='" + Scheme_Code + "' and TSPL_SCHEME_DETAIL_NEW.MainItem_code='" + MainItem_Code + "'", trans)
                            End If
                        End If
                        counter += 1
                    Next
                    trans.Commit()
                    clsCommon.ProgressBarHide()
                    common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)

                Catch ex As Exception
                    trans.Rollback()
                    clsCommon.ProgressBarHide()
                    Throw New Exception(ex.Message)
                End Try
            End If
            Me.Controls.Remove(gv)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub chkSlabWise_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkSlabWise.ToggleStateChanged
        RadGroupBox9.Visible = chkSlabWise.Checked
        RadGroupBox1.Visible = chkSlabWise.Checked
    End Sub

    Sub loadBlankGridRange()
        Try
            gvTS.Rows.Clear()
            gvTS.Columns.Clear()
            Dim repoDeciCol As GridViewDecimalColumn
            repoDeciCol = New GridViewDecimalColumn()
            repoDeciCol.Name = colRange
            repoDeciCol.Width = 120
            repoDeciCol.DecimalPlaces = 0
            repoDeciCol.Minimum = 0
            repoDeciCol.Maximum = 5000
            repoDeciCol.Step = 0
            repoDeciCol.ShowUpDownButtons = False
            'repoDeciCol.HeaderText = "Slab Upto"
            repoDeciCol.HeaderText = "From"
            gvTS.MasterTemplate.Columns.Add(repoDeciCol)

            repoDeciCol = New GridViewDecimalColumn()
            repoDeciCol.Name = colTo
            repoDeciCol.Width = 120
            repoDeciCol.DecimalPlaces = 0
            repoDeciCol.Minimum = 0
            repoDeciCol.Maximum = 5000
            repoDeciCol.ReadOnly = True
            repoDeciCol.ShowUpDownButtons = False
            repoDeciCol.Step = 0
            repoDeciCol.HeaderText = "To"
            gvTS.MasterTemplate.Columns.Add(repoDeciCol)

            repoDeciCol = New GridViewDecimalColumn()
            repoDeciCol.Name = colRate
            repoDeciCol.Width = 200
            repoDeciCol.DecimalPlaces = 2
            repoDeciCol.Minimum = 0
            repoDeciCol.Step = 0
            repoDeciCol.ShowUpDownButtons = False
            repoDeciCol.HeaderText = "Qty"
            gvTS.MasterTemplate.Columns.Add(repoDeciCol)

            gvTS.AllowDeleteRow = True
            gvTS.AllowAddNewRow = False
            gvTS.ShowGroupPanel = False
            gvTS.AllowColumnReorder = False
            gvTS.AllowRowReorder = False
            gvTS.EnableSorting = False
            gvTS.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
            gvTS.MasterTemplate.ShowRowHeaderColumn = False
            gvTS.TableElement.TableHeaderHeight = 40
            gvTS.AutoSizeRows = False
            gvTS.AllowRowReorder = True

            '''''''''''''''''''''''''''''''''

            gvTS2.Rows.Clear()
            gvTS2.Columns.Clear()
            Dim repoDeciCol2 As GridViewDecimalColumn
            repoDeciCol2 = New GridViewDecimalColumn()
            repoDeciCol2.Name = colRange
            repoDeciCol2.Width = 120
            repoDeciCol2.DecimalPlaces = 0
            repoDeciCol2.Minimum = 0
            repoDeciCol2.Maximum = 5000
            repoDeciCol2.Step = 0
            repoDeciCol2.ShowUpDownButtons = False
            'repoDeciCol.HeaderText = "Slab Upto"
            repoDeciCol2.HeaderText = "From"
            gvTS2.MasterTemplate.Columns.Add(repoDeciCol2)

            repoDeciCol2 = New GridViewDecimalColumn()
            repoDeciCol2.Name = colTo
            repoDeciCol2.Width = 120
            repoDeciCol2.DecimalPlaces = 0
            repoDeciCol2.Minimum = 0
            repoDeciCol2.Maximum = 5000
            repoDeciCol2.ReadOnly = True
            repoDeciCol2.ShowUpDownButtons = False
            repoDeciCol2.Step = 0
            repoDeciCol2.HeaderText = "To"
            gvTS2.MasterTemplate.Columns.Add(repoDeciCol2)

            repoDeciCol2 = New GridViewDecimalColumn()
            repoDeciCol2.Name = colRate
            repoDeciCol2.Width = 200
            repoDeciCol2.DecimalPlaces = 2
            repoDeciCol2.Minimum = 0
            repoDeciCol2.Step = 0
            repoDeciCol2.ShowUpDownButtons = False
            repoDeciCol2.HeaderText = "Qty"
            gvTS2.MasterTemplate.Columns.Add(repoDeciCol2)

            gvTS2.AllowDeleteRow = True
            gvTS2.AllowAddNewRow = False
            gvTS2.ShowGroupPanel = False
            gvTS2.AllowColumnReorder = False
            gvTS2.AllowRowReorder = False
            gvTS2.EnableSorting = False
            gvTS2.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
            gvTS2.MasterTemplate.ShowRowHeaderColumn = False
            gvTS2.TableElement.TableHeaderHeight = 40
            gvTS2.AutoSizeRows = False
            gvTS2.AllowRowReorder = True


        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Sub loadBlankVolumeslabGrid()
        Try
            gvVolumeSlab.Rows.Clear()
            gvVolumeSlab.Columns.Clear()

            Dim repoDeciCol As GridViewDecimalColumn

            repoDeciCol = New GridViewDecimalColumn()
            repoDeciCol.Name = colRange
            repoDeciCol.Width = 120
            repoDeciCol.DecimalPlaces = 2
            repoDeciCol.Minimum = 0
            'repoDeciCol.Maximum = 5000
            repoDeciCol.Step = 0
            repoDeciCol.ShowUpDownButtons = False
            repoDeciCol.HeaderText = "From"
            gvVolumeSlab.MasterTemplate.Columns.Add(repoDeciCol)

            ''richa ERO/10/10/19-001044
            repoDeciCol = New GridViewDecimalColumn()
            repoDeciCol.Name = colTo
            repoDeciCol.FormatString = "{0:n2}"
            repoDeciCol.Width = 120
            repoDeciCol.DecimalPlaces = 2
            repoDeciCol.Minimum = 0
            repoDeciCol.ShowUpDownButtons = False
            repoDeciCol.Step = 0
            repoDeciCol.HeaderText = "To"
            gvVolumeSlab.MasterTemplate.Columns.Add(repoDeciCol)

            Dim repoICode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            repoICode.FormatString = ""
            repoICode.HeaderText = "Scheme Item Code"
            repoICode.Name = colICode
            repoICode.HeaderImage = Global.ERP.My.Resources.Resources.search4
            repoICode.TextImageRelation = TextImageRelation.TextBeforeImage
            repoICode.Width = 100
            gvVolumeSlab.MasterTemplate.Columns.Add(repoICode)

            Dim repoIName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            repoIName.FormatString = ""
            repoIName.HeaderText = "Scheme Item Description"
            repoIName.Name = colIName
            repoIName.Width = 200
            repoIName.ReadOnly = True
            gvVolumeSlab.MasterTemplate.Columns.Add(repoIName)

            Dim repoUnitCode As GridViewComboBoxColumn = New GridViewComboBoxColumn()
            repoUnitCode.HeaderText = "Scheme Unit Code"
            repoUnitCode.Name = colUOM
            repoUnitCode.Width = 80
            repoUnitCode.ReadOnly = False
            gvVolumeSlab.MasterTemplate.Columns.Add(repoUnitCode)

            Dim repoReqQty As GridViewDecimalColumn = New GridViewDecimalColumn()
            repoReqQty.FormatString = ""
            repoReqQty.HeaderText = "Scheme Qty"
            repoReqQty.Name = colQty
            repoReqQty.Width = 80
            repoReqQty.Minimum = 0
            repoReqQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
            gvVolumeSlab.MasterTemplate.Columns.Add(repoReqQty)


            gvVolumeSlab.AllowDeleteRow = True
            gvVolumeSlab.AllowAddNewRow = False
            gvVolumeSlab.ShowGroupPanel = False
            gvVolumeSlab.AllowColumnReorder = False
            gvVolumeSlab.AllowRowReorder = False
            gvVolumeSlab.EnableSorting = False
            gvVolumeSlab.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
            gvVolumeSlab.MasterTemplate.ShowRowHeaderColumn = False
            gvVolumeSlab.TableElement.TableHeaderHeight = 40
            gvVolumeSlab.AutoSizeRows = False
            gvVolumeSlab.AllowRowReorder = True


        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub gvTS_CurrentColumnChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gvTS.CurrentColumnChanged
        If gvTS.RowCount > 0 Then
            Dim intCurrRow As Integer = gvTS.CurrentRow.Index
            If intCurrRow = gvTS.Rows.Count - 1 Then
                gvTS.Rows.AddNew()
                gvTS.CurrentRow = gvTS.Rows(intCurrRow)
            End If
        End If
    End Sub

    Private Sub gvTS_UserDeletedRow(sender As Object, e As GridViewRowEventArgs) Handles gvTS.UserDeletedRow
        Try
            If gvTS.CurrentRow.Index > 0 Then
                gvTS.Rows(gvTS.CurrentRow.Index - 1).Cells(colTo).Value = clsCommon.myCdbl(clsCommon.myCdbl(gvTS.Rows(gvTS.CurrentRow.Index).Cells(colRange).Value) - 1)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try

    End Sub

    Private Sub gvTS_UserDeletingRow(sender As Object, e As GridViewRowCancelEventArgs) Handles gvTS.UserDeletingRow
        If Not myMessages.deleteConfirm() Then
            e.Cancel = True
        End If
    End Sub

    Private Sub gvTS_CellValueChanged(sender As Object, e As GridViewCellEventArgs) Handles gvTS.CellValueChanged
        Try
            If e.Column.Name = colRange And clsCommon.myCdbl(e.Value) > 0 And gvTS.CurrentRow.Index > 0 Then
                gvTS.Rows(gvTS.CurrentRow.Index - 1).Cells(colTo).Value = clsCommon.myCdbl(clsCommon.myCdbl(e.Value) - 1)
                If (gvTS.CurrentRow.Index + 1 = gvTS.Rows.Count) OrElse (clsCommon.myCdbl(gvTS.Rows(gvTS.CurrentRow.Index + 1).Cells(colTo).Value) = 0) Then
                    gvTS.Rows(gvTS.CurrentRow.Index).Cells(colTo).Value = 5000
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub rmSchemeWithSlabExport_Click(sender As Object, e As EventArgs) Handles rmSchemeWithSlabExport.Click
        If chkSlabWise.Checked = False Then
            common.clsCommon.MyMessageBoxShow("Scheme Slab not found.")
            Exit Sub
        End If
        Try
            Dim qry As String = " select tspl_scheme_master_New.Scheme_Code,tspl_scheme_master_New.Scheme_Desc,TSPL_SCHEME_BENEFICIARY.Cust_Code,TSPL_CUSTOMER_MASTER.Customer_Name,tspl_scheme_master_New.Item_Code as Item_Group,tspl_scheme_detail_new.MainItem_Code,tspl_item_master.Item_Desc,tspl_scheme_detail_new.MainUnit_Code as MainUnit_Code,tspl_scheme_detail_new.MainQty,tspl_scheme_master_new.Scheme_Type,tspl_scheme_detail_new.Item_Code,tspl_scheme_detail_new.Unit_Code,tspl_scheme_detail_new.Qty,CONVERT(varchar, tspl_scheme_master_New.MaxlimitStart_Date,103) as Start_Date,CONVERT(varchar, tspl_scheme_master_New.MaxlimitEnd_Date,103) as End_Date " & _
            ",TSPL_SCHEME_MASTER_NEW_SLAB.Min_Range " & _
            ",TSPL_SCHEME_MASTER_NEW_SLAB.Value " & _
           " from tspl_scheme_master_New " & _
            " left outer join TSPL_SCHEME_BENEFICIARY on TSPL_SCHEME_BENEFICIARY.Scheme_Code=tspl_scheme_master_New.Scheme_Code  " & _
            " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SCHEME_BENEFICIARY.Cust_Code  " & _
            " left outer join tspl_scheme_detail_new on tspl_scheme_detail_new.Scheme_Code=tspl_scheme_master_New.Scheme_Code  " & _
            " left outer join tspl_item_master on tspl_item_master.Item_Code=tspl_scheme_detail_new.MainItem_Code  " & _
            " left outer join TSPL_SCHEME_MASTER_NEW_SLAB on TSPL_SCHEME_MASTER_NEW_SLAB.Scheme_Code=tspl_scheme_master_New.Scheme_Code "

            transportSql.ExporttoExcel(qry, " and tspl_scheme_master_new.Scheme_Code='" & fndScheme.Value & "'", Me)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub rmSchemeWithSlabImp_Click(sender As Object, e As EventArgs) Handles rmSchemeWithSlabImp.Click
        Try
            Dim gv As New RadGridView()
            Me.Controls.Add(gv)
            Dim issaved As Boolean = True
            If transportSql.importExcel(gv, "Scheme_Code", "Scheme_Desc", "Cust_Code", "Customer_Name", "MainItem_Code", "Item_Desc", "MainUnit_Code", "MainQty", "Scheme_Type", "Item_Code", "Unit_Code", "Qty", "Start_Date", "End_Date", "Min_Range", "Value") Then
                clsCommon.ProgressBarShow()
                Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
                Try
                    Dim Scheme_Code As String = Nothing
                    Dim Scheme_Desc As String = Nothing
                    Dim Cust_Code As String = Nothing
                    Dim Customer_Name As String = Nothing
                    Dim MainItem_Code As String = Nothing
                    Dim Item_Desc As String = Nothing
                    Dim Unit_Code As String = Nothing
                    Dim MainUnit_Code As String = Nothing
                    Dim MainQty As Decimal = Nothing
                    Dim Scheme_Type As String = Nothing
                    Dim Start_Date As Date = Nothing
                    Dim End_Date As Date = Nothing
                    Dim counter As Integer = 0
                    Dim Item_Qty As Decimal = 0
                    Dim Status As String = Nothing
                    Dim Item_Group As String = Nothing
                    Dim Min_Range As Decimal = Nothing
                    Dim Range_Value As Decimal = Nothing
                    Dim Scheme_Item As String = Nothing
                    Dim Scheme_Unit As String = Nothing
                    Dim Scheme_Qty As Decimal = Nothing

                    If clsDBFuncationality.getSingleValue("select count(*) from tspl_scheme_master_New where Scheme_Code='" & gv.Rows(0).Cells("Scheme_Code").Value & "'", trans) > 0 Then
                        If (clsSchemeMasterDairy.fundelete(gv.Rows(0).Cells("Scheme_Code").Value, trans) = False) Then
                            Throw New Exception("Error in Document deletion.")
                        End If
                    End If

                    For Each grow As GridViewRowInfo In gv.Rows
                        Scheme_Code = clsCommon.myCstr(grow.Cells("Scheme_Code").Value)
                        If clsCommon.myLen(Scheme_Code) <= 0 Then
                            Throw New Exception("Please Fill Scheme Code For Scheme [" + Scheme_Code + "] in header part")
                        End If
                        '------------------------------- For Item Group ------------------------------------------------------------
                        Item_Group = clsCommon.myCstr(grow.Cells("Item_Group").Value)
                        If clsCommon.myLen(Item_Group) > 0 Then
                            Dim qry1 As String = "select  distinct CSA_TYPE  from TSPL_ITEM_MASTER where (CSA_TYPE <> '' and CSA_TYPE <> 'None') and CSA_TYPE='" + Item_Group + "' "
                            Item_Group = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry1, trans))
                            If clsCommon.myLen(Item_Group) <= 0 Then
                                Throw New Exception("Item Group invalid For Scheme [" + Scheme_Code + "] in header part")
                            End If
                        Else
                            Throw New Exception("Please Fill Item Group For Scheme [" + Scheme_Code + "] in header part")
                        End If
                        '-----------------------------------------------------------------------------------------------------------
                        Cust_Code = clsCommon.myCstr(grow.Cells("Cust_Code").Value)
                        If clsCommon.myLen(Cust_Code) > 0 Then
                            Dim qry As String = "select Cust_Code from TSPL_CUSTOMER_MASTER where Cust_Code='" + Cust_Code + "'"
                            Cust_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
                            If clsCommon.myLen(Cust_Code) <= 0 Then
                                Throw New Exception("Customer Code invalid For Scheme [" + Scheme_Code + "] in header part")
                            End If
                        Else
                            Throw New Exception("Please Fill Customer Code For Scheme [" + Scheme_Code + "] in header part")
                        End If
                        '------------------------------------------------------------------------------------------------------------
                        MainItem_Code = clsCommon.myCstr(grow.Cells("MainItem_Code").Value)
                        If clsCommon.myLen(MainItem_Code) > 0 Then
                            Dim qry As String = "select Item_Code from tspl_item_master where Item_Code='" + MainItem_Code + "'"
                            MainItem_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
                            If clsCommon.myLen(MainItem_Code) <= 0 Then
                                Throw New Exception("Item Code invalid For Scheme [" + Scheme_Code + "] with Item code  [" + MainItem_Code + "] in header part")
                            End If
                        Else
                            Throw New Exception("Please Fill Item Code For Scheme [" + Scheme_Code + "] in header part")
                        End If

                        MainUnit_Code = clsCommon.myCstr(grow.Cells("MainUnit_Code").Value)
                        If clsCommon.myLen(MainUnit_Code) > 0 Then
                            Dim qry As String = "select Unit_Code from tspl_unit_master where Unit_Code='" + MainUnit_Code + "'"
                            Dim MainUnitCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
                            If clsCommon.myLen(MainUnitCode) <= 0 Then
                                Throw New Exception("Unit Code invalid For Scheme [" + Scheme_Code + "] with Unit code  [" + MainUnitCode + "] in header part")
                            End If
                        Else
                            Throw New Exception("Please Fill Unit Code For Scheme [" + Scheme_Code + "] in header part")
                        End If
                        '-------------------------------------------------------------------------------------------------------------
                        MainQty = clsCommon.myCstr(grow.Cells("MainQty").Value)
                        If clsCommon.myLen(MainItem_Code) <= 0 Then
                            MainQty = 1
                        End If
                        '--------------------------------------------------------------------------------------------------------------

                        '----------Scheme----------------
                        Scheme_Item = clsCommon.myCstr(grow.Cells("Item_Code").Value)
                        If clsCommon.myLen(Scheme_Item) <= 0 Then
                            Throw New Exception("Please Fill Scheme Item Code")
                        End If

                        Scheme_Unit = clsCommon.myCstr(grow.Cells("Unit_Code").Value)
                        If clsCommon.myLen(Scheme_Unit) <= 0 Then
                            Throw New Exception("Please Fill Scheme Unit Code/Unit")
                        End If

                        If clsCommon.myLen(Scheme_Unit) > 0 Then
                            qry = "select unit_code from tspl_unit_master where unit_code='" + Scheme_Unit + "'"
                            Unit_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
                        End If
                        If clsCommon.myLen(Scheme_Unit) <= 0 Then
                            Throw New Exception("Please Make Unit Master First")
                        End If

                        Scheme_Qty = clsCommon.myCdbl(grow.Cells("Qty").Value)
                        '----------Scheme----------------

                        Start_Date = clsCommon.myCstr(grow.Cells("Start_Date").Value)
                        If clsCommon.myLen(Start_Date) > 0 AndAlso IsNothing(Start_Date) = False AndAlso IsDBNull(Start_Date) = False Then
                            Start_Date = clsCommon.myCstr(clsCommon.GetPrintDate(grow.Cells("Start_Date").Value, "dd/MMM/yyyy"))
                        Else
                            Throw New Exception(" Please Fill Scheme Start Date For Scheme [" + Scheme_Code + "] with Item code  [" + MainItem_Code + "] in header part")
                        End If
                        '----------------------------------------------------------------------------------------------------------------
                        End_Date = clsCommon.myCstr(grow.Cells("End_Date").Value)
                        If clsCommon.myLen(End_Date) > 0 AndAlso IsNothing(End_Date) = False AndAlso IsDBNull(End_Date) = False Then
                            End_Date = clsCommon.myCstr(clsCommon.GetPrintDate(grow.Cells("End_Date").Value, "dd/MMM/yyyy"))
                        Else
                            Throw New Exception(" Please Fill Scheme End Date For Scheme [" + Scheme_Code + "] with Item code  [" + MainItem_Code + "] in header part")
                        End If
                        '------------------------------------------------------------------------------------------------------------------
                        Scheme_Type = clsCommon.myCstr(grow.Cells("Scheme_Type").Value)
                        Scheme_Desc = clsCommon.myCstr(grow.Cells("Scheme_Desc").Value)
                        '------------------------------------------------------------------------------------------------------------------
                        Item_Qty = 0

                        Status = "Active"

                        Dim collSchemeMaster As New Hashtable()
                        clsCommon.AddColumnsForChange(collSchemeMaster, "Scheme_Code", Scheme_Code)
                        clsCommon.AddColumnsForChange(collSchemeMaster, "Scheme_Desc", Scheme_Desc)
                        clsCommon.AddColumnsForChange(collSchemeMaster, "Item_Code", Item_Group)
                        clsCommon.AddColumnsForChange(collSchemeMaster, "Unit_Code", MainUnit_Code)
                        clsCommon.AddColumnsForChange(collSchemeMaster, "Scheme_Type", Scheme_Type)
                        clsCommon.AddColumnsForChange(collSchemeMaster, "Start_Date", clsCommon.myCstr(clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy")))
                        clsCommon.AddColumnsForChange(collSchemeMaster, "MaxlimitStart_Date", clsCommon.GetPrintDate(Start_Date, "dd/MMM/yyyy"))
                        clsCommon.AddColumnsForChange(collSchemeMaster, "MaxlimitEnd_Date", clsCommon.GetPrintDate(End_Date, "dd/MMM/yyyy"))
                        clsCommon.AddColumnsForChange(collSchemeMaster, "Item_Qty", Item_Qty)
                        clsCommon.AddColumnsForChange(collSchemeMaster, "comp_code", objCommonVar.CurrentCompanyCode)
                        clsCommon.AddColumnsForChange(collSchemeMaster, "Modify_By", objCommonVar.CurrentUserCode)
                        clsCommon.AddColumnsForChange(collSchemeMaster, "Modify_Date", clsCommon.myCstr(clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt")))
                        clsCommon.AddColumnsForChange(collSchemeMaster, "Status", Status)
                        clsCommon.AddColumnsForChange(collSchemeMaster, "Criteria_Code", "")
                        clsCommon.AddColumnsForChange(collSchemeMaster, "Criteria", "Customer")
                        clsCommon.AddColumnsForChange(collSchemeMaster, "Apply_Slab", 1)

                        Dim checkExcution As Integer = 0
                        Dim check As Integer = clsDBFuncationality.getSingleValue("select count(*) from tspl_scheme_master_new WHERE Scheme_Code ='" + Scheme_Code + "' ", trans)
                        If check <= 0 Then
                            clsCommon.AddColumnsForChange(collSchemeMaster, "Created_By", objCommonVar.CurrentUserCode)
                            clsCommon.AddColumnsForChange(collSchemeMaster, "Created_Date", clsCommon.myCstr(clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt")))
                            issaved = issaved AndAlso clsCommonFunctionality.UpdateDataTable(collSchemeMaster, "tspl_scheme_master_new", OMInsertOrUpdate.Insert, "", trans)
                            checkExcution = checkExcution + 1
                        Else
                            checkExcution = checkExcution + 1
                            issaved = issaved AndAlso clsCommonFunctionality.UpdateDataTable(collSchemeMaster, "tspl_scheme_master_new", OMInsertOrUpdate.Update, " tspl_scheme_master_new.scheme_code='" + Scheme_Code + "'", trans)
                        End If
                        If checkExcution = 1 Then
                            Dim collBeneficiary As New Hashtable()
                            clsCommon.AddColumnsForChange(collBeneficiary, "Scheme_Code", Scheme_Code)
                            clsCommon.AddColumnsForChange(collBeneficiary, "Cust_Code", Cust_Code)
                            check = clsDBFuncationality.getSingleValue("select count(*) from TSPL_SCHEME_BENEFICIARY WHERE Scheme_Code ='" + Scheme_Code + "' and Cust_Code='" + Cust_Code + "'", trans)
                            If check <= 0 Then
                                checkExcution = checkExcution + 1
                                issaved = issaved AndAlso clsCommonFunctionality.UpdateDataTable(collBeneficiary, "TSPL_SCHEME_BENEFICIARY", OMInsertOrUpdate.Insert, "", trans)
                            Else
                                checkExcution = checkExcution + 1
                                issaved = issaved AndAlso clsCommonFunctionality.UpdateDataTable(collBeneficiary, "TSPL_SCHEME_BENEFICIARY", OMInsertOrUpdate.Update, " TSPL_SCHEME_BENEFICIARY.scheme_code='" + Scheme_Code + "' and TSPL_SCHEME_BENEFICIARY.Cust_Code='" + Cust_Code + "'", trans)
                            End If
                        End If
                        If checkExcution = 2 Then
                            Dim collSchemeDetails As New Hashtable()
                            clsCommon.AddColumnsForChange(collSchemeDetails, "Scheme_Code", Scheme_Code)
                            clsCommon.AddColumnsForChange(collSchemeDetails, "MainItem_code", MainItem_Code)
                            clsCommon.AddColumnsForChange(collSchemeDetails, "MainQty", MainQty)
                            clsCommon.AddColumnsForChange(collSchemeDetails, "MainUnit_Code", MainUnit_Code)
                            clsCommon.AddColumnsForChange(collSchemeDetails, "Item_Code", Scheme_Item)
                            clsCommon.AddColumnsForChange(collSchemeDetails, "qty", Scheme_Qty)
                            clsCommon.AddColumnsForChange(collSchemeDetails, "Unit_Code", Scheme_Unit)

                            check = clsDBFuncationality.getSingleValue("select count(*) from TSPL_SCHEME_DETAIL_NEW WHERE Scheme_Code ='" + Scheme_Code + "' and TSPL_SCHEME_DETAIL_NEW.MainItem_code='" + MainItem_Code + "'", trans)
                            If check <= 0 Then

                                issaved = issaved AndAlso clsCommonFunctionality.UpdateDataTable(collSchemeDetails, "TSPL_SCHEME_DETAIL_NEW", OMInsertOrUpdate.Insert, "", trans)
                            Else
                                issaved = issaved AndAlso clsCommonFunctionality.UpdateDataTable(collSchemeDetails, "TSPL_SCHEME_DETAIL_NEW", OMInsertOrUpdate.Update, " TSPL_SCHEME_DETAIL_NEW.scheme_code='" + Scheme_Code + "' and TSPL_SCHEME_DETAIL_NEW.MainItem_code='" + MainItem_Code + "'", trans)
                            End If
                        End If

                        ''''''''''''''''''''''

                        Min_Range = clsCommon.myCstr(grow.Cells("Min_Range").Value)
                        Range_Value = clsCommon.myCstr(grow.Cells("Value").Value)
                        Dim coll As New Hashtable()
                        clsCommon.AddColumnsForChange(coll, "Scheme_Code", Scheme_Code)
                        clsCommon.AddColumnsForChange(coll, "Min_Range", Min_Range)
                        clsCommon.AddColumnsForChange(coll, "Value", Range_Value)

                        issaved = issaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SCHEME_MASTER_NEW_SLAB", OMInsertOrUpdate.Insert, "", trans)
                        ''''''''''''''''

                        counter += 1

                    Next

                    trans.Commit()
                    clsCommon.ProgressBarHide()
                    common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)

                Catch ex As Exception
                    trans.Rollback()
                    clsCommon.ProgressBarHide()
                    Throw New Exception(ex.Message)
                End Try
            End If
            Me.Controls.Remove(gv)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub ChkQuantativeSch_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles ChkQuantativeSch.ToggleStateChanged
        Try
            lblQuantum.Visible = ChkQuantativeSch.Checked
            txtQuantum.Visible = ChkQuantativeSch.Checked
            btnApply.Visible = ChkQuantativeSch.Checked
            gvTS2.Visible = ChkQuantativeSch.Checked
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub QuantativeSchemeWithSlab()
        Try
            Dim Start As Integer = 0
            Dim Last As Integer = 0
            Dim SchemeQty As Integer = 0
            Dim SchemeQtyWithQuantum As Integer = 0
            Dim Quantum As Integer = 0
            Quantum = clsCommon.myCdbl(txtQuantum.Value)
            SlabChangeFlag = False
            For Each grow As GridViewRowInfo In gvTS.Rows
                If (Start > 0) AndAlso (Last Mod Quantum) = 0 Then
                    SlabChangeFlag = True
                End If

                Start = clsCommon.myCdbl(grow.Cells(colRange).Value)
                Last = clsCommon.myCdbl(grow.Cells(colTo).Value)

                SchemeQty = clsCommon.myCdbl(grow.Cells(colRate).Value)
                SchemeQtyWithQuantum = SchemeQty
                While (Start < Last)
                    gvTS2.Rows.AddNew()
                    gvTS2.Rows(gvTS2.Rows.Count - 1).Cells(colRange).Value = Start
                    Start = Start + Quantum - 1
                    If Start <= Last Then
                        If SlabChangeFlag = True Then
                            Start = Start - 1
                            SlabChangeFlag = False
                        End If
                        gvTS2.Rows(gvTS2.Rows.Count - 1).Cells(colTo).Value = Start
                    Else
                        gvTS2.Rows(gvTS2.Rows.Count - 1).Cells(colTo).Value = Last
                    End If
                    SchemeQtyWithQuantum = (CInt(Start / Quantum) - 1) * SchemeQty
                    gvTS2.Rows(gvTS2.Rows.Count - 1).Cells(colRate).Value = SchemeQtyWithQuantum
                    Start = Start + 1
                    If (Start = Last) AndAlso (Last Mod Quantum) = 0 Then
                        gvTS2.Rows.AddNew()
                        gvTS2.Rows(gvTS2.Rows.Count - 1).Cells(colRange).Value = Start
                        gvTS2.Rows(gvTS2.Rows.Count - 1).Cells(colTo).Value = Last
                        gvTS2.Rows(gvTS2.Rows.Count - 1).Cells(colRate).Value = CInt(Start / Quantum) * SchemeQty
                    End If

                End While

            Next
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub btnApply_Click(sender As Object, e As EventArgs) Handles btnApply.Click
        Try
            If ChkQuantativeSch.Checked = False Then
                Throw New Exception("Check Quantative Scheme With Slab.")
            End If
            If clsCommon.myCdbl(txtQuantum.Value) <= 0 Then
                txtQuantum.Focus()
                Throw New Exception("Please Fill Quantative Value.")
            End If
            gvTS2.Rows.Clear()
            QuantativeSchemeWithSlab()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub txtStrcutCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtStrcutCode._MYValidating
        Dim qry = "select Structure_Code as Code,Structure_Descq as [Structure Desc],Item_Structure as structure,Total_Length as TotalLength from tspl_structure_master"
        txtStrcutCode.Value = clsCommon.ShowSelectForm("StructureFinder@SCHMMD", qry, "Code", "", txtStrcutCode.Value, "", isButtonClicked)
        lblStructDesc.Text = clsDBFuncationality.getSingleValue("Select Structure_Descq from tspl_structure_master WHERE Structure_Code ='" + txtStrcutCode.Value + "'")
    End Sub

    Private Sub txtstructUnit__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtstructUnit._MYValidating
        Dim qry = "select unit_code as Code ,unit_desc as [Unit Desc] from TSPL_UNIT_MASTER "
        txtstructUnit.Value = clsCommon.ShowSelectForm("StructUnitFinder@SCHMMD", qry, "Code", " isnull(Weight_Type,'N')='Y' ", txtstructUnit.Value, "", isButtonClicked)
        lblStructUnit.Text = clsDBFuncationality.getSingleValue("Select unit_desc from TSPL_UNIT_MASTER WHERE unit_code ='" + txtstructUnit.Value + "'")
    End Sub

    Private Sub gvVolumeSlab_CellValueChanged(sender As Object, e As GridViewCellEventArgs) Handles gvVolumeSlab.CellValueChanged
        Try
            If (Not isInsideLoadData) Then
                If e.Column.Name = colTo And clsCommon.myCdbl(e.Value) > 0 Then
                    gvVolumeSlab.Rows(gvVolumeSlab.CurrentRow.Index + 1).Cells(colRange).Value = clsCommon.myCdbl(gvVolumeSlab.Rows(gvVolumeSlab.CurrentRow.Index).Cells(colTo).Value) + 0.01
                ElseIf e.Column Is gvVolumeSlab.Columns(colICode) Then
                    qry = "Select Item_Code as Code, Item_Desc as Description from TSPL_ITEM_MASTER"
                    Dim Whr As String = ""
                    If AllowSchemeItems AndAlso clsCommon.myLen(Whr) > 0 Then
                        Whr += " and coalesce(is_scheme_item,0)=1 "
                    ElseIf AllowSchemeItems AndAlso clsCommon.myLen(Whr) <= 0 Then
                        ' Whr += " coalesce(is_scheme_item,0)=1 "
                    End If
                    gvVolumeSlab.CurrentRow.Cells(colICode).Value = clsCommon.ShowSelectForm("ItemCodefind@SMD", qry, "Code", Whr, gvVolumeSlab.CurrentRow.Cells(colICode).Value, "Code", False)
                    gvVolumeSlab.CurrentRow.Cells(colIName).Value = clsItemMaster.GetItemName(gvVolumeSlab.CurrentRow.Cells(colICode).Value, Nothing)
                End If
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub gvVolumeSlab_CurrentColumnChanged(sender As Object, e As CurrentColumnChangedEventArgs) Handles gvVolumeSlab.CurrentColumnChanged
        If gvVolumeSlab.RowCount > 0 Then
            Dim intCurrRow As Integer = gvVolumeSlab.CurrentRow.Index
            If intCurrRow = gvVolumeSlab.Rows.Count - 1 Then
                gvVolumeSlab.Rows.AddNew()
                gvVolumeSlab.CurrentRow = gvVolumeSlab.Rows(intCurrRow)
            End If
        End If
    End Sub

    Private Sub gvVolumeSlab_EditorRequired(sender As Object, e As EditorRequiredEventArgs) Handles gvVolumeSlab.EditorRequired
        Try
            Dim unit As GridViewComboBoxColumn = TryCast(gvVolumeSlab.Columns(colUOM), GridViewComboBoxColumn)
            Dim ds As New DataSet()
            If gvVolumeSlab.CurrentColumn.Name = colUOM Then
                qry = "select UOM_Code as Unit from TSPL_ITEM_UOM_DETAIL where Item_Code = '" + gvVolumeSlab.CurrentRow.Cells(colICode).Value + "'"
                unit.DataSource = clsDBFuncationality.GetDataTable(qry)
                unit.ValueMember = "Unit"
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try

    End Sub

    Private Sub gvVolumeSlab_UserDeletedRow(sender As Object, e As GridViewRowEventArgs) Handles gvVolumeSlab.UserDeletedRow
        Try
            If gvVolumeSlab.CurrentRow.Index > 0 Then
                gvVolumeSlab.Rows(gvVolumeSlab.CurrentRow.Index - 1).Cells(colTo).Value = clsCommon.myCdbl(clsCommon.myCdbl(gvVolumeSlab.Rows(gvVolumeSlab.CurrentRow.Index).Cells(colRange).Value) - 1)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub gvVolumeSlab_UserDeletingRow(sender As Object, e As GridViewRowCancelEventArgs) Handles gvVolumeSlab.UserDeletingRow
        If Not myMessages.deleteConfirm() Then
            e.Cancel = True
        End If
    End Sub

    Private Sub cboQuantitiveType_SelectedIndexChanged(sender As Object, e As UI.Data.PositionChangedEventArgs) Handles cboQuantitiveType.SelectedIndexChanged
        Try
            gvItem.Columns(colMaxLimit).IsVisible = False
            gvItem.Columns(colIncrementValue).IsVisible = False
            If clsCommon.myCdbl(cboQuantitiveType.SelectedValue) = 1 Then
                gvItem.Columns(colMaxLimit).IsVisible = True
            ElseIf clsCommon.myCdbl(cboQuantitiveType.SelectedValue) = 2 Then
                gvItem.Columns(colIncrementValue).IsVisible = True
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtQuantitiveStructureCode__My_Click(sender As Object, e As EventArgs) Handles txtQuantitiveStructureCode._My_Click
        Dim qry = "select Structure_Code as Code,Structure_Descq as [Structure Desc],Item_Structure as structure,Total_Length as TotalLength from tspl_structure_master"
        txtQuantitiveStructureCode.arrValueMember = clsCommon.ShowMultipleSelectForm("Stru@SCHMMD", qry, "Code", Nothing, txtQuantitiveStructureCode.arrValueMember, Nothing)
    End Sub

    Private Sub txtQuantitiveStructureMainUOM__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtQuantitiveStructureMainUOM._MYValidating
        Dim qry = "select Unit_Code as Code,Unit_Desc as Name from TSPL_UNIT_MASTER"
        txtQuantitiveStructureMainUOM.Value = clsCommon.ShowSelectForm("UOMFFins@SCD", qry, "Code", "", txtQuantitiveStructureMainUOM.Value, "", isButtonClicked)
    End Sub

    Private Sub txtQuantitiveStructureFreeICode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtQuantitiveStructureFreeICode._MYValidating
        qry = "Select Item_Code as Code, Item_Desc as Description from TSPL_ITEM_MASTER"
        Dim Whr As String = ""
        txtQuantitiveStructureFreeICode.Value = clsCommon.ShowSelectForm("ItemCodeId@SMD", qry, "Code", Whr, txtQuantitiveStructureFreeICode.Value, "Code", isButtonClicked)
        gvItem.CurrentRow.Cells(colIName).Value = clsItemMaster.GetItemName(txtQuantitiveStructureFreeICode.Value, Nothing)
        txtQuantitiveStructureFreeUOM.Value = ""
    End Sub

    Private Sub txtQuantitiveStructureFreeUOM__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtQuantitiveStructureFreeUOM._MYValidating ''ERO/27/02/19-000501 by balwinder on 11/03/2019
        If clsCommon.myLen(txtQuantitiveStructureFreeICode.Value) <= 0 Then
            clsCommon.MyMessageBoxShow("Please First select Free Item code", Me.Text)
            txtQuantitiveStructureFreeICode.Focus()
            Exit Sub
        End If
        qry = "select UOM_Code as Code,UOM_Description as Description from TSPL_ITEM_UOM_DETAIL "
        Dim whrCls As String = " Item_Code ='" + txtQuantitiveStructureFreeICode.Value + "'"
        txtQuantitiveStructureFreeUOM.Value = clsCommon.ShowSelectForm("UOMFFinder@SCD", qry, "Code", whrCls, txtQuantitiveStructureFreeUOM.Value, "", isButtonClicked)

    End Sub

    Private Sub btn_Apply_Click(sender As Object, e As EventArgs) Handles btn_Apply.Click
        Try
            For ii As Integer = 0 To gvItem.Rows.Count - 1
                gvItem.Rows(ii).Cells(colMainIUnit).Value = clsCommon.myCstr(txtUnitCode.Value)
                gvItem.Rows(ii).Cells(colMainIQty).Value = clsCommon.myCdbl(txtQty.Value)
                gvItem.Rows(ii).Cells(colCashPer).Value = clsCommon.myCdbl(txtPercentage.Text)
                gvItem.Rows(ii).Cells(colCashAmt).Value = clsCommon.myCdbl(txtAmount.Text)
            Next
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub txtItemSturcture__My_Click(sender As Object, e As EventArgs) Handles txtItemSturcture._My_Click
        Try
            Dim qry = "select distinct Structure_Code as Code, Structure_Descq as Name from tspl_Structure_Master "
            txtItemSturcture.arrValueMember = clsCommon.ShowMultipleSelectForm("FND@Structure", qry, "Code", "Name", txtItemSturcture.arrValueMember, txtItemSturcture.arrDispalyMember)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub TxtCashDisunit__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles TxtCashDisunit._MYValidating
        Dim qry = "select unit_code as Code ,unit_desc as [Unit Desc] from TSPL_UNIT_MASTER "
        TxtCashDisunit.Value = clsCommon.ShowSelectForm("StructUnitFinder@SCHMMD", qry, "Code", " isnull(Weight_Type,'N')='Y' ", TxtCashDisunit.Value, "", isButtonClicked)
        lblCashDisunit.Text = clsDBFuncationality.getSingleValue("Select unit_desc from TSPL_UNIT_MASTER WHERE unit_code ='" + TxtCashDisunit.Value + "'")
    End Sub
    Sub LoadBlankVolumeSlabCashDisGrid()
        gvCashDisGrid.Rows.Clear()
        gvCashDisGrid.Columns.Clear()

        gvCashDisGrid.Columns.Add(colInLineNo, "SNo")
        gvCashDisGrid.Columns(colInLineNo).Width = 50
        gvCashDisGrid.Columns(colInLineNo).ReadOnly = True


        Dim repoFromRange As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoFromRange.FormatString = "{0:n2}"
        repoFromRange.HeaderText = "From Range"
        repoFromRange.Name = colFromRange
        repoFromRange.Width = 80
        repoFromRange.Minimum = 0
        repoFromRange.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvCashDisGrid.MasterTemplate.Columns.Add(repoFromRange)

        Dim repoToRange As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoToRange.FormatString = "{0:n2}"
        repoToRange.HeaderText = "To Range"
        repoToRange.Name = colToRange
        repoToRange.Width = 80
        repoToRange.Minimum = 0
        repoToRange.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvCashDisGrid.MasterTemplate.Columns.Add(repoToRange)

        Dim repoRangeUOM As GridViewComboBoxColumn = New GridViewComboBoxColumn()
        repoRangeUOM.HeaderText = "Unit Code"
        repoRangeUOM.Name = colRangeUom
        repoRangeUOM.Width = 80
        repoRangeUOM.ReadOnly = True
        gvCashDisGrid.MasterTemplate.Columns.Add(repoRangeUOM)

        Dim repIncentive As GridViewDecimalColumn = New GridViewDecimalColumn()
        repIncentive.FormatString = "{0:n2}"
        repIncentive.HeaderText = "Cash Discount Amt"
        repIncentive.Name = colCashDisAmt
        repIncentive.Width = 80
        repIncentive.Minimum = 0
        repIncentive.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvCashDisGrid.MasterTemplate.Columns.Add(repIncentive)

        Dim repoIncentiveUOM As GridViewComboBoxColumn = New GridViewComboBoxColumn()
        repoIncentiveUOM.HeaderText = "Per"
        repoIncentiveUOM.Name = colIncentiveUom
        repoIncentiveUOM.Width = 80
        repoIncentiveUOM.ReadOnly = True
        gvCashDisGrid.MasterTemplate.Columns.Add(repoIncentiveUOM)


        gvCashDisGrid.AllowAddNewRow = False
        gvCashDisGrid.AllowDeleteRow = True
        gvCashDisGrid.AllowRowReorder = False
        gvCashDisGrid.ShowGroupPanel = False
        gvCashDisGrid.EnableFiltering = False
        gvCashDisGrid.EnableSorting = False
        gvCashDisGrid.EnableGrouping = False
        gvCashDisGrid.AllowColumnChooser = True
        gvCashDisGrid.AllowColumnReorder = True
        gvCashDisGrid.Rows.AddNew()
    End Sub

    Private Sub gvCashDisGrid_CellFormatting(sender As Object, e As CellFormattingEventArgs) Handles gvCashDisGrid.CellFormatting
        If IsLoaded = True Then
            If e.Column Is gvCashDisGrid.Columns(colFromRange) OrElse e.Column Is gvCashDisGrid.Columns(colToRange) OrElse e.Column Is gvCashDisGrid.Columns(colCashDisAmt) Then
                If clsCommon.CompairString(gvCashDisGrid.CurrentRow.Index, 0) = CompairStringResult.Equal Then
                    gvCashDisGrid.CurrentRow.Cells(colFromRange).ReadOnly = False
                Else
                    gvCashDisGrid.CurrentRow.Cells(colFromRange).ReadOnly = True
                End If
                If String.IsNullOrEmpty(clsCommon.myCstr(gvCashDisGrid.CurrentRow.Cells(colFromRange).Value)) = True Then
                    gvCashDisGrid.CurrentRow.Cells(colToRange).ReadOnly = True
                Else
                    gvCashDisGrid.CurrentRow.Cells(colToRange).ReadOnly = False

                End If
            End If
        End If
    End Sub

    Private Sub gvCashDisGrid_CellValueChanged(sender As Object, e As GridViewCellEventArgs) Handles gvCashDisGrid.CellValueChanged
        Try
            If (Not isInsideLoadData) Then
                If Not isCellValueChangedOpen Then
                    isCellValueChangedOpen = True
                    If e.Column Is gvCashDisGrid.Columns(colFromRange) Then
                        FillUom(False)
                    ElseIf e.Column Is gvCashDisGrid.Columns(colToRange) Then
                        FillUom(False)
                        gvCashDisGrid.Rows(gvCashDisGrid.CurrentRow.Index + 1).Cells(colFromRange).Value = clsCommon.myCdbl(gvCashDisGrid.Rows(gvCashDisGrid.CurrentRow.Index).Cells(colToRange).Value + 0.01)
                        If String.IsNullOrEmpty(clsCommon.myCstr(gvCashDisGrid.Rows(gvCashDisGrid.CurrentRow.Index + 1).Cells(colToRange).Value)) = True Then
                            gvCashDisGrid.Rows.AddNew()
                            gvCashDisGrid.CurrentRow.Cells(colLineNo).Value = clsCommon.myCdbl(gvCashDisGrid.CurrentRow.Index + 1)
                            gvCashDisGrid.Rows(gvCashDisGrid.CurrentRow.Index + 1).Cells(colToRange).Value = 999999.0
                            gvCashDisGrid.CurrentRow.Cells(colRangeUom).Value = txtRangeUnit.Value
                            gvCashDisGrid.CurrentRow.Cells(colIncentiveUom).Value = TxtCashDisunit.Value
                        End If

                    End If
                    isCellValueChangedOpen = False
                End If
            End If
        Catch ex As Exception
            isCellValueChangedOpen = False
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub gvCashDisGrid_CurrentColumnChanged(sender As Object, e As CurrentColumnChangedEventArgs) Handles gvCashDisGrid.CurrentColumnChanged
        If gvCashDisGrid.RowCount > 0 Then
            Dim intCurrRow As Integer = gvCashDisGrid.CurrentRow.Index
            gvCashDisGrid.CurrentRow.Cells(colInLineNo).Value = clsCommon.myCdbl(intCurrRow + 1)
            If intCurrRow = gvCashDisGrid.Rows.Count - 1 Then
                gvCashDisGrid.Rows.AddNew()
                gvCashDisGrid.CurrentRow = gvCashDisGrid.Rows(intCurrRow)
            End If
        End If
    End Sub

    Private Sub gvCashDisGrid_CurrentRowChanged(sender As Object, e As CurrentRowChangedEventArgs) Handles gvCashDisGrid.CurrentRowChanged
        If gvCashDisGrid.RowCount > 0 Then
            Dim intCurrRow As Integer = gvCashDisGrid.CurrentRow.Index
            gvCashDisGrid.CurrentRow.Cells(colInLineNo).Value = clsCommon.myCdbl(intCurrRow + 1)
            If intCurrRow = gvCashDisGrid.Rows.Count - 1 Then
                gvCashDisGrid.CurrentRow = gvCashDisGrid.Rows(intCurrRow)
            End If
        End If
    End Sub
    Sub FillUom(ByVal isButtonClick As Boolean)
        gvCashDisGrid.CurrentRow.Cells(colRangeUom).Value = txtRangeUnit.Value
        gvCashDisGrid.CurrentRow.Cells(colIncentiveUom).Value = TxtCashDisunit.Value
        If gvCashDisGrid.CurrentRow.Index = 0 Then
            gvCashDisGrid.CurrentRow.Cells(colFromRange).ReadOnly = False
            If gvCashDisGrid.Rows.Count > 1 Then
                ' If String.IsNullOrEmpty(clsCommon.myCstr(gvCashDisGrid.Rows(gvCashDisGrid.CurrentRow.Index + 1).Cells(colFromRange).Value)) = False Then
                If String.IsNullOrEmpty(gvCashDisGrid.Rows(gvCashDisGrid.CurrentRow.Index).Cells(colToRange).Value) = True Then
                    gvCashDisGrid.Rows(gvCashDisGrid.CurrentRow.Index).Cells(colToRange).Value = 999999.0
                End If

                'End If

            End If
        Else
            gvCashDisGrid.CurrentRow.Cells(colFromRange).ReadOnly = True
        End If
    End Sub

    Private Sub txtRangeUnit__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtRangeUnit._MYValidating
        qry = "select Unit_Code as Code,Unit_Desc as Description from TSPL_UNIT_MASTER "
        Dim whrCls As String = " "
        txtRangeUnit.Value = clsCommon.ShowSelectForm("RangeUOMFinder@SCHMMD", qry, "Code", whrCls, txtRangeUnit.Value, "", isButtonClicked)
        If gvCashDisGrid.RowCount > 0 Then
            For ii As Integer = 0 To gvCashDisGrid.RowCount - 1
                If clsCommon.myCdbl(gvCashDisGrid.Rows(ii).Cells(colToRange).Value) > 0 Then
                    gvCashDisGrid.Rows(ii).Cells(colRangeUom).Value = txtRangeUnit.Value
                End If
            Next
        End If
    End Sub
End Class

