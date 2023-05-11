
'--------Created By Richa 07/08/2014 Against Ticket No BM00000003249
Imports common
Imports System.Data.SqlClient


Public Class ClsInvoiceBulkSale
#Region "Variable"
    Public ActualTCSBaseAmount As Double = 0
    Public ChangedTCSBaseAmount As Double = 0
    Public Document_No As String = Nothing
    Public Document_Date As Date
    Public Customer_Code As String = Nothing
    Public Location_Code As String = Nothing
    Public InvoiceAgainst As String = Nothing
    Public Tax_Calculation_Type As EnumTaxCalucationType
    Public fromdate As Date?
    Public todate As Date?
    Public Total_Amt As Double = 0
    Public RoundOffAmount As Double = 0
    Public Posted As Integer = 0
    Public Posting_Date As Date?
    Public Shared aLoc As String = Nothing
    Public IsUploader As Integer = 0
    Public arrInvoiceDetailBulkSale As List(Of ClsInvoiceDetailBulkSale) = Nothing
    Public Comments As String = Nothing
    Public EWayBillNo As String = Nothing
    Public EWayBillDate As Date?
    Public Electronic_Ref_No As String = Nothing
    Public Tax_Group As String = Nothing
    Public TaxGroupName As String = Nothing 'Not a table field
    Public TAX1 As String = Nothing
    Public TAX1_Rate As Double = 0
    Public TAX1_Base_Amt As Double = 0
    Public TAX1_Amt As Double = 0
    Public TAX2 As String = Nothing
    Public TAX2_Rate As Double = 0
    Public TAX2_Base_Amt As Double = 0
    Public TAX2_Amt As Double = 0
    Public TAX3 As String = Nothing
    Public TAX3_Rate As Double = 0
    Public TAX3_Base_Amt As Double = 0
    Public TAX3_Amt As Double = 0
    Public TAX4 As String = Nothing
    Public TAX4_Rate As Double = 0
    Public TAX4_Base_Amt As Double = 0
    Public TAX4_Amt As Double = 0
    Public TAX5 As String = Nothing
    Public TAX5_Rate As Double = 0
    Public TAX5_Base_Amt As Double = 0
    Public TAX5_Amt As Double = 0
    Public Total_Tax_Amt As Double = 0
    Public Document_Amount As Double = 0

#End Region

    '----------------Code For Get Finder--------------------------------------------------------------------'
    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = "Select TSPL_INVOICE_MASTER_BULKSAlE.Document_No as Code ,TSPL_INVOICE_MASTER_BULKSAlE.Document_Date as Date from TSPL_INVOICE_MASTER_BULKSAlE  "
        str = clsCommon.ShowSelectForm("InvoiceBulkSale", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str
    End Function
    Public Shared Function SaveData(ByVal obj As ClsInvoiceBulkSale, ByVal isNewEntry As Boolean) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            SaveData(obj, isNewEntry, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function SaveData(ByVal obj As ClsInvoiceBulkSale, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim qry As String = String.Empty
        Dim isSaved As Boolean = True
        ' Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If Not isNewEntry Then
                clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, obj.Document_No, "TSPL_INVOICE_MASTER_BULKSAlE", "Document_No", "TSPL_INVOICE_DETAIL_BulKSALE", "Document_No", trans)
            End If
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleBulkSale, clsUserMgtCode.FrmInvoiceBulkSale, obj.Location_Code, obj.Document_Date, trans)
            qry = "delete from TSPL_INVOICE_DETAIL_BulKSALE where Document_No='" + obj.Document_No + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            If isNewEntry Then
                If clsCommon.myLen(obj.Document_No) <= 0 Then
                    Dim GSTStatus As Boolean = clsERPFuncationality.GetGSTStatus(obj.Document_Date)
                    If GSTStatus Then
                        If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.CreateCommonSeriesLocationwiseForAllSale, clsFixedParameterCode.CreateCommonSeriesLocationwiseForAllSale, trans)) = 0 Then
                            If clsCommon.CompairString(obj.InvoiceAgainst, "Against Dispatch") = CompairStringResult.Equal Then
                                obj.Document_No = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.InvoiceBulkSale, clsDocTransactionType.BULKMilkSale, obj.Location_Code)
                            Else
                                obj.Document_No = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.InvoiceBulkSale, clsDocTransactionType.BULKMilkSaleTrade, obj.Location_Code)
                            End If
                        Else
                            obj.Document_No = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.CommonSaleSeries, clsDocTransactionType.GSTBillofSupply, obj.Location_Code)
                        End If
                    Else
                        obj.Document_No = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.InvoiceBulkSale, clsDocTransactionType.NA, obj.Location_Code)
                    End If
                End If
            End If
            Dim DateTime As String = clsFixedParameter.GetData(clsFixedParameterType.AllowToSaveTimeWithDocumentDate, clsFixedParameterCode.AllowToSaveTimeWithDocumentDate, trans)
            Dim coll As New Hashtable()
            If DateTime = "1" Then
                clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy hh:mm tt"))
            Else
                clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy"))
            End If

            clsCommon.AddColumnsForChange(coll, "Customer_Code", obj.Customer_Code)
            clsCommon.AddColumnsForChange(coll, "Location_Code", obj.Location_Code)
            clsCommon.AddColumnsForChange(coll, "InvoiceAgainst", obj.InvoiceAgainst)
            clsCommon.AddColumnsForChange(coll, "Total_Amt", obj.Total_Amt)
            clsCommon.AddColumnsForChange(coll, "RoundOffAmount", obj.RoundOffAmount)
            clsCommon.AddColumnsForChange(coll, "IsUploader", obj.IsUploader)
            clsCommon.AddColumnsForChange(coll, "EWayBillNo", obj.EWayBillNo, True)
            clsCommon.AddColumnsForChange(coll, "Electronic_Ref_No", obj.Electronic_Ref_No, True)

            '=====shivani
            clsCommon.AddColumnsForChange(coll, "Comments", obj.Comments)
            '====
            If clsCommon.myLen(obj.fromdate) > 0 Then
                clsCommon.AddColumnsForChange(coll, "From_date", clsCommon.GetPrintDate(obj.fromdate, "dd/MMM/yyyy"))
            Else
                clsCommon.AddColumnsForChange(coll, "From_date", Nothing, True)
            End If
            If clsCommon.myLen(obj.todate) > 0 Then
                clsCommon.AddColumnsForChange(coll, "To_date", clsCommon.GetPrintDate(obj.todate, "dd/MMM/yyyy"))
            Else
                clsCommon.AddColumnsForChange(coll, "To_date", Nothing, True)
            End If
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Tax_Calculation_Type", IIf(obj.Tax_Calculation_Type = EnumTaxCalucationType.Automatic, 0, 1))
            clsCommon.AddColumnsForChange(coll, "Tax_Group", obj.Tax_Group)
            clsCommon.AddColumnsForChange(coll, "TAX1", obj.TAX1)
            clsCommon.AddColumnsForChange(coll, "TAX1_Rate", obj.TAX1_Rate)
            clsCommon.AddColumnsForChange(coll, "TAX1_Base_Amt", obj.TAX1_Base_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX1_Amt", obj.TAX1_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX2", obj.TAX2)
            clsCommon.AddColumnsForChange(coll, "TAX2_Rate", obj.TAX2_Rate)
            clsCommon.AddColumnsForChange(coll, "TAX2_Base_Amt", obj.TAX2_Base_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX2_Amt", obj.TAX2_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX3", obj.TAX3)
            clsCommon.AddColumnsForChange(coll, "TAX3_Rate", obj.TAX3_Rate)
            clsCommon.AddColumnsForChange(coll, "TAX3_Base_Amt", obj.TAX3_Base_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX3_Amt", obj.TAX3_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX4", obj.TAX4)
            clsCommon.AddColumnsForChange(coll, "TAX4_Rate", obj.TAX4_Rate)
            clsCommon.AddColumnsForChange(coll, "TAX4_Base_Amt", obj.TAX4_Base_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX4_Amt", obj.TAX4_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX5", obj.TAX5)
            clsCommon.AddColumnsForChange(coll, "TAX5_Rate", obj.TAX5_Rate)
            clsCommon.AddColumnsForChange(coll, "TAX5_Base_Amt", obj.TAX5_Base_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX5_Amt", obj.TAX5_Amt)
            clsCommon.AddColumnsForChange(coll, "Total_Tax_Amt", obj.Total_Tax_Amt)
            clsCommon.AddColumnsForChange(coll, "Document_Amount", obj.Document_Amount)
            clsCommon.AddColumnsForChange(coll, "ActualTCSBaseAmount", obj.ActualTCSBaseAmount)
            clsCommon.AddColumnsForChange(coll, "ChangedTCSBaseAmount", obj.ChangedTCSBaseAmount)

            If isNewEntry Then

                ''richa 05/011/2015 BM00000008340
                Dim strdispatchCode As String = String.Empty
                Dim arrObjdetail As List(Of ClsInvoiceDetailBulkSale) = obj.arrInvoiceDetailBulkSale
                If obj.arrInvoiceDetailBulkSale.Count > 1 Then
                    For Each obj1 As ClsInvoiceDetailBulkSale In arrObjdetail
                        strdispatchCode = strdispatchCode + "'" + obj1.Dispatch_Code + "',"
                    Next
                    strdispatchCode = strdispatchCode.Substring(0, strdispatchCode.Length - 1)
                Else
                    For Each obj1 As ClsInvoiceDetailBulkSale In arrObjdetail
                        strdispatchCode = "'" + obj1.Dispatch_Code + "'"
                    Next
                End If

                ''-----------------

                If clsDBFuncationality.getSingleValue("select count(*) from TSPL_INVOICE_DETAIL_BULKSALE  where Dispatch_Code in (" & strdispatchCode & ") ", trans) < 1 Then
                    clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                    clsCommon.AddColumnsForChange(coll, "Document_No", obj.Document_No)
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_INVOICE_MASTER_BULKSAlE", OMInsertOrUpdate.Insert, "", trans)
                Else
                    Throw New Exception("Document already created for Dispatch No " & strdispatchCode & "")
                End If
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_INVOICE_MASTER_BULKSAlE", OMInsertOrUpdate.Update, "TSPL_INVOICE_MASTER_BULKSAlE.Document_No='" + obj.Document_No + "'", trans)
            End If
            isSaved = isSaved AndAlso ClsInvoiceDetailBulkSale.saveData(obj.arrInvoiceDetailBulkSale, obj.Document_No, trans)

            ' trans.Commit()
        Catch err As Exception
            'trans.Rollback()
            Throw New Exception(err.Message)
        Finally
            qry = Nothing
            obj = Nothing
        End Try
        Return True
    End Function
    Public Shared Function UpdateAfterPosting(ByVal obj As ClsInvoiceBulkSale, ByVal trans As SqlTransaction) As Boolean
        Try
            If obj IsNot Nothing And clsCommon.myLen(obj.Document_No) > 0 Then
                Dim coll As New Hashtable()

                clsCommon.AddColumnsForChange(coll, "EWayBillNo", obj.EWayBillNo)
                If clsCommon.myLen(obj.EWayBillDate) > 0 Then
                    clsCommon.AddColumnsForChange(coll, "EWayBillDate", clsCommon.GetPrintDate(obj.EWayBillDate, "dd/MMM/yyyy"))
                Else
                    clsCommon.AddColumnsForChange(coll, "EWayBillDate", Nothing, True)
                End If
                clsCommon.AddColumnsForChange(coll, "Electronic_Ref_No", obj.Electronic_Ref_No)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_INVOICE_MASTER_BULKSALE", OMInsertOrUpdate.Update, "TSPL_INVOICE_MASTER_BULKSALE.Document_No='" + obj.Document_No + "'", trans)


                If clsCommon.CompairString(obj.InvoiceAgainst, "Against Dispatch") = CompairStringResult.Equal Then
                    If clsCommon.myLen(obj.EWayBillDate) > 0 Then
                        clsDBFuncationality.getSingleValue("Update TSPL_Dispatch_BulkSale set EWayBillNo='" & clsCommon.myCstr(obj.EWayBillNo) & "',EWayBillDate='" & clsCommon.GetPrintDate(obj.EWayBillDate, "dd/MMM/yyyy") & "' where Document_No in (Select Dispatch_Code  from TSPL_INVOICE_DETAIL_BULKSALE where Document_No='" & clsCommon.myCstr(obj.Document_No) & "')", trans)
                        clsDBFuncationality.getSingleValue("Update TSPL_Dispatch_BulkSale_History set EWayBillNo='" & clsCommon.myCstr(obj.EWayBillNo) & "',EWayBillDate='" & clsCommon.GetPrintDate(obj.EWayBillDate, "dd/MMM/yyyy") & "' where Document_No in (Select Dispatch_Code  from TSPL_INVOICE_DETAIL_BULKSALE where Document_No='" & clsCommon.myCstr(obj.Document_No) & "')", trans)
                    Else
                        clsDBFuncationality.getSingleValue("Update TSPL_Dispatch_BulkSale set EWayBillNo='" & clsCommon.myCstr(obj.EWayBillNo) & "',EWayBillDate=NULL where Document_No in (Select Dispatch_Code  from TSPL_INVOICE_DETAIL_BULKSALE where Document_No='" & clsCommon.myCstr(obj.Document_No) & "')", trans)
                        clsDBFuncationality.getSingleValue("Update TSPL_Dispatch_BulkSale_History set EWayBillNo='" & clsCommon.myCstr(obj.EWayBillNo) & "',EWayBillDate=NULL where Document_No in (Select Dispatch_Code  from TSPL_INVOICE_DETAIL_BULKSALE where Document_No='" & clsCommon.myCstr(obj.Document_No) & "')", trans)
                    End If
                   
                Else
                    If clsCommon.myLen(obj.EWayBillDate) > 0 Then
                        clsDBFuncationality.getSingleValue("Update TSPL_Dispatch_BulkSale_Trade set EWayBillNo='" & clsCommon.myCstr(obj.EWayBillNo) & "',EWayBillDate='" & clsCommon.GetPrintDate(obj.EWayBillDate, "dd/MMM/yyyy") & "' where Document_No in (Select Dispatch_Code  from TSPL_INVOICE_DETAIL_BULKSALE where Document_No='" & clsCommon.myCstr(obj.Document_No) & "')", trans)
                    Else
                        clsDBFuncationality.getSingleValue("Update TSPL_Dispatch_BulkSale_Trade set EWayBillNo='" & clsCommon.myCstr(obj.EWayBillNo) & "',EWayBillDate=NULL where Document_No in (Select Dispatch_Code  from TSPL_INVOICE_DETAIL_BULKSALE where Document_No='" & clsCommon.myCstr(obj.Document_No) & "')", trans)
                    End If

                End If

            End If
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function




    Public Shared Function SaveDataForHistory(ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Try
            Dim dt As DataTable = clsDBFuncationality.GetDataTable("select Document_Date,Location_Code from TSPL_INVOICE_MASTER_BULKSAlE where Document_No='" + strCode + "'", trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleBulkSale, clsUserMgtCode.FrmCreateAutoInvoiceBS, clsCommon.myCstr(dt.Rows(0)("Location_Code")), clsCommon.myCDate(dt.Rows(0)("Document_Date")), trans)

            End If
            Dim strHistoryDate As DateTime = clsCommon.GETSERVERDATE(trans)
            '' TSPL_INVOICE_MASTER_BULKSALE_History
            Dim qry As String = String.Empty
            Dim strInvColumns As String = clsERPFuncationality.GetTableColumnNameForQry("TSPL_INVOICE_MASTER_BULKSALE", trans)
            qry = "INSERT INTO TSPL_INVOICE_MASTER_BULKSALE_History (" + strInvColumns + ",History_Date) SELECT  " + strInvColumns + ",'" + clsCommon.GetPrintDate(strHistoryDate, "dd/MMM/yyyy hh:mm tt") + "' FROM TSPL_INVOICE_MASTER_BULKSALE WHERE Document_No='" + clsCommon.myCstr(strCode) + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            '' Detail
            ''-------------- TSPL_INVOICE_DETAIL_BULKSALE_History)
            Dim strDetailColumns As String = clsERPFuncationality.GetTableColumnNameForQry("TSPL_INVOICE_DETAIL_BULKSALE", trans)
            qry = "INSERT INTO TSPL_INVOICE_DETAIL_BULKSALE_History(" + strDetailColumns + ",History_Date) SELECT " + strDetailColumns + ",'" + clsCommon.GetPrintDate(strHistoryDate, "dd/MMM/yyyy hh:mm tt") + "' FROM TSPL_INVOICE_DETAIL_BULKSALE WHERE Document_No='" + clsCommon.myCstr(strCode) + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            '' TSPL_Customer_Invoice_Head_History
            Dim strCustInvColumns As String = clsERPFuncationality.GetTableColumnNameForQry("TSPL_Customer_Invoice_Head", trans)
            qry = "INSERT INTO TSPL_Customer_Invoice_Head_History (" + strCustInvColumns + ",History_Date) SELECT  " + strCustInvColumns + ",'" + clsCommon.GetPrintDate(strHistoryDate, "dd/MMM/yyyy hh:mm tt") + "' FROM TSPL_Customer_Invoice_Head WHERE Against_Sale_No='" + clsCommon.myCstr(strCode) + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            '' Detail
            ''-------------- TSPL_Customer_Invoice_Detail_History)
            Dim strCustDetailColumns As String = clsERPFuncationality.GetTableColumnNameForQry("TSPL_Customer_Invoice_Detail", trans)
            qry = "INSERT INTO TSPL_Customer_Invoice_Detail_History(" + strCustDetailColumns + ",History_Date) SELECT " + strCustDetailColumns + ",'" + clsCommon.GetPrintDate(strHistoryDate, "dd/MMM/yyyy hh:mm tt") + "' FROM TSPL_Customer_Invoice_Detail WHERE Document_No=(Select Document_No from TSPL_Customer_Invoice_Head where Against_Sale_No ='" & clsCommon.myCstr(strCode) & "')"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            '' For Journal Entry ERO/11/01/19-000467
            Dim VoucherNo As String = clsDBFuncationality.getSingleValue("select Voucher_No from TSPL_JOURNAL_MASTER where source_code='AR-IN' and source_doc_no=(Select Document_No from TSPL_Customer_Invoice_Head where Against_Sale_No ='" & clsCommon.myCstr(strCode) & "')", trans)
            If clsCommon.myLen(VoucherNo) > 0 Then
                clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, VoucherNo, "TSPL_JOURNAL_MASTER", "Voucher_No", "TSPL_JOURNAL_DETAILS", "Voucher_No", trans)
                qry = "delete from TSPL_JOURNAL_DETAILS where Voucher_No ='" + VoucherNo + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
                qry = "delete from TSPL_JOURNAL_MASTER where Voucher_No ='" + VoucherNo + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
            End If

            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strCode, "TSPL_Customer_Invoice_Head", "Against_Sale_No", trans)

            qry = "Delete from TSPL_Customer_Invoice_Detail where Document_No =(Select Document_No from TSPL_Customer_Invoice_Head where Against_Sale_No ='" & clsCommon.myCstr(strCode) & "')"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "Delete from TSPL_Customer_Invoice_Head where Against_Sale_No ='" & clsCommon.myCstr(strCode) & "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "Update TSPL_INVOICE_MASTER_BULKSALE set Posted = 0 where Document_No='" & clsCommon.myCstr(strCode) & "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            ''
            ''
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strCode, "TSPL_INVOICE_MASTER_BULKSAlE", "Document_No", trans)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal arrLoc As String, ByVal NavType As NavigatorType) As ClsInvoiceBulkSale
        Return GetData(strCode, arrLoc, NavType, Nothing)
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal arrLoc As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As ClsInvoiceBulkSale
        ''richa agarwal 14/10/2014
        'Dim strfLocation As String = ""
        'Dim strvirlocation As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select STUFF((Select distinct ','+''+Location_Code  from TSPL_INVOICE_MASTER_BULKSALE for xml path ('')), 1,1,'') as Test from TSPL_INVOICE_MASTER_BULKSALE where InvoiceAgainst ='Against Dispatch Trade' group by InvoiceAgainst", trans))

        ' ''===================================

        'If clsCommon.myLen(arrLoc) <= 0 Then
        '    If clsCommon.myLen(FrmInvoiceBulkSale.aLoc) > 0 Then
        '        arrLoc = FrmInvoiceBulkSale.aLoc
        '        If clsCommon.myLen(strvirlocation) > 0 Then
        '            strvirlocation = strvirlocation.Replace(",", "','")
        '            If clsCommon.myLen(arrLoc) > 0 Then
        '                strfLocation = arrLoc + ",'" + strvirlocation + "'"
        '                arrLoc = strfLocation
        '            Else
        '                strfLocation = "'" + strvirlocation + "'"
        '                arrLoc = strfLocation
        '            End If
        '        End If

        '    ElseIf clsCommon.myLen(FrmDispatchBulkSale.Alocation) > 0 Then
        '        arrLoc = FrmDispatchBulkSale.Alocation
        '        If clsCommon.myLen(strvirlocation) > 0 Then
        '            strvirlocation = strvirlocation.Replace(",", "','")
        '            If clsCommon.myLen(arrLoc) > 0 Then
        '                strfLocation = arrLoc + ",'" + strvirlocation + "'"
        '                arrLoc = strfLocation
        '            Else
        '                strfLocation = "'" + strvirlocation + "'"
        '                arrLoc = strfLocation
        '            End If
        '        End If
        '        ''richa 18/11/2014
        '    Else
        '        arrLoc = clsDBFuncationality.getSingleValue("Select Location_Code from TSPL_INVOICE_MASTER_BULKSALE where Document_No='" & strCode & "'", trans)
        '        arrLoc = "'" + arrLoc + "'"
        '        If clsCommon.myLen(strvirlocation) > 0 Then
        '            strvirlocation = strvirlocation.Replace(",", "','")
        '            If clsCommon.myLen(arrLoc) > 0 Then
        '                strfLocation = arrLoc + ",'" + strvirlocation + "'"
        '                arrLoc = strfLocation
        '            Else
        '                strfLocation = "'" + strvirlocation + "'"
        '                arrLoc = strfLocation
        '            End If
        '        End If
        '    End If
        'End If

        'If clsCommon.myLen(arrLoc) <= 0 Then
        '    If clsCommon.myLen(FrmInvoiceBulkSale.aLoc) > 0 Then
        '        arrLoc = FrmInvoiceBulkSale.aLoc
        '    ElseIf clsCommon.myLen(FrmDispatchBulkSale.Alocation) > 0 Then
        '        arrLoc = FrmDispatchBulkSale.Alocation
        '    End If
        'End If
        Dim obj As ClsInvoiceBulkSale = Nothing
        Dim Arr As List(Of ClsInvoiceBulkSale) = Nothing
        Dim qry As String = "Select TSPL_INVOICE_MASTER_BULKSAlE.ChangedTCSBaseAmount,TSPL_INVOICE_MASTER_BULKSAlE.ActualTCSBaseAmount,TSPL_INVOICE_MASTER_BULKSAlE.EWayBillDate,TSPL_INVOICE_MASTER_BULKSAlE.EWayBillNo,TSPL_INVOICE_MASTER_BULKSAlE.Electronic_Ref_No,TSPL_INVOICE_MASTER_BULKSAlE.Document_No,isnull(TSPL_INVOICE_MASTER_BULKSAlE.To_date,GetDate()) as To_date,isnull(TSPL_INVOICE_MASTER_BULKSAlE.From_date,DATEADD(MONTH,-1,GETDATE())) as From_date,TSPL_INVOICE_MASTER_BULKSAlE.Document_Date,TSPL_INVOICE_MASTER_BULKSAlE.Location_Code,TSPL_INVOICE_MASTER_BULKSAlE.Customer_Code,TSPL_INVOICE_MASTER_BULKSAlE.Total_Amt,TSPL_INVOICE_MASTER_BULKSAlE.Posted,TSPL_INVOICE_MASTER_BULKSAlE.RoundOffAmount,TSPL_INVOICE_MASTER_BULKSAlE.InvoiceAgainst,TSPL_INVOICE_MASTER_BULKSAlE.comments,TSPL_INVOICE_MASTER_BULKSAlE.Tax_Group,TSPL_INVOICE_MASTER_BULKSAlE.TAX1,TSPL_INVOICE_MASTER_BULKSAlE.TAX1_Rate,TSPL_INVOICE_MASTER_BULKSAlE.TAX1_Amt,TSPL_INVOICE_MASTER_BULKSAlE.TAX1_Base_Amt,TSPL_INVOICE_MASTER_BULKSAlE.TAX2,TSPL_INVOICE_MASTER_BULKSAlE.TAX2_Rate,TSPL_INVOICE_MASTER_BULKSAlE.TAX2_Amt,TSPL_INVOICE_MASTER_BULKSAlE.TAX2_Base_Amt,TSPL_INVOICE_MASTER_BULKSAlE.TAX3,TSPL_INVOICE_MASTER_BULKSAlE.TAX3_Rate,TSPL_INVOICE_MASTER_BULKSAlE.TAX3_Amt,TSPL_INVOICE_MASTER_BULKSAlE.TAX3_Base_Amt,TSPL_INVOICE_MASTER_BULKSAlE.TAX4,TSPL_INVOICE_MASTER_BULKSAlE.TAX4_Rate,TSPL_INVOICE_MASTER_BULKSAlE.TAX4_Amt,TSPL_INVOICE_MASTER_BULKSAlE.TAX4_Base_Amt,TSPL_INVOICE_MASTER_BULKSAlE.TAX5,TSPL_INVOICE_MASTER_BULKSAlE.TAX5_Rate,TSPL_INVOICE_MASTER_BULKSAlE.TAX5_Amt,TSPL_INVOICE_MASTER_BULKSAlE.TAX5_Base_Amt,TSPL_INVOICE_MASTER_BULKSAlE.Total_Tax_Amt,TSPL_INVOICE_MASTER_BULKSAlE.Document_Amount,TSPL_INVOICE_MASTER_BULKSAlE.Tax_Calculation_Type from TSPL_INVOICE_MASTER_BULKSAlE where 2=2  "
        If clsCommon.myLen(arrLoc) > 0 Then
            qry += " and TSPL_INVOICE_MASTER_BULKSAlE.Location_Code in (" + arrLoc + ") "
        End If
        Dim whrclas As String = ""
        If clsCommon.myLen(arrLoc) > 0 Then
            whrclas += " and TSPL_INVOICE_MASTER_BULKSAlE.Location_Code in (" + arrLoc + ")"
        End If
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_INVOICE_MASTER_BULKSAlE.Document_No = (select MIN(Document_No) from TSPL_INVOICE_MASTER_BULKSAlE WHERE 1=1 " + whrclas + "  )"
            Case NavigatorType.Last
                qry += " and TSPL_INVOICE_MASTER_BULKSAlE.Document_No = (select Max(Document_No) from TSPL_INVOICE_MASTER_BULKSAlE WHERE 1=1 " + whrclas + " )"
            Case NavigatorType.Current
                qry += " and TSPL_INVOICE_MASTER_BULKSAlE.Document_No ='" + strCode + "' "
            Case NavigatorType.Next
                qry += " and TSPL_INVOICE_MASTER_BULKSAlE.Document_No = (select Min(Document_No) from TSPL_INVOICE_MASTER_BULKSAlE where Document_No>'" + strCode + "' " + whrclas + " )"
            Case NavigatorType.Previous
                qry += " and TSPL_INVOICE_MASTER_BULKSAlE.Document_No = (select Max(Document_No) from TSPL_INVOICE_MASTER_BULKSAlE where Document_No<'" + strCode + "' " + whrclas + "  )"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New ClsInvoiceBulkSale()
            obj.Document_No = clsCommon.myCstr(dt.Rows(0)("Document_No"))
            obj.Document_Date = clsCommon.myCstr(dt.Rows(0)("Document_Date"))
            obj.Location_Code = clsCommon.myCstr(dt.Rows(0)("Location_Code"))
            obj.Customer_Code = clsCommon.myCstr(dt.Rows(0)("Customer_Code"))
            obj.EWayBillNo = clsCommon.myCstr(dt.Rows(0)("EWayBillNo"))
            obj.Electronic_Ref_No = clsCommon.myCstr(dt.Rows(0)("Electronic_Ref_No"))
            obj.Total_Amt = clsCommon.myCdbl(dt.Rows(0)("Total_Amt"))
            obj.Posted = clsCommon.myCdbl(dt.Rows(0)("Posted"))
            obj.RoundOffAmount = clsCommon.myCdbl(dt.Rows(0)("RoundOffAmount"))
            obj.InvoiceAgainst = clsCommon.myCstr(dt.Rows(0)("InvoiceAgainst"))
            obj.Comments = clsCommon.myCstr(dt.Rows(0)("Comments"))
            If clsCommon.CompairString(obj.InvoiceAgainst, "Against Dispatch") = CompairStringResult.Equal Then
                If clsCommon.myLen(dt.Rows(0)("From_date")) > 0 Then
                    obj.fromdate = clsCommon.myCDate(dt.Rows(0)("From_date"))
                    obj.todate = clsCommon.myCDate(dt.Rows(0)("To_date"))
                End If

            End If
            obj.ChangedTCSBaseAmount = clsCommon.myCdbl(dt.Rows(0)("ChangedTCSBaseAmount"))
            obj.ActualTCSBaseAmount = clsCommon.myCdbl(dt.Rows(0)("ActualTCSBaseAmount"))
            If dt.Rows(0)("EWayBillDate") IsNot DBNull.Value Then
                obj.EWayBillDate = clsCommon.myCDate(dt.Rows(0)("EWayBillDate"))
            End If
            obj.Tax_Calculation_Type = IIf(clsCommon.myCdbl(dt.Rows(0)("Tax_Calculation_Type")) = 0, EnumTaxCalucationType.Automatic, EnumTaxCalucationType.Mannual)
            obj.Tax_Group = clsCommon.myCstr(dt.Rows(0)("Tax_Group"))
            obj.TAX1 = clsCommon.myCstr(dt.Rows(0)("TAX1"))
            obj.TAX1_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX1_Rate"))
            obj.TAX1_Base_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX1_Base_Amt"))
            obj.TAX1_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX1_Amt"))
            obj.TAX2 = clsCommon.myCstr(dt.Rows(0)("TAX2"))
            obj.TAX2_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX2_Rate"))
            obj.TAX2_Base_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX2_Base_Amt"))
            obj.TAX2_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX2_Amt"))
            obj.TAX3 = clsCommon.myCstr(dt.Rows(0)("TAX3"))
            obj.TAX3_Base_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX3_Base_Amt"))
            obj.TAX3_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX3_Rate"))
            obj.TAX3_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX3_Amt"))
            obj.TAX4 = clsCommon.myCstr(dt.Rows(0)("TAX4"))
            obj.TAX4_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX4_Rate"))
            obj.TAX4_Base_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX4_Base_Amt"))
            obj.TAX4_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX4_Amt"))
            obj.TAX5 = clsCommon.myCstr(dt.Rows(0)("TAX5"))
            obj.TAX5_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX5_Rate"))
            obj.TAX5_Base_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX5_Base_Amt"))
            obj.TAX5_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX5_Amt"))
            obj.Total_Tax_Amt = clsCommon.myCdbl(dt.Rows(0)("Total_Tax_Amt"))
            obj.Document_Amount = clsCommon.myCdbl(dt.Rows(0)("Document_Amount"))

            obj.arrInvoiceDetailBulkSale = ClsInvoiceDetailBulkSale.getData(obj.Document_No, trans)
        End If
        Return obj
    End Function

    Public Shared Function DeleteData(ByVal strDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Dim isSaved As Boolean = False
        Dim qry As String = String.Empty
        If (clsCommon.myLen(strDocNo) <= 0) Then
            Throw New Exception("Document No not found to Delete")
        End If

        Dim dt As DataTable = clsDBFuncationality.GetDataTable("select Document_Date,Location_Code from TSPL_INVOICE_MASTER_BULKSAlE where Document_No='" + strDocNo + "'", trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleBulkSale, clsUserMgtCode.FrmInvoiceBulkSale, clsCommon.myCstr(dt.Rows(0)("Location_Code")), clsCommon.myCDate(dt.Rows(0)("Document_Date")), trans)

        End If

        Try
            'ClsInvoiceDetailBulkSale.deleteData(strDocNo)
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strDocNo, "TSPL_INVOICE_MASTER_BULKSAlE", "Document_No", "TSPL_INVOICE_DETAIL_BulKSALE", "Document_No", trans)
            qry = "delete from TSPL_INVOICE_DETAIL_BulKSALE where Document_No='" + strDocNo + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "delete from TSPL_INVOICE_MASTER_BULKSAlE where Document_No='" + strDocNo + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        Finally
            qry = Nothing
        End Try

        Return isSaved
    End Function
    Public Shared Function PostData(ByVal FormId As String, ByVal arrLoc As String, ByVal strDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            PostData(FormId, arrLoc, strDocNo, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function PostData(ByVal FormId As String, ByVal arrLoc As String, ByVal strDocNo As String, ByVal trans As SqlTransaction) As Boolean

        Try
            Dim isSaved As Boolean = True
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Document No not found to Post")
            End If
            Dim obj As ClsInvoiceBulkSale = ClsInvoiceBulkSale.GetData(strDocNo, arrLoc, NavigatorType.Current, trans)
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleBulkSale, clsUserMgtCode.FrmInvoiceBulkSale, obj.Location_Code, obj.Document_Date, trans)


            If (obj Is Nothing OrElse clsCommon.myLen(obj.Document_No) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If


            createARInvoice(obj, "", "", trans)

            Dim qry = "Update TSPL_INVOICE_MASTER_BULKSAlE set Posted=1, " & _
            "Posting_Date='" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt") + "' " & _
            " where Document_No='" + strDocNo + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strDocNo, "TSPL_INVOICE_MASTER_BULKSAlE", "Document_No", trans)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function

    Public Shared Function createARInvoice(ByVal obj As ClsInvoiceBulkSale, ByVal strARNoForRecreate As String, ByVal strVoucherForRecreate As String, ByVal trans As SqlTransaction) As Boolean
        ''''''''''''''''''''''''''''''''''For Making AR Invoice
        Dim objCustInv As New clsCustomerInvoiceHead()
        ''objCustInv.Document_No ''Will be Generateed
        objCustInv.Document_Date = obj.Document_Date
        ''richa 09/10/2014
        If clsCommon.CompairString(obj.InvoiceAgainst, "Against Dispatch") = CompairStringResult.Equal Then
            objCustInv.Trans_Type = "BS"
        Else
            objCustInv.Trans_Type = "BST"
        End If
        ''===============================

        objCustInv.Document_Type = "I"
        objCustInv.Document_Total = obj.Total_Amt
        objCustInv.Customer_Code = obj.Customer_Code
        objCustInv.RoundOffAmount = obj.RoundOffAmount
        objCustInv.Customer_Name = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Customer_Name from TSPL_CUSTOMER_MASTER where Cust_Code='" + obj.Customer_Code + "'", trans))
        objCustInv.Posting_Date = obj.Document_Date
        Dim qry As String = " select Cust_Account from TSPL_CUSTOMER_MASTER where Cust_Code='" + obj.Customer_Code + "'"
        objCustInv.Account_Set = clsDBFuncationality.getSingleValue(qry, trans)
        ''objCustInv.Order_No
        objCustInv.loc_code = clsLocation.GetSegmentCode(obj.Location_Code, trans)
        objCustInv.On_Hold = 0
        objCustInv.Remarks = ""
        objCustInv.Description = ""
        objCustInv.Tax_Group = obj.Tax_Group
        objCustInv.TAX1 = obj.TAX1
        objCustInv.TAX1_Rate = obj.TAX1_Rate
        objCustInv.TAX1_Amt = obj.TAX1_Amt
        objCustInv.TAX2 = obj.TAX2
        objCustInv.TAX2_Rate = obj.TAX2_Rate
        objCustInv.TAX2_Amt = obj.TAX2_Amt
        objCustInv.TAX3 = obj.TAX3
        objCustInv.TAX3_Rate = obj.TAX3_Rate
        objCustInv.TAX3_Amt = obj.TAX3_Amt
        objCustInv.TAX4 = obj.TAX4
        objCustInv.TAX4_Rate = obj.TAX4_Rate
        objCustInv.TAX4_Amt = obj.TAX4_Amt
        objCustInv.TAX5 = obj.TAX5
        objCustInv.TAX5_Rate = obj.TAX5_Rate
        objCustInv.TAX5_Amt = obj.TAX5_Amt
        objCustInv.TAX6 = ""
        objCustInv.TAX6_Rate = 0
        objCustInv.TAX6_Amt = 0
        objCustInv.TAX7 = ""
        objCustInv.TAX7_Rate = 0
        objCustInv.TAX7_Amt = 0
        objCustInv.TAX8 = ""
        objCustInv.TAX8_Rate = 0
        objCustInv.TAX8_Amt = 0
        objCustInv.TAX9 = ""
        objCustInv.TAX9_Rate = 0
        objCustInv.TAX9_Amt = 0
        objCustInv.TAX10 = ""
        objCustInv.TAX10_Rate = 0
        objCustInv.TAX10_Amt = 0
        objCustInv.Total_Tax = obj.Total_Tax_Amt
        objCustInv.Tax1_BAmount = obj.TAX1_Base_Amt
        objCustInv.Tax2_BAmount = obj.TAX2_Base_Amt
        objCustInv.Tax3_BAmount = obj.TAX3_Base_Amt
        objCustInv.Tax4_BAmount = obj.TAX4_Base_Amt
        objCustInv.Tax5_BAmount = obj.TAX5_Base_Amt
        objCustInv.Tax6_BAmount = 0
        objCustInv.Tax7_BAmount = 0
        objCustInv.Tax8_BAmount = 0
        objCustInv.Tax9_BAmount = 0
        objCustInv.Tax10_BAmount = 0
        objCustInv.Balance_Amt = obj.Total_Amt
        objCustInv.Terms_Code = ""
        objCustInv.PROJECT_ID = ""

        '' currency details
        objCustInv.CURRENCY_CODE = ""
        objCustInv.ConvRate = 0
        objCustInv.ApplicableFrom = Nothing
        'qry = "select Terms_Code,Terms_Desc,No_Days from TSPL_TERMS_MASTER where Terms_Code='" + obj.Terms_Code + "'"
        'Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        'If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
        '    objCustInv.Terms_Description = clsCommon.myCstr(dt.Rows(0)("Terms_Desc"))
        '    objCustInv.Due_Date = obj.Document_Date.AddDays(clsCommon.myCdbl(dt.Rows(0)("No_Days")))
        'End If

        objCustInv.Discount_Percentage = 0
        ' objCustInv.Discount_Base = obj.Total_Amt - obj.RoundOffAmount
        objCustInv.Discount_Base = obj.Document_Amount
        objCustInv.Discount_Amount = 0
        ''richa agarwal
        ' objCustInv.Amount_Less_Discount = obj.Total_Amt - obj.RoundOffAmount
        objCustInv.Amount_Less_Discount = obj.Document_Amount
        ''==============
        Dim dt As DataTable
        dt = clsDBFuncationality.GetDataTable("select Receivable_Control_acct,Receipts_Discount_acct from TSPL_CUSTOMER_ACCOUNT_SET where Cust_Account='" + objCustInv.Account_Set + "'", trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            objCustInv.Customer_Control_AC = clsCommon.myCstr(dt.Rows(0)("Receivable_Control_acct"))
            'If clsCommon.myCdbl(obj.Discount_Amt) > 0 Then
            '    objCustInv.Discount_GL_AC = clsCommon.myCstr(dt.Rows(0)("Receipts_Discount_acct"))
            'End If
        End If

        If obj.TAX1_Amt > 0 AndAlso clsCommon.myLen(obj.TAX1) > 0 Then
            objCustInv.TAX1_GLAC = clsTaxMaster.GetTaxPayAC(obj.TAX1, trans)
            objCustInv.TAX1_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objCustInv.TAX1_GLAC, obj.Location_Code, trans)
        End If
        If obj.TAX2_Amt > 0 AndAlso clsCommon.myLen(obj.TAX2) > 0 Then
            objCustInv.TAX2_GLAC = clsTaxMaster.GetTaxPayAC(obj.TAX2, trans)
            objCustInv.TAX2_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objCustInv.TAX2_GLAC, obj.Location_Code, trans)
        End If
        If obj.TAX3_Amt > 0 AndAlso clsCommon.myLen(obj.TAX3) > 0 Then
            objCustInv.TAX3_GLAC = clsTaxMaster.GetTaxPayAC(obj.TAX3, trans)
            objCustInv.TAX3_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objCustInv.TAX3_GLAC, obj.Location_Code, trans)
        End If
        If obj.TAX4_Amt > 0 AndAlso clsCommon.myLen(obj.TAX4) > 0 Then
            objCustInv.TAX4_GLAC = clsTaxMaster.GetTaxPayAC(obj.TAX4, trans)
            objCustInv.TAX4_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objCustInv.TAX4_GLAC, obj.Location_Code, trans)
        End If
        If obj.TAX5_Amt > 0 AndAlso clsCommon.myLen(obj.TAX5) > 0 Then
            objCustInv.TAX5_GLAC = clsTaxMaster.GetTaxPayAC(obj.TAX5, trans)
            objCustInv.TAX5_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objCustInv.TAX5_GLAC, obj.Location_Code, trans)
        End If
        'If obj.TAX6_Amt > 0 AndAlso clsCommon.myLen(obj.TAX6) > 0 Then
        '    objCustInv.TAX6_GLAC = clsTaxMaster.GetTaxPayAC(obj.TAX6, trans)
        '    objCustInv.TAX6_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objCustInv.TAX6_GLAC, obj.Bill_To_Location, trans)
        'End If
        'If obj.TAX7_Amt > 0 AndAlso clsCommon.myLen(obj.TAX7) > 0 Then
        '    objCustInv.TAX7_GLAC = clsTaxMaster.GetTaxPayAC(obj.TAX7, trans)
        '    objCustInv.TAX7_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objCustInv.TAX7_GLAC, obj.Bill_To_Location, trans)
        'End If
        'If obj.TAX8_Amt > 0 AndAlso clsCommon.myLen(obj.TAX8) > 0 Then
        '    objCustInv.TAX8_GLAC = clsTaxMaster.GetTaxPayAC(obj.TAX8, trans)
        '    objCustInv.TAX8_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objCustInv.TAX8_GLAC, obj.Bill_To_Location, trans)
        'End If
        'If obj.TAX9_Amt > 0 AndAlso clsCommon.myLen(obj.TAX9) > 0 Then
        '    objCustInv.TAX9_GLAC = clsTaxMaster.GetTaxPayAC(obj.TAX9, trans)
        '    objCustInv.TAX9_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objCustInv.TAX9_GLAC, obj.Bill_To_Location, trans)
        'End If
        'If obj.TAX10_Amt > 0 AndAlso clsCommon.myLen(obj.TAX10) > 0 Then
        '    objCustInv.TAX10_GLAC = clsTaxMaster.GetTaxPayAC(obj.TAX10, trans)
        '    objCustInv.TAX10_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objCustInv.TAX10_GLAC, obj.Bill_To_Location, trans)
        'End If

        'objCustInv.RefDocType=
        'objCustInv.RefDocNo
        objCustInv.Add_Charge_Code1 = ""
            objCustInv.Add_Charge_Name1 = ""
        objCustInv.Add_Charge_Amt1 = 0
        objCustInv.Add_Charge_Code2 = ""
        objCustInv.Add_Charge_Name2 = ""
        objCustInv.Add_Charge_Amt2 = 0
        objCustInv.Add_Charge_Code3 = ""
        objCustInv.Add_Charge_Name3 = ""
        objCustInv.Add_Charge_Amt3 = 0
        objCustInv.Add_Charge_Code4 = ""
        objCustInv.Add_Charge_Name4 = ""
        objCustInv.Add_Charge_Amt4 = 0
        objCustInv.Add_Charge_Code5 = ""
        objCustInv.Add_Charge_Name5 = ""
        objCustInv.Add_Charge_Amt5 = 0
        objCustInv.Add_Charge_Code6 = ""
        objCustInv.Add_Charge_Name6 = ""
        objCustInv.Add_Charge_Amt6 = 0
        objCustInv.Add_Charge_Code7 = ""
        objCustInv.Add_Charge_Name7 = ""
        objCustInv.Add_Charge_Amt7 = 0
        objCustInv.Add_Charge_Code8 = ""
        objCustInv.Add_Charge_Name8 = ""
        objCustInv.Add_Charge_Amt8 = 0
        objCustInv.Add_Charge_Code9 = ""
        objCustInv.Add_Charge_Name9 = ""
        objCustInv.Add_Charge_Amt9 = 0
        objCustInv.Add_Charge_Code10 = ""
        objCustInv.Add_Charge_Name10 = ""
        objCustInv.Add_Charge_Amt10 = 0
        objCustInv.Total_Add_Charge = 0
        objCustInv.Tax_Calculation_Type = EnumTaxCalucationType.Automatic
        ''objCustInv.Status
        ''objCustInv.AgainstScrap
        objCustInv.Against_Sale_No = obj.Document_No
        Dim counter As Integer = 1
        objCustInv.Arr = New List(Of clsCustomerInvoiceDetail)
        For Each objTr As ClsInvoiceDetailBulkSale In obj.arrInvoiceDetailBulkSale


            Dim objCustInvTR As clsCustomerInvoiceDetail = New clsCustomerInvoiceDetail()
            objCustInvTR.SNo = counter
            dt = clsItemMaster.GetSaleAccGLAC(objTr.Item_Code, trans)
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                Throw New Exception("Please set sale account for item" + objTr.Item_Code)
            End If
            objCustInvTR.GL_Account_Code = clsCommon.myCstr(dt.Rows(0)("Sales_Account"))
            objCustInvTR.GL_Account_Code = clsERPFuncationality.ChangeGLAccountLocationSegment(objCustInvTR.GL_Account_Code, obj.Location_Code, trans)
            objCustInvTR.GL_Account_Desc = clsGLAccount.GetName(objCustInvTR.GL_Account_Code, trans)
            objCustInvTR.Reco_Control_Account = "S"

            ''richa 18/09/2014 change for dispatch trade and dispatch
            'If clsCommon.CompairString(obj.InvoiceAgainst, "Against Dispatch") = CompairStringResult.Equal Then
            '    objCustInvTR.Amount = objTr.DispatchAmount
            'Else
            '    objCustInvTR.Amount = objTr.InvoiceAmount
            'End If
            'objCustInvTR.Amount = objTr.DispatchAmount
            ''richa 13/10/2014
            'objCustInvTR.Amount = objTr.InvoiceAmount
            objCustInvTR.Amount = objTr.InvoiceAmount
            '==============
            objCustInvTR.Discount_Per = 0
            objCustInvTR.Discount = 0
            'If clsCommon.CompairString(obj.InvoiceAgainst, "Against Dispatch") = CompairStringResult.Equal Then
            '    objCustInvTR.Amount_less_Discount = objTr.DispatchAmount
            'Else
            '    objCustInvTR.Amount_less_Discount = objTr.InvoiceAmount
            'End If
            ' objCustInvTR.Amount_less_Discount = objTr.DispatchAmount
            ''richa 25/09/2014
            objCustInvTR.Amount_less_Discount = objTr.InvoiceAmount
            '==============
            objCustInvTR.TAX1 = objTr.TAX1
            objCustInvTR.TAX1_Rate = objTr.TAX1_Rate
            objCustInvTR.TAX1_Amt = objTr.TAX1_Amt
            objCustInvTR.TAX2 = objTr.TAX2
            objCustInvTR.TAX2_Rate = objTr.TAX2_Rate
            objCustInvTR.TAX2_Amt = objTr.TAX2_Amt
            objCustInvTR.TAX3 = objTr.TAX3
            objCustInvTR.TAX3_Rate = objTr.TAX3_Rate
            objCustInvTR.TAX3_Amt = objTr.TAX3_Amt
            objCustInvTR.TAX4 = objTr.TAX4
            objCustInvTR.TAX4_Rate = objTr.TAX4_Rate
            objCustInvTR.TAX4_Amt = objTr.TAX4_Amt
            objCustInvTR.TAX5 = objTr.TAX5
            objCustInvTR.TAX5_Rate = objTr.TAX5_Rate
            objCustInvTR.TAX5_Amt = objTr.TAX5_Amt
            objCustInvTR.TAX6 = ""
            objCustInvTR.TAX6_Rate = 0
            objCustInvTR.TAX6_Amt = 0
            objCustInvTR.TAX7 = ""
            objCustInvTR.TAX7_Rate = 0
            objCustInvTR.TAX7_Amt = 0
            objCustInvTR.TAX8 = ""
            objCustInvTR.TAX8_Rate = 0
            objCustInvTR.TAX8_Amt = 0
            objCustInvTR.TAX9 = ""
            objCustInvTR.TAX9_Rate = 0
            objCustInvTR.TAX9_Amt = 0
            objCustInvTR.TAX10 = ""
            objCustInvTR.TAX10_Rate = 0
            objCustInvTR.TAX10_Amt = 0
            objCustInvTR.Total_Tax = objTr.Total_Tax_Amt
            ''richa 13/09/2014
            objCustInvTR.Total_Amount = objTr.Item_Net_Amt
            'If clsCommon.CompairString(obj.InvoiceAgainst, "Against Dispatch") = CompairStringResult.Equal Then
            '    objCustInvTR.Total_Amount = objTr.DispatchAmount
            'Else
            '    objCustInvTR.Total_Amount = objTr.InvoiceAmount
            'End If
            'objCustInvTR.Total_Amount = objTr.DispatchAmount
            '===================================
            objCustInvTR.Remarks = ""
            objCustInvTR.TAX1_Base_Amt = objTr.TAX1_Base_Amt
            objCustInvTR.TAX2_Base_Amt = objTr.TAX2_Base_Amt
            objCustInvTR.TAX3_Base_Amt = objTr.TAX3_Base_Amt
            objCustInvTR.TAX4_Base_Amt = objTr.TAX4_Base_Amt
            objCustInvTR.TAX5_Base_Amt = objTr.TAX5_Base_Amt
            objCustInvTR.TAX6_Base_Amt = 0
            objCustInvTR.TAX7_Base_Amt = 0
            objCustInvTR.TAX8_Base_Amt = 0
            objCustInvTR.TAX9_Base_Amt = 0
            objCustInvTR.TAX10_Base_Amt = 0
            'objCustInvTR.Comments=objTr.Comments
            objCustInv.Arr.Add(objCustInvTR)
            counter += 1

        Next
        objCustInv.SaveData(objCustInv, True, trans, "BulkSaleInvoice", strVoucherForRecreate, strARNoForRecreate)
        clsCustomerInvoiceHead.PostData("BulkSaleInvoice", objCustInv.Document_No, "", trans)
        Return True
    End Function
End Class

Public Class ClsInvoiceDetailBulkSale
#Region "Variable"
    Public Document_No As String = Nothing
    Public Dispatch_Code As String = Nothing
    Public Dispatch_Date As Date?
    Public Item_Code As String = Nothing
    Public HSN_code As String = Nothing
    Public Unit_code As String = Nothing
    Public Tanker_Code As String = Nothing
    Public DispatchQty As Double = 0
    Public DispatchFatPer As Double = 0
    Public DispatchSNFPer As Double = 0
    Public DispatchRate As Double = 0
    Public DispatchAmount As Double = 0
    Public InvoiceQty As Double = 0
    Public InvoiceFatPer As Double = 0
    Public InvoiceSNFPer As Double = 0
    Public InvoiceFatKG As Double = 0
    Public InvoiceSNFKG As Double = 0
    Public InvoiceRate As Double = 0
    Public InvoiceAmount As Double = 0
    Public CLR As Double = 0
    Public TradeTanker_No As String = Nothing
    Public DispatchQty_in_Ltr As Double = 0
    Public InvoiceQty_in_Ltr As Double = 0
    Public TAX1 As String = Nothing
    Public TAX1_Base_Amt As Double = 0
    Public TAX1_Rate As Double = 0
    Public TAX1_Amt As Double = 0
    Public TAX2 As String = Nothing
    Public TAX2_Base_Amt As Double = 0
    Public TAX2_Rate As Double = 0
    Public TAX2_Amt As Double = 0
    Public TAX3 As String = Nothing
    Public TAX3_Base_Amt As Double = 0
    Public TAX3_Rate As Double = 0
    Public TAX3_Amt As Double = 0
    Public TAX4 As String = Nothing
    Public TAX4_Base_Amt As Double = 0
    Public TAX4_Rate As Double = 0
    Public TAX4_Amt As Double = 0
    Public TAX5 As String = Nothing
    Public TAX5_Base_Amt As Double = 0
    Public TAX5_Rate As Double = 0
    Public TAX5_Amt As Double = 0
    Public Total_Tax_Amt As Double = 0
    Public Item_Net_Amt As Double = 0

#End Region
    Public Shared Function saveData(ByVal arrObj As List(Of ClsInvoiceDetailBulkSale), ByVal strQCNo As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim issaved As Boolean = True
            Dim coll As Hashtable

            If arrObj IsNot Nothing Then

                For Each obj As ClsInvoiceDetailBulkSale In arrObj
                    coll = New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Document_No", strQCNo)
                    clsCommon.AddColumnsForChange(coll, "Dispatch_Code", obj.Dispatch_Code)
                    clsCommon.AddColumnsForChange(coll, "Dispatch_Date", clsCommon.GetPrintDate(obj.Dispatch_Date, "dd/MMM/yyyy"))
                    clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code)
                    clsCommon.AddColumnsForChange(coll, "Unit_code", obj.Unit_code)
                    clsCommon.AddColumnsForChange(coll, "Tanker_Code", obj.Tanker_Code, True)
                    clsCommon.AddColumnsForChange(coll, "TradeTanker_No", obj.TradeTanker_No, True)
                    clsCommon.AddColumnsForChange(coll, "DispatchQty", obj.DispatchQty)
                    clsCommon.AddColumnsForChange(coll, "DispatchFatPer", obj.DispatchFatPer)
                    clsCommon.AddColumnsForChange(coll, "DispatchSNFPer", obj.DispatchSNFPer)
                    clsCommon.AddColumnsForChange(coll, "DispatchRate", obj.DispatchRate)
                    clsCommon.AddColumnsForChange(coll, "DispatchAmount", obj.DispatchAmount)
                    clsCommon.AddColumnsForChange(coll, "InvoiceQty", obj.InvoiceQty)
                    clsCommon.AddColumnsForChange(coll, "InvoiceFatPer", obj.InvoiceFatPer)
                    clsCommon.AddColumnsForChange(coll, "InvoiceSNFPer", obj.InvoiceSNFPer)
                    clsCommon.AddColumnsForChange(coll, "InvoiceFatKG", obj.InvoiceFatKG)
                    clsCommon.AddColumnsForChange(coll, "InvoiceSNFKG", obj.InvoiceSNFKG)
                    clsCommon.AddColumnsForChange(coll, "InvoiceRate", obj.InvoiceRate)
                    clsCommon.AddColumnsForChange(coll, "InvoiceAmount", obj.InvoiceAmount)
                    clsCommon.AddColumnsForChange(coll, "CLR", obj.CLR)
                    clsCommon.AddColumnsForChange(coll, "DispatchQty_in_Ltr", obj.DispatchQty_in_Ltr)
                    clsCommon.AddColumnsForChange(coll, "InvoiceQty_in_Ltr", obj.InvoiceQty_in_Ltr)
                    clsCommon.AddColumnsForChange(coll, "TAX1", obj.TAX1)
                    clsCommon.AddColumnsForChange(coll, "TAX1_Base_Amt", obj.TAX1_Base_Amt)
                    clsCommon.AddColumnsForChange(coll, "TAX1_Rate", obj.TAX1_Rate)
                    clsCommon.AddColumnsForChange(coll, "TAX1_Amt", obj.TAX1_Amt)
                    clsCommon.AddColumnsForChange(coll, "TAX2", obj.TAX2)
                    clsCommon.AddColumnsForChange(coll, "TAX2_Base_Amt", obj.TAX2_Base_Amt)
                    clsCommon.AddColumnsForChange(coll, "TAX2_Rate", obj.TAX2_Rate)
                    clsCommon.AddColumnsForChange(coll, "TAX2_Amt", obj.TAX2_Amt)
                    clsCommon.AddColumnsForChange(coll, "TAX3", obj.TAX3)
                    clsCommon.AddColumnsForChange(coll, "TAX3_Base_Amt", obj.TAX3_Base_Amt)
                    clsCommon.AddColumnsForChange(coll, "TAX3_Rate", obj.TAX3_Rate)
                    clsCommon.AddColumnsForChange(coll, "TAX3_Amt", obj.TAX3_Amt)
                    clsCommon.AddColumnsForChange(coll, "TAX4", obj.TAX4)
                    clsCommon.AddColumnsForChange(coll, "TAX4_Base_Amt", obj.TAX4_Base_Amt)
                    clsCommon.AddColumnsForChange(coll, "TAX4_Rate", obj.TAX4_Rate)
                    clsCommon.AddColumnsForChange(coll, "TAX4_Amt", obj.TAX4_Amt)
                    clsCommon.AddColumnsForChange(coll, "TAX5", obj.TAX5)
                    clsCommon.AddColumnsForChange(coll, "TAX5_Base_Amt", obj.TAX5_Base_Amt)
                    clsCommon.AddColumnsForChange(coll, "TAX5_Rate", obj.TAX5_Rate)
                    clsCommon.AddColumnsForChange(coll, "TAX5_Amt", obj.TAX5_Amt)

                    clsCommon.AddColumnsForChange(coll, "Total_Tax_Amt", obj.Total_Tax_Amt)
                    clsCommon.AddColumnsForChange(coll, "Item_Net_Amt", obj.Item_Net_Amt)
                    issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, "TSPL_INVOICE_DETAIL_BulKSALE", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If
            Return issaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            arrObj = Nothing
        End Try
    End Function
    Public Shared Function getData(ByVal strQCNo As String, ByVal trans As SqlTransaction) As List(Of ClsInvoiceDetailBulkSale)
        Try
            Dim arrObj As List(Of ClsInvoiceDetailBulkSale) = Nothing
            Dim obj As ClsInvoiceDetailBulkSale = Nothing
            Dim qry As String = "select * from TSPL_INVOICE_DETAIL_BulKSALE where Document_No='" & strQCNo & "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                arrObj = New List(Of ClsInvoiceDetailBulkSale)
                For i As Integer = 0 To dt.Rows.Count - 1
                    obj = New ClsInvoiceDetailBulkSale()
                    obj.Document_No = clsCommon.myCstr(dt.Rows(i)("Document_No"))
                    obj.Dispatch_Code = clsCommon.myCstr(dt.Rows(i)("Dispatch_Code"))
                    obj.Dispatch_Date = clsCommon.myCstr(dt.Rows(i)("Dispatch_Date"))
                    obj.Item_Code = clsCommon.myCstr(dt.Rows(i)("Item_Code"))
                    obj.Unit_code = clsCommon.myCstr(dt.Rows(i)("Unit_Code"))
                    obj.HSN_code = clsItemMaster.GetItemHSNCode(clsCommon.myCstr(dt.Rows(i)("Item_Code")), trans)
                    obj.Tanker_Code = clsCommon.myCstr(dt.Rows(i)("Tanker_Code"))
                    obj.TradeTanker_No = clsCommon.myCstr(dt.Rows(i)("TradeTanker_No"))
                    obj.DispatchQty = clsCommon.myCdbl(dt.Rows(i)("DispatchQty"))
                    obj.DispatchFatPer = clsCommon.myCdbl(dt.Rows(i)("DispatchFatPer"))
                    obj.DispatchSNFPer = clsCommon.myCdbl(dt.Rows(i)("DispatchSNFPer"))
                    obj.DispatchRate = clsCommon.myCdbl(dt.Rows(i)("DispatchRate"))
                    obj.DispatchAmount = clsCommon.myCdbl(dt.Rows(i)("DispatchAmount"))
                    obj.InvoiceQty = clsCommon.myCdbl(dt.Rows(i)("InvoiceQty"))
                    obj.InvoiceFatPer = clsCommon.myCdbl(dt.Rows(i)("InvoiceFatPer"))
                    obj.InvoiceSNFPer = clsCommon.myCdbl(dt.Rows(i)("InvoiceSNFPer"))
                    obj.InvoiceFatKG = clsCommon.myCdbl(dt.Rows(i)("InvoiceFatKG"))
                    obj.InvoiceSNFKG = clsCommon.myCdbl(dt.Rows(i)("InvoiceSNFKG"))
                    obj.InvoiceRate = clsCommon.myCdbl(dt.Rows(i)("InvoiceRate"))
                    obj.InvoiceAmount = clsCommon.myCdbl(dt.Rows(i)("InvoiceAmount"))
                    obj.CLR = clsCommon.myCdbl(dt.Rows(i)("CLR"))
                    obj.DispatchQty_in_Ltr = clsCommon.myCdbl(dt.Rows(i)("DispatchQty_in_Ltr"))
                    obj.InvoiceQty_in_Ltr = clsCommon.myCdbl(dt.Rows(i)("InvoiceQty_in_Ltr"))
                    obj.TAX1 = clsCommon.myCstr(dt.Rows(i)("TAX1"))
                    obj.TAX1_Base_Amt = clsCommon.myCdbl(dt.Rows(i)("TAX1_Base_Amt"))
                    obj.TAX1_Rate = clsCommon.myCdbl(dt.Rows(i)("TAX1_Rate"))
                    obj.TAX1_Amt = clsCommon.myCdbl(dt.Rows(i)("TAX1_Amt"))
                    obj.TAX2 = clsCommon.myCstr(dt.Rows(i)("TAX2"))
                    obj.TAX2_Base_Amt = clsCommon.myCdbl(dt.Rows(i)("TAX2_Base_Amt"))
                    obj.TAX2_Rate = clsCommon.myCdbl(dt.Rows(i)("TAX2_Rate"))
                    obj.TAX2_Amt = clsCommon.myCdbl(dt.Rows(i)("TAX2_Amt"))
                    obj.TAX3 = clsCommon.myCstr(dt.Rows(i)("TAX3"))
                    obj.TAX3_Base_Amt = clsCommon.myCdbl(dt.Rows(i)("TAX3_Base_Amt"))
                    obj.TAX3_Rate = clsCommon.myCdbl(dt.Rows(i)("TAX3_Rate"))
                    obj.TAX3_Amt = clsCommon.myCdbl(dt.Rows(i)("TAX3_Amt"))
                    obj.TAX4 = clsCommon.myCstr(dt.Rows(i)("TAX4"))
                    obj.TAX4_Base_Amt = clsCommon.myCdbl(dt.Rows(i)("TAX4_Base_Amt"))
                    obj.TAX4_Rate = clsCommon.myCdbl(dt.Rows(i)("TAX4_Rate"))
                    obj.TAX4_Amt = clsCommon.myCdbl(dt.Rows(i)("TAX4_Amt"))
                    obj.TAX5 = clsCommon.myCstr(dt.Rows(i)("TAX5"))
                    obj.TAX5_Base_Amt = clsCommon.myCdbl(dt.Rows(i)("TAX5_Base_Amt"))
                    obj.TAX5_Rate = clsCommon.myCdbl(dt.Rows(i)("TAX5_Rate"))
                    obj.TAX5_Amt = clsCommon.myCdbl(dt.Rows(i)("TAX5_Amt"))
                    obj.Total_Tax_Amt = clsCommon.myCdbl(dt.Rows(i)("Total_Tax_Amt"))
                    obj.Item_Net_Amt = clsCommon.myCdbl(dt.Rows(i)("Item_Net_Amt"))
                    arrObj.Add(obj)
                Next
            End If
            Return arrObj
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function



    'Public Shared Function deleteData(ByVal strQCNo As String) As Boolean
    '    Try
    '        Dim isDeleted As Boolean = True
    '        Dim qry As String = "delete from TSPL_INVOICE_DETAIL_BulKSALE where Document_No='" & strQCNo & "'"
    '        isDeleted = isDeleted AndAlso clsDBFuncationality.ExecuteNonQuery(qry)
    '        Return isDeleted
    '    Catch ex As Exception
    '        clsCommon.MyMessageBoxShow(ex.Message)
    '    End Try
    'End Function
End Class
