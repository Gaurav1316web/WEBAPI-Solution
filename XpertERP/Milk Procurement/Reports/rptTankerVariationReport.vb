Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common
Imports System.IO
Public Class RptTankerVariationReport
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    'preeti gupta ticket no.[BM00000004524]
    'Dim arrLoc As String = Nothing
    Dim StrPermission As String
    'Private Sub LOCATIONRIGTHS()
    '    Try
    '        Dim obj As New clsMCCCodes()
    '        obj = clsMCCCodes.GetData()

    '        If obj.arrLocCodes IsNot Nothing AndAlso clsCommon.myLen(obj.arrLocCodes) > 0 Then
    '            arrLoc = obj.arrLocCodes
    '        End If

    '    Catch ex As Exception
    '        clsCommon.MyMessageBoxShow(ex.Message)
    '    End Try
    'End Sub
    Private Sub SetUserMgmtNew()

        ''MyBase.SetUserMgmt(clsUserMgtCode.RptTankerVariation)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        RadSplitButton1.Visible = MyBase.isExport
    End Sub
    Sub LoadMCC()
        'Dim qry As String = "select MCC_Code as [Code] ,MCC_NAME as [Name] from TSPL_MCC_MASTER  "
        Dim qry As String = Nothing
        Dim dt As DataTable = Nothing
        If clsCommon.myLen(StrPermission) > 0 Then
            qry = "select MCC_Code as [Code] ,MCC_NAME as [Name] from TSPL_MCC_MASTER where MCC_Code in (" + StrPermission + ") "
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
    Sub LoadPlant()
        'Dim qry As String = "select Location_Code as [Code],Location_Desc as [Name] from TSPL_LOCATION_MASTER where Type ='Plant'  "

        Dim qry As String = Nothing
        If clsCommon.myLen(StrPermission) > 0 Then

            qry = "select Location_Code as [Code],Location_Desc as [Name] from TSPL_LOCATION_MASTER where  isnull(CSA_Type,'N') ='N' and (isnull(GIT_Type,'N')='N' or isnull(GIT_Type,'N')='') and isnull(Is_Consumption_Location,0) =0  and isnull(Rejected_type,'N') ='N'  and Type ='Plant' and  Location_Code in (" + StrPermission + ") "

        Else
            btnGo.Enabled = False
        End If
        cbgPlant.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgPlant.ValueMember = "Code"
        cbgPlant.DisplayMember = "Name"

    End Sub
    Sub LoadTankerNo()
        Dim qry As String = "select Tanker_No  as [Code] ,Tanker_Name  as [Name]   from TSPL_TANKER_MASTER  "
        cbgTanker.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgTanker.ValueMember = "Code"
        cbgTanker.DisplayMember = "Name"

    End Sub
    Sub LoadTransporter()
        Dim qry As String = "select Tanker_Transporter_Code   as [Code] ,Description as [Name]     from TSPL_TANKER_MASTER  "
        cbgTrans.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgTrans.ValueMember = "Code"
        cbgTrans.DisplayMember = "Name"

    End Sub
    Sub Reset()
        'txtToDate.Value = clsCommon.GETSERVERDATE()
        'txtFromDate.Value = txtToDate.Value.AddMonths(-1)
        'LOCATIONRIGTHS()
        StrPermission = clsERPFuncationality.UserWiseAvailableLocationCode()
        LoadMCC()
        LoadPlant()
        LoadTankerNo()
        'cboUnit.Text = "Kg"
        chkMCCAll.CheckState = CheckState.Checked
        If chkMCCAll.IsChecked Then
            cbgMCC.CheckedAll()
        Else
            cbgMCC.UnCheckedAll()
        End If
        If ChkPlantAll.IsChecked Then
            cbgPlant.CheckedAll()
        Else
            cbgPlant.UnCheckedAll()
        End If
        LoadTransporter()
        chkMCCAll.CheckState = CheckState.Checked
        ChkPlantAll.CheckState = CheckState.Checked
        chkTankerAll.CheckState = CheckState.Checked
        ChkTransAll.CheckState = CheckState.Checked
        gv.DataSource = Nothing
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub
    Public Sub Load_Report()
        ''adding column Transport Code,Transport Name by shivani against[BM00000008360]
        If txtFromDate.Value > txtToDate.Value Then
            common.clsCommon.MyMessageBoxShow(Me, "From date can not be greater then to Date", Me.Text)
            txtFromDate.Focus()
            Exit Sub
        End If
        If chkMCCSelect.IsChecked AndAlso cbgMCC.CheckedValue.Count = 0 Then
            clsCommon.MyMessageBoxShow("Please select atleast single MCC or select all.", Me.Text)
            Exit Sub
        End If
        If chkPlantSelect.IsChecked AndAlso cbgPlant.CheckedValue.Count = 0 Then
            clsCommon.MyMessageBoxShow("Please select atleast single Plant or select all.", Me.Text)
            Exit Sub
        End If
        If chkTankerSelect.IsChecked AndAlso cbgTanker.CheckedValue.Count = 0 Then
            clsCommon.MyMessageBoxShow("Please select atleast single Tanker or select all.", Me.Text)
            Exit Sub
        End If
        If Chktransselect.IsChecked AndAlso cbgTrans.CheckedValue.Count = 0 Then
            clsCommon.MyMessageBoxShow("Please select atleast single Transporter or select all.", Me.Text)
            Exit Sub
        End If

        Dim sQuery As String = "select YY .IssuedBy ,YY.RecdAt ,YY.Dispatch_Date ,YY.Dispatch_Time ,YY.Chalan_NO as Doc_No,YY.Desp_Chemist_Name,YY.Tanker_No ,YY.Tanker_Capacity,yy.Tanker_Transporter_Code,yy.Description ,YY.DispSealNo ,convert(decimal(18,2),YY.Desp_Qty) as Desp_Qty ,convert(decimal(18,2),YY.Desp_TEMP) as Desp_TEMP ,convert(decimal(18,2),YY.Desp_FAT) as Desp_FAT ,convert(decimal(18,2),yy.Desp_CLR) as Desp_CLR ,convert(decimal(18,2),YY.desp_SNF) as desp_SNF ,convert(decimal(18,3),YY.DESPFATKg) as DESPFATKg ,convert(decimal(18,3),YY.DESPSNFKg)  as DESPSNFKg,YY.GateInTime,convert(decimal(18,3),YY.DESPSNFKg) as DESPSNFKg,convert(decimal(18,2),YY.dis_sample_snf) as dis_sample_snf,convert(decimal(18,2),YY.Dis_sample_fat) as Dis_sample_fat ,YY.UnloadingTime ,YY.GateOutTime ,yy.Gate_Entry_Date,YY.RECDDATE ,YY.RECD_Chemist_Name ,convert(decimal(18,2),YY.RECDQTY) as RECDQTY ,convert(decimal(18,2),YY.TEMP_Recd) as TEMP_Recd ,YY.FAT_Recd,convert(decimal(18,2),YY.CLR_Recd) as CLR_Recd  ,convert(decimal(18,2),YY.SNF_Recd) as SNF_Recd,convert(decimal(18,3),YY.RECDFATKg) as RECDFATKg ,convert(decimal(18,3),YY.RECDSNFKg) as RECDSNFKg ,convert(decimal(18,2),YY.DiffQty) as DiffQty ,convert(decimal(18,3),YY.DESPFATKg -YY.RECDFATKg ) as Diff_FAT,convert(decimal(18,3),YY.DESPSNFKg -YY.RECDSNFKg)  as Diff_SNF,convert(decimal(18,2),YY.Recd_Sample_Fat) as Recd_Sample_Fat ,convert(decimal(18,2),YY.Recd_Sample_snf) as Recd_Sample_snf ,YY.KMS ,YY.TimeTakenByTankerInTransit ,YY.TimeTakenForUnloadingPlant    from  (select xx.*,xx.Desp_FAT  *xx.Desp_Qty  /100 as DESPFATKg,xx.desp_SNF  *Desp_Qty  /100 as DESPSNFKg,xx.FAT_Recd   *xx.RECDQTY  /100 as RECDFATKg,xx.SNF_Recd   *RECDQTY   /100 as RECDSNFKg,(xx.Desp_Qty -xx.RECDQTY)  as DiffQty,"
        sQuery += " cast(datediff(ss,xx.GateInTime,xx.Dispatch_Time)/3600 as varchar)+':'+"
        sQuery += " cast((datediff(ss,xx.GateInTime,xx.Dispatch_Time)%3600)/60 as varchar)+':'+ "
        sQuery += " cast(datediff(ss,xx.GateInTime,xx.Dispatch_Time)%60 as varchar) as TimeTakenByTankerInTransit,"
        sQuery += " cast(datediff(ss,xx.UnloadingTime,xx.GateInTime )/3600 as varchar)+':'+"
        sQuery += " cast((datediff(ss,xx.UnloadingTime,xx.GateInTime)%3600)/60 as varchar)+':'+ "
        sQuery += "cast(datediff(ss,xx.UnloadingTime,xx.GateInTime)%60 as varchar) as TimeTakenForUnloadingPlant"
        sQuery += " from  (select  TSPL_MCC_Dispatch_Challan.MCC_Code  as MCC_Code,tspl_mcc_master.mcc_name as  IssuedBy ,TSPL_MCC_Dispatch_Challan.Mcc_Or_Plant_Code  as Mcc_Or_Plant_Code,coalesce(tspl_location_master.location_desc,ToMcc.mcc_name) as RecdAt,convert(varchar ,TSPL_MCC_Dispatch_Challan.Dispatch_Date,103) as  Dispatch_Date,convert(Time,TSPL_MCC_Dispatch_Challan.Dispatch_Date,103) as  Dispatch_Time,TSPL_MCC_Dispatch_Challan.Chalan_NO ,TSPL_MCC_Dispatch_Challan.Chemist_Name as Desp_Chemist_Name ,TSPL_MCC_Dispatch_Challan.Tanker_No ,TSPL_TANKER_MASTER.Storage_Capacity  as Tanker_Capacity,"
        sQuery += " TSPL_MCC_Dispatch_Challan.Seal_No1  +case when len(TSPL_MCC_Dispatch_Challan.Seal_No2)>0 then ', '+TSPL_MCC_Dispatch_Challan.Seal_No2 else '' end +case when LEN(isnull(TSPL_MCC_Dispatch_Challan.Seal_No3,''))>0 then ', '+isnull(TSPL_MCC_Dispatch_Challan.Seal_No3,'') else ' ' end +case when LEN(isnull(TSPL_MCC_Dispatch_Challan.Seal_No4,''))>0 then ', '+isnull(TSPL_MCC_Dispatch_Challan.Seal_No4,'') else ' ' end +case when LEN(isnull(TSPL_MCC_Dispatch_Challan.Seal_No5,''))>0 then ', '+isnull(TSPL_MCC_Dispatch_Challan.Seal_No5,'') else ' ' end +case when LEN(isnull(TSPL_MCC_Dispatch_Challan.Seal_No6,''))>0 then ', '+isnull(TSPL_MCC_Dispatch_Challan.Seal_No6,'') else ' ' end +case when LEN(isnull(TSPL_MCC_Dispatch_Challan.Seal_No7,''))>0 then ', '+isnull(TSPL_MCC_Dispatch_Challan.Seal_No7,'') else ' ' end +case when LEN(isnull(TSPL_MCC_Dispatch_Challan.Seal_No8,''))>0 then ', '+isnull(TSPL_MCC_Dispatch_Challan.Seal_No8,'') else ' ' end +case when LEN(isnull(TSPL_MCC_Dispatch_Challan.Seal_No9,''))>0 then ', '+isnull(TSPL_MCC_Dispatch_Challan.Seal_No9,'') else ' ' end "
        sQuery += " +case when LEN(isnull(TSPL_MCC_Dispatch_Challan.Seal_No10,''))>0 then ', '+isnull(TSPL_MCC_Dispatch_Challan.Seal_No10,'') else ' ' end as DispSealNo,"

        sQuery += " TSPL_MCC_DISPATCH_CHALLAN_DETAIL.Qty_KG  as Desp_Qty,TSPL_MCC_Dispatch_Challan.control_sample_fat as Dis_sample_fat   ,TSPL_MCC_Dispatch_Challan.control_sample_snf  as Dis_sample_snf"
        sQuery += " ,convert(float,t_FAT_Desp .Param_Field_Value) as Desp_FAT ,convert(float,t_SNF_Desp .Param_Field_Value) as desp_SNF,convert(float,t_CLR_Desp .Param_Field_Value) as Desp_CLR,convert(float,t_TEMP_Desp .Param_Field_Value)  as Desp_TEMP"
        sQuery += " ,convert(time,Tspl_Gate_Entry_Details.Date_And_Time) as GateInTime,convert(time,Tspl_Gate_Entry_Details.Date_And_Time) as UnloadingTime,convert(time,TSPL_Gate_Out.Doc_Date)  as GateOutTime,Convert(varchar,TSPL_MILK_TRANSFER_IN.Receipt_Challan_Date,103 ) as RECDDATE,convert(varchar,Tspl_Gate_Entry_Details .Date_And_Time,103 ) as 'Gate_Entry_Date','' as RECD_Chemist_Name,TSPL_WEIGHMENT_CHEMBER_DETAILS.Net_Weight  as RECDQTY,convert(float,t_FAT_Recd .Param_Field_Value)  AS FAT_Recd,convert(float,t_SNF_Recd  .Param_Field_Value)  AS SNF_Recd,convert(float,t_CLR_Recd  .Param_Field_Value)  AS CLR_Recd,t_Temp_Recd  .Param_Field_Value  AS TEMP_Recd,(TSPL_MILK_TRANSFER_IN.km_reading_receipt-TSPL_MCC_Dispatch_Challan.Tanker_KM_Reading) as KMS,TSPL_MILK_TRANSFER_IN.Receipt_Control_FAT as Recd_Sample_Fat,TSPL_MILK_TRANSFER_IN.Receipt_Control_SNF as Recd_Sample_snf,TSPL_TANKER_MASTER.Tanker_Transporter_Code,TSPL_TANKER_MASTER.Description"
        sQuery += " from TSPL_MCC_Dispatch_Challan"
        sQuery += " left outer join TSPL_MCC_DISPATCH_CHALLAN_DETAIL on TSPL_MCC_Dispatch_Challan.Chalan_NO=TSPL_MCC_DISPATCH_CHALLAN_DETAIL.Chalan_No "
        sQuery += " left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER .MCC_Code =TSPL_MCC_Dispatch_Challan.MCC_Code "
        sQuery += " left outer join Tspl_Gate_Entry_Details on Tspl_Gate_Entry_Details.Challan_No =TSPL_MCC_Dispatch_Challan.Chalan_NO "
        sQuery += " left outer join TSPL_LOCATION_MASTER  on TSPL_LOCATION_MASTER .Location_Code =Tspl_Gate_Entry_Details.location_Code"
        sQuery += " left outer join TSPL_MCC_MASTER ToMcc on ToMcc.MCC_Code =TSPL_MCC_Dispatch_Challan.Mcc_Or_Plant_Code "
        sQuery += " left outer join TSPL_MILK_UNLOADING on TSPL_MILK_UNLOADING.Gate_Entry_No =Tspl_Gate_Entry_Details.Gate_Entry_No "
        sQuery += " left outer join TSPL_Gate_Out on TSPL_Gate_Out.Gate_Entry_No =TSPL_MILK_UNLOADING.Gate_Entry_No "
        sQuery += " left outer join TSPL_MILK_TRANSFER_IN on TSPL_MILK_TRANSFER_IN.Gate_Entry_no =TSPL_Gate_Out.Gate_Entry_No "
        sQuery += " left outer join TSPL_TANKER_MASTER on TSPL_TANKER_MASTER.Tanker_No =TSPL_MCC_Dispatch_Challan.Tanker_No "
        sQuery += "  left outer join TSPL_Weighment_Detail on TSPL_Weighment_Detail.Weighment_No =TSPL_MILK_TRANSFER_IN.Weighment_No "
        sQuery += "  left outer join TSPL_WEIGHMENT_CHEMBER_DETAILS on TSPL_WEIGHMENT_CHEMBER_DETAILS.Weighment_No=TSPL_Weighment_Detail.Weighment_No and TSPL_WEIGHMENT_CHEMBER_DETAILS.line_no= TSPL_MCC_DISPATCH_CHALLAN_DETAIL.chamber_no "
        sQuery += " left Outer Join (Select TSPL_Mcc_Dispatch_Chalan_Parameter_Detail. *    From TSPL_MCC_Dispatch_Challan       Left Outer Join TSPL_Mcc_Dispatch_Chalan_Parameter_Detail        On TSPL_Mcc_Dispatch_Chalan_Parameter_Detail.Chalan_No   =        TSPL_MCC_Dispatch_Challan.Chalan_NO   And        TSPL_Mcc_Dispatch_Chalan_Parameter_Detail.Param_Type = 'FAT') t_FAT_Desp      On t_FAT_Desp.Chalan_No = TSPL_MCC_Dispatch_Challan.Chalan_NO and TSPL_MCC_DISPATCH_CHALLAN_DETAIL.chamber_no=t_FAT_Desp.sno "

        sQuery += " Left Outer Join (Select TSPL_Mcc_Dispatch_Chalan_Parameter_Detail. * From TSPL_MCC_Dispatch_Challan Left Outer Join TSPL_Mcc_Dispatch_Chalan_Parameter_Detail On TSPL_Mcc_Dispatch_Chalan_Parameter_Detail.Chalan_No   =        TSPL_MCC_Dispatch_Challan.Chalan_NO   And        TSPL_Mcc_Dispatch_Chalan_Parameter_Detail.Param_Type = 'SNF') t_SNF_Desp     On t_SNF_Desp .Chalan_No = TSPL_MCC_Dispatch_Challan.Chalan_NO and TSPL_MCC_DISPATCH_CHALLAN_DETAIL.chamber_no=t_SNF_Desp.sno "
        sQuery += " Left Outer Join (Select TSPL_Mcc_Dispatch_Chalan_Parameter_Detail. * From TSPL_MCC_Dispatch_Challan Left Outer Join TSPL_Mcc_Dispatch_Chalan_Parameter_Detail On TSPL_Mcc_Dispatch_Chalan_Parameter_Detail.Chalan_No   =        TSPL_MCC_Dispatch_Challan.Chalan_NO   And        TSPL_Mcc_Dispatch_Chalan_Parameter_Detail.Param_Type = 'CLR') t_CLR_Desp     On t_CLR_Desp.Chalan_No = TSPL_MCC_Dispatch_Challan.Chalan_NO and TSPL_MCC_DISPATCH_CHALLAN_DETAIL.chamber_no=t_CLR_Desp.sno "
        sQuery += " Left Outer Join (Select TSPL_Mcc_Dispatch_Chalan_Parameter_Detail. * From TSPL_MCC_Dispatch_Challan  Left Outer Join TSPL_Mcc_Dispatch_Chalan_Parameter_Detail On TSPL_Mcc_Dispatch_Chalan_Parameter_Detail.Chalan_No   =        TSPL_MCC_Dispatch_Challan.Chalan_NO   And        TSPL_Mcc_Dispatch_Chalan_Parameter_Detail.Param_Type = 'TEMP') t_TEMP_Desp     On t_TEMP_Desp .Chalan_No = TSPL_MCC_Dispatch_Challan.Chalan_NO and TSPL_MCC_DISPATCH_CHALLAN_DETAIL.chamber_no=t_TEMP_Desp.sno "

        sQuery += " Left Outer Join (Select TSPL_QC_Parameter_Detail.* From TSPL_MILK_TRANSFER_IN Left Outer Join TSPL_QC_Parameter_Detail On TSPL_QC_Parameter_Detail.QC_No  = TSPL_MILK_TRANSFER_IN.QC_No  And TSPL_QC_Parameter_Detail.Param_Type = 'FAT') t_FAT_Recd On t_FAT_Recd .QC_No   = TSPL_MILK_TRANSFER_IN.QC_No and TSPL_MCC_DISPATCH_CHALLAN_DETAIL.chamber_no=t_FAT_Recd.line_no "
        sQuery += " Left Outer Join (Select TSPL_QC_Parameter_Detail.* From TSPL_MILK_TRANSFER_IN Left Outer Join TSPL_QC_Parameter_Detail On TSPL_QC_Parameter_Detail.QC_No  = TSPL_MILK_TRANSFER_IN.QC_No  And TSPL_QC_Parameter_Detail.Param_Type = 'SNF') t_SNF_Recd On t_SNF_Recd.QC_No   = TSPL_MILK_TRANSFER_IN.QC_No and TSPL_MCC_DISPATCH_CHALLAN_DETAIL.chamber_no=t_SNF_Recd.line_no "
        sQuery += " Left Outer Join (Select TSPL_QC_Parameter_Detail.* From TSPL_MILK_TRANSFER_IN Left Outer Join TSPL_QC_Parameter_Detail On TSPL_QC_Parameter_Detail.QC_No  = TSPL_MILK_TRANSFER_IN.QC_No  And TSPL_QC_Parameter_Detail.Param_Type = 'CLR') t_CLR_Recd On t_CLR_Recd .QC_No   = TSPL_MILK_TRANSFER_IN.QC_No and TSPL_MCC_DISPATCH_CHALLAN_DETAIL.chamber_no=t_CLR_Recd.line_no "
        sQuery += " Left Outer Join (Select TSPL_QC_Parameter_Detail.* From TSPL_MILK_TRANSFER_IN Left Outer Join TSPL_QC_Parameter_Detail On TSPL_QC_Parameter_Detail.QC_No  = TSPL_MILK_TRANSFER_IN.QC_No  And TSPL_QC_Parameter_Detail.Param_Type = 'TEMP') t_Temp_Recd  On t_Temp_Recd.QC_No   = TSPL_MILK_TRANSFER_IN.QC_No and TSPL_MCC_DISPATCH_CHALLAN_DETAIL.chamber_no=t_Temp_Recd.line_no "
        sQuery += " where 2 = 2"
        If cbgMCC.CheckedValue.Count > 0 Then
            sQuery += "and TSPL_MCC_MASTER.MCC_Code  IN (" + clsCommon.GetMulcallString(cbgMCC.CheckedValue) + ") "
            If cbgPlant.CheckedValue.Count > 0 Then
                sQuery += " and (TSPL_MCC_Dispatch_Challan.Mcc_Or_Plant_Code in (" + clsCommon.GetMulcallString(cbgPlant.CheckedValue) + ")  " &
                    " or TSPL_MCC_Dispatch_Challan.Mcc_Or_Plant_Code in (" + clsCommon.GetMulcallString(cbgMCC.CheckedValue) + ")) "
            End If
        Else
            sQuery += "and TSPL_MCC_MASTER.MCC_Code  IN (" + StrPermission + ") "
            If cbgPlant.CheckedValue.Count > 0 Then
                sQuery += " and (TSPL_MCC_Dispatch_Challan.Mcc_Or_Plant_Code in (" + clsCommon.GetMulcallString(cbgPlant.CheckedValue) + ")  " &
                " or TSPL_MCC_Dispatch_Challan.Mcc_Or_Plant_Code in (" + StrPermission + ")) "
            End If
        End If
        'If cbgPlant.CheckedValue.Count > 0 Then
        '    sQuery += " and TSPL_LOCATION_MASTER.Location_Code in (" + clsCommon.GetMulcallString(cbgPlant.CheckedValue) + ")  "
        'End If
        If chkTankerSelect.IsChecked And cbgTanker.CheckedValue.Count > 0 Then
            sQuery += " and TSPL_TANKER_MASTER.Tanker_No in (" + clsCommon.GetMulcallString(cbgTanker.CheckedValue) + ")  "
        End If
        If Chktransselect.IsChecked And cbgTrans.CheckedValue.Count > 0 Then
            sQuery += " and TSPL_TANKER_MASTER.Tanker_Transporter_Code  in (" + clsCommon.GetMulcallString(cbgTrans.CheckedValue) + ")"
        End If
        sQuery += " and convert(date,TSPL_MCC_Dispatch_Challan.Dispatch_Date,103)>=convert(date,'" + txtFromDate.Value + "',103) and convert(date,TSPL_MCC_Dispatch_Challan.Dispatch_Date,103) <=convert(date,'" + txtToDate.Value + "' ,103)"
        sQuery += " ) as xx) as YY  order by convert(date,Dispatch_Date ,103)"

        Dim dtgv As New DataTable
        dtgv = clsDBFuncationality.GetDataTable(sQuery)
        If dtgv IsNot Nothing And dtgv.Rows.Count > 0 Then
            gv.DataSource = Nothing
            gv.Rows.Clear()
            gv.Columns.Clear()
            gv.DataSource = dtgv
            gv.GroupDescriptors.Clear()
            gv.MasterTemplate.SummaryRowsBottom.Clear()
            FormatGrid()

            RadPageView1.SelectedPage = RadPageViewPage2
        Else
            clsCommon.MyMessageBoxShow("No Data Found", Me.Text)
        End If
        ReStoreGridLayout()
        view()
    End Sub
    Sub FormatGrid()
        ' Dim strItemCode, head2 As String

        gv.TableElement.TableHeaderHeight = 25
        gv.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gv.Columns.Count - 1
            gv.Columns(ii).ReadOnly = True
            gv.Columns(ii).IsVisible = False
            'If chkcatewise.Checked AndAlso ii > 18 Then
            '    gv.Columns(ii).IsVisible = True
            '    gv.Columns(ii).Width = 100
            'End If
        Next

        gv.Columns("IssuedBy").IsVisible = True
        gv.Columns("IssuedBy").Width = 100
        gv.Columns("IssuedBy").HeaderText = " ISSUED BY"



        gv.Columns("RecdAt").IsVisible = True
        gv.Columns("RecdAt").Width = 100
        gv.Columns("RecdAt").HeaderText = " RECD AT"

        gv.Columns("Dispatch_Date").IsVisible = True
        gv.Columns("Dispatch_Date").Width = 100
        gv.Columns("Dispatch_Date").HeaderText = "DISP DATE"
        gv.Columns("Dispatch_Date").FormatString = "{0:d}"

        gv.Columns("Dispatch_Time").IsVisible = True
        gv.Columns("Dispatch_Time").Width = 100
        gv.Columns("Dispatch_Time").HeaderText = " DISP TIME"
        gv.Columns("Dispatch_Time").FormatString = "{0:hh:mm tt}"

        gv.Columns("Doc_No").IsVisible = True
        gv.Columns("Doc_No").Width = 100
        gv.Columns("Doc_No").HeaderText = "DOC NO"

        gv.Columns("Desp_Chemist_Name").IsVisible = True
        gv.Columns("Desp_Chemist_Name").Width = 80
        gv.Columns("Desp_Chemist_Name").HeaderText = "CHEMIST NAME"

        gv.Columns("Tanker_Capacity").IsVisible = True
        gv.Columns("Tanker_Capacity").Width = 80
        gv.Columns("Tanker_Capacity").HeaderText = "TANKER CAPACITY"

        gv.Columns("Tanker_Transporter_Code").IsVisible = True
        gv.Columns("Tanker_Transporter_Code").Width = 80
        gv.Columns("Tanker_Transporter_Code").HeaderText = "Transporter Code"

        gv.Columns("Description").IsVisible = True
        gv.Columns("Description").Width = 80
        gv.Columns("Description").HeaderText = "Transporter Name"
        'gv.Columns("SAMPLE_NO").IsVisible = True
        'gv.Columns("SAMPLE_NO").Width = 50
        'gv.Columns("SAMPLE_NO").HeaderText = "Sample No"

        gv.Columns("DispSealNo").IsVisible = True
        gv.Columns("DispSealNo").Width = 100
        gv.Columns("DispSealNo").HeaderText = "SEAL NO's"

        gv.Columns("Desp_Qty").IsVisible = True
        gv.Columns("Desp_Qty").Width = 100
        gv.Columns("Desp_Qty").HeaderText = "DISP QTY"

        gv.Columns("Desp_TEMP").IsVisible = True
        gv.Columns("Desp_TEMP").Width = 100
        gv.Columns("Desp_TEMP").HeaderText = "TEMP"

        gv.Columns("Desp_FAT").IsVisible = True
        gv.Columns("Desp_FAT").Width = 100
        gv.Columns("Desp_FAT").HeaderText = "FAT %"

        gv.Columns("Desp_CLR").IsVisible = True
        gv.Columns("Desp_CLR").Width = 100
        gv.Columns("Desp_CLR").HeaderText = "CLR"

        gv.Columns("desp_SNF").IsVisible = True
        gv.Columns("desp_SNF").Width = 100
        gv.Columns("desp_SNF").HeaderText = "SNF"

        gv.Columns("DESPFATKg").IsVisible = True
        gv.Columns("DESPFATKg").Width = 100
        gv.Columns("DESPFATKg").HeaderText = "FAT (kg)"

        gv.Columns("DESPSNFKg").IsVisible = True
        gv.Columns("DESPSNFKg").Width = 100
        gv.Columns("DESPSNFKg").HeaderText = "SNF (Kg)"

        gv.Columns("dis_sample_snf").IsVisible = True
        gv.Columns("dis_sample_snf").Width = 100
        gv.Columns("dis_sample_snf").HeaderText = "SNF"

        gv.Columns("Dis_sample_fat").IsVisible = True
        gv.Columns("Dis_sample_fat").Width = 100
        gv.Columns("Dis_sample_fat").HeaderText = "FAT"

        gv.Columns("GateInTime").IsVisible = True
        gv.Columns("GateInTime").Width = 100
        gv.Columns("GateInTime").HeaderText = "GATE IN TIME"
        gv.Columns("GateInTime").FormatString = "{0:hh:mm tt}"

        gv.Columns("UnloadingTime").IsVisible = True
        gv.Columns("UnloadingTime").Width = 100
        gv.Columns("UnloadingTime").HeaderText = "UNLOADING TIME"
        gv.Columns("UnloadingTime").FormatString = "{0:hh:mm tt}"

        gv.Columns("GateOutTime").IsVisible = True
        gv.Columns("GateOutTime").Width = 100
        gv.Columns("GateOutTime").HeaderText = "GATE OUT TIME"
        gv.Columns("GateOutTime").FormatString = "{0:hh:mm tt}"

        gv.Columns("RECDDATE").IsVisible = True
        gv.Columns("RECDDATE").Width = 100
        gv.Columns("RECDDATE").HeaderText = "RECD DATE"
        gv.Columns("RECDDATE").FormatString = "{0:d}"

        gv.Columns("Gate_Entry_Date").IsVisible = True
        gv.Columns("Gate_Entry_Date").Width = 100
        gv.Columns("Gate_Entry_Date").HeaderText = "Gate Entry Date"
        gv.Columns("Gate_Entry_Date").FormatString = "{0:d}"

        gv.Columns("RECD_Chemist_Name").IsVisible = True
        gv.Columns("RECD_Chemist_Name").Width = 100
        gv.Columns("RECD_Chemist_Name").HeaderText = "CHEMIST NAME"


        gv.Columns("RECDQTY").IsVisible = True
        gv.Columns("RECDQTY").Width = 100
        gv.Columns("RECDQTY").HeaderText = "RECD QTY"


        gv.Columns("TEMP_Recd").IsVisible = True
        gv.Columns("TEMP_Recd").Width = 100
        gv.Columns("TEMP_Recd").HeaderText = "TEMP"

        gv.Columns("FAT_Recd").IsVisible = True
        gv.Columns("FAT_Recd").Width = 100
        gv.Columns("FAT_Recd").HeaderText = "FAT %"


        gv.Columns("CLR_Recd").IsVisible = True
        gv.Columns("CLR_Recd").Width = 100
        gv.Columns("CLR_Recd").HeaderText = "CLR"


        gv.Columns("SNF_Recd").IsVisible = True
        gv.Columns("SNF_Recd").Width = 100
        gv.Columns("SNF_Recd").HeaderText = "SNF %"


        gv.Columns("RECDFATKg").IsVisible = True
        gv.Columns("RECDFATKg").Width = 100
        gv.Columns("RECDFATKg").HeaderText = "FAT (KG)"


        gv.Columns("RECDSNFKg").IsVisible = True
        gv.Columns("RECDSNFKg").Width = 100
        gv.Columns("RECDSNFKg").HeaderText = "SNF (KG)"


        gv.Columns("DiffQty").IsVisible = True
        gv.Columns("DiffQty").Width = 100
        gv.Columns("DiffQty").HeaderText = "QTY"


        gv.Columns("Diff_FAT").IsVisible = True
        gv.Columns("Diff_FAT").Width = 100
        gv.Columns("Diff_FAT").HeaderText = "FAT"


        gv.Columns("Diff_SNF").IsVisible = True
        gv.Columns("Diff_SNF").Width = 100
        gv.Columns("Diff_SNF").HeaderText = "SNF"


        gv.Columns("Recd_Sample_Fat").IsVisible = True
        gv.Columns("Recd_Sample_Fat").Width = 100
        gv.Columns("Recd_Sample_Fat").HeaderText = "FAT"


        gv.Columns("Recd_Sample_snf").IsVisible = True
        gv.Columns("Recd_Sample_snf").Width = 100
        gv.Columns("Recd_Sample_snf").HeaderText = "SNF"


        gv.Columns("KMS").IsVisible = True
        gv.Columns("KMS").Width = 100
        gv.Columns("KMS").HeaderText = "KMS"


        gv.Columns("TimeTakenByTankerInTransit").IsVisible = True
        gv.Columns("TimeTakenByTankerInTransit").Width = 100
        gv.Columns("TimeTakenByTankerInTransit").HeaderText = "TIME TAKEN BY TANKER IN TRANSIT"
        gv.Columns("TimeTakenByTankerInTransit").FormatString = "{0:d}"

        gv.Columns("TimeTakenForUnloadingPlant").IsVisible = True
        gv.Columns("TimeTakenForUnloadingPlant").Width = 100
        gv.Columns("TimeTakenForUnloadingPlant").HeaderText = "TIME TAKEN FOR UNLOADING AT PLANT"
        gv.Columns("TimeTakenForUnloadingPlant").FormatString = "{0:d}"
        

        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim intCount As Integer = 0

        Dim item1 As New GridViewSummaryItem("Desp_Qty", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)
        Dim item2 As New GridViewSummaryItem("RECDQTY", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item2)
        Dim item3 As New GridViewSummaryItem("DiffQty", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item3)
        
       
        gv.ShowGroupPanel = False
        gv.MasterTemplate.AutoExpandGroups = True

        gv.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        gv.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
    End Sub
    Sub view()
        If gv.Rows.Count > 0 Then
            Dim view As New ColumnGroupsViewDefinition()
            view.ColumnGroups.Add(New GridViewColumnGroup("MCC DISPATCH DETAILS"))

            view.ColumnGroups(0).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv.Columns("IssuedBy").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv.Columns("RecdAt").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv.Columns("Dispatch_Date").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv.Columns("Dispatch_Time").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv.Columns("Doc_No").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv.Columns("Desp_Chemist_Name").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv.Columns("Tanker_No").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv.Columns("Tanker_Capacity").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv.Columns("Tanker_Transporter_Code").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv.Columns("Description").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv.Columns("DispSealNo").Name)

            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv.Columns("Desp_Qty").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv.Columns("Desp_TEMP").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv.Columns("Desp_FAT").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv.Columns("Desp_CLR").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv.Columns("desp_SNF").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv.Columns("DESPFATKg").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv.Columns("DESPSNFKg").Name)

            view.ColumnGroups.Add(New GridViewColumnGroup("DISP CONTROL SAMPLE DETAILS"))

            view.ColumnGroups(1).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv.Columns("dis_sample_snf").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv.Columns("Dis_sample_fat").Name)

            view.ColumnGroups.Add(New GridViewColumnGroup("PLANT RECEIVING DETAILS"))
            view.ColumnGroups(2).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv.Columns("GateInTime").Name)
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv.Columns("UnloadingTime").Name)
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv.Columns("GateOutTime").Name)
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv.Columns("Gate_Entry_Date").Name)
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv.Columns("RECDDATE").Name)
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv.Columns("RECD_Chemist_Name").Name)
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv.Columns("RECDQTY").Name)
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv.Columns("TEMP_Recd").Name)
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv.Columns("FAT_Recd").Name)
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv.Columns("CLR_Recd").Name)
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv.Columns("SNF_Recd").Name)
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv.Columns("RECDFATKg").Name)
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv.Columns("RECDSNFKg").Name)

            view.ColumnGroups.Add(New GridViewColumnGroup("VARIATION"))

            view.ColumnGroups(3).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(3).Rows(0).ColumnNames.Add(gv.Columns("DiffQty").Name)
            view.ColumnGroups(3).Rows(0).ColumnNames.Add(gv.Columns("Diff_FAT").Name)
            view.ColumnGroups(3).Rows(0).ColumnNames.Add(gv.Columns("Diff_SNF").Name)

            view.ColumnGroups.Add(New GridViewColumnGroup("PLANT CONTROL SAMPLE DETAILS"))

            view.ColumnGroups(4).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(4).Rows(0).ColumnNames.Add(gv.Columns("Recd_Sample_Fat").Name)
            view.ColumnGroups(4).Rows(0).ColumnNames.Add(gv.Columns("Recd_Sample_snf").Name)

            view.ColumnGroups.Add(New GridViewColumnGroup(""))

            view.ColumnGroups(5).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(5).Rows(0).ColumnNames.Add(gv.Columns("KMS").Name)
            view.ColumnGroups(5).Rows(0).ColumnNames.Add(gv.Columns("TimeTakenByTankerInTransit").Name)
            view.ColumnGroups(5).Rows(0).ColumnNames.Add(gv.Columns("TimeTakenForUnloadingPlant").Name)
            gv.ViewDefinition = view
        End If
    End Sub
    Sub print(ByVal exporter As EnumExportTo)
        Try
            If gv.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                Dim CompName As String = clsDBFuncationality.getSingleValue("Select Comp_Name from TSPL_COMPANY_MASTER Where Comp_Code='" + objCommonVar.CurrentCompanyCode + "'")
                arrHeader.Add(CompName)
                arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & "'"))
                arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")
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
                If chkPlantSelect.IsChecked Then
                    Dim strPlantName As String = ""
                    For Each StrName As String In cbgPlant.CheckedDisplayMember
                        If clsCommon.myLen(strPlantName) > 0 Then
                            strPlantName += ", "
                        End If
                        strPlantName += StrName
                    Next
                    Dim strPlantCode As String = ""
                    For Each StrCode As String In cbgPlant.CheckedValue
                        If clsCommon.myLen(strPlantCode) > 0 Then
                            strPlantCode += ", "
                        End If
                        strPlantCode += StrCode
                    Next

                    arrHeader.Add((" Plant Code : " + strPlantCode + "  Plant Name: " + strPlantName + " "))

                End If

                If chkTankerSelect.IsChecked Then
                    Dim strTankerName As String = ""
                    For Each StrName As String In cbgTanker.CheckedDisplayMember
                        If clsCommon.myLen(strTankerName) > 0 Then
                            strTankerName += ", "
                        End If
                        strTankerName += StrName
                    Next
                    Dim strTankerCode As String = ""
                    For Each StrCode As String In cbgTanker.CheckedValue
                        If clsCommon.myLen(strTankerCode) > 0 Then
                            strTankerCode += ", "
                        End If
                        strTankerCode += StrCode
                    Next

                    arrHeader.Add((" Tanker Code : " + strTankerCode + "  Tanker Name: " + strTankerName + " "))

                End If
                If Chktransselect.IsChecked Then
                    Dim strTransName As String = ""
                    For Each StrName As String In cbgTrans.CheckedDisplayMember
                        If clsCommon.myLen(strTransName) > 0 Then
                            strTransName += ", "
                        End If
                        strTransName += StrName
                    Next
                    Dim strTransCode As String = ""
                    For Each StrCode As String In cbgTrans.CheckedValue
                        If clsCommon.myLen(strTransCode) > 0 Then
                            strTransCode += ", "
                        End If
                        strTransCode += StrCode
                    Next

                    arrHeader.Add((" Transporter Code : " + strTransCode + "  Transporter Name: " + strTransName + " "))

                End If
                If exporter = EnumExportTo.Excel Then
                    'Dim sfd As SaveFileDialog = New SaveFileDialog()
                    'Dim filePath As String
                    'sfd.FileName = Me.Text
                    'sfd.Filter = "Excel 97-2003 (*.xls) |*.xls;|Excel 2007 (*.xlsx)|*.xlsx;|CSV Files (*.csv) |*.csv"
                    'If sfd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                    '    filePath = sfd.FileName
                    'Else
                    '    Exit Sub
                    'End If
                    transportSql.applyExportTemplate(gv, PageSetupReport_ID)
                    transportSql.QuickExportToExcel(gv, "", Me.Text, , arrHeader)
                    'transportSql.exportdataChilRows(gv, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
                    'common.clsCommon.MyMessageBoxShow("Exported Successfully.")
                    'Process.Start(filePath)
                Else
                    transportSql.applyExportTemplate(gv, PageSetupReport_ID)
                    clsCommon.MyExportToPDF("Tanker Variation Report", gv, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
                End If
            Else
                common.clsCommon.MyMessageBoxShow("No Data Found to Export.", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
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

    Private Sub RadMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem2.Click
        print(EnumExportTo.Excel)
    End Sub

    Private Sub chkMCCAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkMCCAll.ToggleStateChanged
        cbgMCC.Enabled = Not chkMCCAll.IsChecked
        If chkMCCAll.IsChecked Then
            cbgMCC.CheckedAll()
        Else
            cbgMCC.UnCheckedAll()
        End If
    End Sub

    Private Sub ChkPlantAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles ChkPlantAll.ToggleStateChanged
        cbgPlant.Enabled = Not ChkPlantAll.IsChecked
        If ChkPlantAll.IsChecked Then
            cbgPlant.CheckedAll()
        Else
            cbgPlant.UnCheckedAll()
        End If
    End Sub

    Private Sub chkTankerAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkTankerAll.ToggleStateChanged
        cbgTanker.Enabled = Not chkTankerAll.IsChecked
    End Sub

    Private Sub ChkTransAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles ChkTransAll.ToggleStateChanged
        cbgTrans.Enabled = Not ChkTransAll.IsChecked
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
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
            End If
            ''stuti regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub rmdeleteLayout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmdeleteLayout.Click
        clsGridLayout.DeleteData(MyBase.Form_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow("Layout Delete successfully", "Information")
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
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub

    Private Sub RptTankerVariationReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()

        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnGo, "Press Alt+N Refresh ")

        ButtonToolTip.SetToolTip(BtnReset, "Press Alt+R Adding New")
        txtMilkReceiptFromDate.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE, "dd/MM/yyyy")
        txtMilkReciptToDate.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE, "dd/MM/yyyy")
        Reset()
    End Sub

    Private Sub RptTankerVariationReport_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N Then
            Load_Report()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        ElseIf e.Alt And e.KeyCode = Keys.R Then
            Reset()
        End If
    End Sub

    Private Sub RadMenuItem3_Click(sender As Object, e As EventArgs) Handles RadMenuItem3.Click
        print(EnumExportTo.PDF)
    End Sub

    Private Sub fndLoc__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndLoc._MYValidating
        Try
            Dim whrCls As String = "TSPL_LOCATION_MASTER.Type = 'PLANT'"

            fndLoc.Value = clsLocation.getFinder(whrCls, fndLoc.Value, isButtonClicked)
            txtLocName.Text = clsLocation.GetName(fndLoc.Value, Nothing)
        Catch ex As Exception

        End Try

    End Sub

    Private Sub btnDotMatrixPrint_Click(sender As Object, e As EventArgs) Handles btnDotMatrixPrint.Click

        If clsCommon.myLen(fndLoc.Value) <= 0 Then
            clsCommon.MyMessageBoxShow("Please select Plant First.", Me.Text)
            Return
        End If
        Dim strCorrectionFactor As String = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select  Description from TSPL_FIXED_PARAMETER where code = 'MCCdefaultCorrectionFactorBS'  and Type = 'MCCdefaultCorrectionFactorBS'"))
        Dim strBMItem As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select  Description from TSPL_FIXED_PARAMETER where type = 'MCC Default Milk Item Buffalo' and code = 'MilkSetting'"))
        Dim strCMItem As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select  Description from TSPL_FIXED_PARAMETER where type = 'MCC Default Milk Item Cow' and code = 'MilkSetting'"))
        Dim strMMItem As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select  Description from TSPL_FIXED_PARAMETER where type = 'MCCDefaultMilkItem' and code = 'MilkSetting'"))

        Dim qry As String = " Select * from (
                              select 1 as SNo, tspl_mcc_dispatch_challan.MCC_Code ,convert (varchar ,tspl_mcc_dispatch_challan.Dispatch_Date,103) as [Date], tspl_mcc_dispatch_challan.Tanker_No , tspl_mcc_dispatch_challan.Tanker_Dispatch_To , tspl_mcc_dispatch_challan.Mcc_Or_Plant_Code, case when   TSPL_INVENTORY_MOVEMENT_NEW.Item_Code = '" + strBMItem + "' then 'BM'  when  TSPL_INVENTORY_MOVEMENT_NEW.Item_Code = '" + strCMItem + "' then 'CM' when  TSPL_INVENTORY_MOVEMENT_NEW.Item_Code = '" + strMMItem + "' then 'MM' else '' end as [TYPE]  , TSPL_INVENTORY_MOVEMENT_NEW.Qty as [Qty(KG)],convert (decimal(18,2) , (TSPL_INVENTORY_MOVEMENT_NEW.Qty * Stocking_Conversion_Factor.Conversion_Factor ) / nullif (Target_Conversion_Factor.Conversion_Factor,0) )  as [Qty(LTR)] , TSPL_INVENTORY_MOVEMENT_NEW.Fat_Per , TSPL_INVENTORY_MOVEMENT_NEW.SNF_Per , Cast (((TSPL_INVENTORY_MOVEMENT_NEW.SNF_Per -" + strCorrectionFactor + "-(0.2 * TSPL_INVENTORY_MOVEMENT_NEW.Fat_Per))*4 ) as Decimal (18,2)) as [CLR], cast ( TSPL_INVENTORY_MOVEMENT_NEW.Fat_KG as decimal(18,2)) as Fat_KG, cast ( TSPL_INVENTORY_MOVEMENT_NEW.SNF_KG as decimal (18,2) ) as SNF_KG  from tspl_mcc_dispatch_challan 
                              left outer join TSPL_MILK_TRANSFER_IN on TSPL_MILK_TRANSFER_IN.Dispatch_Challan_No = tspl_mcc_dispatch_challan.Chalan_NO
                              inner join TSPL_INVENTORY_MOVEMENT_NEW on TSPL_INVENTORY_MOVEMENT_NEW.Source_Doc_No = TSPL_MILK_TRANSFER_IN.Receipt_Challan_No
                              left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code = tspl_mcc_dispatch_challan.MCC_Code
                              left outer join (Select TSPL_ITEM_UOM_DETAIL.ITem_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where TSPL_ITEM_UOM_DETAIL.UOM_Code = 'Ltr' ) as Target_Conversion_Factor on Target_Conversion_Factor.Item_Code = TSPL_INVENTORY_MOVEMENT_NEW.Item_Code
                              left outer join TSPL_ITEM_UOM_DETAIL as Stocking_Conversion_Factor on TSPL_INVENTORY_MOVEMENT_NEW.Item_Code = Stocking_Conversion_Factor.Item_Code and TSPL_INVENTORY_MOVEMENT_NEW.UOM = Stocking_Conversion_Factor.UOM_Code
                              where tspl_mcc_dispatch_challan.isPosted =1
                              and convert (date ,tspl_mcc_dispatch_challan.Dispatch_Date,103) >=  convert (date ,'" + txtMilkReceiptFromDate.Value + "' ,103)  and convert (date ,tspl_mcc_dispatch_challan.Dispatch_Date,103) <=  convert (date ,'" + txtMilkReciptToDate.Value + "' ,103)  and TSPL_MCC_MASTER.Plant_Code = '" + fndLoc.Value + "' "
        If txtMCC.arrValueMember IsNot Nothing AndAlso txtMCC.arrValueMember.Count > 0 Then
            qry += " and tspl_mcc_dispatch_challan.MCC_Code in ( " + clsCommon.GetMulcallString(txtMCC.arrValueMember) + " )"
        End If
        qry += " Union all "
        qry += " select max ( XXXFinal.SNo ) as  SNo , '' as MCC_Code , XXXFinal.TYPE + ' Total' as  [Date] , '' as Tanker_No , '' as Tanker_Dispatch_To , '' Mcc_Or_Plant_Code  , '' as [TYPE], sum ([Qty(KG)]) as [Qty(KG)], sum ([Qty(LTR)]) as [Qty(LTR)],  cast ( ( sum (Fat_KG) * 100 / nullif (sum ([Qty(KG)]),0)) as decimal(18,2))  as Fat_Per, cast ( ( sum (SNF_KG) * 100 / nullif (sum ([Qty(KG)]),0)) as decimal(18,2)) as SNF_Per , Null as  [CLR] , sum (Fat_KG) as Fat_KG ,  sum (SNF_KG) as SNF_KG   from  ( "
        qry += " select 2 as SNo, tspl_mcc_dispatch_challan.MCC_Code ,convert (varchar ,tspl_mcc_dispatch_challan.Dispatch_Date,103) as [Date], tspl_mcc_dispatch_challan.Tanker_No , tspl_mcc_dispatch_challan.Tanker_Dispatch_To , tspl_mcc_dispatch_challan.Mcc_Or_Plant_Code, case when   TSPL_INVENTORY_MOVEMENT_NEW.Item_Code = '" + strBMItem + "' then 'BM'  when  TSPL_INVENTORY_MOVEMENT_NEW.Item_Code = '" + strCMItem + "' then 'CM' when  TSPL_INVENTORY_MOVEMENT_NEW.Item_Code = '" + strMMItem + "' then 'MM' else '' end as [TYPE]  , TSPL_INVENTORY_MOVEMENT_NEW.Qty as [Qty(KG)],convert (decimal(18,2) , (TSPL_INVENTORY_MOVEMENT_NEW.Qty * Stocking_Conversion_Factor.Conversion_Factor ) / nullif (Target_Conversion_Factor.Conversion_Factor,0) )  as [Qty(LTR)] , TSPL_INVENTORY_MOVEMENT_NEW.Fat_Per , TSPL_INVENTORY_MOVEMENT_NEW.SNF_Per , Cast (((TSPL_INVENTORY_MOVEMENT_NEW.SNF_Per -" + strCorrectionFactor + "-(0.2 * TSPL_INVENTORY_MOVEMENT_NEW.Fat_Per))*4 ) as Decimal (18,2)) as [CLR], cast ( TSPL_INVENTORY_MOVEMENT_NEW.Fat_KG as decimal(18,2)) as Fat_KG, cast ( TSPL_INVENTORY_MOVEMENT_NEW.SNF_KG as decimal (18,2) ) as SNF_KG  from tspl_mcc_dispatch_challan 
                              left outer join TSPL_MILK_TRANSFER_IN on TSPL_MILK_TRANSFER_IN.Dispatch_Challan_No = tspl_mcc_dispatch_challan.Chalan_NO
                              inner join TSPL_INVENTORY_MOVEMENT_NEW on TSPL_INVENTORY_MOVEMENT_NEW.Source_Doc_No = TSPL_MILK_TRANSFER_IN.Receipt_Challan_No
                              left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code = tspl_mcc_dispatch_challan.MCC_Code
                              left outer join (Select TSPL_ITEM_UOM_DETAIL.ITem_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where TSPL_ITEM_UOM_DETAIL.UOM_Code = 'Ltr' ) as Target_Conversion_Factor on Target_Conversion_Factor.Item_Code = TSPL_INVENTORY_MOVEMENT_NEW.Item_Code
                              left outer join TSPL_ITEM_UOM_DETAIL as Stocking_Conversion_Factor on TSPL_INVENTORY_MOVEMENT_NEW.Item_Code = Stocking_Conversion_Factor.Item_Code and TSPL_INVENTORY_MOVEMENT_NEW.UOM = Stocking_Conversion_Factor.UOM_Code
                              where tspl_mcc_dispatch_challan.isPosted =1
                              and convert (date ,tspl_mcc_dispatch_challan.Dispatch_Date,103) >=  convert (date ,'" + txtMilkReceiptFromDate.Value + "' ,103)  and convert (date ,tspl_mcc_dispatch_challan.Dispatch_Date,103) <=  convert (date ,'" + txtMilkReciptToDate.Value + "' ,103)  and TSPL_MCC_MASTER.Plant_Code = '" + fndLoc.Value + "' "
        If txtMCC.arrValueMember IsNot Nothing AndAlso txtMCC.arrValueMember.Count > 0 Then
            qry += " and tspl_mcc_dispatch_challan.MCC_Code in ( " + clsCommon.GetMulcallString(txtMCC.arrValueMember) + " )"
        End If
        qry += " )  XXXFinal group by XXXFinal.TYPE "

        qry += " Union all "
        qry += " select  max ( XXXFinal.SNo ) as  SNo , '' as MCC_Code , 'Grand Total' as  [Date] , '' as Tanker_No , '' as Tanker_Dispatch_To , '' Mcc_Or_Plant_Code  , '' as [TYPE], sum ([Qty(KG)]) as [Qty(KG)], sum ([Qty(LTR)]) as [Qty(LTR)],  cast ( ( sum (Fat_KG) * 100 / nullif (sum ([Qty(KG)]),0)) as decimal(18,2))  as Fat_Per, cast ( ( sum (SNF_KG) * 100 / nullif (sum ([Qty(KG)]),0)) as decimal(18,2)) as SNF_Per , Null as  [CLR] , sum (Fat_KG) as Fat_KG ,  sum (SNF_KG) as SNF_KG    from  ( "
        qry += " select 2 as SNo, tspl_mcc_dispatch_challan.MCC_Code ,convert (varchar ,tspl_mcc_dispatch_challan.Dispatch_Date,103) as [Date], tspl_mcc_dispatch_challan.Tanker_No , tspl_mcc_dispatch_challan.Tanker_Dispatch_To , tspl_mcc_dispatch_challan.Mcc_Or_Plant_Code, case when   TSPL_INVENTORY_MOVEMENT_NEW.Item_Code = '" + strBMItem + "' then 'BM'  when  TSPL_INVENTORY_MOVEMENT_NEW.Item_Code = '" + strCMItem + "' then 'CM' when  TSPL_INVENTORY_MOVEMENT_NEW.Item_Code = '" + strMMItem + "' then 'MM' else '' end as [TYPE]  , TSPL_INVENTORY_MOVEMENT_NEW.Qty as [Qty(KG)],convert (decimal(18,2) , (TSPL_INVENTORY_MOVEMENT_NEW.Qty * Stocking_Conversion_Factor.Conversion_Factor ) / nullif (Target_Conversion_Factor.Conversion_Factor,0) )  as [Qty(LTR)] , TSPL_INVENTORY_MOVEMENT_NEW.Fat_Per , TSPL_INVENTORY_MOVEMENT_NEW.SNF_Per , Cast (((TSPL_INVENTORY_MOVEMENT_NEW.SNF_Per -" + strCorrectionFactor + "-(0.2 * TSPL_INVENTORY_MOVEMENT_NEW.Fat_Per))*4 ) as Decimal (18,2)) as [CLR], cast ( TSPL_INVENTORY_MOVEMENT_NEW.Fat_KG as decimal(18,2)) as Fat_KG, cast ( TSPL_INVENTORY_MOVEMENT_NEW.SNF_KG as decimal (18,2) ) as SNF_KG  from  tspl_mcc_dispatch_challan 
                              left outer join TSPL_MILK_TRANSFER_IN on TSPL_MILK_TRANSFER_IN.Dispatch_Challan_No = tspl_mcc_dispatch_challan.Chalan_NO
                              inner join TSPL_INVENTORY_MOVEMENT_NEW on TSPL_INVENTORY_MOVEMENT_NEW.Source_Doc_No = TSPL_MILK_TRANSFER_IN.Receipt_Challan_No
                              left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code = tspl_mcc_dispatch_challan.MCC_Code
                              left outer join (Select TSPL_ITEM_UOM_DETAIL.ITem_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where TSPL_ITEM_UOM_DETAIL.UOM_Code = 'Ltr' ) as Target_Conversion_Factor on Target_Conversion_Factor.Item_Code = TSPL_INVENTORY_MOVEMENT_NEW.Item_Code
                              left outer join TSPL_ITEM_UOM_DETAIL as Stocking_Conversion_Factor on TSPL_INVENTORY_MOVEMENT_NEW.Item_Code = Stocking_Conversion_Factor.Item_Code and TSPL_INVENTORY_MOVEMENT_NEW.UOM = Stocking_Conversion_Factor.UOM_Code
                              where tspl_mcc_dispatch_challan.isPosted =1
                              and convert (date ,tspl_mcc_dispatch_challan.Dispatch_Date,103) >=  convert (date ,'" + txtMilkReceiptFromDate.Value + "' ,103)  and convert (date ,tspl_mcc_dispatch_challan.Dispatch_Date,103) <=  convert (date ,'" + txtMilkReciptToDate.Value + "' ,103)  and TSPL_MCC_MASTER.Plant_Code = '" + fndLoc.Value + "' "
        If txtMCC.arrValueMember IsNot Nothing AndAlso txtMCC.arrValueMember.Count > 0 Then
            qry += " and tspl_mcc_dispatch_challan.MCC_Code in ( " + clsCommon.GetMulcallString(txtMCC.arrValueMember) + " )"
        End If
        qry += " )  XXXFinal group by XXXFinal.SNo  )Final order by Final.SNo asc "



        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

        If dt IsNot Nothing And dt.Rows.Count > 0 Then
            Dim obj As clsDosPrint = New clsDosPrint()
            obj.ReportName = ""
            obj.ReportName1 = "M.P.F., HYDERABAD MILK RECEIPTS"
            obj.ShowPageNo = True
            obj.PageSetupCustomizeCharColumn = 120
            obj.arrFilter = New List(Of clsDosPrintHeaderFilter)()
            obj.arrFilter.Add(clsDosPrintHeaderFilter.GetObject("UNIT NAME", clsCommon.myCstr(txtLocName.Text)))
            obj.arrFilter.Add(clsDosPrintHeaderFilter.GetObject("PERIOD FROM", clsCommon.myCstr(txtMilkReceiptFromDate.Text) + " To " + clsCommon.myCstr(txtMilkReciptToDate.Text)))



            obj.arrColumn = New List(Of clsDosPrintColumn)()
            obj.arrColumn.Add(clsDosPrintColumn.SetColumn("DATE", "DATE", False, DosPrintAlignment.Left, 15, False, DecimalPlaces.NA))
            obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Tanker_No", "TANKER", False, DosPrintAlignment.Left, 10, False, DecimalPlaces.NA))
            obj.arrColumn.Add(clsDosPrintColumn.SetColumn("TYPE", "TYPE", False, DosPrintAlignment.Right, 10, False, DecimalPlaces.NA))
            obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Qty(LTR)", "QTY-LTS", False, DosPrintAlignment.Right, 10, False, DecimalPlaces.Two))
            obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Fat_Per", "FAT%", False, DosPrintAlignment.Right, 10, False, DecimalPlaces.Two))
            obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Snf_Per", "SNF%", False, DosPrintAlignment.Right, 10, False, DecimalPlaces.Two))
            obj.arrColumn.Add(clsDosPrintColumn.SetColumn("CLR", "CLR", False, DosPrintAlignment.Right, 10, False, DecimalPlaces.Two))
            obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Qty(KG)", "KGS", False, DosPrintAlignment.Right, 10, False, DecimalPlaces.Two))
            obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Fat_KG", "KG-FAT", False, DosPrintAlignment.Right, 10, False, DecimalPlaces.Two))
            obj.arrColumn.Add(clsDosPrintColumn.SetColumn("SNF_KG", "KG-SNF", False, DosPrintAlignment.Right, 10, False, DecimalPlaces.Two))
            obj.Print(obj, dt, PageSetup.Landscap)
        Else
            clsCommon.MyMessageBoxShow("No Data Found", Me.Text)

        End If
    End Sub

    Private Sub txtMCC__My_Click(sender As Object, e As EventArgs) Handles txtMCC._My_Click
        If clsCommon.myLen(fndLoc.Value) <= 0 Then
            clsCommon.MyMessageBoxShow("Please select Plant First.", Me.Text)
            txtMCC.arrValueMember = Nothing
            txtMCC.arrDispalyMember = Nothing
            Return
        End If
        Dim qry As String = "select MCC_Code,MCC_NAME,TSPL_MCC_MASTER.plant_code as [Plant Code],tspl_location_master.location_desc as [Plant Name] from TSPL_MCC_MASTER left join tspl_location_master on tspl_location_master.location_code=TSPL_MCC_MASTER.plant_code where plant_Code = '" + clsCommon.myCstr(fndLoc.Value) + "' "
        txtMCC.arrValueMember = clsCommon.ShowMultipleSelectForm("PCUMCC", qry, "MCC_Code", "MCC_NAME", txtMCC.arrValueMember, txtMCC.arrDispalyMember)
    End Sub

    Private Sub btnMilkReciptReset_Click(sender As Object, e As EventArgs) Handles btnMilkReciptReset.Click
        fndLoc.Value = ""
        txtLocName.Text = ""
        txtMCC.arrValueMember = Nothing
        txtMCC.arrDispalyMember = Nothing
    End Sub
End Class
