Imports common
Imports System.Data.SqlClient

Public Class ClsHRSettings
#Region "Variables"
    Public From_Date As String = Nothing
    Public To_Date As String = Nothing
    Public Single_Parameter As Integer = 0
    Public Double_Parameter As Integer = 0
#End Region
#Region "Functions"
    Public Shared Function SaveData(ByVal obj As ClsHRSettings, ByVal trans As SqlTransaction) As Boolean
        Dim qry As String = ""
        Try
            clsDBFuncationality.ExecuteNonQuery("Delete FROM TSPL_HR_SETTINGS ", trans)
            Dim coll As New Hashtable()

            clsCommon.AddColumnsForChange(coll, "From_Date", clsCommon.GetPrintDate(obj.From_Date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "To_Date", clsCommon.GetPrintDate(obj.To_Date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Single_Parameter", obj.Single_Parameter)
            clsCommon.AddColumnsForChange(coll, "Double_Parameter", obj.Double_Parameter)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))

            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_HR_SETTINGS", OMInsertOrUpdate.Insert, "", trans)

        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function
    Public Shared Function GetData(Optional ByVal trans As SqlTransaction = Nothing) As ClsHRSettings

        Dim obj As ClsHRSettings = Nothing
        Dim Arr As List(Of ClsHRSettings) = Nothing
        Dim qry As String = "SELECT * FROM TSPL_HR_SETTINGS "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New ClsHRSettings()
            obj.From_Date = clsCommon.myCDate(dt.Rows(0)("From_Date"))
            obj.To_Date = clsCommon.myCDate(dt.Rows(0)("To_Date"))
            obj.Single_Parameter = clsCommon.myCdbl(dt.Rows(0)("Single_Parameter"))
            obj.Double_Parameter = clsCommon.myCdbl(dt.Rows(0)("Double_Parameter"))
        End If
        Return obj
    End Function
#End Region
End Class
