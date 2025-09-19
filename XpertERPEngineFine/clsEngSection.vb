Imports System.Data.SqlClient
Imports common

Public Class clsEngSectionMaster
#Region "variables"
    Public Code As String = Nothing
    Public Description As String = Nothing
#End Region

    Public Shared Function SaveData(ByVal objList As List(Of clsEngSectionMaster)) As Boolean
        Dim ISSaved As Boolean = True
        Dim isNewEntry As Boolean = False
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If (objList IsNot Nothing AndAlso objList.Count > 0) Then
                For Each obj As clsEngSectionMaster In objList
                    isNewEntry = IIf(clsDBFuncationality.getSingleValue("Select COunt(*) from TSPL_ENG_SECTION_MASTER WHERE Code='" + obj.Code + "'", trans) = 0, True, False)
                    ISSaved = SaveData(obj, isNewEntry, trans)
                Next
            End If

            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return ISSaved
    End Function

    Public Shared Function SaveData(ByVal obj As clsEngSectionMaster, ByVal isNewEntry As Boolean) As Boolean
        Dim qry As String = ""
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            SaveData(obj, isNewEntry, trans)
            trans.Commit()
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function

    Public Shared Function SaveData(ByVal obj As clsEngSectionMaster, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim qry As String = ""
        Dim IsSaved As Boolean = False
        Try
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Description", obj.Description)
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
            If isNewEntry Then
                If (IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AutoGeneratePrefix, clsFixedParameterCode.AutoGeneratePrefix, trans)) = "1", True, False)) Then
                    Dim ChkNewEntry As String = clsDBFuncationality.getSingleValue("Select count(*) from TSPL_ENG_SECTION_MASTER where Code='" & obj.Code & "'", trans)
                    If ChkNewEntry = 0 Then
                        obj.Code = clsERPFuncationality.GetNextCode(trans, clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"), clsDocType.SectionMasterEng, "", "")
                        If clsCommon.myLen(obj.Code) <= 0 Then
                            Throw New Exception("Error in Code Generation")
                        End If
                    End If
                End If
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "Code", obj.Code)
                IsSaved = clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ENG_SECTION_MASTER", OMInsertOrUpdate.Insert, "", trans)
            Else
                IsSaved = clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ENG_SECTION_MASTER", OMInsertOrUpdate.Update, "TSPL_ENG_SECTION_MASTER.Code='" + obj.Code + "'", trans)
            End If

        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return IsSaved
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsEngSectionMaster
        Return GetData(strCode, NavType, Nothing)
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsEngSectionMaster
        Dim obj As clsEngSectionMaster = Nothing
        Dim Arr As List(Of clsEngSectionMaster) = Nothing
        Dim qry As String = "select Code ,Description from TSPL_ENG_SECTION_MASTER where 2=2 "
        Dim whrclas As String = ""
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_ENG_SECTION_MASTER.Code = (select MIN(Code) from TSPL_ENG_SECTION_MASTER WHERE 1=1 " + whrclas + " )"
            Case NavigatorType.Last
                qry += " and TSPL_ENG_SECTION_MASTER.Code = (select Max(Code) from TSPL_ENG_SECTION_MASTER WHERE 1=1 " + whrclas + " )"
            Case NavigatorType.Current
                qry += " and TSPL_ENG_SECTION_MASTER.Code = (select TOP 1 Code from TSPL_ENG_SECTION_MASTER WHERE 1=1 " + whrclas + " and Code='" + strCode + "' )"
            Case NavigatorType.Next
                qry += " and TSPL_ENG_SECTION_MASTER.Code = (select Min(Code) from TSPL_ENG_SECTION_MASTER where Code > '" + strCode + "' " + whrclas + ")"
            Case NavigatorType.Previous
                qry += " and TSPL_ENG_SECTION_MASTER.Code = (select Max(Code) from TSPL_ENG_SECTION_MASTER where Code < '" + strCode + "' " + whrclas + ")"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsEngSectionMaster()
            obj.Code = clsCommon.myCstr(dt.Rows(0)("Code"))
            obj.Description = clsCommon.myCstr(dt.Rows(0)("Description"))
        End If
        Return obj
    End Function
    Public Shared Function GetName(ByVal strCode As String, ByVal trans As SqlClient.SqlTransaction) As String
        Dim qry As String = "SELECT Description FROM TSPL_ENG_SECTION_MASTER where Code='" + strCode + "' "
        Return clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
    End Function

    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim isSaved As Boolean
        Try
            isSaved = False
            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Code not found to Delete")
            End If

            Dim qry As String
            qry = "delete from TSPL_ENG_SECTION_MASTER where Code ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry)
        Catch ex As Exception
            Throw New Exception(ex.Message.ToString())
        End Try
        Return isSaved
    End Function
End Class

Public Class clsEngConsumptionTypeMaster
#Region "variables"
    Public Code As String = Nothing
    Public Description As String = Nothing
#End Region

    Public Shared Function SaveData(ByVal objList As List(Of clsEngConsumptionTypeMaster)) As Boolean
        Dim ISSaved As Boolean = True
        Dim isNewEntry As Boolean = False
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If (objList IsNot Nothing AndAlso objList.Count > 0) Then
                For Each obj As clsEngConsumptionTypeMaster In objList
                    isNewEntry = IIf(clsDBFuncationality.getSingleValue("Select COunt(*) from TSPL_ENG_CONSUMPTION_TYPE_MASTER WHERE Code='" + obj.Code + "'", trans) = 0, True, False)
                    ISSaved = SaveData(obj, isNewEntry, trans)
                Next
            End If

            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return ISSaved
    End Function

    Public Shared Function SaveData(ByVal obj As clsEngConsumptionTypeMaster, ByVal isNewEntry As Boolean) As Boolean
        Dim qry As String = ""
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            SaveData(obj, isNewEntry, trans)
            trans.Commit()
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function

    Public Shared Function SaveData(ByVal obj As clsEngConsumptionTypeMaster, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim qry As String = ""
        Dim IsSaved As Boolean = False
        Try
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Description", obj.Description)
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
            If isNewEntry Then
                If (IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AutoGeneratePrefix, clsFixedParameterCode.AutoGeneratePrefix, trans)) = "1", True, False)) Then
                    Dim ChkNewEntry As String = clsDBFuncationality.getSingleValue("Select count(*) from TSPL_ENG_CONSUMPTION_TYPE_MASTER where Code='" & obj.Code & "'", trans)
                    If ChkNewEntry = 0 Then
                        obj.Code = clsERPFuncationality.GetNextCode(trans, clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"), clsDocType.ConsumptionTypeMaster, "", "")
                        If clsCommon.myLen(obj.Code) <= 0 Then
                            Throw New Exception("Error in Code Generation")
                        End If
                    End If
                End If
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "Code", obj.Code)
                IsSaved = clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ENG_CONSUMPTION_TYPE_MASTER", OMInsertOrUpdate.Insert, "", trans)
            Else
                IsSaved = clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ENG_CONSUMPTION_TYPE_MASTER", OMInsertOrUpdate.Update, "TSPL_ENG_CONSUMPTION_TYPE_MASTER.Code='" + obj.Code + "'", trans)
            End If

        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return IsSaved
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsEngConsumptionTypeMaster
        Return GetData(strCode, NavType, Nothing)
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsEngConsumptionTypeMaster
        Dim obj As clsEngConsumptionTypeMaster = Nothing
        Dim Arr As List(Of clsEngConsumptionTypeMaster) = Nothing
        Dim qry As String = "select Code ,Description from TSPL_ENG_CONSUMPTION_TYPE_MASTER where 2=2 "
        Dim whrclas As String = ""
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_ENG_CONSUMPTION_TYPE_MASTER.Code = (select MIN(Code) from TSPL_ENG_CONSUMPTION_TYPE_MASTER WHERE 1=1 " + whrclas + " )"
            Case NavigatorType.Last
                qry += " and TSPL_ENG_CONSUMPTION_TYPE_MASTER.Code = (select Max(Code) from TSPL_ENG_CONSUMPTION_TYPE_MASTER WHERE 1=1 " + whrclas + " )"
            Case NavigatorType.Current
                qry += " and TSPL_ENG_CONSUMPTION_TYPE_MASTER.Code = (select TOP 1 Code from TSPL_ENG_CONSUMPTION_TYPE_MASTER WHERE 1=1 " + whrclas + " and Code='" + strCode + "' )"
            Case NavigatorType.Next
                qry += " and TSPL_ENG_CONSUMPTION_TYPE_MASTER.Code = (select Min(Code) from TSPL_ENG_CONSUMPTION_TYPE_MASTER where Code > '" + strCode + "' " + whrclas + ")"
            Case NavigatorType.Previous
                qry += " and TSPL_ENG_CONSUMPTION_TYPE_MASTER.Code = (select Max(Code) from TSPL_ENG_CONSUMPTION_TYPE_MASTER where Code < '" + strCode + "' " + whrclas + ")"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsEngConsumptionTypeMaster()
            obj.Code = clsCommon.myCstr(dt.Rows(0)("Code"))
            obj.Description = clsCommon.myCstr(dt.Rows(0)("Description"))
        End If
        Return obj
    End Function
    Public Shared Function GetName(ByVal strCode As String, ByVal trans As SqlClient.SqlTransaction) As String
        Dim qry As String = "SELECT Description FROM TSPL_ENG_CONSUMPTION_TYPE_MASTER where Code='" + strCode + "' "
        Return clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
    End Function

    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim isSaved As Boolean
        Try
            isSaved = False
            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Code not found to Delete")
            End If

            Dim qry As String
            qry = "delete from TSPL_ENG_CONSUMPTION_TYPE_MASTER where Code ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry)
        Catch ex As Exception
            Throw New Exception(ex.Message.ToString())
        End Try
        Return isSaved
    End Function
End Class


Public Class clsSectionConsumptionMapping
#Region "Variables"
    Public TR_Code As String = Nothing
    Public Code As String = Nothing
    Public arrListConsumption As ArrayList = Nothing
#End Region

    Public Shared Function SaveData(ByVal objList As List(Of clsSectionConsumptionMapping)) As Boolean
        Dim ISSaved As Boolean = True
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If (objList IsNot Nothing AndAlso objList.Count > 0) Then
                For Each obj As clsSectionConsumptionMapping In objList
                    ISSaved = SaveData(obj, trans)
                Next
            End If

            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return ISSaved
    End Function

    Public Shared Function SaveData(ByVal obj As clsSectionConsumptionMapping) As Boolean
        Dim qry As String = ""
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            SaveData(obj, trans)
            trans.Commit()
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function

    Public Shared Function SaveData(ByVal obj As clsSectionConsumptionMapping, ByVal tran As SqlTransaction) As Boolean
        Try
            Dim qry As String
            qry = "delete from TSPL_ENG_SECTION_CONSUMPTION_MAPPING where Section_Code ='" + obj.Code + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, tran)
            If obj.arrListConsumption IsNot Nothing AndAlso obj.arrListConsumption.Count > 0 Then
                For i As Integer = 0 To obj.arrListConsumption.Count - 1
                    Dim coll As New Hashtable()
                    obj.TR_Code = clsERPFuncationality.GetNextCode(tran, clsCommon.GETSERVERDATE(tran), clsDocType.Detail, clsDocTransactionType.Detail, "")
                    clsCommon.AddColumnsForChange(coll, "TR_Code", obj.TR_Code)
                    clsCommon.AddColumnsForChange(coll, "Section_Code", obj.Code)
                    clsCommon.AddColumnsForChange(coll, "Consumption_Code", obj.arrListConsumption.Item(i))
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ENG_SECTION_CONSUMPTION_MAPPING", OMInsertOrUpdate.Insert, "", tran)
                Next
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As common.NavigatorType) As clsSectionConsumptionMapping
        Dim obj As clsSectionConsumptionMapping = New clsSectionConsumptionMapping()
        Dim qry As String = "SELECT TSPL_ENG_SECTION_MASTER.* FROM TSPL_ENG_SECTION_MASTER where  2=2"

        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_ENG_SECTION_MASTER.Code=(select MIN(Code) from TSPL_ENG_SECTION_MASTER  )"
            Case NavigatorType.Last
                qry += " and TSPL_ENG_SECTION_MASTER.Code=(select Max(Code) from TSPL_ENG_SECTION_MASTER  )"
            Case NavigatorType.Next
                qry += " and TSPL_ENG_SECTION_MASTER.Code=(select Min(Code) from TSPL_ENG_SECTION_MASTER where Code > '" + strCode + "' )"
            Case NavigatorType.Previous
                qry += " and TSPL_ENG_SECTION_MASTER.Code=(select Max(Code) from TSPL_ENG_SECTION_MASTER where Code < '" + strCode + "' )"
            Case NavigatorType.Current
                qry += " and TSPL_ENG_SECTION_MASTER.Code='" + strCode + "'"
        End Select

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            'obj = New clsSectionConsumptionMapping()
            obj.Code = clsCommon.myCstr(dt.Rows(0)("Code"))
            'obj.Name = clsCommon.myCstr(dt.Rows(0)("Name"))


            Dim templist As New ArrayList
            qry = "SELECT TSPL_ENG_SECTION_CONSUMPTION_MAPPING.* FROM TSPL_ENG_SECTION_CONSUMPTION_MAPPING  where TSPL_ENG_SECTION_CONSUMPTION_MAPPING.Section_Code='" + obj.Code + "' "
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                For Each dr As DataRow In dt.Rows
                    templist.Add(clsCommon.myCstr(dr("Consumption_Code")))
                Next
            End If
            obj.arrListConsumption = templist
        End If

        Return obj
    End Function


    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim isSaved As Boolean
        Try
            isSaved = False
            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Code not found to Delete")
            End If

            Dim qry As String
            qry = "delete from TSPL_ENG_SECTION_CONSUMPTION_MAPPING where Section_Code ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry)
        Catch ex As Exception
            Throw New Exception(ex.Message.ToString())
        End Try
        Return isSaved
    End Function
End Class

'Parameter
Public Class clsEngParameterMaster
#Region "variables"
    Public Code As String = Nothing
    Public Description As String = Nothing
#End Region

    Public Shared Function SaveData(ByVal objList As List(Of clsEngParameterMaster)) As Boolean
        Dim ISSaved As Boolean = True
        Dim isNewEntry As Boolean = False
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If (objList IsNot Nothing AndAlso objList.Count > 0) Then
                For Each obj As clsEngParameterMaster In objList
                    isNewEntry = IIf(clsDBFuncationality.getSingleValue("Select COunt(*) from TSPL_ENG_PARAMETER_MASTER WHERE Code='" + obj.Code + "'", trans) = 0, True, False)
                    ISSaved = SaveData(obj, isNewEntry, trans)
                Next
            End If

            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return ISSaved
    End Function

    Public Shared Function SaveData(ByVal obj As clsEngParameterMaster, ByVal isNewEntry As Boolean) As Boolean
        Dim qry As String = ""
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            SaveData(obj, isNewEntry, trans)
            trans.Commit()
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function

    Public Shared Function SaveData(ByVal obj As clsEngParameterMaster, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim qry As String = ""
        Dim IsSaved As Boolean = False
        Try
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Description", obj.Description)
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "Code", obj.Code)
                IsSaved = clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ENG_PARAMETER_MASTER", OMInsertOrUpdate.Insert, "", trans)
            Else
                IsSaved = clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ENG_PARAMETER_MASTER", OMInsertOrUpdate.Update, "TSPL_ENG_PARAMETER_MASTER.Code='" + obj.Code + "'", trans)
            End If

        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return IsSaved
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsEngParameterMaster
        Return GetData(strCode, NavType, Nothing)
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsEngParameterMaster
        Dim obj As clsEngParameterMaster = Nothing
        Dim Arr As List(Of clsEngParameterMaster) = Nothing
        Dim qry As String = "select Code ,Description from TSPL_ENG_PARAMETER_MASTER where 2=2 "
        Dim whrclas As String = ""
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_ENG_PARAMETER_MASTER.Code = (select MIN(Code) from TSPL_ENG_PARAMETER_MASTER WHERE 1=1 " + whrclas + " )"
            Case NavigatorType.Last
                qry += " and TSPL_ENG_PARAMETER_MASTER.Code = (select Max(Code) from TSPL_ENG_PARAMETER_MASTER WHERE 1=1 " + whrclas + " )"
            Case NavigatorType.Current
                qry += " and TSPL_ENG_PARAMETER_MASTER.Code = (select TOP 1 Code from TSPL_ENG_PARAMETER_MASTER WHERE 1=1 " + whrclas + " and Code='" + strCode + "' )"
            Case NavigatorType.Next
                qry += " and TSPL_ENG_PARAMETER_MASTER.Code = (select Min(Code) from TSPL_ENG_PARAMETER_MASTER where Code > '" + strCode + "' " + whrclas + ")"
            Case NavigatorType.Previous
                qry += " and TSPL_ENG_PARAMETER_MASTER.Code = (select Max(Code) from TSPL_ENG_PARAMETER_MASTER where Code < '" + strCode + "' " + whrclas + ")"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsEngParameterMaster()
            obj.Code = clsCommon.myCstr(dt.Rows(0)("Code"))
            obj.Description = clsCommon.myCstr(dt.Rows(0)("Description"))
        End If
        Return obj
    End Function
    Public Shared Function GetName(ByVal strCode As String, ByVal trans As SqlClient.SqlTransaction) As String
        Dim qry As String = "SELECT Description FROM TSPL_ENG_PARAMETER_MASTER where Code='" + strCode + "' "
        Return clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
    End Function

    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim isSaved As Boolean
        Try
            isSaved = False
            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Code not found to Delete")
            End If

            Dim qry As String
            qry = "delete from TSPL_ENG_PARAMETER_MASTER where Code ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry)
        Catch ex As Exception
            Throw New Exception(ex.Message.ToString())
        End Try
        Return isSaved
    End Function
End Class

'Public Class clsSectionParameterMapping
'#Region "Variables"
'    Public TR_Code As String = Nothing
'    Public Section_Code As String = Nothing
'    Public Consumption_Code As String = Nothing
'    Public arrListParameter As ArrayList = Nothing
'#End Region

'    Public Shared Function SaveData(ByVal objList As List(Of clsSectionParameterMapping)) As Boolean
'        Dim ISSaved As Boolean = True
'        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
'        Try
'            If (objList IsNot Nothing AndAlso objList.Count > 0) Then
'                For Each obj As clsSectionParameterMapping In objList
'                    ISSaved = SaveData(obj, trans)
'                Next
'            End If

'            trans.Commit()
'        Catch ex As Exception
'            trans.Rollback()
'            Throw New Exception(ex.Message)
'        End Try
'        Return ISSaved
'    End Function

'    Public Shared Function SaveData(ByVal obj As clsSectionParameterMapping) As Boolean
'        Dim qry As String = ""
'        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
'        Try
'            SaveData(obj, trans)
'            trans.Commit()
'        Catch err As Exception
'            trans.Rollback()
'            Throw New Exception(err.Message)
'        End Try
'        Return True
'    End Function

'    Public Shared Function SaveData(ByVal obj As clsSectionParameterMapping, ByVal tran As SqlTransaction) As Boolean
'        Try
'            Dim qry As String
'            qry = "delete from TSPL_ENG_SECTION_PARAMETER_MAPPING where Section_Code ='" + obj.Section_Code + "' and Consumption_Code='" + obj.Consumption_Code + "'"
'            clsDBFuncationality.ExecuteNonQuery(qry, tran)
'            If obj.arrListParameter IsNot Nothing AndAlso obj.arrListParameter.Count > 0 Then
'                For i As Integer = 0 To obj.arrListParameter.Count - 1
'                    Dim coll As New Hashtable()
'                    obj.TR_Code = clsERPFuncationality.GetNextCode(tran, clsCommon.GETSERVERDATE(tran), clsDocType.Detail, clsDocTransactionType.Detail, "")
'                    clsCommon.AddColumnsForChange(coll, "TR_Code", obj.TR_Code)
'                    clsCommon.AddColumnsForChange(coll, "Section_Code", obj.Section_Code)
'                    clsCommon.AddColumnsForChange(coll, "Consumption_Code", obj.Consumption_Code)
'                    clsCommon.AddColumnsForChange(coll, "Parameter_Code", obj.arrListParameter.Item(i))
'                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ENG_SECTION_PARAMETER_MAPPING", OMInsertOrUpdate.Insert, "", tran)
'                Next
'            End If
'        Catch ex As Exception
'            Throw New Exception(ex.Message)
'        End Try
'        Return True
'    End Function

'    Public Shared Function GetData(ByVal strSection_Code As String, ByVal strConsumption_Code As String) As clsSectionParameterMapping
'        Dim obj As clsSectionParameterMapping = New clsSectionParameterMapping()
'        Dim qry As String = ""

'        obj.Section_Code = strSection_Code
'        obj.Consumption_Code = strConsumption_Code
'        'Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
'        'If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then

'        Dim templist As New ArrayList
'        qry = "SELECT TSPL_ENG_SECTION_PARAMETER_MAPPING.* FROM TSPL_ENG_SECTION_PARAMETER_MAPPING  where TSPL_ENG_SECTION_PARAMETER_MAPPING.Section_Code='" + strSection_Code + "'  and TSPL_ENG_SECTION_PARAMETER_MAPPING.Consumption_Code='" + strConsumption_Code + "'"
'        Dim dt As DataTable = New DataTable()
'        dt = clsDBFuncationality.GetDataTable(qry)
'        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
'            For Each dr As DataRow In dt.Rows
'                templist.Add(clsCommon.myCstr(dr("Parameter_Code")))
'            Next
'        End If
'        obj.arrListParameter = templist
'        'End If

'        Return obj
'    End Function


'    Public Shared Function DeleteData(ByVal strSection_Code As String, ByVal strConsumption_Code As String) As Boolean
'        Dim isSaved As Boolean
'        Try
'            isSaved = False
'            If (clsCommon.myLen(strSection_Code) <= 0) OrElse (clsCommon.myLen(strConsumption_Code) <= 0) Then
'                Throw New Exception("Code not found to Delete")
'            End If

'            Dim qry As String
'            qry = "delete from TSPL_ENG_SECTION_PARAMETER_MAPPING where Section_Code ='" + strSection_Code + "' and Consumption_Code='" + strConsumption_Code + "'"
'            isSaved = clsDBFuncationality.ExecuteNonQuery(qry)
'        Catch ex As Exception
'            Throw New Exception(ex.Message.ToString())
'        End Try
'        Return isSaved
'    End Function
'End Class

Public Class clsSectionParameterMapping

#Region "Variables"
    Public Section_Code As String = Nothing
    Public Consumption_Code As String = Nothing
    Public arrListParameter As List(Of clsSectionParameterMappingDetail) = Nothing
#End Region


    Public Shared Function GetData(ByVal strSection_Code As String, ByVal strConsumption_Code As String) As clsSectionParameterMapping
        Return GetData(strSection_Code, strConsumption_Code, Nothing)
    End Function

    Public Shared Function GetData(ByVal strSection_Code As String, ByVal strConsumption_Code As String, ByVal trans As SqlTransaction) As clsSectionParameterMapping
        Dim obj As clsSectionParameterMapping = New clsSectionParameterMapping()
        Dim qry As String = ""

        obj.Section_Code = strSection_Code
        obj.Consumption_Code = strConsumption_Code
        obj.arrListParameter = clsSectionParameterMappingDetail.GetData(obj.Section_Code, obj.Consumption_Code, trans)
        Return obj
    End Function

    Public Shared Function DeleteData(ByVal strSection_Code As String, ByVal strConsumption_Code As String) As Boolean
        Dim isSaved As Boolean
        Try
            isSaved = False
            If (clsCommon.myLen(strSection_Code) <= 0) OrElse (clsCommon.myLen(strConsumption_Code) <= 0) Then
                Throw New Exception("Code not found to Delete")
            End If

            Dim qry As String
            qry = "delete from TSPL_ENG_SECTION_PARAMETER_MAPPING where Section_Code ='" + strSection_Code + "' and Consumption_Code='" + strConsumption_Code + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry)
        Catch ex As Exception
            Throw New Exception(ex.Message.ToString())
        End Try
        Return isSaved
    End Function

    Public Shared Function SaveData(ByVal objList As List(Of clsSectionParameterMapping)) As Boolean
        Dim qry As String = ""
        Dim ISSaved As Boolean = True
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If (objList IsNot Nothing AndAlso objList.Count > 0) Then
                For Each obj As clsSectionParameterMapping In objList
                    'ISSaved = SaveData(obj, trans)

                    qry = "delete from TSPL_ENG_SECTION_PARAMETER_MAPPING where Section_Code ='" + clsCommon.myCstr(obj.Section_Code) + "' and Consumption_Code='" + clsCommon.myCstr(obj.Consumption_Code) + "'"
                    ISSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
                Next
                For Each obj As clsSectionParameterMapping In objList
                    clsSectionParameterMappingDetail.SaveData(obj.Section_Code, obj.Consumption_Code, obj.arrListParameter, trans, True)
                Next
            End If

            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return ISSaved
    End Function

    Public Shared Function SaveData(ByVal obj As clsSectionParameterMapping) As Boolean
        Dim qry As String = ""
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            SaveData(obj, trans)
            trans.Commit()
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function

    Public Shared Function SaveData(ByVal obj As clsSectionParameterMapping, ByVal tran As SqlTransaction) As Boolean
        Try
            Dim isSaved As Boolean = True
            
            If obj.arrListParameter IsNot Nothing AndAlso obj.arrListParameter.Count > 0 Then
                For i As Integer = 0 To obj.arrListParameter.Count - 1
                    isSaved = isSaved AndAlso clsSectionParameterMappingDetail.SaveData(obj.Section_Code, obj.Consumption_Code, obj.arrListParameter, tran)
                Next
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

End Class

Public Class clsSectionParameterMappingDetail
#Region "Variables"
    Public TR_Code As String = Nothing
    Public Parameter_Code As String = Nothing
    Public Parameter_Seq As Integer = Nothing
#End Region

    Public Shared Function SaveData(ByVal DocSection_Code As String, ByVal DocConsumption_Code As String, ByVal arr As List(Of clsSectionParameterMappingDetail), ByVal trans As SqlTransaction, Optional ByVal Is_Import As Boolean = False) As Boolean
        Try
            Dim isSaved As Boolean = True
            Dim qry As String = ""
            If Is_Import = False Then
                qry = "delete from TSPL_ENG_SECTION_PARAMETER_MAPPING where Section_Code ='" + DocSection_Code + "' and Consumption_Code='" + DocConsumption_Code + "'"
                isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
            End If

            Dim coll As New Hashtable()

            If arr IsNot Nothing AndAlso arr.Count > 0 Then
                For Each objtr As clsSectionParameterMappingDetail In arr
                    coll = New Hashtable()
                    objtr.TR_Code = clsERPFuncationality.GetNextCode(trans, clsCommon.GETSERVERDATE(trans), clsDocType.Detail, clsDocTransactionType.Detail, "")
                    clsCommon.AddColumnsForChange(coll, "TR_Code", objtr.TR_Code)
                    clsCommon.AddColumnsForChange(coll, "Section_Code", DocSection_Code)
                    clsCommon.AddColumnsForChange(coll, "Consumption_Code", DocConsumption_Code)
                    clsCommon.AddColumnsForChange(coll, "Parameter_Code", objtr.Parameter_Code)
                    clsCommon.AddColumnsForChange(coll, "Parameter_Seq", objtr.Parameter_Seq)
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ENG_SECTION_PARAMETER_MAPPING", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If

            Return isSaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function GetData(ByVal strSection_Code As String, ByVal strConsumption_Code As String, ByVal trans As SqlTransaction) As List(Of clsSectionParameterMappingDetail)
        Dim arrListParameter As List(Of clsSectionParameterMappingDetail) = Nothing
        Dim qry As String

        qry = "SELECT TSPL_ENG_SECTION_PARAMETER_MAPPING.* FROM TSPL_ENG_SECTION_PARAMETER_MAPPING  where TSPL_ENG_SECTION_PARAMETER_MAPPING.Section_Code='" + strSection_Code + "'  and TSPL_ENG_SECTION_PARAMETER_MAPPING.Consumption_Code='" + strConsumption_Code + "'"

        Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qry)
        arrListParameter = New List(Of clsSectionParameterMappingDetail)
        If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
            For Each dr As DataRow In dt1.Rows
                Dim objtr As New clsSectionParameterMappingDetail
                objtr.Parameter_Code = clsCommon.myCstr(dr("Parameter_Code"))
                objtr.Parameter_Seq = CInt(dr("Parameter_Seq"))
                arrListParameter.Add(objtr)
            Next
        End If
        Return arrListParameter
    End Function
End Class

