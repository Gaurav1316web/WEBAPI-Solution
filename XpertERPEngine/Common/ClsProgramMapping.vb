Imports System.Data.SqlClient
Imports common
Public Class ClsProgramMapping
#Region "variable"
    Public Program_Code As String
    Public Table_1 As String
    Public Table_2 As String
    Public Table_3 As String
    Public Table_4 As String
    Public Table_5 As String
#End Region
    Public Shared Function SaveData(ByVal Arr As List(Of ClsProgramMapping)) As Boolean

        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try

            For Each obj As ClsProgramMapping In Arr
                clsDBFuncationality.ExecuteNonQuery("Delete from TSPL_PROGRAM_TABLE_MAPPING WHERE Program_Code='" + obj.Program_Code + "'", trans)
                Dim IsSaved As Boolean = False
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Program_Code", obj.Program_Code)
                clsCommon.AddColumnsForChange(coll, "Table_1", obj.Table_1)
                clsCommon.AddColumnsForChange(coll, "Table_2", obj.Table_2)
                clsCommon.AddColumnsForChange(coll, "Table_3", obj.Table_3)
                clsCommon.AddColumnsForChange(coll, "Table_4", obj.Table_4)
                clsCommon.AddColumnsForChange(coll, "Table_5", obj.Table_5)
                IsSaved = clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PROGRAM_TABLE_MAPPING", OMInsertOrUpdate.Insert, "", trans)
            Next
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            clsCommon.MyMessageBoxShow(ex.Message)

        End Try
        Return True
    End Function

    Public Shared Function DeleteData(ByVal strcode As String, ByVal trans As SqlTransaction) As Boolean
        Try
            If (clsCommon.myLen(strcode >= 0)) Then
                Dim qry As String = "delete from TSPL_PROGRAM_TABLE_MAPPING where Program_Code='" + strcode + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
            End If
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function

    'Public Shared Function GetData(ByVal strcode As String) As ClsProgramMapping
    '    'Try
    '    '    Dim obj As ClsProgramMapping = Nothing
    '    '    Dim qry As String = " select Program_Code,Program_Name from TSPL_PROGRAM_MASTER where Parent_Code in (select Program_Code from TSPL_PROGRAM_MASTER "
    '    '    qry += "inner join TSPL_MODULE_PERMISSION on TSPL_MODULE_PERMISSION.Module_Name=TSPL_PROGRAM_MASTER.Parent_Code "
    '    '    qry += " where TSPL_PROGRAM_MASTER.Type='SM' and TSPL_PROGRAM_MASTER.Program_Name like '%Tran%' and TSPL_MODULE_PERMISSION.IsAvailable=1)"
    '    '    Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qry)
    '    '    If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
    '    '        obj = New ClsProgramMapping
    '    '        obj.Program_Code = clsCommon.myCstr(dt1.Rows(0)("Program_Code"))

    '    '    End If
    '    '    Return obj

    '    'Catch ex As Exception

    '    'End Try

    'End Function
End Class
Public Class ClsProgramMappingDetail
#Region "variable"
    Public Program_Code As String
    Public Table_Name As String
    Public Column_Name As String
    Public Column_Caption As String
#End Region
    Public Shared Function SaveData(ByVal Array As List(Of ClsProgramMappingDetail)) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            For Each strcode As ClsProgramMappingDetail In Array
                clsDBFuncationality.ExecuteNonQuery("Delete from TSPL_PROGRAM_MAPPING_DETAIL WHERE Program_Code='" + strcode.Program_Code + "'", trans)
            Next
            For Each obj As ClsProgramMappingDetail In Array
                'clsDBFuncationality.ExecuteNonQuery("Delete from TSPL_PROGRAM_MAPPING_DETAIL WHERE Program_Code='" + obj.Program_Code + "'", trans)
                Dim IsSaved As Boolean = False
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Program_Code", obj.Program_Code)
                clsCommon.AddColumnsForChange(coll, "Table_Name", obj.Table_Name)
                clsCommon.AddColumnsForChange(coll, "Column_Name", obj.Column_Name)
                clsCommon.AddColumnsForChange(coll, "Column_Caption", obj.Column_Caption)

                IsSaved = clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PROGRAM_MAPPING_DETAIL", OMInsertOrUpdate.Insert, "", trans)
            Next
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            clsCommon.MyMessageBoxShow(ex.Message)

        End Try
        Return True
    End Function
End Class
