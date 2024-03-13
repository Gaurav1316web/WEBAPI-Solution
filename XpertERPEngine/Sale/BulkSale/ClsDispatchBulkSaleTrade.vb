'--------Created By Richa 16/09/2014 Against Ticket No BM00000003892
Imports System.Data.SqlClient
Imports common

Public Class ClsDispatchBulkSaleTrade
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
    Public EWayBillNo As String = Nothing
    Public EWayBillDate As Date?
    Public arrDispatchDetailTradeBulkSale As List(Of clsDispatchDetailTradeBulkSale) = Nothing
#End Region
    '----------------Code For Get Finder--------------------------------------------------------------------'
    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim qry As String = "Select TSPL_Dispatch_BulkSale_Trade.Document_No As Code ,TSPL_Dispatch_BulkSale_Trade.Document_Date as Date,TSPL_Dispatch_BulkSale_Trade.Location_Code as [Location Code],TSPL_Dispatch_BulkSale_Trade.Price_Code as [Price Chart Code],TSPL_Dispatch_BulkSale_Trade.Tanker_No as [Tanker No],TSPL_Dispatch_BulkSale_Trade.Against_SRN_No as [Against SRN No] from TSPL_Dispatch_BulkSale_Trade "
        Return clsCommon.ShowSelectForm("DispatchBulkSale", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
    End Function
    Public Shared Function SaveData(ByVal obj As ClsDispatchBulkSaleTrade, ByVal isNewEntry As Boolean) As Boolean
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
    Public Shared Function SaveData(ByVal obj As ClsDispatchBulkSaleTrade, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim qry As String = String.Empty
        ' Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleBulkSale, "", obj.Location_Code, obj.Document_Date, trans)
            qry = "delete from TSPL_Dispatch_Detail_BulkSale_Trade where Document_No='" & obj.Document_No & "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            If isNewEntry Then
                obj.Document_No = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.DispatchBulkSaleTrade, "", obj.Location_Code)
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
            clsCommon.AddColumnsForChange(coll, "Total_Amt", obj.Total_Amt)
            clsCommon.AddColumnsForChange(coll, "Is_Create_Auto_Invoice", obj.Is_Create_Auto_Invoice)
            clsCommon.AddColumnsForChange(coll, "Tanker_No", obj.Tanker_No)
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "Document_No", obj.Document_No)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Dispatch_BulkSale_Trade", OMInsertOrUpdate.Insert, "", trans)
            Else
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Dispatch_BulkSale_Trade", OMInsertOrUpdate.Update, "TSPL_Dispatch_BulkSale_Trade.Document_No='" + obj.Document_No + "'", trans)
            End If
            clsDispatchDetailTradeBulkSale.saveData(obj.arrDispatchDetailTradeBulkSale, obj.Document_No, trans)

            ' trans.Commit()
        Catch err As Exception
            ' trans.Rollback()
            Throw New Exception(err.Message)
        Finally
            obj = Nothing
        End Try
        Return True
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As ClsDispatchBulkSaleTrade
        Return GetData(strCode, NavType, Nothing)
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As ClsDispatchBulkSaleTrade
        Return GetData(strCode, NavType, trans, "")
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction, ByVal aLoc As String) As ClsDispatchBulkSaleTrade
        Dim obj As ClsDispatchBulkSaleTrade = Nothing
        Dim Arr As List(Of ClsDispatchBulkSaleTrade) = Nothing
        Dim qry As String = "Select TSPL_Dispatch_BulkSale_Trade.EWayBillDate,TSPL_Dispatch_BulkSale_Trade.EWayBillNo,TSPL_Dispatch_BulkSale_Trade.Document_No,TSPL_Dispatch_BulkSale_Trade.Document_Date,TSPL_Dispatch_BulkSale_Trade.Against_SRN_No,TSPL_Dispatch_BulkSale_Trade.Location_Code,TSPL_Dispatch_BulkSale_Trade.Tanker_No,TSPL_Dispatch_BulkSale_Trade.Price_Code,TSPL_Dispatch_BulkSale_Trade.Total_Amt,TSPL_Dispatch_BulkSale_Trade.Customer_Code,TSPL_Dispatch_BulkSale_Trade.Posted,TSPL_LOCATION_MASTER.Location_Desc,TSPL_CUSTOMER_MASTER.Customer_Name,TSPL_DISPATCH_BULKSALE_Trade.Is_Create_Auto_Invoice  from  TSPL_DISPATCH_BULKSALE_Trade Left outer Join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_Dispatch_BulkSale_Trade.Location_Code Left Outer Join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_Dispatch_BulkSale_Trade.Customer_Code where 2=2  "
        Dim whrclas As String = ""
        ''richa 04/05/2015
        If aLoc IsNot Nothing AndAlso clsCommon.myLen(aLoc) > 0 Then
            whrclas += " and TSPL_Dispatch_BulkSale_Trade.Location_Code in (" & aLoc & ")"
        End If
        ''-----------------------
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_Dispatch_BulkSale_Trade.Document_No = (select MIN(Document_No) from TSPL_Dispatch_BulkSale_Trade WHERE 1=1 " + whrclas + " )"
            Case NavigatorType.Last
                qry += " and TSPL_Dispatch_BulkSale_Trade.Document_No = (select Max(Document_No) from TSPL_Dispatch_BulkSale_Trade WHERE 1=1 " + whrclas + " )"
            Case NavigatorType.Current
                qry += " and TSPL_Dispatch_BulkSale_Trade.Document_No='" + strCode + "' "
            Case NavigatorType.Next
                qry += " and TSPL_Dispatch_BulkSale_Trade.Document_No = (select Min(Document_No) from TSPL_Dispatch_BulkSale_Trade where Document_No>'" + strCode + "' " + whrclas + " )"
            Case NavigatorType.Previous
                qry += " and TSPL_Dispatch_BulkSale_Trade.Document_No = (select Max(Document_No) from TSPL_Dispatch_BulkSale_Trade where Document_No<'" + strCode + "' " + whrclas + " )"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New ClsDispatchBulkSaleTrade()
            obj.Document_No = clsCommon.myCstr(dt.Rows(0)("Document_No"))
            obj.Document_Date = clsCommon.myCDate(dt.Rows(0)("Document_Date"))
            obj.Location_Code = clsCommon.myCstr(dt.Rows(0)("Location_Code"))
            obj.Customer_Code = clsCommon.myCstr(dt.Rows(0)("Customer_Code"))
            obj.Customer_Name = clsCommon.myCstr(dt.Rows(0)("Customer_Name"))
            obj.Price_Code = clsCommon.myCstr(dt.Rows(0)("Price_Code"))
            obj.EWayBillNo = clsCommon.myCstr(dt.Rows(0)("EWayBillNo"))
            If dt.Rows(0)("EWayBillDate") IsNot DBNull.Value Then
                obj.EWayBillDate = clsCommon.myCDate(dt.Rows(0)("EWayBillDate"))
            End If

            obj.Location_Name = clsCommon.myCstr(dt.Rows(0)("Location_Desc"))
            obj.Posted = clsCommon.myCdbl(dt.Rows(0)("Posted"))
            obj.Total_Amt = clsCommon.myCdbl(dt.Rows(0)("Total_Amt"))
            obj.Against_SRN_No = clsCommon.myCstr(dt.Rows(0)("Against_SRN_No"))
            obj.Is_Create_Auto_Invoice = clsCommon.myCdbl(dt.Rows(0)("Is_Create_Auto_Invoice"))
            obj.Tanker_No = clsCommon.myCstr(dt.Rows(0)("Tanker_No"))
            obj.arrDispatchDetailTradeBulkSale = clsDispatchDetailTradeBulkSale.getData(obj.Document_No, trans)
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
            Dim obj As ClsDispatchBulkSaleTrade = ClsDispatchBulkSaleTrade.GetData(strDocNo, NavigatorType.Current, trans)

            If (obj Is Nothing OrElse clsCommon.myLen(obj.Document_No) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If

            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleBulkSale, "", obj.Location_Code, obj.Document_Date, trans)


            ''richa 08/08/2014 for INventory movement
            Dim ArrLocationDetails As List(Of clsItemLocationDetails) = New List(Of clsItemLocationDetails)()
            Dim ArrInventoryMovement As List(Of clsInventoryMovementNew) = New List(Of clsInventoryMovementNew)

            'Dim strFirstItemCodeNonItemRowType As String = GetFirstItemCode(obj.Arr)
            Dim strRgpNo As String = Nothing
            Dim intCounter As Integer = 0
            For Each objTr As clsDispatchDetailTradeBulkSale In obj.arrDispatchDetailTradeBulkSale
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
                Dim DispatchTradeQty As String = ""
                DispatchTradeQty = clsDBFuncationality.getSingleValue("Select Qty from TSPL_INVENTORY_MOVEMENT_NEW where Trans_Type='BulkSRNTrade' and Source_Doc_No ='" & obj.Against_SRN_No & "' and Location_Code ='" & obj.Location_Code & "' and InOut='I'", trans)
                If clsCommon.myCdbl(DispatchTradeQty) > 0 Then
                    If objTr.Qty > DispatchTradeQty Then
                        Throw New Exception("You cannot post this dispatch because stock qty is less than dispatch qty")
                    End If
                Else
                    Throw New Exception("No stock quanity available for itemCode " & objTr.Item_Code & "")
                End If

                ''====================

                Dim objLocationDetails As New clsItemLocationDetails()
                'Dim ConvFac As Double = clsItemMaster.GetConvertionFactor(objTr.Item_Code, objTr.Unit_code, trans)
                'If ConvFac = 0 Then
                '    Throw New Exception("Conversion Factor found zero for item :" + objTr.Item_Code + " and Uom:'" + objTr.Unit_code)
                'End If
                Dim ConvFac As Double = clsItemMaster.GetConvertionFactor(objTr.Item_Code, objTr.Unit_code, trans)
                If ConvFac = 0 Then
                    Throw New Exception("Conversion Factor found zero for item :" + objTr.Item_Code + " and Uom:'" + objTr.Unit_code)
                End If
                objLocationDetails.Item_Code = objTr.Item_Code
                objLocationDetails.Item_Desc = clsDBFuncationality.getSingleValue("Select Item_Desc from TSPL_ITEM_MASTER where Item_Code ='" + objTr.Item_Code + "' ", trans)
                objLocationDetails.Location_Code = obj.Location_Code
                objLocationDetails.Location_Desc = clsDBFuncationality.getSingleValue("Select Location_Desc  from TSPL_LOCATION_MASTER where Location_Code='" + obj.Location_Code + "' ", trans)
                objLocationDetails.Item_Qty = -1 * objTr.Qty
                objLocationDetails.Amount = -1 * objTr.Amount
                objLocationDetails.MRP = 0 * ConvFac

                objLocationDetails.ItemType = strItemTypeToSave
                ArrLocationDetails.Add(objLocationDetails)


                Dim objInventoryMovemnt As New clsInventoryMovementNew()
                objInventoryMovemnt.InOut = "O"

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
            isSaved = isSaved AndAlso clsInventoryMovementNew.SaveData("DispatchBSTrade", obj.Document_No, obj.Document_Date, clsCommon.GetPrintDate(obj.Document_Date, "dd/MM/yyyy"), ArrInventoryMovement, trans)

            If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowPurchaseAccounting, clsFixedParameterCode.AllowPurchaseAccounting, trans)) = 0 Then
                CreateJournalEntry(obj.Document_No, trans)
            End If
            ''
            Dim qry = "Update TSPL_Dispatch_BulkSale_Trade set Posted=1, " & _
            "Posting_Date='" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt") + "' " & _
            " where Document_No='" + strDocNo + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            ''richa 18/10/2014
            If obj.Is_Create_Auto_Invoice Then
                Dim AmountLimitInvoiceBulkSale As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select description from tspl_fixed_parameter where type='" + clsFixedParameterType.AmountLimitForInvoiceBulkSale + "' and code='" + clsFixedParameterCode.AmountLimitForInvoiceBulkSale + "'", trans))
                If AmountLimitInvoiceBulkSale > 0 Then
                    If obj.Total_Amt > AmountLimitInvoiceBulkSale Then
                        Throw New Exception("You cannot post this Dispatch because amount limit of invoice is less than Document amount")
                    End If
                End If
                Dim objSI As ClsInvoiceBulkSale = ConvertDispatchToSaleInvoice(obj)
                ClsInvoiceBulkSale.SaveData(objSI, True, trans)
                Dim aloc As String = "'" + obj.Location_Code + "'"
                ClsInvoiceBulkSale.PostData("", aloc, objSI.Document_No, trans)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Private Shared Function ConvertDispatchToSaleInvoice(ByVal objDispatch As ClsDispatchBulkSaleTrade) As ClsInvoiceBulkSale
        Dim obj As New ClsInvoiceBulkSale()
        obj = New ClsInvoiceBulkSale()
        'obj.Document_No = objDispatch.Document_No
        obj.Document_Date = objDispatch.Document_Date
        obj.Customer_Code = objDispatch.Customer_Code
        obj.Location_Code = objDispatch.Location_Code
        obj.InvoiceAgainst = "Against Dispatch Trade"
        ' obj.Total_Amt = objDispatch.Total_Amt

        If Math.Round(clsCommon.myCdbl(objDispatch.Total_Amt), 0) > clsCommon.myCdbl(objDispatch.Total_Amt) Then
            'obj.RoundOffAmount = Math.Round(clsCommon.myCdbl(clsCommon.myCdbl(clsCommon.myCdbl(objDispatch.Total_Amt)) - Math.Round(clsCommon.myCdbl(objDispatch.Total_Amt), 0)), 2)
            obj.RoundOffAmount = Math.Round(Math.Round(clsCommon.myCdbl(objDispatch.Total_Amt), 0) - clsCommon.myCdbl(objDispatch.Total_Amt), 2)
            obj.Total_Amt = Math.Round(clsCommon.myCdbl(objDispatch.Total_Amt), 0)
        Else
            'obj.RoundOffAmount = Math.Round(clsCommon.myCdbl(clsCommon.myCdbl(objDispatch.Total_Amt) - Math.Round(clsCommon.myCdbl(objDispatch.Total_Amt))), 2)
            obj.RoundOffAmount = Math.Round(Math.Round(clsCommon.myCdbl(objDispatch.Total_Amt)) - clsCommon.myCdbl(objDispatch.Total_Amt), 2)
            obj.Total_Amt = Math.Round(clsCommon.myCdbl(objDispatch.Total_Amt), 0)

        End If

        If (objDispatch.arrDispatchDetailTradeBulkSale IsNot Nothing AndAlso objDispatch.arrDispatchDetailTradeBulkSale.Count > 0) Then
            obj.arrInvoiceDetailBulkSale = New List(Of ClsInvoiceDetailBulkSale)
            Dim objTr As ClsInvoiceDetailBulkSale
            For Each objDispatchDetail As clsDispatchDetailTradeBulkSale In objDispatch.arrDispatchDetailTradeBulkSale
                objTr = New ClsInvoiceDetailBulkSale
                objTr.Dispatch_Code = objDispatchDetail.Document_No
                objTr.Dispatch_Date = objDispatch.Document_Date
                objTr.Item_Code = objDispatchDetail.Item_Code
                objTr.Unit_code = objDispatchDetail.Unit_code
                objTr.TradeTanker_No = objDispatch.Tanker_No
                objTr.DispatchQty = objDispatchDetail.Qty
                objTr.DispatchFatPer = objDispatchDetail.FatPer
                objTr.DispatchSNFPer = objDispatchDetail.SNFPer
                objTr.DispatchRate = objDispatchDetail.Rate

                objTr.DispatchAmount = objDispatchDetail.Amount
                objTr.InvoiceFatPer = objDispatchDetail.FatPer
                objTr.InvoiceSNFPer = objDispatchDetail.SNFPer
                objTr.InvoiceQty = objDispatchDetail.Qty
                objTr.InvoiceRate = objDispatchDetail.Rate
                objTr.InvoiceAmount = objDispatchDetail.Amount
                objTr.InvoiceFatKG = objDispatchDetail.Fat_KG
                objTr.InvoiceSNFKG = objDispatchDetail.SNF_KG
                obj.arrInvoiceDetailBulkSale.Add(objTr)
            Next
        End If
        Return obj
    End Function
    Public Shared Sub CreateJournalEntry(ByVal strCode As String, ByVal trans As SqlTransaction)
        Dim RecoControlACC As String = ""
        If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowPurchaseAccounting, clsFixedParameterCode.AllowPurchaseAccounting, trans)) = 0 Then
            RecoControlACC = "I"
        End If
        Dim obj As New ClsDispatchBulkSaleTrade
        obj = ClsDispatchBulkSaleTrade.GetData(strCode, NavigatorType.Current, trans)
        Dim ArryLstGLAC As ArrayList = New ArrayList()
        Dim strInventoryControlAc As String = ""
        Dim strShipmentClearingAC As String = ""
        Dim dblTotalCost As Double = 0

        strShipmentClearingAC = clsDBFuncationality.getSingleValue("SELECT PA.Shipment_Clearing FROM TSPL_ITEM_MASTER AS IM INNER JOIN " & _
          " TSPL_PURCHASE_ACCOUNTS AS PA ON IM.Purchase_Class_Code = PA.Purchase_Class_Code INNER JOIN " & _
           " TSPL_GL_ACCOUNTS AS GLA ON PA.Inv_Control_Account = GLA.Account_Code WHERE IM.Item_Code='" + obj.arrDispatchDetailTradeBulkSale.Item(0).Item_Code + "'", trans)
        strShipmentClearingAC = clsERPFuncationality.ChangeGLAccountLocationSegment(strShipmentClearingAC, obj.Location_Code, trans)

        If clsCommon.myLen(strShipmentClearingAC) = 0 Then
            Throw New Exception("Please set Shipment clearing Account for first item")
        End If

        Dim dblCogsCost As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select sum(case when Costing_Method=0 then Avg_Cost when Costing_Method=1 then Avg_Cost when Costing_Method=2 then FIFO_Cost when Costing_Method=3 then LIFO_Cost end) as COst from TSPL_INVENTORY_MOVEMENT_NEW left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_INVENTORY_MOVEMENT_NEW.Item_Code left outer join TSPL_PURCHASE_ACCOUNTS on TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code=TSPL_ITEM_MASTER.Purchase_Class_Code where Source_Doc_No='" & obj.Document_No & "'", trans))

        Dim Acc() As String = {strShipmentClearingAC, dblCogsCost}
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
                Dim Acc1() As String = {strInventoryControlAc, -1 * clsCommon.myCdbl(dr("Cost")), "", "", "", "", "", "", RecoControlACC}
                ArryLstGLAC.Add(Acc1)
                If clsCommon.CompairString(RecoControlACC, "I") = CompairStringResult.Equal Then
                    ''TEC/14/02/19-000426 by Richa on 14/02/2019
                    clsInventoryMovement.UpdateInvControlAccount(clsCommon.myCstr(strCode), "DispatchBSTrade", clsCommon.myCstr(dr("Item_Code")), "", strInventoryControlAc, "", trans)
                    ''------------------
                End If
            Next
        End If
        '' BHA/30/10/18-000646 RICHA AGARWAL SEND CUSTOMER CODE AND CUSTOMER NAME INTO JOURNAL ENTRY AND TYPE C instead of O 30 Oct,2018
        clsJournalMaster.FunGrnlEntryWithTrans(obj.Location_Code, False, trans, obj.Document_Date, "Journal Entry Against Dispatch Bulk Sale trade for Document No " + obj.Document_No + " ", "DS-BT", "DISPATCH Bulk Sale Trade", obj.Document_No, "", "C", obj.Customer_Code, clsCustomerMaster.GetName(obj.Customer_Code, trans), objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLstGLAC, , "", "")


    End Sub
    Public Shared Function DeleteData(ByVal strDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Dim isSaved As Boolean = False
        If (clsCommon.myLen(strDocNo) <= 0) Then
            Throw New Exception("Document No not found to Delete")
        End If
        Dim dt As DataTable = clsDBFuncationality.GetDataTable("select Document_Date,Location_Code from TSPL_Dispatch_BulkSale_Trade where Document_No='" + strDocNo + "'", trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleBulkSale, "", clsCommon.myCstr(dt.Rows(0)("Location_Code")), clsCommon.myCDate(dt.Rows(0)("Document_Date")), trans)

        End If
        Try
            Dim qry As String = ""
            qry = "delete from TSPL_Dispatch_Detail_BulkSale_Trade where Document_No='" + strDocNo + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "delete from TSPL_Dispatch_BulkSale_Trade where Document_No='" + strDocNo + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try

        Return isSaved
    End Function
End Class


Public Class clsDispatchDetailTradeBulkSale
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
    Public Shared Function saveData(ByVal arrObj As List(Of clsDispatchDetailTradeBulkSale), ByVal strDocNo As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim issaved As Boolean = True
            Dim coll As Hashtable

            If arrObj IsNot Nothing Then
                For Each obj As clsDispatchDetailTradeBulkSale In arrObj
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
                    issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Dispatch_Detail_BulkSale_Trade", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If
            Return issaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            arrObj = Nothing
        End Try
    End Function
    Public Shared Function getData(ByVal strdocNo As String, ByVal trans As SqlTransaction) As List(Of clsDispatchDetailTradeBulkSale)
        Try
            Dim arrObj As List(Of clsDispatchDetailTradeBulkSale) = Nothing
            Dim obj As clsDispatchDetailTradeBulkSale = Nothing
            Dim qry As String = "Select TSPL_Dispatch_Detail_BulkSale_Trade.Document_No,TSPL_Dispatch_Detail_BulkSale_Trade.Item_Code,TSPL_ITEM_MASTER.Item_Desc,TSPL_Dispatch_Detail_BulkSale_Trade.Qty,TSPL_Dispatch_Detail_BulkSale_Trade.FatPer,TSPL_Dispatch_Detail_BulkSale_Trade.SNFPer,TSPL_Dispatch_Detail_BulkSale_Trade.Fat_KG,TSPL_Dispatch_Detail_BulkSale_Trade.SNF_KG,TSPL_Dispatch_Detail_BulkSale_Trade.FatAmount,TSPL_Dispatch_Detail_BulkSale_Trade.SNFAmount,TSPL_Dispatch_Detail_BulkSale_Trade.Amount,TSPL_Dispatch_Detail_BulkSale_Trade.Rate,TSPL_Dispatch_Detail_BulkSale_Trade.FatRate,TSPL_Dispatch_Detail_BulkSale_Trade.SNFRate,TSPL_Dispatch_Detail_BulkSale_Trade.Unit_Code,TSPL_Dispatch_Detail_BulkSale_Trade.StandardRate from TSPL_Dispatch_Detail_BulkSale_Trade Left Outer Join TSPL_ITEM_MAster on TSPL_ITEM_MASTER.Item_Code=TSPL_Dispatch_Detail_BulkSale_Trade.Item_Code where TSPL_Dispatch_Detail_BulkSale_Trade.Document_No='" & strdocNo & "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                arrObj = New List(Of clsDispatchDetailTradeBulkSale)
                For i As Integer = 0 To dt.Rows.Count - 1
                    obj = New clsDispatchDetailTradeBulkSale()
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
Public Class clsBulkMilkSRNTrade
    Public SRN_NO As String = String.Empty
    Public SRN_Date As Date = Nothing
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
        ' Dim trans As SqlTransaction = Nothing
        Dim isSaved As Boolean = True
        Dim isPosted As Boolean = True
        Try
            If (clsCommon.myLen(StrDocNo) <= 0) Then
                Throw New Exception(" Doc No not found to Post")
            End If

            Dim obj As clsBulkMilkSRNTrade = clsBulkMilkSRNTrade.GetData(StrDocNo, NavigatorType.Current, trans)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.SRN_NO) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            ' trans = clsDBFuncationality.GetTransactin()
            'clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "Bulk Milk SRN Trade", "Bulk Milk SRN Trade", obj.Loc_Code, obj.SRN_Date, trans)
            If (obj.isPosted = 1) Then
                Throw New Exception("Already Post on :" + obj.Posting_Date)
            End If

            '--------------------
            'Dim isResult As Boolean = clsApprovalScreen.CheckApprovalLevel(formId, "tspl_bulk_milk_srn", "SRN_No" obj.SRN_NO, trans)
            'If isResult = False Then
            '    trans.Commit()
            '    Return False
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
            objLocationDetails.Item_Qty = obj.Net_Weight
            objLocationDetails.Amount = obj.Actual_Amount
            objLocationDetails.MRP = 0
            objLocationDetails.ItemType = strItemTypeToSave
            ArrLocationDetails.Add(objLocationDetails)

            Dim objInventoryMovemnt As New clsInventoryMovementNew()
            objInventoryMovemnt.InOut = "I"
            '-----------Getting Sub Location Where Milk Was unloaded
            ' Dim strSiloNo As String = clsDBFuncationality.getSingleValue("select Sub_location_Code  from TSPL_MILK_UNLOADING where Gate_Entry_No='" & obj.Gate_Entry_No & "'", trans)
            '-----------------------------------
            ' objInventoryMovemnt.Location_Code = strSiloNo
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
            ''richa agarwal 07/07/2015
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
            isSaved = isSaved AndAlso clsItemLocationDetails.SaveData(clsCommon.GetPrintDate(obj.SRN_Date, "dd/MM/yyyy"), ArrLocationDetails, trans)
            isSaved = isSaved AndAlso clsInventoryMovementNew.SaveData("BulkSRNTrade", obj.SRN_NO, obj.SRN_Date, clsCommon.GetPrintDate(obj.SRN_Date, "dd/MM/yyyy"), ArrInventoryMovement, trans)
            'Create GL Entry
            If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowPurchaseAccounting, clsFixedParameterCode.AllowPurchaseAccounting, trans)) = 0 Then
                qry = " select TSPL_PURCHASE_ACCOUNTS.Inv_Control_Account,TSPL_PURCHASE_ACCOUNTS.Inv_Payable_Clearing ,TSPL_Bulk_MILK_SRN.Actual_Amount,TSPL_Bulk_MILK_SRN.Loc_Code   from TSPL_PURCHASE_ACCOUNTS left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Purchase_Class_Code =TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code left outer join TSPL_Bulk_MILK_SRN on TSPL_Bulk_MILK_SRN .Item_Code=TSPL_ITEM_MASTER.Item_Code where TSPL_Bulk_MILK_SRN.SRN_NO='" & obj.SRN_NO & "'  and TSPL_Bulk_MILK_SRN.FormType='Bulk Milk SRN Trade' "
                Dim ArryLst As ArrayList = New ArrayList()
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    Dim strInvCntrlAc As String = dt.Rows(0)("Inv_Control_Account")
                    Dim strPaybleClrAc As String = dt.Rows(0)("Inv_Payable_Clearing")
                    strInvCntrlAc = clsERPFuncationality.ChangeGLAccountLocationSegment(strInvCntrlAc, dt.Rows(0)("Loc_Code"), trans)
                    strPaybleClrAc = clsERPFuncationality.ChangeGLAccountLocationSegment(strPaybleClrAc, dt.Rows(0)("Loc_Code"), trans)
                    ArryLst.Add(New String() {strInvCntrlAc, dt.Rows(0)("Actual_Amount"), "", "", "", "", "", "", "I"})

                    ''TEC/14/02/19-000426 by Richa on 14/02/2019
                    clsInventoryMovement.UpdateInvControlAccount(clsCommon.myCstr(obj.SRN_NO), "BulkSRNTrade", clsCommon.myCstr(obj.Item_Code), strInvCntrlAc, "", "", trans)
                    ''------------------
                    ArryLst.Add(New String() {strPaybleClrAc, dt.Rows(0)("Actual_Amount") * -1})
                    clsJournalMaster.FunGrnlEntryWithTrans(obj.Loc_Code, False, trans, clsCommon.GetPrintDate(obj.SRN_Date, "dd/MMM/yyyy"), " GL Entry Against Bulk Milk SRN Trade No  -" + obj.SRN_NO + "", "BM-TR", "Bulk Milk SRN Trade", obj.SRN_NO, "", "C", obj.Item_Code, obj.Item_Desc, objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLst)
                End If
            End If

            Dim strQry As String = " update tspl_bulk_milk_srn set isPosted='1', Posting_Date='" & clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy") & "', sub_location='" & obj.sub_location & "' where srn_no='" & StrDocNo & "' and FormType='Bulk Milk SRN Trade'"
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
            qry = "delete from tspl_bulk_milk_srn where srn_No='" & strDocNo & "' and FormType='Bulk Milk SRN Trade' "
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try

        Return isSaved
    End Function
    Public Shared Function SaveData(ByVal obj As clsBulkMilkSRNTrade, ByVal isNewEntry As Boolean) As Boolean
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
    Public Shared Function saveData(ByVal obj As clsBulkMilkSRNTrade, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        ' Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If isNewEntry Then
                obj.SRN_NO = clsERPFuncationality.GetNextCode(trans, obj.SRN_Date, clsDocType.BulkMilkSRNTrade, "", obj.Loc_Code)
            End If

            Dim coll As New Hashtable()
            If clsCommon.myLen(obj.SRN_Date) > 0 Then
                clsCommon.AddColumnsForChange(coll, "SRN_Date", clsCommon.GetPrintDate(obj.SRN_Date, "dd/MMM/yyyy hh:mm tt"), True)
            End If
            clsCommon.AddColumnsForChange(coll, "Weighment_No", clsCommon.myCstr(obj.Weighment_No), True)
            'clsCommon.AddColumnsForChange(coll, "Weighment_Date", clsCommon.GetPrintDate(obj.Weighment_Date, "dd/MMM/yyyy"), True)
            clsCommon.AddColumnsForChange(coll, "Gate_Entry_No", clsCommon.myCstr(obj.Gate_Entry_No), True)
            clsCommon.AddColumnsForChange(coll, "QC_No", clsCommon.myCstr(obj.QC_No), True)

            'clsCommon.AddColumnsForChange(coll, "Qc_Date", clsCommon.GetPrintDate(obj.Qc_Date, "dd/MMM/yyyy"), True)
            clsCommon.AddColumnsForChange(coll, "Tanker_No", clsCommon.myCstr(obj.Tanker_No), True)
            clsCommon.AddColumnsForChange(coll, "Vendor_Code", clsCommon.myCstr(obj.Vendor_Code))
            clsCommon.AddColumnsForChange(coll, "Loc_Code", clsCommon.myCstr(clsLocation.GetSegmentCode(obj.Loc_Code, trans)))
            clsCommon.AddColumnsForChange(coll, "sub_location", clsCommon.myCstr(obj.sub_location))
            clsCommon.AddColumnsForChange(coll, "Challan_No", clsCommon.myCstr(obj.Challan_No))
            If clsCommon.myLen(obj.Challan_Date) > 0 Then '
                clsCommon.AddColumnsForChange(coll, "Challan_Date", clsCommon.GetPrintDate(obj.Challan_Date, "dd/MMM/yyyy"), True)
            End If
            clsCommon.AddColumnsForChange(coll, "Price_Code", clsCommon.myCstr(obj.Price_Code))
            clsCommon.AddColumnsForChange(coll, "Item_Code", clsCommon.myCstr(obj.Item_Code))
            clsCommon.AddColumnsForChange(coll, "Item_Desc", clsCommon.myCstr(obj.Item_Desc))
            clsCommon.AddColumnsForChange(coll, "UOM", clsCommon.myCstr(obj.UOM))
            clsCommon.AddColumnsForChange(coll, "Gross_Weight", clsCommon.myCdbl(obj.Gross_Weight))
            clsCommon.AddColumnsForChange(coll, "Tare_Weight", clsCommon.myCdbl(obj.Tare_Weight))
            clsCommon.AddColumnsForChange(coll, "Net_Weight", clsCommon.myCdbl(obj.Net_Weight))
            clsCommon.AddColumnsForChange(coll, "snf_Per", clsCommon.myCstr(obj.snf_Per))
            clsCommon.AddColumnsForChange(coll, "fat_per", clsCommon.myCdbl(obj.fat_per))
            clsCommon.AddColumnsForChange(coll, "SNF_KG", clsCommon.myCdbl(obj.SNF_KG))
            clsCommon.AddColumnsForChange(coll, "fat_KG", clsCommon.myCdbl(obj.fat_KG))
            clsCommon.AddColumnsForChange(coll, "fat_Rate", clsCommon.myCdbl(obj.fat_Rate))
            clsCommon.AddColumnsForChange(coll, "SNF_Rate", clsCommon.myCdbl(obj.SNF_Rate))
            ''richa agarwal 07/07/2015
            clsCommon.AddColumnsForChange(coll, "FatAmt", clsCommon.myCdbl(obj.fat_amount))
            clsCommon.AddColumnsForChange(coll, "SnfAmt", clsCommon.myCdbl(obj.SNF_Amount))
            ''-----------------------
            clsCommon.AddColumnsForChange(coll, "Amount", clsCommon.myCdbl(obj.Amount))
            clsCommon.AddColumnsForChange(coll, "Deduction", clsCommon.myCdbl(obj.Deduction))
            clsCommon.AddColumnsForChange(coll, "Incentive", clsCommon.myCdbl(obj.Incentive))
            clsCommon.AddColumnsForChange(coll, "SpecialDeduction", clsCommon.myCdbl(obj.SpecialDeduction))
            clsCommon.AddColumnsForChange(coll, "Actual_Amount", clsCommon.myCdbl(obj.Actual_Amount))
            clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "FormType", "Bulk Milk SRN Trade")
            clsCommon.AddColumnsForChange(coll, "StandardRate", obj.StandardRate)
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "SRN_NO", obj.SRN_NO)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                clsCommonFunctionality.UpdateDataTable(coll, "tspl_bulk_milk_srn", OMInsertOrUpdate.Insert, "", trans)
            Else
                clsCommonFunctionality.UpdateDataTable(coll, "tspl_bulk_milk_srn", OMInsertOrUpdate.Update, "tspl_bulk_milk_srn.srn_no='" + obj.SRN_NO + "'  and TSPL_Bulk_MILK_SRN.FormType='Bulk Milk SRN Trade'", trans)
            End If

            clsDBFuncationality.ExecuteNonQuery("Update TSPL_Dispatch_BulkSale_Trade set Against_SRN_No='" + obj.SRN_NO + "' where Document_No ='" + obj.Challan_No + "'", trans)
            ' trans.Commit()
        Catch ex As Exception
            ' trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsBulkMilkSRNTrade
        Return GetData(strCode, NavType, Nothing)
    End Function

    Public Shared Function getData(ByVal strCode As String, ByVal navtype As NavigatorType, ByVal trans As SqlTransaction) As clsBulkMilkSRNTrade
        Dim obj As New clsBulkMilkSRNTrade
        Try
            Dim qst As String = " select *   From tspl_bulk_milk_srn   where 1=1 and FormType='Bulk Milk SRN Trade'   "

            Dim whrCls As String = String.Empty
            If Not clsMccMaster.isCurrentUserHO(trans) Then
                If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                    whrCls = "and Loc_code in (" & objCommonVar.strCurrUserLocations & ")"
                End If
            End If
            qst = qst & whrCls
            Select Case navtype
                Case NavigatorType.Current
                    qst += " and tspl_bulk_milk_srn.srn_no in ('" + strCode + "') "
                Case NavigatorType.Next
                    qst += " and tspl_bulk_milk_srn.srn_no in (select min(srn_no ) from tspl_bulk_milk_srn where srn_no  >'" + strCode + "' and FormType='Bulk Milk SRN Trade' " & whrCls & "  )"
                Case NavigatorType.First
                    qst += " and tspl_bulk_milk_srn.srn_no in (select MIN(srn_no ) from tspl_bulk_milk_srn  where 1=1 and FormType='Bulk Milk SRN Trade' " & whrCls & "  )"
                Case NavigatorType.Last
                    qst += " and tspl_bulk_milk_srn.srn_no in (select Max(srn_no ) from tspl_bulk_milk_srn  where 1=1 and FormType='Bulk Milk SRN Trade' " & whrCls & "  )"
                Case NavigatorType.Previous
                    qst += " and tspl_bulk_milk_srn.srn_no in (select Max(srn_no ) from tspl_bulk_milk_srn where srn_No  <'" + strCode + "'  and FormType='Bulk Milk SRN Trade' " & whrCls & "  )"
            End Select
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qst, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                obj.SRN_NO = clsCommon.myCstr(dt.Rows(0)("SRN_NO"))
                obj.SRN_Date = clsCommon.myCDate(dt.Rows(0)("srn_date"))
                'obj.Gate_Entry_No = clsCommon.myCstr(dt.Rows(0)("gate_entry_no"))
                'obj.Weighment_No = clsCommon.myCstr(dt.Rows(0)("Weighment_No"))
                ''obj.Weighment_Date = clsCommon.myCDate(dt.Rows(0)("Weighment_Date"), "dd/MMM/yyyy hh:mm:ss tt")
                'obj.QC_No = clsCommon.myCstr(dt.Rows(0)("QC_No"))
                'obj.Qc_Date = clsCommon.myCDate(dt.Rows(0)("qc_date"), "dd/MMM/yyyy hh:mm:ss tt")
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
                ''richa Against Ticket No.BM00000003719 on 04/09/2014
                obj.SpecialDeduction = clsCommon.myCdbl(dt.Rows(0)("SpecialDeduction"))
                obj.StandardRate = clsCommon.myCdbl(dt.Rows(0)("StandardRate"))
                ''=======================================
                ''richa Agarwal 07/07/2015
                obj.fat_amount = clsCommon.myCdbl(dt.Rows(0)("FatAmt"))
                obj.SNF_Amount = clsCommon.myCdbl(dt.Rows(0)("SnfAmt"))
                '------------------------
                obj.HSN_code = clsItemMaster.GetItemHSNCode(clsCommon.myCstr(dt.Rows(0)("item_code")), trans)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
        Return obj
    End Function



End Class




''==================================



