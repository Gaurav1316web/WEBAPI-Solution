Imports common
Imports System.Data.SqlClient
Public Class clsMilkGateEntryIn
#Region "Variables"
    Public Entry_Code As String = Nothing
    Public Entry_Date As DateTime
    Public Shift_Date As Date
    Public Entry_Shift As String = Nothing
    Public MCC_Code As String = Nothing
    Public Route_Code As String = Nothing
    Public Vehicle_No As String = Nothing
    Public Vehicle_No_Other As String = Nothing
    Public Transporter_Code As String = Nothing
    Public Cans_Filled As Integer = 0
    Public Cans_Empty As Integer = 0
    Public Status As ERPTransactionStatus = ERPTransactionStatus.Pending
    Public Remarks As String = Nothing
    Public Posted_Date As DateTime? = Nothing
    Public MCC_Name As String = Nothing 'Not a Table column
    Public Route_Name As String = Nothing 'Not a Table column
    Public Vehicle_Name As String = Nothing 'Not a Table column
    Public Transporter_Name As String = Nothing 'Not a Table column
    'DATE :23-JAN-17 > CLIENT : FOR SAHAYOG 
    Public Manual_FAT_Per As Double = 0
    Public Manual_SNF_Per As Double = 0
    Public KMReading As Double = 0
    Dim WeighmentNotMandatoryInMCC As Boolean = False
    Public Penalty_Amount As Decimal = 0
#End Region

    Public Function SaveData(ByVal obj As clsMilkGateEntryIn, ByVal isNewEntry As Boolean) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            SaveData(obj, isNewEntry, trans)
            trans.Commit()
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function

    Public Function SaveData(ByVal obj As clsMilkGateEntryIn, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Try
            ''========================Update by preeti Gupta[18/09/2017][Skip Validation for GK Client on setting based "WeighmentNotMandatoryInMCC"]=========================
            Dim qry As String = "select Entry_Code,case when Status=1 then 'Posted' else 'Pending' end as Status from TSPL_MILK_GATE_ENTRY_IN where Route_code='" + obj.Route_Code + "' and Entry_Code not in ('" + obj.Entry_Code + "') and convert(date, Shift_Date,103)=convert(date, '" + clsCommon.GetPrintDate(obj.Shift_Date, "dd/MMM/yyyy") + "',103) and Entry_Shift='" + obj.Entry_Shift + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Throw New Exception("Route No " + obj.Route_Code + " is already entered.Gate Entry No- " + clsCommon.myCstr(dt.Rows(0)("Entry_Code")) + " and Status " + clsCommon.myCstr(dt.Rows(0)("Status")))
            End If

            qry = "select Entry_Code from TSPL_MILK_GATE_ENTRY_IN where (Vehicle_No='" + IIf(clsCommon.myLen(obj.Vehicle_No_Other) > 0, obj.Vehicle_No_Other, obj.Vehicle_No) + "' or Vehicle_No_Other='" + IIf(clsCommon.myLen(obj.Vehicle_No_Other) > 0, obj.Vehicle_No_Other, obj.Vehicle_No) + "' ) and Entry_Code not in ('" + obj.Entry_Code + "')" + Environment.NewLine + _
               " and not exists(select 1 from TSPL_MILK_GATE_ENTRY_OUT where TSPL_MILK_GATE_ENTRY_OUT.Against_Gate_Entry_No=TSPL_MILK_GATE_ENTRY_IN.Entry_Code  and TSPL_MILK_GATE_ENTRY_OUT.status=1 )"
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Throw New Exception("Gate Entry No- " + clsCommon.myCstr(dt.Rows(0)("Entry_Code")) + " and Vehicle no" + IIf(clsCommon.myLen(obj.Vehicle_No_Other) > 0, obj.Vehicle_No_Other, obj.Vehicle_No) + " is already entered but not create posted Gate Entry out.")
            End If
            ''By Balwinder On 25/06/2018 BHA/21/06/18-000067 Calculate Penalty Amount of Transport is Transporter coming Late 
            obj.Penalty_Amount = 0
            qry = "select " + IIf(clsCommon.CompairString(obj.Entry_Shift, "M") = CompairStringResult.Equal, "MCC_Reaching_Time_M", "MCC_Reaching_Time_E") + " as Reaching_Time  from TSPL_MCC_ROUTE_MASTER where Route_Code='" + obj.Route_Code + "'"
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                If dt.Rows(0)("Reaching_Time") IsNot DBNull.Value Then
                    Dim ReachingTime As DateTime = clsCommon.myCDate(dt.Rows(0)("Reaching_Time"))
                    ReachingTime = New DateTime(obj.Shift_Date.Year, obj.Shift_Date.Month, obj.Shift_Date.Day, ReachingTime.Hour, ReachingTime.Minute, ReachingTime.Second)
                    Dim ts As TimeSpan = obj.Entry_Date - ReachingTime
                    Dim Min As Integer = ts.Hours * 60 + ts.Minutes
                    qry = "select top 1 Amount from TSPL_PTM_DEDCUTION_RANGE where PTM_Code='" + obj.Transporter_Code + "' and TSPL_PTM_DEDCUTION_RANGE.Minutes <= " + clsCommon.myCstr(Min) + "   order by Minutes DESC "
                    dt = clsDBFuncationality.GetDataTable(qry, trans)
                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                        obj.Penalty_Amount = clsCommon.myCdbl(dt.Rows(0)("Amount"))
                    End If
                End If
            End If
            'End of Calculate Penalty Amount of Transport is Transporter coming Late 


            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Entry_Date", clsCommon.GetPrintDate(obj.Entry_Date, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Shift_Date", clsCommon.GetPrintDate(obj.Shift_Date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Entry_Shift", obj.Entry_Shift)
            clsCommon.AddColumnsForChange(coll, "MCC_CODE", obj.MCC_Code)
            clsCommon.AddColumnsForChange(coll, "Route_Code", obj.Route_Code)
            clsCommon.AddColumnsForChange(coll, "Vehicle_No", obj.Vehicle_No)
            clsCommon.AddColumnsForChange(coll, "Vehicle_No_Other", obj.Vehicle_No_Other)
            clsCommon.AddColumnsForChange(coll, "Transporter_Code", obj.Transporter_Code)
            clsCommon.AddColumnsForChange(coll, "Cans_Filled", obj.Cans_Filled)
            clsCommon.AddColumnsForChange(coll, "Cans_Empty", obj.Cans_Empty)
            clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks)
            'DATE :23-JAN-17 > CLIENT : SAHAYOG 
            clsCommon.AddColumnsForChange(coll, "Manual_FAT_Per", obj.Manual_FAT_Per, True)
            clsCommon.AddColumnsForChange(coll, "Manual_SNF_Per", obj.Manual_SNF_Per, True)
            clsCommon.AddColumnsForChange(coll, "KM_Reading", obj.KMReading)
            clsCommon.AddColumnsForChange(coll, "Penalty_Amount", obj.Penalty_Amount)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
            If isNewEntry Then
                obj.Entry_Code = clsERPFuncationality.GetNextCode(trans, obj.Entry_Date, clsDocType.MilkGateEntryIn, "", obj.MCC_Code)
                If (clsCommon.myLen(obj.Entry_Code) <= 0) Then
                    Throw New Exception("Error in Document Code Generation")
                End If
                clsCommon.AddColumnsForChange(coll, "Entry_Code", obj.Entry_Code)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MILK_GATE_ENTRY_IN", OMInsertOrUpdate.Insert, "", trans)
            Else
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MILK_GATE_ENTRY_IN", OMInsertOrUpdate.Update, "TSPL_MILK_GATE_ENTRY_IN.Entry_Code='" + obj.Entry_Code + "'", trans)
            End If
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function

    Public Shared Function GetData(ByVal strDocNo As String, ByVal NavType As NavigatorType) As clsMilkGateEntryIn
        Return GetData(strDocNo, NavType, Nothing)
    End Function

    Public Shared Function GetData(ByVal strDocNo As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsMilkGateEntryIn
        Dim obj As clsMilkGateEntryIn = Nothing
        Dim qry As String = "select TSPL_MILK_GATE_ENTRY_IN.*,TSPL_MCC_MASTER.MCC_NAME,TSPL_MCC_ROUTE_MASTER.Route_Name,TSPL_VENDOR_MASTER.Vendor_Name from TSPL_MILK_GATE_ENTRY_IN " + Environment.NewLine + _
            " left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_CODE=TSPL_MILK_GATE_ENTRY_IN.MCC_CODE " + Environment.NewLine + _
            " left outer join TSPL_MCC_ROUTE_MASTER on TSPL_MCC_ROUTE_MASTER.Route_Code=TSPL_MILK_GATE_ENTRY_IN.Route_Code " + Environment.NewLine + _
            " left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_MILK_GATE_ENTRY_IN.Transporter_Code where 2=2 "
        Dim whrCls As String = ""
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            whrCls = " AND TSPL_MILK_GATE_ENTRY_IN.MCC_CODE in (" + objCommonVar.strCurrUserLocations + ")"
        End If
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_MILK_GATE_ENTRY_IN.Entry_Code = (select MIN(Entry_Code) from TSPL_MILK_GATE_ENTRY_IN WHERE 1=1 " + whrCls + ")"
            Case NavigatorType.Last
                qry += " and TSPL_MILK_GATE_ENTRY_IN.Entry_Code = (select Max(Entry_Code) from TSPL_MILK_GATE_ENTRY_IN WHERE 1=1 " + whrCls + ")"
            Case NavigatorType.Next
                qry += " and TSPL_MILK_GATE_ENTRY_IN.Entry_Code = (select Min(Entry_Code) from TSPL_MILK_GATE_ENTRY_IN where Entry_Code>'" + strDocNo + "' " + whrCls + ")"
            Case NavigatorType.Previous
                qry += " and TSPL_MILK_GATE_ENTRY_IN.Entry_Code = (select Max(Entry_Code) from TSPL_MILK_GATE_ENTRY_IN where Entry_Code<'" + strDocNo + "' " + whrCls + ")"
            Case NavigatorType.Current
                qry += " and TSPL_MILK_GATE_ENTRY_IN.Entry_Code = '" + strDocNo + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsMilkGateEntryIn()
            obj.Entry_Code = clsCommon.myCstr(dt.Rows(0)("Entry_Code"))
            obj.Entry_Date = clsCommon.myCDate(dt.Rows(0)("Entry_Date"))
            obj.Shift_Date = clsCommon.myCDate(dt.Rows(0)("Shift_Date"))
            obj.Entry_Shift = clsCommon.myCstr(dt.Rows(0)("Entry_Shift"))
            obj.MCC_Code = clsCommon.myCstr(dt.Rows(0)("MCC_CODE"))
            obj.MCC_Name = clsCommon.myCstr(dt.Rows(0)("MCC_NAME"))
            obj.Route_Code = clsCommon.myCstr(dt.Rows(0)("Route_Code"))
            obj.Route_Name = clsCommon.myCstr(dt.Rows(0)("Route_Name"))
            obj.Vehicle_No = clsCommon.myCstr(dt.Rows(0)("Vehicle_No"))
            obj.Vehicle_No_Other = clsCommon.myCstr(dt.Rows(0)("Vehicle_No_Other"))
            obj.Transporter_Code = clsCommon.myCstr(dt.Rows(0)("Transporter_Code"))
            obj.Transporter_Name = clsCommon.myCstr(dt.Rows(0)("Vendor_Name"))
            obj.Cans_Filled = clsCommon.myCdbl(dt.Rows(0)("Cans_Filled"))
            obj.Cans_Empty = clsCommon.myCdbl(dt.Rows(0)("Cans_Empty"))
            obj.Status = IIf(clsCommon.myCdbl(dt.Rows(0)("Status")) = 1, ERPTransactionStatus.Approved, ERPTransactionStatus.Pending)
            obj.Remarks = clsCommon.myCstr(dt.Rows(0)("Remarks"))
            obj.KMReading = clsCommon.myCdbl(dt.Rows(0)("KM_Reading"))
            obj.Penalty_Amount = clsCommon.myCdbl(dt.Rows(0)("Penalty_Amount"))
            'DATE :23-JAN-17 > CLIENT : SAHAYOG 
            obj.Manual_FAT_Per = clsCommon.myCdbl(dt.Rows(0)("Manual_FAT_Per"))
            obj.Manual_SNF_Per = clsCommon.myCdbl(dt.Rows(0)("Manual_SNF_Per"))

            If dt.Rows(0)("Posted_Date") IsNot DBNull.Value Then
                obj.Posted_Date = clsCommon.myCDate(dt.Rows(0)("Posted_Date"))
            End If
        End If
        Return obj
    End Function

    Public Shared Function PostData(ByVal strDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            PostData(strDocNo, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function PostData(ByVal strDocNo As String, ByVal trans As SqlTransaction) As Boolean
        Try
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Entry No not found to Post")
            End If
            Dim obj As clsMilkGateEntryIn = clsMilkGateEntryIn.GetData(strDocNo, NavigatorType.Current, trans)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.Entry_Code) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            If (obj.Status = ERPTransactionStatus.Approved) Then
                Throw New Exception("Already Post on :" + clsCommon.GetPrintDate(obj.Posted_Date, "dd/MM/yyyy"))
            End If

            Dim qry As String = "Update TSPL_MILK_GATE_ENTRY_IN set Status=1, Posted_Date='" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt") + "',Posted_By='" + objCommonVar.CurrentUserCode + "' where Entry_Code='" + strDocNo + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        If (clsCommon.myLen(strCode) <= 0) Then
            Throw New Exception("Entry No not found to Delete")
        End If
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim obj As clsMilkGateEntryIn = clsMilkGateEntryIn.GetData(strCode, NavigatorType.Current, trans)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.Entry_Code) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            If (obj.Status = ERPTransactionStatus.Approved) Then
                Throw New Exception("Already Posted on :" + clsCommon.GetPrintDate(obj.Posted_Date, "dd/MM/yyyy"))
            End If

            Dim qry As String = "delete from TSPL_MILK_GATE_ENTRY_IN where Entry_Code='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function ApprovePenaltyAmount(ByVal ArrGateEntryWithPenalty As ArrayList, ByVal tran As SqlTransaction) As Boolean
        If ArrGateEntryWithPenalty IsNot Nothing AndAlso ArrGateEntryWithPenalty.Count > 0 Then
            Dim qry As String = "Update TSPL_MILK_GATE_ENTRY_IN set Penalty_Status=1 where Entry_Code in (" + clsCommon.GetMulcallString(ArrGateEntryWithPenalty) + ")"
            clsDBFuncationality.ExecuteNonQuery(qry, tran)
        End If
        Return True
    End Function

    Public Shared Function GetQeryForPenalty(ByVal strMCCCode As String, ByVal strShift As String, ByVal dtShiftDate As Date, ByVal strRouteCode As String)
        Dim qry As String = "select TSPL_MILK_GATE_ENTRY_IN.Entry_Code," + Environment.NewLine + _
           "convert(varchar, TSPL_MILK_GATE_ENTRY_IN.Entry_Date,105)+' '+SUBSTRING( convert(varchar,TSPL_MILK_GATE_ENTRY_IN.Entry_Date,100),13,7) as Entry_Date" + Environment.NewLine + _
           ",SUBSTRING( convert(varchar,(case when  TSPL_MILK_GATE_ENTRY_IN.Entry_Shift='M' then TSPL_MCC_ROUTE_MASTER.MCC_Reaching_Time_M else TSPL_MCC_ROUTE_MASTER.MCC_Reaching_Time_E end),100),13,7) as ReachingTime  ,TSPL_MILK_GATE_ENTRY_IN.Route_Code,TSPL_MCC_ROUTE_MASTER.Route_Name,Vehicle_No,Transporter_Code ,TSPL_VENDOR_MASTER.Vendor_Name as TransporterName,TSPL_MILK_GATE_ENTRY_IN.Penalty_Amount" + Environment.NewLine + _
           " from TSPL_MILK_GATE_ENTRY_IN" + Environment.NewLine + _
           "left outer join TSPL_MCC_ROUTE_MASTER on TSPL_MCC_ROUTE_MASTER.Route_Code=TSPL_MILK_GATE_ENTRY_IN.Route_Code" + Environment.NewLine + _
           "left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_MILK_GATE_ENTRY_IN.Transporter_Code" + Environment.NewLine + _
           "where TSPL_MILK_GATE_ENTRY_IN.MCC_CODE='" + strMCCCode + "' and TSPL_MILK_GATE_ENTRY_IN.Entry_Shift='" + strShift + "' and TSPL_MILK_GATE_ENTRY_IN.Shift_Date='" + clsCommon.GetPrintDate(dtShiftDate, "dd/MMM/yyyy") + "' and isnull( TSPL_MILK_GATE_ENTRY_IN.Penalty_Amount,0)>0"
        If clsCommon.myLen(strRouteCode) > 0 Then
            qry += " and TSPL_MILK_GATE_ENTRY_IN.Route_Code='" + strRouteCode + "'"
        End If

        Return qry
    End Function
End Class


