Imports common
Imports System.Data.SqlClient
Public Class clsRALNOC
#Region "Variables"
    Public Document_No As String = Nothing
    Public Document_Date As DateTime
    Public Tender_No As String = Nothing
    Public Vendor_Code As String = Nothing
    Public VendorName As String = Nothing
    Public Item_Code As String = Nothing
    Public ItemName As String = Nothing
    Public Location_Code As String = Nothing
    Public LocationName As String = Nothing
    Public Remarks As String = Nothing
    Public Status As ERPTransactionStatus = ERPTransactionStatus.Pending
    Public ArrSchedule As List(Of clsRALNOCSchedule) = Nothing

#End Region
    Public Function SaveData(ByVal obj As clsRALNOC, ByVal isNewEntry As Boolean) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Dim dts As DataTable = clsDBFuncationality.GetDataTable(" select TSPL_RAL_NOC.Document_No, TSPL_RAL_NOC.Location_Code from TSPL_RAL_NOC where Document_No= '" + obj.Document_No + "' ", trans)
        If dts IsNot Nothing AndAlso dts.Rows.Count > 0 Then
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModulePurchase, clsUserMgtCode.TenderShortPenalty, clsCommon.myCstr(dts.Rows(0)("Location_Code")), obj.Document_Date, trans)
        End If
        Try
            Dim qry As String = "delete from TSPL_TENDER_PENALTY_DETAIL where Document_No='" + obj.Document_No + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Dim strDocNo As String = ""
            If isNewEntry Then
                obj.Document_No = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.RALNoc, "", obj.Location_Code)
            End If
            If (clsCommon.myLen(obj.Document_No) <= 0) Then
                Throw New Exception("Error in Document Code Generation")
            End If
            Dim ServerDate As DateTime = clsCommon.GETSERVERDATE(trans)
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Location_Code", obj.Location_Code)
            clsCommon.AddColumnsForChange(coll, "Tender_No", obj.Tender_No)
            clsCommon.AddColumnsForChange(coll, "Vendor_Code", obj.Vendor_Code)
            clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code)
            clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks)

            clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(ServerDate, "dd/MMM/yyyy hh:mm:ss tt"))

            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Document_No", obj.Document_No)

                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(ServerDate, "dd/MMM/yyyy hh:mm:ss tt"))
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_RAL_NOC", OMInsertOrUpdate.Insert, "", trans)
            Else
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_RAL_NOC", OMInsertOrUpdate.Update, "TSPL_RAL_NOC.Document_No='" + obj.Document_No + "'", trans)
            End If
            clsRALNOCSchedule.SaveData(obj.Document_No, obj.ArrSchedule, trans)

            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, clsCommon.myCstr(obj.Document_No), "TSPL_RAL_NOC", "Document_No", "TSPL_TENDER_PENALTY_DETAIL", "Document_No", trans)
            trans.Commit()
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function
    Public Shared Function GetData(ByVal strPONo As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsRALNOC
        Dim obj As New clsRALNOC
        Dim qry As String = "SELECT TSPL_RAL_NOC.*,TSPL_LOCATION_MASTER.Location_Desc,TSPL_VENDOR_MASTER.Vendor_Name,TSPL_ITEM_MASTER.Item_Desc
FROM TSPL_RAL_NOC 
left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_RAL_NOC.Location_Code 
left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_RAL_NOC.Vendor_Code 
left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_RAL_NOC.Item_Code
where 2=2"
        Dim whrCls As String = ""
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            whrCls = " And Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
        End If
        Select Case NavType
            Case NavigatorType.First
                qry += " And TSPL_RAL_NOC.Document_No = (select MIN(Document_No) from TSPL_RAL_NOC WHERE 1=1 " + whrCls + ")"
            Case NavigatorType.Last
                qry += " And TSPL_RAL_NOC.Document_No = (select Max(Document_No) from TSPL_RAL_NOC WHERE 1=1 " + whrCls + ")"
            Case NavigatorType.Current
                qry += " And TSPL_RAL_NOC.Document_No = '" + strPONo + "'"
            Case NavigatorType.Next
                qry += " and TSPL_RAL_NOC.Document_No = (select Min(Document_No) from TSPL_RAL_NOC where Document_No>'" + strPONo + "' " + whrCls + ")"
            Case NavigatorType.Previous
                qry += " and TSPL_RAL_NOC.Document_No = (select Max(Document_No) from TSPL_RAL_NOC where Document_No<'" + strPONo + "' " + whrCls + ")"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj.Document_No = clsCommon.myCstr(dt.Rows(0)("Document_No"))
            obj.Document_Date = clsCommon.myCDate(dt.Rows(0)("Document_Date"))
            obj.Location_Code = clsCommon.myCstr(dt.Rows(0)("Location_Code"))
            obj.LocationName = clsCommon.myCstr(dt.Rows(0)("Location_Desc"))

            obj.Tender_No = clsCommon.myCstr(dt.Rows(0)("Tender_No"))
            obj.Vendor_Code = clsCommon.myCstr(dt.Rows(0)("Vendor_Code"))
            obj.VendorName = clsCommon.myCstr(dt.Rows(0)("Vendor_Name"))
            obj.Item_Code = clsCommon.myCstr(dt.Rows(0)("Item_Code"))
            obj.ItemName = clsCommon.myCstr(dt.Rows(0)("Item_Desc"))
            obj.Remarks = clsCommon.myCstr(dt.Rows(0)("Remarks"))
            obj.Status = IIf(clsCommon.myCdbl(dt.Rows(0)("Status")) = 1, ERPTransactionStatus.Approved, ERPTransactionStatus.Pending)
            obj.ArrSchedule = clsRALNOCSchedule.GetData(obj.Document_No, trans)

        End If
        Return obj
    End Function
    Public Shared Function PostData(ByVal strDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Document No not found to Post")
            End If
            Dim obj As clsRALNOC = clsRALNOC.GetData(strDocNo, NavigatorType.Current, trans)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.Document_No) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModulePurchase, clsUserMgtCode.RALNOC, obj.Location_Code, obj.Document_Date, trans)
            If (obj.Status = ERPTransactionStatus.Approved) Then
                Throw New Exception("Already Posted")
            End If
            Dim qry As String = "insert into TSPL_RAL_NOC_ORG_SCHEDULE (Document_No,PK_Id,PSNo,Schedule_No,From_Date,To_Date,Schedule_Qty_Per,Schedule_Qty,Schedule_Short_Per,Schedule_Short,Late_Days,Extension_Days,Item_Type )
 select '" + obj.Document_No + "' as Document_No,PK_Id,PSNo,Schedule_No,From_Date,To_Date,Schedule_Qty_Per,Schedule_Qty,Schedule_Short_Per,Schedule_Short,Late_Days,Extension_Days,Item_Type from TSPL_TENDER_SCHEDULE where DocumentCode='" + obj.Tender_No + "' and Vendor_Code='" + obj.Vendor_Code + "' and Location_Code='" + obj.Location_Code + "' and Item_Code='" + obj.Item_Code + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "insert into TSPL_RAL_NOC_ORG_SCHEDULE_PENALTY (PK_Id,Against_Tender_Schedule_PK_Id,Penalty_Date,Penalty )
select PK_Id,Against_Tender_Schedule_PK_Id,Penalty_Date,Penalty from TSPL_TENDER_SCHEDULE_PENALTY where Against_Tender_Schedule_PK_Id in ( select PK_Id from TSPL_TENDER_SCHEDULE where DocumentCode='" + obj.Tender_No + "' and Vendor_Code='" + obj.Vendor_Code + "' and Location_Code='" + obj.Location_Code + "' and Item_Code='" + obj.Item_Code + "')"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_TENDER_SCHEDULE_PENALTY where Against_Tender_Schedule_PK_Id in ( select PK_Id from TSPL_TENDER_SCHEDULE where DocumentCode='" + obj.Tender_No + "' and Vendor_Code='" + obj.Vendor_Code + "' and Location_Code='" + obj.Location_Code + "' and Item_Code='" + obj.Item_Code + "')"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete  from TSPL_TENDER_SCHEDULE where DocumentCode='" + obj.Tender_No + "' and Vendor_Code='" + obj.Vendor_Code + "' and Location_Code='" + obj.Location_Code + "' and Item_Code='" + obj.Item_Code + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Dim ArrSchedule As List(Of clsTenderSchedule) = DeepCopy(obj.Tender_No, obj.Vendor_Code, obj.Item_Code, obj.Location_Code, obj.ArrSchedule)
            clsTenderSchedule.SaveData(obj.Tender_No, ArrSchedule, trans)
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, obj.Tender_No, "TSPL_TENDER_HEADER", "DocumentCode", "TSPL_TENDER_DETAIL", "DocumentCode", "TSPL_TENDER_SCHEDULE", "DocumentCode", trans)

            qry = "Update TSPL_RAL_NOC set Status=1,Post_Date='" + clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy hh:mm tt") + "',Post_By='" + objCommonVar.CurrentUserCode + "' where Document_No='" + strDocNo + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Private Shared Function DeepCopy(strTendorNo As String, strVendor As String, strItem As String, strLocation As String, arrSchedule As List(Of clsRALNOCSchedule)) As List(Of clsTenderSchedule)
        Dim arrReturn As New List(Of clsTenderSchedule)
        For Each obj As clsRALNOCSchedule In arrSchedule
            Dim objRet As New clsTenderSchedule
            objRet.DocumentCode = strTendorNo
            objRet.SNo = arrReturn.Count + 1
            objRet.PSNo = obj.PSNo
            objRet.Schedule_No = obj.Schedule_No
            objRet.From_Date = obj.From_Date
            objRet.To_Date = obj.To_Date
            objRet.Vendor_Code = strVendor
            objRet.Location_Code = strLocation
            objRet.Item_Code = strItem
            objRet.Schedule_Qty_Per = obj.Schedule_Qty_Per
            objRet.Schedule_Qty = obj.Schedule_Qty
            objRet.Schedule_Short_Per = obj.Schedule_Short_Per
            objRet.Schedule_Short = obj.Schedule_Short
            objRet.Late_Days = obj.Late_Days
            objRet.Arr = New List(Of clsTenderSchedulePenelty)
            For Each objtr As clsRALNOCSchedulePenelty In obj.Arr
                Dim objRetTR As New clsTenderSchedulePenelty
                objRetTR.Penalty_Date = objRetTR.Penalty_Date
                objRetTR.Penalty = objRetTR.Penalty

                objRet.Arr.Add(objRetTR)
            Next
            arrReturn.Add(objRet)
        Next
        Return arrReturn
    End Function

    Public Shared Function DeleteData(ByVal strCode As String) As Boolean

        If (clsCommon.myLen(strCode) <= 0) Then
            Throw New Exception("Document No not found to Delete")
        End If
        Dim obj As clsRALNOC = clsRALNOC.GetData(strCode, NavigatorType.Current, Nothing)
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Dim dts As DataTable = clsDBFuncationality.GetDataTable(" select TSPL_RAL_NOC.Document_No, TSPL_RAL_NOC.Location_Code from TSPL_RAL_NOC where Document_No= '" + obj.Document_No + "' ", trans)
        clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModulePurchase, clsUserMgtCode.TenderShortPenalty, clsCommon.myCstr(dts.Rows(0)("Location_Code")), obj.Document_Date, trans)
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Document_No) > 0) Then
            Try
                'clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "Purchase", "Store receipt Note", obj.Location_Code, obj.Document_Date, trans)
                If (obj.Status = 1) Then
                    Throw New Exception("Already Posted Docuemnt [" + obj.Document_No + "]")
                End If
                Dim qry As String = ""

                'DeleteSRNDeduction(obj.Arr, obj.Item_Code, True, True, True, trans)

                qry = "delete from TSPL_TENDER_PENALTY_DETAIL where Document_No='" + strCode + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)

                qry = "delete from TSPL_RAL_NOC where Document_No='" + strCode + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)

                trans.Commit()
            Catch ex As Exception
                trans.Rollback()
                Throw New Exception(ex.Message)
            End Try
        End If
        Return True
    End Function


    Public Shared Function ReverseAndUnpost(ByVal strCode As String) As Boolean

        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim Qry As String = "select Status from TSPL_RAL_NOC where Document_No='" + strCode + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry, trans)

            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                Throw New Exception("Document No [" + strCode + "] not found for reverse and unpost")
            End If

            If Not clsCommon.myCdbl(dt.Rows(0)("Status")) = 1 Then
                Throw New Exception("Transaction status should be posted for reverse and unpost")
            End If

            Qry = "select TSPL_PI_DETAIL.PI_No,TSPL_TENDER_PENALTY_DETAIL.SRN_No from TSPL_TENDER_PENALTY_DETAIL 
inner join TSPL_PI_DETAIL on TSPL_PI_DETAIL.SRN_Id=TSPL_TENDER_PENALTY_DETAIL.SRN_No  
where TSPL_TENDER_PENALTY_DETAIL.Document_No='" + strCode + "'"
            dt = clsDBFuncationality.GetDataTable(Qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Throw New Exception("Purchase Invoice No [" + clsCommon.myCstr(dt.Rows(0)("PI_No")) + "] is Generated Against SRN No [" + clsCommon.myCstr(dt.Rows(0)("SRN_No")) + "] ")
            End If



            Qry = "select Document_No from TSPL_RAL_NOC 
where exists(select 1 from TSPL_RAL_NOC as TabCurr where TabCurr.Document_No='" + strCode + "' and TabCurr.Location_Code=TSPL_RAL_NOC.Location_Code and TabCurr.Tender_No=TSPL_RAL_NOC.Tender_No and TabCurr.Vendor_Code=TSPL_RAL_NOC.Vendor_Code and TabCurr.Item_Code=TSPL_RAL_NOC.Item_Code and TabCurr.Created_Date< TSPL_RAL_NOC.Created_Date)"
            dt = clsDBFuncationality.GetDataTable(Qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Throw New Exception("Please remove Tender Penalty Docuemnt [" + clsCommon.myCstr(dt.Rows(0)("Document_No")) + "] to unpost it")
            End If

            Qry = "Update TSPL_RAL_NOC set Status = 0 where Document_No='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(Qry, trans)


            'clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, clsCommon.myCstr(strCode), "TSPL_RAL_NOC", "Document_No", trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

End Class

Public Class clsRALNOCSchedule
#Region "Variables"
    Public Document_No As String
    Public SNo As Integer
    Public PSNo As Integer
    Public Schedule_No As Integer
    Public From_Date As Date
    Public To_Date As Date
    Public Schedule_Qty_Per As Decimal
    Public Schedule_Qty As Decimal
    Public Schedule_Short_Per As Decimal
    Public Schedule_Short As Decimal
    Public Late_Days As Integer
    Public Arr As List(Of clsRALNOCSchedulePenelty) = Nothing
#End Region

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsRALNOCSchedule), ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsRALNOCSchedule In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Document_No", strDocNo)
                clsCommon.AddColumnsForChange(coll, "PSNo", obj.PSNo)
                clsCommon.AddColumnsForChange(coll, "Schedule_No", obj.Schedule_No)
                clsCommon.AddColumnsForChange(coll, "From_Date", clsCommon.GetPrintDate(obj.From_Date, "dd/MMM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "To_Date", clsCommon.GetPrintDate(obj.To_Date, "dd/MMM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "Schedule_Qty_Per", obj.Schedule_Qty_Per)
                clsCommon.AddColumnsForChange(coll, "Schedule_Qty", obj.Schedule_Qty)
                clsCommon.AddColumnsForChange(coll, "Schedule_Short_Per", obj.Schedule_Short_Per)
                clsCommon.AddColumnsForChange(coll, "Schedule_Short", obj.Schedule_Short)
                clsCommon.AddColumnsForChange(coll, "Late_Days", obj.Late_Days)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_RAL_NOC_SCHEDULE", OMInsertOrUpdate.Insert, "", trans)

                Dim PK As Integer = clsERPFuncationality.GetScopeIdentityValue(trans)
                clsRALNOCSchedulePenelty.SaveData(strDocNo, PK, obj.Arr, trans)
            Next
        End If
        Return True
    End Function

    Public Shared Function GetData(ByVal strDocNo As String, ByVal trans As SqlTransaction) As List(Of clsRALNOCSchedule)
        Dim arr As List(Of clsRALNOCSchedule) = Nothing
        Dim qry As String = "select TSPL_RAL_NOC_SCHEDULE.* from TSPL_RAL_NOC_SCHEDULE  where TSPL_RAL_NOC_SCHEDULE.Document_No='" + clsCommon.myCstr(strDocNo) + "' order by TSPL_RAL_NOC_SCHEDULE.PK_Id"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            arr = New List(Of clsRALNOCSchedule)()
            For ii As Integer = 0 To dt.Rows.Count - 1
                Dim obj As New clsRALNOCSchedule
                obj.SNo = ii + 1
                obj.Document_No = clsCommon.myCstr(dt.Rows(ii)("Document_No"))
                obj.PSNo = clsCommon.myCDecimal(dt.Rows(ii)("PSNo"))
                obj.Schedule_No = clsCommon.myCDecimal(dt.Rows(ii)("Schedule_No"))
                obj.From_Date = clsCommon.myCDate(dt.Rows(ii)("From_Date"))
                obj.To_Date = clsCommon.myCDate(dt.Rows(ii)("To_Date"))
                obj.Schedule_Qty_Per = clsCommon.myCDecimal(dt.Rows(ii)("Schedule_Qty_Per"))
                obj.Schedule_Qty = clsCommon.myCDecimal(dt.Rows(ii)("Schedule_Qty"))
                obj.Schedule_Short_Per = clsCommon.myCDecimal(dt.Rows(ii)("Schedule_Short_Per"))
                obj.Schedule_Short = clsCommon.myCDecimal(dt.Rows(ii)("Schedule_Short"))
                obj.Late_Days = clsCommon.myCDecimal(dt.Rows(ii)("Late_Days"))
                obj.Arr = clsRALNOCSchedulePenelty.GetData(clsCommon.myCDecimal(dt.Rows(ii)("PK_Id")), False, trans)
                arr.Add(obj)
            Next
        End If
        Return arr
    End Function
End Class

Public Class clsRALNOCSchedulePenelty
#Region "Variables"
    Public PK_Id As Integer
    Public Document_No As String
    Public Against_RAL_NOC_Schedule_PK_Id As Integer
    Public Penalty_Date As Date
    Public Penalty As Decimal
#End Region

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal AgainstSchedulePKId As Integer, ByVal Arr As List(Of clsRALNOCSchedulePenelty), ByVal trans As SqlTransaction) As Boolean
        For Each obj As clsRALNOCSchedulePenelty In Arr
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Document_No", strDocNo)
            clsCommon.AddColumnsForChange(coll, "Against_RAL_NOC_Schedule_PK_Id", AgainstSchedulePKId)
            clsCommon.AddColumnsForChange(coll, "Penalty_Date", clsCommon.GetPrintDate(obj.Penalty_Date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Penalty", obj.Penalty)
            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_RAL_NOC_SCHEDULE_PENALTY", OMInsertOrUpdate.Insert, "", trans)
        Next
        Return True
    End Function

    Public Shared Function GetData(ByVal AgainstSchedulePKId As Integer, ByVal AddExtensionDays As Boolean, ByVal trans As SqlTransaction) As List(Of clsRALNOCSchedulePenelty)
        Dim arr As List(Of clsRALNOCSchedulePenelty) = Nothing
        Dim qry As String = "select TSPL_RAL_NOC_SCHEDULE_PENALTY.Document_No,TSPL_RAL_NOC_SCHEDULE_PENALTY.PK_Id,TSPL_RAL_NOC_SCHEDULE_PENALTY.Against_RAL_NOC_Schedule_PK_Id "
        If AddExtensionDays = True Then
            qry += " ,DATEADD(day,isnull(TSPL_RAL_NOC_SCHEDULE.Extension_Days,0),TSPL_RAL_NOC_SCHEDULE_PENALTY.Penalty_Date) "
        Else
            qry += " ,TSPL_RAL_NOC_SCHEDULE_PENALTY.Penalty_Date "
        End If
        qry += " AS Penalty_Date ,TSPL_RAL_NOC_SCHEDULE_PENALTY.Penalty
         from TSPL_RAL_NOC_SCHEDULE_PENALTY
         left outer join TSPL_RAL_NOC_SCHEDULE on TSPL_RAL_NOC_SCHEDULE.Document_No=TSPL_RAL_NOC_SCHEDULE_PENALTY.Document_No
         and TSPL_RAL_NOC_SCHEDULE.PK_ID=TSPL_RAL_NOC_SCHEDULE_PENALTY.Against_RAL_NOC_Schedule_PK_Id
         where TSPL_RAL_NOC_SCHEDULE_PENALTY.Against_RAL_NOC_Schedule_PK_Id='" + clsCommon.myCstr(AgainstSchedulePKId) + "' order by TSPL_RAL_NOC_SCHEDULE_PENALTY.PK_Id"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            arr = New List(Of clsRALNOCSchedulePenelty)()
            For ii As Integer = 0 To dt.Rows.Count - 1
                Dim obj As New clsRALNOCSchedulePenelty
                obj.PK_Id = clsCommon.myCDecimal(dt.Rows(ii)("PK_Id"))
                obj.Document_No = clsCommon.myCstr(dt.Rows(ii)("Document_No"))
                obj.Against_RAL_NOC_Schedule_PK_Id = clsCommon.myCDecimal(dt.Rows(ii)("Against_RAL_NOC_Schedule_PK_Id"))
                obj.Penalty_Date = clsCommon.myCDate(dt.Rows(ii)("Penalty_Date"))
                obj.Penalty = clsCommon.myCDecimal(dt.Rows(ii)("Penalty"))
                arr.Add(obj)
            Next
        End If
        Return arr
    End Function
End Class















Public Class clsTenderDetail
#Region "Variables"
    Public DocumentCode As String = Nothing
    Public Line_No As Integer = 0
    Public Item_Code As String = Nothing
    Public Item_Name As String = Nothing
    Public Item_Type As String = Nothing
    Public Item_Type_Name As String = Nothing
    Public Vendor_Code As String = Nothing
    Public Unit_code As String = Nothing
    Public Location As String = Nothing
    Public Vendor_Name As String = Nothing
    Public Location_Name As String = Nothing
    Public Qty As Double = 0
    Public Discount As Double = 0

    Public Rate As Double = 0
    Public Tax_Exclusive As Boolean = False
    Public Item_Cost As Double = 0
    Public Remarks As String = Nothing
    Public Comments As String = Nothing

#End Region

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsTenderDetail), ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsTenderDetail In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "DocumentCode", strDocNo)
                clsCommon.AddColumnsForChange(coll, "Line_No", obj.Line_No)
                clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code, True)
                clsCommon.AddColumnsForChange(coll, "Item_Type", obj.Item_Type, True)
                clsCommon.AddColumnsForChange(coll, "Vendor_Code", obj.Vendor_Code)
                clsCommon.AddColumnsForChange(coll, "Discount", obj.Discount)
                clsCommon.AddColumnsForChange(coll, "Qty", obj.Qty)
                clsCommon.AddColumnsForChange(coll, "Unit_code", obj.Unit_code)
                clsCommon.AddColumnsForChange(coll, "Location", obj.Location)
                clsCommon.AddColumnsForChange(coll, "Rate", obj.Rate)
                clsCommon.AddColumnsForChange(coll, "Tax_Exclusive", IIf(obj.Tax_Exclusive, 1, 0))
                clsCommon.AddColumnsForChange(coll, "Item_Cost", obj.Item_Cost)
                clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks, True)
                clsCommon.AddColumnsForChange(coll, "Comments", obj.Comments, True)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_TENDER_DETAIL", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function

    Public Shared Function GetFinder(ByVal strTenderNo As String, ByVal strVendorCode As String, ByVal strLocation As String) As clsTenderDetail
        Dim obj As clsTenderDetail = Nothing
        Dim qry As String = " select TSPL_TENDER_DETAIL.Item_Code,TSPL_ITEM_MASTER.Item_Desc,TSPL_TENDER_DETAIL.Item_Type,TSPL_ITEM_TYPE_MASTER.ITEM_TYPE_NAME,TSPL_TENDER_DETAIL.Unit_code,TSPL_TENDER_DETAIL.Rate,TSPL_TENDER_DETAIL.Discount,TSPL_TENDER_DETAIL.Location,TSPL_TENDER_DETAIL.Tax_Exclusive from TSPL_TENDER_DETAIL
left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.ITEM_CODE=TSPL_TENDER_DETAIL.Item_Code
left outer join TSPL_ITEM_TYPE_MASTER on TSPL_ITEM_TYPE_MASTER.ITEM_TYPE_CODE=TSPL_TENDER_DETAIL.Item_Type
 where DocumentCode='" + strTenderNo + "' and Vendor_Code='" + strVendorCode + "'and Location='" + strLocation + "'"
        Dim dr As DataRow = clsCommon.ShowSelectFormForRow("TenVedItm", qry)
        If dr IsNot Nothing Then
            obj = New clsTenderDetail()
            obj.Item_Code = clsCommon.myCstr(dr("Item_Code"))
            obj.Item_Name = clsCommon.myCstr(dr("Item_Desc"))
            obj.Item_Type = clsCommon.myCstr(dr("Item_Type"))
            obj.Item_Type_Name = clsCommon.myCstr(dr("ITEM_TYPE_NAME"))
            obj.Unit_code = clsCommon.myCstr(dr("Unit_code"))
            obj.Rate = clsCommon.myCdbl(dr("Rate"))
            obj.Tax_Exclusive = (clsCommon.myCdbl(dr("Tax_Exclusive")) = 1)
            obj.Discount = clsCommon.myCdbl(dr("Discount"))
            obj.Location = clsCommon.myCdbl(dr("Location"))
        End If
        Return obj
    End Function

    Public Shared Function GetItemTypeFinder(ByVal strTenderNo As String, ByVal strVendorCode As String, ByVal strLocation As String) As clsTenderDetail
        Dim obj As clsTenderDetail = Nothing
        Dim qry As String = " select TSPL_ITEM_MASTER.Item_Code,TSPL_ITEM_MASTER.Item_Desc,TSPL_TENDER_DETAIL.Item_Type,TSPL_ITEM_TYPE_MASTER.ITEM_TYPE_NAME,TSPL_TENDER_DETAIL.Unit_code,TSPL_TENDER_DETAIL.Rate,TSPL_TENDER_DETAIL.Discount,TSPL_TENDER_DETAIL.Location,TSPL_TENDER_DETAIL.Tax_Exclusive 
from TSPL_TENDER_DETAIL
left outer join TSPL_ITEM_MASTER on  TSPL_ITEM_MASTER.Item_Type=TSPL_TENDER_DETAIL.Item_Type
left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_ITEM_MASTER.Item_Code
left outer join TSPL_ITEM_TYPE_MASTER on TSPL_ITEM_TYPE_MASTER.ITEM_TYPE_CODE=TSPL_TENDER_DETAIL.Item_Type and TSPL_ITEM_MASTER.Item_Code is not null
 where DocumentCode='" + strTenderNo + "' and Vendor_Code='" + strVendorCode + "'and Location='" + strLocation + "'  and TSPL_ITEM_UOM_DETAIL.UOM_Code=TSPL_TENDER_DETAIL.Unit_code"
        Dim dr As DataRow = clsCommon.ShowSelectFormForRow("TenVedItm", qry)
        If dr IsNot Nothing Then
            obj = New clsTenderDetail()
            obj.Item_Code = clsCommon.myCstr(dr("Item_Code"))
            obj.Item_Name = clsCommon.myCstr(dr("Item_Desc"))
            obj.Item_Type = clsCommon.myCstr(dr("Item_Type"))
            obj.Item_Type_Name = clsCommon.myCstr(dr("ITEM_TYPE_NAME"))
            obj.Unit_code = clsCommon.myCstr(dr("Unit_code"))
            obj.Rate = clsCommon.myCdbl(dr("Rate"))
            obj.Tax_Exclusive = (clsCommon.myCdbl(dr("Tax_Exclusive")) = 1)
            obj.Discount = clsCommon.myCdbl(dr("Discount"))
            obj.Location = clsCommon.myCdbl(dr("Location"))
        End If
        Return obj
    End Function
End Class

Public Class clsTenderSchedule
#Region "Variables"
    Public DocumentCode As String
    Public SNo As Integer
    Public PSNo As Integer
    Public Schedule_No As Integer
    Public From_Date As Date
    Public To_Date As Date
    Public Vendor_Code As String
    Public Location_Code As String
    Public Item_Code As String
    Public Item_Type As String
    Public Schedule_Qty_Per As Decimal
    Public Schedule_Qty As Decimal
    Public Schedule_Short_Per As Decimal
    Public Schedule_Short As Decimal
    Public Late_Days As Integer
    Public Extension_Days As Integer
    Public Item_Name As String = Nothing
    Public Vendor_Name As String = Nothing
    Public Location_Name As String = Nothing
    Public Arr As List(Of clsTenderSchedulePenelty) = Nothing
#End Region

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsTenderSchedule), ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsTenderSchedule In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "DocumentCode", strDocNo)
                clsCommon.AddColumnsForChange(coll, "PSNo", obj.PSNo)
                clsCommon.AddColumnsForChange(coll, "Schedule_No", obj.Schedule_No)
                clsCommon.AddColumnsForChange(coll, "From_Date", clsCommon.GetPrintDate(obj.From_Date, "dd/MMM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "To_Date", clsCommon.GetPrintDate(obj.To_Date, "dd/MMM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "Vendor_Code", obj.Vendor_Code)
                clsCommon.AddColumnsForChange(coll, "Location_Code", obj.Location_Code)
                clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code, True)
                clsCommon.AddColumnsForChange(coll, "Item_Type", obj.Item_Type, True)
                clsCommon.AddColumnsForChange(coll, "Schedule_Qty_Per", obj.Schedule_Qty_Per)
                clsCommon.AddColumnsForChange(coll, "Schedule_Qty", obj.Schedule_Qty)
                clsCommon.AddColumnsForChange(coll, "Schedule_Short_Per", obj.Schedule_Short_Per)
                clsCommon.AddColumnsForChange(coll, "Schedule_Short", obj.Schedule_Short)
                clsCommon.AddColumnsForChange(coll, "Late_Days", obj.Late_Days)
                clsCommon.AddColumnsForChange(coll, "Extension_Days", obj.Extension_Days)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_TENDER_SCHEDULE", OMInsertOrUpdate.Insert, "", trans)

                Dim PK As Integer = clsCommon.myCDecimal(clsDBFuncationality.getSingleValue("select SCOPE_IDENTITY()", trans))
                clsTenderSchedulePenelty.SaveData(strDocNo, PK, obj.Arr, trans)
            Next
        End If
        Return True
    End Function

    Public Shared Function GetData(ByVal strDocNo As String, ByVal trans As SqlTransaction) As List(Of clsTenderSchedule)
        Dim arr As List(Of clsTenderSchedule) = Nothing
        Dim qry As String = "select TSPL_TENDER_SCHEDULE.* from TSPL_TENDER_SCHEDULE  where TSPL_TENDER_SCHEDULE.DocumentCode='" + clsCommon.myCstr(strDocNo) + "' order by TSPL_TENDER_SCHEDULE.PK_Id"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            arr = New List(Of clsTenderSchedule)()
            For ii As Integer = 0 To dt.Rows.Count - 1
                Dim obj As New clsTenderSchedule
                obj.SNo = ii + 1
                obj.DocumentCode = clsCommon.myCstr(dt.Rows(ii)("DocumentCode"))
                obj.PSNo = clsCommon.myCDecimal(dt.Rows(ii)("PSNo"))
                obj.Schedule_No = clsCommon.myCDecimal(dt.Rows(ii)("Schedule_No"))
                obj.From_Date = clsCommon.myCDate(dt.Rows(ii)("From_Date"))
                obj.To_Date = clsCommon.myCDate(dt.Rows(ii)("To_Date"))

                obj.Vendor_Code = clsCommon.myCstr(dt.Rows(ii)("Vendor_Code"))
                obj.Location_Code = clsCommon.myCstr(dt.Rows(ii)("Location_Code"))
                obj.Item_Code = clsCommon.myCstr(dt.Rows(ii)("Item_Code"))
                obj.Item_Type = clsCommon.myCstr(dt.Rows(ii)("Item_Type"))
                obj.Schedule_Qty_Per = clsCommon.myCDecimal(dt.Rows(ii)("Schedule_Qty_Per"))
                obj.Schedule_Qty = clsCommon.myCDecimal(dt.Rows(ii)("Schedule_Qty"))
                obj.Schedule_Short_Per = clsCommon.myCDecimal(dt.Rows(ii)("Schedule_Short_Per"))
                obj.Schedule_Short = clsCommon.myCDecimal(dt.Rows(ii)("Schedule_Short"))
                obj.Late_Days = clsCommon.myCDecimal(dt.Rows(ii)("Late_Days"))
                obj.Extension_Days = clsCommon.myCDecimal(dt.Rows(ii)("Extension_Days"))
                obj.Arr = clsTenderSchedulePenelty.GetData(clsCommon.myCDecimal(dt.Rows(ii)("PK_Id")), False, trans)
                arr.Add(obj)
            Next
        End If
        Return arr
    End Function
End Class

Public Class clsTenderSchedulePenelty
#Region "Variables"
    Public PK_Id As Integer
    Public DocumentCode As String
    Public Against_Tender_Schedule_PK_Id As Integer
    Public Penalty_Date As Date
    Public Penalty As Decimal
#End Region

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal AgainstSchedulePKId As Integer, ByVal Arr As List(Of clsTenderSchedulePenelty), ByVal trans As SqlTransaction) As Boolean
        For Each obj As clsTenderSchedulePenelty In Arr
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "DocumentCode", strDocNo)
            clsCommon.AddColumnsForChange(coll, "Against_Tender_Schedule_PK_Id", AgainstSchedulePKId)
            clsCommon.AddColumnsForChange(coll, "Penalty_Date", clsCommon.GetPrintDate(obj.Penalty_Date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Penalty", obj.Penalty)
            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_TENDER_SCHEDULE_PENALTY", OMInsertOrUpdate.Insert, "", trans)
        Next
        Return True
    End Function

    Public Shared Function GetData(ByVal AgainstSchedulePKId As Integer, ByVal AddExtensionDays As Boolean, ByVal trans As SqlTransaction) As List(Of clsTenderSchedulePenelty)
        Dim arr As List(Of clsTenderSchedulePenelty) = Nothing
        Dim qry As String = "select TSPL_TENDER_SCHEDULE_PENALTY.DocumentCode,TSPL_TENDER_SCHEDULE_PENALTY.PK_Id,TSPL_TENDER_SCHEDULE_PENALTY.Against_Tender_Schedule_PK_Id "
        If AddExtensionDays = True Then
            qry += " ,DATEADD(day,isnull(TSPL_TENDER_SCHEDULE.Extension_Days,0),TSPL_TENDER_SCHEDULE_PENALTY.Penalty_Date) "
        Else
            qry += " ,TSPL_TENDER_SCHEDULE_PENALTY.Penalty_Date "
        End If
        qry += " AS Penalty_Date ,TSPL_TENDER_SCHEDULE_PENALTY.Penalty
         from TSPL_TENDER_SCHEDULE_PENALTY
         left outer join TSPL_TENDER_SCHEDULE on TSPL_TENDER_SCHEDULE.DocumentCode=TSPL_TENDER_SCHEDULE_PENALTY.DocumentCode
         and TSPL_TENDER_SCHEDULE.PK_ID=TSPL_TENDER_SCHEDULE_PENALTY.Against_Tender_Schedule_PK_Id
         where TSPL_TENDER_SCHEDULE_PENALTY.Against_Tender_Schedule_PK_Id='" + clsCommon.myCstr(AgainstSchedulePKId) + "' order by TSPL_TENDER_SCHEDULE_PENALTY.PK_Id"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            arr = New List(Of clsTenderSchedulePenelty)()
            For ii As Integer = 0 To dt.Rows.Count - 1
                Dim obj As New clsTenderSchedulePenelty
                obj.PK_Id = clsCommon.myCDecimal(dt.Rows(ii)("PK_Id"))
                obj.DocumentCode = clsCommon.myCstr(dt.Rows(ii)("DocumentCode"))
                obj.Against_Tender_Schedule_PK_Id = clsCommon.myCDecimal(dt.Rows(ii)("Against_Tender_Schedule_PK_Id"))
                obj.Penalty_Date = clsCommon.myCDate(dt.Rows(ii)("Penalty_Date"))
                obj.Penalty = clsCommon.myCDecimal(dt.Rows(ii)("Penalty"))
                arr.Add(obj)
            Next
        End If
        Return arr
    End Function
End Class