Imports common
Imports System.Data.SqlClient

Public Class ClsMFSeetings
#Region "variables"
    Public MixingCHrage As Decimal = Nothing
    Public ALLOW_AUTO_CLOSE_MO_DURING_RECEIPT As String = Nothing
    Public AUTO_CLOSE_TOLERANCE As String = Nothing
    Public ACTIVATE_MO_SERIES As String = Nothing
    Public ALLOW_6DEC_STD_UNIT_COST As String = Nothing
    Public ALLOW_RECEIVE_WITHOUT_ISSUANCE As String = Nothing
    Public EXCEED_ISSUE_TOLRC As String = Nothing
    Public ISSUE_TOLRC As String = Nothing
    Public EXCEED_REC_TOLRC As String = Nothing
    Public REC_TOLRC As String = Nothing
    Public AREA_CODE As String = Nothing
    Public LOCATION_CODE As String = Nothing
    Public IC_COST_ITEMS_DURING As String = Nothing
    Public Created_By As String = Nothing
    Public Created_Date As Date? = Nothing
    Public Modified_By As String = Nothing
    Public Modified_Date As Date? = Nothing
#End Region
#Region "Functions"
    Public Shared Function SaveData(ByVal obj As ClsMFSeetings, ByVal trans As SqlTransaction) As Boolean
        Dim qry As String = ""
        Try
            clsDBFuncationality.ExecuteNonQuery("Delete  from TSPL_MF_SETTINGS  ", trans)
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "ALLOW_AUTO_CLOSE_MO_DURING_RECEIPT", obj.ALLOW_AUTO_CLOSE_MO_DURING_RECEIPT)
            clsCommon.AddColumnsForChange(coll, "AUTO_CLOSE_TOLERANCE", obj.AUTO_CLOSE_TOLERANCE)
            clsCommon.AddColumnsForChange(coll, "ACTIVATE_MO_SERIES", obj.ACTIVATE_MO_SERIES)
            clsCommon.AddColumnsForChange(coll, "ALLOW_6DEC_STD_UNIT_COST", obj.ALLOW_6DEC_STD_UNIT_COST)
            clsCommon.AddColumnsForChange(coll, "ALLOW_RECEIVE_WITHOUT_ISSUANCE", obj.ALLOW_RECEIVE_WITHOUT_ISSUANCE)
            clsCommon.AddColumnsForChange(coll, "EXCEED_ISSUE_TOLRC", obj.EXCEED_ISSUE_TOLRC)
            clsCommon.AddColumnsForChange(coll, "EXCEED_REC_TOLRC", obj.EXCEED_REC_TOLRC)
            clsCommon.AddColumnsForChange(coll, "ISSUE_TOLRC", obj.ISSUE_TOLRC)
            clsCommon.AddColumnsForChange(coll, "REC_TOLRC", obj.REC_TOLRC)
            clsCommon.AddColumnsForChange(coll, "Mixing_Charge", obj.MixingCHrage)

            If obj.AREA_CODE <> "" Then
                clsCommon.AddColumnsForChange(coll, "AREA_CODE", obj.AREA_CODE)
            End If
            If obj.LOCATION_CODE <> "" Then
                clsCommon.AddColumnsForChange(coll, "LOCATION_CODE", obj.LOCATION_CODE)
            End If
            clsCommon.AddColumnsForChange(coll, "IC_COST_ITEMS_DURING", obj.IC_COST_ITEMS_DURING)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MF_SETTINGS", OMInsertOrUpdate.Insert, "", trans)


        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function
   

    Public Shared Function GetData(ByVal trans As SqlTransaction) As ClsMFSeetings

        Dim obj As ClsMFSeetings = Nothing
        Dim Arr As List(Of ClsMFSeetings) = Nothing
        Dim qry As String = "select ALLOW_AUTO_CLOSE_MO_DURING_RECEIPT,AUTO_CLOSE_TOLERANCE,ACTIVATE_MO_SERIES,ALLOW_6DEC_STD_UNIT_COST,ALLOW_RECEIVE_WITHOUT_ISSUANCE,EXCEED_ISSUE_TOLRC,ISSUE_TOLRC,EXCEED_REC_TOLRC,REC_TOLRC,AREA_CODE,LOCATION_CODE,IC_COST_ITEMS_DURING,Created_By,Created_Date,Modified_By,Modified_Date,Mixing_Charge from TSPL_MF_SETTINGS "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New ClsMFSeetings()
            obj.MixingCHrage = clsCommon.myCdbl(dt.Rows(0)("Mixing_Charge"))
            obj.ALLOW_AUTO_CLOSE_MO_DURING_RECEIPT = clsCommon.myCstr(dt.Rows(0)("ALLOW_AUTO_CLOSE_MO_DURING_RECEIPT"))
            obj.AUTO_CLOSE_TOLERANCE = clsCommon.myCstr(dt.Rows(0)("AUTO_CLOSE_TOLERANCE"))
            obj.ACTIVATE_MO_SERIES = clsCommon.myCstr(dt.Rows(0)("ACTIVATE_MO_SERIES"))
            obj.ALLOW_6DEC_STD_UNIT_COST = clsCommon.myCstr(dt.Rows(0)("ALLOW_6DEC_STD_UNIT_COST"))
            obj.ALLOW_RECEIVE_WITHOUT_ISSUANCE = clsCommon.myCstr(dt.Rows(0)("ALLOW_RECEIVE_WITHOUT_ISSUANCE"))
            obj.EXCEED_ISSUE_TOLRC = clsCommon.myCstr(dt.Rows(0)("EXCEED_ISSUE_TOLRC"))
            obj.REC_TOLRC = clsCommon.myCstr(dt.Rows(0)("REC_TOLRC"))
            obj.EXCEED_REC_TOLRC = clsCommon.myCstr(dt.Rows(0)("EXCEED_REC_TOLRC"))
            obj.ISSUE_TOLRC = clsCommon.myCstr(dt.Rows(0)("ISSUE_TOLRC"))
            obj.AREA_CODE = clsCommon.myCstr(dt.Rows(0)("AREA_CODE"))
            obj.LOCATION_CODE = clsCommon.myCstr(dt.Rows(0)("LOCATION_CODE"))
            obj.IC_COST_ITEMS_DURING = clsCommon.myCstr(dt.Rows(0)("IC_COST_ITEMS_DURING"))
        End If
        Return obj
    End Function
    Public Shared Function Get_MO_BO_Setting(Optional ByVal trans As SqlTransaction = Nothing) As Boolean
        '' this function returns true if MO screen is on else false.
        Dim qry As String = "select ACTIVATE_MO_SERIES from TSPL_MF_SETTINGS"
        Dim dt As DataTable
        dt = clsDBFuncationality.GetDataTable(qry, trans)
        If dt.Rows.Count > 0 Then
            If dt.Rows(0).Item("ACTIVATE_MO_SERIES") = True Then
                Return True
            Else
                Return False
            End If
        Else
            Return False
        End If
    End Function
#End Region
End Class
