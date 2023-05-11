Imports common
Imports System.Data.SqlClient
Public Class clsMilkGateEntryOut
#Region "Variables"
    Public Gate_Out_Code As String = Nothing
    Public Gate_Out_Date As DateTime
    Public Against_Gate_Entry_No As String = Nothing
    Public Status As ERPTransactionStatus = ERPTransactionStatus.Pending
    Public Cans_Filled As Integer = 0
    Public Cans_Empty As Integer = 0
    Public Posted_Date As DateTime
    Public Remarks As String = Nothing
    Public Is_Gateout_Without_Milk_Receipt As Boolean = False
    Public Reason_Gateout_Without_Milk_Receipt As String = Nothing
#End Region

    Public Function SaveData(ByVal obj As clsMilkGateEntryOut, ByVal isNewEntry As Boolean) As Boolean
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

    Public Function SaveData(ByVal obj As clsMilkGateEntryOut, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim qry As String = "select Gate_Out_Code,case when Status=1 then 'Posted' else 'Pending' end as Status from TSPL_MILK_GATE_ENTRY_OUT where Against_Gate_Entry_No='" + obj.Against_Gate_Entry_No + "' and  Gate_Out_Code not in ('" + obj.Gate_Out_Code + "') "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Throw New Exception("Gate Entry No " + obj.Against_Gate_Entry_No + " is already out.Gate Out No- " + clsCommon.myCstr(dt.Rows(0)("Gate_Out_Code")) + " and Status " + clsCommon.myCstr(dt.Rows(0)("Status")))
            End If

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Gate_Out_Date", clsCommon.GetPrintDate(obj.Gate_Out_Date, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Against_Gate_Entry_No", obj.Against_Gate_Entry_No)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))

            clsCommon.AddColumnsForChange(coll, "Cans_Filled", obj.Cans_Filled)
            clsCommon.AddColumnsForChange(coll, "Cans_Empty", obj.Cans_Empty)
            clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks)

            clsCommon.AddColumnsForChange(coll, "Is_Gateout_Without_Milk_Receipt", IIf(obj.Is_Gateout_Without_Milk_Receipt, 1, 0))
            clsCommon.AddColumnsForChange(coll, "Reason_Gateout_Without_Milk_Receipt", IIf(obj.Is_Gateout_Without_Milk_Receipt, obj.Reason_Gateout_Without_Milk_Receipt, Nothing))

            If isNewEntry Then
                Dim strLoc As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select MCC_CODE from TSPL_MILK_GATE_ENTRY_IN where Entry_Code='" + obj.Against_Gate_Entry_No + "' ", trans))
                If clsCommon.myLen(strLoc) <= 0 Then
                    Throw New Exception("Location not found of Gate Entry In no" + obj.Against_Gate_Entry_No)
                End If
                obj.Gate_Out_Code = clsERPFuncationality.GetNextCode(trans, obj.Gate_Out_Date, clsDocType.MilkGateEntryOut, "", strLoc)
                If (clsCommon.myLen(obj.Gate_Out_Code) <= 0) Then
                    Throw New Exception("Error in Document Code Generation")
                End If
                clsCommon.AddColumnsForChange(coll, "Gate_Out_Code", obj.Gate_Out_Code)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MILK_GATE_ENTRY_OUT", OMInsertOrUpdate.Insert, "", trans)
            Else
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MILK_GATE_ENTRY_OUT", OMInsertOrUpdate.Update, "TSPL_MILK_GATE_ENTRY_OUT.Gate_Out_Code='" + obj.Gate_Out_Code + "'", trans)
            End If
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function

    Public Shared Function GetData(ByVal strDocNo As String, ByVal NavType As NavigatorType) As clsMilkGateEntryOut
        Return GetData(strDocNo, NavType, Nothing)
    End Function

    Public Shared Function GetData(ByVal strDocNo As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsMilkGateEntryOut
        Dim obj As clsMilkGateEntryOut = Nothing
        Dim qry As String = "select TSPL_MILK_GATE_ENTRY_OUT.* from TSPL_MILK_GATE_ENTRY_OUT left outer join TSPL_MILK_GATE_ENTRY_IN on TSPL_MILK_GATE_ENTRY_IN.Entry_Code=TSPL_MILK_GATE_ENTRY_OUT.Against_Gate_Entry_No where 2=2 " + Environment.NewLine
        Dim whrCls As String = ""
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            whrCls = " AND TSPL_MILK_GATE_ENTRY_IN.MCC_CODE in (" + objCommonVar.strCurrUserLocations + ")"
        End If
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_MILK_GATE_ENTRY_OUT.Gate_Out_Code = (select MIN(Gate_Out_Code) from TSPL_MILK_GATE_ENTRY_OUT left outer join TSPL_MILK_GATE_ENTRY_IN on TSPL_MILK_GATE_ENTRY_IN.Entry_Code=TSPL_MILK_GATE_ENTRY_OUT.Against_Gate_Entry_No WHERE 1=1 " + whrCls + ")"
            Case NavigatorType.Last
                qry += " and TSPL_MILK_GATE_ENTRY_OUT.Gate_Out_Code = (select Max(Gate_Out_Code) from TSPL_MILK_GATE_ENTRY_OUT left outer join TSPL_MILK_GATE_ENTRY_IN on TSPL_MILK_GATE_ENTRY_IN.Entry_Code=TSPL_MILK_GATE_ENTRY_OUT.Against_Gate_Entry_No WHERE 1=1 " + whrCls + ")"
            Case NavigatorType.Next
                qry += " and TSPL_MILK_GATE_ENTRY_OUT.Gate_Out_Code = (select Min(Gate_Out_Code) from TSPL_MILK_GATE_ENTRY_OUT left outer join TSPL_MILK_GATE_ENTRY_IN on TSPL_MILK_GATE_ENTRY_IN.Entry_Code=TSPL_MILK_GATE_ENTRY_OUT.Against_Gate_Entry_No where Gate_Out_Code>'" + strDocNo + "' " + whrCls + ")"
            Case NavigatorType.Previous
                qry += " and TSPL_MILK_GATE_ENTRY_OUT.Gate_Out_Code = (select Max(Gate_Out_Code) from TSPL_MILK_GATE_ENTRY_OUT left outer join TSPL_MILK_GATE_ENTRY_IN on TSPL_MILK_GATE_ENTRY_IN.Entry_Code=TSPL_MILK_GATE_ENTRY_OUT.Against_Gate_Entry_No where Gate_Out_Code<'" + strDocNo + "' " + whrCls + ")"
            Case NavigatorType.Current
                qry += " and TSPL_MILK_GATE_ENTRY_OUT.Gate_Out_Code = '" + strDocNo + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsMilkGateEntryOut()
            obj.Gate_Out_Code = clsCommon.myCstr(dt.Rows(0)("Gate_Out_Code"))
            obj.Gate_Out_Date = clsCommon.myCDate(dt.Rows(0)("Gate_Out_Date"))
            obj.Against_Gate_Entry_No = clsCommon.myCstr(dt.Rows(0)("Against_Gate_Entry_No"))
            obj.Cans_Empty = clsCommon.myCdbl(dt.Rows(0)("Cans_Empty"))
            obj.Cans_Filled = clsCommon.myCdbl(dt.Rows(0)("Cans_Filled"))
            obj.Remarks = clsCommon.myCstr(dt.Rows(0)("Remarks"))
            obj.Status = IIf(clsCommon.myCdbl(dt.Rows(0)("Status")) = 1, ERPTransactionStatus.Approved, ERPTransactionStatus.Pending)
            obj.Is_Gateout_Without_Milk_Receipt = IIf(clsCommon.myCdbl(dt.Rows(0)("Is_Gateout_Without_Milk_Receipt")) = 1, True, False)
            obj.Reason_Gateout_Without_Milk_Receipt = clsCommon.myCstr(dt.Rows(0)("Reason_Gateout_Without_Milk_Receipt"))
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
            Dim obj As clsMilkGateEntryOut = clsMilkGateEntryOut.GetData(strDocNo, NavigatorType.Current, trans)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.Gate_Out_Code) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            If (obj.Status = ERPTransactionStatus.Approved) Then
                Throw New Exception("Gross Weight Already Post on :" + clsCommon.GetPrintDate(obj.Posted_Date, "dd/MM/yyyy"))
            End If

            Dim qry As String = "Update TSPL_MILK_GATE_ENTRY_OUT set Status=1, Posted_Date='" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt") + "',Posted_By='" + objCommonVar.CurrentUserCode + "' where Gate_Out_Code='" + strDocNo + "'"
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
            Dim obj As clsMilkGateEntryOut = clsMilkGateEntryOut.GetData(strCode, NavigatorType.Current, trans)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.Gate_Out_Code) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            If (obj.Status = ERPTransactionStatus.Approved) Then
                Throw New Exception("Already Posted on :" + clsCommon.GetPrintDate(obj.Posted_Date, "dd/MM/yyyy"))
            End If

            Dim qry As String = "delete from TSPL_MILK_GATE_ENTRY_OUT where Gate_Out_Code='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
End Class

