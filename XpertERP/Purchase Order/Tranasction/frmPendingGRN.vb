Imports common
Public Class frmPendingGRN

#Region "Variables"
    Public VendorCode As String = Nothing
    Public VendorName As String = Nothing
    Public strCurrCode As String = Nothing
    Public PurchaseOrder_Type As String = Nothing
    Public ArrReturn As List(Of clsGRNDetail) = Nothing
    ''Public objGRNHead As clsGRNHead = Nothing

    Dim ShowCapexCodeandSubCode As Boolean = False
    Dim dtAllData As DataTable = Nothing
    Dim IsInsideLoadData As Boolean = False

    Const colDSelect As String = "SELECT"
    Const colDCode As String = "CODE"
    Const colDPOID As String = "colDPOID"
    Const colDICode As String = "ICODE"
    Const colDIName As String = "INAME"
    Const colDIType As String = "IType"
    Const colDTaxGroup As String = "TAXGROUP"
    Const colDTaxGroupName As String = "TAXGROUPNAME"
    Const colDUnit As String = "UNIT"
    Const colDRate As String = "RATE"
    Const colDOrderQty As String = "ORDERQTY"
    Const colDApprovedQty As String = "APPROVEDQTY"
    Const colDUnApprovedQty As String = "UNAPPROVEDQTY"
    Const colDPendingQty As String = "PENDINGQTY"
    Const colDLeakQty As String = "LEAKQTY"
    Const colDBurstQty As String = "BURSTQTY"
    Const colDShortQty As String = "SHORTQTY"
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
    Const colDHeaderDisPer As String = "colDHeaderDisPer"
    Const colDAgainstItemWiseTaxCode As String = "colDAgainstItemWiseTaxCode"
    Const colDTaxableAmountPer As String = "colDTaxableAmountPer"
    Const colDMRP As String = "MRP"
    Const colDAssessable As String = "ASSESSABLE"
    Const colDBatchNo As String = "BATCHNO"
    Const colDManDate As String = "MANFACTURERDATE"
    Const colDExpiryDate As String = "EXPIRYDATE"
    Const colDDisPer As String = "DISCOUNTPER"
    Const colItemInsuranceApplyOn As String = "colItemInsuranceApplyOn"
    Const colItemInsurancePer As String = "colItemInsurancePer"
    Const colItemInsuranceAmt As String = "colItemInsuranceAmt"


    Const colHSelect As String = "SELECT"
    Const colHCode As String = "CODE"
    Const colHDate As String = "DATE"
    Const colHVendorCode As String = "VENDOR"
    Const colHVendorName As String = "VENDORNAME"
    Const colHVisualQCStatus As String = "VisualQCStatus"
    Const colHVisualQCStatusSecond As String = "VisualQCStatusSecond"
    Const colHPOType As String = "POTYPE"
    Const colCategoryType As String = "COLCATEGORYTYPE"
    Const colEmergency As String = "COLEMERGENCY"
    Const colCapexSubCode As String = "COLCAPEXSUBCODE"
    Const colCapexCode As String = "COLCAPEXCODE"
#End Region

    Private Sub FrmPendingRequistion_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ShowCapexCodeandSubCode = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.ShowOptionforSelectingCapex, clsFixedParameterCode.ShowOptionforSelectingCapex, Nothing)) = "1", True, False))

        If clsCommon.myLen(VendorName) > 0 Then
            Me.Text = Me.Text + " For " + VendorName
        End If
        Dim MRNType As String = ""
        If clsCommon.myLen(PurchaseOrder_Type) > 0 Then
            MRNType = " and TSPL_MRN_HEAD.PurchaseOrder_Type='" + PurchaseOrder_Type + "' "
        End If

        Dim qry As String = "select CAST(0 as bit) as Sel,code,max(Final.Tax_Group) as Tax_Group,max(TSPL_TAX_GROUP_MASTER.Tax_Group_Desc) as TaxGroupName,ICode,max(IName) as IName,MAX(IType) as IType,Unit as Unit,max(Location) as Location,MAX(TSPL_LOCATION_MASTER.Location_Desc) as LocationName," &
        " SUM(Qty* case when RI=1 then 1 else 0 end) as POQty," &
        " SUM(Qty* case when RI=-1 then 1 else 0 end) as GRNQty," &
        " SUM(Unapproved) as UnapprovedQty," &
        " SUM((Qty *RI)- Unapproved) as PedningQty ,MAX(Rate) as Rate," &
        " MAX(Final.TAX1_Rate) as TAX1_Rate,MAX(Final.TAX2_Rate) as TAX2_Rate,MAX(Final.TAX3_Rate) as TAX3_Rate,MAX(Final.TAX4_Rate) as TAX4_Rate,MAX(Final.TAX5_Rate) as TAX5_Rate,MAX(Final.TAX6_Rate) as TAX6_Rate,MAX(Final.TAX7_Rate) as TAX7_Rate,MAX(Final.TAX8_Rate) as TAX8_Rate,MAX(Final.TAX9_Rate) as TAX9_Rate,MAX(Final.TAX10_Rate) as TAX10_Rate,Final.MRP as MRP,max(Final.Batch_No) as Batch_No,max(Final.MFG_Date) as MFG_Date,max(Final.Expiry_Date) as Expiry_Date,max(Disc_Per) as Disc_Per,max(Carrier) as Carrier,max(VehicleNo) as VehicleNo,max(GRNo) as GRNo,max(GENo) as GENo,max(GEDate) as GEDate,max(Ref_No) as Ref_No,max(Description) as Description,max(Remarks) as Remarks,max(Bill_To_Location) as Bill_To_Location,max(Ship_To_Location) as Ship_To_Location,max(TransDate) as TransDate,MAX(Vendor) as Vendor,MAX(TSPL_VENDOR_MASTER.Vendor_Name) as VendorName,0 as Assessable,max(Leak_Qty)  as Leak_Qty,max(Burst_Qty) as Burst_Qty,max(Short_Qty) as Short_Qty,PO_Id,Against_RGP_No,max(Category) as category,max(Emergency) as emergency,max(Capex_Code) as capex_code,max(Capex_SubCode) as capex_subcode,max(Against_Item_Wise_Tax_Rate) as Against_Item_Wise_Tax_Rate,max(Taxable_Amount_Per) as Taxable_Amount_Per,max(Header_Discount_Per) as Header_Discount_Per,max(VisualQCStatus) as VisualQCStatus,max(VisualQCStatusSecond) as VisualQCStatusSecond, MAX(Final.TAX1_Amt) as TAX1_Amt,MAX(Final.TAX2_Amt) as TAX2_Amt,MAX(Final.TAX3_Amt) as TAX3_Amt,MAX(Final.TAX4_Amt) as TAX4_Amt,MAX(Final.TAX5_Amt) as TAX5_Amt,MAX(Final.TAX6_Amt) as TAX6_Amt,MAX(Final.TAX7_Amt) as TAX7_Amt,MAX(Final.TAX8_Amt) as TAX8_Amt,MAX(Final.TAX9_Amt) as TAX9_Amt,MAX(Final.TAX10_Amt) as TAX10_Amt,Max(Final.Item_Insurance_Apply_On) as Item_Insurance_Apply_On,Max(Final.Item_Insurance_Rate) as Item_Insurance_Rate,Max(Final.Item_Insurance_Amt) as Item_Insurance_Amt from ( " + Environment.NewLine &
        "  select case when TSPL_GRN_HEAD.VisualQCStatus=1 then 'Ok' when TSPL_GRN_HEAD.VisualQCStatus='2' then 'Not Ok' when TSPL_GRN_HEAD.VisualQCStatus='3' then 'Partial Ok'  when TSPL_GRN_HEAD.VisualQCStatus='4' then 'On Hold' else ' ' end as [VisualQCStatus], case when TSPL_GRN_HEAD.VisualQCStatusSecond=1 then 'Ok' when TSPL_GRN_HEAD.VisualQCStatusSecond='2' then 'Not Ok' when TSPL_GRN_HEAD.VisualQCStatusSecond='3' then 'Partial Ok'  when TSPL_GRN_HEAD.VisualQCStatusSecond='4' then 'On Hold' else ' ' end as [VisualQCStatusSecond],
           TSPL_GRN_DETAIL.Header_Discount_Per,TSPL_GRN_DETAIL.Taxable_Amount_Per, TSPL_GRN_DETAIL.Against_Item_Wise_Tax_Rate, TSPL_GRN_DETAIL.Category,TSPL_GRN_DETAIL.Emergency,TSPL_GRN_DETAIL.Capex_Code,TSPL_GRN_DETAIL.Capex_SubCode,TSPL_GRN_DETAIL.PO_Id,TSPL_GRN_DETAIL.Against_RGP_No,TSPL_GRN_DETAIL.GRN_No as Code,TSPL_GRN_HEAD.Vendor_Code as Vendor,TSPL_GRN_DETAIL.Item_Code as ICode,TSPL_GRN_DETAIL.Item_Desc as IName,TSPL_GRN_DETAIL.Row_Type as IType,(TSPL_GRN_DETAIL.GRN_Qty+isnull(TSPL_GRN_DETAIL.Leak_Qty,0)+isnull(TSPL_GRN_DETAIL.Burst_Qty,0)+isnull(TSPL_GRN_DETAIL.Short_Qty,0)) as Qty,0 as Unapproved,TSPL_GRN_DETAIL.Unit_Code as Unit,TSPL_GRN_DETAIL.Location as Location,1.0 as RI,TSPL_GRN_DETAIL.Item_Cost as Rate,1 as Chk,TSPL_GRN_HEAD.Tax_Group,TSPL_GRN_DETAIL.TAX1_Rate,TSPL_GRN_DETAIL.TAX2_Rate,TSPL_GRN_DETAIL.TAX3_Rate,TSPL_GRN_DETAIL.TAX4_Rate,TSPL_GRN_DETAIL.TAX5_Rate,TSPL_GRN_DETAIL.TAX6_Rate,TSPL_GRN_DETAIL.TAX7_Rate,TSPL_GRN_DETAIL.TAX8_Rate,TSPL_GRN_DETAIL.TAX9_Rate,TSPL_GRN_DETAIL.TAX10_Rate,TSPL_GRN_DETAIL.MRP,TSPL_GRN_DETAIL.Batch_No,TSPL_GRN_DETAIL.MFG_Date,TSPL_GRN_DETAIL.Expiry_Date,TSPL_GRN_DETAIL.Disc_Per,TSPL_GRN_HEAD.Carrier,TSPL_GRN_HEAD.VehicleNo,TSPL_GRN_HEAD.GRNo,TSPL_GRN_HEAD.GENo,TSPL_GRN_HEAD.GEDate,TSPL_GRN_HEAD.Ref_No,TSPL_GRN_HEAD.Description,TSPL_GRN_HEAD.Remarks,TSPL_GRN_HEAD.Bill_To_Location,TSPL_GRN_HEAD.Ship_To_Location,TSPL_GRN_HEAD.GRN_Date as TransDate,ISNULL( TSPL_GRN_DETAIL.Assessable,0) as Assessable,TSPL_GRN_DETAIL.Leak_Qty,TSPL_GRN_DETAIL.Burst_Qty,TSPL_GRN_DETAIL.Short_Qty,TSPL_GRN_DETAIL.TAX1_Amt,TSPL_GRN_DETAIL.TAX2_Amt,TSPL_GRN_DETAIL.TAX3_Amt,TSPL_GRN_DETAIL.TAX4_Amt,TSPL_GRN_DETAIL.TAX5_Amt,TSPL_GRN_DETAIL.TAX6_Amt,TSPL_GRN_DETAIL.TAX7_Amt,TSPL_GRN_DETAIL.TAX8_Amt,TSPL_GRN_DETAIL.TAX9_Amt,TSPL_GRN_DETAIL.TAX10_Amt,TSPL_GRN_DETAIL.Item_Insurance_Apply_On,TSPL_GRN_DETAIL.Item_Insurance_Rate,TSPL_GRN_DETAIL.Item_Insurance_Amt from TSPL_GRN_DETAIL left outer join TSPL_GRN_HEAD on TSPL_GRN_HEAD.GRN_No=TSPL_GRN_DETAIL.GRN_No left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_GRN_DETAIL.Item_Code where TSPL_GRN_DETAIL.Status=0 and TSPL_GRN_HEAD.Status=1 and TSPL_GRN_HEAD.IsCancel=0  and TSPL_ITEM_MASTER.Is_Auto_Weighment=0 "

        If clsCommon.myLen(VendorCode) > 0 Then
            qry += "and TSPL_GRN_HEAD.Vendor_Code='" + VendorCode + "'" + Environment.NewLine
        End If
        If clsCommon.myLen(PurchaseOrder_Type) > 0 Then
            qry += "and TSPL_GRN_HEAD.PurchaseOrder_Type='" + PurchaseOrder_Type + "'" + Environment.NewLine
        End If
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            qry += "  and TSPL_GRN_HEAD.Bill_To_Location in (" + objCommonVar.strCurrUserLocations + ")"
        End If
        If objCommonVar.RCDFCFP = True Then
            qry += " And TSPL_GRN_HEAD.VisualQCStatus IN (1,3,5) "
        End If
        qry += " union all" + Environment.NewLine &
        " select '' as  VisualQCStatus,'' as VisualQCStatusSecond, 0 as Header_Discount_Per,0 as Taxable_Amount_Per,'' as Against_Item_Wise_Tax_Rate, TSPL_MRN_DETAIL.Category,TSPL_MRN_DETAIL.Emergency,TSPL_MRN_DETAIL.Capex_Code,TSPL_MRN_DETAIL.Capex_SubCode,TSPL_MRN_DETAIL.PO_ID,TSPL_MRN_DETAIL.RGP_No,TSPL_MRN_DETAIL.GRN_ID as Code,TSPL_MRN_HEAD.Vendor_Code as Vendor,TSPL_MRN_DETAIL.Item_Code as ICode,TSPL_MRN_DETAIL.Item_Desc as IName,'' as IType,(TSPL_MRN_DETAIL.MRN_Qty+isnull(TSPL_MRN_DETAIL.Leak_Qty,0)+isnull(TSPL_MRN_DETAIL.Burst_Qty,0)+isnull(TSPL_MRN_DETAIL.Short_Qty,0)-isnull(TSPL_MRN_DETAIL.Excess_Qty,0)) as Qty,0 as Unapproved,TSPL_MRN_DETAIL.Unit_code as Unit,'' as Location,-1 as RI,0 as Rate,0 as Chk,'' as Tax_Group,0 as TAX1_Rate,0 as TAX2_Rate,0 as TAX3_Rate,0 as TAX4_Rate,0 as TAX5_Rate,0 as TAX6_Rate,0 as TAX7_Rate,0 as TAX8_Rate,0 as TAX9_Rate,0 as TAX10_Rate,isnull(TSPL_MRN_DETAIL.MRP,0) as MRP,'' as Batch_No,null as MFG_Date,null as Expiry_Date,0 as Disc_Per,null as Carrier,null as VehicleNo,null as GRNo,null as GENo,null as GEDate,null as Ref_No,null as Description,null as Remarks,null as Bill_To_Location,null as Ship_To_Location,null as TransDate,isnull(TSPL_MRN_DETAIL.Assessable,0) as Assessable,0  as Leak_Qty,0 as Burst_Qty,0 as Short_Qty,0 as  TAX1_Amt,0 as  TAX2_Amt,0 as  TAX3_Amt,0 as  TAX4_Amt,0 as  TAX5_Amt,0 as  TAX6_Amt,0 as  TAX7_Amt,0 as  TAX8_Amt,0 as  TAX9_Amt,0 as  TAX10_Amt,'' as Item_Insurance_Apply_On,0 as Item_Insurance_Rate,0 as Item_Insurance_Amt from TSPL_MRN_DETAIL left outer join TSPL_MRN_HEAD on TSPL_MRN_HEAD.MRN_No=TSPL_MRN_DETAIL.MRN_No where TSPL_MRN_HEAD.Status=1 and len(isnull(TSPL_MRN_DETAIL.GRN_Id,''))>0 and TSPL_MRN_HEAD.IsCancel=0 " + MRNType + "  " + Environment.NewLine &
        " union all  " + Environment.NewLine &
        " select '' as  VisualQCStatus,'' as VisualQCStatusSecond, 0 as Header_Discount_Per,0 as Taxable_Amount_Per,'' as Against_Item_Wise_Tax_Rate,TSPL_MRN_DETAIL.Category,TSPL_MRN_DETAIL.Emergency,TSPL_MRN_DETAIL.Capex_Code,TSPL_MRN_DETAIL.Capex_SubCode,TSPL_MRN_DETAIL.PO_ID,TSPL_MRN_DETAIL.RGP_No,TSPL_MRN_DETAIL.GRN_ID as Code,TSPL_MRN_HEAD.Vendor_Code as Vendor,TSPL_MRN_DETAIL.Item_Code as ICode,TSPL_MRN_DETAIL.Item_Desc as IName,'' as IType,0  as Qty,(TSPL_MRN_DETAIL.MRN_Qty+isnull(TSPL_MRN_DETAIL.Leak_Qty,0)+isnull(TSPL_MRN_DETAIL.Burst_Qty,0)+isnull(TSPL_MRN_DETAIL.Short_Qty,0)-isnull(TSPL_MRN_DETAIL.Excess_Qty,0)) as Unapproved,TSPL_MRN_DETAIL.Unit_code as Unit,'' as Location,-1 as RI,0 as Rate,0 as Chk,'' as Tax_Group,0 as TAX1_Rate,0 as TAX2_Rate,0 as TAX3_Rate,0 as TAX4_Rate,0 as TAX5_Rate,0 as TAX6_Rate,0 as TAX7_Rate,0 as TAX8_Rate,0 as TAX9_Rate,0 as TAX10_Rate,isnull(TSPL_MRN_DETAIL.MRP,0) as MRP,'' as Batch_No,null as MFG_Date,null as Expiry_Date,0 as Disc_Per,null as Carrier,null as VehicleNo,null as GRNo,null as GENo,null as GEDate,null as Ref_No,null as Description,null as Remarks,null as Bill_To_Location,null as Ship_To_Location,null as TransDate,isnull(TSPL_MRN_DETAIL.Assessable,0) as Assessable,0  as Leak_Qty,0 as Burst_Qty,0 as Short_Qty,0 as  TAX1_Amt,0 as  TAX2_Amt,0 as  TAX3_Amt,0 as  TAX4_Amt,0 as  TAX5_Amt,0 as  TAX6_Amt,0 as  TAX7_Amt,0 as  TAX8_Amt,0 as  TAX9_Amt,0 as  TAX10_Amt,'' as Item_Insurance_Apply_On,0 as Item_Insurance_Rate,0 as Item_Insurance_Amt from TSPL_MRN_DETAIL left outer join TSPL_MRN_HEAD on TSPL_MRN_HEAD.MRN_No=TSPL_MRN_DETAIL.MRN_No  where TSPL_MRN_HEAD.Status=0 and len(isnull(TSPL_MRN_DETAIL.GRN_Id,''))>0 and TSPL_MRN_DETAIL.MRN_No not in ('" + strCurrCode + "') and TSPL_MRN_HEAD.IsCancel=0 " + MRNType + "  " + Environment.NewLine &
        " )Final left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=final.Vendor left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=Final.Location left outer join TSPL_TAX_GROUP_MASTER on TSPL_TAX_GROUP_MASTER.Tax_Group_Code=Final.Tax_Group and TSPL_TAX_GROUP_MASTER.Tax_Group_Type='P' "

        qry += " group by PO_ID,Code,ICode,Unit,MRP,Against_RGP_No having SUM(Chk)>0 and SUM(Qty *RI) <>0 order by Code,PO_ID,ICode "
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
            Dim strCode As String = clsCommon.myCstr(dr("code"))
            If Not arr.Contains(strCode) Then
                arr.Add(strCode)
                gvHead.Rows.AddNew()
                gvHead.Rows(gvHead.RowCount - 1).Cells(colHSelect).Value = False
                gvHead.Rows(gvHead.RowCount - 1).Cells(colHCode).Value = strCode
                gvHead.Rows(gvHead.RowCount - 1).Cells(colHDate).Value = clsCommon.myCstr(dr("TransDate"))
                gvHead.Rows(gvHead.RowCount - 1).Cells(colHVendorCode).Value = clsCommon.myCstr(dr("Vendor"))
                gvHead.Rows(gvHead.RowCount - 1).Cells(colHVendorName).Value = clsCommon.myCstr(dr("VendorName"))
                gvHead.Rows(gvHead.RowCount - 1).Cells(colHVisualQCStatus).Value = clsCommon.myCstr(dr("VisualQCStatus"))
                gvHead.Rows(gvHead.RowCount - 1).Cells(colHVisualQCStatusSecond).Value = clsCommon.myCstr(dr("VisualQCStatusSecond"))
                gvHead.Rows(gvHead.RowCount - 1).Cells(colHPOType).Value = clsPurchaseOrderHead.GetPurchaseTypeName(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select purchaseorder_type from tspl_grn_head where grn_no='" + strCode + "'")))
            End If
        Next
        IsInsideLoadData = False
    End Sub

    Private Function isAllowed() As Boolean

        Dim arrVendor As New List(Of String)
        Dim arrPOType As New List(Of String)
        For ii As Integer = 0 To gvHead.RowCount - 1
            If clsCommon.myCBool(gvHead.Rows(ii).Cells(colHSelect).Value) Then
                Dim strCode As String = clsCommon.myCstr(gvHead.Rows(ii).Cells(colHVendorCode).Value)
                Dim strPOType As String = clsCommon.myCstr(gvHead.Rows(ii).Cells(colHPOType).Value)
                VendorCode = strCode
                VendorName = clsCommon.myCstr(gvHead.Rows(ii).Cells(colHVendorName).Value)

                For jj As Integer = ii + 1 To gvHead.RowCount - 1
                    If clsCommon.myCBool(gvHead.Rows(jj).Cells(colHSelect).Value) Then
                        If clsCommon.CompairString(strCode, clsCommon.myCstr(gvHead.Rows(jj).Cells(colHVendorCode).Value)) <> CompairStringResult.Equal Then
                            arrVendor.Add(clsCommon.myCstr(gvHead.Rows(jj).Cells(colHVendorCode).Value))
                        End If
                        If clsCommon.CompairString(strPOType, clsCommon.myCstr(gvHead.Rows(jj).Cells(colHPOType).Value)) <> CompairStringResult.Equal Then
                            arrPOType.Add(clsCommon.myCstr(gvHead.Rows(jj).Cells(colHPOType).Value))
                        End If
                    End If '===detail and head doc no cond.

                Next '==detail for loop

                If arrVendor.Count > 0 Then
                    clsCommon.MyMessageBoxShow("Item more than one vendor are not allowed.")
                    Return False
                End If
                If arrPOType.Count > 0 Then
                    clsCommon.MyMessageBoxShow("Item more than one type are not allowed.")
                    Return False
                End If

                Return True
            End If '==check status of head

        Next '===head for loop
        Return True
    End Function

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
        repoCode.HeaderText = "GRN No"
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

        Dim repoVisualQCStatus As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoVisualQCStatus.FormatString = ""
        repoVisualQCStatus.HeaderText = "VisualQCStatus"
        repoVisualQCStatus.Name = colHVisualQCStatus
        repoVisualQCStatus.Width = 170
        repoVisualQCStatus.ReadOnly = True
        gvHead.MasterTemplate.Columns.Add(repoVisualQCStatus)

        Dim repoVisualQCStatusSecond As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoVisualQCStatusSecond.FormatString = ""
        repoVisualQCStatusSecond.HeaderText = "VisualQCStatusSecond"
        repoVisualQCStatusSecond.Name = colHVisualQCStatusSecond
        repoVisualQCStatusSecond.Width = 170
        repoVisualQCStatusSecond.ReadOnly = True
        gvHead.MasterTemplate.Columns.Add(repoVisualQCStatusSecond)

        repoVendorName = New GridViewTextBoxColumn()
        repoVendorName.FormatString = ""
        repoVendorName.HeaderText = "Type"
        repoVendorName.Name = colHPOType
        repoVendorName.Width = 100
        repoVendorName.ReadOnly = True
        gvHead.MasterTemplate.Columns.Add(repoVendorName)

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
        repoCode.HeaderText = "GRN No"
        repoCode.Name = colDCode
        repoCode.Width = 180
        repoCode.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoCode)

        repoCode = New GridViewTextBoxColumn()
        repoCode.FormatString = ""
        repoCode.HeaderText = "PO ID"
        repoCode.Name = colDPOID
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
        repoIName.HeaderText = "Item"
        repoIName.Name = colDIName
        repoIName.Width = 180
        repoIName.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoIName)


        Dim repoIType As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoIType.FormatString = ""
        repoIType.HeaderText = "Row Type"
        repoIType.Name = colDIType
        repoIType.Width = 180
        repoIType.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoIType)

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
        repoRate.IsVisible = False
        repoRate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoRate)

        Dim repoOrderQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoOrderQty.FormatString = ""
        repoOrderQty.HeaderText = "GRN Qty"
        repoOrderQty.Name = colDOrderQty
        repoOrderQty.ReadOnly = True
        repoOrderQty.Width = 80
        repoOrderQty.WrapText = True
        repoOrderQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoOrderQty)

        Dim repoAppQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAppQty.FormatString = ""
        repoAppQty.HeaderText = "Used in MRN"
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

        Dim repoMRP As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoMRP.FormatString = ""
        repoMRP.HeaderText = "MRP"
        repoMRP.Name = colDMRP
        repoMRP.ReadOnly = True
        repoMRP.IsVisible = False
        repoMRP.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoMRP)

        Dim repoAssessable As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAssessable.FormatString = ""
        repoAssessable.HeaderText = "Assessable"
        repoAssessable.Name = colDAssessable
        repoAssessable.ReadOnly = True
        repoAssessable.IsVisible = False
        repoAssessable.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoAssessable)


        Dim repoBatchNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoBatchNo.FormatString = ""
        repoBatchNo.HeaderText = "Batch No"
        repoBatchNo.Name = colDBatchNo
        repoBatchNo.IsVisible = False
        repoBatchNo.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoBatchNo)

        Dim repoExpiry As GridViewDateTimeColumn = New GridViewDateTimeColumn()
        repoExpiry.Format = DateTimePickerFormat.Custom
        repoExpiry.CustomFormat = "dd-MM-yyyy"
        repoExpiry.HeaderText = "Expiry Date"
        repoExpiry.FormatString = "{0:d}"
        repoExpiry.Name = colDExpiryDate
        repoExpiry.WrapText = True
        repoExpiry.ReadOnly = True
        repoExpiry.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoExpiry)

        Dim repoManDate As GridViewDateTimeColumn = New GridViewDateTimeColumn()
        repoManDate.Format = DateTimePickerFormat.Custom
        repoManDate.CustomFormat = "dd-MM-yyyy"
        repoManDate.HeaderText = "Manufacturer Date"
        repoManDate.WrapText = True
        repoManDate.FormatString = "{0:d}"
        repoManDate.Name = colDManDate
        repoManDate.ReadOnly = True
        repoManDate.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoManDate)

        Dim repoDiscPer As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoDiscPer.FormatString = ""
        repoDiscPer.HeaderText = "Discount Per"
        repoDiscPer.Name = colDDisPer
        repoDiscPer.ReadOnly = True
        repoDiscPer.IsVisible = False
        repoDiscPer.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoDiscPer)


        Dim repoLeakQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoLeakQty.FormatString = ""
        repoLeakQty.HeaderText = "Leakage"
        repoLeakQty.Name = colDLeakQty
        repoLeakQty.ReadOnly = True
        repoLeakQty.IsVisible = False
        repoLeakQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoLeakQty)

        Dim repoBurst As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoBurst.FormatString = ""
        repoBurst.HeaderText = "Burst"
        repoBurst.Name = colDBurstQty
        repoBurst.ReadOnly = True
        repoBurst.IsVisible = False
        repoBurst.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoBurst)

        Dim repoShort As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoShort.FormatString = ""
        repoShort.HeaderText = "Shortage"
        repoShort.Name = colDShortQty
        repoShort.ReadOnly = True
        repoShort.IsVisible = False
        repoShort.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoShort)

        ''done by stuti on 20/10/2016 against purchase points
        Dim repoCategoryType As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCategoryType.FormatString = ""
        repoCategoryType.HeaderText = "Category Type"
        repoCategoryType.Name = colCategoryType
        repoCategoryType.Width = 50
        repoCategoryType.IsVisible = ShowCapexCodeandSubCode
        repoCategoryType.VisibleInColumnChooser = ShowCapexCodeandSubCode
        gv1.MasterTemplate.Columns.Add(repoCategoryType)

        Dim repoEmergency As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoEmergency.Checked = ToggleState.Off
        repoEmergency.HeaderText = "Emergency"
        repoEmergency.Name = colEmergency
        repoEmergency.Width = 50
        repoEmergency.IsVisible = ShowCapexCodeandSubCode
        repoEmergency.VisibleInColumnChooser = ShowCapexCodeandSubCode
        gv1.MasterTemplate.Columns.Add(repoEmergency)

        Dim repoCapexSubCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCapexSubCode.FormatString = ""
        repoCapexSubCode.HeaderText = "Capex Sub Code"
        repoCapexSubCode.Name = colCapexSubCode
        repoCapexSubCode.Width = 100
        repoCapexSubCode.IsVisible = ShowCapexCodeandSubCode
        repoCapexSubCode.VisibleInColumnChooser = ShowCapexCodeandSubCode
        gv1.MasterTemplate.Columns.Add(repoCapexSubCode)

        Dim repoCapexCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCapexCode.FormatString = ""
        repoCapexCode.HeaderText = "Capex Code"
        repoCapexCode.Name = colCapexCode
        repoCapexCode.Width = 100
        repoCapexCode.IsVisible = ShowCapexCodeandSubCode
        repoCapexCode.VisibleInColumnChooser = ShowCapexCodeandSubCode
        gv1.MasterTemplate.Columns.Add(repoCapexCode)

        repoICode = New GridViewTextBoxColumn()
        repoICode.FormatString = ""
        repoICode.HeaderText = "Against Item Wise Tax Code"
        repoICode.Name = colDAgainstItemWiseTaxCode
        repoICode.ReadOnly = True
        repoICode.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoICode)

        repoPendingQty = New GridViewDecimalColumn()
        repoPendingQty.FormatString = ""
        repoPendingQty.HeaderText = "Taxable Amount Per"
        repoPendingQty.Name = colDTaxableAmountPer
        repoPendingQty.ReadOnly = True
        repoPendingQty.IsVisible = False
        repoPendingQty.WrapText = True
        repoPendingQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoPendingQty)

        repoPendingQty = New GridViewDecimalColumn()
        repoPendingQty.FormatString = ""
        repoPendingQty.HeaderText = "Header Discount %"
        repoPendingQty.Name = colDHeaderDisPer
        repoPendingQty.ReadOnly = True
        repoPendingQty.IsVisible = False
        repoPendingQty.WrapText = True
        repoPendingQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoPendingQty)

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


        repoCapexSubCode = New GridViewTextBoxColumn()
        repoCapexSubCode.FormatString = ""
        repoCapexSubCode.HeaderText = "Item Insurance Apply On"
        repoCapexSubCode.Name = colItemInsuranceApplyOn
        repoCapexSubCode.Width = 100
        repoCapexSubCode.IsVisible = False
        repoCapexSubCode.ReadOnly = True
        repoCapexSubCode.VisibleInColumnChooser = ShowCapexCodeandSubCode
        gv1.MasterTemplate.Columns.Add(repoCapexSubCode)

        Dim AbatementRate As New GridViewDecimalColumn()
        AbatementRate.FormatString = ""
        AbatementRate.HeaderText = "Item Insurance %"
        AbatementRate.Name = colItemInsurancePer
        AbatementRate.ReadOnly = True
        AbatementRate.IsVisible = False
        AbatementRate.WrapText = True
        AbatementRate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(AbatementRate)

        AbatementRate = New GridViewDecimalColumn()
        AbatementRate.FormatString = ""
        AbatementRate.HeaderText = "Item Insurance Amt"
        AbatementRate.Name = colItemInsuranceAmt
        AbatementRate.ReadOnly = True
        AbatementRate.IsVisible = False
        AbatementRate.WrapText = True
        AbatementRate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(AbatementRate)

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
        ArrReturn = New List(Of clsGRNDetail)

        If Not isAllowed() Then
            Exit Sub
        End If

        Dim obj As clsGRNDetail = Nothing
        For ii As Integer = 0 To gv1.RowCount - 1
            If (clsCommon.myCBool(gv1.Rows(ii).Cells(colDSelect).Value)) Then
                obj = New clsGRNDetail()

                obj.Category = clsCommon.myCstr(gv1.Rows(ii).Cells(colCategoryType).Value)
                obj.Emergency = CInt(gv1.Rows(ii).Cells(colEmergency).Value)
                obj.Capex_SubCode = clsCommon.myCstr(gv1.Rows(ii).Cells(colCapexSubCode).Value)
                obj.Capex_Code = clsCommon.myCstr(gv1.Rows(ii).Cells(colCapexCode).Value)

                obj.GRN_No = clsCommon.myCstr(gv1.Rows(ii).Cells(colDCode).Value)
                obj.PO_Id = clsCommon.myCstr(gv1.Rows(ii).Cells(colDPOID).Value)
                obj.Item_Code = clsCommon.myCstr(gv1.Rows(ii).Cells(colDICode).Value)
                obj.Item_Desc = clsCommon.myCstr(gv1.Rows(ii).Cells(colDIName).Value)
                obj.Row_Type = clsCommon.myCstr(gv1.Rows(ii).Cells(colDIType).Value)
                obj.Item_Cost = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDRate).Value)
                obj.Unit_code = clsCommon.myCstr(gv1.Rows(ii).Cells(colDUnit).Value)
                ''obj.Location = clsCommon.myCstr(gv1.Rows(ii).Cells(cold).Value)
                ''obj.LocationName = clsCommon.myCstr(gv1.Rows(ii).Cells("LocationName").Value)

                obj.Balance_Qty = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDPendingQty).Value)
                obj.GRN_Qty = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDOrderQty).Value)
                obj.Leak_Qty = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDLeakQty).Value)
                obj.Burst_Qty = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDBurstQty).Value)
                obj.Short_Qty = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDShortQty).Value)
                obj.Taxable_Amount_Per = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDTaxableAmountPer).Value)
                obj.GRNTax_Group = clsCommon.myCstr(gv1.Rows(ii).Cells(colDTaxGroup).Value)
                obj.TAX1_Rate = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDTaxRate1).Value)
                obj.TAX2_Rate = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDTaxRate2).Value)
                obj.TAX3_Rate = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDTaxRate3).Value)
                obj.TAX4_Rate = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDTaxRate4).Value)
                obj.TAX5_Rate = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDTaxRate5).Value)
                obj.TAX6_Rate = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDTaxRate6).Value)
                obj.TAX7_Rate = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDTaxRate7).Value)
                obj.TAX8_Rate = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDTaxRate8).Value)
                obj.TAX9_Rate = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDTaxRate9).Value)
                obj.TAX10_Rate = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDTaxRate10).Value)
                obj.TAX1_Amt = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDTaxAmt1).Value)
                obj.TAX2_Amt = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDTaxAmt2).Value)
                obj.TAX3_Amt = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDTaxAmt3).Value)
                obj.TAX4_Amt = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDTaxAmt4).Value)
                obj.TAX5_Amt = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDTaxAmt5).Value)
                obj.TAX6_Amt = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDTaxAmt6).Value)
                obj.TAX7_Amt = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDTaxAmt7).Value)
                obj.TAX8_Amt = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDTaxAmt8).Value)
                obj.TAX9_Amt = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDTaxAmt9).Value)
                obj.TAX10_Amt = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDTaxAmt10).Value)
                obj.Disc_Per = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDDisPer).Value)
                obj.Header_Discount_Per = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDHeaderDisPer).Value)
                obj.MRP = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDMRP).Value)
                obj.Assessable = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDAssessable).Value)
                obj.Batch_No = clsCommon.myCstr(gv1.Rows(ii).Cells(colDBatchNo).Value)
                obj.Requisition_Id = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select top 1 isnull(Requisition_Id,'')  from tspl_purchase_order_detail where purchaseOrder_No='" & obj.PO_Id & "'  and Item_code='" & obj.Item_Code & "'"))
                obj.Against_Item_Wise_Tax_Rate = clsCommon.myCstr(gv1.Rows(ii).Cells(colDAgainstItemWiseTaxCode).Value)
                If clsCommon.myLen(gv1.Rows(ii).Cells(colDManDate).Value) > 0 Then
                    obj.MFG_Date = clsCommon.myCDate(gv1.Rows(ii).Cells(colDManDate).Value)
                End If
                If clsCommon.myLen(gv1.Rows(ii).Cells(colDExpiryDate).Value) > 0 Then
                    obj.Expiry_Date = clsCommon.myCDate(gv1.Rows(ii).Cells(colDExpiryDate).Value)
                End If

                obj.Item_Insurance_Apply_On = clsCommon.myCstr(gv1.Rows(ii).Cells(colItemInsuranceApplyOn).Value)
                obj.Item_Insurance_Rate = clsCommon.myCdbl(gv1.Rows(ii).Cells(colItemInsurancePer).Value)
                obj.Item_Insurance_Amt = clsCommon.myCdbl(gv1.Rows(ii).Cells(colItemInsuranceAmt).Value)

                If (obj.Balance_Qty > 0) Then
                    ArrReturn.Add(obj)
                End If
            End If
        Next
        If ArrReturn.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please select at least one non zero Pending GRN item")
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
        If Not IsInsideLoadData Then
            If gvHead.CurrentColumn Is gvHead.Columns(colHSelect) Then

                '''''''''''''''''''''
                'Dim SecQc = clsCommon.myCstr(gvHead.CurrentRow.Cells(colHSelect).Value)
                'If (clsCommon.myCBool(gvHead.CurrentRow.Cells(colDSelect).Value) = True) AndAlso clsCommon.CompairString(SecQc, "Not Ok") = CompairStringResult.Equal Then
                '    e.Cancel = True
                '    common.clsCommon.MyMessageBoxShow("Second qc is not ok" + gvHead.CurrentRow.Index)
                '    Exit Sub
                'End If

                ''''''''''''''''''''''
                Dim strVendorCode As String = clsCommon.myCstr(gvHead.CurrentRow.Cells(colHVendorCode).Value)
                Dim strVendorName As String = clsCommon.myCstr(gvHead.CurrentRow.Cells(colHVendorName).Value)
                If clsCommon.myLen(VendorCode) <= 0 Then
                    VendorCode = strVendorCode
                    VendorName = strVendorName
                End If
                If clsCommon.CompairString(strVendorCode, VendorCode) = CompairStringResult.Equal Then
                    Dim strCode As String = clsCommon.myCstr(gvHead.CurrentRow.Cells(colHCode).Value)
                    If clsCommon.myLen(strCode) > 0 Then
                        LoadDetailData(e.NewValue, strCode)
                    End If
                Else
                    common.clsCommon.MyMessageBoxShow("GRN's Vendor should be `" + VendorName)
                    e.Cancel = True
                End If
            End If
        End If

        ''If Not IsInsideLoadData Then
        ''    If gvHead.CurrentColumn Is gvHead.Columns(colHSelect) Then
        ''        Dim strCode As String = clsCommon.myCstr(gvHead.CurrentRow.Cells(colHCode).Value)
        ''        If clsCommon.myLen(strCode) > 0 Then
        ''            LoadDetailData(e.NewValue, strCode)
        ''        End If
        ''    End If
        ''End If
    End Sub

    Sub LoadDetailData(ByVal NewVal As Boolean, ByVal strCode As String)

        If NewVal Then
            For Each dr As DataRow In dtAllData.Rows
                If clsCommon.CompairString(strCode, clsCommon.myCstr(dr("Code"))) = CompairStringResult.Equal Then
                    gv1.Rows.AddNew()
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDSelect).Value = True
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDCode).Value = clsCommon.myCstr(dr("code"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDPOID).Value = IIf(clsCommon.myLen(clsCommon.myCstr(dr("Against_RGP_No"))), clsCommon.myCstr(dr("Against_RGP_No")), clsCommon.myCstr(dr("PO_ID")))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDICode).Value = clsCommon.myCstr(dr("ICode"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDIName).Value = clsCommon.myCstr(dr("IName"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDIType).Value = clsCommon.myCstr(dr("IType"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDUnit).Value = clsCommon.myCstr(dr("Unit"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDRate).Value = clsCommon.myCdbl(dr("Rate"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDOrderQty).Value = clsCommon.myCdbl(dr("POQty"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDApprovedQty).Value = clsCommon.myCdbl(dr("GRNQty"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDUnApprovedQty).Value = clsCommon.myCdbl(dr("UnapprovedQty"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDPendingQty).Value = clsCommon.myCdbl(dr("PedningQty"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDTaxGroup).Value = clsCommon.myCstr(dr("Tax_Group"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDTaxGroupName).Value = clsCommon.myCstr(dr("TaxGroupName"))
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


                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDTaxableAmountPer).Value = clsCommon.myCdbl(dr("Taxable_Amount_Per"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colCategoryType).Value = clsCommon.myCstr(dr("category"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colEmergency).Value = clsCommon.myCdbl(dr("emergency"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colCapexCode).Value = clsCommon.myCstr(dr("capex_code"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colCapexSubCode).Value = clsCommon.myCstr(dr("capex_subcode"))

                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDMRP).Value = clsCommon.myCdbl(dr("MRP"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDAssessable).Value = clsCommon.myCdbl(dr("Assessable"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDBatchNo).Value = clsCommon.myCstr(dr("Batch_No"))
                    If clsCommon.myLen(dr("MFG_Date")) > 0 Then
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDManDate).Value = clsCommon.myCDate(dr("MFG_Date"))
                    End If
                    If clsCommon.myLen(dr("Expiry_Date")) > 0 Then
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDExpiryDate).Value = clsCommon.myCDate(dr("Expiry_Date"))
                    End If
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDDisPer).Value = clsCommon.myCdbl(dr("Disc_Per"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDHeaderDisPer).Value = clsCommon.myCdbl(dr("Header_Discount_Per"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDLeakQty).Value = clsCommon.myCdbl(dr("Leak_Qty"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDBurstQty).Value = clsCommon.myCdbl(dr("Burst_Qty"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDShortQty).Value = clsCommon.myCdbl(dr("Short_Qty"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDAgainstItemWiseTaxCode).Value = clsCommon.myCstr(dr("Against_Item_Wise_Tax_Rate"))

                    gv1.Rows(gv1.Rows.Count - 1).Cells(colItemInsuranceApplyOn).Value = clsCommon.myCstr(dr("Item_Insurance_Apply_On"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colItemInsurancePer).Value = clsCommon.myCdbl(dr("Item_Insurance_Rate"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colItemInsuranceAmt).Value = clsCommon.myCdbl(dr("Item_Insurance_Amt"))
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

