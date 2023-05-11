Imports System.Data.SqlClient
Imports common

Public Class ClsQualificationMaster

#Region "Variables"
    Public Qualification_Code As String = Nothing
    Public Qualification_Name As String = Nothing
    Public Created_By As String = Nothing
    Public Created_Date As Date? = Nothing
    Public Modified_By As String = Nothing
    Public Modified_Date As Date? = Nothing
#End Region

    Public Shared Function SaveData(ByVal obj As ClsQualificationMaster, ByVal isNewEntry As Boolean) As Boolean
        Dim qry As String = ""
        Dim IsSaved As Boolean = False
        Try
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Qualification_Name", obj.Qualification_Name)
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy hh:mm tt"))
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy hh:mm tt"))
                clsCommon.AddColumnsForChange(coll, "Qualification_Code", obj.Qualification_Code)
                IsSaved = clsCommonFunctionality.UpdateDataTable(coll, "TSPL_HR_QUALIFICATION_MASTER", OMInsertOrUpdate.Insert, "")
            Else
                IsSaved = clsCommonFunctionality.UpdateDataTable(coll, "TSPL_HR_QUALIFICATION_MASTER", OMInsertOrUpdate.Update, "TSPL_HR_QUALIFICATION_MASTER.Qualification_Code='" + obj.Qualification_Code + "'")
            End If

        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return IsSaved
    End Function

    'Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As ClsQualificationMaster
    '    Return GetData(strCode, NavType, Nothing)
    'End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As ClsQualificationMaster
        Dim obj As ClsQualificationMaster = Nothing
        Dim Arr As List(Of ClsQualificationMaster) = Nothing
        Dim qry As String = "select Qualification_Code ,Qualification_Name from TSPL_HR_QUALIFICATION_MASTER where 2=2 "
        Dim whrclas As String = ""
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_HR_QUALIFICATION_MASTER.Qualification_Code = (select MIN(Qualification_Code) from TSPL_HR_QUALIFICATION_MASTER WHERE 1=1 " + whrclas + " )"
            Case NavigatorType.Last
                qry += " and TSPL_HR_QUALIFICATION_MASTER.Qualification_Code = (select Max(Qualification_Code) from TSPL_HR_QUALIFICATION_MASTER WHERE 1=1 " + whrclas + " )"
            Case NavigatorType.Current
                qry += " and TSPL_HR_QUALIFICATION_MASTER.Qualification_Code = (select TOP 1 Qualification_Code from TSPL_HR_QUALIFICATION_MASTER WHERE 1=1 " + whrclas + " and Qualification_Code='" + strCode + "' )"
            Case NavigatorType.Next
                qry += " and TSPL_HR_QUALIFICATION_MASTER.Qualification_Code = (select Min(Qualification_Code) from TSPL_HR_QUALIFICATION_MASTER where Qualification_Code > '" + strCode + "' " + whrclas + ")"
            Case NavigatorType.Previous
                qry += " and TSPL_HR_QUALIFICATION_MASTER.Qualification_Code = (select Max(Qualification_Code) from TSPL_HR_QUALIFICATION_MASTER where Qualification_Code < '" + strCode + "' " + whrclas + ")"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New ClsQualificationMaster()
            obj.Qualification_Code = clsCommon.myCstr(dt.Rows(0)("Qualification_Code"))
            obj.Qualification_Name = clsCommon.myCstr(dt.Rows(0)("Qualification_Name"))
        End If
        Return obj
    End Function
End Class
