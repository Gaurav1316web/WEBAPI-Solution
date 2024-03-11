Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common
Imports System.IO
Public Class RptShiftReportCodeWise
    '=========added tree and shift by shivani==========='
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    'preeti gupta ticket no.[BM00000004917]
    Dim arrLoc As String = Nothing

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
        ''MyBase.SetUserMgmt(clsUserMgtCode.rptShiftCodeWise)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If

    End Sub

    Private Sub RptShiftReportCodeWise_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N Then
            Load_Report()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        ElseIf e.Alt And e.KeyCode = Keys.R Then
            Reset()
        End If
    End Sub

    Private Sub btnGo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGo.Click
        Load_Report()
    End Sub

    Private Sub BtnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnReset.Click
        Reset()
    End Sub

    Private Sub btnExportToExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExportToExcel.Click
        print(EnumExportTo.Excel)
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
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
    
   
   
    Sub LoadVSP()
        Dim qry As String = "select Vendor_Code as [Code] ,Vendor_Name as [Name]  from TSPL_VENDOR_MASTER  where Form_Type ='VSP' "
        cbgVSP.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgVSP.ValueMember = "Code"
        cbgVSP.DisplayMember = "Name"

    End Sub
   
    Public Sub Load_Report()
        If txtFromDate.Value > txtToDate.Value Then
            common.clsCommon.MyMessageBoxShow("From date can not be greater then to Date", Me.Text)
            txtFromDate.Focus()
            Exit Sub
        End If
                If chkVSPSelect.IsChecked AndAlso cbgVSP.CheckedValue.Count = 0 Then
            clsCommon.MyMessageBoxShow("Please select atleast single VSP or select all.", Me.Text)
            Exit Sub
        End If
        
        If cbtMCCRouteVLCC.CheckedValue.Count = 0 Then
            clsCommon.MyMessageBoxShow("Please select atleast single MCC or select all.", Me.Text)
            Exit Sub
        End If

        Dim sQuery As String = "select (DOC_DATE),VSP_CODE,Vsp_name ,VLC,VLC_Name,shift_date,Shift_type ,TYPE ,SAMPLE_NO   ,convert(decimal(18,2),QTY) as QTY,[FAT %]   ,[SNF %]  ,convert(DECIMAL(18,2),[FAT %]  *QTY/100) as [FAT kg],convert(DECIMAL(18,2),[SNF %]  *QTY /100) as [SNF kg],convert(decimal(18,2),RATE) as RATE,convert(decimal(18,2),amount) as amount,convert(decimal(18,2),NewAmount) as NewAmount ,ROUTE_CODE ,Route_Name ,Village_Code ,Village_Name ,MCC_Code+'  Qty Mode-'+UOM  as MCC_Code ,MCC_NAME    from (select DOC_DATE,VSP_CODE,Vsp_name ,(TOUOM ) as UOM,(VLC_CODE) as VLC,VLC_Name ,shift_date ,Shift_type ,TYPE,SAMPLE_NO ,RATE ,amount,NewAmount ,convert(DECIMAL(18,1),(FATQTY)/(NewQty)*100) as [FAT %], convert(DECIMAL(18,1),(SNFQTY )/(NewQty)*100) as [SNF %],(isnull(NewQty,0) ) as QTY,ROUTE_CODE ,Route_Name ,Village_Code ,Village_Name ,MCC_Code ,MCC_NAME  from( select DOC_DATE,VSP_CODE,Vsp_name,shift_date,Shift_type ,TYPE,SAMPLE_NO ,RATE ,amount ,amount *CF as NewAmount,ROUTE_CODE ,Route_Name ,Village_Code ,Village_Name ,MCC_Code,MCC_NAME    ,UOM_Code,VLC_CODE,VLC_Name ,FATQTY*CF as FATQTY,SNFQTY *CF as SNFQTY,Qty*CF as NewQty, Qty,FromUOM,TOUOM,CF   from( select TSPL_VENDOR_MASTER.Vendor_Code as VSP_CODE,Vendor_Name as Vsp_name,TSPL_MILK_RECEIPT_HEAD.DOC_DATE,TSPL_MILK_RECEIPT_DETAIL.UOM_Code ,TSPL_MILK_RECEIPT_DETAIL.VLC_CODE+', Name - '+VLC_Name as VLC_CODE ,TSPL_VLC_MASTER_HEAD.VLC_Name,convert(date,TSPL_MILK_RECEIPT_HEAD.DOC_DATE,103) as shift_date,case when TSPL_MILK_RECEIPT_HEAD.SHIFT='M' then 'Morning'  else 'Evening' end as  Shift_type,TSPL_MILK_RECEIPT_DETAIL.TYPE,TSPL_MILK_RECEIPT_DETAIL.SAMPLE_NO,convert(DECIMAL,RATE,2)as RATE ,convert(DECIMAL,amount,2) as amount,TSPL_MILK_RECEIPT_DETAIL.ROUTE_CODE +', Name -'+Route_name as ROUTE_CODE,TSPL_mcc_ROUTE_MASTER.Route_name,TSPL_VLC_MASTER_HEAD.Village_Code,Village_Name,TSPL_MILK_RECEIPT_DETAIL.MCC_Code+' Name- '+MCC_NAME  as MCC_Code ,TSPL_MCC_MASTER .MCC_NAME,  TSPL_MILK_SAMPLE_DETAIL.FAT,(TSPL_MILK_SAMPLE_DETAIL.FAT*TSPL_MILK_SAMPLE_DETAIL.Qty/100) as FATQTY ,TSPL_MILK_SAMPLE_DETAIL.SNF,(TSPL_MILK_SAMPLE_DETAIL.SNF*TSPL_MILK_SAMPLE_DETAIL.Qty /100) as SNFQTY ,TSPL_MILK_RECEIPT_DETAIL. MILK_WEIGHT  as Qty   from TSPL_MILK_RECEIPT_DETAIL"
        sQuery += " left outer join TSPL_MILK_RECEIPT_HEAD on TSPL_MILK_RECEIPT_HEAD.DOC_CODE =TSPL_MILK_RECEIPT_DETAIL.DOC_CODE "
        sQuery += " left outer join TSPL_MCC_MASTER  on TSPL_MCC_MASTER .MCC_Code =TSPL_MILK_RECEIPT_HEAD .MCC_CODE  left outer join TSPL_MILK_SAMPLE_DETAIL on TSPL_MILK_SAMPLE_DETAIL.VLC_DOC_CODE =TSPL_MILK_RECEIPT_DETAIL.VLC_DOC_CODE "
        sQuery += " inner join TSPL_VENDOR_MASTER  on TSPL_VENDOR_MASTER.Form_Type='VSP' and TSPL_MILK_RECEIPT_DETAIL .VSP_CODE =TSPL_VENDOR_MASTER .Vendor_Code"
        sQuery += " left join TSPL_VLC_MASTER_HEAD  on TSPL_VLC_MASTER_HEAD.VLC_Code=TSPL_MILK_RECEIPT_DETAIL.VLC_CODE"
        sQuery += " left join TSPL_mcc_ROUTE_MASTER  on TSPL_mcc_ROUTE_MASTER.Route_code=TSPL_MILK_RECEIPT_DETAIL.route_code left join TSPL_VILLAGE_MASTER vlm on vlm.Village_Code=TSPL_VLC_MASTER_HEAD.Village_Code "
        sQuery += "   where 2 = 2 and convert(date,TSPL_MILK_RECEIPT_HEAD.DOC_DATE,103)>=convert(date,'" + txtFromDate.Value + "',103) and convert(date,TSPL_MILK_RECEIPT_HEAD.DOC_DATE,103) <=convert(date,'" + txtToDate.Value + "' ,103)"
        If clsCommon.CompairString(txtFromShift.Text, "E") = CompairStringResult.Equal Then
            sQuery += " and 2=( case when TSPL_MILK_RECEIPT_HEAD.DOC_DATE >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and TSPL_MILK_RECEIPT_HEAD.DOC_DATE <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and TSPL_MILK_RECEIPT_DETAIL.SHIFT='M' then 3 else 2 end  )"
        End If
        If clsCommon.CompairString(txtToShift.Text, "M") = CompairStringResult.Equal Then
            sQuery += " and 2=( case when TSPL_MILK_RECEIPT_HEAD.DOC_DATE >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and TSPL_MILK_RECEIPT_HEAD.DOC_DATE <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and TSPL_MILK_RECEIPT_DETAIL.SHIFT='E' then 3 else 2 end  )"
        End If
        'If rbtnMCCRouteVLCCSelect.IsChecked Then
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
        'End If

        'If chkMCCSelect.IsChecked And cbgMCC.CheckedValue.Count > 0 Then
        '    sQuery += "and TSPL_MCC_MASTER.MCC_Code  IN (" + clsCommon.GetMulcallString(cbgMCC.CheckedValue) + ") "
        'End If
        'If chkVLCSelect.IsChecked And cbgVLC.CheckedValue.Count > 0 Then
        '    sQuery += " and TSPL_MILK_RECEIPT_DETAIL.VLC_CODE in (" + clsCommon.GetMulcallString(cbgVLC.CheckedValue) + ")  "
        'End If
        'If chkRouteSelect.IsChecked And cbgRoute.CheckedValue.Count > 0 Then
        '    sQuery += " and TSPL_MILK_RECEIPT_DETAIL.ROUTE_CODE in (" + clsCommon.GetMulcallString(cbgRoute.CheckedValue) + ")  "
        'End If
        If chkVSPSelect.IsChecked And cbgVSP.CheckedValue.Count > 0 Then
            sQuery += " and TSPL_VENDOR_MASTER.Vendor_Code  in (" + clsCommon.GetMulcallString(cbgVSP.CheckedValue) + ")"
        End If
        'If ChkShiftSelect.IsChecked And cbgShift.CheckedValue.Count > 0 Then
        '    sQuery += " and TSPL_MILK_RECEIPT_DETAIL.SHIFT  in (" + clsCommon.GetMulcallString(cbgShift.CheckedValue) + ")"
        'End If


        sQuery += "  ) xx   left outer join (Select Distinct yyy.* From (  Select Container_UOM as FromUOM, Contained_UOM as TOUOM, Container_Qty*Contained_Qty as CF from TSPL_WEIGHT_CONVERSION UNION All Select Contained_UOM as FromUOM, Container_UOM as TOUOM, Container_Qty/Contained_Qty as CF from TSPL_WEIGHT_CONVERSION UNION All   Select Contained_UOM as FromUOM, Contained_UOM as TOUOM, 1 as CF from TSPL_WEIGHT_CONVERSION UNION All Select Container_UOM as FromUOM, Container_UOM as TOUOM, 1 as CF from TSPL_WEIGHT_CONVERSION  ) yyy) zzz on zzz.FromUOM =UOM_Code  and (zzz.TOUOM)='" + cboUnit.Text + "'  ) ttt  )ff"

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
    End Sub
    Sub FormatGrid()
        'Dim strItemCode, head2 As String

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

        gv.Columns("VLC").IsVisible = False
        gv.Columns("VLC").Width = 100
        gv.Columns("VLC").HeaderText = " VLC Code"

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
        
        Dim item7 As New GridViewSummaryItem("NewAmount", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item7)
        gv.GroupDescriptors.Add(New GridGroupByExpression("MCC_Code as Item format ""{0}: {1}"" Group By MCC_Code"))
        gv.GroupDescriptors.Add(New GridGroupByExpression("VLC as Item format ""{0}: {1}"" Group By VLC"))

        gv.ShowGroupPanel = False
        gv.MasterTemplate.AutoExpandGroups = True

        gv.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)


    End Sub
    Sub Reset()
        LOCATIONRIGTHS()
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = txtToDate.Value.AddMonths(-1)

        cboUnit.Text = "Kg"

        LoadVSP()
        LoadMCCRouteVLCTree()
        LoadShiftFrom()
        LoadShiftTo()
        'rbtnMCCRouteVLCCAll.IsChecked = True


        chkVSPAll.CheckState = CheckState.Checked
        gv.DataSource = Nothing
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

    
    Sub print(ByVal exporter As EnumExportTo)
        Try
            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")
            'If rbtnMCCRouteVLCCSelect.IsChecked Then
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
            'End If
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
            If exporter = EnumExportTo.Excel Then
                clsCommon.MyExportToExcelGrid("Shift Report Code Wise Report", gv, arrHeader, Me.Text)
            Else
                clsCommon.MyExportToPDF("Shift Report Code Wise Report", gv, arrHeader, Me.Text, True)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub

    Private Sub RptShiftReportCodeWise_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()

        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnGo, "Press Alt+N Refresh ")

        ButtonToolTip.SetToolTip(BtnReset, "Press Alt+R Adding New")
        RadPageView1.SelectedPage = RadPageViewPage1
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = txtToDate.Value.AddMonths(-1)
       
        Reset()
    End Sub

    Private Sub rmSaveLayout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmSaveLayout.Click
        If clsCommon.myLen(MyBase.Form_ID) > 0 Then
            gv1.MasterTemplate.FilterDescriptors.Clear()
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
        common.clsCommon.MyMessageBoxShow("Layout Delete successfully", "Information")
    End Sub



    Sub LoadMCCRouteVLCTree()
        'Dim qry As String = "select TSPL_VLC_MASTER_HEAD.VLC_Code as Code,TSPL_VLC_MASTER_HEAD.VLC_Name as Name,TSPL_VLC_MASTER_HEAD.Route_Code as ParentCode,3 as Lvl from TSPL_VLC_MASTER_HEAD where len(isnull(TSPL_VLC_MASTER_HEAD.Route_Code,''))>0 union all   select TSPL_MCC_ROUTE_MASTER.Route_Code as Code,TSPL_MCC_ROUTE_MASTER.Route_Name as Name,TSPL_MCC_ROUTE_MASTER.MCC_Code as ParentCode,2 as Lvl from TSPL_MCC_ROUTE_MASTER where len(isnull(TSPL_MCC_ROUTE_MASTER.MCC_Code,''))>0  union all   select TSPL_MCC_MASTER.MCC_Code as Code,TSPL_MCC_MASTER.MCC_NAME as Name,null as ParentCode,1 as Lvl from TSPL_MCC_MASTER"
        'cbtMCCRouteVLCC.ValueMember = "Code"
        'cbtMCCRouteVLCC.DisplayMember = "Name"
        'cbtMCCRouteVLCC.ParentValue = "ParentCode"
        'cbtMCCRouteVLCC.DataSource = clsDBFuncationality.GetDataTable(qry)
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
        'cbgShift.DisplayMember = "Shift"
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
        'cbgShift.DisplayMember = "Shift"
    End Sub


    Private Sub rbtnMCCRouteVLCCAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs)
        'cbtMCCRouteVLCC.Enabled = rbtnMCCRouteVLCCSelect.IsChecked
    End Sub

    
End Class
