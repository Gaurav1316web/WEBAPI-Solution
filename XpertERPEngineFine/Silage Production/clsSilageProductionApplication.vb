'=== Document Created by Kunal 
Imports common
Imports System.Data.SqlClient
Public Class clsSilageApplicationMaster

#Region "TSPL_SILAGE_CRITERIA_APPLICATION_MASTER : Variables"

    Public App_No As String = Nothing
    Public App_Type As String = Nothing
    '=============================
    Public Add1 As String = Nothing
    Public Add2 As String = Nothing
    Public Add3 As String = Nothing
    Public Country_Code As String = Nothing
    Public State_Code As String = Nothing
    Public City_Code As String = Nothing
    Public Zip_Code As String = Nothing
    Public Person As String = Nothing
    Public Phone1 As String = Nothing
    Public Phone2 As String = Nothing
    Public Email As String = Nothing
    Public WebSite As String = Nothing
    '==================================
    Public Entr_Type As String = Nothing
    Public Area_Code As String = Nothing
    Public Created_By As String = Nothing
    Public Created_Date As String = Nothing
    Public Modified_By As String = Nothing
    Public Modified_Date As String = Nothing
    '==================================
    Public Arr As List(Of clsSilageApplicationDetail) = Nothing
    '==================================
    Public arr_clsCriteriaValues As List(Of clsCriteriaValues) = Nothing

#End Region

    Public Shared Function SaveData(ByVal obj As clsSilageApplicationMaster, ByVal isNewEntry As Boolean) As Boolean
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
    Public Shared Function SaveData(ByVal obj As clsSilageApplicationMaster, ByVal isNewEntry As Boolean, ByVal trans As SqlClient.SqlTransaction) As Boolean
        Dim qry As String = Nothing
        Dim WhrCls As String = String.Empty

        Try
            If isNewEntry Then
                obj.App_No = clsERPFuncationality.GetNextCode(trans, clsCommon.GETSERVERDATE(trans), clsDocType.frmSilageProdApplicationForm, "", "")
            End If

            '= DELETE EXISTING CRITERIA DETAIL ==========
            clsSilageApplicationDetail.DeleteData(obj.App_No, trans)

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "App_No", clsCommon.myCstr(obj.App_No))
            clsCommon.AddColumnsForChange(coll, "App_Type", clsCommon.myCstr(obj.App_Type))
            clsCommon.AddColumnsForChange(coll, "Add1", clsCommon.myCstr(obj.Add1))
            clsCommon.AddColumnsForChange(coll, "Add2", clsCommon.myCstr(obj.Add2))
            clsCommon.AddColumnsForChange(coll, "Add3", clsCommon.myCstr(obj.Add3))
            clsCommon.AddColumnsForChange(coll, "Country_Code", clsCommon.myCstr(obj.Country_Code))
            clsCommon.AddColumnsForChange(coll, "State_Code", clsCommon.myCstr(obj.State_Code))
            clsCommon.AddColumnsForChange(coll, "City_Code", clsCommon.myCstr(obj.City_Code))
            clsCommon.AddColumnsForChange(coll, "Zip_Code", clsCommon.myCstr(obj.Zip_Code))
            clsCommon.AddColumnsForChange(coll, "Person", clsCommon.myCstr(obj.Person))
            clsCommon.AddColumnsForChange(coll, "Phone1", clsCommon.myCstr(obj.Phone1))
            clsCommon.AddColumnsForChange(coll, "Phone2", clsCommon.myCstr(obj.Phone2))
            clsCommon.AddColumnsForChange(coll, "Email", clsCommon.myCstr(obj.Email))
            clsCommon.AddColumnsForChange(coll, "WebSite", clsCommon.myCstr(obj.WebSite))
            clsCommon.AddColumnsForChange(coll, "Entr_Type", clsCommon.myCstr(obj.Entr_Type))
            clsCommon.AddColumnsForChange(coll, "Area_Code", clsCommon.myCstr(obj.Area_Code))
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
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SILAGE_CRITERIA_APPLICATION_MASTER", OMInsertOrUpdate.Insert, "", trans)
            Else
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SILAGE_CRITERIA_APPLICATION_MASTER", OMInsertOrUpdate.Update,
                                                             "TSPL_SILAGE_CRITERIA_APPLICATION_MASTER.App_No='" & obj.App_No & "'", trans)
            End If

            '= SAVE CRITERIA DETAILS ============================
            clsSilageApplicationDetail.SaveData(obj, trans)
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try

        Return True
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsSilageApplicationMaster
        Try
            Return GetData(strCode, NavType, Nothing)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsSilageApplicationMaster

        Dim obj As clsSilageApplicationMaster = Nothing
        Dim table As String = "TSPL_SILAGE_CRITERIA_APPLICATION_MASTER"
        Dim column As String = "App_No"
        Dim qry As String = Nothing

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

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            '================================================================================
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj = New clsSilageApplicationMaster()
                obj.App_No = clsCommon.myCstr(dt.Rows(0)("App_No"))
                obj.App_Type = clsCommon.myCstr(dt.Rows(0)("App_Type"))
                obj.Add1 = clsCommon.myCstr(dt.Rows(0)("Add1"))
                obj.Add2 = clsCommon.myCstr(dt.Rows(0)("Add2"))
                obj.Add3 = clsCommon.myCstr(dt.Rows(0)("Add3"))
                obj.Country_Code = clsCommon.myCstr(dt.Rows(0)("Country_Code"))
                obj.State_Code = clsCommon.myCstr(dt.Rows(0)("State_Code"))
                obj.City_Code = clsCommon.myCstr(dt.Rows(0)("City_Code"))
                obj.Zip_Code = clsCommon.myCstr(dt.Rows(0)("Zip_Code"))
                obj.Person = clsCommon.myCstr(dt.Rows(0)("Person"))
                obj.Phone1 = clsCommon.myCstr(dt.Rows(0)("Phone1"))
                obj.Phone2 = clsCommon.myCstr(dt.Rows(0)("Phone2"))
                obj.Email = clsCommon.myCstr(dt.Rows(0)("Email"))
                obj.WebSite = clsCommon.myCstr(dt.Rows(0)("WebSite"))
                obj.Entr_Type = clsCommon.myCstr(dt.Rows(0)("Entr_Type"))
                obj.Area_Code = clsCommon.myCstr(dt.Rows(0)("Area_Code"))
                obj.Created_By = clsCommon.myCstr(dt.Rows(0)("Created_By"))
                obj.Created_Date = clsCommon.myCstr(dt.Rows(0)("Created_Date"))
                obj.Modified_By = clsCommon.myCstr(dt.Rows(0)("Modified_By"))
                obj.Modified_Date = clsCommon.myCstr(dt.Rows(0)("Modified_Date"))

                obj.Arr = New List(Of clsSilageApplicationDetail)
                '================================================================================
                Dim dt1 As DataTable = clsSilageApplicationDetail.GetData(strCode, NavType, trans)
                If (dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0) Then
                    For Each dr As DataRow In dt1.Rows
                        Dim obj1 = New clsSilageApplicationDetail
                        obj1.Area_Code = clsCommon.myCstr(dr("Area_Code"))
                        obj1.Criteria_Code = clsCommon.myCstr(dr("Criteria_Code"))
                        obj1.Value = clsCommon.myCstr(dr("Value"))
                        obj1.Description = clsCommon.myCstr(dr("Description"))
                        '= ADD DETAIL'S Obj in MASTER =============
                        obj.Arr.Add(obj1)
                    Next
                End If

            End If
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try

        Return obj
    End Function
    Public Shared Function DeleteData(ByVal strCode As String) As Boolean

        Dim table As String = "TSPL_SILAGE_CRITERIA_APPLICATION_MASTER"
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Dim obj As clsSilageApplicationMaster = Nothing

        If (clsCommon.myLen(strCode) <= 0) Then
            Throw New Exception("Code not found to Delete")
        End If

        obj = New clsSilageApplicationMaster()
        obj = clsSilageApplicationMaster.GetData(strCode, NavigatorType.Current, trans)

        Try
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.App_No) > 0) Then
                If clsSilageApplicationDetail.DeleteData(strCode, trans) Then

                    Dim qry As String = "Delete from " & table & " where App_No = '" + strCode + "'"
                    If (clsDBFuncationality.ExecuteNonQuery(qry, trans)) Then
                        trans.Commit()
                        Return True
                    Else
                        trans.Rollback()
                        Return False
                    End If
                End If
            End If
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
            Return False
        End Try

        Return True
    End Function


End Class

Public Class clsSilageApplicationDetail

#Region "TSPL_SILAGE_CRITERIA_APPLICATION_DETAIL : Variables"

    Public App_No As String = Nothing
    Public Area_Code As String = Nothing
    Public Criteria_Code As String = Nothing
    Public Criteria_Name As String = Nothing
    Public Value As String = Nothing
    Public Description As String = Nothing

#End Region

    Public Shared Function SaveData(ByVal obj As clsSilageApplicationMaster, ByVal trans As SqlClient.SqlTransaction) As Boolean

        Dim table As String = "TSPL_SILAGE_CRITERIA_APPLICATION_DETAIL"
        Try
            For Each obj1 As clsSilageApplicationDetail In obj.Arr
                If (obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0) Then
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "App_No", clsCommon.myCstr(obj.App_No))
                    clsCommon.AddColumnsForChange(coll, "Area_Code", clsCommon.myCstr(obj1.Area_Code))
                    clsCommon.AddColumnsForChange(coll, "Criteria_Code", clsCommon.myCstr(obj1.Criteria_Code))
                    clsCommon.AddColumnsForChange(coll, "Value", clsCommon.myCstr(obj1.Value))
                    clsCommon.AddColumnsForChange(coll, "Description ", clsCommon.myCstr(obj1.Description))
                    clsCommonFunctionality.UpdateDataTable(coll, table, OMInsertOrUpdate.Insert, "", trans)
                End If
            Next

        Catch err As Exception
            Throw New Exception(err.Message)
        End Try

        Return True
    End Function
    Public Shared Function DeleteData(ByVal strCode As String, ByVal trans As SqlClient.SqlTransaction) As Boolean

        Dim dt As DataTable = Nothing
        Dim table As String = "TSPL_SILAGE_CRITERIA_APPLICATION_DETAIL"

        If (clsCommon.myLen(strCode) <= 0) Then
            Throw New Exception("Code not found to Delete")
        End If

        dt = New DataTable()
        dt = clsSilageApplicationDetail.GetData(strCode, NavigatorType.Current, trans)

        Try
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                Dim qry As String = "Delete from " & table & " where App_No = '" + strCode + "'"
                If (clsDBFuncationality.ExecuteNonQuery(qry, trans)) Then
                    Return True
                Else
                    Return False
                End If
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
            Return False
        End Try

        Return True
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As DataTable


        Dim table As String = "TSPL_SILAGE_CRITERIA_APPLICATION_DETAIL"
        Dim column As String = "App_No"
        Dim qry As String = Nothing
        Dim dt As DataTable = Nothing
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

            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry, trans)

        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return dt
    End Function

    Public Shared Function GetAreaCriterias(ByVal strAreaCode As String) As DataTable
        Dim dt As DataTable = New DataTable()
        Dim query As String = Nothing
        Try
            query = "select d.Area_Code AreaCode , m.Area_Name AreaName , d.Criteria_Code , d.Value , d.Description from  TSPL_SILAGE_AREA_CRITERIA_MASTER m LEFT JOIN TSPL_SILAGE_AREA_CRITERIA_DETAIL d ON m.Area_Code = d.Area_Code where m.Area_Code ='" & strAreaCode & "'"
            dt = clsDBFuncationality.GetDataTable(query, Nothing)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
        Return dt
    End Function
    Public Shared Function LoadApplicationArea(ByVal applicationNo As String) As DataTable
        Dim dt As DataTable = New DataTable()
        Dim query As String = Nothing
        Try
            query = "SELECT M.Area_Code, D.Criteria_Code, D.Value, D.Description FROM TSPL_SILAGE_CRITERIA_APPLICATION_MASTER M LEFT JOIN TSPL_SILAGE_CRITERIA_APPLICATION_DETAIL D ON M.App_No = D.App_No WHERE M.Area_Code = '" & applicationNo & "'"
            dt = clsDBFuncationality.GetDataTable(query, Nothing)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
        Return dt
    End Function

End Class

Public Class clsCriteriaValues

#Region "variables"

    Public criteriaCode As String = String.Empty
    Public criteriaValue As String = String.Empty

    'Public arr_clsCriteriaValues As List(Of clsCriteriaValues) = Nothing

#End Region

    Public Shared Function LoadApplicationsForms(ByVal areaCode As String, ByVal obj As clsSilageApplicationMaster) As DataTable
        Dim query As String = String.Empty
        Dim allCriterias As String = ""
        Dim allValues As String = String.Empty
        Dim dt As DataTable = Nothing
        Dim subStringCriteriaValuePair As String = String.Empty
        Dim QryCriteria As String = "select "
        Try
            For Each obj1 As clsCriteriaValues In obj.arr_clsCriteriaValues            
                QryCriteria = QryCriteria & "'" & obj1.criteriaCode & "' as Criteria_Code," & "'" & obj1.criteriaValue & "' as Criteria_Value union all select "
                'subStringCriteriaValuePair += "D.Criteria_Code = '" & obj1.criteriaCode & "' AND D.Value = '" & obj1.criteriaValue & "'" & ") or ( "
            Next
            QryCriteria = QryCriteria & " '' as Criteria_Code,'' as Criteria_Value"
            query = " SELECT distinct sApp.* FROM (select App.Area_Code,APP.App_No,APPD.Criteria_Code,APPD.Value AS Criteria_Value " & _
                    " from TSPL_SILAGE_CRITERIA_APPLICATION_DETAIL APPD " & _
                    " LEFT JOIN TSPL_SILAGE_CRITERIA_APPLICATION_MASTER APP ON APP.App_No=APPD.App_No) App " & _
                    " inner join (" & QryCriteria & ") Criteria on App.Criteria_Code=Criteria.Criteria_Code and App.Criteria_Value=Criteria.Criteria_Value " & _
                    " inner join TSPL_SILAGE_CRITERIA_APPLICATION_MASTER sApp on App.App_No=sApp.App_No " & _
                    " WHERE App.Area_Code='" & areaCode & "' AND App.App_No NOT IN ( SELECT App_No from TSPL_SILAGE_CRITERIA_ENTREPRENEUR_MASTER )"
            '     subStringCriteriaValuePair = subStringCriteriaValuePair
            'If clsCommon.myLen(areaCode) > 0 Then
            '    query = "select m.* from TSPL_SILAGE_CRITERIA_APPLICATION_MASTER m where m.App_No in " & _
            '             "(SELECT distinct D.App_No  FROM TSPL_SILAGE_CRITERIA_APPLICATION_DETAIL D" & _
            '             " WHERE D.Area_Code = '" & areaCode & "' AND (( " & subStringCriteriaValuePair & " )) "

            '    query = query.Replace(" or (  )", ")")
            '    query = query + "AND M.App_No NOT IN ( SELECT App_No from TSPL_SILAGE_CRITERIA_ENTREPRENEUR_MASTER )"
            'End If


            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(query, Nothing)
        Catch ex As Exception
        End Try
        Return dt
    End Function

    Public Shared Function GetCriteriaInfoByAppNo(ByVal appNo As String) As DataTable
        Dim dt As DataTable = Nothing
        Try
            Dim qry As String = " SELECT  Area_Code,Criteria_Code , Value, Description FROM TSPL_SILAGE_CRITERIA_APPLICATION_DETAIL WHERE App_No = '" & appNo & "'"
            dt = clsDBFuncationality.GetDataTable(qry, Nothing)
        Catch ex As Exception

        End Try
        Return dt
    End Function


    Public Shared Function LoadApplicationsForms(ByVal applicationNo As String, ByVal areaCode As String) As DataTable
        Dim query As String = String.Empty
        Dim dt As DataTable = Nothing
        Try
            If clsCommon.myLen(applicationNo) > 0 AndAlso clsCommon.myLen(areaCode) > 0 Then
                query = "select * from TSPL_SILAGE_CRITERIA_APPLICATION_MASTER where App_No = '" & applicationNo & "' AND Area_Code ='" & areaCode & "'"
                dt = New DataTable()
                dt = clsDBFuncationality.GetDataTable(query, Nothing)
            End If
        Catch ex As Exception
        End Try
        Return dt
    End Function
End Class