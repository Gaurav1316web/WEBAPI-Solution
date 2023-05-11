Imports common
Imports System.Data
Imports System.Data.SqlClient
Public Class clsAttachDocument


#Region "Variables"

    Public FormId As String
    Public TransactionId As String
    Public CODE As String
    Public SNo As Int16
    Public FileName As String
    Public FileData As Byte()
    Public COMMENTS As String

#End Region

    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim isSaved As Boolean
        Try
            isSaved = False
            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Code not found to Delete")
            End If
            Dim qry As String
            qry = " delete from TSPL_ATTACHMENTS where CODE='" + strCode + "' "
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry)
        Catch ex As Exception
            Throw New Exception(ex.Message.ToString())
        End Try
        Return isSaved
    End Function

    Public Shared Function GetData(ByVal strFormId As String, ByVal strTransactionId As String, ByVal trans As SqlTransaction) As DataTable

        Dim obj As clsAttachDocument = Nothing
        Dim qry As String = " select CODE,FormId,TransactionId,SNo,FileName, COMMENTS from TSPL_ATTACHMENTS where 1=1 "
        qry += " and FormId = '" + strFormId + "' and TransactionId = '" + strTransactionId + "' "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        'If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
        '    Dim dr As DataRow = dt.Rows(0)
        '    obj = New clsAttachDocument()
        '    obj.CODE = clsCommon.myCstr(dr("CODE"))
        '    obj.FileName = clsCommon.myCstr(dr("FileName"))
        '    obj.DOC_PATH = clsCommon.myCstr(dr("DOC_PATH"))
        '    obj.SUBMIT_DATE = clsCommon.myCDate(dr("SUBMIT_DATE"))
        '    obj.COMMENTS = clsCommon.myCstr(dr("COMMENTS"))
        '    obj.EnteredBy = clsCommon.myCstr(dr("EnteredBy"))
        '    obj.EnteredByName = clsCommon.myCstr(dr("User_Name"))
        'End If
        Return dt
    End Function

    Public Function SaveData(ByVal obj As clsAttachDocument) As String
        Return SaveData(obj, Nothing)
    End Function
    Public Function SaveData(ByVal obj As clsAttachDocument, ByVal trans As SqlTransaction) As String
        Dim isSaved As Boolean = True
        Dim isNewEntry As Boolean = True
        Dim qry As String = ""
        Try
            If clsCommon.myLen(obj.CODE) > 0 Then
                isNewEntry = False
            Else
                qry = " select MAX(CODE) from TSPL_ATTACHMENTS "
                obj.CODE = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
                If clsCommon.myLen(obj.CODE) <= 0 Then
                    obj.CODE = "DOC000000001"
                Else
                    obj.CODE = clsCommon.incval(obj.CODE)
                End If
                isNewEntry = True
            End If

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "FormId", obj.FormId)
            clsCommon.AddColumnsForChange(coll, "TransactionId", obj.TransactionId)
            clsCommon.AddColumnsForChange(coll, "SNo", obj.SNo)
            clsCommon.AddColumnsForChange(coll, "FileName", obj.FileName)
            'clsCommon.AddColumnsForChange(coll, "FileData", obj.FileData)
            clsCommon.AddColumnsForChange(coll, "COMMENTS", obj.COMMENTS)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
            qry = ""
            qry = "SELECT Count(*) FROM TSPL_ATTACHMENTS where CODE = '" + obj.CODE + "' "
            Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
            If check = 0 AndAlso isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "CODE", obj.CODE)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ATTACHMENTS", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ATTACHMENTS", OMInsertOrUpdate.Update, " CODE = '" + obj.CODE + "'  ", trans)
            End If

            If Not isSaved Then
                obj.CODE = ""
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

        Return obj.CODE
    End Function

    Public Shared Function GetFileName(ByVal strCode As String, ByVal trans As SqlTransaction) As String
        Dim qry As String = "select FileName from TSPL_ATTACHMENTS where CODE ='" + strCode + "' "
        Return clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
    End Function

    Public Shared Function GetDocumentByte(ByVal strCode As String) As DataTable
        Dim qry As String = " select * from TSPL_ATTACHMENTS where CODE='" + strCode + "' "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        Dim data As Byte() = dt.Rows(0)("FileData")
        Return dt
    End Function


    Public Shared Function GetGRNQCDocumentByte(ByVal strGRNNo As String) As DataTable
        Dim qry As String = " select * from TSPL_GRN_CATTEL_FEED_QC where GRN_No='" + strGRNNo + "' and FileData is not NULL "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        Return dt
    End Function
End Class
