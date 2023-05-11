Imports common
Imports System.Data.SqlClient

Public Class clsPPLogSheetMaster
#Region "Variables"
    Public AliasName As String = Nothing
    Public nature As String = Nothing
    Public code As String = Nothing
    Public desc As String = Nothing
    Public type As String = Nothing
    Public IsMandatory As Integer = 0
    Public Pick_BO As Integer = 0
    Public IsReq_Parameter_Master As Integer = 0
    Public Department_COde As String = Nothing
    Public Department_Name As String = Nothing
    Public Trans_Id As String = Nothing
    Public Import As Boolean = False
    Public Arr As List(Of clsPPLogSheetUserMaster) = Nothing
#End Region

    Public Shared Function GetFinder(ByVal whrCls As String, ByVal CurrCode As String, ByVal isButtonClicked As Boolean) As String
        Dim strcode As String = ""
        Dim qry As String = "Select Code,Description,Type,Nature,(Case when IsMandatory=0 then 'Not Mandatory' else 'Mandatory' end) as IsMandatory,department_code as Department,AliasName as [Alias Name],created_by as [Created By],created_date as [Created Date],modified_by as [Modified By],modified_date as [Modified Date] from TSPL_QC_LOG_SHEET_MASTER"

        strcode = clsCommon.ShowSelectForm("PMTFND", qry, "Code", whrCls, CurrCode, "Code", isButtonClicked)

        Return strcode
    End Function

    Public Shared Function SaveData(ByVal strCode As String, ByVal isNewEntry As Boolean, ByVal obj As clsPPLogSheetMaster) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            SaveData(strCode, isNewEntry, trans, obj)

            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function SaveData(ByVal strCode As String, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction, ByVal obj As clsPPLogSheetMaster) As Boolean
        Try
            Dim isSaved As Boolean = True

            If isNewEntry Then
                obj.code = clsERPFuncationality.GetNextCode(trans, clsCommon.GETSERVERDATE(trans), clsDocType.QCLOGSHEETMST, "", "")
            End If

            strCode = obj.code

            Dim coll As New Hashtable()

            clsCommon.AddColumnsForChange(coll, "comp_code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Code", strCode)
            clsCommon.AddColumnsForChange(coll, "Trans_Id", obj.Trans_Id)
            clsCommon.AddColumnsForChange(coll, "description", obj.desc)
            clsCommon.AddColumnsForChange(coll, "AliasName", obj.AliasName, True)
            clsCommon.AddColumnsForChange(coll, "type", obj.type)
            clsCommon.AddColumnsForChange(coll, "Nature", obj.nature)
            clsCommon.AddColumnsForChange(coll, "IsMandatory", obj.IsMandatory)
            clsCommon.AddColumnsForChange(coll, "IsRequired_InParameter_Master", obj.IsReq_Parameter_Master)
            clsCommon.AddColumnsForChange(coll, "Pick_Batch_No", obj.Pick_BO)
            clsCommon.AddColumnsForChange(coll, "Department_Code", obj.Department_COde)
            clsCommon.AddColumnsForChange(coll, "modified_by", clsCommon.myCstr(objCommonVar.CurrentUserCode))
            clsCommon.AddColumnsForChange(coll, "modified_date", clsCommon.myCstr(clsCommon.GETSERVERDATE(trans).ToString("dd/MM/yyyy")))

            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "created_by", clsCommon.myCstr(objCommonVar.CurrentUserCode))
                clsCommon.AddColumnsForChange(coll, "created_date", clsCommon.myCstr(clsCommon.GETSERVERDATE(trans).ToString("dd/MM/yyyy")))
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_QC_LOG_SHEET_MASTER", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_QC_LOG_SHEET_MASTER", OMInsertOrUpdate.Update, " Code='" + obj.code + "' and trans_id='" + obj.Trans_Id + "'", trans)
            End If

            isSaved = isSaved AndAlso clsPPLogSheetUserMaster.SaveData(strCode, obj.Arr, trans)

            If obj.IsReq_Parameter_Master = 1 AndAlso clsCommon.CompairString(obj.Trans_Id, "PRODUCTION") = CompairStringResult.Equal Then
                isSaved = isSaved AndAlso SaveParameterMaster(obj, isNewEntry, trans)
            End If

            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function SaveParameterMaster(ByVal obj As clsPPLogSheetMaster, ByVal isNewentry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim isSaved As Boolean = True
            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.code) > 0 Then
                Dim qry As String = ""
                If clsCommon.CompairString(obj.type, "OTHERS") = CompairStringResult.Equal Then
                    qry = "select count(*) from tspl_parameter_master where description='" + obj.desc + "' and type='" + obj.type + "'"
                Else
                    qry = "select count(*) from tspl_parameter_master where type='" + obj.type + "'"
                End If
                Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)

                '===============if not found then create in parameter master.
                If check <= 0 Then
                    check = clsDBFuncationality.getSingleValue("select count(*) from tspl_parameter_master where code in (select Parameter_Code from TSPL_QC_LOG_SHEET_MASTER where code='" + obj.code + "') ", trans)
                    If check > 0 Then
                        isNewentry = False
                    Else
                        isNewentry = True
                    End If

                    Dim strCode As String = ""
                    If isNewentry Then
                        strCode = clsERPFuncationality.GetNextCode(trans, clsCommon.GETSERVERDATE(trans), clsDocType.ParamMaster, "", "")
                    End If

                    Dim coll As New Hashtable()

                    clsCommon.AddColumnsForChange(coll, "comp_code", objCommonVar.CurrentCompanyCode)
                    clsCommon.AddColumnsForChange(coll, "Code", strCode)
                    clsCommon.AddColumnsForChange(coll, "description", obj.desc)
                    clsCommon.AddColumnsForChange(coll, "type", obj.type)
                    clsCommon.AddColumnsForChange(coll, "Nature", obj.nature)
                    clsCommon.AddColumnsForChange(coll, "IsMandatory", obj.IsMandatory)
                    clsCommon.AddColumnsForChange(coll, "modified_by", clsCommon.myCstr(objCommonVar.CurrentUserCode))
                    clsCommon.AddColumnsForChange(coll, "modified_date", clsCommon.myCstr(clsCommon.GETSERVERDATE(trans).ToString("dd/MM/yyyy")))

                    If isNewentry Then
                        clsCommon.AddColumnsForChange(coll, "created_by", clsCommon.myCstr(objCommonVar.CurrentUserCode))
                        clsCommon.AddColumnsForChange(coll, "created_date", clsCommon.myCstr(clsCommon.GETSERVERDATE(trans).ToString("dd/MM/yyyy")))
                        isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PARAMETER_MASTER", OMInsertOrUpdate.Insert, "", trans)
                    Else
                        isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PARAMETER_MASTER", OMInsertOrUpdate.Update, " TSPL_PARAMETER_MASTER.Code='" + strCode + "'", trans)
                    End If

                    isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery("Update TSPL_QC_LOG_SHEET_MASTER set Parameter_Code='" + strCode + "' where code='" + obj.code + "'", trans)
                End If
            End If

            Return isSaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, Optional ByVal Form_Id As String = "PRODUCTION", Optional ByVal trans As SqlTransaction = Nothing) As clsPPLogSheetMaster
        Dim obj As New clsPPLogSheetMaster()
        Try
            Dim qry As String = "select TSPL_QC_LOG_SHEET_MASTER.*,tspl_department_master.department_name from TSPL_QC_LOG_SHEET_MASTER left outer join tspl_department_master on tspl_department_master.department_code=TSPL_QC_LOG_SHEET_MASTER.department_code"

            Select Case NavType
                Case NavigatorType.Current
                    qry += " where TSPL_QC_LOG_SHEET_MASTER.code='" + strCode + "' and trans_id='" + Form_Id + "'"
                Case NavigatorType.First
                    qry += " where TSPL_QC_LOG_SHEET_MASTER.code in (select min(code) from TSPL_QC_LOG_SHEET_MASTER) and trans_id='" + Form_Id + "'"
                Case NavigatorType.Last
                    qry += " where TSPL_QC_LOG_SHEET_MASTER.code in (select max(code) from TSPL_QC_LOG_SHEET_MASTER where trans_id='" + Form_Id + "') and trans_id='" + Form_Id + "'"
                Case NavigatorType.Next
                    qry += " where TSPL_QC_LOG_SHEET_MASTER.code in (select min(code) from TSPL_QC_LOG_SHEET_MASTER where code>'" + strCode + "' and trans_id='" + Form_Id + "') and trans_id='" + Form_Id + "'"
                Case NavigatorType.Previous
                    qry += " where TSPL_QC_LOG_SHEET_MASTER.code in (select max(code) from TSPL_QC_LOG_SHEET_MASTER where code<'" + strCode + "' and trans_id='" + Form_Id + "') and trans_id='" + Form_Id + "'"
            End Select

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

            'Dim obj As New clsPPLogSheetMaster()
            obj.Arr = New List(Of clsPPLogSheetUserMaster)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                obj.code = clsCommon.myCstr(dt.Rows(0)("code"))
                obj.desc = clsCommon.myCstr(dt.Rows(0)("description"))
                obj.AliasName = clsCommon.myCstr(dt.Rows(0)("AliasName"))
                obj.type = clsCommon.myCstr(dt.Rows(0)("type"))
                obj.Trans_Id = clsCommon.myCstr(dt.Rows(0)("trans_id"))
                obj.nature = clsCommon.myCstr(dt.Rows(0)("nature"))
                obj.IsMandatory = clsCommon.myCdbl(dt.Rows(0)("IsMandatory"))
                obj.IsReq_Parameter_Master = clsCommon.myCdbl(dt.Rows(0)("IsRequired_InParameter_Master"))
                obj.Pick_BO = clsCommon.myCdbl(dt.Rows(0)("Pick_Batch_No"))
                obj.Department_COde = clsCommon.myCstr(dt.Rows(0)("Department_Code"))
                obj.Department_Name = clsCommon.myCstr(dt.Rows(0)("department_name"))

                qry = "select * from TSPL_QC_LOG_SHEET_USER_MASTER where code='" + obj.code + "'"
                Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

                If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
                    For Each dr As DataRow In dt1.Rows
                        Dim objtr As New clsPPLogSheetUserMaster()

                        objtr.UserCode = clsCommon.myCstr(dr("Emp_Code"))

                        obj.Arr.Add(objtr)
                    Next
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
        Return obj
    End Function

    Public Shared Function DeleteData(ByVal strCode As String, Optional ByVal Form_Id As String = "PRODUCTION") As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            DeleteData(strCode, trans, Form_Id)

            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function DeleteData(ByVal strCode As String, ByVal trans As SqlTransaction, ByVal Form_Id As String) As Boolean
        Try
            Dim isSaved As Boolean = True
            Dim check As Integer = 0

            check = clsDBFuncationality.getSingleValue("select count(*) from TSPL_PP_LOG_SHEET_DETAIL where parameter_Code='" + strCode + "'", trans)

            If check > 0 Then
                Throw New Exception("Record is in used, cannot be deleted.")
            End If
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery("delete from TSPL_QC_LOG_SHEET_USER_MASTER where code='" + strCode + "'", trans)
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery("delete from TSPL_QC_LOG_SHEET_MASTER where code='" + strCode + "' and trans_id='" + Form_Id + "'", trans)

            Return isSaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    '>>>>>>>>>>>>>>>>>>>>>>>
    Public Shared Function SaveIMPORTData(ByVal strCode As String, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction, ByVal obj As clsPPLogSheetMaster) As Boolean
        Try
            Dim isSaved As Boolean = True

            If isNewEntry Then
                obj.code = clsERPFuncationality.GetNextCode(trans, clsCommon.GETSERVERDATE(trans), clsDocType.QCLOGSHEETMST, "", "")
            End If

            strCode = obj.code

            Dim coll As New Hashtable()

            clsCommon.AddColumnsForChange(coll, "comp_code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Code", strCode)
            clsCommon.AddColumnsForChange(coll, "trans_id", obj.Trans_Id)
            clsCommon.AddColumnsForChange(coll, "description", obj.desc)
            clsCommon.AddColumnsForChange(coll, "AliasName", obj.AliasName, True)
            clsCommon.AddColumnsForChange(coll, "type", obj.type)
            clsCommon.AddColumnsForChange(coll, "Nature", obj.nature)
            clsCommon.AddColumnsForChange(coll, "IsMandatory", obj.IsMandatory)
            clsCommon.AddColumnsForChange(coll, "IsRequired_InParameter_Master", obj.IsReq_Parameter_Master)
            clsCommon.AddColumnsForChange(coll, "Pick_Batch_No", obj.Pick_BO)
            clsCommon.AddColumnsForChange(coll, "Department_Code", obj.Department_COde)
            clsCommon.AddColumnsForChange(coll, "modified_by", clsCommon.myCstr(objCommonVar.CurrentUserCode))
            clsCommon.AddColumnsForChange(coll, "modified_date", clsCommon.myCstr(clsCommon.GETSERVERDATE(trans).ToString("dd/MM/yyyy")))

            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "created_by", clsCommon.myCstr(objCommonVar.CurrentUserCode))
                clsCommon.AddColumnsForChange(coll, "created_date", clsCommon.myCstr(clsCommon.GETSERVERDATE(trans).ToString("dd/MM/yyyy")))
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_QC_LOG_SHEET_MASTER", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_QC_LOG_SHEET_MASTER", OMInsertOrUpdate.Update, " Code='" + obj.code + "' and trans_id='" + obj.Trans_Id + "'", trans)
            End If

            If obj.Import = True Then
                If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                    For Each objtr As clsPPLogSheetUserMaster In obj.Arr
                        isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery("delete from TSPL_QC_LOG_SHEET_USER_MASTER where code='" + obj.code + "' and emp_code='" + objtr.UserCode + "'", trans)

                        coll = New Hashtable()

                        clsCommon.AddColumnsForChange(coll, "code", obj.code)
                        clsCommon.AddColumnsForChange(coll, "Emp_Code", objtr.UserCode)

                        isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_QC_LOG_SHEET_USER_MASTER", OMInsertOrUpdate.Insert, "", trans)
                    Next
                End If
            End If

            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
End Class

Public Class clsPPLogSheetUserMaster
#Region "Variables"
    Public UserCode As String = Nothing
#End Region

    Public Shared Function SaveData(ByVal Code As String, ByVal arr As List(Of clsPPLogSheetUserMaster), ByVal trans As SqlTransaction) As Boolean
        Try
            Dim isSaved As Boolean = True

            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery("delete from TSPL_QC_LOG_SHEET_USER_MASTER where code='" + Code + "'", trans)

            If arr IsNot Nothing AndAlso arr.Count > 0 Then
                For Each objtr As clsPPLogSheetUserMaster In arr
                    Dim coll As New Hashtable()

                    clsCommon.AddColumnsForChange(coll, "code", Code)
                    clsCommon.AddColumnsForChange(coll, "Emp_Code", objtr.UserCode)

                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_QC_LOG_SHEET_USER_MASTER", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If

            Return isSaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
End Class