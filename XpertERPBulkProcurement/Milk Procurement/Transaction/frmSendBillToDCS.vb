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
            Dim qry As String = "select TSPL_PAYMENT_PROCESS_DETAIL.Doc_No,TSPL_PAYMENT_PROCESS_HEAD.From_Date,TSPL_PAYMENT_PROCESS_HEAD.To_Date,TSPL_PAYMENT_PROCESS_DETAIL.VSP_CODE,TSPL_PAYMENT_PROCESS_DETAIL.Milk_Purchase_Invoice_No from TSPL_PAYMENT_PROCESS_DETAIL 
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
                    'SaveFile(PDFPath, clsCommon.myCstr(dr("VSP_CODE")))
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
                Next
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        clsERPFuncationality.closeForm(Me)
    End Sub

    'Private Sub SaveFile(ByVal FilePath As String, ByVal Vendor_Code As String)
    '    If Vendor_Code IsNot Nothing AndAlso clsCommon.myLen(Vendor_Code) > 0 Then
    '        Dim Qry As String = "Select Email from TSPL_VENDOR_MASTER where Email is Not Null And Email!='' and Vendor_Code='" + Vendor_Code + "'"
    '        Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
    '        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
    '            Dim obj As New clsSendBillToDCS()
    '            obj.Email_ID = clsCommon.myCstr(dt.Rows())
    '            clsSendBillToDCS.SaveData(Form_ID, obj, Nothing)
    '        End If
    '    End If
    'End Sub

End Class