Imports System.Data.SqlClient
Imports common
Public Class clsOverheadCost
#Region "Variables"
    Public COST_CODE As String = Nothing
    Public COST_DATE As Date? = Nothing
    Public Description As String = Nothing
    Public Created_By As String = Nothing
    Public Created_Date As Date? = Nothing
    Public Modify_By As String = Nothing
    Public Modify_Date As Date? = Nothing
    Public Comp_code As String = Nothing
    Public GLAccount As String = Nothing
    Public RatePerHour As Double = Nothing
#End Region

    Public Shared Function SaveData(ByVal obj As clsOverheadCost, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim qry As String = ""
        Dim IsSaved As Boolean = False
        Try
            Dim coll As New Hashtable()

            clsCommon.AddColumnsForChange(coll, "COST_DATE", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Description", obj.Description)
            clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Comp_code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "GL_Acc", obj.GLAccount)
            clsCommon.AddColumnsForChange(coll, "RatePerHour", obj.RatePerHour, True)
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                clsCommon.AddColumnsForChange(coll, "COST_CODE", obj.COST_CODE)
                IsSaved = clsCommonFunctionality.UpdateDataTable(coll, "TSPL_OVERHEAD_COST", OMInsertOrUpdate.Insert, "", trans)
            Else
                IsSaved = clsCommonFunctionality.UpdateDataTable(coll, "TSPL_OVERHEAD_COST", OMInsertOrUpdate.Update, "COST_CODE='" + obj.COST_CODE + "'", trans)
            End If

        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return IsSaved
    End Function


    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsOverheadCost
        Dim obj As clsOverheadCost = Nothing
        Dim qry As String = "select Cost_Code,   Description, Created_By , convert (varchar, Created_Date,103) as Created_Date,GL_Acc,RatePerHour  from TSPL_OVERHEAD_COST  where 2=2 "
        Dim whrclas As String = ""
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_OVERHEAD_COST.Cost_Code = (select MIN(Cost_Code) from TSPL_OVERHEAD_COST WHERE 1=1 " + whrclas + " )"
            Case NavigatorType.Last
                qry += " and TSPL_OVERHEAD_COST.Cost_Code = (select Max(Cost_Code) from TSPL_OVERHEAD_COST WHERE 1=1 " + whrclas + " )"
            Case NavigatorType.Current
                qry += " and TSPL_OVERHEAD_COST.Cost_Code = (select TOP 1 Cost_Code from TSPL_OVERHEAD_COST WHERE 1=1 " + whrclas + " and Cost_Code='" + strCode + "' )"
            Case NavigatorType.Next
                qry += " and TSPL_OVERHEAD_COST.Cost_Code = (select Min(Cost_Code) from TSPL_OVERHEAD_COST where Cost_Code > '" + strCode + "' " + whrclas + ")"
            Case NavigatorType.Previous
                qry += " and TSPL_OVERHEAD_COST.Cost_Code = (select Max(Cost_Code) from TSPL_OVERHEAD_COST where Cost_Code < '" + strCode + "' " + whrclas + ")"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsOverheadCost()
            obj.COST_CODE = clsCommon.myCstr(dt.Rows(0)("COST_CODE"))
            obj.Description = clsCommon.myCstr(dt.Rows(0)("Description"))
            obj.GLAccount = clsCommon.myCstr(dt.Rows(0)("GL_Acc"))
            If String.IsNullOrEmpty(clsCommon.myCstr(dt.Rows(0)("RatePerHour"))) = False Then
                obj.RatePerHour = clsCommon.myCdbl(dt.Rows(0)("RatePerHour"))
            Else
                obj.RatePerHour = 0
            End If
        End If
        Return obj
    End Function

    Public Shared Function GetDescription(ByVal strCode As String) As String
        Return clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description from TSPL_OVERHEAD_COST where Cost_Code='" + strCode + "'"))
    End Function

    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim qry As String = ""
        Try
            qry = "Delete from TSPL_OVERHEAD_COST where TSPL_OVERHEAD_COST.Cost_Code='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry)
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function

    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = " select Cost_Code as Code,   Description, Created_By as [Created By] , convert (varchar, Created_Date,103) as [Created Date]  from TSPL_OVERHEAD_COST "
        str = clsCommon.ShowSelectForm("OverCostFnd", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str

    End Function

    Public Shared Function CheckNewEntry(ByVal Code As String, ByVal trans As SqlTransaction) As String
        Dim qry As String = "select Cost_Code from TSPL_OVERHEAD_COST where Cost_Code ='" + Code + "'   "
        Dim dt As DataTable
        dt = clsDBFuncationality.GetDataTable(qry, trans)
        If dt.Rows.Count > 0 Then
            Return False
        Else
            Return True
        End If

    End Function

End Class
