Imports common
Imports System.Data
Imports System.Data.SqlClient
Public Class clsExpenseType

#Region "Variables"
    Dim Qry As String
    Public EXPENSE_CODE As String = Nothing
    Public DESCRIPTION As String = Nothing
    Public EXPENSE_TYPE As String = Nothing
    Public INTEGRATE_AP As String = Nothing
    Public GLACCOUNT As String = Nothing
    Public Remarks As String = Nothing
    Public Form_ID As String = ""
    Public arrCustomFields As List(Of clsCustomFieldValues) = Nothing

#End Region
    '----------------Code For Get Finder--------------------------------------------------------------------'
    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = " select tspl_expense_master.EXPENSE_CODE as [Code] ,tspl_expense_master.DESCRIPTION as [Description] ,tspl_expense_master.EXPENSE_TYPE as [Expense Type] ,tspl_expense_master.INTEGRATE_AP as [Integrate Ap] ,tspl_expense_master.GLACCOUNT as [GL Account] ,tspl_expense_master.Remarks as [Remarks] ,tspl_expense_master.Created_By as [Created By] ,tspl_expense_master.Created_Date as [Created Date] ,tspl_expense_master.Modified_By as [Modified By] ,tspl_expense_master.Modified_Date as [Modified Date] ,tspl_expense_master.Comp_Code as [Company Code]  From tspl_expense_master   "
        str = clsCommon.ShowSelectForm("EXPTYPEMST", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str
    End Function
    '----------------End of Code For Get Finder--------------------------------------------------------------'

    Public Shared Function SaveData(ByVal obj As clsExpenseType, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean

        Dim cntr As Integer = 0
        Dim isSaved As Boolean = True
        Try

            If isNewEntry Then
                Dim strCode As String = clsDBFuncationality.getSingleValue("select isnull(max(Expense_code),'') from tspl_expense_master", trans)
                If clsCommon.myLen(strCode) <= 0 Then
                    obj.EXPENSE_CODE = "EX000000001"
                Else
                    obj.EXPENSE_CODE = clsCommon.incval(strCode)
                End If

            End If


            If (clsCommon.myLen(obj.EXPENSE_CODE) <= 0) Then
                Throw New Exception("Error in Document Code Generation")
            End If

            Dim coll As New Hashtable()

            clsCommon.AddColumnsForChange(coll, "Description", obj.DESCRIPTION)
            clsCommon.AddColumnsForChange(coll, "EXPENSE_TYPE", obj.EXPENSE_TYPE)
            clsCommon.AddColumnsForChange(coll, "INTEGRATE_AP", obj.INTEGRATE_AP)
            clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks)
            clsCommon.AddColumnsForChange(coll, "GLACCOUNT", obj.GLACCOUNT)
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))

            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                clsCommon.AddColumnsForChange(coll, "EXPENSE_CODE", obj.EXPENSE_CODE)
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "tspl_expense_master", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "tspl_expense_master", OMInsertOrUpdate.Update, "EXPENSE_CODE='" + obj.EXPENSE_CODE + "'", trans)
            End If

        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function
    'Public Function SaveData(ByVal obj As clsExpenseType, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean

    'End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsExpenseType
        Return GetData(strCode, NavType, Nothing)
    End Function

    Public Shared Function GetData(ByVal strDocNo As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsExpenseType

        Dim obj As clsExpenseType = Nothing
        Dim qry As String = "SELECT * from tspl_expense_master where 2=2"
        Dim whrClas As String = ""


        Select Case NavType
            Case NavigatorType.First
                qry += " and tspl_expense_master.EXPENSE_CODE = (select MIN(EXPENSE_CODE) from tspl_expense_master where 1=1 " + whrClas + ")"
            Case NavigatorType.Last
                qry += " and tspl_expense_master.EXPENSE_CODE = (select Max(EXPENSE_CODE) from tspl_expense_master where 1=1 " + whrClas + ")"
            Case NavigatorType.Next
                qry += " and tspl_expense_master.EXPENSE_CODE = (select Min(EXPENSE_CODE) from tspl_expense_master where EXPENSE_CODE>'" + strDocNo + "' " + whrClas + ")"
            Case NavigatorType.Previous
                qry += " and tspl_expense_master.EXPENSE_CODE = (select Max(EXPENSE_CODE) from tspl_expense_master where EXPENSE_CODE<'" + strDocNo + "' " + whrClas + ")"
            Case NavigatorType.Current
                qry += " and tspl_expense_master.EXPENSE_CODE = '" + strDocNo + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsExpenseType()
            obj.EXPENSE_CODE = clsCommon.myCstr(dt.Rows(0)("EXPENSE_CODE"))
            obj.DESCRIPTION = clsCommon.myCstr(dt.Rows(0)("DESCRIPTION"))
            obj.EXPENSE_TYPE = clsCommon.myCstr(dt.Rows(0)("EXPENSE_TYPE"))
            obj.INTEGRATE_AP = clsCommon.myCstr(dt.Rows(0)("INTEGRATE_AP"))
            obj.Remarks = clsCommon.myCstr(dt.Rows(0)("Remarks"))
            obj.GLACCOUNT = clsCommon.myCstr(dt.Rows(0)("GLACCOUNT"))
        End If
        Return obj
    End Function



    Public Shared Function GetExpenseTypeTable() As DataTable
        Dim DT_ExpenseType As DataTable = New DataTable
        DT_ExpenseType.Columns.Add("Code", GetType(String))
        DT_ExpenseType.Columns.Add("Name", GetType(String))

        Dim DR As DataRow = DT_ExpenseType.NewRow()
        DR("Name") = "Food"
        DR("Code") = "Food"
        DT_ExpenseType.Rows.Add(DR)

        DR = DT_ExpenseType.NewRow()
        DR("Name") = "Conveyance"
        DR("Code") = "Conveyance"
        DT_ExpenseType.Rows.Add(DR)

        DR = DT_ExpenseType.NewRow()
        DR("Name") = "Travel"
        DR("Code") = "Travel"
        DT_ExpenseType.Rows.Add(DR)

        DR = DT_ExpenseType.NewRow()
        DR("Name") = "Office expense"
        DR("Code") = "Office expense"
        DT_ExpenseType.Rows.Add(DR)

        DR = DT_ExpenseType.NewRow()
        DR("Name") = "Others"
        DR("Code") = "Others"
        DT_ExpenseType.Rows.Add(DR)

        DT_ExpenseType.AcceptChanges()

        Return DT_ExpenseType
    End Function
End Class
