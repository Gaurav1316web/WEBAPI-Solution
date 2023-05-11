Imports System.Data.SqlClient
Imports common
Public Class clsFiscalYear
#Region "variables"
    Public Fiscal_Code As String = Nothing
    Public Fiscal_Name As String = Nothing
    Public Start_Date As Date
    Public End_Date As Date
    Public Is_Current_Year As Integer = 0
#End Region

    Public Function SaveData(ByVal obj As clsFiscalYear, ByVal isNewEntry As Boolean) As Boolean
        Dim qry As String = ""
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Fiscal_Code", obj.Fiscal_Code)
            clsCommon.AddColumnsForChange(coll, "Fiscal_Name", obj.Fiscal_Name)
            clsCommon.AddColumnsForChange(coll, "Start_Date", clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(obj.Start_Date), "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "End_Date", clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(obj.End_Date), "dd/MMM/yyyy hh:mm tt"))
            '' Anubhooti 09-Sep-2014 
            clsCommon.AddColumnsForChange(coll, "Is_Current_Year", obj.Is_Current_Year)

            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode, True)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Fiscal_Year_Master", OMInsertOrUpdate.Insert, "", trans)
            Else
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Fiscal_Year_Master", OMInsertOrUpdate.Update, "TSPL_Fiscal_Year_Master.Fiscal_Code='" + obj.Fiscal_Code + "'", trans)
            End If
            trans.Commit()
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function

    Public Shared Function GetData(ByVal strMOMCode As String, ByVal NavType As NavigatorType) As clsFiscalYear
        Return GetData(strMOMCode, NavType, "", Nothing)
    End Function

    Public Shared Function GetData(ByVal strMOMCode As String, ByVal NavType As NavigatorType, ByVal fromScreen As String, ByVal trans As SqlTransaction) As clsFiscalYear
        Dim obj As clsFiscalYear = Nothing
        Dim Arr As List(Of clsFiscalYear) = Nothing
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
            obj = New clsFiscalYear()
            obj.Fiscal_Code = clsCommon.myCstr(dt.Rows(0)("Fiscal_Code"))
            obj.Fiscal_Name = clsCommon.myCstr(dt.Rows(0)("Fiscal_Name"))
            obj.Start_Date = clsCommon.myCDate(dt.Rows(0)("Start_Date"))
            obj.End_Date = clsCommon.myCDate(dt.Rows(0)("End_Date"))
            '' Anubhooti 09-Sep-2014 BM00000003735
            obj.Is_Current_Year = clsCommon.myCdbl(dt.Rows(0)("Is_Current_Year"))
        End If
        Return obj
    End Function

    Public Shared Function GetName(ByVal StrCode As String, ByVal trans As SqlTransaction) As String
        Try
            Return clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Fiscal_Name from TSPL_Fiscal_Year_Master Where Fiscal_Code='" + StrCode + "'", trans))
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
End Class
