Imports common
Imports System.Data.SqlClient

Public Class frmCSAPendingDO
    Inherits FrmMainTranScreen

#Region "Variables"
    Public MainFormID As String = clsUserMgtCode.frmCSATransfer
    Public VendorCode As String = Nothing
    Public VendorName As String = Nothing
    Public strCurrCode As String = Nothing
    Public strCurrDate As Date = Nothing
    Public ArrReturn As List(Of clsCSADeliveryOrderDetail) = Nothing
    Public ArrCSARequest As List(Of clsCSABookingDetail) = Nothing

    Dim dtAllData As DataTable = Nothing
    Dim IsInsideLoadData As Boolean = False

    Const colDSelect As String = "SELECT"
    Const colDCode As String = "CODE"
    Const colDICode As String = "ICODE"
    Const colDIName As String = "INAME"
    Const colDIType As String = "IType"
    Const colDUnit As String = "UNIT"
    Const colDOrderQty As String = "ORDERQTY"
    Const colDApprovedQty As String = "APPROVEDQTY"
    Const colDUnApprovedQty As String = "UNAPPROVEDQTY"
    Const colDPendingQty As String = "PendingQty"
    Const colDSchemeMainItem As String = "SchemeMainItem"


    Const colHSelect As String = "SELECT"
    Const colHCode As String = "CODE"
    Const colHDate As String = "DATE"
    Const colHVendorCode As String = "VENDOR"
    Const colHVendorName As String = "VENDORNAME"
#End Region

    Private Sub FrmPendingRequistion_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim qry As String = ""
        If clsCommon.CompairString(clsUserMgtCode.frmCSATransfer, MainFormID) = CompairStringResult.Equal Then
            Me.Text = "Pending Delivery Order"
            RadLabel12.Text = "Click on DO No. to select/unselect all items of same DO."
            If clsCommon.myLen(VendorCode) > 0 Then
                Me.Text = Me.Text + " For " + VendorName
            End If

            qry = "select CAST(0 as bit) as Sel,code,max(Final.Tax_Group) as Tax_Group,max(TSPL_TAX_GROUP_MASTER.Tax_Group_Desc) as TaxGroupName,max(Scheme_Code) as Scheme_Code,ICode,max(IName) as IName,MAX(IType) as IType,max(Unit)as Unit,max(Location) as Location,MAX(TSPL_LOCATION_MASTER.Location_Desc) as LocationName," & _
               " SUM(Qty* case when RI=1 then 1 else 0 end) as POQty," & _
               " SUM(Qty* case when RI=-1 then 1 else 0 end) as GRNQty," & _
               " SUM(Unapproved) as UnapprovedQty," & _
               " SUM((Qty *RI)- Unapproved-DamageQty) as PedningQty ,MAX(Rate) as Rate," & _
               " MAX(Final.TAX1_Rate) as TAX1_Rate,MAX(Final.TAX2_Rate) as TAX2_Rate,MAX(Final.TAX3_Rate) as TAX3_Rate,MAX(Final.TAX4_Rate) as TAX4_Rate,MAX(Final.TAX5_Rate) as TAX5_Rate,MAX(Final.TAX6_Rate) as TAX6_Rate,MAX(Final.TAX7_Rate) as TAX7_Rate,MAX(Final.TAX8_Rate) as TAX8_Rate,MAX(Final.TAX9_Rate) as TAX9_Rate,MAX(Final.TAX10_Rate) as TAX10_Rate, MAX(Final.TAX1_Amt) as TAX1_Amt,MAX(Final.TAX2_Amt) as TAX2_Amt,MAX(Final.TAX3_Amt) as TAX3_Amt,MAX(Final.TAX4_Amt) as TAX4_Amt,MAX(Final.TAX5_Amt) as TAX5_Amt,MAX(Final.TAX6_Amt) as TAX6_Amt,MAX(Final.TAX7_Amt) as TAX7_Amt,MAX(Final.TAX8_Amt) as TAX8_Amt,MAX(Final.TAX9_Amt) as TAX9_Amt,MAX(Final.TAX10_Amt) as TAX10_Amt,max(TransDate) as TransDate,max(Vendor) as Vendor,max(TSPL_customer_MASTER.customer_name) as VendorName,MRP,0 as Assessable,SUM(DamageQty) as DamageQty,max(AbatementRate) as AbatementRate from ( " & _
               " select (select top 1 csdel.scheme_code from TSPL_CSA_DO_DETAIL as csdel where csdel.DOC_no=TSPL_CSA_DO_DETAIL.doc_no and TSPL_CSA_DO_DETAIL.Item_Code=csdel.Scheme_Item_Code and TSPL_CSA_DO_DETAIL.UOM=csdel.Scheme_Item_UOM and TSPL_CSA_DO_DETAIL.Scheme_Type=csdel.Scheme_Type and csdel.Scheme_Applicable=0) as Scheme_Code,TSPL_CSA_DO_DETAIL.doc_no as Code,TSPL_CSA_DO_HEAD.cust_code as Vendor,TSPL_CSA_DO_DETAIL.Item_Code as ICode,tspl_item_master.Item_Desc as IName,'' as IType,isnull(TSPL_CSA_DO_DETAIL.qty,0) as Qty,0 as Unapproved,TSPL_CSA_DO_DETAIL.uom as Unit,'' as Location,1 as RI,0 as Rate,1 as Chk,'' as Tax_Group,0 as TAX1_Rate,0 as TAX2_Rate,0 as TAX3_Rate,0 as TAX4_Rate,0 as TAX5_Rate,0 as TAX6_Rate,0 as TAX7_Rate,0 as TAX8_Rate,0 as TAX9_Rate,0 as TAX10_Rate,0 as TAX1_Amt,0 as TAX2_Amt,0 as TAX3_Amt,0 as TAX4_Amt,0 as TAX5_Amt,0 as TAX6_Amt,0 as TAX7_Amt,0 as TAX8_Amt,0 as TAX9_Amt,0 as TAX10_Amt ,TSPL_CSA_DO_HEAD.doc_date as TransDate,0 AS Assessable,0 as MRP,0 as DamageQty,0 as AbatementRate from TSPL_CSA_DO_DETAIL left outer join TSPL_CSA_DO_HEAD on TSPL_CSA_DO_HEAD.doc_no=TSPL_CSA_DO_DETAIL.doc_no " & _
               " left outer join tspl_item_master on tspl_item_master.item_code= TSPL_CSA_DO_DETAIL.item_code " & _
               " where TSPL_CSA_DO_HEAD.is_post=1 and TSPL_CSA_DO_DETAIL.FOC <> 1  and isnull(TSPL_CSA_DO_HEAD.Short_Close,'N')='N' " + Environment.NewLine
            If clsCommon.myLen(VendorCode) > 0 Then
                qry += " and TSPL_CSA_DO_HEAD.cust_code='" + VendorCode + "'" + Environment.NewLine
            End If

            qry += " union all  " + Environment.NewLine & _
            " select '' as Scheme_Code,TSPL_CSA_TRANSFER_DETAIL.DELEVERY_ORDER_NO as Code,null as Vendor,TSPL_CSA_TRANSFER_DETAIL.Item_Code as ICode,tspl_item_master.Item_Desc as IName,'' as IType,isnull(TSPL_CSA_TRANSFER_DETAIL.Qty,0) as Qty,0 as Unapproved,TSPL_CSA_TRANSFER_DETAIL.Unit_code as Unit,'' as Location,-1 as RI,0 as Rate,0 as Chk,'' as Tax_Group,0 as TAX1_Rate,0 as TAX2_Rate,0 as TAX3_Rate,0 as TAX4_Rate,0 as TAX5_Rate,0 as TAX6_Rate,0 as TAX7_Rate,0 as TAX8_Rate,0 as TAX9_Rate,0 as TAX10_Rate ,0 as  TAX1_Amt,0 as  TAX2_Amt,0 as  TAX3_Amt,0 as  TAX4_Amt,0 as  TAX5_Amt,0 as  TAX6_Amt,0 as  TAX7_Amt,0 as  TAX8_Amt,0 as  TAX9_Amt,0 as  TAX10_Amt,null as TransDate,0 as Assessable,0 as MRP,0 as DamageQty,0 as AbatementRate from TSPL_CSA_TRANSFER_DETAIL left outer join TSPL_CSA_TRANSFER_HEAD on TSPL_CSA_TRANSFER_HEAD.doc_code=TSPL_CSA_TRANSFER_DETAIL.doc_code left outer join tspl_item_master on tspl_item_master.item_code=TSPL_CSA_TRANSFER_DETAIL.item_code where TSPL_CSA_TRANSFER_HEAD.Status=1 and len(isnull(TSPL_CSA_TRANSFER_DETAIL.DELEVERY_ORDER_NO,''))>0 and TSPL_CSA_TRANSFER_DETAIL.FOC<>'Y' " + Environment.NewLine & _
            " union all  " + Environment.NewLine & _
            " select '' as Scheme_Code,TSPL_CSA_TRANSFER_DETAIL.DELEVERY_ORDER_NO as Code,null as Vendor,TSPL_CSA_TRANSFER_DETAIL.Item_Code as ICode,tspl_item_master.Item_Desc as IName,'' as IType,0  as Qty,isnull(TSPL_CSA_TRANSFER_DETAIL.Qty,0) as Unapproved,TSPL_CSA_TRANSFER_DETAIL.Unit_code as Unit,'' as Location,-1 as RI,0 as Rate,0 as Chk,'' as Tax_Group,0 as TAX1_Rate,0 as TAX2_Rate,0 as TAX3_Rate,0 as TAX4_Rate,0 as TAX5_Rate,0 as TAX6_Rate,0 as TAX7_Rate,0 as TAX8_Rate,0 as TAX9_Rate,0 as TAX10_Rate ,0 as  TAX1_Amt,0 as  TAX2_Amt,0 as  TAX3_Amt,0 as  TAX4_Amt,0 as  TAX5_Amt,0 as  TAX6_Amt,0 as  TAX7_Amt,0 as  TAX8_Amt,0 as  TAX9_Amt,0 as  TAX10_Amt,null as TransDate,0 as Assessable,0 as MRP,0 as DamageQty, 0 as AbatementRate from TSPL_CSA_TRANSFER_DETAIL left outer join TSPL_CSA_TRANSFER_HEAD on TSPL_CSA_TRANSFER_HEAD.doc_code=TSPL_CSA_TRANSFER_DETAIL.doc_code left outer join tspl_item_master on tspl_item_master.item_code=TSPL_CSA_TRANSFER_DETAIL.item_code where TSPL_CSA_TRANSFER_HEAD.Status=0 and TSPL_CSA_TRANSFER_DETAIL.doc_code not in ('" + strCurrCode + "') and len(isnull(TSPL_CSA_TRANSFER_DETAIL.DELEVERY_ORDER_NO,''))>0 and TSPL_CSA_TRANSFER_DETAIL.FOC<>'Y' " + Environment.NewLine & _
            " )Final left outer join TSPL_customer_MASTER on TSPL_customer_MASTER.cust_code=final.Vendor left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=Final.Location left outer join TSPL_TAX_GROUP_MASTER on TSPL_TAX_GROUP_MASTER.Tax_Group_Code=Final.Tax_Group and TSPL_TAX_GROUP_MASTER.Tax_Group_Type='S' group by Code,ICode,Unit,MRP having SUM(Chk)>0 and SUM((Qty *RI)-Unapproved-DamageQty) <>0 order by Code,ICode "
        ElseIf clsCommon.CompairString(clsUserMgtCode.frmCSADeliveryOrder, MainFormID) = CompairStringResult.Equal Then
            Me.Text = "Pending CSA Request"
            RadLabel12.Text = "Click on Request No. to select/unselect all items of same Request."
            If clsCommon.myLen(VendorCode) > 0 Then
                Me.Text = Me.Text + " For " + VendorName
            End If

            qry = "select CAST(0 as bit) as Sel,code,max(Final.Tax_Group) as Tax_Group,max(TSPL_TAX_GROUP_MASTER.Tax_Group_Desc) as TaxGroupName,'' as Scheme_Code,ICode,max(IName) as IName,max(Unit)as Unit,max(Location) as Location,MAX(TSPL_LOCATION_MASTER.Location_Desc) as LocationName," & _
               " SUM(Qty* case when RI=1 then 1 else 0 end) as POQty," & _
               " SUM(Qty* case when RI=-1 then 1 else 0 end) as GRNQty," & _
               " SUM(Unapproved) as UnapprovedQty," & _
               " SUM((Qty *RI)- Unapproved-DamageQty) as PedningQty ,MAX(Rate) as Rate," & _
               " MAX(Final.TAX1_Rate) as TAX1_Rate,MAX(Final.TAX2_Rate) as TAX2_Rate,MAX(Final.TAX3_Rate) as TAX3_Rate,MAX(Final.TAX4_Rate) as TAX4_Rate,MAX(Final.TAX5_Rate) as TAX5_Rate,MAX(Final.TAX6_Rate) as TAX6_Rate,MAX(Final.TAX7_Rate) as TAX7_Rate,MAX(Final.TAX8_Rate) as TAX8_Rate,MAX(Final.TAX9_Rate) as TAX9_Rate,MAX(Final.TAX10_Rate) as TAX10_Rate, MAX(Final.TAX1_Amt) as TAX1_Amt,MAX(Final.TAX2_Amt) as TAX2_Amt,MAX(Final.TAX3_Amt) as TAX3_Amt,MAX(Final.TAX4_Amt) as TAX4_Amt,MAX(Final.TAX5_Amt) as TAX5_Amt,MAX(Final.TAX6_Amt) as TAX6_Amt,MAX(Final.TAX7_Amt) as TAX7_Amt,MAX(Final.TAX8_Amt) as TAX8_Amt,MAX(Final.TAX9_Amt) as TAX9_Amt,MAX(Final.TAX10_Amt) as TAX10_Amt,max(TransDate) as TransDate,max(Vendor) as Vendor,max(TSPL_customer_MASTER.customer_name) as VendorName,MRP,0 as Assessable,SUM(DamageQty) as DamageQty,max(AbatementRate) as AbatementRate from ( " & _
               " select TSPL_CSA_BOOKING_detail.booking_no as Code,TSPL_CSA_BOOKING_HEAD.csa_code as Vendor,TSPL_CSA_BOOKING_detail.Item_Code as ICode,tspl_item_master.Item_Desc as IName,isnull(TSPL_CSA_BOOKING_detail.book_qty,0) as Qty,0 as Unapproved,TSPL_CSA_BOOKING_detail.book_qty_uom as Unit,'' as Location,1 as RI,0 as Rate,1 as Chk,'' as Tax_Group,0 as TAX1_Rate,0 as TAX2_Rate,0 as TAX3_Rate,0 as TAX4_Rate,0 as TAX5_Rate,0 as TAX6_Rate,0 as TAX7_Rate,0 as TAX8_Rate,0 as TAX9_Rate,0 as TAX10_Rate,0 as TAX1_Amt,0 as TAX2_Amt,0 as TAX3_Amt,0 as TAX4_Amt,0 as TAX5_Amt,0 as TAX6_Amt,0 as TAX7_Amt,0 as TAX8_Amt,0 as TAX9_Amt,0 as TAX10_Amt ,TSPL_CSA_BOOKING_HEAD.booking_date as TransDate,0 AS Assessable,0 as MRP,0 as DamageQty,0 as AbatementRate from TSPL_CSA_BOOKING_detail left outer join TSPL_CSA_BOOKING_HEAD on TSPL_CSA_BOOKING_HEAD.booking_no=TSPL_CSA_BOOKING_detail.booking_no " & _
               " left outer join tspl_item_master on tspl_item_master.item_code= TSPL_CSA_BOOKING_detail.item_code " & _
               " where TSPL_CSA_BOOKING_HEAD.posted=1 and TSPL_CSA_BOOKING_HEAD.trans_type='Request' " + Environment.NewLine
            If clsCommon.myLen(VendorCode) > 0 Then
                qry += " and TSPL_CSA_BOOKING_HEAD.csa_code='" + VendorCode + "'" + Environment.NewLine
            End If
            If Not IsDBNull(strCurrDate) AndAlso clsCommon.myLen(strCurrDate) > 0 AndAlso IsDate(strCurrDate) Then
                qry += " and TSPL_CSA_BOOKING_HEAD.booking_date<='" + clsCommon.GetPrintDate(strCurrDate, "dd/MMM/yyyy") + "' "
            End If

            qry += " union all  " + Environment.NewLine & _
            " select TSPL_CSA_DO_detail.csa_request_no as Code,TSPL_CSA_DO_HEAD.cust_code as Vendor,TSPL_CSA_DO_detail.Item_Code as ICode,tspl_item_master.Item_Desc as IName,isnull(TSPL_CSA_DO_detail.Qty,0) as Qty,0 as Unapproved,TSPL_CSA_DO_detail.uom as Unit,'' as Location,-1 as RI,0 as Rate,0 as Chk,'' as Tax_Group,0 as TAX1_Rate,0 as TAX2_Rate,0 as TAX3_Rate,0 as TAX4_Rate,0 as TAX5_Rate,0 as TAX6_Rate,0 as TAX7_Rate,0 as TAX8_Rate,0 as TAX9_Rate,0 as TAX10_Rate ,0 as  TAX1_Amt,0 as  TAX2_Amt,0 as  TAX3_Amt,0 as  TAX4_Amt,0 as  TAX5_Amt,0 as  TAX6_Amt,0 as  TAX7_Amt,0 as  TAX8_Amt,0 as  TAX9_Amt,0 as  TAX10_Amt,null as TransDate,0 as Assessable,0 as MRP,0 as DamageQty,0 as AbatementRate from TSPL_CSA_DO_detail left outer join TSPL_CSA_DO_HEAD on TSPL_CSA_DO_HEAD.doc_no=TSPL_CSA_DO_detail.doc_no left outer join tspl_item_master on tspl_item_master.item_code=TSPL_CSA_DO_detail.item_code where TSPL_CSA_DO_HEAD.is_post='1' and len(isnull(TSPL_CSA_DO_Detail.csa_request_no,''))>0 and TSPL_CSA_DO_DETAIL.FOC <> 1 " + Environment.NewLine & _
            " union all  " + Environment.NewLine & _
            " select TSPL_CSA_DO_detail.csa_request_no as Code,TSPL_CSA_DO_HEAD.cust_code as Vendor,TSPL_CSA_DO_detail.Item_Code as ICode,tspl_item_master.Item_Desc as IName,0  as Qty,isnull(TSPL_CSA_DO_detail.Qty,0) as Unapproved,TSPL_CSA_DO_detail.uom as Unit,'' as Location,-1 as RI,0 as Rate,0 as Chk,'' as Tax_Group,0 as TAX1_Rate,0 as TAX2_Rate,0 as TAX3_Rate,0 as TAX4_Rate,0 as TAX5_Rate,0 as TAX6_Rate,0 as TAX7_Rate,0 as TAX8_Rate,0 as TAX9_Rate,0 as TAX10_Rate ,0 as  TAX1_Amt,0 as  TAX2_Amt,0 as  TAX3_Amt,0 as  TAX4_Amt,0 as  TAX5_Amt,0 as  TAX6_Amt,0 as  TAX7_Amt,0 as  TAX8_Amt,0 as  TAX9_Amt,0 as  TAX10_Amt,null as TransDate,0 as Assessable,0 as MRP,0 as DamageQty, 0 as AbatementRate from TSPL_CSA_DO_detail left outer join TSPL_CSA_DO_HEAD on TSPL_CSA_DO_HEAD.doc_no=TSPL_CSA_DO_detail.doc_no left outer join tspl_item_master on tspl_item_master.item_code=TSPL_CSA_DO_detail.item_code where isnull(TSPL_CSA_DO_HEAD.is_post,'0') <>'1' and TSPL_CSA_DO_detail.doc_no not in ('" + strCurrCode + "') and len(isnull(TSPL_CSA_DO_detail.csa_request_no,''))>0 and TSPL_CSA_DO_DETAIL.FOC <> 1 " + Environment.NewLine & _
            " )Final left outer join TSPL_customer_MASTER on TSPL_customer_MASTER.cust_code=final.Vendor left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=Final.Location left outer join TSPL_TAX_GROUP_MASTER on TSPL_TAX_GROUP_MASTER.Tax_Group_Code=Final.Tax_Group and TSPL_TAX_GROUP_MASTER.Tax_Group_Type='S' group by Code,ICode,Unit,MRP having SUM(Chk)>0 and SUM((Qty *RI)-Unapproved-DamageQty) <>0 order by Code,ICode "
        End If

        dtAllData = clsDBFuncationality.GetDataTable(qry)
        If dtAllData Is Nothing OrElse dtAllData.Rows.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow("No item found for customer " + VendorName + "")
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
            Dim strCode As String = ""
            strCode = clsCommon.myCstr(dr("Code"))

            If Not arr.Contains(strCode) Then
                arr.Add(strCode)
                gvHead.Rows.AddNew()
                gvHead.Rows(gvHead.RowCount - 1).Cells(colHSelect).Value = False
                gvHead.Rows(gvHead.RowCount - 1).Cells(colHCode).Value = clsCommon.myCstr(dr("Code"))
                gvHead.Rows(gvHead.RowCount - 1).Cells(colHDate).Value = clsCommon.myCstr(dr("TransDate"))
                gvHead.Rows(gvHead.RowCount - 1).Cells(colHVendorCode).Value = clsCommon.myCstr(dr("Vendor"))
                gvHead.Rows(gvHead.RowCount - 1).Cells(colHVendorName).Value = clsCommon.myCstr(dr("VendorName"))
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
        If clsCommon.CompairString(clsUserMgtCode.frmCSATransfer, MainFormID) = CompairStringResult.Equal Then
            repoCode.HeaderText = "Delivery No"
        ElseIf clsCommon.CompairString(clsUserMgtCode.frmCSADeliveryOrder, MainFormID) = CompairStringResult.Equal Then
            repoCode.HeaderText = "Request No"
        End If
        repoCode.Name = colHCode
        repoCode.Width = 170
        repoCode.ReadOnly = True
        gvHead.MasterTemplate.Columns.Add(repoCode)

        Dim repoDate As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoDate.FormatString = ""
        repoDate.HeaderText = "Date"
        repoDate.Name = colHDate
        repoDate.Width = 70
        repoDate.ReadOnly = True
        gvHead.MasterTemplate.Columns.Add(repoDate)

        Dim repoVendor As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoVendor.FormatString = ""
        repoVendor.HeaderText = "Customer"
        repoVendor.Name = colHVendorCode
        repoVendor.Width = 170
        repoVendor.ReadOnly = True
        gvHead.MasterTemplate.Columns.Add(repoVendor)

        Dim repoVendorName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoVendorName.FormatString = ""
        repoVendorName.HeaderText = "Customer Name"
        repoVendorName.Name = colHVendorName
        repoVendorName.Width = 170
        repoVendorName.ReadOnly = True
        gvHead.MasterTemplate.Columns.Add(repoVendorName)

        gvHead.ShowFilteringRow = True
        gvHead.EnableFiltering = True
        gvHead.AllowDeleteRow = False
        gvHead.AllowAddNewRow = False
        gvHead.ShowGroupPanel = False
        gvHead.AllowColumnReorder = True
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
        If clsCommon.CompairString(clsUserMgtCode.frmCSATransfer, MainFormID) = CompairStringResult.Equal Then
            repoCode.HeaderText = "Delivery No"
        ElseIf clsCommon.CompairString(clsUserMgtCode.frmCSADeliveryOrder, MainFormID) = CompairStringResult.Equal Then
            repoCode.HeaderText = "Request No"
        End If
        repoCode.Name = colDCode
        repoCode.Width = 180
        repoCode.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoCode)

        Dim repoICode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoICode.FormatString = ""
        repoICode.HeaderText = "Item Code"
        repoICode.Name = colDICode
        repoICode.Width = 100
        repoICode.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoICode)

        Dim repoIName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoIName.FormatString = ""
        repoIName.HeaderText = "Description"
        repoIName.Name = colDIName
        repoIName.Width = 180
        repoIName.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoIName)

        repoIName = New GridViewTextBoxColumn()
        repoIName.FormatString = ""
        repoIName.HeaderText = "Scheme code"
        repoIName.Name = colDSchemeMainItem
        repoIName.Width = 100
        repoIName.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoIName)

        Dim repoUnit As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoUnit.FormatString = ""
        repoUnit.HeaderText = "Unit"
        repoUnit.Name = colDUnit
        repoUnit.Width = 60
        repoUnit.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoUnit)

        Dim repoOrderQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoOrderQty.FormatString = ""
        If clsCommon.CompairString(clsUserMgtCode.frmCSATransfer, MainFormID) = CompairStringResult.Equal Then
            repoOrderQty.HeaderText = "DO Qty"
        ElseIf clsCommon.CompairString(clsUserMgtCode.frmCSADeliveryOrder, MainFormID) = CompairStringResult.Equal Then
            repoOrderQty.HeaderText = "Request Qty"
        End If
        repoOrderQty.Name = colDOrderQty
        repoOrderQty.ReadOnly = True
        repoOrderQty.Width = 80
        repoOrderQty.WrapText = True
        repoOrderQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoOrderQty)

        Dim repoAppQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAppQty.FormatString = ""
        repoAppQty.HeaderText = "Used Qty"
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

        Dim repoPendingQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoPendingQty = New GridViewDecimalColumn()
        repoPendingQty.FormatString = ""
        repoPendingQty.HeaderText = "Pending Qty"
        repoPendingQty.Name = colDPendingQty
        repoPendingQty.ReadOnly = True
        repoPendingQty.Width = 80
        repoPendingQty.WrapText = True
        repoPendingQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoPendingQty)

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
        clsERPFuncationality.closeForm(Me)
    End Sub

    Sub btnOKPressed()
        btnOk.Focus()
        If Not isAllowed() Then
            Exit Sub
        End If

        ArrReturn = New List(Of clsCSADeliveryOrderDetail)
        If clsCommon.CompairString(clsUserMgtCode.frmCSATransfer, MainFormID) = CompairStringResult.Equal Then
            Dim obj As clsCSADeliveryOrderDetail = Nothing
            For ii As Integer = 0 To gv1.RowCount - 1
                If (clsCommon.myCBool(gv1.Rows(ii).Cells(colDSelect).Value)) Then
                    obj = New clsCSADeliveryOrderDetail()
                    obj.Doc_No = clsCommon.myCstr(gv1.Rows(ii).Cells(colDCode).Value)
                    obj.icode = clsCommon.myCstr(gv1.Rows(ii).Cells(colDICode).Value)
                    obj.iname = clsCommon.myCstr(gv1.Rows(ii).Cells(colDIName).Value)
                    obj.uom = clsCommon.myCstr(gv1.Rows(ii).Cells(colDUnit).Value)
                    obj.Balance_Qty = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDPendingQty).Value)
                    obj.qty = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDOrderQty).Value)
                    obj.Scheme_Code = clsCommon.myCstr(gv1.Rows(ii).Cells(colDSchemeMainItem).Value)

                    obj.csa_type = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select csa_type from tspl_item_master where item_code='" + obj.icode + "'"))
                    obj.unit_rate = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select unit_rate from tspl_csa_do_detail where item_code='" + obj.icode + "' and isnull(FOC,0)=0 and doc_no='" + obj.Doc_No + "'"))
                    obj.toltalamt = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select total_amt from tspl_csa_do_detail where item_code='" + obj.icode + "' and isnull(FOC,0)=0 and doc_no='" + obj.Doc_No + "'"))
                    obj.tax = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select including_tax from tspl_csa_do_detail where item_code='" + obj.icode + "' and isnull(FOC,0)=0 and doc_no='" + obj.Doc_No + "'"))

                    If (obj.Balance_Qty > 0) Then
                        ArrReturn.Add(obj)
                    End If
                End If
            Next

            If ArrReturn.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Please select at least one non zero Pending DO item", Me.Text)
            Else
                clsERPFuncationality.closeForm(Me)
            End If
        ElseIf clsCommon.CompairString(clsUserMgtCode.frmCSADeliveryOrder, MainFormID) = CompairStringResult.Equal Then
            Dim obj As clsCSABookingDetail = Nothing
            ArrCSARequest = New List(Of clsCSABookingDetail)
            For ii As Integer = 0 To gv1.RowCount - 1
                If (clsCommon.myCBool(gv1.Rows(ii).Cells(colDSelect).Value)) Then
                    obj = New clsCSABookingDetail()
                    obj.BOOKING_NO = clsCommon.myCstr(gv1.Rows(ii).Cells(colDCode).Value)
                    obj.Itemcode = clsCommon.myCstr(gv1.Rows(ii).Cells(colDICode).Value)
                    obj.Itemname = clsCommon.myCstr(gv1.Rows(ii).Cells(colDIName).Value)
                    obj.BOOK_QTY_UOM = clsCommon.myCstr(gv1.Rows(ii).Cells(colDUnit).Value)
                    obj.Bal_Qty = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDPendingQty).Value)
                    obj.BOOK_QTY = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDOrderQty).Value)

                    obj.CSA_ITEM_TYPE = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select csa_type from tspl_item_master where item_code='" + obj.Itemcode + "'"))
                    obj.BOOK_Rate = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select book_rate from TSPL_CSA_BOOKING_DETAIL where item_code='" + obj.Itemcode + "' and booking_no='" + obj.BOOKING_NO + "'"))
                    obj.TAX_PAID = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select tax_paid from TSPL_CSA_BOOKING_DETAIL where item_code='" + obj.Itemcode + "' and booking_no='" + obj.BOOKING_NO + "'"))

                    If (obj.Bal_Qty > 0) Then
                        ArrCSARequest.Add(obj)
                    End If
                End If
            Next

            If ArrCSARequest.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Please select at least one non zero Pending DO item", Me.Text)
            Else
                clsERPFuncationality.closeForm(Me)
            End If
        End If
    End Sub

    Private Function isAllowed() As Boolean

        Dim arrVendor As New List(Of String)
        
        For ii As Integer = 0 To gvHead.RowCount - 1
            If clsCommon.myCBool(gvHead.Rows(ii).Cells(colHSelect).Value) Then
                Dim strCode As String = clsCommon.myCstr(gvHead.Rows(ii).Cells(colHVendorCode).Value)
                
                VendorCode = strCode
                VendorName = clsCommon.myCstr(gvHead.Rows(ii).Cells(colHVendorName).Value)

                For jj As Integer = ii + 1 To gvHead.RowCount - 1
                    If clsCommon.myCBool(gvHead.Rows(jj).Cells(colHSelect).Value) Then
                        If clsCommon.CompairString(strCode, clsCommon.myCstr(gvHead.Rows(jj).Cells(colHVendorCode).Value)) <> CompairStringResult.Equal Then
                            arrVendor.Add(clsCommon.myCstr(gvHead.Rows(jj).Cells(colHVendorCode).Value))
                        End If
                    End If '===detail and head doc no cond.

                Next '==detail for loop

                If arrVendor.Count > 0 Then
                    clsCommon.MyMessageBoxShow(Me, "Item more than one customer are not allowed.", Me.Text)
                    Return False
                End If

                Exit For
            End If '==check status of head

        Next '===head for loop

        If clsCommon.CompairString(clsUserMgtCode.frmCSATransfer, MainFormID) = CompairStringResult.Equal Then
            ''check only when screen open as Pending DO on Transfer
            Dim Excisable As String = ""
            Dim OldExcisable As New List(Of String)()
            OldExcisable = New List(Of String)
            For Each grow As GridViewRowInfo In gv1.Rows
                If clsCommon.myLen(grow.Cells(colDICode).Value) > 0 AndAlso clsCommon.myCBool(grow.Cells(colDSelect).Value) = True Then
                    Excisable = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select 1 from TSPL_ITEM_MASTER where Is_Tax_Exempted=2 and Item_Code ='" + clsCommon.myCstr(grow.Cells(colDICode).Value) + "'"))

                    For jj As Integer = grow.Index + 1 To gv1.Rows.Count - 1
                        If clsCommon.myLen(gv1.Rows(jj).Cells(colDICode).Value) > 0 AndAlso clsCommon.myCBool(gv1.Rows(jj).Cells(colDSelect).Value) = True Then
                            Dim xType As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select 1 from TSPL_ITEM_MASTER where Is_Tax_Exempted=2 and Item_Code ='" + clsCommon.myCstr(gv1.Rows(jj).Cells(colDICode).Value) + "'"))

                            If clsCommon.CompairString(xType, Excisable) <> CompairStringResult.Equal AndAlso Not OldExcisable.Contains(clsCommon.myCstr(gv1.Rows(jj).Cells(colDICode).Value)) Then
                                OldExcisable.Add(clsCommon.myCstr(gv1.Rows(jj).Cells(colDIName).Value))
                            End If
                        End If
                    Next

                    If OldExcisable IsNot Nothing AndAlso OldExcisable.Count > 0 Then
                        clsCommon.MyMessageBoxShow("" + clsCommon.GetMulcallString(OldExcisable) + " items are not of " + clsCommon.myCstr(IIf(Excisable = "1", "Excisable", "Non-Excisable")) + " type.")
                        Return False
                    End If

                    Exit For
                End If

            Next ''for cond.
        End If

        Return True
    End Function

    Private Sub FrmPendingRequistion_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.F5 Then
            btnOKPressed()
        ElseIf e.KeyCode = Keys.Escape Then
            btnCancelPressed()
        End If
    End Sub

    Private Sub gv1_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gv1.DoubleClick
        If gv1.CurrentCell Is gv1.Columns(colDCode) Then
            Dim strPONO As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colDCode).Value)
            Dim SelectStatus As Boolean = clsCommon.myCBool(gv1.CurrentRow.Cells(colDSelect).Value)
            For ii As Integer = 0 To gv1.Rows.Count - 1
                If clsCommon.CompairString(strPONO, clsCommon.myCstr(gv1.Rows(ii).Cells(colDCode).Value)) = CompairStringResult.Equal Then
                    gv1.Rows(ii).Cells(colDSelect).Value = Not SelectStatus
                End If
            Next
        End If
    End Sub

    Private Sub gvHead_ValueChanging(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.ValueChangingEventArgs) Handles gvHead.ValueChanging
        Try
            If Not IsInsideLoadData Then
                Dim strCode As String
                strCode = clsCommon.myCstr(gvHead.CurrentRow.Cells(colHCode).Value)

                If clsCommon.myLen(strCode) > 0 Then
                    LoadDetailData(e.NewValue, strCode)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub LoadDetailData(ByVal NewVal As Boolean, ByVal strCode As String)

        If NewVal Then
            For Each dr As DataRow In dtAllData.Rows
                If clsCommon.CompairString(strCode, clsCommon.myCstr(dr("Code"))) = CompairStringResult.Equal Then
                    gv1.Rows.AddNew()

                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDSelect).Value = True
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDCode).Value = clsCommon.myCstr(dr("code"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDICode).Value = clsCommon.myCstr(dr("ICode"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDIName).Value = clsCommon.myCstr(dr("IName"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDUnit).Value = clsCommon.myCstr(dr("Unit"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDOrderQty).Value = clsCommon.myCdbl(dr("POQty"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDApprovedQty).Value = clsCommon.myCdbl(dr("GRNQty"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDUnApprovedQty).Value = clsCommon.myCdbl(dr("UnapprovedQty"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDPendingQty).Value = clsCommon.myCdbl(dr("PedningQty"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDSchemeMainItem).Value = clsCommon.myCstr(dr("Scheme_Code"))
                End If

            Next
        Else
            For ii As Integer = gv1.Rows.Count - 1 To 0 Step -1

                If clsCommon.CompairString(strCode, clsCommon.myCstr(gv1.Rows(ii).Cells(colDCode).Value)) = CompairStringResult.Equal Then
                    gv1.Rows.RemoveAt(ii)
                End If

            Next
        End If

    End Sub

End Class

