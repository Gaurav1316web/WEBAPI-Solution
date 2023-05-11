Imports common
Imports System.Data.SqlClient
Imports System.Windows.Forms
Imports Telerik.WinControls
Imports System.Runtime.Serialization.Formatters.Binary
Imports System.IO
Public Class clsItemProjectionHead
#Region "Variables"
    Public Projection_Code As String = Nothing
    Public Projection_Date As Date = Nothing
    Public Post_Status As ERPTransactionStatus = ERPTransactionStatus.Pending
    Public Arr As List(Of clsItemProjectionDetails) = Nothing
#End Region
    Public Function SaveData(ByVal obj As clsItemProjectionHead, ByVal isNewEntry As Boolean) As Boolean
        Dim isSaved As Boolean = True
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

    Public Function SaveData(ByVal obj As clsItemProjectionHead, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Try

            Dim qry As String = "Select 1 from TSPL_ITEM_PROJECTION_HEAD Where Post_Status=1 and Projection_Code='" + obj.Projection_Code + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                dt = Nothing
                Throw New Exception("This Projection Code-" + obj.Projection_Code + " is already posted.")
            End If

            qry = "delete from TSPL_ITEM_PROJECTION_DETAILS where Projection_Code='" + obj.Projection_Code + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            Dim strDocNo As String = ""
            If isNewEntry Then
                obj.Projection_Code = clsERPFuncationality.GetNextCode(trans, clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"), clsDocType.ItemProjection, "", "")
            End If
            If (clsCommon.myLen(obj.Projection_Code) <= 0) Then
                Throw New Exception("Error in Projection Code Generation")
            End If

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Projection_Date", clsCommon.GetPrintDate(obj.Projection_Date, "dd/MMM/yyyy hh:mm tt"))
            'clsCommon.AddColumnsForChange(coll, "Post_Status", obj.Post_Status)
           
           
            clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))



            
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Projection_Code", obj.Projection_Code)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ITEM_PROJECTION_HEAD", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ITEM_PROJECTION_HEAD", OMInsertOrUpdate.Update, "TSPL_ITEM_PROJECTION_HEAD.Projection_Code='" + obj.Projection_Code + "'", trans)
            End If
            isSaved = isSaved AndAlso clsItemProjectionDetails.SaveData(obj.Projection_Code, obj.Arr, trans)
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function

    Public Shared Function PostData(ByVal strDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim qry As String = "Select 1  from TSPL_ITEM_PROJECTION_HEAD Where Post_Status=1 and Projection_Code='" + strDocNo + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                dt = Nothing
                Throw New Exception("This Projection Code-" + strDocNo + " is already posted.")
            End If

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Posted_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Posted_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Post_Status", 1)
            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ITEM_PROJECTION_HEAD", OMInsertOrUpdate.Update, "Projection_Code='" + strDocNo + "'", trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function deleteData(ByVal strDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim qry As String = "Select 1  from TSPL_ITEM_PROJECTION_HEAD Where Post_Status=1 and Projection_Code='" + strDocNo + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                dt = Nothing
                Throw New Exception("This Projection Code-" + strDocNo + " is already posted.")
            End If
            qry = "delete from TSPL_ITEM_PROJECTION_DETAILS where Projection_Code='" & strDocNo & "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "delete from TSPL_ITEM_PROJECTION_HEAD where Projection_Code='" & strDocNo & "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal navtype As NavigatorType, Optional trans As SqlTransaction = Nothing) As clsItemProjectionHead
        Dim obj As New clsItemProjectionHead
        Try
            Dim whrCls As String = String.Empty
            Dim qst As String = "select * from TSPL_ITEM_PROJECTION_HEAD where 2=2 "
            qst = qst & whrCls
            Select Case navtype
                Case NavigatorType.Current
                    qst += " and TSPL_ITEM_PROJECTION_HEAD.Projection_Code in ('" + strCode + "') "
                Case NavigatorType.Next
                    qst += " and TSPL_ITEM_PROJECTION_HEAD.Projection_Code in (select min(Projection_Code ) from TSPL_ITEM_PROJECTION_HEAD where Projection_Code  >'" + strCode + "'   " & whrCls & ")"
                Case NavigatorType.First
                    qst += " and TSPL_ITEM_PROJECTION_HEAD.Projection_Code in (select MIN(Projection_Code ) from TSPL_ITEM_PROJECTION_HEAD where 1=1  " & whrCls & ")"
                Case NavigatorType.Last
                    qst += " and TSPL_ITEM_PROJECTION_HEAD.Projection_Code in (select Max(Projection_Code ) from TSPL_ITEM_PROJECTION_HEAD where 1=1  " & whrCls & ")"
                Case NavigatorType.Previous
                    qst += " and TSPL_ITEM_PROJECTION_HEAD.Projection_Code in (select Max(Projection_Code ) from TSPL_ITEM_PROJECTION_HEAD where Projection_Code  <'" + strCode + "'   " & whrCls & ")"
            End Select
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qst, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                obj.Projection_Code = clsCommon.myCstr(dt.Rows(0)("Projection_Code"))
                obj.Projection_Date = clsCommon.myCDate(dt.Rows(0)("Projection_Date"))
                obj.Post_Status = clsCommon.myCdbl(dt.Rows(0)("Post_Status"))

                qst = "select * from TSPL_ITEM_PROJECTION_DETAILS where Projection_Code='" & obj.Projection_Code & "'"
                dt = New DataTable()
                dt = clsDBFuncationality.GetDataTable(qst, trans)
                If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                    obj.Arr = New List(Of clsItemProjectionDetails)
                    Dim objTr As clsItemProjectionDetails
                    For Each dr As DataRow In dt.Rows
                        objTr = New clsItemProjectionDetails
                        objTr.Item_Code = clsCommon.myCdbl(dr("Item_Code"))
                        objTr.Item_Desc = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Item_Desc from TSPL_ITEM_MASTER where item_code='" & objTr.Item_Code & "'", trans))
                        objTr.Item_Qty = clsCommon.myCdbl(dr("Item_Qty"))
                        'objTr.Doc_Date = clsCommon.myCDate(dr("Doc_Date"))
                        objTr.Tollence = clsCommon.myCdbl(dr("Tollence"))
                        objTr.Actual_Projection = clsCommon.myCdbl(dr("Actual_Projection"))


                    Next
                End If

            End If


        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return obj
    End Function

End Class
Public Class clsItemProjectionDetails
#Region "Variables"
    Public Projection_Code As String = Nothing
    Public Doc_Date As String = Nothing
    Public Item_Code As String = Nothing
    Public Item_Desc As String = Nothing
    Public Item_Qty As Double = 0
    Public Is_Item_Free As Integer = 0
    Public Defalt_Uom As String = Nothing
    Public Average As Double = 0
    Public Projection_Average As Double = 0
    Public SNO As Integer = 0
    Public Actual_Projection As Double = 0
    Public Tollence As Double = 0
    Public UOM As String = Nothing
#End Region
    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsItemProjectionDetails), ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsItemProjectionDetails In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Projection_Code", strDocNo)
                'clsCommon.AddColumnsForChange(coll, "SNO", obj.SNO)
                clsCommon.AddColumnsForChange(coll, "Doc_Date", obj.Doc_Date)
                clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code)
                clsCommon.AddColumnsForChange(coll, "Item_Qty", obj.Item_Qty)
                clsCommon.AddColumnsForChange(coll, "Is_Item_Free", obj.Is_Item_Free)
                ' clsCommon.AddColumnsForChange(coll, "Defalt_Uom", obj.Defalt_Uom)
                clsCommon.AddColumnsForChange(coll, "Average", obj.Average)
                clsCommon.AddColumnsForChange(coll, "Projection_Average", obj.Projection_Average)
                clsCommon.AddColumnsForChange(coll, "Tollence", obj.Tollence)
                clsCommon.AddColumnsForChange(coll, "Actual_Projection", obj.Actual_Projection)
                clsCommon.AddColumnsForChange(coll, "UOM", obj.UOM)
                clsCommon.AddColumnsForChange(coll, "Item_Desc", obj.Item_Desc)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ITEM_PROJECTION_DETAILS", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function
End Class
