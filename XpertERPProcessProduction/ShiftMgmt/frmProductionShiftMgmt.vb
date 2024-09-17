Imports common
Imports System.IO

Public Class frmProductionShiftMgmt
    Inherits FrmMainTranScreen
#Region "Variables"
    Dim isNewEntry As Boolean = False

    Const ColOPPKID As String = "ColOPPKID"
    Const ColOPSNo As String = "ColOPSNo"
    Const ColOPLocationCode As String = "ColOPLocationCode"
    Const ColOPLocationName As String = "ColOPLocationName"
    Const ColOPItemCode As String = "ColOPItemCode"
    Const ColOPItemName As String = "ColOPItemName"
    Const ColOPQtyLtr As String = "ColOPQtyLtr"
    Const ColOPQtyKG As String = "ColOPQtyKG"
    Const ColOPFAT As String = "ColOPFAT"
    Const ColOPSNF As String = "ColOPSNF"
    Const ColOPFATKG As String = "ColOPFATKG"
    Const ColOPSNFKG As String = "ColOPSNFKG"
    Const ColOPTemp As String = "ColOPTemp"
    Const ColOPAcidity As String = "ColOPAcidity"
    Const ColOPCOB As String = "ColOPCOB"
    Const ColOPAlcohol As String = "ColOPAlcohol"
    Const ColOPRemarks As String = "ColOPRemarks"


    Const ColRecPlantPKID As String = "ColRecPlantPKID"
    Const ColRecPlantSNo As String = "ColRecPlantSNo"
    Const ColRecPlantShift As String = "ColRecPlantLocationCode"
    Const ColRecPlantRejectType As String = "ColRecPlantLocationName"
    Const ColRecPlantItemCode As String = "ColRecPlantItemCode"
    Const ColRecPlantItemName As String = "ColRecPlantItemName"
    Const ColRecPlantQtyLtr As String = "ColRecPlantQtyLtr"
    Const ColRecPlantQtyKG As String = "ColRecPlantQtyKG"
    Const ColRecPlantFAT As String = "ColRecPlantFAT"
    Const ColRecPlantSNF As String = "ColRecPlantSNF"
    Const ColRecPlantFATKG As String = "ColRecPlantFATKG"
    Const ColRecPlantSNFKG As String = "ColRecPlantSNFKG"
    Const ColRecPlantRemarks As String = "ColRecPlantRemarks"

    Const ColRecBulkPKID As String = "ColRecBulkPKID"
    Const ColRecBulkSNo As String = "ColRecBulkSNo"
    Const ColRecBulkTranType As String = "ColRecBulkTranType"
    Const ColRecBulkTranTypeName As String = "ColRecBulkTranTypeName"
    Const ColRecBulkTranNo As String = "ColRecBulkTranNo"
    Const ColRecBulkTankerNo As String = "ColRecBulkTankerNo"
    Const ColRecBulkReciveFrom As String = "ColRecBulkReciveFrom"
    Const ColRecBulkReciveFromName As String = "ColRecBulkReciveFromName"
    Const ColRecBulkItemCode As String = "ColRecBulkItemCode"
    Const ColRecBulkItemName As String = "ColRecBulkItemName"
    Const ColRecBulkQtyLtr As String = "ColRecBulkQtyLtr"
    Const ColRecBulkQtyKG As String = "ColRecBulkQtyKG"
    Const ColRecBulkFAT As String = "ColRecBulkFAT"
    Const ColRecBulkSNF As String = "ColRecBulkSNF"
    Const ColRecBulkFATKG As String = "ColRecBulkFATKG"
    Const ColRecBulkSNFKG As String = "ColRecBulkSNFKG"
    Const ColRecBulkTemp As String = "ColRecBulkTemp"
    Const ColRecBulkAcidity As String = "ColRecBulkAcidity"
    Const ColRecBulkCOB As String = "ColRecBulkCOB"
    Const ColRecBulkAlcohol As String = "ColRecBulkAlcohol"
    Const ColRecBulkRemarks As String = "ColRecBulkRemarks"

    Const ColProPKID As String = "ColProPKID"
    Const ColProSNo As String = "ColProSNo"
    Const ColProItemCode As String = "ColProItemCode"
    Const ColProItemName As String = "ColProItemName"
    Const ColProQtyKG As String = "ColProQtyKG"
    Const ColProQtyLTR As String = "ColProQtyLTR"
    Const ColProFAT As String = "ColProFAT"
    Const ColProSNF As String = "ColProSNF"
    Const ColProFATKG As String = "ColProFATKG"
    Const ColProSNFKG As String = "ColProSNFKG"
    Const ColProAdd As String = "ColProAdd"
    Const ColProRemove As String = "ColProRemove"
    Const ColProTemp As String = "ColProTemp"
    Const ColProAcidity As String = "ColProAcidity"
    Const ColProCOB As String = "ColProCOB"
    Const ColProAlcohol As String = "ColProAlcohol"
    Const ColProRemarks As String = "ColProRemarks"
    Const ColProEnteredUOM As String = "ColProEnteredUOM"
    Const ColProBOMCode As String = "ColProBOMCode"

    Const ColProRMPKID As String = "ColProRMPKID"
    Const ColProRMSNo As String = "ColProRMSNo"
    Const ColProRMItemCode As String = "ColProRMItemCode"
    Const ColProRMItemName As String = "ColProRMItemName"
    Const ColProRMQty As String = "ColProRMQty"
    Const ColProRMUOM As String = "ColProRMUOM"
    Const ColProRMFAT As String = "ColProRMFAT"
    Const ColProRMSNF As String = "ColProRMSNF"
    Const ColProRMFATKG As String = "ColProRMFATKG"
    Const ColProRMSNFKG As String = "ColProRMSNFKG"
    Const ColProRMIssue As String = "ColProRMIssue"

    Const colCLPKID As String = "colCLPKID"
    Const colCLSNo As String = "colCLSNo"
    Const colCLLocationCode As String = "colCLLocationCode"
    Const colCLLocationName As String = "colCLLocationName"
    Const colCLItemCode As String = "colCLItemCode"
    Const colCLItemName As String = "colCLItemName"
    Const colCLOPQtyLtr As String = "colCLOPQtyLtr"
    Const colCLOPQtyKG As String = "colCLOPQtyKG"
    Const colCLOPFAT As String = "colCLOPFAT"
    Const colCLOPSNF As String = "colCLOPSNF"
    Const colCLOPFATKG As String = "colCLOPFATKG"
    Const colCLOPSNFKG As String = "colCLOPSNFKG"
    Const colCLQtyLtr As String = "colCLQtyLtr"
    Const colCLQtyKG As String = "colCLQtyKG"
    Const colCLFAT As String = "colCLFAT"
    Const colCLSNF As String = "colCLSNF"
    Const colCLFATKG As String = "colCLFATKG"
    Const colCLSNFKG As String = "colCLSNFKG"
    Const colCLTemp As String = "colCLTemp"
    Const colCLAcidity As String = "colCLAcidity"
    Const colCLCOB As String = "colCLCOB"
    Const colCLAlcohol As String = "colCLAlcohol"
    Const colCLRemarks As String = "colCLRemarks"

    Const ReportID As String = "ShftMgmt"
    Dim isInsideLoadData As Boolean = False
    Dim isCellValueChangedOpen As Boolean = False
    Dim arrLoc As String = Nothing
#End Region

    Private Sub frmDairyProductionUploader_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        RadPageView1.SelectedPage = RadPageViewPage1
        RadPageView2.SelectedPage = RadPageViewPage5
        RadPageView3.SelectedPage = RadPageViewPage7
        LoadShift()
        LOCATIONRIGTHS()
        AddNew()
        btnReverse.Visible = False
    End Sub
    Sub LoadShift()
        Dim qry As String = " select  SHIFT_CODE,SHIFT_NAME from TSPL_SHIFT_MASTER "
        cboShift.DataSource = clsDBFuncationality.GetDataTable(qry)
        cboShift.ValueMember = "SHIFT_CODE"
        cboShift.DisplayMember = "SHIFT_NAME"
    End Sub
    Private Sub LOCATIONRIGTHS()
        Dim obj As New clsMCCCodes()
        Try
            obj = clsMCCCodes.GetData()
            If obj.arrLocCodes IsNot Nothing AndAlso clsCommon.myLen(obj.arrLocCodes) > 0 Then
                arrLoc = obj.arrLocCodes
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            obj = Nothing
        End Try
    End Sub
    Private Sub RadButton1_Click_1(sender As Object, e As EventArgs) Handles RadButton1.Click
        Try
            If clsCommon.myLen(cboShift.SelectedValue) <= 0 Then
                Throw New Exception("Please select " + cboShift.MyLinkLable1.Text)
            End If
            If clsCommon.myLen(txtLocation.Value) <= 0 Then
                Throw New Exception("Please select " + txtLocation.MyLinkLable1.Text)
            End If
            Dim qry As String = "select Document_No from TSPL_SHIFT_MGMT where Document_Date='" + clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") + "' and Shift_Code='" + clsCommon.myCstr(cboShift.SelectedValue) + "' and Location_Code='" + txtLocation.Value + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                Dim ShiftFromDate As DateTime
                Dim ShiftToDate As DateTime = clsShiftMaster.GetShiftTime(clsCommon.myCstr(cboShift.SelectedValue), txtDate.Value, ShiftFromDate)
                LoadBlankGrid()
                qry = "select ROW_NUMBER() OVER(ORDER BY Location_Code,Item_Code) AS SNo, xxxx.*,case when Stock_Qty_KG>0 then cast((Fat_KG*100/Stock_Qty_KG) as decimal(18,2)) else 0 end FAT,case when Stock_Qty_KG>0 then cast((SNF_KG*100/Stock_Qty_KG) as decimal(18,2)) else 0 end SNF from (
select xxx.Location_Code,xxx.Location_Desc,xxx.Item_Code,xxx.Item_Desc
,case when xxx.Stock_UOM='LTR' then xxx.Stock_Qty else xxx.Stock_Qty/TabUOMLTR.Conversion_Factor end as Stock_Qty_LTR
,case when xxx.Stock_UOM='KG' then xxx.Stock_Qty else xxx.Stock_Qty/TabUOMKG.Conversion_Factor end as Stock_Qty_KG
,xxx.Fat_KG,xxx.SNF_KG  from (
select Location_Code,max(Location_Desc) as Location_Desc,Item_Code,max(Item_Desc) as Item_Desc,sum(Stock_Qty*RI) as Stock_Qty,Stock_UOM,sum(Fat_KG*RI) as Fat_KG,sum(SNF_KG*RI) as SNF_KG from (
select TSPL_INVENTORY_MOVEMENT_NEW.Location_Code,TSPL_LOCATION_MASTER.Location_Desc,TSPL_INVENTORY_MOVEMENT_NEW.Item_Code,TSPL_ITEM_MASTER.Item_Desc,case when TSPL_INVENTORY_MOVEMENT_NEW.InOut='I' then 1 else -1 end as RI,TSPL_INVENTORY_MOVEMENT_NEW.Avg_Cost,TSPL_INVENTORY_MOVEMENT_NEW.Stock_Qty,TSPL_INVENTORY_MOVEMENT_NEW.Stock_UOM,
cast( TSPL_INVENTORY_MOVEMENT_NEW.Fat_KG as decimal(18,3)) as Fat_KG ,cast(TSPL_INVENTORY_MOVEMENT_NEW.SNF_KG as decimal(18,3)) as SNF_KG
 from TSPL_INVENTORY_MOVEMENT_NEW 
left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_INVENTORY_MOVEMENT_NEW.Item_Code
left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_INVENTORY_MOVEMENT_NEW.Location_Code
where TSPL_ITEM_MASTER.Product_Type='MI' and TSPL_LOCATION_MASTER.Main_Location_Code='" + txtLocation.Value + "' and Punching_Date<'" + clsCommon.GetPrintDate(ShiftFromDate, "dd/MMM/yyyy hh:mm tt") + "' and TSPL_INVENTORY_MOVEMENT_NEW.Stock_UOM in ('LTR','KG')
)xx group by Location_Code,Item_Code,Stock_UOM
)xxx 
left outer join TSPL_ITEM_UOM_DETAIL as TabUOMLTR on TabUOMLTR.Item_Code=xxx.Item_Code and TabUOMLTR.UOM_Code='LTR'
left outer join TSPL_ITEM_UOM_DETAIL as TabUOMKG on TabUOMKG.Item_Code=xxx.Item_Code and TabUOMKG.UOM_Code='KG'
where (xxx.Stock_Qty>0 and (xxx.Fat_KG>0 or xxx.SNF_KG>0))
)xxxx"
                dt = clsDBFuncationality.GetDataTable(qry)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    gvOP.DataSource = Nothing
                    gvOP.AutoGenerateColumns = False
                    gvOP.DataSource = dt
                    gvOP.Columns(ColOPSNo).FieldName = "SNo"
                    gvOP.Columns(ColOPLocationCode).FieldName = "Location_Code"
                    gvOP.Columns(ColOPLocationName).FieldName = "Location_Desc"
                    gvOP.Columns(ColOPItemCode).FieldName = "Item_Code"
                    gvOP.Columns(ColOPItemName).FieldName = "Item_Desc"
                    gvOP.Columns(ColOPQtyLtr).FieldName = "Stock_Qty_LTR"
                    gvOP.Columns(ColOPQtyKG).FieldName = "Stock_Qty_KG"
                    gvOP.Columns(ColOPFAT).FieldName = "FAT"
                    gvOP.Columns(ColOPSNF).FieldName = "SNF"
                    gvOP.Columns(ColOPFATKG).FieldName = "Fat_KG"
                    gvOP.Columns(ColOPSNFKG).FieldName = "SNF_KG"


                    gvCL.DataSource = Nothing
                    gvCL.AutoGenerateColumns = False
                    gvCL.DataSource = dt
                    gvCL.Columns(colCLSNo).FieldName = "SNo"
                    gvCL.Columns(colCLLocationCode).FieldName = "Location_Code"
                    gvCL.Columns(colCLLocationName).FieldName = "Location_Desc"
                    gvCL.Columns(colCLItemCode).FieldName = "Item_Code"
                    gvCL.Columns(colCLItemName).FieldName = "Item_Desc"
                    gvCL.Columns(colCLOPQtyLtr).FieldName = "Stock_Qty_LTR"
                    gvCL.Columns(colCLOPQtyKG).FieldName = "Stock_Qty_KG"
                    gvCL.Columns(colCLOPFAT).FieldName = "FAT"
                    gvCL.Columns(colCLOPSNF).FieldName = "SNF"
                    gvCL.Columns(colCLOPFATKG).FieldName = "Fat_KG"
                    gvCL.Columns(colCLOPSNFKG).FieldName = "SNF_KG"
                End If

                qry = "select ROW_NUMBER() OVER(ORDER BY SHIFT,Reject_Type,Item_Code) AS SNo, xxxx.*,case when Stock_Qty_KG>0 then cast((Fat_KG*100/Stock_Qty_KG) as decimal(18,2)) else 0 end FAT,case when Stock_Qty_KG>0 then cast((SNF_KG*100/Stock_Qty_KG) as decimal(18,2)) else 0 end SNF from (
select xxx.SHIFT,xxx.Reject_Type,xxx.Item_Code,xxx.Item_Desc
,case when xxx.Stock_UOM='LTR' then xxx.Stock_Qty else xxx.Stock_Qty/TabUOMLTR.Conversion_Factor end as Stock_Qty_LTR
,case when xxx.Stock_UOM='KG' then xxx.Stock_Qty else xxx.Stock_Qty/TabUOMKG.Conversion_Factor end as Stock_Qty_KG
,xxx.Fat_KG,xxx.SNF_KG  from (
select SHIFT,Reject_Type,Item_Code,max(Item_Desc) as Item_Desc,sum(Stock_Qty*RI) as Stock_Qty,Stock_UOM,sum(Fat_KG*RI) as Fat_KG,sum(SNF_KG*RI) as SNF_KG from (
select * from (
select case when TSPL_MILK_SRN_HEAD.SHIFT='M' then  cast( replace(convert(varchar,TSPL_INVENTORY_MOVEMENT_NEW.Punching_Date,106),' ','/') +' '+TSPL_MCC_MASTER.Shift_Closing_Time as datetime)  else cast( replace( convert(varchar,TSPL_INVENTORY_MOVEMENT_NEW.Punching_Date,106),' ','/') +' '+TSPL_MCC_MASTER.Shift_Eve_Closing_Time as datetime) end as MCC_Shift_Time,
TSPL_MILK_SRN_HEAD.SHIFT,isnull((case when TSPL_MILK_SHIFT_UPLOADER_DETAIL.TR_No is not null then TSPL_MILK_SHIFT_UPLOADER_DETAIL.Reject_Type else TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Reject_Type end),'Good') as Reject_Type,TSPL_INVENTORY_MOVEMENT_NEW.Punching_Date,TSPL_INVENTORY_MOVEMENT_NEW.Item_Code,TSPL_ITEM_MASTER.Item_Desc,case when TSPL_INVENTORY_MOVEMENT_NEW.InOut='I' then 1 else -1 end as RI,TSPL_INVENTORY_MOVEMENT_NEW.Avg_Cost,TSPL_INVENTORY_MOVEMENT_NEW.Stock_Qty,TSPL_INVENTORY_MOVEMENT_NEW.Stock_UOM,
cast( TSPL_INVENTORY_MOVEMENT_NEW.Fat_KG as decimal(18,3)) as Fat_KG ,cast(TSPL_INVENTORY_MOVEMENT_NEW.SNF_KG as decimal(18,3)) as SNF_KG
 from TSPL_INVENTORY_MOVEMENT_NEW 
left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_INVENTORY_MOVEMENT_NEW.Item_Code
inner join TSPL_MILK_SRN_HEAD on TSPL_MILK_SRN_HEAD.DOC_CODE=TSPL_INVENTORY_MOVEMENT_NEW.Source_Doc_No
inner join TSPL_MILK_SRN_DETAIL on TSPL_MILK_SRN_DETAIL.DOC_CODE=TSPL_MILK_SRN_HEAD.DOC_CODE
inner join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code=TSPL_MILK_SRN_HEAD.MCC_CODE
left outer join TSPL_MILK_SHIFT_UPLOADER_DETAIL on TSPL_MILK_SHIFT_UPLOADER_DETAIL.TR_No=TSPL_MILK_SRN_HEAD.Against_Shift_Uploader_TR_No
left outer join TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL on TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.TR_No=TSPL_MILK_SRN_HEAD.Against_Uploader_TR_No
where TSPL_INVENTORY_MOVEMENT_NEW.Trans_Type ='MCC-MSRN' and TSPL_MCC_MASTER.MCC_in_Plant=1
and  TSPL_ITEM_MASTER.Product_Type='MI' and convert(date, Punching_Date)='" + clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") + "' and TSPL_INVENTORY_MOVEMENT_NEW.Stock_UOM in ('LTR','KG')
) xx where  2=(case when MCC_Shift_Time>='" + clsCommon.GetPrintDate(ShiftFromDate, "dd/MMM/yyyy hh:mm:ss tt") + "' and MCC_Shift_Time<='" + clsCommon.GetPrintDate(ShiftToDate, "dd/MMM/yyyy hh:mm:ss tt") + "' then 2 else 3 end )
)x
group by SHIFT,Reject_Type,Item_Code,Stock_UOM
) xxx 
left outer join TSPL_ITEM_UOM_DETAIL as TabUOMLTR on TabUOMLTR.Item_Code=xxx.Item_Code and TabUOMLTR.UOM_Code='LTR'
left outer join TSPL_ITEM_UOM_DETAIL as TabUOMKG on TabUOMKG.Item_Code=xxx.Item_Code and TabUOMKG.UOM_Code='KG'
where (xxx.Stock_Qty>0 and (xxx.Fat_KG>0 or xxx.SNF_KG>0))
)xxxx"
                dt = clsDBFuncationality.GetDataTable(qry)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    gvRecPlant.DataSource = Nothing
                    gvRecPlant.AutoGenerateColumns = False
                    gvRecPlant.DataSource = dt
                    gvRecPlant.Columns(ColRecPlantSNo).FieldName = "SNo"
                    gvRecPlant.Columns(ColRecPlantShift).FieldName = "SHIFT"
                    gvRecPlant.Columns(ColRecPlantRejectType).FieldName = "Reject_Type"
                    gvRecPlant.Columns(ColRecPlantItemCode).FieldName = "Item_Code"
                    gvRecPlant.Columns(ColRecPlantItemName).FieldName = "Item_Desc"
                    gvRecPlant.Columns(ColRecPlantQtyLtr).FieldName = "Stock_Qty_LTR"
                    gvRecPlant.Columns(ColRecPlantQtyKG).FieldName = "Stock_Qty_KG"
                    gvRecPlant.Columns(ColRecPlantFAT).FieldName = "FAT"
                    gvRecPlant.Columns(ColRecPlantSNF).FieldName = "SNF"
                    gvRecPlant.Columns(ColRecPlantFATKG).FieldName = "Fat_KG"
                    gvRecPlant.Columns(ColRecPlantSNFKG).FieldName = "SNF_KG"
                End If



                qry = "select ROW_NUMBER() OVER(ORDER BY Trans_Type,Source_Doc_No) AS SNo,xxxx.*,case when Stock_Qty_KG>0 then cast((Fat_KG*100/Stock_Qty_KG) as decimal(18,2)) else 0 end FAT,case when Stock_Qty_KG>0 then cast((SNF_KG*100/Stock_Qty_KG) as decimal(18,2)) else 0 end SNF from (
select xxx.Trans_Type,xxx.Trans_Name,xxx.Source_Doc_No,xxx.Tanker_No, xxx.ReciveFrom,xxx.ReciveFromName,xxx.Item_Code,xxx.Item_Desc
,case when xxx.Stock_UOM='LTR' then xxx.Stock_Qty else cast(xxx.Stock_Qty/TabUOMLTR.Conversion_Factor as decimal(18,2)) end as Stock_Qty_LTR
,case when xxx.Stock_UOM='KG' then xxx.Stock_Qty else cast(xxx.Stock_Qty/TabUOMKG.Conversion_Factor as decimal(18,2)) end as Stock_Qty_KG
,xxx.Fat_KG,xxx.SNF_KG  from (
select TSPL_INVENTORY_MOVEMENT_NEW.Trans_Type, TSPL_INVENTORY_SOURCE_CODE.Name as  Trans_Name,TSPL_INVENTORY_MOVEMENT_NEW.Source_Doc_No
,(case when TSPL_INVENTORY_MOVEMENT_NEW.Trans_Type='BulkSRN' then TSPL_Bulk_MILK_SRN.Tanker_No else (case when TSPL_INVENTORY_MOVEMENT_NEW.Trans_Type='MilkTransferIn' then Tspl_Gate_Entry_Details.Tanker_No else '' end) end) as Tanker_No
,(case when TSPL_INVENTORY_MOVEMENT_NEW.Trans_Type='BulkSRN' then TSPL_Bulk_MILK_SRN.Vendor_Code else (case when TSPL_INVENTORY_MOVEMENT_NEW.Trans_Type='MilkTransferIn' then Tspl_Gate_Entry_Details.ROUTE_NO else '' end) end) as ReciveFrom
,(case when TSPL_INVENTORY_MOVEMENT_NEW.Trans_Type='BulkSRN' then TSPL_VENDOR_MASTER.Vendor_Name else (case when TSPL_INVENTORY_MOVEMENT_NEW.Trans_Type='MilkTransferIn' then TSPL_BULK_ROUTE_MASTER.ROUTE_NAME else '' end) end) as ReciveFromName
,TSPL_INVENTORY_MOVEMENT_NEW.Location_Code,TSPL_LOCATION_MASTER.Location_Desc,TSPL_INVENTORY_MOVEMENT_NEW.Item_Code,TSPL_ITEM_MASTER.Item_Desc,case when TSPL_INVENTORY_MOVEMENT_NEW.InOut='I' then 1 else -1 end as RI,TSPL_INVENTORY_MOVEMENT_NEW.Avg_Cost,TSPL_INVENTORY_MOVEMENT_NEW.Stock_Qty,TSPL_INVENTORY_MOVEMENT_NEW.Stock_UOM,
cast(TSPL_INVENTORY_MOVEMENT_NEW.Fat_KG as decimal(18,3)) as Fat_KG ,cast(TSPL_INVENTORY_MOVEMENT_NEW.SNF_KG as decimal(18,3)) as SNF_KG
from TSPL_INVENTORY_MOVEMENT_NEW 
left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_INVENTORY_MOVEMENT_NEW.Item_Code
left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_INVENTORY_MOVEMENT_NEW.Location_Code
left outer join TSPL_INVENTORY_SOURCE_CODE on TSPL_INVENTORY_SOURCE_CODE.Code=TSPL_INVENTORY_MOVEMENT_NEW.Trans_Type
left outer join TSPL_ADJUSTMENT_HEADER on TSPL_ADJUSTMENT_HEADER.Adjustment_No=TSPL_INVENTORY_MOVEMENT_NEW.Source_Doc_No  
left outer join TSPL_MILK_TRANSFER_IN on TSPL_MILK_TRANSFER_IN.Receipt_Challan_No=TSPL_INVENTORY_MOVEMENT_NEW.Source_Doc_No
left outer join Tspl_Gate_Entry_Details on Tspl_Gate_Entry_Details.Gate_Entry_No=TSPL_MILK_TRANSFER_IN.Gate_Entry_no
left outer join TSPL_BULK_ROUTE_MASTER on TSPL_BULK_ROUTE_MASTER.ROUTE_NO=Tspl_Gate_Entry_Details.ROUTE_NO
left outer join TSPL_Bulk_MILK_SRN on TSPL_Bulk_MILK_SRN.SRN_NO=TSPL_INVENTORY_MOVEMENT_NEW.Source_Doc_No
left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_Bulk_MILK_SRN.Vendor_Code
where TSPL_ITEM_MASTER.Product_Type='MI' and TSPL_LOCATION_MASTER.Main_Location_Code='" + txtLocation.Value + "' 
and Punching_Date>='" + clsCommon.GetPrintDate(ShiftFromDate, "dd/MMM/yyyy hh:mm:ss tt") + "' and Punching_Date<='" + clsCommon.GetPrintDate(ShiftToDate, "dd/MMM/yyyy hh:mm:ss tt") + "' and TSPL_INVENTORY_MOVEMENT_NEW.Stock_UOM in ('LTR','KG')
and 2= (case when TSPL_INVENTORY_MOVEMENT_NEW.Trans_Type in ('MilkTransferIn','BulkSRN') then 2 else case when TSPL_INVENTORY_MOVEMENT_NEW.Trans_Type ='IC-AD' and TSPL_ADJUSTMENT_HEADER.Adjustment_Type='FLG' then 2 else 3 end end )
) xxx 
left outer join TSPL_ITEM_UOM_DETAIL as TabUOMLTR on TabUOMLTR.Item_Code=xxx.Item_Code and TabUOMLTR.UOM_Code='LTR'
left outer join TSPL_ITEM_UOM_DETAIL as TabUOMKG on TabUOMKG.Item_Code=xxx.Item_Code and TabUOMKG.UOM_Code='KG'
where (xxx.Stock_Qty>0 and (xxx.Fat_KG>0 or xxx.SNF_KG>0))
) xxxx"
                dt = clsDBFuncationality.GetDataTable(qry)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    gvRecBulk.DataSource = Nothing
                    gvRecBulk.AutoGenerateColumns = False
                    gvRecBulk.DataSource = dt
                    gvRecBulk.Columns(ColRecBulkSNo).FieldName = "SNo"
                    gvRecBulk.Columns(ColRecBulkTranType).FieldName = "Trans_Type"
                    gvRecBulk.Columns(ColRecBulkTranTypeName).FieldName = "Trans_Name"
                    gvRecBulk.Columns(ColRecBulkTranNo).FieldName = "Source_Doc_No"
                    gvRecBulk.Columns(ColRecBulkTankerNo).FieldName = "Tanker_No"
                    gvRecBulk.Columns(ColRecBulkReciveFrom).FieldName = "ReciveFrom"
                    gvRecBulk.Columns(ColRecBulkReciveFromName).FieldName = "ReciveFromName"
                    gvRecBulk.Columns(ColRecBulkItemCode).FieldName = "Item_Code"
                    gvRecBulk.Columns(ColRecBulkItemName).FieldName = "Item_Desc"
                    gvRecBulk.Columns(ColRecBulkQtyLtr).FieldName = "Stock_Qty_LTR"
                    gvRecBulk.Columns(ColRecBulkQtyKG).FieldName = "Stock_Qty_KG"
                    gvRecBulk.Columns(ColRecBulkFAT).FieldName = "FAT"
                    gvRecBulk.Columns(ColRecBulkSNF).FieldName = "SNF"
                    gvRecBulk.Columns(ColRecBulkFATKG).FieldName = "Fat_KG"
                    gvRecBulk.Columns(ColRecBulkSNFKG).FieldName = "SNF_KG"
                End If
            Else
                LoadData(clsCommon.myCstr(dt.Rows(0)("Document_No")), NavigatorType.Current)
            End If
            'EnableDisableControl(False)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub EnableDisableControl(v As Boolean)
        txtDate.Enabled = v
        txtLocation.Enabled = v
        cboShift.Enabled = v
        RadButton1.Enabled = v
    End Sub
    Private Sub btnAddNew_Click(sender As Object, e As EventArgs) Handles btnAddNew.Click
        AddNew()
    End Sub
    Sub AddNew()
        btnSave.Enabled = True
        btnPost.Enabled = True
        btnDelete.Enabled = True
        txtDocNo.Value = ""
        txtDate.Value = clsCommon.GETSERVERDATE()
        txtComment.Text = ""
        txtLocation.Value = ""
        lblLocationFG.Text = ""
        'txtLocationRM.Value = ""
        'lblLocationRM.Text = ""
        'txtLocationPK.Value = ""
        'lblLocationPK.Text = ""
        'txtBatchNo.Text = ""
        'txtBatchDate.Value = txtDate.Value
        isNewEntry = True
        UsLock1.Status = ERPTransactionStatus.Pending
        LoadBlankGrid()
        LoadBlankGridProRM()
        gvPro.Rows.AddNew()
        EnableDisableControl(True)
        isCellValueChangedOpen = False
    End Sub
    Private Function GetCOBType() As DataTable
        Dim dt As New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))
        Dim dr As DataRow = dt.NewRow()
        dr("Code") = "0"
        dr("Name") = "-ve"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "1"
        dr("Name") = "+ve"
        dt.Rows.Add(dr)

        Return dt
    End Function
    Sub LoadBlankGrid()
        LoadBlankGridOP()
        LoadBlankGridRecPlant()
        LoadBlankGridRecBulk()
        LoadBlankGridPro()
        LoadBlankGridCL()
    End Sub
    Sub LoadBlankGridOP()
        gvOP.Columns.Clear()
        gvOP.DataSource = Nothing
        gvOP.Rows.Clear()
        Dim repoTextBox As New GridViewTextBoxColumn()
        repoTextBox.FormatString = ""
        repoTextBox.HeaderText = "PK ID"
        repoTextBox.Name = ColOPPKID
        repoTextBox.TextImageRelation = TextImageRelation.TextBeforeImage
        repoTextBox.Width = 80
        repoTextBox.ReadOnly = True
        repoTextBox.IsVisible = False
        gvOP.MasterTemplate.Columns.Add(repoTextBox)

        Dim repoNumBox As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.FormatString = ""
        repoNumBox.HeaderText = "SNo"
        repoNumBox.Name = ColOPSNo
        repoNumBox.Width = 40
        repoNumBox.ReadOnly = True
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvOP.MasterTemplate.Columns.Add(repoNumBox)

        repoTextBox = New GridViewTextBoxColumn()
        repoTextBox.FormatString = ""
        repoTextBox.HeaderText = "Location Code"
        repoTextBox.Name = ColOPLocationCode
        repoTextBox.HeaderImage = Global.XpertERPProcessProduction.My.Resources.Resources.search4
        repoTextBox.TextImageRelation = TextImageRelation.TextBeforeImage
        repoTextBox.Width = 100
        repoTextBox.ReadOnly = True
        repoTextBox.IsVisible = False
        gvOP.MasterTemplate.Columns.Add(repoTextBox)

        repoTextBox = New GridViewTextBoxColumn()
        repoTextBox.FormatString = ""
        repoTextBox.HeaderText = "Location"
        repoTextBox.Name = ColOPLocationName
        repoTextBox.Width = 120
        repoTextBox.ReadOnly = True
        gvOP.MasterTemplate.Columns.Add(repoTextBox)

        repoTextBox = New GridViewTextBoxColumn()
        repoTextBox.FormatString = ""
        repoTextBox.HeaderText = "Item Code"
        repoTextBox.Name = ColOPItemCode
        repoTextBox.HeaderImage = Global.XpertERPProcessProduction.My.Resources.Resources.search4
        repoTextBox.TextImageRelation = TextImageRelation.TextBeforeImage
        repoTextBox.Width = 100
        repoTextBox.ReadOnly = True
        repoTextBox.IsVisible = False
        gvOP.MasterTemplate.Columns.Add(repoTextBox)

        repoTextBox = New GridViewTextBoxColumn()
        repoTextBox.FormatString = ""
        repoTextBox.HeaderText = "Item"
        repoTextBox.Name = ColOPItemName
        repoTextBox.Width = 120
        repoTextBox.ReadOnly = True
        gvOP.MasterTemplate.Columns.Add(repoTextBox)

        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.FormatString = ""
        repoNumBox.HeaderText = "LTR Qty"
        repoNumBox.Name = ColOPQtyLtr
        repoNumBox.Width = 100
        repoNumBox.Minimum = 0
        repoNumBox.ShowUpDownButtons = False
        repoNumBox.Step = 0
        repoNumBox.DecimalPlaces = 2
        repoNumBox.ReadOnly = True
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvOP.MasterTemplate.Columns.Add(repoNumBox)

        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.FormatString = ""
        repoNumBox.HeaderText = "Kg Qty"
        repoNumBox.Name = ColOPQtyKG
        repoNumBox.Width = 80
        repoNumBox.Minimum = 0
        repoNumBox.ShowUpDownButtons = False
        repoNumBox.Step = 0
        repoNumBox.DecimalPlaces = 2
        repoNumBox.ReadOnly = True
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvOP.MasterTemplate.Columns.Add(repoNumBox)

        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.FormatString = ""
        repoNumBox.HeaderText = "FAT %"
        repoNumBox.Name = ColOPFAT
        repoNumBox.Width = 80
        repoNumBox.Minimum = 0
        repoNumBox.ShowUpDownButtons = False
        repoNumBox.Step = 0
        repoNumBox.DecimalPlaces = 2
        repoNumBox.ReadOnly = True
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvOP.MasterTemplate.Columns.Add(repoNumBox)

        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.FormatString = ""
        repoNumBox.HeaderText = "FAT KG"
        repoNumBox.Name = ColOPFATKG
        repoNumBox.Width = 80
        repoNumBox.Minimum = 0
        repoNumBox.ShowUpDownButtons = False
        repoNumBox.Step = 0
        repoNumBox.DecimalPlaces = 3
        repoNumBox.ReadOnly = True
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvOP.MasterTemplate.Columns.Add(repoNumBox)

        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.FormatString = ""
        repoNumBox.HeaderText = "SNF %"
        repoNumBox.Name = ColOPSNF
        repoNumBox.Width = 80
        repoNumBox.Minimum = 0
        repoNumBox.ShowUpDownButtons = False
        repoNumBox.Step = 0
        repoNumBox.DecimalPlaces = 2
        repoNumBox.ReadOnly = True
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvOP.MasterTemplate.Columns.Add(repoNumBox)

        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.FormatString = ""
        repoNumBox.HeaderText = "SNF KG"
        repoNumBox.Name = ColOPSNFKG
        repoNumBox.Width = 80
        repoNumBox.Minimum = 0
        repoNumBox.ShowUpDownButtons = False
        repoNumBox.Step = 0
        repoNumBox.DecimalPlaces = 3
        repoNumBox.ReadOnly = True
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvOP.MasterTemplate.Columns.Add(repoNumBox)


        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.FormatString = ""
        repoNumBox.HeaderText = "Temp."
        repoNumBox.Name = ColOPTemp
        repoNumBox.Width = 100
        repoNumBox.Minimum = 0
        repoNumBox.ShowUpDownButtons = False
        repoNumBox.Step = 0
        repoNumBox.DecimalPlaces = 2
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvOP.MasterTemplate.Columns.Add(repoNumBox)

        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.FormatString = ""
        repoNumBox.HeaderText = "Acidity"
        repoNumBox.Name = ColOPAcidity
        repoNumBox.Width = 100
        repoNumBox.Minimum = 0
        repoNumBox.ShowUpDownButtons = False
        repoNumBox.Step = 0
        repoNumBox.DecimalPlaces = 3
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvOP.MasterTemplate.Columns.Add(repoNumBox)

        Dim repoRowType As GridViewComboBoxColumn = New GridViewComboBoxColumn()
        repoRowType.FormatString = ""
        repoRowType.HeaderText = "MBRT COB"
        repoRowType.Name = ColOPCOB
        repoRowType.Width = 100
        repoRowType.ReadOnly = False
        repoRowType.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        repoRowType.DataSource = GetCOBType()
        repoRowType.ValueMember = "Code"
        repoRowType.DisplayMember = "Name"
        gvOP.MasterTemplate.Columns.Add(repoRowType) '2

        repoTextBox = New GridViewTextBoxColumn()
        repoTextBox.FormatString = ""
        repoTextBox.HeaderText = "Alcohol Test"
        repoTextBox.Name = ColOPAlcohol
        repoTextBox.Width = 100
        repoTextBox.ReadOnly = False
        gvOP.MasterTemplate.Columns.Add(repoTextBox)

        repoTextBox = New GridViewTextBoxColumn()
        repoTextBox.FormatString = ""
        repoTextBox.HeaderText = "Remarks"
        repoTextBox.Name = ColOPRemarks
        repoTextBox.Width = 150
        repoTextBox.ReadOnly = False
        gvOP.MasterTemplate.Columns.Add(repoTextBox)

        gvOP.AllowAddNewRow = False
        gvOP.ShowGroupPanel = False
        gvOP.AllowColumnReorder = True
        gvOP.AllowRowReorder = False
        gvOP.EnableSorting = False
        gvOP.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gvOP.MasterTemplate.ShowRowHeaderColumn = False
        gvOP.TableElement.TableHeaderHeight = 40

        gvOP.MasterTemplate.SummaryRowsBottom.Clear()




        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim item1 As New GridViewSummaryItem(ColOPQtyLtr, "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)
        item1 = New GridViewSummaryItem(ColOPQtyKG, "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)
        item1 = New GridViewSummaryItem(ColOPFATKG, "{0:F3}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)
        item1 = New GridViewSummaryItem(ColOPSNFKG, "{0:F3}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)
        gvOP.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        gvOP.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom

        gvOP.AllowDeleteRow = False
    End Sub
    Sub LoadBlankGridRecPlant()
        gvRecPlant.Columns.Clear()
        gvRecPlant.DataSource = Nothing
        gvRecPlant.Rows.Clear()
        Dim repoTextBox As New GridViewTextBoxColumn()
        repoTextBox.FormatString = ""
        repoTextBox.HeaderText = "PK ID"
        repoTextBox.Name = ColRecPlantPKID
        repoTextBox.TextImageRelation = TextImageRelation.TextBeforeImage
        repoTextBox.Width = 200
        repoTextBox.ReadOnly = True
        repoTextBox.IsVisible = False
        gvRecPlant.MasterTemplate.Columns.Add(repoTextBox)

        Dim repoNumBox As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.FormatString = ""
        repoNumBox.HeaderText = "SNo"
        repoNumBox.Name = ColRecPlantSNo
        repoNumBox.Width = 40
        repoNumBox.ReadOnly = True
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvRecPlant.MasterTemplate.Columns.Add(repoNumBox)

        repoTextBox = New GridViewTextBoxColumn()
        repoTextBox.FormatString = ""
        repoTextBox.HeaderText = "Shift"
        repoTextBox.Name = ColRecPlantShift
        repoTextBox.TextImageRelation = TextImageRelation.TextBeforeImage
        repoTextBox.Width = 100
        repoTextBox.ReadOnly = True
        gvRecPlant.MasterTemplate.Columns.Add(repoTextBox)

        repoTextBox = New GridViewTextBoxColumn()
        repoTextBox.FormatString = ""
        repoTextBox.HeaderText = "Type"
        repoTextBox.Name = ColRecPlantRejectType
        repoTextBox.Width = 100
        repoTextBox.ReadOnly = True
        gvRecPlant.MasterTemplate.Columns.Add(repoTextBox)

        repoTextBox = New GridViewTextBoxColumn()
        repoTextBox.FormatString = ""
        repoTextBox.HeaderText = "Item Code"
        repoTextBox.Name = ColRecPlantItemCode
        repoTextBox.HeaderImage = Global.XpertERPProcessProduction.My.Resources.Resources.search4
        repoTextBox.TextImageRelation = TextImageRelation.TextBeforeImage
        repoTextBox.Width = 100
        repoTextBox.ReadOnly = True
        repoTextBox.IsVisible = False
        gvRecPlant.MasterTemplate.Columns.Add(repoTextBox)

        repoTextBox = New GridViewTextBoxColumn()
        repoTextBox.FormatString = ""
        repoTextBox.HeaderText = "Item"
        repoTextBox.Name = ColRecPlantItemName
        repoTextBox.Width = 150
        repoTextBox.ReadOnly = True
        gvRecPlant.MasterTemplate.Columns.Add(repoTextBox)

        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.FormatString = ""
        repoNumBox.HeaderText = "LTR Qty"
        repoNumBox.Name = ColRecPlantQtyLtr
        repoNumBox.Width = 100
        repoNumBox.Minimum = 0
        repoNumBox.ShowUpDownButtons = False
        repoNumBox.Step = 0
        repoNumBox.DecimalPlaces = 2
        repoNumBox.ReadOnly = True
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvRecPlant.MasterTemplate.Columns.Add(repoNumBox)

        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.FormatString = ""
        repoNumBox.HeaderText = "Kg Qty"
        repoNumBox.Name = ColRecPlantQtyKG
        repoNumBox.Width = 100
        repoNumBox.Minimum = 0
        repoNumBox.ShowUpDownButtons = False
        repoNumBox.Step = 0
        repoNumBox.DecimalPlaces = 2
        repoNumBox.ReadOnly = True
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvRecPlant.MasterTemplate.Columns.Add(repoNumBox)

        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.FormatString = ""
        repoNumBox.HeaderText = "FAT %"
        repoNumBox.Name = ColRecPlantFAT
        repoNumBox.Width = 100
        repoNumBox.Minimum = 0
        repoNumBox.ShowUpDownButtons = False
        repoNumBox.Step = 0
        repoNumBox.DecimalPlaces = 2
        repoNumBox.ReadOnly = True
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvRecPlant.MasterTemplate.Columns.Add(repoNumBox)

        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.FormatString = ""
        repoNumBox.HeaderText = "FAT KG"
        repoNumBox.Name = ColRecPlantFATKG
        repoNumBox.Width = 100
        repoNumBox.Minimum = 0
        repoNumBox.ShowUpDownButtons = False
        repoNumBox.Step = 0
        repoNumBox.DecimalPlaces = 3
        repoNumBox.ReadOnly = True
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvRecPlant.MasterTemplate.Columns.Add(repoNumBox)

        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.FormatString = ""
        repoNumBox.HeaderText = "SNF %"
        repoNumBox.Name = ColRecPlantSNF
        repoNumBox.Width = 100
        repoNumBox.Minimum = 0
        repoNumBox.ShowUpDownButtons = False
        repoNumBox.Step = 0
        repoNumBox.DecimalPlaces = 2
        repoNumBox.ReadOnly = True
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvRecPlant.MasterTemplate.Columns.Add(repoNumBox)

        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.FormatString = ""
        repoNumBox.HeaderText = "SNF KG"
        repoNumBox.Name = ColRecPlantSNFKG
        repoNumBox.Width = 100
        repoNumBox.Minimum = 0
        repoNumBox.ShowUpDownButtons = False
        repoNumBox.Step = 0
        repoNumBox.DecimalPlaces = 3
        repoNumBox.ReadOnly = True
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvRecPlant.MasterTemplate.Columns.Add(repoNumBox)

        repoTextBox = New GridViewTextBoxColumn()
        repoTextBox.FormatString = ""
        repoTextBox.HeaderText = "Remarks"
        repoTextBox.Name = ColRecPlantRemarks
        repoTextBox.Width = 150
        repoTextBox.ReadOnly = False
        gvRecPlant.MasterTemplate.Columns.Add(repoTextBox)

        gvRecPlant.AllowAddNewRow = False
        gvRecPlant.ShowGroupPanel = False
        gvRecPlant.AllowColumnReorder = True
        gvRecPlant.AllowRowReorder = False
        gvRecPlant.EnableSorting = False
        gvRecPlant.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gvRecPlant.MasterTemplate.ShowRowHeaderColumn = False
        gvRecPlant.TableElement.TableHeaderHeight = 40

        gvRecPlant.AllowDeleteRow = True

        gvRecPlant.MasterTemplate.SummaryRowsBottom.Clear()




        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim item1 As New GridViewSummaryItem(ColRecPlantQtyLtr, "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)
        item1 = New GridViewSummaryItem(ColRecPlantQtyKG, "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)
        item1 = New GridViewSummaryItem(ColRecPlantFATKG, "{0:F3}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)
        item1 = New GridViewSummaryItem(ColRecPlantSNFKG, "{0:F3}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)
        gvRecPlant.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        gvRecPlant.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
    End Sub
    Sub LoadBlankGridRecBulk()
        gvRecBulk.Columns.Clear()
        gvRecBulk.DataSource = Nothing
        gvRecBulk.Rows.Clear()
        Dim repoTextBox As New GridViewTextBoxColumn()
        repoTextBox.FormatString = ""
        repoTextBox.HeaderText = "PK ID"
        repoTextBox.Name = ColRecBulkPKID
        repoTextBox.TextImageRelation = TextImageRelation.TextBeforeImage
        repoTextBox.Width = 200
        repoTextBox.ReadOnly = True
        repoTextBox.IsVisible = False
        gvRecBulk.MasterTemplate.Columns.Add(repoTextBox)

        Dim repoNumBox As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.FormatString = ""
        repoNumBox.HeaderText = "SNo"
        repoNumBox.Name = ColRecBulkSNo
        repoNumBox.Width = 40
        repoNumBox.ReadOnly = True
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvRecBulk.MasterTemplate.Columns.Add(repoNumBox)

        repoTextBox = New GridViewTextBoxColumn()
        repoTextBox.FormatString = ""
        repoTextBox.HeaderText = "Trans Type"
        repoTextBox.Name = ColRecBulkTranType
        repoTextBox.TextImageRelation = TextImageRelation.TextBeforeImage
        repoTextBox.Width = 100
        repoTextBox.ReadOnly = True
        repoTextBox.IsVisible = False
        gvRecBulk.MasterTemplate.Columns.Add(repoTextBox)

        repoTextBox = New GridViewTextBoxColumn()
        repoTextBox.FormatString = ""
        repoTextBox.HeaderText = "Transaction"
        repoTextBox.Name = ColRecBulkTranTypeName
        repoTextBox.TextImageRelation = TextImageRelation.TextBeforeImage
        repoTextBox.Width = 100
        repoTextBox.ReadOnly = True
        repoTextBox.IsVisible = True
        gvRecBulk.MasterTemplate.Columns.Add(repoTextBox)

        repoTextBox = New GridViewTextBoxColumn()
        repoTextBox.FormatString = ""
        repoTextBox.HeaderText = "Transaction No"
        repoTextBox.Name = ColRecBulkTranNo
        repoTextBox.Width = 150
        repoTextBox.ReadOnly = True
        repoTextBox.IsVisible = True
        gvRecBulk.MasterTemplate.Columns.Add(repoTextBox)

        repoTextBox = New GridViewTextBoxColumn()
        repoTextBox.FormatString = ""
        repoTextBox.HeaderText = "Tanker No"
        repoTextBox.Name = ColRecBulkTankerNo
        repoTextBox.Width = 150
        repoTextBox.ReadOnly = True
        repoTextBox.IsVisible = True
        gvRecBulk.MasterTemplate.Columns.Add(repoTextBox)

        repoTextBox = New GridViewTextBoxColumn()
        repoTextBox.FormatString = ""
        repoTextBox.HeaderText = "Receive Route/Vendor"
        repoTextBox.Name = ColRecBulkReciveFrom
        repoTextBox.Width = 100
        repoTextBox.ReadOnly = True
        repoTextBox.IsVisible = False
        gvRecBulk.MasterTemplate.Columns.Add(repoTextBox)

        repoTextBox = New GridViewTextBoxColumn()
        repoTextBox.FormatString = ""
        repoTextBox.HeaderText = "Receive From"
        repoTextBox.Name = ColRecBulkReciveFromName
        repoTextBox.Width = 100
        repoTextBox.ReadOnly = True
        repoTextBox.IsVisible = True
        gvRecBulk.MasterTemplate.Columns.Add(repoTextBox)

        repoTextBox = New GridViewTextBoxColumn()
        repoTextBox.FormatString = ""
        repoTextBox.HeaderText = "Item Code"
        repoTextBox.Name = ColRecBulkItemCode
        repoTextBox.HeaderImage = Global.XpertERPProcessProduction.My.Resources.Resources.search4
        repoTextBox.TextImageRelation = TextImageRelation.TextBeforeImage
        repoTextBox.Width = 100
        repoTextBox.ReadOnly = True
        repoTextBox.IsVisible = False
        gvRecBulk.MasterTemplate.Columns.Add(repoTextBox)

        repoTextBox = New GridViewTextBoxColumn()
        repoTextBox.FormatString = ""
        repoTextBox.HeaderText = "Item"
        repoTextBox.Name = ColRecBulkItemName
        repoTextBox.Width = 100
        repoTextBox.ReadOnly = True
        gvRecBulk.MasterTemplate.Columns.Add(repoTextBox)

        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.FormatString = ""
        repoNumBox.HeaderText = "LTR Qty"
        repoNumBox.Name = ColRecBulkQtyLtr
        repoNumBox.Width = 100
        repoNumBox.Minimum = 0
        repoNumBox.ShowUpDownButtons = False
        repoNumBox.Step = 0
        repoNumBox.DecimalPlaces = 0
        repoNumBox.ReadOnly = True
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvRecBulk.MasterTemplate.Columns.Add(repoNumBox)

        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.FormatString = ""
        repoNumBox.HeaderText = "Kg Qty"
        repoNumBox.Name = ColRecBulkQtyKG
        repoNumBox.Width = 100
        repoNumBox.Minimum = 0
        repoNumBox.ShowUpDownButtons = False
        repoNumBox.Step = 0
        repoNumBox.DecimalPlaces = 0
        repoNumBox.ReadOnly = True
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvRecBulk.MasterTemplate.Columns.Add(repoNumBox)

        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.FormatString = ""
        repoNumBox.HeaderText = "FAT %"
        repoNumBox.Name = ColRecBulkFAT
        repoNumBox.Width = 100
        repoNumBox.Minimum = 0
        repoNumBox.ShowUpDownButtons = False
        repoNumBox.Step = 0
        repoNumBox.DecimalPlaces = 0
        repoNumBox.ReadOnly = True
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvRecBulk.MasterTemplate.Columns.Add(repoNumBox)

        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.FormatString = ""
        repoNumBox.HeaderText = "FAT KG"
        repoNumBox.Name = ColRecBulkFATKG
        repoNumBox.Width = 100
        repoNumBox.Minimum = 0
        repoNumBox.ShowUpDownButtons = False
        repoNumBox.Step = 0
        repoNumBox.DecimalPlaces = 0
        repoNumBox.ReadOnly = True
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvRecBulk.MasterTemplate.Columns.Add(repoNumBox)

        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.FormatString = ""
        repoNumBox.HeaderText = "SNF %"
        repoNumBox.Name = ColRecBulkSNF
        repoNumBox.Width = 100
        repoNumBox.Minimum = 0
        repoNumBox.ShowUpDownButtons = False
        repoNumBox.Step = 0
        repoNumBox.DecimalPlaces = 0
        repoNumBox.ReadOnly = True
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvRecBulk.MasterTemplate.Columns.Add(repoNumBox)

        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.FormatString = ""
        repoNumBox.HeaderText = "SNF KG"
        repoNumBox.Name = ColRecBulkSNFKG
        repoNumBox.Width = 100
        repoNumBox.Minimum = 0
        repoNumBox.ShowUpDownButtons = False
        repoNumBox.Step = 0
        repoNumBox.DecimalPlaces = 0
        repoNumBox.ReadOnly = True
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvRecBulk.MasterTemplate.Columns.Add(repoNumBox)


        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.FormatString = ""
        repoNumBox.HeaderText = "Temp."
        repoNumBox.Name = ColRecBulkTemp
        repoNumBox.Width = 100
        repoNumBox.Minimum = 0
        repoNumBox.ShowUpDownButtons = False
        repoNumBox.Step = 0
        repoNumBox.DecimalPlaces = 2
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvRecBulk.MasterTemplate.Columns.Add(repoNumBox)

        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.FormatString = ""
        repoNumBox.HeaderText = "Acidity"
        repoNumBox.Name = ColRecBulkAcidity
        repoNumBox.Width = 100
        repoNumBox.Minimum = 0
        repoNumBox.ShowUpDownButtons = False
        repoNumBox.Step = 0
        repoNumBox.DecimalPlaces = 3
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvRecBulk.MasterTemplate.Columns.Add(repoNumBox)

        Dim repoRowType As GridViewComboBoxColumn = New GridViewComboBoxColumn()
        repoRowType.FormatString = ""
        repoRowType.HeaderText = "MBRT COB"
        repoRowType.Name = ColRecBulkCOB
        repoRowType.Width = 100
        repoRowType.ReadOnly = False
        repoRowType.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        repoRowType.DataSource = GetCOBType()
        repoRowType.ValueMember = "Code"
        repoRowType.DisplayMember = "Name"
        gvRecBulk.MasterTemplate.Columns.Add(repoRowType) '2

        repoTextBox = New GridViewTextBoxColumn()
        repoTextBox.FormatString = ""
        repoTextBox.HeaderText = "Alcohol Test"
        repoTextBox.Name = ColRecBulkAlcohol
        repoTextBox.Width = 100
        repoTextBox.ReadOnly = False
        gvRecBulk.MasterTemplate.Columns.Add(repoTextBox)

        repoTextBox = New GridViewTextBoxColumn()
        repoTextBox.FormatString = ""
        repoTextBox.HeaderText = "Remarks"
        repoTextBox.Name = ColRecBulkRemarks
        repoTextBox.Width = 150
        repoTextBox.ReadOnly = False
        gvRecBulk.MasterTemplate.Columns.Add(repoTextBox)

        gvRecBulk.AllowAddNewRow = False
        gvRecBulk.ShowGroupPanel = False
        gvRecBulk.AllowColumnReorder = True
        gvRecBulk.AllowRowReorder = False
        gvRecBulk.EnableSorting = False
        gvRecBulk.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gvRecBulk.MasterTemplate.ShowRowHeaderColumn = False
        gvRecBulk.TableElement.TableHeaderHeight = 40

        gvRecBulk.AllowDeleteRow = True

        gvRecBulk.MasterTemplate.SummaryRowsBottom.Clear()




        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim item1 As New GridViewSummaryItem(ColRecBulkQtyLtr, "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)
        item1 = New GridViewSummaryItem(ColRecBulkQtyKG, "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)
        item1 = New GridViewSummaryItem(ColRecBulkFATKG, "{0:F3}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)
        item1 = New GridViewSummaryItem(ColRecBulkSNFKG, "{0:F3}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)
        gvRecBulk.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        gvRecBulk.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
    End Sub
    Sub LoadBlankGridPro()
        gvPro.Columns.Clear()
        gvPro.DataSource = Nothing
        gvPro.Rows.Clear()



        Dim repoTextBox As New GridViewTextBoxColumn()
        repoTextBox.FormatString = ""
        repoTextBox.HeaderText = "PK ID"
        repoTextBox.Name = ColProPKID
        repoTextBox.TextImageRelation = TextImageRelation.TextBeforeImage
        repoTextBox.Width = 200
        repoTextBox.ReadOnly = True
        repoTextBox.IsVisible = False
        gvPro.MasterTemplate.Columns.Add(repoTextBox)

        Dim repoNumBox As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.FormatString = ""
        repoNumBox.HeaderText = "SNo"
        repoNumBox.Name = ColProSNo
        repoNumBox.Width = 40
        repoNumBox.ReadOnly = True
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvPro.MasterTemplate.Columns.Add(repoNumBox)

        repoTextBox = New GridViewTextBoxColumn()
        repoTextBox.FormatString = ""
        repoTextBox.HeaderText = "Item Code"
        repoTextBox.Name = ColProItemCode
        repoTextBox.HeaderImage = Global.XpertERPProcessProduction.My.Resources.Resources.search4
        repoTextBox.TextImageRelation = TextImageRelation.TextBeforeImage
        repoTextBox.Width = 100
        repoTextBox.IsVisible = True
        gvPro.MasterTemplate.Columns.Add(repoTextBox)

        repoTextBox = New GridViewTextBoxColumn()
        repoTextBox.FormatString = ""
        repoTextBox.HeaderText = "Item"
        repoTextBox.Name = ColProItemName
        repoTextBox.Width = 150
        repoTextBox.ReadOnly = True
        gvPro.MasterTemplate.Columns.Add(repoTextBox)

        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.FormatString = ""
        repoNumBox.HeaderText = "Qty LTR"
        repoNumBox.Name = ColProQtyLTR
        repoNumBox.Width = 80
        repoNumBox.Minimum = 0
        repoNumBox.ShowUpDownButtons = False
        repoNumBox.Step = 0
        repoNumBox.DecimalPlaces = 2
        repoNumBox.ReadOnly = False
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvPro.MasterTemplate.Columns.Add(repoNumBox)

        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.FormatString = ""
        repoNumBox.HeaderText = "Qty KG"
        repoNumBox.Name = ColProQtyKG
        repoNumBox.Width = 80
        repoNumBox.Minimum = 0
        repoNumBox.ShowUpDownButtons = False
        repoNumBox.Step = 0
        repoNumBox.DecimalPlaces = 2
        repoNumBox.ReadOnly = False
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvPro.MasterTemplate.Columns.Add(repoNumBox)



        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.FormatString = ""
        repoNumBox.HeaderText = "FAT %"
        repoNumBox.Name = ColProFAT
        repoNumBox.Width = 100
        repoNumBox.Minimum = 0
        repoNumBox.ShowUpDownButtons = False
        repoNumBox.Step = 0
        repoNumBox.DecimalPlaces = 0
        repoNumBox.ReadOnly = True
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvPro.MasterTemplate.Columns.Add(repoNumBox)

        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.FormatString = ""
        repoNumBox.HeaderText = "FAT KG"
        repoNumBox.Name = ColProFATKG
        repoNumBox.Width = 100
        repoNumBox.Minimum = 0
        repoNumBox.ShowUpDownButtons = False
        repoNumBox.Step = 0
        repoNumBox.DecimalPlaces = 3
        repoNumBox.ReadOnly = True
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvPro.MasterTemplate.Columns.Add(repoNumBox)

        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.FormatString = ""
        repoNumBox.HeaderText = "SNF %"
        repoNumBox.Name = ColProSNF
        repoNumBox.Width = 100
        repoNumBox.Minimum = 0
        repoNumBox.ShowUpDownButtons = False
        repoNumBox.Step = 0
        repoNumBox.DecimalPlaces = 2
        repoNumBox.ReadOnly = True
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvPro.MasterTemplate.Columns.Add(repoNumBox)

        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.FormatString = ""
        repoNumBox.HeaderText = "SNF KG"
        repoNumBox.Name = ColProSNFKG
        repoNumBox.Width = 100
        repoNumBox.Minimum = 0
        repoNumBox.ShowUpDownButtons = False
        repoNumBox.Step = 0
        repoNumBox.DecimalPlaces = 3
        repoNumBox.ReadOnly = True
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvPro.MasterTemplate.Columns.Add(repoNumBox)

        Dim ShowBtn As New GridViewCommandColumn()
        ShowBtn.FormatString = ""
        ShowBtn.UseDefaultText = True
        ShowBtn.DefaultText = "Add"
        ShowBtn.HeaderText = ""
        ShowBtn.Name = ColProAdd
        ShowBtn.FieldName = ColProAdd
        ShowBtn.Width = 80
        ShowBtn.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvPro.MasterTemplate.Columns.Add(ShowBtn)

        ShowBtn = New GridViewCommandColumn()
        ShowBtn.FormatString = ""
        ShowBtn.UseDefaultText = True
        ShowBtn.DefaultText = "Remove"
        ShowBtn.HeaderText = ""
        ShowBtn.Name = ColProRemove
        ShowBtn.FieldName = ColProRemove
        ShowBtn.Width = 80
        ShowBtn.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvPro.MasterTemplate.Columns.Add(ShowBtn)

        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.FormatString = ""
        repoNumBox.HeaderText = "Temp."
        repoNumBox.Name = ColProTemp
        repoNumBox.Width = 100
        repoNumBox.Minimum = 0
        repoNumBox.ShowUpDownButtons = False
        repoNumBox.Step = 0
        repoNumBox.DecimalPlaces = 2
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvPro.MasterTemplate.Columns.Add(repoNumBox)

        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.FormatString = ""
        repoNumBox.HeaderText = "Acidity"
        repoNumBox.Name = ColProAcidity
        repoNumBox.Width = 100
        repoNumBox.Minimum = 0
        repoNumBox.ShowUpDownButtons = False
        repoNumBox.Step = 0
        repoNumBox.DecimalPlaces = 3
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvPro.MasterTemplate.Columns.Add(repoNumBox)

        Dim repoRowType As GridViewComboBoxColumn = New GridViewComboBoxColumn()
        repoRowType.FormatString = ""
        repoRowType.HeaderText = "MBRT COB"
        repoRowType.Name = ColProCOB
        repoRowType.Width = 100
        repoRowType.ReadOnly = False
        repoRowType.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        repoRowType.DataSource = GetCOBType()
        repoRowType.ValueMember = "Code"
        repoRowType.DisplayMember = "Name"
        gvPro.MasterTemplate.Columns.Add(repoRowType) '2

        repoTextBox = New GridViewTextBoxColumn()
        repoTextBox.FormatString = ""
        repoTextBox.HeaderText = "Alcohol Test"
        repoTextBox.Name = ColProAlcohol
        repoTextBox.Width = 100
        repoTextBox.ReadOnly = False
        gvPro.MasterTemplate.Columns.Add(repoTextBox)

        repoTextBox = New GridViewTextBoxColumn()
        repoTextBox.FormatString = ""
        repoTextBox.HeaderText = "Remarks"
        repoTextBox.Name = ColProRemarks
        repoTextBox.Width = 150
        repoTextBox.ReadOnly = False
        gvPro.MasterTemplate.Columns.Add(repoTextBox)


        repoNumBox = New GridViewDecimalColumn()
        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.FormatString = ""
        repoNumBox.HeaderText = "Entered UOM"
        repoNumBox.Name = ColProEnteredUOM
        repoNumBox.Width = 40
        repoNumBox.ReadOnly = True
        repoNumBox.IsVisible = False
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvPro.MasterTemplate.Columns.Add(repoNumBox)

        repoTextBox = New GridViewTextBoxColumn()
        repoTextBox.FormatString = ""
        repoTextBox.HeaderText = "BOM Code"
        repoTextBox.Name = ColProBOMCode
        repoTextBox.Width = 100
        repoTextBox.ReadOnly = True
        repoTextBox.IsVisible = False
        gvPro.MasterTemplate.Columns.Add(repoTextBox)

        gvPro.AllowAddNewRow = False
        gvPro.ShowGroupPanel = False
        gvPro.AllowColumnReorder = True
        gvPro.AllowRowReorder = False
        gvPro.EnableSorting = False
        gvPro.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gvPro.MasterTemplate.ShowRowHeaderColumn = False
        gvPro.TableElement.TableHeaderHeight = 40

        gvPro.AllowDeleteRow = True

        gvPro.Rows.AddNew()


        gvPro.MasterTemplate.SummaryRowsBottom.Clear()




        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim item1 As New GridViewSummaryItem(ColProQtyKG, "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)
        item1 = New GridViewSummaryItem(ColProQtyLTR, "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)
        item1 = New GridViewSummaryItem(ColProFATKG, "{0:F3}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)
        item1 = New GridViewSummaryItem(ColProSNFKG, "{0:F3}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)
        gvPro.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        gvPro.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
    End Sub
    Sub LoadBlankGridProRM()
        gvProRM.Columns.Clear()
        gvProRM.DataSource = Nothing
        gvProRM.Rows.Clear()

        Dim repoTextBox As New GridViewTextBoxColumn()
        repoTextBox.FormatString = ""
        repoTextBox.HeaderText = "PK ID"
        repoTextBox.Name = ColProRMPKID
        repoTextBox.TextImageRelation = TextImageRelation.TextBeforeImage
        repoTextBox.Width = 200
        repoTextBox.ReadOnly = True
        repoTextBox.IsVisible = False
        gvProRM.MasterTemplate.Columns.Add(repoTextBox)

        Dim repoNumBox As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.FormatString = ""
        repoNumBox.HeaderText = "SNo"
        repoNumBox.Name = ColProRMSNo
        repoNumBox.Width = 40
        repoNumBox.ReadOnly = True
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvProRM.MasterTemplate.Columns.Add(repoNumBox)

        repoTextBox = New GridViewTextBoxColumn()
        repoTextBox.FormatString = ""
        repoTextBox.HeaderText = "Item Code"
        repoTextBox.Name = ColProRMItemCode
        repoTextBox.HeaderImage = Global.XpertERPProcessProduction.My.Resources.Resources.search4
        repoTextBox.TextImageRelation = TextImageRelation.TextBeforeImage
        repoTextBox.Width = 100
        repoTextBox.IsVisible = True
        repoTextBox.ReadOnly = True
        gvProRM.MasterTemplate.Columns.Add(repoTextBox)

        repoTextBox = New GridViewTextBoxColumn()
        repoTextBox.FormatString = ""
        repoTextBox.HeaderText = "Item"
        repoTextBox.Name = ColProRMItemName
        repoTextBox.Width = 150
        repoTextBox.ReadOnly = True
        gvProRM.MasterTemplate.Columns.Add(repoTextBox)

        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.FormatString = ""
        repoNumBox.HeaderText = "Qty"
        repoNumBox.Name = ColProRMQty
        repoNumBox.Width = 80
        repoNumBox.Minimum = 0
        repoNumBox.ShowUpDownButtons = False
        repoNumBox.Step = 0
        repoNumBox.DecimalPlaces = 2
        repoNumBox.ReadOnly = True
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvProRM.MasterTemplate.Columns.Add(repoNumBox)

        repoTextBox = New GridViewTextBoxColumn()
        repoTextBox.FormatString = ""
        repoTextBox.HeaderText = "UOM"
        repoTextBox.Name = ColProRMUOM
        repoTextBox.Width = 100
        repoTextBox.IsVisible = True
        repoTextBox.ReadOnly = True
        gvProRM.MasterTemplate.Columns.Add(repoTextBox)

        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.FormatString = ""
        repoNumBox.HeaderText = "FAT %"
        repoNumBox.Name = ColProRMFAT
        repoNumBox.Width = 100
        repoNumBox.Minimum = 0
        repoNumBox.ShowUpDownButtons = False
        repoNumBox.Step = 0
        repoNumBox.DecimalPlaces = 0
        repoNumBox.ReadOnly = True
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvProRM.MasterTemplate.Columns.Add(repoNumBox)

        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.FormatString = ""
        repoNumBox.HeaderText = "FAT KG"
        repoNumBox.Name = ColProRMFATKG
        repoNumBox.Width = 100
        repoNumBox.Minimum = 0
        repoNumBox.ShowUpDownButtons = False
        repoNumBox.Step = 0
        repoNumBox.DecimalPlaces = 3
        repoNumBox.ReadOnly = True
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvProRM.MasterTemplate.Columns.Add(repoNumBox)

        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.FormatString = ""
        repoNumBox.HeaderText = "SNF %"
        repoNumBox.Name = ColProRMSNF
        repoNumBox.Width = 100
        repoNumBox.Minimum = 0
        repoNumBox.ShowUpDownButtons = False
        repoNumBox.Step = 0
        repoNumBox.DecimalPlaces = 2
        repoNumBox.ReadOnly = True
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvProRM.MasterTemplate.Columns.Add(repoNumBox)

        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.FormatString = ""
        repoNumBox.HeaderText = "SNF KG"
        repoNumBox.Name = ColProRMSNFKG
        repoNumBox.Width = 100
        repoNumBox.Minimum = 0
        repoNumBox.ShowUpDownButtons = False
        repoNumBox.Step = 0
        repoNumBox.DecimalPlaces = 3
        repoNumBox.ReadOnly = True
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvProRM.MasterTemplate.Columns.Add(repoNumBox)

        Dim ShowBtn As New GridViewCommandColumn()
        ShowBtn.FormatString = ""
        ShowBtn.UseDefaultText = True
        ShowBtn.DefaultText = "Issue Items"
        ShowBtn.HeaderText = ""
        ShowBtn.Name = ColProRMIssue
        ShowBtn.FieldName = ColProRMIssue
        ShowBtn.Width = 80
        ShowBtn.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvProRM.MasterTemplate.Columns.Add(ShowBtn)

        gvProRM.AllowAddNewRow = False
        gvProRM.ShowGroupPanel = False
        gvProRM.AllowColumnReorder = True
        gvProRM.AllowRowReorder = False
        gvProRM.EnableSorting = False
        gvProRM.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gvProRM.MasterTemplate.ShowRowHeaderColumn = False
        gvProRM.TableElement.TableHeaderHeight = 40

        gvProRM.AllowDeleteRow = False

        gvProRM.MasterTemplate.SummaryRowsBottom.Clear()

        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim item1 As New GridViewSummaryItem(ColProRMFATKG, "{0:F3}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)
        item1 = New GridViewSummaryItem(ColProRMSNFKG, "{0:F3}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)
        gvProRM.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        gvProRM.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
    End Sub
    Sub LoadBlankGridCL()
        gvCL.Columns.Clear()
        gvCL.DataSource = Nothing
        gvCL.Rows.Clear()
        Dim repoTextBox As New GridViewTextBoxColumn()
        repoTextBox.FormatString = ""
        repoTextBox.HeaderText = "PK ID"
        repoTextBox.Name = colCLPKID
        repoTextBox.TextImageRelation = TextImageRelation.TextBeforeImage
        repoTextBox.Width = 200
        repoTextBox.ReadOnly = True
        repoTextBox.IsVisible = False
        gvCL.MasterTemplate.Columns.Add(repoTextBox)

        Dim repoNumBox As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.FormatString = ""
        repoNumBox.HeaderText = "SNo"
        repoNumBox.Name = colCLSNo
        repoNumBox.Width = 40
        repoNumBox.ReadOnly = True
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvCL.MasterTemplate.Columns.Add(repoNumBox)

        repoTextBox = New GridViewTextBoxColumn()
        repoTextBox.FormatString = ""
        repoTextBox.HeaderText = "Location Code"
        repoTextBox.Name = colCLLocationCode
        repoTextBox.HeaderImage = Global.XpertERPProcessProduction.My.Resources.Resources.search4
        repoTextBox.TextImageRelation = TextImageRelation.TextBeforeImage
        repoTextBox.Width = 100
        repoTextBox.IsVisible = False
        gvCL.MasterTemplate.Columns.Add(repoTextBox)

        repoTextBox = New GridViewTextBoxColumn()
        repoTextBox.FormatString = ""
        repoTextBox.HeaderText = "Location"
        repoTextBox.Name = colCLLocationName
        repoTextBox.Width = 150
        repoTextBox.ReadOnly = True
        gvCL.MasterTemplate.Columns.Add(repoTextBox)

        repoTextBox = New GridViewTextBoxColumn()
        repoTextBox.FormatString = ""
        repoTextBox.HeaderText = "Item Code"
        repoTextBox.Name = colCLItemCode
        repoTextBox.HeaderImage = Global.XpertERPProcessProduction.My.Resources.Resources.search4
        repoTextBox.TextImageRelation = TextImageRelation.TextBeforeImage
        repoTextBox.Width = 100
        repoTextBox.ReadOnly = True
        repoTextBox.IsVisible = False
        gvCL.MasterTemplate.Columns.Add(repoTextBox)

        repoTextBox = New GridViewTextBoxColumn()
        repoTextBox.FormatString = ""
        repoTextBox.HeaderText = "Item Name"
        repoTextBox.Name = colCLItemName
        repoTextBox.Width = 150
        repoTextBox.ReadOnly = True
        gvCL.MasterTemplate.Columns.Add(repoTextBox)


        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.FormatString = ""
        repoNumBox.HeaderText = "OP LTR Qty"
        repoNumBox.Name = colCLOPQtyLtr
        repoNumBox.Width = 80
        repoNumBox.Minimum = 0
        repoNumBox.ShowUpDownButtons = False
        repoNumBox.Step = 0
        repoNumBox.DecimalPlaces = 0
        repoNumBox.ReadOnly = True
        repoNumBox.IsVisible = False
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvCL.MasterTemplate.Columns.Add(repoNumBox)

        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.FormatString = ""
        repoNumBox.HeaderText = "OP Kg Qty"
        repoNumBox.Name = colCLOPQtyKG
        repoNumBox.Width = 80
        repoNumBox.Minimum = 0
        repoNumBox.ShowUpDownButtons = False
        repoNumBox.Step = 0
        repoNumBox.DecimalPlaces = 0
        repoNumBox.ReadOnly = True
        repoNumBox.IsVisible = False
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvCL.MasterTemplate.Columns.Add(repoNumBox)

        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.FormatString = ""
        repoNumBox.HeaderText = "OP FAT %"
        repoNumBox.Name = colCLOPFAT
        repoNumBox.Width = 80
        repoNumBox.Minimum = 0
        repoNumBox.ShowUpDownButtons = False
        repoNumBox.Step = 0
        repoNumBox.DecimalPlaces = 0
        repoNumBox.ReadOnly = True
        repoNumBox.IsVisible = False
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvCL.MasterTemplate.Columns.Add(repoNumBox)

        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.FormatString = ""
        repoNumBox.HeaderText = "OP FAT KG"
        repoNumBox.Name = colCLOPFATKG
        repoNumBox.Width = 80
        repoNumBox.Minimum = 0
        repoNumBox.ShowUpDownButtons = False
        repoNumBox.Step = 0
        repoNumBox.DecimalPlaces = 0
        repoNumBox.ReadOnly = True
        repoNumBox.IsVisible = False
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvCL.MasterTemplate.Columns.Add(repoNumBox)

        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.FormatString = ""
        repoNumBox.HeaderText = "OP SNF %"
        repoNumBox.Name = colCLOPSNF
        repoNumBox.Width = 80
        repoNumBox.Minimum = 0
        repoNumBox.ShowUpDownButtons = False
        repoNumBox.Step = 0
        repoNumBox.DecimalPlaces = 0
        repoNumBox.ReadOnly = True
        repoNumBox.IsVisible = False
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvCL.MasterTemplate.Columns.Add(repoNumBox)

        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.FormatString = ""
        repoNumBox.HeaderText = "OP SNF KG"
        repoNumBox.Name = colCLOPSNFKG
        repoNumBox.Width = 80
        repoNumBox.Minimum = 0
        repoNumBox.ShowUpDownButtons = False
        repoNumBox.Step = 0
        repoNumBox.DecimalPlaces = 0
        repoNumBox.ReadOnly = True
        repoNumBox.IsVisible = False
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvCL.MasterTemplate.Columns.Add(repoNumBox)


        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.FormatString = ""
        repoNumBox.HeaderText = "LTR Qty"
        repoNumBox.Name = colCLQtyLtr
        repoNumBox.Width = 80
        repoNumBox.Minimum = 0
        repoNumBox.ShowUpDownButtons = False
        repoNumBox.Step = 0
        repoNumBox.DecimalPlaces = 0
        repoNumBox.ReadOnly = True
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvCL.MasterTemplate.Columns.Add(repoNumBox)

        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.FormatString = ""
        repoNumBox.HeaderText = "Kg Qty"
        repoNumBox.Name = colCLQtyKG
        repoNumBox.Width = 80
        repoNumBox.Minimum = 0
        repoNumBox.ShowUpDownButtons = False
        repoNumBox.Step = 0
        repoNumBox.DecimalPlaces = 0
        repoNumBox.ReadOnly = True
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvCL.MasterTemplate.Columns.Add(repoNumBox)

        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.FormatString = ""
        repoNumBox.HeaderText = "FAT %"
        repoNumBox.Name = colCLFAT
        repoNumBox.Width = 80
        repoNumBox.Minimum = 0
        repoNumBox.ShowUpDownButtons = False
        repoNumBox.Step = 0
        repoNumBox.DecimalPlaces = 0
        repoNumBox.ReadOnly = True
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvCL.MasterTemplate.Columns.Add(repoNumBox)

        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.FormatString = ""
        repoNumBox.HeaderText = "FAT KG"
        repoNumBox.Name = colCLFATKG
        repoNumBox.Width = 80
        repoNumBox.Minimum = 0
        repoNumBox.ShowUpDownButtons = False
        repoNumBox.Step = 0
        repoNumBox.DecimalPlaces = 0
        repoNumBox.ReadOnly = True
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvCL.MasterTemplate.Columns.Add(repoNumBox)

        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.FormatString = ""
        repoNumBox.HeaderText = "SNF %"
        repoNumBox.Name = colCLSNF
        repoNumBox.Width = 80
        repoNumBox.Minimum = 0
        repoNumBox.ShowUpDownButtons = False
        repoNumBox.Step = 0
        repoNumBox.DecimalPlaces = 0
        repoNumBox.ReadOnly = True
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvCL.MasterTemplate.Columns.Add(repoNumBox)

        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.FormatString = ""
        repoNumBox.HeaderText = "SNF KG"
        repoNumBox.Name = colCLSNFKG
        repoNumBox.Width = 80
        repoNumBox.Minimum = 0
        repoNumBox.ShowUpDownButtons = False
        repoNumBox.Step = 0
        repoNumBox.DecimalPlaces = 0
        repoNumBox.ReadOnly = True
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvCL.MasterTemplate.Columns.Add(repoNumBox)


        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.FormatString = ""
        repoNumBox.HeaderText = "Temp."
        repoNumBox.Name = colCLTemp
        repoNumBox.Width = 80
        repoNumBox.Minimum = 0
        repoNumBox.ShowUpDownButtons = False
        repoNumBox.Step = 0
        repoNumBox.DecimalPlaces = 2
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvCL.MasterTemplate.Columns.Add(repoNumBox)

        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.FormatString = ""
        repoNumBox.HeaderText = "Acidity"
        repoNumBox.Name = colCLAcidity
        repoNumBox.Width = 100
        repoNumBox.Minimum = 0
        repoNumBox.ShowUpDownButtons = False
        repoNumBox.Step = 0
        repoNumBox.DecimalPlaces = 3
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvCL.MasterTemplate.Columns.Add(repoNumBox)

        Dim repoRowType As GridViewComboBoxColumn = New GridViewComboBoxColumn()
        repoRowType.FormatString = ""
        repoRowType.HeaderText = "MBRT COB"
        repoRowType.Name = colCLCOB
        repoRowType.Width = 100
        repoRowType.ReadOnly = False
        repoRowType.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        repoRowType.DataSource = GetCOBType()
        repoRowType.ValueMember = "Code"
        repoRowType.DisplayMember = "Name"

        gvCL.MasterTemplate.Columns.Add(repoRowType) '2

        repoTextBox = New GridViewTextBoxColumn()
        repoTextBox.FormatString = ""
        repoTextBox.HeaderText = "Alcohol Test"
        repoTextBox.Name = colCLAlcohol
        repoTextBox.Width = 100
        repoTextBox.ReadOnly = False
        gvCL.MasterTemplate.Columns.Add(repoTextBox)

        repoTextBox = New GridViewTextBoxColumn()
        repoTextBox.FormatString = ""
        repoTextBox.HeaderText = "Remarks"
        repoTextBox.Name = colCLRemarks
        repoTextBox.Width = 150
        repoTextBox.ReadOnly = False
        gvCL.MasterTemplate.Columns.Add(repoTextBox)

        gvCL.AllowAddNewRow = False
        gvCL.ShowGroupPanel = False
        gvCL.AllowColumnReorder = True
        gvCL.AllowRowReorder = False
        gvCL.EnableSorting = False
        gvCL.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gvCL.MasterTemplate.ShowRowHeaderColumn = False
        gvCL.TableElement.TableHeaderHeight = 40

        gvCL.AllowDeleteRow = False


        gvCL.MasterTemplate.SummaryRowsBottom.Clear()




        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim item1 As New GridViewSummaryItem(colCLOPQtyLtr, "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)
        item1 = New GridViewSummaryItem(colCLOPQtyKG, "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)
        item1 = New GridViewSummaryItem(colCLOPFATKG, "{0:F3}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)
        item1 = New GridViewSummaryItem(colCLOPSNFKG, "{0:F3}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)
        item1 = New GridViewSummaryItem(colCLQtyLtr, "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)
        item1 = New GridViewSummaryItem(colCLQtyKG, "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)
        item1 = New GridViewSummaryItem(colCLFATKG, "{0:F3}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)
        item1 = New GridViewSummaryItem(colCLSNFKG, "{0:F3}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)
        gvCL.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        gvCL.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
    End Sub
    Private Sub gv1_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvPro.CellValueChanged
        Try
            If (Not isInsideLoadData) Then
                If Not isCellValueChangedOpen Then
                    isCellValueChangedOpen = True
                    If e.Column Is gvPro.Columns(ColProItemCode) Then
                        OpenItem(False)
                    ElseIf e.Column Is gvPro.Columns(ColProQtyLTR) Then
                        CalculateQty(True)
                    ElseIf e.Column Is gvPro.Columns(ColProQtyKG) Then
                        CalculateQty(False)
                    End If
                    isCellValueChangedOpen = False
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub OpenItem(ByVal isButtonClicked As Boolean)
        gvPro.CurrentRow.Cells(ColProItemCode).Value = clsItemMaster.getFinder(" tspl_item_master.item_type in ('F') and tspl_item_master.Active='1' ", clsCommon.myCstr(gvPro.CurrentRow.Cells(ColProItemCode).Value), isButtonClicked)
        gvPro.CurrentRow.Cells(ColProItemName).Value = clsItemMaster.GetItemName(clsCommon.myCstr(gvPro.CurrentRow.Cells(ColProItemCode).Value), Nothing)
    End Sub
    Sub CalculateQty(ByVal FromLTR As Boolean)
        If clsCommon.myLen(gvPro.CurrentRow.Cells(ColProItemCode).Value) <= 0 Then
            clsCommon.MyMessageBoxShow("Please first select item.")
            Exit Sub
        End If
        Dim strColumn As String = ColProQtyKG
        Dim strUOM As String = "KG"
        Try
            If FromLTR Then
                strColumn = ColProQtyLTR
                strUOM = "LTR"
            End If
            Dim qry As String = "select 1 from TSPL_ITEM_UOM_DETAIL where item_code='" + clsCommon.myCstr(gvPro.CurrentRow.Cells(ColProItemCode).Value) + "' and UOM_Code='" + strUOM + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                Throw New Exception(strUOM + " is invalid UOM for Item [" + clsCommon.myCstr(gvPro.CurrentRow.Cells(ColProItemCode).Value) + "]")
            End If

            qry = "select top 1 BOM_CODE from TSPL_PP_BOM_HEAD 
        where isnull(TSPL_PP_BOM_HEAD.is_osp,0)<>1 and TSPL_PP_BOM_HEAD.prod_item_code='" + clsCommon.myCstr(gvPro.CurrentRow.Cells(ColProItemCode).Value) + "'  and '" + clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") + "' between cast(TSPL_PP_BOM_HEAD.Valid_FROM_DATE as date) and cast(TSPL_PP_BOM_HEAD.Valid_UPTO_Date as date) order by BOM_CODE desc"
            dt = clsDBFuncationality.GetDataTable(qry)
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                Throw New Exception("BOM Not Found for Item [" + clsCommon.myCstr(gvPro.CurrentRow.Cells(ColProItemCode).Value) + "] and Date [" + clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") + "]")
            End If
            gvPro.CurrentRow.Cells(ColProBOMCode).Value = clsCommon.myCstr(dt.Rows(0)("BOM_CODE"))

            qry = "select xx.ITEM_CODE,xx.Item_Desc,xx.Item_Type,xx.UNIT_CODE,xx.Product_Type,(xx.prod_qty * (xx.quantity/xx.build_qty)) as Qty,xx.fat,xx.snf,xx.fat_kg,xx.snf_kg from (
select  (" + clsCommon.myCstr(clsCommon.myCDecimal(gvPro.CurrentRow.Cells(strColumn).Value)) + " * TabConvFatMul.Conversion_Factor/ TabConvFatDiv.Conversion_Factor) as Prod_Qty,tspl_pp_bom_head.bom_code,tspl_pp_bom_head.prod_item_code,tspl_pp_bom_head.prod_quantity as build_qty,TSPL_PP_BOM_ITEM_DETAIL.deactive,TSPL_PP_BOM_ITEM_DETAIL.effective_date
,TSPL_PP_BOM_ITEM_DETAIL.ITEM_CODE,TSPL_ITEM_MASTER.Item_Desc,TSPL_ITEM_MASTER.Item_Type,TSPL_PP_BOM_ITEM_DETAIL.UNIT_CODE,TSPL_ITEM_MASTER.Product_Type
,(TSPL_PP_BOM_ITEM_DETAIL.QUANTITY+TSPL_PP_BOM_ITEM_DETAIL.QUANTITY*coalesce(TSPL_PP_BOM_ITEM_DETAIL.ProcessLossPer,0)/100) as QUANTITY
,(TSPL_PP_BOM_ITEM_DETAIL.FAT) as fat,(TSPL_PP_BOM_ITEM_DETAIL.SNF) as snf
,(TSPL_PP_BOM_ITEM_DETAIL.fat_kg+TSPL_PP_BOM_ITEM_DETAIL.fat_kg*coalesce(TSPL_PP_BOM_ITEM_DETAIL.ProcessLossPer,0)/100) as fat_kg
,(TSPL_PP_BOM_ITEM_DETAIL.snf_kg+TSPL_PP_BOM_ITEM_DETAIL.snf_kg*coalesce(TSPL_PP_BOM_ITEM_DETAIL.ProcessLossPer,0)/100) as snf_kg 
from   TSPL_PP_BOM_HEAD  
left outer join TSPL_PP_BOM_ITEM_DETAIL on TSPL_PP_BOM_ITEM_DETAIL.BOM_CODE=TSPL_PP_BOM_HEAD.BOM_CODE
left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_PP_BOM_ITEM_DETAIL.ITEM_CODE 
left outer join TSPL_ITEM_UOM_DETAIL as  TabConvFatDiv on TabConvFatDiv.Item_Code=TSPL_PP_BOM_HEAD.PROD_ITEM_CODE and TabConvFatDiv.UOM_Code=TSPL_PP_BOM_HEAD.PROD_ITEM_UNIT_CODE 
left outer join TSPL_ITEM_UOM_DETAIL as  TabConvFatMul on TabConvFatMul.item_code=TSPL_PP_BOM_HEAD.PROD_ITEM_CODE and TabConvFatMul.UOM_Code='" + strUOM + "'
where  TSPL_PP_BOM_HEAD.BOM_CODE='" + clsCommon.myCstr(gvPro.CurrentRow.Cells(ColProBOMCode).Value) + "'
)xx  "
            dt = clsDBFuncationality.GetDataTable(qry)
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                Throw New Exception("Raw items for BOM [" + clsCommon.myCstr(gvPro.CurrentRow.Cells(ColProBOMCode).Value) + "] not found")
            End If
            Dim arrRM As New List(Of clsProductionShiftMgmtProductionRM)
            Dim dblFATKG As Decimal = 0
            Dim dblSNFKG As Decimal = 0
            For Each dr As DataRow In dt.Rows
                Dim obj As New clsProductionShiftMgmtProductionRM
                obj.Item_Code = clsCommon.myCstr(dr("ITEM_CODE"))
                obj.Item_Name = clsCommon.myCstr(dr("Item_Desc"))
                obj.Qty = clsCommon.myCDecimal(dr("Qty"))
                obj.UOM = clsCommon.myCstr(dr("UNIT_CODE"))
                obj.FAT = clsCommon.myCDecimal(dr("fat"))
                obj.SNF = clsCommon.myCDecimal(dr("snf"))
                obj.FAT_KG = clsCommon.myCDecimal(dr("fat_kg"))
                obj.SNF_KG = clsCommon.myCDecimal(dr("snf_kg"))
                arrRM.Add(obj)

                dblFATKG += clsCommon.myCDecimal(dr("fat_kg"))
                dblSNFKG += clsCommon.myCDecimal(dr("snf_kg"))
            Next
            gvPro.CurrentRow.Cells(ColProBOMCode).Tag = arrRM
            gvPro.CurrentRow.Cells(ColProFATKG).Value = dblFATKG
            gvPro.CurrentRow.Cells(ColProSNFKG).Value = dblSNFKG
            If FromLTR Then
                gvPro.CurrentRow.Cells(ColProQtyKG).Value = clsItemMaster.Convert(clsCommon.myCstr(gvPro.CurrentRow.Cells(ColProItemCode).Value), clsCommon.myCDecimal(gvPro.CurrentRow.Cells(ColProQtyLTR).Value), "LTR", "KG")
            Else
                gvPro.CurrentRow.Cells(ColProQtyLTR).Value = clsItemMaster.Convert(clsCommon.myCstr(gvPro.CurrentRow.Cells(ColProItemCode).Value), clsCommon.myCDecimal(gvPro.CurrentRow.Cells(ColProQtyKG).Value), "KG", "LTR")
            End If
            gvPro.CurrentRow.Cells(ColProFAT).Value = clsCommon.myCDivide(dblFATKG * 100, clsCommon.myCDecimal(gvPro.CurrentRow.Cells(ColProQtyKG).Value))
            gvPro.CurrentRow.Cells(ColProSNF).Value = clsCommon.myCDivide(dblSNFKG * 100, clsCommon.myCDecimal(gvPro.CurrentRow.Cells(ColProQtyKG).Value))
            If FromLTR Then
                gvPro.CurrentRow.Cells(ColProEnteredUOM).Value = 1
            Else
                gvPro.CurrentRow.Cells(ColProEnteredUOM).Value = 2
            End If
            CalcuateProuctionRawMilk()
        Catch ex As Exception
            gvPro.CurrentRow.Cells(ColProQtyLTR).Value = 0
            gvPro.CurrentRow.Cells(ColProQtyKG).Value = 0
            gvPro.CurrentRow.Cells(ColProEnteredUOM).Value = 0
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Sub CalcuateProuctionRawMilk()
        LoadBlankGridProRM()
        For ii As Integer = 0 To gvPro.Rows.Count - 1
            Dim ArrRM As List(Of clsProductionShiftMgmtProductionRM) = TryCast(gvPro.CurrentRow.Cells(ColProBOMCode).Tag, List(Of clsProductionShiftMgmtProductionRM))
            If ArrRM IsNot Nothing AndAlso ArrRM.Count > 0 Then
                For Each objtr As clsProductionShiftMgmtProductionRM In ArrRM
                    Dim idx As Integer = -1
                    For jj As Integer = 0 To gvProRM.Rows.Count - 1
                        If clsCommon.CompairString(clsCommon.myCstr(gvProRM.Rows(jj).Cells(ColProRMItemCode).Value), objtr.Item_Code) = CompairStringResult.Equal Then
                            idx = jj
                        End If
                    Next
                    If idx < 0 Then
                        gvProRM.Rows.AddNew()
                        idx = gvProRM.Rows.Count - 1
                        gvProRM.Rows(idx).Cells(ColProRMSNo).Value = idx + 1
                        gvProRM.Rows(idx).Cells(ColProRMItemCode).Value = objtr.Item_Code
                        gvProRM.Rows(idx).Cells(ColProRMItemName).Value = objtr.Item_Name
                        gvProRM.Rows(idx).Cells(ColProRMUOM).Value = clsItemMaster.GetStockUnit(objtr.Item_Code, Nothing)
                        gvProRM.Rows(idx).Cells(ColProRMQty).Value = 0
                        gvProRM.Rows(idx).Cells(ColProRMFAT).Value = 0
                        gvProRM.Rows(idx).Cells(ColProRMSNF).Value = 0
                        gvProRM.Rows(idx).Cells(ColProRMFATKG).Value = 0
                        gvProRM.Rows(idx).Cells(ColProRMSNFKG).Value = 0
                    End If
                    gvProRM.Rows(idx).Cells(ColProRMQty).Value += clsItemMaster.Convert(objtr.Item_Code, objtr.Qty, objtr.UOM, clsCommon.myCstr(gvProRM.Rows(idx).Cells(ColProRMUOM).Value))
                    gvProRM.Rows(idx).Cells(ColProRMFATKG).Value += objtr.FAT_KG
                    gvProRM.Rows(idx).Cells(ColProRMSNFKG).Value += objtr.SNF_KG

                    Dim dclKGQty As Decimal = clsItemMaster.Convert(clsCommon.myCstr(gvProRM.Rows(idx).Cells(ColProRMItemCode).Value), clsCommon.myCDecimal(gvProRM.Rows(idx).Cells(ColProRMQty).Value), clsCommon.myCstr(gvProRM.Rows(idx).Cells(ColProRMUOM).Value), "KG")
                    If dclKGQty > 0 Then
                        gvProRM.Rows(idx).Cells(ColProRMFAT).Value = clsCommon.myCDivide(clsCommon.myCDecimal(gvProRM.Rows(idx).Cells(ColProRMFATKG).Value) * 100, dclKGQty)
                        gvProRM.Rows(idx).Cells(ColProRMSNF).Value = clsCommon.myCDivide(clsCommon.myCDecimal(gvProRM.Rows(idx).Cells(ColProRMSNFKG).Value) * 100, dclKGQty)
                    End If
                Next
            End If
        Next
    End Sub

    Sub OpenShiftCode(ByVal isButtonClicked As Boolean)
        'Dim qry As String = "select shift_code as Code,shift_name as Description,from_time as [From Time],to_time as [To Time],interval_time as [Interval Time],fsthalf_adjust_min as [First Half Adjustment],sechalf_adjust_min as [Second Half Adjustment] from tspl_shift_master"
        'gvDisposal.CurrentRow.Cells(colShift).Value = clsCommon.ShowSelectForm("PUSFTFND", qry, "Code", "", clsCommon.myCstr(gvDisposal.CurrentRow.Cells(colShift).Value), "Code", isButtonClicked)
        ''gv1.CurrentRow.Cells(colShiftName).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select shift_name from tspl_shift_master where shift_code='" + shiftcode + "'"))
    End Sub
    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        SaveData()
    End Sub
    Private Function AllowToSave() As Boolean
        'For ii As Integer = 0 To gv1.RowCount - 1
        '    If clsCommon.myLen(gv1.Rows(ii).Cells(colLocCode).Value) > 0 Then

        '        If objCommonVar.AddValidationofMilkTypeinsample Then
        '            If clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(ii).Cells(colDockCollectionMilkType).Value), "M") = CompairStringResult.Equal Then
        '                If clsCommon.myCDecimal(gv1.Rows(ii).Cells(colFATPer).Value) < objCommonVar.FatMinMix OrElse clsCommon.myCDecimal(gv1.Rows(ii).Cells(colFATPer).Value) > objCommonVar.FatMaxMix Then
        '                    Throw New Exception("Milk Type [" + clsCommon.myCstr(gv1.Rows(ii).Cells(colDockCollectionMilkType).Value) + "] FAT Should be in Range [" + clsCommon.myCstr(objCommonVar.FatMinMix) + " - " + clsCommon.myCstr(objCommonVar.FatMaxMix) + "]")
        '                ElseIf clsCommon.myCDecimal(gv1.Rows(ii).Cells(colSNFPer).Value) < objCommonVar.SNFMinMix OrElse clsCommon.myCDecimal(gv1.Rows(ii).Cells(colSNFPer).Value) > objCommonVar.SNFMaxMix Then

        '                    Throw New Exception("Milk Type [" + clsCommon.myCstr(gv1.Rows(ii).Cells(colDockCollectionMilkType).Value) + "] SNF Should be in Range [" + clsCommon.myCstr(objCommonVar.SNFMinMix) + " - " + clsCommon.myCstr(objCommonVar.SNFMaxMix) + "]")
        '                End If
        '            ElseIf clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(ii).Cells(colDockCollectionMilkType).Value), "C") = CompairStringResult.Equal Then
        '                If clsCommon.myCDecimal(gv1.Rows(ii).Cells(colFATPer).Value) < objCommonVar.FatMinCow OrElse clsCommon.myCDecimal(gv1.Rows(ii).Cells(colFATPer).Value) > objCommonVar.FatMaxCow Then
        '                    Throw New Exception("Milk Type [" + clsCommon.myCstr(gv1.Rows(ii).Cells(colDockCollectionMilkType).Value) + "] FAT Should be in Range [" + clsCommon.myCstr(objCommonVar.FatMinCow) + " - " + clsCommon.myCstr(objCommonVar.FatMaxCow) + "]")
        '                ElseIf clsCommon.myCDecimal(gv1.Rows(ii).Cells(colSNFPer).Value) < objCommonVar.SNFMinCow OrElse clsCommon.myCDecimal(gv1.Rows(ii).Cells(colSNFPer).Value) > objCommonVar.SNFMaxCow Then
        '                    Throw New Exception("Milk Type [" + clsCommon.myCstr(gv1.Rows(ii).Cells(colDockCollectionMilkType).Value) + "] SNF Should be in Range [" + clsCommon.myCstr(objCommonVar.SNFMinCow) + " - " + clsCommon.myCstr(objCommonVar.SNFMaxCow) + "]")
        '                End If
        '            ElseIf clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(ii).Cells(colDockCollectionMilkType).Value), "B") = CompairStringResult.Equal Then
        '                If clsCommon.myCDecimal(gv1.Rows(ii).Cells(colFATPer).Value) < objCommonVar.FatMinBuff OrElse clsCommon.myCDecimal(gv1.Rows(ii).Cells(colFATPer).Value) > objCommonVar.FatMaxBuff Then
        '                    Throw New Exception("Milk Type [" + clsCommon.myCstr(gv1.Rows(ii).Cells(colDockCollectionMilkType).Value) + "] FAT Should be in Range [" + clsCommon.myCstr(objCommonVar.FatMinBuff) + " - " + clsCommon.myCstr(objCommonVar.FatMaxBuff) + "]")
        '                ElseIf clsCommon.myCDecimal(gv1.Rows(ii).Cells(colSNFPer).Value) < objCommonVar.SNFMinBuff OrElse clsCommon.myCDecimal(gv1.Rows(ii).Cells(colSNFPer).Value) > objCommonVar.SNFMaxBuff Then
        '                    Throw New Exception("Milk Type [" + clsCommon.myCstr(gv1.Rows(ii).Cells(colDockCollectionMilkType).Value) + "] SNF Should be in Range [" + clsCommon.myCstr(objCommonVar.SNFMinBuff) + " - " + clsCommon.myCstr(objCommonVar.SNFMaxBuff) + "]")
        '                End If
        '            Else
        '                Throw New Exception("Milk Type should be M/B/C")
        '            End If
        '        End If
        '        If settLastMilkReceiptQtyTollerance > 0 Then
        '            Dim qty As Decimal = clsCommon.myCDecimal(gv1.Rows(ii).Cells(colMilkWeight).Value)
        '            If qty > 0 Then
        '                Dim dtLastQty As DataTable = clsDBFuncationality.GetDataTable("select QTY,cast( case when (QTY-(QTY*" + clsCommon.myCstr(settLastMilkReceiptQtyTollerance) + "/100)) < 0 then 0 else (QTY-(QTY*" + clsCommon.myCstr(settLastMilkReceiptQtyTollerance) + "/100)) end as decimal(18,2)) as MinQty,cast( (QTY+(QTY*" + clsCommon.myCstr(settLastMilkReceiptQtyTollerance) + "/100)) as decimal(18,2)) as MaxQty from TSPL_MILK_SRN_DETAIL where DOC_CODE in (select top 1  DOC_CODE from tspl_milk_srn_head where DOC_DATE<'" + clsCommon.GetPrintDate(clsCommon.myCDate(gv1.Rows(ii).Cells(colShiftDate).Value), "dd/MMM/yyyy") + "' and VLC_CODE='" + clsCommon.myCstr(gv1.Rows(ii).Cells(colVLCCode).Value) + "' order by doc_date desc,SHIFT)")
        '                If dtLastQty IsNot Nothing AndAlso dtLastQty.Rows.Count > 0 Then
        '                    If qty < clsCommon.myCDecimal(dtLastQty.Rows(0)("MinQty")) OrElse qty > clsCommon.myCDecimal(dtLastQty.Rows(0)("MaxQty")) Then
        '                        If clsCommon.MyMessageBoxShow("Row No [" + clsCommon.myCstr(ii + 1) + "] Qty [" + clsCommon.myCstr(qty) + "] Tollerance [" + clsCommon.myCstr(settLastMilkReceiptQtyTollerance) + "] and Valid Qty Range [" + clsCommon.myCstr(dtLastQty.Rows(0)("MinQty")) + "-" + clsCommon.myCstr(dtLastQty.Rows(0)("MaxQty")) + "]" + Environment.NewLine + "Do you want to continue...", Me.Text, MessageBoxButtons.YesNo, WinControls.RadMessageIcon.Question) = System.Windows.Forms.DialogResult.No Then
        '                            Return False
        '                        End If
        '                    End If
        '                End If
        '            End If
        '        End If

        '    End If
        'Next
        Return True
    End Function
    Private Sub RadMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If clsCommon.myLen(ReportID) > 0 Then
            gvPro.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = ReportID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gvPro.SaveLayout(obj.GridLayout)
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            obj.GridColumns = gvPro.ColumnCount
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully", "Information")
            End If
            ''stuti regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub
    Private Sub RadMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        clsGridLayout.DeleteData(ReportID, objCommonVar.CurrentUserCode)
    End Sub
    Private Sub FrmSerializeItemIn_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N Then
            AddNew()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.Escape Then
            CancelPressed()
        ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F12 Then
            If MyBase.isReverse Then
                Dim frm As New FrmPWD(Nothing)
                frm.strType = clsFixedParameterType.SIRC
                frm.strCode = clsFixedParameterCode.SIReversAndCreate
                frm.ShowDialog()
                If frm.isPasswordCorrect Then
                    btnReverse.Visible = True
                End If
            Else
                clsCommon.MyMessageBoxShow(Me, "You are not authorized to perform this action.", Me.Text, MessageBoxButtons.OK, Telerik.WinControls.RadMessageIcon.Error)
            End If
        End If
    End Sub
    Sub CancelPressed()
        Me.Close()
    End Sub
    Private Sub gv1_UserDeletedRow(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewRowEventArgs) Handles gvPro.UserDeletedRow
        RefeshSNO()
    End Sub
    Private Sub gv1_UserDeletingRow(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs) Handles gvPro.UserDeletingRow
        'If gv1.RowCount <= clsCommon.myCDecimal(lblQty.Text) Then
        '    e.Cancel = True
        '    Exit Sub
        'End If

        'If common.clsCommon.MyMessageBoxShow("Delete The Current Row." + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
        '    e.Cancel = True
        'End If
    End Sub
    Sub RefeshSNO()
        For ii As Integer = 1 To gvPro.Rows.Count
            gvPro.Rows(ii - 1).Cells(ColProSNo).Value = ii
        Next
    End Sub
    Private Sub RadButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        CancelPressed()
    End Sub
    Private Sub gv1_CurrentColumnChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gvPro.CurrentColumnChanged
        If gvPro.RowCount > 0 Then
            Dim intCurrRow As Integer = gvPro.CurrentRow.Index
            gvPro.CurrentRow.Cells(ColProSNo).Value = clsCommon.myCDecimal(intCurrRow + 1)
            If intCurrRow = gvPro.Rows.Count - 1 Then
                gvPro.Rows.AddNew()
                gvPro.CurrentRow = gvPro.Rows(intCurrRow)
            End If
        End If
    End Sub
    Private Sub txtDocNo__MYNavigator(sender As Object, e As EventArgs, NavType As NavigatorType) Handles txtDocNo._MYNavigator
        Try
            Dim qst As String = "select count(*) from TSPL_SHIFT_MGMT where Document_No='" + txtDocNo.Value + "'"
            Dim count As Integer = clsCommon.myCDecimal(clsDBFuncationality.getSingleValue(qst))
            If count = 0 Then
                txtDocNo.MyReadOnly = False
            Else
                txtDocNo.MyReadOnly = True
            End If
            LoadData(txtDocNo.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub txtDocNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtDocNo._MYValidating
        Dim qry As String = "select TSPL_SHIFT_MGMT.Document_No,convert (varchar,TSPL_SHIFT_MGMT.Document_Date,103) as Document_Date,TSPL_SHIFT_MGMT.Remarks,TSPL_SHIFT_MGMT.Comment
,case when TSPL_SHIFT_MGMT.Status=1 then 'Posted' else 'Pending' end as Status
,TSPL_SHIFT_MGMT.Location_Code as [FG Location Code]  ,TSPL_LOCATION_MASTER_FG.Location_Desc as [Location Name]  
from TSPL_SHIFT_MGMT 
left outer join TSPL_LOCATION_MASTER as TSPL_LOCATION_MASTER_FG on TSPL_LOCATION_MASTER_FG.Location_Code=TSPL_SHIFT_MGMT.Location_Code"
        LoadData(clsCommon.ShowSelectForm("PUFINDOC", qry, "Document_No", "", txtDocNo.Value, "Document_No", isButtonClicked, "Document_Date"), NavigatorType.Current)
    End Sub
    Public Function SaveData() As Boolean
        Try
            If (AllowToSave()) Then
                Dim obj As New clsProductionShiftMgmt()
                obj.Document_No = txtDocNo.Value
                obj.Document_Date = txtDate.Value
                obj.Shift_Code = clsCommon.myCstr(cboShift.SelectedValue)
                obj.Location_Code = txtLocation.Value
                obj.Comment = txtComment.Text
                obj.Remarks = txtRemarks.Text
                obj.ArrOP = New List(Of clsProductionShiftMgmtOpen)
                For ii As Integer = 0 To gvOP.RowCount - 1
                    If clsCommon.myLen(gvOP.Rows(ii).Cells(ColOPItemCode).Value) > 0 Then
                        Dim objTr As New clsProductionShiftMgmtOpen()
                        objTr.Location_Code = clsCommon.myCstr(gvOP.Rows(ii).Cells(ColOPLocationCode).Value)
                        objTr.Item_Code = clsCommon.myCstr(gvOP.Rows(ii).Cells(ColOPItemCode).Value)
                        objTr.Qty_KG = clsCommon.myCDecimal(gvOP.Rows(ii).Cells(ColOPQtyKG).Value)
                        objTr.Qty_LTR = clsCommon.myCDecimal(gvOP.Rows(ii).Cells(ColOPQtyLtr).Value)

                        objTr.FAT = clsCommon.myCDecimal(gvOP.Rows(ii).Cells(ColOPFAT).Value)
                        objTr.SNF = clsCommon.myCDecimal(gvOP.Rows(ii).Cells(ColOPSNF).Value)
                        objTr.FAT_KG = clsCommon.myCDecimal(gvOP.Rows(ii).Cells(ColOPFATKG).Value)
                        objTr.SNF_KG = clsCommon.myCDecimal(gvOP.Rows(ii).Cells(ColOPSNFKG).Value)
                        objTr.Temp = clsCommon.myCDecimal(gvOP.Rows(ii).Cells(ColOPTemp).Value)
                        objTr.Acidity = clsCommon.myCDecimal(gvOP.Rows(ii).Cells(ColOPAcidity).Value)
                        objTr.COB = clsCommon.myCDecimal(gvOP.Rows(ii).Cells(ColOPCOB).Value)
                        objTr.Alcohol_Test = clsCommon.myCstr(gvOP.Rows(ii).Cells(ColOPAlcohol).Value)
                        objTr.Remarks = clsCommon.myCstr(gvOP.Rows(ii).Cells(ColOPRemarks).Value)

                        obj.ArrOP.Add(objTr)
                    End If
                Next

                obj.ArrRecPlant = New List(Of clsProductionShiftMgmtReceiptPlantMilk)
                For ii As Integer = 0 To gvRecPlant.RowCount - 1
                    If clsCommon.myLen(gvRecPlant.Rows(ii).Cells(ColRecPlantItemCode).Value) > 0 Then
                        Dim objTr As New clsProductionShiftMgmtReceiptPlantMilk()
                        objTr.Shift = clsCommon.myCstr(gvRecPlant.Rows(ii).Cells(ColRecPlantShift).Value)
                        objTr.Reject_Type = clsCommon.myCstr(gvRecPlant.Rows(ii).Cells(ColRecPlantRejectType).Value)
                        objTr.Item_Code = clsCommon.myCstr(gvRecPlant.Rows(ii).Cells(ColRecPlantItemCode).Value)
                        objTr.Qty_KG = clsCommon.myCDecimal(gvRecPlant.Rows(ii).Cells(ColRecPlantQtyKG).Value)
                        objTr.Qty_LTR = clsCommon.myCDecimal(gvRecPlant.Rows(ii).Cells(ColRecPlantQtyLtr).Value)
                        objTr.FAT = clsCommon.myCDecimal(gvRecPlant.Rows(ii).Cells(ColRecPlantFAT).Value)
                        objTr.SNF = clsCommon.myCDecimal(gvRecPlant.Rows(ii).Cells(ColRecPlantSNF).Value)
                        objTr.FAT_KG = clsCommon.myCDecimal(gvRecPlant.Rows(ii).Cells(ColRecPlantFATKG).Value)
                        objTr.SNF_KG = clsCommon.myCDecimal(gvRecPlant.Rows(ii).Cells(ColRecPlantSNFKG).Value)
                        objTr.Remarks = clsCommon.myCstr(gvRecPlant.Rows(ii).Cells(ColRecPlantRemarks).Value)
                        obj.ArrRecPlant.Add(objTr)
                    End If
                Next

                obj.ArrRecBulk = New List(Of clsProductionShiftMgmtReceiptBulkMilk)
                For ii As Integer = 0 To gvRecBulk.RowCount - 1
                    If clsCommon.myLen(gvRecBulk.Rows(ii).Cells(ColRecBulkItemCode).Value) > 0 Then
                        Dim objTr As New clsProductionShiftMgmtReceiptBulkMilk()
                        objTr.Trans_Type = clsCommon.myCstr(gvRecBulk.Rows(ii).Cells(ColRecBulkTranType).Value)
                        If clsCommon.CompairString(objTr.Trans_Type, "BulkSRN") = CompairStringResult.Equal Then
                            objTr.Against_BulkMilkSRN = clsCommon.myCstr(gvRecBulk.Rows(ii).Cells(ColRecBulkTranNo).Value)
                        ElseIf clsCommon.CompairString(objTr.Trans_Type, "MilkTransferIn") = CompairStringResult.Equal Then
                            objTr.Against_MilkTransferIn = clsCommon.myCstr(gvRecBulk.Rows(ii).Cells(ColRecBulkTranNo).Value)
                        ElseIf clsCommon.CompairString(objTr.Trans_Type, "IC-AD") = CompairStringResult.Equal Then
                            objTr.Against_Adjustment = clsCommon.myCstr(gvRecBulk.Rows(ii).Cells(ColRecBulkTranNo).Value)
                        End If
                        objTr.Item_Code = clsCommon.myCstr(gvRecBulk.Rows(ii).Cells(ColRecBulkItemCode).Value)
                        objTr.Qty_KG = clsCommon.myCDecimal(gvRecBulk.Rows(ii).Cells(ColRecBulkQtyKG).Value)
                        objTr.Qty_LTR = clsCommon.myCDecimal(gvRecBulk.Rows(ii).Cells(ColRecBulkQtyLtr).Value)
                        objTr.FAT = clsCommon.myCDecimal(gvRecBulk.Rows(ii).Cells(ColRecBulkFAT).Value)
                        objTr.SNF = clsCommon.myCDecimal(gvRecBulk.Rows(ii).Cells(ColRecBulkSNF).Value)
                        objTr.FAT_KG = clsCommon.myCDecimal(gvRecBulk.Rows(ii).Cells(ColRecBulkFATKG).Value)
                        objTr.SNF_KG = clsCommon.myCDecimal(gvRecBulk.Rows(ii).Cells(ColRecBulkSNFKG).Value)
                        objTr.Temp = clsCommon.myCDecimal(gvRecBulk.Rows(ii).Cells(ColRecBulkTemp).Value)
                        objTr.Acidity = clsCommon.myCDecimal(gvRecBulk.Rows(ii).Cells(ColRecBulkAcidity).Value)
                        objTr.COB = clsCommon.myCDecimal(gvRecBulk.Rows(ii).Cells(ColRecBulkCOB).Value)
                        objTr.Alcohol_Test = clsCommon.myCstr(gvRecBulk.Rows(ii).Cells(ColRecBulkAlcohol).Value)
                        objTr.Remarks = clsCommon.myCstr(gvRecBulk.Rows(ii).Cells(ColRecBulkRemarks).Value)
                        obj.ArrRecBulk.Add(objTr)
                    End If
                Next

                obj.ArrPro = New List(Of clsProductionShiftMgmtProduction)
                For ii As Integer = 0 To gvPro.RowCount - 1
                    If clsCommon.myLen(gvPro.Rows(ii).Cells(ColProItemCode).Value) > 0 Then
                        Dim objTr As New clsProductionShiftMgmtProduction()
                        objTr.Item_Code = clsCommon.myCstr(gvPro.Rows(ii).Cells(ColProItemCode).Value)
                        objTr.Qty_KG = clsCommon.myCDecimal(gvPro.Rows(ii).Cells(ColProQtyKG).Value)
                        objTr.Qty_LTR = clsCommon.myCDecimal(gvPro.Rows(ii).Cells(ColProQtyLTR).Value)
                        objTr.FAT = clsCommon.myCDecimal(gvPro.Rows(ii).Cells(ColProFAT).Value)
                        objTr.SNF = clsCommon.myCDecimal(gvPro.Rows(ii).Cells(ColProSNF).Value)
                        objTr.FAT_KG = clsCommon.myCDecimal(gvPro.Rows(ii).Cells(ColProFATKG).Value)
                        objTr.SNF_KG = clsCommon.myCDecimal(gvPro.Rows(ii).Cells(ColProSNFKG).Value)
                        objTr.Temp = clsCommon.myCDecimal(gvPro.Rows(ii).Cells(ColProTemp).Value)
                        objTr.Acidity = clsCommon.myCDecimal(gvPro.Rows(ii).Cells(ColProAcidity).Value)
                        objTr.COB = clsCommon.myCDecimal(gvPro.Rows(ii).Cells(ColProCOB).Value)
                        objTr.Alcohol_Test = clsCommon.myCstr(gvPro.Rows(ii).Cells(ColProAlcohol).Value)
                        objTr.Remarks = clsCommon.myCstr(gvPro.Rows(ii).Cells(ColProRemarks).Value)
                        objTr.BOM_Code = clsCommon.myCstr(gvPro.Rows(ii).Cells(ColProBOMCode).Value)
                        objTr.Entered_UOM = clsCommon.myCstr(gvPro.Rows(ii).Cells(ColProEnteredUOM).Value)

                        objTr.ArrRM = New List(Of clsProductionShiftMgmtProductionRM)
                        objTr.ArrRM = TryCast(gvPro.Rows(ii).Cells(ColProBOMCode).Tag, List(Of clsProductionShiftMgmtProductionRM))

                        objTr.ArrAdd = New List(Of clsProductionShiftMgmtProductionItemAddRemove)
                        objTr.ArrAdd = TryCast(gvPro.Rows(ii).Cells(ColProAdd).Tag, List(Of clsProductionShiftMgmtProductionItemAddRemove))

                        objTr.ArrRemove = New List(Of clsProductionShiftMgmtProductionItemAddRemove)
                        objTr.ArrRemove = TryCast(gvPro.Rows(ii).Cells(ColProRemove).Tag, List(Of clsProductionShiftMgmtProductionItemAddRemove))

                        obj.ArrPro.Add(objTr)
                    End If
                Next

                obj.ArrProRMSummary = New List(Of clsProductionShiftMgmtProductionRMSummary)
                For ii As Integer = 0 To gvProRM.RowCount - 1
                    If clsCommon.myLen(gvProRM.Rows(ii).Cells(ColProRMItemCode).Value) > 0 Then
                        Dim objTr As New clsProductionShiftMgmtProductionRMSummary()
                        objTr.Item_Code = clsCommon.myCstr(gvProRM.Rows(ii).Cells(ColProRMItemCode).Value)
                        objTr.Qty = clsCommon.myCDecimal(gvProRM.Rows(ii).Cells(ColProRMQty).Value)
                        objTr.UOM = clsCommon.myCstr(gvProRM.Rows(ii).Cells(ColProRMUOM).Value)
                        objTr.FAT = clsCommon.myCDecimal(gvProRM.Rows(ii).Cells(ColProRMFAT).Value)
                        objTr.SNF = clsCommon.myCDecimal(gvProRM.Rows(ii).Cells(ColProRMSNF).Value)
                        objTr.FAT_KG = clsCommon.myCDecimal(gvProRM.Rows(ii).Cells(ColProRMFATKG).Value)
                        objTr.SNF_KG = clsCommon.myCDecimal(gvProRM.Rows(ii).Cells(ColProRMSNFKG).Value)

                        objTr.Arr = New List(Of clsProductionShiftMgmtProductionRMIssue)
                        objTr.Arr = TryCast(gvProRM.Rows(ii).Cells(ColProRMIssue).Tag, List(Of clsProductionShiftMgmtProductionRMIssue))
                        obj.ArrProRMSummary.Add(objTr)
                    End If
                Next

                obj.ArrCL = New List(Of clsProductionShiftMgmtClose)
                For ii As Integer = 0 To gvCL.RowCount - 1
                    If clsCommon.myLen(gvCL.Rows(ii).Cells(colCLItemCode).Value) > 0 Then
                        Dim objTr As New clsProductionShiftMgmtClose()
                        objTr.Location_Code = clsCommon.myCstr(gvCL.Rows(ii).Cells(colCLLocationCode).Value)
                        objTr.Item_Code = clsCommon.myCstr(gvCL.Rows(ii).Cells(colCLItemCode).Value)
                        objTr.Qty_KG = clsCommon.myCDecimal(gvCL.Rows(ii).Cells(colCLQtyKG).Value)
                        objTr.Qty_LTR = clsCommon.myCDecimal(gvCL.Rows(ii).Cells(colCLQtyLtr).Value)
                        objTr.FAT = clsCommon.myCDecimal(gvCL.Rows(ii).Cells(colCLFAT).Value)
                        objTr.SNF = clsCommon.myCDecimal(gvCL.Rows(ii).Cells(colCLSNF).Value)
                        objTr.FAT_KG = clsCommon.myCDecimal(gvCL.Rows(ii).Cells(colCLFATKG).Value)
                        objTr.SNF_KG = clsCommon.myCDecimal(gvCL.Rows(ii).Cells(colCLSNFKG).Value)
                        objTr.Temp = clsCommon.myCDecimal(gvCL.Rows(ii).Cells(colCLTemp).Value)
                        objTr.Acidity = clsCommon.myCDecimal(gvCL.Rows(ii).Cells(colCLAcidity).Value)
                        objTr.COB = clsCommon.myCDecimal(gvCL.Rows(ii).Cells(colCLCOB).Value)
                        objTr.Alcohol_Test = clsCommon.myCstr(gvCL.Rows(ii).Cells(colCLAlcohol).Value)
                        objTr.Remarks = clsCommon.myCstr(gvCL.Rows(ii).Cells(colCLRemarks).Value)
                        obj.ArrCL.Add(objTr)
                    End If
                Next
                If (obj.ArrPro Is Nothing OrElse obj.ArrPro.Count <= 0) Then
                    Throw New Exception("Please Fill at list one Produce Item")
                End If
                obj.SaveData(obj, isNewEntry)
                clsCommon.MyMessageBoxShow(Me, "Data saved successfully", Me.Text)
                LoadData(obj.Document_No, NavigatorType.Current)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Function
    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Try
            isInsideLoadData = True
            LoadBlankGrid()
            LoadBlankGridProRM()
            Dim obj As clsProductionShiftMgmt = clsProductionShiftMgmt.GetData(strCode, NavTyep, Nothing)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Document_No) > 0) Then
                isNewEntry = False
                EnableDisableControl(False)
                If obj.Status = ERPTransactionStatus.Approved Then
                    btnSave.Enabled = False
                    btnPost.Enabled = False
                    btnDelete.Enabled = False
                Else
                    btnSave.Enabled = True
                    btnPost.Enabled = True
                    btnDelete.Enabled = True
                End If
                txtDocNo.Value = obj.Document_No
                txtDate.Value = obj.Document_Date
                cboShift.SelectedValue = obj.Shift_Code
                txtLocation.Value = obj.Location_Code
                txtComment.Text = obj.Comment
                txtRemarks.Text = obj.Remarks

                If obj.ArrOP IsNot Nothing AndAlso obj.ArrOP.Count > 0 Then
                    For Each objTr As clsProductionShiftMgmtOpen In obj.ArrOP
                        gvOP.Rows.AddNew()
                        gvOP.Rows(gvOP.Rows.Count - 1).Cells(ColOPPKID).Value = objTr.PK_ID
                        gvOP.Rows(gvOP.Rows.Count - 1).Cells(ColOPSNo).Value = gvOP.Rows.Count
                        gvOP.Rows(gvOP.Rows.Count - 1).Cells(ColOPLocationCode).Value = objTr.Location_Code
                        gvOP.Rows(gvOP.Rows.Count - 1).Cells(ColOPLocationName).Value = objTr.Location_Name
                        gvOP.Rows(gvOP.Rows.Count - 1).Cells(ColOPItemCode).Value = objTr.Item_Code
                        gvOP.Rows(gvOP.Rows.Count - 1).Cells(ColOPItemName).Value = objTr.Item_Name
                        gvOP.Rows(gvOP.Rows.Count - 1).Cells(ColOPQtyKG).Value = objTr.Qty_KG
                        gvOP.Rows(gvOP.Rows.Count - 1).Cells(ColOPQtyLtr).Value = objTr.Qty_LTR
                        gvOP.Rows(gvOP.Rows.Count - 1).Cells(ColOPFAT).Value = objTr.FAT
                        gvOP.Rows(gvOP.Rows.Count - 1).Cells(ColOPSNF).Value = objTr.SNF
                        gvOP.Rows(gvOP.Rows.Count - 1).Cells(ColOPFATKG).Value = objTr.FAT_KG
                        gvOP.Rows(gvOP.Rows.Count - 1).Cells(ColOPSNFKG).Value = objTr.SNF_KG
                        gvOP.Rows(gvOP.Rows.Count - 1).Cells(ColOPTemp).Value = objTr.Temp
                        gvOP.Rows(gvOP.Rows.Count - 1).Cells(ColOPAcidity).Value = objTr.Acidity
                        gvOP.Rows(gvOP.Rows.Count - 1).Cells(ColOPCOB).Value = objTr.COB
                        gvOP.Rows(gvOP.Rows.Count - 1).Cells(ColOPAlcohol).Value = objTr.Alcohol_Test
                        gvOP.Rows(gvOP.Rows.Count - 1).Cells(ColOPRemarks).Value = objTr.Remarks
                    Next
                End If


                If obj.ArrRecPlant IsNot Nothing AndAlso obj.ArrRecPlant.Count > 0 Then
                    For Each objTr As clsProductionShiftMgmtReceiptPlantMilk In obj.ArrRecPlant
                        gvRecPlant.Rows.AddNew()
                        gvRecPlant.Rows(gvRecPlant.Rows.Count - 1).Cells(ColRecPlantPKID).Value = objTr.PK_ID
                        gvRecPlant.Rows(gvRecPlant.Rows.Count - 1).Cells(ColRecPlantSNo).Value = gvRecPlant.Rows.Count
                        gvRecPlant.Rows(gvRecPlant.Rows.Count - 1).Cells(ColRecPlantShift).Value = objTr.Shift
                        gvRecPlant.Rows(gvRecPlant.Rows.Count - 1).Cells(ColRecPlantRejectType).Value = objTr.Reject_Type
                        gvRecPlant.Rows(gvRecPlant.Rows.Count - 1).Cells(ColRecPlantItemCode).Value = objTr.Item_Code
                        gvRecPlant.Rows(gvRecPlant.Rows.Count - 1).Cells(ColRecPlantItemName).Value = objTr.Item_Name
                        gvRecPlant.Rows(gvRecPlant.Rows.Count - 1).Cells(ColRecPlantQtyKG).Value = objTr.Qty_KG
                        gvRecPlant.Rows(gvRecPlant.Rows.Count - 1).Cells(ColRecPlantQtyLtr).Value = objTr.Qty_LTR
                        gvRecPlant.Rows(gvRecPlant.Rows.Count - 1).Cells(ColRecPlantFAT).Value = objTr.FAT
                        gvRecPlant.Rows(gvRecPlant.Rows.Count - 1).Cells(ColRecPlantSNF).Value = objTr.SNF
                        gvRecPlant.Rows(gvRecPlant.Rows.Count - 1).Cells(ColRecPlantFATKG).Value = objTr.FAT_KG
                        gvRecPlant.Rows(gvRecPlant.Rows.Count - 1).Cells(ColRecPlantSNFKG).Value = objTr.SNF_KG
                        gvRecPlant.Rows(gvRecPlant.Rows.Count - 1).Cells(ColRecPlantRemarks).Value = objTr.Remarks
                    Next
                End If

                If obj.ArrRecBulk IsNot Nothing AndAlso obj.ArrRecBulk.Count > 0 Then
                    For Each objTr As clsProductionShiftMgmtReceiptBulkMilk In obj.ArrRecBulk
                        gvRecBulk.Rows.AddNew()
                        gvRecBulk.Rows(gvRecBulk.Rows.Count - 1).Cells(ColRecBulkPKID).Value = objTr.PK_ID
                        gvRecBulk.Rows(gvRecBulk.Rows.Count - 1).Cells(ColRecBulkSNo).Value = gvRecBulk.Rows.Count
                        gvRecBulk.Rows(gvRecBulk.Rows.Count - 1).Cells(ColRecBulkTranType).Value = objTr.Trans_Type
                        If clsCommon.myLen(objTr.Against_Adjustment) > 0 Then
                            gvRecBulk.Rows(gvRecBulk.Rows.Count - 1).Cells(ColRecBulkTranNo).Value = objTr.Against_Adjustment
                        ElseIf clsCommon.myLen(objTr.Against_BulkMilkSRN) > 0 Then
                            gvRecBulk.Rows(gvRecBulk.Rows.Count - 1).Cells(ColRecBulkTranNo).Value = objTr.Against_BulkMilkSRN
                        ElseIf clsCommon.myLen(objTr.Against_MilkTransferIn) > 0 Then
                            gvRecBulk.Rows(gvRecBulk.Rows.Count - 1).Cells(ColRecBulkTranNo).Value = objTr.Against_MilkTransferIn
                        End If
                        gvRecBulk.Rows(gvRecBulk.Rows.Count - 1).Cells(ColRecBulkItemCode).Value = objTr.Item_Code
                        gvRecBulk.Rows(gvRecBulk.Rows.Count - 1).Cells(ColRecBulkItemName).Value = objTr.Item_Name
                        gvRecBulk.Rows(gvRecBulk.Rows.Count - 1).Cells(ColRecBulkQtyKG).Value = objTr.Qty_KG
                        gvRecBulk.Rows(gvRecBulk.Rows.Count - 1).Cells(ColRecBulkQtyLtr).Value = objTr.Qty_LTR
                        gvRecBulk.Rows(gvRecBulk.Rows.Count - 1).Cells(ColRecBulkFAT).Value = objTr.FAT
                        gvRecBulk.Rows(gvRecBulk.Rows.Count - 1).Cells(ColRecBulkSNF).Value = objTr.SNF
                        gvRecBulk.Rows(gvRecBulk.Rows.Count - 1).Cells(ColRecBulkFATKG).Value = objTr.FAT_KG
                        gvRecBulk.Rows(gvRecBulk.Rows.Count - 1).Cells(ColRecBulkSNFKG).Value = objTr.SNF_KG
                        gvRecBulk.Rows(gvRecBulk.Rows.Count - 1).Cells(ColRecBulkTemp).Value = objTr.Temp
                        gvRecBulk.Rows(gvRecBulk.Rows.Count - 1).Cells(ColRecBulkAcidity).Value = objTr.Acidity
                        gvRecBulk.Rows(gvRecBulk.Rows.Count - 1).Cells(ColRecBulkCOB).Value = objTr.COB
                        gvRecBulk.Rows(gvRecBulk.Rows.Count - 1).Cells(ColRecBulkAlcohol).Value = objTr.Alcohol_Test
                        gvRecBulk.Rows(gvRecBulk.Rows.Count - 1).Cells(ColRecBulkRemarks).Value = objTr.Remarks
                    Next
                End If

                If obj.ArrPro IsNot Nothing AndAlso obj.ArrPro.Count > 0 Then
                    For Each objTr As clsProductionShiftMgmtProduction In obj.ArrPro
                        gvPro.Rows(gvPro.Rows.Count - 1).Cells(ColProPKID).Value = objTr.PK_ID
                        gvPro.Rows(gvPro.Rows.Count - 1).Cells(ColProSNo).Value = gvPro.Rows.Count
                        gvPro.Rows(gvPro.Rows.Count - 1).Cells(ColProItemCode).Value = objTr.Item_Code
                        gvPro.Rows(gvPro.Rows.Count - 1).Cells(ColProItemName).Value = objTr.Item_Name
                        gvPro.Rows(gvPro.Rows.Count - 1).Cells(ColProQtyKG).Value = objTr.Qty_KG
                        gvPro.Rows(gvPro.Rows.Count - 1).Cells(ColProQtyLTR).Value = objTr.Qty_LTR
                        gvPro.Rows(gvPro.Rows.Count - 1).Cells(ColProFAT).Value = objTr.FAT
                        gvPro.Rows(gvPro.Rows.Count - 1).Cells(ColProSNF).Value = objTr.SNF
                        gvPro.Rows(gvPro.Rows.Count - 1).Cells(ColProFATKG).Value = objTr.FAT_KG
                        gvPro.Rows(gvPro.Rows.Count - 1).Cells(ColProSNFKG).Value = objTr.SNF_KG
                        gvPro.Rows(gvPro.Rows.Count - 1).Cells(ColProTemp).Value = objTr.Temp
                        gvPro.Rows(gvPro.Rows.Count - 1).Cells(ColProAcidity).Value = objTr.Acidity
                        gvPro.Rows(gvPro.Rows.Count - 1).Cells(ColProCOB).Value = objTr.COB
                        gvPro.Rows(gvPro.Rows.Count - 1).Cells(ColProAlcohol).Value = objTr.Alcohol_Test
                        gvPro.Rows(gvPro.Rows.Count - 1).Cells(ColProRemarks).Value = objTr.Remarks
                        gvPro.Rows(gvPro.Rows.Count - 1).Cells(ColProBOMCode).Value = objTr.BOM_Code
                        gvPro.Rows(gvPro.Rows.Count - 1).Cells(ColProEnteredUOM).Value = objTr.Entered_UOM
                        gvPro.Rows(gvPro.Rows.Count - 1).Cells(ColProBOMCode).Tag = objTr.ArrRM
                        gvPro.Rows(gvPro.Rows.Count - 1).Cells(ColProAdd).Tag = objTr.ArrAdd
                        gvPro.Rows(gvPro.Rows.Count - 1).Cells(ColProRemove).Tag = objTr.ArrRemove
                        gvPro.Rows.AddNew()
                    Next
                End If

                If obj.ArrProRMSummary IsNot Nothing AndAlso obj.ArrProRMSummary.Count > 0 Then
                    For Each objTr As clsProductionShiftMgmtProductionRMSummary In obj.ArrProRMSummary
                        gvProRM.Rows.AddNew()
                        gvProRM.Rows(gvProRM.Rows.Count - 1).Cells(ColProRMPKID).Value = objTr.PK_ID
                        gvProRM.Rows(gvProRM.Rows.Count - 1).Cells(ColProRMSNo).Value = gvProRM.Rows.Count
                        gvProRM.Rows(gvProRM.Rows.Count - 1).Cells(ColProRMItemCode).Value = objTr.Item_Code
                        gvProRM.Rows(gvProRM.Rows.Count - 1).Cells(ColProRMItemName).Value = objTr.Item_Name
                        gvProRM.Rows(gvProRM.Rows.Count - 1).Cells(ColProRMQty).Value = objTr.Qty
                        gvProRM.Rows(gvProRM.Rows.Count - 1).Cells(ColProRMUOM).Value = objTr.UOM
                        gvProRM.Rows(gvProRM.Rows.Count - 1).Cells(ColProRMFAT).Value = objTr.FAT
                        gvProRM.Rows(gvProRM.Rows.Count - 1).Cells(ColProRMSNF).Value = objTr.SNF
                        gvProRM.Rows(gvProRM.Rows.Count - 1).Cells(ColProRMFATKG).Value = objTr.FAT_KG
                        gvProRM.Rows(gvProRM.Rows.Count - 1).Cells(ColProRMSNFKG).Value = objTr.SNF_KG
                        gvProRM.Rows(gvProRM.Rows.Count - 1).Cells(ColProRMIssue).Tag = objTr.Arr
                    Next
                End If

                If obj.ArrCL IsNot Nothing AndAlso obj.ArrCL.Count > 0 Then
                    For Each objTr As clsProductionShiftMgmtClose In obj.ArrCL
                        gvCL.Rows.AddNew()
                        gvCL.Rows(gvCL.Rows.Count - 1).Cells(colCLPKID).Value = objTr.PK_ID
                        gvCL.Rows(gvCL.Rows.Count - 1).Cells(colCLSNo).Value = gvCL.Rows.Count
                        gvCL.Rows(gvCL.Rows.Count - 1).Cells(colCLLocationCode).Value = objTr.Location_Code
                        gvCL.Rows(gvCL.Rows.Count - 1).Cells(colCLLocationName).Value = objTr.Location_Name
                        gvCL.Rows(gvCL.Rows.Count - 1).Cells(colCLItemCode).Value = objTr.Item_Code
                        gvCL.Rows(gvCL.Rows.Count - 1).Cells(colCLItemName).Value = objTr.Item_Name
                        gvCL.Rows(gvCL.Rows.Count - 1).Cells(colCLQtyKG).Value = objTr.Qty_KG
                        gvCL.Rows(gvCL.Rows.Count - 1).Cells(colCLQtyLtr).Value = objTr.Qty_LTR
                        gvCL.Rows(gvCL.Rows.Count - 1).Cells(colCLFAT).Value = objTr.FAT
                        gvCL.Rows(gvCL.Rows.Count - 1).Cells(colCLSNF).Value = objTr.SNF
                        gvCL.Rows(gvCL.Rows.Count - 1).Cells(colCLFATKG).Value = objTr.FAT_KG
                        gvCL.Rows(gvCL.Rows.Count - 1).Cells(colCLSNFKG).Value = objTr.SNF_KG
                        gvCL.Rows(gvCL.Rows.Count - 1).Cells(colCLTemp).Value = objTr.Temp
                        gvCL.Rows(gvCL.Rows.Count - 1).Cells(colCLAcidity).Value = objTr.Acidity
                        gvCL.Rows(gvCL.Rows.Count - 1).Cells(colCLCOB).Value = objTr.COB
                        gvCL.Rows(gvCL.Rows.Count - 1).Cells(colCLAlcohol).Value = objTr.Alcohol_Test
                        gvCL.Rows(gvCL.Rows.Count - 1).Cells(colCLRemarks).Value = objTr.Remarks
                        If obj.ArrOP IsNot Nothing AndAlso obj.ArrOP.Count > 0 Then
                            For Each objTrOP As clsProductionShiftMgmtOpen In obj.ArrOP
                                If clsCommon.CompairString(objTr.Item_Code, objTrOP.Item_Code) = CompairStringResult.Equal AndAlso
                                    clsCommon.CompairString(objTr.Location_Code, objTrOP.Location_Code) = CompairStringResult.Equal Then
                                    gvCL.Rows(gvCL.Rows.Count - 1).Cells(colCLOPQtyKG).Value = objTrOP.Qty_KG
                                    gvCL.Rows(gvCL.Rows.Count - 1).Cells(colCLOPQtyLtr).Value = objTrOP.Qty_LTR
                                    gvCL.Rows(gvCL.Rows.Count - 1).Cells(colCLOPFAT).Value = objTrOP.FAT
                                    gvCL.Rows(gvCL.Rows.Count - 1).Cells(colCLOPSNF).Value = objTrOP.SNF
                                    gvCL.Rows(gvCL.Rows.Count - 1).Cells(colCLOPFATKG).Value = objTrOP.FAT_KG
                                    gvCL.Rows(gvCL.Rows.Count - 1).Cells(colCLOPSNFKG).Value = objTrOP.SNF_KG
                                    Exit For
                                End If
                            Next
                        End If
                    Next
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isInsideLoadData = False
        End Try
    End Sub
    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Try
            If clsCommon.myLen(txtDocNo.Value) <= 0 Then
                Throw New Exception("Please select document no to delete")
            End If
            If clsCommon.MyMessageBoxShow("Delete the current document" + Environment.NewLine + "Are you sure ? ", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                clsProductionShiftMgmt.DeleteData(txtDocNo.Value)
                clsCommon.MyMessageBoxShow(Me, "Data delete successfully", Me.Text)
                AddNew()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub btnPost_Click(sender As Object, e As EventArgs) Handles btnPost.Click
        Try
            If clsCommon.myLen(txtDocNo.Value) <= 0 Then
                Throw New Exception("No document found to post")
            End If
            If clsCommon.MyMessageBoxShow("Post the Current Document [" + txtDocNo.Value + "]" + Environment.NewLine + "Are You Sure.", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then

                clsProductionShiftMgmt.PostData(txtDocNo.Value)

                clsCommon.MyMessageBoxShow("Data posted successfully", Me.Text)
                LoadData(txtDocNo.Value, NavigatorType.Current)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub txtLocationFG__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtLocation._MYValidating
        If clsCommon.myLen(arrLoc) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "No location rights.", Me.Text)
            Exit Sub
        End If
        txtLocation.Value = clsLocation.getFinder(" tspl_location_master.IsMainPlant=1 and tspl_location_master.location_code in (" + arrLoc + ") and isnull(csa_type,'N')<>'Y' and isnull(Is_Section,'N')<>'Y'  and Location_Category<>'MCC'", txtLocation.Value, isButtonClicked)
        lblLocationFG.Text = clsLocation.GetName(txtLocation.Value, Nothing)
    End Sub
    'Private Sub txtLocationRM__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean)
    '    If clsCommon.myLen(arrLoc) <= 0 Then
    '        clsCommon.MyMessageBoxShow(Me, "No location rights.", Me.Text)
    '        Exit Sub
    '    End If
    '    txtLocationRM.Value = clsLocation.getFinder(" tspl_location_master.location_code in (" + arrLoc + ") and isnull(csa_type,'N')<>'Y' and isnull(Is_Section,'N')<>'Y' and isnull(Is_Sub_Location,'N')<>'N' and Location_Category<>'MCC'", txtLocationRM.Value, isButtonClicked)
    '    lblLocationRM.Text = clsLocation.GetName(txtLocationRM.Value, Nothing)
    'End Sub

    'Private Sub txtLocationPK__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean)
    '    If clsCommon.myLen(arrLoc) <= 0 Then
    '        clsCommon.MyMessageBoxShow(Me, "No location rights.", Me.Text)
    '        Exit Sub
    '    End If
    '    'txtLocationPK.Value = clsLocation.getFinder(" tspl_location_master.location_code in (" + arrLoc + ") and isnull(csa_type,'N')<>'Y' and isnull(Is_Section,'N')<>'Y' and isnull(Is_Sub_Location,'N')<>'Y' and Location_Category<>'MCC'", txtLocationPK.Value, isButtonClicked)
    '    txtLocationPK.Value = clsLocation.getFinder(" tspl_location_master.location_code in (" + arrLoc + ") and isnull(csa_type,'N')<>'Y' and isnull(Is_Section,'N')<>'Y'  and Location_Category<>'MCC'", txtLocationPK.Value, isButtonClicked)
    '    lblLocationPK.Text = clsLocation.GetName(txtLocationPK.Value, Nothing)
    'End Sub
    Private Sub btnHistory_Click(sender As Object, e As EventArgs) Handles btnHistory.Click
        Try
            If clsCommon.myLen(txtDocNo.Value) <= 0 Then
                clsCommon.MyMessageBoxShow("Select Document No")
                Exit Sub
            End If
            clsERPFuncationalityOLD.ShowTransHistoryData(txtDocNo.Value, "Document_No", "TSPL_SHIFT_MGMT", "TSPL_PRODUCTION_UPLOADER_DETAIL")
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
    Private Sub RadButton1_Click(sender As Object, e As EventArgs) Handles btnReverse.Click
        Try
            If common.clsCommon.MyMessageBoxShow("Reverse and Unpost the Current Document" + Environment.NewLine + "Are you sure", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                If clsProductionShiftMgmt.ReverseAndUnpost(txtDocNo.Value) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Successfully Reversed and Recreated", Me.Text)
                    LoadData(txtDocNo.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub btnShowInventory_Click(sender As Object, e As EventArgs) Handles btnShowInventory.Click
        clsOpenInventory.ShowInventoryDatails(txtDocNo.Value)
    End Sub
    Private Sub gvPro_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles gvPro.CellDoubleClick
        '        Try
        '            Dim qry As String
        '            If e.Column Is gvDisposal.Columns(ColQCStatus) Then
        '                If clsCommon.myLen(gvDisposal.CurrentRow.Cells(ColProItemCode).Value) > 0 Then
        '                    Dim Arr As List(Of clsDairyProductionUploaderQC) = TryCast(gvDisposal.CurrentRow.Cells(ColQCStatus).Tag, List(Of clsDairyProductionUploaderQC))
        '                    qry = "select Code as [QC Code],cast(Actual_Range as varchar) as [Standard Range],'' as [Value] From TSPL_ITEM_QC_PARAMETER_MASTER where Item_Code like '" + clsCommon.myCstr(gvDisposal.CurrentRow.Cells(ColProItemCode).Value) + "'
        'union all
        'select 'QC Status' as  [QC Code],'Y/N' as [Standard Range],'" + IIf(clsCommon.myCBool(gvDisposal.CurrentRow.Cells(ColQCStatus).Value), "Y", "N") + "' as [Value] "
        '                    Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        '                    If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
        '                        Throw New Exception("QC Parameter not defined for item [" + clsCommon.myCstr(gvDisposal.CurrentRow.Cells(ColProItemCode).Value) + "]")
        '                    End If
        '                    If Arr IsNot Nothing AndAlso Arr.Count > 0 Then
        '                        For ii As Integer = 0 To dt.Rows.Count - 1
        '                            For Each objtr As clsDairyProductionUploaderQC In Arr
        '                                If clsCommon.CompairString(objtr.QC_Code, clsCommon.myCstr(dt.Rows(ii)("QC Code"))) = CompairStringResult.Equal Then
        '                                    dt.Rows(ii)("Value") = objtr.Value
        '                                    Exit For
        '                                End If
        '                            Next
        '                        Next
        '                    End If
        '                    Dim frm As New FrmFreeGrid
        '                    frm.dt = dt
        '                    frm.arrEditableColumn = New List(Of String)
        '                    frm.arrEditableColumn.Add("Value")
        '                    frm.strFormName = "Fill QC Details of item [" + clsCommon.myCstr(gvDisposal.CurrentRow.Cells(ColProItemCode).Value) + "]"
        '                    frm.ReportID = Me.Form_ID
        '                    frm.WindowState = FormWindowState.Normal
        '                    frm.ShowDialog()
        '                    If frm.dt IsNot Nothing AndAlso frm.dt.Rows.Count > 0 Then
        '                        Dim ArrTemp As New List(Of clsDairyProductionUploaderQC)
        '                        Dim obj As clsDairyProductionUploaderQC = Nothing
        '                        For ii As Integer = 0 To frm.dt.Rows.Count - 1
        '                            If ii = frm.dt.Rows.Count - 1 Then
        '                                gvDisposal.CurrentRow.Cells(ColQCStatus).Value = (clsCommon.CompairString(clsCommon.myCstr(frm.dt.Rows(ii)("Value")), "Y") = CompairStringResult.Equal)
        '                                Exit For
        '                            End If
        '                            obj = New clsDairyProductionUploaderQC()
        '                            obj.QC_Code = clsCommon.myCstr(frm.dt.Rows(ii)("QC Code"))
        '                            obj.Value = clsCommon.myCstr(frm.dt.Rows(ii)("Value"))
        '                            ArrTemp.Add(obj)
        '                        Next
        '                        gvDisposal.CurrentRow.Cells(ColQCStatus).Tag = ArrTemp
        '                    Else
        '                        gvDisposal.CurrentRow.Cells(ColQCStatus).Tag = Nothing
        '                    End If
        '                End If
        '            End If
        '        Catch ex As Exception
        '            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        '        End Try
    End Sub
    Private Sub btnPrintNew_Click(sender As Object, e As EventArgs) Handles btnPrintNew.Click
        If clsCommon.myLen(txtDocNo.Value) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "No data found to Print", Me.Text)
        Else
            funPrint(txtDocNo.Value)
        End If
    End Sub
    Public Sub funPrint(ByVal StrCode As String)
        Try
            'Dim Qry = clsProductionShiftMgmt.GetAttachQry(StrCode)
            'Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
            'If dt.Rows.Count > 0 Then
            '    Dim frmCRV As New frmCrystalReportViewer()
            '    frmCRV.funreport(CrystalReportFolder.PRODUCTION, dt, "crptProductionUploaderPrint", "")
            'Else
            '    clsCommon.MyMessageBoxShow(Me, "No data found to Print", Me.Text)
            'End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub gvProRM_CommandCellClick(sender As Object, e As EventArgs) Handles gvProRM.CommandCellClick
        Try
            If gvProRM.CurrentColumn Is gvProRM.Columns(ColProRMIssue) Then
                If clsCommon.myLen(gvProRM.CurrentRow.Cells(ColProRMItemCode).Value) > 0 Then
                    Dim frm As New frmStockBalance
                    frm.ArrRMIssue = TryCast(gvProRM.CurrentRow.Cells(ColProRMIssue).Tag, List(Of clsProductionShiftMgmtProductionRMIssue))
                    frm.FilterItemCode = clsCommon.myCstr(gvProRM.CurrentRow.Cells(ColProRMItemCode).Value)
                    frm.FilterUOM = clsCommon.myCstr(gvProRM.CurrentRow.Cells(ColProRMUOM).Value)
                    frm.FilterReqQty = clsCommon.myCDecimal(gvProRM.CurrentRow.Cells(ColProRMQty).Value)
                    frm.FilterReqFATKg = clsCommon.myCDecimal(gvProRM.CurrentRow.Cells(ColProRMFATKG).Value)
                    frm.FilterReqSNFKg = clsCommon.myCDecimal(gvProRM.CurrentRow.Cells(ColProRMSNFKG).Value)
                    frm.FilterLocationCode = txtLocation.Value
                    Dim ShiftFromDate As DateTime
                    frm.FilterDate = clsShiftMaster.GetShiftTime(clsCommon.myCstr(cboShift.SelectedValue), txtDate.Value, ShiftFromDate)
                    frm.WindowState = FormWindowState.Normal
                    frm.ShowDialog()
                    If frm.isOKClicked = 1 Then
                        gvProRM.CurrentRow.Cells(ColProRMIssue).Tag = frm.ArrRMIssue
                    ElseIf frm.isOKClicked = 2 Then
                        gvProRM.CurrentRow.Cells(ColProRMIssue).Tag = Nothing
                    End If
                    CalculateClosing()
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub CalculateClosing()
        For jj As Integer = 0 To gvCL.Rows.Count - 1
            gvCL.Rows(jj).Cells(colCLQtyLtr).Value = 0
            gvCL.Rows(jj).Cells(colCLQtyKG).Value = 0
            gvCL.Rows(jj).Cells(colCLFATKG).Value = 0
            gvCL.Rows(jj).Cells(colCLSNFKG).Value = 0
        Next
        For ii As Integer = 0 To gvProRM.RowCount - 1
            If clsCommon.myLen(gvProRM.Rows(ii).Cells(ColProRMItemCode).Value) > 0 Then
                Dim ARR As List(Of clsProductionShiftMgmtProductionRMIssue) = TryCast(gvProRM.Rows(ii).Cells(ColProRMIssue).Tag, List(Of clsProductionShiftMgmtProductionRMIssue))
                If ARR IsNot Nothing AndAlso ARR.Count > 0 Then
                    For Each obj As clsProductionShiftMgmtProductionRMIssue In ARR
                        For jj As Integer = 0 To gvCL.Rows.Count - 1
                            If clsCommon.CompairString(obj.Item_Code, clsCommon.myCstr(gvCL.Rows(jj).Cells(colCLItemCode).Value)) = CompairStringResult.Equal AndAlso
                                clsCommon.CompairString(obj.Location_Code, clsCommon.myCstr(gvCL.Rows(jj).Cells(colCLLocationCode).Value)) = CompairStringResult.Equal Then
                                If clsCommon.CompairString(obj.UOM, "KG") = CompairStringResult.Equal Then
                                    gvCL.Rows(jj).Cells(colCLQtyKG).Value += obj.Qty
                                    gvCL.Rows(jj).Cells(colCLQtyLtr).Value += clsCommon.myCDivide(clsCommon.myCDecimal(gvCL.Rows(jj).Cells(colCLOPQtyKG).Value), clsCommon.myCDecimal(gvCL.Rows(jj).Cells(colCLOPQtyLtr).Value)) * obj.Qty
                                Else
                                    gvCL.Rows(jj).Cells(colCLQtyLtr).Value += obj.Qty
                                    gvCL.Rows(jj).Cells(colCLQtyKG).Value += clsCommon.myCDivide(clsCommon.myCDecimal(gvCL.Rows(jj).Cells(colCLOPQtyLtr).Value), clsCommon.myCDecimal(gvCL.Rows(jj).Cells(colCLOPQtyKG).Value)) * obj.Qty
                                End If
                                gvCL.Rows(jj).Cells(colCLFATKG).Value += obj.FAT_KG
                                gvCL.Rows(jj).Cells(colCLSNFKG).Value += obj.SNF_KG
                            End If
                        Next
                    Next
                End If
            End If
        Next
        For jj As Integer = 0 To gvCL.Rows.Count - 1
            gvCL.Rows(jj).Cells(colCLQtyLtr).Value = clsCommon.myCDecimal(gvCL.Rows(jj).Cells(colCLOPQtyLtr).Value) - clsCommon.myCDecimal(gvCL.Rows(jj).Cells(colCLQtyLtr).Value)
            gvCL.Rows(jj).Cells(colCLQtyKG).Value = clsCommon.myCDecimal(gvCL.Rows(jj).Cells(colCLOPQtyKG).Value) - clsCommon.myCDecimal(gvCL.Rows(jj).Cells(colCLQtyKG).Value)
            gvCL.Rows(jj).Cells(colCLFATKG).Value = clsCommon.myCDecimal(gvCL.Rows(jj).Cells(colCLOPFATKG).Value) - clsCommon.myCDecimal(gvCL.Rows(jj).Cells(colCLFATKG).Value)
            gvCL.Rows(jj).Cells(colCLSNFKG).Value = clsCommon.myCDecimal(gvCL.Rows(jj).Cells(colCLOPSNFKG).Value) - clsCommon.myCDecimal(gvCL.Rows(jj).Cells(colCLSNFKG).Value)
        Next
    End Sub

    Private Sub gvPro_CommandCellClick(sender As Object, e As EventArgs) Handles gvPro.CommandCellClick
        Try
            If clsCommon.myLen(gvPro.CurrentRow.Cells(ColProItemCode).Value) > 0 Then
                If gvPro.CurrentColumn Is gvPro.Columns(ColProAdd) Then
                    Dim frm As New frmProductionShiftMgmtAdd()
                    frm.Arr = TryCast(gvPro.CurrentRow.Cells(ColProAdd).Tag, List(Of clsProductionShiftMgmtProductionItemAddRemove))
                    Dim ShiftFromDate As DateTime
                    frm.FilterDate = clsShiftMaster.GetShiftTime(clsCommon.myCstr(cboShift.SelectedValue), txtDate.Value, ShiftFromDate)
                    frm.FilterLocationCode = txtLocation.Value
                    frm.WindowState = FormWindowState.Normal
                    frm.ShowDialog()
                    If frm.isOKClicked = 1 Then
                        gvPro.CurrentRow.Cells(ColProAdd).Tag = frm.Arr
                    ElseIf frm.isOKClicked = 2 Then
                        gvPro.CurrentRow.Cells(ColProAdd).Tag = Nothing
                    End If
                ElseIf gvPro.CurrentColumn Is gvPro.Columns(ColProRemove) Then
                    Dim frm As New frmProductionShiftMgmtRemove()
                    frm.Arr = TryCast(gvPro.CurrentRow.Cells(ColProRemove).Tag, List(Of clsProductionShiftMgmtProductionItemAddRemove))
                    Dim ShiftFromDate As DateTime
                    frm.FilterDate = clsShiftMaster.GetShiftTime(clsCommon.myCstr(cboShift.SelectedValue), txtDate.Value, ShiftFromDate)
                    frm.FilterLocationCode = txtLocation.Value
                    frm.WindowState = FormWindowState.Normal
                    frm.ShowDialog()
                    If frm.isOKClicked = 1 Then
                        gvPro.CurrentRow.Cells(ColProRemove).Tag = frm.Arr
                    ElseIf frm.isOKClicked = 2 Then
                        gvPro.CurrentRow.Cells(ColProRemove).Tag = Nothing
                    End If
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class
