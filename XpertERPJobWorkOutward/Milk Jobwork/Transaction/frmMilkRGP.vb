''Changed By Balwinde On 19 oct 2011 For Deliverd By Finder.
'-Updation By-[Pankaj Kumar Chaudhary]--Against Ticket No=[BM00000001948, BM00000003163]
'---preeti Gupta---Ticket No.-BM00000003015--01/07/2014
'=============BM00000007480,Rohit=================
'===========BM00000007790,BM00000007620,BM00000007653,BM00000007810,Rohit========================
Imports common
Imports System.Globalization
Imports System.Data.SqlClient
Imports System.IO

Public Class frmMilkRGP
    Inherits FrmMainTranScreen

#Region "Variables"
    Dim IsLoc_Third_Party As String = "NO"
    Dim is_Load_MRN As Boolean = False
    Dim IsPOScheduleOn As Boolean = False
    Dim IsRGPAfterPO As Boolean = False

    Dim check As Integer = 0
    Dim DecimalPoint As Integer = 3
    Public IsSRNSaved As String = "N"
    Private isCellValueChangedOpen As Boolean = False
    Private isNewEntry As Boolean = False
    Private isInsideLoadData As Boolean = False
    Private IsformLoad As Boolean = True
    Const colLineNo As String = "COLLNO"
    Const colICode As String = "COLICODE"
    Const colIName As String = "COLINAME"
    Const colQty As String = "COLQTY"
    Const colBalQty As String = "COLBalQTY"
    Const colRetQty As String = "COLRETQTY"
    Const colUnit As String = "COLUNIT"
    Const colRate As String = "COLRATE"
    Const colApproxCost As String = "colApproxCost"
    Const colAmt As String = "COLAMT"
    Const ColFATRATE As String = "ColFATRATE"
    Const ColSNFRATE As String = "ColSNFRATE"
    Const ColFATCost As String = "ColFATCost"
    Const ColSNFCost As String = "ColSNFCost"
    Const ColFATKG As String = "ColFATKG"
    Const ColSNFKG As String = "ColSNFKG"
    Const ColFATPrice As String = "ColFATPrice"
    Const ColSNFPrice As String = "ColSNFPrice"
    Const ColTankerNo As String = "ColTankerNo"
    Const ColLocationType As String = "ColLocationType"
    Const ColLocationSub As String = "ColLocationSub"
    Const ColLocationHeader As String = "ColLocationHeader"
    Const ColLocationSubName As String = "ColLocationSubName"
    Const ColLocationHeaderName As String = "ColLocationHeaderName"

    Const colSp As String = "COLSPECIFICATION"




    '=============QC===================
    Const colQCsno As String = "SNO"
    Const colQCloc_code As String = "QCLoc_Code"
    Const colQCloc_name As String = "QCLoc_Name"
    Const colQCToloc_code As String = "QCTOLoc_Code"
    Const colQCToloc_name As String = "QCSTOLoc_Name"
    Const colQCitemcode As String = "qcitemcode"
    Const colQCiname As String = "iname"
    Const colQCHSNcode As String = "colQCHSNcode"
    Const colQCparamcode As String = "paramcode"
    Const colQCparam_desc As String = "param_desc"
    Const colQCparam_type As String = "paramtype"
    Const colQCparam_nature As String = "paramnature"
    Const colQCrange1 As String = "range1"
    Const colQCrange2 As String = "range2"
    Const colQCstatus As String = "status"
    Const colQCvalue1 As String = "value1"
    Const colQCvalue2 As String = "value2"
    Const colQCRange As String = "Range"
    Const colQCValue As String = "OUTPUTVALUE"
    Const colQCOutStatus As String = "OutStatus"
    Const colQCremarks As String = "remarkS"
    Const colQCHistort As String = "History"
    '=============================================

    Public DocumentNo As String = Nothing
    Dim ButtonToolTip As ToolTip = New ToolTip()

    Dim repomrp As GridViewDecimalColumn = New GridViewDecimalColumn()
    Dim dt As DataTable
    Dim qry As String
    Dim arrLoc As String = Nothing

#End Region

    Private Sub LOCATIONRIGTHS()
        Dim obj As New clsMCCCodes()
        Try
            obj = clsMCCCodes.GetData()

            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Default_LocCode) > 0 Then
                txtLocation.Value = obj.Default_LocCode
                lblLocation.Text = obj.Default_LocName
            End If
            If obj.arrLocCodes IsNot Nothing AndAlso clsCommon.myLen(obj.arrLocCodes) > 0 Then
                arrLoc = obj.arrLocCodes
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        Finally
            obj = Nothing
        End Try
    End Sub

    Public Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.FrmMilkJobWork)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnPost.Visible = MyBase.isPostFlag
        btnDelete.Visible = MyBase.isDeleteFlag
        If MyBase.isReverse Then
            btnReverse.Enabled = True
        Else
            btnReverse.Enabled = False
        End If
    End Sub

    Sub LoadItemType()
        cboItemType.DataSource = clsItemMaster.GetItemType()
        cboItemType.ValueMember = "Code"
        cboItemType.DisplayMember = "Name"
    End Sub

    Private Sub FrmAPInvoiceEntry_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()

        '=====if rgp after po on then po show on screen andalso if sch. setting on then sch. finder seen========================================
        MyLabel18.Visible = False
        txtPoNo.Visible = False


        RadPageViewPage2.Text = "Item Detail"

        IsRGPAfterPO = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.IsRGPAfterPurchaseOrder, clsFixedParameterCode.IsRGPAfterPurchaseOrder, Nothing)) = "1", True, False))
        If IsRGPAfterPO = True Then
            MyLabel18.Visible = True
            txtPoNo.Visible = True

        End If

        is_Load_MRN = False ' IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ShowGRN, clsFixedParameterCode.ShowGRN, Nothing)) = 1 AndAlso clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ShowMRN, clsFixedParameterCode.ShowMRN, Nothing)) = 1, True, False)
        '=================end here=================================

        Dim qry As String = "select IsThirdPartyLocationOnERP from TSPL_INV_PARAMETERS"
        IsLoc_Third_Party = clsDBFuncationality.getSingleValue(qry)

        If IsLoc_Third_Party = "1" Then
            IsLoc_Third_Party = "YES"
        Else
            IsLoc_Third_Party = "NO"
        End If

        gbRGPNRGP.Visible = True
        txtVendorNo.MendatroryField = True
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction")
        ButtonToolTip.SetToolTip(btnPost, "Press Alt+P Post Trasnaction")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnAddNew, "Press Alt+N Adding New Trasnaction")
        ButtonToolTip.SetToolTip(btnAddNew, "Press Alt+A Create Additional Cost")
        LoadBlankGrid()
        LoadQCBlankGrid()
        gv1.Rows.AddNew()
        IsformLoad = True
        LoadDocType()
        IsformLoad = False
        AddNew()
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
        If clsCommon.CompairString(IsLoc_Third_Party, "YES") = CompairStringResult.Equal Then
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
        cboDocType.DataSource = dt
        cboDocType.ValueMember = "Code"
        cboDocType.DisplayMember = "Desc"

        RadGroupBox1.Enabled = False
        If clsCommon.CompairString(IsLoc_Third_Party, "YES") = CompairStringResult.Equal AndAlso cboDocType.SelectedValue = "RGP" Then
            RadGroupBox1.Enabled = True
        End If
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

        txtDocNo.Value = ""
        txtReason.Text = ""

        txtVendorNo.Value = ""
        txtDepartment.Value = ""
        lblDepartment.Text = ""

        lblVendorName.Text = ""
        txtModeOfTransport.Text = ""
        txtCashMemoDetail.Text = ""
        txtDate.Value = clsCommon.GETSERVERDATE()
        txtReason.Text = ""
        txtRemarks.Text = ""
        UsLock1.Status = ERPTransactionStatus.Pending
        txtVehicleNo.Text = ""
        txtGPNo.Text = ""

        txtGPDate.Checked = False
        txtGPDate.Value = txtDate.Value
        cboDocType.Enabled = True
        txtLocation.Enabled = True
        txtLocation.Value = ""
        lblLocation.Text = ""
        lblDocumentAmt.Text = ""
        txtDeliveredBy.Value = ""
        lblDeliveredBy.Text = ""

        fndCostCentre.Value = ""
        txtCostCentre.Text = ""
        ''richa Ticket No BM00000003061 on 01/08/2014
        
        '-------------------------------------------
        txtPoNo.Value = ""
        txtPoNo.Enabled = False
       

        ''For Custom Fields
        If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
            UcCustomFields1.BlankAllControls()
        End If
        ''End of For Custom Fields
        UcAttachment1.BlankAllControls()

        '=============initially grid of po item is not seen.
        'txtAsitis.Text = ""
        'btnSameasAbove.Enabled = True

        'gv_PO.Rows.Clear()
        'gv_PO.Rows.AddNew()
        'RadPageViewPage3.Item.Visibility = ElementVisibility.Collapsed
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
        repoICode.HeaderImage = My.Resources.search4
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

        Dim repoHSNCODE As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoHSNCODE.FormatString = ""
        repoHSNCODE.HeaderText = "HSN Code"
        repoHSNCODE.Name = colQCHSNcode
        repoHSNCODE.Width = 100
        repoHSNCODE.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoHSNCODE)

        '' Anubhooti 07-Oct-2014

        
        

        Dim repoUnit As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoUnit.FormatString = ""
        repoUnit.HeaderText = "UOM"
        repoUnit.Name = colUnit
        repoUnit.Width = 100
        repoUnit.ReadOnly = False
        repoUnit.HeaderImage = My.Resources.search4
        repoUnit.TextImageRelation = TextImageRelation.TextBeforeImage
        gv1.MasterTemplate.Columns.Add(repoUnit)

        Dim TankerNoto As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        TankerNoto.FormatString = ""
        TankerNoto.Name = ColTankerNo
        TankerNoto.Width = 100
        TankerNoto.HeaderText = "Tanker No"
        gv1.MasterTemplate.Columns.Add(TankerNoto)
        TankerNoto = Nothing

        
        Dim FrmlocType1 As GridViewComboBoxColumn = New GridViewComboBoxColumn()
        FrmlocType1.FormatString = ""
        FrmlocType1.Name = ColLocationType
        FrmlocType1.Width = 70
        FrmlocType1.HeaderText = "Location Type"
        FrmlocType1.DataSource = clsMilkRGPHead.GetLocationTypeEnum()
        FrmlocType1.DisplayMember = "Name"
        FrmlocType1.ValueMember = "Code"
        gv1.MasterTemplate.Columns.Add(FrmlocType1)
        FrmlocType1 = Nothing

        Dim bomcodeto As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        bomcodeto.FormatString = ""
        bomcodeto.Name = ColLocationSub
        bomcodeto.Width = 100
        bomcodeto.HeaderText = "Location Code"
        bomcodeto.HeaderImage = My.Resources.search4
        bomcodeto.TextImageRelation = TextImageRelation.TextBeforeImage
        gv1.MasterTemplate.Columns.Add(bomcodeto)
        bomcodeto = Nothing

        Dim repolocnameto As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repolocnameto.FormatString = ""
        repolocnameto.Name = ColLocationSubName
        repolocnameto.Width = 200
        repolocnameto.HeaderText = "Location"
        repolocnameto.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repolocnameto)
        repolocnameto = Nothing

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

        Dim repoFATRate As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoFATRate = New GridViewDecimalColumn()
        repoFATRate.FormatString = ""
        repoFATRate.HeaderText = "FAT(%)"
        repoFATRate.Name = ColFATRATE
        repoFATRate.Width = 100
        repoFATRate.Minimum = 0
        repoFATRate.FormatString = "{0:n2}"
        repoFATRate.DecimalPlaces = 2
        '' Anubhooti 06-Feb-2014 (Non-Editable By Amit Sir)
        repoFATRate.ReadOnly = False
        repoFATRate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoFATRate)

        Dim repoSNFRate As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoSNFRate = New GridViewDecimalColumn()
        repoSNFRate.FormatString = ""
        repoSNFRate.HeaderText = "SNF(%)"
        repoSNFRate.Name = ColSNFRATE
        repoSNFRate.Width = 100
        repoSNFRate.Minimum = 0
        repoSNFRate.FormatString = "{0:n2}"
        repoSNFRate.DecimalPlaces = 2
        '' Anubhooti 06-Feb-2014 (Non-Editable By Amit Sir)
        repoSNFRate.ReadOnly = False
        repoSNFRate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoSNFRate)

        Dim repoFATCost As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoFATCost = New GridViewDecimalColumn()
        repoFATCost.FormatString = ""
        repoFATCost.HeaderText = "FAT Rate"
        repoFATCost.Name = ColFATCost
        repoFATCost.Width = 100
        repoFATCost.Minimum = 0
        repoFATCost.FormatString = "{0:n2}"
        repoFATCost.DecimalPlaces = 2
        '' Anubhooti 06-Feb-2014 (Non-Editable By Amit Sir)
        repoFATCost.ReadOnly = True
        repoFATCost.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoFATCost)

        Dim repoSNFCost As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoSNFCost = New GridViewDecimalColumn()
        repoSNFCost.FormatString = ""
        repoSNFCost.HeaderText = "SNF Rate"
        repoSNFCost.Name = ColSNFCost
        repoSNFCost.Width = 100
        repoSNFCost.Minimum = 0
        repoSNFCost.FormatString = "{0:n2}"
        repoSNFCost.DecimalPlaces = 2
        '' Anubhooti 06-Feb-2014 (Non-Editable By Amit Sir)
        repoSNFCost.ReadOnly = True
        repoSNFCost.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoSNFCost)

        Dim repoFATKG As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoFATKG = New GridViewDecimalColumn()
        repoFATKG.FormatString = ""
        repoFATKG.HeaderText = "FAT KG"
        repoFATKG.Name = ColFATKG
        repoFATKG.Width = 100
        repoFATKG.Minimum = 0
        repoFATKG.FormatString = "{0:n2}"
        repoFATKG.DecimalPlaces = 2
        '' Anubhooti 06-Feb-2014 (Non-Editable By Amit Sir)
        repoFATKG.ReadOnly = True
        repoFATKG.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoFATKG)

        Dim repoSNFKG As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoSNFKG = New GridViewDecimalColumn()
        repoSNFKG.FormatString = ""
        repoSNFKG.HeaderText = "SNF KG"
        repoSNFKG.Name = ColSNFKG
        repoSNFKG.Width = 100
        repoSNFKG.Minimum = 0
        repoSNFKG.FormatString = "{0:n2}"
        repoSNFKG.DecimalPlaces = 2
        '' Anubhooti 06-Feb-2014 (Non-Editable By Amit Sir)
        repoSNFKG.ReadOnly = True
        repoSNFKG.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoSNFKG)

        Dim repoFATPrice As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoFATPrice = New GridViewDecimalColumn()
        repoFATPrice.FormatString = ""
        repoFATPrice.HeaderText = "FAT Value"
        repoFATPrice.Name = ColFATPrice
        repoFATPrice.Width = 100
        repoFATPrice.ReadOnly = True
        repoFATPrice.Minimum = 0
        repoFATPrice.FormatString = "{0:n2}"
        repoFATPrice.DecimalPlaces = 2
        '' Anubhooti 06-Feb-2014 (Non-Editable By Amit Sir)
        repoFATPrice.ReadOnly = True
        repoFATPrice.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoFATPrice)

        Dim repoSNFPrice As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoSNFPrice = New GridViewDecimalColumn()
        repoSNFPrice.FormatString = ""
        repoSNFPrice.HeaderText = "SNF Value"
        repoSNFPrice.Name = ColSNFPrice
        repoSNFPrice.ReadOnly = True
        repoSNFPrice.Width = 100
        repoSNFPrice.Minimum = 0
        repoSNFPrice.FormatString = "{0:n2}"
        repoSNFPrice.DecimalPlaces = 2
        '' Anubhooti 06-Feb-2014 (Non-Editable By Amit Sir)
        repoSNFPrice.ReadOnly = False
        repoSNFPrice.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoSNFPrice)

       
        '' Anubhooti 06-Feb-2014 (New Column By Amit Sir)
        Dim repoACost As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoACost = New GridViewDecimalColumn()
        repoACost.FormatString = ""
        repoACost.HeaderText = "Approx. Cost"
        repoACost.Name = colApproxCost
        repoACost.Width = 110
        repoACost.Minimum = 0
        repoACost.IsVisible = False
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
        repoSP.Width = 100
        'repoSP.Minimum = 0
        repoSP.ReadOnly = True
        repoSP.IsVisible = True
        If Not IsRGPAfterPO Then
            repoSP.IsVisible = False
        End If
        repoSP.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.MasterTemplate.Columns.Add(repoSP)

       
        clsCustomFieldGrid.LoadBlankGrid(gv1, MyBase.ArrDetailFields)

        gv1.AllowDeleteRow = True
        gv1.AllowAddNewRow = True
        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = True
        gv1.AllowRowReorder = False
        gv1.EnableSorting = False
        gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        gv1.TableElement.TableHeaderHeight = 25
        ReStoreGridLayout()
    End Sub

    Private Sub LoadQCBlankGrid()
        gv_qc.Rows.Clear()
        gv_qc.Columns.Clear()

        Dim reposno As GridViewDecimalColumn = New GridViewDecimalColumn()
        reposno.FormatString = ""
        reposno.Name = colQCsno
        reposno.Width = 60
        reposno.DecimalPlaces = 0
        reposno.HeaderText = "S.No."
        reposno.ReadOnly = True
        gv_qc.MasterTemplate.Columns.Add(reposno)
        reposno = Nothing

        Dim bomcodeFrom As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        bomcodeFrom.FormatString = ""
        bomcodeFrom.Name = colQCloc_code
        bomcodeFrom.Width = 80
        bomcodeFrom.HeaderText = "From Location Code"
        bomcodeFrom.ReadOnly = True
        gv_qc.MasterTemplate.Columns.Add(bomcodeFrom)
        bomcodeFrom = Nothing

        Dim bomcodeFromName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        bomcodeFromName.FormatString = ""
        bomcodeFromName.Name = colQCloc_name
        bomcodeFromName.Width = 130
        bomcodeFromName.HeaderText = "From Location"
        bomcodeFromName.ReadOnly = True
        gv_qc.MasterTemplate.Columns.Add(bomcodeFromName)
        bomcodeFromName = Nothing

        Dim bomcodeTO As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        bomcodeTO.FormatString = ""
        bomcodeTO.Name = colQCToloc_code
        bomcodeTO.Width = 0
        bomcodeTO.HeaderText = "To Location Code"
        bomcodeTO.ReadOnly = True
        bomcodeTO.IsVisible = False
        gv_qc.MasterTemplate.Columns.Add(bomcodeTO)
        bomcodeTO = Nothing

        Dim bomcodeTOName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        bomcodeTOName.FormatString = ""
        bomcodeTOName.Name = colQCToloc_name
        bomcodeTOName.Width = 0
        bomcodeTOName.IsVisible = False
        bomcodeTOName.HeaderText = "To Location"
        bomcodeTOName.ReadOnly = True
        gv_qc.MasterTemplate.Columns.Add(bomcodeTOName)
        bomcodeTOName = Nothing

        Dim bomcode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        bomcode.FormatString = ""
        bomcode.Name = colQCitemcode
        bomcode.Width = 100
        bomcode.HeaderText = "Item Code"
        bomcode.ReadOnly = True
        gv_qc.MasterTemplate.Columns.Add(bomcode)
        bomcode = Nothing

        Dim repolocname As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repolocname.FormatString = ""
        repolocname.Name = colQCiname
        repolocname.Width = 200
        repolocname.HeaderText = "Item Name"
        repolocname.ReadOnly = True
        gv_qc.MasterTemplate.Columns.Add(repolocname)
        repolocname = Nothing

        Dim bomcode1 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        bomcode1.FormatString = ""
        bomcode1.Name = colQCparamcode
        bomcode1.Width = 100
        bomcode1.HeaderText = "Parameter Code"
        bomcode1.ReadOnly = True
        'bomcode1.HeaderImage = Global.ERP.My.Resources.Resources.search4
        'bomcode1.TextImageRelation = TextImageRelation.TextBeforeImage
        gv_qc.MasterTemplate.Columns.Add(bomcode1)
        bomcode1 = Nothing

        Dim repolocname1 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repolocname1.FormatString = ""
        repolocname1.Name = colQCparam_desc
        repolocname1.Width = 200
        repolocname1.HeaderText = "Parameter Description"
        repolocname1.ReadOnly = True
        gv_qc.MasterTemplate.Columns.Add(repolocname1)
        repolocname1 = Nothing

        Dim repotype As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repotype.FormatString = ""
        repotype.Name = colQCparam_type
        repotype.Width = 80
        repotype.HeaderText = "Parameter Type"
        repotype.ReadOnly = True
        gv_qc.MasterTemplate.Columns.Add(repotype)
        repotype = Nothing

        Dim reponature As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        reponature.FormatString = ""
        reponature.Name = colQCparam_nature
        reponature.Width = 80
        reponature.HeaderText = "Nature"
        reponature.ReadOnly = True
        gv_qc.MasterTemplate.Columns.Add(reponature)
        reponature = Nothing

        Dim repolower As GridViewDecimalColumn = New GridViewDecimalColumn()
        repolower.Name = colQCrange1
        repolower.Width = 80
        repolower.HeaderText = "Std. Range"
        repolower.DecimalPlaces = 2
        repolower.ReadOnly = True
        gv_qc.MasterTemplate.Columns.Add(repolower)
        repolower = Nothing

        Dim repoupper As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoupper.Name = colQCrange2
        repoupper.Width = 80
        repoupper.HeaderText = "Upper Range"
        repoupper.DecimalPlaces = 2
        repoupper.IsVisible = False
        gv_qc.MasterTemplate.Columns.Add(repoupper)
        repoupper = Nothing

        Dim repovalue1 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repovalue1.Name = colQCvalue1
        repovalue1.Width = 80
        repovalue1.HeaderText = "Std. Value"
        repovalue1.ReadOnly = True
        gv_qc.MasterTemplate.Columns.Add(repovalue1)
        repovalue1 = Nothing

        Dim repovalue2 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repovalue2.Name = colQCvalue2
        repovalue2.Width = 80
        repovalue2.HeaderText = "Value-2"
        repovalue2.MaxLength = 30
        repovalue2.IsVisible = False
        gv_qc.MasterTemplate.Columns.Add(repovalue2)
        repovalue2 = Nothing

        Dim repostatus As GridViewComboBoxColumn = New GridViewComboBoxColumn()
        repostatus.Name = colQCstatus
        repostatus.Width = 80
        repostatus.HeaderText = "Std. Status(Yes/No)"
        repostatus.DataSource = LoadCombobox()
        repostatus.ValueMember = "Code"
        repostatus.DisplayMember = "Name"
        repostatus.ReadOnly = True
        gv_qc.MasterTemplate.Columns.Add(repostatus)
        repostatus = Nothing

        Dim repoupper1 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoupper1.Name = colQCRange
        repoupper1.Width = 80
        repoupper1.HeaderText = "Actual Range"
        repoupper1.DecimalPlaces = 2
        gv_qc.MasterTemplate.Columns.Add(repoupper1)
        repoupper1 = Nothing

        Dim repovalue21 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repovalue21.Name = colQCValue
        repovalue21.Width = 80
        repovalue21.HeaderText = "Actual Value"
        repovalue21.MaxLength = 30
        repovalue21.ReadOnly = True
        gv_qc.MasterTemplate.Columns.Add(repovalue21)
        repovalue21 = Nothing

        Dim repostatus1 As GridViewComboBoxColumn = New GridViewComboBoxColumn()
        repostatus1.Name = colQCOutStatus
        repostatus1.Width = 80
        repostatus1.HeaderText = "Actual Status(Yes/No)"
        repostatus1.DataSource = LoadCombobox()
        repostatus1.ValueMember = "Code"
        repostatus1.DisplayMember = "Name"
        'repostatus.ReadOnly = True
        gv_qc.MasterTemplate.Columns.Add(repostatus1)
        repostatus1 = Nothing

        Dim repoHis As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoHis.FormatString = ""
        repoHis.Name = colQCHistort
        repoHis.Width = 80
        repoHis.ReadOnly = True
        repoHis.HeaderText = "History"
        gv_qc.MasterTemplate.Columns.Add(repoHis)
        repoHis = Nothing

        Dim reporem As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        reporem.FormatString = ""
        reporem.Name = colQCremarks
        reporem.Width = 150
        reporem.MaxLength = 200
        reporem.HeaderText = "Remarks"
        gv_qc.MasterTemplate.Columns.Add(reporem)
        reporem = Nothing

        gv_qc.AllowDeleteRow = True
        gv_qc.AllowAddNewRow = False
        gv_qc.ShowGroupPanel = False
        gv_qc.AllowColumnReorder = True
        gv_qc.AllowRowReorder = False
        gv_qc.EnableSorting = False
        gv_qc.EnableFiltering = False
        gv_qc.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv_qc.MasterTemplate.ShowRowHeaderColumn = False
        gv_qc.Rows.AddNew()
    End Sub

    Function LoadCombobox() As DataTable
        Dim qry As String = "select * from (select '' as Code,'None' as Name union all select 'YES' as Code,'YES' as Name union all select 'NO' as Code,'NO' as Name)a"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        Return dt
    End Function

    Function LoadComboboxOK() As DataTable
        Dim qry As String = "select * from (select '' as Code,'None' as Name union all select 'Ok' as Code,'Ok' as Name union all select 'Not Ok' as Code,'Not Ok' as Name)a"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        Return dt
    End Function

    Private Sub FillQCGrid(ByVal CurrentIcode As String, ByVal Frm_Loc_Code As String, ByVal To_Loc_Code As String)
        Try
            If clsCommon.myLen(gv1.Rows(0).Cells(colICode).Value) <= 0 Then
                RadPageView1.SelectedPage = RadPageViewPage1
                gv1.Focus()
                gv1.Select()
                Throw New Exception("Fill item detail first.")
            End If
            Dim allicode As String = ""

            qry = "select count(*) from INFORMATION_SCHEMA.TABLES where TABLE_NAME='TEMP_LOC_QC_PARAM'"
            check = clsDBFuncationality.getSingleValue(qry)
            If check > 0 Then
                clsDBFuncationality.ExecuteNonQuery("drop table TEMP_LOC_QC_PARAM")
                clsDBFuncationality.ExecuteNonQuery("create table TEMP_LOC_QC_PARAM (Item_Code varchar(50) null,Frm_Loc varchar(50) null,To_Loc varchar(50) null)")
            Else
                clsDBFuncationality.ExecuteNonQuery("create table TEMP_LOC_QC_PARAM (Item_Code varchar(50) null,Frm_Loc varchar(50) null,To_Loc varchar(50) null)")
            End If

            If CurrentIcode IsNot Nothing AndAlso clsCommon.myLen(CurrentIcode) > 0 Then
                allicode = CurrentIcode
                clsDBFuncationality.ExecuteNonQuery("insert into TEMP_LOC_QC_PARAM select '" + CurrentIcode + "','" + Frm_Loc_Code + "','" + To_Loc_Code + "'")
            Else
                For Each grow As GridViewRowInfo In gv1.Rows
                    allicode = allicode + "','" + clsCommon.myCstr(grow.Cells(colICode).Value)
                    clsDBFuncationality.ExecuteNonQuery("insert into TEMP_LOC_QC_PARAM select '" + clsCommon.myCstr(grow.Cells(colICode).Value) + "','" + clsCommon.myCstr(txtLocation.Value) + "','" + clsCommon.myCstr(grow.Cells(ColLocationSub).Value) + "'")
                Next
            End If

            If clsCommon.myLen(allicode) > 0 AndAlso allicode.Substring(0, 3) = "','" Then
                allicode = allicode.Substring(3, allicode.Length - 3)
            End If

            qry = "select ROW_NUMBER() over(order by TSPL_ITEM_QC_PARAMETER_MASTER.Code) as Sno,TEMP_LOC_QC_PARAM.frm_loc,TEMP_LOC_QC_PARAM.to_loc,TSPL_ITEM_QC_PARAMETER_MASTER.Item_Code,TSPL_ITEM_MASTER.Item_Desc,TSPL_ITEM_MASTER.HSN_CODE,TSPL_ITEM_QC_PARAMETER_MASTER.Code,TSPL_PARAMETER_MASTER.Description as parameterdesc,TSPL_PARAMETER_MASTER.Type,(Case when TSPL_PARAMETER_MASTER.Nature='A' then 'Alphanumeric' else case when TSPL_PARAMETER_MASTER.Nature='B' then 'Boolean' else case when TSPL_PARAMETER_MASTER.Nature='R' then 'Range' end end end) as Nature,sum(TSPL_ITEM_QC_PARAMETER_MASTER.actual_range)/count(TSPL_ITEM_QC_PARAMETER_MASTER.Item_Code) as Lower_range,sum(TSPL_ITEM_QC_PARAMETER_MASTER.Upper_range)/count(TSPL_ITEM_QC_PARAMETER_MASTER.Item_Code) as Upper_range,max(TSPL_ITEM_QC_PARAMETER_MASTER.actual_value) as Value1,max(TSPL_ITEM_QC_PARAMETER_MASTER.Value2) as Value2,max(TSPL_ITEM_QC_PARAMETER_MASTER.actual_status) as Status from TSPL_ITEM_QC_PARAMETER_MASTER "
            qry += " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_ITEM_QC_PARAMETER_MASTER.Item_Code left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code "
            qry += " left outer join TEMP_LOC_QC_PARAM on TEMP_LOC_QC_PARAM.item_code=TSPL_ITEM_QC_PARAMETER_MASTER.item_code "
            qry += " where TSPL_ITEM_QC_PARAMETER_MASTER.Item_Code in ('" + allicode + "') group by TEMP_LOC_QC_PARAM.frm_loc,TEMP_LOC_QC_PARAM.to_loc,TSPL_ITEM_QC_PARAMETER_MASTER.Item_Code,TSPL_ITEM_MASTER.Item_Desc,TSPL_ITEM_QC_PARAMETER_MASTER.Code,TSPL_PARAMETER_MASTER.Description,TSPL_PARAMETER_MASTER.Type,TSPL_PARAMETER_MASTER.Nature,TSPL_ITEM_MASTER.HSN_CODE"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

            If gv_qc.Rows.Count > 0 AndAlso clsCommon.myLen(gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCitemcode).Value) <= 0 Then
                gv_qc.Rows.RemoveAt(gv_qc.Rows.Count - 1)
            End If

            Dim found As Boolean = False
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    found = False
                    For Each grow As GridViewRowInfo In gv_qc.Rows
                        If clsCommon.CompairString(clsCommon.myCstr(grow.Cells(colQCparamcode).Value), clsCommon.myCstr(dr("Code"))) = CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(grow.Cells(colQCitemcode).Value), clsCommon.myCstr(dr("item_code"))) = CompairStringResult.Equal Then
                            found = True
                            GoTo a
                        End If
                    Next
a:
                    If Not found And clsCommon.myLen(clsCommon.myCstr(dr("frm_loc"))) > 0 Then
                        gv_qc.Rows.AddNew()
                        gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCsno).Value = CInt(dr("sno"))
                        gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCloc_code).Value = clsCommon.myCstr(dr("frm_loc"))
                        gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCloc_name).Value = clsLocation.GetName(clsCommon.myCstr(dr("frm_loc")), Nothing)
                        gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCToloc_code).Value = clsCommon.myCstr(dr("to_loc"))
                        gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCToloc_name).Value = clsLocation.GetName(clsCommon.myCstr(dr("to_loc")), Nothing)
                        gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCitemcode).Value = clsCommon.myCstr(dr("Item_Code"))
                        gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCiname).Value = clsCommon.myCstr(dr("Item_Desc"))
                        gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCHSNcode).Value = clsCommon.myCstr(dr("HSN_CODE"))
                        gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCparamcode).Value = clsCommon.myCstr(dr("Code"))
                        gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCparam_desc).Value = clsCommon.myCstr(dr("parameterdesc"))
                        gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCparam_type).Value = clsCommon.myCstr(dr("Type"))
                        gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCparam_nature).Value = clsCommon.myCstr(dr("Nature"))
                        gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCrange1).Value = clsCommon.myCdbl(dr("Lower_range"))
                        gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCrange2).Value = clsCommon.myCdbl(dr("Upper_range"))
                        gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCstatus).Value = clsCommon.myCstr(dr("Status"))
                        gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCvalue1).Value = clsCommon.myCstr(dr("Value1"))
                        gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCvalue2).Value = clsCommon.myCstr(dr("Value2"))

                        If clsCommon.CompairString(clsCommon.myCstr(gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCparam_nature).Value), "Range") = CompairStringResult.Equal Then
                            gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCValue).ReadOnly = True
                            gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCOutStatus).ReadOnly = True
                        End If
                        If clsCommon.CompairString(clsCommon.myCstr(gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCparam_nature).Value), "Boolean") = CompairStringResult.Equal Then
                            gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCRange).ReadOnly = True
                            gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCValue).ReadOnly = True
                        End If
                        If clsCommon.CompairString(clsCommon.myCstr(gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCparam_nature).Value), "Alphanumeric") = CompairStringResult.Equal Then
                            gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCRange).ReadOnly = True
                            gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCOutStatus).ReadOnly = True
                        End If
                    End If ''found cond.

                Next
            Else
                'Throw New Exception("Mapped first QC parameter with items in Item Master screen")
            End If

            '===========refresh sno==
            For Each grow As GridViewRowInfo In gv_qc.Rows
                grow.Cells(colQCsno).Value = grow.Index + 1
            Next

            dt = Nothing
        Catch ex As Exception
            qry = "select count(*) from INFORMATION_SCHEMA.TABLES where TABLE_NAME='TEMP_LOC_QC_PARAM'"
            check = clsDBFuncationality.getSingleValue(qry)
            If check > 0 Then
                clsDBFuncationality.ExecuteNonQuery("drop table TEMP_LOC_QC_PARAM")
            End If
            clsCommon.MyMessageBoxShow(ex.Message)
        Finally
            qry = "select count(*) from INFORMATION_SCHEMA.TABLES where TABLE_NAME='TEMP_LOC_QC_PARAM'"
            check = clsDBFuncationality.getSingleValue(qry)
            If check > 0 Then
                clsDBFuncationality.ExecuteNonQuery("drop table TEMP_LOC_QC_PARAM")
            End If

        End Try
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
                isInsideLoadData = True
                If Not isCellValueChangedOpen Then
                    isCellValueChangedOpen = True
                    If e.Column.FieldName.StartsWith("_CFLD_") Then
                        clsCustomFieldGrid.getFinderForCustomFieldGrid(gv1, e.Column.Name.ToString, MyBase.Form_ID)
                    End If
                    If (e.Column Is gv1.Columns(colICode) OrElse e.Column Is gv1.Columns(colUnit) OrElse e.Column Is gv1.Columns(colQty) Or e.Column Is gv1.Columns(ColLocationSub) Or e.Column Is gv1.Columns(ColSNFRATE) Or e.Column Is gv1.Columns(ColFATRATE) Or e.Column Is gv1.Columns(ColSNFCost) Or e.Column Is gv1.Columns(ColFATCost)) Then 'OrElse e.Column Is gv1.Columns(colRate)
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

                        If e.Column Is gv1.Columns(colQty) Then 'OrElse e.Column Is gv1.Columns(colRate)
                            UpdateCurrentRow()
                            UpdateAllTotals()
                        ElseIf e.Column Is gv1.Columns(colUnit) Then
                            OpenUOMList(False)
                            Cal_FAT()
                            Cal_SNF()
                        ElseIf e.Column Is gv1.Columns(colICode) Then
                            '-----------------check when thrd party location on----------------------------------------
                            If chkthirdparty.Checked Then
                                If clsCommon.myLen(cboItemType.SelectedValue) <= 0 Then
                                    clsCommon.MyMessageBoxShow("Please Select Item Type First", Me.Text)
                                    isInsideLoadData = False
                                    isCellValueChangedOpen = False
                                    Return
                                ElseIf clsCommon.myLen(txtLocation.Value) <= 0 Then
                                    clsCommon.MyMessageBoxShow("Please Select Location First", Me.Text)
                                    isInsideLoadData = False
                                    isCellValueChangedOpen = False
                                    Return
                                End If
                            End If
                            '--------------------------------------------------------------------------------------------

                            OpenICodeList(False)
                            Cal_FAT()
                            Cal_SNF()
                            'ElseIf e.Column Is gv1.Columns(ColLocationHeader) Then
                            '    OpenLocation(False)

                        ElseIf e.Column Is gv1.Columns(ColLocationSub) Then
                            ToOpenLocation(False)
                            setBalance()
                        End If



                        If (e.Column Is gv1.Columns(colQty)) Or (e.Column Is gv1.Columns(ColFATRATE)) Or (e.Column Is gv1.Columns(ColFATKG)) Or (e.Column Is gv1.Columns(ColFATCost)) Then
                            'If clsCommon.myCdbl(gv1.CurrentRow.Cells(colBalQty).Value) <= 0 Then
                            '    gv1.CurrentRow.Cells(colBalQty).Value = UcItemBalance1
                            'End If

                            If clsCommon.myCdbl(gv1.CurrentRow.Cells(colQty).Value) > clsCommon.myCdbl(gv1.CurrentRow.Cells(colBalQty).Value) Then
                                RadPageView1.SelectedPage = RadPageViewPage1
                                gv1.CurrentRow.Cells(colQty).Value = Nothing
                                Throw New Exception("Filled quantity cannot be more than available quantity.")
                            End If
                            Cal_FAT()
                        End If

                        If (e.Column Is gv1.Columns(colQty)) Or (e.Column Is gv1.Columns(ColSNFRATE)) Or (e.Column Is gv1.Columns(ColSNFKG)) Or (e.Column Is gv1.Columns(ColSNFCost)) Then
                            If clsCommon.myCdbl(gv1.CurrentRow.Cells(colQty).Value) > clsCommon.myCdbl(gv1.CurrentRow.Cells(colBalQty).Value) Then
                                RadPageView1.SelectedPage = RadPageViewPage1
                                gv1.CurrentRow.Cells(colQty).Value = Nothing
                                Throw New Exception("Filled quantity cannot be more than available quantity.")
                            End If
                            Cal_SNF()
                        End If
                    End If
                    setGridFocus()
                End If

                isCellValueChangedOpen = False
                isInsideLoadData = False
            End If
        Catch ex As Exception
            isCellValueChangedOpen = False
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub Cal_FAT()
        Try
            Dim qty As Decimal = Nothing
            Dim fat As Decimal = Nothing
            Dim fat_kg As Decimal = Nothing
            Dim fat_Amt As Decimal = Nothing

            qty = clsCommon.myCdbl(gv1.CurrentRow.Cells(colqty).Value)
            fat = clsCommon.myCdbl(gv1.CurrentRow.Cells(colfatRate).Value)

            fat_kg = clsBOM.GetFatSNFKG_AfterConversion(clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value), clsCommon.myCstr(gv1.CurrentRow.Cells(colUnit).Value), qty, fat, Nothing) ' Math.Round((qty * fat) / 100, DecimalPoint)
            gv1.CurrentRow.Cells(ColFATKG).Value = fat_kg

            fat_Amt = clsCommon.myCdbl(gv1.CurrentRow.Cells(ColFATCost).Value) * clsCommon.myCdbl(fat_kg)
            gv1.CurrentRow.Cells(ColFATPrice).Value = fat_Amt

            UpdateCurrentRow()
            UpdateAllTotals()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub Cal_SNF()
        Try
            Dim qty As Decimal = Nothing
            Dim fat As Decimal = Nothing
            Dim fat_kg As Decimal = Nothing
            Dim fat_Amt As Decimal = Nothing

            qty = clsCommon.myCdbl(gv1.CurrentRow.Cells(colqty).Value)
            fat = clsCommon.myCdbl(gv1.CurrentRow.Cells(colSnfRate).Value)

            fat_kg = clsBOM.GetFatSNFKG_AfterConversion(clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value), clsCommon.myCstr(gv1.CurrentRow.Cells(colUnit).Value), qty, fat, Nothing) ' Math.Round((qty * fat) / 100, DecimalPoint)
            gv1.CurrentRow.Cells(ColSNFKG).Value = fat_kg

            fat_Amt = clsCommon.myCdbl(gv1.CurrentRow.Cells(ColSNFCost).Value) * clsCommon.myCdbl(fat_kg)
            gv1.CurrentRow.Cells(ColSNFPrice).Value = fat_Amt

            UpdateCurrentRow()
            UpdateAllTotals()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub ToOpenLocation(ByVal isButtonClicked As Boolean)
        If clsCommon.myLen(gv1.CurrentRow.Cells(ColLocationType).Value) <= 0 Then
            RadPageView1.SelectedPage = RadPageViewPage1
            Throw New Exception("Select first To location type[Sub-Location or Section].")
        End If

        Dim oldIcode As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colicode).Value)
        Dim oldFrmLoc As String = clsCommon.myCstr(txtLocation.Value)
        Dim OldToLoc As String = clsCommon.myCstr(gv1.CurrentRow.Cells(ColLocationSub).Value)

        If clsCommon.myLen(oldIcode) > 0 Then
            RemoveCurrentItemQCRow(oldIcode, oldFrmLoc, OldToLoc)
        End If

        Dim whrcls As String = ""

        Dim loc_type As Integer = 0
        If clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(ColLocationType).Value), "1") = CompairStringResult.Equal Then
            loc_type = 2
        ElseIf clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(ColLocationType).Value), "2") = CompairStringResult.Equal Then
            loc_type = 1
        ElseIf clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(ColLocationType).Value), "3") = CompairStringResult.Equal Then
            loc_type = 0
        End If

        If clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(ColLocationType).Value), "3") = CompairStringResult.Equal Then 'btnTo_Sectn.IsChecked
            whrcls = " location_code in (Select location_code from tspl_location_master where main_location_code='" + txtLocation.Value + "' and isnull(Is_Section,'N')='Y')"
            ' clsCommon.MyMessageBoxShow("No consumption section found.")
            ' gv1.CurrentRow.Cells(ColLocationHeader).Value = Nothing
            'gv1.CurrentRow.Cells(ColLocationSub).Value = Nothing
            'Exit Sub

            'If clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colitemproducttype).Value), "Milk") <> CompairStringResult.Equal Then
            '    whrcls = " location_code in (Select location_code from tspl_location_master where (main_location_code='" + txtLocation.Value + "' and isnull(Is_Section,'N')='Y') or (isnull(csa_type,'N')<>'Y' and ISNULL(Is_Sub_Location,'N')<>'Y'))"
            'End If
        ElseIf clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(ColLocationType).Value), "2") = CompairStringResult.Equal Then 'btnTo_SubLoc.IsChecked
            whrcls = " location_code in (Select location_code from tspl_location_master where main_location_code='" + txtLocation.Value + "' and isnull(Is_Sub_Location,'N')='Y')"
            'If clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colitemproducttype).Value), "Milk") <> CompairStringResult.Equal Then
            '    whrcls = " location_code in (Select location_code from tspl_location_master where (main_location_code='" + txtLocation.Value + "' and isnull(Is_Sub_Location,'N')='Y') or (isnull(csa_type,'N')<>'Y' and ISNULL(Is_Section,'N')<>'Y'))"
            'End If
        ElseIf clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(ColLocationType).Value), "1") = CompairStringResult.Equal Then
            whrcls = " tspl_location_master.location_code = '" + txtLocation.Value + "' " '  and tspl_location_master.location_code in (" + arrLoc + ") and isnull(csa_type,'N')<>'Y' and isnull(Is_Section,'N')<>'Y' and isnull(Is_Sub_Location,'N')<>'Y'
        End If
        Dim intRow As Integer = gv1.CurrentRow.Index
        Dim frm As New FrmPPIssueChildScrren()
        frm.LoadData(txtDate.Text, clsCommon.myCstr(gv1.Rows(intRow).Cells(colICode).Value), clsCommon.myCstr(gv1.Rows(intRow).Cells(colUnit).Value), txtLocation.Value, "MI", loc_type, arrLoc, txtDate.Value)
        frm.ShowDialog()

        If frm.Arr_Loc IsNot Nothing AndAlso frm.Arr_Loc.Count > 0 Then
            Dim ii As Integer = intRow

            For Each Loc_Code As String In frm.Arr_Loc
                'If intRow <> ii Then
                gv1.CurrentRow.Cells(ColLocationSub).Value = Loc_Code 'clsCommon.myCstr(clsLocation.getFinder(whrcls, clsCommon.myCstr(gv1.CurrentRow.Cells(ColLocationSub).Value), isButtonClicked)) '" isnull(csa_type,'N')='N' and location_code in (" + arrLoc + ")"
                gv1.CurrentRow.Cells(ColLocationSubName).Value = clsLocation.GetName(Loc_Code, Nothing) 'clsLocation.GetName(clsCommon.myCstr(gv1.CurrentRow.Cells(ColLocationSub).Value), Nothing)
                ' End If
            Next
        End If
        'gv1.CurrentRow.Cells(ColLocationSub).Value = clsCommon.myCstr(clsLocation.getFinder(whrcls, clsCommon.myCstr(gv1.CurrentRow.Cells(ColLocationSub).Value), isButtonClicked)) '" isnull(csa_type,'N')='N' and location_code in (" + arrLoc + ")"
        'gv1.CurrentRow.Cells(ColLocationSubName).Value = clsLocation.GetName(clsCommon.myCstr(gv1.CurrentRow.Cells(ColLocationSub).Value), Nothing)


        FillAvail_Stock(gv1.CurrentRow.Index, clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value), clsCommon.myCstr(txtLocation.Value), clsCommon.myCstr(gv1.CurrentRow.Cells(ColLocationSub).Value), "MI", clsCommon.myCstr(gv1.CurrentRow.Cells(colUnit).Value), loc_type)
        FillQCGrid(clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value), clsCommon.myCstr(txtLocation.Value), clsCommon.myCstr(gv1.CurrentRow.Cells(ColLocationSub).Value))

    End Sub

    Private Sub OpenLocation(ByVal isButtonClicked As Boolean)
        Dim intRow As Integer = gv1.CurrentRow.Index

        If clsCommon.myLen(gv1.Rows(intRow).Cells(ColLocationType).Value) <= 0 Then
            RadPageView1.SelectedPage = RadPageViewPage1
            Throw New Exception("Select first location type[Sub-Location or Section].")
        End If
        Dim oldIcode As String = clsCommon.myCstr(gv1.Rows(intRow).Cells(colicode).Value)
        Dim oldFrmLoc As String = clsCommon.myCstr(txtLocation.Value)
        Dim OldToLoc As String = clsCommon.myCstr(gv1.Rows(intRow).Cells(ColLocationSub).Value)
        If clsCommon.myLen(oldIcode) > 0 Then
            RemoveCurrentItemQCRow(oldIcode, oldFrmLoc, OldToLoc)
        End If

        Dim loc_type As Integer = 0
        If clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(intRow).Cells(ColLocationType).Value), "1") = CompairStringResult.Equal Then
            loc_type = 2
        ElseIf clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(intRow).Cells(ColLocationType).Value), "2") = CompairStringResult.Equal Then
            loc_type = 1
        ElseIf clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(intRow).Cells(ColLocationType).Value), "3") = CompairStringResult.Equal Then
            loc_type = 0
        End If

        If clsCommon.myLen(arrLoc) <= 0 Then
            clsCommon.MyMessageBoxShow("No location rights.")
            Exit Sub
        End If

        Dim frm As New FrmPPIssueChildScrren()
        frm.LoadData(clsCommon.myCDate(txtDate.Value), clsCommon.myCstr(gv1.Rows(intRow).Cells(colICode).Value), clsCommon.myCstr(gv1.Rows(intRow).Cells(colUnit).Value), txtLocation.Value, "MI", loc_type, arrLoc, txtDate.Value)
        frm.ShowDialog()

        If frm.Arr_Loc IsNot Nothing AndAlso frm.Arr_Loc.Count > 0 Then
            Dim ii As Integer = intRow

            For Each Loc_Code As String In frm.Arr_Loc
                If intRow <> ii Then
                    'gv1.Rows(ii).Cells(ColLocationHeader).Value = clsCommon.myCstr(gv1.Rows(intRow).Cells(ColLocationHeader).Value)
                    gv1.Rows(ii).Cells(ColLocationType).Value = clsCommon.myCstr(gv1.Rows(intRow).Cells(ColLocationType).Value)
                    gv1.Rows(ii).Cells(ColLocationSub).Value = clsCommon.myCstr(gv1.Rows(intRow).Cells(ColLocationSub).Value)
                    gv1.Rows(ii).Cells(ColLocationSubName).Value = clsCommon.myCstr(gv1.Rows(intRow).Cells(ColLocationSubName).Value)
                    gv1.Rows(ii).Cells(colICode).Value = clsCommon.myCstr(gv1.Rows(intRow).Cells(colICode).Value)
                    gv1.Rows(ii).Cells(colIName).Value = clsCommon.myCstr(gv1.Rows(intRow).Cells(colIName).Value)
                    ' gv1.Rows(ii).Cells(colitemtype).Value = clsCommon.myCstr(gv1.Rows(intRow).Cells(colitemtype).Value)
                    ' gv1.Rows(ii).Cells(colitemproducttype).Value = clsCommon.myCstr(gv1.Rows(intRow).Cells(colitemproducttype).Value)
                    gv1.Rows(ii).Cells(colUnit).Value = clsCommon.myCstr(gv1.Rows(intRow).Cells(colUnit).Value)
                    ' gv1.Rows(ii).Cells(colUnit).Value = clsCommon.myCstr(gv1.Rows(intRow).Cells(colunitDesc).Value)
                    ' gv1.Rows(ii).Cells(colrequrdqty).Value = Math.Round(clsCommon.myCdbl(gv1.Rows(intRow).Cells(colrequrdqty).Value), DecimalPoint)
                    gv1.Rows(ii).Cells(colQty).Value = Math.Round(clsCommon.myCdbl(gv1.Rows(intRow).Cells(colQty).Value), DecimalPoint)
                    gv1.Rows(ii).Cells(ColFATRATE).Value = Math.Round(clsCommon.myCdbl(gv1.Rows(intRow).Cells(ColFATRATE).Value), 2)
                    gv1.Rows(ii).Cells(ColSNFRATE).Value = Math.Round(clsCommon.myCdbl(gv1.Rows(intRow).Cells(ColSNFRATE).Value), 2)
                    gv1.Rows(ii).Cells(ColFATKG).Value = Math.Round(clsCommon.myCdbl(gv1.Rows(intRow).Cells(ColFATKG).Value), DecimalPoint)
                    gv1.Rows(ii).Cells(ColSNFKG).Value = Math.Round(clsCommon.myCdbl(gv1.Rows(intRow).Cells(ColSNFKG).Value), DecimalPoint)
                    'gv1.Rows(ii).Cells(col).Value = clsCommon.myCstr(gv1.Rows(intRow).Cells(colrem).Value)

                    'If clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(ii).Cells(colitemproducttype).Value), "Milk") = CompairStringResult.Equal Then
                    '    'gv1.Rows(ii).Cells(colfatkg).ReadOnly = False
                    '    'gv1.Rows(ii).Cells(colfatRate).ReadOnly = False
                    '    'gv1.Rows(ii).Cells(colsnfkg).ReadOnly = False
                    '    'gv1.Rows(ii).Cells(colSnfRate).ReadOnly = False
                    'Else
                    '    gv1.Rows(ii).Cells(ColFATRATE).Value = Nothing
                    '    gv1.Rows(ii).Cells(ColSNFRATE).Value = Nothing
                    '    gv1.Rows(ii).Cells(ColFATKG).ReadOnly = True
                    '    gv1.Rows(ii).Cells(ColFATRATE).ReadOnly = True
                    '    gv1.Rows(ii).Cells(ColSNFKG).ReadOnly = True
                    '    gv1.Rows(ii).Cells(ColSNFRATE).ReadOnly = True
                    'End If
                End If
                'gv1.Rows(ii).Cells(ColLocationHeader).Value = Loc_Code
                'gv1.Rows(ii).Cells(ColLocationHeaderName).Value = clsLocation.GetName(Loc_Code, Nothing)

                FillAvail_Stock(ii, clsCommon.myCstr(gv1.Rows(ii).Cells(colICode).Value), clsCommon.myCstr(txtLocation.Value), clsCommon.myCstr(gv1.Rows(ii).Cells(ColLocationSub).Value), "MI", clsCommon.myCstr(gv1.Rows(ii).Cells(colUnit).Value), loc_type)
                FillQCGrid(clsCommon.myCstr(gv1.Rows(ii).Cells(colICode).Value), clsCommon.myCstr(txtLocation.Value), clsCommon.myCstr(gv1.Rows(ii).Cells(ColLocationSub).Value))

                If intRow <> ii Then
                    gv1.Rows.Move(ii, intRow + 1)
                End If
                gv1.Rows.AddNew()
                ii = gv1.Rows.Count - 1
            Next
        Else
            'gv1.Rows(intRow).Cells(ColLocationHeader).Value = ""
            'gv1.Rows(intRow).Cells(ColLocationHeaderName).Value = ""
            gv1.Rows(intRow).Cells(colBalQty).Value = Nothing
        End If

        RefreshSerialNo()
    End Sub
    Private Sub RefreshSerialNo()

        For Each grow As GridViewRowInfo In gv1.Rows
            If clsCommon.myLen(grow.Cells(colicode).Value) > 0 Then
                grow.Cells(colLineNo).Value = grow.Index + 1
            End If
        Next

        '============remove extra rows===========
        For ii As Integer = gv1.Rows.Count - 1 To 0 Step -1
            If clsCommon.myLen(gv1.Rows(ii).Cells(colicode).Value) <= 0 Then
                gv1.Rows.RemoveAt(ii)
            End If
        Next
        gv1.Rows.AddNew()
    End Sub

    Sub OpenUOMList(ByVal isButtonClick As Boolean)
        Dim strICode As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value)
        If clsCommon.myLen(strICode) > 0 Then
            Dim qry As String = "select UOM_Code as Code,UOM_Description as [Description] from TSPL_ITEM_UOM_DETAIL"
            Dim whrCls As String = "Item_Code='" + strICode + "'"
            gv1.CurrentRow.Cells(colUnit).Value = clsCommon.ShowSelectForm("SRNItefndnder", qry, "Code", whrCls, clsCommon.myCstr(gv1.CurrentRow.Cells(colUnit).Value), "Code", isButtonClick)

            Dim loc_type As Integer = 0
            If clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(ColLocationType).Value), "1") = CompairStringResult.Equal Then
                loc_type = 2
            ElseIf clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(ColLocationType).Value), "2") = CompairStringResult.Equal Then
                loc_type = 1
            ElseIf clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(ColLocationType).Value), "3") = CompairStringResult.Equal Then
                loc_type = 0
            End If
            FillAvail_Stock(CInt(gv1.CurrentCell.RowIndex), clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value), clsCommon.myCstr(txtLocation.Value), clsCommon.myCstr(gv1.CurrentRow.Cells(ColLocationSub).Value), "MI", clsCommon.myCstr(gv1.CurrentRow.Cells(colUnit).Value), loc_type)
            FillQCGrid(clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value), clsCommon.myCstr(gv1.CurrentRow.Cells(ColLocationSub).Value), clsCommon.myCstr(gv1.CurrentRow.Cells(ColLocationSub).Value))
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
                    '    gv1.CurrentColumn = gv1.Columns(colRate)
                    'ElseIf gv1.CurrentColumn Is gv1.Columns(colRate) Then
                    '    gv1.CurrentRow = gv1.Rows(intCurrRow + 1)
                    '    gv1.CurrentColumn = gv1.Columns(colICode)
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Sub OpenICodeList(ByVal isButtonClick As Boolean)
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
       



        Dim obj As clsItemMaster
        If chkthirdparty.Checked Then
            obj = clsItemMaster.FinderForThirdPartyItem(clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value), clsCommon.myCstr(cboItemType.SelectedValue), txtLocation.Value, isButtonClick)
        Else
            obj = clsItemMaster.FinderForItem(clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value), "", isButtonClick, Nothing, whrcls)
        End If

        If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Item_Code) > 0 Then
            gv1.CurrentRow.Cells(colICode).Value = obj.Item_Code
            gv1.CurrentRow.Cells(colIName).Value = obj.Item_Desc
            gv1.CurrentRow.Cells(colUnit).Value = obj.Unit_Code

            '' Anubhooti 06-Jan-2015 (Costing Method Avg/FIFO/LIFO)
            Dim strDate As String = txtDate.Value
            Dim dblUnitCost As Double = 0
            Dim dblCostMethod As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Costing_Method as Costing_Method from TSPL_ITEM_MASTER left outer join TSPL_PURCHASE_ACCOUNTS on TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code=TSPL_ITEM_MASTER.Purchase_Class_Code where Item_Code='" & clsCommon.myCstr(obj.Item_Code) & "' "))
            If dblCostMethod <> 0 Then
                dblUnitCost = clsInventoryMovement.GetCost(dblCostMethod, clsCommon.myCstr(obj.Item_Code), txtLocation.Value, 1, strDate, strDate, False, Nothing)
                '  gv1.CurrentRow.Cells(colRate).Value = dblUnitCost
                gv1.CurrentRow.Cells(colApproxCost).Value = dblUnitCost
            Else
                '   gv1.CurrentRow.Cells(colRate).Value = 0
                gv1.CurrentRow.Cells(colApproxCost).Value = 0
            End If
            ''
            Dim loc_type As Integer = 0
            If clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(ColLocationType).Value), "1") = CompairStringResult.Equal Then
                loc_type = 2
            ElseIf clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(ColLocationType).Value), "2") = CompairStringResult.Equal Then
                loc_type = 1
            ElseIf clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(ColLocationType).Value), "3") = CompairStringResult.Equal Then
                loc_type = 0
            End If
            FillAvail_Stock(CInt(gv1.CurrentCell.RowIndex), clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value), clsCommon.myCstr(txtLocation.Value), clsCommon.myCstr(gv1.CurrentRow.Cells(ColLocationSub).Value), "MI", clsCommon.myCstr(gv1.CurrentRow.Cells(colUnit).Value), loc_type)
            FillQCGrid(clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value), clsCommon.myCstr(gv1.CurrentRow.Cells(ColLocationSub).Value), clsCommon.myCstr(gv1.CurrentRow.Cells(ColLocationSub).Value))
        Else
            gv1.CurrentRow.Cells(colICode).Value = ""
            gv1.CurrentRow.Cells(colIName).Value = ""
            gv1.CurrentRow.Cells(colUnit).Value = ""
        End If
        setBalance()
    End Sub

    Private Sub UpdateCurrentRow()
        Dim dblQty As Double = clsCommon.myCdbl(gv1.CurrentRow.Cells(colQty).Value)
        Dim dblRate As Double = clsCommon.myCdbl(gv1.CurrentRow.Cells(colApproxCost).Value)
        Dim dblFAT As Double = clsCommon.myCdbl(gv1.CurrentRow.Cells(ColFATPrice).Value)
        Dim dblSNF As Double = clsCommon.myCdbl(gv1.CurrentRow.Cells(ColSNFPrice).Value)
        Dim dblAmt As Double = dblFAT + dblSNF 'dblQty * dblRate
        gv1.CurrentRow.Cells(colAmt).Value = Math.Round(dblAmt, 2)
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
        isNewEntry = True
        btnSave.Text = "Save"
        btnSave.Enabled = True
        btnPost.Enabled = True
        btnDelete.Enabled = True
        txtDate.Focus()
        LoadBlankGrid()
        LoadQCBlankGrid()
        gv1.Rows.AddNew()
        RadLabel2.Text = "Vendor No"
        HideUnhideColumn()
        txtModeOfTransport.Text = ""
        txtCashMemoDetail.Text = ""

        ''richa Ticket No BM00000003061 on 01/08/2014

        '-------------------------------------------
        '' Anubhooti 07-Oct-2014 BM00000003663

        '' Anubhooti 10-Dec-2014 BM00000003662
        CmbItemConType.DataSource = GetItemType()
        CmbItemConType.DisplayMember = "Name"
        CmbItemConType.ValueMember = "Code"
        CmbItemConType.SelectedValue = "N"
        ''
        cboDocType.SelectedValue = "RGP"
        ''For Custom Fields
        If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
            UcCustomFields1.SetDefaultValues()
        End If
        txtVendorNo.Enabled = True
        ''End of For Custom Fields
        UcAttachment1.BlankAllControls()

    End Sub


    Function AllowToSave() As Boolean
        Try
            '===================Added by preeti Gupta==============
            If AllowFutureDateTransaction(txtDate.Value, Nothing) = False Then
                txtDate.Select()
                Return False
            End If
            '===========================================================
            Dim MainLoc As String = ""
            Dim SRNBillLoc As String = ""

            If btnSave.Text = "Update" Then
                Dim strchk As String = "select Status from TSPL_Milk_RGP_HEAD where RGP_No='" + txtDocNo.Value + "'"
                Dim chkpost As String = clsDBFuncationality.getSingleValue(strchk)
                If chkpost = "1" Then
                    Throw New Exception("Transaction already posted")
                    Return False
                End If
            End If

            UpdateAllTotals()

            If clsCommon.myLen(txtVendorNo.Value) <= 0 AndAlso chkthirdparty.Checked AndAlso (clsCommon.CompairString(cboDocType.SelectedValue, "NRGP") = CompairStringResult.Equal Or clsCommon.CompairString(cboDocType.SelectedValue, "NRGPR") = CompairStringResult.Equal) Then

            Else

                txtVendorNo.Focus()

                If clsCommon.myLen(txtVendorNo.Value) <= 0 Then
                    Throw New Exception("Please Enter Vendor.")
                    Return False
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
            '' Anubhooti 09-Oct-2014 BM00000003663
            ''

            '=============================================

            Dim arrPONo As New List(Of String)
            Dim arrSchNo As New List(Of String)
            Dim arrBOMIcode As List(Of String) = Nothing
            arrBOMIcode = New List(Of String)

            '================================================

            Dim strICode As String = ""
            Dim strIName As String = ""
            Dim dblQty As Double = 0
            Dim strUOM As String = ""
            Dim strLocCode As String = ""
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
                strLocCode = clsCommon.myCstr(gv1.Rows(ii).Cells(ColLocationSub).Value)


                If clsCommon.myLen(strICode) > 0 Then
                    If clsCommon.myLen(strUOM) <= 0 Then
                        Throw New Exception("Please enter UOM of Item - " + strICode + ".At Line No: " + clsCommon.myCstr(clsCommon.myCdbl(ii + 1)) + " ")
                    End If
                    ''For RM Other balance Qty check And works only for one unit.
                    Dim dblOuterConvFac As Double = clsItemMaster.GetConvertionFactor(strICode, strUOM, Nothing)
                    Dim loc_type As Integer = 0
                    If clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(ii).Cells(ColLocationType).Value), "1") = CompairStringResult.Equal Then
                        loc_type = 2
                    ElseIf clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(ii).Cells(ColLocationType).Value), "2") = CompairStringResult.Equal Then
                        loc_type = 1
                    ElseIf clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(ii).Cells(ColLocationType).Value), "3") = CompairStringResult.Equal Then
                        loc_type = 0
                    End If
                    Dim dt As DataTable = XpertERPEngine.clsProcessProductionPlanning.GetMilkAndALLItemStockBalance_With_FATSNFKG(strICode, txtLocation.Value, strLocCode, txtDate.Text, Nothing, strUOM, loc_type) 'clsDBFuncationality.GetDataTable(qry)'clsDBFuncationality.GetDataTable(qry)
                    Dim dblBalQty As Double = 0 'clsItemLocationDetails.getBalanceWithUnapproveForRMOther(strICode, txtLocation.Value, txtDocNo.Value, txtDate.Value, Nothing, strUOM)
                    If dt IsNot Nothing Then
                        dblBalQty = clsCommon.myCdbl(dt.Rows(0)("qty"))
                    End If
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
                    If dblEnteredQty > dblBalQty Then
                        Throw New Exception("Item - " + strICode + Environment.NewLine + "Entered Quantity - " + clsCommon.myCstr(dblEnteredQty) + " and Balance Quantity - " + clsCommon.myCstr(dblBalQty))
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

                    If clsCommon.myLen(strICode) > 0 Then
                        If dblQty <= 0 Then
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
                Dim Qty As Double = clsCommon.myCdbl(gv1.Rows(ii).Cells(colQty).Value)
                Dim strItemName As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colIName).Value)

            Next

            '------------------------------------------------
            If chkthirdparty.Checked AndAlso clsCommon.myLen(txtsrnlocation_code.Value) <= 0 AndAlso cboDocType.SelectedIndex = 0 Then
                clsCommon.MyMessageBoxShow("Please Select Location For SRN Entry", Me.Text)
                txtsrnlocation_code.Focus()
                txtsrnlocation_code.Select()
                Return False
            End If

            UcCustomFields1.AllowToSave()
            UcAttachment1.AllowToSave()

            Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
            Return False
        End Try
    End Function
    Private Sub btnUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        Try
            If clsCommon.myLen(txtDocNo.Value) > 0 Then
                Dim obj As New clsMilkRGPHead
                obj.RGP_No = clsCommon.myCstr(txtDocNo.Value)
                obj.GRNo = txtGRNo.Text
                obj.Road_Permit_No = txtRoadPermitNo.Text
                If txtGRDate.Checked Then
                    obj.GR_Date = txtGRDate.Value
                End If
                If txtRoadPermitDate.Checked Then
                    obj.RoadPermit_Date = txtRoadPermitDate.Value
                End If
                If clsMilkRGPHead.UpdateAfterPosting(obj, Nothing) Then
                    clsCommon.MyMessageBoxShow("Information updated successfully.")
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
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
                Dim obj As New clsMilkRGPHead()

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
                ''richa Ticket No BM00000003061 on 01/08/2014

                '-------------------------------------------
                obj.Mode_Of_Transport = txtModeOfTransport.Text
                obj.Cash_Memo_Detail = txtCashMemoDetail.Text
                obj.Vendor_Code = txtVendorNo.Value
                obj.Vendor_Name = lblVendorName.Text
                obj.VehicleNo = txtVehicleNo.Text
                obj.GPNo = txtGPNo.Text
                obj.GPDate = txtGPDate.Value
                obj.Remarks = txtRemarks.Text
                obj.Reason = txtReason.Text

                obj.Document_Amount = clsCommon.myCdbl(lblDocumentAmt.Text)
                obj.Location = txtLocation.Value
                obj.Delivered_By = txtDeliveredBy.Value
                obj.Department = txtDepartment.Value

                obj.CostCentre = fndCostCentre.Value
                obj.CostCentreDesc = txtCostCentre.Text
                '' Anubhooti 09-Oct-2014 BM00000003663
                ''
                '' Anubhooti 10-Dec-2014 BM00000003662
                obj.Item_Conversion_Type = clsCommon.myCstr(CmbItemConType.SelectedValue)
                ''

                obj.PO_Id = clsCommon.myCstr(txtPoNo.Value)
                obj.Against_As_It_Is = 0 'IIf(clsCommon.myCstr(txtAsitis.Text) = "Y", 1, 0)
                RefreshSerialNo()

                obj.Arr = New List(Of clsMilkRGPDetail)
                For Each grow As GridViewRowInfo In gv1.Rows
                    Dim objTr As New clsMilkRGPDetail()
                    objTr.Line_No = clsCommon.myCdbl(grow.Cells(colLineNo).Value)
                    objTr.Item_Code = clsCommon.myCstr(grow.Cells(colICode).Value)
                    objTr.Item_Desc = clsCommon.myCstr(grow.Cells(colIName).Value)
                    If clsCommon.CompairString(cboDocType.SelectedValue, "NRGPR") = CompairStringResult.Equal Then
                        objTr.RGP_Qty = clsCommon.myCdbl(grow.Cells(colRetQty).Value)
                    Else
                        objTr.RGP_Qty = clsCommon.myCdbl(grow.Cells(colQty).Value)
                    End If
                    objTr.Unit_code = clsCommon.myCstr(grow.Cells(colUnit).Value)
                    objTr.Item_Cost = clsCommon.myCdbl(grow.Cells(colApproxCost).Value) 'clsCommon.myCdbl(grow.Cells(colRate).Value)
                    objTr.Amount = clsCommon.myCdbl(grow.Cells(colAmt).Value)
                    objTr.FAT_pers = clsCommon.myCdbl(grow.Cells(ColFATRATE).Value)
                    objTr.FAT_Cost = clsCommon.myCdbl(grow.Cells(ColFATCost).Value)
                    objTr.SNF_Cost = clsCommon.myCdbl(grow.Cells(ColSNFCost).Value)
                    objTr.Bulk_Milk_Srn_No = ""
                    objTr.FAT_KG = clsCommon.myCdbl(grow.Cells(ColFATKG).Value)
                    objTr.FAT_Price = clsCommon.myCdbl(grow.Cells(ColFATPrice).Value)
                    objTr.SNF_Pers = clsCommon.myCdbl(grow.Cells(ColSNFRATE).Value)
                    objTr.SNF_KG = clsCommon.myCdbl(grow.Cells(ColSNFKG).Value)
                    objTr.SNF_Price = clsCommon.myCdbl(grow.Cells(ColSNFPrice).Value)
                    objTr.TanKer_No = clsCommon.myCstr(grow.Cells(ColTankerNo).Value)
                    objTr.Location_Code = clsCommon.myCstr(grow.Cells(ColLocationSub).Value)
                    objTr.Location_Type = clsCommon.myCstr(grow.Cells(ColLocationType).Value)
                    objTr.Specification = clsCommon.myCstr(grow.Cells(colSp).Value)
                    '' Anubhooti 06-Feb-2015(Unit Cost)
                    objTr.Approx_Cost = clsCommon.myCdbl(grow.Cells(colApproxCost).Value)
                    If clsCommon.myLen(objTr.Item_Code) > 0 Then
                        obj.Arr.Add(objTr)
                    End If
                Next

                obj.ArrQC = New List(Of clsMilkRGPIssueQCDetail)
                For Each grow As GridViewRowInfo In gv_qc.Rows
                    Dim objtr As New clsMilkRGPIssueQCDetail()

                    objtr.sno = CInt(grow.Cells(colQCsno).Value)
                    objtr.frm_loc_code = clsCommon.myCstr(grow.Cells(colQCloc_code).Value)
                    objtr.to_loc_code = clsCommon.myCstr(grow.Cells(colQCToloc_code).Value)
                    objtr.itemcode = clsCommon.myCstr(grow.Cells(colQCitemcode).Value)
                    objtr.param_code = clsCommon.myCstr(grow.Cells(colQCparamcode).Value)
                    objtr.lrange = clsCommon.myCdbl(grow.Cells(colQCrange1).Value)
                    objtr.urange = clsCommon.myCdbl(grow.Cells(colQCrange2).Value)
                    objtr.value1 = clsCommon.myCstr(grow.Cells(colQCvalue1).Value)
                    objtr.value2 = clsCommon.myCstr(grow.Cells(colQCvalue2).Value)
                    objtr.status_grid = clsCommon.myCstr(grow.Cells(colQCstatus).Value)
                    objtr.QCRange = clsCommon.myCdbl(grow.Cells(colQCRange).Value)
                    objtr.QCStatus = clsCommon.myCstr(grow.Cells(colQCOutStatus).Value)
                    objtr.QCValue = clsCommon.myCstr(grow.Cells(colQCValue).Value)

                    If objtr.status_grid = "None" Then
                        objtr.status_grid = ""
                    End If
                    If objtr.QCStatus = "None" Then
                        objtr.QCStatus = ""
                    End If

                    objtr.remarks = clsCommon.myCstr(grow.Cells(colQCremarks).Value).Replace("'", "`")

                    If clsCommon.myLen(objtr.param_code) > 0 Then
                        obj.ArrQC.Add(objtr)
                    End If
                Next

                If (obj.Arr Is Nothing OrElse obj.Arr.Count <= 0) Then
                    common.clsCommon.MyMessageBoxShow("Please Fill at list one Item")
                    Return
                End If

                '=======================================================

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
                        common.clsCommon.MyMessageBoxShow("Data Saved Successfully")
                    End If

                    LoadData(obj.RGP_No, NavigatorType.Current, False)
                End If
            End If
        Catch ex As Exception
            If Not ChekBtnPost Then
                common.clsCommon.MyMessageBoxShow(ex.Message)
            Else
                Throw New Exception(ex.Message)
            End If

        End Try
    End Sub

    Function AutoSRNFromRGP() As Boolean 'By Monika
        Dim obj As New clsSRNHead()
        Dim objRGP As New clsMilkRGPHead()
        Try
            objRGP = clsMilkRGPHead.GetData(txtDocNo.Value, NavigatorType.Current)

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
                    For Each objRGPDet As clsMilkRGPDetail In objRGP.Arr
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
            Return False
        Finally
            obj = Nothing
            objRGP = Nothing
        End Try
        Return True
    End Function

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType, ByVal IsRefDoc As Boolean)

        Try
            isInsideLoadData = False
            btnSave.Enabled = True
            btnSave.Text = "Save"
            btnPost.Enabled = False
            btnDelete.Enabled = False
            isNewEntry = True
            txtPoNo.Enabled = False
            BlankAllControls()
            LoadBlankGrid()
            LoadQCBlankGrid()
            ' btnSameasAbove.Enabled = True

            'gv_PO.Rows.Clear()
            'gv_PO.Rows.AddNew()
            'RadPageViewPage3.Item.Visibility = ElementVisibility.Collapsed  'RadGroupBox3.Visible = False
            RadPageView2.SelectedPage = RadPageViewPage2

            Dim obj As New clsMilkRGPHead()
            obj = clsMilkRGPHead.GetData(strCode, NavTyep)
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
                txtGRNo.Text = obj.GRNo
                txtRoadPermitNo.Text = obj.Road_Permit_No

                txtModeOfTransport.Text = obj.Mode_Of_Transport
                txtCashMemoDetail.Text = obj.Cash_Memo_Detail
                txtVendorNo.Value = obj.Vendor_Code
                lblVendorName.Text = obj.Vendor_Name

                txtReason.Text = obj.Reason
                txtRemarks.Text = obj.Remarks
                cboDocType.Enabled = False
                txtLocation.Enabled = False
                lblDocumentAmt.Text = clsCommon.myFormat(obj.Document_Amount)
                txtVehicleNo.Text = obj.VehicleNo
                txtGPNo.Text = obj.GPNo
                If clsCommon.myLen(obj.GPDate) > 0 Then
                    txtGPDate.Value = obj.GPDate
                End If

                txtDeliveredBy.Value = obj.Delivered_By
                lblDeliveredBy.Text = obj.Delivered_ByName
                txtDepartment.Value = obj.Department
                fndCostCentre.Value = obj.CostCentre
                txtCostCentre.Text = obj.CostCentreDesc

                '' Anubhooti 09-Oct-2014 BM00000003663
                ''
                '' Anubhooti 10-Dec-2014 BM00000003662
                'If clsCommon.CompairString(obj.Doc_Type, "RGP") = CompairStringResult.Equal Then
                '    LblItemConType.Visible = True
                '    CmbItemConType.Visible = True
                '    CmbItemConType.SelectedValue = obj.Item_Conversion_Type
                'Else
                LblItemConType.Visible = False
                CmbItemConType.Visible = False
                'End If
                ''
                txtVendorNo.Value = obj.Vendor_Code
                lblVendorName.Text = obj.Vendor_Name

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
                lblDepartment.Text = clsDBFuncationality.getSingleValue("select Description from TSPL_GL_SEGMENT_CODE where Seg_No  ='3' and Segment_code='" + txtDepartment.Value + "'")
                If Not IsRefDoc Then
                    UsLock1.Status = obj.Status
                    txtDocNo.Value = obj.RGP_No
                    txtDate.Value = obj.RGP_Date
                    cboDocType.SelectedValue = obj.Doc_Type
                    btnSave.Text = "Update"
                Else
                    isNewEntry = True
                    btnSave.Text = "Save"
                End If

                '' Anubhooti 
                If clsCommon.CompairString(obj.Doc_Type, "NRGP") = CompairStringResult.Equal Then
                    txtVendorNo.Enabled = False
                Else
                    txtVendorNo.Enabled = True
                End If
                ''
                lblMilkTransferIn.Text = obj.Milk_Transfer_In
                txtPoNo.Value = obj.PO_Id
                ' txtAsitis.Text = "" 'IIf(obj.Against_As_It_Is = 1, "Y", "")


                If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                    For Each objTr As clsMilkRGPDetail In obj.Arr
                        gv1.Rows.AddNew()
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = objTr.Line_No
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value = objTr.Item_Code
                        '--------------------------------------------------
                        Try
                            If chkthirdparty.Checked Then
                                cboItemType.SelectedValue = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select item_type from tspl_item_master where item_code='" + objTr.Item_Code + "'"))
                            End If
                        Catch exx As Exception
                            cboItemType.SelectedValue = ""
                        End Try
                        '----------------------------------------------------
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colIName).Value = objTr.Item_Desc
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colQCHSNcode).Value = objTr.HSN_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value = IIf(clsCommon.CompairString(cboDocType.SelectedValue, "NRGPR") = CompairStringResult.Equal And Not IsRefDoc, objTr.NRGP_Qty, objTr.RGP_Qty)
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colBalQty).Value = objTr.Balance_Qty
                        '  gv1.Rows(gv1.Rows.Count - 1).Cells(colRetQty).Value = IIf(clsCommon.CompairString(cboDocType.SelectedValue, "NRGPR") = CompairStringResult.Equal And Not IsRefDoc, objTr.RGP_Qty, 0)
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colUnit).Value = objTr.Unit_code
                        ' gv1.Rows(gv1.Rows.Count - 1).Cells(colRate).Value = objTr.Item_Cost
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colAmt).Value = objTr.Amount
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColTankerNo).Value = objTr.TanKer_No
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColFATRATE).Value = objTr.FAT_pers
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColFATKG).Value = objTr.FAT_KG
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColFATPrice).Value = objTr.FAT_Price
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColFATCost).Value = objTr.FAT_Cost
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColSNFCost).Value = objTr.SNF_Cost
                        LblSRNNO.Text = objTr.Bulk_Milk_Srn_No
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColSNFRATE).Value = objTr.SNF_Pers
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColSNFKG).Value = objTr.SNF_KG
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColSNFPrice).Value = objTr.SNF_Price

                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColLocationSub).Value = objTr.Location_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColLocationType).Value = objTr.Location_Type
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColLocationSubName).Value = clsLocation.GetName(objTr.Location_Code, Nothing)

                        gv1.Rows(gv1.Rows.Count - 1).Cells(colSp).Value = objTr.Specification

                        '' Anubhooti 06-Feb-2015 (Approx_Cost newly added)
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colApproxCost).Value = objTr.Approx_Cost
                        ''''''''''''''''''' Anubhooti 09-Oct-2014
                        Dim loc_type As Integer = 0
                        If clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(gv1.Rows.Count - 1).Cells(ColLocationType).Value), "1") = CompairStringResult.Equal Then
                            loc_type = 2
                        ElseIf clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(gv1.Rows.Count - 1).Cells(ColLocationType).Value), "2") = CompairStringResult.Equal Then
                            loc_type = 1
                        ElseIf clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(gv1.Rows.Count - 1).Cells(ColLocationType).Value), "3") = CompairStringResult.Equal Then
                            loc_type = 0
                        End If
                        'FillAvail_Stock(gv1.Rows.Count - 1, objTr.Item_Code, txtLocation.Value, objTr.Location_Sub_Code, "MI", objTr.Unit_code, loc_type)
                    Next

                    If obj.ArrQC IsNot Nothing AndAlso obj.ArrQC.Count > 0 Then
                        For Each objtr As clsMilkRGPIssueQCDetail In obj.ArrQC
                            gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCsno).Value = objtr.sno
                            gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCloc_code).Value = objtr.frm_loc_code
                            gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCloc_name).Value = objtr.frm_loc_desc
                            gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCToloc_code).Value = objtr.to_loc_code
                            gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCToloc_name).Value = objtr.to_loc_desc
                            gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCitemcode).Value = objtr.itemcode
                            gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCiname).Value = objtr.itemname
                            gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCparamcode).Value = objtr.param_code
                            gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCparam_desc).Value = objtr.param_desc
                            gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCparam_type).Value = objtr.param_type
                            gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCparam_nature).Value = objtr.param_nature
                            gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCrange1).Value = objtr.lrange
                            gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCrange2).Value = objtr.urange
                            gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCstatus).Value = objtr.status_grid
                            gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCvalue1).Value = objtr.value1
                            gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCvalue2).Value = objtr.value2
                            gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCremarks).Value = objtr.remarks
                            gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCRange).Value = objtr.QCRange
                            gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCOutStatus).Value = objtr.QCStatus
                            gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCValue).Value = objtr.QCValue

                            If clsCommon.myCdbl(gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCrange1).Value) > 0 AndAlso (clsCommon.myLen(gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCstatus).Value) <= 0 OrElse clsCommon.CompairString(clsCommon.myCstr(gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCstatus).Value), "None") = CompairStringResult.Equal) AndAlso clsCommon.myLen(gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCvalue1).Value) <= 0 Then
                                gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCValue).ReadOnly = True
                                gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCOutStatus).ReadOnly = True
                            End If
                            If clsCommon.myLen(gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCstatus).Value) > 0 AndAlso clsCommon.myLen(gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCvalue1).Value) <= 0 AndAlso clsCommon.myCdbl(gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCrange1).Value) <= 0 Then
                                gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCRange).ReadOnly = True
                                gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCValue).ReadOnly = True
                            End If
                            If clsCommon.myLen(gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCvalue1).Value) > 0 AndAlso (clsCommon.myLen(gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCstatus).Value) <= 0 OrElse clsCommon.CompairString(clsCommon.myCstr(gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCstatus).Value), "None") = CompairStringResult.Equal) AndAlso clsCommon.myCdbl(gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCrange1).Value) <= 0 Then
                                gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCRange).ReadOnly = True
                                gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCOutStatus).ReadOnly = True
                            End If
                            gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCHistort).Value = "Double Click"

                            gv_qc.Rows.AddNew()
                        Next
                    End If
                    'If obj.Status = ERPTransactionStatus.Pending Then
                    '    gv1.Rows.AddNew()
                    '    gv_PO.Rows.AddNew()

                    '    gv1.CurrentRow = gv1.Rows(0)
                    '    gv_PO.CurrentRow = gv_PO.Rows(0)
                    'End If
                End If
                '' Anubhooti 09-Oct-2014
                ''For Custom Fields
                If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
                    UcCustomFields1.LoadData(obj.RGP_No)
                End If
                clsCustomFieldGrid.FillDataInGrid(obj.RGP_No, MyBase.Form_ID, gv1)
                ''End of For Custom Fields
                UcAttachment1.LoadData(obj.RGP_No)

                RadGroupBox1.Enabled = False
                cboItemType.Enabled = False

                txtPoNo.Enabled = False

                'btnSameasAbove.Enabled = False

                If IsRGPAfterPO Then
                    'RadGroupBox3.Visible = True
                    'RadPageViewPage3.Item.Visibility = ElementVisibility.Visible
                    RadPageView2.SelectedPage = RadPageViewPage2
                Else
                    'RadGroupBox3.Visible = False
                    ' RadPageViewPage3.Item.Visibility = ElementVisibility.Collapsed
                    RadPageView2.SelectedPage = RadPageViewPage2
                End If


            Else
                AddNew()
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
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
                    Dim loc_type As Integer = 0
                    If clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(ColLocationType).Value), "1") = CompairStringResult.Equal Then
                        loc_type = 2
                    ElseIf clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(ColLocationType).Value), "2") = CompairStringResult.Equal Then
                        loc_type = 1
                    ElseIf clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(ColLocationType).Value), "3") = CompairStringResult.Equal Then
                        loc_type = 0
                    End If
                    ' FillAvail_Stock(CInt(gv1.CurrentCell.RowIndex), clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value), clsCommon.myCstr(txtLocation.Value), clsCommon.myCstr(gv1.CurrentRow.Cells(ColLocationSub).Value), "MI", clsCommon.myCstr(gv1.CurrentRow.Cells(colUnit).Value), loc_type)
                    If AutoSRNFromRGP() Then
                        clsCommon.MyMessageBoxShow("Auto SRN Transfer Successfully,For Post GoTo Store Received Note", Me.Text)
                    Else
                        Dim qry As String = ""
                        clsDBFuncationality.ExecuteNonQuery(qry)
                        Return
                    End If
                End If
                '-------------------------------------------------------------
                If PostAllowToSave() Then
                    If (clsMilkRGPHead.PostData(txtDocNo.Value)) Then
                        common.clsCommon.MyMessageBoxShow("Successfully Posted")

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
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
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
                If (clsMilkRGPHead.DeleteData(txtDocNo.Value)) Then
                    saveCancelLog(Reason, "Delete", Nothing)
                    common.clsCommon.MyMessageBoxShow("Data Deleted Successfully ")
                    AddNew()
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
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
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub txtDocNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtDocNo._MYValidating
        Dim qry As String = "select RGP_No as Code,RGP_Date as Date,TSPL_Milk_RGP_HEAD.Vendor_Code as [Vendor Code], TSPL_Milk_RGP_HEAD.Vendor_Name as Vendor,ISNULL(TSPL_VENDOR_MASTER.alies_name,'') As [Alies Name],Document_Amount as Amount,case when TSPL_Milk_RGP_HEAD.Status='0' then 'Pending' else 'Approved' end as [Status] from TSPL_Milk_RGP_HEAD"
        '' Anubhooti 12-Mar-2015 (Fetch Alies Name On Vendor Finder)
        qry += " LEFT OUTER JOIN TSPL_VENDOR_MASTER ON TSPL_VENDOR_MASTER.Vendor_Code = TSPL_Milk_RGP_HEAD.Vendor_Code "
        Dim whrClas As String = ""
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            whrClas = " Location in (" + objCommonVar.strCurrUserLocations + ")"
        End If

        LoadData(clsCommon.ShowSelectForm("RGPFNDR", qry, "Code", whrClas, txtDocNo.Value, "Code", isButtonClicked), NavigatorType.Current, False)
    End Sub

    Private Sub FrmAPInvoiceEntry_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.F2 AndAlso gv1.CurrentCell IsNot Nothing Then
            isCellValueChangedOpen = True
            If gv1.CurrentColumn Is gv1.Columns(colICode) Then
                gv1.CurrentColumn = gv1.Columns(colIName)

                '-----------------check when thrd party location on----------------------------------------
                If chkthirdparty.Checked AndAlso clsCommon.myLen(cboItemType.SelectedValue) <= 0 Then
                    clsCommon.MyMessageBoxShow("Please Select Item Type First", Me.Text)
                    Return
                End If
                '--------------------------------------------------------------------------------------------

                OpenICodeList(True)
                gv1.CurrentColumn = gv1.Columns(colICode)
            End If
            If gv1.CurrentColumn Is gv1.Columns(colUnit) Then
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
            Dim frm As New FrmPWD(Nothing)
            frm.strType = "SIRC"
            frm.strCode = "SIReversAndCreate"
            frm.ShowDialog()
            If frm.isPasswordCorrect Then
                btnReverse.Visible = True
            End If
        End If
        If e.KeyCode = Keys.F2 AndAlso gv_qc.CurrentColumn IsNot Nothing AndAlso gv_qc.CurrentColumn Is gv_qc.Columns(colQCRange) Then
            isCellValueChangedOpen = True
            Dim Icode As String = clsCommon.myCstr(gv_qc.CurrentRow.Cells(colQCitemcode).Value)
            Dim gvIcode As String = ""

            For Each grow As GridViewRowInfo In gv1.Rows
                gvIcode = clsCommon.myCstr(grow.Cells(colICode).Value)

                If clsCommon.CompairString(gvIcode, Icode) = CompairStringResult.Equal Then
                    If clsCommon.CompairString(clsCommon.myCstr(gv_qc.CurrentRow.Cells(colQCparam_type).Value), "FAT") = CompairStringResult.Equal Then
                        grow.Cells(ColFATRATE).Value = Math.Round(clsCommon.myCdbl(gv_qc.CurrentRow.Cells(colQCRange).Value), 2)
                        grow.Cells(ColFATKG).Value = clsBOM.GetFatSNFKG_AfterConversion(gvIcode, clsCommon.myCstr(grow.Cells(colUnit).Value), clsCommon.myCdbl(grow.Cells(colQty).Value), clsCommon.myCdbl(grow.Cells(ColFATRATE).Value), Nothing) ' Math.Round((clsCommon.myCdbl(grow.Cells(colqty).Value) * clsCommon.myCdbl(grow.Cells(colfatpers).Value)) / 100, DecimalPoint)
                    End If
                    If clsCommon.CompairString(clsCommon.myCstr(gv_qc.CurrentRow.Cells(colQCparam_type).Value), "SNF") = CompairStringResult.Equal Then
                        grow.Cells(ColSNFRATE).Value = Math.Round(clsCommon.myCdbl(gv_qc.CurrentRow.Cells(colQCRange).Value), 2)
                        grow.Cells(ColSNFKG).Value = clsBOM.GetFatSNFKG_AfterConversion(gvIcode, clsCommon.myCstr(grow.Cells(colUnit).Value), clsCommon.myCdbl(grow.Cells(colQty).Value), clsCommon.myCdbl(grow.Cells(ColSNFRATE).Value), Nothing) ' Math.Round((clsCommon.myCdbl(grow.Cells(colqty).Value) * clsCommon.myCdbl(grow.Cells(colfatpers).Value)) / 100, DecimalPoint)
                    End If
                End If
            Next
            isCellValueChangedOpen = False
        End If
    End Sub

    Private Sub txtVendorNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtVendorNo._MYValidating
        Dim qry As String = "select Vendor_Code as [Code],Vendor_Name  as [Name] from TSPL_Vendor_MASTER "
        txtVendorNo.Value = clsCommon.ShowSelectForm("RGPCustFNDer", qry, "Code", "status='N'", txtVendorNo.Value, "Code", isButtonClicked)
        lblVendorName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Vendor_Name from TSPL_Vendor_MASTER where Vendor_Code ='" + txtVendorNo.Value + "'"))
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
        Dim qry As String = "Select Location_Code As [Code],ISNULL(Location_Desc,'') As [Location Desc]  From TSPL_LOCATION_MASTER  "
        Dim WhrCls As String = " Rejected_Type='N' AND Location_Type='Physical'  "
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
        Else
            lblLocation.Text = ""
        End If
        LoadBlankGrid()
        LoadQCBlankGrid()
        gv_qc.Rows.Clear()
        gv_qc.Rows.AddNew()
        gv1.Rows.Clear()
        gv1.Rows.AddNew()
    End Sub

    Private Sub btnprint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnprint.Click
        'If clsCommon.CompairString(cboDocType.SelectedValue, "NRGP") = CompairStringResult.Equal AndAlso chkthirdparty.Checked Then
        '    printWith3rdParty()
        'Else
        '    print()
        'End If
        FunRGPChallanPrint()
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
            Dim strqry As String = "SELECT TSPL_LOCATION_MASTER.Tin_No as location_tin_no, TSPL_LOCATION_MASTER.add1 +case when len(TSPL_LOCATION_MASTER.add2)>0 then ', '+TSPL_LOCATION_MASTER.add2 else '' end +case when LEN(isnull(TSPL_LOCATION_MASTER.Add3,''))>0 then ', '+isnull(TSPL_LOCATION_MASTER.Add3,'') else ' ' end   as Location_address, TSPL_Milk_RGP_HEAD.Created_By,TSPL_Milk_RGP_HEAD.Modify_By, (select emp_name from TSPL_EMPLOYEE_MASTER where EMP_CODE =TSPL_Milk_RGP_HEAD .Delivered_By )as DeliverdBy ,TSPL_Milk_RGP_HEAD.RGP_No,TSPL_Milk_RGP_HEAD.VehicleNo, convert(varchar,TSPL_Milk_RGP_HEAD.RGP_Date,103) as RGP_Date , TSPL_Milk_RGP_HEAD.Doc_Type, TSPL_Milk_RGP_HEAD.Vendor_Code, " & _
                     " TSPL_Milk_RGP_HEAD.Vendor_Name, TSPL_Milk_RGP_HEAD.VehicleNo, TSPL_Milk_RGP_HEAD.GPNo, TSPL_Milk_RGP_HEAD.GPDate, TSPL_Milk_RGP_HEAD.Reason, " & _
                     " TSPL_Milk_RGP_HEAD.Remarks, TSPL_Milk_RGP_HEAD.Posting_Date, TSPL_Milk_RGP_HEAD.comp_code, TSPL_Milk_RGP_HEAD.Location, TSPL_Milk_RGP_HEAD.Mode_Of_Transport, TSPL_Milk_RGP_HEAD.Cash_Memo_Detail, " & _
                     " TSPL_Milk_RGP_HEAD.Document_Amount,TSPL_Milk_RGP_HEAD.Created_By, TSPL_COMPANY_MASTER.Comp_Name, tspl_company_Master.add1 +case when len(tspl_company_Master.add2)>0 then ', '+tspl_company_Master.add2 else '' end +case when LEN(isnull(tspl_company_Master.Add3,''))>0 then ', '+isnull(tspl_company_Master.Add3,'') else ' ' end   as Add1" & _
                     " , TSPL_COMPANY_MASTER.State, TSPL_COMPANY_MASTER.Tin_No, TSPL_COMPANY_MASTER.Logo_Img, " & _
                     " TSPL_COMPANY_MASTER.Logo_Img2, TSPL_Milk_RGP_Detail.Line_No, TSPL_Milk_RGP_Detail.Item_Code, TSPL_Milk_RGP_Detail.Item_Desc, " & _
                     " TSPL_Milk_RGP_Detail.RGP_Qty, TSPL_Milk_RGP_Detail.Unit_code, TSPL_Milk_RGP_Detail.Item_Cost, TSPL_Milk_RGP_Detail.Amount, TSPL_Milk_RGP_Detail.Specification ,TSPL_Milk_RGP_HEAD.InvoiceNo,TSPL_Milk_RGP_HEAD.PartyName,TSPL_Milk_RGP_HEAD.PartyAddress,convert (varchar,TSPL_Milk_RGP_HEAD.DispatchDate,103) as DispatchDate "


            strqry += " ,'" + type + "' as RGPType ,TSPL_GL_SEGMENT_CODE.Description as Department,case when  TSPL_Milk_RGP_HEAD.billing='Y' then 'Yes' when  TSPL_Milk_RGP_HEAD.billing='N' then 'No' else '' end as Billing "
            strqry += " FROM TSPL_Milk_RGP_HEAD INNER JOIN "
            strqry += " TSPL_Milk_RGP_Detail ON TSPL_Milk_RGP_HEAD.RGP_No = TSPL_Milk_RGP_Detail.RGP_No  "

            strqry += "   LEFT OUTER JOIN TSPL_COMPANY_MASTER ON TSPL_Milk_RGP_HEAD.comp_code = TSPL_COMPANY_MASTER.Comp_Code left outer join TSPL_LOCATION_MASTER on TSPL_Milk_RGP_HEAD.Location=TSPL_LOCATION_MASTER .Location_Code LEFT OUTER JOIN  TSPL_GL_SEGMENT_CODE on TSPL_Milk_RGP_HEAD.Department = TSPL_GL_SEGMENT_CODE.Segment_code  where   " & strDep & " TSPL_Milk_RGP_HEAD.RGP_No='" + txtDocNo.Value + "'   "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(strqry)
            Dim frmCRV As New frmCrystalReportViewer()
            frmCRV.funreport(CrystalReportFolder.PurchaseOrder, dt, "rptNRGP3rdParty", "NRGP Report")
            frmCRV = Nothing


        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Public Sub print()
        Try

            Dim type As String = cboDocType.Text
            Dim strDep As String = txtDepartment.Value
            If clsCommon.myLen(txtDepartment.Value) > 0 Then
                strDep = " Seg_No  ='3' and "
            Else
                strDep = ""
            End If
            Dim strqry As String = "SELECT TSPL_LOCATION_MASTER.Tin_No as location_tin_no, TSPL_LOCATION_MASTER.add1 +case when len(TSPL_LOCATION_MASTER.add2)>0 then ', '+TSPL_LOCATION_MASTER.add2 else '' end +case when LEN(isnull(TSPL_LOCATION_MASTER.Add3,''))>0 then ', '+isnull(TSPL_LOCATION_MASTER.Add3,'') else ' ' end   as Location_address, TSPL_Milk_RGP_HEAD.Created_By,TSPL_Milk_RGP_HEAD.Modify_By, (select emp_name from TSPL_EMPLOYEE_MASTER where EMP_CODE =TSPL_Milk_RGP_HEAD .Delivered_By )as DeliverdBy ,TSPL_Milk_RGP_HEAD.RGP_No,TSPL_Milk_RGP_HEAD.VehicleNo, convert(varchar,TSPL_Milk_RGP_HEAD.RGP_Date) as RGP_Date , TSPL_Milk_RGP_HEAD.Doc_Type, TSPL_Milk_RGP_HEAD.Vendor_Code, " & _
                     " TSPL_Milk_RGP_HEAD.Vendor_Name, TSPL_Milk_RGP_HEAD.VehicleNo, TSPL_Milk_RGP_HEAD.GPNo, TSPL_Milk_RGP_HEAD.GPDate, TSPL_Milk_RGP_HEAD.Reason, " & _
                     " TSPL_Milk_RGP_HEAD.Remarks, TSPL_Milk_RGP_HEAD.Posting_Date, TSPL_Milk_RGP_HEAD.comp_code, TSPL_Milk_RGP_HEAD.Location,TSPL_LOCATION_MASTER.Location_Desc , TSPL_Milk_RGP_HEAD.Mode_Of_Transport, TSPL_Milk_RGP_HEAD.Cash_Memo_Detail, " & _
                     " TSPL_Milk_RGP_HEAD.Document_Amount,TSPL_Milk_RGP_HEAD.Created_By, TSPL_COMPANY_MASTER.Comp_Name, tspl_company_Master.add1 +case when len(tspl_company_Master.add2)>0 then ', '+tspl_company_Master.add2 else '' end +case when LEN(isnull(tspl_company_Master.Add3,''))>0 then ', '+isnull(tspl_company_Master.Add3,'') else ' ' end   as Add1" & _
                     " , TSPL_COMPANY_MASTER.State, TSPL_COMPANY_MASTER.Tin_No, TSPL_COMPANY_MASTER.Logo_Img, " & _
                     " TSPL_COMPANY_MASTER.Logo_Img2, TSPL_Milk_RGP_Detail.Line_No, TSPL_Milk_RGP_Detail.Item_Code, TSPL_Milk_RGP_Detail.Item_Desc, " & _
                     " TSPL_Milk_RGP_Detail.RGP_Qty, TSPL_Milk_RGP_Detail.Unit_code, TSPL_Milk_RGP_Detail.Item_Cost, TSPL_Milk_RGP_Detail.Amount, TSPL_Milk_RGP_Detail.Specification "

            'If chkAgainst_Sale.Checked = False And chlCust.Checked = False Then
            '    strqry += ", TSPL_VENDOR_MASTER.Add1 AS venadd1, TSPL_VENDOR_MASTER.Add2 AS venadd2, TSPL_VENDOR_MASTER.Add3 AS venadd3,TSPL_VENDOR_MASTER.Tin_No as VenTINNO, " & _
            '         " TSPL_VENDOR_MASTER.City_Code_Desc as vencity,TSPL_VENDOR_MASTER.Lst_No,TSPL_VENDOR_MASTER.CST"
            'Else
            strqry += " , TSPL_CUSTOMER_MASTER.Customer_Name ,ISNULL(TSPL_CUSTOMER_MASTER.Phone1,'')+ Case When ISNULL(TSPL_CUSTOMER_MASTER.Phone2,'')<>'' Then ', '+ TSPL_CUSTOMER_MASTER.Phone2 Else'' End as Customer_Phone ,TSPL_CUSTOMER_MASTER.add1 +case when len(TSPL_CUSTOMER_MASTER.add2)>0 then ', '+TSPL_CUSTOMER_MASTER.add2 else '' end +case when LEN(isnull(TSPL_CUSTOMER_MASTER.Add3,''))>0 then ', '+isnull(TSPL_CUSTOMER_MASTER.Add3,'') else ' ' end   as Customer_address,TSPL_CUSTOMER_MASTER.Tin_No as customer_Tin_No,TSPL_CUSTOMER_MASTER.Remarks1  +case when len(TSPL_CUSTOMER_MASTER.Remarks2 )>0 then ', '+TSPL_CUSTOMER_MASTER.Remarks2 else ''  end   as Customer_Remarks,TSPL_CUSTOMER_MASTER.Add1 AS venadd1, TSPL_CUSTOMER_MASTER.Add2 AS venadd2, TSPL_CUSTOMER_MASTER.Add3 AS venadd3,TSPL_CUSTOMER_MASTER.Tin_No as VenTINNO,  TSPL_CITY_MASTER.City_Name as vencity,TSPL_CUSTOMER_MASTER.Lst_No "
            'End If
            strqry += " ,'" + type + "' as RGPType ,TSPL_GL_SEGMENT_CODE.Description as Department,case when  TSPL_Milk_RGP_HEAD.billing='Y' then 'Yes' when  TSPL_Milk_RGP_HEAD.billing='N' then 'No' else '' end as Billing,TSPL_Milk_RGP_Detail.Approx_Cost,TSPL_Milk_RGP_HEAD.Road_Permit_No, convert(date,TSPL_Milk_RGP_HEAD.RoadPermit_Date)as RoadPermit_Date  "
            strqry += " FROM TSPL_Milk_RGP_HEAD INNER JOIN "
            strqry += " TSPL_Milk_RGP_Detail ON TSPL_Milk_RGP_HEAD.RGP_No = TSPL_Milk_RGP_Detail.RGP_No  "
            'If chkAgainst_Sale.Checked = False And chlCust.Checked = False Then
            '    strqry += " LEFT OUTER JOIN TSPL_VENDOR_MASTER ON TSPL_Milk_RGP_HEAD.Vendor_Code = TSPL_VENDOR_MASTER.Vendor_Code  "
            'Else
            strqry += " LEFT OUTER JOIN tspl_customer_master ON TSPL_Milk_RGP_HEAD.Vendor_Code = tspl_customer_master.cust_code  "
            strqry += " left outer JOIN tspl_city_master ON tspl_customer_master.city_code = tspl_city_master.city_code  "
            ' End If
            strqry += "   LEFT OUTER JOIN TSPL_COMPANY_MASTER ON TSPL_Milk_RGP_HEAD.comp_code = TSPL_COMPANY_MASTER.Comp_Code left outer join TSPL_LOCATION_MASTER on TSPL_Milk_RGP_HEAD.Location=TSPL_LOCATION_MASTER .Location_Code LEFT OUTER JOIN  TSPL_GL_SEGMENT_CODE on TSPL_Milk_RGP_HEAD.Department = TSPL_GL_SEGMENT_CODE.Segment_code  where   " & strDep & " TSPL_Milk_RGP_HEAD.RGP_No='" + txtDocNo.Value + "'   "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(strqry)
            '--- Field Approx.Cost , RoadPermit and subreport Added by shivani---'
            Dim frmCRV As New frmCrystalReportViewer()
            If (type = "Returnable Gate Pass") Then
                ' PurchaseOrderViewer.funreport(dt, "rptRGPNew", "RGP Report")
                frmCRV.funsubreportWithdt(CrystalReportFolder.PurchaseOrder, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "rptRGPNew", "RGP Report", "rptCompanyAddress.rpt")
            Else
                'PurchaseOrderViewer.funreport(dt, "rptNRGP", "NRGP Report")
                frmCRV.funsubreportWithdt(CrystalReportFolder.PurchaseOrder, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "rptNRGP", "NRGP Report", "rptCompanyAddress.rpt")
            End If
            frmCRV = Nothing

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
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

    Private Sub cboDocType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.Data.PositionChangedEventArgs) Handles cboDocType.SelectedIndexChanged
        If Not IsformLoad Then
            HideUnhideColumn()
            gbRGPNRGP.Enabled = True
            If (cboDocType.SelectedValue = "NRGP") Then
                RadGroupBox1.Enabled = False
                txtVendorNo.Enabled = False
                If chkthirdparty.Checked Then
                    txtVendorNo.Enabled = False
                Else
                    txtVendorNo.Enabled = True
                End If
            ElseIf cboDocType.SelectedValue = "NRGPR" Then
                gbRGPNRGP.Enabled = False

                txtVendorNo.Value = ""
                lblVendorName.Text = ""
                txtLocation.Value = ""
                lblLocation.Text = ""
                txtVendorNo.Enabled = True
            Else
                txtVendorNo.Enabled = True
                RadLabel2.Text = "Vendor No"
                txtVendorNo.Value = ""
                lblVendorName.Text = ""
                txtLocation.Value = ""
                lblLocation.Text = ""
                txtVendorNo.Enabled = True
                If RadGroupBox1.Visible Then
                    RadGroupBox1.Enabled = True
                End If
            End If
        End If
    End Sub

    Private Sub HideUnhideColumn()
        gv1.Columns(colBalQty).IsVisible = False
        '   gv1.Columns(colRetQty).IsVisible = False
        If clsCommon.CompairString(cboDocType.SelectedValue, "RGP") = CompairStringResult.Equal Then
            ''richa Ticket No BM00000003061 on 01/08/2014

            '-------------------------------------------
            '' Anubhooti 10-Dec-2014 BM00000003662 (Item Conversion Type)

            CmbItemConType.Visible = False
            LblItemConType.Visible = False
            ''
        Else

            ''richa Ticket No BM00000003061 on 01/08/2014
            '-------------------------------------------
            '' Anubhooti 10-Dec-2014 BM00000003662 (Item Conversion Type)
            CmbItemConType.Visible = False
            LblItemConType.Visible = False
            ''
            If clsCommon.CompairString(cboDocType.SelectedValue, "NRGPR") = CompairStringResult.Equal Then
                gv1.Columns(colBalQty).IsVisible = True
                ' gv1.Columns(colRetQty).IsVisible = True
            End If
        End If

        txtPoNo.Enabled = False

        If IsRGPAfterPO Then
            txtPoNo.Enabled = True
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


    Private Sub ControlNonInventory(ByVal Ischecked As Boolean)
        If Not isInsideLoadData Then
            If Ischecked Then
                LoadBlankGrid()
                LoadQCBlankGrid()
                gv1.Rows.AddNew()
            End If
        End If
    End Sub

    Private Sub fndCostCentre__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndCostCentre._MYValidating
        Dim Qry As String = "select Segment_code as [Code], Description,Segment_name as [Segment Name] From TSPL_GL_SEGMENT_CODE  "
        Dim WhrCls As String = " seg_no <>'7'  "
        fndCostCentre.Value = clsCommon.ShowSelectForm("Vehicle Selector", Qry, "Code", WhrCls, fndCostCentre.Value, "", isButtonClicked)
        txtCostCentre.Text = clsDBFuncationality.getSingleValue("select Description From TSPL_GL_SEGMENT_CODE Where   Segment_Code= '" + fndCostCentre.Value + "'")
    End Sub

    Private Sub chlCust_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        txtVendorNo.Value = ""
        lblVendorName.Text = ""
        RadLabel2.Text = "Customer No"
    End Sub

    Private Sub btnReverse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReverse.Click
        Try
            If common.clsCommon.MyMessageBoxShow("Reverse and Unpost the Current Document" + Environment.NewLine + "Are you sure", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                If clsMilkRGPHead.ReverseAndUnpost(txtDocNo.Value) Then
                    common.clsCommon.MyMessageBoxShow("Successfully Reversed and Recreated", Me.Text)
                    LoadData(txtDocNo.Value, NavigatorType.Current, False)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Sub setBalance()
        UcItemBalance1.ItemCode = clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value)
        UcItemBalance1.ItemName = clsCommon.myCstr(gv1.CurrentRow.Cells(colIName).Value)
        'UcItemBalance1.ItemMRP = clsCommon.myCdbl(gv1.CurrentRow.Cells(colMRP).Value)
        UcItemBalance1.LocationCode = clsCommon.myCstr(gv1.CurrentRow.Cells(ColLocationSub).Value) 'txtLocation.Value
        UcItemBalance1.LocationName = clsCommon.myCstr(gv1.CurrentRow.Cells(ColLocationSubName).Value) 'lblLocation.Text
        UcItemBalance1.UOM = clsCommon.myCstr(gv1.CurrentRow.Cells(colUnit).Value)
        UcItemBalance1.TransNo = txtDocNo.Value
        UcItemBalance1.TransDate = txtDate.Value
        UcItemBalance1.ShowPOQty = False
        UcItemBalance1.RefreshData()
    End Sub

    Sub FillAvail_Stock(ByVal XR As Integer, ByVal Itemcode As String, ByVal Main_Loc_Code As String, ByVal Loc_Code As String, ByVal ProductType1 As String, ByVal UOM_CODE As String, ByVal is_sub_sec_main_location As Integer)
        gv1.Rows(XR).Cells(colBalQty).Value = Nothing
        'If clsCommon.CompairString(ProductType1, "Milk") = CompairStringResult.Equal Then
        '    qry = "select sum(qty) as qty,sum(fat_pers) as fat_pers,sum(fat_kg) as fat_kg,sum(snf_pers) as snf_pers,sum(snf_kg) as snf_kg from (select (qty*Conversion_Factor/finalconvrt) as qty,(Fat_Per*Conversion_Factor/finalconvrt) as fat_pers,(FAT_KG*Conversion_Factor/finalconvrt) as fat_kg,(SNF_Per*Conversion_Factor/finalconvrt) as snf_pers,(SNF_KG*Conversion_Factor/finalconvrt) as snf_kg from ( "
        '    qry += "select a.Location_Code,a.Item_Code,a.UOM,a.Conversion_Factor,SUM(a.qty) as qty,sum(a.Fat_Per) as Fat_Per,sum(a.FAT_KG) as FAT_KG,sum(a.SNF_Per) as SNF_Per,sum(a.SNF_KG) as SNF_KG,finalconversion.Conversion_Factor as finalconvrt from ( "
        '    qry += "select TSPL_INVENTORY_MOVEMENT_NEW.Location_Code,TSPL_INVENTORY_MOVEMENT_NEW.Main_Location,TSPL_INVENTORY_MOVEMENT_NEW.Item_Code,TSPL_INVENTORY_MOVEMENT_NEW.UOM,TSPL_INVENTORY_MOVEMENT_NEW.InOut,(case when TSPL_INVENTORY_MOVEMENT_NEW.InOut='I' then TSPL_INVENTORY_MOVEMENT_NEW.Qty else case when TSPL_INVENTORY_MOVEMENT_NEW.inout='O' then (0-TSPL_INVENTORY_MOVEMENT_NEW.Qty) end end) as QTY,(case when TSPL_INVENTORY_MOVEMENT_NEW.InOut='I' then TSPL_INVENTORY_MOVEMENT_NEW.Fat_KG else case when TSPL_INVENTORY_MOVEMENT_NEW.inout='O' then (0-TSPL_INVENTORY_MOVEMENT_NEW.Fat_KG) end end) as FAT_KG,(case when TSPL_INVENTORY_MOVEMENT_NEW.InOut='I' then TSPL_INVENTORY_MOVEMENT_NEW.Fat_Per else case when TSPL_INVENTORY_MOVEMENT_NEW.inout='O' then (0-TSPL_INVENTORY_MOVEMENT_NEW.Fat_Per) end end) as Fat_Per,(case when TSPL_INVENTORY_MOVEMENT_NEW.InOut='I' then TSPL_INVENTORY_MOVEMENT_NEW.SNF_KG else case when TSPL_INVENTORY_MOVEMENT_NEW.inout='O' then (0-TSPL_INVENTORY_MOVEMENT_NEW.SNF_KG) end end) as SNF_KG,(case when TSPL_INVENTORY_MOVEMENT_NEW.InOut='I' then TSPL_INVENTORY_MOVEMENT_NEW.SNF_Per else case when TSPL_INVENTORY_MOVEMENT_NEW.inout='O' then (0-TSPL_INVENTORY_MOVEMENT_NEW.SNF_Per) end end) as SNF_Per,TSPL_ITEM_UOM_DETAIL.Conversion_Factor "
        '    qry += "from TSPL_INVENTORY_MOVEMENT_NEW  left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_INVENTORY_MOVEMENT_NEW.Item_Code and TSPL_INVENTORY_MOVEMENT_NEW.UOM=TSPL_ITEM_UOM_DETAIL.UOM_Code "
        '    qry += "  )a left outer join TSPL_ITEM_UOM_DETAIL as finalconversion on finalconversion.Item_Code=a.Item_Code and finalconversion.UOM_Code='" + UOM_CODE + "' "
        '    qry += " where a.location_code='" + Loc_Code + "' and a.main_location='" + Main_Loc_Code + "' and a.item_code='" + Itemcode + "' "
        '    qry += " group by a.Location_Code,a.Item_Code,a.UOM,a.Conversion_Factor,finalconversion.conversion_factor)b )axa"
        Dim dt As DataTable = XpertERPEngine.clsProcessProductionPlanning.GetMilkAndALLItemStockBalance_With_FATSNFKG(Itemcode, Main_Loc_Code, Loc_Code, txtDate.Text, Nothing, UOM_CODE, is_sub_sec_main_location) 'clsDBFuncationality.GetDataTable(qry)'clsDBFuncationality.GetDataTable(qry)

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            gv1.Rows(XR).Cells(colBalQty).Value = Math.Round(clsCommon.myCdbl(dt.Rows(0)("qty")), DecimalPoint)

            Dim fractnvalue As Decimal = 0
            Dim index As Integer = 0

            If clsCommon.myCdbl(dt.Rows(0)("qty")) = 0 Then
                gv1.Rows(XR).Cells(ColFATRATE).Value = 0
            Else
                gv1.Rows(XR).Cells(ColFATRATE).Value = System.Math.Round((clsCommon.myCdbl(dt.Rows(0)("fat_kg")) / clsCommon.myCdbl(dt.Rows(0)("qty"))) * 100, 2) ' clsCommon.myCdbl(dt.Rows(0)("fat_pers"))

                fractnvalue = clsCommon.myCdbl(gv1.Rows(XR).Cells(ColFATRATE).Value)
                fractnvalue = System.Math.Round((fractnvalue - CInt(fractnvalue)) * 100, 2)
                Dim reminder As Decimal = 0 ' clsCommon.myCdbl(System.Math.Round(((CInt(fractnvalue) Mod 5) / 100), 2))

                gv1.Rows(XR).Cells(ColFATRATE).Value = clsCommon.myCdbl(gv1.Rows(XR).Cells(ColFATRATE).Value) - reminder
            End If

            If clsCommon.myCdbl(dt.Rows(0)("qty")) = 0 Then
                gv1.Rows(XR).Cells(ColSNFRATE).Value = 0
            Else
                gv1.Rows(XR).Cells(ColSNFRATE).Value = System.Math.Round((clsCommon.myCdbl(dt.Rows(0)("snf_kg")) / clsCommon.myCdbl(dt.Rows(0)("qty"))) * 100, 2) ' clsCommon.myCdbl(dt.Rows(0)("fat_pers"))

                fractnvalue = clsCommon.myCdbl(gv1.Rows(XR).Cells(ColSNFRATE).Value)
                fractnvalue = System.Math.Round((fractnvalue - CInt(fractnvalue)) * 100, 2)
                Dim actvalue As Decimal = 0 ' clsCommon.myCdbl(System.Math.Round((CInt(fractnvalue) Mod 5) / 100, 2))
                gv1.Rows(XR).Cells(ColSNFRATE).Value = clsCommon.myCdbl(gv1.Rows(XR).Cells(ColSNFRATE).Value) - actvalue
            End If
        End If

        dt = Nothing
        'Else
        'qry = "select sum(qty) as qty from (select (qty*Conversion_Factor/finalconvrt) as qty from ("
        'qry += "select a.Location_Code,a.Item_Code,a.UOM,a.Conversion_Factor,SUM(isnull(a.qty,0)) as qty,finalconversion.conversion_factor as finalconvrt from ( "
        'qry += "select TSPL_INVENTORY_MOVEMENT.Location_Code,TSPL_INVENTORY_MOVEMENT.Item_Code,TSPL_INVENTORY_MOVEMENT.UOM,TSPL_INVENTORY_MOVEMENT.InOut,(case when TSPL_INVENTORY_MOVEMENT.InOut='I' then TSPL_INVENTORY_MOVEMENT.Qty else case when TSPL_INVENTORY_MOVEMENT.inout='O' then (0-TSPL_INVENTORY_MOVEMENT.Qty) end end) as QTY,TSPL_ITEM_UOM_DETAIL.Conversion_Factor from TSPL_INVENTORY_MOVEMENT left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_INVENTORY_MOVEMENT.Item_Code and TSPL_INVENTORY_MOVEMENT.UOM=TSPL_ITEM_UOM_DETAIL.UOM_Code "
        'qry += " )a left outer join TSPL_ITEM_UOM_DETAIL as finalconversion on finalconversion.Item_Code=a.Item_Code and finalconversion.UOM_Code='" + UOM_CODE + "' "
        'qry += "where a.location_code='" + Loc_Code + "' and a.item_code='" + Itemcode + "' "
        'qry += "group by a.Location_Code,a.Item_Code,a.UOM,a.Conversion_Factor,finalconversion.conversion_factor)b )axa "
        'Dim str As Decimal = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))

        'If str > 0 Or str < 0 Then
        '    gv1.Rows(XR).Cells(colBalQty).Value = Math.Round(str, DecimalPoint)
        'End If
        'End If
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
                common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
            End If
            ''stuti regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub deleteLayout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles deleteLayout.Click
        clsGridLayout.DeleteData(MyBase.Form_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow("Layout Delete successfully", "Information")
    End Sub

    Private Sub chkthirdparty_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkthirdparty.ToggleStateChanged
        txtLocation.Value = ""
        lblLocation.Text = ""
        cboItemType.Enabled = False
        If chkthirdparty.Checked Then
            cboItemType.Enabled = True
            If clsCommon.CompairString(cboDocType.SelectedValue, "NRGP") = CompairStringResult.Equal Then
                txtVendorNo.Enabled = False
            Else
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

    Private Function ReturnBalQty(ByVal strSRNNo As String, ByVal strItemCode As String, ByVal strRGPNo As String, ByVal strLoc As String) As Double
        Dim BalQty As Double = 0
        BalQty = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("SELECT  SUM(Qty*RI) as Qty FROM ( select TSPL_SRN_DETAIL.SRN_No,  Item_Code, Rejected_Qty As Qty,1 as RI  From tspl_SRN_Head  Left outer join TSPL_SRN_DETAIL on TSPL_SRN_DETAIL.SRN_No =tspl_SRN_Head.SRN_No Where TSPL_SRN_DETAIL.Rejected_Qty >0 AND tspl_SRN_Head.Bill_To_Location in (Select ISNULL(Location_Code ,'') As Location_Code From TSPL_LOCATION_MASTER Where Rejected_Location='" & strLoc & "') And tspl_SRN_Head.SRN_No='" & strSRNNo & "' and TSPL_SRN_DETAIL.Item_Code ='" & strItemCode & "' " & _
        " Union All " & _
        " select SRN_No, Item_Code,RGP_Qty   As Qty,-1 as RI From TSPL_Milk_RGP_Detail LEFT OUTER JOIN TSPL_Milk_RGP_HEAD  ON TSPL_Milk_RGP_HEAD.RGP_No  =TSPL_Milk_RGP_Detail.RGP_No where TSPL_Milk_RGP_HEAD.SRN_No='" & strSRNNo & "'  and TSPL_Milk_RGP_Detail.Item_Code ='" & strItemCode & "'   and TSPL_Milk_RGP_HEAD.RGP_No not IN('" & strRGPNo & "') " & _
        " )  aa group by SRN_No,Item_Code having SUM(Qty*RI) >0"))
        Return BalQty
    End Function

    ''=========================================================================================
#Region "Schedule and PO"

    Private Sub txtPoNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtPoNo._MYValidating
        Try
            isInsideLoadData = True
            ' btnSameasAbove.Enabled = True



            Dim frm As New frmPendingPO()
            frm.strFormType = Me.Form_ID
            frm.PurchaseOrder_Type = "J"
            frm.VendorCode = clsCommon.myCstr(txtVendorNo.Value)
            frm.VendorName = clsCommon.myCstr(lblVendorName.Text)
            frm.strCurrCode = clsCommon.myCstr(txtDocNo.Value)
            frm.Is_From_RGP = True
            frm.ShowDialog()

            '  gv_PO.Rows.Clear()

            If frm.ArrReturn IsNot Nothing AndAlso frm.ArrReturn.Count > 0 Then
                Dim obj As clsPurchaseOrderHead = clsPurchaseOrderHead.GetData(frm.ArrReturn(0).PurchaseOrder_No, NavigatorType.Current)

                If obj IsNot Nothing AndAlso clsCommon.myLen(obj.PurchaseOrder_No) > 0 Then
                    txtModeOfTransport.Text = obj.Mode_Of_Transport
                    cboDocType.SelectedValue = "RGP"
                    txtVendorNo.Value = obj.Vendor_Code
                    lblVendorName.Text = obj.Vendor_Name

                    txtReason.Text = obj.Remarks
                    txtRemarks.Text = obj.Remarks
                    cboDocType.Enabled = False
                    txtLocation.Enabled = False


                    lblDocumentAmt.Text = clsCommon.myFormat(obj.PO_Total_Amt)

                    txtDepartment.Value = obj.Dept
                    lblDepartment.Text = obj.Dept_Desc




                    LblItemConType.Visible = False
                    CmbItemConType.Visible = False

                    ''
                    txtVendorNo.Value = obj.Vendor_Code
                    lblVendorName.Text = obj.Vendor_Name


                    chkthirdparty.Checked = False
                    txtsrnlocation_code.Value = ""
                    txtsrnlocation.Text = ""

                    txtLocation.Value = obj.Bill_To_Location
                    lblLocation.Text = obj.BillToLocationName
                    lblDepartment.Text = clsDBFuncationality.getSingleValue("select Description from TSPL_GL_SEGMENT_CODE where Seg_No  ='3' and Segment_code='" + txtDepartment.Value + "'")

                    txtVendorNo.Enabled = True


                    txtPoNo.Value = obj.PurchaseOrder_No
                    If clsCommon.myLen(txtPoNo.Value) > 0 Then
                        txtPoNo.Enabled = True
                    End If



                    ' btnSameasAbove.Enabled = False


                End If 'if cond. of PO obj
            End If 'if cond. frm.arr

            RadPageView2.SelectedPage = RadPageViewPage2
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        Finally
            isInsideLoadData = False
        End Try
    End Sub


    Private Sub txtScheduleNo__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean)
        Try
            isInsideLoadData = True
            ' btnSameasAbove.Enabled = True

            If clsCommon.myLen(txtPoNo.Value) > 0 Then ' if po used then no sch. used
                Exit Sub
            End If

            Dim frm As New FrmPendingPOSchedule()
            frm.PurchaseOrder_Type = "J"
            frm.VendorCode = clsCommon.myCstr(txtVendorNo.Value)
            frm.VendorName = clsCommon.myCstr(lblVendorName.Text)
            frm.strCurrCode = clsCommon.myCstr(txtDocNo.Value)
            frm.strCurrDate = clsCommon.myCDate(txtDate.Text)
            frm.Is_From_RGP = True
            frm.ShowDialog()

            ' gv_PO.Rows.Clear()

            If frm.ArrReturn IsNot Nothing AndAlso frm.ArrReturn.Count > 0 Then
                Dim obj As clsPurchaseSchedule = clsPurchaseSchedule.GetData(frm.ArrReturn(0).Document_Code, NavigatorType.Current)

                If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Document_Code) > 0 Then
                    txtModeOfTransport.Text = ""
                    cboDocType.SelectedValue = "RGP"
                    txtVendorNo.Value = obj.Vendor_Code
                    lblVendorName.Text = obj.Vendor_Name

                    txtReason.Text = ""
                    txtRemarks.Text = ""
                    cboDocType.Enabled = False
                    txtLocation.Enabled = True


                    lblDocumentAmt.Text = Nothing

                    txtDepartment.Value = Nothing
                    lblDepartment.Text = Nothing


                    LblItemConType.Visible = False
                    CmbItemConType.Visible = False


                    chkthirdparty.Checked = False
                    txtsrnlocation_code.Value = ""
                    txtsrnlocation.Text = ""

                    txtLocation.Value = Nothing
                    lblLocation.Text = Nothing
                    lblDepartment.Text = clsDBFuncationality.getSingleValue("select Description from TSPL_GL_SEGMENT_CODE where Seg_No  ='3' and Segment_code='" + txtDepartment.Value + "'")

                    txtVendorNo.Enabled = True


                    txtPoNo.Value = Nothing
                    If clsCommon.myLen(txtPoNo.Value) > 0 Then
                        txtPoNo.Enabled = True
                    End If



                    ' btnSameasAbove.Enabled = False


                End If 'if cond. of PO obj
            End If 'if cond. frm.arr

            ' RadPageView2.SelectedPage = RadPageViewPage3
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        Finally
            isInsideLoadData = False
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
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value = rawqty
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colBalQty).Value = rawqty
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colRetQty).Value = 0
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colUnit).Value = objtr.CONSM_ITEM_UNIT_CODE
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colApproxCost).Value = 0 ' gv1.Rows(gv1.Rows.Count - 1).Cells(colRate).Value = 0
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colAmt).Value = 0
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colSp).Value = objtr.REMARKS
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
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value = rawqty
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colBalQty).Value = rawqty
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colRetQty).Value = 0
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colUnit).Value = objtr.CONSM_ITEM_UNIT_CODE
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colApproxCost).Value = 0 ' gv1.Rows(gv1.Rows.Count - 1).Cells(colRate).Value = 0
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colAmt).Value = 0

                    gv1.Rows(gv1.Rows.Count - 1).Cells(colSp).Value = objtr.REMARKS

                Next
            Else
                gv1.Rows.AddNew()
            End If '==obj cond.
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub gv_PO_UserDeletingRow(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs)
        If common.clsCommon.MyMessageBoxShow("Delete The Current Row." + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
            e.Cancel = True
        Else
        End If
    End Sub

#End Region

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    Private Sub btngo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btngo.Click
        'gv_qc.Rows.Clear()
        FillQCGrid(Nothing, Nothing, Nothing)
    End Sub

    Private Sub RemoveCurrentItemQCRow(ByVal Item_Code As String, ByVal Frm_Loc As String, ByVal To_Loc As String)
        For ii As Integer = gv_qc.Rows.Count - 1 To 0 Step -1
            If clsCommon.myLen(gv_qc.Rows(ii).Cells(colQCitemcode).Value) > 0 AndAlso clsCommon.CompairString(clsCommon.myCstr(gv_qc.Rows(ii).Cells(colQCitemcode).Value), Item_Code) = CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(gv_qc.Rows(ii).Cells(colQCloc_code).Value), Frm_Loc) = CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(gv_qc.Rows(ii).Cells(colQCToloc_code).Value), To_Loc) = CompairStringResult.Equal Then
                gv_qc.Rows.RemoveAt(ii)
            ElseIf clsCommon.myLen(gv_qc.Rows(ii).Cells(colQCitemcode).Value) > 0 AndAlso clsCommon.CompairString(clsCommon.myCstr(gv_qc.Rows(ii).Cells(colQCitemcode).Value), Item_Code) = CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(gv_qc.Rows(ii).Cells(colQCToloc_code).Value), To_Loc) = CompairStringResult.Equal Then
                gv_qc.Rows.RemoveAt(ii)
            ElseIf clsCommon.myLen(gv_qc.Rows(ii).Cells(colQCitemcode).Value) > 0 AndAlso clsCommon.CompairString(clsCommon.myCstr(gv_qc.Rows(ii).Cells(colQCitemcode).Value), Item_Code) = CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(gv_qc.Rows(ii).Cells(colQCloc_code).Value), Frm_Loc) = CompairStringResult.Equal Then
                gv_qc.Rows.RemoveAt(ii)
            End If
        Next
    End Sub

    Private Sub gv_qc_CellDoubleClick(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv_qc.CellDoubleClick
        Try
            If e.Column Is gv_qc.Columns(colQCHistort) Then
                Dim frm As New FrmPPQCCheckHistory()
                frm.Item_Code = clsCommon.myCstr(gv_qc.CurrentRow.Cells(colQCitemcode).Value)
                frm.WindowState = FormWindowState.Maximized
                frm.ShowDialog()
            End If
            If e.Column Is gv_qc.Columns(colQCValue) Then
                Dim arrValue As New List(Of String)
                Dim xvalue As String = clsCommon.myCstr(gv_qc.CurrentRow.Cells(colQCValue).Value)
                If clsCommon.myLen(xvalue) > 0 Then
                    Dim split() As String
                    split = xvalue.Split(",")

                    Dim ii As Integer = split.Length

                    While (ii > 0)
                        If clsCommon.myLen(split(ii)) > 0 Then
                            arrValue.Add(split(ii))
                        End If
                        ii -= 1
                    End While
                End If

                Dim frm As New FrmCheckBoxGrid()
                frm.qry = "select value from tspl_Parameter_value_master where parameter_code='" + clsCommon.myCstr(gv_qc.CurrentRow.Cells(colQCparamcode).Value) + "'"
                frm.arrValue = arrValue
                frm.ShowDialog()

                arrValue = frm.arrValue
                gv_qc.CurrentRow.Cells(colQCValue).Value = ""
                If arrValue IsNot Nothing AndAlso arrValue.Count > 0 Then
                    For Each arr As String In arrValue
                        gv_qc.CurrentRow.Cells(colQCValue).Value = clsCommon.myCstr(gv_qc.CurrentRow.Cells(colQCValue).Value) + "," + clsCommon.myCstr(arr)

                        If clsCommon.myCstr(gv_qc.CurrentRow.Cells(colQCValue).Value).Substring(0, 1) = "," Then
                            gv_qc.CurrentRow.Cells(colQCValue).Value = clsCommon.myCstr(gv_qc.CurrentRow.Cells(colQCValue).Value).Substring(1, clsCommon.myCstr(gv_qc.CurrentRow.Cells(colQCValue).Value).Length - 1)
                        End If
                    Next
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub gv_qc_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv_qc.CellValueChanged
        If Not isInsideLoadData Then
            If Not isCellValueChangedOpen Then
                'If (e.Column Is gv_qc.Columns(colQCparamcode)) Or (e.Column Is gv_qc.Columns(colQCparam_desc)) Or (e.Column Is gv_qc.Columns(colQCparam_type)) Or (e.Column Is gv_qc.Columns(colQCparam_nature)) Then
                isCellValueChangedOpen = True
                If clsCommon.CompairString(clsCommon.myCstr(gv_qc.CurrentRow.Cells(colQCparam_nature).Value), "Range") = CompairStringResult.Equal Then
                    gv_qc.CurrentRow.Cells(colQCvalue1).Value = Nothing
                    gv_qc.CurrentRow.Cells(colQCvalue2).Value = Nothing
                    gv_qc.CurrentRow.Cells(colQCstatus).Value = "None"
                ElseIf clsCommon.CompairString(clsCommon.myCstr(gv_qc.CurrentRow.Cells(colQCparam_nature).Value), "Alphanumeric") = CompairStringResult.Equal Then
                    gv_qc.CurrentRow.Cells(colQCrange1).Value = Nothing
                    gv_qc.CurrentRow.Cells(colQCrange2).Value = Nothing
                    gv_qc.CurrentRow.Cells(colQCstatus).Value = "None"
                ElseIf clsCommon.CompairString(clsCommon.myCstr(gv_qc.CurrentRow.Cells(colQCparam_nature).Value), "Boolean") = CompairStringResult.Equal Then
                    gv_qc.CurrentRow.Cells(colQCvalue1).Value = Nothing
                    gv_qc.CurrentRow.Cells(colQCvalue2).Value = Nothing
                    gv_qc.CurrentRow.Cells(colQCrange1).Value = Nothing
                    gv_qc.CurrentRow.Cells(colQCrange2).Value = Nothing
                End If
                isCellValueChangedOpen = False

                If e.Column Is gv_qc.Columns(colQCRange) Then
                    isCellValueChangedOpen = True
                    Dim Icode As String = clsCommon.myCstr(gv_qc.CurrentRow.Cells(colQCitemcode).Value)
                    Dim frmloccode As String = clsCommon.myCstr(gv_qc.CurrentRow.Cells(colQCloc_code).Value)
                    Dim toloccode As String = clsCommon.myCstr(gv_qc.CurrentRow.Cells(colQCToloc_code).Value)
                    Dim gvIcode As String = ""
                    Dim gvfrmloccode As String = ""
                    Dim gvtoloccode As String = ""

                    For Each grow As GridViewRowInfo In gv1.Rows
                        gvIcode = clsCommon.myCstr(grow.Cells(colICode).Value)
                        gvfrmloccode = clsCommon.myCstr(txtLocation.Value)
                        gvtoloccode = clsCommon.myCstr(grow.Cells(ColLocationSub).Value)

                        If clsCommon.CompairString(gvIcode, Icode) = CompairStringResult.Equal AndAlso clsCommon.CompairString(gvfrmloccode, frmloccode) = CompairStringResult.Equal AndAlso clsCommon.CompairString(gvtoloccode, toloccode) = CompairStringResult.Equal Then
                            If clsCommon.CompairString(clsCommon.myCstr(gv_qc.CurrentRow.Cells(colQCparam_type).Value), "FAT") = CompairStringResult.Equal Then
                                grow.Cells(ColFATRATE).Value = Math.Round(clsCommon.myCdbl(gv_qc.CurrentRow.Cells(colQCRange).Value), 2)
                                grow.Cells(ColFATKG).Value = clsBOM.GetFatSNFKG_AfterConversion(gvIcode, clsCommon.myCstr(grow.Cells(colUnit).Value), clsCommon.myCdbl(grow.Cells(colQty).Value), clsCommon.myCdbl(grow.Cells(ColFATRATE).Value), Nothing) ' Math.Round((clsCommon.myCdbl(grow.Cells(colqty).Value) * clsCommon.myCdbl(grow.Cells(colfatpers).Value)) / 100, DecimalPoint)
                            End If
                            If clsCommon.CompairString(clsCommon.myCstr(gv_qc.CurrentRow.Cells(colQCparam_type).Value), "SNF") = CompairStringResult.Equal Then
                                grow.Cells(ColSNFRATE).Value = Math.Round(clsCommon.myCdbl(gv_qc.CurrentRow.Cells(colQCRange).Value), 2)
                                grow.Cells(ColSNFKG).Value = clsBOM.GetFatSNFKG_AfterConversion(gvIcode, clsCommon.myCstr(grow.Cells(colUnit).Value), clsCommon.myCdbl(grow.Cells(colQty).Value), clsCommon.myCdbl(grow.Cells(ColSNFRATE).Value), Nothing) ' Math.Round((clsCommon.myCdbl(grow.Cells(colqty).Value) * clsCommon.myCdbl(grow.Cells(colsnfpers).Value)) / 100, DecimalPoint)
                            End If
                        End If
                    Next
                    isCellValueChangedOpen = False
                End If
                'End If
            End If
        End If
    End Sub

    Private Sub gv_qc_CurrentColumnChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gv_qc.CurrentColumnChanged
        If gv_qc.RowCount > 0 Then
            gv_qc.CurrentRow.Cells(colQCHistort).Value = "Double Click"
            'If intCurrRow = gv.Rows.Count - 1 Then
            '    gv.Rows.AddNew()
            '    gv.CurrentRow = gv.Rows(intCurrRow)
            'End If
        End If
    End Sub

    Private Function PostAllowToSave() As Boolean
        Try

            Dim arrGVIcode As List(Of String) = Nothing
            arrGVIcode = New List(Of String)
            Dim arrMilkIcode As List(Of String) = Nothing
            arrMilkIcode = New List(Of String)
            Dim MilkProduct As String = Nothing

            For Each grow As GridViewRowInfo In gv1.Rows
                Dim icode As String = clsCommon.myCstr(grow.Cells(colICode).Value)
                ' Dim to_location_code As String = clsCommon.myCstr(grow.Cells(ColLocationSub).Value)

                Dim loc_type As Integer = 0
                If clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(ColLocationType).Value), "1") = CompairStringResult.Equal Then
                    loc_type = 2
                ElseIf clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(ColLocationType).Value), "2") = CompairStringResult.Equal Then
                    loc_type = 1
                ElseIf clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(ColLocationType).Value), "3") = CompairStringResult.Equal Then
                    loc_type = 0
                End If

                FillAvail_Stock(grow.Index, icode, txtLocation.Value, clsCommon.myCstr(grow.Cells(ColLocationSub).Value), "MI", clsCommon.myCstr(grow.Cells(colUnit).Value), loc_type)
                Dim availstck As Decimal = clsCommon.myCdbl(grow.Cells(colBalQty).Value)
                Dim issueqty As Decimal = clsCommon.myCdbl(grow.Cells(colQty).Value)

                If clsCommon.myLen(icode) > 0 Then

                    If issueqty > availstck Then
                        RadPageView1.SelectedPage = RadPageViewPage1
                        gv1.CurrentRow = gv1.Rows(grow.Index)
                        gv1.CurrentColumn = gv1.Columns(colQty)
                        isInsideLoadData = False
                        Throw New Exception("Issue qty is more than available stock at row no. " + clsCommon.myCstr(grow.Index + 1) + "")
                    End If

                    MilkProduct = clsItemMaster.GetItemProductType(icode, Nothing)

                    Dim qry As String = "select count(*) from TSPL_ITEM_QC_PARAMETER_MASTER where item_code='" + icode + "'"
                    If clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry)) > 0 AndAlso Not arrGVIcode.Contains(icode) Then
                        arrGVIcode.Add(icode)
                    End If

                    '==============if item is "Milk" of "Milk Product" and not have parameters then add this item to array of milk
                    If (clsCommon.CompairString(MilkProduct, "MI") = CompairStringResult.Equal OrElse clsCommon.CompairString(MilkProduct, "MP") = CompairStringResult.Equal) AndAlso Not arrGVIcode.Contains(icode) Then
                        If Not arrMilkIcode.Contains(icode) Then
                            arrMilkIcode.Add(icode)
                        End If
                    End If
                    '======end here==========================
                End If
                '========================fill section detail=========================
                'If clsCommon.myLen(icode) > 0 AndAlso clsCommon.myLen(to_location_code) <= 0 Then
                '    isInsideLoadData = True
                '    Dim sec_loc As String = ""
                '    sec_loc = clsProcessProductionIssueEntry.GetBOSectionLocationCode(txtbatchorder.Value, txtmain_Loc_Code.Value, icode)

                '    If clsCommon.myLen(sec_loc) > 0 Then
                '        grow.Cells(colToloc_code).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select top 1 location_code from tspl_location_master where location_code in (" + sec_loc + ")"))
                '        to_location_code = clsCommon.myCstr(grow.Cells(colToloc_code).Value)
                '        grow.Cells(colToloc_name).Value = clsCommon.myCstr(clsLocation.GetName(to_location_code, Nothing))
                '    End If
                '    If clsCommon.myLen(to_location_code) <= 0 Then
                '        RadPageView1.SelectedPage = RadPageViewPage1
                '        gv.CurrentRow = gv.Rows(grow.Index)
                '        gv.CurrentColumn = gv.Columns(colToloc_code)
                '        isInsideLoadData = False
                '        Throw New Exception("Consumption location not found at row no. " + clsCommon.myCstr(grow.Index + 1) + "")
                '    End If
                '    isInsideLoadData = False
                'End If


            Next

            Dim paramcode As String = ""
            Dim nature As String = ""
            Dim range1 As Decimal = Nothing
            Dim range2 As Decimal = Nothing
            Dim status As String = ""
            Dim value1 As String = ""
            Dim value2 As String = ""
            Dim QC_Range As Decimal = Nothing
            Dim QC_Value As String = ""
            Dim QC_Status As String = ""
            Dim arrParameter As New List(Of String)
            arrParameter = New List(Of String)
            Dim arrParamIcode As New List(Of String)
            arrParamIcode = New List(Of String)
            MilkProduct = Nothing


            For ii As Integer = 0 To gv_qc.Rows.Count - 1
                paramcode = clsCommon.myCstr(gv_qc.Rows(ii).Cells(colQCparamcode).Value)

                If clsCommon.myLen(paramcode) > 0 AndAlso Not arrParameter.Contains(paramcode) Then
                    arrParameter.Add(paramcode)
                End If

                Dim icode As String = clsCommon.myCstr(gv_qc.Rows(ii).Cells(colQCitemcode).Value)

                If clsCommon.myLen(icode) > 0 AndAlso clsCommon.myLen(paramcode) > 0 Then
                    MilkProduct = clsItemMaster.GetItemProductType(icode, Nothing)


                    If Not arrParamIcode.Contains(icode) Then
                        arrParamIcode.Add(icode)
                    End If
                End If


                nature = clsCommon.myCstr(gv_qc.Rows(ii).Cells(colQCparam_nature).Value)
                range1 = clsCommon.myCdbl(gv_qc.Rows(ii).Cells(colQCrange1).Value)
                range2 = clsCommon.myCdbl(gv_qc.Rows(ii).Cells(colQCrange2).Value)
                status = clsCommon.myCstr(gv_qc.Rows(ii).Cells(colQCstatus).Value)
                value1 = clsCommon.myCstr(gv_qc.Rows(ii).Cells(colQCvalue1).Value)
                value2 = clsCommon.myCstr(gv_qc.Rows(ii).Cells(colQCvalue2).Value)
                QC_Range = clsCommon.myCdbl(gv_qc.Rows(ii).Cells(colQCRange).Value)
                QC_Status = clsCommon.myCstr(gv_qc.Rows(ii).Cells(colQCOutStatus).Value)
                QC_Value = clsCommon.myCstr(gv_qc.Rows(ii).Cells(colQCValue).Value)

                If clsCommon.myLen(paramcode) > 0 AndAlso clsCommon.CompairString(nature, "Boolean") = CompairStringResult.Equal AndAlso clsCommon.myLen(status) > 0 AndAlso clsCommon.CompairString(status, "None") <> CompairStringResult.Equal AndAlso (clsCommon.myLen(QC_Status) <= 0 Or QC_Status = "None") Then
                    RadPageView1.SelectedPage = RadPageViewPage4
                    gv_qc.Rows(ii).Cells(colQCRange).Value = Nothing
                    gv_qc.Rows(ii).Cells(colQCValue).Value = Nothing
                    gv_qc.CurrentRow = gv_qc.Rows(ii)
                    gv_qc.CurrentColumn = gv_qc.Columns(colQCOutStatus)
                    Throw New Exception("Fill Status at row no. " + clsCommon.myCstr(ii + 1) + "")
                End If

                '=====================range can be 0
                If clsCommon.myLen(paramcode) > 0 AndAlso clsCommon.CompairString(nature, "Range") = CompairStringResult.Equal AndAlso clsCommon.myCdbl(QC_Range) <= 0 Then
                    RadPageView1.SelectedPage = RadPageViewPage4
                    gv_qc.Rows(ii).Cells(colQCOutStatus).Value = ""
                    gv_qc.Rows(ii).Cells(colQCValue).Value = Nothing
                    gv_qc.CurrentRow = gv_qc.Rows(ii)
                    gv_qc.CurrentColumn = gv_qc.Columns(colQCRange)
                    If Not clsCommon.MyMessageBoxShow("Actual Range at row no. " + clsCommon.myCstr(ii + 1) + " is 0,Do you want to continue?", "Attention", MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                        Return False
                    End If
                End If

                If clsCommon.myLen(paramcode) > 0 AndAlso clsCommon.CompairString(nature, "Alphanumeric") = CompairStringResult.Equal AndAlso clsCommon.myLen(value1) > 0 AndAlso clsCommon.myLen(QC_Value) <= 0 Then
                    RadPageView1.SelectedPage = RadPageViewPage4
                    gv_qc.Rows(ii).Cells(colQCRange).Value = Nothing
                    gv_qc.Rows(ii).Cells(colQCOutStatus).Value = Nothing
                    gv_qc.CurrentRow = gv_qc.Rows(ii)
                    gv_qc.CurrentColumn = gv_qc.Columns(colQCValue)
                    Throw New Exception("Fill value at row no. " + clsCommon.myCstr(ii + 1) + "")
                End If
            Next
            '==============if item is "Milk" of "Milk Product" and have parameters but not fill in qc grid then add this item to array of milk
            If arrGVIcode IsNot Nothing AndAlso arrGVIcode.Count > 0 Then
                For Each Str As String In arrGVIcode
                    If Not arrParamIcode.Contains(Str) AndAlso Not arrMilkIcode.Contains(Str) Then
                        arrMilkIcode.Add(Str)
                    End If
                Next
            End If
            '============end here

            If (arrGVIcode IsNot Nothing AndAlso arrGVIcode.Count > 0) AndAlso (arrParameter Is Nothing OrElse arrParameter.Count <= 0) Then
                RadPageView1.SelectedPage = RadPageViewPage4
                btngo.PerformClick()
                gv_qc.Focus()
                gv_qc.Select()
                Throw New Exception("Fill QC item parameters.")
            End If

            '=================message send in case of milk item when no qc record found.
            MilkProduct = Nothing
            If arrMilkIcode IsNot Nothing AndAlso arrMilkIcode.Count > 0 Then
                For Each Str As String In arrMilkIcode
                    MilkProduct = MilkProduct + ", " + clsCommon.myCstr(clsItemMaster.GetItemName(Str, Nothing))
                Next
                RadPageView1.SelectedPage = RadPageViewPage4
                btngo.PerformClick()
                gv_qc.Focus()
                gv_qc.Select()
                Throw New Exception("Fill QC for items (" + MilkProduct + ").")
            End If

            Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
            Return False
        End Try
    End Function
    Sub FunRGPChallanPrint()
        Try

            Dim qry As String = " select TSPL_MILK_RGP_HEAD.RGP_No ,convert(varchar,RGP_Date,103) as RGP_Date ,Location,Comp_Name ,m.Location_Desc,m.GSTNO as Loc_GstInNo  ,TSPL_MILK_RGP_HEAD.Vendor_Code ,TSPL_VENDOR_MASTER.Vendor_Name,TSPL_VENDOR_MASTER.GSTFinalNo AS Vendor_GSTIN_NO ,GRNo ,GR_Date,TSPL_MILK_RGP_DETAIL.Item_Code,TSPL_ITEM_MASTER.Item_Desc,TSPL_ITEM_MASTER.HSN_Code  ,Item_Cost,RGP_Qty  ,TSPL_MILK_RGP_DETAIL.Unit_code ,FAT_KG ,SNF_Kg ,Amount,To_Location_Code,tt.Location_Desc  ,Tanker_No , m.add1 +case when len(m.add2)>0 then ', '+m.add2 else '' end +case when LEN(isnull(m.Add3,''))>0 then ', '+isnull(m.Add3,'') else ' ' end + case when LEN(City_Location.City_Name)>0 then ', '+City_Location .City_Name else ' ' end + case when len(State_Location.STATE_NAME  )>0 then ', '+ State_Location.STATE_NAME else ' ' end  + case when len(m.Pin_Code   )>0 then ', Pin Code - '+ cast(m.Pin_Code  as varchar)  else ' ' end  + case when len(m.Tin_No     )>0 then ', Tin No - '+ cast(m.Tin_No as varchar)  else ' ' end  + Case when len(ISNULL(m.Phone1,''))>0 and m.Phone1='(+__)__________' then '' else ',Phone'+m.Phone1 end +  Case When   ISNULL(m.Phone2,'')<>'(+__)__________' Then ',  '+ m.Phone2 Else'' End   + case when len(m.Email    )>0 then ',Email - '+ m.Email else '' end  as Location_Address,m.add1 +case when len(m.add2)>0 then ', '+m.add2 else '' end +case when LEN(isnull(m.Add3,''))>0 then ', '+isnull(m.Add3,'') else ' ' end + case when LEN(City_Location.City_Name)>0 then ', '+City_Location .City_Name else ' ' end + case when len(State_Location.STATE_NAME  )>0 then ', '+ State_Location.STATE_NAME else ' ' end  + case when len(m.Pin_Code   )>0 then ', Pin Code - '+ cast(m.Pin_Code  as varchar)  else ' ' end  + case when len(m.GSTNO)>0 then ', GSTIN No - '+ cast(m.GSTNO as varchar)  else ' ' end  + Case when len(ISNULL(m.Phone1,''))>0 and m.Phone1='(+__)__________' then '' else ',Phone'+m.Phone1 end +  Case When   ISNULL(m.Phone2,'')<>'(+__)__________' Then ',  '+ m.Phone2 Else'' End   + case when len(m.Email    )>0 then ',Email - '+ m.Email else '' end  as Location_Address_GST," & _
                          " TSPL_COMPANY_MASTER.add1 +case when len(TSPL_COMPANY_MASTER.add2)>0 then ', '+TSPL_COMPANY_MASTER.add2 else '' end +case when LEN(isnull(TSPL_COMPANY_MASTER.Add3,''))>0 then ', '+isnull(TSPL_COMPANY_MASTER.Add3,'') else ' ' end + case when LEN(City_Company.City_Name)>0 then ', '+City_Company .City_Name else ' ' end + case when len(State_Company.STATE_NAME  )>0 then ', '+ State_Company.STATE_NAME else ' ' end   + case when len(m.Tin_No     )>0 then ', Tin No - '+ cast(TSPL_COMPANY_MASTER.Tin_No as varchar)  else ' ' end  + Case when len(ISNULL(m.Phone1,''))>0 and TSPL_COMPANY_MASTER.Phone1='(+__)__________' then '' else ',Phone'+TSPL_COMPANY_MASTER.Phone1 end +  Case When   ISNULL(TSPL_COMPANY_MASTER.Phone2,'')<>'(+__)__________' Then ',  '+ TSPL_COMPANY_MASTER.Phone2 Else'' End   + case when len(TSPL_COMPANY_MASTER.Email    )>0 then ',Email - '+ TSPL_COMPANY_MASTER.Email else '' end  as Company_Address,TSPL_COMPANY_MASTER.add1 +case when len(TSPL_COMPANY_MASTER.add2)>0 then ', '+TSPL_COMPANY_MASTER.add2 else '' end +case when LEN(isnull(TSPL_COMPANY_MASTER.Add3,''))>0 then ', '+isnull(TSPL_COMPANY_MASTER.Add3,'') else ' ' end + case when LEN(City_Company.City_Name)>0 then ', '+City_Company .City_Name else ' ' end + case when len(State_Company.STATE_NAME  )>0 then ', '+ State_Company.STATE_NAME else ' ' end   + case when len(m.GSTNO     )>0 then ', GSTIN No - '+ cast(TSPL_COMPANY_MASTER.GSTINNo as varchar)  else ' ' end  + Case when len(ISNULL(m.Phone1,''))>0 and TSPL_COMPANY_MASTER.Phone1='(+__)__________' then '' else ',Phone'+TSPL_COMPANY_MASTER.Phone1 end +  Case When   ISNULL(TSPL_COMPANY_MASTER.Phone2,'')<>'(+__)__________' Then ',  '+ TSPL_COMPANY_MASTER.Phone2 Else'' End   + case when len(TSPL_COMPANY_MASTER.Email    )>0 then ',Email - '+ TSPL_COMPANY_MASTER.Email else '' end  as Company_Address_GST," & _
                          " TSPL_VENDOR_MASTER.add1 +case when len(TSPL_VENDOR_MASTER.add2)>0 then ', '+TSPL_VENDOR_MASTER.add2 else '' end +case when LEN(isnull(TSPL_VENDOR_MASTER.Add3,''))>0 then ', '+isnull(TSPL_VENDOR_MASTER.Add3,'') else ' ' end + case when LEN(City_Vendor.City_Name)>0 then ', '+City_Vendor .City_Name else ' ' end + case when len(State_Vendor.STATE_NAME  )>0 then ', '+ State_Company.STATE_NAME else ' ' end   + case when len(TSPL_VENDOR_MASTER.Tin_No     )>0 then ', Tin No - '+ cast(TSPL_VENDOR_MASTER.Tin_No as varchar)  else ' ' end  + Case when len(ISNULL(m.Phone1,''))>0 and TSPL_VENDOR_MASTER.Phone1='(+__)__________' then '' else ',Phone'+TSPL_VENDOR_MASTER.Phone1 end +  Case When   ISNULL(TSPL_VENDOR_MASTER.Phone2,'')<>'(+__)__________' Then ',  '+ TSPL_VENDOR_MASTER.Phone2 Else'' End   + case when len(TSPL_VENDOR_MASTER.Email    )>0 then ',Email - '+ TSPL_VENDOR_MASTER.Email else '' end  as Vendor_Address,TSPL_VENDOR_MASTER.add1 +case when len(TSPL_VENDOR_MASTER.add2)>0 then ', '+TSPL_VENDOR_MASTER.add2 else '' end +case when LEN(isnull(TSPL_VENDOR_MASTER.Add3,''))>0 then ', '+isnull(TSPL_VENDOR_MASTER.Add3,'') else ' ' end + case when LEN(City_Vendor.City_Name)>0 then ', '+City_Vendor .City_Name else ' ' end + case when len(State_Vendor.STATE_NAME  )>0 then ', '+ State_Company.STATE_NAME else ' ' end   + case when len(TSPL_VENDOR_MASTER.GSTFinalNo     )>0 then ', GSTIN No - '+ cast(TSPL_VENDOR_MASTER.GSTFinalNo as varchar)  else ' ' end  + Case when len(ISNULL(m.Phone1,''))>0 and TSPL_VENDOR_MASTER.Phone1='(+__)__________' then '' else ',Phone'+TSPL_VENDOR_MASTER.Phone1 end +  Case When   ISNULL(TSPL_VENDOR_MASTER.Phone2,'')<>'(+__)__________' Then ',  '+ TSPL_VENDOR_MASTER.Phone2 Else'' End   + case when len(TSPL_VENDOR_MASTER.Email    )>0 then ',Email - '+ TSPL_VENDOR_MASTER.Email else '' end  as Vendor_Address_GST,stuff((select ',' + isnull(Tanker_No ,'') from TSPL_MILK_RGP_DETAIL where TSPL_MILK_RGP_DETAIL.RGP_No =TSPL_MILK_RGP_HEAD.RGP_No  for xml path ('')),1,1,'' )as Tanker_No ,case when TSPL_VENDOR_MASTER.Transporter='Y' then TSPL_VENDOR_MASTER.Vendor_Name  else '' end as Transporter,TSPL_VENDOR_MASTER.Tin_No ,TSPL_VENDOR_MASTER.CST  " & _
                          " from TSPL_MILK_RGP_DETAIL left join TSPL_MILK_RGP_HEAD on TSPL_MILK_RGP_HEAD.RGP_No =   TSPL_MILK_RGP_DETAIL.RGP_No left join TSPL_VENDOR_MASTER on  TSPL_VENDOR_MASTER.Vendor_Code = TSPL_MILK_RGP_HEAD.Vendor_Code  " & _
                          " left join TSPL_LOCATION_MASTER as M on m.Location_Code =TSPL_MILK_RGP_HEAD.Location " & _
                          " left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code =TSPL_MILK_RGP_DETAIL.Item_Code " & _
                          " left join TSPL_LOCATION_MASTER as tt on tt.Location_Code = TSPL_MILK_RGP_DETAIL.To_Location_Code " & _
                          " left join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code =TSPL_MILK_RGP_HEAD.comp_code " & _
                          " left join TSPL_CITY_MASTER as City_Location on City_Location.City_Code = m.City_Code " & _
                          " left join TSPL_STATE_MASTER  as State_Location on State_Location.STATE_CODE = m.State " & _
                          " left join TSPL_CITY_MASTER as City_Company on City_Company.City_Code = TSPL_COMPANY_MASTER.City_Code " & _
                          " left join TSPL_STATE_MASTER  as State_Company on State_Company.STATE_CODE = TSPL_COMPANY_MASTER.State " & _
                          " left join TSPL_CITY_MASTER as City_Vendor on City_Vendor.City_Code = TSPL_VENDOR_MASTER.City_Code " & _
                          " left join TSPL_STATE_MASTER  as State_Vendor on State_Vendor.STATE_CODE = TSPL_VENDOR_MASTER.State where TSPL_MILK_RGP_HEAD.RGP_No = '" + txtDocNo.Value + " '"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt.Rows.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("No Data Found")
            Else
                Dim frmCRV As New frmCrystalReportViewer()
                frmCRV.funreport(CrystalReportFolder.MilkProcurement, dt, "MilkRGPChallan", "Milk RGP Challan Report", txtDate.Value)
                frmCRV = Nothing
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    
End Class
