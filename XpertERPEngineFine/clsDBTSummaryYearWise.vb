Imports System.Data.SqlClient
Imports common

Public Class clsDBTSummaryYearWise
#Region "variables"
    Public Fiscal_Code As String = Nothing
    Public Start_Date As Date
    Public End_Date As Date
#End Region
    Public Shared Function GetData(ByVal strMOMCode As String, ByVal NavType As NavigatorType) As clsDBTSummaryYearWise
        Return GetData(strMOMCode, NavType, "", Nothing)

    End Function
    Public Shared Function GetData(ByVal strMOMCode As String, ByVal NavType As NavigatorType, ByVal fromScreen As String, ByVal trans As SqlTransaction) As clsDBTSummaryYearWise
        Dim obj As clsDBTSummaryYearWise = Nothing
        Dim Arr As List(Of clsDBTSummaryYearWise) = Nothing
        Dim qry As String = "select TSPL_Fiscal_Year_Master.* from TSPL_Fiscal_Year_Master where 2=2"
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_Fiscal_Year_Master.Fiscal_Code = (select MIN(Fiscal_Code) from TSPL_Fiscal_Year_Master WHERE 1=1 and Comp_Code = '" + objCommonVar.CurrentCompanyCode + "'  )"
            Case NavigatorType.Last
                qry += " and TSPL_Fiscal_Year_Master.Fiscal_Code = (select Max(Fiscal_Code) from TSPL_Fiscal_Year_Master WHERE 1=1 and Comp_Code = '" + objCommonVar.CurrentCompanyCode + "' )"
            Case NavigatorType.Current
                qry += " and TSPL_Fiscal_Year_Master.Fiscal_Code = (select top 1 Fiscal_Code from TSPL_Fiscal_Year_Master WHERE 1=1  and Fiscal_Code='" + strMOMCode + "' and Comp_Code = '" + objCommonVar.CurrentCompanyCode + "')"
            Case NavigatorType.Next
                qry += " and TSPL_Fiscal_Year_Master.Fiscal_Code = (select Min(Fiscal_Code) from TSPL_Fiscal_Year_Master where Fiscal_Code>'" + strMOMCode + "' and Comp_Code = '" + objCommonVar.CurrentCompanyCode + "')"
            Case NavigatorType.Previous
                qry += " and TSPL_Fiscal_Year_Master.Fiscal_Code = (select Max(Fiscal_Code) from TSPL_Fiscal_Year_Master where Fiscal_Code<'" + strMOMCode + "' and Comp_Code = '" + objCommonVar.CurrentCompanyCode + "')"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsDBTSummaryYearWise()
            obj.Fiscal_Code = clsCommon.myCstr(dt.Rows(0)("Fiscal_Code"))
            obj.Start_Date = clsCommon.myCDate(dt.Rows(0)("Start_Date"))
            obj.End_Date = clsCommon.myCDate(dt.Rows(0)("End_Date"))
        End If
        Return obj
    End Function

End Class

Public Class clsDBTSummaryYearWiseDetails

End Class
