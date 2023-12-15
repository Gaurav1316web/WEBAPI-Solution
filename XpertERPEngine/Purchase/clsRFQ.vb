Imports common
Imports System.Data.SqlClient
Public Class clsRFQ
#Region "Variables"
    Public RFQ_No As String = Nothing
    Public RFQ_Date As String = Nothing
    Public Post_Date As String = Nothing
    Public Requisition_Id As String = Nothing
    Public Is_Post As ERPTransactionStatus = ERPTransactionStatus.Pending
    Public Post_By As String = Nothing
    Public Modify_By As String = Nothing
    Public Modify_Date As String = Nothing
    Public Created_By As String = Nothing
    Public Created_Date As String = Nothing
    Public Arr As List(Of clsRFQDetails) = Nothing
#End Region

    Public Function SaveData(ByVal obj As clsRFQ, ByVal isNewEntry As Boolean) As Boolean
        Dim isSaved As Boolean = True
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim qry As String = "delete from TSPL_RFQ_DETAIL_ITEM where RFQ_No='" + obj.RFQ_No + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_RFQ_DETAIL where RFQ_No='" + obj.RFQ_No + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Dim strDocNo As String = ""

            If isNewEntry Then
                obj.RFQ_No = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(obj.RFQ_Date), clsDocType.RFQ, "", "")
            End If
            If (clsCommon.myLen(obj.RFQ_No) <= 0) Then
                Throw New Exception("Error in Document Code Generation")
            End If

            Dim coll As New Hashtable()

            clsCommon.AddColumnsForChange(coll, "comp_code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "RFQ_Date", clsCommon.GetPrintDate(obj.RFQ_Date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Requisition_Id", obj.Requisition_Id)
            'If (Is_Post = ERPTransactionStatus.Approved) Then
            clsCommon.AddColumnsForChange(coll, "Is_Post", clsCommon.myCdbl(obj.Is_Post))
            clsCommon.AddColumnsForChange(coll, "Post_By", objCommonVar.CurrentUserCode)
            '    clsCommon.AddColumnsForChange(coll, "Post_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy"))
            'End If
            clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
            If isNewEntry Then

                clsCommon.AddColumnsForChange(coll, "RFQ_No", obj.RFQ_No)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_RFQ_HEAD", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_RFQ_HEAD", OMInsertOrUpdate.Update, "TSPL_RFQ_HEAD.RFQ_No='" + obj.RFQ_No + "'", trans)
            End If


            isSaved = isSaved AndAlso clsRFQDetails.SaveData(obj.RFQ_No, Arr, trans)

            trans.Commit()

            'If Not isNewEntry Then
            '    clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, clsCommon.myCstr(obj.RFQ_No), "TSPL_RFQ_HEAD", "RFQ_No", "TSPL_RFQ_DETAIL_ITEM", "RFQ_No", trans)
            'End If


        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function

    Public Shared Function PostData(ByVal strDocNo As String, ByVal isCheckForPosted As Boolean) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("No data found to Post")
            End If
            Dim strPostDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy")

            Dim obj As clsRFQ = clsRFQ.GetData(strDocNo, NavigatorType.Current, trans)

            If (obj Is Nothing OrElse clsCommon.myLen(obj.RFQ_No) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            If (isCheckForPosted AndAlso obj.Is_Post = 1) Then
                Throw New Exception("Already Post on :" + obj.Post_Date)
            End If

            Dim qry As String = "Update TSPL_RFQ_HEAD set Is_Post=1, Post_Date='" + strPostDate + "',Posted_By='" + objCommonVar.CurrentUserCode + "', Modify_Date='" + strPostDate + "',Modify_By='" + objCommonVar.CurrentUserCode + "' where RFQ_No='" + strDocNo + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            trans.Commit()

        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim isSaved As Boolean = False
        If (clsCommon.myLen(strCode) <= 0) Then
            Throw New Exception("RFQ No not found to Delete")
        End If
        Dim obj As clsRFQ = clsRFQ.GetData(strCode, NavigatorType.Current)
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.RFQ_No) > 0) Then
            Try
                If (obj.Is_Post = 1) Then
                    Throw New Exception("Already Posted on :" + obj.Post_Date)
                End If
                Dim qry As String = "delete from TSPL_RFQ_DETAIL_ITEM where RFQ_No='" + strCode + "'"
                isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)

                qry = "delete from TSPL_RFQ_DETAIL where RFQ_No='" + strCode + "'"
                isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)

                qry = "delete from TSPL_RFQ_HEAD where RFQ_No='" + strCode + "'"
                isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

                If (isSaved) Then
                    trans.Commit()
                Else
                    trans.Rollback()
                End If
            Catch ex As Exception
                trans.Rollback()
                Throw New Exception(ex.Message)
            End Try
        End If
        Return isSaved
    End Function

    Public Shared Function GetData(ByVal strDocumentNo As String, ByVal NavType As NavigatorType) As clsRFQ
        Return GetData(strDocumentNo, NavType, Nothing)
    End Function

    Public Shared Function GetData(ByVal strPONo As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsRFQ
        Dim obj As clsRFQ = Nothing
        Dim qry As String = "SELECT TSPL_RFQ_HEAD.RFQ_No,TSPL_RFQ_HEAD.Requisition_Id ,TSPL_RFQ_HEAD.RFQ_Date,TSPL_RFQ_HEAD.Is_Post,TSPL_RFQ_HEAD.Post_By,TSPL_RFQ_HEAD.Post_Date,TSPL_RFQ_HEAD.Modify_By,TSPL_RFQ_HEAD.Modify_Date,TSPL_RFQ_HEAD.Created_By,TSPL_RFQ_HEAD.Created_Date FROM TSPL_RFQ_HEAD where 2=2"
        Dim whrClas As String = ""
     
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_RFQ_HEAD.RFQ_No = (select MIN(RFQ_No) from TSPL_RFQ_HEAD where 1=1 " + whrClas + ")"
            Case NavigatorType.Last
                qry += " and TSPL_RFQ_HEAD.RFQ_No = (select Max(RFQ_No) from TSPL_RFQ_HEAD where 1=1 " + whrClas + ")"
            Case NavigatorType.Next
                qry += " and TSPL_RFQ_HEAD.RFQ_No = (select Min(RFQ_No) from TSPL_RFQ_HEAD where RFQ_No>'" + strPONo + "' " + whrClas + ")"
            Case NavigatorType.Previous
                qry += " and TSPL_RFQ_HEAD.RFQ_No = (select Max(RFQ_No) from TSPL_RFQ_HEAD where RFQ_No<'" + strPONo + "' " + whrClas + ")"
            Case NavigatorType.Current
                qry += " and TSPL_RFQ_HEAD.RFQ_No = '" + strPONo + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsRFQ()
            obj.RFQ_No = clsCommon.myCstr(dt.Rows(0)("RFQ_No"))
            obj.RFQ_Date = clsCommon.myCstr(dt.Rows(0)("RFQ_Date"))
            obj.Requisition_Id = clsCommon.myCstr(dt.Rows(0)("Requisition_Id"))
            obj.Is_Post = clsCommon.myCdbl(dt.Rows(0)("Is_Post"))
            obj.Post_By = clsCommon.myCstr(dt.Rows(0)("Post_By"))
            obj.Post_Date = clsCommon.myCstr(dt.Rows(0)("Post_Date"))
            obj.Modify_By = clsCommon.myCstr(dt.Rows(0)("Modify_By"))
            obj.Modify_Date = clsCommon.myCstr(dt.Rows(0)("Modify_Date"))
            obj.Created_By = clsCommon.myCstr(dt.Rows(0)("Created_By"))
            obj.Created_Date = clsCommon.myCstr(dt.Rows(0)("Created_Date"))

            qry = "SELECT  TSPL_RFQ_DETAIL.RFQ_No,TSPL_RFQ_DETAIL.Vendor_Code,TSPL_VENDOR_MASTER.Vendor_Name from TSPL_RFQ_DETAIL left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_RFQ_DETAIL.Vendor_Code  where TSPL_RFQ_DETAIL.RFQ_No='" + obj.RFQ_No + "' "
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj.Arr = New List(Of clsRFQDetails)
                Dim objTr As clsRFQDetails
                For Each dr As DataRow In dt.Rows
                    objTr = New clsRFQDetails
                    objTr.RFQ_No = clsCommon.myCstr(dr("RFQ_No"))
                    objTr.Vendor_Code = clsCommon.myCstr(dr("Vendor_Code"))
                    objTr.Vendor_Name = clsCommon.myCstr(dr("Vendor_Name"))
                    objTr.arrItem = clsRFQDetailsItems.GetData(objTr.RFQ_No, objTr.Vendor_Code, trans)
                    obj.Arr.Add(objTr)
                Next
            End If
        End If
        Return obj
    End Function

End Class

Public Class clsRFQDetails
#Region "Variables"
    Public RFQ_No As String = Nothing
    Public Vendor_Code As String = Nothing
    Public Vendor_Name As String = Nothing
    Public arrItem As ArrayList = Nothing
#End Region

    Public Shared Function SaveData(ByVal RFQ_No As String, ByVal Arr As List(Of clsRFQDetails), ByVal trans As SqlTransaction) As Boolean

        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsRFQDetails In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "RFQ_NO", RFQ_No)
                clsCommon.AddColumnsForChange(coll, "Vendor_Code", obj.Vendor_Code)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_RFQ_DETAIL", OMInsertOrUpdate.Insert, "", trans)
                clsRFQDetailsItems.SaveData(RFQ_No, obj.Vendor_Code, obj.arrItem, trans)
            Next
        End If
        Return True
    End Function
End Class

''Against Ticke No BHA/27/06/18-000097 by balwinder on 27/06/2018
Public Class clsRFQDetailsItems
#Region "Variables"
    Public RFQ_No As String = Nothing
    Public Vendor_Code As String = Nothing
    Public Item_Code As String = Nothing

#End Region

    Public Shared Function SaveData(ByVal RFQ_No As String, ByVal VendorCode As String, ByVal Arr As ArrayList, ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each strICode As String In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "RFQ_NO", RFQ_No)
                clsCommon.AddColumnsForChange(coll, "Vendor_Code", VendorCode)
                clsCommon.AddColumnsForChange(coll, "Item_Code", strICode)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_RFQ_DETAIL_ITEM", OMInsertOrUpdate.Insert, "", trans)
                ''CreateEmailContent(Arr, trans)
            Next
        End If
        Return True
    End Function
    

    Public Shared Function GetData(ByVal strRFQNo As String, ByVal strVCode As String, ByVal trans As SqlTransaction) As ArrayList
        Dim arr As ArrayList = Nothing
        Dim qry As String = "SELECT Item_Code from TSPL_RFQ_DETAIL_ITEM where TSPL_RFQ_DETAIL_ITEM.RFQ_No='" + strRFQNo + "' and Vendor_Code='" + strVCode + "' "
        Dim dtTR As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dtTR IsNot Nothing AndAlso dtTR.Rows.Count > 0) Then
            arr = New ArrayList
            For Each drTR As DataRow In dtTR.Rows
                arr.Add(clsCommon.myCstr(drTR("Item_Code")))
            Next
        End If
        dtTR = Nothing
        Return arr
    End Function

End Class



