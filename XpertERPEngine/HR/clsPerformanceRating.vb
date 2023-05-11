Imports System.Data.SqlClient
Imports common
Public Class clsPerformanceRating

#Region "variables"
    Public CODE As String = Nothing
    Public MONTH_YEAR As DateTime
    Public Emp_Code As String = Nothing
    Public PERFORMANCE_GROUP As String = Nothing
    Public PerformanceCode As String = Nothing
    Public PERFORMANCE_PERSENT As Double = 0
    Public PERFORMANCE_PERSENT_GAIN As Double = 0
#End Region

    Public Shared Function SaveData(ByVal strUserCode As String, ByVal ForMonth As Date, ByVal arr As List(Of clsPerformanceRating)) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Dim qry As String = ""
        Dim IsSave As Boolean = True
        Try
            qry = " delete from TSPL_HR_PERFORMANCE_RATING where Emp_Code = '" + strUserCode + "' and Comp_Code = '" + objCommonVar.CurrentCompanyCode + "' " & _
                                " and DatePart(Month,MONTH_YEAR) =DatePart(Month,'" + clsCommon.GetPrintDate(ForMonth, "dd/MMM/yyyy") + "') and DatePart(year,MONTH_YEAR) =DatePart(year,'" + clsCommon.GetPrintDate(ForMonth, "dd/MMM/yyyy") + "') "
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            For Each obj As clsPerformanceRating In arr
                Dim coll As New Hashtable()
                qry = "select MAX(CODE) from TSPL_HR_PERFORMANCE_RATING "
                obj.CODE = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
                If clsCommon.myLen(obj.CODE) <= 0 Then
                    obj.CODE = "BM00000000001"
                Else
                    obj.CODE = clsCommon.incval(obj.CODE)
                End If
                clsCommon.AddColumnsForChange(coll, "CODE", obj.CODE)
                clsCommon.AddColumnsForChange(coll, "MONTH_YEAR", clsCommon.GetPrintDate(obj.MONTH_YEAR, "dd/MMM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "Emp_Code ", obj.Emp_Code)
                clsCommon.AddColumnsForChange(coll, "PERFORMANCE_GROUP", obj.PERFORMANCE_GROUP, True)
                clsCommon.AddColumnsForChange(coll, "PerformanceCode", obj.PerformanceCode, True)
                clsCommon.AddColumnsForChange(coll, "PERFORMANCE_PERSENT", obj.PERFORMANCE_PERSENT)
                clsCommon.AddColumnsForChange(coll, "PERFORMANCE_PERSENT_GAIN", obj.PERFORMANCE_PERSENT_GAIN)
                clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode, True)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                IsSave = IsSave AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_HR_PERFORMANCE_RATING", OMInsertOrUpdate.Insert, "", trans)
            Next
            If IsSave Then
                trans.Commit()
            End If
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return IsSave
    End Function

    Public Shared Function GetData(ByVal strUserCode As String, ByVal ForMonth As Date, ByVal trans As SqlTransaction) As DataTable
        Dim obj As clsPerformanceRating = Nothing
        Dim Arr As List(Of clsPerformanceRating) = Nothing
        Dim QRY As String = ""
        'Dim qry As String = " select CODE, User_Code, MONTH_YEAR, PERFORMANCE_GROUP, PerformanceCode, PERFORMANCE_PERSENT, PERFORMANCE_PERSENT_GAIN " & _
        '                    " from TSPL_HR_PERFORMANCE_RATING " & _
        '                    " where User_Code='" + strUserCode + "' and TSPL_HR_PERFORMANCE_RATING.Comp_Code='" + objCommonVar.CurrentCompanyCode + "' " & _
        '                    " and DatePart(Month,MONTH_YEAR) =DatePart(Month,'" + clsCommon.GetPrintDate(ForMonth, "dd/MMM/yyyy") + "') and DatePart(year,MONTH_YEAR) =DatePart(year,'" + clsCommon.GetPrintDate(ForMonth, "dd/MMM/yyyy") + "') "
        QRY = "select TSPL_HR_PERFORMANCE_RATING.CODE, TSPL_HR_PERFORMANCE_RATING.Emp_Code , MONTH_YEAR, PERFORMANCE_GROUP, PerformanceCode, PERFORMANCE_PERSENT, PERFORMANCE_PERSENT_GAIN  ,(TSPL_HR_PERFORMANCE_GROUP_MAPPING.Persent * TSPL_HR_PERFORMANCE_RATING.PERFORMANCE_PERSENT  ) /100 As [Actual Total],(TSPL_HR_PERFORMANCE_GROUP_MAPPING.Persent * TSPL_HR_PERFORMANCE_RATING.PERFORMANCE_PERSENT_GAIN  ) /100 As [Actual Score],TSPL_HR_PERFORMANCE_GROUP_MAPPING.Persent  AS TotalPer  from  TSPL_HR_PERFORMANCE_RATING " & _
            " left outer join TSPL_HR_PERFORMANCE_GROUP_MAPPING on TSPL_HR_PERFORMANCE_GROUP_MAPPING.PERFORMANCE_GROUP_Code = TSPL_HR_PERFORMANCE_RATING.PERFORMANCE_GROUP " & _
            " AND TSPL_HR_PERFORMANCE_RATING.Emp_Code = TSPL_HR_PERFORMANCE_GROUP_MAPPING.Emp_Code   "
        QRY += "  WHERE TSPL_HR_PERFORMANCE_GROUP_MAPPING.Emp_Code='" + strUserCode + "' and TSPL_HR_PERFORMANCE_RATING.Comp_Code='" + objCommonVar.CurrentCompanyCode + "' " & _
            " AND DatePart(Month,MONTH_YEAR) =DatePart(Month,'" + clsCommon.GetPrintDate(ForMonth, "dd/MMM/yyyy") + "') and DatePart(year,MONTH_YEAR) =DatePart(year,'" + clsCommon.GetPrintDate(ForMonth, "dd/MMM/yyyy") + "')"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(QRY, trans)
        Return dt
    End Function

End Class
