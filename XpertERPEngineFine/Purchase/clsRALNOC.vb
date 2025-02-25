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
            Dim qry As String = "delete from TSPL_RAL_NOC_SCHEDULE_PENALTY where Document_No='" + obj.Document_No + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_RAL_NOC_SCHEDULE where Document_No='" + obj.Document_No + "'"
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

            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, clsCommon.myCstr(obj.Document_No), "TSPL_RAL_NOC", "Document_No", "TSPL_RAL_NOC_SCHEDULE", "Document_No", trans)
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

    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim obj As clsRALNOC = clsRALNOC.GetData(strCode, NavigatorType.Current, trans)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.Document_No) <= 0) Then
                Throw New Exception("Document No not found to Delete")
            End If
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModulePurchase, clsUserMgtCode.RALNOC, obj.Location_Code, obj.Document_Date, trans)
            clsCommonFunctionality.SaveDeletedData(objCommonVar.CurrentUserCode, strCode, "TSPL_RAL_NOC", "Document_No", "TSPL_RAL_NOC_SCHEDULE", "Document_No", trans)
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strCode, "TSPL_RAL_NOC", "Document_No", "TSPL_RAL_NOC_SCHEDULE", "Document_No", trans)

            If (obj.Status = ERPTransactionStatus.Approved) Then
                Throw New Exception("Already Posted Docuemnt [" + obj.Document_No + "]")
            End If
            Dim qry As String = "delete from TSPL_RAL_NOC_SCHEDULE_PENALTY where Document_No='" + obj.Document_No + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_RAL_NOC_SCHEDULE where Document_No='" + obj.Document_No + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_RAL_NOC where Document_No='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function


    Public Shared Function ReverseAndUnpost(ByVal strCode As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim obj As clsRALNOC = clsRALNOC.GetData(strCode, NavigatorType.Current, trans)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.Document_No) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            If Not obj.Status = ERPTransactionStatus.Approved Then
                Throw New Exception("Transaction status should be posted for reverse and unpost")
            End If

            Dim Qry As String = "delete from TSPL_TENDER_SCHEDULE_PENALTY where Against_Tender_Schedule_PK_Id in ( select PK_Id from TSPL_TENDER_SCHEDULE where DocumentCode='" + obj.Tender_No + "' and Vendor_Code='" + obj.Vendor_Code + "' and Location_Code='" + obj.Location_Code + "' and Item_Code='" + obj.Item_Code + "')"
            clsDBFuncationality.ExecuteNonQuery(Qry, trans)

            Qry = "delete  from TSPL_TENDER_SCHEDULE where DocumentCode='" + obj.Tender_No + "' and Vendor_Code='" + obj.Vendor_Code + "' and Location_Code='" + obj.Location_Code + "' and Item_Code='" + obj.Item_Code + "'"
            clsDBFuncationality.ExecuteNonQuery(Qry, trans)

            Dim ArrSchedule As New List(Of clsTenderSchedule)
            Qry = "select * from TSPL_RAL_NOC_ORG_SCHEDULE where Document_No='" + obj.Document_No + "'"
            Dim dtOrg As DataTable = clsDBFuncationality.GetDataTable(Qry, trans)
            If dtOrg IsNot Nothing AndAlso dtOrg.Rows.Count > 0 Then
                For Each drOrg As DataRow In dtOrg.Rows
                    Dim objRet As New clsTenderSchedule
                    objRet.DocumentCode = obj.Tender_No
                    objRet.PSNo = clsCommon.myCDecimal(drOrg("PSNo"))
                    objRet.Schedule_No = clsCommon.myCDecimal(drOrg("Schedule_No"))
                    objRet.From_Date = clsCommon.myCDate(drOrg("From_Date"))
                    objRet.To_Date = clsCommon.myCDate(drOrg("To_Date"))
                    objRet.Vendor_Code = obj.Vendor_Code
                    objRet.Location_Code = obj.Location_Code
                    objRet.Item_Code = obj.Item_Code
                    objRet.Schedule_Qty_Per = clsCommon.myCDecimal(drOrg("Schedule_Qty_Per"))
                    objRet.Schedule_Qty = clsCommon.myCDecimal(drOrg("Schedule_Qty"))
                    objRet.Schedule_Short_Per = clsCommon.myCDecimal(drOrg("Schedule_Short_Per"))
                    objRet.Schedule_Short = clsCommon.myCDecimal(drOrg("Schedule_Short"))
                    objRet.Late_Days = clsCommon.myCDecimal(drOrg("Late_Days"))
                    objRet.Extension_Days = clsCommon.myCDecimal(drOrg("Extension_Days"))
                    objRet.Item_Type = clsCommon.myCstr(drOrg("Item_Type"))
                    objRet.Arr = New List(Of clsTenderSchedulePenelty)
                    Qry = "select Penalty,Penalty_Date from TSPL_RAL_NOC_ORG_SCHEDULE_PENALTY where Against_Tender_Schedule_PK_Id=" + clsCommon.myCstr(drOrg("PK_Id")) + ""
                    Dim dtTR As DataTable = clsDBFuncationality.GetDataTable(Qry, trans)
                    If dtTR IsNot Nothing AndAlso dtTR.Rows.Count > 0 Then
                        For Each drTr As DataRow In dtTR.Rows
                            Dim objRetTr As New clsTenderSchedulePenelty
                            objRetTr.Penalty = clsCommon.myCDecimal(drTr("Penalty"))
                            objRetTr.Penalty_Date = clsCommon.myCDate(drTr("Penalty_Date"))
                            objRet.Arr.Add(objRetTr)
                        Next
                    End If
                    ArrSchedule.Add(objRet)
                Next
            End If
            clsTenderSchedule.SaveData(obj.Tender_No, ArrSchedule, trans)
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, obj.Tender_No, "TSPL_TENDER_HEADER", "DocumentCode", "TSPL_TENDER_DETAIL", "DocumentCode", "TSPL_TENDER_SCHEDULE", "DocumentCode", trans)

            Qry = "delete from TSPL_RAL_NOC_ORG_SCHEDULE_PENALTY where Against_Tender_Schedule_PK_Id in ( select PK_Id from TSPL_RAL_NOC_ORG_SCHEDULE where Document_No='" + obj.Document_No + "' )"
            clsDBFuncationality.ExecuteNonQuery(Qry, trans)

            Qry = "delete from TSPL_RAL_NOC_ORG_SCHEDULE where Document_No='" + obj.Document_No + "' "
            clsDBFuncationality.ExecuteNonQuery(Qry, trans)

            Qry = "Update TSPL_RAL_NOC set Status = 0 where Document_No='" + obj.Document_No + "'"
            clsDBFuncationality.ExecuteNonQuery(Qry, trans)

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
                objRetTR.Penalty_Date = objtr.Penalty_Date
                objRetTR.Penalty = objtr.Penalty

                objRet.Arr.Add(objRetTR)
            Next
            arrReturn.Add(objRet)
        Next
        Return arrReturn
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
                obj.Arr = clsRALNOCSchedulePenelty.GetData(clsCommon.myCDecimal(dt.Rows(ii)("PK_Id")), trans)
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

    Public Shared Function GetData(ByVal AgainstSchedulePKId As Integer, ByVal trans As SqlTransaction) As List(Of clsRALNOCSchedulePenelty)
        Dim arr As List(Of clsRALNOCSchedulePenelty) = Nothing
        Dim qry As String = "select TSPL_RAL_NOC_SCHEDULE_PENALTY.*
from TSPL_RAL_NOC_SCHEDULE_PENALTY
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















