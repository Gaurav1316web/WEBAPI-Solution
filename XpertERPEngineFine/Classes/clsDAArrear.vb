Imports common
Imports System.Data.SqlClient
Public Class clsDAArrear
#Region "Variable"
    Public document_code As String = Nothing
    Public document_date As Date? = Nothing
    Public Applicable_date As Date? = Nothing
    Public Pay_Period As String = Nothing
    Public Location As String = Nothing
    Public LocationDesc As String = Nothing
    Public DA_Arrear As Double = 0
    Public Status As String = Nothing
    Public ArrD As List(Of ClsDAArrearDetail) = Nothing
    Public Arr_PayPeriod As List(Of clsPayPeriod_detail) = Nothing
    Public Arr_Location As List(Of clsDALocation_detail) = Nothing
    Public Template_Status As String = Nothing
    Public Posting_Date As DateTime?
    Public Fromdate As DateTime?
    Public Todate As DateTime?
#End Region
    Public Shared Function SaveData(ByVal obj As clsDAArrear, ByVal isNewEntry As Boolean) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            SaveData(obj, isNewEntry, trans)
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Shared Function SaveData(ByVal obj As clsDAArrear, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim coll As New Hashtable()
        Try
            If isNewEntry Then
                obj.document_code = clsERPFuncationality.GetNextCode(trans, obj.document_date, clsDocType.DAArrear, "", "")
            End If

            clsCommon.AddColumnsForChange(coll, "Document_Code", obj.document_code)
            clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(obj.document_date, "dd/MMM/yyyy"))
            'clsCommon.AddColumnsForChange(coll, "Location", obj.Location)
            clsCommon.AddColumnsForChange(coll, "Pay_Period", obj.Pay_Period)
            clsCommon.AddColumnsForChange(coll, "DA_Arrear", obj.DA_Arrear)
            clsCommon.AddColumnsForChange(coll, "Applicable_date", clsCommon.GetPrintDate(obj.Applicable_date, "dd/MMM/yyyy hh:mm:ss tt "))
            clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))

                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_DA_Arrear_Header", OMInsertOrUpdate.Insert, "", trans)
            Else

                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_DA_Arrear_Header", OMInsertOrUpdate.Update, "  Document_Code='" + obj.document_code + "'", trans)
            End If
            ClsDAArrearDetail.SaveData(obj.document_code, obj.ArrD, trans)

            clsPayPeriod_detail.SaveData(obj.document_code, obj.Arr_PayPeriod, trans)
            clsDALocation_detail.SaveData(obj.document_code, obj.Arr_Location, trans)
            clsCommonFunctionality.SaveHistoryData(EnumSaveType.History, objCommonVar.CurrentUserCode, obj.document_code, "TSPL_DA_Arrear_Header", "Document_Code", "TSPL_DA_Arrear_Detail", "Document_Code", "TSPL_DAAREAR_PAYPERIOD_DETAIL", "Document_Code", "TSPL_DAAREAR_Location_DETAIL", "Document_Code", "", "", "", "", "", "", trans)

            'ClsRmProcessLossDetail.SaveData(obj.document_code, obj.Arr_Pd, trans)
            'clsProductionEntry.SaveData(obj.document_code, obj.Arr_Prod, trans)
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            coll = Nothing
        End Try
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, Optional ByVal trans As SqlTransaction = Nothing) As clsDAArrear
        Dim dt As New DataTable()
        Dim dt1 As New DataTable()
        Dim dt2 As New DataTable()
        Dim objpd As New ClsDAArrearDetail()
        Try
            Dim obj As New clsDAArrear()
            obj.ArrD = New List(Of ClsDAArrearDetail)
            Dim qry As String = "select TSPL_DA_Arrear_Header.document_Code,Document_Date,TSPL_DA_Arrear_Header.location,Status,DA_Arrear,Location_Desc,Pay_Period,TSPL_DA_Arrear_Header.Applicable_date from TSPL_DA_Arrear_Header
            left outer join tspl_location_master on tspl_location_master.location_code=TSPL_DA_Arrear_Header.location "
            Select Case NavType
                Case NavigatorType.Current
                    qry += " where TSPL_DA_Arrear_Header.document_code='" + strCode + "'"
                Case NavigatorType.First
                    qry += " where TSPL_DA_Arrear_Header.document_code in (select min(document_code) from TSPL_DA_Arrear_Header )"
                Case NavigatorType.Last
                    qry += " where TSPL_DA_Arrear_Header.document_code in (select max(document_code) from TSPL_DA_Arrear_Header )"
                Case NavigatorType.Next
                    qry += "where TSPL_DA_Arrear_Header.document_code in (select min(document_code) from TSPL_DA_Arrear_Header where  document_code>'" + strCode + "')"
                Case NavigatorType.Previous
                    qry += " where TSPL_DA_Arrear_Header.document_code in (select max(document_code) from TSPL_DA_Arrear_Header Where document_code <'" + strCode + "')"
            End Select
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                obj.document_code = clsCommon.myCstr(dt.Rows(0)("Document_Code"))
                obj.document_date = clsCommon.myCDate(dt.Rows(0)("Document_Date"))
                obj.Location = clsCommon.myCstr(dt.Rows(0)("Location"))
                obj.Pay_Period = clsCommon.myCstr(dt.Rows(0)("Pay_Period"))
                obj.DA_Arrear = clsCommon.myCdbl(dt.Rows(0)("DA_Arrear"))
                obj.Status = clsCommon.myCstr(dt.Rows(0)("Status"))
                If Not IsDBNull(dt.Rows(0)("Applicable_date")) Then
                    obj.Applicable_date = clsCommon.myCDate(dt.Rows(0)("Applicable_date"))
                End If
                'obj.QC_Start_date = clsCommon.myCDate(dt.Rows(0)("QC_Start_Date"))
                'bj.QC_end_date = clsCommon.myCDate(dt.Rows(0)("QC_END_Date"))
                qry = "	select Apply,TSPL_EMPLOYEE_MASTER.EMP_CODE,TSPL_EMPLOYEE_MASTER.Emp_Name,Basic,da,DA_Arrear,PF from TSPL_DA_Arrear_Detail
			        left outer join TSPL_EMPLOYEE_MASTER on TSPL_EMPLOYEE_MASTER.EMP_CODE=TSPL_DA_Arrear_Detail.Emp_Code "
                qry += "   where TSPL_DA_Arrear_Detail.document_code ='" + obj.document_code + "'"
                dt1 = New DataTable()
                dt1 = clsDBFuncationality.GetDataTable(qry, trans)
                For Each dr As DataRow In dt1.Rows
                    objpd = New ClsDAArrearDetail()
                    objpd.Apply = clsCommon.myCstr(dr("Apply"))
                    objpd.Emp_Code = clsCommon.myCstr(dr("EMP_CODE"))
                    objpd.Emp_Name = clsCommon.myCstr(dr("Emp_Name"))
                    objpd.Basic = clsCommon.myCstr(dr("Basic"))
                    objpd.DA = clsCommon.myCDecimal(dr("da"))
                    objpd.DA_Arrear = clsCommon.myCdbl(dr("DA_Arrear"))
                    objpd.PF = clsCommon.myCDecimal(dr("PF"))
                    obj.ArrD.Add(objpd)
                Next
            End If
            Return obj
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Shared Function PostData(ByVal FormId As String, ByVal strDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Document No not found to Post")
            End If
            Dim strPostDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt")
            Dim obj As clsDAArrear = clsDAArrear.GetData(strDocNo, NavigatorType.Current, trans)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.document_code) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            If (obj.Status = "1") Then
                Throw New Exception("Already Posted")
            End If
            Dim qry As String = "Update TSPL_DA_Arrear_Header set Status=1, Posted_Date='" + strPostDate + "',Posted_By='" + objCommonVar.CurrentUserCode + "'  where Document_code ='" + strDocNo + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            clsCommonFunctionality.SaveHistoryData(EnumSaveType.History, objCommonVar.CurrentUserCode, obj.document_code, "TSPL_DA_Arrear_Header", "Document_Code", "TSPL_DA_Arrear_Detail", "Document_Code", "TSPL_DAAREAR_PAYPERIOD_DETAIL", "Document_Code", "TSPL_DAAREAR_Location_DETAIL", "Document_Code", "", "", "", "", "", "", trans)

            'clsDBFuncationality.ExecuteNonQuery("Update TSPL_PROD_QC_CHECK_HEAD set posted='1', Modified_By = '" + objCommonVar.CurrentUserCode + "',Modified_Date = '" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "yyyy-MM-dd") + "'  where document_code='" & obj.document_code & "'", trans)
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
        Dim obj As New clsDAArrear()
        Try
            isSaved = False
            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Code not found to Delete")
            End If
            Dim isPosted As Integer = 0
            isPosted = clsDBFuncationality.getSingleValue("SELECT Count(*) FROM TSPL_DA_Arrear_Header where Document_code = '" & strCode & "' and Status=1", trans)
            If (isPosted = 1) Then
                Throw New Exception("Already Posted on :" + obj.Posting_Date)
            End If
            clsCommonFunctionality.SaveDeletedData(objCommonVar.CurrentUserCode, strCode, "TSPL_DA_Arrear_Header", "Document_Code", "TSPL_DA_Arrear_Detail", "Document_Code", trans)
            clsCommonFunctionality.SaveHistoryData(EnumSaveType.History, objCommonVar.CurrentUserCode, obj.document_code, "TSPL_DA_Arrear_Header", "Document_Code", "TSPL_DA_Arrear_Detail", "Document_Code", "TSPL_DAAREAR_PAYPERIOD_DETAIL", "Document_Code", "TSPL_DAAREAR_Location_DETAIL", "Document_Code", "", "", "", "", "", "", trans)

            Dim qry As String
            qry = "delete from TSPL_DAAREAR_PAYPERIOD_DETAIL where Document_code ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "delete from TSPL_DAAREAR_Location_DETAIL where Document_code ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "delete from TSPL_DA_Arrear_Detail where Document_code ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "DELETE FROM TSPL_DA_Arrear_Header WHERE Document_code='" + strCode + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message.ToString())
        End Try
        Return isSaved
    End Function
    Public Shared Function ReverseAndUnpost(ByVal strCode As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim obj As clsDAArrear = clsDAArrear.GetData(strCode, NavigatorType.Current, trans)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.document_code) <= 0) Then
                Throw New Exception("Document No not found to Post")
            End If
            'clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strCode, "TSPL_DA_Arrear_Header", "Document_Code", "TSPL_DA_Arrear_Detail", "Document_Code", trans)
            If Not (obj.Status = ERPTransactionStatus.Approved) Then
                Throw New Exception("Transaction status should be posted.")
            End If
            Dim qry As String
            If obj.Status = 1 Then
                qry = "update TSPL_DA_Arrear_Header set Status=0,Posted_Date=null,Posted_By=null where document_code='" + strCode + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
            End If
            clsCommonFunctionality.SaveHistoryData(EnumSaveType.History, objCommonVar.CurrentUserCode, obj.document_code, "TSPL_DA_Arrear_Header", "Document_Code", "TSPL_DA_Arrear_Detail", "Document_Code", "TSPL_DAAREAR_PAYPERIOD_DETAIL", "Document_Code", "TSPL_DAAREAR_Location_DETAIL", "Document_Code", "", "", "", "", "", "", trans)

            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
End Class
Public Class ClsDAArrearDetail
#Region "variables"
    Public Document_Code As String = Nothing
    Public Emp_Code As String = Nothing
    Public Emp_Name As String = Nothing
    Public Apply As String = Nothing
    Public Basic As Double = 0
    Public PF As Double = 0
    Public DA As Double = 0
    Public DA_Arrear As Double = 0
    Public GPF As Double = 0
#End Region
    Public Shared Function SaveData(ByVal strCode As String, ByVal arr As List(Of ClsDAArrearDetail), ByVal trans As SqlTransaction) As Boolean
        Dim coll As New Hashtable()
        Try
            Dim qry As String = "delete from TSPL_DA_Arrear_Detail where document_code='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            If arr IsNot Nothing AndAlso arr.Count > 0 Then
                For Each objtr As ClsDAArrearDetail In arr
                    coll = New Hashtable()
                    'clsCommon.AddColumnsForChange(coll, "PK_Id", objtr.PK_Id)
                    clsCommon.AddColumnsForChange(coll, "Document_Code", strCode)
                    clsCommon.AddColumnsForChange(coll, "Emp_Code", objtr.Emp_Code)
                    clsCommon.AddColumnsForChange(coll, "Apply", objtr.Apply)
                    clsCommon.AddColumnsForChange(coll, "Basic", objtr.Basic)
                    clsCommon.AddColumnsForChange(coll, "DA", objtr.DA)
                    clsCommon.AddColumnsForChange(coll, "DA_Arrear", objtr.DA_Arrear)
                    clsCommon.AddColumnsForChange(coll, "PF", objtr.PF)
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_DA_Arrear_Detail", OMInsertOrUpdate.Insert, "", trans)
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
Public Class clsPayPeriod_detail
#Region "variables"
    Public Document_Code As String = Nothing
    Public PAY_PERIOD_Code As String = Nothing
#End Region
    Public Shared Function SaveData(ByVal strCode As String, ByVal arr As List(Of clsPayPeriod_detail), ByVal trans As SqlTransaction) As Boolean
        Dim coll As New Hashtable()
        Try
            Dim qry As String = "delete from TSPL_DAAREAR_PAYPERIOD_DETAIL where document_code='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            If arr IsNot Nothing AndAlso arr.Count > 0 Then
                For Each objtr As clsPayPeriod_detail In arr
                    coll = New Hashtable()
                    'clsCommon.AddColumnsForChange(coll, "PK_Id", objtr.PK_Id)
                    clsCommon.AddColumnsForChange(coll, "Document_Code", strCode)
                    clsCommon.AddColumnsForChange(coll, "Pay_Period", objtr.PAY_PERIOD_Code)
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_DAAREAR_PAYPERIOD_DETAIL", OMInsertOrUpdate.Insert, "", trans)
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
Public Class clsDALocation_detail
#Region "variables"
    Public Document_Code As String = Nothing
    Public Location As String = Nothing
#End Region
    Public Shared Function SaveData(ByVal strCode As String, ByVal arr As List(Of clsDALocation_detail), ByVal trans As SqlTransaction) As Boolean
        Dim coll As New Hashtable()
        Try
            Dim qry As String = "delete from TSPL_DAAREAR_Location_DETAIL where document_code='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            If arr IsNot Nothing AndAlso arr.Count > 0 Then
                For Each objtr As clsDALocation_detail In arr
                    coll = New Hashtable()
                    'clsCommon.AddColumnsForChange(coll, "PK_Id", objtr.PK_Id)
                    clsCommon.AddColumnsForChange(coll, "Document_Code", strCode)
                    clsCommon.AddColumnsForChange(coll, "Location", objtr.Location)
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_DAAREAR_Location_DETAIL", OMInsertOrUpdate.Insert, "", trans)
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

