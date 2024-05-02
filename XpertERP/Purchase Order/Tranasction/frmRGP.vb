''Changed By Balwinde On 19 oct 2011 For Deliverd By Finder.
'-Updation By-[Pankaj Kumar Chaudhary]--Against Ticket No=[BM00000001948, BM00000003163]
'---preeti Gupta---Ticket No.-BM00000003015--01/07/2014
'' Work done against ticket no. BHA/01/02/19-000803
Imports common
Imports System.Globalization
Imports System.Data.SqlClient
Imports System.IO

Public Class frmRGP
    Inherits FrmMainTranScreen

#Region "Variables"
    Dim is_Load_MRN As Boolean = False
    Dim IsPOScheduleOn As Boolean = False
    Dim IsRGPAfterPO As Boolean = False
    'Dim CreateJVinAllCases As Boolean = False
    Dim RunBatchFifowise As Integer = 0
    Public IsSRNSaved As String = "N"
    Private isCellValueChangedOpen As Boolean = False
    Private isNewEntry As Boolean = False
    Private isInsideLoadData As Boolean = False
    Private IsformLoad As Boolean = True
    Const colLineNo As String = "COLLNO"
    Const colICode As String = "COLICODE"
    Const colIName As String = "COLINAME"
    Const colHSNNo As String = "COLHSNNo"
    Const colQty As String = "COLQTY"
    Const colBalQty As String = "COLBalQTY"
    Const colRetQty As String = "COLRETQTY"
    Const colUnit As String = "COLUNIT"
    Const colRate As String = "COLRATE"
    Const colApproxCost As String = "colApproxCost"
    Const colAmt As String = "COLAMT"
    Const colLastRGPNRGP As String = "RGPNRGP"
    Const colDate As String = "Date"
    Const colSp As String = "COLSPECIFICATION"

    Const colRejSRNQty As String = "COLRejSRNQty"
    Const colMainPOItem As String = "MainPOItem"
    Const colMainBOMCode As String = "MainBOMCode"
    Const colIsSerialseItem As String = "COLISSERIALSEITEM"

    Public DocumentNo As String = Nothing
    Dim ButtonToolTip As ToolTip = New ToolTip()

    Dim repomrp As GridViewDecimalColumn = New GridViewDecimalColumn()
    Const colmrp As String = "COLMRP"
    Const colmrpmmandatory As String = "MRPMANDATORY"
    Const colItemType As String = "COLItemType"
    Const colPurchaseOrderNo As String = "PONO"
    Const colScheduleNo As String = "ScheduleNo"
    Const colPO_Sch_Qty As String = "POSchQty"
    Dim dt As DataTable
    Dim qry As String

    Const colBOMLineNo As String = "BLineNo"
    Const colBOMCode As String = "BCode"
    Const colBOMDesc As String = "BDesc"
    Const colBOMIcode As String = "Bicode"
    Const colBOMIname As String = "BIname"
    Const colBOMIUnit As String = "BIunit"
    Const colBOMQty As String = "BQty"
    Const colBOMBalanceQty As String = "BBalanceqty"
    Const colBOMRate As String = "Brate"
    Const colBOMMRP As String = "BMRP"
    Const colBOMAmt As String = "BAmt"
    Const colBOMSpecification As String = "BSP"
    Const colBOMPONo As String = "BPono"
    Const colBOMScheduleNo As String = "BSchNo"
    Const colBOMModule As String = "BModule"
    Dim ChkAutoDepOnPurchaseCycle As Boolean = False
#End Region

    Sub LoadBlankBOMGrid()
        gv_PO.Columns.Clear()
        gv_PO.Rows.Clear()

        Dim repoline As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoline.FormatString = ""
        repoline.Name = colBOMLineNo
        repoline.HeaderText = "Line No"
        repoline.ReadOnly = True
        repoline.Width = 70
        gv_PO.MasterTemplate.Columns.Add(repoline)

        repoline = New GridViewTextBoxColumn()
        repoline.FormatString = ""
        repoline.Name = colBOMCode
        repoline.HeaderText = "BOM Code"
        repoline.Width = 120
        repoline.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoline.TextImageRelation = TextImageRelation.TextBeforeImage
        repoline.IsVisible = False
        gv_PO.MasterTemplate.Columns.Add(repoline)

        repoline = New GridViewTextBoxColumn()
        repoline.FormatString = ""
        repoline.Name = colBOMDesc
        repoline.HeaderText = "BOM Description"
        repoline.Width = 180
        repoline.ReadOnly = True
        repoline.IsVisible = False
        gv_PO.MasterTemplate.Columns.Add(repoline)

        repoline = New GridViewTextBoxColumn()
        repoline.FormatString = ""
        repoline.Name = colBOMIcode
        repoline.HeaderText = "Item Code"
        repoline.Width = 130
        repoline.ReadOnly = False
        repoline.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoline.TextImageRelation = TextImageRelation.TextBeforeImage
        gv_PO.MasterTemplate.Columns.Add(repoline)

        repoline = New GridViewTextBoxColumn()
        repoline.FormatString = ""
        repoline.Name = colBOMIname
        repoline.HeaderText = "Item Description"
        repoline.Width = 280
        repoline.ReadOnly = True
        gv_PO.MasterTemplate.Columns.Add(repoline)

        repoline = New GridViewTextBoxColumn()
        repoline.FormatString = ""
        repoline.Name = colBOMIUnit
        repoline.HeaderText = "Unit"
        repoline.Width = 80
        repoline.ReadOnly = False
        repoline.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoline.TextImageRelation = TextImageRelation.TextBeforeImage
        gv_PO.MasterTemplate.Columns.Add(repoline)

        Dim repoline1 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoline1.FormatString = ""
        repoline1.Name = colBOMQty
        repoline1.HeaderText = "Quantity"
        repoline1.Width = 80
        repoline1.DecimalPlaces = 2
        gv_PO.MasterTemplate.Columns.Add(repoline1)

        repoline1 = New GridViewDecimalColumn()
        repoline1.FormatString = ""
        repoline1.Name = colBOMBalanceQty
        repoline1.HeaderText = "Balance Qty"
        repoline1.Width = 80
        repoline1.DecimalPlaces = 2
        repoline1.ReadOnly = True
        repoline1.IsVisible = True
        gv_PO.MasterTemplate.Columns.Add(repoline1)

        repoline1 = New GridViewDecimalColumn()
        repoline1.FormatString = ""
        repoline1.Name = colBOMRate
        repoline1.HeaderText = "Unit Cost"
        repoline1.Width = 80
        repoline1.DecimalPlaces = 2
        repoline1.ReadOnly = False
        gv_PO.MasterTemplate.Columns.Add(repoline1)

        repoline1 = New GridViewDecimalColumn()
        repoline1.FormatString = ""
        repoline1.Name = colBOMMRP
        repoline1.HeaderText = "MRP"
        repoline1.Width = 80
        repoline1.DecimalPlaces = 2
        repoline1.ReadOnly = False
        gv_PO.MasterTemplate.Columns.Add(repoline1)

        repoline1 = New GridViewDecimalColumn()
        repoline1.FormatString = ""
        repoline1.Name = colBOMAmt
        repoline1.HeaderText = "Amount"
        repoline1.Width = 80
        repoline1.DecimalPlaces = 2
        repoline1.ReadOnly = True
        gv_PO.MasterTemplate.Columns.Add(repoline1)

        repoline = New GridViewTextBoxColumn()
        repoline.FormatString = ""
        repoline.Name = colBOMSpecification
        repoline.HeaderText = "Specification"
        repoline.Width = 100
        repoline.MaxLength = 200
        gv_PO.MasterTemplate.Columns.Add(repoline)

        repoline = New GridViewTextBoxColumn()
        repoline.FormatString = ""
        repoline.Name = colBOMPONo
        repoline.HeaderText = "Purchase No."
        repoline.Width = 110
        repoline.ReadOnly = True
        gv_PO.MasterTemplate.Columns.Add(repoline)

        repoline = New GridViewTextBoxColumn()
        repoline.FormatString = ""
        repoline.Name = colBOMScheduleNo
        repoline.HeaderText = "Schedule No."
        repoline.Width = 110
        repoline.ReadOnly = True
        gv_PO.MasterTemplate.Columns.Add(repoline)

        repoline = New GridViewTextBoxColumn()
        repoline.FormatString = ""
        repoline.Name = colBOMModule
        repoline.HeaderText = "Module Id"
        repoline.Width = 100
        repoline.IsVisible = False
        gv_PO.MasterTemplate.Columns.Add(repoline)

        gv_PO.AllowDeleteRow = True
        gv_PO.AllowAddNewRow = False
        gv_PO.ShowGroupPanel = False
        gv_PO.AllowColumnReorder = True
        gv_PO.AllowRowReorder = False
        gv_PO.EnableSorting = False
        gv_PO.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv_PO.MasterTemplate.ShowRowHeaderColumn = False
        gv_PO.TableElement.TableHeaderHeight = 25
    End Sub

    Public Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.mbtnGatePass)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnPost.Visible = MyBase.isPostFlag
        btnDelete.Visible = MyBase.isDeleteFlag
        btnprint.Visible = MyBase.isPrintFlag
        btnReverse.Visible = False
        'If MyBase.isReverse Then
        '    btnReverse.Enabled = True
        'Else
        '    btnReverse.Enabled = False
        'End If
    End Sub

    Sub LoadItemType()
        cboItemType.DataSource = clsItemMaster.GetItemType()
        cboItemType.ValueMember = "Code"
        cboItemType.DisplayMember = "Name"
    End Sub

    Private Sub FrmAPInvoiceEntry_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Dim coll As Dictionary(Of String, String)
        'coll = New Dictionary(Of String, String)()
        'coll.Add("Is_Against_CC_Transfer", "int default 0")
        'coll.Add("To_Location_Code", "varchar(12) NULL")
        'clsCommonFunctionality.CreateOrAlterTable(True, False, "TSPL_RGP_HEAD", coll, Nothing, False, False)

        SetUserMgmtNew()
        lblNrgpReq.Visible = False
        nrgpReqFnd.Visible = False
        '=====if rgp after po on then po show on screen andalso if sch. setting on then sch. finder seen========================================
        MyLabel19.Visible = False
        txtScheduleNo.Visible = False
        MyLabel18.Visible = False
        txtPoNo.Visible = False
        '=========Added by preeti gupta Against Ticket No[BHA/22/05/18-000032]
        lblPODate.Visible = False
        dtPoDate.Visible = False


        chkAsPerBOM.Visible = False

        RadPageViewPage2.Text = "Item Detail"
        RunBatchFifowise = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.RunBatchFifowise, clsFixedParameterCode.RunBatchFifowise, Nothing))
        IsRGPAfterPO = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.IsRGPAfterPurchaseOrder, clsFixedParameterCode.IsRGPAfterPurchaseOrder, Nothing)) = "1", True, False))
        If IsRGPAfterPO = True Then
            MyLabel18.Visible = True
            txtPoNo.Visible = True
            lblPODate.Visible = True
            dtPoDate.Visible = True
            chkAsPerBOM.Visible = True

            IsPOScheduleOn = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowPOScheduling, clsFixedParameterCode.AllowPOScheduling, Nothing)) = "1", True, False))
            If IsPOScheduleOn = True Then
                MyLabel19.Visible = True
                txtScheduleNo.Visible = True
            End If
        End If
        'CreateJVinAllCases = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.CreateJVForAllCasesinRGP, clsFixedParameterCode.CreateJVForAllCasesinRGP, Nothing)) = "1", True, False))
        'If CreateJVinAllCases Then
        '    chkjvdisplaytype.Visible = True
        'Else
        '    chkjvdisplaytype.Visible = False
        'End If
        is_Load_MRN = False ' IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ShowGRN, clsFixedParameterCode.ShowGRN, Nothing)) = 1 AndAlso clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ShowMRN, clsFixedParameterCode.ShowMRN, Nothing)) = 1, True, False)
        '=================end here=================================

        gbRGPNRGP.Visible = True
        txtVendorNo.MendatroryField = True
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction")
        ButtonToolTip.SetToolTip(btnPost, "Press Alt+P Post Trasnaction")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnAddNew, "Press Alt+N Adding New Trasnaction")
        ButtonToolTip.SetToolTip(btnAddNew, "Press Alt+A Create Additional Cost")
        LoadBlankGrid()
        gv1.Rows.AddNew()
        IsformLoad = True
        LoadBilling()
        LoadDocType()
        IsformLoad = False
        LoadBlankBOMGrid()
        AddNew()
        txtNRGPNo.Enabled = False
        SetLength()
        RadPageView1.SelectedPage = RadPageViewPage1

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

        '-------------Done By Monika---------------------
        cboItemType.Enabled = False
        If clsCommon.CompairString(MDI.IsLoc_Third_Party, "YES") = CompairStringResult.Equal Then
            RadGroupBox1.Visible = True

            If cboDocType.SelectedValue = "RGP" Then
                RadGroupBox1.Enabled = True
            Else
                RadGroupBox1.Enabled = False
            End If

            LoadItemType()

            MyLabel3.Visible = True
            cboItemType.Visible = True
        Else
            RadGroupBox1.Visible = False
            RadGroupBox1.Enabled = False
            MyLabel3.Visible = False
            cboItemType.Visible = False
        End If
        '---------------------------------------------------------

        If clsCommon.myLen(DocumentNo) > 0 Then
            LoadData(DocumentNo, NavigatorType.Current, False)
        End If
        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current, False)
        End If
    End Sub
    Sub AllowDepartmentMandatoryOnPurchaseCycle()
        ChkAutoDepOnPurchaseCycle = IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AutoDepartmentMendatroryFieldOnPurcahseCycle, clsFixedParameterCode.AutoDepartmentMendatroryFieldOnPurcahseCycle, Nothing)) = "1", True, False)
        If ChkAutoDepOnPurchaseCycle Then
            txtDepartment.Enabled = False
            txtDepartment.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Segment_code from TSPL_USER_MASTER where User_Code ='" + objCommonVar.CurrentUserCode + "'"))
            lblDepartment.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select top 1 Segment_name from TSPL_GL_SEGMENT_CODE where Seg_No=3 and Segment_code='" + txtDepartment.Value + "'"))
        Else
            txtDepartment.Enabled = True
        End If
    End Sub
    Sub SetLength()
        txtDocNo.MyMaxLength = 30
        txtRemarks.MaxLength = 200
        txtReason.MaxLength = 200
        txtVehicleNo.MaxLength = 50
    End Sub

    Public Sub LoadDocType()
        dt = New DataTable
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Desc", GetType(String))
        dt.Rows.Add("RGP", "Returnable Gate Pass")
        dt.Rows.Add("NRGP", "Non Returnable Gate Pass")
        dt.Rows.Add("NRGPR", "NRGP Return")
        dt.Rows.Add("RGPR", "RGP Return")
        cboDocType.DataSource = dt
        cboDocType.ValueMember = "Code"
        cboDocType.DisplayMember = "Desc"

        RadGroupBox1.Enabled = False
        If clsCommon.CompairString(MDI.IsLoc_Third_Party, "YES") = CompairStringResult.Equal AndAlso cboDocType.SelectedValue = "RGP" Then
            RadGroupBox1.Enabled = True
        End If
    End Sub

    Sub LoadBilling()
        dt = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))
        dt.Rows.Add("Y", "Yes")
        dt.Rows.Add("N", "No")
        ddlBilling.DataSource = dt
        ddlBilling.ValueMember = "Code"
        ddlBilling.DisplayMember = "Name"
    End Sub

    Sub BlankAllControls()
        txtRoadPermitDate.Checked = False
        txtRoadPermitDate.Value = clsCommon.GETSERVERDATE()
        txtGRDate.Checked = False
        txtGRDate.Value = clsCommon.GETSERVERDATE()
        txtGRNo.Text = ""
        txtRoadPermitNo.Text = ""
        txtsrnlocation_code.Value = ""
        txtsrnlocation.Text = ""
        chkthirdparty.Checked = False
        cboItemType.SelectedValue = ""
        RadGroupBox1.Enabled = True
        chkjvdisplaytype.Checked = False
        txtDocNo.Value = ""
        txtReason.Text = ""
        chkOnHold.Checked = False
        txtVendorNo.Value = ""
        txtDepartment.Value = ""
        lblDepartment.Text = ""
        chkAgainst_Sale.Checked = False
        lblVendorName.Text = ""
        txtModeOfTransport.Text = ""
        txtCashMemoDetail.Text = ""
        txtDate.Value = clsCommon.GETSERVERDATE()
        txtReason.Text = ""
        txtRemarks.Text = ""
        UsLock1.Status = ERPTransactionStatus.Pending
        txtVehicleNo.Text = ""
        txtGPNo.Text = ""
        txtNRGPNo.Value = ""
        txtGPDate.Checked = False
        txtGPDate.Value = txtDate.Value
        cboDocType.Enabled = True
        txtLocation.Enabled = True
        txtLocation.Value = ""
        lblLocation.Text = ""
        lblDocumentAmt.Text = ""
        txtDeliveredBy.Value = ""
        lblDeliveredBy.Text = ""
        chkNonInventoryItem.Checked = False
        fndCostCentre.Value = ""
        txtCostCentre.Text = ""
        txtInvoiceNO.Text = ""
        txtPartyName.Text = ""
        txtPartyAddress.Text = ""
        grp3rdParty.Visible = False
        ''richa Ticket No BM00000003061 on 01/08/2014
        chkAgainstJobWork.Checked = False
        chkAgainstJobWork.Enabled = False
        If clsCommon.CompairString(cboDocType.SelectedValue, "RGP") = CompairStringResult.Equal Then
            chkAgainstJobWork.Enabled = True
        End If
        '-------------------------------------------
        txtPoNo.Value = ""
        txtPoNo.Enabled = False
        dtPoDate.Value = clsCommon.GETSERVERDATE()
        dtPoDate.Enabled = False
        txtScheduleNo.Value = ""
        txtScheduleNo.Enabled = False
        txtAMCRefNo.Text = ""

        ''For Custom Fields
        If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
            UcCustomFields1.BlankAllControls()
        End If
        ''End of For Custom Fields
        UcAttachment1.BlankAllControls()
        txtManPoNo.Text = ""
        'txtManPoNo.Enabled = False
        'dtManPoDate.Enabled = False
        dtManPoDate.Value = clsCommon.GETSERVERDATE()
        '=============initially grid of po item is not seen.
        chkAsPerBOM.Enabled = False
        chkAsPerBOM.Checked = False
        txtAsitis.Text = ""
        btnSameasAbove.Enabled = True

        gv_PO.Rows.Clear()
        gv_PO.Rows.AddNew()
        RadPageViewPage3.Item.Visibility = ElementVisibility.Collapsed
        RadPageView2.SelectedPage = RadPageViewPage2
        ''==============================================
    End Sub

    Sub LoadBlankGrid()
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

        Dim repoICode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoICode.FormatString = ""
        repoICode.HeaderText = "Item Code"
        repoICode.Name = colICode
        repoICode.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoICode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoICode.Width = 120
        gv1.MasterTemplate.Columns.Add(repoICode)

        Dim repoIName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoIName.FormatString = ""
        repoIName.HeaderText = "Item Description"
        repoIName.Name = colIName
        repoIName.Width = 220
        repoIName.ReadOnly = False
        gv1.MasterTemplate.Columns.Add(repoIName)

        repoIName = New GridViewTextBoxColumn()
        repoIName.FormatString = ""
        repoIName.HeaderText = "HSN No"
        repoIName.Name = colHSNNo
        repoIName.Width = 150
        repoIName.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoIName)

        '' Anubhooti 07-Oct-2014
        Dim repoRejSRNQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoRejSRNQty.FormatString = ""
        repoRejSRNQty.HeaderText = "Rejected SRN Quantity"
        repoRejSRNQty.Name = colRejSRNQty
        repoRejSRNQty.Width = 100
        repoRejSRNQty.Minimum = 0
        repoRejSRNQty.IsVisible = True
        repoRejSRNQty.ReadOnly = True
        repoRejSRNQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoRejSRNQty)
        ''

        repoRejSRNQty = New GridViewDecimalColumn()
        repoRejSRNQty.FormatString = ""
        repoRejSRNQty.HeaderText = "PO Quantity"
        repoRejSRNQty.Name = colPO_Sch_Qty
        repoRejSRNQty.Width = 100
        repoRejSRNQty.Minimum = 0
        repoRejSRNQty.IsVisible = False
        repoRejSRNQty.ReadOnly = True
        repoRejSRNQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoRejSRNQty)

        Dim repoQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoQty.FormatString = ""
        repoQty.HeaderText = "Quantity"
        repoQty.Name = colQty
        repoQty.Width = 100
        repoQty.Minimum = 0
        repoQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoQty)

        repoQty = New GridViewDecimalColumn()
        repoQty.FormatString = ""
        repoQty.HeaderText = "Balance Quantity"
        repoQty.Name = colBalQty
        repoQty.ReadOnly = True
        repoQty.Width = 100
        repoQty.Minimum = 0
        repoQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoQty)

        repoQty = New GridViewDecimalColumn()
        repoQty.FormatString = ""
        repoQty.HeaderText = "Return Quantity"
        repoQty.Name = colRetQty
        repoQty.Width = 100
        repoQty.Minimum = 0
        repoQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoQty)


        Dim repoUnit As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoUnit.FormatString = ""
        repoUnit.HeaderText = "UOM"
        repoUnit.Name = colUnit
        repoUnit.Width = 100
        repoUnit.ReadOnly = False
        repoUnit.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoUnit.TextImageRelation = TextImageRelation.TextBeforeImage
        gv1.MasterTemplate.Columns.Add(repoUnit)

        Dim repoRate As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoRate = New GridViewDecimalColumn()
        repoRate.FormatString = ""
        repoRate.HeaderText = "Unit Cost"
        repoRate.Name = colRate
        repoRate.Width = 100
        repoRate.Minimum = 0
        repoRate.FormatString = "{0:n4}"
        repoRate.DecimalPlaces = 4
        '' Anubhooti 06-Feb-2014 (Non-Editable By Amit Sir)
        repoRate.ReadOnly = True
        repoRate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoRate)

        '' Anubhooti 06-Feb-2014 (New Column By Amit Sir)
        Dim repoACost As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoACost = New GridViewDecimalColumn()
        repoACost.FormatString = ""
        repoACost.HeaderText = "Approx. Cost"
        repoACost.Name = colApproxCost
        repoACost.Width = 110
        repoACost.Minimum = 0
        repoACost.FormatString = "{0:n4}"
        repoACost.DecimalPlaces = 4
        repoACost.ReadOnly = False
        repoACost.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoACost)
        ''
        Dim repoAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAmt = New GridViewDecimalColumn()
        repoAmt.FormatString = ""
        repoAmt.HeaderText = "Extended Cost"
        repoAmt.Name = colAmt
        repoAmt.Width = 100
        repoAmt.Minimum = 0
        repoAmt.ReadOnly = True
        repoAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoAmt)

        Dim LastRGPNRGP As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        LastRGPNRGP.FormatString = ""
        LastRGPNRGP.HeaderText = "Last RGP/NRGP"
        LastRGPNRGP.Name = colLastRGPNRGP
        LastRGPNRGP.Width = 100
        LastRGPNRGP.ReadOnly = True
        LastRGPNRGP.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.MasterTemplate.Columns.Add(LastRGPNRGP)

        Dim GRPDate As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        GRPDate.FormatString = ""
        GRPDate.HeaderText = "RGP/NRGP Date"
        GRPDate.Name = colDate
        GRPDate.Width = 100
        GRPDate.ReadOnly = True
        GRPDate.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.MasterTemplate.Columns.Add(GRPDate)

        Dim repoSP As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSP = New GridViewTextBoxColumn()
        repoSP.FormatString = ""
        repoSP.HeaderText = "Specification"
        repoSP.Name = colSp
        repoSP.Width = 100
        'repoSP.Minimum = 0
        repoSP.ReadOnly = False
        repoSP.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoSP)

        repoSP = New GridViewTextBoxColumn()
        repoSP.FormatString = ""
        repoSP.HeaderText = "PO No."
        repoSP.Name = colPurchaseOrderNo
        repoSP.Width = 100
        'repoSP.Minimum = 0
        repoSP.ReadOnly = True
        repoSP.IsVisible = True
        If Not IsRGPAfterPO Then
            repoSP.IsVisible = False
        End If
        repoSP.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.MasterTemplate.Columns.Add(repoSP)

        repoSP = New GridViewTextBoxColumn()
        repoSP.FormatString = ""
        repoSP.HeaderText = "Schedule No."
        repoSP.Name = colScheduleNo
        repoSP.Width = 100
        repoSP.IsVisible = False
        repoSP.ReadOnly = True
        repoSP.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.MasterTemplate.Columns.Add(repoSP)
        If IsPOScheduleOn = True AndAlso IsRGPAfterPO Then
            repoSP.IsVisible = True
        End If
        '-------------------------------------------------------------------------
        Dim repoismrp As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoismrp = New GridViewTextBoxColumn()
        repoismrp.FormatString = ""
        repoismrp.HeaderText = "ISMRP"
        repoismrp.Name = colmrpmmandatory
        repoismrp.Width = 80
        repoismrp.ReadOnly = False
        repoismrp.IsVisible = False
        repoismrp.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoismrp)

        repomrp = New GridViewDecimalColumn()
        repomrp.FormatString = ""
        repomrp.HeaderText = "MRP"
        repomrp.Name = colmrp
        repomrp.Width = 80
        repomrp.ReadOnly = False
        repomrp.IsVisible = False
        repomrp.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repomrp)

        If clsCommon.CompairString(MDI.IsLoc_Third_Party, "YES") = CompairStringResult.Equal Then
            repomrp.IsVisible = True
        End If
        '---------------------------------------------------------------------------------

        repoismrp = New GridViewTextBoxColumn()
        repoismrp.FormatString = ""
        repoismrp.HeaderText = "Main PO Item"
        repoismrp.Name = colMainPOItem
        repoismrp.Width = 80
        repoismrp.ReadOnly = True
        repoismrp.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoismrp)

        repoismrp = New GridViewTextBoxColumn()
        repoismrp.FormatString = ""
        repoismrp.HeaderText = "Main BOM Code"
        repoismrp.Name = colMainBOMCode
        repoismrp.Width = 80
        repoismrp.ReadOnly = True
        repoismrp.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoismrp)


        Dim repoIsSerItem As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsSerItem.HeaderText = "Is Serialize Item"
        repoIsSerItem.Name = colIsSerialseItem
        repoIsSerItem.ReadOnly = True
        repoIsSerItem.IsVisible = False
        repoIsSerItem.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsSerItem)

        clsCustomFieldGrid.LoadBlankGrid(gv1, MyBase.ArrDetailFields)

        gv1.AllowDeleteRow = True
        gv1.AllowAddNewRow = False
        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = True
        gv1.AllowRowReorder = False
        gv1.EnableSorting = False
        gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        gv1.TableElement.TableHeaderHeight = 25
        ReStoreGridLayout()
    End Sub

    '' Anubhooti 10-Dec-2014 (Added New Col. Item Type(O2M/M2O))
    Public Shared Function GetItemType() As DataTable
        Dim dt As New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow = dt.NewRow()
        dr("Code") = "N"
        dr("Name") = "None"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "O"
        dr("Name") = "One To Many"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "M"
        dr("Name") = "Many To One"
        dt.Rows.Add(dr)

        Return dt
    End Function
    ''
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
                    If e.Column.FieldName.StartsWith("_CFLD_") Then
                        clsCustomFieldGrid.getFinderForCustomFieldGrid(gv1, e.Column.Name.ToString, MyBase.Form_ID)
                    End If
                    If e.Column Is gv1.Columns(colQty) Then

                        OpenBatchItem()
                    End If
                    If (e.Column Is gv1.Columns(colICode) OrElse e.Column Is gv1.Columns(colUnit) OrElse e.Column Is gv1.Columns(colQty) OrElse e.Column Is gv1.Columns(colRate)) Then
                        'If gv1.CurrentColumn Is gv1.Columns(colQty) And chkNonInventoryItem.Checked = False Then
                        '    Dim stockqty As Double = 0
                        '    If clsCommon.myLen(txtLocation.Value) <> 0 And clsCommon.myLen(clsCommon.myCdbl(gv1.CurrentRow.Cells(colICode).Value)) <> 0 Then
                        '        Dim str As String = "select sum(Item_Qty) from TSPL_ITEM_LOCATION_DETAILS where Item_Code='" + gv1.CurrentRow.Cells(colICode).Value + "' and Location_Code='" + txtLocation.Value + "' "
                        '        stockqty = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
                        '        Dim item As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value)
                        '        If stockqty = 0 Then
                        '            common.clsCommon.MyMessageBoxShow("Stock Qty  not available at this location ")
                        '            gv1.CurrentRow.Cells(colQty).Value = 0
                        '        Else
                        '            If stockqty < clsCommon.myCdbl(gv1.CurrentRow.Cells(colQty).Value) Then
                        '                common.clsCommon.MyMessageBoxShow("Qty more then stock qty not allowed,balance qty of  '" + item + "' is '" + clsCommon.myCstr(stockqty) + "' ")
                        '                gv1.CurrentRow.Cells(colQty).Value = 0
                        '            End If
                        '        End If
                        '    Else
                        '        common.clsCommon.MyMessageBoxShow("Select the Location")
                        '        gv1.CurrentRow.Cells(colQty).Value = 0
                        '    End If
                        'End If

                        If e.Column Is gv1.Columns(colQty) OrElse e.Column Is gv1.Columns(colRate) Then
                            If e.Column Is gv1.Columns(colQty) Then
                                OpenSerialItem()
                            End If
                            UpdateCurrentRow(0)
                            UpdateAllTotals()


                        ElseIf e.Column Is gv1.Columns(colUnit) And chkNonInventoryItem.Checked = False Then
                            OpenUOMList(False)
                        ElseIf e.Column Is gv1.Columns(colICode) And chkNonInventoryItem.Checked = False Then
                            '-----------------check when thrd party location on----------------------------------------
                            If chkthirdparty.Checked Then
                                If clsCommon.myLen(cboItemType.SelectedValue) <= 0 Then
                                    clsCommon.MyMessageBoxShow(Me, "Please Select Item Type First", Me.Text)
                                    isInsideLoadData = False
                                    isCellValueChangedOpen = False
                                    Return
                                ElseIf clsCommon.myLen(txtLocation.Value) <= 0 Then
                                    clsCommon.MyMessageBoxShow(Me, "Please Select Location First", Me.Text)
                                    isInsideLoadData = False
                                    isCellValueChangedOpen = False
                                    Return
                                End If
                            End If
                            '--------------------------------------------------------------------------------------------

                            OpenICodeList(False)
                            If clsCommon.CompairString(cboDocType.SelectedValue, "RGP") = CompairStringResult.Equal Then
                                Dim Qry As String = "Select RGP_No, CONVERT(VARCHAR,RGP_Date,103) as RGP_Date, Specification from ("
                                Qry += " Select TSPL_RGP_HEAD.RGP_No, TSPL_RGP_HEAD.RGP_Date, TSPL_RGP_DETAIL.Specification,ROW_NUMBER() OVER (Order By RGP_Date DESC) as Row from TSPL_RGP_HEAD "
                                Qry += " LEFT OUTER JOIN TSPL_RGP_DETAIL ON TSPL_RGP_HEAD.RGP_No=TSPL_RGP_DETAIL.RGP_No  "
                                Qry += " Where TSPL_RGP_HEAD.Doc_Type='RGP' AND TSPL_RGP_DETAIL.Item_Code='" + gv1.CurrentRow.Cells(colICode).Value + "') XXX WHERE XXX.Row=1"
                                Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
                                If dt.Rows.Count > 0 Then
                                    gv1.CurrentRow.Cells(colLastRGPNRGP).Value = clsCommon.myCstr(dt.Rows(0)("RGP_No"))
                                    gv1.CurrentRow.Cells(colDate).Value = clsCommon.myCstr(dt.Rows(0)("RGP_Date"))
                                    gv1.CurrentRow.Cells(colSp).Value = clsCommon.myCstr(dt.Rows(0)("Specification"))
                                Else
                                    gv1.CurrentRow.Cells(colLastRGPNRGP).Value = ""
                                    gv1.CurrentRow.Cells(colDate).Value = ""
                                    gv1.CurrentRow.Cells(colSp).Value = ""
                                End If
                            End If
                        End If
                        setGridFocus()
                    End If

                    isCellValueChangedOpen = False
                End If
            End If
        Catch ex As Exception
            isCellValueChangedOpen = False
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub OpenUOMList(ByVal isButtonClick As Boolean)
        If clsCommon.myLen(gv1.CurrentRow.Cells(colPurchaseOrderNo).Value) > 0 OrElse clsCommon.myLen(gv1.CurrentRow.Cells(colScheduleNo).Value) > 0 Then
            Exit Sub
        End If

        Dim strICode As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value)
        If clsCommon.myLen(strICode) > 0 Then
            Dim qry As String = "select UOM_Code as Code,UOM_Description as [Description] from TSPL_ITEM_UOM_DETAIL"
            Dim whrCls As String = "Item_Code='" + strICode + "'"
            gv1.CurrentRow.Cells(colUnit).Value = clsCommon.ShowSelectForm("SRNItefndnder", qry, "Code", whrCls, clsCommon.myCstr(gv1.CurrentRow.Cells(colUnit).Value), "Code", isButtonClick)
        End If
        setBalance()
    End Sub

    Private Sub setGridFocus()
        Try
            Dim intCurrRow As Integer = gv1.CurrentRow.Index
            If clsCommon.myLen(gv1.CurrentRow.Cells(colICode).Value) > 0 Then
                If gv1.CurrentColumn Is gv1.Columns(colICode) Then
                    gv1.CurrentRow = gv1.Rows(intCurrRow)
                    gv1.CurrentColumn = gv1.Columns(colQty)
                ElseIf gv1.CurrentColumn Is gv1.Columns(colQty) Then
                    gv1.CurrentRow = gv1.Rows(intCurrRow)
                    gv1.CurrentColumn = gv1.Columns(colRate)
                ElseIf gv1.CurrentColumn Is gv1.Columns(colRate) Then
                    gv1.CurrentRow = gv1.Rows(intCurrRow + 1)
                    gv1.CurrentColumn = gv1.Columns(colICode)
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub OpenICodeList(ByVal isButtonClick As Boolean)
        If clsCommon.myLen(gv1.CurrentRow.Cells(colPurchaseOrderNo).Value) > 0 OrElse clsCommon.myLen(gv1.CurrentRow.Cells(colScheduleNo).Value) > 0 Then
            Exit Sub
        End If

        '========
        'Dim AllBomCOde As String = ""
        'If chkAsPerBOM.Checked AndAlso chkAgainstJobWork.Checked AndAlso IsRGPAfterPO Then
        '    For Each grow As GridViewRowInfo In gv_PO.Rows
        '        If clsCommon.myLen(grow.Cells(colBOMCode).Value) > 0 Then
        '            AllBomCOde = AllBomCOde + "','" + clsCommon.myCstr(grow.Cells(colBOMCode).Value)
        '        End If
        '    Next

        '    If clsCommon.myLen(AllBomCOde) > 0 AndAlso AllBomCOde.Substring(0, 3) = "','" Then
        '        AllBomCOde = AllBomCOde.Substring(2, clsCommon.myLen(AllBomCOde) - 2) + "'"
        '    End If

        'End If
        Dim whrcls As String = ""
        If chkAsPerBOM.Checked AndAlso chkAgainstJobWork.Checked AndAlso IsRGPAfterPO Then
            whrcls = " tspl_item_master.item_code in (select item_code from TSPL_PP_BOM_ITEM_DETAIL where bom_code in (select bom_code from tspl_pp_bom_head where Is_Post='1' and is_osp='1' and vendor_code='" + txtVendorNo.Value + "'))"
        End If




        Dim obj As clsItemMaster
        If chkthirdparty.Checked Then
            obj = clsItemMaster.FinderForThirdPartyItem(clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value), clsCommon.myCstr(cboItemType.SelectedValue), txtLocation.Value, isButtonClick)
        Else
            obj = clsItemMaster.FinderForItem(clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value), "", isButtonClick, Nothing, whrcls)
        End If

        If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Item_Code) > 0 Then
            gv1.CurrentRow.Cells(colICode).Value = obj.Item_Code
            gv1.CurrentRow.Cells(colIName).Value = obj.Item_Desc
            gv1.CurrentRow.Cells(colHSNNo).Value = clsItemMaster.GetItemHSNCode(obj.Item_Code, Nothing)
            gv1.CurrentRow.Cells(colUnit).Value = obj.Unit_Code
            gv1.CurrentRow.Cells(colIsSerialseItem).Value = obj.Is_Serial_Item
            '' Anubhooti 06-Jan-2015 (Costing Method Avg/FIFO/LIFO)
            Dim strDate As String = txtDate.Value
            Dim dblUnitCost As Double = 0
            Dim dblCostMethod As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Costing_Method as Costing_Method from TSPL_ITEM_MASTER left outer join TSPL_PURCHASE_ACCOUNTS on TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code=TSPL_ITEM_MASTER.Purchase_Class_Code where Item_Code='" & clsCommon.myCstr(obj.Item_Code) & "' "))
            If dblCostMethod <> 0 Then
                dblUnitCost = clsInventoryMovement.GetCost(dblCostMethod, clsCommon.myCstr(obj.Item_Code), txtLocation.Value, 1, strDate, strDate, False, Nothing)
                gv1.CurrentRow.Cells(colRate).Value = dblUnitCost
                gv1.CurrentRow.Cells(colApproxCost).Value = dblUnitCost
            Else
                gv1.CurrentRow.Cells(colRate).Value = 0
                gv1.CurrentRow.Cells(colApproxCost).Value = 0
            End If
            ''
            '---------------------------------check for mrp in case of third party location---------------
            Try
                If (clsCommon.CompairString(MDI.IsLoc_Third_Party, "YES") = CompairStringResult.Equal) AndAlso chkthirdparty.Checked Then
                    Dim qry As String = "select is_mrp from tspl_item_master where item_code='" + obj.Item_Code + "'"
                    gv1.CurrentRow.Cells(colmrpmmandatory).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
                End If
            Catch ex As Exception
            End Try
        Else
            gv1.CurrentRow.Cells(colICode).Value = ""
            gv1.CurrentRow.Cells(colIName).Value = ""
            gv1.CurrentRow.Cells(colHSNNo).Value = ""
            gv1.CurrentRow.Cells(colUnit).Value = ""
            gv1.CurrentRow.Cells(colmrpmmandatory).Value = ""
        End If
        setBalance()
    End Sub

    Private Sub UpdateCurrentRow(ByVal intRowNo As Integer)
        If intRowNo > 0 Then
            Dim dblQty As Double = clsCommon.myCdbl(gv1.Rows(intRowNo).Cells(colQty).Value)
            Dim dblRate As Double = clsCommon.myCdbl(gv1.Rows(intRowNo).Cells(colRate).Value)
            Dim dblAmt As Double = dblQty * dblRate
            gv1.Rows(intRowNo).Cells(colAmt).Value = Math.Round(dblAmt, 2)
        Else
            Dim dblQty As Double = clsCommon.myCdbl(gv1.CurrentRow.Cells(colQty).Value)
            Dim dblRate As Double = clsCommon.myCdbl(gv1.CurrentRow.Cells(colRate).Value)
            Dim dblAmt As Double = dblQty * dblRate
            gv1.CurrentRow.Cells(colAmt).Value = Math.Round(dblAmt, 2)
        End If
    End Sub

    Private Sub UpdateAllTotals()
        Dim dblTotAmt As Double = 0
        For ii As Integer = 0 To gv1.Rows.Count - 1
            If (clsCommon.myLen(gv1.Rows(ii).Cells(colICode).Value) > 0) Then
                dblTotAmt = dblTotAmt + clsCommon.myCdbl(gv1.Rows(ii).Cells(colAmt).Value)
            End If
        Next
        lblDocumentAmt.Text = clsCommon.myFormat(dblTotAmt)
    End Sub

    Private Sub btnAddNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddNew.Click
        AddNew()
    End Sub

    Sub AddNew()
        BlankAllControls()
        gv1.ReadOnly = False
        isNewEntry = True
        btnSave.Text = "Save"
        btnSave.Enabled = True
        btnPost.Enabled = True
        btnDelete.Enabled = True
        txtDate.Focus()
        LoadBlankGrid()
        gv1.Rows.AddNew()
        RadLabel2.Text = "Vendor No"
        HideUnhideColumn()
        txtModeOfTransport.Text = ""
        txtCashMemoDetail.Text = ""
        chlCust.Checked = False
        ''richa Ticket No BM00000003061 on 01/08/2014
        chkAgainstJobWork.Checked = False
        '-------------------------------------------
        '' Anubhooti 07-Oct-2014 BM00000003663
        ChkRejLoc.Checked = False
        '' Anubhooti 10-Dec-2014 BM00000003662
        CmbItemConType.DataSource = GetItemType()
        CmbItemConType.DisplayMember = "Name"
        CmbItemConType.ValueMember = "Code"
        CmbItemConType.SelectedValue = "N"
        ''
        gv1.Columns(colRejSRNQty).IsVisible = False
        cboDocType.SelectedValue = "RGP"
        GrpRej.Enabled = False
        GrpRej.Visible = False
        ChkRejLoc.Visible = False
        txtSRNNo.Value = ""
        ''For Custom Fields
        If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
            UcCustomFields1.SetDefaultValues()
        End If
        txtVendorNo.Enabled = True
        ''End of For Custom Fields
        UcAttachment1.BlankAllControls()
        lblNrgpReq.Visible = False
        nrgpReqFnd.Visible = False
        chkAgainstCCTransfer.Visible = False
        'lblToLocation.Visible = False
        'fndToLocationCode.Visible = False
        'lblToLocationCode.Visible = False
        'fndToLocationCode.Value = ""
        'RadLabel2.Visible = True
        'txtVendorNo.Visible = True
        'lblVendorName.Visible = True
        'txtVendorNo.Value = ""
        If chkNonInventoryItem.Checked Then
            chkNonInventoryItem.Checked = True
            chkAgainstCCTransfer.Visible = True
        Else
            chkNonInventoryItem.Checked = False
            chkAgainstCCTransfer.Visible = False
        End If
        If chkNonInventoryItem.Checked AndAlso chkAgainstCCTransfer.Checked Then
            chkAgainstCCTransfer.Checked = True
            fndToLocationCode.Value = ""
            lblToLocationCode.Text = ""
            lblToLocation.Visible = True
            fndToLocationCode.Visible = True
            lblToLocationCode.Visible = True
        Else
            chkAgainstCCTransfer.Checked = False
            fndToLocationCode.Value = ""
            lblToLocationCode.Text = ""

            lblToLocation.Visible = False
            fndToLocationCode.Visible = False
            lblToLocationCode.Visible = False

        End If
        AllowDepartmentMandatoryOnPurchaseCycle()
    End Sub

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


    Function AllowToSave() As Boolean
        Try
            '= KUNAL > TICKET : BM00000009580 ======
            If AllowFutureDateTransaction(txtDate.Value, Nothing) = False Then
                txtDate.Focus()
                Return False
            End If

            Dim MainLoc As String = ""
            Dim SRNBillLoc As String = ""

            If btnSave.Text = "Update" Then
                Dim strchk As String = "select Status from TSPL_RGP_HEAD where RGP_No='" + txtDocNo.Value + "'"
                Dim chkpost As String = clsDBFuncationality.getSingleValue(strchk)
                If chkpost = "1" Then
                    Throw New Exception("Transaction already posted")
                    Return False
                End If
            End If

            UpdateAllTotals()

            If clsCommon.myLen(txtVendorNo.Value) <= 0 AndAlso chkthirdparty.Checked AndAlso (clsCommon.CompairString(cboDocType.SelectedValue, "NRGP") = CompairStringResult.Equal Or clsCommon.CompairString(cboDocType.SelectedValue, "NRGPR") = CompairStringResult.Equal) Then

            Else
                If chkNonInventoryItem.Checked AndAlso chkAgainstCCTransfer.Checked Then
                Else
                    txtVendorNo.Focus()
                    If chlCust.Checked Then
                        If clsCommon.myLen(txtVendorNo.Value) <= 0 Then
                            Throw New Exception("Please Enter Customer.")
                            Return False
                        End If
                    Else
                        If clsCommon.myLen(txtVendorNo.Value) <= 0 Then
                            Throw New Exception("Please Enter Vendor.")
                            Return False
                        End If
                    End If
                End If
            End If
            If Not isNewEntry AndAlso clsCommon.myLen(txtDocNo.Value) <= 0 Then
                txtDocNo.Focus()
                Throw New Exception("SRN No Not found to save")
                Return False
            End If
            If clsCommon.myLen(cboDocType.SelectedValue) <= 0 Then
                cboDocType.Focus()
                Throw New Exception("Please select Document Type")
                Return False
            End If
            If clsCommon.myLen(txtLocation.Value) <= 0 Then
                txtLocation.Focus()
                Throw New Exception("Please Enter Location")
                Return False
            End If
            If clsCommon.myLen(txtDeliveredBy.Value) <= 0 Then
                txtDeliveredBy.Focus()
                Throw New Exception("Please Enter Deliverd By")
                Return False
            End If

            If chkjvdisplaytype.Checked AndAlso chkAgainstJobWork.Checked Then
                chkjvdisplaytype.Focus()
                Throw New Exception("Tansaction can't be Against job work Or dispaly Type.Please select one type")
            End If
            If chkNonInventoryItem.Checked AndAlso chkAgainstCCTransfer.Checked AndAlso clsCommon.myLen(clsCommon.myCstr(fndToLocationCode.Value)) <= 0 Then
                fndToLocationCode.Focus()
                Throw New Exception("Please Select To Location")
            End If

            '' Anubhooti 09-Oct-2014 BM00000003663
            If clsCommon.myLen(txtSRNNo.Value) > 0 AndAlso clsCommon.myLen(txtLocation.Value) > 0 Then
                MainLoc = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select ISNULL(Location_Code ,'') As Location_Code From TSPL_LOCATION_MASTER Where Rejected_Location= '" & clsCommon.myCstr(txtLocation.Value) & "'"))
                SRNBillLoc = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select ISNULL(Bill_To_Location ,'') As Bill_To_Location From TSPL_SRN_HEAD Where SRN_No= '" & clsCommon.myCstr(txtSRNNo.Value) & "'"))
                If clsCommon.CompairString(MainLoc, SRNBillLoc) <> CompairStringResult.Equal Then
                    txtSRNNo.Focus()
                    Throw New Exception("Please check ! main location of rejected location does not exists in SRN no (" + clsCommon.myCstr(txtSRNNo.Value) + ")")
                    Return False
                End If
            End If

            If ChkRejLoc.Checked = True AndAlso clsCommon.myLen(txtSRNNo.Value) <= 0 Then
                txtSRNNo.Focus()
                Throw New Exception("Please select SRN No.")
                Return False
            End If
            ''

            '=============================================

            Dim arrPONo As New List(Of String)
            Dim arrSchNo As New List(Of String)
            Dim arrBOMIcode As List(Of String) = Nothing
            arrBOMIcode = New List(Of String)

            If chkAgainstJobWork.Checked AndAlso IsRGPAfterPO Then
                gv_PO.Focus()
                gv_PO.Select()
                RadPageView2.SelectedPage = RadPageViewPage3

                For ii As Integer = 0 To gv_PO.Rows.Count - 1
                    Dim icode As String = clsCommon.myCstr(gv_PO.Rows(ii).Cells(colBOMIcode).Value)
                    Dim Bomcode As String = clsCommon.myCstr(gv_PO.Rows(ii).Cells(colBOMCode).Value)
                    Dim unitcode As String = clsCommon.myCstr(gv_PO.Rows(ii).Cells(colBOMIUnit).Value)
                    Dim pono As String = clsCommon.myCstr(gv_PO.Rows(ii).Cells(colBOMPONo).Value)
                    Dim schno As String = clsCommon.myCstr(gv_PO.Rows(ii).Cells(colBOMScheduleNo).Value)

                    If clsCommon.myLen(pono) > 0 AndAlso Not (arrPONo.Contains(pono)) Then
                        arrPONo.Add(pono)
                    End If
                    If clsCommon.myLen(schno) > 0 AndAlso Not (arrSchNo.Contains(schno)) Then
                        arrSchNo.Add(schno)
                    End If

                    If clsCommon.myLen(icode) > 0 AndAlso Not arrBOMIcode.Contains(icode) Then
                        arrBOMIcode.Add(icode)
                    End If

                    If chkAsPerBOM.Checked AndAlso IsRGPAfterPO AndAlso chkAgainstJobWork.Checked AndAlso clsCommon.myLen(Bomcode) <= 0 AndAlso clsCommon.myLen(icode) > 0 Then
                        gv_PO.CurrentRow = gv_PO.Rows(ii)
                        Throw New Exception("Select bom detail at row no. " + clsCommon.myCstr(ii + 1) + "")
                    End If
                    'If clsCommon.myLen(icode) > 0 AndAlso clsCommon.myLen(Bomcode) <= 0 Then
                    '    gv_PO.CurrentRow = gv_PO.Rows(ii)
                    '    Throw New Exception("Select BOM detail at row no. " + clsCommon.myCstr(ii + 1) + "")
                    'End If

                    If clsCommon.myLen(icode) > 0 AndAlso clsCommon.myCdbl(gv_PO.Rows(ii).Cells(colBOMQty).Value) <= 0 Then
                        gv_PO.CurrentRow = gv_PO.Rows(ii)
                        Throw New Exception("Fill quantity at row no. " + clsCommon.myCstr(ii + 1) + "")
                    End If

                    If clsCommon.myLen(icode) > 0 AndAlso clsCommon.myLen(unitcode) <= 0 Then
                        gv_PO.CurrentRow = gv_PO.Rows(ii)
                        Throw New Exception("Select item unit at row no. " + clsCommon.myCstr(ii + 1) + "")
                    End If

                    If clsCommon.myLen(icode) > 0 Then
                        For jj As Integer = ii + 1 To gv_PO.Rows.Count - 1
                            Dim oldicode As String = clsCommon.myCstr(gv_PO.Rows(jj).Cells(colBOMIcode).Value)
                            Dim oldBomcode As String = clsCommon.myCstr(gv_PO.Rows(jj).Cells(colBOMCode).Value)
                            Dim oldunitcode As String = clsCommon.myCstr(gv_PO.Rows(jj).Cells(colBOMIUnit).Value)
                            Dim oldpono As String = clsCommon.myCstr(gv_PO.Rows(jj).Cells(colBOMPONo).Value)
                            Dim oldschno As String = clsCommon.myCstr(gv_PO.Rows(jj).Cells(colBOMScheduleNo).Value)

                            If clsCommon.CompairString(Bomcode, oldBomcode) = CompairStringResult.Equal AndAlso clsCommon.CompairString(icode, oldicode) = CompairStringResult.Equal AndAlso clsCommon.CompairString(unitcode, oldunitcode) = CompairStringResult.Equal AndAlso clsCommon.CompairString(pono, oldpono) = CompairStringResult.Equal AndAlso clsCommon.CompairString(schno, oldschno) = CompairStringResult.Equal Then
                                gv_PO.CurrentRow = gv_PO.Rows(jj)
                                Throw New Exception("Duplicate data found at row no. " + clsCommon.myCstr(jj + 1) + "")
                            End If
                        Next
                    End If
                Next

                clsPurchaseOrderHead.IsValidVendorForPO(arrPONo, txtVendorNo.Value)
                clsPurchaseSchedule.IsValidVendorForSchedule(arrSchNo, txtVendorNo.Value)
                clsPurchaseOrderHead.IsValidRepairForPO(arrPONo, chkRepair.Checked)
            End If

            If chkAgainstJobWork.Checked AndAlso IsRGPAfterPO AndAlso (Not chkAsPerBOM.Checked) AndAlso (arrBOMIcode Is Nothing OrElse arrBOMIcode.Count <= 0) Then
                gv_PO.CurrentRow = gv_PO.Rows(0)
                RadPageView2.SelectedPage = RadPageViewPage3
                Throw New Exception("Fill receiving item in grid.")
            End If
            '================================================

            Dim strICode As String = ""
            Dim strIName As String = ""
            Dim dblQty As Double = 0
            Dim strUOM As String = ""
            Dim mrpmandatory As String = ""
            Dim mrp As Decimal = 0.0
            Dim po_sch_qty As Decimal = 0
            Dim po_no As String = ""
            Dim sch_no As String = ""

            For ii As Integer = 0 To gv1.Rows.Count - 1
                gv1.Focus()
                gv1.Select()
                RadPageView2.SelectedPage = RadPageViewPage2

                strICode = clsCommon.myCstr(gv1.Rows(ii).Cells(colICode).Value)
                strIName = clsCommon.myCstr(gv1.Rows(ii).Cells(colIName).Value)
                dblQty = clsCommon.myCdbl(gv1.Rows(ii).Cells(colQty).Value)
                strUOM = clsCommon.myCstr(gv1.Rows(ii).Cells(colUnit).Value)
                mrpmandatory = clsCommon.myCstr(gv1.Rows(ii).Cells(colmrpmmandatory).Value)
                mrp = clsCommon.myCdbl(gv1.Rows(ii).Cells(colmrp).Value)
                po_sch_qty = clsCommon.myCdbl(gv1.Rows(ii).Cells(colPO_Sch_Qty).Value)
                po_no = clsCommon.myCstr(gv1.Rows(ii).Cells(colPurchaseOrderNo).Value)
                sch_no = clsCommon.myCstr(gv1.Rows(ii).Cells(colScheduleNo).Value)



                If clsCommon.myLen(strICode) > 0 Then
                    If clsCommon.myLen(strUOM) <= 0 Then
                        Throw New Exception("Please enter UOM of Item - " + strICode + ".At Line No: " + clsCommon.myCstr(clsCommon.myCdbl(ii + 1)) + " ")
                    End If
                    ''For RM Other balance Qty check And works only for one unit.
                    Dim dblOuterConvFac As Double = clsItemMaster.GetConvertionFactor(strICode, strUOM, Nothing)
                    Dim dblBalQty As Double = clsItemLocationDetails.getBalanceWithUnapproveForRMOther(strICode, txtLocation.Value, txtDocNo.Value, txtDate.Value, Nothing, strUOM)
                    Dim dblEnteredQty As Double = dblQty
                    For jj As Integer = 0 To gv1.Rows.Count - 1
                        If ii = jj Then
                            Continue For
                        End If
                        Dim dblInnerConvFac As Double = clsItemMaster.GetConvertionFactor(clsCommon.myCstr(gv1.Rows(jj).Cells(colICode).Value), clsCommon.myCstr(gv1.Rows(jj).Cells(colUnit).Value), Nothing)
                        If clsCommon.myCdbl(gv1.Rows(jj).Cells(colQty).Value) > 0 AndAlso clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(jj).Cells(colICode).Value), strICode) = CompairStringResult.Equal Then
                            dblEnteredQty += clsCommon.myCdbl(gv1.Rows(jj).Cells(colQty).Value)
                        End If
                    Next
                    dblEnteredQty = Math.Round(dblEnteredQty, 2, MidpointRounding.ToEven)

                    If clsCommon.CompairString(cboDocType.SelectedValue, "RGPR") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(cboDocType.SelectedValue, "NRGPR") <> CompairStringResult.Equal Then
                        If dblEnteredQty > dblBalQty And chkNonInventoryItem.Checked = False Then
                            Throw New Exception("Item - " + strICode + Environment.NewLine + "Entered Quantity - " + clsCommon.myCstr(dblEnteredQty) + " and Balance Quantity - " + clsCommon.myCstr(dblBalQty))
                        End If
                    End If

                    If clsCommon.myLen(po_no) > 0 AndAlso dblQty > po_sch_qty Then
                        RadPageView1.SelectedPage = RadPageViewPage1
                        gv1.CurrentRow = gv1.Rows(ii)
                        Throw New Exception("Filled quantity cannot be more than PO Qty at row no. " + clsCommon.myCstr(ii + 1) + "")
                    End If
                    If clsCommon.myLen(sch_no) > 0 AndAlso dblQty > po_sch_qty Then
                        RadPageView1.SelectedPage = RadPageViewPage1
                        gv1.CurrentRow = gv1.Rows(ii)
                        Throw New Exception("Filled quantity cannot be more than Schedule Qty at row no. " + clsCommon.myCstr(ii + 1) + "")
                    End If

                    If clsCommon.myLen(strICode) > 0 AndAlso chkAsPerBOM.Checked Then
                        If dblQty <= 0 AndAlso chkAsPerBOM.Checked Then
                            RadPageView1.SelectedPage = RadPageViewPage1
                            gv1.CurrentRow = gv1.Rows(ii)
                            Throw New Exception("Fill quantity at row no. " + clsCommon.myCstr(ii + 1) + "")
                        End If

                        For jj As Integer = ii + 1 To gv1.Rows.Count - 1
                            If clsCommon.CompairString(strICode, clsCommon.myCstr(gv1.Rows(jj).Cells(colICode).Value)) = CompairStringResult.Equal Then
                                RadPageView1.SelectedPage = RadPageViewPage1
                                gv1.CurrentRow = gv1.Rows(jj)
                                Throw New Exception("Duplicate item at row no. " + clsCommon.myCstr(jj + 1) + "")
                            End If
                        Next
                    End If
                End If

                '------when third party location checked,then it check for item mrp is enable,if enable then MRP rate should not be 0(zero)------------------------------------------------------------------------
                If chkthirdparty.Checked Then
                    If (clsCommon.CompairString(mrpmandatory, "1") = CompairStringResult.Equal) AndAlso mrp <= 0 Then
                        Throw New Exception("Please Fill MRP Of Item " + strIName + " At Line No: " + clsCommon.myCstr(clsCommon.myCdbl(ii + 1)) + "")
                    End If
                End If
                '--------------------------------------------------------------
                If clsCommon.CompairString(cboDocType.SelectedValue, "NRGPR") = CompairStringResult.Equal Then
                    If clsCommon.myCdbl(gv1.Rows(ii).Cells(colRetQty).Value) > dblQty Then
                        Throw New Exception("Line No: " + clsCommon.myCstr(clsCommon.myCdbl(ii + 1)) + "  -  Return Quantity can not be more than Quantity")
                    End If
                End If
                '' Anubhooti 09-Oct-2014 BM00000003663
                Dim RejQty As Double = clsCommon.myCdbl(gv1.Rows(ii).Cells(colRejSRNQty).Value)
                Dim Qty As Double = clsCommon.myCdbl(gv1.Rows(ii).Cells(colQty).Value)
                Dim strItemName As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colIName).Value)
                If ChkRejLoc.Checked = True Then
                    If RejQty < 0 Then
                        common.clsCommon.MyMessageBoxShow("Rejected quantity can not be negative for item : " + strICode + " . at line no" + clsCommon.myCstr(ii + 1))
                        Return False
                    End If
                    If Qty > RejQty Then
                        common.clsCommon.MyMessageBoxShow("Item " + strICode + "( " + strItemName.Trim() + " ) Entered Quantity(" + clsCommon.myCstr(Qty) + ") Can't be more than rejected Quantity(" + clsCommon.myCstr(RejQty) + ") at line no: " + clsCommon.myCstr(clsCommon.myCdbl(ii + 1)) + " ")
                        Return False
                    End If
                End If

                ''Serial ITem

                If clsCommon.myCBool(gv1.Rows(ii).Cells(colIsSerialseItem).Value) Then
                    Dim arrSerailNo As List(Of clsSerializeInvenotry) = TryCast(gv1.Rows(ii).Tag, List(Of clsSerializeInvenotry))
                    If arrSerailNo Is Nothing OrElse dblQty <> arrSerailNo.Count Then
                        Throw New Exception("Please provide serial no for item : " + strICode + " . At Line No" + clsCommon.myCstr(ii + 1))
                    End If
                End If
                If dblQty > 0 AndAlso clsCommon.myCBool(clsDBFuncationality.getSingleValue("select TSPL_ITEM_MASTER.Is_Batch_Item  from TSPL_ITEM_MASTER where TSPL_ITEM_MASTER.Item_Code ='" + clsCommon.myCstr(gv1.Rows(ii).Cells(colICode).Value) + "'", Nothing)) Then
                    If RunBatchFifowise = 1 Then
                        gv1.CurrentRow = gv1.Rows(ii)
                        OpenBatchItem()
                    End If
                    Dim arrBatchNo As List(Of clsBatchInventory) = TryCast(gv1.Rows(ii).Cells(colICode).Tag, List(Of clsBatchInventory))
                    If arrBatchNo Is Nothing Then
                        Throw New Exception("Please provide Batch no for item : " + strICode + " . At Line No" + clsCommon.myCstr(ii + 1))
                    Else
                        Dim tQty As Decimal = 0
                        For Each objBatch As clsBatchInventory In arrBatchNo
                            tQty += objBatch.Qty
                        Next
                        If tQty <> dblQty Then
                            Throw New Exception("Item : " + strICode + " Entered Qty " + clsCommon.myCstr(dblQty) + Environment.NewLine + "And Batchwise Qty " + clsCommon.myCstr(tQty) + " . At Line No" + clsCommon.myCstr(ii + 1))
                        End If
                    End If
                End If
            Next

            '------------------------------------------------
            If chkthirdparty.Checked AndAlso clsCommon.myLen(txtsrnlocation_code.Value) <= 0 AndAlso cboDocType.SelectedIndex = 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please Select Location For SRN Entry", Me.Text)
                txtsrnlocation_code.Focus()
                txtsrnlocation_code.Select()
                Return False
            End If

            UcCustomFields1.AllowToSave()
            UcAttachment1.AllowToSave()

            Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            Return False
        End Try
    End Function
    Private Sub btnUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        Try
            If clsCommon.myLen(txtDocNo.Value) > 0 Then
                Dim obj As New clsRGPHead
                obj.RGP_No = clsCommon.myCstr(txtDocNo.Value)
                obj.GRNo = txtGRNo.Text
                obj.Road_Permit_No = txtRoadPermitNo.Text
                If txtGRDate.Checked Then
                    obj.GR_Date = txtGRDate.Value
                End If
                If txtRoadPermitDate.Checked Then
                    obj.RoadPermit_Date = txtRoadPermitDate.Value
                End If
                If clsRGPHead.UpdateAfterPosting(obj, Nothing) Then
                    clsCommon.MyMessageBoxShow(Me, "Information updated successfully.", Me.Text)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        SaveData(False)
    End Sub

    Sub SaveData(ByVal ChekBtnPost As Boolean)
        Try
            '' Anubhooti 13-Sep-2014 BM00000003735
            If ChekBtnPost = False Then
                If FrmMainTranScreen.ValidateTransactionAccToFinYear("RGP/NRGP", txtDate.Value) = False Then
                    Exit Sub
                End If
            End If
            ''
            If (AllowToSave()) Then
                Dim obj As New clsRGPHead()

                obj.chklocstion = "N"
                obj.srnlocation = ""
                If chkthirdparty.Checked Then
                    obj.chklocstion = "Y"
                    obj.srnlocation = txtsrnlocation_code.Value
                End If
                If txtGRDate.Checked Then
                    obj.GR_Date = txtGRDate.Value
                End If
                If txtRoadPermitDate.Checked Then
                    obj.RoadPermit_Date = txtRoadPermitDate.Value
                End If
                obj.GRNo = txtGRNo.Text
                obj.Road_Permit_No = txtRoadPermitNo.Text

                obj.RGP_No = txtDocNo.Value
                obj.RGP_Date = txtDate.Value
                obj.Doc_Type = clsCommon.myCstr(cboDocType.SelectedValue)
                If (cboDocType.SelectedValue = "NRGP" Or cboDocType.SelectedValue = "NRGPR") Then
                    obj.Against_Sale = clsCommon.myCdbl(chkAgainst_Sale.Checked)
                    If chkthirdparty.Checked Then
                        obj.invoiceNo = clsCommon.myCstr(txtInvoiceNO.Text)
                        obj.partyName = clsCommon.myCstr(txtPartyName.Text)
                        obj.partyAddress = clsCommon.myCstr(txtPartyAddress.Text)
                        obj.DispatchDate = clsCommon.GetPrintDate(dtpDispatchDate.Value, "dd/MMM/yyyy")
                    End If
                End If
                ''richa Ticket No BM00000003061 on 01/08/2014
                obj.Against_JobWork = clsCommon.myCdbl(chkAgainstJobWork.Checked)
                '-------------------------------------------
                obj.JVDisplayType = clsCommon.myCdbl(chkjvdisplaytype.Checked)
                obj.Mode_Of_Transport = txtModeOfTransport.Text
                obj.Cash_Memo_Detail = txtCashMemoDetail.Text
                obj.Vendor_Code = txtVendorNo.Value
                obj.Vendor_Name = lblVendorName.Text
                obj.VehicleNo = txtVehicleNo.Text
                obj.GPNo = txtGPNo.Text
                obj.GPDate = txtGPDate.Value
                obj.Remarks = txtRemarks.Text
                obj.Reason = txtReason.Text
                obj.On_Hold = chkOnHold.Checked
                obj.Document_Amount = clsCommon.myCdbl(lblDocumentAmt.Text)
                obj.Location = txtLocation.Value
                obj.Delivered_By = txtDeliveredBy.Value
                obj.Department = txtDepartment.Value
                obj.Billing = clsCommon.myCstr(ddlBilling.SelectedValue)
                obj.CostCentre = fndCostCentre.Value
                obj.CostCentreDesc = txtCostCentre.Text
                obj.Against_Customer = clsCommon.myCdbl(chlCust.Checked)
                If clsCommon.CompairString(cboDocType.SelectedValue, "RGPR") = CompairStringResult.Equal Then
                    obj.Against_RGP = clsCommon.myCstr(txtNRGPNo.Value)
                Else
                    obj.Against_NRGP = clsCommon.myCstr(txtNRGPNo.Value)
                End If
                obj.AMC_Ref_No = txtAMCRefNo.Text

                ' obj.Against_NRGP = clsCommon.myCstr(txtNRGPNo.Value)
                If chkNonInventoryItem.Checked Then
                    obj.Is_Non_Inventory = 1
                End If
                If chkNonInventoryItem.Checked AndAlso chkAgainstCCTransfer.Checked Then
                    obj.Is_Against_CC_Transfer = 1
                    obj.To_Location_Code = clsCommon.myCstr(fndToLocationCode.Value)
                End If
                '' Anubhooti 09-Oct-2014 BM00000003663
                If ChkRejLoc.Checked = True Then
                    obj.Is_Rejected = 1
                Else
                    obj.Is_Rejected = 0
                End If
                If clsCommon.myLen(txtSRNNo.Value) > 0 Then
                    obj.SRN_No = clsCommon.myCstr(txtSRNNo.Value)
                End If
                ''
                '' Anubhooti 10-Dec-2014 BM00000003662
                obj.Item_Conversion_Type = clsCommon.myCstr(CmbItemConType.SelectedValue)
                ''
                obj.Against_Schedule_Code = clsCommon.myCstr(txtScheduleNo.Value)
                obj.PO_Id = clsCommon.myCstr(txtPoNo.Value)
                obj.Against_As_It_Is = IIf(clsCommon.myCstr(txtAsitis.Text) = "Y", 1, 0)
                obj.Against_BOM = IIf(chkAsPerBOM.Checked, 1, 0)
                '===========Added by preeti Gupta Against ticket no[BHA/29/05/18-000039]==
                obj.Man_PO_Id = txtManPoNo.Text
                obj.Man_PO_Date = dtManPoDate.Value
                '==================================
                obj.Is_Repair = chkRepair.Checked
                obj.Arr = New List(Of clsRGPDetail)
                For Each grow As GridViewRowInfo In gv1.Rows
                    Dim objTr As New clsRGPDetail()
                    objTr.Item_MRP = clsCommon.myCdbl(grow.Cells(colmrp).Value)
                    objTr.Line_No = clsCommon.myCdbl(grow.Cells(colLineNo).Value)
                    objTr.Item_Code = clsCommon.myCstr(grow.Cells(colICode).Value)
                    objTr.Item_Desc = clsCommon.myCstr(grow.Cells(colIName).Value)
                    If clsCommon.CompairString(cboDocType.SelectedValue, "NRGPR") = CompairStringResult.Equal Then
                        objTr.RGP_Qty = clsCommon.myCdbl(grow.Cells(colRetQty).Value)
                    Else
                        objTr.RGP_Qty = clsCommon.myCdbl(grow.Cells(colQty).Value)
                    End If
                    objTr.Unit_code = clsCommon.myCstr(grow.Cells(colUnit).Value)
                    objTr.Item_Cost = clsCommon.myCdbl(grow.Cells(colRate).Value)
                    objTr.Amount = clsCommon.myCdbl(grow.Cells(colAmt).Value)
                    objTr.Last_RGP_No = clsCommon.myCstr(grow.Cells(colLastRGPNRGP).Value)
                    objTr.Last_RGP_Date = clsCommon.myCstr(grow.Cells(colDate).Value)
                    objTr.Specification = clsCommon.myCstr(grow.Cells(colSp).Value)
                    objTr.Against_Schedule_Code = clsCommon.myCstr(grow.Cells(colScheduleNo).Value)
                    objTr.PO_Id = clsCommon.myCstr(grow.Cells(colPurchaseOrderNo).Value)
                    objTr.PO_Sch_Qty = clsCommon.myCdbl(grow.Cells(colPO_Sch_Qty).Value)
                    objTr.Main_PO_Icode = clsCommon.myCstr(grow.Cells(colMainPOItem).Value)
                    objTr.BOM_Code = clsCommon.myCstr(grow.Cells(colMainBOMCode).Value)
                    '' Anubhooti 06-Feb-2015(Unit Cost)
                    objTr.Approx_Cost = clsCommon.myCdbl(grow.Cells(colApproxCost).Value)
                    objTr.arrSrItem = TryCast(grow.Tag, List(Of clsSerializeInvenotry))
                    objTr.arrBatchItem = TryCast(grow.Cells(colICode).Tag, List(Of clsBatchInventory))  ' Change By prabhakar
                    If clsCommon.myLen(objTr.Item_Code) > 0 Then
                        obj.Arr.Add(objTr)
                    End If
                Next
                If (obj.Arr Is Nothing OrElse obj.Arr.Count <= 0) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Please Fill at list one Item", Me.Text)
                    Return
                End If

                '=======================================================
                obj.Arr_BOM = New List(Of clsRGPBOMItem)
                For Each grow As GridViewRowInfo In gv_PO.Rows
                    Dim objtr As New clsRGPBOMItem()

                    objtr.Line_No = clsCommon.myCstr(grow.Cells(colBOMLineNo).Value)
                    objtr.BOM_Code = clsCommon.myCstr(grow.Cells(colBOMCode).Value)
                    objtr.Item_Code = clsCommon.myCstr(grow.Cells(colBOMIcode).Value)
                    objtr.Unit_Code = clsCommon.myCstr(grow.Cells(colBOMIUnit).Value)
                    objtr.RGP_Qty = clsCommon.myCdbl(grow.Cells(colBOMQty).Value)
                    objtr.Balance_Qty = clsCommon.myCdbl(grow.Cells(colBOMBalanceQty).Value)
                    objtr.Rate = clsCommon.myCdbl(grow.Cells(colBOMRate).Value)
                    objtr.MRP = clsCommon.myCdbl(grow.Cells(colBOMMRP).Value)
                    objtr.Amount = clsCommon.myCdbl(grow.Cells(colBOMAmt).Value)
                    objtr.Specification = clsCommon.myCstr(grow.Cells(colBOMSpecification).Value).Replace("'", "`")
                    If clsCommon.myLen(objtr.Specification) > 200 Then
                        objtr.Specification = objtr.Specification.Substring(0, 200)
                    End If
                    objtr.PO_Id = clsCommon.myCstr(grow.Cells(colBOMPONo).Value)
                    objtr.Against_Schedule_Code = clsCommon.myCstr(grow.Cells(colBOMScheduleNo).Value)
                    objtr.Module_Id = clsCommon.myCstr(grow.Cells(colBOMModule).Value)

                    If clsCommon.myLen(objtr.Item_Code) > 0 Then
                        obj.Arr_BOM.Add(objtr)
                    End If
                Next

                '======================================================

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
                    UcAttachment1.SaveData(obj.RGP_No)
                    If ChekBtnPost = False Then
                        common.clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                    End If

                    LoadData(obj.RGP_No, NavigatorType.Current, False)
                End If
            End If
        Catch ex As Exception
            If Not ChekBtnPost Then
                common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            Else
                Throw New Exception(ex.Message)
            End If

        End Try
    End Sub

    Function AutoSRNFromRGP() As Boolean 'By Monika
        Dim obj As New clsSRNHead()
        Dim objRGP As New clsRGPHead()
        Try
            objRGP = clsRGPHead.GetData(txtDocNo.Value, NavigatorType.Current)

            If objRGP IsNot Nothing AndAlso clsCommon.myLen(objRGP.RGP_No) > 0 Then
                obj.VehicleNo = objRGP.VehicleNo
                obj.Bill_To_Location = objRGP.srnlocation
                obj.BillToLocationName = objRGP.srnlocationdesc
                obj.is_RGP_Non_Inventory = IIf(objRGP.Is_Non_Inventory = 0, False, True)
                obj.autosrnfromrgp = "Auto SRN From RGP(Third Party Location)"
                obj.SRN_Date = clsCommon.GETSERVERDATE()
                obj.Challan_Date = clsCommon.GETSERVERDATE()
                obj.Inv_Date = clsCommon.GETSERVERDATE()
                obj.Against_RGP = objRGP.RGP_No
                obj.Item_Type = objRGP.ItemType
                obj.PurchaseOrder_Type = "J"

                Dim qry As String = "select TSPL_LOCATION_MASTER.vendor_code,TSPL_VENDOR_MASTER.Vendor_Name,TSPL_VENDOR_MASTER.Terms_Code,TSPL_VENDOR_MASTER.Terms_Code_Desc ,TSPL_VENDOR_MASTER.Vendor_Account ,TSPL_VENDOR_MASTER.Tax_Group,TSPL_VENDOR_MASTER.Tax_Group_Desc from TSPL_LOCATION_MASTER left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_LOCATION_MASTER.vendor_code WHERE TSPL_LOCATION_MASTER.location_code='" + objRGP.Location + "'"
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

                If dt IsNot Nothing AndAlso dt IsNot DBNull.Value AndAlso dt.Rows.Count > 0 Then
                    obj.Vendor_Code = clsCommon.myCstr(dt.Rows(0)("vendor_code"))
                    obj.Vendor_Name = clsCommon.myCstr(dt.Rows(0)("Vendor_Name"))
                    obj.Terms_Code = clsCommon.myCstr(dt.Rows(0)("Terms_Code"))
                    obj.TermsName = clsCommon.myCstr(dt.Rows(0)("Terms_Code_Desc"))
                    obj.Tax_Group = clsCommon.myCstr(dt.Rows(0)("Tax_Group"))
                    obj.TaxGroupName = clsCommon.myCstr(dt.Rows(0)("Tax_Group_Desc"))

                    qry = "select currency_code from TSPL_VENDOR_MASTER where VENDOR_CODE='" & obj.Vendor_Code & "'"
                    obj.CURRENCY_CODE = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
                    obj.ConvRate = clsEXSalesOrder.GetCurrencyRate(obj.CURRENCY_CODE)
                    Dim applicabledate As String = clsEXSalesOrder.GetCurrencyApplydate(obj.CURRENCY_CODE, clsCommon.GETSERVERDATE())

                    If clsCommon.myLen(applicabledate) > 0 Then
                        obj.ApplicableFrom = clsCommon.GetPrintDate(applicabledate, "dd/MMM/yyyy")
                    Else
                        obj.ApplicableFrom = Nothing
                    End If
                End If

                obj.Arr = New List(Of clsSRNDetail)

                If objRGP.Arr IsNot Nothing AndAlso objRGP.Arr.Count > 0 Then
                    For Each objRGPDet As clsRGPDetail In objRGP.Arr
                        Dim objtr As New clsSRNDetail()

                        objtr.Line_No = objRGPDet.Line_No
                        objtr.Row_Type = "Item"
                        objtr.RGP_Id = objRGP.RGP_No
                        objtr.Item_Code = objRGPDet.Item_Code
                        objtr.Item_Desc = objRGPDet.Item_Desc
                        objtr.Item_Cost = objRGPDet.Item_Cost
                        objtr.Unit_code = objRGPDet.Unit_code
                        objtr.SRN_Qty = objRGPDet.RGP_Qty
                        objtr.MRP = objRGPDet.Item_MRP

                        obj.Arr.Add(objtr)
                    Next
                End If
            End If

            If obj IsNot Nothing Then
                If obj.SaveData(obj, True) Then
                    Return True
                End If
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            obj = Nothing
            objRGP = Nothing
        End Try
    End Function
    Sub EnabledDisabledControl()
        If clsCommon.CompairString(clsCommon.myCstr(cboDocType.SelectedValue), "NRGPR") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(cboDocType.SelectedValue), "RGPR") = CompairStringResult.Equal Then
            txtSRNNo.Enabled = False
            txtLocation.Enabled = False
            txtVendorNo.Enabled = False
            txtDeliveredBy.Enabled = False
            txtDepartment.Enabled = False
            txtGRNo.Enabled = False
            txtGPDate.Enabled = False
            txtGPNo.Enabled = False
        Else
            txtSRNNo.Enabled = True
            txtLocation.Enabled = True
            txtVendorNo.Enabled = True
            txtDeliveredBy.Enabled = True
            txtDepartment.Enabled = True
            txtGRNo.Enabled = True
            txtGPDate.Enabled = True
            txtGPNo.Enabled = True

        End If
    End Sub

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType, ByVal IsRefDoc As Boolean)

        Try
            isInsideLoadData = False
            btnSave.Enabled = True
            btnSave.Text = "Save"
            btnPost.Enabled = False
            btnDelete.Enabled = False
            isNewEntry = True
            txtPoNo.Enabled = False
            dtPoDate.Enabled = False
            txtScheduleNo.Enabled = False
            BlankAllControls()
            LoadBlankGrid()
            btnSameasAbove.Enabled = True

            gv_PO.Rows.Clear()
            gv_PO.Rows.AddNew()
            RadPageViewPage3.Item.Visibility = ElementVisibility.Collapsed  'RadGroupBox3.Visible = False
            RadPageView2.SelectedPage = RadPageViewPage2

            Dim obj As New clsRGPHead()
            obj = clsRGPHead.GetData(strCode, NavTyep)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.RGP_No) > 0) Then
                isInsideLoadData = True
                btnSave.Enabled = True
                btnPost.Enabled = True
                btnDelete.Enabled = True
                isNewEntry = False
                If obj.Status = ERPTransactionStatus.Approved And Not IsRefDoc Then
                    btnSave.Enabled = False
                    btnPost.Enabled = False
                    btnDelete.Enabled = False
                End If
                If obj.GR_Date IsNot Nothing Then
                    txtGRDate.Value = obj.GR_Date
                    txtGRDate.Checked = True
                End If
                If obj.RoadPermit_Date IsNot Nothing Then
                    txtRoadPermitDate.Value = obj.RoadPermit_Date
                    txtRoadPermitDate.Checked = True
                End If
                If Not IsRefDoc Then
                    cboDocType.SelectedValue = obj.Doc_Type
                End If
                EnabledDisabledControl()
                txtGRNo.Text = obj.GRNo
                txtRoadPermitNo.Text = obj.Road_Permit_No
                txtModeOfTransport.Text = obj.Mode_Of_Transport
                txtCashMemoDetail.Text = obj.Cash_Memo_Detail
                txtVendorNo.Value = obj.Vendor_Code
                lblVendorName.Text = obj.Vendor_Name
                chkOnHold.Checked = obj.On_Hold
                txtReason.Text = obj.Reason
                txtRemarks.Text = obj.Remarks
                cboDocType.Enabled = False
                txtLocation.Enabled = False
                chkAgainst_Sale.Checked = obj.Against_Sale
                ''richa Ticket No BM00000003061 on 01/08/2014
                chkAgainstJobWork.Checked = obj.Against_JobWork
                '-------------------------------------------
                chkjvdisplaytype.Checked = obj.JVDisplayType
                ddlBilling.SelectedValue = obj.Billing
                lblDocumentAmt.Text = clsCommon.myFormat(obj.Document_Amount)
                txtVehicleNo.Text = obj.VehicleNo
                txtGPNo.Text = obj.GPNo
                txtGPDate.Value = obj.GPDate

                txtDeliveredBy.Value = obj.Delivered_By
                lblDeliveredBy.Text = obj.Delivered_ByName
                txtDepartment.Value = obj.Department
                fndCostCentre.Value = obj.CostCentre
                txtCostCentre.Text = obj.CostCentreDesc
                chlCust.Checked = obj.Against_Customer
                If obj.Is_Non_Inventory = 1 Then
                    chkNonInventoryItem.Checked = True
                    chkAgainstCCTransfer.Visible = True
                Else
                    chkNonInventoryItem.Checked = False
                    chkAgainstCCTransfer.Visible = False
                End If
                If obj.Is_Non_Inventory = 1 AndAlso obj.Is_Against_CC_Transfer = 1 Then
                    chkAgainstCCTransfer.Checked = True
                    fndToLocationCode.Value = obj.To_Location_Code
                    lblToLocationCode.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_Location_Master where Location_code = '" + obj.To_Location_Code + "'"))
                    lblToLocation.Visible = True
                    fndToLocationCode.Visible = True
                    lblToLocationCode.Visible = True
                Else
                    chkAgainstCCTransfer.Checked = False
                    fndToLocationCode.Value = ""
                    lblToLocationCode.Text = ""

                    lblToLocation.Visible = False
                    fndToLocationCode.Visible = False
                    lblToLocationCode.Visible = False

                End If
                '' Anubhooti 09-Oct-2014 BM00000003663
                If obj.Is_Rejected = 1 Then
                    ChkRejLoc.Checked = True
                Else
                    ChkRejLoc.Checked = False
                End If
                If clsCommon.myLen(obj.SRN_No) > 0 Then
                    txtSRNNo.Value = clsCommon.myCstr(obj.SRN_No)
                Else
                    txtSRNNo.Value = ""
                End If
                ''
                '' Anubhooti 10-Dec-2014 BM00000003662
                If chkAgainstJobWork.Checked = True AndAlso clsCommon.CompairString(obj.Doc_Type, "RGP") = CompairStringResult.Equal OrElse clsCommon.CompairString(obj.Doc_Type, "RGPR") = CompairStringResult.Equal Then
                    LblItemConType.Visible = True
                    CmbItemConType.Visible = True
                    CmbItemConType.SelectedValue = obj.Item_Conversion_Type
                Else
                    LblItemConType.Visible = False
                    CmbItemConType.Visible = False
                End If
                ''
                txtVendorNo.Value = obj.Vendor_Code
                lblVendorName.Text = obj.Vendor_Name

                If obj.chklocstion = "Y" AndAlso clsCommon.CompairString(obj.Doc_Type, "NRGP") = CompairStringResult.Equal Then
                    txtInvoiceNO.Text = obj.invoiceNo
                    txtPartyName.Text = obj.partyName
                    txtPartyAddress.Text = obj.partyAddress
                    dtpDispatchDate.Value = obj.DispatchDate
                End If
                '---------------------------------------------------------------
                chkthirdparty.Checked = False
                txtsrnlocation_code.Value = ""
                txtsrnlocation.Text = ""
                If obj.chklocstion = "Y" Then
                    chkthirdparty.Checked = True
                    txtsrnlocation.Text = obj.srnlocationdesc
                    txtsrnlocation_code.Value = obj.srnlocation
                End If
                '---------------------------------------------------------------------
                txtLocation.Value = obj.Location
                lblLocation.Text = obj.LocationName
                txtAMCRefNo.Text = obj.AMC_Ref_No
                lblDepartment.Text = clsDBFuncationality.getSingleValue("select Description from TSPL_GL_SEGMENT_CODE where Seg_No  ='3' and Segment_code='" + txtDepartment.Value + "'")
                If Not IsRefDoc Then
                    UsLock1.Status = obj.Status
                    txtDocNo.Value = obj.RGP_No
                    txtDate.Value = obj.RGP_Date
                    If clsCommon.CompairString(obj.Doc_Type, "NRGPR") = CompairStringResult.Equal Then
                        txtNRGPNo.Value = obj.Against_NRGP
                    Else
                        txtNRGPNo.Value = obj.Against_RGP
                    End If
                    btnSave.Text = "Update"
                Else
                    txtNRGPNo.Value = obj.RGP_No
                    isNewEntry = True
                    btnSave.Text = "Save"
                End If

                '' Anubhooti 
                If clsCommon.CompairString(obj.Doc_Type, "NRGP") = CompairStringResult.Equal OrElse clsCommon.CompairString(obj.Doc_Type, "NRGPR") = CompairStringResult.Equal Then
                    GrpRej.Visible = True
                    ChkRejLoc.Visible = True
                    txtVendorNo.Enabled = False
                Else
                    GrpRej.Visible = False
                    ChkRejLoc.Visible = False
                    ' txtVendorNo.Enabled = True
                End If
                ''

                txtPoNo.Value = obj.PO_Id
                dtPoDate.Value = obj.PO_Date
                txtScheduleNo.Value = obj.Against_Schedule_Code
                If clsCommon.myLen(txtScheduleNo.Value) > 0 Then
                    gv1.Columns(colPO_Sch_Qty).HeaderText = "Schedule Qty"
                End If
                chkAsPerBOM.Checked = IIf(obj.Against_BOM = 1, True, False)
                txtAsitis.Text = IIf(obj.Against_As_It_Is = 1, "Y", "")
                txtManPoNo.Text = obj.Man_PO_Id
                If clsCommon.myLen(obj.Man_PO_Date) > 0 Then
                    dtManPoDate.Value = obj.Man_PO_Date
                End If
                chkRepair.Checked = obj.Is_Repair

                '============================================================================
                If obj.Arr_BOM IsNot Nothing AndAlso obj.Arr_BOM.Count > 0 Then
                    For Each objtr As clsRGPBOMItem In obj.Arr_BOM
                        gv_PO.Rows(gv_PO.Rows.Count - 1).Cells(colBOMLineNo).Value = objtr.Line_No
                        gv_PO.Rows(gv_PO.Rows.Count - 1).Cells(colBOMCode).Value = objtr.BOM_Code
                        gv_PO.Rows(gv_PO.Rows.Count - 1).Cells(colBOMDesc).Value = objtr.BOM_desc
                        gv_PO.Rows(gv_PO.Rows.Count - 1).Cells(colBOMIcode).Value = objtr.Item_Code
                        'gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Tag = objtr.arrBatchItem
                        gv_PO.Rows(gv_PO.Rows.Count - 1).Cells(colBOMIname).Value = objtr.Iname
                        gv_PO.Rows(gv_PO.Rows.Count - 1).Cells(colBOMIUnit).Value = objtr.Unit_Code
                        gv_PO.Rows(gv_PO.Rows.Count - 1).Cells(colBOMQty).Value = objtr.RGP_Qty
                        If clsCommon.myLen(objtr.PO_Id) > 0 Then
                            gv_PO.Rows(gv_PO.Rows.Count - 1).Cells(colBOMBalanceQty).Value = clsPurchaseOrderDetail.GetBalancePOQtyBySchedule(objtr.PO_Id, objtr.Item_Code, obj.RGP_No, objtr.Unit_Code)
                        End If
                        If clsCommon.myLen(objtr.Against_Schedule_Code) > 0 Then
                            gv_PO.Rows(gv_PO.Rows.Count - 1).Cells(colBOMBalanceQty).Value = clsPurchaseScheduleDetail.GetBalanceScheduleQty(objtr.Against_Schedule_Code, objtr.Item_Code, obj.RGP_No, obj.RGP_Date, objtr.Unit_Code)
                        End If

                        gv_PO.Rows(gv_PO.Rows.Count - 1).Cells(colBOMRate).Value = objtr.Rate
                        gv_PO.Rows(gv_PO.Rows.Count - 1).Cells(colBOMMRP).Value = objtr.MRP
                        gv_PO.Rows(gv_PO.Rows.Count - 1).Cells(colBOMAmt).Value = objtr.Amount
                        gv_PO.Rows(gv_PO.Rows.Count - 1).Cells(colBOMSpecification).Value = objtr.Specification
                        gv_PO.Rows(gv_PO.Rows.Count - 1).Cells(colBOMPONo).Value = objtr.PO_Id
                        gv_PO.Rows(gv_PO.Rows.Count - 1).Cells(colBOMScheduleNo).Value = objtr.Against_Schedule_Code
                        gv_PO.Rows(gv_PO.Rows.Count - 1).Cells(colBOMModule).Value = objtr.Module_Id
                        gv_PO.Rows.AddNew()
                    Next


                End If
                '==========================================================================

                If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                    For Each objTr As clsRGPDetail In obj.Arr
                        gv1.Rows.AddNew()
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = objTr.Line_No
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value = objTr.Item_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Tag = objTr.arrBatchItem
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colIsSerialseItem).Value = clsItemMaster.IsSerializeItem(objTr.Item_Code)
                        '--------------------------------------------------
                        If clsCommon.CompairString(clsCommon.myCstr(cboDocType.SelectedValue), "RGPR") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(cboDocType.SelectedValue), "NRGPR") = CompairStringResult.Equal Then
                            gv1.ReadOnly = True
                        Else
                            gv1.ReadOnly = False
                        End If

                        Try
                            If chkthirdparty.Checked Then
                                cboItemType.SelectedValue = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select item_type from tspl_item_master where item_code='" + objTr.Item_Code + "'"))
                            End If
                        Catch exx As Exception
                            cboItemType.SelectedValue = ""
                        End Try
                        '----------------------------------------------------
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colIName).Value = objTr.Item_Desc
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colHSNNo).Value = clsItemMaster.GetItemHSNCode(objTr.Item_Code, Nothing)
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value = IIf(clsCommon.CompairString(cboDocType.SelectedValue, "NRGPR") = CompairStringResult.Equal And Not IsRefDoc, objTr.NRGP_Qty, objTr.RGP_Qty)
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colBalQty).Value = objTr.Balance_Qty
                        If clsCommon.CompairString(cboDocType.SelectedValue, "NRGPR") = CompairStringResult.Equal And Not IsRefDoc Then
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colRetQty).Value = objTr.NRGP_Qty
                        ElseIf clsCommon.CompairString(cboDocType.SelectedValue, "NRGPR") = CompairStringResult.Equal And IsRefDoc Then
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colRetQty).Value = objTr.RGP_Qty

                        ElseIf clsCommon.CompairString(cboDocType.SelectedValue, "RGPR") = CompairStringResult.Equal Then
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colRetQty).Value = objTr.RGP_Qty
                        Else
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colRetQty).Value = 0
                        End If
                        'gv1.Rows(gv1.Rows.Count - 1).Cells(colRetQty).Value = IIf(clsCommon.CompairString(cboDocType.SelectedValue, "NRGPR") = CompairStringResult.Equal And Not IsRefDoc, objTr.RGP_Qty, 0)
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colUnit).Value = objTr.Unit_code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colRate).Value = objTr.Item_Cost
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colAmt).Value = objTr.Amount
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colLastRGPNRGP).Value = objTr.Last_RGP_No
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDate).Value = objTr.Last_RGP_Date
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colSp).Value = objTr.Specification
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colmrp).Value = objTr.Item_MRP
                        '' Anubhooti 06-Feb-2015 (Approx_Cost newly added)
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colApproxCost).Value = objTr.Approx_Cost
                        ''''''''''''''''''' Anubhooti 09-Oct-2014
                        If clsCommon.myLen(txtSRNNo.Value) > 0 AndAlso ChkRejLoc.Checked = True Then
                            'Dim Qry As String = "select * from  TSPL_SRN_DETAIL  Where SRN_No='" & clsCommon.myCstr(txtSRNNo.Value) & "' AND Rejected_Qty >0 "
                            gv1.Columns(colRejSRNQty).IsVisible = True
                            'Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
                            'If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                            '    For Each dr As DataRow In dt.Rows
                            '        gv1.Columns(colRejSRNQty).ReadOnly = True
                            '        gv1.Rows(gv1.Rows.Count - 1).Cells(colRejSRNQty).Value = clsCommon.myCdbl(dr("Rejected_Qty"))
                            '    Next
                            'End If
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colRejSRNQty).Value = ReturnBalQty(clsCommon.myCstr(obj.SRN_No), clsCommon.myCstr(objTr.Item_Code), clsCommon.myCstr(obj.RGP_No), clsCommon.myCstr(obj.Location), obj.Doc_Type)
                        Else
                            gv1.Columns(colRejSRNQty).IsVisible = False
                        End If

                        gv1.Rows(gv1.Rows.Count - 1).Cells(colPO_Sch_Qty).Value = objTr.PO_Sch_Qty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colPurchaseOrderNo).Value = objTr.PO_Id
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colScheduleNo).Value = objTr.Against_Schedule_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colMainPOItem).Value = objTr.Main_PO_Icode
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colMainBOMCode).Value = objTr.BOM_Code
                        gv1.Rows(gv1.Rows.Count - 1).Tag = objTr.arrSrItem
                    Next
                    If obj.Status = ERPTransactionStatus.Pending Then
                        gv1.Rows.AddNew()
                        gv_PO.Rows.AddNew()

                        gv1.CurrentRow = gv1.Rows(0)
                        gv_PO.CurrentRow = gv_PO.Rows(0)
                    End If
                End If
                '' Anubhooti 09-Oct-2014
                If clsCommon.myLen(txtSRNNo.Value) > 0 AndAlso ChkRejLoc.Checked = True Then
                    'Dim Qry As String = "select * from  TSPL_SRN_DETAIL  Where SRN_No='" & clsCommon.myCstr(txtSRNNo.Value) & "' AND Rejected_Qty >0 "

                    'Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
                    'If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    '    For Each dr As DataRow In dt.Rows
                    '        'gv1.Rows.AddNew()

                    '        gv1.Columns(colRejSRNQty).ReadOnly = True
                    '        'gv1.Columns(colmrp ).ReadOnly = True
                    '        gv1.Rows(gv1.Rows.Count - 1).Cells(colRejSRNQty).Value = clsCommon.myCdbl(dr("Rejected_Qty"))
                    '    Next
                    'End If
                End If
                ''
                If (chlCust.Checked) Or chkAgainst_Sale.Checked Then
                    RadLabel2.Text = "Customer No"
                Else
                    RadLabel2.Text = "Vendor No"
                End If
                ''For Custom Fields
                If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
                    UcCustomFields1.LoadData(obj.RGP_No)
                End If
                clsCustomFieldGrid.FillDataInGrid(obj.RGP_No, MyBase.Form_ID, gv1)
                ''End of For Custom Fields
                UcAttachment1.LoadData(obj.RGP_No)

                RadGroupBox1.Enabled = False
                cboItemType.Enabled = False

                chkAgainstJobWork.Enabled = False
                txtPoNo.Enabled = False
                dtPoDate.Enabled = False
                txtScheduleNo.Enabled = False
                chkAsPerBOM.Enabled = False
                gv_PO.Columns(colBOMCode).IsVisible = chkAsPerBOM.Checked
                gv_PO.Columns(colBOMDesc).IsVisible = chkAsPerBOM.Checked
                gv1.Columns(colPO_Sch_Qty).IsVisible = chkAsPerBOM.Checked
                btnSameasAbove.Enabled = False

                If chkAgainstJobWork.Checked AndAlso IsRGPAfterPO AndAlso Not chkAsPerBOM.Checked Then
                    'RadGroupBox3.Visible = True
                    RadPageViewPage3.Item.Visibility = ElementVisibility.Visible
                    RadPageView2.SelectedPage = RadPageViewPage2
                Else
                    'RadGroupBox3.Visible = False
                    RadPageViewPage3.Item.Visibility = ElementVisibility.Collapsed
                    RadPageView2.SelectedPage = RadPageViewPage2
                End If


            Else
                AddNew()
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isInsideLoadData = False
            IsRefDoc = False
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
                SaveData(True)
                '' Anubhooti 12-Sep-2014 BM00000003735
                If FrmMainTranScreen.ValidateTransactionAccToFinYear("RGP/NRGP", txtDate.Value) = False Then
                    Exit Sub
                End If
                ''

                '--------------------By Monika----------------------------------
                If chkthirdparty.Checked = True AndAlso clsCommon.CompairString(cboDocType.SelectedValue, "RGP") = CompairStringResult.Equal Then
                    If Not (common.clsCommon.MyMessageBoxShow("Are You Sure, Want To Make Auto SRN?", "Attention", MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes) Then
                        Return
                    End If

                    If AutoSRNFromRGP() Then
                        clsCommon.MyMessageBoxShow(Me, "Auto SRN Transfer Successfully,For Post GoTo Store Received Note", Me.Text)
                    Else
                        Dim qry As String = ""
                        clsDBFuncationality.ExecuteNonQuery(qry)
                        Return
                    End If
                End If
                '-------------------------------------------------------------

                If (clsRGPHead.PostData(txtDocNo.Value)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Successfully Posted", Me.Text)

                    LoadData(txtDocNo.Value, NavigatorType.Current, False)

                    If (common.clsCommon.MyMessageBoxShow("Do you want to print", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes) Then
                        If clsCommon.CompairString(cboDocType.SelectedValue, "NRGP") = CompairStringResult.Equal AndAlso chkthirdparty.Checked Then
                            printWith3rdParty()
                        Else
                            print()
                        End If
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
                If (clsRGPHead.DeleteData(txtDocNo.Value)) Then
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

    Private Sub txtDocNo__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtDocNo._MYNavigator
        Try
            LoadData(txtDocNo.Value, NavType, False)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtDocNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtDocNo._MYValidating
        Dim qry As String = "select RGP_No as Code,RGP_Date as Date,TSPL_RGP_HEAD.Vendor_Code as [Vendor Code], TSPL_RGP_HEAD.Vendor_Name as Vendor,ISNULL(TSPL_VENDOR_MASTER.alies_name,'') As [Alies Name],Document_Amount as Amount,case when TSPL_RGP_HEAD.Status='0' then 'Pending' else 'Approved' end as [Status] from TSPL_RGP_HEAD"
        '' Anubhooti 12-Mar-2015 (Fetch Alies Name On Vendor Finder)
        qry += " LEFT OUTER JOIN TSPL_VENDOR_MASTER ON TSPL_VENDOR_MASTER.Vendor_Code = TSPL_RGP_HEAD.Vendor_Code "
        Dim whrClas As String = ""
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            whrClas = " Location in (" + objCommonVar.strCurrUserLocations + ")"
        End If

        LoadData(clsCommon.ShowSelectForm("RGPFNDR", qry, "Code", whrClas, txtDocNo.Value, "RGP_Date desc", isButtonClicked), NavigatorType.Current, False)
    End Sub

    Private Sub FrmAPInvoiceEntry_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.F2 AndAlso gv1.CurrentCell IsNot Nothing Then
            isCellValueChangedOpen = True
            If gv1.CurrentColumn Is gv1.Columns(colICode) AndAlso chkNonInventoryItem.Checked = False Then
                gv1.CurrentColumn = gv1.Columns(colIName)

                '-----------------check when thrd party location on----------------------------------------
                If chkthirdparty.Checked AndAlso clsCommon.myLen(cboItemType.SelectedValue) <= 0 Then
                    clsCommon.MyMessageBoxShow(Me, "Please Select Item Type First", Me.Text)
                    Return
                End If
                '--------------------------------------------------------------------------------------------

                OpenICodeList(True)
                gv1.CurrentColumn = gv1.Columns(colICode)
            End If
            If gv1.CurrentColumn Is gv1.Columns(colUnit) AndAlso chkNonInventoryItem.Checked = False AndAlso (Not chkAsPerBOM.Checked) Then
                OpenUOMList(True)
            End If
            setGridFocus()
            isCellValueChangedOpen = False
        ElseIf e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnAddNew.Enabled Then
            AddNew()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnSave.Enabled Then
            SaveData(False)
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag AndAlso btnPost.Enabled Then
            PostData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isDeleteFlag AndAlso btnDelete.Enabled Then
            DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnClose.Enabled Then
            CloseForm()
        ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F12 Then
            If MyBase.isReverse Then

                'Add Tool tip Task No- TEC/22/05/18-000245
                ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction" + Environment.NewLine +
                                                      "TSPL_RGP_HEAD " + Environment.NewLine +
                                                      "TSPL_RGP_DETAIL " + Environment.NewLine +
                                                      "TSPL_RGP_JOB_WORK_DETAIL " + Environment.NewLine +
                                                      "TSPL_SERIAL_ITEM " + Environment.NewLine +
                                                      "TSPL_BATCH_ITEM")
                'Add Tool tip Task No- TEC/22/05/18-000245
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

        If e.KeyCode = Keys.F2 AndAlso gv_PO.CurrentCell IsNot Nothing AndAlso gv_PO.CurrentColumn Is gv_PO.Columns(colBOMCode) Then
            isCellValueChangedOpen = True
            OpenBOMCode(True)
            isCellValueChangedOpen = False
        End If

        If e.KeyCode = Keys.F2 AndAlso gv_PO.CurrentCell IsNot Nothing AndAlso gv_PO.CurrentColumn Is gv_PO.Columns(colBOMIcode) Then
            isCellValueChangedOpen = True
            If clsCommon.CompairString(txtAsitis.Text, "Y") <> CompairStringResult.Equal Then
                OpenBOMIcode(True)
            End If
            isCellValueChangedOpen = False
        End If

        If e.KeyCode = Keys.F2 AndAlso gv_PO.CurrentCell IsNot Nothing AndAlso gv_PO.CurrentColumn Is gv_PO.Columns(colBOMIUnit) Then
            isCellValueChangedOpen = True
            OpenBOMUnit(True)
            isCellValueChangedOpen = False
        End If

        If e.KeyCode = Keys.F4 Then
            OpenSerialItem()
        End If
    End Sub
    '' changes by shivani against ticket[BM00000008709]
    Private Sub txtVendorNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtVendorNo._MYValidating
        If chkAgainst_Sale.Checked = True Or chlCust.Checked = True Then
            Dim qry As String = "select Cust_Code as [Code],Customer_Name  as [Name] from TSPL_CUSTOMER_MASTER "
            txtVendorNo.Value = clsCommon.ShowSelectForm("RGPCustFNDer", qry, "Code", "status='N'", txtVendorNo.Value, "Code", isButtonClicked)
            lblVendorName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Customer_Name from TSPL_CUSTOMER_MASTER where Cust_Code ='" + txtVendorNo.Value + "'"))
        Else
            Dim qry As String = "select Vendor_Code as Code,Vendor_Name as Name,ISNULL(TSPL_VENDOR_MASTER.alies_name,'') As [Alies Name],(isnull(ADD1,'')+' '+isnull(Add2,'')+' '+isnull(Add3,''))as Address from TSPL_VENDOR_MASTER"
            Dim whrcls As String = " Status='N' and TSPL_VENDOR_MASTER.Form_Type<>'VSP' "
            If chkAsPerBOM.Checked Then
                whrcls += " and vendor_code in (select vendor_code from tspl_pp_bom_head where is_post='1')"
            End If
            txtVendorNo.Value = clsCommon.ShowSelectForm("RGPVeFNDer", qry, "Code", whrcls, txtVendorNo.Value, "Code", isButtonClicked)
            lblVendorName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Vendor_Name from TSPL_VENDOR_MASTER where Vendor_Code ='" + txtVendorNo.Value + "'"))
        End If
    End Sub

    Private Sub gv1_CellFormatting(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CellFormattingEventArgs) Handles gv1.CellFormatting
        Try
            If e.Column.Index >= 0 Then
                If e.Column Is gv1.Columns(colICode) Then
                    If clsCommon.myLen(gv1.CurrentRow.Cells(colPurchaseOrderNo).Value) > 0 OrElse clsCommon.myLen(gv1.CurrentRow.Cells(colScheduleNo).Value) > 0 Then
                        gv1.Columns(colICode).ReadOnly = True
                    Else
                        gv1.Columns(colICode).ReadOnly = False
                    End If
                End If
                If e.Column Is gv1.Columns(colUnit) Then
                    If clsCommon.myLen(gv1.CurrentRow.Cells(colPurchaseOrderNo).Value) > 0 OrElse clsCommon.myLen(gv1.CurrentRow.Cells(colScheduleNo).Value) > 0 OrElse (chkAgainstJobWork.Checked AndAlso chkAsPerBOM.Checked) Then
                        gv1.Columns(colUnit).ReadOnly = True
                    Else
                        gv1.Columns(colUnit).ReadOnly = False
                    End If
                    'If chkNonInventoryItem.Checked Then
                    '    gv1.CurrentRow.Cells(colUnit).ReadOnly = True
                    'Else
                    '    gv1.CurrentRow.Cells(colUnit).ReadOnly = False
                    'End If
                End If
                'Dim cell As GridDataCellElement = TryCast(e.CellElement, GridDataCellElement)
                'cell.GradientStyle = GradientStyles.Solid
                'cell.BackColor = Color.FromArgb(243, 181, 51)
            End If
        Catch ex As Exception
            'common.clsCommon.MyMessageBoxShow(me,ex.Message,me.text)
        End Try
    End Sub

    Private Sub gv1_UserDeletingRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs) Handles gv1.UserDeletingRow
        If common.clsCommon.MyMessageBoxShow("Delete The Current Row." + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
            e.Cancel = True
        End If
    End Sub

    Private Sub gv1_UserDeletedRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowEventArgs) Handles gv1.UserDeletedRow
        UpdateAllTotals()
        For ii As Integer = 1 To gv1.Rows.Count
            gv1.Rows(ii - 1).Cells(colLineNo).Value = ii
        Next
    End Sub

    Private Sub txtLocation__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtLocation._MYValidating
        '' Anubhooti 07-Oct-2014 (Location Finder Changes On The Basis Of Rejected Location)
        If ChkRejLoc.Checked = False Then

            Dim qry As String = "select Location_Code as Code,Location_Desc as Name from TSPL_LOCATION_MASTER "
            Dim WhrCls As String = " Location_Type='Physical'  "
            If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                WhrCls += "  and  Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
            End If

            '-------------Done By Monika---------------------
            '--------------------------Done By pankaj Jha to display only third party location in NRGP
            If (clsCommon.CompairString(MDI.IsLoc_Third_Party, "YES") = CompairStringResult.Equal) AndAlso chkthirdparty.Checked AndAlso clsCommon.CompairString(cboDocType.SelectedValue, "NRGP") = CompairStringResult.Equal Then
                WhrCls += " and isnull(vendor_code,'')<>''"
                '------------------------------------------------------------------------------------------
            ElseIf (clsCommon.CompairString(MDI.IsLoc_Third_Party, "YES") = CompairStringResult.Equal) AndAlso chkthirdparty.Checked Then
                'WhrCls += "  and isnull(vendor_code,'')<>''"
            ElseIf (clsCommon.CompairString(MDI.IsLoc_Third_Party, "YES") = CompairStringResult.Equal) AndAlso chkthirdparty.Checked = False Then
                WhrCls += " and isnull(vendor_code,'')=''"
            End If
            txtLocation.Value = clsCommon.ShowSelectForm("Location_MasterFD", qry, "Code", WhrCls, txtLocation.Value, "Code", isButtonClicked)
            lblLocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" + txtLocation.Value + "'"))

        Else
            Dim qry As String = "Select Location_Code As [Code],ISNULL(Location_Desc,'') As [Location Desc]  From TSPL_LOCATION_MASTER  "
            Dim WhrCls As String = " Rejected_Type='Y' AND Location_Type='Physical'  "
            If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                WhrCls += "  and  Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
            End If

            '-------------Done By Monika---------------------
            '--------------------------Done By pankaj Jha to display only third party location in NRGP
            'If (clsCommon.CompairString(MDI.IsLoc_Third_Party, "YES") = CompairStringResult.Equal) AndAlso chkthirdparty.Checked AndAlso clsCommon.CompairString(cboDocType.SelectedValue, "NRGP") = CompairStringResult.Equal Then
            '    WhrCls += " and isnull(vendor_code,'')<>''"
            '    '------------------------------------------------------------------------------------------
            'ElseIf (clsCommon.CompairString(MDI.IsLoc_Third_Party, "YES") = CompairStringResult.Equal) AndAlso chkthirdparty.Checked Then
            '    'WhrCls += "  and isnull(vendor_code,'')<>''"
            'ElseIf (clsCommon.CompairString(MDI.IsLoc_Third_Party, "YES") = CompairStringResult.Equal) AndAlso chkthirdparty.Checked = False Then
            '    WhrCls += " and isnull(vendor_code,'')=''"
            'End If
            txtLocation.Value = clsCommon.ShowSelectForm("RejLocNRGP", qry, "Code", WhrCls, txtLocation.Value, "Code", isButtonClicked)
            If clsCommon.myLen(txtLocation.Value) > 0 Then
                lblLocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(Location_Desc,'') AS Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" + txtLocation.Value + "'"))
                txtSRNNo.Value = ""
            Else
                lblLocation.Text = ""
            End If
            LoadBlankGrid()
        End If
    End Sub

    Private Sub btnprint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnprint.Click
        ' Ticket No : BHA/10/09/18-000532 By prabhakar - On RPT File 
        If clsCommon.CompairString(cboDocType.SelectedValue, "NRGP") = CompairStringResult.Equal AndAlso chkthirdparty.Checked Then
            printWith3rdParty()
        Else
            print()
        End If

    End Sub

    Private Sub printWith3rdParty()
        Try

            Dim type As String = cboDocType.Text
            Dim strDep As String = txtDepartment.Value
            If clsCommon.myLen(txtDepartment.Value) > 0 Then
                strDep = " Seg_No  ='3' and "
            Else
                strDep = ""
            End If
            Dim strqry As String = "SELECT TSPL_ITEM_MASTER.HSN_Code,tspl_state_master_for_location_state.GST_STATE_Code as LOC_GST_State_Code,TSPL_LOCATION_MASTER.GSTNO as Loc_GstInNo ,TSPL_VENDOR_MASTER.GSTFinalNo AS Vendor_GSTIN_NO,TSPL_STATE_MASTER.GST_STATE_Code AS Vendor_GST_StateCode,  TSPL_LOCATION_MASTER.Tin_No as location_tin_no, TSPL_LOCATION_MASTER.add1 +case when len(TSPL_LOCATION_MASTER.add2)>0 then ', '+TSPL_LOCATION_MASTER.add2 else '' end +case when LEN(isnull(TSPL_LOCATION_MASTER.Add3,''))>0 then ', '+isnull(TSPL_LOCATION_MASTER.Add3,'') else ' ' end   as Location_address, TSPL_RGP_HEAD.Created_By,TSPL_RGP_HEAD.Modify_By, (select emp_name from TSPL_EMPLOYEE_MASTER where EMP_CODE =TSPL_RGP_HEAD .Delivered_By )as DeliverdBy ,TSPL_RGP_HEAD.RGP_No,TSPL_RGP_HEAD.VehicleNo, convert(varchar,TSPL_RGP_HEAD.RGP_Date,103) as RGP_Date , TSPL_RGP_HEAD.Doc_Type, TSPL_RGP_HEAD.Vendor_Code, " &
                     " TSPL_RGP_HEAD.Vendor_Name, TSPL_RGP_HEAD.VehicleNo, TSPL_RGP_HEAD.GPNo, TSPL_RGP_HEAD.GPDate, TSPL_RGP_HEAD.Reason, " &
                     " TSPL_RGP_HEAD.Remarks, TSPL_RGP_HEAD.Posting_Date, TSPL_RGP_HEAD.comp_code, TSPL_RGP_HEAD.Location, TSPL_RGP_HEAD.Mode_Of_Transport, TSPL_RGP_HEAD.Cash_Memo_Detail, " &
                     " TSPL_RGP_HEAD.Document_Amount,TSPL_RGP_HEAD.Created_By, TSPL_COMPANY_MASTER.Comp_Name, tspl_company_Master.add1 +case when len(tspl_company_Master.add2)>0 then ', '+tspl_company_Master.add2 else '' end +case when LEN(isnull(tspl_company_Master.Add3,''))>0 then ', '+isnull(tspl_company_Master.Add3,'') else ' ' end   as Add1" &
                     " , TSPL_COMPANY_MASTER.State, TSPL_COMPANY_MASTER.Tin_No, TSPL_COMPANY_MASTER.Logo_Img, " &
                     " TSPL_COMPANY_MASTER.Logo_Img2, TSPL_RGP_DETAIL.Line_No, TSPL_RGP_DETAIL.Item_Code, TSPL_RGP_DETAIL.Item_Desc, " &
                     " TSPL_RGP_DETAIL.RGP_Qty, TSPL_RGP_DETAIL.Unit_code, TSPL_RGP_DETAIL.Item_Cost, TSPL_RGP_DETAIL.Amount, TSPL_RGP_DETAIL.Specification ,TSPL_RGP_HEAD.InvoiceNo,TSPL_RGP_HEAD.PartyName,TSPL_RGP_HEAD.PartyAddress,convert (varchar,TSPL_RGP_HEAD.DispatchDate,103) as DispatchDate "


            strqry += " ,'" + type + "' as RGPType ,TSPL_GL_SEGMENT_CODE.Description as Department,case when  TSPL_RGP_HEAD.billing='Y' then 'Yes' when  TSPL_RGP_HEAD.billing='N' then 'No' else '' end as Billing "
            strqry += " FROM TSPL_RGP_HEAD INNER JOIN "
            strqry += " TSPL_RGP_DETAIL ON TSPL_RGP_HEAD.RGP_No = TSPL_RGP_DETAIL.RGP_No  "

            strqry += "   LEFT OUTER JOIN TSPL_COMPANY_MASTER ON TSPL_RGP_HEAD.comp_code = TSPL_COMPANY_MASTER.Comp_Code left outer join TSPL_LOCATION_MASTER on TSPL_RGP_HEAD.Location=TSPL_LOCATION_MASTER .Location_Code LEFT OUTER JOIN  TSPL_GL_SEGMENT_CODE on TSPL_RGP_HEAD.Department = TSPL_GL_SEGMENT_CODE.Segment_code left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code= TSPL_RGP_DETAIL.Item_Code  left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code = TSPL_RGP_HEAD.Vendor_Code left outer join tspl_state_master as tspl_state_master_for_location_state on  tspl_state_master_for_location_state.state_code=tspl_location_master.state  left outer join TSPL_STATE_MASTER on TSPL_VENDOR_MASTER.State_Code= TSPL_STATE_MASTER.State_Code  where   " & strDep & " TSPL_RGP_HEAD.RGP_No='" + txtDocNo.Value + "'   "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(strqry)
            Dim frmCRV As New frmCrystalReportViewer()
            frmCRV.funreport(CrystalReportFolder.PurchaseOrder, dt, "rptNRGP3rdParty", "NRGP Report", clsCommon.myCDate(dt.Rows(0)("RGP_Date")))
            frmCRV = Nothing


        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Public Sub print()
        Try
            '========update by preeti Against Ticket No[BHA/22/05/18-000032]
            Dim type As String = cboDocType.Text
            Dim strDep As String = txtDepartment.Value
            If clsCommon.myLen(txtDepartment.Value) > 0 Then
                strDep = " Seg_No  ='3' and "
            Else
                strDep = ""
            End If
            '---------------------------------------------------------------
            Dim VendorCustomerGSTNo As String = String.Empty
            Dim VendorCustomerGSTNoForJoin As String = String.Empty
            If chkAgainst_Sale.Checked = False And chlCust.Checked = False Then
                VendorCustomerGSTNo = " TSPL_VENDOR_MASTER.GSTFinalNo AS Vendor_GSTIN_NO "
                VendorCustomerGSTNoForJoin = " TSPL_VENDOR_MASTER.State_Code "
            Else
                VendorCustomerGSTNo = " tspl_customer_master.GSTNO AS Vendor_GSTIN_NO "
                VendorCustomerGSTNoForJoin = " tspl_customer_master.State "
            End If
            '-----------------------------------------------------------------
            Dim strqry As String = "SELECT TSPL_RGP_HEAD.Is_Against_CC_Transfer, TSPL_RGP_HEAD.To_Location_Code,TSPL_LOCATION_MASTER_To.Location_Desc as To_Location_Desc,TSPL_LOCATION_MASTER_To.Add1 as To_Location_Add1, TSPL_LOCATION_MASTER_To.add2 as To_Location_Add2,TSPL_LOCATION_MASTER_To.add3 as To_Location_Add3, TSPL_LOCATION_MASTER_To.City_Code as To_Location_City_code, TSPL_LOCATION_MASTER_To.State  as To_Location_State_code, TSPL_LOCATION_MASTER_To.Pin_Code as To_Location_Pin,TSPL_LOCATION_MASTER_To.GSTNO as To_Location_GSTNO,tspl_state_master_To_location_state.GST_STATE_Code AS To_Location_GST_StateCode, tspl_rgp_head.Man_po_no,convert(varchar,tspl_rgp_head.man_po_date,103) as man_po_date,tspl_rgp_head.po_id,convert(varchar,tspl_purchase_order_head.purchaseorder_date,103) as purchaseorder_date,tspl_rgp_head.AMC_Ref_No, TSPL_ITEM_MASTER.HSN_Code,tspl_state_master_for_location_state.GST_STATE_Code as LOC_GST_State_Code,TSPL_LOCATION_MASTER.GSTNO as Loc_GstInNo ," + VendorCustomerGSTNo + ",TSPL_STATE_MASTER.GST_STATE_Code AS Vendor_GST_StateCode, TSPL_LOCATION_MASTER.Tin_No as location_tin_no, TSPL_LOCATION_MASTER.add1 +case when len(TSPL_LOCATION_MASTER.add2)>0 then ', '+TSPL_LOCATION_MASTER.add2 else '' end +case when LEN(isnull(TSPL_LOCATION_MASTER.Add3,''))>0 then ', '+isnull(TSPL_LOCATION_MASTER.Add3,'') else ' ' end   as Location_address, TSPL_RGP_HEAD.Created_By,TSPL_RGP_HEAD.Modify_By, (select emp_name from TSPL_EMPLOYEE_MASTER where EMP_CODE =TSPL_RGP_HEAD .Delivered_By )as DeliverdBy ,TSPL_RGP_HEAD.RGP_No,TSPL_RGP_HEAD.VehicleNo, convert(Varchar,TSPL_RGP_HEAD.RGP_Date,103) as RGP_Date , TSPL_RGP_HEAD.Doc_Type, TSPL_RGP_HEAD.Vendor_Code, " &
                     " TSPL_RGP_HEAD.Vendor_Name, TSPL_RGP_HEAD.VehicleNo, TSPL_RGP_HEAD.GPNo, TSPL_RGP_HEAD.GPDate, TSPL_RGP_HEAD.Reason, " &
                     " TSPL_RGP_HEAD.Remarks, TSPL_RGP_HEAD.Posting_Date, TSPL_RGP_HEAD.comp_code, TSPL_RGP_HEAD.Location,TSPL_LOCATION_MASTER.Location_Desc , TSPL_RGP_HEAD.Mode_Of_Transport, TSPL_RGP_HEAD.Cash_Memo_Detail, " &
                     " TSPL_RGP_HEAD.Document_Amount,TSPL_RGP_HEAD.Created_By, TSPL_COMPANY_MASTER.Comp_Name, tspl_company_Master.add1 +case when len(tspl_company_Master.add2)>0 then ', '+tspl_company_Master.add2 else '' end +case when LEN(isnull(tspl_company_Master.Add3,''))>0 then ', '+isnull(tspl_company_Master.Add3,'') else ' ' end   as Add1" &
                     " , TSPL_COMPANY_MASTER.State, TSPL_COMPANY_MASTER.Tin_No, TSPL_COMPANY_MASTER.Logo_Img, " &
                     " TSPL_COMPANY_MASTER.Logo_Img2, TSPL_RGP_DETAIL.Line_No, TSPL_RGP_DETAIL.Item_Code, TSPL_RGP_DETAIL.Item_Desc, " &
                     " TSPL_RGP_DETAIL.RGP_Qty, TSPL_RGP_DETAIL.Unit_code, TSPL_RGP_DETAIL.Item_Cost, TSPL_RGP_DETAIL.Amount, TSPL_RGP_DETAIL.Specification "

            If chkAgainst_Sale.Checked = False And chlCust.Checked = False Then
                strqry += ", TSPL_VENDOR_MASTER.Add1 AS venadd1, TSPL_VENDOR_MASTER.Add2 AS venadd2, TSPL_VENDOR_MASTER.Add3 AS venadd3,TSPL_VENDOR_MASTER.Tin_No as VenTINNO, " &
                     " TSPL_VENDOR_MASTER.City_Code_Desc as vencity,TSPL_VENDOR_MASTER.Lst_No,TSPL_VENDOR_MASTER.CST"
            Else
                strqry += " , TSPL_CUSTOMER_MASTER.Customer_Name ,ISNULL(TSPL_CUSTOMER_MASTER.Phone1,'')+ Case When ISNULL(TSPL_CUSTOMER_MASTER.Phone2,'')<>'' Then ', '+ TSPL_CUSTOMER_MASTER.Phone2 Else'' End as Customer_Phone ,TSPL_CUSTOMER_MASTER.add1 +case when len(TSPL_CUSTOMER_MASTER.add2)>0 then ', '+TSPL_CUSTOMER_MASTER.add2 else '' end +case when LEN(isnull(TSPL_CUSTOMER_MASTER.Add3,''))>0 then ', '+isnull(TSPL_CUSTOMER_MASTER.Add3,'') else ' ' end   as Customer_address,TSPL_CUSTOMER_MASTER.Tin_No as customer_Tin_No,TSPL_CUSTOMER_MASTER.Remarks1  +case when len(TSPL_CUSTOMER_MASTER.Remarks2 )>0 then ', '+TSPL_CUSTOMER_MASTER.Remarks2 else ''  end   as Customer_Remarks,TSPL_CUSTOMER_MASTER.Add1 AS venadd1, TSPL_CUSTOMER_MASTER.Add2 AS venadd2, TSPL_CUSTOMER_MASTER.Add3 AS venadd3,TSPL_CUSTOMER_MASTER.Tin_No as VenTINNO,  TSPL_CITY_MASTER.City_Name as vencity,TSPL_CUSTOMER_MASTER.Lst_No "
            End If
            strqry += " ,'" + type + "' as RGPType ,TSPL_GL_SEGMENT_CODE.Description as Department,case when  TSPL_RGP_HEAD.billing='Y' then 'Yes' when  TSPL_RGP_HEAD.billing='N' then 'No' else '' end as Billing,TSPL_RGP_DETAIL.Approx_Cost,TSPL_RGP_HEAD.Road_Permit_No, convert(date,TSPL_RGP_HEAD.RoadPermit_Date)as RoadPermit_Date  "
            strqry += " FROM TSPL_RGP_HEAD INNER JOIN "
            strqry += " TSPL_RGP_DETAIL ON TSPL_RGP_HEAD.RGP_No = TSPL_RGP_DETAIL.RGP_No  "
            If chkAgainst_Sale.Checked = False And chlCust.Checked = False Then
                strqry += " LEFT OUTER JOIN TSPL_VENDOR_MASTER ON TSPL_RGP_HEAD.Vendor_Code = TSPL_VENDOR_MASTER.Vendor_Code  "
            Else
                strqry += " LEFT OUTER JOIN tspl_customer_master ON TSPL_RGP_HEAD.Vendor_Code = tspl_customer_master.cust_code  "
                strqry += " left outer JOIN tspl_city_master ON tspl_customer_master.city_code = tspl_city_master.city_code  "
            End If
            strqry += "   LEFT OUTER JOIN TSPL_COMPANY_MASTER ON TSPL_RGP_HEAD.comp_code = TSPL_COMPANY_MASTER.Comp_Code left outer join TSPL_LOCATION_MASTER on TSPL_RGP_HEAD.Location=TSPL_LOCATION_MASTER .Location_Code LEFT OUTER JOIN  TSPL_GL_SEGMENT_CODE on TSPL_RGP_HEAD.Department = TSPL_GL_SEGMENT_CODE.Segment_code   left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code= TSPL_RGP_DETAIL.Item_Code left outer join tspl_state_master as tspl_state_master_for_location_state on  tspl_state_master_for_location_state.state_code=tspl_location_master.state  left outer join TSPL_STATE_MASTER on " + VendorCustomerGSTNoForJoin + " = TSPL_STATE_MASTER.State_Code left join tspl_purchase_order_head on tspl_purchase_order_head.purchaseorder_no=TSPL_RGP_HEAD.po_id   left outer join TSPL_LOCATION_MASTER as TSPL_LOCATION_MASTER_To on TSPL_RGP_HEAD.To_Location_Code=TSPL_LOCATION_MASTER_To .Location_Code left outer join tspl_state_master as tspl_state_master_To_location_state on  tspl_state_master_To_location_state.state_code=TSPL_LOCATION_MASTER_To.state  where   " & strDep & " TSPL_RGP_HEAD.RGP_No='" + txtDocNo.Value + "'   "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(strqry)
            '--- Field Approx.Cost , RoadPermit and subreport Added by shivani---'
            Dim frmCRV As New frmCrystalReportViewer()
            If (type = "Returnable Gate Pass") Then
                ' PurchaseOrderViewer.funreport(dt, "rptRGPNew", "RGP Report") 
                frmCRV.funsubreportWithdt(CrystalReportFolder.PurchaseOrder, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "rptRGPNew", "RGP Report", clsCommon.myCDate(dt.Rows(0)("RGP_Date")), "rptCompanyAddress.rpt")
            Else
                'PurchaseOrderViewer.funreport(dt, "rptNRGP", "NRGP Report")
                frmCRV.funsubreportWithdt(CrystalReportFolder.PurchaseOrder, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "rptNRGP", "NRGP Report", clsCommon.myCDate(dt.Rows(0)("RGP_Date")), "rptCompanyAddress.rpt")
            End If
            frmCRV = Nothing

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub txtDeliveredBy__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtDeliveredBy._MYValidating

        Dim obj As clsEmployeeMaster = clsEmployeeMaster.FinderForEmployeeInGL_Segment_Code(txtDeliveredBy.Value, isButtonClicked)
        If obj IsNot Nothing AndAlso clsCommon.myLen(obj.EMP_CODE) > 0 Then
            txtDeliveredBy.Value = obj.EMP_CODE
            lblDeliveredBy.Text = obj.Emp_Name
        Else
            txtDeliveredBy.Value = ""
            lblDeliveredBy.Text = ""
        End If
    End Sub

    Private Sub gv1_CurrentColumnChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gv1.CurrentColumnChanged
        '' Anubhooti 09-Oct-2014
        If ChkRejLoc.Checked = False Then
            If gv1.RowCount > 0 Then
                Dim intCurrRow As Integer = gv1.CurrentRow.Index
                gv1.CurrentRow.Cells(colLineNo).Value = clsCommon.myCdbl(intCurrRow + 1)
                If intCurrRow = gv1.Rows.Count - 1 Then
                    gv1.Rows.AddNew()
                    gv1.CurrentRow = gv1.Rows(intCurrRow)
                End If
            End If
        End If
    End Sub

    Private Sub CheckItemQty()

    End Sub


    'KUNAL > DATE : 16 - NOV - 2016
    Dim NRGPBookingNo As String = Nothing
    Private Sub LoadNRGP_Request()
        Dim chkItemQty As Decimal = 0.0

        Try

            'Dim qry As String = " SELECT NH.BOOKING_NO , NH.Posted , ND.Item_Code , NH.CSA_CODE, NH.VEN_CODE , ND.BOOK_QTY , convert(decimal(18,2),(isnull(ND.BOOK_QTY,0) - isnull(IM.RGP_Qty,0))) AS [LEFT QTY]  FROM TSPL_NRGP_REQUEST_HEAD NH LEFT JOIN TSPL_NRGP_REQUEST_DETAIL ND ON NH.BOOKING_NO = ND.BOOKING_NO LEFT JOIN TSPL_RGP_DETAIL IM ON ND.Item_Code = IM.Item_Code "
            Dim qry As String = " select TMP.BOOKING_NO , TMP.Posted , TMP.Item_Code , TMP.CSA_CODE, TMP.VEN_CODE , TMP.BOOK_QTY  ,tmp.[LEFT QTY]   from ( SELECT NH.BOOKING_NO , NH.Posted , ND.Item_Code , NH.CSA_CODE, NH.VEN_CODE , ND.BOOK_QTY , convert(decimal(18,2),(isnull(ND.BOOK_QTY,0) - isnull(IM.RGP_Qty,0))) AS [LEFT QTY]  FROM  TSPL_NRGP_REQUEST_HEAD NH LEFT JOIN TSPL_NRGP_REQUEST_DETAIL ND ON NH.BOOKING_NO = ND.BOOKING_NO LEFT JOIN TSPL_RGP_DETAIL IM ON ND.Item_Code = IM.Item_Code ) as tmp "
            Dim WhrCls As String = " TMP.[LEFT QTY] > 0 "
            NRGPBookingNo = clsCommon.ShowSelectForm("frmNRGPFinder", qry, "BOOKING_NO", WhrCls, "BOOKING_DATE", "BOOKING_NO", True)
            chkItemQty = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("SELECT convert(decimal(18,2),(isnull(ND.BOOK_QTY,0) - isnull(IM.RGP_Qty,0))) AS [LEFT QTY]  FROM TSPL_NRGP_REQUEST_HEAD NH LEFT JOIN TSPL_NRGP_REQUEST_DETAIL ND ON NH.BOOKING_NO = ND.BOOKING_NO LEFT JOIN TSPL_RGP_DETAIL IM ON ND.Item_Code = IM.Item_Code WHERE NH.BOOKING_NO='" + NRGPBookingNo + "'"))
            If (NRGPBookingNo IsNot Nothing AndAlso NRGPBookingNo.Length > 0) Then
                nrgpReqFnd.Value = NRGPBookingNo
                If chkItemQty > 0 Then
                    FillGrid_By_NRGPRequestNo(NRGPBookingNo, NavigatorType.Current)
                Else
                    Dim itemCode As String = clsDBFuncationality.getSingleValue("SELECT D.Item_Code FROM TSPL_NRGP_REQUEST_HEAD H LEFT JOIN TSPL_NRGP_REQUEST_DETAIL D ON H.BOOKING_NO = D.BOOKING_NO WHERE H.BOOKING_NO = '" + NRGPBookingNo + "'")
                    clsCommon.MyMessageBoxShow("Item " + itemCode + " is totally Consumed against Request No " + NRGPBookingNo)
                    AddNew()
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub
    'KUNAL > DATE : 16 - NOV - 2016
    Private Sub FillGrid_By_NRGPRequestNo(ByVal strCode As String, ByVal NavType As NavigatorType)
        Try
            Dim obj As cls_TSPL_NRGP_REQUEST_HEAD = New cls_TSPL_NRGP_REQUEST_HEAD()
            obj = cls_TSPL_NRGP_REQUEST_HEAD.GetData(strCode, Nothing, NavType, "Request")

            If (cls_TSPL_NRGP_REQUEST_HEAD.ObjList IsNot Nothing AndAlso cls_TSPL_NRGP_REQUEST_HEAD.ObjList.Count > 0) Then
                For Each objTr As cls_TSPL_NRGP_REQUEST_DETAIL In cls_TSPL_NRGP_REQUEST_HEAD.ObjList

                    gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = clsCommon.myCstr(Me.gv1.Rows.Count)
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value = clsCommon.myCdbl(objTr.BOOK_QTY)
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colUnit).Value = clsCommon.myCstr(objTr.BOOK_QTY_UOM)

                    gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value = objTr.Itemcode
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colIName).Value = objTr.Itemname
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colHSNNo).Value = clsItemMaster.GetItemHSNCode(objTr.Itemcode, Nothing)
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colRate).Value = clsCommon.myCdbl(objTr.BOOK_Rate)

                Next

            Else
                gv1.Rows.AddNew()
            End If


            If obj.VEN_CODE.Length > 0 AndAlso obj.VEN_CODE IsNot Nothing Then
                txtVendorNo.Value = obj.VEN_CODE
                lblVendorName.Text = clsDBFuncationality.getSingleValue("select TSPL_VENDOR_MASTER.Vendor_Name  from TSPL_VENDOR_MASTER where Vendor_Code = '" + obj.VEN_CODE + "'")
            Else
                txtVendorNo.Value = obj.CSA_CODE
                lblVendorName.Text = obj.CSA_NAME
            End If
            txtLocation.Value = obj.Location_Code
            lblLocation.Text = clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code ='" + obj.Location_Code + "'")
            txtNRGPNo.Value = obj.BOOKING_NO


        Catch ex As Exception
            isInsideLoadData = False

            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Dim IsSettingOn As Boolean = False
    Private Sub cboDocType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.Data.PositionChangedEventArgs) Handles cboDocType.SelectedIndexChanged, ddlBilling.SelectedIndexChanged
        Try

            If Not IsformLoad Then
                HideUnhideColumn()
                gbRGPNRGP.Enabled = True
                txtNRGPNo.Enabled = False
                txtNRGPNo.Value = ""
                If (cboDocType.SelectedValue = "NRGP") Then
                    txtManPoNo.Enabled = True
                    dtManPoDate.Enabled = True
                    chkAgainst_Sale.Enabled = True
                    RadGroupBox1.Enabled = False
                    GrpRej.Visible = True
                    ChkRejLoc.Visible = True
                    txtVendorNo.Enabled = False
                    If chkthirdparty.Checked Then
                        grp3rdParty.Visible = True
                        txtVendorNo.Enabled = False
                    Else
                        grp3rdParty.Visible = False
                        txtVendorNo.Enabled = True
                    End If
                    ' KUNAL > UDIL > DATE 16 NOV 2016
                    IsSettingOn = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.FindNRGP_Request, clsFixedParameterType.FindNRGP_Request, Nothing)) = 1, True, False)
                    If IsSettingOn Then
                        lblNrgpReq.Visible = True
                        nrgpReqFnd.Visible = True
                    End If

                ElseIf cboDocType.SelectedValue = "NRGPR" Then
                    txtManPoNo.Enabled = False
                    dtManPoDate.Enabled = False
                    gbRGPNRGP.Enabled = False
                    txtNRGPNo.Enabled = True
                    lblNrgpRgpNo.Text = "NRGP NO."
                    GrpRej.Visible = False
                    ChkRejLoc.Visible = False
                    txtVendorNo.Value = ""
                    lblVendorName.Text = ""
                    txtLocation.Value = ""
                    lblLocation.Text = ""
                    txtSRNNo.Value = ""
                    txtVendorNo.Enabled = True
                    lblNrgpReq.Visible = False
                    nrgpReqFnd.Visible = False
                ElseIf cboDocType.SelectedValue = "RGPR" Then
                    txtManPoNo.Enabled = False
                    dtManPoDate.Enabled = False
                    txtNRGPNo.Enabled = True
                    lblNrgpRgpNo.Text = "RGP NO."
                    grp3rdParty.Visible = False
                    txtVendorNo.Enabled = False
                    chkAgainst_Sale.Enabled = False
                    chkAgainst_Sale.Checked = False
                    RadLabel2.Text = "Vendor No"
                    GrpRej.Visible = False
                    ChkRejLoc.Visible = False
                    txtVendorNo.Value = ""
                    lblVendorName.Text = ""
                    txtLocation.Value = ""
                    lblLocation.Text = ""
                    txtSRNNo.Value = ""
                    txtVendorNo.Enabled = False
                    lblNrgpReq.Visible = False
                    nrgpReqFnd.Visible = False
                    If RadGroupBox1.Visible Then
                        RadGroupBox1.Enabled = False
                    End If
                Else
                    txtManPoNo.Enabled = False
                    dtManPoDate.Enabled = False
                    grp3rdParty.Visible = False
                    txtVendorNo.Enabled = True
                    chkAgainst_Sale.Enabled = False
                    chkAgainst_Sale.Checked = False
                    RadLabel2.Text = "Vendor No"
                    GrpRej.Visible = False
                    ChkRejLoc.Visible = False
                    txtVendorNo.Value = ""
                    lblVendorName.Text = ""
                    txtLocation.Value = ""
                    lblLocation.Text = ""
                    txtSRNNo.Value = ""
                    txtVendorNo.Enabled = True
                    lblNrgpReq.Visible = False
                    nrgpReqFnd.Visible = False
                    If RadGroupBox1.Visible Then
                        RadGroupBox1.Enabled = True
                    End If
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub HideUnhideColumn()
        gv1.Columns(colBalQty).IsVisible = False
        gv1.Columns(colRetQty).IsVisible = False
        If clsCommon.CompairString(cboDocType.SelectedValue, "RGP") = CompairStringResult.Equal Then
            gv1.Columns(colLastRGPNRGP).IsVisible = True
            gv1.Columns(colDate).IsVisible = True
            chkAgainst_Sale.Enabled = False
            chkAgainst_Sale.Checked = False
            ''richa Ticket No BM00000003061 on 01/08/2014
            chkAgainstJobWork.Enabled = True
            '-------------------------------------------
            '' Anubhooti 10-Dec-2014 BM00000003662 (Item Conversion Type)
            If chkAgainstJobWork.Checked = True Then
                CmbItemConType.Visible = True
                LblItemConType.Visible = True
            Else
                CmbItemConType.Visible = False
                LblItemConType.Visible = False
            End If
            ''
        Else
            gv1.Columns(colLastRGPNRGP).IsVisible = False
            gv1.Columns(colDate).IsVisible = False
            chkAgainst_Sale.Enabled = True
            ''richa Ticket No BM00000003061 on 01/08/2014
            chkAgainstJobWork.Enabled = False
            chkAgainstJobWork.Checked = False
            '-------------------------------------------
            '' Anubhooti 10-Dec-2014 BM00000003662 (Item Conversion Type)
            CmbItemConType.Visible = False
            LblItemConType.Visible = False
            ''
            If clsCommon.CompairString(cboDocType.SelectedValue, "NRGPR") = CompairStringResult.Equal Then
                gv1.Columns(colBalQty).IsVisible = True
                gv1.Columns(colRetQty).IsVisible = True
            End If
        End If

        txtPoNo.Enabled = False
        dtPoDate.Value = clsCommon.GETSERVERDATE()
        txtScheduleNo.Enabled = False
        If chkAgainstJobWork.Checked = True AndAlso IsRGPAfterPO Then
            txtPoNo.Enabled = True
            dtPoDate.Enabled = True
            txtScheduleNo.Enabled = IIf(IsPOScheduleOn = True, True, False)
        End If
    End Sub

    Private Sub txtDepartment__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtDepartment._MYValidating
        Dim qry As String = "select Segment_code as [Code], Description as [Name] from TSPL_GL_SEGMENT_CODE "
        Dim WhrCls As String = "Seg_No  ='3'"
        txtDepartment.Value = clsCommon.ShowSelectForm("EmployeeFinder", qry, "Code", WhrCls, txtDepartment.Value, "Code", isButtonClicked)
        If clsCommon.myLen(txtDepartment.Value) > 0 Then
            qry += "where Seg_No  ='3' and Segment_code='" + txtDepartment.Value + "' "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                lblDepartment.Text = clsCommon.myCstr(dt.Rows(0)("Name"))
            End If
        Else
            txtDepartment.Value = ""
            lblDepartment.Text = ""
        End If
    End Sub

    Private Sub chkAgainst_Sale_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkAgainst_Sale.Click
        txtVendorNo.Value = ""
        lblVendorName.Text = ""
        If (chkAgainst_Sale.Checked) Then
            RadLabel2.Text = "Vendor No"
        Else
            RadLabel2.Text = "Customer No"
        End If
    End Sub

    Private Sub chkNonInventoryItem_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkNonInventoryItem.ToggleStateChanged
        ControlNonInventory(True)
    End Sub

    Private Sub ControlNonInventory(ByVal Ischecked As Boolean)
        If Not isInsideLoadData Then
            If Ischecked Then
                LoadBlankGrid()
                gv1.Rows.AddNew()
            End If
            'If chkNonInventoryItem.Checked Then
            '    chkAgainstCCTransfer.Visible = True
            '    RadLabel2.Visible = False
            '    txtVendorNo.Visible = False
            '    lblVendorName.Visible = False
            '    txtVendorNo.Value = ""
            '    lblToLocation.Visible = True
            '    fndToLocationCode.Visible = True
            '    lblToLocationCode.Visible = True
            '    fndToLocationCode.Value = ""
            'Else
            '    chkAgainstCCTransfer.Visible = False
            '    chkAgainstCCTransfer.Checked = False
            '    lblToLocation.Visible = False
            '    fndToLocationCode.Visible = False
            '    lblToLocationCode.Visible = False
            '    fndToLocationCode.Value = ""
            '    RadLabel2.Visible = True
            '    txtVendorNo.Visible = True
            '    lblVendorName.Visible = True
            'End If
            If chkNonInventoryItem.Checked Then
                chkNonInventoryItem.Checked = True
                chkAgainstCCTransfer.Visible = True
            Else
                chkNonInventoryItem.Checked = False
                chkAgainstCCTransfer.Visible = False
            End If
            If chkNonInventoryItem.Checked AndAlso chkAgainstCCTransfer.Checked Then
                chkAgainstCCTransfer.Checked = True
                fndToLocationCode.Value = ""
                lblToLocationCode.Text = ""
                lblToLocation.Visible = True
                fndToLocationCode.Visible = True
                lblToLocationCode.Visible = True
            Else
                chkAgainstCCTransfer.Checked = False
                fndToLocationCode.Value = ""
                lblToLocationCode.Text = ""

                lblToLocation.Visible = False
                fndToLocationCode.Visible = False
                lblToLocationCode.Visible = False

            End If
        End If
    End Sub

    Private Sub fndCostCentre__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndCostCentre._MYValidating
        Dim Qry As String = "select Segment_code as [Code], Description,Segment_name as [Segment Name] From TSPL_GL_SEGMENT_CODE  "
        Dim WhrCls As String = " seg_no <>'7'  "
        fndCostCentre.Value = clsCommon.ShowSelectForm("Vehicle Selector", Qry, "Code", WhrCls, fndCostCentre.Value, "", isButtonClicked)
        txtCostCentre.Text = clsDBFuncationality.getSingleValue("select Description From TSPL_GL_SEGMENT_CODE Where   Segment_Code= '" + fndCostCentre.Value + "'")
    End Sub

    Private Sub chlCust_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chlCust.Click
        txtVendorNo.Value = ""
        lblVendorName.Text = ""
        If (chlCust.Checked) Then
            RadLabel2.Text = "Vendor No"
        Else
            RadLabel2.Text = "Customer No"
        End If
    End Sub

    Private Sub btnReverse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReverse.Click
        Try
            If common.clsCommon.MyMessageBoxShow("Reverse and Unpost the Current Document" + Environment.NewLine + "Are you sure", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                If clsRGPHead.ReverseAndUnpost(txtDocNo.Value) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Successfully Reversed and Recreated", Me.Text)
                    LoadData(txtDocNo.Value, NavigatorType.Current, False)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub setBalance()
        UcItemBalance1.ItemCode = clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value)
        UcItemBalance1.ItemName = clsCommon.myCstr(gv1.CurrentRow.Cells(colIName).Value)
        'UcItemBalance1.ItemMRP = clsCommon.myCdbl(gv1.CurrentRow.Cells(colMRP).Value)
        UcItemBalance1.LocationCode = txtLocation.Value
        UcItemBalance1.LocationName = lblLocation.Text
        UcItemBalance1.UOM = clsCommon.myCstr(gv1.CurrentRow.Cells(colUnit).Value)
        UcItemBalance1.TransNo = txtDocNo.Value
        UcItemBalance1.TransDate = txtDate.Value
        UcItemBalance1.ShowPOQty = False
        UcItemBalance1.RefreshData()
    End Sub

    Private Sub gv1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gv1.Click
        If gv1.CurrentRow IsNot Nothing Then
            setBalance()
        End If
    End Sub

    Private Sub gv1_CurrentRowChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.CurrentRowChangedEventArgs) Handles gv1.CurrentRowChanged
        If gv1.CurrentRow IsNot Nothing AndAlso Not e.CurrentRow.Index < 0 Then
            setBalance()
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
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully", "Information")
            End If
            ''stuti regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub deleteLayout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles deleteLayout.Click
        clsGridLayout.DeleteData(MyBase.Form_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", "Information")
    End Sub

    Private Sub chkthirdparty_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkthirdparty.ToggleStateChanged
        txtLocation.Value = ""
        lblLocation.Text = ""
        cboItemType.Enabled = False
        If chkthirdparty.Checked Then
            cboItemType.Enabled = True
            If clsCommon.CompairString(cboDocType.SelectedValue, "NRGP") = CompairStringResult.Equal Then
                grp3rdParty.Visible = True
                txtVendorNo.Enabled = False
            Else
                grp3rdParty.Visible = False
                txtVendorNo.Enabled = True
            End If
        End If
    End Sub

    Private Sub txtsrnlocation_code__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtsrnlocation_code._MYValidating
        Dim qry As String = "select Location_Code as Code,Location_Desc as Name from TSPL_LOCATION_MASTER "
        Dim WhrCls As String = " Location_Type='Physical' and isnull(vendor_code,'')='' " 'location comes other than third party
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            WhrCls += "  and  Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
        End If


        txtsrnlocation_code.Value = clsCommon.ShowSelectForm("VendorMasteidfndr", qry, "Code", WhrCls, txtsrnlocation_code.Value, "Code", isButtonClicked)
        txtsrnlocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" + txtsrnlocation_code.Value + "'"))
    End Sub

    Private Sub txtNRGPNo__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtNRGPNo._MYValidating
        If clsCommon.CompairString(cboDocType.SelectedValue, "NRGPR") = CompairStringResult.Equal Then
            qry = "Select * from (" &
       " Select DISTINCT RGP_No, MAX(RGP_Date) as RGP_Date, MAX(Location) as Location, MAX(Location_Desc) as Location_Desc, MAX(Vendor_Code) as Vendor_Code, MAX(Vendor_Name) as Vendor_Name, MAX(IsThirdParty) as IsThirdParty from (" &
       " Select TSPL_RGP_HEAD.RGP_No, TSPL_RGP_HEAD.RGP_Date, TSPL_RGP_HEAD.Location, TSPL_LOCATION_MASTER.Location_Desc, TSPL_RGP_HEAD.Vendor_Code, TSPL_RGP_HEAD.Vendor_Name, Case When IsThirdParty='Y' Then 'Yes' Else 'No' End as IsThirdParty, TSPL_RGP_DETAIL.Item_Code, TSPL_RGP_DETAIL.Unit_code, TSPL_RGP_DETAIL.RGP_Qty from TSPL_RGP_HEAD LEFT OUTER JOIN TSPL_RGP_DETAIL ON TSPL_RGP_DETAIL.RGP_No=TSPL_RGP_HEAD.RGP_No LEFT OUTER JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code=TSPL_RGP_HEAD.Location WHERE Doc_Type='NRGP' AND Status=1" &
        " AND (NOT EXISTS (SELECT Against_RGP FROM TSPL_SRN_HEAD WHERE Against_RGP=TSPL_RGP_HEAD.RGP_No) OR NOT EXISTS (SELECT Against_RGP_No FROM TSPL_GRN_HEAD WHERE Against_RGP_No=TSPL_RGP_HEAD.RGP_No) OR NOT EXISTS (SELECT Against_RGP_No FROM TSPL_MRN_HEAD WHERE Against_RGP_No=TSPL_RGP_HEAD.RGP_No)) " &
       " UNION ALL" &
       " Select TSPL_RGP_HEAD.Against_NRGP as RGP_No, TSPL_RGP_HEAD.RGP_Date, TSPL_RGP_HEAD.Location, TSPL_LOCATION_MASTER.Location_Desc, TSPL_RGP_HEAD.Vendor_Code, TSPL_RGP_HEAD.Vendor_Name, Case When IsThirdParty='Y' Then 'Yes' Else 'No' End as IsThirdParty, TSPL_RGP_DETAIL.Item_Code, TSPL_RGP_DETAIL.Unit_code, TSPL_RGP_DETAIL.RGP_Qty*-1 as RGP_Qty from TSPL_RGP_HEAD LEFT OUTER JOIN TSPL_RGP_DETAIL ON TSPL_RGP_DETAIL.RGP_No=TSPL_RGP_HEAD.RGP_No LEFT OUTER JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code=TSPL_RGP_HEAD.Location WHERE Doc_Type='NRGPR'" &
       " ) XXX Group By RGP_No, Item_Code, Unit_code having SUM(RGP_Qty)>0" &
       " ) YYY"
        Else
            qry = "Select * from (" &
       " Select DISTINCT RGP_No, MAX(RGP_Date) as RGP_Date, MAX(Location) as Location, MAX(Location_Desc) as Location_Desc, MAX(Vendor_Code) as Vendor_Code, MAX(Vendor_Name) as Vendor_Name, MAX(IsThirdParty) as IsThirdParty from (" &
       " Select TSPL_RGP_HEAD.RGP_No, TSPL_RGP_HEAD.RGP_Date, TSPL_RGP_HEAD.Location, TSPL_LOCATION_MASTER.Location_Desc, TSPL_RGP_HEAD.Vendor_Code, TSPL_RGP_HEAD.Vendor_Name, Case When IsThirdParty='Y' Then 'Yes' Else 'No' End as IsThirdParty, TSPL_RGP_DETAIL.Item_Code, TSPL_RGP_DETAIL.Unit_code, TSPL_RGP_DETAIL.RGP_Qty from TSPL_RGP_HEAD LEFT OUTER JOIN TSPL_RGP_DETAIL ON TSPL_RGP_DETAIL.RGP_No=TSPL_RGP_HEAD.RGP_No LEFT OUTER JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code=TSPL_RGP_HEAD.Location WHERE Doc_Type='RGP' AND Status=1" &
       " AND (NOT EXISTS (SELECT Against_RGP FROM TSPL_SRN_HEAD WHERE Against_RGP=TSPL_RGP_HEAD.RGP_No) OR NOT EXISTS (SELECT Against_RGP_No FROM TSPL_GRN_HEAD WHERE Against_RGP_No=TSPL_RGP_HEAD.RGP_No) OR NOT EXISTS (SELECT Against_RGP_No FROM TSPL_MRN_HEAD WHERE Against_RGP_No=TSPL_RGP_HEAD.RGP_No)) " &
       " UNION ALL" &
       " Select TSPL_RGP_HEAD.Against_RGP as RGP_No, TSPL_RGP_HEAD.RGP_Date, TSPL_RGP_HEAD.Location, TSPL_LOCATION_MASTER.Location_Desc, TSPL_RGP_HEAD.Vendor_Code, TSPL_RGP_HEAD.Vendor_Name, Case When IsThirdParty='Y' Then 'Yes' Else 'No' End as IsThirdParty, TSPL_RGP_DETAIL.Item_Code, TSPL_RGP_DETAIL.Unit_code, TSPL_RGP_DETAIL.RGP_Qty*-1 as RGP_Qty from TSPL_RGP_HEAD LEFT OUTER JOIN TSPL_RGP_DETAIL ON TSPL_RGP_DETAIL.RGP_No=TSPL_RGP_HEAD.RGP_No LEFT OUTER JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code=TSPL_RGP_HEAD.Location WHERE Doc_Type='RGPR'" &
       " ) XXX Group By RGP_No, Item_Code, Unit_code having SUM(RGP_Qty)>0" &
       " ) YYY"
        End If
        txtNRGPNo.Value = clsCommon.ShowSelectForm("NRGP@GRPNRGP", qry, "RGP_No", "", txtNRGPNo.Value, "RGP_No", isButtonClicked)
        LoadData(txtNRGPNo.Value, NavigatorType.Current, True)
    End Sub


    '' Anubhooti 07-Oct-2014 BM00000003663
    Private Sub ChkRejLoc_CheckStateChanged_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkRejLoc.CheckStateChanged
        If ChkRejLoc.Checked = True Then
            GrpRej.Enabled = True
            txtVendorNo.Enabled = False
            txtSRNNo.Value = ""
            txtVendorNo.Value = ""
            lblVendorName.Text = ""
            txtLocation.Value = ""
            lblLocation.Text = ""
        Else
            txtSRNNo.Value = ""
            txtVendorNo.Enabled = True
            GrpRej.Enabled = False
        End If
    End Sub

    Private Sub txtSRNNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtSRNNo._MYValidating
        'Dim qry As String = "select TSPL_SRN_HEAD.SRN_No As [Code],TSPL_SRN_HEAD.Vendor_Code AS [Vendor Code],TSPL_SRN_HEAD.Bill_To_Location As [Bill To Location],TSPL_SRN_HEAD.Ship_To_Location AS [Ship To Location] From TSPL_SRN_HEAD Left Outer Join TSPL_SRN_DETAIL On TSPL_SRN_HEAD.SRN_No =TSPL_SRN_DETAIL.SRN_No "
        'Dim WhrCls As String = " Bill_To_Location in (Select ISNULL(Location_Code ,'') As Location_Code From TSPL_LOCATION_MASTER Where Rejected_Location='" & clsCommon.myCstr(txtLocation.Value) & "') AND TSPL_SRN_DETAIL.Rejected_Qty >0 Group By TSPL_SRN_HEAD.SRN_No,TSPL_SRN_HEAD.Vendor_Code,TSPL_SRN_HEAD.Bill_To_Location,TSPL_SRN_HEAD.Ship_To_Location  " 'location comes other than third party
        If clsCommon.myLen(txtLocation.Value) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Please select location first.", Me.Text)
            txtLocation.Focus()
            Exit Sub
        End If
        'Dim qry1 As String = "SELECT SRN_No  As [SRN No] FROM (select aa.SRN_No, SUM(Qty) as Qty FROM ( select TSPL_SRN_DETAIL.SRN_No,  Item_Code, Rejected_Qty As Qty,1 as RI  From tspl_SRN_Head  Left outer join TSPL_SRN_DETAIL on TSPL_SRN_DETAIL.SRN_No =tspl_SRN_Head.SRN_No Where TSPL_SRN_DETAIL.Rejected_Qty >0 AND tspl_SRN_Head.Bill_To_Location in (Select ISNULL(Location_Code ,'') As Location_Code From TSPL_LOCATION_MASTER Where Rejected_Location='" & clsCommon.myCstr(txtLocation.Value) & "') Union All select SRN_No, Item_Code,RGP_Qty   As Qty,-1 as RI From TSPL_RGP_DETAIL LEFT OUTER JOIN TSPL_RGP_HEAD  ON TSPL_RGP_HEAD.RGP_No  =TSPL_RGP_DETAIL.RGP_No where TSPL_RGP_HEAD.SRN_No='' and TSPL_RGP_HEAD.RGP_No not IN(''))  aa group by SRN_No ) XXX"
        Dim qry As String = " SELECT SRN_No  As [SRN No] FROM (select aa.SRN_No, aa.Item_Code, SUM(Qty* RI ) as Qty FROM ( select TSPL_SRN_DETAIL.SRN_No,  Item_Code, Rejected_Qty As Qty,1 as RI  From tspl_SRN_Head  Left outer join TSPL_SRN_DETAIL on TSPL_SRN_DETAIL.SRN_No =tspl_SRN_Head.SRN_No Where TSPL_SRN_DETAIL.Rejected_Qty >0 AND tspl_SRN_Head.Bill_To_Location in (Select ISNULL(Location_Code ,'') As Location_Code From TSPL_LOCATION_MASTER Where Rejected_Location='" & clsCommon.myCstr(txtLocation.Value) & "') Union All select SRN_No, Item_Code,RGP_Qty   As Qty,-1 as RI From TSPL_RGP_DETAIL LEFT OUTER JOIN TSPL_RGP_HEAD  ON TSPL_RGP_HEAD.RGP_No  =TSPL_RGP_DETAIL.RGP_No)  aa group by SRN_No ,Item_Code  ) XXX   "
        txtSRNNo.Value = clsCommon.ShowSelectForm("SRNNRGP", qry, "SRN No", " Qty >0 group by SRN_No ", txtSRNNo.Value, "SRN_No", isButtonClicked)
        If clsCommon.myLen(txtSRNNo.Value) > 0 Then
            txtVendorNo.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT isnull(Vendor_Code,'') As Vendor_Code FROM TSPL_SRN_HEAD Where SRN_No ='" & clsCommon.myCstr(txtSRNNo.Value) & "'"))
            If clsCommon.myLen(txtVendorNo.Value) > 0 Then
                lblVendorName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT isnull(Vendor_Name,'') As Vendor_Name FROM TSPL_VENDOR_MASTER Where Vendor_Code ='" & clsCommon.myCstr(txtVendorNo.Value) & "'"))
            Else
                lblVendorName.Text = ""
            End If
            LoadSRNItems(clsCommon.myCstr(txtSRNNo.Value))
        Else
            txtVendorNo.Value = ""
            lblVendorName.Text = ""
            LoadBlankGrid()
        End If

    End Sub

    Private Function ReturnBalQty(ByVal strSRNNo As String, ByVal strItemCode As String, ByVal strRGPNo As String, ByVal strLoc As String, ByVal Doc_Type As String) As Double
        Dim BalQty As Double = 0
        BalQty = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("SELECT  SUM(Qty*RI) as Qty FROM ( select TSPL_SRN_DETAIL.SRN_No,  Item_Code, Rejected_Qty As Qty,1 as RI  From tspl_SRN_Head  Left outer join TSPL_SRN_DETAIL on TSPL_SRN_DETAIL.SRN_No =tspl_SRN_Head.SRN_No Where TSPL_SRN_DETAIL.Rejected_Qty >0 AND tspl_SRN_Head.Bill_To_Location in (Select ISNULL(Location_Code ,'') As Location_Code From TSPL_LOCATION_MASTER Where Rejected_Location='" & strLoc & "') And tspl_SRN_Head.SRN_No='" & strSRNNo & "' and TSPL_SRN_DETAIL.Item_Code ='" & strItemCode & "' " &
        " Union All " &
        " select SRN_No, Item_Code,RGP_Qty   As Qty,-1 as RI From TSPL_RGP_DETAIL LEFT OUTER JOIN TSPL_RGP_HEAD  ON TSPL_RGP_HEAD.RGP_No  =TSPL_RGP_DETAIL.RGP_No where TSPL_RGP_HEAD.SRN_No='" & strSRNNo & "'  and TSPL_RGP_DETAIL.Item_Code ='" & strItemCode & "'   and TSPL_RGP_HEAD.RGP_No not IN('" & strRGPNo & "') AND Doc_Type='" + Doc_Type + "' " &
        " )  aa group by SRN_No,Item_Code having SUM(Qty*RI) >0"))
        Return BalQty
    End Function

    Private Sub LoadSRNItems(ByVal strSRNNo As String)
        gv1.Rows.Clear()
        isInsideLoadData = True
        Dim QryBal As String = ""
        Dim BalQty As Double
        Dim Qry As String = "select * from  TSPL_SRN_DETAIL  Where SRN_No='" & strSRNNo & "' AND Rejected_Qty >0 "

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            For Each dr As DataRow In dt.Rows

                QryBal = ""
                QryBal = "SELECT  SUM(Qty*RI) as Qty FROM ( select TSPL_SRN_DETAIL.SRN_No,  Item_Code, Rejected_Qty As Qty,1 as RI  From tspl_SRN_Head  Left outer join TSPL_SRN_DETAIL on TSPL_SRN_DETAIL.SRN_No =tspl_SRN_Head.SRN_No Where TSPL_SRN_DETAIL.Rejected_Qty >0 AND tspl_SRN_Head.Bill_To_Location in (Select ISNULL(Location_Code ,'') As Location_Code From TSPL_LOCATION_MASTER Where Rejected_Location='" & clsCommon.myCstr(txtLocation.Value) & "') And tspl_SRN_Head.SRN_No='" & clsCommon.myCstr(txtSRNNo.Value) & "' and TSPL_SRN_DETAIL.Item_Code ='" & clsCommon.myCstr(dr("Item_Code")) & "'"
                'If clsCommon.myLen(strItemCode) > 0 Then
                '    QryBal += " and TSPL_SRN_DETAIL.Item_Code ='" & strItemCode & "' "
                'End If
                QryBal += " Union All  "
                QryBal += " select SRN_No, Item_Code,RGP_Qty   As Qty,-1 as RI From TSPL_RGP_DETAIL LEFT OUTER JOIN TSPL_RGP_HEAD  ON TSPL_RGP_HEAD.RGP_No  =TSPL_RGP_DETAIL.RGP_No where TSPL_RGP_HEAD.SRN_No='" & clsCommon.myCstr(txtSRNNo.Value) & "' AND TSPL_RGP_DETAIL.Item_Code ='" & clsCommon.myCstr(dr("Item_Code")) & "' "
                'If clsCommon.myLen(strItemCode) > 0 Then
                '    QryBal += " and TSPL_RGP_DETAIL.Item_Code ='" & strItemCode & "' and TSPL_RGP_HEAD.RGP_No not IN('" & strRGPNo & "') "
                'End If
                QryBal += " )  aa group by SRN_No,Item_Code having SUM(Qty*RI) >0 "
                BalQty = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(QryBal))
                If BalQty > 0 Then
                    gv1.Rows.AddNew()
                    gv1.Columns(colRejSRNQty).IsVisible = True
                    gv1.Columns(colLineNo).ReadOnly = True
                    gv1.Columns(colICode).ReadOnly = True
                    gv1.Columns(colIName).ReadOnly = True
                    gv1.Columns(colRejSRNQty).ReadOnly = True
                    'gv1.Columns(colmrp ).ReadOnly = True
                    'gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = clsCommon.myCstr(dr("Line_No"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = clsCommon.myCstr(gv1.Rows.Count)
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value = clsCommon.myCstr(dr("Item_Code"))
                    'gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Tag = objTr.arrBatchItem
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colIName).Value = clsCommon.myCstr(dr("Item_Desc"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colHSNNo).Value = clsItemMaster.GetItemHSNCode(clsCommon.myCstr(dr("Item_Code")), Nothing)
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colUnit).Value = clsCommon.myCstr(dr("Unit_Code"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colmrp).Value = clsCommon.myCdbl(dr("MRP"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colRate).Value = clsCommon.myCdbl(dr("Item_Cost"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colRejSRNQty).Value = clsCommon.myCdbl(BalQty)
                End If

            Next
        Else
            clsCommon.MyMessageBoxShow(Me, "No data found", Me.Text)
            gv1.Columns(colRejSRNQty).IsVisible = False
        End If
        isInsideLoadData = False
    End Sub
    '' Anubhooti 11-Dec-2014
    Private Sub chkAgainstJobWork_CheckStateChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkAgainstJobWork.CheckStateChanged
        If chkAgainstJobWork.Checked = True AndAlso clsCommon.CompairString(cboDocType.SelectedValue, "RGP") = CompairStringResult.Equal Then
            LblItemConType.Visible = True
            CmbItemConType.Visible = True
        Else
            LblItemConType.Visible = False
            CmbItemConType.Visible = False
        End If
    End Sub

    Private Sub chkAgainstJobWork_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkAgainstJobWork.ToggleStateChanged
        txtPoNo.Enabled = chkAgainstJobWork.Checked
        txtScheduleNo.Enabled = chkAgainstJobWork.Checked
        If IsRGPAfterPO Then
            'RadGroupBox3.Visible = chkAgainstJobWork.Checked
            chkAsPerBOM.Enabled = chkAgainstJobWork.Checked
            If chkAgainstJobWork.Checked Then
                RadPageViewPage3.Item.Visibility = ElementVisibility.Visible
                RadPageView2.SelectedPage = RadPageViewPage2
            Else
                RadPageViewPage3.Item.Visibility = ElementVisibility.Collapsed
                RadPageView2.SelectedPage = RadPageViewPage2
            End If
        End If

    End Sub

    ''=========================================================================================
#Region "Schedule and PO"

    Private Sub txtPoNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtPoNo._MYValidating
        Try
            isInsideLoadData = True
            btnSameasAbove.Enabled = True

            If Not chkAgainstJobWork.Checked Then
                RadPageView1.SelectedPage = RadPageViewPage1
                Throw New Exception("select first against job work.")
            End If
            If clsCommon.myLen(txtScheduleNo.Value) > 0 Then ' if sch. used then no po used
                Exit Sub
            End If
            gv1.Columns(colPO_Sch_Qty).HeaderText = "PO Quantity"

            Dim frm As New frmPendingPO()
            frm.strFormType = Me.Form_ID
            frm.PurchaseOrder_Type = "J"
            frm.VendorCode = clsCommon.myCstr(txtVendorNo.Value)
            frm.VendorName = clsCommon.myCstr(lblVendorName.Text)
            frm.strCurrCode = clsCommon.myCstr(txtDocNo.Value)
            frm.Is_From_RGP = True
            frm.ShowDialog()

            gv_PO.Rows.Clear()

            If frm.ArrReturn IsNot Nothing AndAlso frm.ArrReturn.Count > 0 Then
                Dim obj As clsPurchaseOrderHead = clsPurchaseOrderHead.GetData(frm.ArrReturn(0).PurchaseOrder_No, NavigatorType.Current)

                If obj IsNot Nothing AndAlso clsCommon.myLen(obj.PurchaseOrder_No) > 0 Then
                    txtModeOfTransport.Text = obj.Mode_Of_Transport
                    cboDocType.SelectedValue = "RGP"
                    txtVendorNo.Value = obj.Vendor_Code
                    lblVendorName.Text = obj.Vendor_Name
                    chkOnHold.Checked = obj.On_Hold
                    txtReason.Text = obj.Remarks
                    txtRemarks.Text = obj.Remarks
                    cboDocType.Enabled = False
                    txtLocation.Enabled = False
                    chkAgainst_Sale.Checked = False
                    chkRepair.Checked = obj.Is_Repair
                    lblDocumentAmt.Text = clsCommon.myFormat(obj.PO_Total_Amt)

                    txtDepartment.Value = obj.Dept
                    lblDepartment.Text = obj.Dept_Desc

                    chlCust.Checked = False
                    chkNonInventoryItem.Checked = False
                    ChkRejLoc.Checked = False
                    txtSRNNo.Value = ""

                    If chkAgainstJobWork.Checked = True Then 'rgp
                        LblItemConType.Visible = True
                        CmbItemConType.Visible = True
                        'CmbItemConType.SelectedValue = obj.Item_Conversion_Type
                    Else
                        LblItemConType.Visible = False
                        CmbItemConType.Visible = False
                    End If
                    ''
                    txtVendorNo.Value = obj.Vendor_Code
                    lblVendorName.Text = obj.Vendor_Name


                    chkthirdparty.Checked = False
                    txtsrnlocation_code.Value = ""
                    txtsrnlocation.Text = ""

                    txtLocation.Value = obj.Bill_To_Location
                    lblLocation.Text = obj.BillToLocationName
                    lblDepartment.Text = clsDBFuncationality.getSingleValue("select Description from TSPL_GL_SEGMENT_CODE where Seg_No  ='3' and Segment_code='" + txtDepartment.Value + "'")

                    GrpRej.Visible = False
                    ChkRejLoc.Visible = False
                    txtVendorNo.Enabled = True


                    txtPoNo.Value = obj.PurchaseOrder_No
                    dtPoDate.Value = obj.PurchaseOrder_Date
                    gv1.Columns(colPO_Sch_Qty).HeaderText = "PO Qty"
                    If clsCommon.myLen(txtPoNo.Value) > 0 Then
                        txtPoNo.Enabled = True
                    End If
                    txtScheduleNo.Value = ""
                    If clsCommon.myLen(txtScheduleNo.Value) > 0 Then
                        txtScheduleNo.Enabled = True
                        gv1.Columns(colPO_Sch_Qty).HeaderText = "Schedule Qty"
                    End If

                    If gv_PO.Rows.Count > 0 AndAlso clsCommon.myLen(gv_PO.Rows(gv_PO.Rows.Count - 1).Cells(colBOMIcode).Value) <= 0 Then
                        gv_PO.Rows.RemoveAt(gv_PO.Rows.Count - 1)
                    End If

                    chkAsPerBOM.Checked = False
                    btnSameasAbove.Enabled = False

                    For Each objTr As clsPurchaseOrderDetail In frm.ArrReturn
                        If IsValidItem(objTr) Then
                            gv_PO.Rows.AddNew()

                            gv_PO.Rows(gv_PO.Rows.Count - 1).Cells(colBOMLineNo).Value = gv_PO.Rows.Count
                            gv_PO.Rows(gv_PO.Rows.Count - 1).Cells(colBOMCode).Value = Nothing
                            gv_PO.Rows(gv_PO.Rows.Count - 1).Cells(colBOMDesc).Value = Nothing
                            gv_PO.Rows(gv_PO.Rows.Count - 1).Cells(colBOMIcode).Value = objTr.Item_Code
                            '--------------------------------------------------
                            Try
                                If chkthirdparty.Checked Then
                                    cboItemType.SelectedValue = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select item_type from tspl_item_master where item_code='" + objTr.Item_Code + "'"))
                                End If
                            Catch exx As Exception
                                cboItemType.SelectedValue = ""
                            End Try
                            '----------------------------------------------------
                            gv_PO.Rows(gv_PO.Rows.Count - 1).Cells(colBOMIname).Value = objTr.Item_Desc
                            gv_PO.Rows(gv_PO.Rows.Count - 1).Cells(colBOMIUnit).Value = objTr.Unit_code
                            gv_PO.Rows(gv_PO.Rows.Count - 1).Cells(colBOMQty).Value = objTr.Balance_Qty
                            gv_PO.Rows(gv_PO.Rows.Count - 1).Cells(colBOMBalanceQty).Value = objTr.Balance_Qty
                            gv_PO.Rows(gv_PO.Rows.Count - 1).Cells(colBOMRate).Value = objTr.Item_Cost
                            gv_PO.Rows(gv_PO.Rows.Count - 1).Cells(colBOMMRP).Value = objTr.MRP
                            gv_PO.Rows(gv_PO.Rows.Count - 1).Cells(colBOMAmt).Value = objTr.Amount
                            gv_PO.Rows(gv_PO.Rows.Count - 1).Cells(colBOMSpecification).Value = objTr.Specification
                            gv_PO.Rows(gv_PO.Rows.Count - 1).Cells(colBOMPONo).Value = objTr.PurchaseOrder_No
                            gv_PO.Rows(gv_PO.Rows.Count - 1).Cells(colBOMScheduleNo).Value = Nothing

                            If objCommonVar.IsKDIL Then
                                gv_PO.Rows(gv_PO.Rows.Count - 1).Cells(colBOMModule).Value = "Dairy"
                            Else
                                gv_PO.Rows(gv_PO.Rows.Count - 1).Cells(colBOMModule).Value = "Standard"
                            End If

                            If clsPurchaseOrderHead.RepariedPO(objTr.PurchaseOrder_No, Nothing) Then
                                gv1.Rows(gv1.RowCount - 1).Cells(colICode).Value = objTr.Item_Code
                                gv1.Rows(gv1.RowCount - 1).Cells(colIName).Value = objTr.Item_Desc
                                gv1.Rows(gv1.RowCount - 1).Cells(colHSNNo).Value = clsItemMaster.GetItemHSNCode(objTr.Item_Code, Nothing)
                                gv1.Rows(gv1.RowCount - 1).Cells(colUnit).Value = objTr.Unit_code
                                gv1.Rows(gv1.RowCount - 1).Cells(colIsSerialseItem).Value = clsItemMaster.IsSerializeItem(objTr.Item_Code)
                                gv1.Rows(gv1.RowCount - 1).Cells(colQty).Value = objTr.Balance_Qty
                                Dim dblUnitCost As Double = 0
                                Dim dblCostMethod As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Costing_Method as Costing_Method from TSPL_ITEM_MASTER left outer join TSPL_PURCHASE_ACCOUNTS on TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code=TSPL_ITEM_MASTER.Purchase_Class_Code where Item_Code='" & clsCommon.myCstr(objTr.Item_Code) & "' "))
                                If dblCostMethod <> 0 Then
                                    dblUnitCost = clsInventoryMovement.GetCost(dblCostMethod, clsCommon.myCstr(objTr.Item_Code), txtLocation.Value, 1, txtDate.Value, txtDate.Value, False, Nothing)
                                    gv1.Rows(gv1.RowCount - 1).Cells(colRate).Value = dblUnitCost
                                    gv1.Rows(gv1.RowCount - 1).Cells(colApproxCost).Value = dblUnitCost
                                Else
                                    gv1.Rows(gv1.RowCount - 1).Cells(colRate).Value = 0
                                    gv1.Rows(gv1.RowCount - 1).Cells(colApproxCost).Value = 0
                                End If
                                UpdateCurrentRow(gv1.RowCount - 1)
                                gv1.Rows.AddNew()
                            End If
                            UpdateAllTotals()
                        End If 'if cond. check item validity                        
                    Next

                End If 'if cond. of PO obj
            End If 'if cond. frm.arr

            RadPageView2.SelectedPage = RadPageViewPage2
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isInsideLoadData = False
        End Try
    End Sub

    Function IsValidItem(ByVal obj As clsPurchaseOrderDetail)
        For ii As Integer = 0 To gv_PO.RowCount - 1
            Dim strICode As String = clsCommon.myCstr(gv_PO.Rows(ii).Cells(colBOMIcode).Value)
            Dim strReqCode As String = clsCommon.myCstr(gv_PO.Rows(ii).Cells(colBOMPONo).Value)

            Dim strUOM As String = clsCommon.myCstr(gv_PO.Rows(ii).Cells(colBOMIUnit).Value)
            Dim dblMRP As Double = clsCommon.myCdbl(gv_PO.Rows(ii).Cells(colBOMMRP).Value)

            If clsCommon.myLen(strReqCode) > 0 AndAlso clsCommon.CompairString(strReqCode, obj.PurchaseOrder_No) = CompairStringResult.Equal AndAlso clsCommon.CompairString(strICode, obj.Item_Code) = CompairStringResult.Equal AndAlso clsCommon.CompairString(strUOM, obj.Unit_code) = CompairStringResult.Equal AndAlso dblMRP = obj.MRP Then
                Dim strMsg As String = "Already exist at row no:" + clsCommon.myCstr(ii + 1) + Environment.NewLine + " PO No : " + obj.PurchaseOrder_No + "  Item : " + obj.Item_Desc + Environment.NewLine + " UOM : " + obj.Unit_code + ""
                If dblMRP > 0 Then
                    strMsg = strMsg + Environment.NewLine + "MRP : " + clsCommon.myCstr(dblMRP)
                End If
                common.clsCommon.MyMessageBoxShow(strMsg)
                Return False
            End If
        Next
        Return True
    End Function

    Function IsValidScheduleItem(ByVal obj As clsPurchaseScheduleDetail)
        For ii As Integer = 0 To gv_PO.RowCount - 1
            Dim strICode As String = clsCommon.myCstr(gv_PO.Rows(ii).Cells(colBOMIcode).Value)
            Dim strReqCode As String = clsCommon.myCstr(gv_PO.Rows(ii).Cells(colBOMScheduleNo).Value)

            Dim strUOM As String = clsCommon.myCstr(gv_PO.Rows(ii).Cells(colBOMIUnit).Value)
            Dim dblMRP As Double = clsCommon.myCdbl(gv_PO.Rows(ii).Cells(colBOMMRP).Value)

            If clsCommon.myLen(strReqCode) > 0 AndAlso clsCommon.CompairString(strReqCode, obj.Document_Code) = CompairStringResult.Equal AndAlso clsCommon.CompairString(strICode, obj.Item_Code) = CompairStringResult.Equal AndAlso clsCommon.CompairString(strUOM, obj.Unit_Code) = CompairStringResult.Equal Then
                Dim strMsg As String = "Already exist at row no:" + clsCommon.myCstr(ii + 1) + Environment.NewLine + " Schedule No : " + obj.Document_Code + "  Item : " + obj.Item_Name + Environment.NewLine + " UOM : " + obj.Unit_Code + ""
                If dblMRP > 0 Then
                    strMsg = strMsg + Environment.NewLine + "MRP : " + clsCommon.myCstr(dblMRP)
                End If
                common.clsCommon.MyMessageBoxShow(strMsg)
                Return False
            End If
        Next
        Return True
    End Function

    Private Sub txtScheduleNo__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtScheduleNo._MYValidating
        Try
            isInsideLoadData = True
            btnSameasAbove.Enabled = True

            If Not chkAgainstJobWork.Checked Then
                RadPageView1.SelectedPage = RadPageViewPage1
                Throw New Exception("select first against job work.")
            End If
            If clsCommon.myLen(txtPoNo.Value) > 0 Then ' if po used then no sch. used
                Exit Sub
            End If
            gv1.Columns(colPO_Sch_Qty).HeaderText = "Schedule Quantity"

            Dim frm As New FrmPendingPOSchedule()
            frm.PurchaseOrder_Type = "J"
            frm.VendorCode = clsCommon.myCstr(txtVendorNo.Value)
            frm.VendorName = clsCommon.myCstr(lblVendorName.Text)
            frm.strCurrCode = clsCommon.myCstr(txtDocNo.Value)
            frm.strCurrDate = clsCommon.myCDate(txtDate.Text)
            frm.Is_From_RGP = True
            frm.ShowDialog()

            gv_PO.Rows.Clear()

            If frm.ArrReturn IsNot Nothing AndAlso frm.ArrReturn.Count > 0 Then
                Dim obj As clsPurchaseSchedule = clsPurchaseSchedule.GetData(frm.ArrReturn(0).Document_Code, NavigatorType.Current)

                If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Document_Code) > 0 Then
                    txtModeOfTransport.Text = ""
                    cboDocType.SelectedValue = "RGP"
                    txtVendorNo.Value = obj.Vendor_Code
                    lblVendorName.Text = obj.Vendor_Name
                    chkOnHold.Checked = False
                    txtReason.Text = ""
                    txtRemarks.Text = ""
                    cboDocType.Enabled = False
                    txtLocation.Enabled = True
                    chkAgainst_Sale.Checked = False

                    lblDocumentAmt.Text = Nothing

                    txtDepartment.Value = Nothing
                    lblDepartment.Text = Nothing

                    chlCust.Checked = False
                    chkNonInventoryItem.Checked = False
                    ChkRejLoc.Checked = False
                    txtSRNNo.Value = ""

                    If chkAgainstJobWork.Checked = True Then 'rgp
                        LblItemConType.Visible = True
                        CmbItemConType.Visible = True
                        'CmbItemConType.SelectedValue = obj.Item_Conversion_Type
                    Else
                        LblItemConType.Visible = False
                        CmbItemConType.Visible = False
                    End If


                    chkthirdparty.Checked = False
                    txtsrnlocation_code.Value = ""
                    txtsrnlocation.Text = ""

                    txtLocation.Value = Nothing
                    lblLocation.Text = Nothing
                    lblDepartment.Text = clsDBFuncationality.getSingleValue("select Description from TSPL_GL_SEGMENT_CODE where Seg_No  ='3' and Segment_code='" + txtDepartment.Value + "'")

                    GrpRej.Visible = False
                    ChkRejLoc.Visible = False
                    txtVendorNo.Enabled = True


                    txtPoNo.Value = Nothing
                    gv1.Columns(colPO_Sch_Qty).HeaderText = "PO Qty"
                    If clsCommon.myLen(txtPoNo.Value) > 0 Then
                        txtPoNo.Enabled = True
                    End If
                    txtScheduleNo.Value = obj.Document_Code
                    If clsCommon.myLen(txtScheduleNo.Value) > 0 Then
                        txtScheduleNo.Enabled = True
                        gv1.Columns(colPO_Sch_Qty).HeaderText = "Schedule Qty"
                    End If

                    If gv_PO.Rows.Count > 0 AndAlso clsCommon.myLen(gv_PO.Rows(gv_PO.Rows.Count - 1).Cells(colBOMIcode).Value) <= 0 Then
                        gv_PO.Rows.RemoveAt(gv_PO.Rows.Count - 1)
                    End If

                    chkAsPerBOM.Checked = False
                    btnSameasAbove.Enabled = False

                    For Each objTr As clsPurchaseScheduleDetail In frm.ArrReturn
                        If IsValidScheduleItem(objTr) Then
                            gv_PO.Rows.AddNew()

                            gv_PO.Rows(gv_PO.Rows.Count - 1).Cells(colBOMLineNo).Value = gv_PO.Rows.Count
                            gv_PO.Rows(gv_PO.Rows.Count - 1).Cells(colBOMCode).Value = Nothing
                            gv_PO.Rows(gv_PO.Rows.Count - 1).Cells(colBOMDesc).Value = Nothing
                            gv_PO.Rows(gv_PO.Rows.Count - 1).Cells(colBOMIcode).Value = objTr.Item_Code
                            '--------------------------------------------------
                            Try
                                If chkthirdparty.Checked Then
                                    cboItemType.SelectedValue = objTr.Item_Type
                                End If
                            Catch exx As Exception
                                cboItemType.SelectedValue = ""
                            End Try
                            '----------------------------------------------------
                            gv_PO.Rows(gv_PO.Rows.Count - 1).Cells(colBOMIname).Value = objTr.Item_Name
                            gv_PO.Rows(gv_PO.Rows.Count - 1).Cells(colBOMIUnit).Value = objTr.Unit_Code
                            gv_PO.Rows(gv_PO.Rows.Count - 1).Cells(colBOMQty).Value = 0
                            gv_PO.Rows(gv_PO.Rows.Count - 1).Cells(colBOMBalanceQty).Value = objTr.balance_qty
                            gv_PO.Rows(gv_PO.Rows.Count - 1).Cells(colBOMRate).Value = Nothing
                            gv_PO.Rows(gv_PO.Rows.Count - 1).Cells(colBOMMRP).Value = Nothing
                            gv_PO.Rows(gv_PO.Rows.Count - 1).Cells(colBOMAmt).Value = Nothing
                            gv_PO.Rows(gv_PO.Rows.Count - 1).Cells(colBOMSpecification).Value = objTr.Remarks
                            gv_PO.Rows(gv_PO.Rows.Count - 1).Cells(colBOMPONo).Value = Nothing
                            gv_PO.Rows(gv_PO.Rows.Count - 1).Cells(colBOMScheduleNo).Value = objTr.Document_Code

                            If objCommonVar.IsKDIL Then
                                gv_PO.Rows(gv_PO.Rows.Count - 1).Cells(colBOMModule).Value = "Dairy"
                            Else
                                gv_PO.Rows(gv_PO.Rows.Count - 1).Cells(colBOMModule).Value = "Standard"
                            End If
                        End If 'if cond. check item validity                        
                    Next

                End If 'if cond. of PO obj
            End If 'if cond. frm.arr

            RadPageView2.SelectedPage = RadPageViewPage3
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isInsideLoadData = False
        End Try
    End Sub

    Private Sub FillBOMItemInGrid(ByVal intRow As Integer, ByVal CellValueChanged As Boolean)
        Try
            If Not CellValueChanged Then
                '================first clean all that rows contains po no or sch no.
                For Each grow As GridViewRowInfo In gv_PO.Rows
                    RemoveRowOfBOM(grow.Index)
                Next

                For Each grow As GridViewRowInfo In gv_PO.Rows
                    Dim ICode As String = clsCommon.myCstr(grow.Cells(colBOMIcode).Value)
                    Dim qty As Double = clsCommon.myCdbl(grow.Cells(colBOMQty).Value)
                    Dim bomcode As String = clsCommon.myCstr(grow.Cells(colBOMCode).Value)
                    Dim po_No As String = clsCommon.myCstr(grow.Cells(colBOMPONo).Value)
                    Dim sch_No As String = clsCommon.myCstr(grow.Cells(colBOMScheduleNo).Value)

                    If objCommonVar.IsKDIL Then
                        'LoadBOMRawGrid(ICode, qty, bomcode, po_No, sch_No)
                    Else
                        LoadBillofMaterialRawGrid(ICode, qty, bomcode, po_No, sch_No)
                    End If
                Next '====po grid loop
            Else
                RemoveRowOfBOM(intRow)

                Dim ICode As String = clsCommon.myCstr(gv_PO.Rows(intRow).Cells(colBOMIcode).Value)
                Dim qty As Double = clsCommon.myCdbl(gv_PO.Rows(intRow).Cells(colBOMQty).Value)
                Dim bomcode As String = clsCommon.myCstr(gv_PO.Rows(intRow).Cells(colBOMCode).Value)
                Dim po_No As String = clsCommon.myCstr(gv_PO.Rows(intRow).Cells(colBOMPONo).Value)
                Dim sch_No As String = clsCommon.myCstr(gv_PO.Rows(intRow).Cells(colBOMScheduleNo).Value)

                If objCommonVar.IsKDIL Then
                    'LoadBOMRawGrid(ICode, qty, bomcode, po_No, sch_No)
                Else
                    LoadBillofMaterialRawGrid(ICode, qty, bomcode, po_No, sch_No)
                End If
            End If '==cellvaluechanged cond.
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub LoadBOMRawGrid(ByVal ICode As String, ByVal qty As Double, ByVal bomcode As String, ByVal po_No As String, ByVal sch_No As String)
        Try
            Dim obj As clsBOM = clsBOM.GetData(bomcode, NavigatorType.Current, Nothing)

            If gv1.Rows.Count > 0 AndAlso clsCommon.myLen(gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value) <= 0 Then
                gv1.Rows.RemoveAt(gv1.Rows.Count - 1)
            End If

            If obj IsNot Nothing AndAlso obj.ObjListBOM IsNot Nothing AndAlso obj.ObjListBOM.Count > 0 Then
                For Each objtr As clsBOMItemDetail In obj.ObjListBOM
                    gv1.Rows.AddNew()

                    gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = gv1.Rows.Count
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value = objtr.CONSM_ITEM_CODE
                    '--------------------------------------------------
                    Try
                        If chkthirdparty.Checked Then
                            cboItemType.SelectedValue = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select item_type from tspl_item_master where item_code='" + objtr.CONSM_ITEM_CODE + "'"))
                        End If
                    Catch exx As Exception
                        cboItemType.SelectedValue = ""
                    End Try
                    '----------------------------------------------------
                    Dim rawqty As Double = 0
                    If clsCommon.myCdbl(obj.PROD_QUANTITY) > 0 Then
                        rawqty = (clsCommon.myCdbl(objtr.CONSM_QUANTITY) * clsCommon.myCdbl(qty)) / clsCommon.myCdbl(obj.PROD_QUANTITY)
                    End If

                    gv1.Rows(gv1.Rows.Count - 1).Cells(colIName).Value = clsItemMaster.GetItemName(objtr.CONSM_ITEM_CODE, Nothing)
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colHSNNo).Value = clsItemMaster.GetItemHSNCode(objtr.CONSM_ITEM_CODE, Nothing)
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value = rawqty
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colBalQty).Value = rawqty
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colRetQty).Value = 0
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colUnit).Value = objtr.CONSM_ITEM_UNIT_CODE
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colRate).Value = 0
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colAmt).Value = 0
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colLastRGPNRGP).Value = Nothing
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDate).Value = Nothing
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colSp).Value = objtr.REMARKS
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colmrp).Value = 0

                    gv1.Rows(gv1.Rows.Count - 1).Cells(colRejSRNQty).Value = 0
                    gv1.Columns(colRejSRNQty).IsVisible = False

                    gv1.Rows(gv1.Rows.Count - 1).Cells(colPO_Sch_Qty).Value = 0
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colPurchaseOrderNo).Value = Nothing
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colScheduleNo).Value = Nothing
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colMainBOMCode).Value = bomcode
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colMainPOItem).Value = ICode

                    If clsCommon.myLen(po_No) > 0 Then
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colPO_Sch_Qty).Value = rawqty  'objtr.Balance_Qty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colPurchaseOrderNo).Value = po_No
                    End If
                    If clsCommon.myLen(sch_No) > 0 Then
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colPO_Sch_Qty).Value = rawqty  'objtr.Balance_Qty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colScheduleNo).Value = sch_No
                    End If
                Next
            Else
                gv1.Rows.AddNew()
            End If '=KDIL obj end
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub LoadBillofMaterialRawGrid(ByVal ICode As String, ByVal qty As Double, ByVal bomcode As String, ByVal po_No As String, ByVal sch_No As String)
        Try
            Dim obj As clsBillOfMaterial = clsBillOfMaterial.GetData(bomcode, NavigatorType.Current)

            If gv1.Rows.Count > 0 AndAlso clsCommon.myLen(gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value) <= 0 Then
                gv1.Rows.RemoveAt(gv1.Rows.Count - 1)
            End If

            If obj IsNot Nothing AndAlso clsBillOfMaterial.ObjList IsNot Nothing AndAlso clsBillOfMaterial.ObjList.Count > 0 Then
                For Each objtr As clsBillOfMaterial In clsBillOfMaterial.ObjList
                    gv1.Rows.AddNew()

                    gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = gv1.Rows.Count
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value = objtr.CONSM_ITEM_CODE
                    '--------------------------------------------------
                    Try
                        If chkthirdparty.Checked Then
                            cboItemType.SelectedValue = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select item_type from tspl_item_master where item_code='" + objtr.CONSM_ITEM_CODE + "'"))
                        End If
                    Catch exx As Exception
                        cboItemType.SelectedValue = ""
                    End Try
                    '----------------------------------------------------
                    Dim rawqty As Double = 0
                    If clsCommon.myCdbl(obj.PROD_QUANTITY) > 0 Then
                        rawqty = (clsCommon.myCdbl(objtr.CONSM_QUANTITY) * clsCommon.myCdbl(qty)) / clsCommon.myCdbl(obj.PROD_QUANTITY)
                    End If

                    gv1.Rows(gv1.Rows.Count - 1).Cells(colIName).Value = clsItemMaster.GetItemName(objtr.CONSM_ITEM_CODE, Nothing)
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colHSNNo).Value = clsItemMaster.GetItemHSNCode(objtr.CONSM_ITEM_CODE, Nothing)
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value = rawqty
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colBalQty).Value = rawqty
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colRetQty).Value = 0
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colUnit).Value = objtr.CONSM_ITEM_UNIT_CODE
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colRate).Value = 0
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colAmt).Value = 0
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colLastRGPNRGP).Value = Nothing
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDate).Value = Nothing
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colSp).Value = objtr.REMARKS
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colmrp).Value = 0

                    gv1.Rows(gv1.Rows.Count - 1).Cells(colRejSRNQty).Value = 0
                    gv1.Columns(colRejSRNQty).IsVisible = False

                    gv1.Rows(gv1.Rows.Count - 1).Cells(colPO_Sch_Qty).Value = 0
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colPurchaseOrderNo).Value = Nothing
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colScheduleNo).Value = Nothing
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colMainBOMCode).Value = bomcode
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colMainPOItem).Value = ICode

                    If clsCommon.myLen(po_No) > 0 Then
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colPO_Sch_Qty).Value = rawqty  'objtr.Balance_Qty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colPurchaseOrderNo).Value = po_No
                    End If
                    If clsCommon.myLen(sch_No) > 0 Then
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colPO_Sch_Qty).Value = rawqty  'objtr.Balance_Qty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colScheduleNo).Value = sch_No
                    End If
                Next
            Else
                gv1.Rows.AddNew()
            End If '==obj cond.
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub OpenBOMCode(ByVal isButtonClicked As Boolean)

        Dim bomcode As String = clsCommon.myCstr(gv_PO.CurrentRow.Cells(colBOMCode).Value)
        Dim Icode As String = clsCommon.myCstr(gv_PO.CurrentRow.Cells(colBOMIcode).Value)
        Dim Iunit As String = clsCommon.myCstr(gv_PO.CurrentRow.Cells(colBOMIUnit).Value)
        Dim qry As String = ""
        Dim whrCls As String = ""

        If objCommonVar.IsKDIL Then
            qry = "select TSPL_PP_BOM_HEAD.bom_code as Code,TSPL_PP_BOM_HEAD.Description,TSPL_PP_BOM_HEAD.bom_date as [Date],TSPL_PP_BOM_HEAD.prod_item_code as [Item Code],tspl_item_master.item_desc as [Item Name],TSPL_PP_BOM_HEAD.Prod_item_unit_code as [Unit],TSPL_PP_BOM_HEAD.revision_no as [Revision No] from TSPL_PP_BOM_HEAD "
            qry += " left outer join tspl_item_master on tspl_item_master.item_code=TSPL_PP_BOM_HEAD.prod_item_code "

            whrCls = " TSPL_PP_BOM_HEAD.is_post='1' and TSPL_PP_BOM_HEAD.is_osp='1' and TSPL_PP_BOM_HEAD.vendor_code='" + txtVendorNo.Value + "' and (convert(date,'" + clsCommon.GetPrintDate(clsCommon.myCDate(txtDate.Text), "dd/MMM/yyyy") + "',103) between convert(date,TSPL_PP_BOM_HEAD.valid_from_date,103) and convert(date,isnull(TSPL_PP_BOM_HEAD.valid_upto_date,dateadd(year,1,TSPL_PP_BOM_HEAD.valid_from_date)),103)) "
            If clsCommon.myLen(Icode) > 0 Then
                whrCls += " and TSPL_PP_BOM_HEAD.prod_item_code='" + Icode + "' "
            End If
            If clsCommon.myLen(Iunit) > 0 Then
                whrCls += " and TSPL_PP_BOM_HEAD.prod_item_unit_code='" + Iunit + "' "
            End If

            bomcode = clsCommon.ShowSelectForm("BOMRGPFND", qry, "Code", whrCls, bomcode, "Code", isButtonClicked)
            Dim obj As clsBOM = clsBOM.GetData(bomcode, NavigatorType.Current, Nothing)

            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.BOM_CODE) > 0 Then
                gv_PO.CurrentRow.Cells(colBOMCode).Value = obj.BOM_CODE
                gv_PO.CurrentRow.Cells(colBOMDesc).Value = obj.DESCRIPTION

                If clsCommon.myLen(gv_PO.CurrentRow.Cells(colBOMPONo).Value) > 0 OrElse clsCommon.myLen(gv_PO.CurrentRow.Cells(colBOMScheduleNo).Value) > 0 Then
                    'because data is filled when po or schedule is selected.,so here no need to fill again
                Else
                    gv_PO.CurrentRow.Cells(colBOMIcode).Value = obj.PROD_ITEM_CODE
                    gv_PO.CurrentRow.Cells(colBOMIname).Value = obj.PROD_ITEM_NAME
                    gv_PO.CurrentRow.Cells(colBOMIUnit).Value = obj.PROD_ITEM_UNIT_CODE
                    gv_PO.CurrentRow.Cells(colBOMQty).Value = obj.PROD_QUANTITY
                    gv_PO.CurrentRow.Cells(colBOMBalanceQty).Value = Nothing
                    gv_PO.CurrentRow.Cells(colBOMRate).Value = Nothing
                    gv_PO.CurrentRow.Cells(colBOMMRP).Value = Nothing
                    gv_PO.CurrentRow.Cells(colBOMAmt).Value = Nothing
                    gv_PO.CurrentRow.Cells(colBOMSpecification).Value = Nothing
                    gv_PO.CurrentRow.Cells(colBOMPONo).Value = Nothing
                    gv_PO.CurrentRow.Cells(colBOMScheduleNo).Value = Nothing
                End If
                gv_PO.CurrentRow.Cells(colBOMModule).Value = "Dairy"

                'RemoveRowOfBOM(gv_PO.CurrentRow.Index)
                'FillBOMItemInGrid(gv_PO.CurrentRow.Index, True)
            Else
                ClearBOMDetail()
            End If
        Else
            qry = "select TSPL_MF_BOM_HEAD.bom_code as Code,TSPL_MF_BOM_HEAD.Description,TSPL_MF_BOM_HEAD.bom_date as [Date],TSPL_MF_BOM_HEAD.prod_item_code as [Item Code],tspl_item_master.item_desc as [Item Name],TSPL_MF_BOM_HEAD.Prod_item_unit_code as [Unit],TSPL_MF_BOM_HEAD.revision_no as [Revision No] from TSPL_MF_BOM_HEAD "
            qry += " left outer join tspl_item_master on tspl_item_master.item_code=TSPL_MF_BOM_HEAD.prod_item_code "

            whrCls = " TSPL_MF_BOM_HEAD.posted='1' and (convert(date,'" + clsCommon.GetPrintDate(clsCommon.myCDate(txtDate.Text), "dd/MMM/yyyy") + "',103) between convert(date,TSPL_MF_BOM_HEAD.start_date,103) and convert(date,isnull(TSPL_MF_BOM_HEAD.end_date,dateadd(year,1,TSPL_MF_BOM_HEAD.start_date)),103)) "
            If clsCommon.myLen(Icode) > 0 Then
                whrCls += " and TSPL_MF_BOM_HEAD.prod_item_code='" + Icode + "' "
            End If
            If clsCommon.myLen(Iunit) > 0 Then
                whrCls += " and TSPL_MF_BOM_HEAD.prod_item_unit_code='" + Iunit + "' "
            End If

            bomcode = clsCommon.ShowSelectForm("BOMRGPFND", qry, "Code", whrCls, bomcode, "Code", isButtonClicked)
            Dim obj As clsBillOfMaterial = clsBillOfMaterial.GetData(bomcode, NavigatorType.Current, Nothing)

            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.BOM_CODE) > 0 Then
                gv_PO.CurrentRow.Cells(colBOMCode).Value = obj.BOM_CODE
                gv_PO.CurrentRow.Cells(colBOMDesc).Value = obj.DESCRIPTION

                If clsCommon.myLen(gv_PO.CurrentRow.Cells(colBOMPONo).Value) > 0 OrElse clsCommon.myLen(gv_PO.CurrentRow.Cells(colBOMScheduleNo).Value) > 0 Then
                Else
                    gv_PO.CurrentRow.Cells(colBOMIcode).Value = obj.PROD_ITEM_CODE
                    gv_PO.CurrentRow.Cells(colBOMIname).Value = obj.PROD_ITEM_NAME
                    gv_PO.CurrentRow.Cells(colBOMIUnit).Value = obj.PROD_ITEM_UNIT_CODE
                    gv_PO.CurrentRow.Cells(colBOMQty).Value = obj.PROD_QUANTITY
                    gv_PO.CurrentRow.Cells(colBOMBalanceQty).Value = Nothing
                    gv_PO.CurrentRow.Cells(colBOMRate).Value = Nothing
                    gv_PO.CurrentRow.Cells(colBOMMRP).Value = Nothing
                    gv_PO.CurrentRow.Cells(colBOMAmt).Value = Nothing
                    gv_PO.CurrentRow.Cells(colBOMSpecification).Value = Nothing
                    gv_PO.CurrentRow.Cells(colBOMPONo).Value = Nothing
                    gv_PO.CurrentRow.Cells(colBOMScheduleNo).Value = Nothing
                End If

                gv_PO.CurrentRow.Cells(colBOMModule).Value = "Standard"

                RemoveRowOfBOM(gv_PO.CurrentRow.Index)
                FillBOMItemInGrid(gv_PO.CurrentRow.Index, True)
            Else
                ClearBOMDetail()
            End If
        End If
    End Sub

    Private Sub ClearBOMDetail()
        RemoveRowOfBOM(gv_PO.CurrentRow.Index)

        gv_PO.CurrentRow.Cells(colBOMCode).Value = Nothing
        gv_PO.CurrentRow.Cells(colBOMDesc).Value = Nothing
        'gv_PO.CurrentRow.Cells(colBOMIcode).Value = Nothing
        'gv_PO.CurrentRow.Cells(colBOMIname).Value = Nothing
        'gv_PO.CurrentRow.Cells(colBOMIUnit).Value = Nothing
        'gv_PO.CurrentRow.Cells(colBOMQty).Value = Nothing
        'gv_PO.CurrentRow.Cells(colBOMBalanceQty).Value = Nothing
        'gv_PO.CurrentRow.Cells(colBOMRate).Value = Nothing
        'gv_PO.CurrentRow.Cells(colBOMMRP).Value = Nothing
        'gv_PO.CurrentRow.Cells(colBOMAmt).Value = Nothing
        'gv_PO.CurrentRow.Cells(colBOMSpecification).Value = Nothing
        'gv_PO.CurrentRow.Cells(colBOMPONo).Value = Nothing
        'gv_PO.CurrentRow.Cells(colBOMScheduleNo).Value = Nothing
        'gv_PO.CurrentRow.Cells(colBOMModule).Value = Nothing
    End Sub

    Private Sub OpenBOMIcode(ByVal isbuttonClicked As Boolean)
        If clsCommon.myLen(gv_PO.CurrentRow.Cells(colBOMPONo).Value) > 0 OrElse clsCommon.myLen(gv_PO.CurrentRow.Cells(colBOMScheduleNo).Value) > 0 Then
            Exit Sub
        End If
        Dim Icode As String = clsCommon.myCstr(gv_PO.CurrentRow.Cells(colBOMIcode).Value)
        Dim bomcode As String = clsCommon.myCstr(gv_PO.CurrentRow.Cells(colBOMCode).Value)
        Dim whrcls As String = ""

        If chkAsPerBOM.Checked Then
            If clsCommon.myLen(bomcode) > 0 AndAlso objCommonVar.IsKDIL Then
                whrcls = " and tspl_item_code.item_code in (select prod_item_code from TSPL_PP_BOM_HEAD where bom_code='" + bomcode + "')"
            ElseIf clsCommon.myLen(bomcode) > 0 AndAlso Not objCommonVar.IsKDIL Then
                whrcls = " and tspl_item_code.item_code in (select prod_item_code from TSPL_MF_BOM_HEAD where bom_code='" + bomcode + "')"
            End If
        End If


        Icode = clsItemMaster.getFinder(whrcls, Icode, isbuttonClicked)
        gv_PO.CurrentRow.Cells(colBOMIcode).Value = Icode
        gv_PO.CurrentRow.Cells(colBOMIname).Value = clsItemMaster.GetItemName(Icode, Nothing)
        gv_PO.CurrentRow.Cells(colBOMIUnit).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select unit_code from tspl_item_master where item_code='" + Icode + "'"))
        If clsCommon.myLen(bomcode) > 0 AndAlso objCommonVar.IsKDIL Then
            gv_PO.CurrentRow.Cells(colBOMIUnit).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select prod_item_unit_code from tspl_pp_bom_head where bom_code='" + bomcode + "'"))
            gv_PO.CurrentRow.Cells(colBOMModule).Value = "Dairy"
        ElseIf clsCommon.myLen(bomcode) > 0 AndAlso Not objCommonVar.IsKDIL Then
            gv_PO.CurrentRow.Cells(colBOMIUnit).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select prod_item_unit_code from tspl_mf_bom_head where bom_code='" + bomcode + "'"))
            gv_PO.CurrentRow.Cells(colBOMModule).Value = "Standard"
        End If
        gv_PO.CurrentRow.Cells(colBOMPONo).Value = Nothing
        gv_PO.CurrentRow.Cells(colBOMScheduleNo).Value = Nothing

        If chkAsPerBOM.Checked Then
            RemoveRowOfBOM(gv_PO.CurrentRow.Index)
            FillBOMItemInGrid(gv_PO.CurrentRow.Index, True)
        End If

    End Sub

    Private Sub OpenBOMUnit(ByVal isbuttonClicked As Boolean)
        If clsCommon.myLen(gv_PO.CurrentRow.Cells(colBOMPONo).Value) > 0 OrElse clsCommon.myLen(gv_PO.CurrentRow.Cells(colBOMScheduleNo).Value) > 0 Then
            Exit Sub
        End If
        Dim Iunit As String = clsCommon.myCstr(gv_PO.CurrentRow.Cells(colBOMIUnit).Value)
        Dim bomcode As String = clsCommon.myCstr(gv_PO.CurrentRow.Cells(colBOMCode).Value)
        Dim whrcls As String = " tspl_item_uom_detail.item_code='" + clsCommon.myCstr(gv_PO.CurrentRow.Cells(colBOMIcode).Value) + "' "

        If clsCommon.myLen(bomcode) > 0 AndAlso objCommonVar.IsKDIL Then
            whrcls += " and tspl_item_uom_detail.uom_code in (select prod_item_unit_code from TSPL_PP_BOM_HEAD where bom_code='" + bomcode + "')"
        ElseIf clsCommon.myLen(bomcode) > 0 AndAlso Not objCommonVar.IsKDIL Then
            whrcls += " and tspl_item_uom_detail.uom_code in (select prod_item_unit_code from TSPL_MF_BOM_HEAD where bom_code='" + bomcode + "')"
        End If

        Dim qry As String = "select tspl_item_uom_detail.uom_code as Code,tspl_unit_master.unit_desc as Unit from tspl_item_uom_detail left outer join tspl_unit_master on tspl_unit_master.unit_code=tspl_item_uom_detail.uom_code "
        Iunit = clsCommon.ShowSelectForm("BOMIUFND", qry, "Code", whrcls, Iunit, "Code", isbuttonClicked)
        gv_PO.CurrentRow.Cells(colBOMIUnit).Value = Iunit
    End Sub

    Private Sub gv_PO_CellFormatting(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.CellFormattingEventArgs) Handles gv_PO.CellFormatting
        Try
            If e.Column.Index >= 0 Then
                If e.Column Is gv_PO.Columns(colBOMIcode) Then
                    If clsCommon.myLen(gv_PO.CurrentRow.Cells(colBOMPONo).Value) > 0 OrElse clsCommon.myLen(gv_PO.CurrentRow.Cells(colBOMScheduleNo).Value) > 0 OrElse clsCommon.CompairString(txtAsitis.Text, "Y") = CompairStringResult.Equal Then
                        gv_PO.Columns(colBOMIcode).ReadOnly = True
                    Else
                        gv_PO.Columns(colBOMIcode).ReadOnly = False
                    End If
                End If
                If e.Column Is gv_PO.Columns(colBOMIUnit) Then
                    If clsCommon.myLen(gv_PO.CurrentRow.Cells(colBOMPONo).Value) > 0 OrElse clsCommon.myLen(gv_PO.CurrentRow.Cells(colBOMScheduleNo).Value) > 0 Then
                        gv_PO.Columns(colBOMIUnit).ReadOnly = True
                    Else
                        gv_PO.Columns(colBOMIUnit).ReadOnly = False
                    End If
                End If

            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub gv_PO_CellValueChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv_PO.CellValueChanged
        Try
            If Not isInsideLoadData Then
                If Not isCellValueChangedOpen Then
                    If e.Column Is gv_PO.Columns(colBOMCode) Then
                        isCellValueChangedOpen = True
                        OpenBOMCode(False)
                        isCellValueChangedOpen = False
                    End If

                    If e.Column Is gv_PO.Columns(colBOMIcode) Then
                        isCellValueChangedOpen = True
                        OpenBOMIcode(False)
                        isCellValueChangedOpen = False
                    End If

                    If e.Column Is gv_PO.Columns(colBOMIUnit) Then
                        isCellValueChangedOpen = True
                        OpenBOMUnit(False)
                        isCellValueChangedOpen = False
                    End If

                    If e.Column Is gv_PO.Columns(colBOMQty) Then
                        isCellValueChangedOpen = True
                        Dim dblqty As Double = clsCommon.myCdbl(gv_PO.CurrentRow.Cells(colBOMQty).Value)
                        Dim dblbalqty As Double = clsCommon.myCdbl(gv_PO.CurrentRow.Cells(colBOMBalanceQty).Value)
                        If clsCommon.myLen(gv_PO.CurrentRow.Cells(colBOMScheduleNo).Value) > 0 Then
                            dblbalqty = clsPurchaseScheduleDetail.GetBalanceScheduleQty(clsCommon.myCstr(gv_PO.CurrentRow.Cells(colBOMScheduleNo).Value), clsCommon.myCstr(gv_PO.CurrentRow.Cells(colBOMIcode).Value), txtDocNo.Value, txtDate.Text, clsCommon.myCstr(gv_PO.CurrentRow.Cells(colBOMIUnit).Value), True)
                        End If

                        If clsCommon.myLen(gv_PO.CurrentRow.Cells(colBOMIcode).Value) > 0 AndAlso (clsCommon.myLen(gv_PO.CurrentRow.Cells(colBOMPONo).Value) > 0 OrElse clsCommon.myLen(gv_PO.CurrentRow.Cells(colBOMScheduleNo).Value) > 0) AndAlso dblqty > dblbalqty Then
                            gv_PO.CurrentRow.Cells(colBOMQty).Value = 0
                            Throw New Exception("Quantity cannot be greater than balance quantity i.e. " + clsCommon.myCstr(dblbalqty) + "")
                        End If
                        gv_PO.CurrentRow.Cells(colBOMAmt).Value = clsCommon.myCdbl(gv_PO.CurrentRow.Cells(colBOMQty).Value) * clsCommon.myCdbl(gv_PO.CurrentRow.Cells(colBOMRate).Value)

                        If chkAsPerBOM.Checked Then
                            RemoveRowOfBOM(gv_PO.CurrentRow.Index)
                            FillBOMItemInGrid(gv_PO.CurrentRow.Index, True)
                        End If

                        isCellValueChangedOpen = False
                    End If

                    If e.Column Is gv_PO.Columns(colBOMRate) Then
                        isCellValueChangedOpen = True
                        gv_PO.CurrentRow.Cells(colBOMAmt).Value = clsCommon.myCdbl(gv_PO.CurrentRow.Cells(colBOMQty).Value) * clsCommon.myCdbl(gv_PO.CurrentRow.Cells(colBOMRate).Value)
                        isCellValueChangedOpen = False
                    End If
                End If
            End If
        Catch ex As Exception
            isCellValueChangedOpen = False
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub gv_PO_CurrentColumnChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gv_PO.CurrentColumnChanged
        If gv_PO.RowCount > 0 Then
            Dim intCurrRow As Integer = gv_PO.CurrentRow.Index
            gv_PO.CurrentRow.Cells(colBOMLineNo).Value = clsCommon.myCdbl(intCurrRow + 1)
            If objCommonVar.IsKDIL Then
                gv_PO.CurrentRow.Cells(colBOMModule).Value = "Dairy"
            Else
                gv_PO.CurrentRow.Cells(colBOMModule).Value = "Standard"
            End If
            If intCurrRow = gv_PO.Rows.Count - 1 Then
                gv_PO.Rows.AddNew()
                gv_PO.CurrentRow = gv_PO.Rows(intCurrRow)
                If objCommonVar.IsKDIL Then
                    gv_PO.CurrentRow.Cells(colBOMModule).Value = "Dairy"
                Else
                    gv_PO.CurrentRow.Cells(colBOMModule).Value = "Standard"
                End If
            End If
        End If
    End Sub

    Private Sub gv_PO_UserDeletedRow(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewRowEventArgs) Handles gv_PO.UserDeletedRow
        For ii As Integer = 1 To gv_PO.Rows.Count
            gv_PO.Rows(ii - 1).Cells(colBOMLineNo).Value = ii
        Next
    End Sub

    Private Sub gv_PO_UserDeletingRow(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs) Handles gv_PO.UserDeletingRow
        If common.clsCommon.MyMessageBoxShow("Delete The Current Row." + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
            e.Cancel = True
        Else
            RemoveRowOfBOM(gv_PO.CurrentRow.Index)
        End If
    End Sub

    Private Sub RemoveRowOfBOM(ByVal intRow As Integer)
        If objCommonVar.IsKDIL Then
            Exit Sub
        End If
        Dim strCode As String = clsCommon.myCstr(gv_PO.Rows(intRow).Cells(colBOMIcode).Value)
        Dim bomcode As String = clsCommon.myCstr(gv_PO.Rows(intRow).Cells(colBOMCode).Value)
        Dim po_No As String = clsCommon.myCstr(gv_PO.Rows(intRow).Cells(colBOMPONo).Value)
        Dim sch_No As String = clsCommon.myCstr(gv_PO.Rows(intRow).Cells(colBOMScheduleNo).Value)
        Dim counter As Integer = 0
        For Each grow As GridViewRowInfo In gv1.Rows
            counter += 1
        Next
        If counter > 0 Then
            For ii As Integer = gv1.Rows.Count - 1 To 0 Step -1
                If clsCommon.CompairString(strCode, clsCommon.myCstr(gv1.Rows(ii).Cells(colMainPOItem).Value)) = CompairStringResult.Equal AndAlso clsCommon.CompairString(bomcode, clsCommon.myCstr(gv1.Rows(ii).Cells(colMainBOMCode).Value)) = CompairStringResult.Equal AndAlso clsCommon.CompairString(po_No, clsCommon.myCstr(gv1.Rows(ii).Cells(colPurchaseOrderNo).Value)) = CompairStringResult.Equal AndAlso clsCommon.CompairString(sch_No, clsCommon.myCstr(gv1.Rows(ii).Cells(colScheduleNo).Value)) = CompairStringResult.Equal Then
                    gv1.Rows.RemoveAt(ii)
                End If
            Next
        End If
    End Sub
#End Region

    Private Sub btnSameasAbove_Click(sender As Object, e As EventArgs) Handles btnSameasAbove.Click
        Try
            gv_PO.Rows.Clear()
            txtPoNo.Value = ""
            txtScheduleNo.Value = ""

            txtAsitis.Text = "Y"
            chkAsPerBOM.Checked = False

            Dim counter As Integer = 0

            isInsideLoadData = True
            For Each grow As GridViewRowInfo In gv1.Rows
                If clsCommon.myLen(grow.Cells(colICode).Value) <= 0 Then
                    Continue For
                End If
                counter += 1
                gv_PO.Rows.AddNew()

                gv_PO.Rows(gv_PO.Rows.Count - 1).Cells(colBOMLineNo).Value = gv_PO.Rows.Count
                gv_PO.Rows(gv_PO.Rows.Count - 1).Cells(colBOMCode).Value = Nothing
                gv_PO.Rows(gv_PO.Rows.Count - 1).Cells(colBOMDesc).Value = Nothing
                gv_PO.Rows(gv_PO.Rows.Count - 1).Cells(colBOMIcode).Value = clsCommon.myCstr(grow.Cells(colICode).Value)
                gv_PO.Rows(gv_PO.Rows.Count - 1).Cells(colBOMIname).Value = clsCommon.myCstr(grow.Cells(colIName).Value)
                gv_PO.Rows(gv_PO.Rows.Count - 1).Cells(colBOMQty).Value = clsCommon.myCdbl(grow.Cells(colQty).Value)
                gv_PO.Rows(gv_PO.Rows.Count - 1).Cells(colBOMBalanceQty).Value = clsCommon.myCdbl(grow.Cells(colBalQty).Value)
                gv_PO.Rows(gv_PO.Rows.Count - 1).Cells(colBOMIUnit).Value = clsCommon.myCstr(grow.Cells(colUnit).Value)
                gv_PO.Rows(gv_PO.Rows.Count - 1).Cells(colBOMRate).Value = clsCommon.myCstr(grow.Cells(colRate).Value)
                gv_PO.Rows(gv_PO.Rows.Count - 1).Cells(colBOMAmt).Value = clsCommon.myCdbl(grow.Cells(colQty).Value) * clsCommon.myCstr(grow.Cells(colRate).Value)
                gv_PO.Rows(gv_PO.Rows.Count - 1).Cells(colBOMSpecification).Value = clsCommon.myCstr(grow.Cells(colSp).Value)
                gv_PO.Rows(gv_PO.Rows.Count - 1).Cells(colBOMMRP).Value = 0
                gv_PO.Rows(gv_PO.Rows.Count - 1).Cells(colBOMPONo).Value = clsCommon.myCstr(grow.Cells(colPurchaseOrderNo).Value)
                gv_PO.Rows(gv_PO.Rows.Count - 1).Cells(colBOMScheduleNo).Value = clsCommon.myCstr(grow.Cells(colScheduleNo).Value)

                If objCommonVar.IsKDIL Then
                    gv_PO.Rows(gv_PO.Rows.Count - 1).Cells(colBOMModule).Value = "Dairy"
                Else
                    gv_PO.Rows(gv_PO.Rows.Count - 1).Cells(colBOMModule).Value = "Standard"
                End If
            Next
            If counter = 0 Then
                gv_PO.Rows.AddNew()
                txtAsitis.Text = ""
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isInsideLoadData = False
        End Try
    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    Private Sub chkAsPerBOM_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkAsPerBOM.ToggleStateChanged
        gv_PO.Columns(colBOMCode).IsVisible = chkAsPerBOM.Checked
        gv_PO.Columns(colBOMDesc).IsVisible = chkAsPerBOM.Checked
        gv1.Columns(colPO_Sch_Qty).IsVisible = chkAsPerBOM.Checked
        If chkAsPerBOM.Checked AndAlso Not isInsideLoadData Then
            gv1.Rows.Clear()
            gv1.Rows.AddNew()
            txtVendorNo.Value = ""
            lblVendorName.Text = ""
            txtAsitis.Text = ""
            gv_PO.Rows.Clear()
            gv_PO.Rows.AddNew()
            RadPageViewPage3.Item.Visibility = ElementVisibility.Collapsed
            RadPageView2.SelectedPage = RadPageViewPage2
        End If
        If chkAgainstJobWork.Checked AndAlso Not chkAsPerBOM.Checked Then
            RadPageViewPage3.Item.Visibility = ElementVisibility.Visible
            RadPageView2.SelectedPage = RadPageViewPage2
        End If
    End Sub

    Sub OpenSerialItem()
        If clsCommon.myCBool(gv1.CurrentRow.Cells(colIsSerialseItem).Value) Then
            Dim Item_type As String = clsDBFuncationality.getSingleValue("select Item_Type from TSPL_ITEM_MASTER where Item_Code='" + gv1.CurrentRow.Cells(colICode).Value + "'")
            Dim frm As frmSerializeItemOut = New frmSerializeItemOut()
            frm.strItemCode = clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value)
            frm.strItemName = clsCommon.myCstr(gv1.CurrentRow.Cells(colIName).Value)
            frm.strLocationCode = txtLocation.Value
            frm.strCurrDocNo = txtDocNo.Value
            frm.strCurrDocType = clsCommon.myCstr(cboDocType.SelectedValue)
            frm.dblqty = clsCommon.myCdbl(gv1.CurrentRow.Cells(colQty).Value)
            frm.strItemType = Item_type
            frm.arr = TryCast(gv1.CurrentRow.Tag, List(Of clsSerializeInvenotry))
            frm.ShowDialog()
            If Not frm.isCencelButtonClicked Then
                gv1.CurrentRow.Tag = frm.arr
            End If
        End If
    End Sub

    Private Sub gv1_KeyDown(sender As Object, e As KeyEventArgs) Handles gv1.KeyDown
        If e.KeyCode = Keys.F4 Then
            OpenSerialItem()
        End If
        If e.KeyCode = Keys.F5 Then
            '======update by preeti gupta 17/10/2018
            If RunBatchFifowise = 0 Then
                OpenBatchItem()
            Else
                OpenBatchItemIfFIFIOSettingON()
            End If
        End If

    End Sub
    '============created by preeti gupta===============
    Public Sub OpenBatchItemIfFIFIOSettingON()
        Dim arr As List(Of clsBatchInventory) = Nothing
        Dim strBatchunion As String = ""
        If clsCommon.myLen(gv1.CurrentRow.Cells(colICode).Value) > 0 Then
            arr = TryCast(gv1.CurrentRow.Cells(colICode).Tag, List(Of clsBatchInventory))
        End If
        If Not arr Is Nothing Then
            If arr.Count > 0 Then
                For Each obj As clsBatchInventory In arr
                    strBatchunion += " Batch No - " & clsCommon.myCstr(obj.Batch_No) & "         Qty - " & clsCommon.myCstr(obj.Qty) + Environment.NewLine
                Next
                clsCommon.MyMessageBoxShow(strBatchunion, Me.Text)
            End If
        End If
    End Sub
    Sub OpenBatchItem()
        Try

            If (clsCommon.myLen(clsCommon.myCdbl(gv1.CurrentRow.Cells(colICode).Value)) > 0) Then
                Dim qry As String = " select Is_Batch_Item from TSPL_ITEM_MASTER where Item_Code='" & clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value) & "'"
                Dim isBatchAvtive As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
                If isBatchAvtive = "1" Then
                    Dim frm As frmBatchItemOut = New frmBatchItemOut()
                    frm.strItemCode = clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value)
                    frm.strItemName = clsCommon.myCstr(gv1.CurrentRow.Cells(colIName).Value)
                    frm.strLocationCode = txtLocation.Value
                    frm.strCurrDocNo = txtDocNo.Value
                    frm.strCurrDocType = clsCommon.myCstr(cboDocType.SelectedValue)
                    frm.strUOM = clsCommon.myCstr(gv1.CurrentRow.Cells(colUnit).Value)
                    frm.dblMRP = clsCommon.myCdbl(gv1.CurrentRow.Cells(colmrp).Value)
                    frm.dblqty = clsCommon.myCdbl(gv1.CurrentRow.Cells(colQty).Value)
                    frm.arr = TryCast(gv1.CurrentRow.Cells(colICode).Tag, List(Of clsBatchInventory))
                    If RunBatchFifowise = 0 Then
                        frm.ShowDialog()
                        If Not frm.isCencelButtonClicked Then
                            gv1.CurrentRow.Cells(colICode).Tag = frm.arr
                        End If
                    Else
                        frm.OpenSerialList(0, "")
                        gv1.CurrentRow.Cells(colICode).Tag = frm.arr
                    End If
                End If
            End If


        Catch ex As Exception
            isCellValueChangedOpen = False
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try


    End Sub


    Private Sub TxtFinder1_Load(sender As Object, e As EventArgs) Handles nrgpReqFnd.Load

    End Sub

    Private Sub nrgpReqFnd__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles nrgpReqFnd._MYValidating
        Try
            If IsSettingOn = True Then
                LoadNRGP_Request()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try


    End Sub
    ' Ticket No : TEC/29/10/18-000354 By Prabhakar
    Private Sub btnShowInventory_Click(sender As Object, e As EventArgs) Handles btnShowInventory.Click
        clsOpenInventory.ShowInventoryDatails(txtDocNo.Value)
    End Sub

    Private Sub chkAgainstCCTransfer_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkAgainstCCTransfer.ToggleStateChanged
        If Not isInsideLoadData Then
            'If chkAgainstCCTransfer.Checked Then
            '    RadLabel2.Visible = False
            '    txtVendorNo.Visible = False
            '    lblVendorName.Visible = False
            '    txtVendorNo.Value = ""
            '    lblToLocation.Visible = True
            '    fndToLocationCode.Visible = True
            '    lblToLocationCode.Visible = True


            'Else
            '    fndToLocationCode.Value = ""
            '    lblToLocation.Visible = False
            '    fndToLocationCode.Visible = False
            '    lblToLocationCode.Visible = False

            '    RadLabel2.Visible = True
            '    txtVendorNo.Visible = True
            '    lblVendorName.Visible = True
            'End If
            If chkNonInventoryItem.Checked Then
                chkNonInventoryItem.Checked = True
                chkAgainstCCTransfer.Visible = True
            Else
                chkNonInventoryItem.Checked = False
                chkAgainstCCTransfer.Visible = False
            End If
            If chkNonInventoryItem.Checked AndAlso chkAgainstCCTransfer.Checked Then
                chkAgainstCCTransfer.Checked = True
                fndToLocationCode.Value = ""
                lblToLocationCode.Text = ""
                lblToLocation.Visible = True
                fndToLocationCode.Visible = True
                lblToLocationCode.Visible = True
            Else
                chkAgainstCCTransfer.Checked = False
                fndToLocationCode.Value = ""
                lblToLocationCode.Text = ""

                lblToLocation.Visible = False
                fndToLocationCode.Visible = False
                lblToLocationCode.Visible = False

            End If
        End If

    End Sub

    Private Sub fndToLocationCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndToLocationCode._MYValidating
        Dim qry As String = "Select XXXFinal.* from ( select Location_Code as Code, Location_Desc as Name from TSPL_Location_Master where   is_sub_location ='N' and is_section = 'N'  and location_type ='Physical' and Location_Category = 'MCC' " &
                           " union All " &
                           " select Location_Code as Code, Location_Desc as Name from TSPL_Location_Master where is_sub_location ='N' and is_section = 'N'  and location_type ='Physical' and Location_Category <> 'MCC' and type ='Depot') XXXFinal "

        Dim WhrCls As String = ""
        fndToLocationCode.Value = clsCommon.ShowSelectForm("FromLocationFinder@RgpNrgp", qry, "Code", WhrCls, fndToLocationCode.Value, "Code", isButtonClicked)
        lblToLocationCode.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_Location_Master where Location_code = '" + fndToLocationCode.Value + "'"))
    End Sub

    Private Sub btnHistory_Click(sender As Object, e As EventArgs) Handles btnHistory.Click
        Try
            If clsCommon.myLen(txtDocNo.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Select Document Code", Me.Text)
                Exit Sub
            End If
            clsERPFuncationalityold.ShowTransHistoryData(txtDocNo.Value, "RGP_No", "TSPL_RGP_HEAD", "TSPL_RGP_DETAIL")
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
End Class
