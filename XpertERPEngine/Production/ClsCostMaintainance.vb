Imports System.Data.SqlClient
Imports common
Public Class ClsCostMaintainance
#Region "variables"
    Public ITEM_CODE As String = Nothing
    Public MATERIAL_COST As String = Nothing
    Public PACKAGING_COST As String = Nothing
    Public SETUP_COST As String = Nothing
    Public LABOR_COST As String = Nothing
    Public OVERHEAD_COST As String = Nothing
    Public SUBCONTRACT_COST As String = Nothing
    Public TOOL_COST As String = Nothing
    Public TOTAL_COST As String = Nothing
    Public QTY_ON_HAND As String = Nothing
    Public AVERAGE_COST As String = Nothing
    Public Created_By As String = Nothing
    Public Created_Date As Date? = Nothing
    Public Modified_By As String = Nothing
    Public Modified_Date As Date? = Nothing
#End Region
#Region "Functions"
    Public Shared Function SaveData(ByVal obj As ClsCostMaintainance, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim qry As String = ""
        Try
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "ITEM_CODE", obj.ITEM_CODE)
            clsCommon.AddColumnsForChange(coll, "comp_code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "MATERIAL_COST", clsCommon.myCdbl(obj.MATERIAL_COST))
            clsCommon.AddColumnsForChange(coll, "PACKAGING_COST", clsCommon.myCdbl(obj.PACKAGING_COST))
            clsCommon.AddColumnsForChange(coll, "SETUP_COST", clsCommon.myCdbl(obj.SETUP_COST))
            clsCommon.AddColumnsForChange(coll, "LABOR_COST", clsCommon.myCdbl(obj.LABOR_COST))
            clsCommon.AddColumnsForChange(coll, "OVERHEAD_COST", clsCommon.myCdbl(obj.OVERHEAD_COST))
            clsCommon.AddColumnsForChange(coll, "SUBCONTRACT_COST", clsCommon.myCdbl(obj.SUBCONTRACT_COST))
            clsCommon.AddColumnsForChange(coll, "TOOL_COST", clsCommon.myCdbl(obj.TOOL_COST))
            clsCommon.AddColumnsForChange(coll, "TOTAL_COST", clsCommon.myCdbl(obj.TOTAL_COST))
            clsCommon.AddColumnsForChange(coll, "QTY_ON_HAND", clsCommon.myCdbl(obj.QTY_ON_HAND))
            clsCommon.AddColumnsForChange(coll, "AVERAGE_COST", clsCommon.myCdbl(obj.AVERAGE_COST))
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MF_ITEM_COST_MAINTENANCE", OMInsertOrUpdate.Insert, "", trans)
            Else

                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MF_ITEM_COST_MAINTENANCE", OMInsertOrUpdate.Update, "TSPL_MF_ITEM_COST_MAINTENANCE.ITEM_CODE='" + obj.ITEM_CODE + "'", trans)
            End If

        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As ClsCostMaintainance
        Return GetData(strCode, NavType, Nothing)
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As ClsCostMaintainance
        Dim obj As ClsCostMaintainance = Nothing
        Dim Arr As List(Of ClsCostMaintainance) = Nothing
        Dim qry As String = "select * from TSPL_MF_ITEM_COST_MAINTENANCE where 2=2 "
        Dim whrclas As String = ""
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_MF_ITEM_COST_MAINTENANCE.ITEM_CODE = (select MIN(ITEM_CODE) from TSPL_MF_ITEM_COST_MAINTENANCE WHERE 1=1 " + whrclas + " )"
            Case NavigatorType.Last
                qry += " and TSPL_MF_ITEM_COST_MAINTENANCE.ITEM_CODE = (select Max(ITEM_CODE) from TSPL_MF_ITEM_COST_MAINTENANCE WHERE 1=1 " + whrclas + " )"
            Case NavigatorType.Current
                qry += " and TSPL_MF_ITEM_COST_MAINTENANCE.ITEM_CODE = (select top 1 ITEM_CODE from TSPL_MF_ITEM_COST_MAINTENANCE WHERE 1=1 " + whrclas + " and ITEM_CODE='" + strCode + "' )"
            Case NavigatorType.Next
                qry += " and TSPL_MF_ITEM_COST_MAINTENANCE.ITEM_CODE = (select Min(ITEM_CODE) from TSPL_MF_ITEM_COST_MAINTENANCE where ITEM_CODE>'" + strCode + "' " + whrclas + ")"
            Case NavigatorType.Previous
                qry += " and TSPL_MF_ITEM_COST_MAINTENANCE.ITEM_CODE = (select Max(ITEM_CODE) from TSPL_MF_ITEM_COST_MAINTENANCE where ITEM_CODE<'" + strCode + "' " + whrclas + ")"
            Case Else
                qry += " and TSPL_MF_ITEM_COST_MAINTENANCE.ITEM_CODE ='" & strCode & "' """

        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New ClsCostMaintainance()
            obj.ITEM_CODE = clsCommon.myCstr(dt.Rows(0)("ITEM_CODE"))
            obj.MATERIAL_COST = clsCommon.myCstr(dt.Rows(0)("MATERIAL_COST"))
            obj.PACKAGING_COST = clsCommon.myCstr(dt.Rows(0)("PACKAGING_COST"))
            obj.SETUP_COST = clsCommon.myCstr(dt.Rows(0)("SETUP_COST"))
            obj.LABOR_COST = clsCommon.myCstr(dt.Rows(0)("LABOR_COST"))
            obj.OVERHEAD_COST = clsCommon.myCstr(dt.Rows(0)("OVERHEAD_COST"))
            obj.SUBCONTRACT_COST = clsCommon.myCstr(dt.Rows(0)("SUBCONTRACT_COST"))
            obj.TOOL_COST = clsCommon.myCstr(dt.Rows(0)("TOOL_COST"))
            obj.TOTAL_COST = clsCommon.myCstr(dt.Rows(0)("TOTAL_COST"))
            obj.QTY_ON_HAND = clsCommon.myCstr(dt.Rows(0)("QTY_ON_HAND"))
            obj.AVERAGE_COST = clsCommon.myCstr(dt.Rows(0)("AVERAGE_COST"))

        End If
        Return obj
    End Function
    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim isDelete As Boolean
        Try
            isDelete = False

            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Code not found to Delete")
            End If

            Dim qry As String
            qry = "delete from TSPL_MF_ITEM_COST_MAINTENANCE where ITEM_CODE ='" + strCode + "'"
            isDelete = clsDBFuncationality.ExecuteNonQuery(qry)
        Catch ex As Exception

            common.clsCommon.MyMessageBoxShow(ex.Message.ToString())
        End Try
        Return isDelete
    End Function
#End Region
End Class
