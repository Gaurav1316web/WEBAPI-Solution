Imports common
Imports System.Data.SqlClient
Public Class ClsBagReceipt

#Region "Variables"
    Public Document_Code As String = Nothing
    Public Document_Date As DateTime
    Public From_Date As DateTime
    Public To_Date As DateTime
    Public Location As String = Nothing
    Public Remarks As String = Nothing
    Public Status As ERPTransactionStatus = ERPTransactionStatus.Pending
    Public Posted_Date As DateTime?

    Public Arr As List(Of clsBagReceiptDetail) = Nothing
    Public ArrGunny As New List(Of clsBagReceiptDetail)
    Public ArrDT As DataTable = Nothing
#End Region

    Public Function SaveData(ByVal obj As ClsBagReceipt, ByVal isNewEntry As Boolean) As Boolean
        Dim isSaved As Boolean = False
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            isSaved = obj.SaveData(obj, isNewEntry, trans)
            If (isSaved) Then
                trans.Commit()
            Else
                trans.Rollback()
            End If
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function

    Public Function SaveData(ByVal obj As ClsBagReceipt, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True

        Dim qry As String = "delete from TSPL_BAG_RECEIPT_DETAIL where Document_Code='" + obj.Document_Code + "'"
        isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)


        If obj.Arr.Count <= 0 Then
            Throw New Exception("No detail found to save")
        End If

        If (isNewEntry) Then
            obj.Document_Code = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(obj.Document_Date), clsDocType.BMCTransporterBill, "", Nothing)
        End If
        If (clsCommon.myLen(obj.Document_Code) <= 0) Then
            Throw New Exception("Error in Document Code Generation")
        End If

        Dim coll As New Hashtable()
        clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy hh:mm tt"))
        clsCommon.AddColumnsForChange(coll, "Location", obj.Location)
        clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks)
        clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
        clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))
        clsCommon.AddColumnsForChange(coll, "Posted_By", objCommonVar.CurrentUserCode)
        clsCommon.AddColumnsForChange(coll, "Posted_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))
        If isNewEntry Then
            'obj.Document_Code = clsERPFuncationality.GetNextCode(trans, DateTime.Now, clsDocType.DistributorRouteTagging, "", "")
            'If clsCommon.myLen(obj.Document_Code) <= 0 Then
            '    Throw New Exception("Error in Code Generation")
            'End If
            clsCommon.AddColumnsForChange(coll, "Document_Code", obj.Document_Code)
            clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))
            isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_BAG_RECEIPT_HEAD", OMInsertOrUpdate.Insert, "", trans)
        Else
            isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_BAG_RECEIPT_HEAD", OMInsertOrUpdate.Update, "Document_Code='" + obj.Document_Code + "'", trans)
        End If
        'isSaved = isSaved AndAlso clsBagReceiptDetail.SaveData(obj.Document_Code, Arr, trans)
        isSaved = isSaved AndAlso clsBagReceiptDetail.SaveData(obj.Document_Code, obj.ArrGunny, trans)
        clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, obj.Document_Code, "TSPL_BAG_RECEIPT_HEAD", "Document_Code", "TSPL_BAG_RECEIPT_DETAIL", "Document_Code", trans)
        Return isSaved

    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal arrloc As String, ByVal NavType As NavigatorType) As ClsBagReceipt
        Return GetData(strCode, arrloc, NavType, Nothing)
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal arrloc As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As ClsBagReceipt
        Dim obj As New ClsBagReceipt()
        Dim objtr As New clsBagReceiptDetail()

        Dim LocCond As String = " where 1=1 "
        If clsCommon.myLen(arrloc) > 0 Then
            LocCond = LocCond & " and T1.LOCATION_CODE in (" + arrloc + ")"
        End If


        Dim qry As String = "SELECT * FROM TSPL_BAG_RECEIPT_HEAD where 2=2   "
        Dim whrClas As String = ""
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_BAG_RECEIPT_HEAD.Document_Code = (select MIN(Document_Code) from TSPL_BAG_RECEIPT_HEAD where 1=1 " + whrClas + ")"
            Case NavigatorType.Last
                qry += " and TSPL_BAG_RECEIPT_HEAD.Document_Code = (select Max(Document_Code) from TSPL_BAG_RECEIPT_HEAD where 1=1 " + whrClas + ")"
            Case NavigatorType.Next
                qry += " and TSPL_BAG_RECEIPT_HEAD.Document_Code = (select Min(Document_Code) from TSPL_BAG_RECEIPT_HEAD where Document_Code>'" + strCode + "' " + whrClas + ")"
            Case NavigatorType.Previous
                qry += " and TSPL_BAG_RECEIPT_HEAD.Document_Code = (select Max(Document_Code) from TSPL_BAG_RECEIPT_HEAD where Document_Code<'" + strCode + "' " + whrClas + ")"
            Case NavigatorType.Current
                qry += " and TSPL_BAG_RECEIPT_HEAD.Document_Code = '" + strCode + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New ClsBagReceipt()
            obj.Document_Code = clsCommon.myCstr(dt.Rows(0)("Document_Code"))
            obj.Document_Date = clsCommon.myCstr(dt.Rows(0)("Document_Date"))
            obj.Location = clsCommon.myCstr(dt.Rows(0)("Location"))
            obj.Remarks = clsCommon.myCstr(dt.Rows(0)("Remarks"))
            obj.Status = IIf(clsCommon.myCdbl(dt.Rows(0)("Status")) = 0, ERPTransactionStatus.Pending, ERPTransactionStatus.Approved)

            'qry = "Select TSPL_BAG_RECEIPT_DETAIL.* from TSPL_BMC_TRANSPORTER_BILL_DETAIL 
            '       where TSPL_BAG_RECEIPT_DETAIL.Document_Code='" + obj.Document_Code + "' ORDER BY TSPL_BAG_RECEIPT_DETAIL.PK_ID"
            'obj.ArrDT = clsDBFuncationality.GetDataTable(qry, trans)
            'If (obj.ArrDT IsNot Nothing AndAlso obj.ArrDT.Rows.Count > 0) Then
            '    obj.Arr = New List(Of clsBagReceiptDetail)
            '    For Each dr As DataRow In obj.ArrDT.Rows
            '        'Dim objTr As New clsBagReceiptDetail
            '        objtr.Document_Code = clsCommon.myCstr(dr("Document_Code"))
            '        objtr.PK_ID = clsCommon.myCstr(dr("PK_ID"))
            '        objtr.Item_Code = clsCommon.myCstr(dr("Item_Code"))
            '        objtr.UOM = clsCommon.myCstr(dr("UOM"))
            '        objtr.Qty = clsCommon.myCdbl(dr("Qty"))
            '        obj.Arr.Add(objtr)
            '    Next


            'End If
            obj.ArrGunny = clsBagReceiptDetail.GetData(strCode, trans)
        End If
        Return obj
    End Function

    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Code not found to Delete")
            End If
            Dim dt As DataTable = clsDBFuncationality.GetDataTable("select Document_Date,LOCATION from TSPL_BAG_RECEIPT_HEAD where Document_Code='" + strCode + "'", trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                clsERPFuncationality.ValidateLocationSegment(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleProductionSTD, clsUserMgtCode.frmStanderdProductionEntry, clsCommon.myCstr(dt.Rows(0)("LOCATION_CODE")), clsCommon.myCDate(dt.Rows(0)("PROD_DATE")), trans)
            End If

            Dim obj As ClsBagReceipt = ClsBagReceipt.GetData(strCode, "", NavigatorType.Current, trans)

            If (obj.Status = True) Then
                Throw New Exception("Already Posted on :" + obj.Posted_Date)
            End If

            clsSerializeInvenotry.DeleteData("Production", strCode, trans)

            HistoryData(strCode, trans)
            clsCommonFunctionality.SaveCancelData(objCommonVar.CurrentUserCode, strCode, "TSPL_SPP_PRODUCTION_ENTRY", "PROD_ENTRY_CODE", "TSPL_SPP_PRODUCTION_ENTRY_DETAIL", "PROD_ENTRY_CODE", "TSPL_SPP_PRODUCTION_CONSUMPTION_DETAIL", "PROD_ENTRY_CODE", trans)

            Dim qry As String = "delete from TSPL_BAG_RECEIPT_DETAIL where Document_Code ='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_BAG_RECEIPT_HEAD where Document_Code ='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)


            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message.ToString())
        End Try
        Return True
    End Function

    Public Shared Function HistoryData(ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        Return clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strCode, "TSPL_BAG_RECEIPT_HEAD", "Document_Code", "TSPL_BAG_RECEIPT_DETAIL", "Document_Code", trans)
    End Function

    Public Shared Function PostData(ByVal Form_Id As String, ByVal strDocNo As String, ByVal arrloc As String, ByVal isCheckForPosted As Boolean) As Boolean
        Dim trans As SqlTransaction = Nothing
        Try
            trans = clsDBFuncationality.GetTransactin
            PostData(Form_Id, strDocNo, arrloc, isCheckForPosted, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    'Public Shared Function UpdateInventoryMovement(ByVal Form_Id As String, ByVal obj As clsStanderdProductionEntry, ByVal arrloc As String, ByVal trans As SqlTransaction) As Boolean
    '    clsDBFuncationality.ExecuteNonQuery("update tspl_batch_item set Against_Inv_Movement_Trans_Id=null where Document_Code='" & obj.PROD_ENTRY_CODE & "'", trans)
    '    clsDBFuncationality.ExecuteNonQuery("Delete from TSPL_INVENTORY_MOVEMENT where Source_Doc_No='" & obj.PROD_ENTRY_CODE & "'", trans)

    '    clsStanderdProductionEntryRM.SaveRM(obj.PROD_ENTRY_CODE, arrloc, trans)
    '    clsStanderdProductionEntryRM.UpdateInventoryMovement(Form_Id, obj.PROD_ENTRY_CODE, arrloc, trans)
    '    Return True
    'End Function
    Public Shared Function PostData(ByVal Form_Id As String, ByVal strDocNo As String, ByVal arrloc As String, ByVal isCheckForPosted As Boolean, ByVal trans As SqlTransaction) As Boolean

        If (clsCommon.myLen(strDocNo) <= 0) Then
            Throw New Exception("Code not found to Post")
        End If
        Dim strPostDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt")
        Dim obj As ClsBagReceipt = ClsBagReceipt.GetData(strDocNo, arrloc, NavigatorType.Current, trans)

        clsERPFuncationality.ValidateLocationSegment(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleProductionSTD, clsUserMgtCode.frmStanderdProductionEntry, obj.Location, obj.Document_Date, trans)



        If (obj Is Nothing OrElse clsCommon.myLen(obj.Document_Code) <= 0) Then
            Throw New Exception("No Data found to Post")
        End If
        If (isCheckForPosted AndAlso obj.Status = True) Then
            Throw New Exception("Already Post on :" + obj.Posted_Date)
        End If
        Dim isSaved As Boolean = True
        'UpdateInventoryMovement(Form_Id, obj, arrloc, trans)
        Dim qry As String = "Update TSPL_SPP_PRODUCTION_ENTRY set POSTED=1, Posting_Date='" + strPostDate + "',Modified_By='" + objCommonVar.CurrentUserCode + "' where PROD_ENTRY_CODE ='" + strDocNo + "'"
        isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
        'isSaved = isSaved And JournalEntry(trans, obj)
        HistoryData(strDocNo, trans)
        Return isSaved

    End Function
End Class

Public Class clsBagReceiptDetail
#Region "Variables"
    Public PK_ID As Integer
    Public Document_Code As String = Nothing
    Public Item_Code As String = Nothing
    Public Item_Name As String = Nothing
    Public UOM As String = Nothing
    Public Qty As Decimal

#End Region

    Public Shared Function SaveData(ByVal ReceiptCode As String, ByVal arr As List(Of clsBagReceiptDetail), Optional ByVal trans As SqlTransaction = Nothing) As Boolean
        If arr IsNot Nothing AndAlso arr.Count > 0 Then
            For Each objtr As clsBagReceiptDetail In arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Document_Code", ReceiptCode)
                clsCommon.AddColumnsForChange(coll, "Item_Code", objtr.Item_Code)
                clsCommon.AddColumnsForChange(coll, "UOM", objtr.UOM)
                clsCommon.AddColumnsForChange(coll, "Qty", objtr.Qty)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_BAG_RECEIPT_DETAIL", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function

    Public Shared Function GetData(ByVal ReceiptCode As String, Optional ByVal trans As SqlTransaction = Nothing) As List(Of clsBagReceiptDetail)
        Dim qry As String = ""
        Dim objList As New List(Of clsBagReceiptDetail)
        Dim objtr As New clsBagReceiptDetail
        qry = "select TSPL_BAG_RECEIPT_DETAIL.*,TSPL_ITEM_MASTER.ITEM_DESC 
from TSPL_BAG_RECEIPT_DETAIL  
LEFT JOIN TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.ITEM_CODE=TSPL_BAG_RECEIPT_DETAIL.Item_Code
where TSPL_BAG_RECEIPT_DETAIL.Document_Code='" & ReceiptCode & "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        For Each dr As DataRow In dt.Rows
            objtr = New clsBagReceiptDetail
            objtr.PK_ID = clsCommon.myCDecimal(dr.Item("PK_ID"))
            objtr.Document_Code = clsCommon.myCstr(dr.Item("Document_Code"))
            objtr.Item_Code = clsCommon.myCstr(dr.Item("Item_Code"))
            objtr.Item_Name = clsCommon.myCstr(dr.Item("ITEM_DESC"))
            objtr.UOM = clsCommon.myCstr(dr.Item("UOM"))
            objtr.Qty = clsCommon.myCDecimal(dr.Item("Qty"))
            objList.Add(objtr)
        Next
        Return objList
    End Function

End Class
