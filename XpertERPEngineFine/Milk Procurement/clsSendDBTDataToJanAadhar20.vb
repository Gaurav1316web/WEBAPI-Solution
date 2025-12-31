Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports System.Web.Script.Serialization

Public Class clsSendDBTDataToJanAadhar20
    'Public Shared Function SendData(ByVal PortNo As String, ByVal EntitlementId As String, ByVal JanaadhaarId As String,
    '         ByVal JanaadhaarMemId As String, ByVal BankAccNo As String, ByVal IFSC As String, ByVal MICR As String,
    '        ByVal AadharNo As String, ByVal PaymentAmount As String, ByVal PaymentDate As String) As SendDBTDataToJanAadharApiResponse
    '    Dim url As String = "http://saras.rajasthan.gov.in:7888/api/JanAadhar/JanAadharSendDBTDataToJanAadhar"
    '    Dim boundary As String = "---------------------------" & DateTime.Now.Ticks.ToString("x")
    '    Dim request As HttpWebRequest = CType(WebRequest.Create(url), HttpWebRequest)

    '    request.Method = "POST"
    '    request.ContentType = "multipart/form-data; boundary=" & boundary
    '    request.KeepAlive = True
    '    request.Timeout = 60000

    '    Dim postData As New StringBuilder()

    '    AddFormField(postData, boundary, "Key", "Tecxpert@MP#123$456%789^")
    '    AddFormField(postData, boundary, "EntitlementId", "8092D03391")
    '    AddFormField(postData, boundary, "EntitlementMemId", "8092D03391")
    '    AddFormField(postData, boundary, "JanaadhaarId", "5125747221")
    '    AddFormField(postData, boundary, "JanaadhaarMemId", "30281775235")
    '    AddFormField(postData, boundary, "BankAccNo", "34530100004039")
    '    AddFormField(postData, boundary, "IFSC", "BARB0SALUMB")

    '    ' Blank values
    '    AddFormField(postData, boundary, "MICR", "")
    '    AddFormField(postData, boundary, "AadharNo", "")

    '    AddFormField(postData, boundary, "PaymentAmount", "280.00")
    '    AddFormField(postData, boundary, "PaymentDate", "27/02/2024")
    '    AddFormField(postData, boundary, "PortNo", "1234")

    '    postData.Append("--" & boundary & "--" & vbCrLf)

    '    Dim dataBytes As Byte() = Encoding.UTF8.GetBytes(postData.ToString())
    '    request.ContentLength = dataBytes.Length

    '    Using requestStream = request.GetRequestStream()
    '        requestStream.Write(dataBytes, 0, dataBytes.Length)
    '    End Using

    '    Dim response As HttpWebResponse = CType(request.GetResponse(), HttpWebResponse)
    '    Dim responseText As String = ""

    '    Using reader As New StreamReader(response.GetResponseStream())
    '        responseText = reader.ReadToEnd()
    '    End Using


    '    Dim json As String =
    '    Dim result As SendDBTDataToJanAadharApiResponse = JsonConvert.DeserializeObject(Of SendDBTDataToJanAadharApiResponse)(json)
    '    '' Access values
    '    'Dim status = result.Response.Status
    '    'Dim message = result.Response.Message
    '    'Dim txnId = result.Response.TransactionId
    '    Return result
    'End Function
End Class

Public Class SendDBTDataToJanAadharApiResponse

    <JsonProperty("response")>
    Public Property Response As SendDBTDataToJanAadharResponseData

    <JsonProperty("signature")>
    Public Property Signature As String

End Class


Public Class SendDBTDataToJanAadharResponseData

    <JsonProperty("status")>
    Public Property Status As Boolean

    <JsonProperty("message")>
    Public Property Message As String

    <JsonProperty("responseCode")>
    Public Property ResponseCode As String

    <JsonProperty("transactionId")>
    Public Property TransactionId As String

    <JsonProperty("schemeCode")>
    Public Property SchemeCode As String

    <JsonProperty("appCode")>
    Public Property AppCode As String

    <JsonProperty("tid")>
    Public Property Tid As String   ' nullable

    <JsonProperty("data")>
    Public Property Data As Object  ' can be object or String

    <JsonProperty("janId")>
    Public Property JanId As String

End Class






