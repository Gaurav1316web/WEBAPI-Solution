Imports common
Imports System.Data.SqlClient

Public Class clsMilkGateEntryWeighment
#Region "Variables"
    Public Weighment_Code As String = Nothing
    Public GW_Weighment_Date As DateTime
    Public TW_Weighment_Date As DateTime?
    Public Against_Gate_Entry_No As String = Nothing
    Public Gross_Weight As String = Nothing
    Public Tare_Weight As String = Nothing
    Public Net_Weight As String = Nothing
    Public GW_Status As ERPTransactionStatus = ERPTransactionStatus.Pending
    Public GW_Posted_Date As DateTime?
    Public TW_Status As ERPTransactionStatus = ERPTransactionStatus.Pending
    Public TW_Posted_Date As DateTime
    Public TW_Modified_By As String = Nothing
#End Region

    Public Function SaveDataGW(ByVal obj As clsMilkGateEntryWeighment, ByVal isNewEntry As Boolean) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            SaveDataGW(obj, isNewEntry, trans)
            trans.Commit()
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function
    Public Function SaveDataGW(ByVal obj As clsMilkGateEntryWeighment, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim qry As String = "select Weighment_Code,case when GW_Status=1 then 'Posted' else 'Pending' end as GW_Status from TSPL_MILK_GATE_ENTRY_WEIGHTMENT where Against_Gate_Entry_No='" + obj.Against_Gate_Entry_No + "' and  Weighment_Code not in ('" + obj.Weighment_Code + "') "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Throw New Exception("Gate Entry No " + obj.Against_Gate_Entry_No + " is already entered.Weightment Gate Entry No- " + clsCommon.myCstr(dt.Rows(0)("Weighment_Code")) + " and Status " + clsCommon.myCstr(dt.Rows(0)("GW_Status")))
            End If
            qry = "select 1 from TSPL_MILK_GATE_ENTRY_WEIGHTMENT where GW_Status=1 and Weighment_Code in ('" + obj.Weighment_Code + "') "
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Throw New Exception("Gross Weighment Approved of Transaction no -" + obj.Weighment_Code)
            End If

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "GW_Weighment_Date", clsCommon.GetPrintDate(obj.GW_Weighment_Date, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Against_Gate_Entry_No", obj.Against_Gate_Entry_No)
            clsCommon.AddColumnsForChange(coll, "Gross_Weight", obj.Gross_Weight)
            clsCommon.AddColumnsForChange(coll, "GW_Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "GW_Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
            If isNewEntry Then
                Dim strLoc As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select MCC_CODE   from TSPL_MILK_GATE_ENTRY_IN where Entry_Code='" + obj.Against_Gate_Entry_No + "' ", trans))
                If clsCommon.myLen(strLoc) <= 0 Then
                    Throw New Exception("Location not found of Gate Entry In no" + obj.Against_Gate_Entry_No)
                End If

                obj.Weighment_Code = clsERPFuncationality.GetNextCode(trans, obj.GW_Weighment_Date, clsDocType.MilkGateEntryWeighment, "", strLoc)
                If (clsCommon.myLen(obj.Weighment_Code) <= 0) Then
                    Throw New Exception("Error in Document Code Generation")
                End If
                clsCommon.AddColumnsForChange(coll, "Weighment_Code", obj.Weighment_Code)
                clsCommon.AddColumnsForChange(coll, "GW_Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "GW_Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MILK_GATE_ENTRY_WEIGHTMENT", OMInsertOrUpdate.Insert, "", trans)
            Else
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MILK_GATE_ENTRY_WEIGHTMENT", OMInsertOrUpdate.Update, "TSPL_MILK_GATE_ENTRY_WEIGHTMENT.Weighment_Code='" + obj.Weighment_Code + "'", trans)
            End If
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function

    Public Function SaveDataTW(ByVal obj As clsMilkGateEntryWeighment) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            SaveDataTW(obj, True, trans)
            trans.Commit()
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function
    Public Function SaveDataTW(ByVal obj As clsMilkGateEntryWeighment, ByVal isCheckCondition As Boolean, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim qry As String = "select 1 from TSPL_MILK_GATE_ENTRY_WEIGHTMENT where TW_Status=1 and Weighment_Code in ('" + obj.Weighment_Code + "') "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Throw New Exception("Tare Weighment Approved of Transaction no -" + obj.Weighment_Code)
            End If
            If isCheckCondition Then
                qry = "select 1 from TSPL_MILK_RECEIPT_DETAIL" + Environment.NewLine + _
                " left outer join TSPL_MILK_RECEIPT_HEAD on TSPL_MILK_RECEIPT_HEAD.DOC_CODE=TSPL_MILK_RECEIPT_DETAIL.DOC_CODE " + Environment.NewLine + _
                " where exists (select 1 from TSPL_MILK_GATE_ENTRY_IN where Entry_Code='" + obj.Against_Gate_Entry_No + "' and TSPL_MILK_GATE_ENTRY_IN.Route_Code=TSPL_MILK_RECEIPT_DETAIL.ROUTE_CODE and TSPL_MILK_GATE_ENTRY_IN.MCC_CODE=TSPL_MILK_RECEIPT_HEAD.MCC_CODE and  convert(date,TSPL_MILK_GATE_ENTRY_IN.Shift_Date,103)= convert(date,TSPL_MILK_RECEIPT_HEAD.DOC_DATE,103)  and  TSPL_MILK_GATE_ENTRY_IN.Entry_Shift=TSPL_MILK_RECEIPT_HEAD.SHIFT)"
                dt = clsDBFuncationality.GetDataTable(qry, trans)
                If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                    Throw New Exception("Please first make the milk receipt entry of current route ")
                End If
            End If


            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "TW_Weighment_Date", clsCommon.GetPrintDate(obj.TW_Weighment_Date, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Tare_Weight", obj.Tare_Weight)
            clsCommon.AddColumnsForChange(coll, "Net_Weight", obj.Net_Weight)
            clsCommon.AddColumnsForChange(coll, "TW_Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "TW_Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MILK_GATE_ENTRY_WEIGHTMENT", OMInsertOrUpdate.Update, "TSPL_MILK_GATE_ENTRY_WEIGHTMENT.Weighment_Code='" + obj.Weighment_Code + "'", trans)
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function

    Public Shared Function GetData(ByVal strDocNo As String, ByVal NavType As NavigatorType) As clsMilkGateEntryWeighment
        Return GetData(strDocNo, NavType, Nothing)
    End Function

    Public Shared Function GetData(ByVal strDocNo As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsMilkGateEntryWeighment
        Dim obj As clsMilkGateEntryWeighment = Nothing
        Dim qry As String = "select TSPL_MILK_GATE_ENTRY_WEIGHTMENT.* from TSPL_MILK_GATE_ENTRY_WEIGHTMENT left outer join TSPL_MILK_GATE_ENTRY_IN on TSPL_MILK_GATE_ENTRY_IN.Entry_Code=TSPL_MILK_GATE_ENTRY_WEIGHTMENT.Against_Gate_Entry_No  where 2=2 " + Environment.NewLine
        Dim whrCls As String = ""
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            whrCls = " AND TSPL_MILK_GATE_ENTRY_IN.MCC_CODE in (" + objCommonVar.strCurrUserLocations + ")"
        End If
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_MILK_GATE_ENTRY_WEIGHTMENT.Weighment_Code = (select MIN(Weighment_Code) from TSPL_MILK_GATE_ENTRY_WEIGHTMENT left outer join TSPL_MILK_GATE_ENTRY_IN on TSPL_MILK_GATE_ENTRY_IN.Entry_Code=TSPL_MILK_GATE_ENTRY_WEIGHTMENT.Against_Gate_Entry_No WHERE 1=1 " + whrCls + ")"
            Case NavigatorType.Last
                qry += " and TSPL_MILK_GATE_ENTRY_WEIGHTMENT.Weighment_Code = (select Max(Weighment_Code) from TSPL_MILK_GATE_ENTRY_WEIGHTMENT left outer join TSPL_MILK_GATE_ENTRY_IN on TSPL_MILK_GATE_ENTRY_IN.Entry_Code=TSPL_MILK_GATE_ENTRY_WEIGHTMENT.Against_Gate_Entry_No WHERE 1=1 " + whrCls + ")"
            Case NavigatorType.Next
                qry += " and TSPL_MILK_GATE_ENTRY_WEIGHTMENT.Weighment_Code = (select Min(Weighment_Code) from TSPL_MILK_GATE_ENTRY_WEIGHTMENT left outer join TSPL_MILK_GATE_ENTRY_IN on TSPL_MILK_GATE_ENTRY_IN.Entry_Code=TSPL_MILK_GATE_ENTRY_WEIGHTMENT.Against_Gate_Entry_No where Weighment_Code>'" + strDocNo + "' " + whrCls + ")"
            Case NavigatorType.Previous
                qry += " and TSPL_MILK_GATE_ENTRY_WEIGHTMENT.Weighment_Code = (select Max(Weighment_Code) from TSPL_MILK_GATE_ENTRY_WEIGHTMENT left outer join TSPL_MILK_GATE_ENTRY_IN on TSPL_MILK_GATE_ENTRY_IN.Entry_Code=TSPL_MILK_GATE_ENTRY_WEIGHTMENT.Against_Gate_Entry_No where Weighment_Code<'" + strDocNo + "' " + whrCls + ")"
            Case NavigatorType.Current
                qry += " and TSPL_MILK_GATE_ENTRY_WEIGHTMENT.Weighment_Code = '" + strDocNo + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsMilkGateEntryWeighment()
            obj.Weighment_Code = clsCommon.myCstr(dt.Rows(0)("Weighment_Code"))
            obj.GW_Weighment_Date = clsCommon.myCDate(dt.Rows(0)("GW_Weighment_Date"))

            If dt.Rows(0)("TW_Weighment_Date") IsNot DBNull.Value Then
                obj.TW_Weighment_Date = clsCommon.myCDate(dt.Rows(0)("TW_Weighment_Date"))
            End If
            obj.Against_Gate_Entry_No = clsCommon.myCstr(dt.Rows(0)("Against_Gate_Entry_No"))
            obj.Gross_Weight = clsCommon.myCdbl(dt.Rows(0)("Gross_Weight"))
            obj.Tare_Weight = clsCommon.myCdbl(dt.Rows(0)("Tare_Weight"))
            obj.Net_Weight = clsCommon.myCdbl(dt.Rows(0)("Net_Weight"))
            obj.GW_Status = IIf(clsCommon.myCdbl(dt.Rows(0)("GW_Status")) = 1, ERPTransactionStatus.Approved, ERPTransactionStatus.Pending)
            If dt.Rows(0)("GW_Posted_Date") IsNot DBNull.Value Then
                obj.GW_Posted_Date = clsCommon.myCDate(dt.Rows(0)("GW_Posted_Date"))
            End If
            obj.TW_Status = IIf(clsCommon.myCdbl(dt.Rows(0)("TW_Status")) = 1, ERPTransactionStatus.Approved, ERPTransactionStatus.Pending)
            If dt.Rows(0)("TW_Posted_Date") IsNot DBNull.Value Then
                obj.TW_Posted_Date = clsCommon.myCDate(dt.Rows(0)("TW_Posted_Date"))
            End If
            obj.TW_Modified_By = clsCommon.myCstr(dt.Rows(0)("TW_Modified_By"))

        End If
        Return obj
    End Function

    Public Shared Function PostDataGW(ByVal strDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            PostDataGW(strDocNo, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function PostDataGW(ByVal strDocNo As String, ByVal trans As SqlTransaction) As Boolean
        Try
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Entry No not found to Post")
            End If
            Dim obj As clsMilkGateEntryWeighment = clsMilkGateEntryWeighment.GetData(strDocNo, NavigatorType.Current, trans)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.Weighment_Code) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            If (obj.GW_Status = ERPTransactionStatus.Approved) Then
                Throw New Exception("Gross Weight Already Post on :" + clsCommon.GetPrintDate(obj.GW_Posted_Date, "dd/MM/yyyy"))
            End If

            Dim qry As String = "Update TSPL_MILK_GATE_ENTRY_WEIGHTMENT set GW_Status=1, GW_Posted_Date='" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt") + "',GW_Posted_By='" + objCommonVar.CurrentUserCode + "' where Weighment_Code='" + strDocNo + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function PostDataTW(ByVal strDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            PostDataTW(strDocNo, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function PostDataTW(ByVal strDocNo As String, ByVal trans As SqlTransaction) As Boolean
        Try
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Entry No not found to Post")
            End If
            Dim obj As clsMilkGateEntryWeighment = clsMilkGateEntryWeighment.GetData(strDocNo, NavigatorType.Current, trans)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.Weighment_Code) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            If (obj.TW_Status = ERPTransactionStatus.Approved) Then
                Throw New Exception("Tare Weight Already Post on :" + clsCommon.GetPrintDate(obj.TW_Posted_Date, "dd/MM/yyyy"))
            End If

            Dim qry As String = "Update TSPL_MILK_GATE_ENTRY_WEIGHTMENT set TW_Status=1, TW_Posted_Date='" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt") + "',TW_Posted_By='" + objCommonVar.CurrentUserCode + "' where Weighment_Code='" + strDocNo + "'"
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
            Dim obj As clsMilkGateEntryWeighment = clsMilkGateEntryWeighment.GetData(strCode, NavigatorType.Current, trans)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.Weighment_Code) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            If (obj.GW_Status = ERPTransactionStatus.Approved) Then
                Throw New Exception("Already Posted on :" + clsCommon.GetPrintDate(obj.GW_Posted_Date, "dd/MM/yyyy"))
            End If
             
            Dim qry As String = "delete from TSPL_MILK_GATE_ENTRY_WEIGHTMENT where Weighment_Code='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
End Class
