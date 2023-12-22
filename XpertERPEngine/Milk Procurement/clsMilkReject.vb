Imports common
Imports System.Data.SqlClient

Public Class clsMilkRejectHead

#Region "Variables"
    Public DOC_CODE As String
    Public MCC_CODE As String
    Public DOC_DATE As DateTime
    Public SHIFT As String
    Public TOTAL_WEIGHT As Decimal
    Public POSTED As ERPTransactionStatus = ERPTransactionStatus.Pending
    Public Posting_Date As Date?

    Public MCC_NAME As String
    Public Arr As List(Of clsMilkRejectDetail)

#End Region

    Public Shared Function SaveData(ByVal obj As clsMilkRejectHead, ByVal isSkipShiftOpenCheck As Boolean) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            SaveData(obj, isSkipShiftOpenCheck, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function SaveData(ByVal obj As clsMilkRejectHead, ByVal isSkipShiftOpenCheck As Boolean, ByVal trans As SqlTransaction) As Boolean
        Try
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleMCCMilkProcurement, clsUserMgtCode.MilkReject, obj.MCC_CODE, obj.DOC_DATE, trans)

            Dim isNewEntry As Boolean

            If Not isSkipShiftOpenCheck Then
                obj.DOC_CODE = GetDocCode(obj.DOC_DATE, obj.MCC_CODE, obj.SHIFT, trans)
                If GetPost(obj.DOC_DATE, obj.MCC_CODE, obj.SHIFT, trans) Then
                    Throw New Exception("This Shift is posted. Check Code:" + obj.DOC_CODE + ".")
                End If
            End If

            If clsCommon.myLen(obj.DOC_CODE) <= 0 Then
                isNewEntry = True
                obj.DOC_CODE = clsERPFuncationality.GetNextCode(trans, clsCommon.GetPrintDate(obj.DOC_DATE, "dd/MMM/yyyy hh:mm:ss tt"), clsDocType.MilkReject, "", obj.MCC_CODE, False, True, False, False, objCommonVar.ShowMCCFinderInPaymentProcess)
            Else
                isNewEntry = False
            End If
            If (clsCommon.myLen(obj.DOC_CODE) <= 0) Then
                Throw New Exception("Error in Document Code Generation")
            End If
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "DOC_CODE", obj.DOC_CODE)
            clsCommon.AddColumnsForChange(coll, "MCC_CODE", obj.MCC_CODE)
            clsCommon.AddColumnsForChange(coll, "DOC_DATE", clsCommon.GetPrintDate(obj.DOC_DATE, "dd/MMM/yyyy hh:mm:ss tt"))
            clsCommon.AddColumnsForChange(coll, "SHIFT", obj.SHIFT)
            clsCommon.AddColumnsForChange(coll, "TOTAL_WEIGHT", clsCommon.myCstr(obj.TOTAL_WEIGHT))
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))
                Dim Strqry As String = "SELECT Count(*) FROM TSPL_MILK_REJECT_HEAD where DOC_CODE = '" & obj.DOC_CODE & "'"
                Dim check As Integer = clsDBFuncationality.getSingleValue(Strqry, trans)
                If check = 0 Then
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MILK_REJECT_HEAD", OMInsertOrUpdate.Insert, "", trans)
                Else
                    Throw New Exception("This Code:" + obj.DOC_CODE + " Is Already Exist")
                End If
            Else
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MILK_REJECT_HEAD", OMInsertOrUpdate.Update, "TSPL_MILK_REJECT_HEAD.DOC_CODE='" + obj.DOC_CODE + "'", trans)
            End If
            Dim settAlwaysVSPDefaulter As Boolean = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AlwaysVSPDefaulter, clsFixedParameterCode.AlwaysVSPDefaulter, trans)) = 1)
            clsMilkRejectDetail.SaveData(obj.DOC_CODE, obj, settAlwaysVSPDefaulter, trans)
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsMilkRejectHead
        Return GetData(strCode, NavType, Nothing)
    End Function

    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim isSaved As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            isSaved = False

            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Code not found to Delete")
            End If

            Dim qry As String
            qry = "select * from TSPL_MILK_SAMPLE_HEAD where MILK_RECEIPT_CODE='" & strCode & "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dt.Rows.Count > 0 Then
                Throw New Exception("Code can not Deleted.It has been Sampled")
            End If

            qry = "delete from TSPL_MILK_REJECT_DETAIL where DOC_CODE ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)


            qry = "delete from TSPL_MILK_REJECT_HEAD where DOC_CODE ='" + strCode + "'"
            isSaved = isSaved And clsDBFuncationality.ExecuteNonQuery(qry, trans)

            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message.ToString())
        End Try
        Return isSaved
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsMilkRejectHead
        Return GetData(strCode, NavType, trans, 0)
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction, ByVal intSampleNo As Integer) As clsMilkRejectHead

        Dim obj As New clsMilkRejectHead()
        Dim objtr As New clsMilkRejectDetail
        obj.Arr = New List(Of clsMilkRejectDetail)

        Dim qry As String = "SELECT TSPL_MILK_REJECT_HEAD.DOC_CODE,TSPL_MILK_REJECT_HEAD.MCC_CODE,TSPL_MILK_REJECT_HEAD.DOC_DATE,TSPL_MILK_REJECT_HEAD.SHIFT,TSPL_MILK_REJECT_HEAD.TOTAL_WEIGHT,TSPL_MCC_MASTER.MCC_NAME,  TSPL_MILK_REJECT_HEAD.Created_By,TSPL_MILK_REJECT_HEAD.Posted,TSPL_MILK_REJECT_HEAD.POSTED,TSPL_MILK_REJECT_HEAD.Posting_Date FROM TSPL_MILK_REJECT_HEAD  INNER JOIN TSPL_MCC_MASTER   ON TSPL_MILK_REJECT_HEAD.MCC_CODE=TSPL_MCC_MASTER.MCC_CODE where 2=2 "
        Select Case NavType
            Case NavigatorType.First
                qry += " AND DOC_CODE = (select MIN(DOC_CODE) from TSPL_MILK_REJECT_HEAD)"
            Case NavigatorType.Last
                qry += " AND DOC_CODE = (select Max(DOC_CODE) from TSPL_MILK_REJECT_HEAD)"
            Case NavigatorType.Next
                qry += " AND DOC_CODE = (select Min(DOC_CODE) from TSPL_MILK_REJECT_HEAD where  DOC_CODE>'" + strCode + "')"
            Case NavigatorType.Previous
                qry += " AND DOC_CODE = (select Max(DOC_CODE) from TSPL_MILK_REJECT_HEAD where DOC_CODE<'" + strCode + "')"
            Case NavigatorType.Current
                qry += " AND DOC_CODE = '" + strCode + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then

            obj.DOC_CODE = dt.Rows(0)("DOC_CODE")
            obj.MCC_CODE = clsCommon.myCstr(dt.Rows(0)("MCC_CODE"))
            obj.DOC_DATE = clsCommon.GetPrintDate(dt.Rows(0)("DOC_DATE"), "dd/MMM/yyyy hh:mm:ss tt")
            obj.SHIFT = clsCommon.myCstr(dt.Rows(0)("SHIFT"))
            obj.TOTAL_WEIGHT = clsCommon.myCstr(dt.Rows(0)("TOTAL_WEIGHT"))
            obj.MCC_NAME = clsCommon.myCstr(dt.Rows(0)("MCC_NAME"))
            obj.POSTED = IIf(clsCommon.myCdbl(dt.Rows(0)("Posted")) = 1, ERPTransactionStatus.Approved, ERPTransactionStatus.Pending)
            If dt.Rows(0)("Posting_Date") IsNot DBNull.Value Then
                obj.Posting_Date = clsCommon.myCDate(dt.Rows(0)("Posting_Date"))
            Else
                obj.Posting_Date = Nothing
            End If
        End If
        qry = "  SELECT TSPL_MILK_REJECT_DETAIL.*,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader" + Environment.NewLine +
        " FROM TSPL_MILK_REJECT_DETAIL " + Environment.NewLine +
        " left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code=TSPL_MILK_REJECT_DETAIL.VLC_CODE   " + Environment.NewLine +
        " WHERE 2=2 AND TSPL_MILK_REJECT_DETAIL.DOC_CODE = '" + obj.DOC_CODE + "'"
        If intSampleNo > 0 Then
            qry += " and SAMPLE_NO='" + clsCommon.myCstr(intSampleNo) + "' "
        End If
        qry += "  ORDER BY [SAMPLE_NO] "

        dt = New DataTable()
        dt = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            For Each dr As DataRow In dt.Rows
                objtr = New clsMilkRejectDetail
                objtr.DOC_CODE = strCode
                objtr.VLC_CODE = clsCommon.myCstr(dr("VLC_CODE"))
                objtr.VLC_CODE_Uploader_Code = clsCommon.myCstr(dr("VLC_Code_VLC_Uploader"))
                objtr.ROUTE_CODE = clsCommon.myCstr(dr("ROUTE_CODE"))
                objtr.NO_OF_CANS = clsCommon.myCdbl(dr("NO_OF_CANS"))
                objtr.VSP_CODE = clsCommon.myCstr(dr("VSP_CODE"))
                objtr.Item_CODE = clsCommon.myCstr(dr("Item_CODE"))
                objtr.MILK_WEIGHT = clsCommon.myCdbl(dr("MILK_WEIGHT"))
                objtr.ACC_WEIGHT_KG = clsCommon.myCdbl(dr("ACC_WEIGHT_KG"))
                objtr.ACC_WEIGHT_LTR = clsCommon.myCdbl(dr("ACC_WEIGHT_LTR"))
                objtr.SAMPLE_NO = clsCommon.myCdbl(dr("SAMPLE_NO"))
                objtr.VEHICLE_CODE = clsCommon.myCstr(dr("VEHICLE_CODE"))
                objtr.Other_VEHICLE = IIf(clsCommon.myCdbl(dr("Other_VEHICLE")) = 1, True, False)
                objtr.Other_VLC = IIf(clsCommon.myCdbl(dr("Other_VLC")) = 1, True, False)
                objtr.Reject_Type = clsCommon.myCstr(dr("Reject_Type"))
                objtr.FAT = clsCommon.myCdbl(dr("FAT"))
                objtr.SNF = clsCommon.myCdbl(dr("SNF"))
                objtr.RATE = clsCommon.myCdbl(dr("RATE"))
                objtr.Amount = clsCommon.myCdbl(dr("Amount"))
                objtr.SNF_RATE = clsCommon.myCdbl(dr("SNF_RATE"))
                objtr.SNF_Amount = clsCommon.myCdbl(dr("SNF_Amount"))
                objtr.FAT_RATE = clsCommon.myCdbl(dr("FAT_RATE"))
                objtr.FAT_Amount = clsCommon.myCdbl(dr("FAT_Amount"))
                objtr.UOM_Code = clsCommon.myCstr(dr("UOM_COde"))
                objtr.FAT_Deduction_Per = clsCommon.myCdbl(dr("FAT_Deduction_Per"))
                objtr.FAT_Deduction_Amount = clsCommon.myCdbl(dr("FAT_Deduction_Amount"))
                objtr.FAT_Amount_After_Deduction = clsCommon.myCdbl(dr("FAT_Amount_After_Deduction"))
                objtr.SNF_Deduction_Per = clsCommon.myCdbl(dr("SNF_Deduction_Per"))
                objtr.SNF_Deduction_Amount = clsCommon.myCdbl(dr("SNF_Deduction_Amount"))
                objtr.SNF_Amount_After_Deduction = clsCommon.myCdbl(dr("SNF_Amount_After_Deduction"))
                objtr.Is_Return = clsCommon.myCdbl(dr("Is_Return"))
                objtr.Conversion_Factor = clsCommon.myCdbl(dr("Conversion_factor"))
                objtr.Price_Code = clsCommon.myCstr(dr("Price_Code"))
                objtr.Defaulter = clsCommon.myCstr(dr("Defaulter"))
                obj.Arr.Add(objtr)
            Next
        End If
        Return obj
    End Function

    Public Function Gettable(ByVal strCode As String, ByVal NavType As NavigatorType, Optional ByVal trans As SqlTransaction = Nothing) As DataTable
        Dim qry As String = "   SELECT TSPL_MILK_REJECT_DETAIL.DOC_CODE as [DOC CODE],SAMPLE_NO as [SAMPLE NO],TSPL_MILK_REJECT_DETAIL.VLC_CODE as [VLC CODE],TSPL_Vlc_Master_Head.VLC_NAME as [VLC NAME],TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader as [VLC UPLOADER CODE],Item_Desc  as [ITEM NAME],TSPL_MILK_REJECT_DETAIL.ROUTE_CODE as [ROUTE CODE],TSPL_Mcc_route_master.Route_Name as [ROUTE NAME],TSPL_MILK_REJECT_DETAIL.VSP_CODE as [VSP CODE],vendor_name as [VSP Name],TSPL_MILK_REJECT_DETAIL.VEHICLE_CODE as [VEHICLE CODE],case when Other_VEHICLE=1 then 'Yes' end as [Other Vehicle] ,  NO_OF_CANS as [NO OF CANS],MILK_WEIGHT as [MILK WEIGHT],TSPL_MILK_REJECT_DETAIL.UOM_Code as [Unit Code],round(Conversion_Factor,2) as [Conversion Ratio],TSPL_MILK_REJECT_DETAIL.ACC_WEIGHT_KG as [Actual Qty(KG)] ,TSPL_MILK_REJECT_DETAIL.Reject_Type as [Reject Type],TSPL_MILK_REJECT_DETAIL.FAT,TSPL_MILK_REJECT_DETAIL.SNF,TSPL_MILK_REJECT_DETAIL.Rate,TSPL_MILK_REJECT_DETAIL.Amount,TSPL_MCC_MASTER.MCC_CODE as [MCC CODE],DOC_DATE as [DOC DATE],Case when SHIFT='M' then 'Morning' else 'Evening' end as [Shift],TSPL_MILK_REJECT_DETAIL.SNF_RATE as [SNF Rate],TSPL_MILK_REJECT_DETAIL.SNF_Amount as [SNF Amount],TSPL_MILK_REJECT_DETAIL.SNF_Deduction_Per as [SNF Deduction Per],TSPL_MILK_REJECT_DETAIL.SNF_Deduction_Amount as [SNF Deduction Amount],TSPL_MILK_REJECT_DETAIL.SNF_Amount_After_Deduction as [SNF Amount After Deduction],TSPL_MILK_REJECT_DETAIL.FAT_RATE as [FAT Rate],TSPL_MILK_REJECT_DETAIL.FAT_Amount as [FAT Amount],TSPL_MILK_REJECT_DETAIL.FAT_Deduction_Per as [FAT Deduction Per],TSPL_MILK_REJECT_DETAIL.FAT_Deduction_Amount as [FAT Deduction Amount],TSPL_MILK_REJECT_DETAIL.FAT_Amount_After_Deduction as [FAT Amount After Deduction],  TSPL_MILK_REJECT_DETAIL.Is_Return  as [Return],TSPL_MILK_REJECT_DETAIL.Defaulter "
        If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ShowVehicleNoSeparatelyInPrimaryTransVehicleMaster, clsFixedParameterCode.ShowVehicleNoSeparatelyInPrimaryTransVehicleMaster, Nothing)) = 1 Then
            qry += " ,TSPL_Primary_Vehicle_Master.Vehicle"
        End If
        qry += " FROM TSPL_MILK_REJECT_DETAIL " + Environment.NewLine +
         " left outer join TSPL_MILK_REJECT_HEAD on TSPL_MILK_REJECT_HEAD.DOC_CODE=TSPL_MILK_REJECT_DETAIL.DOC_CODE " + Environment.NewLine +
         " left outer join  TSPL_MCC_MASTER on TSPL_MCC_MASTER.mcc_code=TSPL_MILK_REJECT_HEAD.MCC_CODE " + Environment.NewLine +
         " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_MILK_REJECT_DETAIL.Item_Code  " + Environment.NewLine +
         " left outer join TSPL_UNIT_MASTER on TSPL_UNIT_MASTER.Unit_Code=TSPL_MILK_REJECT_DETAIL.UOM_Code  " + Environment.NewLine +
         " left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code=TSPL_MILK_REJECT_DETAIL.VLC_CODE " + Environment.NewLine +
         " left outer join TSPL_MCC_ROUTE_MASTER on TSPL_MCC_ROUTE_MASTER.Route_Code=TSPL_MILK_REJECT_DETAIL.ROUTE_CODE  " + Environment.NewLine +
         " left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code = TSPL_MILK_REJECT_DETAIL.VSP_CODE " + Environment.NewLine +
         " left join TSPL_Primary_Vehicle_Master on TSPL_Primary_Vehicle_Master.Vehicle_Code=TSPL_MILK_REJECT_DETAIL.VEHICLE_CODE " + Environment.NewLine +
         " WHERE 2=2  AND TSPL_MILK_REJECT_DETAIL.DOC_CODE = '" + strCode + "'  ORDER BY TSPL_MILK_REJECT_DETAIL.Sample_No"
        Dim dt As DataTable = New DataTable()
        dt = clsDBFuncationality.GetDataTable(qry, trans)
        Return dt
    End Function

    Public Shared Function GetShift(ByVal Mcc_Code As String, Optional ByVal trans As SqlTransaction = Nothing) As DataTable
        Dim sQuery As String = "select TSPL_OPEN_MCC_SHIFT.*,UOM_Code from TSPL_OPEN_MCC_SHIFT Left join TSPL_Mcc_UOM_DETAIL on TSPL_Mcc_UOM_DETAIL.MCC_CODE=TSPL_OPEN_MCC_SHIFT.MCC_CODE and Stocking_Unit='Y' where lower(status)='o' and TSPL_OPEN_MCC_SHIFT.mcc_code='" & Mcc_Code & "'"
        Return clsDBFuncationality.GetDataTable(sQuery, trans)
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
        Return PostData(strDocNo, False, trans)
    End Function

    Public Shared Function PostData(ByVal strDocNo As String, ByVal isSkipUpdateDefaulter As Boolean, ByVal trans As SqlTransaction) As Boolean
        Try
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Code not found to Post")
            End If
            Dim strPostDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt")
            Dim obj As clsMilkRejectHead = clsMilkRejectHead.GetData(strDocNo, NavigatorType.Current, trans)

            If (obj Is Nothing OrElse clsCommon.myLen(obj.DOC_CODE) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            If (obj.POSTED = 1) Then
                Throw New Exception("Already Post on :" + obj.Posting_Date)
            End If
            If Not isSkipUpdateDefaulter Then
                UpdateDefaulter(obj, trans)
            End If
            SaveSRNData(obj, trans)
            SaveAdjustmentData(obj, trans)
            Dim qry As String = "Update TSPL_MILK_REJECT_HEAD set POSTED=1, Posting_Date='" + strPostDate + "',Modified_By='" + objCommonVar.CurrentUserCode + "' where DOC_CODE ='" + strDocNo + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Shared Function UpdateDefaulter(ByVal obj As clsMilkRejectHead, ByVal trans As SqlTransaction) As Boolean
        If (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.SelectMilkRejectDefaulterManually, clsFixedParameterCode.SelectMilkRejectDefaulterManually, trans)) = 0) Then
            If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                Dim settAlwaysVSPDefaulter As Boolean = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AlwaysVSPDefaulter, clsFixedParameterCode.AlwaysVSPDefaulter, trans)) = 1)
                For ii As Integer = 0 To obj.Arr.Count - 1
                    Dim objtr As clsMilkRejectDetail = obj.Arr(ii)
                    Dim strDefaulter As String = "VSP"
                    Dim qry As String
                    Dim dt As DataTable
                    If objtr.Is_Return = 0 AndAlso Not settAlwaysVSPDefaulter Then
                        If clsCommon.CompairString(objtr.Reject_Type, "Sour") = CompairStringResult.Equal OrElse clsCommon.CompairString(objtr.Reject_Type, "Curd") = CompairStringResult.Equal Then
                            qry = "select Entry_Date  from TSPL_MILK_GATE_ENTRY_IN where convert(date, Shift_Date,103)='" + clsCommon.GetPrintDate(obj.DOC_DATE, "dd/MMM/yyyy") + "' and Entry_Shift='" + obj.SHIFT + "'  and MCC_CODE='" + obj.MCC_CODE + "' and Route_Code='" + objtr.ROUTE_CODE + "'"
                            dt = clsDBFuncationality.GetDataTable(qry, trans)
                            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                                Throw New Exception("No gate entry in transaction found to apply dedection.")
                            End If
                            Dim dtActualEntryDate As DateTime = clsCommon.myCDate(dt.Rows(0)("Entry_Date"))
                            qry = "select " + IIf(clsCommon.CompairString(obj.SHIFT, "M") = CompairStringResult.Equal, "MCC_Reaching_Time_M", "MCC_Reaching_Time_E") + " as ReachingTime  from TSPL_MCC_ROUTE_MASTER where Route_Code='" + objtr.ROUTE_CODE + "'"
                            dt = clsDBFuncationality.GetDataTable(qry, trans)
                            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                                Throw New Exception("Please provide reaching time " + obj.SHIFT + " of route " + objtr.ROUTE_CODE)
                            End If
                            If dt.Rows(0)("ReachingTime") Is DBNull.Value Then
                                Throw New Exception("Please provide reaching time " + obj.SHIFT + " of route " + objtr.ROUTE_CODE)
                            End If
                            Dim dblGracePeriod As Integer = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.GraceTimeForTransporter, clsFixedParameterCode.GraceTimeForTransporter, trans))
                            Dim dtRouteReachingTime As DateTime = clsCommon.myCDate(dt.Rows(0)("ReachingTime"))
                            Dim dblGraceDay As Integer = 0
                            If clsCommon.CompairString(obj.SHIFT, "E") = CompairStringResult.Equal Then
                                If dtRouteReachingTime.Hour < 12 Then ''Time is In AM 
                                    If obj.DOC_DATE.Day = dtActualEntryDate.Day Then
                                        dblGraceDay = 1
                                    End If
                                End If
                            End If

                            dtRouteReachingTime = New Date(dtActualEntryDate.Year, dtActualEntryDate.Month, dtActualEntryDate.Day, dtRouteReachingTime.Hour, dtRouteReachingTime.Minute, dtRouteReachingTime.Second).AddMinutes(dblGracePeriod)
                            dtRouteReachingTime = dtRouteReachingTime.AddDays(dblGraceDay)
                            If dtActualEntryDate > dtRouteReachingTime Then
                                strDefaulter = "Transporter"
                            Else
                                ''Check for company Fault
                                ''UDL/03/05/18-000149 change doc_date to Entry_Date_time  
                                qry = "select  Top 1 TSPL_MILK_RECEIPT_DETAIL.Entry_Date_Time  from TSPL_MILK_RECEIPT_DETAIL" + Environment.NewLine +
                                " left outer join TSPL_MILK_RECEIPT_HEAD on TSPL_MILK_RECEIPT_HEAD.DOC_CODE =TSPL_MILK_RECEIPT_DETAIL.DOC_CODE " + Environment.NewLine +
                                " where convert(date, TSPL_MILK_RECEIPT_HEAD.DOC_DATE,103)='" + clsCommon.GetPrintDate(obj.DOC_DATE, "dd/MMM/yyyy") + "' and TSPL_MILK_RECEIPT_HEAD.SHIFT='" + obj.SHIFT + "'  and TSPL_MILK_RECEIPT_HEAD.MCC_CODE='" + obj.MCC_CODE + "'  and TSPL_MILK_RECEIPT_DETAIL.Route_Code='" + objtr.ROUTE_CODE + "' order by TSPL_MILK_RECEIPT_DETAIL.Entry_Date_Time"
                                Dim dtFirstDockReceipt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                                If dtFirstDockReceipt IsNot Nothing AndAlso dtFirstDockReceipt.Rows.Count > 0 Then
                                    Dim FirstDockReceiptDate As DateTime = clsCommon.myCDate(dtFirstDockReceipt.Rows(0)("Entry_Date_Time"))
                                    dblGracePeriod = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.GraceTimeFromGateEntryToDocWeighing, clsFixedParameterCode.GraceTimeFromGateEntryToDocWeighing, trans))
                                    Dim dtActualEntryDateWithDock As DateTime = dtActualEntryDate.AddMinutes(dblGracePeriod)
                                    If dtActualEntryDateWithDock < FirstDockReceiptDate Then
                                        strDefaulter = "Company"
                                    End If
                                Else
                                    strDefaulter = "VSP"
                                    'Throw New Exception("No Milk Receipt found to get company fault")
                                End If
                            End If
                        End If
                    End If
                    qry = "Update TSPL_MILK_REJECT_DETAIL Set Defaulter='" + strDefaulter + "' where DOC_CODE ='" + objtr.DOC_CODE + "' and SAMPLE_NO = '" + clsCommon.myCstr(objtr.SAMPLE_NO) + "'"
                    clsDBFuncationality.ExecuteNonQuery(qry, trans)
                    obj.Arr(ii).Defaulter = strDefaulter ''because used in next save srn data function.
                Next
            End If
        End If
        Return True
    End Function

    Shared Function SaveAdjustmentData(ByVal obj As clsMilkRejectHead, ByVal trans As SqlTransaction) As Boolean
        Dim settAlwaysVSPDefaulter As Boolean = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AlwaysVSPDefaulter, clsFixedParameterCode.AlwaysVSPDefaulter, trans)) = 1)
        If settAlwaysVSPDefaulter Then
            Return True
        End If

        If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
            Dim settRejectedMilkSendToRejectLocation As Boolean = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.RejectedMilkSendToRejectLocation, clsFixedParameterCode.RejectedMilkSendToRejectLocation, trans)) = 1)
            Dim strRejectLocation As String = ""
            If settRejectedMilkSendToRejectLocation Then
                strRejectLocation = clsLocation.GetRejectedLocation(obj.MCC_CODE, trans)
                If clsCommon.myLen(strRejectLocation) <= 0 Then
                    Throw New Exception("Please set rejected location for loaction : " + obj.MCC_CODE)
                End If
            End If

            For Each objtr As clsMilkRejectDetail In obj.Arr
                If objtr.Is_Return = 0 Then
                    Dim objAdj As New ClsAdjustments()
                    objAdj.Arr = New List(Of ClsAdjustmentsDetails)
                    ''Adjustment Entry
                    objAdj.Adjustment_No = ""
                    objAdj.Adjustment_Date = clsCommon.GetPrintDate(obj.DOC_DATE, "dd/MMM/yyyy hh:mm tt")
                    objAdj.Description = "Auto Adjustment Against Milk Reject -" & clsCommon.myCstr(obj.DOC_CODE) & "- Sample No-" + clsCommon.myCstr(objtr.SAMPLE_NO)
                    objAdj.Unit_Code = "ALL"
                    objAdj.IsMilkType = "1"
                    objAdj.Loc_Code = IIf(settRejectedMilkSendToRejectLocation, strRejectLocation, obj.MCC_CODE)
                    objAdj.Loc_Desc = clsLocation.GetName(objAdj.Loc_Code, trans)
                    objAdj.Trans_Type = "Out"
                    objAdj.chklocation = "N"
                    objAdj.MainLocationCode = ""
                    objAdj.MainLocationDesc = ""
                    objAdj.Reference_Document = "MLK-REJ"
                    objAdj.Document_No = obj.DOC_CODE
                    objAdj.Reference = clsCommon.myCstr(objtr.SAMPLE_NO)
                    If clsCommon.myLen(objtr.Item_CODE) > 0 Then
                        Dim objAdTr As New ClsAdjustmentsDetails()
                        objAdTr.Adjustment_Line_No = "1"
                        objAdTr.Item_Code = clsCommon.myCstr(objtr.Item_CODE)
                        If clsCommon.myLen(objAdTr.Item_Code) > 0 Then
                            objAdTr.Item_Description = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select ISNULL(Item_Desc,'') AS Item_Desc From TSPL_ITEM_MASTER Where Item_Code ='" & clsCommon.myCstr(objAdTr.Item_Code) & "'", trans))
                        Else
                            objAdTr.Item_Description = ""
                        End If
                        objAdTr.Adjustment_Type = clsCommon.myCstr("Cost").Substring(0, 1) + IIf(clsCommon.CompairString(objAdj.Trans_Type, "In") = CompairStringResult.Equal, "I", "D")
                        objAdTr.Item_Quantity = 0
                        objAdTr.Unit_Code = clsCommon.myCstr(objtr.UOM_Code)
                        Dim dt1 As DataTable = clsDBFuncationality.GetDataTable("select TSPL_PURCHASE_ACCOUNTS.Adjustment_Account ,TSPL_GL_ACCOUNTS.Description  from  TSPL_ITEM_MASTER left outer join TSPL_PURCHASE_ACCOUNTS on TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code=TSPL_ITEM_MASTER.Purchase_Class_Code left outer join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code=TSPL_PURCHASE_ACCOUNTS.Adjustment_Account where TSPL_ITEM_MASTER.Item_Code='" + objtr.Item_CODE + "'", trans)
                        If dt1 Is Nothing OrElse dt1.Rows.Count <= 0 Then
                            Throw New Exception("Plese set the Purchase Account set or its Adjustment Writeoff Account for item " + objAdTr.Item_Code)
                        End If
                        objAdTr.Account_Code = clsCommon.myCstr(dt1.Rows(0)("Adjustment_Account"))
                        objAdTr.Account_Description = clsCommon.myCstr(dt1.Rows(0)("Description"))
                        objAdTr.Remarks = ""
                        objAdTr.Comments = ""
                        objAdTr.mrp = 0
                        objAdTr.Batch_No = ""
                        objAdTr.Breakage = 0
                        objAdTr.Breakage_Cost = 0
                        objAdTr.ItemType = ""
                        objAdTr.BreakageType = ""
                        objAdTr.LeakageQty = 0
                        objAdTr.Basic_Price = 0
                        objAdTr.fat_Amt = objtr.FAT_Deduction_Amount
                        objAdTr.snf_Amt = objtr.SNF_Deduction_Amount
                        If clsCommon.CompairString(objtr.Reject_Type, "Curd") = CompairStringResult.Equal Then
                            objAdTr.Item_Cost = objtr.FAT_Deduction_Amount + objtr.SNF_Deduction_Amount
                            objAdTr.fat_pers = objtr.FAT
                            objAdTr.fat_kg = Math.Round(((objtr.ACC_WEIGHT_KG * objtr.FAT / 100) * objtr.FAT_Deduction_Per / 100), 3, MidpointRounding.ToEven)
                        Else
                            objAdTr.Item_Cost = objtr.SNF_Deduction_Amount
                        End If
                        objAdTr.snf_kg = Math.Round(((objtr.ACC_WEIGHT_KG * objtr.SNF / 100) * objtr.SNF_Deduction_Per / 100), 3, MidpointRounding.ToEven)
                        objAdTr.snf_pers = objtr.SNF
                        objAdTr.ItemType = clsItemMaster.GetStoreAdjustmentItemTypeWithTrans(objtr.Item_CODE, trans)
                        objAdTr.Itemstatus = "NEW"
                        If (clsCommon.myLen(objAdTr.Item_Code) > 0) Then
                            objAdj.Arr.Add(objAdTr)
                        End If
                    End If
                    If objAdj.Arr IsNot Nothing AndAlso objAdj.Arr.Count > 0 Then
                        objAdj.SaveData(objAdj, True, "", trans)
                        ClsAdjustments.PostData(objAdj.Adjustment_No, AdjustmentEnum.strCostTransaction, trans, False)
                    End If
                End If
            Next
        End If
        Return True
    End Function

    Shared Function SaveSRNData(ByVal obj As clsMilkRejectHead, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim objHead As clsMilkSRNMCC
            Dim objList As New List(Of clsMilkSRNMCCDetail)
            Dim obj1 As clsMilkSRNMCCDetail
            Dim objVSPChargeList As New List(Of clsMilkSRNVSpChargeDetail)
            Dim objVSP_Charge1 As clsMilkSRNVSpChargeDetail
            Dim objPriceChargeList As New List(Of clsMilkSRNPriceChargeDetail)
            Dim objPrice_Charge1 As clsMilkSRNPriceChargeDetail
            clsCommon.ProgressBarShow()
            For Each objtr As clsMilkRejectDetail In obj.Arr
                If objtr.Is_Return = 0 Then
                    objList = New List(Of clsMilkSRNMCCDetail)
                    objVSPChargeList = New List(Of clsMilkSRNVSpChargeDetail)
                    objPriceChargeList = New List(Of clsMilkSRNPriceChargeDetail)

                    Dim qry As String = ""
                    Dim dtVendor As DataTable = clsDBFuncationality.GetDataTable("select  TSPL_VENDOR_MASTER.Service_Charge_Type, coalesce(TSPL_VENDOR_MASTER.Rate_Head_Load,0) as Rate_Head_Load ,coalesce(TSPL_VENDOR_MASTER.Rate_Own_Asset,0) as Rate_Own_Asset,TSPL_VENDOR_MASTER.Service_Basis_Head_Load,TSPL_VENDOR_MASTER.Service_Basis_Own_Asset,TSPL_VENDOR_MASTER.EMP_Type,TSPL_VENDOR_MASTER.EMP_Fixed_Amount  ,TSPL_VENDOR_MASTER.Actual_charges_Slab   ,TSPL_VENDOR_MASTER.Actual_charges   ,TSPL_VENDOR_MASTER.Actual_charges_Slab2  ,TSPL_VENDOR_MASTER.Actual_charges2 ,TSPL_VENDOR_MASTER.Actual_charges_Slab3 ,TSPL_VENDOR_MASTER.Actual_charges3  ,TSPL_VENDOR_MASTER.Actual_charges_Slab4  ,TSPL_VENDOR_MASTER.Actual_charges4  ,TSPL_VENDOR_MASTER.Actual_charges_Slab5 ,TSPL_VENDOR_MASTER.Actual_charges5 from  TSPL_VENDOR_MASTER  where Vendor_Code='" + objtr.VSP_CODE + "'", trans)
                    If dtVendor Is Nothing OrElse dtVendor.Rows.Count <= 0 Then
                        Throw New Exception("VSP -" + objtr.VSP_CODE + " not found in master.")
                    End If
                    objHead = New clsMilkSRNMCC
                    objHead.Against_Reject_No = obj.DOC_CODE
                    objHead.DOC_DATE = obj.DOC_DATE
                    objHead.SHIFT = obj.SHIFT
                    objHead.COMM_PORT = ""
                    objHead.MCC_CODE = obj.MCC_CODE
                    objHead.SAMPLE_NO = objtr.SAMPLE_NO
                    objHead.VLC_DOC_CODE = ""
                    objHead.VEHICLE_CODE = objtr.VEHICLE_CODE
                    objHead.VLC_CODE = objtr.VLC_CODE
                    objHead.ROUTE_CODE = objtr.ROUTE_CODE
                    objHead.VSP_CODE = objtr.VSP_CODE
                    objHead.TransPorter = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Vendor_Code  from TSPL_Primary_Vehicle_Master where Vehicle_Code='" + objtr.VEHICLE_CODE + "'", trans))
                    If clsCommon.myLen(objHead.TransPorter) <= 0 Then
                        objHead.TransPorter = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Tanker_Transporter_Code from TSPL_TANKER_MASTER   where Tanker_No='" + objtr.VEHICLE_CODE + "'", trans))
                        If clsCommon.myLen(objHead.TransPorter) <= 0 Then
                            Throw New Exception("Please set the transport for vehicle no" + objtr.VEHICLE_CODE)
                        End If
                    End If

                    obj1 = New clsMilkSRNMCCDetail()
                    obj1.Item_CODE = objtr.Item_CODE
                    obj1.MILK_Qty = objtr.MILK_WEIGHT
                    obj1.ACC_Qty = objtr.ACC_WEIGHT_KG
                    obj1.FAT = objtr.FAT
                    obj1.SNF = objtr.SNF
                    obj1.MCC_CODE = obj.MCC_CODE
                    obj1.Correction_Factor = objtr.Conversion_Factor
                    obj1.RATE = objtr.RATE
                    obj1.UOM = objtr.UOM_Code
                    obj1.Price_Code = objtr.Price_Code
                    obj1.AMOUNT = objtr.Amount
                    obj1.Own_Asset_Rate = clsCommon.myCdbl(dtVendor.Rows(0)("Rate_Own_Asset"))


                    ''Reduce debit note amt from Amt and make EMP BASE Amt
                    Dim dblDebitNoteAmt As Decimal = 0
                    If clsCommon.CompairString(objtr.Defaulter, "VSP") = CompairStringResult.Equal Then
                        dblDebitNoteAmt = objtr.SNF_Deduction_Amount
                        If clsCommon.CompairString(objtr.Reject_Type, "Curd") = CompairStringResult.Equal Then
                            dblDebitNoteAmt += objtr.FAT_Deduction_Amount
                        End If
                    End If


                    Dim dblEMPBaseAmt As Decimal = obj1.AMOUNT - dblDebitNoteAmt

                    obj1.Commission = 0 ' because nature is always E and it is never C 
                    obj1.Commission_Amount = Math.Round(dblEMPBaseAmt * obj1.Commission / 100, 2)
                    If clsCommon.CompairString(clsCommon.myCstr(dtVendor.Rows(0)("EMP_Type")), "FP") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(dtVendor.Rows(0)("EMP_Type")), "FAFP") = CompairStringResult.Equal Then
                        obj1.Payment_Commission = clsCommon.myCdbl(dtVendor.Rows(0)("Actual_charges"))
                        If clsCommon.CompairString(clsCommon.myCstr(dtVendor.Rows(0)("Service_Charge_Type")), "%(Percentage)") = CompairStringResult.Equal Then
                            obj1.Emp_Amount = Math.Round(dblEMPBaseAmt * obj1.Payment_Commission / 100, 2)
                        ElseIf clsCommon.CompairString(clsCommon.myCstr(dtVendor.Rows(0)("Service_Charge_Type")), "Rate/Kg") = CompairStringResult.Equal Then
                            obj1.Emp_Amount = Math.Round(obj1.ACC_Qty * obj1.Payment_Commission, 2)
                        ElseIf clsCommon.CompairString(clsCommon.myCstr(dtVendor.Rows(0)("Service_Charge_Type")), "Rate/Ltr") = CompairStringResult.Equal Then
                            obj1.Emp_Amount = Math.Round(obj1.MILK_Qty * obj1.Payment_Commission, 2)
                        End If
                        If clsCommon.CompairString(clsCommon.myCstr(dtVendor.Rows(0)("EMP_Type")), "FAFP") = CompairStringResult.Equal Then
                            obj1.Emp_Amount += clsCommon.myCdbl(dtVendor.Rows(0)("EMP_Fixed_Amount"))
                        End If
                    ElseIf clsCommon.CompairString(clsCommon.myCstr(dtVendor.Rows(0)("EMP_Type")), "SWP") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(dtVendor.Rows(0)("EMP_Type")), "FASWP") = CompairStringResult.Equal Then
                        If clsCommon.CompairString(clsCommon.myCstr(dtVendor.Rows(0)("Service_Charge_Type")), "%(Percentage)") = CompairStringResult.Equal Then
                            If clsCommon.myCdbl(dtVendor.Rows(0)("Actual_charges_Slab5")) > 0 AndAlso dblEMPBaseAmt >= clsCommon.myCdbl(dtVendor.Rows(0)("Actual_charges_Slab5")) Then
                                obj1.Payment_Commission = clsCommon.myCdbl(dtVendor.Rows(0)("Actual_charges5"))
                            ElseIf clsCommon.myCdbl(dtVendor.Rows(0)("Actual_charges_Slab4")) > 0 AndAlso dblEMPBaseAmt >= clsCommon.myCdbl(dtVendor.Rows(0)("Actual_charges_Slab4")) Then
                                obj1.Payment_Commission = clsCommon.myCdbl(dtVendor.Rows(0)("Actual_charges4"))
                            ElseIf clsCommon.myCdbl(dtVendor.Rows(0)("Actual_charges_Slab3")) > 0 AndAlso dblEMPBaseAmt >= clsCommon.myCdbl(dtVendor.Rows(0)("Actual_charges_Slab3")) Then
                                obj1.Payment_Commission = clsCommon.myCdbl(dtVendor.Rows(0)("Actual_charges3"))
                            ElseIf clsCommon.myCdbl(dtVendor.Rows(0)("Actual_charges_Slab2")) > 0 AndAlso dblEMPBaseAmt >= clsCommon.myCdbl(dtVendor.Rows(0)("Actual_charges_Slab2")) Then
                                obj1.Payment_Commission = clsCommon.myCdbl(dtVendor.Rows(0)("Actual_charges2"))
                            Else
                                obj1.Payment_Commission = clsCommon.myCdbl(dtVendor.Rows(0)("Actual_charges"))
                            End If
                            obj1.Emp_Amount = Math.Round(dblEMPBaseAmt * obj1.Payment_Commission / 100, 2)
                        ElseIf clsCommon.CompairString(clsCommon.myCstr(dtVendor.Rows(0)("Service_Charge_Type")), "Rate/Kg") = CompairStringResult.Equal Then
                            If clsCommon.myCdbl(dtVendor.Rows(0)("Actual_charges_Slab5")) > 0 AndAlso obj1.ACC_Qty >= clsCommon.myCdbl(dtVendor.Rows(0)("Actual_charges_Slab5")) Then
                                obj1.Payment_Commission = clsCommon.myCdbl(dtVendor.Rows(0)("Actual_charges5"))
                            ElseIf clsCommon.myCdbl(dtVendor.Rows(0)("Actual_charges_Slab4")) > 0 AndAlso obj1.ACC_Qty >= clsCommon.myCdbl(dtVendor.Rows(0)("Actual_charges_Slab4")) Then
                                obj1.Payment_Commission = clsCommon.myCdbl(dtVendor.Rows(0)("Actual_charges4"))
                            ElseIf clsCommon.myCdbl(dtVendor.Rows(0)("Actual_charges_Slab3")) > 0 AndAlso obj1.ACC_Qty >= clsCommon.myCdbl(dtVendor.Rows(0)("Actual_charges_Slab3")) Then
                                obj1.Payment_Commission = clsCommon.myCdbl(dtVendor.Rows(0)("Actual_charges3"))
                            ElseIf clsCommon.myCdbl(dtVendor.Rows(0)("Actual_charges_Slab2")) > 0 AndAlso obj1.ACC_Qty >= clsCommon.myCdbl(dtVendor.Rows(0)("Actual_charges_Slab2")) Then
                                obj1.Payment_Commission = clsCommon.myCdbl(dtVendor.Rows(0)("Actual_charges2"))
                            Else
                                obj1.Payment_Commission = clsCommon.myCdbl(dtVendor.Rows(0)("Actual_charges"))
                            End If
                            obj1.Emp_Amount = Math.Round(obj1.ACC_Qty * obj1.Payment_Commission, 2)
                        ElseIf clsCommon.CompairString(clsCommon.myCstr(dtVendor.Rows(0)("Service_Charge_Type")), "Rate/Ltr") = CompairStringResult.Equal Then
                            If clsCommon.myCdbl(dtVendor.Rows(0)("Actual_charges_Slab5")) > 0 AndAlso obj1.MILK_Qty >= clsCommon.myCdbl(dtVendor.Rows(0)("Actual_charges_Slab5")) Then
                                obj1.Payment_Commission = clsCommon.myCdbl(dtVendor.Rows(0)("Actual_charges5"))
                            ElseIf clsCommon.myCdbl(dtVendor.Rows(0)("Actual_charges_Slab4")) > 0 AndAlso obj1.MILK_Qty >= clsCommon.myCdbl(dtVendor.Rows(0)("Actual_charges_Slab4")) Then
                                obj1.Payment_Commission = clsCommon.myCdbl(dtVendor.Rows(0)("Actual_charges4"))
                            ElseIf clsCommon.myCdbl(dtVendor.Rows(0)("Actual_charges_Slab3")) > 0 AndAlso obj1.MILK_Qty >= clsCommon.myCdbl(dtVendor.Rows(0)("Actual_charges_Slab3")) Then
                                obj1.Payment_Commission = clsCommon.myCdbl(dtVendor.Rows(0)("Actual_charges3"))
                            ElseIf clsCommon.myCdbl(dtVendor.Rows(0)("Actual_charges_Slab2")) > 0 AndAlso obj1.MILK_Qty >= clsCommon.myCdbl(dtVendor.Rows(0)("Actual_charges_Slab2")) Then
                                obj1.Payment_Commission = clsCommon.myCdbl(dtVendor.Rows(0)("Actual_charges2"))
                            Else
                                obj1.Payment_Commission = clsCommon.myCdbl(dtVendor.Rows(0)("Actual_charges"))
                            End If
                            obj1.Emp_Amount = Math.Round(obj1.MILK_Qty * obj1.Payment_Commission, 2)
                        End If
                        If clsCommon.CompairString(clsCommon.myCstr(dtVendor.Rows(0)("EMP_Type")), "FASWP") = CompairStringResult.Equal Then
                            obj1.Emp_Amount += clsCommon.myCdbl(dtVendor.Rows(0)("EMP_Fixed_Amount"))
                        End If
                    Else
                        Throw New Exception("EMP Type is Not a valid ")
                    End If
                    obj1.Service_Charge_Type = clsCommon.myCstr(dtVendor.Rows(0)("Service_Charge_Type"))
                    Dim MinimumQtyForHeadLoad As Decimal = clsCommon.myCDecimal(clsFixedParameter.GetData(clsFixedParameterType.MinimumQtyForHeadLoad, clsFixedParameterCode.MinimumQtyForHeadLoad, trans))

                    Dim objHeadLoad As New clsHeadLoadDCS()
                    objHeadLoad = clsHeadLoadDCS.GetDcsData(obj1.VlC_Code, obj.DOC_DATE, trans)
                    obj1.Head_Load_Rate = clsCommon.myCdbl(objHeadLoad.Head_Load_Rate)


                    If clsCommon.CompairString(clsCommon.myCstr(objHeadLoad.Head_Load_Basis), "K") = CompairStringResult.Equal Then
                        If obj1.ACC_Qty >= MinimumQtyForHeadLoad Then
                            obj1.Head_Load_Amount = Math.Round(obj1.ACC_Qty * objHeadLoad.Head_Load_Rate, 2)
                        End If
                    ElseIf clsCommon.CompairString(clsCommon.myCstr(objHeadLoad.Head_Load_Basis), "L") = CompairStringResult.Equal Then
                        If objtr.ACC_WEIGHT_LTR >= MinimumQtyForHeadLoad Then
                            obj1.Head_Load_Amount = Math.Round(objtr.ACC_WEIGHT_LTR * objHeadLoad.Head_Load_Rate, 2)
                        End If
                    End If
                    obj1.Head_Load_Type = clsCommon.myCstr(objHeadLoad.Head_Load_Basis)
                    If clsCommon.CompairString(clsCommon.myCstr(dtVendor.Rows(0)("Service_Basis_Own_Asset")), "K") = CompairStringResult.Equal Then
                        obj1.Own_Asset_Amount = Math.Round(obj1.ACC_Qty * obj1.Own_Asset_Rate, 2)
                    ElseIf clsCommon.CompairString(clsCommon.myCstr(dtVendor.Rows(0)("Service_Basis_Own_Asset")), "L") = CompairStringResult.Equal Then
                        obj1.Own_Asset_Amount = Math.Round(obj1.MILK_Qty * obj1.Own_Asset_Rate, 2)
                    End If
                    obj1.Own_Asset_Type = clsCommon.myCstr(dtVendor.Rows(0)("Service_Basis_Own_Asset"))
                    obj1.NET_AMOUNT = Math.Round(obj1.AMOUNT + obj1.Emp_Amount, 2)
                    obj1.COMM_PORT = ""
                    objList.Add(obj1)
                    Dim DtVSPChargeDetail As DataTable = clsDBFuncationality.GetDataTable("SELECT * FROM  TSPL_MCC_VSP_ChargeCategory_MAPPING where Vsp_Code='" & objHead.VSP_CODE & "' ", trans)
                    If DtVSPChargeDetail IsNot Nothing AndAlso DtVSPChargeDetail.Rows.Count > 0 Then
                        For Each row_VSP_Charge As DataRow In DtVSPChargeDetail.Rows
                            objVSP_Charge1 = New clsMilkSRNVSpChargeDetail()
                            objVSP_Charge1.Vsp_Code = clsCommon.myCstr(objHead.VSP_CODE)
                            objVSP_Charge1.Vlc_Doc_Code = objtr.SAMPLE_NO
                            objVSP_Charge1.Charge_Code = clsCommon.myCstr(row_VSP_Charge("Charge_Code"))
                            objVSP_Charge1.Charge_Rate = clsCommon.myCstr(row_VSP_Charge("Rate"))
                            objVSP_Charge1.Service_Type = clsCommon.myCstr(dtVendor.Rows(0)("Service_Charge_Type"))
                            If clsCommon.CompairString(objVSP_Charge1.Service_Type, "%(Percentage)") = CompairStringResult.Equal Then
                                objVSP_Charge1.AMOUNT = Math.Round(obj1.AMOUNT * objVSP_Charge1.Charge_Rate / 100, 2)
                            ElseIf clsCommon.CompairString(objVSP_Charge1.Service_Type, "Rate/Kg") = CompairStringResult.Equal Then
                                objVSP_Charge1.AMOUNT = Math.Round(obj1.ACC_Qty * objVSP_Charge1.Charge_Rate, 2)
                            ElseIf clsCommon.CompairString(objVSP_Charge1.Service_Type, "Rate/Ltr") = CompairStringResult.Equal And clsCommon.CompairString(dtVendor.Rows(0)("UOM_Code"), "LTR") = CompairStringResult.Equal Then
                                objVSP_Charge1.AMOUNT = Math.Round(obj1.MILK_Qty * objVSP_Charge1.Charge_Rate, 2)
                            End If
                            objVSPChargeList.Add(objVSP_Charge1)
                        Next
                    End If
                    Dim DtPriceChargeDetail As DataTable = clsDBFuncationality.GetDataTable("SELECT * FROM  TSPL_FAT_SNF_UPLOADER_Chart_Detail where Price_Code='" & obj1.Price_Code & "' ", trans)
                    For Each row_Price_Charge As DataRow In DtPriceChargeDetail.Rows
                        objPrice_Charge1 = New clsMilkSRNPriceChargeDetail()
                        objPrice_Charge1.Price_Code = clsCommon.myCstr(obj1.Price_Code)
                        objPrice_Charge1.Vlc_Doc_Code = objtr.SAMPLE_NO
                        objPrice_Charge1.Charge_Code = clsCommon.myCstr(row_Price_Charge("Charge_Code"))
                        objPrice_Charge1.Charge_Rate = clsCommon.myCstr(row_Price_Charge("Rate"))
                        objPrice_Charge1.Service_type = clsCommon.myCstr(dtVendor.Rows(0)("Service_Charge_Type"))
                        If clsCommon.CompairString(objPrice_Charge1.Service_type, "%(Percentage)") = CompairStringResult.Equal Then
                            objPrice_Charge1.AMOUNT = Math.Round(obj1.AMOUNT * objPrice_Charge1.Charge_Rate / 100, 2)
                        ElseIf clsCommon.CompairString(objPrice_Charge1.Service_type, "Rate/Kg") = CompairStringResult.Equal Then
                            objPrice_Charge1.AMOUNT = Math.Round(obj1.ACC_Qty * objPrice_Charge1.Charge_Rate, 2)
                        ElseIf clsCommon.CompairString(objPrice_Charge1.Service_type, "Rate/Ltr") = CompairStringResult.Equal And clsCommon.CompairString(dtVendor.Rows(0)("UOM_Code"), "LTR") = CompairStringResult.Equal Then
                            objPrice_Charge1.AMOUNT = Math.Round(obj1.MILK_Qty * objPrice_Charge1.Charge_Rate, 2)
                        End If
                        objPriceChargeList.Add(objPrice_Charge1)
                    Next
                    If Not clsMilkSRNMCC.SaveData(objHead, objList, objVSPChargeList, objPriceChargeList, trans) Then
                        Return False
                    Else
                        objHead = Nothing
                        objList = Nothing
                        obj1 = Nothing
                        objVSPChargeList = Nothing
                        objVSP_Charge1 = Nothing
                        objPriceChargeList = Nothing
                        objPrice_Charge1 = Nothing
                    End If
                End If
            Next
            clsCommon.ProgressBarHide()
        Catch ex As Exception
            clsCommon.ProgressBarHide()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function GetDocCode(ByVal DocDate As Date, ByVal MCC_Code As String, ByVal Shift As String, Optional ByVal trans As SqlTransaction = Nothing) As String
        Dim qry As String
        qry = "SELECT DOC_CODE FROM TSPL_MILK_REJECT_HEAD WHERE MCC_CODE='" & MCC_Code & "' AND convert(date,DOC_DATE,103)='" & clsCommon.GetPrintDate(DocDate, "dd/MMM/yyyy") & "' AND SHIFT='" & Shift & "'"
        Dim dt As DataTable
        dt = clsDBFuncationality.GetDataTable(qry, trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            Return dt.Rows(0).Item("DOC_CODE")
        Else
            Return ""
        End If
    End Function

    Public Shared Function GetPost(ByVal DocDate As Date, ByVal MCC_Code As String, ByVal Shift As String, Optional ByVal trans As SqlTransaction = Nothing) As Boolean
        Dim qry As String = "SELECT Posted FROM TSPL_MILK_REJECT_HEAD WHERE MCC_CODE='" & MCC_Code & "' AND  convert(date, DOC_DATE,103)='" & clsCommon.GetPrintDate(DocDate, "dd/MMM/yyyy") & "' AND SHIFT='" & Shift & "' and Posted=1"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            Return True
        End If
        qry = "SELECT 1 FROM TSPL_MILK_Shift_End_HEAD WHERE MCC_CODE='" & MCC_Code & "' AND  convert(date, DOC_DATE,103)='" & clsCommon.GetPrintDate(DocDate, "dd/MMM/yyyy") & "' AND SHIFT='" & Shift & "'"
        dt = clsDBFuncationality.GetDataTable(qry, trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            Return True
        End If
        Return False
    End Function

    Public Shared Function GetConversion_factor(ByVal FromUOM As String, ByVal ToUOM As String, ByVal trans As SqlTransaction)
        Dim conv_fac As Double = 0
        Try
            Dim dt As DataTable = clsDBFuncationality.GetDataTable("select coalesce(Contained_Qty,1) as Contained_Qty from  TSPL_WEIGHT_CONVERSION where Container_UOM='" & FromUOM & "' and Contained_UOM='" + ToUOM + "'  and Product_Type in ('MI','ALL') order by Product_Type desc", trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                conv_fac = clsCommon.myCdbl(dt.Rows(0)("Contained_Qty"))
            Else
                dt = clsDBFuncationality.GetDataTable("select coalesce(Contained_Qty,1) as Contained_Qty from  TSPL_WEIGHT_CONVERSION where Container_UOM='" & ToUOM & "' and Contained_UOM='" + FromUOM + "'  and Product_Type in ('MI','ALL') order by Product_Type desc", trans)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    conv_fac = 1 / clsCommon.myCdbl(dt.Rows(0)("Contained_Qty"))
                Else
                    Throw New Exception("Please porvide Weight conversion from " + FromUOM + " To " + ToUOM)
                End If
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return conv_fac
    End Function

    Public Shared Sub CreateDebitNoteForRejection(ByVal FromDate As Date, ByVal ToDate As Date, ByVal strRejectNo As String, ByVal trans As SqlTransaction)
        Dim settAlwaysVSPDefaulter As Boolean = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AlwaysVSPDefaulter, clsFixedParameterCode.AlwaysVSPDefaulter, trans)) = 1)
        Dim settSelectMilkRejectDefaulterManually As Boolean = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.SelectMilkRejectDefaulterManually, clsFixedParameterCode.SelectMilkRejectDefaulterManually, trans)) = 1)
        Dim obj As clsMilkRejectHead = clsMilkRejectHead.GetData(strRejectNo, NavigatorType.Current, trans)
        If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
            Dim dblSetFATPer As Double = 0
            Dim dblSetSNFPer As Double = 0
            Dim dblSetGraceMinutes As Double = 0
            ''If You chagne anything is Amount of debit note please change in 
            For Each objtr As clsMilkRejectDetail In obj.Arr
                Dim cond As Boolean = False
                If objtr.Is_Return = 0 Then
                    If objtr.FAT_Deduction_Amount > 0 OrElse objtr.SNF_Deduction_Amount > 0 Then
                        cond = True
                    End If
                    If settAlwaysVSPDefaulter Then
                        cond = False
                        If settSelectMilkRejectDefaulterManually Then
                            If clsCommon.CompairString(objtr.Defaulter, "Transporter") = CompairStringResult.Equal Then
                                cond = True
                            End If
                        End If
                    End If
                ElseIf objtr.Is_Return = 1 OrElse objtr.Is_Return = 2 OrElse objtr.Is_Return = 3 Then
                    If objtr.Amount > 0 Then
                        cond = True
                    End If
                End If
                If cond Then
                    Dim strVendor As String = objtr.VSP_CODE
                    Dim dblAmt As Decimal = objtr.SNF_Deduction_Amount
                    Dim dt As DataTable
                    Dim objVendorInvHead As New clsVedorInvoiceHead()
                    Dim objVendorInvDetail As New clsVedorInvoiceDetail()

                    objVendorInvHead.isDeduction = 1

                    Dim strDetailGLAccount As String = ""

                    If objtr.Is_Return = 0 Then
                        If clsCommon.CompairString(objtr.Defaulter, "Company") = CompairStringResult.Equal Then
                            Continue For
                        ElseIf clsCommon.CompairString(objtr.Defaulter, "Transporter") = CompairStringResult.Equal Then
                            strVendor = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Vendor_Code from TSPL_Primary_Vehicle_Master where Vehicle_Code='" + objtr.VEHICLE_CODE + "'", trans))
                            objVendorInvHead.isDeduction = 0
                            Dim dtDed As DataTable = clsDBFuncationality.GetDataTable("select code,GL_Account_Code from TSPL_DEDUCTION_MASTER  where Is_Default_Transporter_Deduction=1", trans)
                            If dtDed Is Nothing OrElse dtDed.Rows.Count <= 0 Then
                                Throw New Exception("Please set default Tranporter deduction code")
                            End If
                            objVendorInvDetail.DeductionCode = clsCommon.myCstr(dtDed.Rows(0)("code"))
                            strDetailGLAccount = clsCommon.myCstr(dtDed.Rows(0)("GL_Account_Code"))
                        ElseIf clsCommon.CompairString(objtr.Defaulter, "VSP") = CompairStringResult.Equal Then
                            ''Set On above.
                            Dim dtDed As DataTable = clsDBFuncationality.GetDataTable("select code,GL_Account_Code from TSPL_DEDUCTION_MASTER  where Is_Default_VSP_Deduction=1", trans)
                            If dtDed Is Nothing OrElse dtDed.Rows.Count <= 0 Then
                                Throw New Exception("Please set default VSP deduction code")
                            End If
                            objVendorInvDetail.DeductionCode = clsCommon.myCstr(dtDed.Rows(0)("code"))
                            strDetailGLAccount = clsCommon.myCstr(dtDed.Rows(0)("GL_Account_Code"))
                        End If
                        If clsCommon.CompairString(objtr.Reject_Type, "Curd") = CompairStringResult.Equal Then
                            dblAmt += objtr.FAT_Deduction_Amount
                        End If
                    ElseIf objtr.Is_Return = 1 Then ''For Return Type Return
                        dblAmt = objtr.Amount
                        Dim dtDed As DataTable = clsDBFuncationality.GetDataTable("select code,GL_Account_Code from TSPL_DEDUCTION_MASTER  where Is_Default_VSP_Deduction=1", trans)
                        If dtDed Is Nothing OrElse dtDed.Rows.Count <= 0 Then
                            Throw New Exception("Please set default VSP deduction code")
                        End If
                        objVendorInvDetail.DeductionCode = clsCommon.myCstr(dtDed.Rows(0)("code"))
                        strDetailGLAccount = clsCommon.myCstr(dtDed.Rows(0)("GL_Account_Code"))
                    ElseIf objtr.Is_Return = 3 Then ''For Return Type Return COB
                        dblAmt = objtr.Amount
                        Dim dtDed As DataTable = clsDBFuncationality.GetDataTable("select code,GL_Account_Code from TSPL_DEDUCTION_MASTER  where Is_Default_VSP_Deduction=1", trans)
                        If dtDed Is Nothing OrElse dtDed.Rows.Count <= 0 Then
                            Throw New Exception("Please set default VSP deduction code")
                        End If
                        objVendorInvDetail.DeductionCode = clsCommon.myCstr(dtDed.Rows(0)("code"))
                        strDetailGLAccount = clsCommon.myCstr(dtDed.Rows(0)("GL_Account_Code"))
                    ElseIf objtr.Is_Return = 2 Then '' for Return Type Drain
                        dblAmt = objtr.Amount
                        Dim dtDed As DataTable = clsDBFuncationality.GetDataTable("select code,GL_Account_Code from TSPL_DEDUCTION_MASTER  where Is_Default_VSP_Deduction=1", trans)
                        If dtDed Is Nothing OrElse dtDed.Rows.Count <= 0 Then
                            Throw New Exception("Please set default VSP deduction code")
                        End If
                        objVendorInvDetail.DeductionCode = clsCommon.myCstr(dtDed.Rows(0)("code"))
                        strDetailGLAccount = clsCommon.myCstr(dtDed.Rows(0)("GL_Account_Code"))
                    Else
                        Throw New Exception("Return Type is not valid")
                    End If

                    If clsCommon.myLen(strDetailGLAccount) <= 0 Then
                        Throw New Exception("Please set GL Account for Deduction :" + objVendorInvDetail.DeductionCode)
                    End If
                    'objVendorInvHead.Document_No = txtDocNo.Value'ToBeGenerated
                    objVendorInvHead.Invoice_Entry_Date = clsCommon.GetPrintDate(obj.DOC_DATE, "dd/MMM/yyyy")
                    objVendorInvHead.Vendor_Code = strVendor
                    objVendorInvHead.Vendor_Name = clsVendorMaster.GetName(strVendor, trans)
                    objVendorInvHead.Vendor_Invoice_No = "" ''No Need to send vendor invoice no because it is of debit note type
                    objVendorInvHead.Invoice_Type = "AP"
                    objVendorInvHead.Vendor_Invoice_Date = obj.DOC_DATE
                    objVendorInvHead.loc_code = clsLocation.GetSegmentCode(obj.MCC_CODE, trans) 'obj.MCC_CODE
                    'objVendorInvHead.Irregular_loc_code = obj.Irregular_MCC_CODE
                    objVendorInvHead.Description = "AP Debit Note Against Rejection Entry: " & obj.DOC_CODE & " .Sample No : " & clsCommon.myCstr(objtr.SAMPLE_NO) & ".VSP : " & objVendorInvHead.Vendor_Name & "(" + objVendorInvHead.Vendor_Code + ")"
                    'objVendorInvHead.PROJECT_ID = 1 'obj.PROJECT_ID
                    objVendorInvHead.Account_Set = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  Vendor_Account from TSPL_VENDOR_MASTER where Vendor_Code ='" + objVendorInvHead.Vendor_Code + "'", trans))
                    If (clsCommon.myLen(objVendorInvHead.Account_Set) < 0) Then
                        Throw New Exception("Please set the vendor Account Set For Vendor : " + objVendorInvHead.Vendor_Name)
                    End If

                    objVendorInvHead.Document_Type = "D" ''For Purchase Invoice Type
                    ''objVendorInvHead.PO_Number = obj.p

                    '' ''added by priti
                    objVendorInvHead.RefDocType = "MILK-REJ"
                    objVendorInvHead.RefDocNo = obj.DOC_CODE
                    objVendorInvHead.Ref_SNo = objtr.SAMPLE_NO
                    '' '' priti ends here
                    'objVendorInvHead.Order_No = txtOrderNo.Text
                    ' objVendorInvHead.Total_Tax = 0

                    objVendorInvHead.On_Hold = False
                    'Dim srndate As String = ""
                    'Dim srncode As String = ""
                    'Dim Vlc_Code As String = ""
                    'Dim Vlc_Name As String = ""
                    'For Each objTr As clsMilkPurchaseInvoiceMCCDetail In obj.ObjList
                    '    If clsCommon.myLen(objTr.SRN_CODE) > 0 Then
                    '        Dim query As String = "select doc_date,vd.VLC_Code,VLC_Name from TSPL_Milk_SRN_HEAD sh inner join TSPL_VLC_MASTER_HEAD vd on sh.VLC_CODE=vd.VLC_Code where DOc_Code ='" + objTr.SRN_CODE + "' "
                    '        Dim Dt_SRN As DataTable = clsDBFuncationality.GetDataTable(query, trans)
                    '        srndate = IIf(srndate = "", clsCommon.myCDate(CStr(Dt_SRN.Rows(0).Item("Doc_Date")), "dd/MMM/yyyy"), srndate & "," & clsCommon.myCDate(CStr(Dt_SRN.Rows(0).Item("Doc_Date")), "dd/MMM/yyyy"))
                    '        srncode = IIf(srncode = "", objTr.SRN_CODE, srncode & "," & objTr.SRN_CODE)
                    '        Vlc_Code = IIf(Vlc_Code = "", Dt_SRN.Rows(0).Item("VLC_Code").ToString, Vlc_Code & "," & Dt_SRN.Rows(0).Item("VLC_Code").ToString)
                    '        Vlc_Name = IIf(Vlc_Name = "", Dt_SRN.Rows(0).Item("VLC_Name").ToString, Vlc_Name & "," & Dt_SRN.Rows(0).Item("VLC_name").ToString)
                    '    End If
                    'Next



                    'objVendorInvHead.Description = "VSP : " + obj.VSP_CODE + "/" + vendor_name + "VLC : " + Vlc_Code + "/" + Vlc_Name + " .Against PI Invoice No " + obj.DOC_CODE + "-" + srncode + "-" + srndate
                    'objVendorInvHead.Tax_Calculation_Type = Nothing
                    'objVendorInvHead.Tax_Group = Nothing
                    'If (clsCommon.myLen(obj.TAX1) > 0) Then
                    '    objVendorInvHead.TAX1 = obj.TAX1
                    '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX1, trans) Then
                    '        objVendorInvHead.TAX1_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX1, trans)
                    '        objVendorInvHead.TAX1_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX1_GLAC, obj.MCC_CODE, trans)
                    '    End If
                    '    objVendorInvHead.TAX1_Rate = obj.TAX1_Rate
                    '    objVendorInvHead.Tax1_BAmount = obj.TAX1_Base_Amt
                    '    objVendorInvHead.TAX1_Amt = obj.TAX1_Amt
                    'End If
                    'If (clsCommon.myLen(obj.TAX2) > 0) Then
                    '    objVendorInvHead.TAX2 = obj.TAX2
                    '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX2, trans) Then
                    '        objVendorInvHead.TAX2_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX2, trans)
                    '        objVendorInvHead.TAX2_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX2_GLAC, obj.MCC_CODE, trans)
                    '    End If
                    '    objVendorInvHead.TAX2_Rate = obj.TAX2_Rate
                    '    objVendorInvHead.Tax2_BAmount = obj.TAX2_Base_Amt
                    '    objVendorInvHead.TAX2_Amt = obj.TAX2_Amt
                    'End If
                    'If (clsCommon.myLen(obj.TAX3) > 0) Then
                    '    objVendorInvHead.TAX3 = obj.TAX3
                    '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX3, trans) Then
                    '        objVendorInvHead.TAX3_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX3, trans)
                    '        objVendorInvHead.TAX3_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX3_GLAC, obj.MCC_CODE, trans)
                    '    End If
                    '    objVendorInvHead.TAX3_Rate = obj.TAX3_Rate
                    '    objVendorInvHead.Tax3_BAmount = obj.TAX3_Base_Amt
                    '    objVendorInvHead.TAX3_Amt = obj.TAX3_Amt
                    'End If
                    'If (clsCommon.myLen(obj.TAX4) > 0) Then
                    '    objVendorInvHead.TAX4 = obj.TAX4
                    '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX4, trans) Then
                    '        objVendorInvHead.TAX4_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX4, trans)
                    '        objVendorInvHead.TAX4_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX4_GLAC, obj.MCC_CODE, trans)
                    '    End If
                    '    objVendorInvHead.TAX4_Rate = obj.TAX4_Rate
                    '    objVendorInvHead.Tax4_BAmount = obj.TAX4_Base_Amt
                    '    objVendorInvHead.TAX4_Amt = obj.TAX4_Amt
                    'End If
                    'If (clsCommon.myLen(obj.TAX5) > 0) Then
                    '    objVendorInvHead.TAX5 = obj.TAX5
                    '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX5, trans) Then
                    '        objVendorInvHead.TAX5_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX5, trans)
                    '        objVendorInvHead.TAX5_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX5_GLAC, obj.MCC_CODE, trans)

                    '    End If
                    '    objVendorInvHead.TAX5_Rate = obj.TAX5_Rate
                    '    objVendorInvHead.Tax5_BAmount = obj.TAX5_Base_Amt
                    '    objVendorInvHead.TAX5_Amt = obj.TAX5_Amt
                    'End If
                    'If (clsCommon.myLen(obj.TAX6) > 0) Then
                    '    objVendorInvHead.TAX6 = obj.TAX6
                    '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX6, trans) Then
                    '        objVendorInvHead.TAX6_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX6, trans)
                    '        objVendorInvHead.TAX6_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX6_GLAC, obj.MCC_CODE, trans)
                    '    End If
                    '    objVendorInvHead.TAX6_Rate = obj.TAX6_Rate
                    '    objVendorInvHead.Tax6_BAmount = obj.TAX6_Base_Amt
                    '    objVendorInvHead.TAX6_Amt = obj.TAX6_Amt
                    'End If
                    'If (clsCommon.myLen(obj.TAX7) > 0) Then
                    '    objVendorInvHead.TAX7 = obj.TAX7
                    '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX7, trans) Then
                    '        objVendorInvHead.TAX7_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX7, trans)
                    '        objVendorInvHead.TAX7_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX7_GLAC, obj.MCC_CODE, trans)

                    '    End If
                    '    objVendorInvHead.TAX7_Rate = obj.TAX7_Rate
                    '    objVendorInvHead.Tax7_BAmount = obj.TAX7_Base_Amt
                    '    objVendorInvHead.TAX7_Amt = obj.TAX7_Amt
                    'End If
                    'If (clsCommon.myLen(obj.TAX8) > 0) Then
                    '    objVendorInvHead.TAX8 = obj.TAX8
                    '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX8, trans) Then
                    '        objVendorInvHead.TAX8_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX8, trans)
                    '        objVendorInvHead.TAX8_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX8_GLAC, obj.MCC_CODE, trans)
                    '    End If
                    '    objVendorInvHead.TAX8_Rate = obj.TAX8_Rate
                    '    objVendorInvHead.Tax8_BAmount = obj.TAX8_Base_Amt
                    '    objVendorInvHead.TAX8_Amt = obj.TAX8_Amt
                    'End If
                    'If (clsCommon.myLen(obj.TAX9) > 0) Then
                    '    objVendorInvHead.TAX9 = obj.TAX9
                    '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX9, trans) Then
                    '        objVendorInvHead.TAX9_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX9, trans)
                    '        objVendorInvHead.TAX9_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX9_GLAC, obj.MCC_CODE, trans)
                    '    End If
                    '    objVendorInvHead.TAX9_Rate = obj.TAX9_Rate
                    '    objVendorInvHead.Tax9_BAmount = obj.TAX9_Base_Amt
                    '    objVendorInvHead.TAX9_Amt = obj.TAX9_Amt
                    'End If
                    'If (clsCommon.myLen(obj.TAX10) > 0) Then
                    '    objVendorInvHead.TAX10 = obj.TAX10
                    '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX10, trans) Then
                    '        objVendorInvHead.TAX10_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX10, trans)
                    '        objVendorInvHead.TAX10_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX10_GLAC, obj.MCC_CODE, trans)
                    '    End If
                    '    objVendorInvHead.TAX10_Rate = obj.TAX10_Rate
                    '    objVendorInvHead.Tax10_BAmount = obj.TAX10_Base_Amt
                    '    objVendorInvHead.TAX10_Amt = obj.TAX10_Amt
                    'End If

                    'objVendorInvHead.Terms_Code = obj.Terms_Code
                    'objVendorInvHead.Terms_Description = obj.TermsName
                    objVendorInvHead.Due_Date = obj.DOC_DATE

                    'objVendorInvHead.Against_POInvoice_No = obj.DOC_CODE
                    'objVendorInvHead.Against_MillkPurchaseInvoice_No = obj.DOC_CODE

                    dt = clsDBFuncationality.GetDataTable("select Acct_Set_Code,Payable_Account,Discount_Account,Deduction_ACCOUNT from TSPL_VENDOR_ACCOUNT_SET  where Acct_Set_Code='" + objVendorInvHead.Account_Set + "'", trans)
                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                        objVendorInvHead.Vendor_Control_AC = clsCommon.myCstr(dt.Rows(0)("Payable_Account"))
                        objVendorInvHead.Vendor_Control_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.Vendor_Control_AC, obj.MCC_CODE, trans)
                        If clsCommon.myCdbl(objVendorInvHead.Discount_Amount) > 0 Then
                            objVendorInvHead.Discount_GL_AC = clsCommon.myCstr(dt.Rows(0)("Discount_Account"))
                            objVendorInvHead.Discount_GL_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.Discount_GL_AC, obj.MCC_CODE, trans)
                        End If
                    End If
                    If clsCommon.myLen(objVendorInvHead.Vendor_Control_AC) <= 0 Then
                        Throw New Exception("Please set the vendor payable Account")
                    End If

                    'objVendorInvHead.Total_Add_Charge = obj.Total_Add_Charge

                    'objVendorInvHead.Add_Charge_Code1 = obj.Add_Charge_Code1
                    'objVendorInvHead.Add_Charge_Name1 = obj.Add_Charge_Name1
                    'objVendorInvHead.Add_Charge_Amt1 = obj.Add_Charge_Amt1

                    'objVendorInvHead.Add_Charge_Code2 = obj.Add_Charge_Code2
                    'objVendorInvHead.Add_Charge_Name2 = obj.Add_Charge_Name2
                    'objVendorInvHead.Add_Charge_Amt2 = obj.Add_Charge_Amt2

                    'objVendorInvHead.Add_Charge_Code3 = obj.Add_Charge_Code3
                    'objVendorInvHead.Add_Charge_Name3 = obj.Add_Charge_Name3
                    'objVendorInvHead.Add_Charge_Amt3 = obj.Add_Charge_Amt3

                    'objVendorInvHead.Add_Charge_Code4 = obj.Add_Charge_Code4
                    'objVendorInvHead.Add_Charge_Name4 = obj.Add_Charge_Name4
                    'objVendorInvHead.Add_Charge_Amt4 = obj.Add_Charge_Amt4

                    'objVendorInvHead.Add_Charge_Code5 = obj.Add_Charge_Code5
                    'objVendorInvHead.Add_Charge_Name5 = obj.Add_Charge_Name5
                    'objVendorInvHead.Add_Charge_Amt5 = obj.Add_Charge_Amt5

                    'objVendorInvHead.Add_Charge_Code6 = obj.Add_Charge_Code6
                    'objVendorInvHead.Add_Charge_Name6 = obj.Add_Charge_Name6
                    'objVendorInvHead.Add_Charge_Amt6 = obj.Add_Charge_Amt6

                    'objVendorInvHead.Add_Charge_Code7 = obj.Add_Charge_Code7
                    'objVendorInvHead.Add_Charge_Name7 = obj.Add_Charge_Name7
                    'objVendorInvHead.Add_Charge_Amt7 = obj.Add_Charge_Amt7

                    'objVendorInvHead.Add_Charge_Code8 = obj.Add_Charge_Code8
                    'objVendorInvHead.Add_Charge_Name8 = obj.Add_Charge_Name8
                    'objVendorInvHead.Add_Charge_Amt8 = obj.Add_Charge_Amt8

                    'objVendorInvHead.Add_Charge_Code9 = obj.Add_Charge_Code9
                    'objVendorInvHead.Add_Charge_Name9 = obj.Add_Charge_Name9
                    'objVendorInvHead.Add_Charge_Amt9 = obj.Add_Charge_Amt9

                    'objVendorInvHead.Add_Charge_Code10 = obj.Add_Charge_Code10
                    'objVendorInvHead.Add_Charge_Name10 = obj.Add_Charge_Name10
                    'objVendorInvHead.Add_Charge_Amt10 = obj.Add_Charge_Amt10


                    objVendorInvHead.Arr = New List(Of clsVedorInvoiceDetail)
                    Dim ii As Integer = 0
                    Dim isFirstTime As Boolean = True
                    ' Dim strFirstItemCode As String = GetFirstItemCode(obj.ObjList)
                    'objVendorInvHead.Empty_Amount = obj.Tot_Empty_Amount
                    objVendorInvHead.Total_Landed_Amt = 0

                    objVendorInvHead.ArrAssetEMI = New List(Of clsAPInvoiceAssetEMIDetails)()



                    ''Set AP Invvoice Detail Table
                    'now pick it from dedcution master gl account code not from deduction account of account set
                    'Dim strInvCtrlAC As String = clsCommon.myCstr(dt.Rows(0)("Deduction_ACCOUNT"))
                    'If clsCommon.myLen(strInvCtrlAC) <= 0 Then
                    '    Throw New Exception("Please set Deduction Account for Vendor Account set Code :" + clsCommon.myCstr(dt.Rows(0)("Acct_Set_Code")))
                    'End If





                    ii = ii + 1
                    objVendorInvDetail.Detail_Line_No = ii
                    strDetailGLAccount = clsERPFuncationality.ChangeGLAccountLocationSegment(strDetailGLAccount, obj.MCC_CODE, trans)
                    objVendorInvDetail.GL_Account_Code = strDetailGLAccount
                    objVendorInvDetail.GL_Account_Desc = clsGLAccount.GetName(strDetailGLAccount, trans)
                    objVendorInvDetail.Amount = dblAmt

                    objVendorInvDetail.Discount_Per = 0
                    objVendorInvDetail.Discount = 0
                    objVendorInvDetail.Amount_less_Discount = dblAmt
                    objVendorInvDetail.Total_Tax = 0
                    objVendorInvDetail.Total_Amount = dblAmt
                    objVendorInvDetail.Landed_Amount = dblAmt
                    ''End of Set AP Invvoice Detail Table

                    If (clsCommon.myLen(objVendorInvDetail.GL_Account_Code) > 0) Then
                        objVendorInvHead.Arr.Add(objVendorInvDetail)
                    End If

                    ''Set AP Invvoice Header Table
                    objVendorInvHead.Total_Landed_Amt += dblAmt
                    objVendorInvHead.Discount_Base += dblAmt
                    objVendorInvHead.Discount_Amount += 0
                    objVendorInvHead.Amount_Less_Discount += dblAmt
                    objVendorInvHead.Document_Total += dblAmt
                    objVendorInvHead.Balance_Amt += dblAmt
                    ''End of Set AP Invvoice Header Table

                    objVendorInvHead.Empty_Amount = 0 'obj.Tot_Empty_Amount
                    If objVendorInvHead.Empty_Amount > 0 Then
                        If clsCommon.myLen(objVendorInvHead.Empty_Account) <= 0 Then
                            Throw New Exception("Please set Inventory Control Empties")
                        End If
                        objVendorInvHead.Document_Total += objVendorInvHead.Empty_Amount
                    End If
                    If (objVendorInvHead.Arr Is Nothing OrElse objVendorInvHead.Arr.Count <= 0) Then
                        Throw New Exception("No GL Account Found For AP Invoice")
                    End If
                    ''multicurrency
                    'objVendorInvHead.CURRENCY_CODE = obj.CURRENCY_CODE
                    'objVendorInvHead.ConvRate = 1
                    objVendorInvHead.ApplicableFrom = clsCommon.GetPrintDate(obj.DOC_DATE, "dd/MMM/yyyy")
                    ''end multicurrency
                    objVendorInvHead.Main_VSP_Milk_AP_Invoice_No = clsVedorInvoiceHead.GetMainVSPMilkAPInvoiceNo(ToDate, objVendorInvHead.Vendor_Code, trans)
                    objVendorInvHead.SaveData(objVendorInvHead, True, trans)
                    clsVedorInvoiceHead.PostData("", objVendorInvHead.Document_No, "", trans, obj.DOC_DATE)

                    Dim qry As String = " select Item_Code,max(Item_Desc) as Item_Desc,max(UOM) as UOM,sum(Fat_Amt) as Fat_Amt,sum(SNF_Amt) as SNF_Amt from TSPL_INVENTORY_MOVEMENT_NEW where source_doc_no in (select DOC_CODE  from TSPL_MILK_SRN_HEAD where convert(date, Doc_Date,103)>='" + clsCommon.GetPrintDate(FromDate, "dd/MMM/yyyy") + "' and convert(date, Doc_Date,103)<='" + clsCommon.GetPrintDate(ToDate, "dd/MMM/yyyy") + "'  and MCC_CODE='" + obj.MCC_CODE + "' and VSP_CODE='" + objVendorInvHead.Vendor_Code + "') and Trans_Type='MCC-MSRN' group by Item_Code "
                    Dim dtSRNFATSNF As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                    clsVSPBillAndIncentiveCalculation.CreateCostAdjustmentAgainstAPInvoice(dtSRNFATSNF, objVendorInvHead, obj.MCC_CODE, obj.MCC_NAME, "Cost Adjustment Against AP Invoice (Milk Rejection) : ", trans)

                End If
            Next
        End If

    End Sub

    Public Shared Function ReverseAndUnpost(ByVal strDocNo As String) As Boolean
        Dim tran As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim qry As String = "select distinct DOC_CODE from TSPL_MILK_PURCHASE_INVOICE_DETAIL where SRN_CODE in (select DOC_CODE from tspl_milk_srn_head where Against_Reject_No='" + strDocNo + "')"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, tran)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Throw New Exception(" Can't Reverse the document." + Environment.NewLine + "Reject document is used in Milk purchase invoice :" + clsCommon.myCstr(dt.Rows(0)("DOC_CODE")))
            End If
            qry = "delete from TSPL_JOURNAL_DETAILS where Voucher_No in ( select Voucher_No from TSPL_JOURNAL_MASTER where Source_Doc_No in (select Adjustment_No from TSPL_ADJUSTMENT_HEADER where Reference_Document = 'MLK-REJ' and Document_No in ('" + strDocNo + "')))"
            clsDBFuncationality.ExecuteNonQuery(qry, tran)

            qry = "delete from TSPL_JOURNAL_MASTER where Source_Doc_No in (select Adjustment_No from TSPL_ADJUSTMENT_HEADER where Reference_Document = 'MLK-REJ' and Document_No in ('" + strDocNo + "'))"
            clsDBFuncationality.ExecuteNonQuery(qry, tran)

            qry = "delete from TSPL_INVENTORY_MOVEMENT_NEW where Trans_Type='IC-AD' and Source_Doc_No in (select Adjustment_No from TSPL_ADJUSTMENT_HEADER where Reference_Document = 'MLK-REJ' and Document_No in ('" + strDocNo + "'))"
            clsDBFuncationality.ExecuteNonQuery(qry, tran)

            qry = "delete from TSPL_ADJUSTMENT_DETAIL where Adjustment_No in (select Adjustment_No from TSPL_ADJUSTMENT_HEADER where Reference_Document = 'MLK-REJ' and Document_No in ('" + strDocNo + "'))"
            clsDBFuncationality.ExecuteNonQuery(qry, tran)

            qry = "delete from TSPL_ADJUSTMENT_HEADER where Reference_Document = 'MLK-REJ' and Document_No in ('" + strDocNo + "')"
            clsDBFuncationality.ExecuteNonQuery(qry, tran)

            qry = "delete from TSPL_JOURNAL_DETAILS where Voucher_No in ( select Voucher_No from TSPL_JOURNAL_MASTER where Source_Doc_No in ((select DOC_CODE from TSPL_MILK_SRN_HEAD where Against_Reject_No='" + strDocNo + "')))"
            clsDBFuncationality.ExecuteNonQuery(qry, tran)

            qry = "delete from TSPL_JOURNAL_MASTER where Source_Doc_No in ((select DOC_CODE from TSPL_MILK_SRN_HEAD where Against_Reject_No='" + strDocNo + "'))"
            clsDBFuncationality.ExecuteNonQuery(qry, tran)

            qry = "delete from TSPL_INVENTORY_MOVEMENT_NEW where Trans_Type='MCC-MSRN' and Source_Doc_No in (select DOC_CODE from TSPL_MILK_SRN_HEAD where Against_Reject_No='" + strDocNo + "')"
            clsDBFuncationality.ExecuteNonQuery(qry, tran)

            qry = "delete from TSPL_MILK_SRN_DETAIL where  DOC_CODE in (select DOC_CODE from TSPL_MILK_SRN_HEAD where Against_Reject_No='" + strDocNo + "')"
            clsDBFuncationality.ExecuteNonQuery(qry, tran)

            qry = "delete from TSPL_MILK_SRN_HEAD where Against_Reject_No='" + strDocNo + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, tran)

            qry = "Update TSPL_MILK_REJECT_HEAD set POSTED=0, Posting_Date=null where DOC_CODE ='" + strDocNo + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, tran)

            tran.Commit()
        Catch ex As Exception
            tran.Rollback()
            Throw New Exception(ex.Message)
        End Try

        Return True
    End Function

    Public Shared Function GetMCCRegisterQuery(ByVal FromDate As DateTime, ByVal ToDate As DateTime, ByVal FromShift As String, ByVal ToShift As String, ByVal SRNAmounType As String, ByVal StrPermission As String, ByVal arrMCC As ArrayList, ByVal arrRoute As ArrayList, ByVal arrVLC As ArrayList, ByVal ReceivingUOM As String) As String
        Return GetMCCRegisterQuery(FromDate, ToDate, FromShift, ToShift, SRNAmounType, StrPermission, Nothing, arrMCC, arrRoute, arrVLC, ReceivingUOM)
    End Function
    Public Shared Function GetMCCRegisterQuery(ByVal FromDate As DateTime, ByVal ToDate As DateTime, ByVal FromShift As String, ByVal ToShift As String, ByVal SRNAmounType As String, ByVal StrPermission As String, ByVal arrPlant As ArrayList, ByVal arrMCC As ArrayList, ByVal arrRoute As ArrayList, ByVal arrVLC As ArrayList, ByVal ReceivingUOM As String) As String
        Return GetMCCRegisterQuery(FromDate, ToDate, FromShift, ToShift, SRNAmounType, StrPermission, arrPlant, arrMCC, arrRoute, arrVLC, ReceivingUOM, "")
    End Function
    Public Shared Function GetMCCRegisterQuery(ByVal FromDate As DateTime, ByVal ToDate As DateTime, ByVal FromShift As String, ByVal ToShift As String, ByVal SRNAmounType As String, ByVal StrPermission As String, ByVal arrPlant As ArrayList, ByVal arrMCC As ArrayList, ByVal arrRoute As ArrayList, ByVal arrVLC As ArrayList, ByVal ReceivingUOM As String, ByVal MilkType As String) As String
        Return GetMCCRegisterQuery(FromDate, ToDate, FromShift, ToShift, SRNAmounType, StrPermission, arrPlant, arrMCC, arrRoute, arrVLC, ReceivingUOM, MilkType, Nothing, False)
    End Function
    Public Shared Function GetMCCRegisterQuery(ByVal FromDate As DateTime, ByVal ToDate As DateTime, ByVal FromShift As String, ByVal ToShift As String, ByVal SRNAmounType As String, ByVal StrPermission As String, ByVal arrPlant As ArrayList, ByVal arrMCC As ArrayList, ByVal arrRoute As ArrayList, ByVal arrVLC As ArrayList, ByVal ReceivingUOM As String, ByVal MilkType As String, ByVal arrVSP As ArrayList, ByVal IsMilkBillReport As Boolean) As String
        Dim settPickFATSNFKgFromInventory As Boolean = clsFixedParameter.GetData(clsFixedParameterType.PickFATSNFKgFromInventory, clsFixedParameterCode.PickFATSNFKgFromInventory, Nothing)
        Dim SetCowFatPer As Decimal = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.CowFATPer, clsFixedParameterCode.CowFATPer, Nothing))

        Dim qry As String = "Select final.[Milk Receipt Code] ,final.MCC as [MCC Code] ,final.[MCC Name],final.Short_Description_MCC,final.[MCC Type] ,final.[Chilling Center],final.[Plant Code],final.[Plant Name] ,final.Date ,final.[Doc Date] ,final.Shift ," &
                "final.[Route Code],final.[Route Name],final.Short_Description_Route ,final.[Vehicle Code] ,final.[VSP Code],final.[VSP Name],final.[Vendor Group Code] ,final.[Vendor Group Desc],final.[Vlc Uploader Code] ,final.[Vlc Code] ,final.[VLC Name],final.Short_Description_VLC ," &
                " final.[Sample No] ,final.[No Of Cans] ,final.Item_Code,final.Item_Desc,final.[Milk Weight],final.UOM_Code as [UOM],final.[Milk Weight(KG)]," &
                " final.[Milk Weight(LTR)]  as [Milk Weight(LTR)],final.[DBT Amount]," &
                " final.Capping_FAT,final.[FAT(%)]  ,final.CLR,final.Capping_SNF,final.[SNF(%)] ,final.[FAT(KG)],final.[SNF(KG)],final.[FAT(LTR)],final.[SNF(LTR)], final.FAT_Amount , final.SNF_Amount ,final.[Cow Milk Qty (KG)],final.[Cow Milk Qty (Ltr)],final.[Cow FAT(%)], Case When final.[FAT(%)] <= 5 Then final.CLR Else 0 End [Cow CLR],final.[Cow SNF(%)] , Case When final.[FAT(%)] <= 5 Then final.[FAT(KG)] Else 0 End [Cow FAT (KG)], Case When final.[FAT(%)] <= 5 Then final.[SNF(KG)] Else 0 End [Cow SNF (KG)]," &
                " final.[Buffalo Milk Qty (KG)],final.[Buffalo Milk Qty (Ltr)] ,Case When final.[FAT(%)] > 5 Then final.CLR Else 0 End [Buffalo CLR],final.[Buffalo SNF(%)],final.[Buffalo FAT(%)], Case When final.[FAT(%)] > 5 Then final.[FAT(KG)] Else 0 End [Buffalo FAT (KG)], Case When final.[FAT(%)] > 5 Then final.[SNF(KG)] Else 0 End [Buffalo SNF (KG)],final.[Milk Type],final.[SRN No],final.[SRN Amount]," &
                " final.[SRN Qty],final.[SRN Rate],final.[Shift Status] ,Invoice_no ,Invoice_Date , IS_MANUAL, MACHINE_NO,(CASE WHEN [Sample Status]='Auto' THEN 'N' ELSE 'Y' END) AS IS_MILK_SAMPLE_MANUAL,[Transporter Code],[Transporter Name],EMP_Amount,TIP_Amount,NET_AMOUNT,Round_Off,Handling_Charges_Amount,final.Planning_Code,final.Planning_Posted_Date,final.Planning_Posted_Time ,final.Declared_Rate,final.[Price Code],final.Purchase_Order_No,final.Head_Load_Amount,final.SNF_Ded_Value,final.SNF_Ded_Rate,final.SNF_Ded_Amount,final.VSP_Commission_Amount,final.VSP_Deduction_Amount,final.VSP_Day_Wise_Incentive,final.IncetiveAmt,final.SubStandard,final.Vehicle,Mcc_Code_VLC_Uploader as [Mcc_Uploader_Code], final.[SubStandardQty],final.[Doc Date Time] " &
                " From (Select case when isnull(TSPL_MILK_SRN_HEAD.Capping_Apply,0)=1 then TSPL_MILK_SRN_DETAIL.Capping_FAT else null end as Capping_FAT,case when isnull(TSPL_MILK_SRN_HEAD.Capping_Apply,0)=1 then TSPL_MILK_SRN_DETAIL.Capping_SNF else null end as Capping_SNF,TSPL_MCC_MASTER.MCC_Type as [MCC Type],TSPL_MCC_MASTER.Short_Description as Short_Description_MCC,TSPL_MCC_ROUTE_MASTER.Short_Description as Short_Description_Route,TSPL_VLC_MASTER_HEAD.Short_Description as Short_Description_VLC,case when TSPL_MCC_MASTER.is_Mcc=1 then 'MCC' else 'BMCC' end [Chilling Center] ,TSPL_MILK_SRN_DETAIL.Item_Code,TSPL_ITEM_MASTER.Item_Desc "
        If objCommonVar.PricePlan = 6 OrElse objCommonVar.PricePlan = 7 Then
            qry += " , Case When TSPL_MILK_SAMPLE_DETAIL.TYPE = 'C' Then TSPL_MILK_SAMPLE_DETAIL.FAT Else 0 End [Cow FAT(%)], Case When TSPL_MILK_SAMPLE_DETAIL.TYPE = 'C' Then TSPL_MILK_SAMPLE_DETAIL.SNF Else 0 End [Cow SNF(%)]," &
    " Case When TSPL_MILK_SAMPLE_DETAIL.TYPE = 'B' Then TSPL_MILK_SAMPLE_DETAIL.FAT Else 0 End [Buffalo FAT(%)], Case When TSPL_MILK_SAMPLE_DETAIL.TYPE = 'B' Then TSPL_MILK_SAMPLE_DETAIL.SNF Else 0 End [Buffalo SNF(%)], Case When TSPL_MILK_SAMPLE_DETAIL.TYPE = 'C' Then TSPL_MILK_RECEIPT_DETAIL.ACC_WEIGHT Else 0 End [Cow Milk Qty (KG)],TSPL_MCC_MASTER.Mcc_Code_VLC_Uploader,Case When TSPL_MILK_SAMPLE_DETAIL.TYPE = 'C' Then TSPL_MILK_RECEIPT_DETAIL.ACC_WEIGHT_LTR Else 0 End [Cow Milk Qty (Ltr)], " &
    " Case When TSPL_MILK_SAMPLE_DETAIL.TYPE = 'B' Then TSPL_MILK_RECEIPT_DETAIL.ACC_WEIGHT Else 0 End [Buffalo Milk Qty (KG)],Case When TSPL_MILK_SAMPLE_DETAIL.TYPE = 'B' Then TSPL_MILK_RECEIPT_DETAIL.ACC_WEIGHT_LTR Else 0 End [Buffalo Milk Qty (Ltr)] "
            qry += ", TPCP.Dock_Collection_Milk_Type As [Milk Type]"
        ElseIf objCommonVar.DisplayTypeInMilkReceipt Then
            qry += " , Case When TSPL_MILK_SAMPLE_DETAIL.TYPE = 'C' Then TSPL_MILK_SAMPLE_DETAIL.FAT Else 0 End [Cow FAT(%)], Case When TSPL_MILK_SAMPLE_DETAIL.TYPE = 'C' Then TSPL_MILK_SAMPLE_DETAIL.SNF Else 0 End [Cow SNF(%)]," &
                " Case When TSPL_MILK_SAMPLE_DETAIL.TYPE = 'B' Then TSPL_MILK_SAMPLE_DETAIL.FAT Else 0 End [Buffalo FAT(%)], Case When TSPL_MILK_SAMPLE_DETAIL.TYPE = 'B' Then TSPL_MILK_SAMPLE_DETAIL.SNF Else 0 End [Buffalo SNF(%)], Case When TSPL_MILK_SAMPLE_DETAIL.TYPE = 'C' Then TSPL_MILK_RECEIPT_DETAIL.ACC_WEIGHT Else 0 End [Cow Milk Qty (KG)],TSPL_MCC_MASTER.Mcc_Code_VLC_Uploader,Case When TSPL_MILK_SAMPLE_DETAIL.TYPE = 'C' Then TSPL_MILK_RECEIPT_DETAIL.ACC_WEIGHT_LTR Else 0 End [Cow Milk Qty (Ltr)], " &
                " Case When TSPL_MILK_SAMPLE_DETAIL.TYPE = 'B' Then TSPL_MILK_RECEIPT_DETAIL.ACC_WEIGHT Else 0 End [Buffalo Milk Qty (KG)],Case When TSPL_MILK_SAMPLE_DETAIL.TYPE = 'B' Then TSPL_MILK_RECEIPT_DETAIL.ACC_WEIGHT_LTR Else 0 End [Buffalo Milk Qty (Ltr)] "
            qry += ", TSPL_MILK_SAMPLE_DETAIL.TYPE  As [Milk Type]"
        Else
            qry += " ,Case When TSPL_MILK_SAMPLE_DETAIL.FAT <= 5 Then TSPL_MILK_SAMPLE_DETAIL.FAT Else 0 End [Cow FAT(%)], Case When TSPL_MILK_SAMPLE_DETAIL.FAT <= 5 Then TSPL_MILK_SAMPLE_DETAIL.SNF Else 0 End [Cow SNF(%)]," &
                " Case When TSPL_MILK_SAMPLE_DETAIL.FAT > 5 Then TSPL_MILK_SAMPLE_DETAIL.FAT Else 0 End [Buffalo FAT(%)], Case When TSPL_MILK_SAMPLE_DETAIL.FAT > 5 Then TSPL_MILK_SAMPLE_DETAIL.SNF Else 0 End [Buffalo SNF(%)], Case When TSPL_MILK_SAMPLE_DETAIL.FAT <= 5 Then TSPL_MILK_RECEIPT_DETAIL.ACC_WEIGHT Else 0 End [Cow Milk Qty (KG)],TSPL_MCC_MASTER.Mcc_Code_VLC_Uploader, Case When TSPL_MILK_SAMPLE_DETAIL.FAT <= 5 Then TSPL_MILK_RECEIPT_DETAIL.ACC_WEIGHT_LTR Else 0 End [Cow Milk Qty (Ltr)]," &
                " Case When TSPL_MILK_SAMPLE_DETAIL.FAT > 5 Then TSPL_MILK_RECEIPT_DETAIL.ACC_WEIGHT Else 0 End [Buffalo Milk Qty (KG)] ,Case When TSPL_MILK_SAMPLE_DETAIL.FAT > 5 Then TSPL_MILK_RECEIPT_DETAIL.ACC_WEIGHT_LTR Else 0 End [Buffalo Milk Qty (Ltr)] "
            qry += " , Case When Coalesce(TSPL_MILK_SAMPLE_DETAIL.FAT, 0) <= 0 Then '' When Coalesce(TSPL_MILK_SAMPLE_DETAIL.FAT, 0) <= " + clsCommon.myCstr(SetCowFatPer) + "  Then 'C' Else 'M' End As [Milk Type]"
        End If
        qry += ", TSPL_MILK_RECEIPT_HEAD.DOC_CODE As [Milk Receipt Code]," &
                " TSPL_MILK_RECEIPT_HEAD.MCC_CODE As MCC, TSPL_MCC_MASTER.MCC_NAME As [MCC Name],isnull(TSPL_MCC_MASTER.plant_code,'') As [Plant Code], isnull(tspl_location_master.location_desc,'') As [Plant Name], Convert(date,TSPL_MILK_RECEIPT_HEAD.DOC_DATE,103) As Date, " &
                " Convert(varchar,TSPL_MILK_RECEIPT_HEAD.DOC_DATE,103) As [Doc Date], Case When TSPL_MILK_RECEIPT_DETAIL.SHIFT = 'M' Then 'Morning' Else 'Evening' End As Shift, " &
                " TSPL_MILK_RECEIPT_DETAIL.ROUTE_CODE As [Route Code], TSPL_MCC_ROUTE_MASTER.Route_Name As [Route Name], TSPL_MILK_RECEIPT_DETAIL.VEHICLE_CODE As [Vehicle Code]," &
                " TSPL_MILK_SRN_HEAD.VSP_CODE As [VSP Code], TSPL_VENDOR_MASTER.Vendor_Name As [VSP Name], TSPL_VENDOR_MASTER.Vendor_Group_Code As [Vendor Group Code],TSPL_VENDOR_GROUP.Group_Desc as [Vendor Group Desc],TSPL_VLC_MASTER_HEAD.VLC_Code As [Vlc Code]," &
                " TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader As [Vlc Uploader Code], TSPL_VLC_MASTER_HEAD.VLC_Name As [VLC Name], TSPL_MILK_RECEIPT_DETAIL.SAMPLE_NO As [Sample No], " &
                " TSPL_MILK_RECEIPT_DETAIL.NO_OF_CANS As [No Of Cans], TSPL_MILK_RECEIPT_DETAIL.MILK_WEIGHT As [Milk Weight],TSPL_MILK_RECEIPT_DETAIL.UOM_Code, TSPL_MILK_RECEIPT_DETAIL.ACC_WEIGHT As [Milk Weight(KG)]," &
                " convert(decimal(18,2),TSPL_MILK_RECEIPT_DETAIL.ACC_WEIGHT_LTR) As [Milk Weight(LTR)],Convert(decimal(18,2),TSPL_MILK_RECEIPT_DETAIL.ACC_WEIGHT_LTR)*5 as [DBT Amount] , TSPL_MILK_SAMPLE_DETAIL.FAT As [FAT(%)], TSPL_MILK_SAMPLE_DETAIL.SNF As [SNF(%)], TSPL_MILK_SAMPLE_DETAIL.CLR, "

        If settPickFATSNFKgFromInventory Then
            qry += "TSPL_INVENTORY_MOVEMENT_NEW.FAT_KG  As [FAT(KG)],TSPL_INVENTORY_MOVEMENT_NEW.SNF_KG  As [SNF(KG)],"
        Else
            qry += "Convert(decimal(18," + clsCommon.myCstr(objCommonVar.MilkSRNFATSNFDecimalPlaces) + "), TSPL_MILK_SRN_DETAIL.FAT_KG) As [FAT(KG)]," &
                " Convert(decimal(18," + clsCommon.myCstr(objCommonVar.MilkSRNFATSNFDecimalPlaces) + "),TSPL_MILK_SRN_DETAIL.SNF_KG) As [SNF(KG)],"
        End If
        qry += " Convert(decimal(18," + clsCommon.myCstr(objCommonVar.MilkSRNFATSNFDecimalPlaces) + "), ROUND(TSPL_MILK_SAMPLE_DETAIL.FAT * TSPL_MILK_RECEIPT_DETAIL.ACC_WEIGHT_LTR / 100," + clsCommon.myCstr(objCommonVar.MilkSRNFATSNFDecimalPlaces) + ",1)) As [FAT(LTR)]," &
                " Convert(decimal(18," + clsCommon.myCstr(objCommonVar.MilkSRNFATSNFDecimalPlaces) + "),ROUND(TSPL_MILK_SAMPLE_DETAIL.SNF * TSPL_MILK_RECEIPT_DETAIL.ACC_WEIGHT_LTR / 100," + clsCommon.myCstr(objCommonVar.MilkSRNFATSNFDecimalPlaces) + ",1)) As [SNF(LTR)]," &
                " cast( round((TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_Net_Amount +isnull(TSPL_MILK_SRN_DETAIL.VSP_Day_Wise_Incentive,0) )  * isnull(TSPL_MILK_SRN_DETAIL.FAT_Ratio,0),0) as integer) as FAT_Amount ," &
                " cast(((TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_Net_Amount+ isnull(TSPL_MILK_SRN_DETAIL.VSP_Day_Wise_Incentive,0)))-round( (TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_Net_Amount+ isnull(TSPL_MILK_SRN_DETAIL.VSP_Day_Wise_Incentive,0))*isnull(TSPL_MILK_SRN_DETAIL.FAT_Ratio,0),0) as integer) as SNF_Amount , " &
                " Case When TSPL_MILK_SAMPLE_DETAIL.IS_MANUAL = '' Then 'Auto' Else TSPL_MILK_SAMPLE_DETAIL.IS_MANUAL End As [Sample Status]," &
                " TSPL_MILK_SRN_HEAD.DOC_CODE As [SRN No], Convert(decimal(18,2),TSPL_MILK_SRN_DETAIL.AMOUNT) As [SRN Amount], TSPL_MILK_SRN_DETAIL.RATE As [SRN Rate], TSPL_MILK_SRN_DETAIL.Qty As [SRN Qty], Case When TSPL_MILK_Shift_End_HEAD.DOC_CODE Is Null Then 'Open' Else 'Close' End [Shift Status],TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE as Invoice_no," &
                " convert(varchar,TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_DATE,103) as Invoice_Date , tspl_milk_receipt_detail.IS_MANUAL , tspl_milk_receipt_detail.MACHINE_NO,[Transporter Code],[Transporter Name],isnull( TSPL_MILK_SRN_DETAIL.EMP_Amount,0) as EMP_Amount,TSPL_MILK_SRN_DETAIL.TIP_Amount,isnull(TSPL_MILK_SRN_DETAIL.NET_AMOUNT,0) as NET_AMOUNT,isnull(TSPL_MILK_SRN_DETAIL.Round_Off,0) as Round_Off,isnull(TSPL_MILK_PURCHASE_INVOICE_DETAIL.Handling_Charges_Amount,0) as Handling_Charges_Amount,TabTSPL_FAT_SNF_UPLOADER_MASTER.Planning_Code,FORMAT ( TSPL_PRICE_CHART_PLANNING.Posted_Date , 'dd/MM/yyyy') as Planning_Posted_Date, FORMAT (TSPL_PRICE_CHART_PLANNING. Posted_Date , 'hh:mm:ss tt') as Planning_Posted_Time,TSPL_MILK_PRICE_MASTER.Declared_Rate,TSPL_MILK_SRN_DETAIL.Price_Code as [Price Code],TSPL_MILK_SRN_HEAD.Purchase_Order_No,TSPL_MILK_SRN_DETAIL.Head_Load_Amount " & Environment.NewLine +
                ",TSPL_MILK_PRICE_SNF_DEDUCTION.Amount as SNF_Ded_Value,cast((TSPL_MILK_PRICE_SNF_DEDUCTION.Amount+TSPL_MILK_SRN_DETAIL.RATE) as decimal(18,2)) as SNF_Ded_Rate,cast((TSPL_MILK_PRICE_SNF_DEDUCTION.Amount+TSPL_MILK_SRN_DETAIL.RATE)*TSPL_MILK_SRN_DETAIL.ACC_Qty as decimal(18,2)) as SNF_Ded_Amount " + Environment.NewLine +
                " ,(isnull(TSPL_MILK_SRN_DETAIL.VSP_Commission_Apply,0)*TSPL_MILK_SRN_DETAIL.VSP_Commission_Amount)  as VSP_Commission_Amount ,(isnull(TSPL_MILK_SRN_DETAIL.VSP_Deduction_Apply,0)*TSPL_MILK_SRN_DETAIL.VSP_Deduction_Amount)  as VSP_Deduction_Amount,TSPL_MILK_SRN_DETAIL.VSP_Day_Wise_Incentive " + Environment.NewLine +
                " ,case when TSPL_MILK_PURCHASE_INVOICE_INCENTIVEDETAIL.MILK_SRN_Code is null then isnull( TSPL_MILK_PURCHASE_INVOICE_PROVISON_INCENTIVEDETAIL.Incentive_Amount,0) else isnull(TSPL_MILK_PURCHASE_INVOICE_INCENTIVEDETAIL.Incentive_Amount,0) end as  IncetiveAmt,case when isnull( TSPL_MILK_SRN_DETAIL.Sub_Standard,0)=1 then 'Sub Standard' else '' end as SubStandard,TSPL_Primary_Vehicle_Master.Vehicle,case when isnull( TSPL_MILK_SRN_DETAIL.Sub_Standard,0)=1 then  TSPL_MILK_RECEIPT_DETAIL.ACC_WEIGHT_LTR else 0 end as [SubStandardQty],Convert(varchar,TSPL_MILK_RECEIPT_HEAD.DOC_DATE,103) + ' ' + CONVERT(varchar,TSPL_MILK_RECEIPT_HEAD.DOC_DATE,8) as [Doc Date Time] " + Environment.NewLine +
                " From TSPL_MILK_RECEIPT_DETAIL " + Environment.NewLine +
                " Left Outer Join TSPL_MILK_RECEIPT_HEAD On TSPL_MILK_RECEIPT_HEAD.DOC_CODE = TSPL_MILK_RECEIPT_DETAIL.DOC_CODE " + Environment.NewLine +
                " Left Outer Join TSPL_MILK_SAMPLE_HEAD On TSPL_MILK_SAMPLE_HEAD.MILK_RECEIPT_CODE = TSPL_MILK_RECEIPT_HEAD.DOC_CODE" + Environment.NewLine +
                " Left Outer Join TSPL_MILK_SAMPLE_DETAIL On TSPL_MILK_SAMPLE_DETAIL.SAMPLE_NO = TSPL_MILK_RECEIPT_DETAIL.SAMPLE_NO And TSPL_MILK_SAMPLE_DETAIL.DOC_CODE = TSPL_MILK_SAMPLE_HEAD.DOC_CODE " + Environment.NewLine +
                " Left Outer Join TSPL_MILK_SRN_HEAD On TSPL_MILK_SRN_HEAD.MILK_SAMPLE_CODE = TSPL_MILK_SAMPLE_HEAD.DOC_CODE And TSPL_MILK_SRN_HEAD.SAMPLE_NO = TSPL_MILK_SAMPLE_DETAIL.SAMPLE_NO " + Environment.NewLine +
                " Left Outer Join TSPL_MILK_SRN_DETAIL On TSPL_MILK_SRN_HEAD.DOC_CODE = TSPL_MILK_SRN_DETAIL.DOC_CODE" + Environment.NewLine +
                " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.item_code=TSPL_MILK_SRN_DETAIL.item_code " + Environment.NewLine +
                " Left Outer Join TSPL_MILK_PURCHASE_INVOICE_DETAIL On TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_CODE = TSPL_MILK_SRN_HEAD.DOC_CODE " + Environment.NewLine +
                " Left Outer Join TSPL_MILK_PURCHASE_INVOICE_HEAD On TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE = TSPL_MILK_PURCHASE_INVOICE_DETAIL.DOC_CODE " + Environment.NewLine +
                " Left Outer Join TSPL_MCC_MASTER On TSPL_MCC_MASTER.MCC_Code = TSPL_MILK_RECEIPT_HEAD.MCC_CODE " + Environment.NewLine +
                " Left Outer Join TSPL_VLC_MASTER_HEAD On TSPL_VLC_MASTER_HEAD.VLC_Code = TSPL_MILK_RECEIPT_DETAIL.VLC_CODE" + Environment.NewLine +
                " Left Outer Join TSPL_VENDOR_MASTER On TSPL_VENDOR_MASTER.Vendor_Code = TSPL_MILK_RECEIPT_DETAIL.VSP_CODE" + Environment.NewLine +
                " left outer join TSPL_VENDOR_GROUP on TSPL_VENDOR_MASTER.Vendor_Group_Code = TSPL_VENDOR_GROUP.Ven_Group_Code " + Environment.NewLine +
                " Left Outer Join TSPL_MCC_ROUTE_MASTER On TSPL_MCC_ROUTE_MASTER.Route_Code = TSPL_MILK_RECEIPT_DETAIL.ROUTE_CODE" + Environment.NewLine +
                " Left join (select TSPL_Primary_Vehicle_Master.vendor_code as [Transporter Code],tspl_vendor_master.vendor_name as [Transporter Name],TSPL_Primary_Vehicle_Master.mcc_code,TSPL_Primary_Vehicle_Master.vehicle_code from TSPL_Primary_Vehicle_Master left outer join tspl_vendor_master on tspl_vendor_master.vendor_code=TSPL_Primary_Vehicle_Master.vendor_code and tspl_vendor_master.form_type='PTM' left outer join tspl_mcc_master on tspl_mcc_master.mcc_code=TSPL_Primary_Vehicle_Master.mcc_code) as t1 on t1.vehicle_code=TSPL_MCC_ROUTE_MASTER.Vehicle_Code " + Environment.NewLine +
                " Left Outer Join TSPL_Primary_Vehicle_Master On TSPL_Primary_Vehicle_Master.Vehicle_Code = TSPL_MCC_ROUTE_MASTER.Vehicle_Code " + Environment.NewLine +
                " Left Outer Join TSPL_MILK_Shift_End_HEAD On TSPL_MILK_Shift_End_HEAD.MCC_CODE = TSPL_MILK_RECEIPT_HEAD.MCC_CODE " + Environment.NewLine +
                " And convert(date,TSPL_MILK_Shift_End_HEAD.DOC_DATE,103) = convert(date,TSPL_MILK_RECEIPT_HEAD.DOC_DATE,103) " + Environment.NewLine +
                " And TSPL_MILK_Shift_End_HEAD.SHIFT = TSPL_MILK_RECEIPT_HEAD.SHIFT " &
                " Left Outer Join TSPL_MILK_Shift_End_Route_DETAIL On TSPL_MILK_Shift_End_Route_DETAIL.DOC_CODE = TSPL_MILK_Shift_End_HEAD.DOC_CODE " &
                " And TSPL_MILK_Shift_End_Route_DETAIL.Route_CODE = TSPL_MCC_ROUTE_MASTER.Route_Code " &
                " left outer join (select code,max(Price_code) as Price_code,max(Planning_Code) as Planning_Code from  TSPL_FAT_SNF_UPLOADER_MASTER group by code) as TabTSPL_FAT_SNF_UPLOADER_MASTER on TabTSPL_FAT_SNF_UPLOADER_MASTER.code=TSPL_MILK_SRN_DETAIL.Price_Code " + Environment.NewLine +
                " left outer join TSPL_MILK_PRICE_MASTER on TSPL_MILK_PRICE_MASTER.Price_Code=TabTSPL_FAT_SNF_UPLOADER_MASTER.Price_code" + Environment.NewLine +
                " left outer join TSPL_MILK_PRICE_SNF_DEDUCTION on TSPL_MILK_PRICE_SNF_DEDUCTION.Price_code=TabTSPL_FAT_SNF_UPLOADER_MASTER.Price_code and cast(TSPL_MILK_SRN_DETAIL.SNF_PER as decimal(18,1))=TSPL_MILK_PRICE_SNF_DEDUCTION.Per left outer join TSPL_PRICE_CHART_PLANNING on TSPL_PRICE_CHART_PLANNING.Planning_Code =  TabTSPL_FAT_SNF_UPLOADER_MASTER.Planning_Code " + Environment.NewLine +
                " left join tspl_location_master on tspl_location_master.location_code=TSPL_MCC_MASTER.Plant_Code " &
                "left outer join (select MILK_SRN_Code,sum(Incentive_Amount) as Incentive_Amount from TSPL_MILK_PURCHASE_INVOICE_INCENTIVEDETAIL group by MILK_SRN_Code) as TSPL_MILK_PURCHASE_INVOICE_INCENTIVEDETAIL on TSPL_MILK_PURCHASE_INVOICE_INCENTIVEDETAIL.MILK_SRN_Code=TSPL_MILK_SRN_HEAD.DOC_CODE " &
                "left outer join (select MILK_SRN_Code,sum(Incentive_Amount) as Incentive_Amount from TSPL_MILK_PURCHASE_INVOICE_PROVISON_INCENTIVEDETAIL group by MILK_SRN_Code) as TSPL_MILK_PURCHASE_INVOICE_PROVISON_INCENTIVEDETAIL on TSPL_MILK_PURCHASE_INVOICE_PROVISON_INCENTIVEDETAIL.MILK_SRN_Code=TSPL_MILK_SRN_HEAD.DOC_CODE "
        If settPickFATSNFKgFromInventory Then
            qry += "left outer join TSPL_INVENTORY_MOVEMENT_NEW on TSPL_INVENTORY_MOVEMENT_NEW.Source_doc_no=TSPL_MILK_SRN_HEAD.DOC_CODE and TSPL_INVENTORY_MOVEMENT_NEW.Trans_Type='MCC-MSRN' "
        End If
        If objCommonVar.PricePlan = 6 OrElse objCommonVar.PricePlan = 7 Then
            qry += " left outer join  TSPL_PRICE_CHART_PLANNING TPCP on TSPL_MILK_SRN_DETAIL.Price_Code=TPCP.Planning_Code "
        End If
        qry += " where 2 = 2 "


        If IsMilkBillReport = True Then
            qry += " and TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE is not null and Cast(TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_DATE as Date) >='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(FromDate), "dd/MMM/yyyy") + "' and Cast(TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_DATE as Date) <='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(ToDate), "dd/MMM/yyyy") + "'"
            If clsCommon.CompairString(FromShift, "E") = CompairStringResult.Equal Then
                qry += " and 2=( case when TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_DATE >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(FromDate), "dd/MMM/yyyy hh:mm tt") + "' and TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_DATE <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(FromDate), "dd/MMM/yyyy hh:mm tt") + "' and TSPL_MILK_RECEIPT_HEAD.SHIFT='M' then 3 else 2 end  )"
            End If
            If clsCommon.CompairString(ToShift, "M") = CompairStringResult.Equal Then
                qry += " and 2=( case when TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_DATE >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(ToDate), "dd/MMM/yyyy hh:mm tt") + "' and TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_DATE <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(ToDate), "dd/MMM/yyyy hh:mm tt") + "' and TSPL_MILK_RECEIPT_HEAD.SHIFT='E' then 3 else 2 end  )"
            End If

        Else
            qry += " and Cast(TSPL_MILK_RECEIPT_HEAD.DOC_DATE as Date) >='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(FromDate), "dd/MMM/yyyy") + "' and Cast(TSPL_MILK_RECEIPT_HEAD.DOC_DATE as Date) <='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(ToDate), "dd/MMM/yyyy") + "'"
            If clsCommon.CompairString(FromShift, "E") = CompairStringResult.Equal Then
                qry += " and 2=( case when Cast(TSPL_MILK_RECEIPT_HEAD.DOC_DATE as Date) >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(FromDate), "dd/MMM/yyyy") + "' and Cast(TSPL_MILK_RECEIPT_HEAD.DOC_DATE as Date) <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(FromDate), "dd/MMM/yyyy") + "' and TSPL_MILK_RECEIPT_DETAIL.SHIFT='M' then 3 else 2 end  )"
            End If
            If clsCommon.CompairString(ToShift, "M") = CompairStringResult.Equal Then
                qry += " and 2=( case when Cast(TSPL_MILK_RECEIPT_HEAD.DOC_DATE as Date) >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(ToDate), "dd/MMM/yyyy") + "' and Cast(TSPL_MILK_RECEIPT_HEAD.DOC_DATE as Date) <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(ToDate), "dd/MMM/yyyy") + "' and TSPL_MILK_RECEIPT_DETAIL.SHIFT='E' then 3 else 2 end  )"
            End If
        End If



        If clsCommon.CompairString(SRNAmounType, "Zero") = CompairStringResult.Equal Then
            qry += " and TSPL_MILK_SRN_DETAIL.AMOUNT = 0 "
        ElseIf clsCommon.CompairString(SRNAmounType, "NonZero") = CompairStringResult.Equal Then
            qry += " and TSPL_MILK_SRN_DETAIL.AMOUNT <> 0 "
        End If

        If arrMCC IsNot Nothing AndAlso arrMCC.Count > 0 Then
            qry += "and TSPL_MILK_RECEIPT_HEAD.MCC_Code  IN (" + clsCommon.GetMulcallString(arrMCC) + ") "
        Else
            If clsCommon.myLen(StrPermission) > 0 Then
                qry += "And TSPL_MILK_RECEIPT_HEAD.mcc_Code in (" & StrPermission & ") "
            End If
        End If
        If arrPlant IsNot Nothing AndAlso arrPlant.Count > 0 Then
            qry += " and isnull(TSPL_MCC_MASTER.plant_code,'') in (" + clsCommon.GetMulcallString(arrPlant) + ")"
        End If
        If arrRoute IsNot Nothing AndAlso arrRoute.Count > 0 Then
            qry += " and TSPL_MILK_RECEIPT_DETAIL.Route_Code in (" + clsCommon.GetMulcallString(arrRoute) + ")  "
        End If
        If arrVLC IsNot Nothing AndAlso arrVLC.Count > 0 Then
            qry += " and TSPL_MILK_RECEIPT_DETAIL.VLC_CODE in (" + clsCommon.GetMulcallString(arrVLC) + ")  "
        End If
        If arrVSP IsNot Nothing AndAlso arrVSP.Count > 0 Then
            qry += " and TSPL_MILK_SRN_HEAD.VSP_CODE in (" + clsCommon.GetMulcallString(arrVSP) + ")  "
        End If
        If clsCommon.myLen(ReceivingUOM) > 0 Then
            qry += " and TSPL_MILK_RECEIPT_DETAIL.UOM_Code='" + ReceivingUOM + "'"
        End If
        If clsCommon.myLen(MilkType) > 0 Then
            qry += " and TSPL_MILK_SAMPLE_DETAIL.TYPE='" + MilkType + "'"
        End If

        qry += " ) As final where 2=2 "
        Return qry
    End Function

    Public Shared Function GetMCCRegisterWithRejectionColumnQuery(ByVal FromDate As DateTime, ByVal ToDate As DateTime, ByVal FromShift As String, ByVal ToShift As String, ByVal SRNAmounType As String, ByVal StrPermission As String, ByVal arrMCC As ArrayList, ByVal arrRoute As ArrayList, ByVal arrVLC As ArrayList, ByVal ReceivingUOM As String, ByVal strRejection As String, ByVal ShowVLCUploaderData As Boolean, ByVal SetCowFatPer As Decimal) As String
        Dim strSRNQuery As String = Nothing
        'Dim SetCowFatPer As Integer

        strSRNQuery = "Select  TSPL_MCC_MASTER.MCC_Type as [MCC Type],case when TSPL_MCC_MASTER.is_Mcc=1 then 'MCC' else 'BMCC' end [Chilling Center] ,TSPL_MILK_SRN_DETAIL.Item_Code,TSPL_ITEM_MASTER.Item_Desc, TSPL_MILK_SRN_DETAIL.EMP_Amount,TSPL_MILK_SRN_DETAIL.TIP_Amount,TSPL_MILK_SRN_DETAIL.Service_Charge_Amount"
        If objCommonVar.DisplayTypeInMilkReceipt Then
            strSRNQuery += ",Case When TSPL_MILK_SAMPLE_DETAIL.TYPE = 'C' Then TSPL_MILK_SAMPLE_DETAIL.FAT Else 0 End [Cow FAT(%)], Case When TSPL_MILK_SAMPLE_DETAIL.TYPE = 'C' Then TSPL_MILK_SAMPLE_DETAIL.SNF Else 0 End [Cow SNF(%)]," &
        " Case When TSPL_MILK_SAMPLE_DETAIL.TYPE = 'B' Then TSPL_MILK_SAMPLE_DETAIL.FAT Else 0 End [Buffalo FAT(%)], Case When TSPL_MILK_SAMPLE_DETAIL.TYPE = 'B' Then TSPL_MILK_SAMPLE_DETAIL.SNF Else 0 End [Buffalo SNF(%)], Case When TSPL_MILK_SAMPLE_DETAIL.TYPE = 'C' Then TSPL_MILK_RECEIPT_DETAIL.ACC_WEIGHT Else 0 End [Cow Milk Qty (KG)]," &
        " Case When TSPL_MILK_SAMPLE_DETAIL.TYPE = 'B' Then TSPL_MILK_RECEIPT_DETAIL.ACC_WEIGHT Else 0 End [Buffalo Milk Qty (KG)]" + Environment.NewLine
            strSRNQuery += ",TSPL_MILK_SAMPLE_DETAIL.TYPE  As [Milk Type] "
        Else
            strSRNQuery += ",Case When TSPL_MILK_SAMPLE_DETAIL.FAT <= 5 Then TSPL_MILK_SAMPLE_DETAIL.FAT Else 0 End [Cow FAT(%)], Case When TSPL_MILK_SAMPLE_DETAIL.FAT <= 5 Then TSPL_MILK_SAMPLE_DETAIL.SNF Else 0 End [Cow SNF(%)]," &
        " Case When TSPL_MILK_SAMPLE_DETAIL.FAT > 5 Then TSPL_MILK_SAMPLE_DETAIL.FAT Else 0 End [Buffalo FAT(%)], Case When TSPL_MILK_SAMPLE_DETAIL.FAT > 5 Then TSPL_MILK_SAMPLE_DETAIL.SNF Else 0 End [Buffalo SNF(%)], Case When TSPL_MILK_SAMPLE_DETAIL.FAT <= 5 Then TSPL_MILK_RECEIPT_DETAIL.ACC_WEIGHT Else 0 End [Cow Milk Qty (KG)]," &
        " Case When TSPL_MILK_SAMPLE_DETAIL.FAT > 5 Then TSPL_MILK_RECEIPT_DETAIL.ACC_WEIGHT Else 0 End [Buffalo Milk Qty (KG)]" + Environment.NewLine
            strSRNQuery += ", Case When Coalesce(TSPL_MILK_SAMPLE_DETAIL.FAT, 0) <= 0 Then 'M' When Coalesce(TSPL_MILK_SAMPLE_DETAIL.FAT, 0) <= " + clsCommon.myCstr(SetCowFatPer) + "  Then 'C' Else 'M' End As [Milk Type]"
        End If
        strSRNQuery += ", TSPL_MILK_RECEIPT_HEAD.DOC_CODE As [Milk Receipt Code]," &
        " TSPL_MILK_RECEIPT_HEAD.MCC_CODE As MCC, TSPL_MCC_MASTER.MCC_NAME As [MCC Name],isnull(TSPL_MCC_MASTER.plant_code,'') As [Plant Code], isnull(tspl_location_master.location_desc,'') As [Plant Name], Convert(date,TSPL_MILK_RECEIPT_HEAD.DOC_DATE,103) As Date, " &
        " Convert(varchar,TSPL_MILK_RECEIPT_HEAD.DOC_DATE,103) As [Doc Date], Case When TSPL_MILK_RECEIPT_DETAIL.SHIFT = 'M' Then 'Morning' Else 'Evening' End As Shift, " &
        " TSPL_MILK_RECEIPT_DETAIL.ROUTE_CODE As [Route Code],tspl_mcc_route_master.Supervisor_Name as [SuperVisor Code], TSPL_MCC_ROUTE_MASTER.Route_Name As [Route Name], TSPL_MILK_RECEIPT_DETAIL.VEHICLE_CODE As [Vehicle Code]," &
        " TSPL_MILK_SRN_HEAD.VSP_CODE As [VSP Code], TSPL_VENDOR_MASTER.Vendor_Name As [VSP Name], TSPL_VENDOR_MASTER.Vendor_Group_Code As [Vendor Group Code],TSPL_VENDOR_GROUP.Group_Desc as [Vendor Group Desc] ,TSPL_VLC_MASTER_HEAD.VLC_Code As [Vlc Code]," &
        " TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader As [Vlc Uploader Code], TSPL_VLC_MASTER_HEAD.VLC_Name As [VLC Name], TSPL_MILK_RECEIPT_DETAIL.SAMPLE_NO As [Sample No], " &
        " TSPL_MILK_RECEIPT_DETAIL.NO_OF_CANS As [No Of Cans], TSPL_MILK_RECEIPT_DETAIL.MILK_WEIGHT As [Milk Weight],TSPL_MILK_RECEIPT_DETAIL.UOM_Code, TSPL_MILK_RECEIPT_DETAIL.ACC_WEIGHT As [Milk Weight(KG)]," &
        " TSPL_MILK_RECEIPT_DETAIL.ACC_WEIGHT_LTR As [Milk Weight(LTR)], TSPL_MILK_SAMPLE_DETAIL.FAT As [FAT(%)], TSPL_MILK_SAMPLE_DETAIL.SNF As [SNF(%)], TSPL_MILK_SAMPLE_DETAIL.CLR,  " &
        " TSPL_MILK_SRN_DETAIL.FAT_kg As [FAT(KG)], TSPL_MILK_SRN_DETAIL.SNF_kg As [SNF(KG)], Case When TSPL_MILK_SAMPLE_DETAIL.IS_MANUAL = '' Then 'Auto' Else TSPL_MILK_SAMPLE_DETAIL.IS_MANUAL End As [Sample Status]," &
        " TSPL_MILK_SRN_HEAD.DOC_CODE As [SRN No], Convert(decimal(18,2),TSPL_MILK_SRN_DETAIL.AMOUNT) As [SRN Amount], TSPL_MILK_SRN_DETAIL.RATE As [SRN Rate], TSPL_MILK_SRN_DETAIL.Qty As [SRN Qty], Case When TSPL_MILK_Shift_End_HEAD.DOC_CODE Is Null Then 'Open' Else 'Close' End [Shift Status],TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE as Invoice_no," &
        " convert(varchar,TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_DATE,103) as Invoice_Date , tspl_milk_receipt_detail.IS_MANUAL , tspl_milk_receipt_detail.MACHINE_NO,(CASE WHEN TSPL_MILK_SAMPLE_DETAIL.IS_MANUAL='Auto' THEN 'N' ELSE 'Y' END) AS IS_MILK_SAMPLE_MANUAL,TSPL_MILK_SRN_HEAD.Purchase_Order_No,TSPL_MILK_SRN_DETAIL.Head_Load_Amount " & strRejection & "  " &
        " ,TSPL_MILK_PRICE_SNF_DEDUCTION.Amount as SNF_Ded_Value,cast((TSPL_MILK_PRICE_SNF_DEDUCTION.Amount+TSPL_MILK_SRN_DETAIL.RATE) as decimal(18,2)) as SNF_Ded_Rate,cast((TSPL_MILK_PRICE_SNF_DEDUCTION.Amount+TSPL_MILK_SRN_DETAIL.RATE)*TSPL_MILK_SRN_DETAIL.ACC_Qty as decimal(18,2)) as SNF_Ded_Amount " + Environment.NewLine +
        " ,TabTSPL_FAT_SNF_UPLOADER_MASTER.Price_code,[Transporter Code], [Transporter Name],isnull(TSPL_MILK_PURCHASE_INVOICE_DETAIL.Handling_Charges_Amount,0) as Handling_Charges_Amount " &
        "  ,(isnull(TSPL_MILK_SRN_DETAIL.VSP_Commission_Apply,0)*TSPL_MILK_SRN_DETAIL.VSP_Commission_Amount)  as VSP_Commission_Amount,(isnull(TSPL_MILK_SRN_DETAIL.VSP_Deduction_Apply,0)*TSPL_MILK_SRN_DETAIL.VSP_Deduction_Amount)  as VSP_Deduction_Amount,TSPL_MILK_SRN_DETAIL.VSP_Day_Wise_Incentive ,case when isnull( TSPL_MILK_SRN_DETAIL.Sub_Standard,0)=1 then 'Sub Standard' else '' end as SubStandard,TSPL_Primary_Vehicle_Master.Vehicle,TSPL_MCC_MASTER.Mcc_Code_VLC_Uploader as [Mcc_Uploader_Code] " + Environment.NewLine +
        " From TSPL_MILK_RECEIPT_DETAIL " + Environment.NewLine +
        " Left Outer Join TSPL_MILK_RECEIPT_HEAD On TSPL_MILK_RECEIPT_HEAD.DOC_CODE = TSPL_MILK_RECEIPT_DETAIL.DOC_CODE " + Environment.NewLine +
        " Left Outer Join TSPL_MILK_SAMPLE_HEAD On TSPL_MILK_SAMPLE_HEAD.MILK_RECEIPT_CODE = TSPL_MILK_RECEIPT_HEAD.DOC_CODE" + Environment.NewLine +
        " Left Outer Join TSPL_MILK_SAMPLE_DETAIL On TSPL_MILK_SAMPLE_DETAIL.SAMPLE_NO = TSPL_MILK_RECEIPT_DETAIL.SAMPLE_NO And TSPL_MILK_SAMPLE_DETAIL.DOC_CODE = TSPL_MILK_SAMPLE_HEAD.DOC_CODE " &
        " Left Outer Join TSPL_MILK_SRN_HEAD On TSPL_MILK_SRN_HEAD.MILK_SAMPLE_CODE = TSPL_MILK_SAMPLE_HEAD.DOC_CODE And TSPL_MILK_SRN_HEAD.SAMPLE_NO = TSPL_MILK_SAMPLE_DETAIL.SAMPLE_NO " + Environment.NewLine +
        " Left Outer Join TSPL_MILK_SRN_DETAIL On TSPL_MILK_SRN_HEAD.DOC_CODE = TSPL_MILK_SRN_DETAIL.DOC_CODE" + Environment.NewLine +
        " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.item_code=TSPL_MILK_SRN_DETAIL.item_code " + Environment.NewLine +
        " Left Outer Join TSPL_MILK_PURCHASE_INVOICE_DETAIL On TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_CODE = TSPL_MILK_SRN_HEAD.DOC_CODE " + Environment.NewLine +
        " Left Outer Join TSPL_MILK_PURCHASE_INVOICE_HEAD On TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE = TSPL_MILK_PURCHASE_INVOICE_DETAIL.DOC_CODE " &
        " Left Outer Join TSPL_MCC_MASTER On TSPL_MCC_MASTER.MCC_Code = TSPL_MILK_RECEIPT_HEAD.MCC_CODE " + Environment.NewLine +
        " Left Outer Join TSPL_VLC_MASTER_HEAD On TSPL_VLC_MASTER_HEAD.VLC_Code = TSPL_MILK_RECEIPT_DETAIL.VLC_CODE" + Environment.NewLine +
        " Left Outer Join TSPL_VENDOR_MASTER On TSPL_VENDOR_MASTER.Vendor_Code = TSPL_MILK_RECEIPT_DETAIL.VSP_CODE" + Environment.NewLine +
        " left outer join TSPL_VENDOR_GROUP on TSPL_VENDOR_MASTER.Vendor_Group_Code = TSPL_VENDOR_GROUP.Ven_Group_Code " + Environment.NewLine +
        " Left Outer Join TSPL_MCC_ROUTE_MASTER On TSPL_MCC_ROUTE_MASTER.Route_Code = TSPL_MILK_RECEIPT_DETAIL.ROUTE_CODE" + Environment.NewLine +
        " left join (select TSPL_Primary_Vehicle_Master.vendor_code as [Transporter Code],tspl_vendor_master.vendor_name as [Transporter Name],TSPL_Primary_Vehicle_Master.mcc_code,TSPL_Primary_Vehicle_Master.vehicle_code from TSPL_Primary_Vehicle_Master left outer join tspl_vendor_master on tspl_vendor_master.vendor_code=TSPL_Primary_Vehicle_Master.vendor_code and tspl_vendor_master.form_type='PTM' left outer join tspl_mcc_master on tspl_mcc_master.mcc_code=TSPL_Primary_Vehicle_Master.mcc_code) as t1 on t1.vehicle_code=TSPL_MCC_ROUTE_MASTER.Vehicle_Code " + Environment.NewLine +
        " Left Outer Join TSPL_Primary_Vehicle_Master On TSPL_Primary_Vehicle_Master.Vehicle_Code = TSPL_MCC_ROUTE_MASTER.Vehicle_Code " + Environment.NewLine +
        " Left Outer Join TSPL_MILK_Shift_End_HEAD On TSPL_MILK_Shift_End_HEAD.MCC_CODE = TSPL_MILK_RECEIPT_HEAD.MCC_CODE " + Environment.NewLine +
        " And convert(date,TSPL_MILK_Shift_End_HEAD.DOC_DATE,103) = convert(date,TSPL_MILK_RECEIPT_HEAD.DOC_DATE,103) " + Environment.NewLine +
        " And TSPL_MILK_Shift_End_HEAD.SHIFT = TSPL_MILK_RECEIPT_HEAD.SHIFT " + Environment.NewLine +
        " Left Outer Join TSPL_MILK_Shift_End_Route_DETAIL On TSPL_MILK_Shift_End_Route_DETAIL.DOC_CODE = TSPL_MILK_Shift_End_HEAD.DOC_CODE " + Environment.NewLine +
        " And TSPL_MILK_Shift_End_Route_DETAIL.Route_CODE = TSPL_MCC_ROUTE_MASTER.Route_Code " + Environment.NewLine +
        "left outer join (select code,max(Price_code) as Price_code from  TSPL_FAT_SNF_UPLOADER_MASTER group by code) as TabTSPL_FAT_SNF_UPLOADER_MASTER on TabTSPL_FAT_SNF_UPLOADER_MASTER.code=TSPL_MILK_SRN_DETAIL.Price_Code" + Environment.NewLine +
        "left outer join TSPL_MILK_PRICE_SNF_DEDUCTION on TSPL_MILK_PRICE_SNF_DEDUCTION.Price_code=TabTSPL_FAT_SNF_UPLOADER_MASTER.Price_code and cast(TSPL_MILK_SRN_DETAIL.SNF_PER as decimal(18,1))=TSPL_MILK_PRICE_SNF_DEDUCTION.Per" + Environment.NewLine +
        " left join tspl_location_master on tspl_location_master.location_code=TSPL_MCC_MASTER.Plant_Code " &
        " where 2 = 2 "
        strSRNQuery += " and Cast(TSPL_MILK_RECEIPT_HEAD.DOC_DATE as Date) >='" + clsCommon.GetPrintDate(FromDate, "dd/MMM/yyyy") + "' and Cast(TSPL_MILK_RECEIPT_HEAD.DOC_DATE as date) <='" + clsCommon.GetPrintDate(ToDate, "dd/MMM/yyyy") + "'"
        If clsCommon.CompairString(FromShift, "E") = CompairStringResult.Equal Then
            strSRNQuery += " and 2=( case when Cast(TSPL_MILK_RECEIPT_HEAD.DOC_DATE as Date) >= '" + clsCommon.GetPrintDate(FromDate, "dd/MMM/yyyy") + "' and Cast(TSPL_MILK_RECEIPT_HEAD.DOC_DATE as Date) <= '" + clsCommon.GetPrintDate(FromDate, "dd/MMM/yyyy") + "' and TSPL_MILK_RECEIPT_DETAIL.SHIFT='M' then 3 else 2 end  )"
        End If
        If clsCommon.CompairString(ToShift, "M") = CompairStringResult.Equal Then
            strSRNQuery += " and 2=( case when Cast(TSPL_MILK_RECEIPT_HEAD.DOC_DATE as Date) >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(ToDate), "dd/MMM/yyyy") + "' and Cast(TSPL_MILK_RECEIPT_HEAD.DOC_DATE as Date) <= '" + clsCommon.GetPrintDate(ToDate, "dd/MMM/yyyy") + "' and TSPL_MILK_RECEIPT_DETAIL.SHIFT='E' then 3 else 2 end  )"
        End If
        If clsCommon.CompairString(SRNAmounType, "Zero") = CompairStringResult.Equal Then
            strSRNQuery += " and  TSPL_MILK_SRN_DETAIL.AMOUNT = 0 "
        ElseIf clsCommon.CompairString(SRNAmounType, "NonZero") = CompairStringResult.Equal Then
            strSRNQuery += " and TSPL_MILK_SRN_DETAIL.AMOUNT <> 0 "
        End If

        If Not ShowVLCUploaderData Then
            strSRNQuery += " and TSPL_MILK_RECEIPT_DETAIL.Against_Uploader_TR_No is null "
        End If
        If clsCommon.myLen(ReceivingUOM) > 0 Then
            strSRNQuery += " and TSPL_MILK_RECEIPT_DETAIL.UOM_Code='" + ReceivingUOM + "'"
        End If

        If arrMCC IsNot Nothing AndAlso arrMCC.Count > 0 Then
            strSRNQuery += "and TSPL_MILK_RECEIPT_HEAD.MCC_Code  IN (" + clsCommon.GetMulcallString(arrMCC) + ") "
        Else
            strSRNQuery += "And TSPL_MILK_RECEIPT_HEAD.mcc_Code in (" & StrPermission & ")"
        End If
        If arrRoute IsNot Nothing AndAlso arrRoute.Count > 0 Then
            strSRNQuery += " and TSPL_MILK_RECEIPT_DETAIL .Route_Code in (" + clsCommon.GetMulcallString(arrRoute) + ")  "
        End If
        If arrVLC IsNot Nothing AndAlso arrVLC.Count > 0 Then
            strSRNQuery += " and TSPL_MILK_RECEIPT_DETAIL.VLC_CODE in (" + clsCommon.GetMulcallString(arrVLC) + ")  "
        End If
        Return strSRNQuery
    End Function

    Public Shared Function GetMCCRegisterRejectionQuery(ByVal FromDate As DateTime, ByVal ToDate As DateTime, ByVal FromShift As String, ByVal ToShift As String, ByVal StrPermission As String, ByVal arrMCC As ArrayList, ByVal arrRoute As ArrayList, ByVal arrVLC As ArrayList, ByVal ReceivingUOM As String, ByVal SetCowFatPer As Decimal) As String
        Dim strRejectionQuery As String = Nothing
        'Dim SetCowFatPer As Integer

        strRejectionQuery = "  Select TSPL_MCC_MASTER.MCC_Type as [MCC Type],case when TSPL_MCC_MASTER.is_Mcc=1 then 'MCC' else 'BMCC' end [Chilling Center] ,TSPL_MILK_SRN_DETAIL.Item_Code,TSPL_ITEM_MASTER.Item_Desc,TSPL_MILK_SRN_DETAIL.EMP_Amount,TSPL_MILK_SRN_DETAIL.TIP_Amount,TSPL_MILK_SRN_DETAIL.Service_Charge_Amount,Case When TSPL_MILK_REJECT_DETAIL.FAT < 5 Then TSPL_MILK_REJECT_DETAIL.FAT Else 0 End [Cow FAT(%)], " &
                " Case When TSPL_MILK_REJECT_DETAIL.FAT < 5 Then TSPL_MILK_REJECT_DETAIL.SNF Else 0 End [Cow SNF(%)], " &
                " Case When TSPL_MILK_REJECT_DETAIL.FAT > 5 Then TSPL_MILK_REJECT_DETAIL.FAT Else 0 End [Buffalo FAT(%)], " &
                " Case When TSPL_MILK_REJECT_DETAIL.FAT > 5 Then TSPL_MILK_REJECT_DETAIL.SNF Else 0 End [Buffalo SNF(%)], " &
                " Case When TSPL_MILK_REJECT_DETAIL.FAT <= 5 Then TSPL_MILK_REJECT_DETAIL.ACC_WEIGHT_KG Else 0 End [Cow Milk Qty (KG)], " &
                " Case When TSPL_MILK_REJECT_DETAIL.FAT > 5 Then TSPL_MILK_REJECT_DETAIL.ACC_WEIGHT_LTR Else 0 End [Buffalo Milk Qty (KG)], "

        strRejectionQuery += " case when TSPL_MILK_REJECT_TYPE.Type is not null  then TSPL_MILK_REJECT_TYPE.Type When Coalesce(TSPL_MILK_REJECT_DETAIL.FAT, 0) <= 0 Then 'M' When Coalesce(TSPL_MILK_REJECT_DETAIL.FAT, 0) <= " + clsCommon.myCstr(SetCowFatPer) + "  Then 'C' Else 'M' End As [Milk Type], "
        strRejectionQuery += " TSPL_MILK_REJECT_HEAD.DOC_CODE As [Milk Receipt Code], TSPL_MILK_REJECT_HEAD.MCC_CODE As MCC, TSPL_MCC_MASTER.MCC_NAME As [MCC Name],isnull(TSPL_MCC_MASTER.plant_code,'') As [Plant Code], isnull(tspl_location_master.location_desc,'') As [Plant Name], " &
        " Convert(date,TSPL_MILK_REJECT_HEAD.DOC_DATE,103) As Date,  Convert(varchar,TSPL_MILK_REJECT_HEAD.DOC_DATE,103) As [Doc Date], Case When TSPL_MILK_REJECT_HEAD.SHIFT = 'M' Then 'Morning' Else 'Evening' End As Shift,  TSPL_MILK_REJECT_DETAIL.ROUTE_CODE As [Route Code],tspl_mcc_route_master.Supervisor_Name as [SuperVisor Code], TSPL_MCC_ROUTE_MASTER.Route_Name As [Route Name], TSPL_MILK_REJECT_DETAIL.VEHICLE_CODE As [Vehicle Code], TSPL_MILK_REJECT_DETAIL.VSP_CODE As [VSP Code], TSPL_VENDOR_MASTER.Vendor_Name As [VSP Name],TSPL_VENDOR_MASTER.Vendor_Group_Code As [Vendor Group Code],TSPL_VENDOR_GROUP.Group_Desc as [Vendor Group Desc] ,TSPL_VLC_MASTER_HEAD.VLC_Code As [Vlc Code], TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader As [Vlc Uploader Code], TSPL_VLC_MASTER_HEAD.VLC_Name As [VLC Name], TSPL_MILK_REJECT_DETAIL.SAMPLE_NO As [Sample No],  TSPL_MILK_REJECT_DETAIL.NO_OF_CANS As [No Of Cans], TSPL_MILK_REJECT_DETAIL.MILK_WEIGHT As [Milk Weight],TSPL_MILK_REJECT_DETAIL.UOM_Code, TSPL_MILK_REJECT_DETAIL.ACC_WEIGHT_KG As [Milk Weight(KG)], TSPL_MILK_REJECT_DETAIL.ACC_WEIGHT_LTR As [Milk Weight(LTR)], TSPL_MILK_REJECT_DETAIL.FAT As [FAT(%)], TSPL_MILK_REJECT_DETAIL.SNF As [SNF(%)],0 as CLR, Convert(decimal(18,3), TSPL_MILK_REJECT_DETAIL.FAT * TSPL_MILK_REJECT_DETAIL.ACC_WEIGHT_KG / 100) As [FAT(KG)], " &
        " Convert(decimal(18,3),TSPL_MILK_REJECT_DETAIL.SNF * TSPL_MILK_REJECT_DETAIL.ACC_WEIGHT_KG / 100) As [SNF(KG)], '' As [Sample Status], " &
        " TSPL_MILK_SRN_HEAD.DOC_CODE As [SRN No], Convert(decimal(18,2),TSPL_MILK_SRN_DETAIL.AMOUNT) As [SRN Amount], TSPL_MILK_SRN_DETAIL.RATE As [SRN Rate], " &
        " TSPL_MILK_SRN_DETAIL.Qty As [SRN Qty], Case When TSPL_MILK_Shift_End_HEAD.DOC_CODE Is Null Then 'Open' Else 'Close' End [Shift Status], " &
        " TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE as Invoice_no, convert(varchar,TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_DATE,103) as Invoice_Date , " &
        " '' as IS_MANUAL ,'' as MACHINE_NO ,'' as IS_MILK_SAMPLE_MANUAL,TSPL_MILK_SRN_HEAD.Purchase_Order_No,TSPL_MILK_SRN_DETAIL.Head_Load_Amount,TSPL_MILK_REJECT_TYPE.Code as RejectType, " &
        " case when TSPL_MILK_REJECT_DETAIL.Is_Return=0 then '' when TSPL_MILK_REJECT_DETAIL.Is_Return=1 then 'Return' when TSPL_MILK_REJECT_DETAIL.Is_Return=2 then 'Drain' when TSPL_MILK_REJECT_DETAIL.Is_Return=3 then 'COB'  end as RejectReason,TSPL_MILK_REJECT_DETAIL.Defaulter  " + Environment.NewLine +
        " ,TSPL_MILK_PRICE_SNF_DEDUCTION.Amount as SNF_Ded_Value,cast((TSPL_MILK_PRICE_SNF_DEDUCTION.Amount+TSPL_MILK_SRN_DETAIL.RATE) as decimal(18,2)) as SNF_Ded_Rate,cast((TSPL_MILK_PRICE_SNF_DEDUCTION.Amount+TSPL_MILK_SRN_DETAIL.RATE)*TSPL_MILK_SRN_DETAIL.ACC_Qty as decimal(18,2)) as SNF_Ded_Amount " + Environment.NewLine +
        " ,TabTSPL_FAT_SNF_UPLOADER_MASTER.Price_code,[Transporter Code], [Transporter Name],isnull(TSPL_MILK_PURCHASE_INVOICE_DETAIL.Handling_Charges_Amount,0) as Handling_Charges_Amount " + Environment.NewLine +
        " ,(isnull(TSPL_MILK_SRN_DETAIL.VSP_Commission_Apply,0)*TSPL_MILK_SRN_DETAIL.VSP_Commission_Amount)  as VSP_Commission_Amount ,(isnull(TSPL_MILK_SRN_DETAIL.VSP_Deduction_Apply,0)*TSPL_MILK_SRN_DETAIL.VSP_Deduction_Amount)  as VSP_Deduction_Amount,TSPL_MILK_SRN_DETAIL.VSP_Day_Wise_Incentive,case when isnull( TSPL_MILK_SRN_DETAIL.Sub_Standard,0)=1 then 'Sub Standard' else '' end as SubStandard,t1.Vehicle,TSPL_MCC_MASTER.Mcc_Code_VLC_Uploader as [Mcc_Uploader_Code] " + Environment.NewLine +
        " From   TSPL_MILK_REJECT_DETAIL " + Environment.NewLine +
        " Left Outer Join TSPL_MILK_REJECT_HEAD On TSPL_MILK_REJECT_HEAD.DOC_CODE = TSPL_MILK_REJECT_DETAIL.DOC_CODE " + Environment.NewLine +
        " left outer join TSPL_MILK_SRN_HEAD on TSPL_MILK_REJECT_HEAD.DOC_CODe=TSPL_MILK_SRN_HEAD.Against_Reject_No and TSPL_MILK_SRN_HEAD.SAMPLE_NO=TSPL_MILK_REJECT_DETAIL.SAMPLE_NO " + Environment.NewLine +
        " Left Outer Join TSPL_MILK_SRN_DETAIL On TSPL_MILK_SRN_HEAD.DOC_CODE = TSPL_MILK_SRN_DETAIL.DOC_CODE " + Environment.NewLine +
        " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.item_code=TSPL_MILK_SRN_DETAIL.item_code" + Environment.NewLine +
        " Left Outer Join TSPL_MILK_PURCHASE_INVOICE_DETAIL On TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_CODE = TSPL_MILK_SRN_HEAD.DOC_CODE " + Environment.NewLine +
        " Left Outer Join TSPL_MILK_PURCHASE_INVOICE_HEAD On TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE = TSPL_MILK_PURCHASE_INVOICE_DETAIL.DOC_CODE " + Environment.NewLine +
        " Left Outer Join TSPL_MCC_MASTER On TSPL_MCC_MASTER.MCC_Code = TSPL_MILK_REJECT_HEAD.MCC_CODE " + Environment.NewLine +
        " Left Outer Join TSPL_VLC_MASTER_HEAD On  TSPL_VLC_MASTER_HEAD.VLC_Code = TSPL_MILK_REJECT_DETAIL.VLC_CODE " + Environment.NewLine +
        "   " + Environment.NewLine +
        " Left Outer Join TSPL_VENDOR_MASTER On TSPL_VENDOR_MASTER.Vendor_Code = TSPL_MILK_REJECT_DETAIL.VSP_CODE " + Environment.NewLine +
        "  left outer join TSPL_VENDOR_GROUP on TSPL_VENDOR_MASTER.Vendor_Group_Code = TSPL_VENDOR_GROUP.Ven_Group_Code " + Environment.NewLine +
        " Left Outer Join TSPL_MCC_ROUTE_MASTER On TSPL_MCC_ROUTE_MASTER.Route_Code = TSPL_MILK_REJECT_DETAIL.ROUTE_CODE " + Environment.NewLine +
        " Left Outer Join (select TSPL_Primary_Vehicle_Master.vendor_code as [Transporter Code],tspl_vendor_master.vendor_name as [Transporter Name],TSPL_Primary_Vehicle_Master.mcc_code,TSPL_Primary_Vehicle_Master.vehicle_code,TSPL_Primary_Vehicle_Master.Vehicle from TSPL_Primary_Vehicle_Master left outer join tspl_vendor_master on tspl_vendor_master.vendor_code=TSPL_Primary_Vehicle_Master.vendor_code and tspl_vendor_master.form_type='PTM' left outer join tspl_mcc_master on tspl_mcc_master.mcc_code=TSPL_Primary_Vehicle_Master.mcc_code) as t1 on t1.vehicle_code=TSPL_MCC_ROUTE_MASTER.Vehicle_Code " + Environment.NewLine +
        " Left Outer Join TSPL_MILK_Shift_End_HEAD On TSPL_MILK_Shift_End_HEAD.MCC_CODE = TSPL_MILK_REJECT_HEAD.MCC_CODE " &
        " And convert(date,TSPL_MILK_Shift_End_HEAD.DOC_DATE,103) = convert(date,TSPL_MILK_REJECT_HEAD.DOC_DATE,103) " &
        " And TSPL_MILK_Shift_End_HEAD.SHIFT = TSPL_MILK_REJECT_HEAD.SHIFT " + Environment.NewLine +
        " Left Outer Join TSPL_MILK_Shift_End_Route_DETAIL On TSPL_MILK_Shift_End_Route_DETAIL.DOC_CODE = TSPL_MILK_Shift_End_HEAD.DOC_CODE " &
        " And TSPL_MILK_Shift_End_Route_DETAIL.Route_CODE = TSPL_MCC_ROUTE_MASTER.Route_Code " &
        " left outer join (select code,max(Price_code) as Price_code from  TSPL_FAT_SNF_UPLOADER_MASTER group by code) as TabTSPL_FAT_SNF_UPLOADER_MASTER on TabTSPL_FAT_SNF_UPLOADER_MASTER.code=TSPL_MILK_SRN_DETAIL.Price_Code " + Environment.NewLine +
        " left outer join TSPL_MILK_PRICE_SNF_DEDUCTION on TSPL_MILK_PRICE_SNF_DEDUCTION.Price_code=TabTSPL_FAT_SNF_UPLOADER_MASTER.Price_code and cast(TSPL_MILK_SRN_DETAIL.SNF_PER as decimal(18,1))=TSPL_MILK_PRICE_SNF_DEDUCTION.Per " + Environment.NewLine +
        " left join tspl_location_master on tspl_location_master.location_code=TSPL_MCC_MASTER.Plant_Code " &
        " left join TSPL_MILK_REJECT_TYPE on TSPL_MILK_REJECT_TYPE.code=TSPL_MILK_REJECT_DETAIL.Reject_Type " &
        " where 2=2  "
        If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "UDL") = CompairStringResult.Equal Then
            strRejectionQuery += " and Against_Reject_No <> ''"
        End If
        strRejectionQuery += " and TSPL_MILK_REJECT_HEAD.DOC_DATE >='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(FromDate), "dd/MMM/yyyy hh:mm tt") + "' and TSPL_MILK_REJECT_HEAD.DOC_DATE <='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(ToDate), "dd/MMM/yyyy hh:mm tt") + "'"

        If clsCommon.CompairString(FromShift, "E") = CompairStringResult.Equal Then
            strRejectionQuery += " and 2=( case when TSPL_MILK_REJECT_HEAD.DOC_DATE >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(FromDate), "dd/MMM/yyyy hh:mm tt") + "' and TSPL_MILK_REJECT_HEAD.DOC_DATE <='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(FromDate), "dd/MMM/yyyy hh:mm tt") + "' and TSPL_MILK_REJECT_HEAD.SHIFT='M' then 3 else 2 end  )"
        End If
        If clsCommon.CompairString(ToShift, "M") = CompairStringResult.Equal Then
            strRejectionQuery += " and 2=( case when TSPL_MILK_REJECT_HEAD.DOC_DATE >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(ToDate), "dd/MMM/yyyy hh:mm tt") + "' and TSPL_MILK_REJECT_HEAD.DOC_DATE <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(ToDate), "dd/MMM/yyyy hh:mm tt") + "' and TSPL_MILK_REJECT_HEAD.SHIFT='E' then 3 else 2 end  )"
        End If
        If clsCommon.myLen(ReceivingUOM) > 0 Then
            strRejectionQuery += " and TSPL_MILK_REJECT_DETAIL.UOM_Code='" + ReceivingUOM + "'"
        End If

        If arrMCC IsNot Nothing AndAlso arrMCC.Count > 0 Then
            strRejectionQuery += "and TSPL_MILK_REJECT_HEAD.MCC_Code  IN (" + clsCommon.GetMulcallString(arrMCC) + ") "
        Else
            If clsCommon.myLen(StrPermission) > 0 Then
                strRejectionQuery += "And TSPL_MILK_REJECT_HEAD.mcc_Code in (" & StrPermission & ") "
            End If
        End If
        If arrRoute IsNot Nothing AndAlso arrRoute.Count > 0 Then
            strRejectionQuery += " and TSPL_MILK_REJECT_DETAIL.Route_Code in (" + clsCommon.GetMulcallString(arrRoute) + ")  "
        End If
        If arrVLC IsNot Nothing AndAlso arrVLC.Count > 0 Then
            strRejectionQuery += " and TSPL_MILK_REJECT_DETAIL.VLC_CODE in (" + clsCommon.GetMulcallString(arrVLC) + ")  "
        End If

        Return strRejectionQuery
    End Function
End Class

Public Class clsMilkRejectDetail
#Region "Variables"
    Public DOC_CODE As String
    Public Item_CODE As String
    Public SAMPLE_NO As Integer
    Public VLC_CODE As String
    Public ROUTE_CODE As String
    Public VSP_CODE As String
    Public VEHICLE_CODE As String
    Public Other_VEHICLE As Boolean
    Public Other_VLC As Boolean
    Public NO_OF_CANS As Integer
    Public MILK_WEIGHT As Decimal
    Public ACC_WEIGHT_KG As Decimal
    Public ACC_WEIGHT_LTR As Decimal
    Public Conversion_Factor As Double = 0
    Public UOM_Code As String = String.Empty
    Public FAT As Decimal = 0
    Public SNF As Decimal = 0
    Public Reject_Type As String
    Public SNF_RATE As Decimal = 0
    Public SNF_Amount As Decimal = 0
    Public RATE As Decimal = 0
    Public Amount As Decimal = 0
    Public FAT_RATE As Decimal = 0
    Public FAT_Amount As Decimal = 0

    Public SNF_Deduction_Per As Decimal = 0
    Public SNF_Amount_After_Deduction As Decimal = 0
    Public SNF_Deduction_Amount As Decimal = 0
    Public FAT_Deduction_Per As Decimal = 0
    Public FAT_Deduction_Amount As Decimal = 0
    Public FAT_Amount_After_Deduction As Decimal = 0
    Public Is_Return As Integer ''0-NA,1=-Return,2-Drain
    Public Price_Code As String
    Public VLC_CODE_Uploader_Code As String
    Public Defaulter As String
    Public dblPenaltyPerUnit As Decimal = 0 ''Not a TableColumn

    Public dclFATSNFDeductionMixMilkFATMinValue As Decimal = 3.0 ''Not a TableColumn
    Public dclFATSNFDeductionMixMilkFATMaxValue As Decimal = 4.5 ''Not a TableColumn
    Public dclFATSNFDeductionMixMilkSNFMinValue As Decimal = 7.0 ''Not a TableColumn
    Public dclFATSNFDeductionMixMilkSNFMaxValue As Decimal = 7.2 ''Not a TableColumn
    Public dclFATSNFDeductionMixMilkDeductionPer As Decimal = 20 ''Not a TableColumn
    Public Against_Shift_Uploader_TR_No As String

#End Region

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal objH As clsMilkRejectHead, ByVal settAlwaysVSPDefaulter As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim settMilkCollectionPickBulkRoute As Boolean = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MilkCollectionPickBulkRoute, clsFixedParameterCode.MilkCollectionPickBulkRoute, trans)) = 1)
        Dim isPickCLRInsteadOfSNF As Boolean = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MilkProcuremntPickCLRInsteadOfSNF, clsFixedParameterCode.MilkProcuremntPickCLRInsteadOfSNF, trans)) > 0)
        Dim PickPriceFromFATAndSNF As Boolean = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MilkProcuremntPickCLRInsteadOfSNF, clsFixedParameterCode.PickPriceFromFATAndSNF, trans)) > 0)
        Dim corrFactor As Double = clsFixedParameter.GetData(clsFixedParameterType.defaultCorrectionFactor, clsFixedParameterCode.MilkSetting, trans)
        If (objH.Arr IsNot Nothing AndAlso objH.Arr.Count > 0) Then
            For Each obj As clsMilkRejectDetail In objH.Arr
                Dim qry As String = "delete from TSPL_MILK_REJECT_DETAIL where DOC_CODE='" + strDocNo + "' and SAMPLE_NO='" + clsCommon.myCstr(obj.SAMPLE_NO) + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)

                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "DOC_CODE", strDocNo)
                clsCommon.AddColumnsForChange(coll, "Item_CODE", obj.Item_CODE)
                clsCommon.AddColumnsForChange(coll, "SAMPLE_NO", obj.SAMPLE_NO)
                clsCommon.AddColumnsForChange(coll, "VLC_CODE", obj.VLC_CODE)
                clsCommon.AddColumnsForChange(coll, "ROUTE_CODE", obj.ROUTE_CODE)
                clsCommon.AddColumnsForChange(coll, "VSP_CODE", obj.VSP_CODE)
                clsCommon.AddColumnsForChange(coll, "VEHICLE_CODE", obj.VEHICLE_CODE)
                clsCommon.AddColumnsForChange(coll, "Other_VEHICLE", IIf(obj.Other_VEHICLE, 1, 0))
                clsCommon.AddColumnsForChange(coll, "Other_VLC", IIf(obj.Other_VLC, 1, 0))
                clsCommon.AddColumnsForChange(coll, "NO_OF_CANS", obj.NO_OF_CANS)
                clsCommon.AddColumnsForChange(coll, "MILK_WEIGHT", obj.MILK_WEIGHT)
                clsCommon.AddColumnsForChange(coll, "ACC_WEIGHT_KG", obj.ACC_WEIGHT_KG)
                clsCommon.AddColumnsForChange(coll, "ACC_WEIGHT_LTR", obj.ACC_WEIGHT_LTR)
                clsCommon.AddColumnsForChange(coll, "Conversion_factor", obj.Conversion_Factor)
                clsCommon.AddColumnsForChange(coll, "UOM_Code", obj.UOM_Code)
                clsCommon.AddColumnsForChange(coll, "FAT", obj.FAT)
                clsCommon.AddColumnsForChange(coll, "SNF", obj.SNF)
                clsCommon.AddColumnsForChange(coll, "Reject_Type", obj.Reject_Type)
                clsCommon.AddColumnsForChange(coll, "Is_Return", obj.Is_Return)
                clsCommon.AddColumnsForChange(coll, "SNF_Deduction_Per", obj.SNF_Deduction_Per)
                clsCommon.AddColumnsForChange(coll, "FAT_Deduction_Per", obj.FAT_Deduction_Per)
                clsCommon.AddColumnsForChange(coll, "Against_Shift_Uploader_TR_No", obj.Against_Shift_Uploader_TR_No, True)
                If settAlwaysVSPDefaulter Then
                    If clsCommon.myLen(obj.Defaulter) <= 0 Then
                        obj.Defaulter = "VSP"
                    End If
                End If
                clsCommon.AddColumnsForChange(coll, "Defaulter", obj.Defaulter)

                Dim dtMilkReject As DataTable = clsDBFuncationality.GetDataTable("select Applicable_On,Applicable_Per,Item_Code from TSPL_MILK_REJECT_TYPE where Code='" + obj.Reject_Type + "'", trans)
                Dim MRRejectApplicableOn As Decimal = 0
                Dim MRPenaltyPerUnit As Decimal = 0
                Dim MRItemCode As String = ""
                If dtMilkReject IsNot Nothing AndAlso dtMilkReject.Rows.Count > 0 Then
                    MRRejectApplicableOn = clsCommon.myCDecimal(dtMilkReject.Rows(0)("Applicable_On"))
                    MRPenaltyPerUnit = clsCommon.myCDecimal(dtMilkReject.Rows(0)("Applicable_Per"))
                    MRItemCode = clsCommon.myCstr(dtMilkReject.Rows(0)("Item_Code"))
                End If
                If obj.Is_Return = 1 OrElse obj.Is_Return = 2 OrElse obj.Is_Return = 3 Then
                    If settAlwaysVSPDefaulter Then
                        obj.Item_CODE = clsFixedParameter.GetData(clsFixedParameterType.MCCDefaultMilkItem, clsFixedParameterCode.MilkSetting, trans)
                        coll.Remove("Item_CODE")
                        clsCommon.AddColumnsForChange(coll, "Item_CODE", obj.Item_CODE)
                    End If
                    clsCommon.AddColumnsForChange(coll, "SNF_RATE", 0)
                    clsCommon.AddColumnsForChange(coll, "SNF_Amount", 0)
                    clsCommon.AddColumnsForChange(coll, "FAT_RATE", 0)
                    clsCommon.AddColumnsForChange(coll, "FAT_Amount", 0)
                    clsCommon.AddColumnsForChange(coll, "RATE", obj.dblPenaltyPerUnit)
                    clsCommon.AddColumnsForChange(coll, "Amount", Math.Round(obj.dblPenaltyPerUnit * obj.MILK_WEIGHT, 2, MidpointRounding.ToEven))
                    If Math.Round(obj.dblPenaltyPerUnit * obj.MILK_WEIGHT, 2, MidpointRounding.ToEven) <= 0 Then
                        If Not obj.Is_Return = 3 Then
                            If Not clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "UDP") = CompairStringResult.Equal Then
                                Throw New Exception("Amount cannot be zero at sample no" + clsCommon.myCstr(obj.SAMPLE_NO))
                            End If
                        End If
                    End If
                ElseIf MRRejectApplicableOn = 1 Then ''1-Reject Type is Rate 
                    obj.Item_CODE = MRItemCode
                    coll.Remove("Item_CODE")
                    clsCommon.AddColumnsForChange(coll, "Item_CODE", obj.Item_CODE)

                    obj.dblPenaltyPerUnit = MRPenaltyPerUnit
                    clsCommon.AddColumnsForChange(coll, "SNF_RATE", 0)
                    clsCommon.AddColumnsForChange(coll, "SNF_Amount", 0)
                    clsCommon.AddColumnsForChange(coll, "FAT_RATE", 0)
                    clsCommon.AddColumnsForChange(coll, "FAT_Amount", 0)
                    clsCommon.AddColumnsForChange(coll, "RATE", obj.dblPenaltyPerUnit)
                    clsCommon.AddColumnsForChange(coll, "Amount", Math.Round(obj.dblPenaltyPerUnit * obj.MILK_WEIGHT, 2, MidpointRounding.ToEven))
                Else
                    Dim TempSNFPer As Decimal = obj.SNF
                    If objCommonVar.PricePlan = 5 Then
                        TempSNFPer = Math.Truncate(obj.SNF * 10) / 10
                    End If
                    If isPickCLRInsteadOfSNF Then
                        If PickPriceFromFATAndSNF Then
                            TempSNFPer = clsCommon.myRoundOFF(clsEkoPro.getSnfOnCalculation(obj.FAT, TempSNFPer, corrFactor), 1, 4)
                        End If
                    End If
                    qry = " select top 1 TSPL_MILK_PRICE_MASTER.*,TSPL_FAT_SNF_UPLOADER_MASTER.Rate,TSPL_FAT_SNF_UPLOADER_MASTER.Code from TSPL_FAT_SNF_UPLOADER_MASTER  " + Environment.NewLine +
                    " left outer join TSPL_FAT_SNF_UPLOADER_MCC on TSPL_FAT_SNF_UPLOADER_MCC.Code=TSPL_FAT_SNF_UPLOADER_MASTER.Code" + Environment.NewLine +
                    " left outer join TSPL_MILK_PRICE_MASTER on TSPL_MILK_PRICE_MASTER.Price_Code=TSPL_FAT_SNF_UPLOADER_MASTER.Price_Code" + Environment.NewLine +
                    " left outer join TSPL_FAT_SNF_UPLOADER_VLC on TSPL_FAT_SNF_UPLOADER_VLC.Code=TSPL_FAT_SNF_UPLOADER_MASTER.Code " + Environment.NewLine +
                    " where TSPL_FAT_SNF_UPLOADER_MASTER.posted=1 and FAT='" + clsCommon.myCstr(obj.FAT) + "'"
                    If isPickCLRInsteadOfSNF AndAlso Not PickPriceFromFATAndSNF Then
                        qry += " And TSPL_FAT_SNF_UPLOADER_MASTER.CLR='" + clsCommon.myCstr(TempSNFPer) + "'"
                    Else
                        qry += " And SNF='" + clsCommon.myCstr(TempSNFPer) + "'"
                    End If

                    If Not settMilkCollectionPickBulkRoute Then
                        qry += " And TSPL_FAT_SNF_UPLOADER_MCC.MCC_Code='" + objH.MCC_CODE + "' and TSPL_FAT_SNF_UPLOADER_VLC.VLC_Code='" + obj.VLC_CODE + "'"
                    End If
                    qry += "  And (TSPL_FAT_SNF_UPLOADER_MASTER.Date < '" + clsCommon.GetPrintDate(objH.DOC_DATE, "dd/MMM/yyyy hh:mm tt") + "' or (TSPL_FAT_SNF_UPLOADER_MASTER.Date='" + clsCommon.GetPrintDate(objH.DOC_DATE, "dd/MMM/yyyy hh:mm tt") + "' and Price_code_shift >= '" + objH.SHIFT + "'))  "
                    Dim objMRT As clsMilkRejectType = Nothing
                    If settAlwaysVSPDefaulter Then
                        objMRT = clsMilkRejectType.GetData(obj.Reject_Type, NavigatorType.Current, trans)
                        If objMRT Is Nothing Then
                            Throw New Exception("Invalid rejection type " + obj.Reject_Type)
                        End If
                        If objCommonVar.DisplayTypeInMilkReceipt Then
                            qry += " and Dock_Collection_Milk_Type='" + objMRT.Type + "'"
                        End If
                    End If
                    qry += "  order by Date desc,TSPL_FAT_SNF_UPLOADER_MASTER.code desc"

                    If objCommonVar.PricePlan = 6 OrElse objCommonVar.PricePlan = 7 Then
                        Dim strPlaningCode As String = Nothing
                        Dim dblRate As Decimal = clsEkoPro.getRateAndPriceCodeFromUploaderShiftWise(obj.MILK_WEIGHT, strPlaningCode, obj.FAT, obj.SNF, objH.MCC_CODE, "", objH.SHIFT, objH.DOC_DATE, trans, "M")
                        qry = "select TSPL_MILK_PRICE_MASTER.*," + clsCommon.myCstr(dblRate) + " as Rate,'" + strPlaningCode + "' as Code  from TSPL_MILK_PRICE_MASTER where Price_Code in (select Price_Chart_Code from TSPL_PRICE_CHART_PLANNING where Planning_Code='" & strPlaningCode & "')"
                    End If


                    Dim dtPrice As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                    If dtPrice Is Nothing OrElse dtPrice.Rows.Count <= 0 Then
                        Throw New Exception("Price not found")
                    End If

                    If objCommonVar.PricePlan = 5 Then
                        Dim appPer As Decimal = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Applicable_Per from TSPL_MILK_REJECT_TYPE where Code='" + obj.Reject_Type + "'", trans))

                        Dim dclFATRate As Decimal = clsCommon.myCDivide((clsCommon.myCdbl(dtPrice.Rows(0)("Milk_Rate")) * clsCommon.myCdbl(dtPrice.Rows(0)("Ratio"))), clsCommon.myCdbl(dtPrice.Rows(0)("FAT_Pers")))
                        Dim dclSNFRate As Decimal = clsCommon.myCDivide((clsCommon.myCdbl(dtPrice.Rows(0)("Milk_Rate")) * clsCommon.myCdbl(dtPrice.Rows(0)("SNF_Ratio"))), clsCommon.myCdbl(dtPrice.Rows(0)("SNF_Pers")))

                        If clsCommon.CompairString(obj.Defaulter, "VSP") = CompairStringResult.Equal Then
                            dclFATRate = Math.Round(dclFATRate * appPer / 100, 2, MidpointRounding.ToEven)
                        End If

                        If settAlwaysVSPDefaulter Then
                            coll.Remove("Item_CODE")
                            clsCommon.AddColumnsForChange(coll, "Item_CODE", objMRT.Item_Code)
                            clsCommon.AddColumnsForChange(coll, "Org_Rate", clsCommon.myCdbl(dtPrice.Rows(0)("Rate")))
                            clsCommon.AddColumnsForChange(coll, "Applicable_Per", appPer)
                        End If

                        Dim FATKG As Decimal = obj.MILK_WEIGHT * obj.FAT / 100
                        obj.FAT_Amount = FATKG * dclFATRate
                        obj.FAT_RATE = Math.Round(clsCommon.myCDivide(obj.FAT_Amount, FATKG), 2, MidpointRounding.AwayFromZero)
                        If clsCommon.CompairString(obj.Defaulter, "Transporter") = CompairStringResult.Equal Then
                            Dim SNFKG As Decimal = obj.MILK_WEIGHT * obj.SNF / 100
                            obj.SNF_Amount = SNFKG * dclSNFRate
                            obj.SNF_RATE = Math.Round(clsCommon.myCDivide(obj.SNF_Amount, SNFKG), 2, MidpointRounding.AwayFromZero)

                            obj.SNF_Deduction_Amount = obj.SNF_Amount
                            obj.SNF_Deduction_Amount += ((obj.FAT_Amount * (100 - appPer)) / 100)
                            obj.SNF_Deduction_Amount = Math.Round(obj.SNF_Deduction_Amount, 2, MidpointRounding.AwayFromZero)
                        End If
                        obj.Amount = obj.FAT_Amount + obj.SNF_Amount
                        obj.RATE = Math.Round(clsCommon.myCDivide(obj.Amount, obj.MILK_WEIGHT), 2, MidpointRounding.AwayFromZero)



                        clsCommon.AddColumnsForChange(coll, "SNF_RATE", obj.SNF_RATE)
                        clsCommon.AddColumnsForChange(coll, "SNF_Amount", obj.SNF_Amount)
                        clsCommon.AddColumnsForChange(coll, "Price_Code", clsCommon.myCstr(dtPrice.Rows(0)("code")))
                        clsCommon.AddColumnsForChange(coll, "FAT_RATE", obj.FAT_RATE)
                        clsCommon.AddColumnsForChange(coll, "FAT_Amount", obj.FAT_Amount)

                        clsCommon.AddColumnsForChange(coll, "RATE", obj.RATE)
                        clsCommon.AddColumnsForChange(coll, "Amount", obj.Amount, 2, MidpointRounding.ToEven)
                    Else
                        If True Then
                            Dim dclRate As Decimal = clsCommon.myCdbl(dtPrice.Rows(0)("Rate"))
                            Dim dclAmt As Decimal = 0
                            Dim CalKG As Decimal = 0
                            If settAlwaysVSPDefaulter Then
                                coll.Remove("Item_CODE")
                                clsCommon.AddColumnsForChange(coll, "Item_CODE", objMRT.Item_Code)
                                clsCommon.AddColumnsForChange(coll, "Org_Rate", dclRate)
                                clsCommon.AddColumnsForChange(coll, "Applicable_Per", objMRT.Applicable_Per)
                                If objMRT.Applicable_On = 1 Then  ''RAte
                                    dclRate = objMRT.Applicable_Per
                                    dclAmt = Math.Round((dclRate * obj.MILK_WEIGHT), 2, MidpointRounding.ToEven)
                                ElseIf objMRT.Applicable_On = 2 Then  ''FAT KG RAte
                                    CalKG = (obj.MILK_WEIGHT * obj.FAT) / 100
                                    dclAmt = Math.Round((objMRT.Applicable_Per * CalKG), 2, MidpointRounding.ToEven)
                                    dclRate = clsCommon.myCDivide(dclAmt, obj.MILK_WEIGHT)
                                ElseIf objMRT.Applicable_On = 3 Then  ''SNF KG RAte
                                    CalKG = (obj.MILK_WEIGHT * obj.SNF) / 100
                                    dclAmt = Math.Round((objMRT.Applicable_Per * CalKG), 2, MidpointRounding.ToEven)
                                    dclRate = clsCommon.myCDivide(dclAmt, obj.MILK_WEIGHT)
                                Else ''%Age
                                    dclRate = Math.Round(dclRate * objMRT.Applicable_Per / 100, 2, MidpointRounding.ToEven)
                                    dclAmt = Math.Round((dclRate * obj.MILK_WEIGHT), 2, MidpointRounding.ToEven)
                                End If
                            End If

                            CalKG = (obj.MILK_WEIGHT * obj.SNF) / 100
                            obj.SNF_RATE = Math.Round((clsCommon.myCdbl(dtPrice.Rows(0)("SNF_Ratio")) / clsCommon.myCdbl(dtPrice.Rows(0)("SNF_Pers"))) * clsCommon.myCdbl(dtPrice.Rows(0)("Milk_Rate")), 2, MidpointRounding.ToEven)
                            If obj.SNF < obj.dclFATSNFDeductionMixMilkSNFMinValue Then
                                obj.SNF_RATE = 0
                            ElseIf obj.SNF >= obj.dclFATSNFDeductionMixMilkSNFMinValue AndAlso obj.SNF <= obj.dclFATSNFDeductionMixMilkSNFMaxValue Then
                                obj.SNF_RATE = obj.SNF_RATE * (100 - obj.dclFATSNFDeductionMixMilkDeductionPer) / 100
                            End If
                            obj.SNF_Amount = Math.Round(CalKG * obj.SNF_RATE, 2, MidpointRounding.ToEven)
                            clsCommon.AddColumnsForChange(coll, "SNF_RATE", obj.SNF_RATE)
                            clsCommon.AddColumnsForChange(coll, "SNF_Amount", obj.SNF_Amount)
                            clsCommon.AddColumnsForChange(coll, "Price_Code", clsCommon.myCstr(dtPrice.Rows(0)("code")))

                            If clsCommon.CompairString(obj.Reject_Type, "Curd") = CompairStringResult.Equal Then
                                CalKG = (obj.MILK_WEIGHT * obj.FAT) / 100
                                obj.FAT_RATE = Math.Round((clsCommon.myCdbl(dtPrice.Rows(0)("Ratio")) / clsCommon.myCdbl(dtPrice.Rows(0)("FAT_Pers"))) * clsCommon.myCdbl(dtPrice.Rows(0)("Milk_Rate")), 3, MidpointRounding.ToEven)
                                If obj.FAT < obj.dclFATSNFDeductionMixMilkFATMinValue Then
                                    obj.FAT_RATE = 0
                                ElseIf obj.FAT >= obj.dclFATSNFDeductionMixMilkFATMinValue AndAlso obj.FAT <= obj.dclFATSNFDeductionMixMilkFATMaxValue Then
                                    obj.FAT_RATE = obj.FAT_RATE * (100 - obj.dclFATSNFDeductionMixMilkDeductionPer) / 100
                                End If
                                obj.FAT_Amount = Math.Round(CalKG * obj.FAT_RATE, 2, MidpointRounding.ToEven)
                            End If
                            clsCommon.AddColumnsForChange(coll, "FAT_RATE", obj.FAT_RATE)
                            clsCommon.AddColumnsForChange(coll, "FAT_Amount", obj.FAT_Amount)

                            clsCommon.AddColumnsForChange(coll, "RATE", dclRate)
                            clsCommon.AddColumnsForChange(coll, "Amount", dclAmt)
                            If Math.Round((dclRate * obj.MILK_WEIGHT), 2, MidpointRounding.ToEven) <= 0 Then
                                If Not clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "UDP") = CompairStringResult.Equal Then
                                    Throw New Exception("Amount cannot be zero at sample no" + clsCommon.myCstr(obj.SAMPLE_NO))
                                End If
                            End If
                            obj.SNF_Deduction_Amount = Math.Round((obj.SNF_Amount * obj.SNF_Deduction_Per / 100), 2, MidpointRounding.ToEven)
                            obj.SNF_Amount_After_Deduction = Math.Round(obj.SNF_Amount - obj.SNF_Deduction_Amount, 2, MidpointRounding.ToEven)
                            obj.FAT_Deduction_Amount = Math.Round((obj.FAT_Amount * obj.FAT_Deduction_Per / 100), 2, MidpointRounding.ToEven)
                            obj.FAT_Amount_After_Deduction = Math.Round(obj.FAT_Amount - obj.FAT_Deduction_Amount, 2, MidpointRounding.ToEven)
                        End If
                    End If
                End If


                clsCommon.AddColumnsForChange(coll, "SNF_Deduction_Amount", obj.SNF_Deduction_Amount)
                clsCommon.AddColumnsForChange(coll, "SNF_Amount_After_Deduction", obj.SNF_Amount_After_Deduction)

                clsCommon.AddColumnsForChange(coll, "FAT_Deduction_Amount", obj.FAT_Deduction_Amount)
                clsCommon.AddColumnsForChange(coll, "FAT_Amount_After_Deduction", obj.FAT_Amount_After_Deduction)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MILK_REJECT_DETAIL", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function

    Public Shared Function GetAvgFATSNF(ByVal strVLCCode As String, ByVal transDate As DateTime, ByVal settNoOfPreNxtDayToPickAvgFATSNF As Integer, ByVal tran As SqlTransaction) As clsFatSnfRateCalculator
        Dim obj As New clsFatSnfRateCalculator()
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(GetAvgFATSNFQry(strVLCCode, transDate.AddDays(-1 * (settNoOfPreNxtDayToPickAvgFATSNF)), transDate.AddDays(-1)), tran)
        If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
            dt = clsDBFuncationality.GetDataTable(GetAvgFATSNFQry(strVLCCode, transDate.AddDays(1), transDate.AddDays(settNoOfPreNxtDayToPickAvgFATSNF)), tran)
        End If
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            obj.fatR = clsCommon.myCstr(dt.Rows(0)("FAT_PER"))
            obj.snfR = clsCommon.myCstr(dt.Rows(0)("SNF_PER"))
        End If
        Return obj
    End Function

    Private Shared Function GetAvgFATSNFQry(ByVal strVLCCode As String, ByVal FromDate As DateTime, ByVal ToDate As DateTime) As String
        Dim qry As String = "select * from (select cast( AVG(FAT_PER) as decimal(18,1)) as FAT_PER,cast(AVG(SNF_PER)as decimal(18,1)) as SNF_PER " + Environment.NewLine +
"from (" + Environment.NewLine +
"select FAT_PER,SNF_PER from TSPL_MILK_SRN_DETAIL" + Environment.NewLine +
"left outer join TSPL_MILK_SRN_HEAD on TSPL_MILK_SRN_HEAD.DOC_CODE=TSPL_MILK_SRN_DETAIL.DOC_CODE" + Environment.NewLine +
"where TSPL_MILK_SRN_HEAD.VLC_CODE='" + strVLCCode + "' and TSPL_MILK_SRN_HEAD.DOC_DATE>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(FromDate), "dd/MMM/yyyy hh:mm:ss tt") + "' and TSPL_MILK_SRN_HEAD.DOC_DATE<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(ToDate), "dd/MMM/yyyy hh:mm:ss tt") + "'" + Environment.NewLine +
")xx)xxx where FAT_PER is not null and SNF_PER is not null"

        Return qry
    End Function
End Class