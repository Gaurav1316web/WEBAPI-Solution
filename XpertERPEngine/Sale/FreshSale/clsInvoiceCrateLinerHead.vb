Imports common
Imports System.Data.SqlClient
Public Class clsInvoiceCrateLinerHead

#Region "Variable"

    Public DOCUMENT_NO As String = Nothing
    Public DOCUMENT_DATE As Date = Nothing
    Public LOCATION_CODE As String = Nothing
    Public CREATED_BY As String = Nothing
    Public CREATED_DATE As Date = Nothing
    Public MODIFY_BY As String = Nothing
    Public MODIFY_DATE As Date = Nothing
    Public POSTED As Integer
    Public POSTED_BY As String = Nothing
    Public POSTED_DATE As Date = Nothing
    Public Arr As List(Of clsInvoiceCrateLinerDetail) = Nothing
    Dim qry As String

#End Region
    Public Function SaveData(ByVal obj As clsInvoiceCrateLinerHead, ByVal isNewEntry As Boolean) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            SaveData(obj, isNewEntry, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Function SaveData(ByVal obj As clsInvoiceCrateLinerHead, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        qry = "delete from TSPL_INVOICE_CRATE_LINER_DETAIL where Document_No='" + obj.Document_No + "'"
        isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
        Dim strDocNo As String = ""
        If isNewEntry Then
            obj.DOCUMENT_NO = clsERPFuncationality.GetNextCode(trans, obj.DOCUMENT_DATE, clsDocType.FrmInvoiceCrateLinerDetail, "", "")
        End If

        If (clsCommon.myLen(obj.Document_No) <= 0) Then
            Throw New Exception("Error in Document Code Generation")
        End If
        Dim coll As New Hashtable()

        clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(obj.DOCUMENT_DATE, "dd/MMM/yyyy"))
        clsCommon.AddColumnsForChange(coll, "LOCATION_CODE", objCommonVar.CurrLocationCode)
        clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
        clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))

        If isNewEntry Then
            clsCommon.AddColumnsForChange(coll, "Document_No", obj.Document_No)
            clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
            isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_INVOICE_CRATE_LINER_HEAD", OMInsertOrUpdate.Insert, "", trans)
        Else
            isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_INVOICE_CRATE_LINER_HEAD", OMInsertOrUpdate.Update, "TSPL_INVOICE_CRATE_LINER_HEAD.Document_No='" + obj.Document_No + "'", trans)
        End If

        isSaved = isSaved AndAlso clsInvoiceCrateLinerDetail.SaveData(obj.Document_No, Arr, trans)
        Return isSaved

    End Function
    Public Shared Function GetData(ByVal strDocumentNo As String, ByVal NavType As NavigatorType) As clsInvoiceCrateLinerHead
        Return GetData(strDocumentNo, NavType, Nothing)
    End Function
    Public Shared Function GetData(ByVal strDocNo As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsInvoiceCrateLinerHead
        Dim obj As clsInvoiceCrateLinerHead = Nothing
        Dim qry = "SELECT DOCUMENT_NO,DOCUMENT_DATE,POSTED FROM TSPL_INVOICE_CRATE_LINER_HEAD where 2=2 "
        Dim whrCls As String = ""
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            whrCls = " AND Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
        End If
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_INVOICE_CRATE_LINER_HEAD.Document_No = (select MIN(Document_No) from TSPL_INVOICE_CRATE_LINER_HEAD WHERE 1=1 " + whrCls + ")"
            Case NavigatorType.Last
                qry += " and TSPL_INVOICE_CRATE_LINER_HEAD.Document_No = (select Max(Document_No) from TSPL_INVOICE_CRATE_LINER_HEAD WHERE 1=1 " + whrCls + ")"
            Case NavigatorType.Current
                qry += " and TSPL_INVOICE_CRATE_LINER_HEAD.Document_No = '" + strDocNo + "'"
            Case NavigatorType.Next
                qry += " and TSPL_INVOICE_CRATE_LINER_HEAD.Document_No = (select Min(Document_No) from TSPL_INVOICE_CRATE_LINER_HEAD where Document_No>'" + strDocNo + "' " + whrCls + ")"
            Case NavigatorType.Previous
                qry += " and TSPL_INVOICE_CRATE_LINER_HEAD.Document_No = (select Max(Document_No) from TSPL_INVOICE_CRATE_LINER_HEAD where Document_No<'" + strDocNo + "' " + whrCls + ")"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsInvoiceCrateLinerHead()
            obj.Document_No = clsCommon.myCstr(dt.Rows(0)("Document_No"))
            obj.Document_Date = clsCommon.myCDate(dt.Rows(0)("Document_Date"))
            obj.POSTED = clsCommon.myCdbl(dt.Rows(0)("POSTED"))
            
            qry = "SELECT TSPL_INVOICE_CRATE_LINER_DETAIL.LINE_NO,TSPL_INVOICE_CRATE_LINER_DETAIL.INVOICE_NO,TSPL_INVOICE_CRATE_LINER_DETAIL.INVOICE_DATE,TSPL_INVOICE_CRATE_LINER_DETAIL.CRATE,TSPL_INVOICE_CRATE_LINER_DETAIL.LINER " & _
                   ",TSPL_CUSTOMER_MASTER.CUSTOMER_NAME " & _
                    ",TSPL_LOCATION_MASTER.LOCATION_DESC " & _
               " FROM TSPL_INVOICE_CRATE_LINER_DETAIL " & _
            " left join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code=TSPL_INVOICE_CRATE_LINER_DETAIL.INVOICE_NO " & _
                " left join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SALE_INVOICE_HEAD.Customer_Code  " & _
                " left join TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code=TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location " & _
                " where Document_No='" & obj.DOCUMENT_NO & "' ORDER BY LINE_NO"
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj.Arr = New List(Of clsInvoiceCrateLinerDetail)
                Dim objTr As clsInvoiceCrateLinerDetail
                For Each dr As DataRow In dt.Rows
                    objTr = New clsInvoiceCrateLinerDetail
                    objTr.LINE_NO = clsCommon.myCdbl(dr("LINE_NO"))
                    objTr.CUSTOMER = clsCommon.myCstr(dr("CUSTOMER_NAME"))
                    objTr.LOCATION = clsCommon.myCstr(dr("LOCATION_DESC"))
                    objTr.DOCUMENT_NO = clsCommon.myCstr(obj.DOCUMENT_NO)
                    objTr.INVOICE_DATE = clsCommon.myCDate(dr("INVOICE_DATE"))
                    objTr.INVOICE_NO = clsCommon.myCstr(dr("INVOICE_NO"))
                    objTr.CRATE = clsCommon.myCdbl(dr("CRATE"))
                    objTr.LINER = clsCommon.myCdbl(dr("LINER"))
                    obj.Arr.Add(objTr)
                Next
            End If
        End If
        Return obj
    End Function
    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim isSaved As Boolean = False
        If (clsCommon.myLen(strCode) <= 0) Then
            Throw New Exception("Document No not found to Delete")
        End If
        Dim obj As clsInvoiceCrateLinerHead = clsInvoiceCrateLinerHead.GetData(strCode, NavigatorType.Current)
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Document_No) > 0) Then
            Try

                Dim qry = "delete from TSPL_INVOICE_CRATE_LINER_DETAIL where Document_No='" + strCode + "'"
                isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)

                qry = "delete from TSPL_INVOICE_CRATE_LINER_HEAD where Document_No='" + strCode + "'"
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
            Dim isSaved As Boolean = True
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Document No not found to Post")
            End If
            'Dim obj As clsInvoiceCrateLinerHead = clsInvoiceCrateLinerHead.GetData(strDocNo, NavigatorType.Current, trans)

            'If (obj Is Nothing OrElse clsCommon.myLen(obj.DOCUMENT_NO) <= 0) Then
            '    Throw New Exception("No Data found to Post")
            'End If

            Dim qry = "Update TSPL_INVOICE_CRATE_LINER_HEAD set POSTED=1, " & _
            "POSTED_DATE='" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy") + "',POSTED_BY='" + objCommonVar.CurrentUserCode + "'" & _
            ",MODIFY_BY='" + objCommonVar.CurrentUserCode + "', " & _
            "MODIFY_DATE='" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy") + "' " & _
            " where Document_No='" + strDocNo + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "SELECT INVOICE_NO,CRATE,LINER FROM TSPL_INVOICE_CRATE_LINER_DETAIL where Document_No='" & strDocNo & "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                '' woring on post function in against ticket no. SWA/09/05/18-000019

                For Each dr As DataRow In dt.Rows
                    qry = "update TSPL_SD_SALE_INVOICE_HEAD set CrateQty='" + clsCommon.myCstr(dr("CRATE").ToString()) + "'"
                    qry += ",Liner='" + clsCommon.myCstr(dr("LINER").ToString()) + "'"
                    qry += " where DOCUMENT_CODE='" + clsCommon.myCstr(dr("INVOICE_NO").ToString()) + "'"
                    clsDBFuncationality.ExecuteNonQuery(qry, trans)

                    qry = "update TSPL_SD_SHIPMENT_HEAD set CrateQty='" + clsCommon.myCstr(dr("CRATE").ToString()) + "',Crate='" + clsCommon.myCstr(dr("CRATE").ToString()) + "'"
                    qry += ",Liner='" + clsCommon.myCstr(dr("LINER").ToString()) + "'"
                    qry += " where Sale_Invoice_No='" + clsCommon.myCstr(dr("INVOICE_NO").ToString()) + "'"
                    clsDBFuncationality.ExecuteNonQuery(qry, trans)
                Next
            End If
        Catch ex As Exception

            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
End Class
Public Class clsInvoiceCrateLinerDetail
#Region "Variable"
    Public LINE_NO As Integer = 0
    Public DOCUMENT_NO As String = Nothing
    Public CUSTOMER As String = Nothing
    Public LOCATION As String = Nothing
    Public INVOICE_NO As String = Nothing
    Public INVOICE_DATE As Date = Nothing
    Public CRATE As Integer = 0
    Public LINER As Integer = 0

#End Region

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsInvoiceCrateLinerDetail), ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsInvoiceCrateLinerDetail In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "LINE_NO", clsCommon.myCdbl(obj.LINE_NO))
                clsCommon.AddColumnsForChange(coll, "DOCUMENT_NO", clsCommon.myCstr(strDocNo))
                clsCommon.AddColumnsForChange(coll, "INVOICE_DATE", clsCommon.GetPrintDate(obj.INVOICE_DATE, "dd/MMM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "INVOICE_NO", obj.INVOICE_NO)
                clsCommon.AddColumnsForChange(coll, "CRATE", obj.CRATE)
                clsCommon.AddColumnsForChange(coll, "LINER", obj.LINER)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_INVOICE_CRATE_LINER_DETAIL", OMInsertOrUpdate.Insert, "", trans)
            Next

        End If
        Return True
    End Function

End Class
