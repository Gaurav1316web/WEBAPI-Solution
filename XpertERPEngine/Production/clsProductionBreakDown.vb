Imports common
Imports System.Data.SqlClient

Public Class clsProductionBreakDown
#Region "Variables"
    Public Doc_no As String = Nothing
    Public Doc_Date As Date = Nothing
    Public Description As String = Nothing
    Public LOCATION_CODE As String = Nothing
    Public Break_Down_Code As String = Nothing
    Public start_time As DateTime = Nothing
    Public end_time As DateTime = Nothing

#End Region

    Public Shared Function GetFinder(ByVal whrCls As String, ByVal CurrCode As String, ByVal isButtonClicked As Boolean) As String
        Dim qry As String = "select convert(varchar,TSPL_BREAK_DOWN_ENTRY.doc_date,103) as [Date],TSPL_BREAK_DOWN_ENTRY.doc_no as Code,TSPL_BREAK_DOWN_ENTRY.description as [Description],TSPL_LOCATION_MASTER.Loc_Short_Name as [Location],TSPL_BREAK_DOWN_MASTER.Name as [Break Down Desc],TSPL_BREAK_DOWN_ENTRY.start_time as [Start Time],TSPL_BREAK_DOWN_ENTRY.end_time as [End Time] from TSPL_BREAK_DOWN_ENTRY left join TSPL_BREAK_DOWN_MASTER ON TSPL_BREAK_DOWN_ENTRY.Break_Down_Code = TSPL_BREAK_DOWN_MASTER.CODE left join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_BREAK_DOWN_ENTRY.Location_Code "
        Dim str As String = ""

        str = clsCommon.ShowSelectForm("PBDFND", qry, "Code", whrCls, CurrCode, "Code", isButtonClicked)

        Return str
    End Function

    Public Shared Function SaveData(ByVal strCode As String, ByVal obj As clsProductionBreakDown, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim isSaved As Boolean = True

            If isNewEntry Then
                obj.Doc_no = clsCommon.myCstr(clsERPFuncationality.GetNextCode(trans, clsCommon.GetPrintDate(obj.Doc_Date, "dd/MMM/yyyy"), clsDocType.BreakDownEntry, "", ""))
            End If

            Dim coll As New Hashtable()

            clsCommon.AddColumnsForChange(coll, "comp_code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Doc_No", obj.Doc_no)
            clsCommon.AddColumnsForChange(coll, "Doc_Date", clsCommon.GetPrintDate(obj.Doc_Date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Description", obj.Description)
            clsCommon.AddColumnsForChange(coll, "Location_Code", obj.LOCATION_CODE)
            clsCommon.AddColumnsForChange(coll, "Break_Down_Code", obj.Break_Down_Code)
            clsCommon.AddColumnsForChange(coll, "Start_Time", clsCommon.GetPrintDate(obj.start_time, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "End_Time", clsCommon.GetPrintDate(obj.end_time, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))

            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))

                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_BREAK_DOWN_ENTRY", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_BREAK_DOWN_ENTRY", OMInsertOrUpdate.Update, " doc_no='" + obj.Doc_no + "'", trans)
            End If
            trans.Commit()
            Return isSaved
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsProductionBreakDown
        Try
            Dim obj As New clsProductionBreakDown()

            Dim qry As String = "select TSPL_BREAK_DOWN_ENTRY.Location_Code,TSPL_BREAK_DOWN_ENTRY.Break_Down_Code,TSPL_BREAK_DOWN_ENTRY.description as description ,TSPL_BREAK_DOWN_ENTRY.doc_no,TSPL_BREAK_DOWN_ENTRY.doc_date,TSPL_BREAK_DOWN_ENTRY.start_time,TSPL_BREAK_DOWN_ENTRY.end_time from TSPL_BREAK_DOWN_ENTRY "

            Select Case NavType
                Case NavigatorType.Current
                    qry += " where TSPL_BREAK_DOWN_ENTRY.doc_no='" + strCode + "'"
                Case NavigatorType.First
                    qry += " where TSPL_BREAK_DOWN_ENTRY.doc_no in (Select min(doc_no) from TSPL_BREAK_DOWN_ENTRY)"
                Case NavigatorType.Last
                    qry += " where TSPL_BREAK_DOWN_ENTRY.doc_no in (Select max(doc_no) from TSPL_BREAK_DOWN_ENTRY)"
                Case NavigatorType.Next
                    qry += " where TSPL_BREAK_DOWN_ENTRY.doc_no in (Select min(doc_no) from TSPL_BREAK_DOWN_ENTRY where doc_no>'" + strCode + "')"
                Case NavigatorType.Previous
                    qry += " where TSPL_BREAK_DOWN_ENTRY.doc_no in (Select max(doc_no) from TSPL_BREAK_DOWN_ENTRY where doc_no<'" + strCode + "')"
            End Select
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                obj.Doc_no = clsCommon.myCstr(dt.Rows(0)("doc_no"))
                obj.Doc_Date = clsCommon.myCDate(dt.Rows(0)("doc_date"))
                obj.Description = clsCommon.myCstr(dt.Rows(0)("description"))
                obj.LOCATION_CODE = clsCommon.myCstr(dt.Rows(0)("Location_Code"))
                obj.Break_Down_Code = clsCommon.myCstr(dt.Rows(0)("Break_Down_Code"))
                obj.start_time = clsCommon.GetPrintDate(clsCommon.myCDate(dt.Rows(0)("start_time")), "dd/MM/yyyy hh:mm tt")
                obj.end_time = clsCommon.GetPrintDate(clsCommon.myCDate(dt.Rows(0)("end_time")), "dd/MM/yyyy hh:mm tt")
            End If

            Return obj
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function DeleteData(ByVal strcode As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim qry As String = "delete from TSPL_BREAK_DOWN_ENTRY where doc_no='" + strcode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function
End Class


