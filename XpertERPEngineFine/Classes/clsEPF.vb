Imports common
Imports System.Data.SqlClient
Public Class clsEPF
#Region "Variables"
    Public Doc_Code As String
    Public DOC_DATE As Date?
    Public Location_Code As String
    Public Location_desc As String
    Public Pay_period_code As String
    Public Pay_period_Name As String
    Public Remarks As String
    Public Created_By As String
    Public Created_Date As Date?
    Public Modify_By As String
    Public Modify_Date As Date?
    Public Posting_Date As Date
    Public Status As String = Nothing
    Public arr_epfentry As List(Of clsEPFEntry) = Nothing
#End Region
    Public Shared Function SaveData(ByVal obj As clsEPF, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim coll As New Hashtable()
        Try
            If isNewEntry Then
                obj.Doc_Code = clsERPFuncationality.GetNextCode(trans, obj.DOC_DATE, clsDocType.EPF, "", "")
            End If

            clsCommon.AddColumnsForChange(coll, "Doc_Code", obj.Doc_Code)
            clsCommon.AddColumnsForChange(coll, "Doc_Date", clsCommon.GetPrintDate(obj.DOC_DATE, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks)
            clsCommon.AddColumnsForChange(coll, "Location_code", obj.Location_Code)
            clsCommon.AddColumnsForChange(coll, "Pay_Period_Code", obj.Pay_period_code)
            clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))

                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_EPF_ENTRY", OMInsertOrUpdate.Insert, "", trans)
            Else

                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_EPF_ENTRY", OMInsertOrUpdate.Update, "  Doc_Code='" + obj.Doc_Code + "'", trans)
            End If
            clsEPFEntry.SaveData(obj.Doc_Code, obj.arr_epfentry, trans)
            'clsProductionEntry.SaveData(obj.document_code, obj.Arr_Prod, trans)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            coll = Nothing
        End Try
    End Function
    Public Shared Function PostData(ByVal FormId As String, ByVal strDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Document No not found to Post")
            End If
            Dim strPostDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt")
            Dim obj As clsEPF = clsEPF.GetData(strDocNo, NavigatorType.Current, trans)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.Doc_Code) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            If (obj.Status = "1") Then
                Throw New Exception("Already Posted")
            End If
            Dim qry As String = "Update TSPL_EPF_ENTRY set Status=1, Posted_Date='" + strPostDate + "',Posted_By='" + objCommonVar.CurrentUserCode + "'  where Doc_code ='" + strDocNo + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            'clsDBFuncationality.ExecuteNonQuery("Update TSPL_PROD_QC_CHECK_HEAD set posted='1', Modified_By = '" + objCommonVar.CurrentUserCode + "',Modified_Date = '" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "yyyy-MM-dd") + "'  where document_code='" & obj.document_code & "'", trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, Optional ByVal trans As SqlTransaction = Nothing) As clsEPF
        Dim dt As New DataTable()
        Dim dt1 As New DataTable()
        Dim dt2 As New DataTable()
        'Dim objtr As New clsQualityCheckForSRNDetail()
        Dim objpd As New clsEPFEntry()

        'Dim objtr_Detail As New clsQualityCheckDetail()
        Try
            Dim obj As New clsEPF()
            obj.arr_epfentry = New List(Of clsEPFEntry)
            Dim qry As String = "select doc_code,doc_date,TSPL_EPF_ENTRY.location_code, TSPL_EPF_ENTRY.pay_period_code,TSPL_PAYPERIOD_MASTER.PAY_PERIOD_NAME,TSPL_LOCATION_MASTER.Location_Desc,status,TSPL_EPF_ENTRY.remarks from TSPL_EPF_ENTRY
                                left outer join TSPL_PAYPERIOD_MASTER on TSPL_PAYPERIOD_MASTER.PAY_PERIOD_CODE=TSPL_EPF_ENTRY.PAY_PERIOD_CODE
                                left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_EPF_ENTRY.Location_Code "
            Select Case NavType
                Case NavigatorType.Current
                    qry += " where TSPL_EPF_ENTRY.doc_code='" + strCode + "'"
                Case NavigatorType.First
                    qry += " where TSPL_EPF_ENTRY.doc_code in (select min(doc_code) from TSPL_EPF_ENTRY )"
                Case NavigatorType.Last
                    qry += " where TSPL_EPF_ENTRY.doc_code in (select max(doc_code) from TSPL_EPF_ENTRY )"
                Case NavigatorType.Next
                    qry += "where TSPL_EPF_ENTRY.doc_code in (select min(doc_code) from TSPL_EPF_ENTRY where  doc_code>'" + strCode + "')"
                Case NavigatorType.Previous
                    qry += " where TSPL_EPF_ENTRY.doc_code in (select max(doc_code) from TSPL_EPF_ENTRY Where doc_code <'" + strCode + "')"
            End Select
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                obj.Doc_Code = clsCommon.myCstr(dt.Rows(0)("doc_code"))
                obj.DOC_DATE = clsCommon.myCDate(dt.Rows(0)("doc_Date"))
                obj.Location_Code = clsCommon.myCstr(dt.Rows(0)("Location_Code"))
                obj.Pay_period_code = clsCommon.myCstr(dt.Rows(0)("Pay_period_code"))
                obj.Pay_period_Name = clsCommon.myCstr(dt.Rows(0)("PAY_PERIOD_NAME"))
                obj.Remarks = clsCommon.myCstr(dt.Rows(0)("Remarks"))
                obj.Location_desc = clsCommon.myCstr(dt.Rows(0)("location_desc"))
                obj.Status = clsCommon.myCstr(dt.Rows(0)("Status"))
                qry = "select TSPL_EPF_ENTRY_detail.emp_code,TSPL_EPF_ENTRY_detail.COEPF_AC01,COEPS_AC10,Adm_EPF_ACEPF_AC02,EDLI_COM_AC21,Adm_EDLI_ACEDLI_AC22,EMP_EPF_AC01,MAX_EPF_AMT,OTHER_CHARGES,MAX_OTHER_CHARGES,MAX_EPS_AMT,MAX_ACEPF_AMT,MAX_EDLI,MIN_ACEDLI,MAX_ACEDLI,TSPL_EMPLOYEE_MASTER.Emp_Name,TSPL_EPF_ENTRY_detail.Actual_Amount,TSPL_EPF_ENTRY_detail.HeadValue from TSPL_EPF_ENTRY_detail
                        left outer join TSPL_EMPLOYEE_MASTER on TSPL_EMPLOYEE_MASTER.EMP_CODE=TSPL_EPF_ENTRY_detail.EMP_CODE "
                qry += " where TSPL_EPF_ENTRY_detail.doc_code ='" + obj.Doc_Code + "'"
                dt1 = New DataTable()
                dt1 = clsDBFuncationality.GetDataTable(qry, trans)

                For Each dr As DataRow In dt1.Rows
                    objpd = New clsEPFEntry()

                    'objpd.Line_No = clsCommon.myCstr(dr("SNo"))
                    objpd.EmployeeCode = clsCommon.myCstr(dr("EMP_CODE"))
                    objpd.EmployeeName = clsCommon.myCstr(dr("Emp_Name"))
                    objpd.COEPF_A01 = clsCommon.myCDecimal(dr("COEPF_AC01"))
                    objpd.COEPS_AC10 = clsCommon.myCDecimal(dr("COEPS_AC10"))
                    objpd.Adm_EPFACEPF_AC02 = clsCommon.myCDecimal(dr("Adm_EPF_ACEPF_AC02"))
                    objpd.EDLI_COM_AC21 = clsCommon.myCDecimal(dr("EDLI_COM_AC21"))
                    objpd.Adm_EDLIACEDLI_AC22 = clsCommon.myCDecimal(dr("Adm_EDLI_ACEDLI_AC22"))
                    objpd.EMP_EPF_AC01 = clsCommon.myCDecimal(dr("EMP_EPF_AC01"))
                    objpd.MAX_EPF_AMT = clsCommon.myCDecimal(dr("MAX_EPF_AMT"))
                    objpd.OTHER_CHARGES = clsCommon.myCDecimal(dr("OTHER_CHARGES"))
                    objpd.MAX_OTHER_CHARGES = clsCommon.myCDecimal(dr("MAX_OTHER_CHARGES"))
                    objpd.MAX_EPS_AMT = clsCommon.myCDecimal(dr("MAX_EPS_AMT"))
                    objpd.MAX_ACEPF_AMT = clsCommon.myCDecimal(dr("MAX_ACEPF_AMT"))
                    objpd.MAX_EDLI = clsCommon.myCDecimal(dr("MAX_EDLI"))
                    objpd.MIN_ACEDLI = clsCommon.myCDecimal(dr("MIN_ACEDLI"))
                    objpd.MAX_ACEDLI = clsCommon.myCDecimal(dr("MAX_ACEDLI"))
                    objpd.ActualAmt = clsCommon.myCDecimal(dr("Actual_Amount"))
                    objpd.headvalue = clsCommon.myCDecimal(dr("HeadValue"))
                    obj.arr_epfentry.Add(objpd)
                Next
            End If 'dt1 cond.
            Return obj
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Shared Function ReverseAndUnpost(ByVal strCode As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim obj As clsEPF = clsEPF.GetData(strCode, NavigatorType.Current, trans)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.Doc_Code) <= 0) Then
                Throw New Exception("Document No not found to Post")
            End If
            If Not (obj.Status = ERPTransactionStatus.Approved) Then
                Throw New Exception("Transaction status should be posted.")
            End If
            Dim qry As String
            If obj.Status = 1 Then
                qry = "update TSPL_EPF_ENTRY set Status=0,Posted_Date=null,Posted_By=null where doc_code='" + strCode + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
            End If
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim isSaved As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Dim obj As New clsEPF()
        Try
            isSaved = False

            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Code not found to Delete")
            End If

            Dim isPosted As Integer = 0
            isPosted = clsDBFuncationality.getSingleValue("SELECT Count(*) FROM TSPL_EPF_ENTRY where Doc_code = '" & strCode & "' and Status=1", trans)
            If (isPosted = 1) Then
                Throw New Exception("Already Posted on :" + obj.Posting_Date)
            End If

            Dim qry As String
            qry = "delete from TSPL_EPF_ENTRY_detail where Doc_code ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "DELETE FROM TSPL_EPF_ENTRY WHERE Doc_code='" + strCode + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message.ToString())
        End Try
        Return isSaved
    End Function
End Class
Public Class clsEPFEntry
#Region "variables"
    Public Document_Code As String = Nothing
    Public EmployeeCode As String = Nothing
    Public EmployeeName As String = Nothing
    Public COEPF_A01 As Decimal = 0
    Public COEPS_AC10 As Decimal = 0
    Public Adm_EPFACEPF_AC02 As Decimal = 0
    Public EDLI_COM_AC21 As Decimal = 0
    Public Adm_EDLIACEDLI_AC22 As Decimal = 0
    Public EMP_EPF_AC01 As Decimal = 0
    Public MAX_EPF_AMT As Decimal = 0
    Public OTHER_CHARGES As Decimal = 0
    Public MAX_OTHER_CHARGES As Decimal = 0
    Public MAX_EPS_AMT As Decimal = 0
    Public MAX_ACEPF_AMT As Decimal = 0
    Public MAX_EDLI As Decimal = 0
    Public MIN_ACEDLI As Decimal = 0
    Public MAX_ACEDLI As Decimal = 0
    Public ActualAmt As Decimal = 0
    Public headvalue As Decimal = 0
#End Region
    Public Shared Function SaveData(ByVal strCode As String, ByVal arr As List(Of clsEPFEntry), ByVal trans As SqlTransaction) As Boolean
        Dim coll As New Hashtable()
        Try
            Dim qry As String = "delete from TSPL_EPF_ENTRY_detail where doc_code='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            If arr IsNot Nothing AndAlso arr.Count > 0 Then
                For Each objtr As clsEPFEntry In arr
                    coll = New Hashtable()
                    'clsCommon.AddColumnsForChange(coll, "PK_Id", objtr.PK_Id)
                    clsCommon.AddColumnsForChange(coll, "Doc_Code", strCode)
                    clsCommon.AddColumnsForChange(coll, "EMP_CODE", objtr.EmployeeCode)
                    clsCommon.AddColumnsForChange(coll, "COEPF_AC01", objtr.COEPF_A01)
                    clsCommon.AddColumnsForChange(coll, "COEPS_AC10", objtr.COEPS_AC10)
                    clsCommon.AddColumnsForChange(coll, "Adm_EPF_ACEPF_AC02", objtr.Adm_EPFACEPF_AC02)
                    clsCommon.AddColumnsForChange(coll, "EDLI_COM_AC21", objtr.EDLI_COM_AC21)
                    clsCommon.AddColumnsForChange(coll, "Adm_EDLI_ACEDLI_AC22", objtr.Adm_EDLIACEDLI_AC22)
                    clsCommon.AddColumnsForChange(coll, "EMP_EPF_AC01", objtr.EMP_EPF_AC01)
                    clsCommon.AddColumnsForChange(coll, "MAX_EPF_AMT", objtr.MAX_EPF_AMT)
                    clsCommon.AddColumnsForChange(coll, "OTHER_CHARGES", objtr.OTHER_CHARGES)
                    clsCommon.AddColumnsForChange(coll, "MAX_OTHER_CHARGES", objtr.MAX_OTHER_CHARGES)
                    clsCommon.AddColumnsForChange(coll, "MAX_EPS_AMT", objtr.MAX_EPS_AMT)
                    clsCommon.AddColumnsForChange(coll, "MAX_ACEPF_AMT", objtr.MAX_ACEPF_AMT)
                    clsCommon.AddColumnsForChange(coll, "MAX_EDLI", objtr.MAX_EDLI)
                    clsCommon.AddColumnsForChange(coll, "MIN_ACEDLI", objtr.MIN_ACEDLI)
                    clsCommon.AddColumnsForChange(coll, "MAX_ACEDLI", objtr.MAX_ACEDLI)
                    clsCommon.AddColumnsForChange(coll, "Actual_Amount", objtr.ActualAmt)
                    clsCommon.AddColumnsForChange(coll, "HeadValue", objtr.headvalue)
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_EPF_ENTRY_detail", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            coll = Nothing
        End Try
    End Function
End Class

