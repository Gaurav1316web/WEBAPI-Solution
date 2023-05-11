'---BM00000003351--------Monika----13/08/2014--------------
Imports common
Imports System.Data.SqlClient

Public Class clsProcessProductionLogSheet
#Region "Variables"
    Public Description As String = Nothing
    Public Is_Mannual As Integer = Nothing
    Public columns_name As String = Nothing
    Public Doc_no As String = Nothing
    Public Doc_Date As Date = Nothing
    Public Cate_Code As String = Nothing
    Public Cate_Name As String = Nothing
    Public Stage_code As String = Nothing
    Public stage_name As String = Nothing
    Public start_time As DateTime = Nothing
    Public end_time As DateTime = Nothing
    Public diff_time As Integer = Nothing
    Public Combo_Value As String = Nothing
    Public sequnce As Integer = Nothing
    Public Arr As List(Of clsProcessProductionLogSheetDetail) = Nothing
#End Region

    Public Shared Function GetFinder(ByVal whrCls As String, ByVal CurrCode As String, ByVal isButtonClicked As Boolean) As String
        Dim qry As String = "select TSPL_PP_LOG_SHEET_HEAD.doc_no as Code,TSPL_PP_LOG_SHEET_HEAD.description as [Log Description],TSPL_PP_LOG_SHEET_HEAD.doc_date as [Date],TSPL_PP_LOG_SHEET_HEAD.stage_code as [Stage Code],tspl_stage_master.description as [Stage],(case when Is_Mannual=0 then 'Timewise Log Sheet' else 'Mannual Log Sheet' end) as Status,TSPL_PP_LOG_SHEET_HEAD.start_time as [Start Time],TSPL_PP_LOG_SHEET_HEAD.end_time as [End Time],(TSPL_PP_LOG_SHEET_HEAD.diff_time+' '+TSPL_PP_LOG_SHEET_HEAD.diff_time_in) as Difference from TSPL_PP_LOG_SHEET_HEAD left outer join tspl_stage_master on tspl_stage_master.stage_code=TSPL_PP_LOG_SHEET_HEAD.stage_code  "
        Dim str As String = ""
        '',TSPL_PP_LOG_SHEET_HEAD.structure_code as [Production Category],tspl_structure_master.structure_descq as [Description],TSPL_PP_LOG_SHEET_HEAD.Sequence_No as [Sequence No]
        ''left outer join tspl_structure_master on tspl_structure_master.structure_code=TSPL_PP_LOG_SHEET_HEAD.structure_code

        str = clsCommon.ShowSelectForm("LOGFND", qry, "Code", whrCls, CurrCode, "Code", isButtonClicked)

        Return str
    End Function

    Public Shared Function SaveData(ByVal strCode As String, ByVal obj As clsProcessProductionLogSheet, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim isSaved As Boolean = True

            If isNewEntry Then
                obj.Doc_no = clsCommon.myCstr(clsERPFuncationality.GetNextCode(trans, clsCommon.GetPrintDate(obj.Doc_Date, "dd/MMM/yyyy"), clsDocType.PPLOGSHEET, "", ""))
            End If

            Dim coll As New Hashtable()

            clsCommon.AddColumnsForChange(coll, "comp_code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Doc_No", obj.Doc_no)
            clsCommon.AddColumnsForChange(coll, "Doc_Date", clsCommon.GetPrintDate(obj.Doc_Date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Structure_Code", "S") ' obj.Cate_Code
            clsCommon.AddColumnsForChange(coll, "Description", obj.Description)
            clsCommon.AddColumnsForChange(coll, "Stage_Code", obj.Stage_code)
            clsCommon.AddColumnsForChange(coll, "Start_Time", clsCommon.GetPrintDate(obj.start_time, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "End_Time", clsCommon.GetPrintDate(obj.end_time, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Diff_Time", obj.diff_time)
            clsCommon.AddColumnsForChange(coll, "Diff_Time_In", obj.Combo_Value)
            clsCommon.AddColumnsForChange(coll, "Sequence_No", "1") 'obj.sequnce
            clsCommon.AddColumnsForChange(coll, "Column_Names", obj.columns_name)
            clsCommon.AddColumnsForChange(coll, "Is_Mannual", obj.Is_Mannual)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))

            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))

                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PP_LOG_SHEET_HEAD", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PP_LOG_SHEET_HEAD", OMInsertOrUpdate.Update, " doc_no='" + obj.Doc_no + "'", trans)
            End If

            isSaved = isSaved AndAlso clsProcessProductionLogSheetDetail.SaveData(obj.Doc_no, obj.Arr, trans)

            trans.Commit()
            Return isSaved
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function GetParam_Name(ByVal Param_Code As String) As String
        Dim str As String = ""
        str = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select description from TSPL_QC_LOG_SHEET_MASTER where code='" + Param_Code + "'"))

        Return str
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsProcessProductionLogSheet
        Try
            Dim obj As New clsProcessProductionLogSheet()

            Dim qry As String = "select TSPL_PP_LOG_SHEET_HEAD.description as Log_Desc ,1 as Sequence_No,'' as structure_code,'' as structure_descq,TSPL_PP_LOG_SHEET_HEAD.Is_Mannual,TSPL_PP_LOG_SHEET_HEAD.doc_no,TSPL_PP_LOG_SHEET_HEAD.doc_date,TSPL_PP_LOG_SHEET_HEAD.stage_code,tspl_stage_master.description,TSPL_PP_LOG_SHEET_HEAD.start_time,TSPL_PP_LOG_SHEET_HEAD.end_time,TSPL_PP_LOG_SHEET_HEAD.diff_time,TSPL_PP_LOG_SHEET_HEAD.diff_time_in,Column_Names from TSPL_PP_LOG_SHEET_HEAD left outer join tspl_stage_master on tspl_stage_master.stage_code=TSPL_PP_LOG_SHEET_HEAD.stage_code " 'left outer join tspl_structure_master on tspl_structure_master.structure_code=TSPL_PP_LOG_SHEET_HEAD.structure_code 
            ',TSPL_PP_LOG_SHEET_HEAD.Sequence_No,TSPL_PP_LOG_SHEET_HEAD.structure_code,tspl_structure_master.structure_descq

            Select Case NavType
                Case NavigatorType.Current
                    qry += " where TSPL_PP_LOG_SHEET_HEAD.doc_no='" + strCode + "'"
                Case NavigatorType.First
                    qry += " where TSPL_PP_LOG_SHEET_HEAD.doc_no in (Select min(doc_no) from TSPL_PP_LOG_SHEET_HEAD)"
                Case NavigatorType.Last
                    qry += " where TSPL_PP_LOG_SHEET_HEAD.doc_no in (Select max(doc_no) from TSPL_PP_LOG_SHEET_HEAD)"
                Case NavigatorType.Next
                    qry += " where TSPL_PP_LOG_SHEET_HEAD.doc_no in (Select min(doc_no) from TSPL_PP_LOG_SHEET_HEAD where doc_no>'" + strCode + "')"
                Case NavigatorType.Previous
                    qry += " where TSPL_PP_LOG_SHEET_HEAD.doc_no in (Select max(doc_no) from TSPL_PP_LOG_SHEET_HEAD where doc_no<'" + strCode + "')"
            End Select
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                obj.Is_Mannual = CInt(dt.Rows(0)("Is_Mannual"))
                obj.Doc_no = clsCommon.myCstr(dt.Rows(0)("doc_no"))
                obj.Doc_Date = clsCommon.myCDate(dt.Rows(0)("doc_date"))
                obj.Description = clsCommon.myCstr(dt.Rows(0)("log_desc"))
                obj.Cate_Code = clsCommon.myCstr(dt.Rows(0)("structure_code"))
                obj.Cate_Name = clsCommon.myCstr(dt.Rows(0)("structure_descq"))
                obj.Stage_code = clsCommon.myCstr(dt.Rows(0)("stage_code"))
                obj.stage_name = clsCommon.myCstr(dt.Rows(0)("description"))
                obj.sequnce = CInt(dt.Rows(0)("Sequence_No"))
                obj.columns_name = clsCommon.myCstr(dt.Rows(0)("Column_Names"))
                obj.start_time = clsCommon.GetPrintDate(clsCommon.myCDate(dt.Rows(0)("start_time")), "dd/MM/yyyy hh:mm tt")
                obj.end_time = clsCommon.GetPrintDate(clsCommon.myCDate(dt.Rows(0)("end_time")), "dd/MM/yyyy hh:mm tt")
                obj.diff_time = CInt(dt.Rows(0)("diff_time"))
                obj.Combo_Value = clsCommon.myCstr(dt.Rows(0)("diff_time_in"))

                obj.Arr = New List(Of clsProcessProductionLogSheetDetail)

                qry = "select distinct sno,parameter_code from TSPL_PP_LOG_SHEET_DETAIL where doc_no='" + obj.Doc_no + "' order by sno"
                Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qry)

                If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
                    For Each dr As DataRow In dt1.Rows
                        Dim objtr As New clsProcessProductionLogSheetDetail()

                        objtr.Sno = CInt(dr("sno"))
                        'objtr.xtime = clsCommon.myCstr(dr("time_value"))
                        objtr.param_code = clsCommon.myCstr(dr("parameter_code"))
                        'objtr.param_value = clsCommon.myCstr(dr("parameter_value"))
                        objtr.param_header = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select description from tspl_parameter_master where code='" + objtr.param_code + "'"))

                        obj.Arr.Add(objtr)
                    Next
                End If
            End If

            Return obj
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function DeleteData(ByVal strcode As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim qry As String = "delete from TSPL_PP_LOG_SHEET_DETAIL where doc_no='" + strcode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_PP_LOG_SHEET_HEAD where doc_no='" + strcode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function
End Class


Public Class clsProcessProductionLogSheetDetail
#Region "Variables"
    Public Sno As Integer = Nothing
    Public xtime As String = Nothing
    Public param_code As String = Nothing
    Public param_value As String = Nothing
    Public param_header As String = Nothing
#End Region

    Public Shared Function SaveData(ByVal strCode As String, ByVal arr As List(Of clsProcessProductionLogSheetDetail), ByVal trans As SqlTransaction) As Boolean
        Try
            Dim isSaved As Boolean = True
            Dim coll As New Hashtable()

            Dim qry As String = "delete from TSPL_PP_LOG_SHEET_DETAIL where doc_no='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            If arr IsNot Nothing AndAlso arr.Count > 0 Then
                For Each objtr As clsProcessProductionLogSheetDetail In arr
                    coll = New Hashtable()

                    clsCommon.AddColumnsForChange(coll, "comp_code", objCommonVar.CurrentCompanyCode)
                    clsCommon.AddColumnsForChange(coll, "Doc_No", strCode)
                    clsCommon.AddColumnsForChange(coll, "SNO", objtr.Sno)
                    clsCommon.AddColumnsForChange(coll, "Time_Value", objtr.xtime)
                    clsCommon.AddColumnsForChange(coll, "Parameter_Code", objtr.param_code)
                    clsCommon.AddColumnsForChange(coll, "Parameter_Value", objtr.param_value)

                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PP_LOG_SHEET_DETAIL", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If

            Return isSaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
End Class
