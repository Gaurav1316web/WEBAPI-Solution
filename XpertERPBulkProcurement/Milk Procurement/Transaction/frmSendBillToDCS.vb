Imports System.Data.SqlClient
Imports common
Imports System.IO
Imports System.Net
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports System.Collections.Specialized
Public Class frmSendBillToDCS
#Region "Variables"
    Dim arrFilePath As List(Of String) = Nothing
#End Region
    Private Sub fndPaymentProcessDocNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndPaymentProcessDocNo._MYValidating
        fndPaymentProcessDocNo.Value = clsPaymentProcessHead.getFinder("FarmType='PP' And isPosted=1", fndPaymentProcessDocNo.Value, isButtonClicked)
    End Sub

    Private Sub RadButton2_Click(sender As Object, e As EventArgs) Handles RadButton2.Click
        fndPaymentProcessDocNo.Value = Nothing
    End Sub

    Private Sub btnPrintBillMobUser_Click(sender As Object, e As EventArgs) Handles btnPrintBillMobUser.Click
        Try
            Dim qry As String = "select TSPL_PAYMENT_PROCESS_DETAIL.Doc_No,TSPL_PAYMENT_PROCESS_HEAD.Doc_Date,TSPL_PAYMENT_PROCESS_HEAD.From_Date,TSPL_PAYMENT_PROCESS_HEAD.To_Date,
                                 TSPL_PAYMENT_PROCESS_DETAIL.VSP_CODE,TSPL_PAYMENT_PROCESS_DETAIL.VSP_NAME,TSPL_PAYMENT_PROCESS_DETAIL.VLC_CODE_Uploader,TSPL_PAYMENT_PROCESS_DETAIL.Milk_Purchase_Invoice_No 
                                 from TSPL_PAYMENT_PROCESS_DETAIL 
                                 left outer join TSPL_MILK_PURCHASE_INVOICE_HEAD on TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE=TSPL_PAYMENT_PROCESS_DETAIL.Milk_Purchase_Invoice_No
                                 left outer join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_HEAD.Doc_No=TSPL_PAYMENT_PROCESS_DETAIL.Doc_No
                                 where TSPL_PAYMENT_PROCESS_DETAIL.Doc_No='" + fndPaymentProcessDocNo.Value + "' and TSPL_MILK_PURCHASE_INVOICE_HEAD.FILE_INFO is null"

            'Dim qry As String = "select top 2 TSPL_PAYMENT_PROCESS_DETAIL.Doc_No,TSPL_PAYMENT_PROCESS_HEAD.From_Date,TSPL_PAYMENT_PROCESS_HEAD.To_Date,TSPL_PAYMENT_PROCESS_DETAIL.VSP_CODE,TSPL_PAYMENT_PROCESS_DETAIL.Milk_Purchase_Invoice_No from TSPL_PAYMENT_PROCESS_DETAIL 
            'left outer join TSPL_MILK_PURCHASE_INVOICE_HEAD on TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE=TSPL_PAYMENT_PROCESS_DETAIL.Milk_Purchase_Invoice_No
            'left outer join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_HEAD.Doc_No=TSPL_PAYMENT_PROCESS_DETAIL.Doc_No
            'where TSPL_PAYMENT_PROCESS_DETAIL.Doc_No='PPR/2324/000032' and TSPL_MILK_PURCHASE_INVOICE_HEAD.FILE_INFO is null"

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    Dim PDFPath As String = clsPaymentProcessHead.Load_Report_Paymnet_RCDF("'" + clsCommon.myCstr(clsCommon.myCstr(dr("Doc_No"))) + "'", clsCommon.myCDate(clsCommon.myCstr(dr("From_Date"))), clsCommon.myCDate(clsCommon.myCstr(dr("To_Date"))), "", "'" + clsCommon.myCstr(dr("VSP_CODE")) + "'", "", "", "", False, True)
                    Dim Str As String = clsAttachDocument.UploadWithHttpRequest(PDFPath, Path.GetFileName(PDFPath))
                    Dim jObj As JObject = JObject.Parse(Str)
                    Dim ArrJ As JArray = Nothing
                    If clsCommon.CompairString(clsCommon.myCstr(jObj.SelectToken("result")), "true") = CompairStringResult.Equal Then
                        ArrJ = JArray.Parse(clsCommon.myCstr(jObj.SelectToken("data")))
                        If clsCommon.myCDecimal(ArrJ(0).SelectToken("Result")) > 0 Then
                            Dim FileNo As Integer = clsCommon.myCDecimal(ArrJ(0).SelectToken("Result"))
                            If FileNo > 0 Then
                                Str = " UPDATE TSPL_MILK_PURCHASE_INVOICE_HEAD set FILE_INFO=" + clsCommon.myCstr(FileNo) + " where DOC_CODE='" + clsCommon.myCstr(dr("Milk_Purchase_Invoice_No")) + "'"
                                clsDBFuncationality.ExecuteNonQuery(Str)
                            End If
                        Else
                            Throw New Exception(ArrJ(0).SelectToken("Message"))
                        End If
                    Else
                        ArrJ = JArray.Parse(clsCommon.myCstr(jObj.SelectToken("data")))
                        Throw New Exception(ArrJ(0).SelectToken("Message"))
                    End If
                    SaveFile(PDFPath, clsCommon.myCstr(dr("VSP_CODE")), clsCommon.myCstr(dr("Doc_No")), clsCommon.myCDate(dr("Doc_Date")), clsCommon.myCstr(dr("VSP_CODE")), clsCommon.myCstr(dr("VSP_NAME")), clsCommon.myCstr(dr("VLC_CODE_Uploader")), clsCommon.myCDate(dr("From_Date")), clsCommon.myCDate(dr("To_Date")))
                Next
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        clsERPFuncationality.closeForm(Me)
    End Sub

    Private Sub SaveFile(ByVal FilePath As String, ByVal Vendor_Code As String, ByVal Documnet_No As String, ByVal Document_Date As DateTime, ByVal VSP_Code As String, ByVal VSP_Name As String, ByVal VLC_Uploader_Code As String, ByVal FromDate As Date, ByVal ToDate As Date)
        Try
            If Vendor_Code IsNot Nothing AndAlso clsCommon.myLen(Vendor_Code) > 0 Then
                Dim dtContent As DataTable = clsDBFuncationality.GetDataTable("SELECT SMS_Text,Email_Text,Email_subject from TSPL_ES_Content where Form_ID='" + clsUserMgtCode.frmSendBillToDCS + "'", Nothing)
                Dim Qry As String = "Select Email from TSPL_VENDOR_MASTER where Vendor_Code='" + Vendor_Code + "'"
                Dim arrMailID As List(Of String) = New List(Of String)()
                arrMailID.Add(clsCommon.myCstr(clsDBFuncationality.getSingleValue(Qry)))
                If clsCommon.myLen(dtContent.Rows(0)("Email_Text")) > 0 AndAlso (arrMailID IsNot Nothing AndAlso arrMailID.Count > 0) Then
                    Dim objEmailH As New clsEMailHead()
                    'objEmailH.arrEMail = New List(Of String)()
                    objEmailH.arrEMail = arrMailID
                    objEmailH.Email_Subject = clsCommon.myCstr(dtContent.Rows(0)("Email_subject"))
                    objEmailH.Email_Text = clsCommon.myCstr(dtContent.Rows(0)("Email_Text"))
                    objEmailH.Email_Text = objEmailH.Email_Text.Replace(clsEmailSMSConstants.DOC_NO, Documnet_No)
                    objEmailH.Email_Text = objEmailH.Email_Text.Replace(clsEmailSMSConstants.DOC_Date, clsCommon.GetPrintDate(Document_Date, "dd/MMM/yyyy"))
                    objEmailH.Email_Text = objEmailH.Email_Text.Replace(clsEmailSMSConstants.VLCCode, VSP_Code)
                    objEmailH.Email_Text = objEmailH.Email_Text.Replace(clsEmailSMSConstants.VLCName, VSP_Name)
                    objEmailH.Email_Text = objEmailH.Email_Text.Replace(clsEmailSMSConstants.VLCUploaderCode, VLC_Uploader_Code)
                    objEmailH.Email_Text = objEmailH.Email_Text.Replace(clsEmailSMSConstants.FromDate, clsCommon.GetPrintDate(FromDate, "dd/MMM/yyyy"))
                    objEmailH.Email_Text = objEmailH.Email_Text.Replace(clsEmailSMSConstants.ToDate, clsCommon.GetPrintDate(ToDate, "dd/MMM/yyyy"))
                    objEmailH.Attachment_1_Path = FilePath
                    objEmailH.SaveData(clsUserMgtCode.frmSendBillToDCS, objEmailH, Nothing)
                    objEmailH = Nothing
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

End Class