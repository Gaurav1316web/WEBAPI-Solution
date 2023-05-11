Imports common
Imports System.Data
Imports System.Data.SqlClient
Public Class clsMilkReceiptMCC

#Region "Variables"
    Public DOC_CODE As String
    Public MCC_CODE As String
    Public DOC_DATE As DateTime
    Public SHIFT As String
    Public COMM_PORT As String
    Public MACHINE_NO As String
    Public TOTAL_WEIGHT As Decimal
    Public FAT As Decimal = 0
    Public SNF As Decimal = 0
    Public Dock_Collection_Milk_Type As String
    Public IS_SAMPLED As String
    Public MCC_NAME As String
    Public CREATED_BY As String
    Public Posting_Date As Date
    Public Irregular_MCC_CODE As String
    Public POSTED As ERPTransactionStatus = ERPTransactionStatus.Pending
    Public Shared ObjList As List(Of clsMilkReceiptMCCDetail)
    Public Shared ObjList_Route As ArrayList
    Public Form_ID As String = ""
    Public arrCustomFields As List(Of clsCustomFieldValues) = Nothing
    Public DOCK_Code As String
#End Region

    Public Shared Function SaveData(ByVal obj As clsMilkReceiptMCC, ByVal objList As List(Of clsMilkReceiptMCCDetail)) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            SaveData(obj, objList, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function SaveData(ByVal obj As clsMilkReceiptMCC, ByVal objList As List(Of clsMilkReceiptMCCDetail), ByVal trans As SqlTransaction) As Boolean
        Try
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleMCCMilkProcurement, clsUserMgtCode.frmMilkReceipt, obj.MCC_CODE, obj.DOC_DATE, trans)
            Dim isNewEntry As Boolean
            obj.DOC_CODE = GetDocCode(obj.DOC_DATE, obj.MCC_CODE, obj.SHIFT, obj.Dock_Collection_Milk_Type, obj.DOCK_Code, trans)
            If GetPostNew(obj.DOC_DATE, obj.MCC_CODE, obj.SHIFT, obj.DOCK_Code, trans) Then
                Throw New Exception("This Shift is posted. Check Code:" + obj.DOC_CODE + ".")
            End If
            If clsCommon.myLen(obj.DOC_CODE) <= 0 Then
                isNewEntry = True
                obj.DOC_CODE = clsERPFuncationality.GetNextCode(trans, clsCommon.GetPrintDate(obj.DOC_DATE, "dd/MMM/yyyy hh:mm:ss tt"), clsDocType.MilkReceipt, "", obj.MCC_CODE, False, True, False, False, objCommonVar.ShowMCCFinderInPaymentProcess)
                'aaaaa()
            Else
                isNewEntry = False
            End If
            If (clsCommon.myLen(obj.DOC_CODE) <= 0) Then
                Throw New Exception("Error in Document Code Generation")
            End If
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "DOC_CODE", obj.DOC_CODE)
            clsCommon.AddColumnsForChange(coll, "MCC_CODE", obj.MCC_CODE)
            clsCommon.AddColumnsForChange(coll, "Irregular_MCC_CODE", obj.Irregular_MCC_CODE, True)
            clsCommon.AddColumnsForChange(coll, "DOC_DATE", clsCommon.GetPrintDate(obj.DOC_DATE, "dd/MMM/yyyy hh:mm:ss tt"))
            clsCommon.AddColumnsForChange(coll, "SHIFT", obj.SHIFT)
            clsCommon.AddColumnsForChange(coll, "COMM_PORT", clsCommon.myCstr(obj.COMM_PORT))
            clsCommon.AddColumnsForChange(coll, "MACHINE_NO", clsCommon.myCstr(obj.MACHINE_NO))
            clsCommon.AddColumnsForChange(coll, "TOTAL_WEIGHT", clsCommon.myCstr(obj.TOTAL_WEIGHT))
            clsCommon.AddColumnsForChange(coll, "FAT", clsCommon.myCdbl(obj.FAT))
            clsCommon.AddColumnsForChange(coll, "SNF", clsCommon.myCdbl(obj.SNF))
            clsCommon.AddColumnsForChange(coll, "Dock_Collection_Milk_Type", obj.Dock_Collection_Milk_Type)
            clsCommon.AddColumnsForChange(coll, "POSTED", "0")
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))
            '' update Sync Satatus
            clsCommon.AddColumnsForChange(coll, "SYNC_STATUS", 0)
            clsCommon.AddColumnsForChange(coll, "DOCK_Code", obj.DOCK_Code, True)
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))
                Dim Strqry As String = "SELECT Count(*) FROM TSPL_MILK_RECEIPT_HEAD where DOC_CODE = '" & obj.DOC_CODE & "'"
                Dim check As Integer = clsDBFuncationality.getSingleValue(Strqry, trans)
                If check = 0 Then
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MILK_RECEIPT_HEAD", OMInsertOrUpdate.Insert, "", trans)
                Else
                    Throw New Exception("This Code:" + obj.DOC_CODE + " Is Already Exist")
                End If
            Else
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MILK_RECEIPT_HEAD", OMInsertOrUpdate.Update, "TSPL_MILK_RECEIPT_HEAD.DOC_CODE='" + obj.DOC_CODE + "'", trans)
            End If
            clsMilkReceiptMCCDetail.SaveData(obj.DOC_CODE, objList, obj.Dock_Collection_Milk_Type, obj.DOCK_Code, trans)
            clsCustomFieldValues.SaveData(obj.Form_ID, obj.DOC_CODE, obj.arrCustomFields, trans)
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, Optional ByVal isSampledata As Boolean = True) As clsMilkReceiptMCC
        Return GetData(strCode, NavType, Nothing, isSampledata, "")
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction, ByVal isLoadForSampleData As Boolean) As clsMilkReceiptMCC
        Return GetData(strCode, NavType, trans, isLoadForSampleData, "")
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction, ByVal isLoadForSampleData As Boolean, ByVal strWhrclas As String) As clsMilkReceiptMCC

        Dim obj As New clsMilkReceiptMCC()
        Dim objtr As New clsMilkReceiptMCCDetail

        ObjList = New List(Of clsMilkReceiptMCCDetail)
        ObjList_Route = New ArrayList
        If clsCommon.myLen(strWhrclas) > 0 Then
            strWhrclas = " and " + strWhrclas
        End If
        Dim qry As String = "SELECT TSPL_MILK_RECEIPT_HEAD.DOC_CODE,TSPL_MILK_RECEIPT_HEAD.MCC_CODE,TSPL_MILK_RECEIPT_HEAD.Irregular_MCC_CODE,TSPL_MILK_RECEIPT_HEAD.DOC_DATE,TSPL_MILK_RECEIPT_HEAD.SHIFT,TSPL_MILK_RECEIPT_HEAD.COMM_PORT,"
        qry += " TSPL_MILK_RECEIPT_HEAD.MACHINE_NO,TSPL_MILK_RECEIPT_HEAD.TOTAL_WEIGHT,TSPL_MCC_MASTER.MCC_NAME, "
        qry += " TSPL_MILK_RECEIPT_HEAD.Created_By,TSPL_MILK_RECEIPT_HEAD.Posted,TSPL_MILK_RECEIPT_HEAD.POSTED,TSPL_MILK_RECEIPT_HEAD.POSTING_DATE,FAT,SNF,TSPL_MILK_RECEIPT_HEAD.Dock_Collection_Milk_Type,TSPL_MILK_RECEIPT_HEAD.DOCK_Code FROM TSPL_MILK_RECEIPT_HEAD  INNER JOIN TSPL_MCC_MASTER   ON TSPL_MILK_RECEIPT_HEAD.MCC_CODE=TSPL_MCC_MASTER.MCC_CODE where 2=2 " + strWhrclas



        Select Case NavType
            Case NavigatorType.First
                qry += " AND DOC_CODE = (select MIN(DOC_CODE) from TSPL_MILK_RECEIPT_HEAD where 2=2 " + strWhrclas + ")"
            Case NavigatorType.Last
                qry += " AND DOC_CODE = (select Max(DOC_CODE) from TSPL_MILK_RECEIPT_HEAD where 2=2 " + strWhrclas + ")"
            Case NavigatorType.Next
                qry += " AND DOC_CODE = (select Min(DOC_CODE) from TSPL_MILK_RECEIPT_HEAD where  DOC_CODE>'" + strCode + "' " + strWhrclas + ")"
            Case NavigatorType.Previous
                qry += " AND DOC_CODE = (select Max(DOC_CODE) from TSPL_MILK_RECEIPT_HEAD where DOC_CODE<'" + strCode + "' " + strWhrclas + ")"
            Case NavigatorType.Current
                qry += " AND DOC_CODE = '" + strCode + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj.DOC_CODE = dt.Rows(0)("DOC_CODE")
            obj.MCC_CODE = clsCommon.myCstr(dt.Rows(0)("MCC_CODE"))
            obj.Irregular_MCC_CODE = clsCommon.myCstr(dt.Rows(0)("Irregular_MCC_CODE"))
            obj.DOC_DATE = clsCommon.GetPrintDate(dt.Rows(0)("DOC_DATE"), "dd/MMM/yyyy hh:mm:ss tt")
            obj.SHIFT = clsCommon.myCstr(dt.Rows(0)("SHIFT"))
            obj.COMM_PORT = clsCommon.myCstr(dt.Rows(0)("COMM_PORT"))
            obj.MACHINE_NO = clsCommon.myCstr(dt.Rows(0)("MACHINE_NO"))
            obj.TOTAL_WEIGHT = clsCommon.myCstr(dt.Rows(0)("TOTAL_WEIGHT"))
            obj.MCC_NAME = clsCommon.myCstr(dt.Rows(0)("MCC_NAME"))
            obj.FAT = clsCommon.myCdbl(dt.Rows(0)("FAT"))
            obj.SNF = clsCommon.myCdbl(dt.Rows(0)("SNF"))
            obj.Dock_Collection_Milk_Type = clsCommon.myCstr(dt.Rows(0)("Dock_Collection_Milk_Type"))
            obj.DOCK_Code = clsCommon.myCstr(dt.Rows(0)("DOCK_Code"))
            obj.CREATED_BY = clsCommon.myCstr(dt.Rows(0)("CREATED_BY"))
            obj.POSTED = IIf(clsCommon.myCdbl(dt.Rows(0)("Posted")) = 1, ERPTransactionStatus.Approved, ERPTransactionStatus.Pending)
            strCode = dt.Rows(0)("DOC_CODE")
            If clsCommon.myLen(dt.Rows(0)("Posting_Date")) > 0 Then
                obj.Posting_Date = clsCommon.myCDate(dt.Rows(0)("Posting_Date"))
            Else
                obj.Posting_Date = Nothing
            End If
        End If
        If isLoadForSampleData Then
            qry = "SELECT DOC_CODE,VLC_DOC_CODE,convert(integer,SAMPLE_NO) as SAMPLE_NO,TSPL_MILK_RECEIPT_DETAIL.VLC_CODE,TSPL_VLC_MASTER_HEAD.VLC_CODE_VLC_Uploader," _
                    & " TSPL_MILK_RECEIPT_DETAIL.ROUTE_CODE,TSPL_MILK_RECEIPT_DETAIL.VSP_CODE,Item_Code,VEHICLE_CODE,Other_Vehicle,Other_VLC,  NO_OF_CANS,MILK_WEIGHT,UOm_Code," _
                    & " TSPL_MILK_RECEIPT_DETAIL.Conversion_factor,ACC_Weight,ACC_Weight_LTR,TYPE,MILK_TYPE,FAT_per as Fat,SNF_Per as SNf,SAMPLE_NO_VALUES,MCC_CODE," _
                    & " DOC_DATE,SHIFT,COMM_PORT,MACHINE_NO,_sample.IS_MANUAL,Eco_pro_Name,_sample.rate,_sample.AMT,_sample.CLR as CLR FROM TSPL_MILK_RECEIPT_DETAIL  inner join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code" _
                    & " =TSPL_MILK_RECEIPT_DETAIL.VLC_CODE inner join (select MILK_RECEIPT_CODE,VLC_DOC_CODE as V_d_C,sample_no as s_no,Fat as FAT_Per,snf as SNF_Per,Posted as pstd,Rate,TSPL_MILK_SAMPLE_DETAIL.amount as AMT,TSPL_MILK_SAMPLE_DETAIL.CLR,TSPL_MILK_SAMPLE_DETAIL.IS_MANUAL from TSPL_MILK_Sample_HEAD " _
                    & " inner join TSPL_MILK_SAMPLE_DETAIL on TSPL_MILK_SAMPLE_DETAIL.DOC_CODE=TSPL_MILK_SAMPLE_HEAD.DOC_CODE) _sample on _sample.MILK_RECEIPT_CODE=" _
                    & " TSPL_MILK_RECEIPT_DETAIL.DOC_CODE and _sample.V_d_C=TSPL_MILK_RECEIPT_DETAIL.VLC_DOC_CODE and _sample.s_no=TSPL_MILK_RECEIPT_DETAIL.sample_No  WHERE 2=2 AND TSPL_MILK_RECEIPT_DETAIL.DOC_CODE " _
                    & " = '" + strCode + "' and coalesce(is_Sampleed,'F')='T' and coalesce(_sample.Pstd,0)=0 " _
                    & " Union " _
                    & " SELECT DOC_CODE,VLC_DOC_CODE,convert(integer,SAMPLE_NO) as SAMPLE_NO,TSPL_MILK_RECEIPT_DETAIL.VLC_CODE,TSPL_VLC_MASTER_HEAD.VLC_CODE_VLC_Uploader,TSPL_MILK_RECEIPT_DETAIL.ROUTE_CODE,TSPL_MILK_RECEIPT_DETAIL.VSP_CODE,Item_Code,VEHICLE_CODE,Other_Vehicle,Other_VLC, " _
                    & " NO_OF_CANS,MILK_WEIGHT,UOm_Code,TSPL_MILK_RECEIPT_DETAIL.Conversion_factor,ACC_Weight,ACC_Weight_LTR,TYPE,MILK_TYPE,FAT,SNF,SAMPLE_NO_VALUES,MCC_CODE,DOC_DATE,SHIFT,COMM_PORT,MACHINE_NO,'' as IS_MANUAL,Eco_pro_Name,0,0,0 as CLR FROM TSPL_MILK_RECEIPT_DETAIL  inner join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code=TSPL_MILK_RECEIPT_DETAIL.VLC_CODE   WHERE 2=2" _
                    & " AND TSPL_MILK_RECEIPT_DETAIL.DOC_CODE = '" + strCode + "' and coalesce(is_Sampleed,'F')='" & IIf(isLoadForSampleData, "F", "T") & "'" _
                    & "  ORDER BY [SAMPLE_NO]"
        Else
            qry = " SELECT DOC_CODE,VLC_DOC_CODE,convert(integer,SAMPLE_NO) as SAMPLE_NO,TSPL_MILK_RECEIPT_DETAIL.VLC_CODE,TSPL_VLC_MASTER_HEAD.VLC_CODE_VLC_Uploader,TSPL_MILK_RECEIPT_DETAIL.ROUTE_CODE,TSPL_MILK_RECEIPT_DETAIL.VSP_CODE,Item_Code,VEHICLE_CODE,Other_Vehicle,Other_VLC, " _
                   & " NO_OF_CANS,MILK_WEIGHT,UOm_Code,TSPL_MILK_RECEIPT_DETAIL.Conversion_factor,ACC_Weight,ACC_Weight_LTR,TYPE,MILK_TYPE,FAT,SNF,SAMPLE_NO_VALUES,MCC_CODE,DOC_DATE,SHIFT,COMM_PORT,MACHINE_NO,IS_MANUAL,Eco_pro_Name,0 as Rate,0 as AMT,0 as CLR FROM TSPL_MILK_RECEIPT_DETAIL  inner join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code=TSPL_MILK_RECEIPT_DETAIL.VLC_CODE   WHERE 2=2" _
                   & " AND TSPL_MILK_RECEIPT_DETAIL.DOC_CODE = '" + strCode + "' and coalesce(is_Sampleed,'F')='" & IIf(isLoadForSampleData, "F", "T") & "'" _
                   & " " & IIf(isLoadForSampleData, " and TSPL_MILK_RECEIPT_DETAIL.created_by='" & objCommonVar.CurrentUserCode & "'", "") & " ORDER BY [SAMPLE_NO]"
        End If

        dt = New DataTable()
        dt = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            For Each dr As DataRow In dt.Rows
                objtr = New clsMilkReceiptMCCDetail
                objtr.DOC_CODE = strCode
                objtr.VLC_DOC_CODE = clsCommon.myCstr(dr("VLC_DOC_CODE"))
                objtr.VLC_CODE = clsCommon.myCstr(dr("VLC_CODE"))
                objtr.VLC_CODE_Uploader_Code = clsCommon.myCstr(dr("VLC_CODE_VLC_Uploader"))
                objtr.ROUTE_CODE = clsCommon.myCstr(dr("ROUTE_CODE"))
                objtr.NO_OF_CANS = clsCommon.myCdbl(dr("NO_OF_CANS"))
                objtr.VSP_CODE = clsCommon.myCstr(dr("VSP_CODE"))
                objtr.Item_CODE = clsCommon.myCstr(dr("Item_CODE"))
                objtr.MILK_WEIGHT = clsCommon.myCdbl(dr("MILK_WEIGHT"))
                objtr.ACC_WEIGHT = clsCommon.myCdbl(dr("ACC_WEIGHT"))
                objtr.LTR_WEIGHT = clsCommon.myCdbl(dr("ACC_WEIGHT_LTR"))
                objtr.SAMPLE_NO = clsCommon.myCdbl(dr("SAMPLE_NO"))
                objtr.VEHICLE_CODE = clsCommon.myCstr(dr("VEHICLE_CODE"))
                objtr.Other_VEHICLE = clsCommon.myCstr(dr("Other_VEHICLE"))
                objtr.Other_VLC = clsCommon.myCstr(dr("Other_VLC"))
                objtr.TYPE = clsCommon.myCstr(dr("TYPE"))
                objtr.MILK_TYPE = clsCommon.myCstr(dr("MILK_TYPE"))
                objtr.FAT = clsCommon.myCdbl(dr("FAT"))
                objtr.SNF = clsCommon.myCdbl(dr("SNF"))
                objtr.CLR = clsCommon.myCdbl(dr("CLR"))
                objtr.RATE = clsCommon.myCdbl(dr("Rate"))
                objtr.Amount = clsCommon.myCdbl(dr("AMT"))
                objtr.SAMPLE_NO_VALUES = clsCommon.myCstr(dr("SAMPLE_NO_VALUES"))
                objtr.MCC_CODE = clsCommon.myCstr(dr("MCC_CODE"))

                objtr.DOC_DATE = clsCommon.myCDate(dr("DOC_DATE"))
                objtr.SHIFT = clsCommon.myCstr(dr("SHIFT"))
                objtr.COMM_PORT = clsCommon.myCstr(dr("COMM_PORT"))
                objtr.MACHINE_NO = clsCommon.myCstr(dr("MACHINE_NO"))
                objtr.IS_ENTERED_MANUAL = clsCommon.myCstr(dr("IS_MANUAL"))
                objtr.UOM_Code = clsCommon.myCstr(dr("UOM_COde"))
                objtr.Conversion_Factor = clsCommon.myCdbl(dr("Conversion_factor"))
                objtr.Eco_Pro_Name = clsCommon.myCstr(dr("Eco_pro_Name"))
                ObjList.Add(objtr)
                If Not ObjList_Route.Contains(objtr.ROUTE_CODE) Then
                    ObjList_Route.Add(objtr.ROUTE_CODE)
                End If
            Next
        End If

        clsMilkReceiptMCC.ObjList = ObjList
        Return obj
    End Function

    Public Function Gettable(ByVal strCode As String, ByVal NavType As NavigatorType, Optional ByVal trans As SqlTransaction = Nothing) As DataTable
        ',Case when IS_MANUAL='N' then 'Auto' else 'Manual' end as [Entry Type]//Commented by Rohit as Per Meeting with rakesh Sir it will not visible to Client.
        Dim qry As String = " SELECT DOC_CODE as [DOC CODE],VLC_DOC_CODE as [VLC DOC CODE],SAMPLE_NO as [SAMPLE NO],TSPL_MILK_RECEIPT_DETAIL.VLC_CODE as [VLC CODE],TSPL_Vlc_Master_Head.VLC_NAME as [VLC NAME],TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader as [VLC UPLOADER CODE],Item_Desc  as [ITEM NAME],TSPL_MILK_RECEIPT_DETAIL.ROUTE_CODE as [ROUTE CODE],TSPL_Mcc_route_master.route_name as [ROUTE NAME],TSPL_MILK_RECEIPT_DETAIL.VSP_CODE as [VSP CODE],vendor_name as [VSP Name],TSPL_MILK_RECEIPT_DETAIL.VEHICLE_CODE as [VEHICLE CODE],case when other_Vehicle>'T' then 'Yes' end as [Other Vehicle] , "
        qry += " NO_OF_CANS as [NO OF CANS],MILK_WEIGHT as [MILK WEIGHT],TSPL_MILK_RECEIPT_DETAIL.UOM_Code as [Unit Code],round(Conversion_factor,2) as [Conversion Ratio],CASE WHEN COALESCE(IS_SAMPLEED,'F')='T' and ACC_WEIGHT<=0 THEN convert(decimal(18,2),MILK_WEIGHT) when  COALESCE(IS_SAMPLEED,'F')='T' and ACC_WEIGHT>0 then convert(decimal(18,2),Acc_Weight) ELSE convert(decimal(18,2),Conversion_factor*MILK_WEIGHT) END as [Actual Qty(KG)]" _
        & " ,TYPE,MILK_TYPE as [MILK TYPE],SAMPLE_NO_VALUES as [SAMPLE NO VALUES],MC.MCC_CODE as [MCC CODE],DOC_DATE as [DOC DATE],Case when SHIFT='M' then 'Morning' else 'Evening' end as Shift,COMM_PORT as [COM PORT]" _
        & " ,MACHINE_NO as [MACHINE NO],Entry_Date_Time as [Entry At],TSPL_VLC_MASTER_HEAD.Village_Code as VillageCode,TSPL_VILLAGE_MASTER.Village_Name as VillageName "
        If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ShowVehicleNoSeparatelyInPrimaryTransVehicleMaster, clsFixedParameterCode.ShowVehicleNoSeparatelyInPrimaryTransVehicleMaster, Nothing)) = 1 Then
            qry += " ,TSPL_Primary_Vehicle_Master.Vehicle"
        End If
        qry += " FROM TSPL_MILK_RECEIPT_DETAIL inner join tspl_mcc_master mc on mc.mcc_code=TSPL_MILK_RECEIPT_DETAIL.mcc_code inner join tspl_item_master on tspl_item_master.item_Code=TSPL_MILK_RECEIPT_DETAIL.item_Code " _
        & " Inner join TSPL_Unit_Master on tspl_Unit_master.Unit_Code=TSPL_MILK_RECEIPT_DETAIL.UOM_Code  inner join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code=TSPL_MILK_RECEIPT_DETAIL.VLC_CODE inner join TSPL_Mcc_route_master on TSPL_Mcc_route_master.route_Code=TSPL_MILK_RECEIPT_DETAIL.route_CODE  left join TSPL_VENDOR_MASTER on TSPL_MILK_RECEIPT_DETAIL.VSP_Code=Vendor_Code left outer join TSPL_VILLAGE_MASTER on TSPL_VILLAGE_MASTER.Village_Code=TSPL_VLC_MASTER_HEAD.Village_Code left join TSPL_Primary_Vehicle_Master on TSPL_Primary_Vehicle_Master.Vehicle_Code=TSPL_MILK_RECEIPT_DETAIL.Vehicle_Code WHERE 2=2 " _
        & " AND TSPL_MILK_RECEIPT_DETAIL.DOC_CODE = '" + strCode + "'  ORDER BY TSPL_MILK_RECEIPT_DETAIL.Sample_No" 'and TSPL_MILK_RECEIPT_DETAIL.created_by='" & objCommonVar.CurrentUserCode & "'

        Dim dt As DataTable = New DataTable()
        dt = clsDBFuncationality.GetDataTable(qry, trans)
        Return dt
    End Function

    Public Shared Function LoadDataFromDetails(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsMilkReceiptMCC
        Dim obj As New clsMilkReceiptMCC()
        Dim objtr As New clsMilkReceiptMCCDetail

        ObjList = New List(Of clsMilkReceiptMCCDetail)
        Dim qry As String = " SELECT DOC_CODE,VLC_DOC_CODE,SAMPLE_NO,TSPL_MILK_RECEIPT_DETAIL.VLC_CODE,TSPL_MILK_RECEIPT_DETAIL.ROUTE_CODE,TSPL_MILK_RECEIPT_DETAIL.VSP_CODE,Item_Code,VEHICLE_CODE,Other_vehicle,Other_VLC "
        qry += ", NO_OF_CANS,MILK_WEIGHT,Uom_Code,ACC_WEIGHT,ACC_WEIGHT_LTR,TYPE,MILK_TYPE,FAT,SNF,SAMPLE_NO_VALUES,MCC_CODE,DOC_DATE,SHIFT,COMM_PORT,MACHINE_NO,is_Manual,case when is_sampleed='T' then 1 else 0 end is_sampleed,Eco_pro_Name,VLC_Code_VLC_Uploader,TSPL_MILK_RECEIPT_DETAIL.Can_Code,TSPL_MILK_RECEIPT_DETAIL.Gross_Weight,TSPL_MILK_RECEIPT_DETAIL.Tare_Weight,TSPL_CAN_MASTER.Tare_Weight as OneCanTareWeight FROM TSPL_MILK_RECEIPT_DETAIL   inner join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code=TSPL_MILK_RECEIPT_DETAIL.VLC_CODE left outer join TSPL_CAN_MASTER on TSPL_CAN_MASTER.Code=TSPL_MILK_RECEIPT_DETAIL.Can_Code WHERE 2=2"
        qry += " AND TSPL_MILK_RECEIPT_DETAIL.VLC_DOC_CODE = '" + strCode + "' ORDER BY TSPL_MILK_RECEIPT_DETAIL.VLC_DOC_CODE"

        Dim dt As DataTable = New DataTable()
        dt = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            For Each dr As DataRow In dt.Rows
                objtr = New clsMilkReceiptMCCDetail

                objtr.DOC_CODE = strCode
                objtr.VLC_DOC_CODE = clsCommon.myCstr(dr("VLC_DOC_CODE"))
                objtr.VLC_CODE = clsCommon.myCstr(dr("VLC_CODE"))
                objtr.VLC_CODE_Uploader_Code = clsCommon.myCstr(dr("VLC_Code_VLC_Uploader"))
                objtr.ROUTE_CODE = clsCommon.myCstr(dr("ROUTE_CODE"))
                objtr.NO_OF_CANS = clsCommon.myCdbl(dr("NO_OF_CANS"))
                objtr.VSP_CODE = clsCommon.myCstr(dr("VSP_CODE"))
                objtr.Item_CODE = clsCommon.myCstr(dr("Item_CODE"))
                objtr.MILK_WEIGHT = clsCommon.myCdbl(dr("MILK_WEIGHT"))
                objtr.ACC_WEIGHT = clsCommon.myCdbl(dr("ACC_WEIGHT"))
                objtr.LTR_WEIGHT = clsCommon.myCdbl(dr("ACC_WEIGHT_Ltr"))
                objtr.SAMPLE_NO = clsCommon.myCdbl(dr("SAMPLE_NO"))
                objtr.VEHICLE_CODE = clsCommon.myCstr(dr("VEHICLE_CODE"))
                objtr.TYPE = clsCommon.myCstr(dr("TYPE"))
                objtr.MILK_TYPE = clsCommon.myCstr(dr("MILK_TYPE"))
                objtr.FAT = clsCommon.myCdbl(dr("FAT"))
                objtr.SNF = clsCommon.myCdbl(dr("SNF"))
                objtr.SAMPLE_NO_VALUES = clsCommon.myCstr(dr("SAMPLE_NO_VALUES"))
                objtr.MCC_CODE = clsCommon.myCstr(dr("MCC_CODE"))

                objtr.DOC_DATE = clsCommon.myCDate(dr("DOC_DATE"))
                objtr.SHIFT = clsCommon.myCstr(dr("SHIFT"))
                objtr.COMM_PORT = clsCommon.myCstr(dr("COMM_PORT"))
                objtr.MACHINE_NO = clsCommon.myCstr(dr("MACHINE_NO"))
                objtr.IS_ENTERED_MANUAL = clsCommon.myCstr(dr("IS_MANUAL"))
                objtr.UOM_Code = clsCommon.myCstr(dr("Uom_Code"))
                objtr.IS_Sampled = clsCommon.myCstr(dr("IS_sampleed"))
                objtr.Eco_Pro_Name = clsCommon.myCstr(dr("Eco_pro_Name"))
                objtr.Other_VEHICLE = clsCommon.myCstr(dr("Other_vehicle"))
                objtr.Other_VLC = clsCommon.myCstr(dr("Other_vLC"))

                objtr.Can_Code = clsCommon.myCstr(dr("Can_Code"))
                objtr.Gross_Weight = clsCommon.myCdbl(dr("Gross_Weight"))
                objtr.Tare_Weight = clsCommon.myCdbl(dr("Tare_Weight"))
                objtr.CanTareWeightOne = clsCommon.myCdbl(dr("OneCanTareWeight"))

                ObjList.Add(objtr)
            Next
        End If
        Return obj
    End Function

    Public Shared Function GetShift(ByVal Mcc_Code As String, Optional ByVal trans As SqlTransaction = Nothing) As DataTable
        Dim sQuery As String = "select TSPL_OPEN_MCC_SHIFT.*,UOM_Code from TSPL_OPEN_MCC_SHIFT Left join TSPL_Mcc_UOM_DETAIL on TSPL_Mcc_UOM_DETAIL.MCC_CODE=TSPL_OPEN_MCC_SHIFT.MCC_CODE and Stocking_Unit='Y' where lower(status)='o' and TSPL_OPEN_MCC_SHIFT.mcc_code='" & Mcc_Code & "'"
        Return clsDBFuncationality.GetDataTable(sQuery, trans)
    End Function

    Public Shared Function PostData(ByVal strDocNo As String, ByVal isCheckForPosted As Boolean) As Boolean
        'Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Code not found to Post")
            End If
            Dim strPostDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy hh:mm tt")
            Dim obj As clsMilkReceiptMCC = clsMilkReceiptMCC.GetData(strDocNo, NavigatorType.Current)
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleMCCMilkProcurement, clsUserMgtCode.frmMilkReceipt, obj.MCC_CODE, obj.DOC_DATE, Nothing)


            If (obj Is Nothing OrElse clsCommon.myLen(obj.DOC_CODE) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            If (isCheckForPosted AndAlso obj.POSTED = 1) Then
                Throw New Exception("Already Post on :" + obj.Posting_Date)
            End If

            Dim qry As String = "Update TSPL_MILK_RECEIPT_HEAD set POSTED=1, Posting_Date='" + strPostDate + "',Modified_By='" + objCommonVar.CurrentUserCode + "' where DOC_CODE ='" + strDocNo + "'"
            clsDBFuncationality.ExecuteNonQuery(qry)
            'trans.Commit()
        Catch ex As Exception
            'trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function GetDocCode(ByVal DocDate As Date, ByVal MCC_Code As String, ByVal Shift As String, Optional ByVal trans As SqlTransaction = Nothing) As String
        Return GetDocCode(DocDate, MCC_Code, Shift, "M", trans)
    End Function

    Public Shared Function GetDocCode(ByVal DocDate As Date, ByVal MCC_Code As String, ByVal Shift As String, ByVal strDockCollectionMilkType As String, Optional ByVal trans As SqlTransaction = Nothing) As String
        Return GetDocCode(DocDate, MCC_Code, Shift, "M", "", trans)
    End Function

    Public Shared Function GetDocCode(ByVal DocDate As Date, ByVal MCC_Code As String, ByVal Shift As String, ByVal strDockCollectionMilkType As String, ByVal strDockCode As String, Optional ByVal trans As SqlTransaction = Nothing) As String
        Dim qry As String
        qry = "SELECT DOC_CODE FROM TSPL_MILK_RECEIPT_HEAD WHERE MCC_CODE='" & MCC_Code & "' AND convert(date,DOC_DATE,103)='" & clsCommon.GetPrintDate(DocDate, "dd/MMM/yyyy") & "' AND SHIFT='" & Shift & "' and Dock_Collection_Milk_Type ='" + strDockCollectionMilkType + "'"
        If clsCommon.myLen(strDockCode) > 0 Then
            qry += " and TSPL_MILK_RECEIPT_HEAD.DOCK_Code='" + strDockCode + "'"
        End If
        Dim dt As DataTable
        dt = clsDBFuncationality.GetDataTable(qry, trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            Return dt.Rows(0).Item("DOC_CODE")
        Else
            Return ""
        End If
    End Function

    Public Shared Function GetPostNew(ByVal DocDate As Date, ByVal MCC_Code As String, ByVal Shift As String, ByVal DockCode As String, Optional ByVal trans As SqlTransaction = Nothing) As Boolean
        Dim qry As String
        qry = "SELECT Posted FROM TSPL_MILK_RECEIPT_HEAD WHERE MCC_CODE='" & MCC_Code & "' AND DOC_DATE='" & clsCommon.GetPrintDate(DocDate, "dd/MMM/yyyy") & "' AND SHIFT='" & Shift & "' and Posted=1"
        If clsCommon.myLen(DockCode) > 0 Then
            qry += " and DOCK_Code='" + DockCode + "'"
        End If

        Dim dt As DataTable
        dt = clsDBFuncationality.GetDataTable(qry, trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    'Public Shared Function GetConversion_factor(ByVal FromUOM As String, ByVal ToUOM As String, ByVal trans As SqlTransaction)
    '    Dim conv_fac As Double = 0
    '    Try
    '        Dim dt As DataTable = clsDBFuncationality.GetDataTable("select coalesce(Contained_Qty,1) as Contained_Qty from  TSPL_WEIGHT_CONVERSION where Container_UOM='" & FromUOM & "' and Contained_UOM='" + ToUOM + "'  and Product_Type in ('MI','ALL') order by Product_Type desc", trans)
    '        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
    '            conv_fac = clsCommon.myCdbl(dt.Rows(0)("Contained_Qty"))
    '        Else
    '            dt = clsDBFuncationality.GetDataTable("select coalesce(Contained_Qty,1) as Contained_Qty from  TSPL_WEIGHT_CONVERSION where Container_UOM='" & ToUOM & "' and Contained_UOM='" + FromUOM + "'  and Product_Type in ('MI','ALL') order by Product_Type desc", trans)
    '            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
    '                conv_fac = 1 / clsCommon.myCdbl(dt.Rows(0)("Contained_Qty"))
    '            Else
    '                Throw New Exception("Please porvide Weight conversion from " + FromUOM + " To " + ToUOM)
    '            End If
    '        End If
    '    Catch ex As Exception
    '        Throw New Exception(ex.Message)
    '    End Try
    '    Return conv_fac
    'End Function

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

            qry = "delete from TSPL_MILK_Receipt_DETAIL where DOC_CODE ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)


            qry = "delete from TSPL_MILK_Receipt_Head where DOC_CODE ='" + strCode + "'"
            isSaved = isSaved And clsDBFuncationality.ExecuteNonQuery(qry, trans)

            '' LOG FOR SYNC DATA
            isSaved = isSaved AndAlso clsSyncHeadTables.SaveSyncDelete("TSPL_MILK_Receipt_Head", strCode, trans)

            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message.ToString())
        End Try
        Return isSaved
    End Function

    Public Shared Function GetMilkType() As DataTable
        Return GetMilkType(True)
    End Function
    Public Shared Function GetMilkType(ByVal showBuffalow As Boolean) As DataTable
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))
        Dim dr As DataRow


        dr = dt.NewRow()
        dr("Code") = "M"
        dr("Name") = "Mix"
        dt.Rows.Add(dr)
        If showBuffalow Then
            dr = dt.NewRow()
            dr("Code") = "B"
            dr("Name") = "Buffalo"
            dt.Rows.Add(dr)
        End If


        dr = dt.NewRow()
        dr("Code") = "C"
        dr("Name") = "Cow"
        dt.Rows.Add(dr)

        Return dt
    End Function

    Public Shared Function GetReject(ByVal isGoodCodeGood As Boolean) As DataTable
        'Dim qry As String = "select Code,description as Name from TSPL_MILK_REJECT_TYPE order by SNo"
        'Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        'If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
        '    Dim dr As DataRow = dt.NewRow
        '    If isGoodCodeGood Then
        '        dr("Code") = "Good"
        '    Else
        '        dr("Code") = ""
        '    End If

        '    dr("Name") = "Good"
        '    dt.Rows.InsertAt(dr, 0)
        'End If
        'Return dt
        Return clsDBFuncationality.GetDataTable(GetRejectQry(isGoodCodeGood))
    End Function

    Public Shared Function GetRejectQry(ByVal isGoodCodeGood As Boolean) As String
        Dim qry As String = "select Code ,Name from ( select "
        If isGoodCodeGood Then
            qry += " 'Good' as Code,"
        Else
            qry += " '' as Code,"
        End If
        qry += " 'Good' as Name,-1 as SNo
union
select Code,description as Name,SNo from TSPL_MILK_REJECT_TYPE 
)xx
order by SNo"
        Return qry
    End Function

    Public Shared Function GetDockCollectionMilkType(ByVal ShowMixMilk As Boolean) As DataTable
        Return GetDockCollectionMilkType(ShowMixMilk, False)
    End Function

    Public Shared Function GetDockCollectionMilkType(ByVal ShowMixMilk As Boolean, ByVal isMergeMixAndBuffalo As Boolean) As DataTable
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))
        Dim dr As DataRow

        If ShowMixMilk Then
            dr = dt.NewRow()
            dr("Code") = "M"
            dr("Name") = "Mix"
            dt.Rows.Add(dr)
        End If
        If isMergeMixAndBuffalo Then
            dr = dt.NewRow()
            dr("Code") = "M"
            dr("Name") = "Mix/Buffalo"
            dt.Rows.Add(dr)
        Else
            dr = dt.NewRow()
            dr("Code") = "B"
            dr("Name") = "Buffalo"
            dt.Rows.Add(dr)
        End If
        dr = dt.NewRow()
        dr("Code") = "C"
        dr("Name") = "Cow"
        dt.Rows.Add(dr)

        Return dt
    End Function

    Public Shared Function GetSampleNo(ByVal DocDate As Date, ByVal MCC_Code As String, ByVal Shift As String, ByVal strVLCCode As String, ByRef TotalReceipt As Integer, Optional ByVal trans As SqlTransaction = Nothing) As Integer
        TotalReceipt = 0
        Dim intRet As Integer = 0
        Dim qry As String = "SELECT TSPL_MILK_RECEIPT_DETAIL.SAMPLE_NO 
FROM TSPL_MILK_RECEIPT_DETAIL  
left join TSPL_MILK_RECEIPT_HEAD on TSPL_MILK_RECEIPT_HEAD.doc_code=TSPL_MILK_RECEIPT_DETAIL.DOC_CODE 
WHERE TSPL_MILK_RECEIPT_HEAD.MCC_CODE='" & MCC_Code & "' AND convert(date,TSPL_MILK_RECEIPT_HEAD.DOC_DATE,103)='" & clsCommon.GetPrintDate(DocDate, "dd/MMM/yyyy") & "' AND TSPL_MILK_RECEIPT_HEAD.SHIFT='" & Shift & "' and TSPL_MILK_RECEIPT_DETAIL.VLC_CODE='" & strVLCCode & "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dt IsNot Nothing OrElse dt.Rows.Count > 0 Then
            TotalReceipt = dt.Rows.Count
            intRet = clsCommon.myCdbl(dt.Rows(0)("SAMPLE_NO"))
        End If
        Return intRet
    End Function

End Class


Public Class clsMilkReceiptMCCDetail
#Region "Variables"
    Public DOC_CODE As String
    Public VLC_DOC_CODE As String
    Public VLC_DOC_NUM As Integer
    Public SAMPLE_NO As Integer

    Public VLC_CODE As String
    Public VLC_CODE_Uploader_Code As String
    Public ROUTE_CODE As String
    Public VSP_CODE As String
    Public Item_CODE As String
    Public VEHICLE_CODE As String
    Public Other_VEHICLE As String
    Public Other_VLC As String
    Public NO_OF_CANS As Integer
    Public MILK_WEIGHT As Decimal
    Public ACC_WEIGHT As Decimal
    Public LTR_WEIGHT As Decimal
    Public TYPE As String
    Public MILK_TYPE As String
    Public SAMPLE_NO_VALUES As String

    Public FAT As Decimal = 0
    Public SNF As Decimal = 0
    Public RATE As Decimal = 0
    Public Amount As Decimal = 0
    Public CLR As Decimal = 0

    Public MCC_CODE As String
    Public DOC_DATE As DateTime
    Public SHIFT As String
    Public COMM_PORT As String
    Public MACHINE_NO As String
    Public IS_ENTERED_MANUAL As String
    Public UOM_Code As String = String.Empty
    Public Conversion_Factor As Double = 0
    Public IS_Sampled As String
    Public Eco_Pro_Name As String
    Public Against_Uploader_TR_No As String
    Public Against_Shift_Uploader_TR_No As String
    Public Gross_Weight As Decimal
    Public Tare_Weight As Decimal
    Public Can_Code As String
    Public CanTareWeightOne As Decimal '' not a table column
#End Region


    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsMilkReceiptMCCDetail), ByVal strDocCollectionMilkType As String, ByVal strDockCode As String, ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsMilkReceiptMCCDetail In Arr
                Dim coll As New Hashtable()
                Dim vlcDoc(1) As String
                vlcDoc = GetVLC_DOC_CODE(obj.VLC_CODE, trans, obj.VLC_DOC_CODE)
                Dim Sample(1) As String
                Sample = GetMilkSampleNo(obj, trans, obj.VLC_DOC_CODE)
                If GetSavedMilkSampleNo(obj, trans, obj.SAMPLE_NO, obj.SAMPLE_NO, obj.VLC_DOC_CODE, strDocCollectionMilkType, strDockCode) Then
                    Throw New Exception("This Sample No " & obj.SAMPLE_NO & " has been Already saved.")
                End If
                clsCommon.AddColumnsForChange(coll, "DOC_CODE", strDocNo)
                clsCommon.AddColumnsForChange(coll, "VLC_DOC_CODE", vlcDoc(0))
                clsCommon.AddColumnsForChange(coll, "VLC_DOC_NUM", vlcDoc(1))
                clsCommon.AddColumnsForChange(coll, "VLC_CODE", obj.VLC_CODE)
                clsCommon.AddColumnsForChange(coll, "ROUTE_CODE", obj.ROUTE_CODE)
                clsCommon.AddColumnsForChange(coll, "NO_OF_CANS", obj.NO_OF_CANS)
                clsCommon.AddColumnsForChange(coll, "VSP_CODE", obj.VSP_CODE)
                If objCommonVar.DisplayTypeInMilkReceipt Then
                    If clsCommon.CompairString(obj.TYPE, "C") = CompairStringResult.Equal Then
                        obj.Item_CODE = objCommonVar.DefaultMilkItemCodeCow
                    ElseIf clsCommon.CompairString(obj.TYPE, "B") = CompairStringResult.Equal Then
                        obj.Item_CODE = objCommonVar.DefaultMilkItemCodeBuffalo
                    ElseIf clsCommon.CompairString(obj.TYPE, "M") = CompairStringResult.Equal Then
                        obj.Item_CODE = objCommonVar.DefaultMilkItemCode
                    End If
                Else
                    obj.Item_CODE = objCommonVar.DefaultMilkItemCode
                End If
                If clsCommon.myLen(obj.Item_CODE) <= 0 Then
                    Throw New Exception("Please select defalut Milk Item Code")
                End If

                clsCommon.AddColumnsForChange(coll, "Item_CODE", obj.Item_CODE)
                clsCommon.AddColumnsForChange(coll, "MILK_WEIGHT", obj.MILK_WEIGHT)
                clsCommon.AddColumnsForChange(coll, "ACC_WEIGHT", obj.ACC_WEIGHT)
                clsCommon.AddColumnsForChange(coll, "ACC_WEIGHT_LTR", obj.LTR_WEIGHT)
                clsCommon.AddColumnsForChange(coll, "FAT", obj.FAT)
                clsCommon.AddColumnsForChange(coll, "SNF", obj.SNF)
                clsCommon.AddColumnsForChange(coll, "VEHICLE_CODE", obj.VEHICLE_CODE)
                clsCommon.AddColumnsForChange(coll, "Other_VEHICLE", obj.Other_VEHICLE)
                clsCommon.AddColumnsForChange(coll, "Other_VLC", obj.Other_VLC)
                clsCommon.AddColumnsForChange(coll, "SHIFT", obj.SHIFT)
                clsCommon.AddColumnsForChange(coll, "TYPE", obj.TYPE)
                clsCommon.AddColumnsForChange(coll, "MILK_TYPE", obj.MILK_TYPE)
                clsCommon.AddColumnsForChange(coll, "SAMPLE_NO_VALUES", Sample(1))
                clsCommon.AddColumnsForChange(coll, "MCC_CODE", obj.MCC_CODE)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "DOC_DATE", clsCommon.GetPrintDate(obj.DOC_DATE, "dd/MMM/yyyy hh:mm:ss tt"))
                clsCommon.AddColumnsForChange(coll, "COMM_PORT", obj.COMM_PORT)
                clsCommon.AddColumnsForChange(coll, "MACHINE_NO", obj.MACHINE_NO)
                clsCommon.AddColumnsForChange(coll, "IS_MANUAL", obj.IS_ENTERED_MANUAL)
                clsCommon.AddColumnsForChange(coll, "Against_Uploader_TR_No", obj.Against_Uploader_TR_No, True)
                clsCommon.AddColumnsForChange(coll, "Against_Shift_Uploader_TR_No", obj.Against_Shift_Uploader_TR_No, True)
                clsCommon.AddColumnsForChange(coll, "Gross_Weight", obj.Gross_Weight)
                clsCommon.AddColumnsForChange(coll, "Tare_Weight", obj.Tare_Weight)
                clsCommon.AddColumnsForChange(coll, "Can_Code", obj.Can_Code, True)
                clsCommon.AddColumnsForChange(coll, "Entry_Date_Time", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))
                If obj.VLC_DOC_CODE = "" Then
                    clsCommon.AddColumnsForChange(coll, "SAMPLE_NO", obj.SAMPLE_NO)
                    clsCommon.AddColumnsForChange(coll, "UOM_Code", obj.UOM_Code)
                    clsCommon.AddColumnsForChange(coll, "Conversion_factor", obj.Conversion_Factor)
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MILK_RECEIPT_DETAIL", OMInsertOrUpdate.Insert, "TSPL_MILK_RECEIPT_DETAIL.DOC_CODE='" + strDocNo + "'", trans)
                    If obj.Other_VLC = 1 Then
                        If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MilkReceiptRequiredApproval, clsFixedParameterCode.MilkReceiptRequiredApproval, trans)) = 1 Then
                            Dim qry As String = "select * from TSPL_TRANSACTION_APPROVAL where Document_No='" & strDocNo & "' and Screen_Name='Mcc Receipt'"
                            Dim dtApproval As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                            If dtApproval.Rows.Count <= 0 Then
                                qry = "insert into TSPL_TRANSACTION_APPROVAL(Screen_Name,Program_Code,Document_No,Doc_Date,approval_type,Approve,Created_By,Created_Date,Modified_By,Modified_Date,Comp_Code) " & _
                                "values ('Mcc Receipt','" & clsUserMgtCode.frmMilkReceipt & "','" & strDocNo & "','" & clsCommon.GetPrintDate(obj.DOC_DATE, "dd-MMM-yyyy hh:mm:ss") & "','Other',0,'" + objCommonVar.CurrentUserCode + "','" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt") + "','" + objCommonVar.CurrentUserCode + "','" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt") + "','" & objCommonVar.CurrentCompanyCode & "')"
                                clsDBFuncationality.ExecuteNonQuery(qry, trans)
                            Else
                                qry = "update TSPL_TRANSACTION_APPROVAL set  Approve=0  where Document_No='" & strDocNo & "' and Screen_Name='Mcc Receipt'" & _
                                clsDBFuncationality.ExecuteNonQuery(qry, trans)
                            End If
                        End If
                    End If
                Else
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MILK_RECEIPT_DETAIL", OMInsertOrUpdate.Update, "TSPL_MILK_RECEIPT_DETAIL.DOC_CODE='" + strDocNo + "' and TSPL_MILK_RECEIPT_DETAIL.SAMPLE_nO='" & Sample(0) & "'", trans)
                End If
                obj.VLC_DOC_CODE = vlcDoc(0)
                obj.VLC_DOC_NUM = vlcDoc(1)
                obj.SAMPLE_NO_VALUES = Sample(1)
            Next
        End If
        Return True
    End Function

    Public Shared Function GetVLC_DOC_CODE(ByVal VLC_Code As String, ByVal trans As SqlTransaction, Optional ByVal vlc_doc_code As String = "") As String()
        Dim arr(1) As String
        Dim qry As String
        qry = "select (coalesce(MAX(VLC_DOC_NUM),0)+1) AS VLC_DOC_NUM from TSPL_MILK_RECEIPT_DETAIL  WHERE VLC_CODE='" & VLC_Code & "' "
        Dim dtVLC As DataTable
        dtVLC = clsDBFuncationality.GetDataTable(qry, trans)
        If dtVLC.Rows.Count > 0 Then
            arr(0) = VLC_Code & "/" & dtVLC.Rows(0).Item("VLC_DOC_NUM")
            arr(1) = dtVLC.Rows(0).Item("VLC_DOC_NUM")
        Else
            arr(0) = VLC_Code & "/" & "0001"
            arr(1) = "1"
        End If
        Return arr
    End Function

    Public Shared Function GetMilkSampleNo(ByVal objDtl As clsMilkReceiptMCCDetail, ByVal trans As SqlTransaction, Optional ByVal vlc_doc_code As String = "") As String()
        Dim arr(1) As String
        Dim qry As String
        If vlc_doc_code = "" Then
            qry = "select (coalesce(MAX(SAMPLE_NO),0)+1) AS SAMPLE_NO from TSPL_MILK_RECEIPT_DETAIL WHERE MCC_CODE='" & objDtl.MCC_CODE & "' and convert(date,DOC_DATE,103)='" & clsCommon.GetPrintDate(objDtl.DOC_DATE, "dd/MMM/yyyy") & "'  and SHIFT='" & objDtl.SHIFT & "' and COMM_PORT='" & objDtl.COMM_PORT & "' and  MACHINE_NO='" & objDtl.MACHINE_NO & "' "
        Else
            qry = "select  SAMPLE_NO from TSPL_MILK_RECEIPT_DETAIL  WHERE VLC_DOC_CODE='" & vlc_doc_code & "' "
        End If
        Dim dtSample As DataTable
        dtSample = clsDBFuncationality.GetDataTable(qry, trans)
        If dtSample.Rows.Count > 0 Then
            arr(0) = dtSample.Rows(0).Item("SAMPLE_NO")
            arr(1) = objDtl.MCC_CODE & " / " & objDtl.VLC_CODE & " / " & clsCommon.GetPrintDate(objDtl.DOC_DATE, "dd/MMM/yyyy") & " / " & objDtl.SHIFT & " / " & objDtl.COMM_PORT & " / " & objDtl.MACHINE_NO & ""
        Else
            arr(0) = "1"
            arr(1) = objDtl.MCC_CODE & " / " & objDtl.VLC_CODE & " / " & clsCommon.GetPrintDate(objDtl.DOC_DATE, "dd/MMM/yyyy") & "  / " & objDtl.SHIFT & " / " & objDtl.COMM_PORT & " / " & objDtl.MACHINE_NO & ""
        End If
        Return arr
    End Function

    Public Shared Function GetSavedMilkSampleNo(ByVal objDtl As clsMilkReceiptMCCDetail, ByVal trans As SqlTransaction, ByVal sample_no As String, ByVal To_sample_no As String, ByVal vlc_doc_code As String, ByVal strDocCollectionMilkType As String, ByVal strDockCode As String) As Boolean
        Dim arr(1) As String
        Dim qry As String
        If vlc_doc_code = "" Then
            qry = "select TSPL_MILK_RECEIPT_DETAIL.* from TSPL_MILK_RECEIPT_DETAIL left outer join TSPL_MILK_RECEIPT_HEAD on  TSPL_MILK_RECEIPT_HEAD.DOC_CODE=TSPL_MILK_RECEIPT_DETAIL.DOC_CODE WHERE TSPL_MILK_RECEIPT_DETAIL.MCC_CODE='" & objDtl.MCC_CODE & "' and convert(date,TSPL_MILK_RECEIPT_DETAIL.DOC_DATE,103)='" & clsCommon.GetPrintDate(objDtl.DOC_DATE, "dd/MMM/yyyy") & "'  and TSPL_MILK_RECEIPT_DETAIL.SHIFT='" & objDtl.SHIFT & "' and TSPL_MILK_RECEIPT_DETAIL.COMM_PORT='" & objDtl.COMM_PORT & "' and  TSPL_MILK_RECEIPT_DETAIL.MACHINE_NO='" & objDtl.MACHINE_NO & "' and TSPL_MILK_RECEIPT_DETAIL.sample_no>=" & sample_no & " and TSPL_MILK_RECEIPT_DETAIL.sample_no<=" & To_sample_no & " and TSPL_MILK_RECEIPT_HEAD.Dock_Collection_Milk_Type='" + strDocCollectionMilkType + "'"
            If clsCommon.myLen(strDockCode) > 0 Then
                qry += " and TSPL_MILK_RECEIPT_HEAD.DOCK_Code='" + strDockCode + "'"
            End If
        Else
            qry = "select  * from TSPL_MILK_RECEIPT_DETAIL  WHERE VLC_DOC_CODE='" & vlc_doc_code & "' and sample_no='" & sample_no & "' "
        End If
        Dim dtSample As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dtSample IsNot Nothing AndAlso dtSample.Rows.Count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function
End Class


Public Class clsMilkReceiptImproperWeightLog
#Region "Variables"
    Public Log_Code As String
    Public Doc_Code As String
    Public Sample_No As Integer
    Public Min_Weight_Value As Decimal
    Public MCC_Code As String ''not a table column

    Public Function SaveData(ByVal obj As clsMilkReceiptImproperWeightLog) As Boolean
        Dim tran As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If clsCommon.myLen(obj.Doc_Code) > 0 AndAlso clsCommon.myLen(obj.MCC_Code) > 0 Then
                Dim coll As New Hashtable()

                Dim qry As String = "delete from TSPL_MILK_RECEIPT_IMPROPER_WEIGHT_LOG where Doc_Code='" + obj.Doc_Code + "' and sample_No='" + clsCommon.myCstr(obj.Sample_No) + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, tran)
                qry = "select max(Log_Code) from TSPL_MILK_RECEIPT_IMPROPER_WEIGHT_LOG where Log_Code like '" + obj.MCC_Code + "%' "
                obj.Log_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, tran))
                If clsCommon.myLen(obj.Log_Code) > 0 Then
                    obj.Log_Code = clsCommon.incval(obj.Log_Code)
                Else
                    obj.Log_Code = obj.MCC_Code + "/0000000001"
                End If
                clsCommon.AddColumnsForChange(coll, "Log_Code", obj.Log_Code)
                clsCommon.AddColumnsForChange(coll, "Doc_Code", obj.Doc_Code)
                clsCommon.AddColumnsForChange(coll, "Sample_No", obj.Sample_No)
                clsCommon.AddColumnsForChange(coll, "Min_Weight_Value", obj.Min_Weight_Value)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(tran), "dd/MMM/yyyy hh:mm:ss tt"))
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MILK_RECEIPT_IMPROPER_WEIGHT_LOG", OMInsertOrUpdate.Insert, "", tran)
            End If
            tran.Commit()
        Catch ex As Exception
            tran.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True

    End Function
#End Region
End Class



