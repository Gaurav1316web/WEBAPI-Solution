Imports common
Imports System.Data.SqlClient


Public Class clsEmpASMZMDetails

#Region "Variables"
    Public empcode As String = Nothing
    Public empltype As String = Nothing
    Public statecode As String = Nothing
    Public statename As String = Nothing
    Public regioncode As String = Nothing
    Public regionname As String = Nothing
    Public citycode As String = Nothing
    Public cityname As String = Nothing

#End Region
    Public Shared Function SaveData(ByVal strCode As String, ByVal Arr As List(Of clsEmpASMZMDetails)) As Boolean

        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            'trans = clsDBFuncationality.GetTransactin()
            If clsEmpASMZMDetails.SaveData(strCode, Arr, trans) Then
                trans.Commit()
            End If
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function SaveData(ByVal strCode As String, ByVal Arr As List(Of clsEmpASMZMDetails), ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Try
            If clsCommon.myLen(strCode) > 0 Then
                Dim qry As String = "delete from TSPL_emptype_ASMZM_details where emp_code='" + strCode + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
            End If

            If Arr IsNot Nothing AndAlso Arr.Count > 0 Then
                For Each obj As clsEmpASMZMDetails In Arr
                    Dim coll As New Hashtable()

                    clsCommon.AddColumnsForChange(coll, "state_code", clsCommon.myCstr(obj.statecode))
                    clsCommon.AddColumnsForChange(coll, "state_name", clsCommon.myCstr(obj.statename))
                    clsCommon.AddColumnsForChange(coll, "region_code", clsCommon.myCstr(obj.regioncode))
                    clsCommon.AddColumnsForChange(coll, "region_name", clsCommon.myCstr(obj.regionname))
                    clsCommon.AddColumnsForChange(coll, "city_code", clsCommon.myCstr(obj.citycode))
                    clsCommon.AddColumnsForChange(coll, "city_name", clsCommon.myCstr(obj.cityname))
                    clsCommon.AddColumnsForChange(coll, "emp_code", obj.empcode)
                    clsCommon.AddColumnsForChange(coll, "emp_type", obj.empltype)

                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_emptype_ASMZM_details", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function GetData(ByVal empcode As String) As List(Of clsEmpASMZMDetails)
        Dim arr As List(Of clsEmpASMZMDetails) = Nothing


        Dim qst As String = "select TSPL_emptype_ASMZM_details.emp_code ,TSPL_emptype_ASMZM_details.emp_type ,TSPL_emptype_ASMZM_details.city_code,tspl_city_master.city_name ,TSPL_emptype_ASMZM_details.state_code ,tspl_state_master.state_name ,TSPL_emptype_ASMZM_details.region_code ,tspl_region_master.region_name  from TSPL_emptype_ASMZM_details left outer join tspl_city_master on tspl_city_master.city_code=TSPL_emptype_ASMZM_details.city_code left outer join tspl_state_master on tspl_state_master.state_code=TSPL_emptype_ASMZM_details.state_code left outer join tspl_region_master on tspl_region_master.region_code=TSPL_emptype_ASMZM_details.region_code  where TSPL_emptype_ASMZM_details.emp_code='" + empcode + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qst)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            arr = New List(Of clsEmpASMZMDetails)
            For Each dr As DataRow In dt.Rows
                Dim obj As clsEmpASMZMDetails = New clsEmpASMZMDetails()
                obj.statecode = clsCommon.myCstr(dr("state_code"))
                obj.statename = clsCommon.myCstr(dr("state_name"))
                obj.regioncode = clsCommon.myCstr(dr("region_code"))
                obj.regionname = clsCommon.myCstr(dr("region_name"))
                obj.citycode = clsCommon.myCstr(dr("city_code"))
                obj.cityname = clsCommon.myCstr(dr("city_name"))

                arr.Add(obj)
            Next
        End If
        Return arr
    End Function
End Class

