Imports common
Imports System.Data
Imports System.Data.SqlClient
Public Class clsItemCategoryLevel

#Region "Variables"
    Public ITEM_CATEGORY_CODE As String
    Public DESCRIPTION As String
    Public CATEGORY_LEVEL As Integer
    Public Master_Packing As Integer = Nothing
    Public Bin_Mapping As Integer = Nothing
    Public ObjList As List(Of clsItemCategoryLevelDetails) = Nothing
    Dim objitmCatLevelDetails As New clsItemCategoryLevelDetails()
    Public formtype As String = Nothing
    Public strMsg As String = ""
#End Region

    '----------------Code For Get Finder--------------------------------------------------------------------'
    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean, ByVal formtype As String) As String
        Dim str As String = ""
        Dim qry As String = "  select TSPL_ITEM_CATEGORY_LEVEL.ITEM_CATEGORY_CODE as [Code] ,TSPL_ITEM_CATEGORY_LEVEL.DESCRIPTION as [Description] ,TSPL_ITEM_CATEGORY_LEVEL.CATEGORY_LEVEL as [Category Level] ,TSPL_ITEM_CATEGORY_LEVEL.Comp_Code as [Company Code] ,TSPL_ITEM_CATEGORY_LEVEL.Created_By as [Created By] ,TSPL_ITEM_CATEGORY_LEVEL.Created_Date as [Created Date] ,TSPL_ITEM_CATEGORY_LEVEL.Modified_By as [Modified By] ,TSPL_ITEM_CATEGORY_LEVEL.Modified_Date as [Modified Date]  From TSPL_ITEM_CATEGORY_LEVEL  "
        str = clsCommon.ShowSelectForm("ITMCATLVL", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str
    End Function
    '----------------End of Code For Get Finder--------------------------------------------------------------'
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal formtype As String) As clsItemCategoryLevel
        Return GetData(strCode, NavType, formtype, Nothing)
    End Function
    Public Shared Function DeleteData(ByVal strCode As String, ByVal formtype As String, Optional ByVal Trans As SqlTransaction = Nothing) As Boolean
        Dim isSaved As Boolean
        Try
            ' Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
            isSaved = False

            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Code not found to Delete")
            End If
            'isSaved = clsItemCategoryLevelDetails.DeleteData(strCode)
            Dim qry As String
            qry = "delete from TSPL_ITEM_CATEGORY_LEVEL_VALUES where ITEM_CATEGORY_CODE ='" + strCode + "' and isnull(form_type,'ITEM')='" + formtype + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, Trans)
            qry = "delete from TSPL_ITEM_CATEGORY_LEVEL where ITEM_CATEGORY_CODE ='" + strCode + "' and isnull(form_type,'ITEM')='" + formtype + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, Trans)
          
        Catch ex As Exception

            Throw New Exception(ex.Message.ToString())
        End Try
        Return isSaved
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal formtype As String, ByVal trans As SqlTransaction) As clsItemCategoryLevel
        Dim obj As clsItemCategoryLevel = Nothing
        Dim qry As String = " select * from TSPL_ITEM_CATEGORY_LEVEL  where 2=2 and isnull(form_type,'ITEM')='" + formtype + "'"
        Select Case NavType
            Case NavigatorType.First
                qry += " and ITEM_CATEGORY_CODE = (select MIN(ITEM_CATEGORY_CODE) from TSPL_ITEM_CATEGORY_LEVEL where isnull(form_type,'ITEM')='" + formtype + "')"
            Case NavigatorType.Last
                qry += " and ITEM_CATEGORY_CODE = (select Max(ITEM_CATEGORY_CODE) from TSPL_ITEM_CATEGORY_LEVEL where isnull(form_type,'ITEM')='" + formtype + "')"
            Case NavigatorType.Next
                qry += " and ITEM_CATEGORY_CODE = (select Min(ITEM_CATEGORY_CODE) from TSPL_ITEM_CATEGORY_LEVEL where  ITEM_CATEGORY_CODE>'" + strCode + "' and isnull(form_type,'ITEM')='" + formtype + "')"
            Case NavigatorType.Previous
                qry += " and ITEM_CATEGORY_CODE = (select Max(ITEM_CATEGORY_CODE) from TSPL_ITEM_CATEGORY_LEVEL where ITEM_CATEGORY_CODE<'" + strCode + "' and isnull(form_type,'ITEM')='" + formtype + "')"
            Case NavigatorType.Current
                qry += " and ITEM_CATEGORY_CODE = '" + strCode + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsItemCategoryLevel()
            obj.ITEM_CATEGORY_CODE = clsCommon.myCstr(dt.Rows(0)("ITEM_CATEGORY_CODE"))
            obj.DESCRIPTION = clsCommon.myCstr(dt.Rows(0)("DESCRIPTION"))
            obj.CATEGORY_LEVEL = clsCommon.myCstr(dt.Rows(0)("CATEGORY_LEVEL"))
            obj.ObjList = clsItemCategoryLevelDetails.GetData(obj.ITEM_CATEGORY_CODE, formtype, trans)
            obj.Master_Packing = CInt(dt.Rows(0)("Master_Packing"))
            obj.Bin_Mapping = CInt(dt.Rows(0)("Bin_Mapping"))
        End If
        Return obj
    End Function

    Public Function SaveData(ByVal obj As clsItemCategoryLevel, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Try
            ''richa agarwal against ticket no. BM00000004266
            'clsDBFuncationality.ExecuteNonQuery("delete from TSPL_ITEM_CATEGORY_LEVEL_VALUES where ITEM_CATEGORY_CODE ='" & obj.ITEM_CATEGORY_CODE & "'", trans)
            ' ''-----------------------------
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "DESCRIPTION", obj.DESCRIPTION)
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "CATEGORY_LEVEL", obj.CATEGORY_LEVEL)
            clsCommon.AddColumnsForChange(coll, "form_type", obj.formtype)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Master_Packing", obj.Master_Packing)
            clsCommon.AddColumnsForChange(coll, "Bin_Mapping", obj.Bin_Mapping)

            If isNewEntry Then
                If (IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowAutoGenerateDocNoInMaster, clsFixedParameterCode.AllowAutoGenerateDocNoInMaster, trans)) = "1", True, False)) Then
                    Dim ChkNewEntry As String = clsDBFuncationality.getSingleValue("Select count(*) from TSPL_ITEM_CATEGORY_LEVEL where ITEM_CATEGORY_CODE='" & obj.ITEM_CATEGORY_CODE & "'", trans)
                    If ChkNewEntry = 0 Then
                        obj.ITEM_CATEGORY_CODE = clsERPFuncationality.GetNextCode(trans, clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"), clsDocType.ItemCategoryLevel, "", "")
                        If clsCommon.myLen(obj.ITEM_CATEGORY_CODE) <= 0 Then
                            Throw New Exception("Error in Code Generation")
                        End If
                    End If
                End If
                clsCommon.AddColumnsForChange(coll, "ITEM_CATEGORY_CODE", obj.ITEM_CATEGORY_CODE)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                Dim qry As String = "SELECT Count(*) FROM TSPL_ITEM_CATEGORY_LEVEL where ITEM_CATEGORY_CODE= '" & obj.ITEM_CATEGORY_CODE & "' and isnull(form_type,'ITEM')='" + obj.formtype + "'"
                Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
                If check = 0 Then
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ITEM_CATEGORY_LEVEL", OMInsertOrUpdate.Insert, "", trans)
                Else
                    Throw New Exception("This Code Is Already Exist")

                End If
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ITEM_CATEGORY_LEVEL", OMInsertOrUpdate.Update, "ITEM_CATEGORY_CODE='" + obj.ITEM_CATEGORY_CODE + "' and isnull(form_type,'ITEM')='" + obj.formtype + "'", trans)
            End If
            isSaved = objitmCatLevelDetails.SaveData(obj.ITEM_CATEGORY_CODE, obj.ObjList, trans)
            'If isSaved Then
            '    trans.Commit()
            'Else
            '    trans.Rollback()
            'End If
        Catch ex As Exception
            'trans.Rollback()
            'Throw New Exception(ex.Message)
            strMsg = clsCommon.myCstr(ex.Message)
            isSaved = False
        End Try
        Return isSaved
    End Function

End Class
Public Class clsItemCategoryLevelDetails

#Region "Variables"
    Public ITEM_CATEGORY_CODE As String
    Public CODE As String
    Public DESCRIPTION As String
    Public formtype_detail As String = Nothing
    Public BinNo As String

#End Region

    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim isSaved As Boolean
        Try
            isSaved = False

            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Code not found to Delete")
            End If

            Dim qry As String
            qry = " delete from TSPL_ITEM_CATEGORY_LEVEL_VALUES where ITEM_CATEGORY_CODE ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry)
        Catch ex As Exception
            Throw New Exception(ex.Message.ToString())
        End Try
        Return isSaved
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal formtype As String, ByVal trans As SqlTransaction) As List(Of clsItemCategoryLevelDetails)
        Dim obj As clsItemCategoryLevelDetails = Nothing
        Dim ObjList As New List(Of clsItemCategoryLevelDetails)
        Dim qry As String = " select *  from TSPL_ITEM_CATEGORY_LEVEL_VALUES WHERE ITEM_CATEGORY_CODE = '" + strCode + "' and isnull(form_type,'ITEM')='" + formtype + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            For Each dr As DataRow In dt.Rows
                obj = New clsItemCategoryLevelDetails()
                obj.ITEM_CATEGORY_CODE = clsCommon.myCstr(dr("ITEM_CATEGORY_CODE"))
                obj.CODE = clsCommon.myCstr(dr("CODE"))
                obj.DESCRIPTION = clsCommon.myCstr(dr("DESCRIPTION"))
                obj.BinNo = clsCommon.myCstr(dr("Bin_No"))
                ObjList.Add(obj)
            Next
        End If
        Return ObjList

    End Function

    Public Function SaveData(ByVal strCode As String, ByVal ObjList As List(Of clsItemCategoryLevelDetails), ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Dim ArrList As New List(Of String)
        Try
            For Each obj As clsItemCategoryLevelDetails In ObjList
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "ITEM_CATEGORY_CODE", strCode)
                clsCommon.AddColumnsForChange(coll, "CODE", obj.CODE)
                clsCommon.AddColumnsForChange(coll, "DESCRIPTION", obj.DESCRIPTION)
                clsCommon.AddColumnsForChange(coll, "form_type", obj.formtype_detail)
                clsCommon.AddColumnsForChange(coll, "Bin_No", obj.BinNo)

                Dim qry As String = "SELECT Count(*) FROM TSPL_ITEM_CATEGORY_LEVEL_VALUES where ITEM_CATEGORY_CODE = '" & strCode & "' and code='" & obj.CODE & "' and isnull(form_type,'ITEM')='" + obj.formtype_detail + "'"
                Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)

                If check = 0 Then
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ITEM_CATEGORY_LEVEL_VALUES", OMInsertOrUpdate.Insert, "", trans)
                Else
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ITEM_CATEGORY_LEVEL_VALUES", OMInsertOrUpdate.Update, " ITEM_CATEGORY_CODE = '" & strCode & "' and CODE = '" & obj.CODE & "' and isnull(form_type,'ITEM')='" + obj.formtype_detail + "' ", trans)
                End If
                ArrList.Add(obj.CODE)
            Next
            ''richa agarwal on 08/12/2014 against ticket no. 
            clsDBFuncationality.ExecuteNonQuery("delete from TSPL_ITEM_CATEGORY_LEVEL_VALUES where ITEM_CATEGORY_CODE ='" & strCode & "' and CODE NOT IN (" & clsCommon.GetMulcallString(ArrList) & ")", trans)
            ' ''-----------------------------
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function

    'Public Shared Function BlankDataTableForGrid() As DataTable
    '    Dim qry As String = " select TSPL_LEAVE_MASTER.LEAVE_CODE, TSPL_LEAVE_MASTER.LEAVE_NAME, '' as ALLOTED_LEAVE  from TSPL_LEAVE_MASTER "
    '    Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
    '    Return dt
    'End Function

End Class
