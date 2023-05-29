Imports common
Imports System.Data.SqlClient
Public Class clsTransferReturn
#Region "Variables"
    Public Document_No As String = Nothing
    Public Document_Date As DateTime
    Public Transfer_No As String = Nothing
    Public Document_Type As String = Nothing
    Public Remarks As String = Nothing

    Public Bill_To_Location As String = Nothing ''Not a Table field
    Public Form_ID As String = Nothing
    Public arrCustomFields As List(Of clsCustomFieldValues) = Nothing
    ' Prabhakar Anand 
    Public arrSrItem As List(Of clsSerializeInvenotry) = Nothing
    Public Ship_To_Location As String = Nothing
    Public Gate_ReturnNo As String = Nothing

#End Region

    Public Function SaveData(ByVal obj As clsTransferReturn, ByVal isNewEntry As Boolean) As Boolean
        Dim isSaved As Boolean = True
        Try

            Dim objSRN As clsTransferReturn = clsTransferReturn.GetData(obj.Transfer_No, NavigatorType.Current, Nothing)
            AllowToSave(obj, objSRN)
            Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
            Try
                If isNewEntry Then
                    obj.Document_No = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(obj.Document_Date), clsDocType.TransferReturn, "", obj.Bill_To_Location)
                End If
                If (clsCommon.myLen(obj.Document_No) <= 0) Then
                    Throw New Exception("Error in Document Code Generation")
                End If
                If Not isNewEntry Then
                    clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, obj.Document_No, "TSPL_Transfer_RETURN", "Document_No", trans)
                End If

                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy hh:mm tt"))
                clsCommon.AddColumnsForChange(coll, "Transfer_No", obj.Transfer_No)
                clsCommon.AddColumnsForChange(coll, "Document_Type", obj.Document_Type)
                clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks)
                clsCommon.AddColumnsForChange(coll, "Gate_ReturnNo", obj.Gate_ReturnNo)

                ''richa agarwal 
                If (clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Transfer_No from TSPL_Transfer_RETURN where Document_No='" & obj.Document_No & "' ", trans)), obj.Transfer_No) = CompairStringResult.Equal) Or (clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Transfer_No from TSPL_Transfer_RETURN where Document_No='" & obj.Document_No & "' ", trans)), "") = CompairStringResult.Equal) Then
                    If clsCommon.CompairString(obj.Document_Type, "O") = CompairStringResult.Equal Then
                        clsDBFuncationality.ExecuteNonQuery("Update TSPL_TRANSFER_ORDER_HEAD set Is_Status_IN='Y' where Document_No='" & obj.Transfer_No & "' and Transfer_Type='O'", trans)
                    ElseIf clsCommon.CompairString(obj.Document_Type, "I") = CompairStringResult.Equal Then
                        clsDBFuncationality.ExecuteNonQuery("Update TSPL_TRANSFER_ORDER_HEAD set Is_Status_IN='N' where Document_No=(Select TransferOutNo from TSPL_TRANSFER_ORDER_HEAD where Document_No='" & obj.Transfer_No & "' and Transfer_Type='I' ) and Transfer_Type='O'", trans)
                        clsDBFuncationality.ExecuteNonQuery("Update TSPL_TRANSFER_ORDER_HEAD set Is_Status_IN='Y' where Document_No='" & obj.Transfer_No & "' and Transfer_Type='I'", trans)
                    End If
                Else
                    'If clsCommon.CompairString(obj.Document_Type, "O") = CompairStringResult.Equal Then
                    '    clsDBFuncationality.ExecuteNonQuery("Update TSPL_TRANSFER_ORDER_HEAD set Is_Status_IN='Y' where Document_No=(Select Transfer_No from TSPL_Transfer_RETURN where Document_No='" & obj.Document_No & "' and Document_Type='O') and Transfer_Type='O'", trans)
                    '    clsDBFuncationality.ExecuteNonQuery("Update TSPL_TRANSFER_ORDER_HEAD set Is_Status_IN='N' where Document_No='" & obj.Transfer_No & "' and Transfer_Type='O'", trans)
                    'ElseIf clsCommon.CompairString(obj.Document_Type, "I") = CompairStringResult.Equal Then
                    '    clsDBFuncationality.ExecuteNonQuery("Update TSPL_TRANSFER_ORDER_HEAD set Is_Status_IN='Y' where Document_No=(Select TransferOutNo from TSPL_TRANSFER_ORDER_HEAD where Document_No=(Select Transfer_No from TSPL_Transfer_RETURN where Document_No='" & obj.Document_No & "' and Transfer_Type ='I') and Document_Type='I' ) and Transfer_Type='O'", trans)
                    '    clsDBFuncationality.ExecuteNonQuery("Update TSPL_TRANSFER_ORDER_HEAD set Is_Status_IN='N' where Document_No=(Select TransferOutNo from TSPL_TRANSFER_ORDER_HEAD where Document_No='" & obj.Transfer_No & "' and Transfer_Type='I' ) and Transfer_Type='O'", trans)
                    'End If

                End If

                If isNewEntry Then
                    clsCommon.AddColumnsForChange(coll, "Document_No", obj.Document_No)
                    clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Transfer_RETURN", OMInsertOrUpdate.Insert, "", trans)
                Else
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Transfer_RETURN", OMInsertOrUpdate.Update, "TSPL_Transfer_RETURN.Document_No='" + obj.Document_No + "'", trans)
                End If
                isSaved = isSaved AndAlso clsCustomFieldValues.SaveData(obj.Form_ID, obj.Document_No, obj.arrCustomFields, trans)



                ' End If
                ''-----------------
                ''richa agarwal UDL/06/06/18-000183
                isSaved = isSaved AndAlso ConvertTrasfertoBatchItem(obj.Transfer_No, obj.Document_No, trans)
                ''-------------


                ''Revese Inventory movement
                Dim qry As String = "select * from TSPL_INVENTORY_MOVEMENT where Trans_Type='Transfer' and Source_Doc_No='" + obj.Transfer_No + "' and INOUT='I'"
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    Dim ArrInventoryMovement As New List(Of clsInventoryMovement)
                    Dim objInvMov As clsInventoryMovement
                    Dim count As Integer = 1
                    For Each dr As DataRow In dt.Rows
                        objInvMov = New clsInventoryMovement
                        objInvMov.InOut = "O"
                        objInvMov.Location_Code = clsCommon.myCstr(dr("Location_Code"))
                        objInvMov.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                        objInvMov.Item_Desc = clsCommon.myCstr(dr("Item_Desc"))
                        objInvMov.Qty = clsCommon.myCstr(dr("Qty"))
                        objInvMov.UOM = clsCommon.myCstr(dr("UOM"))
                        objInvMov.Source_Doc_No = obj.Document_No
                        objInvMov.Source_Doc_Date = obj.Document_Date
                        objInvMov.Entry_Date = clsCommon.GetPrintDate(obj.Document_Date, "dd/MM/yyyy")
                        objInvMov.Basic_Cost = clsCommon.myCdbl(dr("Basic_Cost"))
                        objInvMov.Rec_Cost = clsCommon.myCdbl(dr("Rec_Cost"))
                        objInvMov.Add_Cost = clsCommon.myCdbl(dr("Add_Cost"))
                        objInvMov.Net_Cost = clsCommon.myCdbl(dr("Net_Cost"))
                        objInvMov.ItemType = clsCommon.myCstr(dr("ItemType"))
                        objInvMov.Punching_Date = obj.Document_Date
                        objInvMov.MRP = clsCommon.myCdbl(dr("MRP"))
                        objInvMov.Batch_No = clsCommon.myCstr(dr("Batch_No"))
                        objInvMov.FIFO_Cost = clsCommon.myCdbl(dr("FIFO_Cost"))
                        objInvMov.LIFO_Cost = clsCommon.myCdbl(dr("LIFO_Cost"))
                        objInvMov.Avg_Cost = clsCommon.myCdbl(dr("Avg_Cost"))
                        objInvMov.Posting_Date = obj.Document_Date
                        objInvMov.Stock_UOM = clsCommon.myCstr(dr("Stock_UOM"))
                        objInvMov.Stock_Qty = clsCommon.myCdbl(dr("Stock_Qty"))
                        If dr("MFG_Date") IsNot DBNull.Value Then
                            objInvMov.MFG_Date = clsCommon.myCstr(dr("MFG_Date"))
                        End If
                        If dr("Expiry_Date") IsNot DBNull.Value Then
                            objInvMov.Expiry_Date = clsCommon.myCDate(dr("Expiry_Date"))
                        End If
                        objInvMov.IS_CONSUMPTION = clsCommon.myCdbl(dr("IS_CONSUMPTION"))
                        objInvMov.Cust_Code = clsCommon.myCstr(dr("Cust_Code"))
                        objInvMov.Cust_Name = clsCommon.myCstr(dr("Cust_Name"))
                        objInvMov.Vendor_Code = clsCommon.myCstr(dr("Vendor_Code"))
                        objInvMov.Vendor_Name = clsCommon.myCstr(dr("Vendor_Name"))
                        objInvMov.Other_Location_Code = clsCommon.myCstr(dr("Other_Location_Code"))

                        objInvMov.Other_Location_Desc = clsCommon.myCstr(dr("Other_Location_Desc"))

                        '' Work done by Parteek on Against ticket no. on 01/02/2019
                        Dim item_Purchase_Class As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Purchase_Class_Code from TSPL_ITEM_MASTER where Item_Code='" & objInvMov.Item_Code & "'", trans))
                        Dim qry1 As String = "select Loc_Segment_Code from TSPL_LOCATION_MASTER where Location_Code='" + objInvMov.Location_Code + "'"
                        '==========update by preeti Gupta Against ticket no[TEC/05/07/19-000933]
                        Dim strLocatinSegment As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry1, trans))
                        If clsCommon.myLen(item_Purchase_Class) > 0 Then
                            Dim Inventory_Purchase_code As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Inv_Control_Account from TSPL_PURCHASE_ACCOUNTS where Purchase_Class_Code='" & item_Purchase_Class & "'", trans))
                            If clsCommon.myLen(Inventory_Purchase_code) > 0 Then
                                objInvMov.Inventory_CrAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(Inventory_Purchase_code, objInvMov.Location_Code, trans)
                            End If
                        End If

                        '' end



                        obj.arrSrItem = clsSerializeInvenotry.GetData("Transfer", obj.Transfer_No, objInvMov.Item_Code, count, trans)
                        Dim Ship_Loc As String = Nothing
                        Ship_Loc = clsDBFuncationality.getSingleValue("  select Top 1 Location_Code from TSPL_SERIAL_ITEM where Document_Code='" + obj.Transfer_No + "' and In_Out_Type='I' ", trans)
                        ArrInventoryMovement.Add(objInvMov)
                        'clsSerializeInvenotry.SaveData("TRN-RET", obj.Document_No, obj.Document_Date, "I", objInvMov.Item_Code, obj.Ship_To_Location, count, obj.arrSrItem, trans)
                        clsSerializeInvenotry.SaveData("TRN-RET", obj.Document_No, obj.Document_Date, "O", objInvMov.Item_Code, Ship_Loc, count, arrSrItem, trans)
                        count = count + 1
                    Next
                    isSaved = isSaved AndAlso clsInventoryMovement.SaveData("TRN-RET", obj.Document_No, obj.Document_Date, clsCommon.GetPrintDate(obj.Document_Date, "dd/MM/yyyy"), ArrInventoryMovement, trans)
                End If

                qry = "select * from TSPL_INVENTORY_MOVEMENT where Trans_Type='Transfer' and Source_Doc_No='" + obj.Transfer_No + "' and INOUT='O'"
                dt = clsDBFuncationality.GetDataTable(qry, trans)

                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    Dim ArrInventoryMovement As New List(Of clsInventoryMovement)
                    Dim objInvMov As clsInventoryMovement
                    Dim counter As Integer = 1
                    For Each dr As DataRow In dt.Rows
                        objInvMov = New clsInventoryMovement
                        objInvMov.InOut = "I"
                        objInvMov.Location_Code = clsCommon.myCstr(dr("Location_Code"))
                        objInvMov.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                        objInvMov.Item_Desc = clsCommon.myCstr(dr("Item_Desc"))
                        objInvMov.Qty = clsCommon.myCstr(dr("Qty"))
                        objInvMov.UOM = clsCommon.myCstr(dr("UOM"))
                        objInvMov.Source_Doc_No = obj.Document_No
                        objInvMov.Source_Doc_Date = obj.Document_Date
                        objInvMov.Entry_Date = clsCommon.GetPrintDate(obj.Document_Date, "dd/MM/yyyy")
                        objInvMov.Basic_Cost = clsCommon.myCdbl(dr("Basic_Cost"))
                        objInvMov.Rec_Cost = clsCommon.myCdbl(dr("Rec_Cost"))
                        objInvMov.Add_Cost = clsCommon.myCdbl(dr("Add_Cost"))
                        objInvMov.Net_Cost = clsCommon.myCdbl(dr("Net_Cost"))
                        objInvMov.ItemType = clsCommon.myCstr(dr("ItemType"))
                        objInvMov.Punching_Date = obj.Document_Date
                        objInvMov.MRP = clsCommon.myCdbl(dr("MRP"))
                        objInvMov.Batch_No = clsCommon.myCstr(dr("Batch_No"))
                        objInvMov.FIFO_Cost = clsCommon.myCdbl(dr("FIFO_Cost"))
                        objInvMov.LIFO_Cost = clsCommon.myCdbl(dr("LIFO_Cost"))
                        objInvMov.Avg_Cost = clsCommon.myCdbl(dr("Avg_Cost"))
                        objInvMov.Posting_Date = obj.Document_Date
                        objInvMov.Stock_UOM = clsCommon.myCstr(dr("Stock_UOM"))
                        objInvMov.Stock_Qty = clsCommon.myCdbl(dr("Stock_Qty"))
                        If dr("MFG_Date") IsNot DBNull.Value Then
                            objInvMov.MFG_Date = clsCommon.myCstr(dr("MFG_Date"))
                        End If
                        If dr("Expiry_Date") IsNot DBNull.Value Then
                            objInvMov.Expiry_Date = clsCommon.myCDate(dr("Expiry_Date"))
                        End If
                        objInvMov.IS_CONSUMPTION = clsCommon.myCdbl(dr("IS_CONSUMPTION"))
                        objInvMov.Cust_Code = clsCommon.myCstr(dr("Cust_Code"))
                        objInvMov.Cust_Name = clsCommon.myCstr(dr("Cust_Name"))
                        objInvMov.Vendor_Code = clsCommon.myCstr(dr("Vendor_Code"))
                        objInvMov.Vendor_Name = clsCommon.myCstr(dr("Vendor_Name"))
                        objInvMov.Other_Location_Code = clsCommon.myCstr(dr("Other_Location_Code"))
                        objInvMov.Other_Location_Desc = clsCommon.myCstr(dr("Other_Location_Desc"))

                        '' Work done by Parteek on Against ticket no. on 04/02/2019
                        Dim item_Purchase_Class As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Purchase_Class_Code from TSPL_ITEM_MASTER where Item_Code='" & objInvMov.Item_Code & "'", trans))
                        Dim qry1 As String = "select Loc_Segment_Code from TSPL_LOCATION_MASTER where Location_Code='" + objInvMov.Location_Code + "'"
                        Dim strLocatinSegment As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry1, trans))
                        '==========update by preeti Gupta Against ticket no[TEC/05/07/19-000933]
                        If clsCommon.myLen(item_Purchase_Class) > 0 Then
                            Dim Inventory_Purchase_code As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Inv_Control_Account from TSPL_PURCHASE_ACCOUNTS where Purchase_Class_Code='" & item_Purchase_Class & "'", trans))
                            If clsCommon.myLen(Inventory_Purchase_code) > 0 Then
                                objInvMov.Inventory_DrAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(Inventory_Purchase_code, objInvMov.Location_Code, trans)
                            End If
                        End If

                        '' end



                        obj.arrSrItem = clsSerializeInvenotry.GetData("Transfer", obj.Transfer_No, objInvMov.Item_Code, counter, trans)
                        ArrInventoryMovement.Add(objInvMov)
                        'clsSerializeInvenotry.SaveData("TRN-RET", obj.Document_No, obj.Document_Date, "O", objInvMov.Item_Code, obj.Bill_To_Location, counter, arrSrItem, trans)
                        clsSerializeInvenotry.SaveData("TRN-RET", obj.Document_No, obj.Document_Date, "I", objInvMov.Item_Code, obj.Ship_To_Location, counter, obj.arrSrItem, trans)
                        counter = counter + 1
                    Next
                    isSaved = isSaved AndAlso clsInventoryMovement.SaveData("TRN-RET", obj.Document_No, obj.Document_Date, clsCommon.GetPrintDate(obj.Document_Date, "dd/MM/yyyy"), ArrInventoryMovement, trans)
                End If
                ''Journal Entry
                qry = " select TSPL_JOURNAL_DETAILS.Account_code,-1*TSPL_JOURNAL_DETAILS.Amount as Amount,TSPL_JOURNAL_DETAILS.Reco_Control_Account from TSPL_JOURNAL_DETAILS " & _
                " left outer join TSPL_JOURNAL_MASTER on TSPL_JOURNAL_MASTER.Voucher_No=TSPL_JOURNAL_DETAILS.Voucher_No" & _
                " where TSPL_JOURNAL_MASTER.Source_Doc_No='" + obj.Transfer_No + "'  and Source_Code in ('MM-TF')"
                dt = clsDBFuncationality.GetDataTable(qry, trans)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    Dim ArryLstGLAC As ArrayList = New ArrayList()
                    For Each dr As DataRow In dt.Rows
                        Dim Acc() As String = {clsCommon.myCstr(dr("Account_code")), clsCommon.myCdbl(dr("Amount")), "", "", "", "", "", "", clsCommon.myCstr(dr("Reco_Control_Account"))}
                        ArryLstGLAC.Add(Acc)
                    Next
                    transportSql.FunGrnlEntryWithTrans(obj.Bill_To_Location, False, trans, obj.Document_Date, "Against Transfer Return " + obj.Document_No, "SN-RT", "Store Received Note Return", obj.Document_No, obj.Remarks, "V", "", "", objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLstGLAC)
                End If
                trans.Commit()
            Catch ex As Exception
                trans.Rollback()
                Throw New Exception(ex.Message)
            End Try
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function
    Public Shared Function EInvoice_Implementation(ByVal strDocNo As String, ByVal strLocation As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim isSaved As Boolean = True
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Code not found to Post")
            End If

            Dim strtoken As String = ClsEInvoiceOFAPIs.IsGenerateAuthTokenNo_Required(objCommonVar.CurrentCompanyCode, strLocation, trans)
            If clsCommon.myLen(strtoken) > 0 Then
                Dim strQry As String = "select TSPL_TRANSFER_RETURN.Document_No as DocNo,convert(date,TSPL_TRANSFER_RETURN.Document_Date,103) as DocDate,'INV' as DocType ,'B2B' as SupTyp, 'N'  as IgstOnIntra,FromLocation.GSTNO as SellerGSTINNo ,FromLocation.location_desc as SellerLglNm,TSPL_COMPANY_MASTER.Comp_Name as SellerTrdNm,FromLocation.Add1 as SellerAdd1,FromLocation.Add2 as SellerAdd2 ,FromLocation.city_code  as SellerLoc,FromLocation.Pin_Code  as SellerPincode,Seller_State_Master .GST_STATE_Code as SellerStcd,FromLocation.Phone1 as SellerPhone,FromLocation.Email as SellerEmail,ToLocation.GSTNo as BuyerGSTINNo ,ToLocation.location_desc as BuyerLglNm,ToLocation.location_desc as BuyerTrdNm, Buyer_State_Master.GST_STATE_Code as BuyerPOS,Tolocation.Add1 as BuyerAdd1,Tolocation.Add2 as BuyerAdd2 ,BuyerCity.City_Name as BuyerLoc,Tolocation.Pin_Code as BuyerPincode,Buyer_State_Master.GST_STATE_Code as BuyerStcd,Tolocation.Phone1 as BuyerPhone,Tolocation.Email as BuyerEmail,TSPL_TRANSFER_ORDER_DETAIL.Line_No as ItemSlNo, 'N' as ItemIsServc,TSPL_ITEM_MASTER.Item_Desc AS ItemPrdDesc,TSPL_ITEM_MASTER.HSN_Code AS ItemHsnCd,TSPL_TRANSFER_ORDER_DETAIL.Out_Qty as ItemQty,case when TSPL_TRANSFER_ORDER_DETAIL.Unit_code='CASE' then 'CTN' when TSPL_TRANSFER_ORDER_DETAIL.Unit_code='JAR' then 'PCS' when TSPL_TRANSFER_ORDER_DETAIL.Unit_code='POUCH' then 'PKT' ELSE TSPL_TRANSFER_ORDER_DETAIL.Unit_code  END as ItemUnit,TSPL_TRANSFER_ORDER_DETAIL.Item_cost as ItemUnitPrice,TSPL_TRANSFER_ORDER_DETAIL.Amount as ItemTotAmt,TSPL_TRANSFER_ORDER_DETAIL.Disc_Amt as ItemDiscount,TSPL_TRANSFER_ORDER_DETAIL.Amount-TSPL_TRANSFER_ORDER_DETAIL.Disc_Amt as ItemAssAmt,case when ISNULL(TSPL_TRANSFER_ORDER_DETAIL .tax1,'') ='IGST' THEN TSPL_TRANSFER_ORDER_DETAIL.TAX1_Rate when ISNULL(TSPL_TRANSFER_ORDER_DETAIL .tax1,'') ='CGST' AND   ISNULL(TSPL_TRANSFER_ORDER_DETAIL .tax2,'') ='SGST'  THEN TSPL_TRANSFER_ORDER_DETAIL.TAX1_Rate+TSPL_TRANSFER_ORDER_DETAIL.TAX2_Rate  ELSE 0 end as ItemGstRt, case when TSPL_TRANSFER_ORDER_DETAIL .TAX1 ='SGST' AND TSPL_TRANSFER_ORDER_DETAIL .TAX2  ='CGST' then TSPL_TRANSFER_ORDER_DETAIL.TAX1_Amt when TSPL_TRANSFER_ORDER_DETAIL .TAX1 ='CGST' AND TSPL_TRANSFER_ORDER_DETAIL .TAX2  ='SGST' then TSPL_TRANSFER_ORDER_DETAIL.TAX2_Amt else 0 end ItemSgstAmt,case when TSPL_TRANSFER_ORDER_DETAIL .TAX1 ='IGST' then TSPL_TRANSFER_ORDER_DETAIL.TAX1_Amt else 0 end ItemIgstAmt,
case when TSPL_TRANSFER_ORDER_DETAIL .TAX1 ='CGST' AND TSPL_TRANSFER_ORDER_DETAIL .TAX2  ='SGST' then TSPL_TRANSFER_ORDER_DETAIL.TAX1_Amt
when TSPL_TRANSFER_ORDER_DETAIL .TAX1 ='SGST' AND TSPL_TRANSFER_ORDER_DETAIL .TAX2  ='CGST' then TSPL_TRANSFER_ORDER_DETAIL.TAX2_Amt 
else 0 end ItemCgstAmt,0 as ItemOthChrg,TSPL_TRANSFER_ORDER_DETAIL.item_net_amt-case when isnull(TSPL_TRANSFER_ORDER_DETAIL.tax2,'')='TCS' THEN  TSPL_TRANSFER_ORDER_DETAIL.TAX2_AMT when isnull(TSPL_TRANSFER_ORDER_DETAIL.tax3,'')='TCS' THEN  TSPL_TRANSFER_ORDER_DETAIL.TAX3_AMT ELSE 0 END as ItemTotItemVal,TSPL_TRANSFER_ORDER_HEAD .discount_base as ValDtlsAssVal,
case when TSPL_TRANSFER_ORDER_HEAD .TAX1 ='CGST' AND TSPL_TRANSFER_ORDER_HEAD .TAX2  ='SGST' then TSPL_TRANSFER_ORDER_HEAD.TAX1_Amt
when TSPL_TRANSFER_ORDER_HEAD .TAX1 ='SGST' AND TSPL_TRANSFER_ORDER_HEAD .TAX2  ='CGST' then TSPL_TRANSFER_ORDER_HEAD.TAX2_Amt 
else 0 end ValDtlsCgstVal, case when TSPL_TRANSFER_ORDER_HEAD .TAX1 ='SGST' AND TSPL_TRANSFER_ORDER_HEAD .TAX2  ='CGST' then TSPL_TRANSFER_ORDER_HEAD.TAX1_Amt when TSPL_TRANSFER_ORDER_HEAD .TAX1 ='CGST' AND TSPL_TRANSFER_ORDER_HEAD .TAX2  ='SGST' then TSPL_TRANSFER_ORDER_HEAD.TAX2_Amt else 0 end ValDtlsSgstVal,case when TSPL_TRANSFER_ORDER_HEAD .TAX1 ='IGST' then TSPL_TRANSFER_ORDER_HEAD.TAX1_Amt else 0 end ValDtlsIgstVal,TSPL_TRANSFER_ORDER_HEAD.Discount_Amt as ValDtlsDiscount,case when isnull(TSPL_TRANSFER_ORDER_HEAD.tax2,'')='TCS' THEN  TSPL_TRANSFER_ORDER_HEAD.TAX2_AMT when isnull(TSPL_TRANSFER_ORDER_HEAD.tax3,'')='TCS' THEN  TSPL_TRANSFER_ORDER_HEAD.TAX3_AMT ELSE 0 END as ValDtlsOthChrg,TSPL_TRANSFER_ORDER_HEAD.Doc_Total_amt  as ValDtlsTotInvVal,TSPL_TRANSFER_ORDER_HEAD.RoundOffAmount  as ValDtlsRndOffAmt
from TSPL_TRANSFER_ORDER_HEAD
Left Outer Join TSPL_COMPANY_MASTER  on TSPL_COMPANY_MASTER.Comp_Code  ='" & objCommonVar.CurrentCompanyCode & "'
left Outer Join TSPL_LOCATION_MASTER as ToLocation on ToLocation.GIT_Location  =TSPL_TRANSFER_ORDER_HEAD.To_Location  
left Outer Join TSPL_LOCATION_MASTER as FromLocation on FromLocation.Location_Code =TSPL_TRANSFER_ORDER_HEAD.From_Location    
left outer join TSPL_TRANSFER_ORDER_DETAIL on TSPL_TRANSFER_ORDER_DETAIL.Document_No =TSPL_TRANSFER_ORDER_HEAD.Document_No
left outer join tspl_item_master on tspl_item_master.Item_code=TSPL_TRANSFER_ORDER_DETAIL.Item_code
left outer join TSPL_STATE_MASTER as Seller_State_Master on Seller_State_Master.STATE_CODE  =FromLocation.State
left outer join TSPL_STATE_MASTER as Buyer_State_Master on Buyer_State_Master.STATE_CODE  =ToLocation.State
left outer join tspl_city_master on tspl_city_master.city_code=ToLocation.City_Code
left outer join tspl_city_master as BuyerCity on BuyerCity.city_code=FromLocation.City_Code
left outer join TSPL_TRANSFER_RETURN on TSPL_TRANSFER_RETURN.Transfer_No=TSPL_TRANSFER_ORDER_HEAD.Document_No
where TSPL_TRANSFER_RETURN.document_no  ='" & strDocNo & "' AND TSPL_TRANSFER_RETURN.document_type ='O' AND TSPL_TRANSFER_ORDER_HEAD.IsJobWorkType =0"

                Dim objResult As Object = ClsEInvoiceOFAPIs.PostAuthTokenNo_withInvoiceData(objCommonVar.CurrentCompanyCode, strtoken, strQry, strLocation, trans)
                If objResult IsNot Nothing Then
                    'assign to variable
                    Dim AckNo As String = objResult.SelectToken("AckNo").ToString
                    Dim AckDt As String = objResult.SelectToken("AckDt").ToString
                    Dim Irn As String = objResult.SelectToken("Irn").ToString
                    Dim SignedQRCode As String = objResult.SelectToken("SignedQRCode").ToString
                    clsDBFuncationality.ExecuteNonQuery("update TSPL_TRANSFER_RETURN set  IRN_No ='" & Irn & "',qr_code='" & SignedQRCode & "',ack_no='" & AckNo & "',ack_date='" & clsCommon.GetPrintDate(AckDt, "dd/MMM/yyyy hh:mm tt") & "' where TSPL_TRANSFER_RETURN.Document_No ='" & strDocNo & "'", trans)

                    Dim TempByte As Byte() = clsERPFuncationalityOLD.GenerateMyQCCode(SignedQRCode)
                    clsDBFuncationality.UpdateImage("BarCode_Img", TempByte, "TSPL_TRANSFER_RETURN", "TSPL_TRANSFER_RETURN.Document_No='" & strDocNo & "'", trans)
                Else
                    Return False
                End If
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    ''richa agarwal UDL/06/06/18-000183  create ConvertTrasfertoBatchItem function
    Private Shared Function ConvertTrasfertoBatchItem(ByVal strDocNo As String, ByVal strTransferReturnNo As String, ByVal trans As SqlTransaction) As Boolean
        Try
            ''Revese Batch Item
            Dim qry As String = "Select * from TSPL_BATCH_ITEM where  Document_Type='Transfer' and Document_Code='" + strDocNo + "' and In_Out_Type='O'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim Arr As List(Of clsBatchInventory) = Nothing
                If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                    For Each dr As DataRow In dt.Rows
                        Arr = New List(Of clsBatchInventory)
                        Dim objTr As clsBatchInventory = New clsBatchInventory()
                        objTr.Manual_BatchNo = clsCommon.myCstr(dr("Manual_BatchNo"))
                        objTr.Code = clsCommon.myCstr(dr("Code"))
                        objTr.Line_No = clsCommon.myCdbl(dr("Line_No"))
                        objTr.Parent_Line_No = clsCommon.myCdbl(dr("Parent_Line_No"))
                        objTr.Batch_No = clsCommon.myCstr(dr("Batch_No"))
                        objTr.Manufacture_Date = clsCommon.myCDate(dr("Manufacture_Date"))
                        objTr.Expiry_Date = clsCommon.myCDate(dr("Expiry_Date"))
                        objTr.MRP = clsCommon.myCdbl(dr("MRP"))
                        objTr.UOM = clsCommon.myCstr(dr("UOM"))
                        objTr.Qty = clsCommon.myCdbl(dr("Qty"))
                        objTr.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                        objTr.Document_Code = strTransferReturnNo
                        objTr.In_Out_Type = "I"
                        objTr.Location_Code = clsCommon.myCstr(dr("Location_Code"))
                        objTr.Document_Date = clsCommon.myCDate(dr("Document_Date"))
                        Arr.Add(objTr)
                        clsBatchInventory.SaveData("TRN-RET", strTransferReturnNo, objTr.Document_Date, objTr.In_Out_Type, objTr.Item_Code, objTr.Location_Code, objTr.Parent_Line_No, objTr.MRP, objTr.UOM, Arr, trans)
                    Next
                End If
            End If


            qry = "Select * from TSPL_BATCH_ITEM where  Document_Type='Transfer' and Document_Code='" + strDocNo + "' and In_Out_Type='I'"
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim Arr As List(Of clsBatchInventory) = Nothing
                If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                    For Each dr As DataRow In dt.Rows
                        Arr = New List(Of clsBatchInventory)
                        Dim objTr As clsBatchInventory = New clsBatchInventory()
                        objTr.Manual_BatchNo = clsCommon.myCstr(dr("Manual_BatchNo"))
                        objTr.Code = clsCommon.myCstr(dr("Code"))
                        objTr.Line_No = clsCommon.myCdbl(dr("Line_No"))
                        objTr.Parent_Line_No = clsCommon.myCdbl(dr("Parent_Line_No"))
                        objTr.Batch_No = clsCommon.myCstr(dr("Batch_No"))
                        objTr.Manufacture_Date = clsCommon.myCDate(dr("Manufacture_Date"))
                        objTr.Expiry_Date = clsCommon.myCDate(dr("Expiry_Date"))
                        objTr.MRP = clsCommon.myCdbl(dr("MRP"))
                        objTr.UOM = clsCommon.myCstr(dr("UOM"))
                        objTr.Qty = clsCommon.myCdbl(dr("Qty"))
                        objTr.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                        objTr.Document_Code = strTransferReturnNo
                        objTr.In_Out_Type = "O"
                        objTr.Location_Code = clsCommon.myCstr(dr("Location_Code"))
                        objTr.Document_Date = clsCommon.myCDate(dr("Document_Date"))
                        Arr.Add(objTr)
                        clsBatchInventory.SaveData("TRN-RET", strTransferReturnNo, objTr.Document_Date, objTr.In_Out_Type, objTr.Item_Code, objTr.Location_Code, objTr.Parent_Line_No, objTr.MRP, objTr.UOM, Arr, trans)
                    Next
                End If
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Private Shared Function AllowToSave(ByVal obj As clsTransferReturn, ByVal objSRN As clsTransferReturn) As Boolean
        Dim Qry As String = "select Document_No from TSPL_Transfer_RETURN where Transfer_No='" + obj.Transfer_No + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            Throw New Exception("Transfer Return No " + clsCommon.myCstr(clsDBFuncationality.getSingleValue(dt.Rows(0)("Document_No")) + " already created"))
        End If

        'Qry = "select distinct PI_No from TSPL_PI_DETAIL where SRN_Id ='" + obj.Transfer_No + "'"
        'If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
        '    Throw New Exception("Transfer_No is used in Purchase Invoice No " + clsCommon.myCstr(clsDBFuncationality.getSingleValue(dt.Rows(0)("PI_No")) + " can not return it."))
        'End If


        'If objSRN.Arr IsNot Nothing AndAlso objSRN.Arr.Count > 0 Then
        '    For Each objsrntr As clsSRNDetail In objSRN.Arr
        '        Dim bal As Double = clsItemLocationDetails.getBalanceWithUnapprove(objsrntr.Item_Code, objSRN.Bill_To_Location, objsrntr.MRP, objsrntr.Unit_code, obj.Document_No, obj.Document_Date)
        '        If bal < 0 Then
        '            Throw New Exception("Balance is going to -ve for item " + objsrntr.Item_Code)
        '        End If
        '    Next
        'End If
        Return True
    End Function


    Public Shared Function GetData(ByVal strDocNo As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsTransferReturn
        Dim obj As clsTransferReturn = Nothing
        Dim qry As String = "SELECT TSPL_Transfer_RETURN.* from TSPL_Transfer_RETURN  where 2=2"
        Dim whrCls As String = ""
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_Transfer_RETURN.Document_No = (select MIN(Document_No) from TSPL_Transfer_RETURN WHERE 1=1 " + whrCls + ")"
            Case NavigatorType.Last
                qry += " and TSPL_Transfer_RETURN.Document_No = (select Max(Document_No) from TSPL_Transfer_RETURN WHERE 1=1 " + whrCls + ")"
            Case NavigatorType.Current
                qry += " and TSPL_Transfer_RETURN.Document_No = '" + strDocNo + "'"
            Case NavigatorType.Next
                qry += " and TSPL_Transfer_RETURN.Document_No = (select Min(Document_No) from TSPL_Transfer_RETURN where Document_No>'" + strDocNo + "' " + whrCls + ")"
            Case NavigatorType.Previous
                qry += " and TSPL_Transfer_RETURN.Document_No = (select Max(Document_No) from TSPL_Transfer_RETURN where Document_No<'" + strDocNo + "' " + whrCls + ")"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsTransferReturn()
            obj.Document_No = clsCommon.myCstr(dt.Rows(0)("Document_No"))
            obj.Document_Date = clsCommon.myCDate(dt.Rows(0)("Document_Date"))
            obj.Transfer_No = clsCommon.myCstr(dt.Rows(0)("Transfer_No"))
            obj.Document_Type = clsCommon.myCstr(dt.Rows(0)("Document_Type"))
            obj.Remarks = clsCommon.myCstr(dt.Rows(0)("Remarks"))
            obj.Gate_ReturnNo = clsCommon.myCstr(dt.Rows(0)("Gate_ReturnNo"))
        End If

        Return obj
    End Function
End Class
