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

Public Class clsRCDFStanardizationTemp

    Public Shared Function PostData(ByVal strCode As String, ByVal arrLoc As String, ByVal VoucherNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim dt As DataTable = clsDBFuncationality.GetDataTable("select Doc_Date,Location_Code from TSPL_RCDF_STD where Doc_Code='" + strCode + "'", trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleProductionDairy, clsUserMgtCode.frmProcessProductionStandardization, clsCommon.myCstr(dt.Rows(0)("Location_Code")), clsCommon.myCDate(dt.Rows(0)("Doc_Date")), trans)
            End If

            CreateInventoryMovement(strCode, trans, VoucherNo)
            CreateJE(strCode, trans, VoucherNo)
            Dim qry As String = "update TSPL_RCDF_STD set Status='1',Posted_By='" + objCommonVar.CurrentUserCode + "',Posted_Date='" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt") + "' where Doc_Code='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception("Production Stdardization No [" + strCode + "]" + ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function CreateInventoryMovement(ByVal strCode As String, ByVal trans As SqlTransaction, ByVal VoucherNo As String)
        Dim obj As clsRCDFStanardization = clsRCDFStanardization.GetData(strCode, "", NavigatorType.Current, trans)
        If obj Is Nothing AndAlso clsCommon.myLen(obj.Doc_Code) <= 0 Then
            Throw New Exception("Invalid docuemnt  [" + strCode + "]")
        End If
        If obj.Status = ERPTransactionStatus.Approved Then
            Throw New Exception("Posted docuemnt  [" + obj.Doc_Code + "]")
        End If
        Dim ArrInventoryMovement As List(Of clsInventoryMovement) = New List(Of clsInventoryMovement)
        Dim ArrInventoryMovementNew As List(Of clsInventoryMovementNew) = New List(Of clsInventoryMovementNew)

        Dim objIMMilk As clsInventoryMovementNew
        Dim objIM As clsInventoryMovement


        Dim objCost As New MIlkComponentType
        Dim strItemType As String

        Dim AvgAmt As Decimal = 0
        Dim AvgFATPart As Decimal = 0
        Dim AvgSNFPart As Decimal = 0
        Dim AvgFATKGPart As Decimal = 0
        Dim AvgSNFKGPart As Decimal = 0

        Dim InFATPart As Decimal = 0
        Dim InSNFPart As Decimal = 0

        If (obj.ArrIssue IsNot Nothing AndAlso obj.ArrIssue.Count > 0) Then
            For Each objIssue As clsRCDFStanardizationIssue In obj.ArrIssue
                If clsCommon.CompairString(objIssue.Product_Type, "MI") = CompairStringResult.Equal Then
                    objIMMilk = New clsInventoryMovementNew
                    objIMMilk.InOut = "O"
                    objIMMilk.Location_Code = objIssue.Location_Code
                    objIMMilk.main_location = obj.Location_Code
                    objIMMilk.Item_Code = objIssue.Item_Code
                    objIMMilk.Item_Desc = clsItemMaster.GetItemName(objIssue.Item_Code, trans)
                    objIMMilk.Qty = objIssue.Qty
                    objIMMilk.UOM = objIssue.Unit_Code
                    objIMMilk.Source_Doc_No = obj.Doc_Code
                    objIMMilk.Source_Doc_Date = obj.Doc_Date

                    objIMMilk.CalculateAvgCost = False
                    objIMMilk.DonNotCalculateAvgFATSNFCost = True

                    objCost = clsInventoryMovementNew.GetAvgCost(objIssue.Product_Type, objIssue.Item_Code, objIssue.Location_Code, objIssue.Qty, objIssue.Unit_Code, objIssue.FAT_KG, objIssue.SNF_KG, obj.Doc_Date, clsCommon.GETSERVERDATE(trans), False, trans)
                    objIMMilk.Avg_Cost = objCost.FAT_Cost + objCost.SNF_Cost
                    objIMMilk.FIFO_Cost = objIMMilk.Avg_Cost
                    objIMMilk.LIFO_Cost = objIMMilk.Avg_Cost

                    objIMMilk.FAT_Per = objIssue.FAT
                    objIMMilk.SNF_Per = objIssue.SNF
                    objIMMilk.FAT_KG = objIssue.FAT_KG
                    objIMMilk.SNF_KG = objIssue.SNF_KG



                    '' UPDATE PRODUCTION COST
                    objIMMilk.Fat_Rate = clsCommon.myCDivide(objCost.FAT_Cost, objIssue.FAT_KG)
                    objIMMilk.SNF_Rate = clsCommon.myCDivide(objCost.SNF_Cost, objIssue.SNF_KG)
                    objIMMilk.Fat_Amt = objCost.FAT_Cost
                    objIMMilk.SNF_Amt = objCost.SNF_Cost


                    If clsCommon.CompairString(objIMMilk.InOut, "I") = CompairStringResult.Equal Then
                        objIMMilk.Basic_Cost = clsCommon.myCDivide(objIMMilk.Avg_Cost, objIssue.Qty)
                        objIMMilk.Net_Cost = objIMMilk.Avg_Cost
                    End If

                    strItemType = clsItemMaster.GetItemType(objIssue.Item_Code, trans)
                    If clsCommon.CompairString(strItemType, "R") = CompairStringResult.Equal Then
                        objIMMilk.ItemType = "RM"
                    ElseIf clsCommon.CompairString(strItemType, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(strItemType, "O") = CompairStringResult.Equal Then
                        objIMMilk.ItemType = "OT"
                    ElseIf clsCommon.CompairString(strItemType, "F") = CompairStringResult.Equal Then
                        objIMMilk.ItemType = "FT"
                    Else
                        objIMMilk.ItemType = strItemType
                    End If

                    objIMMilk.Batch_No = "Issue"
                    objIMMilk.MRP = 0
                    objIMMilk.Add_Cost = 0
                    objIMMilk.MRP = 0
                    objIMMilk.MFG_Date = obj.Doc_Date

                    ArrInventoryMovementNew.Add(objIMMilk)

                    AvgFATPart += objCost.FAT_Cost
                    AvgSNFPart += objCost.SNF_Cost
                    AvgFATKGPart += objIssue.FAT_KG
                    AvgSNFKGPart += objIssue.SNF_KG
                    AvgAmt += (objCost.FAT_Cost + objCost.SNF_Cost)
                Else
                    objIM = New clsInventoryMovement
                    objIM.Trans_Type = ""
                    objIM.InOut = "O"
                    objIM.Location_Code = objIssue.Location_Code
                    objIM.Item_Code = objIssue.Item_Code
                    objIM.Item_Desc = clsItemMaster.GetItemName(objIssue.Item_Code, trans)
                    objIM.Qty = objIssue.Qty
                    objIM.UOM = objIssue.Unit_Code
                    objIM.Source_Doc_No = obj.Doc_Code
                    objIM.Source_Doc_Date = obj.Doc_Date
                    objIM.CalculateAvgCost = False

                    objIM.FAT_Per = objIssue.FAT
                    objIM.SNF_Per = objIssue.SNF
                    objIM.FAT_KG = objIssue.FAT_KG
                    objIM.SNF_KG = objIssue.SNF_KG

                    objCost = clsInventoryMovementNew.GetAvgCost(objIssue.Product_Type, objIssue.Item_Code, objIssue.Location_Code, objIssue.Qty, objIssue.Unit_Code, objIssue.FAT_KG, objIssue.SNF_KG, obj.Doc_Date, clsCommon.GETSERVERDATE(trans), False, trans)

                    objIM.Avg_Cost = objCost.FAT_Cost + objCost.SNF_Cost
                    objIM.FIFO_Cost = objIM.Avg_Cost
                    objIM.LIFO_Cost = objIM.Avg_Cost

                    objIM.Fat_Rate = clsCommon.myCDivide(objCost.FAT_Cost, objIssue.FAT_KG)
                    objIM.SNF_Rate = clsCommon.myCDivide(objCost.SNF_Cost, objIssue.SNF_KG)

                    objIM.Fat_Amt = objCost.FAT_Cost
                    objIM.SNF_Amt = objCost.SNF_Cost



                    If clsCommon.CompairString(objIM.InOut, "I") = CompairStringResult.Equal Then
                        objIM.Basic_Cost = clsCommon.myCDivide(objIM.Avg_Cost, objIssue.Qty)
                        objIM.Net_Cost = objIM.Avg_Cost
                    End If
                    strItemType = clsItemMaster.GetItemType(objIssue.Item_Code, trans)
                    If clsCommon.CompairString(strItemType, "R") = CompairStringResult.Equal Then
                        objIM.ItemType = "RM"
                    ElseIf clsCommon.CompairString(strItemType, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(strItemType, "O") = CompairStringResult.Equal Then
                        objIM.ItemType = "OT"
                    ElseIf clsCommon.CompairString(strItemType, "F") = CompairStringResult.Equal Then
                        objIM.ItemType = "FT"
                    Else
                        objIM.ItemType = strItemType
                    End If
                    objIM.Batch_No = "Issue"
                    objIM.MRP = 0
                    objIM.Add_Cost = 0
                    objIM.MRP = 0
                    objIM.MFG_Date = obj.Doc_Date
                    ArrInventoryMovement.Add(objIM)

                    AvgFATPart += objCost.FAT_Cost
                    AvgSNFPart += objCost.SNF_Cost
                    AvgFATKGPart += objIssue.FAT_KG
                    AvgSNFKGPart += objIssue.SNF_KG
                    AvgAmt += (objCost.FAT_Cost + objCost.SNF_Cost)
                End If
            Next
        End If
        InFATPart = AvgFATPart
        InSNFPart = AvgSNFPart

        If obj.ArrARItem IsNot Nothing AndAlso obj.ArrARItem.Count > 0 Then
            For Each objAR As clsRCDFStanardizationAddRemove In obj.ArrARItem
                If clsCommon.CompairString(objAR.ADD_REMOVE_TYPE, "Add") = CompairStringResult.Equal Then
                    If clsCommon.CompairString(objAR.Product_Type, "MI") = CompairStringResult.Equal Then
                        objIMMilk = New clsInventoryMovementNew
                        If clsCommon.CompairString(objAR.ADD_REMOVE_TYPE, "Add") = CompairStringResult.Equal Then
                            objIMMilk.InOut = "O"
                        Else
                            objIMMilk.InOut = "I"
                        End If

                        objIMMilk.Location_Code = objAR.Location_Code
                        objIMMilk.Item_Code = objAR.Item_Code
                        objIMMilk.Item_Desc = objAR.Item_Desc
                        objIMMilk.Qty = objAR.Qty
                        objIMMilk.UOM = objAR.Unit_Code
                        objIMMilk.MRP = Nothing
                        objIMMilk.Add_Cost = Nothing
                        objIMMilk.Net_Cost = Nothing

                        objIMMilk.FAT_Per = objAR.FAT
                        objIMMilk.FAT_KG = objAR.FAT_KG
                        objIMMilk.SNF_KG = objAR.SNF_KG
                        objIMMilk.SNF_Per = objAR.SNF


                        objCost = clsInventoryMovementNew.GetAvgCost(objAR.Product_Type, objAR.Item_Code, objAR.Location_Code, objAR.Qty, objAR.Unit_Code, objAR.FAT_KG, objAR.SNF_KG, obj.Doc_Date, clsCommon.GETSERVERDATE(trans), False, trans)
                        objIMMilk.Avg_Cost = objCost.FAT_Cost + objCost.SNF_Cost
                        objIMMilk.FIFO_Cost = objIMMilk.Avg_Cost
                        objIMMilk.LIFO_Cost = objIMMilk.Avg_Cost

                        objIMMilk.FAT_Per = objAR.FAT
                        objIMMilk.SNF_Per = objAR.SNF
                        objIMMilk.FAT_KG = objAR.FAT_KG
                        objIMMilk.SNF_KG = objAR.SNF_KG

                        strItemType = clsItemMaster.GetItemType(objAR.Item_Code, trans)
                        If clsCommon.CompairString(strItemType, "R") = CompairStringResult.Equal Then
                            objIMMilk.ItemType = "RM"
                        ElseIf clsCommon.CompairString(strItemType, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(strItemType, "O") = CompairStringResult.Equal Then
                            objIMMilk.ItemType = "OT"
                        ElseIf clsCommon.CompairString(strItemType, "F") = CompairStringResult.Equal Then
                            objIMMilk.ItemType = "FT"
                        Else
                            objIMMilk.ItemType = strItemType
                        End If


                        objIMMilk.Batch_No = "Add"
                        objIMMilk.MFG_Date = Nothing
                        objIMMilk.Expiry_Date = Nothing
                        ArrInventoryMovementNew.Add(objIMMilk)

                        AvgFATPart += objCost.FAT_Cost
                        AvgSNFPart += objCost.SNF_Cost
                        AvgFATKGPart += objAR.FAT_KG
                        AvgSNFKGPart += objAR.SNF_KG
                        AvgAmt += (objCost.FAT_Cost + objCost.SNF_Cost)
                    Else
                        objIM = New clsInventoryMovement
                        If clsCommon.CompairString(objAR.ADD_REMOVE_TYPE, "Add") = CompairStringResult.Equal Then
                            objIM.InOut = "O"
                        Else
                            objIM.InOut = "I"
                        End If

                        objIM.Location_Code = objAR.Location_Code
                        objIM.Item_Code = objAR.Item_Code
                        objIM.Item_Desc = objAR.Item_Desc
                        objIM.Qty = objAR.Qty
                        objIM.UOM = objAR.Unit_Code
                        objIM.MRP = Nothing
                        objIM.Add_Cost = Nothing

                        objIM.FAT_Per = objAR.FAT
                        objIM.FAT_KG = objAR.FAT_KG
                        objIM.SNF_KG = objAR.SNF_KG
                        objIM.SNF_Per = objAR.SNF

                        objCost = clsInventoryMovementNew.GetAvgCost(objAR.Product_Type, objAR.Item_Code, objAR.Location_Code, objAR.Qty, objAR.Unit_Code, objAR.FAT_KG, objAR.SNF_KG, obj.Doc_Date, clsCommon.GETSERVERDATE(trans), False, trans)

                        objIM.Avg_Cost = objCost.FAT_Cost + objCost.SNF_Cost
                        objIM.FIFO_Cost = objIM.Avg_Cost
                        objIM.LIFO_Cost = objIM.Avg_Cost

                        objIM.Fat_Rate = clsCommon.myCDivide(objCost.FAT_Cost, objAR.FAT_KG)
                        objIM.SNF_Rate = clsCommon.myCDivide(objCost.SNF_Cost, objAR.SNF_KG)

                        objIM.Fat_Amt = objCost.FAT_Cost
                        objIM.SNF_Amt = objCost.SNF_Cost

                        If clsCommon.CompairString(objIM.InOut, "I") = CompairStringResult.Equal Then
                            objIM.Basic_Cost = clsCommon.myCDivide(objIM.Avg_Cost, objAR.Qty)
                            objIM.Net_Cost = objIM.Avg_Cost
                        End If
                        strItemType = clsItemMaster.GetItemType(objAR.Item_Code, trans)
                        If clsCommon.CompairString(strItemType, "R") = CompairStringResult.Equal Then
                            objIM.ItemType = "RM"
                        ElseIf clsCommon.CompairString(strItemType, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(strItemType, "O") = CompairStringResult.Equal Then
                            objIM.ItemType = "OT"
                        ElseIf clsCommon.CompairString(strItemType, "F") = CompairStringResult.Equal Then
                            objIM.ItemType = "FT"
                        Else
                            objIM.ItemType = strItemType
                        End If
                        objIM.Batch_No = "Add"
                        objIM.MRP = 0
                        objIM.Add_Cost = 0
                        objIM.MRP = 0
                        objIM.MFG_Date = obj.Doc_Date
                        ArrInventoryMovement.Add(objIM)

                        AvgFATPart += objCost.FAT_Cost
                        AvgSNFPart += objCost.SNF_Cost
                        AvgFATKGPart += objAR.FAT_KG
                        AvgSNFKGPart += objAR.SNF_KG
                        AvgAmt += (objCost.FAT_Cost + objCost.SNF_Cost)
                    End If
                End If
            Next

            InFATPart = AvgFATPart
            InSNFPart = AvgSNFPart

            For Each objAR As clsRCDFStanardizationAddRemove In obj.ArrARItem
                Dim AvgFATRate As Decimal = clsCommon.myCDivide(AvgFATPart, AvgFATKGPart)
                Dim AvgSNFRate As Decimal = clsCommon.myCDivide(AvgSNFPart, AvgSNFKGPart)

                If clsCommon.CompairString(objAR.ADD_REMOVE_TYPE, "Remove") = CompairStringResult.Equal Then
                    If clsCommon.CompairString(objAR.Product_Type, "MI") = CompairStringResult.Equal Then
                        objIMMilk = New clsInventoryMovementNew
                        If clsCommon.CompairString(objAR.ADD_REMOVE_TYPE, "Add") = CompairStringResult.Equal Then
                            objIMMilk.InOut = "O"
                        Else
                            objIMMilk.InOut = "I"
                        End If

                        objIMMilk.Location_Code = objAR.Location_Code
                        objIMMilk.Item_Code = objAR.Item_Code
                        objIMMilk.Item_Desc = objAR.Item_Desc
                        objIMMilk.Qty = objAR.Qty
                        objIMMilk.UOM = objAR.Unit_Code
                        objIMMilk.MRP = Nothing
                        objIMMilk.Add_Cost = Nothing
                        objIMMilk.Net_Cost = Nothing

                        objIMMilk.FAT_Per = objAR.FAT
                        objIMMilk.FAT_KG = objAR.FAT_KG
                        objIMMilk.SNF_Per = objAR.SNF
                        objIMMilk.SNF_KG = objAR.SNF_KG


                        objIMMilk.Fat_Rate = AvgFATRate
                        objIMMilk.SNF_Rate = AvgSNFRate
                        objIMMilk.Fat_Amt = AvgFATRate * objAR.FAT_KG
                        objIMMilk.SNF_Amt = AvgSNFRate * objAR.SNF_KG


                        objIMMilk.Avg_Cost = objIMMilk.Fat_Amt + objIMMilk.SNF_Amt
                        objIMMilk.FIFO_Cost = objIMMilk.Avg_Cost
                        objIMMilk.LIFO_Cost = objIMMilk.Avg_Cost

                        objIMMilk.FAT_Per = objAR.FAT
                        objIMMilk.SNF_Per = objAR.SNF
                        objIMMilk.FAT_KG = objAR.FAT_KG
                        objIMMilk.SNF_KG = objAR.SNF_KG

                        strItemType = clsItemMaster.GetItemType(objAR.Item_Code, trans)
                        If clsCommon.CompairString(strItemType, "R") = CompairStringResult.Equal Then
                            objIMMilk.ItemType = "RM"
                        ElseIf clsCommon.CompairString(strItemType, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(strItemType, "O") = CompairStringResult.Equal Then
                            objIMMilk.ItemType = "OT"
                        ElseIf clsCommon.CompairString(strItemType, "F") = CompairStringResult.Equal Then
                            objIMMilk.ItemType = "FT"
                        Else
                            objIMMilk.ItemType = strItemType
                        End If


                        objIMMilk.Batch_No = "Remove"
                        objIMMilk.MFG_Date = Nothing
                        objIMMilk.Expiry_Date = Nothing
                        objIMMilk.CalculateAvgCost = False
                        ArrInventoryMovementNew.Add(objIMMilk)

                        InFATPart -= objIMMilk.Fat_Amt
                        InSNFPart -= objIMMilk.SNF_Amt
                    Else
                        objIM = New clsInventoryMovement
                        If clsCommon.CompairString(objAR.ADD_REMOVE_TYPE, "Add") = CompairStringResult.Equal Then
                            objIM.InOut = "O"
                        Else
                            objIM.InOut = "I"
                        End If

                        objIM.Location_Code = objAR.Location_Code
                        objIM.Item_Code = objAR.Item_Code
                        objIM.Item_Desc = objAR.Item_Desc
                        objIM.Qty = objAR.Qty
                        objIM.UOM = objAR.Unit_Code
                        objIM.MRP = Nothing
                        objIM.Add_Cost = Nothing

                        objIM.FAT_Per = objAR.FAT
                        objIM.FAT_KG = objAR.FAT_KG
                        objIM.SNF_KG = objAR.SNF_KG
                        objIM.SNF_Per = objAR.SNF

                        objIM.Fat_Rate = AvgFATRate
                        objIM.SNF_Rate = AvgSNFRate
                        objIM.Fat_Amt = AvgFATRate * objAR.FAT_KG
                        objIM.SNF_Amt = AvgSNFRate * objAR.SNF_KG


                        objIM.Avg_Cost = objIM.Fat_Amt + objIM.SNF_Amt
                        objIM.FIFO_Cost = objIM.Avg_Cost
                        objIM.LIFO_Cost = objIM.Avg_Cost

                        objIM.FAT_Per = objAR.FAT
                        objIM.SNF_Per = objAR.SNF
                        objIM.FAT_KG = objAR.FAT_KG
                        objIM.SNF_KG = objAR.SNF_KG

                        If clsCommon.CompairString(objIM.InOut, "I") = CompairStringResult.Equal Then
                            objIM.Basic_Cost = clsCommon.myCDivide(objIM.Avg_Cost, objAR.Qty)
                            objIM.Net_Cost = objIM.Avg_Cost
                        End If
                        strItemType = clsItemMaster.GetItemType(objAR.Item_Code, trans)
                        If clsCommon.CompairString(strItemType, "R") = CompairStringResult.Equal Then
                            objIM.ItemType = "RM"
                        ElseIf clsCommon.CompairString(strItemType, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(strItemType, "O") = CompairStringResult.Equal Then
                            objIM.ItemType = "OT"
                        ElseIf clsCommon.CompairString(strItemType, "F") = CompairStringResult.Equal Then
                            objIM.ItemType = "FT"
                        Else
                            objIM.ItemType = strItemType
                        End If
                        objIM.Batch_No = "Remove"
                        objIM.MRP = 0
                        objIM.Add_Cost = 0
                        objIM.MRP = 0
                        objIM.MFG_Date = obj.Doc_Date
                        objIM.CalculateAvgCost = False
                        ArrInventoryMovement.Add(objIM)

                        InFATPart -= objIM.Fat_Amt
                        InSNFPart -= objIM.SNF_Amt

                    End If
                End If
            Next


        End If
        If (obj.ArrProduce IsNot Nothing AndAlso obj.ArrProduce.Count > 0) Then
            For Each objProduce As clsRCDFStanardizationProduce In obj.ArrProduce
                objIMMilk = New clsInventoryMovementNew
                objIMMilk.InOut = "I"
                objIMMilk.Location_Code = objProduce.Location_Code
                objIMMilk.main_location = obj.Location_Code
                objIMMilk.Item_Code = objProduce.Item_Code
                objIMMilk.Item_Desc = clsItemMaster.GetItemName(objProduce.Item_Code, trans)
                objIMMilk.Qty = objProduce.Qty
                objIMMilk.UOM = objProduce.Unit_Code
                objIMMilk.Source_Doc_No = obj.Doc_Code
                objIMMilk.Source_Doc_Date = obj.Doc_Date

                objIMMilk.CalculateAvgCost = False
                objIMMilk.DonNotCalculateAvgFATSNFCost = True

                objIMMilk.Avg_Cost = InFATPart + InSNFPart
                objIMMilk.FIFO_Cost = objIMMilk.Avg_Cost
                objIMMilk.LIFO_Cost = objIMMilk.Avg_Cost

                objIMMilk.FAT_Per = objProduce.FAT
                objIMMilk.SNF_Per = objProduce.SNF
                objIMMilk.FAT_KG = objProduce.FAT_KG
                objIMMilk.SNF_KG = objProduce.SNF_KG



                '' UPDATE PRODUCTION COST
                objIMMilk.Fat_Rate = clsCommon.myCDivide(InFATPart, objProduce.FAT_KG)
                objIMMilk.SNF_Rate = clsCommon.myCDivide(InSNFPart, objProduce.SNF_KG)
                objIMMilk.Fat_Amt = InFATPart
                objIMMilk.SNF_Amt = InSNFPart


                If clsCommon.CompairString(objIMMilk.InOut, "I") = CompairStringResult.Equal Then
                    objIMMilk.Basic_Cost = clsCommon.myCDivide(objIMMilk.Avg_Cost, objProduce.Qty)
                    objIMMilk.Net_Cost = objIMMilk.Avg_Cost
                End If

                strItemType = clsItemMaster.GetItemType(objProduce.Item_Code, trans)
                If clsCommon.CompairString(strItemType, "R") = CompairStringResult.Equal Then
                    objIMMilk.ItemType = "RM"
                ElseIf clsCommon.CompairString(strItemType, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(strItemType, "O") = CompairStringResult.Equal Then
                    objIMMilk.ItemType = "OT"
                ElseIf clsCommon.CompairString(strItemType, "F") = CompairStringResult.Equal Then
                    objIMMilk.ItemType = "FT"
                Else
                    objIMMilk.ItemType = strItemType
                End If

                objIMMilk.Batch_No = "Produce"
                objIMMilk.MRP = 0
                objIMMilk.Add_Cost = 0
                objIMMilk.MRP = 0
                objIMMilk.MFG_Date = obj.Doc_Date

                ArrInventoryMovementNew.Add(objIMMilk)
            Next
        End If


        If ArrInventoryMovement.Count > 0 Then
            clsInventoryMovement.SaveData("RP-SZ", obj.Doc_Code, obj.Doc_Date, clsCommon.GetPrintDate(obj.Doc_Date, "dd/MM/yyyy"), ArrInventoryMovement, trans)
        End If

        If ArrInventoryMovementNew.Count > 0 Then
            clsInventoryMovementNew.SaveData("RP-SZ", obj.Doc_Code, obj.Doc_Date, clsCommon.GetPrintDate(obj.Doc_Date, "dd/MM/yyyy"), ArrInventoryMovementNew, trans)
        End If
        Return True
    End Function

    Public Shared Function CreateJE(ByVal Doc_Code As String, ByVal trans As SqlTransaction, Optional ByVal strVourcherNoForRecreateOnly As String = "") As Boolean
        Try
            Dim obj As clsRCDFStanardization = clsRCDFStanardization.GetData(Doc_Code, "", NavigatorType.Current, trans)
            Dim VoucherNo As String = ""
            If clsCommon.myLen(strVourcherNoForRecreateOnly) > 0 Then
                VoucherNo = strVourcherNoForRecreateOnly
            Else
                VoucherNo = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Voucher_No from TSPL_JOURNAL_MASTER where Source_Code='RP-SZ' and Source_Doc_No='" & obj.Doc_Code & "'", trans))
            End If

            Dim Count As Integer = 0
            Dim qry As String

            Dim ArryLstGLAC As ArrayList = New ArrayList()
            Dim VoucherDesc As String = "Financial Entry for Production Standardization -" & obj.Doc_Code & " "
            Dim SourceDocDesc As String = "Production Standardization in Bulk"
            Dim SourceDocNo As String = obj.Doc_Code
            Dim Comments As String = VoucherDesc
            Dim Remarks As String = VoucherDesc

            qry = "select xx.*,TSPL_PURCHASE_ACCOUNTS.Inv_Control_Account from (
select Item_Code,InOut,Avg_Cost from TSPL_INVENTORY_MOVEMENT where Trans_Type='RP-SZ' and Source_Doc_No='" + obj.Doc_Code + "'
union all
select Item_Code,InOut,Avg_Cost from TSPL_INVENTORY_MOVEMENT_NEW  where Trans_Type='RP-SZ' and  Source_Doc_No='" + obj.Doc_Code + "'
)xx
left join TSPL_ITEM_MASTER on xx.Item_Code=TSPL_ITEM_MASTER.Item_Code
left join TSPL_PURCHASE_ACCOUNTS on TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code=TSPL_ITEM_MASTER.Purchase_Class_Code"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    If clsCommon.myLen(dr("Inv_Control_Account")) <= 0 Then
                        Throw New Exception("Inventory Control Account not found for Item " & clsCommon.myCstr(dr("Item_Code")) & "")
                    End If
                    Dim Acc As String = clsERPFuncationality.ChangeGLAccountLocationSegment(clsCommon.myCstr(dr("Inv_Control_Account")), obj.Location_Code, trans)
                    If clsCommon.myLen(Acc) > 0 Then
                        If clsCommon.CompairString(clsCommon.myCstr(dr("InOut")), "O") Then
                            Dim Acc2() As String = {Acc, -1 * clsCommon.myCDecimal(dr("Avg_Cost"))}
                            ArryLstGLAC.Add(Acc2)
                        Else
                            Dim Acc2() As String = {Acc, clsCommon.myCDecimal(dr("Avg_Cost"))}
                            ArryLstGLAC.Add(Acc2)
                        End If
                    End If
                Next
            End If
            Dim GLDesc As String = "Journal Entry Against Production Standardization- Doc No." & obj.Doc_Code & " "
            If clsCommon.myLen(VoucherNo) > 0 Then
                transportSql.FunGrnlEntryWithTrans(obj.Location_Code, False, VoucherNo, trans, obj.Doc_Date, GLDesc, "RP-SZ", "Production Standardization", obj.Doc_Code, Comments, "I", "", "", objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLstGLAC, Nothing, GLDesc, "")
            Else
                transportSql.FunGrnlEntryWithTrans(obj.Location_Code, False, trans, obj.Doc_Date, GLDesc, "RP-SZ", "Production Standardization", obj.Doc_Code, Comments, "I", "", "", objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLstGLAC, , GLDesc, "")
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

End Class
