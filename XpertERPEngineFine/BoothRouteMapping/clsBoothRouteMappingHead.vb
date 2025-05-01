Imports System.Data.SqlClient
Public Class clsBoothRouteMappingHead
#Region "Variable"
    Public Document_No As String = Nothing
    Public Supply_Date As DateTime = Nothing
    Public Shift_Type As String = Nothing
    Public Route_No As String = Nothing
    Public Item_Type As String = Nothing
    Public Remark As String = Nothing
    Public Posted As Integer = 0
    Public Posted_Date As DateTime = Nothing
    Public Arr As List(Of clsBoothRouteMappingDetail)

#End Region
    Public Function SaveData(ByVal obj As clsBoothRouteMappingHead, ByVal isNewEntry As Boolean) As Boolean
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
    Public Shared Function SaveData(ByVal obj As clsBoothRouteMappingHead, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim qry As String = "delete from TSPL_BOOTH_ROUTE_MAPPING_DETAIL where Document_No='" + clsCommon.myCstr(obj.Document_No) + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Supply_Date", clsCommon.GetPrintDate(obj.Supply_Date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Shift_Type", obj.Shift_Type)
            clsCommon.AddColumnsForChange(coll, "Route_No", obj.Route_No)
            clsCommon.AddColumnsForChange(coll, "Item_Type", obj.Item_Type)
            clsCommon.AddColumnsForChange(coll, "Remark", obj.Remark)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))

            If isNewEntry Then
                obj.Document_No = clsERPFuncationality.GetNextCode(trans, DateTime.Now, clsDocType.BoothRouteMapping, "", "")
                If clsCommon.myLen(obj.Document_No) <= 0 Then
                    Throw New Exception("Error in Code Generation")
                End If
                clsCommon.AddColumnsForChange(coll, "Document_No", obj.Document_No)

                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_BOOTH_ROUTE_MAPPING_HEAD", OMInsertOrUpdate.Insert, "", trans)
            Else
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_BOOTH_ROUTE_MAPPING_HEAD", OMInsertOrUpdate.Update, "Document_No='" + clsCommon.myCstr(obj.Document_No) + "'", trans)
            End If
            clsBoothRouteMappingDetail.SaveData(obj.Document_No, obj.Arr, trans)
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, obj.Document_No, "TSPL_BOOTH_ROUTE_MAPPING_HEAD", "Document_No", "TSPL_BOOTH_ROUTE_MAPPING_DETAIL", "Document_No", trans)

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

        Return True
    End Function
    Public Shared Function SaveBulkImport(ByVal lstObj As List(Of clsBoothRouteMappingHead), ByVal isDefaultPost As Boolean) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            For Each obj As clsBoothRouteMappingHead In lstObj
                SaveData(obj, True, trans)
                If isDefaultPost Then
                    PostData(obj.Document_No, trans)
                End If
            Next
            'SaveData(obj, isNewEntry, trans)
            trans.Commit()
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function

    Public Shared Function GetData(ByVal strDocNo As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsBoothRouteMappingHead
        Dim obj As clsBoothRouteMappingHead = Nothing

        Try
            Dim Whrcls As String = ""
            Dim strQry As String = "select Document_No,Supply_Date,Shift_Type,Route_No,Item_Type,Remark,Posted,Posted_Date from TSPL_BOOTH_ROUTE_MAPPING_HEAD where 2=2"

            Select Case NavType
                Case NavigatorType.First
                    strQry += " and TSPL_BOOTH_ROUTE_MAPPING_HEAD.Document_No = (select MIN(Document_No) from TSPL_BOOTH_ROUTE_MAPPING_HEAD where 1=1 " + Whrcls + "  )"
                Case NavigatorType.Last
                    strQry += " and TSPL_BOOTH_ROUTE_MAPPING_HEAD.Document_No = (select Max(Document_No) from TSPL_BOOTH_ROUTE_MAPPING_HEAD where 1=1 " + Whrcls + "  )"
                Case NavigatorType.Next
                    strQry += " and TSPL_BOOTH_ROUTE_MAPPING_HEAD.Document_No = (select Min(Document_No) from TSPL_BOOTH_ROUTE_MAPPING_HEAD where Document_No>'" + clsCommon.myCstr(strDocNo) + "' " + Whrcls + "   )"
                Case NavigatorType.Previous
                    strQry += " and TSPL_BOOTH_ROUTE_MAPPING_HEAD.Document_No = (select Max(Document_No) from TSPL_BOOTH_ROUTE_MAPPING_HEAD where Document_No<'" + clsCommon.myCstr(strDocNo) + "' " + Whrcls + "  )"
                Case NavigatorType.Current
                    strQry += " and TSPL_BOOTH_ROUTE_MAPPING_HEAD.Document_No = '" + clsCommon.myCstr(strDocNo) + "'  " + Whrcls + " "
            End Select

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(strQry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then

                obj = New clsBoothRouteMappingHead()
                obj.Document_No = clsCommon.myCstr(dt.Rows(0)("Document_No"))
                obj.Supply_Date = clsCommon.GetPrintDate(dt.Rows(0)("Supply_Date"), "dd/MMM/yyyy")
                obj.Shift_Type = clsCommon.myCstr(dt.Rows(0)("Shift_Type"))
                obj.Route_No = clsCommon.myCstr(dt.Rows(0)("Route_No"))
                obj.Item_Type = clsCommon.myCstr(dt.Rows(0)("Item_Type"))
                obj.Remark = clsCommon.myCstr(dt.Rows(0)("Remark"))
                obj.Posted = IIf(clsCommon.myCDecimal(dt.Rows(0)("Posted")) = 1, ERPTransactionStatus.Approved, ERPTransactionStatus.Pending)
                If dt.Rows(0)("Posted_Date") IsNot DBNull.Value Then
                    obj.Posted_Date = clsCommon.myCDate(dt.Rows(0)("Posted_Date"))
                End If
                obj.Arr = clsBoothRouteMappingDetail.GetData(obj.Document_No, trans)


            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return obj
    End Function
    Public Shared Function DeleteData(ByVal strDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            DeleteData(strDocNo, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function DeleteData(ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean
        Dim obj As New clsBoothRouteMappingHead()
        Try
            isSaved = False

            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Code not found to Delete")
            End If
            clsCommonFunctionality.SaveDeletedData(objCommonVar.CurrentUserCode, strCode, "TSPL_BOOTH_ROUTE_MAPPING_HEAD", "Document_No", "TSPL_BOOTH_ROUTE_MAPPING_DETAIL", "Document_No", trans)
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strCode, "TSPL_BOOTH_ROUTE_MAPPING_HEAD", "Document_No", "TSPL_BOOTH_ROUTE_MAPPING_DETAIL", "Document_No", trans)

            Dim isPosted As Integer = 0
            isPosted = clsDBFuncationality.getSingleValue("SELECT Count(*) FROM TSPL_BOOTH_ROUTE_MAPPING_HEAD where Document_No = '" & strCode & "' and Posted=1", trans)
            If (isPosted = 1) Then
                Throw New Exception("Already Posted on :" + obj.Posted_Date)
            End If

            Dim qry As String

            qry = "DELETE FROM TSPL_BOOTH_ROUTE_MAPPING_DETAIL WHERE Document_No='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_BOOTH_ROUTE_MAPPING_HEAD where Document_No ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
        Catch ex As Exception

            Throw New Exception(ex.Message.ToString())
        End Try
        Return isSaved
    End Function
    Public Shared Function PostData(ByVal strDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            PostData(strDocNo, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function PostData(ByVal strDocNo As String, ByVal trans As SqlTransaction) As Boolean
        Try
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Code not found to Post")
            End If
            Dim obj As clsBoothRouteMappingHead = clsBoothRouteMappingHead.GetData(strDocNo, NavigatorType.Current, trans)
            'clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleMCCMilkProcurement, clsUserMgtCode.MilkShiftUploader, obj.MCC_Code, obj.Document_Date, trans)

            If (obj Is Nothing OrElse clsCommon.myLen(obj.Document_No) <= 0) Then
                Throw New Exception("Code : " + strDocNo + " not found to Post")
            End If
            If (obj.Posted = ERPTransactionStatus.Approved) Then
                Throw New Exception("Already Posted on :" + obj.Posted_Date)
            End If
            For Each items As clsBoothRouteMappingDetail In obj.Arr
                Dim RouteDesc As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Route_Desc from TSPL_ROUTE_MASTER where Route_No='" + obj.Route_No + "'", trans))
                clsDBFuncationality.ExecuteNonQuery("update TSPL_CUSTOMER_MASTER set Route_No='" + obj.Route_No + "',Route_Desc='" + RouteDesc + "' where Cust_Code='" + items.Booth_Code + "'", trans)
            Next

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Posted", 1)
            clsCommon.AddColumnsForChange(coll, "Posted_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Posted_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))
            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_BOOTH_ROUTE_MAPPING_HEAD", OMInsertOrUpdate.Update, "Document_No='" + clsCommon.myCstr(obj.Document_No) + "'", trans)
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strDocNo, "TSPL_BOOTH_ROUTE_MAPPING_HEAD", "Document_No", trans)

        Catch ex As Exception

            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
End Class
Public Class clsBoothRouteMappingDetail
#Region "Variable"
    Public PK_ID As Integer = 0
    Public Document_No As String = Nothing
    Public Serial_No As Integer = 0
    Public Booth_Code As String = Nothing
    Public Prev_Route_No As String = Nothing

#End Region
    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsBoothRouteMappingDetail), ByVal trans As SqlTransaction) As Boolean
        Try


            If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
                For Each obj As clsBoothRouteMappingDetail In Arr
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Document_No", strDocNo)
                    clsCommon.AddColumnsForChange(coll, "Serial_No", obj.Serial_No)
                    clsCommon.AddColumnsForChange(coll, "Booth_Code", obj.Booth_Code)
                    clsCommon.AddColumnsForChange(coll, "Prev_Route_No", obj.Prev_Route_No)

                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_BOOTH_ROUTE_MAPPING_DETAIL", OMInsertOrUpdate.Insert, "", trans)

                Next
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function GetData(ByVal strDocNo As String, ByVal trans As SqlTransaction) As List(Of clsBoothRouteMappingDetail)
        Dim arr As List(Of clsBoothRouteMappingDetail) = Nothing

        Try
            Dim dt As DataTable
            Dim strQry As String = "select PK_ID,Document_No,Serial_No,Booth_Code,Prev_Route_No from TSPL_BOOTH_ROUTE_MAPPING_DETAIL where Document_No='" & strDocNo & "' order by Serial_No"
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(strQry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                arr = New List(Of clsBoothRouteMappingDetail)
                Dim objTr As clsBoothRouteMappingDetail
                For Each dr As DataRow In dt.Rows
                    objTr = New clsBoothRouteMappingDetail
                    objTr.PK_ID = clsCommon.myCdbl(dr("PK_ID"))
                    objTr.Document_No = clsCommon.myCstr(dr("Document_No"))
                    objTr.Serial_No = clsCommon.myCstr(dr("Serial_No"))
                    objTr.Booth_Code = clsCommon.myCstr(dr("Booth_Code"))
                    objTr.Prev_Route_No = clsCommon.myCDecimal(dr("Prev_Route_No"))
                    arr.Add(objTr)
                Next
            End If



        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

        Return arr
    End Function

End Class
