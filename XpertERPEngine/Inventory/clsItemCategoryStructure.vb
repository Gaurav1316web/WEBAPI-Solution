Imports common
Imports System.Data
Imports System.Data.SqlClient
Public Class clsItemCategoryStructure

#Region "Variables"
    Public ITEM_CATEGORY_STRUCT_CODE As String
    Public DESCRIPTION As String
    'Public CATEGORY_LEVEL As Integer

    Public ObjList As List(Of clsItemCategoryStructureDetails) = Nothing
    Dim objitmCatLevelDetails As New clsItemCategoryStructureDetails()
    Public formtype As String = Nothing
#End Region

    '----------------Code For Get Finder--------------------------------------------------------------------'
    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = "  select TSPL_ITEM_CATEGORY_STRUCTURE.ITEM_CATEGORY_STRUCT_CODE as [Code] ,TSPL_ITEM_CATEGORY_STRUCTURE.DESCRIPTION as [Description] ,TSPL_ITEM_CATEGORY_STRUCTURE.Comp_Code as [Company Code] ,TSPL_ITEM_CATEGORY_STRUCTURE.Created_By as [Created By] ,TSPL_ITEM_CATEGORY_STRUCTURE.Created_Date as [Created Date] ,TSPL_ITEM_CATEGORY_STRUCTURE.Modified_By as [Modified By] ,TSPL_ITEM_CATEGORY_STRUCTURE.Modified_Date as [Modified Date]  From TSPL_ITEM_CATEGORY_STRUCTURE  "
        str = clsCommon.ShowSelectForm("ITMCATSTRU", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str
    End Function
    '----------------End of Code For Get Finder--------------------------------------------------------------'
    Public Shared Function GetData(ByVal strCode As String, ByVal formtype As String, ByVal NavType As NavigatorType) As clsItemCategoryStructure
        Return GetData(strCode, formtype, NavType, Nothing)
    End Function
    Public Shared Function DeleteData(ByVal strCode As String, ByVal fromtype1 As String) As Boolean
        Dim isSaved As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try

            isSaved = True

            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Code not found to Delete")
            End If
            'isSaved = clsItemCategoryStructureDetails.DeleteData(strCode)
            Dim qry As String
            qry = "delete from TSPL_ITEM_CATEGORY_STRUCT_DETAIL where ITEM_CATEGORY_STRUCT_CODE ='" + strCode + "' and isnull(form_type,'item')='" + fromtype1 + "'"
            isSaved = isSaved And clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "delete from TSPL_ITEM_CATEGORY_STRUCTURE where ITEM_CATEGORY_STRUCT_CODE ='" + strCode + "' and isnull(form_type,'item')='" + fromtype1 + "'"
            isSaved = isSaved And clsDBFuncationality.ExecuteNonQuery(qry, trans)
            If isSaved = True Then
                trans.Commit()
            Else
                trans.Rollback()
            End If
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message.ToString())
            Return False
        End Try
        Return isSaved
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal formtype As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsItemCategoryStructure
        Dim obj As clsItemCategoryStructure = Nothing
        Dim qry As String = " select * from TSPL_ITEM_CATEGORY_STRUCTURE  where 2=2 and isnull(form_type,'item')='" + formtype + "'"
        Select Case NavType
            Case NavigatorType.First
                qry += " and ITEM_CATEGORY_STRUCT_CODE = (select MIN(ITEM_CATEGORY_STRUCT_CODE) from TSPL_ITEM_CATEGORY_STRUCTURE where isnull(form_type,'item')='" + formtype + "')"
            Case NavigatorType.Last
                qry += " and ITEM_CATEGORY_STRUCT_CODE = (select Max(ITEM_CATEGORY_STRUCT_CODE) from TSPL_ITEM_CATEGORY_STRUCTURE where isnull(form_type,'item')='" + formtype + "')"
            Case NavigatorType.Next
                qry += " and ITEM_CATEGORY_STRUCT_CODE = (select Min(ITEM_CATEGORY_STRUCT_CODE) from TSPL_ITEM_CATEGORY_STRUCTURE where  ITEM_CATEGORY_STRUCT_CODE>'" + strCode + "' and isnull(form_type,'item')='" + formtype + "')"
            Case NavigatorType.Previous
                qry += " and ITEM_CATEGORY_STRUCT_CODE = (select Max(ITEM_CATEGORY_STRUCT_CODE) from TSPL_ITEM_CATEGORY_STRUCTURE where ITEM_CATEGORY_STRUCT_CODE<'" + strCode + "' and isnull(form_type,'item')='" + formtype + "')"
            Case NavigatorType.Current
                qry += " and ITEM_CATEGORY_STRUCT_CODE = '" + strCode + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsItemCategoryStructure()
            obj.ITEM_CATEGORY_STRUCT_CODE = clsCommon.myCstr(dt.Rows(0)("ITEM_CATEGORY_STRUCT_CODE"))
            obj.DESCRIPTION = clsCommon.myCstr(dt.Rows(0)("DESCRIPTION"))

            obj.ObjList = clsItemCategoryStructureDetails.GetData(obj.ITEM_CATEGORY_STRUCT_CODE, formtype, trans)
        End If
        Return obj
    End Function

    Public Function SaveData(ByVal obj As clsItemCategoryStructure, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Try
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "DESCRIPTION", obj.DESCRIPTION)
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Form_Type", obj.formtype)

            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
            If isNewEntry Then

                If (IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowAutoGenerateDocNoInMaster, clsFixedParameterCode.AllowAutoGenerateDocNoInMaster, trans)) = "1", True, False)) Then
                    Dim ChkNewEntry As String = clsDBFuncationality.getSingleValue("Select count(*) from TSPL_ITEM_CATEGORY_STRUCTURE where ITEM_CATEGORY_STRUCT_CODE='" & obj.ITEM_CATEGORY_STRUCT_CODE & "'", trans)
                    If ChkNewEntry = 0 Then
                        obj.ITEM_CATEGORY_STRUCT_CODE = clsERPFuncationality.GetNextCode(trans, clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"), clsDocType.ItemCategoryStructure, "", "")
                        If clsCommon.myLen(obj.ITEM_CATEGORY_STRUCT_CODE) <= 0 Then
                            Throw New Exception("Error in Code Generation")
                        End If
                    End If
                End If

                clsCommon.AddColumnsForChange(coll, "ITEM_CATEGORY_STRUCT_CODE", obj.ITEM_CATEGORY_STRUCT_CODE)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                Dim qry As String = "SELECT Count(*) FROM TSPL_ITEM_CATEGORY_STRUCTURE where ITEM_CATEGORY_STRUCT_CODE= '" & obj.ITEM_CATEGORY_STRUCT_CODE & "' and isnull(form_type,'item')='" + obj.formtype + "'"
                Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
                If check = 0 Then
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ITEM_CATEGORY_STRUCTURE", OMInsertOrUpdate.Insert, "", trans)
                Else
                    Throw New Exception("This Code Is Already Exist")

                End If
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ITEM_CATEGORY_STRUCTURE", OMInsertOrUpdate.Update, "ITEM_CATEGORY_STRUCT_CODE='" + obj.ITEM_CATEGORY_STRUCT_CODE + "' and isnull(form_type,'item')='" + obj.formtype + "'", trans)
            End If
            isSaved = objitmCatLevelDetails.SaveData(obj.ITEM_CATEGORY_STRUCT_CODE, obj.formtype, obj.ObjList, trans)
            'If isSaved Then
            '    trans.Commit()
            'Else
            '    trans.Rollback()

            'End If


        Catch ex As Exception
            'trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function

End Class
Public Class clsItemCategoryStructureDetails

#Region "Variables"
    Public ITEM_CATEGORY_STRUCT_CODE As String
    Public ITEM_CATEGORY_CODE As String
    Public ITEM_CATEGORY_DESCRIPTION As String
    Public CATEGORY_LEVEL As Integer
    Public formtype_detail As String = Nothing
    Public Line_No As Integer = Nothing
#End Region

    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim isSaved As Boolean
        Try
            isSaved = False

            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Code not found to Delete")
            End If

            Dim qry As String
            qry = " delete from TSPL_ITEM_CATEGORY_STRUCT_DETAIL where ITEM_CATEGORY_STRUCT_CODE ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry)
        Catch ex As Exception
            Throw New Exception(ex.Message.ToString())
        End Try
        Return isSaved
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal formtype As String, ByVal trans As SqlTransaction) As List(Of clsItemCategoryStructureDetails)
        Dim obj As clsItemCategoryStructureDetails = Nothing
        Dim ObjList As New List(Of clsItemCategoryStructureDetails)
        Dim qry As String = " select TSPL_ITEM_CATEGORY_STRUCT_DETAIL.line_no,TSPL_ITEM_CATEGORY_STRUCT_DETAIL.ITEM_CATEGORY_STRUCT_CODE,TSPL_ITEM_CATEGORY_STRUCT_DETAIL.ITEM_CATEGORY_CODE, " & _
         " TSPL_ITEM_CATEGORY_STRUCT_DETAIL.CATEGORY_LEVEL,TSPL_ITEM_CATEGORY_LEVEL.DESCRIPTION AS ITEM_CATEGORY_DESCRIPTION from  " & _
         " TSPL_ITEM_CATEGORY_STRUCT_DETAIL INNER JOIN TSPL_ITEM_CATEGORY_LEVEL ON TSPL_ITEM_CATEGORY_STRUCT_DETAIL.ITEM_CATEGORY_CODE=TSPL_ITEM_CATEGORY_LEVEL.ITEM_CATEGORY_CODE and TSPL_ITEM_CATEGORY_LEVEL.form_type=TSPL_ITEM_CATEGORY_STRUCT_DETAIL.form_type and isnull(TSPL_ITEM_CATEGORY_LEVEL.form_type,'item')='" + formtype + "'" & _
         " WHERE TSPL_ITEM_CATEGORY_STRUCT_DETAIL.ITEM_CATEGORY_STRUCT_CODE = '" + strCode + "' and isnull(TSPL_ITEM_CATEGORY_STRUCT_DETAIL.form_type,'item')='" + formtype + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            For Each dr As DataRow In dt.Rows
                obj = New clsItemCategoryStructureDetails()
                obj.ITEM_CATEGORY_STRUCT_CODE = clsCommon.myCstr(dr("ITEM_CATEGORY_STRUCT_CODE"))
                obj.ITEM_CATEGORY_CODE = clsCommon.myCstr(dr("ITEM_CATEGORY_CODE"))
                obj.ITEM_CATEGORY_DESCRIPTION = clsCommon.myCstr(dr("ITEM_CATEGORY_DESCRIPTION"))
                obj.CATEGORY_LEVEL = clsCommon.myCdbl(dr("CATEGORY_LEVEL"))
                obj.Line_No = CInt(clsCommon.myCdbl(dr("line_no")))

                ObjList.Add(obj)
            Next
        End If
        Return ObjList

    End Function

    Public Function SaveData(ByVal strCode As String, ByVal formtype As String, ByVal ObjList As List(Of clsItemCategoryStructureDetails), ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Try
            Dim qry As String
            qry = "delete from TSPL_ITEM_CATEGORY_STRUCT_DETAIL where ITEM_CATEGORY_STRUCT_CODE ='" + strCode + "' and isnull(form_type,'item')='" + formtype + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)

            For Each obj As clsItemCategoryStructureDetails In ObjList
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "ITEM_CATEGORY_STRUCT_CODE", obj.ITEM_CATEGORY_STRUCT_CODE)
                clsCommon.AddColumnsForChange(coll, "ITEM_CATEGORY_CODE", obj.ITEM_CATEGORY_CODE)
                clsCommon.AddColumnsForChange(coll, "CATEGORY_LEVEL", obj.CATEGORY_LEVEL)
                clsCommon.AddColumnsForChange(coll, "form_type", obj.formtype_detail)
                clsCommon.AddColumnsForChange(coll, "line_no", obj.Line_No)

                qry = "SELECT Count(*) FROM TSPL_ITEM_CATEGORY_STRUCT_DETAIL where ITEM_CATEGORY_STRUCT_CODE = '" & strCode & "' and ITEM_CATEGORY_CODE='" & obj.ITEM_CATEGORY_CODE & "' and isnull(form_type,'item')='" + obj.formtype_detail + "' "
                Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)

                If check = 0 Then
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ITEM_CATEGORY_STRUCT_DETAIL", OMInsertOrUpdate.Insert, "", trans)
                Else
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ITEM_CATEGORY_STRUCT_DETAIL", OMInsertOrUpdate.Update, " ITEM_CATEGORY_STRUCT_CODE = '" & strCode & "' and ITEM_CATEGORY_CODE = '" & obj.ITEM_CATEGORY_CODE & "' and isnull(form_type,'item')='" + obj.formtype_detail + "' ", trans)
                End If
            Next
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function

End Class
