''18/06/2012---Updation by --[Pankaj kumar]-- Commented Grid Formating Event Code So that Grid Cell's Color Could Not be change on clicking 
'' Added By Abhishek as on 30 Nov 2012 4:16 Pm For Location Lock
'' By vipin for psoting status check on update on 05/02/2013
''updation by richa agarwal against ticket no BM00000003745(add condition in vendor finder)
Imports System
Imports common
Imports Microsoft.Office.Interop
Imports System.IO
Imports System.Data.SqlClient


Public Class frmStoreRequistion
    Inherits FrmMainTranScreen
#Region "Variables"
    Dim StoreRequisitionMandatory As Boolean
    Dim ApplyDepartmentWiseDataVisibleInDepartmentIndent As Boolean
    Public atchqry As String = ""
    Dim vaddnew As String
    Dim closeyn As String
    Private isCellValueChangedOpen As Boolean = False
    Dim iStxtTaxGroup_TxtChangedComplete As Boolean = True
    Private isNewEntry As Boolean = False
    Private isInsideLoadData As Boolean = False
    Private IsFormLoad As Boolean = False
    Const colLineNo As String = "LNO"
    Const colComplete As String = "COMPLETE"
    Const colICode As String = "ICODE"
    Const colIName As String = "INAME"
    Const colCCCode As String = "COLCCCode"
    Const colCCDesc As String = "COLCCDesc"
    Const colBalanceQty As String = "BALANCEQTY"
    Const colQty As String = "QTY"
    Const colUnit As String = "COLTAX3"
    Const colRate As String = "RATE"
    Const colAmt As String = "AMT"
    Const colVendorCode As String = "VCODE"
    Const colVendorName As String = "VNAME"
    ''Const colLocationCode As String = "LOCATION"
    ''Const colLocationName As String = "LOCATIONNAME"
    Const colVendorItemNo As String = "VENDORITEMNO"
    Const colOrderNo As String = "ORDERNO"

    Const colSpecification As String = "SPECIFICATION"
    Const colRemarks As String = "REMARKS"
    Const colHierarchyCode As String = "colHierarchyCode"
    Const colCostCenterCode As String = "colCostCenterCode"
    Const colHierarchyLevel2 As String = "colHierarchyLevel2"
    Const colHierarchyLevel3 As String = "colHierarchyLevel3"
    Dim ButtonToolTip As ToolTip = New ToolTip()


    Dim repoBalQty As GridViewDecimalColumn
    Dim repoComplete As GridViewTextBoxColumn

    Public strDocumentNo As String = ""

    '--------------richa 14/07/2014 Ticket No BM00000003042---------
    Public ArrItem As List(Of clsItemMaster)
    Dim ChkAutoDepOnPurchaseCycle As Boolean = False

    ''
    Public ShowItemAllStructureWise As Boolean = False
    Dim EnableHirerachyCostCentre As Double = 0
    Dim ShowCapexCodeandSubCode As Boolean = False
    Dim EnableStoreCostCentre As Double = 0
    Dim ShowDefaultUser As Boolean = False
#End Region
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmStoreRequistion)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnPost.Visible = MyBase.isPostFlag
        btnDelete.Visible = MyBase.isDeleteFlag
        btnPrint.Visible = MyBase.isPrintFlag
        If MyBase.isReverse Then
            btnUnpost.Enabled = True
        Else
            btnUnpost.Enabled = False
        End If
        btncancel.Visible = MyBase.isCancel_Flag_After_Posting
    End Sub

    Private Sub FrmAPInvoiceEntry_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        'If Not clsCommon.CompairString(objCommonVar.CurrentUserCode, "ADMIN") = CompairStringResult.Equal Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If
        '====Sanjeet (For Cost Centre Hiererachy level)====
        EnableHirerachyCostCentre = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.EnableHirerachyCostCentre, clsFixedParameterCode.EnableHirerachyCostCentre, Nothing))
        EnableStoreCostCentre = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.EnableStoreCostCentre, clsFixedParameterCode.EnableStoreCostCentre, Nothing))
        ShowDefaultUser = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ShowDefaultUser, clsFixedParameterCode.ShowDefaultUser, Nothing)) = 1, True, False)
        '========End=================
        ShowItemAllStructureWise = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ShowItemAllStructureWise, clsFixedParameterCode.ShowItemAllStructureWise, Nothing)) = 1, True, False)


        StoreRequisitionMandatory = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.StoreRequisitionMandatoryforstorerequest, clsFixedParameterCode.StoreRequisitionMandatoryforstorerequest, Nothing)) = 1, True, False)

        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction")
        ButtonToolTip.SetToolTip(btnPost, "Press Alt+P Post Trasnaction")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnAddNew, "Press Alt+N Adding New Trasnaction")
        RadPageView1.SelectedPage = RadPageViewPage1
        ShowCapexCodeandSubCode = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.ShowOptionforSelectingCapex, clsFixedParameterCode.ShowOptionforSelectingCapex, Nothing)) = "1", True, False))
        ApplyDepartmentWiseDataVisibleInDepartmentIndent = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ApplyDepartmentWiseDataVisibleInDepartmentIndent, clsFixedParameterCode.ApplyDepartmentWiseDataVisibleInDepartmentIndent, Nothing)) = 1, True, False)
        'Hide Capex,SubCapex Detail  Ticket No-UDL/07/05/18-000154
        'If ShowCapexCodeandSubCode Then
        '    SplitContainer3.Panel1Collapsed = False
        'Else
        SplitContainer3.Panel1Collapsed = True
        'End If
        LoadBlankGrid()
        LoadItemType()
        IsFormLoad = True
        IsFormLoad = False
        AddNew()
        SetLength()
        btnUnpost.Visible = False

        ''For Custom Fields
        RadPageView1.Pages("pvpCustomFields").Item.Visibility = MyBase.customFieldTabProperty
        If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
            UcCustomFields1.Report_ID = MyBase.Form_ID
            UcCustomFields1.LoadCustomControls()
        End If
        ''End of For Custom Fields

        ''For Attachment
        If objCommonVar.IsDemoERP Then
            UcAttachment1.Form_ID = MyBase.Form_ID
            RadPageView1.Pages("Attachments").Item.Visibility = ElementVisibility.Visible
        Else
            RadPageView1.Pages("Attachments").Item.Visibility = ElementVisibility.Collapsed
        End If
        ''End of For Attachment
        If Not objCommonVar.IsDemoERP Then
            pnlPCJ.Visible = False
        End If
        '--------------richa 14/07/2014 Ticket No BM00000003042---------
        LoadItems()

        '--------------------------------------------------------------
        If EnableStoreCostCentre = 1 Then
            pnlUnit_CostType.Visible = True
            txtDept.Enabled = False
        Else
            pnlUnit_CostType.Visible = False
            txtDept.Enabled = True
        End If

        If clsCommon.myLen(strDocumentNo) > 0 Then
            LoadData(strDocumentNo, NavigatorType.Current)
        End If
        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
        chkInternal.Checked = True
        RdEmailAndSmsSetting.Visibility = ElementVisibility.Collapsed
        btncancel.Enabled = False
    End Sub
    Sub AllowDepartmentMandatoryOnPurchaseCycle()
        ChkAutoDepOnPurchaseCycle = IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AutoDepartmentMendatroryFieldOnPurcahseCycle, clsFixedParameterCode.AutoDepartmentMendatroryFieldOnPurcahseCycle, Nothing)) = "1", True, False)
        If ChkAutoDepOnPurchaseCycle Then
            txtDept.Enabled = False
            txtDept.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Segment_code from TSPL_USER_MASTER where User_Code ='" + objCommonVar.CurrentUserCode + "'"))
            'txtDept.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select top 1 TSPL_GL_SEGMENT_CODE.Segment_code from TSPL_COST_CENTER_TYPE_MASTER inner  join TSPL_GL_SEGMENT_CODE ON TSPL_GL_SEGMENT_CODE.Segment_code=TSPL_COST_CENTER_TYPE_MASTER.Department_Cost Where TSPL_GL_SEGMENT_CODE.Seg_No=3 Order By TSPL_COST_CENTER_TYPE_MASTER.Created_Date Desc"))
            ' Ticket No : UDL/22/05/18-000172 By Prabhakar
            lblDept.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select top 1 Description from TSPL_GL_SEGMENT_CODE where Seg_No=3 and Segment_code='" + txtDept.Value + "' "))
            Dim qry As String = "Select TSPL_GL_SEGMENT_CODE.Segment_code as Code, TSPL_GL_SEGMENT_CODE.Description as Name,TSPL_COST_CENTER_TYPE_MASTER.Unit_Code,TSPL_COST_CENTER_TYPE_MASTER.Code as COST_CENTER_TYPE_Code from TSPL_COST_CENTER_TYPE_MASTER
            left outer join TSPL_GL_SEGMENT_CODE on TSPL_GL_SEGMENT_CODE.Segment_code = TSPL_COST_CENTER_TYPE_MASTER.Department_Cost where  Segment_code = '" & txtDept.Value & "'"

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            txtUnitCode.Value = dt.Rows(0)("Unit_Code")
            If clsCommon.myLen(txtUnitCode.Value) > 0 Then
                lblUnitDesc.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description from TSPL_COST_CENTER_UNIT_MASTER where Code='" + txtUnitCode.Value + "'"))
            Else
                lblUnitDesc.Text = ""
            End If
            txtCostCenterType.Value = dt.Rows(0)("COST_CENTER_TYPE_Code")
            If clsCommon.myLen(txtCostCenterType.Value) > 0 Then
                lblCostcenterTypeDesc.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description from TSPL_COST_CENTER_TYPE_MASTER where Code='" + txtCostCenterType.Value + "'"))
            Else
                lblCostcenterTypeDesc.Text = ""
            End If
        Else
            txtDept.Enabled = True
        End If
    End Sub


    Sub SetLength()
        txtReqNo.MyMaxLength = 30
        txtDesc.MaxLength = 200
        txtRmks.MaxLength = 200
        txtComment.MaxLength = 200
    End Sub
    Public Shared Function GetItemall() As DataTable
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))
        Dim dr As DataRow = dt.NewRow()
        dr("Code") = "Z"
        dr("Name") = "All"
        dt.Rows.Add(dr)
        dr = dt.NewRow()
        Return dt
    End Function
    Sub LoadItemType()
        If ShowItemAllStructureWise = True Then
            cboItemType.DataSource = GetItemall()
            cboItemType.ValueMember = "Code"
            cboItemType.DisplayMember = "Name"
        Else
            ' cboItemType.DataSource = clsItemMaster.GetItemType()
            Dim Whr = " AND IS_NON_INVENTORY=0   AND ITEM_TYPE_CODE NOT IN('J') "
        cboItemType.DataSource = clsItemMaster.getItemTypeQuery(Whr)
        cboItemType.ValueMember = "Code"
            cboItemType.DisplayMember = "Name"
        End If
    End Sub

    Sub BlankAllControls()
        txtReqNo.Value = ""
        txtDesc.Text = ""
        txtLocation.Text = ""

        vaddnew = "Y"
        chkprclose.Checked = False

        txtRmks.Text = ""
        txtLocation.Value = ""
        lblLocation.Text = ""
        txtComment.Text = ""
        chkInternal.Checked = True
        txtDate.Value = clsCommon.myCDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy")
        txtExpireDate.Value = txtDate.Value
        txtRequiredDate.Value = txtDate.Value
        lblTotRAmt.Text = ""
        txtDept.Value = ""
        lblDept.Text = ""
        txtRequestBy.Value = ""
        lblRequestBy.Text = ""
        ''For Custom Fields
        If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
            UcCustomFields1.BlankAllControls()
        End If
        ''End of For Custom Fields
        '====CLEAR CAPEX DETAIL=====
        lbl_capexcode.Text = ""
        lbl_capexsubcode.Text = ""
        fndcapexcode.Value = ""
        fndcapexsubcode.Value = ""
        lbl_budgetamt.Text = ""
        lbl_budgetamtwithtolerence.Text = ""
        lbl_rebudgetamt.Text = ""
        lbl_rebudgetamtwithtolerence.Text = ""
        '===========================
        txtUnitCode.Value = ""
        lblUnitDesc.Text = ""
        txtCostCenterType.Value = ""
        lblCostcenterTypeDesc.Text = ""
        fndProject.Value = ""
        lblProject.Text = ""
        btncancel.Enabled = False
        UcAttachment1.BlankAllControls()
    End Sub

    Sub LoadBlankGrid()
        gv1.Rows.Clear()
        gv1.Columns.Clear()

        Dim repoLineNo As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoLineNo = New GridViewDecimalColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "Line"
        repoLineNo.Name = colLineNo
        repoLineNo.Width = 30
        repoLineNo.ReadOnly = True
        repoLineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoLineNo)

        repoComplete = New GridViewTextBoxColumn()
        repoComplete.FormatString = ""
        repoComplete.HeaderText = "Complete"
        repoComplete.Width = 70
        repoComplete.Name = colComplete
        repoComplete.ReadOnly = True
        repoComplete.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoComplete)


        Dim repoICode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoICode.FormatString = ""
        repoICode.HeaderText = "Item Code"
        repoICode.Name = colICode
        repoICode.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoICode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoICode.Width = 100
        gv1.MasterTemplate.Columns.Add(repoICode)

        Dim repoIName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoIName.FormatString = ""
        repoIName.HeaderText = "Item"
        repoIName.Name = colIName
        repoIName.Width = 150
        repoIName.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoIName)

        repoBalQty = New GridViewDecimalColumn()
        repoBalQty.FormatString = ""
        repoBalQty.WrapText = True
        repoBalQty.HeaderText = "Balance Quantity"
        repoBalQty.Name = colBalanceQty
        repoBalQty.Width = 80
        repoBalQty.Minimum = 0
        repoBalQty.IsVisible = False
        repoBalQty.ReadOnly = True
        repoBalQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoBalQty)

        Dim repoQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoQty = New GridViewDecimalColumn()
        repoQty.FormatString = ""
        repoQty.HeaderText = "Req. Quantity"
        repoQty.Name = colQty
        repoQty.Width = 80
        repoQty.Minimum = 0
        repoQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoQty)

        Dim repoUnit As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoUnit.FormatString = ""
        repoUnit.HeaderText = "UOM"
        repoUnit.Name = colUnit
        repoUnit.ReadOnly = False
        repoUnit.Width = 80
        gv1.MasterTemplate.Columns.Add(repoUnit)

        Dim repoRate As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoRate = New GridViewDecimalColumn()
        repoRate.FormatString = ""
        repoRate.HeaderText = "Unit Cost"
        repoRate.Name = colRate
        repoRate.Width = 80
        repoRate.Minimum = 0
        repoRate.FormatString = "{0:n4}"
        repoRate.DecimalPlaces = 4
        repoRate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoRate)

        Dim repoAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAmt = New GridViewDecimalColumn()
        repoAmt.FormatString = ""
        repoAmt.HeaderText = "Amount"
        repoAmt.Name = colAmt
        repoAmt.Width = 80
        repoAmt.ReadOnly = True
        repoAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoAmt)

        Dim repoVendorCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoVendorCode = New GridViewTextBoxColumn()
        repoVendorCode.FormatString = ""
        repoVendorCode.HeaderText = "Vendor Code"
        repoVendorCode.Name = colVendorCode
        repoVendorCode.Width = 100
        repoVendorCode.IsVisible = False
        repoVendorCode.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoVendorCode.TextImageRelation = TextImageRelation.TextBeforeImage
        gv1.MasterTemplate.Columns.Add(repoVendorCode)

        Dim repoVendorName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoVendorName = New GridViewTextBoxColumn()
        repoVendorName.FormatString = ""
        repoVendorName.HeaderText = "Vendor"
        repoVendorName.Name = colVendorName
        repoVendorName.Width = 150
        repoVendorName.IsVisible = False
        repoVendorName.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoVendorName)

        ''Dim repoLCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        ''repoLCode = New GridViewTextBoxColumn()
        ''repoLCode.FormatString = ""
        ''repoLCode.HeaderText = "Location Code"
        ''repoLCode.Name = colLocationCode
        ''repoLCode.Width = 100
        ''repoLCode.HeaderImage = Global.ERP.My.Resources.Resources.search4
        ''repoLCode.TextImageRelation = TextImageRelation.TextBeforeImage
        ''gv1.MasterTemplate.Columns.Add(repoLCode)

        ''Dim repoLName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        ''repoLName = New GridViewTextBoxColumn()
        ''repoLName.FormatString = ""
        ''repoLName.HeaderText = "Location"
        ''repoLName.Name = colLocationName
        ''repoLName.Width = 150
        ''repoLName.ReadOnly = True
        ''gv1.MasterTemplate.Columns.Add(repoLName)

        Dim repoVendroItemNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoVendroItemNo = New GridViewTextBoxColumn()
        repoVendroItemNo.FormatString = ""
        repoVendroItemNo.HeaderText = "Vendor Item No"
        repoVendroItemNo.Name = colVendorItemNo
        repoVendroItemNo.IsVisible = False
        repoVendroItemNo.Width = 100
        gv1.MasterTemplate.Columns.Add(repoVendroItemNo)

        Dim repoOrderNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoOrderNo = New GridViewTextBoxColumn()
        repoOrderNo.FormatString = ""
        repoOrderNo.HeaderText = "Order No"
        repoOrderNo.Name = colOrderNo
        repoOrderNo.Width = 100
        repoOrderNo.IsVisible = False
        repoOrderNo.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoOrderNo.TextImageRelation = TextImageRelation.TextBeforeImage
        gv1.MasterTemplate.Columns.Add(repoOrderNo)

        Dim repoSpecification As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSpecification = New GridViewTextBoxColumn()
        repoSpecification.FormatString = ""
        repoSpecification.HeaderText = "Specification"
        repoSpecification.Name = colSpecification
        repoSpecification.Width = 100
        gv1.MasterTemplate.Columns.Add(repoSpecification)


        Dim repoRemarks As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoRemarks = New GridViewTextBoxColumn()
        repoRemarks.FormatString = ""
        repoRemarks.HeaderText = "Remarks"
        repoRemarks.Name = colRemarks
        repoRemarks.Width = 100
        gv1.MasterTemplate.Columns.Add(repoRemarks)



        ' '==========added by richa agarwal 1 Dec,2016 BM00000010390

        Dim CostCenter As String = clsFixedParameter.GetData(clsFixedParameterType.ShowCostCenterAndHierarchyLevelInPurchaseModule, clsFixedParameterCode.ShowCostCenterAndHierarchyLevelInPurchaseModule, Nothing)
        If CostCenter = "1" Then
            Dim repoHierarchyCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()

            repoHierarchyCode.FormatString = ""
            repoHierarchyCode.HeaderText = "Hierarchy Level"
            repoHierarchyCode.Name = colHierarchyCode
            repoHierarchyCode.HeaderImage = Global.ERP.My.Resources.Resources.search4
            repoHierarchyCode.TextImageRelation = TextImageRelation.TextBeforeImage
            repoHierarchyCode.Width = 150
            gv1.MasterTemplate.Columns.Add(repoHierarchyCode)

            Dim repoCostCenterCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            repoCostCenterCode.FormatString = ""
            repoCostCenterCode.HeaderText = "Hierarchy Level1"
            repoCostCenterCode.Name = colCostCenterCode
            repoCostCenterCode.HeaderImage = Global.ERP.My.Resources.Resources.search4
            repoCostCenterCode.TextImageRelation = TextImageRelation.TextBeforeImage
            repoCostCenterCode.Width = 150
            gv1.MasterTemplate.Columns.Add(repoCostCenterCode)

            If EnableHirerachyCostCentre = 1 Then

                Dim repoHierarchyLevel2 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
                repoHierarchyLevel2.FormatString = ""
                repoHierarchyLevel2.HeaderText = "Hierarchy Level2"
                repoHierarchyLevel2.Name = colHierarchyLevel2
                repoHierarchyLevel2.HeaderImage = Global.ERP.My.Resources.Resources.search4
                repoHierarchyLevel2.TextImageRelation = TextImageRelation.TextBeforeImage
                repoHierarchyLevel2.Width = 150
                gv1.MasterTemplate.Columns.Add(repoHierarchyLevel2)

                Dim repoHierarchyLevel3 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
                repoHierarchyLevel3.FormatString = ""
                repoHierarchyLevel3.HeaderText = "Hierarchy Level3"
                repoHierarchyLevel3.Name = colHierarchyLevel3
                repoHierarchyLevel3.HeaderImage = Global.ERP.My.Resources.Resources.search4
                repoHierarchyLevel3.TextImageRelation = TextImageRelation.TextBeforeImage
                repoHierarchyLevel3.Width = 150
                gv1.MasterTemplate.Columns.Add(repoHierarchyLevel3)

            End If

        Else
            Dim repoCCCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            repoCCCode = New GridViewTextBoxColumn()
            repoCCCode.HeaderText = "Cost Center"
            repoCCCode.Name = colCCCode
            repoCCCode.HeaderImage = Global.ERP.My.Resources.Resources.search4
            repoCCCode.TextImageRelation = TextImageRelation.TextBeforeImage
            repoCCCode.Width = 100
            If EnableStoreCostCentre = 1 Then
                repoCCCode.ReadOnly = True
            Else
                repoCCCode.ReadOnly = False
            End If
            gv1.MasterTemplate.Columns.Add(repoCCCode)

            Dim repoCCDesc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            repoCCDesc = New GridViewTextBoxColumn()
            repoCCDesc.FormatString = ""
            repoCCDesc.HeaderText = "Description"
            repoCCDesc.Name = colCCDesc
            repoCCDesc.Width = 150
            repoCCDesc.ReadOnly = True
            gv1.MasterTemplate.Columns.Add(repoCCDesc)
        End If



        clsCustomFieldGrid.LoadBlankGrid(gv1, MyBase.ArrDetailFields)

        gv1.AllowDeleteRow = True
        gv1.AllowAddNewRow = False
        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = False
        gv1.AllowRowReorder = False
        gv1.EnableSorting = False
        gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        ' ReStoreGridLayout()
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

    Private Sub OpenCCList(ByVal isButtonClick As Boolean)
        Dim qry As String = "select Cost_Code as Code,Cost_name as Name from  TSPL_CostCenter_MASTER"
        gv1.CurrentRow.Cells(colCCCode).Value = clsCommon.ShowSelectForm("TSPL_CostCenter_MASTER", qry, "Code", "", clsCommon.myCstr(gv1.CurrentRow.Cells(colCCCode).Value), "", isButtonClick)
        gv1.CurrentRow.Cells(colCCDesc).Value = ClsCostCenter.GetCostCenterDesc(gv1.CurrentRow.Cells(colCCCode).Value)
    End Sub
    ''richa agarwal 1 Dec,2016
    Private Sub OpenHierarchyCode(ByVal isButtonClick As Boolean)
        If EnableHirerachyCostCentre = 1 Then
            Dim qry As String = Nothing

            qry = " select TSPL_HIRERACHY_LEVEL_MASTER.HIRERACHY_CODE as [Code] ,TSPL_HIRERACHY_LEVEL_MASTER.Description as [Description],TSPL_HIRERACHY_LEVEL_MASTER.Level as [Level] ,TSPL_HIRERACHY_LEVEL_MASTER.Created_By as [Created By] ,CONVERT(VARCHAR,TSPL_HIRERACHY_LEVEL_MASTER.Created_Date,103) as [Created Date] ,TSPL_HIRERACHY_LEVEL_MASTER.Modified_By as [Modified By] ,CONVERT(VARCHAR,TSPL_HIRERACHY_LEVEL_MASTER.Modified_Date,103) as [Modified Date]  From TSPL_HIRERACHY_LEVEL_MASTER  "
            gv1.CurrentRow.Cells(colHierarchyCode).Value = clsCommon.ShowSelectForm("Hierarchy", qry, "Code", "", clsCommon.myCstr(gv1.CurrentRow.Cells(colHierarchyCode).Value), "Code", isButtonClick)
            OpenCostCentreLevelName(False)
            OpenDefaultCostCentreLevel1(False)
            OpenDefaultCostCentreLevel2(False)
            OpenDefaultCostCentreLevel3(False)
        Else
            Dim qry As String = " select TSPL_HIRERACHY_LEVEL_MASTER.HIRERACHY_CODE as [Code] ,TSPL_HIRERACHY_LEVEL_MASTER.Description as [Description],TSPL_HIRERACHY_LEVEL_MASTER.Level as [Level] ,TSPL_HIRERACHY_LEVEL_MASTER.Created_By as [Created By] ,CONVERT(VARCHAR,TSPL_HIRERACHY_LEVEL_MASTER.Created_Date,103) as [Created Date] ,TSPL_HIRERACHY_LEVEL_MASTER.Modified_By as [Modified By] ,CONVERT(VARCHAR,TSPL_HIRERACHY_LEVEL_MASTER.Modified_Date,103) as [Modified Date]  From TSPL_HIRERACHY_LEVEL_MASTER  "
            gv1.CurrentRow.Cells(colHierarchyCode).Value = clsCommon.ShowSelectForm("Hierarchy", qry, "Code", "", clsCommon.myCstr(gv1.CurrentRow.Cells(colHierarchyCode).Value), "Code", isButtonClick)
        End If
    End Sub
    Private Sub OpenCostCenterCode(ByVal isButtonClick As Boolean)
        If clsCommon.myLen(clsCommon.myCstr(gv1.CurrentRow.Cells(colHierarchyCode).Value)) > 0 Then
            If EnableHirerachyCostCentre = 1 Then
                Dim DBLevel As String = String.Empty
                DBLevel = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select ISNULL(Level,'') AS Level from TSPL_HIRERACHY_LEVEL_MASTER Where HIRERACHY_CODE='" + clsCommon.myCstr(gv1.CurrentRow.Cells(colHierarchyCode).Value) + "' "))
                Dim qry As String = "select TSPL_COST_CENTRE_FINANCIAL.Cost_Center_Fin_Code as [Code] ,TSPL_COST_CENTRE_FINANCIAL.Cost_Center_Fin_Name,ISNULL(TSPL_COST_CENTRE_FINANCIAL.Hirerachy_Level_Code,'') AS [Hirerachy Level Code],ISNULL(TSPL_COST_CENTRE_FINANCIAL.Cost_Centre_Fin_Level_Code,'') AS [Cost Centre Fin Level Code],ISNULL(TSPL_COST_CENTRE_FINANCIAL.Hirerachy_Level,'') AS [Hirerachy Level] ,TSPL_COST_CENTRE_FINANCIAL.Created_By as [Created By] ,Convert(varchar,TSPL_COST_CENTRE_FINANCIAL.Created_Date,103) as [Created Date] ,TSPL_COST_CENTRE_FINANCIAL.Modified_By as [Modified By] ,Convert(varchar,TSPL_COST_CENTRE_FINANCIAL.Modified_Date,103) as [Modified Date]  From TSPL_COST_CENTRE_FINANCIAL "
                gv1.CurrentRow.Cells(colCostCenterCode).Value = clsCommon.ShowSelectForm("Hierarchy", qry, "Code", " HIRERACHY_LEVEL = '" + DBLevel + "'", clsCommon.myCstr(gv1.CurrentRow.Cells(colCostCenterCode).Value), "Code", isButtonClick)
                OpenDefaultCostCentreLevel2(False)
                OpenDefaultCostCentreLevel3(False)
            Else
                Dim DBLevel As String = String.Empty
                DBLevel = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select ISNULL(Level,'') AS Level from TSPL_HIRERACHY_LEVEL_MASTER Where HIRERACHY_CODE='" + clsCommon.myCstr(gv1.CurrentRow.Cells(colHierarchyCode).Value) + "' "))
                Dim qry As String = "select TSPL_COST_CENTRE_FINANCIAL.Cost_Center_Fin_Code as [Code] ,TSPL_COST_CENTRE_FINANCIAL.Cost_Center_Fin_Name,ISNULL(TSPL_COST_CENTRE_FINANCIAL.Hirerachy_Level_Code,'') AS [Hirerachy Level Code],ISNULL(TSPL_COST_CENTRE_FINANCIAL.Cost_Centre_Fin_Level_Code,'') AS [Cost Centre Fin Level Code],ISNULL(TSPL_COST_CENTRE_FINANCIAL.Hirerachy_Level,'') AS [Hirerachy Level] ,TSPL_COST_CENTRE_FINANCIAL.Created_By as [Created By] ,Convert(varchar,TSPL_COST_CENTRE_FINANCIAL.Created_Date,103) as [Created Date] ,TSPL_COST_CENTRE_FINANCIAL.Modified_By as [Modified By] ,Convert(varchar,TSPL_COST_CENTRE_FINANCIAL.Modified_Date,103) as [Modified Date]  From TSPL_COST_CENTRE_FINANCIAL "
                gv1.CurrentRow.Cells(colCostCenterCode).Value = clsCommon.ShowSelectForm("Hierarchy", qry, "Code", " Hirerachy_Level = '" + DBLevel + "'", clsCommon.myCstr(gv1.CurrentRow.Cells(colCostCenterCode).Value), "Code", isButtonClick)
            End If
        Else
            ' clsCommon.MyMessageBoxShow("Please select hirerachy level first.")
        End If
    End Sub
    ''--------
    '========Sanjeet(For Hirerachy Level)======
    Private Sub OpenCostCentreLevel2(ByVal isButtonClick As Boolean)
        If clsCommon.myLen(clsCommon.myCstr(gv1.CurrentRow.Cells(colHierarchyLevel2).Value)) > 0 Then
            If EnableHirerachyCostCentre = 1 Then
                Dim DBLevel As String = String.Empty
                DBLevel = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Hirerachy_Level  FROM TSPL_COST_CENTRE_FINANCIAL WHERE Cost_Center_Fin_Code='" + clsCommon.myCstr(gv1.CurrentRow.Cells(colCostCenterCode).Value) + "' "))
                Dim QRY As String = "select DISTINCT MAIN.Hierarchy_Level_Code as Code,TSPL_COST_CENTRE_FINANCIAL.Cost_Center_Fin_Name AS Descriptiion  from (select TSPL_COST_CENTRE_HIRERACHY_DETAIL.COST_CENTRE_HIRERACHY_CODE, case when '" + DBLevel + "'='4' then  TSPL_COST_CENTRE_HIRERACHY_DETAIL.HIRERACHY_LEVEL_CODE3 when '" + DBLevel + "'='3' " & _
                    " then TSPL_COST_CENTRE_HIRERACHY_DETAIL.HIRERACHY_LEVEL_CODE2 when '" + DBLevel + "'='2' then TSPL_COST_CENTRE_HIRERACHY_DETAIL.HIRERACHY_LEVEL_CODE1 end as Hierarchy_Level_Code" & _
                    " from TSPL_COST_CENTRE_HIRERACHY_DETAIL WHERE TSPL_COST_CENTRE_HIRERACHY_DETAIL.HIRERACHY_LEVEL='" + DBLevel + "') as MAIN  " & _
                    " LEFT OUTER JOIN TSPL_COST_CENTRE_FINANCIAL  ON MAIN.Hierarchy_Level_Code=TSPL_COST_CENTRE_FINANCIAL.Cost_Center_Fin_Code "
                gv1.CurrentRow.Cells(colHierarchyLevel2).Value = clsCommon.ShowSelectForm("Hierarchy", QRY, "Code", " MAIN.COST_CENTRE_HIRERACHY_CODE = '" + clsCommon.myCstr(gv1.CurrentRow.Cells(colCostCenterCode).Value) + "'", clsCommon.myCstr(gv1.CurrentRow.Cells(colHierarchyLevel2).Value), "Code", isButtonClick)
                OpenDefaultCostCentreLevel3(False)
            End If
        Else
            clsCommon.MyMessageBoxShow("Please select " + clsCommon.myCstr(gv1.Columns(colHierarchyLevel2).HeaderText) + " first.")
        End If
    End Sub
    Private Sub OpenCostCentreLevel3(ByVal isButtonClick As Boolean)
        If clsCommon.myLen(clsCommon.myCstr(gv1.CurrentRow.Cells(colHierarchyLevel3).Value)) > 0 Then
            If EnableHirerachyCostCentre = 1 Then
                Dim DBLevel As String = String.Empty
                DBLevel = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Hirerachy_Level  FROM TSPL_COST_CENTRE_FINANCIAL WHERE Cost_Center_Fin_Code='" + clsCommon.myCstr(gv1.CurrentRow.Cells(colHierarchyLevel2).Value) + "' "))
                Dim QRY As String = "select DISTINCT MAIN.Hierarchy_Level_Code as Code,TSPL_COST_CENTRE_FINANCIAL.Cost_Center_Fin_Name AS Descriptiion  from (select TSPL_COST_CENTRE_HIRERACHY_DETAIL.COST_CENTRE_HIRERACHY_CODE, case when '" + DBLevel + "'='4' then  TSPL_COST_CENTRE_HIRERACHY_DETAIL.HIRERACHY_LEVEL_CODE3 when '" + DBLevel + "'='3' " & _
                    " then TSPL_COST_CENTRE_HIRERACHY_DETAIL.HIRERACHY_LEVEL_CODE2 when '" + DBLevel + "'='2' then TSPL_COST_CENTRE_HIRERACHY_DETAIL.HIRERACHY_LEVEL_CODE1 end as Hierarchy_Level_Code" & _
                    " from TSPL_COST_CENTRE_HIRERACHY_DETAIL WHERE TSPL_COST_CENTRE_HIRERACHY_DETAIL.HIRERACHY_LEVEL='" + DBLevel + "') as MAIN  " & _
                    " LEFT OUTER JOIN TSPL_COST_CENTRE_FINANCIAL  ON MAIN.Hierarchy_Level_Code=TSPL_COST_CENTRE_FINANCIAL.Cost_Center_Fin_Code "
                gv1.CurrentRow.Cells(colHierarchyLevel3).Value = clsCommon.ShowSelectForm("Hierarchy", QRY, "Code", "  MAIN.COST_CENTRE_HIRERACHY_CODE = '" + clsCommon.myCstr(gv1.CurrentRow.Cells(colHierarchyLevel2).Value) + "'", clsCommon.myCstr(gv1.CurrentRow.Cells(colHierarchyLevel3).Value), "Code", isButtonClick)
            End If
        Else
            clsCommon.MyMessageBoxShow("Please select " + clsCommon.myCstr(gv1.Columns(colHierarchyLevel3).HeaderText) + " first.")
        End If
    End Sub

    Private Sub OpenDefaultCostCentreLevel1(ByVal isButtonClick As Boolean)
        If EnableHirerachyCostCentre = 1 Then
            If clsCommon.myLen(clsCommon.myCstr(gv1.Columns(colHierarchyCode))) > 0 Then
                Dim DBLevel As String = String.Empty
                Dim dt As DataTable = New DataTable()
                Dim qry As String = Nothing
                DBLevel = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select ISNULL(Level,'') AS Level from TSPL_HIRERACHY_LEVEL_MASTER Where HIRERACHY_CODE='" + clsCommon.myCstr(gv1.CurrentRow.Cells(colHierarchyCode).Value) + "' "))
                qry = "select TSPL_COST_CENTRE_FINANCIAL.Cost_Center_Fin_Code as [Code] ,TSPL_COST_CENTRE_FINANCIAL.Cost_Center_Fin_Name,ISNULL(TSPL_COST_CENTRE_FINANCIAL.Hirerachy_Level_Code,'') AS [Hirerachy Level Code],ISNULL(TSPL_COST_CENTRE_FINANCIAL.Cost_Centre_Fin_Level_Code,'') AS [Cost Centre Fin Level Code],ISNULL(TSPL_COST_CENTRE_FINANCIAL.Hirerachy_Level,'') AS [Hirerachy Level] ,TSPL_COST_CENTRE_FINANCIAL.Created_By as [Created By] ,Convert(varchar,TSPL_COST_CENTRE_FINANCIAL.Created_Date,103) as [Created Date] ,TSPL_COST_CENTRE_FINANCIAL.Modified_By as [Modified By] ,Convert(varchar,TSPL_COST_CENTRE_FINANCIAL.Modified_Date,103) as [Modified Date]  From TSPL_COST_CENTRE_FINANCIAL where HIRERACHY_LEVEL='" + DBLevel + "'"
                dt = clsDBFuncationality.GetDataTable(qry)
                If dt.Rows.Count = 1 Then
                    gv1.CurrentRow.Cells(colCostCenterCode).Value = clsCommon.myCstr(dt.Rows(0)("Code"))
                Else
                    gv1.CurrentRow.Cells(colCostCenterCode).Value = ""
                End If
            End If
        End If
    End Sub
    Private Sub OpenDefaultCostCentreLevel2(ByVal isButtonClick As Boolean)
        If EnableHirerachyCostCentre = 1 Then
            If clsCommon.myLen(clsCommon.myCstr(gv1.Columns(colCostCenterCode))) > 0 Then
                Dim DBLevel As String = String.Empty
                Dim dt As DataTable = New DataTable()
                Dim qry As String = Nothing
                DBLevel = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Hirerachy_Level  FROM TSPL_COST_CENTRE_FINANCIAL WHERE Cost_Center_Fin_Code='" + clsCommon.myCstr(gv1.CurrentRow.Cells(colCostCenterCode).Value) + "' "))
                qry = "select DISTINCT MAIN.Hierarchy_Level_Code as Code,TSPL_COST_CENTRE_FINANCIAL.Cost_Center_Fin_Name AS Descriptiion  from (select TSPL_COST_CENTRE_HIRERACHY_DETAIL.COST_CENTRE_HIRERACHY_CODE, case when '" + DBLevel + "'='4' then  TSPL_COST_CENTRE_HIRERACHY_DETAIL.HIRERACHY_LEVEL_CODE3 when '" + DBLevel + "'='3' " & _
                " then TSPL_COST_CENTRE_HIRERACHY_DETAIL.HIRERACHY_LEVEL_CODE2 when '" + DBLevel + "'='2' then TSPL_COST_CENTRE_HIRERACHY_DETAIL.HIRERACHY_LEVEL_CODE1 end as Hierarchy_Level_Code" & _
                " from TSPL_COST_CENTRE_HIRERACHY_DETAIL WHERE TSPL_COST_CENTRE_HIRERACHY_DETAIL.HIRERACHY_LEVEL='" + DBLevel + "') as MAIN  " & _
                " LEFT OUTER JOIN TSPL_COST_CENTRE_FINANCIAL  ON MAIN.Hierarchy_Level_Code=TSPL_COST_CENTRE_FINANCIAL.Cost_Center_Fin_Code WHERE MAIN.COST_CENTRE_HIRERACHY_CODE = '" + clsCommon.myCstr(gv1.CurrentRow.Cells(colCostCenterCode).Value) + "'"
                dt = clsDBFuncationality.GetDataTable(qry)
                If dt.Rows.Count = 1 Then
                    gv1.CurrentRow.Cells(colHierarchyLevel2).Value = clsCommon.myCstr(dt.Rows(0)("Code"))
                Else
                    gv1.CurrentRow.Cells(colHierarchyLevel2).Value = ""
                End If
            End If
        End If
    End Sub
    Private Sub OpenDefaultCostCentreLevel3(ByVal isButtonClick As Boolean)
        If EnableHirerachyCostCentre = 1 Then
            If clsCommon.myLen(clsCommon.myCstr(gv1.Columns(colHierarchyLevel2))) > 0 Then
                Dim DBLevel As String = String.Empty
                Dim dt As DataTable = New DataTable()
                Dim qry As String = Nothing
                DBLevel = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Hirerachy_Level  FROM TSPL_COST_CENTRE_FINANCIAL WHERE Cost_Center_Fin_Code='" + clsCommon.myCstr(gv1.CurrentRow.Cells(colHierarchyLevel2).Value) + "' "))
                qry = "select distinct MAIN.Hierarchy_Level_Code as Code,TSPL_COST_CENTRE_FINANCIAL.Cost_Center_Fin_Name AS Descriptiion  from (select TSPL_COST_CENTRE_HIRERACHY_DETAIL.COST_CENTRE_HIRERACHY_CODE, case when '" + DBLevel + "'='4' then  TSPL_COST_CENTRE_HIRERACHY_DETAIL.HIRERACHY_LEVEL_CODE3 when '" + DBLevel + "'='3' " & _
                " then TSPL_COST_CENTRE_HIRERACHY_DETAIL.HIRERACHY_LEVEL_CODE2 when '" + DBLevel + "'='2' then TSPL_COST_CENTRE_HIRERACHY_DETAIL.HIRERACHY_LEVEL_CODE1 end as Hierarchy_Level_Code" & _
                " from TSPL_COST_CENTRE_HIRERACHY_DETAIL WHERE TSPL_COST_CENTRE_HIRERACHY_DETAIL.HIRERACHY_LEVEL='" + DBLevel + "') as MAIN  " & _
                " LEFT OUTER JOIN TSPL_COST_CENTRE_FINANCIAL  ON MAIN.Hierarchy_Level_Code=TSPL_COST_CENTRE_FINANCIAL.Cost_Center_Fin_Code where  MAIN.COST_CENTRE_HIRERACHY_CODE = '" + clsCommon.myCstr(gv1.CurrentRow.Cells(colHierarchyLevel2).Value) + "'"
                dt = clsDBFuncationality.GetDataTable(qry)
                If dt.Rows.Count = 1 Then
                    gv1.CurrentRow.Cells(colHierarchyLevel3).Value = clsCommon.myCstr(dt.Rows(0)("Code"))
                Else
                    gv1.CurrentRow.Cells(colHierarchyLevel3).Value = ""
                End If
            End If
        End If
    End Sub

    Private Sub OpenCostCentreLevelName(ByVal isButtonClick As Boolean)
        If EnableHirerachyCostCentre = 1 Then
            Dim DBLevel As String = String.Empty
            DBLevel = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select ISNULL(Level,'') AS Level from TSPL_HIRERACHY_LEVEL_MASTER Where HIRERACHY_CODE='" + clsCommon.myCstr(gv1.CurrentRow.Cells(colHierarchyCode).Value) + "' "))
            If clsCommon.CompairString(DBLevel, 4) = CompairStringResult.Equal Then
                gv1.Columns(colCostCenterCode).HeaderText = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select HIRERACHY_CODE  from TSPL_HIRERACHY_LEVEL_MASTER Where Level= '" + DBLevel + "'"))
                gv1.Columns(colHierarchyLevel2).HeaderText = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select HIRERACHY_CODE  from TSPL_HIRERACHY_LEVEL_MASTER Where Level= '" + clsCommon.myCstr(clsCommon.myCdbl(DBLevel) - 1) + "'"))
                gv1.Columns(colHierarchyLevel3).HeaderText = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select HIRERACHY_CODE  from TSPL_HIRERACHY_LEVEL_MASTER Where Level= '" + clsCommon.myCstr(clsCommon.myCdbl(DBLevel) - 2) + "'"))
                'gv1.CurrentRow.Cells(colCostCenterCode).Value = ""
                'gv1.CurrentRow.Cells(colHierarchyLevel2).Value = ""
                'gv1.CurrentRow.Cells(colHierarchyLevel3).Value = ""
            ElseIf clsCommon.CompairString(DBLevel, 3) = CompairStringResult.Equal Then
                gv1.Columns(colCostCenterCode).HeaderText = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select HIRERACHY_CODE  from TSPL_HIRERACHY_LEVEL_MASTER Where Level= '" + DBLevel + "'"))
                gv1.Columns(colHierarchyLevel2).HeaderText = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select HIRERACHY_CODE  from TSPL_HIRERACHY_LEVEL_MASTER Where Level= '" + clsCommon.myCstr(clsCommon.myCdbl(DBLevel) - 1) + "'"))
                gv1.Columns(colHierarchyLevel3).HeaderText = "Hiererachy Level3"
                'gv1.CurrentRow.Cells(colCostCenterCode).Value = ""
                'gv1.CurrentRow.Cells(colHierarchyLevel2).Value = ""
                'gv1.CurrentRow.Cells(colHierarchyLevel3).Value = ""
                gv1.Columns(colHierarchyLevel3).ReadOnly = True
            Else
                gv1.Columns(colCostCenterCode).HeaderText = "Hiererachy Level1"
                'gv1.Columns(colCostCenterCode).HeaderText = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select HIRERACHY_CODE  from TSPL_HIRERACHY_LEVEL_MASTER Where Level= '" + DBLevel + "'"))
                gv1.Columns(colHierarchyLevel2).HeaderText = "Hiererachy Level2"
                gv1.Columns(colHierarchyLevel3).HeaderText = "Hiererachy Level3"
                gv1.Columns(colHierarchyLevel2).ReadOnly = True
                gv1.Columns(colHierarchyLevel3).ReadOnly = True
            End If
        End If
    End Sub
    '====================
    Private Sub gv1_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellValueChanged
        Try
            If (Not isInsideLoadData) Then
                If Not isCellValueChangedOpen Then
                    isCellValueChangedOpen = True
                    If e.Column.FieldName.StartsWith("_CFLD_") Then
                        clsCustomFieldGrid.getFinderForCustomFieldGrid(gv1, e.Column.Name.ToString, MyBase.Form_ID)
                    End If

                    ''richa agarwal 1 Dec,2016
                    If clsCommon.CompairString(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.ShowCostCenterAndHierarchyLevelInPurchaseModule, clsFixedParameterCode.ShowCostCenterAndHierarchyLevelInPurchaseModule, Nothing)), "1") = CompairStringResult.Equal Then
                        If e.Column Is gv1.Columns(colHierarchyCode) Then
                            If EnableHirerachyCostCentre = 1 Then
                                If e.RowIndex = 0 Then
                                    OpenHierarchyCode(False)
                                Else
                                    Dim qry As String = Nothing
                                    qry = " select TSPL_HIRERACHY_LEVEL_MASTER.HIRERACHY_CODE as [Code] ,TSPL_HIRERACHY_LEVEL_MASTER.Description as [Description],TSPL_HIRERACHY_LEVEL_MASTER.Level as [Level] ,TSPL_HIRERACHY_LEVEL_MASTER.Created_By as [Created By] ,CONVERT(VARCHAR,TSPL_HIRERACHY_LEVEL_MASTER.Created_Date,103) as [Created Date] ,TSPL_HIRERACHY_LEVEL_MASTER.Modified_By as [Modified By] ,CONVERT(VARCHAR,TSPL_HIRERACHY_LEVEL_MASTER.Modified_Date,103) as [Modified Date]  From TSPL_HIRERACHY_LEVEL_MASTER "
                                    gv1.CurrentRow.Cells(colHierarchyCode).Value = clsCommon.ShowSelectForm("Hierarchy", qry, "Code", "TSPL_HIRERACHY_LEVEL_MASTER.HIRERACHY_CODE='" + clsCommon.myCstr(gv1.Rows(e.RowIndex - 1).Cells(colHierarchyCode).Value) + "'", clsCommon.myCstr(gv1.CurrentRow.Cells(colHierarchyCode).Value), "Code", False)
                                    OpenCostCentreLevelName(False)
                                    OpenDefaultCostCentreLevel1(False)
                                    OpenDefaultCostCentreLevel2(False)
                                    OpenDefaultCostCentreLevel3(False)
                                End If
                            Else
                                OpenHierarchyCode(False)
                            End If


                        End If
                        If e.Column Is gv1.Columns(colCostCenterCode) Then
                            OpenCostCenterCode(False)
                        End If
                        If e.Column Is gv1.Columns(colHierarchyLevel2) Then
                            OpenCostCentreLevel2(False)
                        End If
                        If e.Column Is gv1.Columns(colHierarchyLevel3) Then
                            OpenCostCentreLevel3(False)
                        End If
                    Else
                        If e.Column Is gv1.Columns(colCCCode) Then
                            OpenCCList(False)
                        End If
                    End If
                    ''-----------------

                    If e.Column Is gv1.Columns(colICode) OrElse e.Column Is gv1.Columns(colQty) OrElse e.Column Is gv1.Columns(colRate) OrElse e.Column Is gv1.Columns(colVendorCode) OrElse e.Column Is gv1.Columns(colVendorItemNo) OrElse e.Column Is gv1.Columns(colOrderNo) OrElse e.Column Is gv1.Columns(colSpecification) OrElse e.Column Is gv1.Columns(colRemarks) OrElse e.Column Is gv1.Columns(colUnit) Then
                        If e.Column Is gv1.Columns(colICode) Then
                            OpenICodeList(False)
                        ElseIf (e.Column Is gv1.Columns(colVendorCode)) Then
                            OpenVendorList(False)
                            ''ElseIf (clsCommon.CompairString(e.Column.Name, colLocationCode) = CompairStringResult.Equal) Then
                            ''    Dim qry As String = "select Location_Code as Code,Location_Desc as Name  from TSPL_LOCATION_MASTER"
                            ''    gv1.CurrentRow.Cells(colLocationCode).Value = clsCommon.ShowSelectForm("PRLocationCode", qry, "Code", "", clsCommon.myCstr(gv1.CurrentRow.Cells(colLocationCode).Value), "Code", False)
                            ''    gv1.CurrentRow.Cells(colLocationName).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code ='" + clsCommon.myCstr(gv1.CurrentRow.Cells(colLocationCode).Value) + "'"))
                        ElseIf e.Column Is gv1.Columns(colOrderNo) Then
                            OpenOrderList(False)
                        ElseIf (e.Column Is gv1.Columns(colQty) OrElse e.Column Is gv1.Columns(colRate)) Then
                            UpdateCurrentRow()
                            UpdateAllTotals()
                        ElseIf e.Column Is gv1.Columns(colUnit) Then
                            OpenUOMList(False)
                        End If
                        setGridFocus()
                    End If
                    isCellValueChangedOpen = False
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally

        End Try
    End Sub

    Sub OpenUOMList(ByVal isButtonClick As Boolean)
        Dim strICode As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value)
        If clsCommon.myLen(strICode) > 0 Then
            Dim qry As String = "select UOM_Code as Code,UOM_Description as [Description] from TSPL_ITEM_UOM_DETAIL"
            Dim whrCls As String = "Item_Code='" + strICode + "'"
            gv1.CurrentRow.Cells(colUnit).Value = clsCommon.ShowSelectForm("SRNItefndnder", qry, "Code", whrCls, clsCommon.myCstr(gv1.CurrentRow.Cells(colUnit).Value), "Code", isButtonClick)
            setCost()
        End If
    End Sub

    Sub OpenICodeList(ByVal isButtonClick As Boolean)
        ''Dim qry As String = "select Item_Code as Code,Item_Desc as Name from TSPL_ITEM_MASTER"
        ''gv1.CurrentRow.Cells(colICode).Value = clsCommon.ShowSelectForm("PRItemList", qry, "Code", "", clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value), "Code", isButtonClick)
        ''Dim dt As DataTable = clsDBFuncationality.GetDataTable("select Item_Desc,Unit_Code from TSPL_ITEM_MASTER where Item_Code='" + clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value) + "'")
        ''richa UDL/16/11/18-000240 16 Nov,2018
        If clsCommon.myLen(cboItemType.SelectedValue) <= 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "Please select Item Type", Me.Text)
            gv1.CurrentRow.Cells(colICode).Value = ""
            gv1.CurrentRow.Cells(colIName).Value = ""
            gv1.CurrentRow.Cells(colUnit).Value = ""
            cboItemType.Focus()
            Exit Sub
        ElseIf clsCommon.myLen(txtLocation.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "Please select Location", Me.Text)
            gv1.CurrentRow.Cells(colICode).Value = ""
            gv1.CurrentRow.Cells(colIName).Value = ""
            gv1.CurrentRow.Cells(colUnit).Value = ""
            txtLocation.Focus()
            Exit Sub
        ElseIf clsCommon.myLen(txtDept.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "Please select Department", Me.Text)
            gv1.CurrentRow.Cells(colICode).Value = ""
            gv1.CurrentRow.Cells(colIName).Value = ""
            gv1.CurrentRow.Cells(colUnit).Value = ""
            txtDept.Focus()
            Exit Sub
        End If

        Dim obj As clsItemMaster = clsItemMaster.FinderForItemALL(clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value), cboItemType.SelectedValue, True, ShowItemAllStructureWise, isButtonClick, "", "")
        If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Item_Code) > 0 Then
            gv1.CurrentRow.Cells(colICode).Value = obj.Item_Code
            gv1.CurrentRow.Cells(colIName).Value = obj.Item_Desc
            gv1.CurrentRow.Cells(colUnit).Value = obj.Unit_Code
            If EnableStoreCostCentre = 1 Then
                gv1.CurrentRow.Cells(colCCCode).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Cost_Code from TSPL_COST_CENTER_TYPE_MASTER where Code='" & txtCostCenterType.Value & "' "))
            Else
                gv1.CurrentRow.Cells(colCCCode).Value = ""
            End If
            'gv1.CurrentRow.Cells(colCCCode).Value = clsCommon.myCstr(lblDept.Text)

            setCost()
        Else
            gv1.CurrentRow.Cells(colICode).Value = ""
            gv1.CurrentRow.Cells(colIName).Value = ""
            gv1.CurrentRow.Cells(colUnit).Value = ""
        End If
        setBalance()
    End Sub

    Sub setCost()
        Dim dblCostMethod As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Costing_Method as Costing_Method from TSPL_ITEM_MASTER left outer join TSPL_PURCHASE_ACCOUNTS on TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code=TSPL_ITEM_MASTER.Purchase_Class_Code where Item_Code='" & clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value) & "' "))
        If dblCostMethod <> 0 Then
            Dim ItemCost As Double = 0
            Try ''ERO/05/04/19-000546 by balwinder on 05/04/2019
                ItemCost = clsInventoryMovement.GetCost(dblCostMethod, clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value), txtLocation.Value, 1, txtDate.Value, txtDate.Value, False, Nothing, "TSPL_INVENTORY_MOVEMENT", clsCommon.myCstr(gv1.CurrentRow.Cells(colUnit).Value))
            Catch ex As Exception
            End Try
            gv1.CurrentRow.Cells(colRate).Value = ItemCost
        Else
            gv1.CurrentRow.Cells(colRate).Value = 0
        End If
    End Sub

    Sub OpenVendorList(ByVal isButtonClick As Boolean)
        Dim qry As String = "select Vendor_Code as Code,Vendor_Name as Name from TSPL_VENDOR_MASTER"
        gv1.CurrentRow.Cells(colVendorCode).Value = clsCommon.ShowSelectForm("PRVendofnd", qry, "Code", " TSPL_VENDOR_MASTER.Status='N'", clsCommon.myCstr(gv1.CurrentRow.Cells(colVendorCode).Value), "Code", isButtonClick)
        gv1.CurrentRow.Cells(colVendorName).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Vendor_Name from TSPL_VENDOR_MASTER where Vendor_Code ='" + clsCommon.myCstr(gv1.CurrentRow.Cells(colVendorCode).Value) + "'"))
        gv1.CurrentRow.Cells(colVendorItemNo).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select vendor_item_no from TSPL_VENDOR_ITEM_DETAIL WHERE vendor_code='" + clsCommon.myCstr(gv1.CurrentRow.Cells(colVendorCode).Value) + "' AND item_no='" + clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value) + "'"))
    End Sub

    Sub OpenOrderList(ByVal isButtonClick As Boolean)
        'richa 10/07/2014 Change Table Name for xpert erp
        'Dim qry As String = "select Order_No as Code,CONVERT(varchar(10),Order_Date,103) as Date from TSPL_SALES_ORDER_HEAD"
        Dim qry As String = "select Document_Code  as Code,CONVERT(varchar(10),Document_Date ,103) as Date from TSPL_SD_SALES_ORDER_HEAD"
        '-------richa Code Ends Here--------------------------
        gv1.CurrentRow.Cells(colOrderNo).Value = clsCommon.ShowSelectForm("PROrdfnd", qry, "Code", "", clsCommon.myCstr(gv1.CurrentRow.Cells(colOrderNo).Value), "Code", isButtonClick)
    End Sub

    Private Sub setGridFocus()
        Dim intCurrRow As Integer = gv1.CurrentRow.Index
        'gv1.CurrentRow.Cells(colLineNo).Value = clsCommon.myCdbl(intCurrRow + 1)
        'If intCurrRow = gv1.Rows.Count - 1 Then
        '    gv1.Rows.AddNew()
        '    gv1.CurrentRow = gv1.Rows(intCurrRow)
        'End If
        If clsCommon.myLen(gv1.CurrentRow.Cells(colICode).Value) > 0 Then
            If gv1.CurrentColumn Is gv1.Columns(colICode) Then
                gv1.CurrentRow = gv1.Rows(intCurrRow)
                gv1.CurrentColumn = gv1.Columns(colQty)
            ElseIf gv1.CurrentColumn Is gv1.Columns(colQty) Then
                gv1.CurrentRow = gv1.Rows(intCurrRow)
                gv1.CurrentColumn = gv1.Columns(colRate)
            ElseIf gv1.CurrentColumn Is gv1.Columns(colRate) Then
                gv1.CurrentRow = gv1.Rows(intCurrRow)
                gv1.CurrentColumn = gv1.Columns(colVendorCode)
            ElseIf gv1.CurrentColumn Is gv1.Columns(colVendorCode) Then
                gv1.CurrentRow = gv1.Rows(intCurrRow)
                gv1.CurrentColumn = gv1.Columns(colVendorItemNo)
            ElseIf gv1.CurrentColumn Is gv1.Columns(colVendorItemNo) Then
                gv1.CurrentRow = gv1.Rows(intCurrRow)
                gv1.CurrentColumn = gv1.Columns(colOrderNo)
            ElseIf gv1.CurrentColumn Is gv1.Columns(colOrderNo) Then
                gv1.CurrentRow = gv1.Rows(intCurrRow)
                gv1.CurrentColumn = gv1.Columns(colSpecification)
            ElseIf gv1.CurrentColumn Is gv1.Columns(colSpecification) Then
                gv1.CurrentRow = gv1.Rows(intCurrRow)
                gv1.CurrentColumn = gv1.Columns(colRemarks)
            ElseIf gv1.CurrentColumn Is gv1.Columns(colRemarks) Then
                gv1.CurrentRow = gv1.Rows(intCurrRow + 1)
                gv1.CurrentColumn = gv1.Columns(colICode)
            End If
        End If
    End Sub

    Private Sub gv1_UserAddedRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowEventArgs) Handles gv1.UserAddedRow
        For i As Integer = 0 To gv1.Rows.Count - 1
            gv1.Rows(0).Cells(0).Value = 1
            If i <> 0 Then
                gv1.Rows(i).Cells(colLineNo).Value = i + 1
            End If
        Next
    End Sub

    Private Sub UpdateCurrentRow()
        If clsCommon.myLen(gv1.CurrentRow.Cells(colICode).Value) > 0 Then
            Dim dblQty As Double = clsCommon.myCdbl(gv1.CurrentRow.Cells(colQty).Value)
            Dim dblRate As Double = clsCommon.myCdbl(gv1.CurrentRow.Cells(colRate).Value)
            Dim dblAmt As Double = (dblQty * dblRate)
            gv1.CurrentRow.Cells(colAmt).Value = dblAmt
        End If
    End Sub

    Private Sub BlankTaxDetailsCurrentRowWihtRowNo(ByVal intRowNo As Integer)
        For ii As Integer = 1 To 10
            Dim strII As String = clsCommon.myCstr(ii)
            gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTax" + strII)).Value = Nothing
            gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTaxRate" + strII)).Value = Nothing
            gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTaxAmt" + strII)).Value = Nothing
        Next
    End Sub

    Private Sub UpdateAllTotals()
        Dim dblNetAmt As Double = 0
        For ii As Integer = 0 To gv1.Rows.Count - 1
            If (clsCommon.myLen(gv1.Rows(ii).Cells(colICode).Value) > 0) Then
                dblNetAmt = dblNetAmt + clsCommon.myCdbl(gv1.Rows(ii).Cells(colAmt).Value)
            End If
        Next
        lblTotRAmt.Text = clsCommon.myFormat(dblNetAmt)
    End Sub

    Private Sub btnAddNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddNew.Click
        AddNew()

    End Sub

    Sub AddNew()
        BlankAllControls()
        LoadBlankGrid()
        isNewEntry = True
        btnSave.Text = "Save"
        btnSave.Enabled = True
        btnPost.Enabled = True
        btnDelete.Enabled = True
        UsLock1.Status = ERPTransactionStatus.Pending
        txtDate.Focus()
        cboItemType.SelectedIndex = 0
        cboItemType.Enabled = True
        gv1.Rows.AddNew()

        If EnableStoreCostCentre = 1 Then
            txtDept.Enabled = False
        Else
            txtDept.Enabled = True
        End If

        ''For Custom Fields

        '' updation by preeti gupta
        Dim desc As String = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.EnableProjectFinder, clsFixedParameterCode.EnableProjectFinder, Nothing))
        If clsCommon.CompairString(desc, "1") = CompairStringResult.Equal Then
            fndProject.Enabled = True
        Else
            fndProject.Enabled = False
        End If
        '-----------------------
        If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
            UcCustomFields1.SetDefaultValues()
        End If
        ''End of For Custom Fields
        UcAttachment1.BlankAllControls()
        If ShowDefaultUser Then
            txtRequestBy.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select emp_code from tspl_user_master where user_code='" + objCommonVar.CurrentUserCode + "' "))
            lblRequestBy.Text = clsEmployeeMaster.GetName(txtRequestBy.Value, Nothing)
            txtRequestBy.Enabled = False
            txtDept.Enabled = True
            txtDept.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Segment_Code from TSPL_USER_MASTER where User_Code ='" + objCommonVar.CurrentUserCode + "'"))
            lblDept.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select DEPARTMENT_NAME from TSPL_DEPARTMENT_MASTER WHERE DEPARTMENT_CODE='" + txtDept.Value + "'"))


        Else
            txtRequestBy.Value = ""
            lblRequestBy.Text = ""
            txtRequestBy.Enabled = True
            txtDept.Enabled = True
            txtDept.Value = ""
            lblDept.Text = ""
        End If
        txtUnitCode.Enabled = False
        txtCostCenterType.Enabled = False
        ' txtDept.Enabled = True
        If ShowDefaultUser = False Then
            AllowDepartmentMandatoryOnPurchaseCycle()
        End If



        'txtUnitCode.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Unit_Code from TSPL_COST_CENTER_TYPE_MASTER where Department_Cost='" + txtDept.Value + "' "))
        'lblUnitDesc.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description from TSPL_COST_CENTER_UNIT_MASTER WHERE Code='" + txtUnitCode.Value + "'"))

        'txtCostCenterType.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Code from TSPL_COST_CENTER_TYPE_MASTER where Department_Cost='" + txtDept.Value + "' "))
        'lblCostcenterTypeDesc.Text = txtCostCenterType.Value ' clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Cost_Code from TSPL_COST_CENTER_TYPE_MASTER WHERE Code='" + txtCostCenterType.Value + "'"))

        'colCCCode. = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Cost_Code from TSPL_COST_CENTER_TYPE_MASTER "))

        'clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Segment_code from TSPL_USER_MASTER where User_Code ='" + objCommonVar.CurrentUserCode + "'"))
        'If EnableStoreCostCentre = 1 Then
        '    txtDept.Value = ""
        '    lblDept.Text = ""
        '    txtUnitCode.Value = ""
        '    lblUnitDesc.Text = ""
        '    txtCostCenterType.Value = ""
        '    lblCostcenterTypeDesc.Text = ""

        'End If

        txtLocation.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Default_Location from TSPL_USER_MASTER where User_Code='" + objCommonVar.CurrentUserCode + "' "))
        If clsCommon.myLen(txtLocation.Value) > 0 Then
            lblLocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_Location_Master where Location_Code='" + txtLocation.Value + "' "))
        End If

        isCellValueChangedOpen = False


    End Sub

    Function AllowToSave() As Boolean
        Try
            ' KUNAL > TICKET : BM00000009580 ========
            If AllowFutureDateTransaction(txtDate.Value, Nothing) = False Then
                txtDate.Focus()
                Return False
            End If

            If btnSave.Text = "Update" Then
                Dim strchk As String = "select Status from TSPL_REQUISITION_HEAD where Requisition_Id='" + txtReqNo.Value + "'"
                Dim chkpost As String = clsDBFuncationality.getSingleValue(strchk)
                If chkpost = "1" Then
                    clsCommon.MyMessageBoxShow(Me, "Transection already posted", Me.Text)
                    Return False
                End If
            End If

            UpdateAllTotals()
            If clsCommon.myLen(txtLocation.Value) <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Please select Location", Me.Text)
                txtLocation.Focus()
                Return False
            End If

            If clsCommon.myLen(cboItemType.SelectedValue) <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Please select Item Type", Me.Text)
                cboItemType.Focus()
                Return False
            End If

            If clsCommon.CompairString(clsCommon.myCstr(cboItemType.SelectedValue), "F") = CompairStringResult.Equal AndAlso clsLocation.isLocatinExcisable(txtLocation.Value) Then
                common.clsCommon.MyMessageBoxShow(Me, "Location Can't be excisable of finished goods", Me.Text)
                Return False
            End If

            If clsCommon.myLen(txtDept.Value) <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Please select Department", Me.Text)
                txtDept.Focus()
                Return False
            End If
            'If clsCommon.CompairString("R", cboItemType.SelectedValue) = CompairStringResult.Equal AndAlso Not (clsLocation.isLocatinExcisable(txtLocation.Value)) Then
            '    common.clsCommon.MyMessageBoxShow("Location should be Excisable for Raw Material")
            '    txtLocation.Focus()
            '    Return False
            'End If
            If EnableStoreCostCentre = 1 Then
                'If clsCommon.myLen(txtDept.Value) <= 0 Then
                '    common.clsCommon.MyMessageBoxShow("Please select Department Code")
                '    txtDept.Focus()
                '    Return False
                'End If
                If clsCommon.myLen(txtUnitCode.Value) <= 0 Then
                    common.clsCommon.MyMessageBoxShow(Me, "Please select Unit Code", Me.Text)
                    txtUnitCode.Focus()
                    Return False
                End If
                If clsCommon.myLen(txtCostCenterType.Value) <= 0 Then
                    common.clsCommon.MyMessageBoxShow(Me, "Please select Cost center Type Code", Me.Text)
                    txtCostCenterType.Focus()
                    Return False
                End If
            End If

            Dim arrICode As New List(Of String)()
            For ii As Integer = 0 To gv1.Rows.Count - 1
                Dim strICode As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colICode).Value)
                Dim strIName As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colIName).Value)
                Dim strIQty As Double = clsCommon.myCdbl(gv1.Rows(ii).Cells(colQty).Value)
                Dim strIUOM As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colUnit).Value)
                If clsCommon.myLen(strICode) > 0 Then
                    For jj As Integer = 0 To gv1.Rows.Count - 1
                        If (ii = jj) Then
                            Continue For
                        End If
                        If (clsCommon.CompairString(strICode, clsCommon.myCstr(gv1.Rows(jj).Cells(colICode).Value)) = CompairStringResult.Equal) Then
                            common.clsCommon.MyMessageBoxShow("Already selected Item " + strICode.Trim() + "( " + strIName.Trim() + " ) At Line No: " + clsCommon.myCstr(clsCommon.myCdbl(ii + 1)) + " and  " + clsCommon.myCstr(clsCommon.myCdbl(jj + 1)) + "")
                            Return False
                        End If
                    Next

                    If Not arrICode.Contains(strICode) Then
                        arrICode.Add(strICode)
                    End If

                    ''richa agarwal 1 Dec,2016
                    '==sanjeet(22/01/2018)--To validate with Capex code if capex code has value then Hierarchy Level is not mandetory======
                    If clsCommon.myLen(fndcapexsubcode.Value) > 0 Then
                        If clsCommon.myCdbl(lbl_rebudgetamtwithtolerence.Text) < clsCommon.myCdbl(lblTotRAmt.Text) Then
                            Dim response = MsgBox("Warning: Document amount exceed budget amount and above tolerence limit.", MsgBoxStyle.YesNo, "Attention")
                            If response = MsgBoxResult.No Then
                                Return False
                            End If
                            'clsCommon.MyMessageBoxShow("Warning: Document amount exceed budget amount and above tolerence limit.")
                            ' Return False
                        End If
                        If clsCommon.myCdbl(lbl_rebudgetamt.Text) < clsCommon.myCdbl(lblTotRAmt.Text) AndAlso clsCommon.myCdbl(lbl_rebudgetamtwithtolerence.Text) > clsCommon.myCdbl(lblTotRAmt.Text) Then
                            clsCommon.MyMessageBoxShow(Me, "Warning: Document amount exceed budget amount but under tolerence limit.", Me.Text)
                        End If
                    Else
                        If clsCommon.CompairString(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.ShowCostCenterAndHierarchyLevelInPurchaseModule, clsFixedParameterCode.ShowCostCenterAndHierarchyLevelInPurchaseModule, Nothing)), "1") = CompairStringResult.Equal Then
                            If clsCommon.myLen(clsCommon.myCstr(gv1.Rows(ii).Cells(colHierarchyCode).Value)) <= 0 Then
                                common.clsCommon.MyMessageBoxShow(Me, " Please select Hierarchy Level ", Me.Text)
                                Return False
                            End If
                            If clsCommon.myLen(clsCommon.myCstr(gv1.Rows(ii).Cells(colCostCenterCode).Value)) <= 0 Then
                                common.clsCommon.MyMessageBoxShow(Me, " Please select Cost Center ", Me.Text)
                                Return False
                            End If
                        Else
                            Dim strCostCode As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colCCCode).Value)
                            If clsCommon.myLen(strCostCode) <= 0 Then
                                common.clsCommon.MyMessageBoxShow("Please provide cost code for item : " + strICode + " . At Line No" + clsCommon.myCstr(ii + 1))
                                Return False
                            End If
                        End If
                    End If
                    '=========================== Ticket No : UDL/13/03/19-000278 By Prabhakar
                    If strIQty <= 0 Then
                        common.clsCommon.MyMessageBoxShow("Please Enter Qty for item : " + strICode + " . At Line No" + clsCommon.myCstr(ii + 1))
                        Return False
                    End If
                    If clsCommon.myLen(strIUOM) <= 0 Then
                        common.clsCommon.MyMessageBoxShow("Please Enter UOM for item : " + strICode + " . At Line No" + clsCommon.myCstr(ii + 1))
                        Return False
                    End If
                End If
            Next
            If ShowItemAllStructureWise = False Then
                clsItemMaster.isItemOfSameType(clsCommon.myCstr(cboItemType.SelectedValue), cboItemType.Text, arrICode)
            End If
            UcCustomFields1.AllowToSave()
            UcAttachment1.AllowToSave()
            '--------------richa 09/07/2014 Ticket No BM00000003042---------
            CheckQuantityForPurchaseRequisition()
            ''--------------------------------------------------------------
            '' Anubhooti 13-Sep-2014 BM00000003735
            If FrmMainTranScreen.ValidateTransactionAccToFinYear("Purchase Requisition", txtDate.Value) = False Then
                Exit Function
            End If
            ''
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            Return False
        End Try
        Return True
    End Function

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        SaveData(False)
    End Sub

    Sub SaveData(ByVal ChekBtnPost As Boolean)
        Try
            If (AllowToSave()) Then
                Dim obj As New clsRequistionHead()
                obj.Requisition_Id = txtReqNo.Value
                obj.Requisition_Date = txtDate.Value
                obj.Cust_OrderNo = ""
                If txtExpireDate.Checked Then
                    obj.Expire_Date = txtExpireDate.Value
                End If
                If txtRequiredDate.Checked Then
                    obj.Require_Date = txtRequiredDate.Value
                End If
                obj.Ref_No = ""
                obj.Description = txtDesc.Text
                obj.Remarks = txtRmks.Text
                obj.On_Hold = IIf(chkInternal.Checked, 1, 0)
                obj.Location = txtLocation.Value
                obj.RQ_Detail_Total_Amt = clsCommon.myCdbl(lblTotRAmt.Text)
                obj.Total_RQ_Amt = clsCommon.myCdbl(lblTotRAmt.Text)
                obj.Mode_Of_Transport = ""
                obj.Comments = txtComment.Text
                obj.Is_Internal = IIf(chkInternal.Checked, "Y", "N")
                If ShowItemAllStructureWise = False Then
                    obj.Item_Type = clsCommon.myCstr(cboItemType.SelectedValue)
                Else
                    obj.Item_Type = "A"
                End If
                obj.Dept = txtDept.Value
                    obj.Dept_Desc = lblDept.Text
                    obj.Request_By = txtRequestBy.Value
                    obj.Requisition_Type = ""
                    obj.PROJECT_ID = fndProject.Value
                    '====Sanjeet(22/01/2018)===
                    obj.Capex_Code = fndcapexcode.Value
                    obj.Capex_SubCode = fndcapexsubcode.Value
                    obj.CosCenter_Unit = txtUnitCode.Value
                    obj.CostCenter_Type = txtCostCenterType.Value
                    '=======================
                    'Sanjay
                    obj.SubCapex_Amount = clsCommon.myCdbl(lbl_budgetamt.Text)
                    obj.SubCapex_AmountWithTol = clsCommon.myCdbl(lbl_budgetamtwithtolerence.Text)
                    obj.SubCapex_BalAmount = clsCommon.myCdbl(lbl_rebudgetamt.Text)
                    obj.SubCapex_BalAmountWithTol = clsCommon.myCdbl(lbl_rebudgetamtwithtolerence.Text)
                    'Sanjay
                    If chkprclose.Checked = True Then
                        obj.close_yn = "Y"
                    ElseIf chkprclose.Checked = False Then
                        obj.close_yn = "N"
                    End If

                    Dim dt As DataTable = clsDBFuncationality.GetDataTable("Select Level1, Level2 from TSPL_REQUISITION_APPROVAL")
                    If dt.Rows.Count > 0 Then
                        If clsCommon.myCdbl(lblTotRAmt.Text) <= clsCommon.myCdbl(dt.Rows(0)("Level1")) Then
                            obj.Approvel_Level_Required = 1
                        ElseIf clsCommon.myCdbl(lblTotRAmt.Text) > clsCommon.myCdbl(dt.Rows(0)("Level1")) And clsCommon.myCdbl(lblTotRAmt.Text) <= clsCommon.myCdbl(dt.Rows(0)("Level2")) Then
                            obj.Approvel_Level_Required = 2
                        Else
                            obj.Approvel_Level_Required = 3
                        End If
                    End If
                    obj.ArrTr = New List(Of clsRequistionDetail)
                    For Each grow As GridViewRowInfo In gv1.Rows
                        Dim objTr As New clsRequistionDetail()
                        objTr.Item_Code = clsCommon.myCstr(grow.Cells(colICode).Value)
                        objTr.Item_Desc = clsCommon.myCstr(grow.Cells(colIName).Value)
                        objTr.Vendor_Code = clsCommon.myCstr(grow.Cells(colVendorCode).Value)
                        objTr.Requisition_Qty = clsCommon.myCdbl(grow.Cells(colQty).Value)
                        objTr.Balance_Qty = clsCommon.myCdbl(grow.Cells(colQty).Value)
                        objTr.Location = txtLocation.Value 'clsCommon.myCstr(grow.Cells(colLocationCode).Value)
                        objTr.Item_Cost = clsCommon.myCdbl(grow.Cells(colRate).Value)
                        objTr.Unit_Code = clsCommon.myCstr(grow.Cells(colUnit).Value)
                        objTr.Item_Net_Amt = clsCommon.myCdbl(grow.Cells(colAmt).Value)
                        objTr.Vendor_ItemNo = clsCommon.myCstr(grow.Cells(colVendorItemNo).Value)
                        objTr.Order_No = clsCommon.myCstr(grow.Cells(colOrderNo).Value)
                        objTr.Status = "N"

                        objTr.Specification = clsCommon.myCstr(grow.Cells(colSpecification).Value)
                        objTr.Remarks = clsCommon.myCstr(grow.Cells(colRemarks).Value)

                        ''richa agarwal 1 Dec ,2016
                        If clsCommon.CompairString(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.ShowCostCenterAndHierarchyLevelInPurchaseModule, clsFixedParameterCode.ShowCostCenterAndHierarchyLevelInPurchaseModule, Nothing)), "1") = CompairStringResult.Equal Then
                            objTr.Hirerachy_Code = clsCommon.myCstr(grow.Cells(colHierarchyCode).Value)
                            objTr.Cost_Centre_Code = clsCommon.myCstr(grow.Cells(colCostCenterCode).Value)
                            If EnableHirerachyCostCentre = 1 Then
                                objTr.HirerachyLevelCode3 = clsCommon.myCstr(grow.Cells(colHierarchyLevel2).Value)
                                objTr.HirerachyLevelCode4 = clsCommon.myCstr(grow.Cells(colHierarchyLevel3).Value)
                            End If
                        Else
                            objTr.CostCode = clsCommon.myCstr(grow.Cells(colCCCode).Value)
                        End If


                        'objTr.Order_No = clsCommon.myCdbl(grow.Cells(colorderno).Value)
                        If (clsCommon.myLen(objTr.Item_Code) > 0) Then
                            obj.ArrTr.Add(objTr)
                        End If
                    Next


                    If (obj.ArrTr Is Nothing OrElse obj.ArrTr.Count <= 0) Then
                        common.clsCommon.MyMessageBoxShow(Me, "Please Fill at list one Item", Me.Text)
                        Return
                    End If

                    ''For Custom Fields
                    obj.Form_ID = MyBase.Form_ID
                    obj.arrCustomFields = New List(Of clsCustomFieldValues)
                    If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
                        UcCustomFields1.GetData(obj.arrCustomFields)
                    End If
                    If MyBase.ArrDetailFields IsNot Nothing AndAlso MyBase.ArrDetailFields.Count > 0 Then
                        clsCustomFieldGrid.GetData(obj.arrCustomFields, gv1, MyBase.ArrDetailFields, colICode)
                    End If
                    ''End of For Custom Fields

                    If (obj.SaveData(obj, isNewEntry)) Then
                        UcAttachment1.SaveData(obj.Requisition_Id)
                        If ChekBtnPost = False Then
                            common.clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                        End If
                        LoadData(obj.Requisition_Id, NavigatorType.Current)
                        'If objCommonVar.IsDemoERP Then
                        '    SendMail()
                        'End If

                    End If

                End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    '--------------richa 09/07/2014 Ticket No BM00000003042---------
    Sub CheckQuantityForPurchaseRequisition()
        Dim desc As String = ""
        Dim strCondition As String = ""
        Dim strCondition1 As String = ""
        Dim StrMessage As String = Nothing


        desc = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.NotificationSettingforReOrderInPurchaseRequisition, clsFixedParameterCode.NotificationSettingforReOrderInPurchaseRequisition, Nothing))
        If clsCommon.CompairString(desc, "1") = CompairStringResult.Equal Then
            If btnSave.Text = "Update" Then
                strCondition = " where 1=1 and TSPL_REQUISITION_DETAIL.Requisition_Id<>'" + txtReqNo.Value + "' "
                'strCondition1 = " and TSPL_PURCHASE_ORDER_DETAIL.Requisition_Id<>'" + txtReqNo.Value + "' "
            End If

            For Each grow As GridViewRowInfo In gv1.Rows
                Dim StockBalQty As Double = 0
                Dim ReorderLevel As Double = 0
                Dim MaxLevel As Double = 0
                Dim MinLevel As Double = 0
                Dim RequiredQty As Double = 0
                Dim QtyAfterAddingStockQty As Double = 0
                Dim SumOfQtyofRequest As Double = 0

                If (clsCommon.myLen(grow.Cells(colICode).Value) > 0) Then
                    Dim StrItemCode = clsCommon.myCstr(grow.Cells(colICode).Value)
                    Dim PurchaseQty = clsCommon.myCdbl(grow.Cells(colQty).Value)
                    Dim Qry As String = ""
                    Qry = "Select TSPL_ITEM_REORDER_LEVEL_NEW.Item_Code, TSPL_ITEM_MASTER.Item_Desc,TSPL_ITEM_MASTER.Unit_Code, FINAL.ActualBalanceQty As StockQty, ISNUll(TSPL_ITEM_REORDER_LEVEL_NEW.Reorder_Level,0) As ReOrder_Level,ISNULL(TSPL_ITEM_REORDER_LEVEL_NEW.Min_Level,0) As Min_Level,ISNULL(TSPL_ITEM_REORDER_LEVEL_NEW.Max_Level,0) As Max_Level  from (" & _
                  " select ICode,SUM(Qty * case when TransType='' then 1 else 0 end)as BalanceQty,SUM(Qty * case when TransType='' then 0 else 1 end)as CommitQty,SUM(Qty *RI )as ActualBalanceQty from (select  xx.TransType,xx.TransCode,xx.DocNo, xx.ICode,xx.Location,xx.RI ,TSPL_ITEM_UOM_DETAIL.Conversion_Factor,( (xx.Qty*TSPL_ITEM_UOM_DETAIL.Conversion_Factor)/FinalUOM.Conversion_Factor) as Qty" & _
                  " from (" & _
                  " select '' as TransType,'' as TransCode,'' as DocNo, Item_Code as ICode,Location_Code as Location ,SUM(QtyNew*RI) as Qty,1 as RI,UOMNew as UOM  from( select Trans_Id,Item_Code ,Location_Code,case when InOut='I' then 1 else -1 end as RI,Qty as QtyNew,UOMNew from( select TSPL_INVENTORY_MOVEMENT.Trans_Id, TSPL_INVENTORY_MOVEMENT.Item_Code ,TSPL_INVENTORY_MOVEMENT.Location_Code , TSPL_INVENTORY_MOVEMENT.InOut,TSPL_INVENTORY_MOVEMENT.Qty   ,TSPL_INVENTORY_MOVEMENT.UOM as UOMNew  from TSPL_INVENTORY_MOVEMENT  where TSPL_INVENTORY_MOVEMENT.Qty<>0 and 2=(case when TSPL_INVENTORY_MOVEMENT.InOut='O' then 2 else case when TSPL_INVENTORY_MOVEMENT.InOut='I' and TSPL_INVENTORY_MOVEMENT.Punching_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(clsCommon.GETSERVERDATE), "dd/MMM/yyyy hh:mm tt") + "' then 2 else 0 end end)  )xxx   )xxxx group by Item_Code,Location_Code,UOMNew" & _
                  " union all" & _
                  " select 'Purchase Return' as TransType,'PurchaseReturn' as TransCode,TSPL_PR_HEAD.PR_No as DocNo, TSPL_PR_DETAIL.Item_Code as ICode,TSPL_PR_DETAIL.Location as Locaion,TSPL_PR_DETAIL.PR_Qty as Qty,-1 as RI,TSPL_PR_DETAIL.Unit_code AS Uom  from TSPL_PR_DETAIL  left outer join TSPL_PR_HEAD on TSPL_PR_HEAD.PR_No=TSPL_PR_DETAIL.PR_No where TSPL_PR_HEAD.Status=0 and TSPL_PR_DETAIL.PR_Qty<>0" & _
                  " UNION ALL" & _
                  " select 'IC-AD' as TransType,'ICAdj' as TransCode,TSPL_ADJUSTMENT_HEADER.Adjustment_No as DocNo, TSPL_ADJUSTMENT_DETAIL.Item_Code as ICode,TSPL_ADJUSTMENT_HEADER.Loc_Code as Locaion,TSPL_ADJUSTMENT_DETAIL.Item_Quantity as Qty,-1 as RI,TSPL_ADJUSTMENT_DETAIL.Unit_Code AS Uom  from TSPL_ADJUSTMENT_DETAIL  left outer join TSPL_ADJUSTMENT_HEADER on TSPL_ADJUSTMENT_HEADER.Adjustment_No=TSPL_ADJUSTMENT_DETAIL.Adjustment_No where TSPL_ADJUSTMENT_HEADER.Posted='N' and TSPL_ADJUSTMENT_DETAIL.Item_Quantity<>0  and TSPL_ADJUSTMENT_DETAIL.Adjustment_Type in ('BD','QD')" & _
                  " UNION ALL" & _
                  " select 'RGP' as TransType,'RGP' as TransCode,TSPL_RGP_HEAD.RGP_No as DocNo, TSPL_RGP_DETAIL.Item_Code as ICode,TSPL_RGP_HEAD.Location as Locaion,TSPL_RGP_DETAIL.RGP_Qty as Qty,-1 as RI,TSPL_RGP_DETAIL.Unit_code AS Uom  from TSPL_RGP_DETAIL  left outer join TSPL_RGP_HEAD on TSPL_RGP_HEAD.RGP_No=TSPL_RGP_DETAIL.RGP_No where TSPL_RGP_HEAD.Status=0 and TSPL_RGP_DETAIL.RGP_Qty<>0" & _
                  " union all" & _
                  " select 'Scrap' as TransType,'ScrapShipment' as TransCode,TSPL_SCRAPSALE_HEAD.shipment_No as DocNo, TSPL_SCRAPSALE_DETAIL.Item_Code as ICode,TSPL_SCRAPSALE_HEAD.Loc_Code as Locaion,TSPL_SCRAPSALE_DETAIL.shipped_Qty as Qty,-1 as RI,TSPL_SCRAPSALE_DETAIL.Unit_code AS Uom  from TSPL_SCRAPSALE_DETAIL  left outer join TSPL_SCRAPSALE_HEAD on TSPL_SCRAPSALE_HEAD.shipment_No=TSPL_SCRAPSALE_DETAIL.shipment_No where TSPL_SCRAPSALE_HEAD.IsPost=0 and TSPL_SCRAPSALE_DETAIL.shipped_Qty<>0" & _
                  " union all" & _
                  " select 'Issue/Return/Transfer' as TransType,'IssueReturnTransfer' as TransCode,TSPL_IssueReturn_HEAD.Doc_No as DocNo, TSPL_IssueReturn_DETAIL.Item_Code as ICode,TSPL_IssueReturn_HEAD.From_Location as Locaion,TSPL_IssueReturn_DETAIL.Issued_Qty as Qty,-1 as RI,TSPL_IssueReturn_DETAIL.Unit_code AS Uom  from TSPL_IssueReturn_DETAIL  left outer join TSPL_IssueReturn_HEAD on TSPL_IssueReturn_HEAD.Doc_No=TSPL_IssueReturn_DETAIL.Doc_No where TSPL_IssueReturn_HEAD.Status=0 and TSPL_IssueReturn_DETAIL.Issued_Qty<>0" & _
                  " union all" & _
                  " select  'Shipment' as TransType,'SDShipment' as TransCode,TSPL_SD_SHIPMENT_HEAD.Document_Code as DocNo, TSPL_SD_SHIPMENT_DETAIL.Item_Code as ICode,TSPL_SD_SHIPMENT_HEAD.Bill_To_Location as Locaion,TSPL_SD_SHIPMENT_DETAIL.Qty as Qty,-1 as RI,TSPL_SD_SHIPMENT_DETAIL.Unit_code AS Uom   from TSPL_SD_SHIPMENT_DETAIL  left outer join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_Code=TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE where TSPL_SD_SHIPMENT_HEAD.Status=0 and TSPL_SD_SHIPMENT_DETAIL.Qty<>0" & _
                  " union all" & _
                  " Select '' AS TransType, '' AS TransCode, TSPL_REQUISITION_DETAIL.Requisition_Id, Case When ISNULL(PurchaseOrder_No,'')='' Then TSPL_REQUISITION_DETAIL.Item_Code Else TSPL_PURCHASE_ORDER_DETAIL.Item_Code End As Item_Code, TSPL_REQUISITION_DETAIL.Location, Case When ISNULL(Requisition_Qty,0)>ISNULL(PurchaseOrder_Qty,0) Then Requisition_Qty Else PurchaseOrder_Qty End as Qty, 1 as RI, TSPL_REQUISITION_DETAIL.Unit_Code from TSPL_REQUISITION_DETAIL LEFT OUTER JOIN TSPL_PURCHASE_ORDER_DETAIL ON TSPL_REQUISITION_DETAIL.Requisition_Id=TSPL_PURCHASE_ORDER_DETAIL.Requisition_Id " + strCondition + " " & _
                    " union all" & _
                    " Select '' AS TransType, '' AS TransCode, TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No, Case When ISNULL(PurchaseOrder_No,'')='' Then TSPL_REQUISITION_DETAIL.Item_Code Else TSPL_PURCHASE_ORDER_DETAIL.Item_Code End As Item_Code, TSPL_PURCHASE_ORDER_DETAIL.Location, Case When ISNULL(Requisition_Qty,0)>ISNULL(PurchaseOrder_Qty,0) Then Requisition_Qty Else PurchaseOrder_Qty End as Qty, 1 as RI, TSPL_PURCHASE_ORDER_DETAIL.Unit_code from   TSPL_PURCHASE_ORDER_DETAIL LEFT OUTER JOIN TSPL_REQUISITION_DETAIL ON TSPL_REQUISITION_DETAIL.Requisition_Id=TSPL_PURCHASE_ORDER_DETAIL.Requisition_Id WHERE ISNULL(TSPL_REQUISITION_DETAIL.Requisition_Id,'')='' " & _
                    " union all" & _
                  " Select 'SRN' As TransType,'SRN' As TransCode,TSPL_SRN_HEAD.SRN_No  AS DocNo,TSPL_SRN_DETAIL .Item_Code As ICODE,TSPL_SRN_HEAD.Bill_To_Location as Location, TSPL_SRN_DETAIL.SRN_Qty  As Qty,1 as RI,TSPL_SRN_DETAIL.Unit_code As UnitCode from TSPL_SRN_HEAD Left Outer Join TSPL_SRN_DETAIL on TSPL_SRN_HEAD.SRN_No=TSPL_SRN_DETAIL.SRN_No where TSPL_SRN_HEAD.Status<>1 and ISNULL(TSPL_SRN_HEAD.Against_PO,'')='' and ISNULL(TSPL_SRN_HEAD.Against_Requisition,'')='' " & _
                  " union all" & _
                  " select 'Assemblies' as TransType,'Assemblies' as TransCode,TSPL_PJC_ASSEMBLIES.CODE as DocNo, Main_Item_Code as ICode,LOCATION_CODE as Location,QUANTITY,(case when TRANSACTION_TYPE='Assembly' then 1  else -1 end) as RI, BUILD_ITEM_UNIT_CODE as UnitCode from TSPL_PJC_ASSEMBLIES where TSPL_PJC_ASSEMBLIES.POSTED=0" & _
                  " union all" & _
                  " select  'Assemblies' as TransType,'Assemblies' as TransCode,TSPL_PJC_ASSEMBLIES.CODE as DocNo, TSPL_MF_BOM_DETAIL.CONSM_ITEM_CODE AS ICode,TSPL_PJC_ASSEMBLIES.LOCATION_CODE as Location, TSPL_MF_BOM_DETAIL.CONSM_QUANTITY*(TSPL_PJC_ASSEMBLIES.QUANTITY/TSPL_PJC_ASSEMBLIES.BUILD_QUANTITY) as Qty, (case when TSPL_PJC_ASSEMBLIES.TRANSACTION_TYPE='Assembly' then  -1 else  1 end) AS RI, TSPL_MF_BOM_DETAIL.CONSM_ITEM_UNIT_CODE as UnitCode from TSPL_PJC_ASSEMBLIES  inner join TSPL_MF_BOM_HEAD on TSPL_MF_BOM_HEAD.BOM_CODE=TSPL_PJC_ASSEMBLIES.BOM_CODE inner JOIN TSPL_MF_BOM_DETAIL ON TSPL_MF_BOM_HEAD.BOM_CODE=TSPL_MF_BOM_DETAIL.BOM_CODE  where TSPL_PJC_ASSEMBLIES.POSTED=0)xx" & _
                  " left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=xx.ICode and TSPL_ITEM_UOM_DETAIL.UOM_Code=xx.UOM  left outer join TSPL_ITEM_UOM_DETAIL as FinalUOM on FinalUOM.Item_Code=xx.ICode AND FinalUOM.Stocking_Unit='Y' and xx.Location='" + txtLocation.Value + "')FinalQry group by ICode" & _
                  " ) FINAL RIGHT OUTER JOIN TSPL_ITEM_REORDER_LEVEL_NEW ON TSPL_ITEM_REORDER_LEVEL_NEW.Item_Code=FINAL.ICode LEFT OUTER JOIN TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code=TSPL_ITEM_REORDER_LEVEL_NEW.Item_Code WHERE TSPL_ITEM_REORDER_LEVEL_NEW.Item_Code='" + StrItemCode + "'"
                    Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                        StockBalQty = clsCommon.myCdbl(dt.Rows(0)("StockQty"))
                        ReorderLevel = clsCommon.myCdbl(dt.Rows(0)("ReOrder_Level"))
                        MaxLevel = clsCommon.myCdbl(dt.Rows(0)("Max_Level"))
                        MinLevel = clsCommon.myCdbl(dt.Rows(0)("Min_Level"))
                        QtyAfterAddingStockQty = StockBalQty + PurchaseQty
                        If QtyAfterAddingStockQty > MaxLevel Then
                            RequiredQty = QtyAfterAddingStockQty - MaxLevel
                            StrMessage = "Some Items have reached above to their MAX LEVEL "
                        End If
                    End If
                End If

            Next
            If StrMessage <> "" Then
                clsCommon.MyMessageBoxShow(StrMessage)
            End If
        End If
    End Sub

    ''---------------------Richa Code Ends Here-------------------------
    Sub LoadData(ByVal strDocumentNo As String, ByVal navType As common.NavigatorType)
        Try
            btnSave.Enabled = True
            btnPost.Enabled = True
            btnDelete.Enabled = True
            isInsideLoadData = True
            isNewEntry = False
            btnSave.Text = "Update"
            UsLock1.Status = ERPTransactionStatus.Pending
            cboItemType.Enabled = False
            txtLocation.Enabled = False
            'txtUnitCode.ReadOnly = True
            BlankAllControls()
            LoadBlankGrid()
            Dim obj As New clsRequistionHead()
            obj = clsRequistionHead.GetData(strDocumentNo, navType, "Y")
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Requisition_Id) > 0) Then

                If obj.close_yn = "Y" Then
                    vaddnew = "Y"
                    chkprclose.Checked = True
                ElseIf obj.close_yn = "N" Then
                    chkprclose.Checked = False
                    vaddnew = "N"
                End If

                If obj.Status = ERPTransactionStatus.Approved Then
                    btnSave.Enabled = False
                    btnPost.Enabled = False
                    btnDelete.Enabled = False

                    repoComplete.IsVisible = True
                    repoBalQty.IsVisible = True
                    btncancel.Enabled = True
                Else
                    btncancel.Enabled = False
                End If
                If chkprclose.Checked Then
                    vaddnew = "N"
                    btncancel.Enabled = False
                End If

                txtReqNo.Value = obj.Requisition_Id
                txtDate.Value = obj.Requisition_Date
                UsLock1.Status = obj.Status
                txtExpireDate.Checked = IIf(obj.Expire_Date Is Nothing, False, True)
                If txtExpireDate.Checked Then
                    txtExpireDate.Value = obj.Expire_Date
                End If
                txtRequiredDate.Checked = IIf(obj.Require_Date Is Nothing, False, True)
                If txtRequiredDate.Checked Then
                    txtRequiredDate.Value = obj.Require_Date
                End If
                txtDesc.Text = obj.Description
                txtRmks.Text = obj.Remarks
                'chkInternal.Checked = IIf(obj.On_Hold, 1, 0)
                If obj.Is_Internal = "Y" Then
                    chkInternal.Checked = True
                Else
                    chkInternal.Checked = False
                End If
                txtLocation.Value = obj.Location
                lblLocation.Text = obj.LocationName
                lblTotRAmt.Text = clsCommon.myFormat(obj.RQ_Detail_Total_Amt)
                txtComment.Text = obj.Comments
                If ShowItemAllStructureWise = False Then
                    cboItemType.SelectedValue = obj.Item_Type
                Else
                    LoadItemType()
                End If
                txtDept.Value = obj.Dept
                lblDept.Text = obj.Dept_Desc
                txtUnitCode.Value = obj.unit
                lblUnitDesc.Text = obj.unit_Desc
                txtCostCenterType.Value = obj.Cost
                lblCostcenterTypeDesc.Text = obj.Cost_Desc
                txtRequestBy.Value = obj.Request_By
                    lblRequestBy.Text = clsEmployeeMaster.GetName(obj.Request_By, Nothing)
                    fndProject.Value = obj.PROJECT_ID
                    lblProject.Text = clsDBFuncationality.getSingleValue("select SPECIFICATION from TSPL_PJC_PROJECT where PROJECT_CODE='" + fndProject.Value + "'")
                    ''==Sanjeet (Load CAPEX Detail and check it's amount)======
                    fndcapexcode.Value = obj.Capex_Code
                    If clsCommon.myLen(Me.fndcapexcode.Value) > 0 Then
                        lbl_capexcode.Text = clsCapexMaster.GetName(Me.fndcapexcode.Value, Nothing)
                    End If
                    fndcapexsubcode.Value = obj.Capex_SubCode
                    If clsCommon.myLen(Me.fndcapexsubcode.Value) > 0 Then
                        lbl_capexsubcode.Text = clsCapexBudget.GetName(Me.fndcapexsubcode.Value, Nothing)
                        lbl_budgetamt.Text = clsCapexBudget.GetBudget(Me.fndcapexsubcode.Value, Nothing)
                        lbl_budgetamtwithtolerence.Text = clsCapexBudget.GetBudgetWithTolerence(Me.fndcapexsubcode.Value, Nothing)
                        lbl_rebudgetamt.Text = clsCapexBudget.GetReBudget(Me.fndcapexsubcode.Value, obj.Requisition_Id, Nothing)
                        lbl_rebudgetamtwithtolerence.Text = clsCapexBudget.GetReBudgetWithTolerence(Me.fndcapexsubcode.Value, obj.Requisition_Id, Nothing)
                    End If
                    'Use if you want to show store balance
                    'lbl_budgetamt.Text = clsCommon.myCstr(obj.SubCapex_Amount)
                    'lbl_budgetamtwithtolerence.Text = clsCommon.myCstr(obj.SubCapex_AmountWithTol)
                    'lbl_rebudgetamt.Text = clsCommon.myCstr(obj.SubCapex_BalAmount)
                    'lbl_rebudgetamtwithtolerence.Text = clsCommon.myCstr(obj.SubCapex_BalAmountWithTol)
                    'Use if you want to show store balance

                    txtUnitCode.Value = obj.CosCenter_Unit
                    If clsCommon.myLen(Me.txtUnitCode.Value) > 0 Then
                        lblUnitDesc.Text = clsUnitMaster.GetName(Me.txtUnitCode.Value)
                    End If
                    txtCostCenterType.Value = obj.CostCenter_Type
                    If clsCommon.myLen(Me.txtCostCenterType.Value) > 0 Then
                        lblCostcenterTypeDesc.Text = clsCostCenterTypeMaster.GetName(Me.txtCostCenterType.Value)
                    End If
                    '======================
                    For Each objTr As clsRequistionDetail In obj.ArrTr
                        gv1.Rows.AddNew()
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = objTr.Line_No
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colComplete).Value = objTr.Status
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value = objTr.Item_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colIName).Value = objTr.Item_Desc
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colVendorCode).Value = objTr.Vendor_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colVendorName).Value = objTr.VendorName
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colBalanceQty).Value = objTr.Balance_Qty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value = objTr.Requisition_Qty
                        ''gv1.Rows(gv1.Rows.Count - 1).Cells(colLocationCode).Value = objTr.Location
                        ''gv1.Rows(gv1.Rows.Count - 1).Cells(colLocationName).Value = objTr.LocationName
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colRate).Value = objTr.Item_Cost
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colUnit).Value = objTr.Unit_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colAmt).Value = objTr.Item_Net_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colVendorItemNo).Value = objTr.Vendor_ItemNo
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colOrderNo).Value = objTr.Order_No

                        gv1.Rows(gv1.Rows.Count - 1).Cells(colSpecification).Value = objTr.Specification
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colRemarks).Value = objTr.Remarks

                        ''richa agarwal 1 Dec ,2016
                        If clsCommon.CompairString(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.ShowCostCenterAndHierarchyLevelInPurchaseModule, clsFixedParameterCode.ShowCostCenterAndHierarchyLevelInPurchaseModule, Nothing)), "1") = CompairStringResult.Equal Then
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colHierarchyCode).Value = objTr.Hirerachy_Code
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colCostCenterCode).Value = objTr.Cost_Centre_Code
                            If EnableHirerachyCostCentre = 1 Then
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colHierarchyLevel2).Value = objTr.HirerachyLevelCode3
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colHierarchyLevel3).Value = objTr.HirerachyLevelCode4
                                OpenCostCentreLevelName(False)
                            End If

                        Else
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colCCCode).Value = objTr.CostCode
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colCCDesc).Value = ClsCostCenter.GetCostCenterDesc(objTr.CostCode)
                        End If


                    Next
                    If obj.Status = ERPTransactionStatus.Pending Then
                        gv1.Rows.AddNew()
                    End If

                    ''For Custom Fields
                    If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
                        UcCustomFields1.LoadData(obj.Requisition_Id)
                    End If
                    clsCustomFieldGrid.FillDataInGrid(obj.Requisition_Id, MyBase.Form_ID, gv1)
                    ''End of For Custom Fields
                    UcAttachment1.LoadData(obj.Requisition_Id)

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

    Private Sub btnPost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPost.Click
        PostData()
    End Sub

    Sub PostData()
        Try
            If (myMessages.postConfirm()) Then
                ' '' Anubhooti 13-Sep-2014 BM00000003735
                If FrmMainTranScreen.ValidateTransactionAccToFinYear("Purchase Requisition", txtDate.Value) = False Then
                    Exit Sub
                End If
                ''
                If (clsRequistionHead.PostData(txtReqNo.Value)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Data Posted Successfully ", Me.Text)
                    LoadData(txtReqNo.Value, NavigatorType.Current)
                    '=send sms at post if setting is on===================
                    If clsSMSAtPost_Purchase.SMSATPOST_PUR() Then
                        SMSSendOnly(True)
                    End If
                    '====================================================
                    If (common.clsCommon.MyMessageBoxShow("Do you want to print", Me.Text, MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes) Then
                        funPrint()
                    End If

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
                If (clsRequistionHead.DeleteData(txtReqNo.Value)) Then
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
        obj.DOCUMENT_NO = clsCommon.myCstr(Me.txtReqNo.Value)
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
                Dim autoFilter As FilterDescriptor = New FilterDescriptor("Description", FilterOperator.StartsWith, "")
                autoFilter.IsFilterEditor = True
                editor.EditorControl.FilterDescriptors.Add(autoFilter)
            End If
        End If
    End Sub

    'Private Function funSetUserAccess() As Boolean
    '    Try
    '        btnSave.Visible = True
    '        btnDelete.Visible = True
    '        btnPost.Visible = True

    '        Dim strRights As String
    '        Dim strTemp() As String
    '        Dim strProgCode = "PO-REQ"
    '        strRights = enuUserRights.enuRead & "," & enuUserRights.enuModify & "," & enuUserRights.enuDelete & "," & enuUserRights.enuAuthorised
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
    '            btnSave.Visible = False
    '        End If
    '        If strTemp(2) = "0" Then 'Grant modify access
    '            btnDelete.Visible = False
    '        End If
    '        If strTemp(3) = "0" Then 'Grant Authorize access
    '            btnPost.Visible = False
    '        End If
    '        funSetUserAccess = True
    '    Catch er As Exception

    '    End Try
    'End Function

    Private Sub txtDocNo__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtReqNo._MYNavigator
        Try
            'Dim qst As String = "select count(*) from TSPL_REQUISITION_HEAD where bank_code='" + txtReqNo.Value + "'"
            vaddnew = "Y"
            Dim qst As String = "select count(*) from TSPL_REQUISITION_HEAD  where Is_Internal='Y'"

            Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qst))
            If count = 0 Then
                txtReqNo.MyReadOnly = False
            Else
                txtReqNo.MyReadOnly = True
            End If
            LoadData(txtReqNo.Value, NavType)
        Catch ex As Exception
            vaddnew = "N"
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtDocNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtReqNo._MYValidating
        'Ticket No  ERO/20/08/19-000997 ,Sanjay
        vaddnew = "Y"
        Dim qry As String = "select TSPL_REQUISITION_HEAD.Requisition_Id as Code,TSPL_REQUISITION_HEAD.Requisition_Date as Date,TSPL_REQUISITION_HEAD.Description, case when TSPL_REQUISITION_HEAD.Status='0' then 'Pending' else 'Approved' end as [Status],case when  TSPL_REQUISITION_HEAD.Is_Internal='Y' then 'Internal' else 'External' end as Internal,case when TSPL_EMPLOYEE_MASTER.Emp_Name is null then TSPL_REQUISITION_HEAD.Request_By else TSPL_EMPLOYEE_MASTER.Emp_Name end as [Request By],Dept_Desc as [Department] from TSPL_REQUISITION_HEAD left join TSPL_EMPLOYEE_MASTER on TSPL_EMPLOYEE_MASTER.EMP_CODE= TSPL_REQUISITION_HEAD.Request_By "
        Dim whrClas As String = " Is_Internal='Y'"
        If ApplyDepartmentWiseDataVisibleInDepartmentIndent = True Then
            whrClas += " and TSPL_REQUISITION_HEAD.Dept in (select Segment_code from tspl_User_Master where User_Code = '" + objCommonVar.CurrentUserCode + "')"
        End If
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            whrClas += " and  Location in (" + objCommonVar.strCurrUserLocations + ")"
        End If
        LoadData(clsCommon.ShowSelectForm("PRReqfndNo", qry, "Code", whrClas, txtReqNo.Value, "Code", isButtonClicked, "Requisition_Date"), NavigatorType.Current)

    End Sub

    Private Sub txtLocation__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtLocation._MYValidating
        Try
            ''Dim qry As String = "select  Location_Code as Code,Location_Desc as Name  from TSPL_LOCATION_MASTER"
            ''Dim whrcls As String = "" ''"Loc_Segment_Code in (" + clsCommon.GetMulcallString(clsERPFuncationality.UserAvailableLocationData()) + ")"
            ''txtLocation.Value = clsCommon.ShowSelectForm("PRLocation", qry, "Code", whrcls, txtLocation.Value, "Code", isButtonClicked)
            ''lblLocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" + txtLocation.Value + "'"))
            'Dim obj As clsLocation = clsLocation.FinderForPhysicalLoaction(txtLocation.Value, isButtonClicked)
            'If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Code) > 0 Then
            '    txtLocation.Value = obj.Code
            '    lblLocation.Text = obj.Name
            'Else
            '    txtLocation.Value = ""
            '    lblLocation.Text = ""
            'End If


            Dim qry As String = "select Location_Code as Code,Location_Desc as Name from TSPL_LOCATION_MASTER "
            Dim WhrCls As String = " Location_Category not in( 'MCC')  "
            If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                WhrCls += "  and  Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
            End If

            txtLocation.Value = clsCommon.ShowSelectForm("VendorLocFND", qry, "Code", WhrCls, txtLocation.Value, "Code", isButtonClicked)
            lblLocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" + txtLocation.Value + "'"))



        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub frmPurchaseRequistion_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown

        If e.KeyCode = Keys.F2 AndAlso gv1.CurrentCell IsNot Nothing Then
            isCellValueChangedOpen = True
            If gv1.CurrentColumn Is gv1.Columns(colICode) Then
                gv1.CurrentColumn = gv1.Columns(colIName)
                OpenICodeList(True)
                gv1.CurrentColumn = gv1.Columns(colICode)
            ElseIf gv1.CurrentColumn Is gv1.Columns(colVendorCode) Then
                gv1.CurrentColumn = gv1.Columns(colVendorName)
                OpenVendorList(True)
                gv1.CurrentColumn = gv1.Columns(colVendorCode)
            ElseIf gv1.CurrentColumn Is gv1.Columns(colOrderNo) Then
                gv1.CurrentColumn = gv1.Columns(colSpecification)
                OpenOrderList(True)
                gv1.CurrentColumn = gv1.Columns(colOrderNo)
            End If
            setGridFocus()
            isCellValueChangedOpen = False
        ElseIf e.Alt AndAlso e.KeyCode = Keys.N Then
            AddNew()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnSave.Enabled Then
            SaveData(False)
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag AndAlso btnPost.Enabled Then
            PostData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btnDelete.Enabled Then
            DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnClose.Enabled Then
            CloseForm()
        ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F12 Then
            'Add Tool tip Task No- TEC/22/05/18-000245
            ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction" + Environment.NewLine + _
                                    "TSPL_REQUISITION_HEAD " + Environment.NewLine + _
                                    "TSPL_REQUISITION_DETAIL " + Environment.NewLine + _
                                    "Press Alt+P for Post Trasnaction " + Environment.NewLine + _
                                    "TSPL_ITEM_REORDER_LEVEL_NEW(Auto Indent- Update Reorder Qty) ")
            'Add Tool tip Task No- TEC/22/05/18-000245
        End If
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        If txtReqNo.Value = "" Then
            myMessages.blankValue(Me, "Requisition Number", Me.Text)
        Else
            funPrint()
        End If

    End Sub

    Private Sub funPrint()
        '       Dim qry As String = "select TSPL_REQUISITION_HEAD.Requisition_Id ,TSPL_REQUISITION_HEAD.Requisition_Date ,TSPL_REQUISITION_HEAD.Expire_Date ,TSPL_REQUISITION_HEAD.Require_Date ,TSPL_REQUISITION_HEAD.Ref_No ,TSPL_REQUISITION_HEAD.Description,TSPL_REQUISITION_HEAD.Remarks ,TSPL_REQUISITION_DETAIL.Item_Code ,TSPL_REQUISITION_DETAIL.Item_Desc ,TSPL_REQUISITION_DETAIL.Unit_Code ,TSPL_REQUISITION_DETAIL.Requisition_Qty ,TSPL_VENDOR_MASTER.Vendor_Name,TSPL_REQUISITION_HEAD.Comments ,TSPL_COMPANY_MASTER.Comp_Name ,TSPL_COMPANY_MASTER.Logo_Img ,TSPL_COMPANY_MASTER.Logo_Img2  from TSPL_REQUISITION_HEAD join TSPL_REQUISITION_DETAIL on TSPL_REQUISITION_HEAD.Requisition_Id =TSPL_REQUISITION_DETAIL.Requisition_Id " & _
        '" left outer join TSPL_COMPANY_MASTER on  TSPL_REQUISITION_HEAD.Comp_Code = TSPL_COMPANY_MASTER.Comp_Code left outer join TSPL_VENDOR_MASTER on TSPL_REQUISITION_DETAIL.Vendor_Code =TSPL_VENDOR_MASTER.Vendor_Code  where  2=2 "
        Try
            '============Update by preeti Gupta Against Ticket no[BHA/22/05/18-000031]=========================
            ' Ticket No : BHA/12/06/18-000048  Other :  BHA/08/06/18-000046 (For Dairy sale Print), BHA/12/06/18-000047 (For SRN Print)
            Dim no As Integer = 0
            Dim qry As String = ""
            Dim frmCRV As New frmCrystalReportViewer()
            If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "TSDDCF") = CompairStringResult.Equal Then
                qry = "SELECT XX.*, isnull((select SUM(case when InOut='O' then Qty else (Qty*-1) end) from TSPL_INVENTORY_MOVEMENT where Item_Code=XX.Item_Code and TSPL_INVENTORY_MOVEMENT.Location_Code=XX.Location
                        AND TSPL_INVENTORY_MOVEMENT.Trans_Type='ISSTRAN'
                        AND CONVERT(DATE,TSPL_INVENTORY_MOVEMENT.SOURCE_DOC_DATE,103)>= CONVERT(DATE,XX.PreviousIndentPlaceDate,103)
                        AND CONVERT(DATE,TSPL_INVENTORY_MOVEMENT.SOURCE_DOC_DATE,103)<= CONVERT(DATE,XX.Requisition_Date,103)
                        ),0) as ConsumptionQty
                        FROM (select TSPL_LOCATION_MASTER.GSTNO as Loc_GstInNo ,tspl_company_master.cinno as Comp_CIN, case when len(TSPL_COMPANY_MASTER.Pan_No) >0 then cast (TSPL_COMPANY_MASTER.Pan_No as varchar) else '' end as PAN_NO, tspl_state_master_for_location_state.GST_STATE_Code as LOC_GST_State_Code,  TSPL_REQUISITION_DETAIL.Item_Cost ,TSPL_REQUISITION_DETAIL.Item_Net_Amt  as Amount ,TSPL_REQUISITION_HEAD.Requisition_Id ,convert(varchar,TSPL_REQUISITION_HEAD.Requisition_Date,103) as Requisition_Date , convert(varchar,TSPL_REQUISITION_HEAD.Expire_Date,103) as Expire_Date ,convert(varchar,TSPL_REQUISITION_HEAD.Require_Date,103) as Require_Date , TSPL_REQUISITION_HEAD.Ref_No ,TSPL_REQUISITION_HEAD.Description,TSPL_REQUISITION_HEAD.Remarks,TSPL_REQUISITION_HEAD.Request_By , TSPL_REQUISITION_DETAIL.Item_Code ,TSPL_REQUISITION_DETAIL.Item_Desc,TSPL_REQUISITION_DETAIL.Specification,TSPL_REQUISITION_DETAIL.Capacity,TSPL_REQUISITION_DETAIL.Make,TSPL_REQUISITION_DETAIL.Model,Category = TSPL_REQUISITION_HEAD.Category+case when TSPL_REQUISITION_HEAD.emergency>0 then ' [Emergency]' else '' end,Item_Detail= TSPL_REQUISITION_DETAIL.Item_Desc+ case when len(TSPL_REQUISITION_DETAIL.Specification)>0 then ' [Spec:'+TSPL_REQUISITION_DETAIL.Specification else '' END + case when len(TSPL_REQUISITION_DETAIL.Remarks)>0 then ', Remarks:'+TSPL_REQUISITION_DETAIL.Remarks else '' END +case when len(TSPL_REQUISITION_DETAIL.Capacity)>0 then ', Capacity:'+TSPL_REQUISITION_DETAIL.Capacity else '' END +case when len(TSPL_REQUISITION_DETAIL.Make)>0 then ', Make:'+TSPL_REQUISITION_DETAIL.Make else '' END  +case when len(TSPL_REQUISITION_DETAIL.Model)>0 then ', Model:'+TSPL_REQUISITION_DETAIL.Model else '' END + case when len(TSPL_REQUISITION_DETAIL.Specification)>0 then ']' else '' end, ItemDesc_Detail= TSPL_REQUISITION_DETAIL.Item_Desc+ case when len(TSPL_REQUISITION_DETAIL.Specification)>0 then ' [Spec:'+TSPL_REQUISITION_DETAIL.Specification else '' END + case when len(TSPL_REQUISITION_DETAIL.Remarks)>0 then ', Remarks:'+TSPL_REQUISITION_DETAIL.Remarks else '' END +case when len(TSPL_REQUISITION_DETAIL.Capacity)>0 then ', Capacity:'+TSPL_REQUISITION_DETAIL.Capacity else '' END +case when len(TSPL_REQUISITION_DETAIL.Make)>0 then ', Make:'+TSPL_REQUISITION_DETAIL.Make else '' END  +case when len(TSPL_REQUISITION_DETAIL.Model)>0 then ', Model:'+TSPL_REQUISITION_DETAIL.Model else '' END + case when len(TSPL_REQUISITION_DETAIL.Specification)>0 then ']' else '' end, TSPL_REQUISITION_DETAIL.Remarks as DRemarks ,TSPL_REQUISITION_DETAIL.Unit_Code ,TSPL_REQUISITION_DETAIL.Requisition_Qty
                        , TSPL_VENDOR_MASTER.Vendor_Name,TSPL_REQUISITION_HEAD.Comments ,TSPL_COMPANY_MASTER.Comp_Name ,TSPL_COMPANY_MASTER.Logo_Img , TSPL_COMPANY_MASTER.Logo_Img2,user1.User_Name as CreatedBy,case when TSPL_REQUISITION_HEAD.status=1 then TSPL_REQUISITION_HEAD.modify_by else '' end as AuthorizeBy
                          ,case when TSPL_REQUISITION_HEAD.status=1 then convert(varchar,TSPL_REQUISITION_HEAD.Posting_Date,103) else '' end as AuthorizeDate
                         ,TSPL_REQUISITION_HEAD.Dept_Desc,TSPL_REQUISITION_HEAD.Location , TSPL_COMPANY_MASTER.Add1,case when  is_internal ='Y' then 'MATERIAL REQUISITION' else 'PURCHASE INDENT' END AS Heading ,isnull(TSPL_ITEM_MASTER.HSN_Code,'') as HSN_Code  
                        ,convert(varchar,(select convert(varchar,max(RH.Requisition_Date),103) as Requisition_Date
                        from TSPL_REQUISITION_HEAD RH join TSPL_REQUISITION_DETAIL RD on RH.Requisition_Id =RD.Requisition_Id
                        where 
                        RH.From_Screen_Code='STORE-REQ'
                        and RD.Item_Code=TSPL_REQUISITION_DETAIL.Item_Code
                        and RH.Location=TSPL_REQUISITION_HEAD.Location
                        AND RH.Requisition_Date<TSPL_REQUISITION_HEAD.Requisition_Date
                        ),103) as PreviousIndentPlaceDate
                        ,convert(varchar,( SELECT top 1 (TOI.DOCUMENT_DATE) FROM TSPL_TRANSFER_ORDER_DETAIL TOD 
                        left outer join TSPL_TRANSFER_ORDER_HEAD TOI  on TOD.Document_No=TOI.Document_No and TOI.Transfer_Type='I'
                        left outer join TSPL_TRANSFER_ORDER_HEAD on TOI.TransferOutNo=TSPL_TRANSFER_ORDER_HEAD.Document_No
                        and TSPL_TRANSFER_ORDER_HEAD.Transfer_Type='O'
                        left outer join TSPL_TRANSFER_ORDER_DETAIL on TSPL_TRANSFER_ORDER_DETAIL.Document_No=TSPL_TRANSFER_ORDER_HEAD.Document_No
                        and TOD.Item_Code=TSPL_TRANSFER_ORDER_DETAIL.Item_Code
                        LEFT OUTER JOIN TSPL_REQUISITION_HEAD TRH ON TSPL_TRANSFER_ORDER_HEAD.Requisition_id=TRH.requisition_id
                        left outer join TSPL_REQUISITION_DETAIL TRD on TRH.Requisition_Id =TRD.Requisition_Id
                        and TRD.Item_Code=TOD.Item_Code
                        where  TRH.From_Screen_Code='STORE-REQ' AND TRH.LOCATION=TSPL_REQUISITION_HEAD.LOCATION 
                        and TSPL_REQUISITION_DETAIL.Item_Code=TRD.Item_Code 
                        AND TRH.Requisition_Date<TSPL_REQUISITION_HEAD.Requisition_Date order by TRH.Requisition_Date desc
                        ),103) AS PreviouslyReceivedDate
                        ,(SELECT top 1 (isnull(TOD.OUT_Qty,0)) FROM TSPL_TRANSFER_ORDER_DETAIL TOD 
                        left outer join TSPL_TRANSFER_ORDER_HEAD TOI  on TOD.Document_No=TOI.Document_No and TOI.Transfer_Type='I'
                        left outer join TSPL_TRANSFER_ORDER_HEAD on TOI.TransferOutNo=TSPL_TRANSFER_ORDER_HEAD.Document_No
                        and TSPL_TRANSFER_ORDER_HEAD.Transfer_Type='O'
                        left outer join TSPL_TRANSFER_ORDER_DETAIL on TSPL_TRANSFER_ORDER_DETAIL.Document_No=TSPL_TRANSFER_ORDER_HEAD.Document_No
                        and TOD.Item_Code=TSPL_TRANSFER_ORDER_DETAIL.Item_Code
                        LEFT OUTER JOIN TSPL_REQUISITION_HEAD TRH ON TSPL_TRANSFER_ORDER_HEAD.Requisition_id=TRH.requisition_id
                        left outer join TSPL_REQUISITION_DETAIL TRD on TRH.Requisition_Id =TRD.Requisition_Id
                        and TRD.Item_Code=TOD.Item_Code
                        where  TRH.From_Screen_Code='STORE-REQ' AND TRH.LOCATION=TSPL_REQUISITION_HEAD.LOCATION
                        and TSPL_REQUISITION_DETAIL.Item_Code=TRD.Item_Code 
                        AND TRH.Requisition_Date<TSPL_REQUISITION_HEAD.Requisition_Date order by TRH.Requisition_Date desc
                        ) as PreviouslyReceivedQty
                        from TSPL_REQUISITION_HEAD join TSPL_REQUISITION_DETAIL on TSPL_REQUISITION_HEAD.Requisition_Id =TSPL_REQUISITION_DETAIL.Requisition_Id left outer join TSPL_COMPANY_MASTER on  TSPL_REQUISITION_HEAD.Comp_Code = TSPL_COMPANY_MASTER.Comp_Code left outer join TSPL_VENDOR_MASTER on TSPL_REQUISITION_DETAIL.Vendor_Code =TSPL_VENDOR_MASTER.Vendor_Code left outer join TSPL_USER_MASTER as user1 on TSPL_REQUISITION_HEAD.Created_By=user1.User_Code left outer join TSPL_USER_MASTER as user2 on TSPL_REQUISITION_HEAD.Modify_By=user2.User_Code left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_REQUISITION_DETAIL.Item_Code  
                        left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER .Location_Code=  TSPL_REQUISITION_HEAD.Location  left outer join tspl_state_master as tspl_state_master_for_location_state on   tspl_state_master_for_location_state.state_code=tspl_location_master.state  where(2 = 2) and  TSPL_REQUISITION_HEAD.Requisition_Id='" + txtReqNo.Value + "'
                        )XX "
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                frmCRV.funreport(CrystalReportFolder.PurchaseOrder, dt, "PurchaseRequisitionForVijaya", "Store Requisition", clsCommon.myCDate(dt.Rows(0)("Requisition_Date")))
            Else
                qry = "select TSPL_REQUISITION_HEAD.Requisition_Id ,convert(varchar,TSPL_REQUISITION_HEAD.Requisition_Date,103) as Requisition_Date , " &
            "convert(varchar,TSPL_REQUISITION_HEAD.Expire_Date,103) as Expire_Date ,convert(varchar,TSPL_REQUISITION_HEAD.Require_Date,103) as Require_Date , " &
            "TSPL_REQUISITION_HEAD.Ref_No ,TSPL_REQUISITION_HEAD.Description,TSPL_REQUISITION_HEAD.Remarks,TSPL_REQUISITION_HEAD.Request_By, isnull(TSPL_EMPLOYEE_MASTER.Emp_Name,'') as  Request_By_Name , " &
            "TSPL_REQUISITION_DETAIL.Item_Code ,TSPL_REQUISITION_DETAIL.Item_Desc as Item_Detail,TSPL_REQUISITION_DETAIL.Specification, " &
            "TSPL_REQUISITION_DETAIL.Remarks as DRemarks ,TSPL_REQUISITION_DETAIL.Unit_Code ,TSPL_REQUISITION_DETAIL.Requisition_Qty, " &
            "isnull((select SUM( case when InOut='I' then Qty else  -1* Qty end )from TSPL_INVENTORY_MOVEMENT where Item_Code=TSPL_REQUISITION_DETAIL.Item_Code and TSPL_INVENTORY_MOVEMENT.Location_Code=TSPL_REQUISITION_HEAD.Location),0) as AvaiQty  ,TSPL_REQUISITION_DETAIL.Item_Net_Amt as Amount,TSPL_REQUISITION_DETAIL.Item_Cost,TSPL_COMPANY_MASTER.Phone1,TSPL_COMPANY_MASTER.Email,TSPL_COMPANY_MASTER.GSTReg_No, " &
            "TSPL_VENDOR_MASTER.Vendor_Name,TSPL_REQUISITION_HEAD.Comments ,TSPL_COMPANY_MASTER.Comp_Name ,TSPL_COMPANY_MASTER.Logo_Img , " &
            "TSPL_COMPANY_MASTER.Logo_Img2,user1.User_Name as CreatedBy,'' as AuthorizeBy ,TSPL_REQUISITION_HEAD.Request_By, " &
            "TSPL_REQUISITION_HEAD.Require_Date,TSPL_REQUISITION_HEAD.Dept_Desc,TSPL_REQUISITION_HEAD.Location , " &
            "TSPL_COMPANY_MASTER.Add1,case when  is_internal ='Y' then 'MATERIAL REQUISITION' else 'PURCHASE INDENT' END AS Heading ,TSPL_ITEM_MASTER.HSN_Code  " &
            ",TSPL_REQUISITION_HEAD.Capex_Code,TSPL_REQUISITION_HEAD.Capex_SubCode" &
            ",isnull(SubCapex_Amount,0) as SubCapex_Amount,isnull(SubCapex_AmountWithTol,0) as SubCapex_AmountWithTol,isnull(SubCapex_BalAmount,0) as SubCapex_BalAmount,isnull(SubCapex_BalAmountWithTol,0) as SubCapex_BalAmountWithTol,PendQty.PendingQty " &
            " from TSPL_REQUISITION_HEAD join TSPL_REQUISITION_DETAIL on TSPL_REQUISITION_HEAD.Requisition_Id =TSPL_REQUISITION_DETAIL.Requisition_Id  Left Outer Join TSPL_EMPLOYEE_MASTER On TSPL_EMPLOYEE_MASTER.EMP_CODE = TSPL_REQUISITION_HEAD.Request_By left outer join TSPL_COMPANY_MASTER on  TSPL_REQUISITION_HEAD.Comp_Code = TSPL_COMPANY_MASTER.Comp_Code left outer join TSPL_VENDOR_MASTER on TSPL_REQUISITION_DETAIL.Vendor_Code =TSPL_VENDOR_MASTER.Vendor_Code left outer join TSPL_USER_MASTER as user1 on TSPL_REQUISITION_HEAD.Created_By=user1.User_Code left outer join TSPL_USER_MASTER as user2 on TSPL_REQUISITION_HEAD.Modify_By=user2.User_Code  left outer join tspl_item_master on TSPL_ITEM_MASTER.Item_Code=TSPL_REQUISITION_DETAIL.Item_Code  " &
            " left join ( select Item_Code,Unit_code,Bill_To_Location,sum(qty*(RI))" &
            " as PendingQty  from (" &
            " Select 'PO' AS TransType,  TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No, TSPL_PURCHASE_ORDER_DETAIL.Item_Code  As Item_Code, TSPL_PURCHASE_ORDER_DETAIL.Location, " &
                    " PurchaseOrder_Qty  as Qty, 1 as RI, TSPL_PURCHASE_ORDER_DETAIL.Unit_code,TSPL_PURCHASE_ORDER_HEAD.Bill_To_Location from   TSPL_PURCHASE_ORDER_DETAIL " &
                    " LEFT OUTER JOIN TSPL_PURCHASE_ORDER_HEAD  ON TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No=TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No " &
                    " WHERE TSPL_PURCHASE_ORDER_HEAD.Status=1 " &
                      "  union all " &
                     " Select 'SRN' As TransType,TSPL_SRN_HEAD.SRN_No  AS DocNo,TSPL_SRN_DETAIL .Item_Code As ICODE,TSPL_SRN_HEAD.Bill_To_Location as Location, TSPL_SRN_DETAIL.SRN_Qty  As Qty,-1 as RI,TSPL_SRN_DETAIL.Unit_code As UnitCode,TSPL_SRN_HEAD.Bill_To_Location from TSPL_SRN_HEAD Left Outer Join TSPL_SRN_DETAIL on TSPL_SRN_HEAD.SRN_No=TSPL_SRN_DETAIL.SRN_No where TSPL_SRN_HEAD.Status=1" &
                    " ) as pp " &
                    "  group by Item_Code,Unit_code,Bill_To_Location) as PendQty on PendQty.Item_Code =TSPL_REQUISITION_DETAIL.Item_Code and PendQty.Bill_To_Location =TSPL_REQUISITION_HEAD.Location and PendQty.Unit_code =TSPL_REQUISITION_DETAIL.Unit_Code " &
                    " where(2 = 2)"

                If txtReqNo.Value <> "" Then
                qry += " and  TSPL_REQUISITION_HEAD.Requisition_Id='" + txtReqNo.Value + "'"
            End If
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

            For i As Integer = 0 To dt.Rows.Count - 1
                If dt.Rows(i)("vendor_name").ToString() <> "" Then
                    no = no + 1
                End If
            Next

                If no = 0 Then
                    If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "GUNTUR") = CompairStringResult.Equal Then
                        frmCRV.funreport(CrystalReportFolder.PurchaseOrder, dt, "PurchaseRequisitionWithoutVendor-G", "Purchase Requisition")
                    Else
                        If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "TSDDCF") = CompairStringResult.Equal Then
                            frmCRV.funreport(CrystalReportFolder.PurchaseOrder, dt, "StoreRequisitionWithoutVendor", "Store Requisition", clsCommon.myCDate(dt.Rows(0)("Requisition_Date")))
                        Else
                            frmCRV.funreport(CrystalReportFolder.PurchaseOrder, dt, "PurchaseRequisitionWithoutVendor", "Purchase Requisition", clsCommon.myCDate(dt.Rows(0)("Requisition_Date")))
                        End If
                    End If

                Else
                    If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "GUNTUR") = CompairStringResult.Equal Then
                        frmCRV.funreport(CrystalReportFolder.PurchaseOrder, dt, "PurchaseRequisition-G", "Purchase Requisition")
                    Else
                        frmCRV.funreport(CrystalReportFolder.PurchaseOrder, dt, "PurchaseRequisition", "Purchase Requisition", clsCommon.myCDate(dt.Rows(0)("Requisition_Date")))
                    End If
                End If
            End If
            frmCRV = Nothing


        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub gv1_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gv1.DoubleClick
        Try
            ''If clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentCell.ColumnInfo.Name), colComplete) = CompairStringResult.Equal AndAlso UsLock1.Status = ERPTransactionStatus.Approved Then
            If gv1.Columns(colIName) Is gv1.CurrentColumn AndAlso UsLock1.Status = ERPTransactionStatus.Approved Then
                Dim strICode As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value)
                Dim intSNo As Integer = Convert.ToInt32((clsCommon.myCdbl(gv1.CurrentRow.Cells(colLineNo).Value)))
                Dim strStatus As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colComplete).Value)
                If clsCommon.myLen(txtReqNo.Value) > 0 AndAlso clsCommon.myLen(strICode) > 0 AndAlso intSNo > 0 AndAlso clsCommon.CompairString(strStatus, "N") = CompairStringResult.Equal Then
                    If common.clsCommon.MyMessageBoxShow("Do you want to complete the item " + clsCommon.myCstr(gv1.CurrentRow.Cells(colIName).Value), Me.Text, MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
                        If clsRequistionDetail.CompleteRequition(txtReqNo.Value, strICode, intSNo) Then
                            common.clsCommon.MyMessageBoxShow(Me, "Successfully Completed", Me.Text)
                            LoadData(txtReqNo.Value, NavigatorType.Current)
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub gv1_UserDeletingRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs) Handles gv1.UserDeletingRow
        If common.clsCommon.MyMessageBoxShow("Delete The Current Row." + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.No Then
            e.Cancel = True
        End If
    End Sub

    Private Sub gv1_UserDeletedRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowEventArgs) Handles gv1.UserDeletedRow
        UpdateAllTotals()
        For ii As Integer = 1 To gv1.Rows.Count
            gv1.Rows(ii - 1).Cells(colLineNo).Value = ii
        Next
    End Sub

    Private Sub txtDept__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtDept._MYValidating
        Try

            Dim obj As clsDepartment = clsDepartment.Finder(txtDept.Value, isButtonClicked)
            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Code) > 0 Then
                txtDept.Value = obj.Code
                lblDept.Text = obj.Name
                ''=======For Blank all Cost Centre value in Department Changed============
                'txtUnitCode.Value = ""
                'lblUnitDesc.Text = ""
                'txtCostCenterType.Value = ""
                'lblCostcenterTypeDesc.Text = ""
                ''===============================
            Else
                txtDept.Value = ""
                lblDept.Text = ""
            End If

            'If EnableStoreCostCentre = 1 And clsCommon.myLen(txtDept.Value) > 0 Then

            '    txtUnitCode.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select unit_code from TSPL_COST_CENTER_TYPE_MASTER where Department='" + txtDept.Value + "'"))

            '    txtCostCenterType.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select cost_code from TSPL_COST_CENTER_TYPE_MASTER where Department='" + txtDept.Value + "'"))

            '    If clsCommon.myLen(txtUnitCode.Value) > 0 Then
            '        lblUnitDesc.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description from TSPL_COST_CENTER_UNIT_MASTER where Code='" + txtUnitCode.Value + "'"))
            '    Else
            '        lblUnitDesc.Text = ""
            '    End If

            '    If clsCommon.myLen(txtCostCenterType.Value) > 0 Then
            '        lblCostcenterTypeDesc.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Cost_name from TSPL_CostCenter_MASTER where Cost_Code='" + txtCostCenterType.Value + "'"))
            '    Else
            '        lblCostcenterTypeDesc.Text = ""
            '    End If
            '    lblUnitDesc.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description from TSPL_COST_CENTER_UNIT_MASTER where Code='" + txtUnitCode.Value + "'"))

            '    If gv1.Rows.Count > 0 Then
            '        For ii As Integer = 0 To gv1.Rows.Count - 1
            '            gv1.Rows(ii).Cells(colCostCenterCode).Value = clsCommon.myCstr(txtCostCenterType.Value)
            '        Next
            '    End If
            'End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub gv1_CellFormatting(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CellFormattingEventArgs) Handles gv1.CellFormatting
        Try
            'If (e.RowIndex = gv1.CurrentRow.Index AndAlso e.ColumnIndex = gv1.CurrentColumn.Index) Then
            '    Dim cell As GridDataCellElement = TryCast(e.CellElement, GridDataCellElement)
            '    cell.GradientStyle = GradientStyles.Solid
            '    cell.BackColor = Color.FromArgb(243, 181, 51)
            'End If

            ''Dim columnIndex As Integer = e.CellElement.ColumnIndex
            ''Dim rowIndex As Integer = e.CellElement.RowIndex
            ''Dim cell As GridCellElement = e.CellElement
            ''If rowIndex = rowBeginIndex - 1 Then
            ''    If columnIndex = totalColumnIndex OrElse columnIndex = comissionColumnIndex OrElse columnIndex = feeColumnIndex OrElse columnIndex = clientColumnIndex Then

            ''        If cell.IsSelected Then
            ''            cell.BackColor = Color.FromArgb(162, 207, 223)
            ''        End If
            ''    End If
            ''ElseIf rowIndex > rowBeginIndex - 1 AndAlso rowIndex < rowBeginIndex + 8 Then
            ''    If columnIndex = totalColumnIndex OrElse columnIndex = comissionColumnIndex OrElse columnIndex = feeColumnIndex OrElse columnIndex = clientColumnIndex Then
            ''        If cell.IsSelected Then
            ''            cell.BackColor = Color.FromArgb(228, 227, 216)
            ''        End If
            ''    End If
            ''End If
            ''If IsNumber(e.CellElement.Value) Then
            ''    e.CellElement.Alignment = ContentAlignment.TopRight
            ''Else
            ''    e.CellElement.Alignment = ContentAlignment.TopLeft
            ''End If
        Catch ex As Exception
            'common.clsCommon.MyMessageBoxShow(me,ex.Message,me.text)
        End Try
    End Sub

    Private Sub gv1_RowsChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCollectionChangedEventArgs) Handles gv1.RowsChanged


    End Sub

    Private Sub gv1_CurrentRowChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CurrentRowChangedEventArgs) Handles gv1.CurrentRowChanged
        If gv1.CurrentRow IsNot Nothing AndAlso Not e.CurrentRow.Index < 0 Then
            setBalance()
        End If
    End Sub

    Private Sub gv1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gv1.Click
        If gv1.CurrentRow IsNot Nothing Then
            setBalance()
        End If
    End Sub
    Sub setBalance()
        If clsCommon.CompairString(objCommonVar.CurrComp_Code1, "JPR") = CompairStringResult.Equal Then
            'UcItemBalance1.ItemCode = clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value)
            'UcItemBalance1.ItemName = clsCommon.myCstr(gv1.CurrentRow.Cells(colIName).Value)
            'UcItemBalance1.ItemMRP = -1 ' clsCommon.myCdbl(gv1.CurrentRow.Cells(colMRP).Value)
            'UcItemBalance1.LocationCode = txtLocation.Value
            'UcItemBalance1.LocationName = lblLocation.Text
            'UcItemBalance1.UOM = clsCommon.myCstr(gv1.CurrentRow.Cells(colUnit).Value)
            'UcItemBalance1.TransNo = txtReqNo.Value
            'UcItemBalance1.TransDate = txtDate.Value
            'UcItemBalance1.ShowPOQty = True
            'UcItemBalance1.RefreshData()
            UcItemBalance1.Visible = False
        Else
            UcItemBalance1.ItemCode = clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value)
            UcItemBalance1.ItemName = clsCommon.myCstr(gv1.CurrentRow.Cells(colIName).Value)
            UcItemBalance1.ItemMRP = -1 ' clsCommon.myCdbl(gv1.CurrentRow.Cells(colMRP).Value)
            UcItemBalance1.LocationCode = txtLocation.Value
            UcItemBalance1.LocationName = lblLocation.Text
            UcItemBalance1.UOM = clsCommon.myCstr(gv1.CurrentRow.Cells(colUnit).Value)
            UcItemBalance1.TransNo = txtReqNo.Value
            UcItemBalance1.TransDate = txtDate.Value
            UcItemBalance1.ShowPOQty = True
            UcItemBalance1.RefreshData()
        End If
    End Sub

    'If no = 0 Then
    '                  strRptPath = frmCRV.funreport1(CrystalReportFolder.PurchaseOrder, dt1, "PurchaseRequisitionWithoutVendor", "Purchase Requisition")
    '              Else
    '                  strRptPath = frmCRV.funreport1(CrystalReportFolder.PurchaseOrder, dt1, "PurchaseRequisition", "Purchase Requisition")
    '              End If

    Private Sub gv1_CurrentColumnChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gv1.CurrentColumnChanged
        If gv1.RowCount > 0 Then

            Dim intCurrRow As Integer = gv1.CurrentRow.Index
            gv1.CurrentRow.Cells(colLineNo).Value = clsCommon.myCdbl(intCurrRow + 1)
            If intCurrRow = gv1.Rows.Count - 1 Then
                gv1.Rows.AddNew()
                gv1.CurrentRow = gv1.Rows(intCurrRow)
            End If
        End If
    End Sub



    Private Sub btnUnpost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUnpost.Click
        Try
            If clsCommon.myLen(txtReqNo.Value) > 0 Then
                Dim qry As String = ""
                'Dim qry As String = "select 1 from TSPL_REQUISITION_HEAD where Requisition_Id='" + txtReqNo.Value + "' and Status=1"
                'Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                'If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                '    Throw New Exception("Transaction status should be posted.")
                'End If
                'qry = "select distinct PurchaseOrder_No from TSPL_PURCHASE_ORDER_DETAIL where Requisition_Id='" + txtReqNo.Value + "'"
                'dt = New DataTable()
                'dt = clsDBFuncationality.GetDataTable(qry)
                'If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                '    qry = "Purchase Requisition used in following PO"
                '    For Each dr As DataRow In dt.Rows
                '        qry += Environment.NewLine + clsCommon.myCstr(dr("PurchaseOrder_No"))
                '    Next
                '    qry += Environment.NewLine + "Can't unpost it"
                '    Throw New Exception(qry)
                'End If
                If clsCommon.MyMessageBoxShow(Me, "Unpost the current transaction" + Environment.NewLine + "Are you sure", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = Windows.Forms.DialogResult.Yes Then

                    'qry = "update TSPL_REQUISITION_HEAD set Status=0,Posting_Date=null where Requisition_Id='" + txtReqNo.Value + "'"
                    'clsDBFuncationality.ExecuteNonQuery(qry)
                    clsRequistionHead.ReverseAndUnpost(txtReqNo.Value)
                    clsCommon.MyMessageBoxShow(Me, "Tansaction unposted succesffuly", Me.Text)
                    LoadData(txtReqNo.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    'Public Sub SendMail()
    '    Try
    '        If Not objCommonVar.IsMailSend Then
    '            Exit Sub
    '        End If

    '        Dim ArrReceivers As New List(Of String)
    '        Dim ArrUsers As New List(Of String)
    '        Dim no As Integer = 0
    '        Dim qry As String = "select TSPL_REQUISITION_HEAD.Requisition_Id ,TSPL_REQUISITION_HEAD.Requisition_Date ,TSPL_REQUISITION_HEAD.Expire_Date ,TSPL_REQUISITION_HEAD.Require_Date ,TSPL_REQUISITION_HEAD.Ref_No ,TSPL_REQUISITION_HEAD.Description,TSPL_REQUISITION_HEAD.Remarks,TSPL_REQUISITION_HEAD.Request_By ,TSPL_REQUISITION_DETAIL.Item_Code ,TSPL_REQUISITION_DETAIL.Item_Desc ,TSPL_REQUISITION_DETAIL.Unit_Code ,TSPL_REQUISITION_DETAIL.Requisition_Qty,(select SUM(Item_Qty)from TSPL_ITEM_LOCATION_DETAILS where Item_Code=TSPL_REQUISITION_DETAIL.Item_Code)as AvaiQty  ,TSPL_VENDOR_MASTER.Vendor_Name,TSPL_REQUISITION_HEAD.Comments ,TSPL_COMPANY_MASTER.Comp_Name ,TSPL_COMPANY_MASTER.Logo_Img ,TSPL_COMPANY_MASTER.Logo_Img2,user1.User_Name as CreatedBy,TSPL_REQUISITION_HEAD.Approvel_Level_Required"
    '        qry += " , case when TSPL_REQUISITION_HEAD.Status =1 then  user2.User_Name else '' end  as AuthorizeBy ,"
    '        qry += " TSPL_REQUISITION_HEAD.Request_By,TSPL_REQUISITION_HEAD.Require_Date,TSPL_REQUISITION_HEAD.Dept_Desc,TSPL_REQUISITION_HEAD.Location ,TSPL_COMPANY_MASTER.Add1  from TSPL_REQUISITION_HEAD join TSPL_REQUISITION_DETAIL on TSPL_REQUISITION_HEAD.Requisition_Id =TSPL_REQUISITION_DETAIL.Requisition_Id left outer join TSPL_COMPANY_MASTER on  TSPL_REQUISITION_HEAD.Comp_Code = TSPL_COMPANY_MASTER.Comp_Code left outer join TSPL_VENDOR_MASTER on TSPL_REQUISITION_DETAIL.Vendor_Code =TSPL_VENDOR_MASTER.Vendor_Code left outer join TSPL_USER_MASTER as user1 on TSPL_REQUISITION_HEAD.Created_By=user1.User_Code left outer join TSPL_USER_MASTER as user2 on TSPL_REQUISITION_HEAD.Modify_By=user2.User_Code   where(2 = 2)"
    '        qry += " and  TSPL_REQUISITION_HEAD.Requisition_Id='" + txtReqNo.Value + "'"


    '        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
    '        Dim ReqNo As String = dt.Rows(0)("Requisition_Id").ToString
    '        Dim Reqdate As String = dt.Rows(0)("Requisition_Date").ToString
    '        For i As Integer = 0 To dt.Rows.Count - 1
    '            If dt.Rows(i)("vendor_name").ToString() <> "" Then
    '                no = no + 1
    '            End If
    '        Next
    '        If dt.Rows(0)("Approvel_Level_Required").ToString <> "" Then

    '            Dim level As String = dt.Rows(0)("Approvel_Level_Required").ToString
    '            Dim Query As String = "select E_mail,user_code from TSPL_USER_MASTER  where level <=" + level + " "
    '            Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(Query)
    '            For Each dr As DataRow In dt1.Rows
    '                If clsCommon.myLen(dr("E_mail")) > 0 Then
    '                    ArrReceivers.Add(clsCommon.myCstr(dr("E_mail")))
    '                    ArrUsers.Add(clsCommon.myCstr(dr("user_code")))
    '                End If
    '            Next

    '        End If

    '        Dim strreportPath As String = ""
    '        'If no = 0 Then
    '        Dim frmCRV As New frmCrystalReportViewer()
    '        strreportPath = frmCRV.funreport1(CrystalReportFolder.PurchaseOrder, dt, "PurchaseRequisition", "Purchase Requisition")
    '        frmCRV = Nothing
    '        'Else
    '        '    strreportPath = PurchaseOrderViewer.funreport1(dt, "PurchaseRequisition", "Purchase Requisition")
    '        'End If
    '        'ArrReceivers.Add("Shipra.jain@tecxpert.in")
    '        If ArrReceivers.Count > 0 Then
    '            sendEMailThroughOUTLOOK(strreportPath, ArrReceivers, ArrUsers, ReqNo, Reqdate)
    '            clsCommon.MyMessageBoxShow("Mail has been sent succcessfully.")
    '        Else
    '            Throw New Exception("No Recipients found to mail.")
    '        End If
    '        'Else
    '        '    Throw New Exception("No report found to mail.")
    '        'End If

    '    Catch ex As Exception
    '        clsCommon.MyMessageBoxShow(me,ex.Message,me.text)
    '    End Try
    'End Sub
    'Public Sub sendEMailThroughOUTLOOK(ByVal strPath As String, ByVal arrReceivers As List(Of String), ByVal arrUsers As List(Of String), ByVal ReqNo As String, ByVal ReqDate As String)
    '    Try

    '        If Process.GetProcessesByName("OutLook").Length < 1 Then
    '            'restarts the Process
    '            Process.Start("OutLook.exe")
    '        End If
    '        Dim oApp As New Outlook.Application()
    '        Dim oMsg As Outlook.MailItem = DirectCast(oApp.CreateItem(Outlook.OlItemType.olMailItem), Outlook.MailItem)





    '        'oMsg.HTMLBody = "Please Find Attachment"
    '        'oMsg.HTMLBody = obj.EMail_Text 
    '        Dim sDisplayName As [String] = "MyAttachment"
    '        oMsg.Subject = "Approval required for Requisition No:" + ReqNo + " dated :" + clsCommon.GetPrintDate(ReqDate, "dd/MMM/yyyy") + ""

    '        oMsg.Body = "Please find the attached Requisition No:" + ReqNo + " dated :" + clsCommon.GetPrintDate(ReqDate, "dd/MMM/yyyy") + " for your kind approval." & vbCrLf & "" & vbCrLf & "Best Regards" & vbCrLf & "" + objCommonVar.CurrentUser + ""


    '        Dim iPosition As Integer = CInt(oMsg.Body.Length) + 1
    '        Dim iAttachType As Integer = CInt(Outlook.OlAttachmentType.olByValue)

    '        If clsCommon.myLen(strPath) > 0 Then
    '            Dim oAttach As Outlook.Attachment = oMsg.Attachments.Add(strPath, iAttachType, iPosition, sDisplayName)
    '        End If


    '        'oMsg.Subject = "Minutes of Meeting"


    '        'arrReceivers.Add("Rakesh.sharma@tecxpert.in")
    '        For ii As Integer = 0 To arrReceivers.Count - 1
    '            oMsg.Recipients.Add(arrReceivers(ii))
    '        Next
    '        'oMsg.Recipients.Add("shipra.jain@tecxpert.in")
    '        oMsg.Send()
    '        oMsg = Nothing
    '        oApp = Nothing
    '    Catch ex As Exception
    '        Throw New Exception(ex.Message)
    '    End Try
    'End Sub

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub


    Private Sub fndProject__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndProject._MYValidating, TxtFinder2._MYValidating
        Dim qry As String = "select PROJECT_CODE as Code,SPECIFICATION,PROJECT_STATUS as Status from TSPL_PJC_PROJECT"
        fndProject.Value = clsCommon.ShowSelectForm("Project Code", qry, "Code", "", fndProject.Value, "", isButtonClicked)
        lblProject.Text = clsDBFuncationality.getSingleValue("select SPECIFICATION from TSPL_PJC_PROJECT where PROJECT_CODE='" + fndProject.Value + "'")
    End Sub
    Sub PRCLOSEDATA()
        Try
            If (clsRequistionHead.CloseprData(txtReqNo.Value, closeyn)) Then
                If closeyn = "Y" Then
                    common.clsCommon.MyMessageBoxShow(Me, "Data Closed Successfully ", Me.Text)
                ElseIf closeyn = "N" Then
                    common.clsCommon.MyMessageBoxShow(Me, "Data Opened Successfully ", Me.Text)
                End If
                LoadData(txtReqNo.Value, NavigatorType.Current)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub chkprclose_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkprclose.CheckedChanged
        If chkprclose.Checked = True And vaddnew = "N" Then
            Dim response = MsgBox("Are you sure want to close Store Requisition?", MsgBoxStyle.YesNo, "Attention")
            If response = MsgBoxResult.Yes Then
                closeyn = "Y"
                PRCLOSEDATA()
            ElseIf response = MsgBoxResult.No Then
                closeyn = "N"
                chkprclose.Checked = False
            End If
        ElseIf chkprclose.Checked = False And vaddnew = "N" Then
            closeyn = "N"
            PRCLOSEDATA()
        End If
    End Sub

    Private Sub SaveLayout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveLayoutbtn.Click
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
                common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
            End If
            ''stuti regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub DeleteLayout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteLayout.Click
        clsGridLayout.DeleteData(MyBase.Form_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow("Layout Delete successfully", "Information")
    End Sub

    Private Sub RadPageView1_SelectedPageChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadPageView1.SelectedPageChanged

    End Sub

    '---------------------Richa  10/07/2014 Ticket no BM00000003042-------------for Mail Setting----------------------------
    Private Sub LoadItems()
        Try
            isInsideLoadData = True
            If ArrItem IsNot Nothing Then
                cboItemType.SelectedValue = "O"
                For Each obj As clsItemMaster In ArrItem

                    gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value = obj.Item_Code
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colIName).Value = obj.Item_Desc
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colUnit).Value = obj.Unit_Code
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value = obj.RemainingQtyToPurchase
                    'gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value = obj.co
                    gv1.Rows.AddNew()
                Next
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isInsideLoadData = False
        End Try
    End Sub
    Private Sub RdEmailAndSmsSetting_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RdEmailAndSmsSetting.Click
        Dim frm As New FrmMailSMSSettingNew2()
        frm.FormId = clsUserMgtCode.mbtnPurchaseRequistion
        frm.ShowDialog()
    End Sub



    Private Sub SendSMSandEmail(ByVal lstUsers As List(Of String), ByVal isSendForApproval As Boolean)
        Try
            'Dim obj As clsEmailSMSSettingNew = clsEmailSMSSettingNew.GetData(clsUserMgtCode.mbtnPurchaseRequistion)

            'If obj Is Nothing Then
            '    clsCommon.MyMessageBoxShow("First do email and sms setting", Me.Text)
            '    Return
            'End If
            'If clsCommon.myLen(obj.mailsubjct) <= 0 Then
            '    clsCommon.MyMessageBoxShow("First do email and sms setting", Me.Text)
            '    Return
            'End If

            'Dim strContactPerson As String = ""
            'Dim strSubject As String = obj.mailsubjct.Replace(clsEmailSMSConstants.PurchaseRequisitionNo, txtReqNo.Value)
            'strSubject = strSubject.Replace(clsEmailSMSConstants.PurchaseRequisitionDate, clsCommon.GetPrintDate(txtDate.Text, "dd/MMM/yyyy"))

            'Dim strbody As String = obj.mailbody.Replace(clsEmailSMSConstants.PurchaseRequisitionNo, txtReqNo.Value)
            'strbody = strbody.Replace(clsEmailSMSConstants.PurchaseRequisitionDate, clsCommon.GetPrintDate(txtDate.Text, "dd/MMM/yyyy"))
            'strbody = strbody.Replace(clsEmailSMSConstants.TotalAmount, lblTotRAmt.Text)
            'strbody = strbody.Replace(clsEmailSMSConstants.Form_Code, MyBase.Form_ID)

            ''------------------------code for attchament-------------------------------------
            'Dim no As Integer = 0
            'Dim strRptPath As String = ""
            'If obj.atchmnt = "Y" Then
            '    atchqry = GetMailPrintPreview(txtReqNo.Value)
            '    Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(atchqry)
            '    For i As Integer = 0 To dt1.Rows.Count - 1
            '        If dt1.Rows(i)("vendor_name").ToString() <> "" Then
            '            no = no + 1
            '        End If
            '    Next
            '    Dim frmCRV As New frmCrystalReportViewer()
            '    If no = 0 Then
            '        strRptPath = frmCRV.funreport1(CrystalReportFolder.PurchaseOrder, dt1, "PurchaseRequisitionWithoutVendor", "Purchase Requisition")
            '    Else
            '        strRptPath = frmCRV.funreport1(CrystalReportFolder.PurchaseOrder, dt1, "PurchaseRequisition", "Purchase Requisition")
            '    End If
            '    frmCRV = Nothing
            'End If
            ''---------------------------------------------------------------------------

            'Dim lstReceiptents As New List(Of String)
            'For Each strUser As String In lstUsers
            '    'lstUsers.Add(dr("User_Code").ToString())
            '    Dim qry As String = ""
            '    Dim emailId As String = ""
            '    If isSendForApproval Then
            '        'strContactPerson = strUser
            '        'qry = "select E_Mail from TSPL_USER_MASTER where User_Code in (Select User_Code from TSPL_APPROVAL_LEVEL_SCREEN where Module_Code ='MPurchase' and TRANS_Code='PO-REQ') "
            '        'emailId = clsDBFuncationality.getSingleValue(qry)
            '    Else

            '        Dim dt As DataTable
            '        qry = "Select isnull(E_Mail,'') As Email  from TSPL_USER_MASTER where User_Code  in (Select User_Code from TSPL_APPROVAL_LEVEL_SCREEN where Module_Code ='MPurchase' and TRANS_Code='PO-REQ')"
            '        dt = clsDBFuncationality.GetDataTable(qry)
            '        If dt.Rows.Count > 0 Then
            '            For Each dr As DataRow In dt.Rows
            '                lstReceiptents.Add(clsCommon.myCstr(dr("Email")))

            '            Next
            '        End If

            '    End If

            '    strbody = strbody.Replace(clsEmailSMSConstants.ContactPerson, strContactPerson)


            '    Dim body As String = strbody.Replace(clsEmailSMSConstants.UserCode, strUser)

            '    clsMailViaOutlook.SendEmail(strSubject, body, lstReceiptents, Nothing, strRptPath)
            'Next
            'clsCommon.MyMessageBoxShow("E-Mail Send Successfully", Me.Text)


            'sanjay
            Dim strContactPerson As String = ""

            Dim dtContent As DataTable = clsDBFuncationality.GetDataTable("SELECT SMS_Text,Email_Text,Email_subject from TSPL_ES_Content where Form_ID='" + clsUserMgtCode.frmStoreRequistion + "'", Nothing)
            Dim objEmailH As New clsEMailHead()
            objEmailH.arrEMail = New List(Of String)()

            If dtContent IsNot Nothing AndAlso dtContent.Rows.Count > 0 Then

                If clsCommon.myLen(dtContent.Rows(0)("Email_Text")) > 0 Then
                    objEmailH.Email_Subject = clsCommon.myCstr(dtContent.Rows(0)("Email_subject"))
                    objEmailH.Email_Subject = objEmailH.Email_Subject.Replace(frmEMailAndSMSSetting.PurchaseRequisitionNo, txtReqNo.Value)
                    objEmailH.Email_Subject = objEmailH.Email_Subject.Replace(frmEMailAndSMSSetting.PurchaseRequisitionDate, clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy"))

                    objEmailH.Email_Text = clsCommon.myCstr(dtContent.Rows(0)("Email_Text"))
                    objEmailH.Email_Text = objEmailH.Email_Text.Replace(frmEMailAndSMSSetting.PurchaseRequisitionNo, txtReqNo.Value)
                    objEmailH.Email_Text = objEmailH.Email_Text.Replace(frmEMailAndSMSSetting.PurchaseRequisitionDate, clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy"))
                    objEmailH.Email_Text = objEmailH.Email_Text.Replace(frmEMailAndSMSSetting.TotalAmount, clsCommon.myCstr(lblTotRAmt.Text))
                    objEmailH.Email_Text = objEmailH.Email_Text.Replace(frmEMailAndSMSSetting.Form_Code, clsUserMgtCode.frmStoreRequistion)

                    '------------------------code for attchament-------------------------------------
                    Dim no As Integer = 0
                    Dim strRptPath As String = ""
                    'If obj.atchmnt = "Y" Then
                    atchqry = GetMailPrintPreview(txtReqNo.Value)
                    Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(atchqry)
                    For i As Integer = 0 To dt1.Rows.Count - 1
                        If dt1.Rows(i)("vendor_name").ToString() <> "" Then
                            no = no + 1
                        End If
                    Next
                    Dim frmCRV As New frmCrystalReportViewer()
                    If no = 0 Then
                        strRptPath = frmCRV.funreport1(CrystalReportFolder.PurchaseOrder, dt1, "PurchaseRequisitionWithoutVendor", "Purchase Requisition")
                    Else
                        strRptPath = frmCRV.funreport1(CrystalReportFolder.PurchaseOrder, dt1, "PurchaseRequisition", "Purchase Requisition")
                    End If
                    objEmailH.Attachment_1_Path = strRptPath
                    frmCRV = Nothing
                    'End If
                    '---------------------------------------------------------------------------

                    For Each strUser As String In lstUsers
                        'lstUsers.Add(dr("User_Code").ToString())
                        Dim lstReceiptents As New List(Of String)
                        Dim qry As String = ""
                        Dim emailId As String = ""
                        If isSendForApproval Then
                            strContactPerson = strUser
                            qry = "select E_Mail from TSPL_USER_MASTER where User_Code in ('" + strUser + "') "
                            emailId = clsDBFuncationality.getSingleValue(qry)

                            lstReceiptents.Add(emailId)
                            objEmailH.arrEMail.Add(emailId)

                        Else
                            strContactPerson = clsDBFuncationality.getSingleValue("select upper(substring(Contact_Person_Name,1,1)) + lower(substring(Contact_Person_Name,2,49)) from TSPL_VENDOR_MASTER where vendor_code ='" & strUser & "' ")
                            emailId = clsDBFuncationality.getSingleValue("select Email from TSPL_VENDOR_MASTER where vendor_code ='" & strUser & "' ")


                            Dim dt As DataTable
                            qry = "Select isnull(E_Mail,'') As Email  from TSPL_USER_MASTER where User_Code  in (Select User_Code from TSPL_APPROVAL_LEVEL_SCREEN where Module_Code ='MPurchase' and TRANS_Code='STORE-REQ')"
                            dt = clsDBFuncationality.GetDataTable(qry)
                            If dt.Rows.Count > 0 Then
                                For Each dr As DataRow In dt.Rows
                                    lstReceiptents.Add(clsCommon.myCstr(dr("Email")))
                                    objEmailH.arrEMail.Add(clsCommon.myCstr(dr("Email")))
                                Next
                            End If

                        End If

                    Next


                    objEmailH.SaveData(clsUserMgtCode.frmStoreRequistion, objEmailH, Nothing)
                    objEmailH = Nothing
                    clsCommon.MyMessageBoxShow(Me, "E-Mail Send Successfully", Me.Text)
                End If
            Else
                clsCommon.MyMessageBoxShow(Me, "First do email and sms setting", Me.Text)

            End If


            '=========================SMS Code=========================================
            If Not clsSMSAtPost_Purchase.SMSATPOST_PUR() Then
                SMSSendOnly(False)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub SMSSendOnly(ByVal isPost As Boolean)
        Try
            'Dim obj As clsEmailSMSSettingNew = clsEmailSMSSettingNew.GetData(clsUserMgtCode.mbtnPurchaseRequistion)

            'If obj Is Nothing Then
            '    clsCommon.MyMessageBoxShow("First do email and sms setting", Me.Text)
            '    Return
            'End If


            'If clsCommon.myLen(obj.smsbody) <= 0 Then
            '    Return
            'End If

            'Dim strMes As String = obj.smsbody
            'If strMes.Contains(clsEmailSMSConstants.PurchaseRequisitionNo) Then
            '    strMes = strMes.Replace(clsEmailSMSConstants.PurchaseRequisitionNo, txtReqNo.Value)
            'End If
            'If strMes.Contains(clsEmailSMSConstants.PurchaseRequisitionDate) Then
            '    strMes = strMes.Replace(clsEmailSMSConstants.PurchaseRequisitionDate, clsCommon.GetPrintDate(txtDate.Text, "dd/MMM/yyyy"))
            'End If
            'If strMes.Contains(clsEmailSMSConstants.TotalAmount) Then
            '    strMes = strMes.Replace(clsEmailSMSConstants.TotalAmount, lblTotRAmt.Text)
            'End If
            'If strMes.Contains(clsEmailSMSConstants.Form_Code) Then
            '    strMes = strMes.Replace(clsEmailSMSConstants.Form_Code, MyBase.Form_ID)
            'End If


            'Dim strphone As String = "" 'clsDBFuncationality.getSingleValue("select Phone1 from tspl_vendor_master where vendor_code ='" & txtVendorNo.Value & "' ")

            'If clsSMSSend.SendSMS(clsUserMgtCode.mbtnPurchaseRequistion, strMes, strphone) Then
            '    If Not isPost Then
            '        clsCommon.MyMessageBoxShow("SMS Send Successfully", Me.Text)
            '    End If
            'End If

            'sanjay
            Dim strContactPerson As String = ""
            Dim strotherno As String = Nothing
            strotherno = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select isnull(Phone,'') As Phone  from TSPL_EMPLOYEE_MASTER where emp_Code  in (Select User_Code from TSPL_APPROVAL_LEVEL_SCREEN where Module_Code ='MPurchase' and TRANS_Code='STORE-REQ')"))

            Dim dtContent As DataTable = clsDBFuncationality.GetDataTable("SELECT SMS_Text,Email_Text,Email_subject from TSPL_ES_Content where Form_ID='" + clsUserMgtCode.frmStoreRequistion + "'", Nothing)
            Dim objSMSH As New clsSMSHead()
            objSMSH.arrMobilNo = New List(Of String)()
            objSMSH.arrMobilNo.Add(clsCommon.myCstr(strotherno))
            If dtContent IsNot Nothing AndAlso dtContent.Rows.Count > 0 Then

                If clsCommon.myLen(dtContent.Rows(0)("SMS_Text")) > 0 Then

                    objSMSH.SMS_Text = clsCommon.myCstr(dtContent.Rows(0)("SMS_Text"))
                    objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.PurchaseRequisitionNo, txtReqNo.Value)
                    objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.PurchaseRequisitionDate, clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy"))
                    objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.TotalAmount, clsCommon.myCstr(lblTotRAmt.Text))
                    objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.Form_Code, clsUserMgtCode.frmStoreRequistion)
                End If

                If clsCommon.myLen(dtContent.Rows(0)("SMS_Text")) > 0 Then
                    objSMSH.SaveData(clsUserMgtCode.frmStoreRequistion, objSMSH, Nothing)
                    objSMSH = Nothing
                    clsCommon.MyMessageBoxShow("SMS Send Successfully", Me.Text)
                End If
            End If
            'Sanjay

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Public Function GetMailPrintPreview(ByVal strcode As String)
        Dim atchqry As String = "select TSPL_REQUISITION_HEAD.Requisition_Id ,convert(varchar,TSPL_REQUISITION_HEAD.Requisition_Date,103) as Requisition_Date , " & _
           "convert(varchar,TSPL_REQUISITION_HEAD.Expire_Date,103) as Expire_Date ,convert(varchar,TSPL_REQUISITION_HEAD.Require_Date,103) as Require_Date , " & _
           "TSPL_REQUISITION_HEAD.Ref_No ,TSPL_REQUISITION_HEAD.Description,TSPL_REQUISITION_HEAD.Remarks,TSPL_REQUISITION_HEAD.Request_By , " & _
           "TSPL_REQUISITION_DETAIL.Item_Code ,TSPL_REQUISITION_DETAIL.Item_Desc,TSPL_REQUISITION_DETAIL.Specification, " & _
           "TSPL_REQUISITION_DETAIL.Remarks as DRemarks ,TSPL_REQUISITION_DETAIL.Unit_Code ,TSPL_REQUISITION_DETAIL.Requisition_Qty, " & _
           "(select SUM( case when InOut='I' then Qty else  -1* Qty end )from TSPL_INVENTORY_MOVEMENT where Item_Code=TSPL_REQUISITION_DETAIL.Item_Code and TSPL_INVENTORY_MOVEMENT.Location_Code=TSPL_REQUISITION_HEAD.Location) as AvaiQty  , " & _
           "TSPL_VENDOR_MASTER.Vendor_Name,TSPL_REQUISITION_HEAD.Comments ,TSPL_COMPANY_MASTER.Comp_Name ,TSPL_COMPANY_MASTER.Logo_Img , " & _
           "TSPL_COMPANY_MASTER.Logo_Img2,user1.User_Name as CreatedBy,'' as AuthorizeBy ,TSPL_REQUISITION_HEAD.Request_By, " & _
           "TSPL_REQUISITION_HEAD.Require_Date,TSPL_REQUISITION_HEAD.Dept_Desc,TSPL_REQUISITION_HEAD.Location , " & _
           "TSPL_COMPANY_MASTER.Add1,case when  is_internal ='Y' then 'MATERIAL REQUISITION' else 'PURCHASE INDENT' END AS Heading  " & _
           "from TSPL_REQUISITION_HEAD join TSPL_REQUISITION_DETAIL on TSPL_REQUISITION_HEAD.Requisition_Id =TSPL_REQUISITION_DETAIL.Requisition_Id left outer join TSPL_COMPANY_MASTER on  TSPL_REQUISITION_HEAD.Comp_Code = TSPL_COMPANY_MASTER.Comp_Code left outer join TSPL_VENDOR_MASTER on TSPL_REQUISITION_DETAIL.Vendor_Code =TSPL_VENDOR_MASTER.Vendor_Code left outer join TSPL_USER_MASTER as user1 on TSPL_REQUISITION_HEAD.Created_By=user1.User_Code left outer join TSPL_USER_MASTER as user2 on TSPL_REQUISITION_HEAD.Modify_By=user2.User_Code   where(2 = 2)"

        If txtReqNo.Value <> "" Then
            atchqry += " and  TSPL_REQUISITION_HEAD.Requisition_Id='" + txtReqNo.Value + "'"
        End If

        Return atchqry
    End Function
    Private Sub BtnMailPreview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnMailPreview.Click
        If clsCommon.myLen(txtReqNo.Value) <= 0 Then
            clsCommon.MyMessageBoxShow("Select First Document No.", Me.Text)
            txtReqNo.Focus()
            txtReqNo.Select()
            Return
        End If
        Dim no As Integer = 0
        atchqry = GetMailPrintPreview(txtReqNo.Value)
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(atchqry)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                If dt.Rows(i)("vendor_name").ToString() <> "" Then
                    no = no + 1
                End If
            Next
            Dim frmCRV As New frmCrystalReportViewer()
            If no = 0 Then
                frmCRV.funreport(CrystalReportFolder.PurchaseOrder, dt, "PurchaseRequisitionWithoutVendor", "Purchase Requisition")
            Else
                frmCRV.funreport(CrystalReportFolder.PurchaseOrder, dt, "PurchaseRequisition", "Purchase Requisition")
            End If
            frmCRV = Nothing
        End If
    End Sub

    Private Sub btnMailSendemail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMailSendemail.Click

        Try
            If clsCommon.myLen(txtReqNo.Value) <= 0 Then
                clsCommon.MyMessageBoxShow("Please Select Document No. First", Me.Text)
                txtReqNo.Focus()
                txtReqNo.Select()
                Return
            End If

            If Not (common.clsCommon.MyMessageBoxShow("Send E-Mail/SMS Of Respective Purchase Requisition No. " + txtReqNo.Value + "" + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = DialogResult.Yes) Then
                Return
            End If
            LoadData(txtReqNo.Value, NavigatorType.Current)
            Dim lstUsers As New List(Of String)
            Dim strEmail As String = ""
            Dim Qry As String = ""
            Dim dt As DataTable
            Qry = "Select isnull(E_Mail,'') As Email  from TSPL_USER_MASTER where User_Code  in (Select User_Code from TSPL_APPROVAL_LEVEL_SCREEN where Module_Code ='MPurchase' and TRANS_Code='PO-REQ')"
            dt = clsDBFuncationality.GetDataTable(Qry)
            If dt.Rows.Count > 0 Then
                strEmail = clsCommon.myCstr(dt.Rows(0)("Email"))
            End If
            lstUsers.Add(strEmail)
            SendSMSandEmail(lstUsers, False)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub
    '-------------Richa Code Ends Here------------

    Private Sub txtRequestBy__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtRequestBy._MYValidating

        Dim obj As clsEmployeeMaster = clsEmployeeMaster.FinderForEmployeeSTORE(txtRequestBy.Value, isButtonClicked)
        If obj IsNot Nothing AndAlso clsCommon.myLen(obj.EMP_CODE) > 0 Then
            txtRequestBy.Value = obj.EMP_CODE
            lblRequestBy.Text = obj.Emp_Name
        Else
            txtRequestBy.Value = ""
            lblRequestBy.Text = ""
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
                lbl_budgetamt.Text = clsCapexBudget.GetBudget(Me.fndcapexsubcode.Value, Nothing)
                lbl_budgetamtwithtolerence.Text = clsCapexBudget.GetBudgetWithTolerence(Me.fndcapexsubcode.Value, Nothing)
                lbl_rebudgetamt.Text = clsCapexBudget.GetReBudget(Me.fndcapexsubcode.Value, txtReqNo.Value, Nothing)
                lbl_rebudgetamtwithtolerence.Text = clsCapexBudget.GetReBudgetWithTolerence(Me.fndcapexsubcode.Value, txtReqNo.Value, Nothing)
                If clsCommon.myLen(Me.fndcapexcode.Value) > 0 Then
                    lbl_capexcode.Text = clsCapexMaster.GetName(Me.fndcapexcode.Value, Nothing)
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtUnitCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtUnitCode._MYValidating
        Try
            Dim obj As clsUnitMaster = clsUnitMaster.Finder(txtUnitCode.Value, isButtonClicked)
            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Code) > 0 Then
                txtUnitCode.Value = obj.Code
                lblUnitDesc.Text = obj.Description
            Else
                txtUnitCode.Value = ""
                lblUnitDesc.Text = ""
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtCostCenterType__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtCostCenterType._MYValidating
        Try
            Dim obj As clsCostCenterTypeMaster = clsCostCenterTypeMaster.Finder(txtCostCenterType.Value, isButtonClicked, txtDept.Value)
            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Code) > 0 Then
                txtCostCenterType.Value = obj.Code
                lblCostcenterTypeDesc.Text = obj.Description
            Else
                txtCostCenterType.Value = ""
                lblCostcenterTypeDesc.Text = ""
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub Btncancel_Click(sender As Object, e As EventArgs) Handles btncancel.Click
        Try
            If clsCommon.myLen(txtReqNo.Value) > 0 Then
                If common.clsCommon.MyMessageBoxShow("Do you want to cancel the Requisition?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                    If clsRequistionHead.CancelData(Me.Form_ID, txtReqNo.Value) Then
                        clsCommon.MyMessageBoxShow(Me, "Requisition cancelled successfully!", Me.Text)
                        AddNew()
                    End If
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub


End Class


