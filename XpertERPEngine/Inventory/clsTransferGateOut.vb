Imports common
Imports System.Data.SqlClient
Public Class clsTransferGateOut
#Region "Variable"
    'Document_No,Document_Date,Vehicle_Mannual_No,From_Location,To_Location, Created_By,Created_Date,Modify_Date,Comp_Code
    Public Document_No As String = Nothing
    Public Gate_Out_Date As Date = Nothing
    Public Transfer_No As String = Nothing
    Public Transfer_Date As Date = Nothing
    Public Vehicle_Mannual_No As String = Nothing
    Public Vehicle_Id As String = Nothing          ' Not Table Column 
    Public From_Location As String = Nothing
    Public From_Location_Desc As String = Nothing  ' Not Table Column 
    Public To_Location As String = Nothing
    Public To_Location_Desc As String = Nothing    ' Not Table Column 
    Public Created_By As String = Nothing
    Public Created_Date As Date = Nothing
    Public Modify_By As String = Nothing
    Public Modify_Date As Date = Nothing
    Public Comp_Code As String = Nothing
    Public isNewEntry As Boolean = False

#End Region

    Public Shared Function deleteData(ByVal strDocNo As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim qry As String = "delete from TSPL_Transfer_Gate_Out where Document_No='" & strDocNo & "'"
            Dim isDeleted As Boolean = True
            isDeleted = isDeleted AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            Return isDeleted
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function getGateOutFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Try
            Dim qry As String = "  select TSPL_Transfer_Gate_Out.Document_No,TSPL_Transfer_Gate_Out.Gate_Out_Date,TSPL_Transfer_Gate_Out.Transfer_No,TSPL_Transfer_Gate_Out.Transfer_Date,TSPL_VEHICLE_MASTER.Vehicle_Id ,TSPL_Transfer_Gate_Out.Vehicle_Mannual_No,TSPL_Transfer_Gate_Out.From_Location,TSPL_LOCATION_MASTER_1.Location_Desc as From_Location_Des,TSPL_Transfer_Gate_Out.To_Location,TSPL_LOCATION_MASTER_2.Location_Desc as To_Location_Desc  from TSPL_Transfer_Gate_Out left outer join TSPL_VEHICLE_MASTER  on TSPL_VEHICLE_MASTER.Number=TSPL_Transfer_Gate_Out.Vehicle_Mannual_No left outer join  TSPL_LOCATION_MASTER as TSPL_LOCATION_MASTER_1 on TSPL_LOCATION_MASTER_1 .Location_Code=TSPL_Transfer_Gate_Out.From_Location  left outer join TSPL_LOCATION_MASTER as TSPL_LOCATION_MASTER_2 on TSPL_LOCATION_MASTER_2 .Location_Code=TSPL_Transfer_Gate_Out.To_Location  "
            str = customFinder.getFinder("DOCTRNGATOUT", qry, "", curcode, "", "Document_No")
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
        Return str
    End Function
    Public Shared Function getGateOutData(ByVal strCode As String, ByVal navtype As NavigatorType, Optional trans As SqlTransaction = Nothing) As clsTransferGateOut
        Dim obj As New clsTransferGateOut
        Try
            Dim whrCls As String = String.Empty
            Dim qst As String = " select TSPL_Transfer_Gate_Out.Document_No,TSPL_Transfer_Gate_Out.Gate_Out_Date,TSPL_Transfer_Gate_Out.Transfer_No,TSPL_Transfer_Gate_Out.Transfer_Date,TSPL_VEHICLE_MASTER.Vehicle_Id ,TSPL_Transfer_Gate_Out.Vehicle_Mannual_No,TSPL_Transfer_Gate_Out.From_Location,TSPL_LOCATION_MASTER_1.Location_Desc as From_Location_Desc,TSPL_Transfer_Gate_Out.To_Location,TSPL_LOCATION_MASTER_2.Location_Desc as To_Location_Desc  from TSPL_Transfer_Gate_Out left outer join TSPL_VEHICLE_MASTER  on TSPL_VEHICLE_MASTER.Number=TSPL_Transfer_Gate_Out.Vehicle_Mannual_No left outer join  TSPL_LOCATION_MASTER as TSPL_LOCATION_MASTER_1 on TSPL_LOCATION_MASTER_1 .Location_Code=TSPL_Transfer_Gate_Out.From_Location  left outer join TSPL_LOCATION_MASTER as TSPL_LOCATION_MASTER_2 on TSPL_LOCATION_MASTER_2 .Location_Code=TSPL_Transfer_Gate_Out.To_Location   where 1=1  and TSPL_Transfer_Gate_Out.comp_code='" & objCommonVar.CurrentCompanyCode & "' "

            qst = qst & whrCls
            Select Case navtype
                Case NavigatorType.Current
                    qst += " and TSPL_Transfer_Gate_Out.Document_No in ('" + strCode + "') "
                Case NavigatorType.Next
                    qst += " and TSPL_Transfer_Gate_Out.Document_No in (select min(Document_No ) from TSPL_Transfer_Gate_Out where Document_No  >'" + strCode + "'   " & whrCls & ")"
                Case NavigatorType.First
                    qst += " and TSPL_Transfer_Gate_Out.Document_No in (select MIN(Document_No ) from TSPL_Transfer_Gate_Out where 1=1  " & whrCls & ")"
                Case NavigatorType.Last
                    qst += " and TSPL_Transfer_Gate_Out.Document_No in (select Max(Document_No ) from TSPL_Transfer_Gate_Out where 1=1  " & whrCls & ")"
                Case NavigatorType.Previous
                    qst += " and TSPL_Transfer_Gate_Out.Document_No in (select Max(Document_No ) from TSPL_Transfer_Gate_Out where Document_No  <'" + strCode + "'   " & whrCls & ")"
            End Select
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qst, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                obj.Document_No = clsCommon.myCstr(dt.Rows(0)("Document_No"))
                obj.Gate_Out_Date = clsCommon.myCDate(dt.Rows(0)("Gate_Out_Date"))
                obj.Transfer_No = clsCommon.myCstr(dt.Rows(0)("Transfer_No"))
                obj.Transfer_Date = clsCommon.myCDate(dt.Rows(0)("Transfer_Date"))
                obj.Vehicle_Id = clsCommon.myCstr(dt.Rows(0)("Vehicle_Id"))
                obj.Vehicle_Mannual_No = clsCommon.myCstr(dt.Rows(0)("Vehicle_Mannual_No"))
                obj.From_Location = clsCommon.myCstr(dt.Rows(0)("From_Location"))
                obj.From_Location_Desc = clsCommon.myCstr(dt.Rows(0)("From_Location_Desc"))
                obj.To_Location = clsCommon.myCstr(dt.Rows(0)("To_Location"))
                obj.To_Location_Desc = clsCommon.myCstr(dt.Rows(0)("To_Location_Desc"))
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return obj
    End Function
    Public Shared Function getTransferFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Try
            Dim qry As String = "select TSPL_TRANSFER_ORDER_HEAD.Document_No as Transfer_No, convert(varchar, TSPL_TRANSFER_ORDER_HEAD.Document_Date, 103) as Transfer_Date,TSPL_VEHICLE_MASTER.Vehicle_Id ,TSPL_TRANSFER_ORDER_HEAD.Vehicle_Mannual_No ,TSPL_TRANSFER_ORDER_HEAD.Transport_Id ,TSPL_TRANSFER_ORDER_HEAD.From_Location,TSPL_LOCATION_MASTER_1.Location_Desc as From_Location_Des,TSPL_TRANSFER_ORDER_HEAD.To_Location,TSPL_LOCATION_MASTER_2.Location_Desc as To_Location_Desc from TSPL_TRANSFER_ORDER_HEAD  left outer join TSPL_VEHICLE_MASTER  on TSPL_VEHICLE_MASTER.Number=TSPL_TRANSFER_ORDER_HEAD.Vehicle_Mannual_No  left outer join  TSPL_LOCATION_MASTER as TSPL_LOCATION_MASTER_1 on TSPL_LOCATION_MASTER_1 .Location_Code=TSPL_TRANSFER_ORDER_HEAD.From_Location  left outer join TSPL_LOCATION_MASTER as TSPL_LOCATION_MASTER_2 on TSPL_LOCATION_MASTER_2 .Location_Code=TSPL_TRANSFER_ORDER_HEAD.To_Location   "  '  where TSPL_TRANSFER_ORDER_HEAD.Transfer_Type='O' and  TSPL_TRANSFER_ORDER_HEAD.Document_No not in (  select TSPL_Transfer_Gate_Out.Document_No from  TSPL_Transfer_Gate_Out )
            If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "KL") = CompairStringResult.Equal Then
                str = customFinder.getFinder("TRNGATOUT", qry, " TSPL_TRANSFER_ORDER_HEAD.Status=1 and TSPL_TRANSFER_ORDER_HEAD.Transfer_Type='O' and  TSPL_TRANSFER_ORDER_HEAD.Document_No not in (  select TSPL_Transfer_Gate_Out.Transfer_No from  TSPL_Transfer_Gate_Out )", curcode, "", "Transfer_No")
            Else
                str = customFinder.getFinder("TRNGATOUT", qry, " TSPL_TRANSFER_ORDER_HEAD.Transfer_Type='O' and  TSPL_TRANSFER_ORDER_HEAD.Document_No not in (  select TSPL_Transfer_Gate_Out.Transfer_No from  TSPL_Transfer_Gate_Out )", curcode, "", "Transfer_No")
                'str = clsCommon.ShowSelectForm("GTENGTOUT", qry, "TankerNo", whrcls, curcode, "GateEntryNo", isButtonClicked)
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
        Return str
    End Function
    Public Shared Function getTransferData(ByVal strCode As String, ByVal navtype As NavigatorType, Optional trans As SqlTransaction = Nothing) As clsTransferGateOut
        Dim obj As New clsTransferGateOut
        Try
            Dim whrCls As String = String.Empty
            Dim qst As String = " select TSPL_TRANSFER_ORDER_HEAD.Document_No as Transfer_No, convert (varchar,TSPL_TRANSFER_ORDER_HEAD.Document_Date,103) as Transfer_Date,TSPL_VEHICLE_MASTER.Vehicle_Id ,TSPL_TRANSFER_ORDER_HEAD.Vehicle_Mannual_No ,TSPL_TRANSFER_ORDER_HEAD.Transport_Id ,TSPL_TRANSFER_ORDER_HEAD.From_Location,TSPL_LOCATION_MASTER_1.Location_Desc as From_Location_Desc,TSPL_TRANSFER_ORDER_HEAD.To_Location,TSPL_LOCATION_MASTER_2.Location_Desc as To_Location_Desc from TSPL_TRANSFER_ORDER_HEAD  left outer join TSPL_VEHICLE_MASTER  on TSPL_VEHICLE_MASTER.Number=TSPL_TRANSFER_ORDER_HEAD.Vehicle_Mannual_No  left outer join  TSPL_LOCATION_MASTER as TSPL_LOCATION_MASTER_1 on TSPL_LOCATION_MASTER_1 .Location_Code=TSPL_TRANSFER_ORDER_HEAD.From_Location  left outer join TSPL_LOCATION_MASTER as TSPL_LOCATION_MASTER_2 on TSPL_LOCATION_MASTER_2 .Location_Code=TSPL_TRANSFER_ORDER_HEAD.To_Location      where 1=1 and TSPL_TRANSFER_ORDER_HEAD.Transfer_Type='O' and  TSPL_TRANSFER_ORDER_HEAD.Document_No not in (  select TSPL_Transfer_Gate_Out.Transfer_No from  TSPL_Transfer_Gate_Out )  and TSPL_TRANSFER_ORDER_HEAD.comp_code='" & objCommonVar.CurrentCompanyCode & "' "
           
            qst = qst & whrCls
            Select Case navtype
                Case NavigatorType.Current
                    qst += " and TSPL_TRANSFER_ORDER_HEAD.Document_No in ('" + strCode + "') "
                Case NavigatorType.Next
                    qst += " and TSPL_TRANSFER_ORDER_HEAD.Document_No in (select min(Document_No ) from TSPL_TRANSFER_ORDER_HEAD where Document_No  >'" + strCode + "'   " & whrCls & ")"
                Case NavigatorType.First
                    qst += " and TSPL_TRANSFER_ORDER_HEAD.Document_No in (select MIN(Document_No ) from TSPL_TRANSFER_ORDER_HEAD where 1=1  " & whrCls & ")"
                Case NavigatorType.Last
                    qst += " and TSPL_TRANSFER_ORDER_HEAD.Document_No in (select Max(Document_No ) from TSPL_TRANSFER_ORDER_HEAD where 1=1  " & whrCls & ")"
                Case NavigatorType.Previous
                    qst += " and TSPL_TRANSFER_ORDER_HEAD.Document_No in (select Max(Document_No ) from TSPL_TRANSFER_ORDER_HEAD where Document_No  <'" + strCode + "'   " & whrCls & ")"
            End Select
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qst, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                obj.Transfer_No = clsCommon.myCstr(dt.Rows(0)("Transfer_No"))
                obj.Transfer_Date = clsCommon.myCDate(dt.Rows(0)("Transfer_Date"))
                obj.Vehicle_Id = clsCommon.myCstr(dt.Rows(0)("Vehicle_Id"))
                obj.Vehicle_Mannual_No = clsCommon.myCstr(dt.Rows(0)("Vehicle_Mannual_No"))
                obj.From_Location = clsCommon.myCstr(dt.Rows(0)("From_Location"))
                obj.From_Location_Desc = clsCommon.myCstr(dt.Rows(0)("From_Location_Desc"))
                obj.To_Location = clsCommon.myCstr(dt.Rows(0)("To_Location"))
                obj.To_Location_Desc = clsCommon.myCstr(dt.Rows(0)("To_Location_Desc"))
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return obj
    End Function

   
    Public Shared Function saveData(ByVal obj As clsTransferGateOut, Optional ByVal trans As SqlTransaction = Nothing) As Boolean
        Dim issaved As Boolean = True

        Try


            Dim coll As New Hashtable()
            'trans = clsDBFuncationality.GetTransactin()
            'obj.Document_No = "DOC0001"
            If clsCommon.myLen(obj.Document_No) < 1 Then
                obj.Document_No = clsERPFuncationality.GetNextCode(trans, clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"), clsDocType.TransferGateOut, "", "")
                If (clsCommon.myLen(obj.Document_No) <= 0) Then
                    Throw New Exception("Error in Document Code Generation")
                End If
            End If
            clsCommon.AddColumnsForChange(coll, "Document_No", clsCommon.myCstr(obj.Document_No))
            clsCommon.AddColumnsForChange(coll, "Gate_Out_Date", clsCommon.GetPrintDate(obj.Gate_Out_Date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Transfer_No", clsCommon.myCstr(obj.Transfer_No))
            clsCommon.AddColumnsForChange(coll, "Transfer_Date", clsCommon.GetPrintDate(obj.Transfer_Date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Vehicle_Mannual_No", clsCommon.myCstr(obj.Vehicle_Mannual_No))
            clsCommon.AddColumnsForChange(coll, "From_Location", clsCommon.myCstr(obj.From_Location))
            clsCommon.AddColumnsForChange(coll, "To_Location", clsCommon.myCstr(obj.To_Location))
            clsCommon.AddColumnsForChange(coll, "Modify_By", clsCommon.myCstr(obj.Modify_By))
            clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(obj.Modify_Date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Comp_Code", clsCommon.myCstr(obj.Comp_Code))
            If obj.isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", clsCommon.myCstr(obj.Created_By))
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(obj.Created_Date, "dd/MMM/yyyy"))
                issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Transfer_Gate_Out", OMInsertOrUpdate.Insert, "", trans)
            Else
                issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Transfer_Gate_Out", OMInsertOrUpdate.Update, "TSPL_Transfer_Gate_Out.Document_No='" + obj.Document_No + "'", trans)
            End If
            'trans.Commit()
        Catch ex As Exception
            'trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return issaved
    End Function

End Class
