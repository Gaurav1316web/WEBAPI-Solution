Imports common
Imports System.Data
Imports System.Data.SqlClient
Public Class clsSalaryStructure

#Region "Variables"
    Public SALARY_STRUCTURE_CODE As String
    Public SALARY_STRUCTURE_NAME As String
    Public SAL_PRINT_NAME As String
    Public Location_Code As String
#End Region

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsSalaryStructure
        Return GetData(strCode, NavType, Nothing)
    End Function
    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim isSaved As Boolean
        Try
            isSaved = False

            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Code not found to Delete")
            End If

            Dim qry As String
            qry = "delete from TSPL_SALARY_STRUCTURE where SALARY_STRUCTURE_CODE ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry)
        Catch ex As Exception

            common.clsCommon.MyMessageBoxShow(ex.Message.ToString())
        End Try
        Return isSaved
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsSalaryStructure
        Dim obj As clsSalaryStructure = Nothing
        Dim qry As String = "select SALARY_STRUCTURE_CODE, SALARY_STRUCTURE_NAME, SAL_PRINT_NAME,Location_Code from TSPL_SALARY_STRUCTURE where 2=2"
        Select Case NavType
            Case NavigatorType.First
                qry += " and SALARY_STRUCTURE_CODE = (select MIN(SALARY_STRUCTURE_CODE) from TSPL_SALARY_STRUCTURE)"
            Case NavigatorType.Last
                qry += " and SALARY_STRUCTURE_CODE = (select Max(SALARY_STRUCTURE_CODE) from TSPL_SALARY_STRUCTURE)"
            Case NavigatorType.Next
                qry += " and SALARY_STRUCTURE_CODE = (select Min(SALARY_STRUCTURE_CODE) from TSPL_SALARY_STRUCTURE where  SALARY_STRUCTURE_CODE>'" + strCode + "')"
            Case NavigatorType.Previous
                qry += " and SALARY_STRUCTURE_CODE = (select Max(SALARY_STRUCTURE_CODE) from TSPL_SALARY_STRUCTURE where SALARY_STRUCTURE_CODE<'" + strCode + "')"
            Case NavigatorType.Current
                qry += " and SALARY_STRUCTURE_CODE = '" + strCode + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsSalaryStructure()
            obj.SALARY_STRUCTURE_CODE = clsCommon.myCstr(dt.Rows(0)("SALARY_STRUCTURE_CODE"))
            obj.SALARY_STRUCTURE_NAME = clsCommon.myCstr(dt.Rows(0)("SALARY_STRUCTURE_NAME"))
            obj.SAL_PRINT_NAME = clsCommon.myCstr(dt.Rows(0)("SAL_PRINT_NAME"))
            obj.Location_Code = clsCommon.myCstr(dt.Rows(0)("Location_Code"))
        End If
        Return obj


    End Function

    Public Function SaveData(ByVal obj As clsSalaryStructure, ByVal isNewEntry As Boolean) As Boolean
        Dim isSaved As Boolean = True
        Try
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "SALARY_STRUCTURE_NAME", obj.SALARY_STRUCTURE_NAME)
            clsCommon.AddColumnsForChange(coll, "SAL_PRINT_NAME", obj.SAL_PRINT_NAME)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Location_Code", obj.Location_Code)
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "SALARY_STRUCTURE_CODE", obj.SALARY_STRUCTURE_CODE)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy"))
                Dim qry As String = "SELECT Count(*) FROM TSPL_SALARY_STRUCTURE where SALARY_STRUCTURE_CODE= '" & obj.SALARY_STRUCTURE_CODE & "'"
                Dim check As Integer = clsDBFuncationality.getSingleValue(qry)
                If check = 0 Then
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SALARY_STRUCTURE", OMInsertOrUpdate.Insert, "")
                Else
                    Throw New Exception("This Code Is Already Exist")

                End If
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SALARY_STRUCTURE", OMInsertOrUpdate.Update, "SALARY_STRUCTURE_CODE='" + obj.SALARY_STRUCTURE_CODE + "'")
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function

End Class
