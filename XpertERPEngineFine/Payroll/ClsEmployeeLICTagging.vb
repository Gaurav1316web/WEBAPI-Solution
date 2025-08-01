Imports common
Imports System.Data
Imports System.Data.SqlClient

Public Class ClsEmployeeLICTagging

#Region "Variables"

    Public Document_code As String
    Public EMP_CODE As String
    Public PAY_HEAD As String
    'Public REVISION_NO As Integer
    Public Document_Date As DateTime
    Public Applicable_From As DateTime
    Public Applicable_To As DateTime
    Public POSTED As Boolean
    Public Posting_Date As DateTime
    'Public Location_Code As String = Nothing
    ' '' grid columns
    'Public Line_No As Integer
    'Public PayHeadCode As String
    'Public PayHeadName As String
    'Public Formula As String
    Public Total_AMT As Decimal
    'Public IsHiddenComponent As Boolean

    Public Shared ObjList As List(Of ClsEmployeeLICTaggingDetails)
    ' Public Arr As New List(Of clsEmpSalaryPayHeadDetails)

#End Region

    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Try
            Dim str As String = ""
            Dim qry As String = ""

            qry = "Select DOCUMENT_CODE,DOCUMENT_Date,EMP_CODE,APPLICABLE_FROM,APPLICABLE_TO from TSPL_EMPLOYEE_LIC_TAGGING "
            str = clsCommon.ShowSelectForm("fndPayProcess", qry, "DOCUMENT_CODE", whrcls, curcode, "DOCUMENT_CODE", isButtonClicked, "DOCUMENT_Date")

            Return str
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function


    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As ClsEmployeeLICTagging
        Return GetData(strCode, NavType, Nothing)
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As ClsEmployeeLICTagging
        Dim whrQry As String = Nothing
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            whrQry = " And TAV.Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
        End If
        Dim obj As New ClsEmployeeLICTagging()
        Dim objtr As New ClsEmployeeLICTaggingDetails()
        ObjList = New List(Of ClsEmployeeLICTaggingDetails)

        Dim qry As String = "SELECT * from TSPL_EMPLOYEE_LIC_TAGGING where 2=2 "

        Select Case NavType
            Case NavigatorType.First
                qry += " and DOCUMENT_CODE = (select MIN(DOCUMENT_CODE) from TSPL_EMPLOYEE_LIC_TAGGING)"
            Case NavigatorType.Last
                qry += " and DOCUMENT_CODE = (select Max(DOCUMENT_CODE) from TSPL_EMPLOYEE_LIC_TAGGING)"
            Case NavigatorType.Next
                qry += " and DOCUMENT_CODE = (select Min(DOCUMENT_CODE) from TSPL_EMPLOYEE_LIC_TAGGING where  DOCUMENT_CODE>'" + strCode + "')"
            Case NavigatorType.Previous
                qry += " and DOCUMENT_CODE = (select Max(DOCUMENT_CODE) from TSPL_EMPLOYEE_LIC_TAGGING where DOCUMENT_CODE<'" + strCode + "')"
            Case NavigatorType.Current
                qry += " and DOCUMENT_CODE = '" + strCode + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj.Document_code = dt.Rows(0)("DOCUMENT_CODE")
            obj.Applicable_From = dt.Rows(0)("APPLICABLE_FROM")
            obj.Applicable_To = dt.Rows(0)("APPLICABLE_TO")
            strCode = dt.Rows(0)("DOCUMENT_CODE")
            obj.Document_Date = dt.Rows(0)("DOCUMENT_Date")

            obj.EMP_CODE = clsCommon.myCstr(dt.Rows(0)("EMP_CODE"))
            obj.Total_AMT = clsCommon.myCdbl(dt.Rows(0)("Total_AMT"))
            obj.POSTED = clsCommon.myCBool(dt.Rows(0)("POSTED"))
            If clsCommon.myLen(dt.Rows(0)("Posting_Date")) > 0 Then
                obj.Posting_Date = clsCommon.myCDate(dt.Rows(0)("Posting_Date"))
            Else
                obj.Posting_Date = Nothing
            End If
        End If

        qry = " Select * from TSPL_EMPLOYEE_LIC_TAGGING_DETAILS"
        dt = New DataTable()
        dt = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            For Each dr As DataRow In dt.Rows
                objtr = New ClsEmployeeLICTaggingDetails()
                objtr.PolicyAccountNo = clsCommon.myCstr(dr("LINE_NO"))
                objtr.Amount = clsCommon.myCdbl(dr("PAY_HEAD_CODE"))
                ObjList.Add(objtr)
            Next
        End If
        ClsEmployeeLICTagging.ObjList = ObjList
        Return obj

    End Function

    Public Function SaveData(ByVal obj As ClsEmployeeLICTagging, ByVal objList As List(Of ClsEmployeeLICTaggingDetails), ByVal isNewEntry As Boolean, Optional ByVal strCode As String = "") As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Dim isSaved As Boolean = SaveData(obj, objList, isNewEntry, strCode, trans)
        Try
            If isSaved Then
                trans.Commit()
            End If
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function

    Public Function SaveData(ByVal obj As ClsEmployeeLICTagging, ByVal objList As List(Of ClsEmployeeLICTaggingDetails), ByVal isNewEntry As Boolean, Optional ByVal strCode As String = "", Optional ByVal trans As SqlTransaction = Nothing) As Boolean
        Dim isSaved As Boolean = True

        If isNewEntry Then
            If strCode = "" Then
                obj.Document_code = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(clsCommon.GETSERVERDATE(trans)), clsDocType.EmployeeSalary, "", "")
            Else
                obj.Document_code = strCode
            End If
        End If

        Dim qry As String = "delete from TSPL_EMPLOYEE_LIC_TAGGING_DETAIL where DOCUMENT_CODE='" + obj.Document_code + "'"
        isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
        Dim strDocNo As String = ""

        If (clsCommon.myLen(obj.Document_code) <= 0) Then
            Throw New Exception("Error in Document Code Generation")
        End If

        Dim coll As New Hashtable()
        clsCommon.AddColumnsForChange(coll, "EMP_CODE", obj.EMP_CODE)
        clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy hh:mm tt"))
        clsCommon.AddColumnsForChange(coll, "PAY_HEAD_CODE", obj.PAY_HEAD)
        clsCommon.AddColumnsForChange(coll, "Total_AMT", obj.Total_AMT)
        clsCommon.AddColumnsForChange(coll, "APPLICABLE_FROM", clsCommon.GetPrintDate(obj.Applicable_From, "dd/MMM/yyyy"))
        clsCommon.AddColumnsForChange(coll, "APPLICABLE_TO", clsCommon.GetPrintDate(obj.Applicable_To, "dd/MMM/yyyy"))
        clsCommon.AddColumnsForChange(coll, "POSTED", "0")
        clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
        clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))
        clsCommon.AddColumnsForChange(coll, "Posting_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))
        If isNewEntry Then
            clsCommon.AddColumnsForChange(coll, "Document_code", obj.Document_code)
            clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))
            Dim Strqry As String = "SELECT Count(*) FROM TSPL_EMPLOYEE_LIC_TAGGING where Document_code = '" & obj.Document_code & "'"
            Dim check As Integer = clsDBFuncationality.getSingleValue(Strqry, trans)
            If check = 0 Then
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_EMPLOYEE_LIC_TAGGING", OMInsertOrUpdate.Insert, "", trans)
            Else
                Throw New Exception("This Code:" + obj.Document_code + " Is Already Exist")
            End If
        Else
            isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_EMPLOYEE_LIC_TAGGING", OMInsertOrUpdate.Update, "TSPL_EMPLOYEE_LIC_TAGGING.Document_code='" + obj.Document_code + "'", trans)
        End If
        isSaved = isSaved AndAlso ClsEmployeeLICTaggingDetails.SaveData(obj.Document_code, objList, trans)
        clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, obj.Document_code, "TSPL_EMPLOYEE_LIC_TAGGING", "Document_code", "TSPL_EMPLOYEE_LIC_TAGGING_DETAIL", "Document_code", trans)


        Return True
    End Function

    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim isDelete As Boolean = DeleteData(strCode, trans)
            trans.Commit()
            Return isDelete
        Catch ex As Exception
            trans.Rollback()
            common.clsCommon.MyMessageBoxShow(ex.Message.ToString())
            Return False
        End Try
    End Function

    Public Shared Function DeleteData(ByVal strCode As String, Optional ByVal trans As SqlTransaction = Nothing) As Boolean
        Dim isSaved As Boolean
        isSaved = False
        If (clsCommon.myLen(strCode) <= 0) Then
            Throw New Exception("Code not found to Delete")
        End If

        Dim qry As String
        qry = "delete from TSPL_EMPLOYEE_LIC_TAGGING_DETAIL where DOCUMENT_CODE ='" + strCode + "'"
        isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
        qry = "delete from TSPL_EMPLOYEE_LIC_TAGGING where DOCUMENT_CODE ='" + strCode + "'"
        isSaved = isSaved And clsDBFuncationality.ExecuteNonQuery(qry, trans)

        Return isSaved
    End Function
End Class

Public Class ClsEmployeeLICTaggingDetails
#Region "Variables"
    '' grid columns
    Public Line_No As Integer
    Public PolicyAccountNo As String
    Public PayHeadName As String
    Public PayheadMode As String
    Public Payhead As String
    Public Formula As String
    Public Amount As Decimal
    Public IsHiddenComponent As Boolean
    Public MAX_AMOUNT As Decimal = 0
    Public PAYPERIOD_AMOUNT As Decimal = 0
    Public Location_Code As String = Nothing
    Public Attendance_Wise As String = Nothing
    'Public Shared ObjList As List(Of clsEmpSalaryPayHeadDetails)
    'Public Const AttendanceCode As String = "MT"
#End Region

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of ClsEmployeeLICTaggingDetails), ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As ClsEmployeeLICTaggingDetails In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Document_Code", strDocNo)
                clsCommon.AddColumnsForChange(coll, "Policy_Account_No", obj.PolicyAccountNo)
                clsCommon.AddColumnsForChange(coll, "LIC_PREMIUM_AMT", obj.Amount)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_EMPLOYEE_LIC_TAGGING_DETAIL", OMInsertOrUpdate.Insert, "TSPL_EMPLOYEE_LIC_TAGGING_DETAIL.Document_Code='" + strDocNo + "'", trans)

            Next
        End If
        Return True
    End Function
End Class
