Imports common
Imports System.Data.SqlClient

Public Class clsMediclaimEntry

#Region "Variables"
    Public docno As String = Nothing
    Public docdate As String = Nothing
    Public description As String = Nothing
    Public emp_code As String = Nothing
    Public frmdate As String = Nothing
    Public todate As String = Nothing
    Public toalamount As String = Nothing
    Public post_status As String = Nothing
    Public yearname As String = Nothing
    Public monthname As String = Nothing
    Public days As String = Nothing
    Public basicamt As String = Nothing
    Public claimamt As String = Nothing
    Public Arr As List(Of clsMediclaimEntry) = Nothing
    Public empname As String = Nothing
    Public desig As String = Nothing
    Public depart As String = Nothing
    Public doj As String = Nothing
    Public monthdays As String = Nothing
#End Region

    Public Shared Function SaveData(ByVal obj As clsMediclaimEntry, ByVal strDocNo As String, ByVal Arr As List(Of clsMediclaimEntry)) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim issaved As Boolean = True

            Dim coll As New Hashtable()

            Dim qry As String = "select count(*) from TSPL_MEDICLAIM_HEAD where comp_code='" + objCommonVar.CurrentCompanyCode + "' and document_code='" + strDocNo + "'"
            Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
            If check > 0 Then
                qry = "delete from TSPL_MEDICLAIM_HEAD where comp_code='" + objCommonVar.CurrentCompanyCode + "' and document_code='" + strDocNo + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)

                qry = "delete from tspl_mediclaim_detail where document_code='" + strDocNo + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
            Else
                obj.docno = clsERPFuncationality.GetNextCode(trans, obj.docdate, clsDocType.EmpMediclaim, "", "")
            End If


            clsCommon.AddColumnsForChange(coll, "comp_code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "document_code", obj.docno)
            clsCommon.AddColumnsForChange(coll, "date", obj.docdate)
            clsCommon.AddColumnsForChange(coll, "description", obj.description)
            clsCommon.AddColumnsForChange(coll, "emp_code", obj.emp_code)
            clsCommon.AddColumnsForChange(coll, "fromdate", obj.frmdate)
            clsCommon.AddColumnsForChange(coll, "todate", obj.todate)
            clsCommon.AddColumnsForChange(coll, "total_amount", obj.toalamount)
            clsCommon.AddColumnsForChange(coll, "created_by", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "created_date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "modified_by", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "modified_date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))

            issaved = issaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MEDICLAIM_HEAD", OMInsertOrUpdate.Insert, "", trans)

            If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                For Each objtr As clsMediclaimEntry In obj.Arr
                    coll = New Hashtable()

                    clsCommon.AddColumnsForChange(coll, "document_code", obj.docno)
                    clsCommon.AddColumnsForChange(coll, "emp_code", objtr.emp_code)
                    clsCommon.AddColumnsForChange(coll, "yearname", objtr.yearname)
                    clsCommon.AddColumnsForChange(coll, "monthname", objtr.monthname)
                    Try
                        Convert.ToDecimal(objtr.days)
                    Catch ex As Exception
                        objtr.days = "0"
                    End Try


                    Try
                        Convert.ToDecimal(objtr.basicamt)
                    Catch ex As Exception
                        objtr.basicamt = "0"
                    End Try

                    Try
                        Convert.ToDecimal(objtr.claimamt)
                    Catch ex As Exception
                        objtr.claimamt = "0"
                    End Try

                    clsCommon.AddColumnsForChange(coll, "monthdays", objtr.monthdays)
                    clsCommon.AddColumnsForChange(coll, "presntdays", objtr.days)
                    clsCommon.AddColumnsForChange(coll, "basicamt", objtr.basicamt)
                    clsCommon.AddColumnsForChange(coll, "claimamt", objtr.claimamt)

                    issaved = issaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "tspl_mediclaim_detail", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function GetData(ByVal strcode As String, ByVal Navtype As NavigatorType) As clsMediclaimEntry
        Try
            Dim qry As String = "select TSPL_MEDICLAIM_HEAD.document_code,TSPL_MEDICLAIM_HEAD.date,TSPL_MEDICLAIM_HEAD.description,TSPL_MEDICLAIM_HEAD.emp_code,TSPL_MEDICLAIM_HEAD.fromdate,TSPL_MEDICLAIM_HEAD.todate,TSPL_MEDICLAIM_HEAD.total_amount,TSPL_MEDICLAIM_HEAD.status,TSPL_EMPLOYEE_MASTER.emp_name,TSPL_EMPLOYEE_MASTER.birth_date,TSPL_EMPLOYEE_MASTER.joining_date,TSPL_DESIGNATION_MASTER.designation_desc,TSPL_DEPARTMENT_MASTER.department_name from TSPL_MEDICLAIM_HEAD left outer join TSPL_EMPLOYEE_MASTER on TSPL_EMPLOYEE_MASTER.emp_code=TSPL_MEDICLAIM_HEAD.emp_code left outer join TSPL_DESIGNATION_MASTER on TSPL_DESIGNATION_MASTER.designation_id=TSPL_EMPLOYEE_MASTER.designation left outer join TSPL_DEPARTMENT_MASTER on TSPL_DEPARTMENT_MASTER.department_code=TSPL_EMPLOYEE_MASTER.department_code where TSPL_MEDICLAIM_HEAD.comp_code='" + objCommonVar.CurrentCompanyCode + "'"
            Dim whrcls As String = " and TSPL_MEDICLAIM_HEAD.document_code='" + strcode + "'"

            Select Case Navtype
                Case NavigatorType.First
                    qry += " and TSPL_MEDICLAIM_HEAD.document_code = (select MIN(document_code) from TSPL_MEDICLAIM_HEAD where TSPL_MEDICLAIM_HEAD.comp_code='" + objCommonVar.CurrentCompanyCode + "')"
                Case NavigatorType.Last
                    qry += " and TSPL_MEDICLAIM_HEAD.document_code = (select Max(document_code) from TSPL_MEDICLAIM_HEAD where TSPL_MEDICLAIM_HEAD.comp_code='" + objCommonVar.CurrentCompanyCode + "')"
                Case NavigatorType.Next
                    qry += " and TSPL_MEDICLAIM_HEAD.document_code = (select Min(document_code) from TSPL_MEDICLAIM_HEAD where document_code>'" + strcode + "' and TSPL_MEDICLAIM_HEAD.comp_code='" + objCommonVar.CurrentCompanyCode + "')"
                Case NavigatorType.Previous
                    qry += " and TSPL_MEDICLAIM_HEAD.document_code = (select Max(document_code) from TSPL_MEDICLAIM_HEAD where document_code<'" + strcode + "' and TSPL_MEDICLAIM_HEAD.comp_code='" + objCommonVar.CurrentCompanyCode + "')"
                Case NavigatorType.Current
                    qry += " and TSPL_MEDICLAIM_HEAD.document_code = '" + strcode + "'"
            End Select

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            Dim obj As New clsMediclaimEntry()

            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj = New clsMediclaimEntry()

                obj.docno = clsCommon.myCstr(dt.Rows(0)("document_code"))
                obj.docdate = clsCommon.myCstr(dt.Rows(0)("date"))
                obj.description = clsCommon.myCstr(dt.Rows(0)("description"))
                obj.emp_code = clsCommon.myCstr(dt.Rows(0)("emp_code"))
                obj.empname = clsCommon.myCstr(dt.Rows(0)("emp_name"))
                obj.desig = clsCommon.myCstr(dt.Rows(0)("designation_desc"))
                obj.depart = clsCommon.myCstr(dt.Rows(0)("department_name"))
                obj.doj = clsCommon.myCstr(dt.Rows(0)("joining_date"))
                obj.frmdate = clsCommon.myCstr(dt.Rows(0)("fromdate"))
                obj.todate = clsCommon.myCstr(dt.Rows(0)("todate"))
                obj.toalamount = clsCommon.myCstr(dt.Rows(0)("total_amount"))
                obj.post_status = clsCommon.myCstr(dt.Rows(0)("status"))

            End If


            qry = "select TSPL_MEDICLAIM_DETAIL.yearname,TSPL_MEDICLAIM_DETAIL.monthname,TSPL_MEDICLAIM_DETAIL.presntdays,TSPL_MEDICLAIM_DETAIL.basicamt,TSPL_MEDICLAIM_DETAIL.claimamt,TSPL_MEDICLAIM_DETAIL.monthdays from TSPL_MEDICLAIM_DETAIL where TSPL_MEDICLAIM_DETAIL.document_code='" + obj.docno + "'"
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry)

            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj.Arr = New List(Of clsMediclaimEntry)
                Dim objtr As clsMediclaimEntry

                For Each dr As DataRow In dt.Rows
                    objtr = New clsMediclaimEntry
                    objtr.yearname = clsCommon.myCstr(dr("yearname"))
                    objtr.monthname = clsCommon.myCstr(dr("monthname"))
                    objtr.days = clsCommon.myCstr(dr("presntdays"))
                    objtr.basicamt = clsCommon.myCstr(dr("basicamt"))
                    objtr.claimamt = clsCommon.myCstr(dr("claimamt"))
                    objtr.monthdays = clsCommon.myCstr(dr("monthdays"))
                    obj.Arr.Add(objtr)
                Next
            End If

            Return obj
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

End Class
