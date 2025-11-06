Imports System.Data.SqlClient
Imports common

Public Class clsStartBatchEntry
#Region "Variables"
    Public Document_No As String = Nothing
    Public Document_date As Date? = Nothing
    Public Default_Batch As String = Nothing
    Public Remarks As String = Nothing
    Public Status As Integer = 0
    Public Arr As List(Of clsStartBatchEntryDetail) = Nothing
    Public arrItem As ArrayList = Nothing
    Public arrItemType As ArrayList = Nothing
#End Region
    Public Function SaveData(ByVal obj As clsStartBatchEntry, ByVal isNewEntry As Boolean, ByVal strTransType As String, ByVal AutoSave As Boolean) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            SaveData(obj, isNewEntry, Nothing, trans, AutoSave)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Function SaveData(ByVal obj As clsStartBatchEntry, ByVal isNewEntry As Boolean, ByVal strTransType As String, ByVal trans As SqlTransaction, ByVal AutoSave As Boolean) As Boolean
        Dim isSaved As Boolean = True
        Try
            Dim qry As String = "delete from TSPL_START_BATCH_ENTRY_DETAIL where Document_No='" & obj.Document_No & "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            clsDBFuncationality.ExecuteNonQuery("update tspl_item_master set Is_Batch_Item= 0 where Item_Code in (select distinct Item_Code from TSPL_START_BATCH_ENTRY_DETAIL where Document_No = '" & obj.Document_No & "' ) ", trans)
            clsBatchInventory.DeleteData("SBE", obj.Document_No, trans)
            qry = "delete from TSPL_INVENTORY_MOVEMENT where Source_Doc_No='" + obj.Document_No + "' and Trans_Type='SBE'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Document_date", clsCommon.GetPrintDate(obj.Document_date, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Default_Batch", obj.Default_Batch)
            clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks, True)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))
            If isNewEntry Then
                obj.Document_No = clsERPFuncationality.GetNextCode(trans, obj.Document_date, clsDocType.StartBatchEntry, "", "")
                If (clsCommon.myLen(obj.Document_No) <= 0) Then
                    Throw New Exception("Error in Document Code Generation")
                End If
                clsCommon.AddColumnsForChange(coll, "Document_No", obj.Document_No)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))

                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_START_BATCH_ENTRY", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_START_BATCH_ENTRY", OMInsertOrUpdate.Update, "TSPL_START_BATCH_ENTRY.Document_No='" & obj.Document_No & "'", trans)
            End If
            Dim objDetail As New clsStartBatchEntryDetail()
            isSaved = isSaved AndAlso objDetail.SaveData(obj.Document_No, obj.Arr, trans)
            If isSaved Then
                PostData("S-BATCH-ENT", obj, trans)
            End If
        Catch err As Exception

            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function

    Public Function GetData(ByVal strRetCode As String, ByVal NavType As NavigatorType) As clsStartBatchEntry
        Return GetData(strRetCode, NavType, Nothing)
    End Function

    Public Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsStartBatchEntry
        Dim obj As clsStartBatchEntry = Nothing
        Dim qry As String = "select * from TSPL_START_BATCH_ENTRY where 2=2 "
        Select Case NavType
            Case NavigatorType.First
                qry &= " and TSPL_START_BATCH_ENTRY.Document_No = (select MIN(Document_No) from TSPL_START_BATCH_ENTRY)"
            Case NavigatorType.Last
                qry &= " and TSPL_START_BATCH_ENTRY.Document_No = (select Max(Document_No) from TSPL_START_BATCH_ENTRY)"
            Case NavigatorType.Next
                qry &= " and TSPL_START_BATCH_ENTRY.Document_No = (select Min(Document_No) from TSPL_START_BATCH_ENTRY where Document_No >'" & strCode & "')"
            Case NavigatorType.Previous
                qry &= " and TSPL_START_BATCH_ENTRY.Document_No = (select Max(Document_No) from TSPL_START_BATCH_ENTRY where Document_No <'" & strCode & "')"
            Case NavigatorType.Current
                qry &= " and TSPL_START_BATCH_ENTRY.Document_No = '" & strCode & "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            obj = New clsStartBatchEntry()
            obj.Document_No = clsCommon.myCstr(dt.Rows(0)("Document_No"))
            obj.Document_date = clsCommon.myCDate(dt.Rows(0)("Document_date"))
            obj.Default_Batch = clsCommon.myCstr(dt.Rows(0)("Default_Batch"))
            obj.Remarks = clsCommon.myCstr(dt.Rows(0)("Remarks"))
            obj.Status = IIf(clsCommon.myCdbl(dt.Rows(0)("Status")) = 1, ERPTransactionStatus.Approved, ERPTransactionStatus.Pending)
            Dim objDetail As New clsStartBatchEntryDetail()
            obj.Arr = objDetail.GetData(obj.Document_No, trans)
            obj.arrItem = objDetail.GetItem(obj.Document_No, trans)
            obj.arrItemType = objDetail.GetItemType(obj.Document_No, trans)
        End If
        Return obj
    End Function

    Public Function getFinder(ByVal strCode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim sql As String = "select Document_No as DocumentNo ,convert(varchar(12),Document_date,103) as DocumentDate,Default_Batch as [Default Batch],case when Status = 1 then 'posted' else 'Unposted' end as Posted from TSPL_START_BATCH_ENTRY"
        str = clsCommon.ShowSelectForm("CustPnlty", sql, "DocumentNo", "", strCode, "DocumentNo", isButtonClicked)
        Return str
    End Function
    Public Function PostData(ByVal FormId As String, ByVal OBJ As clsStartBatchEntry, ByVal trans As SqlTransaction) As Boolean
        Try
            If (OBJ Is Nothing OrElse clsCommon.myLen(OBJ.Document_No) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            If (OBJ.Status = 1) Then
                Throw New Exception("Already Posted")
            End If

            '----Update inventory movement Out-----
            Dim ArrInventoryMovement As List(Of clsInventoryMovement) = New List(Of clsInventoryMovement)
            clsDBFuncationality.ExecuteNonQuery("update tspl_batch_item set Against_Inv_Movement_Trans_Id=null where Document_Code='" & OBJ.Document_No & "'", trans)
            clsDBFuncationality.ExecuteNonQuery("Delete from TSPL_INVENTORY_MOVEMENT where Source_Doc_No='" & OBJ.Document_No & "'", trans)
            Dim intCounter As Integer = 0
            Dim objInventoryMovemnt As New clsInventoryMovement()

            For Each objTr As clsStartBatchEntryDetail In OBJ.Arr
                If objTr.Qty = 0 AndAlso objTr.Amount = 0 Then
                Else
                    intCounter = intCounter + 1
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
                    End If
                    objInventoryMovemnt = New clsInventoryMovement()
                    objInventoryMovemnt.InOut = "O"

                    objInventoryMovemnt.Location_Code = objTr.Location_Code
                    objInventoryMovemnt.Item_Code = objTr.Item_Code
                    objInventoryMovemnt.Item_Desc = objTr.Item_Desc
                    objInventoryMovemnt.Qty = objTr.Qty
                    objInventoryMovemnt.UOM = objTr.Unit_code
                    objInventoryMovemnt.CalculateAvgCost = False
                    objInventoryMovemnt.Avg_Cost = objTr.Amount
                    objInventoryMovemnt.Basic_Cost = clsCommon.myCDivide(objTr.Amount, objTr.Qty)
                    objInventoryMovemnt.ItemType = strItemTypeToSave
                    ArrInventoryMovement.Add(objInventoryMovemnt)
                End If
            Next
            clsInventoryMovement.SaveData("SBE", OBJ.Document_No, OBJ.Document_date, clsCommon.GetPrintDate(OBJ.Document_date, "dd/MM/yyyy"), ArrInventoryMovement, trans)
            '---End
            clsDBFuncationality.ExecuteNonQuery("update tspl_item_master set Is_Batch_Item= 1 where Item_Code in (select distinct Item_Code from TSPL_START_BATCH_ENTRY_DETAIL where Document_No = '" & OBJ.Document_No & "' ) ", trans)

            '---Batch In
            If objCommonVar.AutoGenrateBatchInventory Then
                For Each objtr As clsStartBatchEntryDetail In OBJ.Arr
                    If objtr.Qty = 0 AndAlso objtr.Amount = 0 Then
                    Else
                        If clsItemMaster.IsBatchItem(objtr.Item_Code, trans) Then
                            Dim ArrBatchItem As New List(Of clsBatchInventory)
                            Dim objBatch As New clsBatchInventory
                            objBatch.Parent_Line_No = objtr.Line_No
                            objBatch.Line_No = 1
                            objBatch.Batch_No = clsERPFuncationality.GetNextCode(trans, clsCommon.GetPrintDate(OBJ.Document_date, "dd/MMM/yyyy"), clsDocType.ItemBatch, clsItemMaster.GetItemType(objtr.Item_Code, trans), objtr.Location_Code, False, True, False, False, False, False, "", clsCommon.GetPrintDate(OBJ.Document_date, "ddMMyyHHmm"), "")
                            objBatch.Manual_BatchNo = objBatch.Batch_No
                            objBatch.Manufacture_Date = OBJ.Document_date
                            objBatch.Expiry_Date = clsCommon.myCDate(OBJ.Document_date).AddDays(clsItemMaster.GetSelfLife(objtr.Item_Code, trans))
                            objBatch.Qty = objtr.Qty
                            ArrBatchItem.Add(objBatch)
                            clsBatchInventory.SaveData("SBE", OBJ.Document_No, OBJ.Document_date, "I", objtr.Item_Code, objtr.Location_Code, objtr.Line_No, 0, objtr.Unit_code, ArrBatchItem, trans)
                        End If
                    End If
                Next
            Else
                For Each objtr As clsStartBatchEntryDetail In OBJ.Arr
                    If objtr.Qty = 0 AndAlso objtr.Amount = 0 Then
                    Else
                        If clsItemMaster.IsBatchItem(objtr.Item_Code, trans) Then
                            clsBatchInventory.SaveData("SBE", OBJ.Document_No, OBJ.Document_date, "I", objtr.Item_Code, objtr.Location_Code, objtr.Line_No, 0, objtr.Unit_code, objtr.arrBatchItem, trans)
                        End If
                    End If
                Next
            End If
            '---End

            '---- Update inventory movement In
            ArrInventoryMovement = New List(Of clsInventoryMovement)
            For Each objTr As clsStartBatchEntryDetail In OBJ.Arr
                If objTr.Qty = 0 AndAlso objTr.Amount = 0 Then
                Else
                    Dim itemtype As String = "select item_type from TSPL_ITEM_MASTER where item_code='" + objTr.Item_Code + "'"
                    Dim type As String = clsDBFuncationality.getSingleValue(itemtype, trans)
                    objInventoryMovemnt = New clsInventoryMovement()
                    objInventoryMovemnt.InOut = "I"
                    objInventoryMovemnt.Location_Code = objTr.Location_Code
                    objInventoryMovemnt.Other_Location_Code = objTr.Location_Code
                    objInventoryMovemnt.Other_Location_Desc = objTr.Location_Desc
                    objInventoryMovemnt.Item_Code = objTr.Item_Code
                    objInventoryMovemnt.Item_Desc = objTr.Item_Desc
                    objInventoryMovemnt.Qty = objTr.Qty
                    objInventoryMovemnt.UOM = objTr.Unit_code
                    objInventoryMovemnt.Basic_Cost = clsCommon.myCDivide(objTr.Amount, objTr.Qty)
                    objInventoryMovemnt.Avg_Cost = objTr.Amount
                    objInventoryMovemnt.CalculateAvgCost = False
                    If clsCommon.CompairString(type, "R") = CompairStringResult.Equal Then
                        objInventoryMovemnt.ItemType = "RM"
                    ElseIf clsCommon.CompairString(type, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(type, "O") = CompairStringResult.Equal Then
                        objInventoryMovemnt.ItemType = "OT"
                    ElseIf clsCommon.CompairString(type, "F") = CompairStringResult.Equal Then
                        objInventoryMovemnt.ItemType = "FT"
                    End If
                    ArrInventoryMovement.Add(objInventoryMovemnt)
                End If
            Next
            clsInventoryMovement.SaveData("SBE", OBJ.Document_No, OBJ.Document_date, clsCommon.GetPrintDate(OBJ.Document_date, "dd/MM/yyyy"), ArrInventoryMovement, trans)
            '---End

            clsDBFuncationality.ExecuteNonQuery("Update TSPL_START_BATCH_ENTRY set Status= 1, Posted_By = '" & objCommonVar.CurrentUserCode & "',Posted_Date = '" & clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt") & "'  where Document_No='" & OBJ.Document_No & "'", trans)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Function DeleteData(ByVal strCode As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            DeleteData(strCode, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Function DeleteData(ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        If (clsCommon.myLen(strCode) <= 0) Then
            Throw New Exception("Document No not found to Delete")
        End If
        Dim obj As clsStartBatchEntry = New clsStartBatchEntry()
        obj = obj.GetData(strCode, NavigatorType.Current, trans)
        Try
            If (obj Is Nothing OrElse clsCommon.myLen(obj.Document_No) <= 0) Then
                Throw New Exception("Document No not found to Delete")
            End If
            Dim qry As String = Nothing
            For Each objtr As clsStartBatchEntryDetail In obj.Arr
                Dim batchNoList As String = String.Join(","c, objtr.arrBatchItem.Where(Function(x) Not String.IsNullOrEmpty(x.Batch_No)).Select(Function(x) "'" & x.Batch_No.Replace("'", "''") & "'").ToArray())
                qry = " select count(1) from TSPL_BATCH_ITEM where item_code ='" & objtr.Item_Code & "' and Document_Type not in ('SBE')  and Batch_No in (" & batchNoList & ")"
                Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))
                If count > 0 Then
                    Throw New Exception("Same Batch No is also used in other transaction")
                End If
            Next
            clsDBFuncationality.ExecuteNonQuery("update tspl_batch_item set Against_Inv_Movement_Trans_Id=null where Document_Code='" & obj.Document_No & "'", trans)
            qry = "delete from TSPL_INVENTORY_MOVEMENT where Source_Doc_No='" + obj.Document_No + "' and Trans_Type='SBE' and InOut='I' "
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            clsBatchInventory.DeleteData("SBE", obj.Document_No, trans)

            clsDBFuncationality.ExecuteNonQuery("update tspl_item_master set Is_Batch_Item= 0 where Item_Code in (select distinct Item_Code from TSPL_START_BATCH_ENTRY_DETAIL where Document_No = '" & obj.Document_No & "' ) ", trans)

            qry = "delete from TSPL_INVENTORY_MOVEMENT where Source_Doc_No='" + obj.Document_No + "' and Trans_Type='SBE' and InOut='O' "
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_START_BATCH_ENTRY_DETAIL where Document_No='" & obj.Document_No & "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_START_BATCH_ENTRY where Document_No='" & obj.Document_No & "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
End Class

Public Class clsStartBatchEntryDetail

#Region "Variables"
    Public Document_No As String = Nothing
    Public Line_No As Integer = 0
    Public Location_Code As String = Nothing
    Public Location_Desc As String = Nothing
    Public Item_Code As String = Nothing
    Public Item_Desc As String = Nothing
    Public Qty As Decimal
    Public Unit_code As String = Nothing
    Public Amount As Decimal
    Public arrBatchItem As List(Of clsBatchInventory) = Nothing
#End Region
    Public Function SaveData(ByVal strCode As String, ByVal Arr As List(Of clsStartBatchEntryDetail), ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsStartBatchEntryDetail In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Document_No", strCode)
                clsCommon.AddColumnsForChange(coll, "Line_No", obj.Line_No)
                clsCommon.AddColumnsForChange(coll, "Location_Code", obj.Location_Code)
                clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code)
                clsCommon.AddColumnsForChange(coll, "Qty", obj.Qty)
                clsCommon.AddColumnsForChange(coll, "Unit_code", obj.Unit_code)
                clsCommon.AddColumnsForChange(coll, "Amount", obj.Amount)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_START_BATCH_ENTRY_DETAIL", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function

    Public Function GetData(ByVal strCode As String, ByVal trans As SqlTransaction) As List(Of clsStartBatchEntryDetail)
        Dim arr As List(Of clsStartBatchEntryDetail) = Nothing
        Dim qry As String = "select TSPL_START_BATCH_ENTRY_DETAIL.* ,TSPL_LOCATION_MASTER.Location_Desc,TSPL_ITEM_MASTER.ITEM_DESC
         from TSPL_START_BATCH_ENTRY_DETAIL LEFT OUTER JOIN TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.ITEM_CODE = TSPL_START_BATCH_ENTRY_DETAIL.ITEM_CODE LEFT OUTER JOIN TSPL_LOCATION_MASTER 
   ON TSPL_LOCATION_MASTER.LOCATION_CODE = TSPL_START_BATCH_ENTRY_DETAIL.LOCATION_CODE where  TSPL_START_BATCH_ENTRY_DETAIL.Document_No = '" & strCode & "' order by Document_No,PK_ID "
        Dim PK_ID As Integer = 0
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            arr = New List(Of clsStartBatchEntryDetail)()
            For Each dr As DataRow In dt.Rows
                Dim obj As clsStartBatchEntryDetail = New clsStartBatchEntryDetail()
                obj.Document_No = clsCommon.myCstr(dr("Document_No"))
                obj.Line_No = clsCommon.myCdbl(dr("Line_No"))
                obj.Location_Code = clsCommon.myCstr(dr("Location_Code"))
                obj.Location_Desc = clsCommon.myCstr(dr("Location_Desc"))
                obj.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                obj.Item_Desc = clsCommon.myCstr(dr("Item_Desc"))
                obj.Qty = clsCommon.myCDecimal(dr("Qty"))
                obj.Unit_code = clsCommon.myCstr(dr("Unit_code"))
                obj.Amount = clsCommon.myCDecimal(dr("Amount"))
                obj.arrBatchItem = clsBatchInventory.GetData("SBE", obj.Document_No, obj.Item_Code, obj.Line_No, trans)
                arr.Add(obj)
            Next
        End If
        Return arr
    End Function

    Public Function GetItem(ByVal strCode As String, ByVal trans As SqlTransaction) As ArrayList
        Dim arrItem As ArrayList = Nothing
        Dim qry As String = "select distinct TSPL_START_BATCH_ENTRY_DETAIL.ITEM_CODE from TSPL_START_BATCH_ENTRY_DETAIL LEFT OUTER JOIN TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.ITEM_CODE = TSPL_START_BATCH_ENTRY_DETAIL.ITEM_CODE where TSPL_START_BATCH_ENTRY_DETAIL.Document_No = '" & strCode & "' "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            arrItem = New ArrayList()
            For Each dr As DataRow In dt.Rows
                arrItem.Add(clsCommon.myCstr(dr("ITEM_CODE")))
            Next
        End If
        Return arrItem
    End Function

    Public Function GetItemType(ByVal strCode As String, ByVal trans As SqlTransaction) As ArrayList
        Dim arrItemType As ArrayList = Nothing
        Dim qry As String = "select distinct TSPL_ITEM_MASTER.Item_Type from TSPL_START_BATCH_ENTRY_DETAIL LEFT OUTER JOIN TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.ITEM_CODE = TSPL_START_BATCH_ENTRY_DETAIL.ITEM_CODE where TSPL_START_BATCH_ENTRY_DETAIL.Document_No = '" & strCode & "' "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            arrItemType = New ArrayList()
            For Each dr As DataRow In dt.Rows
                arrItemType.Add(clsCommon.myCstr(dr("Item_Type")))
            Next
        End If
        Return arrItemType
    End Function

End Class