Imports common
Imports System.Data.SqlClient

Public Class clsEngLogSheetDeatil
#Region "Variables"
    Public TR_Code As String = Nothing
    Public Doc_No As String = Nothing
    Public sno As Integer = Nothing
    Public parameter_code As String = Nothing
    Public parameter_Value As String = Nothing
#End Region


    Public Shared Function GetEngLogSheetDeatil(ByVal Doc_Code As String, ByVal trans As SqlTransaction) As List(Of clsEngLogSheetDeatil)
        Dim objPMList As New List(Of clsEngLogSheetDeatil)
        Dim qry As String = "select * from TSPL_ENG_LOG_SHEET_DETAIL " & _
                " where TSPL_ENG_LOG_SHEET_DETAIL.Doc_No='" + Doc_Code + "' order by sno"
        Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
            For Each dr As DataRow In dt1.Rows
                Dim objtr As New clsEngLogSheetDeatil()
                objtr.sno = CInt(dr("SNO"))
                objtr.Doc_No = clsCommon.myCdbl(dr("Doc_No"))
                objtr.parameter_code = clsCommon.myCstr(dr("Parameter_Code"))
                objtr.parameter_Value = clsCommon.myCstr(dr("Parameter_Value"))
                objPMList.Add(objtr)
            Next
        End If
        Return objPMList
    End Function


    Public Shared Function SaveData(ByVal Doc_No As String, ByVal arr As List(Of clsEngLogSheetDeatil), ByVal trans As SqlTransaction) As Boolean
        Try
            Dim isSaved As Boolean = True
            Dim qry As String = "delete from TSPL_ENG_LOG_SHEET_DETAIL where Doc_No='" + Doc_No + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Dim coll As New Hashtable()

            If arr IsNot Nothing AndAlso arr.Count > 0 Then
                For Each objtr As clsEngLogSheetDeatil In arr
                    coll = New Hashtable()
                    objtr.TR_Code = clsERPFuncationality.GetNextCode(trans, clsCommon.GETSERVERDATE(trans), clsDocType.Detail, clsDocTransactionType.Detail, "")
                    clsCommon.AddColumnsForChange(coll, "TR_Code", objtr.TR_Code)
                    clsCommon.AddColumnsForChange(coll, "Doc_No", Doc_No)
                    clsCommon.AddColumnsForChange(coll, "SNO", objtr.sno)
                    clsCommon.AddColumnsForChange(coll, "Parameter_Code", objtr.parameter_code)
                    clsCommon.AddColumnsForChange(coll, "Parameter_Value", objtr.parameter_Value)
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ENG_LOG_SHEET_DETAIL", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If

            Return isSaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

End Class

Public Class clsEngLogSheetMaster



#Region "Variables"
    Public Doc_No As String = Nothing
    Public Doc_Date As Date? = Nothing
    Public Section_Code As String = Nothing
    Public Consumption_Code As String = Nothing
    Public Location_Code As String = Nothing
    Public OperatedEarlierHrs As String = Nothing
    Public OperatedHrs As String = Nothing
    Public OperatedTotalHrs As String = Nothing
    Public OilChange As String = Nothing
    Public Repair As String = Nothing
    Public ArrPM As List(Of clsEngLogSheetDeatil) = Nothing

#End Region


    Public Shared Function SaveData(ByVal obj As clsEngLogSheetMaster, ByVal isNewEntry As Boolean) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            SaveData(obj, isNewEntry, trans)

            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function SaveData(ByVal obj As clsEngLogSheetMaster, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim isSaved As Boolean = True
            If isNewEntry Then

                If (IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AutoGeneratePrefix, clsFixedParameterCode.AutoGeneratePrefix, trans)) = "1", True, False)) Then
                    Dim ChkNewEntry As String = clsDBFuncationality.getSingleValue("Select count(*) from TSPL_ENG_LOG_SHEET_HEAD where Doc_No='" & obj.Doc_No & "'", trans)
                    If ChkNewEntry = 0 Then
                        obj.Doc_No = clsERPFuncationality.GetNextCode(trans, clsCommon.GETSERVERDATE(trans), clsDocType.LogSheetEng, "", obj.Location_Code)
                        If clsCommon.myLen(obj.Doc_No) <= 0 Then
                            Throw New Exception("Error in Code Generation")
                        End If
                    End If
                End If
            End If


            Dim coll As New Hashtable()

            clsCommon.AddColumnsForChange(coll, "Doc_No", obj.Doc_No)
            clsCommon.AddColumnsForChange(coll, "Doc_Date", clsCommon.GetPrintDate(obj.Doc_Date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Section_Code", obj.Section_Code)
            clsCommon.AddColumnsForChange(coll, "Consumption_Code", obj.Consumption_Code)
            clsCommon.AddColumnsForChange(coll, "OperatedEarlierHrs", obj.OperatedEarlierHrs)
            clsCommon.AddColumnsForChange(coll, "OperatedHrs", obj.OperatedHrs)
            clsCommon.AddColumnsForChange(coll, "OperatedTotalHrs", obj.OperatedTotalHrs)
            clsCommon.AddColumnsForChange(coll, "OilChange", obj.OilChange)
            clsCommon.AddColumnsForChange(coll, "Repair", obj.Repair)
            clsCommon.AddColumnsForChange(coll, "comp_code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Location_Code", obj.Location_Code)
            clsCommon.AddColumnsForChange(coll, "modified_by", clsCommon.myCstr(objCommonVar.CurrentUserCode))
            clsCommon.AddColumnsForChange(coll, "modified_date", clsCommon.myCstr(clsCommon.GETSERVERDATE(trans).ToString("dd/MM/yyyy")))

            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "created_by", clsCommon.myCstr(objCommonVar.CurrentUserCode))
                clsCommon.AddColumnsForChange(coll, "created_date", clsCommon.myCstr(clsCommon.GETSERVERDATE(trans).ToString("dd/MM/yyyy")))
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ENG_LOG_SHEET_HEAD", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ENG_LOG_SHEET_HEAD", OMInsertOrUpdate.Update, " Doc_No='" + obj.Doc_No + "'", trans)
            End If

            isSaved = isSaved AndAlso clsEngLogSheetDeatil.SaveData(obj.Doc_No, obj.ArrPM, trans)

            Return isSaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, Optional ByVal trans As SqlTransaction = Nothing) As clsEngLogSheetMaster
        Dim obj As New clsEngLogSheetMaster()
        Try
            Dim qry As String = "select TSPL_ENG_LOG_SHEET_HEAD.* from TSPL_ENG_LOG_SHEET_HEAD "

            Select Case NavType
                Case NavigatorType.Current
                    qry += " where TSPL_ENG_LOG_SHEET_HEAD.Doc_No='" + strCode + "'"
                Case NavigatorType.First
                    qry += " where TSPL_ENG_LOG_SHEET_HEAD.Doc_No in (select min(Doc_No) from TSPL_ENG_LOG_SHEET_HEAD) "
                Case NavigatorType.Last
                    qry += " where TSPL_ENG_LOG_SHEET_HEAD.Doc_No in (select max(Doc_No) from TSPL_ENG_LOG_SHEET_HEAD ) "
                Case NavigatorType.Next
                    qry += " where TSPL_ENG_LOG_SHEET_HEAD.Doc_No in (select min(Doc_No) from TSPL_ENG_LOG_SHEET_HEAD where Doc_No>'" + strCode + "' ) "
                Case NavigatorType.Previous
                    qry += " where TSPL_ENG_LOG_SHEET_HEAD.Doc_No in (select max(Doc_No) from TSPL_ENG_LOG_SHEET_HEAD where Doc_No<'" + strCode + "' ) "
            End Select

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                obj.Doc_No = clsCommon.myCstr(dt.Rows(0)("Doc_No"))
                obj.Doc_Date = clsCommon.myCstr(dt.Rows(0)("Doc_Date"))
                obj.Location_Code = clsCommon.myCstr(dt.Rows(0)("Location_Code"))
                obj.Section_Code = clsCommon.myCstr(dt.Rows(0)("Section_Code"))
                obj.Consumption_Code = clsCommon.myCstr(dt.Rows(0)("Consumption_Code"))
                obj.OperatedEarlierHrs = clsCommon.myCstr(dt.Rows(0)("OperatedEarlierHrs"))
                obj.OperatedHrs = clsCommon.myCstr(dt.Rows(0)("OperatedHrs"))
                obj.OperatedTotalHrs = clsCommon.myCstr(dt.Rows(0)("OperatedTotalHrs"))
                obj.OilChange = clsCommon.myCstr(dt.Rows(0)("OilChange"))
                obj.Repair = clsCommon.myCstr(dt.Rows(0)("Repair"))
                obj.ArrPM = clsEngLogSheetDeatil.GetEngLogSheetDeatil(obj.Doc_No, trans)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
        Return obj
    End Function

    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            DeleteData(strCode, trans)

            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function DeleteData(ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim isSaved As Boolean = True
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery("delete from TSPL_ENG_LOG_SHEET_DETAIL where Doc_No='" + strCode + "'", trans)
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery("delete from TSPL_ENG_LOG_SHEET_HEAD where Doc_No='" + strCode + "'", trans)

            Return isSaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    '>>>>>>>>>>>>>>>>>>>>>>>
    'Public Shared Function SaveIMPORTData(ByVal strCode As String, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction, ByVal obj As clsPPLogSheetMaster) As Boolean
    '    Try
    '        Dim isSaved As Boolean = True

    '        If isNewEntry Then
    '            obj.code = clsERPFuncationality.GetNextCode(trans, clsCommon.GETSERVERDATE(trans), clsDocType.QCLOGSHEETMST, "", "")
    '        End If

    '        strCode = obj.code

    '        Dim coll As New Hashtable()

    '        clsCommon.AddColumnsForChange(coll, "comp_code", objCommonVar.CurrentCompanyCode)
    '        clsCommon.AddColumnsForChange(coll, "Code", strCode)
    '        clsCommon.AddColumnsForChange(coll, "trans_id", obj.Trans_Id)
    '        clsCommon.AddColumnsForChange(coll, "description", obj.desc)
    '        clsCommon.AddColumnsForChange(coll, "type", obj.type)
    '        clsCommon.AddColumnsForChange(coll, "Nature", obj.nature)
    '        clsCommon.AddColumnsForChange(coll, "IsMandatory", obj.IsMandatory)
    '        clsCommon.AddColumnsForChange(coll, "IsRequired_InParameter_Master", obj.IsReq_Parameter_Master)
    '        clsCommon.AddColumnsForChange(coll, "Pick_Batch_No", obj.Pick_BO)
    '        clsCommon.AddColumnsForChange(coll, "Department_Code", obj.Department_COde)
    '        clsCommon.AddColumnsForChange(coll, "modified_by", clsCommon.myCstr(objCommonVar.CurrentUserCode))
    '        clsCommon.AddColumnsForChange(coll, "modified_date", clsCommon.myCstr(clsCommon.GETSERVERDATE(trans).ToString("dd/MM/yyyy")))

    '        If isNewEntry Then
    '            clsCommon.AddColumnsForChange(coll, "created_by", clsCommon.myCstr(objCommonVar.CurrentUserCode))
    '            clsCommon.AddColumnsForChange(coll, "created_date", clsCommon.myCstr(clsCommon.GETSERVERDATE(trans).ToString("dd/MM/yyyy")))
    '            isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_QC_LOG_SHEET_MASTER", OMInsertOrUpdate.Insert, "", trans)
    '        Else
    '            isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_QC_LOG_SHEET_MASTER", OMInsertOrUpdate.Update, " Code='" + obj.code + "' and trans_id='" + obj.Trans_Id + "'", trans)
    '        End If

    '        If obj.Import = True Then
    '            If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
    '                For Each objtr As clsPPLogSheetUserMaster In obj.Arr
    '                    isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery("delete from TSPL_QC_LOG_SHEET_USER_MASTER where code='" + obj.code + "' and emp_code='" + objtr.UserCode + "'", trans)

    '                    coll = New Hashtable()

    '                    clsCommon.AddColumnsForChange(coll, "code", obj.code)
    '                    clsCommon.AddColumnsForChange(coll, "Emp_Code", objtr.UserCode)

    '                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_QC_LOG_SHEET_USER_MASTER", OMInsertOrUpdate.Insert, "", trans)
    '                Next
    '            End If
    '        End If

    '        Return True
    '    Catch ex As Exception
    '        Throw New Exception(ex.Message)
    '    End Try
    'End Function

End Class
