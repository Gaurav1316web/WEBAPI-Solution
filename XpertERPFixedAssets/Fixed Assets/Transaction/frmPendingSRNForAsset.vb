'===============BM00000003057====================
Imports common
Imports Telerik.WinControls.UI
Imports XpertERPEngine

Public Class frmPendingSRNForAsset

#Region "Variables"
    Dim IsInsideLoadData As Boolean = False
    Public VendorCode As String = Nothing
    Public VendorName As String = Nothing
    Public strCurrCode As String = Nothing

    Dim dtAllData As DataTable = Nothing
    Public obj As New clsAcquisitionHead
    'Public objsrn As New clsAcquisitionPendingSRN
    Public IsMerchantTrade As String = Nothing
    Const colDSelect As String = "SELECT"
    Const colDCode As String = "CODE"
    Const colPOID As String = "colPOID"
    Const colGRNID As String = "colGRNID"
    Const colMRNID As String = "colMRNID"
    Const colPI As String = "colPI"

    Const colDICode As String = "ICODE"
    Const colDIName As String = "INAME"
    Const colDTaxGroup As String = "TAXGROUP"
    Const colDTaxGroupName As String = "TAXGROUPNAME"
    Const colDUnit As String = "UNIT"
    Const colDRate As String = "RATE"

    Const colDOrderQty As String = "ORDERQTY"
    Const colDApprovedQty As String = "APPROVEDQTY"
    Const colDUnApprovedQty As String = "UNAPPROVEDQTY"
    Const colDPendingQty As String = "PENDINGQTY"
    Const colFreeQty As String = "FREEQTY"
    Const colDTaxRate1 As String = "TaxRate1"
    Const colDTaxRate2 As String = "TaxRate2"
    Const colDTaxRate3 As String = "TaxRate3"
    Const colDTaxRate4 As String = "TaxRate4"
    Const colDTaxRate5 As String = "TaxRate5"
    Const colDTaxRate6 As String = "TaxRate6"
    Const colDTaxRate7 As String = "TaxRate7"
    Const colDTaxRate8 As String = "TaxRate8"
    Const colDTaxRate9 As String = "TaxRate9"
    Const colDTaxRate10 As String = "TaxRate10"
    Const colDTaxAmt1 As String = "TaxAmt1"
    Const colDTaxAmt2 As String = "TaxAmt2"
    Const colDTaxAmt3 As String = "TaxAmt3"
    Const colDTaxAmt4 As String = "TaxAmt4"
    Const colDTaxAmt5 As String = "TaxAmt5"
    Const colDTaxAmt6 As String = "TaxAmt6"
    Const colDTaxAmt7 As String = "TaxAmt7"
    Const colDTaxAmt8 As String = "TaxAmt8"
    Const colDTaxAmt9 As String = "TaxAmt9"
    Const colDTaxAmt10 As String = "TaxAmt10"
    Const colDMRP As String = "MRP"
    Const colDAssessable As String = "ASSESSABLE"
    Const colDBatchNo As String = "BATCHNO"
    Const colDBinNo As String = "BinNo"
    Const colDManDate As String = "MANFACTURERDATE"
    Const colDExpiryDate As String = "EXPIRYDATE"
    Const colDDisPer As String = "DISCOUNTPER"
    Const colDLeakQty As String = "COLEAKQTY"
    Const colDBurstQty As String = "COLBURSTQTY"
    Const colDShortQty As String = "COLSHORTQTY"
    Const colDRejectQty As String = "COLDREJECTQTY"
    Const colDDiscountType As String = "COLDDISCOUNTTYPE"

    Const colDMannualAmt As String = "COLMANUALAMT"
    Const colDAmount As String = "COLAMOUNT"
    Const colAbatementRate As String = "colAbatementRate"
    Const colDBalanceQty As String = "BALANCEQTY"
    '===============Added by preeti gupta==============
    Const colAmtAftrDis As String = "AmtAfterDisc"
    ''================================================
    Const colDPRQty As String = "PRQTY"
    Const colDReturnNo As String = "RETURNNO"
    Const colPIQty As String = "PIQTY"

    Const colHSelect As String = "SELECT"
    Const colHCode As String = "CODE"
    Const colHDate As String = "DATE"
    Const colHVendorCode As String = "VENDOR"
    Const colHVendorName As String = "VENDORNAME"
    Const colHLocation As String = "LocationCode"
    Const colDLocation As String = "LocationName"
#End Region

    Private Sub FrmPendingRequistion_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try

            '============================================== pick Landed_Cost_Amount instead of Amt_Less_Discount from purchase invoice detail 22 Apr,2019
            'Dim qry As String = " select * ,TSPL_TAX_GROUP_MASTER.Tax_Group_Desc from (  select distinct SRN.[SRN No],SRN.[SRN Date],SRN.[Vendor Code],SRN.[Vendor Name],SRN.[PI No],SRN.Bill_To_Location,Location_Desc as [Location Name],PIT.Line_No,PIT.Item_Code as [Item Code],Item_Desc as [Item Name],  PIT.Unit_Code as [Unit Code],PIT.PI_Qty as [PI Qty],isnull(PRN.PR_No,'') as [Return No],PRN.PR_Qty as [PR Qty],case when PI_Qty < = 0 then srn_qty else  (PIT.PI_Qty-coalesce(PRN.PR_Qty,0)-coalesce(srn.acq_qty,0)) end as [Balance Qty]" &
            '    " , pit.Tax_Group,pit. TAX1_Rate,pit. TAX2_Rate,pit.TAX3_Rate,pit.TAX4_Rate,pit.TAX5_Rate,pit. TAX6_Rate,pit. TAX7_Rate,pit. TAX8_Rate,pit.TAX9_Rate,pit.TAX10_Rate,pit.TAX1_Amt,pit. TAX2_Amt,pit. TAX3_Amt,pit.TAX4_Amt,pit. TAX5_Amt,pit. TAX6_Amt,pit.TAX7_Amt,pit.TAX8_Amt,pit. TAX9_Amt,pit. TAX10_Amt,pit. Item_Cost,PIT.Amt_Less_Discount " &
            '    " from ( select fin.* from ( select TSPL_SRN_HEAD.SRN_No as [SRN No],max(CONVERT(varchar, SRN_Date,103)) as [SRN Date],max(TSPL_SRN_HEAD.Vendor_Code) as [Vendor Code],  max( TSPL_SRN_HEAD.Vendor_Name) as [Vendor Name],max(TSPL_PI_HEAD.PI_No) as [PI No],max(TSPL_SRN_HEAD.Bill_To_Location) as Bill_To_Location,max(TSPL_LOCATION_MASTER.Location_Desc) as Location_Desc,  TSPL_SRN_DETAIL.Line_No,TSPL_SRN_DETAIL.Item_Code,avg(TSPL_SRN_DETAIL.Amount) as amount,max(TSPL_SRN_DETAIL.Unit_code) as unit_code,sum(TSPL_SRN_DETAIL.SRN_Qty) as srn_qty," &
            '    " sum(TSPL_ACQUISITION_detail.SRNQty) as acq_qty ,max(tspl_item_master.Item_Desc) as item_desc " &
            '    " ,'' as Tax_Group,0 as TAX1_Rate,0 as TAX2_Rate,0 as TAX3_Rate,0 as TAX4_Rate,0 as TAX5_Rate,0 as TAX6_Rate,0 as TAX7_Rate,0 as TAX8_Rate,0 as TAX9_Rate,0 as TAX10_Rate,0 as TAX1_Amt,0 as TAX2_Amt,0 as TAX3_Amt,0 as TAX4_Amt,0 as TAX5_Amt,0 as TAX6_Amt,0 as TAX7_Amt,0 as TAX8_Amt,0 as TAX9_Amt,0 as TAX10_Amt,0 as Item_Cost,max(TSPL_ACQUISITION_detail.Item_Code) as ac_item,avg(TSPL_SRN_DETAIL.Amt_Less_Discount) as Amt_Less_Discount " &
            '    " from TSPL_SRN_HEAD  inner join TSPL_SRN_DETAIL on TSPL_SRN_HEAD.SRN_No=TSPL_SRN_DETAIL.SRN_No  left join TSPL_PI_HEAD on TSPL_SRN_HEAD.SRN_No=TSPL_PI_HEAD.Against_SRN left join  tspl_item_master on tspl_item_master.Item_Code =TSPL_SRN_DETAIL.Item_Code left join TSPL_LOCATION_MASTER on TSPL_SRN_HEAD.Bill_To_Location=TSPL_LOCATION_MASTER.Location_Code left outer join TSPL_ACQUISITION_HEAD on TSPL_ACQUISITION_HEAD.vendor_code=tspl_srn_head.vendor_code and tspl_srn_head.srn_no=TSPL_ACQUISITION_HEAD.srn_no left outer join TSPL_ACQUISITION_detail on TSPL_ACQUISITION_detail.acquisition_code=TSPL_ACQUISITION_HEAD.acquisition_code and TSPL_ACQUISITION_detail.item_code=tspl_srn_detail.item_code  and TSPL_ACQUISITION_detail.srn_no= tspl_srn_detail.SRN_No where  TSPL_SRN_HEAD.Item_Type='A' and TSPL_PI_HEAD.Status=1 group by TSPL_SRN_HEAD.SRN_No,TSPL_SRN_DETAIL.Line_No,TSPL_SRN_DETAIL.Item_Code)fin where fin.srn_qty-coalesce(fin.acq_qty,0)>0) as SRN " &
            '    " inner join ( select TSPL_PI_HEAD.PI_No,TSPL_PI_HEAD.Against_SRN as SRN_No,TSPL_PI_DETAIL.Line_No,TSPL_PI_DETAIL.Item_Code,TSPL_PI_DETAIL.Amount,TSPL_PI_DETAIL.Unit_Code,TSPL_PI_DETAIL.PI_Qty " &
            '    " ,TSPL_PI_HEAD.Tax_Group,TSPL_PI_DETAIL.TAX1_Rate,TSPL_PI_DETAIL.TAX2_Rate,TSPL_PI_DETAIL.TAX3_Rate,TSPL_PI_DETAIL.TAX4_Rate,TSPL_PI_DETAIL.TAX5_Rate,TSPL_PI_DETAIL.TAX6_Rate,TSPL_PI_DETAIL.TAX7_Rate,TSPL_PI_DETAIL.TAX8_Rate,TSPL_PI_DETAIL.TAX9_Rate,TSPL_PI_DETAIL.TAX10_Rate,TSPL_PI_DETAIL.TAX1_Amt,TSPL_PI_DETAIL.TAX2_Amt,TSPL_PI_DETAIL.TAX3_Amt,TSPL_PI_DETAIL.TAX4_Amt,TSPL_PI_DETAIL.TAX5_Amt,TSPL_PI_DETAIL.TAX6_Amt,TSPL_PI_DETAIL.TAX7_Amt,TSPL_PI_DETAIL.TAX8_Amt,TSPL_PI_DETAIL.TAX9_Amt,TSPL_PI_DETAIL.TAX10_Amt, Item_Cost,TSPL_PI_DETAIL.Landed_Cost_Amount as Amt_Less_Discount " &
            '    " from TSPL_PI_HEAD inner join TSPL_PI_DETAIL on TSPL_PI_HEAD.PI_No=TSPL_PI_DETAIL.PI_No) as PIT  on SRN.[PI No]=PIT.PI_No and SRN.[SRN No]=PIT.SRN_No and SRN.Item_Code=PIT.Item_Code and SRN.Unit_code=PIT.Unit_Code " &
            '    " left join ( select TSPL_PR_HEAD.PR_No,TSPL_PR_HEAD.Against_PI as [PI No],TSPL_PR_HEAD.Against_SRN as SRN_No,TSPL_PR_DETAIL.Line_No,TSPL_PR_DETAIL.Item_Code,TSPL_PR_DETAIL.Amount,TSPL_PR_DETAIL.Unit_code,TSPL_PR_DETAIL.PR_Qty" &
            '    " ,'' as Tax_Group,0 as TAX1_Rate,0 as TAX2_Rate,0 asTAX3_Rate,0 asTAX4_Rate,0 asTAX5_Rate,0 asTAX6_Rate,0 asTAX7_Rate,0 asTAX8_Rate,0 asTAX9_Rate,0 asTAX10_Rate,0 asTAX1_Amt,0 asTAX2_Amt,0 asTAX3_Amt,0 as TAX4_Amt,0 as TAX5_Amt,0 as TAX6_Amt,0 as TAX7_Amt,0 as TAX8_Amt,0 as TAX9_Amt,0 as TAX10_Amt ,0 as Item_Cost" &
            '    " from TSPL_PR_HEAD inner join TSPL_PR_DETAIL on TSPL_PR_HEAD.PR_No=TSPL_PR_DETAIL.PR_No ) as PRN on PRN.[PI No]=PIT.PI_No and PRN.SRN_No=PIT.SRN_No and PRN.Item_Code=PIT.Item_Code and PRN.Unit_code=PIT.Unit_code " &
            '    " ) as Final left outer join TSPL_TAX_GROUP_MASTER on TSPL_TAX_GROUP_MASTER.Tax_Group_Code=Final.Tax_Group and TSPL_TAX_GROUP_MASTER.Tax_Group_Type='P'  where [Balance Qty]>0  order by [SRN No],Line_No "

            '' --changes done against purchase return price adjustment type 
            Dim qry As String = " select * ,TSPL_TAX_GROUP_MASTER.Tax_Group_Desc from (  select distinct SRN.[SRN No],SRN.[SRN Date],SRN.[Vendor Code],SRN.[Vendor Name],SRN.[PI No],SRN.Bill_To_Location,Location_Desc as [Location Name],PIT.Line_No,PIT.Item_Code as [Item Code],Item_Desc as [Item Name],  PIT.Unit_Code as [Unit Code],PIT.PI_Qty as [PI Qty],isnull(PRN.PR_No,'') as [Return No],PRN.PR_Qty as [PR Qty],case when PI_Qty < = 0 then srn_qty else  (PIT.PI_Qty-coalesce(PRN.PR_Qty,0)-coalesce(srn.acq_qty,0)) end as [Balance Qty]" &
                " , pit.Tax_Group,pit. TAX1_Rate,pit. TAX2_Rate,pit.TAX3_Rate,pit.TAX4_Rate,pit.TAX5_Rate,pit. TAX6_Rate,pit. TAX7_Rate,pit. TAX8_Rate,pit.TAX9_Rate,pit.TAX10_Rate,pit.TAX1_Amt,pit. TAX2_Amt,pit. TAX3_Amt,pit.TAX4_Amt,pit. TAX5_Amt,pit. TAX6_Amt,pit.TAX7_Amt,pit.TAX8_Amt,pit. TAX9_Amt,pit. TAX10_Amt,pit. Item_Cost,PIT.Amt_Less_Discount " &
                " from ( select fin.* from ( select TSPL_SRN_HEAD.SRN_No as [SRN No],max(CONVERT(varchar, SRN_Date,103)) as [SRN Date],max(TSPL_SRN_HEAD.Vendor_Code) as [Vendor Code],  max( TSPL_SRN_HEAD.Vendor_Name) as [Vendor Name],max(TSPL_PI_HEAD.PI_No) as [PI No],max(TSPL_SRN_HEAD.Bill_To_Location) as Bill_To_Location,max(TSPL_LOCATION_MASTER.Location_Desc) as Location_Desc,  TSPL_SRN_DETAIL.Line_No,TSPL_SRN_DETAIL.Item_Code,avg(TSPL_SRN_DETAIL.Amount) as amount,max(TSPL_SRN_DETAIL.Unit_code) as unit_code,sum(TSPL_SRN_DETAIL.SRN_Qty) as srn_qty," &
                " sum(TSPL_ACQUISITION_detail.SRNQty) as acq_qty ,max(tspl_item_master.Item_Desc) as item_desc " &
                " ,'' as Tax_Group,0 as TAX1_Rate,0 as TAX2_Rate,0 as TAX3_Rate,0 as TAX4_Rate,0 as TAX5_Rate,0 as TAX6_Rate,0 as TAX7_Rate,0 as TAX8_Rate,0 as TAX9_Rate,0 as TAX10_Rate,0 as TAX1_Amt,0 as TAX2_Amt,0 as TAX3_Amt,0 as TAX4_Amt,0 as TAX5_Amt,0 as TAX6_Amt,0 as TAX7_Amt,0 as TAX8_Amt,0 as TAX9_Amt,0 as TAX10_Amt,0 as Item_Cost,max(TSPL_ACQUISITION_detail.Item_Code) as ac_item,avg(TSPL_SRN_DETAIL.Amt_Less_Discount) as Amt_Less_Discount " &
                " from TSPL_SRN_HEAD  inner join TSPL_SRN_DETAIL on TSPL_SRN_HEAD.SRN_No=TSPL_SRN_DETAIL.SRN_No  left join TSPL_PI_HEAD on TSPL_SRN_HEAD.SRN_No=TSPL_PI_HEAD.Against_SRN left join  tspl_item_master on tspl_item_master.Item_Code =TSPL_SRN_DETAIL.Item_Code left join TSPL_LOCATION_MASTER on TSPL_SRN_HEAD.Bill_To_Location=TSPL_LOCATION_MASTER.Location_Code left outer join TSPL_ACQUISITION_HEAD on TSPL_ACQUISITION_HEAD.vendor_code=tspl_srn_head.vendor_code and tspl_srn_head.srn_no=TSPL_ACQUISITION_HEAD.srn_no left outer join TSPL_ACQUISITION_detail on TSPL_ACQUISITION_detail.acquisition_code=TSPL_ACQUISITION_HEAD.acquisition_code and TSPL_ACQUISITION_detail.item_code=tspl_srn_detail.item_code  and TSPL_ACQUISITION_detail.srn_no= tspl_srn_detail.SRN_No where  TSPL_SRN_HEAD.Item_Type='A' and TSPL_PI_HEAD.Status=1 group by TSPL_SRN_HEAD.SRN_No,TSPL_SRN_DETAIL.Line_No,TSPL_SRN_DETAIL.Item_Code)fin where fin.srn_qty-coalesce(fin.acq_qty,0)>0) as SRN " &
                " inner join (select PI_No,SRN_No,max(Line_no) as Line_no,Item_Code,sum(Amount) as Amount,Unit_Code,max(Pi_qty) as Pi_qty, max(Tax_Group) as Tax_Group,max(TAX1_Rate) as TAX1_Rate,max(TAX2_Rate) as TAX2_Rate,max(TAX3_Rate) as TAX3_Rate,max(TAX4_Rate) as TAX4_Rate,max(TAX5_Rate) as TAX5_Rate,max(TAX6_Rate) as TAX6_Rate,max(TAX7_Rate) as TAX7_Rate,max(TAX8_Rate) as TAX8_Rate,max(TAX9_Rate) as TAX9_Rate,max(TAX10_Rate) as TAX10_Rate,sum(TAX1_Amt) as TAX1_Amt,sum(TAX2_Amt) as  TAX2_Amt,sum(TAX3_Amt) as TAX3_Amt,sum(TAX4_Amt) as TAX4_Amt,sum(TAX5_Amt) as TAX5_Amt,sum(TAX6_Amt) as TAX6_Amt,sum(TAX7_Amt) as TAX7_Amt,sum(TAX8_Amt) as TAX8_Amt,sum(TAX9_Amt) as TAX9_Amt,sum(TAX10_Amt) as TAX10_Amt,sum(Item_Cost) as Item_Cost,sum(Amt_Less_Discount) as Amt_Less_Discount from ( select TSPL_PI_HEAD.PI_No,TSPL_PI_HEAD.Against_SRN as SRN_No,TSPL_PI_DETAIL.Line_No,TSPL_PI_DETAIL.Item_Code,TSPL_PI_DETAIL.Amount,TSPL_PI_DETAIL.Unit_Code,TSPL_PI_DETAIL.PI_Qty " &
                " ,TSPL_PI_HEAD.Tax_Group,TSPL_PI_DETAIL.TAX1_Rate,TSPL_PI_DETAIL.TAX2_Rate,TSPL_PI_DETAIL.TAX3_Rate,TSPL_PI_DETAIL.TAX4_Rate,TSPL_PI_DETAIL.TAX5_Rate,TSPL_PI_DETAIL.TAX6_Rate,TSPL_PI_DETAIL.TAX7_Rate,TSPL_PI_DETAIL.TAX8_Rate,TSPL_PI_DETAIL.TAX9_Rate,TSPL_PI_DETAIL.TAX10_Rate,TSPL_PI_DETAIL.TAX1_Amt,TSPL_PI_DETAIL.TAX2_Amt,TSPL_PI_DETAIL.TAX3_Amt,TSPL_PI_DETAIL.TAX4_Amt,TSPL_PI_DETAIL.TAX5_Amt,TSPL_PI_DETAIL.TAX6_Amt,TSPL_PI_DETAIL.TAX7_Amt,TSPL_PI_DETAIL.TAX8_Amt,TSPL_PI_DETAIL.TAX9_Amt,TSPL_PI_DETAIL.TAX10_Amt, Item_Cost,TSPL_PI_DETAIL.Landed_Cost_Amount as Amt_Less_Discount " &
                " from TSPL_PI_HEAD inner join TSPL_PI_DETAIL on TSPL_PI_HEAD.PI_No=TSPL_PI_DETAIL.PI_No  union
select TSPL_PR_HEAD.Against_pi as PI_No,TSPL_PR_HEAD.Against_SRN as SRN_No,TSPL_PR_DETAIL.Line_No,TSPL_PR_DETAIL.Item_Code,TSPL_PR_DETAIL.Amount * case when TSPL_PR_HEAD.Note_type='D' then -1 else 1 end  as Amount,TSPL_PR_DETAIL.Unit_Code,TSPL_PR_DETAIL.PR_Qty as PI_Qty  ,TSPL_PR_HEAD.Tax_Group,TSPL_PR_DETAIL.TAX1_Rate,TSPL_PR_DETAIL.TAX2_Rate,TSPL_PR_DETAIL.TAX3_Rate,TSPL_PR_DETAIL.TAX4_Rate,TSPL_PR_DETAIL.TAX5_Rate,TSPL_PR_DETAIL.TAX6_Rate,TSPL_PR_DETAIL.TAX7_Rate,TSPL_PR_DETAIL.TAX8_Rate,TSPL_PR_DETAIL.TAX9_Rate,TSPL_PR_DETAIL.TAX10_Rate,TSPL_PR_DETAIL.TAX1_Amt * case when TSPL_PR_HEAD.Note_type='D' then -1 else 1 end  as TAX1_Amt,TSPL_PR_DETAIL.TAX2_Amt * case when TSPL_PR_HEAD.Note_type='D' then -1 else 1 end as TAX2_Amt,TSPL_PR_DETAIL.TAX3_Amt * case when TSPL_PR_HEAD.Note_type='D' then -1 else 1 end as TAX3_Amt,TSPL_PR_DETAIL.TAX4_Amt * case when TSPL_PR_HEAD.Note_type='D' then -1 else 1 end as TAX4_Amt,TSPL_PR_DETAIL.TAX5_Amt * case when TSPL_PR_HEAD.Note_type='D' then -1 else 1 end as TAX5_Amt,TSPL_PR_DETAIL.TAX6_Amt * case when TSPL_PR_HEAD.Note_type='D' then -1 else 1 end as TAX6_Amt,TSPL_PR_DETAIL.TAX7_Amt * case when TSPL_PR_HEAD.Note_type='D' then -1 else 1 end as TAX7_Amt,TSPL_PR_DETAIL.TAX8_Amt * case when TSPL_PR_HEAD.Note_type='D' then -1 else 1 end as TAX8_Amt,TSPL_PR_DETAIL.TAX9_Amt * case when TSPL_PR_HEAD.Note_type='D' then -1 else 1 end as TAX9_Amt,TSPL_PR_DETAIL.TAX10_Amt * case when TSPL_PR_HEAD.Note_type='D' then -1 else 1 end as TAX10_Amt, Item_Cost * case when TSPL_PR_HEAD.Note_type='D' then -1 else 1 end as Item_Cost,TSPL_PR_DETAIL.Landed_Cost_Amount * case when TSPL_PR_HEAD.Note_type='D' then -1 else 1 end as Amt_Less_Discount  from TSPL_PR_HEAD inner join TSPL_PR_DETAIL on TSPL_PR_HEAD.PR_No=TSPL_PR_DETAIL.PR_No
)final group by PI_No,SRN_No,Item_code,Unit_Code ) as PIT  on SRN.[PI No]=PIT.PI_No and SRN.[SRN No]=PIT.SRN_No and SRN.Item_Code=PIT.Item_Code and SRN.Unit_code=PIT.Unit_Code " &
                " left join ( select TSPL_PR_HEAD.PR_No,TSPL_PR_HEAD.Against_PI as [PI No],TSPL_PR_HEAD.Against_SRN as SRN_No,TSPL_PR_DETAIL.Line_No,TSPL_PR_DETAIL.Item_Code,TSPL_PR_DETAIL.Amount,TSPL_PR_DETAIL.Unit_code,TSPL_PR_DETAIL.PR_Qty" &
                " ,'' as Tax_Group,0 as TAX1_Rate,0 as TAX2_Rate,0 asTAX3_Rate,0 asTAX4_Rate,0 asTAX5_Rate,0 asTAX6_Rate,0 asTAX7_Rate,0 asTAX8_Rate,0 asTAX9_Rate,0 asTAX10_Rate,0 asTAX1_Amt,0 asTAX2_Amt,0 asTAX3_Amt,0 as TAX4_Amt,0 as TAX5_Amt,0 as TAX6_Amt,0 as TAX7_Amt,0 as TAX8_Amt,0 as TAX9_Amt,0 as TAX10_Amt ,0 as Item_Cost" &
                " from TSPL_PR_HEAD inner join TSPL_PR_DETAIL on TSPL_PR_HEAD.PR_No=TSPL_PR_DETAIL.PR_No where tspl_pr_head.transaction_type<>'P' ) as PRN on PRN.[PI No]=PIT.PI_No and PRN.SRN_No=PIT.SRN_No and PRN.Item_Code=PIT.Item_Code and PRN.Unit_code=PIT.Unit_code " &
                " ) as Final left outer join TSPL_TAX_GROUP_MASTER on TSPL_TAX_GROUP_MASTER.Tax_Group_Code=Final.Tax_Group and TSPL_TAX_GROUP_MASTER.Tax_Group_Type='P'  where [Balance Qty]>0  order by [SRN No],Line_No "

            dtAllData = clsDBFuncationality.GetDataTable(qry)
            'If dtAllData Is Nothing OrElse dtAllData.Rows.Count <= 0 Then
            '    common.clsCommon.MyMessageBoxShow("No item found for vendor " + VendorName + "")
            '    Me.Close()
            '                End If
            'LoadHeadData()
            LoadDetailData()
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
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
                gvHead.Rows(gvHead.RowCount - 1).Cells(colHDate).Value = clsCommon.myCstr(dr("SRN Date"))
                gvHead.Rows(gvHead.RowCount - 1).Cells(colHVendorCode).Value = clsCommon.myCstr(dr("Vendor Code"))
                gvHead.Rows(gvHead.RowCount - 1).Cells(colHVendorName).Value = clsCommon.myCstr(dr("Vendor Name"))
                gvHead.Rows(gvHead.RowCount - 1).Cells(colHLocation).Value = clsCommon.myCstr(dr("Bill_To_Location"))
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
        repoCode.HeaderText = "SRN No"
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


        Dim repoLocation As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoLocation.FormatString = ""
        repoLocation.HeaderText = "Location"
        repoLocation.Name = colHLocation
        repoLocation.Width = 170
        repoLocation.ReadOnly = True
        gvHead.MasterTemplate.Columns.Add(repoLocation)

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
        repoCode.HeaderText = "SRN No"
        repoCode.Name = colDCode
        repoCode.Width = 180
        repoCode.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoCode)

        Dim repoPINO As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoPINO.FormatString = ""
        repoPINO.HeaderText = "PI No"
        repoPINO.Name = colPI
        repoPINO.Width = 180
        repoPINO.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoPINO)


        Dim repoDate As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoDate.FormatString = ""
        repoDate.HeaderText = "Date"
        repoDate.Name = colHDate
        repoDate.Width = 70
        repoDate.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoDate)

        Dim repoVendor As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoVendor.FormatString = ""
        repoVendor.HeaderText = "Vendor"
        repoVendor.Name = colHVendorCode
        repoVendor.Width = 170
        repoVendor.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoVendor)

        Dim repoVendorName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoVendorName.FormatString = ""
        repoVendorName.HeaderText = "Vendor Name"
        repoVendorName.Name = colHVendorName
        repoVendorName.Width = 170
        repoVendorName.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoVendorName)

        Dim repoLocation As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoLocation.FormatString = ""
        repoLocation.HeaderText = "Location Code"
        repoLocation.Name = colHLocation
        repoLocation.Width = 170
        repoLocation.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoLocation)

        Dim repoLocationName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoLocationName.FormatString = ""
        repoLocationName.HeaderText = "Location Name"
        repoLocationName.Name = colDLocation
        repoLocationName.Width = 170
        repoLocationName.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoLocationName)


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




        Dim repoPIQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoPIQty.FormatString = ""
        repoPIQty.HeaderText = "PI Qty"
        repoPIQty.Name = colPIQty
        repoPIQty.ReadOnly = True
        repoPIQty.Width = 80
        repoPIQty.WrapText = True
        repoPIQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoPIQty)

        Dim repoReturnNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoReturnNo.FormatString = ""
        repoReturnNo.HeaderText = "Return No"
        repoReturnNo.Name = colDReturnNo
        repoReturnNo.ReadOnly = True
        repoReturnNo.Width = 80
        repoReturnNo.WrapText = True
        repoReturnNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoReturnNo)

        Dim repoPRQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoPRQty.FormatString = ""
        repoPRQty.HeaderText = "PR Qty"
        repoPRQty.Name = colDPRQty
        repoPRQty.ReadOnly = True
        repoPRQty.Width = 80
        repoPRQty.WrapText = True
        repoPRQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoPRQty)

        Dim repoBalanceQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoBalanceQty = New GridViewDecimalColumn()
        repoBalanceQty.FormatString = ""
        repoBalanceQty.HeaderText = "Balance Qty"
        repoBalanceQty.Name = colDBalanceQty
        repoBalanceQty.ReadOnly = True
        repoBalanceQty.Width = 80
        repoBalanceQty.WrapText = True
        repoBalanceQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoBalanceQty)

        Dim repoAmtAfterDisc As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAmtAfterDisc = New GridViewDecimalColumn()
        repoAmtAfterDisc.FormatString = ""
        repoAmtAfterDisc.HeaderText = "Amount After Disc."
        repoAmtAfterDisc.Name = colAmtAftrDis
        repoAmtAfterDisc.ReadOnly = True
        repoAmtAfterDisc.Width = 80
        repoAmtAfterDisc.WrapText = True
        repoAmtAfterDisc.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoAmtAfterDisc)

        Dim repoTaxCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTaxCode.FormatString = ""
        repoTaxCode.HeaderText = "Tax Group Code"
        repoTaxCode.Name = colDTaxGroup
        repoTaxCode.Width = 100
        repoTaxCode.ReadOnly = True
        repoTaxCode.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoTaxCode)

        Dim repoTaxName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTaxName.FormatString = ""
        repoTaxName.HeaderText = "Tax Group"
        repoTaxName.Name = colDTaxGroupName
        repoTaxName.Width = 100
        repoTaxName.ReadOnly = True
        repoTaxName.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoTaxName)

        Dim repoTaxRate1 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate1 = New GridViewDecimalColumn()
        repoTaxRate1.FormatString = ""
        repoTaxRate1.HeaderText = "Tax Rate 1"
        repoTaxRate1.Name = colDTaxRate1
        repoTaxRate1.ReadOnly = True
        repoTaxRate1.IsVisible = False
        repoTaxRate1.WrapText = True
        repoTaxRate1.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxRate1)

        Dim repoTaxRate2 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate2 = New GridViewDecimalColumn()
        repoTaxRate2.FormatString = ""
        repoTaxRate2.HeaderText = "Tax Rate 2"
        repoTaxRate2.Name = colDTaxRate2
        repoTaxRate2.ReadOnly = True
        repoTaxRate2.IsVisible = False
        repoTaxRate2.WrapText = True
        repoTaxRate2.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxRate2)

        Dim repoTaxRate3 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate3 = New GridViewDecimalColumn()
        repoTaxRate3.FormatString = ""
        repoTaxRate3.HeaderText = "Tax Rate 3"
        repoTaxRate3.Name = colDTaxRate3
        repoTaxRate3.ReadOnly = True
        repoTaxRate3.IsVisible = False
        repoTaxRate3.WrapText = True
        repoTaxRate3.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxRate3)

        Dim repoTaxRate4 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate4 = New GridViewDecimalColumn()
        repoTaxRate4.FormatString = ""
        repoTaxRate4.HeaderText = "Tax Rate 4"
        repoTaxRate4.Name = colDTaxRate4
        repoTaxRate4.ReadOnly = True
        repoTaxRate4.IsVisible = False
        repoTaxRate4.WrapText = True
        repoTaxRate4.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxRate4)

        Dim repoTaxRate5 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate5 = New GridViewDecimalColumn()
        repoTaxRate5.FormatString = ""
        repoTaxRate5.HeaderText = "Tax Rate 5"
        repoTaxRate5.Name = colDTaxRate5
        repoTaxRate5.ReadOnly = True
        repoTaxRate5.IsVisible = False
        repoTaxRate5.WrapText = True
        repoTaxRate5.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxRate5)

        Dim repoTaxRate6 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate6 = New GridViewDecimalColumn()
        repoTaxRate6.FormatString = ""
        repoTaxRate6.HeaderText = "Tax Rate 6"
        repoTaxRate6.Name = colDTaxRate6
        repoTaxRate6.ReadOnly = True
        repoTaxRate6.IsVisible = False
        repoTaxRate6.WrapText = True
        repoTaxRate6.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxRate6)

        Dim repoTaxRate7 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate7 = New GridViewDecimalColumn()
        repoTaxRate7.FormatString = ""
        repoTaxRate7.HeaderText = "Tax Rate 7"
        repoTaxRate7.Name = colDTaxRate7
        repoTaxRate7.ReadOnly = True
        repoTaxRate7.IsVisible = False
        repoTaxRate7.WrapText = True
        repoTaxRate7.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxRate7)

        Dim repoTaxRate8 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate8 = New GridViewDecimalColumn()
        repoTaxRate8.FormatString = ""
        repoTaxRate8.HeaderText = "Tax Rate 8"
        repoTaxRate8.Name = colDTaxRate8
        repoTaxRate8.ReadOnly = True
        repoTaxRate8.IsVisible = False
        repoTaxRate8.WrapText = True
        repoTaxRate8.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxRate8)

        Dim repoTaxRate9 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate9 = New GridViewDecimalColumn()
        repoTaxRate9.FormatString = ""
        repoTaxRate9.HeaderText = "Tax Rate 9"
        repoTaxRate9.Name = colDTaxRate9
        repoTaxRate9.ReadOnly = True
        repoTaxRate9.IsVisible = False
        repoTaxRate9.WrapText = True
        repoTaxRate9.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxRate9)

        Dim repoTaxRate10 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate10 = New GridViewDecimalColumn()
        repoTaxRate10.FormatString = ""
        repoTaxRate10.HeaderText = "Tax Rate 10"
        repoTaxRate10.Name = colDTaxRate10
        repoTaxRate10.ReadOnly = True
        repoTaxRate10.IsVisible = False
        repoTaxRate10.WrapText = True
        repoTaxRate10.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxRate10)


        Dim repoTaxAmt1 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxAmt1 = New GridViewDecimalColumn()
        repoTaxAmt1.FormatString = ""
        repoTaxAmt1.HeaderText = "Tax Amt 1"
        repoTaxAmt1.Name = colDTaxAmt1
        repoTaxAmt1.ReadOnly = True
        repoTaxAmt1.IsVisible = False
        repoTaxAmt1.WrapText = True
        repoTaxAmt1.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxAmt1)

        Dim repoTaxAmt2 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxAmt2 = New GridViewDecimalColumn()
        repoTaxAmt2.FormatString = ""
        repoTaxAmt2.HeaderText = "Tax Amt 2"
        repoTaxAmt2.Name = colDTaxAmt2
        repoTaxAmt2.ReadOnly = True
        repoTaxAmt2.IsVisible = False
        repoTaxAmt2.WrapText = True
        repoTaxAmt2.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxAmt2)

        Dim repoTaxAmt3 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxAmt3 = New GridViewDecimalColumn()
        repoTaxAmt3.FormatString = ""
        repoTaxAmt3.HeaderText = "Tax Amt 3"
        repoTaxAmt3.Name = colDTaxAmt3
        repoTaxAmt3.ReadOnly = True
        repoTaxAmt3.IsVisible = False
        repoTaxAmt3.WrapText = True
        repoTaxAmt3.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxAmt3)

        Dim repoTaxAmt4 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxAmt4 = New GridViewDecimalColumn()
        repoTaxAmt4.FormatString = ""
        repoTaxAmt4.HeaderText = "Tax Amt 4"
        repoTaxAmt4.Name = colDTaxAmt4
        repoTaxAmt4.ReadOnly = True
        repoTaxAmt4.IsVisible = False
        repoTaxAmt4.WrapText = True
        repoTaxAmt4.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxAmt4)

        Dim repoTaxAmt5 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxAmt5 = New GridViewDecimalColumn()
        repoTaxAmt5.FormatString = ""
        repoTaxAmt5.HeaderText = "Tax Amt 5"
        repoTaxAmt5.Name = colDTaxAmt5
        repoTaxAmt5.ReadOnly = True
        repoTaxAmt5.IsVisible = False
        repoTaxAmt5.WrapText = True
        repoTaxAmt5.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxAmt5)

        Dim repoTaxAmt6 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxAmt6 = New GridViewDecimalColumn()
        repoTaxAmt6.FormatString = ""
        repoTaxAmt6.HeaderText = "Tax Amt 6"
        repoTaxAmt6.Name = colDTaxAmt6
        repoTaxAmt6.ReadOnly = True
        repoTaxAmt6.IsVisible = False
        repoTaxAmt6.WrapText = True
        repoTaxAmt6.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxAmt6)

        Dim repoTaxAmt7 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxAmt7 = New GridViewDecimalColumn()
        repoTaxAmt7.FormatString = ""
        repoTaxAmt7.HeaderText = "Tax Amt 7"
        repoTaxAmt7.Name = colDTaxAmt7
        repoTaxAmt7.ReadOnly = True
        repoTaxAmt7.IsVisible = False
        repoTaxAmt7.WrapText = True
        repoTaxAmt7.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxAmt7)

        Dim repoTaxAmt8 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxAmt8 = New GridViewDecimalColumn()
        repoTaxAmt8.FormatString = ""
        repoTaxAmt8.HeaderText = "Tax Amt 8"
        repoTaxAmt8.Name = colDTaxAmt8
        repoTaxAmt8.ReadOnly = True
        repoTaxAmt8.IsVisible = False
        repoTaxAmt8.WrapText = True
        repoTaxAmt8.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxAmt8)

        Dim repoTaxAmt9 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxAmt9 = New GridViewDecimalColumn()
        repoTaxAmt9.FormatString = ""
        repoTaxAmt9.HeaderText = "Tax Amt 9"
        repoTaxAmt9.Name = colDTaxAmt9
        repoTaxAmt9.ReadOnly = True
        repoTaxAmt9.IsVisible = False
        repoTaxAmt9.WrapText = True
        repoTaxAmt9.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxAmt9)

        Dim repoTaxAmt10 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxAmt10 = New GridViewDecimalColumn()
        repoTaxAmt10.FormatString = ""
        repoTaxAmt10.HeaderText = "Tax Amt 10"
        repoTaxAmt10.Name = colDTaxAmt10
        repoTaxAmt10.ReadOnly = True
        repoTaxAmt10.IsVisible = False
        repoTaxAmt10.WrapText = True
        repoTaxAmt10.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxAmt10)

        Dim repoRate As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoRate.FormatString = ""
        repoRate.HeaderText = "Rate"
        repoRate.Name = colDRate
        repoRate.ReadOnly = True
        repoRate.IsVisible = False
        repoRate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoRate)



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

        obj = New clsAcquisitionHead
        obj.Arr = New List(Of clsAcquisitionDetail)
        Dim countenter As Integer = 0
        Dim obj1 As clsAcquisitionDetail = Nothing
        For i As Integer = 0 To gv1.Rows.Count - 1
            If clsCommon.myCBool(gv1.Rows(i).Cells(colDSelect).Value) Then
                If countenter = 0 Then
                    obj = New clsAcquisitionHead
                    obj.Arr = New List(Of clsAcquisitionDetail)

                    obj.SRN_No = clsCommon.myCstr(gv1.Rows(i).Cells(colDCode).Value)
                    obj.PI_No = clsCommon.myCstr(gv1.Rows(i).Cells(colPI).Value)
                    obj.Vendor_Code = clsCommon.myCstr(gv1.Rows(i).Cells(colHVendorCode).Value)
                    obj.Vendor_Name = clsCommon.myCstr(gv1.Rows(i).Cells(colHVendorName).Value)
                    obj.Loc_Code = clsCommon.myCstr(gv1.Rows(i).Cells(colHLocation).Value)
                    obj.Tax_Group = clsCommon.myCstr(gv1.Rows(i).Cells(colDTaxGroup).Value)
                    obj.TaxGroupName = clsCommon.myCstr(gv1.Rows(i).Cells(colDTaxGroupName).Value)
                    countenter += 1
                End If
                obj1 = New clsAcquisitionDetail()
                obj1.Item_Code = clsCommon.myCstr(gv1.Rows(i).Cells(colDICode).Value)
                obj1.Item_Name = clsCommon.myCstr(gv1.Rows(i).Cells(colDIName).Value)
                obj1.TAX1_Rate = clsCommon.myCdbl(gv1.Rows(i).Cells(colDTaxRate1).Value)
                obj1.TAX2_Rate = clsCommon.myCdbl(gv1.Rows(i).Cells(colDTaxRate2).Value)
                obj1.TAX3_Rate = clsCommon.myCdbl(gv1.Rows(i).Cells(colDTaxRate3).Value)
                obj1.TAX4_Rate = clsCommon.myCdbl(gv1.Rows(i).Cells(colDTaxRate4).Value)
                obj1.TAX5_Rate = clsCommon.myCdbl(gv1.Rows(i).Cells(colDTaxRate5).Value)
                obj1.TAX6_Rate = clsCommon.myCdbl(gv1.Rows(i).Cells(colDTaxRate6).Value)
                obj1.TAX7_Rate = clsCommon.myCdbl(gv1.Rows(i).Cells(colDTaxRate7).Value)
                obj1.TAX8_Rate = clsCommon.myCdbl(gv1.Rows(i).Cells(colDTaxRate8).Value)
                obj1.TAX9_Rate = clsCommon.myCdbl(gv1.Rows(i).Cells(colDTaxRate9).Value)
                obj1.TAX10_Rate = clsCommon.myCdbl(gv1.Rows(i).Cells(colDTaxRate10).Value)
                obj1.TAX1_Amt = clsCommon.myCdbl(gv1.Rows(i).Cells(colDTaxAmt1).Value)
                obj1.TAX2_Amt = clsCommon.myCdbl(gv1.Rows(i).Cells(colDTaxAmt2).Value)
                obj1.TAX3_Amt = clsCommon.myCdbl(gv1.Rows(i).Cells(colDTaxAmt3).Value)
                obj1.TAX4_Amt = clsCommon.myCdbl(gv1.Rows(i).Cells(colDTaxAmt4).Value)
                obj1.TAX5_Amt = clsCommon.myCdbl(gv1.Rows(i).Cells(colDTaxAmt5).Value)
                obj1.TAX6_Amt = clsCommon.myCdbl(gv1.Rows(i).Cells(colDTaxAmt6).Value)
                obj1.TAX7_Amt = clsCommon.myCdbl(gv1.Rows(i).Cells(colDTaxAmt7).Value)
                obj1.TAX8_Amt = clsCommon.myCdbl(gv1.Rows(i).Cells(colDTaxAmt8).Value)
                obj1.TAX9_Amt = clsCommon.myCdbl(gv1.Rows(i).Cells(colDTaxAmt9).Value)
                obj1.TAX10_Amt = clsCommon.myCdbl(gv1.Rows(i).Cells(colDTaxAmt10).Value)
                obj1.Total_Tax_Amt = (obj1.TAX1_Amt + obj1.TAX2_Amt + obj1.TAX3_Amt + obj1.TAX4_Amt + obj1.TAX5_Amt + obj1.TAX6_Amt + obj1.TAX7_Amt + obj1.TAX8_Amt + obj1.TAX9_Amt + obj1.TAX10_Amt)
                obj1.No_Of_Rows_Qty = clsCommon.myCdbl(gv1.Rows(i).Cells(colDBalanceQty).Value)
                obj1.No_Of_Rows_Qty_for_discount = clsCommon.myCdbl(gv1.Rows(i).Cells(colAmtAftrDis).Value)
                obj1.SRN_Rate = clsCommon.myCdbl(gv1.Rows(i).Cells(colDRate).Value)
                obj1.SRNQty = clsCommon.myCdbl(gv1.Rows(i).Cells(colDBalanceQty).Value)
                obj1.Unit_Code = clsCommon.myCstr(gv1.Rows(i).Cells(colDUnit).Value)
                obj1.SRN_No = clsCommon.myCstr(gv1.Rows(i).Cells(colDCode).Value)
                obj1.PI_No = clsCommon.myCstr(gv1.Rows(i).Cells(colPI).Value)
                obj.Arr.Add(obj1)
            End If
        Next

        Me.Close()

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
            Dim strPONO As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colDCode).Value)
            Dim SelectStatus As Boolean = clsCommon.myCBool(gv1.CurrentRow.Cells(colDSelect).Value)
            For ii As Integer = 0 To gv1.Rows.Count - 1
                If clsCommon.CompairString(strPONO, clsCommon.myCstr(gv1.Rows(ii).Cells(colDCode).Value)) = CompairStringResult.Equal Then
                    gv1.Rows(ii).Cells(colDSelect).Value = Not SelectStatus
                End If
            Next
        End If
    End Sub

    'Private Sub gvHead_ValueChanging(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.ValueChangingEventArgs) Handles gvHead.ValueChanging
    '    If gvHead.CurrentRow.Index >= 0 AndAlso Not IsInsideLoadData Then
    '        Dim strCode As String = clsCommon.myCstr(gvHead.CurrentRow.Cells(colHCode).Value)
    '        If clsCommon.myLen(strCode) > 0 Then
    '            LoadDetailData(e.NewValue, strCode)
    '        End If
    '    End If
    ' End Sub

    'Sub LoadDetailData(ByVal NewVal As Boolean, ByVal strCode As String)
    Sub LoadDetailData()
        IsInsideLoadData = True
        LoadBlankGridDetail()

        For Each dr As DataRow In dtAllData.Rows
            Dim strCode1 As String = clsCommon.myCstr(dr("SRN No"))


            gv1.Rows.AddNew()
            gv1.Rows(gv1.Rows.Count - 1).Cells(colDSelect).Value = False
            gv1.Rows(gv1.Rows.Count - 1).Cells(colDCode).Value = strCode1
            gv1.Rows(gv1.Rows.Count - 1).Cells(colHDate).Value = clsCommon.myCstr(dr("SRN Date"))
            gv1.Rows(gv1.Rows.Count - 1).Cells(colHVendorCode).Value = clsCommon.myCstr(dr("Vendor Code"))
            gv1.Rows(gv1.Rows.Count - 1).Cells(colHVendorName).Value = clsCommon.myCstr(dr("Vendor Name"))
            gv1.Rows(gv1.Rows.Count - 1).Cells(colDICode).Value = clsCommon.myCstr(dr("Item Code"))
            gv1.Rows(gv1.Rows.Count - 1).Cells(colDIName).Value = clsCommon.myCstr(dr("Item Name"))
            gv1.Rows(gv1.Rows.Count - 1).Cells(colDUnit).Value = clsCommon.myCstr(dr("Unit Code"))
            gv1.Rows(gv1.Rows.Count - 1).Cells(colDBalanceQty).Value = clsCommon.myCdbl(dr("Balance Qty"))

            gv1.Rows(gv1.Rows.Count - 1).Cells(colDPRQty).Value = clsCommon.myCdbl(dr("PR Qty"))
            gv1.Rows(gv1.Rows.Count - 1).Cells(colDReturnNo).Value = clsCommon.myCstr(dr("Return No"))
            gv1.Rows(gv1.Rows.Count - 1).Cells(colPIQty).Value = clsCommon.myCdbl(dr("PI Qty"))
            gv1.Rows(gv1.Rows.Count - 1).Cells(colHLocation).Value = clsCommon.myCstr(dr("Bill_To_Location"))
            gv1.Rows(gv1.Rows.Count - 1).Cells(colDLocation).Value = clsCommon.myCstr(dr("Location Name"))
            gv1.Rows(gv1.Rows.Count - 1).Cells(colDTaxGroup).Value = clsCommon.myCstr(dr("Tax_Group"))
            gv1.Rows(gv1.Rows.Count - 1).Cells(colDTaxGroupName).Value = clsCommon.myCstr(dr("Tax_Group_Desc"))
            gv1.Rows(gv1.Rows.Count - 1).Cells(colDTaxRate1).Value = clsCommon.myCdbl(dr("TAX1_Rate"))
            gv1.Rows(gv1.Rows.Count - 1).Cells(colDTaxRate2).Value = clsCommon.myCdbl(dr("TAX2_Rate"))
            gv1.Rows(gv1.Rows.Count - 1).Cells(colDTaxRate3).Value = clsCommon.myCdbl(dr("TAX3_Rate"))
            gv1.Rows(gv1.Rows.Count - 1).Cells(colDTaxRate4).Value = clsCommon.myCdbl(dr("TAX4_Rate"))
            gv1.Rows(gv1.Rows.Count - 1).Cells(colDTaxRate5).Value = clsCommon.myCdbl(dr("TAX5_Rate"))
            gv1.Rows(gv1.Rows.Count - 1).Cells(colDTaxRate6).Value = clsCommon.myCdbl(dr("TAX6_Rate"))
            gv1.Rows(gv1.Rows.Count - 1).Cells(colDTaxRate7).Value = clsCommon.myCdbl(dr("TAX7_Rate"))
            gv1.Rows(gv1.Rows.Count - 1).Cells(colDTaxRate8).Value = clsCommon.myCdbl(dr("TAX8_Rate"))
            gv1.Rows(gv1.Rows.Count - 1).Cells(colDTaxRate9).Value = clsCommon.myCdbl(dr("TAX9_Rate"))
            gv1.Rows(gv1.Rows.Count - 1).Cells(colDTaxRate10).Value = clsCommon.myCdbl(dr("TAX10_Rate"))
            gv1.Rows(gv1.Rows.Count - 1).Cells(colDTaxAmt1).Value = clsCommon.myCdbl(dr("TAX1_Amt"))
            gv1.Rows(gv1.Rows.Count - 1).Cells(colDTaxAmt2).Value = clsCommon.myCdbl(dr("TAX2_Amt"))
            gv1.Rows(gv1.Rows.Count - 1).Cells(colDTaxAmt3).Value = clsCommon.myCdbl(dr("TAX3_Amt"))
            gv1.Rows(gv1.Rows.Count - 1).Cells(colDTaxAmt4).Value = clsCommon.myCdbl(dr("TAX4_Amt"))
            gv1.Rows(gv1.Rows.Count - 1).Cells(colDTaxAmt5).Value = clsCommon.myCdbl(dr("TAX5_Amt"))
            gv1.Rows(gv1.Rows.Count - 1).Cells(colDTaxAmt6).Value = clsCommon.myCdbl(dr("TAX6_Amt"))
            gv1.Rows(gv1.Rows.Count - 1).Cells(colDTaxAmt7).Value = clsCommon.myCdbl(dr("TAX7_Amt"))
            gv1.Rows(gv1.Rows.Count - 1).Cells(colDTaxAmt8).Value = clsCommon.myCdbl(dr("TAX8_Amt"))
            gv1.Rows(gv1.Rows.Count - 1).Cells(colDTaxAmt9).Value = clsCommon.myCdbl(dr("TAX9_Amt"))
            gv1.Rows(gv1.Rows.Count - 1).Cells(colDTaxAmt10).Value = clsCommon.myCdbl(dr("TAX10_Amt"))
            gv1.Rows(gv1.Rows.Count - 1).Cells(colAmtAftrDis).Value = clsCommon.myCdbl(dr("Amt_Less_Discount"))
            gv1.Rows(gv1.Rows.Count - 1).Cells(colDRate).Value = clsCommon.myCdbl(dr("Item_Cost"))
            gv1.Rows(gv1.Rows.Count - 1).Cells(colPI).Value = clsCommon.myCstr(dr("PI No"))

        Next
        IsInsideLoadData = False
    End Sub

    Private Function isAllowed() As Boolean

        For i As Integer = 0 To gv1.Rows.Count - 1
            If clsCommon.myCBool(gv1.Rows(i).Cells(colDSelect).Value) Then
                Dim FirstSRNNo As String = clsCommon.myCstr(gv1.Rows(i).Cells(colDCode).Value)
                For j As Integer = i + 1 To gv1.Rows.Count - 1
                    If clsCommon.myCBool(gv1.Rows(j).Cells(colDSelect).Value) Then
                        Dim SecondSRNNo As String = clsCommon.myCstr(gv1.Rows(j).Cells(colDCode).Value)
                        If clsCommon.myCBool(gv1.Rows(j).Cells(colDSelect).Value) And clsCommon.CompairString(FirstSRNNo, SecondSRNNo) = CompairStringResult.Equal Then
                        Else
                            clsCommon.MyMessageBoxShow("More than one SRN No. not acceptable ")
                            Return False
                        End If
                    End If
                Next
            End If
        Next
        Return True
    End Function


End Class

