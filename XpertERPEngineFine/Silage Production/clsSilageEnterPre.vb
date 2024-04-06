'== Develop by Kunal =====
Imports System.Data.SqlClient

Public Class clsSilageEntreDocumentGenerator

#Region "TSPL_SILAGE_ENTREPRENEUR_DOCUMENT_MASTER : Variables"

    Public ES_Code As String = Nothing
    Public Area_Code As String = Nothing
    Public Created_By As String = Nothing
    Public Created_Date As String = Nothing
    Public Modified_By As String = Nothing
    Public Modified_Date As String = Nothing
    '==============================================
    Public Arr_clsSilageEntrMaster As List(Of clsSilageEntrMaster) = Nothing
    Public Arr_clsSilageEntrDetails As List(Of clsSilageEntrDetails) = Nothing
    'Public Arr_clsGenerateVendor As List(Of clsGenerateVendor) = Nothing

#End Region

    Public Shared Function SaveData(ByVal obj As clsSilageEntreDocumentGenerator, ByVal isNewEntry As Boolean) As Boolean
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
            Return False
        End Try
    End Function
    Public Shared Function SaveData(ByVal obj As clsSilageEntreDocumentGenerator, isNewEntry As Boolean, trans As SqlTransaction) As Boolean
        Try
            If isNewEntry AndAlso obj.ES_Code Is Nothing Then

                '==============================================================================================
                Dim qry As String = "SELECT MAX(ES_Code) FROM TSPL_SILAGE_ENTREPRENEUR_DOCUMENT_MASTER"
                Dim escode As String = String.Empty
                If clsCommon.myLen(clsDBFuncationality.getSingleValue(qry, trans)) > 0 Then
                    escode = clsDBFuncationality.getSingleValue(qry, trans)
                End If
                If clsCommon.myLen(escode) <= 0 Then
                    obj.ES_Code = "ES00001"
                Else
                    obj.ES_Code = clsCommon.incval(escode)
                End If

                '' delete all criteria from detail
                qry = "delete from TSPL_SILAGE_CRITERIA_ENTREPRENEUR_DETAIL where ES_Code='" & obj.ES_Code & "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)

                '==============================================================================================
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "ES_Code", clsCommon.myCstr(obj.ES_Code))
                clsCommon.AddColumnsForChange(coll, "Area_Code", clsCommon.myCstr(obj.Area_Code))
                If isNewEntry = True Then
                    clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
                    clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
                Else
                    clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
                End If
                If isNewEntry = True Then
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SILAGE_ENTREPRENEUR_DOCUMENT_MASTER", OMInsertOrUpdate.Insert, "", trans)
                Else
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SILAGE_ENTREPRENEUR_DOCUMENT_MASTER", OMInsertOrUpdate.Update,
                                                                 "TSPL_SILAGE_ENTREPRENEUR_DOCUMENT_MASTER.ES_Code = '" & obj.ES_Code & "'", trans)
                End If
            End If

            '= SAVE IN ENTR-MASTER-TABLE AGAINST SAME DOC NO ======
            '======================================================
            clsSilageEntrMaster.SaveData(obj, trans)
            clsSilageEntrDetails.SaveData(obj.ES_Code, obj, trans)

        Catch err As Exception

            Throw New Exception(err.Message)
        End Try

        Return True
    End Function
    Public Shared Function DeleteData(ByVal ES_Code As String) As Boolean
        Dim isSaved As Boolean = True
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin        
        '======================================
        Try

            If (clsCommon.myLen(ES_Code) <= 0) Then
                Throw New Exception("Code not found to delete")
            End If
            Dim qry As String = ""
            qry = "delete from TSPL_SILAGE_CRITERIA_ENTREPRENEUR_DETAIL where ES_CODE='" & ES_Code & "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "Delete from TSPL_VENDOR_MASTER where Vendor_Code  = '" & ES_Code & "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "Delete from TSPL_SILAGE_CRITERIA_ENTREPRENEUR_MASTER where ES_Code  = '" & ES_Code & "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

          
            qry = "delete from TSPL_SILAGE_ENTREPRENEUR_DOCUMENT_MASTER where ES_Code  = '" & ES_Code & "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            trans.Commit()
        Catch Err As Exception
            trans.Rollback()
            Throw New Exception(Err.Message)            
        End Try

        Return True
    End Function
    Public Shared Function GetData(ByVal esCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsSilageEntreDocumentGenerator

        Dim obj As clsSilageEntreDocumentGenerator = Nothing
        Dim objM As clsSilageEntrMaster = Nothing
        Dim objD As clsSilageEntrDetails = Nothing
        'Dim objV As clsGenerateVendor = Nothing
        '==========================================================================
        Dim qry As String = String.Empty
        Dim table As String = "TSPL_SILAGE_ENTREPRENEUR_DOCUMENT_MASTER"
        Dim column As String = "ES_Code"

        Try
            qry = "SELECT * FROM " & table & " WHERE 1=1"
            '==========================================================================
            Select Case NavType

                Case NavigatorType.First
                    qry += " and " & table & "." & column & "  = (select MIN(" & column & ") from " & table & " where 1=1 )"
                Case NavigatorType.Last
                    qry += " and " & table & "." & column & "  = (select Max(" & column & ") from " & table & " where 1=1 )"
                Case NavigatorType.Next
                    qry += " and " & table & "." & column & "  = (select Min(" & column & ") from " & table & " where " & column & ">'" & esCode & "' )"
                Case NavigatorType.Previous
                    qry += " and " & table & "." & column & "  = (select Max(" & column & ") from " & table & " where " & column & "<'" & esCode & "' )"
                Case NavigatorType.Current
                    qry += " and " & table & "." & column & "  = '" + esCode + "'"
            End Select

            obj = New clsSilageEntreDocumentGenerator()
            '==================================================
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                For Each dr As DataRow In dt.Rows
                    obj.ES_Code = clsCommon.myCstr(dt.Rows(0)("ES_Code"))
                    obj.Area_Code = clsCommon.myCstr(dt.Rows(0)("Area_Code"))
                    obj.Created_By = clsCommon.myCstr(dt.Rows(0)("Created_By"))
                    obj.Created_Date = clsCommon.myCstr(dt.Rows(0)("Created_Date"))
                    obj.Modified_By = clsCommon.myCstr(dt.Rows(0)("Modified_By"))
                    obj.Modified_Date = clsCommon.myCstr(dt.Rows(0)("Modified_Date"))

                Next
            End If

            obj.Arr_clsSilageEntrMaster = New List(Of clsSilageEntrMaster)
            '=====================================================================================
            Dim count As Integer = 0
            Dim dtEm As DataTable = clsSilageEntrMaster.GetData(obj.ES_Code, NavType, trans)
            If (dtEm IsNot Nothing AndAlso dtEm.Rows.Count > 0) Then
                For Each dr As DataRow In dtEm.Rows
                    If count <= dtEm.Rows.Count Then
                        objM = New clsSilageEntrMaster()
                        '==================================================
                        objM.Entr_Code = clsCommon.myCstr(dtEm.Rows(count)("Entr_Code"))
                        objM.ES_Code = clsCommon.myCstr(dtEm.Rows(count)("ES_Code"))
                        objM.App_No = clsCommon.myCstr(dtEm.Rows(count)("App_No"))
                        objM.App_Type = clsCommon.myCstr(dtEm.Rows(count)("App_Type"))
                        objM.Entr_Type = clsCommon.myCstr(dtEm.Rows(count)("Entr_Type"))
                        objM.Area_Code = clsCommon.myCstr(dtEm.Rows(count)("Area_Code"))
                        objM.CommissionType = clsCommon.myCstr(dtEm.Rows(count)("CommissionType"))
                        objM.Created_By = clsCommon.myCstr(dtEm.Rows(count)("Created_By"))
                        objM.Created_Date = clsCommon.myCstr(dtEm.Rows(count)("Created_Date"))
                        objM.Modified_By = clsCommon.myCstr(dtEm.Rows(count)("Modified_By"))
                        objM.Modified_Date = clsCommon.myCstr(dtEm.Rows(count)("Modified_Date"))
                        objM.objVendor = clsGenerateVendor.GetData(objM.Entr_Code, trans)
                        '===========================================
                        obj.Arr_clsSilageEntrMaster.Add(objM)

                        count = count + 1
                    End If
                Next
            End If


            obj.Arr_clsSilageEntrDetails = New List(Of clsSilageEntrDetails)
            Dim edDt As DataTable = clsSilageEntrDetails.GetData(esCode, NavType, trans)
            If (edDt IsNot Nothing AndAlso edDt.Rows.Count > 0) Then
                For Each dr As DataRow In edDt.Rows
                    objD = New clsSilageEntrDetails()

                    '==============================================================
                    objD.ES_Code = esCode 'clsCommon.myCstr(dr("ES_Code"))
                    'objD.App_No = clsCommon.myCstr(dr("App_No"))
                    'objD.Area_Code = clsCommon.myCstr(dr("Area_Code"))
                    objD.Criteria_Code = clsCommon.myCstr(dr("Criteria_Code"))
                    objD.Criteria_Name = clsCommon.myCstr(dr("Criteria_Name"))
                    objD.Value = clsCommon.myCstr(dr("Value"))
                    objD.Description = clsCommon.myCstr(dr("Description"))
                    obj.Arr_clsSilageEntrDetails.Add(objD)

                Next
            End If


        Catch err As Exception
            Throw New Exception(err.Message)
        End Try

        Return obj
    End Function
    Public Shared Function GetData(ByVal colES_Code As String, ByVal transaction As SqlTransaction) As DataTable

        Dim selectQry As String = String.Empty
        Dim dt As DataTable = Nothing
        Try
            selectQry = "select d.ES_Code, d.Area_Code, m.Entr_Code, m.App_No, m.CommissionType, c.Criteria_Code, c.Value, c.Description from TSPL_SILAGE_ENTREPRENEUR_DOCUMENT_MASTER d left join TSPL_SILAGE_CRITERIA_ENTREPRENEUR_MASTER m on d.ES_Code = m.ES_Code left join TSPL_SILAGE_CRITERIA_ENTREPRENEUR_DETAIL c on m.ES_Code = c.ES_Code where d.ES_Code = '" & colES_Code & "'"
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(selectQry, transaction)

        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return dt

    End Function
    Public Shared Function GetData(ByVal esCode As String) As DataTable
        Dim DataTable As DataTable = Nothing
        Try
            Dim qry As String = "SELECT  DM.ES_Code, DM.Area_Code, EM.Entr_Code, EM.App_No, EM.CommissionType ,ED.Criteria_Code, ED.Value, ED.Description ,v.Vendor_Code, v.Vendor_Name, v.State_Code, v.Country_Code, v.Is_Parent_Vendor, v.Parent_Vendor_Code, v.Vendor_Type, v.Pin_Code, v.Add1, v.Add2, v.Add3, v.City_Code,  v.State, v.Country,  v.Phone1, v.Phone2,  v.Email,  v.WebSite,  v.Contact_Person_Name, v.Transporter  FROM TSPL_SILAGE_ENTREPRENEUR_DOCUMENT_MASTER DM  LEFT JOIN TSPL_SILAGE_CRITERIA_ENTREPRENEUR_MASTER EM ON DM.ES_Code = EM.ES_Code LEFT JOIN TSPL_SILAGE_CRITERIA_ENTREPRENEUR_DETAIL ED ON EM.ES_Code = ED.ES_Code LEFT JOIN TSPL_VENDOR_MASTER V ON V.Vendor_Code = EM.Entr_Code WHERE DM.ES_Code ='" & esCode & "'"
            DataTable = New DataTable()
            DataTable = clsDBFuncationality.GetDataTable(qry, Nothing)

        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return DataTable
    End Function

    Public Shared Function Entre_DataTable(ByVal esCode As String) As DataTable

        Dim qry As String = String.Empty
        Dim dt As DataTable = Nothing
        Try
            If clsCommon.myLen(esCode) > 0 Then
                qry = "SELECT D.ES_Code , M.App_No , M.Entr_Code , m.CommissionType FROM TSPL_SILAGE_ENTREPRENEUR_DOCUMENT_MASTER D LEFT JOIN TSPL_SILAGE_CRITERIA_ENTREPRENEUR_MASTER M ON D.ES_Code = M.ES_Code WHERE D.ES_Code = '" & esCode & "' "
            End If
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry, Nothing)
        Catch ex As Exception
        End Try

        Return dt
    End Function

End Class
Public Class clsSilageEntrMaster

#Region "TSPL_SILAGE_CRITERIA_ENTREPRENEUR_MASTER : Variables"

    Public Entr_Code As String = Nothing
    Public ES_Code As String = Nothing
    Public App_No As String = Nothing
    Public Area_Code As String = Nothing
    Public App_Type As String = Nothing
    Public Entr_Type As String = Nothing
    
    Public CommissionType As String = Nothing
    Public Arr_CriteriasInfo As List(Of clsSilageEntrDetails) = Nothing
    Public Created_By As String = Nothing
    Public Created_Date As String = Nothing
    Public Modified_By As String = Nothing
    Public Modified_Date As String = Nothing
    Public objVendor As clsGenerateVendor
    'Public arr_DocumentMaster As List(Of clsSilageEntreDocumentGenerator) = Nothing


#End Region

    Public Shared Function SaveData(ByVal obj As clsSilageEntreDocumentGenerator, ByVal trans As SqlClient.SqlTransaction) As Boolean
        Dim coll As Hashtable = Nothing
        Try
            If obj.Arr_clsSilageEntrMaster IsNot Nothing AndAlso obj.Arr_clsSilageEntrMaster.Count > 0 Then

                For Each objM As clsSilageEntrMaster In obj.Arr_clsSilageEntrMaster
                    Dim qry As String = "select count(Entr_Code) as total from TSPL_SILAGE_CRITERIA_ENTREPRENEUR_MASTER where Entr_Code='" & objM.Entr_Code & "'"
                    Dim isNewEntry As Boolean = IIf(clsCommon.myCdbl(clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))) > 0, False, True)

                    If isNewEntry Then
                        objM.Entr_Code = clsERPFuncationality.GetNextCode(trans, clsCommon.GETSERVERDATE(trans), clsDocType.frmSilageEnterPrenur, "", "")
                    End If

                    '  clsSilageEntrDetails.DeleteData(objM.App_No, trans)
                    '================================================================
                    coll = New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Entr_Code", objM.Entr_Code)
                    clsCommon.AddColumnsForChange(coll, "ES_Code", obj.ES_Code)
                    clsCommon.AddColumnsForChange(coll, "App_No", objM.App_No)
                    clsCommon.AddColumnsForChange(coll, "Area_Code", obj.Area_Code)
                    clsCommon.AddColumnsForChange(coll, "CommissionType", objM.CommissionType, True)
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
                        clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SILAGE_CRITERIA_ENTREPRENEUR_MASTER", OMInsertOrUpdate.Insert, "", trans)
                    Else
                        clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SILAGE_CRITERIA_ENTREPRENEUR_MASTER", OMInsertOrUpdate.Update,
                                                                     "TSPL_SILAGE_CRITERIA_ENTREPRENEUR_MASTER.ES_Code = '" & objM.ES_Code & "'", trans)
                    End If
                    objM.objVendor.Vendor_Code = objM.Entr_Code
                    clsGenerateVendor.SaveData(objM.objVendor, trans)
                Next
            End If
        Catch err As Exception
            Throw New Exception(err.Message)
            Return False
        End Try

        Return True
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal transaction As SqlTransaction) As DataTable

        Dim qry As String = String.Empty
        Dim column As String = "ES_Code"
        Dim dt As DataTable = Nothing
        Dim table As String = "TSPL_SILAGE_CRITERIA_ENTREPRENEUR_MASTER"
        Try
            qry = "SELECT ent.*,app.App_Type,app.Entr_Type FROM TSPL_SILAGE_CRITERIA_ENTREPRENEUR_MASTER ent left join TSPL_SILAGE_CRITERIA_APPLICATION_MASTER  app on ent.App_No=app.App_No WHERE 1=1"
            '=============================================================================================================================
            Select Case NavType
                Case NavigatorType.First
                    qry += " and ent." & column & "  = (select MIN(" & column & ") from " & table & " where 1=1 )"
                Case NavigatorType.Last
                    qry += " and ent." & column & "  = (select Max(" & column & ") from " & table & " where 1=1 )"
                Case NavigatorType.Next
                    qry += " and ent." & column & "  = (select Min(" & column & ") from " & table & " where " & column & ">'" & strCode & "' )"
                Case NavigatorType.Previous
                    qry += " and ent." & column & "  = (select Max(" & column & ") from " & table & " where " & column & "<'" & strCode & "' )"
                Case NavigatorType.Current
                    qry += " and ent." & column & "  = '" + strCode + "'"
            End Select
            '=============================================================================================================================
            dt = clsDBFuncationality.GetDataTable(qry, transaction)

        Catch err As Exception
            Throw New Exception(err.Message)
        End Try

        Return dt
    End Function
    Public Shared Function DeleteData(ByVal esCode As String, ByVal transaction As SqlClient.SqlTransaction) As Boolean
        Dim qry As String = String.Empty
        Try
            If clsSilageEntrDetails.DeleteData(esCode, transaction) = True Then
                '=================================================================
                qry = "Delete from TSPL_SILAGE_CRITERIA_ENTREPRENEUR_MASTER where ES_Code  = '" & esCode & "'"
                If (clsDBFuncationality.ExecuteNonQuery(qry, transaction)) Then
                    Return True
                Else
                    Return False
                End If
            End If
        Catch Err As Exception
            Throw New Exception(Err.Message)
            Return False
        End Try

        Return True
    End Function

End Class

Public Class clsSilageEntrDetails

#Region "TSPL_SILAGE_CRITERIA_ENTREPRENEUR_DETAIL : Variables"

    Public ES_Code As String = Nothing
    Public App_No As String = Nothing
    Public Area_Code As String = Nothing
    Public Criteria_Code As String = Nothing
    Public Criteria_Name As String = Nothing
    Public Value As String = Nothing
    Public Description As String = Nothing


#End Region

    Public Shared Function SaveData(ByVal esCode As String, ByVal obj As clsSilageEntreDocumentGenerator, trans As SqlTransaction) As Boolean

        Dim coll As Hashtable = Nothing
        Try
            If obj IsNot Nothing AndAlso obj.Arr_clsSilageEntrDetails IsNot Nothing AndAlso obj.Arr_clsSilageEntrDetails.Count > 0 Then
                For Each oDtl As clsSilageEntrDetails In obj.Arr_clsSilageEntrDetails
                    coll = New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "ES_Code", esCode)
                    clsCommon.AddColumnsForChange(coll, "App_No", oDtl.App_No)
                    clsCommon.AddColumnsForChange(coll, "Area_Code", obj.Area_Code)
                    clsCommon.AddColumnsForChange(coll, "Criteria_Code", oDtl.Criteria_Code)
                    clsCommon.AddColumnsForChange(coll, "Value", oDtl.Value)
                    clsCommon.AddColumnsForChange(coll, "Description", oDtl.Description)
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SILAGE_CRITERIA_ENTREPRENEUR_DETAIL", OMInsertOrUpdate.Insert, "", trans)
                    'If isNewEntry Then
                    '    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SILAGE_CRITERIA_ENTREPRENEUR_DETAIL", OMInsertOrUpdate.Insert, "", trans)
                    'Else
                    '    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SILAGE_CRITERIA_ENTREPRENEUR_DETAIL", OMInsertOrUpdate.Update,                    '                                                 "TSPL_SILAGE_CRITERIA_ENTREPRENEUR_DETAIL.App_No = '" & obj.App_No & "'", trans)
                    'End If
                Next
            End If
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try

        Return True
    End Function
    Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As DataTable

        Dim qry As String = String.Empty
        Dim dt As DataTable = Nothing
        Dim table As String = "TSPL_SILAGE_CRITERIA_ENTREPRENEUR_DETAIL"
        Dim column As String = "ES_Code"

        Try
            qry = "SELECT distinct Critd.Criteria_Code,Critd.Value,Critd.Description,Crit.Name as Criteria_Name FROM " & table & " Critd left join TSPL_SILAGE_CRITERIA_MASTER Crit on Critd.Criteria_Code=Crit.Criteria_Code WHERE 1=1"
            '=========================================================
            Select Case NavType
                Case NavigatorType.First
                    qry += " and Critd." & column & "  = (select MIN(" & column & ") from " & table & " where 1=1 )"
                Case NavigatorType.Last
                    qry += " and Critd." & column & "  = (select Max(" & column & ") from " & table & " where 1=1 )"
                Case NavigatorType.Next
                    qry += " and Critd." & column & "  = (select Min(" & column & ") from " & table & " where " & column & ">'" & strCode & "' )"
                Case NavigatorType.Previous
                    qry += " and Critd." & column & "  = (select Max(" & column & ") from " & table & " where " & column & "<'" & strCode & "' )"
                Case NavigatorType.Current
                    qry += " and Critd." & column & "  = '" + strCode + "'"
            End Select
            '=========================================================
            dt = clsDBFuncationality.GetDataTable(qry, trans)
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try

        Return dt
    End Function
    Public Shared Function DeleteData(ByVal appNo As String, ByVal transaction As SqlClient.SqlTransaction) As Boolean

        Dim deleteQuery As String = String.Empty
        Try
            deleteQuery = " DELETE FROM  [TSPL_SILAGE_CRITERIA_ENTREPRENEUR_DETAIL] where App_No = '" & appNo & "'"
            If (clsDBFuncationality.ExecuteNonQuery(deleteQuery, transaction)) Then
                Return True
            Else
                Return False
            End If
        Catch Err As Exception
            Throw New Exception(Err.Message)
            Return False
        End Try

        Return True
    End Function

End Class

Public Class clsGenerateVendor

#Region "TSPL_VENDOR_MASTER : Variables"

    Public App_No As String = Nothing
    Public Person As String = Nothing
    Public State_Code As String = Nothing
    Public Country_Code As String = Nothing
    Public Is_Parent_Vendor As String = Nothing
    Public Parent_Vendor_Code As String = Nothing
    Public Entr_Type As String = Nothing
    Public Zip_Code As String = Nothing
    Public Vendor_Code As String = Nothing
    Public Vendor_Name As String = Nothing
    Public Add1 As String = Nothing
    Public Add2 As String = Nothing
    Public Add3 As String = Nothing
    Public City_Code As String = Nothing
    Public State As String = Nothing
    Public Country As String = Nothing
    Public Phone1 As String = Nothing
    Public Phone2 As String = Nothing
    Public Fax As String = Nothing
    Public Email As String = Nothing
    Public WebSite As String = Nothing
    Public Contact_Person_Name As String = Nothing
    Public Created_By As String = Nothing
    Public Created_Date As String = Nothing
    Public Modify_By As String = Nothing
    Public Modify_Date As String = Nothing
    Public Transporter = "N"
    Public Vendor_Type As String = Nothing
    Public Pin_Code As String = Nothing
    '===========================================

#End Region

    Public Shared Function SaveData(ByVal oVendor As clsGenerateVendor, trans As SqlTransaction) As Boolean
        Try
            'For Each oVendor As clsGenerateVendor In obj.Arr_clsGenerateVendor
            If oVendor IsNot Nothing AndAlso clsCommon.myLen(oVendor.Vendor_Code) > 0 Then
                Dim coll As New Hashtable()

                Dim qry As String = "select count(Vendor_Code) from TSPL_VENDOR_MASTER where Vendor_Code='" & oVendor.Vendor_Code & "'"
                Dim isNewEntry As Boolean = IIf(clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans)) > 0, False, True)

                clsCommon.AddColumnsForChange(coll, "Vendor_Code", clsCommon.myCstr(oVendor.Vendor_Code))
                clsCommon.AddColumnsForChange(coll, "Vendor_Name", clsCommon.myCstr(oVendor.Person))
                clsCommon.AddColumnsForChange(coll, "State_Code", clsCommon.myCstr(oVendor.State_Code))
                clsCommon.AddColumnsForChange(coll, "Country_Code", clsCommon.myCstr(oVendor.Country_Code))
                clsCommon.AddColumnsForChange(coll, "Is_Parent_Vendor", "1")
                clsCommon.AddColumnsForChange(coll, "Parent_Vendor_Code", "", True)
                clsCommon.AddColumnsForChange(coll, "Vendor_Type", clsCommon.myCstr(oVendor.Entr_Type))
                clsCommon.AddColumnsForChange(coll, "Pin_Code", clsCommon.myCstr(oVendor.Zip_Code))
                clsCommon.AddColumnsForChange(coll, "Add1", clsCommon.myCstr(oVendor.Add1))
                clsCommon.AddColumnsForChange(coll, "Add2", clsCommon.myCstr(oVendor.Add2))
                clsCommon.AddColumnsForChange(coll, "Add3", clsCommon.myCstr(oVendor.Add3))
                clsCommon.AddColumnsForChange(coll, "City_Code", clsCommon.myCstr(oVendor.City_Code))
                clsCommon.AddColumnsForChange(coll, "State", clsCommon.myCstr(oVendor.State))
                clsCommon.AddColumnsForChange(coll, "Country", clsCommon.myCstr(oVendor.Country))
                clsCommon.AddColumnsForChange(coll, "Phone1", clsCommon.myCstr(oVendor.Phone1))
                clsCommon.AddColumnsForChange(coll, "Phone2", clsCommon.myCstr(oVendor.Phone2))
                clsCommon.AddColumnsForChange(coll, "Email", clsCommon.myCstr(oVendor.Email))
                clsCommon.AddColumnsForChange(coll, "WebSite", clsCommon.myCstr(oVendor.WebSite))
                clsCommon.AddColumnsForChange(coll, "Contact_Person_Name", clsCommon.myCstr(oVendor.Person))
                clsCommon.AddColumnsForChange(coll, "Transporter", "N")
                If isNewEntry = True Then
                    clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
                    clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
                Else
                    clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
                End If
                If isNewEntry = True Then
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_VENDOR_MASTER", OMInsertOrUpdate.Insert, "", trans)
                Else
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_VENDOR_MASTER", OMInsertOrUpdate.Update,
                                                                 "TSPL_VENDOR_MASTER.Vendor_Code = '" & oVendor.Vendor_Code & "'", trans)
                End If
            End If
            'Next

        Catch err As Exception
            Throw New Exception(err.Message)
        End Try

        Return True
    End Function
    Shared Function GetData(ByVal entrpreneurCode As String, navigatorType As NavigatorType, ByVal transaction As SqlTransaction) As DataTable

        Dim selectQry As String = String.Empty
        Dim dt As DataTable = Nothing
        Dim columnName As String = "Vendor_Code"
        Dim table As String = "TSPL_VENDOR_MASTER"
        '===================================================================
        Try
            selectQry = "SELECT * FROM " & table & " WHERE 1=1"

            Select Case navigatorType
                Case navigatorType.First
                    selectQry += " and " & table & "." & columnName & "  = (select MIN(" & columnName & ") from " & table & " where 1=1 )"
                Case navigatorType.Last
                    selectQry += " and " & table & "." & columnName & "  = (select Max(" & columnName & ") from " & table & " where 1=1 )"
                Case navigatorType.Next
                    selectQry += " and " & table & "." & columnName & "  = (select Min(" & columnName & ") from " & table & " where " & columnName & ">'" & entrpreneurCode & "' )"
                Case navigatorType.Previous
                    selectQry += " and " & table & "." & columnName & "  = (select Max(" & columnName & ") from " & table & " where " & columnName & "<'" & entrpreneurCode & "' )"
                Case navigatorType.Current
                    selectQry += " and " & table & "." & columnName & "  = '" + entrpreneurCode + "'"
            End Select
            If selectQry IsNot Nothing Then
                dt = clsDBFuncationality.GetDataTable(selectQry, transaction)
            End If


        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return dt
    End Function

    Shared Function GetData(ByVal Vendor_Code As String, ByVal transaction As SqlTransaction) As clsGenerateVendor
        Dim objV As New clsGenerateVendor
        Dim selectQry As String = String.Empty
        Dim dt As DataTable = Nothing
        Dim columnName As String = "Vendor_Code"
        Dim table As String = "TSPL_VENDOR_MASTER"
        '===================================================================
        Try
            selectQry = "SELECT * FROM " & table & " WHERE 1=1"
            If selectQry IsNot Nothing Then
                dt = clsDBFuncationality.GetDataTable(selectQry, transaction)
                If dt.Rows.Count > 0 Then
                    Dim dr As DataRow = dt.Rows(0)
                    objV.Vendor_Code = clsCommon.myCstr(dr("Vendor_Code"))
                    objV.Vendor_Name = clsCommon.myCstr(dr("Vendor_Name"))
                    objV.Entr_Type = clsCommon.myCstr(dr("Vendor_Type"))
                    objV.State_Code = clsCommon.myCstr(dr("State_Code"))
                    objV.Country_Code = clsCommon.myCstr(dr("Country_Code"))
                    objV.Is_Parent_Vendor = clsCommon.myCstr(dr("Is_Parent_Vendor"))
                    objV.Parent_Vendor_Code = clsCommon.myCstr(dr("Parent_Vendor_Code"))
                    objV.Vendor_Type = clsCommon.myCstr(dr("Vendor_Type"))
                    objV.Pin_Code = clsCommon.myCstr(dr("Pin_Code"))
                    objV.Add1 = clsCommon.myCstr(dr("Add1"))
                    objV.Add2 = clsCommon.myCstr(dr("Add2"))
                    objV.Add3 = clsCommon.myCstr(dr("Add3"))
                    objV.City_Code = clsCommon.myCstr(dr("City_Code"))
                    objV.State = clsCommon.myCstr(dr("State"))
                    objV.Country = clsCommon.myCstr(dr("Country"))
                    objV.Phone1 = clsCommon.myCstr(dr("Phone1"))
                    objV.Phone2 = clsCommon.myCstr(dr("Phone2"))
                    objV.Email = clsCommon.myCstr(dr("Email"))
                    objV.WebSite = clsCommon.myCstr(dr("WebSite"))
                    objV.Contact_Person_Name = clsCommon.myCstr(dr("Contact_Person_Name"))
                    objV.Transporter = clsCommon.myCstr(dr("Transporter"))
                    objV.Created_By = clsCommon.myCstr(dr("Created_By"))
                    objV.Created_Date = clsCommon.myCstr(dr("Created_Date"))
                    objV.Modify_By = clsCommon.myCstr(dr("Modify_By"))
                    objV.Modify_Date = clsCommon.myCstr(dr("Modify_Date"))
                End If
            End If
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return objV
    End Function
End Class

