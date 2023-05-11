Imports common
Imports System.Data
Imports System.Data.SqlClient
Public Class clsMRPAutoMobile

#Region "Variables"
    Public MRP_CODE As String = Nothing
    Public MRP_DATE As Date = Nothing
    Public MRP_REMARKS As String = Nothing
    Public MRP_DESCRIPTION As String = Nothing
    Public PROD_PLAN_CODE As String = Nothing
    Public PROD_PLAN_DESC As String = Nothing
    Public MRP_Location As String = Nothing
    Public Location_Desc As String = Nothing
    Public MRP_FROM As Date = Nothing
    Public MRP_TO As Date = Nothing
    Public POSTED As Boolean = Nothing
    Public Include_Stock As Integer = Nothing
    Public Include_Pending_PO As Integer = Nothing
    Public Include_Pending_QC As Integer = Nothing
    Public Include_Item_Level As Integer = Nothing
    Public Auto_Indent As Integer = Nothing
    Public Auto_PO As Integer = Nothing
    Public Consider_Open_PO As Integer
    Public Auto_Schedule_Open_PO As Integer

    Public ObjListMRPDetail As List(Of clsMRPAutoMobileDetail)
    '===========auto po=============
    Public AAuto_Vendor_Code As String = Nothing
    Public AAuto_Vendor_Name As String = Nothing
    Public AAuto_PO_Rate As Double = Nothing
    Public AAuto_Last_Rate As Double = Nothing
    Public AAuto_Avg_Rate As Double = Nothing
    Public Arr_Auto_Po As List(Of clsMRPAutoMobile) = Nothing
#End Region

    Public Shared Function GetFinder(ByVal whrCls As String, ByVal strCurrCode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = "SELECT MRP_CODE AS Code,MRP_DESCRIPTION AS Description,MRP_FROM AS [From],MRP_TO as [To]," & _
                            " MRP_REMARKS as [Remarks],MRP_LOCATION as [Location] FROM TSPL_MRP_HEAD "
        If clsCommon.myLen(whrCls) > 0 Then
            whrCls += " and trans_id='A_Mobile' "
        Else
            whrCls = " trans_id='A_Mobile' "
        End If
        str = clsCommon.ShowSelectForm("TSPL_MRP_HEAD", qry, "Code", whrCls, strCurrCode, "Code", isButtonClicked)

        Return clsCommon.myCstr(str)
    End Function

    Public Shared Function GetItemLevelQty(ByVal Type As String, ByVal Item_Code As String) As Double
        Dim qty As Double = Nothing
        Dim qry As String = ""
        If clsCommon.CompairString(Type, "Min") = CompairStringResult.Equal Then
            qry = "select min_level from TSPL_ITEM_REORDER_LEVEL_NEW where item_code='" + Item_Code + "'"
        ElseIf clsCommon.CompairString(Type, "Max") = CompairStringResult.Equal Then
            qry = "select max_level from TSPL_ITEM_REORDER_LEVEL_NEW where item_code='" + Item_Code + "'"
        ElseIf clsCommon.CompairString(Type, "ROL") = CompairStringResult.Equal Then
            qry = "select reorder_level from TSPL_ITEM_REORDER_LEVEL_NEW where item_code='" + Item_Code + "'"
        End If
        qty = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))

        Return qty
    End Function

    Public Shared Function GetPendingPOQty(ByVal Item_Code As String) As Double
        Dim qty As Double = Nothing
        Dim qry As String = "select SUM((Qty *RI)- Unapproved-DamageQty) as PedningQty from ( " + Environment.NewLine & _
                        " select TSPL_PURCHASE_ORDER_HEAD.SaleInvoiceNo,TSPL_PURCHASE_ORDER_DETAIL.Bin_No,TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No as Code,TSPL_PURCHASE_ORDER_HEAD.Vendor_Code as Vendor,TSPL_PURCHASE_ORDER_DETAIL.Item_Code as ICode,tspl_item_master.Item_Desc as IName,TSPL_PURCHASE_ORDER_DETAIL.Row_Type as IType,TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_Qty as Qty,0 as Unapproved,TSPL_PURCHASE_ORDER_DETAIL.Unit_Code as Unit,TSPL_PURCHASE_ORDER_DETAIL.Location as Location,1 as RI,TSPL_PURCHASE_ORDER_DETAIL.Item_Cost as Rate,1 as Chk,TSPL_PURCHASE_ORDER_HEAD.Tax_Group,TSPL_PURCHASE_ORDER_DETAIL.TAX1_Rate,TSPL_PURCHASE_ORDER_DETAIL.TAX2_Rate,TSPL_PURCHASE_ORDER_DETAIL.TAX3_Rate,TSPL_PURCHASE_ORDER_DETAIL.TAX4_Rate,TSPL_PURCHASE_ORDER_DETAIL.TAX5_Rate,TSPL_PURCHASE_ORDER_DETAIL.TAX6_Rate,TSPL_PURCHASE_ORDER_DETAIL.TAX7_Rate,TSPL_PURCHASE_ORDER_DETAIL.TAX8_Rate,TSPL_PURCHASE_ORDER_DETAIL.TAX9_Rate,TSPL_PURCHASE_ORDER_DETAIL.TAX10_Rate,TSPL_PURCHASE_ORDER_DETAIL.TAX1_Amt,TSPL_PURCHASE_ORDER_DETAIL.TAX2_Amt,TSPL_PURCHASE_ORDER_DETAIL.TAX3_Amt,TSPL_PURCHASE_ORDER_DETAIL.TAX4_Amt,TSPL_PURCHASE_ORDER_DETAIL.TAX5_Amt,TSPL_PURCHASE_ORDER_DETAIL.TAX6_Amt,TSPL_PURCHASE_ORDER_DETAIL.TAX7_Amt,TSPL_PURCHASE_ORDER_DETAIL.TAX8_Amt,TSPL_PURCHASE_ORDER_DETAIL.TAX9_Amt,TSPL_PURCHASE_ORDER_DETAIL.TAX10_Amt ,TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_Date as TransDate,ISNULL(TSPL_PURCHASE_ORDER_DETAIL.Assessable,0) AS Assessable,ISNULL(TSPL_PURCHASE_ORDER_DETAIL.MRP,0) as MRP,0 as DamageQty,TSPL_PURCHASE_ORDER_DETAIL.AbatementRate from TSPL_PURCHASE_ORDER_DETAIL left outer join TSPL_PURCHASE_ORDER_HEAD on TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No=TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No left outer join tspl_item_master on tspl_item_master.item_code=TSPL_PURCHASE_ORDER_DETAIL.item_code where TSPL_PURCHASE_ORDER_DETAIL.Status=0 and TSPL_PURCHASE_ORDER_HEAD.Status=1 and TSPL_PURCHASE_ORDER_HEAD.close_yn='N' " & _
        " union all  " + Environment.NewLine & _
        " select '' as SaleInvoiceNo,'' as Bin_No,TSPL_GRN_DETAIL.PO_ID as Code,null as Vendor,TSPL_GRN_DETAIL.Item_Code as ICode,tspl_item_master.Item_Desc as IName,'' as IType,TSPL_GRN_DETAIL.GRN_Qty as Qty,0 as Unapproved,TSPL_GRN_DETAIL.Unit_code as Unit,'' as Location,-1 as RI,0 as Rate,0 as Chk,'' as Tax_Group,0 as TAX1_Rate,0 as TAX2_Rate,0 as TAX3_Rate,0 as TAX4_Rate,0 as TAX5_Rate,0 as TAX6_Rate,0 as TAX7_Rate,0 as TAX8_Rate,0 as TAX9_Rate,0 as TAX10_Rate ,0 as  TAX1_Amt,0 as  TAX2_Amt,0 as  TAX3_Amt,0 as  TAX4_Amt,0 as  TAX5_Amt,0 as  TAX6_Amt,0 as  TAX7_Amt,0 as  TAX8_Amt,0 as  TAX9_Amt,0 as  TAX10_Amt,null as TransDate,isnull(TSPL_GRN_DETAIL.Assessable,0) as Assessable,isnull(TSPL_GRN_DETAIL.MRP,0) as MRP,(isnull(TSPL_GRN_DETAIL.Leak_Qty,0)+isnull(TSPL_GRN_DETAIL.Burst_Qty,0) +isnull(TSPL_GRN_DETAIL.Short_Qty,0)) as DamageQty,0 as AbatementRate  from TSPL_GRN_DETAIL  left outer join TSPL_GRN_HEAD on TSPL_GRN_HEAD.GRN_No=TSPL_GRN_DETAIL.GRN_No left outer join tspl_item_master on tspl_item_master.item_code=TSPL_GRN_DETAIL.item_code where TSPL_GRN_HEAD.Status=1 and len(isnull(TSPL_GRN_DETAIL.PO_ID,''))>0 and len(isnull(TSPL_GRN_DETAIL.Against_Schedule_Code,''))<=0 and len(isnull(TSPL_GRN_DETAIL.Against_RGP_No,''))<=0 " + Environment.NewLine & _
        " union all  " + Environment.NewLine & _
        " select '' as SaleInvoiceNo,''as BinNo, TSPL_GRN_DETAIL.PO_ID as Code,null as Vendor,TSPL_GRN_DETAIL.Item_Code as ICode,tspl_item_master.Item_Desc as IName,'' as IType,0  as Qty,TSPL_GRN_DETAIL.GRN_Qty as Unapproved,TSPL_GRN_DETAIL.Unit_code as Unit,'' as Location,-1 as RI,0 as Rate,0 as Chk,'' as Tax_Group,0 as TAX1_Rate,0 as TAX2_Rate,0 as TAX3_Rate,0 as TAX4_Rate,0 as TAX5_Rate,0 as TAX6_Rate,0 as TAX7_Rate,0 as TAX8_Rate,0 as TAX9_Rate,0 as TAX10_Rate ,0 as  TAX1_Amt,0 as  TAX2_Amt,0 as  TAX3_Amt,0 as  TAX4_Amt,0 as  TAX5_Amt,0 as  TAX6_Amt,0 as  TAX7_Amt,0 as  TAX8_Amt,0 as  TAX9_Amt,0 as  TAX10_Amt,null as TransDate,isnull(TSPL_GRN_DETAIL.Assessable,0) as Assessable,isnull(TSPL_GRN_DETAIL.MRP,0) as MRP,(isnull(TSPL_GRN_DETAIL.Leak_Qty,0)+isnull(TSPL_GRN_DETAIL.Burst_Qty,0) +isnull(TSPL_GRN_DETAIL.Short_Qty,0)) as DamageQty, 0 as AbatementRate  from TSPL_GRN_DETAIL  left outer join TSPL_GRN_HEAD on TSPL_GRN_HEAD.GRN_No=TSPL_GRN_DETAIL.GRN_No left outer join tspl_item_master on tspl_item_master.item_code=TSPL_GRN_DETAIL.item_code where TSPL_GRN_HEAD.Status=0 and len(isnull(TSPL_GRN_DETAIL.PO_ID,''))>0 and len(isnull(TSPL_GRN_DETAIL.Against_Schedule_Code,''))<=0 and len(isnull(TSPL_GRN_DETAIL.Against_RGP_No,''))<=0 " + Environment.NewLine & _
                " union all  " + Environment.NewLine & _
        " select '' as SaleInvoiceNo,'' as Bin_No,TSPL_PO_SCH_DETAIL.PO_code as Code,null as Vendor,TSPL_PO_SCH_DETAIL.Item_Code as ICode,TSPL_item_master.Item_Desc as IName,'' as IType,TSPL_PO_SCH_DETAIL.schedule_qty as Qty,0 as Unapproved,TSPL_PO_SCH_DETAIL.Unit_code as Unit,'' as Location,-1 as RI,0 as Rate,0 as Chk,'' as Tax_Group,0 as TAX1_Rate,0 as TAX2_Rate,0 as TAX3_Rate,0 as TAX4_Rate,0 as TAX5_Rate,0 as TAX6_Rate,0 as TAX7_Rate,0 as TAX8_Rate,0 as TAX9_Rate,0 as TAX10_Rate ,0 as  TAX1_Amt,0 as  TAX2_Amt,0 as  TAX3_Amt,0 as  TAX4_Amt,0 as  TAX5_Amt,0 as  TAX6_Amt,0 as  TAX7_Amt,0 as  TAX8_Amt,0 as  TAX9_Amt,0 as  TAX10_Amt,null as TransDate,0 as Assessable,0 as MRP,0 as DamageQty,0 as AbatementRate  from TSPL_PO_SCH_DETAIL  left outer join TSPL_PO_SCH_HEAD on TSPL_PO_SCH_HEAD.document_code=TSPL_PO_SCH_DETAIL.document_code left outer join tspl_item_master on tspl_item_master.item_code=TSPL_PO_SCH_DETAIL.item_code where TSPL_PO_SCH_HEAD.is_post=1 and len(isnull(TSPL_PO_SCH_DETAIL.PO_code,''))>0 " + Environment.NewLine & _
        " union all  " + Environment.NewLine & _
        " select '' as SaleInvoiceNo,''as BinNo, TSPL_PO_SCH_DETAIL.PO_Code as Code,null as Vendor,TSPL_PO_SCH_DETAIL.Item_Code as ICode,tspl_item_master.Item_Desc as IName,'' as IType,0  as Qty,TSPL_PO_SCH_DETAIL.schedule_qty as Unapproved,TSPL_PO_SCH_DETAIL.Unit_code as Unit,'' as Location,-1 as RI,0 as Rate,0 as Chk,'' as Tax_Group,0 as TAX1_Rate,0 as TAX2_Rate,0 as TAX3_Rate,0 as TAX4_Rate,0 as TAX5_Rate,0 as TAX6_Rate,0 as TAX7_Rate,0 as TAX8_Rate,0 as TAX9_Rate,0 as TAX10_Rate ,0 as  TAX1_Amt,0 as  TAX2_Amt,0 as  TAX3_Amt,0 as  TAX4_Amt,0 as  TAX5_Amt,0 as  TAX6_Amt,0 as  TAX7_Amt,0 as  TAX8_Amt,0 as  TAX9_Amt,0 as  TAX10_Amt,null as TransDate,0 as Assessable,0 as MRP,0 as DamageQty, 0 as AbatementRate  from TSPL_PO_SCH_DETAIL  left outer join TSPL_PO_SCH_HEAD on TSPL_PO_SCH_HEAD.document_code=TSPL_PO_SCH_DETAIL.document_code left outer join tspl_item_master on tspl_item_master.item_code=TSPL_PO_SCH_DETAIL.item_code  where TSPL_PO_SCH_HEAD.is_post=0 and len(isnull(TSPL_PO_SCH_DETAIL.PO_code,''))>0  " + Environment.NewLine & _
        " union all  " + Environment.NewLine & _
        " select '' as SaleInvoiceNo,'' as Bin_No,TSPL_RGP_JOB_WORK_DETAIL.PO_id as Code,null as Vendor,TSPL_RGP_JOB_WORK_DETAIL.Item_Code as ICode,TSPL_item_master.Item_Desc as IName,'' as IType,TSPL_RGP_JOB_WORK_DETAIL.rgp_qty as Qty,0 as Unapproved,TSPL_RGP_JOB_WORK_DETAIL.Unit_code as Unit,'' as Location,-1 as RI,0 as Rate,0 as Chk,'' as Tax_Group,0 as TAX1_Rate,0 as TAX2_Rate,0 as TAX3_Rate,0 as TAX4_Rate,0 as TAX5_Rate,0 as TAX6_Rate,0 as TAX7_Rate,0 as TAX8_Rate,0 as TAX9_Rate,0 as TAX10_Rate ,0 as  TAX1_Amt,0 as  TAX2_Amt,0 as  TAX3_Amt,0 as  TAX4_Amt,0 as  TAX5_Amt,0 as  TAX6_Amt,0 as  TAX7_Amt,0 as  TAX8_Amt,0 as  TAX9_Amt,0 as  TAX10_Amt,null as TransDate,0 as Assessable,0 as MRP,0 as DamageQty,0 as AbatementRate from TSPL_RGP_JOB_WORK_DETAIL left outer join tspl_rgp_head on tspl_rgp_head.rgp_no=TSPL_RGP_JOB_WORK_DETAIL.rgp_no left outer join tspl_item_master on tspl_item_master.item_code=TSPL_RGP_JOB_WORK_DETAIL.item_code where tspl_rgp_head.status=1 and len(isnull(TSPL_RGP_JOB_WORK_DETAIL.PO_id,''))>0 and len(isnull(TSPL_RGP_JOB_WORK_DETAIL.against_schedule_code,''))<=0  " + Environment.NewLine & _
        " union all  " + Environment.NewLine & _
        " select '' as SaleInvoiceNo,''as BinNo, TSPL_RGP_JOB_WORK_DETAIL.PO_id as Code,null as Vendor,TSPL_RGP_JOB_WORK_DETAIL.Item_Code as ICode,tspl_item_master.Item_Desc as IName,'' as IType,0  as Qty,TSPL_RGP_JOB_WORK_DETAIL.rgp_qty as Unapproved,TSPL_RGP_JOB_WORK_DETAIL.Unit_code as Unit,'' as Location,-1 as RI,0 as Rate,0 as Chk,'' as Tax_Group,0 as TAX1_Rate,0 as TAX2_Rate,0 as TAX3_Rate,0 as TAX4_Rate,0 as TAX5_Rate,0 as TAX6_Rate,0 as TAX7_Rate,0 as TAX8_Rate,0 as TAX9_Rate,0 as TAX10_Rate ,0 as  TAX1_Amt,0 as  TAX2_Amt,0 as  TAX3_Amt,0 as  TAX4_Amt,0 as  TAX5_Amt,0 as  TAX6_Amt,0 as  TAX7_Amt,0 as  TAX8_Amt,0 as  TAX9_Amt,0 as  TAX10_Amt,null as TransDate,0 as Assessable,0 as MRP,0 as DamageQty, 0 as AbatementRate from TSPL_RGP_JOB_WORK_DETAIL left outer join tspl_rgp_head on tspl_rgp_head.rgp_no=TSPL_RGP_JOB_WORK_DETAIL.rgp_no left outer join tspl_item_master on tspl_item_master.item_code=TSPL_RGP_JOB_WORK_DETAIL.item_code where tspl_rgp_head.status=0 and len(isnull(TSPL_RGP_JOB_WORK_DETAIL.PO_id,''))>0 and len(isnull(TSPL_RGP_JOB_WORK_DETAIL.against_schedule_code,''))<=0 " + Environment.NewLine & _
        " union all  " + Environment.NewLine & _
        " select '' as SaleInvoiceNo,'' as Bin_No,TSPL_RGP_DETAIL.PO_id as Code,null as Vendor,TSPL_RGP_DETAIL.Item_Code as ICode,TSPL_item_master.Item_Desc as IName,'' as IType,TSPL_RGP_DETAIL.rgp_qty as Qty,0 as Unapproved,TSPL_RGP_DETAIL.Unit_code as Unit,'' as Location,-1 as RI,0 as Rate,0 as Chk,'' as Tax_Group,0 as TAX1_Rate,0 as TAX2_Rate,0 as TAX3_Rate,0 as TAX4_Rate,0 as TAX5_Rate,0 as TAX6_Rate,0 as TAX7_Rate,0 as TAX8_Rate,0 as TAX9_Rate,0 as TAX10_Rate ,0 as  TAX1_Amt,0 as  TAX2_Amt,0 as  TAX3_Amt,0 as  TAX4_Amt,0 as  TAX5_Amt,0 as  TAX6_Amt,0 as  TAX7_Amt,0 as  TAX8_Amt,0 as  TAX9_Amt,0 as  TAX10_Amt,null as TransDate,0 as Assessable,0 as MRP,0 as DamageQty,0 as AbatementRate from TSPL_RGP_DETAIL left outer join tspl_rgp_head on tspl_rgp_head.rgp_no=TSPL_RGP_DETAIL.rgp_no left outer join tspl_item_master on tspl_item_master.item_code=TSPL_RGP_DETAIL.item_code where tspl_rgp_head.status=1 and isnull(tspl_rgp_head.Against_JobWork,0)=0 and len(isnull(TSPL_RGP_DETAIL.PO_id,''))>0 and len(isnull(TSPL_RGP_DETAIL.against_schedule_code,''))<=0  " + Environment.NewLine & _
    " union all  " + Environment.NewLine & _
    " select '' as SaleInvoiceNo,''as BinNo, TSPL_RGP_DETAIL.PO_id as Code,null as Vendor,TSPL_RGP_DETAIL.Item_Code as ICode,tspl_item_master.Item_Desc as IName,'' as IType,0  as Qty,TSPL_RGP_DETAIL.rgp_qty as Unapproved,TSPL_RGP_DETAIL.Unit_code as Unit,'' as Location,-1 as RI,0 as Rate,0 as Chk,'' as Tax_Group,0 as TAX1_Rate,0 as TAX2_Rate,0 as TAX3_Rate,0 as TAX4_Rate,0 as TAX5_Rate,0 as TAX6_Rate,0 as TAX7_Rate,0 as TAX8_Rate,0 as TAX9_Rate,0 as TAX10_Rate ,0 as  TAX1_Amt,0 as  TAX2_Amt,0 as  TAX3_Amt,0 as  TAX4_Amt,0 as  TAX5_Amt,0 as  TAX6_Amt,0 as  TAX7_Amt,0 as  TAX8_Amt,0 as  TAX9_Amt,0 as  TAX10_Amt,null as TransDate,0 as Assessable,0 as MRP,0 as DamageQty, 0 as AbatementRate from TSPL_RGP_DETAIL left outer join tspl_rgp_head on tspl_rgp_head.rgp_no=TSPL_RGP_DETAIL.rgp_no left outer join tspl_item_master on tspl_item_master.item_code=TSPL_RGP_DETAIL.item_code where tspl_rgp_head.status=0 and isnull(tspl_rgp_head.Against_JobWork,0)=0 and len(isnull(TSPL_RGP_DETAIL.PO_id,''))>0 and len(isnull(TSPL_RGP_DETAIL.against_schedule_code,''))<=0 " + Environment.NewLine
        qry += " )Final where final.icode='" + Item_Code + "'"
        qty = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))

        Return qty
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsMRPAutoMobile
        Try
            Return GetData(strCode, NavType, Nothing)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim issaved As Boolean = True
            issaved = issaved AndAlso DeleteData(strCode, trans)

            trans.Commit()
            Return issaved
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function DeleteData(ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Try
            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Code not found to Delete")
            End If
            Dim qry As String

            '' delete TSPL_MRP_DETAIL
            qry = "DELETE FROM TSPL_MRP_DETAIL WHERE MRP_CODE='" + strCode + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)


            '' delete TSPL_MRP_DETAIL
            qry = "DELETE FROM TSPL_MRP_PO_DETAIL WHERE MRP_CODE='" + strCode + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            '' delete TSPL_MRP_DETAIL
            qry = "DELETE FROM TSPL_MRP_SRN_DETAIL WHERE MRP_CODE='" + strCode + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)


            qry = "delete from TSPL_MRP_HEAD where MRP_CODE ='" + strCode + "' and trans_id='A_Mobile'"
            isSaved = isSaved And clsDBFuncationality.ExecuteNonQuery(qry, trans)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message.ToString())
        End Try
        Return isSaved
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsMRPAutoMobile
        Try
            Dim obj As New clsMRPAutoMobile()
            obj.ObjListMRPDetail = New List(Of clsMRPAutoMobileDetail)

            Dim qry As String = "SELECT TSPL_MRP_HEAD.*,TSPL_MF_PRODUCTION_PLAN_HEAD.DESCRIPTION AS PROD_PLAN_DESC,tspl_location_master.location_desc FROM TSPL_MRP_HEAD " & _
            " LEFT JOIN TSPL_MF_PRODUCTION_PLAN_HEAD ON TSPL_MRP_HEAD.PROD_PLAN_CODE=TSPL_MRP_HEAD.PROD_PLAN_CODE left outer join tspl_location_master on tspl_location_master.location_code=TSPL_MRP_HEAD.mrp_location " & _
            " where trans_id='A_Mobile' "

            Select Case NavType
                Case NavigatorType.First
                    qry += " AND MRP_CODE = (select MIN(MRP_CODE) from TSPL_MRP_HEAD where trans_id='A_Mobile')"
                Case NavigatorType.Last
                    qry += " AND MRP_CODE = (select Max(MRP_CODE) from TSPL_MRP_HEAD where trans_id='A_Mobile')"
                Case NavigatorType.Next
                    qry += " AND MRP_CODE = (select Min(MRP_CODE) from TSPL_MRP_HEAD where trans_id='A_Mobile' and MRP_CODE>'" + strCode + "')"
                Case NavigatorType.Previous
                    qry += " AND MRP_CODE = (select Max(MRP_CODE) from TSPL_MRP_HEAD where trans_id='A_Mobile' and MRP_CODE<'" + strCode + "')"
                Case NavigatorType.Current
                    qry += " AND MRP_CODE = '" + strCode + "'"
            End Select
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then

                obj.MRP_CODE = dt.Rows(0)("MRP_CODE")
                obj.MRP_DESCRIPTION = dt.Rows(0)("MRP_DESCRIPTION")
                obj.MRP_REMARKS = clsCommon.myCstr(dt.Rows(0)("MRP_REMARKS"))
                obj.MRP_DATE = clsCommon.myCDate(dt.Rows(0)("MRP_DATE"))
                obj.MRP_FROM = clsCommon.myCDate(dt.Rows(0)("MRP_FROM"))
                obj.MRP_TO = clsCommon.myCDate(dt.Rows(0)("MRP_To"))
                obj.MRP_Location = clsCommon.myCstr(dt.Rows(0)("MRP_Location"))
                obj.Location_Desc = clsCommon.myCstr(dt.Rows(0)("location_desc"))
                obj.POSTED = clsCommon.myCstr(dt.Rows(0)("POSTED"))
                obj.PROD_PLAN_CODE = clsCommon.myCstr(dt.Rows(0)("PROD_PLAN_CODE"))
                obj.PROD_PLAN_DESC = clsCommon.myCstr(dt.Rows(0)("PROD_PLAN_DESC"))
                obj.Include_Stock = CInt(clsCommon.myCdbl(dt.Rows(0)("Include_Stock")))
                obj.Include_Pending_QC = CInt(clsCommon.myCdbl(dt.Rows(0)("Include_Pending_QC")))
                obj.Include_Pending_PO = CInt(clsCommon.myCdbl(dt.Rows(0)("Include_Pending_PO")))
                obj.Include_Item_Level = CInt(clsCommon.myCdbl(dt.Rows(0)("Include_Item_Level")))
                obj.Auto_Indent = CInt(clsCommon.myCdbl(dt.Rows(0)("Auto_Indent")))
                obj.Auto_PO = CInt(clsCommon.myCdbl(dt.Rows(0)("Auto_PO")))
                obj.Consider_Open_PO = CInt(clsCommon.myCdbl(dt.Rows(0)("Consider_Open_PO")))
                obj.Auto_Schedule_Open_PO = CInt(clsCommon.myCdbl(dt.Rows(0)("Auto_Schedule_Open_PO")))
                strCode = dt.Rows(0)("MRP_CODE")
            End If

            obj.ObjListMRPDetail = clsMRPAutoMobileDetail.GetMRPDetail(strCode, trans)

            Return obj
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        
    End Function

    Public Shared Function SaveData(ByVal obj As clsMRPAutoMobile, ByVal isNewEntry As Boolean, Optional ByVal strCode As String = "") As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim isSaved As Boolean = True
            isSaved = isSaved AndAlso SaveData(obj, isNewEntry, trans, strCode)

            trans.Commit()
            Return isSaved
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        Finally

        End Try
    End Function

    Public Shared Function SaveData(ByVal obj As clsMRPAutoMobile, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction, Optional ByVal strCode As String = "") As Boolean
        Try
            Dim isSaved As Boolean = True

            If isNewEntry Then
                obj.MRP_CODE = clsERPFuncationality.GetNextCode(trans, obj.MRP_DATE, clsDocType.MRP, "", obj.MRP_Location)
                strCode = obj.MRP_CODE
            End If
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "Standard Production", "MRP(STD)", obj.MRP_Location, obj.MRP_DATE, trans)
            Dim qry As String
            '' delete TSPL_MRP_DETAIL
            qry = "DELETE FROM TSPL_MRP_DETAIL WHERE MRP_CODE='" + obj.MRP_CODE + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            '' delete TSPL_MRP_DETAIL
            qry = "DELETE FROM TSPL_MRP_PO_DETAIL WHERE MRP_CODE='" + obj.MRP_CODE + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            '' delete TSPL_MRP_DETAIL
            qry = "DELETE FROM TSPL_MRP_SRN_DETAIL WHERE MRP_CODE='" + obj.MRP_CODE + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)


            Dim strDocNo As String = ""

            If (clsCommon.myLen(obj.MRP_CODE) <= 0) Then
                Throw New Exception("Error in Document Code Generation")
            End If

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "comp_code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "MRP_CODE", obj.MRP_CODE)
            clsCommon.AddColumnsForChange(coll, "MRP_DESCRIPTION", obj.MRP_DESCRIPTION)
            clsCommon.AddColumnsForChange(coll, "MRP_DATE", clsCommon.GetPrintDate(obj.MRP_DATE, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "MRP_FROM", clsCommon.GetPrintDate(obj.MRP_FROM, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "MRP_REMARKS", obj.MRP_REMARKS)
            clsCommon.AddColumnsForChange(coll, "MRP_Location", clsCommon.myCstr(obj.MRP_Location), True)
            clsCommon.AddColumnsForChange(coll, "POSTED", "0")
            clsCommon.AddColumnsForChange(coll, "PROD_PLAN_CODE", clsCommon.myCstr(obj.PROD_PLAN_CODE), True)
            clsCommon.AddColumnsForChange(coll, "Include_Stock", obj.Include_Stock)
            clsCommon.AddColumnsForChange(coll, "Include_Pending_QC", obj.Include_Pending_QC)
            clsCommon.AddColumnsForChange(coll, "Include_Pending_PO", obj.Include_Pending_PO)
            clsCommon.AddColumnsForChange(coll, "Include_Item_Level", obj.Include_Item_Level)
            clsCommon.AddColumnsForChange(coll, "Auto_PO", obj.Auto_PO)
            clsCommon.AddColumnsForChange(coll, "Auto_Indent", obj.Auto_Indent)
            clsCommon.AddColumnsForChange(coll, "Consider_Open_PO", obj.Consider_Open_PO)
            clsCommon.AddColumnsForChange(coll, "Auto_Schedule_Open_PO", obj.Auto_Schedule_Open_PO)
            clsCommon.AddColumnsForChange(coll, "Trans_Id", "A_Mobile")

            clsCommon.AddColumnsForChange(coll, "MRP_DAYS", "0")
            clsCommon.AddColumnsForChange(coll, "MRP_QTY", "0")
            clsCommon.AddColumnsForChange(coll, "MRP_TO", clsCommon.GetPrintDate(obj.MRP_TO, "dd/MMM/yyyy"))

            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))

            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))

                 isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MRP_HEAD", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MRP_HEAD", OMInsertOrUpdate.Update, " Trans_Id='A_Mobile' and TSPL_MRP_HEAD.MRP_CODE='" + obj.MRP_CODE + "'", trans)
            End If

            isSaved = isSaved AndAlso clsMRPAutoMobileDetail.SaveData(obj.MRP_CODE, obj.ObjListMRPDetail, trans)

            Return isSaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function PostData(ByVal strDocno As String, ByVal isAutoIndent As Boolean) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim issaved As Boolean = True
            issaved = issaved AndAlso PostData(strDocno, isAutoIndent, trans)

            trans.Commit()
            Return issaved
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function PostData(ByVal strDocNo As String, ByVal isAutoIndent As Boolean, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim issaved As Boolean = True

            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Code not found to Post")
            End If
            Dim strPostDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt")
            Dim obj As clsMRPAutoMobile = clsMRPAutoMobile.GetData(strDocNo, NavigatorType.Current, trans)

            If (obj Is Nothing OrElse clsCommon.myLen(obj.MRP_CODE) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If

            If isAutoIndent Then
                issaved = issaved AndAlso SavePurchaseIndent(obj, trans)
            End If

            Dim qry As String = ""
            qry = "update TSPL_MRP_DETAIL set Actual_Requird_Qty=dtl.ActualQty from ( " & _
                 " select MRP_CODE,Item_Code,Unit_Code,sum(case when ActualQty>0 then ActualQty else Qty end) as ActualQty  from tspl_mrp_po_detail  where mrp_code='" + strDocNo + "' " & _
                 " group by MRP_CODE,Item_Code,Unit_Code) dtl where TSPL_MRP_DETAIL.MRP_CODE=dtl.MRP_CODE and TSPL_MRP_DETAIL.Item_Code=dtl.Item_Code " & _
                 " and TSPL_MRP_DETAIL.RM_UNIT_CODE=dtl.Unit_Code"

            issaved = issaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "Update TSPL_MRP_HEAD set POSTED=1, Posting_Date='" + strPostDate + "',Modified_By='" + objCommonVar.CurrentUserCode + "' where MRP_CODE ='" + strDocNo + "' and trans_id='A_Mobile'"
            issaved = issaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            obj = Nothing

            Return issaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function


    Public Shared Function ReverseAndUnpost(ByVal strCode As String, ByVal isAutoIndent As Boolean) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            ReverseAndUnpost(strCode, isAutoIndent, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function


    Public Shared Function ReverseAndUnpost(ByVal strCode As String, ByVal isAutoIndent As Boolean, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim issaved As Boolean = True
            Dim qry As String = "select 1 from TSPL_MRP_HEAD where mrp_code='" + strCode + "' and POSTED=1"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                Throw New Exception("Transaction status should be posted.")
            End If

            If isAutoIndent Then

                qry = "select max(REQUISITION_ID) as REQUISITION_ID from TSPL_MRP_DETAIL where MRP_CODE ='" + strCode + "'"
                Dim StrRequisitionId As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))

                issaved = issaved AndAlso clsRequistionHead.DeleteData(StrRequisitionId, trans)

                qry = "Update TSPL_MRP_DETAIL set REQUISITION_ID=NULL where MRP_CODE ='" + strCode + "'"
                issaved = issaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            End If

            qry = "update TSPL_MRP_HEAD set POSTED=0,Posting_Date=null where mrp_code='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function


    Public Shared Function SavePurchaseIndent(ByVal obj As clsMRPAutoMobile, ByVal trans As SqlTransaction) As Boolean
        Dim objReq As New clsRequistionHead()
        Try
            Dim issaved As Boolean = True

            Dim qry As String = "select ROW_NUMBER() over (partition by tspl_item_master.item_type order by TSPL_MRP_DETAIL.Item_Code) as sno, TSPL_ITEM_MASTER.Item_Type,TSPL_MRP_DETAIL.* from TSPL_MRP_DETAIL left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_MRP_DETAIL.Item_Code "
            qry += " where TSPL_MRP_DETAIL.mrp_code='" + obj.MRP_CODE + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

            Dim line_no As Integer = 0
            Dim item_code As String = ""
            objReq = New clsRequistionHead()
            objReq.ArrTr = New List(Of clsRequistionDetail)

            If obj IsNot Nothing Then

                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    For Each dr As DataRow In dt.Rows
                        If clsCommon.myCdbl(dr("sno")) = 1 Then 'doing so that itemtype-wise pr made.
                            If objReq.ArrTr.Count > 0 Then
                                If objReq.SaveData(objReq, True, trans) Then
                                    qry = "Update TSPL_MRP_DETAIL set REQUISITION_ID='" + objReq.Requisition_Id + "' where MRP_CODE ='" + obj.MRP_CODE + "' and item_code in ('" + item_code + "')"
                                    issaved = issaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
                                End If
                            End If

                            item_code = ""
                            objReq = New clsRequistionHead()
                            objReq.ArrTr = New List(Of clsRequistionDetail)
                        End If

                        objReq.Requisition_Date = clsCommon.GETSERVERDATE(trans)
                        objReq.Cust_OrderNo = Nothing
                        objReq.Expire_Date = clsCommon.GETSERVERDATE(trans).AddYears(1)
                        objReq.Requisition_Date = clsCommon.GETSERVERDATE(trans)
                        objReq.Ref_No = Nothing
                        objReq.Description = obj.MRP_DESCRIPTION
                        objReq.Remarks = obj.MRP_REMARKS
                        objReq.Location = obj.MRP_Location
                        objReq.RQ_Detail_Total_Amt = 0
                        objReq.Mode_Of_Transport = "By Road"
                        objReq.Comments = Nothing
                        objReq.Is_Internal = "N"
                        objReq.Requisition_Type = "L"
                        objReq.Dept = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select top 1 Segment_code as Code from TSPL_GL_SEGMENT_CODE where Seg_No='3'", trans))
                        objReq.Dept_Desc = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select description from TSPL_GL_SEGMENT_CODE where Seg_No='3' and Segment_code='" + objReq.Dept + "'", trans))
                        objReq.Item_Type = clsCommon.myCstr(dr("item_type"))
                        objReq.Request_By = objCommonVar.CurrentUserCode
                        objReq.close_yn = "N"

                        Dim objReqDetail As New clsRequistionDetail()

                        objReqDetail.Line_No = line_no + 1
                        objReqDetail.Item_Code = clsCommon.myCstr(dr("item_code"))
                        objReqDetail.Item_Desc = clsItemMaster.GetItemName(clsCommon.myCstr(dr("item_code")), trans)
                        objReqDetail.Vendor_Code = Nothing
                        objReqDetail.Requisition_Qty = clsCommon.myCdbl(dr("Net_Requird_Qty"))
                        objReqDetail.Balance_Qty = Nothing
                        objReqDetail.Location = obj.MRP_Location
                        objReqDetail.Item_Cost = 0
                        objReqDetail.Status = "N"
                        objReqDetail.Order_No = Nothing
                        objReqDetail.Vendor_ItemNo = Nothing
                        objReqDetail.Item_Net_Amt = 0
                        objReqDetail.Unit_Code = clsCommon.myCstr(dr("RM_UNIT_CODE"))
                        objReqDetail.Remarks = clsCommon.myCstr(dr("Remarks"))
                        objReqDetail.Specification = Nothing

                        item_code = item_code + "','" + clsCommon.myCstr(dr("item_code"))

                        objReq.ArrTr.Add(objReqDetail)
                    Next
                End If
                

            End If 'if for obj cond. check

            If objReq IsNot Nothing Then
                If objReq.SaveData(objReq, True, trans) Then
                    qry = "Update TSPL_MRP_DETAIL set REQUISITION_ID='" + objReq.Requisition_Id + "' where MRP_CODE ='" + obj.MRP_CODE + "' and item_code in ('" + item_code + "')"
                    issaved = issaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
                End If
            End If

            item_code = Nothing
            Return issaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            objReq = Nothing
        End Try

    End Function

    Public Shared Function GetAutoPODetail(ByVal Item_Code As String) As clsMRPAutoMobile
        Dim obj As New clsMRPAutoMobile()
        obj.Arr_Auto_Po = New List(Of clsMRPAutoMobile)
        Dim qty As Double = Nothing
        Dim qry As String = "select final.vendor,final.rate,SUM((Qty *RI)- Unapproved-DamageQty) as PedningQty from ( " + Environment.NewLine & _
                        " select TSPL_PURCHASE_ORDER_HEAD.SaleInvoiceNo,TSPL_PURCHASE_ORDER_DETAIL.Bin_No,TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No as Code,TSPL_PURCHASE_ORDER_HEAD.Vendor_Code as Vendor,TSPL_PURCHASE_ORDER_DETAIL.Item_Code as ICode,tspl_item_master.Item_Desc as IName,TSPL_PURCHASE_ORDER_DETAIL.Row_Type as IType,TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_Qty as Qty,0 as Unapproved,TSPL_PURCHASE_ORDER_DETAIL.Unit_Code as Unit,TSPL_PURCHASE_ORDER_DETAIL.Location as Location,1 as RI,TSPL_PURCHASE_ORDER_DETAIL.Item_Cost as Rate,1 as Chk,TSPL_PURCHASE_ORDER_HEAD.Tax_Group,TSPL_PURCHASE_ORDER_DETAIL.TAX1_Rate,TSPL_PURCHASE_ORDER_DETAIL.TAX2_Rate,TSPL_PURCHASE_ORDER_DETAIL.TAX3_Rate,TSPL_PURCHASE_ORDER_DETAIL.TAX4_Rate,TSPL_PURCHASE_ORDER_DETAIL.TAX5_Rate,TSPL_PURCHASE_ORDER_DETAIL.TAX6_Rate,TSPL_PURCHASE_ORDER_DETAIL.TAX7_Rate,TSPL_PURCHASE_ORDER_DETAIL.TAX8_Rate,TSPL_PURCHASE_ORDER_DETAIL.TAX9_Rate,TSPL_PURCHASE_ORDER_DETAIL.TAX10_Rate,TSPL_PURCHASE_ORDER_DETAIL.TAX1_Amt,TSPL_PURCHASE_ORDER_DETAIL.TAX2_Amt,TSPL_PURCHASE_ORDER_DETAIL.TAX3_Amt,TSPL_PURCHASE_ORDER_DETAIL.TAX4_Amt,TSPL_PURCHASE_ORDER_DETAIL.TAX5_Amt,TSPL_PURCHASE_ORDER_DETAIL.TAX6_Amt,TSPL_PURCHASE_ORDER_DETAIL.TAX7_Amt,TSPL_PURCHASE_ORDER_DETAIL.TAX8_Amt,TSPL_PURCHASE_ORDER_DETAIL.TAX9_Amt,TSPL_PURCHASE_ORDER_DETAIL.TAX10_Amt ,TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_Date as TransDate,ISNULL(TSPL_PURCHASE_ORDER_DETAIL.Assessable,0) AS Assessable,ISNULL(TSPL_PURCHASE_ORDER_DETAIL.MRP,0) as MRP,0 as DamageQty,TSPL_PURCHASE_ORDER_DETAIL.AbatementRate from TSPL_PURCHASE_ORDER_DETAIL left outer join TSPL_PURCHASE_ORDER_HEAD on TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No=TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No left outer join tspl_item_master on tspl_item_master.item_code=TSPL_PURCHASE_ORDER_DETAIL.item_code where TSPL_PURCHASE_ORDER_DETAIL.Status=0 and TSPL_PURCHASE_ORDER_HEAD.Status=1 and TSPL_PURCHASE_ORDER_HEAD.close_yn='N' " & _
        " union all  " + Environment.NewLine & _
        " select '' as SaleInvoiceNo,'' as Bin_No,TSPL_GRN_DETAIL.PO_ID as Code,null as Vendor,TSPL_GRN_DETAIL.Item_Code as ICode,tspl_item_master.Item_Desc as IName,'' as IType,TSPL_GRN_DETAIL.GRN_Qty as Qty,0 as Unapproved,TSPL_GRN_DETAIL.Unit_code as Unit,'' as Location,-1 as RI,0 as Rate,0 as Chk,'' as Tax_Group,0 as TAX1_Rate,0 as TAX2_Rate,0 as TAX3_Rate,0 as TAX4_Rate,0 as TAX5_Rate,0 as TAX6_Rate,0 as TAX7_Rate,0 as TAX8_Rate,0 as TAX9_Rate,0 as TAX10_Rate ,0 as  TAX1_Amt,0 as  TAX2_Amt,0 as  TAX3_Amt,0 as  TAX4_Amt,0 as  TAX5_Amt,0 as  TAX6_Amt,0 as  TAX7_Amt,0 as  TAX8_Amt,0 as  TAX9_Amt,0 as  TAX10_Amt,null as TransDate,isnull(TSPL_GRN_DETAIL.Assessable,0) as Assessable,isnull(TSPL_GRN_DETAIL.MRP,0) as MRP,(isnull(TSPL_GRN_DETAIL.Leak_Qty,0)+isnull(TSPL_GRN_DETAIL.Burst_Qty,0) +isnull(TSPL_GRN_DETAIL.Short_Qty,0)) as DamageQty,0 as AbatementRate  from TSPL_GRN_DETAIL  left outer join TSPL_GRN_HEAD on TSPL_GRN_HEAD.GRN_No=TSPL_GRN_DETAIL.GRN_No left outer join tspl_item_master on tspl_item_master.item_code=TSPL_GRN_DETAIL.item_code where TSPL_GRN_HEAD.Status=1 and len(isnull(TSPL_GRN_DETAIL.PO_ID,''))>0 and len(isnull(TSPL_GRN_DETAIL.Against_Schedule_Code,''))<=0 and len(isnull(TSPL_GRN_DETAIL.Against_RGP_No,''))<=0 " + Environment.NewLine & _
        " union all  " + Environment.NewLine & _
        " select '' as SaleInvoiceNo,''as BinNo, TSPL_GRN_DETAIL.PO_ID as Code,null as Vendor,TSPL_GRN_DETAIL.Item_Code as ICode,tspl_item_master.Item_Desc as IName,'' as IType,0  as Qty,TSPL_GRN_DETAIL.GRN_Qty as Unapproved,TSPL_GRN_DETAIL.Unit_code as Unit,'' as Location,-1 as RI,0 as Rate,0 as Chk,'' as Tax_Group,0 as TAX1_Rate,0 as TAX2_Rate,0 as TAX3_Rate,0 as TAX4_Rate,0 as TAX5_Rate,0 as TAX6_Rate,0 as TAX7_Rate,0 as TAX8_Rate,0 as TAX9_Rate,0 as TAX10_Rate ,0 as  TAX1_Amt,0 as  TAX2_Amt,0 as  TAX3_Amt,0 as  TAX4_Amt,0 as  TAX5_Amt,0 as  TAX6_Amt,0 as  TAX7_Amt,0 as  TAX8_Amt,0 as  TAX9_Amt,0 as  TAX10_Amt,null as TransDate,isnull(TSPL_GRN_DETAIL.Assessable,0) as Assessable,isnull(TSPL_GRN_DETAIL.MRP,0) as MRP,(isnull(TSPL_GRN_DETAIL.Leak_Qty,0)+isnull(TSPL_GRN_DETAIL.Burst_Qty,0) +isnull(TSPL_GRN_DETAIL.Short_Qty,0)) as DamageQty, 0 as AbatementRate  from TSPL_GRN_DETAIL  left outer join TSPL_GRN_HEAD on TSPL_GRN_HEAD.GRN_No=TSPL_GRN_DETAIL.GRN_No left outer join tspl_item_master on tspl_item_master.item_code=TSPL_GRN_DETAIL.item_code where TSPL_GRN_HEAD.Status=0 and len(isnull(TSPL_GRN_DETAIL.PO_ID,''))>0 and len(isnull(TSPL_GRN_DETAIL.Against_Schedule_Code,''))<=0 and len(isnull(TSPL_GRN_DETAIL.Against_RGP_No,''))<=0 " + Environment.NewLine & _
                " union all  " + Environment.NewLine & _
        " select '' as SaleInvoiceNo,'' as Bin_No,TSPL_PO_SCH_DETAIL.PO_code as Code,null as Vendor,TSPL_PO_SCH_DETAIL.Item_Code as ICode,TSPL_item_master.Item_Desc as IName,'' as IType,TSPL_PO_SCH_DETAIL.schedule_qty as Qty,0 as Unapproved,TSPL_PO_SCH_DETAIL.Unit_code as Unit,'' as Location,-1 as RI,0 as Rate,0 as Chk,'' as Tax_Group,0 as TAX1_Rate,0 as TAX2_Rate,0 as TAX3_Rate,0 as TAX4_Rate,0 as TAX5_Rate,0 as TAX6_Rate,0 as TAX7_Rate,0 as TAX8_Rate,0 as TAX9_Rate,0 as TAX10_Rate ,0 as  TAX1_Amt,0 as  TAX2_Amt,0 as  TAX3_Amt,0 as  TAX4_Amt,0 as  TAX5_Amt,0 as  TAX6_Amt,0 as  TAX7_Amt,0 as  TAX8_Amt,0 as  TAX9_Amt,0 as  TAX10_Amt,null as TransDate,0 as Assessable,0 as MRP,0 as DamageQty,0 as AbatementRate  from TSPL_PO_SCH_DETAIL  left outer join TSPL_PO_SCH_HEAD on TSPL_PO_SCH_HEAD.document_code=TSPL_PO_SCH_DETAIL.document_code left outer join tspl_item_master on tspl_item_master.item_code=TSPL_PO_SCH_DETAIL.item_code where TSPL_PO_SCH_HEAD.is_post=1 and len(isnull(TSPL_PO_SCH_DETAIL.PO_code,''))>0 " + Environment.NewLine & _
        " union all  " + Environment.NewLine & _
        " select '' as SaleInvoiceNo,''as BinNo, TSPL_PO_SCH_DETAIL.PO_Code as Code,null as Vendor,TSPL_PO_SCH_DETAIL.Item_Code as ICode,tspl_item_master.Item_Desc as IName,'' as IType,0  as Qty,TSPL_PO_SCH_DETAIL.schedule_qty as Unapproved,TSPL_PO_SCH_DETAIL.Unit_code as Unit,'' as Location,-1 as RI,0 as Rate,0 as Chk,'' as Tax_Group,0 as TAX1_Rate,0 as TAX2_Rate,0 as TAX3_Rate,0 as TAX4_Rate,0 as TAX5_Rate,0 as TAX6_Rate,0 as TAX7_Rate,0 as TAX8_Rate,0 as TAX9_Rate,0 as TAX10_Rate ,0 as  TAX1_Amt,0 as  TAX2_Amt,0 as  TAX3_Amt,0 as  TAX4_Amt,0 as  TAX5_Amt,0 as  TAX6_Amt,0 as  TAX7_Amt,0 as  TAX8_Amt,0 as  TAX9_Amt,0 as  TAX10_Amt,null as TransDate,0 as Assessable,0 as MRP,0 as DamageQty, 0 as AbatementRate  from TSPL_PO_SCH_DETAIL  left outer join TSPL_PO_SCH_HEAD on TSPL_PO_SCH_HEAD.document_code=TSPL_PO_SCH_DETAIL.document_code left outer join tspl_item_master on tspl_item_master.item_code=TSPL_PO_SCH_DETAIL.item_code  where TSPL_PO_SCH_HEAD.is_post=0 and len(isnull(TSPL_PO_SCH_DETAIL.PO_code,''))>0  " + Environment.NewLine & _
        " union all  " + Environment.NewLine & _
        " select '' as SaleInvoiceNo,'' as Bin_No,TSPL_RGP_JOB_WORK_DETAIL.PO_id as Code,null as Vendor,TSPL_RGP_JOB_WORK_DETAIL.Item_Code as ICode,TSPL_item_master.Item_Desc as IName,'' as IType,TSPL_RGP_JOB_WORK_DETAIL.rgp_qty as Qty,0 as Unapproved,TSPL_RGP_JOB_WORK_DETAIL.Unit_code as Unit,'' as Location,-1 as RI,0 as Rate,0 as Chk,'' as Tax_Group,0 as TAX1_Rate,0 as TAX2_Rate,0 as TAX3_Rate,0 as TAX4_Rate,0 as TAX5_Rate,0 as TAX6_Rate,0 as TAX7_Rate,0 as TAX8_Rate,0 as TAX9_Rate,0 as TAX10_Rate ,0 as  TAX1_Amt,0 as  TAX2_Amt,0 as  TAX3_Amt,0 as  TAX4_Amt,0 as  TAX5_Amt,0 as  TAX6_Amt,0 as  TAX7_Amt,0 as  TAX8_Amt,0 as  TAX9_Amt,0 as  TAX10_Amt,null as TransDate,0 as Assessable,0 as MRP,0 as DamageQty,0 as AbatementRate from TSPL_RGP_JOB_WORK_DETAIL left outer join tspl_rgp_head on tspl_rgp_head.rgp_no=TSPL_RGP_JOB_WORK_DETAIL.rgp_no left outer join tspl_item_master on tspl_item_master.item_code=TSPL_RGP_JOB_WORK_DETAIL.item_code where tspl_rgp_head.status=1 and len(isnull(TSPL_RGP_JOB_WORK_DETAIL.PO_id,''))>0 and len(isnull(TSPL_RGP_JOB_WORK_DETAIL.against_schedule_code,''))<=0  " + Environment.NewLine & _
        " union all  " + Environment.NewLine & _
        " select '' as SaleInvoiceNo,''as BinNo, TSPL_RGP_JOB_WORK_DETAIL.PO_id as Code,null as Vendor,TSPL_RGP_JOB_WORK_DETAIL.Item_Code as ICode,tspl_item_master.Item_Desc as IName,'' as IType,0  as Qty,TSPL_RGP_JOB_WORK_DETAIL.rgp_qty as Unapproved,TSPL_RGP_JOB_WORK_DETAIL.Unit_code as Unit,'' as Location,-1 as RI,0 as Rate,0 as Chk,'' as Tax_Group,0 as TAX1_Rate,0 as TAX2_Rate,0 as TAX3_Rate,0 as TAX4_Rate,0 as TAX5_Rate,0 as TAX6_Rate,0 as TAX7_Rate,0 as TAX8_Rate,0 as TAX9_Rate,0 as TAX10_Rate ,0 as  TAX1_Amt,0 as  TAX2_Amt,0 as  TAX3_Amt,0 as  TAX4_Amt,0 as  TAX5_Amt,0 as  TAX6_Amt,0 as  TAX7_Amt,0 as  TAX8_Amt,0 as  TAX9_Amt,0 as  TAX10_Amt,null as TransDate,0 as Assessable,0 as MRP,0 as DamageQty, 0 as AbatementRate from TSPL_RGP_JOB_WORK_DETAIL left outer join tspl_rgp_head on tspl_rgp_head.rgp_no=TSPL_RGP_JOB_WORK_DETAIL.rgp_no left outer join tspl_item_master on tspl_item_master.item_code=TSPL_RGP_JOB_WORK_DETAIL.item_code where tspl_rgp_head.status=0 and len(isnull(TSPL_RGP_JOB_WORK_DETAIL.PO_id,''))>0 and len(isnull(TSPL_RGP_JOB_WORK_DETAIL.against_schedule_code,''))<=0 " + Environment.NewLine & _
        " union all  " + Environment.NewLine & _
        " select '' as SaleInvoiceNo,'' as Bin_No,TSPL_RGP_DETAIL.PO_id as Code,null as Vendor,TSPL_RGP_DETAIL.Item_Code as ICode,TSPL_item_master.Item_Desc as IName,'' as IType,TSPL_RGP_DETAIL.rgp_qty as Qty,0 as Unapproved,TSPL_RGP_DETAIL.Unit_code as Unit,'' as Location,-1 as RI,0 as Rate,0 as Chk,'' as Tax_Group,0 as TAX1_Rate,0 as TAX2_Rate,0 as TAX3_Rate,0 as TAX4_Rate,0 as TAX5_Rate,0 as TAX6_Rate,0 as TAX7_Rate,0 as TAX8_Rate,0 as TAX9_Rate,0 as TAX10_Rate ,0 as  TAX1_Amt,0 as  TAX2_Amt,0 as  TAX3_Amt,0 as  TAX4_Amt,0 as  TAX5_Amt,0 as  TAX6_Amt,0 as  TAX7_Amt,0 as  TAX8_Amt,0 as  TAX9_Amt,0 as  TAX10_Amt,null as TransDate,0 as Assessable,0 as MRP,0 as DamageQty,0 as AbatementRate from TSPL_RGP_DETAIL left outer join tspl_rgp_head on tspl_rgp_head.rgp_no=TSPL_RGP_DETAIL.rgp_no left outer join tspl_item_master on tspl_item_master.item_code=TSPL_RGP_DETAIL.item_code where tspl_rgp_head.status=1 and isnull(tspl_rgp_head.Against_JobWork,0)=0 and len(isnull(TSPL_RGP_DETAIL.PO_id,''))>0 and len(isnull(TSPL_RGP_DETAIL.against_schedule_code,''))<=0  " + Environment.NewLine & _
    " union all  " + Environment.NewLine & _
    " select '' as SaleInvoiceNo,''as BinNo, TSPL_RGP_DETAIL.PO_id as Code,null as Vendor,TSPL_RGP_DETAIL.Item_Code as ICode,tspl_item_master.Item_Desc as IName,'' as IType,0  as Qty,TSPL_RGP_DETAIL.rgp_qty as Unapproved,TSPL_RGP_DETAIL.Unit_code as Unit,'' as Location,-1 as RI,0 as Rate,0 as Chk,'' as Tax_Group,0 as TAX1_Rate,0 as TAX2_Rate,0 as TAX3_Rate,0 as TAX4_Rate,0 as TAX5_Rate,0 as TAX6_Rate,0 as TAX7_Rate,0 as TAX8_Rate,0 as TAX9_Rate,0 as TAX10_Rate ,0 as  TAX1_Amt,0 as  TAX2_Amt,0 as  TAX3_Amt,0 as  TAX4_Amt,0 as  TAX5_Amt,0 as  TAX6_Amt,0 as  TAX7_Amt,0 as  TAX8_Amt,0 as  TAX9_Amt,0 as  TAX10_Amt,null as TransDate,0 as Assessable,0 as MRP,0 as DamageQty, 0 as AbatementRate from TSPL_RGP_DETAIL left outer join tspl_rgp_head on tspl_rgp_head.rgp_no=TSPL_RGP_DETAIL.rgp_no left outer join tspl_item_master on tspl_item_master.item_code=TSPL_RGP_DETAIL.item_code where tspl_rgp_head.status=0 and isnull(tspl_rgp_head.Against_JobWork,0)=0 and len(isnull(TSPL_RGP_DETAIL.PO_id,''))>0 and len(isnull(TSPL_RGP_DETAIL.against_schedule_code,''))<=0 " + Environment.NewLine
        qry += " )Final where final.icode='" + Item_Code + "' and isnull(final.vendor,'')<>'' group by final.vendor,final.rate"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            For Each dr As DataRow In dt.Rows
                Dim objtr As New clsMRPAutoMobile()

                objtr.AAuto_Vendor_Code = clsCommon.myCstr(dr("vendor"))
                objtr.AAuto_Vendor_Name = clsVendorMaster.GetName(objtr.AAuto_Vendor_Code, Nothing)
                objtr.AAuto_PO_Rate = clsCommon.myCdbl(dr("rate"))
                objtr.AAuto_Last_Rate = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select top 1 item_cost from TSPL_PURCHASE_ORDER_DETAIL left outer join tspl_purchase_order_head on tspl_purchase_order_head.purchaseorder_no=tspl_purchase_order_detail.purchaseorder_no where item_code='" + Item_Code + "' and tspl_purchase_order_head.status='1' and tspl_purchase_order_head.vendor_code='" + clsCommon.myCstr(dr("vendor")) + "' order by tspl_purchase_order_head.purchaseorder_date desc"))
                objtr.AAuto_Avg_Rate = Math.Round(clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select sum(isnull(item_cost,0))/count(item_code) from TSPL_PURCHASE_ORDER_DETAIL left outer join tspl_purchase_order_head on tspl_purchase_order_head.purchaseorder_no=tspl_purchase_order_detail.purchaseorder_no where item_code='" + Item_Code + "' and tspl_purchase_order_head.status='1' and tspl_purchase_order_head.vendor_code='" + clsCommon.myCstr(dr("vendor")) + "'")), 2)

                obj.Arr_Auto_Po.Add(objtr)
            Next
        End If

        Return obj
    End Function
    Public Shared Function GetOpenPO(ByVal Item_Code As String, Optional trans As SqlTransaction = Nothing) As String
        Dim qry As String = "select Head.PurchaseOrder_No from TSPL_PURCHASE_ORDER_HEAD Head inner join TSPL_PURCHASE_ORDER_DETAIL dtl on Head.PurchaseOrder_No=dtl.PurchaseOrder_No" & _
                            " where dtl.Item_Code='" & Item_Code & "' and Head.Is_Open_PO=1 and Head.Status=1 "
        Dim dt As DataTable
        dt = clsDBFuncationality.GetDataTable(qry, trans)
        Dim strCode As String = ""
        For Each dr As DataRow In dt.Rows
            If dt.Rows.IndexOf(dr) = 0 Then
                strCode = clsCommon.myCstr(dr.Item("PurchaseOrder_No"))
            Else
                strCode = strCode & ";" & clsCommon.myCstr(dr.Item("PurchaseOrder_No"))
            End If
        Next
        Return strCode
    End Function
End Class

Public Class clsMRPAutoMobileDetail
#Region "Variables"
    Public MRP_CODE As String = Nothing
    Public Line_No As String = Nothing
    Public Item_Code As String = Nothing
    Public Item_Desc As String = Nothing
    Public RM_UNIT_CODE As String = Nothing
    Public Min_Qty As Double = Nothing
    Public Max_Qty As Double = Nothing
    Public ROL_Qty As Double = Nothing
    Public Total_Requird_Qty As Decimal = Nothing
    Public PO_Qty As Decimal = Nothing
    Public QC_Qty As Decimal = Nothing
    Public Stock_Qty As Decimal = Nothing
    Public Extra_Qty As Double = Nothing
    Public Net_Requird_Qty As Decimal = Nothing
    Public Actual_Requird_Qty As Decimal = 0
    Public Remarks As String = Nothing
#End Region


    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsMRPAutoMobileDetail), ByVal trans As SqlTransaction) As Boolean
        Try
            If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
                For Each obj As clsMRPAutoMobileDetail In Arr
                    Dim coll As New Hashtable()

                    clsCommon.AddColumnsForChange(coll, "MRP_CODE", strDocNo)
                    clsCommon.AddColumnsForChange(coll, "Line_No", obj.Line_No)
                    clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code)
                    clsCommon.AddColumnsForChange(coll, "RM_UNIT_CODE", obj.RM_UNIT_CODE, True)
                    clsCommon.AddColumnsForChange(coll, "Min_Qty", obj.Min_Qty)
                    clsCommon.AddColumnsForChange(coll, "Max_Qty", obj.Max_Qty)
                    clsCommon.AddColumnsForChange(coll, "ROL_Qty", obj.ROL_Qty)
                    clsCommon.AddColumnsForChange(coll, "QC_Qty", obj.QC_Qty)
                    clsCommon.AddColumnsForChange(coll, "Extra_Qty", obj.Extra_Qty)
                    clsCommon.AddColumnsForChange(coll, "PO_Qty", obj.PO_Qty)
                    clsCommon.AddColumnsForChange(coll, "Total_Requird_Qty", obj.Total_Requird_Qty)
                    clsCommon.AddColumnsForChange(coll, "Net_Requird_Qty", obj.Net_Requird_Qty)
                    clsCommon.AddColumnsForChange(coll, "Stock_Qty", obj.Stock_Qty)
                    clsCommon.AddColumnsForChange(coll, "Actual_Requird_Qty", obj.Actual_Requird_Qty)
                    clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks.Replace("'", "`"))
                    
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MRP_DETAIL", OMInsertOrUpdate.Insert, "TSPL_MRP_DETAIL.MRP_CODE='" + strDocNo + "' ", trans)
                Next

            End If

            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function GetMRPDetail(ByVal MRP_CODE As String, Optional ByVal trans As SqlTransaction = Nothing) As List(Of clsMRPAutoMobileDetail)
        Dim dt As New DataTable
        Try

            Dim stock_qty As Decimal = Nothing
            Dim qry As String = ""
            qry = "SELECT TSPL_MRP_DETAIL.*,tspl_item_master.item_desc as itmdesc FROM TSPL_MRP_DETAIL left outer join tspl_item_master on tspl_item_master.item_code=TSPL_MRP_DETAIL.item_code where TSPL_MRP_DETAIL.MRP_CODE='" & MRP_CODE & "'"
            dt = clsDBFuncationality.GetDataTable(qry, trans)

            Dim objtr As clsMRPAutoMobileDetail
            Dim ObjList As New List(Of clsMRPAutoMobileDetail)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                For Each dr As DataRow In dt.Rows
                    objtr = New clsMRPAutoMobileDetail()

                    objtr.MRP_CODE = clsCommon.myCstr(dr("MRP_CODE"))
                    objtr.Line_No = clsCommon.myCstr(dr("line_no"))
                    objtr.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                    objtr.Item_Desc = clsCommon.myCstr(dr("itmdesc"))
                    objtr.RM_UNIT_CODE = clsCommon.myCstr(dr("RM_UNIT_CODE"))
                    objtr.Min_Qty = clsCommon.myCdbl(dr("min_qty"))
                    objtr.Max_Qty = clsCommon.myCdbl(dr("max_qty"))
                    objtr.ROL_Qty = clsCommon.myCdbl(dr("rol_qty"))
                    objtr.QC_Qty = clsCommon.myCdbl(dr("qc_qty"))
                    objtr.PO_Qty = clsCommon.myCdbl(dr("PO_Qty"))
                    objtr.Total_Requird_Qty = clsCommon.myCdbl(dr("Total_Requird_Qty"))
                    objtr.Net_Requird_Qty = clsCommon.myCdbl(dr("Net_Requird_Qty"))
                    objtr.Extra_Qty = clsCommon.myCdbl(dr("extra_qty"))
                    objtr.Remarks = clsCommon.myCstr(dr("remarks"))
                    '=============get stock_qty from po detail table is posted otherwise from detail table,so that no chnge in calc. reflect on screen.
                    If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*) from tspl_mrp_po_detail where mrp_code='" + objtr.MRP_CODE + "' and item_code='" + objtr.Item_Code + "'", trans)) Then
                        stock_qty = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select top 1 stock_qty from tspl_mrp_po_detail where mrp_code='" + objtr.MRP_CODE + "' and item_code='" + objtr.Item_Code + "'", trans))
                        objtr.Stock_Qty = stock_qty
                    Else
                        objtr.Stock_Qty = clsCommon.myCdbl(dr("stock_qty"))
                    End If
                    objtr.Actual_Requird_Qty = clsCommon.myCdbl(dr("Actual_Requird_Qty"))
                    ObjList.Add(objtr)
                Next
            End If
            Return ObjList
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            dt = Nothing
        End Try

    End Function
    
End Class

Public Class clsMRPAutoMobilePODetail
#Region "variables"
    Public MRP_CODE As String = Nothing
    Public PurchaseOrder_No As String = Nothing
    Public PurchaseOrder_Date As DateTime = Nothing
    Public Bill_To_Location As String = Nothing
    Public Vendor_Code As String = Nothing
    Public Vendor_Name As String = Nothing
    Public Item_Code As String = Nothing
    Public Unit_Code As String = Nothing
    Public Net_Req_Qty As Double = Nothing
    Public MRP_PO_Rate As Double = Nothing
    Public PO_Last_Rate As Double = Nothing
    Public PO_Avg_Rate As Double = Nothing
    Public Item_Cost As Double = Nothing
    Public Qty As Double = Nothing
    Public Remarks As String = Nothing
    Public Stock_Qty As Decimal = Nothing

    Public OpenPONO As String = Nothing
    Public OpenPODate As String = Nothing
    Public MOQ As Double = 0
    Public SPQ As Double = 0
    Public SOB As Double = 0
    Public SOBQty As Double = 0
    Public ActualQty As Double = 0
    Public ScheduleType As String = Nothing

    Public Arr As List(Of clsMRPAutoMobilePODetail) = Nothing
#End Region

    Public Shared Function GetBOMOtherItems(ByVal ItemCode As String) As String
        Dim str As String = ""
        Dim qry As String = "select distinct (select ','''+bom_code+'''' from tspl_mf_bom_head where prod_item_code in ('" + ItemCode + "') for xml path('')) as bomcode"
        str = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))

        If clsCommon.myLen(str) > 0 AndAlso str.Substring(0, 1) = "," Then
            str = str.Substring(1, str.Length - 1)
        End If

        Return str
    End Function

    Public Shared Function SaveData(ByVal obj As clsMRPAutoMobilePODetail, ByVal MRP_CODE As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim isSaved As Boolean = True
            isSaved = isSaved AndAlso SaveData(obj, MRP_CODE, trans)

            trans.Commit()

            trans = clsDBFuncationality.GetTransactin()

            isSaved = isSaved AndAlso CreateAutoPO(MRP_CODE, trans)
            isSaved = isSaved AndAlso clsMRPAutoMobile.PostData(MRP_CODE, False, trans)

            trans.Commit()
            Return isSaved
        Catch ex As Exception
            trans.Rollback()

            Dim qry As String = "delete from tspl_mrp_po_detail where mrp_code='" + MRP_CODE + "'"
            clsDBFuncationality.ExecuteNonQuery(qry)
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function SaveData(ByVal obj As clsMRPAutoMobilePODetail, ByVal MRP_CODE As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim isSaved As Boolean = True

            Dim qry As String = "delete from tspl_mrp_po_detail where mrp_code='" + MRP_CODE + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            If obj IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                For Each objtr As clsMRPAutoMobilePODetail In obj.Arr
                    Dim coll As New Hashtable()

                    clsCommon.AddColumnsForChange(coll, "MRP_CODE", objtr.MRP_CODE)
                    'clsCommon.AddColumnsForChange(coll, "PurchaseOrder_No", objtr.PurchaseOrder_No)
                    'clsCommon.AddColumnsForChange(coll, "PurchaseOrder_Date", objtr.PurchaseOrder_Date)
                    clsCommon.AddColumnsForChange(coll, "Bill_To_Location", objtr.Bill_To_Location)
                    clsCommon.AddColumnsForChange(coll, "Vendor_Code", objtr.Vendor_Code)
                    clsCommon.AddColumnsForChange(coll, "Vendor_Name", objtr.Vendor_Name)
                    clsCommon.AddColumnsForChange(coll, "Item_Code", objtr.Item_Code, True)
                    clsCommon.AddColumnsForChange(coll, "Unit_Code", objtr.Unit_Code, True)
                    clsCommon.AddColumnsForChange(coll, "Net_Req_Qty", objtr.Net_Req_Qty)
                    clsCommon.AddColumnsForChange(coll, "MRP_PO_Rate", objtr.MRP_PO_Rate)
                    clsCommon.AddColumnsForChange(coll, "PO_Last_Rate", objtr.PO_Last_Rate)
                    clsCommon.AddColumnsForChange(coll, "PO_Avg_Rate", objtr.PO_Avg_Rate)
                    clsCommon.AddColumnsForChange(coll, "Item_Cost", objtr.Item_Cost)
                    clsCommon.AddColumnsForChange(coll, "Qty", objtr.Qty)
                    clsCommon.AddColumnsForChange(coll, "stock_qty", objtr.Stock_Qty)
                    clsCommon.AddColumnsForChange(coll, "Remarks", objtr.Remarks)

                    clsCommon.AddColumnsForChange(coll, "OpenPONO", objtr.OpenPONO, True)
                    clsCommon.AddColumnsForChange(coll, "OpenPODate", objtr.OpenPODate, True)
                    clsCommon.AddColumnsForChange(coll, "MOQ", objtr.MOQ)
                    clsCommon.AddColumnsForChange(coll, "SPQ", objtr.SPQ)
                    clsCommon.AddColumnsForChange(coll, "SOB", objtr.SOB)
                    clsCommon.AddColumnsForChange(coll, "SOBQty", objtr.SOBQty)
                    clsCommon.AddColumnsForChange(coll, "ActualQty", objtr.ActualQty)
                    clsCommon.AddColumnsForChange(coll, "ScheduleType", objtr.ScheduleType, True)

                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "tspl_mrp_po_detail", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If

            Return isSaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function CreateAutoPO(ByVal MRP_CODE As String, ByVal trans As SqlTransaction) As Boolean
        Dim obj As New clsPurchaseOrderHead()
        Dim objOpenPO As New clsPurchaseOrderHead()
        Try
            Dim isSaved As Boolean = True
            Dim objMRP As clsMRPAutoMobile = clsMRPAutoMobile.GetData(MRP_CODE, NavigatorType.Current, trans)
            Dim qry As String = "select final.* from (select row_number() over (partition by OpenPONO,tspl_item_master.item_type,tspl_mrp_po_detail.vendor_code,tspl_mrp_po_detail.ScheduleType order by tspl_mrp_po_detail.OpenPONO,tspl_item_master.item_type,tspl_mrp_po_detail.vendor_code,tspl_mrp_po_detail.ScheduleType) as sno,tspl_item_master.item_type,tspl_mrp_po_detail.* from tspl_mrp_po_detail left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_MRP_PO_DETAIL.Item_Code where mrp_code='" + MRP_CODE + "')final order by final.OpenPONO,final.item_type,final.vendor_code,final.ScheduleType"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

            Dim Comparision_Sno As Decimal = 0
            Dim item_code As String = ""
            Dim line_no As Integer = 0
            Dim vendr_code As String = Nothing

            obj = New clsPurchaseOrderHead()
            obj.Arr = New List(Of clsPurchaseOrderDetail)

            objOpenPO = New clsPurchaseOrderHead()
            objOpenPO.Arr = New List(Of clsPurchaseOrderDetail)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    
                    If clsCommon.myCdbl(dr("sno")) = 1 Then 'beacuse ther is multi line data of same vendor and merge it in one array for single po
                        Comparision_Sno = clsCommon.myCdbl(dr("sno"))
                        line_no = 0
                        If obj IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                            isSaved = isSaved AndAlso obj.SaveData(obj, True, False, trans)
                            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery("update TSPL_MRP_PO_DETAIL set purchaseorder_no='" + obj.PurchaseOrder_No + "',purchaseorder_date='" + clsCommon.GetPrintDate(obj.PurchaseOrder_Date, "dd/MMM/yyyy hh:mm tt") + "' where mrp_code='" + MRP_CODE + "' and vendor_code='" + vendr_code + "' and item_code in ('" + item_code + "')", trans)
                        End If
                        If objOpenPO IsNot Nothing AndAlso objOpenPO.Arr.Count > 0 Then
                            clsPurchaseOrderHead.SaveScheduleData(objOpenPO, objOpenPO.Schedule_Type, objMRP.MRP_FROM, trans)
                        End If

                        item_code = ""
                        obj = New clsPurchaseOrderHead()
                        obj.Arr = New List(Of clsPurchaseOrderDetail)

                        objOpenPO = New clsPurchaseOrderHead()
                        objOpenPO.Arr = New List(Of clsPurchaseOrderDetail)
                    End If
                    obj.PurchaseOrder_Date = clsCommon.myCDate(clsCommon.GETSERVERDATE(trans))
                    obj.Delivery_date = obj.PurchaseOrder_Date
                    obj.Delivery_Duration = Nothing
                    obj.PurchaseOrder_Type = "L" 'domestic
                    obj.Against_RGP = "0"
                    obj.Against_Vendor_Quotation = Nothing
                    obj.Is_Open_PO = "0"
                    obj.Vendor_Code = clsCommon.myCstr(dr("vendor_code"))
                    vendr_code = clsCommon.myCstr(dr("vendor_code"))
                    obj.Vendor_Name = clsCommon.myCstr(dr("vendor_name"))
                    obj.On_Hold = False
                    obj.Ref_No = Nothing
                    obj.Remarks = clsCommon.myCstr(dr("remarks"))
                    obj.Description = "Auto PO from MRP(STD) having code: " + MRP_CODE + ""
                    obj.Bill_To_Location = clsCommon.myCstr(dr("bill_to_location"))
                    obj.Ship_To_Location = Nothing
                    obj.Tax_Group = GetDefaultTaxGroup(clsCommon.myCstr(dr("bill_to_location")), clsCommon.myCstr(dr("vendor_code")), "P", trans)
                    obj.Mode_Of_Transport = "By Road"
                    obj.Item_Type = clsItemMaster.GetItemType(clsCommon.myCstr(dr("item_code")), trans)
                    'obj.Auto_PO = "1"
                    Dim objtr As New clsPurchaseOrderDetail()
                    objtr.Line_No = line_no + 1
                    objtr.Row_Type = "Item"
                    objtr.Item_Code = clsCommon.myCstr(dr("item_code"))
                    objtr.Item_Desc = clsItemMaster.GetItemName(objtr.Item_Code, trans)
                    objtr.PurchaseOrder_Qty = clsCommon.myCdbl(dr("qty"))
                    objtr.Unit_code = clsCommon.myCstr(dr("unit_code"))
                    objtr.Location = clsCommon.myCstr(dr("bill_to_location"))
                    objtr.Item_Cost = clsCommon.myCdbl(dr("item_cost"))
                    objtr.Amount = clsCommon.myCdbl(objtr.Item_Cost) * clsCommon.myCdbl(objtr.PurchaseOrder_Qty)
                    objtr.Disc_Per = 0
                    objtr.Disc_Amt = 0
                    objtr.Amt_Less_Discount = objtr.Amount
                    objtr.Item_Net_Amt = objtr.Amount
                    objtr.Remarks = clsCommon.myCstr(dr("remarks"))
                    objtr.Last_Same_Vendor_Rate = clsCommon.myCdbl(dr("PO_Last_Rate"))
                    item_code = item_code + "','" + clsCommon.myCstr(dr("item_code"))

                    '' Open PO columns

                    If clsCommon.myLen(clsCommon.myCstr(dr.Item("OpenPONO"))) <= 0 Then
                        obj.Arr.Add(objtr)
                    Else
                        Dim SchType As String = "W"
                        If objOpenPO Is Nothing OrElse objOpenPO.Arr.Count <= 0 Then
                            objOpenPO = obj.Clone()
                        End If

                        objOpenPO.PurchaseOrder_No = clsCommon.myCstr(dr.Item("OpenPONO"))
                        objtr.PurchaseOrder_Qty = clsCommon.myCdbl(dr.Item("ActualQty"))
                        If clsCommon.CompairString(clsCommon.myCstr(dr.Item("ScheduleType")), "Monthly") = CompairStringResult.Equal Then
                            SchType = "M"
                        ElseIf clsCommon.CompairString(clsCommon.myCstr(dr.Item("ScheduleType")), "Weekly") = CompairStringResult.Equal Then
                            SchType = "W"
                        ElseIf clsCommon.CompairString(clsCommon.myCstr(dr.Item("ScheduleType")), "Daily") = CompairStringResult.Equal Then
                            SchType = "D"
                        End If
                        objOpenPO.Schedule_Type = SchType
                        objOpenPO.Arr.Add(objtr)

                        'clsPurchaseOrderHead.SaveScheduleData(objOpenPO, SchType, objMRP.MRP_FROM, trans)
                    End If

                Next
                If obj IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                    isSaved = isSaved AndAlso obj.SaveData(obj, True, False, trans)
                    isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery("update TSPL_MRP_PO_DETAIL set purchaseorder_no='" + obj.PurchaseOrder_No + "',purchaseorder_date='" + clsCommon.GetPrintDate(obj.PurchaseOrder_Date, "dd/MMM/yyyy hh:mm tt") + "' where mrp_code='" + MRP_CODE + "' and vendor_code='" + vendr_code + "' and item_code in ('" + item_code + "')", trans)
                End If
                If objOpenPO IsNot Nothing AndAlso objOpenPO.Arr.Count > 0 Then
                    clsPurchaseOrderHead.SaveScheduleData(objOpenPO, objOpenPO.Schedule_Type, objMRP.MRP_FROM, trans)
                End If
            End If

            Return isSaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            obj = Nothing
        End Try
    End Function

    Public Shared Function GetDefaultTaxGroup(ByVal strTransLocation As String, ByVal strVendorCustomerCode As String, ByVal strTaxType As String, ByVal trans As SqlTransaction) As String
        Dim qry As String = " Select Tax_Group_Code"
        qry += " from TSPL_LOCATION_WISE_TAX_MASTER "
        qry += " where TSPL_LOCATION_WISE_TAX_MASTER.Is_Default_Tax_Group=1 and  Location_Code = '" + strTransLocation + "' and Tax_Type='" + strTaxType + "'  "
        qry += " and TSPL_LOCATION_WISE_TAX_MASTER.Tax_Category in (select case when MIN(x.State)=MAX(x.State) then 'L' else 'I' end  from  (select State   from TSPL_LOCATION_MASTER where Location_Code='" + strTransLocation + "' union all   "

        If clsCommon.CompairString("S", strTaxType) = CompairStringResult.Equal Then
            qry += "  select   State from TSPL_CUSTOMER_MASTER where Cust_Code='" + strVendorCustomerCode + "' "
        ElseIf clsCommon.CompairString("P", strTaxType) = CompairStringResult.Equal Then
            qry += "   select  State_Code as State from TSPL_VENDOR_MASTER where Vendor_Code='" + strVendorCustomerCode + "' "
        Else
            Throw New Exception("Please enter valid Tax Type it should be 'P' or 'S'")
        End If
        qry += " )x) "
        qry += " group by Tax_Group_Code"

        Return clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))

    End Function
End Class
Public Class clsMRPOpenPOItemDetail
#Region "Variables"
    Public MRP_CODE As String = Nothing
    Public OpenPONo As String = Nothing
    Public OpenPODate As String = Nothing
    Public Vendor_Code As String = Nothing
    Public Vendor_Name As String = Nothing
    Public Item_Code As String = Nothing
    Public Item_Desc As String = Nothing
    Public OpenPoRate As Double = 0
    Public Unit_Code As String = Nothing
    Public Unit_Rate As Double = 0
    Public Vendor_Unit_Rate As Double = 0
    Public TOTAL_STD_DAY As Decimal = 0
    Public MOQ As Double = 0
    Public SPQ As Double = 0
    Public SOB As Double = 0
#End Region
    Public Shared Function GetOpenPODetail(ByVal PO_No As String, ByVal ItemCode As String, Optional ByVal trans As SqlTransaction = Nothing) As clsMRPOpenPOItemDetail
        Dim obj As New clsMRPOpenPOItemDetail
        Dim qry As String = " select PO.PurchaseOrder_No,convert(varchar,PO.PurchaseOrder_Date,103) as PurchaseOrder_Date,PO.Vendor_Code,PO.Vendor_Name,PO.Item_Code,PO.Item_Desc,PO.Unit_code,PO.Item_Cost, " & _
                            " VID.vendor_code as Vendor_Code_Map,VID.Min_Order_Qty,VID.SP_QTY,VID.SOB,VID.item_rate as Vendor_Unit_Rate,VID.TOTAL_STD_DAY from (" & _
                            " select Head.PurchaseOrder_No,Head.PurchaseOrder_Date,Head.Vendor_Code,Head.Vendor_Name,dtl.Item_Code,dtl.Item_Desc,dtl.Unit_code,dtl.Item_Cost " & _
                            " from TSPL_PURCHASE_ORDER_HEAD Head inner join TSPL_PURCHASE_ORDER_DETAIL dtl on Head.PurchaseOrder_No=dtl.PurchaseOrder_No " & _
                            " where dtl.Item_Code='" & ItemCode & "' and Head.PurchaseOrder_No='" & PO_No & "' ) as PO " & _
                            " left join TSPL_VENDOR_ITEM_DETAIL VID on PO.Vendor_Code=VID.vendor_code and po.Item_Code=VID.item_no "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dt.Rows.Count > 0 Then
            If clsCommon.myLen(clsCommon.myCstr(dt.Rows(0).Item("Vendor_Code_Map"))) <= 0 Then
                Throw New Exception("Item : " & ItemCode & " is not mapped with any vendor. ")
            End If

            obj.OpenPONo = clsCommon.myCstr(dt.Rows(0).Item("PurchaseOrder_No"))
            obj.OpenPODate = clsCommon.myCstr(dt.Rows(0).Item("PurchaseOrder_Date"))
            obj.OpenPoRate = clsCommon.myCdbl(dt.Rows(0).Item("Item_Cost"))
            obj.Unit_Code = clsCommon.myCstr(dt.Rows(0).Item("Unit_code"))
            obj.Vendor_Code = clsCommon.myCstr(dt.Rows(0).Item("Vendor_Code"))
            obj.Vendor_Name = clsCommon.myCstr(dt.Rows(0).Item("Vendor_Name"))

            obj.Item_Code = clsCommon.myCstr(dt.Rows(0).Item("Item_Code"))
            obj.Item_Desc = clsCommon.myCstr(dt.Rows(0).Item("Item_Desc"))
            obj.Unit_Rate = clsCommon.myCdbl(dt.Rows(0).Item("Item_Cost"))
            obj.Vendor_Unit_Rate = clsCommon.myCdbl(dt.Rows(0).Item("Vendor_Unit_Rate"))
            obj.TOTAL_STD_DAY = clsCommon.myCdbl(dt.Rows(0).Item("TOTAL_STD_DAY"))
            obj.MOQ = clsCommon.myCdbl(dt.Rows(0).Item("Min_Order_Qty"))
            obj.SPQ = clsCommon.myCdbl(dt.Rows(0).Item("SP_QTY"))
            obj.SOB = clsCommon.myCdbl(dt.Rows(0).Item("SOB"))
        End If
        Return obj
    End Function

End Class