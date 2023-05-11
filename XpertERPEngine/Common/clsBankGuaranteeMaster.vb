Imports common
Imports System.Data.SqlClient
Public Class clsBankGuaranteeMaster

#Region "Variables"
    Public code As String = Nothing
    Public desc As String = Nothing
    Public docdate As String = Nothing
    Public strtdate As String = Nothing
    Public enddate As String = Nothing
    Public extnddate As String = Nothing
    Public bankcode As String = Nothing
    Public bankdesc As String = Nothing
    Public vndrcode As String = Nothing
    Public vndrname As String = Nothing
    Public remarks As String = Nothing
    Public rimnder As String = Nothing
    Public amount As String = Nothing
    Public post As String = Nothing
    Public extndreminder As String = Nothing
    '' Anubhooti 24-Sep-2014 BM00000004063
    Public Type As String = Nothing
    '' Rohit 28-Oct-2014 
    Public Bank_Guarantee_Type As String = Nothing
    Public Receiving_code As String = Nothing

#End Region

    Public Shared Function GetData(ByVal strcompid As String, ByVal Navtype As NavigatorType) As clsBankGuaranteeMaster
        Dim obj As clsBankGuaranteeMaster = Nothing
        Dim qry As String = "select distinct * from tspl_bank_guarantee_master where comp_code='" + objCommonVar.CurrentCompanyCode + "' and "

        Select Case Navtype
            Case NavigatorType.Current
                qry += "  docno='" + strcompid + "'"
            Case NavigatorType.Next
                qry += " docno in (select min(t.docno) from tspl_bank_guarantee_master  as t where t.docno  >'" + strcompid + "')"
            Case NavigatorType.First
                qry += " docno in (select min(t.docno ) from tspl_bank_guarantee_master  as t )"
            Case NavigatorType.Last
                qry += " docno in (select max(t.docno ) from tspl_bank_guarantee_master  as t )"
            Case NavigatorType.Previous
                qry += " docno in (select max(t.docno ) from tspl_bank_guarantee_master  as t where t.docno  <'" + strcompid + "')"
        End Select

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            obj = New clsBankGuaranteeMaster
            obj.code = clsCommon.myCstr(dt.Rows(0)("docno"))
            obj.docdate = clsCommon.myCDate(dt.Rows(0)("date"))
            obj.desc = clsCommon.myCstr(dt.Rows(0)("description"))
            obj.strtdate = clsCommon.myCstr(dt.Rows(0)("start_date"))
            obj.enddate = clsCommon.myCstr(dt.Rows(0)("end_date"))
            obj.extnddate = clsCommon.myCstr(dt.Rows(0)("extended_date"))
            obj.bankcode = clsCommon.myCstr(dt.Rows(0)("bank_code"))
            obj.bankdesc = clsDBFuncationality.getSingleValue("select distinct description from tspl_bank_master where bank_code='" + obj.bankcode + "'")
            obj.vndrcode = clsCommon.myCstr(dt.Rows(0)("vendor_code"))
            obj.vndrname = clsDBFuncationality.getSingleValue("select distinct vendor_name from tspl_vendor_master where vendor_code='" + obj.vndrcode + "'")
            obj.amount = clsCommon.myCstr(dt.Rows(0)("amount"))
            obj.remarks = clsCommon.myCstr(dt.Rows(0)("remarks"))
            obj.rimnder = clsCommon.myCstr(dt.Rows(0)("reminder_days"))
            obj.post = clsCommon.myCstr(dt.Rows(0)("status"))
            obj.extndreminder = clsCommon.myCstr(dt.Rows(0)("Extnd_Reminder_Days"))
            '' Anubhooti 24-Sep-2014 BM00000004063
            obj.Type = clsCommon.myCstr(dt.Rows(0)("Type"))
            obj.Bank_Guarantee_Type = clsCommon.myCstr(dt.Rows(0)("Bank_Guarantee_Type"))
            obj.Receiving_code = clsCommon.myCstr(dt.Rows(0)("Receiving_code"))
        End If
        Return obj
    End Function


    Public Shared Function SaveData(ByVal obj As clsBankGuaranteeMaster, ByVal isNewEntry As Boolean) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Dim isSaved As Boolean = True
        Try
            Dim coll As New Hashtable()
            If clsCommon.myLen(obj.code) <= 0 AndAlso isNewEntry Then
                obj.code = clsCommon.myCstr(clsERPFuncationality.GetNextCode(trans, clsCommon.GetPrintDate(obj.docdate, "dd/MM/yyyy"), clsDocType.BankGuaranteeMaster, "", ""))
            End If

            clsCommon.AddColumnsForChange(coll, "comp_code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "docno", obj.code)
            clsCommon.AddColumnsForChange(coll, "date", obj.docdate)
            clsCommon.AddColumnsForChange(coll, "description", obj.desc)
            clsCommon.AddColumnsForChange(coll, "start_date", clsCommon.GetPrintDate(obj.strtdate, "dd/MM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "end_date", clsCommon.GetPrintDate(obj.enddate, "dd/MM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "extended_date", clsCommon.GetPrintDate(obj.extnddate, "dd/MM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "bank_code", obj.bankcode)
            clsCommon.AddColumnsForChange(coll, "bank_desc", obj.bankdesc)
            clsCommon.AddColumnsForChange(coll, "vendor_code", obj.vndrcode)
            clsCommon.AddColumnsForChange(coll, "vendor_name", obj.vndrname)
            clsCommon.AddColumnsForChange(coll, "amount", obj.amount)
            clsCommon.AddColumnsForChange(coll, "reminder_days", obj.rimnder)
            clsCommon.AddColumnsForChange(coll, "Extnd_Reminder_Days", obj.extndreminder)
            '' Anubhooti 24-Sep-2014 BM00000004063
            clsCommon.AddColumnsForChange(coll, "Type", obj.Type)
            'Rohit
            clsCommon.AddColumnsForChange(coll, "Bank_Guarantee_Type", obj.Bank_Guarantee_Type)
            clsCommon.AddColumnsForChange(coll, "Receiving_code", obj.Receiving_code)
            '==========
            clsCommon.AddColumnsForChange(coll, "remarks", obj.remarks)
            clsCommon.AddColumnsForChange(coll, "modify_by", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "modify_date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "created_by", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "created_date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
            If isNewEntry Then
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "tspl_bank_guarantee_master", OMInsertOrUpdate.Insert, "", trans)
            Else
                Dim qry As String = "delete from tspl_bank_guarantee_master where docno='" + obj.code + "' and comp_code='" + objCommonVar.CurrentCompanyCode + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)

                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "tspl_bank_guarantee_master", OMInsertOrUpdate.Insert, "", trans)
            End If
            trans.Commit()

        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function

    Public Shared Function DeleteData(ByVal strcode As String) As Boolean
        Try
            Dim qry As String = "delete from tspl_bank_guarantee_master where docno='" + strcode + "' and comp_code='" + objCommonVar.CurrentCompanyCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
End Class
