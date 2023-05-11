Imports common

Imports System.Net.Cache
Imports System.Net.Security
Imports System.Runtime.Serialization
Imports System.Security.Cryptography.X509Certificates
Imports System.Web
Imports System.Windows.Forms
Imports Telerik.WinControls
Imports System.ComponentModel
Imports System.Data.SqlClient
Imports System.IO
Imports System.Net

Imports Newtonsoft.Json.Linq
Imports Newtonsoft.Json
Public Class clsEWayBill
#Region "Variables"
    Public Property supplyType As String
    Public Property subSupplyType As String
    Public Property subSupplyDesc As String
    Public Property docType As String
    Public Property docNo As String
    Public Property docDate As String
    Public Property fromGstin As String
    Public Property fromTrdName As String
    Public Property fromAddr1 As String
    Public Property fromAddr2 As String
    Public Property fromPlace As String
    Public Property actFromStateCode As String
    Public Property fromPincode As String
    Public Property fromStateCode As String
    Public Property toGstin As String
    Public Property toTrdName As String
    Public Property toAddr1 As String
    Public Property toAddr2 As String
    Public Property toPlace As String
    Public Property toPincode As String
    Public Property actToStateCode As String
    Public Property toStateCode As String
    Public Property transactionType As String
    Public Property dispatchFromGSTIN As String
    Public Property dispatchFromTradeName As String
    Public Property shipToGSTIN As String
    Public Property shipToTradeName As String
    Public Property totalValue As String
    Public Property cgstValue As String
    Public Property sgstValue As String
    Public Property igstValue As String
    Public Property cessValue As String
    Public Property cessNonAdvolValue As String
    Public Property totInvValue As String
    Public Property transMode As String
    Public Property transDistance As String
    Public Property transporterName As String
    Public Property transporterId As String
    Public Property transDocNo As String
    Public Property transDocDate As String
    Public Property vehicleNo As String
    Public Property vehicleType As String
    Public Property itemList As ItemList()
#End Region

    Public Shared Function PostDataOnPortal(ByVal strCompCode As String, ByVal strTokenNo As String, ByVal strQry As String, ByVal strLocation As String, ByVal trans As SqlTransaction) As Object
        Dim httpRequest As HttpWebRequest = Nothing
        Dim httpResponse As HttpWebResponse = Nothing
        Dim objResult As Object = Nothing
        Try
            Dim qry As String = String.Empty
            Dim strEInvoiceVendor As String = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.EInvoiceVendor, clsFixedParameterCode.EInvoiceVendor, trans))
            If clsCommon.myLen(clsCommon.myCstr(strEInvoiceVendor)) > 0 Then
                If clsCommon.CompairString(strEInvoiceVendor.ToUpper(), "MASTERGST") = CompairStringResult.Equal Then
                    qry = "Select * from TSPL_EInvoiceHeader_Info where Comp_Code='" & strCompCode & "' and RequiredFor ='GenerateAuthToken_IRN' and VendorName='MASTERGST' and Location_Code='" & strLocation & "'"
                Else
                    qry = "Select * from TSPL_EInvoiceHeader_Info where Comp_Code='" & strCompCode & "' and RequiredFor ='GenerateAuthToken_IRN' and VendorName='CLEARTAX' and Location_Code='" & strLocation & "'"
                End If

                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                If dt IsNot Nothing AndAlso dt.Rows.Count >> 0 Then
                    httpRequest = CType(WebRequest.Create(clsCommon.myCstr(dt.Rows(0)("Url"))), HttpWebRequest)
                    httpRequest.ContentType = "application/json"
                    If clsCommon.CompairString(strEInvoiceVendor.ToUpper(), "MASTERGST") = CompairStringResult.Equal Then
                        httpRequest.Headers.Add("ip_address", clsCommon.myCstr(dt.Rows(0)("ip_address")))
                        httpRequest.Headers.Add("client_id", clsCommon.myCstr(dt.Rows(0)("client_id")))
                        httpRequest.Headers.Add("client_secret", clsCommon.myCstr(dt.Rows(0)("client_secret")))
                        httpRequest.Headers.Add("username", clsCommon.myCstr(dt.Rows(0)("username")))
                        httpRequest.Headers.Add("auth-token", strTokenNo)
                        httpRequest.Headers.Add("gstin", clsCommon.myCstr(dt.Rows(0)("gstin")))
                        httpRequest.Method = "POST"
                    Else
                        httpRequest.Headers.Add("x-cleartax-auth-token", clsCommon.myCstr(dt.Rows(0)("client_secret")))
                        httpRequest.Headers.Add("owner_id", clsCommon.myCstr(dt.Rows(0)("client_id")))
                        httpRequest.Headers.Add("gstin", clsCommon.myCstr(dt.Rows(0)("gstin")))
                        httpRequest.Method = "PUT"
                    End If

                    httpRequest.KeepAlive = True
                    'Dim jObj As JObject
                    Dim strInvoiceDetails As String = String.Empty
                    Using streamWriter = New StreamWriter(httpRequest.GetRequestStream())

                        Dim dtDetails As DataTable = clsDBFuncationality.GetDataTable(strQry, trans)
                        If dtDetails IsNot Nothing AndAlso dtDetails.Rows.Count > 0 Then

                            Dim objInvDetails As Object
                            If objCommonVar.GenerateEWayBillWithEInvoice = True Then
                                objInvDetails = New clsEInvoiceDetailsWithEWayBill()
                            Else
                                objInvDetails = New clsEInvoiceDetails()
                            End If


                            objInvDetails.Version = "1.1"
                            objInvDetails.ItemList = New List(Of ClsItemList)

                            ''Transaction Info
                            objInvDetails.TranDtls.Add("TaxSch", "GST")
                            objInvDetails.TranDtls.Add("SupTyp", clsCommon.myCstr(dtDetails.Rows(0)("SupTyp")))
                            objInvDetails.TranDtls.Add("RegRev", "N")
                            objInvDetails.TranDtls.Add("IgstOnIntra", clsCommon.myCstr(dtDetails.Rows(0)("IgstOnIntra")))

                            ''Document info
                            objInvDetails.DocDtls.Add("Typ", clsCommon.myCstr(dtDetails.Rows(0)("DocType")))
                            objInvDetails.DocDtls.Add("No", clsCommon.myCstr(dtDetails.Rows(0)("DocNo")))
                            objInvDetails.DocDtls.Add("Dt", clsCommon.myCDate(dtDetails.Rows(0)("DocDate")))

                            ''Seller Info 
                            objInvDetails.SellerDtls.Add("Gstin", clsCommon.myCstr(dtDetails.Rows(0)("SellerGSTINNo")))
                            objInvDetails.SellerDtls.Add("LglNm", clsCommon.myCstr(dtDetails.Rows(0)("SellerLglNm")))
                            objInvDetails.SellerDtls.Add("TrdNm", clsCommon.myCstr(dtDetails.Rows(0)("SellerTrdNm")))
                            If clsCommon.myLen(clsCommon.myCstr(dtDetails.Rows(0)("SellerAdd1"))) > 0 Then
                                objInvDetails.SellerDtls.Add("Addr1", clsCommon.myCstr(dtDetails.Rows(0)("SellerAdd1")))
                            End If
                            If clsCommon.myLen(clsCommon.myCstr(dtDetails.Rows(0)("SellerAdd2"))) > 0 Then
                                objInvDetails.SellerDtls.Add("Addr2", clsCommon.myCstr(dtDetails.Rows(0)("SellerAdd2")))
                            End If
                            If clsCommon.myLen(clsCommon.myCstr(dtDetails.Rows(0)("SellerLoc"))) > 0 Then
                                objInvDetails.SellerDtls.Add("Loc", clsCommon.myCstr(dtDetails.Rows(0)("SellerLoc")))
                            End If
                            If clsCommon.myLen(clsCommon.myCstr(dtDetails.Rows(0)("SellerPincode"))) > 0 Then
                                objInvDetails.SellerDtls.Add("Pin", dtDetails.Rows(0)("SellerPincode"))
                            End If
                            If clsCommon.myLen(clsCommon.myCstr(dtDetails.Rows(0)("SellerStcd"))) > 0 Then
                                objInvDetails.SellerDtls.Add("Stcd", clsCommon.myCstr(dtDetails.Rows(0)("SellerStcd")))
                            End If
                            If clsCommon.myLen(clsCommon.myCstr(dtDetails.Rows(0)("SellerPhone"))) > 0 Then
                                Dim strPhone As String = clsCommon.myCstr(dtDetails.Rows(0)("SellerPhone"))
                                If strPhone.Contains(")") Then
                                    Dim strindex As Integer = strPhone.IndexOf(")") + 1
                                    strPhone = strPhone.Substring(strindex, strPhone.Length - strindex)
                                    strPhone = strPhone.Replace("_", "")
                                Else
                                    strPhone = strPhone.Replace("_", "")
                                End If
                                If clsCommon.myLen(strPhone) > 0 Then
                                    objInvDetails.SellerDtls.Add("Ph", strPhone)
                                End If
                            End If
                            If clsCommon.myLen(clsCommon.myCstr(dtDetails.Rows(0)("SellerEmail"))) > 0 Then
                                objInvDetails.SellerDtls.Add("Em", clsCommon.myCstr(dtDetails.Rows(0)("SellerEmail")))
                            End If


                            ''Buyer Info 
                            objInvDetails.BuyerDtls.Add("Gstin", clsCommon.myCstr(dtDetails.Rows(0)("BuyerGSTINNo")))
                            objInvDetails.BuyerDtls.Add("LglNm", clsCommon.myCstr(dtDetails.Rows(0)("BuyerLglNm")))
                            objInvDetails.BuyerDtls.Add("TrdNm", clsCommon.myCstr(dtDetails.Rows(0)("BuyerTrdNm")))
                            If clsCommon.myLen(clsCommon.myCstr(dtDetails.Rows(0)("BuyerPOS"))) > 0 Then
                                objInvDetails.BuyerDtls.Add("Pos", clsCommon.myCstr(dtDetails.Rows(0)("BuyerPOS")))
                            End If
                            If clsCommon.myLen(clsCommon.myCstr(dtDetails.Rows(0)("BuyerAdd1"))) > 0 Then
                                objInvDetails.BuyerDtls.Add("Addr1", clsCommon.myCstr(dtDetails.Rows(0)("BuyerAdd1")))
                            End If
                            If clsCommon.myLen(clsCommon.myCstr(dtDetails.Rows(0)("BuyerAdd2"))) > 0 Then
                                objInvDetails.BuyerDtls.Add("Addr2", clsCommon.myCstr(dtDetails.Rows(0)("BuyerAdd2")))
                            End If
                            If clsCommon.myLen(clsCommon.myCstr(dtDetails.Rows(0)("BuyerLoc"))) > 0 Then
                                objInvDetails.BuyerDtls.Add("Loc", clsCommon.myCstr(dtDetails.Rows(0)("BuyerLoc")))
                            End If
                            If clsCommon.myLen(clsCommon.myCstr(dtDetails.Rows(0)("BuyerPincode"))) > 0 Then
                                objInvDetails.BuyerDtls.Add("Pin", dtDetails.Rows(0)("BuyerPincode"))
                            End If
                            If clsCommon.myLen(clsCommon.myCstr(dtDetails.Rows(0)("BuyerStcd"))) > 0 Then
                                objInvDetails.BuyerDtls.Add("Stcd", clsCommon.myCstr(dtDetails.Rows(0)("BuyerStcd")))
                            End If
                            If clsCommon.myLen(clsCommon.myCstr(dtDetails.Rows(0)("BuyerPhone"))) > 0 Then
                                Dim strPhone As String = clsCommon.myCstr(dtDetails.Rows(0)("BuyerPhone"))
                                If strPhone.Contains(")") Then
                                    Dim strindex As Integer = strPhone.IndexOf(")") + 1
                                    strPhone = strPhone.Substring(strindex, strPhone.Length - strindex)
                                    strPhone = strPhone.Replace("_", "")
                                Else
                                    strPhone = strPhone.Replace("_", "")
                                End If
                                If clsCommon.myLen(strPhone) > 0 Then
                                    objInvDetails.BuyerDtls.Add("Ph", strPhone)
                                End If
                            End If
                            If clsCommon.myLen(clsCommon.myCstr(dtDetails.Rows(0)("BuyerEmail"))) > 0 Then
                                objInvDetails.BuyerDtls.Add("Em", clsCommon.myCstr(dtDetails.Rows(0)("BuyerEmail")))
                            End If

                            For Each dr As DataRow In dtDetails.Rows
                                Dim objItemList As New ClsItemList()
                                objItemList.SlNo = clsCommon.myCstr(dr("ItemSlNo"))
                                objItemList.IsServc = clsCommon.myCstr(dr("ItemIsServc"))
                                objItemList.PrdDesc = clsCommon.myCstr(dr("ItemPrdDesc"))
                                objItemList.HsnCd = clsCommon.myCstr(dr("ItemHsnCd"))
                                objItemList.Qty = clsCommon.myCdbl(dr("ItemQty"))

                                If clsCommon.CompairString(clsCommon.myCstr(dr("ItemIsServc")).ToUpper, "Y") = CompairStringResult.Equal Then
                                    objItemList.Unit = "OTH"
                                Else
                                    'If clsCommon.CompairString(clsCommon.myCstr(dr("ItemUnit")).ToUpper, "CASE") = CompairStringResult.Equal Then
                                    '    objItemList.Unit = "CTN"
                                    'ElseIf clsCommon.CompairString(clsCommon.myCstr(dr("ItemUnit")).ToUpper, "KG") = CompairStringResult.Equal Then
                                    '    objItemList.Unit = "KGS"
                                    'ElseIf clsCommon.CompairString(clsCommon.myCstr(dr("ItemUnit")).ToUpper, "JAR") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(dr("ItemUnit")).ToUpper, "POUCH") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(dr("ItemUnit")).ToUpper, "TIN") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(dr("ItemUnit")).ToUpper, "PKT") = CompairStringResult.Equal Then
                                    '    objItemList.Unit = "PCS"
                                    'ElseIf clsCommon.CompairString(clsCommon.myCstr(dr("ItemUnit")).ToUpper, "BOTTLE") = CompairStringResult.Equal Then
                                    '    objItemList.Unit = "BTL"
                                    'Else
                                    '    objItemList.Unit = clsCommon.myCstr(dr("ItemUnit")).ToUpper
                                    'End If
                                    objItemList.Unit = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT ISNULL(GST_UNIT_CODE ,'') FROM TSPL_UNIT_MASTER WHERE UNIT_CODE='" & clsCommon.myCstr(dr("ItemUnit")).ToUpper & "'", trans))
                                End If

                                objItemList.UnitPrice = Math.Round(clsCommon.myCdbl(dr("ItemUnitPrice")), 3)
                                objItemList.TotAmt = clsCommon.myCdbl(dr("ItemTotAmt"))
                                objItemList.Discount = clsCommon.myCdbl(dr("ItemDiscount"))
                                objItemList.AssAmt = clsCommon.myCdbl(dr("ItemAssAmt"))
                                objItemList.GstRt = clsCommon.myCdbl(dr("ItemGstRt"))
                                objItemList.SgstAmt = clsCommon.myCdbl(dr("ItemSgstAmt"))
                                objItemList.IgstAmt = clsCommon.myCdbl(dr("ItemIgstAmt"))
                                objItemList.CgstAmt = clsCommon.myCdbl(dr("ItemCgstAmt"))
                                objItemList.OthChrg = clsCommon.myCdbl(dr("ItemOthChrg"))
                                objItemList.TotItemVal = clsCommon.myCdbl(dr("ItemTotItemVal"))
                                objInvDetails.ItemList.Add(objItemList)
                            Next

                            ''ValDtls info
                            Dim ValDtlsDiscount As Double = 0
                            objInvDetails.ValDtls.Add("AssVal", clsCommon.myCdbl(dtDetails.Rows(0)("ValDtlsAssVal")))
                            objInvDetails.ValDtls.Add("CgstVal", clsCommon.myCdbl(dtDetails.Rows(0)("ValDtlsCgstVal")))
                            objInvDetails.ValDtls.Add("SgstVal", clsCommon.myCdbl(dtDetails.Rows(0)("ValDtlsSgstVal")))
                            objInvDetails.ValDtls.Add("IgstVal", clsCommon.myCdbl(dtDetails.Rows(0)("ValDtlsIgstVal")))
                            ''objInvDetails.ValDtls.Add("Discount", clsCommon.myCdbl(dtDetails.Rows(0)("ValDtlsDiscount")))
                            objInvDetails.ValDtls.Add("Discount", clsCommon.myCdbl(ValDtlsDiscount))
                            objInvDetails.ValDtls.Add("OthChrg", clsCommon.myCdbl(dtDetails.Rows(0)("ValDtlsOthChrg")))
                            objInvDetails.ValDtls.Add("TotInvVal", clsCommon.myCdbl(dtDetails.Rows(0)("ValDtlsTotInvVal")))
                            objInvDetails.ValDtls.Add("RndOffAmt", clsCommon.myCdbl(dtDetails.Rows(0)("ValDtlsRndOffAmt")))

                            'EwbDtls info
                            If objCommonVar.GenerateEWayBillWithEInvoice = True Then
                                If clsCommon.myLen(clsCommon.myCstr(dtDetails.Rows(0)("EwbTransId"))) > 0 Then
                                    objInvDetails.EwbDtls.Add("TransId", clsCommon.myCstr(dtDetails.Rows(0)("EwbTransId")))
                                    objInvDetails.EwbDtls.Add("TransName", clsCommon.myCstr(dtDetails.Rows(0)("EwbTransName")))
                                End If
                                objInvDetails.EwbDtls.Add("Distance", Convert.ToInt32(dtDetails.Rows(0)("EwbDistance")))
                                If clsCommon.myLen(clsCommon.myCstr(dtDetails.Rows(0)("EwbVehNo"))) > 0 Then
                                    objInvDetails.EwbDtls.Add("VehNo", clsCommon.myCstr(dtDetails.Rows(0)("EwbVehNo")))
                                End If
                                objInvDetails.EwbDtls.Add("VehType", "R")
                                objInvDetails.EwbDtls.Add("TransMode", "1")
                            End If

                            strInvoiceDetails = JsonConvert.SerializeObject(objInvDetails)
                            If clsCommon.CompairString(strEInvoiceVendor.ToUpper(), "CLEARTAX") = CompairStringResult.Equal Then
                                strInvoiceDetails = "[{" + """transaction"":" + strInvoiceDetails + "}]"
                            End If
                            streamWriter.Write(strInvoiceDetails)
                        End If

                    End Using

                    ''------
                    httpResponse = CType(httpRequest.GetResponse(), HttpWebResponse)
                    '' required validation httresponse
                    Dim strResult As String = String.Empty
                    Using streamReader = New StreamReader(httpResponse.GetResponseStream())
                        strResult = streamReader.ReadToEnd()
                    End Using
                    If clsCommon.CompairString(strEInvoiceVendor.ToUpper(), "CLEARTAX") = CompairStringResult.Equal Then
                        ''
                        Dim jSONObj As JArray = JArray.Parse(strResult)

                        Dim jObj As JObject = jSONObj(0)
                        If TypeOf jObj.SelectToken("govt_response") Is JObject Then
                            objResult = jObj.SelectToken("govt_response")
                            Dim status As String = objResult.SelectToken("Success").ToString
                            If clsCommon.CompairString(status, "N") = CompairStringResult.Equal Then
                                Dim strStatusDesc As String = (objResult.SelectToken("ErrorDetails")).ToString()
                                objResult = Nothing
                                Throw New Exception(strStatusDesc)
                            End If
                            Return objResult
                        Else
                            Dim strStatusDesc As String = (jObj.SelectToken("status_desc")).ToString()
                            objResult = Nothing
                            Throw New Exception(strStatusDesc)
                        End If


                    Else
                        Dim jObj As JObject = JObject.Parse(strResult)

                        If TypeOf jObj.SelectToken("data") Is JObject Then
                            objResult = jObj.SelectToken("data")
                            Return objResult
                        Else
                            Dim strStatusDesc As String = (jObj.SelectToken("status_desc")).ToString()
                            objResult = Nothing
                            Throw New Exception(strStatusDesc)
                        End If

                    End If

                End If

            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return objResult
    End Function
End Class

Public Class ItemList
    Public Property productName As String
    Public Property productDesc As String
    Public Property hsnCode As String
    Public Property quantity As String
    Public Property qtyUnit As String
    Public Property taxableAmount As String
    Public Property sgstRate As String
    Public Property cgstRate As String
    Public Property igstRate As String
    Public Property cessRate As String
End Class
