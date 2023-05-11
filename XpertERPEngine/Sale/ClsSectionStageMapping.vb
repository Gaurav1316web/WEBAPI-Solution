'-------------------------------BM00000003317----------------------------
Imports common
Imports System.Data
Imports System.Data.SqlClient

Public Class ClsSectionStageMapping

#Region "Variables"
    Public doc_code As String = Nothing
    Public doc_date As Date = Nothing
    Public Section_Code As String = Nothing
    Public section_desc As String = Nothing
    Public Cate_Code As String = Nothing
    Public Cate_desc As String = Nothing


    Public Arr As List(Of clsSectionStageMappingDetail) = Nothing
    Public Arr_User As List(Of clsSectionStageMapping_User) = Nothing
#End Region

    Public Shared Function GetFinder(ByVal whrCls As String, ByVal CurrCode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        If clsCommon.myLen(whrCls) > 0 Then
            whrCls = whrCls '+ " and tspl_section_stage_mapping_head.comp_code='" + objCommonVar.CurrentCompanyCode + "'"
        Else
            whrCls = "" ' tspl_section_stage_mapping_head.comp_code='" + objCommonVar.CurrentCompanyCode + "'"
        End If
        Dim qry As String = "select TSPL_SECTION_STAGE_MAPPING_HEAD.doc_code as [Code],TSPL_SECTION_STAGE_MAPPING_HEAD.doc_date as [Date],TSPL_SECTION_STAGE_MAPPING_HEAD.section_code as [Section Code],TSPL_SECTION_MASTER.Description,TSPL_SECTION_STAGE_MAPPING_HEAD.structure_code as [Production Category],TSPL_STRUCTURE_MASTER.Structure_Descq as [Category Description] from TSPL_SECTION_STAGE_MAPPING_HEAD left outer join TSPL_SECTION_MASTER on TSPL_SECTION_MASTER.Section_Code=TSPL_SECTION_STAGE_MAPPING_HEAD.section_code left outer join TSPL_STRUCTURE_MASTER on TSPL_STRUCTURE_MASTER.Structure_Code=TSPL_SECTION_STAGE_MAPPING_HEAD.structure_code "
        str = clsCommon.ShowSelectForm("SCTFND", qry, "Code", whrCls, CurrCode, "Code", isButtonClicked)

        Return str
    End Function

    Public Shared Function SaveData(ByVal strCode As String, ByVal obj As ClsSectionStageMapping, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim isSaved As Boolean = True
            Dim qry As String = ""

            If isNewEntry Then
                qry = "select max(doc_code) from TSPL_SECTION_STAGE_MAPPING_HEAD"
                obj.doc_code = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))

                If clsCommon.myLen(obj.doc_code) > 0 Then
                    obj.doc_code = clsCommon.incval(obj.doc_code)
                Else
                    obj.doc_code = "SCM000001"
                End If
            End If

            Dim coll As New Hashtable()

            clsCommon.AddColumnsForChange(coll, "comp_code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "doc_code", obj.doc_code)
            clsCommon.AddColumnsForChange(coll, "doc_date", clsCommon.GetPrintDate(obj.doc_date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Section_Code", obj.Section_Code)
            clsCommon.AddColumnsForChange(coll, "Structure_Code", obj.Cate_Code)
            clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.myCstr(clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy")))
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.myCstr(clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy")))

            If isNewEntry Then
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SECTION_STAGE_MAPPING_HEAD", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SECTION_STAGE_MAPPING_HEAD", OMInsertOrUpdate.Update, " TSPL_SECTION_STAGE_MAPPING_HEAD.doc_code='" + obj.doc_code + "'", trans)
            End If

            isSaved = isSaved AndAlso clsSectionStageMappingDetail.SaveData(obj.doc_code, obj.Section_Code, obj.Arr, trans)
            isSaved = isSaved AndAlso clsSectionStageMapping_User.SaveData(obj.doc_code, obj.Arr_User, obj.Section_Code, trans)

            trans.Commit()
            Return isSaved
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function DeleteData(ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        Try
            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Code not found to Delete")
            End If

            Dim qry As String
            qry = "delete from TSPL_SECTION_STAGE_MAPPING_HEAD where DOC_CODE ='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_SECTION_STAGE_MAPPING where DOC_CODE ='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_SECTION_STAGE_USER_DETAIL where DOC_CODE ='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As ClsSectionStageMapping
        Return GetData(strCode, NavType, Nothing)
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal tran As SqlTransaction) As ClsSectionStageMapping
        Try
            Dim obj As New ClsSectionStageMapping()

            Dim qst As String = "Select * From TSPL_SECTION_STAGE_MAPPING_HEAD where 2=2 "
            Select Case NavType
                Case NavigatorType.Current
                    qst += " and DOC_CODE = '" + strCode + "'"
                Case NavigatorType.Next
                    qst += "and DOC_CODE in (select min(DOC_CODE) from TSPL_SECTION_STAGE_MAPPING_HEAD where DOC_CODE > '" + strCode + "' ) "
                Case NavigatorType.First
                    qst += "and DOC_CODE in (select MIN(DOC_CODE) from TSPL_SECTION_STAGE_MAPPING_HEAD )"
                Case NavigatorType.Last
                    qst += "and DOC_CODE in (select Max(DOC_CODE) from TSPL_SECTION_STAGE_MAPPING_HEAD )"
                Case NavigatorType.Previous
                    qst += "and DOC_CODE in (select max(DOC_CODE) from TSPL_SECTION_STAGE_MAPPING_HEAD where DOC_CODE < '" + strCode + "'  )"
            End Select
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qst, tran)

            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj.doc_code = clsCommon.myCstr(dt.Rows(0)("doc_code"))
                obj.doc_date = clsCommon.myCDate(dt.Rows(0)("doc_date"))
                obj.Section_Code = clsCommon.myCstr(dt.Rows(0)("Section_Code"))
                obj.section_desc = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select description from tspl_section_master where section_code='" + obj.Section_Code + "'", tran))
                obj.Cate_Code = clsCommon.myCstr(dt.Rows(0)("structure_code"))
                obj.Cate_desc = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select structure_descq from TSPL_STRUCTURE_MASTER where structure_code='" + obj.Cate_Code + "'", tran))

                obj.Arr = New List(Of clsSectionStageMappingDetail)
                obj.Arr_User = New List(Of clsSectionStageMapping_User)

                Dim qry As String = "select * from TSPL_SECTION_STAGE_MAPPING where doc_code='" + obj.doc_code + "'"
                Dim dt2 As DataTable = clsDBFuncationality.GetDataTable(qry, tran)

                If dt2 IsNot Nothing AndAlso dt2.Rows.Count > 0 Then
                    For Each dr As DataRow In dt2.Rows
                        Dim objtr As New clsSectionStageMappingDetail()

                        objtr.sno = CInt(dr("sno"))
                        objtr.stagecode = clsCommon.myCstr(dr("Stage_Code"))
                        objtr.stagedesc = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select description from TSPL_STAGE_MASTER where stage_code='" + objtr.stagecode + "'", tran))
                        objtr.logsheetno = clsCommon.myCstr(dr("Log_Sheet_No"))
                        objtr.sequnceno = CInt(dr("Sequence_No")) 'CInt(clsDBFuncationality.getSingleValue("select sequence_no from TSPL_PP_LOG_SHEET_HEAD where doc_no='" + objtr.logsheetno + "'"))
                        objtr.departcode = clsCommon.myCstr(dr("Department_Code"))
                        objtr.departname = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select department_name from TSPL_DEPARTMENT_MASTER where department_code='" + objtr.departcode + "'", tran))
                        objtr.No_of_Department = CInt(clsCommon.myCdbl(dr("No_of_Department")))
                        objtr.Stage_Type = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Stage_Type from tspl_stage_master where Stage_Code='" + objtr.stagecode + "'", tran))
                        qry = "select * from TSPL_SECTION_STAGE_USER_DETAIL where doc_code='" + obj.doc_code + "' and section_code='" + obj.Section_Code + "' and stage_code='" + objtr.stagecode + "'"
                        Dim dt3 As DataTable = clsDBFuncationality.GetDataTable(qry, tran)
                        Dim objtr1 As New clsSectionStageMapping_User()
                        Dim count As Integer = 0

                        If dt3 IsNot Nothing AndAlso dt3.Rows.Count > 0 Then
                            For Each dr1 As DataRow In dt3.Rows
                                'objtr1 = New clsSectionStageMapping_User()

                                objtr1.stagecode = objtr.stagecode
                                objtr1.usercode = objtr1.usercode + "," + clsCommon.myCstr(dr1("User_Code"))

                                count += 1
                            Next
                        End If 'end dt3

                        If clsCommon.myLen(objtr1.usercode) > 0 AndAlso objtr1.usercode.Substring(0, 1) = "," Then
                            objtr1.usercode = objtr1.usercode.Substring(1, objtr1.usercode.Length - 1)
                        End If

                        objtr.users = objtr1.usercode
                        objtr.noofuser = count

                        obj.Arr.Add(objtr)
                    Next
                End If 'end dt2

            End If 'end dt

            Return obj
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function


End Class

Public Class clsSectionStageMappingDetail
#Region "variables"
    Public sno As Integer = Nothing
    Public stagecode As String = Nothing
    Public stagedesc As String = Nothing
    Public logsheetno As String = Nothing
    Public sequnceno As Integer = Nothing
    Public departcode As String = Nothing
    Public departname As String = Nothing
    Public users As String = Nothing
    Public noofuser As Integer = Nothing
    Public Stage_Type As String = Nothing
    Public No_of_Department As Integer = Nothing
#End Region

    Public Shared Function SaveData(ByVal strCode As String, ByVal Section_code As String, ByVal arr As List(Of clsSectionStageMappingDetail), ByVal trans As SqlTransaction) As Boolean
        Try
            Dim isSaved As Boolean = True
            Dim coll As New Hashtable()

            Dim qry As String = "delete from TSPL_SECTION_STAGE_MAPPING where Doc_CODE ='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            If arr IsNot Nothing AndAlso arr.Count > 0 Then
                For Each objtr As clsSectionStageMappingDetail In arr
                    coll = New Hashtable()

                    clsCommon.AddColumnsForChange(coll, "comp_code", objCommonVar.CurrentCompanyCode)
                    clsCommon.AddColumnsForChange(coll, "doc_code", strCode)
                    clsCommon.AddColumnsForChange(coll, "Section_Code", Section_code)
                    clsCommon.AddColumnsForChange(coll, "SNO", objtr.sno)
                    clsCommon.AddColumnsForChange(coll, "Stage_Code", objtr.stagecode)
                    clsCommon.AddColumnsForChange(coll, "Department_Code", objtr.departcode)
                    clsCommon.AddColumnsForChange(coll, "Log_Sheet_No", objtr.logsheetno, True)
                    clsCommon.AddColumnsForChange(coll, "Sequence_No", objtr.sequnceno)
                    clsCommon.AddColumnsForChange(coll, "No_of_Department", objtr.No_of_Department)

                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SECTION_STAGE_MAPPING", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If

            Return isSaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
End Class

Public Class clsSectionStageMapping_User
#Region "variables"
    Public stagecode As String = Nothing
    Public usercode As String = Nothing
#End Region

    Public Shared Function SaveData(ByVal strCode As String, ByVal arr As List(Of clsSectionStageMapping_User), ByVal Section_code As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim isSaved As Boolean = True
            Dim coll As New Hashtable()

            Dim qry As String = "delete from TSPL_SECTION_STAGE_USER_DETAIL where doc_CODE ='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            If arr IsNot Nothing AndAlso arr.Count > 0 Then
                For Each objtr As clsSectionStageMapping_User In arr
                    coll = New Hashtable()

                    clsCommon.AddColumnsForChange(coll, "comp_code", objCommonVar.CurrentCompanyCode)
                    clsCommon.AddColumnsForChange(coll, "doc_code", strCode)
                    clsCommon.AddColumnsForChange(coll, "Section_Code", Section_code)
                    clsCommon.AddColumnsForChange(coll, "Stage_Code", objtr.stagecode)
                    clsCommon.AddColumnsForChange(coll, "User_Code", objtr.usercode)

                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SECTION_STAGE_USER_DETAIL", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If

            Return isSaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Shared Function GetLogsheetUsers(ByVal doc_code As String, ByVal Stage_Code As String, ByVal trans As SqlTransaction) As List(Of clsSectionStageMapping_User)
        Dim objQCList As New List(Of clsSectionStageMapping_User)
        Dim qry As String = " select Section_Code,Stage_Code,User_Code,Doc_Code from TSPL_SECTION_STAGE_USER_DETAIL where Doc_Code='" & doc_code & "' and Stage_Code='" & Stage_Code & "'"
        Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
            For Each dr As DataRow In dt1.Rows
                Dim objtr As New clsSectionStageMapping_User()


                objtr.stagecode = clsCommon.myCstr(dr("Stage_Code"))
                objtr.usercode = clsCommon.myCstr(dr("User_Code"))

                objQCList.Add(objtr)
            Next
        End If
        Return objQCList
    End Function
End Class
