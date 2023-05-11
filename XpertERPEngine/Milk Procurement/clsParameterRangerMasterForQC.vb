Imports common
Imports System.Data.SqlClient
Public Class clsParameterRangeMasterForQC
#Region "Variables"
    Public code As String = Nothing
    Public desc As String = Nothing
    Public Lrange As Decimal = Nothing
    Public Urange As Decimal = Nothing
    Public Eff_date As Date = Nothing
    Public status As String = Nothing
    Public value1 As String = Nothing
    Public value2 As String = Nothing
    Public Qc_Status As String = Nothing
    Public created_by As String = String.Empty
    Public created_date As String = String.Empty
    Public modified_by As String = String.Empty
    Public modified_date As String = String.Empty
    Public comp_code As String = String.Empty
    Public QC_Param_Code As String = Nothing
    Public Trans_Id As String = Nothing
    Public ShowinDigitalAnalyzer As String = Nothing
    Public TextinDigitalAnalyzer As String = Nothing
    Public Analyzer_Index As String = Nothing
    Public Deduction_Per As Decimal = Nothing
    Public Lrange_Prev As Decimal = Nothing
    Public Urange_Prev As Decimal = Nothing
    Public Qc_Status_prev As String = Nothing
    Public is_NewEntry As Boolean = True
    Public Deduction_lower_range As Decimal = Nothing
    Public Deduction_upper_range As Decimal = Nothing
    Public Deduction_Ratio As Decimal = Nothing
    Public Deduction_lower_range2 As Decimal = Nothing
    Public Deduction_upper_range2 As Decimal = Nothing
    Public Deduction_Ratio2 As Decimal = Nothing
    Public Deduction_lower_range3 As Decimal = Nothing
    Public Deduction_upper_range3 As Decimal = Nothing
    Public Deduction_Ratio3 As Decimal = Nothing
#End Region

    Public Shared Function SaveData(ByVal arr As List(Of clsParameterRangeMasterForQC), ByVal Trans_Id As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            SaveData(arr, Trans_Id, trans)

            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function SaveData(ByVal arr As List(Of clsParameterRangeMasterForQC), ByVal Trans_Id As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim SetItemWiseQualityCheckInGeneralPurchase As Boolean = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ItemWiseQualityCheckInGeneralPurchase, clsFixedParameterCode.ItemWiseQualityCheckInGeneralPurchase, trans)) = 1)
            Dim isSaved As Boolean = True
            ' Dim Chk As Integer = 0
            'Dim qrydel As String = Nothing
            If arr IsNot Nothing AndAlso arr.Count > 0 Then
                'Dim qrydel As String = "delete from tspl_parameter_range_master_QC where trans_id='" + Trans_Id + "' "
                'isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qrydel, trans)

                For Each obj As clsParameterRangeMasterForQC In arr
                    Dim coll As New Hashtable()
                    'If Import Then
                    '    Chk = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select count(1) from tspl_parameter_range_master_QC where lower_range='" + clsCommon.myCstr(obj.Lrange) + "' and upper_range='" + clsCommon.myCstr(obj.Urange) + "' and Qc_Status='" + obj.Qc_Status + "' and QC_Param_Code ='" + obj.QC_Param_Code + "'", trans))
                    '    If Chk > 0 Then
                    '        qrydel = "delete from tspl_parameter_range_master_QC where lower_range='" + clsCommon.myCstr(obj.Lrange) + "' and upper_range='" + clsCommon.myCstr(obj.Urange) + "' and Qc_Status='" + obj.Qc_Status + "' and QC_Param_Code ='" + obj.QC_Param_Code + "'"
                    '        isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qrydel, trans)
                    '    End If
                    'End If
                    clsCommon.AddColumnsForChange(coll, "comp_code", objCommonVar.CurrentCompanyCode)
                    clsCommon.AddColumnsForChange(coll, "code", obj.code, True)
                    clsCommon.AddColumnsForChange(coll, "QC_Param_Code", obj.QC_Param_Code, True)
                    clsCommon.AddColumnsForChange(coll, "Trans_Id", obj.Trans_Id)
                    clsCommon.AddColumnsForChange(coll, "lower_range", obj.Lrange)
                    clsCommon.AddColumnsForChange(coll, "upper_range", obj.Urange)
                    clsCommon.AddColumnsForChange(coll, "status", obj.status)
                    clsCommon.AddColumnsForChange(coll, "value1", obj.value1)
                    clsCommon.AddColumnsForChange(coll, "value2", obj.value2)
                    clsCommon.AddColumnsForChange(coll, "Qc_Status", obj.Qc_Status)
                    clsCommon.AddColumnsForChange(coll, "Show_in_Analyzer", obj.ShowinDigitalAnalyzer)
                    clsCommon.AddColumnsForChange(coll, "Text_In_Analyzer", obj.TextinDigitalAnalyzer)
                    clsCommon.AddColumnsForChange(coll, "Analyzer_Index", obj.Analyzer_Index)
                    clsCommon.AddColumnsForChange(coll, "Deduction_Per", obj.Deduction_Per)
                    clsCommon.AddColumnsForChange(coll, "Deduction_lower_range", obj.Deduction_lower_range)
                    clsCommon.AddColumnsForChange(coll, "Deduction_upper_range", obj.Deduction_upper_range)
                    clsCommon.AddColumnsForChange(coll, "Deduction_Ratio", obj.Deduction_Ratio)
                    clsCommon.AddColumnsForChange(coll, "Deduction_lower_range2", obj.Deduction_lower_range2)
                    clsCommon.AddColumnsForChange(coll, "Deduction_upper_range2", obj.Deduction_upper_range2)
                    clsCommon.AddColumnsForChange(coll, "Deduction_Ratio2", obj.Deduction_Ratio2)
                    clsCommon.AddColumnsForChange(coll, "Deduction_lower_range3", obj.Deduction_lower_range3)
                    clsCommon.AddColumnsForChange(coll, "Deduction_upper_range3", obj.Deduction_upper_range3)
                    clsCommon.AddColumnsForChange(coll, "Deduction_Ratio3", obj.Deduction_Ratio3)
                    If IsDate(obj.Eff_date) Then
                        clsCommon.AddColumnsForChange(coll, "effective_date", clsCommon.GetPrintDate(obj.Eff_date, "dd/MMM/yyyy"))
                    End If


                    If SetItemWiseQualityCheckInGeneralPurchase = True Then
                        Dim qry As String = "select count(*) from tspl_parameter_range_master_qc where QC_Param_Code='" + obj.QC_Param_Code + "'"
                        Dim check1 = clsDBFuncationality.getSingleValue(qry, trans)

                        If check1 > 0 Then
                            obj.is_NewEntry = False
                        Else
                            obj.is_NewEntry = True
                        End If
                        If obj.is_NewEntry Then
                            clsCommon.AddColumnsForChange(coll, "modified_by", objCommonVar.CurrentUserCode)
                            clsCommon.AddColumnsForChange(coll, "modified_date", clsCommon.myCstr(clsCommon.GETSERVERDATE(trans).ToString("dd/MM/yyyy")))

                            clsCommon.AddColumnsForChange(coll, "created_by", objCommonVar.CurrentUserCode)
                            clsCommon.AddColumnsForChange(coll, "created_date", clsCommon.myCstr(clsCommon.GETSERVERDATE(trans).ToString("dd/MM/yyyy")))
                            isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PARAMETER_RANGE_MASTER_QC", OMInsertOrUpdate.Insert, "", trans)
                        Else
                            clsCommon.AddColumnsForChange(coll, "modified_by", objCommonVar.CurrentUserCode)
                            clsCommon.AddColumnsForChange(coll, "modified_date", clsCommon.myCstr(clsCommon.GETSERVERDATE(trans).ToString("dd/MM/yyyy")))

                            Dim strwhrcls As String = Nothing
                            strwhrcls = " QC_Param_Code ='" + obj.QC_Param_Code + "'"
                            isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PARAMETER_RANGE_MASTER_QC", OMInsertOrUpdate.Update, strwhrcls, trans)
                        End If

                        ' isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PARAMETER_RANGE_MASTER_QC", OMInsertOrUpdate.Insert, "", trans)

                        '=history table============================================
                        Dim qry1 As String = Nothing
                        qry = " select count(*) from TSPL_PARAMETER_RANGE_MASTER_QC_HISTORY where trans_id='" + obj.Trans_Id + "' "
                        qry += " and QC_Param_Code='" + obj.QC_Param_Code + "'"
                        If clsDBFuncationality.getSingleValue(qry, trans) = 0 Then
                            If Not coll.ContainsKey("created_by") Then
                                clsCommon.AddColumnsForChange(coll, "created_by", objCommonVar.CurrentUserCode)
                            End If
                            If Not coll.ContainsKey("created_date") Then
                                clsCommon.AddColumnsForChange(coll, "created_date", clsCommon.myCstr(clsCommon.GETSERVERDATE(trans).ToString("dd/MM/yyyy")))
                            End If
                            isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PARAMETER_RANGE_MASTER_QC_HISTORY", OMInsertOrUpdate.Insert, "", trans)
                        End If
                    Else

                        clsCommon.AddColumnsForChange(coll, "modified_by", objCommonVar.CurrentUserCode)
                        clsCommon.AddColumnsForChange(coll, "modified_date", clsCommon.myCstr(clsCommon.GETSERVERDATE(trans).ToString("dd/MM/yyyy")))

                        clsCommon.AddColumnsForChange(coll, "created_by", objCommonVar.CurrentUserCode)
                        clsCommon.AddColumnsForChange(coll, "created_date", clsCommon.myCstr(clsCommon.GETSERVERDATE(trans).ToString("dd/MM/yyyy")))

                        If obj.is_NewEntry Then
                            isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PARAMETER_RANGE_MASTER_QC", OMInsertOrUpdate.Insert, "", trans)
                        Else
                            Dim strwhrcls As String = Nothing
                            strwhrcls = " lower_range='" + clsCommon.myCstr(obj.Lrange_Prev) + "' and upper_range='" + clsCommon.myCstr(obj.Urange_Prev) + "' and Qc_Status='" + obj.Qc_Status_prev + "' "
                            If clsCommon.myLen(obj.QC_Param_Code) > 0 Then
                                strwhrcls += " and QC_Param_Code ='" + obj.QC_Param_Code + "'"
                            Else
                                strwhrcls += " and Code ='" + obj.code + "'"
                            End If
                            isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PARAMETER_RANGE_MASTER_QC", OMInsertOrUpdate.Update, strwhrcls, trans)
                        End If

                        ' isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PARAMETER_RANGE_MASTER_QC", OMInsertOrUpdate.Insert, "", trans)

                        '=history table============================================
                        Dim qry As String = Nothing
                        qry = " select count(*) from TSPL_PARAMETER_RANGE_MASTER_QC_HISTORY where trans_id='" + obj.Trans_Id + "' "
                        If clsCommon.myLen(obj.QC_Param_Code) > 0 Then
                            qry += " and QC_Param_Code='" + obj.QC_Param_Code + "'"
                        Else
                            qry += " and Code ='" + obj.code + "'"
                        End If

                        qry += " and code='" & clsCommon.myCstr(obj.code) & "' and lower_range='" & obj.Lrange & "' and upper_range='" & obj.Urange & "' and effective_date='" & clsCommon.GetPrintDate(obj.Eff_date, "dd/MMM/yyyy") & "' and status='" + obj.status + "' and value1='" + obj.value1 + "' and value2='" + obj.value2 + "' and Qc_status='" + obj.Qc_Status + "' and Show_in_Analyzer='" + obj.ShowinDigitalAnalyzer + "'  and Text_In_Analyzer='" + obj.TextinDigitalAnalyzer + "'  and Analyzer_Index='" + obj.Analyzer_Index + "'"
                        If clsDBFuncationality.getSingleValue(qry, trans) = 0 Then
                            If Not coll.ContainsKey("created_by") Then
                                clsCommon.AddColumnsForChange(coll, "created_by", objCommonVar.CurrentUserCode)
                            End If
                            If Not coll.ContainsKey("created_date") Then
                                clsCommon.AddColumnsForChange(coll, "created_date", clsCommon.myCstr(clsCommon.GETSERVERDATE(trans).ToString("dd/MM/yyyy")))
                            End If
                            isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PARAMETER_RANGE_MASTER_QC_HISTORY", OMInsertOrUpdate.Insert, "", trans)
                        End If
                    End If
                Next
            End If

            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function DeleteData(ByVal Trans_Id As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            DeleteData(Trans_Id, trans)

            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function DeleteData(ByVal Trans_Id As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim qry As String = "delete from tspl_parameter_range_master_qc where trans_id='" + Trans_Id + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
End Class

