' '' '' '' ''------ modified by priti adding functionality of internal requsition will not come for PO on 27/05/2012
'-----------BM00000003122
'' -- updation by richa agarwal against ticket BM00000005977 on 05/04/2015
Imports common
Public Class FrmPendingRequistion
#Region "Variables"
    Dim settCreatePOFromMultipleLocation As Boolean = True
    Dim ShowPOCancelButton As Boolean = False
    Public ProjectID As String = Nothing
    Public VendorCode As String = Nothing
    Public VendorName As String = Nothing
    Public strCurrCode As String = Nothing
    Public strCurrCodeSRN As String = Nothing
    Public isFromSRN As Boolean = False
    Public ArrReturn As List(Of clsRequistionDetail) = Nothing
    Public Against_Tendor As String = Nothing
    Const colDSelect As String = "SELECT"
    Const colDCode As String = "CODE"
    Const colDICode As String = "ICODE"
    Const colDIName As String = "INAME"
    Const colDUnit As String = "UNIT"
    Const colDRate As String = "RATE"
    Const colDOrderQty As String = "ORDERQTY"
    Const colDApprovedQty As String = "APPROVEDQTY"
    Const colDUnApprovedQty As String = "UNAPPROVEDQTY"
    Const colDPOCloseQty As String = "POCloseQty"
    Const colDPendingQty As String = "PENDINGQTY"
    Const colDVendor As String = "VENDOR"
    Const colDVendorName As String = "VENDORNAME"

    Const colHSelect As String = "SELECT"
    Const colHCode As String = "CODE"
    Const colHLOCATION_CODE As String = "LOCATION_CODE"
    Const colHDate As String = "DATE"
    Const colHPRType As String = "PRTYPE"

    Const colEmergency As String = "PEmergency"

    Dim dtAllData As DataTable = Nothing
    Dim IsInsideLoadData As Boolean = False
    Dim Str_Location As String = String.Empty
    '=====Sanjeet(22/12/2016)========
    Const colspecification As String = "Specification"
    Const colRemarks As String = "Remarks"
    Const colCapacity As String = "Capacity"
    Const colMake As String = "Make"
    Const colModel As String = "Model"
    '==============================
    Public SettingIndendFreePOClose As Boolean = False
#End Region

    Private Sub FrmPendingRequistion_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        settCreatePOFromMultipleLocation = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.CreatePOFromMultipleLocation, clsFixedParameterCode.CreatePOFromMultipleLocation, Nothing)) > 0)
        ShowPOCancelButton = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.ShowCancelButtonPO, clsFixedParameterCode.ShowCancelButtonPO, Nothing)) = "1", True, False))
        If clsCommon.myLen(VendorName) > 0 Then
            Me.Text = Me.Text + " For " + VendorName
        End If
        Dim qry As String = " select CAST(0 as bit) as Sel,code,ICode,max(IName) as IName" + Environment.NewLine & _
                ",max(Unit)as Unit,max(Location) as Location,MAX(TSPL_LOCATION_MASTER.Location_Desc) as LocationName" + Environment.NewLine & _
                ",SUM(Qty* case when RI=1 then 1 else 0 end) as RequitionQty" + Environment.NewLine & _
                ",SUM(Qty* case when RI=-1 then 1 else 0 end) as POQty" + Environment.NewLine & _
                ",SUM(Unapproved) as UnapprovedQty" + Environment.NewLine & _
                ",sum(POCloseQty) as POCloseQty" + Environment.NewLine + _
                ",SUM((Qty *RI)- Unapproved + POCloseQty) as PedningQty ,MAX(Rate) as Rate,max(TransDate) as TransDate,max(final.Vendor) as Vendor,MAX(TSPL_VENDOR_MASTER.Vendor_Name) as VendorName,MAX(Specification) as Specification,MAX(Remarks) as Remarks,max(Capacity) as Capacity,max(Make) as Make,max(Model) as Model  " + Environment.NewLine & _
                " from (" + Environment.NewLine & _
                " select TSPL_REQUISITION_DETAIL.Requisition_Id as Code,TSPL_REQUISITION_DETAIL.Vendor_Code as Vendor,TSPL_REQUISITION_DETAIL.Item_Code as ICode,TSPL_REQUISITION_DETAIL.Item_Desc as IName,TSPL_REQUISITION_DETAIL.Requisition_Qty as Qty,0 as Unapproved,TSPL_REQUISITION_DETAIL.Unit_Code as Unit,TSPL_REQUISITION_DETAIL.Location as Location,1 as RI,TSPL_REQUISITION_DETAIL.Item_Cost as Rate,1 as Chk,convert(varchar,TSPL_REQUISITION_HEAD.Requisition_Date,103) as TransDate,TSPL_REQUISITION_DETAIL.Specification,TSPL_REQUISITION_DETAIL.Remarks,TSPL_REQUISITION_DETAIL.Capacity,TSPL_REQUISITION_DETAIL.Make,TSPL_REQUISITION_DETAIL.Model,0 as POCloseQty from TSPL_REQUISITION_DETAIL left outer join TSPL_REQUISITION_HEAD on TSPL_REQUISITION_HEAD.Requisition_Id=TSPL_REQUISITION_DETAIL.Requisition_Id where TSPL_REQUISITION_DETAIL.Status='N' and TSPL_REQUISITION_HEAD.Status=1 and TSPL_REQUISITION_HEAD.Is_internal='N' and TSPL_REQUISITION_HEAD.close_yn='N' "
        If clsCommon.myLen(VendorCode) > 0 Then
            qry += " and (len(ISNULL(TSPL_REQUISITION_DETAIL.Vendor_Code,''))=0 or TSPL_REQUISITION_DETAIL.Vendor_Code='" + VendorCode + "')" + Environment.NewLine
        End If
        If clsCommon.myLen(Against_Tendor) > 0 Then
            qry += " and TSPL_REQUISITION_HEAD.Is_Tender='" + Against_Tendor + "'" + Environment.NewLine
        End If
        qry += " and TSPL_REQUISITION_HEAD.From_Screen_Code='" + clsUserMgtCode.mbtnPurchaseRequistion + "'" + Environment.NewLine
        If isFromSRN Then
            Dim dblReqLimit As Double = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ReqLimitOnSRN, clsFixedParameterCode.ReqLimitOnSRN, Nothing))
            qry += " and TSPL_REQUISITION_HEAD.Total_RQ_Amt <='" + clsCommon.myCstr(dblReqLimit) + "'"
        End If

        qry += " union all " + Environment.NewLine & _
               " select TSPL_PURCHASE_ORDER_DETAIL.Requisition_Id as Code,TSPL_PURCHASE_ORDER_HEAD.Vendor_Code as Vendor,TSPL_PURCHASE_ORDER_DETAIL.Item_Code as ICode,TSPL_PURCHASE_ORDER_DETAIL.Item_Desc as IName,case when TSPL_PURCHASE_ORDER_HEAD.Status=0 then 0 else TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_Qty end as Qty ,case when TSPL_PURCHASE_ORDER_HEAD.Status=0 then TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_Qty else 0 end as Unapproved,'' as Unit,'' as Location,-1 as RI,0 as Rate,0 as Chk,null as TransDate,TSPL_PURCHASE_ORDER_DETAIL.Specification,TSPL_PURCHASE_ORDER_DETAIL.Remarks,TSPL_PURCHASE_ORDER_DETAIL.Capacity,TSPL_PURCHASE_ORDER_DETAIL.Make,TSPL_PURCHASE_ORDER_DETAIL.Model,0 as POCloseQty  from TSPL_PURCHASE_ORDER_DETAIL left outer join TSPL_PURCHASE_ORDER_HEAD on TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No=TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No where  len(isnull(TSPL_PURCHASE_ORDER_DETAIL.Requisition_Id,''))>0 and TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No not in ('" + strCurrCode + "')  " + Environment.NewLine
        qry += " and TSPL_PURCHASE_ORDER_HEAD.IsCancel=0 "
        qry += " union all " + Environment.NewLine
        qry += " select TSPL_SRN_DETAIL.Req_No as Code,TSPL_SRN_HEAD.Vendor_Code as Vendor,TSPL_SRN_DETAIL.Item_Code as ICode,TSPL_SRN_DETAIL.Item_Desc as IName,case when TSPL_SRN_HEAD.Status=0 then 0 else TSPL_SRN_DETAIL.SRN_Qty end as Qty ,case when TSPL_SRN_HEAD.Status=0 then TSPL_SRN_DETAIL.SRN_Qty else 0 end as Unapproved,'' as Unit,'' as Location,-1 as RI,0 as Rate,0 as Chk,null as TransDate,'' as Specification,'' as Remarks,'' as Capacity,'' as Make,'' as Model,0 as POCloseQty from TSPL_SRN_DETAIL left outer join TSPL_SRN_HEAD on TSPL_SRN_HEAD.SRN_No=TSPL_SRN_DETAIL.SRN_No where len(isnull(TSPL_SRN_DETAIL.Req_No,''))>0 and len(isnull( PO_Id,''))<=0  and TSPL_SRN_DETAIL.SRN_No not in ('" + strCurrCodeSRN + "')"
        If SettingIndendFreePOClose AndAlso Not isFromSRN Then
            qry += "Union all " + Environment.NewLine + _
                "select Code,'' as Vendor,ICode,'' as IName,0 as Qty,0 as Unapproved,'' as Unit,'' as Location,0 as RI,0 as Rate,0 as Chk,null as TransDate,'' as Specification,'' as Remarks,'' as Capacity,'' as Make,'' as Model,sum(Qty*RI) as POCloseQty   from (" + Environment.NewLine + _
                "select  TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No, TSPL_PURCHASE_ORDER_DETAIL.Requisition_Id as Code,TSPL_PURCHASE_ORDER_DETAIL.Item_Code as ICode ,TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_Qty  as Qty ,1 as RI,1 as Chk  " + Environment.NewLine + _
                "from TSPL_PURCHASE_ORDER_DETAIL left outer join TSPL_PURCHASE_ORDER_HEAD on TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No=TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No where len(isnull(TSPL_PURCHASE_ORDER_DETAIL.Requisition_Id,''))>0 and TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No not in ('')   and TSPL_PURCHASE_ORDER_HEAD.IsCancel=0 " + Environment.NewLine + _
                "and (TSPL_PURCHASE_ORDER_HEAD.close_yn='Y' or TSPL_PURCHASE_ORDER_DETAIL.Status=1) " + Environment.NewLine + _
                "union all" + Environment.NewLine + _
                "select TSPL_GRN_DETAIL.PO_ID as PurchaseOrder_No, TSPL_GRN_DETAIL.Requisition_Id as Code,TSPL_GRN_DETAIL.Item_Code as ICode,TSPL_GRN_DETAIL.GRN_Qty  Qty ,-1 as RI,0 as Chk  " + Environment.NewLine + _
                "from TSPL_GRN_DETAIL " + Environment.NewLine + _
                "left outer join TSPL_GRN_HEAD on TSPL_GRN_HEAD.GRN_No=TSPL_GRN_DETAIL.GRN_No where len(isnull(TSPL_GRN_DETAIL.Requisition_Id,''))>0 and len(isnull( TSPL_GRN_DETAIL.PO_Id,''))>0  " + Environment.NewLine + _
                "union all" + Environment.NewLine + _
                "select TSPL_MRN_DETAIL.PO_ID as PurchaseOrder_No, TSPL_MRN_DETAIL.Requisition_Id as Code,TSPL_MRN_DETAIL.Item_Code as ICode,TSPL_MRN_DETAIL.Reject_Qty as Qty ,1 as RI,0 as chk" + Environment.NewLine + _
                "from TSPL_MRN_DETAIL " + Environment.NewLine + _
                "left outer join TSPL_MRN_HEAD on TSPL_MRN_HEAD.MRN_No=TSPL_MRN_DETAIL.MRN_No where len(isnull(TSPL_MRN_DETAIL.Requisition_Id,''))>0 and len(isnull( TSPL_MRN_DETAIL.PO_ID,''))>0  " + Environment.NewLine + _
                " and TSPL_MRN_HEAD.Status=1 and not exists(select 1 from TSPL_SRN_DETAIL where TSPL_SRN_DETAIL.MRN_Id =TSPL_MRN_DETAIL.MRN_No and  TSPL_SRN_DETAIL.PO_ID=TSPL_MRN_DETAIL.PO_ID and TSPL_SRN_DETAIL.Req_No =TSPL_MRN_DETAIL.Requisition_Id and TSPL_SRN_DETAIL.Item_Code =TSPL_MRN_DETAIL.Item_Code)" + Environment.NewLine + _
                "union all" + Environment.NewLine + _
                "select   TSPL_SRN_DETAIL.PO_ID as PurchaseOrder_No, TSPL_SRN_DETAIL.Req_No as Code,TSPL_SRN_DETAIL.Item_Code as ICode ,TSPL_SRN_DETAIL.Rejected_Qty+TSPL_SRN_DETAIL.Short_Qty as Qty   ,1 as RI,0 as chk" + Environment.NewLine + _
                "from TSPL_SRN_DETAIL " + Environment.NewLine + _
                "left outer join TSPL_SRN_HEAD on TSPL_SRN_HEAD.SRN_No=TSPL_SRN_DETAIL.SRN_No where len(isnull(TSPL_SRN_DETAIL.Req_No,''))>0 and len(isnull( PO_Id,''))>0  " + Environment.NewLine + _
                "and TSPL_SRN_HEAD.Status=1" + Environment.NewLine + _
                ")x group by PurchaseOrder_No,Code,ICode having sum(chk)>0 and sum(Qty*RI)>0"
        End If


        qry += " )Final" + Environment.NewLine & _
                " left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=final.Vendor" + Environment.NewLine & _
                " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=Final.Location where 1=1"
        qry += " group by Code,ICode" + Environment.NewLine & _
                " having SUM(Chk)>0 and SUM((Qty *RI)- Unapproved + POCloseQty)>0 "

        'done by stuti on 14/12/2016 (refer by monika)
        Dim mainqry As String = Nothing
        mainqry += "select * from (" + qry + ") as xyz where 1=1"
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            mainqry += "  and  xyz.Location in (" + objCommonVar.strCurrUserLocations + ")"
        End If
        mainqry += " order by Code,ICode"

        dtAllData = clsDBFuncationality.GetDataTable(mainqry)


        If dtAllData Is Nothing OrElse dtAllData.Rows.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow("No Pending item found ")
            Me.Close()
        End If
        LoadHeadData()
        LoadBlankGridDetail()

    End Sub

    Sub LoadHeadData()
        IsInsideLoadData = True
        LoadBlankHeadGrid()
        Dim arr As New List(Of String)
        For Each dr As DataRow In dtAllData.Rows
            Dim strCode As String = clsCommon.myCstr(dr("code"))
            If Not arr.Contains(strCode) Then
                arr.Add(strCode)
                gvHead.Rows.AddNew()
                gvHead.Rows(gvHead.RowCount - 1).Cells(colHSelect).Value = False
                gvHead.Rows(gvHead.RowCount - 1).Cells(colHCode).Value = strCode
                gvHead.Rows(gvHead.RowCount - 1).Cells(colHDate).Value = clsCommon.myCstr(dr("TransDate"))
                gvHead.Rows(gvHead.RowCount - 1).Cells(colHLOCATION_CODE).Value = clsCommon.myCstr(dr("Location"))
                gvHead.Rows(gvHead.RowCount - 1).Cells(colHPRType).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select (case when Requisition_Type='L' then 'Domestic' else case when Requisition_Type='J' then 'Job Work' else case when Requisition_Type='S' then 'Work Order(Service PO)' end end end) as PRtype from TSPL_REQUISITION_HEAD where Requisition_Id='" + strCode + "'"))
                gvHead.Rows(gvHead.RowCount - 1).Cells(colEmergency).Value = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(" select emergency from TSPL_REQUISITION_HEAD where Requisition_Id='" + strCode + "'"))
            End If
        Next
        IsInsideLoadData = False
    End Sub

    Sub LoadBlankHeadGrid()
        gvHead.Rows.Clear()
        gvHead.Columns.Clear()

        Dim repoSelect As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoSelect.HeaderText = " "
        repoSelect.Name = colHSelect
        repoSelect.ReadOnly = False
        repoSelect.Width = 25
        repoSelect.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gvHead.MasterTemplate.Columns.Add(repoSelect)


        Dim repoCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCode.FormatString = ""
        repoCode.HeaderText = "Requisition No"
        repoCode.Name = colHCode
        repoCode.Width = 170
        repoCode.ReadOnly = True
        gvHead.MasterTemplate.Columns.Add(repoCode)

        Dim repoLocation_Code As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoLocation_Code.FormatString = ""
        repoLocation_Code.HeaderText = "Location Code"
        repoLocation_Code.Name = colHLOCATION_CODE
        repoLocation_Code.Width = 170
        repoLocation_Code.ReadOnly = True
        gvHead.MasterTemplate.Columns.Add(repoLocation_Code)

        Dim repoDate As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoDate.FormatString = ""
        repoDate.HeaderText = "Date"
        repoDate.Name = colHDate
        repoDate.Width = 70
        repoDate.ReadOnly = True
        gvHead.MasterTemplate.Columns.Add(repoDate)

        repoDate = New GridViewTextBoxColumn()
        repoDate.FormatString = ""
        repoDate.HeaderText = "Type"
        repoDate.Name = colHPRType
        repoDate.Width = 70
        repoDate.ReadOnly = True
        gvHead.MasterTemplate.Columns.Add(repoDate)

        repoSelect = New GridViewCheckBoxColumn()
        repoSelect.HeaderText = " "
        repoSelect.Name = colEmergency
        repoSelect.ReadOnly = False
        repoSelect.HeaderText = "Emergency"
        repoSelect.Width = 70
        repoSelect.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gvHead.MasterTemplate.Columns.Add(repoSelect)


        gvHead.ShowFilteringRow = True
        gvHead.EnableFiltering = True
        gvHead.AllowDeleteRow = False
        gvHead.AllowAddNewRow = False
        gvHead.ShowGroupPanel = False
        gvHead.AllowColumnReorder = False
        gvHead.AllowRowReorder = False
        gvHead.EnableSorting = False
        gvHead.EnableAlternatingRowColor = True
        gvHead.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gvHead.MasterTemplate.ShowRowHeaderColumn = False
        gvHead.TableElement.TableHeaderHeight = 40
    End Sub

    Sub LoadBlankGridDetail()
        gv1.Rows.Clear()
        gv1.Columns.Clear()

        Dim repoSelect As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoSelect.HeaderText = " "
        repoSelect.Name = colDSelect
        repoSelect.ReadOnly = False
        repoSelect.Width = 25
        repoSelect.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoSelect)


        Dim repoCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCode.FormatString = ""
        repoCode.HeaderText = "Requisition No"
        repoCode.Name = colDCode
        repoCode.Width = 180
        repoCode.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoCode)

        Dim repoVendor As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoVendor.FormatString = ""
        repoVendor.HeaderText = "Vendor Code"
        repoVendor.Name = colDVendor
        repoVendor.Width = 100
        repoVendor.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoVendor)

        Dim repoVendorName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoVendorName.FormatString = ""
        repoVendorName.HeaderText = "Vendor"
        repoVendorName.Name = colDVendorName
        repoVendorName.Width = 150
        repoVendorName.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoVendorName)


        Dim repoICode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoICode.FormatString = ""
        repoICode.HeaderText = "Item Code"
        repoICode.Name = colDICode
        repoICode.Width = 100
        repoICode.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoICode)

        Dim repoIName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoIName.FormatString = ""
        repoIName.HeaderText = "Item"
        repoIName.Name = colDIName
        repoIName.Width = 180
        repoIName.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoIName)

        Dim repoUnit As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoUnit.FormatString = ""
        repoUnit.HeaderText = "Unit"
        repoUnit.Name = colDUnit
        repoUnit.Width = 60
        repoUnit.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoUnit)


        Dim repoRate As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoRate.FormatString = ""
        repoRate.HeaderText = "Rate"
        repoRate.Name = colDRate
        repoRate.ReadOnly = True
        repoUnit.IsVisible = False
        repoRate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoRate)

        Dim repoOrderQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoOrderQty.FormatString = ""
        repoOrderQty.HeaderText = "Requition Qty"
        repoOrderQty.Name = colDOrderQty
        repoOrderQty.ReadOnly = True
        repoOrderQty.Width = 80
        repoOrderQty.WrapText = True
        repoOrderQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoOrderQty)

        Dim repoAppQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAppQty.FormatString = ""
        repoAppQty.HeaderText = "Used in Purchase Order / SRN"
        repoAppQty.Name = colDApprovedQty
        repoAppQty.ReadOnly = True
        repoAppQty.Width = 100
        repoAppQty.WrapText = True
        repoAppQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoAppQty)

        Dim repoUnAppQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoUnAppQty.FormatString = ""
        repoUnAppQty.HeaderText = "Unapproved Qty"
        repoUnAppQty.Name = colDUnApprovedQty
        repoUnAppQty.ReadOnly = True
        repoUnAppQty.Width = 80
        repoUnAppQty.WrapText = True
        repoUnAppQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoUnAppQty)

        repoUnAppQty = New GridViewDecimalColumn()
        repoUnAppQty.FormatString = ""
        repoUnAppQty.HeaderText = "PO Close Qty"
        repoUnAppQty.Name = colDPOCloseQty
        repoUnAppQty.ReadOnly = True
        repoUnAppQty.Width = 80
        repoUnAppQty.WrapText = True
        repoUnAppQty.IsVisible = SettingIndendFreePOClose
        repoUnAppQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoUnAppQty)

        Dim repoPendingQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoPendingQty = New GridViewDecimalColumn()
        repoPendingQty.FormatString = ""
        repoPendingQty.HeaderText = "Pending"
        repoPendingQty.Name = colDPendingQty
        repoPendingQty.ReadOnly = True
        repoPendingQty.Width = 80
        repoPendingQty.WrapText = True
        repoPendingQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoPendingQty)

        Dim RepoSpecification As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        RepoSpecification.FormatString = ""
        RepoSpecification.HeaderText = "Specification"
        RepoSpecification.Name = colspecification
        RepoSpecification.Width = 60
        RepoSpecification.ReadOnly = True
        RepoSpecification.IsVisible = False
        RepoSpecification.VisibleInColumnChooser = False
        gv1.MasterTemplate.Columns.Add(RepoSpecification)

        Dim RepoRemarks As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        RepoRemarks.FormatString = ""
        RepoRemarks.HeaderText = "Remarks"
        RepoRemarks.Name = colRemarks
        RepoRemarks.Width = 60
        RepoRemarks.ReadOnly = True
        RepoRemarks.IsVisible = False
        RepoRemarks.VisibleInColumnChooser = False
        gv1.MasterTemplate.Columns.Add(RepoRemarks)


        Dim RepoCapacity As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        RepoCapacity.FormatString = ""
        RepoCapacity.HeaderText = "Capacity"
        RepoCapacity.Name = colCapacity
        RepoCapacity.Width = 60
        RepoCapacity.ReadOnly = True
        RepoCapacity.IsVisible = False
        RepoCapacity.VisibleInColumnChooser = False
        gv1.MasterTemplate.Columns.Add(RepoCapacity)

        Dim RepoMake As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        RepoMake.FormatString = ""
        RepoMake.HeaderText = "Make"
        RepoMake.Name = colMake
        RepoMake.Width = 60
        RepoMake.ReadOnly = True
        RepoMake.IsVisible = False
        RepoMake.VisibleInColumnChooser = False
        gv1.MasterTemplate.Columns.Add(RepoMake)

        Dim RepoModel As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        RepoModel.FormatString = ""
        RepoModel.HeaderText = "Model"
        RepoModel.Name = colModel
        RepoModel.Width = 60
        RepoModel.ReadOnly = True
        RepoModel.IsVisible = False
        RepoModel.VisibleInColumnChooser = False
        gv1.MasterTemplate.Columns.Add(RepoModel)

        gv1.AllowAddNewRow = False
        gv1.AllowDeleteRow = False
        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = True
        gv1.AllowRowReorder = True
        gv1.EnableSorting = False
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        gv1.MasterTemplate.ShowColumnHeaders = True
        gv1.EnableAlternatingRowColor = True
        gv1.EnableFiltering = True
        gv1.ShowFilteringRow = True
        gv1.TableElement.TableHeaderHeight = 40
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        btnCancelPressed()
    End Sub

    Private Sub btnOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOk.Click
        btnOKPressed()

    End Sub

    Sub btnCancelPressed()
        Me.Close()
    End Sub

    Sub btnOKPressed()
        ArrReturn = New List(Of clsRequistionDetail)
        Dim obj As clsRequistionDetail = Nothing
        For ii As Integer = 0 To gv1.RowCount - 1
            If (clsCommon.myCBool(gv1.Rows(ii).Cells(colDSelect).Value)) Then
                obj = New clsRequistionDetail()
                obj.Requisition_Id = clsCommon.myCstr(gv1.Rows(ii).Cells(colDCode).Value)
                obj.Item_Code = clsCommon.myCstr(gv1.Rows(ii).Cells(colDICode).Value)
                obj.Item_Desc = clsCommon.myCstr(gv1.Rows(ii).Cells(colDIName).Value)
                obj.Item_Cost = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDRate).Value)
                obj.Unit_Code = clsCommon.myCstr(gv1.Rows(ii).Cells(colDUnit).Value)
                'obj.Location = clsCommon.myCstr(gv1.Rows(ii).Cells(coldlo "Location").Value)
                'obj.LocationName = clsCommon.myCstr(gv1.Rows(ii).Cells("LocationName").Value)
                obj.Requisition_Qty = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDOrderQty).Value)
                obj.Balance_Qty = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDPendingQty).Value)
                obj.Specification = clsCommon.myCstr(gv1.Rows(ii).Cells(colspecification).Value)
                obj.Remarks = clsCommon.myCstr(gv1.Rows(ii).Cells(colRemarks).Value)
                obj.Capacity = clsCommon.myCstr(gv1.Rows(ii).Cells(colCapacity).Value)
                obj.Make = clsCommon.myCstr(gv1.Rows(ii).Cells(colMake).Value)
                obj.Model = clsCommon.myCstr(gv1.Rows(ii).Cells(colModel).Value)

                If (obj.Requisition_Qty > 0) Then
                    ArrReturn.Add(obj)
                End If
            End If
        Next

        If ArrReturn.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please select at least one non zero Pending Requition item")
        Else
            Me.Close()
        End If
    End Sub

    Private Sub FrmPendingRequistion_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.F5 Then
            btnOKPressed()
        ElseIf e.KeyCode = Keys.Escape Then
            btnCancelPressed()
        End If
    End Sub

    Private Sub gv1_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gv1.DoubleClick
        If gv1.CurrentColumn Is gv1.Columns(colDCode) Then
            Dim strCode As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colDCode).Value)
            Dim SelectStatus As Boolean = clsCommon.myCBool(gv1.CurrentRow.Cells(colDSelect).Value)
            For ii As Integer = 0 To gv1.Rows.Count - 1
                If clsCommon.CompairString(strCode, clsCommon.myCstr(gv1.Rows(ii).Cells(colDCode).Value)) = CompairStringResult.Equal Then
                    gv1.Rows(ii).Cells(colDSelect).Value = Not SelectStatus
                End If
            Next
        End If
    End Sub

    Private Sub gvHead_ValueChanging(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.ValueChangingEventArgs) Handles gvHead.ValueChanging
        If Not IsInsideLoadData Then
            If gvHead.CurrentColumn Is gvHead.Columns(colHSelect) Then
                Dim strLocationCode As String = clsCommon.myCstr(gvHead.CurrentRow.Cells(colHLOCATION_CODE).Value)
                If clsCommon.myLen(Str_Location) <= 0 Then
                    Str_Location = strLocationCode
                End If
                If clsCommon.CompairString(strLocationCode, Str_Location) = CompairStringResult.Equal OrElse settCreatePOFromMultipleLocation Then
                    Dim strCode As String = clsCommon.myCstr(gvHead.CurrentRow.Cells(colHCode).Value)
                    Dim strPOType As String = clsCommon.myCstr(gvHead.CurrentRow.Cells(colHPRType).Value)
                    If clsCommon.myLen(strCode) > 0 Then
                        '====check for PR Type,if diff. then pop-up msg==
                        For Each grow As GridViewRowInfo In gvHead.Rows
                            If clsCommon.myCBool(grow.Cells(colHSelect).Value) = True AndAlso clsCommon.myLen(grow.Cells(colHCode).Value) > 0 AndAlso clsCommon.CompairString(clsCommon.myCstr(grow.Cells(colHPRType).Value), strPOType) <> CompairStringResult.Equal Then
                                common.clsCommon.MyMessageBoxShow("Cannot select Current document.It is not For Type:" + clsCommon.myCstr(grow.Cells(colHPRType).Value))
                                e.Cancel = True
                                Exit Sub
                            End If
                        Next
                        '==================================
                        LoadDetailData(e.NewValue, strCode)
                    End If
                Else
                    common.clsCommon.MyMessageBoxShow("Location should be " + Str_Location)
                    e.Cancel = True
                End If
            End If
        End If
    End Sub

    Sub LoadDetailData(ByVal NewVal As Boolean, ByVal strCode As String)
        IsInsideLoadDataOfItem = True
        If NewVal Then
            For Each dr As DataRow In dtAllData.Rows
                If clsCommon.CompairString(strCode, clsCommon.myCstr(dr("Code"))) = CompairStringResult.Equal Then
                    Dim strVendorCode As String = clsCommon.myCstr(dr("Vendor"))
                    If clsCommon.myLen(VendorCode) <= 0 AndAlso clsCommon.myLen(strVendorCode) > 0 Then
                        VendorCode = strVendorCode
                        VendorName = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select vendor_name from tspl_vendor_master where vendor_code='" + VendorCode + "'"))
                    End If

                    gv1.Rows.AddNew()
                    If (clsCommon.myLen(strVendorCode) > 0 Xor clsCommon.CompairString(VendorCode, strVendorCode) = CompairStringResult.Equal) Then
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDSelect).Value = False
                    Else
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDSelect).Value = True
                    End If
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDCode).Value = clsCommon.myCstr(dr("code"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDICode).Value = clsCommon.myCstr(dr("ICode"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDIName).Value = clsCommon.myCstr(dr("IName"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDUnit).Value = clsCommon.myCstr(dr("Unit"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDVendor).Value = clsCommon.myCstr(dr("Vendor"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDVendorName).Value = clsCommon.myCstr(dr("VendorName"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDRate).Value = clsCommon.myCdbl(dr("Rate"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDOrderQty).Value = clsCommon.myCdbl(dr("RequitionQty"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDApprovedQty).Value = clsCommon.myCdbl(dr("POQty"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDUnApprovedQty).Value = clsCommon.myCdbl(dr("UnapprovedQty"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDPOCloseQty).Value = clsCommon.myCdbl(dr("POCloseQty"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDPendingQty).Value = clsCommon.myCdbl(dr("PedningQty"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colspecification).Value = clsCommon.myCstr(dr("Specification"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colRemarks).Value = clsCommon.myCstr(dr("Remarks"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colCapacity).Value = clsCommon.myCstr(dr("Capacity"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colMake).Value = clsCommon.myCstr(dr("Make"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colModel).Value = clsCommon.myCstr(dr("Model"))
                End If
            Next
        Else
            For ii As Integer = gv1.Rows.Count - 1 To 0 Step -1
                If clsCommon.CompairString(strCode, clsCommon.myCstr(gv1.Rows(ii).Cells(colDCode).Value)) = CompairStringResult.Equal Then
                    gv1.Rows.RemoveAt(ii)
                End If
            Next
        End If
        IsInsideLoadDataOfItem = False
    End Sub
    Dim IsInsideLoadDataOfItem As Boolean = False
    Private Sub gv1_ValueChanging(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.ValueChangingEventArgs) Handles gv1.ValueChanging
        If Not IsInsideLoadDataOfItem Then
            If gv1.CurrentColumn Is gv1.Columns(colDSelect) Then
                If e.NewValue Then
                    Dim strVendorCode As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colDVendor).Value)
                    Dim strVendoeName As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colDVendorName).Value)

                    '============Added by Rohit on June 04,2015=================
                    'Dim strLocationCode As String = clsCommon.myCstr(gv1.CurrentRow.Cells(col).Value)
                    'Dim strLocationName As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colDVendorName).Value)
                    '=============================================================
                    For Each grow As GridViewRowInfo In gv1.Rows
                        If grow.Index = gv1.CurrentRow.Index Then
                            Continue For
                        End If

                        If clsCommon.myLen(strVendorCode) > 0 AndAlso clsCommon.myLen(grow.Cells(colDVendor).Value) > 0 AndAlso clsCommon.myCBool(grow.Cells(colDSelect).Value) = True AndAlso clsCommon.CompairString(clsCommon.myCstr(grow.Cells(colDVendor).Value), strVendorCode) <> CompairStringResult.Equal Then
                            common.clsCommon.MyMessageBoxShow("Cannot select Current item.It is For Vendor:" + strVendoeName)
                            e.Cancel = True
                        End If
                    Next
                    'If clsCommon.myLen(strVendorCode) > 0 AndAlso Not clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colDVendor).Value), VendorCode) = CompairStringResult.Equal Then
                    '    common.clsCommon.MyMessageBoxShow("Cannot select Current item.It is For Vendor:" + strVendoeName)
                    '    e.Cancel = True
                    'End If
                End If
            End If
        End If
    End Sub
End Class

