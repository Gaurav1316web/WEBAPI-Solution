'' work done against ticket no. TEC/08/02/19-000419 , MIL/18/02/19-000042
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common
Imports System.IO
Public Class FrmMCCShiftReportRouteWise
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim btnReferesh As Boolean = False
    Dim tmpValLoad As Boolean = True
    'preeti gupta ticket no.[BM00000004218]
    Dim arrLoc As String = Nothing
    Dim ItemStructureMandatoryOnWeightConversion As Boolean = False
    Private Sub LOCATIONRIGTHS()
        Try
            Dim obj As New clsMCCCodes()
            obj = clsMCCCodes.GetData()
            If obj.arrLocCodes IsNot Nothing AndAlso clsCommon.myLen(obj.arrLocCodes) > 0 Then
                arrLoc = obj.arrLocCodes
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.rptMCCShiftReportRouteWise)
        If Not (MyBase.isReadFlag) Then
            If MDI.blnShowAllMenu = False Then
                Throw New Exception("Permission Denied")
            Else
                Throw New Exception("Can't Access in demo version. " + Environment.NewLine + " For any queries/details, contact tecxpert@tecxpert.in. ")
            End If
        End If
        RadSplitButton1.Visible = MyBase.isExport
        btnPrint.Visible = MyBase.isPrintFlag
    End Sub
    Private Sub chkVSPAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkVSPAll.ToggleStateChanged
        cbgVSP.Enabled = Not chkVSPAll.IsChecked
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
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully",  Me.Text)
            End If
            ''stuti regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub
    Private Sub rmDeleteLayout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(MyBase.Form_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", Me.Text)
    End Sub
    Private Sub FrmMCCShiftReportRouteWise_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ItemStructureMandatoryOnWeightConversion = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.ItemStructureMandatoryOnWeightConversion, clsFixedParameterCode.ItemStructureMandatoryOnWeightConversion, Nothing)) = 1, True, False))
        LOCATIONRIGTHS()
        SetUserMgmtNew()
        cboUnit.Text = "Kg"
        chkVSPAll.IsChecked = True
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnGo, "Press Alt+R Refresh ")
        ButtonToolTip.SetToolTip(btnPrint, "Press Alt+P For Print")
        ButtonToolTip.SetToolTip(BtnReset, "Pres%s Alt+N Adding New")
        RadPageView1.SelectedPage = RadPageViewPage1
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = clsCommon.GETSERVERDATE()
        rdbRate.IsChecked = True
        LoadMCCRouteVLCTree()
        LoadVSP()
        LoadShiftFrom()
        LoadShiftTo()
        Reset()
    End Sub
    Sub LoadMCCRouteVLCTree()
        Dim qry As String = Nothing
        Dim dt As DataTable = Nothing
        If clsCommon.myLen(arrLoc) > 0 Then
            qry = "select TSPL_VLC_MASTER_HEAD.VLC_Code as Code,TSPL_VLC_MASTER_HEAD.VLC_Name as Name,TSPL_VLC_MASTER_HEAD.Route_Code as ParentCode,3 as Lvl from TSPL_VLC_MASTER_HEAD where len(isnull(TSPL_VLC_MASTER_HEAD.Route_Code,''))>0 union all   select TSPL_MCC_ROUTE_MASTER.Route_Code as Code,TSPL_MCC_ROUTE_MASTER.Route_Name as Name,TSPL_MCC_ROUTE_MASTER.MCC_Code as ParentCode,2 as Lvl from TSPL_MCC_ROUTE_MASTER where len(isnull(TSPL_MCC_ROUTE_MASTER.MCC_Code,''))>0  union all   select TSPL_MCC_MASTER.MCC_Code as Code,TSPL_MCC_MASTER.MCC_NAME as Name,null as ParentCode,1 as Lvl from TSPL_MCC_MASTER   where TSPL_MCC_MASTER.MCC_Code in (" + arrLoc + ") "
            dt = clsDBFuncationality.GetDataTable(qry)
        End If
        If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
            btnGo.Enabled = False
        Else
            cbtMCCRouteVLCC.DataSource = dt
            cbtMCCRouteVLCC.ValueMember = "Code"
            cbtMCCRouteVLCC.DisplayMember = "Name"
            cbtMCCRouteVLCC.ParentValue = "ParentCode"
        End If
    End Sub
    Sub LoadShiftFrom()
        Dim dt As DataTable = New DataTable
        dt.Columns.Add("Code")
        dt.Columns.Add("Shift")

        Dim dr As DataRow = dt.NewRow
        dr("Code") = "M"
        dr("Shift") = "Morning"
        dt.Rows.Add(dr)

        dr = dt.NewRow
        dr("Code") = "E"
        dr("Shift") = "Evening"
        dt.Rows.Add(dr)

        txtFromShift.DataSource = dt
        txtFromShift.ValueMember = "Code"
    End Sub
    Sub LoadShiftTo()
        Dim dt As DataTable = New DataTable
        dt.Columns.Add("Code")
        dt.Columns.Add("Shift")

        Dim dr As DataRow = dt.NewRow
        dr("Code") = "M"
        dr("Shift") = "Morning"
        dt.Rows.Add(dr)

        dr = dt.NewRow
        dr("Code") = "E"
        dr("Shift") = "Evening"
        dt.Rows.Add(dr)

        txtToShift.DataSource = dt
        txtToShift.ValueMember = "Code"
    End Sub
    Sub LoadVSP()
        Dim qry As String = "select Vendor_Code as [Code] ,Vendor_Name as [Name]  from TSPL_VENDOR_MASTER  where Form_Type ='VSP' "
        cbgVSP.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgVSP.ValueMember = "Code"
        cbgVSP.DisplayMember = "Name"

    End Sub
    Public Sub Load_Report()
        Try
            Dim sQuery As String

            If txtFromDate.Value > txtToDate.Value Then
                txtFromDate.Focus()
                Throw New Exception("From date can not be greater then to Date")
            End If

            If chkVSPSelect.IsChecked AndAlso cbgVSP.CheckedValue.Count = 0 Then
                Throw New Exception("Please select atleast single VSP or select all.")
            End If

            If cbtMCCRouteVLCC.CheckedValue.Count = 0 Then
                Throw New Exception("Please select atleast single MCC or select all.")
            End If
            ' Ticket No : BHA/21/11/18-000686 By Prabhakar - for Devided by Zero error
            sQuery = " (select '" & objCommonVar.CurrentUser & "' as User_Name , '" & clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") & "'  as fromDate ,'" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy") & "'  as Todate , (TSPL_COMPANY_MASTER.add1 +case when len(TSPL_COMPANY_MASTER.add2)>0 then ', '+TSPL_COMPANY_MASTER.add2 else '' end +case when LEN(isnull(TSPL_COMPANY_MASTER.Add3,''))>0 then ', '+isnull(TSPL_COMPANY_MASTER.Add3,'') else ' ' end + case when LEN(TSPL_COMPANY_MASTER.City_Code)>0 then ', '+TSPL_COMPANY_MASTER.City_Code else ' ' end + case when len(TSPL_COMPANY_MASTER.State )>0 then TSPL_COMPANY_MASTER.State else '' end)  as companyADD, '" & objCommonVar.CurrentCompanyName & "'  as CompName,'" & objCommonVar.CurrentCompanyCode & "'  as CompCode," & Environment.NewLine &
             "null as compLogo1,null as compLogo2,  DOC_DATE ,ROUTE_CODE,Route_Name,MCC_Code+'  Qty Mode-'+UOM  as MCC_Code,MCC_NAME ,VSP_CODE,Vsp_name ,VLC,VLC_Name,VLC_Code_VLC_Uploader,shift_date, Shift_type,no_of_cans ,case when [FAT %]<=5 then 'C' else 'B' end as TYPE ,SAMPLE_NO   ," _
            & " convert(decimal(18,2),QTY) as QTY,[FAT %]   ,[SNF %]  ,convert(DECIMAL(18,2),[FAT %]  *QTY/100) as [FAT kg],convert(DECIMAL(18,2),[SNF %]  *QTY /100) as [SNF kg]," _
            & " convert(decimal(18,2),RATE) as RATE,convert(decimal(18,2),amount) as amount,convert(decimal(18,2),NewAmount) as NewAmount ,isnull(EMP_Fixed_Amount,0) as EMP_Fixed_Amount,isnull(IncentiveEMP,0) as IncentiveEMP ,isnull(Rate_Head_Load,0) as Rate_Head_Load,isnull(Head_Load_Amount,0) as Head_Load_Amount,isnull(IncentiveEMP+Head_Load_Amount+amount,0) as [Total Amount] ," _
            & "  Village_Code ,Village_Name ,VLC_UPLOADER,RANK() over(partition by MCC_NAME " _
            & " order by sample_no,shift_date,Shift_type) as [SamNO] ,convert(varchar,DOC_DATE,103 )as Date   from (select DOC_DATE,VSP_CODE,Vsp_name ," _
            & " (TOUOM ) as UOM,(VLC_CODE) as VLC,VLC_Name,VLC_Code_VLC_Uploader ,shift_date ,Shift_type,no_of_cans ,TYPE,SAMPLE_NO ,RATE ,amount,NewAmount" _
            & " ,convert(DECIMAL(18,1),(FATQTY)/(nullif(NewQty,0))*100) as [FAT %], convert(DECIMAL(18,1),(SNFQTY )/(nullif(NewQty,0))*100) as [SNF %],(isnull(NewQty,0) ) as QTY," _
            & " ROUTE_CODE ,Route_Name ,Village_Code ,Village_Name ,MCC_Code ,MCC_NAME ,VLC_UPLOADER,IncentiveEMP,Head_Load_Amount,Rate_Head_Load,EMP_Fixed_Amount from( select DOC_DATE,VSP_CODE,Vsp_name,shift_date," _
            & " Shift_type ,TYPE,SAMPLE_NO ,xx.RATE ,amount ,amount *CF as NewAmount,ROUTE_CODE ,Route_Name ,Village_Code ,Village_Name ,MCC_Code,MCC_NAME ," _
            & " no_of_cans   ,UOM_Code,VLC_CODE,VLC_Name,VLC_Code_VLC_Uploader ,FATQTY*CF as FATQTY,SNFQTY *CF as SNFQTY,Qty*CF as NewQty, Qty,FromUOM,TOUOM," _
            & " CF,VLC_UPLOADER ,IncentiveEMP,Head_Load_Amount,Rate_Head_Load,EMP_Fixed_Amount  from( select TSPL_MILK_SRN_DETAIL.Item_Code,TSPL_VENDOR_MASTER.Vendor_Code as VSP_CODE,Vendor_Name as Vsp_name,TSPL_MILK_SRN_HEAD.DOC_DATE," _
            & " TSPL_MILK_SRN_DETAIL.UOM_Code,TSPL_VLC_MASTER_HEAD .VLC_Code_VLC_Uploader+', Name - '+VLC_Name as VLC_UPLOADER ,TSPL_MILK_SRN_HEAD.VLC_CODE+'," _
            & " Name - '+VLC_Name as VLC_CODE ,TSPL_VLC_MASTER_HEAD.VLC_Name,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader ,convert(date,TSPL_MILK_SRN_HEAD.DOC_DATE,103)" _
            & " as shift_date,case when TSPL_MILK_SRN_HEAD.SHIFT='M' then 'M'  else 'E' end as  Shift_type,TSPL_MILK_SRN_HEAD.Dock_Collection_Milk_Type as Type," _
            & " TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.no_of_cans,TSPL_MILK_SRN_HEAD.SAMPLE_NO,tspl_milk_srn_detail.RATE as RATE ,tspl_milk_srn_detail.amount as amount,TSPL_MILK_SRN_HEAD.ROUTE_CODE +'," _
            & " Name -'+Route_name as ROUTE_CODE,TSPL_mcc_ROUTE_MASTER.Route_name,TSPL_VLC_MASTER_HEAD.Village_Code,Village_Name," _
            & " TSPL_MILK_SRN_HEAD.MCC_Code+' Name- '+MCC_NAME  as MCC_Code ,TSPL_MCC_MASTER .MCC_NAME,  TSPL_MILK_SRN_DETAIL.FAT_PER,(TSPL_MILK_SRN_DETAIL.FAT_PER*" _
            & " TSPL_MILK_SRN_DETAIL.Qty/100) as FATQTY ,TSPL_MILK_SRN_DETAIL.SNF_PER,(TSPL_MILK_SRN_DETAIL.SNF_PER*TSPL_MILK_SRN_DETAIL.Qty /100) as SNFQTY ," _
            & " TSPL_MILK_SRN_DETAIL. Qty  as Qty    ,TSPL_MILK_SRN_DETAIL.EMP_Amount as IncentiveEMP,TSPL_MILK_SRN_DETAIL.Head_Load_AMOUNT,TSPL_VENDOR_MASTER.Rate_Head_Load,TSPL_VENDOR_MASTER.Actual_charges as EMP_Fixed_Amount  from TSPL_MILK_SRN_DETAIL  Left Outer Join TSPL_MILK_SRN_HEAD On TSPL_MILK_SRN_HEAD.DOC_CODE = TSPL_MILK_SRN_DETAIL.DOC_CODE 
             left outer join TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL on TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.TR_No = TSPL_MILK_SRN_HEAD.Against_Uploader_TR_No   " _
            & " left outer join TSPL_MCC_MASTER  on TSPL_MCC_MASTER .MCC_Code =TSPL_MILK_SRN_HEAD .MCC_CODE  " _
            & " inner join TSPL_VENDOR_MASTER  on TSPL_VENDOR_MASTER.Form_Type='VSP' and TSPL_MILK_SRN_HEAD .VSP_CODE =TSPL_VENDOR_MASTER .Vendor_Code" _
            & " left join TSPL_VLC_MASTER_HEAD  on TSPL_VLC_MASTER_HEAD.VLC_Code=TSPL_MILK_SRN_HEAD.VLC_CODE" _
            & " left join TSPL_mcc_ROUTE_MASTER  on TSPL_mcc_ROUTE_MASTER.Route_code=TSPL_MILK_SRN_HEAD.route_code left join TSPL_VILLAGE_MASTER vlm on " _
            & " vlm.Village_Code=TSPL_VLC_MASTER_HEAD.Village_Code " _
            & " where 2 = 2  and  convert(date,TSPL_MILK_SRN_HEAD.DOC_DATE,103) >= convert(date,'" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + "',103) and  convert(date,TSPL_MILK_SRN_HEAD.DOC_DATE,103) <= convert(date,'" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy") + "',103)"
            If clsCommon.CompairString(txtFromShift.Text, "E") = CompairStringResult.Equal Then
                sQuery += " and 2=( case when TSPL_MILK_SRN_HEAD.DOC_DATE >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and TSPL_MILK_SRN_HEAD.DOC_DATE <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and TSPL_MILK_SRN_HEAD.SHIFT='M' then 3 else 2 end  )"
            End If
            If clsCommon.CompairString(txtToShift.Text, "M") = CompairStringResult.Equal Then
                sQuery += " and 2=( case when TSPL_MILK_SRN_HEAD.DOC_DATE >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and TSPL_MILK_SRN_HEAD.DOC_DATE <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and TSPL_MILK_SRN_HEAD.SHIFT='E' then 3 else 2 end  )"
            End If

            Dim arr As List(Of String) = Nothing
            If cbtMCCRouteVLCC.CheckedValue.Count > 0 Then
                arr = cbtMCCRouteVLCC.CheckedValue(1)
                If arr IsNot Nothing AndAlso arr.Count > 0 Then
                    sQuery += "and TSPL_MCC_MASTER.MCC_Code  IN (" + clsCommon.GetMulcallString(arr) + ") "
                Else
                    Throw New Exception("Please select at least one MCC")
                End If
            End If
            If cbtMCCRouteVLCC.CheckedValue.Count > 1 Then
                arr = cbtMCCRouteVLCC.CheckedValue(2)
                If arr IsNot Nothing AndAlso arr.Count > 0 Then
                    sQuery += " and TSPL_MILK_SRN_HEAD.ROUTE_CODE in (" + clsCommon.GetMulcallString(arr) + ")  "
                Else
                    Throw New Exception("Please select at least one Route")
                End If
            End If
            If cbtMCCRouteVLCC.CheckedValue.Count > 2 Then
                arr = cbtMCCRouteVLCC.CheckedValue(3)
                If arr IsNot Nothing AndAlso arr.Count > 0 Then
                    sQuery += " and TSPL_MILK_SRN_HEAD.VLC_CODE in (" + clsCommon.GetMulcallString(arr) + ")  "
                Else
                    Throw New Exception("Please select at least one Route")
                End If
            End If

            If chkVSPSelect.IsChecked And cbgVSP.CheckedValue.Count > 0 Then
                sQuery += " and TSPL_VENDOR_MASTER.Vendor_Code  in (" + clsCommon.GetMulcallString(cbgVSP.CheckedValue) + ")"
            End If
            If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "UDL") = CompairStringResult.Equal Then
                ''richa agarwal 04 jUN,2019  TEC/28/03/19-000462 add item structure on setting based
                If ItemStructureMandatoryOnWeightConversion = True Then
                    sQuery += " ) xx LEFT OUTER JOIN TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code =xx.Item_Code left outer join (Select Distinct yyy.* From (  Select Container_UOM as FromUOM, Contained_UOM as TOUOM, Container_Qty*Contained_Qty as CF,Structure_code  from TSPL_WEIGHT_CONVERSION UNION All   Select Contained_UOM as FromUOM, Contained_UOM as TOUOM, 1 as CF,Structure_code  from TSPL_WEIGHT_CONVERSION UNION All Select Container_UOM as FromUOM, Container_UOM as TOUOM, 1 as CF,Structure_code  from TSPL_WEIGHT_CONVERSION  ) yyy) zzz on zzz.FromUOM =xx.UOM_Code   and lower(zzz.TOUOM)='" + cboUnit.Text + "' where 2 = 2 AND TSPL_ITEM_MASTER.Structure_Code =zzz.Structure_code ) ttt  )ff left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_name='" & objCommonVar.CurrentCompanyName & "')order by ff.DOC_DATE"
                Else
                    sQuery += ") xx LEFT OUTER JOIN TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code =xx.Item_Code  left outer join (Select Distinct yyy.* From ( " & Environment.NewLine & _
               " Select TOP 1 Container_UOM as FromUOM, Contained_UOM as TOUOM, Container_Qty*Contained_Qty as CF from TSPL_WEIGHT_CONVERSION WHERE Product_Type IN ('ALL','MI') order by Product_Type desc" & Environment.NewLine & _
               " UNION All Select  TOP 1 Contained_UOM as FromUOM, Container_UOM as TOUOM, Container_Qty/Contained_Qty as CF from TSPL_WEIGHT_CONVERSION WHERE  Product_Type IN ('ALL','MI') order by Product_Type desc" & Environment.NewLine & _
               " UNION All   Select  TOP 1 Contained_UOM as FromUOM, Contained_UOM as TOUOM, 1 as CF from TSPL_WEIGHT_CONVERSION WHERE  Product_Type IN ('ALL','MI') order by Product_Type desc" & Environment.NewLine & _
               " UNION All Select  TOP 1  Container_UOM as FromUOM, Container_UOM as TOUOM, 1 as CF from TSPL_WEIGHT_CONVERSION WHERE  Product_Type IN ('ALL','MI') order by Product_Type desc" & Environment.NewLine & _
                ") yyy) zzz on zzz.FromUOM =UOM_Code  and (zzz.TOUOM)='" + cboUnit.Text + "'  ) ttt  )ff left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_name='" & objCommonVar.CurrentCompanyName & "')order by ff.DOC_DATE" & Environment.NewLine
                End If
            Else

                ''richa agarwal 04 jUN,2019  TEC/28/03/19-000462 add item structure on setting based
                If ItemStructureMandatoryOnWeightConversion = True Then
                    sQuery += " ) xx LEFT OUTER JOIN TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code =xx.Item_Code left outer join (Select Distinct yyy.* From (  Select Container_UOM as FromUOM, Contained_UOM as TOUOM, Container_Qty*Contained_Qty as CF,Structure_code  from TSPL_WEIGHT_CONVERSION where Contained_UOM='KG' UNION All Select Contained_UOM as FromUOM, Container_UOM as TOUOM, Container_Qty/ nullif (Contained_Qty,0) as CF,Structure_code from TSPL_WEIGHT_CONVERSION where Container_UOM='LTR' UNION All   Select Contained_UOM as FromUOM, Contained_UOM as TOUOM, 1 as CF,Structure_code  from TSPL_WEIGHT_CONVERSION UNION All Select Container_UOM as FromUOM, Container_UOM as TOUOM, 1 as CF,Structure_code  from TSPL_WEIGHT_CONVERSION  ) yyy) zzz on zzz.FromUOM =xx.UOM_Code   and lower(zzz.TOUOM)='" + cboUnit.Text + "' where 2 = 2 AND TSPL_ITEM_MASTER.Structure_Code =zzz.Structure_code ) ttt  )ff left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_name='" & objCommonVar.CurrentCompanyName & "')order by ff.DOC_DATE"
                Else
                    sQuery += ") xx LEFT OUTER JOIN TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code =xx.Item_Code  left outer join (Select Distinct yyy.* From (  Select Container_UOM as FromUOM, Contained_UOM as TOUOM, Container_Qty*Contained_Qty as CF from TSPL_WEIGHT_CONVERSION where Contained_UOM='KG' UNION All Select Contained_UOM as FromUOM, Container_UOM as TOUOM, Container_Qty/Contained_Qty as CF from TSPL_WEIGHT_CONVERSION where Container_UOM='LTR' UNION All   Select Contained_UOM as FromUOM, Contained_UOM as TOUOM, 1 as CF from TSPL_WEIGHT_CONVERSION UNION All Select Container_UOM as FromUOM, Container_UOM as TOUOM, 1 as CF from TSPL_WEIGHT_CONVERSION  ) yyy) zzz on zzz.FromUOM =UOM_Code  and (zzz.TOUOM)='" + cboUnit.Text + "'  ) ttt  )ff left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_name='" & objCommonVar.CurrentCompanyName & "')order by ff.DOC_DATE"
                End If

            End If


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
                If btnReferesh = False Then
                    Dim frmCRV As New frmCrystalReportViewer()
                    If rdbRate.IsChecked = True Then
                        frmCRV.funsubreportWithdt(CrystalReportFolder.MilkProcurement, dtgv, clsERPFuncationality.CompanyAddresShowinHeader(), "MCCShiftReport(RouteWise)", "Milk Shift Report (Route Wise)", "Address.rpt")
                    Else
                        frmCRV.funsubreportWithdt(CrystalReportFolder.MilkProcurement, dtgv, clsERPFuncationality.CompanyAddresShowinHeader(), "MCCShiftReport(RouteWise)RateAndAmount", "Milk Shift Report (Route Wise)", "Address.rpt")
                    End If
                    frmCRV = Nothing
                End If

                RadPageView1.SelectedPage = RadPageViewPage2
                EnableDisableControl(False)
            Else
                tmpValLoad = False
                clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)
            End If
            ReStoreGridLayout()
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub
    Sub FormatGrid()
        gv.TableElement.TableHeaderHeight = 25
        gv.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gv.Columns.Count - 1
            gv.Columns(ii).ReadOnly = True
            gv.Columns(ii).IsVisible = False
        Next

        gv.Columns("VSP_CODE").IsVisible = True
        gv.Columns("VSP_CODE").Width = 100
        gv.Columns("VSP_CODE").HeaderText = " VSP Code"

        gv.Columns("Vsp_name").IsVisible = True
        gv.Columns("Vsp_name").Width = 100
        gv.Columns("Vsp_name").HeaderText = " VSP Name"

        gv.Columns("shift_date").IsVisible = True
        gv.Columns("shift_date").Width = 100
        gv.Columns("shift_date").HeaderText = " Date"
        gv.Columns("shift_date").FormatString = "{0:d}"

        gv.Columns("VLC_UPLOADER").IsVisible = False
        gv.Columns("VLC_UPLOADER").Width = 100
        gv.Columns("VLC_UPLOADER").HeaderText = " VLC Code"

        gv.Columns("VLC_Name").IsVisible = False
        gv.Columns("VLC_Name").Width = 100
        gv.Columns("VLC_Name").HeaderText = "VLC Name"

        gv.Columns("Shift_type").IsVisible = True
        gv.Columns("Shift_type").Width = 80
        gv.Columns("Shift_type").HeaderText = "Shift"

        gv.Columns("no_of_cans").IsVisible = True
        gv.Columns("no_of_cans").Width = 80
        gv.Columns("no_of_cans").HeaderText = "Cans"

        gv.Columns("TYPE").IsVisible = True
        gv.Columns("TYPE").Width = 80
        gv.Columns("TYPE").HeaderText = "Type"

        gv.Columns("SAMPLE_NO").IsVisible = True
        gv.Columns("SAMPLE_NO").Width = 50
        gv.Columns("SAMPLE_NO").HeaderText = "Sample No"

        gv.Columns("Qty").IsVisible = True
        gv.Columns("Qty").Width = 100
        gv.Columns("Qty").HeaderText = "Quantity"

        gv.Columns("FAT %").IsVisible = True
        gv.Columns("FAT %").Width = 100
        gv.Columns("FAT %").HeaderText = "FAT %"

        gv.Columns("SNF %").IsVisible = True
        gv.Columns("SNF %").Width = 100
        gv.Columns("SNF %").HeaderText = "SNF %"

        gv.Columns("FAT kg").IsVisible = True
        gv.Columns("FAT kg").Width = 100
        gv.Columns("FAT kg").HeaderText = "TFAT"

        gv.Columns("SNF kg").IsVisible = True
        gv.Columns("SNF kg").Width = 100
        gv.Columns("SNF kg").HeaderText = "TSNF"

        gv.Columns("RATE").IsVisible = True
        gv.Columns("RATE").Width = 100
        gv.Columns("RATE").HeaderText = "Rate"

        gv.Columns("NewAmount").IsVisible = True
        gv.Columns("NewAmount").Width = 100
        gv.Columns("NewAmount").HeaderText = "Amount"

        gv.Columns("Village_Code").IsVisible = False
        gv.Columns("Village_Code").Width = 100
        gv.Columns("Village_Code").HeaderText = "Village Code"

        gv.Columns("Village_Name").IsVisible = False
        gv.Columns("Village_Name").Width = 100
        gv.Columns("Village_Name").HeaderText = "Village Name"

        gv.Columns("ROUTE_CODE").IsVisible = False
        gv.Columns("ROUTE_CODE").Width = 100
        gv.Columns("ROUTE_CODE").HeaderText = "Route Code"

        gv.Columns("Village_Name").IsVisible = False
        gv.Columns("Village_Name").Width = 100
        gv.Columns("Village_Name").HeaderText = "Route Name"

        gv.Columns("MCC_Code").IsVisible = False
        gv.Columns("MCC_Code").Width = 100
        gv.Columns("MCC_Code").HeaderText = "MCC Code"

        gv.Columns("IncentiveEMP").IsVisible = True
        gv.Columns("IncentiveEMP").Width = 100
        gv.Columns("IncentiveEMP").HeaderText = "EMP Amount"

        gv.Columns("Head_Load_Amount").IsVisible = True
        gv.Columns("Head_Load_Amount").Width = 100
        gv.Columns("Head_Load_Amount").HeaderText = "Head Load Amount"

        gv.Columns("EMP_Fixed_Amount").IsVisible = True
        gv.Columns("EMP_Fixed_Amount").Width = 100
        gv.Columns("EMP_Fixed_Amount").HeaderText = "EMP %"

        gv.Columns("Rate_Head_Load").IsVisible = True
        gv.Columns("Rate_Head_Load").Width = 100
        gv.Columns("Rate_Head_Load").HeaderText = "H.L Rate"

        gv.Columns("Total Amount").IsVisible = True
        gv.Columns("Total Amount").Width = 100
        gv.Columns("Total Amount").HeaderText = "Total Amount"

        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim intCount As Integer = 0

        Dim item1 As New GridViewSummaryItem("QTY", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)
        Dim item4 As New GridViewSummaryItem("FAT kg", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item4)
        Dim item5 As New GridViewSummaryItem("SNF kg", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item5)
        Dim item7 As New GridViewSummaryItem("NewAmount", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item7)
        Dim item8 As New GridViewSummaryItem("Total Amount", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item8)
        Dim item9 As New GridViewSummaryItem("IncentiveEMP", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item9)
        Dim item10 As New GridViewSummaryItem("Head_Load_Amount", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item10)

        If chkGroupingWise.Checked = True Then
            gv.GroupDescriptors.Add(New GridGroupByExpression("MCC_Code as Item format ""{0}: {1}"" Group By MCC_Code"))
            gv.GroupDescriptors.Add(New GridGroupByExpression("ROUTE_CODE as Item format ""{0}: {1}"" Group By ROUTE_CODE"))
            gv.GroupDescriptors.Add(New GridGroupByExpression("VLC_UPLOADER as Item format ""{0}: {1}"" Group By VLC_UPLOADER"))
        Else
            gv.Columns("MCC_Code").IsVisible = True
            gv.Columns("MCC_Code").Width = 100
            gv.Columns("MCC_Code").HeaderText = "MCC Code"

            gv.Columns("MCC_NAME").IsVisible = True
            gv.Columns("MCC_NAME").Width = 100
            gv.Columns("MCC_NAME").HeaderText = "MCC Name"

            gv.Columns("ROUTE_CODE").IsVisible = True
            gv.Columns("ROUTE_CODE").Width = 100
            gv.Columns("ROUTE_CODE").HeaderText = "Route Code"

            gv.Columns("Route_Name").IsVisible = True
            gv.Columns("Route_Name").Width = 100
            gv.Columns("Route_Name").HeaderText = "Route Name"
        End If
      
        gv.ShowGroupPanel = False
        gv.MasterTemplate.AutoExpandGroups = True
        gv.MasterTemplate.ShowTotals = True
        '========update by preeti gupta Against ticket no[MIL/21/02/19-000043,MIL/21/02/19-000044]
        gv.MasterTemplate.ShowParentGroupSummaries = True
        gv.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

    End Sub
    Sub Reset()
        gv.DataSource = Nothing
        RadPageView1.SelectedPage = RadPageViewPage1
        EnableDisableControl(True)
        chkGroupingWise.Checked = True
    End Sub

    Private Sub EnableDisableControl(ByVal val As Boolean)
        RadGroupBox1.Enabled = val
        RadGroupBox5.Enabled = val
        RadGroupBox2.Enabled = val
    End Sub
    Private Sub btnGo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGo.Click
        btnReferesh = True
        PageSetupReport_ID = MyBase.Form_ID
        TemplateGridview = gv
        Load_Report()
        tmpValLoad = False
    End Sub
    Private Sub BtnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnReset.Click
        Reset()
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub
    Sub print(ByVal exporter As EnumExportTo)
        Try
            Dim arrHeader As List(Of String) = New List(Of String)()
            Dim CompName As String = clsDBFuncationality.getSingleValue("Select Comp_Name from TSPL_COMPANY_MASTER Where Comp_Code='" + objCommonVar.CurrentCompanyCode + "'")
            arrHeader.Add(CompName)
            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")

            Dim arr As List(Of String)
            If cbtMCCRouteVLCC.CheckedText.Count > 0 Then
                arr = cbtMCCRouteVLCC.CheckedText(1)
                If arr IsNot Nothing AndAlso arr.Count > 0 Then
                    arrHeader.Add(("MCC : " + clsCommon.GetMulcallStringWithComma(arr) + " "))
                End If
            End If
            If cbtMCCRouteVLCC.CheckedText.Count > 1 Then
                arr = cbtMCCRouteVLCC.CheckedText(2)
                If arr IsNot Nothing AndAlso arr.Count > 0 Then
                    arrHeader.Add(("Route : " + clsCommon.GetMulcallStringWithComma(arr) + " "))
                End If
            End If
            If cbtMCCRouteVLCC.CheckedText.Count > 2 Then
                arr = cbtMCCRouteVLCC.CheckedText(3)
                If arr IsNot Nothing AndAlso arr.Count > 0 Then
                    arrHeader.Add(("VLC : " + clsCommon.GetMulcallStringWithComma(arr) + " "))
                End If
            End If
            If chkVSPSelect.IsChecked Then
                Dim stVSPName As String = ""
                For Each StrName As String In cbgVSP.CheckedDisplayMember
                    If clsCommon.myLen(stVSPName) > 0 Then
                        stVSPName += ", "
                    End If
                    stVSPName += StrName
                Next
                Dim strVSPCode As String = ""
                For Each StrCode As String In cbgVSP.CheckedValue
                    If clsCommon.myLen(strVSPCode) > 0 Then
                        strVSPCode += ", "
                    End If
                    strVSPCode += StrCode
                Next
                arrHeader.Add(("VSP Name: " + stVSPName + " "))
            End If

            transportSql.applyExportTemplate(gv, PageSetupReport_ID)
            If exporter = EnumExportTo.Excel Then
                clsCommon.MyExportToExcelGrid("Milk Shift Report (Route Wise)", gv, arrHeader, Me.Text)
            Else
                clsCommon.MyExportToPDF("Milk Shift Report (Route Wise)", gv, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error, Me.Text)
        End Try
    End Sub
    Private Sub RadMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem2.Click
        print(EnumExportTo.Excel)
    End Sub
    Private Sub RadMenuItem3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem3.Click
        print(EnumExportTo.PDF)
    End Sub
    Private Sub FrmMCCShiftReportRouteWise_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N Then
            Load_Report()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        ElseIf e.Alt And e.KeyCode = Keys.R Then
            Reset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P Then
            Load_Report()
        End If
    End Sub
    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        btnReferesh = False
        Load_Report()
    End Sub
End Class
