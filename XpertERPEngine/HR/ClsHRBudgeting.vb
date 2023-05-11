Imports common
Imports System.Data
Imports System.Data.SqlClient

Public Class ClsHRBudgeting

#Region "Variables"
    Public Is_Applied As Integer = 0
    Public DeptCode As String = Nothing
    Public Budget As Double = 0
#End Region
    '----------------Code For Get Finder--------------------------------------------------------------------'
    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = " SELECT TSPL_HR_BUDGETING.Is_Applied,TSPL_HR_BUDGETING.Department_Code As [Department Code] ,TSPL_HR_BUDGETING.Created_By as [Created By] ,Convert(varchar,TSPL_HR_BUDGETING.Created_Date,103) as [Created Date] ,TSPL_HR_BUDGETING.Modified_By as [Modified By] ,Convert(varchar,TSPL_HR_BUDGETING.Modified_Date,103) as [Modified Date]  From TSPL_HR_BUDGETING "
        str = clsCommon.ShowSelectForm("HRBudg", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str
    End Function
    Public Shared Function GetData(ByVal trans As SqlTransaction) As List(Of ClsHRBudgeting)
        Dim arr As New List(Of ClsHRBudgeting)
        Dim qry As String = "select * from TSPL_HR_BUDGETING where 2=2"

        Dim dt As DataTable
        Try
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            For Each dr As DataRow In dt.Rows
                Dim obj As New ClsHRBudgeting
                obj.Is_Applied = clsCommon.myCdbl(dr("Is_Applied"))
                obj.DeptCode = clsCommon.myCstr(dr("Department_Code"))
                obj.Budget = clsCommon.myCstr(dr("Budget"))
                arr.Add(obj)
            Next
            Return arr

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Shared Function SaveData(ByVal arr As List(Of ClsHRBudgeting)) As Boolean
        Dim trans As SqlTransaction = Nothing
        Try
            Dim DuplicateEntry As String = ""
            trans = clsDBFuncationality.GetTransactin()
            clsDBFuncationality.ExecuteNonQuery("Delete From TSPL_HR_BUDGETING", trans)
            For Each obj As ClsHRBudgeting In arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Is_Applied", obj.Is_Applied)
                clsCommon.AddColumnsForChange(coll, "Department_Code", obj.DeptCode)
                clsCommon.AddColumnsForChange(coll, "Budget", obj.Budget)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_HR_BUDGETING", OMInsertOrUpdate.Insert, "", trans)
            Next
            DuplicateEntry = "SELECT Department_Code,  SUM(1) AS Repeated FROM TSPL_HR_BUDGETING group by Department_Code HAVING SUM(1) > 1"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(DuplicateEntry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Throw New Exception("Please check ! Dept code (" & clsCommon.myCstr(dt.Rows(0)("Department_Code")) & ") ) repeated " & clsCommon.myCstr(dt.Rows(0)("Repeated")) & " times.")
            End If
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function
End Class
