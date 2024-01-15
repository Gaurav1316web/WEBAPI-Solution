Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common
Imports System.IO
'=========added tree and shift by shivani==========='
Public Class RptVillageSlip
    Inherits FrmMainTranScreen
    Private Const ReportID As String = "Viilage"
    Dim btnReferesh As Boolean = False
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim tmpValLoad As Boolean = True
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
        ''MyBase.SetUserMgmt(clsUserMgtCode.rptvillageslip)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        RadSplitButton1.Visible = MyBase.isExport
        btnPrint.Visible = MyBase.isPrintFlag
    End Sub
    
    Sub LoadVLC()
        Dim qry As String = "select VLC_Code as Code ,VLC_Name as Name from TSPL_VLC_MASTER_HEAD "
        cbgVLC.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgVLC.ValueMember = "Code"
        cbgVLC.DisplayMember = "Name"
    End Sub
    Sub LoadMCC()
        Dim qry As String = "select MCC_Code as Code ,MCC_NAME as Name from TSPL_MCC_MASTER"
        cbgMCC.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgMCC.ValueMember = "Code"
        cbgMCC.DisplayMember = "Name"
    End Sub
    Sub LoadRoute()
        Dim qry As String = "select Route_No as Code,Route_Desc as Name from TSPL_ROUTE_MASTER "
        cbgRoute.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgRoute.ValueMember = "Code"
        cbgRoute.DisplayMember = "Name"
    End Sub
    Sub LoadVSP()
        Dim qry As String = "select Vendor_Code as Code ,Vendor_Name as Name from TSPL_VENDOR_MASTER "
        cbgVSP.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgVSP.ValueMember = "Code"
        cbgVSP.DisplayMember = "Name"
    End Sub

    Private Sub RptVillageSlip_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ItemStructureMandatoryOnWeightConversion = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.ItemStructureMandatoryOnWeightConversion, clsFixedParameterCode.ItemStructureMandatoryOnWeightConversion, Nothing)) = 1, True, False))
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnGo, "Press Alt+R Refresh ")
        ButtonToolTip.SetToolTip(btnPrint, "Press Alt+P For Print")
        ButtonToolTip.SetToolTip(BtnReset, "Press Alt+N Adding New")
        Reset()
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

    Private Sub chkVLCAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkVLCAll.ToggleStateChanged
        cbgVLC.Enabled = Not chkVLCAll.IsChecked
    End Sub

    Private Sub chkMCCAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkMCCAll.ToggleStateChanged
        cbgMCC.Enabled = Not chkMCCAll.IsChecked
    End Sub

    Private Sub chkRouteAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkRouteAll.ToggleStateChanged
        cbgRoute.Enabled = Not chkRouteAll.IsChecked
    End Sub

    Private Sub chkVSPAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkVSPAll.ToggleStateChanged
        cbgVSP.Enabled = Not chkVSPAll.IsChecked
    End Sub
    Public Sub Load_Report()
        Dim sQuery As String
        Dim companyADD, CompName, CompCode As String
        If txtFromDate.Value > txtToDate.Value Then
            common.clsCommon.MyMessageBoxShow(Me, "From date can not be greater then to Date", Me.Text)
            txtFromDate.Focus()
            Exit Sub
        End If

        If chkVSPSelect.IsChecked AndAlso cbgVSP.CheckedValue.Count = 0 Then
            clsCommon.MyMessageBoxShow(Me, "Please select atleast single VSP or select all.", Me.Text)
            Exit Sub
        End If
        If cbtMCCRouteVLCC.CheckedValue.Count = 0 Then
            clsCommon.MyMessageBoxShow(Me, "Please select atleast single MCC or select all.", Me.Text)
            Exit Sub
        End If
        sQuery = ""
        sQuery += " select   TSPL_COMPANY_MASTER.add1 +case when len(TSPL_COMPANY_MASTER.add2)>0 then ', '+TSPL_COMPANY_MASTER.add2 else '' end +case when LEN(isnull(TSPL_COMPANY_MASTER.Add3,''))>0 then ', '+isnull(TSPL_COMPANY_MASTER.Add3,'') else ' ' end + case when LEN(TSPL_COMPANY_MASTER.City_Code)>0 then ', '+TSPL_COMPANY_MASTER.City_Code else ' ' end + case when len(TSPL_COMPANY_MASTER.State )>0 then TSPL_COMPANY_MASTER.State else '' end  as comp_address from TSPL_COMPANY_MASTER where  Comp_Code = '" + objCommonVar.CurrentCompanyCode + "' "
        Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(sQuery)
        companyADD = dt1.Rows(0).Item("comp_address")

        sQuery = ""
        sQuery += " select   TSPL_COMPANY_MASTER.Comp_Name  from TSPL_COMPANY_MASTER where  Comp_Code = '" + objCommonVar.CurrentCompanyCode + "' "
        Dim dt2 As DataTable = clsDBFuncationality.GetDataTable(sQuery)
        CompName = dt2.Rows(0).Item("Comp_Name")


        sQuery = ""
        sQuery += " select   TSPL_COMPANY_MASTER.comp_code  from TSPL_COMPANY_MASTER where  Comp_Code = '" + objCommonVar.CurrentCompanyCode + "' "
        Dim dt5 As DataTable = clsDBFuncationality.GetDataTable(sQuery)
        CompCode = dt5.Rows(0).Item("Comp_Code")
        Dim fromDate As String = txtFromDate.Value

        Dim Todate As String = txtToDate.Value
        sQuery = ""


        sQuery += "select '" & fromDate & "'  as fromDate ,'" & Todate & "'  as Todate ,'" & objCommonVar.CurrentUser & "' as User_Name , '" & companyADD & "'  as companyADD, '" & CompName & "'  as CompName,'" & CompCode & "'  as CompCode,''  as compLogo1 ,'' as compLogo2,(DOC_DATE),VSP_CODE,Vsp_name,VLC_Uploader+' ,  Agent Name - '+Vsp_name as VLC_Uploader ,VLC+' ,  Agent Name - '+Vsp_name as VLC,VLC_Name,shift_date,Shift_type ,case when [FAT %]<=5 then 'C' else 'B' end as TYPE ,SAMPLE_NO,NO_OF_CANS as [Cans]   ,convert(decimal(18,2),QTY) as QTY,[FAT %]   ,[SNF %]  ,convert(DECIMAL(18,3),[FAT %]  *QTY/100) as [FAT kg],convert(DECIMAL(18,3),[SNF %]  *QTY /100) as [SNF kg],convert(decimal(18,2),ff.RATE) as RATE,convert(decimal(18,2),amount) as amount,convert(decimal(18,2),NewAmount) as NewAmount ,ROUTE_CODE ,Route_Name ,Village_Code ,Village_Name ,MCC_Code as MCC_Code ,MCC_NAME,RANK() over(partition by vlc_name,VSP_NAme order by sample_no) as [SamNO],VLC_CODE_FOR_PRINT    from (select DOC_DATE,VSP_CODE,Vsp_name ,(TOUOM ) as UOM,(VLC_CODE) as VLC,VLC_Uploader,VLC_Name ,shift_date ,Shift_type ,TYPE,SAMPLE_NO ,ttt.RATE ,amount,NewAmount ,convert(DECIMAL(18,1),(FATQTY)/(nullif(NewQty,0))*100) as [FAT %]," & Environment.NewLine &
        " convert(DECIMAL(18,1),(SNFQTY )/(nullif(NewQty,0))*100) as [SNF %] " & Environment.NewLine &
        " ,(isnull(NewQty,0) ) as QTY,ROUTE_CODE ,Route_Name ,Village_Code ,Village_Name ,MCC_Code ,MCC_NAME,NO_OF_CANS , VLC_CODE_FOR_PRINT from( select DOC_DATE,VSP_CODE,Vsp_name,shift_date,Shift_type ,TYPE,SAMPLE_NO ,xx.RATE ,amount ,amount *CF as NewAmount,ROUTE_CODE ,Route_Name ,Village_Code ,Village_Name ,MCC_Code,MCC_NAME    ,UOM_Code,VLC_Uploader,VLC_CODE,VLC_Name ,FATQTY*CF as FATQTY,SNFQTY *CF as SNFQTY,Qty*CF as NewQty, Qty,FromUOM,TOUOM,CF,NO_OF_CANS , VLC_CODE_FOR_PRINT  from( select TSPL_MILK_RECEIPT_DETAIL.Item_Code ,TSPL_VENDOR_MASTER.Vendor_Code as VSP_CODE,Vendor_Name as Vsp_name,TSPL_MILK_RECEIPT_HEAD.DOC_DATE,TSPL_MILK_RECEIPT_DETAIL.UOM_Code,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader+', Name - '+VLC_Name as VLC_Uploader ,TSPL_MILK_RECEIPT_DETAIL.VLC_CODE+', Name - '+VLC_Name as VLC_CODE ,TSPL_VLC_MASTER_HEAD.VLC_Name,convert(date,TSPL_MILK_RECEIPT_HEAD.DOC_DATE,103) as shift_date,case when TSPL_MILK_RECEIPT_HEAD.SHIFT='M' then 'Morning'  else 'Evening' end as  Shift_type,TSPL_MILK_RECEIPT_DETAIL.TYPE,TSPL_MILK_RECEIPT_DETAIL.SAMPLE_NO,convert(DECIMAL(18,2),RATE)as RATE ,convert(DECIMAL(18,2),amount) as amount,TSPL_MILK_RECEIPT_DETAIL.ROUTE_CODE +', Name -'+Route_name as ROUTE_CODE,TSPL_mcc_ROUTE_MASTER.Route_name,TSPL_VLC_MASTER_HEAD.Village_Code,Village_Name,TSPL_MILK_RECEIPT_DETAIL.MCC_Code+' Name- '+MCC_NAME  as MCC_Code ,TSPL_MCC_MASTER .MCC_NAME,  TSPL_MILK_SAMPLE_DETAIL.FAT,(TSPL_MILK_SAMPLE_DETAIL.FAT*TSPL_MILK_SAMPLE_DETAIL.Qty/100) as FATQTY ,TSPL_MILK_SAMPLE_DETAIL.SNF,(TSPL_MILK_SAMPLE_DETAIL.SNF*TSPL_MILK_SAMPLE_DETAIL.Qty /100) as SNFQTY ,TSPL_MILK_RECEIPT_DETAIL. MILK_WEIGHT  as Qty,NO_OF_CANS ,TSPL_MILK_RECEIPT_DETAIL.VLC_CODE as VLC_CODE_FOR_PRINT   from TSPL_MILK_RECEIPT_DETAIL" & Environment.NewLine &
        " left outer join TSPL_MILK_RECEIPT_HEAD on TSPL_MILK_RECEIPT_HEAD.DOC_CODE =TSPL_MILK_RECEIPT_DETAIL.DOC_CODE " & Environment.NewLine &
        " left outer join TSPL_MCC_MASTER  on TSPL_MCC_MASTER .MCC_Code =TSPL_MILK_RECEIPT_HEAD .MCC_CODE  left outer join (select TSPL_MILK_SAMPLE_DETAIL.*,milk_receipt_code from TSPL_MILK_SAMPLE_DETAIL inner join TSPL_MILK_SAMPLE_Head on TSPL_MILK_SAMPLE_Head.doc_code= TSPL_MILK_SAMPLE_DETAIL.doc_code ) TSPL_MILK_SAMPLE_DETAIL on TSPL_MILK_SAMPLE_DETAIL.VLC_DOC_CODE =TSPL_MILK_RECEIPT_DETAIL.VLC_DOC_CODE and TSPL_MILK_SAMPLE_DETAIL.Milk_receipt_CODE=TSPL_MILK_RECEIPT_DETAIL.DOC_CODE and TSPL_MILK_RECEIPT_DETAIL.SAMPLE_NO=TSPL_MILK_sample_DETAIL.SAMPLE_NO " & Environment.NewLine &
        " inner join TSPL_VENDOR_MASTER  on TSPL_VENDOR_MASTER.Form_Type='VSP' and TSPL_MILK_RECEIPT_DETAIL .VSP_CODE =TSPL_VENDOR_MASTER .Vendor_Code" & Environment.NewLine &
        " left join TSPL_VLC_MASTER_HEAD  on TSPL_VLC_MASTER_HEAD.VLC_Code=TSPL_MILK_RECEIPT_DETAIL.VLC_CODE" & Environment.NewLine &
        " left join TSPL_mcc_ROUTE_MASTER  on TSPL_mcc_ROUTE_MASTER.Route_code=TSPL_MILK_RECEIPT_DETAIL.route_code left join TSPL_VILLAGE_MASTER vlm on vlm.Village_Code=TSPL_VLC_MASTER_HEAD.Village_Code " & Environment.NewLine &
        " where 2 = 2 and convert(date,TSPL_MILK_RECEIPT_HEAD.DOC_DATE,103)>=convert(date,'" + txtFromDate.Value + "',103) and convert(date,TSPL_MILK_RECEIPT_HEAD.DOC_DATE,103) <=convert(date,'" + txtToDate.Value + "' ,103)" & Environment.NewLine

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
                sQuery += " and TSPL_MILK_RECEIPT_DETAIL.ROUTE_CODE in (" + clsCommon.GetMulcallString(arr) + ")  "
            Else
                Throw New Exception("Please select at least one Route")
            End If
        End If
        If cbtMCCRouteVLCC.CheckedValue.Count > 2 Then
            arr = cbtMCCRouteVLCC.CheckedValue(3)
            If arr IsNot Nothing AndAlso arr.Count > 0 Then
                sQuery += " and TSPL_MILK_RECEIPT_DETAIL.VLC_CODE in (" + clsCommon.GetMulcallString(arr) + ")  "
            Else
                Throw New Exception("Please select at least one Route")
            End If
        End If

        If chkVSPSelect.IsChecked And cbgVSP.CheckedValue.Count > 0 Then
            sQuery += " and TSPL_VENDOR_MASTER.Vendor_Code  in (" + clsCommon.GetMulcallString(cbgVSP.CheckedValue) + ")"
        End If
        If clsCommon.CompairString(txtFromShift.Text, "E") = CompairStringResult.Equal Then
            sQuery += " and 2=( case when TSPL_MILK_RECEIPT_HEAD.DOC_DATE >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and TSPL_MILK_RECEIPT_HEAD.DOC_DATE <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and TSPL_MILK_RECEIPT_HEAD.SHIFT='M' then 3 else 2 end  )"
        End If
        If clsCommon.CompairString(txtToShift.Text, "M") = CompairStringResult.Equal Then
            sQuery += " and 2=( case when TSPL_MILK_RECEIPT_HEAD.DOC_DATE >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and TSPL_MILK_RECEIPT_HEAD.DOC_DATE <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and TSPL_MILK_RECEIPT_HEAD.SHIFT='E' then 3 else 2 end  )"
        End If

        sQuery += "  ) xx  " & Environment.NewLine & _
        " LEFT OUTER JOIN TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code =xx.Item_Code "

        ''richa agarwal 28 May,2019  TEC/28/03/19-000462 add item structure on setting based
        If ItemStructureMandatoryOnWeightConversion = True Then
            sQuery += " left outer join (Select Distinct yyy.* From (  Select Container_UOM as FromUOM, Contained_UOM as TOUOM, Container_Qty*Contained_Qty as CF,Structure_code  from TSPL_WEIGHT_CONVERSION UNION All   Select Contained_UOM as FromUOM, Contained_UOM as TOUOM, 1 as CF,Structure_code  from TSPL_WEIGHT_CONVERSION UNION All Select Container_UOM as FromUOM, Container_UOM as TOUOM, 1 as CF,Structure_code  from TSPL_WEIGHT_CONVERSION  ) yyy) zzz on zzz.FromUOM =xx.UOM_Code   and lower(zzz.TOUOM)='" + cboUnit.Text + "' where 2 = 2 AND TSPL_ITEM_MASTER.Structure_Code =zzz.Structure_code ) "
        Else
            sQuery += "  left outer join (Select Distinct yyy.* From ( " & Environment.NewLine & _
            " Select TOP 1 Container_UOM as FromUOM, Contained_UOM as TOUOM, Container_Qty*Contained_Qty as CF from TSPL_WEIGHT_CONVERSION WHERE Product_Type IN ('ALL','MI') order by Product_Type desc" & Environment.NewLine & _
            " UNION All Select  TOP 1 Contained_UOM as FromUOM, Container_UOM as TOUOM, Container_Qty/Contained_Qty as CF from TSPL_WEIGHT_CONVERSION WHERE  Product_Type IN ('ALL','MI') order by Product_Type desc" & Environment.NewLine & _
            " UNION All   Select  TOP 1 Contained_UOM as FromUOM, Contained_UOM as TOUOM, 1 as CF from TSPL_WEIGHT_CONVERSION WHERE  Product_Type IN ('ALL','MI') order by Product_Type desc" & Environment.NewLine & _
            " UNION All Select  TOP 1  Container_UOM as FromUOM, Container_UOM as TOUOM, 1 as CF from TSPL_WEIGHT_CONVERSION WHERE  Product_Type IN ('ALL','MI') order by Product_Type desc" & Environment.NewLine & _
            "  ) yyy) zzz on zzz.FromUOM =UOM_Code  and (zzz.TOUOM)='" + cboUnit.Text + "'  ) "
        End If

        sQuery += " ttt  )ff left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_name='" & CompName & "' order by DOC_DATE "

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
                frmCRV.funsubreportWithdt(CrystalReportFolder.MilkProcurement, dtgv, clsERPFuncationality.CompanyAddresShowinHeader(), "crptVillageSlipReport", "Village Slip Report", "Address.rpt")
                frmCRV = Nothing
            End If
            RadPageView1.SelectedPage = RadPageViewPage2
        Else
            tmpValLoad = False
            clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)
        End If
        ReStoreGridLayout()
    End Sub
    Sub FormatGrid()
        gv.TableElement.TableHeaderHeight = 25
        gv.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gv.Columns.Count - 1
            gv.Columns(ii).ReadOnly = True
            gv.Columns(ii).IsVisible = False
        Next

        gv.Columns("VSP_CODE").IsVisible = False
        gv.Columns("VSP_CODE").Width = 100
        gv.Columns("VSP_CODE").HeaderText = " VSP Code"

        gv.Columns("Vsp_name").IsVisible = False
        gv.Columns("Vsp_name").Width = 100
        gv.Columns("Vsp_name").HeaderText = " VSP Name"

        gv.Columns("shift_date").IsVisible = True
        gv.Columns("shift_date").Width = 100
        gv.Columns("shift_date").HeaderText = " Date"
        gv.Columns("shift_date").FormatString = "{0:d}"

        gv.Columns("VLC_Uploader").IsVisible = False
        gv.Columns("VLC_Uploader").Width = 100
        gv.Columns("VLC_Uploader").HeaderText = " VLC Code"

        gv.Columns("VLC_Name").IsVisible = False
        gv.Columns("VLC_Name").Width = 100
        gv.Columns("VLC_Name").HeaderText = "VLC Name"

        gv.Columns("Shift_type").IsVisible = True
        gv.Columns("Shift_type").Width = 80
        gv.Columns("Shift_type").HeaderText = "Shift"

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
        gv.GroupDescriptors.Add(New GridGroupByExpression("MCC_Code as Item format ""{0}: {1}"" Group By MCC_Code"))
        gv.GroupDescriptors.Add(New GridGroupByExpression("ROUTE_CODE as Item format ""{0}: {1}"" Group By ROUTE_CODE"))
        gv.GroupDescriptors.Add(New GridGroupByExpression("VLC_Uploader as Item format ""{0}: {1}"" Group By VLC_Uploader"))
        gv.ShowGroupPanel = False
        gv.MasterTemplate.AutoExpandGroups = True
        gv.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
    End Sub
    Sub Reset()
        LOCATIONRIGTHS()
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = txtToDate.Value.AddMonths(-1)
        LoadShiftFrom()
        LoadShiftTo()
        LoadMCCRouteVLCTree()
        LoadVSP()
        cboUnit.Text = "Kg"
        chkVSPAll.CheckState = CheckState.Checked
        RadPageView1.SelectedPage = RadPageViewPage1
        gv.DataSource = Nothing
    End Sub
    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(ReportID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(ReportID, "", objCommonVar.CurrentUserCode), clsGridLayout)
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

    Private Sub rmDeleteL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmDeleteL.Click
        clsGridLayout.DeleteData(MyBase.Form_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", "Information")
    End Sub

    Private Sub rmSaveL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmSaveL.Click
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
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully", "Information")
            End If
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
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

    Sub print(ByVal exporter As EnumExportTo)
        Try
            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
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
                clsCommon.MyExportToExcelGrid("Village Slip report", gv, arrHeader, Me.Text)
            Else
                clsCommon.MyExportToPDF("Village Slip report", gv, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub RptVillageSlip_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.R Then
            Load_Report()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            Reset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P Then
            Load_Report()
        End If
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        btnReferesh = False
        Load_Report()
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
    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        print(EnumExportTo.Excel)
    End Sub
    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        print(EnumExportTo.PDF)
    End Sub
End Class
