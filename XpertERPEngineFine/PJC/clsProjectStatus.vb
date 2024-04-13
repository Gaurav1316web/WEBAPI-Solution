Imports common
Imports System.Data
Imports System.Data.SqlClient

Public Class clsProjectStatus

    '----------------Code For Get Finder--------------------------------------------------------------------'
    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = " select TSPL_PJC_PROJECT.PROJECT_CODE as [Code] ,TSPL_PJC_PROJECT.SPECIFICATION as [Specification] ,TSPL_PJC_PROJECT.PROJECT_STATUS as [Project Status] ,TSPL_PJC_PROJECT.Cust_Code as [Customer Code] ,TSPL_PJC_PROJECT.Created_By as [Created By] ,TSPL_PJC_PROJECT.Created_Date as [Created Date] ,TSPL_PJC_PROJECT.Modified_By as [Modified By] ,TSPL_PJC_PROJECT.Modified_Date as [Modified Date] ,TSPL_PJC_PROJECT.Comp_Code as [Company Code] ,TSPL_PJC_PROJECT.Project_Manager as [Project Manager] ,TSPL_PJC_PROJECT.Sale_Order_No as [Sale Order No] ,TSPL_PJC_PROJECT.Project_Type as [Project Type] ,TSPL_PJC_PROJECT.Project_Type_Value as [Project Type Value] ,TSPL_PJC_PROJECT.Account_Method as [Account Method] ,TSPL_PJC_PROJECT.Approve_By as [Approve By] ,TSPL_PJC_PROJECT.Approve_Date as [Approve Date] ,TSPL_PJC_PROJECT.Approve_Date_Actual as [Approve Date Actual] ,TSPL_PJC_PROJECT.Open_By as [Open By] ,TSPL_PJC_PROJECT.Open_Date as [Open Date] ,TSPL_PJC_PROJECT.Open_Date_Actual as [Open Date Actual] ,TSPL_PJC_PROJECT.On_Hold_By as [On Hold By] ,TSPL_PJC_PROJECT.On_Hold_Date as [On Hold Date] ,TSPL_PJC_PROJECT.Close_By as [Close By] ,TSPL_PJC_PROJECT.Close_Date as [Close Date] ,TSPL_PJC_PROJECT.Completed_By as [Completed By] ,TSPL_PJC_PROJECT.Completed_Date as [Completed Date] ,TSPL_PJC_PROJECT.Completed_Date_Actual as [Completed Date Actual] ,TSPL_PJC_PROJECT.Inactive_By as [Inactive By] ,TSPL_PJC_PROJECT.Inactive_Date as [Inactive Date] ,TSPL_PJC_PROJECT.Comment as [Comment] ,TSPL_PJC_PROJECT.Total_Cost as [Total Cost] ,TSPL_PJC_PROJECT.Total_Billing as [Total Billing] ,TSPL_PJC_PROJECT.Total_Profit as [Total Profit]  From TSPL_PJC_PROJECT  "
        str = clsCommon.ShowSelectForm("PRJSTATSU", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str
    End Function
    '----------------End of Code For Get Finder--------------------------------------------------------------'
    Public Shared Function GetProjectStatus() As DataTable
        Dim DT_Status As DataTable = New DataTable
        DT_Status.Columns.Add("Code", GetType(String))
        DT_Status.Columns.Add("Name", GetType(String))

        Dim DR As DataRow = DT_Status.NewRow()
        DR("Name") = "Estimated"
        DR("Code") = "Estimated"
        DT_Status.Rows.Add(DR)

        DR = DT_Status.NewRow()
        DR("Name") = "Approve"
        DR("Code") = "Approve"
        DT_Status.Rows.Add(DR)

        DR = DT_Status.NewRow()
        DR("Name") = "Open"
        DR("Code") = "Open"
        DT_Status.Rows.Add(DR)

        DR = DT_Status.NewRow()
        DR("Name") = "On Hold"
        DR("Code") = "On Hold"
        DT_Status.Rows.Add(DR)

        DR = DT_Status.NewRow()
        DR("Name") = "Close"
        DR("Code") = "Close"
        DT_Status.Rows.Add(DR)

        DR = DT_Status.NewRow()
        DR("Name") = "Complete"
        DR("Code") = "Complete"
        DT_Status.Rows.Add(DR)

        DR = DT_Status.NewRow()
        DR("Name") = "InActive"
        DR("Code") = "InActive"
        DT_Status.Rows.Add(DR)

        DT_Status.AcceptChanges()

        Return DT_Status
    End Function

     
End Class
