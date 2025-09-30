Imports common
Imports System.Data.SqlClient
Public Class clsRouteWiseSaleTarget
#Region "Variables"
    Public Document_Code As String = Nothing
    Public Document_Date As DateTime = Nothing
    Public Month As DateTime = Nothing
    Public UOM As String = Nothing
    Public Target_On As Integer = 0
    Public Item_Sub_Category As String = Nothing
    Public Remarks As String = Nothing
    Public Status As Integer = 0
    Public Inactive As Integer = 0
    Public Created_By As String = Nothing
    Public Created_Date As DateTime = Nothing
    Public Modify_By As String = Nothing
    Public Modify_Date As DateTime = Nothing
    Public Posted_By As String = Nothing
    Public Posted_Date As DateTime = Nothing
    Public Inactive_By As String = Nothing
    Public Inactive_Date As DateTime = Nothing
    Public isPosted As Boolean = False
    Public Arr As List(Of clsRouteWiseSaleTargetDetail)
#End Region

    Public Function SaveData(ByVal obj As clsRouteWiseSaleTarget, ByVal isNewEntry As Boolean) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            SaveData(obj, isNewEntry, trans)
            trans.Commit()
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function

    Public Function SaveData(ByVal obj As clsRouteWiseSaleTarget, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim qry As String = "delete from TSPL_ROUTE_WISE_SALE_TARGET_DETAIL where Document_Code='" & clsCommon.myCstr(obj.Document_Code) & "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Months", clsCommon.GetPrintDate(obj.Month, "MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "UOM", obj.UOM)
            clsCommon.AddColumnsForChange(coll, "Target_On", obj.Target_On)
            clsCommon.AddColumnsForChange(coll, "Item_Sub_Category", obj.Item_Sub_Category)
            clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
            If isNewEntry Then
                obj.Document_Code = clsERPFuncationality.GetNextCode(trans, DateTime.Now, clsDocType.RouteWiseSaleTarget, "", "")
                If clsCommon.myLen(obj.Document_Code) <= 0 Then
                    Throw New Exception("Error in Code Generation")
                End If
                clsCommon.AddColumnsForChange(coll, "Document_Code", obj.Document_Code)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ROUTE_WISE_SALE_TARGET", OMInsertOrUpdate.Insert, "", trans)
            Else
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ROUTE_WISE_SALE_TARGET", OMInsertOrUpdate.Update, "Document_Code='" & clsCommon.myCstr(obj.Document_Code) & "'", trans)
            End If
            Dim objDetail As New clsRouteWiseSaleTargetDetail()
            objDetail.SaveData(obj.Document_Code, obj.Arr, trans)
            objDetail = Nothing
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, obj.Document_Code, "TSPL_ROUTE_WISE_SALE_TARGET", "Document_Code", "TSPL_ROUTE_WISE_SALE_TARGET_DETAIL", "Document_Code", trans)
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function

    Public Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsRouteWiseSaleTarget
        Return GetData(strCode, NavType, Nothing)
    End Function

    Public Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsRouteWiseSaleTarget
        Dim obj As clsRouteWiseSaleTarget = Nothing
        Try
            Dim strQry As String = "Select * from TSPL_ROUTE_WISE_SALE_TARGET Where 2=2 "
            Select Case NavType
                Case NavigatorType.First
                    strQry += " and TSPL_ROUTE_WISE_SALE_TARGET.Document_Code = (select MIN(Document_Code) from TSPL_ROUTE_WISE_SALE_TARGET where 1=1 )"
                Case NavigatorType.Last
                    strQry += " and TSPL_ROUTE_WISE_SALE_TARGET.Document_Code = (select Max(Document_Code) from TSPL_ROUTE_WISE_SALE_TARGET where 1=1 )"
                Case NavigatorType.Next
                    strQry += " and TSPL_ROUTE_WISE_SALE_TARGET.Document_Code = (select Min(Document_Code) from TSPL_ROUTE_WISE_SALE_TARGET where Document_Code>'" & strCode & "' )"
                Case NavigatorType.Previous
                    strQry += " and TSPL_ROUTE_WISE_SALE_TARGET.Document_Code = (select Max(Document_Code) from TSPL_ROUTE_WISE_SALE_TARGET where Document_Code<'" & strCode & "' )"
                Case NavigatorType.Current
                    strQry += " and TSPL_ROUTE_WISE_SALE_TARGET.Document_Code = '" & strCode & "'"
            End Select
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(strQry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                obj = New clsRouteWiseSaleTarget()
                obj.Document_Code = clsCommon.myCstr(dt.Rows(0)("Document_Code"))
                obj.Document_Date = clsCommon.myCDate(dt.Rows(0)("Document_Date"))
                obj.Month = clsCommon.myCDate(dt.Rows(0)("Months"))
                obj.UOM = clsCommon.myCstr(dt.Rows(0)("UOM"))
                obj.Target_On = clsCommon.myCDecimal(dt.Rows(0)("Target_On"))
                obj.Item_Sub_Category = clsCommon.myCstr(dt.Rows(0)("Item_Sub_Category"))
                obj.Remarks = clsCommon.myCstr(dt.Rows(0)("Remarks"))
                obj.Status = clsCommon.myCDecimal(dt.Rows(0)("Status"))
                obj.Inactive = clsCommon.myCDecimal(dt.Rows(0)("Inactive"))

                Dim objDetail As New clsRouteWiseSaleTargetDetail()
                obj.Arr = objDetail.GetData(obj.Document_Code, trans)
                objDetail = Nothing
            End If
        Catch ex As Exception
            obj = Nothing
            Throw New Exception(ex.Message)
        End Try
        Return obj
    End Function

    Public Function DeleteData(ByVal strCode As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            clsDBFuncationality.ExecuteNonQuery("Delete from TSPL_ROUTE_WISE_SALE_TARGET_DETAIL Where Document_Code='" & strCode & "' ", trans)
            clsDBFuncationality.ExecuteNonQuery("Delete from TSPL_ROUTE_WISE_SALE_TARGET Where Document_Code='" & strCode & "' ", trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Function PostData(ByVal strDoc As String) As Boolean
        'Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Status", 1)
            clsCommon.AddColumnsForChange(coll, "Posted_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Posted_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(Nothing), "dd/MMM/yyyy"))
            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ROUTE_WISE_SALE_TARGET", OMInsertOrUpdate.Update, "TSPL_ROUTE_WISE_SALE_TARGET.Document_Code='" & strDoc & "'", Nothing)
            'trans.Commit()
        Catch ex As Exception
            'trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Function DataInactive(ByVal strDoc As String) As Boolean
        'Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Inactive", 1)
            clsCommon.AddColumnsForChange(coll, "Inactive_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Inactive_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(Nothing), "dd/MMM/yyyy"))
            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ROUTE_WISE_SALE_TARGET", OMInsertOrUpdate.Update, "TSPL_ROUTE_WISE_SALE_TARGET.Document_Code='" & strDoc & "'", Nothing)
            'trans.Commit()
        Catch ex As Exception
            'trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Function ReverseAndUnpost(ByVal strCode As String) As Boolean
        Try
            Dim obj As New clsRouteWiseSaleTarget()
            obj = obj.GetData(strCode, NavigatorType.Current)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.Status) <= 0) Then
                clsCommon.MyMessageBoxShow("No Data found to Reverse And UnPost")
                Return False
            End If
            If Not obj.Status = 1 Then
                clsCommon.MyMessageBoxShow("Transaction status should be posted for reverse and unpost")
                Return False
            End If
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Status", 0)
            clsCommon.AddColumnsForChange(coll, "Posted_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Posted_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy"))
            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ROUTE_WISE_SALE_TARGET", OMInsertOrUpdate.Update, "TSPL_ROUTE_WISE_SALE_TARGET.Document_Code='" & strCode & "'", Nothing)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

End Class

Public Class clsRouteWiseSaleTargetDetail
#Region "Variables"
    Public LineNo As Integer = 0
    Public Document_Code As String = Nothing
    Public Route_Code As String = Nothing
    Public Route_Name As String = Nothing
    Public Group_Code As String = Nothing
    Public Group_Name As String = Nothing
    Public Target_Qty As Decimal = 0
#End Region

    Public Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsRouteWiseSaleTargetDetail), ByVal trans As SqlTransaction) As Boolean
        Try
            If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
                For Each objTr In Arr
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Document_Code", strDocNo)
                    If clsCommon.myLen(objTr.Route_Code) > 0 Then
                        clsCommon.AddColumnsForChange(coll, "Route_Code", objTr.Route_Code)
                    Else
                        clsCommon.AddColumnsForChange(coll, "Cust_Group_Code", objTr.Group_Code)
                    End If
                    clsCommon.AddColumnsForChange(coll, "Target_Qty", objTr.Target_Qty)
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ROUTE_WISE_SALE_TARGET_DETAIL", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Function GetData(ByVal strCode As String, ByVal trans As SqlTransaction) As List(Of clsRouteWiseSaleTargetDetail)
        Dim arr As List(Of clsRouteWiseSaleTargetDetail) = Nothing
        Try
            Dim qry As String = "Select TSPL_ROUTE_WISE_SALE_TARGET_DETAIL.*,TSPL_ROUTE_MASTER.Route_Desc,TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Desc from TSPL_ROUTE_WISE_SALE_TARGET_DETAIL
Left Outer Join TSPL_ROUTE_MASTER On TSPL_ROUTE_MASTER.Route_No=TSPL_ROUTE_WISE_SALE_TARGET_DETAIL.Route_Code Left Outer Join TSPL_CUSTOMER_GROUP_MASTER On TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code=TSPL_ROUTE_WISE_SALE_TARGET_DETAIL.Cust_Group_Code Where TSPL_ROUTE_WISE_SALE_TARGET_DETAIL.Document_Code = '" & strCode & "' order by TSPL_ROUTE_WISE_SALE_TARGET_DETAIL.PK_ID,TSPL_ROUTE_WISE_SALE_TARGET_DETAIL.Document_Code"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                arr = New List(Of clsRouteWiseSaleTargetDetail)()
                Dim LineNo As Integer = 1
                For Each dr As DataRow In dt.Rows
                    Dim obj As New clsRouteWiseSaleTargetDetail()
                    obj.LineNo = LineNo
                    obj.Document_Code = clsCommon.myCstr(dr("Document_Code"))
                    obj.Route_Code = clsCommon.myCstr(dr("Route_Code"))
                    obj.Route_Name = clsCommon.myCstr(dr("Route_Desc"))
                    obj.Group_Code = clsCommon.myCstr(dr("Cust_Group_Code"))
                    obj.Group_Name = clsCommon.myCstr(dr("Cust_Group_Desc"))
                    obj.Target_Qty = clsCommon.myCDecimal(dr("Target_Qty"))
                    arr.Add(obj)
                    LineNo += 1
                Next
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return arr
    End Function


End Class
