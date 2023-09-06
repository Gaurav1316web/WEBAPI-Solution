Imports System.Data.SqlClient
Imports System.IO
Imports System.Net
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq

Public Class ClsEInvoiceOFAPIs
#Region "Variables"
    '' Public HttpWebRequest()
#End Region
    Public Shared Function IsGenerateAuthTokenNo_Required(ByVal strCompCode As String, ByVal strLocation As String, ByVal trans As SqlTransaction) As String
        Dim strToken As String = String.Empty
        Try
            Dim strEInvoiceVendor As String = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.EInvoiceVendor, clsFixedParameterCode.EInvoiceVendor, trans))
            Dim TokenTimeReGenForEinvoice As String = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.TokenTimeReGenForEinvoice, clsFixedParameterCode.TokenTimeReGenForEinvoice, trans))
            If clsCommon.myLen(clsCommon.myCstr(strEInvoiceVendor)) > 0 Then
                If clsCommon.CompairString(strEInvoiceVendor.ToUpper(), "MASTERGST") = CompairStringResult.Equal Then
                    Dim dt1 As DataTable = clsDBFuncationality.GetDataTable("select * from TSPL_EInvoiceToken where Location_Code='" & strLocation & "'", trans)
                    If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
                        'Dim isGenerateTokenRequired As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select 'No' from TSPL_EINVOICETOKEN where  DATEDIFF (minute,getdate(),dateadd(MINUTE ,360,ResponseTime ))>0 and  DATEDIFF (minute,getdate(),dateadd(MINUTE ,360,ResponseTime ))<=58", trans))
                        'Dim isGenerateTokenRequired As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select 'No' from TSPL_EINVOICETOKEN where Location_Code='" & strLocation & "' and  DATEDIFF (minute,getdate(),dateadd(MINUTE ,60,ResponseTime ))>0 and  DATEDIFF (minute,getdate(),dateadd(MINUTE ,60,ResponseTime ))<=58", trans))
                        Dim isGenerateTokenRequired As String = String.Empty
                        If clsCommon.myCdbl(TokenTimeReGenForEinvoice) > 0 Then
                            isGenerateTokenRequired = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select 'No' from TSPL_EINVOICETOKEN where Location_Code='" & strLocation & "' and  DATEDIFF (minute,getdate(),dateadd(MINUTE ," & TokenTimeReGenForEinvoice & ",ResponseTime ))>0 and  DATEDIFF (minute,getdate(),dateadd(MINUTE ," & TokenTimeReGenForEinvoice & ",ResponseTime ))<=" & clsCommon.myCdbl(TokenTimeReGenForEinvoice) - 2 & "", trans))
                        Else
                            isGenerateTokenRequired = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select 'No' from TSPL_EINVOICETOKEN where Location_Code='" & strLocation & "' and convert(smalldatetime,expirytime,103)>convert(smalldatetime,getdate(),103)", trans))
                        End If

                        If clsCommon.myLen(isGenerateTokenRequired) > 0 AndAlso clsCommon.CompairString(isGenerateTokenRequired, "No") = CompairStringResult.Equal Then
                            strToken = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select AuthToken from TSPL_EInvoiceToken where Location_Code='" & strLocation & "'", trans))
                        Else
                            GenerateAuthTokenNo(strCompCode, strLocation, trans)
                            strToken = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select AuthToken from TSPL_EInvoiceToken where Location_Code='" & strLocation & "'", trans))
                        End If
                    Else
                        GenerateAuthTokenNo(strCompCode, strLocation, trans)
                        strToken = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select AuthToken from TSPL_EInvoiceToken where Location_Code='" & strLocation & "'", trans))
                    End If
                Else
                    strToken = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select top 1 client_secret As TokenId from tspl_einvoiceHeader_info where VendorName='CLEARTAX' and Location_Code='" & strLocation & "'", trans))
                End If
            End If


        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return strToken
    End Function
    Public Shared Function GenerateAuthTokenNo(ByVal strCompCode As String, ByVal strLocation As String, ByVal trans As SqlTransaction)
        Dim httpRequest As HttpWebRequest = Nothing
        Dim httpResponse As HttpWebResponse = Nothing
        Try
            Dim dt As DataTable = clsDBFuncationality.GetDataTable("Select * from TSPL_EInvoiceHeader_Info where Comp_Code='" & strCompCode & "' and RequiredFor ='GenerateAuthToken_Get' and Location_Code='" & strLocation & "'", trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count >> 0 Then
                httpRequest = CType(WebRequest.Create(clsCommon.myCstr(dt.Rows(0)("Url"))), HttpWebRequest)
                httpRequest.ContentType = "application/json"
                httpRequest.Headers.Add("username", clsCommon.myCstr(dt.Rows(0)("username")))
                httpRequest.Headers.Add("password", clsCommon.myCstr(dt.Rows(0)("password")))
                httpRequest.Headers.Add("ip_address", clsCommon.myCstr(dt.Rows(0)("ip_address")))
                httpRequest.Headers.Add("client_id", clsCommon.myCstr(dt.Rows(0)("client_id")))
                httpRequest.Headers.Add("client_secret", clsCommon.myCstr(dt.Rows(0)("client_secret")))
                httpRequest.Headers.Add("gstin", clsCommon.myCstr(dt.Rows(0)("gstin")))
                httpRequest.Method = "GET"

                httpRequest.KeepAlive = True

                httpResponse = CType(httpRequest.GetResponse(), HttpWebResponse)
                '' required validation httresponse
                Dim strResponseDate As DateTime = clsCommon.GETSERVERDATE(trans)
                Dim strResult As String = String.Empty
                Using streamReader = New StreamReader(httpResponse.GetResponseStream())
                    strResult = streamReader.ReadToEnd()
                End Using

                Dim jObj As JObject = JObject.Parse(strResult)
                'Dim strToken As String = (jObj.SelectToken("data")).SelectToken("AuthToken").ToString()
                Dim objResult As Object = Nothing
                Dim strExpiryDate As DateTime?
                Dim strToken As String = String.Empty
                If TypeOf jObj.SelectToken("data") Is JObject Then
                    objResult = jObj.SelectToken("data")
                    strToken = objResult.SelectToken("AuthToken").ToString
                    strExpiryDate = objResult.SelectToken("TokenExpiry").ToString
                Else
                    Dim strStatusDesc As String = (jObj.SelectToken("status_desc")).ToString()
                    Throw New Exception(strStatusDesc)
                End If

                ''insert token and datetime value into tspl_einnvoice_token table
                If clsCommon.myLen(strToken) > 0 Then
                    Dim dt1 As DataTable = clsDBFuncationality.GetDataTable("select * from TSPL_EInvoiceToken where Location_Code='" & strLocation & "' ", trans)
                    If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
                        clsDBFuncationality.ExecuteNonQuery("update TSPL_EInvoiceToken set AuthToken='" & strToken & "',ResponseTime='" & clsCommon.GetPrintDate(strResponseDate, "dd/MMM/yyyy hh:mm tt") & "',ExpiryTime='" & clsCommon.GetPrintDate(strExpiryDate, "dd/MMM/yyyy hh:mm tt") & "' where Location_Code='" & strLocation & "'", trans)
                    Else
                        clsDBFuncationality.ExecuteNonQuery("Insert Into TSPL_EInvoiceToken values ('" & strToken & "','" & clsCommon.GetPrintDate(strResponseDate, "dd/MMM/yyyy hh:mm tt") & "','" & strLocation & "','" & clsCommon.GetPrintDate(strExpiryDate, "dd/MMM/yyyy hh:mm tt") & "')", trans)
                    End If

                End If
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

        Return True
    End Function
    Public Shared Function PostAuthTokenNo_withInvoiceData_ForCustomerType_BC(ByVal strCompCode As String, ByVal strTokenNo As String, ByVal strQry As String, ByVal strLocation As String, ByVal trans As SqlTransaction) As Object
        Dim httpRequest As HttpWebRequest = Nothing
        Dim httpResponse As HttpWebResponse = Nothing
        Dim objResult As Object = Nothing
        Try
            Dim qry As String = String.Empty
            Dim strEInvoiceVendor As String = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.EInvoiceVendor, clsFixedParameterCode.EInvoiceVendor, trans))
            If clsCommon.myLen(clsCommon.myCstr(strEInvoiceVendor)) > 0 Then
                If clsCommon.CompairString(strEInvoiceVendor.ToUpper(), "MASTERGST") = CompairStringResult.Equal Then
                    qry = "Select * from TSPL_EInvoiceHeader_Info where Comp_Code='" & strCompCode & "' and RequiredFor ='GenerateDynamicQRCode' and VendorName='MASTERGST' and Location_Code='" & strLocation & "'"
                Else
                    qry = "Select * from TSPL_EInvoiceHeader_Info where Comp_Code='" & strCompCode & "' and RequiredFor ='GenerateDynamicQRCode' and VendorName='CLEARTAX' and Location_Code='" & strLocation & "'"
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
                        httpRequest.Headers.Add("sgstin", clsCommon.myCstr(dt.Rows(0)("gstin")))

                        Dim dtDetails As DataTable = clsDBFuncationality.GetDataTable(strQry, trans)
                        If dtDetails IsNot Nothing AndAlso dtDetails.Rows.Count > 0 Then
                            httpRequest.Headers.Add("docno", clsCommon.myCstr(dtDetails.Rows(0)("docno")))
                            httpRequest.Headers.Add("docdate", clsCommon.myCstr(dtDetails.Rows(0)("docdate")))
                            httpRequest.Headers.Add("totinvval", clsCommon.myCstr(dtDetails.Rows(0)("totinvval")))


                            If clsCommon.myLen(clsCommon.myCstr(dtDetails.Rows(0)("upiid"))) > 0 Then
                                httpRequest.Headers.Add("upiid", clsCommon.myCstr(dtDetails.Rows(0)("upiid")))
                            End If

                            If clsCommon.myLen(clsCommon.myCstr(dtDetails.Rows(0)("bankaccno"))) > 0 Then
                                httpRequest.Headers.Add("bankaccno", clsCommon.myCstr(dtDetails.Rows(0)("bankaccno")))
                            Else
                                Throw New Exception("Please fill Bank Acc No into Location Master for Location " & clsCommon.myCstr(dtDetails.Rows(0)("Bill_To_Location")) & "")
                            End If

                            If clsCommon.myLen(clsCommon.myCstr(dtDetails.Rows(0)("bankifsccode"))) > 0 Then
                                httpRequest.Headers.Add("bankifsccode", clsCommon.myCstr(dtDetails.Rows(0)("bankifsccode")))
                            Else
                                Throw New Exception("Please fill Bank IFSC Code into Location Master for Location " & clsCommon.myCstr(dtDetails.Rows(0)("Bill_To_Location")) & "")
                            End If

                            If clsCommon.myLen(clsCommon.myCstr(dtDetails.Rows(0)("accountholdername"))) > 0 Then
                                httpRequest.Headers.Add("accountholdername", clsCommon.myCstr(dtDetails.Rows(0)("accountholdername")))
                            Else
                                Throw New Exception("Please fill A/c holder Name into Location Master for Location " & clsCommon.myCstr(dtDetails.Rows(0)("Bill_To_Location")) & "")
                            End If

                            httpRequest.Headers.Add("igstamount", clsCommon.myCstr(dtDetails.Rows(0)("igstamount")))
                            httpRequest.Headers.Add("cgstamount", clsCommon.myCstr(dtDetails.Rows(0)("cgstamount")))
                            httpRequest.Headers.Add("sgstamount", clsCommon.myCstr(dtDetails.Rows(0)("sgstamount")))
                            httpRequest.Headers.Add("cessamount", clsCommon.myCstr(dtDetails.Rows(0)("cessamount")))
                        End If

                        httpRequest.Method = "GET"

                    Else
                            httpRequest.Headers.Add("x-cleartax-auth-token", clsCommon.myCstr(dt.Rows(0)("client_secret")))
                        httpRequest.Headers.Add("owner_id", clsCommon.myCstr(dt.Rows(0)("client_id")))
                        httpRequest.Headers.Add("gstin", clsCommon.myCstr(dt.Rows(0)("gstin")))
                        httpRequest.Method = "PUT"
                    End If

                    httpRequest.KeepAlive = True
                    ''Dim StreamWriter As StreamWriter = New StreamWriter(httpRequest.GetRequestStream())


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

    Public Shared Function PostAuthTokenNo_withInvoiceData(ByVal strCompCode As String, ByVal strTokenNo As String, ByVal strQry As String, ByVal strLocation As String, ByVal trans As SqlTransaction) As Object
        Return PostAuthTokenNo_withInvoiceData(strCompCode, strTokenNo, strQry, strLocation, trans, False)
    End Function
    Public Shared Function PostAuthTokenNo_withInvoiceData(ByVal strCompCode As String, ByVal strTokenNo As String, ByVal strQry As String, ByVal strLocation As String, ByVal trans As SqlTransaction, ByVal stopEWayBill As Boolean) As Object
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
                    Dim dtDetails As DataTable = clsDBFuncationality.GetDataTable(strQry, trans)
                    Using streamWriter = New StreamWriter(httpRequest.GetRequestStream())
                        If dtDetails IsNot Nothing AndAlso dtDetails.Rows.Count > 0 Then
                            Dim objInvDetails As Object
                            If objCommonVar.GenerateEWayBillWithEInvoice = True AndAlso Not stopEWayBill Then
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
                            If clsCommon.myLen(clsCommon.myCstr(dtDetails.Rows(0)("SellerGSTINNo"))) = 15 Then
                                objInvDetails.SellerDtls.Add("Gstin", clsCommon.myCstr(dtDetails.Rows(0)("SellerGSTINNo")))
                            Else
                                Throw New Exception("Seller GSTIN No. not Found/Invalid!")

                            End If

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
                                If clsCommon.myLen(clsCommon.myCstr(dtDetails.Rows(0)("SellerPincode"))) = 6 Then
                                    objInvDetails.SellerDtls.Add("Pin", dtDetails.Rows(0)("SellerPincode"))
                                Else
                                    Throw New Exception("Seller Pincode not Found/Invalid!")
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
                            If clsCommon.myLen(clsCommon.myCstr(dtDetails.Rows(0)("BuyerGSTINNo"))) = 15 Then
                                objInvDetails.BuyerDtls.Add("Gstin", clsCommon.myCstr(dtDetails.Rows(0)("BuyerGSTINNo")))

                            Else
                                Throw New Exception("Buyer GSTIN No. not Found/Invalid!")

                            End If
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
                                If clsCommon.myLen(clsCommon.myCstr(dtDetails.Rows(0)("BuyerPincode"))) = 6 Then
                                    objInvDetails.BuyerDtls.Add("Pin", dtDetails.Rows(0)("BuyerPincode"))
                                Else
                                    Throw New Exception("Buyer Pincode not Found/Invalid!")

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
                                    If Not stopEWayBill Then
                                        If clsCommon.myLen(clsCommon.myCstr(dtDetails.Rows(0)("EwbTransId"))) > 0 Then
                                            objInvDetails.EwbDtls.Add("TransId", clsCommon.myCstr(dtDetails.Rows(0)("EwbTransId")))
                                            objInvDetails.EwbDtls.Add("TransName", clsCommon.myCstr(dtDetails.Rows(0)("EwbTransName")))
                                        End If
                                        'objInvDetails.EwbDtls.Add("Distance", Convert.ToInt32(dtDetails.Rows(0)("EwbDistance")))
                                        objInvDetails.EwbDtls.Add("Distance", 0)
                                        If clsCommon.myLen(clsCommon.myCstr(dtDetails.Rows(0)("EwbVehNo"))) > 0 Then
                                            objInvDetails.EwbDtls.Add("VehNo", clsCommon.myCstr(dtDetails.Rows(0)("EwbVehNo")))
                                        End If
                                        objInvDetails.EwbDtls.Add("VehType", "R")
                                        objInvDetails.EwbDtls.Add("TransMode", "1")
                                    End If
                                End If

                                strInvoiceDetails = JsonConvert.SerializeObject(objInvDetails)
                                If clsCommon.CompairString(strEInvoiceVendor.ToUpper(), "CLEARTAX") = CompairStringResult.Equal Then
                                    strInvoiceDetails = "[{" + """transaction"":" + strInvoiceDetails + "}]"
                                End If
                                streamWriter.Write(strInvoiceDetails)
                            End If
                    End Using

                    Try
                        Dim coll As New Hashtable()
                        clsCommon.AddColumnsForChange(coll, "Document_Code", clsCommon.myCstr(dtDetails.Rows(0)("DocNo")))
                        clsCommon.AddColumnsForChange(coll, "JSON", strInvoiceDetails)
                        clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                        clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))
                        clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SD_SALE_INVOICE_JSON", OMInsertOrUpdate.Insert, "", trans)
                    Catch ex As Exception
                    End Try

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

    Public Shared Function PostAuthTokenNo_EWAYBillOnly(ByVal strCompCode As String, ByVal strTokenNo As String, ByVal strQry As String, ByVal strLocation As String, ByVal trans As SqlTransaction, ByVal strIRNNo As String) As Object
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
                    qry = clsCommon.myCstr(dt.Rows(0)("Url"))
                    qry = qry.Replace("GENERATE", "GENERATE_EWAYBILL")

                    httpRequest = CType(WebRequest.Create(qry), HttpWebRequest)
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
                    Dim dtDetails As DataTable = clsDBFuncationality.GetDataTable(strQry, trans)
                    Using streamWriter = New StreamWriter(httpRequest.GetRequestStream())
                        If dtDetails IsNot Nothing AndAlso dtDetails.Rows.Count > 0 Then
                            Dim objInvDetails As New clsWayBillOnly
                            objInvDetails.Irn = strIRNNo
                            objInvDetails.Distance = 0 ''Convert.ToInt32(dtDetails.Rows(0)("EwbDistance"))
                            objInvDetails.TransMode = 1
                            If clsCommon.myLen(clsCommon.myCstr(dtDetails.Rows(0)("EwbTransId"))) > 0 Then
                                objInvDetails.TransId = clsCommon.myCstr(dtDetails.Rows(0)("EwbTransId"))
                                objInvDetails.TransName = clsCommon.myCstr(dtDetails.Rows(0)("EwbTransName"))
                            End If

                            If clsCommon.myLen(clsCommon.myCstr(dtDetails.Rows(0)("EwbVehNo"))) > 0 Then
                                objInvDetails.VehNo = clsCommon.myCstr(dtDetails.Rows(0)("EwbVehNo"))
                            End If
                            objInvDetails.VehType = "R"
                            strInvoiceDetails = JsonConvert.SerializeObject(objInvDetails)
                            If clsCommon.CompairString(strEInvoiceVendor.ToUpper(), "CLEARTAX") = CompairStringResult.Equal Then
                                strInvoiceDetails = "[{" + """transaction"":" + strInvoiceDetails + "}]"
                            End If
                            streamWriter.Write(strInvoiceDetails)
                        End If
                    End Using

                    Try
                        Dim coll As New Hashtable()
                        clsCommon.AddColumnsForChange(coll, "Document_Code", clsCommon.myCstr(dtDetails.Rows(0)("DocNo")))
                        clsCommon.AddColumnsForChange(coll, "JSON", strInvoiceDetails)
                        clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                        clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))
                        clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SD_SALE_INVOICE_JSON", OMInsertOrUpdate.Insert, "", trans)
                    Catch ex As Exception
                    End Try

                    ''------
                    httpResponse = CType(httpRequest.GetResponse(), HttpWebResponse)
                    '' required validation httresponse
                    Dim strResult As String = String.Empty
                    Using streamReader = New StreamReader(httpResponse.GetResponseStream())
                        strResult = streamReader.ReadToEnd()
                    End Using
                    If clsCommon.CompairString(strEInvoiceVendor.ToUpper(), "CLEARTAX") = CompairStringResult.Equal Then
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

    Public Shared Function CancelIRN_withInvoiceData(ByVal strCompCode As String, ByVal strTokenNo As String, ByVal strIRN As String, ByVal strLocation As String, ByVal trans As SqlTransaction) As Object
        Dim httpRequest As HttpWebRequest = Nothing
        Dim httpResponse As HttpWebResponse = Nothing
        Dim objResult As Object = Nothing
        Try
            Dim qry As String = String.Empty
            Dim strEInvoiceVendor As String = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.EInvoiceVendor, clsFixedParameterCode.EInvoiceVendor, trans))
            If clsCommon.myLen(clsCommon.myCstr(strEInvoiceVendor)) > 0 Then
                If clsCommon.CompairString(strEInvoiceVendor.ToUpper(), "MASTERGST") = CompairStringResult.Equal Then
                    qry = "Select * from TSPL_EInvoiceHeader_Info where Comp_Code='" & strCompCode & "' and RequiredFor ='CancelIRN' and VendorName='MASTERGST' and Location_Code='" & strLocation & "' "
                Else
                    qry = "Select * from TSPL_EInvoiceHeader_Info where Comp_Code='" & strCompCode & "' and RequiredFor ='CancelIRN' and VendorName='CLEARTAX' and Location_Code='" & strLocation & "' "
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

                        If clsCommon.myLen(clsCommon.myCstr(strIRN)) > 0 Then
                            If clsCommon.CompairString(strEInvoiceVendor.ToUpper(), "CLEARTAX") = CompairStringResult.Equal Then
                                Dim objInvDetails As New clsEInvoiceCancelDetails_ClearTax()

                                objInvDetails.irn = strIRN
                                objInvDetails.CnlRsn = "1"
                                objInvDetails.CnlRem = "Wrong Entry"
                                strInvoiceDetails = JsonConvert.SerializeObject(objInvDetails)
                                strInvoiceDetails = "[" + strInvoiceDetails + "]"
                            Else
                                Dim objInvDetails As New clsEInvoiceCancelDetails()
                                objInvDetails.Irn = strIRN
                                objInvDetails.CnlRsn = "1"
                                objInvDetails.CnlRem = "Wrong Entry"
                                strInvoiceDetails = JsonConvert.SerializeObject(objInvDetails)
                            End If
                            streamWriter.Write(strInvoiceDetails)
                        Else
                            Throw New Exception("Please send IRN number")
                        End If

                    End Using

                    httpResponse = CType(httpRequest.GetResponse(), HttpWebResponse)
                    '' required validation httresponse
                    Dim strResult As String = String.Empty
                    Using streamReader = New StreamReader(httpResponse.GetResponseStream())
                        strResult = streamReader.ReadToEnd()
                    End Using
                    If clsCommon.CompairString(strEInvoiceVendor.ToUpper(), "CLEARTAX") = CompairStringResult.Equal Then
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

    Public Shared Function EInvoice_Cancellation(ByVal strDocNo As String, ByVal strIRNNo As String, ByVal strLocation As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim isSaved As Boolean = True
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Code not found to Post")
            End If

            Dim strtoken As String = ClsEInvoiceOFAPIs.IsGenerateAuthTokenNo_Required(objCommonVar.CurrentCompanyCode, strLocation, trans)
            If clsCommon.myLen(strtoken) > 0 Then

                Dim objResult As Object = ClsEInvoiceOFAPIs.CancelIRN_withInvoiceData(objCommonVar.CurrentCompanyCode, strtoken, strIRNNo, strLocation, trans)
                If objResult IsNot Nothing Then
                    Return True
                Else
                    Return False
                End If
            Else
                Return False
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

End Class
Public Class clsEInvoiceCancelDetails
    Public Property Irn As String = String.Empty
    Public Property CnlRsn As String = String.Empty
    Public Property CnlRem As String = String.Empty

End Class
Public Class clsEInvoiceCancelDetails_ClearTax
    Public Property irn As String = String.Empty
    Public Property CnlRsn As String = String.Empty
    Public Property CnlRem As String = String.Empty

End Class

Public Class clsEInvoiceDetails
    Public Property Version As String = String.Empty
    Public Property TranDtls As New Dictionary(Of String, String)
    Public Property DocDtls As New Dictionary(Of String, String)
    Public Property SellerDtls As New Dictionary(Of String, Object)
    Public Property BuyerDtls As New Dictionary(Of String, Object)
    Public Property ItemList As List(Of ClsItemList) = Nothing
    Public Property ValDtls As New Dictionary(Of String, Double)
End Class

Public Class clsEInvoiceDetailsWithEWayBill
    Public Property Version As String = String.Empty
    Public Property TranDtls As New Dictionary(Of String, String)
    Public Property DocDtls As New Dictionary(Of String, String)
    Public Property SellerDtls As New Dictionary(Of String, Object)
    Public Property BuyerDtls As New Dictionary(Of String, Object)
    Public Property ItemList As List(Of ClsItemList) = Nothing
    Public Property ValDtls As New Dictionary(Of String, Double)
    Public Property EwbDtls As New Dictionary(Of String, Object)
End Class
Public Class ClsItemList
    Public Property SlNo As String = String.Empty
    Public Property IsServc As String = String.Empty
    Public Property PrdDesc As String = String.Empty
    Public Property HsnCd As String = String.Empty
    Public Property Qty As Double = 0
    Public Property Unit As String = String.Empty
    Public Property UnitPrice As Double = 0
    Public Property TotAmt As Double = 0
    Public Property Discount As Double = 0
    Public Property AssAmt As Double = 0
    Public Property GstRt As Double = 0
    Public Property SgstAmt As Double = 0
    Public Property IgstAmt As Double = 0
    Public Property CgstAmt As Double = 0
    Public Property OthChrg As Double = 0
    Public Property TotItemVal As Double = 0

End Class




Public Class clsWayBillOnly
    Public Property Irn As String
    Public Property Distance As Integer
    Public Property TransMode As String
    Public Property TransId As String
    Public Property TransName As String
    Public Property TransDocDt As String
    Public Property TransDocNo As String
    Public Property VehNo As String
    Public Property VehType As String
End Class



