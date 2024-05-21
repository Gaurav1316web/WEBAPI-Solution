Imports common
Imports System.Data.SqlClient
Public Class clsEmployeeDeductionMaster
#Region "Variables"
    Public CODE As String
    Public EMP_CODE As String
    Public LIC_POLICY_NO As String
    Public LIC_PREMIUM_AMT As Decimal
    Public BANK_NAME As String
    Public BANK_ACCOUNT_NO As String
    Public BANK_INSTALMENT As Decimal
    Public QUARTER_TYPE As String
    Public QUARTER_ALLOTED_DATE As Date?
    Public QUARTER_LEFT_DATE As Date?
    Public KKK_INSTALMENT As Decimal
    Public KKK_LOAN_TOTAL As Decimal
    Public Created_By As String
    Public Created_Date As DateTime
    Public Modified_By As String
    Public Modified_Date As DateTime
#End Region

    Public Function SaveData(ByVal obj As clsEmployeeDeductionMaster, ByVal isNewEntry As Boolean) As Boolean
        Dim isSaved As Boolean = True
        Try
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "EMP_CODE", obj.EMP_CODE)
            clsCommon.AddColumnsForChange(coll, "LIC_POLICY_NO", obj.LIC_POLICY_NO)
            clsCommon.AddColumnsForChange(coll, "LIC_PREMIUM_AMT", obj.LIC_PREMIUM_AMT)
            clsCommon.AddColumnsForChange(coll, "BANK_NAME", obj.BANK_NAME)
            clsCommon.AddColumnsForChange(coll, "BANK_ACCOUNT_NO", obj.BANK_ACCOUNT_NO)
            clsCommon.AddColumnsForChange(coll, "BANK_INSTALMENT", obj.BANK_INSTALMENT)
            clsCommon.AddColumnsForChange(coll, "QUARTER_TYPE", obj.QUARTER_TYPE)
            clsCommon.AddColumnsForChange(coll, "QUARTER_ALLOTED_DATE", clsCommon.GetPrintDate(obj.QUARTER_ALLOTED_DATE, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "QUARTER_LEFT_DATE", clsCommon.GetPrintDate(obj.QUARTER_LEFT_DATE, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "KKK_INSTALMENT", obj.KKK_INSTALMENT)
            clsCommon.AddColumnsForChange(coll, "KKK_LOAN_TOTAL", obj.KKK_LOAN_TOTAL)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy"))
            If isNewEntry Then
                Dim ChkNewEntry As String = clsDBFuncationality.getSingleValue("Select count(*) from TSPL_EMPLOYEE_DEDUCTION_MASTER where EMP_CODE='" & obj.EMP_CODE & "'")
                If ChkNewEntry = 0 Then
                    obj.CODE = clsERPFuncationality.GetNextCode(Nothing, clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy"), clsDocType.frmEmployeeDeductionMaster, "", "")
                    If clsCommon.myLen(obj.CODE) <= 0 Then
                        Throw New Exception("Error in Code Generation")
                    End If
                End If
                clsCommon.AddColumnsForChange(coll, "CODE", obj.CODE)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy"))
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_EMPLOYEE_DEDUCTION_MASTER", OMInsertOrUpdate.Insert, "")
            Else
                Dim Code As String = clsDBFuncationality.getSingleValue("Select Code From TSPL_EMPLOYEE_DEDUCTION_MASTER Where EMP_Code='" + obj.EMP_CODE + "'")
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_EMPLOYEE_DEDUCTION_MASTER", OMInsertOrUpdate.Update, "CODE='" + Code + "'")
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function


    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim isSaved As Boolean
        Try
            isSaved = False
            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Code not found to Delete")
            End If
            Dim qry As String
            qry = "delete from TSPL_EMPLOYEE_DEDUCTION_MASTER where CODE ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function

    Public Shared Function CheckNewEntry(ByVal EMP_Code As String, Optional ByVal trans As SqlTransaction = Nothing) As Boolean
        Dim IsNew As Boolean = False
        Dim qry As String = "select CODE from TSPL_EMPLOYEE_DEDUCTION_MASTER where EMP_CODE ='" + EMP_Code + "'   "
        Dim dt As DataTable
        dt = clsDBFuncationality.GetDataTable(qry)
        If dt.Rows.Count > 0 Then
            IsNew = False
        Else
            IsNew = True
        End If
        Return IsNew
    End Function


    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsEmployeeDeductionMaster
        Return GetData(strCode, NavType, Nothing)
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsEmployeeDeductionMaster
        Dim obj As clsEmployeeDeductionMaster = Nothing
        Dim qry As String = "select * from TSPL_EMPLOYEE_DEDUCTION_MASTER where 2=2"
        Select Case NavType
            Case NavigatorType.First
                qry += " and CODE = (select MIN(CODE) from TSPL_EMPLOYEE_DEDUCTION_MASTER)"
            Case NavigatorType.Last
                qry += " and CODE = (select Max(CODE) from TSPL_EMPLOYEE_DEDUCTION_MASTER)"
            Case NavigatorType.Next
                qry += " and CODE = (select Min(CODE) from TSPL_EMPLOYEE_DEDUCTION_MASTER where  CODE>'" + strCode + "')"
            Case NavigatorType.Previous
                qry += " and CODE = (select Max(CODE) from TSPL_EMPLOYEE_DEDUCTION_MASTER where CODE<'" + strCode + "')"
            Case NavigatorType.Current
                qry += " and CODE = '" + strCode + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsEmployeeDeductionMaster()
            obj.CODE = clsCommon.myCstr(dt.Rows(0)("CODE"))
            obj.EMP_CODE = clsCommon.myCstr(dt.Rows(0)("EMP_CODE"))
            obj.LIC_POLICY_NO = clsCommon.myCstr(dt.Rows(0)("LIC_POLICY_NO"))
            obj.LIC_PREMIUM_AMT = clsCommon.myCDecimal(dt.Rows(0)("LIC_PREMIUM_AMT"))
            obj.BANK_NAME = clsCommon.myCstr(dt.Rows(0)("BANK_NAME"))
            obj.BANK_ACCOUNT_NO = clsCommon.myCstr(dt.Rows(0)("BANK_ACCOUNT_NO"))
            obj.BANK_INSTALMENT = clsCommon.myCDecimal(dt.Rows(0)("BANK_INSTALMENT"))
            obj.QUARTER_TYPE = clsCommon.myCstr(dt.Rows(0)("QUARTER_TYPE"))
            obj.QUARTER_ALLOTED_DATE = clsCommon.GetPrintDate(dt.Rows(0)("QUARTER_ALLOTED_DATE"), "dd/MM/yyyy")
            obj.QUARTER_LEFT_DATE = clsCommon.GetPrintDate(dt.Rows(0)("QUARTER_LEFT_DATE"), "dd/MM/yyyy")
            obj.KKK_INSTALMENT = clsCommon.myCDecimal(dt.Rows(0)("KKK_INSTALMENT"))
            obj.KKK_LOAN_TOTAL = clsCommon.myCDecimal(dt.Rows(0)("KKK_LOAN_TOTAL"))
        End If
        Return obj
    End Function
End Class
