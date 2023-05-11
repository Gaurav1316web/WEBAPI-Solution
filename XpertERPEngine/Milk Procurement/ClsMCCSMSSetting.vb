Imports System.Data.SqlClient
Imports common
Public Class ClsMCCSMSSetting
#Region "variable"
    Public Program_Code As String
    Public Loc_Code As String
    Public _Type As String
    Public _Name As String
    Public _MailId As String
    Public _MobileNo As String
    Public DTMccSMS As DataTable

#End Region
    Public Shared Function SaveData(ByVal Arr As List(Of ClsMCCSMSSetting)) As Boolean

        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim isDeleted As Boolean = False
            For Each obj As ClsMCCSMSSetting In Arr
                If isDeleted = False Then
                    clsDBFuncationality.ExecuteNonQuery("Delete from TSPL_MCC_MAIL_SMS_Setting WHERE Program_Code='" + obj.Program_Code + "'", trans)
                    isDeleted = True
                End If
                Dim IsSaved As Boolean = False
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Program_Code", obj.Program_Code)
                clsCommon.AddColumnsForChange(coll, "Loc_Code", obj.Loc_Code)
                clsCommon.AddColumnsForChange(coll, "_Type", obj._Type)
                clsCommon.AddColumnsForChange(coll, "_Name", obj._Name)
                clsCommon.AddColumnsForChange(coll, "_EMailId", obj._MailId)
                clsCommon.AddColumnsForChange(coll, "_MobileNo", obj._MobileNo)
                clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
                clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))

                IsSaved = clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MCC_MAIL_SMS_Setting", OMInsertOrUpdate.Insert, "", trans)
            Next
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            clsCommon.MyMessageBoxShow(ex.Message)

        End Try
        Return True
    End Function

    Public Shared Function DeleteData(ByVal strcode As String, ByVal trans As SqlTransaction) As Boolean
        Try
            If (clsCommon.myLen(strcode >= 0)) Then
                Dim qry As String = "delete from TSPL_MCC_MAIL_SMS_Setting where Program_Code='" + strcode + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
            End If
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function GetData(ByVal strcode As String) As List(Of ClsMCCSMSSetting)
        Try
            Dim qry As String = " select _Type ,_Name,_EMailId ,_MobileNo,Loc_Code  from " _
            & " TSPL_MCC_MAIL_SMS_Setting where proGRAM_code='" & strcode & "'"
            Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qry)
            Dim obj As ClsMCCSMSSetting
            Dim objlist As New List(Of ClsMCCSMSSetting)
            For Each row As DataRow In dt1.Rows
                obj = New ClsMCCSMSSetting
                obj._Name = clsCommon.myCstr(row.Item("_Name"))
                obj._Type = clsCommon.myCstr(row.Item("_Type"))
                obj._MailId = clsCommon.myCstr(row.Item("_EMailId"))
                obj._MobileNo = clsCommon.myCstr(row.Item("_MobileNo"))
                obj.Loc_Code = clsCommon.myCstr(row.Item("Loc_Code"))
                objlist.Add(obj)
            Next
            Return objlist
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
End Class
