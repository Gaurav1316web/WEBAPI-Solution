Imports common
Imports System.Data.SqlClient
Imports System.Data
Public Class clsCrystalReportActionType
#Region "Variables"
    Public Action_Type As Boolean = False
    Public Form_ID As String = Nothing
    Public Report_Name As String = Nothing
    Public Arr As List(Of clsCrystalReportActionType) = Nothing
#End Region
    Public Function SaveData(ByVal obj As clsCrystalReportActionType, ByVal isNewEntry As Boolean) As Boolean
        Dim isSaved As Boolean = True
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            For Each objtr As clsCrystalReportActionType In obj.Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Form_ID", objtr.Form_ID)
                clsCommon.AddColumnsForChange(coll, "Report_Name", objtr.Report_Name)
                clsCommon.AddColumnsForChange(coll, "Action_Type", IIf(objtr.ACTION_TYPE, "1", "0"))
                If isNewEntry Then
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CRYSTAL_REPORT_ACTION", OMInsertOrUpdate.Insert, "", trans)
                Else
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CRYSTAL_REPORT_ACTION", OMInsertOrUpdate.Update, "Form_ID='" + objtr.Form_ID + "' and Report_Name = '" + objtr.Report_Name + "'", trans)
                End If
            Next
            If isSaved Then
                trans.Commit()
            End If
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function

End Class
