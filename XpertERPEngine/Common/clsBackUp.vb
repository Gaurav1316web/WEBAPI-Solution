Imports common
Imports System.Data.SqlClient

Public Class clsBackUp

    Public Shared Sub TakeBackup(ByVal backUpLoc As String)
        Try
            Dim qry As String = "Select * from TSPL_SERVER_PATH "
            Dim dtPath As DataTable = clsDBFuncationality.GetDataTable(qry)

            If dtPath IsNot Nothing AndAlso dtPath.Rows.Count > 0 Then
                Dim strServerName As String = clsCommon.myCstr(dtPath.Rows(0)("SERVER_NAME"))
                Dim strServerPath As String = clsCommon.myCstr(dtPath.Rows(0)("SERVER_PATH"))
                Dim strClientPath As String = backUpLoc

                Dim strDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "yyyy_MM_dd_hh_mm_tt")
                Dim strFileName As String = strServerPath + "\" + objCommonVar.CurrentCompanyCode + "_" + strDate + ".bak"

                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Backup_Code", strFileName)
                clsCommon.AddColumnsForChange(coll, "Backup_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy"))
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Backup", OMInsertOrUpdate.Insert, "", Nothing)

                Try
                    qry = "BACKUP DATABASE " + objCommonVar.CurrentCompanyCode + " TO DISK = '" + strFileName + "'  "
                    clsDBFuncationality.ExecuteNonQuery(qry)

                    Dim copyFileName As String = strFileName.Replace(":\", "\")
                    System.IO.File.Copy("\\" + strServerName.Trim() + "\\" + copyFileName, strClientPath + "\" + objCommonVar.CurrentCompanyCode + "_" + strDate + ".bak ")

                    'clsPreStart.TakeBakup(strFileName)
                Catch ex As Exception
                    qry = "Delete from TSPL_Backup where Backup_Code='" + strFileName + "'"
                    clsDBFuncationality.ExecuteNonQuery(qry)
                End Try
            Else
                clsCommon.MyMessageBoxShow("Please Set Server Name and Server Path at Back Up Form.")
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

End Class
