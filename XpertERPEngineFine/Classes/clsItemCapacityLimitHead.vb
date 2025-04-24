Imports System.Data.SqlClient

Public Class clsItemCapacityLimitHead

#Region "Variable"
    Public Document_No As String = Nothing
    Public Document_Date As DateTime = Nothing
    Public From_Date As DateTime = Nothing
    Public To_Date As DateTime? = Nothing
    Public IN_Active As Boolean = False
    Public InActive_date As DateTime = Nothing
    Public Posted As Integer = 0
    Public Posted_Date As DateTime = Nothing
    Public Arr As List(Of clsItemCapacityLimitDetail)

#End Region
    Public Function SaveData(ByVal obj As clsItemCapacityLimitHead, ByVal isNewEntry As Boolean) As Boolean
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
    Public Function SaveData(ByVal obj As clsItemCapacityLimitHead, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim qry As String = "delete from TSPL_ITEM_CAPACITY_LIMIT_DETAIL where Document_No='" + clsCommon.myCstr(obj.Document_No) + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "From_Date", clsCommon.GetPrintDate(obj.From_Date, "dd/MMM/yyyy"))
            If obj.To_Date IsNot Nothing Then
                clsCommon.AddColumnsForChange(coll, "To_Date", clsCommon.GetPrintDate(obj.To_Date, "dd/MMM/yyyy"), True)
            End If
            clsCommon.AddColumnsForChange(coll, "IN_Active", IIf(obj.IN_Active, 1, 0))
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
            If obj.IN_Active Then
                clsCommon.AddColumnsForChange(coll, "InActive_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "InActive_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))

            End If
            If isNewEntry Then
                obj.Document_No = clsERPFuncationality.GetNextCode(trans, DateTime.Now, clsDocType.ItemCapacityLimit, "", "")
                If clsCommon.myLen(obj.Document_No) <= 0 Then
                    Throw New Exception("Error in Code Generation")
                End If
                clsCommon.AddColumnsForChange(coll, "Document_No", obj.Document_No)

                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ITEM_CAPACITY_LIMIT_HEAD", OMInsertOrUpdate.Insert, "", trans)
            Else
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ITEM_CAPACITY_LIMIT_HEAD", OMInsertOrUpdate.Update, "Document_No='" + clsCommon.myCstr(obj.Document_No) + "'", trans)
            End If
            clsItemCapacityLimitDetail.SaveData(obj.Document_No, obj.Arr, trans)
            'clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, obj.Document_No, "TSPL_ITEM_CAPACITY_LIMIT_HEAD", "Document_No", "TSPL_ITEM_CAPACITY_LIMIT_DETAIL", "Document_No", "TSPL_ITEM_CAPACITY_LIMIT_DETAIL_ALL_UOM", "Document_No", trans)

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

        Return True
    End Function
    Public Shared Function GetData(ByVal strDocNo As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsItemCapacityLimitHead
        Dim obj As clsItemCapacityLimitHead = Nothing

        Try
            Dim Whrcls As String = ""
            Dim strQry As String = "select Document_No,Document_Date,From_Date,To_Date,IN_Active,Posted,InActive_Date,Posted_Date from TSPL_ITEM_CAPACITY_LIMIT_HEAD where 2=2"

            Select Case NavType
                Case NavigatorType.First
                    strQry += " and TSPL_ITEM_CAPACITY_LIMIT_HEAD.Document_No = (select MIN(Document_No) from TSPL_ITEM_CAPACITY_LIMIT_HEAD where 1=1 " + Whrcls + "  )"
                Case NavigatorType.Last
                    strQry += " and TSPL_ITEM_CAPACITY_LIMIT_HEAD.Document_No = (select Max(Document_No) from TSPL_ITEM_CAPACITY_LIMIT_HEAD where 1=1 " + Whrcls + "  )"
                Case NavigatorType.Next
                    strQry += " and TSPL_ITEM_CAPACITY_LIMIT_HEAD.Document_No = (select Min(Document_No) from TSPL_ITEM_CAPACITY_LIMIT_HEAD where Document_No>'" + clsCommon.myCstr(strDocNo) + "' " + Whrcls + "   )"
                Case NavigatorType.Previous
                    strQry += " and TSPL_ITEM_CAPACITY_LIMIT_HEAD.Document_No = (select Max(Document_No) from TSPL_ITEM_CAPACITY_LIMIT_HEAD where Document_No<'" + clsCommon.myCstr(strDocNo) + "' " + Whrcls + "  )"
                Case NavigatorType.Current
                    strQry += " and TSPL_ITEM_CAPACITY_LIMIT_HEAD.Document_No = '" + clsCommon.myCstr(strDocNo) + "'  " + Whrcls + " "
            End Select

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(strQry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then

                obj = New clsItemCapacityLimitHead()
                obj.Document_No = clsCommon.myCstr(dt.Rows(0)("Document_No"))
                obj.Document_Date = clsCommon.GetPrintDate(dt.Rows(0)("Document_Date"), "dd/MMM/yyyy")
                obj.From_Date = clsCommon.GetPrintDate(dt.Rows(0)("From_Date"), "dd/MMM/yyyy")
                If dt.Rows(0)("To_Date") IsNot DBNull.Value Then
                    obj.To_Date = clsCommon.myCDate(dt.Rows(0)("To_Date"))
                End If
                If dt.Rows(0)("InActive_Date") IsNot DBNull.Value Then
                    obj.InActive_date = clsCommon.myCDate(dt.Rows(0)("InActive_Date"))
                End If
                obj.IN_Active = clsCommon.myCBool(IIf(clsCommon.myCdbl(dt.Rows(0)("IN_Active")) = 1, True, False))
                obj.Posted = IIf(clsCommon.myCDecimal(dt.Rows(0)("Posted")) = 1, ERPTransactionStatus.Approved, ERPTransactionStatus.Pending)
                If dt.Rows(0)("Posted_Date") IsNot DBNull.Value Then
                    obj.Posted_Date = clsCommon.myCDate(dt.Rows(0)("Posted_Date"))
                End If
                obj.Arr = clsItemCapacityLimitDetail.GetData(obj.Document_No, trans)


            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return obj
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
            Dim obj As clsItemCapacityLimitHead = clsItemCapacityLimitHead.GetData(strDocNo, NavigatorType.Current, trans)
            'clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleMCCMilkProcurement, clsUserMgtCode.MilkShiftUploader, obj.MCC_Code, obj.Document_Date, trans)

            If (obj Is Nothing OrElse clsCommon.myLen(obj.Document_No) <= 0) Then
                Throw New Exception("Code : " + strDocNo + " not found to Post")
            End If
            If (obj.Posted = ERPTransactionStatus.Approved) Then
                Throw New Exception("Already Posted on :" + obj.Posted_Date)
            End If

            UnitConvertion(obj, trans)

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Posted", 1)
            clsCommon.AddColumnsForChange(coll, "Posted_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Posted_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))
            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ITEM_CAPACITY_LIMIT_HEAD", OMInsertOrUpdate.Update, "Document_No='" + clsCommon.myCstr(obj.Document_No) + "'", trans)
            ' clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strCode, "TSPL_ITEM_CAPACITY_LIMIT_HEAD", "Document_No", trans)

        Catch ex As Exception

            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Shared Sub UnitConvertion(ByVal objPP As clsItemCapacityLimitHead, ByVal trans As SqlTransaction)
        Dim Arr As New List(Of clsItemCapacityLimitDetail)
        Dim ObjAllUOM As New List(Of clsItemCapacityLimitDetailAllUOM)
        Dim dt As DataTable = Nothing
        Dim qry As String = Nothing
        Dim UOM_CF As Double = 0
        For Each objPD As clsItemCapacityLimitDetail In objPP.Arr
            UOM_CF = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Conversion_Factor from TSPL_ITEM_UOM_DETAIL where Item_Code='" + objPD.Item_Code + "' and UOM_Code ='" + objPD.UOM + "'", trans))
            qry = "select UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where Item_Code='" + objPD.Item_Code + "' and UOM_Code not in ('" + objPD.UOM + "')"
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    Dim objtr As New clsItemCapacityLimitDetailAllUOM()
                    objtr.Document_No = objPP.Document_No
                    objtr.REF_PK_ID = objPD.PK_ID
                    objtr.Item_Code = objPD.Item_Code
                    objtr.UOM = clsCommon.myCstr(dr("UOM_Code"))
                    objtr.Qty = (objPD.Qty * UOM_CF) / clsCommon.myCdbl(dr("Conversion_Factor"))
                    ObjAllUOM.Add(objtr)
                Next
                Dim objtr1 As New clsItemCapacityLimitDetailAllUOM()
                objtr1.Document_No = objPP.Document_No
                objtr1.REF_PK_ID = objPD.PK_ID
                objtr1.Item_Code = objPD.Item_Code
                objtr1.UOM = objPD.UOM
                objtr1.Qty = objPD.Qty
                ObjAllUOM.Add(objtr1)
            End If


        Next
        clsItemCapacityLimitDetailAllUOM.SaveData(objPP.Document_No, ObjAllUOM, trans)
        clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, objPP.Document_No, "TSPL_ITEM_CAPACITY_LIMIT_HEAD", "Document_No", "TSPL_ITEM_CAPACITY_LIMIT_DETAIL", "Document_No", "TSPL_ITEM_CAPACITY_LIMIT_DETAIL_ALL_UOM", "Document_No", trans)

    End Sub
    Public Shared Function ReverseAndUnpost(ByVal strDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            ReverseAndUnpost(strDocNo, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function ReverseAndUnpost(ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim obj As clsItemCapacityLimitHead = clsItemCapacityLimitHead.GetData(strCode, NavigatorType.Current, trans)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.Document_No) <= 0) Then
                Throw New Exception("Document No not found to Post")
            End If
            If Not (obj.Posted = ERPTransactionStatus.Approved) Then
                Throw New Exception("Transaction status should be posted.")
            End If
            Dim qry As String
            If obj.Posted = 1 Then
                qry = "update TSPL_ITEM_CAPACITY_LIMIT_HEAD set Posted=0,Posted_Date=null,Posted_By=null where Document_No='" + strCode + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
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
        Dim obj As New clsItemCapacityLimitHead()
        Try
            isSaved = False

            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Code not found to Delete")
            End If

            Dim isPosted As Integer = 0
            isPosted = clsDBFuncationality.getSingleValue("SELECT Count(*) FROM TSPL_ITEM_CAPACITY_LIMIT_HEAD where Document_No = '" & strCode & "' and Posted=1", trans)
            If (isPosted = 1) Then
                Throw New Exception("Already Posted on :" + obj.Posted_Date)
            End If
            'clsCommonFunctionality.SaveDeletedData(objCommonVar.CurrentUserCode, strCode, "TSPL_ITEM_CAPACITY_LIMIT_HEAD", "Document_No", "TSPL_ITEM_CAPACITY_LIMIT_DETAIL", "Document_No", trans)
            'clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strCode, "TSPL_ITEM_CAPACITY_LIMIT_HEAD", "Document_No", "TSPL_ITEM_CAPACITY_LIMIT_DETAIL", "Document_No", trans)

            Dim qry As String

            qry = "DELETE FROM TSPL_ITEM_CAPACITY_LIMIT_Detail WHERE Document_No='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_ITEM_CAPACITY_LIMIT_HEAD where Document_No ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
        Catch ex As Exception

            Throw New Exception(ex.Message.ToString())
        End Try
        Return isSaved
    End Function

End Class
Public Class clsItemCapacityLimitDetail
#Region "Variable"
    Public Document_No As String = ""
    Public PK_ID As Integer = 0
    Public Item_Code As String = ""
    Public UOM As String = ""
    Public Qty As Decimal = 0

#End Region
    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsItemCapacityLimitDetail), ByVal trans As SqlTransaction) As Boolean
        Try


            If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
                For Each obj As clsItemCapacityLimitDetail In Arr
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Document_No", strDocNo)
                    clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code)
                    clsCommon.AddColumnsForChange(coll, "UOM", obj.UOM)
                    clsCommon.AddColumnsForChange(coll, "Qty", obj.Qty)

                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ITEM_CAPACITY_LIMIT_DETAIL", OMInsertOrUpdate.Insert, "", trans)

                Next
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function GetData(ByVal strDocNo As String, ByVal trans As SqlTransaction) As List(Of clsItemCapacityLimitDetail)
        Dim arr As List(Of clsItemCapacityLimitDetail) = Nothing

        Try
            Dim dt As DataTable
            Dim strQry As String = "select PK_ID,Document_No,Item_Code,UOM,Qty from TSPL_ITEM_CAPACITY_LIMIT_DETAIL where Document_No='" & strDocNo & "'"
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(strQry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                arr = New List(Of clsItemCapacityLimitDetail)
                Dim objTr As clsItemCapacityLimitDetail
                For Each dr As DataRow In dt.Rows
                    objTr = New clsItemCapacityLimitDetail
                    objTr.PK_ID = clsCommon.myCdbl(dr("PK_ID"))
                    objTr.Document_No = clsCommon.myCstr(dr("Document_No"))
                    objTr.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                    objTr.UOM = clsCommon.myCstr(dr("UOM"))
                    objTr.Qty = clsCommon.myCDecimal(dr("Qty"))
                    arr.Add(objTr)
                Next
            End If



        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

        Return arr
    End Function
End Class
Public Class clsItemCapacityLimitDetailAllUOM
#Region "Variable"
    Public Document_No As String = ""
    Public REF_PK_ID As Integer = 0
    Public Item_Code As String = ""
    Public UOM As String = ""
    Public Qty As Decimal = 0

#End Region
    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsItemCapacityLimitDetailAllUOM), ByVal trans As SqlTransaction) As Boolean
        Try


            If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
                For Each obj As clsItemCapacityLimitDetailAllUOM In Arr
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Document_No", strDocNo)
                    clsCommon.AddColumnsForChange(coll, "REF_PK_ID", obj.REF_PK_ID)
                    clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code)
                    clsCommon.AddColumnsForChange(coll, "UOM", obj.UOM)
                    clsCommon.AddColumnsForChange(coll, "Qty", obj.Qty)

                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ITEM_CAPACITY_LIMIT_DETAIL_ALL_UOM", OMInsertOrUpdate.Insert, "", trans)

                Next
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
End Class
