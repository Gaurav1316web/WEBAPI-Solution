Imports System
Imports System.IO
Imports System.Net
Imports System.Text
Imports System.Xml
Imports System.Xml.Serialization
Public Class clsPDAccountBankResponse
    Public Shared Function GetRejectionList(ByVal DeptRefNo As String) As List(Of RejectionTmp)

        Dim url As String = "http://paymanagerapi.rajasthan.gov.in/PayWebServicePro/EkuberWebService.asmx"

        Try
            Dim soapEnvelope As String =
                "<?xml version=""1.0"" encoding=""utf-8""?>" &
                "<soap:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance""" &
                " xmlns:xsd=""http://www.w3.org/2001/XMLSchema""" &
                " xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">" &
                "<soap:Body>" &
                "<RejectionDataDisplay xmlns=""http://tempuri.org/"">" &
                "<UserName>9595</UserName>" &
                "<Password>Nic@9595</Password>" &
                "<DeptRefNo>" & DeptRefNo & "</DeptRefNo>" &
                "<DeptCode>948</DeptCode>" &
                "</RejectionDataDisplay>" &
                "</soap:Body>" &
                "</soap:Envelope>"

            Dim request As HttpWebRequest = CType(WebRequest.Create(url), HttpWebRequest)
            request.ContentType = "text/xml; charset=utf-8"
            request.Method = "POST"

            request.Headers.Add("SOAPAction", "http://tempuri.org/RejectionDataDisplay")
            request.Timeout = 60000

            Dim bytes() As Byte = Encoding.UTF8.GetBytes(soapEnvelope)
            request.ContentLength = bytes.Length

            Using stream As Stream = request.GetRequestStream()
                stream.Write(bytes, 0, bytes.Length)
            End Using

            Dim response As HttpWebResponse = CType(request.GetResponse(), HttpWebResponse)
            Dim responseXml As String = ""

            Using reader As New StreamReader(response.GetResponseStream())
                responseXml = reader.ReadToEnd()
            End Using

            Return GetTmpListFromSoapXML(responseXml)
        Catch ex As Exception
            Throw New Exception("Ekuber Service Error: " & ex.Message)
        End Try

    End Function

    Public Shared Function GetRejectionData(ByVal DeptRefNo As String) As DataTable ''List(Of RejectionTmp)

        Dim url As String = "http://paymanagerapi.rajasthan.gov.in/PayWebServicePro/EkuberWebService.asmx"

        Try
            Dim soapEnvelope As String =
                "<?xml version=""1.0"" encoding=""utf-8""?>" &
                "<soap:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance""" &
                " xmlns:xsd=""http://www.w3.org/2001/XMLSchema""" &
                " xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">" &
                "<soap:Body>" &
                "<RejectionDataDisplay xmlns=""http://tempuri.org/"">" &
                "<UserName>9595</UserName>" &
                "<Password>Nic@9595</Password>" &
                "<DeptRefNo>" & DeptRefNo & "</DeptRefNo>" &
                "<DeptCode>948</DeptCode>" &
                "</RejectionDataDisplay>" &
                "</soap:Body>" &
                "</soap:Envelope>"

            Dim request As HttpWebRequest = CType(WebRequest.Create(url), HttpWebRequest)
            request.ContentType = "text/xml; charset=utf-8"
            request.Method = "POST"
            request.ContentType = "text/xml; charset=utf-8"
            request.Headers.Add("SOAPAction", "http://tempuri.org/RejectionDataDisplay")
            request.Timeout = 60000

            Dim bytes() As Byte = Encoding.UTF8.GetBytes(soapEnvelope)
            request.ContentLength = bytes.Length

            Using stream As Stream = request.GetRequestStream()
                stream.Write(bytes, 0, bytes.Length)
            End Using

            Dim response As HttpWebResponse = CType(request.GetResponse(), HttpWebResponse)
            Dim responseXml As String = ""

            Using reader As New StreamReader(response.GetResponseStream())
                responseXml = reader.ReadToEnd()
            End Using

            Dim ds As New DataSet()

            Using sr As New StringReader(responseXml)
                ds.ReadXml(sr)
            End Using

            If ds.Tables.Count > 0 Then
                Return ds.Tables(0)
            Else
                Return Nothing
            End If

            'Return GetTmpListFromSoapXML(responseXml)
        Catch ex As Exception
            Throw New Exception("Ekuber Service Error: " & ex.Message)
        End Try

    End Function



    Public Shared Function GetTmpListFromSoapXML(ByVal soapXml As String) As List(Of RejectionTmp)

        Dim result As New List(Of RejectionTmp)
        Dim doc As New XmlDocument()
        doc.LoadXml(soapXml)

        Dim ns As New XmlNamespaceManager(doc.NameTable)
        ns.AddNamespace("soap", "http://schemas.xmlsoap.org/soap/envelope/")
        ns.AddNamespace("diffgr", "urn:schemas-microsoft-com:xml-diffgram-v1")

        'All tmp nodes
        Dim nodes As XmlNodeList = doc.SelectNodes("//diffgr:diffgram/*/tmp", ns)

        For Each node As XmlNode In nodes

            Dim obj As New RejectionTmp With {
                .RefNo = GetNodeValue(node, "RefNo"),
                .Appid = clsCommon.myCDecimal(GetNodeValue(node, "Appid")),
                .BeneficiaryName = GetNodeValue(node, "BeneficiaryName"),
                .BeneficiaryAmt = clsCommon.myCDecimal(GetNodeValue(node, "BeneficiaryAmt")),
                .Reason = GetNodeValue(node, "Reason"),
                .RBIRjectionDate = GetNodeValue(node, "RBIRjectionDate"),
                .ApplicantID = clsCommon.myCDecimal(GetNodeValue(node, "ApplicantID"))
            }

            result.Add(obj)
        Next

        Return result

    End Function

    Private Shared Function GetNodeValue(parent As XmlNode, nodeName As String) As String
        Dim node As XmlNode = parent.SelectSingleNode(nodeName)
        If node Is Nothing Then
            Return ""
        Else
            Return node.InnerText.Trim()
        End If
    End Function
End Class

Public Class RejectionTmp
    Public Property RefNo As String
    Public Property Appid As Integer
    Public Property BeneficiaryName As String
    Public Property BeneficiaryAmt As Decimal
    Public Property Reason As String
    Public Property RBIRjectionDate As String
    Public Property ApplicantID As Integer
End Class
