Imports common
Imports System.Data.SqlClient
Imports CrystalDecisions.CrystalReports.Engine
Public Class clsDocPrefix

#Region "Variables"
    Public Doc_Type As String = Nothing
    Public Doc_Trans_Type As String = Nothing
    Public Location_Code As String = Nothing
    Public RouteNo As String = Nothing
    Public Doc_Prfeix As String = Nothing
    Public Dont_Add_Prefix As Boolean = False
    Public Fin_Year As Integer = 0
    Public Next_Number As Integer = 0
    Public Separator As String = Nothing
    Public Curr_Month As Integer = 0
    Public Is_Change_Monthly As Boolean = False
    Public Is_Change_Daily As Boolean = False
    Public Curr_Date As Date? = Nothing
    Public dontDisplayYearInSeries As Boolean = False
    Public Short_Fiscal_Year As Boolean = False
    Public Year_Separator As String = Nothing
    Public MinSizeofSeries As Integer = 5
    Public isNewEntry As Boolean = False
    Public OldDocTransType As String = Nothing
    Public OldLocationCode As String = Nothing

    Public Shared Function SaveData(ByVal strDocType As String, ByVal intFiscalYear As Integer, ByVal Arr As List(Of clsDocPrefix)) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            SaveData(strDocType, intFiscalYear, Arr, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function SaveData(ByVal strDocType As String, ByVal intFiscalYear As Integer, ByVal Arr As List(Of clsDocPrefix), ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Try
            'Dim qry As String = "Delete from TSPL_DOCPREFIX_MASTER where Doc_Type='" + strDocType + "' and Fin_Year='" + clsCommon.myCstr(intFiscalYear) + "'"
            'isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            Dim CurrDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "yyyy/MM/dd")
            If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
                For Each obj As clsDocPrefix In Arr
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Doc_Type", strDocType)
                    clsCommon.AddColumnsForChange(coll, "Doc_Trans_Type", obj.Doc_Trans_Type)
                    clsCommon.AddColumnsForChange(coll, "Location_Code", obj.Location_Code)
                    clsCommon.AddColumnsForChange(coll, "RouteNo", obj.RouteNo)
                    clsCommon.AddColumnsForChange(coll, "Doc_Prfeix", obj.Doc_Prfeix)
                    clsCommon.AddColumnsForChange(coll, "Dont_Add_Prefix", IIf(obj.Dont_Add_Prefix, 1, 0))
                    clsCommon.AddColumnsForChange(coll, "Fin_Year", intFiscalYear)
                    clsCommon.AddColumnsForChange(coll, "Next_Number", obj.Next_Number)
                    clsCommon.AddColumnsForChange(coll, "Separator", obj.Separator)
                    clsCommon.AddColumnsForChange(coll, "Year_Separator", obj.Year_Separator)
                    clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
                    clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "Created_Date", CurrDate)
                    clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "Modify_Date", CurrDate)
                    clsCommon.AddColumnsForChange(coll, "Is_Change_Monthly", IIf(obj.Is_Change_Monthly, 1, 0))
                    clsCommon.AddColumnsForChange(coll, "Curr_Month", obj.Curr_Month)
                    clsCommon.AddColumnsForChange(coll, "Is_Change_Daily", IIf(obj.Is_Change_Daily, 1, 0))
                    If obj.Curr_Date IsNot Nothing Then
                        clsCommon.AddColumnsForChange(coll, "Curr_Date", clsCommon.GetPrintDate(obj.Curr_Date, "dd/MMM/yyyy"))
                    End If
                    clsCommon.AddColumnsForChange(coll, "dontDisplayYearInSeries", obj.dontDisplayYearInSeries)
                    clsCommon.AddColumnsForChange(coll, "Short_Fiscal_Year", obj.Short_Fiscal_Year)
                    clsCommon.AddColumnsForChange(coll, "MinSizeofSeries", obj.MinSizeofSeries)
                    If obj.isNewEntry Then
                        isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_DOCPREFIX_MASTER", OMInsertOrUpdate.Insert, "", trans)
                    Else
                        Dim strWhrclas As String = "Doc_Type= '" + strDocType + "' and Doc_Trans_Type=ISNULL('" + obj.OldDocTransType + "','')  and Location_Code=isnull('" + obj.OldLocationCode + "','')"
                        If clsCommon.myCdbl(intFiscalYear) > 0 Then
                            strWhrclas += " and Fin_Year='" + clsCommon.myCstr(intFiscalYear) + "'"
                        End If
                        '=====================update by preeti gupta against ticket no [BM00000008979]
                        If obj.Is_Change_Monthly Then
                            strWhrclas += " and Curr_Month='" + clsCommon.myCstr(obj.Curr_Month) + "'"

                        End If
                        If obj.Is_Change_Daily Then
                            strWhrclas += "and Curr_Date='" + clsCommon.GetPrintDate(obj.Curr_Date, "dd/MMM/yyyy") + "'"
                        End If
                        '=============================END==================================================
                        isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_DOCPREFIX_MASTER", OMInsertOrUpdate.Update, strWhrclas, trans)
                    End If
                Next
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function
#End Region



End Class

Public Class clsScrenScheduling
#Region "Variables"

    Public Shared Function FillDataValues(ByVal modulename As String, ByVal doctype As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim ScrnCode As String = ""

            Dim qry As String = "Select TSPL_PROGRAM_MASTER.program_code from TSPL_PROGRAM_MASTER Where TSPL_PROGRAM_MASTER.parent_code in (select program_code from TSPL_PROGRAM_MASTER where TSPL_PROGRAM_MASTER.Parent_Code='" + modulename + "' AND TSPL_PROGRAM_MASTER.Program_Code like '%" + doctype + "')"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

            For Each dr As DataRow In dt.Rows
                Try
                    ScrnCode = clsCommon.myCstr(dr("program_code"))
                Catch ex1 As Exception
                End Try

                If clsCommon.CompairString(ScrnCode, "BNK-GURNTE") = CompairStringResult.Equal Then
                    qry = "insert into TSPL_NOTIFICATION_SETTING_CRITERIA"
                    qry += " select '" + ScrnCode + "','End Date','Screen' union all select '" + ScrnCode + "','Extended Date','Screen'"
                    qry += " except select * from TSPL_NOTIFICATION_SETTING_CRITERIA "
                    clsDBFuncationality.ExecuteNonQuery(qry, trans)

                    qry = "insert into TSPL_NOTIFICATION_SETTING_CRITERIA"
                    qry += " select '" + ScrnCode + "','End Date','Login' union all select '" + ScrnCode + "','Extended Date','Login'"
                    qry += " except select * from TSPL_NOTIFICATION_SETTING_CRITERIA "
                    clsDBFuncationality.ExecuteNonQuery(qry, trans)
                End If
            Next

            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Return False
        End Try
    End Function
#End Region
End Class
