Imports common
Imports System.Data.SqlClient

Public Class clsTankerCleaningItemHead
#Region "Variables"
    Public Code As String = Nothing
    Public Apply_Date As DateTime
    Public Description As String = Nothing
    Public Location As String = Nothing
    Public LocationName As String = Nothing 'Not a table field
    Public Sub_Location As String = Nothing
    Public Sub_LocationName As String = Nothing 'Not a table field
    Public Status As ERPTransactionStatus = ERPTransactionStatus.Pending
    Public Is_Closed As Boolean = False
    Public Arr As List(Of clsTankerCleaningItemDetail) = Nothing
#End Region

    Public Function SaveData(ByVal obj As clsTankerCleaningItemHead, ByVal isNewEntry As Boolean) As Boolean
        Dim isSaved As Boolean = True
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            isSaved = isSaved AndAlso SaveData(obj, isNewEntry, trans)

            If isSaved Then
                trans.Commit()
            End If
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function

    Public Function SaveData(ByVal obj As clsTankerCleaningItemHead, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim qry As String = "delete from TSPL_TANKER_CLEANING_ITEM_DETAIL where Code='" + obj.Code + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            Dim strDocNo As String = ""
            If isNewEntry Then
                obj.Code = clsERPFuncationality.GetNextCode(trans, obj.Apply_Date, clsDocType.TankerCleaningItems, "", obj.Location)
            End If
            If (clsCommon.myLen(obj.Code) <= 0) Then
                Throw New Exception("Error in Document Code Generation")
            End If
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Apply_Date", clsCommon.GetPrintDate(obj.Apply_Date, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Description", obj.Description)
            clsCommon.AddColumnsForChange(coll, "Location", obj.Location)
            clsCommon.AddColumnsForChange(coll, "Sub_Location", obj.Sub_Location)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Code", obj.Code)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_TANKER_CLEANING_ITEM_HEAD", OMInsertOrUpdate.Insert, "", trans)
            Else
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_TANKER_CLEANING_ITEM_HEAD", OMInsertOrUpdate.Update, "Code='" + obj.Code + "'", trans)
            End If
            clsTankerCleaningItemDetail.SaveData(obj.Code, obj.Apply_Date, obj.Arr, trans)

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As common.NavigatorType) As clsTankerCleaningItemHead
        Return GetData(strCode, NavType, Nothing)
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As common.NavigatorType, ByVal trans As SqlTransaction) As clsTankerCleaningItemHead
        Dim obj As clsTankerCleaningItemHead = Nothing
        Dim whrclas As String = ""
        Dim qry As String = "select TSPL_TANKER_CLEANING_ITEM_HEAD.*,TSPL_LOCATION_MASTER.Location_Desc,SubL.Location_Desc as Sub_LocationName"
        qry += " FROM TSPL_TANKER_CLEANING_ITEM_HEAD left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_TANKER_CLEANING_ITEM_HEAD.Location left outer join TSPL_LOCATION_MASTER as SubL on SubL.Location_Code=TSPL_TANKER_CLEANING_ITEM_HEAD.Sub_Location  where  2=2"
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            whrclas += " AND Location in (" + objCommonVar.strCurrUserLocations + ")"
        End If
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_TANKER_CLEANING_ITEM_HEAD.Code=(select MIN(Code) from TSPL_TANKER_CLEANING_ITEM_HEAD Where 1=1 " + whrclas + ")"
            Case NavigatorType.Last
                qry += " and TSPL_TANKER_CLEANING_ITEM_HEAD.Code=(select Max(Code) from TSPL_TANKER_CLEANING_ITEM_HEAD Where 1=1 " + whrclas + ")"
            Case NavigatorType.Next
                qry += " and TSPL_TANKER_CLEANING_ITEM_HEAD.Code=(select Min(Code) from TSPL_TANKER_CLEANING_ITEM_HEAD where Code > '" + strCode + "' " + whrclas + ")"
            Case NavigatorType.Previous
                qry += " and TSPL_TANKER_CLEANING_ITEM_HEAD.Code=(select Max(Code) from TSPL_TANKER_CLEANING_ITEM_HEAD where Code < '" + strCode + "' " + whrclas + ")"
            Case NavigatorType.Current
                qry += " and TSPL_TANKER_CLEANING_ITEM_HEAD.Code='" + strCode + "'  "
        End Select

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsTankerCleaningItemHead()
            obj.Code = clsCommon.myCstr(dt.Rows(0)("Code"))
            obj.Apply_Date = clsCommon.myCDate(dt.Rows(0)("Apply_Date"))
            obj.Location = clsCommon.myCstr(dt.Rows(0)("Location"))
            obj.LocationName = clsCommon.myCstr(dt.Rows(0)("Location_Desc"))
            obj.Sub_Location = clsCommon.myCstr(dt.Rows(0)("Sub_Location"))
            obj.Sub_LocationName = clsCommon.myCstr(dt.Rows(0)("Sub_LocationName"))
            obj.Description = clsCommon.myCstr(dt.Rows(0)("Description"))
            obj.Is_Closed = (clsCommon.myCdbl(dt.Rows(0)("Is_Closed")) = 1)
            obj.Status = IIf(clsCommon.myCdbl(dt.Rows(0)("Status")) = 1, ERPTransactionStatus.Approved, ERPTransactionStatus.Pending)
            obj.Arr = clsTankerCleaningItemDetail.GetData(obj.Code, trans)
        End If
        Return obj
    End Function

    Public Shared Function CloseprData(ByVal strDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Requistion No not found to close")
            End If
            Dim obj As clsTankerCleaningItemHead = clsTankerCleaningItemHead.GetData(strDocNo, NavigatorType.Current, trans)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.Code) <= 0) Then
                Throw New Exception("No Data found to close")
            End If
            If Not obj.Status = ERPTransactionStatus.Approved Then
                Throw New Exception("Document should be posted for close")
            End If
            If obj.Is_Closed Then
                Throw New Exception("Already closed")
            End If
            Dim qry As String = "Update TSPL_TANKER_CLEANING_ITEM_HEAD set Is_Closed=1,Closed_By='" + objCommonVar.CurrentUserCode + "',Closed_Date='" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt") + "' where Code='" + strDocNo + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function PostData(ByVal strDocNo As String) As Boolean 'BM00000008148
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            PostData(strDocNo, trans)
            trans.Commit()

            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function PostData(ByVal strDocNo As String, ByVal trans As SqlTransaction) As Boolean
        Try
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Document No not found to Post")
            End If
            Dim strPostDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt")
            Dim obj As clsTankerCleaningItemHead = clsTankerCleaningItemHead.GetData(strDocNo, NavigatorType.Current, trans)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.Code) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            If (obj.Status = ERPTransactionStatus.Approved) Then
                Throw New Exception("Already Post")
            End If

            Dim qry As String = "Update TSPL_TANKER_CLEANING_ITEM_HEAD set Status=1, Posted_Date='" + strPostDate + "',Posted_By='" + objCommonVar.CurrentUserCode + "' where Code='" + strDocNo + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function DeleteData(ByVal strCode As String) As Boolean

        Dim obj As clsTankerCleaningItemHead = clsTankerCleaningItemHead.GetData(strCode, NavigatorType.Current)
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Code) > 0) Then
            Try
                If (clsCommon.myLen(obj.Code) <= 0) Then
                    Throw New Exception("Document No not found to Delete")
                End If
                If (obj.Status = ERPTransactionStatus.Approved) Then
                    Throw New Exception("Posted Document can't be delete")
                End If
                Dim qry As String = "delete from TSPL_TANKER_CLEANING_ITEM_DETAIL where Code='" + strCode + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)

                qry = "delete from TSPL_TANKER_CLEANING_ITEM_HEAD where Code='" + strCode + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
                trans.Commit()
            Catch ex As Exception
                trans.Rollback()
                Throw New Exception(ex.Message)
            End Try
        End If
        Return True
    End Function

    Public Shared Function GetLatestCleaningItem(ByVal strLocCode As String, ByVal TransDate As DateTime, ByVal Trans As SqlTransaction) As clsTankerCleaningItemHead
        Dim qry As String = "select top 1 TSPL_TANKER_CLEANING_ITEM_HEAD.Code from TSPL_TANKER_CLEANING_ITEM_HEAD  " + Environment.NewLine + _
        "where TSPL_TANKER_CLEANING_ITEM_HEAD.location='" + strLocCode + "' and TSPL_TANKER_CLEANING_ITEM_HEAD.Is_Closed=0 and TSPL_TANKER_CLEANING_ITEM_HEAD.Status=1 " + Environment.NewLine + _
        "and TSPL_TANKER_CLEANING_ITEM_HEAD.Apply_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(TransDate), "dd/MMM/yyyy hh:mm:ss tt") + "' order by TSPL_TANKER_CLEANING_ITEM_HEAD.Apply_Date desc"
        Return clsTankerCleaningItemHead.GetData(clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, Trans)), NavigatorType.Current, Trans)
    End Function
End Class

Public Class clsTankerCleaningItemDetail
#Region "Variables"
    Public TR_Code As String
    Public SNo As Integer
    Public Code As String = Nothing
    Public Item_Code As String = Nothing
    Public Item_Desc As String = Nothing
    Public Qty As Double = 0
    Public Unit_Code As String = Nothing
#End Region

    Public Shared Function SaveData(ByVal DocCode As String, ByVal DocDate As DateTime, ByVal Arr As List(Of clsTankerCleaningItemDetail), ByVal trans As SqlTransaction) As Boolean
        Dim intLineNo As Integer = 1
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsTankerCleaningItemDetail In Arr
                Dim coll As New Hashtable()
                obj.TR_Code = clsERPFuncationality.GetNextCode(trans, DocDate, clsDocType.Detail, clsDocTransactionType.Detail, "")
                clsCommon.AddColumnsForChange(coll, "TR_Code", obj.TR_Code)
                clsCommon.AddColumnsForChange(coll, "Code", DocCode)
                clsCommon.AddColumnsForChange(coll, "SNo", intLineNo)
                clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code)
                clsCommon.AddColumnsForChange(coll, "Unit_Code", obj.Unit_Code)
                clsCommon.AddColumnsForChange(coll, "Qty", obj.Qty)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_TANKER_CLEANING_ITEM_DETAIL", OMInsertOrUpdate.Insert, "", trans)
                intLineNo = intLineNo + 1
            Next
        End If
        Return True
    End Function

    Public Shared Function GetData(ByVal DocCode As String, ByVal trans As SqlTransaction) As List(Of clsTankerCleaningItemDetail)
        Dim Arr As List(Of clsTankerCleaningItemDetail) = Nothing
        If clsCommon.myLen(DocCode) > 0 Then
            Dim qry As String = "SELECT TSPL_TANKER_CLEANING_ITEM_DETAIL.*,TSPL_ITEM_MASTER.Item_Desc FROM TSPL_TANKER_CLEANING_ITEM_DETAIL left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code =TSPL_TANKER_CLEANING_ITEM_DETAIL.Item_Code where TSPL_TANKER_CLEANING_ITEM_DETAIL.Code='" + DocCode + "' ORDER BY TSPL_TANKER_CLEANING_ITEM_DETAIL.SNo"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                Arr = New List(Of clsTankerCleaningItemDetail)
                Dim objTr As clsTankerCleaningItemDetail
                For Each dr As DataRow In dt.Rows
                    objTr = New clsTankerCleaningItemDetail()
                    objTr.Code = clsCommon.myCstr(dr("Code"))
                    objTr.SNo = clsCommon.myCdbl(dr("SNo"))
                    objTr.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                    objTr.Item_Desc = clsCommon.myCstr(dr("Item_Desc"))
                    objTr.Qty = clsCommon.myCdbl(dr("Qty"))
                    objTr.Unit_Code = clsCommon.myCstr(dr("Unit_Code"))
                    Arr.Add(objTr)
                Next
            End If
        End If
        Return Arr
    End Function
End Class

