Imports common
Imports System.Data.SqlClient
Imports XpertERPEngine

Public Class clsCartMaintenanceEntry

#Region "Variables Declaration"
    Public docno As String = Nothing
    Public docdate As String = Nothing
    Public description As String = Nothing
    Public custcode As String = Nothing
    Public assetno As String = Nothing
    Public sparecode As String = Nothing
    Public values As String = Nothing
    Public status As String = Nothing
    Public newdocdate As String = Nothing
    Public rate As Decimal = Nothing
    Public amt As Decimal = Nothing
#End Region

    Public Shared Function SaveData(ByVal strcode As String, ByVal isNewEntry As Boolean, ByVal Arr As List(Of clsCartMaintenanceEntry), ByVal trans As SqlTransaction, ByRef rcode As String) As Boolean
        Dim isSaved As Boolean = True

        Try


            If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
                Dim counter As Integer = 1
                For Each obj As clsCartMaintenanceEntry In Arr
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "comp_code", objCommonVar.CurrentCompanyCode)

                    If isNewEntry = True AndAlso counter = 1 Then
                        obj.docno = clsERPFuncationality.GetNextCode(trans, obj.newdocdate, clsDocType.CartMaintenance, "", "")
                        strcode = obj.docno
                        rcode = obj.docno
                    ElseIf counter > 1 Then
                        obj.docno = strcode
                        rcode = obj.docno
                    End If

                    If isNewEntry = False Then
                        obj.docno = strcode
                        rcode = strcode
                    End If

                    clsCommon.AddColumnsForChange(coll, "docno", obj.docno)
                    clsCommon.AddColumnsForChange(coll, "created_by", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "created_date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy"))



                    clsCommon.AddColumnsForChange(coll, "date", clsCommon.GetPrintDate(obj.docdate, "dd/MMM/yyyy"))
                    clsCommon.AddColumnsForChange(coll, "description", obj.description)
                    clsCommon.AddColumnsForChange(coll, "cust_code", obj.custcode)
                    clsCommon.AddColumnsForChange(coll, "modify_by", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "modify_date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy"))
                    clsCommon.AddColumnsForChange(coll, "asset_no", obj.assetno)
                    clsCommon.AddColumnsForChange(coll, "spare_code", obj.sparecode)
                    clsCommon.AddColumnsForChange(coll, "asset_value", obj.values)
                    clsCommon.AddColumnsForChange(coll, "Rate", obj.rate)
                    clsCommon.AddColumnsForChange(coll, "Net_Amt", obj.amt)


                    If (clsCommon.myLen(obj.docno) = 0) Then
                        Throw New Exception("Please Fill Document No. Of Cart Maintenance Entry")
                        isSaved = False
                    End If

                    If isNewEntry = False AndAlso counter = 1 Then
                        Dim qry1 As String
                        qry1 = "delete from TSPL_CART_MAINTENANCE where comp_code='" + objCommonVar.CurrentCompanyCode + "' and docno='" + obj.docno + "'"
                        clsDBFuncationality.ExecuteNonQuery(qry1)
                    End If
                    counter += 1
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CART_MAINTENANCE", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If


        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function GetData(ByVal strcompid As String, ByVal Navtype As NavigatorType) As clsCartMaintenanceEntry
        Dim obj As clsCartMaintenanceEntry = Nothing
        Dim qry As String = "select distinct docno,date,description,cust_code,status from TSPL_CART_MAINTENANCE where comp_code='" + objCommonVar.CurrentCompanyCode + "' and "

        Select Case Navtype
            Case NavigatorType.Current
                qry += "  docno='" + strcompid + "'"
            Case NavigatorType.Next
                qry += " docno in (select min(t.docno) from TSPL_CART_MAINTENANCE  as t where t.docno  >'" + strcompid + "')"
            Case NavigatorType.First
                qry += " docno in (select min(t.docno ) from TSPL_CART_MAINTENANCE  as t )"
            Case NavigatorType.Last
                qry += " docno in (select max(t.docno ) from TSPL_CART_MAINTENANCE  as t )"
            Case NavigatorType.Previous
                qry += " docno in (select max(t.docno ) from TSPL_CART_MAINTENANCE  as t where t.docno  <'" + strcompid + "')"
        End Select

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            obj = New clsCartMaintenanceEntry
            obj.docno = clsCommon.myCstr(dt.Rows(0)("docno"))
            obj.docdate = clsCommon.myCDate(dt.Rows(0)("date"))
            obj.description = clsCommon.myCstr(dt.Rows(0)("description"))
            obj.status = clsCommon.myCstr(dt.Rows(0)("status"))

            obj.custcode = clsCommon.myCstr(dt.Rows(0)("cust_code"))
        End If
        Return obj
    End Function
End Class
