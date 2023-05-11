Imports System.Data.SqlClient
Imports common

Public Class ClsServiceChargeMaster
#Region "Variables"
    Public Service_Charge_Code As String = String.Empty
    Public Service_Charge_Name As String = String.Empty
    Public GL_Account_Code As String = String.Empty
    Public Created_By As String = String.Empty
    Public Created_Date As Date? = Nothing
    Public Modified_By As String = String.Empty
    Public Modified_Date As Date? = Nothing
#End Region

    Public Shared Function SaveData(ByVal obj As ClsServiceChargeMaster, ByVal isNewEntry As Boolean) As Boolean
        Dim qry As String = ""
        Dim IsSaved As Boolean = False
        Try
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Service_Charge_Name", obj.Service_Charge_Name)
            clsCommon.AddColumnsForChange(coll, "GL_Account_Code", obj.GL_Account_Code, True)
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy hh:mm tt"))
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy hh:mm tt"))
                clsCommon.AddColumnsForChange(coll, "Service_Charge_Code", obj.Service_Charge_Code)
                IsSaved = clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SW_SERVICE_CHARGE_MASTER", OMInsertOrUpdate.Insert, "")
            Else
                IsSaved = clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SW_SERVICE_CHARGE_MASTER", OMInsertOrUpdate.Update, "TSPL_SW_SERVICE_CHARGE_MASTER.Service_Charge_Code='" + obj.Service_Charge_Code + "'")
            End If

        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return IsSaved
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As ClsServiceChargeMaster
        Dim obj As ClsServiceChargeMaster = Nothing
        Dim Arr As List(Of ClsServiceChargeMaster) = Nothing
        Dim qry As String = "SELECT * FROM TSPL_SW_SERVICE_CHARGE_MASTER where 2=2 "
        Dim whrclas As String = ""
        Select Case NavType
            Case NavigatorType.First
                qry += " AND TSPL_SW_SERVICE_CHARGE_MASTER.Service_Charge_Code = (select MIN(Service_Charge_Code) FROM TSPL_SW_SERVICE_CHARGE_MASTER WHERE 1=1 " + whrclas + " )"
            Case NavigatorType.Last
                qry += " AND TSPL_SW_SERVICE_CHARGE_MASTER.Service_Charge_Code = (select Max(Service_Charge_Code) FROM TSPL_SW_SERVICE_CHARGE_MASTER WHERE 1=1 " + whrclas + " )"
            Case NavigatorType.Current
                qry += " AND TSPL_SW_SERVICE_CHARGE_MASTER.Service_Charge_Code = (select TOP 1 Service_Charge_Code FROM TSPL_SW_SERVICE_CHARGE_MASTER WHERE 1=1 " + whrclas + " AND Service_Charge_Code='" + strCode + "' )"
            Case NavigatorType.Next
                qry += " AND TSPL_SW_SERVICE_CHARGE_MASTER.Service_Charge_Code = (select Min(Service_Charge_Code) FROM TSPL_SW_SERVICE_CHARGE_MASTER where Service_Charge_Code > '" + strCode + "' " + whrclas + ")"
            Case NavigatorType.Previous
                qry += " AND TSPL_SW_SERVICE_CHARGE_MASTER.Service_Charge_Code = (select Max(Service_Charge_Code) FROM TSPL_SW_SERVICE_CHARGE_MASTER where Service_Charge_Code < '" + strCode + "' " + whrclas + ")"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New ClsServiceChargeMaster()
            obj.Service_Charge_Code = clsCommon.myCstr(dt.Rows(0)("Service_Charge_Code"))
            obj.Service_Charge_Name = clsCommon.myCstr(dt.Rows(0)("Service_Charge_Name"))
            obj.GL_Account_Code = clsCommon.myCstr(dt.Rows(0)("GL_Account_Code"))
        End If
        Return obj
    End Function
End Class
