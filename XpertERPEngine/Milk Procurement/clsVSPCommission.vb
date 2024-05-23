Imports System.Data.SqlClient
Imports common
Public Class clsVSPCommission

#Region "Variables"
    Public Commission_Code As String = Nothing
    Public Commission_Name As String = Nothing
    Public Commission_Rate As Decimal
    Public Commission_Minimum_Shift_In_Payment_Cycle As Integer
    Public Commission_Minimum_Qty_In_Shift As Integer
    Public Commission_No_Of_Payment_Cycle_For_New_VSP As Integer
#End Region

    Public Shared Function SaveData(ByVal obj As clsVSPCommission) As Boolean
        Dim qry As String = ""
        Dim isNewEntry As Boolean = True
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Dim qry1 As String = clsDBFuncationality.getSingleValue("Select Commission_Code from TSPL_VSP_COMMISSION_MASTER where Commission_Code='" & obj.Commission_Code & "' ", trans)
        If clsCommon.myLen(qry1) > 0 Then
            isNewEntry = False
        End If
        Try
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Commission_Name", obj.Commission_Name)
            clsCommon.AddColumnsForChange(coll, "Commission_Rate", obj.Commission_Rate)
            clsCommon.AddColumnsForChange(coll, "Commission_Minimum_Qty_In_Shift", obj.Commission_Minimum_Qty_In_Shift)
            clsCommon.AddColumnsForChange(coll, "Commission_Minimum_Shift_In_Payment_Cycle", obj.Commission_Minimum_Shift_In_Payment_Cycle)
            clsCommon.AddColumnsForChange(coll, "Commission_No_Of_Payment_Cycle_For_New_VSP", obj.Commission_No_Of_Payment_Cycle_For_New_VSP)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                clsCommon.AddColumnsForChange(coll, "Commission_Code", obj.Commission_Code)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_VSP_COMMISSION_MASTER", OMInsertOrUpdate.Insert, "", trans)
            Else
                clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, obj.Commission_Code, "TSPL_VSP_COMMISSION_MASTER", "Commission_Code", trans)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_VSP_COMMISSION_MASTER", OMInsertOrUpdate.Update, "Commission_Code='" + obj.Commission_Code + "'", trans)
            End If
            trans.Commit()
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsVSPCommission
        Return GetData(strCode, NavType, Nothing)
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsVSPCommission
        Dim obj As clsVSPCommission = Nothing
        Dim qry As String = "select TSPL_VSP_COMMISSION_MASTER.* from TSPL_VSP_COMMISSION_MASTER   where 2=2 "
        Dim whrclas As String = ""
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_VSP_COMMISSION_MASTER.Commission_Code = (select MIN(Commission_Code) from TSPL_VSP_COMMISSION_MASTER WHERE 1=1 " + whrclas + " )"
            Case NavigatorType.Last
                qry += " and TSPL_VSP_COMMISSION_MASTER.Commission_Code = (select Max(Commission_Code) from TSPL_VSP_COMMISSION_MASTER WHERE 1=1 " + whrclas + " )"
            Case NavigatorType.Current
                qry += " and TSPL_VSP_COMMISSION_MASTER.Commission_Code = (select TOP 1 Commission_Code from TSPL_VSP_COMMISSION_MASTER WHERE 1=1 " + whrclas + " and Commission_Code='" + strCode + "' )"
            Case NavigatorType.Next
                qry += " and TSPL_VSP_COMMISSION_MASTER.Commission_Code = (select Min(Commission_Code) from TSPL_VSP_COMMISSION_MASTER where Commission_Code > '" + strCode + "' " + whrclas + ")"
            Case NavigatorType.Previous
                qry += " and TSPL_VSP_COMMISSION_MASTER.Commission_Code = (select Max(Commission_Code) from TSPL_VSP_COMMISSION_MASTER where Commission_Code < '" + strCode + "' " + whrclas + ")"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsVSPCommission()
            obj.Commission_Code = clsCommon.myCstr(dt.Rows(0)("Commission_Code"))
            obj.Commission_Name = clsCommon.myCstr(dt.Rows(0)("Commission_Name"))
            obj.Commission_Rate = clsCommon.myCdbl(dt.Rows(0)("Commission_Rate"))
            obj.Commission_Minimum_Shift_In_Payment_Cycle = clsCommon.myCdbl(dt.Rows(0)("Commission_Minimum_Shift_In_Payment_Cycle"))
            obj.Commission_Minimum_Qty_In_Shift = clsCommon.myCdbl(dt.Rows(0)("Commission_Minimum_Qty_In_Shift"))
            obj.Commission_No_Of_Payment_Cycle_For_New_VSP = clsCommon.myCdbl(dt.Rows(0)("Commission_No_Of_Payment_Cycle_For_New_VSP"))
        End If
        Return obj
    End Function

    Public Shared Function GetName(ByVal strCode As String) As String
        Return clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Commission_Name from TSPL_VSP_COMMISSION_MASTER where Commission_Code='" + strCode + "'"))
    End Function

    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim qry As String = ""
        Try
            qry = "Delete from TSPL_VSP_COMMISSION_MASTER where TSPL_VSP_COMMISSION_MASTER.Commission_Code='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry)
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function

    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = "select TSPL_VSP_COMMISSION_MASTER.Commission_Code,TSPL_VSP_COMMISSION_MASTER.Commission_Name,TSPL_VSP_COMMISSION_MASTER.Commission_Rate as [Tare Weight] from TSPL_VSP_COMMISSION_MASTER "
        str = clsCommon.ShowSelectForm("canmasterFnd", qry, "Commission_Code", whrcls, curcode, "Commission_Code", isButtonClicked)
        Return str

    End Function

    Public Shared Function getFinderObeject(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As clsVSPCommission
        Dim obj As clsVSPCommission = Nothing
        Dim strCode As String = getFinder(whrcls, curcode, isButtonClicked)
        If clsCommon.myLen(strCode) > 0 Then
            obj = GetData(strCode, NavigatorType.Current)
        End If
        Return obj
    End Function
End Class
