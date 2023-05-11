''--27/08/2012--Updation By--[Pankaj Kumar]--Applied GL Security while Navigating Document FInder---Fwd By--Ranjana Mam
Imports common
Imports System.Data.SqlClient
''BM00000000543
Public Class clsRGPHead

#Region "Variables"

    Public Against_BOM As Integer = Nothing
    Public Against_As_It_Is As Integer = Nothing
    Public GRNo As String = Nothing
    Public GR_Date As Date?
    Public Road_Permit_No As String = Nothing
    Public RoadPermit_Date As Date?
    Public Arr_BOM As List(Of clsRGPBOMItem) = Nothing
    Public Against_Schedule_Code As String = Nothing
    Public PO_Id As String = Nothing
    Public PO_Date As DateTime = Nothing
    Public Man_PO_Id As String = Nothing
    Public Man_PO_Date As Date? = Nothing

    Public RGP_No As String = Nothing
    Public RGP_Date As DateTime = Nothing
    Public Doc_Type As String = Nothing
    Public Against_Sale As Integer = 0
    Public Vendor_Code As String = Nothing
    Public Vendor_Name As String = Nothing
    Public VehicleNo As String = Nothing
    Public GPNo As String = Nothing
    Public GPDate As Date? = Nothing
    Public Reason As String = Nothing
    Public Remarks As String = Nothing
    Public Status As ERPTransactionStatus = ERPTransactionStatus.Pending
    Public On_Hold As Boolean = Nothing
    Public Posting_Date As DateTime?
    Public Document_Amount As Double = 0
    Public Location As String = Nothing
    Public LocationName As String = Nothing
    Public Delivered_By As String = Nothing
    Public Department As String = Nothing
    Public Delivered_ByName As String = Nothing
    Public Arr As List(Of clsRGPDetail) = Nothing
    Public TaxGrp As String = Nothing
    Public ItemType As String = Nothing
    Public Billing As String = Nothing
    Public Is_Non_Inventory As Integer = 0
    Public Is_Against_CC_Transfer As Integer = 0
    Public To_Location_Code As String = Nothing
    Public Mode_Of_Transport As String = Nothing
    Public Cash_Memo_Detail As String = Nothing
    Public CostCentre As String = Nothing
    Public CostCentreDesc As String = Nothing
    Public Against_Customer As Integer = 0

    Public Form_ID As String = ""
    Public arrCustomFields As List(Of clsCustomFieldValues) = Nothing

    Public chklocstion As String = Nothing
    Public srnlocation As String = Nothing
    Public srnlocationdesc As String = Nothing
    Public invoiceNo As String = String.Empty
    Public partyName As String = String.Empty
    Public partyAddress As String = String.Empty
    Public DispatchDate As String = String.Empty
    Public Against_JobWork As Integer = 0
    Public Against_NRGP As String = ""
    Public Is_Rejected As Integer = 0
    Public SRN_No As String = Nothing
    Public Item_Conversion_Type As String = Nothing
    Public JVDisplayType As Integer = 0
    Public Against_RGP As String = ""
    Public AMC_Ref_No As String = Nothing
    Public Is_Repair As Boolean = False
#End Region

    Public Shared Function HistoryUpdate(ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, clsCommon.myCstr(strCode), "TSPL_RGP_HEAD", "RGP_No", "TSPL_RGP_DETAIL", "RGP_No", trans)
        Return True
    End Function
    Public Function SaveData(ByVal obj As clsRGPHead, ByVal isNewEntry As Boolean) As Boolean

        Dim isSaved As Boolean = True
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()

        Try
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "Purchase Order", "RGP/NRGP", obj.Location, obj.RGP_Date, trans)
            If Not isNewEntry Then
                HistoryUpdate(obj.RGP_No, trans)
            End If
            clsSerializeInvenotry.DeleteData(obj.Doc_Type, obj.RGP_No, trans)
            clsBatchInventory.DeleteData(obj.Doc_Type, obj.RGP_No, trans)
            Dim qry As String = "delete from TSPL_RGP_DETAIL where RGP_No='" + obj.RGP_No + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Dim strDocNo As String = ""
            If isNewEntry Then
                If clsCommon.CompairString(obj.Doc_Type, "RGP") = CompairStringResult.Equal Then
                    obj.RGP_No = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(obj.RGP_Date), clsDocType.RGP, "", obj.Location)
                ElseIf clsCommon.CompairString(obj.Doc_Type, "RGPR") = CompairStringResult.Equal Then
                    obj.RGP_No = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(obj.RGP_Date), clsDocType.RGPR, "", obj.Location)
                ElseIf clsCommon.CompairString(obj.Doc_Type, "NRGP") = CompairStringResult.Equal Then
                    obj.RGP_No = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(obj.RGP_Date), clsDocType.NRGP, "", obj.Location)
                ElseIf clsCommon.CompairString(obj.Doc_Type, "NRGPR") = CompairStringResult.Equal Then
                    obj.RGP_No = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(obj.RGP_Date), clsDocType.NRGPR, "", obj.Location)
                Else
                    Throw New Exception("Document Type is not correct")
                End If
            End If
            If (clsCommon.myLen(obj.RGP_No) <= 0) Then
                Throw New Exception("Error in Document Code Generation")
            End If

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "RGP_Date", clsCommon.GetPrintDate(obj.RGP_Date, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Vendor_Code", obj.Vendor_Code)
            clsCommon.AddColumnsForChange(coll, "Vendor_Name", obj.Vendor_Name)
            If obj.chklocstion = "Y" AndAlso clsCommon.CompairString(obj.Doc_Type, "NRGP") = CompairStringResult.Equal Then
                clsCommon.AddColumnsForChange(coll, "InvoiceNo", obj.invoiceNo)
                clsCommon.AddColumnsForChange(coll, "PartyName", obj.partyName)
                clsCommon.AddColumnsForChange(coll, "PartyAddress", obj.partyAddress)
                clsCommon.AddColumnsForChange(coll, "DispatchDate", obj.DispatchDate)
            End If

            clsCommon.AddColumnsForChange(coll, "IsThirdParty", obj.chklocstion)
            clsCommon.AddColumnsForChange(coll, "SRN_Location_Code", obj.srnlocation)

            clsCommon.AddColumnsForChange(coll, "On_Hold", IIf(obj.On_Hold, 1, 0))
            clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks)
            clsCommon.AddColumnsForChange(coll, "Reason", obj.Reason)
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "VehicleNo", obj.VehicleNo)
            clsCommon.AddColumnsForChange(coll, "GPNo", obj.GPNo)
            clsCommon.AddColumnsForChange(coll, "Doc_Type", obj.Doc_Type)
            clsCommon.AddColumnsForChange(coll, "Against_Sale", obj.Against_Sale)
            clsCommon.AddColumnsForChange(coll, "Document_Amount", obj.Document_Amount)
            clsCommon.AddColumnsForChange(coll, "Location", obj.Location)
            clsCommon.AddColumnsForChange(coll, "Delivered_By", obj.Delivered_By)
            clsCommon.AddColumnsForChange(coll, "Department", obj.Department)
            clsCommon.AddColumnsForChange(coll, "Billing", obj.Billing)
            clsCommon.AddColumnsForChange(coll, "Is_Non_Inventory", obj.Is_Non_Inventory)
            clsCommon.AddColumnsForChange(coll, "Is_Against_CC_Transfer", obj.Is_Against_CC_Transfer)
            clsCommon.AddColumnsForChange(coll, "To_Location_Code", obj.To_Location_Code, True)
            clsCommon.AddColumnsForChange(coll, "Mode_Of_Transport", obj.Mode_Of_Transport)
            clsCommon.AddColumnsForChange(coll, "Cash_Memo_Detail", obj.Cash_Memo_Detail)
            clsCommon.AddColumnsForChange(coll, "CostCentre", obj.CostCentre)
            clsCommon.AddColumnsForChange(coll, "CostCentreDesc", obj.CostCentreDesc)
            clsCommon.AddColumnsForChange(coll, "Against_Customer", obj.Against_Customer)
            ''richa Ticket No BM00000003061 on 01/08/2014
            clsCommon.AddColumnsForChange(coll, "Against_JobWork", obj.Against_JobWork)
            clsCommon.AddColumnsForChange(coll, "Against_NRGP", obj.Against_NRGP)
            '-------------------------------------------

            clsCommon.AddColumnsForChange(coll, "PO_Id", obj.PO_Id, True)
            clsCommon.AddColumnsForChange(coll, "Against_Schedule_Code", obj.Against_Schedule_Code, True)
            clsCommon.AddColumnsForChange(coll, "Against_As_It_Is", obj.Against_As_It_Is)
            clsCommon.AddColumnsForChange(coll, "Against_BOM", obj.Against_BOM)

            '' Anubhooti 09-Oct-2014 BM00000003663
            clsCommon.AddColumnsForChange(coll, "Is_Rejected", obj.Is_Rejected)
            clsCommon.AddColumnsForChange(coll, "SRN_No", obj.SRN_No, True)
            ''
            '' Anubhooti 10-Dec-2014 BM00000003662
            clsCommon.AddColumnsForChange(coll, "Item_Conversion_Type", obj.Item_Conversion_Type, True)
            ''
            clsCommon.AddColumnsForChange(coll, "Against_RGP", obj.Against_RGP)
            clsCommon.AddColumnsForChange(coll, "AMC_Ref_No", obj.AMC_Ref_No)
            If obj.GPDate.HasValue Then
                clsCommon.AddColumnsForChange(coll, "GPDate", clsCommon.GetPrintDate(obj.GPDate, "dd/MMM/yyyy"))
            Else
                clsCommon.AddColumnsForChange(coll, "GPDate", Nothing, True)
            End If

            clsCommon.AddColumnsForChange(coll, "Road_Permit_No", obj.Road_Permit_No)
            clsCommon.AddColumnsForChange(coll, "GRNo", obj.GRNo)
            clsCommon.AddColumnsForChange(coll, "Man_Po_No", obj.Man_PO_Id, True)

            If obj.Man_PO_Date.HasValue Then

                clsCommon.AddColumnsForChange(coll, "Man_PO_Date", clsCommon.GetPrintDate(obj.Man_PO_Date, "dd/MMM/yyyy"))
            Else
                clsCommon.AddColumnsForChange(coll, "Man_PO_Date", Nothing, True)
            End If




            'If clsCommon.myLen(obj.RoadPermit_Date) > 0 Then
            If obj.RoadPermit_Date.HasValue Then
                clsCommon.AddColumnsForChange(coll, "RoadPermit_Date", clsCommon.GetPrintDate(obj.RoadPermit_Date, "dd/MMM/yyyy"))
            Else
                clsCommon.AddColumnsForChange(coll, "RoadPermit_Date", Nothing, True)
            End If
            If obj.GR_Date.HasValue Then
                clsCommon.AddColumnsForChange(coll, "GR_Date", clsCommon.GetPrintDate(obj.GR_Date, "dd/MMM/yyyy"))
            Else
                clsCommon.AddColumnsForChange(coll, "GR_Date", Nothing, True)
            End If
            clsCommon.AddColumnsForChange(coll, "JVDisplayType", CInt(obj.JVDisplayType))
            clsCommon.AddColumnsForChange(coll, "Is_Repair", IIf(obj.Is_Repair, 1, 0))
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "RGP_No", obj.RGP_No)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_RGP_HEAD", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_RGP_HEAD", OMInsertOrUpdate.Update, "TSPL_RGP_HEAD.RGP_No='" + obj.RGP_No + "'", trans)
            End If
            isSaved = isSaved AndAlso clsRGPDetail.SaveData(obj.RGP_No, obj, trans)
            isSaved = isSaved AndAlso clsRGPBOMItem.SaveData(obj.RGP_No, obj.Arr_BOM, trans)
            isSaved = isSaved AndAlso clsCustomFieldValues.SaveData(obj.Form_ID, obj.RGP_No, obj.arrCustomFields, trans)
            If isSaved Then
                trans.Commit()
            End If
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function

    Public Shared Function GetRGPTypeItemFInder(ByVal RGPType As String, ByVal Vendor_Code As String, ByVal GRN_No As String, ByVal Item_Code As String, ByVal Item_Type As String) As clsRGPDetail
        Dim obj As New clsRGPDetail()
        Dim whrcls As String = ""
        Dim qry As String = ""

        If clsCommon.CompairString(RGPType, "AR") = CompairStringResult.Equal Then 'against rgp
            whrcls = " and rgp_no in (select rgp_no from tspl_rgp_head where doc_type='RGP' and status='1' and Against_JobWork='1' and Against_BOM='0' and Against_As_It_Is='0' "
        ElseIf clsCommon.CompairString(RGPType, "AI") = CompairStringResult.Equal Then 'as it is
            whrcls = " and rgp_no in (select rgp_no from tspl_rgp_head where doc_type='RGP' and status='1' and Against_JobWork='1' and Against_BOM='0' and Against_As_It_Is='1' "
        End If
        If clsCommon.myLen(Vendor_Code) > 0 Then
            whrcls += " and vendor_code='" + Vendor_Code + "' "
        End If
        If clsCommon.myLen(whrcls) > 0 Then
            whrcls += ")"
        End If

        If clsCommon.CompairString(RGPType, "AB") = CompairStringResult.Equal Then
            qry = "select TSPL_PP_BOM_HEAD.prod_item_code as Code,tspl_item_master.item_desc as Description,TSPL_PP_BOM_HEAD.prod_item_unit_code as Unit,0 as Pending_Qty,rgp.rgp_no as [RGP No] from TSPL_PP_BOM_HEAD left outer join tspl_item_master on tspl_item_master.item_code=tspl_pp_bom_head.prod_item_code left outer join "
            qry += " (select tspl_pp_bom_item_detail.bom_code,TSPL_RGP_DETAIL.rgp_no from tspl_pp_bom_item_detail left outer join TSPL_RGP_DETAIL on TSPL_RGP_DETAIL.item_code=tspl_pp_bom_item_detail.item_code and TSPL_RGP_DETAIL.unit_code=tspl_pp_bom_item_detail.unit_code left outer join TSPL_RGP_HEAD on TSPL_RGP_HEAD.rgp_no=TSPL_RGP_detail.rgp_no where tspl_rgp_head.doc_type='RGP' and tspl_rgp_head.status='1' and tspl_rgp_head.Against_JobWork='1' and tspl_rgp_head.Against_BOM='1' and tspl_rgp_head.Against_As_It_Is='0' and tspl_rgp_head.vendor_code='" + Vendor_Code + "')rgp on rgp.bom_code=tspl_pp_bom_head.bom_code "
            qry += " where TSPL_PP_BOM_HEAD.is_post='1' and TSPL_PP_BOM_HEAD.is_osp='1' and TSPL_PP_BOM_HEAD.vendor_code='" + Vendor_Code + "' "
            If clsCommon.myLen(Item_Type) > 0 Then
                qry += " and tspl_item_master.item_type='" + Item_Type + "'"
            End If
        Else
            qry = "select final.item_code as Code,tspl_item_master.item_desc as Description,final.unit_code as Unit,final.qty as Pending_Qty,final.rgp_no as [RGP No] from (select rgp_no,Item_Code,unit_code,SUM(rgp_qty) as qty from (select rgp_no,item_code,unit_code,rgp_qty from TSPL_RGP_JOB_WORK_DETAIL where 1=1 " + whrcls + " union all select Against_RGP_No as rgp_no,Item_Code,unit_code,(0-(isnull(GRN_Qty,0)+isnull(Leak_Qty,0)+isnull(Burst_Qty,0)+isnull(Short_Qty,0))) as rgp_qty from TSPL_GRN_DETAIL where len(isnull(Against_RGP_No,''))>0 and grn_no <> '" + GRN_No + "')a group by rgp_no,Item_Code,unit_code)final "
            qry += " left outer join tspl_item_master on tspl_item_master.item_code=final.item_code where final.qty>0"
            If clsCommon.myLen(Item_Type) > 0 Then
                qry += " and tspl_item_master.item_type='" + Item_Type + "'"
            End If
            qry += " " + whrcls
        End If
        Dim dr As DataRow = clsCommon.ShowSelectFormForRow("RGPITEMFND", qry, "Code", Item_Code)

        If dr IsNot Nothing Then
            obj.Item_Code = clsCommon.myCstr(dr("code"))
            obj.RGP_No = clsCommon.myCstr(dr("RGP No"))
            obj.Item_Desc = clsCommon.myCstr(dr("Description"))
            obj.Unit_code = clsCommon.myCstr(dr("Unit"))
            obj.RGP_Qty = clsCommon.myCstr(dr("Pending_Qty"))
        End If


        Return obj
    End Function

    Public Shared Function GetRGPTypeItemBalance(ByVal RGPType As String, ByVal Qty As Double, ByVal Vendor_Code As String, ByVal GRN_No As String, ByVal Item_Code As String, ByVal Unit_Code As String, ByVal CurrentDate As Date, Optional ByVal SRN_No As String = Nothing, Optional ByVal Is_From_SRN As Boolean = False) As String
        Dim str As String = ""

        If clsCommon.CompairString(RGPType, "AB") = CompairStringResult.Equal Then
            Dim qry As String = ""
            Dim whrCls As String = ""
            If Is_From_SRN Then

                If clsCommon.myLen(SRN_No) > 0 Then
                    whrCls = " and isnull(TSPL_RGP_BOM_DETAIL.srn_no,'') <> '" + SRN_No + "' "
                End If
                qry = "select final.bom_code,final.prod_item_code,final.prod_item_unit_code,final.item_code,final.unit_code,(final.unit_qty * " + clsCommon.myCstr(Qty) + ") as Qty,final.bal_qty from (select TSPL_PP_BOM_HEAD.BOM_CODE,TSPL_PP_BOM_HEAD.PROD_ITEM_CODE,TSPL_PP_BOM_HEAD.PROD_ITEM_UNIT_CODE,TSPL_PP_BOM_ITEM_DETAIL.ITEM_CODE,TSPL_PP_BOM_ITEM_DETAIL.UNIT_CODE,(isnull(TSPL_PP_BOM_ITEM_DETAIL.QUANTITY,0)/isnull(TSPL_PP_BOM_HEAD.PROD_QUANTITY,0))+(((isnull(TSPL_PP_BOM_ITEM_DETAIL.QUANTITY,0)/isnull(TSPL_PP_BOM_HEAD.PROD_QUANTITY,0))*isnull(TSPL_PP_BOM_ITEM_DETAIL.Rejection_Pers,0))/100) as unit_qty " & _
                                " ,(rgp.Qty*TSPL_ITEM_UOM_DETAIL.Conversion_Factor)/itemuom.Conversion_Factor as bal_qty from TSPL_PP_BOM_HEAD " & _
                                " left outer join TSPL_PP_BOM_ITEM_DETAIL on TSPL_PP_BOM_ITEM_DETAIL.BOM_CODE=TSPL_PP_BOM_HEAD.BOM_CODE " & _
                                " left outer join (select ax.Item_Code,ax.Unit_Code,sum(ax.Qty) as Qty from ( " & _
                                " select Item_Code,Unit_Code,(case when InOut='I' then -1*isnull(qty,0) else isnull(qty,0) end) as Qty from TSPL_RGP_BOM_DETAIL left join tspl_rgp_head on tspl_rgp_head.rgp_no = TSPL_RGP_BOM_DETAIL.rgp_no and TSPL_RGP_BOM_DETAIL.Vendor_Code = tspl_rgp_head.Vendor_Code where TSPL_RGP_BOM_DETAIL.vendor_code='" + Vendor_Code + "' and  convert(date,tspl_rgp_head.RGP_Date ,103)<= (convert(date,'" + CurrentDate + "',103))  " + whrCls + " )ax group by ax.Item_Code,ax.Unit_Code)RGP on rgp.Item_Code=TSPL_PP_BOM_ITEM_DETAIL.ITEM_CODE " & _
                                " left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=RGP.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code=rgp.Unit_Code " & _
                                " left outer join TSPL_ITEM_UOM_DETAIL itemuom on itemuom.Item_Code=RGP.Item_Code and itemuom.UOM_Code=TSPL_PP_BOM_ITEM_DETAIL.UNIT_CODE where TSPL_PP_BOM_HEAD.PROD_ITEM_CODE='" + Item_Code + "' and TSPL_PP_BOM_HEAD.PROD_ITEM_UNIT_CODE='" + Unit_Code + "' and tspl_pp_bom_head.vendor_code='" + Vendor_Code + "')final "
            Else
                If clsCommon.myLen(GRN_No) > 0 Then
                    whrCls = " and isnull(TSPL_RGP_BOM_DETAIL.grn_no,'') <> '" + GRN_No + "' "
                End If
                qry = "select final.bom_code,final.prod_item_code,final.prod_item_unit_code,final.item_code,final.unit_code,(final.unit_qty * " + clsCommon.myCstr(Qty) + ") as Qty,final.bal_qty from (select TSPL_PP_BOM_HEAD.BOM_CODE,TSPL_PP_BOM_HEAD.PROD_ITEM_CODE,TSPL_PP_BOM_HEAD.PROD_ITEM_UNIT_CODE,TSPL_PP_BOM_ITEM_DETAIL.ITEM_CODE,TSPL_PP_BOM_ITEM_DETAIL.UNIT_CODE,(isnull(TSPL_PP_BOM_ITEM_DETAIL.QUANTITY,0)/isnull(TSPL_PP_BOM_HEAD.PROD_QUANTITY,0))+(((isnull(TSPL_PP_BOM_ITEM_DETAIL.QUANTITY,0)/isnull(TSPL_PP_BOM_HEAD.PROD_QUANTITY,0))*isnull(TSPL_PP_BOM_ITEM_DETAIL.Rejection_Pers,0))/100) as unit_qty " & _
                                " ,(rgp.Qty*TSPL_ITEM_UOM_DETAIL.Conversion_Factor)/itemuom.Conversion_Factor as bal_qty from TSPL_PP_BOM_HEAD " & _
                                " left outer join TSPL_PP_BOM_ITEM_DETAIL on TSPL_PP_BOM_ITEM_DETAIL.BOM_CODE=TSPL_PP_BOM_HEAD.BOM_CODE " & _
                                " left outer join (select ax.Item_Code,ax.Unit_Code,sum(ax.Qty) as Qty from ( " & _
                                " select Item_Code,Unit_Code,(case when InOut='I' then -1*isnull(qty,0) else isnull(qty,0) end) as Qty from TSPL_RGP_BOM_DETAIL left join tspl_rgp_head on tspl_rgp_head.rgp_no = TSPL_RGP_BOM_DETAIL.rgp_no and TSPL_RGP_BOM_DETAIL.Vendor_Code = tspl_rgp_head.Vendor_Code where TSPL_RGP_BOM_DETAIL.vendor_code='" + Vendor_Code + "' and  convert(date,tspl_rgp_head.RGP_Date ,103)<= (convert(date,'" + CurrentDate + "',103)) " + whrCls + " )ax group by ax.Item_Code,ax.Unit_Code)RGP on rgp.Item_Code=TSPL_PP_BOM_ITEM_DETAIL.ITEM_CODE " & _
                                " left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=RGP.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code=rgp.Unit_Code " & _
                                " left outer join TSPL_ITEM_UOM_DETAIL itemuom on itemuom.Item_Code=RGP.Item_Code and itemuom.UOM_Code=TSPL_PP_BOM_ITEM_DETAIL.UNIT_CODE where TSPL_PP_BOM_HEAD.PROD_ITEM_CODE='" + Item_Code + "' and TSPL_PP_BOM_HEAD.PROD_ITEM_UNIT_CODE='" + Unit_Code + "' and tspl_pp_bom_head.vendor_code='" + Vendor_Code + "')final "
            End If

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    If clsCommon.myCdbl(dr("Qty")) > clsCommon.myCdbl(dr("bal_qty")) Then 'if balance is not exist then
                        str += " [" + clsItemMaster.GetItemName(clsCommon.myCstr(dr("item_code")), Nothing) + " has RGP balance " + clsCommon.myCstr(clsCommon.myCdbl(dr("bal_qty"))) + "  and required qty is " + clsCommon.myCstr(clsCommon.myCdbl(dr("qty"))) + "]."
                    End If
                Next
            Else ''if no record found
                str += "no raw-material balance exist."
            End If
        End If

        If clsCommon.myLen(str) > 0 Then
            str = "Alert msg. " + Environment.NewLine + "Item(s) " + str
        End If


        Return str
    End Function

    Public Shared Function GetRGPTypeBOMItemsDetail(ByVal RGPType As String, ByVal Qty As Double, ByVal Vendor_Code As String, ByVal GRN_No As String, ByVal Item_Code As String, ByVal Unit_Code As String, ByVal trans As SqlTransaction, Optional ByVal SRN_No As String = Nothing, Optional ByVal is_From_SRN As Boolean = False) As DataTable
        Dim dt As New DataTable()

        If clsCommon.CompairString(RGPType, "AB") = CompairStringResult.Equal Then
            Dim qry As String = ""
            If Not is_From_SRN Then
                qry = "select final.bom_code,final.prod_item_code,final.prod_item_unit_code,final.item_code,final.unit_code,(final.unit_qty * " + clsCommon.myCstr(Qty) + ") as Qty,final.bal_qty from (select TSPL_PP_BOM_HEAD.BOM_CODE,TSPL_PP_BOM_HEAD.PROD_ITEM_CODE,TSPL_PP_BOM_HEAD.PROD_ITEM_UNIT_CODE,TSPL_PP_BOM_ITEM_DETAIL.ITEM_CODE,TSPL_PP_BOM_ITEM_DETAIL.UNIT_CODE,(isnull(TSPL_PP_BOM_ITEM_DETAIL.QUANTITY,0)/isnull(TSPL_PP_BOM_HEAD.PROD_QUANTITY,0))+(((isnull(TSPL_PP_BOM_ITEM_DETAIL.QUANTITY,0)/isnull(TSPL_PP_BOM_HEAD.PROD_QUANTITY,0))*isnull(TSPL_PP_BOM_ITEM_DETAIL.Rejection_Pers,0))/100) as unit_qty " & _
                                " ,(rgp.Qty*TSPL_ITEM_UOM_DETAIL.Conversion_Factor)/itemuom.Conversion_Factor as bal_qty from TSPL_PP_BOM_HEAD " & _
                                " left outer join TSPL_PP_BOM_ITEM_DETAIL on TSPL_PP_BOM_ITEM_DETAIL.BOM_CODE=TSPL_PP_BOM_HEAD.BOM_CODE " & _
                                " left outer join (select ax.Item_Code,ax.Unit_Code,sum(ax.Qty) as Qty from ( " & _
                                " select Item_Code,Unit_Code,(case when InOut='I' then -1*isnull(qty,0) else isnull(qty,0) end) as Qty from TSPL_RGP_BOM_DETAIL where isnull(grn_no,'') <> '" + GRN_No + "' and vendor_code='" + Vendor_Code + "' union all select Item_Code,Unit_Code,(case when InOut='I' then -1*isnull(qty,0) else isnull(qty,0) end) as Qty from TSPL_RGP_BOM_DETAIL where isnull(grn_no,'')='' and isnull(srn_no,'')='' and vendor_code='" + Vendor_Code + "' )ax group by ax.Item_Code,ax.Unit_Code)RGP on rgp.Item_Code=TSPL_PP_BOM_ITEM_DETAIL.ITEM_CODE " & _
                                " left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=RGP.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code=rgp.Unit_Code " & _
                                " left outer join TSPL_ITEM_UOM_DETAIL itemuom on itemuom.Item_Code=RGP.Item_Code and itemuom.UOM_Code=TSPL_PP_BOM_ITEM_DETAIL.UNIT_CODE where TSPL_PP_BOM_HEAD.PROD_ITEM_CODE='" + Item_Code + "' and TSPL_PP_BOM_HEAD.PROD_ITEM_UNIT_CODE='" + Unit_Code + "' and tspl_pp_bom_head.vendor_code='" + Vendor_Code + "')final "
            ElseIf is_From_SRN Then
                qry = "select final.bom_code,final.prod_item_code,final.prod_item_unit_code,final.item_code,final.unit_code,(final.unit_qty * " + clsCommon.myCstr(Qty) + ") as Qty,final.bal_qty from (select TSPL_PP_BOM_HEAD.BOM_CODE,TSPL_PP_BOM_HEAD.PROD_ITEM_CODE,TSPL_PP_BOM_HEAD.PROD_ITEM_UNIT_CODE,TSPL_PP_BOM_ITEM_DETAIL.ITEM_CODE,TSPL_PP_BOM_ITEM_DETAIL.UNIT_CODE,(isnull(TSPL_PP_BOM_ITEM_DETAIL.QUANTITY,0)/isnull(TSPL_PP_BOM_HEAD.PROD_QUANTITY,0))+(((isnull(TSPL_PP_BOM_ITEM_DETAIL.QUANTITY,0)/isnull(TSPL_PP_BOM_HEAD.PROD_QUANTITY,0))*isnull(TSPL_PP_BOM_ITEM_DETAIL.Rejection_Pers,0))/100) as unit_qty " & _
                                " ,(rgp.Qty*TSPL_ITEM_UOM_DETAIL.Conversion_Factor)/itemuom.Conversion_Factor as bal_qty from TSPL_PP_BOM_HEAD " & _
                                " left outer join TSPL_PP_BOM_ITEM_DETAIL on TSPL_PP_BOM_ITEM_DETAIL.BOM_CODE=TSPL_PP_BOM_HEAD.BOM_CODE " & _
                                " left outer join (select ax.Item_Code,ax.Unit_Code,sum(ax.Qty) as Qty from ( " & _
                                " select Item_Code,Unit_Code,(case when InOut='I' then -1*isnull(qty,0) else isnull(qty,0) end) as Qty from TSPL_RGP_BOM_DETAIL where isnull(srn_no,'') <> '" + SRN_No + "' and vendor_code='" + Vendor_Code + "' union all select Item_Code,Unit_Code,(case when InOut='I' then -1*isnull(qty,0) else isnull(qty,0) end) as Qty from TSPL_RGP_BOM_DETAIL where isnull(grn_no,'')='' and isnull(srn_no,'')='' and vendor_code='" + Vendor_Code + "' )ax group by ax.Item_Code,ax.Unit_Code)RGP on rgp.Item_Code=TSPL_PP_BOM_ITEM_DETAIL.ITEM_CODE " & _
                                " left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=RGP.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code=rgp.Unit_Code " & _
                                " left outer join TSPL_ITEM_UOM_DETAIL itemuom on itemuom.Item_Code=RGP.Item_Code and itemuom.UOM_Code=TSPL_PP_BOM_ITEM_DETAIL.UNIT_CODE where TSPL_PP_BOM_HEAD.PROD_ITEM_CODE='" + Item_Code + "' and TSPL_PP_BOM_HEAD.PROD_ITEM_UNIT_CODE='" + Unit_Code + "' and tspl_pp_bom_head.vendor_code='" + Vendor_Code + "')final "
            End If

            dt = clsDBFuncationality.GetDataTable(qry, trans)
        End If

        Return dt
    End Function

    Public Shared Function IsValidVendorForRGP(ByVal Arr As List(Of String), ByVal strVendorCode As String) As Boolean
        If Arr IsNot Nothing AndAlso Arr.Count > 0 Then
            Dim qry As String = "select RGP_No,Vendor_Code,Vendor_Name from TSPL_RGP_HEAD where RGP_No in (" + clsCommon.GetMulcallString(Arr) + ") and Vendor_Code not in ('" + strVendorCode + "')"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim msg As String = "Error. "
                For Each dr As DataRow In dt.Rows
                    msg += Environment.NewLine + "RGP No:" + clsCommon.myCstr(dr("RGP_No")) + " Is For Vendor Code: " + clsCommon.myCstr(dr("Vendor_Code")) + " Vendor Name:" + clsCommon.myCstr(dr("Vendor_Name"))
                Next
                Throw New Exception(msg)
            End If

        End If
        Return True
    End Function

    Public Shared Function IsValidRGPType(ByVal Arr As List(Of String), ByVal strRGPType As String) As Boolean
        Dim str As String = ""
        If Arr IsNot Nothing AndAlso Arr.Count > 0 Then
            If clsCommon.myLen(strRGPType) <= 0 Then
                Return True
            End If
            Dim qry As String = "select RGP_No,Vendor_Code,Vendor_Name from TSPL_RGP_HEAD where RGP_No in (" + clsCommon.GetMulcallString(Arr) + ") "
            If clsCommon.CompairString(strRGPType, "AR") = CompairStringResult.Equal Then
                qry += " and Against_JobWork<>'1' "
                str = "Against RGP"
            End If
            If clsCommon.CompairString(strRGPType, "AB") = CompairStringResult.Equal Then
                qry += " and Against_BOM<>'1' "
                str = "Against BOM"
            End If
            If clsCommon.CompairString(strRGPType, "AI") = CompairStringResult.Equal Then
                qry += " and Against_As_It_Is<>'1' "
                str = "Against As It Is"
            End If
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim msg As String = "Error. "
                For Each dr As DataRow In dt.Rows
                    msg += Environment.NewLine + "RGP No:" + clsCommon.myCstr(dr("RGP_No")) + " Is For RGP Type: " + str
                Next
                Throw New Exception(msg)
            End If

        End If
        Return True
    End Function

    Public Shared Function UpdateAfterPosting(ByVal obj As clsRGPHead, ByVal trans As SqlTransaction) As Boolean
        Try
            If obj IsNot Nothing And clsCommon.myLen(obj.RGP_No) > 0 Then
                Dim coll As New Hashtable()

                clsCommon.AddColumnsForChange(coll, "GRNo", obj.GRNo)
                clsCommon.AddColumnsForChange(coll, "Road_Permit_No", obj.Road_Permit_No)
                If clsCommon.myLen(obj.GR_Date) > 0 Then
                    clsCommon.AddColumnsForChange(coll, "GR_Date", clsCommon.GetPrintDate(obj.GR_Date, "dd/MMM/yyyy"))
                Else
                    clsCommon.AddColumnsForChange(coll, "GR_Date", Nothing, True)
                End If
                If clsCommon.myLen(obj.RoadPermit_Date) > 0 Then
                    clsCommon.AddColumnsForChange(coll, "RoadPermit_Date", clsCommon.GetPrintDate(obj.RoadPermit_Date, "dd/MMM/yyyy"))
                Else
                    clsCommon.AddColumnsForChange(coll, "RoadPermit_Date", Nothing, True)
                End If

                clsCommonFunctionality.UpdateDataTable(coll, "tspl_rgp_head", OMInsertOrUpdate.Update, "tspl_rgp_head.RGP_No='" + obj.RGP_No + "'", trans)
            End If
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Shared Function GetData(ByVal strDocumentNo As String, ByVal NavType As NavigatorType) As clsRGPHead
        Return GetData(strDocumentNo, NavType, Nothing)
    End Function
    Public Shared Function GetData(ByVal strPONo As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsRGPHead
        Dim obj As clsRGPHead = Nothing
        Dim qry As String = "SELECT tspl_purchase_order_head.purchaseorder_date,TSPL_RGP_HEAD.Man_Po_No,TSPL_RGP_HEAD.Man_PO_Date,TSPL_RGP_HEAD.Against_As_It_Is,TSPL_RGP_HEAD.Against_BOM,TSPL_RGP_HEAD.Road_Permit_No,TSPL_RGP_HEAD.RoadPermit_Date,TSPL_RGP_HEAD.GRNo,TSPL_RGP_HEAD.GR_Date,TSPL_RGP_HEAD.po_id,TSPL_RGP_HEAD.Against_Schedule_Code,TSPL_RGP_HEAD.RGP_No,TSPL_RGP_HEAD.Billing,TSPL_RGP_HEAD.RGP_Date,TSPL_RGP_HEAD.Vendor_Code,TSPL_RGP_HEAD.Vendor_Name,TSPL_RGP_HEAD.Status,TSPL_RGP_HEAD.On_Hold,TSPL_RGP_HEAD.Reason,TSPL_RGP_HEAD.Remarks,TSPL_RGP_HEAD.Reason,TSPL_RGP_HEAD.Posting_Date,TSPL_RGP_HEAD.VehicleNo,TSPL_RGP_HEAD.GPNo,TSPL_RGP_HEAD.GPDate,TSPL_RGP_HEAD.Doc_Type,TSPL_RGP_HEAD.Document_Amount,TSPL_RGP_HEAD.Location,TSPL_RGP_HEAD.Mode_Of_Transport,TSPL_RGP_HEAD.Cash_Memo_Detail,TSPL_LOCATION_MASTER.Location_Desc as LocationName,TSPL_RGP_HEAD.Delivered_By,TSPL_EMPLOYEE_MASTER.Emp_Name as DeliveredBYName,TSPL_RGP_HEAD.Department,TSPL_RGP_HEAD.Against_Sale,Against_JobWork, TSPL_RGP_HEAD.Is_Non_Inventory,TSPL_RGP_HEAD.Is_Against_CC_Transfer,TSPL_RGP_HEAD.To_Location_Code,CostCentre,CostCentreDesc,Against_Customer,TSPL_RGP_HEAD.IsThirdParty,TSPL_RGP_HEAD.SRN_Location_Code,TSPL_RGP_HEAD.InvoiceNo,TSPL_RGP_HEAD.PartyName,TSPL_RGP_HEAD.PartyAddress,TSPL_RGP_HEAD.DispatchDate, TSPL_RGP_HEAD.Against_NRGP,TSPL_RGP_HEAD.Is_Rejected,TSPL_RGP_HEAD.SRN_No,TSPL_RGP_HEAD.Item_Conversion_Type,TSPL_RGP_HEAD.JVDisplayType,TSPL_RGP_HEAD.Against_RGP ,TSPL_RGP_HEAD.AMC_Ref_No,TSPL_RGP_HEAD.Is_Repair  FROM TSPL_RGP_HEAD left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_RGP_HEAD.Location left outer join TSPL_EMPLOYEE_MASTER on TSPL_EMPLOYEE_MASTER.EMP_CODE=TSPL_RGP_HEAD.Delivered_By left join tspl_purchase_order_head on tspl_purchase_order_head.purchaseorder_no=TSPL_RGP_HEAD.po_id where 2=2"
        Dim whrCls As String = ""
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            whrCls = " AND Location in (" + objCommonVar.strCurrUserLocations + ")"
        End If
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_RGP_HEAD.RGP_No = (select MIN(RGP_No) from TSPL_RGP_HEAD WHERE 1=1 " + whrCls + ")"
            Case NavigatorType.Last
                qry += " and TSPL_RGP_HEAD.RGP_No = (select Max(RGP_No) from TSPL_RGP_HEAD WHERE 1=1 " + whrCls + ")"
            Case NavigatorType.Current
                qry += " and TSPL_RGP_HEAD.RGP_No = '" + strPONo + "'"
            Case NavigatorType.Next
                qry += " and TSPL_RGP_HEAD.RGP_No = (select Min(RGP_No) from TSPL_RGP_HEAD where RGP_No>'" + strPONo + "' " + whrCls + ")"
            Case NavigatorType.Previous
                qry += " and TSPL_RGP_HEAD.RGP_No = (select Max(RGP_No) from TSPL_RGP_HEAD where RGP_No<'" + strPONo + "' " + whrCls + ")"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsRGPHead()

            '----------------------------------------------------
            Try
                obj.chklocstion = clsCommon.myCstr(dt.Rows(0)("IsThirdParty"))
                obj.srnlocation = clsCommon.myCstr(dt.Rows(0)("SRN_Location_Code"))
                obj.srnlocationdesc = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select location_desc from tspl_location_master where location_code='" + obj.srnlocation + "'", trans))
            Catch exx As Exception
            End Try
            '-----------------------------------------------------------------------
            obj.GRNo = clsCommon.myCstr(dt.Rows(0)("GRNo"))
            obj.Road_Permit_No = clsCommon.myCstr(dt.Rows(0)("Road_Permit_No"))
            If dt.Rows(0)("GR_Date") IsNot DBNull.Value Then
                obj.GR_Date = clsCommon.myCstr(dt.Rows(0)("GR_Date"))
            End If
            If dt.Rows(0)("RoadPermit_Date") IsNot DBNull.Value Then
                obj.RoadPermit_Date = clsCommon.myCstr(dt.Rows(0)("RoadPermit_Date"))
            End If

            obj.Against_BOM = CInt(clsCommon.myCdbl(dt.Rows(0)("Against_BOM")))
            obj.Against_As_It_Is = CInt(clsCommon.myCdbl(dt.Rows(0)("Against_As_It_Is")))
            obj.JVDisplayType = CInt(clsCommon.myCdbl(dt.Rows(0)("JVDisplayType")))
            obj.PO_Id = clsCommon.myCstr(dt.Rows(0)("po_id"))
            If dt.Rows(0)("purchaseorder_date") IsNot DBNull.Value Then
                obj.PO_Date = clsCommon.myCDate(dt.Rows(0)("purchaseorder_date"))
            End If

            obj.Man_PO_Id = clsCommon.myCstr(dt.Rows(0)("Man_Po_No"))
            If dt.Rows(0)("Man_PO_Date") IsNot DBNull.Value Then
                obj.Man_PO_Date = clsCommon.myCDate(dt.Rows(0)("Man_PO_Date"))
            End If


            obj.Against_Schedule_Code = clsCommon.myCstr(dt.Rows(0)("Against_Schedule_Code"))
            obj.RGP_No = clsCommon.myCstr(dt.Rows(0)("RGP_No"))
            obj.RGP_Date = clsCommon.myCstr(dt.Rows(0)("RGP_Date"))
            obj.Vendor_Code = clsCommon.myCstr(dt.Rows(0)("Vendor_Code"))
            obj.Vendor_Name = clsCommon.myCstr(dt.Rows(0)("Vendor_Name"))
            obj.Status = IIf(clsCommon.myCdbl(dt.Rows(0)("Status")) = 1, ERPTransactionStatus.Approved, ERPTransactionStatus.Pending)
            obj.On_Hold = IIf(clsCommon.myCdbl(dt.Rows(0)("On_Hold")) = 1, True, False)
            obj.Remarks = clsCommon.myCstr(dt.Rows(0)("Remarks"))
            obj.Reason = clsCommon.myCstr(dt.Rows(0)("Reason"))
            obj.Against_Sale = clsCommon.myCdbl(dt.Rows(0)("Against_Sale"))
            ''richa Ticket No BM00000003061 on 01/08/2014
            obj.Against_JobWork = clsCommon.myCdbl(dt.Rows(0)("Against_JobWork"))
            '-------------------------------------------
            If clsCommon.myLen(dt.Rows(0)("Posting_Date")) > 0 Then
                obj.Posting_Date = clsCommon.myCstr(dt.Rows(0)("Posting_Date"))
            End If
            obj.Mode_Of_Transport = clsCommon.myCstr(dt.Rows(0)("Mode_Of_Transport"))
            obj.Cash_Memo_Detail = clsCommon.myCstr(dt.Rows(0)("Cash_Memo_Detail"))
            obj.VehicleNo = clsCommon.myCstr(dt.Rows(0)("VehicleNo"))
            obj.GPNo = clsCommon.myCstr(dt.Rows(0)("GPNo"))
            obj.Location = clsCommon.myCstr(dt.Rows(0)("Location"))
            obj.LocationName = clsCommon.myCstr(dt.Rows(0)("LocationName"))
            obj.Delivered_By = clsCommon.myCstr(dt.Rows(0)("Delivered_By"))
            obj.Delivered_ByName = clsCommon.myCstr(dt.Rows(0)("DeliveredBYName"))
            If dt.Rows(0)("GPDate") IsNot DBNull.Value Then
                obj.GPDate = clsCommon.myCDate(dt.Rows(0)("GPDate"))
            End If
            obj.Doc_Type = clsCommon.myCstr(dt.Rows(0)("Doc_Type"))
            obj.Billing = clsCommon.myCstr(dt.Rows(0)("Billing"))
            obj.Document_Amount = clsCommon.myCstr(dt.Rows(0)("Document_Amount"))
            obj.Department = clsCommon.myCstr(dt.Rows(0)("Department"))
            obj.Is_Non_Inventory = clsCommon.myCdbl(dt.Rows(0)("Is_Non_Inventory"))
            obj.Is_Against_CC_Transfer = clsCommon.myCdbl(dt.Rows(0)("Is_Against_CC_Transfer"))
            obj.To_Location_Code = clsCommon.myCstr(dt.Rows(0)("To_Location_Code"))
            obj.CostCentre = clsCommon.myCstr(dt.Rows(0)("CostCentre"))
            obj.CostCentreDesc = clsCommon.myCstr(dt.Rows(0)("CostCentreDesc"))
            obj.Against_Customer = clsCommon.myCdbl(dt.Rows(0)("Against_Customer"))
            obj.invoiceNo = clsCommon.myCstr(dt.Rows(0)("InvoiceNo"))
            obj.partyName = clsCommon.myCstr(dt.Rows(0)("PartyName"))
            obj.partyAddress = clsCommon.myCstr(dt.Rows(0)("PartyAddress"))
            If clsCommon.myLen(dt.Rows(0)("DispatchDate")) > 0 Then
                obj.DispatchDate = clsCommon.myCDate(dt.Rows(0)("DispatchDate"))
            End If
            obj.Against_NRGP = clsCommon.myCstr(dt.Rows(0)("Against_NRGP"))
            '' Anubhooti 09-Oct-2014 BM00000003663
            obj.Is_Rejected = clsCommon.myCdbl(dt.Rows(0)("Is_Rejected"))
            obj.SRN_No = clsCommon.myCstr(dt.Rows(0)("SRN_No"))
            ''
            '' Anubhooti 10-Dec-2014 BM00000003662
            obj.Item_Conversion_Type = clsCommon.myCstr(dt.Rows(0)("Item_Conversion_Type"))
            ''
            obj.Against_RGP = clsCommon.myCstr(dt.Rows(0)("Against_RGP"))
            obj.AMC_Ref_No = clsCommon.myCstr(dt.Rows(0)("AMC_Ref_No"))
            obj.Is_Repair = IIf(clsCommon.myCdbl(dt.Rows(0)("Is_Repair")) > 0, True, False)

            'qry = "SELECT TSPL_RGP_DETAIL.MRP,TSPL_RGP_DETAIL.RGP_No,TSPL_RGP_DETAIL.Line_No,TSPL_RGP_DETAIL.Item_Code,TSPL_RGP_DETAIL.Item_Desc,TSPL_RGP_DETAIL.RGP_Qty, (Select ISNULL(RGP_Qty,0) from TSPL_RGP_DETAIL as dtl Where dtl.RGP_No=TSPL_RGP_HEAD.Against_NRGP AND dtl.Item_Code=TSPL_RGP_DETAIL.Item_Code) as NRGP_Qty, TSPL_RGP_DETAIL.Unit_code,TSPL_RGP_DETAIL.Item_Cost,TSPL_RGP_DETAIL.Amount, TSPL_RGP_DETAIL.Last_RGP_No, TSPL_RGP_DETAIL.Last_RGP_Date, TSPL_RGP_DETAIL.Specification FROM TSPL_RGP_DETAIL LEFT OUTER JOIN TSPL_RGP_HEAD ON TSPL_RGP_HEAD.RGP_No=TSPL_RGP_DETAIL.RGP_No where TSPL_RGP_DETAIL.RGP_No='" & obj.RGP_No & "'  ORDER BY TSPL_RGP_DETAIL.Line_No"
            If clsCommon.myLen(obj.Against_NRGP) <= 0 AndAlso clsCommon.CompairString(obj.Doc_Type, "RGPR") <> CompairStringResult.Equal Then
                qry = "Select XXX.BOM_Code,XXX.Main_PO_Icode,XXX.PO_Sch_Qty,XXX.po_id,XXX.Against_Schedule_Code,XXX.MRP, XXX.RGP_No, XXX.Line_No, XXX.Item_Code, XXX.Item_Desc, XXX.RGP_Qty, XXX.NRGP_Qty, XXX.Unit_code, XXX.Item_Cost,XXX.Approx_Cost, XXX.Amount, XXX.Last_RGP_No, XXX.Last_RGP_Date, XXX.Specification, RGP_Qty-(select ISNULL(SUM(RGP_Qty),0) from TSPL_RGP_HEAD LEFT OUTER JOIN TSPL_RGP_DETAIL ON TSPL_RGP_DETAIL.RGP_No=TSPL_RGP_HEAD.RGP_No WHERE TSPL_RGP_HEAD.Against_NRGP='" & obj.RGP_No & "' AND TSPL_RGP_DETAIL.Item_Code=XXX.Item_Code) as Balance_Qty from ("
            ElseIf clsCommon.myLen(obj.Against_RGP) <= 0 AndAlso clsCommon.CompairString(obj.Doc_Type, "RGPR") = CompairStringResult.Equal Then
                qry = "Select XXX.BOM_Code,XXX.Main_PO_Icode,XXX.PO_Sch_Qty,XXX.po_id,XXX.Against_Schedule_Code,XXX.MRP, XXX.RGP_No, XXX.Line_No, XXX.Item_Code, XXX.Item_Desc, XXX.RGP_Qty, XXX.NRGP_Qty, XXX.Unit_code, XXX.Item_Cost,XXX.Approx_Cost, XXX.Amount, XXX.Last_RGP_No, XXX.Last_RGP_Date, XXX.Specification, RGP_Qty-(select ISNULL(SUM(RGP_Qty),0) from TSPL_RGP_HEAD LEFT OUTER JOIN TSPL_RGP_DETAIL ON TSPL_RGP_DETAIL.RGP_No=TSPL_RGP_HEAD.RGP_No WHERE TSPL_RGP_HEAD.Against_RGP='" & obj.RGP_No & "' AND TSPL_RGP_DETAIL.Item_Code=XXX.Item_Code) as Balance_Qty from ("
            ElseIf clsCommon.myLen(obj.Against_RGP) >= 0 AndAlso clsCommon.CompairString(obj.Doc_Type, "RGPR") = CompairStringResult.Equal Then
                qry = "Select XXX.BOM_Code,XXX.Main_PO_Icode,XXX.PO_Sch_Qty,XXX.po_id,XXX.Against_Schedule_Code,XXX.MRP, XXX.RGP_No, XXX.Line_No, XXX.Item_Code, XXX.Item_Desc, XXX.RGP_Qty, XXX.NRGP_Qty, XXX.Unit_code, XXX.Item_Cost,XXX.Approx_Cost, XXX.Amount, XXX.Last_RGP_No, XXX.Last_RGP_Date, XXX.Specification, RGP_Qty-(select ISNULL(SUM(RGP_Qty),0) from TSPL_RGP_HEAD LEFT OUTER JOIN TSPL_RGP_DETAIL ON TSPL_RGP_DETAIL.RGP_No=TSPL_RGP_HEAD.RGP_No WHERE TSPL_RGP_HEAD.Against_RGP='" & obj.Against_RGP & "' AND TSPL_RGP_DETAIL.Item_Code=XXX.Item_Code AND TSPL_RGP_HEAD.RGP_No<>'" & obj.RGP_No & "') as Balance_Qty from ("
            Else
                qry = "Select XXX.BOM_Code,XXX.Main_PO_Icode,XXX.PO_Sch_Qty,XXX.po_id,XXX.Against_Schedule_Code,XXX.MRP, XXX.RGP_No, XXX.Line_No, XXX.Item_Code, XXX.Item_Desc, XXX.RGP_Qty, XXX.NRGP_Qty, XXX.Unit_code, XXX.Item_Cost,XXX.Approx_Cost, XXX.Amount, XXX.Last_RGP_No, XXX.Last_RGP_Date, XXX.Specification, NRGP_Qty-(select ISNULL(SUM(RGP_Qty),0) from TSPL_RGP_HEAD LEFT OUTER JOIN TSPL_RGP_DETAIL ON TSPL_RGP_DETAIL.RGP_No=TSPL_RGP_HEAD.RGP_No WHERE TSPL_RGP_HEAD.Against_NRGP='" & obj.Against_NRGP & "' AND TSPL_RGP_DETAIL.Item_Code=XXX.Item_Code AND TSPL_RGP_HEAD.RGP_No<>'" & obj.RGP_No & "') as Balance_Qty from ("
            End If
            qry += " SELECT TSPL_RGP_DETAIL.BOM_Code,TSPL_RGP_DETAIL.Main_PO_Icode,TSPL_RGP_DETAIL.PO_Sch_Qty,TSPL_RGP_DETAIL.po_id,TSPL_RGP_DETAIL.Against_Schedule_Code,TSPL_RGP_DETAIL.MRP,TSPL_RGP_DETAIL.RGP_No,TSPL_RGP_DETAIL.Line_No,TSPL_RGP_DETAIL.Item_Code,TSPL_RGP_DETAIL.Item_Desc,TSPL_RGP_DETAIL.RGP_Qty," & _
        " (Select ISNULL(RGP_Qty,0) from TSPL_RGP_DETAIL as dtl Where dtl.RGP_No=TSPL_RGP_HEAD.Against_NRGP AND dtl.Item_Code=TSPL_RGP_DETAIL.Item_Code) as NRGP_Qty," & _
        " TSPL_RGP_DETAIL.Unit_code,TSPL_RGP_DETAIL.Item_Cost,isnull(TSPL_RGP_DETAIL.Approx_Cost,0) As Approx_Cost,TSPL_RGP_DETAIL.Amount, TSPL_RGP_DETAIL.Last_RGP_No, TSPL_RGP_DETAIL.Last_RGP_Date," & _
        " TSPL_RGP_DETAIL.Specification, TSPL_RGP_HEAD.Against_NRGP FROM TSPL_RGP_DETAIL" & _
        " LEFT OUTER JOIN TSPL_RGP_HEAD ON TSPL_RGP_HEAD.RGP_No=TSPL_RGP_DETAIL.RGP_No where TSPL_RGP_DETAIL.RGP_No='" & obj.RGP_No & "'" & _
        " ) XXX ORDER BY XXX.Line_No"
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj.Arr = New List(Of clsRGPDetail)
                Dim objTr As clsRGPDetail
                For Each dr As DataRow In dt.Rows
                    objTr = New clsRGPDetail
                    objTr.Item_MRP = clsCommon.myCdbl(dr("MRP"))
                    objTr.RGP_No = clsCommon.myCstr(dr("RGP_No"))
                    objTr.Line_No = clsCommon.myCstr(dr("Line_No"))
                    objTr.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                    objTr.Item_Desc = clsCommon.myCstr(dr("Item_Desc"))
                    objTr.RGP_Qty = clsCommon.myCdbl(dr("RGP_Qty"))
                    objTr.NRGP_Qty = clsCommon.myCdbl(dr("NRGP_Qty"))
                    objTr.Balance_Qty = clsCommon.myCdbl(dr("Balance_Qty"))
                    objTr.Unit_code = clsCommon.myCstr(dr("Unit_code"))
                    objTr.Item_Cost = clsCommon.myCdbl(dr("Item_Cost"))
                    objTr.Last_RGP_No = clsCommon.myCstr(dr("Last_RGP_No"))
                    objTr.Last_RGP_Date = clsCommon.myCstr(dr("Last_RGP_Date"))
                    objTr.Amount = clsCommon.myCdbl(dr("Amount"))
                    objTr.Specification = clsCommon.myCstr(dr("Specification"))
                    objTr.PO_Id = clsCommon.myCstr(dr("po_id"))
                    objTr.Against_Schedule_Code = clsCommon.myCstr(dr("Against_Schedule_Code"))
                    objTr.PO_Sch_Qty = clsCommon.myCdbl(dr("PO_Sch_Qty"))
                    objTr.Main_PO_Icode = clsCommon.myCstr(dr("Main_PO_Icode"))
                    objTr.BOM_Code = clsCommon.myCstr(dr("BOM_Code"))
                    '' Anubhooti 06-Feb-2015 (Fetch Approx_Cost Also from query-newly added)
                    objTr.Approx_Cost = clsCommon.myCdbl(dr("Approx_Cost"))
                    objTr.arrSrItem = clsSerializeInvenotry.GetData(obj.Doc_Type, objTr.RGP_No, objTr.Item_Code, objTr.Line_No, trans)
                    objTr.arrBatchItem = clsBatchInventory.GetData(obj.Doc_Type, objTr.RGP_No, objTr.Item_Code, objTr.Line_No, trans)
                    obj.Arr.Add(objTr)
                Next
            End If

            If objCommonVar.IsKDIL Then
                qry = "select TSPL_RGP_JOB_WORK_DETAIL.*,tspl_item_master.item_desc,tspl_pp_bom_head.description as bom_desc from TSPL_RGP_JOB_WORK_DETAIL left outer join tspl_pp_bom_head on tspl_pp_bom_head.bom_code=TSPL_RGP_JOB_WORK_DETAIL.bom_code left outer join tspl_item_master on tspl_item_master.item_code=TSPL_RGP_JOB_WORK_DETAIL.item_code where TSPL_RGP_JOB_WORK_DETAIL.rgp_no='" + obj.RGP_No + "'"
            Else
                qry = "select TSPL_RGP_JOB_WORK_DETAIL.*,tspl_item_master.item_desc,tspl_mf_bom_head.description as bom_desc from TSPL_RGP_JOB_WORK_DETAIL left outer join tspl_mf_bom_head on tspl_mf_bom_head.bom_code=TSPL_RGP_JOB_WORK_DETAIL.bom_code left outer join tspl_item_master on tspl_item_master.item_code=TSPL_RGP_JOB_WORK_DETAIL.item_code where TSPL_RGP_JOB_WORK_DETAIL.rgp_no='" + obj.RGP_No + "'"
            End If
            Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            obj.Arr_BOM = New List(Of clsRGPBOMItem)

            If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
                For Each dr As DataRow In dt1.Rows
                    Dim objtr As New clsRGPBOMItem()

                    objtr.Line_No = clsCommon.myCstr(dr("line_no"))
                    objtr.BOM_Code = clsCommon.myCstr(dr("bom_code"))
                    objtr.BOM_desc = clsCommon.myCstr(dr("bom_desc"))
                    objtr.Item_Code = clsCommon.myCstr(dr("item_code"))
                    objtr.Iname = clsCommon.myCstr(dr("item_desc"))
                    objtr.Unit_Code = clsCommon.myCstr(dr("unit_code"))
                    objtr.RGP_Qty = clsCommon.myCdbl(dr("rgp_qty"))
                    objtr.Balance_Qty = clsCommon.myCdbl(dr("balance_qty"))
                    objtr.Rate = clsCommon.myCdbl(dr("rate"))
                    objtr.MRP = clsCommon.myCdbl(dr("mrp"))
                    objtr.Amount = clsCommon.myCdbl(dr("amount"))
                    objtr.Specification = clsCommon.myCstr(dr("Specification"))
                    objtr.PO_Id = clsCommon.myCstr(dr("po_id"))
                    objtr.Against_Schedule_Code = clsCommon.myCstr(dr("Against_Schedule_Code"))
                    objtr.Module_Id = clsCommon.myCstr(dr("module_id"))
                    obj.Arr_BOM.Add(objtr)
                Next
            End If


        End If

        Return obj
    End Function
    '' Anubhooti 10-Dec-2014
    Public Shared Function GetItemDataForSRN(ByVal strPONo As String, ByVal strItemCode As String, ByVal NavType As NavigatorType) As clsRGPHead
        Dim obj As clsRGPHead = Nothing
        Dim qry As String = "SELECT TSPL_RGP_HEAD.RGP_No,TSPL_RGP_HEAD.Billing,TSPL_RGP_HEAD.RGP_Date,TSPL_RGP_HEAD.Vendor_Code,TSPL_RGP_HEAD.Vendor_Name,TSPL_RGP_HEAD.Status,TSPL_RGP_HEAD.On_Hold,TSPL_RGP_HEAD.Reason,TSPL_RGP_HEAD.Remarks,TSPL_RGP_HEAD.Reason,TSPL_RGP_HEAD.Posting_Date,TSPL_RGP_HEAD.VehicleNo,TSPL_RGP_HEAD.GPNo,TSPL_RGP_HEAD.GPDate,TSPL_RGP_HEAD.Doc_Type,TSPL_RGP_HEAD.Document_Amount,TSPL_RGP_HEAD.Location,TSPL_RGP_HEAD.Mode_Of_Transport,TSPL_RGP_HEAD.Cash_Memo_Detail,TSPL_LOCATION_MASTER.Location_Desc as LocationName,TSPL_RGP_HEAD.Delivered_By,TSPL_EMPLOYEE_MASTER.Emp_Name as DeliveredBYName,TSPL_RGP_HEAD.Department,TSPL_RGP_HEAD.Against_Sale,Against_JobWork, TSPL_RGP_HEAD.Is_Non_Inventory,CostCentre,CostCentreDesc,Against_Customer,TSPL_RGP_HEAD.IsThirdParty,TSPL_RGP_HEAD.SRN_Location_Code,TSPL_RGP_HEAD.InvoiceNo,TSPL_RGP_HEAD.PartyName,TSPL_RGP_HEAD.PartyAddress,TSPL_RGP_HEAD.DispatchDate, TSPL_RGP_HEAD.Against_NRGP,TSPL_RGP_HEAD.Is_Rejected,TSPL_RGP_HEAD.SRN_No,TSPL_RGP_HEAD.Item_Conversion_Type  FROM TSPL_RGP_HEAD left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_RGP_HEAD.Location left outer join TSPL_EMPLOYEE_MASTER on TSPL_EMPLOYEE_MASTER.EMP_CODE=TSPL_RGP_HEAD.Delivered_By where 2=2"
        Dim whrCls As String = ""
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            whrCls = " AND Location in (" + objCommonVar.strCurrUserLocations + ")"
        End If
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_RGP_HEAD.RGP_No = (select MIN(RGP_No) from TSPL_RGP_HEAD WHERE 1=1 " + whrCls + ")"
            Case NavigatorType.Last
                qry += " and TSPL_RGP_HEAD.RGP_No = (select Max(RGP_No) from TSPL_RGP_HEAD WHERE 1=1 " + whrCls + ")"
            Case NavigatorType.Current
                qry += " and TSPL_RGP_HEAD.RGP_No = '" + strPONo + "'"
            Case NavigatorType.Next
                qry += " and TSPL_RGP_HEAD.RGP_No = (select Min(RGP_No) from TSPL_RGP_HEAD where RGP_No>'" + strPONo + "' " + whrCls + ")"
            Case NavigatorType.Previous
                qry += " and TSPL_RGP_HEAD.RGP_No = (select Max(RGP_No) from TSPL_RGP_HEAD where RGP_No<'" + strPONo + "' " + whrCls + ")"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, Nothing)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsRGPHead()

            '----------------------------------------------------
            Try
                obj.chklocstion = clsCommon.myCstr(dt.Rows(0)("IsThirdParty"))
                obj.srnlocation = clsCommon.myCstr(dt.Rows(0)("SRN_Location_Code"))
                obj.srnlocationdesc = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select location_desc from tspl_location_master where location_code='" + obj.srnlocation + "'", Nothing))
            Catch exx As Exception
            End Try
            '-----------------------------------------------------------------------

            obj.RGP_No = clsCommon.myCstr(dt.Rows(0)("RGP_No"))
            obj.RGP_Date = clsCommon.myCstr(dt.Rows(0)("RGP_Date"))
            obj.Vendor_Code = clsCommon.myCstr(dt.Rows(0)("Vendor_Code"))
            obj.Vendor_Name = clsCommon.myCstr(dt.Rows(0)("Vendor_Name"))
            obj.Status = IIf(clsCommon.myCdbl(dt.Rows(0)("Status")) = 1, ERPTransactionStatus.Approved, ERPTransactionStatus.Pending)
            obj.On_Hold = IIf(clsCommon.myCdbl(dt.Rows(0)("On_Hold")) = 1, True, False)
            obj.Remarks = clsCommon.myCstr(dt.Rows(0)("Remarks"))
            obj.Reason = clsCommon.myCstr(dt.Rows(0)("Reason"))
            obj.Against_Sale = clsCommon.myCdbl(dt.Rows(0)("Against_Sale"))
            ''richa Ticket No BM00000003061 on 01/08/2014
            obj.Against_JobWork = clsCommon.myCdbl(dt.Rows(0)("Against_JobWork"))
            '-------------------------------------------
            If clsCommon.myLen(dt.Rows(0)("Posting_Date")) > 0 Then
                obj.Posting_Date = clsCommon.myCstr(dt.Rows(0)("Posting_Date"))
            End If
            obj.Mode_Of_Transport = clsCommon.myCstr(dt.Rows(0)("Mode_Of_Transport"))
            obj.Cash_Memo_Detail = clsCommon.myCstr(dt.Rows(0)("Cash_Memo_Detail"))
            obj.VehicleNo = clsCommon.myCstr(dt.Rows(0)("VehicleNo"))
            obj.GPNo = clsCommon.myCstr(dt.Rows(0)("GPNo"))
            obj.Location = clsCommon.myCstr(dt.Rows(0)("Location"))
            obj.LocationName = clsCommon.myCstr(dt.Rows(0)("LocationName"))
            obj.Delivered_By = clsCommon.myCstr(dt.Rows(0)("Delivered_By"))
            obj.Delivered_ByName = clsCommon.myCstr(dt.Rows(0)("DeliveredBYName"))
            If dt.Rows(0)("GPDate") IsNot DBNull.Value Then
                obj.GPDate = clsCommon.myCDate(dt.Rows(0)("GPDate"))
            End If
            obj.Doc_Type = clsCommon.myCstr(dt.Rows(0)("Doc_Type"))
            obj.Billing = clsCommon.myCstr(dt.Rows(0)("Billing"))
            obj.Document_Amount = clsCommon.myCstr(dt.Rows(0)("Document_Amount"))
            obj.Department = clsCommon.myCstr(dt.Rows(0)("Department"))
            obj.Is_Non_Inventory = clsCommon.myCdbl(dt.Rows(0)("Is_Non_Inventory"))
            obj.CostCentre = clsCommon.myCstr(dt.Rows(0)("CostCentre"))
            obj.CostCentreDesc = clsCommon.myCstr(dt.Rows(0)("CostCentreDesc"))
            obj.Against_Customer = clsCommon.myCdbl(dt.Rows(0)("Against_Customer"))
            obj.invoiceNo = clsCommon.myCstr(dt.Rows(0)("InvoiceNo"))
            obj.partyName = clsCommon.myCstr(dt.Rows(0)("PartyName"))
            obj.partyAddress = clsCommon.myCstr(dt.Rows(0)("PartyAddress"))
            If clsCommon.myLen(dt.Rows(0)("DispatchDate")) > 0 Then
                obj.DispatchDate = clsCommon.myCDate(dt.Rows(0)("DispatchDate"))
            End If
            obj.Against_NRGP = clsCommon.myCstr(dt.Rows(0)("Against_NRGP"))
            '' Anubhooti 09-Oct-2014 BM00000003663
            obj.Is_Rejected = clsCommon.myCdbl(dt.Rows(0)("Is_Rejected"))
            obj.SRN_No = clsCommon.myCstr(dt.Rows(0)("SRN_No"))
            ''
            '' Anubhooti 10-Dec-2014 BM00000003662
            obj.Item_Conversion_Type = clsCommon.myCstr(dt.Rows(0)("Item_Conversion_Type"))
            ''
            'qry = "SELECT TSPL_RGP_DETAIL.MRP,TSPL_RGP_DETAIL.RGP_No,TSPL_RGP_DETAIL.Line_No,TSPL_RGP_DETAIL.Item_Code,TSPL_RGP_DETAIL.Item_Desc,TSPL_RGP_DETAIL.RGP_Qty, (Select ISNULL(RGP_Qty,0) from TSPL_RGP_DETAIL as dtl Where dtl.RGP_No=TSPL_RGP_HEAD.Against_NRGP AND dtl.Item_Code=TSPL_RGP_DETAIL.Item_Code) as NRGP_Qty, TSPL_RGP_DETAIL.Unit_code,TSPL_RGP_DETAIL.Item_Cost,TSPL_RGP_DETAIL.Amount, TSPL_RGP_DETAIL.Last_RGP_No, TSPL_RGP_DETAIL.Last_RGP_Date, TSPL_RGP_DETAIL.Specification FROM TSPL_RGP_DETAIL LEFT OUTER JOIN TSPL_RGP_HEAD ON TSPL_RGP_HEAD.RGP_No=TSPL_RGP_DETAIL.RGP_No where TSPL_RGP_DETAIL.RGP_No='" & obj.RGP_No & "'  ORDER BY TSPL_RGP_DETAIL.Line_No"
            If clsCommon.myLen(obj.Against_NRGP) <= 0 Then
                qry = "Select XXX.MRP, XXX.RGP_No, XXX.Line_No, XXX.Item_Code, XXX.Item_Desc, XXX.RGP_Qty, XXX.NRGP_Qty, XXX.Unit_code, XXX.Item_Cost,XXX.Approx_Cost, XXX.Amount, XXX.Last_RGP_No, XXX.Last_RGP_Date, XXX.Specification, RGP_Qty-(select ISNULL(SUM(RGP_Qty),0) from TSPL_RGP_HEAD LEFT OUTER JOIN TSPL_RGP_DETAIL ON TSPL_RGP_DETAIL.RGP_No=TSPL_RGP_HEAD.RGP_No WHERE TSPL_RGP_HEAD.Against_NRGP='" & obj.RGP_No & "' AND TSPL_RGP_DETAIL.Item_Code=XXX.Item_Code) as Balance_Qty from ("
            Else
                qry = "Select XXX.MRP, XXX.RGP_No, XXX.Line_No, XXX.Item_Code, XXX.Item_Desc, XXX.RGP_Qty, XXX.NRGP_Qty, XXX.Unit_code, XXX.Item_Cost,XXX.Approx_Cost, XXX.Amount, XXX.Last_RGP_No, XXX.Last_RGP_Date, XXX.Specification, NRGP_Qty-(select ISNULL(SUM(RGP_Qty),0) from TSPL_RGP_HEAD LEFT OUTER JOIN TSPL_RGP_DETAIL ON TSPL_RGP_DETAIL.RGP_No=TSPL_RGP_HEAD.RGP_No WHERE TSPL_RGP_HEAD.Against_NRGP='" & obj.Against_NRGP & "' AND TSPL_RGP_DETAIL.Item_Code=XXX.Item_Code AND TSPL_RGP_HEAD.RGP_No<>'" & obj.RGP_No & "') as Balance_Qty from ("
            End If
            qry += " SELECT TSPL_RGP_DETAIL.MRP,TSPL_RGP_DETAIL.RGP_No,TSPL_RGP_DETAIL.Line_No,TSPL_RGP_DETAIL.Item_Code,TSPL_RGP_DETAIL.Item_Desc,TSPL_RGP_DETAIL.RGP_Qty," & _
        " (Select ISNULL(RGP_Qty,0) from TSPL_RGP_DETAIL as dtl Where dtl.RGP_No=TSPL_RGP_HEAD.Against_NRGP AND dtl.Item_Code=TSPL_RGP_DETAIL.Item_Code) as NRGP_Qty," & _
        " TSPL_RGP_DETAIL.Unit_code,TSPL_RGP_DETAIL.Item_Cost,isnull(TSPL_RGP_DETAIL.Approx_Cost,0) AS Approx_Cost,TSPL_RGP_DETAIL.Amount, TSPL_RGP_DETAIL.Last_RGP_No, TSPL_RGP_DETAIL.Last_RGP_Date," & _
        " TSPL_RGP_DETAIL.Specification, TSPL_RGP_HEAD.Against_NRGP FROM TSPL_RGP_DETAIL" & _
        " LEFT OUTER JOIN TSPL_RGP_HEAD ON TSPL_RGP_HEAD.RGP_No=TSPL_RGP_DETAIL.RGP_No where TSPL_RGP_DETAIL.RGP_No='" & obj.RGP_No & "' AND TSPL_RGP_DETAIL.Item_Code='" & strItemCode & "'" & _
        " ) XXX ORDER BY XXX.Line_No"
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry, Nothing)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj.Arr = New List(Of clsRGPDetail)
                Dim objTr As clsRGPDetail
                For Each dr As DataRow In dt.Rows
                    objTr = New clsRGPDetail
                    objTr.Item_MRP = clsCommon.myCdbl(dr("MRP"))
                    objTr.RGP_No = clsCommon.myCstr(dr("RGP_No"))
                    objTr.Line_No = clsCommon.myCstr(dr("Line_No"))
                    objTr.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                    objTr.Item_Desc = clsCommon.myCstr(dr("Item_Desc"))
                    objTr.RGP_Qty = clsCommon.myCdbl(dr("RGP_Qty"))
                    objTr.NRGP_Qty = clsCommon.myCdbl(dr("NRGP_Qty"))
                    objTr.Balance_Qty = clsCommon.myCdbl(dr("Balance_Qty"))
                    objTr.Unit_code = clsCommon.myCstr(dr("Unit_code"))
                    objTr.Item_Cost = clsCommon.myCdbl(dr("Item_Cost"))
                    objTr.Last_RGP_No = clsCommon.myCstr(dr("Last_RGP_No"))
                    objTr.Last_RGP_Date = clsCommon.myCstr(dr("Last_RGP_Date"))
                    objTr.Amount = clsCommon.myCdbl(dr("Amount"))
                    objTr.Specification = clsCommon.myCstr(dr("Specification"))
                    '' Anubhooti 06-Feb-2015 (Newly Added Approx_Cost fetch ,also added in query)
                    objTr.Approx_Cost = clsCommon.myCdbl(dr("Approx_Cost"))
                    obj.Arr.Add(objTr)
                Next
            End If
        End If

        Return obj
    End Function
    ''
    Public Shared Function PostData(ByVal strDocNo As String) As Boolean

        Dim trans As SqlTransaction = Nothing
        Try
            trans = clsDBFuncationality.GetTransactin()
            Dim isSaved As Boolean = True
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("SRN No not found to Post")
            End If
            Dim strPostDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt")
            Dim obj As clsRGPHead = clsRGPHead.GetData(strDocNo, NavigatorType.Current, trans)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.RGP_No) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "Purchase Order", "RGP/NRGP", obj.Location, obj.RGP_Date, trans)
            If (obj.Status = ERPTransactionStatus.Approved) Then
                Throw New Exception("Already Post on :" + obj.Posting_Date)
            End If
            If (obj.On_Hold) Then
                Throw New Exception("Document No " + obj.RGP_No + " Is On Hold.Can't Post it")
            End If

            Dim qry As String = ""
            Dim ArrInventoryMovement As List(Of clsInventoryMovement) = New List(Of clsInventoryMovement)
            Dim objInventoryMovemnt As Object
            For Each objTr As clsRGPDetail In obj.Arr
                Dim strItemType As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Item_Type from TSPL_ITEM_MASTER where Item_Code='" + objTr.Item_Code + "'", trans))
                Dim strItemTypeToSave As String = ""
                If clsCommon.CompairString(strItemType, "R") = CompairStringResult.Equal Then
                    strItemTypeToSave = "RM"
                ElseIf clsCommon.CompairString(strItemType, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(strItemType, "O") = CompairStringResult.Equal Then
                    strItemTypeToSave = "OT"
                ElseIf clsCommon.CompairString(strItemType, "F") = CompairStringResult.Equal Then
                    strItemTypeToSave = "FT"
                End If
                objInventoryMovemnt = New clsInventoryMovement()
                If clsCommon.CompairString(obj.Doc_Type, "NRGPR") = CompairStringResult.Equal OrElse clsCommon.CompairString(obj.Doc_Type, "RGPR") = CompairStringResult.Equal Then
                    objInventoryMovemnt.InOut = "I"
                Else
                    objInventoryMovemnt.InOut = "O"
                End If
                objInventoryMovemnt.Location_Code = obj.Location
                If obj.Against_Customer = 1 Then
                    objInventoryMovemnt.Cust_Code = obj.Vendor_Code
                    objInventoryMovemnt.Cust_Name = obj.Vendor_Name
                Else
                    objInventoryMovemnt.Vendor_Code = obj.Vendor_Code
                    objInventoryMovemnt.Vendor_Name = obj.Vendor_Name
                End If
                objInventoryMovemnt.Item_Code = objTr.Item_Code
                objInventoryMovemnt.Item_Desc = objTr.Item_Desc
                objInventoryMovemnt.Qty = objTr.RGP_Qty
                objInventoryMovemnt.UOM = objTr.Unit_code
                objInventoryMovemnt.Basic_Cost = objTr.Item_Cost
                objInventoryMovemnt.Net_Cost = objTr.Amount
                objInventoryMovemnt.ItemType = strItemTypeToSave
                Dim item_Purchase_Class As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Purchase_Class_Code from TSPL_ITEM_MASTER where Item_Code='" & objTr.Item_Code & "'", trans))
                Dim qry1 As String = "select Loc_Segment_Code from TSPL_LOCATION_MASTER where Location_Code='" + obj.Location + "'"
                Dim strLocatinSegment As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry1, trans))
                If clsCommon.myLen(item_Purchase_Class) > 0 Then
                    Dim Inventory_Purchase_code As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Inv_Control_Account from TSPL_PURCHASE_ACCOUNTS where Purchase_Class_Code='" & item_Purchase_Class & "'", trans))
                    If clsCommon.myLen(Inventory_Purchase_code) > 0 Then
                        If clsCommon.CompairString(objInventoryMovemnt.InOut, "I") = CompairStringResult.Equal Then
                            objInventoryMovemnt.Inventory_DrAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(Inventory_Purchase_code, strLocatinSegment, True, trans)
                        Else
                            objInventoryMovemnt.Inventory_CrAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(Inventory_Purchase_code, strLocatinSegment, True, trans)
                        End If
                    End If
                End If
                ArrInventoryMovement.Add(objInventoryMovemnt)
            Next

            If obj.Is_Non_Inventory = 0 Then
                isSaved = isSaved AndAlso clsInventoryMovement.SaveData(obj.Doc_Type, obj.RGP_No, obj.RGP_Date, clsCommon.GetPrintDate(strPostDate, "dd/MM/yyyy"), ArrInventoryMovement, trans)
            End If
            CreateJV(obj, trans, "")
            ''
            '========================================================================
            If obj.Against_JobWork = 1 AndAlso obj.Against_BOM = 1 Then
                SaveRGPBOMDetail(obj, trans)
            End If
            '==============================================================

            qry = "Update TSPL_RGP_HEAD set Status=1, Posting_Date='" + strPostDate + "',Modify_By='" + objCommonVar.CurrentUserCode + "' where RGP_No='" + strDocNo + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, clsCommon.myCstr(strDocNo), "TSPL_RGP_HEAD", "RGP_No", trans)
            If objCommonVar.InternalSMSEmailinPurchaseModule = True Then
                CreateInternalEmailSMS(obj, trans)
            End If

            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function CreateJV(ByVal obj As clsRGPHead, ByVal trans As SqlTransaction, ByVal strJVNo As String) As Boolean
        Dim RecoControlACC As String = ""
        If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowPurchaseAccounting, clsFixedParameterCode.AllowPurchaseAccounting, trans)) = 0 Then
            RecoControlACC = "I"
        End If
        Dim settCreateJVOFRGPNRGPAndItsRetrun As Boolean = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.CreateJVOFRGPNRGPAndItsRetrun, clsFixedParameterCode.CreateJVOFRGPNRGPAndItsRetrun, trans)) = 1)
        If clsCommon.CompairString(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowPurchaseAccounting, clsFixedParameterCode.AllowPurchaseAccounting, trans)), "0") = CompairStringResult.Equal Then
            Dim ArryLstGLAC As ArrayList = New ArrayList()
            Dim flag As Boolean = False
            Dim qry As String
            If clsCommon.CompairString(obj.Doc_Type, "RGP") = CompairStringResult.Equal OrElse clsCommon.CompairString(obj.Doc_Type, "RGPR") = CompairStringResult.Equal Then

                If (clsCommon.CompairString(obj.Against_JobWork, "1") = CompairStringResult.Equal) Then
                    Dim PurSetJobWork As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select ISNULL(Job_Work_Account,'') AS Job_Work_Account From TSPL_PURCHASE_SETTINGS", trans))
                    For Each objTr As clsRGPDetail In obj.Arr
                        qry = "select TSPL_ITEM_MASTER.Purchase_Class_Code,TSPL_PURCHASE_ACCOUNTS.Inv_Control_Account,TSPL_PURCHASE_ACCOUNTS.Inv_Payable_Clearing,TSPL_PURCHASE_ACCOUNTS.Assembly_Cost_Credit,TSPL_PURCHASE_ACCOUNTS.Breakage_Gl_Account,TSPL_PURCHASE_ACCOUNTS.RM_Consumption,TSPL_PURCHASE_ACCOUNTS.Reserve_Stock  from TSPL_ITEM_MASTER left outer join TSPL_PURCHASE_ACCOUNTS on TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code=TSPL_ITEM_MASTER.Purchase_Class_Code where TSPL_ITEM_MASTER.Item_Code='" + objTr.Item_Code + "'"
                        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                        If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                            Throw New Exception("Please set Purchase Account set for item " + objTr.Item_Code + "(" + objTr.Item_Desc + ")")
                        End If
                        ''1)
                        Dim strInvCtrlAC As String = clsCommon.myCstr(dt.Rows(0)("Inv_Control_Account"))
                        If clsCommon.myLen(strInvCtrlAC) <= 0 Then
                            Throw New Exception("Please set Inventory Control Account for Purchase Account set Code :" + clsCommon.myCstr(dt.Rows(0)("Purchase_Class_Code")) + " and Item: " + objTr.Item_Code + "(" + objTr.Item_Desc + ") ")
                        End If
                        strInvCtrlAC = clsERPFuncationality.ChangeGLAccountLocationSegment(strInvCtrlAC, obj.Location, trans)
                        If clsCommon.CompairString(obj.Doc_Type, "RGP") = CompairStringResult.Equal Then
                            Dim AccCr() As String = {strInvCtrlAC, -1 * Math.Round(((objTr.Amount)), 2, MidpointRounding.ToEven), "", "", "", "", "", "", RecoControlACC}
                            ArryLstGLAC.Add(AccCr)
                            If clsCommon.CompairString(RecoControlACC, "I") = CompairStringResult.Equal Then
                                clsInventoryMovement.UpdateInvControlAccount(obj.RGP_No, obj.Doc_Type, objTr.Item_Code, "", strInvCtrlAC, "", trans)
                            End If
                        Else
                            Dim AccCr() As String = {strInvCtrlAC, Math.Round(((objTr.Amount)), 2, MidpointRounding.ToEven), "", "", "", "", "", "", RecoControlACC}
                            ArryLstGLAC.Add(AccCr)
                            If clsCommon.CompairString(RecoControlACC, "I") = CompairStringResult.Equal Then
                                clsInventoryMovement.UpdateInvControlAccount(obj.RGP_No, obj.Doc_Type, objTr.Item_Code, strInvCtrlAC, "", "", trans)
                            End If
                        End If
                        If obj.Is_Repair Then
                            PurSetJobWork = clsCommon.myCstr(dt.Rows(0)("Reserve_Stock"))
                            If clsCommon.myLen(PurSetJobWork) <= 0 Then
                                Throw New Exception("Please set RGP clearing account of purchase account set " + clsCommon.myCstr(dt.Rows(0)("Purchase_Class_Code")))
                            End If
                        End If
                        If clsCommon.myLen(PurSetJobWork) > 0 Then
                            PurSetJobWork = clsERPFuncationality.ChangeGLAccountLocationSegment(PurSetJobWork, obj.Location, trans)
                            If clsCommon.CompairString(obj.Doc_Type, "RGP") = CompairStringResult.Equal Then
                                Dim AccDr() As String = {PurSetJobWork, Math.Round(((objTr.Amount)), 2, MidpointRounding.ToEven)}
                                ArryLstGLAC.Add(AccDr)
                            Else
                                Dim AccDr() As String = {PurSetJobWork, Math.Round(((objTr.Amount)) * -1, 2, MidpointRounding.ToEven)}
                                ArryLstGLAC.Add(AccDr)
                            End If
                        Else
                            Throw New Exception("Please set job work account in purchase settings")
                        End If
                    Next
                    If obj.Is_Repair Then
                        If clsCommon.CompairString(obj.Doc_Type, "RGP") = CompairStringResult.Equal Then
                            flag = True
                            transportSql.FunGrnlEntryWithTrans(obj.Location, False, strJVNo, trans, obj.RGP_Date, "RGP Job Work Against-" & obj.RGP_No & "", "RG-JW", "RGP Job Work", obj.RGP_No, obj.Remarks, "V", obj.Vendor_Code, obj.Vendor_Name, objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLstGLAC)
                        Else
                            flag = True
                            transportSql.FunGrnlEntryWithTrans(obj.Location, False, strJVNo, trans, obj.RGP_Date, "RGP Return Job Work Against-" & obj.RGP_No & "", "RGR-JW", "RGP Return Job Work", obj.RGP_No, obj.Remarks, "V", obj.Vendor_Code, obj.Vendor_Name, objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLstGLAC)
                        End If
                    End If
                ElseIf obj.JVDisplayType = 1 Then
                    For Each objTr As clsRGPDetail In obj.Arr
                        qry = "select TSPL_ITEM_MASTER.Sale_Class_Code,TSPL_SALES_ACCOUNTS.DisplayPurpose_Account,TSPL_ITEM_MASTER.Purchase_Class_Code,TSPL_PURCHASE_ACCOUNTS.Inv_Control_Account from TSPL_ITEM_MASTER left outer join TSPL_SALES_ACCOUNTS on TSPL_SALES_ACCOUNTS.Sales_Class_Code=TSPL_ITEM_MASTER.Sale_Class_Code left outer join TSPL_PURCHASE_ACCOUNTS on TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code=TSPL_ITEM_MASTER.Purchase_Class_Code where TSPL_ITEM_MASTER.Item_Code='" + objTr.Item_Code + "'"
                        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                        If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                            Throw New Exception("Please set Sale/Purchase Account set for item " + objTr.Item_Code + "(" + objTr.Item_Desc + ")")
                        End If
                        Dim strAccount As String = clsCommon.myCstr(dt.Rows(0)("DisplayPurpose_Account"))
                        If clsCommon.myLen(strAccount) <= 0 Then
                            Throw New Exception("Please set Display Purpose Account for sale Account set Code :" + clsCommon.myCstr(dt.Rows(0)("Sale_Class_Code")) + " and Item: " + objTr.Item_Code + "(" + objTr.Item_Desc + ") ")
                        End If
                        strAccount = clsERPFuncationality.ChangeGLAccountLocationSegment(strAccount, obj.Location, trans)
                        If clsCommon.CompairString(obj.Doc_Type, "RGP") = CompairStringResult.Equal Then
                            Dim Acc1() As String = {strAccount, Math.Round(((objTr.Amount)), 2, MidpointRounding.ToEven)}
                            ArryLstGLAC.Add(Acc1)
                        Else
                            Dim Acc1() As String = {strAccount, Math.Round(((objTr.Amount)) * -1, 2, MidpointRounding.ToEven)}
                            ArryLstGLAC.Add(Acc1)
                        End If

                        strAccount = clsCommon.myCstr(dt.Rows(0)("Inv_Control_Account"))
                        If clsCommon.myLen(strAccount) <= 0 Then
                            Throw New Exception("Please set Inventory Control Account for Purchase Account set Code :" + clsCommon.myCstr(dt.Rows(0)("Purchase_Class_Code")) + " and Item: " + objTr.Item_Code + "(" + objTr.Item_Desc + ") ")
                        End If
                        strAccount = clsERPFuncationality.ChangeGLAccountLocationSegment(strAccount, obj.Location, trans)
                        If clsCommon.CompairString(obj.Doc_Type, "RGP") = CompairStringResult.Equal Then
                            Dim AccCr() As String = {strAccount, -1 * Math.Round(((objTr.Amount)), 2, MidpointRounding.ToEven), "", "", "", "", "", "", RecoControlACC}
                            ArryLstGLAC.Add(AccCr)
                            If clsCommon.CompairString(RecoControlACC, "I") = CompairStringResult.Equal Then
                                clsInventoryMovement.UpdateInvControlAccount(obj.RGP_No, obj.Doc_Type, objTr.Item_Code, "", strAccount, "", trans)
                            End If
                        Else
                            Dim AccCr() As String = {strAccount, Math.Round(((objTr.Amount)), 2, MidpointRounding.ToEven), "", "", "", "", "", "", RecoControlACC}
                            ArryLstGLAC.Add(AccCr)
                            If clsCommon.CompairString(RecoControlACC, "I") = CompairStringResult.Equal Then
                                clsInventoryMovement.UpdateInvControlAccount(obj.RGP_No, obj.Doc_Type, objTr.Item_Code, strAccount, "", "", trans)
                            End If
                        End If
                    Next
                    If clsCommon.CompairString(obj.Doc_Type, "RGP") = CompairStringResult.Equal Then
                        flag = True
                        transportSql.FunGrnlEntryWithTrans(obj.Location, False, strJVNo, trans, obj.RGP_Date, "RGP Job Work Against-" & obj.RGP_No & "", "RG-JW", "RGP Job Work", obj.RGP_No, obj.Remarks, "V", obj.Vendor_Code, obj.Vendor_Name, objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLstGLAC)
                    Else
                        flag = True
                        transportSql.FunGrnlEntryWithTrans(obj.Location, False, strJVNo, trans, obj.RGP_Date, "RGP Return Job Work Against-" & obj.RGP_No & "", "RGR-JW", "RGP Return Job Work", obj.RGP_No, obj.Remarks, "V", obj.Vendor_Code, obj.Vendor_Name, objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLstGLAC)
                    End If
                End If
            End If

            If settCreateJVOFRGPNRGPAndItsRetrun AndAlso Not flag AndAlso obj.Is_Non_Inventory = 0 Then
                For Each objTr As clsRGPDetail In obj.Arr
                    Dim RI As Integer = -1
                    Dim RIReverse As Integer = 1
                    If clsCommon.CompairString(obj.Doc_Type, "RGP") = CompairStringResult.Equal OrElse clsCommon.CompairString(obj.Doc_Type, "NRGP") = CompairStringResult.Equal Then
                        RI = 1
                        RIReverse = -1
                    End If

                    qry = "select TSPL_ITEM_MASTER.Purchase_Class_Code,TSPL_PURCHASE_ACCOUNTS.Inv_Control_Account,TSPL_PURCHASE_ACCOUNTS.Store_Consumption_Acc,TSPL_PURCHASE_ACCOUNTS.Reserve_Stock  from TSPL_ITEM_MASTER left outer join TSPL_PURCHASE_ACCOUNTS on TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code=TSPL_ITEM_MASTER.Purchase_Class_Code where TSPL_ITEM_MASTER.Item_Code='" + objTr.Item_Code + "'"
                    Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                    If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                        Throw New Exception("Please set Purchase Account set for item " + objTr.Item_Code + "(" + objTr.Item_Desc + ")")
                    End If
                    Dim strInvCtrlAC As String = clsCommon.myCstr(dt.Rows(0)("Inv_Control_Account"))
                    If clsCommon.myLen(strInvCtrlAC) <= 0 Then
                        Throw New Exception("Please set Inventory Control Account for Purchase Account set Code :" + clsCommon.myCstr(dt.Rows(0)("Purchase_Class_Code")) + " and Item: " + objTr.Item_Code + "(" + objTr.Item_Desc + ") ")
                    End If
                    strInvCtrlAC = clsERPFuncationality.ChangeGLAccountLocationSegment(strInvCtrlAC, obj.Location, trans)

                    Dim AccCr() As String = {strInvCtrlAC, RIReverse * Math.Round(((objTr.Amount)), 2, MidpointRounding.ToEven), "", "", "", "", "", "", RecoControlACC}
                    ArryLstGLAC.Add(AccCr)
                    If clsCommon.CompairString(RecoControlACC, "I") = CompairStringResult.Equal Then
                        clsInventoryMovement.UpdateInvControlAccount(obj.RGP_No, obj.Doc_Type, objTr.Item_Code, "", strInvCtrlAC, "", trans)
                    End If


                    Dim strStoreConsumbtion As String = clsCommon.myCstr(dt.Rows(0)("Store_Consumption_Acc"))
                    If clsCommon.CompairString(obj.Doc_Type, "RGP") = CompairStringResult.Equal OrElse clsCommon.CompairString(obj.Doc_Type, "RGPR") = CompairStringResult.Equal Then
                        strStoreConsumbtion = clsCommon.myCstr(dt.Rows(0)("Reserve_Stock"))
                        If clsCommon.myLen(strStoreConsumbtion) <= 0 Then
                            Throw New Exception("Please set RGP Clearing Account for Purchase Account set Code :" + clsCommon.myCstr(dt.Rows(0)("Purchase_Class_Code")) + " and Item: " + objTr.Item_Code + "(" + objTr.Item_Desc + ") ")
                        End If
                    End If
                    If clsCommon.myLen(strStoreConsumbtion) <= 0 Then
                        Throw New Exception("Please set Store Consumption Account for Purchase Account set Code :" + clsCommon.myCstr(dt.Rows(0)("Purchase_Class_Code")) + " and Item: " + objTr.Item_Code + "(" + objTr.Item_Desc + ") ")
                    End If
                    strStoreConsumbtion = clsERPFuncationality.ChangeGLAccountLocationSegment(strStoreConsumbtion, obj.Location, trans)
                    Dim AccDr() As String = {strStoreConsumbtion, RI * Math.Round(((objTr.Amount)), 2, MidpointRounding.ToEven)}
                    ArryLstGLAC.Add(AccDr)
                Next
                transportSql.FunGrnlEntryWithTrans(obj.Location, False, strJVNo, trans, obj.RGP_Date, obj.Doc_Type + " Against-" & obj.RGP_No & "", obj.Doc_Type, obj.Doc_Type, obj.RGP_No, obj.Remarks, "V", obj.Vendor_Code, obj.Vendor_Name, objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLstGLAC)
            End If
        End If
        Return True
    End Function
    Public Shared Function SaveRGPBOMDetail(ByVal obj As clsRGPHead, ByVal trans As SqlTransaction) As Boolean
        Dim coll As New Hashtable()
        Try
            If obj IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                For Each objtr As clsRGPDetail In obj.Arr
                    coll = New Hashtable()

                    clsCommon.AddColumnsForChange(coll, "RGP_No", obj.RGP_No)
                    clsCommon.AddColumnsForChange(coll, "Item_Code", objtr.Item_Code)
                    clsCommon.AddColumnsForChange(coll, "Unit_Code", objtr.Unit_code)
                    clsCommon.AddColumnsForChange(coll, "Qty", objtr.RGP_Qty)
                    clsCommon.AddColumnsForChange(coll, "Vendor_Code", obj.Vendor_Code)
                    clsCommon.AddColumnsForChange(coll, "InOut", "O")
                    clsCommon.AddColumnsForChange(coll, "GRN_No", Nothing)
                    clsCommon.AddColumnsForChange(coll, "SRN_No", Nothing)

                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_RGP_BOM_DETAIL", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            coll = Nothing
        End Try

        Return True
    End Function

    Private Shared Sub CreateInternalEmailSMS(ByVal obj As clsRGPHead, ByVal trans As SqlTransaction)
        Dim itemName As String = ""
        Dim UOM As String = ""
        Dim qty As String = ""
        Dim ItemDetail As String = ""
        Dim dtContent As DataTable = clsDBFuncationality.GetDataTable("SELECT SMS_Text,Email_Text,Email_subject from TSPL_ES_Content where Form_ID='" + clsUserMgtCode.mbtnGatePass + "2" + "'", trans)

        Dim qry2 As String = "select TSPL_USER_MASTER.User_Code from TSPL_USER_MASTER " & _
               " left join TSPL_RGP_HEAD on TSPL_RGP_HEAD.Created_By=TSPL_USER_MASTER.User_Code " & _
               " where TSPL_RGP_HEAD.RGP_No='" + obj.RGP_No + "'"
        Dim StrUserCode As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry2, trans))
        Dim arrMobileNo As New List(Of String)
        Dim arrMailID As List(Of String) = clsERPFuncationality.ReportingMailIdandPhone(StrUserCode, arrMobileNo, trans)
        If dtContent IsNot Nothing AndAlso dtContent.Rows.Count > 0 AndAlso ((arrMailID IsNot Nothing AndAlso arrMailID.Count > 0) Or (arrMobileNo IsNot Nothing AndAlso arrMobileNo.Count > 0)) Then

            If (obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0) Then
                For Each objdetail As clsRGPDetail In obj.Arr
                    itemName = clsCommon.myCstr(objdetail.Item_Desc)
                    UOM = clsCommon.myCstr(objdetail.Unit_code)
                    qty = clsCommon.myCstr(objdetail.RGP_Qty)
                    ItemDetail += itemName + " " + UOM + "-" + qty + Environment.NewLine
                Next
            End If

            If clsCommon.myLen(dtContent.Rows(0)("Email_Text")) > 0 AndAlso (arrMailID IsNot Nothing AndAlso arrMailID.Count > 0) Then
                Dim objEmailH As New clsEMailHead()
                objEmailH.arrEMail = New List(Of String)()
                objEmailH.arrEMail = arrMailID
                objEmailH.Email_Subject = clsCommon.myCstr(dtContent.Rows(0)("Email_subject"))
                objEmailH.Email_Text = clsCommon.myCstr(dtContent.Rows(0)("Email_Text"))
                objEmailH.Email_Text = objEmailH.Email_Text.Replace(clsEmailSMSConstants.DOC_NO, obj.RGP_No)
                objEmailH.Email_Text = objEmailH.Email_Text.Replace(clsEmailSMSConstants.DOC_Date, clsCommon.GetPrintDate(obj.RGP_Date, "dd/MMM/yyyy"))
                objEmailH.Email_Text = objEmailH.Email_Text.Replace(clsEmailSMSConstants.Doc_Amount, clsCommon.myCstr(obj.Document_Amount))
                objEmailH.Email_Text = objEmailH.Email_Text.Replace(frmEMailAndSMSSetting.ItemDetail, ItemDetail)
                objEmailH.SaveData(clsUserMgtCode.mbtnGatePass, objEmailH, trans)
                objEmailH = Nothing

            End If

            If clsCommon.myLen(dtContent.Rows(0)("SMS_Text")) > 0 AndAlso (arrMobileNo IsNot Nothing AndAlso arrMobileNo.Count > 0) Then
                Dim objSMSH As New clsSMSHead()
                objSMSH.arrMobilNo = New List(Of String)()
                objSMSH.arrMobilNo = arrMobileNo
                objSMSH.SMS_Text = clsCommon.myCstr(dtContent.Rows(0)("SMS_Text"))
                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(clsEmailSMSConstants.DOC_NO, obj.RGP_No)
                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(clsEmailSMSConstants.DOC_Date, clsCommon.GetPrintDate(obj.RGP_Date, "dd/MMM/yyyy"))
                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(clsEmailSMSConstants.Doc_Amount, clsCommon.myCstr(obj.Document_Amount))
                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.ItemDetail, ItemDetail)
                objSMSH.SaveData(clsUserMgtCode.mbtnGatePass, objSMSH, trans)
                objSMSH = Nothing
            End If
        End If
    End Sub

    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim isSaved As Boolean = False
        If (clsCommon.myLen(strCode) <= 0) Then
            Throw New Exception("Purchase Order No not found to Delete")
        End If
        Dim obj As clsRGPHead = clsRGPHead.GetData(strCode, NavigatorType.Current)
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.RGP_No) > 0) Then
            Try
                clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "Purchase Order", "RGP/NRGP", obj.Location, obj.RGP_Date, trans)
                If (obj.Status = 1) Then
                    Throw New Exception("Already Posted on :" + obj.Posting_Date)
                End If

                clsSerializeInvenotry.DeleteData(obj.Doc_Type, obj.RGP_No, trans)

                HistoryUpdate(strCode, trans)
                Dim qry As String = "delete from TSPL_RGP_DETAIL where RGP_No='" + strCode + "'"
                isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)

                qry = "delete from TSPL_RGP_JOB_WORK_DETAIL where RGP_No='" + strCode + "'"
                isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
                clsBatchInventory.DeleteData(obj.Doc_Type, obj.RGP_No, trans)

                qry = "delete from TSPL_RGP_HEAD where RGP_No='" + strCode + "'"
                isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
                isSaved = isSaved AndAlso clsCustomFieldValues.DeleteData(obj.Form_ID, strCode, trans)
                If (isSaved) Then
                    trans.Commit()
                Else
                    trans.Rollback()
                End If
            Catch ex As Exception
                trans.Rollback()
                Throw New Exception(ex.Message)
            End Try
        End If
        Return isSaved
    End Function


    Public Shared Function ReverseAndUnpost(ByVal strCode As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim Qry As String = "select Status from TSPL_RGP_HEAD where RGP_No='" + strCode + "'"
            If Not clsCommon.myCdbl(clsDBFuncationality.getSingleValue(Qry, trans)) = 1 Then
                Throw New Exception("Transaction status should be posted for reverse and unpost")
            End If

            Qry = "select invoice_No as DocNo,'Scrap Invoice' as DocType  from TSPL_SCRAPINVOICE_HEAD where NRG_No='" + strCode + "'"
            Qry += " Union all "
            Qry += " select distinct SRN_No as DocNo,'SRN' as DocType from TSPL_SRN_DETAIL where RGP_Id='" + strCode + "'"
            Qry += " Union all "
            Qry += " select distinct GRN_No as DocNo,'GRN' as DocType from TSPL_GRN_DETAIL where Against_RGP_No='" + strCode + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Qry = "Current RGP/NRGP is used in following Transactions -"
                For Each dr As DataRow In dt.Rows
                    Qry += Environment.NewLine + clsCommon.myCstr(dr("DocType")) + "-" + clsCommon.myCstr(dr("DocNo"))
                Next
                Throw New Exception(Qry)
            End If

            Qry = "select InOut,Trans_Type,Item_Code,Item_Desc,Location_Code,case when InOut='I' then -1 else 1 end *Qty as Qty ,UOM,MRP,ItemType,case when InOut='I' then -1 else 1 end* Basic_Cost as Basic_Cost from TSPL_INVENTORY_MOVEMENT where Source_Doc_No='" + strCode + "' and Trans_Type in ('NRGP','RGP')"
            dt = clsDBFuncationality.GetDataTable(Qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim ArrLocationDetails As List(Of clsItemLocationDetails) = New List(Of clsItemLocationDetails)
                For Each objtr As DataRow In dt.Rows
                    Dim dblConvFac As Double = clsItemMaster.GetConvertionFactor(clsCommon.myCstr(objtr("Item_Code")), clsCommon.myCstr(objtr("UOM")), trans)
                    Dim objLocationDetails As New clsItemLocationDetails()
                    objLocationDetails.Item_Code = clsCommon.myCstr(objtr("Item_Code"))
                    objLocationDetails.Item_Desc = clsCommon.myCstr(objtr("Item_Desc"))
                    objLocationDetails.Location_Code = clsCommon.myCstr(objtr("Location_Code"))
                    objLocationDetails.Location_Desc = clsLocation.GetName(objLocationDetails.Location_Code, trans)
                    objLocationDetails.Item_Qty = clsCommon.myCdbl(objtr("Qty")) / dblConvFac
                    objLocationDetails.Amount = clsCommon.myCdbl(objtr("Basic_Cost"))
                    objLocationDetails.MRP = clsCommon.myCdbl(objtr("MRP")) * dblConvFac
                    objLocationDetails.ItemType = clsCommon.myCstr(objtr("ItemType"))
                    ArrLocationDetails.Add(objLocationDetails)
                Next
                Dim strPostDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy")
                clsItemLocationDetails.SaveData(strPostDate, ArrLocationDetails, trans)
            End If


            Qry = "delete from TSPL_RGP_BOM_DETAIL where rgp_no='" + strCode + "' and inout='O'"
            clsDBFuncationality.ExecuteNonQuery(Qry, trans)
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, clsCommon.myCstr(strCode), "TSPL_INVENTORY_MOVEMENT", "Source_Doc_No", trans)
            Qry = "delete from TSPL_INVENTORY_MOVEMENT where Source_Doc_No='" + strCode + "' and Trans_Type in ('NRGP','RGP','NRGPR','RGPR')"
            clsDBFuncationality.ExecuteNonQuery(Qry, trans)

            Qry = "Update TSPL_RGP_HEAD set Status = 0,Posting_Date=null where RGP_No='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(Qry, trans)

            'Qry = "delete from TSPL_JOURNAL_DETAILS where voucher_no in (select voucher_no from TSPL_JOURNAL_MASTER where Source_Doc_No ='" + strCode + "')"
            'clsDBFuncationality.ExecuteNonQuery(Qry, trans)

            'Qry = "delete from TSPL_JOURNAL_MASTER where Source_Doc_No ='" + strCode + "'"
            'clsDBFuncationality.ExecuteNonQuery(Qry, trans)

            Dim VoucherNo As String = clsDBFuncationality.getSingleValue("select Voucher_No from TSPL_JOURNAL_MASTER where Source_Doc_No ='" + strCode + "'", trans)
            If clsCommon.myLen(VoucherNo) > 0 Then
                clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, VoucherNo, "TSPL_JOURNAL_MASTER", "Voucher_No", "TSPL_JOURNAL_DETAILS", "Voucher_No", trans)
                Qry = "delete from TSPL_JOURNAL_DETAILS where Voucher_No ='" + VoucherNo + "'"
                clsDBFuncationality.ExecuteNonQuery(Qry, trans)
                Qry = "delete from TSPL_JOURNAL_MASTER where Voucher_No ='" + VoucherNo + "'"
                clsDBFuncationality.ExecuteNonQuery(Qry, trans)
            End If

            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, clsCommon.myCstr(strCode), "TSPL_RGP_HEAD", "RGP_No", trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
End Class

Public Class clsRGPDetail
#Region "Variables"
    Public Item_MRP As Decimal = Nothing
    Public RGP_No As String = Nothing
    Public Line_No As Integer = 0
    Public Item_Code As String = Nothing
    Public Item_Desc As String = Nothing
    Public RGP_Qty As Double = 0
    Public NRGP_Qty As Double = 0
    Public Balance_Qty As Double = 0
    Public Unit_code As String = Nothing
    Public Item_Cost As Double = 0
    Public Amount As Double = 0
    Public TaxGrp As String = Nothing
    Public ItemType As String = Nothing
    Public Last_RGP_No As String = Nothing
    Public Last_RGP_Date As String = Nothing
    Public Specification As String = Nothing
    Public PO_Id As String = Nothing
    Public Against_Schedule_Code As String = Nothing
    Public PO_Sch_Qty As Double = Nothing
    Public Main_PO_Icode As String = Nothing
    Public BOM_Code As String = Nothing
    Public Approx_Cost As Double = 0
    Public arrSrItem As List(Of clsSerializeInvenotry) = Nothing
    Public arrBatchItem As List(Of clsBatchInventory) = Nothing ' prabhakar
#End Region

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal objHead As clsRGPHead, ByVal trans As SqlTransaction) As Boolean
        If (objHead.Arr IsNot Nothing AndAlso objHead.Arr.Count > 0) Then
            For Each obj As clsRGPDetail In objHead.Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "MRP", obj.Item_MRP)
                clsCommon.AddColumnsForChange(coll, "RGP_No", strDocNo)
                clsCommon.AddColumnsForChange(coll, "Line_No", obj.Line_No)
                clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code)
                clsCommon.AddColumnsForChange(coll, "Item_Desc", obj.Item_Desc)
                clsCommon.AddColumnsForChange(coll, "RGP_Qty", obj.RGP_Qty)
                clsCommon.AddColumnsForChange(coll, "Unit_code", obj.Unit_code)
                clsCommon.AddColumnsForChange(coll, "Item_Cost", obj.Item_Cost)
                clsCommon.AddColumnsForChange(coll, "Amount", obj.Amount)
                clsCommon.AddColumnsForChange(coll, "Main_PO_Icode", obj.Main_PO_Icode, True)
                clsCommon.AddColumnsForChange(coll, "BOM_Code", obj.BOM_Code)
                '' Anubhooti 06-Feb-2015 
                clsCommon.AddColumnsForChange(coll, "Approx_Cost", obj.Approx_Cost, True)

                clsCommon.AddColumnsForChange(coll, "Last_RGP_No", obj.Last_RGP_No)
                If clsCommon.myLen(obj.Last_RGP_Date) > 0 Then
                    clsCommon.AddColumnsForChange(coll, "Last_RGP_Date", clsCommon.GetPrintDate(obj.Last_RGP_Date, "dd/MM/yyyy"))
                Else
                    clsCommon.AddColumnsForChange(coll, "Last_RGP_Date", Nothing)
                End If
                clsCommon.AddColumnsForChange(coll, "Specification", obj.Specification)
                clsCommon.AddColumnsForChange(coll, "PO_Id", obj.PO_Id, True)
                clsCommon.AddColumnsForChange(coll, "Against_Schedule_Code", obj.Against_Schedule_Code, True)
                clsCommon.AddColumnsForChange(coll, "PO_Sch_Qty", obj.PO_Sch_Qty)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_RGP_DETAIL", OMInsertOrUpdate.Insert, "", trans)

                clsSerializeInvenotry.SaveData(objHead.Doc_Type, strDocNo, objHead.RGP_Date, "O", obj.Item_Code, objHead.Location, obj.Line_No, obj.arrSrItem, trans)
                clsBatchInventory.SaveData(objHead.Doc_Type, strDocNo, objHead.RGP_Date, "O", obj.Item_Code, objHead.Location, obj.Line_No, obj.Item_MRP, obj.Unit_code, obj.arrBatchItem, trans) ' Change by Prabhakar
            Next
        End If
        Return True
    End Function

    Public Shared Function GetBalanceRGPQty(ByVal strRGPCode As String, ByVal strICode As String, ByVal strCurrGRN_SRNNo As String, ByVal strUomCode As String, Optional ByVal IsJobWork As Boolean = False) As Double
        Dim qry As String = "select SUM(qty * RI) as Balance from(  "
        If IsJobWork Then
            qry += " select TSPL_RGP_JOB_WORK_DETAIL.Item_Code as ICode,TSPL_RGP_JOB_WORK_DETAIL.RGP_Qty as Qty,1 as RI from TSPL_RGP_JOB_WORK_DETAIL left outer join TSPL_RGP_HEAD on TSPL_RGP_HEAD.RGP_No=TSPL_RGP_JOB_WORK_DETAIL.RGP_No where TSPL_RGP_HEAD.Status=1 and TSPL_RGP_JOB_WORK_DETAIL.RGP_No ='" + strRGPCode + "' and TSPL_RGP_JOB_WORK_DETAIL.Item_Code='" + strICode + "' and TSPL_RGP_JOB_WORK_DETAIL.unit_code='" + strUomCode + "' "
        Else
            qry += " select TSPL_RGP_DETAIL.Item_Code as ICode,TSPL_RGP_DETAIL.RGP_Qty as Qty,1 as RI from TSPL_RGP_DETAIL left outer join TSPL_RGP_HEAD on TSPL_RGP_HEAD.RGP_No=TSPL_RGP_DETAIL.RGP_No where TSPL_RGP_HEAD.Status=1 and TSPL_RGP_DETAIL.RGP_No ='" + strRGPCode + "' and TSPL_RGP_DETAIL.Item_Code='" + strICode + "' and TSPL_RGP_DETAIL.unit_code='" + strUomCode + "' and isnull(TSPL_RGP_HEAD.Against_JobWork,0)=0 "
        End If

        qry += " union all " & _
            " select TSPL_SRN_DETAIL.Item_Code as ICode,(ISNULL(SRN_Qty,0)+ISNULL(Rejected_Qty,0)) as Qty,-1 as RI from TSPL_SRN_DETAIL left outer join TSPL_SRN_HEAD on TSPL_SRN_HEAD.SRN_No=TSPL_SRN_DETAIL.SRN_No where TSPL_SRN_DETAIL.RGP_Id='" + strRGPCode + "'   and TSPL_SRN_DETAIL.Item_Code='" + strICode + "' and TSPL_SRN_DETAIL.unit_code='" + strUomCode + "' and TSPL_SRN_DETAIL.SRN_No not in ('" + strCurrGRN_SRNNo + "')  " & _
            " union all " & _
            " select TSPL_GRN_DETAIL.Item_Code as ICode,(TSPL_GRN_DETAIL.GRN_Qty) as Qty,-1 as RI from TSPL_GRN_DETAIL left outer join TSPL_GRN_HEAD on TSPL_GRN_HEAD.GRN_No=TSPL_GRN_DETAIL.GRN_No where TSPL_GRN_DETAIL.against_RGP_no='" + strRGPCode + "'   and TSPL_GRN_DETAIL.Item_Code='" + strICode + "' and TSPL_GRN_DETAIL.unit_code='" + strUomCode + "' and TSPL_GRN_DETAIL.GRN_No not in ('" + strCurrGRN_SRNNo + "')  " & _
            " )Final "
        Return clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
    End Function
End Class

Public Class clsRGPBOMItem
#Region "Variables"
    Public RGP_No As String = Nothing
    Public Line_No As String = Nothing
    Public BOM_Code As String = Nothing
    Public BOM_desc As String = Nothing
    Public Item_Code As String = Nothing
    Public Iname As String = Nothing
    Public Unit_Code As String = Nothing
    Public RGP_Qty As Double = Nothing
    Public Balance_Qty As Double = Nothing
    Public Rate As Double = Nothing
    Public MRP As Double = Nothing
    Public Amount As Double = Nothing
    Public Specification As String = Nothing '200
    Public PO_Id As String = Nothing
    Public Against_Schedule_Code As String = Nothing
    Public Module_Id As String = Nothing
#End Region

    Public Shared Function SaveData(ByVal strCode As String, ByVal arr As List(Of clsRGPBOMItem), ByVal trans As SqlTransaction) As Boolean
        Try
            Dim isSaved As Boolean = True

            Dim qry As String = "delete from TSPL_RGP_JOB_WORK_DETAIL where RGP_No='" + strCode + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            For Each objtr As clsRGPBOMItem In arr
                Dim coll As New Hashtable()

                clsCommon.AddColumnsForChange(coll, "rgp_no", strCode, True)
                clsCommon.AddColumnsForChange(coll, "Line_No", objtr.Line_No)
                clsCommon.AddColumnsForChange(coll, "BOM_Code", objtr.BOM_Code)
                clsCommon.AddColumnsForChange(coll, "Item_Code", objtr.Item_Code, True)
                clsCommon.AddColumnsForChange(coll, "Unit_Code", objtr.Unit_Code, True)
                clsCommon.AddColumnsForChange(coll, "RGP_Qty", objtr.RGP_Qty)
                clsCommon.AddColumnsForChange(coll, "Balance_Qty", objtr.Balance_Qty)
                clsCommon.AddColumnsForChange(coll, "Rate", objtr.Rate)
                clsCommon.AddColumnsForChange(coll, "MRP", objtr.MRP)
                clsCommon.AddColumnsForChange(coll, "Amount", objtr.Amount)
                clsCommon.AddColumnsForChange(coll, "Specification", objtr.Specification)
                clsCommon.AddColumnsForChange(coll, "PO_Id", objtr.PO_Id, True)
                clsCommon.AddColumnsForChange(coll, "Against_Schedule_Code", objtr.Against_Schedule_Code, True)
                clsCommon.AddColumnsForChange(coll, "Module_Id", objtr.Module_Id)

                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_RGP_JOB_WORK_DETAIL", OMInsertOrUpdate.Insert, "", trans)
            Next

            Return isSaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
End Class