Imports common
Imports System.Data.SqlClient

Public Class FrmPendingPOSchedule
    Inherits FrmMainTranScreen

#Region "Variables"
    Public VendorCode As String = Nothing
    Public VendorName As String = Nothing
    Public strCurrCode As String = Nothing
    Public strCurrDate As Date = Nothing
    Public PurchaseOrder_Type As String = Nothing
    Public Is_From_RGP As Boolean = False
    Public ArrReturn As List(Of clsPurchaseScheduleDetail) = Nothing

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

    
    Const colHSelect As String = "SELECT"
    Const colHCode As String = "CODE"
    Const colHDate As String = "DATE"
    Const colHVendorCode As String = "VENDOR"
    Const colHVendorName As String = "VENDORNAME"
    Const colHSchType As String = "SchType"
    Const colHpotype As String = "potype"
#End Region

    Private Sub FrmPendingRequistion_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim qry As String = ""
        Dim days As Integer = CInt(clsCommon.myCdbl(clsCommon.GetPrintDate(strCurrDate, "dd")))
        Dim Week_of_sch As Integer = clsPurchaseSchedule.GetNoOfWeekInMonth(strCurrDate, True)
        Dim month As Integer = CInt(clsCommon.myCdbl(clsCommon.GetPrintDate(strCurrDate, "MM")))
        Dim weekColumns As String = "0"
        Dim monthColumns As String = "0"
        Dim dayColumns As String = "0"

        If clsCommon.myLen(strCurrDate) > 0 Then
            For ii As Integer = 1 To days
                dayColumns = dayColumns + " + isnull(Day" + clsCommon.myCstr(ii) + "_Qty,0)"
            Next
            For ii As Integer = 1 To Week_of_sch
                weekColumns = weekColumns + " + isnull(Week" + clsCommon.myCstr(ii) + "_Qty,0)"
            Next
            For ii As Integer = 1 To month
                monthColumns = monthColumns + " + isnull(Month" + clsCommon.myCstr(ii) + "_Qty,0)"
            Next
        End If
        
        If clsCommon.myLen(VendorCode) > 0 Then
            Me.Text = Me.Text + " For " + VendorName
        End If

        Dim strcondition As String = ""
        Dim strGRNcondition As String = ""
        Dim strSRNcondition As String = ""

        If clsCommon.myLen(PurchaseOrder_Type) > 0 Then
            strcondition = " and TSPL_PO_SCH_HEAD.po_type='" + PurchaseOrder_Type + "'"
            strGRNcondition = " and tspl_grn_head.PurchaseOrder_Type='" + PurchaseOrder_Type + "'"
            strSRNcondition = " and tspl_srn_head.PurchaseOrder_Type='" + PurchaseOrder_Type + "'"
        End If

        qry = "select CAST(0 as bit) as Sel,code,max(Final.Tax_Group) as Tax_Group,max(TSPL_TAX_GROUP_MASTER.Tax_Group_Desc) as TaxGroupName,ICode,max(IName) as IName,MAX(IType) as IType,max(Unit)as Unit,max(Location) as Location,MAX(TSPL_LOCATION_MASTER.Location_Desc) as LocationName," & _
                " SUM(Qty* case when RI=1 then 1 else 0 end) as POQty," & _
                " SUM(Qty* case when RI=-1 then 1 else 0 end) as GRNQty," & _
                " SUM(Unapproved) as UnapprovedQty," & _
                " SUM((Qty *RI)- Unapproved-DamageQty) as PedningQty ,MAX(Rate) as Rate," & _
                " MAX(Final.TAX1_Rate) as TAX1_Rate,MAX(Final.TAX2_Rate) as TAX2_Rate,MAX(Final.TAX3_Rate) as TAX3_Rate,MAX(Final.TAX4_Rate) as TAX4_Rate,MAX(Final.TAX5_Rate) as TAX5_Rate,MAX(Final.TAX6_Rate) as TAX6_Rate,MAX(Final.TAX7_Rate) as TAX7_Rate,MAX(Final.TAX8_Rate) as TAX8_Rate,MAX(Final.TAX9_Rate) as TAX9_Rate,MAX(Final.TAX10_Rate) as TAX10_Rate, MAX(Final.TAX1_Amt) as TAX1_Amt,MAX(Final.TAX2_Amt) as TAX2_Amt,MAX(Final.TAX3_Amt) as TAX3_Amt,MAX(Final.TAX4_Amt) as TAX4_Amt,MAX(Final.TAX5_Amt) as TAX5_Amt,MAX(Final.TAX6_Amt) as TAX6_Amt,MAX(Final.TAX7_Amt) as TAX7_Amt,MAX(Final.TAX8_Amt) as TAX8_Amt,MAX(Final.TAX9_Amt) as TAX9_Amt,MAX(Final.TAX10_Amt) as TAX10_Amt,max(TransDate) as TransDate,max(Vendor) as Vendor,max(TSPL_VENDOR_MASTER.Vendor_Name) as VendorName,MRP,0 as Assessable,SUM(DamageQty) as DamageQty,max(AbatementRate) as AbatementRate from ( " + Environment.NewLine

        '==================daily schedule=================
        qry += " select TSPL_PO_SCH_DETAIL.document_code as Code,TSPL_PO_SCH_HEAD.Vendor_Code as Vendor,TSPL_PO_SCH_DETAIL.Item_Code as ICode,tspl_item_master.Item_Desc as IName,'' as IType,(" + dayColumns + ") as Qty,0 as Unapproved,TSPL_PO_SCH_DETAIL.Unit_Code as Unit,'' as Location,1 as RI,0 as Rate,1 as Chk,'' as Tax_Group,0 as TAX1_Rate,0 as TAX2_Rate,0 as TAX3_Rate,0 as TAX4_Rate,0 as TAX5_Rate,0 as TAX6_Rate,0 as TAX7_Rate,0 as TAX8_Rate,0 as TAX9_Rate,0 as TAX10_Rate,0 as TAX1_Amt,0 as TAX2_Amt,0 as TAX3_Amt,0 as TAX4_Amt,0 as TAX5_Amt,0 as TAX6_Amt,0 as TAX7_Amt,0 as TAX8_Amt,0 as TAX9_Amt,0 as TAX10_Amt ,TSPL_PO_SCH_HEAD.document_date as TransDate,0 AS Assessable,0 as MRP,0 as DamageQty,0 as AbatementRate from TSPL_PO_SCH_DETAIL left outer join TSPL_PO_SCH_HEAD on TSPL_PO_SCH_HEAD.document_code=TSPL_PO_SCH_DETAIL.document_code "
        qry += " left outer join tspl_item_master on tspl_item_master.item_code= TSPL_PO_SCH_DETAIL.item_code "
        qry += " where TSPL_PO_SCH_HEAD.is_post=1 and TSPL_PO_SCH_HEAD.schedule_type='D' " + strcondition + " "
        qry += " and month(TSPL_PO_SCH_HEAD.schedule_month)=month('" + clsCommon.GetPrintDate(strCurrDate, "dd/MMM/yyyy") + "') "
        qry += " and TSPL_PO_SCH_HEAD.document_code in (select document_code from TSPL_PO_SCH_HEAD where year(TSPL_PO_SCH_HEAD.schedule_month)=year('" + clsCommon.GetPrintDate(strCurrDate, "dd/MMM/yyyy") + "')) "
        If Not Is_From_RGP Then
            qry += " AND TSPL_PO_SCH_HEAD.po_type<>'J' " + Environment.NewLine
        End If
        If clsCommon.myLen(VendorCode) > 0 Then
            qry += " and TSPL_PO_SCH_HEAD.Vendor_Code='" + VendorCode + "'" + Environment.NewLine
        End If

        '=============weekly schedule-======================
        qry += " union all " + Environment.NewLine
        qry += " select TSPL_PO_SCH_DETAIL.document_code as Code,TSPL_PO_SCH_HEAD.Vendor_Code as Vendor,TSPL_PO_SCH_DETAIL.Item_Code as ICode,tspl_item_master.Item_Desc as IName,'' as IType,(" + weekColumns + ") as Qty,0 as Unapproved,TSPL_PO_SCH_DETAIL.Unit_Code as Unit,'' as Location,1 as RI,0 as Rate,1 as Chk,'' as Tax_Group,0 as TAX1_Rate,0 as TAX2_Rate,0 as TAX3_Rate,0 as TAX4_Rate,0 as TAX5_Rate,0 as TAX6_Rate,0 as TAX7_Rate,0 as TAX8_Rate,0 as TAX9_Rate,0 as TAX10_Rate,0 as TAX1_Amt,0 as TAX2_Amt,0 as TAX3_Amt,0 as TAX4_Amt,0 as TAX5_Amt,0 as TAX6_Amt,0 as TAX7_Amt,0 as TAX8_Amt,0 as TAX9_Amt,0 as TAX10_Amt ,TSPL_PO_SCH_HEAD.document_date as TransDate,0 AS Assessable,0 as MRP,0 as DamageQty,0 as AbatementRate from TSPL_PO_SCH_DETAIL left outer join TSPL_PO_SCH_HEAD on TSPL_PO_SCH_HEAD.document_code=TSPL_PO_SCH_DETAIL.document_code "
        qry += " left outer join tspl_item_master on tspl_item_master.item_code= TSPL_PO_SCH_DETAIL.item_code "
        qry += " where TSPL_PO_SCH_HEAD.is_post=1 and TSPL_PO_SCH_HEAD.schedule_type='W' " + strcondition + " "
        qry += " and month(TSPL_PO_SCH_HEAD.schedule_month)=month('" + clsCommon.GetPrintDate(strCurrDate, "dd/MMM/yyyy") + "') "
        qry += " and TSPL_PO_SCH_HEAD.document_code in (select document_code from TSPL_PO_SCH_HEAD where year(TSPL_PO_SCH_HEAD.schedule_month)=year('" + clsCommon.GetPrintDate(strCurrDate, "dd/MMM/yyyy") + "')) "
        If Not Is_From_RGP Then
            qry += " AND TSPL_PO_SCH_HEAD.po_type<>'J' " + Environment.NewLine
        End If
        If clsCommon.myLen(VendorCode) > 0 Then
            qry += " and TSPL_PO_SCH_HEAD.Vendor_Code='" + VendorCode + "'" + Environment.NewLine
        End If

        '================monthly schedule=======================
        qry += " union all " + Environment.NewLine
        qry += " select TSPL_PO_SCH_DETAIL.document_code as Code,TSPL_PO_SCH_HEAD.Vendor_Code as Vendor,TSPL_PO_SCH_DETAIL.Item_Code as ICode,tspl_item_master.Item_Desc as IName,'' as IType,(" + monthColumns + ") as Qty,0 as Unapproved,TSPL_PO_SCH_DETAIL.Unit_Code as Unit,'' as Location,1 as RI,0 as Rate,1 as Chk,'' as Tax_Group,0 as TAX1_Rate,0 as TAX2_Rate,0 as TAX3_Rate,0 as TAX4_Rate,0 as TAX5_Rate,0 as TAX6_Rate,0 as TAX7_Rate,0 as TAX8_Rate,0 as TAX9_Rate,0 as TAX10_Rate,0 as TAX1_Amt,0 as TAX2_Amt,0 as TAX3_Amt,0 as TAX4_Amt,0 as TAX5_Amt,0 as TAX6_Amt,0 as TAX7_Amt,0 as TAX8_Amt,0 as TAX9_Amt,0 as TAX10_Amt ,TSPL_PO_SCH_HEAD.document_date as TransDate,0 AS Assessable,0 as MRP,0 as DamageQty,0 as AbatementRate from TSPL_PO_SCH_DETAIL left outer join TSPL_PO_SCH_HEAD on TSPL_PO_SCH_HEAD.document_code=TSPL_PO_SCH_DETAIL.document_code "
        qry += " left outer join tspl_item_master on tspl_item_master.item_code= TSPL_PO_SCH_DETAIL.item_code "
        qry += " where TSPL_PO_SCH_HEAD.is_post=1 and TSPL_PO_SCH_HEAD.schedule_type='M' " + strcondition + " "
        qry += " and year(TSPL_PO_SCH_HEAD.schedule_month)=year('" + clsCommon.GetPrintDate(strCurrDate, "dd/MMM/yyyy") + "')"
        If Not Is_From_RGP Then
            qry += " AND TSPL_PO_SCH_HEAD.po_type<>'J' " + Environment.NewLine
        End If
        If clsCommon.myLen(VendorCode) > 0 Then
            qry += " and TSPL_PO_SCH_HEAD.Vendor_Code='" + VendorCode + "'" + Environment.NewLine
        End If
        '=====================================================================================

        qry += " union all  " + Environment.NewLine & _
        " select TSPL_GRN_DETAIL.Against_Schedule_Code as Code,null as Vendor,TSPL_GRN_DETAIL.Item_Code as ICode,tspl_item_master.Item_Desc as IName,'' as IType,TSPL_GRN_DETAIL.GRN_Qty as Qty,0 as Unapproved,TSPL_GRN_DETAIL.Unit_code as Unit,'' as Location,-1 as RI,0 as Rate,0 as Chk,'' as Tax_Group,0 as TAX1_Rate,0 as TAX2_Rate,0 as TAX3_Rate,0 as TAX4_Rate,0 as TAX5_Rate,0 as TAX6_Rate,0 as TAX7_Rate,0 as TAX8_Rate,0 as TAX9_Rate,0 as TAX10_Rate ,0 as  TAX1_Amt,0 as  TAX2_Amt,0 as  TAX3_Amt,0 as  TAX4_Amt,0 as  TAX5_Amt,0 as  TAX6_Amt,0 as  TAX7_Amt,0 as  TAX8_Amt,0 as  TAX9_Amt,0 as  TAX10_Amt,null as TransDate,0 as Assessable,0 as MRP,(isnull(TSPL_GRN_DETAIL.Leak_Qty,0)+isnull(TSPL_GRN_DETAIL.Burst_Qty,0) +isnull(TSPL_GRN_DETAIL.Short_Qty,0)) as DamageQty,0 as AbatementRate  from TSPL_GRN_DETAIL  left outer join TSPL_GRN_HEAD on TSPL_GRN_HEAD.GRN_No=TSPL_GRN_DETAIL.GRN_No left outer join tspl_item_master on tspl_item_master.item_code=TSPL_GRN_DETAIL.item_code where TSPL_GRN_HEAD.Status=1 and len(isnull(TSPL_GRN_DETAIL.Against_Schedule_Code,''))>0 and len(isnull(TSPL_GRN_DETAIL.Against_RGP_No,''))<=0 " + strGRNcondition + "  " + Environment.NewLine & _
        " union all  " + Environment.NewLine & _
        " select TSPL_GRN_DETAIL.Against_Schedule_Code as Code,null as Vendor,TSPL_GRN_DETAIL.Item_Code as ICode,tspl_item_master.Item_Desc as IName,'' as IType,0  as Qty,TSPL_GRN_DETAIL.GRN_Qty as Unapproved,TSPL_GRN_DETAIL.Unit_code as Unit,'' as Location,-1 as RI,0 as Rate,0 as Chk,'' as Tax_Group,0 as TAX1_Rate,0 as TAX2_Rate,0 as TAX3_Rate,0 as TAX4_Rate,0 as TAX5_Rate,0 as TAX6_Rate,0 as TAX7_Rate,0 as TAX8_Rate,0 as TAX9_Rate,0 as TAX10_Rate ,0 as  TAX1_Amt,0 as  TAX2_Amt,0 as  TAX3_Amt,0 as  TAX4_Amt,0 as  TAX5_Amt,0 as  TAX6_Amt,0 as  TAX7_Amt,0 as  TAX8_Amt,0 as  TAX9_Amt,0 as  TAX10_Amt,null as TransDate,0 as Assessable,0 as MRP,(isnull(TSPL_GRN_DETAIL.Leak_Qty,0)+isnull(TSPL_GRN_DETAIL.Burst_Qty,0) +isnull(TSPL_GRN_DETAIL.Short_Qty,0)) as DamageQty, 0 as AbatementRate  from TSPL_GRN_DETAIL  left outer join TSPL_GRN_HEAD on TSPL_GRN_HEAD.GRN_No=TSPL_GRN_DETAIL.GRN_No left outer join tspl_item_master on tspl_item_master.item_code=TSPL_GRN_DETAIL.item_code where TSPL_GRN_HEAD.Status=0 and TSPL_GRN_DETAIL.GRN_No not in ('" + strCurrCode + "') and len(isnull(TSPL_GRN_DETAIL.Against_Schedule_Code,''))>0 and len(isnull(TSPL_GRN_DETAIL.Against_RGP_No,''))<=0 " + strGRNcondition + "  " + Environment.NewLine & _
        " union all  " + Environment.NewLine & _
        " select TSPL_SRN_DETAIL.Against_Schedule_Code as Code,null as Vendor,TSPL_SRN_DETAIL.Item_Code as ICode,tspl_item_master.Item_Desc as IName,'' as IType,TSPL_SRN_DETAIL.SRN_Qty as Qty,0 as Unapproved,TSPL_SRN_DETAIL.Unit_code as Unit,'' as Location,-1 as RI,0 as Rate,0 as Chk,'' as Tax_Group,0 as TAX1_Rate,0 as TAX2_Rate,0 as TAX3_Rate,0 as TAX4_Rate,0 as TAX5_Rate,0 as TAX6_Rate,0 as TAX7_Rate,0 as TAX8_Rate,0 as TAX9_Rate,0 as TAX10_Rate ,0 as  TAX1_Amt,0 as  TAX2_Amt,0 as  TAX3_Amt,0 as  TAX4_Amt,0 as  TAX5_Amt,0 as  TAX6_Amt,0 as  TAX7_Amt,0 as  TAX8_Amt,0 as  TAX9_Amt,0 as  TAX10_Amt,null as TransDate,0 as Assessable,0 as MRP,(isnull(TSPL_SRN_DETAIL.Leak_Qty,0)+isnull(TSPL_SRN_DETAIL.Burst_Qty,0) +isnull(TSPL_SRN_DETAIL.Short_Qty,0)) as DamageQty,0 as AbatementRate  from TSPL_SRN_DETAIL  left outer join TSPL_SRN_HEAD on TSPL_SRN_HEAD.SRN_No=TSPL_SRN_DETAIL.SRN_No left outer join tspl_item_master on tspl_item_master.item_code=TSPL_SRN_DETAIL.item_code where TSPL_SRN_HEAD.Status=1 and len(isnull(TSPL_SRN_DETAIL.Against_Schedule_Code,''))>0 and len(isnull(TSPL_SRN_DETAIL.RGP_Id,''))<=0 " + strSRNcondition + "  " + Environment.NewLine & _
        " union all  " + Environment.NewLine & _
        " select TSPL_SRN_DETAIL.Against_Schedule_Code as Code,null as Vendor,TSPL_SRN_DETAIL.Item_Code as ICode,tspl_item_master.Item_Desc as IName,'' as IType,0  as Qty,TSPL_SRN_DETAIL.SRN_Qty as Unapproved,TSPL_SRN_DETAIL.Unit_code as Unit,'' as Location,-1 as RI,0 as Rate,0 as Chk,'' as Tax_Group,0 as TAX1_Rate,0 as TAX2_Rate,0 as TAX3_Rate,0 as TAX4_Rate,0 as TAX5_Rate,0 as TAX6_Rate,0 as TAX7_Rate,0 as TAX8_Rate,0 as TAX9_Rate,0 as TAX10_Rate ,0 as  TAX1_Amt,0 as  TAX2_Amt,0 as  TAX3_Amt,0 as  TAX4_Amt,0 as  TAX5_Amt,0 as  TAX6_Amt,0 as  TAX7_Amt,0 as  TAX8_Amt,0 as  TAX9_Amt,0 as  TAX10_Amt,null as TransDate,0 as Assessable,0 as MRP,(isnull(TSPL_SRN_DETAIL.Leak_Qty,0)+isnull(TSPL_SRN_DETAIL.Burst_Qty,0) +isnull(TSPL_SRN_DETAIL.Short_Qty,0)) as DamageQty, 0 as AbatementRate  from TSPL_SRN_DETAIL  left outer join TSPL_SRN_HEAD on TSPL_SRN_HEAD.SRN_No=TSPL_SRN_DETAIL.SRN_No left outer join tspl_item_master on tspl_item_master.item_code=TSPL_SRN_DETAIL.item_code where TSPL_SRN_HEAD.Status=0 and TSPL_SRN_DETAIL.SRN_No not in ('" + strCurrCode + "') and len(isnull(TSPL_SRN_DETAIL.Against_Schedule_Code,''))>0 and len(isnull(TSPL_SRN_DETAIL.RGP_Id,''))<=0 " + strSRNcondition + "  " + Environment.NewLine & _
        " union all  " + Environment.NewLine
        If clsCommon.CompairString(PurchaseOrder_Type, "J") = CompairStringResult.Equal OrElse Is_From_RGP Then
            qry += " select TSPL_RGP_JOB_WORK_DETAIL.Against_Schedule_Code as Code,null as Vendor,TSPL_RGP_JOB_WORK_DETAIL.Item_Code as ICode,tspl_item_master.Item_Desc as IName,'' as IType,TSPL_RGP_JOB_WORK_DETAIL.rgp_qty as Qty,0 as Unapproved,TSPL_RGP_JOB_WORK_DETAIL.Unit_code as Unit,'' as Location,-1 as RI,0 as Rate,0 as Chk,'' as Tax_Group,0 as TAX1_Rate,0 as TAX2_Rate,0 as TAX3_Rate,0 as TAX4_Rate,0 as TAX5_Rate,0 as TAX6_Rate,0 as TAX7_Rate,0 as TAX8_Rate,0 as TAX9_Rate,0 as TAX10_Rate ,0 as  TAX1_Amt,0 as  TAX2_Amt,0 as  TAX3_Amt,0 as  TAX4_Amt,0 as  TAX5_Amt,0 as  TAX6_Amt,0 as  TAX7_Amt,0 as  TAX8_Amt,0 as  TAX9_Amt,0 as  TAX10_Amt,null as TransDate,0 as Assessable,0 as MRP,0 as DamageQty,0 as AbatementRate  from TSPL_RGP_JOB_WORK_DETAIL  left outer join TSPL_RGP_HEAD on TSPL_RGP_HEAD.RGP_No=TSPL_RGP_JOB_WORK_DETAIL.RGP_No left outer join tspl_item_master on tspl_item_master.item_code=TSPL_RGP_JOB_WORK_DETAIL.item_code where TSPL_RGP_HEAD.Status=1 and len(isnull(TSPL_RGP_JOB_WORK_DETAIL.Against_Schedule_Code,''))>0  " + Environment.NewLine & _
        " union all  " + Environment.NewLine & _
        " select TSPL_RGP_JOB_WORK_DETAIL.Against_Schedule_Code as Code,null as Vendor,TSPL_RGP_JOB_WORK_DETAIL.Item_Code as ICode,tspl_item_master.Item_Desc as IName,'' as IType,0  as Qty,TSPL_RGP_JOB_WORK_DETAIL.rgp_qty as Unapproved,TSPL_RGP_JOB_WORK_DETAIL.Unit_code as Unit,'' as Location,-1 as RI,0 as Rate,0 as Chk,'' as Tax_Group,0 as TAX1_Rate,0 as TAX2_Rate,0 as TAX3_Rate,0 as TAX4_Rate,0 as TAX5_Rate,0 as TAX6_Rate,0 as TAX7_Rate,0 as TAX8_Rate,0 as TAX9_Rate,0 as TAX10_Rate ,0 as  TAX1_Amt,0 as  TAX2_Amt,0 as  TAX3_Amt,0 as  TAX4_Amt,0 as  TAX5_Amt,0 as  TAX6_Amt,0 as  TAX7_Amt,0 as  TAX8_Amt,0 as  TAX9_Amt,0 as  TAX10_Amt,null as TransDate,0 as Assessable,0 as MRP,0 as DamageQty, 0 as AbatementRate  from TSPL_RGP_JOB_WORK_DETAIL  left outer join TSPL_RGP_HEAD on TSPL_RGP_HEAD.RGP_No=TSPL_RGP_JOB_WORK_DETAIL.RGP_No left outer join tspl_item_master on tspl_item_master.item_code=TSPL_RGP_JOB_WORK_DETAIL.item_code where TSPL_RGP_HEAD.Status=0 and TSPL_RGP_JOB_WORK_DETAIL.RGP_No not in ('" + strCurrCode + "') and len(isnull(TSPL_RGP_JOB_WORK_DETAIL.Against_Schedule_Code,''))>0   " + Environment.NewLine
        Else
            qry += " select TSPL_RGP_DETAIL.Against_Schedule_Code as Code,null as Vendor,TSPL_RGP_DETAIL.Item_Code as ICode,tspl_item_master.Item_Desc as IName,'' as IType,TSPL_RGP_DETAIL.rgp_qty as Qty,0 as Unapproved,TSPL_RGP_DETAIL.Unit_code as Unit,'' as Location,-1 as RI,0 as Rate,0 as Chk,'' as Tax_Group,0 as TAX1_Rate,0 as TAX2_Rate,0 as TAX3_Rate,0 as TAX4_Rate,0 as TAX5_Rate,0 as TAX6_Rate,0 as TAX7_Rate,0 as TAX8_Rate,0 as TAX9_Rate,0 as TAX10_Rate ,0 as  TAX1_Amt,0 as  TAX2_Amt,0 as  TAX3_Amt,0 as  TAX4_Amt,0 as  TAX5_Amt,0 as  TAX6_Amt,0 as  TAX7_Amt,0 as  TAX8_Amt,0 as  TAX9_Amt,0 as  TAX10_Amt,null as TransDate,0 as Assessable,0 as MRP,0 as DamageQty,0 as AbatementRate  from TSPL_RGP_DETAIL  left outer join TSPL_RGP_HEAD on TSPL_RGP_HEAD.RGP_No=TSPL_RGP_DETAIL.RGP_No left outer join tspl_item_master on tspl_item_master.item_code=TSPL_RGP_DETAIL.item_code where TSPL_RGP_HEAD.Status=1 and isnull(TSPL_RGP_HEAD.Against_JobWork,0)=0 and len(isnull(TSPL_RGP_DETAIL.Against_Schedule_Code,''))>0  " + Environment.NewLine & _
        " union all  " + Environment.NewLine & _
        " select TSPL_RGP_DETAIL.Against_Schedule_Code as Code,null as Vendor,TSPL_RGP_DETAIL.Item_Code as ICode,tspl_item_master.Item_Desc as IName,'' as IType,0  as Qty,TSPL_RGP_DETAIL.rgp_qty as Unapproved,TSPL_RGP_DETAIL.Unit_code as Unit,'' as Location,-1 as RI,0 as Rate,0 as Chk,'' as Tax_Group,0 as TAX1_Rate,0 as TAX2_Rate,0 as TAX3_Rate,0 as TAX4_Rate,0 as TAX5_Rate,0 as TAX6_Rate,0 as TAX7_Rate,0 as TAX8_Rate,0 as TAX9_Rate,0 as TAX10_Rate ,0 as  TAX1_Amt,0 as  TAX2_Amt,0 as  TAX3_Amt,0 as  TAX4_Amt,0 as  TAX5_Amt,0 as  TAX6_Amt,0 as  TAX7_Amt,0 as  TAX8_Amt,0 as  TAX9_Amt,0 as  TAX10_Amt,null as TransDate,0 as Assessable,0 as MRP,0 as DamageQty, 0 as AbatementRate  from TSPL_RGP_DETAIL  left outer join TSPL_RGP_HEAD on TSPL_RGP_HEAD.RGP_No=TSPL_RGP_DETAIL.RGP_No left outer join tspl_item_master on tspl_item_master.item_code=TSPL_RGP_DETAIL.item_code where TSPL_RGP_HEAD.Status=0 and isnull(TSPL_RGP_HEAD.Against_JobWork,0)=0 and TSPL_RGP_DETAIL.RGP_No not in ('" + strCurrCode + "') and len(isnull(TSPL_RGP_DETAIL.Against_Schedule_Code,''))>0  " + Environment.NewLine
        End If
        qry += " )Final left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=final.Vendor left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=Final.Location left outer join TSPL_TAX_GROUP_MASTER on TSPL_TAX_GROUP_MASTER.Tax_Group_Code=Final.Tax_Group and TSPL_TAX_GROUP_MASTER.Tax_Group_Type='P' group by Code,ICode,Unit,MRP having SUM(Chk)>0 and SUM((Qty *RI)-Unapproved-DamageQty) <>0 order by Code,ICode "


        dtAllData = clsDBFuncationality.GetDataTable(qry)
        If dtAllData Is Nothing OrElse dtAllData.Rows.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow("No item found for vendor " + VendorName + "")
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
                gvHead.Rows(gvHead.RowCount - 1).Cells(colHSchType).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select (case when Schedule_Type='M' then 'Monthly' else case when Schedule_Type='D' then 'Daily' else case when Schedule_Type='W' then 'Weekly' end end end) as sch_type from tspl_po_sch_head where document_code='" + clsCommon.myCstr(dr("Code")) + "'"))
                gvHead.Rows(gvHead.RowCount - 1).Cells(colHpotype).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select (case when po_type='L' then 'Domestic' else case when po_type='J' then 'Jobwork' else case when po_type='I' then 'Import' else case when po_type='S' then 'Workorder (Service PO)' end end end end) as po_type from tspl_po_sch_head where document_code='" + clsCommon.myCstr(dr("Code")) + "'"))
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
        repoCode.HeaderText = "PO Schedule No"
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

        repoDate = New GridViewTextBoxColumn()
        repoDate.FormatString = ""
        repoDate.HeaderText = "Schedule Type"
        repoDate.Name = colHSchType
        repoDate.Width = 70
        repoDate.ReadOnly = True
        gvHead.MasterTemplate.Columns.Add(repoDate)


        Dim repoVendor As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoVendor.FormatString = ""
        repoVendor.HeaderText = "Vendor"
        repoVendor.Name = colHVendorCode
        repoVendor.Width = 170
        repoVendor.ReadOnly = True
        gvHead.MasterTemplate.Columns.Add(repoVendor)

        Dim repoVendorName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoVendorName.FormatString = ""
        repoVendorName.HeaderText = "Vendor Name"
        repoVendorName.Name = colHVendorName
        repoVendorName.Width = 170
        repoVendorName.ReadOnly = True
        gvHead.MasterTemplate.Columns.Add(repoVendorName)

        repoVendorName = New GridViewTextBoxColumn()
        repoVendorName.FormatString = ""
        repoVendorName.HeaderText = "PO Type"
        repoVendorName.Name = colHpotype
        repoVendorName.Width = 70
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
        repoCode.HeaderText = "PO Schedule No"
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

        Dim repoUnit As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoUnit.FormatString = ""
        repoUnit.HeaderText = "Unit"
        repoUnit.Name = colDUnit
        repoUnit.Width = 60
        repoUnit.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoUnit)

        Dim repoOrderQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoOrderQty.FormatString = ""
        repoOrderQty.HeaderText = "Schedule Qty"
        repoOrderQty.Name = colDOrderQty
        repoOrderQty.ReadOnly = True
        repoOrderQty.Width = 80
        repoOrderQty.WrapText = True
        repoOrderQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoOrderQty)

        Dim repoAppQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAppQty.FormatString = ""
        repoAppQty.HeaderText = "Used In GRN/SRN"
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
        repoPendingQty.HeaderText = "Pending"
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
        Me.Close()
    End Sub

    Sub btnOKPressed()
        btnOk.Focus()
        If Not isAllowed() Then
            Exit Sub
        End If
        
        ArrReturn = New List(Of clsPurchaseScheduleDetail)
        Dim obj As clsPurchaseScheduleDetail = Nothing
        For ii As Integer = 0 To gv1.RowCount - 1
            If (clsCommon.myCBool(gv1.Rows(ii).Cells(colDSelect).Value)) Then
                obj = New clsPurchaseScheduleDetail()
                obj.Document_Code = clsCommon.myCstr(gv1.Rows(ii).Cells(colDCode).Value)
                obj.Item_Code = clsCommon.myCstr(gv1.Rows(ii).Cells(colDICode).Value)
                obj.Item_Name = clsCommon.myCstr(gv1.Rows(ii).Cells(colDIName).Value)
                obj.Item_Type = clsItemMaster.GetItemType(obj.Item_Code, Nothing)
                obj.Unit_Code = clsCommon.myCstr(gv1.Rows(ii).Cells(colDUnit).Value)
                obj.Balance_Qty = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDPendingQty).Value)
                obj.Schedule_Qty = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDOrderQty).Value)
                obj.PO_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select po_code from TSPL_PO_SCH_DETAIL where document_code='" + obj.Document_Code + "'"))
                If clsCommon.myLen(obj.PO_Code) > 0 Then
                    obj.PO_Date = clsCommon.myCDate(clsDBFuncationality.getSingleValue("select po_date from TSPL_PO_SCH_DETAIL where document_code='" + obj.Document_Code + "'"))
                    obj.PO_Qty = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select po_qty from TSPL_PO_SCH_DETAIL where document_code='" + obj.Document_Code + "'"))
                End If

                If (obj.Balance_Qty > 0) Then
                    ArrReturn.Add(obj)
                End If
            End If
        Next

        If ArrReturn.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "Please select at least one non zero Pending PO Schedule item", Me.Text)
        Else
            Me.Close()
        End If

    End Sub

    Private Function isAllowed() As Boolean

        Dim arrVendor As New List(Of String)
        Dim arrSchedule As New List(Of String)
        Dim arrPOType As New List(Of String)
        For ii As Integer = 0 To gvHead.RowCount - 1
            If clsCommon.myCBool(gvHead.Rows(ii).Cells(colHSelect).Value) Then
                Dim strCode As String = clsCommon.myCstr(gvHead.Rows(ii).Cells(colHVendorCode).Value)
                Dim strPOType As String = clsCommon.myCstr(gvHead.Rows(ii).Cells(colHpotype).Value)
                Dim strSchedule As String = clsCommon.myCstr(gvHead.Rows(ii).Cells(colHSchType).Value)
                VendorCode = strCode
                VendorName = clsCommon.myCstr(gvHead.Rows(ii).Cells(colHVendorName).Value)

                For jj As Integer = ii + 1 To gvHead.RowCount - 1
                    If clsCommon.myCBool(gvHead.Rows(jj).Cells(colHSelect).Value) Then
                        If clsCommon.CompairString(strCode, clsCommon.myCstr(gvHead.Rows(jj).Cells(colHVendorCode).Value)) <> CompairStringResult.Equal Then
                            arrVendor.Add(clsCommon.myCstr(gvHead.Rows(jj).Cells(colHVendorCode).Value))
                        End If
                        If clsCommon.CompairString(strPOType, clsCommon.myCstr(gvHead.Rows(jj).Cells(colHpotype).Value)) <> CompairStringResult.Equal Then
                            arrPOType.Add(clsCommon.myCstr(gvHead.Rows(jj).Cells(colHpotype).Value))
                        End If
                        If clsCommon.CompairString(strSchedule, clsCommon.myCstr(gvHead.Rows(jj).Cells(colHSchType).Value)) <> CompairStringResult.Equal Then
                            arrSchedule.Add(clsCommon.myCstr(gvHead.Rows(jj).Cells(colHSchType).Value))
                        End If
                    End If '===detail and head doc no cond.

                Next '==detail for loop

                If arrVendor.Count > 0 Then
                    clsCommon.MyMessageBoxShow(Me, "Item more than one vendor are not allowed.", Me.Text)
                    Return False
                End If
                If arrPOType.Count > 0 Then
                    clsCommon.MyMessageBoxShow(Me, "Item more than one type are not allowed.", Me.Text)
                    Return False
                End If
                If arrSchedule.Count > 0 Then
                    clsCommon.MyMessageBoxShow(Me, "Item more than one schedule type are not allowed.", Me.Text)
                    Return False
                End If

                Return True
            End If '==check status of head

        Next '===head for loop
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
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.text)
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
                    'gv1.Rows(gv1.Rows.Count - 1).Cells(colDIType).Value = clsCommon.myCstr(dr("IType"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDUnit).Value = clsCommon.myCstr(dr("Unit"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDOrderQty).Value = clsCommon.myCdbl(dr("POQty"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDApprovedQty).Value = clsCommon.myCdbl(dr("GRNQty"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDUnApprovedQty).Value = clsCommon.myCdbl(dr("UnapprovedQty"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDPendingQty).Value = clsCommon.myCdbl(dr("PedningQty"))
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

