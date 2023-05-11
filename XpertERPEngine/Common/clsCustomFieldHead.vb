Imports common
Imports System.Data.SqlClient
Imports Telerik.WinControls.UI
Imports System.Windows.Forms

Public Class clsCustomFieldHead
#Region "Variables"
    Public Code As String = Nothing
    Public Name As String = Nothing
    Public Type As Integer = Nothing
    Public Is_Validate As Boolean = Nothing
    Public Arr As List(Of clsCustomFieldDetail)
    Public ArrCondition As List(Of clsCustomFieldConditions)
    Public FieldName As String = String.Empty
    Public MaxLength As Integer = 0
    Public Is_Mandatory As Integer = 0
    Public ReferenceTableName As String = String.Empty
    Public ReferenceFieldName As String = String.Empty
    Public IsSourceFromTable As Integer = 0
    Public IsSourceFromValueList As Integer = 0
    Public IsUnique As Integer = 0
#End Region

    Public Function SaveData(ByVal obj As clsCustomFieldHead, ByVal isNewEntry As Boolean) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Dim isSaved As Boolean = True
        Try
            Dim qry As String = "delete from TSPL_CUSTOM_FIELD_DETAIL where Custom_Field_Code='" + obj.Code + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            Dim strDocNo As String = ""
            If isNewEntry Then
                qry = "select max(Code) from TSPL_CUSTOM_FIELD_HEAD"
                obj.Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
                If clsCommon.myLen(obj.Code) <= 0 Then
                    obj.Code = "CF00000001"
                Else
                    obj.Code = clsCommon.incval(obj.Code)
                End If
            End If
            If (clsCommon.myLen(obj.Code) <= 0) Then
                Throw New Exception("Error in Document Code Generation")
            End If

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Name", obj.Name)
            clsCommon.AddColumnsForChange(coll, "Type", obj.Type)
            clsCommon.AddColumnsForChange(coll, "FieldName", obj.FieldName)
            clsCommon.AddColumnsForChange(coll, "Is_Validate", IIf(obj.Is_Validate, 1, 0))
            clsCommon.AddColumnsForChange(coll, "Is_Mandatory", obj.Is_Mandatory)
            clsCommon.AddColumnsForChange(coll, "MaxLength", obj.MaxLength)
            clsCommon.AddColumnsForChange(coll, "ReferenceTableName", obj.ReferenceTableName)
            clsCommon.AddColumnsForChange(coll, "ReferenceFieldName", obj.ReferenceFieldName)
            clsCommon.AddColumnsForChange(coll, "IsSourceFromTable", obj.IsSourceFromTable)
            clsCommon.AddColumnsForChange(coll, "IsSourceFromValueList", obj.IsSourceFromValueList)
            clsCommon.AddColumnsForChange(coll, "IsUnique", obj.IsUnique)
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Code", obj.Code)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CUSTOM_FIELD_HEAD", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CUSTOM_FIELD_HEAD", OMInsertOrUpdate.Update, "TSPL_CUSTOM_FIELD_HEAD.Code='" + obj.Code + "'", trans)
            End If
            isSaved = isSaved AndAlso clsCustomFieldConditions.SaveData(obj.Code, obj.ArrCondition, trans)
            isSaved = isSaved AndAlso clsCustomFieldDetail.SaveData(obj.Code, obj.Arr, trans)
            trans.Commit()
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function

    Public Shared Function GetData(ByVal strPONo As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsCustomFieldHead
        Dim obj As clsCustomFieldHead = Nothing
        Dim qry As String = "SELECT * FROM TSPL_CUSTOM_FIELD_HEAD  where 2=2"
        Dim whrClas As String = ""
        Select Case NavType
            Case NavigatorType.First
                qry += " and Code = (select MIN(Code) from TSPL_CUSTOM_FIELD_HEAD where 1=1 " + whrClas + ")"
            Case NavigatorType.Last
                qry += " and Code = (select Max(Code) from TSPL_CUSTOM_FIELD_HEAD where 1=1 " + whrClas + ")"
            Case NavigatorType.Next
                qry += " and Code = (select Min(Code) from TSPL_CUSTOM_FIELD_HEAD where Code>'" + strPONo + "' " + whrClas + ")"
            Case NavigatorType.Previous
                qry += " and Code = (select Max(Code) from TSPL_CUSTOM_FIELD_HEAD where Code<'" + strPONo + "' " + whrClas + ")"
            Case NavigatorType.Current
                qry += " and Code = '" + strPONo + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsCustomFieldHead()
            obj.Code = clsCommon.myCstr(dt.Rows(0)("Code"))
            obj.Name = clsCommon.myCstr(dt.Rows(0)("Name"))
            obj.Type = clsCommon.myCstr(dt.Rows(0)("Type"))
            obj.FieldName = clsCommon.myCstr(dt.Rows(0)("FieldName"))
            obj.MaxLength = clsCommon.myCdbl(dt.Rows(0)("MaxLength"))
            obj.Is_Mandatory = clsCommon.myCdbl(dt.Rows(0)("Is_Mandatory"))
            obj.ReferenceTableName = clsCommon.myCstr(dt.Rows(0)("ReferenceTableName"))
            obj.ReferenceFieldName = clsCommon.myCstr(dt.Rows(0)("ReferenceFieldName"))
            obj.IsSourceFromTable = clsCommon.myCdbl(dt.Rows(0)("IsSourceFromTable"))
            obj.IsSourceFromValueList = clsCommon.myCdbl(dt.Rows(0)("IsSourceFromValueList"))
            obj.Is_Validate = clsCommon.myCdbl(dt.Rows(0)("Is_Validate"))
            obj.IsUnique = clsCommon.myCdbl(dt.Rows(0)("IsUnique"))

            qry = "SELECT * from TSPL_CUSTOM_FIELD_DETAIL where Custom_Field_Code='" + obj.Code + "' ORDER BY SNo"
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj.Arr = New List(Of clsCustomFieldDetail)
                Dim objTr As clsCustomFieldDetail
                For Each dr As DataRow In dt.Rows
                    objTr = New clsCustomFieldDetail
                    objTr.Custom_Field_Code = clsCommon.myCstr(dr("Custom_Field_Code"))
                    objTr.Value = clsCommon.myCstr(dr("Value"))
                    objTr.Description = clsCommon.myCstr(dr("Description"))
                    objTr.SNo = Convert.ToInt32(clsCommon.myCdbl(dr("SNo")))
                    obj.Arr.Add(objTr)
                Next
            End If

            qry = "SELECT * from TSPL_CUSTOM_FIELD_Conditions where Custom_Field_Code='" + obj.Code + "' ORDER BY SNo"
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj.ArrCondition = New List(Of clsCustomFieldConditions)
                Dim objTr As clsCustomFieldConditions
                For Each dr As DataRow In dt.Rows
                    objTr = New clsCustomFieldConditions
                    objTr.Custom_Field_Code = clsCommon.myCstr(dr("Custom_Field_Code"))
                    objTr.LogicalOperator = clsCommon.myCstr(dr("LogicalOperator"))
                    objTr.ConditionalOperator = clsCommon.myCdbl(dr("ConditionalOperator"))
                    objTr.ConditionValue = clsCommon.myCstr(dr("ConditionValue"))
                    objTr.SNo = Convert.ToInt32(clsCommon.myCdbl(dr("SNo")))
                    obj.ArrCondition.Add(objTr)
                Next
            End If
        End If
        Return obj
    End Function

    Public Shared Function DeleteData(ByVal strDocCode As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim obj As New clsCustomFieldHead()
            obj = clsCustomFieldHead.GetData(strDocCode, NavigatorType.Current, trans)
            If obj IsNot Nothing AndAlso Len(obj.Code) > 0 Then
                Dim Count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select COUNT(*) from TSPL_CUSTOM_FIELD_MAPPING Where Custom_Field_Code='" + strDocCode + "'", trans))
                If Count <= 0 Then
                    clsDBFuncationality.ExecuteNonQuery("Delete from TSPL_CUSTOM_FIELD_DETAIL Where Custom_Field_Code='" + strDocCode + "'", trans)
                    clsDBFuncationality.ExecuteNonQuery("Delete from TSPL_CUSTOM_FIELD_Conditions Where Custom_Field_Code='" + strDocCode + "'", trans)
                    clsDBFuncationality.ExecuteNonQuery("Delete from TSPL_CUSTOM_FIELD_HEAD Where Code='" + strDocCode + "'", trans)
                    Return True
                Else
                    Throw New Exception("This field is in use, can not be deleted.")
                End If
            Else
                Throw New Exception("No data found to delete.")
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
End Class

Public Class clsCustomFieldDetail
#Region "Variables"
    Public Custom_Field_Code As String = Nothing
    Public Value As String = Nothing
    Public Description As String = Nothing
    Public SNo As Integer = 0

#End Region

    Public Shared Function SaveData(ByVal strCode As String, ByVal Arr As List(Of clsCustomFieldDetail), ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            Dim counter As Integer = 1
            For Each obj As clsCustomFieldDetail In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Custom_Field_Code", strCode)
                clsCommon.AddColumnsForChange(coll, "Value", obj.Value)
                clsCommon.AddColumnsForChange(coll, "Description", obj.Description)
                clsCommon.AddColumnsForChange(coll, "SNo", counter)
                counter += 1
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CUSTOM_FIELD_DETAIL", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function
End Class

Public Class clsCustomFieldMapping
#Region "Variables"
    Public SNo As Integer = 0
    Public Program_Code As String = Nothing
    Public Custom_Field_Code As String = Nothing
    Public Custom_Field_Name As String = Nothing ''Not a table column
    Public Custom_Field_Field_Name As String = Nothing
    Public Is_Validate As Boolean = False ''Not a table column
    Public Type As Integer = Nothing ''Not a table column
    Public Is_Mandatory As Boolean = False
    Public Default_Value As String = Nothing
    Public Is_For_Detail_Level As Boolean = False
    Public Is_For_Print As Boolean = False
    Public Is_CalCulated_Column As Integer = 0
    Public CalculationExpression As String = String.Empty
    Public FieldName As String = String.Empty
    Public MaxLength As Integer = 0
    Public ReferenceTableName As String = String.Empty
    Public ReferenceFieldName As String = String.Empty
    Public IsSourceFromTable As Integer = 0
    Public IsSourceFromValueList As Integer = 0
    Public IsUnique As Integer = 0
    Public Created_By As String = String.Empty
    Public Created_Date As Date = Nothing
    Public Modified_By As String = String.Empty
    Public Modified_Date As Date = Nothing
    Public comp_code As String = String.Empty
    Public MethodCode As String = String.Empty
    Public MethodArg As String = String.Empty
    Public arrValueList As List(Of clsCustomFieldMappingValueList) = Nothing
    Public arrConditions As List(Of clsCustomFieldMappingConditions) = Nothing
#End Region
    Public Shared Function getFieldNameFromCode(code As String) As String
        Dim rValue As String = String.Empty
        Try
            rValue = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select FieldName from tspl_custom_field_head where code='" & code & "'"))
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return rValue
    End Function

    Public Shared Function getCodeFromFieldName(fieldName As String) As String
        Dim rValue As String = String.Empty
        Try
            rValue = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select code from tspl_custom_field_head where FieldName='" & fieldName & "'"))
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return rValue
    End Function
    Public Shared Function getGridFieldNameFromCode(code As String, gvName As String, FormName As String) As String
        Dim rValue As String = String.Empty
        Try
            rValue = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select ColumnDescription from TSPL_SCREEN_Grid_CONTROL_MASTER where ColumnName='" & code & "' and ProgramCode='" & FormName & "' and GridName='" & gvName & "'"))
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return rValue
    End Function

    Public Shared Function getGridCodeFromFieldName(fieldName As String, gvName As String, FormName As String) As String
        Dim rValue As String = String.Empty
        Try
            rValue = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select ColumnName  from TSPL_SCREEN_Grid_CONTROL_MASTER where ColumnDescription='" & fieldName & "' and ProgramCode='" & FormName & "' and GridName='" & gvName & "'"))
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return rValue
    End Function

    Public Shared Function SaveData(ByVal Obj As clsCustomFieldMapping) As Boolean

        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If (Obj IsNot Nothing) Then
                Dim qry As String = "select count(*) from TSPL_CUSTOM_FIELD_MAPPING where Program_Code='" + Obj.Program_Code + "' and Custom_Field_Code='" & Obj.Custom_Field_Code & "'"
                Dim cnt As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))
                Dim isNewEnry As Boolean = IIf(cnt > 0, False, True)
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Program_Code", Obj.Program_Code)
                clsCommon.AddColumnsForChange(coll, "Custom_Field_Code", Obj.Custom_Field_Code)
                clsCommon.AddColumnsForChange(coll, "Is_Mandatory", IIf(Obj.Is_Mandatory, 1, 0))
                clsCommon.AddColumnsForChange(coll, "Is_For_Detail_Level", IIf(Obj.Is_For_Detail_Level, 1, 0))
                clsCommon.AddColumnsForChange(coll, "Is_For_Print", IIf(Obj.Is_For_Print, 1, 0))
                clsCommon.AddColumnsForChange(coll, "Default_Value", Obj.Default_Value)
                clsCommon.AddColumnsForChange(coll, "MethodCode", clsCommon.myCstr(Obj.MethodCode), True)
                clsCommon.AddColumnsForChange(coll, "MethodArg", clsCommon.myCstr(Obj.MethodArg), True)
                clsCommon.AddColumnsForChange(coll, "SNo", Obj.SNo)
                clsCommon.AddColumnsForChange(coll, "Is_Validate", IIf(Obj.Is_Validate = True, 1, 0))
                clsCommon.AddColumnsForChange(coll, "Is_CalCulated_Column", Obj.Is_CalCulated_Column)
                clsCommon.AddColumnsForChange(coll, "CalculationExpression", clsCommon.myCstr(Obj.CalculationExpression))
                clsCommon.AddColumnsForChange(coll, "MaxLength", clsCommon.myCdbl(Obj.MaxLength))
                clsCommon.AddColumnsForChange(coll, "ReferenceTableName", clsCommon.myCstr(Obj.ReferenceTableName))
                clsCommon.AddColumnsForChange(coll, "ReferenceFieldName", clsCommon.myCstr(Obj.ReferenceFieldName))
                clsCommon.AddColumnsForChange(coll, "IsSourceFromTable", clsCommon.myCdbl(Obj.IsSourceFromTable))
                clsCommon.AddColumnsForChange(coll, "IsSourceFromValueList", clsCommon.myCdbl(Obj.IsSourceFromValueList))
                clsCommon.AddColumnsForChange(coll, "IsUnique", clsCommon.myCdbl(Obj.IsUnique))
                clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                If isNewEnry Then
                    clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "comp_code", objCommonVar.CurrentCompanyCode)
                    clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CUSTOM_FIELD_MAPPING", OMInsertOrUpdate.Insert, "", trans)
                Else
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CUSTOM_FIELD_MAPPING", OMInsertOrUpdate.Update, " Program_Code='" & Obj.Program_Code & "' and Custom_Field_Code='" & Obj.Custom_Field_Code & "' ", trans)
                End If
                clsCustomFieldMappingValueList.SaveData(Obj.Custom_Field_Code, Obj.Program_Code, Obj.arrValueList, trans)
                clsCustomFieldMappingConditions.SaveData(Obj.Custom_Field_Code, Obj.Program_Code, Obj.arrConditions, trans)

            End If
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try

        Return True
    End Function

    ' Will not be used next, In Place of this above method having single value object will be used
    Public Shared Function SaveData(ByVal strProgramCode As String, ByVal Arr As List(Of clsCustomFieldMapping)) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim qry As String = "delete from TSPL_CUSTOM_FIELD_MAPPING where Program_Code='" + strProgramCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
                Dim counter As Integer = 1
                For Each obj As clsCustomFieldMapping In Arr
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Program_Code", strProgramCode)
                    clsCommon.AddColumnsForChange(coll, "Custom_Field_Code", obj.Custom_Field_Code)
                    clsCommon.AddColumnsForChange(coll, "Is_Mandatory", IIf(obj.Is_Mandatory, 1, 0))
                    clsCommon.AddColumnsForChange(coll, "Is_For_Detail_Level", IIf(obj.Is_For_Detail_Level, 1, 0))
                    clsCommon.AddColumnsForChange(coll, "Is_For_Print", IIf(obj.Is_For_Print, 1, 0))
                    clsCommon.AddColumnsForChange(coll, "Default_Value", obj.Default_Value)
                    clsCommon.AddColumnsForChange(coll, "SNo", counter)
                    counter += 1
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CUSTOM_FIELD_MAPPING", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try

        Return True
    End Function
    '' End Of SaveData Function which is not to be used

    Public Shared Function GetData(ByVal strProgramCode As String, ByVal isHeadDetailAll As String) As List(Of clsCustomFieldMapping)
        Dim Arr As List(Of clsCustomFieldMapping) = Nothing
        Dim qry As String = "SELECT TSPL_CUSTOM_FIELD_MAPPING.*,TSPL_CUSTOM_FIELD_HEAD.Name as Custom_Field_Name,TSPL_CUSTOM_FIELD_HEAD.Is_Validate,TSPL_CUSTOM_FIELD_HEAD.Type FROM TSPL_CUSTOM_FIELD_MAPPING left outer join TSPL_CUSTOM_FIELD_HEAD on TSPL_CUSTOM_FIELD_HEAD.Code=TSPL_CUSTOM_FIELD_MAPPING.Custom_Field_Code  where Program_Code='" + strProgramCode + "' "
        If clsCommon.CompairString(isHeadDetailAll, "H") = CompairStringResult.Equal Then
            qry += " and Is_For_Detail_Level='0'"
        ElseIf clsCommon.CompairString(isHeadDetailAll, "D") = CompairStringResult.Equal Then
            qry += " and Is_For_Detail_Level='1'"
        End If

        qry += "order by SNo"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            Arr = New List(Of clsCustomFieldMapping)
            Dim objTr As clsCustomFieldMapping
            For Each dr As DataRow In dt.Rows
                objTr = New clsCustomFieldMapping
                objTr.SNo = clsCommon.myCstr(dr("SNo"))
                objTr.Program_Code = clsCommon.myCstr(dr("Program_Code"))
                objTr.Custom_Field_Code = clsCommon.myCstr(dr("Custom_Field_Code"))
                objTr.Custom_Field_Name = clsCommon.myCstr(dr("Custom_Field_Name"))
                objTr.Is_Mandatory = IIf(clsCommon.myCdbl(clsCommon.myCdbl(dr("Is_Mandatory"))) = 1, True, False)
                objTr.Type = clsCommon.myCdbl(clsCommon.myCdbl(dr("Type")))
                objTr.Default_Value = clsCommon.myCstr(dr("Default_Value"))
                objTr.MethodCode = clsCommon.myCstr(dr("MethodCode"))
                objTr.MethodArg = clsCommon.myCstr(dr("MethodArg"))
                objTr.SNo = clsCommon.myCdbl(dr("SNo"))
                objTr.Is_Validate = IIf(clsCommon.myCdbl(clsCommon.myCdbl(dr("Is_Validate"))) = 1, True, False)
                objTr.Is_For_Detail_Level = IIf(clsCommon.myCdbl(clsCommon.myCdbl(dr("Is_For_Detail_Level"))) = 1, True, False)
                objTr.Is_For_Print = IIf(clsCommon.myCdbl(clsCommon.myCdbl(dr("Is_For_Print"))) = 1, True, False)
                Arr.Add(objTr)
            Next
        End If
        Return Arr
    End Function

    Public Shared Function DeleteData(strCustomFieldCode As String, strProgramCode As String, Optional trans As SqlTransaction = Nothing) As Boolean
        Try
            Dim qry As String = "delete from TSPL_CUSTOM_FIELD_MAPPING where Program_Code='" & strProgramCode & "' and Custom_Field_Code='" & strCustomFieldCode & "'"
            clsCustomFieldMappingConditions.DeleteData(strCustomFieldCode, strProgramCode, trans)
            clsCustomFieldMappingValueList.deleteData(strCustomFieldCode, strProgramCode, trans)
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function GetData(strCustomFieldCode As String, ByVal strProgramCode As String, Optional trans As SqlTransaction = Nothing) As clsCustomFieldMapping
        Dim obj As clsCustomFieldMapping = Nothing
        Try
            Dim qry As String = "SELECT TSPL_CUSTOM_FIELD_MAPPING.*,TSPL_CUSTOM_FIELD_HEAD.Name as Custom_Field_Name,TSPL_CUSTOM_FIELD_HEAD.FieldName as Custom_Field_Field_Name,TSPL_CUSTOM_FIELD_HEAD.Is_Validate,TSPL_CUSTOM_FIELD_HEAD.Type FROM TSPL_CUSTOM_FIELD_MAPPING left outer join TSPL_CUSTOM_FIELD_HEAD on TSPL_CUSTOM_FIELD_HEAD.Code=TSPL_CUSTOM_FIELD_MAPPING.Custom_Field_Code  where Program_Code='" + strProgramCode + "'  and Custom_Field_Code='" & strCustomFieldCode & "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj = New clsCustomFieldMapping
                Dim dr As DataRow = dt.Rows(0)
                obj = New clsCustomFieldMapping
                obj.SNo = clsCommon.myCstr(dr("SNo"))
                obj.Program_Code = clsCommon.myCstr(dr("Program_Code"))
                obj.Custom_Field_Code = clsCommon.myCstr(dr("Custom_Field_Code"))
                obj.Custom_Field_Name = clsCommon.myCstr(dr("Custom_Field_Name"))
                obj.Custom_Field_Field_Name = clsCommon.myCstr(dr("Custom_Field_Field_Name"))
                obj.Is_Mandatory = IIf(clsCommon.myCdbl(dr("Is_Mandatory")) = 1, True, False)
                obj.Type = clsCommon.myCdbl(dr("Type"))
                obj.Default_Value = clsCommon.myCstr(dr("Default_Value"))
                obj.SNo = clsCommon.myCdbl(dr("SNo"))
                obj.Is_Validate = IIf(clsCommon.myCdbl(dr("Is_Validate")) = 1, True, False)
                obj.Is_For_Detail_Level = IIf(clsCommon.myCdbl(dr("Is_For_Detail_Level")) = 1, True, False)
                obj.Is_For_Print = IIf(clsCommon.myCdbl(dr("Is_For_Print")) = 1, True, False)
                obj.Is_CalCulated_Column = clsCommon.myCdbl(dr("Is_CalCulated_Column"))
                obj.CalculationExpression = clsCommon.myCstr(dr("CalculationExpression"))
                obj.MaxLength = clsCommon.myCdbl(dr("MaxLength"))
                obj.ReferenceTableName = clsCommon.myCstr(dr("ReferenceTableName"))
                obj.ReferenceFieldName = clsCommon.myCstr(dr("ReferenceFieldName"))
                obj.MethodCode = clsCommon.myCstr(dr("MethodCode"))
                obj.MethodArg = clsCommon.myCstr(dr("MethodArg"))
                obj.IsSourceFromTable = clsCommon.myCdbl(dr("IsSourceFromTable"))
                obj.IsSourceFromValueList = clsCommon.myCdbl(dr("IsSourceFromValueList"))
                obj.IsUnique = clsCommon.myCdbl(dr("IsUnique"))
                obj.arrConditions = clsCustomFieldMappingConditions.GetData(obj.Custom_Field_Code, obj.Program_Code, trans)
                obj.arrValueList = clsCustomFieldMappingValueList.getData(obj.Custom_Field_Code, obj.Program_Code, trans)

            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return obj
    End Function
    Public Shared Function GetData(ByVal strProgramCode As String, ByVal isHeadOnly As Boolean, Optional trans As SqlTransaction = Nothing) As List(Of clsCustomFieldMapping)
        Dim obj As clsCustomFieldMapping = Nothing
        Dim arr As List(Of clsCustomFieldMapping) = Nothing
        Try
            Dim qry As String = "SELECT TSPL_CUSTOM_FIELD_MAPPING.*,TSPL_CUSTOM_FIELD_HEAD.Name as Custom_Field_Name,TSPL_CUSTOM_FIELD_HEAD.FieldName as Custom_Field_Field_Name,TSPL_CUSTOM_FIELD_HEAD.Is_Validate,TSPL_CUSTOM_FIELD_HEAD.Type FROM TSPL_CUSTOM_FIELD_MAPPING left outer join TSPL_CUSTOM_FIELD_HEAD on TSPL_CUSTOM_FIELD_HEAD.Code=TSPL_CUSTOM_FIELD_MAPPING.Custom_Field_Code  where Program_Code='" + strProgramCode + "' "
            If isHeadOnly Then
                qry += " and Is_For_Detail_Level='0'"
            Else
                qry += " and Is_For_Detail_Level='1'"
            End If
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                arr = New List(Of clsCustomFieldMapping)
                For Each dr As DataRow In dt.Rows
                    obj = New clsCustomFieldMapping
                    'Dim dr As DataRow = dt.Rows(0)
                    obj = New clsCustomFieldMapping
                    obj.SNo = clsCommon.myCstr(dr("SNo"))
                    obj.Program_Code = clsCommon.myCstr(dr("Program_Code"))
                    obj.Custom_Field_Code = clsCommon.myCstr(dr("Custom_Field_Code"))
                    obj.Custom_Field_Name = clsCommon.myCstr(dr("Custom_Field_Name"))
                    obj.Custom_Field_Field_Name = clsCommon.myCstr(dr("Custom_Field_Field_Name"))
                    obj.Is_Mandatory = IIf(clsCommon.myCdbl(clsCommon.myCdbl(dr("Is_Mandatory"))) = 1, True, False)
                    obj.Type = clsCommon.myCdbl(clsCommon.myCdbl(dr("Type")))
                    obj.Default_Value = clsCommon.myCstr(dr("Default_Value"))
                    obj.SNo = clsCommon.myCdbl(dr("SNo"))
                    obj.Is_Validate = IIf(clsCommon.myCdbl(clsCommon.myCdbl(dr("Is_Validate"))) = 1, True, False)
                    obj.Is_For_Detail_Level = IIf(clsCommon.myCdbl(clsCommon.myCdbl(dr("Is_For_Detail_Level"))) = 1, True, False)
                    obj.Is_For_Print = IIf(clsCommon.myCdbl(clsCommon.myCdbl(dr("Is_For_Print"))) = 1, True, False)
                    obj.Is_CalCulated_Column = clsCommon.myCdbl(clsCommon.myCdbl(dr("Is_CalCulated_Column")))
                    obj.CalculationExpression = clsCommon.myCstr(dr("CalculationExpression"))
                    obj.MaxLength = clsCommon.myCdbl(clsCommon.myCdbl(dr("MaxLength")))
                    obj.ReferenceTableName = clsCommon.myCstr(dr("ReferenceTableName"))
                    obj.ReferenceFieldName = clsCommon.myCstr(dr("ReferenceFieldName"))
                    obj.MethodCode = clsCommon.myCstr(dr("MethodCode"))
                    obj.MethodArg = clsCommon.myCstr(dr("MethodArg"))
                    obj.IsSourceFromTable = clsCommon.myCdbl(clsCommon.myCdbl(dr("IsSourceFromTable")))
                    obj.IsSourceFromValueList = clsCommon.myCdbl(clsCommon.myCdbl(dr("IsSourceFromValueList")))
                    obj.IsUnique = clsCommon.myCdbl(clsCommon.myCdbl(dr("IsUnique")))
                    obj.arrConditions = clsCustomFieldMappingConditions.GetData(obj.Custom_Field_Code, obj.Program_Code, trans)
                    obj.arrValueList = clsCustomFieldMappingValueList.getData(obj.Custom_Field_Code, obj.Program_Code, trans)
                    arr.Add(obj)
                Next
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return arr
    End Function

End Class


Public Class clsCustomFieldValues
#Region "Variables"
    Public Program_Code As String = Nothing
    Public Transaction_Code As String = Nothing
    Public Custom_Field_Code As String = Nothing
    Public Value As String = Nothing
    Public Line_N0_For_Detail As Integer = 0
    Public ValueDescription As String = Nothing ''Not a table filed
#End Region

    Public Shared Function SaveData(ByVal strProgramCode As String, ByVal strTransactionCode As String, ByVal Arr As List(Of clsCustomFieldValues), ByVal trans As SqlTransaction) As Boolean
        Try
            Dim qry As String = "delete from TSPL_CUSTOM_FIELD_VALUES where Program_Code='" + strProgramCode + "' and Transaction_Code='" + strTransactionCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
                Dim counter As Integer = 1
                For Each obj As clsCustomFieldValues In Arr
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Program_Code", strProgramCode)
                    clsCommon.AddColumnsForChange(coll, "Transaction_Code", strTransactionCode)
                    clsCommon.AddColumnsForChange(coll, "Custom_Field_Code", obj.Custom_Field_Code)
                    clsCommon.AddColumnsForChange(coll, "Value", obj.Value)
                    clsCommon.AddColumnsForChange(coll, "Line_N0_For_Detail", obj.Line_N0_For_Detail)
                    counter += 1
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CUSTOM_FIELD_VALUES", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function DeleteData(ByVal strProgramCode As String, ByVal strTransactionCode As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim qry As String = "delete from TSPL_CUSTOM_FIELD_VALUES where Program_Code='" + strProgramCode + "' and Transaction_Code='" + strTransactionCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function getQueryStringForRPT(ByVal strProgramCode As String, ByVal strTransactionCode As String, ByVal trans As SqlTransaction) As String
        Try
            Dim qry As String = "delete from TSPL_CUSTOM_FIELD_VALUES where Program_Code='" + strProgramCode + "' and Transaction_Code='" + strTransactionCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function GetData(ByVal strProgramCode As String, ByVal strTransactionCode As String, ByVal isForDetail As Boolean) As List(Of clsCustomFieldValues)
        Dim Arr As List(Of clsCustomFieldValues) = Nothing
        Dim qry As String = "SELECT TSPL_CUSTOM_FIELD_VALUES.*,TSPL_CUSTOM_FIELD_DETAIL.Description as ValueDescription  FROM TSPL_CUSTOM_FIELD_VALUES  left outer join TSPL_CUSTOM_FIELD_DETAIL on TSPL_CUSTOM_FIELD_DETAIL.Custom_Field_Code=TSPL_CUSTOM_FIELD_VALUES.Custom_Field_Code  and TSPL_CUSTOM_FIELD_DETAIL.Value=TSPL_CUSTOM_FIELD_VALUES.Value where Program_Code='" + strProgramCode + "' and Transaction_Code='" + strTransactionCode + "' "
        If isForDetail Then
            qry += " and Line_N0_For_Detail >0"
        Else
            qry += " and Line_N0_For_Detail =0"
        End If

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            Arr = New List(Of clsCustomFieldValues)
            Dim objTr As clsCustomFieldValues
            For Each dr As DataRow In dt.Rows
                objTr = New clsCustomFieldValues
                objTr.Program_Code = clsCommon.myCstr(dr("Program_Code"))
                objTr.Transaction_Code = clsCommon.myCstr(dr("Transaction_Code"))
                objTr.Custom_Field_Code = clsCommon.myCstr(dr("Custom_Field_Code"))
                objTr.Value = clsCommon.myCstr(dr("Value"))
                objTr.ValueDescription = clsCommon.myCstr(dr("ValueDescription"))
                objTr.Line_N0_For_Detail = clsCommon.myCstr(dr("Line_N0_For_Detail"))
                Arr.Add(objTr)
            Next
        End If
        Return Arr
    End Function
End Class

Public Class clsCustomFieldGrid
    Public iCode As String = ""
    Public iDesc As String = ""
    Public Shared Sub getFinderForCustomFieldGrid(ByVal gv As common.UserControls.MyRadGridView, ByVal colName As String, ByVal pCode As String)
        Dim objCF As clsCustomFieldGrid = New clsCustomFieldGrid()
        objCF = objCF.ShowFinder(pCode, colName)
        If objCF IsNot Nothing Then
            gv.CurrentRow.Cells(colName).Value = objCF.iCode
            gv.CurrentRow.Cells(colName & "DESC").Value = objCF.iDesc
        End If
    End Sub
    Private Function ShowFinder(ByVal pCode As String, ByVal CustomFieldCode As String) As clsCustomFieldGrid
        Dim s As String = ""
        Dim s2 As String = ""
        Dim obj As clsCustomFieldGrid = New clsCustomFieldGrid()
        Dim q As String = " select TSPL_CUSTOM_FIELD_DETAIL.Value as Value,TSPL_CUSTOM_FIELD_DETAIL.Description as Description    from TSPL_CUSTOM_FIELD_MAPPING  left outer join TSPL_CUSTOM_FIELD_HEAD on TSPL_CUSTOM_FIELD_HEAD.Code=TSPL_CUSTOM_FIELD_MAPPING.Custom_Field_Code   left outer join TSPL_CUSTOM_FIELD_DETAIL on TSPL_CUSTOM_FIELD_DETAIL.Custom_Field_Code=TSPL_CUSTOM_FIELD_HEAD.Code "
        Dim whrCls As String = " where TSPL_CUSTOM_FIELD_MAPPING.Program_Code='" & pCode & "' and TSPL_CUSTOM_FIELD_MAPPING.Custom_Field_Code='" & CustomFieldCode & "'"
        q = q & whrCls
        Dim dt As DataRow = clsCommon.ShowSelectFormForRow(pCode & CustomFieldCode, q)
        If dt IsNot Nothing Then
            s = clsCommon.myCstr(dt("Value"))
            s2 = clsCommon.myCstr(dt("Description"))
            obj.iCode = s
            obj.iDesc = s2
        End If

        Return obj

    End Function
    Public Shared ArrCustomFields As List(Of clsCustomFieldMapping) = New List(Of clsCustomFieldMapping)
    Public Shared FormId As String = String.Empty
    Public Shared ControlsInvolvedinCalculation As List(Of String) = New List(Of String)
    Public Shared WithEvents Evaluator1 As New Evaluator
    Public Shared grid As New common.UserControls.MyRadGridView
    Private Shared Sub Evaluator1_GetVariable(ByVal name As String, Row_Index As Integer, ByRef value As Object) Handles Evaluator1.GetVariableGrid
        Try
            If clsCommon.myLen(name) > 0 AndAlso Row_Index >= 0 Then
                value = clsCommon.myCdbl(grid.Rows(Row_Index).Cells(clsCustomFieldMapping.getGridCodeFromFieldName(name, grid.Name, FormId)).Value)
            End If

        Catch ex As Exception
            value = "Error: " & ex.Message
        End Try
    End Sub
    Public Shared Sub Grid_CellValueChanged(sender As Object, e As GridViewCellEventArgs)
        Dim dt As DataTable = clsDBFuncationality.GetDataTable("select calculationExpression,Custom_Field_Code from TSPL_CUSTOM_FIELD_MAPPING where Program_Code='" & FormId & "' and isnull(calculationExpression,'')<>''")
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                If clsCommon.myCstr(dt.Rows(i)("calculationExpression")).Contains("#" & clsCustomFieldMapping.getGridFieldNameFromCode(e.Column.Name, grid.Name, FormId) & "#") Then
                    Evaluator1.type = 1
                    Evaluator1.Rownum = e.RowIndex
                    grid.Rows(e.RowIndex).Cells(clsCommon.myCstr(dt.Rows(i)("Custom_Field_Code"))).Value = clsCommon.myCdbl(Evaluator1.Eval(dt.Rows(i)("calculationExpression")))
                End If
            Next
        End If
    End Sub
    Public Shared Sub LoadBlankGrid(ByVal gv1 As RadGridView, ByVal ArrDetailFields As List(Of clsCustomFieldMapping), Optional Report_Id As String = "")
        If ArrDetailFields IsNot Nothing AndAlso ArrDetailFields.Count > 0 AndAlso clsCommon.myLen(Report_Id) > 0 Then

            ControlsInvolvedinCalculation = ucCustomFields.getControlInvolvedInCalculation(Report_Id)
            If ControlsInvolvedinCalculation IsNot Nothing AndAlso ControlsInvolvedinCalculation.Count > 0 Then
                FormId = Report_Id
                ArrCustomFields = ArrDetailFields
                grid = gv1
                AddHandler gv1.CellValueChanged, AddressOf clsCustomFieldGrid.Grid_CellValueChanged
            End If
            For Each obj As clsCustomFieldMapping In ArrDetailFields
                If obj.Is_Validate Then
                    'Dim qry As String = "select Value,Description from TSPL_CUSTOM_FIELD_DETAIL where Custom_Field_Code='" + obj.Custom_Field_Code + "' order by SNo"
                    'Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                    'Dim dr As DataRow = dt.NewRow()
                    'dr("Value") = ""
                    'dt.Rows.InsertAt(dr, 0)

                    'Dim repoItem As GridViewMultiComboBoxColumn = New GridViewMultiComboBoxColumn()
                    'repoItem.FormatString = ""
                    'repoItem.HeaderText = obj.Custom_Field_Name
                    'repoItem.Name = obj.Custom_Field_Code
                    'repoItem.Width = 100
                    'repoItem.ReadOnly = False
                    'repoItem.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
                    'repoItem.DataSource = dt
                    'repoItem.ValueMember = "Value"
                    'repoItem.DisplayMember = "Description"
                    'gv1.MasterTemplate.Columns.Add(repoItem)

                    Dim repoItem As GridViewTextBoxColumn = New GridViewTextBoxColumn()
                    repoItem.FormatString = ""
                    repoItem.HeaderText = obj.Custom_Field_Name
                    repoItem.Name = obj.Custom_Field_Code
                    repoItem.Width = 100
                    repoItem.ReadOnly = False
                    repoItem.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
                    repoItem.HeaderImage = Global.XpertERPEngine.My.Resources.search4
                    repoItem.TextImageRelation = TextImageRelation.TextBeforeImage
                    'repoItem.Tag=obj.Custom_Field_Field_Name 
                    If obj.Is_CalCulated_Column = 1 Then
                        repoItem.ReadOnly = True
                    End If
                    gv1.MasterTemplate.Columns.Add(repoItem)
                    ' gv1.MasterTemplate.Columns(obj.Custom_Field_Code).Tag = "CFLD"
                    gv1.MasterTemplate.Columns(obj.Custom_Field_Code).FieldName = "_CFLD_" & obj.Custom_Field_Code

                    Dim repoItemDesc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
                    repoItemDesc.FormatString = ""
                    repoItemDesc.HeaderText = obj.Custom_Field_Name & " Description"
                    repoItemDesc.Name = obj.Custom_Field_Code & "DESC"
                    repoItemDesc.Width = 100
                    repoItemDesc.ReadOnly = True
                    repoItemDesc.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
                    If obj.Is_CalCulated_Column = 1 Then
                        repoItemDesc.ReadOnly = True
                    End If
                    gv1.MasterTemplate.Columns.Add(repoItemDesc)


                    'Dim repoItem As GridViewComboBoxColumn = New GridViewComboBoxColumn()
                    'repoItem.FormatString = ""
                    'repoItem.HeaderText = obj.Custom_Field_Name
                    'repoItem.Name = obj.Custom_Field_Code
                    'repoItem.Width = 100
                    'repoItem.ReadOnly = False
                    'repoItem.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
                    'repoItem.DataSource = dt
                    'repoItem.ValueMember = "Value"
                    'repoItem.DisplayMember = "Description"
                    'gv1.MasterTemplate.Columns.Add(repoItem)
                ElseIf obj.Type = EnumCustomFieldType.TextType Then
                    Dim repoItem As GridViewTextBoxColumn = New GridViewTextBoxColumn()
                    repoItem.HeaderText = obj.Custom_Field_Name
                    repoItem.Name = obj.Custom_Field_Code
                    repoItem.Width = 100
                    repoItem.ReadOnly = False
                    If obj.Is_CalCulated_Column = 1 Then
                        repoItem.ReadOnly = True
                    End If
                    gv1.MasterTemplate.Columns.Add(repoItem)
                ElseIf obj.Type = EnumCustomFieldType.NumberType Then
                    Dim repoItem As GridViewDecimalColumn = New GridViewDecimalColumn()
                    repoItem.WrapText = True
                    repoItem.ReadOnly = False
                    repoItem.FormatString = ""
                    repoItem.HeaderText = obj.Custom_Field_Name
                    repoItem.Name = obj.Custom_Field_Code
                    repoItem.Width = 100
                    repoItem.Minimum = 0
                    If obj.Is_CalCulated_Column = 1 Then
                        repoItem.ReadOnly = True
                    End If
                    repoItem.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
                    gv1.MasterTemplate.Columns.Add(repoItem)
                ElseIf obj.Type = EnumCustomFieldType.DateType Then
                    Dim repoItem As GridViewDateTimeColumn = New GridViewDateTimeColumn()
                    repoItem.Format = DateTimePickerFormat.Custom
                    repoItem.CustomFormat = "dd-MM-yyyy"
                    repoItem.HeaderText = obj.Custom_Field_Name
                    repoItem.FormatString = "{0:d}"
                    repoItem.Name = obj.Custom_Field_Code
                    repoItem.WrapText = True
                    repoItem.ReadOnly = False
                    If obj.Is_CalCulated_Column = 1 Then
                        repoItem.ReadOnly = True
                    End If
                    repoItem.Width = 100
                    gv1.MasterTemplate.Columns.Add(repoItem)
                ElseIf obj.Type = EnumCustomFieldType.CheckType Then
                    Dim repoItem As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
                    repoItem.HeaderText = obj.Custom_Field_Name
                    repoItem.Name = obj.Custom_Field_Code
                    repoItem.ReadOnly = False
                    repoItem.IsVisible = True
                    If obj.Is_CalCulated_Column = 1 Then
                        repoItem.ReadOnly = True
                    End If
                    repoItem.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
                    repoItem.Width = 100
                    gv1.MasterTemplate.Columns.Add(repoItem)
                End If
            Next
        End If

    End Sub

    Public Shared Sub GetData(ByRef arr As List(Of clsCustomFieldValues), ByVal gv1 As RadGridView, ByVal ArrDetailFields As List(Of clsCustomFieldMapping), ByVal strConditionalCol As String)
        If ArrDetailFields IsNot Nothing AndAlso ArrDetailFields.Count > 0 Then
            For Each objSetting As clsCustomFieldMapping In ArrDetailFields
                For ii As Integer = 0 To gv1.Columns.Count - 1
                    If clsCommon.CompairString(gv1.Columns(ii).Name, objSetting.Custom_Field_Code) = CompairStringResult.Equal Then
                        For jj As Integer = 0 To gv1.Rows.Count - 1
                            If clsCommon.myLen(gv1.Rows(jj).Cells(strConditionalCol).Value) > 0 Then
                                If clsCommon.myLen(gv1.Rows(jj).Cells(objSetting.Custom_Field_Code).Value) > 0 Then
                                    Dim obj As clsCustomFieldValues = New clsCustomFieldValues()
                                    obj.Custom_Field_Code = objSetting.Custom_Field_Code
                                    If objSetting.Type = EnumCustomFieldType.CheckType Then
                                        obj.Value = clsCommon.myCBool(gv1.Rows(jj).Cells(objSetting.Custom_Field_Code).Value)
                                    ElseIf objSetting.Type = EnumCustomFieldType.DateType Then
                                        obj.Value = clsCommon.GetPrintDate(clsCommon.myCDate(gv1.Rows(jj).Cells(objSetting.Custom_Field_Code).Value), "dd/MMM/yyyy")
                                    ElseIf objSetting.Type = EnumCustomFieldType.NumberType Then
                                        obj.Value = clsCommon.myCstr(clsCommon.myCdbl(gv1.Rows(jj).Cells(objSetting.Custom_Field_Code).Value))
                                    ElseIf objSetting.Type = EnumCustomFieldType.TextType Then
                                        obj.Value = clsCommon.myCstr(gv1.Rows(jj).Cells(objSetting.Custom_Field_Code).Value)
                                    End If
                                    obj.Value = clsCommon.myCstr(gv1.Rows(jj).Cells(objSetting.Custom_Field_Code).Value)
                                    obj.Line_N0_For_Detail = jj + 1
                                    arr.Add(obj)
                                End If
                            End If
                        Next
                        'Exit For
                    End If
                Next
            Next
        End If
    End Sub


    Public Shared Sub FillDataInGrid(ByVal strTransactionCode As String, ByVal strProgramCode As String, ByVal gv1 As RadGridView)
        Dim arr As List(Of clsCustomFieldValues) = clsCustomFieldValues.GetData(strProgramCode, strTransactionCode, True)
        If arr IsNot Nothing AndAlso arr.Count > 0 Then
            For Each obj As clsCustomFieldValues In arr
                For jj As Integer = 0 To gv1.Rows.Count - 1
                    If obj.Line_N0_For_Detail = jj + 1 Then
                        Try
                            gv1.Rows(jj).Cells(obj.Custom_Field_Code).Value = obj.Value
                        Catch ex As Exception
                        End Try
                    End If
                Next
            Next
        End If
    End Sub

End Class
Public Class clsCustomFieldConditions
#Region "Variables"
    Public Custom_Field_Code As String = Nothing
    Public SNo As String = Nothing
    Public LogicalOperator As String = Nothing
    Public ConditionalOperator As Integer = 0
    Public ConditionValue As String = Nothing
#End Region

    Public Shared Function SaveData(ByVal strCustomFieldCode As String, ByVal Arr As List(Of clsCustomFieldConditions), Optional trans As SqlTransaction = Nothing) As Boolean
        Try
            Dim qry As String = "delete from TSPL_CUSTOM_FIELD_CONDITIONS where Custom_Field_Code='" + strCustomFieldCode + "' "
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
                Dim counter As Integer = 1
                For Each obj As clsCustomFieldConditions In Arr
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Custom_Field_Code", strCustomFieldCode)
                    clsCommon.AddColumnsForChange(coll, "SNo", obj.SNo)
                    clsCommon.AddColumnsForChange(coll, "LogicalOperator", obj.LogicalOperator)
                    clsCommon.AddColumnsForChange(coll, "ConditionalOperator", obj.ConditionalOperator)
                    clsCommon.AddColumnsForChange(coll, "ConditionValue", obj.ConditionValue)
                    counter += 1
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CUSTOM_FIELD_CONDITIONS", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function DeleteData(ByVal strCustomFieldCode As String, Optional ByVal trans As SqlTransaction = Nothing) As Boolean
        Try
            Dim qry As String = "delete from TSPL_CUSTOM_FIELD_CONDITIONS where Custom_Field_Code='" + strCustomFieldCode + "' "
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function GetData(ByVal strCustomFieldCode As String, Optional trans As SqlTransaction = Nothing) As List(Of clsCustomFieldConditions)
        Dim Arr As List(Of clsCustomFieldConditions) = Nothing
        Dim qry As String = "SELECT * from TSPL_CUSTOM_FIELD_CONDITIONS where Custom_Field_Code='" & strCustomFieldCode & "'"

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            Arr = New List(Of clsCustomFieldConditions)
            Dim objTr As clsCustomFieldConditions
            For Each dr As DataRow In dt.Rows
                objTr = New clsCustomFieldConditions
                objTr.Custom_Field_Code = clsCommon.myCstr(dr("Custom_Field_Code"))
                objTr.SNo = clsCommon.myCstr(dr("SNo"))
                objTr.ConditionalOperator = clsCommon.myCdbl(dr("ConditionalOperator"))
                objTr.LogicalOperator = clsCommon.myCstr(dr("LogicalOperator"))
                objTr.ConditionValue = clsCommon.myCstr(dr("ConditionValue"))
                Arr.Add(objTr)
            Next
        End If
        Return Arr
    End Function

End Class

Public Class clsCustomFieldMappingConditions
#Region "Variables"
    Public Program_Code As String = String.Empty
    Public Custom_Field_Code As String = Nothing
    Public SNo As String = Nothing
    Public LogicalOperator As String = Nothing
    Public ConditionalOperator As Integer = 0
    Public ConditionValue As String = Nothing
#End Region

    Public Shared Function SaveData(ByVal strCustomFieldCode As String, strProgCode As String, ByVal Arr As List(Of clsCustomFieldMappingConditions), Optional trans As SqlTransaction = Nothing) As Boolean
        Try
            Dim qry As String = "delete from TSPL_CUSTOM_FIELD_Mapping_Conditions where Custom_Field_Code='" + strCustomFieldCode + "' and Program_Code='" & strProgCode & "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
                Dim counter As Integer = 1
                For Each obj As clsCustomFieldMappingConditions In Arr
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Program_Code", strProgCode)
                    clsCommon.AddColumnsForChange(coll, "Custom_Field_Code", strCustomFieldCode)
                    clsCommon.AddColumnsForChange(coll, "SNo", obj.SNo)
                    clsCommon.AddColumnsForChange(coll, "LogicalOperator", obj.LogicalOperator)
                    clsCommon.AddColumnsForChange(coll, "ConditionalOperator", obj.ConditionalOperator)
                    clsCommon.AddColumnsForChange(coll, "ConditionValue", obj.ConditionValue)
                    counter += 1
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CUSTOM_FIELD_Mapping_Conditions", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function DeleteData(ByVal strCustomFieldCode As String, strProgCode As String, Optional ByVal trans As SqlTransaction = Nothing) As Boolean
        Try
            Dim qry As String = "delete from TSPL_CUSTOM_FIELD_Mapping_Conditions where Custom_Field_Code='" + strCustomFieldCode + "'  and Program_Code='" & strProgCode & "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function GetData(ByVal strCustomFieldCode As String, strProgCode As String, Optional trans As SqlTransaction = Nothing) As List(Of clsCustomFieldMappingConditions)
        Dim Arr As List(Of clsCustomFieldMappingConditions) = Nothing
        Dim qry As String = "SELECT * from TSPL_CUSTOM_FIELD_Mapping_Conditions where Custom_Field_Code='" + strCustomFieldCode + "'  and Program_Code='" & strProgCode & "'"

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            Arr = New List(Of clsCustomFieldMappingConditions)
            Dim objTr As clsCustomFieldMappingConditions
            For Each dr As DataRow In dt.Rows
                objTr = New clsCustomFieldMappingConditions
                objTr.Program_Code = clsCommon.myCstr(dr("Program_Code"))
                objTr.Custom_Field_Code = clsCommon.myCstr(dr("Custom_Field_Code"))
                objTr.SNo = clsCommon.myCstr(dr("SNo"))
                objTr.ConditionalOperator = clsCommon.myCdbl(dr("ConditionalOperator"))
                objTr.LogicalOperator = clsCommon.myCstr(dr("LogicalOperator"))
                objTr.ConditionValue = clsCommon.myCstr(dr("ConditionValue"))
                Arr.Add(objTr)
            Next
        End If
        Return Arr
    End Function

End Class
Public Class clsCustomFieldMappingValueList
    Public Program_Code As String = String.Empty
    Public Custom_Field_Code As String = String.Empty
    Public SNo As Integer = 0
    Public Value As String = String.Empty
    Public Description As String = String.Empty
    Public Shared Function SaveData(ByVal strCode As String, Prog_Code As String, ByVal Arr As List(Of clsCustomFieldMappingValueList), Optional trans As SqlTransaction = Nothing) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsCustomFieldMappingValueList In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Custom_Field_Code", strCode)
                clsCommon.AddColumnsForChange(coll, "Program_Code", Prog_Code)
                clsCommon.AddColumnsForChange(coll, "Value", obj.Value)
                clsCommon.AddColumnsForChange(coll, "Description", obj.Description)
                clsCommon.AddColumnsForChange(coll, "SNo", obj.SNo)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CUSTOM_FIELD_Mapping_ManualValueList", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function

    Public Shared Function getData(strCode As String, strProgCode As String, Optional trans As SqlTransaction = Nothing) As List(Of clsCustomFieldMappingValueList)
        Dim arr As List(Of clsCustomFieldMappingValueList) = Nothing
        Try
            Dim obj As clsCustomFieldMappingValueList = Nothing
            Dim dt As DataTable = Nothing
            Dim qry As String = "select * from TSPL_CUSTOM_FIELD_Mapping_ManualValueList where Program_Code='" & strProgCode & "' and  Custom_Field_Code='" & strCode & "'"
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                arr = New List(Of clsCustomFieldMappingValueList)
                For i As Integer = 0 To dt.Rows.Count - 1
                    obj = New clsCustomFieldMappingValueList
                    obj.Custom_Field_Code = clsCommon.myCstr(dt.Rows(i)("Custom_Field_Code"))
                    obj.Program_Code = clsCommon.myCstr(dt.Rows(i)("Program_Code"))
                    obj.Value = clsCommon.myCstr(dt.Rows(i)("Value"))
                    obj.Description = clsCommon.myCstr(dt.Rows(i)("Description"))
                    obj.SNo = clsCommon.myCdbl(dt.Rows(i)("SNo"))
                    arr.Add(obj)
                Next
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return arr
    End Function

    Public Shared Function deleteData(strCode As String, strProgCode As String, Optional trans As SqlTransaction = Nothing) As Boolean
        Dim rValue As Boolean = False
        Try

            Dim qry As String = "delete from TSPL_CUSTOM_FIELD_Mapping_ManualValueList where Program_Code='" & strProgCode & "' and  Custom_Field_Code='" & strCode & "'"
            rValue = clsDBFuncationality.ExecuteNonQuery(qry, trans)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return rValue
    End Function

End Class