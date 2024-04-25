Imports common
Imports System.Data.SqlClient
Imports System.Data.Sql
Public Class clsPhysicalstock

#Region "Variables"
    Public Physical_No As String = Nothing
    Public Line_No As String = Nothing
    Public Description As String = Nothing
    Public Stock_Date As DateTime = Nothing
    Public Location_Code As String = Nothing
    Public Main_Location As String = Nothing
    Public Item_Code As String = Nothing
    Public Item_Desc As String = Nothing

    Public GL_Account_Inventroy_Ctrl As String = Nothing
    Public GL_Account_Inventroy_CtrlName As String = Nothing
    Public GL_Account As String = Nothing
    Public GL_AccountName As String = Nothing

    Public MRP As Double = 0
    Public Stock_Unit As String = Nothing
    Public Batch_No As String = Nothing
    Public Existing_Qty As Decimal = 0
    Public Existing_FAT_Pers As Decimal = Nothing
    Public Existing_FAT_Kg As Decimal = Nothing
    Public Existing_FAT_Amt As Decimal
    Public Existing_SNF_Pers As Decimal = Nothing
    Public Existing_SNF_Kg As Decimal = Nothing
    Public Existing_SNF_Amt As Decimal
    Public Existing_Amount As Decimal

    Public Physical_Qty As Decimal = 0
    Public FAT_Pers As Decimal = Nothing
    Public FAT_Kg As Decimal = Nothing
    Public FAT_Amt As Decimal
    Public SNF_Pers As Decimal = Nothing
    Public SNF_Kg As Decimal = Nothing
    Public SNF_Amt As Decimal
    Public Amt As Decimal

    Public Difference As Decimal = 0
    Public FatPerDifference As Decimal = 0
    Public FatKgDifference As Decimal = 0
    Public FatAmtDifference As Decimal = 0
    Public SNFPerDifference As Decimal = 0
    Public SNFKgDifference As Decimal = 0
    Public SNFAmtDifference As Decimal
    Public DifferenceAmt As Decimal

    Public Is_Milk As Integer = Nothing
    Public Is_Posted As Integer = Nothing

    Public arrSrItem As List(Of clsSerializeInvenotry) = Nothing
    Public arrBatchItem As List(Of clsBatchInventory) = Nothing
    Public Nill_Balance As Boolean = False
    Public Multiple_Location As Boolean = False
    Public Arr As List(Of clsPhysicalstock) = Nothing
#End Region

    Private Property is_MRPWise As Integer

    Public Shared Function GetFinder(ByVal whrCls As String, ByVal strCurrCode As String, ByVal isButtonClicked As Boolean) As String
        Dim qry As String = " select *  from  (select TSPL_PHYSICAL_STOCK.physical_no as Code,max(TSPL_PHYSICAL_STOCK.Description) as Description,max(TSPL_PHYSICAL_STOCK.stock_Date) as [Date],max(TSPL_PHYSICAL_STOCK.location) as [Location Code],max(tspl_location_master.location_desc) as [Location],max(TSPL_PHYSICAL_STOCK.silo_location) as [Sub Location],(case when max( TSPL_PHYSICAL_STOCK.is_milk)=0 then '' else 'Milk Type' end) as [Type],case when max(TSPL_PHYSICAL_STOCK.Is_Posted)=1 then 'Posted' else 'Pending' end as Posted,max(TSPL_PHYSICAL_STOCK.Multiple_Location) as Multiple_Location from TSPL_PHYSICAL_STOCK "
        qry += "left outer join tspl_location_master on tspl_location_master.location_code=TSPL_PHYSICAL_STOCK.location group by TSPL_PHYSICAL_STOCK.physical_no ) xx "

        Dim str As String = ""
        str = clsCommon.ShowSelectForm("PHYDOCFND", qry, "Code", whrCls, strCurrCode, "Code", isButtonClicked)

        Return str
    End Function

    Public Shared Function SaveData(ByVal obj As clsPhysicalstock, ByVal physicalNo As String, ByVal isNewEntry As Boolean) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            SaveData(obj, physicalNo, isNewEntry, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function SaveData(ByVal obj1 As clsPhysicalstock, ByVal physicalNo As String, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim qry As String = ""
        Dim whrCls As String = ""

        Try
            If (obj1.Arr IsNot Nothing AndAlso obj1.Arr.Count > 0) Then
                If isNewEntry Then
                    qry = "select max(Physical_No) from tspl_physical_stock"
                    physicalNo = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))

                    If clsCommon.myLen(physicalNo) > 0 Then
                        physicalNo = clsCommon.incval(physicalNo)
                    Else
                        physicalNo = "PHYSTK0000000000000000001"
                    End If
                End If
                If Not isNewEntry Then
                    clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, physicalNo, "tspl_physical_stock", "Physical_No", "TSPL_ADJUSTMENT_HEADER", "against_physical_stock_no", trans)
                End If
                obj1.Physical_No = physicalNo
                clsDBFuncationality.ExecuteNonQuery("delete from tspl_physical_stock where physical_no='" + physicalNo + "'", trans)
                clsDBFuncationality.ExecuteNonQuery("Delete from TSPL_SERIAL_ITEM where Document_Type='IC-AD' and Document_Code in (select adjustment_no from TSPL_ADJUSTMENT_HEADER where against_physical_stock_no='" + physicalNo + "')", trans)
                clsDBFuncationality.ExecuteNonQuery("delete from TSPL_INVENTORY_MOVEMENT where trans_type='IC-AD' and source_doc_no in (select adjustment_no from TSPL_ADJUSTMENT_HEADER where against_physical_stock_no='" + physicalNo + "')", trans)
                clsDBFuncationality.ExecuteNonQuery("delete from TSPL_INVENTORY_MOVEMENT_NEW where trans_type='IC-AD' and source_doc_no in (select adjustment_no from TSPL_ADJUSTMENT_HEADER where against_physical_stock_no='" + physicalNo + "')", trans)
                clsDBFuncationality.ExecuteNonQuery("delete from TSPL_ADJUSTMENT_DETAIL where adjustment_no in (select adjustment_no from TSPL_ADJUSTMENT_HEADER where against_physical_stock_no='" + physicalNo + "')", trans)
                clsDBFuncationality.ExecuteNonQuery("delete from TSPL_ADJUSTMENT_HEADER where against_physical_stock_no='" + physicalNo + "'", trans)
                clsBatchInventory.DeleteData("PH-ST", physicalNo, trans)
                Dim ii As Integer = 1
                For Each obj As clsPhysicalstock In obj1.Arr
                    Dim coll As New Hashtable()
                    Dim Entrydate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt")

                    If isNewEntry Then
                        obj.Physical_No = physicalNo
                    End If
                    clsCommon.AddColumnsForChange(coll, "GL_Account_Inventroy_Ctrl", obj.GL_Account_Inventroy_Ctrl, True)
                    clsCommon.AddColumnsForChange(coll, "GL_Account", obj.GL_Account, True)
                    clsCommon.AddColumnsForChange(coll, "Physical_No", obj.Physical_No)
                    clsCommon.AddColumnsForChange(coll, "Is_Milk", obj.Is_Milk)
                    clsCommon.AddColumnsForChange(coll, "Description", obj.Description)
                    clsCommon.AddColumnsForChange(coll, "Stock_Date", clsCommon.GetPrintDate(obj.Stock_Date, "dd/MMM/yyyy"))
                    clsCommon.AddColumnsForChange(coll, "Silo_Location", obj.Location_Code, True)
                    clsCommon.AddColumnsForChange(coll, "Location", obj.Main_Location, True)
                    clsCommon.AddColumnsForChange(coll, "Line_No", ii)
                    clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code)
                    clsCommon.AddColumnsForChange(coll, "Item_Desc", obj.Item_Desc)
                    clsCommon.AddColumnsForChange(coll, "MRP", obj.MRP)
                    clsCommon.AddColumnsForChange(coll, "Stock_Unit", obj.Stock_Unit)
                    clsCommon.AddColumnsForChange(coll, "Batch_No", obj.Batch_No)
                    clsCommon.AddColumnsForChange(coll, "Nill_Balance", IIf(obj.Nill_Balance, 1, 0))
                    clsCommon.AddColumnsForChange(coll, "Existing_Qty", obj.Existing_Qty)
                    clsCommon.AddColumnsForChange(coll, "Existing_SNF_Pers", obj.Existing_SNF_Pers)
                    clsCommon.AddColumnsForChange(coll, "Existing_SNF_Kg", obj.Existing_SNF_Kg)
                    clsCommon.AddColumnsForChange(coll, "Existing_SNF_Amt", obj.Existing_SNF_Amt)
                    clsCommon.AddColumnsForChange(coll, "Existing_FAT_Pers", obj.Existing_FAT_Pers)
                    clsCommon.AddColumnsForChange(coll, "Existing_FAT_Kg", obj.Existing_FAT_Kg)
                    clsCommon.AddColumnsForChange(coll, "Existing_FAT_Amt", obj.Existing_FAT_Amt)
                    clsCommon.AddColumnsForChange(coll, "Existing_Amount", obj.Existing_Amount)

                    clsCommon.AddColumnsForChange(coll, "Physical_Qty", obj.Physical_Qty)
                    clsCommon.AddColumnsForChange(coll, "FAT_Kg", obj.FAT_Kg)
                    clsCommon.AddColumnsForChange(coll, "FAT_Pers", obj.FAT_Pers)
                    clsCommon.AddColumnsForChange(coll, "FAT_Amt", obj.FAT_Amt)
                    clsCommon.AddColumnsForChange(coll, "SNF_Kg", obj.SNF_Kg)
                    clsCommon.AddColumnsForChange(coll, "SNF_Pers", obj.SNF_Pers)
                    clsCommon.AddColumnsForChange(coll, "SNF_Amt", obj.SNF_Amt)
                    clsCommon.AddColumnsForChange(coll, "Amt", obj.Amt)
                    clsCommon.AddColumnsForChange(coll, "Multiple_Location", IIf(obj.Multiple_Location, 1, 0))
                    clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "Modified_Date", Entrydate)
                    clsCommon.AddColumnsForChange(coll, "Comp_code", objCommonVar.CurrentCompanyCode)

                    clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "Created_Date", Entrydate)
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PHYSICAL_STOCK", OMInsertOrUpdate.Insert, "", trans)

                    clsBatchInventory.SaveData("PH-ST", obj.Physical_No, obj.Stock_Date, IIf((obj.Existing_Qty - obj.Physical_Qty) < 0, "I", "O"), obj.Item_Code, obj.Main_Location, ii, obj.MRP, obj.Stock_Unit, obj.arrBatchItem, trans)
                    ii += 1
                Next
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function ReverseAndUnpost(ByVal strDocNo As String)
        Dim tran As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim qry As String = "select adjustment_no from TSPL_ADJUSTMENT_HEADER where against_physical_stock_no='" + strDocNo + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, tran)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    Dim VoucherNo As String = clsDBFuncationality.getSingleValue("select Voucher_No from TSPL_JOURNAL_MASTER where Source_Code in('IC-AD', 'GL-JE') and Source_Doc_No='" + clsCommon.myCstr(dr("adjustment_no")) + "'", tran)
                    If clsCommon.myLen(VoucherNo) > 0 Then
                        clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, VoucherNo, "TSPL_JOURNAL_MASTER", "Voucher_No", "TSPL_JOURNAL_DETAILS", "Voucher_No", tran)
                        qry = "delete from TSPL_JOURNAL_DETAILS where Voucher_No ='" + VoucherNo + "'"
                        clsDBFuncationality.ExecuteNonQuery(qry, tran)
                        qry = "delete from TSPL_JOURNAL_MASTER where Voucher_No ='" + VoucherNo + "'"
                        clsDBFuncationality.ExecuteNonQuery(qry, tran)
                    End If

                    qry = "update TSPL_BATCH_ITEM set Against_Inv_Movement_Trans_Id=null where Against_Inv_Movement_Trans_Id in (select Trans_Id from TSPL_INVENTORY_MOVEMENT where Source_Doc_No='" + clsCommon.myCstr(dr("adjustment_no")) + "' and Trans_Type='IC-AD')"
                    clsDBFuncationality.ExecuteNonQuery(qry, tran)
                    qry = " update TSPL_BATCH_ITEM_New set Against_Inv_Movement_New_Trans_Id=null where Against_Inv_Movement_New_Trans_Id in (select Trans_Id from TSPL_INVENTORY_MOVEMENT_NEW where Source_Doc_No='" + clsCommon.myCstr(dr("adjustment_no")) + "' and Trans_Type='IC-AD') "
                    clsDBFuncationality.ExecuteNonQuery(qry, tran)

                    clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strDocNo, "TSPL_INVENTORY_MOVEMENT", "Source_Doc_No", tran)
                    clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strDocNo, "TSPL_INVENTORY_MOVEMENT_New", "Source_Doc_No", tran)

                    qry = "delete from TSPL_INVENTORY_MOVEMENT where Source_Doc_No='" + clsCommon.myCstr(dr("adjustment_no")) + "' and Trans_Type='IC-AD'"
                    clsDBFuncationality.ExecuteNonQuery(qry, tran)
                    qry = "delete from TSPL_INVENTORY_MOVEMENT_New where Source_Doc_No='" + clsCommon.myCstr(dr("adjustment_no")) + "' and Trans_Type='IC-AD'"
                    clsDBFuncationality.ExecuteNonQuery(qry, tran)

                    qry = "Update TSPL_ADJUSTMENT_HEADER set Posted = 'N' where adjustment_no='" + clsCommon.myCstr(dr("adjustment_no")) + "'"
                    clsDBFuncationality.ExecuteNonQuery(qry, tran)

                    ClsAdjustments.DeleteData(clsCommon.myCstr(dr("adjustment_no")), "", tran)
                Next
            End If

            clsDBFuncationality.ExecuteNonQuery("Update TSPL_PHYSICAL_STOCK set Is_Posted=0 where Physical_No ='" + strDocNo + "'", tran)
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strDocNo, "tspl_physical_stock", "Physical_No", tran)
            'Batch Inventory
            qry = "SELECT code FROM TSPL_BATCH_ITEM_INDIRECT WHERE Document_Type='PH-ST' and Document_Code='" + strDocNo + "' order by code"
            dt = clsDBFuncationality.GetDataTable(qry, tran)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                qry = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT max(code) FROM TSPL_BATCH_ITEM", tran))
                For Each dr As DataRow In dt.Rows
                    qry = clsCommon.incval(qry)
                    clsDBFuncationality.ExecuteNonQuery("Update TSPL_BATCH_ITEM_INDIRECT set code='" + qry + "' where code='" + clsCommon.myCstr(dr("code")) + "' ", tran)
                Next
            End If
            Dim strInvColumns As String = clsERPFuncationality.GetTableColumnNameForQry("TSPL_BATCH_ITEM", tran)
            strInvColumns = "[" + strInvColumns.Replace(",", "],[") + "]"
            qry = "INSERT INTO TSPL_BATCH_ITEM (" + strInvColumns + ") SELECT " + strInvColumns + " FROM TSPL_BATCH_ITEM_INDIRECT WHERE Document_Type='PH-ST' and Document_Code='" + strDocNo + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, tran)

            qry = "Delete from TSPL_BATCH_ITEM_INDIRECT where Document_Type='PH-ST' and Document_Code='" + strDocNo + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, tran)
            'End of Batch Inventory

            tran.Commit()
        Catch ex As Exception
            tran.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True

    End Function
    Public Shared Function PostData(ByVal strDocNo As String, ByVal is_Milk As Boolean) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin
        Try
            Dim isCheckForPosted As Boolean = True
            Dim qry As String = "select Location,Silo_Location from TSPL_PHYSICAL_STOCK where Physical_No='" + strDocNo + "' group by Location,Silo_Location"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    PostData(strDocNo, isCheckForPosted, trans, clsCommon.myCstr(dr("Location")), clsCommon.myCstr(dr("Silo_Location")), is_Milk)
                    isCheckForPosted = False
                Next
            End If
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function PostData(ByVal strDocNo As String, ByVal isCheckForPosted As Boolean, ByVal location As String, ByVal sublocation As String, ByVal is_Milk As Boolean) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin
        Try
            PostData(strDocNo, isCheckForPosted, trans, location, sublocation, is_Milk)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function PostData(ByVal strDocNo As String, ByVal isCheckForPosted As Boolean, ByVal trans As SqlTransaction, ByVal location As String, ByVal sublocation As String, ByVal is_Milk As Boolean) As Boolean
        Try
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Code not found to Post")
            End If
            Dim qry As String
            Dim strPostDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt")
            Dim arr As New List(Of clsPhysicalstock)
            arr = clsPhysicalstock.GetData(strDocNo, location, sublocation, is_Milk, NavigatorType.Current, trans, "")
            For Each obj As clsPhysicalstock In arr
                If (arr Is Nothing OrElse clsCommon.myLen(obj.Physical_No) <= 0) Then
                    Throw New Exception("No Data found to Post")
                End If
                If (isCheckForPosted AndAlso obj.Is_Posted = 1) Then
                    Throw New Exception("Already Posted ")
                End If

                qry = "Update TSPL_PHYSICAL_STOCK set Is_Posted=1, Posted_Date='" + strPostDate + "',Posted_By='" + objCommonVar.CurrentUserCode + "' where Physical_No ='" + strDocNo + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
                clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strDocNo, "tspl_physical_stock", "Physical_No", trans)
                Exit For
            Next
            'Batch Inventory
            Dim strInvColumns As String = clsERPFuncationality.GetTableColumnNameForQry("TSPL_BATCH_ITEM", trans)
            strInvColumns = "[" + strInvColumns.Replace(",", "],[") + "]"
            qry = "INSERT INTO TSPL_BATCH_ITEM_INDIRECT (" + strInvColumns + ") SELECT " + strInvColumns + " FROM TSPL_BATCH_ITEM WHERE Document_Type='PH-ST' and Document_Code='" + strDocNo + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            clsBatchInventory.DeleteData("PH-ST", strDocNo, trans)
            'End of Batch Inventory
            SaveAdjustmentData(arr, trans)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Private Shared Function SaveAdjustmentData(ByVal arr As List(Of clsPhysicalstock), ByVal trans As SqlTransaction) As Boolean
        Try
            Dim obj As New ClsAdjustments()
            obj.Arr = New List(Of ClsAdjustmentsDetails)
            'Dim physicalqty As Decimal = 0
            obj.Adjustment_Type = "ADJ"
            Dim lineNo As Integer = 0
            If arr IsNot Nothing AndAlso arr.Count > 0 Then
                For Each objtr As clsPhysicalstock In arr
                    obj.Trans_Type = "In"
                    If objtr.Difference < 0 OrElse objtr.DifferenceAmt < 0 Then
                        obj.Adjustment_Date = objtr.Stock_Date
                        obj.Reference = ""
                        obj.Description = "Auto Adjustment Created due to Physical stock entry no " + objtr.Physical_No + " on " + clsCommon.GetPrintDate(objtr.Stock_Date, "dd/MMM/yyyy") + ""
                        If clsCommon.myLen(obj.Description) > 300 Then
                            obj.Description = obj.Description.Substring(0, 300)
                        End If
                        obj.Reference_Document = ""
                        obj.Document_No = ""
                        obj.Unit_Code = "ALL"
                        obj.ItemType = ""
                        obj.EMP_CODE = ""
                        obj.EMP_NAME = ""
                        obj.Customer_CODE = ""
                        obj.Customer_NAME = ""
                        obj.Against_Physical_Stock_No = objtr.Physical_No
                        obj.Vehicle_Code = ""
                        obj.Vehicle_No = ""
                        obj.Challan_No = ""
                        obj.GateEntry_No = ""
                        obj.IsMilkType = objtr.Is_Milk
                        If objtr.Is_Milk > 0 Then
                            obj.MainLocationCode = objtr.Main_Location
                            obj.MainLocationDesc = clsLocation.GetName(objtr.Main_Location, trans)
                            obj.Loc_Code = objtr.Location_Code
                            obj.Loc_Desc = clsLocation.GetName(objtr.Location_Code, trans)
                        Else
                            obj.Loc_Code = objtr.Location_Code
                            obj.Loc_Desc = clsLocation.GetName(objtr.Location_Code, trans)
                        End If
                        obj.chklocation = "N"
                        obj.Against_Item_Stock_Conversion = ""
                        obj.Against_Item_Stock_Conv_Doc = ""
                        obj.Against_Bulk_Srn_PI_adjustment = ""

                        Dim objArr As New ClsAdjustmentsDetails()
                        objArr.Adjustment_Line_No = lineNo
                        lineNo += 1
                        objArr.Item_Code = objtr.Item_Code
                        objArr.Item_Description = objtr.Item_Desc
                        objArr.Bar_Code = ""
                        objArr.Adjustment_Type = "BI"
                        If objtr.Difference < 0 Then
                            objArr.Item_Quantity = Math.Abs(objtr.Difference)
                        End If
                        If objtr.DifferenceAmt < 0 Then
                            objArr.Item_Cost = Math.Abs(objtr.DifferenceAmt)
                        End If
                        'sanjay Ticket No- TEC/12/03/19-000442
                        If objArr.Item_Quantity > 0 AndAlso objArr.Item_Cost > 0 Then
                            objArr.Unit_Cost = (objArr.Item_Cost / objArr.Item_Quantity)
                        End If
                        'sanjay Ticket No- TEC/12/03/19-000442
                        objArr.Unit_Code = objtr.Stock_Unit
                        objArr.mrp = objtr.MRP
                        objArr.Batch_No = objtr.Batch_No
                        objArr.ItemType = clsItemMaster.GetStoreAdjustmentItemTypeWithTrans(objtr.Item_Code, trans)
                        objArr.Itemstatus = "NEW"
                        objArr.fat_kg = 0
                        objArr.fat_pers = 0
                        objArr.snf_kg = 0
                        objArr.snf_pers = 0
                        objArr.arrSrItem = objtr.arrSrItem

                        If objtr.arrBatchItem IsNot Nothing AndAlso objtr.arrBatchItem.Count > 0 Then
                            For jj As Integer = 0 To objtr.arrBatchItem.Count - 1
                                objtr.arrBatchItem(jj).Parent_Line_No = objArr.Adjustment_Line_No
                            Next
                        End If
                        objArr.arrBatchItem = objtr.arrBatchItem
                        'objArr.PS_GL_Account = objtr.GL_Account
                        'objArr.PS_GL_Account_Inventroy_Ctrl = objtr.GL_Account_Inventroy_Ctrl
                        obj.Arr.Add(objArr)
                    End If

                    '' add fat/snf or both
                    If objtr.FatKgDifference < 0 OrElse objtr.SNFKgDifference < 0 OrElse objtr.FatAmtDifference < 0 OrElse objtr.SNFAmtDifference < 0 Then 'remove -ve sign
                        obj.Adjustment_Date = objtr.Stock_Date
                        obj.Reference = ""
                        obj.Description = objtr.Description
                        If clsCommon.myLen(obj.Description) > 100 Then
                            obj.Description = obj.Description.Substring(0, 100)
                        End If
                        obj.Reference_Document = ""
                        obj.Document_No = ""
                        obj.Unit_Code = "ALL"
                        obj.ItemType = ""
                        obj.EMP_CODE = ""
                        obj.EMP_NAME = ""
                        obj.Customer_CODE = ""
                        obj.Customer_NAME = ""
                        obj.Against_Physical_Stock_No = objtr.Physical_No
                        obj.Vehicle_Code = ""
                        obj.Vehicle_No = ""
                        obj.Challan_No = ""
                        obj.GateEntry_No = ""
                        obj.IsMilkType = objtr.Is_Milk
                        If objtr.Is_Milk > 0 Then
                            obj.MainLocationCode = objtr.Main_Location
                            obj.MainLocationDesc = clsLocation.GetName(objtr.Main_Location, trans)
                            obj.Loc_Code = objtr.Location_Code
                            obj.Loc_Desc = clsLocation.GetName(objtr.Location_Code, trans)
                        Else
                            obj.Loc_Code = objtr.Location_Code
                            obj.Loc_Desc = clsLocation.GetName(objtr.Location_Code, trans)
                        End If

                        obj.chklocation = "N"
                        obj.Against_Item_Stock_Conversion = ""
                        obj.Against_Item_Stock_Conv_Doc = ""
                        obj.Against_Bulk_Srn_PI_adjustment = ""

                        Dim objArr As New ClsAdjustmentsDetails()
                        objArr.Adjustment_Line_No = lineNo
                        lineNo += 1
                        objArr.Item_Code = objtr.Item_Code
                        objArr.Item_Description = objtr.Item_Desc
                        objArr.Bar_Code = ""
                        objArr.Adjustment_Type = "FI"
                        objArr.Item_Quantity = 0
                        objArr.Item_Cost = 0
                        objArr.Unit_Code = objtr.Stock_Unit
                        objArr.mrp = objtr.MRP
                        objArr.Batch_No = objtr.Batch_No
                        objArr.ItemType = clsItemMaster.GetStoreAdjustmentItemTypeWithTrans(objtr.Item_Code, trans)
                        objArr.Itemstatus = "NEW"
                        If objtr.FatKgDifference < 0 Then
                            objArr.fat_kg = Math.Abs(objtr.FatKgDifference)
                            objArr.fat_pers = Math.Abs(objtr.FatPerDifference)
                        End If
                        If objtr.FatAmtDifference < 0 Then
                            objArr.fat_Amt = Math.Abs(objtr.FatAmtDifference)
                        End If

                        If objtr.SNFKgDifference < 0 Then
                            objArr.snf_kg = Math.Abs(objtr.SNFKgDifference)
                            objArr.snf_pers = Math.Abs(objtr.SNFPerDifference)
                        End If
                        If objtr.SNFAmtDifference < 0 Then
                            objArr.snf_Amt = Math.Abs(objtr.SNFAmtDifference)
                        End If
                        objArr.arrSrItem = objtr.arrSrItem
                        obj.Arr.Add(objArr)
                    End If
                Next
                If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                    obj.SaveData(obj, True, "", trans)
                    ClsAdjustments.PostData(obj.Adjustment_No, "Store Adjustment", trans)
                End If

                '====out===
                obj = New ClsAdjustments()
                obj.Arr = New List(Of ClsAdjustmentsDetails)
                lineNo = 1
                For Each objtr As clsPhysicalstock In arr
                    obj.Trans_Type = "Out"
                    If objtr.Difference > 0 OrElse objtr.DifferenceAmt > 0 Then
                        obj.Adjustment_Date = objtr.Stock_Date
                        obj.Reference = ""
                        obj.Description = objtr.Description
                        If clsCommon.myLen(obj.Description) > 100 Then
                            obj.Description = obj.Description.Substring(0, 100)
                        End If
                        obj.Reference_Document = ""
                        obj.Document_No = ""
                        obj.Unit_Code = "ALL"
                        obj.ItemType = ""
                        obj.EMP_CODE = ""
                        obj.EMP_NAME = ""
                        obj.Customer_CODE = ""
                        obj.Customer_NAME = ""
                        obj.Against_Physical_Stock_No = objtr.Physical_No
                        obj.Vehicle_Code = ""
                        obj.Vehicle_No = ""
                        obj.Challan_No = ""
                        obj.GateEntry_No = ""
                        obj.IsMilkType = objtr.Is_Milk
                        If objtr.Is_Milk > 0 Then
                            obj.MainLocationCode = objtr.Main_Location
                            obj.MainLocationDesc = clsLocation.GetName(objtr.Main_Location, trans)
                            obj.Loc_Code = objtr.Location_Code
                            obj.Loc_Desc = clsLocation.GetName(objtr.Location_Code, trans)
                        Else
                            obj.Loc_Code = objtr.Location_Code
                            obj.Loc_Desc = clsLocation.GetName(objtr.Location_Code, trans)
                        End If

                        obj.chklocation = "N"
                        obj.Against_Item_Stock_Conversion = ""
                        obj.Against_Item_Stock_Conv_Doc = ""
                        obj.Against_Bulk_Srn_PI_adjustment = ""

                        Dim objArrOut As New ClsAdjustmentsDetails()
                        objArrOut.Adjustment_Line_No = lineNo
                        lineNo += 1
                        objArrOut.Item_Code = objtr.Item_Code
                        objArrOut.Item_Description = objtr.Item_Desc
                        objArrOut.Bar_Code = ""
                        objArrOut.Adjustment_Type = "BD"
                        If objtr.Difference > 0 Then
                            objArrOut.Item_Quantity = objtr.Difference
                        End If
                        If objtr.DifferenceAmt > 0 Then
                            objArrOut.Item_Cost = objtr.DifferenceAmt
                        End If
                        'sanjay Ticket No- TEC/12/03/19-000442
                        If objArrOut.Item_Quantity > 0 AndAlso objArrOut.Item_Cost > 0 Then
                            objArrOut.Unit_Cost = (objArrOut.Item_Cost / objArrOut.Item_Quantity)
                        End If
                        'sanjay Ticket No- TEC/12/03/19-000442
                        objArrOut.Unit_Code = objtr.Stock_Unit
                        objArrOut.mrp = objtr.MRP
                        objArrOut.Batch_No = objtr.Batch_No
                        objArrOut.ItemType = clsItemMaster.GetStoreAdjustmentItemTypeWithTrans(objtr.Item_Code, trans)
                        objArrOut.Itemstatus = "NEW"
                        objArrOut.fat_kg = 0
                        objArrOut.fat_pers = 0
                        objArrOut.snf_kg = 0
                        objArrOut.snf_pers = 0
                        objArrOut.arrSrItem = objtr.arrSrItem
                        If objtr.arrBatchItem IsNot Nothing AndAlso objtr.arrBatchItem.Count > 0 Then
                            For jj As Integer = 0 To objtr.arrBatchItem.Count - 1
                                objtr.arrBatchItem(jj).Parent_Line_No = objArrOut.Adjustment_Line_No
                            Next
                        End If
                        objArrOut.arrBatchItem = objtr.arrBatchItem
                        obj.Arr.Add(objArrOut)
                    End If
                    '' add fat/snf or both
                    If objtr.FatKgDifference > 0 OrElse objtr.SNFKgDifference > 0 OrElse objtr.FatAmtDifference > 0 Or objtr.SNFAmtDifference > 0 Then
                        obj.Adjustment_Date = objtr.Stock_Date
                        obj.Reference = ""
                        obj.Description = objtr.Description
                        If clsCommon.myLen(obj.Description) > 100 Then
                            obj.Description = obj.Description.Substring(0, 100)
                        End If
                        obj.Reference_Document = ""
                        obj.Document_No = ""
                        obj.Unit_Code = "ALL"
                        obj.ItemType = ""
                        obj.EMP_CODE = ""
                        obj.EMP_NAME = ""
                        obj.Customer_CODE = ""
                        obj.Customer_NAME = ""
                        obj.Against_Physical_Stock_No = objtr.Physical_No
                        obj.Vehicle_Code = ""
                        obj.Vehicle_No = ""
                        obj.Challan_No = ""
                        obj.GateEntry_No = ""
                        obj.IsMilkType = objtr.Is_Milk
                        If objtr.Is_Milk > 0 Then
                            obj.MainLocationCode = objtr.Main_Location
                            obj.MainLocationDesc = clsLocation.GetName(objtr.Main_Location, trans)
                            obj.Loc_Code = objtr.Location_Code
                            obj.Loc_Desc = clsLocation.GetName(objtr.Location_Code, trans)
                        Else
                            obj.Loc_Code = objtr.Location_Code
                            obj.Loc_Desc = clsLocation.GetName(objtr.Location_Code, trans)
                        End If

                        obj.chklocation = "N"
                        obj.Against_Item_Stock_Conversion = ""
                        obj.Against_Item_Stock_Conv_Doc = ""
                        obj.Against_Bulk_Srn_PI_adjustment = ""

                        Dim objArr As New ClsAdjustmentsDetails()
                        objArr.Adjustment_Line_No = lineNo
                        lineNo += 1
                        objArr.Item_Code = objtr.Item_Code
                        objArr.Item_Description = objtr.Item_Desc
                        objArr.Bar_Code = ""
                        objArr.Adjustment_Type = "FI"
                        objArr.Item_Quantity = 0
                        objArr.Item_Cost = 0
                        objArr.Unit_Code = objtr.Stock_Unit
                        objArr.mrp = objtr.MRP
                        objArr.Batch_No = objtr.Batch_No
                        objArr.ItemType = clsItemMaster.GetStoreAdjustmentItemTypeWithTrans(objtr.Item_Code, trans)
                        objArr.Itemstatus = "NEW"
                        If objtr.FatKgDifference > 0 Then
                            objArr.fat_kg = Math.Abs(objtr.FatKgDifference)
                            objArr.fat_pers = Math.Abs(objtr.FatPerDifference)
                        End If
                        If objtr.FatAmtDifference > 0 Then
                            objArr.fat_Amt = Math.Abs(objtr.FatAmtDifference)
                        End If

                        If objtr.SNFKgDifference > 0 Then
                            objArr.snf_kg = Math.Abs(objtr.SNFKgDifference)
                            objArr.snf_pers = Math.Abs(objtr.SNFPerDifference)
                        End If
                        If objtr.SNFAmtDifference > 0 Then
                            objArr.snf_Amt = Math.Abs(objtr.SNFAmtDifference)
                        End If

                        objArr.arrSrItem = objtr.arrSrItem
                        obj.Arr.Add(objArr)
                    End If
                Next
                If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                    obj.SaveData(obj, True, "", trans)
                    ClsAdjustments.PostData(obj.Adjustment_No, "Store Adjustment", trans)
                End If
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function GetData(ByVal Physical_No As String, ByVal strLocaion As String, ByVal Sub_Loc_Code As String, ByVal is_Milk As Boolean, ByVal NavType As NavigatorType, ByVal strStockDate As String) As List(Of clsPhysicalstock)
        Try
            Return GetData(Physical_No, strLocaion, Sub_Loc_Code, is_Milk, NavType, Nothing, strStockDate)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function GetData(ByVal strDocNo As String, ByVal strLocaion As String, ByVal Sub_Loc_Code As String, ByVal Is_Milk As Boolean, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction, ByVal strStockDate As String) As List(Of clsPhysicalstock)
        Dim arr As New List(Of clsPhysicalstock)
        Try
            Dim qry As String = ""
            Dim isInvRead As Boolean = True
            Dim strStockByDate As String = ""
            If clsCommon.myLen(strStockDate) > 0 Then
                If Is_Milk = False Then
                    strStockByDate = "  where convert (date,TSPL_INVENTORY_MOVEMENT.Punching_Date,103) <= convert(date, '" + clsCommon.GetPrintDate(strStockDate, "dd/MMM/yyyy") + "',103) "
                Else
                    strStockByDate = "  where convert (date,TSPL_INVENTORY_MOVEMENT_NEW.Punching_Date,103) <= convert(date, '" + clsCommon.GetPrintDate(strStockDate, "dd/MMM/yyyy") + "',103) "
                End If
            End If
            If clsCommon.myLen(strLocaion) <= 0 OrElse clsCommon.myLen(strDocNo) > 0 Then
                qry = "select TSPL_PHYSICAL_STOCK.*,TabInvCtrl.Description as GL_Account_Inventroy_CtrlName,TabGL.Description as GL_AccountName from TSPL_PHYSICAL_STOCK " + Environment.NewLine +
                    "left outer join TSPL_GL_ACCOUNTS as TabInvCtrl  on TabInvCtrl .Account_Code=TSPL_PHYSICAL_STOCK.GL_Account_Inventroy_Ctrl" + Environment.NewLine +
                    "left outer join TSPL_GL_ACCOUNTS as TabGL on TabGL.Account_Code=TSPL_PHYSICAL_STOCK.GL_Account" + Environment.NewLine +
                    "where 2=2 "
                If clsCommon.myLen(strLocaion) > 0 OrElse clsCommon.myLen(Sub_Loc_Code) > 0 Then
                    qry += " and 2 = case when isnull(TSPL_PHYSICAL_STOCK.Multiple_Location,0)=0 then 2 else case when isnull(Location,'')='" + strLocaion + "' and isnull(Silo_Location,'')='" + Sub_Loc_Code + "' then 2 else 3 end end"
                End If
                Select Case NavType
                    Case NavigatorType.Current
                        qry += " and physical_no='" + strDocNo + "'"
                    Case NavigatorType.First
                        qry += " and physical_no in (select min(physical_no) from TSPL_PHYSICAL_STOCK)"
                    Case NavigatorType.Last
                        qry += " and physical_no in (select max(physical_no) from TSPL_PHYSICAL_STOCK)"
                    Case NavigatorType.Next
                        qry += " and physical_no in (select min(physical_no) from TSPL_PHYSICAL_STOCK where physical_no>'" + strDocNo + "')"
                    Case NavigatorType.Previous
                        qry += " and physical_no in (select max(physical_no) from TSPL_PHYSICAL_STOCK where physical_no<'" + strDocNo + "')"
                End Select
                isInvRead = False
            Else
                Dim whrcls As String = ""
                If clsCommon.myLen(strLocaion) > 0 Then
                    whrcls = " and TSPL_PHYSICAL_STOCK.location='" + strLocaion + "'"
                End If
                If clsCommon.myLen(Sub_Loc_Code) > 0 Then
                    whrcls += " and TSPL_PHYSICAL_STOCK.silo_location='" + Sub_Loc_Code + "'"
                End If

                qry = "select   TSPL_PHYSICAL_STOCK.physical_no,TSPL_PHYSICAL_STOCK.Description,TSPL_PHYSICAL_STOCK.Stock_Date,TSPL_PHYSICAL_STOCK.Silo_Location as location_code,TSPL_PHYSICAL_STOCK.Location as main_location,TSPL_PHYSICAL_STOCK.Item_Code"
                qry += ",TSPL_PHYSICAL_STOCK.Existing_Qty as qty,TSPL_PHYSICAL_STOCK.existing_fat_kg as fat_kg,TSPL_PHYSICAL_STOCK.existing_snf_kg as snf_kg,TSPL_PHYSICAL_STOCK.Stock_Unit as uom,TSPL_PHYSICAL_STOCK.MRP,TSPL_PHYSICAL_STOCK.Batch_No,TSPL_PHYSICAL_STOCK.Physical_Qty,TSPL_PHYSICAL_STOCK.FAT_Kg as Phy_Fat_Kg,TSPL_PHYSICAL_STOCK.SNF_Kg as Phy_Snf_Kg,TSPL_PHYSICAL_STOCK.Is_Milk,TSPL_PHYSICAL_STOCK.Existing_Amount,TSPL_PHYSICAL_STOCK.Existing_FAT_Amt,TSPL_PHYSICAL_STOCK.Existing_SNF_Amt    from TSPL_PHYSICAL_STOCK where 2=2 "
                Select Case NavType
                    Case NavigatorType.Current
                        qry += " and physical_no='" + strDocNo + "' " + whrcls + ""
                    Case NavigatorType.First
                        qry += " and physical_no in (select min(physical_no) from TSPL_PHYSICAL_STOCK where 1=1 " + whrcls + ")"
                    Case NavigatorType.Last
                        qry += " and physical_no in (select max(physical_no) from TSPL_PHYSICAL_STOCK where 1=1 " + whrcls + ")"
                    Case NavigatorType.Next
                        qry += " and physical_no in (select min(physical_no) from TSPL_PHYSICAL_STOCK where physical_no>'" + strDocNo + "' " + whrcls + ")"
                    Case NavigatorType.Previous
                        qry += " and physical_no in (select max(physical_no) from TSPL_PHYSICAL_STOCK where physical_no<'" + strDocNo + "' " + whrcls + ")"
                End Select
                If Is_Milk Then
                    qry += " union all "
                    qry += " select  axa.physical_no,axa.Description,axa.Stock_Date,axa.location_code,axa.main_location,axa.Item_Code,axa.qty ,axa.fat_kg ,axa.snf_kg as snf_kg,axa.UOM as UOM_Code,axa.MRP,axa.Batch_No,axa.Physical_Qty,axa.Phy_Fat_Kg,axa.Phy_Snf_Kg,axa.is_milk,axa.Avg_Cost as Existing_Amount,axa.Fat_Amt,axa.SNF_Amt from (" + Environment.NewLine +
                    "select '' as physical_no,'' as Description,cast('01/01/1900' as DATEtime) as Stock_Date,aa.Location_Code,aa.main_location,aa.Item_Code,sum(aa.qty) as qty,0 as fat_pers,sum(aa.Fat_KG) as Fat_KG,0 as snf_pers,sum(aa.SNF_KG) as SNF_KG,aa.UOM,aa.MRP,aa.Batch_No,0 as Physical_Qty,0 as Phy_Fat_Pers,0 as Phy_Fat_Kg,0 as Phy_Snf_Pers,0 as Phy_Snf_Kg,1 as Is_Milk,sum(Avg_Cost) as Avg_Cost,sum(Fat_Amt) as Fat_Amt,sum(SNF_Amt) SNF_Amt  from ( " + Environment.NewLine +
                    "select TSPL_INVENTORY_MOVEMENT_NEW.Trans_Id,TSPL_INVENTORY_MOVEMENT_NEW.Location_Code,TSPL_INVENTORY_MOVEMENT_NEW.main_location,TSPL_INVENTORY_MOVEMENT_NEW.Item_Code," + Environment.NewLine +
                    "isnull( TSPL_INVENTORY_MOVEMENT_NEW.Stock_Qty,0) *case when TSPL_INVENTORY_MOVEMENT_NEW.inout='i' then 1 else -1 end as qty," + Environment.NewLine +
                    "isnull( TSPL_INVENTORY_MOVEMENT_NEW.Fat_KG,0) *case when TSPL_INVENTORY_MOVEMENT_NEW.inout='i' then 1 else -1 end as Fat_KG," + Environment.NewLine +
                    "isnull( TSPL_INVENTORY_MOVEMENT_NEW.SNF_KG,0) *case when TSPL_INVENTORY_MOVEMENT_NEW.inout='i' then 1 else -1 end as SNF_KG," + Environment.NewLine +
                    "TSPL_INVENTORY_MOVEMENT_NEW.Stock_UOM as UOM,TSPL_INVENTORY_MOVEMENT_NEW.MRP,TSPL_INVENTORY_MOVEMENT_NEW.Batch_No " + Environment.NewLine +
                    ",isnull( TSPL_INVENTORY_MOVEMENT_NEW.Avg_Cost,0) *case when TSPL_INVENTORY_MOVEMENT_NEW.inout='i' then 1 else -1 end as Avg_Cost" + Environment.NewLine +
                    ",isnull( TSPL_INVENTORY_MOVEMENT_NEW.Fat_Amt,0) * case when TSPL_INVENTORY_MOVEMENT_NEW.inout='i' then 1 else -1 end as Fat_Amt " + Environment.NewLine +
                    ",isnull( TSPL_INVENTORY_MOVEMENT_NEW.SNF_Amt,0) * case when TSPL_INVENTORY_MOVEMENT_NEW.inout='i' then 1 else -1 end as SNF_Amt" + Environment.NewLine +
                    "from TSPL_INVENTORY_MOVEMENT_NEW " + Environment.NewLine + strStockByDate + Environment.NewLine +
                    ")aa group by aa.Location_Code,aa.Item_Code,aa.UOM,aa.MRP,aa.Batch_No,aa.main_location" + Environment.NewLine +
                    ")axa  where 1=1  "
                    If clsCommon.myLen(strLocaion) > 0 AndAlso clsCommon.myLen(Sub_Loc_Code) <= 0 Then
                        qry += " and axa.Location_Code='" + strLocaion + "'"
                    End If
                    If clsCommon.myLen(Sub_Loc_Code) > 0 Then
                        qry += " and axa.location_code='" + Sub_Loc_Code + "'"
                    End If
                Else
                    qry += " union all "
                    qry += "select  axa.physical_no,axa.Description,axa.Stock_Date,axa.location_code,axa.main_location,axa.Item_Code,(axa.qty*TSPL_ITEM_UOM_DETAIL.Conversion_Factor)/finalcnvrsn.Conversion_Factor as qty,(axa.fat_kg*TSPL_ITEM_UOM_DETAIL.Conversion_Factor)/finalcnvrsn.Conversion_Factor as fat_kg,(axa.snf_kg*TSPL_ITEM_UOM_DETAIL.Conversion_Factor)/finalcnvrsn.Conversion_Factor as snf_kg,finalcnvrsn.UOM_Code,axa.MRP,axa.Batch_No,axa.Physical_Qty,axa.Phy_Fat_Kg,axa.Phy_Snf_Kg,axa.is_milk,axa.Existing_Amount,0 as Existing_FAT_Amt,0 as Existing_SNF_Amt from ( "
                    qry += "select '' as physical_no,'' as Description,cast('01/01/1900' as DATEtime) as Stock_Date,'' as location_code,aa.Location_Code as main_location,aa.Item_Code,sum(aa.qty) as qty,0 as fat_pers,0 as fat_kg,0 as snf_pers,0 as snf_kg,aa.UOM,aa.MRP,aa.Batch_No,0 as Physical_Qty,0 as Phy_Fat_Pers,0 as Phy_Fat_Kg,0 as Phy_Snf_Pers,0 as Phy_Snf_Kg,0 as Is_Milk,sum(Existing_Amount) as Existing_Amount from ( "
                    qry += "select TSPL_INVENTORY_MOVEMENT.Trans_Id,TSPL_INVENTORY_MOVEMENT.Location_Code,TSPL_INVENTORY_MOVEMENT.Item_Code,isnull(TSPL_INVENTORY_MOVEMENT.Stock_Qty,0)*case when TSPL_INVENTORY_MOVEMENT.inout='i' then 1 else -1 end as qty ,TSPL_INVENTORY_MOVEMENT.Stock_UOM  as UOM,TSPL_INVENTORY_MOVEMENT.MRP,TSPL_INVENTORY_MOVEMENT.Batch_No,isnull( TSPL_INVENTORY_MOVEMENT.Avg_Cost,0) *case when TSPL_INVENTORY_MOVEMENT.inout='i' then 1 else -1 end as Existing_Amount from TSPL_INVENTORY_MOVEMENT  " + strStockByDate + " "
                    qry += ")aa group by aa.Location_Code,aa.Item_Code,aa.UOM,aa.MRP,aa.Batch_No)axa "
                    qry += "left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=axa.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code=axa.UOM "
                    qry += "left outer join TSPL_ITEM_UOM_DETAIL finalcnvrsn on finalcnvrsn.Item_Code=axa.Item_Code and finalcnvrsn.Stocking_Unit='Y' where 1=1 "
                    If clsCommon.myLen(strLocaion) > 0 Then
                        qry += " and axa.main_location='" + strLocaion + "'"
                    End If
                End If

                Dim colExtraGroup As String = ""
                Dim colExtraSel As String = ""

                colExtraGroup = ""
                colExtraSel = ",0 as MRP"

                colExtraGroup = colExtraGroup
                colExtraSel = colExtraSel & ",'' as Batch_No"

                Dim str As String = qry
                qry = "select row_number() over (order by final.item_code) as line_no,final.physical_no,final.Description,convert(date,SYSDATETIME(),103) as Stock_Date,final.location_code as Silo_Location,final.main_location  as Location,final.Item_Code" & colExtraSel & ",sum(final.qty) as Existing_Qty,sum(final.fat_kg) as existing_fat_kg "
                qry += ",sum(final.snf_kg) as existing_snf_kg,final.uom as Stock_Unit,sum(final.Physical_Qty) as Physical_Qty,sum(final.Phy_Fat_Kg) as fat_kg,sum(final.Phy_Snf_Kg) as snf_kg,final.Is_Milk,sum(Existing_Amount) as Existing_Amount,0 as Amt,sum(Existing_FAT_Amt) as Existing_FAT_Amt,sum(Existing_SNF_Amt) as Existing_SNF_Amt  from (" + str + ") final"
                qry += " group by final.physical_no,final.Description,final.Stock_Date,final.location_code ,final.main_location,final.Item_Code,final.uom,final.Is_Milk " & colExtraGroup & ""

                qry = "select xx.*,0 as Nill_Balance,TabGLOverride.Account_Code as GL_Account_Inventroy_Ctrl,TabGLOverride.Description as GL_Account_Inventroy_CtrlName,'' as GL_Account,'' as GL_AccountName,0 as SNF_Amt,0 as FAT_Amt from (" + qry + ")xx left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=xx.Item_Code" + Environment.NewLine +
                    "left outer join TSPL_PURCHASE_ACCOUNTS on TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code=TSPL_ITEM_MASTER.Purchase_Class_Code " + Environment.NewLine +
                    "left outer join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code=TSPL_PURCHASE_ACCOUNTS.Inv_Control_Account " + Environment.NewLine +
                    "left outer join TSPL_GL_ACCOUNTS as TabGLOverride on TabGLOverride.Account_Seg_Code1=TSPL_GL_ACCOUNTS.Account_Seg_Code1 and  TabGLOverride.Account_Seg_Code7='" + clsLocation.GetSegmentCode(strLocaion, trans) + "'"
                isInvRead = True
            End If

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    Dim obj As New clsPhysicalstock()
                    obj.Physical_No = clsCommon.myCstr(dr("physical_no"))
                    obj.Description = clsCommon.myCstr(dr("description"))
                    obj.Is_Milk = CInt(clsCommon.myCdbl(dr("is_milk")))
                    obj.Stock_Date = clsCommon.myCDate(dr("STOCK_DATE"))
                    If clsCommon.myLen(dr.Item("Silo_Location")) > 0 Then
                        obj.Main_Location = clsCommon.myCstr(dr("Location"))
                        obj.Location_Code = clsCommon.myCstr(dr("Silo_Location"))
                    Else
                        obj.Main_Location = ""
                        obj.Location_Code = clsCommon.myCstr(dr("Location"))
                    End If
                    obj.Nill_Balance = (clsCommon.myCdbl(dr("Nill_Balance")) = 1)
                    obj.Line_No = clsCommon.myCstr(dr("line_no"))
                    obj.Item_Code = clsCommon.myCstr(dr("item_code"))
                    obj.Item_Desc = clsItemMaster.GetItemName(obj.Item_Code, trans)
                    obj.Stock_Unit = clsCommon.myCstr(dr("stock_unit"))
                    obj.Batch_No = clsCommon.myCstr(dr("batch_no"))
                    obj.MRP = clsCommon.myCstr(dr("mrp"))
                    obj.Existing_Qty = clsCommon.myCdbl(dr("Existing_Qty"))
                    obj.Existing_FAT_Kg = clsCommon.myCdbl(dr("Existing_FAT_Kg"))
                    obj.Existing_SNF_Kg = clsCommon.myCdbl(dr("Existing_SNF_Kg"))

                    obj.Existing_FAT_Amt = clsCommon.myCdbl(dr("Existing_FAT_Amt"))
                    obj.Existing_SNF_Amt = clsCommon.myCdbl(dr("Existing_SNF_Amt"))
                    obj.Existing_Amount = clsCommon.myCdbl(dr("Existing_Amount"))
                    obj.FAT_Amt = clsCommon.myCdbl(dr("FAT_Amt"))
                    obj.SNF_Amt = clsCommon.myCdbl(dr("SNF_Amt"))
                    obj.Amt = clsCommon.myCdbl(dr("Amt"))

                    obj.GL_Account_Inventroy_Ctrl = clsCommon.myCstr(dr("GL_Account_Inventroy_Ctrl"))
                    obj.GL_Account_Inventroy_CtrlName = clsCommon.myCstr(dr("GL_Account_Inventroy_CtrlName"))
                    obj.GL_Account = clsCommon.myCstr(dr("GL_Account"))
                    obj.GL_AccountName = clsCommon.myCstr(dr("GL_AccountName"))

                    If clsCommon.myCdbl(dr("Existing_Qty")) <> 0 Then
                        obj.Existing_FAT_Pers = System.Math.Round((clsCommon.myCdbl(dr("Existing_FAT_Kg")) * 100) / clsCommon.myCdbl(dr("Existing_Qty")), 2)
                        obj.Existing_SNF_Pers = System.Math.Round((clsCommon.myCdbl(dr("Existing_SNF_Kg")) * 100) / clsCommon.myCdbl(dr("Existing_Qty")), 2)
                    Else
                        obj.Existing_FAT_Pers = 0
                        obj.Existing_SNF_Pers = 0
                    End If


                    obj.Physical_Qty = clsCommon.myCdbl(dr("Physical_Qty"))
                    obj.FAT_Kg = clsCommon.myCdbl(dr("FAT_Kg"))
                    obj.SNF_Kg = clsCommon.myCdbl(dr("SNF_Kg"))
                    If clsCommon.myCdbl(dr("Physical_Qty")) <> 0 Then
                        obj.FAT_Pers = System.Math.Round((clsCommon.myCdbl(dr("FAT_Kg")) * 100) / clsCommon.myCdbl(dr("Physical_Qty")), 2)
                        obj.SNF_Pers = System.Math.Round((clsCommon.myCdbl(dr("SNF_Kg")) * 100) / clsCommon.myCdbl(dr("Physical_Qty")), 2)
                    Else
                        obj.FAT_Pers = 0
                        obj.SNF_Pers = 0
                    End If

                    obj.Difference = clsCommon.myCdbl(obj.Existing_Qty - obj.Physical_Qty)
                    obj.SNFKgDifference = clsCommon.myCdbl(obj.Existing_SNF_Kg - obj.SNF_Kg)
                    obj.FatKgDifference = clsCommon.myCdbl(obj.Existing_FAT_Kg - obj.FAT_Kg)

                    obj.FatAmtDifference = clsCommon.myCdbl(obj.Existing_FAT_Amt - obj.FAT_Amt)
                    obj.SNFAmtDifference = clsCommon.myCdbl(obj.Existing_SNF_Amt - obj.SNF_Amt)
                    obj.DifferenceAmt = clsCommon.myCdbl(obj.Existing_Amount - obj.Amt)

                    If clsCommon.myCdbl(obj.Difference) <> 0 Then
                        obj.FatPerDifference = System.Math.Round((clsCommon.myCdbl(obj.FatKgDifference) * 100) / clsCommon.myCdbl(obj.Difference), 2)
                        obj.SNFPerDifference = System.Math.Round((clsCommon.myCdbl(obj.SNFKgDifference) * 100) / clsCommon.myCdbl(obj.Difference), 2)
                    Else
                        obj.FatPerDifference = 0
                        obj.SNFPerDifference = 0
                    End If
                    obj.arrBatchItem = clsBatchInventory.GetData("PH-ST", obj.Physical_No, obj.Item_Code, obj.Line_No, trans)
                    If Not isInvRead Then
                        Dim doc_no As String = ""
                        Dim line_No As Integer = 0
                        doc_no = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select adjustment_no from TSPL_ADJUSTMENT_DETAIL where item_code='" + obj.Item_Code + "' and mrp='" + clsCommon.myCstr(obj.MRP) + "' and batch_no='" + obj.Batch_No + "' and unit_code='" + obj.Stock_Unit + "' and adjustment_no in (select adjustment_no from TSPL_ADJUSTMENT_HEADER where against_physical_stock_no='" + obj.Physical_No + "')", trans))
                        line_No = CInt(clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select adjustment_line_no from TSPL_ADJUSTMENT_DETAIL where item_code='" + obj.Item_Code + "' and mrp='" + clsCommon.myCstr(obj.MRP) + "' and batch_no='" + obj.Batch_No + "' and unit_code='" + obj.Stock_Unit + "' and adjustment_no in (select adjustment_no from TSPL_ADJUSTMENT_HEADER where against_physical_stock_no='" + obj.Physical_No + "')", trans)))
                        obj.Is_Posted = CInt(clsCommon.myCdbl(dr("Is_Posted")))
                        obj.arrSrItem = clsSerializeInvenotry.GetData("IC-AD", doc_no, obj.Item_Code, line_No, trans)
                    End If
                    arr.Add(obj)
                Next
            End If
            Return arr
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function


    Public Shared Function GetDataForFirstTime(ByVal strDocNo As String, ByVal strLocaion As String, ByVal Sub_Loc_Code As String, ByVal Is_Milk As Boolean, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction, ByVal strStockDate As String, Optional ByVal strItemType As String = "", Optional ByVal strInventoryAccount As String = "") As DataTable
        Return GetDataForFirstTime(Nothing, strDocNo, strLocaion, Sub_Loc_Code, Is_Milk, NavType, trans, strStockDate, strItemType, strInventoryAccount)
    End Function

    Public Shared Function GetDataForFirstTime(ByVal ArrItem As ArrayList, ByVal strDocNo As String, ByVal strLocaion As String, ByVal Sub_Loc_Code As String, ByVal Is_Milk As Boolean, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction, ByVal strStockDate As String, Optional ByVal strItemType As String = "", Optional ByVal strInventoryAccount As String = "") As DataTable
        Dim dt As DataTable = Nothing
        Try
            Dim qry As String = ""
            Dim isInvRead As Boolean = True
            Dim strStockByDate As String = ""
            If clsCommon.myLen(strStockDate) > 0 Then
                If Is_Milk = False Then
                    strStockByDate = "  where convert (date,TSPL_INVENTORY_MOVEMENT.Punching_Date,103) <= convert(date, '" + clsCommon.GetPrintDate(strStockDate, "dd/MMM/yyyy") + "',103) "
                Else
                    strStockByDate = "  where convert (date,TSPL_INVENTORY_MOVEMENT_NEW.Punching_Date,103) <= convert(date, '" + clsCommon.GetPrintDate(strStockDate, "dd/MMM/yyyy") + "',103) "
                End If
            End If
            If clsCommon.myLen(strLocaion) <= 0 OrElse clsCommon.myLen(strDocNo) > 0 Then
                qry = "select TSPL_PHYSICAL_STOCK.*,TabInvCtrl.Description as GL_Account_Inventroy_CtrlName,TabGL.Description as GL_AccountName from TSPL_PHYSICAL_STOCK " + Environment.NewLine +
                    "left outer join TSPL_GL_ACCOUNTS as TabInvCtrl  on TabInvCtrl .Account_Code=TSPL_PHYSICAL_STOCK.GL_Account_Inventroy_Ctrl" + Environment.NewLine +
                    "left outer join TSPL_GL_ACCOUNTS as TabGL on TabGL.Account_Code=TSPL_PHYSICAL_STOCK.GL_Account" + Environment.NewLine +
                    "where 2=2 and physical_no='' "
                isInvRead = False
            Else
                Dim whrcls As String = ""
                If clsCommon.myLen(strLocaion) > 0 Then
                    whrcls = " and TSPL_PHYSICAL_STOCK.location='" + strLocaion + "'"
                End If
                If clsCommon.myLen(Sub_Loc_Code) > 0 Then
                    whrcls += " and TSPL_PHYSICAL_STOCK.silo_location='" + Sub_Loc_Code + "'"
                End If

                qry = "select   TSPL_PHYSICAL_STOCK.physical_no,TSPL_PHYSICAL_STOCK.Description,TSPL_PHYSICAL_STOCK.Stock_Date,TSPL_PHYSICAL_STOCK.Silo_Location as location_code,TSPL_PHYSICAL_STOCK.Location as main_location,TSPL_PHYSICAL_STOCK.Item_Code"
                qry += ",TSPL_PHYSICAL_STOCK.Existing_Qty as qty,TSPL_PHYSICAL_STOCK.existing_fat_kg as fat_kg,TSPL_PHYSICAL_STOCK.existing_snf_kg as snf_kg,TSPL_PHYSICAL_STOCK.Stock_Unit as uom,TSPL_PHYSICAL_STOCK.MRP,TSPL_PHYSICAL_STOCK.Batch_No,TSPL_PHYSICAL_STOCK.Physical_Qty,TSPL_PHYSICAL_STOCK.FAT_Kg as Phy_Fat_Kg,TSPL_PHYSICAL_STOCK.SNF_Kg as Phy_Snf_Kg,TSPL_PHYSICAL_STOCK.Is_Milk,TSPL_PHYSICAL_STOCK.Existing_Amount,TSPL_PHYSICAL_STOCK.Existing_FAT_Amt,TSPL_PHYSICAL_STOCK.Existing_SNF_Amt    from TSPL_PHYSICAL_STOCK where 2=2 and physical_no='' "
                If Is_Milk Then
                    qry += " union all " & Environment.NewLine &
                     " select  axa.physical_no,axa.Description,axa.Stock_Date,axa.location_code,axa.main_location,axa.Item_Code,axa.qty ,axa.fat_kg ,axa.snf_kg as snf_kg,axa.UOM as UOM_Code,axa.MRP,axa.Batch_No,axa.Physical_Qty,axa.Phy_Fat_Kg,axa.Phy_Snf_Kg,axa.is_milk,axa.Avg_Cost as Existing_Amount,axa.Fat_Amt,axa.SNF_Amt from (" + Environment.NewLine +
                    "select '' as physical_no,'' as Description,cast('01/01/1900' as DATEtime) as Stock_Date,aa.Location_Code,aa.main_location,aa.Item_Code,sum(aa.qty) as qty,0 as fat_pers,sum(aa.Fat_KG) as Fat_KG,0 as snf_pers,sum(aa.SNF_KG) as SNF_KG,aa.UOM,aa.MRP,aa.Batch_No,0 as Physical_Qty,0 as Phy_Fat_Pers,0 as Phy_Fat_Kg,0 as Phy_Snf_Pers,0 as Phy_Snf_Kg,1 as Is_Milk,sum(Avg_Cost) as Avg_Cost,sum(Fat_Amt) as Fat_Amt,sum(SNF_Amt) SNF_Amt  from ( " + Environment.NewLine +
                    "select TSPL_INVENTORY_MOVEMENT_NEW.Trans_Id,TSPL_INVENTORY_MOVEMENT_NEW.Location_Code,TSPL_INVENTORY_MOVEMENT_NEW.main_location,TSPL_INVENTORY_MOVEMENT_NEW.Item_Code," + Environment.NewLine +
                    "isnull( TSPL_INVENTORY_MOVEMENT_NEW.Stock_Qty,0) *case when TSPL_INVENTORY_MOVEMENT_NEW.inout='i' then 1 else -1 end as qty," + Environment.NewLine +
                    "isnull( TSPL_INVENTORY_MOVEMENT_NEW.Fat_KG,0) *case when TSPL_INVENTORY_MOVEMENT_NEW.inout='i' then 1 else -1 end as Fat_KG," + Environment.NewLine +
                    "isnull( TSPL_INVENTORY_MOVEMENT_NEW.SNF_KG,0) *case when TSPL_INVENTORY_MOVEMENT_NEW.inout='i' then 1 else -1 end as SNF_KG," + Environment.NewLine +
                    "TSPL_INVENTORY_MOVEMENT_NEW.Stock_UOM as UOM,TSPL_INVENTORY_MOVEMENT_NEW.MRP,TSPL_INVENTORY_MOVEMENT_NEW.Batch_No " + Environment.NewLine +
                    ",isnull( TSPL_INVENTORY_MOVEMENT_NEW.Avg_Cost,0) *case when TSPL_INVENTORY_MOVEMENT_NEW.inout='i' then 1 else -1 end as Avg_Cost" + Environment.NewLine +
                    ",isnull( TSPL_INVENTORY_MOVEMENT_NEW.Fat_Amt,0) * case when TSPL_INVENTORY_MOVEMENT_NEW.inout='i' then 1 else -1 end as Fat_Amt " + Environment.NewLine +
                    ",isnull( TSPL_INVENTORY_MOVEMENT_NEW.SNF_Amt,0) * case when TSPL_INVENTORY_MOVEMENT_NEW.inout='i' then 1 else -1 end as SNF_Amt" + Environment.NewLine +
                    "from TSPL_INVENTORY_MOVEMENT_NEW " + Environment.NewLine + strStockByDate + Environment.NewLine +
                    ")aa group by aa.Location_Code,aa.Item_Code,aa.UOM,aa.MRP,aa.Batch_No,aa.main_location" + Environment.NewLine +
                    ")axa  where 1=1  "
                    If clsCommon.myLen(strLocaion) > 0 AndAlso clsCommon.myLen(Sub_Loc_Code) <= 0 Then
                        qry += " and axa.Location_Code='" + strLocaion + "'"
                    End If
                    If clsCommon.myLen(Sub_Loc_Code) > 0 Then
                        qry += " and axa.location_code='" + Sub_Loc_Code + "'"
                    End If
                Else
                    qry += " union all " & Environment.NewLine &
                    "select  axa.physical_no,axa.Description,axa.Stock_Date,axa.location_code,axa.main_location,axa.Item_Code,(axa.qty*TSPL_ITEM_UOM_DETAIL.Conversion_Factor)/finalcnvrsn.Conversion_Factor as qty,(axa.fat_kg*TSPL_ITEM_UOM_DETAIL.Conversion_Factor)/finalcnvrsn.Conversion_Factor as fat_kg,(axa.snf_kg*TSPL_ITEM_UOM_DETAIL.Conversion_Factor)/finalcnvrsn.Conversion_Factor as snf_kg,finalcnvrsn.UOM_Code,axa.MRP,axa.Batch_No,axa.Physical_Qty,axa.Phy_Fat_Kg,axa.Phy_Snf_Kg,axa.is_milk,axa.Existing_Amount,0 as Existing_FAT_Amt,0 as Existing_SNF_Amt from ( " & Environment.NewLine &
                    "select '' as physical_no,'' as Description,cast('01/01/1900' as DATEtime) as Stock_Date,'' as location_code,aa.Location_Code as main_location,aa.Item_Code,sum(aa.qty) as qty,0 as fat_pers,0 as fat_kg,0 as snf_pers,0 as snf_kg,aa.UOM,aa.MRP,aa.Batch_No,0 as Physical_Qty,0 as Phy_Fat_Pers,0 as Phy_Fat_Kg,0 as Phy_Snf_Pers,0 as Phy_Snf_Kg,0 as Is_Milk,sum(Existing_Amount) as Existing_Amount from ( " & Environment.NewLine &
                    "select TSPL_INVENTORY_MOVEMENT.Trans_Id,TSPL_INVENTORY_MOVEMENT.Location_Code,TSPL_INVENTORY_MOVEMENT.Item_Code,isnull(TSPL_INVENTORY_MOVEMENT.Stock_Qty,0)*case when TSPL_INVENTORY_MOVEMENT.inout='i' then 1 else -1 end as qty ,TSPL_INVENTORY_MOVEMENT.Stock_UOM  as UOM,TSPL_INVENTORY_MOVEMENT.MRP,TSPL_INVENTORY_MOVEMENT.Batch_No,isnull( TSPL_INVENTORY_MOVEMENT.Avg_Cost,0) *case when TSPL_INVENTORY_MOVEMENT.inout='i' then 1 else -1 end as Existing_Amount from TSPL_INVENTORY_MOVEMENT  " + strStockByDate + " " & Environment.NewLine &
                    ")aa group by aa.Location_Code,aa.Item_Code,aa.UOM,aa.MRP,aa.Batch_No)axa " & Environment.NewLine &
                    "left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=axa.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code=axa.UOM " & Environment.NewLine &
                    "left outer join TSPL_ITEM_UOM_DETAIL finalcnvrsn on finalcnvrsn.Item_Code=axa.Item_Code and finalcnvrsn.Stocking_Unit='Y' where 1=1 " & Environment.NewLine
                    If clsCommon.myLen(strLocaion) > 0 Then
                        qry += " and axa.main_location='" + strLocaion + "'"
                    End If
                End If

                Dim colExtraGroup As String = ""
                Dim colExtraSel As String = ""

                colExtraGroup = ""
                colExtraSel = ",0 as MRP"

                colExtraGroup = colExtraGroup
                colExtraSel = colExtraSel & ",'' as Batch_No"

                Dim str As String = qry
                qry = "select row_number() over (order by final.item_code) as line_no,final.physical_no,final.Description,convert(date,SYSDATETIME(),103) as Stock_Date,final.location_code as Silo_Location,final.main_location  as Location,final.Item_Code" & colExtraSel & ",sum(final.qty) as Existing_Qty,sum(final.fat_kg) as existing_fat_kg " & Environment.NewLine &
                ",sum(final.snf_kg) as existing_snf_kg,final.uom as Stock_Unit,sum(final.Physical_Qty) as Physical_Qty,sum(final.Phy_Fat_Kg) as fat_kg,sum(final.Phy_Snf_Kg) as snf_kg,final.Is_Milk,sum(Existing_Amount) as Existing_Amount,0 as Amt,sum(Existing_FAT_Amt) as Existing_FAT_Amt,sum(Existing_SNF_Amt) as Existing_SNF_Amt  from (" + str + ") final " & Environment.NewLine &
                " group by final.physical_no,final.Description,final.Stock_Date,final.location_code ,final.main_location,final.Item_Code,final.uom,final.Is_Milk " & colExtraGroup & "" & Environment.NewLine

                qry = "select xx.*,TSPL_ITEM_MASTER.Item_Desc,TSPL_ITEM_MASTER.Is_Batch_Item,TSPL_ITEM_MASTER.Is_Serial_Item,TSPL_ITEM_MASTER.Is_Pick_Auto_SrNo, TSPL_ITEM_MASTER.Is_Serial_Item,0 as Nill_Balance,TabGLOverride.Account_Code as GL_Account_Inventroy_Ctrl,TabGLOverride.Description as GL_Account_Inventroy_CtrlName,'' as GL_Account,'' as GL_AccountName,0 as SNF_Amt,0 as FAT_Amt from (" + qry + ")xx left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=xx.Item_Code" + Environment.NewLine +
                "left outer join TSPL_PURCHASE_ACCOUNTS on TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code=TSPL_ITEM_MASTER.Purchase_Class_Code " + Environment.NewLine +
                "left outer join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code=TSPL_PURCHASE_ACCOUNTS.Inv_Control_Account " + Environment.NewLine +
                "left outer join TSPL_GL_ACCOUNTS as TabGLOverride on TabGLOverride.Account_Seg_Code1=TSPL_GL_ACCOUNTS.Account_Seg_Code1 and  TabGLOverride.Account_Seg_Code7='" + clsLocation.GetSegmentCode(strLocaion, trans) + "'  where 2=2 "
                If clsCommon.myLen(strItemType) > 0 Then
                    qry += " and TSPL_ITEM_MASTER.Item_Type in (" + strItemType + ") "
                End If
                If ArrItem IsNot Nothing AndAlso ArrItem.Count > 0 Then
                    qry += " and TSPL_ITEM_MASTER.Item_Code in (" + clsCommon.GetMulcallString(ArrItem) + ") "
                End If
                If clsCommon.myLen(strInventoryAccount) > 0 Then
                    qry += " and TSPL_PURCHASE_ACCOUNTS .Inv_Control_Account in (" + strInventoryAccount + ")  "
                End If
                isInvRead = True
            End If

            dt = clsDBFuncationality.GetDataTable(qry, trans)

            Return dt
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function DeleteData(ByVal strDocNo As String) As Boolean
        Dim tran As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Code not found to Post")
            End If
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strDocNo, "tspl_physical_stock", "Physical_No", tran)
            clsDBFuncationality.ExecuteNonQuery("delete from tspl_physical_stock where physical_no='" + strDocNo + "'", tran)
            clsBatchInventory.DeleteData("PH-ST", strDocNo, tran)
            tran.Commit()
        Catch ex As Exception
            tran.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
End Class