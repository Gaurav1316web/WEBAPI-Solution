'--------Created By Monika 10/07/2014-----------BM00000003051
Imports common
Imports System.Data.SqlClient
Imports System.Windows.Forms
Imports Telerik.WinControls

Public Class clsFormSerialNoMaster

#Region "Variables"
    Public docno As String = Nothing
    Public formcode As String = Nothing
    Public formname As String = Nothing
    Public formtype As String = Nothing
    Public prefix As String = Nothing
    Public startno As Decimal = Nothing
    Public endno As Decimal = Nothing
    Public totalno As Decimal = Nothing
    Public docdate As Date = Nothing
#End Region

    Public Shared Function GetFinder(ByVal whrCls As String, ByVal strCurrCode As String, ByVal isButtonClicked As Boolean) As String
        Dim strvalue As String = ""
        Try
            Dim qry As String = "select TSPL_FORM_SERIAL_NO_MASTER.doc_no as [Code],TSPL_FORM_SERIAL_NO_MASTER.doc_date as [Date],TSPL_FORM_SERIAL_NO_MASTER.form_code as [Form Code],tspl_form_master.form_name as [Form Name],tspl_form_master.form_type as [Form Type],TSPL_FORM_SERIAL_NO_MASTER.Prefix,TSPL_FORM_SERIAL_NO_MASTER.start_no as [Series Start From],TSPL_FORM_SERIAL_NO_MASTER.end_no as [End At],TSPL_FORM_SERIAL_NO_MASTER.total_form as [Total No. of Form],TSPL_FORM_SERIAL_NO_MASTER.created_by as [Created By],TSPL_FORM_SERIAL_NO_MASTER.created_date as [Created Date],TSPL_FORM_SERIAL_NO_MASTER.modified_by as [Modified By],TSPL_FORM_SERIAL_NO_MASTER.modified_date as [Modified Date] from TSPL_FORM_SERIAL_NO_MASTER left outer join tspl_form_master on tspl_form_master.form_code=TSPL_FORM_SERIAL_NO_MASTER.form_code"
            strvalue = clsCommon.ShowSelectForm("FRMFND", qry, "Code", whrCls, strCurrCode, "Code", isButtonClicked)


        Catch ex As Exception
            'Throw New Exception(ex.Message)
        End Try
        Return strvalue
    End Function

    Public Shared Function SaveData(ByVal obj As clsFormSerialNoMaster, ByVal trans As SqlTransaction) As Boolean
        Try

            Dim qry As String = "select count(*) from TSPL_FORM_SERIAL_NO_MASTER where doc_no='" + obj.docno + "'"
            Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)

            If clsCommon.myLen(obj.docno) <= 0 Then
                qry = "select max(doc_no) from TSPL_FORM_SERIAL_NO_MASTER where form_code='" + obj.formcode + "'"
                Dim value As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))

                If clsCommon.myLen(value) <= 0 Then
                    value = obj.formcode + "000001"
                Else
                    value = clsCommon.myCstr(clsCommon.incval(value))
                End If
                obj.docno = value
            End If

            Dim issaved As Boolean = True
            Dim coll As New Hashtable()

            clsCommon.AddColumnsForChange(coll, "comp_code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "doc_no", obj.docno)
            clsCommon.AddColumnsForChange(coll, "Doc_Date", clsCommon.GetPrintDate(obj.docdate, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "form_code", obj.formcode)
            clsCommon.AddColumnsForChange(coll, "prefix", obj.prefix)
            clsCommon.AddColumnsForChange(coll, "start_no", obj.startno)
            clsCommon.AddColumnsForChange(coll, "end_no", obj.endno)
            clsCommon.AddColumnsForChange(coll, "total_form", obj.totalno)
            clsCommon.AddColumnsForChange(coll, "created_by", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "created_date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "modified_by", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "modified_date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))

            If check <= 0 Then
                issaved = issaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_FORM_SERIAL_NO_MASTER", OMInsertOrUpdate.Insert, "", trans)
            Else
                issaved = issaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_FORM_SERIAL_NO_MASTER", OMInsertOrUpdate.Update, " TSPL_FORM_SERIAL_NO_MASTER.doc_no='" + obj.docno + "'", trans)
            End If

            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function DeleteData(ByVal docno As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim qry As String = "select count(*) from TSPL_FORM_SERIAL_NO_MASTER where doc_no='" + docno + "'"
            Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)

            If check <= 0 Then
                Throw New Exception("Document No. not found for deletion")
            End If

            If Not clsCommon.MyMessageBoxShow("Are you sure want to Delete document no. " + docno + "?", "Attention", MessageBoxButtons.YesNo, RadMessageIcon.Question) = DialogResult.Yes Then
                Return False
            End If

            qry = "delete from TSPL_FORM_SERIAL_NO_MASTER where doc_no='" + docno + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function GetData(ByVal docno As String, ByVal NavType As NavigatorType) As clsFormSerialNoMaster
        Try
            Dim obj As New clsFormSerialNoMaster()

            Dim qry As String = "select TSPL_FORM_SERIAL_NO_MASTER.doc_no,TSPL_FORM_SERIAL_NO_MASTER.doc_date,TSPL_FORM_SERIAL_NO_MASTER.form_code,tspl_form_master.form_name,tspl_form_master.form_type,TSPL_FORM_SERIAL_NO_MASTER.prefix,TSPL_FORM_SERIAL_NO_MASTER.start_no,TSPL_FORM_SERIAL_NO_MASTER.end_no,TSPL_FORM_SERIAL_NO_MASTER.total_form from TSPL_FORM_SERIAL_NO_MASTER left outer join tspl_form_master on tspl_form_master.form_code=TSPL_FORM_SERIAL_NO_MASTER.form_code"

            Select Case NavType
                Case NavigatorType.Current
                    qry += " where TSPL_FORM_SERIAL_NO_MASTER.doc_no='" + docno + "'"
                Case NavigatorType.First
                    qry += " where TSPL_FORM_SERIAL_NO_MASTER.doc_no in (select min(doc_no) from TSPL_FORM_SERIAL_NO_MASTER where doc_no='" + docno + "')"
                Case NavigatorType.Last
                    qry += " where TSPL_FORM_SERIAL_NO_MASTER.doc_no in (select max(doc_no) from TSPL_FORM_SERIAL_NO_MASTER where doc_no='" + docno + "')"
                Case NavigatorType.Next
                    qry += " where TSPL_FORM_SERIAL_NO_MASTER.doc_no in (select min(doc_no) from TSPL_FORM_SERIAL_NO_MASTER where doc_no>'" + docno + "')"
                Case NavigatorType.Previous
                    qry += " where TSPL_FORM_SERIAL_NO_MASTER.doc_no in (select max(doc_no) from TSPL_FORM_SERIAL_NO_MASTER where doc_no<'" + docno + "')"
            End Select

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                obj.docno = clsCommon.myCstr(dt.Rows(0)("doc_no"))
                obj.docdate = clsCommon.myCDate(dt.Rows(0)("doc_date"))
                obj.formcode = clsCommon.myCstr(dt.Rows(0)("form_code"))
                obj.formname = clsCommon.myCstr(dt.Rows(0)("form_name"))
                obj.formtype = clsCommon.myCstr(dt.Rows(0)("form_type"))
                obj.prefix = clsCommon.myCstr(dt.Rows(0)("prefix"))
                obj.startno = clsCommon.myCdbl(dt.Rows(0)("start_no"))
                obj.endno = clsCommon.myCdbl(dt.Rows(0)("end_no"))
                obj.totalno = clsCommon.myCdbl(dt.Rows(0)("total_form"))
            End If

            Return obj
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
End Class

Public Class clsFormIssueReceiptEntry
#Region "Variables"
    Public Docno As String = Nothing
    Public Formcode As String = Nothing
    Public sno As Integer = Nothing
    Public formserialno As String = Nothing
    Public issuedate As Date = Nothing
    Public vendorcode As String = Nothing
    Public billno As String = Nothing
    Public billdate As Date = Nothing
    Public amount As Decimal = Nothing
    Public POno As String = Nothing
    Public saleinvoiceno As String = Nothing
    Public custcode As String = Nothing
    Public vendorcustomer_type As String = Nothing
    Public iss_rcv As String = Nothing
    Public arr As List(Of clsFormIssueReceiptEntry)
#End Region

    Public Shared Function SaveData(ByVal obj As clsFormIssueReceiptEntry, ByVal arr As List(Of clsFormIssueReceiptEntry), ByVal trans As SqlTransaction) As Boolean
        Try
            Dim qry As String = "delete from TSPL_ROAD_PERMIT_ISSUE_RECEIVED_LIST where doc_no='" + obj.Docno + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Dim isSaved As Boolean = True
            Dim coll As New Hashtable()

            If arr IsNot Nothing AndAlso arr.Count > 0 Then
                For Each objtr As clsFormIssueReceiptEntry In arr
                    coll = New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "doc_no", obj.Docno)
                    clsCommon.AddColumnsForChange(coll, "form_code", obj.Formcode)
                    clsCommon.AddColumnsForChange(coll, "sno", objtr.sno)
                    clsCommon.AddColumnsForChange(coll, "iss_rcv_form_no", objtr.formserialno)
                    clsCommon.AddColumnsForChange(coll, "iss_rcv_date", clsCommon.GetPrintDate(objtr.issuedate, "dd/MMM/yyyy"))
                    clsCommon.AddColumnsForChange(coll, "vendor_code", objtr.vendorcode)
                    clsCommon.AddColumnsForChange(coll, "customer_code", objtr.custcode)
                    clsCommon.AddColumnsForChange(coll, "bill_no", objtr.billno)
                    clsCommon.AddColumnsForChange(coll, "bill_date", clsCommon.GetPrintDate(objtr.billdate, "dd/MMM/yyyy"))
                    clsCommon.AddColumnsForChange(coll, "amount", objtr.amount)
                    clsCommon.AddColumnsForChange(coll, "purchaseorder_no", objtr.POno)
                    clsCommon.AddColumnsForChange(coll, "sale_invoice_no", objtr.saleinvoiceno)
                    clsCommon.AddColumnsForChange(coll, "Issue_Received", objtr.iss_rcv)
                    clsCommon.AddColumnsForChange(coll, "type", objtr.vendorcustomer_type)
                    clsCommon.AddColumnsForChange(coll, "created_by", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "created_date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
                    clsCommon.AddColumnsForChange(coll, "modified_by", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "modified_date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))


                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ROAD_PERMIT_ISSUE_RECEIVED_LIST", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If

            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function DeleteData(ByVal docno As String, ByVal trans As SqlTransaction) As Boolean
        Try
            If Not clsCommon.MyMessageBoxShow("Are you sure want to delete Issue/Receipt List Entry of " + docno + "?", "Attention", MessageBoxButtons.YesNo, RadMessageIcon.Question) = DialogResult.Yes Then
                Return False
            End If

            Dim qry As String = "delete from TSPL_ROAD_PERMIT_ISSUE_RECEIVED_LIST where doc_no='" + docno + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function GetData(ByVal docno As String) As clsFormIssueReceiptEntry
        Try
            Dim obj As New clsFormIssueReceiptEntry()
            Dim objtr As New clsFormIssueReceiptEntry()

            Dim qry As String = "select TSPL_ROAD_PERMIT_ISSUE_RECEIVED_LIST.type,TSPL_ROAD_PERMIT_ISSUE_RECEIVED_LIST.Issue_Received,TSPL_ROAD_PERMIT_ISSUE_RECEIVED_LIST.customer_code,TSPL_ROAD_PERMIT_ISSUE_RECEIVED_LIST.doc_no,TSPL_ROAD_PERMIT_ISSUE_RECEIVED_LIST.form_code,TSPL_ROAD_PERMIT_ISSUE_RECEIVED_LIST.sno,TSPL_ROAD_PERMIT_ISSUE_RECEIVED_LIST.iss_rcv_form_no,TSPL_ROAD_PERMIT_ISSUE_RECEIVED_LIST.iss_rcv_date,TSPL_ROAD_PERMIT_ISSUE_RECEIVED_LIST.vendor_code,TSPL_ROAD_PERMIT_ISSUE_RECEIVED_LIST.bill_no,TSPL_ROAD_PERMIT_ISSUE_RECEIVED_LIST.bill_date,TSPL_ROAD_PERMIT_ISSUE_RECEIVED_LIST.amount,TSPL_ROAD_PERMIT_ISSUE_RECEIVED_LIST.purchaseorder_no,TSPL_ROAD_PERMIT_ISSUE_RECEIVED_LIST.sale_invoice_no from TSPL_ROAD_PERMIT_ISSUE_RECEIVED_LIST where doc_no='" + docno + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                obj.arr = New List(Of clsFormIssueReceiptEntry)
                For Each dr As DataRow In dt.Rows
                    objtr = New clsFormIssueReceiptEntry()
                    objtr.Docno = clsCommon.myCstr(dr("doc_no"))
                    objtr.sno = CInt(dr("sno"))
                    objtr.formserialno = clsCommon.myCstr(dr("Iss_Rcv_Form_No"))
                    objtr.issuedate = clsCommon.myCDate(dr("Iss_Rcv_Date"))
                    objtr.vendorcode = clsCommon.myCstr(dr("vendor_code"))
                    objtr.custcode = clsCommon.myCstr(dr("customer_code"))
                    objtr.billno = clsCommon.myCstr(dr("bill_no"))
                    objtr.billdate = clsCommon.myCDate(dr("bill_date"))
                    objtr.amount = clsCommon.myCdbl(dr("amount"))
                    objtr.POno = clsCommon.myCstr(dr("purchaseorder_no"))
                    objtr.saleinvoiceno = clsCommon.myCstr(dr("sale_invoice_no"))
                    objtr.iss_rcv = clsCommon.myCstr(dr("Issue_Received"))
                    objtr.vendorcustomer_type = clsCommon.myCstr(dr("type"))

                    If clsCommon.myLen(objtr.Docno) > 0 Then
                        obj.arr.Add(objtr)
                    End If
                Next
            End If

            Return obj
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
End Class
