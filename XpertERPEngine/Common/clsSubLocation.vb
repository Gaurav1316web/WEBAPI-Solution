'==============BM00000002920,Created By Rohit on June 26,2014 on 5:00 PM.===================
Imports common
Imports System.Data.SqlClient
Public Class clsSubLocation
#Region "Variables"
    Public Sub_Location_code As String = ""
    Public Description As String = ""
    Public Location_Code As String = ""
    Public Location_Name As String = ""

#End Region


    Public Function SaveData(ByVal obj As clsSubLocation, ByVal isNewEntry As Boolean) As Boolean
        Dim isSaved As Boolean = True
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If isNewEntry Then
                Dim qry As String = " select max(Sub_Location_code) from TSPL_Sub_Location_Master"
                obj.Sub_Location_code = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
                If clsCommon.myLen(obj.Sub_Location_code) > 0 Then
                    obj.Sub_Location_code = clsCommon.incval(obj.Sub_Location_code)
                Else
                    obj.Sub_Location_code = "000000000000000000000000001"
                End If
            End If
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Location_Code", obj.Location_Code)
            clsCommon.AddColumnsForChange(coll, "Description", obj.Description)
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Sub_Location_code", obj.Sub_Location_code)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))

                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SUB_LOCATION_MASTER", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SUB_LOCATION_MASTER", OMInsertOrUpdate.Update, "Sub_Location_code='" + obj.Sub_Location_code + "'", trans)
            End If
            If isSaved Then
                trans.Commit()
            End If
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function

    Public Shared Function GetData(ByVal SubCategoryCode As String, ByVal NavType As common.NavigatorType) As clsSubLocation
        Return GetData(SubCategoryCode, NavType, Nothing)
    End Function

    Public Shared Function GetData(ByVal SubCategoryCode As String, ByVal NavType As common.NavigatorType, ByVal trans As SqlTransaction) As clsSubLocation
        Dim obj As clsSubLocation = Nothing
        Dim qry As String = "SELECT TSPL_SUB_LOCATION_MASTER.Sub_Location_code, TSPL_SUB_LOCATION_MASTER.Location_Code, TSPL_SUB_LOCATION_MASTER.Description,TSPL_LOCATION_MASTER.Location_Desc as LocName  from TSPL_SUB_LOCATION_MASTER left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_SUB_LOCATION_MASTER.Location_Code where  2=2"
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_SUB_LOCATION_MASTER.Sub_Location_code=(select MIN(Sub_Location_code) from TSPL_SUB_LOCATION_MASTER)"
            Case NavigatorType.Last
                qry += " and TSPL_SUB_LOCATION_MASTER.Sub_Location_code=(select Max(Sub_Location_code) from TSPL_SUB_LOCATION_MASTER)"
            Case NavigatorType.Next
                qry += " and TSPL_SUB_LOCATION_MASTER.Sub_Location_code=(select Min(Sub_Location_code) from TSPL_SUB_LOCATION_MASTER where Sub_Location_code > '" + SubCategoryCode + "')"
            Case NavigatorType.Previous
                qry += " and TSPL_SUB_LOCATION_MASTER.Sub_Location_code=(select Max(Sub_Location_code) from TSPL_SUB_LOCATION_MASTER where Sub_Location_code < '" + SubCategoryCode + "')"
            Case NavigatorType.Current
                qry += " and TSPL_SUB_LOCATION_MASTER.Sub_Location_code='" + SubCategoryCode + "'"
        End Select

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsSubLocation()
            obj.Sub_Location_code = clsCommon.myCstr(dt.Rows(0)("Sub_Location_code"))
            obj.Location_Code = clsCommon.myCstr(dt.Rows(0)("Location_Code"))
            obj.Description = clsCommon.myCstr(dt.Rows(0)("Description"))
            obj.Location_Name = clsCommon.myCstr(dt.Rows(0)("LocName"))

        End If

        Return obj
    End Function


    Public Shared Function DeleteData(ByVal Sub_Location_code As String) As Boolean
        Dim isSaved As Boolean = False
        If (clsCommon.myLen(Sub_Location_code) <= 0) Then
            Throw New Exception("Sub Location code not found to Delete")
        End If
        Dim obj As clsSubLocation = clsSubLocation.GetData(Sub_Location_code, NavigatorType.Current)
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Sub_Location_code) > 0) Then
            Try
                Dim qry As String = "delete from TSPL_SUB_LOCATION_MASTER where Sub_Location_code='" + Sub_Location_code + "'"
                isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)

                qry = "delete from TSPL_SUB_LOCATION_MASTER where Sub_Location_code='" + Sub_Location_code + "'"
                isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

                If (isSaved) Then
                    trans.Commit()
                Else
                    trans.Rollback()
                End If
            Catch ex As Exception
                trans.Rollback()
                Throw New Exception(ex.Message)
            End Try
        End If
        Return isSaved
    End Function


    Public Shared Function SaveData(ByVal Sub_Location_code As String, ByVal Arr As List(Of clsSubLocation), ByVal trans As SqlTransaction) As Boolean
        Dim Location_Name As String = ""
        Dim Location_Code As String = ""
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsSubLocation In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Sub_Location_code", Sub_Location_code)
                clsCommon.AddColumnsForChange(coll, "Location_Code", Location_Code)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SUB_LOCATION_MASTER", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function


    'Public Shared Function CompleteRequition(ByVal Sub_Location_code As String, ByVal strICode As String, ByVal LineNo As Integer) As Boolean
    '    Dim qry As String = "update TSPL_SUB_LOCATION_MASTER set Description ='tbCategoryName.text' where Location_Code='" + Sub_Location_code + "' "
    '    Return clsDBFuncationality.ExecuteNonQuery(qry)
    'End Function
End Class


