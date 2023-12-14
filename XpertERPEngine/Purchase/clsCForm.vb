Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.WinControls.UI

Public Class clsCForm
#Region "Variables"
    Public Document_No As String = Nothing
    Public Document_Date As DateTime = Nothing
    Public CForm_No As String = Nothing
    Public CForm_Date As String = Nothing
    Public CollectionType As String = Nothing
    Public Source_Code As String = Nothing
    Public Source_Name As String = Nothing
    Public Reference As String = Nothing
    Public Description As String = Nothing
    Public Posted As String = Nothing
    Public Posting_Date As DateTime = Nothing
    Dim Created_By As String = Nothing
    Dim Created_Date As DateTime = Nothing
    Dim Modify_By As String = Nothing
    Dim Modify_Date As DateTime = Nothing
    Dim Comp_Code As String = Nothing
    Public From_Date As String = Nothing
    Public To_Date As String = Nothing
    Public Form_Code As String = Nothing

    Public Arr As List(Of ClsCFormDetails) = Nothing
#End Region

    Public Function SaveData(ByVal obj As clsCForm, ByVal isNewEntry As Boolean) As Boolean
        Dim isSaved As Boolean = True
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim qry As String = "delete from TSPL_CForm_Detail where Document_No='" + obj.Document_No + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Dim strDocNo As String = ""
            If isNewEntry Then
                Dim strCode As String = clsDBFuncationality.getSingleValue("select isnull(max(Document_No),'') from TSPL_CForm_HEADER", trans)
                If clsCommon.myLen(strCode) <= 0 Then
                    obj.Document_No = "CFORM000000001"
                Else
                    obj.Document_No = clsCommon.incval(strCode)
                End If

            End If

            If (clsCommon.myLen(obj.Document_No) <= 0) Then
                Throw New Exception("Error in Document Code Generation")
            End If
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "CForm_No", obj.CForm_No)
            clsCommon.AddColumnsForChange(coll, "CForm_Date", clsCommon.GetPrintDate(obj.CForm_Date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "CollectionType", obj.CollectionType)
            clsCommon.AddColumnsForChange(coll, "Source_Code", obj.Source_Code)
            clsCommon.AddColumnsForChange(coll, "Source_Name", obj.Source_Name)
            clsCommon.AddColumnsForChange(coll, "Reference", obj.Reference)
            clsCommon.AddColumnsForChange(coll, "Description", obj.Description)
            clsCommon.AddColumnsForChange(coll, "From_Date", clsCommon.GetPrintDate(obj.From_Date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "To_Date", clsCommon.GetPrintDate(obj.To_Date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Form_Code", obj.Form_Code)
            clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)

            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Document_No", obj.Document_No)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CForm_HEADER", OMInsertOrUpdate.Insert, "", trans)

            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CForm_HEADER", OMInsertOrUpdate.Update, "TSPL_CForm_HEADER.Document_No='" + obj.Document_No + "'", trans)
            End If

            'If Not isNewEntry Then
            '    clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, clsCommon.myCstr(obj.Document_No), "TSPL_CForm_HEADER", "Document_No", "TSPL_CForm_DETAIL", "Document_No", trans)
            'End If

            isSaved = isSaved AndAlso ClsCFormDetails.SaveData(obj.Document_No, obj.Arr, trans, obj.CollectionType)
            If isSaved Then
                trans.Commit()
            End If
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function

    Public Shared Function GetData(ByVal strRetCode As String, ByVal NavType As NavigatorType) As clsCForm
        Return GetData(strRetCode, NavType, Nothing)
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsCForm
        Dim obj As clsCForm = Nothing
        Dim qry As String = "select * from TSPL_CForm_HEADER where 2=2 "
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_CForm_HEADER.Document_No = (select MIN(Document_No) from TSPL_CForm_HEADER)"
            Case NavigatorType.Last
                qry += " and TSPL_CForm_HEADER.Document_No = (select Max(Document_No) from TSPL_CForm_HEADER)"
            Case NavigatorType.Next
                qry += " and TSPL_CForm_HEADER.Document_No = (select Min(Document_No) from TSPL_CForm_HEADER where Document_No>'" + strCode + "')"
            Case NavigatorType.Previous
                qry += " and TSPL_CForm_HEADER.Document_No = (select Max(Document_No) from TSPL_CForm_HEADER where Document_No<'" + strCode + "')"
            Case NavigatorType.Current
                qry += " and TSPL_CForm_HEADER.Document_No = '" + strCode + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            obj = New clsCForm()
            obj.Document_No = clsCommon.myCstr(dt.Rows(0)("Document_No"))
            obj.Document_Date = clsCommon.myCDate(dt.Rows(0)("Document_Date"))
            obj.CForm_No = clsCommon.myCstr(dt.Rows(0)("CForm_No"))
            obj.CForm_Date = clsCommon.myCstr(dt.Rows(0)("CForm_Date"))
            obj.CollectionType = clsCommon.myCstr(dt.Rows(0)("CollectionType"))
            obj.Source_Code = clsCommon.myCstr(dt.Rows(0)("Source_Code"))
            obj.Source_Name = clsCommon.myCstr(dt.Rows(0)("Source_Name"))
            obj.Reference = clsCommon.myCstr(dt.Rows(0)("Reference"))
            obj.Description = clsCommon.myCstr(dt.Rows(0)("Description"))
            obj.From_Date = clsCommon.myCstr(dt.Rows(0)("From_Date"))
            obj.To_Date = clsCommon.myCstr(dt.Rows(0)("To_Date"))
            obj.Form_Code = clsCommon.myCstr(dt.Rows(0)("Form_Code"))
            ' obj.Post = clsCommon.myCstr(dt.Rows(0)("Post"))

            obj.Arr = ClsCFormDetails.GetData(obj.Document_No, trans, obj.CollectionType, obj.From_Date, obj.To_Date, obj.Source_Code)

        End If
        Return obj
    End Function


    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim isSaved As Boolean = False
        If (clsCommon.myLen(strCode) <= 0) Then
            Throw New Exception("Document No not found to Delete")
        End If
        Dim obj As New clsCForm()
        obj = clsCForm.GetData(strCode, NavigatorType.Current, Nothing)
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Document_No) > 0) Then
            Try
                If (clsCommon.CompairString(obj.Posted, "Y") = CompairStringResult.Equal) Then
                    Throw New Exception("Already Posted on :" + clsCommon.GetPrintDate(obj.Posting_Date, "dd/MM/yyyy hh:mm tt"))
                End If
                Dim qry As String = "delete from TSPL_CForm_DETAIL where Document_No='" + strCode + "'"
                isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)

                qry = "delete from TSPL_CForm_HEADER where Document_No='" + strCode + "'"
                isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)


                'obj.Arr = ClsCFormDetails.GetData(obj.Document_No, trans, obj.CollectionType, obj.From_Date, obj.To_Date, obj.Source_Code)
                If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                    For Each objTr As ClsCFormDetails In obj.Arr
                        If clsCommon.CompairString(obj.CollectionType, "C") = CompairStringResult.Equal Then
                            If clsCommon.CompairString(objTr.doc_type, "CSA_Transfer") = CompairStringResult.Equal Then
                                qry = "Update TSPL_CSA_TRANSFER_HEAD set CFormApplied=0  where doc_code='" + objTr.Invoice_No + "'"
                            Else
                                qry = "Update TSPL_SD_SALE_INVOICE_HEAD set CFormApplied=0  where Document_Code='" + objTr.Invoice_No + "'"
                            End If
                        ElseIf clsCommon.CompairString(obj.CollectionType, "L") = CompairStringResult.Equal Then
                            If clsCommon.CompairString(objTr.doc_type, "CSA TRANSFER") = CompairStringResult.Equal Then
                                qry = "Update TSPL_CSA_TRANSFER_HEAD set CFormApplied=0  where doc_code='" + objTr.Invoice_No + "'"
                            Else
                                qry = "Update TSPL_TRANSFER_ORDER_HEAD set Form_Received=0 where Document_No='" + objTr.Invoice_No + "'"
                            End If
                        Else
                            qry = "Update TSPL_PI_HEAD set CFormApplied=0  where PI_NO='" + objTr.Invoice_No + "'"
                        End If

                        isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
                    Next
                End If

                If (isSaved) Then
                    trans.Commit()
                Else
                    trans.Rollback()
                End If
            Catch ex As Exception
                trans.Rollback()
                Throw New Exception(ex.Message)
            End Try
        End If
        Return isSaved
    End Function
End Class
Public Class ClsCFormDetails
#Region "Variables"
    Public Document_No As String = Nothing
    Public Document_Line_No As Integer = 0
    Public Invoice_No As String = Nothing
    Public Invoice_Date As Date = Nothing
    Public TaxableAmount As Double = 0
    Public TaxAmount As Double = 0
    Public InvoiceAmount As Double = 0
    Public CFormAmount As Double = 0
    Public Diff As Double = 0
    Public Remarks As String = Nothing
    Public Comments As String = Nothing
    Public Status As String = Nothing
    Public party As String = Nothing
    Public Form_No As String = Nothing
    Public doc_type As String = Nothing
    Public To_Location As String = Nothing
    Public Manual_Form_No As String = Nothing
    Public Is_Manual As Integer
#End Region

    Public Shared Function FinderForm_No(ByVal strCode As String, ByVal isButtonClicked As Boolean) As String
        Dim qry As String = "select start_No,END_No from TSPL_FORM_SERIAL_NO_MASTER where Form_Code='" & strCode & "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        qry = "select * from TSPL_CForm_DETAIL"
        Dim dtExits As DataTable = clsDBFuncationality.GetDataTable(qry)
        Dim WhrCls As String = ""
        qry = ""
        For ix As Integer = dt.Rows(0)(0) To dt.Rows(0)(1)
            If dtExits.Select("Form_No='" & ix & "'").Length <= 0 Then
                If ix <> dt.Rows(0)(0) And qry <> "" Then
                    qry &= " Union All "
                End If
                qry &= " select '" & ix & "' as CForm_No"
            End If
        Next
        'If clsCommon.myLen(strExitsFormNo) > 0 Then
        '    WhrCls = "Item_code='" + strExitsFormNo + "'"
        'End If
        Dim cc = clsCommon.ShowSelectForm("Form_No_Finder", qry, "CForm_No", WhrCls, strCode, "CForm_No", isButtonClicked)
        Return cc
    End Function

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of ClsCFormDetails), ByVal trans As SqlTransaction, ByVal strType As String) As Boolean
        Dim qry As String


        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As ClsCFormDetails In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Document_No", strDocNo)
                clsCommon.AddColumnsForChange(coll, "Document_Line_No", obj.Document_Line_No)
                clsCommon.AddColumnsForChange(coll, "Invoice_No", obj.Invoice_No)
                clsCommon.AddColumnsForChange(coll, "Invoice_Date", clsCommon.GetPrintDate(obj.Invoice_Date, "dd/MMM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "InvoiceAmount", obj.InvoiceAmount)
                clsCommon.AddColumnsForChange(coll, "CFormAmount", obj.CFormAmount)
                clsCommon.AddColumnsForChange(coll, "Diff", obj.Diff)
                clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks)
                clsCommon.AddColumnsForChange(coll, "Form_No", obj.Form_No)
                clsCommon.AddColumnsForChange(coll, "TAXABLE_AMOUNT", obj.TaxableAmount)
                clsCommon.AddColumnsForChange(coll, "TAX_AMOUNT", obj.TaxAmount)
                clsCommon.AddColumnsForChange(coll, "Doc_Type", obj.doc_type)
                clsCommon.AddColumnsForChange(coll, "To_Location", obj.To_Location)
                '  clsCommon.AddColumnsForChange(coll, "Manual_Form_No", obj.Manual_Form_No)
                clsCommon.AddColumnsForChange(coll, "Is_Manual", obj.Is_Manual)

                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CForm_DETAIL", OMInsertOrUpdate.Insert, "", trans)

                If clsCommon.CompairString(strType, "C") = CompairStringResult.Equal Then
                    If clsCommon.CompairString(obj.doc_type, "CSA_Transfer") = CompairStringResult.Equal Then
                        qry = "Update TSPL_CSA_TRANSFER_HEAD set CFormApplied=1  where doc_code='" + obj.Invoice_No + "'"
                    Else
                        qry = "Update TSPL_SD_SALE_INVOICE_HEAD set CFormApplied=1  where Document_Code='" + obj.Invoice_No + "'"
                    End If

                ElseIf clsCommon.CompairString(strType, "V") = CompairStringResult.Equal Then
                    qry = "Update TSPL_PI_HEAD set CFormApplied=1  where PI_NO='" + obj.Invoice_No + "'"
                ElseIf clsCommon.CompairString(strType, "L") = CompairStringResult.Equal Then
                    If clsCommon.CompairString(obj.doc_type, "CSA TRANSFER") = CompairStringResult.Equal Then
                        qry = "Update TSPL_CSA_TRANSFER_HEAD set CFormApplied=1  where doc_code='" + obj.Invoice_No + "'"
                    Else
                        qry = "Update TSPL_TRANSFER_ORDER_HEAD set Form_Received=1  where Document_No='" + obj.Invoice_No + "'"
                    End If
                Else
                    qry = "Update TSPL_TRANSFER_ORDER_HEAD SET Form_Received= 1 WHERE Document_No='" + obj.Invoice_No + "' "
                End If
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
            Next
        End If
        Return True
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal trans As SqlTransaction, ByVal strType As String, ByVal FromDate As String, ByVal Todate As String, ByVal sourcecode As String) As List(Of ClsCFormDetails)
        Dim arr As List(Of ClsCFormDetails) = Nothing
        Dim qry As String
        If clsCommon.CompairString(strType, "C") = CompairStringResult.Equal Then
            qry = "select doc_type,1 as Status,Invoice_No as Document_Code,Invoice_Date as Inv_Date,InvoiceAmount as Total_Amt,CFormAmount,Diff,Remarks,'' as Party,Taxable_Amount,Tax_Amount,form_no,Is_Manual " & _
            "from TSPL_CForm_DETAIL where Document_No='" & strCode & "'  " & _
            "union all " & _
            " select 'SALE' as doc_type,0 as Status,Document_Code,Inv_Date,Total_Amt,0 as CFormAmount,0 as Diff,'' as Remarks " & _
            ",Customer_NAME AS Party,amount_less_Discount,total_tax_Amt,'' as Form_No,'' AS Is_Manual from TSPL_SD_SALE_INVOICE_HEAD inner join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.cust_code=TSPL_SD_SALE_INVOICE_HEAD.customer_code and cust_code='" & sourcecode & "' where Against_C_Form=1 and Document_Code not in (select Invoice_No from TSPL_CForm_DETAIL) and " & _
            "CFormApplied=0 and CFormRecd=0 and Total_Amt > 0 and Inv_Date > = '" & Format(CDate(FromDate), "dd-MMM-yyyy") & "' and Inv_Date <='" & Format(CDate(Todate), "dd-MMM-yyyy") & "'" & _
            "union all " & _
            " select 'CSA_Transfer' as doc_type,0 as Status,doc_code as Document_Code,transfer_date as Inv_Date,document_amount as Total_Amt,0 as CFormAmount,0 as Diff,'' as Remarks " & _
            ",Customer_NAME AS Party,tax1_base_amt as amount_less_Discount,total_tax_Amt,'' as Form_No,'' AS Is_Manual from TSPL_CSA_TRANSFER_HEAD inner join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.cust_code=TSPL_CSA_TRANSFER_HEAD.cust_code and TSPL_CSA_TRANSFER_HEAD.cust_code='" & sourcecode & "' where Against_Form='F' and doc_code not in (select Invoice_No from TSPL_CForm_DETAIL) and " & _
            "CFormApplied=0 and CFormRecd=0 and document_amount > 0 and convert(date,transfer_date,103) > = convert(date,'" & Format(CDate(FromDate), "dd-MMM-yyyy") & "',103) and convert(date,transfer_date,103) <=convert(date,'" & Format(CDate(Todate), "dd-MMM-yyyy") & "',103)"

        Else
            qry = "select doc_type,1 as Status,Invoice_No as Document_Code,Invoice_Date as  Inv_Date,InvoiceAmount as Total_Amt,CFormAmount,Diff,Remarks,'' as Party,Taxable_Amount,Tax_Amount,form_no,Is_Manual,To_Location " & _
            "from TSPL_CForm_DETAIL where Document_No='" & strCode & "'  " & _
            "union all " & _
            "select 'PURCHASE' as doc_type,0 as Status,PI_No as Document_Code,PI_Date as Inv_Date,PI_Total_Amt as Total_Amt, " & _
            "0 as CFormAmount,0 as Diff,'' as Remarks,Vendor_name as Party,amount_less_Discount,total_tax_Amt,'' as Form_No,0 AS Is_Manual,'' AS To_Location  from TSPL_PI_HEAD  where  vendor_code='" & sourcecode & "' and Against_C_Form=1 and " & _
            "PI_No not in (select Invoice_No from TSPL_CForm_DETAIL)  and CFormApplied=0 and CFormRecd=0 and PI_Total_Amt > 0  and PI_Date > = '" & Format(CDate(FromDate), "dd-MMM-yyyy") & "' and PI_Date <='" & Format(CDate(Todate), "dd-MMM-yyyy") & "'  "
        End If

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            arr = New List(Of ClsCFormDetails)()
            For Each dr As DataRow In dt.Rows
                Dim obj As ClsCFormDetails = New ClsCFormDetails()
                obj.doc_type = clsCommon.myCstr(dr("doc_type"))
                obj.Status = clsCommon.myCstr(dr("Status"))
                obj.Invoice_No = clsCommon.myCstr(dr("Document_Code"))
                obj.Invoice_Date = clsCommon.myCstr(dr("Inv_Date"))
                obj.InvoiceAmount = clsCommon.myCstr(dr("Total_Amt"))
                obj.CFormAmount = clsCommon.myCstr(dr("CFormAmount"))
                obj.Diff = clsCommon.myCstr(dr("Diff"))
                obj.Remarks = clsCommon.myCstr(dr("Remarks"))
                obj.party = clsCommon.myCstr(dr("Party"))
                obj.Form_No = clsCommon.myCstr(dr("Form_No"))
                obj.TaxableAmount = clsCommon.myCstr(dr("Taxable_Amount"))
                obj.TaxAmount = clsCommon.myCstr(dr("Tax_Amount"))
                If clsCommon.CompairString(strType, "L") = CompairStringResult.Equal Then
                    obj.To_Location = clsCommon.myCstr(dr("To_Location"))
                Else
                    obj.To_Location = ""
                End If

                obj.Is_Manual = clsCommon.myCdbl(dr("Is_Manual"))
                arr.Add(obj)
            Next
        End If
        Return arr
    End Function
End Class
