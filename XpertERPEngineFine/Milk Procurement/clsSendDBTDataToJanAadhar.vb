Imports System
Imports System.IO
Imports System.Net
Imports System.Text
Imports System.Xml
Imports System.Xml.Serialization

Public Class clsSendDBTDataToJanAadhar
    Public Shared Function SendData(ByVal obj As clsXMLSendDBTDataToJanAadharHead) As clsXMLSendDBTDataToJanAadharResponseRoot
        Dim objReturn As clsXMLSendDBTDataToJanAadharResponseRoot = Nothing

        Try
            Dim serializer As XmlSerializer = New XmlSerializer(GetType(clsXMLSendDBTDataToJanAadharHead))
            Dim xmlWriterSettings = New XmlWriterSettings With {
                .OmitXmlDeclaration = True
            }
            Dim emptyNamespaces = New XmlSerializerNamespaces()
            emptyNamespaces.Add(String.Empty, String.Empty)
            Dim xmlRequest As String

            Using stringWriter = New StringWriter()
                Using varXmlWriter = XmlWriter.Create(stringWriter, xmlWriterSettings)
                    serializer.Serialize(varXmlWriter, obj, emptyNamespaces)
                    xmlRequest = stringWriter.ToString()
                End Using
            End Using

            Dim url As String = "http://janapp.rajasthan.gov.in/Service/action/transaction"
            Dim paramscheme As String = "cdusy"
            Dim parameters As String = "scheme=" & Uri.EscapeDataString(paramscheme)
            parameters += "&reqXml=" & Uri.EscapeDataString(xmlRequest)
            Dim request As HttpWebRequest = CType(WebRequest.Create(url), HttpWebRequest)
            request.Method = "POST"
            request.ContentType = "application/x-www-form-urlencoded"
            request.KeepAlive = False
            Dim byteArray As Byte() = Encoding.UTF8.GetBytes(parameters)
            request.ContentLength = byteArray.Length

            Using dataStream As Stream = request.GetRequestStream()
                dataStream.Write(byteArray, 0, byteArray.Length)
            End Using

            ServicePointManager.SecurityProtocol = CType(768, SecurityProtocolType) Or CType(3072, SecurityProtocolType)

            Using response As HttpWebResponse = CType(request.GetResponse(), HttpWebResponse)

                Using reader As StreamReader = New StreamReader(response.GetResponseStream())
                    Dim responseText As String = reader.ReadToEnd()
                    Dim Deserializer As XmlSerializer = New XmlSerializer(GetType(clsXMLSendDBTDataToJanAadharResponseRoot))

                    Using stringReader As StringReader = New StringReader(responseText)
                        objReturn = CType(Deserializer.Deserialize(stringReader), clsXMLSendDBTDataToJanAadharResponseRoot)
                    End Using
                End Using
            End Using

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

        Return objReturn
    End Function
End Class

Public Class clsXMLSendDBTDataToJanAadharHead
    <XmlElement("Transaction")>
    Public Property Transaction As clsXMLSendDBTDataToJanAadharDetail
End Class


<XmlRoot(ElementName:="Transaction")>
Public Class clsXMLSendDBTDataToJanAadharDetail
    <XmlElement(ElementName:="entitlementId")>
    Public Property EntitlementId As String
    <XmlElement(ElementName:="entitlementMemId")>
    Public Property EntitlementMemId As String
    <XmlElement(ElementName:="janaadhaarId")>
    Public Property JanaadhaarId As String
    <XmlElement(ElementName:="janaadhaarMemId")>
    Public Property JanaadhaarMemId As String
    <XmlElement(ElementName:="transactionId")>
    Public Property TransactionId As String
    <XmlElement(ElementName:="dueTransactionId")>
    Public Property DueTransactionId As String
    <XmlElement(ElementName:="aadharNo")>
    Public Property AadharNo As String
    <XmlElement(ElementName:="eid")>
    Public Property Eid As String
    <XmlElement(ElementName:="bankAccNo")>
    Public Property BankAccNo As String
    <XmlElement(ElementName:="ifsc")>
    Public Property Ifsc As String
    <XmlElement(ElementName:="micr")>
    Public Property Micr As String
    <XmlElement(ElementName:="paymentAmount")>
    Public Property PaymentAmount As String
    <XmlElement(ElementName:="paymentDate")>
    Public Property PaymentDate As String
    <XmlElement(ElementName:="status")>
    Public Property Status As String
End Class


<XmlRoot(ElementName:="root")>
Public Class clsXMLSendDBTDataToJanAadharResponseRoot
    <XmlElement(ElementName:="requestId")>
    Public Property RequestId As String
    <XmlElement(ElementName:="cmsg")>
    Public Property Cmsg As String
    <XmlElement(ElementName:="transaction")>
    Public Property Transaction As clsXMLSendDBTDataToJanAadharResponseTransaction
End Class

<XmlRoot(ElementName:="transaction")>
Public Class clsXMLSendDBTDataToJanAadharResponseTransaction
    <XmlElement(ElementName:="transactionId")>
    Public Property TransactionId As String
    <XmlElement(ElementName:="isSaved")>
    Public Property IsSaved As String
    <XmlElement(ElementName:="msg")>
    Public Property Msg As String
End Class


