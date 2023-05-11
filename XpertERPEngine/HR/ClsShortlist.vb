Imports common
Imports System.Data
Imports System.Data.SqlClient

Public Class ClsShortlist

#Region "Variables"
    Public APPLICANT_CODE As String
    Public Requisition_Code As String
    Public IsShort As String
    Public Post As String

#End Region

    Public Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As ClsShortlist
        Return GetData(strCode, NavType, Nothing)
    End Function
    Public Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As ClsShortlist
        Dim obj As New ClsShortlist()

        Dim qst As String = "Select * From TSPL_HR_APPLICANT_ENTRY where  2=2"
        Select Case NavType
            Case NavigatorType.Current
                qst += " and Requisition_Code = '" + strCode + "'"
            Case NavigatorType.Next
                qst += "and Requisition_Code in (select min(Requisition_Code) from TSPL_HR_APPLICANT_ENTRY where Requisition_Code > '" + strCode + "' ) "
            Case NavigatorType.First
                qst += "and Requisition_Code in (select MIN(Requisition_Code) from TSPL_HR_APPLICANT_ENTRY)"
            Case NavigatorType.Last
                qst += "and Requisition_Code in (select Max(Requisition_Code) from TSPL_HR_APPLICANT_ENTRY)"
            Case NavigatorType.Previous
                qst += "and Requisition_Code in (select max(Requisition_Code) from TSPL_HR_APPLICANT_ENTRY where Requisition_Code < '" + strCode + "'  )"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qst)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            'obj.APPLICANT_CODE = clsCommon.myCstr(dt.Rows(0)("Section Code"))
            'obj.APPLICANT_CODE = clsCommon.myCstr(dt.Rows(0)("APPLICANT_CODE"))
            obj.Requisition_Code = clsCommon.myCstr(dt.Rows(0)("Requisition_Code"))
        End If
        Return obj
    End Function
End Class
