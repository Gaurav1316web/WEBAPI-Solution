'--------Created By Richa 
Imports System.Data.SqlClient
Imports common

Public Class ClsDispatchBulkSaleTradeReturn
#Region "Variable"
    Public Document_No As String = Nothing
    Public Document_Date As Date
    Public Location_Code As String = Nothing
    Public Location_Name As String = Nothing
    Public Customer_Code As String = Nothing
    Public Customer_Name As String = Nothing
    Public Price_Code As String = Nothing
    Public Posted As Integer = 0
    Public Posting_Date As Date?
    Public Total_Amt As Double = 0
    Public Tanker_No As String = Nothing
    Public Is_Create_Auto_Invoice As Integer = 0
    Public Against_SRN_No As String = Nothing
    Public DispatchTrade_No As String = String.Empty
    Public Against_SRN_No_Return As String = String.Empty
    Public EWayBillNo As String = Nothing
    Public EWayBillDate As Date?
    Public arrDispatchDetailTradeBulkSaleReturn As List(Of clsDispatchDetailTradeBulkSaleReturn) = Nothing
#End Region
    '----------------Code For Get Finder--------------------------------------------------------------------'
    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim qry As String = "Select TSPL_Dispatch_BulkSale_Trade_Return.Document_No As Code ,TSPL_Dispatch_BulkSale_Trade_Return.Document_Date as Date,TSPL_Dispatch_BulkSale_Trade_Return.Location_Code as [Location Code],TSPL_Dispatch_BulkSale_Trade_Return.Price_Code as [Price Chart Code],TSPL_Dispatch_BulkSale_Trade_Return.Tanker_No as [Tanker No],TSPL_Dispatch_BulkSale_Trade_Return.DispatchTrade_No as [Dispatch Doc No],TSPL_Dispatch_BulkSale_Trade_Return.Against_SRN_No_Return as [Against SRN No] from TSPL_Dispatch_BulkSale_Trade_Return "
        Return clsCommon.ShowSelectForm("DispatchBulkSale", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
    End Function
    Public Shared Function SaveData(ByVal obj As ClsDispatchBulkSaleTradeReturn, ByVal isNewEntry As Boolean) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            SaveData(obj, isNewEntry, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function SaveData(ByVal obj As ClsDispatchBulkSaleTradeReturn, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim qry As String = String.Empty
        Try
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleBulkSale, "", obj.Location_Code, obj.Document_Date, trans)
            qry = "delete from TSPL_Dispatch_Detail_BulkSale_Trade_Return where Document_No='" & obj.Document_No & "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            If isNewEntry Then
                obj.Document_No = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.DispatchBulkSaleTradeReturn, "", obj.Location_Code)
            End If
            Dim DateTime As String = clsFixedParameter.GetData(clsFixedParameterType.AllowToSaveTimeWithDocumentDate, clsFixedParameterCode.AllowToSaveTimeWithDocumentDate, trans)
            Dim coll As New Hashtable()
            If DateTime = "1" Then
                clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy hh:mm tt"))
            Else
                clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy"))
            End If

            clsCommon.AddColumnsForChange(coll, "Location_Code", obj.Location_Code)
            clsCommon.AddColumnsForChange(coll, "Customer_Code", obj.Customer_Code)
            clsCommon.AddColumnsForChange(coll, "Price_Code", obj.Price_Code)
            clsCommon.AddColumnsForChange(coll, "DispatchTrade_No", obj.DispatchTrade_No)
            clsCommon.AddColumnsForChange(coll, "Against_SRN_No", obj.Against_SRN_No)
            clsCommon.AddColumnsForChange(coll, "Total_Amt", obj.Total_Amt)
            clsCommon.AddColumnsForChange(coll, "Tanker_No", obj.Tanker_No)
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "Document_No", obj.Document_No)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Dispatch_BulkSale_Trade_Return", OMInsertOrUpdate.Insert, "", trans)
            Else
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Dispatch_BulkSale_Trade_Return", OMInsertOrUpdate.Update, "TSPL_Dispatch_BulkSale_Trade_Return.Document_No='" + obj.Document_No + "'", trans)
            End If
            clsDispatchDetailTradeBulkSaleReturn.saveData(obj.arrDispatchDetailTradeBulkSaleReturn, obj.Document_No, trans)


        Catch err As Exception
            Throw New Exception(err.Message)
        Finally
            obj = Nothing
        End Try
        Return True
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As ClsDispatchBulkSaleTradeReturn
        Return GetData(strCode, NavType, Nothing)
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As ClsDispatchBulkSaleTradeReturn
        Return GetData(strCode, NavType, trans, "")
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction, ByVal aLoc As String) As ClsDispatchBulkSaleTradeReturn
        Dim obj As ClsDispatchBulkSaleTradeReturn = Nothing
        Dim Arr As List(Of ClsDispatchBulkSaleTradeReturn) = Nothing
        Dim qry As String = "Select TSPL_Dispatch_BulkSale_Trade_Return.DispatchTrade_No ,TSPL_Dispatch_BulkSale_Trade_Return.Against_SRN_No_Return,TSPL_Dispatch_BulkSale_Trade_Return.EWayBillDate,TSPL_Dispatch_BulkSale_Trade_Return.EWayBillNo,TSPL_Dispatch_BulkSale_Trade_Return.Document_No,TSPL_Dispatch_BulkSale_Trade_Return.Document_Date,TSPL_Dispatch_BulkSale_Trade_Return.Against_SRN_No,TSPL_Dispatch_BulkSale_Trade_Return.Location_Code,TSPL_Dispatch_BulkSale_Trade_Return.Tanker_No,TSPL_Dispatch_BulkSale_Trade_Return.Price_Code,TSPL_Dispatch_BulkSale_Trade_Return.Total_Amt,TSPL_Dispatch_BulkSale_Trade_Return.Customer_Code,TSPL_Dispatch_BulkSale_Trade_Return.Posted,TSPL_LOCATION_MASTER.Location_Desc,TSPL_CUSTOMER_MASTER.Customer_Name,TSPL_Dispatch_BulkSale_Trade_Return.Is_Create_Auto_Invoice  from  TSPL_Dispatch_BulkSale_Trade_Return Left outer Join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_Dispatch_BulkSale_Trade_Return.Location_Code Left Outer Join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_Dispatch_BulkSale_Trade_Return.Customer_Code where 2=2  "
        Dim whrclas As String = ""
        If aLoc IsNot Nothing AndAlso clsCommon.myLen(aLoc) > 0 Then
            whrclas += " and TSPL_Dispatch_BulkSale_Trade_Return.Location_Code in (" & aLoc & ")"
        End If
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_Dispatch_BulkSale_Trade_Return.Document_No = (select MIN(Document_No) from TSPL_Dispatch_BulkSale_Trade_Return WHERE 1=1 " + whrclas + " )"
            Case NavigatorType.Last
                qry += " and TSPL_Dispatch_BulkSale_Trade_Return.Document_No = (select Max(Document_No) from TSPL_Dispatch_BulkSale_Trade_Return WHERE 1=1 " + whrclas + " )"
            Case NavigatorType.Current
                qry += " and TSPL_Dispatch_BulkSale_Trade_Return.Document_No='" + strCode + "' "
            Case NavigatorType.Next
                qry += " and TSPL_Dispatch_BulkSale_Trade_Return.Document_No = (select Min(Document_No) from TSPL_Dispatch_BulkSale_Trade_Return where Document_No>'" + strCode + "' " + whrclas + " )"
            Case NavigatorType.Previous
                qry += " and TSPL_Dispatch_BulkSale_Trade_Return.Document_No = (select Max(Document_No) from TSPL_Dispatch_BulkSale_Trade_Return where Document_No<'" + strCode + "' " + whrclas + " )"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New ClsDispatchBulkSaleTradeReturn()
            obj.Document_No = clsCommon.myCstr(dt.Rows(0)("Document_No"))
            obj.Document_Date = clsCommon.myCDate(dt.Rows(0)("Document_Date"))
            obj.Location_Code = clsCommon.myCstr(dt.Rows(0)("Location_Code"))
            obj.Customer_Code = clsCommon.myCstr(dt.Rows(0)("Customer_Code"))
            obj.Customer_Name = clsCommon.myCstr(dt.Rows(0)("Customer_Name"))
            obj.Price_Code = clsCommon.myCstr(dt.Rows(0)("Price_Code"))
            obj.EWayBillNo = clsCommon.myCstr(dt.Rows(0)("EWayBillNo"))
            obj.DispatchTrade_No = clsCommon.myCstr(dt.Rows(0)("DispatchTrade_No"))
            If dt.Rows(0)("EWayBillDate") IsNot DBNull.Value Then
                obj.EWayBillDate = clsCommon.myCDate(dt.Rows(0)("EWayBillDate"))
            End If

            obj.Location_Name = clsCommon.myCstr(dt.Rows(0)("Location_Desc"))
            obj.Posted = clsCommon.myCdbl(dt.Rows(0)("Posted"))
            obj.Total_Amt = clsCommon.myCdbl(dt.Rows(0)("Total_Amt"))
            obj.Against_SRN_No = clsCommon.myCstr(dt.Rows(0)("Against_SRN_No"))
            obj.Against_SRN_No_Return = clsCommon.myCstr(dt.Rows(0)("Against_SRN_No_Return"))
            obj.Tanker_No = clsCommon.myCstr(dt.Rows(0)("Tanker_No"))
            obj.arrDispatchDetailTradeBulkSaleReturn = clsDispatchDetailTradeBulkSaleReturn.getData(obj.Document_No, trans)
        End If
        Return obj
    End Function
    Public Shared Function PostData(ByVal FormId As String, ByVal strDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            PostData(FormId, strDocNo, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function PostData(ByVal FormId As String, ByVal strDocNo As String, ByVal trans As SqlTransaction) As Boolean

        Try
            Dim isSaved As Boolean = True
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Dispatch No not found to Post")
            End If
            Dim obj As ClsDispatchBulkSaleTradeReturn = ClsDispatchBulkSaleTradeReturn.GetData(strDocNo, NavigatorType.Current, trans)
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleBulkSale, "", obj.Location_Code, obj.Document_Date, trans)


            If (obj Is Nothing OrElse clsCommon.myLen(obj.Document_No) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If

          

            ''richa 08/08/2014 for INventory movement
            Dim ArrLocationDetails As List(Of clsItemLocationDetails) = New List(Of clsItemLocationDetails)()
            Dim ArrInventoryMovement As List(Of clsInventoryMovementNew) = New List(Of clsInventoryMovementNew)

            'Dim strFirstItemCodeNonItemRowType As String = GetFirstItemCode(obj.Arr)
            Dim strRgpNo As String = Nothing
            Dim intCounter As Integer = 0
            For Each objTr As clsDispatchDetailTradeBulkSaleReturn In obj.arrDispatchDetailTradeBulkSaleReturn
                intCounter = intCounter + 1
                ' If clsCommon.CompairString(objTr.Row_Type, clsItemRowType.RowTypeItem) = CompairStringResult.Equal Then
                Dim strItemType As String = clsItemMaster.GetItemType(objTr.Item_Code, trans)
                Dim strItemTypeToSave As String = ""
                If clsCommon.CompairString(strItemType, "R") = CompairStringResult.Equal Then
                    strItemTypeToSave = "RM"
                ElseIf clsCommon.CompairString(strItemType, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(strItemType, "O") = CompairStringResult.Equal Then
                    strItemTypeToSave = "OT"
                ElseIf clsCommon.CompairString(strItemType, "F") = CompairStringResult.Equal Then
                    strItemTypeToSave = "FT"
                Else
                    strItemTypeToSave = strItemType
                    'Throw New Exception("Item Type not found: " + strItemType)
                End If

                ''richa 17/09/2014
                'Dim DispatchTradeQty As String = ""
                'DispatchTradeQty = clsDBFuncationality.getSingleValue("Select Qty from TSPL_INVENTORY_MOVEMENT_NEW where Trans_Type='BulkSRNTradeReturn' and Source_Doc_No ='" & obj.Against_SRN_No & "' and Location_Code ='" & obj.Location_Code & "' and InOut='O'", trans)
                'If clsCommon.myCdbl(DispatchTradeQty) > 0 Then
                '    If objTr.Qty > DispatchTradeQty Then
                '        Throw New Exception("You cannot post this dispatch because stock qty is less than dispatch qty")
                '    End If
                'Else
                '    Throw New Exception("No stock quanity available for itemCode " & objTr.Item_Code & "")
                'End If

                Dim objLocationDetails As New clsItemLocationDetails()
              
                Dim ConvFac As Double = clsItemMaster.GetConvertionFactor(objTr.Item_Code, objTr.Unit_code, trans)
                If ConvFac = 0 Then
                    Throw New Exception("Conversion Factor found zero for item :" + objTr.Item_Code + " and Uom:'" + objTr.Unit_code)
                End If
                objLocationDetails.Item_Code = objTr.Item_Code
                objLocationDetails.Item_Desc = clsDBFuncationality.getSingleValue("Select Item_Desc from TSPL_ITEM_MASTER where Item_Code ='" + objTr.Item_Code + "' ", trans)
                objLocationDetails.Location_Code = obj.Location_Code
                objLocationDetails.Location_Desc = clsDBFuncationality.getSingleValue("Select Location_Desc  from TSPL_LOCATION_MASTER where Location_Code='" + obj.Location_Code + "' ", trans)
                'objLocationDetails.Item_Qty = -1 * objTr.Qty
                'objLocationDetails.Amount = -1 * objTr.Amount
                objLocationDetails.Item_Qty = 1 * objTr.Qty
                objLocationDetails.Amount = 1 * objTr.Amount
                objLocationDetails.MRP = 0 * ConvFac

                objLocationDetails.ItemType = strItemTypeToSave
                ArrLocationDetails.Add(objLocationDetails)


                Dim objInventoryMovemnt As New clsInventoryMovementNew()
                objInventoryMovemnt.InOut = "I"

                objInventoryMovemnt.Location_Code = obj.Location_Code
                objInventoryMovemnt.main_location = clsLocation.GetSegmentCode(obj.Location_Code, trans)

                objInventoryMovemnt.Cust_Code = obj.Customer_Code
                objInventoryMovemnt.Cust_Name = obj.Customer_Name

                objInventoryMovemnt.Item_Code = objTr.Item_Code
                objInventoryMovemnt.Item_Desc = clsDBFuncationality.getSingleValue("Select Item_Desc from TSPL_ITEM_MASTER where Item_Code ='" + objTr.Item_Code + "' ", trans)
                objInventoryMovemnt.Qty = objTr.Qty
                objInventoryMovemnt.UOM = objTr.Unit_code
                objInventoryMovemnt.MRP = objTr.Amount / objTr.Qty
                objInventoryMovemnt.Add_Cost = 0
                objInventoryMovemnt.FAT_Per = objTr.FatPer
                objInventoryMovemnt.SNF_Per = objTr.SNFPer
                objInventoryMovemnt.FAT_KG = objTr.Fat_KG
                objInventoryMovemnt.SNF_KG = objTr.SNF_KG
                objInventoryMovemnt.Net_Cost = objTr.Amount
                objInventoryMovemnt.Basic_Cost = objTr.Amount / objTr.Qty
                '' cost columns
                objInventoryMovemnt.Fat_Rate = objTr.FatRate
                objInventoryMovemnt.SNF_Rate = objTr.SNFRate
                objInventoryMovemnt.Fat_Amt = objTr.FatAmount
                objInventoryMovemnt.SNF_Amt = objTr.SNFAmount

                If clsCommon.CompairString(strItemType, "R") = CompairStringResult.Equal Then
                    objInventoryMovemnt.ItemType = "RM"
                ElseIf clsCommon.CompairString(strItemType, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(strItemType, "O") = CompairStringResult.Equal Then
                    objInventoryMovemnt.ItemType = "OT"
                ElseIf clsCommon.CompairString(strItemType, "F") = CompairStringResult.Equal Then
                    objInventoryMovemnt.ItemType = "FT"
                End If
                objInventoryMovemnt.ItemType = strItemTypeToSave
                ArrInventoryMovement.Add(objInventoryMovemnt)
            Next
            isSaved = isSaved AndAlso clsItemLocationDetails.SaveData(clsCommon.GetPrintDate(obj.Document_Date, "dd/MM/yyyy"), ArrLocationDetails, trans)
            isSaved = isSaved AndAlso clsInventoryMovementNew.SaveData("DispatchBSTrdReturn", obj.Document_No, obj.Document_Date, clsCommon.GetPrintDate(obj.Document_Date, "dd/MM/yyyy"), ArrInventoryMovement, trans)

            If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowPurchaseAccounting, clsFixedParameterCode.AllowPurchaseAccounting, trans)) = 0 Then
                CreateJournalEntry(obj.Document_No, trans)
            End If
            ''

            ''to save data into bulk milk srn return trade tables 
            Dim objSRN As clsBulkMilkSRNTradeReturn = clsBulkMilkSRNTradeReturn.GetData(obj.Against_SRN_No, NavigatorType.Current, trans)
            clsBulkMilkSRNTradeReturn.SaveData(objSRN, True, trans)
            clsBulkMilkSRNTradeReturn.PostData(objSRN.SRN_NO, "", trans)

            Dim StrSRN_Return_No As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select SRN_Return_NO from TSPL_Bulk_Milk_SRN_Return where SRN_NO='" & objSRN.SRN_NO & "'", trans))

            Dim qry = "Update TSPL_Dispatch_BulkSale_Trade_Return set Posted=1, " & _
            "Posting_Date='" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt") + "',Against_SRN_No_Return='" & StrSRN_Return_No & "' " & _
            " where Document_No='" + strDocNo + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
          
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    
    Public Shared Sub CreateJournalEntry(ByVal strCode As String, ByVal trans As SqlTransaction)
        Dim obj As New ClsDispatchBulkSaleTradeReturn
        obj = ClsDispatchBulkSaleTradeReturn.GetData(strCode, NavigatorType.Current, trans)
        Dim ArryLstGLAC As ArrayList = New ArrayList()
        Dim strInventoryControlAc As String = ""
        Dim strShipmentClearingAC As String = ""
        Dim dblTotalCost As Double = 0

        strShipmentClearingAC = clsDBFuncationality.getSingleValue("SELECT PA.Shipment_Clearing FROM TSPL_ITEM_MASTER AS IM INNER JOIN " & _
          " TSPL_PURCHASE_ACCOUNTS AS PA ON IM.Purchase_Class_Code = PA.Purchase_Class_Code INNER JOIN " & _
           " TSPL_GL_ACCOUNTS AS GLA ON PA.Inv_Control_Account = GLA.Account_Code WHERE IM.Item_Code='" + obj.arrDispatchDetailTradeBulkSaleReturn.Item(0).Item_Code + "'", trans)
        strShipmentClearingAC = clsERPFuncationality.ChangeGLAccountLocationSegment(strShipmentClearingAC, obj.Location_Code, trans)

        If clsCommon.myLen(strShipmentClearingAC) = 0 Then
            Throw New Exception("Please set Shipment clearing Account for first item")
        End If

        Dim dblCogsCost As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select sum(case when Costing_Method=0 then Avg_Cost when Costing_Method=1 then Avg_Cost when Costing_Method=2 then FIFO_Cost when Costing_Method=3 then LIFO_Cost end) as COst from TSPL_INVENTORY_MOVEMENT_NEW left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_INVENTORY_MOVEMENT_NEW.Item_Code left outer join TSPL_PURCHASE_ACCOUNTS on TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code=TSPL_ITEM_MASTER.Purchase_Class_Code where Source_Doc_No='" & obj.Document_No & "'", trans))

        'Dim Acc() As String = {strShipmentClearingAC, dblCogsCost}
        Dim Acc() As String = {strShipmentClearingAC, dblCogsCost * -1}
        ArryLstGLAC.Add(Acc)

        Dim strSql As String = "select TSPL_INVENTORY_MOVEMENT_NEW.Item_Code,case when Costing_Method=0 then Avg_Cost when Costing_Method=1 then Avg_Cost when Costing_Method=2 then FIFO_Cost when Costing_Method=3 then LIFO_Cost end as Cost from TSPL_INVENTORY_MOVEMENT_NEW left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_INVENTORY_MOVEMENT_NEW.Item_Code left outer join TSPL_PURCHASE_ACCOUNTS on TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code=TSPL_ITEM_MASTER.Purchase_Class_Code  where Source_Doc_No='" & obj.Document_No & "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(strSql, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            For Each dr As DataRow In dt.Rows
                strInventoryControlAc = clsDBFuncationality.getSingleValue("SELECT PA.Inv_Control_Account FROM TSPL_ITEM_MASTER AS IM INNER JOIN " & _
                " TSPL_PURCHASE_ACCOUNTS AS PA ON IM.Purchase_Class_Code = PA.Purchase_Class_Code INNER JOIN " & _
                " TSPL_GL_ACCOUNTS AS GLA ON PA.Inv_Control_Account = GLA.Account_Code WHERE IM.Item_Code='" + clsCommon.myCstr(dr("Item_Code")) + "'", trans)
                strInventoryControlAc = clsERPFuncationality.ChangeGLAccountLocationSegment(strInventoryControlAc, obj.Location_Code, trans)

                If clsCommon.myLen(strInventoryControlAc) = 0 Then
                    Throw New Exception("Please set Inventory Control Account for first item")
                End If
                'Dim Acc1() As String = {strInventoryControlAc, -1 * clsCommon.myCdbl(dr("Cost"))}
                Dim Acc1() As String = {strInventoryControlAc, 1 * clsCommon.myCdbl(dr("Cost"))}
                ArryLstGLAC.Add(Acc1)
            Next
        End If

        transportSql.FunGrnlEntryWithTrans(obj.Location_Code, False, trans, obj.Document_Date, "Journal Entry Against Dispatch Bulk Sale Trade Return for Document No " + obj.Document_No + " ", "DS-TR", "DISPATCH Bulk Sale Trade", obj.Document_No, "", "O", "", "", objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLstGLAC, , "", "")


    End Sub
    Public Shared Function DeleteData(ByVal strDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Dim isSaved As Boolean = False
        If (clsCommon.myLen(strDocNo) <= 0) Then
            Throw New Exception("Document No not found to Delete")
        End If
        Try
            Dim dt As DataTable = clsDBFuncationality.GetDataTable("select Document_Date,Location_Code from TSPL_Dispatch_BulkSale_Trade_Return where Document_No='" + strDocNo + "'", trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleBulkSale, "", clsCommon.myCstr(dt.Rows(0)("Location_Code")), clsCommon.myCDate(dt.Rows(0)("Document_Date")), trans)
            End If
            Dim qry As String = ""
            qry = "delete from TSPL_Dispatch_Detail_BulkSale_Trade_Return where Document_No='" + strDocNo + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "delete from TSPL_Dispatch_BulkSale_Trade_Return where Document_No='" + strDocNo + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try

        Return isSaved
    End Function
End Class


Public Class clsDispatchDetailTradeBulkSaleReturn
    Public Document_No As String = Nothing
    Public Item_Code As String = Nothing
    Public Unit_code As String = Nothing
    Public Qty As Double = 0
    Public FatPer As Double = 0
    Public SNFPer As Double = 0
    Public Fat_KG As Double = 0
    Public SNF_KG As Double = 0
    Public FatAmount As Double = 0
    Public SNFAmount As Double = 0
    Public HSN_code As String = Nothing
    Public Rate As Double = 0
    Public Amount As Double = 0
    Public FatRate As Double = 0
    Public SNFRate As Double = 0
    Public ItemDesc As String = Nothing
    Public StandardRate As Double = 0
    Public Shared Function saveData(ByVal arrObj As List(Of clsDispatchDetailTradeBulkSaleReturn), ByVal strDocNo As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim issaved As Boolean = True
            Dim coll As Hashtable

            If arrObj IsNot Nothing Then
                For Each obj As clsDispatchDetailTradeBulkSaleReturn In arrObj
                    coll = New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Document_No", strDocNo)
                    clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code)
                    clsCommon.AddColumnsForChange(coll, "Unit_code", obj.Unit_code)
                    clsCommon.AddColumnsForChange(coll, "Qty", obj.Qty)
                    clsCommon.AddColumnsForChange(coll, "FatPer", obj.FatPer)
                    clsCommon.AddColumnsForChange(coll, "SNFPer", obj.SNFPer)
                    clsCommon.AddColumnsForChange(coll, "FatRate", obj.FatRate)
                    clsCommon.AddColumnsForChange(coll, "SNFRate", obj.SNFRate)
                    clsCommon.AddColumnsForChange(coll, "Fat_KG", obj.Fat_KG)
                    clsCommon.AddColumnsForChange(coll, "SNF_KG", obj.SNF_KG)
                    clsCommon.AddColumnsForChange(coll, "FatAmount", obj.FatAmount)
                    clsCommon.AddColumnsForChange(coll, "Rate", obj.Rate)
                    clsCommon.AddColumnsForChange(coll, "SNFAmount", obj.SNFAmount)
                    clsCommon.AddColumnsForChange(coll, "Amount", obj.Amount)
                    clsCommon.AddColumnsForChange(coll, "StandardRate", obj.StandardRate)
                    issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Dispatch_Detail_BulkSale_Trade_Return", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If
            Return issaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            arrObj = Nothing
        End Try
    End Function
    Public Shared Function getData(ByVal strdocNo As String, ByVal trans As SqlTransaction) As List(Of clsDispatchDetailTradeBulkSaleReturn)
        Try
            Dim arrObj As List(Of clsDispatchDetailTradeBulkSaleReturn) = Nothing
            Dim obj As clsDispatchDetailTradeBulkSaleReturn = Nothing
            Dim qry As String = "Select TSPL_Dispatch_Detail_BulkSale_Trade_Return.Document_No,TSPL_Dispatch_Detail_BulkSale_Trade_Return.Item_Code,TSPL_ITEM_MASTER.Item_Desc,TSPL_Dispatch_Detail_BulkSale_Trade_Return.Qty,TSPL_Dispatch_Detail_BulkSale_Trade_Return.FatPer,TSPL_Dispatch_Detail_BulkSale_Trade_Return.SNFPer,TSPL_Dispatch_Detail_BulkSale_Trade_Return.Fat_KG,TSPL_Dispatch_Detail_BulkSale_Trade_Return.SNF_KG,TSPL_Dispatch_Detail_BulkSale_Trade_Return.FatAmount,TSPL_Dispatch_Detail_BulkSale_Trade_Return.SNFAmount,TSPL_Dispatch_Detail_BulkSale_Trade_Return.Amount,TSPL_Dispatch_Detail_BulkSale_Trade_Return.Rate,TSPL_Dispatch_Detail_BulkSale_Trade_Return.FatRate,TSPL_Dispatch_Detail_BulkSale_Trade_Return.SNFRate,TSPL_Dispatch_Detail_BulkSale_Trade_Return.Unit_Code,TSPL_Dispatch_Detail_BulkSale_Trade_Return.StandardRate from TSPL_Dispatch_Detail_BulkSale_Trade_Return Left Outer Join TSPL_ITEM_MAster on TSPL_ITEM_MASTER.Item_Code=TSPL_Dispatch_Detail_BulkSale_Trade_Return.Item_Code where TSPL_Dispatch_Detail_BulkSale_Trade_Return.Document_No='" & strdocNo & "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                arrObj = New List(Of clsDispatchDetailTradeBulkSaleReturn)
                For i As Integer = 0 To dt.Rows.Count - 1
                    obj = New clsDispatchDetailTradeBulkSaleReturn()
                    obj.Document_No = clsCommon.myCstr(dt.Rows(i)("Document_No"))
                    obj.Item_Code = clsCommon.myCstr(dt.Rows(i)("Item_Code"))
                    obj.Qty = clsCommon.myCdbl(dt.Rows(i)("Qty"))
                    obj.FatPer = clsCommon.myCdbl(dt.Rows(i)("FatPer"))
                    obj.SNFPer = clsCommon.myCdbl(dt.Rows(i)("SNFPer"))
                    obj.Unit_code = clsCommon.myCstr(dt.Rows(0)("Unit_code"))
                    obj.Fat_KG = clsCommon.myCdbl(dt.Rows(i)("Fat_KG"))
                    obj.SNF_KG = clsCommon.myCdbl(dt.Rows(i)("SNF_KG"))
                    obj.FatAmount = clsCommon.myCdbl(dt.Rows(i)("FatAmount"))
                    obj.SNFAmount = clsCommon.myCdbl(dt.Rows(i)("SNFAmount"))
                    obj.Amount = clsCommon.myCdbl(dt.Rows(i)("Amount"))
                    obj.FatRate = clsCommon.myCdbl(dt.Rows(i)("FatRate"))
                    obj.SNFRate = clsCommon.myCdbl(dt.Rows(i)("SNFRate"))
                    obj.ItemDesc = clsCommon.myCstr(dt.Rows(i)("Item_Desc"))
                    obj.Rate = clsCommon.myCdbl(dt.Rows(i)("Rate"))
                    obj.StandardRate = clsCommon.myCdbl(dt.Rows(i)("StandardRate"))
                    obj.HSN_code = clsItemMaster.GetItemHSNCode(clsCommon.myCstr(dt.Rows(i)("Item_Code")), trans)
                    arrObj.Add(obj)
                Next
            End If
            Return arrObj
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

End Class


''richa 17/09/2014
Public Class clsBulkMilkSRNTradeReturn
    Public SRN_NO As String = String.Empty
    Public SRN_Return_Date As Date = Nothing
    Public Weighment_No As String = String.Empty
    Public Weighment_Date As Date = Nothing
    Public QC_No As String = String.Empty
    Public Qc_Date As Date = Nothing
    Public Vendor_Code As String = String.Empty
    Public Loc_Code As String = String.Empty
    Public sub_location As String = String.Empty
    Public Challan_No As String = String.Empty
    Public Challan_Date As Date = Nothing
    Public Tanker_No As String = String.Empty
    Public Price_Code As String = String.Empty
    Public isPosted As Integer = 0
    Public Posting_Date As Date = Nothing
    Public Item_Code As String = String.Empty
    Public Item_Desc As String = String.Empty
    Public UOM As String = String.Empty
    Public Gross_Weight As Double = 0
    Public Tare_Weight As Double = 0
    Public Net_Weight As Double = 0
    Public snf_Per As Double = 0
    Public fat_per As Double = 0
    Public fat_KG As Double = 0
    Public SNF_KG As Double = 0
    Public fat_Rate As Double = 0
    Public SNF_Rate As Double = 0
    ''richa agarwal 07/07/2015
    Public fat_amount As Double = 0
    Public SNF_Amount As Double = 0
    Public HSN_code As String = String.Empty
    ''-----------------
    Public Amount As Double = 0
    Public Deduction As Double = 0
    Public Incentive As Double = 0
    Public Actual_Amount As Double = 0
    Public SpecialDeduction As Double = 0
    Public Created_By As String = String.Empty
    Public Created_Date As String = String.Empty
    Public Modify_By As String = String.Empty
    Public Modify_Date As String = String.Empty
    Public comp_code As String = String.Empty
    Public isNewEntry As Boolean = False
    Public Gate_Entry_No As String = String.Empty
    Public StandardRate As Double = 0


    Public SRN_Return_NO As String = String.Empty

    Public Shared Function PostData(ByVal FormId As String, ByVal strDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            PostData(FormId, strDocNo, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function postData(ByVal StrDocNo As String, ByVal formId As String, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Dim isPosted As Boolean = True
        Try
            If (clsCommon.myLen(StrDocNo) <= 0) Then
                Throw New Exception(" Doc No not found to Post")
            End If

            Dim obj As clsBulkMilkSRNTradeReturn = clsBulkMilkSRNTradeReturn.GetData(StrDocNo, NavigatorType.Current, trans)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.SRN_NO) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            ' trans = clsDBFuncationality.GetTransactin()
            Dim SRN_Return_Date As Date = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select SRN_Return_Date from TSPL_Bulk_Milk_SRN_Return where SRN_NO='" & obj.SRN_NO & "'", trans))
            'clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleBulkSale, clsUserMgtCode.FrmDispatchBulkSaleTradeReturn, obj.Loc_Code, SRN_Return_Date, trans)

            'If (obj.isPosted = 1) Then
            '    Throw New Exception("Already Post on :" + obj.Posting_Date)
            'End If

            Dim qry As String = ""
            Dim ArrLocationDetails As List(Of clsItemLocationDetails) = New List(Of clsItemLocationDetails)()
            Dim ArrInventoryMovement As List(Of clsInventoryMovementNew) = New List(Of clsInventoryMovementNew)
            Dim strItemType As String = clsItemMaster.GetItemType(obj.Item_Code, trans)
            Dim strItemTypeToSave As String = ""
            If clsCommon.CompairString(strItemType, "R") = CompairStringResult.Equal Then
                strItemTypeToSave = "RM"
            ElseIf clsCommon.CompairString(strItemType, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(strItemType, "O") = CompairStringResult.Equal Then
                strItemTypeToSave = "OT"
            ElseIf clsCommon.CompairString(strItemType, "F") = CompairStringResult.Equal Then
                strItemTypeToSave = "FT"
            ElseIf clsCommon.CompairString(strItemType, "A") = CompairStringResult.Equal Then
                strItemTypeToSave = "A"
            Else
                strItemTypeToSave = strItemType
            End If
            Dim strItemUnitCode As String = clsItemMaster.GetStockUnit(obj.Item_Code, trans)

            Dim objLocationDetails As New clsItemLocationDetails()


            Dim ConvFac As Double = clsItemMaster.GetConvertionFactor(obj.Item_Code, strItemUnitCode, trans)
            If ConvFac = 0 Then
                Throw New Exception("Conversion Factor found zero for item :" + obj.Item_Code + " and Uom:'" + strItemUnitCode)
            End If

            objLocationDetails.Item_Code = obj.Item_Code
            objLocationDetails.Item_Desc = obj.Item_Desc
            objLocationDetails.Location_Code = obj.sub_location
            objLocationDetails.Location_Desc = clsLocation.GetName(obj.sub_location, trans)
            objLocationDetails.Item_Qty = obj.Net_Weight * -1
            objLocationDetails.Amount = obj.Actual_Amount * -1
            objLocationDetails.MRP = 0
            objLocationDetails.ItemType = strItemTypeToSave
            ArrLocationDetails.Add(objLocationDetails)

            Dim objInventoryMovemnt As New clsInventoryMovementNew()
            objInventoryMovemnt.InOut = "O"
          
            objInventoryMovemnt.Location_Code = obj.sub_location

            objInventoryMovemnt.Vendor_Code = obj.Vendor_Code
            objInventoryMovemnt.Vendor_Name = clsVendorMaster.GetName(obj.Vendor_Code, trans)

            objInventoryMovemnt.Item_Code = obj.Item_Code
            objInventoryMovemnt.Item_Desc = obj.Item_Desc
            objInventoryMovemnt.Qty = obj.Net_Weight
            objInventoryMovemnt.UOM = strItemUnitCode
            objInventoryMovemnt.MRP = 0
            objInventoryMovemnt.Add_Cost = 0
            objInventoryMovemnt.FAT_Per = obj.fat_per
            objInventoryMovemnt.SNF_Per = obj.snf_Per
            objInventoryMovemnt.FAT_KG = obj.fat_KG
            objInventoryMovemnt.SNF_KG = obj.SNF_KG
            objInventoryMovemnt.Net_Cost = obj.Actual_Amount
            objInventoryMovemnt.main_location = obj.Loc_Code

            objInventoryMovemnt.Fat_Rate = obj.fat_Rate
            objInventoryMovemnt.SNF_Rate = obj.SNF_Rate
            objInventoryMovemnt.Fat_Amt = obj.fat_amount
            objInventoryMovemnt.SNF_Amt = obj.SNF_Amount
            ''---------------------
            If clsCommon.CompairString(strItemType, "R") = CompairStringResult.Equal Then
                objInventoryMovemnt.ItemType = "RM"
            ElseIf clsCommon.CompairString(strItemType, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(strItemType, "O") = CompairStringResult.Equal Then
                objInventoryMovemnt.ItemType = "OT"
            ElseIf clsCommon.CompairString(strItemType, "F") = CompairStringResult.Equal Then
                objInventoryMovemnt.ItemType = "FT"
            End If
            objInventoryMovemnt.ItemType = strItemTypeToSave
            objInventoryMovemnt.Basic_Cost = obj.Actual_Amount / obj.Net_Weight
            ArrInventoryMovement.Add(objInventoryMovemnt)
            isSaved = isSaved AndAlso clsItemLocationDetails.SaveData(clsCommon.GetPrintDate(SRN_Return_Date, "dd/MM/yyyy"), ArrLocationDetails, trans)
            Dim StrSRN_Return_No As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select SRN_Return_NO from TSPL_Bulk_Milk_SRN_Return where SRN_NO='" & obj.SRN_NO & "'", trans))
            isSaved = isSaved AndAlso clsInventoryMovementNew.SaveData("BulkSRNTradeReturn", StrSRN_Return_No, SRN_Return_Date, clsCommon.GetPrintDate(SRN_Return_Date, "dd/MM/yyyy"), ArrInventoryMovement, trans)
            'Create GL Entry
            If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowPurchaseAccounting, clsFixedParameterCode.AllowPurchaseAccounting, trans)) = 0 Then
                ' qry = " select TSPL_PURCHASE_ACCOUNTS.Inv_Control_Account,TSPL_PURCHASE_ACCOUNTS.Inv_Payable_Clearing ,TSPL_Bulk_Milk_SRN_Return.Actual_Amount,TSPL_Bulk_Milk_SRN_Return.Loc_Code   from TSPL_PURCHASE_ACCOUNTS left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Purchase_Class_Code =TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code left outer join TSPL_Bulk_Milk_SRN_Return on TSPL_Bulk_Milk_SRN_Return .Item_Code=TSPL_ITEM_MASTER.Item_Code where TSPL_Bulk_Milk_SRN_Return.SRN_Return_NO='" & obj.SRN_NO & "'  and TSPL_Bulk_Milk_SRN_Return.FormType='Bulk Milk SRN Trade' "
                qry = " select TSPL_PURCHASE_ACCOUNTS.Inv_Control_Account,TSPL_PURCHASE_ACCOUNTS.Inv_Payable_Clearing ,TSPL_Bulk_Milk_SRN.Actual_Amount,TSPL_Bulk_Milk_SRN.Loc_Code   from TSPL_PURCHASE_ACCOUNTS left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Purchase_Class_Code =TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code left outer join TSPL_Bulk_Milk_SRN on TSPL_Bulk_Milk_SRN .Item_Code=TSPL_ITEM_MASTER.Item_Code where TSPL_Bulk_Milk_SRN.SRN_NO='" & obj.SRN_NO & "'  and TSPL_Bulk_Milk_SRN.FormType='Bulk Milk SRN Trade' "
                Dim ArryLst As ArrayList = New ArrayList()
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    Dim strInvCntrlAc As String = dt.Rows(0)("Inv_Control_Account")
                    Dim strPaybleClrAc As String = dt.Rows(0)("Inv_Payable_Clearing")
                    strInvCntrlAc = clsERPFuncationality.ChangeGLAccountLocationSegment(strInvCntrlAc, dt.Rows(0)("Loc_Code"), trans)
                    strPaybleClrAc = clsERPFuncationality.ChangeGLAccountLocationSegment(strPaybleClrAc, dt.Rows(0)("Loc_Code"), trans)
                    'ArryLst.Add(New String() {strInvCntrlAc, dt.Rows(0)("Actual_Amount")})
                    'ArryLst.Add(New String() {strPaybleClrAc, dt.Rows(0)("Actual_Amount") * -1})
                    ArryLst.Add(New String() {strInvCntrlAc, dt.Rows(0)("Actual_Amount") * -1})
                    ArryLst.Add(New String() {strPaybleClrAc, dt.Rows(0)("Actual_Amount")})
                    transportSql.FunGrnlEntryWithTrans(obj.Loc_Code, False, trans, clsCommon.GetPrintDate(SRN_Return_Date, "dd/MMM/yyyy"), " GL Entry Against Bulk Milk SRN Trade Return No  -" + StrSRN_Return_No + "", "BS-TR", "Bulk Milk SRN Trade Return", StrSRN_Return_No, "", "C", obj.Item_Code, obj.Item_Desc, objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLst)
                End If
            End If

            Dim strQry As String = " update TSPL_Bulk_Milk_SRN_Return set isPosted='1', Posting_Date='" & clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy") & "' where SRN_Return_NO='" & StrSRN_Return_No & "' and FormType='Bulk Milk SRN Trade Return'"
            isPosted = isPosted AndAlso clsDBFuncationality.ExecuteNonQuery(strQry, trans)

            strQry = " update tspl_bulk_milk_srn set SRN_Return_NO='" & StrSRN_Return_No & "' where srn_no='" & obj.SRN_NO & "' "
            isPosted = isPosted AndAlso clsDBFuncationality.ExecuteNonQuery(strQry, trans)
          
            If isPosted Then
                '  trans.Commit()
            Else
                '  trans.Rollback()
            End If

        Catch ex As Exception
            ' trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return isPosted
    End Function


    Public Shared Function DeleteData(ByVal strDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Dim isSaved As Boolean = False
        If (clsCommon.myLen(strDocNo) <= 0) Then
            Throw New Exception("Document No not found to Delete")
        End If
        Try
            Dim qry As String = ""
            qry = "delete from TSPL_Bulk_Milk_SRN_Return where srn_No='" & strDocNo & "' and FormType='Bulk Milk SRN Trade' "
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try

        Return isSaved
    End Function
    Public Shared Function SaveData(ByVal obj As clsBulkMilkSRNTradeReturn, ByVal isNewEntry As Boolean) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            SaveData(obj, isNewEntry, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function saveData(ByVal obj As clsBulkMilkSRNTradeReturn, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        ' Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            ' Dim loccode As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Loc_Code from TSPL_Bulk_Milk_SRN where SRN_NO ='" & obj.SRN_NO & "'"))
            If isNewEntry Then
                obj.SRN_Return_NO = clsERPFuncationality.GetNextCode(trans, obj.SRN_Return_Date, clsDocType.BulkMilkSRNTradeReturn, "", obj.Loc_Code)
            End If

            Dim coll As New Hashtable()
            If clsCommon.myLen(obj.SRN_Return_Date) > 0 Then
                clsCommon.AddColumnsForChange(coll, "SRN_Return_Date", clsCommon.GetPrintDate(obj.SRN_Return_Date, "dd/MMM/yyyy hh:mm tt"), True)
            End If
            clsCommon.AddColumnsForChange(coll, "SRN_NO", clsCommon.myCstr(obj.SRN_NO), True)

            clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "FormType", "Bulk Milk SRN Trade Return")
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "SRN_Return_NO", obj.SRN_Return_NO)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Bulk_Milk_SRN_Return", OMInsertOrUpdate.Insert, "", trans)
            Else
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Bulk_Milk_SRN_Return", OMInsertOrUpdate.Update, "TSPL_Bulk_Milk_SRN_Return.SRN_Return_NO='" + obj.SRN_Return_NO + "'  and TSPL_Bulk_Milk_SRN_Return.FormType='Bulk Milk SRN Trade Return'", trans)
            End If

            'clsDBFuncationality.ExecuteNonQuery("Update TSPL_Dispatch_BulkSale_Trade_Return set Against_SRN_No_Return='" + obj.SRN_Return_NO + "' where Document_No ='" + obj.Challan_No + "'", trans)
            ' trans.Commit()
        Catch ex As Exception
            ' trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsBulkMilkSRNTradeReturn
        Return GetData(strCode, NavType, Nothing)
    End Function

    Public Shared Function getData(ByVal strCode As String, ByVal navtype As NavigatorType, ByVal trans As SqlTransaction) As clsBulkMilkSRNTradeReturn
        Dim obj As New clsBulkMilkSRNTradeReturn
        Try
            Dim qst As String = " select *   From TSPL_Bulk_Milk_SRN   where 1=1 and FormType='Bulk Milk SRN Trade'   "

            Dim whrCls As String = String.Empty
            If Not clsMccMaster.isCurrentUserHO(trans) Then
                If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                    whrCls = "and Loc_code in (" & objCommonVar.strCurrUserLocations & ")"
                End If
            End If
            qst = qst & whrCls
            Select Case navtype
                Case NavigatorType.Current
                    qst += " and TSPL_Bulk_Milk_SRN.srn_no in ('" + strCode + "') "
                Case NavigatorType.Next
                    qst += " and TSPL_Bulk_Milk_SRN.srn_no in (select min(srn_no ) from TSPL_Bulk_Milk_SRN where srn_no  >'" + strCode + "' and FormType='Bulk Milk SRN Trade' " & whrCls & "  )"
                Case NavigatorType.First
                    qst += " and TSPL_Bulk_Milk_SRN.srn_no in (select MIN(srn_no ) from TSPL_Bulk_Milk_SRN  where 1=1 and FormType='Bulk Milk SRN Trade' " & whrCls & "  )"
                Case NavigatorType.Last
                    qst += " and TSPL_Bulk_Milk_SRN.srn_no in (select Max(srn_no ) from TSPL_Bulk_Milk_SRN  where 1=1 and FormType='Bulk Milk SRN Trade' " & whrCls & "  )"
                Case NavigatorType.Previous
                    qst += " and TSPL_Bulk_Milk_SRN.srn_no in (select Max(srn_no ) from TSPL_Bulk_Milk_SRN where srn_No  <'" + strCode + "'  and FormType='Bulk Milk SRN Trade' " & whrCls & "  )"
            End Select
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qst, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                obj.SRN_NO = clsCommon.myCstr(dt.Rows(0)("SRN_NO"))
                obj.SRN_Return_Date = clsCommon.myCDate(dt.Rows(0)("srn_date"))
                obj.Tanker_No = clsCommon.myCstr(dt.Rows(0)("Tanker_No"))
                obj.isPosted = clsCommon.myCdbl(dt.Rows(0)("isPosted"))
                If obj.isPosted = 1 Then
                    obj.Posting_Date = clsCommon.myCDate(dt.Rows(0)("Posting_Date"))
                End If
                obj.Vendor_Code = clsCommon.myCstr(dt.Rows(0)("Vendor_Code"))
                obj.Loc_Code = clsCommon.myCstr(dt.Rows(0)("Loc_code"))
                obj.sub_location = clsCommon.myCstr(dt.Rows(0)("sub_location"))
                obj.Challan_No = clsCommon.myCstr(dt.Rows(0)("challan_no"))
                obj.Challan_Date = clsCommon.myCDate(dt.Rows(0)("Challan_Date"))
                obj.Price_Code = clsCommon.myCstr(dt.Rows(0)("Price_Code"))
                obj.Item_Code = clsCommon.myCstr(dt.Rows(0)("item_code"))
                obj.Item_Desc = clsCommon.myCstr(dt.Rows(0)("Item_Desc"))
                obj.UOM = clsCommon.myCstr(dt.Rows(0)("uom"))
                obj.Gross_Weight = clsCommon.myCdbl(dt.Rows(0)("Gross_Weight"))
                obj.Tare_Weight = clsCommon.myCdbl(dt.Rows(0)("Tare_Weight"))
                obj.Net_Weight = clsCommon.myCdbl(dt.Rows(0)("Net_Weight"))
                obj.fat_per = clsCommon.myCdbl(dt.Rows(0)("fat_per"))
                obj.snf_Per = clsCommon.myCdbl(dt.Rows(0)("snf_Per"))
                obj.fat_KG = clsCommon.myCdbl(dt.Rows(0)("fat_KG"))
                obj.SNF_KG = clsCommon.myCdbl(dt.Rows(0)("SNF_KG"))
                obj.fat_Rate = clsCommon.myCdbl(dt.Rows(0)("fat_Rate"))
                obj.SNF_Rate = clsCommon.myCdbl(dt.Rows(0)("SNF_Rate"))
                obj.Amount = clsCommon.myCdbl(dt.Rows(0)("Amount"))
                obj.Deduction = clsCommon.myCdbl(dt.Rows(0)("Deduction"))
                obj.Incentive = clsCommon.myCdbl(dt.Rows(0)("Incentive"))
                obj.Actual_Amount = clsCommon.myCdbl(dt.Rows(0)("Actual_Amount"))
                obj.SpecialDeduction = clsCommon.myCdbl(dt.Rows(0)("SpecialDeduction"))
                obj.StandardRate = clsCommon.myCdbl(dt.Rows(0)("StandardRate"))
                obj.fat_amount = clsCommon.myCdbl(dt.Rows(0)("FatAmt"))
                obj.SNF_Amount = clsCommon.myCdbl(dt.Rows(0)("SnfAmt"))
                obj.HSN_code = clsItemMaster.GetItemHSNCode(clsCommon.myCstr(dt.Rows(0)("item_code")), trans)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
        Return obj
    End Function



End Class




''==================================



