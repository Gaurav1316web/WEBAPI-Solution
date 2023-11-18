Imports common
Imports System.Data.SqlClient

Public Class clsMilkCollectionMCC
#Region "Variables"
    Public Document_No As String
    Public Document_Date As DateTime
    Public Late As Integer
    Public Route_Code As String
    Public Route_Name As String ''Not a table Column
    Public Tanker_No As String
    Public Vehicle_No As String
    Public Trip_No As Integer
    Public Entered_Qty As Decimal
    Public Entered_FATKg As Decimal
    Public Entered_SNFKg As Decimal
    Public Slip_No As String
    Public Status As ERPTransactionStatus = ERPTransactionStatus.Pending
    Public Posting_Date As DateTime? = Nothing
    Public FAT_SNF_Type As Integer
    Public Against_DCS_Multiple_Days As String
    Public Age As Decimal
    Public ALCOB As String
    Public txtDate As String

    Public Description As String
    Public Temp As Decimal
    Public Acidity As Decimal

    Public ORG As String
    Public Arr As List(Of clsMilkCollectionMCCDetail) = Nothing




#End Region

    Public Function SaveData(ByVal obj As clsMilkCollectionMCC, ByVal isNewEntry As Boolean) As Boolean
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
    Public Function SaveData(ByVal obj As clsMilkCollectionMCC, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Try
            'clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleMCCMilkProcurement, clsUserMgtCode.MilkShiftUploader, obj.MCC_Code, obj.Document_Date, trans)
            'clsMCCPaymentCycleLockForScheduler.CheckForSchedulerLock(obj.MCC_Code, obj.Document_Date, trans)
            If isNewEntry = False Then
                If clsCommon.myCDecimal(clsDBFuncationality.getSingleValue("select isnull(Status,0) as Status from  TSPL_MILK_COLLECTION_MCC where Document_No ='" + obj.Document_No + "' ", trans)) = 1 Then
                    Throw New Exception("Posted Document [" + obj.Document_No + "]")
                End If

                HistoryUpdate(obj.Document_No, trans)
            End If
            Dim qry As String = "delete from TSPL_MILK_COLLECTION_MCC_DETAIL where Document_No='" + obj.Document_No + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Dim coll As New Hashtable()
            'clsCommon.AddColumnsForChange(coll, "REF_PK_ID", obj.REF_PK_ID)
            clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Late", obj.Late)
            clsCommon.AddColumnsForChange(coll, "Route_Code", obj.Route_Code, True)
            clsCommon.AddColumnsForChange(coll, "Tanker_No", obj.Tanker_No)
            clsCommon.AddColumnsForChange(coll, "Vehicle_No", obj.Vehicle_No)
            clsCommon.AddColumnsForChange(coll, "Trip_No", obj.Trip_No)
            clsCommon.AddColumnsForChange(coll, "Entered_Qty", obj.Entered_Qty)
            clsCommon.AddColumnsForChange(coll, "Entered_FATKg", obj.Entered_FATKg)
            clsCommon.AddColumnsForChange(coll, "Entered_SNFKg", obj.Entered_SNFKg)
            clsCommon.AddColumnsForChange(coll, "Description", obj.Description)
            clsCommon.AddColumnsForChange(coll, "FAT_SNF_Type", obj.FAT_SNF_Type)
            clsCommon.AddColumnsForChange(coll, "Slip_No", obj.Slip_No)
            clsCommon.AddColumnsForChange(coll, "Temp", obj.Temp)
            clsCommon.AddColumnsForChange(coll, "Age", obj.Age)
            clsCommon.AddColumnsForChange(coll, "ALCOB", obj.ALCOB)
            clsCommon.AddColumnsForChange(coll, "ORG", obj.ORG)
            clsCommon.AddColumnsForChange(coll, "Acidity", obj.Acidity)
            clsCommon.AddColumnsForChange(coll, "Against_DCS_Multiple_Days", obj.Against_DCS_Multiple_Days, True)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))
            If isNewEntry Then
                obj.Document_No = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.MilkCollectionMCC, "", "")
                If (clsCommon.myLen(obj.Document_No) <= 0) Then
                    Throw New Exception("Error in Document Code Generation")
                End If
                clsCommon.AddColumnsForChange(coll, "Document_No", obj.Document_No)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MILK_COLLECTION_MCC", OMInsertOrUpdate.Insert, "", trans)
            Else
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MILK_COLLECTION_MCC", OMInsertOrUpdate.Update, "TSPL_MILK_COLLECTION_MCC.Document_No='" + obj.Document_No + "'", trans)
            End If
            clsMilkCollectionMCCDetail.SaveData(obj.Document_No, obj.Document_Date, obj.Arr, False, trans)
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function
    Public Shared Function HistoryUpdate(ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, clsCommon.myCstr(strCode), "TSPL_MILK_COLLECTION_MCC", "Document_No", "TSPL_MILK_COLLECTION_MCC_DETAIL", "Document_No", trans)
        Return True
    End Function
    Public Shared Function GetData(ByVal strPONo As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsMilkCollectionMCC
        Return GetData(strPONo, NavType, trans, "")
    End Function
    Public Shared Function GetData(ByVal strPONo As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction, ByVal strDetailWhrlCls As String) As clsMilkCollectionMCC
        Dim obj As clsMilkCollectionMCC = Nothing
        Dim qry As String = "SELECT TSPL_MILK_COLLECTION_MCC.*,TSPL_BULK_ROUTE_MASTER.ROUTE_NAME as Route_Name  
FROM TSPL_MILK_COLLECTION_MCC 
left outer join TSPL_BULK_ROUTE_MASTER on TSPL_BULK_ROUTE_MASTER.ROUTE_NO=TSPL_MILK_COLLECTION_MCC.Route_Code 
where 2=2"
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_MILK_COLLECTION_MCC.Document_No = (select MIN(Document_No) from TSPL_MILK_COLLECTION_MCC where 1=1  )"
            Case NavigatorType.Last
                qry += " and TSPL_MILK_COLLECTION_MCC.Document_No = (select Max(Document_No) from TSPL_MILK_COLLECTION_MCC where 1=1  )"
            Case NavigatorType.Next
                qry += " and TSPL_MILK_COLLECTION_MCC.Document_No = (select Min(Document_No) from TSPL_MILK_COLLECTION_MCC where Document_No>'" + strPONo + "'  )"
            Case NavigatorType.Previous
                qry += " and TSPL_MILK_COLLECTION_MCC.Document_No = (select Max(Document_No) from TSPL_MILK_COLLECTION_MCC where Document_No<'" + strPONo + "'  )"
            Case NavigatorType.Current
                qry += " and TSPL_MILK_COLLECTION_MCC.Document_No = '" + strPONo + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsMilkCollectionMCC()
            obj.Document_No = clsCommon.myCstr(dt.Rows(0)("Document_No"))
            obj.Document_Date = clsCommon.myCDate(dt.Rows(0)("Document_Date"))
            obj.Late = clsCommon.myCDecimal(dt.Rows(0)("Late"))
            obj.Route_Code = clsCommon.myCstr(dt.Rows(0)("Route_Code"))
            obj.Route_Name = clsCommon.myCstr(dt.Rows(0)("Route_Name"))
            obj.Trip_No = clsCommon.myCDecimal(dt.Rows(0)("Trip_No"))
            obj.Tanker_No = clsCommon.myCstr(dt.Rows(0)("Tanker_No"))
            obj.Vehicle_No = clsCommon.myCstr(dt.Rows(0)("Vehicle_No"))
            obj.Entered_Qty = clsCommon.myCDecimal(dt.Rows(0)("Entered_Qty"))
            obj.Entered_FATKg = clsCommon.myCDecimal(dt.Rows(0)("Entered_FATKg"))
            obj.Entered_SNFKg = clsCommon.myCDecimal(dt.Rows(0)("Entered_SNFKg"))
            obj.Description = clsCommon.myCstr(dt.Rows(0)("Description"))
            obj.Slip_No = clsCommon.myCstr(dt.Rows(0)("Slip_No"))
            obj.Temp = clsCommon.myCDecimal(dt.Rows(0)("Temp"))
            obj.Age = clsCommon.myCDecimal(dt.Rows(0)("Age"))
            obj.ALCOB = clsCommon.myCstr(dt.Rows(0)("ALCOB"))
            obj.ORG = clsCommon.myCstr(dt.Rows(0)("ORG"))
            obj.Acidity = clsCommon.myCDecimal(dt.Rows(0)("Acidity"))
            obj.Against_DCS_Multiple_Days = clsCommon.myCstr(dt.Rows(0)("Against_DCS_Multiple_Days"))
            obj.Status = IIf(clsCommon.myCDecimal(dt.Rows(0)("Status")) = 1, ERPTransactionStatus.Approved, ERPTransactionStatus.Pending)
            obj.FAT_SNF_Type = clsCommon.myCDecimal(dt.Rows(0)("FAT_SNF_Type"))
            If dt.Rows(0)("Posted_Date") IsNot DBNull.Value Then
                obj.Posting_Date = clsCommon.myCDate(dt.Rows(0)("Posted_Date"))
            End If
            'qry = "select sum(Qty) as Qty,cast(sum(Qty*FAT/100) as decimal(18,3)) as FAT,cast(sum(Qty*SNF/100) as decimal(18,3)) as SNF from TSPL_MILK_COLLECTION_MCC_DETAIL where Document_No='" + obj.Document_No + "'"
            'Dim dtRaj As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            'If dtRaj IsNot Nothing AndAlso dtRaj.Rows.Count > 0 Then
            '    obj.Received_Qty = clsCommon.myCDecimal(dtRaj.Rows(0)("Qty"))
            '    obj.Received_FATKg = clsCommon.myCDecimal(dtRaj.Rows(0)("FAT"))
            '    obj.Received_SNFKg = clsCommon.myCDecimal(dtRaj.Rows(0)("SNF"))
            'End If
            obj.Arr = clsMilkCollectionMCCDetail.GetData(obj.Document_No, strDetailWhrlCls, trans)
        End If
        Return obj
    End Function

    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        If (clsCommon.myLen(strCode) <= 0) Then
            Throw New Exception("Document No not found to Delete")
        End If

        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            'Dim dt As DataTable = clsDBFuncationality.GetDataTable("select TSPL_MILK_COLLECTION_MCC.Document_Date,TSPL_MILK_COLLECTION_MCC.MCC_Code from TSPL_MILK_COLLECTION_MCC where Document_No='" + strCode + "'", trans)
            'If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            '    clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleMCCMilkProcurement, clsUserMgtCode.MilkShiftUploader, clsCommon.myCstr(dt.Rows(0)("MCC_Code")), clsCommon.myCDate(dt.Rows(0)("Document_Date")), trans)
            'End If

            Dim obj As clsMilkCollectionMCC = clsMilkCollectionMCC.GetData(strCode, NavigatorType.Current, trans)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.Document_No) <= 0) Then
                Throw New Exception("Document No: " + strCode + " not found to Delete")
            End If

            If (obj.Status = ERPTransactionStatus.Approved OrElse obj.Status = ERPTransactionStatus.Posted) Then
                Throw New Exception("Already Posted on :" + obj.Posting_Date)
            End If
            HistoryUpdate(strCode, trans)
            clsDBFuncationality.ExecuteNonQuery("delete from TSPL_MILK_COLLECTION_MCC_DETAIL where Document_No='" + strCode + "'", trans)
            clsDBFuncationality.ExecuteNonQuery("delete from TSPL_MILK_COLLECTION_MCC where Document_No='" + strCode + "'", trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function PostData(ByVal strCode As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            PostData(strCode, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function PostData(ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        Try
            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Document No not found to Post")
            End If
            Dim obj As clsMilkCollectionMCC = clsMilkCollectionMCC.GetData(strCode, NavigatorType.Current, trans)
            'clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleMCCMilkProcurement, clsUserMgtCode.MilkShiftUploader, obj.MCC_Code, obj.Document_Date, trans)

            If (obj Is Nothing OrElse clsCommon.myLen(obj.Document_No) <= 0) Then
                Throw New Exception("Document No: " + strCode + " not found to Post")
            End If
            If (obj.Status = ERPTransactionStatus.Approved) Then
                Throw New Exception("Already Posted on :" + obj.Posting_Date)
            End If
            'clsMCCPaymentCycleLockForScheduler.CheckForSchedulerLock(obj.MCC_Code, obj.Document_Date, trans)
            For Each objtr As clsMilkCollectionMCCDetail In obj.Arr
                If objtr.Qty <= 0 Then
                    Throw New Exception("Qty is Zero at Sample No" + clsCommon.myCstr(objtr.SNo))
                End If
                If objtr.FAT <= 0 Then
                    Throw New Exception("FAT is Zero at Sample No" + clsCommon.myCstr(objtr.SNo))
                End If
                If objtr.SNF <= 0 Then
                    Throw New Exception("SNF is Zero at Sample No" + clsCommon.myCstr(objtr.SNo))
                End If
            Next

            'HistoryUpdate(strCode, trans)
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Status", 1)
            clsCommon.AddColumnsForChange(coll, "Posted_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Posted_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))
            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MILK_COLLECTION_MCC", OMInsertOrUpdate.Update, "Document_No='" + obj.Document_No + "'", trans)
            'Throw New Exception("Balwinder Singh Premi")

        Catch ex As Exception

            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function ReverseAndUnpost(ByVal strCode As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            ReverseAndUnpost(strCode, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function ReverseAndUnpost(ByVal strDocNo As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim obj As clsMilkCollectionMCC = clsMilkCollectionMCC.GetData(strDocNo, NavigatorType.Current, trans)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.Status) <= 0) Then
                clsCommon.MyMessageBoxShow("No Data found to Reverse And UnPost")
            End If

            If Not obj.Status = ERPTransactionStatus.Approved Then
                clsCommon.MyMessageBoxShow("Transaction status should be posted for reverse and unpost")
            End If

            Dim qry As String = "select Document_No from TSPL_MILK_COLLECTION_DCS_MCC_DETAIL where Against_Milk_Collection_MCC_Detail in (
select PK_Id from TSPL_MILK_COLLECTION_MCC_DETAIL where Document_No='" + strDocNo + "')"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Throw New Exception("BMC Truck Sheet Document No [" + strDocNo + "] is used in DCS Trcuk Sheet No [" + clsCommon.myCstr(dt.Rows(0)("Document_No")) + "]")
            End If

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Status", 0)
            clsCommon.AddColumnsForChange(coll, "Posted_By", Nothing, True)
            clsCommon.AddColumnsForChange(coll, "Posted_Date", Nothing, True)
            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MILK_COLLECTION_MCC", OMInsertOrUpdate.Update, "Document_No='" + obj.Document_No + "'", trans)


        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function CorrectionData(ByVal obj As clsMilkCollectionMCC) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            HistoryUpdate(obj.Document_No, trans)

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Entered_Qty", obj.Entered_Qty)
            clsCommon.AddColumnsForChange(coll, "Entered_FATKg", obj.Entered_FATKg)
            clsCommon.AddColumnsForChange(coll, "Entered_SNFKg", obj.Entered_SNFKg)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))

            clsCommon.AddColumnsForChange(coll, "Temp", obj.Temp)
            clsCommon.AddColumnsForChange(coll, "ORG", obj.ORG)
            clsCommon.AddColumnsForChange(coll, "Acidity", obj.Acidity)
            clsCommon.AddColumnsForChange(coll, "Description", obj.Description)

            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MILK_COLLECTION_MCC", OMInsertOrUpdate.Update, "TSPL_MILK_COLLECTION_MCC.Document_No='" + obj.Document_No + "'", trans)
            trans.Commit()
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function

End Class

Public Class clsMilkCollectionMCCDetail
#Region "Variables"
    Public PK_Id As Integer = 0
    Public Document_No As String
    Public SNo As Integer
    Public Sample_No As Integer
    Public MCC_Uploader_Code As String ''Not a Table Column
    Public MCC_Code As String
    Public MCC_Name As String
    Public Milk_Type As String
    Public Qty As Decimal
    Public FAT As Decimal
    Public SNF As Decimal
    Public Retesting_FAT As Decimal
    Public Retesting_SNF As Decimal
    Public Retesting_CLR As Decimal
    Public Retesting_OR_Correction As Integer
    Public Correction_FAT As Decimal
    Public Correction_SNF As Decimal
    Public Machine_FAT As Decimal
    Public Machine_SNF As Decimal
    Public FATKG As Decimal
    Public SNFKG As Decimal
    Public Temp As Decimal
    Public Gaze_Reading As Decimal
    Public Gaze_Qty As Decimal
    Public Gaze_Reading_Code As String
    Public Silo_Capacity As Integer
    Public Against_Multiple_Days As Integer
    Public REF_PK_ID_BMCDCS_TRIP As Integer



#End Region

    Public Shared Function AddMissing(ByVal strDocNo As String, ByVal Arr As List(Of clsMilkCollectionMCCDetail)) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim dtDocDate As DateTime = clsCommon.myCDate(clsDBFuncationality.getSingleValue("select Document_Date from TSPL_MILK_COLLECTION_MCC where Document_No='" + strDocNo + "'", trans))
            SaveData(strDocNo, dtDocDate, Arr, False, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal dtDocDate As DateTime, ByVal Arr As List(Of clsMilkCollectionMCCDetail), ByVal IsUpdatedFromCorrection As Boolean) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            SaveData(strDocNo, dtDocDate, Arr, False, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal dtDocDate As DateTime, ByVal Arr As List(Of clsMilkCollectionMCCDetail), ByVal IsUpdatedFromCorrection As Boolean, ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsMilkCollectionMCCDetail In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Document_No", strDocNo)
                clsCommon.AddColumnsForChange(coll, "SNo", obj.SNo)
                clsCommon.AddColumnsForChange(coll, "Sample_No", obj.Sample_No)
                clsCommon.AddColumnsForChange(coll, "MCC_Code", obj.MCC_Code)
                If clsCommon.CompairString(obj.Milk_Type, "Good") = CompairStringResult.Equal Then
                    obj.Milk_Type = "Good" ''To Handle Case sensitivity
                End If
                clsCommon.AddColumnsForChange(coll, "Milk_Type", obj.Milk_Type)
                clsCommon.AddColumnsForChange(coll, "Qty", obj.Qty)
                clsCommon.AddColumnsForChange(coll, "FAT", obj.FAT)
                clsCommon.AddColumnsForChange(coll, "SNF", obj.SNF)
                clsCommon.AddColumnsForChange(coll, "FATKG", obj.FATKG)
                clsCommon.AddColumnsForChange(coll, "SNFKG", obj.SNFKG)
                If obj.Retesting_OR_Correction = 1 Then
                    clsCommon.AddColumnsForChange(coll, "Retesting_FAT", obj.Retesting_FAT)
                    clsCommon.AddColumnsForChange(coll, "Retesting_SNF", obj.Retesting_SNF)
                    clsCommon.AddColumnsForChange(coll, "Retesting_CLR", obj.Retesting_CLR)
                ElseIf obj.Retesting_OR_Correction = 2 Then
                    clsCommon.AddColumnsForChange(coll, "Correction_FAT", obj.Correction_FAT)
                    clsCommon.AddColumnsForChange(coll, "Correction_SNF", obj.Correction_SNF)
                End If
                clsCommon.AddColumnsForChange(coll, "Retesting_OR_Correction", obj.Retesting_OR_Correction)
                clsCommon.AddColumnsForChange(coll, "Temp", obj.Temp)
                clsCommon.AddColumnsForChange(coll, "Gaze_Reading", obj.Gaze_Reading)
                clsCommon.AddColumnsForChange(coll, "Gaze_Qty", obj.Gaze_Qty, True)
                clsCommon.AddColumnsForChange(coll, "Gaze_Reading_Code", obj.Gaze_Reading_Code, True)
                clsCommon.AddColumnsForChange(coll, "Silo_Capacity", obj.Silo_Capacity)
                clsCommon.AddColumnsForChange(coll, "Against_Multiple_Days", obj.Against_Multiple_Days, True)

                clsCommon.AddColumnsForChange(coll, "REF_PK_ID_BMCDCS_TRIP", obj.REF_PK_ID_BMCDCS_TRIP, True)
                clsCommon.AddColumnsForChange(coll, "IsUpdatedFromCorrection", IIf(IsUpdatedFromCorrection = True, 1, 0))

                If obj.PK_Id > 0 Then
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MILK_COLLECTION_MCC_DETAIL", OMInsertOrUpdate.Update, "PK_Id='" + clsCommon.myCstr(obj.PK_Id) + "' ", trans)
                Else
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MILK_COLLECTION_MCC_DETAIL", OMInsertOrUpdate.Insert, "", trans)
                End If
            Next
        End If

        If clsCommon.myCDecimal(clsFixedParameter.GetData(clsFixedParameterType.RepeatBMCSampleNo, clsFixedParameterCode.RepeatBMCSampleNo, trans)) = 0 Then
            Dim qry As String = "select Sample_No,max(SNo) as SNo,MCC_Code,max(Document_No) as Document_No,max(Mcc_Code_VLC_Uploader) as Mcc_Code_VLC_Uploader,max(MCC_NAME) as MCC_NAME from (
select TSPL_MILK_COLLECTION_MCC.Document_No,TSPL_MILK_COLLECTION_MCC_DETAIL.SNo,isnull(TSPL_MILK_COLLECTION_MCC_DETAIL.Sample_No,0) as Sample_No,TSPL_MILK_COLLECTION_MCC_DETAIL.MCC_Code,TSPL_MCC_MASTER.Mcc_Code_VLC_Uploader,TSPL_MCC_MASTER.MCC_NAME
from TSPL_MILK_COLLECTION_MCC_DETAIL  
left outer join TSPL_MILK_COLLECTION_MCC on TSPL_MILK_COLLECTION_MCC.Document_No=TSPL_MILK_COLLECTION_MCC_DETAIL.Document_No
left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER .MCC_Code=TSPL_MILK_COLLECTION_MCC_DETAIL.MCC_Code
where   convert(Date,TSPL_MILK_COLLECTION_MCC.Document_Date,103)='" + clsCommon.GetPrintDate(dtDocDate, "dd/MMM/yyyy") + "' 
and exists (select 1 from (select TSPL_MILK_COLLECTION_MCC_DETAIL.MCC_Code,isnull(TSPL_MILK_COLLECTION_MCC_DETAIL.Sample_No,0) as Sample_No from TSPL_MILK_COLLECTION_MCC_DETAIL where Document_No ='" + strDocNo + "')Tab where Tab.MCC_Code=TSPL_MILK_COLLECTION_MCC_DETAIL.MCC_Code and  Tab.Sample_No=isnull(TSPL_MILK_COLLECTION_MCC_DETAIL.Sample_No,0))
)x Group by Sample_No,MCC_Code  having sum(1)>1"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Throw New Exception("Repeated Sample Found For Same BMC [" + clsCommon.myCstr(dt.Rows(0)("Mcc_Code_VLC_Uploader")) + " " + clsCommon.myCstr(dt.Rows(0)("MCC_Code")) + " " + clsCommon.myCstr(dt.Rows(0)("MCC_NAME")) + " ] and sample no [" + clsCommon.myCstr(dt.Rows(0)("Sample_No")) + "] ")
            End If
        End If

        Return True
    End Function

    Public Shared Function GetData(ByVal strPONo As String, ByVal strExtraWhrclas As String, ByVal trans As SqlTransaction) As List(Of clsMilkCollectionMCCDetail)
        Dim arr As List(Of clsMilkCollectionMCCDetail) = Nothing
        Dim qry As String = "SELECT TSPL_MILK_COLLECTION_MCC_DETAIL.*,TSPL_MCC_MASTER.MCC_NAME,TSPL_MCC_MASTER.Mcc_Code_VLC_Uploader  
FROM TSPL_MILK_COLLECTION_MCC_DETAIL 
left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_code=TSPL_MILK_COLLECTION_MCC_DETAIL.MCC_Code  
where  TSPL_MILK_COLLECTION_MCC_DETAIL.Document_No='" + strPONo + "' "
        If clsCommon.myLen(strExtraWhrclas) > 0 Then
            qry += " and " + strExtraWhrclas
        End If
        qry += " ORDER BY TSPL_MILK_COLLECTION_MCC_DETAIL.SNO"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            arr = New List(Of clsMilkCollectionMCCDetail)
            Dim objTr As clsMilkCollectionMCCDetail
            For Each dr As DataRow In dt.Rows
                objTr = New clsMilkCollectionMCCDetail
                objTr.PK_Id = clsCommon.myCDecimal(dr("PK_Id"))
                objTr.Document_No = clsCommon.myCstr(dr("Document_No"))
                objTr.SNo = clsCommon.myCDecimal(dr("SNo"))
                objTr.Sample_No = clsCommon.myCDecimal(dr("Sample_No"))
                objTr.MCC_Uploader_Code = clsCommon.myCstr(dr("Mcc_Code_VLC_Uploader"))
                objTr.MCC_Code = clsCommon.myCstr(dr("MCC_Code"))
                objTr.MCC_Name = clsCommon.myCstr(dr("MCC_NAME"))
                objTr.Qty = clsCommon.myCDecimal(dr("Qty"))
                objTr.FAT = clsCommon.myCDecimal(dr("FAT"))
                objTr.SNF = clsCommon.myCDecimal(dr("SNF"))
                objTr.FATKG = clsCommon.myCDecimal(dr("FATKG"))
                objTr.SNFKG = clsCommon.myCDecimal(dr("SNFKG"))
                objTr.Milk_Type = clsCommon.myCstr(dr("Milk_Type"))
                objTr.Temp = clsCommon.myCDecimal(dr("Temp"))
                objTr.Gaze_Reading = clsCommon.myCDecimal(dr("Gaze_Reading"))
                objTr.Gaze_Qty = clsCommon.myCDecimal(dr("Gaze_Qty"))
                objTr.Gaze_Reading_Code = clsCommon.myCstr(dr("Gaze_Reading_Code"))
                objTr.Silo_Capacity = clsCommon.myCDecimal(dr("Silo_Capacity"))
                objTr.Against_Multiple_Days = clsCommon.myCDecimal(dr("Against_Multiple_Days"))
                arr.Add(objTr)
            Next
        End If
        Return arr
    End Function

    Public Shared Function DeleteData(ByVal PKID As Integer) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim qry As String = "select Document_No from TSPL_MILK_COLLECTION_DCS_MCC_DETAIL where Against_Milk_Collection_MCC_Detail=" + clsCommon.myCstr(PKID) + ""
            qry = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
            If clsCommon.myLen(qry) > 0 Then
                Throw New Exception("DCS Truck sheet entered [" + qry + "].Cant Delete it")
            End If

            qry = "select Document_No from  TSPL_MILK_COLLECTION_MCC_DETAIL where Document_No in ( select Document_No from TSPL_MILK_COLLECTION_MCC_DETAIL where PK_Id = " + clsCommon.myCstr(PKID) + ")"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                Throw New Exception("Invalid PK ID")
            End If
            qry = "Delete from TSPL_MILK_COLLECTION_MCC_DETAIL where PK_Id=" + clsCommon.myCstr(PKID) + ""
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            If dt.Rows.Count = 1 Then
                qry = "Delete from TSPL_MILK_COLLECTION_MCC where Document_No='" + clsCommon.myCstr(dt.Rows(0)("Document_No")) + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
            End If
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
End Class

