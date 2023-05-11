Imports common
Imports System.Data.SqlClient
Public Class ClsAgencyMaster

#Region "variable"
    Public Code As String = Nothing
    Public Name As String = Nothing
#End Region
    Public Shared Function SaveData(ByVal Agency As ClsAgencyMaster, ByVal isnewentry As Boolean)
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try

            Dim qry As String = "select count(*) from TSPL_HR_Agency_master where comp_code='" + objCommonVar.CurrentCompanyCode + "' and Code='" + Agency.Code + "'"
            Dim isexist As Integer = clsDBFuncationality.getSingleValue(qry, trans)
            If isexist = 0 Then
                isnewentry = True
            Else
                isnewentry = False

            End If


            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Code", Agency.Code)
            clsCommon.AddColumnsForChange(coll, "Name", Agency.Name)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            If isnewentry Then
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_HR_Agency_master", OMInsertOrUpdate.Insert, "", trans)
            Else
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_HR_Agency_master ", OMInsertOrUpdate.Update, " comp_code='" + objCommonVar.CurrentCompanyCode + "' and Code='" + Agency.Code + "'", trans)

            End If
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
            Return False
        End Try

    End Function



    Public Function DeleteData(ByVal Agency As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If (clsCommon.myLen(Agency >= 0)) Then
                Dim qry As String = "delete from TSPL_HR_Agency_master where comp_code='" + objCommonVar.CurrentCompanyCode + "' and code='" + Agency + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
            End If
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try

    End Function
    Public Shared Function GetData(ByVal code As String, ByVal navigatortype As NavigatorType) As ClsAgencyMaster
        Try


            Dim obj As ClsAgencyMaster = Nothing
            Dim qst As String = ("select Code,Name from TSPL_HR_Agency_master where 2=2 and comp_code='" + objCommonVar.CurrentCompanyCode + "' ")
            Select Case navigatortype
                Case navigatortype.Current
                    qst += "and Code in ('" + code + "')"
                Case navigatortype.Next
                    qst += "and Code in (select  min(Code) from TSPL_HR_Agency_master where comp_code='" + objCommonVar.CurrentCompanyCode + "' and Code >'" + code + "')"
                Case navigatortype.First
                    qst += "and Code in (select MIN(Code) from TSPL_HR_Agency_master where comp_code='" + objCommonVar.CurrentCompanyCode + "')"

                Case navigatortype.Last
                    qst += "and Code in (select Max(Code) from TSPL_HR_Agency_master where comp_code='" + objCommonVar.CurrentCompanyCode + "')"
                Case navigatortype.Previous
                    qst += "and Code in (select  max(Code) from TSPL_HR_Agency_master where comp_code='" + objCommonVar.CurrentCompanyCode + "' and Code <'" + code + "')"

            End Select
            Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qst)
            If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
                obj = New ClsAgencyMaster
                obj.Code = clsCommon.myCstr(dt1.Rows(0)("Code"))
                obj.Name = clsCommon.myCstr(dt1.Rows(0)("Name"))

            End If
            Return obj
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function


End Class
