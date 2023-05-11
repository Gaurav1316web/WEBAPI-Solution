Public Class clsReportDataMemory
#Region "Variables"
    Dim REPORT_ID As String
    Dim Report_Type As String
    Dim Periodicity As String
    Dim ColumnsCount As Integer
    Dim RowsCount As Integer
    Dim PerRecordMemory As Decimal
    Dim Total_Memory_Consumed As Decimal   
#End Region

    Public Shared Function SaveData(ByVal obj As clsReportDataMemory, ByVal trans As SqlClient.SqlTransaction)
        Dim coll As New Hashtable()
        Dim isNewEntry As Boolean
        Dim qry As String = "select count(*) as RecCount from TSPL_REPORT_MEMORY_CONSM where REPORT_ID='" & obj.REPORT_ID & "' and Report_Type='" & obj.Report_Type & "'"
        Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))
        If count > 0 Then
            isNewEntry = False
        Else
            isNewEntry = True
        End If

        '' update production avg cost
        clsCommon.AddColumnsForChange(coll, "REPORT_ID", obj.REPORT_ID)
        clsCommon.AddColumnsForChange(coll, "Report_Type", obj.Report_Type)
        clsCommon.AddColumnsForChange(coll, "Periodicity", obj.Periodicity)
        clsCommon.AddColumnsForChange(coll, "ColumnsCount", obj.ColumnsCount)
        clsCommon.AddColumnsForChange(coll, "RowsCount", obj.RowsCount)
        clsCommon.AddColumnsForChange(coll, "PerRecordMemory", obj.PerRecordMemory)
        clsCommon.AddColumnsForChange(coll, "Total_Memory_Consumed", obj.Total_Memory_Consumed)
        If isNewEntry Then
            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_REPORT_MEMORY_CONSM", OMInsertOrUpdate.Insert, "", trans)
        Else
            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_REPORT_MEMORY_CONSM", OMInsertOrUpdate.Update, "TSPL_REPORT_MEMORY_CONSM.REPORT_ID='" & obj.REPORT_ID & "' and TSPL_REPORT_MEMORY_CONSM.Report_Type='" & obj.Report_Type & "' ", trans)
        End If
        Return True
    End Function
End Class
