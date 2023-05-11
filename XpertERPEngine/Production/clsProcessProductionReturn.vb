Imports common
Imports System.Data
Imports System.Data.SqlClient
Public Class clsProcessProductionReturn

#Region "Variables"

    Public PROD_RETURN_CODE As String
    Public Transaction_Type As String
    Public PROD_ENTRY_CODE As String
    Public DESCRIPTION As String            
    Public COMMENTS As String
    Public RETURN_DATE As Date
    Public Batch_Code As String
    Public BATCH_DATE As Date
    Public LOCATION_CODE As String
    Public LOCATION_NAME As String
    Public CONSM_LOCATION_CODE As String
    Public CONSM_SECTION_CODE As String    
    Public POSTED As Boolean
    Public POSTING_DATE As Date
    Public Prod_Date As Date
    Public ManualBatchNo As String = Nothing
    Public LINE_NO As String = String.Empty
    Public CostCenterCode As String = String.Empty
    Public ProfitCenterCode As String = String.Empty
    Public CostCenterName As String = String.Empty
    Public ProfitCenterName As String = String.Empty
    Public arrBatchItem As List(Of clsBatchInventory) = Nothing
    Public strLocationCode As String = ""

#End Region

    Public Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsProcessProductionReturn
        Return GetData(strCode, NavType, Nothing)
    End Function
    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim isSaved As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            isSaved = False
            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Code not found to Delete")
            End If
            Dim dt As DataTable = clsDBFuncationality.GetDataTable("select	RETURN_DATE,LOCATION_CODE from TSPL_PP_PRODUCTION_RETURN where PROD_RETURN_CODE='" + strCode + "'", trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleProductionDairy, clsUserMgtCode.frmProcessProdReturn, clsCommon.myCstr(dt.Rows(0)("LOCATION_CODE")), clsCommon.myCDate(dt.Rows(0)("RETURN_DATE")), trans)

            End If

            Dim qry As String
            qry = "delete from TSPL_PP_PRODUCTION_RETURN where PROD_RETURN_CODE ='" + strCode + "'"
            isSaved = isSaved And clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "update TSPL_PP_PRODUCTION_RETURN_Delete_Data set Delete_By = '" + objCommonVar.CurrentUserCode + "' where PROD_RETURN_CODE='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsProcessProductionReturn
        Dim obj As New clsProcessProductionReturn()
        Dim qry As String = " SELECT TSPL_PP_PRODUCTION_RETURN.*,TSPL_LOCATION_MASTER.Location_Desc,TSPL_PROFIT_CENTER_MASTER.Name as [ProfitCenterName],TSPL_CostCenter_MASTER.Cost_name as [Cost_Center_Name] FROM TSPL_PP_PRODUCTION_RETURN " & _
         " LEFT OUTER JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code = TSPL_PP_PRODUCTION_RETURN.LOCATION_CODE " & _
             " left outer join TSPL_PROFIT_CENTER_MASTER on TSPL_PROFIT_CENTER_MASTER.Code =TSPL_PP_PRODUCTION_RETURN.ProfitCenterCode " & _
       " left outer join TSPL_CostCenter_MASTER on TSPL_CostCenter_MASTER.Cost_Code =TSPL_PP_PRODUCTION_RETURN.CostCenterCode "

        qry += " where 2=2 "
        Select Case NavType
            Case NavigatorType.First
                qry += " and PROD_RETURN_CODE = (select MIN(PROD_RETURN_CODE) from TSPL_PP_PRODUCTION_RETURN)"
            Case NavigatorType.Last
                qry += " and PROD_RETURN_CODE = (select Max(PROD_RETURN_CODE) from TSPL_PP_PRODUCTION_RETURN)"
            Case NavigatorType.Next
                qry += " and PROD_RETURN_CODE = (select Min(PROD_RETURN_CODE) from TSPL_PP_PRODUCTION_RETURN where PROD_RETURN_CODE > '" + strCode + "')"
            Case NavigatorType.Previous
                qry += " and PROD_RETURN_CODE = (select Max(PROD_RETURN_CODE) from TSPL_PP_PRODUCTION_RETURN where PROD_RETURN_CODE < '" + strCode + "')"
            Case NavigatorType.Current
                qry += " and PROD_RETURN_CODE = '" + strCode + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj.PROD_RETURN_CODE = clsCommon.myCstr(dt.Rows(0)("PROD_RETURN_CODE"))
            obj.Batch_Code = clsCommon.myCstr(dt.Rows(0)("Batch_Code"))
            obj.BATCH_DATE = clsCommon.myCDate(dt.Rows(0)("BATCH_DATE"))
            obj.COMMENTS = clsCommon.myCstr(dt.Rows(0)("COMMENTS"))
            obj.CONSM_LOCATION_CODE = clsCommon.myCstr(dt.Rows(0)("CONSM_LOCATION_CODE"))
            obj.CONSM_SECTION_CODE = clsCommon.myCstr(dt.Rows(0)("CONSM_SECTION_CODE"))
            obj.DESCRIPTION = clsCommon.myCstr(dt.Rows(0)("DESCRIPTION"))
            obj.LOCATION_CODE = clsCommon.myCstr(dt.Rows(0)("LOCATION_CODE"))
            obj.LOCATION_NAME = clsCommon.myCstr(dt.Rows(0)("Location_Desc"))
            obj.Transaction_Type = clsCommon.myCstr(dt.Rows(0)("Transaction_Type"))
            obj.PROD_ENTRY_CODE = clsCommon.myCstr(dt.Rows(0)("PROD_ENTRY_CODE"))
            obj.RETURN_DATE = clsCommon.myCDate(dt.Rows(0)("RETURN_DATE"))
            obj.Prod_Date = clsCommon.myCDate(dt.Rows(0)("Prod_Date"))
            obj.POSTED = clsCommon.myCBool(dt.Rows(0)("POSTED"))

            ''richa agarwal againt ticket no BHA/02/07/18-000120
            obj.ManualBatchNo = clsCommon.myCstr(dt.Rows(0)("ManualBatchNo"))
            obj.LINE_NO = clsCommon.myCstr(dt.Rows(0)("LINE_NO"))
            obj.CostCenterCode = clsCommon.myCstr(dt.Rows(0)("CostCenterCode"))
            obj.CostCenterName = clsCommon.myCstr(dt.Rows(0)("Cost_Center_Name"))
            obj.ProfitCenterCode = clsCommon.myCstr(dt.Rows(0)("ProfitCenterCode"))
            obj.ProfitCenterName = clsCommon.myCstr(dt.Rows(0)("ProfitCenterName"))
            ''--------------------------
            If IsDBNull(dt.Rows(0)("POSTING_DATE")) Then
                obj.POSTING_DATE = Nothing
            Else
                obj.POSTING_DATE = clsCommon.myCDate(dt.Rows(0)("POSTING_DATE"))
            End If
        End If
        Return obj
    End Function
    Public Function SaveData(ByVal obj As clsProcessProductionReturn, ByVal isNewEntry As Boolean) As Boolean
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
    Public Function SaveData(ByVal obj As clsProcessProductionReturn, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Try
            If isNewEntry Then
                If (clsCommon.myLen(obj.PROD_RETURN_CODE) <= 0) Then
                    obj.PROD_RETURN_CODE = clsERPFuncationality.GetNextCode(trans, clsCommon.GetPrintDate(obj.RETURN_DATE, "dd/MMM/yyyy"), clsDocType.ProductionReturn, obj.Transaction_Type, obj.LOCATION_CODE)
                End If
            End If
            If (clsCommon.myLen(obj.PROD_RETURN_CODE) <= 0) Then
                Throw New Exception("Error in Document Code Generation")
            End If
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleProductionDairy, clsUserMgtCode.frmProcessProdReturn, obj.LOCATION_CODE, obj.RETURN_DATE, trans)
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Batch_Code", obj.Batch_Code)
            clsCommon.AddColumnsForChange(coll, "BATCH_DATE", clsCommon.GetPrintDate(obj.BATCH_DATE, "dd-MMM-yyyy"))
            clsCommon.AddColumnsForChange(coll, "COMMENTS", obj.COMMENTS)
            clsCommon.AddColumnsForChange(coll, "CONSM_LOCATION_CODE", obj.CONSM_LOCATION_CODE)
            clsCommon.AddColumnsForChange(coll, "CONSM_SECTION_CODE", obj.CONSM_SECTION_CODE)
            clsCommon.AddColumnsForChange(coll, "DESCRIPTION", obj.DESCRIPTION)
            clsCommon.AddColumnsForChange(coll, "LOCATION_CODE", obj.LOCATION_CODE)
            clsCommon.AddColumnsForChange(coll, "Transaction_Type", obj.Transaction_Type)
            clsCommon.AddColumnsForChange(coll, "PROD_ENTRY_CODE", obj.PROD_ENTRY_CODE)
            clsCommon.AddColumnsForChange(coll, "PROD_RETURN_CODE", obj.PROD_RETURN_CODE)
            clsCommon.AddColumnsForChange(coll, "RETURN_DATE", clsCommon.GetPrintDate(obj.RETURN_DATE, "dd-MMM-yyyy"))
            clsCommon.AddColumnsForChange(coll, "Prod_Date", clsCommon.GetPrintDate(obj.Prod_Date, "dd-MMM-yyyy"))
            ''richa agarwal againt ticket no BHA/02/07/18-000120
            clsCommon.AddColumnsForChange(coll, "ManualBatchNo", obj.ManualBatchNo)
            clsCommon.AddColumnsForChange(coll, "LINE_NO", obj.LINE_NO, True)
            clsCommon.AddColumnsForChange(coll, "CostCenterCode", obj.CostCenterCode, True)
            clsCommon.AddColumnsForChange(coll, "ProfitCenterCode", obj.ProfitCenterCode, True)
            ''------------------
            clsCommon.AddColumnsForChange(coll, "comp_code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "POSTED", "0")
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                isSaved = clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PP_PRODUCTION_RETURN", OMInsertOrUpdate.Insert, "", trans)
            Else
                HistoryUpdate(obj.PROD_RETURN_CODE, trans)
                isSaved = clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PP_PRODUCTION_RETURN", OMInsertOrUpdate.Update, "TSPL_PP_PRODUCTION_RETURN.PROD_RETURN_CODE='" + obj.PROD_RETURN_CODE + "'", trans)
            End If
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function
    Public Shared Function HistoryUpdate(ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, clsCommon.myCstr(strCode), "TSPL_PP_PRODUCTION_RETURN", "PROD_RETURN_CODE", trans)
        Return True
    End Function
    Public Shared Function PostData(ByVal strDocNo As String) As Boolean
        Dim objforLocation As clsProcessProductionReturn = New clsProcessProductionReturn()
        objforLocation.strLocationCode = LOCATIONRIGTHS()
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Code not found to Post")
            End If
            Dim strPostDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt")
            Dim obj As New clsProcessProductionReturn
            obj = clsProcessProductionReturn.GetData(strDocNo, NavigatorType.Current, trans)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.PROD_RETURN_CODE) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleProductionDairy, clsUserMgtCode.frmProcessProdReturn, obj.LOCATION_CODE, obj.RETURN_DATE, trans)

            If (obj.POSTED = 1) Then
                Throw New Exception("Already Post on :" + obj.POSTING_DATE)
            End If
            Dim qry As String = ""
            HistoryUpdate(strDocNo, trans)
            UpdateBatchItem(obj.PROD_ENTRY_CODE, obj.PROD_RETURN_CODE, NavigatorType.Current, objforLocation.strLocationCode, trans)
            UpdateInventoryMovement("PP-PR", strDocNo, trans)
            qry = "Update TSPL_PP_PRODUCTION_RETURN set POSTED=1, POSTING_DATE ='" + strPostDate + "',Modified_By='" + objCommonVar.CurrentUserCode + "' where PROD_RETURN_CODE ='" + strDocNo + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            '================================= Ticket No : BHA/03/08/18-000386 =================================================
            ''Journal Entry  
            qry = " select TSPL_JOURNAL_DETAILS.Account_code,-1*TSPL_JOURNAL_DETAILS.Amount as Amount from TSPL_JOURNAL_DETAILS " & _
            " left outer join TSPL_JOURNAL_MASTER on TSPL_JOURNAL_MASTER.Voucher_No=TSPL_JOURNAL_DETAILS.Voucher_No" & _
            " where TSPL_JOURNAL_MASTER.Source_Doc_No='" + obj.PROD_ENTRY_CODE + "' and Source_Code in ('PR-ER') "

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim ArryLstGLAC As ArrayList = New ArrayList()
                For Each dr As DataRow In dt.Rows
                    Dim Acc() As String = {clsCommon.myCstr(dr("Account_code")), clsCommon.myCdbl(dr("Amount"))}
                    ArryLstGLAC.Add(Acc)
                Next
                transportSql.FunGrnlEntryWithTrans(obj.LOCATION_CODE, False, trans, obj.Prod_Date, "Against Production Return " + obj.PROD_RETURN_CODE, "PR_RT", "Production Return", obj.PROD_RETURN_CODE, obj.COMMENTS, "V", "", "", objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLstGLAC)
            End If

            '==================================================================================
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function UpdateInventoryMovement(ByVal form_id As String, ByVal Return_Code As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim objDoc As clsProcessProductionReturn = clsProcessProductionReturn.GetData(Return_Code, NavigatorType.Current, trans)
            Dim obj As clsInventoryMovement = Nothing
            Dim objNew As clsInventoryMovementNew = Nothing
            Dim ArrInventoryMovement As List(Of clsInventoryMovement) = New List(Of clsInventoryMovement)
            Dim ArrInventoryMovementNew As List(Of clsInventoryMovementNew) = New List(Of clsInventoryMovementNew)
            Dim qry As String = ""
            Dim dt As DataTable


            '' reverse consumption detail
            qry = " Insert into TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL (PROD_RETURN_CODE,PROD_ENTRY_CODE,CONSM_ITEM_CODE,CONSM_QTY,LOCATION_CODE,UNIT_CODE,FIFO_Cost,LIFO_Cost,Avg_Cost," & _
                  " FAT_Per,SNF_Per,FAT_KG,SNF_KG,Standardization_Code,Fat_Rate,SNF_Rate,Fat_Amt,SNF_Amt)" & _
                  " select '" & Return_Code & "' as PROD_RETURN_CODE ,PROD_ENTRY_CODE,CONSM_ITEM_CODE,-CONSM_QTY as CONSM_QTY,LOCATION_CODE,UNIT_CODE,-FIFO_Cost as FIFO_Cost , " & _
                  " -LIFO_Cost as LIFO_Cost,-Avg_Cost as Avg_Cost,FAT_Per,SNF_Per,-FAT_KG as FAT_KG,-SNF_KG as SNF_KG,Standardization_Code,Fat_Rate,SNF_Rate,-Fat_Amt,-SNF_Amt  " & _
                  " from TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL where  " & If(clsCommon.CompairString(objDoc.Transaction_Type, "PE") = CompairStringResult.Equal, "PROD_ENTRY_CODE", "Standardization_Code") & "='" & objDoc.PROD_ENTRY_CODE & "' and PROD_RETURN_CODE is null"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = " select 'PP-PR' as Trans_Type,(case when InOut='I' then 'O' else 'I' end) as InOut,Location_Code,Item_Code,Item_Desc,Qty," & _
                  " UOM,Source_Doc_No,'Return Date',(CASE WHEN InOut='O' THEN (CASE WHEN Qty<=0 THEN 0 ELSE  Avg_Cost/Qty END) ELSE  Basic_Cost END) AS Basic_Cost,Rec_Cost,Add_Cost,Net_Cost,'Created_By',Comp_Code,ItemType,'Punching_Date',MRP,Batch_No," & _
                  " FIFO_Cost,LIFO_Cost,Avg_Cost,'Posting_Date',PI_Cost,Stock_UOM,Stock_Qty,MFG_Date,Expiry_Date,Item_Status,Assmbly_Status," & _
                  " IS_CONSUMPTION, Cust_Code, Cust_Name, Vendor_Name, Other_Location_Code, Other_Location_Desc, Fat_Per, SNF_Per, Fat_KG, SNF_KG, Fat_Rate, SNF_Rate, Fat_Amt, SNF_Amt,Vendor_Code,Vendor_Name " & _
                  " from TSPL_INVENTORY_MOVEMENT  where Trans_Type=(case when '" & objDoc.Transaction_Type & "'='PE' then  'PROD_ENTRY' else 'PP_STDN' end) and Source_Doc_No='" & objDoc.PROD_ENTRY_CODE & "'"
                '' update inventory for consumption
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            For Each dr As DataRow In dt.Rows
                obj = New clsInventoryMovement
                obj.Add_Cost = clsCommon.myCdbl(dr.Item("Add_Cost"))
                obj.Avg_Cost = clsCommon.myCdbl(dr.Item("Avg_Cost"))
                obj.Basic_Cost = clsCommon.myCdbl(dr.Item("Basic_Cost"))
                obj.Batch_No = clsCommon.myCstr(dr.Item("Batch_No"))
                obj.Cust_Code = clsCommon.myCstr(dr.Item("Cust_Code"))
                obj.Cust_Name = clsCommon.myCstr(dr.Item("Cust_Name"))
                obj.Entry_Date = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd-MMM-yyyy")
                If IsDBNull(dr.Item("Expiry_Date")) Then
                    obj.Expiry_Date = Nothing
                Else
                    obj.Expiry_Date = clsCommon.GetPrintDate(dr.Item("Expiry_Date"), "dd-MMM-yyyy")
                    End If
                obj.Fat_Amt = clsCommon.myCdbl(dr.Item("Fat_Amt"))
                obj.FAT_KG = clsCommon.myCdbl(dr.Item("FAT_KG"))
                obj.FAT_Per = clsCommon.myCdbl(dr.Item("FAT_Per"))
                obj.Fat_Rate = clsCommon.myCdbl(dr.Item("Fat_Rate"))
                obj.FIFO_Cost = clsCommon.myCdbl(dr.Item("FIFO_Cost"))
                obj.InOut = clsCommon.myCstr(dr.Item("InOut"))
                obj.IS_CONSUMPTION = clsCommon.myCdbl(dr.Item("IS_CONSUMPTION"))
                obj.Item_Code = clsCommon.myCstr(dr.Item("Item_Code"))
                obj.Item_Desc = clsCommon.myCstr(dr.Item("Item_Desc"))
                obj.itemstatus = clsCommon.myCstr(dr.Item("Item_Status"))
                obj.ItemType = clsCommon.myCstr(dr.Item("ItemType"))
                obj.LIFO_Cost = clsCommon.myCdbl(dr.Item("LIFO_Cost"))
                obj.Location_Code = clsCommon.myCstr(dr.Item("Location_Code"))
                If IsDBNull(dr.Item("MFG_Date")) Then
                    obj.MFG_Date = Nothing
                Else
                    obj.MFG_Date = clsCommon.GetPrintDate(dr.Item("MFG_Date"), "dd-MMM-yyyy")
                    End If
                obj.MRP = clsCommon.myCdbl(dr.Item("MRP"))
                obj.Net_Cost = clsCommon.myCdbl(dr.Item("Net_Cost"))
                obj.Other_Location_Code = clsCommon.myCstr(dr.Item("Other_Location_Code"))
                obj.Other_Location_Desc = clsCommon.myCstr(dr.Item("Other_Location_Desc"))
                obj.Posting_Date = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd-MMM-yyyy")
                obj.Punching_Date = clsCommon.GetPrintDate(objDoc.RETURN_DATE, "dd-MMM-yyyy")
                obj.Qty = clsCommon.myCdbl(dr.Item("Qty"))
                obj.Rec_Cost = clsCommon.myCdbl(dr.Item("Rec_Cost"))
                obj.SNF_Amt = clsCommon.myCdbl(dr.Item("SNF_Amt"))
                obj.SNF_KG = clsCommon.myCdbl(dr.Item("SNF_KG"))
                obj.SNF_Per = clsCommon.myCdbl(dr.Item("SNF_Per"))
                obj.SNF_Rate = clsCommon.myCdbl(dr.Item("SNF_Rate"))
                obj.Source_Doc_Date = clsCommon.GetPrintDate(objDoc.RETURN_DATE, "dd-MMM-yyyy")
                obj.Source_Doc_No = objDoc.PROD_RETURN_CODE
                obj.Stock_Qty = clsCommon.myCdbl(dr.Item("Stock_Qty"))
                obj.Stock_UOM = clsCommon.myCstr(dr.Item("Stock_UOM"))
                obj.Trans_Type = clsCommon.myCstr(dr.Item("Trans_Type"))
                obj.UOM = clsCommon.myCstr(dr.Item("UOM"))
                obj.Vendor_Code = clsCommon.myCstr(dr.Item("Vendor_Code"))
                obj.Vendor_Name = clsCommon.myCstr(dr.Item("Vendor_Name"))
                If clsCommon.CompairString(obj.InOut, "O") = CompairStringResult.Equal Then
                        '' check balance 
                    Dim bal As Decimal = clsItemLocationDetails.getBalance(obj.Item_Code, obj.Location_Code, obj.Source_Doc_No, clsCommon.GetPrintDate(obj.Source_Doc_Date, "dd-MMM-yyyy"), trans, obj.UOM, 0)
                    If bal < obj.Qty Then
                        Throw New Exception("Insuficient Stock: Item -" & obj.Item_Code & ", Location- " & obj.Location_Code & " UOM: " & obj.UOM & " Available Qty: " & bal & " Required Qty: " & obj.Qty & "")
                        End If

                    End If
                ArrInventoryMovement.Add(obj)
            Next
            dt = New DataTable
                '' milk inventory
            qry = " select 'PP-PR' as Trans_Type,(case when InOut='I' then 'O' else 'I' end) as InOut,Location_Code,main_location,Item_Code,Item_Desc,Qty," & _
                 " UOM,Source_Doc_No,'Return Date',(CASE WHEN InOut='O' THEN (CASE WHEN Qty<=0 THEN 0 ELSE  Avg_Cost/Qty END) ELSE  Basic_Cost END) AS Basic_Cost,Rec_Cost,Add_Cost,Net_Cost,'Created_By',Comp_Code,ItemType,'Punching_Date',MRP,Batch_No," & _
                 " FIFO_Cost,LIFO_Cost,Avg_Cost,'Posting_Date',PI_Cost,Stock_UOM,Stock_Qty,MFG_Date,Expiry_Date,Item_Status,Assmbly_Status," & _
                 " IS_CONSUMPTION, Cust_Code, Cust_Name, Vendor_Name, Other_Location_Code, Other_Location_Desc, Fat_Per, SNF_Per, Fat_KG, SNF_KG, Fat_Rate, SNF_Rate, Fat_Amt, SNF_Amt,Vendor_Code,Vendor_Name " & _
                 " from TSPL_INVENTORY_MOVEMENT_NEW  where Trans_Type=(case when '" & objDoc.Transaction_Type & "'='PE' then  'PROD_ENTRY' else 'PP_STDN' end) and Source_Doc_No='" & objDoc.PROD_ENTRY_CODE & "'"
                '' update inventory for consumption
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            For Each dr As DataRow In dt.Rows
                objNew = New clsInventoryMovementNew
                objNew.Add_Cost = clsCommon.myCdbl(dr.Item("Add_Cost"))
                objNew.Avg_Cost = clsCommon.myCdbl(dr.Item("Avg_Cost"))
                objNew.Basic_Cost = clsCommon.myCdbl(dr.Item("Basic_Cost"))
                objNew.Batch_No = clsCommon.myCstr(dr.Item("Batch_No"))
                objNew.Cust_Code = clsCommon.myCstr(dr.Item("Cust_Code"))
                objNew.Cust_Name = clsCommon.myCstr(dr.Item("Cust_Name"))
                objNew.Entry_Date = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd-MMM-yyyy")
                If IsDBNull(dr.Item("Expiry_Date")) Then
                    objNew.Expiry_Date = Nothing
                Else
                    objNew.Expiry_Date = clsCommon.GetPrintDate(dr.Item("Expiry_Date"), "dd-MMM-yyyy")
                    End If
                objNew.Fat_Amt = clsCommon.myCdbl(dr.Item("Fat_Amt"))
                objNew.FAT_KG = clsCommon.myCdbl(dr.Item("FAT_KG"))
                objNew.FAT_Per = clsCommon.myCdbl(dr.Item("FAT_Per"))
                objNew.Fat_Rate = clsCommon.myCdbl(dr.Item("Fat_Rate"))
                objNew.FIFO_Cost = clsCommon.myCdbl(dr.Item("FIFO_Cost"))
                objNew.InOut = clsCommon.myCstr(dr.Item("InOut"))
                objNew.IS_CONSUMPTION = clsCommon.myCdbl(dr.Item("IS_CONSUMPTION"))
                objNew.Item_Code = clsCommon.myCstr(dr.Item("Item_Code"))
                objNew.Item_Desc = clsCommon.myCstr(dr.Item("Item_Desc"))
                objNew.itemstatus = clsCommon.myCstr(dr.Item("Item_Status"))
                objNew.ItemType = clsCommon.myCstr(dr.Item("ItemType"))
                objNew.LIFO_Cost = clsCommon.myCdbl(dr.Item("LIFO_Cost"))
                objNew.Location_Code = clsCommon.myCstr(dr.Item("Location_Code"))
                objNew.main_location = clsCommon.myCstr(dr.Item("main_location"))
                If IsDBNull(dr.Item("MFG_Date")) Then
                    objNew.MFG_Date = Nothing
                Else
                    objNew.MFG_Date = clsCommon.GetPrintDate(dr.Item("MFG_Date"), "dd-MMM-yyyy")
                    End If
                objNew.MRP = clsCommon.myCdbl(dr.Item("MRP"))
                objNew.Net_Cost = clsCommon.myCdbl(dr.Item("Net_Cost"))
                objNew.Other_Location_Code = clsCommon.myCstr(dr.Item("Other_Location_Code"))
                objNew.Other_Location_Desc = clsCommon.myCstr(dr.Item("Other_Location_Desc"))
                objNew.Posting_Date = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd-MMM-yyyy")
                objNew.Punching_Date = clsCommon.GetPrintDate(objDoc.RETURN_DATE, "dd-MMM-yyyy")
                objNew.Qty = clsCommon.myCdbl(dr.Item("Qty"))
                objNew.Rec_Cost = clsCommon.myCdbl(dr.Item("Rec_Cost"))
                objNew.SNF_Amt = clsCommon.myCdbl(dr.Item("SNF_Amt"))
                objNew.SNF_KG = clsCommon.myCdbl(dr.Item("SNF_KG"))
                objNew.SNF_Per = clsCommon.myCdbl(dr.Item("SNF_Per"))
                objNew.SNF_Rate = clsCommon.myCdbl(dr.Item("SNF_Rate"))
                objNew.Source_Doc_Date = clsCommon.GetPrintDate(objDoc.RETURN_DATE, "dd-MMM-yyyy")
                objNew.Source_Doc_No = objDoc.PROD_RETURN_CODE
                objNew.Stock_Qty = clsCommon.myCdbl(dr.Item("Stock_Qty"))
                objNew.Stock_UOM = clsCommon.myCstr(dr.Item("Stock_UOM"))
                objNew.Trans_Type = clsCommon.myCstr(dr.Item("Trans_Type"))
                objNew.UOM = clsCommon.myCstr(dr.Item("UOM"))
                objNew.Vendor_Code = clsCommon.myCstr(dr.Item("Vendor_Code"))
                objNew.Vendor_Name = clsCommon.myCstr(dr.Item("Vendor_Name"))
                If clsCommon.CompairString(objNew.InOut, "O") = CompairStringResult.Equal Then
                        '' check balance 
                    Dim bal As Decimal = clsInventoryMovementNew.getBalance(objNew.Item_Code, objNew.main_location, objNew.Location_Code, objNew.Source_Doc_No, clsCommon.GetPrintDate(objNew.Source_Doc_Date, "dd-MMM-yyyy"), trans, objNew.UOM)
                    If bal < objNew.Qty Then
                        Throw New Exception("Insuficient Stock: Item -" & objNew.Item_Code & ", Location- " & objNew.Location_Code & " UOM: " & objNew.UOM & " Available Qty: " & bal & " Required Qty: " & objNew.Qty & "")
                        End If

                    End If
                ArrInventoryMovementNew.Add(objNew)
            Next
            If ArrInventoryMovement.Count > 0 Then
                clsInventoryMovement.SaveData(form_id, objDoc.PROD_RETURN_CODE, clsCommon.GetPrintDate(objDoc.RETURN_DATE, "dd/MMM/yyyy"), clsCommon.GetPrintDate(objDoc.RETURN_DATE, "dd/MM/yyyy"), ArrInventoryMovement, trans)
                End If
            If ArrInventoryMovementNew.Count > 0 Then
                clsInventoryMovementNew.SaveData(form_id, objDoc.PROD_RETURN_CODE, clsCommon.GetPrintDate(objDoc.RETURN_DATE, "dd/MMM/yyyy"), clsCommon.GetPrintDate(objDoc.RETURN_DATE, "dd/MM/yyyy"), ArrInventoryMovementNew, trans)
                End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
            Return False

        End Try
        Return True
    End Function
    Public Shared Function GetProductionFinder(ByVal whrCls As String, ByVal currCode As String, ByVal isButtonClicked As Boolean) As String
        Dim qry As String = "SELECT TSPL_PP_PRODUCTION_ENTRY.PROD_ENTRY_CODE AS Code,TSPL_PP_PRODUCTION_ENTRY.DESCRIPTION,TSPL_PP_PRODUCTION_ENTRY.Batch_Code as [Batch Code],TSPL_PP_PRODUCTION_ENTRY.LOCATION_CODE as [Location Code],TSPL_PP_PRODUCTION_ENTRY.CONSM_LOCATION_CODE,TSPL_PP_PRODUCTION_ENTRY.CONSM_SECTION_CODE,TSPL_PP_PRODUCTION_ENTRY.Section_Stage_Map_Code,TSPL_PP_PRODUCTION_ENTRY.PROD_DATE, "
        qry += " TSPL_PP_PRODUCTION_ENTRY.MODIFIED_BY AS APPROVED_BY,TSPL_PP_PRODUCTION_ENTRY.Created_By,TSPL_PP_PRODUCTION_ENTRY.POSTED,TSPL_PP_PRODUCTION_ENTRY.POSTING_DATE,TSPL_PP_PRODUCTION_RETURN.PROD_RETURN_CODE as [Prod Return Code], " & _
        " TSPL_PP_PRODUCTION_RETURN.ManualBatchNo,TSPL_PP_PRODUCTION_RETURN.Line_No as [Line No],TSPL_PP_PRODUCTION_RETURN.CostCenterCode as [Cost Center Code] , TSPL_CostCenter_MASTER.Cost_name as [Cost Center Name], TSPL_PP_PRODUCTION_RETURN.ProfitCenterCode as [Profit Center Code]  ,TSPL_PROFIT_CENTER_MASTER.Name as [Profit Center Name] " & _
        " FROM TSPL_PP_PRODUCTION_ENTRY left join TSPL_PP_PRODUCTION_RETURN on TSPL_PP_PRODUCTION_ENTRY.PROD_ENTRY_CODE=TSPL_PP_PRODUCTION_RETURN.PROD_ENTRY_CODE " & _
        " left outer join TSPL_PROFIT_CENTER_MASTER on TSPL_PROFIT_CENTER_MASTER.Code =TSPL_PP_PRODUCTION_RETURN.ProfitCenterCode " & _
        " left outer join TSPL_CostCenter_MASTER on TSPL_CostCenter_MASTER.Cost_Code =TSPL_PP_PRODUCTION_RETURN.CostCenterCode "

        Dim str As String = ""
        If clsCommon.myLen(whrCls) > 0 Then
            whrCls = whrCls + " and TSPL_PP_PRODUCTION_ENTRY.comp_code='" + objCommonVar.CurrentCompanyCode + "'"
        Else
            whrCls = " TSPL_PP_PRODUCTION_ENTRY.comp_code='" + objCommonVar.CurrentCompanyCode + "'"
        End If
        str = clsCommon.ShowSelectForm("STD", qry, "Code", whrCls, currCode, "Code", isButtonClicked)

        Return str
    End Function
    Public Shared Function UpdateBatchItem(ByVal strCode As String, strProReturncode As String, ByVal NavTyep As NavigatorType, ByVal strLoc As String, trans As SqlTransaction) As Boolean
        Dim obj As New clsProductionEntry()
        Dim obj1 As New clsProductionEntryDetail()
        obj = clsProductionEntry.GetData(strCode, strLoc, NavTyep, trans)
        If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Batch_Code) > 0 Then
            Dim arr_BatchItem As New List(Of String)
            arr_BatchItem = New List(Of String)
            If (obj.ArrBatchItem IsNot Nothing AndAlso obj.ArrBatchItem.Count > 0) Then
                For Each objTr As clsProductionEntryDetail In obj.ArrBatchItem
                    If clsItemMaster.IsBatchItem(objTr.ITEM_CODE, trans) Then
                        Dim count As Integer = 1
                        obj1 = New clsProductionEntryDetail()
                        obj1.arrBatchItem = New List(Of clsBatchInventory)
                        Dim objBatchInv As clsBatchInventory = New clsBatchInventory()
                        objBatchInv.arr = New List(Of clsBatchInventory)
                        objBatchInv.Batch_No = obj.Batch_Code
                        objBatchInv.Manufacture_Date = objTr.MFG_DATE
                        objBatchInv.Expiry_Date = objTr.EXP_DATE
                        objBatchInv.Qty = objTr.FINAL_PRODUCTION_QTY
                        objBatchInv.Manual_BatchNo = obj.ManualBatchNo

                        If clsCommon.myLen(objBatchInv.Batch_No) > 0 AndAlso objBatchInv.Qty <> 0 Then
                            objBatchInv.arr.Add(objBatchInv)
                        End If
                        obj1.arrBatchItem.Add(objBatchInv)
                        clsBatchInventory.SaveData("PP-PR", strProReturncode, obj.PROD_DATE, "O", objTr.ITEM_CODE, objTr.LOCATION_CODE, count, 0, objTr.UNIT_CODE, obj1.arrBatchItem, trans)
                        count = count + 1
                    End If
                Next
            End If
        End If
        Return True
    End Function
    Public Shared Function LOCATIONRIGTHS() As String
        Try
            Dim obj As New clsMCCCodes()
            obj = clsMCCCodes.GetData()
            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Default_LocCode) > 0 Then
            End If
            If obj.arrLocCodes IsNot Nothing AndAlso clsCommon.myLen(obj.arrLocCodes) > 0 Then
                Return obj.arrLocCodes
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
        Return ""
    End Function
    

End Class

