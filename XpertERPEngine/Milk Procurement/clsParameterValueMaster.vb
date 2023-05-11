Imports common
Imports System.Data.SqlClient

Public Class clsParameterValueMaster
    Public Parameter_CODE As String = Nothing
    Public Value As String = Nothing
    Public Specification As String = Nothing
    Public Created_By As String = Nothing
    Public Created_Date As String = Nothing
    Public Modified_By As String = Nothing
    Public Modified_Date As String = Nothing
    Public Comp_Code As String = Nothing
    Public isNewEntry As Boolean = False
    Public Shared Function GetFinder(ByVal whrCls As String, ByVal CurrCode As String, ByVal isButtonClicked As Boolean) As String
        Dim strcode As String = ""
        Dim qry As String = "select Parameter_CODE as Code,Value,Specification,Created_By as [Created By],Modified_By as [Modified By],Modified_Date as [Modified Date] from tspl_Parameter_value_master"
        strcode = clsCommon.ShowSelectForm("PMTFND", qry, "Value", "", CurrCode, "Code", isButtonClicked)

        Return strcode
    End Function
    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim isSaved As Boolean

        Try
            isSaved = False

            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Code not found to Delete")
            End If

            Dim qry As String
            qry = "delete from tspl_Parameter_value_master where Parameter_code ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry)


        Catch ex As Exception

            common.clsCommon.MyMessageBoxShow(ex.Message.ToString())
        End Try
        Return isSaved
    End Function

    Public Shared Function SaveData(ByVal arrObj As List(Of clsParameterValueMaster)) As Boolean
        Dim isSaved As Boolean = True
        Try
            If arrObj.Count <= 0 Then
                Throw New Exception("No Found To Save")
            End If
            clsParameterValueMaster.DeleteData(arrObj(0).Parameter_CODE)
            Dim coll As Hashtable
            For i As Integer = 0 To arrObj.Count - 1
                coll = New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Parameter_CODE", arrObj(i).Parameter_CODE)
                clsCommon.AddColumnsForChange(coll, "Value", arrObj(i).Value)
                clsCommon.AddColumnsForChange(coll, "Specification", arrObj(i).Specification)
                clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "Comp_code", objCommonVar.CurrentCompanyCode)
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "tspl_Parameter_value_master", OMInsertOrUpdate.Insert, "")
            Next
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function


End Class
