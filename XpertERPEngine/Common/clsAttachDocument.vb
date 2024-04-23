Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports System.Net

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
        Dim qry As String = "Select TSPL_ATTACHMENTS.CODE,TSPL_ATTACHMENTS.FormId,TSPL_ATTACHMENTS.TransactionId,TSPL_ATTACHMENTS.SNo,TSPL_ATTACHMENTS.FileName, TSPL_ATTACHMENTS.COMMENTS,TSPL_USER_MASTER.User_Name as Attach_By 
from TSPL_ATTACHMENTS 
left outer join TSPL_USER_MASTER on TSPL_USER_MASTER.User_Code=TSPL_ATTACHMENTS.Created_By
where 1=1 "
        qry += " and TSPL_ATTACHMENTS.FormId = '" + strFormId + "' and TSPL_ATTACHMENTS.TransactionId = '" + strTransactionId + "' "
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

    Public Shared Function UploadWithHttpRequest(ByVal filePath As String, ByVal fileName As String) As String
        Try
            Dim url As String = Nothing
            Dim FullResponse As String = Nothing
            If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "UDP") = CompairStringResult.Equal Then
                url = "http://172.21.80.251:7888/api/FileUploads/FileUpload"  ''Server Live IP
            Else
                url = "http://103.122.38.34:7888/api/FileUploads/FileUpload" '' Local IP
            End If
            Dim fileByteArray As Byte() = File.ReadAllBytes(filePath)
            Dim formDataBoundary As String = $"----------{Guid.NewGuid()}"
            Dim contentType As String = "multipart/form-data; boundary=" & formDataBoundary
            Dim formData As Byte() = GetMultipartFormDataForUpload(fileByteArray, fileName, contentType, formDataBoundary)
            Dim request = TryCast(WebRequest.Create(url), HttpWebRequest)
            request.Headers.Add("Key", "Tecxpert@MP#123$456%789^")
            request.Method = "POST"
            request.ContentType = contentType
            'request.UserAgent = Credentials.UserName
            request.CookieContainer = New CookieContainer()
            request.ContentLength = formData.Length
            'request.Credentials = Credentials

            Using RequestStream As Stream = request.GetRequestStream()
                RequestStream.Write(formData, 0, formData.Length)
                RequestStream.Close()
                RequestStream.Dispose()
            End Using

            Dim response = TryCast(request.GetResponse(), HttpWebResponse)
            Dim ResponseReader = New StreamReader(response.GetResponseStream())
            FullResponse = ResponseReader.ReadToEnd()
            response.Close()

            response = Nothing
            fileByteArray = Nothing
            formDataBoundary = Nothing
            contentType = Nothing
            formData = Nothing
            request = Nothing
            ResponseReader = Nothing

            Return FullResponse
        Catch ex As Exception
            Throw ex
        Finally
            GC.Collect()
            GC.WaitForPendingFinalizers()
            GC.Collect()
        End Try
    End Function


    Private Shared Function GetMultipartFormDataForUpload(ByVal byteArray As Byte(), ByVal fileName As String, ByVal contentType As String, ByVal Boundary As String) As Byte()
        Dim FormDataStream As Stream = New MemoryStream()
        Dim Header As String = String.Format("--{0}" & Environment.NewLine & "Content-Disposition: form-data; name=""{1}""; filename=""{2}""" + Environment.NewLine + Environment.NewLine, Boundary, "file", fileName)
        FormDataStream.Write(System.Text.Encoding.UTF8.GetBytes(Header), 0, System.Text.Encoding.UTF8.GetByteCount(Header))
        FormDataStream.Write(byteArray, 0, byteArray.Length)
        Dim Footer As String = Environment.NewLine & "--" & Boundary & "--" + Environment.NewLine
        FormDataStream.Write(System.Text.Encoding.UTF8.GetBytes(Footer), 0, System.Text.Encoding.UTF8.GetByteCount(Footer))
        FormDataStream.Position = 0L
        Dim FormData = New Byte(CInt((FormDataStream.Length - 1L + 1)) - 1) {}
        FormDataStream.Read(FormData, 0, FormData.Length)
        FormDataStream.Close()
        Return FormData
    End Function
End Class
