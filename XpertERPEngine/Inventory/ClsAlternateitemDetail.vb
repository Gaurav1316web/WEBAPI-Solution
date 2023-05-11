Imports common
Imports System.Data.SqlClient

Public Class ClsAlternateitemDetail
#Region "Variables"


    Public ITEM_CODE As String = Nothing
    Public SUBSTITUTE_ITEM_CODE As String = Nothing
    Public DESCRIPTION As String = Nothing
    Public UNIT_CODE As String = Nothing
    Public QUANTITY As Double = Nothing
    Public PRIORITY As String = Nothing
    Public COMMENTS As String = Nothing
    Public Created_By As String = Nothing
    Public Created_Date As Date? = Nothing
    Public Modified_By As String = Nothing
    Public Modified_Date As Date? = Nothing
#End Region
#Region "Functions"

    Public Function SaveData(ByVal ItemCODE As String, ByVal ItemDesc As String, ByVal Arr As List(Of ClsAlternateitemDetail)) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()

        Dim isSaved As Boolean = True
        Try

            Dim qry As String = "delete from TSPL_MF_SUBSTITUTE_ITEMS where Item_code='" + ItemCODE + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            Dim strDocNo As String = ""
            For Each obj As ClsAlternateitemDetail In Arr
                Dim coll As New Hashtable()

                clsCommon.AddColumnsForChange(coll, "comp_code", objCommonVar.CurrentCompanyCode)
                clsCommon.AddColumnsForChange(coll, "ITEM_CODE", ItemCODE)
                clsCommon.AddColumnsForChange(coll, "SUBSTITUTE_ITEM_CODE", obj.SUBSTITUTE_ITEM_CODE)
                clsCommon.AddColumnsForChange(coll, "DESCRIPTION", obj.DESCRIPTION)
                clsCommon.AddColumnsForChange(coll, "UNIT_CODE", obj.UNIT_CODE)
                clsCommon.AddColumnsForChange(coll, "QUANTITY", obj.QUANTITY)
                clsCommon.AddColumnsForChange(coll, "PRIORITY", obj.PRIORITY)
                clsCommon.AddColumnsForChange(coll, "COMMENTS", obj.COMMENTS)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MF_SUBSTITUTE_ITEMS", OMInsertOrUpdate.Insert, "", trans)
            Next

            If isSaved Then
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


    Public Shared Function GetData(ByVal strCode As String) As List(Of ClsAlternateitemDetail)
        Dim obj As ClsAlternateitemDetail = Nothing
        Dim qry As String = "select  ITEM_CODE,SUBSTITUTE_ITEM_CODE,DESCRIPTION,QUANTITY,UNIT_CODE,PRIORITY,COMMENTS  from TSPL_MF_SUBSTITUTE_ITEMS WHERE ITEM_CODE = '" + strCode + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        dt = clsDBFuncationality.GetDataTable(qry)
        Dim Arr As New List(Of ClsAlternateitemDetail)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            Dim objTr As ClsAlternateitemDetail
            For Each dr As DataRow In dt.Rows
                objTr = New ClsAlternateitemDetail()
                objTr.ITEM_CODE = clsCommon.myCstr(dr("ITEM_CODE"))
                objTr.SUBSTITUTE_ITEM_CODE = clsCommon.myCstr(dr("SUBSTITUTE_ITEM_CODE"))
                objTr.DESCRIPTION = clsCommon.myCstr(dr("DESCRIPTION"))
                objTr.UNIT_CODE = clsCommon.myCstr(dr("UNIT_CODE"))
                objTr.QUANTITY = clsCommon.myCdbl(dr("QUANTITY"))
                objTr.PRIORITY = clsCommon.myCdbl(dr("PRIORITY"))
                objTr.COMMENTS = clsCommon.myCstr(dr("COMMENTS"))
                Arr.Add(objTr)
            Next
        End If
        Return Arr
    End Function
#End Region

End Class
