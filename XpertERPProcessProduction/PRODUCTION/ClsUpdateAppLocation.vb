Imports common
Imports System.Data.SqlClient
Public Class ClsUpdateAppLocation

    Public Code As String
    Public Location_Name As String
    Public DataBase_Name As String
    Public Created_By As String
    Public Created_Date As String
    Public Modified_By As String
    Public Modified_Date As String
    Public Customer_Code As String
    Public Customer_Name As String
    Public Customer_Account_No As String
    Public Scheduler_Apply_SMS As Integer = 0
    Public Scheduler_Apply_EMail As Integer = 0
    Public Apply_PD_Account As Integer = 0
    Public Apply_ECollect As Integer = 0
    Public Arr As List(Of ClsUpdateAppLocation) = Nothing
    Public isNewEntry As Boolean = False



    Public Function SaveData(ByVal Arr As List(Of ClsUpdateAppLocation)) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()

        Dim isSaved As Boolean = True
        Try
            Dim isNewEntry As Boolean = False
            Dim strDocNo As String = ""
            For Each obj As ClsUpdateAppLocation In Arr
                Dim coll As New Hashtable()
                Dim qry As String = "Select Count(1) from TSPL_MASTER.dbo.TSPL_APP_LOCATION   where Code='" + obj.Code + "'"
                If clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans)) > 0 Then
                    isNewEntry = False
                Else
                    isNewEntry = True
                End If
                'clsCommon.AddColumnsForChange(coll, "comp_code", objCommonVar.CurrentCompanyCode)
                clsCommon.AddColumnsForChange(coll, "Code", obj.Code)
                clsCommon.AddColumnsForChange(coll, "Location_Name", obj.Location_Name)
                clsCommon.AddColumnsForChange(coll, "DataBase_Name", obj.DataBase_Name)
                clsCommon.AddColumnsForChange(coll, "Customer_Code", obj.Customer_Code)
                clsCommon.AddColumnsForChange(coll, "Customer_Name", obj.Customer_Name)
                clsCommon.AddColumnsForChange(coll, "Customer_Account_No", obj.Customer_Account_No)
                clsCommon.AddColumnsForChange(coll, "Scheduler_Apply_SMS", obj.Scheduler_Apply_SMS)
                clsCommon.AddColumnsForChange(coll, "Scheduler_Apply_EMail", obj.Scheduler_Apply_EMail)
                clsCommon.AddColumnsForChange(coll, "Apply_PD_Account", obj.Apply_PD_Account)
                clsCommon.AddColumnsForChange(coll, "Apply_ECollect", obj.Apply_ECollect)
                'clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
                'clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                'clsCommon.AddColumnsForChange(coll, "Created_By", obj.Created_By)
                'clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                If isNewEntry Then
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "tspl_master.dbo.TSPL_APP_LOCATION ", OMInsertOrUpdate.Insert, "", trans)
                Else
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "tspl_master.dbo.TSPL_APP_LOCATION ", OMInsertOrUpdate.Update, "Code ='" + obj.Code + "'", trans)
                End If
                'Else
                '    isSaved = isSaved And clsCommonFunctionality.UpdateDataTable(coll, "TSPL_APP_LOCATION", OMInsertOrUpdate.Update, "TSPL_APP_LOCATION.Code='" + obj.Code + "'", trans)
                'End If
            Next

            If isSaved Then
                trans.Commit()
            Else
                trans.Rollback()
            End If
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function




    Public Shared Function GetData(ByVal strCode As String) As List(Of ClsUpdateAppLocation)
        Dim obj As ClsAlternateitemDetail = Nothing
        Dim qry As String = "select  * from TSPL_MASTER.dbo.TSPL_APP_LOCATION WHERE Code = '" + strCode + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        dt = clsDBFuncationality.GetDataTable(qry)
        Dim Arr As New List(Of ClsUpdateAppLocation)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            Dim objTr As ClsUpdateAppLocation
            For Each dr As DataRow In dt.Rows
                objTr = New ClsUpdateAppLocation()
                objTr.Code = clsCommon.myCstr(dr("Code"))
                objTr.Location_Name = clsCommon.myCstr(dr("Location_Name"))
                objTr.DataBase_Name = clsCommon.myCstr(dr("DataBase_Name"))
                objTr.Customer_Code = clsCommon.myCstr(dr("Customer_Code"))
                objTr.Customer_Name = clsCommon.myCstr(dr("Customer_Name"))
                objTr.Customer_Account_No = clsCommon.myCstr(dr("Customer_Account_No"))
                objTr.Scheduler_Apply_SMS = clsCommon.myCstr(dr("Scheduler_Apply_SMS"))
                objTr.Scheduler_Apply_EMail = clsCommon.myCdbl(dr("Scheduler_Apply_EMail"))
                objTr.Apply_PD_Account = clsCommon.myCdbl(dr("Apply_PD_Account"))
                objTr.Apply_ECollect = clsCommon.myCstr(dr("Apply_ECollect"))
                Arr.Add(objTr)
            Next
        End If
        Return Arr
    End Function
End Class
