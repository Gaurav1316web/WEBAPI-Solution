'=== Document Created by Kunal 
Imports common
Imports System.Data.SqlClient
Public Class clsSilageProductionAreaMaster

#Region "TSPL_SILAGE_AREA_CRITERIA_MASTER : Variables"
    Public Area_Code As String = Nothing
    Public Area_Name As String = Nothing
    Public Created_By As String = Nothing
    Public Created_Date As String = Nothing
    Public Modified_By As String = Nothing
    Public Modified_Date As String = Nothing
    Public Current_Status = 0
    Public ArrayDetailCls As List(Of clsSilageProductionAreaDetails) = Nothing
#End Region

    Public Shared Function SaveData(ByVal obj As clsSilageProductionAreaMaster, ByVal isNewEntry As Boolean) As Boolean
        Dim trans As SqlClient.SqlTransaction = clsDBFuncationality.GetTransactin
        Try
            If (SaveData(obj, isNewEntry, trans)) = True Then
                trans.Commit()
                Return True
            Else
                trans.Rollback()
                Return False
            End If

        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Shared Function SaveData(ByVal obj As clsSilageProductionAreaMaster, ByVal isNewEntry As Boolean, ByVal trans As SqlClient.SqlTransaction) As Boolean
        Try
            '= DELETE EXISTING CRITERIA DETAIL ==============================
            clsSilageProductionAreaDetails.DeleteData(obj.Area_Code, trans)

            ' SAVE AREA MASTER ENTRIES ===============================
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Area_Code", obj.Area_Code)
            clsCommon.AddColumnsForChange(coll, "Area_Name", obj.Area_Name)
            clsCommon.AddColumnsForChange(coll, "Current_Status", obj.Current_Status)
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
            Else
                clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
            End If
            If isNewEntry Then
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SILAGE_AREA_CRITERIA_MASTER", OMInsertOrUpdate.Insert, "", trans)
            Else
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SILAGE_AREA_CRITERIA_MASTER", OMInsertOrUpdate.Update,
                                                             "TSPL_SILAGE_AREA_CRITERIA_MASTER.Area_Code='" & obj.Area_Code & "'", trans)
            End If

            '= SAVE CRITERIA DETAILS ===========================================
            clsSilageProductionAreaDetails.SaveData(obj, isNewEntry, trans)

        Catch err As Exception
            Throw New Exception(err.Message)
        End Try

        Return True
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsSilageProductionAreaMaster
        Try
            Return GetData(strCode, NavType, Nothing)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsSilageProductionAreaMaster

        Dim qry As String = Nothing
        Dim column As String = "Area_Code"
        Dim table As String = "TSPL_SILAGE_AREA_CRITERIA_MASTER"
        Dim obj As clsSilageProductionAreaMaster = Nothing

        Try
            qry = "SELECT * FROM " & table & " where 2=2 "
            Select Case NavType
                Case NavigatorType.First
                    qry += " and " & table & "." & column & "  = (select MIN(" & column & ") from " & table & " where 1=1 )"
                Case NavigatorType.Last
                    qry += " and " & table & "." & column & "  = (select Max(" & column & ") from " & table & " where 1=1 )"
                Case NavigatorType.Next
                    qry += " and " & table & "." & column & "  = (select Min(" & column & ") from " & table & " where " & column & ">'" & strCode & "' )"
                Case NavigatorType.Previous
                    qry += " and " & table & "." & column & "  = (select Max(" & column & ") from " & table & " where " & column & "<'" & strCode & "' )"
                Case NavigatorType.Current
                    qry += " and " & table & "." & column & "  = '" + strCode + "'"
            End Select

            '==========================================================
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj = New clsSilageProductionAreaMaster()
                obj.Area_Code = clsCommon.myCstr(dt.Rows(0)("Area_Code"))
                obj.Area_Name = clsCommon.myCstr(dt.Rows(0)("Area_Name"))
                obj.Current_Status = clsCommon.myCstr(dt.Rows(0)("Current_Status"))
                obj.Created_By = clsCommon.myCstr(dt.Rows(0)("Created_By"))
                obj.Created_Date = clsCommon.myCstr(dt.Rows(0)("Created_Date"))
                obj.Modified_By = clsCommon.myCstr(dt.Rows(0)("Modified_By"))
                obj.Modified_Date = clsCommon.myCstr(dt.Rows(0)("Modified_Date"))
             
                obj.ArrayDetailCls = New List(Of clsSilageProductionAreaDetails)
                '==========================================================
                Dim dt1 As DataTable = clsSilageProductionAreaDetails.GetData(strCode, NavType, trans)
                Dim objD As clsSilageProductionAreaDetails = Nothing
                If (dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0) Then
                    For Each dr As DataRow In dt1.Rows
                        objD = New clsSilageProductionAreaDetails()
                        objD.Area_Code = clsCommon.myCstr(dr("Area_Code"))
                        objD.Criteria_Code = clsCommon.myCstr(dr("Criteria_Code"))
                        objD.Value = clsCommon.myCstr(dr("Value"))
                        objD.Description = clsCommon.myCstr(dr("Description"))
                        obj.ArrayDetailCls.Add(objD)
                    Next
                End If
                '==========================================================
            End If

        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return obj
    End Function

    Public Shared Function DeleteData(ByVal strCode As String) As Boolean

        Dim tableAreaMaster As String = "TSPL_SILAGE_AREA_CRITERIA_MASTER"
        Dim tableAreaDetail As String = "TSPL_SILAGE_AREA_CRITERIA_DETAIL"
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Dim obj As clsSilageProductionAreaMaster = Nothing

        If (clsCommon.myLen(strCode) <= 0) Then
            Throw New Exception("Code not found to Delete")
        End If

        obj = New clsSilageProductionAreaMaster()
        obj = clsSilageProductionAreaMaster.GetData(strCode, NavigatorType.Current, trans)
        '===============================================================================================
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Area_Code) > 0) Then
            Try
                If clsSilageProductionAreaDetails.DeleteData(obj.Area_Code, trans) = True Then
                    Dim qry As String = "Delete from " & tableAreaMaster & " where Area_Code = '" + strCode + "'"
                    If (clsDBFuncationality.ExecuteNonQuery(qry, trans)) Then
                        trans.Commit()
                        Return True
                    Else
                        trans.Rollback()
                        Return False
                    End If
                End If
            Catch ex As Exception
                trans.Rollback()
                Throw New Exception(ex.Message)
                Return False
            End Try
        End If
        '===============================================================================================
        Return True
    End Function

End Class
Public Class clsSilageProductionAreaDetails

#Region "TSPL_SILAGE_AREA_CRITERIA_DETAIL : Variables "

    Public Area_Code As String = Nothing
    Public Criteria_Code As String = Nothing
    Public Value As String = Nothing
    Public Description As String = Nothing

#End Region

    Public Shared Function SaveData(ByVal obj As clsSilageProductionAreaMaster, ByVal isNewEntry As Boolean, ByVal trans As SqlClient.SqlTransaction) As Boolean

        Dim table As String = "TSPL_SILAGE_AREA_CRITERIA_DETAIL"
        Try
            If obj.ArrayDetailCls IsNot Nothing AndAlso obj.ArrayDetailCls.Count > 0 Then
                For Each obj1 As clsSilageProductionAreaDetails In obj.ArrayDetailCls
                    If clsCommon.myLen(obj1.Area_Code) > 0 Then

                        Dim coll As New Hashtable()
                        clsCommon.AddColumnsForChange(coll, "Area_Code", obj1.Area_Code)
                        clsCommon.AddColumnsForChange(coll, "Criteria_Code", obj1.Criteria_Code)
                        clsCommon.AddColumnsForChange(coll, "Value", obj1.Value)
                        clsCommon.AddColumnsForChange(coll, "Description", obj1.Description)

                        '= INSERT INTO TABLE : TSPL_SILAGE_AREA_CRITERIA_DETAIL ==================================
                        clsCommonFunctionality.UpdateDataTable(coll, table, OMInsertOrUpdate.Insert, "", trans)
                    End If

                Next

            End If
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try

        Return True
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As DataTable

        Dim obj As clsSilageProductionAreaDetails = Nothing
        Dim qry As String = Nothing
        Dim dt As DataTable = Nothing
        Dim table As String = "TSPL_SILAGE_AREA_CRITERIA_DETAIL"
        Dim column As String = "Area_Code"
            '==========================================================
        Try
            qry = "SELECT * FROM " & table & " where 2=2 "
            Select Case NavType
                Case NavigatorType.First
                    qry += " and " & table & "." & column & "  = (select MIN(" & column & ") from " & table & " where 1=1 )"
                Case NavigatorType.Last
                    qry += " and " & table & "." & column & "  = (select Max(" & column & ") from " & table & " where 1=1 )"
                Case NavigatorType.Next
                    qry += " and " & table & "." & column & "  = (select Min(" & column & ") from " & table & " where " & column & ">'" & strCode & "' )"
                Case NavigatorType.Previous
                    qry += " and " & table & "." & column & "  = (select Max(" & column & ") from " & table & " where " & column & "<'" & strCode & "' )"
                Case NavigatorType.Current
                    qry += " and " & table & "." & column & "  = '" + strCode + "'"
            End Select
            qry += " union select '' Area_Code , Criteria_Code ,'No' as Value , '' Description from  TSPL_SILAGE_CRITERIA_MASTER order by Area_Code desc"
            dt = clsDBFuncationality.GetDataTable(qry, trans)

        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return dt
    End Function
    Public Shared Function DeleteData(ByVal strCode As String, ByVal trans As SqlClient.SqlTransaction) As Boolean

        'Dim dt As DataTable = Nothing
        Dim table As String = "TSPL_SILAGE_AREA_CRITERIA_DETAIL"
        '=======================================================================
        Try
            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Code not found to Delete")
            End If
            '=======================================================================
            'dt = clsSilageProductionAreaDetails.GetData(strCode, NavigatorType.Current, trans)
            'If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            Dim qry As String = "Delete from " & table & " where Area_Code = '" & strCode & "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            'If (clsDBFuncationality.ExecuteNonQuery(qry, trans)) Then
            '    Return True
            'Else
            '    Return False
            'End If
            'End If
            '=======================================================================
        Catch ex As Exception            
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

End Class