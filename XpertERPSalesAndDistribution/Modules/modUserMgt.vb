Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Data.SqlClient
Imports Telerik.WinControls.UI
Imports Telerik.WinControls.Data
Imports Telerik.Data
Imports Telerik.WinControls.Enumerations
Imports Telerik.WinControls
Imports System.Text.RegularExpressions
Imports common



Module modUserMgt
    Dim blnAddMode As Boolean
    Dim Rowcount As Integer
    Dim strSql As String
    Public strUserCode, strCompany As String
    Public blnRead As Boolean
    Dim dr As DataTable
    Dim userCode, companyCode As String

   

    Public Enum enuUserRights
        enuRead = 0
        enuModify = 1
        enuDelete = 2
        enuAuthorised = 3
    End Enum

    Public Function funGetPermissions(ByVal strActivity As String, ByVal strProgramCode As String, Optional ByVal strUser As String = vbNullString) As String
        Try
            Dim strRight As String = ""
            Dim arrRight() As String
            Dim intCtr As Integer
            ' Dim cmdUserRight As SqlCommand
            'Dim objReader As SqlDataReader

            If strUser = vbNullString Then strUser = strUserCode
            arrRight = Split(strActivity, ",")
            strActivity = vbNullString
            For intCtr = 0 To UBound(arrRight)
                Select Case UCase(Trim(arrRight(intCtr)))
                    Case enuUserRights.enuRead
                        strRight = "READ_FLAG"
                    Case enuUserRights.enuModify
                        strRight = "MODIFY_FLAG"
                    Case enuUserRights.enuDelete
                        strRight = "DELETE_FLAG"
                    Case enuUserRights.enuAuthorised
                        strRight = "Authorized_Flag"
                End Select

                If strRight <> vbNullString Then
                    strSql = "SELECT " & strRight & " FROM TSPL_GROUP_PROGRAM_MAPPING WHERE PROGRAM_CODE='" & strProgramCode & "' AND GROUP_CODE IN(" & _
                        "SELECT GROUP_CODE FROM TSPL_USER_GROUP_MAPPING WHERE USER_CODE='" & strUser & "' and comp_code='" & strCompany & "')"
                    dr = clsDBFuncationality.GetDataTable(strSql)
                    strSql = "0"
                    For Each tdr As DataRow In dr.Rows
                        If UCase(tdr(0)) = "1" Then
                            strSql = "1"
                            Exit For
                        End If
                    Next
                   
                    strActivity = strActivity & strSql

                End If
                strActivity = strActivity & ","
            Next
            If intCtr > 0 Then
                strActivity = Left(strActivity, (Len(strActivity) - 1))
            End If

            funGetPermissions = strActivity
        Catch er As Exception
            MsgBox(er.Message)
            Return False
        End Try
    End Function

    'Public Function funCheckLoginStatus() As Boolean
    '    Try
    '        If strUserCode = vbNullString Then
    '            Dim fv As New frmLogin
    '            fv.Show()
    '            If strUserCode = vbNullString Then Exit Function
    '        End If
    '        funCheckLoginStatus = True
    '    Catch er As Exception
    '        MsgBox(er.Message)
    '    End Try
    'End Function
End Module
