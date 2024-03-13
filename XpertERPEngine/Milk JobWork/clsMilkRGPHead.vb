'===========BM00000007790,Rohit========================
Imports common
Imports System.Data.SqlClient
Public Class clsMilkRGPHead

#Region "Variables"
    Public Milk_Transfer_In As String = Nothing
    Public Against_BOM As Integer = Nothing
    Public Against_As_It_Is As Integer = Nothing
    Public GRNo As String = Nothing
    Public GR_Date As Date?
    Public Road_Permit_No As String = Nothing
    Public RoadPermit_Date As Date?
    Public Arr_BOM As List(Of clsRGPBOMItem) = Nothing
    Public Against_Schedule_Code As String = Nothing
    Public PO_Id As String = Nothing
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
    Public Arr As List(Of clsMilkRGPDetail) = Nothing
    Public TaxGrp As String = Nothing
    Public ItemType As String = Nothing
    Public Billing As String = Nothing
    Public Is_Non_Inventory As Integer = 0
    Public Mode_Of_Transport As String = Nothing
    Public Cash_Memo_Detail As String = Nothing
    Public CostCentre As String = Nothing
    Public CostCentreDesc As String = Nothing
    Public Against_Customer As Integer = 0

    Public Form_ID As String = ""
    Public arrCustomFields As List(Of clsCustomFieldValues) = Nothing
    Public ArrQC As List(Of clsMilkRGPIssueQCDetail) = Nothing

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
#End Region


    Public Function SaveData(ByVal obj As clsMilkRGPHead, ByVal isNewEntry As Boolean, Optional isPostdata As Boolean = False) As Boolean

        Dim isSaved As Boolean = True
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()

        Try
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "Milk Jobwork", "Milk JobWork RGP", obj.Location, obj.RGP_Date, trans)

            Dim qry As String = "delete from TSPL_Milk_RGP_DETAIL where RGP_No='" + obj.RGP_No + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Dim strDocNo As String = ""
            If isNewEntry Then
                If clsCommon.CompairString(obj.Doc_Type, "RGP") = CompairStringResult.Equal Then
                    obj.RGP_No = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(obj.RGP_Date), clsDocType.MilkRGP, "", obj.Location)
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
            clsCommon.AddColumnsForChange(coll, "RGP_Date", clsCommon.GetPrintDate(obj.RGP_Date, "dd/MM/yyyy"))
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
            clsCommon.AddColumnsForChange(coll, "Milk_Transfer_In", obj.Milk_Transfer_In, True)

            '' Anubhooti 09-Oct-2014 BM00000003663
            clsCommon.AddColumnsForChange(coll, "Is_Rejected", obj.Is_Rejected)
            '  clsCommon.AddColumnsForChange(coll, "SRN_No", obj.SRN_No, True)
            ''
            '' Anubhooti 10-Dec-2014 BM00000003662
            clsCommon.AddColumnsForChange(coll, "Item_Conversion_Type", obj.Item_Conversion_Type, True)
            ''
            If obj.GPDate.HasValue Then
                clsCommon.AddColumnsForChange(coll, "GPDate", clsCommon.GetPrintDate(obj.GPDate, "dd/MMM/yyyy"))
            Else
                clsCommon.AddColumnsForChange(coll, "GPDate", Nothing, True)
            End If

            clsCommon.AddColumnsForChange(coll, "Road_Permit_No", obj.Road_Permit_No)
            clsCommon.AddColumnsForChange(coll, "GRNo", obj.GRNo)
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

            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "RGP_No", obj.RGP_No)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Milk_RGP_Head", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Milk_RGP_Head", OMInsertOrUpdate.Update, "TSPL_Milk_RGP_Head.RGP_No='" + obj.RGP_No + "'", trans)
            End If
            isSaved = isSaved AndAlso clsMilkRGPDetail.SaveData(obj.RGP_No, Arr, trans, obj.RGP_Date)
            'isSaved = isSaved AndAlso clsRGPBOMItem.SaveData(obj.RGP_No, obj.Arr_BOM, trans)
            isSaved = isSaved AndAlso clsCustomFieldValues.SaveData(obj.Form_ID, obj.RGP_No, obj.arrCustomFields, trans)
            isSaved = isSaved AndAlso clsMilkRGPIssueQCDetail.SaveData(obj.RGP_No, obj.ArrQC, trans)
            If isPostdata Then
                clsMilkRGPHead.PostData(obj.RGP_No, trans)
            End If
            If isSaved Then
                trans.Commit()
            End If
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function

    Public Shared Function GetLocationTypeEnum() As DataTable
        Dim qry As String = "select '1' as Code,'Main Location' as Name union all select '2' as Code,'Sub-Location' as Name union all select '3' as Code,'Section-wise Tanker' as Name"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

        Return dt
    End Function

    Public Shared Function GetRGPTypeItemFInder(ByVal RGPType As String, ByVal Vendor_Code As String, ByVal GRN_No As String, ByVal Item_Code As String, ByVal Item_Type As String) As clsMilkRGPDetail
        Dim obj As New clsMilkRGPDetail()
        Dim whrcls As String = ""
        Dim qry As String = ""

        If clsCommon.CompairString(RGPType, "AR") = CompairStringResult.Equal Then 'against rgp
            whrcls = " and rgp_no in (select rgp_no from TSPL_Milk_RGP_Head where doc_type='RGP' and status='1' and Against_JobWork='1' and Against_BOM='0' and Against_As_It_Is='0' "
        ElseIf clsCommon.CompairString(RGPType, "AI") = CompairStringResult.Equal Then 'as it is
            whrcls = " and rgp_no in (select rgp_no from TSPL_Milk_RGP_Head where doc_type='RGP' and status='1' and Against_JobWork='1' and Against_BOM='0' and Against_As_It_Is='1' "
        End If
        If clsCommon.myLen(Vendor_Code) > 0 Then
            whrcls += " and vendor_code='" + Vendor_Code + "' "
        End If
        If clsCommon.myLen(whrcls) > 0 Then
            whrcls += ")"
        End If

        If clsCommon.CompairString(RGPType, "AB") = CompairStringResult.Equal Then
            qry = "select TSPL_PP_BOM_HEAD.prod_item_code as Code,tspl_item_master.item_desc as Description,TSPL_PP_BOM_HEAD.prod_item_unit_code as Unit,0 as Pending_Qty,rgp.rgp_no as [RGP No] from TSPL_PP_BOM_HEAD left outer join tspl_item_master on tspl_item_master.item_code=tspl_pp_bom_head.prod_item_code left outer join "
            qry += " (select tspl_pp_bom_item_detail.bom_code,TSPL_Milk_RGP_DETAIL.rgp_no from tspl_pp_bom_item_detail left outer join TSPL_Milk_RGP_DETAIL on TSPL_Milk_RGP_DETAIL.item_code=tspl_pp_bom_item_detail.item_code and TSPL_Milk_RGP_DETAIL.unit_code=tspl_pp_bom_item_detail.unit_code left outer join TSPL_Milk_RGP_Head on TSPL_Milk_RGP_Head.rgp_no=TSPL_Milk_RGP_DETAIL.rgp_no where TSPL_Milk_RGP_Head.doc_type='RGP' and TSPL_Milk_RGP_Head.status='1' and TSPL_Milk_RGP_Head.Against_JobWork='1' and TSPL_Milk_RGP_Head.Against_BOM='1' and TSPL_Milk_RGP_Head.Against_As_It_Is='0' and TSPL_Milk_RGP_Head.vendor_code='" + Vendor_Code + "')rgp on rgp.bom_code=tspl_pp_bom_head.bom_code "
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

    Public Shared Function GetRGPTypeItemBalance(ByVal RGPType As String, ByVal Qty As Double, ByVal Vendor_Code As String, ByVal GRN_No As String, ByVal Item_Code As String, ByVal Unit_Code As String, Optional ByVal SRN_No As String = Nothing, Optional ByVal Is_From_SRN As Boolean = False) As String
        Dim str As String = ""

        If clsCommon.CompairString(RGPType, "AB") = CompairStringResult.Equal Then
            Dim qry As String = ""
            Dim whrCls As String = ""
            If Is_From_SRN Then
                'If clsCommon.myLen(SRN_No) > 0 Then
                '    whrCls = " and isnull(srn_no,'') <> '" + SRN_No + "' "
                'End If
                qry = "select final.bom_code,final.prod_item_code,final.prod_item_unit_code,final.item_code,final.unit_code,(final.unit_qty * " + clsCommon.myCstr(Qty) + ") as Qty,final.bal_qty from (select TSPL_PP_BOM_HEAD.BOM_CODE,TSPL_PP_BOM_HEAD.PROD_ITEM_CODE,TSPL_PP_BOM_HEAD.PROD_ITEM_UNIT_CODE,TSPL_PP_BOM_ITEM_DETAIL.ITEM_CODE,TSPL_PP_BOM_ITEM_DETAIL.UNIT_CODE,(isnull(TSPL_PP_BOM_ITEM_DETAIL.QUANTITY,0)/isnull(TSPL_PP_BOM_HEAD.PROD_QUANTITY,0))+(((isnull(TSPL_PP_BOM_ITEM_DETAIL.QUANTITY,0)/isnull(TSPL_PP_BOM_HEAD.PROD_QUANTITY,0))*isnull(TSPL_PP_BOM_ITEM_DETAIL.Rejection_Pers,0))/100) as unit_qty " & _
                                " ,(rgp.Qty*TSPL_ITEM_UOM_DETAIL.Conversion_Factor)/itemuom.Conversion_Factor as bal_qty from TSPL_PP_BOM_HEAD " & _
                                " left outer join TSPL_PP_BOM_ITEM_DETAIL on TSPL_PP_BOM_ITEM_DETAIL.BOM_CODE=TSPL_PP_BOM_HEAD.BOM_CODE " & _
                                " left outer join (select ax.Item_Code,ax.Unit_Code,sum(ax.Qty) as Qty from ( " & _
                                " select Item_Code,Unit_Code,(case when InOut='I' then -1*isnull(qty,0) else isnull(qty,0) end) as Qty from TSPL_RGP_BOM_DETAIL where vendor_code='" + Vendor_Code + "' " + whrCls + " )ax group by ax.Item_Code,ax.Unit_Code)RGP on rgp.Item_Code=TSPL_PP_BOM_ITEM_DETAIL.ITEM_CODE " & _
                                " left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=RGP.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code=rgp.Unit_Code " & _
                                " left outer join TSPL_ITEM_UOM_DETAIL itemuom on itemuom.Item_Code=RGP.Item_Code and itemuom.UOM_Code=TSPL_PP_BOM_ITEM_DETAIL.UNIT_CODE where TSPL_PP_BOM_HEAD.PROD_ITEM_CODE='" + Item_Code + "' and TSPL_PP_BOM_HEAD.PROD_ITEM_UNIT_CODE='" + Unit_Code + "' and tspl_pp_bom_head.vendor_code='" + Vendor_Code + "')final "
            Else
                If clsCommon.myLen(GRN_No) > 0 Then
                    whrCls = " and isnull(grn_no,'') <> '" + GRN_No + "' "
                End If
                qry = "select final.bom_code,final.prod_item_code,final.prod_item_unit_code,final.item_code,final.unit_code,(final.unit_qty * " + clsCommon.myCstr(Qty) + ") as Qty,final.bal_qty from (select TSPL_PP_BOM_HEAD.BOM_CODE,TSPL_PP_BOM_HEAD.PROD_ITEM_CODE,TSPL_PP_BOM_HEAD.PROD_ITEM_UNIT_CODE,TSPL_PP_BOM_ITEM_DETAIL.ITEM_CODE,TSPL_PP_BOM_ITEM_DETAIL.UNIT_CODE,(isnull(TSPL_PP_BOM_ITEM_DETAIL.QUANTITY,0)/isnull(TSPL_PP_BOM_HEAD.PROD_QUANTITY,0))+(((isnull(TSPL_PP_BOM_ITEM_DETAIL.QUANTITY,0)/isnull(TSPL_PP_BOM_HEAD.PROD_QUANTITY,0))*isnull(TSPL_PP_BOM_ITEM_DETAIL.Rejection_Pers,0))/100) as unit_qty " & _
                                " ,(rgp.Qty*TSPL_ITEM_UOM_DETAIL.Conversion_Factor)/itemuom.Conversion_Factor as bal_qty from TSPL_PP_BOM_HEAD " & _
                                " left outer join TSPL_PP_BOM_ITEM_DETAIL on TSPL_PP_BOM_ITEM_DETAIL.BOM_CODE=TSPL_PP_BOM_HEAD.BOM_CODE " & _
                                " left outer join (select ax.Item_Code,ax.Unit_Code,sum(ax.Qty) as Qty from ( " & _
                                " select Item_Code,Unit_Code,(case when InOut='I' then -1*isnull(qty,0) else isnull(qty,0) end) as Qty from TSPL_RGP_BOM_DETAIL where vendor_code='" + Vendor_Code + "' " + whrCls + " )ax group by ax.Item_Code,ax.Unit_Code)RGP on rgp.Item_Code=TSPL_PP_BOM_ITEM_DETAIL.ITEM_CODE " & _
                                " left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=RGP.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code=rgp.Unit_Code " & _
                                " left outer join TSPL_ITEM_UOM_DETAIL itemuom on itemuom.Item_Code=RGP.Item_Code and itemuom.UOM_Code=TSPL_PP_BOM_ITEM_DETAIL.UNIT_CODE where TSPL_PP_BOM_HEAD.PROD_ITEM_CODE='" + Item_Code + "' and TSPL_PP_BOM_HEAD.PROD_ITEM_UNIT_CODE='" + Unit_Code + "' and tspl_pp_bom_head.vendor_code='" + Vendor_Code + "')final "
            End If

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    If clsCommon.myCdbl(dr("Qty")) > clsCommon.myCdbl(dr("bal_qty")) Then 'if balance is not exist then
                        str += " [" + clsItemMaster.GetItemName(clsCommon.myCstr(dr("item_code")), Nothing) + " has RGP balance " + clsCommon.myCstr(dr("bal_qty")) + " and required qty is " + clsCommon.myCstr(dr("qty")) + "]."
                    End If
                Next
            End If
        End If

        If clsCommon.myLen(str) > 0 Then
            str = "Error msg. " + Environment.NewLine + "Item(s) " + str
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
                                " select Item_Code,Unit_Code,(case when InOut='I' then -1*isnull(qty,0) else isnull(qty,0) end) as Qty from TSPL_RGP_BOM_DETAIL where isnull(grn_no,'') <> '" + GRN_No + "' and vendor_code='" + Vendor_Code + "' union all select Item_Code,Unit_Code,(case when InOut='I' then -1*isnull(qty,0) else isnull(qty,0) end) as Qty from TSPL_RGP_BOM_DETAIL where isnull(grn_no,'')='' and vendor_code='" + Vendor_Code + "' )ax group by ax.Item_Code,ax.Unit_Code)RGP on rgp.Item_Code=TSPL_PP_BOM_ITEM_DETAIL.ITEM_CODE " & _
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
            Dim qry As String = "select RGP_No,Vendor_Code,Vendor_Name from TSPL_Milk_RGP_Head where RGP_No in (" + clsCommon.GetMulcallString(Arr) + ") and Vendor_Code not in ('" + strVendorCode + "')"
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
            Dim qry As String = "select RGP_No,Vendor_Code,Vendor_Name from TSPL_Milk_RGP_Head where RGP_No in (" + clsCommon.GetMulcallString(Arr) + ") "
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

    Public Shared Function UpdateAfterPosting(ByVal obj As clsMilkRGPHead, ByVal trans As SqlTransaction) As Boolean
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

                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Milk_RGP_Head", OMInsertOrUpdate.Update, "TSPL_Milk_RGP_Head.RGP_No='" + obj.RGP_No + "'", trans)
            End If
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Shared Function GetData(ByVal strDocumentNo As String, ByVal NavType As NavigatorType) As clsMilkRGPHead
        Return GetData(strDocumentNo, NavType, Nothing)
    End Function
    Public Shared Function GetData(ByVal strPONo As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsMilkRGPHead
        Dim obj As clsMilkRGPHead = Nothing
        Dim qry As String = "SELECT TSPL_Milk_RGP_Head.Milk_Transfer_In,TSPL_Milk_RGP_Head.Against_As_It_Is,TSPL_Milk_RGP_Head.Against_BOM,TSPL_Milk_RGP_Head.Road_Permit_No,TSPL_Milk_RGP_Head.RoadPermit_Date,TSPL_Milk_RGP_Head.GRNo,TSPL_Milk_RGP_Head.GR_Date,TSPL_Milk_RGP_Head.po_id,TSPL_Milk_RGP_Head.Against_Schedule_Code,TSPL_Milk_RGP_Head.RGP_No,TSPL_Milk_RGP_Head.Billing,TSPL_Milk_RGP_Head.RGP_Date,TSPL_Milk_RGP_Head.Vendor_Code,TSPL_Milk_RGP_Head.Vendor_Name,TSPL_Milk_RGP_Head.Status,TSPL_Milk_RGP_Head.On_Hold,TSPL_Milk_RGP_Head.Reason,TSPL_Milk_RGP_Head.Remarks,TSPL_Milk_RGP_Head.Reason,TSPL_Milk_RGP_Head.Posting_Date,TSPL_Milk_RGP_Head.VehicleNo,TSPL_Milk_RGP_Head.GPNo,TSPL_Milk_RGP_Head.GPDate,TSPL_Milk_RGP_Head.Doc_Type,TSPL_Milk_RGP_Head.Document_Amount,TSPL_Milk_RGP_Head.Location,TSPL_Milk_RGP_Head.Mode_Of_Transport,TSPL_Milk_RGP_Head.Cash_Memo_Detail,TSPL_LOCATION_MASTER.Location_Desc as LocationName,TSPL_Milk_RGP_Head.Delivered_By,TSPL_EMPLOYEE_MASTER.Emp_Name as DeliveredBYName,TSPL_Milk_RGP_Head.Department,TSPL_Milk_RGP_Head.Against_Sale,Against_JobWork, TSPL_Milk_RGP_Head.Is_Non_Inventory,CostCentre,CostCentreDesc,Against_Customer,TSPL_Milk_RGP_Head.IsThirdParty,TSPL_Milk_RGP_Head.SRN_Location_Code,TSPL_Milk_RGP_Head.InvoiceNo,TSPL_Milk_RGP_Head.PartyName,TSPL_Milk_RGP_Head.PartyAddress,TSPL_Milk_RGP_Head.DispatchDate, TSPL_Milk_RGP_Head.Against_NRGP,TSPL_Milk_RGP_Head.Is_Rejected,TSPL_Milk_RGP_Head.Item_Conversion_Type  FROM TSPL_Milk_RGP_Head left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_Milk_RGP_Head.Location left outer join TSPL_EMPLOYEE_MASTER on TSPL_EMPLOYEE_MASTER.EMP_CODE=TSPL_Milk_RGP_Head.Delivered_By where 2=2"
        Dim whrCls As String = ""
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            whrCls = " AND Location in (" + objCommonVar.strCurrUserLocations + ")"
        End If
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_Milk_RGP_Head.RGP_No = (select MIN(RGP_No) from TSPL_Milk_RGP_Head WHERE 1=1 " + whrCls + ")"
            Case NavigatorType.Last
                qry += " and TSPL_Milk_RGP_Head.RGP_No = (select Max(RGP_No) from TSPL_Milk_RGP_Head WHERE 1=1 " + whrCls + ")"
            Case NavigatorType.Current
                qry += " and TSPL_Milk_RGP_Head.RGP_No = '" + strPONo + "'"
            Case NavigatorType.Next
                qry += " and TSPL_Milk_RGP_Head.RGP_No = (select Min(RGP_No) from TSPL_Milk_RGP_Head where RGP_No>'" + strPONo + "' " + whrCls + ")"
            Case NavigatorType.Previous
                qry += " and TSPL_Milk_RGP_Head.RGP_No = (select Max(RGP_No) from TSPL_Milk_RGP_Head where RGP_No<'" + strPONo + "' " + whrCls + ")"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsMilkRGPHead()

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

            obj.PO_Id = clsCommon.myCstr(dt.Rows(0)("po_id"))
            obj.Against_Schedule_Code = clsCommon.myCstr(dt.Rows(0)("Against_Schedule_Code"))
            obj.Milk_Transfer_In = clsCommon.myCstr(dt.Rows(0)("Milk_Transfer_In"))
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
            'obj.SRN_No = clsCommon.myCstr(dt.Rows(0)("SRN_No"))
            ''
            '' Anubhooti 10-Dec-2014 BM00000003662
            obj.Item_Conversion_Type = clsCommon.myCstr(dt.Rows(0)("Item_Conversion_Type"))
            ''
            'qry = "SELECT TSPL_Milk_RGP_DETAIL.MRP,TSPL_Milk_RGP_DETAIL.RGP_No,TSPL_Milk_RGP_DETAIL.Line_No,TSPL_Milk_RGP_DETAIL.Item_Code,TSPL_Milk_RGP_DETAIL.Item_Desc,TSPL_Milk_RGP_DETAIL.RGP_Qty, (Select ISNULL(RGP_Qty,0) from TSPL_Milk_RGP_DETAIL as dtl Where dtl.RGP_No=TSPL_Milk_RGP_Head.Against_NRGP AND dtl.Item_Code=TSPL_Milk_RGP_DETAIL.Item_Code) as NRGP_Qty, TSPL_Milk_RGP_DETAIL.Unit_code,TSPL_Milk_RGP_DETAIL.Item_Cost,TSPL_Milk_RGP_DETAIL.Amount, TSPL_Milk_RGP_DETAIL.Last_RGP_No, TSPL_Milk_RGP_DETAIL.Last_RGP_Date, TSPL_Milk_RGP_DETAIL.Specification FROM TSPL_Milk_RGP_DETAIL LEFT OUTER JOIN TSPL_Milk_RGP_Head ON TSPL_Milk_RGP_Head.RGP_No=TSPL_Milk_RGP_DETAIL.RGP_No where TSPL_Milk_RGP_DETAIL.RGP_No='" & obj.RGP_No & "'  ORDER BY TSPL_Milk_RGP_DETAIL.Line_No"
            If clsCommon.myLen(obj.Against_NRGP) <= 0 Then
                qry = "Select FAT_Pers,Bulk_Milk_Srn_Code, SNF_Pers,FAT_Cost, SNF_Cost,FAT_KG,SNF_KG,FAT_Price,SNF_Price,To_Location_Code,Tanker_No,To_SubLocation_YN,XXX.BOM_Code,XXX.Main_PO_Icode,XXX.PO_Sch_Qty,XXX.po_id,XXX.Against_Schedule_Code,XXX.MRP, XXX.RGP_No, XXX.Line_No, XXX.Item_Code, XXX.Item_Desc, XXX.RGP_Qty, XXX.NRGP_Qty, XXX.Unit_code, XXX.Item_Cost,XXX.Approx_Cost, XXX.Amount, XXX.Last_RGP_No, XXX.Last_RGP_Date, XXX.Specification, RGP_Qty-(select ISNULL(SUM(RGP_Qty),0) from TSPL_Milk_RGP_Head LEFT OUTER JOIN TSPL_Milk_RGP_DETAIL ON TSPL_Milk_RGP_DETAIL.RGP_No=TSPL_Milk_RGP_Head.RGP_No WHERE TSPL_Milk_RGP_Head.Against_NRGP='" & obj.RGP_No & "' AND TSPL_Milk_RGP_DETAIL.Item_Code=XXX.Item_Code) as Balance_Qty from ("
            Else
                qry = "Select FAT_Pers,Bulk_Milk_Srn_Code, SNF_Pers,FAT_Cost, SNF_Cost,FAT_KG,SNF_KG,FAT_Price,SNF_Price,To_Location_Code,Tanker_No,To_SubLocation_YN,XXX.BOM_Code,XXX.Main_PO_Icode,XXX.PO_Sch_Qty,XXX.po_id,XXX.Against_Schedule_Code,XXX.MRP, XXX.RGP_No, XXX.Line_No, XXX.Item_Code, XXX.Item_Desc, XXX.RGP_Qty, XXX.NRGP_Qty, XXX.Unit_code, XXX.Item_Cost,XXX.Approx_Cost, XXX.Amount, XXX.Last_RGP_No, XXX.Last_RGP_Date, XXX.Specification, NRGP_Qty-(select ISNULL(SUM(RGP_Qty),0) from TSPL_Milk_RGP_Head LEFT OUTER JOIN TSPL_Milk_RGP_DETAIL ON TSPL_Milk_RGP_DETAIL.RGP_No=TSPL_Milk_RGP_Head.RGP_No WHERE TSPL_Milk_RGP_Head.Against_NRGP='" & obj.Against_NRGP & "' AND TSPL_Milk_RGP_DETAIL.Item_Code=XXX.Item_Code AND TSPL_Milk_RGP_Head.RGP_No<>'" & obj.RGP_No & "') as Balance_Qty from ("
            End If
            qry += " SELECT Tanker_No,TSPL_Milk_RGP_DETAIL.BOM_Code,Bulk_Milk_Srn_Code,FAT_Pers, SNF_Pers,FAT_Cost, SNF_Cost,FAT_KG,SNF_KG,FAT_Price,SNF_Price,To_Location_Code,To_SubLocation_YN,TSPL_Milk_RGP_DETAIL.Main_PO_Icode,TSPL_Milk_RGP_DETAIL.PO_Sch_Qty,TSPL_Milk_RGP_DETAIL.po_id,TSPL_Milk_RGP_DETAIL.Against_Schedule_Code,TSPL_Milk_RGP_DETAIL.MRP,TSPL_Milk_RGP_DETAIL.RGP_No,TSPL_Milk_RGP_DETAIL.Line_No,TSPL_Milk_RGP_DETAIL.Item_Code,TSPL_Milk_RGP_DETAIL.Item_Desc,TSPL_Milk_RGP_DETAIL.RGP_Qty," & _
        " (Select ISNULL(RGP_Qty,0) from TSPL_Milk_RGP_DETAIL as dtl Where dtl.RGP_No=TSPL_Milk_RGP_Head.Against_NRGP AND dtl.Item_Code=TSPL_Milk_RGP_DETAIL.Item_Code) as NRGP_Qty," & _
        " TSPL_Milk_RGP_DETAIL.Unit_code,TSPL_Milk_RGP_DETAIL.Item_Cost,isnull(TSPL_Milk_RGP_DETAIL.Approx_Cost,0) As Approx_Cost,TSPL_Milk_RGP_DETAIL.Amount, TSPL_Milk_RGP_DETAIL.Last_RGP_No, TSPL_Milk_RGP_DETAIL.Last_RGP_Date," & _
        " TSPL_Milk_RGP_DETAIL.Specification, TSPL_Milk_RGP_Head.Against_NRGP FROM TSPL_Milk_RGP_DETAIL" & _
        " LEFT OUTER JOIN TSPL_Milk_RGP_Head ON TSPL_Milk_RGP_Head.RGP_No=TSPL_Milk_RGP_DETAIL.RGP_No where TSPL_Milk_RGP_DETAIL.RGP_No='" & obj.RGP_No & "'" & _
        " ) XXX ORDER BY XXX.Line_No"
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj.Arr = New List(Of clsMilkRGPDetail)
                Dim objTr As clsMilkRGPDetail
                For Each dr As DataRow In dt.Rows
                    objTr = New clsMilkRGPDetail
                    objTr.Item_MRP = clsCommon.myCdbl(dr("MRP"))
                    objTr.RGP_No = clsCommon.myCstr(dr("RGP_No"))
                    objTr.Line_No = clsCommon.myCstr(dr("Line_No"))
                    objTr.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                    objTr.Item_Desc = clsCommon.myCstr(dr("Item_Desc"))
                    objTr.HSN_Code = clsItemMaster.GetItemHSNCode(objTr.Item_Code, trans)
                    objTr.RGP_Qty = clsCommon.myCdbl(dr("RGP_Qty"))
                    objTr.NRGP_Qty = clsCommon.myCdbl(dr("NRGP_Qty"))
                    objTr.Balance_Qty = clsCommon.myCdbl(dr("Balance_Qty"))
                    objTr.Unit_code = clsCommon.myCstr(dr("Unit_code"))
                    objTr.Item_Cost = clsCommon.myCdbl(dr("Item_Cost"))
                    objTr.Last_RGP_No = clsCommon.myCstr(dr("Last_RGP_No"))
                    objTr.TanKer_No = clsCommon.myCstr(dr("Tanker_No"))
                    objTr.Last_RGP_Date = clsCommon.myCstr(dr("Last_RGP_Date"))
                    objTr.Amount = clsCommon.myCdbl(dr("Amount"))
                    objTr.FAT_pers = clsCommon.myCdbl(dr("Fat_Pers"))
                    objTr.FAT_Cost = clsCommon.myCdbl(dr("Fat_Cost"))
                    objTr.SNF_Cost = clsCommon.myCdbl(dr("Snf_Cost"))
                    objTr.FAT_KG = clsCommon.myCdbl(dr("Fat_KG"))
                    objTr.FAT_Price = clsCommon.myCdbl(dr("Fat_Price"))

                    objTr.SNF_Pers = clsCommon.myCdbl(dr("SNF_Pers"))
                    objTr.SNF_KG = clsCommon.myCdbl(dr("SNF_KG"))
                    objTr.SNF_Price = clsCommon.myCdbl(dr("SNF_Price"))
                    objTr.Location_Code = clsCommon.myCstr(dr("To_Location_Code"))
                    objTr.Location_Type = clsCommon.myCstr(dr("To_SubLocation_YN"))
                    objTr.Specification = clsCommon.myCstr(dr("Specification"))
                    objTr.PO_Id = clsCommon.myCstr(dr("po_id"))
                    objTr.Against_Schedule_Code = clsCommon.myCstr(dr("Against_Schedule_Code"))
                    objTr.PO_Sch_Qty = clsCommon.myCdbl(dr("PO_Sch_Qty"))
                    objTr.Main_PO_Icode = clsCommon.myCstr(dr("Main_PO_Icode"))
                    objTr.BOM_Code = clsCommon.myCstr(dr("BOM_Code"))
                    objTr.Bulk_Milk_Srn_No = clsCommon.myCstr(dr("Bulk_Milk_Srn_Code"))
                    '' Anubhooti 06-Feb-2015 (Fetch Approx_Cost Also from query-newly added)
                    objTr.Approx_Cost = clsCommon.myCdbl(dr("Approx_Cost"))
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
            obj.ArrQC = New List(Of clsMilkRGPIssueQCDetail)

            qry = "select * from TSPL_MR_ISSUE_QC_DETAIL where issue_code='" + obj.RGP_No + "' order by sno"
            dt1 = New DataTable()
            dt1 = clsDBFuncationality.GetDataTable(qry, trans)

            If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
                For Each dr As DataRow In dt1.Rows
                    Dim objtr As New clsMilkRGPIssueQCDetail()

                    objtr.sno = CInt(dr("sno"))
                    objtr.frm_loc_code = clsCommon.myCstr(dr("From_Location_Code"))
                    objtr.frm_loc_desc = clsLocation.GetName(objtr.frm_loc_code, trans)
                    objtr.to_loc_code = clsCommon.myCstr(dr("To_Location_Code"))
                    objtr.to_loc_desc = clsLocation.GetName(objtr.to_loc_code, trans)
                    objtr.itemcode = clsCommon.myCstr(dr("Item_Code"))
                    objtr.itemname = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select item_desc from tspl_item_master where item_code='" + objtr.itemcode + "'", trans))
                    objtr.param_code = clsCommon.myCstr(dr("Parameter_Code"))
                    objtr.param_desc = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select description from TSPL_PARAMETER_MASTER where code='" + objtr.param_code + "'", trans))
                    objtr.param_type = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select type from TSPL_PARAMETER_MASTER where code='" + objtr.param_code + "'", trans))
                    objtr.param_nature = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select (Case when Nature='A' then 'Alphanumeric' else case when Nature='B' then 'Boolean' else case when Nature='R' then 'Range' end end end) as Nature from TSPL_PARAMETER_MASTER where code='" + objtr.param_code + "'", trans))
                    objtr.lrange = clsCommon.myCdbl(dr("Lower_range"))
                    objtr.urange = clsCommon.myCdbl(dr("Upper_range"))
                    objtr.status_grid = clsCommon.myCstr(dr("Status"))
                    objtr.value1 = clsCommon.myCstr(dr("Value1"))
                    objtr.value2 = clsCommon.myCstr(dr("Value2"))
                    objtr.remarks = clsCommon.myCstr(dr("Remarks"))
                    objtr.QCRange = clsCommon.myCdbl(dr("QC_Range"))
                    objtr.QCStatus = clsCommon.myCstr(dr("QC_Status"))
                    objtr.QCValue = clsCommon.myCstr(dr("QC_Value"))

                    obj.ArrQC.Add(objtr)
                Next
            End If

        End If

        Return obj
    End Function
    '' Anubhooti 10-Dec-2014
    Public Shared Function GetItemDataForSRN(ByVal strPONo As String, ByVal strItemCode As String, ByVal NavType As NavigatorType) As clsMilkRGPHead
        Dim obj As clsMilkRGPHead = Nothing
        Dim qry As String = "SELECT TSPL_Milk_RGP_Head.RGP_No,TSPL_Milk_RGP_Head.Billing,TSPL_Milk_RGP_Head.RGP_Date,TSPL_Milk_RGP_Head.Vendor_Code,TSPL_Milk_RGP_Head.Vendor_Name,TSPL_Milk_RGP_Head.Status,TSPL_Milk_RGP_Head.On_Hold,TSPL_Milk_RGP_Head.Reason,TSPL_Milk_RGP_Head.Remarks,TSPL_Milk_RGP_Head.Reason,TSPL_Milk_RGP_Head.Posting_Date,TSPL_Milk_RGP_Head.VehicleNo,TSPL_Milk_RGP_Head.GPNo,TSPL_Milk_RGP_Head.GPDate,TSPL_Milk_RGP_Head.Doc_Type,TSPL_Milk_RGP_Head.Document_Amount,TSPL_Milk_RGP_Head.Location,TSPL_Milk_RGP_Head.Mode_Of_Transport,TSPL_Milk_RGP_Head.Cash_Memo_Detail,TSPL_LOCATION_MASTER.Location_Desc as LocationName,TSPL_Milk_RGP_Head.Delivered_By,TSPL_EMPLOYEE_MASTER.Emp_Name as DeliveredBYName,TSPL_Milk_RGP_Head.Department,TSPL_Milk_RGP_Head.Against_Sale,Against_JobWork, TSPL_Milk_RGP_Head.Is_Non_Inventory,CostCentre,CostCentreDesc,Against_Customer,TSPL_Milk_RGP_Head.IsThirdParty,TSPL_Milk_RGP_Head.SRN_Location_Code,TSPL_Milk_RGP_Head.InvoiceNo,TSPL_Milk_RGP_Head.PartyName,TSPL_Milk_RGP_Head.PartyAddress,TSPL_Milk_RGP_Head.DispatchDate, TSPL_Milk_RGP_Head.Against_NRGP,TSPL_Milk_RGP_Head.Is_Rejected,TSPL_Milk_RGP_Head.SRN_No,TSPL_Milk_RGP_Head.Item_Conversion_Type  FROM TSPL_Milk_RGP_Head left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_Milk_RGP_Head.Location left outer join TSPL_EMPLOYEE_MASTER on TSPL_EMPLOYEE_MASTER.EMP_CODE=TSPL_Milk_RGP_Head.Delivered_By where 2=2"
        Dim whrCls As String = ""
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            whrCls = " AND Location in (" + objCommonVar.strCurrUserLocations + ")"
        End If
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_Milk_RGP_Head.RGP_No = (select MIN(RGP_No) from TSPL_Milk_RGP_Head WHERE 1=1 " + whrCls + ")"
            Case NavigatorType.Last
                qry += " and TSPL_Milk_RGP_Head.RGP_No = (select Max(RGP_No) from TSPL_Milk_RGP_Head WHERE 1=1 " + whrCls + ")"
            Case NavigatorType.Current
                qry += " and TSPL_Milk_RGP_Head.RGP_No = '" + strPONo + "'"
            Case NavigatorType.Next
                qry += " and TSPL_Milk_RGP_Head.RGP_No = (select Min(RGP_No) from TSPL_Milk_RGP_Head where RGP_No>'" + strPONo + "' " + whrCls + ")"
            Case NavigatorType.Previous
                qry += " and TSPL_Milk_RGP_Head.RGP_No = (select Max(RGP_No) from TSPL_Milk_RGP_Head where RGP_No<'" + strPONo + "' " + whrCls + ")"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, Nothing)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsMilkRGPHead()

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
            'obj.SRN_No = clsCommon.myCstr(dt.Rows(0)("SRN_No"))
            ''
            '' Anubhooti 10-Dec-2014 BM00000003662
            obj.Item_Conversion_Type = clsCommon.myCstr(dt.Rows(0)("Item_Conversion_Type"))
            ''
            'qry = "SELECT TSPL_Milk_RGP_DETAIL.MRP,TSPL_Milk_RGP_DETAIL.RGP_No,TSPL_Milk_RGP_DETAIL.Line_No,TSPL_Milk_RGP_DETAIL.Item_Code,TSPL_Milk_RGP_DETAIL.Item_Desc,TSPL_Milk_RGP_DETAIL.RGP_Qty, (Select ISNULL(RGP_Qty,0) from TSPL_Milk_RGP_DETAIL as dtl Where dtl.RGP_No=TSPL_Milk_RGP_Head.Against_NRGP AND dtl.Item_Code=TSPL_Milk_RGP_DETAIL.Item_Code) as NRGP_Qty, TSPL_Milk_RGP_DETAIL.Unit_code,TSPL_Milk_RGP_DETAIL.Item_Cost,TSPL_Milk_RGP_DETAIL.Amount, TSPL_Milk_RGP_DETAIL.Last_RGP_No, TSPL_Milk_RGP_DETAIL.Last_RGP_Date, TSPL_Milk_RGP_DETAIL.Specification FROM TSPL_Milk_RGP_DETAIL LEFT OUTER JOIN TSPL_Milk_RGP_Head ON TSPL_Milk_RGP_Head.RGP_No=TSPL_Milk_RGP_DETAIL.RGP_No where TSPL_Milk_RGP_DETAIL.RGP_No='" & obj.RGP_No & "'  ORDER BY TSPL_Milk_RGP_DETAIL.Line_No"
            If clsCommon.myLen(obj.Against_NRGP) <= 0 Then
                qry = "Select XXX.MRP, XXX.RGP_No, XXX.Line_No, XXX.Item_Code, XXX.Item_Desc, XXX.RGP_Qty, XXX.NRGP_Qty, XXX.Unit_code, XXX.Item_Cost,XXX.Approx_Cost, XXX.Amount, XXX.Last_RGP_No, XXX.Last_RGP_Date, XXX.Specification, RGP_Qty-(select ISNULL(SUM(RGP_Qty),0) from TSPL_Milk_RGP_Head LEFT OUTER JOIN TSPL_Milk_RGP_DETAIL ON TSPL_Milk_RGP_DETAIL.RGP_No=TSPL_Milk_RGP_Head.RGP_No WHERE TSPL_Milk_RGP_Head.Against_NRGP='" & obj.RGP_No & "' AND TSPL_Milk_RGP_DETAIL.Item_Code=XXX.Item_Code) as Balance_Qty from ("
            Else
                qry = "Select XXX.MRP, XXX.RGP_No, XXX.Line_No, XXX.Item_Code, XXX.Item_Desc, XXX.RGP_Qty, XXX.NRGP_Qty, XXX.Unit_code, XXX.Item_Cost,XXX.Approx_Cost, XXX.Amount, XXX.Last_RGP_No, XXX.Last_RGP_Date, XXX.Specification, NRGP_Qty-(select ISNULL(SUM(RGP_Qty),0) from TSPL_Milk_RGP_Head LEFT OUTER JOIN TSPL_Milk_RGP_DETAIL ON TSPL_Milk_RGP_DETAIL.RGP_No=TSPL_Milk_RGP_Head.RGP_No WHERE TSPL_Milk_RGP_Head.Against_NRGP='" & obj.Against_NRGP & "' AND TSPL_Milk_RGP_DETAIL.Item_Code=XXX.Item_Code AND TSPL_Milk_RGP_Head.RGP_No<>'" & obj.RGP_No & "') as Balance_Qty from ("
            End If
            qry += " SELECT TSPL_Milk_RGP_DETAIL.MRP,TSPL_Milk_RGP_DETAIL.RGP_No,TSPL_Milk_RGP_DETAIL.Line_No,TSPL_Milk_RGP_DETAIL.Item_Code,TSPL_Milk_RGP_DETAIL.Item_Desc,TSPL_Milk_RGP_DETAIL.RGP_Qty," & _
        " (Select ISNULL(RGP_Qty,0) from TSPL_Milk_RGP_DETAIL as dtl Where dtl.RGP_No=TSPL_Milk_RGP_Head.Against_NRGP AND dtl.Item_Code=TSPL_Milk_RGP_DETAIL.Item_Code) as NRGP_Qty," & _
        " TSPL_Milk_RGP_DETAIL.Unit_code,TSPL_Milk_RGP_DETAIL.Item_Cost,isnull(TSPL_Milk_RGP_DETAIL.Approx_Cost,0) AS Approx_Cost,TSPL_Milk_RGP_DETAIL.Amount, TSPL_Milk_RGP_DETAIL.Last_RGP_No, TSPL_Milk_RGP_DETAIL.Last_RGP_Date," & _
        " TSPL_Milk_RGP_DETAIL.Specification, TSPL_Milk_RGP_Head.Against_NRGP FROM TSPL_Milk_RGP_DETAIL" & _
        " LEFT OUTER JOIN TSPL_Milk_RGP_Head ON TSPL_Milk_RGP_Head.RGP_No=TSPL_Milk_RGP_DETAIL.RGP_No where TSPL_Milk_RGP_DETAIL.RGP_No='" & obj.RGP_No & "' AND TSPL_Milk_RGP_DETAIL.Item_Code='" & strItemCode & "'" & _
        " ) XXX ORDER BY XXX.Line_No"
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry, Nothing)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj.Arr = New List(Of clsMilkRGPDetail)
                Dim objTr As clsMilkRGPDetail
                For Each dr As DataRow In dt.Rows
                    objTr = New clsMilkRGPDetail
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

            Dim obj As clsMilkRGPHead = clsMilkRGPHead.GetData(strDocNo, NavigatorType.Current, trans)

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
            'Dim ArrLocationDetails As List(Of clsItemLocationDetails) = New List(Of clsItemLocationDetails)()
            Dim ArrInventoryMovement As List(Of clsInventoryMovement) = New List(Of clsInventoryMovement)
            Dim objInventoryMovemnt As Object
            For Each objTr As clsMilkRGPDetail In obj.Arr

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
                objInventoryMovemnt.InOut = IIf(clsCommon.CompairString(obj.Doc_Type, "NRGPR") = CompairStringResult.Equal, "I", "O")
                objInventoryMovemnt.Location_Code = obj.Arr(0).Location_Code 'obj.Location

                objInventoryMovemnt.Vendor_Code = obj.Vendor_Code
                objInventoryMovemnt.Vendor_Name = obj.Vendor_Name

                objInventoryMovemnt.Item_Code = objTr.Item_Code
                objInventoryMovemnt.Item_Desc = objTr.Item_Desc
                objInventoryMovemnt.Qty = objTr.RGP_Qty
                objInventoryMovemnt.UOM = objTr.Unit_code
                objInventoryMovemnt.Basic_Cost = objTr.Item_Cost
                ''objInventoryMovemnt.Rec_Cost= objTr.MRP
                ''objInventoryMovemnt.Add_Cost = 'objTr.Total_Tax_Amt
                objInventoryMovemnt.Net_Cost = objTr.Amount
                objInventoryMovemnt.ItemType = strItemTypeToSave
                ArrInventoryMovement.Add(objInventoryMovemnt)
            Next

            If obj.Is_Non_Inventory = 0 Then
                'isSaved = isSaved AndAlso clsItemLocationDetails.SaveData(clsCommon.GetPrintDate(strPostDate, "dd/MM/yyyy"), ArrLocationDetails, trans)
                isSaved = isSaved AndAlso clsInventoryMovement.SaveData(obj.Doc_Type, obj.RGP_No, obj.RGP_Date, clsCommon.GetPrintDate(strPostDate, "dd/MM/yyyy"), ArrInventoryMovement, trans)
            End If
            '' Anubhooti 12-Nov-2014 BM00000003662
            Dim ArryLstGLAC As ArrayList = New ArrayList()
            Dim PurAccQry As String = ""
            'Dim PurSetJobWork As String = ""
            'PurSetJobWork = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select ISNULL(Job_Work_Ac,'') AS Job_Work_Account From TSPL_PURCHASE_ACCOUNTS", trans))
            ' If clsCommon.CompairString(obj.Against_JobWork, "1") = CompairStringResult.Equal AndAlso clsCommon.CompairString(obj.Doc_Type, "RGP") = CompairStringResult.Equal Then
            If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowPurchaseAccounting, clsFixedParameterCode.AllowPurchaseAccounting, trans)) = 0 Then
                For Each objTr As clsMilkRGPDetail In obj.Arr
                    PurAccQry = "select TSPL_ITEM_MASTER.Purchase_Class_Code,TSPL_PURCHASE_ACCOUNTS.Inv_Control_Account,TSPL_PURCHASE_ACCOUNTS.Inv_Payable_Clearing,TSPL_PURCHASE_ACCOUNTS.Assembly_Cost_Credit,TSPL_PURCHASE_ACCOUNTS.Breakage_Gl_Account,TSPL_PURCHASE_ACCOUNTS.RM_Consumption,ISNULL(Job_Work_Ac,'') AS Job_Work_Account  from TSPL_ITEM_MASTER left outer join TSPL_PURCHASE_ACCOUNTS on TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code=TSPL_ITEM_MASTER.Purchase_Class_Code where TSPL_ITEM_MASTER.Item_Code='" + objTr.Item_Code + "'"
                    Dim dt As DataTable = clsDBFuncationality.GetDataTable(PurAccQry, trans)
                    If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                        Throw New Exception("Please set Purchase Account set for item " + objTr.Item_Code + "(" + objTr.Item_Desc + ")")
                    End If
                    ''1)
                    Dim strInvCtrlAC As String = clsCommon.myCstr(dt.Rows(0)("Inv_Control_Account"))
                    If clsCommon.myLen(strInvCtrlAC) <= 0 Then
                        Throw New Exception("Please set Inventory Control Account for Purchase Account set Code :" + clsCommon.myCstr(dt.Rows(0)("Purchase_Class_Code")) + " and Item: " + objTr.Item_Code + "(" + objTr.Item_Desc + ") ")
                    End If
                    strInvCtrlAC = clsERPFuncationality.ChangeGLAccountLocationSegment(strInvCtrlAC, obj.Location, trans)
                    Dim AccCr() As String = {strInvCtrlAC, -1 * Math.Round(((objTr.Amount)), 2, MidpointRounding.ToEven)}
                    ArryLstGLAC.Add(AccCr)

                    Dim PurSetJobWork As String = clsCommon.myCstr(dt.Rows(0)("Job_Work_Account"))
                    If clsCommon.myLen(PurSetJobWork) <= 0 Then
                        Throw New Exception("Please set Job Work Account for Purchase Account set Code :" + clsCommon.myCstr(dt.Rows(0)("Purchase_Class_Code")) + " and Item: " + objTr.Item_Code + "(" + objTr.Item_Desc + ") ")
                    End If
                    If clsCommon.myLen(PurSetJobWork) > 0 Then
                        PurSetJobWork = clsERPFuncationality.ChangeGLAccountLocationSegment(PurSetJobWork, obj.Location, trans)
                        Dim AccDr() As String = {PurSetJobWork, Math.Round(((objTr.Amount)), 2, MidpointRounding.ToEven)}
                        ArryLstGLAC.Add(AccDr)
                    Else
                        Throw New Exception("Please set job work account in purchase Account")
                    End If
                Next
                clsJournalMaster.FunGrnlEntryWithTrans(obj.Location, False, trans, obj.RGP_Date, "Milk RGP Job Work Against-" & obj.RGP_No & "", "MR-JW", "RGP Job Work", obj.RGP_No, obj.Remarks, "V", obj.Vendor_Code, obj.Vendor_Name, objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLstGLAC)
            End If
            'End If
            ''
            '========================================================================
            If obj.Against_JobWork = 1 AndAlso obj.Against_BOM = 1 Then
                SaveRGPBOMDetail(obj, trans)
            End If
            '==============================================================

            qry = "Update TSPL_Milk_RGP_Head set Status=1, Posting_Date='" + strPostDate + "',Modify_By='" + objCommonVar.CurrentUserCode + "' where RGP_No='" + strDocNo + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)


            If isSaved Then
                trans.Commit()
            Else
                trans.Rollback()
            End If
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function PostData(ByVal strDocNo As String, ByVal Trans As SqlTransaction) As Boolean
        Try
            Dim isSaved As Boolean = True
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("SRN No not found to Post")
            End If
            Dim strPostDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(Trans), "dd/MMM/yyyy hh:mm tt")

            Dim obj As clsMilkRGPHead = clsMilkRGPHead.GetData(strDocNo, NavigatorType.Current, Trans)

            If (obj Is Nothing OrElse clsCommon.myLen(obj.RGP_No) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "Milk Jobwork", "Milk JobWork RGP", obj.Location, obj.RGP_Date, Trans)
            If (obj.Status = ERPTransactionStatus.Approved) Then
                Throw New Exception("Already Post on :" + obj.Posting_Date)
            End If
            If (obj.On_Hold) Then
                Throw New Exception("Document No " + obj.RGP_No + " Is On Hold.Can't Post it")
            End If
            Dim qry As String = ""
            'Dim ArrLocationDetails As List(Of clsItemLocationDetails) = New List(Of clsItemLocationDetails)()
            Dim ArrInventoryMovement As List(Of clsInventoryMovement) = New List(Of clsInventoryMovement)
            Dim objInventoryMovemnt As clsInventoryMovement

            Dim ArrInventoryMovementMilk As List(Of clsInventoryMovementNew) = New List(Of clsInventoryMovementNew)
            Dim objInventoryMovemntMilk As clsInventoryMovementNew
            For Each objTr As clsMilkRGPDetail In obj.Arr
                qry = "select Item_Code,Item_Type,Product_Type from TSPL_ITEM_MASTER where Item_Code='" + objTr.Item_Code + "'"
                Dim dtItem As DataTable = clsDBFuncationality.GetDataTable(qry, Trans)

                Dim strItemType As String = If(dtItem.Rows.Count > 0, clsCommon.myCstr(dtItem.Rows(0).Item("Item_Type")), "") 'clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Item_Type from TSPL_ITEM_MASTER where Item_Code='" + objTr.Item_Code + "'", Trans))
                Dim strProductType As String = If(dtItem.Rows.Count > 0, clsCommon.myCstr(dtItem.Rows(0).Item("Product_Type")), "")
                dtItem = Nothing
                If clsCommon.CompairString(strProductType, "MI") <> CompairStringResult.Equal Then
                    Dim strItemTypeToSave As String = ""
                    If clsCommon.CompairString(strItemType, "R") = CompairStringResult.Equal Then
                        strItemTypeToSave = "RM"
                    ElseIf clsCommon.CompairString(strItemType, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(strItemType, "O") = CompairStringResult.Equal Then
                        strItemTypeToSave = "OT"
                    ElseIf clsCommon.CompairString(strItemType, "F") = CompairStringResult.Equal Then
                        strItemTypeToSave = "FT"
                    End If

                    objInventoryMovemnt = New clsInventoryMovement()
                    objInventoryMovemnt.InOut = IIf(clsCommon.CompairString(obj.Doc_Type, "NRGPR") = CompairStringResult.Equal, "I", "O")
                    objInventoryMovemnt.Location_Code = obj.Arr(0).Location_Code 'obj.Location

                    objInventoryMovemnt.Vendor_Code = obj.Vendor_Code
                    objInventoryMovemnt.Vendor_Name = obj.Vendor_Name

                    objInventoryMovemnt.Item_Code = objTr.Item_Code
                    objInventoryMovemnt.Item_Desc = objTr.Item_Desc
                    objInventoryMovemnt.Qty = objTr.RGP_Qty
                    objInventoryMovemnt.UOM = objTr.Unit_code
                    objInventoryMovemnt.Basic_Cost = objTr.Item_Cost
                    ''objInventoryMovemnt.Rec_Cost= objTr.MRP
                    ''objInventoryMovemnt.Add_Cost = 'objTr.Total_Tax_Amt
                    objInventoryMovemnt.Net_Cost = objTr.Amount
                    objInventoryMovemnt.ItemType = strItemTypeToSave
                    ArrInventoryMovement.Add(objInventoryMovemnt)
                Else
                    Dim strItemTypeToSave As String = ""
                    If clsCommon.CompairString(strItemType, "R") = CompairStringResult.Equal Then
                        strItemTypeToSave = "RM"
                    ElseIf clsCommon.CompairString(strItemType, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(strItemType, "O") = CompairStringResult.Equal Then
                        strItemTypeToSave = "OT"
                    ElseIf clsCommon.CompairString(strItemType, "F") = CompairStringResult.Equal Then
                        strItemTypeToSave = "FT"
                    End If

                    objInventoryMovemntMilk = New clsInventoryMovementNew()
                    objInventoryMovemntMilk.InOut = IIf(clsCommon.CompairString(obj.Doc_Type, "NRGPR") = CompairStringResult.Equal, "I", "O")
                    objInventoryMovemntMilk.Location_Code = obj.Arr(0).Location_Code 'obj.Location

                    objInventoryMovemntMilk.Vendor_Code = obj.Vendor_Code
                    objInventoryMovemntMilk.Vendor_Name = obj.Vendor_Name

                    objInventoryMovemntMilk.Item_Code = objTr.Item_Code
                    objInventoryMovemntMilk.Item_Desc = objTr.Item_Desc
                    objInventoryMovemntMilk.Qty = objTr.RGP_Qty
                    objInventoryMovemntMilk.UOM = objTr.Unit_code
                    objInventoryMovemntMilk.Basic_Cost = objTr.Item_Cost
                    objInventoryMovemntMilk.Net_Cost = objTr.Amount
                    objInventoryMovemntMilk.ItemType = strItemTypeToSave

                    objInventoryMovemntMilk.FAT_Per = objTr.FAT_pers
                    objInventoryMovemntMilk.SNF_Per = objTr.SNF_Pers
                    objInventoryMovemntMilk.FAT_KG = objTr.FAT_KG
                    objInventoryMovemntMilk.SNF_KG = objTr.SNF_KG


                    '' UPDATE PRODUCTION COST
                    'objInventoryMovemntMilk.Fat_Rate = objTr.FAT_Rate
                    'objInventoryMovemntMilk.SNF_Rate = objTr.SNF_Rate
                    'objInventoryMovemntMilk.Fat_Amt = objTr.Fat_Amt
                    'objInventoryMovemntMilk.SNF_Amt = objTr.SNF_Amt

                    'objInventoryMovemntMilk.Avg_Cost = objTr.Fat_Amt + objTr.SNF_Amt
                    'objInventoryMovemntMilk.FIFO_Cost = objTr.Fat_Amt + objTr.SNF_Amt
                    'objInventoryMovemntMilk.LIFO_Cost = objTr.Fat_Amt + objTr.SNF_Amt
                    'If clsCommon.CompairString(objInventoryMovemntMilk.InOut, "I") = CompairStringResult.Equal Then
                    'objInventoryMovemntMilk.Basic_Cost = (objTr.Fat_Amt + objTr.SNF_Amt) / IIf(objTr.FINAL_PRODUCTION_QTY = 0, 1, objTr.FINAL_PRODUCTION_QTY)
                    'objInventoryMovemntMilk.Net_Cost = objTr.Fat_Amt + objTr.SNF_Amt
                    'End If

                    objInventoryMovemntMilk.MFG_Date = obj.RGP_Date
                    ArrInventoryMovementMilk.Add(objInventoryMovemntMilk)
                End If

            Next

            If obj.Is_Non_Inventory = 0 Then
                isSaved = isSaved AndAlso clsInventoryMovement.SaveData(obj.Doc_Type, obj.RGP_No, obj.RGP_Date, clsCommon.GetPrintDate(strPostDate, "dd/MM/yyyy"), ArrInventoryMovement, Trans)
                isSaved = isSaved AndAlso clsInventoryMovementNew.SaveData(obj.Doc_Type, obj.RGP_No, obj.RGP_Date, clsCommon.GetPrintDate(strPostDate, "dd/MM/yyyy"), ArrInventoryMovementMilk, Trans)
            End If
            If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowPurchaseAccounting, clsFixedParameterCode.AllowPurchaseAccounting, Trans)) = 0 Then
                '' Anubhooti 12-Nov-2014 BM00000003662
                Dim ArryLstGLAC As ArrayList = New ArrayList()
                Dim PurAccQry As String = ""
                'Dim PurSetJobWork As String = ""
                'PurSetJobWork = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select ISNULL(Job_Work_Ac,'') AS Job_Work_Account From TSPL_PURCHASE_ACCOUNTS", trans))
                ' If clsCommon.CompairString(obj.Against_JobWork, "1") = CompairStringResult.Equal AndAlso clsCommon.CompairString(obj.Doc_Type, "RGP") = CompairStringResult.Equal Then
                For Each objTr As clsMilkRGPDetail In obj.Arr
                    PurAccQry = "select TSPL_ITEM_MASTER.Purchase_Class_Code,TSPL_PURCHASE_ACCOUNTS.Inv_Control_Account,TSPL_PURCHASE_ACCOUNTS.Inv_Payable_Clearing,TSPL_PURCHASE_ACCOUNTS.Assembly_Cost_Credit,TSPL_PURCHASE_ACCOUNTS.Breakage_Gl_Account,TSPL_PURCHASE_ACCOUNTS.RM_Consumption,ISNULL(Job_Work_Ac,'') AS Job_Work_Account  from TSPL_ITEM_MASTER left outer join TSPL_PURCHASE_ACCOUNTS on TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code=TSPL_ITEM_MASTER.Purchase_Class_Code where TSPL_ITEM_MASTER.Item_Code='" + objTr.Item_Code + "'"
                    Dim dt As DataTable = clsDBFuncationality.GetDataTable(PurAccQry, Trans)
                    If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                        Throw New Exception("Please set Purchase Account set for item " + objTr.Item_Code + "(" + objTr.Item_Desc + ")")
                    End If
                    ''1)
                    Dim strInvCtrlAC As String = clsCommon.myCstr(dt.Rows(0)("Inv_Control_Account"))
                    If clsCommon.myLen(strInvCtrlAC) <= 0 Then
                        Throw New Exception("Please set Inventory Control Account for Purchase Account set Code :" + clsCommon.myCstr(dt.Rows(0)("Purchase_Class_Code")) + " and Item: " + objTr.Item_Code + "(" + objTr.Item_Desc + ") ")
                    End If
                    strInvCtrlAC = clsERPFuncationality.ChangeGLAccountLocationSegment(strInvCtrlAC, obj.Location, Trans)
                    Dim AccCr() As String = {strInvCtrlAC, -1 * Math.Round(((objTr.Amount)), 2, MidpointRounding.ToEven)}
                    ArryLstGLAC.Add(AccCr)

                    Dim PurSetJobWork As String = clsCommon.myCstr(dt.Rows(0)("Job_Work_Account"))
                    If clsCommon.myLen(PurSetJobWork) <= 0 Then
                        Throw New Exception("Please set Job Work Account for Purchase Account set Code :" + clsCommon.myCstr(dt.Rows(0)("Purchase_Class_Code")) + " and Item: " + objTr.Item_Code + "(" + objTr.Item_Desc + ") ")
                    End If
                    If clsCommon.myLen(PurSetJobWork) > 0 Then
                        PurSetJobWork = clsERPFuncationality.ChangeGLAccountLocationSegment(PurSetJobWork, obj.Location, Trans)
                        Dim AccDr() As String = {PurSetJobWork, Math.Round(((objTr.Amount)), 2, MidpointRounding.ToEven)}
                        ArryLstGLAC.Add(AccDr)
                    Else
                        Throw New Exception("Please set job work account in purchase Account")
                    End If
                Next
                clsJournalMaster.FunGrnlEntryWithTrans(obj.Location, False, Trans, obj.RGP_Date, "Milk RGP Job Work Against-" & obj.RGP_No & "", "MR-JW", "RGP Job Work", obj.RGP_No, obj.Remarks, "V", obj.Vendor_Code, obj.Vendor_Name, objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLstGLAC)

            End If
            'End If
            ''
            '========================================================================
            'If obj.Against_JobWork = 1 AndAlso obj.Against_BOM = 1 Then
            '    SaveRGPBOMDetail(obj, Trans)
            'End If
            '==============================================================

            qry = "Update TSPL_Milk_RGP_Head set Status=1, Posting_Date='" + strPostDate + "',Modify_By='" + objCommonVar.CurrentUserCode + "' where RGP_No='" + strDocNo + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, Trans)


            'If isSaved Then
            '    Trans.Commit()
            'Else
            '    Trans.Rollback()
            'End If
        Catch ex As Exception
            'Trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function SaveRGPBOMDetail(ByVal obj As clsMilkRGPHead, ByVal trans As SqlTransaction) As Boolean
        Dim coll As New Hashtable()
        Try
            If obj IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                For Each objtr As clsMilkRGPDetail In obj.Arr
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

    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim isSaved As Boolean = False
        If (clsCommon.myLen(strCode) <= 0) Then
            Throw New Exception("Purchase Order No not found to Delete")
        End If
        Dim obj As clsMilkRGPHead = clsMilkRGPHead.GetData(strCode, NavigatorType.Current)
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.RGP_No) > 0) Then
            Try
                clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "Purchase Order", "RGP/NRGP", obj.Location, obj.RGP_Date, trans)
                If (obj.Status = 1) Then
                    Throw New Exception("Already Posted on :" + obj.Posting_Date)
                End If
                Dim qry As String = "delete from TSPL_Milk_RGP_DETAIL where RGP_No='" + strCode + "'"
                isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)

                qry = "delete from TSPL_RGP_JOB_WORK_DETAIL where RGP_No='" + strCode + "'"
                isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

                qry = "delete from TSPL_Milk_RGP_Head where RGP_No='" + strCode + "'"
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
            If ReverseAndUnpost(strCode, trans) Then
                trans.Commit()
            Else
                trans.Rollback()
                Return False
            End If        
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True


    End Function
    Public Shared Function ReverseAndUnpost(ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        'Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim Qry As String = "select Status from TSPL_Milk_RGP_Head where RGP_No='" + strCode + "'"
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

            Qry = "select InOut,Trans_Type,Item_Code,Item_Desc,Location_Code,case when InOut='I' then -1 else 1 end *Qty as Qty ,UOM,MRP,ItemType,case when InOut='I' then -1 else 1 end* Basic_Cost as Basic_Cost from TSPL_INVENTORY_MOVEMENT_new where Source_Doc_No='" + strCode + "' and Trans_Type in ('NRGP','RGP')"
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

            Qry = "delete from TSPL_INVENTORY_MOVEMENT_new where Source_Doc_No='" + strCode + "' and Trans_Type in ('NRGP','RGP')"
            clsDBFuncationality.ExecuteNonQuery(Qry, trans)

            Qry = "Update TSPL_Milk_RGP_Head set Status = 0,Posting_Date=null where RGP_No='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(Qry, trans)

            'trans.Commit()
        Catch ex As Exception
            'trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
End Class

Public Class clsMilkRGPDetail
#Region "Variables"
    Public Item_MRP As Decimal = Nothing
    Public RGP_No As String = Nothing
    Public Line_No As Integer = 0
    Public Item_Code As String = Nothing
    Public Item_Desc As String = Nothing
    Public HSN_Code As String = Nothing  '' Read only
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
    Public FAT_pers As Double = 0
    Public SNF_Pers As Double = 0
    Public FAT_Cost As Double = 0
    Public SNF_Cost As Double = 0
    Public FAT_KG As Double = 0
    Public SNF_KG As Double = 0
    Public FAT_Price As Double = 0
    Public SNF_Price As Double = 0
    Public TanKer_No As String = Nothing
    Public Location_Sub_Code As String = String.Empty
    Public Location_Code As String = String.Empty
    Public Location_Type As String = String.Empty
    Public Bulk_Milk_Srn_No As String = String.Empty
#End Region

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsMilkRGPDetail), ByVal trans As SqlTransaction, ByVal ObjDate As Date) As Boolean
        Dim Item_Production_type As String = String.Empty
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsMilkRGPDetail In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "MRP", obj.Item_MRP)
                clsCommon.AddColumnsForChange(coll, "RGP_No", strDocNo)
                clsCommon.AddColumnsForChange(coll, "Bulk_Milk_Srn_Code", obj.Bulk_Milk_Srn_No, True)
                clsCommon.AddColumnsForChange(coll, "Line_No", obj.Line_No)
                clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code)
                clsCommon.AddColumnsForChange(coll, "Item_Desc", obj.Item_Desc)
                clsCommon.AddColumnsForChange(coll, "RGP_Qty", obj.RGP_Qty)
                clsCommon.AddColumnsForChange(coll, "FAT_pers", obj.FAT_pers)
                clsCommon.AddColumnsForChange(coll, "SNF_Pers", obj.SNF_Pers)
                'clsCommon.AddColumnsForChange(coll, "FAT_Cost", obj.FAT_Cost)
                'clsCommon.AddColumnsForChange(coll, "SNF_Cost", obj.SNF_Cost)
                clsCommon.AddColumnsForChange(coll, "FAT_KG", obj.FAT_KG)
                clsCommon.AddColumnsForChange(coll, "SNF_KG", obj.SNF_KG)
                'clsCommon.AddColumnsForChange(coll, "FAT_Price", obj.FAT_Price)
                'clsCommon.AddColumnsForChange(coll, "SNF_Price", obj.SNF_Price)
                clsCommon.AddColumnsForChange(coll, "TanKer_No", obj.TanKer_No)
                clsCommon.AddColumnsForChange(coll, "To_Location_Code", obj.Location_Code)
                clsCommon.AddColumnsForChange(coll, "To_SubLocation_YN", obj.Location_Type)
                clsCommon.AddColumnsForChange(coll, "Unit_code", obj.Unit_code)
                clsCommon.AddColumnsForChange(coll, "Item_Cost", obj.Item_Cost)

                clsCommon.AddColumnsForChange(coll, "Main_PO_Icode", obj.Main_PO_Icode, True)
                clsCommon.AddColumnsForChange(coll, "BOM_Code", obj.BOM_Code)
                '' production costing columns
                Dim objCost As New MIlkComponentType
                If clsCommon.myLen(obj.Bulk_Milk_Srn_No) <= 0 Then
                    Item_Production_type = clsDBFuncationality.getSingleValue("select Product_Type from TSPL_ITEM_MASTER where item_Code='" & obj.Item_Code & "'", trans)
                    objCost = clsInventoryMovementNew.GetAvgCost(Item_Production_type, obj.Item_Code, IIf(clsCommon.myLen(obj.Location_Code) <= 0, obj.Location_Code, obj.Location_Code), obj.RGP_Qty, obj.Unit_code, obj.FAT_KG, obj.SNF_KG, ObjDate, ObjDate, True, trans)
                    obj.FAT_Cost = objCost.FAT_Cost / IIf(obj.FAT_KG <= 0, 1, obj.FAT_KG)
                    obj.SNF_Cost = objCost.SNF_Cost / IIf(obj.SNF_KG <= 0, 1, obj.SNF_KG)
                    obj.FAT_Price = objCost.FAT_Cost
                    obj.SNF_Price = objCost.SNF_Cost
                End If
                Item_Production_type = Nothing
                clsCommon.AddColumnsForChange(coll, "FAT_Cost", obj.FAT_Cost)
                clsCommon.AddColumnsForChange(coll, "SNF_Cost", obj.SNF_Cost)
                clsCommon.AddColumnsForChange(coll, "FAT_Price", obj.FAT_Price)
                clsCommon.AddColumnsForChange(coll, "SNF_Price", obj.SNF_Price)
                clsCommon.AddColumnsForChange(coll, "Amount", Math.Round(obj.FAT_Price + obj.SNF_Price, 0))
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

                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Milk_RGP_DETAIL", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function

    Public Shared Function GetBalanceRGPQty(ByVal strRGPCode As String, ByVal strICode As String, ByVal strCurrGRN_SRNNo As String, ByVal strUomCode As String, Optional ByVal IsJobWork As Boolean = False) As Double
        Dim qry As String = "select SUM(qty * RI) as Balance from(  "
        If IsJobWork Then
            qry += " select TSPL_RGP_JOB_WORK_DETAIL.Item_Code as ICode,TSPL_RGP_JOB_WORK_DETAIL.RGP_Qty as Qty,1 as RI from TSPL_RGP_JOB_WORK_DETAIL left outer join TSPL_Milk_RGP_Head on TSPL_Milk_RGP_Head.RGP_No=TSPL_RGP_JOB_WORK_DETAIL.RGP_No where TSPL_Milk_RGP_Head.Status=1 and TSPL_RGP_JOB_WORK_DETAIL.RGP_No ='" + strRGPCode + "' and TSPL_RGP_JOB_WORK_DETAIL.Item_Code='" + strICode + "' and TSPL_RGP_JOB_WORK_DETAIL.unit_code='" + strUomCode + "' "
        Else
            qry += " select TSPL_Milk_RGP_DETAIL.Item_Code as ICode,TSPL_Milk_RGP_DETAIL.RGP_Qty as Qty,1 as RI from TSPL_Milk_RGP_DETAIL left outer join TSPL_Milk_RGP_Head on TSPL_Milk_RGP_Head.RGP_No=TSPL_Milk_RGP_DETAIL.RGP_No where TSPL_Milk_RGP_Head.Status=1 and TSPL_Milk_RGP_DETAIL.RGP_No ='" + strRGPCode + "' and TSPL_Milk_RGP_DETAIL.Item_Code='" + strICode + "' and TSPL_Milk_RGP_DETAIL.unit_code='" + strUomCode + "' and isnull(TSPL_Milk_RGP_Head.Against_JobWork,0)=0 "
        End If

        qry += " union all " & _
            " select TSPL_SRN_DETAIL.Item_Code as ICode,(TSPL_SRN_DETAIL.SRN_Qty) as Qty,-1 as RI from TSPL_SRN_DETAIL left outer join TSPL_SRN_HEAD on TSPL_SRN_HEAD.SRN_No=TSPL_SRN_DETAIL.SRN_No where TSPL_SRN_DETAIL.RGP_Id='" + strRGPCode + "'   and TSPL_SRN_DETAIL.Item_Code='" + strICode + "' and TSPL_SRN_DETAIL.unit_code='" + strUomCode + "' and TSPL_SRN_DETAIL.SRN_No not in ('" + strCurrGRN_SRNNo + "')  " & _
            " union all " & _
            " select TSPL_GRN_DETAIL.Item_Code as ICode,(TSPL_GRN_DETAIL.GRN_Qty) as Qty,-1 as RI from TSPL_GRN_DETAIL left outer join TSPL_GRN_HEAD on TSPL_GRN_HEAD.GRN_No=TSPL_GRN_DETAIL.GRN_No where TSPL_GRN_DETAIL.against_RGP_no='" + strRGPCode + "'   and TSPL_GRN_DETAIL.Item_Code='" + strICode + "' and TSPL_GRN_DETAIL.unit_code='" + strUomCode + "' and TSPL_GRN_DETAIL.GRN_No not in ('" + strCurrGRN_SRNNo + "')  " & _
            " )Final "
        Return clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
    End Function
End Class

Public Class clsMilkRGPIssueQCDetail
#Region "Variables"
    Public sno As Integer = Nothing
    Public frm_loc_code As String = Nothing
    Public frm_loc_desc As String = Nothing
    Public to_loc_code As String = Nothing
    Public to_loc_desc As String = Nothing
    Public itemcode As String = Nothing
    Public itemname As String = Nothing
    Public param_code As String = Nothing
    Public param_desc As String = Nothing
    Public param_type As String = Nothing
    Public param_nature As String = Nothing
    Public lrange As Decimal = Nothing
    Public urange As Decimal = Nothing
    Public status_grid As String = Nothing
    Public value1 As String = Nothing
    Public value2 As String = Nothing
    Public remarks As String = Nothing
    Public QCRange As Decimal = Nothing
    Public QCValue As String = Nothing
    Public QCStatus As String = Nothing
#End Region

    Public Shared Function SaveData(ByVal issue_code As String, ByVal arr As List(Of clsMilkRGPIssueQCDetail), ByVal trans As SqlTransaction) As Boolean
        Try
            Dim isSaved As Boolean = True
            Dim qry As String = "delete from TSPL_MR_ISSUE_QC_DETAIL where issue_code='" + issue_code + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Dim coll As New Hashtable()

            If arr IsNot Nothing AndAlso arr.Count > 0 Then
                For Each objtr As clsMilkRGPIssueQCDetail In arr
                    coll = New Hashtable()

                    clsCommon.AddColumnsForChange(coll, "comp_code", objCommonVar.CurrentCompanyCode)
                    clsCommon.AddColumnsForChange(coll, "Issue_Code", issue_code)
                    clsCommon.AddColumnsForChange(coll, "SNO", objtr.sno)
                    clsCommon.AddColumnsForChange(coll, "From_Location_Code", objtr.frm_loc_code, True)
                    clsCommon.AddColumnsForChange(coll, "To_Location_Code", objtr.to_loc_code, True)
                    clsCommon.AddColumnsForChange(coll, "Item_Code", objtr.itemcode)
                    clsCommon.AddColumnsForChange(coll, "Parameter_Code", objtr.param_code)
                    clsCommon.AddColumnsForChange(coll, "Lower_range", objtr.lrange)
                    clsCommon.AddColumnsForChange(coll, "Upper_range", objtr.urange)
                    clsCommon.AddColumnsForChange(coll, "Status", objtr.status_grid)
                    clsCommon.AddColumnsForChange(coll, "Value1", objtr.value1)
                    clsCommon.AddColumnsForChange(coll, "Value2", objtr.value2)
                    clsCommon.AddColumnsForChange(coll, "Remarks", objtr.remarks)
                    clsCommon.AddColumnsForChange(coll, "QC_Range", objtr.QCRange)
                    clsCommon.AddColumnsForChange(coll, "QC_Value", objtr.QCValue)
                    clsCommon.AddColumnsForChange(coll, "QC_Status", objtr.QCStatus)

                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MR_ISSUE_QC_DETAIL", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If

            Return isSaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
End Class
