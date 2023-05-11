Imports common
Imports System.Data
Imports System.Data.SqlClient
Public Class clsCrptFooterSetting

#Region "Variables"
    Public Footer_Text As String
    Public Frm_ID As String

#End Region
    Public Shared Function DeleteData(ByVal strID As String) As Boolean
        Dim isSaved As Boolean

        Try
            isSaved = False

            If (clsCommon.myLen(strID) <= 0) Then
                Throw New Exception("Code not found to Delete")
            End If

            Dim qry As String
            qry = "delete from TSPL_Crystal_Report_Footer_Setting where Frm_ID ='" + strID + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry)


        Catch ex As Exception

            Throw New Exception(ex.Message.ToString())
        End Try
        Return isSaved
    End Function
    Public Function SaveData(ByVal obj As clsCrptFooterSetting) As Boolean
        Dim isSaved As Boolean = True
        Try
            Dim coll As New Hashtable()

            Dim qry As String
            qry = "delete from TSPL_Crystal_Report_Footer_Setting where Frm_ID= '" & obj.Frm_ID & "'"
            clsCommon.AddColumnsForChange(coll, "Frm_ID", obj.Frm_ID)
            clsCommon.AddColumnsForChange(coll, "Footer_Text", obj.Footer_Text)



            Dim check As Integer = clsDBFuncationality.getSingleValue(qry)
            If check = 0 Then
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Crystal_Report_Footer_Setting", OMInsertOrUpdate.Insert, "")
            Else
                Throw New Exception("This Code Is Already Exist")
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal FormID As String) As clsCrptFooterSetting
        Dim obj As clsCrptFooterSetting = Nothing

        Dim qry As String = "select * from TSPL_Crystal_Report_Footer_Setting where Frm_ID= '" + FormID + "' "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsCrptFooterSetting()

            obj.Footer_Text = clsCommon.myCstr(dt.Rows(0)("Footer_Text"))


        End If
        Return obj
    End Function


End Class
