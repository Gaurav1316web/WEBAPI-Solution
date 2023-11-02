Imports System.Data.SqlClient
Imports common
Public Class clsBulkRoutMaster
#Region "Variables"
    Public ROUTE_NO As String = Nothing
    Public ROUTE_NAME As String = Nothing
    Public Distance As Decimal = 0
    Public Rate As Decimal = 0
    Public Amount As Decimal = 0
    Public Weight As Decimal = 0
    Public TollAmount As Decimal = 0
    Public IsContractor As Integer = 0
    Public IsDefault As Integer = 0
    Public Location_Code As String = String.Empty
    Public ROUTE_NAME_HINDI As String = Nothing
    Public Tanker_No As String = Nothing
    Public arrMCC As ArrayList
    Public CuttOff_Time As DateTime
#End Region
    Public Shared Function SaveData(ByVal obj As clsBulkRoutMaster) As Boolean
        Dim qry As String = ""
        Dim isNewEntry As Boolean = True
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()

        If obj.IsDefault = 1 Then
            clsDBFuncationality.ExecuteNonQuery("UPDATE TSPL_BULK_ROUTE_MASTER SET IsDefault=0 where IsDefault=1", trans)
        End If

        Dim qry1 As String = "Delete from TSPL_BULK_ROUTE_MASTER_MCC where ROUTE_NO='" & obj.ROUTE_NO & "' "
        clsDBFuncationality.ExecuteNonQuery(qry1, trans)

        qry1 = clsDBFuncationality.getSingleValue("Select ROUTE_NO from TSPL_BULK_ROUTE_MASTER where ROUTE_NO='" & obj.ROUTE_NO & "' ", trans)
        If clsCommon.myLen(qry1) > 0 Then
            isNewEntry = False
        End If
        Try
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "ROUTE_NAME", obj.ROUTE_NAME)
            clsCommon.AddColumnsForChange(coll, "ROUTE_NAME_HINDI", obj.ROUTE_NAME_HINDI, True, True)
            clsCommon.AddColumnsForChange(coll, "Distance", obj.Distance)
            clsCommon.AddColumnsForChange(coll, "Rate", obj.Rate)
            clsCommon.AddColumnsForChange(coll, "Weight", obj.Weight)
            clsCommon.AddColumnsForChange(coll, "Amount", obj.Amount)
            clsCommon.AddColumnsForChange(coll, "TollAmount", obj.TollAmount)
            clsCommon.AddColumnsForChange(coll, "IsContractor", obj.IsContractor)
            clsCommon.AddColumnsForChange(coll, "IsDefault", obj.IsDefault)
            clsCommon.AddColumnsForChange(coll, "Location_Code", obj.Location_Code, True)
            clsCommon.AddColumnsForChange(coll, "Tanker_No", obj.Tanker_No, True)
            clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "CuttOff_Time", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                clsCommon.AddColumnsForChange(coll, "ROUTE_NO", obj.ROUTE_NO)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_BULK_ROUTE_MASTER", OMInsertOrUpdate.Insert, "", trans)
            Else
                clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, obj.ROUTE_NO, "TSPL_BULK_ROUTE_MASTER", "ROUTE_NO", trans)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_BULK_ROUTE_MASTER", OMInsertOrUpdate.Update, "ROUTE_NO='" + obj.ROUTE_NO + "'", trans)
            End If

            clsBulkRoutMasterMCC.SaveData(obj.ROUTE_NO, obj.arrMCC, trans)
            trans.Commit()
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function


    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsBulkRoutMaster
        Dim obj As clsBulkRoutMaster = Nothing
        Dim qry As String = "select TSPL_BULK_ROUTE_MASTER.* from TSPL_BULK_ROUTE_MASTER   where 2=2 "
        Dim whrclas As String = ""
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_BULK_ROUTE_MASTER.ROUTE_NO = (select MIN(ROUTE_NO) from TSPL_BULK_ROUTE_MASTER WHERE 1=1 " + whrclas + " )"
            Case NavigatorType.Last
                qry += " and TSPL_BULK_ROUTE_MASTER.ROUTE_NO = (select Max(ROUTE_NO) from TSPL_BULK_ROUTE_MASTER WHERE 1=1 " + whrclas + " )"
            Case NavigatorType.Current
                qry += " and TSPL_BULK_ROUTE_MASTER.ROUTE_NO = (select TOP 1 ROUTE_NO from TSPL_BULK_ROUTE_MASTER WHERE 1=1 " + whrclas + " and ROUTE_NO='" + strCode + "' )"
            Case NavigatorType.Next
                qry += " and TSPL_BULK_ROUTE_MASTER.ROUTE_NO = (select Min(ROUTE_NO) from TSPL_BULK_ROUTE_MASTER where ROUTE_NO > '" + strCode + "' " + whrclas + ")"
            Case NavigatorType.Previous
                qry += " and TSPL_BULK_ROUTE_MASTER.ROUTE_NO = (select Max(ROUTE_NO) from TSPL_BULK_ROUTE_MASTER where ROUTE_NO < '" + strCode + "' " + whrclas + ")"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsBulkRoutMaster()
            obj.ROUTE_NO = clsCommon.myCstr(dt.Rows(0)("ROUTE_NO"))
            obj.ROUTE_NAME = clsCommon.myCstr(dt.Rows(0)("ROUTE_NAME"))
            obj.ROUTE_NAME_HINDI = clsCommon.myCstr(dt.Rows(0)("ROUTE_NAME_HINDI"))
            obj.Distance = clsCommon.myCdbl(dt.Rows(0)("Distance"))
            obj.Rate = clsCommon.myCdbl(dt.Rows(0)("Rate"))
            obj.Weight = clsCommon.myCdbl(dt.Rows(0)("Weight"))
            obj.Amount = clsCommon.myCdbl(dt.Rows(0)("Amount"))
            obj.Location_Code = clsCommon.myCstr(dt.Rows(0)("Location_Code"))
            obj.Tanker_No = clsCommon.myCstr(dt.Rows(0)("Tanker_No"))
            obj.TollAmount = clsCommon.myCdbl(dt.Rows(0)("TollAmount"))
            obj.IsContractor = clsCommon.myCdbl(dt.Rows(0)("IsContractor"))
            obj.IsDefault = clsCommon.myCdbl(dt.Rows(0)("IsDefault"))
            obj.arrMCC = clsBulkRoutMasterMCC.GetData(obj.ROUTE_NO)
            obj.CuttOff_Time = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy hh:mm:ss tt")
        End If
        Return obj
    End Function

    Public Shared Function GetName(ByVal strCode As String) As String
        Return clsCommon.myCstr(clsDBFuncationality.getSingleValue("select ROUTE_NAME from TSPL_BULK_ROUTE_MASTER where ROUTE_NO='" + strCode + "'"))
    End Function

    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim qry As String = ""
        Try
            qry = "Delete from TSPL_BULK_ROUTE_MASTER where TSPL_BULK_ROUTE_MASTER.ROUTE_NO='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry)
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function

    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = "select TSPL_BULK_ROUTE_MASTER.ROUTE_NO,TSPL_BULK_ROUTE_MASTER.ROUTE_NAME,TSPL_BULK_ROUTE_MASTER.Distance ,TSPL_BULK_ROUTE_MASTER.Rate,TSPL_BULK_ROUTE_MASTER.Weight,TSPL_BULK_ROUTE_MASTER.Amount from TSPL_BULK_ROUTE_MASTER "
        str = clsCommon.ShowSelectForm("FndBulkRoute@Master", qry, "ROUTE_NO", whrcls, curcode, "ROUTE_NO", isButtonClicked)
        Return str

    End Function

    Public Shared Function getFinderObeject(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As clsBulkRoutMaster
        Dim obj As clsBulkRoutMaster = Nothing
        Dim strCode As String = getFinder(whrcls, curcode, isButtonClicked)
        If clsCommon.myLen(strCode) > 0 Then
            obj = GetData(strCode, NavigatorType.Current)
        End If
        Return obj
    End Function
End Class

Public Class clsBulkRoutMasterMCC
    Public Shared Function SaveData(ByVal strRoute As String, ByVal arr As ArrayList, ByVal trans As SqlTransaction) As Boolean
        If arr IsNot Nothing AndAlso arr.Count > 0 Then
            For Each strMCC As String In arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "ROUTE_NO", strRoute)
                clsCommon.AddColumnsForChange(coll, "MCC_Code", strMCC)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_BULK_ROUTE_MASTER_MCC", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function
    Public Shared Function GetData(ByVal strCode As String) As ArrayList
        Dim arr As ArrayList = Nothing
        Dim qry As String = "select MCC_Code from TSPL_BULK_ROUTE_MASTER_MCC where ROUTE_NO='" + strCode + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            arr = New ArrayList
            For Each dr As DataRow In dt.Rows
                arr.Add(clsCommon.myCstr(dr("MCC_Code")))
            Next
        End If
        Return arr
    End Function
End Class
