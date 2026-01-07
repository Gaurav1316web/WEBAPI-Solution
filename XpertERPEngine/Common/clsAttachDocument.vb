Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports System.Net
Imports System.Text
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports System.Collections.Specialized
Imports System.Web.Script.Serialization

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

    'Public Function SaveData(ByVal obj As clsAttachDocument) As String
    '    Return SaveData(obj, Nothing)
    'End Function
    Public Function SaveData(ByVal obj As clsAttachDocument, ByVal trans As SqlTransaction, ByVal FilePath As String, ByVal RunServiceForUploadFolder As Boolean) As String
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
            Dim FileNo As Integer = -1
            If clsCommon.myLen(FilePath) > 0 Then
                Dim str As String
                If RunServiceForUploadFolder Then
                    FileNo = clsAttachDocument.UploadWithHttpRequest(FilePath, obj.FileName, obj.FormId, obj.TransactionId)
                End If
                Dim bData As Byte()
                Dim br As BinaryReader = New BinaryReader(System.IO.File.OpenRead(FilePath))
                bData = br.ReadBytes(br.BaseStream.Length)

                str = " UPDATE TSPL_ATTACHMENTS set FileData = @BLOBData "
                If FileNo > 0 Then
                    str += " ,FILE_INFO=" + clsCommon.myCstr(FileNo) + ""
                End If
                str += " where CODE='" + obj.CODE + "'"

                Dim cmd As SqlCommand = New SqlCommand(str, clsDBFuncationality.GetConnnection)
                Dim prm As New SqlParameter("@BLOBData", bData)
                cmd.Transaction = trans
                cmd.Parameters.Add(prm)
                cmd.ExecuteNonQuery()
                br.Close() 'done by stuti reagrding memory leakage
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
    Public Shared Function GetDocumentByteForEmail(ByVal strCode As String) As DataTable
        Dim qry As String = " select * from TSPL_EMAIL_HEAD where CODE='" + strCode + "' "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        Dim data As Byte() = dt.Rows(0)("Attachment_1")
        Return dt
    End Function


    Public Shared Function GetGRNQCDocumentByte(ByVal strGRNNo As String) As DataTable
        Dim qry As String = " select * from TSPL_GRN_CATTEL_FEED_QC where GRN_No='" + strGRNNo + "' and FileData is not NULL "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        Return dt
    End Function

    Public Shared Function UploadWithHttpRequest(ByVal filePath As String, ByVal fileName As String, ByVal ProgramCode As String, ByVal DocumentNo As String) As Integer
        Dim FileNo As Integer = -1
        Try
            Dim url As String = Nothing
            Dim FullResponse As String = Nothing
            If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "UDP") = CompairStringResult.Equal Then
                url = "http://172.21.80.251:7888/api/FileUploads/FileUploadWithDetails"  ''Server Live IP
            Else
                url = "http://103.122.38.34:7888/api/FileUploads/FileUploadWithDetails" '' Local IP
            End If

            'url = "http://localhost:1111/api/FileUploads/FileUploadWithDetails" ''Testing LOCALIIS BSP

            Dim fileByteArray As Byte() = File.ReadAllBytes(filePath)
            Dim formDataBoundary As String = $"----------{Guid.NewGuid()}"
            Dim contentType As String = "multipart/form-data; boundary=" & formDataBoundary
            Dim formData As Byte() = GetMultipartFormDataForUpload(fileByteArray, fileName, contentType, formDataBoundary)
            Dim request = TryCast(WebRequest.Create(url), HttpWebRequest)
            request.Headers.Add("Key", "Tecxpert@MP#123$456%789^")
            request.Headers.Add("DBName", objCommonVar.CurrDatabase)
            request.Headers.Add("ProgramCode", ProgramCode)
            request.Headers.Add("DocumentCode", DocumentNo)
            request.Method = "POST"
            request.ContentType = contentType
            'request.UserAgent = Credentials.UserName
            request.CookieContainer = New CookieContainer()
            request.ContentLength = formData.Length
            'request.Credentials = Credentials

            'Or (SecurityProtocolType)3072
            Using RequestStream As Stream = request.GetRequestStream()
                RequestStream.Write(formData, 0, formData.Length)
                RequestStream.Close()
                RequestStream.Dispose()
            End Using

            Dim response = TryCast(request.GetResponse(), HttpWebResponse)
            Dim ResponseReader = New StreamReader(response.GetResponseStream())
            FullResponse = ResponseReader.ReadToEnd()
            response.Close()
            'Return FullResponse


            Dim jObj As JObject = JObject.Parse(FullResponse)
            Dim ArrJ As JArray = Nothing
            Try
                If clsCommon.CompairString(clsCommon.myCstr(jObj.SelectToken("result")), "true") = CompairStringResult.Equal Then
                    ArrJ = JArray.Parse(clsCommon.myCstr(jObj.SelectToken("data")))
                    If clsCommon.myCDecimal(ArrJ(0).SelectToken("Result")) > 0 Then
                        FileNo = clsCommon.myCDecimal(ArrJ(0).SelectToken("Result"))
                    Else
                        Throw New Exception(ArrJ(0).SelectToken("Message"))
                    End If
                Else
                    ArrJ = JArray.Parse(clsCommon.myCstr(jObj.SelectToken("data")))
                    Throw New Exception(ArrJ(0).SelectToken("Message"))
                End If
            Finally
                ' Dispose objects to release memory
                If jObj IsNot Nothing Then
                    jObj = Nothing
                End If
                If ArrJ IsNot Nothing Then
                    ArrJ = Nothing
                End If
            End Try

            response = Nothing
            fileByteArray = Nothing
            formDataBoundary = Nothing
            contentType = Nothing
            formData = Nothing
            request = Nothing
            ResponseReader = Nothing
        Catch ex As Exception
            Throw ex
        Finally
            GC.Collect()
            GC.WaitForPendingFinalizers()
            GC.Collect()
        End Try
        Return FileNo
    End Function


    Public Shared Function SendOnWhatsApp(ByVal phoneNumberId As String, ByVal AccessToken As String, ByVal URL As String, ByVal API_Version As String, ByVal fileName As String, ByVal json As String) As Boolean
        Try
            ' 🔒 FORCE TLS 1.2 (CRITICAL)
            ServicePointManager.SecurityProtocol = CType(3072, SecurityProtocolType)
            Dim fileURL As String = Nothing
            If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "UDP") = CompairStringResult.Equal Then
                fileURL = "http://172.21.80.251:7888/api/FileUploads/FileUploadWithDetails"  ''Server Live IP
            Else
                fileURL = "http://103.122.38.34:7888/api/FileUploads/FileUploadWithDetails" '' Local IP
            End If
            fileURL += "/" & fileName
            'fileURL = "http://localhost:1111/api/FileUploads/FileUploadWithDetails" ''Testing LOCALIIS BSP

            Dim strURL As String = URL & "/" & API_Version & "/" & phoneNumberId & "/messages"
            Dim mobile As String = "919170001844"
            Dim customerName As String = "Alok"
            Dim messageText As String = "Hii Alok"
            ' --- Build JSON safely ---


            Dim payload As New Dictionary(Of String, Object) From {
    {"messaging_product", "whatsapp"},
    {"to", mobile},
    {"type", "template"},
    {"template", New Dictionary(Of String, Object) From {
        {"name", "tecxpert_send_common_document"},
        {"language", New Dictionary(Of String, String) From {
            {"code", "en_US"}
        }},
        {"components", New Object() {
            New Dictionary(Of String, Object) From {
                {"type", "header"},
                {"parameters", New Object() {
                    New Dictionary(Of String, Object) From {
                        {"type", "document"},
                        {"document", New Dictionary(Of String, String) From {
                            {"link", fileURL},
                            {"filename", fileName}
                        }}
                    }
                }}
            },
            New Dictionary(Of String, Object) From {
                {"type", "body"},
                {"parameters", New Object() {
                    New Dictionary(Of String, Object) From {
                        {"type", "text"},
                        {"text", customerName}
                    },
                    New Dictionary(Of String, Object) From {
                        {"type", "text"},
                        {"text", messageText}
                    }
                }}
            }
        }}
    }}
}



            Dim serializer As New JavaScriptSerializer()
            json = serializer.Serialize(payload)

            ' --- HTTP Request ---
            Dim request As HttpWebRequest = CType(WebRequest.Create(strURL), HttpWebRequest)
            request.Method = "POST"
            request.ContentType = "application/json"
            request.Headers.Add("Authorization", "Bearer " & AccessToken)

            Using stream As Stream = request.GetRequestStream()
                Dim bytes = Encoding.UTF8.GetBytes(json)
                stream.Write(bytes, 0, bytes.Length)
            End Using

            ' --- Read Response ---
            Using response As HttpWebResponse = CType(request.GetResponse(), HttpWebResponse)
                Using reader As New StreamReader(response.GetResponseStream())
                    Dim responseText As String = reader.ReadToEnd()
                    ' Optional: log responseText
                End Using
            End Using
            Return True
        Catch ex As WebException
            Using errResp = ex.Response
                If errResp IsNot Nothing Then
                    Using reader As New StreamReader(errResp.GetResponseStream())
                        Dim errorText As String = reader.ReadToEnd()
                        Throw New Exception("WhatsApp Error" & Environment.NewLine & errorText)
                    End Using
                Else
                    Throw New Exception(ex.Message)
                End If
            End Using
            Return False

        Catch ex As Exception
            Throw New Exception(ex.Message)
            Return False
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
