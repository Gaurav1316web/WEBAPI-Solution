Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports System.Web.Script.Serialization

Public Class clsSendDBTDataToJanAadhar20
    Public Shared Function SendData(ByVal obj As JSONClsSendDBTToJanaadhar) As JSONClsSendDBTToJanaadharResponse

        Dim objReturn As New JSONClsSendDBTToJanaadharResponse
        Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer
        Dim strJSON As String = serializer.Serialize(obj)
        Return objReturn
    End Function
End Class

Public Class JSONClsSendDBTToJanaadhar

    <JsonProperty("transactionId")>
    Public Property TransactionId As String

    <JsonProperty("schemeCode")>
    Public Property SchemeCode As String

    ' Note: JSON key has spaces → " appCode "
    <JsonProperty("appCode")>
    Public Property AppCode As String

    <JsonProperty("entitlementId")>
    Public Property EntitlementId As String

    <JsonProperty("entitlementMemId")>
    Public Property EntitlementMemId As String

    <JsonProperty("janaadhaarId")>
    Public Property JanaadhaarId As String

    <JsonProperty("janaadhaarMemId")>
    Public Property JanaadhaarMemId As String

    <JsonProperty("transactionMode")>
    Public Property TransactionMode As String

    <JsonProperty("aadharNo")>
    Public Property AadharNo As String

    <JsonProperty("bankAccNo")>
    Public Property BankAccNo As String

    <JsonProperty("ifsc")>
    Public Property IFSC As String

    <JsonProperty("micr")>
    Public Property MICR As String

    <JsonProperty("paymentAmount")>
    Public Property PaymentAmount As String

    <JsonProperty("paymentDate")>
    Public Property PaymentDate As String

End Class

Public Class JSONClsSendDBTToJanaadharResponse

End Class
