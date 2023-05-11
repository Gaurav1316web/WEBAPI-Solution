Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO

Public Class clsMilkShiftEndMCC

#Region "Variables"

    Public DOC_CODE As String
    Public MCC_CODE As String
    Public DOC_DATE As DateTime
    Public MCC_DATE As DateTime
    Public SHIFT As String
    Public COMM_PORT As String
    Public MACHINE_NO As String
    Public Opening_KM As Decimal = 0
    Public Closing_KM As Decimal = 0
    Public TOTAL_KM As Decimal = 0
    Public ACTUAL_KM As Decimal = 0
    Public ACTUAL_Payable_KM As Decimal = 0
    Public Difference As Decimal = 0
    Public Reason As String
    Public ROUTE_CODE As String
    Public Truck_Sheet As String
    Public Deduction_of_Transporter As Double
    Public Attachment_Id As String
    '===============Added by Rohit===========
    Public Actual_Stock As Double = 0

    Public Manual_Stock As Double = 0
    Public Manual_FAT As Double = 0
    Public Manual_SNF As Double = 0
    Public Actual_FAT As Double = 0
    Public Actual_SNF As Double = 0
    Public Manual_FAT_Per As Double = 0
    Public Manual_SNF_Per As Double = 0
    Public Actual_FAT_Per As Double = 0
    Public Actual_SNF_Per As Double = 0
    Public Provision_Amt As Double = 0
    Public Irregular_Mcc_Code As String = String.Empty
    Public AskSiloatShiftEnd As Boolean = False
    Public SILOIn_Location As String = Nothing
    Public AutoIn_Location As String = Nothing
    Public CLR As Double = 0
    Public Mix_Milk As Boolean
    '==========================================
    Public MCC_NAME As String
    Public CREATED_BY As String
    Public Posting_Date As Date
    Public POSTED As ERPTransactionStatus = ERPTransactionStatus.Pending
    '' grid columns

    Public Shared ObjList As List(Of clsMilkShiftEndMCCDetail)
    Public Shared ObjList_Route As List(Of clsMilkShiftEndMCC_Route_Detail)
    Public Shared ObjListAttachment As List(Of clsMilkShiftEndAttachment)

    Public Form_ID As String = ""
    Public arrCustomFields As List(Of clsCustomFieldValues) = Nothing
    Public ArrGateEntryWithPenalty As ArrayList
#End Region

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsMilkShiftEndMCC
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

            Dim dt As DataTable = clsDBFuncationality.GetDataTable("select top 1 MCC_CODE,MCC_SHIFT_DATE from TSPL_OPEN_MCC_SHIFT where MCC_SHIFT_CODE ='" + strCode + "'", trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleMCCMilkProcurement, clsUserMgtCode.frmMilkShiftEndMCC, clsCommon.myCstr(dt.Rows(0)("MCC_CODE")), clsCommon.myCDate(dt.Rows(0)("MCC_SHIFT_DATE")), trans)

            End If

            Dim qry As String

            qry = "update TSPL_OPEN_MCC_SHIFT set status='O' where mcc_Code=(SELECT MCC_CODE FROM TSPL_MILK_Shift_End_head WHERE DOC_CODE='" & strCode & "')" _
            & " and shift=(SELECT SHIFT FROM TSPL_MILK_Shift_End_head WHERE DOC_CODE='" & strCode & "') and convert(date,Mcc_Shift_date,103)=" _
            & " (SELECT convert(date,DOC_date,103) FROM TSPL_MILK_Shift_End_head WHERE DOC_CODE='" & strCode & "')"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "Delete From TSPL_ATTACHMENTS where Transactionid in (SELECT vlc_doc_code from TSPL_MILK_Shift_End_DETAIL where DOC_CODE ='" + strCode + "')"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "Delete From TSPL_ATTACHMENTS where Transactionid  ='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)



            qry = "delete from TSPL_MILK_Shift_End_DETAIL where DOC_CODE ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)


            qry = "delete from TSPL_MILK_Shift_End_Head where DOC_CODE ='" + strCode + "'"
            isSaved = isSaved And clsDBFuncationality.ExecuteNonQuery(qry, trans)

            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message.ToString())
        End Try
        Return isSaved
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsMilkShiftEndMCC

        Dim obj As New clsMilkShiftEndMCC()
        Dim objtr As New clsMilkShiftEndMCCDetail

        ObjList = New List(Of clsMilkShiftEndMCCDetail)

        Dim objtr_Route As New clsMilkShiftEndMCC_Route_Detail

        ObjList_Route = New List(Of clsMilkShiftEndMCC_Route_Detail)

        Dim qry As String = "SELECT TSPL_MILK_Shift_End_Head.*,TSPL_MCC_MASTER.MCC_NAME,Filename,TSPL_ATTACHMENTS.code as Attachment_Id FROM TSPL_MILK_Shift_End_Head " _
        & " LEFT JOIN TSPL_MCC_MASTER   ON TSPL_MILK_Shift_End_Head.MCC_CODE=TSPL_MCC_MASTER.MCC_CODE " _
        & " LEFT join TSPL_ATTACHMENTS on TransactionId=DOC_CODE and FormId='M-RECEIPT' where 2=2 "

        Select Case NavType
            Case NavigatorType.First
                qry += " AND DOC_CODE = (select MIN(DOC_CODE) from TSPL_MILK_Shift_End_Head)"
            Case NavigatorType.Last
                qry += " AND DOC_CODE = (select Max(DOC_CODE) from TSPL_MILK_Shift_End_Head)"
            Case NavigatorType.Next
                qry += " AND DOC_CODE = (select Min(DOC_CODE) from TSPL_MILK_Shift_End_Head where  DOC_CODE>'" + strCode + "')"
            Case NavigatorType.Previous
                qry += " AND DOC_CODE = (select Max(DOC_CODE) from TSPL_MILK_Shift_End_Head where DOC_CODE<'" + strCode + "')"
            Case NavigatorType.Current
                qry += " AND DOC_CODE = '" + strCode + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then

            obj.DOC_CODE = dt.Rows(0)("DOC_CODE")
            obj.MCC_CODE = clsCommon.myCstr(dt.Rows(0)("MCC_CODE"))
            obj.DOC_DATE = clsCommon.GetPrintDate(dt.Rows(0)("DOC_DATE"), "dd/MMM/yyyy")
            obj.MCC_DATE = clsCommon.GetPrintDate(dt.Rows(0)("MCC_DATE"), "dd/MMM/yyyy")
            obj.SHIFT = clsCommon.myCstr(dt.Rows(0)("SHIFT"))
            'obj.TOTAL_KM = clsCommon.myCstr(dt.Rows(0)("Total_KM"))
            'obj.ACTUAL_KM = clsCommon.myCstr(dt.Rows(0)("Actual_KM"))
            'obj.Difference = clsCommon.myCstr(dt.Rows(0)("Difference"))
            'obj.Reason = clsCommon.myCstr(dt.Rows(0)("Reason"))
            'obj.ROUTE_CODE = clsCommon.myCstr(dt.Rows(0)("Route_CODE"))
            'obj.Truck_Sheet = clsCommon.myCstr(dt.Rows(0)("Filename"))
            'obj.Deduction_of_Transporter = clsCommon.myCstr(dt.Rows(0)("Deduction_of_transporter"))
            obj.MCC_NAME = clsCommon.myCstr(dt.Rows(0)("MCC_Name"))
            '  obj.Opening_KM = clsCommon.myCdbl(dt.Rows(0)("Opening_KM"))
            ' obj.Closing_KM = clsCommon.myCdbl(dt.Rows(0)("Closing_KM"))
            obj.Attachment_Id = clsCommon.myCstr(dt.Rows(0)("Attachment_Id"))
            ' obj.Reason = clsCommon.myCstr(dt.Rows(0)("Reason"))
            '=============Added by Rohit for Stock,FAt and SNF==================
            obj.Actual_Stock = clsCommon.myCdbl(dt.Rows(0)("Actual_Stock"))

            obj.Manual_Stock = clsCommon.myCdbl(dt.Rows(0)("Manual_Stock"))
            obj.Manual_FAT = clsCommon.myCdbl(dt.Rows(0)("Manual_FAT"))
            obj.Manual_SNF = clsCommon.myCdbl(dt.Rows(0)("Manual_SNF"))
            obj.Actual_FAT = clsCommon.myCdbl(dt.Rows(0)("Actual_FAT"))
            obj.Actual_SNF = clsCommon.myCdbl(dt.Rows(0)("Actual_SNF"))
            obj.Manual_FAT_Per = clsCommon.myCdbl(dt.Rows(0)("Manual_FAT_Per"))
            obj.Manual_SNF_Per = clsCommon.myCdbl(dt.Rows(0)("Manual_SNF_Per"))
            obj.Actual_FAT_Per = clsCommon.myCdbl(dt.Rows(0)("Actual_FAT_Per"))
            obj.Actual_SNF_Per = clsCommon.myCdbl(dt.Rows(0)("Actual_SNF_Per"))
            '===================================================
            obj.AutoIn_Location = clsCommon.myCstr(dt.Rows(0)("AutoIn_Location"))
            obj.SILOIn_Location = clsCommon.myCstr(dt.Rows(0)("SILOIn_Location"))
            obj.AskSiloatShiftEnd = IIf(clsCommon.myCdbl(dt.Rows(0)("AskSiloatShiftEnd")) = 1, True, False)
            obj.CREATED_BY = clsCommon.myCstr(dt.Rows(0)("CREATED_BY"))
            obj.POSTED = IIf(clsCommon.myCdbl(dt.Rows(0)("Posted")) = 1, ERPTransactionStatus.Approved, ERPTransactionStatus.Pending)
            '        strCode = dt.Rows(0)("DOC_CODE")
            obj.CLR = clsCommon.myCdbl(dt.Rows(0)("CLR"))
            obj.Mix_Milk = (clsCommon.myCdbl(dt.Rows(0)("Mix_Milk")) = 1)
            If clsCommon.myLen(dt.Rows(0)("Posting_Date")) > 0 Then
                obj.Posting_Date = clsCommon.myCDate(dt.Rows(0)("Posting_Date"))
            Else
                obj.Posting_Date = Nothing
            End If
        End If
        qry = " SELECT Distinct TSPL_MILK_Shift_End_DETAIL.DOC_CODE,TSPL_MILK_Shift_End_DETAIL.VLC_DOC_CODE,TSPL_MILK_Shift_End_DETAIL.VLC_CODE,TSPL_MILK_Shift_End_DETAIL.VSP_CODE,TSPL_MILK_Shift_End_DETAIL.VEHICLE_CODE,  TSPL_MILK_Shift_End_DETAIL.MCC_CODE,TSPL_MILK_Shift_End_DETAIL.DOC_DATE,TSPL_MILK_Shift_End_DETAIL.SHIFT,Deduction_of_VSP,A_Or_R,Remarks,FileName,TSPL_ATTACHMENTS.code as Attachment_Id,route_code,Deduction_Reason,Rejected_or_Acccepted FROM TSPL_MILK_Shift_End_DETAIL " _
        & " LEFT join TSPL_ATTACHMENTS on TransactionId=VLC_DOC_CODE and FormId='M-Shift-End' left join TSPL_MILK_RECEIPT_DETAIL on TSPL_MILK_RECEIPT_DETAIL.vlc_Doc_code=TSPL_MILK_Shift_End_DETAIL.vlc_Doc_code WHERE 2=2 AND TSPL_MILK_Shift_End_DETAIL.DOC_CODE = '" & strCode & "'  " _
        & " ORDER BY TSPL_MILK_Shift_End_DETAIL.VLC_DOC_CODE"

        dt = New DataTable()
        dt = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            For Each dr As DataRow In dt.Rows
                objtr = New clsMilkShiftEndMCCDetail

                objtr.DOC_CODE = strCode
                objtr.VLC_DOC_CODE = clsCommon.myCstr(dr("VLC_DOC_CODE"))
                objtr.VLC_CODE = clsCommon.myCstr(dr("VLC_CODE"))
                objtr.VSP_CODE = clsCommon.myCstr(dr("VSP_CODE"))
                objtr.VEHICLE_CODE = clsCommon.myCstr(dr("VEHICLE_CODE"))
                objtr.MCC_CODE = clsCommon.myCstr(dr("MCC_CODE"))
                objtr.Remarks = clsCommon.myCstr(dr("Remarks"))
                objtr.deduction_of_VSP = clsCommon.myCdbl(dr("Deduction_of_VSP"))
                objtr.Deduction_Reason = clsCommon.myCstr(dr("Deduction_Reason"))
                objtr.Amount_or_Rate = clsCommon.myCstr(dr("A_Or_R"))
                objtr.Rejected_or_Acccepted = clsCommon.myCstr(dr("Rejected_or_Acccepted"))
                objtr.DOC_DATE = clsCommon.myCDate(dr("DOC_DATE"))
                objtr.SHIFT = clsCommon.myCstr(dr("SHIFT"))
                objtr.vlc_procurement_mp_data = clsCommon.myCstr(dr("FileName"))
                objtr.Attachment_Id = clsCommon.myCstr(dr("Attachment_Id"))
                objtr.Route_Code = clsCommon.myCstr(dr("Route_Code"))
                ObjList.Add(objtr)
            Next
        End If

        qry = " SELECT DOC_CODE,erd.Route_CODE,Route_name,Opening_KM,Closing_KM,Total_KM,Actual_KM,Actual_Payable_KM,Diff_KM,deduction_of_Transporter,Milk_Weight,Actual_Weight,erd.vehicle_code,Shift_Charge,Rate,Amount,DOC_DATE,Reason,FileName,TSPL_ATTACHMENTS.code as Attachment_Id,TSPL_Primary_Vehicle_Master.Description as  vehicle_name,Deduction_Reason,coalesce(is_Head_load,0) as is_Head_load,charge_Amount,Head_Load_Amount,Own_Asset_Amount,erd.Std_Qty FROM TSPL_MILK_Shift_End_Route_DETAIL erd " _
        & " LEFT join tspl_mcc_route_master rm on rm.Route_Code=erd.Route_Code LEFT join TSPL_ATTACHMENTS on TransactionId=erd.Route_CODE and TSPL_ATTACHMENTS.code=Attachment_id and FormId='M-RECEIPT' left join TSPL_Primary_Vehicle_Master on TSPL_Primary_Vehicle_Master.Vehicle_Code=rm.Vehicle_Code  WHERE 2=2 AND erd.DOC_CODE = '" & strCode & "'  " _
        & " ORDER BY erd.Route_CODE"

        dt = New DataTable()
        dt = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            For Each dr As DataRow In dt.Rows
                objtr_Route = New clsMilkShiftEndMCC_Route_Detail

                objtr_Route.DOC_CODE = strCode
                objtr_Route.Route_CODE = clsCommon.myCstr(dr("Route_CODE"))
                objtr_Route.Route_name = clsCommon.myCstr(dr("Route_Name"))
                objtr_Route.Opening_KM = clsCommon.myCdbl(dr("Opening_KM"))
                objtr_Route.Closing_KM = clsCommon.myCdbl(dr("Closing_KM"))
                objtr_Route.Total_KM = clsCommon.myCdbl(dr("Total_KM"))
                objtr_Route.Actual_KM = clsCommon.myCdbl(dr("Actual_KM"))
                objtr_Route.Actual_Payable_KM = clsCommon.myCdbl(dr("Actual_Payable_KM"))
                objtr_Route.Diff_Km = clsCommon.myCdbl(dr("Diff_Km"))
                objtr_Route.deduction_of_Transporter = clsCommon.myCdbl(dr("deduction_of_Transporter"))
                objtr_Route.Milk_Weight = clsCommon.myCdbl(dr("Milk_weight"))
                objtr_Route.Actual_Weight = clsCommon.myCdbl(dr("Actual_weight"))

                objtr_Route.Vehicle_Code = clsCommon.myCstr(dr("Vehicle_code"))
                objtr_Route.Vehicle_Name = clsCommon.myCstr(dr("Vehicle_name"))
                objtr_Route.Rate_KM = clsCommon.myCdbl(dr("Rate"))
                objtr_Route.Shift_Charge = clsCommon.myCdbl(dr("Shift_Charge"))
                objtr_Route.Amount = clsCommon.myCdbl(dr("Amount"))
                objtr_Route.Std_Qty = clsCommon.myCdbl(dr("Std_Qty"))
                objtr_Route.Attachment_Id = clsCommon.myCdbl(dr("Attachment_id"))
                objtr_Route.Truck_Sheet_of_TransPorter = clsCommon.myCstr(dr("FileName"))
                objtr_Route.DOC_DATE = clsCommon.myCDate(dr("DOC_DATE"))
                objtr_Route.Reason = clsCommon.myCstr(dr("Reason"))
                objtr_Route.Deduction_Reason = clsCommon.myCstr(dr("Deduction_Reason"))
                objtr_Route.Charge_Amt = clsCommon.myCstr(dr("Charge_Amount"))
                objtr_Route.Head_Load_Amt = clsCommon.myCstr(dr("Head_Load_Amount"))
                objtr_Route.Own_Asset_Amount = clsCommon.myCstr(dr("Own_Asset_Amount"))
                objtr_Route.Is_Head_Load = clsCommon.myCstr(dr("Is_Head_Load"))
                ObjList_Route.Add(objtr_Route)
            Next
        End If

        clsMilkShiftEndMCC.ObjList = ObjList
        clsMilkShiftEndMCC.ObjList_Route = ObjList_Route
        Return obj
    End Function

    Public Shared Function GetHeadLoadData(ByVal Route_code As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim sQuery As String = "select TSPL_MCC_ROUTE_MASTER.Route_Code,TSPL_MCC_ROUTE_MASTER.Route_Name,TSPL_MCC_ROUTE_MASTER.Vehicle_Code,VLC_Code," _
            & " Is_Head_Load from TSPL_VLC_MASTER_HEAD inner join TSPL_MCC_ROUTE_MASTER on TSPL_MCC_ROUTE_MASTER.Route_Code=TSPL_VLC_MASTER_HEAD.Route_Code " _
            & " inner join TSPL_Primary_Vehicle_Master on TSPL_Primary_Vehicle_Master.Vehicle_Code=TSPL_MCC_ROUTE_MASTER.Vehicle_Code inner join " _
            & " TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_VLC_MASTER_HEAD.VSP_Code where coalesce(Is_Head_Load,'F')='T' and TSPL_MCC_ROUTE_MASTER.Route_Code" _
            & " ='" & Route_code & "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(sQuery, trans)
            If dt.Rows.Count > 0 Then
                Return True
            Else : Return False
            End If
        Catch ex As Exception
            Throw New Exception(ex.ToString)
        End Try
        Return True
    End Function

    Public Function Gettable(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As DataTable
        ',Case when IS_MANUAL='N' then 'Auto' else 'Manual' end as [Entry Type]//Commented by Rohit as Per Meeting with rakesh Sir it will not visible to Client.
        Dim qry As String = " SELECT DOC_CODE as [DOC CODE],VLC_DOC_CODE as [VLC DOC CODE],SAMPLE_NO as [SAMPLE NO],VLC_CODE as [VLC CODE],TSPL_ITEM_MASTER.Item_Code + '(' + Item_Desc + ')' as [ITEM],ROUTE_CODE as [ROUTE CODE],VSP_CODE as [VSP CODE],VEHICLE_CODE as [VEHICLE CODE], "
        qry += " NO_OF_CANS as [NO OF CANS],MILK_WEIGHT as [MILK WEIGHT],TYPE,MILK_TYPE as [MILK TYPE],SAMPLE_NO_VALUES as [SAMPLE NO VALUES],MCC_CODE as [MCC CODE],DOC_DATE as [DOC DATE],SHIFT,COMM_PORT as [COM PORT],MACHINE_NO as [MACHINE NO] FROM TSPL_MILK_Shift_End_DETAIL inner join tspl_item_master on tspl_item_master.item_Code=TSPL_MILK_Shift_End_DETAIL.item_Code  WHERE 2=2"
        qry += " AND TSPL_MILK_Shift_End_DETAIL.DOC_CODE = '" + strCode + "' ORDER BY TSPL_MILK_Shift_End_DETAIL.VLC_DOC_CODE"

        Dim dt As DataTable = New DataTable()
        dt = clsDBFuncationality.GetDataTable(qry, trans)
        Return dt
    End Function

    Public Shared Function LoadDataFromDetails(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsMilkShiftEndMCC
        Dim obj As New clsMilkShiftEndMCC()
        Dim objtr As New clsMilkShiftEndMCCDetail

        ObjList = New List(Of clsMilkShiftEndMCCDetail)
        Dim qry As String = " SELECT DOC_CODE,VLC_DOC_CODE,SAMPLE_NO,VLC_CODE,ROUTE_CODE,VSP_CODE,Item_Code,VEHICLE_CODE, "
        qry += " NO_OF_CANS,MILK_WEIGHT,TYPE,MILK_TYPE,FAT,SNF,SAMPLE_NO_VALUES,MCC_CODE,DOC_DATE,SHIFT,COMM_PORT,MACHINE_NO,is_Manual,case when is_sampleed='T' then 1 else 0 end is_sampleed FROM TSPL_MILK_Shift_End_DETAIL   WHERE 2=2"
        qry += " AND TSPL_MILK_Shift_End_DETAIL.VLC_DOC_CODE = '" + strCode + "' ORDER BY TSPL_MILK_Shift_End_DETAIL.VLC_DOC_CODE"

        Dim dt As DataTable = New DataTable()
        dt = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            For Each dr As DataRow In dt.Rows
                objtr = New clsMilkShiftEndMCCDetail

                objtr.DOC_CODE = strCode
                objtr.VLC_DOC_CODE = clsCommon.myCstr(dr("VLC_DOC_CODE"))
                objtr.VLC_CODE = clsCommon.myCstr(dr("VLC_CODE"))



                objtr.VSP_CODE = clsCommon.myCstr(dr("VSP_CODE"))
                objtr.VEHICLE_CODE = clsCommon.myCstr(dr("VEHICLE_CODE"))
                objtr.MCC_CODE = clsCommon.myCstr(dr("MCC_CODE"))

                objtr.DOC_DATE = clsCommon.myCDate(dr("DOC_DATE"))
                objtr.SHIFT = clsCommon.myCstr(dr("SHIFT"))
                ObjList.Add(objtr)
            Next
        End If
        Return obj
    End Function

    Public Shared Function GetShift(ByVal Mcc_Code As String) As DataTable
        Dim sQuery As String = "select * from TSPL_OPEN_MCC_SHIFT where lower(status)='open' and mcc_code='" & Mcc_Code & "'"
        Dim Dt As DataTable = clsDBFuncationality.GetDataTable(sQuery)
        Try
            If Dt.Rows.Count <= 0 Or Dt.Rows.Count > 1 Then
                clsCommon.MyMessageBoxShow("There are more then one shifts are opened.Only one Shift can be Opened..")
                Return Nothing
            Else
                Return Dt
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.ToString)
        End Try
        Return Dt
    End Function

    Public Shared Function SaveData(ByVal obj As clsMilkShiftEndMCC, ByVal objList As List(Of clsMilkShiftEndMCCDetail), ByVal objList_Route As List(Of clsMilkShiftEndMCC_Route_Detail), ByVal trans As SqlTransaction) As Boolean
        Try
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleMCCMilkProcurement, clsUserMgtCode.frmMilkShiftEndMCC, obj.MCC_CODE, obj.DOC_DATE, trans)

            Dim isNewEntry As Boolean
            obj.DOC_CODE = GetDocCode(obj.DOC_DATE, obj.MCC_CODE, obj.SHIFT, trans)
            If GetPost(obj.DOC_DATE, obj.MCC_CODE, obj.SHIFT, trans) Then
                Throw New Exception("This Code:" + obj.DOC_CODE + " Is Posted.")
            End If
            Dim sQuery As String = "delete from TSPL_INVENTORY_MOVEMENT_NEW where Trans_Type ='MCC-MSRN' and Location_Code='" + obj.MCC_CODE + "' and convert(date,punching_Date,103)=convert(date, '" + clsCommon.GetPrintDate(obj.DOC_DATE, "dd/MMM/yyyy") + "',103)  and not exists(select 1 from TSPL_MILK_SRN_HEAD where DOC_CODE=TSPL_INVENTORY_MOVEMENT_NEW.source_doc_no)"
            clsDBFuncationality.ExecuteNonQuery(sQuery, trans)


            sQuery = "select DOC_CODE from TSPL_MILK_REJECT_HEAD where convert(date, DOC_DATE,103)='" + clsCommon.GetPrintDate(obj.DOC_DATE, "dd/MMM/yyyy") + "' and SHIFT='" + obj.SHIFT + "' and MCC_CODE='" + obj.MCC_CODE + "' and isnull(Posted,0)=0"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(sQuery, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                clsMilkRejectHead.PostData(clsCommon.myCstr(dt.Rows(0)("DOC_CODE")), trans)
            End If

            If clsCommon.myLen(obj.DOC_CODE) <= 0 Then
                isNewEntry = True
                obj.DOC_CODE = clsERPFuncationality.GetNextCode(trans, clsCommon.GetPrintDate(obj.DOC_DATE, "dd/MMM/yyyy hh:mm:ss tt"), clsDocType.MilkShift_End, "", obj.MCC_CODE, False, True, False, False, objCommonVar.ShowMCCFinderInPaymentProcess)
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
            clsCommon.AddColumnsForChange(coll, "MCC_DATE", clsCommon.GetPrintDate(obj.MCC_DATE, "dd/MMM/yyyy hh:mm:ss tt"))
            clsCommon.AddColumnsForChange(coll, "SHIFT", obj.SHIFT)
            clsCommon.AddColumnsForChange(coll, "Actual_Stock", obj.Actual_Stock)
            clsCommon.AddColumnsForChange(coll, "Manual_Stock", obj.Manual_Stock)
            clsCommon.AddColumnsForChange(coll, "Manual_FAT", obj.Manual_FAT)
            clsCommon.AddColumnsForChange(coll, "Manual_SNF", obj.Manual_SNF)
            clsCommon.AddColumnsForChange(coll, "Actual_FAT", obj.Actual_FAT)
            clsCommon.AddColumnsForChange(coll, "Actual_SNF", obj.Actual_SNF)
            clsCommon.AddColumnsForChange(coll, "Manual_FAT_Per", obj.Manual_FAT_Per)
            clsCommon.AddColumnsForChange(coll, "Manual_SNF_Per", obj.Manual_SNF_Per)
            clsCommon.AddColumnsForChange(coll, "Actual_FAT_Per", obj.Actual_FAT_Per)
            clsCommon.AddColumnsForChange(coll, "Actual_SNF_Per", obj.Actual_SNF_Per)
            clsCommon.AddColumnsForChange(coll, "CLR", obj.CLR)
            clsCommon.AddColumnsForChange(coll, "POSTED", "1")
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Mix_Milk", IIf(obj.Mix_Milk, 1, 0))
            '' update Sync Satatus
            clsCommon.AddColumnsForChange(coll, "SYNC_STATUS", 0)
            clsCommon.AddColumnsForChange(coll, "AutoIn_Location", obj.AutoIn_Location)
            clsCommon.AddColumnsForChange(coll, "SILOIn_Location", obj.SILOIn_Location)
            clsCommon.AddColumnsForChange(coll, "AskSiloatShiftEnd", IIf(obj.AskSiloatShiftEnd, 1, 0))
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                Dim Strqry As String = "SELECT Count(*) FROM TSPL_MILK_Shift_End_Head where DOC_CODE = '" & obj.DOC_CODE & "'"
                Dim check As Integer = clsDBFuncationality.getSingleValue(Strqry, trans)
                If check = 0 Then
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MILK_Shift_End_Head", OMInsertOrUpdate.Insert, "", trans)
                Else
                    Throw New Exception("This Code:" + obj.DOC_CODE + " Is Already Exist")
                End If
            Else
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MILK_Shift_End_Head", OMInsertOrUpdate.Update, "TSPL_MILK_Shift_End_Head.DOC_CODE='" + obj.DOC_CODE + "'", trans)
            End If
            clsMilkShiftEndMCCDetail.SaveData(obj.DOC_CODE, objList, trans, isNewEntry)
            clsCustomFieldValues.SaveData(obj.Form_ID, obj.DOC_CODE, obj.arrCustomFields, trans)

            clsMilkGateEntryIn.ApprovePenaltyAmount(obj.ArrGateEntryWithPenalty, trans)

            If clsCommon.myLen(obj.Irregular_Mcc_Code) > 0 Then
                clsMilkShiftEndMCC_Route_Detail.SaveData(obj.DOC_CODE, objList_Route, obj.Irregular_Mcc_Code, trans, isNewEntry, obj)
            Else
                clsMilkShiftEndMCC_Route_Detail.SaveData(obj.DOC_CODE, objList_Route, obj.MCC_CODE, trans, isNewEntry, obj)
            End If
            sQuery = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select MCC_SHIFT_CODE from TSPL_OPEN_MCC_SHIFT where mcc_Code='" & obj.MCC_CODE & "' and shift='" & obj.SHIFT & "' and convert(date,Mcc_Shift_date,103)='" & clsCommon.GetPrintDate(obj.MCC_DATE, "dd-MMM-yyyy") & "'", trans))
            sQuery = "update TSPL_OPEN_MCC_SHIFT set status='C' where MCC_SHIFT_CODE='" + sQuery + "' "
            clsDBFuncationality.ExecuteNonQuery(sQuery, trans)

            sQuery = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select DOC_CODE from TSPL_MILK_RECEIPT_HEAD where mcc_Code='" & obj.MCC_CODE & "' and shift='" & obj.SHIFT & "' and convert(date,Doc_date,103)='" & clsCommon.GetPrintDate(obj.MCC_DATE, "dd-MMM-yyyy") & "'", trans))
            sQuery = "update TSPL_MILK_RECEIPT_HEAD set POSTED='1' where DOC_CODE='" + sQuery + "' "
            clsDBFuncationality.ExecuteNonQuery(sQuery, trans)


            CreateAdjustmentToStockTransfer(obj, True, trans, "", "", "", "", "", "")
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function



    Public Shared Sub SendSMSDataNew(ByVal obj As clsMilkShiftEndMCC, ByVal trans As SqlTransaction)
        CreateSMSContentEMP(obj, trans)
        CreateSMSContentVSP(obj, trans)
        CreateMailContent(obj, trans)
    End Sub

    Shared Sub CreateSMSContentEMP(ByVal obj As clsMilkShiftEndMCC, ByVal trans As SqlTransaction)
        Dim strSMSContent As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT SMS_Text from TSPL_ES_Content where Form_ID='" + clsUserMgtCode.frmMilkShiftEndMCC + "'", trans))
        If clsCommon.myLen(strSMSContent) > 0 Then
            Dim qry As String = "select max(State_Code) as State_Code,max(TSPL_MCC_MASTER.Mcc_Code_VLC_Uploader) as Mcc_Code_VLC_Uploader,TSPL_MILK_SRN_HEAD.MCC_CODE,max(MCC_NAME) as MCC_NAME,convert(date,DOC_DATE,103) as DOC_DATE,cast(round(sum(Qty),2,2) as float) as Qty,max(TSPL_Mcc_UOM_DETAIL.UOM_Code) as UOM_Code,count(distinct(TSPL_MILK_SRN_HEAD.ROUTE_CODE)) as Route_Count,count(distinct(TSPL_MILK_SRN_HEAD.VLC_CODE)) as VLC_Count,convert(decimal(18,2), case when sum(Qty)>0 then sum (FAT_KG)*100/sum(Qty) else 0 end) as FATPER,convert(decimal(18,2), case when sum(Qty)>0 then sum(SNF_KG)*100/sum(Qty) else 0 end) as SNFPER,convert(decimal(18,2), case when sum(Qty)>0 then sum(Amount)/sum(Qty) else 0 end) as Rate,convert(decimal(18,2), sum(Amount) ) as Amount " + Environment.NewLine +
              " from TSPL_MILK_SRN_DETAIL " + Environment.NewLine +
              " inner join TSPL_MILK_SRN_HEAD on TSPL_MILK_SRN_DETAIL.DOC_CODE=TSPL_MILK_SRN_HEAD.DOC_CODE " + Environment.NewLine +
              " left join TSPL_Mcc_UOM_DETAIL on TSPL_Mcc_UOM_DETAIL.MCC_CODE=TSPL_MILK_SRN_HEAD.MCC_CODE and Stocking_Unit='Y' " + Environment.NewLine +
              " left join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code=TSPL_MILK_SRN_HEAD.MCC_CODE " + Environment.NewLine +
              " where  convert(date,doc_date,103)=convert(date,'" + clsCommon.GetPrintDate(obj.DOC_DATE, "dd/MMM/yyyy") + "',103) and shift='" + obj.SHIFT + "' and TSPL_MILK_SRN_HEAD.MCC_CODE ='" + obj.MCC_CODE + "' " + Environment.NewLine +
              " group by tspl_milk_srn_Head.mcc_code,convert(date,doc_date,103)"
            Dim dtData As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dtData IsNot Nothing AndAlso dtData.Rows.Count > 0 Then
                Dim objSMSH As New clsSMSHead()
                objSMSH.Created_Date = obj.DOC_DATE
                objSMSH.SMS_Text = strSMSContent
                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.State, clsCommon.myCstr(dtData.Rows(0)("State_Code")))
                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.MCCCode, clsCommon.myCstr(dtData.Rows(0)("MCC_CODE")))
                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.MCCName, clsCommon.myCstr(dtData.Rows(0)("MCC_NAME")))
                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.MCCUploaderCode, clsCommon.myCstr(dtData.Rows(0)("Mcc_Code_VLC_Uploader")))
                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.VLCDataUploaderDate, clsCommon.GetPrintDate(clsCommon.myCDate(dtData.Rows(0)("DOC_DATE")), "dd/MMM/yyyy"))
                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.TotalQty, clsCommon.myCstr(dtData.Rows(0)("Qty")))
                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.UOM, clsCommon.myCstr(dtData.Rows(0)("UOM_Code")))
                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.NoOfVLC, clsCommon.myCstr(dtData.Rows(0)("VLC_Count")))
                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.NoOfRoute, clsCommon.myCstr(dtData.Rows(0)("Route_Count")))
                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.VLCDataUploaderFat, clsCommon.myFormat(clsCommon.myCdbl(dtData.Rows(0)("FATPER"))))
                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.VLCDataUploaderSNF, clsCommon.myFormat(clsCommon.myCdbl(dtData.Rows(0)("SNFPER"))))
                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.VLCDataUploaderRate, clsCommon.myFormat(clsCommon.myCdbl(dtData.Rows(0)("Rate"))))
                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.VLCDataUploaderAmt, clsCommon.myFormat(clsCommon.myCdbl(dtData.Rows(0)("Amount"))))
                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.VLCDataUploaderShift, obj.SHIFT)
                objSMSH.arrMobilNo = New List(Of String)()
                'objSMSH.arrMobilNo.Add(clsCommon.myCstr(dt.Rows(0)("MobileNo")))
                objSMSH.SaveData(clsUserMgtCode.frmMilkShiftEndMCC, objSMSH, trans)
                objSMSH = Nothing
            End If
        End If
    End Sub

    Shared Sub CreateSMSContentVSP(ByVal obj As clsMilkShiftEndMCC, ByVal trans As SqlTransaction)
        Dim strSMSContent As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT SMS_Text from TSPL_ES_Content where Form_ID='" + clsUserMgtCode.frmMilkSample + "'", trans))
        If clsCommon.myLen(strSMSContent) > 0 Then
            Dim qry As String = "select TSPL_MILK_SRN_HEAD.SAMPLE_NO,TSPL_MILK_SRN_HEAD.MCC_CODE,TSPL_MCC_MASTER.MCC_NAME,TSPL_MCC_MASTER.Mcc_Code_VLC_Uploader,convert(Date, TSPL_MILK_SRN_HEAD.DOC_DATE,103) as DOC_DATE,TSPL_MILK_SRN_HEAD.VSP_CODE,TSPL_VLC_MASTER_HEAD.VLC_Name,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader" + Environment.NewLine +
            " ,case when TSPL_MILK_SRN_DETAIL.FAT_PER<=5 then 'C' else 'B' end as MilkType,TSPL_MILK_SRN_DETAIL.Qty,TSPL_MILK_SRN_DETAIL.UOM_Code,TSPL_MILK_SRN_DETAIL.FAT_PER,TSPL_MILK_SRN_DETAIL.SNF_PER,TSPL_MILK_SRN_DETAIL.RATE,TSPL_MILK_SRN_DETAIL.AMOUNT" + Environment.NewLine +
            " ,substring (Phone1 ,6,10) as Phone1" + Environment.NewLine +
            " from TSPL_MILK_SRN_DETAIL" + Environment.NewLine +
            " left outer join TSPL_MILK_SRN_HEAD on TSPL_MILK_SRN_HEAD.DOC_CODE=TSPL_MILK_SRN_DETAIL.DOC_CODE" + Environment.NewLine +
            " Left Outer Join TSPL_VLC_MASTER_HEAD On TSPL_VLC_MASTER_HEAD.VLC_Code = TSPL_MILK_SRN_HEAD.VLC_CODE " + Environment.NewLine +
            " Left Outer Join TSPL_VENDOR_MASTER On TSPL_VENDOR_MASTER.Vendor_Code = TSPL_MILK_SRN_HEAD.VSP_CODE " + Environment.NewLine +
            " Left Outer Join TSPL_MCC_MASTER On TSPL_MCC_MASTER.MCC_Code = TSPL_MILK_SRN_HEAD.MCC_CODE  " + Environment.NewLine +
            " where TSPL_MILK_SRN_HEAD.MCC_CODE='" + obj.MCC_CODE + "' and convert(Date, TSPL_MILK_SRN_HEAD.DOC_DATE,103)=convert(date,'" + clsCommon.GetPrintDate(obj.DOC_DATE, "dd/MMM/yyyy") + "',103) and  TSPL_MILK_SRN_HEAD.SHIFT='" + obj.SHIFT + "' and len(replace( isnull(substring(TSPL_VENDOR_MASTER.Phone1,6,10),''),'_',''))>0 order by TSPL_MILK_SRN_HEAD.SAMPLE_NO  "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    Dim objSMSH As New clsSMSHead()
                    objSMSH.SMS_Text = strSMSContent
                    objSMSH.Created_Date = obj.DOC_DATE
                    objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.VLCDataUploaderDate, clsCommon.GetPrintDate(clsCommon.myCDate(dr("DOC_DATE")), "dd/MM/yyyy"))
                    objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.VLCDataUploaderShift, obj.SHIFT)
                    objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.MCCCode, clsCommon.myCstr(dr("MCC_CODE")))
                    objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.MCCName, clsCommon.myCstr(dr("MCC_NAME")))
                    objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.MCCUploaderCode, clsCommon.myCstr(dr("Mcc_Code_VLC_Uploader")))
                    objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.VSPCode, clsCommon.myCstr(dr("VSP_CODE")))
                    objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.VSP, clsCommon.myCstr(dr("VLC_Name")))
                    objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.VSPUploaderCode, clsCommon.myCstr(dr("VLC_Code_VLC_Uploader")))
                    objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.MilkTypeCB, clsCommon.myCstr(dr("MilkType")))
                    objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.UOM, clsCommon.myCstr(dr("UOM_Code")))
                    objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.VLCDataUploaderQty, clsCommon.myCstr(dr("Qty")))
                    objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.VLCDataUploaderFat, clsCommon.myCstr(dr("FAT_PER")))
                    objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.VLCDataUploaderSNF, clsCommon.myCstr(dr("SNF_PER")))
                    objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.VLCDataUploaderRate, clsCommon.myCstr(dr("RATE")))
                    objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.VLCDataUploaderAmt, clsCommon.myCstr(dr("AMOUNT")))

                    objSMSH.arrMobilNo = New List(Of String)()
                    objSMSH.arrMobilNo.Add(clsCommon.myCstr(dr("Phone1")))
                    objSMSH.SaveData(clsUserMgtCode.frmMilkSample, objSMSH, trans)
                    objSMSH = Nothing
                Next
            End If
        End If
    End Sub
    Shared Sub CreateMailContent(ByVal obj As clsMilkShiftEndMCC, ByVal trans As SqlTransaction)
        Dim strMailContent As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Email_Text from TSPL_ES_Content where Form_ID='" + clsUserMgtCode.frmMilkShiftEndMCC + "'", trans))
        If clsCommon.myLen(strMailContent) > 0 Then
            Dim qry As String = "select max(State_Code) as State_Code,max(TSPL_MCC_MASTER.Mcc_Code_VLC_Uploader) as Mcc_Code_VLC_Uploader,TSPL_MILK_SRN_HEAD.MCC_CODE,max(MCC_NAME) as MCC_NAME,convert(date,DOC_DATE,103) as DOC_DATE,cast(round(sum(Qty),2,2) as float) as Qty,max(TSPL_Mcc_UOM_DETAIL.UOM_Code) as UOM_Code,count(distinct(TSPL_MILK_SRN_HEAD.ROUTE_CODE)) as Route_Count,count(distinct(TSPL_MILK_SRN_HEAD.VLC_CODE)) as VLC_Count,convert(decimal(18,2), case when sum(Qty)>0 then sum (FAT_KG)*100/sum(Qty) else 0 end) as FATPER,convert(decimal(18,2), case when sum(Qty)>0 then sum(SNF_KG)*100/sum(Qty) else 0 end) as SNFPER,convert(decimal(18,2), case when sum(Qty)>0 then sum(Amount)/sum(Qty) else 0 end) as Rate,convert(decimal(18,2), sum(Amount) ) as Amount " + Environment.NewLine +
              " from TSPL_MILK_SRN_DETAIL " + Environment.NewLine +
              " inner join TSPL_MILK_SRN_HEAD on TSPL_MILK_SRN_DETAIL.DOC_CODE=TSPL_MILK_SRN_HEAD.DOC_CODE " + Environment.NewLine +
              " left join TSPL_Mcc_UOM_DETAIL on TSPL_Mcc_UOM_DETAIL.MCC_CODE=TSPL_MILK_SRN_HEAD.MCC_CODE and Stocking_Unit='Y' " + Environment.NewLine +
              " left join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code=TSPL_MILK_SRN_HEAD.MCC_CODE " + Environment.NewLine +
              " where  convert(date,doc_date,103)=convert(date,'" + clsCommon.GetPrintDate(obj.DOC_DATE, "dd/MMM/yyyy") + "',103) and shift='" + obj.SHIFT + "' and TSPL_MILK_SRN_HEAD.MCC_CODE ='" + obj.MCC_CODE + "' " + Environment.NewLine +
              " group by tspl_milk_srn_Head.mcc_code,convert(date,doc_date,103)"
            Dim dtData As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dtData IsNot Nothing AndAlso dtData.Rows.Count > 0 Then
                Dim objSMSH As New clsEMailHead()
                objSMSH.Created_Date = obj.DOC_DATE
                objSMSH.Email_Text = strMailContent
                objSMSH.Email_Text = objSMSH.Email_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.State, clsCommon.myCstr(dtData.Rows(0)("State_Code")))
                objSMSH.Email_Text = objSMSH.Email_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.MCCCode, clsCommon.myCstr(dtData.Rows(0)("MCC_CODE")))
                objSMSH.Email_Text = objSMSH.Email_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.MCCName, clsCommon.myCstr(dtData.Rows(0)("MCC_NAME")))
                objSMSH.Email_Text = objSMSH.Email_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.MCCUploaderCode, clsCommon.myCstr(dtData.Rows(0)("Mcc_Code_VLC_Uploader")))
                objSMSH.Email_Text = objSMSH.Email_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.VLCDataUploaderDate, clsCommon.GetPrintDate(clsCommon.myCDate(dtData.Rows(0)("DOC_DATE")), "dd/MMM/yyyy"))
                objSMSH.Email_Text = objSMSH.Email_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.TotalQty, clsCommon.myCstr(dtData.Rows(0)("Qty")))
                objSMSH.Email_Text = objSMSH.Email_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.UOM, clsCommon.myCstr(dtData.Rows(0)("UOM_Code")))
                objSMSH.Email_Text = objSMSH.Email_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.NoOfVLC, clsCommon.myCstr(dtData.Rows(0)("VLC_Count")))
                objSMSH.Email_Text = objSMSH.Email_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.NoOfRoute, clsCommon.myCstr(dtData.Rows(0)("Route_Count")))
                objSMSH.Email_Text = objSMSH.Email_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.VLCDataUploaderFat, clsCommon.myFormat(clsCommon.myCdbl(dtData.Rows(0)("FATPER"))))
                objSMSH.Email_Text = objSMSH.Email_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.VLCDataUploaderSNF, clsCommon.myFormat(clsCommon.myCdbl(dtData.Rows(0)("SNFPER"))))
                objSMSH.Email_Text = objSMSH.Email_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.VLCDataUploaderRate, clsCommon.myFormat(clsCommon.myCdbl(dtData.Rows(0)("Rate"))))
                objSMSH.Email_Text = objSMSH.Email_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.VLCDataUploaderAmt, clsCommon.myFormat(clsCommon.myCdbl(dtData.Rows(0)("Amount"))))
                objSMSH.Email_Text = objSMSH.Email_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.VLCDataUploaderShift, obj.SHIFT)

                objSMSH.SaveData(clsUserMgtCode.frmMilkShiftEndMCC, objSMSH, trans)
                objSMSH = Nothing
            End If
        End If
    End Sub

    Public Shared Function CreateAdjustmentToStockTransfer(ByVal obj As clsMilkShiftEndMCC, ByVal isCreateFluhsingEntry As Boolean, ByVal trans As SqlTransaction, ByVal MCCOutAdjNo As String, ByVal MCCOutAdjVoucherNo As String, ByVal PlantInAdjNo As String, ByVal PlantInAdjVoucherNo As String, ByVal ConsumptionAdjNo As String, ByVal ConsumptionAdjVoucherNo As String) As Boolean
        Dim qry As String
        Dim dtMCC As DataTable
        Dim dtItem As DataTable
        Try
            qry = "select AllowAutoMilkIn,AutoIn_Location,SILOIn_Location,MCC_in_Plant from TSPL_MCC_MASTER where MCC_Code='" + obj.MCC_CODE + "'"
            dtMCC = clsDBFuncationality.GetDataTable(qry, trans)
            If dtMCC IsNot Nothing AndAlso dtMCC.Rows.Count > 0 Then
                Dim isCreateConsumeEntry As Boolean = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.CreateConsumeEntry, clsFixedParameterCode.CreateConsumeEntry, trans)) > 0)
                Dim SettMilkShiftEndAutoAdjInItemCode As String = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.MilkShiftEndAutoAdjInItemCode, clsFixedParameterCode.MilkShiftEndAutoAdjInItemCode, trans))
                If clsCommon.myCdbl(dtMCC.Rows(0)("AllowAutoMilkIn")) = 1 Then
                    qry = "select xxx.Item_Code,TSPL_ITEM_MASTER.Item_Desc,Qty,ACC_QTY,UOM_Code,case when Qty<=0 then 0 else AMOUNT/Qty end as Rate,ACC_QTY,AMOUNT,case when ACC_QTY<=0 then 0 else convert(decimal(18,2), FAT_KG*100/ACC_QTY) end as FatPer,FAT_KG ,case when ACC_QTY<=0 then 0 else convert(decimal(18,2), SNF_KG*100/ACC_QTY) end as SNFPer,SNF_KG," + Environment.NewLine +
                    " Fat_Amt,SNF_Amt, " + Environment.NewLine +
                    " case when FAT_KG<=0 then 0 else Fat_Amt/FAT_KG end as FAT_Rate,Fat_Amt," + Environment.NewLine +
                    " case when SNF_KG<=0 then 0 else SNF_Amt/SNF_KG end as SNF_Rate,SNF_Amt,Rejected_Type,location_code " + Environment.NewLine +
                    " from (" + Environment.NewLine +
                    " select Item_Code,sum(Qty) as Qty,sum(ACC_QTY) AS ACC_QTY,max(UOM_Code) as UOM_Code,sum(NET_AMOUNT) AS AMOUNT,sum(FAT_KG) as FAT_KG,sum(SNF_KG) as SNF_KG," + Environment.NewLine +
                    " sum(Fat_Amt) as Fat_Amt,sum(SNF_Amt) as SNF_Amt,Rejected_Type,location_code from (" + Environment.NewLine +
                    " select TSPL_MILK_SRN_DETAIL.*,TSPL_INVENTORY_MOVEMENT_NEW.Fat_Amt,TSPL_INVENTORY_MOVEMENT_NEW.SNF_Amt,TSPL_INVENTORY_MOVEMENT_NEW.Trans_Type,isnull( TSPL_LOCATION_MASTER.Rejected_Type,'N') as Rejected_Type,TSPL_INVENTORY_MOVEMENT_NEW.Location_Code from " + Environment.NewLine +
                    " TSPL_MILK_SRN_DETAIL" + Environment.NewLine +
                    " left outer join TSPL_MILK_SRN_HEAD on TSPL_MILK_SRN_HEAD.DOC_CODE=TSPL_MILK_SRN_DETAIL.DOC_CODE " + Environment.NewLine +
                    " left outer join TSPL_INVENTORY_MOVEMENT_NEW on TSPL_INVENTORY_MOVEMENT_NEW.Source_Doc_No=TSPL_MILK_SRN_HEAD.DOC_CODE and TSPL_INVENTORY_MOVEMENT_NEW.Trans_Type='MCC-MSRN'" + Environment.NewLine +
                    " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_INVENTORY_MOVEMENT_NEW.Location_Code " + Environment.NewLine +
                    " where TSPL_MILK_SRN_HEAD.SHIFT='" + obj.SHIFT + "' and CONVERT(date, TSPL_MILK_SRN_HEAD.DOC_DATE,103)='" + clsCommon.GetPrintDate(obj.DOC_DATE, "dd/MMM/yyyy") + "' and TSPL_MILK_SRN_HEAD.MCC_CODE='" + obj.MCC_CODE + "'" + Environment.NewLine +
                    " )xx group by Item_Code,Rejected_Type,Location_Code" + Environment.NewLine +
                    " )xxx " + Environment.NewLine +
                    " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=xxx.Item_Code"
                    dtItem = clsDBFuncationality.GetDataTable(qry, trans)
                    If dtItem IsNot Nothing AndAlso dtItem.Rows.Count > 0 Then
                        Dim objAdjOut As New ClsAdjustments
                        objAdjOut.Trans_Type = "Out"
                        objAdjOut.Adjustment_Date = obj.DOC_DATE
                        objAdjOut.Posting_Date = obj.DOC_DATE
                        objAdjOut.EntryDateTime = obj.DOC_DATE
                        objAdjOut.IsMilkType = 1

                        objAdjOut.Description = "Adjustment for Stock Out from MCC.Shift End No :" & obj.DOC_CODE & ""
                        objAdjOut.Reference_Document = "MSE-MCC-OUT"
                        objAdjOut.Document_No = obj.DOC_CODE
                        objAdjOut.Arr = New List(Of ClsAdjustmentsDetails)


                        Dim objAdjIn As ClsAdjustments = New ClsAdjustments
                        objAdjIn.Trans_Type = "In"
                        objAdjIn.Adjustment_Date = obj.DOC_DATE
                        objAdjIn.Posting_Date = obj.DOC_DATE
                        objAdjIn.EntryDateTime = obj.DOC_DATE
                        objAdjIn.IsMilkType = 1
                        objAdjIn.Description = "Adjustment for Stock In Plant.Shift End No :" & obj.DOC_CODE & ""
                        objAdjIn.Reference_Document = "MSE-PLT-IN"
                        objAdjIn.Document_No = obj.DOC_CODE
                        objAdjIn.Arr = New List(Of ClsAdjustmentsDetails)


                        Dim objConsumbption As New ClsJobWorkRMConsum
                        objConsumbption.Trans_Type = "Out"
                        objConsumbption.Adjustment_Date = obj.DOC_DATE
                        objConsumbption.Posting_Date = obj.DOC_DATE
                        objConsumbption.EntryDateTime = obj.DOC_DATE
                        objConsumbption.IsMilkType = 1
                        objConsumbption.Description = "Adjustment for Consum.Shift End No :" & obj.DOC_CODE & ""
                        objConsumbption.Reference_Document = "MSE-PLT-CONSUME"
                        objConsumbption.Document_No = obj.DOC_CODE
                        objConsumbption.Arr = New List(Of ClsJobWorkRMConsumDetails)

                        Dim SNo As Integer = 0
                        For Each drItem As DataRow In dtItem.Rows
                            SNo += 1
                            ''Out From MCC
                            objAdjOut.Loc_Code = clsCommon.myCstr(drItem("Location_Code"))
                            objAdjOut.Loc_Desc = clsLocation.GetName(objAdjOut.Loc_Code, trans)
                            Dim objAdjTR As New ClsAdjustmentsDetails()
                            objAdjTR.Adjustment_Line_No = SNo
                            objAdjTR.Item_Code = clsCommon.myCstr(drItem("Item_Code"))
                            objAdjTR.Item_Description = clsCommon.myCstr(drItem("Item_Desc"))
                            objAdjTR.Adjustment_Type = "BD"
                            objAdjTR.Item_Quantity = clsCommon.myCdbl(drItem("Qty"))
                            objAdjTR.Item_Cost = clsCommon.myCdbl(drItem("AMOUNT"))
                            objAdjTR.mrp = 0
                            objAdjTR.Unit_Code = clsCommon.myCstr(drItem("UOM_Code"))
                            objAdjTR.fat_pers = clsCommon.myCdbl(drItem("FatPer"))
                            objAdjTR.fat_kg = clsCommon.myCdbl(drItem("FAT_KG"))
                            objAdjTR.snf_pers = clsCommon.myCdbl(drItem("SNFPer"))
                            objAdjTR.snf_kg = clsCommon.myCdbl(drItem("SNF_KG"))
                            objAdjTR.Unit_Cost = clsCommon.myCdbl(drItem("Rate"))
                            objAdjTR.fat_Rate = clsCommon.myCdbl(drItem("FAT_Rate"))
                            objAdjTR.fat_Amt = clsCommon.myCdbl(drItem("Fat_Amt"))
                            objAdjTR.snf_Rate = clsCommon.myCdbl(drItem("SNF_Rate"))
                            objAdjTR.snf_Amt = clsCommon.myCdbl(drItem("SNF_Amt"))
                            objAdjOut.Arr.Add(objAdjTR)

                            ''End Out From MCC

                            ''In To Plant 
                            If clsCommon.CompairString(clsCommon.myCstr(drItem("Rejected_Type")), "Y") = CompairStringResult.Equal Then
                                If clsCommon.myLen(clsCommon.myCstr(dtMCC.Rows(0)("AutoIn_Location"))) <= 0 Then
                                    Throw New Exception("Please set Auto In location of MCC :" + obj.MCC_CODE)
                                End If
                                objAdjIn.Loc_Code = clsLocation.GetRejectedLocation(clsCommon.myCstr(dtMCC.Rows(0)("AutoIn_Location")), trans)
                                If clsCommon.myLen(objAdjIn.Loc_Code) <= 0 Then
                                    Throw New Exception("Please set reject Location of location :" + clsCommon.myCstr(dtMCC.Rows(0)("AutoIn_Location")))
                                End If
                                objAdjIn.Loc_Desc = clsLocation.GetName(objAdjIn.Loc_Code, trans)
                            Else
                                ''RICHA BHA/25/10/18-000640 25 OCT,2018
                                If obj.AskSiloatShiftEnd = True AndAlso clsCommon.myLen(obj.SILOIn_Location) > 0 Then
                                    objAdjIn.Loc_Code = clsCommon.myCstr(obj.SILOIn_Location)
                                Else
                                    objAdjIn.Loc_Code = clsCommon.myCstr(dtMCC.Rows(0)("SILOIn_Location"))
                                End If
                                ''----------------------
                                If clsCommon.myLen(objAdjIn.Loc_Code) <= 0 Then
                                    objAdjIn.Loc_Code = clsCommon.myCstr(dtMCC.Rows(0)("AutoIn_Location"))
                                    If clsCommon.myLen(objAdjIn.Loc_Code) <= 0 Then
                                        Throw New Exception("Please set Silo In location of MCC :" + obj.MCC_CODE)
                                    End If
                                End If
                                objAdjIn.Loc_Desc = clsLocation.GetName(objAdjIn.Loc_Code, trans)

                                objAdjIn.MainLocationCode = clsCommon.myCstr(dtMCC.Rows(0)("AutoIn_Location"))
                                If clsCommon.myLen(objAdjIn.MainLocationCode) <= 0 Then
                                    Throw New Exception("Please set Auto In location of MCC :" + obj.MCC_CODE)
                                End If
                                objAdjIn.MainLocationDesc = clsLocation.GetName(objAdjIn.MainLocationCode, trans)
                            End If
                            objAdjTR = New ClsAdjustmentsDetails()
                            objAdjTR.Adjustment_Line_No = SNo
                            If obj.Mix_Milk Then
                                If clsCommon.myLen(SettMilkShiftEndAutoAdjInItemCode) > 0 Then
                                    qry = "select 1 from tspl_item_uom_detail where item_code='" + SettMilkShiftEndAutoAdjInItemCode + "' and UOM_Code='" + clsCommon.myCstr(drItem("UOM_Code")) + "'"
                                    Dim dtTemp1 As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                                    If dtTemp1 Is Nothing OrElse dtTemp1.Rows.Count <= 0 Then
                                        Throw New Exception("Item -" + SettMilkShiftEndAutoAdjInItemCode + " Not a valid UOM -" + clsCommon.myCstr(drItem("UOM_Code")))
                                    End If
                                    objAdjTR.Item_Code = SettMilkShiftEndAutoAdjInItemCode
                                    objAdjTR.Item_Description = clsItemMaster.GetItemName(SettMilkShiftEndAutoAdjInItemCode, trans)
                                Else
                                    Throw New Exception("Please provide " + clsFixedParameterType.MilkShiftEndAutoAdjInItemCode)
                                End If
                            Else
                                objAdjTR.Item_Code = clsCommon.myCstr(drItem("Item_Code"))
                                objAdjTR.Item_Description = clsCommon.myCstr(drItem("Item_Desc"))
                            End If


                            objAdjTR.Adjustment_Type = "BI"
                            objAdjTR.Item_Quantity = clsCommon.myCdbl(drItem("Qty"))
                            objAdjTR.Item_Cost = clsCommon.myCdbl(drItem("AMOUNT"))
                            objAdjTR.mrp = 0
                            objAdjTR.Unit_Code = clsCommon.myCstr(drItem("UOM_Code"))
                            objAdjTR.fat_pers = clsCommon.myCdbl(drItem("FatPer"))
                            objAdjTR.fat_kg = clsCommon.myCdbl(drItem("FAT_KG"))
                            objAdjTR.snf_pers = clsCommon.myCdbl(drItem("SNFPer"))
                            objAdjTR.snf_kg = clsCommon.myCdbl(drItem("SNF_KG"))
                            objAdjTR.Unit_Cost = clsCommon.myCdbl(drItem("Rate"))
                            objAdjTR.fat_Rate = clsCommon.myCdbl(drItem("FAT_Rate"))
                            objAdjTR.fat_Amt = clsCommon.myCdbl(drItem("Fat_Amt"))
                            objAdjTR.snf_Rate = clsCommon.myCdbl(drItem("SNF_Rate"))
                            objAdjTR.snf_Amt = clsCommon.myCdbl(drItem("SNF_Amt"))
                            objAdjIn.Arr.Add(objAdjTR)
                            ''End of In To Plant 




                            If isCreateConsumeEntry Then
                                ''Use in RM Consumption 
                                If clsCommon.CompairString(clsCommon.myCstr(drItem("Rejected_Type")), "Y") = CompairStringResult.Equal Then
                                    objConsumbption.Loc_Code = clsLocation.GetRejectedLocation(clsCommon.myCstr(dtMCC.Rows(0)("AutoIn_Location")), trans)
                                    objConsumbption.Loc_Desc = clsLocation.GetName(objConsumbption.Loc_Code, trans)
                                Else
                                    objConsumbption.Loc_Code = clsCommon.myCstr(dtMCC.Rows(0)("SILOIn_Location"))
                                    objConsumbption.Loc_Desc = clsLocation.GetName(objConsumbption.Loc_Code, trans)
                                    objConsumbption.MainLocationCode = clsCommon.myCstr(dtMCC.Rows(0)("AutoIn_Location"))
                                    objConsumbption.MainLocationDesc = clsLocation.GetName(objConsumbption.MainLocationCode, trans)
                                End If

                                Dim objConsumbptionTR As New ClsJobWorkRMConsumDetails()
                                objConsumbptionTR.Adjustment_Line_No = SNo
                                objConsumbptionTR.Item_Code = clsCommon.myCstr(drItem("Item_Code"))
                                objConsumbptionTR.Item_Description = clsCommon.myCstr(drItem("Item_Desc"))
                                objConsumbptionTR.Adjustment_Type = "BD"
                                objConsumbptionTR.Item_Quantity = clsCommon.myCdbl(drItem("Qty"))
                                objConsumbptionTR.Item_Cost = clsCommon.myCdbl(drItem("AMOUNT"))
                                objConsumbptionTR.mrp = 0
                                objConsumbptionTR.Unit_Code = clsCommon.myCstr(drItem("UOM_Code"))
                                objConsumbptionTR.fat_pers = clsCommon.myCdbl(drItem("FatPer"))
                                objConsumbptionTR.fat_kg = clsCommon.myCdbl(drItem("FAT_KG"))
                                objConsumbptionTR.snf_pers = clsCommon.myCdbl(drItem("SNFPer"))
                                objConsumbptionTR.snf_kg = clsCommon.myCdbl(drItem("SNF_KG"))
                                objConsumbptionTR.Unit_Cost = clsCommon.myCdbl(drItem("Rate"))
                                objConsumbptionTR.fat_Rate = clsCommon.myCdbl(drItem("FAT_Rate"))
                                objConsumbptionTR.fat_Amt = clsCommon.myCdbl(drItem("Fat_Amt"))
                                objConsumbptionTR.snf_Rate = clsCommon.myCdbl(drItem("SNF_Rate"))
                                objConsumbptionTR.snf_Amt = clsCommon.myCdbl(drItem("SNF_Amt"))
                                objConsumbption.Arr.Add(objConsumbptionTR)

                                ''End of Use in RM Consumption 
                            End If
                        Next
                        If objAdjOut.Arr IsNot Nothing AndAlso objAdjOut.Arr.Count > 0 Then
                            objAdjOut.SaveData(objAdjOut, True, MCCOutAdjNo, trans)
                            ClsAdjustments.PostData(objAdjOut.Adjustment_No, "Store Adjustment", trans, True, MCCOutAdjVoucherNo)
                        End If
                        If objAdjIn.Arr IsNot Nothing AndAlso objAdjIn.Arr.Count > 0 Then
                            objAdjIn.SaveData(objAdjIn, True, PlantInAdjNo, trans)
                            ClsAdjustments.PostData(objAdjIn.Adjustment_No, "Store Adjustment", trans, True, PlantInAdjVoucherNo)
                        End If
                        If objConsumbption.Adjustment_No IsNot Nothing AndAlso objConsumbption.Adjustment_No.Count > 0 Then
                            objConsumbption.SaveData(objConsumbption, True, ConsumptionAdjNo, trans, "RM")
                            ClsJobWorkRMConsum.PostData(objConsumbption.Adjustment_No, "Store Adjustment", trans, "RM", True, ConsumptionAdjVoucherNo)
                        End If
                    End If
                End If


                If isCreateFluhsingEntry Then
                    '--------------Create Flusing Adjustment Entry---------------------'
                    If clsCommon.myCdbl(dtMCC.Rows(0)("MCC_in_Plant")) = 1 Then
                        qry = "select MCC_Code from TSPL_MCC_MASTER where MCC_in_Plant=1"
                        Dim dtTemp As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                        If dtTemp IsNot Nothing AndAlso dtTemp.Rows.Count > 0 Then
                            If dtTemp.Rows.Count > 1 Then
                                qry = "There should be only one MCC in plant.MCC in Plant type MCC are "
                                For Each dr As DataRow In dtTemp.Rows
                                    qry += Environment.NewLine + clsCommon.myCstr(dr("MCC_Code"))
                                Next
                                Throw New Exception(qry)
                            End If
                        End If
                        qry = "select MCC_Code,Flusing_Adj_Qty_Shift_End from TSPL_MCC_MASTER where isnull(Flusing_Adj_Qty_Shift_End,0)>0"
                        dtTemp = clsDBFuncationality.GetDataTable(qry, trans)
                        If dtTemp IsNot Nothing AndAlso dtTemp.Rows.Count > 0 Then
                            For Each dr As DataRow In dtTemp.Rows
                                qry = "select TSPL_MILK_Shift_End_Head.DOC_CODE,TSPL_MILK_Shift_End_Head.DOC_DATE,TSPL_MILK_Shift_End_Head.SHIFT from TSPL_MILK_Shift_End_Head where MCC_CODE='" + clsCommon.myCstr(dr("MCC_Code")) + "' and Is_Flushing_Created is null"
                                Dim dtShiftEnd As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                                If dtShiftEnd IsNot Nothing AndAlso dtShiftEnd.Rows.Count > 0 Then
                                    For Each drShiftEnd As DataRow In dtShiftEnd.Rows
                                        ''MCC In 
                                        Dim objAdj As New ClsAdjustments
                                        objAdj.Trans_Type = "In"
                                        objAdj.Adjustment_Date = clsCommon.myCDate(drShiftEnd("DOC_DATE"))
                                        objAdj.Posting_Date = objAdj.Adjustment_Date
                                        objAdj.EntryDateTime = objAdj.Adjustment_Date
                                        objAdj.IsMilkType = 1
                                        objAdj.Loc_Code = clsCommon.myCstr(dr("MCC_Code"))
                                        objAdj.Loc_Desc = clsLocation.GetName(objAdj.Loc_Code, trans)
                                        objAdj.Description = "Adjustment for Flusing Stock In MCC.MCC :" & objAdj.Loc_Code & " Date :" + clsCommon.GetPrintDate(objAdj.Adjustment_Date) + " Shift :" + clsCommon.myCstr(drShiftEnd("SHIFT"))
                                        objAdj.Reference_Document = "MSE-FLU-MI"
                                        objAdj.Document_No = clsCommon.myCstr(drShiftEnd("DOC_CODE"))
                                        objAdj.Adjustment_Type = "FLG"
                                        objAdj.Arr = New List(Of ClsAdjustmentsDetails)

                                        Dim objAdjTR As New ClsAdjustmentsDetails()
                                        objAdjTR.Adjustment_Line_No = 1
                                        objAdjTR.Item_Code = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.MCCDefaultMilkItem, clsFixedParameterCode.MilkSetting, trans))
                                        objAdjTR.Item_Description = clsItemMaster.GetItemName(objAdjTR.Item_Code, trans)
                                        objAdjTR.Adjustment_Type = "QI"
                                        objAdjTR.Item_Quantity = clsCommon.myCdbl(dr("Flusing_Adj_Qty_Shift_End"))
                                        objAdjTR.Item_Cost = 0
                                        objAdjTR.mrp = 0
                                        objAdjTR.Unit_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select UOM_Code from TSPL_ITEM_UOM_DETAIL where Item_Code='" & objAdjTR.Item_Code & "' and Default_UOM='1' ", trans))
                                        If clsCommon.myLen(objAdjTR.Unit_Code) <= 0 Then
                                            objAdjTR.Unit_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select UOM_Code from TSPL_ITEM_UOM_DETAIL where Item_Code='" & objAdjTR.Item_Code & "' and Stocking_Unit='Y' ", trans))
                                        End If
                                        objAdjTR.fat_pers = 0
                                        objAdjTR.fat_kg = 0
                                        objAdjTR.snf_pers = 0
                                        objAdjTR.snf_kg = 0
                                        objAdjTR.Unit_Cost = 0
                                        objAdjTR.fat_Rate = 0
                                        objAdjTR.fat_Amt = 0
                                        objAdjTR.snf_Rate = 0
                                        objAdjTR.snf_Amt = 0
                                        objAdj.Arr.Add(objAdjTR)
                                        objAdj.SaveData(objAdj, True, "", trans)
                                        ClsAdjustments.PostData(objAdj.Adjustment_No, "Store Adjustment", trans)
                                        ''End MCC In
                                        ''BHA/06/07/18-000132 By balwinder on 10/07/2018
                                        ''MCC Out 
                                        objAdj = New ClsAdjustments
                                        objAdj.Trans_Type = "Out"
                                        objAdj.Adjustment_Date = clsCommon.myCDate(drShiftEnd("DOC_DATE"))
                                        objAdj.Posting_Date = objAdj.Adjustment_Date
                                        objAdj.EntryDateTime = objAdj.Adjustment_Date
                                        objAdj.IsMilkType = 1
                                        objAdj.Loc_Code = clsCommon.myCstr(dr("MCC_Code"))
                                        objAdj.Loc_Desc = clsLocation.GetName(objAdj.Loc_Code, trans)
                                        objAdj.Description = "Adjustment for Flusing Stock out MCC.MCC :" & objAdj.Loc_Code & " Date :" + clsCommon.GetPrintDate(objAdj.Adjustment_Date) + " Shift :" + clsCommon.myCstr(drShiftEnd("SHIFT"))
                                        objAdj.Reference_Document = "MSE-FLU-MO"
                                        objAdj.Document_No = clsCommon.myCstr(drShiftEnd("DOC_CODE"))
                                        objAdj.Adjustment_Type = "FLG"
                                        objAdj.Arr = New List(Of ClsAdjustmentsDetails)

                                        objAdjTR = New ClsAdjustmentsDetails()
                                        objAdjTR.Adjustment_Line_No = 1
                                        objAdjTR.Item_Code = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.MCCDefaultMilkItem, clsFixedParameterCode.MilkSetting, trans))
                                        objAdjTR.Item_Description = clsItemMaster.GetItemName(objAdjTR.Item_Code, trans)
                                        objAdjTR.Adjustment_Type = "QO"
                                        objAdjTR.Item_Quantity = clsCommon.myCdbl(dr("Flusing_Adj_Qty_Shift_End"))
                                        objAdjTR.Item_Cost = 0
                                        objAdjTR.mrp = 0
                                        objAdjTR.Unit_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select UOM_Code from TSPL_ITEM_UOM_DETAIL where Item_Code='" & objAdjTR.Item_Code & "' and Default_UOM='1' ", trans))
                                        If clsCommon.myLen(objAdjTR.Unit_Code) <= 0 Then
                                            objAdjTR.Unit_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select UOM_Code from TSPL_ITEM_UOM_DETAIL where Item_Code='" & objAdjTR.Item_Code & "' and Stocking_Unit='Y' ", trans))
                                        End If
                                        objAdjTR.fat_pers = 0
                                        objAdjTR.fat_kg = 0
                                        objAdjTR.snf_pers = 0
                                        objAdjTR.snf_kg = 0
                                        objAdjTR.Unit_Cost = 0
                                        objAdjTR.fat_Rate = 0
                                        objAdjTR.fat_Amt = 0
                                        objAdjTR.snf_Rate = 0
                                        objAdjTR.snf_Amt = 0
                                        objAdj.Arr.Add(objAdjTR)
                                        objAdj.SaveData(objAdj, True, "", trans)
                                        ClsAdjustments.PostData(objAdj.Adjustment_No, "Store Adjustment", trans)
                                        ''End MCC Out

                                        ''Plant In 
                                        objAdj = New ClsAdjustments
                                        objAdj.Trans_Type = "In"
                                        objAdj.Adjustment_Date = clsCommon.myCDate(drShiftEnd("DOC_DATE"))
                                        objAdj.Posting_Date = objAdj.Adjustment_Date
                                        objAdj.EntryDateTime = objAdj.Adjustment_Date
                                        objAdj.IsMilkType = 1
                                        objAdj.Loc_Code = clsCommon.myCstr(dtMCC.Rows(0)("SILOIn_Location"))
                                        If clsCommon.myLen(objAdj.Loc_Code) <= 0 Then
                                            Throw New Exception("Please set Silo In location of MCC :" + obj.MCC_CODE)
                                        End If

                                        objAdj.Loc_Desc = clsLocation.GetName(objAdj.Loc_Code, trans)
                                        objAdj.MainLocationCode = clsCommon.myCstr(dtMCC.Rows(0)("AutoIn_Location"))
                                        If clsCommon.myLen(objAdj.MainLocationCode) <= 0 Then
                                            Throw New Exception("Please set Auto In location of MCC :" + obj.MCC_CODE)
                                        End If
                                        objAdj.MainLocationDesc = clsLocation.GetName(objAdj.MainLocationCode, trans)

                                        objAdj.Description = "Adjustment for Flusing Stock In Plant.Plant :" & objAdj.Loc_Code & "," + objAdj.MainLocationCode + " Date :" + clsCommon.GetPrintDate(objAdj.Adjustment_Date) + " Shift :" + clsCommon.myCstr(drShiftEnd("SHIFT"))
                                        objAdj.Reference_Document = "MSE-FLU-PI"
                                        objAdj.Document_No = clsCommon.myCstr(drShiftEnd("DOC_CODE"))
                                        objAdj.Adjustment_Type = "FLG"
                                        objAdj.Arr = New List(Of ClsAdjustmentsDetails)

                                        objAdjTR = New ClsAdjustmentsDetails()
                                        objAdjTR.Adjustment_Line_No = 1
                                        objAdjTR.Item_Code = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.MCCDefaultMilkItem, clsFixedParameterCode.MilkSetting, trans))
                                        objAdjTR.Item_Description = clsItemMaster.GetItemName(objAdjTR.Item_Code, trans)
                                        objAdjTR.Adjustment_Type = "QI"
                                        objAdjTR.Item_Quantity = clsCommon.myCdbl(dr("Flusing_Adj_Qty_Shift_End"))
                                        objAdjTR.Item_Cost = 0
                                        objAdjTR.mrp = 0
                                        objAdjTR.Unit_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select UOM_Code from TSPL_ITEM_UOM_DETAIL where Item_Code='" & objAdjTR.Item_Code & "' and Default_UOM='1' ", trans))
                                        If clsCommon.myLen(objAdjTR.Unit_Code) <= 0 Then
                                            objAdjTR.Unit_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select UOM_Code from TSPL_ITEM_UOM_DETAIL where Item_Code='" & objAdjTR.Item_Code & "' and Stocking_Unit='Y' ", trans))
                                        End If
                                        objAdjTR.fat_pers = 0
                                        objAdjTR.fat_kg = 0
                                        objAdjTR.snf_pers = 0
                                        objAdjTR.snf_kg = 0
                                        objAdjTR.Unit_Cost = 0
                                        objAdjTR.fat_Rate = 0
                                        objAdjTR.fat_Amt = 0
                                        objAdjTR.snf_Rate = 0
                                        objAdjTR.snf_Amt = 0
                                        objAdj.Arr.Add(objAdjTR)
                                        objAdj.SaveData(objAdj, True, "", trans)
                                        ClsAdjustments.PostData(objAdj.Adjustment_No, "Store Adjustment", trans)
                                        ''End Plant In

                                        If isCreateConsumeEntry Then
                                            ''Use in RM Consumption of Plant
                                            objAdj = New ClsAdjustments
                                            objAdj.Trans_Type = "Out"
                                            objAdj.Adjustment_Date = clsCommon.myCDate(drShiftEnd("DOC_DATE"))
                                            objAdj.Posting_Date = objAdj.Adjustment_Date
                                            objAdj.EntryDateTime = objAdj.Adjustment_Date
                                            objAdj.IsMilkType = 1
                                            objAdj.Loc_Code = clsCommon.myCstr(dtMCC.Rows(0)("SILOIn_Location"))
                                            If clsCommon.myLen(objAdj.Loc_Code) <= 0 Then
                                                Throw New Exception("Please set Silo In location of MCC :" + obj.MCC_CODE)
                                            End If

                                            objAdj.Loc_Desc = clsLocation.GetName(objAdj.Loc_Code, trans)
                                            objAdj.MainLocationCode = clsCommon.myCstr(dtMCC.Rows(0)("AutoIn_Location"))
                                            If clsCommon.myLen(objAdj.MainLocationCode) <= 0 Then
                                                Throw New Exception("Please set Auto In location of MCC :" + obj.MCC_CODE)
                                            End If
                                            objAdj.MainLocationDesc = clsLocation.GetName(objAdj.MainLocationCode, trans)

                                            objAdj.Description = "Adjustment for Flusing Stock Out Plant.MCC :" & objAdj.Loc_Code & "," + objAdj.MainLocationCode + " Date :" + clsCommon.GetPrintDate(objAdj.Adjustment_Date) + " Shift :" + clsCommon.myCstr(drShiftEnd("SHIFT"))
                                            objAdj.Reference_Document = "MSE-FLU-PC"
                                            objAdj.Document_No = clsCommon.myCstr(drShiftEnd("DOC_CODE"))
                                            objAdj.Adjustment_Type = "FLG"
                                            objAdj.Arr = New List(Of ClsAdjustmentsDetails)

                                            objAdjTR = New ClsAdjustmentsDetails()
                                            objAdjTR.Adjustment_Line_No = 1
                                            objAdjTR.Item_Code = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.MCCDefaultMilkItem, clsFixedParameterCode.MilkSetting, trans))
                                            objAdjTR.Item_Description = clsItemMaster.GetItemName(objAdjTR.Item_Code, trans)
                                            objAdjTR.Adjustment_Type = "QO"
                                            objAdjTR.Item_Quantity = clsCommon.myCdbl(dr("Flusing_Adj_Qty_Shift_End"))
                                            objAdjTR.Item_Cost = 0
                                            objAdjTR.mrp = 0
                                            objAdjTR.Unit_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select UOM_Code from TSPL_ITEM_UOM_DETAIL where Item_Code='" & objAdjTR.Item_Code & "' and Default_UOM='1' ", trans))
                                            If clsCommon.myLen(objAdjTR.Unit_Code) <= 0 Then
                                                objAdjTR.Unit_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select UOM_Code from TSPL_ITEM_UOM_DETAIL where Item_Code='" & objAdjTR.Item_Code & "' and Stocking_Unit='Y' ", trans))
                                            End If
                                            objAdjTR.fat_pers = 0
                                            objAdjTR.fat_kg = 0
                                            objAdjTR.snf_pers = 0
                                            objAdjTR.snf_kg = 0
                                            objAdjTR.Unit_Cost = 0
                                            objAdjTR.fat_Rate = 0
                                            objAdjTR.fat_Amt = 0
                                            objAdjTR.snf_Rate = 0
                                            objAdjTR.snf_Amt = 0
                                            objAdj.Arr.Add(objAdjTR)
                                            objAdj.SaveData(objAdj, True, "", trans)
                                            ClsAdjustments.PostData(objAdj.Adjustment_No, "Store Adjustment", trans)
                                            ''End of Use in RM Consumption of Plant
                                        End If

                                        qry = "update TSPL_MILK_Shift_End_Head set Is_Flushing_Created=1 where DOC_CODE='" + objAdj.Document_No + "'"
                                        clsDBFuncationality.ExecuteNonQuery(qry, trans)
                                    Next
                                End If
                            Next
                        End If
                    End If
                    '--------------End of Create Flusing Adjustment Entry---------------------'
                End If

            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            qry = Nothing
            dtMCC = Nothing
        End Try
        Return True
    End Function


    Public Shared Function PostData(ByVal strDocNo As String, ByVal isCheckForPosted As Boolean) As Boolean
        'Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Code not found to Post")
            End If
            Dim strPostDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy hh:mm tt")
            Dim obj As clsMilkShiftEndMCC = clsMilkShiftEndMCC.GetData(strDocNo, NavigatorType.Current)

            If (obj Is Nothing OrElse clsCommon.myLen(obj.DOC_CODE) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            If (isCheckForPosted AndAlso obj.POSTED = 1) Then
                Throw New Exception("Already Post on :" + obj.Posting_Date)
            End If

            Dim qry As String = "Update TSPL_MILK_Shift_End_Head set POSTED=1, Posting_Date='" + strPostDate + "',Modified_By='" + objCommonVar.CurrentUserCode + "' where DOC_CODE ='" + strDocNo + "'"
            clsDBFuncationality.ExecuteNonQuery(qry)

            'Dim AutoMilkTransferIn As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select AllowAutoMilkIn from TSPL_MCC_MASTER where MCC_Code='" & obj.MCC_CODE & "'"))
            'If AutoMilkTransferIn = 1 Then
            '    Dim AutoIn_Location As Integer = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select AutoIn_Location from TSPL_MCC_MASTER where MCC_Code='" & obj.MCC_CODE & "'"))
            '    Dim objAdjOut As New ClsAdjustments
            '    objAdjOut.Adjustment_Date = obj.DOC_DATE
            '    objAdjOut.Posting_Date = obj.DOC_DATE
            '    objAdjOut.EntryDateTime = obj.DOC_DATE
            '    'objAdjOut.Against_Transfer_In_Doc_No = obj.DOC_NO
            '    objAdjOut.Loc_Code = obj.MCC_CODE
            '    objAdjOut.Loc_Desc = clsLocation.GetName(obj.MCC_CODE, trans)
            '    objAdjOut.Trans_Type = "Out"
            '    objAdjOut.IsMilkType = 1
            '    objAdjOut.Loc_Code = Silo
            '    objAdjOut.MainLocationCode = Loc()
            '    objAdjOut.Description = " Adjustment Against Bulk Milk SRN-PI Cost Adjustment For SRN NO: " & obj.arrDetail.Item(i).SRN_NO & " PI No: " & obj.DOC_NO & " Tanker No: " & tnkrNo & " Vendor : " & clsVendorMaster.GetName(vendorNo, trans) & " Location: " & clsLocation.GetName(Loc, trans)
            '    objAdjOut.Arr = New List(Of ClsAdjustmentsDetails)
            '    Dim objAdjOutTR As New ClsAdjustmentsDetails()

            '    objAdjOutTR.Item_Code = obj.arrDetail.Item(0).Item_Code
            '    objAdjOutTR.Item_Description = obj.arrDetail.Item(0).Item_Desc
            '    objAdjOutTR.Adjustment_Type = "CD"
            '    objAdjOutTR.Item_Quantity = 0
            '    objAdjOutTR.Item_Cost = diffamt
            '    objAdjOutTR.mrp = 0
            '    objAdjOutTR.Unit_Code = obj.arrDetail.Item(0).UOM
            '    objAdjOut.Arr.Add(objAdjOutTR)
            '    objAdjOut.SaveData(objAdjOut, True, "", trans)
            '    ClsAdjustments.PostData(objAdjOut.Adjustment_No, objAdjOut.Trans_Type, trans, False)
            'End If
            'trans.Commit()
        Catch ex As Exception
            'trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function GetDocCode(ByVal DocDate As Date, ByVal MCC_Code As String, ByVal Shift As String, Optional ByVal trans As SqlTransaction = Nothing) As String
        Dim qry As String
        qry = "SELECT DOC_CODE FROM TSPL_MILK_Shift_End_Head WHERE MCC_CODE='" & MCC_Code & "' AND convert(date,DOC_DATE,103)='" & clsCommon.GetPrintDate(DocDate, "dd/MMM/yyyy") & "' AND SHIFT='" & Shift & "'"
        Dim dt As DataTable
        dt = clsDBFuncationality.GetDataTable(qry, trans)
        If dt.Rows.Count > 0 Then
            Return dt.Rows(0).Item("DOC_CODE")
        Else
            Return ""
        End If
    End Function

    Public Shared Function GetFirstWeightmentSampleCode(ByVal DocDate As Date, ByVal MCC_Code As String, ByVal Shift As String, Optional ByVal trans As SqlTransaction = Nothing) As String
        Dim qry As String
        qry = "SELECT Created_Date FROM TSPL_MILK_Receipt_Head WHERE MCC_CODE='" & MCC_Code & "' AND convert(date,DOC_DATE,103)='" & clsCommon.GetPrintDate(DocDate, "dd/MMM/yyyy") & "' AND SHIFT='" & Shift & "'"
        Dim dt As DataTable
        dt = clsDBFuncationality.GetDataTable(qry, trans)
        If dt.Rows.Count > 0 Then
            Return dt.Rows(0).Item("Created_Date")
        Else
            Return ""
        End If
    End Function

    Public Shared Function GetLastSampleFATCode(ByVal DocDate As Date, ByVal MCC_Code As String, ByVal Shift As String, Optional ByVal trans As SqlTransaction = Nothing) As String
        Dim qry As String
        qry = "SELECT Modified_Date FROM TSPL_MILK_sample_Head WHERE MCC_CODE='" & MCC_Code & "' AND convert(date,DOC_DATE,103)='" & clsCommon.GetPrintDate(DocDate, "dd/MMM/yyyy") & "' AND SHIFT='" & Shift & "'"
        Dim dt As DataTable
        dt = clsDBFuncationality.GetDataTable(qry, trans)
        If dt.Rows.Count > 0 Then
            Return dt.Rows(0).Item("Modified_Date")
        Else
            Return ""
        End If
    End Function

    Public Shared Function GetShiftOpenTime(ByVal DocDate As Date, ByVal MCC_Code As String, ByVal Shift As String, Optional ByVal trans As SqlTransaction = Nothing) As String
        Dim qry As String
        qry = "SELECT Mcc_Shift_DATE FROM tspl_open_mcc_shift WHERE MCC_CODE='" & MCC_Code & "' AND convert(date,Mcc_Shift_DATE,103)='" & clsCommon.GetPrintDate(DocDate, "dd/MMM/yyyy") & "' AND SHIFT='" & Shift & "'"
        Dim dt As DataTable
        dt = clsDBFuncationality.GetDataTable(qry, trans)
        If dt.Rows.Count > 0 Then
            Return dt.Rows(0).Item("Mcc_Shift_DATE")
        Else
            Return ""
        End If
    End Function

    Public Shared Function GetPost(ByVal DocDate As Date, ByVal MCC_Code As String, ByVal Shift As String, Optional ByVal trans As SqlTransaction = Nothing) As Boolean
        Dim qry As String
        qry = "SELECT Posted FROM TSPL_MILK_Shift_End_Head WHERE MCC_CODE='" & MCC_Code & "' AND DOC_DATE='" & clsCommon.GetPrintDate(DocDate, "dd/MMM/yyyy") & "' AND SHIFT='" & Shift & "' and Posted=1"
        Dim dt As DataTable
        dt = clsDBFuncationality.GetDataTable(qry, trans)
        If dt.Rows.Count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Shared Function RecreateConsumptionEntry(ByVal arrShiftEndNo As ArrayList, ByVal trans As SqlTransaction) As Boolean
        If arrShiftEndNo Is Nothing OrElse arrShiftEndNo.Count <= 0 Then
            Throw New Exception("Please select at leat one shift end document")
        End If
        Try
            Dim qry As String = getShiftEndQuery(False) + " and TSPL_MILK_Shift_End_HEAD.DOC_CODE in (" + clsCommon.GetMulcallString(arrShiftEndNo) + ")"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    Dim obj As clsMilkShiftEndMCC = clsMilkShiftEndMCC.GetData(clsCommon.myCstr(dr("DOC_CODE")), NavigatorType.Current, trans)
                    If obj Is Nothing OrElse clsCommon.myLen(obj.DOC_CODE) <= 0 Then
                        Throw New Exception("Error in shift end no " + clsCommon.myCstr(dr("DOC_CODE")))
                    End If
                    If clsCommon.myLen(clsCommon.myCstr(dr("MCCOutAdjustmentNo"))) > 0 Then
                        clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, clsCommon.myCstr(dr("MCCOutAdjustmentNo")), "TSPL_ADJUSTMENT_HEADER", "Adjustment_No", "TSPL_ADJUSTMENT_DETAIL", "Adjustment_No", trans)
                        ClsAdjustments.ReverseAndUnpost(clsCommon.myCstr(dr("MCCOutAdjustmentNo")), trans)
                        ClsAdjustments.DeleteData(clsCommon.myCstr(dr("MCCOutAdjustmentNo")), "", trans)
                    End If
                    If clsCommon.myLen(clsCommon.myCstr(dr("PlantInAdjustmentNo"))) > 0 Then
                        clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, clsCommon.myCstr(dr("PlantInAdjustmentNo")), "TSPL_ADJUSTMENT_HEADER", "Adjustment_No", "TSPL_ADJUSTMENT_DETAIL", "Adjustment_No", trans)
                        ClsAdjustments.ReverseAndUnpost(clsCommon.myCstr(dr("PlantInAdjustmentNo")), trans)
                        ClsAdjustments.DeleteData(clsCommon.myCstr(dr("PlantInAdjustmentNo")), "", trans)
                    End If
                    If clsCommon.myLen(clsCommon.myCstr(dr("ConsumptionAdjustmentNo"))) > 0 Then
                        clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, clsCommon.myCstr(dr("ConsumptionAdjustmentNo")), "TSPL_ADJUSTMENT_HEADER", "Adjustment_No", "TSPL_ADJUSTMENT_DETAIL", "Adjustment_No", trans)
                        ClsAdjustments.ReverseAndUnpost(clsCommon.myCstr(dr("ConsumptionAdjustmentNo")), trans)
                        ClsAdjustments.DeleteData(clsCommon.myCstr(dr("ConsumptionAdjustmentNo")), "", trans)
                    End If
                    clsMilkShiftEndMCC.CreateAdjustmentToStockTransfer(obj, False, trans, clsCommon.myCstr(dr("MCCOutAdjustmentNo")), clsCommon.myCstr(dr("MCCOutAdjustmentVoucherNo")), clsCommon.myCstr(dr("PlantInAdjustmentNo")), clsCommon.myCstr(dr("PlantInAdjustmentVoucherNo")), clsCommon.myCstr(dr("ConsumptionAdjustmentNo")), clsCommon.myCstr(dr("ConsumptionAdjustmentVoucherNo")))
                Next
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function getShiftEndQuery(ByVal isCheckForAdjustment As Boolean) As String
        Dim qry As String = "select TSPL_MILK_Shift_End_HEAD.DOC_CODE,TSPL_MILK_Shift_End_HEAD.MCC_CODE,TSPL_MCC_MASTER.MCC_NAME,TSPL_MILK_Shift_End_HEAD.DOC_DATE,TSPL_MILK_Shift_End_HEAD.MCC_DATE,TSPL_MILK_Shift_End_HEAD.SHIFT,  MSE_MCC_OUT.Adjustment_No as MCCOutAdjustmentNo,MSE_MCC_OUT_JV.Voucher_No as MCCOutAdjustmentVoucherNo,MSE_PLT_IN.Adjustment_No as PlantInAdjustmentNo,MSE_PLT_IN_JV.Voucher_No as PlantInAdjustmentVoucherNo,MSE_PLT_CONSUME.Adjustment_No as ConsumptionAdjustmentNo,MSE_PLT_CONSUME_JV.Voucher_No as ConsumptionAdjustmentVoucherNo" + Environment.NewLine +
        " from TSPL_MILK_Shift_End_HEAD" + Environment.NewLine +
        " left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code=TSPL_MILK_Shift_End_HEAD.MCC_CODE " + Environment.NewLine +
        " left outer join TSPL_ADJUSTMENT_HEADER as MSE_MCC_OUT on MSE_MCC_OUT.Document_No=TSPL_MILK_Shift_End_HEAD.DOC_CODE and MSE_MCC_OUT.Reference_Document='MSE-MCC-OUT'" + Environment.NewLine +
        " left outer join TSPL_JOURNAL_MASTER as MSE_MCC_OUT_JV on MSE_MCC_OUT_JV.Source_Doc_No=MSE_MCC_OUT.Adjustment_No " + Environment.NewLine +
        " left outer join TSPL_ADJUSTMENT_HEADER as MSE_PLT_IN on MSE_PLT_IN.Document_No=TSPL_MILK_Shift_End_HEAD.DOC_CODE and MSE_PLT_IN.Reference_Document='MSE-PLT-IN'" + Environment.NewLine +
        " left outer join TSPL_JOURNAL_MASTER as MSE_PLT_IN_JV on MSE_PLT_IN_JV.Source_Doc_No=MSE_PLT_IN.Adjustment_No" + Environment.NewLine +
        " left outer join TSPL_ADJUSTMENT_HEADER as MSE_PLT_CONSUME on MSE_PLT_CONSUME.Document_No=TSPL_MILK_Shift_End_HEAD.DOC_CODE and MSE_PLT_CONSUME.Reference_Document='MSE-PLT-CONSUME'" + Environment.NewLine +
        " left outer join TSPL_JOURNAL_MASTER as MSE_PLT_CONSUME_JV on MSE_PLT_CONSUME_JV.Source_Doc_No=MSE_PLT_CONSUME.Adjustment_No" + Environment.NewLine +
        " where 2=2 "
        If isCheckForAdjustment Then
            qry += " and len( isnull( MSE_MCC_OUT.Adjustment_No,''))>0 "
        End If

        Return qry

    End Function

    Public Shared Function CreateMilkShiftEntrRouteDetailsWithProvision(ByVal strShiftEndNo As String, ByVal settSeprateDistanceMorningEveningShift As Boolean, ByVal isSendSMS As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim objSE As clsMilkShiftEndMCC = clsMilkShiftEndMCC.GetData(strShiftEndNo, NavigatorType.Current, trans)
        Dim ArrSEDetail As New List(Of clsMilkShiftEndMCC_Route_Detail)
        Dim qry As String = "select TSPL_MILK_SRN_DETAIL.ROUTE_CODE,charge_amount,Head_Load_amount,Own_Asset_Amount,Std_Qty from  (select  ROUTE_CODE," _
               & " sum(Head_load_amount) as Head_load_amount,sum(Own_Asset_Amount) as Own_Asset_Amount,sum(isnull(Std_Qty,0)) as Std_Qty from TSPL_MILK_SRN_DETAIL inner join TSPL_MILK_SRN_HEAD on TSPL_MILK_SRN_HEAD.DOC_CODE=TSPL_MILK_SRN_DETAIL.DOC_CODE " _
               & " and TSPL_MILK_SRN_HEAD.MCC_CODE='" & objSE.MCC_CODE & "' and convert(date,TSPL_MILK_SRN_Head.DOC_DATE,103)='" & clsCommon.GetPrintDate(objSE.DOC_DATE, "dd-MMM-yyyy") & "' and TSPL_MILK_SRN_Head.SHIFT='" & objSE.SHIFT & "' " _
               & " group by ROUTE_CODE) TSPL_MILK_SRN_DETAIL  left join (select  ROUTE_CODE," _
               & " sum(Charge_Amount) as Charge_Amount from TSPL_MILK_SRN_Price_Charge_Detail inner join TSPL_MILK_SRN_HEAD on TSPL_MILK_SRN_HEAD.DOC_CODE=" _
               & " TSPL_MILK_SRN_Price_Charge_Detail.DOC_CODE and TSPL_MILK_SRN_HEAD.MCC_CODE='" & objSE.MCC_CODE & "'  and convert(date,TSPL_MILK_SRN_Head.DOC_DATE,103)='" & clsCommon.GetPrintDate(objSE.DOC_DATE, "dd-MMM-yyyy") & "'  and TSPL_MILK_SRN_Head.SHIFT='" & objSE.SHIFT & "' " _
               & " group by ROUTE_CODE) TSPL_MILK_SRN_Price_Charge_Detail on TSPL_MILK_SRN_Price_Charge_Detail.ROUTE_CODE=" _
               & " TSPL_MILK_SRN_DETAIL.ROUTE_CODE "
        Dim DtCharge_detail As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        Dim dtMilkReceipt As DataTable = clsDBFuncationality.GetDataTable("select DOC_CODE from TSPL_MILK_RECEIPT_HEAD where MCC_CODE='" + objSE.MCC_CODE + "' and convert(date, DOC_DATE,103)='" + clsCommon.GetPrintDate(objSE.MCC_DATE, "dd/MMM/yyyy") + "' and SHIFT='" + objSE.SHIFT + "'", trans)
        If dtMilkReceipt IsNot Nothing AndAlso dtMilkReceipt.Rows.Count > 0 Then
            For Each drMilkReceipt As DataRow In dtMilkReceipt.Rows
                qry = "select ROUTE_CODE from TSPL_MILK_RECEIPT_DETAIL where DOC_CODE='" + clsCommon.myCstr(drMilkReceipt("DOC_CODE")) + "' group by ROUTE_CODE"
                Dim dtMilkReceiptRoute As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                If dtMilkReceiptRoute IsNot Nothing AndAlso dtMilkReceiptRoute.Rows.Count > 0 Then
                    For Each drMilkReceiptRoute As DataRow In dtMilkReceiptRoute.Rows
                        qry = "select distinct rm.route_CODE,rm.ROUTE_NAME,rm.MCC_CODE,rm.KILOMETER,rm.Kilometer_Morning,rm.Kilometer_Evening,rd.VEHICLE_CODE,pvm.Description as Vehicle_Name," _
                               & " status,case when status='Day/Diesel' then Diesel_Rate/coalesce(Avg_km_Ltr,1) when status='Rate/K.M' then Price_KM when status='Rate/Ltr' then Price_Ltr_KG   " _
                               & " when status='Rental' then case when Rental_Type='Day' then RenTal_Amount/2 when Rental_Type='Month' then RenTal_Amount" _
                               & " /(2*" & Date.DaysInMonth(Today.Year, Today.Month) & ") when Rental_Type='Year' then RenTal_Amount/" _
                               & " (24*" & Date.DaysInMonth(Today.Year, Today.Month) & ") end end as Rate,case when status='Day/Diesel' then Shift_Charges else 0 " _
                               & " end as Shift_Charges,case when status='Rate/Ltr' then case when Rate_Type='KG' then ACC_Wgt when  Rate_Type='LTR' then " _
                               & " tt.Milk_Weight end else 0 end as Milk_Weight,ACC_Wgt as Actual_Wgt,tt.Milk_wg as Milk_wgt,coalesce(is_additional,'F') as " _
                               & " Is_Additional, pvm.Vendor_Code from tspl_mcc_route_master rm inner " _
                               & " join TSPL_MILK_RECEIPT_DETAIL rd on rm.Route_Code=rd.ROUTE_CODE inner join TSPL_Primary_Vehicle_Master pvm on pvm.Vehicle_Code=rd.VEHICLE_CODE " _
                               & " inner join (select SUM(ACC_Weight_LTR) as Milk_Weight,SUM(Acc_Weight) as ACC_Wgt,SUM(Milk_Weight) as Milk_Wg ,vehicle_code as vh from TSPL_MILK_RECEIPT_DETAIL where " _
                               & " TSPL_MILK_RECEIPT_DETAIL.doc_code='" & clsCommon.myCstr(drMilkReceipt("DOC_CODE")) & "' group by VEHICLE_CODE) tt on tt.vh=rd.VEHICLE_CODE where rm.Route_Code='" & clsCommon.myCstr(drMilkReceiptRoute("ROUTE_CODE")) & "' and rd.doc_code='" & clsCommon.myCstr(drMilkReceipt("DOC_CODE")) & "'"
                        Dim dtRoute As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                        Dim dr() As DataRow = DtCharge_detail.Select("route_CODE='" & clsCommon.myCstr(drMilkReceiptRoute("ROUTE_CODE")) & "'")
                        If dtRoute.Rows.Count > 0 Then
                            Dim objSHDetail As New clsMilkShiftEndMCC_Route_Detail
                            objSHDetail.deduction_of_Transporter = 0
                            objSHDetail.Route_CODE = clsCommon.myCstr(drMilkReceiptRoute("ROUTE_CODE"))
                            objSHDetail.Route_Name = clsCommon.myCstr(dtRoute.Rows(0).Item("Route_Name"))
                            objSHDetail.Opening_KM = 0
                            objSHDetail.Closing_KM = 0
                            objSHDetail.Total_KM = 0
                            If settSeprateDistanceMorningEveningShift Then
                                If clsCommon.CompairString(objSE.SHIFT, "M") = CompairStringResult.Equal Then
                                    objSHDetail.Actual_KM = clsCommon.myCdbl(dtRoute.Rows(0).Item("Kilometer_Morning"))
                                Else
                                    objSHDetail.Actual_KM = clsCommon.myCdbl(dtRoute.Rows(0).Item("Kilometer_Evening"))
                                End If
                            Else
                                objSHDetail.Actual_KM = clsCommon.myCdbl(dtRoute.Rows(0).Item("Kilometer"))
                            End If
                            objSHDetail.Status = clsCommon.myCstr(dtRoute.Rows(0).Item("Status"))
                            objSHDetail.Diff_Km = 0
                            objSHDetail.Actual_Payable_KM = 0
                            objSHDetail.Reason = ""
                            objSHDetail.DOC_DATE = objSE.MCC_DATE


                            objSHDetail.Vehicle_Code = clsCommon.myCstr(dtRoute.Rows(0).Item("Vehicle_code"))
                            objSHDetail.Vehicle_Name = clsCommon.myCstr(dtRoute.Rows(0).Item("Vehicle_Name"))
                            objSHDetail.Is_Head_Load = clsMilkShiftEndMCC.GetHeadLoadData(clsCommon.myCstr(drMilkReceiptRoute("ROUTE_CODE")), trans)

                            '============Weight===================================
                            objSHDetail.Milk_Weight = clsCommon.myCdbl(dtRoute.Rows(0).Item("Milk_wgt"))
                            objSHDetail.Actual_Weight = clsCommon.myCdbl(dtRoute.Rows(0).Item("Actual_wgt"))
                            '=========================================================================
                            If clsCommon.CompairString(objSHDetail.Is_Head_Load, "False") = CompairStringResult.Equal Then
                                objSHDetail.Rate_KM = Math.Round(clsCommon.myCdbl(clsCommon.myCdbl(dtRoute.Rows(0).Item("Rate"))), 2)
                                objSHDetail.Amount = Math.Round((IIf(clsCommon.myCdbl(dtRoute.Rows(0).Item("Milk_weight")) > 0, clsCommon.myCdbl(dtRoute.Rows(0).Item("Rate")) * clsCommon.myCdbl(dtRoute.Rows(0).Item("Milk_Weight")), clsCommon.myCdbl(dtRoute.Rows(0).Item("Shift_Charges")) + clsCommon.myCdbl(dtRoute.Rows(0).Item("Rate")) * clsCommon.myCdbl(dtRoute.Rows(0).Item("Kilometer")))), 2)
                                objSHDetail.Shift_Charge = clsCommon.myCdbl(dtRoute.Rows(0).Item("Shift_Charges"))
                                objSHDetail.Is_Head_Load = "0"
                            Else
                                objSHDetail.Rate_KM = 0
                                objSHDetail.Amount = 0
                                objSHDetail.Shift_Charge = 0
                                objSHDetail.Is_Head_Load = "1"
                            End If

                            'If clsCommon.myCdbl(dtRoute.Rows(0).Item("Milk_weight")) > 0 Then
                            '    GvRoute.Tag = dtRoute.Rows(0).Item("Milk_weight")
                            '    objSHDetail.we GvRoute.Rows(GvRoute.Rows.Count - 1).Cells(col_Weight).Value = clsCommon.myCdbl(dtRoute.Rows(0).Item("Milk_weight"))
                            'End If
                            If dr.Length > 0 Then
                                objSHDetail.Std_Qty = clsCommon.myCdbl(dr(0)("Std_Qty"))
                                objSHDetail.Charge_Amt = clsCommon.myCdbl(dr(0)("Charge_amount"))
                                objSHDetail.Head_Load_Amt = clsCommon.myCdbl(dr(0)("Head_Load_amount"))
                                objSHDetail.Own_Asset_Amount = clsCommon.myCdbl(dr(0)("Own_Asset_Amount"))
                            Else
                                objSHDetail.Std_Qty = "0"
                                objSHDetail.Charge_Amt = "0"
                                objSHDetail.Head_Load_Amt = "0"
                                objSHDetail.Own_Asset_Amount = "0"
                            End If
                            ArrSEDetail.Add(objSHDetail)
                        End If
                    Next
                End If
            Next
        End If
        If ArrSEDetail IsNot Nothing AndAlso ArrSEDetail.Count > 0 Then
            clsMilkShiftEndMCC_Route_Detail.SaveData(objSE.DOC_CODE, ArrSEDetail, objSE.MCC_CODE, trans, True, objSE)
        End If
        If isSendSMS Then
            clsMilkShiftEndMCC.SendSMSDataNew(objSE, trans)
        End If
        Return True
    End Function
End Class


Public Class clsMilkShiftEndMCCDetail
#Region "Variables"
    Public DOC_CODE As String
    Public VLC_DOC_CODE As String
    Public VLC_CODE As String
    Public VSP_CODE As String
    Public VEHICLE_CODE As String
    Public Remarks As String
    Public Attachment_Id As String


    Public MCC_CODE As String
    Public DOC_DATE As DateTime
    Public SHIFT As String
    Public deduction_of_VSP As Double
    Public Deduction_Reason As String
    Public Amount_or_Rate As String
    Public Rejected_or_Acccepted As String
    Public vlc_procurement_mp_data As String
    Public Route_Code As String


#End Region


    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsMilkShiftEndMCCDetail), ByVal trans As SqlTransaction, ByVal isNewentry As Boolean) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsMilkShiftEndMCCDetail In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "DOC_CODE", strDocNo)
                clsCommon.AddColumnsForChange(coll, "VLC_DOC_CODE", obj.VLC_DOC_CODE)
                clsCommon.AddColumnsForChange(coll, "VLC_CODE", obj.VLC_CODE)
                clsCommon.AddColumnsForChange(coll, "VSP_CODE", obj.VSP_CODE)
                clsCommon.AddColumnsForChange(coll, "VEHICLE_CODE", obj.VEHICLE_CODE)
                clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks)
                clsCommon.AddColumnsForChange(coll, "SHIFT", obj.SHIFT)
                clsCommon.AddColumnsForChange(coll, "Deduction_of_vsp", obj.deduction_of_VSP)
                clsCommon.AddColumnsForChange(coll, "Deduction_Reason", obj.Deduction_Reason)
                clsCommon.AddColumnsForChange(coll, "A_or_R", obj.Amount_or_Rate)
                clsCommon.AddColumnsForChange(coll, "Rejected_or_Acccepted", obj.Rejected_or_Acccepted)
                clsCommon.AddColumnsForChange(coll, "MCC_CODE", obj.MCC_CODE)
                clsCommon.AddColumnsForChange(coll, "DOC_DATE", clsCommon.GetPrintDate(obj.DOC_DATE, "dd/MMM/yyyy hh:mm:ss tt"))
                If isNewentry Then
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MILK_Shift_End_DETAIL", OMInsertOrUpdate.Insert, "TSPL_MILK_Shift_End_DETAIL.DOC_CODE='" + strDocNo + "'", trans)
                Else
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MILK_Shift_End_DETAIL", OMInsertOrUpdate.Update, "TSPL_MILK_Shift_End_DETAIL.DOC_CODE='" + strDocNo + "' and TSPL_MILK_Shift_End_DETAIL.VLC_DOC_CODE='" & obj.VLC_DOC_CODE & "'", trans)
                End If
            Next
        End If
        Return True
    End Function
End Class


Public Class clsMilkShiftEndMCC_Route_Detail
#Region "Variables"
    Public DOC_CODE As String
    Public Route_CODE As String
    Public Route_Name As String
    Public Opening_KM As String
    Public Closing_KM As String
    Public Total_KM As String
    Public Status As String
    Public Actual_KM As String
    Public Actual_Payable_KM As String


    Public Diff_Km As String
    Public DOC_DATE As DateTime
    Public Reason As String
    Public Deduction_Reason As String
    Public deduction_of_Transporter As Double
    Public Milk_Weight As Double
    Public Actual_Weight As Double
    Public Attachment_Id As String
    Public Truck_Sheet_of_TransPorter As String

    Public Vehicle_Code As String

    Public Vehicle_Name As String
    Public Rate_KM As Double
    Public Amount As Double
    Public Shift_Charge As String
    Public Is_Head_Load As String
    Public Charge_Amt As String
    Public Head_Load_Amt As String
    Public Own_Asset_Amount As String
    Public Std_Qty As Double

    Public FixedCharge As Decimal ''Not Table Filed
    Public FixedAmount As Decimal ''Not Table Filed
#End Region


    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsMilkShiftEndMCC_Route_Detail), ByVal Mcc_code As String, ByVal trans As SqlTransaction, ByVal isNewentry As Boolean, ByVal objShiftHead As clsMilkShiftEndMCC) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            Dim SettingStdQty As Boolean = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterCode.PTMRatePerLtrKGOnStdQty, clsFixedParameterType.PTMRatePerLtrKGOnStdQty, trans)) > 0, True, False)
            Dim settApplyEffectiveStartDate As Boolean = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ApplyEffectiveStartDate, clsFixedParameterCode.ApplyEffectiveStartDate, trans)) > 0, True, False)
            Dim settSeprateDistanceMorningEveningShift As Boolean = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.SeprateDistanceMorningEveningShift, clsFixedParameterCode.SeprateDistanceMorningEveningShift, trans)) > 0, True, False)
            Dim dt As DataTable
            For Each obj As clsMilkShiftEndMCC_Route_Detail In Arr
                Dim qry As String = ""
                Dim flag As Boolean = True
                If settApplyEffectiveStartDate Then
                    qry = "select top 1 * from (select  (case when Effective_Start_Date is null then '01/Jan/1900' else Effective_Start_Date end) as Effective_Start_Date ,Hist_Version,KiloMeter from ( " + Environment.NewLine +
                    "select Effective_Start_Date,-1 as Hist_Version,KiloMeter from TSPL_MCC_ROUTE_MASTER  where Route_Code='" + obj.Route_CODE + "'" + Environment.NewLine +
                    "union all" + Environment.NewLine +
                    "select Effective_Start_Date, Hist_Version,KiloMeter from TSPL_MCC_ROUTE_MASTER_ESD where Route_Code='" + obj.Route_CODE + "'" + Environment.NewLine +
                    ")xx)xxx where Effective_Start_Date <= '" + clsCommon.GetPrintDate(objShiftHead.MCC_DATE, "dd/MMM/yyyy") + "'" + Environment.NewLine +
                    "order by Effective_Start_Date  desc"
                    dt = clsDBFuncationality.GetDataTable(qry, trans)
                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                        If clsCommon.myCdbl(dt.Rows(0)("Hist_Version")) > 0 Then
                            qry = "select sum(Out_Route_KM) from TSPL_MCC_ROUTE_VLC_MAPPING_ESD where Route_Code='" + obj.Route_CODE + "' and Out_Route=1 and Hist_Version='" + clsCommon.myCstr(clsCommon.myCdbl(dt.Rows(0)("Hist_Version"))) + "' and  not exists (" + Environment.NewLine +
                                  "select 1 from  TSPL_MILK_RECEIPT_DETAIL inner join TSPL_MILK_RECEIPT_HEAD on TSPL_MILK_RECEIPT_HEAD.DOC_CODE=TSPL_MILK_RECEIPT_DETAIL.DOC_CODE where TSPL_MILK_RECEIPT_HEAD.MCC_CODE='" + objShiftHead.MCC_CODE + "' and CONVERT(date,TSPL_MILK_RECEIPT_HEAD.DOC_DATE,103)='" + clsCommon.GetPrintDate(objShiftHead.MCC_DATE, "dd/MMM/yyyy") + "' and	TSPL_MILK_RECEIPT_HEAD.SHIFT='" + objShiftHead.SHIFT + "'  and TSPL_MILK_RECEIPT_DETAIL.VLC_CODE=TSPL_MCC_ROUTE_VLC_MAPPING_ESD.VLC_CODE)"
                            flag = False
                            obj.Actual_KM = clsCommon.myCdbl(dt.Rows(0)("KiloMeter"))
                        End If
                    End If
                End If
                If flag Then
                    qry = "select sum(Out_Route_KM) from TSPL_MCC_ROUTE_VLC_MAPPING where Route_Code='" + obj.Route_CODE + "' and Out_Route=1 and  not exists (" + Environment.NewLine +
                    "select 1 from  TSPL_MILK_RECEIPT_DETAIL inner join TSPL_MILK_RECEIPT_HEAD on TSPL_MILK_RECEIPT_HEAD.DOC_CODE=TSPL_MILK_RECEIPT_DETAIL.DOC_CODE where TSPL_MILK_RECEIPT_HEAD.MCC_CODE='" + objShiftHead.MCC_CODE + "' and CONVERT(date,TSPL_MILK_RECEIPT_HEAD.DOC_DATE,103)='" + clsCommon.GetPrintDate(objShiftHead.MCC_DATE, "dd/MMM/yyyy") + "' and	TSPL_MILK_RECEIPT_HEAD.SHIFT='" + objShiftHead.SHIFT + "'  and TSPL_MCC_ROUTE_VLC_MAPPING.VLC_CODE=TSPL_MILK_RECEIPT_DETAIL.VLC_CODE)"
                End If

                Dim OutRouteKM As Decimal = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))

                obj.Actual_KM = obj.Actual_KM - OutRouteKM
                If obj.Actual_KM < 0 Then
                    obj.Actual_KM = 0
                End If
                Dim coll As New Hashtable()
                obj.DOC_CODE = strDocNo
                clsCommon.AddColumnsForChange(coll, "DOC_CODE", strDocNo)
                clsCommon.AddColumnsForChange(coll, "Route_CODE", obj.Route_CODE)
                clsCommon.AddColumnsForChange(coll, "Opening_KM", obj.Opening_KM)
                clsCommon.AddColumnsForChange(coll, "Closing_KM", obj.Closing_KM)
                clsCommon.AddColumnsForChange(coll, "Total_KM", obj.Total_KM)
                clsCommon.AddColumnsForChange(coll, "Actual_Payable_KM", obj.Actual_Payable_KM)
                clsCommon.AddColumnsForChange(coll, "Diff_Km", obj.Diff_Km)
                clsCommon.AddColumnsForChange(coll, "Milk_Weight", obj.Milk_Weight)
                clsCommon.AddColumnsForChange(coll, "Actual_Weight", obj.Actual_Weight)
                clsCommon.AddColumnsForChange(coll, "Vehicle_Code", obj.Vehicle_Code)
                clsCommon.AddColumnsForChange(coll, "Shift_Charge", obj.Shift_Charge)
                clsCommon.AddColumnsForChange(coll, "Charge_Amount", obj.Charge_Amt)
                clsCommon.AddColumnsForChange(coll, "Head_Load_Amount", obj.Head_Load_Amt)
                clsCommon.AddColumnsForChange(coll, "Own_Asset_Amount", obj.Own_Asset_Amount)
                clsCommon.AddColumnsForChange(coll, "Reason", obj.Reason)
                clsCommon.AddColumnsForChange(coll, "Deduction_Reason", obj.Deduction_Reason)
                clsCommon.AddColumnsForChange(coll, "DOC_DATE", clsCommon.GetPrintDate(obj.DOC_DATE, "dd/MMM/yyyy hh:mm:ss tt"))
                clsCommon.AddColumnsForChange(coll, "deduction_of_Transporter", obj.deduction_of_Transporter)
                clsCommon.AddColumnsForChange(coll, "Is_Head_Load", obj.Is_Head_Load)
                clsCommon.AddColumnsForChange(coll, "Std_Qty", obj.Std_Qty)
                If obj.Is_Head_Load > 0 Then
                    clsCommon.AddColumnsForChange(coll, "Status", "")
                    clsCommon.AddColumnsForChange(coll, "Rate", 0)
                    clsCommon.AddColumnsForChange(coll, "Amount", 0)
                    clsCommon.AddColumnsForChange(coll, "Actual_KM", 0)
                Else
                    flag = True
                    If settApplyEffectiveStartDate Then
                        qry = "select top 1 * from (select  (case when Effective_Start_Date is null then '01/Jan/1900' else Effective_Start_Date end) as Effective_Start_Date ,Hist_Version from ( " + Environment.NewLine +
                        "select Effective_Start_Date,-1 as Hist_Version from TSPL_Primary_Vehicle_Master  where Vehicle_Code='" + obj.Vehicle_Code + "'" + Environment.NewLine +
                        "union all" + Environment.NewLine +
                        "select Effective_Start_Date, Hist_Version from TSPL_Primary_Vehicle_Master_ESD where Vehicle_Code='" + obj.Vehicle_Code + "'" + Environment.NewLine +
                        ")xx)xxx where Effective_Start_Date <= '" + clsCommon.GetPrintDate(objShiftHead.MCC_DATE, "dd/MMM/yyyy") + "'" + Environment.NewLine +
                        "order by Effective_Start_Date  desc"
                        dt = clsDBFuncationality.GetDataTable(qry, trans)
                        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                            If clsCommon.myCdbl(dt.Rows(0)("Hist_Version")) > 0 Then
                                qry = "select  TSPL_PRIMARY_VEHICLE_MASTER_ESD.Status,TSPL_PRIMARY_VEHICLE_MASTER_ESD.Shift_Charges,TSPL_PRIMARY_VEHICLE_MASTER_ESD.Avg_Km_Ltr,TSPL_PRIMARY_VEHICLE_MASTER_ESD.Diesel_Rate,TSPL_PRIMARY_VEHICLE_MASTER_ESD.Price_KM, TSPL_PRIMARY_VEHICLE_MASTER_ESD.Price_Ltr_KG,TSPL_PRIMARY_VEHICLE_MASTER_ESD.Rate_Type,TSPL_PRIMARY_VEHICLE_MASTER_ESD.Rental_Type,TSPL_PRIMARY_VEHICLE_MASTER_ESD.Rental_Amount,Is_Additional,TSPL_PRIMARY_VEHICLE_MASTER_ESD.Two_Way " + Environment.NewLine +
                                    " from TSPL_PRIMARY_VEHICLE_MASTER_ESD " + Environment.NewLine +
                                    " left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_PRIMARY_VEHICLE_MASTER_ESD.Vendor_Code " + Environment.NewLine +
                                    " where TSPL_PRIMARY_VEHICLE_MASTER_ESD.Vehicle_Code='" + obj.Vehicle_Code + "' and TSPL_PRIMARY_VEHICLE_MASTER_ESD.Hist_Version='" + clsCommon.myCstr(clsCommon.myCdbl(dt.Rows(0)("Hist_Version"))) + "'"
                                flag = False
                            End If
                        End If
                    End If
                    If flag Then
                        qry = "select  TSPL_Primary_Vehicle_Master.Status,TSPL_Primary_Vehicle_Master.Shift_Charges,TSPL_Primary_Vehicle_Master.Avg_Km_Ltr,TSPL_Primary_Vehicle_Master.Diesel_Rate,TSPL_Primary_Vehicle_Master.Price_KM, TSPL_Primary_Vehicle_Master.Price_Ltr_KG,TSPL_Primary_Vehicle_Master.Rate_Type,TSPL_Primary_Vehicle_Master.Rental_Type,TSPL_Primary_Vehicle_Master.Rental_Amount,Is_Additional,TSPL_Primary_Vehicle_Master.Two_Way " + Environment.NewLine +
                        " from TSPL_Primary_Vehicle_Master " + Environment.NewLine +
                        " left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_Primary_Vehicle_Master.Vendor_Code " + Environment.NewLine +
                        " where Vehicle_Code='" + obj.Vehicle_Code + "'"
                    End If

                    dt = clsDBFuncationality.GetDataTable(qry, trans)
                    obj.Status = clsCommon.myCstr(dt.Rows(0)("Status"))

                    If clsCommon.CompairString(obj.Status, "Day/Diesel") = CompairStringResult.Equal Then
                        obj.Amount = Math.Round(clsCommon.myCdbl(dt.Rows(0)("Shift_Charges")) + ((obj.Actual_KM * clsCommon.myCdbl(dt.Rows(0)("Diesel_Rate"))) / clsCommon.myCdbl(dt.Rows(0)("Avg_Km_Ltr"))), 2, MidpointRounding.ToEven)
                    ElseIf clsCommon.CompairString(obj.Status, "Rate/K.M") = CompairStringResult.Equal Then
                        obj.Amount = Math.Round(obj.Actual_KM * clsCommon.myCdbl(dt.Rows(0)("Price_KM")), 2, MidpointRounding.ToEven)
                    ElseIf clsCommon.CompairString(obj.Status, "Rate/Ltr") = CompairStringResult.Equal Then
                        '''''BBBBBBBBBBBBB
                        If clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0)("Rate_Type")), "LTR") = CompairStringResult.Equal Then
                            Dim convFactor As Double = clsWeightConversionInfo.GetWeightConverionFactorMilkType(objCommonVar.DefaultMilkItemCode, "KG", "LTR", trans)
                            obj.Amount = Math.Round((IIf(SettingStdQty, obj.Std_Qty, obj.Actual_Weight) * clsCommon.myCdbl(dt.Rows(0)("Price_Ltr_KG")) * convFactor), 2, MidpointRounding.ToEven)
                        ElseIf clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0)("Rate_Type")), "KG") = CompairStringResult.Equal Then
                            obj.Amount = Math.Round(IIf(SettingStdQty, obj.Std_Qty, obj.Actual_Weight) * clsCommon.myCdbl(dt.Rows(0)("Price_Ltr_KG")), 2, MidpointRounding.ToEven)
                        Else
                            Throw New Exception("Wrong Rate Type of " + obj.Status + " and vehicle no " + obj.Vehicle_Code)
                        End If
                    ElseIf clsCommon.CompairString(obj.Status, "Rental") = CompairStringResult.Equal Then
                        Dim Days As Integer = 0
                        If clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0)("Rental_Type")), "Year") = CompairStringResult.Equal Then
                            Days = IIf(DateTime.IsLeapYear(obj.DOC_DATE.Year), 366, 365)
                        ElseIf clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0)("Rental_Type")), "Month") = CompairStringResult.Equal Then
                            Days = System.DateTime.DaysInMonth(obj.DOC_DATE.Year, obj.DOC_DATE.Month)
                        ElseIf clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0)("Rental_Type")), "Day") = CompairStringResult.Equal Then
                            Days = 1
                        Else
                            Throw New Exception("Wrong Rental Type of " + obj.Status + " and vehicle no " + obj.Vehicle_Code)
                        End If
                        obj.FixedCharge = clsCommon.myCdbl(dt.Rows(0)("Rental_Amount"))
                        obj.FixedAmount = Math.Round(clsCommon.myCdbl(dt.Rows(0)("Rental_Amount") / (Days * 2)), 2, MidpointRounding.ToEven)
                        obj.Amount = obj.FixedAmount
                    ElseIf clsCommon.CompairString(obj.Status, "Rental/Diesel") = CompairStringResult.Equal Then
                        obj.FixedCharge = clsCommon.myCdbl(dt.Rows(0)("Rental_Amount"))
                        obj.FixedAmount = Math.Round(clsCommon.myCdbl(dt.Rows(0)("Rental_Amount") / (System.DateTime.DaysInMonth(obj.DOC_DATE.Year, obj.DOC_DATE.Month) * 2)), 2, MidpointRounding.ToEven)
                        obj.Amount = obj.FixedAmount
                        obj.Amount += Math.Round(((obj.Actual_KM * clsCommon.myCdbl(dt.Rows(0)("Diesel_Rate"))) / clsCommon.myCdbl(dt.Rows(0)("Avg_Km_Ltr"))), 2, MidpointRounding.ToEven)
                    ElseIf clsCommon.CompairString(obj.Status, "KM_Range") = CompairStringResult.Equal Then
                        'Today do work
                        obj.Amount = 0
                        Dim dblRemainingKM As Double = obj.Actual_KM
                        Dim dtSlab As DataTable = clsDBFuncationality.GetDataTable("select Slab_Upto,Slab_Rate from tspl_slab_range_detail where Trans_ID='" + obj.Vehicle_Code + "' and Form_ID='PTV-MST' order by Slab_Upto desc", trans)
                        If dtSlab IsNot Nothing AndAlso dtSlab.Rows.Count > 0 Then
                            If dtSlab.Rows.Count = 1 Then
                                obj.Amount = Math.Round((clsCommon.myCdbl(dtSlab.Rows(0)("Slab_Rate")) * (obj.Actual_KM)), 2, MidpointRounding.ToEven)
                            ElseIf clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0)("Is_Additional")), "T") = CompairStringResult.Equal Then
                                For ii As Integer = 0 To dtSlab.Rows.Count - 1
                                    Dim previousRange As Double = 0
                                    If (dtSlab.Rows.Count - (ii + 1)) > 0 Then
                                        previousRange = clsCommon.myCdbl(dtSlab.Rows(ii + 1)("Slab_Upto"))
                                    End If
                                    If dblRemainingKM >= clsCommon.myCdbl(dtSlab.Rows(ii)("Slab_Upto")) Then
                                        obj.Amount += (clsCommon.myCdbl(dtSlab.Rows(ii)("Slab_Rate")) * (dblRemainingKM))
                                        Exit For
                                    ElseIf dblRemainingKM > previousRange AndAlso dblRemainingKM <= clsCommon.myCdbl(dtSlab.Rows(ii)("Slab_Upto")) Then
                                        obj.Amount += (clsCommon.myCdbl(dtSlab.Rows(ii)("Slab_Rate")) * (dblRemainingKM - previousRange))
                                        dblRemainingKM = previousRange
                                    End If
                                Next
                                obj.Amount = Math.Round(obj.Amount, 2, MidpointRounding.ToEven)
                            Else
                                For Each drSlab As DataRow In dtSlab.Rows
                                    If obj.Actual_KM >= clsCommon.myCdbl(drSlab("Slab_Upto")) Then
                                        obj.Amount = Math.Round((clsCommon.myCdbl(drSlab("Slab_Rate")) * (obj.Actual_KM)), 2, MidpointRounding.ToEven)
                                        Exit For
                                    End If
                                Next
                            End If
                        End If
                    Else
                        Throw New Exception("Wrong method " + obj.Status + " for vehicle no " + obj.Vehicle_Code)
                    End If
                    If clsCommon.myCdbl(dt.Rows(0)("Two_Way")) = 1 Then
                        obj.Amount = obj.Amount * 2
                    End If

                    qry = "Select sum(Penalty_Amount) as Penalty_Amount from ( " + clsMilkGateEntryIn.GetQeryForPenalty(Mcc_code, objShiftHead.SHIFT, objShiftHead.DOC_DATE, obj.Route_CODE) + " and TSPL_MILK_GATE_ENTRY_IN.Penalty_Status=1 )xx "
                    Dim dtPenalty As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                    Dim dclPenalty As Decimal = 0
                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                        dclPenalty = clsCommon.myCdbl(dtPenalty.Rows(0)("Penalty_Amount"))
                    End If

                    obj.Amount = Math.Round(obj.Amount - obj.deduction_of_Transporter - dclPenalty, 2, MidpointRounding.ToEven)
                    obj.Rate_KM = IIf(obj.Actual_KM = 0, 0, Math.Round(obj.Amount / obj.Actual_KM, 3, MidpointRounding.AwayFromZero))
                    clsCommon.AddColumnsForChange(coll, "Status", obj.Status)
                    clsCommon.AddColumnsForChange(coll, "Rate", obj.Rate_KM)
                    clsCommon.AddColumnsForChange(coll, "Amount", obj.Amount)
                    clsCommon.AddColumnsForChange(coll, "Actual_KM", obj.Actual_KM)
                End If
                'Throw New Exception("Method-" + obj.Status + " Amount- " + clsCommon.myCstr(obj.Amount) + " Rate- " + clsCommon.myCstr(obj.Rate_KM) + " Actual KM-" + obj.Actual_KM)
                If isNewentry Then
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MILK_Shift_End_Route_DETAIL", OMInsertOrUpdate.Insert, "TSPL_MILK_Shift_End_Route_DETAIL.DOC_CODE='" + strDocNo + "'", trans)
                Else
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MILK_Shift_End_Route_DETAIL", OMInsertOrUpdate.Update, "TSPL_MILK_Shift_End_Route_DETAIL.DOC_CODE='" + strDocNo + "' and TSPL_MILK_Shift_End_Route_DETAIL.Route_CODE='" & obj.Route_CODE & "'", trans)
                End If
                If obj.Amount > 0 Then
                    ConvertProvision(obj, Mcc_code, trans)
                End If
            Next
        End If
        Return True
    End Function

    Public Shared Function ConvertProvision(ByVal objM As clsMilkShiftEndMCC_Route_Detail, ByVal Mcc_code As String, ByVal trans As SqlTransaction) As Boolean
        Dim dtmcc As DataTable = clsDBFuncationality.GetDataTable("select TSPL_VENDOR_MASTER.Vendor_Code,Vendor_name,Mcc_name  from TSPL_MCC_ROUTE_MASTER " _
                                 & " inner join TSPL_Primary_Vehicle_Master ptv on ptv.Vehicle_Code=TSPL_MCC_ROUTE_MASTER.Vehicle_Code inner join TSPL_VENDOR_MASTER " _
                                 & " on TSPL_VENDOR_MASTER.Vendor_Code=ptv.Vendor_Code inner join tspl_mcc_master on tspl_mcc_master.MCC_Code=TSPL_MCC_ROUTE_MASTER.MCC_Code " _
                                 & " where Route_Code='" & objM.Route_CODE & "'", trans)
        If dtmcc.Rows.Count > 0 Then
            Dim obj As New clsProvisionEntry()
            obj = New clsProvisionEntry()
            obj.Doc_Date = objM.DOC_DATE
            obj.Vendor_Code = dtmcc.Rows(0)("Vendor_Code")
            obj.Vendor_Desc = dtmcc.Rows(0)("Vendor_Name")
            obj.Vendor_Type = "Primary Transporter"
            obj.Status = "NO"
            obj.Loc_Code = Mcc_code
            obj.Loc_Desc = dtmcc.Rows(0)("MCC_Name")
            obj.Route_Code = objM.Route_CODE
            obj.Ref_Doc_No = objM.DOC_CODE
            obj.Prov_type = "Freight"
            obj.Amount = objM.Amount


            obj.Prog_Code = clsUserMgtCode.frmMilkShiftEndMCC
            obj.Prov_Month = Month(objM.DOC_DATE)
            obj.Prov_Year = Year(objM.DOC_DATE)
            obj.Modified_Date = clsCommon.GETSERVERDATE(trans)
            obj.isNewEntry = True

            obj.FixedCharge = objM.FixedCharge
            obj.FixedAmount = objM.FixedAmount

            clsProvisionEntry.SaveData(obj, trans)
            clsProvisionEntry.PostData(obj.Doc_No, trans, False)
        End If
        Return True
    End Function

    Public Shared Function GetData(ByVal strDocNo As String, ByVal strPKID As String, ByVal trans As SqlTransaction) As clsMilkShiftEndMCC_Route_Detail
        Dim objtr_Route As clsMilkShiftEndMCC_Route_Detail = Nothing
        Dim qry As String = " SELECT DOC_CODE,erd.Route_CODE,Route_name,Opening_KM,Closing_KM,Total_KM,Actual_KM,Actual_Payable_KM,Diff_KM,deduction_of_Transporter,Milk_Weight,Actual_Weight,erd.vehicle_code,Shift_Charge,Rate,Amount,DOC_DATE,Reason,FileName,TSPL_ATTACHMENTS.code as Attachment_Id,TSPL_Primary_Vehicle_Master.Description as  vehicle_name,Deduction_Reason,coalesce(is_Head_load,0) as is_Head_load,charge_Amount,Head_Load_Amount,Own_Asset_Amount,erd.Std_Qty FROM TSPL_MILK_Shift_End_Route_DETAIL erd " _
        & " LEFT join tspl_mcc_route_master rm on rm.Route_Code=erd.Route_Code LEFT join TSPL_ATTACHMENTS on TransactionId=erd.Route_CODE and TSPL_ATTACHMENTS.code=Attachment_id and FormId='M-RECEIPT' left join TSPL_Primary_Vehicle_Master on TSPL_Primary_Vehicle_Master.Vehicle_Code=rm.Vehicle_Code  WHERE 2=2 AND erd.DOC_CODE = '" & strDocNo & "'  and  erd.PK_Id='" + strPKID + "' " _
        & " ORDER BY erd.Route_CODE"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            For Each dr As DataRow In dt.Rows
                objtr_Route = New clsMilkShiftEndMCC_Route_Detail
                objtr_Route.DOC_CODE = strDocNo
                objtr_Route.Route_CODE = clsCommon.myCstr(dr("Route_CODE"))
                objtr_Route.Route_name = clsCommon.myCstr(dr("Route_Name"))
                objtr_Route.Opening_KM = clsCommon.myCdbl(dr("Opening_KM"))
                objtr_Route.Closing_KM = clsCommon.myCdbl(dr("Closing_KM"))
                objtr_Route.Total_KM = clsCommon.myCdbl(dr("Total_KM"))
                objtr_Route.Actual_KM = clsCommon.myCdbl(dr("Actual_KM"))
                objtr_Route.Actual_Payable_KM = clsCommon.myCdbl(dr("Actual_Payable_KM"))
                objtr_Route.Diff_Km = clsCommon.myCdbl(dr("Diff_Km"))
                objtr_Route.deduction_of_Transporter = clsCommon.myCdbl(dr("deduction_of_Transporter"))
                objtr_Route.Milk_Weight = clsCommon.myCdbl(dr("Milk_weight"))
                objtr_Route.Actual_Weight = clsCommon.myCdbl(dr("Actual_weight"))

                objtr_Route.Vehicle_Code = clsCommon.myCstr(dr("Vehicle_code"))
                objtr_Route.Vehicle_Name = clsCommon.myCstr(dr("Vehicle_name"))
                objtr_Route.Rate_KM = clsCommon.myCdbl(dr("Rate"))
                objtr_Route.Shift_Charge = clsCommon.myCdbl(dr("Shift_Charge"))
                objtr_Route.Amount = clsCommon.myCdbl(dr("Amount"))
                objtr_Route.Std_Qty = clsCommon.myCdbl(dr("Std_Qty"))
                objtr_Route.Attachment_Id = clsCommon.myCdbl(dr("Attachment_id"))
                objtr_Route.Truck_Sheet_of_TransPorter = clsCommon.myCstr(dr("FileName"))
                objtr_Route.DOC_DATE = clsCommon.myCDate(dr("DOC_DATE"))
                objtr_Route.Reason = clsCommon.myCstr(dr("Reason"))
                objtr_Route.Deduction_Reason = clsCommon.myCstr(dr("Deduction_Reason"))
                objtr_Route.Charge_Amt = clsCommon.myCstr(dr("Charge_Amount"))
                objtr_Route.Head_Load_Amt = clsCommon.myCstr(dr("Head_Load_Amount"))
                objtr_Route.Own_Asset_Amount = clsCommon.myCstr(dr("Own_Asset_Amount"))
                objtr_Route.Is_Head_Load = clsCommon.myCstr(dr("Is_Head_Load"))

            Next
        End If
        Return objtr_Route
    End Function

End Class



Public Class clsMilkShiftEndAttachment
    Public Form_ID As String = ""
    Public Transaction_ID As String = ""
    Dim isInsideLoadData As Boolean = False

    ''
    Public ColCODE As String = ""
    Public ColFormId As String = ""
    Public ColTransactionId As String = ""
    Public ColSNo As String = ""
    Public ColFileName As String = ""
    Public ColCOMMENTS As String = ""
    Public ColPath As String = ""
    Public ColView As String = ""
    Public ColDelete As String = ""
    Public ColSelect As String = ""


    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsMilkShiftEndAttachment), ByVal trans As SqlTransaction) As Boolean
        Try
            If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
                Dim obj As clsAttachDocument
                For Each obj1 As clsMilkShiftEndAttachment In Arr
                    obj = New clsAttachDocument
                    obj.CODE = clsCommon.myCstr(obj1.ColCODE)
                    obj.FormId = obj1.Form_ID
                    obj.TransactionId = IIf(obj1.Transaction_ID = "", strDocNo, obj1.Transaction_ID)
                    obj.SNo = 1 'clsCommon.myCstr(obj1.ColSNo)
                    obj.FileName = clsCommon.myCstr(obj1.ColFileName)
                    obj.COMMENTS = clsCommon.myCstr(obj1.ColCOMMENTS)
                    obj.SaveData(obj)

                    Dim str As String
                    If clsCommon.myLen(obj1.ColPath) > 0 Then
                        Dim bData As Byte()
                        Dim br As BinaryReader = New BinaryReader(System.IO.File.OpenRead(clsCommon.myCstr(obj1.ColPath)))
                        bData = br.ReadBytes(br.BaseStream.Length)

                        str = " UPDATE TSPL_ATTACHMENTS set FileData = @BLOBData where CODE='" + obj.CODE + "'"
                        Dim cmd As SqlCommand = New SqlCommand(str, clsDBFuncationality.GetConnnection)
                        Dim prm As New SqlParameter("@BLOBData", bData)
                        cmd.Parameters.Add(prm)
                        cmd.ExecuteNonQuery()
                        br.Close() ' done by stuti reagrding memory leakage
                    End If
                    If clsCommon.CompairString(LCase(obj.COMMENTS), "for route") = CompairStringResult.Equal Then
                        str = "update tspl_milk_shift_End_Route_Detail set Attachment_Id='" & obj.CODE & "' where Route_Code='" & obj.TransactionId & "' and doc_Code='" & strDocNo & "'"
                        clsDBFuncationality.ExecuteNonQuery(str)
                    Else
                        str = "update tspl_milk_shift_End_Detail set Attachment_Id='" & obj.CODE & "' where vlc_doc_Code='" & obj.TransactionId & "' and doc_Code='" & strDocNo & "'"
                        clsDBFuncationality.ExecuteNonQuery(str)
                    End If
                Next
            End If
            Return True
            'LoadData(Transaction_ID)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
            Return False
        End Try

    End Function

End Class