
Imports common
Imports System.IO

Public Class frmRptMCCProvision
    Inherits FrmMainTranScreen
#Region "Variables"
    Dim arrBack As List(Of String)
    Dim ButtonToolTip As ToolTip = New ToolTip()

    Dim arrMCC As ArrayList

    Public FilterON As Boolean = False
    Public FilterfromDate As Date
    Public FilterToDate As Date
    Public FilterMCCCode As String
    Dim AllowManualQty As Boolean = False
    Dim isLoadData As Boolean = False
    Dim AllowRoundDownAmtForMCCDateWiseChilling As Boolean
    Dim ShowVehicleNoSeparatelyInPrimaryTransVehicleMaster As Boolean = False
#End Region

    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.MCCProvisonReport)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
    End Sub

    Private Sub frmRptTrialBalance_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso MyBase.isModifyFlag Then
            funreset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag Then
            'SaveData()
        ElseIf e.Control AndAlso e.KeyCode = Keys.P Then
            RefreshData()
            PrintData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()
        End If
    End Sub

    Private Sub FrmRptSales_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        isLoadData = True
        SetUserMgmtNew()
        LoadReportType()
        LoadShift()
        LoadTransactionType()
        arrBack = New List(Of String)
        RadPageView1.SelectedPage = RadPageViewPage1
        cboReportType.SelectedIndex = 0
      
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = txtToDate.Value.AddMonths(-1)
        LoadBlankGrid()

        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N Adding New Trasnaction")
        AllowRoundDownAmtForMCCDateWiseChilling = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowRoundDownAmtForMCCDateWiseChilling, clsFixedParameterCode.AllowRoundDownAmtForMCCDateWiseChilling, Nothing)) = 1, True, False))
        ShowVehicleNoSeparatelyInPrimaryTransVehicleMaster = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ShowVehicleNoSeparatelyInPrimaryTransVehicleMaster, clsFixedParameterCode.ShowVehicleNoSeparatelyInPrimaryTransVehicleMaster, Nothing)) > 0, True, False)
        funreset()

        If FilterON Then
            txtFromDate.Value = FilterfromDate
            txtToDate.Value = FilterToDate
            cboFromDateShift.SelectedValue = "M"
            cboToDateShift.SelectedValue = "E"
            cboTransactionType.SelectedValue = "Transport"
            cboReportType.SelectedValue = "MCC Wise Summary"
            Dim arr As New ArrayList
            arr.Add(FilterMCCCode)
            txtMCC.arrValueMember = arr
            btnRefresh.PerformClick()
        End If
        isLoadData = False
    End Sub

    Sub LoadReportType()
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))
        Dim dr As DataRow = dt.NewRow()

        dr = dt.NewRow()
        dr("Code") = "MCC Wise Summary"
        dr("Name") = "MCC Wise Summary"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "MCC Date Wise Summary"
        dr("Name") = "MCC Date Wise Summary"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Detail Report"
        dr("Name") = "Detail Report"
        dt.Rows.Add(dr)

        cboReportType.DataSource = dt
        cboReportType.ValueMember = "Code"
        cboReportType.DisplayMember = "Name"
    End Sub

    Sub LoadTransactionType()
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))
        Dim dr As DataRow = dt.NewRow()

        dr = dt.NewRow()
        dr("Code") = "Transport"
        dr("Name") = "Transport"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Chilling"
        dr("Name") = "Chilling"
        dt.Rows.Add(dr)

        cboTransactionType.DataSource = dt
        cboTransactionType.ValueMember = "Code"
        cboTransactionType.DisplayMember = "Name"
    End Sub

    Sub LoadShift()
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))
        Dim dr As DataRow = dt.NewRow()

        dr = dt.NewRow()
        dr("Code") = "M"
        dr("Name") = "Morning"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "E"
        dr("Name") = "Evening"
        dt.Rows.Add(dr)

        cboFromDateShift.DataSource = dt.Copy()
        cboFromDateShift.ValueMember = "Code"
        cboFromDateShift.DisplayMember = "Name"

        cboToDateShift.DataSource = dt.Copy()
        cboToDateShift.ValueMember = "Code"
        cboToDateShift.DisplayMember = "Name"

    End Sub

    Private Sub LoadBlankGrid()


    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        RefreshData()
        PrintData()
    End Sub

    Private Sub PrintData()
        'If clsCommon.CompairString(cbgSrcCode.Text, "Trial Balance") = CompairStringResult.Equal Then
        '    If chkRollupWise.Checked Then
        '        frmCrystalReportViewer.funreport(CrystalReportFolder.GeneralLedger, dt, "rptTrialBalanceRP", "Trial Balance")
        '    Else

        '        frmCrystalReportViewer.funreport(CrystalReportFolder.GeneralLedger, dt, "rptTrialBalance", "Trial Balance")
        '    End If
        'ElseIf clsCommon.CompairString(cbgSrcCode.Text, "Subledger Trial Balance") = CompairStringResult.Equal Then
        '    If chkRollupWise.Checked Then
        '        frmCrystalReportViewer.funreport(CrystalReportFolder.GeneralLedger, dt, "rptTrialBalanceSubLdgRP", "Trial Balance")
        '    Else
        '        frmCrystalReportViewer.funreport(CrystalReportFolder.GeneralLedger, dt, "rptTrialBalanceSubLdg", "Trial Balance")
        '    End If

        'ElseIf clsCommon.CompairString(cbgSrcCode.Text, "Period Trial Balance") = CompairStringResult.Equal Then
        '    If chkShowOPBal.Checked Then
        '        If chkRollupWise.Checked Then
        '            frmCrystalReportViewer.funreport(CrystalReportFolder.GeneralLedger, dt, "rptTrialBalancePeriodRP", "Periodical Trial Balance")
        '        Else
        '            frmCrystalReportViewer.funreport(CrystalReportFolder.GeneralLedger, dt, "rptTrialBalancePeriod", "Periodical Trial Balance")
        '        End If
        '    Else
        '        If chkRollupWise.Checked Then
        '            frmCrystalReportViewer.funreport(CrystalReportFolder.GeneralLedger, dt, "rptTrialBalancePeriodOPBalRP", "Periodical Trial Balance")
        '        Else
        '            frmCrystalReportViewer.funreport(CrystalReportFolder.GeneralLedger, dt, "rptTrialBalancePeriodOPBal", "Periodical Trial Balance")
        '        End If
        '    End If
        'ElseIf clsCommon.CompairString(cbgSrcCode.Text, "Basic Trial Balance") = CompairStringResult.Equal Then
        '    frmCrystalReportViewer.funreport(CrystalReportFolder.GeneralLedger, dt, "rptTrialBalanceBasic", "Trial Balance")
        'End If
    End Sub

    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        funreset()
        cboFromDateShift.Visible = True
        cboToDateShift.Visible = True
    End Sub

    Sub funreset()
        gv1.GroupDescriptors.Clear()
        gv1.MasterTemplate.SummaryRowsBottom.Clear()

        gv1.DataSource = Nothing
        gv1.Columns.Clear()
        gv1.Rows.Clear()
        AllowManualQty = False
        EnableDisableControls(True)
        arrBack = New List(Of String)
    End Sub

    Private Function GetSelectedDatabase() As List(Of String)
        Dim arrDBName As New List(Of String)
        'For ii As Integer = 0 To gvDB.Rows.Count - 1
        '    If txtCompany.arrValueMember Then
        '        arrDBName.Add(clsCommon.myCstr(gvDB.Rows(ii).Cells(colDataBaseName).Value))
        '    End If
        'Next
        arrDBName.Add(objCommonVar.CurrDatabase)
        Return arrDBName
    End Function

    Private Sub cmbtype_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.Data.PositionChangedEventArgs) Handles cboReportType.SelectedIndexChanged
        'chkShowOPBal.Visible = False
        ''chkRollupWise.Visible = True
        'If clsCommon.CompairString(cbgSrcCode.Text, "Period Trial Balance") = CompairStringResult.Equal Then
        '    chkShowOPBal.Visible = True
        '    chkMultipleRollup.Visible = True
        '    lblFromdate.Visible = True
        '    txtFromDate.Visible = True
        '    lblToDate.Text = "To Date"
        'ElseIf clsCommon.CompairString(cbgSrcCode.Text, "Basic Trial Balance") = CompairStringResult.Equal Then
        '    'chkRollupWise.Visible = False
        '    chkMultipleRollup.Visible = False
        '    lblFromdate.Visible = True
        '    txtFromDate.Visible = True
        '    lblToDate.Text = "To Date"
        'ElseIf clsCommon.CompairString(cbgSrcCode.Text, "Trial Balance") = CompairStringResult.Equal Then
        '    'chkRollupWise.Visible = True
        '    chkMultipleRollup.Visible = True
        '    lblFromdate.Visible = False
        '    txtFromDate.Visible = False
        '    lblToDate.Text = "As On Date"
        'ElseIf clsCommon.CompairString(cbgSrcCode.Text, "Subledger Trial Balance") = CompairStringResult.Equal Then
        '    chkMultipleRollup.Visible = True
        '    lblFromdate.Visible = True
        '    txtFromDate.Visible = True
        '    lblToDate.Text = "To Date"
        'ElseIf clsCommon.CompairString(cbgSrcCode.Text, "Location wise") = CompairStringResult.Equal Then

        'Else
        '    chkMultipleRollup.Visible = False
        'End If
        If isLoadData = False AndAlso clsCommon.CompairString(cboReportType.SelectedValue, "MCC Date Wise Summary") = CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(cboTransactionType.SelectedValue), "Chilling") = CompairStringResult.Equal Then
            cboFromDateShift.Visible = False
            cboToDateShift.Visible = False
        Else
            cboFromDateShift.Visible = True
            cboToDateShift.Visible = True
        End If

    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        AllowManualQty = False
        PageSetupReport_ID = GetReportID()
        TemplateGridview = gv1
        RefreshData()
    End Sub

    Public Sub RefreshData()
        Try
            gv1.EnableFiltering = True
            gv1.GroupDescriptors.Clear()
            gv1.MasterTemplate.SummaryRowsBottom.Clear()
            If clsCommon.GetDateWithStartTime(txtFromDate.Value) > clsCommon.GetDateWithStartTime(txtToDate.Value) Then
                Throw New Exception("To Date Cant be less than from date")
            End If

            Dim FinalQty As String = ""
            Dim BaseQry As String = ""
            If clsCommon.CompairString(clsCommon.myCstr(cboTransactionType.SelectedValue), "Transport") = CompairStringResult.Equal Then
                BaseQry = " select xx.MCC_CODE,TSPL_MCC_MASTER.MCC_NAME,xx.Route_CODE,TSPL_MCC_ROUTE_MASTER.Route_Name,xx.Vendor_Code,TSPL_VENDOR_MASTER.Vendor_Name,TransType,xx.DOC_CODE,convert(varchar,xx.DOC_DATE,103) as DOC_DATE_VIEW,xx.DOC_DATE, TSPL_PROVISION_ENTRY.Doc_No as [Provision Code] ,convert (varchar, TSPL_PROVISION_ENTRY.Doc_Date,103) as [Provision Date] ,tspl_provision_Entry.Status_Update_Doc_No as [AP Invoice No],convert(varchar, TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date) as [AP Invoice Date] ,SHIFT,Total_KM,Actual_KM,xx.Amount,xx.Vehicle from (" + Environment.NewLine ',
                BaseQry += " select TSPL_MILK_SRN_Detail.MCC_CODE,TSPL_Primary_Vehicle_Master.Vendor_Code ,TSPL_MILK_SRN_Detail.DOC_CODE,TSPL_MILK_SRN_HEAD.DOC_DATE,TSPL_MILK_SRN_HEAD.SHIFT, TSPL_MILK_SRN_HEAD.Route_CODE,0 AS Total_KM,0 AS Actual_KM,0 AS Amount,'Transport' as TransType,TSPL_Primary_Vehicle_Master.Vehicle" + Environment.NewLine +
                " from TSPL_MILK_SRN_Detail " + Environment.NewLine +
                " left outer join TSPL_MILK_SRN_HEAD on TSPL_MILK_SRN_HEAD.DOC_CODE=TSPL_MILK_SRN_Detail.DOC_CODE" + Environment.NewLine +
                " left outer join TSPL_Primary_Vehicle_Master on TSPL_Primary_Vehicle_Master.Vehicle_Code=TSPL_MILK_SRN_HEAD.VEHICLE_CODE" + Environment.NewLine +
                " where TSPL_MILK_SRN_HEAD.Posted=1  " + Environment.NewLine +
                " and TSPL_MILK_SRN_HEAD.DOC_DATE >='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and TSPL_MILK_SRN_HEAD.DOC_DATE <='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "'"
                If clsCommon.CompairString(clsCommon.myCstr(cboFromDateShift.SelectedValue), "E") = CompairStringResult.Equal Then
                    BaseQry += " and 2=( case when TSPL_MILK_SRN_HEAD.DOC_DATE >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and TSPL_MILK_SRN_HEAD.DOC_DATE <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and TSPL_MILK_SRN_HEAD.SHIFT='M' then 3 else 2 end  )"
                End If
                If clsCommon.CompairString(clsCommon.myCstr(cboToDateShift.SelectedValue), "M") = CompairStringResult.Equal Then
                    BaseQry += " and 2=( case when TSPL_MILK_SRN_HEAD.DOC_DATE >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and TSPL_MILK_SRN_HEAD.DOC_DATE <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and TSPL_MILK_SRN_HEAD.SHIFT='E' then 3 else 2 end  )"
                End If
                If txtMCC.arrValueMember IsNot Nothing AndAlso txtMCC.arrValueMember.Count > 0 Then
                    BaseQry += " and TSPL_MILK_SRN_HEAD.MCC_CODE in (" + clsCommon.GetMulcallString(txtMCC.arrValueMember) + ")"
                End If
                If txtVendor.arrValueMember IsNot Nothing AndAlso txtVendor.arrValueMember.Count > 0 Then
                    BaseQry += " and TSPL_Primary_Vehicle_Master.Vendor_Code in (" + clsCommon.GetMulcallString(txtVendor.arrValueMember) + ")"
                End If
                BaseQry += " )xx" + Environment.NewLine +
           " left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code=xx.MCC_CODE" + Environment.NewLine +
           " left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=xx.Vendor_Code" + Environment.NewLine +
           " left outer join TSPL_MCC_ROUTE_MASTER on TSPL_MCC_ROUTE_MASTER.Route_Code=xx.Route_CODE " + Environment.NewLine +
           " left outer join TSPL_PROVISION_ENTRY on TSPL_PROVISION_ENTRY.Ref_Doc_No = xx.DOC_CODE  and TSPL_PROVISION_ENTRY.Vendor_Code = xx.Vendor_Code  and TSPL_PROVISION_ENTRY.Route_Code = xx.Route_CODE  left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No = tspl_provision_Entry.Status_Update_Doc_No "
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboTransactionType.SelectedValue), "Chilling") = CompairStringResult.Equal Then
                'Dim qry As String = "select top 1 Doc_No from tspl_provision_entry where Prov_type = 'Chilling Charge' and Prog_Code = 'MCC-VSP-P' "
                'If clsCommon.myLen(clsDBFuncationality.getSingleValue(qry)) > 0 Then

                'End If


                BaseQry = " select xx.MCC_CODE,TSPL_MCC_MASTER.MCC_NAME,xx.Route_CODE,TSPL_MCC_ROUTE_MASTER.Route_Name,xx.Vendor_Code,TSPL_VENDOR_MASTER.Vendor_Name,TransType,xx.DOC_CODE,convert(varchar,xx.DOC_DATE,103) as DOC_DATE_VIEW,xx.DOC_DATE,TSPL_PROVISION_ENTRY.Doc_No as [Provision Code],convert (varchar, TSPL_PROVISION_ENTRY.Doc_Date,103) as [Provision Date],tspl_provision_Entry.Status_Update_Doc_No as [AP Invoice No],convert(varchar, TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date) as [AP Invoice Date],SHIFT,Qty,Unit,Rate,xx.Amount,xx.Vehicle from (" + Environment.NewLine
                BaseQry += " select TSPL_MILK_SRN_HEAD.MCC_CODE,TSPL_MCC_MASTER.Chilling_Vendor as Vendor_Code, TSPL_MILK_SRN_DETAIL.DOC_CODE,TSPL_MILK_SRN_HEAD.DOC_DATE,TSPL_MILK_SRN_HEAD.SHIFT,TSPL_MILK_SRN_HEAD.ROUTE_CODE,convert( decimal(18,2), (case when TSPL_MCC_MASTER.Unit_ChillingOn='K' then TSPL_MILK_SRN_DETAIL.ACC_Qty else (TSPL_MILK_SRN_DETAIL.ACC_Qty/1.03) end)) as Qty,case when TSPL_MCC_MASTER.Unit_ChillingOn='K' then 'KG' else 'LTR' end as Unit, convert(decimal(18,2), TSPL_MCC_MASTER.Chilling_Rate) as Rate , " + Environment.NewLine +
                " convert( decimal(18,2), (case when TSPL_MCC_MASTER.Unit_ChillingOn='K' then TSPL_MILK_SRN_DETAIL.ACC_Qty * TSPL_MCC_MASTER.Chilling_Rate   else (TSPL_MILK_SRN_DETAIL.ACC_Qty/1.03)*TSPL_MCC_MASTER.Chilling_Rate  end)) as Amount ,'Chilling' as TransType,TSPL_Primary_Vehicle_Master.Vehicle " + Environment.NewLine +
                " from TSPL_MILK_SRN_DETAIL" + Environment.NewLine +
                " left outer join TSPL_MILK_SRN_HEAD on TSPL_MILK_SRN_HEAD.DOC_CODE=TSPL_MILK_SRN_DETAIL.DOC_CODE" + Environment.NewLine +
                " inner join TSPL_MCC_MASTER  on TSPL_MCC_MASTER.MCC_Code=TSPL_MILK_SRN_HEAD.MCC_CODE and TSPL_MCC_MASTER.MCC_Type='Chilling Basis'" + Environment.NewLine +
                " left join TSPL_Primary_Vehicle_Master on TSPL_Primary_Vehicle_Master.VEHICLE_CODE = TSPL_MILK_SRN_HEAD.VEHICLE_CODE " + Environment.NewLine +
                " where TSPL_MILK_SRN_HEAD.Posted = 1" + Environment.NewLine +
                " and TSPL_MILK_SRN_HEAD.DOC_DATE>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and TSPL_MILK_SRN_HEAD.DOC_DATE<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "'"
                If clsCommon.CompairString(cboReportType.SelectedValue, "MCC Date Wise Summary") = CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(cboTransactionType.SelectedValue), "Chilling") = CompairStringResult.Equal Then
                Else
                    If clsCommon.CompairString(clsCommon.myCstr(cboFromDateShift.SelectedValue), "E") = CompairStringResult.Equal Then
                        BaseQry += " and 2=( case when TSPL_MILK_SRN_HEAD.DOC_DATE>= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and TSPL_MILK_SRN_HEAD.DOC_DATE <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and TSPL_MILK_SRN_HEAD.SHIFT='M' then 3 else 2 end  )"
                    End If
                    If clsCommon.CompairString(clsCommon.myCstr(cboToDateShift.SelectedValue), "M") = CompairStringResult.Equal Then
                        BaseQry += " and 2=( case when TSPL_MILK_SRN_HEAD.DOC_DATE >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and TSPL_MILK_SRN_HEAD.DOC_DATE <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and TSPL_MILK_SRN_HEAD.SHIFT='E' then 3 else 2 end  )"
                    End If
                End If

                If txtMCC.arrValueMember IsNot Nothing AndAlso txtMCC.arrValueMember.Count > 0 Then
                        BaseQry += " and TSPL_MILK_SRN_HEAD.MCC_CODE in (" + clsCommon.GetMulcallString(txtMCC.arrValueMember) + ")"
                    End If
                    If txtVendor.arrValueMember IsNot Nothing AndAlso txtVendor.arrValueMember.Count > 0 Then
                        BaseQry += " and TSPL_MCC_MASTER.Chilling_Vendor in (" + clsCommon.GetMulcallString(txtVendor.arrValueMember) + ")"
                    End If
                BaseQry += " )xx" + Environment.NewLine +
           " left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code=xx.MCC_CODE" + Environment.NewLine +
           " left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=xx.Vendor_Code" + Environment.NewLine +
           " left outer join TSPL_MCC_ROUTE_MASTER on TSPL_MCC_ROUTE_MASTER.Route_Code=xx.Route_CODE " + Environment.NewLine +
           " left outer join TSPL_PROVISION_ENTRY on TSPL_PROVISION_ENTRY.Ref_Doc_No = xx.DOC_CODE  and TSPL_PROVISION_ENTRY.Vendor_Code = xx.Vendor_Code and TSPL_PROVISION_ENTRY.Route_Code = xx.Route_CODE  left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No = tspl_provision_Entry.Status_Update_Doc_No"
            Else
                    Throw New Exception("Not a valid transaction Type")
            End If


            If clsCommon.CompairString(cboReportType.SelectedValue, "MCC Wise Summary") = CompairStringResult.Equal Then
                FinalQty = "select xxx.MCC_CODE,max(xxx.MCC_NAME) as MCC_NAME"
                If clsCommon.CompairString(clsCommon.myCstr(cboTransactionType.SelectedValue), "Transport") = CompairStringResult.Equal Then
                    FinalQty += " ,convert(decimal(18,2),sum(Total_KM)) as Total_KM,convert(decimal(18,2),sum(Actual_KM)) as Actual_KM "
                ElseIf clsCommon.CompairString(clsCommon.myCstr(cboTransactionType.SelectedValue), "Chilling") = CompairStringResult.Equal Then
                    FinalQty += ",convert(decimal(18,2), sum(Qty)) as Qty,max(Unit) as Unit,case when sum(Qty)=0 then 0 else convert(decimal(18,2), sum(Amount)/sum(qty)) end Rate "
                End If
                FinalQty += ",convert(decimal(18,2), sum(Amount)) as Amount  from (" + Environment.NewLine + BaseQry + Environment.NewLine +
                 " )xxx group by MCC_CODE"
            ElseIf clsCommon.CompairString(cboReportType.SelectedValue, "MCC Date Wise Summary") = CompairStringResult.Equal Then
                FinalQty = "select xxx. DOC_DATE_VIEW, xxx.MCC_CODE,max(xxx.MCC_NAME) as MCC_NAME"
                If clsCommon.CompairString(clsCommon.myCstr(cboTransactionType.SelectedValue), "Transport") = CompairStringResult.Equal Then
                    FinalQty += " ,convert(decimal(18,2),sum(Total_KM)) as Total_KM,convert(decimal(18,2),sum(Actual_KM)) as Actual_KM ,convert(decimal(18,2), sum(Amount)) as Amount  "
                ElseIf clsCommon.CompairString(clsCommon.myCstr(cboTransactionType.SelectedValue), "Chilling") = CompairStringResult.Equal Then
                    Dim strAmount As String = ""
                    If AllowRoundDownAmtForMCCDateWiseChilling = True Then
                        strAmount = " round ( case when convert(decimal(18,2), sum(Qty)) < =  max( TSPL_MCC_MASTER.Chilling_Assure_Qty ) then    case when sum(Qty)=0 then 0 else convert(decimal(18,2), sum(Amount)/sum(qty))  * max( TSPL_MCC_MASTER.Chilling_Assure_Qty ) end else convert(decimal(18,2), sum(Amount)) end,0,1) as Amount  "
                    Else
                        strAmount = " convert(decimal(18,2), case when convert(decimal(18,2), sum(Qty)) < =  max( TSPL_MCC_MASTER.Chilling_Assure_Qty ) then    case when sum(Qty)=0 then 0 else convert(decimal(18,2), sum(Amount)/sum(qty))  * max( TSPL_MCC_MASTER.Chilling_Assure_Qty ) end else convert(decimal(18,2), sum(Amount)) end) as Amount "
                    End If
                    FinalQty += " ,max(TSPL_MCC_MASTER.Chilling_Vendor) as Vendor_Code , max(TSPL_VENDOR_MASTER.Vendor_Name) as Vendor_Name  ,convert(decimal(18,2), sum(Qty)) as Qty,max(Unit) as Unit,case when sum(Qty)=0 then 0 else convert(decimal(18,2), sum(Amount)/sum(qty)) end Rate, " + strAmount + ", 0.00 as [Manual Qty],0 as [Manual Amount] " ' convert(decimal(18,2), sum(Amount)) as Amount
                End If
                FinalQty += "  from (" + Environment.NewLine + BaseQry + Environment.NewLine +
                 " )xxx   left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code=xxx.MCC_CODE  left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=xxx.Vendor_Code group by xxx.MCC_CODE, xxx. DOC_DATE_VIEW"
            ElseIf clsCommon.CompairString(cboReportType.SelectedValue, "Detail Report") = CompairStringResult.Equal Then
                FinalQty = BaseQry + " order by DOC_DATE,Shift desc"
            Else
                Throw New Exception("Wrong Report type")
            End If
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(FinalQty)
            FinalQty = Nothing
            BaseQry = Nothing
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                Throw New Exception("No Data found to display")
            End If
            SetGridFormation(dt)
            AllowManualQty = True
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub SetGridFormation(ByVal dt As DataTable)
        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim item1 As GridViewSummaryItem
        gv1.DataSource = Nothing
        gv1.Columns.Clear()
        gv1.Rows.Clear()
        gv1.DataSource = dt
        RadPageView1.SelectedPage = RadPageViewPage2
        RadPageViewPage2.Text = "Report ( " + clsCommon.myCstr(cboReportType.SelectedValue) + " )"
        For ii As Integer = 0 To gv1.Columns.Count - 1
            gv1.Columns(ii).ReadOnly = True
            gv1.Columns(ii).IsVisible = False
        Next

        If clsCommon.CompairString(cboReportType.SelectedValue, "MCC Wise Summary") = CompairStringResult.Equal Then
            gv1.Columns("MCC_CODE").IsVisible = True
            gv1.Columns("MCC_CODE").HeaderText = "MCC Code"

            gv1.Columns("MCC_NAME").IsVisible = True
            gv1.Columns("MCC_NAME").HeaderText = "MCC"

            gv1.Columns("Amount").IsVisible = True
            gv1.Columns("Amount").HeaderText = "Amount"
        ElseIf clsCommon.CompairString(cboReportType.SelectedValue, "MCC Date Wise Summary") = CompairStringResult.Equal Then

            gv1.Columns("DOC_DATE_VIEW").IsVisible = True
            gv1.Columns("DOC_DATE_VIEW").HeaderText = "Date"

            gv1.Columns("MCC_CODE").IsVisible = True
            gv1.Columns("MCC_CODE").HeaderText = "MCC Code"

            gv1.Columns("MCC_NAME").IsVisible = True
            gv1.Columns("MCC_NAME").HeaderText = "MCC"

            gv1.Columns("Amount").IsVisible = True
            gv1.Columns("Amount").HeaderText = "Amount"

        ElseIf clsCommon.CompairString(cboReportType.SelectedValue, "Detail Report") = CompairStringResult.Equal Then
            gv1.Columns("MCC_CODE").IsVisible = True
            gv1.Columns("MCC_CODE").HeaderText = "MCC Code"

            gv1.Columns("MCC_NAME").IsVisible = True
            gv1.Columns("MCC_NAME").HeaderText = "MCC"

            gv1.Columns("Route_CODE").IsVisible = True
            gv1.Columns("Route_CODE").HeaderText = "Route Code"

            gv1.Columns("Route_Name").IsVisible = True
            gv1.Columns("Route_Name").HeaderText = "Route"

            gv1.Columns("Vendor_Code").IsVisible = True
            gv1.Columns("Vendor_Code").HeaderText = "Vendor Code"

            gv1.Columns("Vendor_Name").IsVisible = True
            gv1.Columns("Vendor_Name").HeaderText = "Vendor"

            gv1.Columns("TransType").IsVisible = False
            gv1.Columns("TransType").HeaderText = "Transaction"

            gv1.Columns("DOC_CODE").IsVisible = True
            gv1.Columns("DOC_CODE").HeaderText = "Document No"

            gv1.Columns("DOC_DATE").IsVisible = False
            gv1.Columns("DOC_DATE").HeaderText = "Document Date"

            gv1.Columns("DOC_DATE_VIEW").IsVisible = True
            gv1.Columns("DOC_DATE_VIEW").HeaderText = "Document Date"

            gv1.Columns("Provision Code").IsVisible = True
            gv1.Columns("Provision Code").HeaderText = "Provision Code"

            gv1.Columns("Provision Date").IsVisible = True
            gv1.Columns("Provision Date").HeaderText = "Provision Date"

            gv1.Columns("AP Invoice No").IsVisible = True
            gv1.Columns("AP Invoice No").HeaderText = "AP Invoice No"

            gv1.Columns("AP Invoice Date").IsVisible = True
            gv1.Columns("AP Invoice Date").HeaderText = "AP Invoice Date"

            gv1.Columns("SHIFT").IsVisible = True
            gv1.Columns("SHIFT").HeaderText = "Shift"

            gv1.Columns("Amount").IsVisible = True
            gv1.Columns("Amount").HeaderText = "Amount"

            gv1.Columns("Vehicle").IsVisible = ShowVehicleNoSeparatelyInPrimaryTransVehicleMaster

        End If

        If clsCommon.CompairString(clsCommon.myCstr(cboTransactionType.SelectedValue), "Transport") = CompairStringResult.Equal Then
            gv1.Columns("Total_KM").IsVisible = True
            gv1.Columns("Total_KM").HeaderText = "Actual Lenght"

            gv1.Columns("Actual_KM").IsVisible = True
            gv1.Columns("Actual_KM").HeaderText = "Route Length"

            item1 = New GridViewSummaryItem("Total_KM", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item1)
            item1 = New GridViewSummaryItem("Actual_KM", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item1)
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboTransactionType.SelectedValue), "Chilling") = CompairStringResult.Equal Then
            gv1.Columns("Qty").IsVisible = True
            gv1.Columns("Qty").HeaderText = "Procure Qty"

            gv1.Columns("Unit").IsVisible = True
            gv1.Columns("Unit").HeaderText = "Unit"

            gv1.Columns("Rate").IsVisible = True
            gv1.Columns("Rate").HeaderText = "Rate"
            If clsCommon.CompairString(cboReportType.SelectedValue, "MCC Date Wise Summary") = CompairStringResult.Equal Then
                gv1.Columns("Manual Amount").IsVisible = True
                gv1.Columns("Manual Amount").HeaderText = "Manual Amount"


                gv1.Columns("Manual Qty").IsVisible = True
                gv1.Columns("Manual Qty").ReadOnly = False
                gv1.Columns("Manual Qty").HeaderText = "Manual Qty"
                'gv1.Columns("Manual Qty").FormatString = "{0:N4}" 'String.Format("{0:0.00}") '"{0:F2}"
                'CType(gv1.Columns("Manual Qty"), GridViewDecimalColumn).DecimalPlaces = 2



                gv1.Columns("Vendor_Code").IsVisible = True
                gv1.Columns("Vendor_Code").HeaderText = "Vendor Code"

                gv1.Columns("Vendor_Name").IsVisible = True
                gv1.Columns("Vendor_Name").HeaderText = "Vendor Name"

            End If


            item1 = New GridViewSummaryItem("Qty", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item1)
        End If

       
        item1 = New GridViewSummaryItem("Amount", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)

        gv1.MasterTemplate.ExpandAllGroups()
        gv1.ShowGroupPanel = False
        gv1.MasterTemplate.AutoExpandGroups = True
        gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
        EnableDisableControls(False)
        gv1.BestFitColumns()
        ReStoreGridLayout()
    End Sub

    Private Sub EnableDisableControls(ByVal Val As Boolean)
        txtMCC.Enabled = Val
        txtVendor.Enabled = Val
        cboTransactionType.Enabled = Val
        cboReportType.Enabled = Val
        txtFromDate.Enabled = Val
        txtToDate.Enabled = Val
    End Sub

    Private Sub gv1_ViewCellFormatting(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CellFormattingEventArgs) Handles gv1.ViewCellFormatting
        If TypeOf e.CellElement Is Telerik.WinControls.UI.GridSummaryCellElement Then
            e.CellElement.TextAlignment = ContentAlignment.MiddleRight
        End If
    End Sub

    Public Sub SetDiplayMember(ByVal Fnd As common.UserControls.txtMultiSelectFinder, ByVal Col_Name As String, ByVal tb_name As String, ByVal val_col_Name As String)
        Try
            Dim sQuery As String = "select TSPL_GL_SEGMENT_CODE." & Col_Name & " as Name,xxx." & val_col_Name & " as Code from (select Loc_Segment_Code  from " & tb_name & " where LEN(isnull(Loc_Segment_Code,''))>0 group by Loc_Segment_Code having Loc_Segment_Code in (" + objCommonVar.strCurrUserLocationsSegment + "))xxx left outer join TSPL_GL_SEGMENT_CODE on TSPL_GL_SEGMENT_CODE.Segment_code=xxx.Loc_Segment_Code and TSPL_GL_SEGMENT_CODE.Seg_No='7' where Loc_Segment_Code in (" & clsCommon.GetMulcallString(Fnd.arrValueMember) & ") order by xxx.Loc_Segment_Code"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(sQuery)
            Dim arrList As New ArrayList
            For Each row As DataRow In dt.Rows
                arrList.Add(row(0))
            Next
            Fnd.arrDispalyMember = arrList
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.ToString, Me.Text)
        End Try
    End Sub
    
    

    Private Sub txtMainGroup__My_Click(sender As Object, e As EventArgs) Handles txtMCC._My_Click
        Dim qry As String = "select TSPL_MCC_MASTER.MCC_Code as Code,TSPL_MCC_MASTER.MCC_NAME as Name from TSPL_MCC_MASTER"
        txtMCC.arrValueMember = clsCommon.ShowMultipleSelectForm("rptMCProM", qry, "Code", "Name", txtMCC.arrValueMember, txtMCC.arrDispalyMember)
    End Sub

    Private Sub txtGroup__My_Click(sender As Object, e As EventArgs) Handles txtVendor._My_Click
        Dim qry As String = " select Vendor_Code as Code,Vendor_Name as Name,case when Transporter='Y' then 'Transport' else 'Chilling' end  as Type from tspl_vendor_master where   (Transporter='Y' or Is_Chilling_vendor='1')"
        txtVendor.arrValueMember = clsCommon.ShowMultipleSelectForm("rptMCProV", qry, "Code", "Name", txtVendor.arrValueMember, txtVendor.arrDispalyMember)
    End Sub

    Private Sub gv1_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles gv1.CellDoubleClick
        Try
            If clsCommon.CompairString(cboReportType.SelectedValue, "MCC Wise Summary") = CompairStringResult.Equal Then
                If Not arrBack.Contains("MCC Wise Summary") Then
                    arrBack.Add("MCC Wise Summary")
                End If
                cboReportType.SelectedValue = "Detail Report"
                arrMCC = New ArrayList()
                arrMCC = txtMCC.arrValueMember()

                Dim tmp As New ArrayList()
                tmp.Add(clsCommon.myCstr(gv1.CurrentRow.Cells("MCC_CODE").Value))
                txtMCC.arrValueMember = tmp

                tmp = New ArrayList
                tmp.Add(clsCommon.myCstr(gv1.CurrentRow.Cells("MCC_NAME").Value))
                txtMCC.arrDispalyMember = tmp

                RefreshData()
            ElseIf clsCommon.CompairString(cboReportType.SelectedValue, "Detail Report") = CompairStringResult.Equal Then
                If clsCommon.myLen(gv1.CurrentRow.Cells("TransType").Value) > 0 AndAlso clsCommon.myLen(gv1.CurrentRow.Cells("DOC_CODE").Value) > 0 Then
                    If clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells("TransType").Value), "Transport") = CompairStringResult.Equal Then
                        MDI.ShowForm(clsUserMgtCode.frmMilkShiftEndMCC, "", True, clsCommon.myCstr(gv1.CurrentRow.Cells("DOC_CODE").Value))
                    ElseIf clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells("TransType").Value), "Chilling") = CompairStringResult.Equal Then
                        MDI.ShowForm(clsUserMgtCode.frmMilkSRN, "", True, clsCommon.myCstr(gv1.CurrentRow.Cells("DOC_CODE").Value))
                    End If
                End If
            ElseIf clsCommon.CompairString(cboReportType.SelectedValue, "MCC Date Wise Summary") = CompairStringResult.Equal Then

            Else
                Throw New Exception("Wrong Report type")
            End If
            PageSetupReport_ID = GetReportID()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(me,ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadButton1_Click(sender As Object, e As EventArgs) Handles RadButton1.Click
        Try
            If clsCommon.CompairString(clsCommon.myCstr(cboReportType.SelectedValue), "MCC Wise Summary") = CompairStringResult.Equal Then
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboReportType.SelectedValue), "Detail Report") = CompairStringResult.Equal Then
                If arrBack.Contains("MCC Wise Summary") Then
                    arrBack.Remove("MCC Wise Summary")
                    cboReportType.SelectedValue = "MCC Wise Summary"
                    txtMCC.arrValueMember = arrMCC
                    RefreshData()
                End If
            End If
            PageSetupReport_ID = GetReportID()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadMenuItem1_Click(sender As Object, e As EventArgs) Handles RadMenuItem1.Click
        Dim ReportID As String = GetReportID()
        If clsCommon.myLen(ReportID) > 0 Then
            gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = ReportID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv1.SaveLayout(obj.GridLayout)
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            obj.GridColumns = gv1.ColumnCount
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully",  Me.Text)
            End If
            ''stuti regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub RadMenuItem2_Click(sender As Object, e As EventArgs) Handles RadMenuItem2.Click
        clsGridLayout.DeleteData(GetReportID(), objCommonVar.CurrentUserCode)
    End Sub

    Private Function GetReportID() As String
        Dim ReportID As String = "MCCP"
        If clsCommon.CompairString(clsCommon.myCstr(cboReportType.SelectedValue), "MCC Wise Summary") = CompairStringResult.Equal Then
            ReportID += "SU"
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboReportType.SelectedValue), "Detail Report") = CompairStringResult.Equal Then
            ReportID += "DR"
        End If
        If clsCommon.CompairString(clsCommon.myCstr(cboTransactionType.SelectedValue), "Transport") = CompairStringResult.Equal Then
            ReportID += "T"
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboTransactionType.SelectedValue), "Chilling") = CompairStringResult.Equal Then
            ReportID += "C"
        End If
        Return ReportID
    End Function

    Private Sub ReStoreGridLayout()
        Try
            Dim ReportID As String = GetReportID()
            If clsCommon.myLen(ReportID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(ReportID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gv1.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gv1.Columns.Count - 1 Step ii + 1
                        gv1.Columns(ii).IsVisible = False
                        gv1.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gv1.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            common.clsCommon.MyMessageBoxShow(Me, err.Message, Me.Text)
        End Try
    End Sub

    Private Sub Print(ByVal exporter As EnumExportTo)
        Try
            If gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()

                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add("Report Type : " + clsCommon.myCstr(cboReportType.SelectedValue) + "")
                arrHeader.Add("Transaction Type : " + clsCommon.myCstr(cboTransactionType.SelectedValue) + "")
                arrHeader.Add("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + "[" + cboFromDateShift.Text + "]  To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy") + "[" + cboToDateShift.Text + "]")
                If txtMCC.arrValueMember IsNot Nothing AndAlso txtMCC.arrValueMember.Count > 0 Then
                    arrHeader.Add(txtMCC.MyLinkLable1.Text + " : " + clsCommon.GetMulcallStringWithComma(txtMCC.arrDispalyMember))
                End If
                If txtVendor.arrValueMember IsNot Nothing AndAlso txtVendor.arrValueMember.Count > 0 Then
                    arrHeader.Add(txtVendor.MyLinkLable1.Text + " : " + clsCommon.GetMulcallStringWithComma(txtVendor.arrDispalyMember))
                End If
                If exporter = EnumExportTo.Excel Then
                    'Dim sfd As SaveFileDialog = New SaveFileDialog()
                    'Dim filePath As String
                    'sfd.FileName = Me.Text
                    'sfd.Filter = "Excel 97-2003 (*.xls) |*.xls;|Excel 2007 (*.xlsx)|*.xlsx;|CSV Files (*.csv) |*.csv"
                    'If sfd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                    '    filePath = sfd.FileName
                    transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
                    transportSql.QuickExportToExcel(gv1, "", Me.Text, , arrHeader)
                    'transportSql.exportdataChilRows(gv1, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader) 'frm.Text)
                    'common.clsCommon.MyMessageBoxShow("Exported Successfully.")
                    'Process.Start(filePath)
                    'End If
                Else
                    transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
                    If clsCommon.CompairString(cboReportType.SelectedValue, "MCC Date Wise Summary") = CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(cboTransactionType.SelectedValue), "Chilling") = CompairStringResult.Equal Then
                        clsCommon.MyExportToPDF("Milk Chilling Report", gv1, arrHeader, "Milk Chilling Report", PageSetupReport_ID, objCommonVar.CurrentUserCode)
                    Else
                        clsCommon.MyExportToPDF(Me.Text, gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
                    End If

                End If
            Else
                common.clsCommon.MyMessageBoxShow(Me, "No Data Found to Export.", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

       
    End Sub


    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        Print(EnumExportTo.Excel)
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        Print(EnumExportTo.PDF)
    End Sub

    Private Sub gv1_CellValueChanged(sender As Object, e As GridViewCellEventArgs) Handles gv1.CellValueChanged
        Try
            If e.Column Is gv1.Columns("Manual Qty") AndAlso isLoadData = False AndAlso AllowManualQty = True AndAlso clsCommon.CompairString(cboReportType.SelectedValue, "MCC Date Wise Summary") = CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(cboTransactionType.SelectedValue), "Chilling") = CompairStringResult.Equal Then
                Dim dblAssure_Qty As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Chilling_Assure_Qty  from TSPL_MCC_MASTER where MCC_Code = '" + clsCommon.myCstr(gv1.CurrentRow.Cells("MCC_CODE").Value) + "' "))
                Dim dblProvionQty As Double = clsCommon.myCdbl(gv1.CurrentRow.Cells("Qty").Value)
                Dim dblMaunalQty As Double = clsCommon.myCdbl(gv1.CurrentRow.Cells("Manual Qty").Value)
                Dim CalculateQty As Double = 0
                'If dblAssure_Qty > dblProvionQty Then
                '    If dblMaunalQty < dblProvionQty AndAlso dblMaunalQty > dblAssure_Qty Then
                '        CalculateQty = dblAssure_Qty
                '    ElseIf dblMaunalQty > dblProvionQty AndAlso dblMaunalQty > dblAssure_Qty Then
                '        CalculateQty = dblProvionQty
                '    ElseIf dblMaunalQty < dblProvionQty AndAlso dblMaunalQty < dblAssure_Qty Then
                '        CalculateQty = dblMaunalQty
                '    ElseIf dblMaunalQty > dblProvionQty AndAlso dblMaunalQty > dblAssure_Qty Then
                '        CalculateQty = dblAssure_Qty
                '    End If
                'Else
                '    If dblMaunalQty < dblProvionQty AndAlso dblMaunalQty > dblAssure_Qty Then
                '        CalculateQty = dblAssure_Qty
                '    ElseIf dblMaunalQty > dblProvionQty AndAlso dblMaunalQty > dblAssure_Qty Then
                '        CalculateQty = dblAssure_Qty ' dblProvionQty
                '    ElseIf dblMaunalQty < dblProvionQty AndAlso dblMaunalQty < dblAssure_Qty Then
                '        CalculateQty = dblMaunalQty
                '    ElseIf dblMaunalQty > dblProvionQty AndAlso dblMaunalQty > dblAssure_Qty Then
                '        CalculateQty = dblAssure_Qty
                '    End If
                'End If

                If dblMaunalQty >= dblAssure_Qty AndAlso dblMaunalQty >= dblProvionQty AndAlso dblAssure_Qty >= dblProvionQty Then
                    CalculateQty = dblAssure_Qty
                ElseIf dblMaunalQty >= dblAssure_Qty AndAlso dblMaunalQty >= dblProvionQty AndAlso dblAssure_Qty <= dblProvionQty Then
                    CalculateQty = dblProvionQty
                ElseIf dblMaunalQty >= dblAssure_Qty AndAlso dblMaunalQty <= dblProvionQty AndAlso dblAssure_Qty <= dblProvionQty Then
                    CalculateQty = dblMaunalQty
                ElseIf dblMaunalQty <= dblAssure_Qty AndAlso dblMaunalQty <= dblProvionQty AndAlso dblAssure_Qty >= dblProvionQty Then
                    CalculateQty = dblAssure_Qty 'dblMaunalQty
                ElseIf dblMaunalQty <= dblAssure_Qty AndAlso dblMaunalQty <= dblProvionQty AndAlso dblAssure_Qty <= dblProvionQty Then
                    CalculateQty = dblAssure_Qty 'dblMaunalQty
                ElseIf dblMaunalQty <= dblAssure_Qty AndAlso dblMaunalQty >= dblProvionQty AndAlso dblAssure_Qty >= dblProvionQty Then
                    CalculateQty = dblAssure_Qty 'dblMaunalQty
                ElseIf dblMaunalQty <= dblAssure_Qty AndAlso dblMaunalQty >= dblProvionQty AndAlso dblAssure_Qty <= dblProvionQty Then
                    CalculateQty = dblMaunalQty

                End If


                gv1.CurrentRow.Cells("Manual Amount").Value = Math.Round(clsCommon.myCdbl(CalculateQty) * clsCommon.myCdbl(gv1.CurrentRow.Cells("Rate").Value), 2, MidpointRounding.ToEven)
                    'gv1.CurrentRow.Cells("Manual Amount").Value = Math.Round(clsCommon.myCdbl(gv1.CurrentRow.Cells("Manual Qty").Value) * clsCommon.myCdbl(gv1.CurrentRow.Cells("Rate").Value), 2, MidpointRounding.ToEven)

                End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub cboTransactionType_SelectedIndexChanged(sender As Object, e As UI.Data.PositionChangedEventArgs) Handles cboTransactionType.SelectedIndexChanged
        If isLoadData = False AndAlso clsCommon.CompairString(cboReportType.SelectedValue, "MCC Date Wise Summary") = CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(cboTransactionType.SelectedValue), "Chilling") = CompairStringResult.Equal Then
            cboFromDateShift.Visible = False
            cboToDateShift.Visible = False
        Else
            cboFromDateShift.Visible = True
            cboToDateShift.Visible = True
        End If
    End Sub
End Class
