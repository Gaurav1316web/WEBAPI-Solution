Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common
Imports System.IO
'====================== created by Preeti Gupta--Ticket No.[BM00000004820]==========
Public Class RptTankerSummaryReport
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim arrLoc As String = Nothing

    Private Sub LOCATIONRIGTHS()
        Try
            Dim obj As New clsMCCCodes()
            obj = clsMCCCodes.GetData()

            If obj.arrLocCodes IsNot Nothing AndAlso clsCommon.myLen(obj.arrLocCodes) > 0 Then
                arrLoc = obj.arrLocCodes
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Private Sub SetUserMgmtNew()
        MyBase.SetUserMgmt(clsUserMgtCode.RptTankerSummaryReport)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        radbtnExp.Visible = MyBase.isExport
    End Sub
   
    Sub LoadMCC()
        'Dim qry As String = "select MCC_Code as [Code] ,MCC_NAME as [Name] from TSPL_MCC_MASTER  "

        Dim qry As String = Nothing
        Dim dt As DataTable = Nothing
        If clsCommon.myLen(arrLoc) > 0 Then
            'qry = "select MCC_Code as [Code] ,MCC_NAME as [Name] from TSPL_MCC_MASTER where MCC_Code in (" + arrLoc + ") "
            qry = "select Location_Code as [Code] ,Location_Desc as [Name] from TSPL_LOCATION_MASTER where Location_Code in (" + arrLoc + ") "

            dt = clsDBFuncationality.GetDataTable(qry)
        End If

        If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
            btnGo.Enabled = False
        Else
            cbgMCC.DataSource = dt
            cbgMCC.ValueMember = "Code"
            cbgMCC.DisplayMember = "Name"
        End If
        

    End Sub
    Sub LoadTanker()
        Dim qry As String = "select Tanker_No as [Code] ,Tanker_Name as [Name] from TSPL_TANKER_MASTER   "
        cbgTanker.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgTanker.ValueMember = "Code"
        cbgTanker.DisplayMember = "Name"

    End Sub
    '=============================Update by Preeti Gupta Against Ticket No[BM00000008169,ERO/15/02/19-000489]======================
    Public Sub Load_Report()
        If txtFromDate.Value > txtToDate.Value Then
            common.clsCommon.MyMessageBoxShow("From date can not be greater then to Date")
            txtFromDate.Focus()
            Exit Sub
        End If
        If chkMCCSelect.IsChecked AndAlso cbgMCC.CheckedValue.Count = 0 Then
            clsCommon.MyMessageBoxShow("Please select atleast single MCC or select all.")
            Exit Sub
        End If
        If chkTankerSelect.IsChecked AndAlso cbgTanker.CheckedValue.Count = 0 Then
            clsCommon.MyMessageBoxShow("Please select atleast single Tanker or select all.")
            Exit Sub
        End If
        'Take care Chamber wise Ticket no- ERO/17/05/18-000315
        ' Ticket No : ERO/05/06/19-000636 by prabhakar - add [TS KG] 
        Dim sQuery As String = "select final.*,cast ((isnull(final.Trans_Fat_Kg- final.Des_FAT_Kg,0)) as Decimal(18,2)) as Fat_Diff,cast ((isnull(final.Trans_Snf_kg -final.Des_SNF_kg,0)) as Decimal(18,2)) as SNF_diff ,cast ((isnull (Des_Amount - Rec_Amount ,0)) as Decimal(18,2)) as Amount_Diff,cast (isnull( Fat_Per_Dis,0) as decimal(10,2)) +cast (isnull( SNF_Per_dis,0) as decimal(10,2)) as Dispatch_TS_Per,  cast (isnull (fat_per_trans,0) as Decimal(18,2)) +Cast ( isnull (SNF_Per_trans,0) as Decimal(18,2)) as Transation_TS_Per, isnull(Des_FAT_Kg,0)  + isnull(Des_SNF_kg,0)  as Dispatch_TS_KG , isnull(Trans_Fat_Kg,0)  +  isnull(Trans_Snf_kg,0)  as Transation_TS_KG  from(select xx.*,isnull(xx.Rec_Qty *xx.fat_per_trans /100,0) as Trans_Fat_Kg,isnull(xx.Rec_Qty *xx.SNF_Per_trans /100,0) as Trans_Snf_kg from ( "
        sQuery += "  select distinct TSPL_MCC_Dispatch_Challan.MCC_Code as From_Location_Code,Floc.Location_Desc as From_Location_Name,TSPL_MCC_Dispatch_Challan.Mcc_Or_Plant_Code  as To_Location_Code,Toloc.Location_Desc as To_Location_Description, " & _
            "TSPL_MCC_Dispatch_Challan.Sublocation_Code as Job_Work_Location_Code,Jobloc.Location_Desc as Job_Work_Location_Description,  " & _
            "convert(varchar,TSPL_MCC_Dispatch_Challan.Dispatch_Date,103) as Dispatch_Date  ,TSPL_MCC_Dispatch_Challan.MCC_Code +'-'+ Floc.Location_Desc as MCC_Code,Floc.Location_Desc as MCC_NAME ,TSPL_MCC_Dispatch_Challan.Chalan_NO as Doc_no,'' as [Return No],'' as ReturnDate,  " & _
            "TSPL_MCC_Dispatch_Challan.Tanker_No ,  " & _
            "t_FAT_dis .Param_Field_Value as Fat_Per_Dis,  " & _
            "t_SNF_dis .Param_Field_Value as SNF_Per_dis ,convert(varchar,TSPL_MILK_TRANSFER_IN.Receipt_Challan_Date,103) as Date_And_Time  ,  " & _
            "isnull(t_FAT_Trans .Param_Field_Value,0) as fat_per_trans,  " & _
            "isnull(t_SNF_Trans .Param_Field_Value,0) as SNF_Per_trans,  " & _
            " case when ISNULL(TSPL_MCC_DISPATCH_CHALLAN_DETAIL.SNO,0)=0 then isnull(TSPL_MCC_Dispatch_Challan .Net_Qty,0)else TSPL_MCC_DISPATCH_CHALLAN_DETAIL.Qty_KG end Des_Qty ," & _
            " case when ISNULL(TSPL_MCC_DISPATCH_CHALLAN_DETAIL.SNO,0)=0 then isnull(TSPL_MCC_Dispatch_Challan .Amount,0)else TSPL_MCC_DISPATCH_CHALLAN_DETAIL.Amount  end Des_Amount ," & _
            " case when ISNULL(TSPL_MCC_DISPATCH_CHALLAN_DETAIL.SNO,0)=0 then isnull(TSPL_MCC_Dispatch_Challan .SNF_KG,0)else TSPL_MCC_DISPATCH_CHALLAN_DETAIL.SNF_KG   end Des_SNF_kg ," & _
            " case when ISNULL(TSPL_MCC_DISPATCH_CHALLAN_DETAIL.SNO,0)=0 then isnull(TSPL_MCC_Dispatch_Challan .FAT_KG,0)else TSPL_MCC_DISPATCH_CHALLAN_DETAIL.FAT_KG   end Des_FAT_Kg ," & _
            "case when isnull(TSPL_MILK_TRANSFER_IN_RETURN.Receipt_Challan_Return_No,'')='' then isnull(TSPL_Weighment_Detail .Net_Weight,0)  else 0 end as Rec_Qty, case when isnull(TSPL_MILK_TRANSFER_IN_RETURN.Receipt_Challan_Return_No,'')='' then isnull(TSPL_Weighment_Detail .Amount,0)  else 0 end as Rec_Amount  from  " & _
            "TSPL_MCC_Dispatch_Challan left outer join TSPL_LOCATION_MASTER as Floc on TSPL_MCC_Dispatch_Challan.MCC_Code=Floc.Location_Code  " & _
            "left outer join TSPL_LOCATION_MASTER as Toloc on TSPL_MCC_Dispatch_Challan.Mcc_Or_Plant_Code=Toloc.Location_Code  " & _
            "left outer join TSPL_LOCATION_MASTER as Jobloc on TSPL_MCC_Dispatch_Challan.Sublocation_Code=Jobloc.Location_Code  " & _
            "left outer join TSPL_MCC_DISPATCH_CHALLAN_DETAIL on TSPL_MCC_DISPATCH_CHALLAN_DETAIL.Chalan_No=TSPL_MCC_Dispatch_Challan.Chalan_No " & _
            "Left Outer Join (Select TSPL_Mcc_Dispatch_Chalan_Parameter_Detail.*,TSPL_MCC_Dispatch_Challan.MCC_Code    From TSPL_MCC_Dispatch_Challan  " & _
            "Left Outer Join TSPL_Mcc_Dispatch_Chalan_Parameter_Detail On TSPL_Mcc_Dispatch_Chalan_Parameter_Detail.Chalan_No = TSPL_MCC_Dispatch_Challan.Chalan_NO And TSPL_Mcc_Dispatch_Chalan_Parameter_Detail.Param_Type = 'FAT') t_FAT_dis  On t_FAT_dis.Chalan_No = TSPL_MCC_Dispatch_Challan.Chalan_NO AND ISNULL(t_FAT_dis.SNO,0)=ISNULL(TSPL_MCC_DISPATCH_CHALLAN_DETAIL.SNO,0)" & _
            "Left Outer Join (Select TSPL_Mcc_Dispatch_Chalan_Parameter_Detail.*,TSPL_MCC_Dispatch_Challan.MCC_Code From TSPL_MCC_Dispatch_Challan Left Outer Join TSPL_Mcc_Dispatch_Chalan_Parameter_Detail On TSPL_Mcc_Dispatch_Chalan_Parameter_Detail.Chalan_No =TSPL_MCC_Dispatch_Challan.Chalan_NO And   " & _
            "TSPL_Mcc_Dispatch_Chalan_Parameter_Detail.Param_Type = 'SNF') t_SNF_dis On t_SNF_dis.Chalan_No = TSPL_MCC_Dispatch_Challan.Chalan_NO  AND ISNULL(t_SNF_dis.SNO,0)=ISNULL(TSPL_MCC_DISPATCH_CHALLAN_DETAIL.SNO,0)  " & _
            "left outer join TSPL_MILK_TRANSFER_IN  on  TSPL_MILK_TRANSFER_IN.Dispatch_Challan_No  =TSPL_MCC_Dispatch_Challan.Chalan_NO  " & _
            "left outer join TSPL_MILK_TRANSFER_IN_RETURN on TSPL_MILK_TRANSFER_IN.Receipt_Challan_No=TSPL_MILK_TRANSFER_IN_RETURN.Receipt_Challan_No  " & _
            "left join TSPL_Weighment_Detail on TSPL_MILK_TRANSFER_IN.Weighment_No=TSPL_Weighment_Detail.Weighment_No  " & _
            "Left Outer Join (Select TSPL_QC_Parameter_Detail.*    From TSPL_MILK_TRANSFER_IN    Left Outer Join TSPL_QC_Parameter_Detail On  " & _
            "TSPL_QC_Parameter_Detail.QC_No  = TSPL_MILK_TRANSFER_IN.Qc_No  And  " & _
            "TSPL_QC_Parameter_Detail.Param_Type = 'SNF') t_SNF_Trans On t_SNF_Trans.QC_No  = TSPL_MILK_TRANSFER_IN.Qc_No  " & _
            " AND ISNULL(t_SNF_Trans.LINE_NO,0)=ISNULL(TSPL_MCC_DISPATCH_CHALLAN_DETAIL.SNO,0)" & _
            "Left Outer Join (Select TSPL_QC_Parameter_Detail.*  From TSPL_MILK_TRANSFER_IN  Left Outer Join TSPL_QC_Parameter_Detail On TSPL_QC_Parameter_Detail.QC_No  = TSPL_MILK_TRANSFER_IN.Qc_No  And  " & _
            "TSPL_QC_Parameter_Detail.Param_Type = 'FAT') t_FAT_Trans On t_FAT_Trans.QC_No  = TSPL_MILK_TRANSFER_IN.Qc_No  " & _
            " AND ISNULL(t_FAT_Trans.LINE_NO,0)=ISNULL(TSPL_MCC_DISPATCH_CHALLAN_DETAIL.SNO,0)"
        sQuery += "   where 2 = 2 and  (isnull(TSPL_MILK_TRANSFER_IN.In_Return,0)=0  or  ( select count(Receipt_Challan_No) from TSPL_MILK_TRANSFER_IN where TSPL_MILK_TRANSFER_IN.Dispatch_Challan_No=TSPL_MCC_Dispatch_Challan.Chalan_NO)= (select count(Receipt_Challan_No) from TSPL_MILK_TRANSFER_IN_RETURN where TSPL_MILK_TRANSFER_IN_RETURN.Dispatch_Challan_No=TSPL_MCC_Dispatch_Challan.Chalan_NO) ) " & _
        " and  convert(date,TSPL_MCC_Dispatch_Challan.Dispatch_Date,103)>=convert(date,'" + txtFromDate.Value + "',103) and convert(date,TSPL_MCC_Dispatch_Challan.Dispatch_Date,103) <=convert(date,'" + txtToDate.Value + "' ,103)"
        If cbgMCC.CheckedValue.Count > 0 Then 'chkMCCSelect.IsChecked And
            sQuery += "and TSPL_MCC_Dispatch_Challan.MCC_Code  IN (" + clsCommon.GetMulcallString(cbgMCC.CheckedValue) + ") "
            End If
        If chkTankerSelect.IsChecked And cbgTanker.CheckedValue.Count > 0 Then
                'sQuery += " and TSPL_MILK_RECEIPT_DETAIL.VLC_CODE in (" + clsCommon.GetMulcallString(cbgTanker.CheckedValue) + ")  "
            sQuery += " and TSPL_MCC_Dispatch_Challan.Tanker_No in (" + clsCommon.GetMulcallString(cbgTanker.CheckedValue) + ")  "
            End If
            ' ============================================================
            ' For Tanker Dispatch Return 
        ' ============================================================

        sQuery += " Union All "
        sQuery += "  select Floc.Location_Code as From_Location_Code,Floc.Location_Desc as From_Location_Name,Toloc.Location_Code  as To_Location_Code,Toloc.Location_Desc as To_Location_Description, " & _
            "TSPL_MCC_Dispatch_Challan.Sublocation_Code as Job_Work_Location_Code,Jobloc.Location_Desc as Job_Work_Location_Description, " & _
            "convert(varchar,TSPL_MCC_Dispatch_Challan.Dispatch_Date,103) as Dispatch_Date  , " & _
            "TSPL_MCC_Dispatch_Challan.MCC_Code +'-'+ Floc.Location_Desc as MCC_Code,Floc.Location_Desc as MCC_NAME , " & _
            "TSPL_MCC_Dispatch_Challan.Chalan_NO as Doc_no,TSPL_MCC_DISPATCH_CHALLAN_RETURN.Document_No as [Return No],convert(varchar,TSPL_MCC_DISPATCH_CHALLAN_RETURN.Document_Date,103) as ReturnDate,TSPL_MCC_Dispatch_Challan.Tanker_No , " & _
            "t_FAT_dis .Param_Field_Value as Fat_Per_Dis, " & _
            "t_SNF_dis .Param_Field_Value as SNF_Per_dis ,'' as Date_And_Time  , " & _
            "'0' as fat_per_trans, " & _
            "'0' as SNF_Per_trans, " & _
            " ( case when ISNULL(TSPL_MCC_DISPATCH_CHALLAN_DETAIL.SNO,0)=0 then isnull(TSPL_MCC_Dispatch_Challan .Net_Qty,0)else TSPL_MCC_DISPATCH_CHALLAN_DETAIL.Qty_KG end) * (-1) as Des_Qty ," & _
            " ( case when ISNULL(TSPL_MCC_DISPATCH_CHALLAN_DETAIL.SNO,0)=0 then isnull(TSPL_MCC_Dispatch_Challan .Amount,0)else TSPL_MCC_DISPATCH_CHALLAN_DETAIL.Amount  end ) * (-1) as Des_Amount ," & _
            " ( case when ISNULL(TSPL_MCC_DISPATCH_CHALLAN_DETAIL.SNO,0)=0 then isnull(TSPL_MCC_Dispatch_Challan .SNF_KG,0)else TSPL_MCC_DISPATCH_CHALLAN_DETAIL.SNF_KG   end ) * (-1) as Des_SNF_kg ," & _
            " ( case when ISNULL(TSPL_MCC_DISPATCH_CHALLAN_DETAIL.SNO,0)=0 then isnull(TSPL_MCC_Dispatch_Challan .FAT_KG,0)else TSPL_MCC_DISPATCH_CHALLAN_DETAIL.FAT_KG   end ) * (-1) as  Des_FAT_Kg ," & _
            "'0' as Rec_Qty, '0' as Rec_Amount   from TSPL_MCC_DISPATCH_CHALLAN_RETURN " & _
            " left  join TSPL_MCC_Dispatch_Challan on TSPL_MCC_Dispatch_Challan.Chalan_No =TSPL_MCC_DISPATCH_CHALLAN_RETURN.Challan_No  " & _
            " left outer join TSPL_MCC_DISPATCH_CHALLAN_DETAIL on TSPL_MCC_DISPATCH_CHALLAN_DETAIL.Chalan_No=TSPL_MCC_Dispatch_Challan.Chalan_No " & _
            "left outer join TSPL_LOCATION_MASTER as Floc on TSPL_MCC_Dispatch_Challan.MCC_Code=Floc.Location_Code " & _
            "left outer join TSPL_LOCATION_MASTER as Toloc on TSPL_MCC_Dispatch_Challan.Mcc_Or_Plant_Code=Toloc.Location_Code " & _
            "left outer join TSPL_LOCATION_MASTER as Jobloc on TSPL_MCC_Dispatch_Challan.Sublocation_Code=Jobloc.Location_Code " & _
            "Left Outer Join (Select TSPL_Mcc_Dispatch_Chalan_Parameter_Detail.*,TSPL_MCC_Dispatch_Challan.MCC_Code From TSPL_MCC_Dispatch_Challan " & _
            "Left Outer Join TSPL_Mcc_Dispatch_Chalan_Parameter_Detail On TSPL_Mcc_Dispatch_Chalan_Parameter_Detail.Chalan_No =TSPL_MCC_Dispatch_Challan.Chalan_NO  " & _
            "And TSPL_Mcc_Dispatch_Chalan_Parameter_Detail.Param_Type = 'FAT') t_FAT_dis On t_FAT_dis.Chalan_No = TSPL_MCC_Dispatch_Challan.Chalan_NO  AND ISNULL(t_FAT_dis.SNO,0)=ISNULL(TSPL_MCC_DISPATCH_CHALLAN_DETAIL.SNO,0) " & _
            "Left Outer Join (Select TSPL_Mcc_Dispatch_Chalan_Parameter_Detail.*,TSPL_MCC_Dispatch_Challan.MCC_Code  From TSPL_MCC_Dispatch_Challan " & _
            "Left Outer Join TSPL_Mcc_Dispatch_Chalan_Parameter_Detail On TSPL_Mcc_Dispatch_Chalan_Parameter_Detail.Chalan_No = TSPL_MCC_Dispatch_Challan.Chalan_NO  " & _
            "And TSPL_Mcc_Dispatch_Chalan_Parameter_Detail.Param_Type = 'SNF') t_SNF_dis On t_SNF_dis.Chalan_No = TSPL_MCC_Dispatch_Challan.Chalan_NO AND ISNULL(t_SNF_dis.SNO,0)=ISNULL(TSPL_MCC_DISPATCH_CHALLAN_DETAIL.SNO,0)  "

        sQuery += "   where 2 = 2 and convert(date,TSPL_MCC_DISPATCH_CHALLAN_RETURN.Document_Date,103)>=convert(date,'" + txtFromDate.Value + "',103) and " & _
            "convert(date,TSPL_MCC_DISPATCH_CHALLAN_RETURN.Document_Date,103) <=convert(date,'" + txtToDate.Value + "' ,103)"
        If cbgMCC.CheckedValue.Count > 0 Then 'chkMCCSelect.IsChecked And
            sQuery += "and TSPL_MCC_Dispatch_Challan.MCC_Code  IN (" + clsCommon.GetMulcallString(cbgMCC.CheckedValue) + ") "
            End If
        If chkTankerSelect.IsChecked And cbgTanker.CheckedValue.Count > 0 Then
                'sQuery += " and TSPL_MILK_RECEIPT_DETAIL.VLC_CODE in (" + clsCommon.GetMulcallString(cbgTanker.CheckedValue) + ")  "
            sQuery += " and TSPL_MCC_Dispatch_Challan.Tanker_No in (" + clsCommon.GetMulcallString(cbgTanker.CheckedValue) + ")  "
            End If

            ' ============================ End ===========================


            ' For MCC Tanker Dispatch Return 

        sQuery += " Union All "

        sQuery += "  select Floc.Location_Code as From_Location_Code,Floc.Location_Desc as From_Location_Name,Toloc.Location_Code  as To_Location_Code,Toloc.Location_Desc as To_Location_Description, " & _
            "TSPL_MCC_Dispatch_Challan.Sublocation_Code as Job_Work_Location_Code,Jobloc.Location_Desc as Job_Work_Location_Description, " & _
            "convert(varchar,TSPL_MCC_Dispatch_Challan.Dispatch_Date,103) as Dispatch_Date  ,TSPL_MCC_Dispatch_Challan.MCC_Code +'-'+ floc.Location_Desc as MCC_Code,  " & _
            "floc.Location_Desc as MCC_NAME ,TSPL_MCC_TANKER_DISPATCH_RETURN_HEAD.Chalan_NO as Doc_no,TSPL_MCC_TANKER_DISPATCH_RETURN_HEAD.Return_NO  as [Return No],convert(varchar,TSPL_MCC_TANKER_DISPATCH_RETURN_HEAD.Return_Date,103) as ReturnDate,  " & _
            "TSPL_MCC_Dispatch_Challan.Tanker_No  ,  " & _
            "t_FAT_dis .Param_Field_Value as Fat_Per_Dis  ,  " & _
            "t_SNF_dis .Param_Field_Value as SNF_Per_dis  ,'' as Date_And_Time  ,  " & _
            "'0' as fat_per_trans  ,  " & _
            "'0' as SNF_Per_trans  ,  " & _
           " ( case when ISNULL(TSPL_MCC_TANKER_DISPATCH_RETURN_DETAIL.SNO,0)=0 then isnull(TSPL_MCC_TANKER_DISPATCH_RETURN_HEAD .Net_Qty,0)else TSPL_MCC_TANKER_DISPATCH_RETURN_DETAIL.Qty_KG end) * (-1) as Des_Qty ," & _
            " ( case when ISNULL(TSPL_MCC_TANKER_DISPATCH_RETURN_DETAIL.SNO,0)=0 then isnull(TSPL_MCC_TANKER_DISPATCH_RETURN_HEAD .Amount,0)else TSPL_MCC_TANKER_DISPATCH_RETURN_DETAIL.Amount  end ) * (-1) as Des_Amount ," & _
            " ( case when ISNULL(TSPL_MCC_TANKER_DISPATCH_RETURN_DETAIL.SNO,0)=0 then isnull(TSPL_MCC_TANKER_DISPATCH_RETURN_HEAD .SNF_KG,0)else TSPL_MCC_TANKER_DISPATCH_RETURN_DETAIL.SNF_KG   end ) * (-1) as Des_SNF_kg ," & _
            " ( case when ISNULL(TSPL_MCC_TANKER_DISPATCH_RETURN_DETAIL.SNO,0)=0 then isnull(TSPL_MCC_TANKER_DISPATCH_RETURN_HEAD .FAT_KG,0)else TSPL_MCC_TANKER_DISPATCH_RETURN_DETAIL.FAT_KG   end ) * (-1) as  Des_FAT_Kg ," & _
            "0 as Rec_Qty , 0 as Rec_Amount   from TSPL_MCC_TANKER_DISPATCH_RETURN_HEAD  " & _
           " left outer join TSPL_MCC_TANKER_DISPATCH_RETURN_DETAIL on TSPL_MCC_TANKER_DISPATCH_RETURN_DETAIL.Return_No=TSPL_MCC_TANKER_DISPATCH_RETURN_HEAD.Return_No " & _
           " left outer join TSPL_MCC_Dispatch_Challan on TSPL_MCC_Dispatch_Challan.Chalan_No=TSPL_MCC_TANKER_DISPATCH_RETURN_HEAD.Chalan_No " & _
            " left outer join TSPL_LOCATION_MASTER as Floc on TSPL_MCC_Dispatch_Challan.MCC_Code=Floc.Location_Code  " & _
            " left outer join TSPL_LOCATION_MASTER as Toloc on TSPL_MCC_Dispatch_Challan.Mcc_Or_Plant_Code=Toloc.Location_Code  " & _
            " left outer join TSPL_LOCATION_MASTER as Jobloc on TSPL_MCC_Dispatch_Challan.Sublocation_Code=Jobloc.Location_Code   " & _
            " Left Outer Join (Select TSPL_Mcc_Dispatch_Chalan_Return_Parameter_Detail.*,TSPL_MCC_TANKER_DISPATCH_RETURN_HEAD.MCC_Code From  " & _
            "TSPL_MCC_TANKER_DISPATCH_RETURN_HEAD  " & _
            "Left Outer Join TSPL_Mcc_Dispatch_Chalan_Return_Parameter_Detail On TSPL_Mcc_Dispatch_Chalan_Return_Parameter_Detail.Return_No = TSPL_MCC_TANKER_DISPATCH_RETURN_HEAD.Return_NO And  " & _
            "TSPL_Mcc_Dispatch_Chalan_Return_Parameter_Detail.Param_Type = 'FAT') t_FAT_dis On t_FAT_dis.Return_No = TSPL_MCC_TANKER_DISPATCH_RETURN_HEAD.Return_NO AND ISNULL(t_FAT_dis.SNO,0)=ISNULL(TSPL_MCC_TANKER_DISPATCH_RETURN_DETAIL.Chamber_No,0) " & _
            "Left Outer Join (  Select TSPL_Mcc_Dispatch_Chalan_Return_Parameter_Detail.*,TSPL_MCC_TANKER_DISPATCH_RETURN_HEAD.MCC_Code  From TSPL_MCC_TANKER_DISPATCH_RETURN_HEAD  " & _
            "Left Outer Join TSPL_Mcc_Dispatch_Chalan_Return_Parameter_Detail On TSPL_Mcc_Dispatch_Chalan_Return_Parameter_Detail.Return_No =TSPL_MCC_TANKER_DISPATCH_RETURN_HEAD.Return_NO And   " & _
            "TSPL_Mcc_Dispatch_Chalan_Return_Parameter_Detail.Param_Type = 'SNF') t_SNF_dis On t_SNF_dis.Return_No = TSPL_MCC_TANKER_DISPATCH_RETURN_HEAD.Return_NO AND ISNULL(t_SNF_dis.SNO,0)=ISNULL(TSPL_MCC_TANKER_DISPATCH_RETURN_DETAIL.Chamber_No,0)"
        sQuery += "   where 2 = 2 and convert(date,TSPL_MCC_TANKER_DISPATCH_RETURN_HEAD.Return_Date,103)>=convert(date,'" + txtFromDate.Value + "',103) and " & _
            "convert(date,TSPL_MCC_TANKER_DISPATCH_RETURN_HEAD.Return_Date,103) <=convert(date,'" + txtToDate.Value + "' ,103)"
        If cbgMCC.CheckedValue.Count > 0 Then 'chkMCCSelect.IsChecked And
            sQuery += "and TSPL_MCC_Dispatch_Challan.MCC_Code  IN (" + clsCommon.GetMulcallString(cbgMCC.CheckedValue) + ") "
            End If
        If chkTankerSelect.IsChecked And cbgTanker.CheckedValue.Count > 0 Then
                'sQuery += " and TSPL_MILK_RECEIPT_DETAIL.VLC_CODE in (" + clsCommon.GetMulcallString(cbgTanker.CheckedValue) + ")  "
            sQuery += " and TSPL_MCC_Dispatch_Challan.Tanker_No in (" + clsCommon.GetMulcallString(cbgTanker.CheckedValue) + ")  "    ''By balwinder On 29/06/2018 becuase tanker filter is not working. 
            End If
            ' For MCC Tanker Dispatch Return 
        sQuery += " )xx)final order by convert(datetime,Dispatch_Date,103), Doc_no  "

        Dim dtgv As New DataTable
        dtgv = clsDBFuncationality.GetDataTable(sQuery)
        If dtgv IsNot Nothing And dtgv.Rows.Count > 0 Then
            gv.DataSource = Nothing
            gv.Rows.Clear()
            gv.Columns.Clear()
            gv.DataSource = dtgv
            gv.GroupDescriptors.Clear()
            gv.MasterTemplate.SummaryRowsTop.Clear()
            FormatGrid()
            View()
            RadPageView1.SelectedPage = RadPageViewPage2
        Else
            clsCommon.MyMessageBoxShow("No Data Found")
            End If
        ReStoreGridLayout()
    End Sub

    Sub FormatGrid()
        ' Dim strItemCode, head2 As String

        gv.TableElement.TableHeaderHeight = 25
        gv.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gv.Columns.Count - 1
            gv.Columns(ii).ReadOnly = True
            gv.Columns(ii).IsVisible = False
            
        Next

        gv.Columns("MCC_Code").IsVisible = True
        gv.Columns("MCC_Code").Width = 100
        gv.Columns("MCC_Code").HeaderText = "Location"

        gv.Columns("Dispatch_Date").IsVisible = True
        gv.Columns("Dispatch_Date").Width = 100
        gv.Columns("Dispatch_Date").HeaderText = " Date"
        'gv.Columns("shift_date").FormatString = "{0:d}"

        gv.Columns("Doc_no").IsVisible = True
        gv.Columns("Doc_no").Width = 100
        gv.Columns("Doc_no").HeaderText = " Doc No"

        gv.Columns("From_Location_Code").IsVisible = True
        gv.Columns("From_Location_Code").Width = 100
        gv.Columns("From_Location_Code").HeaderText = "From Location Code"

        gv.Columns("From_Location_Name").IsVisible = True
        gv.Columns("From_Location_Name").Width = 100
        gv.Columns("From_Location_Name").HeaderText = "From Location Name"

        gv.Columns("To_Location_Code").IsVisible = True
        gv.Columns("To_Location_Code").Width = 100
        gv.Columns("To_Location_Code").HeaderText = "To Location Code"

        gv.Columns("To_Location_Description").IsVisible = True
        gv.Columns("To_Location_Description").Width = 100
        gv.Columns("To_Location_Description").HeaderText = "To Location Description"

        '*******************************************
        gv.Columns("Job_Work_Location_Code").IsVisible = True
        gv.Columns("Job_Work_Location_Code").Width = 100
        gv.Columns("Job_Work_Location_Code").HeaderText = "Job Work Location Code"

        gv.Columns("Job_Work_Location_Description").IsVisible = True
        gv.Columns("Job_Work_Location_Description").Width = 100
        gv.Columns("Job_Work_Location_Description").HeaderText = "Job Work Location Description"

        '*******************************************

        gv.Columns("Return No").IsVisible = True
        gv.Columns("Return No").Width = 100
        gv.Columns("Return No").HeaderText = "Return No"

        gv.Columns("ReturnDate").IsVisible = True
        gv.Columns("ReturnDate").Width = 100
        gv.Columns("ReturnDate").HeaderText = "Return Date"


        gv.Columns("Tanker_No").IsVisible = True
        gv.Columns("Tanker_No").Width = 100
        gv.Columns("Tanker_No").HeaderText = " Tanker No"
        'gv.Columns("shift_date").FormatString = "{0:d}"

        gv.Columns("Fat_Per_Dis").IsVisible = True
        gv.Columns("Fat_Per_Dis").Width = 100
        gv.Columns("Fat_Per_Dis").HeaderText = "FAT%"

        gv.Columns("SNF_Per_dis").IsVisible = True
        gv.Columns("SNF_Per_dis").Width = 100
        gv.Columns("SNF_Per_dis").HeaderText = "SNF%"

        gv.Columns("Dispatch_TS_Per").IsVisible = True
        gv.Columns("Dispatch_TS_Per").Width = 100
        gv.Columns("Dispatch_TS_Per").HeaderText = "TS%"



        gv.Columns("Date_And_Time").IsVisible = True
        gv.Columns("Date_And_Time").Width = 80
        gv.Columns("Date_And_Time").HeaderText = "Recd Date"

        gv.Columns("SNF_Per_trans").IsVisible = True
        gv.Columns("SNF_Per_trans").Width = 80
        gv.Columns("SNF_Per_trans").HeaderText = "SNF%"

        gv.Columns("fat_per_trans").IsVisible = True
        gv.Columns("fat_per_trans").Width = 50
        gv.Columns("fat_per_trans").HeaderText = "FAT%"

        gv.Columns("Transation_TS_Per").IsVisible = True
        gv.Columns("Transation_TS_Per").Width = 50
        gv.Columns("Transation_TS_Per").HeaderText = "TS%"



        gv.Columns("Des_Qty").IsVisible = True
        gv.Columns("Des_Qty").Width = 100
        gv.Columns("Des_Qty").HeaderText = "Desp Qty"

        gv.Columns("Des_Amount").IsVisible = True
        gv.Columns("Des_Amount").Width = 100
        gv.Columns("Des_Amount").HeaderText = "Desp Amount"

        gv.Columns("Des_FAT_Kg").IsVisible = True
        gv.Columns("Des_FAT_Kg").Width = 100
        gv.Columns("Des_FAT_Kg").HeaderText = "Disp FAT (Kg)"

        gv.Columns("Des_SNF_kg").IsVisible = True
        gv.Columns("Des_SNF_kg").Width = 100
        gv.Columns("Des_SNF_kg").HeaderText = "Disp SNF (kg)"

        'Dispatch_TS_KG
        gv.Columns("Dispatch_TS_KG").IsVisible = True
        gv.Columns("Dispatch_TS_KG").Width = 100
        gv.Columns("Dispatch_TS_KG").HeaderText = "Disp TS (kg)"

        gv.Columns("Rec_Qty").IsVisible = True
        gv.Columns("Rec_Qty").Width = 100
        gv.Columns("Rec_Qty").HeaderText = "Recd Qty"

        gv.Columns("Rec_Amount").IsVisible = True
        gv.Columns("Rec_Amount").Width = 100
        gv.Columns("Rec_Amount").HeaderText = "Recd Amount"

        gv.Columns("Trans_Fat_Kg").IsVisible = True
        gv.Columns("Trans_Fat_Kg").Width = 100
        gv.Columns("Trans_Fat_Kg").HeaderText = "Recd FAT(Kg)"

        gv.Columns("Trans_Snf_kg").IsVisible = True
        gv.Columns("Trans_Snf_kg").Width = 100
        gv.Columns("Trans_Snf_kg").HeaderText = "Recd SNF (Kg)"

        gv.Columns("Transation_TS_KG").IsVisible = True
        gv.Columns("Transation_TS_KG").Width = 100
        gv.Columns("Transation_TS_KG").HeaderText = "Recd TS (Kg)"


        gv.Columns("Fat_Diff").IsVisible = True
        gv.Columns("Fat_Diff").Width = 100
        gv.Columns("Fat_Diff").HeaderText = "FAT Diff"

        gv.Columns("SNF_diff").IsVisible = True
        gv.Columns("SNF_diff").Width = 100
        gv.Columns("SNF_diff").HeaderText = "SNF Diff"

        gv.Columns("Amount_Diff").IsVisible = True
        gv.Columns("Amount_Diff").Width = 100
        gv.Columns("Amount_Diff").HeaderText = "Amount Diff"


        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim intCount As Integer = 0

        Dim item1 As New GridViewSummaryItem("Des_Qty", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)
        Dim item2 As New GridViewSummaryItem("Des_FAT_Kg", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item2)
        Dim item3 As New GridViewSummaryItem("Des_SNF_kg", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item3)
        Dim item4 As New GridViewSummaryItem("Rec_Qty", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item4)
        Dim item5 As New GridViewSummaryItem("Trans_Fat_Kg", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item5)
        Dim item6 As New GridViewSummaryItem("Trans_Snf_kg", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item6)
        ' Ticket : BHA/01/11/18-000655 by Prabhakar Anand
        Dim item7 As New GridViewSummaryItem("Des_Amount", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item7)
        Dim item8 As New GridViewSummaryItem("Rec_Amount", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item8)

        Dim item9 As New GridViewSummaryItem("Dispatch_TS_KG", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item9)
        Dim item10 As New GridViewSummaryItem("Transation_TS_KG", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item10)

        'Dim item7 As New GridViewSummaryItem("amount", "{0:F2}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(item7)
        gv.GroupDescriptors.Add(New GridGroupByExpression("MCC_Code as Item format ""{0}: {1}"" Group By MCC_Code"))
        'gv.GroupDescriptors.Add(New GridGroupByExpression("Location_Desc as Item format ""{0}: {1}"" Group By Location_Desc"))
        'gv.GroupDescriptors.Add(New GridGroupByExpression("VLC as Item format ""{0}: {1}"" Group By VLC"))

        gv.ShowGroupPanel = False
        gv.MasterTemplate.AutoExpandGroups = True

        gv.MasterTemplate.SummaryRowsTop.Add(summaryRowItem)

    End Sub
    Sub View()
        If gv.Rows.Count > 0 Then
            Dim view As New ColumnGroupsViewDefinition()


            view.ColumnGroups.Add(New GridViewColumnGroup(""))

            view.ColumnGroups(0).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv.Columns("Dispatch_Date").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv.Columns("From_Location_Code").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv.Columns("From_Location_Name").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv.Columns("To_Location_Code").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv.Columns("To_Location_Description").Name)
            '**************************************************************************************
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv.Columns("Job_Work_Location_Code").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv.Columns("Job_Work_Location_Description").Name)
            '******************************************************************************************
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv.Columns("Doc_no").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv.Columns("Return No").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv.Columns("ReturnDate").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv.Columns("Tanker_No").Name)

            view.ColumnGroups.Add(New GridViewColumnGroup("Dispatch Percentages"))
            view.ColumnGroups(1).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv.Columns("Fat_Per_Dis").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv.Columns("SNF_Per_dis").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv.Columns("Dispatch_TS_Per").Name)


            view.ColumnGroups.Add(New GridViewColumnGroup(""))
            view.ColumnGroups(2).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv.Columns("Date_And_Time").Name)

            view.ColumnGroups.Add(New GridViewColumnGroup("Receiving Percentages"))
            view.ColumnGroups(3).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(3).Rows(0).ColumnNames.Add(gv.Columns("fat_per_trans").Name)
            view.ColumnGroups(3).Rows(0).ColumnNames.Add(gv.Columns("SNF_Per_trans").Name)
            view.ColumnGroups(3).Rows(0).ColumnNames.Add(gv.Columns("Transation_TS_Per").Name)

            view.ColumnGroups.Add(New GridViewColumnGroup("Dispatch Quantites"))
            view.ColumnGroups(4).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(4).Rows(0).ColumnNames.Add(gv.Columns("Des_Qty").Name)
            view.ColumnGroups(4).Rows(0).ColumnNames.Add(gv.Columns("Des_FAT_Kg").Name)
            view.ColumnGroups(4).Rows(0).ColumnNames.Add(gv.Columns("Des_SNF_kg").Name)
            view.ColumnGroups(4).Rows(0).ColumnNames.Add(gv.Columns("Dispatch_TS_KG").Name)
            view.ColumnGroups(4).Rows(0).ColumnNames.Add(gv.Columns("Des_Amount").Name)


            view.ColumnGroups.Add(New GridViewColumnGroup("Receiving Quantites"))
            view.ColumnGroups(5).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(5).Rows(0).ColumnNames.Add(gv.Columns("Rec_Qty").Name)
            view.ColumnGroups(5).Rows(0).ColumnNames.Add(gv.Columns("Trans_Fat_Kg").Name)
            view.ColumnGroups(5).Rows(0).ColumnNames.Add(gv.Columns("Trans_Snf_kg").Name)
            view.ColumnGroups(5).Rows(0).ColumnNames.Add(gv.Columns("Transation_TS_KG").Name)
            view.ColumnGroups(5).Rows(0).ColumnNames.Add(gv.Columns("Rec_Amount").Name)


            view.ColumnGroups.Add(New GridViewColumnGroup("Differences"))
            view.ColumnGroups(6).Rows.Add(New GridViewColumnGroupRow())

            view.ColumnGroups(6).Rows(0).ColumnNames.Add(gv.Columns("Fat_Diff").Name)
            view.ColumnGroups(6).Rows(0).ColumnNames.Add(gv.Columns("SNF_diff").Name)
            view.ColumnGroups(6).Rows(0).ColumnNames.Add(gv.Columns("Amount_Diff").Name)



            gv.ViewDefinition = view
        End If

    End Sub

    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(MyBase.Form_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(Form_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gv.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gv.Columns.Count - 1 Step ii + 1
                        gv.Columns(ii).IsVisible = False
                        gv.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gv.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                    If obj.arrGVF IsNot Nothing AndAlso obj.arrGVF.Count > 0 Then
                        Dim view As New ColumnGroupsViewDefinition()
                        ii = -1
                        For Each objtr As clsGridLayoutViewDefinitation In obj.arrGVF
                            If ii <> objtr.GroupIndex Then
                                ii = objtr.GroupIndex
                                view.ColumnGroups.Add(New GridViewColumnGroup(objtr.GroupText))
                                view.ColumnGroups(ii).Rows.Add(New GridViewColumnGroupRow())
                            End If
                            view.ColumnGroups(ii).Rows(0).ColumnNames.Add(gv.Columns(objtr.GroupColumnName).Name)
                        Next
                        gv.ViewDefinition = view
                    End If
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub

    Private Sub rmSaveLayout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmSaveLayout.Click
        If clsCommon.myLen(MyBase.Form_ID) > 0 Then
            gv.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = MyBase.Form_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv.SaveLayout(obj.GridLayout)
            obj.GridColumns = gv.ColumnCount
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            Dim view As ColumnGroupsViewDefinition = TryCast(gv.ViewDefinition, ColumnGroupsViewDefinition)
            If view IsNot Nothing AndAlso view.ColumnGroups.Count > 0 Then
                obj.arrGVF = New List(Of clsGridLayoutViewDefinitation)
                For ii As Integer = 0 To view.ColumnGroups.Count - 1
                    If view.ColumnGroups(ii).Rows(0).ColumnNames.Count > 0 Then
                        For jj As Integer = 0 To view.ColumnGroups(ii).Rows(0).ColumnNames.Count - 1
                            Dim objtr As New clsGridLayoutViewDefinitation
                            objtr.GroupIndex = ii
                            objtr.GroupText = view.ColumnGroups(ii).Text
                            objtr.GroupColumnIndex = jj
                            'objtr.GroupColumnName = view.ColumnGroups(ii).Rows(0).Columns(jj).Name ''TELERIK2015->2022
                            objtr.GroupColumnName = clsCommon.myCstr(jj)
                            obj.arrGVF.Add(objtr)
                        Next
                    End If
                Next
            End If

            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
            End If


            ''richa agarwal regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
            ''---------------
        End If
    End Sub

    

    Sub Reset()
        LOCATIONRIGTHS()
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = txtToDate.Value.AddMonths(-1)
        LoadMCC()
        LoadTanker()
        chkMCCAll.CheckState = CheckState.Checked
        chkTankerAll.CheckState = CheckState.Checked
        If chkMCCAll.IsChecked Then
            cbgMCC.CheckedAll()
        Else
            cbgMCC.UnCheckedAll()
        End If
        gv.DataSource = Nothing
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

    Private Sub RptTankerSummaryReport_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.R Then
            Load_Report()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            Reset()
        End If
    End Sub
    Sub print(ByVal exporter As EnumExportTo)
        Try
            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            If chkMCCSelect.IsChecked Then
                Dim strMCCName As String = ""
                For Each StrName As String In cbgMCC.CheckedDisplayMember
                    If clsCommon.myLen(strMCCName) > 0 Then
                        strMCCName += ", "
                    End If
                    strMCCName += StrName
                Next
                Dim strMCCCode As String = ""
                For Each StrCode As String In cbgMCC.CheckedValue
                    If clsCommon.myLen(strMCCCode) > 0 Then
                        strMCCCode += ", "
                    End If
                    strMCCCode += StrCode
                Next
                arrHeader.Add((" MCC Name: " + strMCCName + " "))
            End If
            If chkTankerSelect.IsChecked Then
                Dim strTankerName As String = ""
                For Each StrName As String In cbgTanker.CheckedDisplayMember
                    If clsCommon.myLen(strTankerName) > 0 Then
                        strTankerName += ", "
                    End If
                    strTankerName += StrName
                Next

                arrHeader.Add(("Tanker Name: " + strTankerName + " "))
            End If
            
            If exporter = EnumExportTo.Excel Then
                transportSql.applyExportTemplate(gv, PageSetupReport_ID)
                clsCommon.MyExportToExcelGrid("TANKER SUMMARY REPORT", gv, arrHeader, Me.Text)
            Else
                clsCommon.MyExportToPDF("TANKER SUMMARY REPORT", gv, arrHeader, Me.Text, True)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub


    Private Sub RptTankerSummaryReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnGo, "Press Alt+R Refresh ")
        ButtonToolTip.SetToolTip(BtnReset, "Press Alt+N Adding New ")
      

        gv.DataSource = Nothing
        RadPageView1.SelectedPage = RadPageViewPage1
        Reset()
    End Sub

  

    Private Sub btnGo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGo.Click
        PageSetupReport_ID = MyBase.Form_ID
        TemplateGridview = gv
        Load_Report()
    End Sub

    Private Sub BtnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnReset.Click
        Reset()
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub chkMCCAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkMCCAll.ToggleStateChanged
        cbgMCC.Enabled = Not chkMCCAll.IsChecked
        If chkMCCAll.IsChecked Then
            cbgMCC.CheckedAll()
        Else
            cbgMCC.UnCheckedAll()
        End If
    End Sub

    Private Sub chkTankerAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkTankerAll.ToggleStateChanged
        cbgTanker.Enabled = Not chkTankerAll.IsChecked
    End Sub

    

    Private Sub rmDeleteLayout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(MyBase.Form_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow("Layout Delete successfully", "Information")
    End Sub


    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        Try
            print(EnumExportTo.Excel)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.RptTankerSummaryReport & "'"))

            If chkMCCSelect.IsChecked Then
                Dim strMCCName As String = ""
                For Each StrName As String In cbgMCC.CheckedDisplayMember
                    If clsCommon.myLen(strMCCName) > 0 Then
                        strMCCName += ", "
                    End If
                    strMCCName += StrName
                Next
                Dim strMCCCode As String = ""
                For Each StrCode As String In cbgMCC.CheckedValue
                    If clsCommon.myLen(strMCCCode) > 0 Then
                        strMCCCode += ", "
                    End If
                    strMCCCode += StrCode
                Next
                arrHeader.Add((" MCC Name: " + strMCCName + " "))
            End If
            If chkTankerSelect.IsChecked Then
                Dim strTankerName As String = ""
                For Each StrName As String In cbgTanker.CheckedDisplayMember
                    If clsCommon.myLen(strTankerName) > 0 Then
                        strTankerName += ", "
                    End If
                    strTankerName += StrName
                Next
                arrHeader.Add(("Tanker Name: " + strTankerName + " "))
            End If
            transportSql.applyExportTemplate(gv, PageSetupReport_ID)
            clsCommon.MyExportToPDF("TANKER SUMMARY REPORT", gv, arrHeader, "TANKER SUMMARY REPORT", PageSetupReport_ID, objCommonVar.CurrentUserCode)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
End Class
