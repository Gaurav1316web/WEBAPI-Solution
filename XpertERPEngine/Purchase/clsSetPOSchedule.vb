Imports System.Data.SqlClient

Public Class clsSetPOSchedule
#Region "Variables"
    Public Code As String = Nothing
    Public DDate As DateTime
    Public PO_No As String = Nothing
    Public Vendor_Code As String = Nothing
    Public Vendor_Name As String = Nothing ''No a Table Coumns
    Public Schedule_Date As DateTime
    Public Remarks As String = Nothing
    Public Status As ERPTransactionStatus = 0
    Public Posting_Date As DateTime? = Nothing
    Public ArrTr As List(Of clsSetPOScheduleDetail) = Nothing
    Public Form_ID As String = ""
    Public arrCustomFields As List(Of clsCustomFieldValues) = Nothing
#End Region

    Public Function SaveData(ByVal obj As clsSetPOSchedule, ByVal isNewEntry As Boolean) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim qry As String
            If Not isNewEntry Then
                qry = "Select Status from TSPL_SET_PO_SCHEDULE Where Code='" + obj.Code + "'"
                Dim Status As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))
                If Status = 1 Then
                    Throw New Exception("This document is already posted.")
                End If
            End If

            qry = "delete from TSPL_SET_PO_SCHEDULE_DETAIL where Code='" + obj.Code + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "DDate", clsCommon.GetPrintDate(obj.DDate, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Schedule_Date", clsCommon.GetPrintDate(obj.Schedule_Date, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "PO_No", obj.PO_No)
            clsCommon.AddColumnsForChange(coll, "Vendor_Code", obj.Vendor_Code)
            clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
            If isNewEntry Then
                obj.Code = clsERPFuncationality.GetNextCode(trans, obj.DDate, clsDocType.SetPOSchedule, "", "")
                If (clsCommon.myLen(obj.Code) <= 0) Then
                    Throw New Exception("Error in Document Code Generation")
                End If
                clsCommon.AddColumnsForChange(coll, "Code", obj.Code)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SET_PO_SCHEDULE", OMInsertOrUpdate.Insert, "", trans)
            Else
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SET_PO_SCHEDULE", OMInsertOrUpdate.Update, "Code='" + obj.Code + "'", trans)
            End If
            clsSetPOScheduleDetail.SaveData(obj.Code, obj.ArrTr, trans)
            clsCustomFieldValues.SaveData(obj.Form_ID, obj.Code, obj.arrCustomFields, trans)
            trans.Commit()

            'If Not isNewEntry Then
            '    clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, clsCommon.myCstr(obj.Code), "TSPL_SET_PO_SCHEDULE", "Code", "TSPL_SET_PO_SCHEDULE_DETAIL", "Code", trans)
            'End If

        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As common.NavigatorType) As clsSetPOSchedule
        Return GetData(strCode, NavType, Nothing)
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As common.NavigatorType, ByVal trans As SqlTransaction) As clsSetPOSchedule
        Dim obj As clsSetPOSchedule = Nothing
        Dim qry As String = "SELECT TSPL_SET_PO_SCHEDULE.*,TSPL_VENDOR_MASTER.Vendor_Name FROM TSPL_SET_PO_SCHEDULE left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_SET_PO_SCHEDULE.Vendor_Code where  2=2"

        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_SET_PO_SCHEDULE.Code=(select MIN(Code) from TSPL_SET_PO_SCHEDULE  )"
            Case NavigatorType.Last
                qry += " and TSPL_SET_PO_SCHEDULE.Code=(select Max(Code) from TSPL_SET_PO_SCHEDULE  )"
            Case NavigatorType.Next
                qry += " and TSPL_SET_PO_SCHEDULE.Code=(select Min(Code) from TSPL_SET_PO_SCHEDULE where Code > '" + strCode + "' )"
            Case NavigatorType.Previous
                qry += " and TSPL_SET_PO_SCHEDULE.Code=(select Max(Code) from TSPL_SET_PO_SCHEDULE where Code < '" + strCode + "' )"
            Case NavigatorType.Current
                qry += " and TSPL_SET_PO_SCHEDULE.Code='" + strCode + "'"
        End Select

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsSetPOSchedule()
            obj.Code = clsCommon.myCstr(dt.Rows(0)("Code"))
            obj.DDate = clsCommon.myCDate(dt.Rows(0)("DDate"))
            obj.Schedule_Date = clsCommon.myCDate(dt.Rows(0)("Schedule_Date"))
            obj.PO_No = clsCommon.myCstr(dt.Rows(0)("PO_No"))
            obj.Vendor_Code = clsCommon.myCstr(dt.Rows(0)("Vendor_Code"))
            obj.Vendor_Name = clsCommon.myCstr(dt.Rows(0)("Vendor_Name"))
            obj.Remarks = clsCommon.myCstr(dt.Rows(0)("Remarks"))
            If (clsCommon.myCdbl(dt.Rows(0)("Status")) = 0) Then
                obj.Status = ERPTransactionStatus.Pending
            Else
                obj.Status = ERPTransactionStatus.Approved
            End If

            If dt.Rows(0)("Posted_Date") Is DBNull.Value Then
                obj.Posting_Date = Nothing
            Else
                obj.Posting_Date = clsCommon.myCDate(dt.Rows(0)("Posted_Date"))
            End If

            qry = "SELECT TSPL_SET_PO_SCHEDULE_DETAIL.*,TSPL_ITEM_MASTER.Item_Desc FROM TSPL_SET_PO_SCHEDULE_DETAIL left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SET_PO_SCHEDULE_DETAIL.Item_Code  where TSPL_SET_PO_SCHEDULE_DETAIL.Code='" + obj.Code + "' ORDER BY PK_ID"
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj.ArrTr = New List(Of clsSetPOScheduleDetail)
                Dim objTr As clsSetPOScheduleDetail
                For Each dr As DataRow In dt.Rows
                    objTr = New clsSetPOScheduleDetail()
                    objTr.Code = clsCommon.myCstr(dr("Code"))
                    objTr.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                    objTr.Item_Desc = clsCommon.myCstr(dr("Item_Desc"))
                    objTr.Qty = clsCommon.myCdbl(dr("Qty"))
                    objTr.UOM = clsCommon.myCstr(dr("UOM"))
                    obj.ArrTr.Add(objTr)
                Next
            End If
        End If

        Return obj
    End Function

    Public Shared Function PostData(ByVal strDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Document No not found to Post")
            End If
            Dim strPostDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt")
            Dim obj As clsSetPOSchedule = clsSetPOSchedule.GetData(strDocNo, NavigatorType.Current, trans)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.Code) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            If (obj.Status = ERPTransactionStatus.Approved) Then
                Throw New Exception("Already Post on :" + obj.Posting_Date)
            End If

            Dim qry As String = "Update TSPL_SET_PO_SCHEDULE set Status=1,Posted_Date='" + strPostDate + "' where Code='" + strDocNo + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            trans.Commit()

        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        If (clsCommon.myLen(strCode) <= 0) Then
            Throw New Exception("Document No not found to Delete")
        End If
        Dim obj As clsSetPOSchedule = clsSetPOSchedule.GetData(strCode, NavigatorType.Current)
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Code) > 0) Then
            Try
                If (obj.Status = ERPTransactionStatus.Approved) Then
                    Throw New Exception("Already Post on :" + obj.Posting_Date)
                End If
                Dim qry As String = "delete from TSPL_SET_PO_SCHEDULE_DETAIL where Code='" + strCode + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)

                qry = "delete from TSPL_SET_PO_SCHEDULE where Code='" + strCode + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
                clsCustomFieldValues.DeleteData(obj.Form_ID, obj.Code, trans)
                trans.Commit()
            Catch ex As Exception
                trans.Rollback()
                Throw New Exception(ex.Message)
            End Try
        End If
        Return True
    End Function

    Public Shared Function GetPendingBaseQry(ByVal strPONo As String, ByVal strScheduleNo As String, ByVal strVendorCode As String) As String
        Dim qry As String = "select PurchaseOrder_No,max(PurchaseOrder_Date) as PurchaseOrder_Date,Item_Code,SUM(PurchaseOrder_Qty*case when RI=1 then 1 else 0 end) as POQty,SUM(PurchaseOrder_Qty*RI) as BalanceQty,max(Unit_code) as Unit_code   from (
select tspl_Purchase_order_head.PurchaseOrder_No,tspl_Purchase_order_head.PurchaseOrder_Date,tspl_Purchase_order_head.Vendor_Code,tspl_Purchase_order_head.Vendor_Name,TSPL_PURCHASE_ORDER_DETAIL.Item_Code,TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_Qty,TSPL_PURCHASE_ORDER_DETAIL.Unit_code,1 as Chk,1 as RI
from TSPL_PURCHASE_ORDER_DETAIL 
left outer join TSPL_PURCHASE_ORDER_HEAD on TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No=TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No 
where  TSPL_PURCHASE_ORDER_HEAD.Status=1 and TSPL_PURCHASE_ORDER_HEAD.Vendor_Code='" + strVendorCode + "' and TSPL_PURCHASE_ORDER_DETAIL.Row_Type='Item'"
        If clsCommon.myLen(strPONo)>0 Then
            qry+= " and TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No='"+strPONo+"'"
        End If
        qry += "union all
select TSPL_SET_PO_SCHEDULE.PO_No as PurchaseOrder_No,null as PurchaseOrder_Date,TSPL_SET_PO_SCHEDULE.Vendor_Code,null as Vendor_Name,TSPL_SET_PO_SCHEDULE_DETAIL.Item_Code,TSPL_SET_PO_SCHEDULE_DETAIL.Qty as PurchaseOrder_Qty,null as Unit_code,0 as Chk,-1 as RI
from TSPL_SET_PO_SCHEDULE_DETAIL 
left outer join TSPL_SET_PO_SCHEDULE on TSPL_SET_PO_SCHEDULE.Code=TSPL_SET_PO_SCHEDULE_DETAIL.code
where TSPL_SET_PO_SCHEDULE.Vendor_Code='" + strVendorCode + "' and TSPL_SET_PO_SCHEDULE.Code not in ('" + strScheduleNo + "')
) xx group by PurchaseOrder_No,Item_Code having SUM(PurchaseOrder_Qty*RI)>0 and sum(chk)>0"
        Return qry
    End Function
End Class

Public Class clsSetPOScheduleDetail
#Region "Variables"
    Public Code As String = Nothing
    Public Item_Code As String = Nothing
    Public Item_Desc As String = Nothing ''Not a table Column
    Public Qty As Double = 0
    Public UOM As String = Nothing
#End Region
    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsSetPOScheduleDetail), ByVal trans As SqlTransaction, Optional ByVal import As Boolean = False) As Boolean
        Dim intLineNo As Integer = 1
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsSetPOScheduleDetail In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Code", strDocNo)
                clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code)
                clsCommon.AddColumnsForChange(coll, "Qty", obj.Qty)
                clsCommon.AddColumnsForChange(coll, "UOM", obj.UOM)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SET_PO_SCHEDULE_DETAIL", OMInsertOrUpdate.Insert, "", trans)
                intLineNo = intLineNo + 1
            Next
        End If
        Return True
    End Function
End Class