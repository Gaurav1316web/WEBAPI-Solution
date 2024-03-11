' Created By Preeti Gupta ticket no[BM00000004283,BM00000004353,BM00000007200]
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common
Imports System.IO
Public Class RptMilkPurchaseBill
    Inherits FrmMainTranScreen

    Dim DynamicDt As New DataTable()
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim btnReferesh As Boolean = False
    '=========added tree and shift by shivani==========='
    Dim arrLoc As String = Nothing
    Public isEmpOnAmtOnly As Boolean = False
    Dim isLoad As Boolean = True
    Dim AllowDateChanged As Boolean = False

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
        ''MyBase.SetUserMgmt(clsUserMgtCode.rptMillPurchaseBill)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        RadSplitButton1.Visible = MyBase.isExport
        btnPrint.Visible = MyBase.isPrintFlag
    End Sub

    Private Sub btnGo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGo.Click
        PageSetupReport_ID = MyBase.Form_ID
        TemplateGridview = gv
        Dim Location As String = fndLoc.Value
        Dim FrmDate As Date = dtpFromDate.Value
        Dim ToDate As Date = dtpToDate.Value
        Dim arrVSP As List(Of String) = New List(Of String)()
        Dim objpaymentProcess As New FrmPaymentProcess
        DynamicDt = Nothing
        DynamicDt = objpaymentProcess.Load_Report(Location, FrmDate, ToDate, cbgVSP.CheckedValue, True, False)

        'gv.DataSource = Nothing
        'gv.DataSource = DynamicDt

        gv.DataSource = Nothing
        gv.Rows.Clear()
        gv.Columns.Clear()

        If DynamicDt IsNot Nothing AndAlso DynamicDt.Rows.Count > 0 Then
            gv.DataSource = DynamicDt
            gv.GroupDescriptors.Clear()
            gv.MasterTemplate.SummaryRowsBottom.Clear()
            FormatGrid()
            ReStoreGridLayout()
            RadPageView1.SelectedPage = RadPageViewPage2
        End If
    End Sub

    Private Sub BtnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnReset.Click
        Reset()
    End Sub

    Private Sub rmExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmExcel.Click
        print(EnumExportTo.Excel)
    End Sub




    'Sub LoadShiftFrom()
    '    Dim dt As DataTable = New DataTable
    '    dt.Columns.Add("Code")
    '    dt.Columns.Add("Shift")

    '    Dim dr As DataRow = dt.NewRow
    '    dr("Code") = "M"
    '    dr("Shift") = "Morning"
    '    dt.Rows.Add(dr)

    '    dr = dt.NewRow
    '    dr("Code") = "E"
    '    dr("Shift") = "Evening"
    '    dt.Rows.Add(dr)

    '    txtFromShift.DataSource = dt
    '    txtFromShift.ValueMember = "Code"
    '    'cbgShift.DisplayMember = "Shift"
    'End Sub
    'Sub LoadShiftTo()
    '    Dim dt As DataTable = New DataTable
    '    dt.Columns.Add("Code")
    '    dt.Columns.Add("Shift")

    '    Dim dr As DataRow = dt.NewRow
    '    dr("Code") = "M"
    '    dr("Shift") = "Morning"
    '    dt.Rows.Add(dr)

    '    dr = dt.NewRow
    '    dr("Code") = "E"
    '    dr("Shift") = "Evening"
    '    dt.Rows.Add(dr)

    '    txtToShift.DataSource = dt
    '    txtToShift.ValueMember = "Code"
    '    'cbgShift.DisplayMember = "Shift"
    'End Sub


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

    Private Sub RptMilkPurchaseBill_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        LOCATIONRIGTHS()

        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnGo, "Press Alt+R Refresh ")
        ButtonToolTip.SetToolTip(btnPrint, "Press Alt+P for print ")
        ButtonToolTip.SetToolTip(BtnReset, "Press Alt+N Adding New ")
        RadPageView1.SelectedPage = RadPageViewPage1

        Reset()
        AllowDateChanged = True
    End Sub

    'Sub LoadVSP()
    '    Dim qry As String = "select Vendor_Code as [Code] ,Vendor_Name as [Name]  from TSPL_VENDOR_MASTER  where Form_Type ='VSP' "
    '    cbgVSP.DataSource = clsDBFuncationality.GetDataTable(qry)
    '    cbgVSP.ValueMember = "Code"
    '    cbgVSP.DisplayMember = "Name"

    'End Sub
    Sub loadVSP(Optional ByVal LocSeg As String = "")
        Dim whrCls As String = ""
        'If clsCommon.myLen(LocSeg) > 0 Then
        '    whrCls = " and vendor_code in ( select distinct VSP_Code from TSPL_VLC_MASTER_HEAD where MCC=(select Location_Code  from TSPL_LOCATION_MASTER where Loc_Segment_Code='" & LocSeg & "' and Rejected_Type='N' and Location_Category='MCC')) "
        'End If
        'Dim qry As String = "select Vendor_Code,Vendor_Name from TSPL_VENDOR_MASTER  where Form_Type='VSP' " & whrCls & " order by Vendor_Code"

        'Dim qry As String = "select xx.VSP_CODE as Code,TSPL_VENDOR_MASTER.Vendor_Name as Name,xx.VLC_CODE,TSPL_VLC_MASTER_HEAD.VLC_Name from (" + Environment.NewLine +
        '   " select VSP_CODE,max(VLC_CODE)as VLC_CODE from TSPL_MILK_SRN_Head left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_MILK_SRN_Head.MCC_CODE where TSPL_LOCATION_MASTER.Loc_Segment_Code ='" + fndLoc.Value + "' and DOC_DATE>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(dtpFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and DOC_DATE<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(dtpToDate.Value), "dd/MMM/yyyy hh:mm tt") + "' group by VSP_CODE " + Environment.NewLine +
        '   " )xx " + Environment.NewLine +
        '   " left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=xx.VSP_CODE " + Environment.NewLine +
        '   " left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code=xx.VLC_CODE " + Environment.NewLine +
        '   " order by xx.VSP_CODE"


        Dim qry As String = " select TSPL_VENDOR_MASTER.Vendor_Code ,TSPL_VENDOR_MASTER.Vendor_Name from TSPL_VENDOR_MASTER  where Form_Type ='VSP'  order by TSPL_VENDOR_MASTER.Vendor_Code"

        cbgVSP.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgVSP.ValueMember = "Vendor_Code"
        cbgVSP.DisplayMember = "Vendor_Name"
        chkVSPAll.IsChecked = True
        cbgVSP.Enabled = False
        cbgVSP.CheckedAll()
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
    'Public Sub Load_Report()
    '    Dim sQuery As String
    '    Dim companyADD, CompName, CompCode As String
    '    'If txtFromDate.Value > txtToDate.Value Then
    '    '    common.clsCommon.MyMessageBoxShow("From date can not be greater then to Date")
    '    '    txtFromDate.Focus()
    '    '    Exit Sub
    '    'End If

    '    'If chkVSPSelect.IsChecked AndAlso cbgVSP.CheckedValue.Count = 0 Then
    '    '    clsCommon.MyMessageBoxShow("Please select atleast single VSP or select all.")
    '    '    Exit Sub
    '    'End If
    '    'If cbtMCCRouteVLCC.CheckedValue.Count = 0 Then
    '    '    clsCommon.MyMessageBoxShow("Please select atleast single MCC or select all.")
    '    '    Exit Sub
    '    'End If
    '    Dim whrcls As String = " where 2=2 "
    '    'If rbtnMCCRouteVLCCSelect.IsChecked Then

    '    If clsCommon.myLen(fndLoc.Value) <= 0 Then
    '        clsCommon.MyMessageBoxShow("Please select Location segment")
    '        Exit Sub
    '    End If
    '    'Dim arr As List(Of String) = Nothing
    '    'If cbtMCCRouteVLCC.CheckedValue.Count > 0 Then
    '    '    arr = cbtMCCRouteVLCC.CheckedValue(1)
    '    '    If arr IsNot Nothing AndAlso arr.Count > 0 Then
    '    '        whrcls += "and TSPL_MILK_PURCHASE_INVOICE_HEAD.MCC_Code  IN (" + clsCommon.GetMulcallString(arr) + ") "
    '    '    Else
    '    '        Throw New Exception("Please select at least one MCC")
    '    '    End If
    '    'End If
    '    'If cbtMCCRouteVLCC.CheckedValue.Count > 1 Then
    '    '    arr = cbtMCCRouteVLCC.CheckedValue(2)
    '    '    If arr IsNot Nothing AndAlso arr.Count > 0 Then
    '    '        whrcls += " and TSPL_MILK_PURCHASE_INVOICE_HEAD.ROUTE_CODE in (" + clsCommon.GetMulcallString(arr) + ")  "
    '    '    Else
    '    '        Throw New Exception("Please select at least one Route")
    '    '    End If
    '    'End If
    '    'If cbtMCCRouteVLCC.CheckedValue.Count > 2 Then
    '    '    arr = cbtMCCRouteVLCC.CheckedValue(3)
    '    '    If arr IsNot Nothing AndAlso arr.Count > 0 Then
    '    '        whrcls += " and TSPL_MILK_PURCHASE_INVOICE_DETAIL.VLC_No in (" + clsCommon.GetMulcallString(arr) + ")  "
    '    '    Else
    '    '        Throw New Exception("Please select at least one Route")
    '    '    End If
    '    'End If
    '    ''End If


    '    If clsCommon.myLen(fndLoc.Value) > 0 Then
    '        whrcls += " and TSPL_LOCATION_MASTER.Loc_Segment_Code     IN (" + fndLoc.Value + ") "
    '    End If
    '    'If chkVLCSelect.IsChecked And cbgVLC.CheckedValue.Count > 0 Then
    '    '    whrcls += " and TSPL_MILK_PURCHASE_INVOICE_DETAIL.VLC_No in (" + clsCommon.GetMulcallString(cbgVLC.CheckedValue) + ")  "
    '    'End If
    '    'If chkRouteSelect.IsChecked And cbgRoute.CheckedValue.Count > 0 Then
    '    '    whrcls += " and TSPL_MILK_PURCHASE_INVOICE_HEAD.ROUTE_CODE in (" + clsCommon.GetMulcallString(cbgRoute.CheckedValue) + ")  "
    '    'End If
    '    If cbgVSP.CheckedValue.Count > 0 Then ''chkVSPSelect.IsChecked AndAlso
    '        whrcls += " and TSPL_MILK_PURCHASE_INVOICE_HEAD.VSP_CODE  in (" + clsCommon.GetMulcallString(cbgVSP.CheckedValue) + ")"
    '    End If
    '    whrcls += "  and convert(date,TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_DATE,103)>=convert(date,('" + dtpFromDate.Value + "'),103) and convert(date,TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_DATE,103) <=convert(date,('" + dtpToDate.Value + "'),103) "

    '    'If ChkShiftSelect.IsChecked And cbgShift.CheckedValue.Count > 0 Then
    '    '    whrcls += " and TSPL_MILK_SAMPLE_HEAD.shift  in (" + clsCommon.GetMulcallString(cbgShift.CheckedValue) + ")"
    '    'End If
    '    'Dim sQuery As String = "SELECT DOC_DATE ,VSP_CODE,Vendor_Name,SHIFT,Convert(Decimal,Quantity,2)as Quantity ,convert(Decimal,(FAT/indexx ),2)as Fat, convert(decimal,SNF/indexx,2) as SNF,convert(decimal,(Fat* Quantity)/100,2) as TFat,convert(Decimal,(SNF*Quantity)/100,2) as TSNF, convert(decimal,RATE/indexx,2) as Rate, convert (Decimal,Amount,2) as Amount ,xx.MCC_CODE+'  -  '+ MCC_NAME+'    QtyMode:'+Unit_Code as MCC_CODE,xx.ROUTE_CODE+' - '+Route_Name as ROUTE_CODE ,VSP_CODE, TSPL_MCC_MASTER.MCC_NAME,xx.Vendor_Name from (select distinct COUNT(TSPL_MILK_PURCHASE_INVOICE_HEAD.MCC_CODE) as indexx,SUM(TSPL_MILK_PURCHASE_INVOICE_DETAIL.Qty ) As Quantity,SUM(FAT_PER ) As FAT,SUM(SNF_PER ) As SNF,"
    '    'sQuery += "SUM(TSPL_MILK_PURCHASE_INVOICE_DETAIL.RATE) as RATE,Sum(TSPL_MILK_PURCHASE_INVOICE_DETAIL.Net_AMOUNT ) As Amount, TSPL_MILK_PURCHASE_INVOICE_HEAD.MCC_CODE , TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_DATE,TSPL_MILK_PURCHASE_INVOICE_HEAD.VSP_CODE ,TSPL_MILK_SAMPLE_HEAD.SHIFT, TSPL_MILK_PURCHASE_INVOICE_HEAD.ROUTE_CODE ,"
    '    'sQuery += "TSPL_VENDOR_MASTER.Vendor_Name from TSPL_MILK_PURCHASE_INVOICE_DETAIL Inner Join TSPL_MILK_PURCHASE_INVOICE_HEAD On TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE =TSPL_MILK_PURCHASE_INVOICE_DETAIL.DOC_CODE left outer join TSPL_MILK_SRN_HEAD  on TSPL_MILK_SRN_HEAD .DOC_CODE  =TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_CODE left outer join "
    '    'sQuery += "TSPL_MILK_SAMPLE_HEAD on TSPL_MILK_SAMPLE_HEAD.DOC_CODE =TSPL_MILK_SRN_HEAD.MILK_SAMPLE_CODE Left Outer Join TSPL_VENDOR_MASTER On TSPL_MILK_PURCHASE_INVOICE_HEAD.VSP_CODE =TSPL_VENDOR_MASTER.Vendor_Code And TSPL_VENDOR_MASTER.Form_Type = 'VSP' " & whrcls & " Group By TSPL_MILK_PURCHASE_INVOICE_HEAD.MCC_CODE,TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_DATE,TSPL_MILK_PURCHASE_INVOICE_HEAD.VSP_CODE,"
    '    'sQuery += " TSPL_MILK_SAMPLE_HEAD.SHIFT,TSPL_VENDOR_MASTER.Vendor_Name,TSPL_MILK_PURCHASE_INVOICE_HEAD.ROUTE_CODE )xx Left Outer Join TSPL_MCC_MASTER On xx.MCC_CODE = TSPL_MCC_MASTER.MCC_Code Left Outer Join TSPL_MCC_ROUTE_MASTER On xx.ROUTE_CODE =TSPL_MCC_ROUTE_MASTER.Route_Code"
    '    'If clsCommon.CompairString(txtFromShift.Text, "E") = CompairStringResult.Equal Then
    '    '    whrcls += " and 2=( case when TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_DATE >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(dtpFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_DATE <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(dtpFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and TSPL_MILK_SAMPLE_HEAD.SHIFT='M' then 3 else 2 end  )"
    '    'End If
    '    'If clsCommon.CompairString(txtToShift.Text, "M") = CompairStringResult.Equal Then
    '    '    whrcls += " and 2=( case when TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_DATE >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(dtpToDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_DATE <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(dtpToDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and TSPL_MILK_SAMPLE_HEAD.SHIFT='E' then 3 else 2 end  )"
    '    'End If
    '    sQuery = ""
    '    sQuery += " select   TSPL_COMPANY_MASTER.add1 +case when len(TSPL_COMPANY_MASTER.add2)>0 then ', '+TSPL_COMPANY_MASTER.add2 else '' end +case when LEN(isnull(TSPL_COMPANY_MASTER.Add3,''))>0 then ', '+isnull(TSPL_COMPANY_MASTER.Add3,'') else ' ' end + case when LEN(TSPL_COMPANY_MASTER.City_Code)>0 then ', '+TSPL_COMPANY_MASTER.City_Code else ' ' end + case when len(TSPL_COMPANY_MASTER.State )>0 then TSPL_COMPANY_MASTER.State else '' end  as comp_address from TSPL_COMPANY_MASTER where  Comp_Code = '" + objCommonVar.CurrentCompanyCode + "' "
    '    Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(sQuery)
    '    companyADD = dt1.Rows(0).Item("comp_address")

    '    sQuery = ""
    '    sQuery += " select   TSPL_COMPANY_MASTER.Comp_Name  from TSPL_COMPANY_MASTER where  Comp_Code = '" + objCommonVar.CurrentCompanyCode + "' "
    '    Dim dt2 As DataTable = clsDBFuncationality.GetDataTable(sQuery)
    '    CompName = dt2.Rows(0).Item("Comp_Name")


    '    sQuery = ""
    '    sQuery += " select   TSPL_COMPANY_MASTER.comp_code  from TSPL_COMPANY_MASTER where  Comp_Code = '" + objCommonVar.CurrentCompanyCode + "' "
    '    Dim dt5 As DataTable = clsDBFuncationality.GetDataTable(sQuery)
    '    CompCode = dt5.Rows(0).Item("Comp_Code")

    '    Dim fromDate As String = dtpFromDate.Value
    '    Dim Todate As String = dtpToDate.Value

    '    sQuery = ""

    '    sQuery += " select  '" & fromDate & "'  as fromDate ,'" & Todate & "'  as Todate ,case when '" & cbtMCCRouteVLCC.CheckedValue.Count & "'=1 then addd else '' end  as companyADD, '" & CompName & "'  as CompName,'" & CompCode & "'  as CompCode,TSPL_COMPANY_MASTER .Logo_Img   as compLogo1 ,TSPL_COMPANY_MASTER .Logo_Img2 as compLogo2,convert(varchar,DOC_DATE,103) as DOC_DATE,SHIFT,TYPE,SAMPLE_NO,convert(decimal(18,2),NewQty)as NewQty ,convert(decimal(18,1),FAT_PER) as FAT_PER ,convert(decimal(18,1),SNF_PER) as SNF_PER  ,convert(decimal(18,1),CLR)as CLR ,convert(decimal(18,2),(FAT_PER * NewQty)/100 )as TFAT,convert(decimal(18,2),(SNF_PER * NewQty)/100) as TSNF,convert(decimal(18,2),Amount/NewQty) as RATE  ,convert(decimal(18,2),Amount)as Amount ,MCC_CODE,VLC_Code ,VLC_Code_VLC_Uploader,emp,incentive,HEDAmt,AstAMT,DedAmt,coalesce(sale_AMt,0) as sale_AMt,VSP_CODE, ISNULL(Deduction_Debit_Amount,0) as Deduction_Debit_Amount, ISNULL(Deduction_MCC_Sale_Amount,0) as Deduction_MCC_Sale_Amount, ISNULL(Deduction_MCC_Sale_Return_Amount,0) as Deduction_MCC_Sale_Return_Amount, ISNULL(Deduction_Item_Issue_Amount,0) as Deduction_Item_Issue_Amount, ISNULL(Deduction_CREDIT_Amount,0) as Deduction_CREDIT_Amount,ISNULL(Issue_Return_Amount,0) as Issue_Return_Amount,Total_Basic_AMOUNT from( "
    '    sQuery += " select addd,DOC_DATE,UOM_Code,Qty as NewQty, Qty,RATE,Net_AMOUNT as Amount ,MCC_CODE+' -'+MCC_NAME+'  QtyMode :'+UOM_Code    as MCC_CODE ,VSP_CODE ,SHIFT,ROUTE_CODE+' -'+Route_Name as ROUTE_CODE  ,Vendor_Name ,MCC_NAME,SAMPLE_NO ,TYPE ,CLR,FAT_PER ,SNF_PER ,VLC_Code_VLC_Uploader+' - '+VLC_Name as VLC_Code_VLC_Uploader, VLC_Code,emp,incentive,HEDAmt,AstAMT,DedAmt,sale_AMt,Deduction_Debit_Amount, Deduction_MCC_Sale_Amount, Deduction_MCC_Sale_Return_Amount, Deduction_Item_Issue_Amount, Deduction_CREDIT_Amount,Issue_Return_Amount,Total_Basic_AMOUNT from "
    '    sQuery += "(select  sale_AMt,TSPL_MCC_MASTER.add1 + TSPL_MCC_MASTER.add2 as addd,TSPL_MILK_RECEIPT_DETAIL.UOM_Code,TSPL_MILK_PURCHASE_INVOICE_DETAIL.Qty  ,FAT_PER ,(TSPL_MILK_PURCHASE_INVOICE_DETAIL.FAT_PER*TSPL_MILK_PURCHASE_INVOICE_DETAIL.Qty/100) as FATQTY,SNF_PER,(TSPL_MILK_PURCHASE_INVOICE_DETAIL.SNF_PER *TSPL_MILK_PURCHASE_INVOICE_DETAIL.Qty/100) as SNFQTY "
    '    sQuery += " ,TSPL_MILK_PURCHASE_INVOICE_DETAIL.RATE as RATE,TSPL_MILK_PURCHASE_INVOICE_DETAIL.AMOUNT as Net_AMOUNT, TSPL_MILK_PURCHASE_INVOICE_HEAD.MCC_CODE , TSPL_MILK_RECEIPT_HEAD.DOC_DATE,TSPL_MILK_PURCHASE_INVOICE_HEAD.VSP_CODE ,TSPL_MILK_SAMPLE_HEAD.SHIFT,"
    '    sQuery += " TSPL_MILK_PURCHASE_INVOICE_HEAD.ROUTE_CODE ,TSPL_VENDOR_MASTER.Vendor_Name,TSPL_MCC_ROUTE_MASTER .Route_Name  ,TSPL_MCC_MASTER .MCC_NAME ,TSPL_MILK_SAMPLE_DETAIL.TYPE ,TSPL_MILK_SAMPLE_DETAIL.CLR,TSPL_MILK_SAMPLE_DETAIL.SAMPLE_NO ,TSPL_VLC_MASTER_HEAD.VLC_Code,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader,"
    '    sQuery += " TSPL_VLC_MASTER_HEAD.VLC_Name ,coalesce(TSPL_MILK_PURCHASE_INVOICE_HEAD.TOTAL_PaymentCOMMISSION,0) as [EMP],coalesce(TSPL_MILK_PURCHASE_INVOICE_HEAD.incentive_head,0) as Incentive,coalesce(TSPL_MILK_PURCHASE_INVOICE_HEAD.total_head_load_amount,0) as HEDAmt,coalesce(TSPL_MILK_PURCHASE_INVOICE_HEAD.total_Own_Asset_Amount,0) as AstAMT,coalesce(Total_dEDUCTION_AMOUNT,0) as DedAmt ,Deduction_Debit.Total_Amount as Deduction_Debit_Amount,Deduction_MCC_Sale.Amount as Deduction_MCC_Sale_Amount,Deduction_MCC_Sale_Return.Amount as Deduction_MCC_Sale_Return_Amount,Deduction_Item_Issue.amount as Deduction_Item_Issue_Amount,Deduction_CREDIT.Amount as Deduction_CREDIT_Amount,Issue_Return.Amount as Issue_Return_Amount,TSPL_MILK_PURCHASE_INVOICE_HEAD.Total_Basic_AMOUNT"
    '    sQuery += "  from TSPL_MILK_PURCHASE_INVOICE_DETAIL  Inner Join TSPL_MILK_PURCHASE_INVOICE_HEAD On TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE =TSPL_MILK_PURCHASE_INVOICE_DETAIL.DOC_CODE  left outer join TSPL_MILK_SRN_HEAD  on TSPL_MILK_SRN_HEAD .DOC_CODE  =TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_CODE"
    '    sQuery += "   Left Outer Join TSPL_MILK_SAMPLE_HEAD On TSPL_MILK_SAMPLE_HEAD.DOC_CODE =      TSPL_MILK_SRN_HEAD.MILK_SAMPLE_CODE    Left Outer Join TSPL_MILK_SAMPLE_DETAIL On TSPL_MILK_SAMPLE_DETAIL.DOC_CODE      = TSPL_MILK_SAMPLE_HEAD.DOC_CODE And TSPL_MILK_SAMPLE_DETAIL.VLC_DOC_CODE      = TSPL_MILK_SRN_HEAD.VLC_DOC_CODE  "
    '    sQuery += " left outer join TSPL_MILK_RECEIPT_HEAD on TSPL_MILK_RECEIPT_HEAD.DOC_CODE =TSPL_MILK_SAMPLE_HEAD.MILK_RECEIPT_CODE   left outer join TSPL_MILK_RECEIPT_DETAIL on TSPL_MILK_RECEIPT_DETAIL.DOC_CODE =TSPL_MILK_RECEIPT_HEAD.DOC_CODE and   TSPL_MILK_SRN_HEAD.vlc_doc_Code = TSPL_MILK_RECEIPT_DETAIL.VLC_DOC_CODE Left Outer Join TSPL_VENDOR_MASTER On"
    '    sQuery += " TSPL_MILK_PURCHASE_INVOICE_HEAD.VSP_CODE =TSPL_VENDOR_MASTER.Vendor_Code And TSPL_VENDOR_MASTER.Form_Type = 'VSP'   Left Outer Join TSPL_MCC_MASTER On TSPL_MILK_RECEIPT_HEAD .MCC_CODE = TSPL_MCC_MASTER.MCC_Code  left join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code =TSPL_MCC_MASTER.MCC_Code Left Outer Join TSPL_MCC_ROUTE_MASTER On TSPL_MILK_RECEIPT_DETAIL.ROUTE_CODE =TSPL_MCC_ROUTE_MASTER.Route_Code  left outer join TSPL_VLC_MASTER_HEAD on"
    '    sQuery += " TSPL_VLC_MASTER_HEAD.VLC_Code =TSPL_MILK_PURCHASE_INVOICE_DETAIL.VLC_NO  "

    '    'sQuery += " left join( "
    '    'sQuery += " select Loc_Code ,VLC_Code  ,Vendor_Code ,sum(Total_Amount ) as Total_Amount from ( select TSPL_VENDOR_INVOICE_HEAD.Loc_Code,TSPL_VENDOR_INVOICE_DETAIL.Document_No,Posting_Date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,TSPL_VLC_MASTER_HEAD.VLC_Code  ,TSPL_VENDOR_INVOICE_HEAD.Vendor_Code,TSPL_VENDOR_INVOICE_HEAD.Vendor_Name,TSPL_VENDOR_INVOICE_DETAIL.DeductionCode,TSPL_VENDOR_INVOICE_DETAIL.Deduction_Desc,TSPL_VENDOR_INVOICE_DETAIL.Total_Amount    from TSPL_VENDOR_INVOICE_DETAIL"
    '    'sQuery += " left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No =TSPL_VENDOR_INVOICE_DETAIL.Document_No"
    '    'sQuery += "  left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code =TSPL_VENDOR_INVOICE_HEAD.Vendor_Code "
    '    'sQuery += "  where TSPL_VENDOR_INVOICE_HEAD.isDeduction='1' and Document_Type='D' and ISNULL(TSPL_VENDOR_INVOICE_DETAIL.DeductionCode,'')<>''  and TSPL_VENDOR_INVOICE_HEAD.Balance_Amt<>0   and  coalesce(Posting_Date,'')<>''    "
    '    'sQuery += " and  convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103) between convert(date,'" + dtpFromDate.Value + "',103) and convert (date,'" + dtpToDate.Value + "',103)"
    '    'sQuery += ") xx group by Loc_Code ,VLC_Code ,Vendor_Code  "
    '    'sQuery += " ) deduction on  deduction.Vendor_Code =TSPL_MILK_PURCHASE_INVOICE_Head.vsp_code and deduction .VLC_Code=TSPL_MILK_RECEIPT_DETAIL.VLC_Code"
    '    '============================================ALL TYPE DEDUCTION =========================================================

    '    sQuery += "  left join(   select 'DEDUCTION' as Type,  max(convert(datetime,Invoice_Entry_Date,103)) as Invoice_Entry_Date,Loc_Code as MCC_CODE ,VLC_Code  ,Vendor_Code ,max(DeductionCode)as DeductionCode ,max(Deduction_Desc) as Deduction_Desc ,sum(Total_Amount ) as Total_Amount from "
    '    sQuery += " ( select TSPL_VENDOR_INVOICE_HEAD.Loc_Code,TSPL_VENDOR_INVOICE_DETAIL.Document_No,Posting_Date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,TSPL_VLC_MASTER_HEAD.VLC_Code  ,TSPL_VENDOR_INVOICE_HEAD.Vendor_Code,TSPL_VENDOR_INVOICE_HEAD.Vendor_Name,TSPL_VENDOR_INVOICE_DETAIL.DeductionCode,"
    '    sQuery += " TSPL_VENDOR_INVOICE_DETAIL.Deduction_Desc,TSPL_VENDOR_INVOICE_DETAIL.Total_Amount    from TSPL_VENDOR_INVOICE_DETAIL "
    '    sQuery += " left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No =TSPL_VENDOR_INVOICE_DETAIL.Document_No  "
    '    sQuery += " left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code =TSPL_VENDOR_INVOICE_HEAD.Vendor_Code "
    '    sQuery += "  where TSPL_VENDOR_INVOICE_HEAD.isDeduction='1' and Document_Type='D' and ISNULL(TSPL_VENDOR_INVOICE_DETAIL.DeductionCode,'')<>'' "
    '    sQuery += "  and TSPL_VENDOR_INVOICE_HEAD.Balance_Amt<>0   and  coalesce(Posting_Date,'')<>''  "
    '    sQuery += " and TSPL_VENDOR_INVOICE_HEAD.Vendor_Code in (" + clsCommon.GetMulcallString(cbgVSP.CheckedValue) + ") and  "
    '    sQuery += " convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103) between convert(date,'" + dtpFromDate.Value + "',103) and convert (date,'" + dtpToDate.Value + "',103) and TSPL_VENDOR_INVOICE_HEAD.Loc_Code  IN (" + fndLoc.Value + ")  ) xx group by Loc_Code ,VLC_Code ,Vendor_Code  "
    '    sQuery += " ) Deduction_Debit on  Deduction_Debit.Vendor_Code =TSPL_MILK_PURCHASE_INVOICE_Head.vsp_code and Deduction_Debit .VLC_Code=TSPL_MILK_RECEIPT_DETAIL.VLC_Code " + Environment.NewLine

    '    sQuery += "  left join (select 'MCC SALE' as Type,  max([Shipment_Date]) as [Shipment_Date],Loc_Code ,VLC_Code ,Vendor_Code ,max(Item_Code) as Item_Code ,"
    '    sQuery += " max(Item_Desc ) as Item_Desc,sum(Amount ) as Amount from(select TSPL_SD_SHIPMENT_HEAD.Document_Code , TSPL_SD_SHIPMENT_HEAD.Document_Date as [Shipment_Date],"
    '    sQuery += " TSPL_Customer_Invoice_Head.Loc_Code,TSPL_VLC_MASTER_HEAD.VLC_Code  ,TSPL_CUSTOMER_VENDOR_MAPPING.vendor_code [Vendor_Code] ,"
    '    sQuery += " TSPL_VENDOR_MASTER.Vendor_Name as [Vendor_Name] ,TSPL_SD_SHIPMENT_DETAIL.Item_Code,TSPL_ITEM_MASTER.Item_Desc  , TSPL_SD_SHIPMENT_DETAIL.Amount  "
    '    sQuery += "  from TSPL_SD_SHIPMENT_HEAD  left join TSPL_SD_SHIPMENT_DETAIL on TSPL_SD_SHIPMENT_HEAD.Document_Code =TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE "
    '    sQuery += "  left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code =TSPL_SD_SHIPMENT_DETAIL.Item_Code  "
    '    sQuery += " left outer join  TSPL_CUSTOMER_VENDOR_MAPPING on TSPL_CUSTOMER_VENDOR_MAPPING.Cust_Code=TSPL_SD_SHIPMENT_HEAD.Customer_Code  "
    '    sQuery += "  inner join TSPL_Customer_Invoice_Head on  TSPL_Customer_Invoice_Head.Against_Sale_No=TSPL_SD_SHIPMENT_HEAD.Sale_Invoice_No  "
    '    sQuery += " and coalesce(TSPL_Customer_Invoice_Head.Against_Sale_No,'')<>''  "
    '    sQuery += " left outer join TSPL_VENDOR_MASTER   on  TSPL_VENDOR_MASTER .Vendor_code  =TSPL_CUSTOMER_VENDOR_MAPPING.vendor_code  "
    '    sQuery += "  left join  TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code =TSPL_CUSTOMER_VENDOR_MAPPING.vendor_code  "
    '    sQuery += " left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code=TSPL_SD_SHIPMENT_HEAD.Sale_Invoice_No "
    '    sQuery += "   where TSPL_SD_SHIPMENT_HEAD.Trans_Type='MCC'  and tspl_customer_invoice_head.Balance_Amt<>0   "
    '    sQuery += " and TSPL_CUSTOMER_VENDOR_MAPPING.Vendor_Code in (" + clsCommon.GetMulcallString(cbgVSP.CheckedValue) + ") and "
    '    sQuery += " convert(date,TSPL_SD_SHIPMENT_HEAD.Document_Date,103) between convert(date,'" + dtpFromDate.Value + "',103) and convert (date,'" + dtpToDate.Value + "',103) and TSPL_Customer_Invoice_Head.Loc_Code  IN (" + fndLoc.Value + ") ) xx group by Loc_Code ,Vendor_Code,VLC_Code"
    '    sQuery += ") as Deduction_MCC_Sale on  Deduction_MCC_Sale.Vendor_Code =TSPL_MILK_PURCHASE_INVOICE_Head.vsp_code and Deduction_MCC_Sale .VLC_Code=TSPL_MILK_RECEIPT_DETAIL.VLC_Code " + Environment.NewLine

    '    sQuery += " left join (select 'MCC SALE RETURN' as Type,max(Shipment_Date )as Shipment_Date,Loc_Code ,VLC_Code ,Vendor_Code ,max(Item_Code) as Item_Code ,"
    '    sQuery += " max(Item_Desc) as Item_Desc,sum(Amount )  as Amount from ( select TSPL_SD_SHIPMENT_HEAD.Document_Code ,TSPL_Customer_Invoice_Head.Loc_Code, "
    '    sQuery += " TSPL_SD_SHIPMENT_HEAD.Document_Date as [Shipment_Date],TSPL_VLC_MASTER_HEAD.VLC_Code  ,TSPL_CUSTOMER_VENDOR_MAPPING.vendor_code [Vendor_Code] , "
    '    sQuery += " TSPL_VENDOR_MASTER.Vendor_Name as [Vendor_Name],TSPL_SD_SALE_RETURN_DETAIL.Item_Code,tspl_item_master.Item_Desc  ,TSPL_SD_SALE_RETURN_DETAIL.Amount  "
    '    sQuery += "  from TSPL_SD_SALE_RETURN_HEAD left join TSPL_SD_SALE_RETURN_DETAIL on TSPL_SD_SALE_RETURN_DETAIL.DOCUMENT_CODE =TSPL_SD_SALE_RETURN_HEAD.Document_Code "
    '    sQuery += " left join tspl_item_master on tspl_item_master .Item_Code =TSPL_SD_SALE_RETURN_DETAIL.Item_Code  "
    '    sQuery += " left join TSPL_SD_SHIPMENT_HEAD on  TSPL_SD_SHIPMENT_HEAD.Sale_Invoice_No=TSPL_SD_SALE_RETURN_HEAD.Against_Invoice_No  "
    '    sQuery += " left outer join  TSPL_CUSTOMER_VENDOR_MAPPING on  TSPL_CUSTOMER_VENDOR_MAPPING.Cust_Code=TSPL_SD_SHIPMENT_HEAD.Customer_Code   "
    '    sQuery += " inner join TSPL_Customer_Invoice_Head on   TSPL_Customer_Invoice_Head.against_Mcc_Material_Sale_return=TSPL_SD_SALE_RETURN_HEAD.Document_Code "
    '    sQuery += " and coalesce(TSPL_Customer_Invoice_Head.against_Mcc_Material_Sale_return,'')<>''  "
    '    sQuery += "  left outer join TSPL_VENDOR_MASTER   on  TSPL_VENDOR_MASTER .Vendor_code  =TSPL_CUSTOMER_VENDOR_MAPPING.vendor_code"
    '    sQuery += "  left join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code  =TSPL_CUSTOMER_VENDOR_MAPPING.vendor_code "
    '    sQuery += " left outer join  TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code=TSPL_SD_SHIPMENT_HEAD.Sale_Invoice_No   "
    '    sQuery += " where TSPL_SD_SHIPMENT_HEAD.Trans_Type='MCC'  "
    '    sQuery += "  and  tspl_customer_invoice_head.Balance_Amt<>0 "
    '    sQuery += " and TSPL_CUSTOMER_VENDOR_MAPPING.Vendor_Code in (" + clsCommon.GetMulcallString(cbgVSP.CheckedValue) + ") "
    '    sQuery += " and  convert(date,TSPL_SD_SHIPMENT_HEAD.Document_Date,103) between convert(date,'" + dtpFromDate.Value + "',103) and convert (date,'" + dtpToDate.Value + "',103) and  TSPL_Customer_Invoice_Head.Loc_Code  IN (" + fndLoc.Value + ")   )xx group by Loc_Code ,Vendor_Code ,VLC_Code   "
    '    sQuery += " ) as Deduction_MCC_Sale_Return on  Deduction_MCC_Sale_Return.Vendor_Code =TSPL_MILK_PURCHASE_INVOICE_Head.vsp_code and Deduction_MCC_Sale_Return .VLC_Code=TSPL_MILK_RECEIPT_DETAIL.VLC_Code " + Environment.NewLine

    '    sQuery += " left join (select 'ITEM ISSUE' as TYPE,max(Item_Issue_Doc_Date) as Item_Issue_Doc_Date,From_Location ,VLC_Code ,vendor_Code ,max(Item_Code) as Item_Code ,"
    '    sQuery += " max(Item_Desc) as Item_Desc,sum(Amount )as Amount  from (select TSPL_VSPItem_HEAD.Doc_Date  as [Item_Issue_Doc_Date] ,TSPL_VSPItem_HEAD.From_Location,VLC_Code  ,"
    '    sQuery += "  TSPL_VSPItem_HEAD.Issue_To as [vendor_Code] ,TSPL_VENDOR_MASTER.Vendor_Name,TSPL_VSPItem_DETAIL.Item_Code ,tspl_item_master.Item_Desc ,TSPL_VSPItem_DETAIL.Amount  "
    '    sQuery += "  from TSPL_VENDOR_INVOICE_HEAD   inner join TSPL_VSPItem_HEAD on TSPL_VSPItem_HEAD .Doc_No=TSPL_VENDOR_INVOICE_HEAD.Against_VSPItemIssue_No     "
    '    sQuery += " left join TSPL_VSPItem_DETAIL on TSPL_VSPItem_DETAIL.Doc_No =TSPL_VSPItem_HEAD.Doc_No  	"
    '    sQuery += " left join tspl_item_master on TSPL_ITEM_MASTER.Item_Code = TSPL_VSPItem_DETAIL.Item_Code  "
    '    sQuery += " left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_VSPItem_HEAD.Issue_To "
    '    sQuery += "  left join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code =TSPL_VSPItem_HEAD.Issue_To  "
    '    sQuery += "  where TSPL_VENDOR_INVOICE_HEAD.Balance_Amt <> 0"
    '    sQuery += " and TSPL_VSPItem_HEAD.From_Location in  (  select location_code from TSPL_LOCATION_MASTER where Loc_Segment_Code  IN (" + fndLoc.Value + ") )   "
    '    sQuery += " and TSPL_VSPItem_HEAD.Issue_To in (" + clsCommon.GetMulcallString(cbgVSP.CheckedValue) + ") and "
    '    sQuery += " convert(date,TSPL_VSPItem_HEAD.Doc_Date ,103) between convert(date,'" + dtpFromDate.Value + "',103) and convert (date,'" + dtpToDate.Value + "',103)  ) xx group by   From_Location, VLC_Code, vendor_Code "
    '    sQuery += " ) as Deduction_Item_Issue on  Deduction_Item_Issue.Vendor_Code =TSPL_MILK_PURCHASE_INVOICE_Head.vsp_code and Deduction_Item_Issue .VLC_Code=TSPL_MILK_RECEIPT_DETAIL.VLC_Code " + Environment.NewLine
    '    sQuery += " left join (select 'CREDIT' as TYPE, max(convert(date,Invoice_Entry_Date,103)) as Invoice_Entry_Date,Loc_Code ,VLC_Code ,Vendor_Code ,"
    '    sQuery += " max(DeductionCode)  as DeductionCode ,max(Deduction_Desc ) as Item_Desc,sum(Total_Amount ) as Amount from"
    '    sQuery += " ( select TSPL_VENDOR_INVOICE_HEAD.Loc_Code,TSPL_VENDOR_INVOICE_DETAIL.Document_No,Posting_Date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,"
    '    sQuery += " TSPL_VLC_MASTER_HEAD.VLC_Code, TSPL_VENDOR_INVOICE_HEAD.Vendor_Code, TSPL_VENDOR_INVOICE_HEAD.Vendor_Name, TSPL_VENDOR_INVOICE_DETAIL.DeductionCode,"
    '    sQuery += " TSPL_VENDOR_INVOICE_DETAIL.Deduction_Desc, TSPL_VENDOR_INVOICE_DETAIL.Total_Amount"
    '    sQuery += " from TSPL_VENDOR_INVOICE_DETAIL left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No =TSPL_VENDOR_INVOICE_DETAIL.Document_No  "
    '    sQuery += "  left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code =TSPL_VENDOR_INVOICE_HEAD.Vendor_Code  "
    '    sQuery += " where   TSPL_VENDOR_INVOICE_HEAD.Document_Type='C' and  TSPL_VENDOR_INVOICE_HEAD.Balance_Amt<>0 and refDocType not in ('Milk_HE','Milk_OW') and "
    '    sQuery += " coalesce(Posting_Date,'')<>''  and   ISNULL(TSPL_VENDOR_INVOICE_DETAIL.DeductionCode,'')<>'' and TSPL_VENDOR_INVOICE_HEAD.isDeduction='1' "
    '    sQuery += " and TSPL_VENDOR_INVOICE_HEAD.Vendor_Code in  (" + clsCommon.GetMulcallString(cbgVSP.CheckedValue) + ") and  "
    '    sQuery += " convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date ,103) between convert(date,'" + dtpFromDate.Value + "',103) and convert (date,'" + dtpToDate.Value + "',103) "
    '    sQuery += " and TSPL_VENDOR_INVOICE_HEAD.Loc_Code  IN (" + fndLoc.Value + ")  )xx group by Loc_Code ,VLC_Code ,Vendor_Code  ) as Deduction_CREDIT"
    '    sQuery += " on  Deduction_CREDIT.Vendor_Code =TSPL_MILK_PURCHASE_INVOICE_Head.vsp_code and Deduction_CREDIT .VLC_Code=TSPL_MILK_RECEIPT_DETAIL.VLC_Code " + Environment.NewLine
    '    '=====================================================================================


    '    sQuery += "  left join (select 'ISSUE Return' as TYPE, max(convert(date,[Item_Issue_Return_Date],103)) as Invoice_Entry_Date,From_Location ,VLC_Code ,Vendor_Code ,max(item_code) as item_code  ,max(item_desc )+' (Qty*Rate)' as Item_Desc,sum(Item_Net_Amt ) as Amount "
    '    sQuery += " from ("
    '    sQuery += "   select   TSPL_VSPItem_HEAD.From_Location,TSPL_VSPItem_HEAD.Doc_No  as [Item_Issue_Return_No],"
    '    sQuery += " TSPL_VSPItem_HEAD.Posting_Date,TSPL_VSPItem_HEAD.Doc_Date  as [Item_Issue_Return_Date],TSPL_VLC_MASTER_HEAD.VLC_Code "
    '    sQuery += " ,  TSPL_VSPItem_HEAD.Issue_To as [vendor_Code] ,TSPL_VENDOR_MASTER.Vendor_Name "
    '    sQuery += " , TSPL_VSPItem_DETAIL.Item_Code ,TSPL_ITEM_MASTER.Item_Desc ,TSPL_VSPItem_DETAIL.Item_Net_Amt "
    '    sQuery += "    from TSPL_VENDOR_INVOICE_HEAD   inner join TSPL_VSPItem_HEAD on TSPL_VSPItem_HEAD .Doc_No=TSPL_VENDOR_INVOICE_HEAD.Against_VSPItemIssue_No     left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_VSPItem_HEAD.Issue_To"
    '    sQuery += "  left join TSPL_VSPItem_DETAIL on TSPL_VSPItem_DETAIL.Doc_No =TSPL_VSPItem_HEAD.Doc_No "
    '    sQuery += " left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code =TSPL_VSPItem_DETAIL.Item_Code "
    '    sQuery += " left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code =TSPL_VENDOR_INVOICE_HEAD.Vendor_Code "
    '    sQuery += "   where TSPL_VSPItem_HEAD.Doc_Type='Return' "
    '    sQuery += "  and  TSPL_VENDOR_INVOICE_HEAD.Balance_Amt<>0 and  TSPL_VSPItem_HEAD.From_Location in  (  select location_code from TSPL_LOCATION_MASTER where Loc_Segment_Code   = " + fndLoc.Value + ")  "
    '    sQuery += "  and TSPL_VENDOR_INVOICE_HEAD.Vendor_Code in (" + clsCommon.GetMulcallString(cbgVSP.CheckedValue) + ") and  convert(date,TSPL_VSPItem_HEAD.Doc_Date ,103) between convert(date,'" + dtpFromDate.Value + "',103) and convert (date,'" + dtpToDate.Value + "',103) "
    '    sQuery += " ) as xx group by From_Location  ,VLC_Code ,Vendor_Code  ) Issue_Return on  Issue_Return.Vendor_Code =TSPL_MILK_PURCHASE_INVOICE_Head.vsp_code and Issue_Return .VLC_Code=TSPL_MILK_RECEIPT_DETAIL.VLC_Code   "
    '    '=================================================


    '    '================================================= END ALL TYPE DEDUCTION=================================================
    '    sQuery += " left join (select sum(total_Amt) as sale_Amt,customer_code from TSPL_SD_SALE_INVOICE_HEAD where Trans_Type='MCC' and document_date between '" & clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(dtpFromDate.Value), "dd/MMM/yyyy hh:mm tt") & "' and '" & clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(dtpToDate.Value), "dd/MMM/yyyy hh:mm tt") & "' group  by customer_code) mcc_mat on Mcc_Mat.customer_code=TSPL_MILK_PURCHASE_INVOICE_Head.vsp_code  " & whrcls & ")  xx   "
    '    sQuery += " ) as yy left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_name='" & CompName & "'"
    '    sQuery += "  order by convert(date,DOC_DATE,103)"
    '    Dim dtgv As New DataTable
    '    dtgv = clsDBFuncationality.GetDataTable(sQuery)


    '    'sQuery = " select Loc_Code as MCC_CODE ,VLC_Code  ,Vendor_Code ,DeductionCode ,max(Deduction_Desc) as Deduction_Desc ,sum(Total_Amount ) as Total_Amount from ( select TSPL_VENDOR_INVOICE_HEAD.Loc_Code,TSPL_VENDOR_INVOICE_DETAIL.Document_No,Posting_Date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,TSPL_VLC_MASTER_HEAD.VLC_Code  ,TSPL_VENDOR_INVOICE_HEAD.Vendor_Code,TSPL_VENDOR_INVOICE_HEAD.Vendor_Name,TSPL_VENDOR_INVOICE_DETAIL.DeductionCode,TSPL_VENDOR_INVOICE_DETAIL.Deduction_Desc,TSPL_VENDOR_INVOICE_DETAIL.Total_Amount    from TSPL_VENDOR_INVOICE_DETAIL"
    '    'sQuery += " left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No =TSPL_VENDOR_INVOICE_DETAIL.Document_No"
    '    'sQuery += "  left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code =TSPL_VENDOR_INVOICE_HEAD.Vendor_Code "
    '    'sQuery += "  where TSPL_VENDOR_INVOICE_HEAD.isDeduction='1' and Document_Type='D' and ISNULL(TSPL_VENDOR_INVOICE_DETAIL.DeductionCode,'')<>''  and TSPL_VENDOR_INVOICE_HEAD.Balance_Amt<>0  and TSPL_VENDOR_INVOICE_HEAD.Vendor_Code  in (" + clsCommon.GetMulcallString(cbgVSP.CheckedValue) + ")  and  coalesce(Posting_Date,'')<>''   and TSPL_VENDOR_INVOICE_HEAD.Loc_Code  IN (" + fndLoc.Value + ") "
    '    'sQuery += " and  convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103) between convert(date,'" + dtpFromDate.Value + "',103) and convert (date,'" + dtpToDate.Value + "',103)"
    '    'sQuery += ") xx group by Loc_Code ,VLC_Code ,Vendor_Code ,DeductionCode "
    '    'Dim dtgv1 As New DataTable
    '    'dtgv1 = clsDBFuncationality.GetDataTable(sQuery)

    '    sQuery = " select   final.Loc_Code ,final.VLC_Code ,final.Vendor_Code ,final.Item_Code  ,final.Item_Desc  ,final.Amount from ("
    '   '-- --===========================MCC Sale Return===============

    '    sQuery += " select 'MCC SALE RETURN' as Type,max(Shipment_Date )as Shipment_Date,Loc_Code ,VLC_Code ,Vendor_Code ,Item_Code ,max(Item_Desc) as Item_Desc,sum(Amount )  as Amount from ( select TSPL_SD_SHIPMENT_HEAD.Document_Code ,TSPL_Customer_Invoice_Head.Loc_Code, TSPL_SD_SHIPMENT_HEAD.Document_Date as [Shipment_Date],TSPL_VLC_MASTER_HEAD.VLC_Code  ,TSPL_CUSTOMER_VENDOR_MAPPING.vendor_code [Vendor_Code] , TSPL_VENDOR_MASTER.Vendor_Name as [Vendor_Name],TSPL_SD_SALE_RETURN_DETAIL.Item_Code,tspl_item_master.Item_Desc  ,TSPL_SD_SALE_RETURN_DETAIL.Amount    from TSPL_SD_SALE_RETURN_HEAD"
    '    sQuery += " left join TSPL_SD_SALE_RETURN_DETAIL on TSPL_SD_SALE_RETURN_DETAIL.DOCUMENT_CODE =TSPL_SD_SALE_RETURN_HEAD.Document_Code "
    '    sQuery += " left join tspl_item_master on tspl_item_master .Item_Code =TSPL_SD_SALE_RETURN_DETAIL.Item_Code "
    '    sQuery += " left join TSPL_SD_SHIPMENT_HEAD on  TSPL_SD_SHIPMENT_HEAD.Sale_Invoice_No=TSPL_SD_SALE_RETURN_HEAD.Against_Invoice_No "
    '    sQuery += " left outer join  TSPL_CUSTOMER_VENDOR_MAPPING on  TSPL_CUSTOMER_VENDOR_MAPPING.Cust_Code=TSPL_SD_SHIPMENT_HEAD.Customer_Code   "
    '    sQuery += " inner join TSPL_Customer_Invoice_Head on   TSPL_Customer_Invoice_Head.against_Mcc_Material_Sale_return=TSPL_SD_SALE_RETURN_HEAD.Document_Code  and coalesce(TSPL_Customer_Invoice_Head.against_Mcc_Material_Sale_return,'')<>''  "
    '    sQuery += " left outer join TSPL_VENDOR_MASTER   on  TSPL_VENDOR_MASTER .Vendor_code  =TSPL_CUSTOMER_VENDOR_MAPPING.vendor_code "
    '    sQuery += " left join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code  =TSPL_CUSTOMER_VENDOR_MAPPING.vendor_code"
    '    sQuery += " left outer join  TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code=TSPL_SD_SHIPMENT_HEAD.Sale_Invoice_No   where TSPL_SD_SHIPMENT_HEAD.Trans_Type='MCC'   and  tspl_customer_invoice_head.Balance_Amt<>0 "
    '    sQuery += " and TSPL_CUSTOMER_VENDOR_MAPPING.Vendor_Code  in (" + clsCommon.GetMulcallString(cbgVSP.CheckedValue) + ") and  convert(date,TSPL_SD_SHIPMENT_HEAD.Document_Date,103) between convert(date,'" + dtpFromDate.Value + "',103) and convert (date,'" + dtpToDate.Value + "',103) and  TSPL_Customer_Invoice_Head.Loc_Code IN (" + fndLoc.Value + ") "

    '    sQuery += "  )xx group by Loc_Code ,Vendor_Code ,VLC_Code ,Item_Code "

    '    '--'=================CREADIT===========
    '    sQuery += "   union all"
    '    sQuery += "  select 'CREDIT' as TYPE, max(convert(date,Invoice_Entry_Date,103)) as Invoice_Entry_Date,Loc_Code ,VLC_Code ,Vendor_Code ,DeductionCode  ,max(Deduction_Desc )+' (Qty*Rate)' as Item_Desc,sum(Total_Amount ) as Amount from ( select TSPL_VENDOR_INVOICE_HEAD.Loc_Code,TSPL_VENDOR_INVOICE_DETAIL.Document_No,Posting_Date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,TSPL_VLC_MASTER_HEAD.VLC_Code  ,TSPL_VENDOR_INVOICE_HEAD.Vendor_Code,TSPL_VENDOR_INVOICE_HEAD.Vendor_Name,TSPL_VENDOR_INVOICE_DETAIL.DeductionCode,TSPL_VENDOR_INVOICE_DETAIL.Deduction_Desc,TSPL_VENDOR_INVOICE_DETAIL.Total_Amount    from TSPL_VENDOR_INVOICE_DETAIL left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No =TSPL_VENDOR_INVOICE_DETAIL.Document_No  left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code =TSPL_VENDOR_INVOICE_HEAD.Vendor_Code "
    '    sQuery += "   where   TSPL_VENDOR_INVOICE_HEAD.Document_Type='C' and  TSPL_VENDOR_INVOICE_HEAD.Balance_Amt<>0 and refDocType not in ('Milk_HE','Milk_OW') and  coalesce(Posting_Date,'')<>''  and   ISNULL(TSPL_VENDOR_INVOICE_DETAIL.DeductionCode,'')<>'' and TSPL_VENDOR_INVOICE_HEAD.isDeduction='1'"
    '    sQuery += "  and TSPL_VENDOR_INVOICE_HEAD.Vendor_Code in (" + clsCommon.GetMulcallString(cbgVSP.CheckedValue) + ") and  convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date ,103) between convert(date,'" + dtpFromDate.Value + "',103) and convert (date,'" + dtpToDate.Value + "',103) and TSPL_VENDOR_INVOICE_HEAD.Loc_Code IN (" + fndLoc.Value + ") "
    '    sQuery += " )xx group by Loc_Code ,VLC_Code ,Vendor_Code ,DeductionCode  "

    '    '======================Issue Return=====================
    '    sQuery += "   union all"

    '    sQuery += " select 'ISSUE Return' as TYPE, max(convert(date,[Item_Issue_Return_Date],103)) as Invoice_Entry_Date,From_Location ,VLC_Code ,Vendor_Code ,item_code  ,max(item_desc )+' (Qty*Rate)' as Item_Desc,sum(Item_Net_Amt ) as Amount "
    '    sQuery += " from ("
    '    sQuery += "   select   TSPL_VSPItem_HEAD.From_Location,TSPL_VSPItem_HEAD.Doc_No  as [Item_Issue_Return_No],"
    '    sQuery += " TSPL_VSPItem_HEAD.Posting_Date,TSPL_VSPItem_HEAD.Doc_Date  as [Item_Issue_Return_Date],TSPL_VLC_MASTER_HEAD.VLC_Code "
    '    sQuery += " ,  TSPL_VSPItem_HEAD.Issue_To as [vendor_Code] ,TSPL_VENDOR_MASTER.Vendor_Name "
    '    sQuery += " , TSPL_VSPItem_DETAIL.Item_Code ,TSPL_ITEM_MASTER.Item_Desc ,TSPL_VSPItem_DETAIL.Item_Net_Amt "
    '    sQuery += "    from TSPL_VENDOR_INVOICE_HEAD   inner join TSPL_VSPItem_HEAD on TSPL_VSPItem_HEAD .Doc_No=TSPL_VENDOR_INVOICE_HEAD.Against_VSPItemIssue_No     left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_VSPItem_HEAD.Issue_To"
    '    sQuery += "  left join TSPL_VSPItem_DETAIL on TSPL_VSPItem_DETAIL.Doc_No =TSPL_VSPItem_HEAD.Doc_No "
    '    sQuery += " left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code =TSPL_VSPItem_DETAIL.Item_Code "
    '    sQuery += " left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code =TSPL_VENDOR_INVOICE_HEAD.Vendor_Code "
    '    sQuery += "   where TSPL_VSPItem_HEAD.Doc_Type='Return' "
    '    sQuery += "  and  TSPL_VENDOR_INVOICE_HEAD.Balance_Amt<>0 and  TSPL_VSPItem_HEAD.From_Location in  (  select location_code from TSPL_LOCATION_MASTER where Loc_Segment_Code   = " + fndLoc.Value + ")  "
    '    sQuery += "  and TSPL_VENDOR_INVOICE_HEAD.Vendor_Code in (" + clsCommon.GetMulcallString(cbgVSP.CheckedValue) + ") and  convert(date,TSPL_VSPItem_HEAD.Doc_Date ,103) between convert(date,'" + dtpFromDate.Value + "',103) and convert (date,'" + dtpToDate.Value + "',103) "
    '    sQuery += " ) as xx group by From_Location  ,VLC_Code ,Vendor_Code ,Item_Code   "
    '    '=================================================
    '    '--======================MCC Sale
    '    sQuery += "   union all"
    '    sQuery += " select 'MCC SALE' as Type,  max([Shipment_Date]) as [Shipment_Date],Loc_Code ,VLC_Code ,Vendor_Code ,Item_Code ,max(Item_Desc ) as Item_Desc,sum(Amount )*(-1) as Amount from(select TSPL_SD_SHIPMENT_HEAD.Document_Code , TSPL_SD_SHIPMENT_HEAD.Document_Date as [Shipment_Date],TSPL_Customer_Invoice_Head.Loc_Code,TSPL_VLC_MASTER_HEAD.VLC_Code  ,TSPL_CUSTOMER_VENDOR_MAPPING.vendor_code [Vendor_Code] ,TSPL_VENDOR_MASTER.Vendor_Name as [Vendor_Name] ,TSPL_SD_SHIPMENT_DETAIL.Item_Code,TSPL_ITEM_MASTER.Item_Desc  , TSPL_SD_SHIPMENT_DETAIL.Amount    from TSPL_SD_SHIPMENT_HEAD "
    '    sQuery += " left join TSPL_SD_SHIPMENT_DETAIL on TSPL_SD_SHIPMENT_HEAD.Document_Code =TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE "
    '    sQuery += " left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code =TSPL_SD_SHIPMENT_DETAIL.Item_Code "
    '    sQuery += " left outer join  TSPL_CUSTOMER_VENDOR_MAPPING on TSPL_CUSTOMER_VENDOR_MAPPING.Cust_Code=TSPL_SD_SHIPMENT_HEAD.Customer_Code  "
    '    sQuery += " inner join TSPL_Customer_Invoice_Head on  TSPL_Customer_Invoice_Head.Against_Sale_No=TSPL_SD_SHIPMENT_HEAD.Sale_Invoice_No  and coalesce(TSPL_Customer_Invoice_Head.Against_Sale_No,'')<>'' "
    '    sQuery += " left outer join TSPL_VENDOR_MASTER   on  TSPL_VENDOR_MASTER .Vendor_code  =TSPL_CUSTOMER_VENDOR_MAPPING.vendor_code  "
    '    sQuery += " left join  TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code =TSPL_CUSTOMER_VENDOR_MAPPING.vendor_code "
    '    sQuery += " left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code=TSPL_SD_SHIPMENT_HEAD.Sale_Invoice_No   where TSPL_SD_SHIPMENT_HEAD.Trans_Type='MCC'  and tspl_customer_invoice_head.Balance_Amt<>0  "
    '    sQuery += "  and TSPL_CUSTOMER_VENDOR_MAPPING.Vendor_Code in (" + clsCommon.GetMulcallString(cbgVSP.CheckedValue) + ") and  convert(date,TSPL_SD_SHIPMENT_HEAD.Document_Date,103) between convert(date,'" + dtpFromDate.Value + "',103) and convert (date,'" + dtpToDate.Value + "',103) and TSPL_Customer_Invoice_Head.Loc_Code IN (" + fndLoc.Value + ") "

    '    sQuery += ") xx group by Loc_Code ,Vendor_Code,VLC_Code  ,Item_Code "

    '    '--=============Deduction=================
    '    sQuery += "   union all"
    '    sQuery += " select 'DEDUCTION' as Type,  max(convert(datetime,Invoice_Entry_Date,103)) as Invoice_Entry_Date,Loc_Code as MCC_CODE ,VLC_Code  ,Vendor_Code ,DeductionCode ,max(Deduction_Desc) as Deduction_Desc ,sum(Total_Amount )*(-1) as Total_Amount from ( select TSPL_VENDOR_INVOICE_HEAD.Loc_Code,TSPL_VENDOR_INVOICE_DETAIL.Document_No,Posting_Date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,TSPL_VLC_MASTER_HEAD.VLC_Code  ,TSPL_VENDOR_INVOICE_HEAD.Vendor_Code,TSPL_VENDOR_INVOICE_HEAD.Vendor_Name,TSPL_VENDOR_INVOICE_DETAIL.DeductionCode,TSPL_VENDOR_INVOICE_DETAIL.Deduction_Desc,TSPL_VENDOR_INVOICE_DETAIL.Total_Amount    from TSPL_VENDOR_INVOICE_DETAIL left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No =TSPL_VENDOR_INVOICE_DETAIL.Document_No  left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code =TSPL_VENDOR_INVOICE_HEAD.Vendor_Code   where TSPL_VENDOR_INVOICE_HEAD.isDeduction='1' and Document_Type='D' and ISNULL(TSPL_VENDOR_INVOICE_DETAIL.DeductionCode,'')<>''  and TSPL_VENDOR_INVOICE_HEAD.Balance_Amt<>0   and  coalesce(Posting_Date,'')<>'' "
    '    sQuery += " and TSPL_VENDOR_INVOICE_HEAD.Vendor_Code in (" + clsCommon.GetMulcallString(cbgVSP.CheckedValue) + ") and  convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103) between convert(date,'" + dtpFromDate.Value + "',103) and convert (date,'" + dtpToDate.Value + "',103) and TSPL_VENDOR_INVOICE_HEAD.Loc_Code IN (" + fndLoc.Value + ") "
    '    sQuery += " ) xx group by Loc_Code ,VLC_Code ,Vendor_Code ,DeductionCode "



    '    '-- --===========================ITEM ISSUE===============
    '    sQuery += "   union all"
    '    sQuery += " select 'ITEM ISSUE' as TYPE,max(Item_Issue_Doc_Date) as Item_Issue_Doc_Date,From_Location ,VLC_Code ,vendor_Code ,Item_Code ,max(Item_Desc) as Item_Desc,sum(Amount )*(-1) as Amount  from (select TSPL_VSPItem_HEAD.Doc_Date  as [Item_Issue_Doc_Date] ,TSPL_VSPItem_HEAD.From_Location,VLC_Code  ,TSPL_VSPItem_HEAD.Issue_To as [vendor_Code] ,TSPL_VENDOR_MASTER.Vendor_Name,TSPL_VSPItem_DETAIL.Item_Code ,tspl_item_master.Item_Desc ,TSPL_VSPItem_DETAIL.Amount    from TSPL_VENDOR_INVOICE_HEAD "

    '    sQuery += "  inner join TSPL_VSPItem_HEAD on TSPL_VSPItem_HEAD .Doc_No=TSPL_VENDOR_INVOICE_HEAD.Against_VSPItemIssue_No  "
    '    sQuery += "   left join TSPL_VSPItem_DETAIL on TSPL_VSPItem_DETAIL.Doc_No =TSPL_VSPItem_HEAD.Doc_No  "
    '    sQuery += "	left join tspl_item_master on TSPL_ITEM_MASTER.Item_Code = TSPL_VSPItem_DETAIL.Item_Code "
    '    sQuery += " left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_VSPItem_HEAD.Issue_To"
    '    sQuery += " left join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code =TSPL_VSPItem_HEAD.Issue_To"
    '    sQuery += "   where TSPL_VENDOR_INVOICE_HEAD.Balance_Amt<>0  and TSPL_VSPItem_HEAD.From_Location in  (  select location_code from TSPL_LOCATION_MASTER where Loc_Segment_Code    IN (" + fndLoc.Value + ") )"
    '    sQuery += "   and TSPL_VSPItem_HEAD.Issue_To in (" + clsCommon.GetMulcallString(cbgVSP.CheckedValue) + ") and  convert(date,TSPL_VSPItem_HEAD.Doc_Date ,103) between convert(date,'" + dtpFromDate.Value + "',103) and convert (date,'" + dtpToDate.Value + "',103)"
    '    sQuery += "  ) xx group by"
    '    sQuery += "   From_Location, VLC_Code, vendor_Code, Item_Code   ) as final "



    '    Dim dtgv1 As New DataTable
    '    dtgv1 = clsDBFuncationality.GetDataTable(sQuery)

    '    If dtgv IsNot Nothing And dtgv.Rows.Count > 0 Then
    '        gv.DataSource = Nothing
    '        gv.Rows.Clear()
    '        gv.Columns.Clear()
    '        gv.DataSource = dtgv
    '        gv.GroupDescriptors.Clear()
    '        gv.MasterTemplate.SummaryRowsBottom.Clear()
    '        FormatGrid()
    '        If btnReferesh = False Then

    '            frmCrystalReportViewer.funsubreportWithdt(CrystalReportFolder.MilkProcurement, dtgv, dtgv1, "crptMilkPurchaseBill", "SubMilkPurchaseBill.rpt", "SubMilkPurchaseBill.rpt", "Address.rpt")

    '            'MilkProcurementReportViewer.funreport(dtgv, "crptMilkPurchaseBill", "Milk Purchase Bill")SubMilkPurchaseBill
    '            'frmCrystalReportViewer.funsubreportWithdt(CrystalReportFolder.MilkProcurement, dtgv, MccAddresShowinHeader(), "crptMilkPurchaseBill", "MCC Purchase Bill", "Address.rpt")
    '            ' frmCrystalReportViewer.funsubreportWithdt(CrystalReportFolder.MilkProcurement, dtgv1, MccAddresShowinHeader(), "SubMilkPurchaseBill", "MCC Purchase Bill", "")
    '        End If
    '        RadPageView1.SelectedPage = RadPageViewPage2
    '    Else
    '        clsCommon.MyMessageBoxShow("No Data Found")
    '    End If
    '    ReStoreGridLayout()
    'End Sub

    Public Function MccAddresShowinHeader() As DataTable
        Dim strmcc As List(Of String) = Nothing
        ' If cbtMCCRouteVLCC.CheckedValue(1).Count = 1 Then
        'If cbgVSP.CheckedValue(1).Count = 1 Then
        '    strmcc = cbgVSP.CheckedValue(1)
        'Else

        'End If
        If IsNothing(strmcc) Then
            Return clsDBFuncationality.GetDataTable("select Comp_Name ,TSPL_COMPANY_MASTER.Logo_Img ,TSPL_COMPANY_MASTER.Logo_Img2  ,tspl_mcc_master.add1 +case when len(tspl_mcc_master.add2)>0 then ', '+tspl_mcc_master.add2 else '' end  as Loc_Add from TSPL_COMPANY_MASTER  left join tspl_mcc_master on mcc_code=''")
        Else
            Return clsDBFuncationality.GetDataTable("select Comp_Name ,TSPL_COMPANY_MASTER.Logo_Img ,TSPL_COMPANY_MASTER.Logo_Img2  ,tspl_mcc_master.add1 +case when len(tspl_mcc_master.add2)>0 then ', '+tspl_mcc_master.add2 else '' end  as Loc_Add from TSPL_COMPANY_MASTER  left join tspl_mcc_master on mcc_code='" & strmcc(0) & "'")
        End If
    End Function

    Sub FormatGrid()
        gv.TableElement.TableHeaderHeight = 20
        gv.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gv.Columns.Count - 1
            gv.Columns(ii).ReadOnly = True
            gv.Columns(ii).IsVisible = False
        Next

        gv.Columns("DOC_DATE").IsVisible = True
        gv.Columns("DOC_DATE").Width = 100
        gv.Columns("DOC_DATE").HeaderText = " Date"
        gv.Columns("DOC_DATE").FormatString = "{0:d}"

        gv.Columns("SHIFT").IsVisible = True
        gv.Columns("SHIFT").Width = 30
        gv.Columns("SHIFT").HeaderText = " Shift"

        gv.Columns("TYPE").IsVisible = True
        gv.Columns("TYPE").Width = 100
        gv.Columns("TYPE").HeaderText = " TYPE"

        gv.Columns("CLR").IsVisible = True
        gv.Columns("CLR").Width = 100
        gv.Columns("CLR").HeaderText = " CLR"

        gv.Columns("SAMPLE_NO").IsVisible = True
        gv.Columns("SAMPLE_NO").Width = 100
        gv.Columns("SAMPLE_NO").HeaderText = " SAMPLE_NO"



        gv.Columns("Qty").IsVisible = True
        gv.Columns("Qty").Width = 100
        gv.Columns("Qty").HeaderText = " Quantity"

        gv.Columns("FAT_Per").IsVisible = True
        gv.Columns("FAT_Per").Width = 100
        gv.Columns("FAT_Per").HeaderText = " Fat %"

        gv.Columns("SNF_Per").IsVisible = True
        gv.Columns("SNF_Per").Width = 100
        gv.Columns("SNF_Per").HeaderText = "Snf %"

        'gv.Columns("TFAT").IsVisible = True
        'gv.Columns("TFAT").Width = 100
        'gv.Columns("TFAT").HeaderText = "TFAT"

        'gv.Columns("TSNF").IsVisible = True
        'gv.Columns("TSNF").Width = 100
        'gv.Columns("TSNF").HeaderText = "TSNF"

        gv.Columns("Rate").IsVisible = True
        gv.Columns("Rate").Width = 100
        gv.Columns("Rate").HeaderText = "Rate"

        gv.Columns("Net_AMOUNT").IsVisible = True
        gv.Columns("Net_AMOUNT").Width = 100
        gv.Columns("Net_AMOUNT").HeaderText = "Amount"

        gv.Columns("MCC_CODE").IsVisible = False
        gv.Columns("MCC_CODE").Width = 100
        gv.Columns("MCC_CODE").HeaderText = "MCC Code"

        'gv.Columns("MCC_NAME").IsVisible = False
        'gv.Columns("MCC_NAME").Width = 100
        'gv.Columns("MCC_NAME").HeaderText = "MCC Name"

        'gv.Columns("VSP_CODE").IsVisible = True
        'gv.Columns("VSP_CODE").Width = 100
        'gv.Columns("VSP_CODE").HeaderText = "VSP Code"

        gv.Columns("VLC_Code_VLC_Uploader").IsVisible = True
        gv.Columns("VLC_Code_VLC_Uploader").Width = 100
        gv.Columns("VLC_Code_VLC_Uploader").HeaderText = "VLC Code"

        'gv.Columns("Vendor_Name").IsVisible = True
        'gv.Columns("Vendor_Name").Width = 100
        'gv.Columns("Vendor_Name").HeaderText = "Vendor Name"


        'gv.Columns("ROUTE_CODE").IsVisible = False
        'gv.Columns("ROUTE_CODE").Width = 100
        'gv.Columns("ROUTE_CODE").HeaderText = "Route Code"

        'gv.Columns("Route_Name").IsVisible = False
        'gv.Columns("Route_Name").Width = 100
        'gv.Columns("Route_Name").HeaderText = "Route Name"


        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim intCount As Integer = 0

        Dim item1 As New GridViewSummaryItem("Qty", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)
        'Dim item2 As New GridViewSummaryItem("FAT_Per", "{0:F2}", GridAggregateFunction.Avg)
        'summaryRowItem.Add(item2)
        'Dim item3 As New GridViewSummaryItem("SNF_Per", "{0:F2}", GridAggregateFunction.Avg)
        'summaryRowItem.Add(item3)

        'Dim item7 As New GridViewSummaryItem("Amount", "{0:F2}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(item7)
        gv.GroupDescriptors.Add(New GridGroupByExpression("MCC_CODE as Item format ""{0}: {1}"" Group By MCC_CODE"))
        gv.GroupDescriptors.Add(New GridGroupByExpression("VLC_Code_VLC_Uploader as Item format ""{0}: {1}"" Group By VLC_Code_VLC_Uploader"))
        gv.GroupDescriptors.Add(New GridGroupByExpression("VSP_CODE as Item format ""{0}: {1}"" Group By VSP_CODE"))

        gv.ShowGroupPanel = False
        gv.MasterTemplate.AutoExpandGroups = True

        gv.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

    End Sub

    Sub Reset()
        isLoad = True
        'dtpFromDate.Value = clsCommon.GETSERVERDATE()
        'dtpToDate.Value = clsCommon.GETSERVERDATE()
        txtMonth.Value = clsCommon.GETSERVERDATE()
        loadVSP()
        chkVSPAll.CheckState = CheckState.Checked
        gv.DataSource = Nothing
        RadPageView1.SelectedPage = RadPageViewPage1
        isLoad = False
    End Sub

    Private Sub RptMilkPurchaseBill_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.R Then
            Load_ReportForPaymentProcess()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            Reset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P Then
            Load_ReportForPaymentProcess()
        End If
    End Sub
    Sub print(ByVal exporter As EnumExportTo)
        Try
            If gv.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(dtpFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(dtpToDate.Value, "dd/MM/yyyy")) + " ")
                'If rbtnMCCRouteVLCCSelect.IsChecked Then
                'Dim arr As List(Of String)
                arrHeader.Add("Location : " & fndLoc.Value)

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
                    clsCommon.MyExportToExcelGrid("Milk Purchase Bill", gv, arrHeader, Me.Text)
                Else
                    clsCommon.MyExportToPDF("Milk Purchase Bill", gv, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
                End If
            Else
                common.clsCommon.MyMessageBoxShow("No Data Found to Export.", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        ' Ticket : ERO/18/09/18-000399 work on print 
        Dim Location As String = fndLoc.Value
        Dim FrmDate As Date = dtpFromDate.Value
        Dim ToDate As Date = dtpToDate.Value
        Dim arrVSP As List(Of String) = New List(Of String)()
        Dim objpaymentProcess As New FrmPaymentProcess
        DynamicDt = Nothing
        DynamicDt = objpaymentProcess.Load_Report(Location, FrmDate, ToDate, cbgVSP.CheckedValue, True, True)

        'gv.DataSource = Nothing
        'gv.DataSource = DynamicDt
    End Sub




    Private Sub rbtnMCCRouteVLCCAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs)
        'cbtMCCRouteVLCC.Enabled = rbtnMCCRouteVLCCSelect.IsChecked
    End Sub
    Sub loadVendor(Optional ByVal LocSeg As String = "")
        Dim whrCls As String = ""
        If clsCommon.myLen(LocSeg) > 0 Then
            whrCls = " and vendor_code in ( select distinct VSP_Code from TSPL_VLC_MASTER_HEAD where MCC=(select Location_Code  from TSPL_LOCATION_MASTER where Loc_Segment_Code='" & LocSeg & "' and Rejected_Type='N' and Location_Category='MCC')) "
        End If
        Dim qry As String = "select Vendor_Code,Vendor_Name from TSPL_VENDOR_MASTER  where Form_Type='VSP' " & whrCls & " order by Vendor_Code"
        cbgVSP.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgVSP.ValueMember = "Vendor_Code"
        cbgVSP.DisplayMember = "Vendor_Name"
        chkVSPAll.IsChecked = True
        cbgVSP.Enabled = False
        cbgVSP.CheckedAll()
    End Sub
    Private Sub fndLoc__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndLoc._MYValidating
        Dim whrCls As String = " 1=1 "
        If Not clsMccMaster.isCurrentUserHO() Then
            If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                whrCls = "  Location_Code in (" & objCommonVar.strCurrUserLocations & ") "
            End If
        End If
        whrCls = whrCls & "  and  Rejected_Type='N' and Location_Category='MCC'"
        fndLoc.Value = clsLocation.getLocSegFinder(whrCls, fndLoc.Value, isButtonClicked)
        txtLocName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select Description  from TSPL_GL_SEGMENT_CODE WHERE  Segment_code='" & fndLoc.Value & "' "))
        Try
            If clsCommon.myLen(fndLoc.Value) > 0 Then
                ' fndLoc.Enabled = False
                ' txtLocName.Enabled = False
                loadVSP(fndLoc.Value)
                If Not isLoad Then
                    Dim PaymentCycleType As String = ""
                    Dim PaymentCycleValue As Integer = 0
                    ' If Not isLoad Then
                    If clsCommon.myLen(fndLoc.Value) <= 0 Then
                        clsCommon.MyMessageBoxShow("Please select the Location first", Me.Text)
                        Exit Sub
                    End If
                    Dim dt As DataTable = clsDBFuncationality.GetDataTable(" select isnull(TSPL_MCC_MASTER.empOnAmountOnly,0) as empOnAmountOnly,TSPL_MCC_MASTER.Payment_Cycle,TSPL_PAYMENT_CYCLE_MASTER.PC_TYPE,TSPL_PAYMENT_CYCLE_MASTER.PC_VALUE  from TSPL_MCC_MASTER left outer join TSPL_PAYMENT_CYCLE_MASTER on TSPL_PAYMENT_CYCLE_MASTER.PC_CODE=TSPL_MCC_MASTER.Payment_Cycle   where TSPL_MCC_MASTER.MCC_Code =(select Location_Code  from TSPL_LOCATION_MASTER where Loc_Segment_Code='" & fndLoc.Value & "' and Location_Category='MCC' and Rejected_Type='N') ")
                    If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                        clsCommon.MyMessageBoxShow("No Payment Cycle found on current MCC/Location", Me.Text)
                        Exit Sub
                    End If
                    PaymentCycleType = clsCommon.myCstr(dt.Rows(0)("PC_TYPE"))
                    PaymentCycleValue = clsCommon.myCdbl(dt.Rows(0)("PC_VALUE"))
                    isEmpOnAmtOnly = IIf(clsCommon.myCdbl(dt.Rows(0)("empOnAmountOnly")) = 0, False, True)
                    If clsCommon.CompairString(PaymentCycleType, "Day") = CompairStringResult.Equal Then

                        If clsCommon.myCdbl(clsCommon.GetPrintDate(dtpFromDate.Value, "dd")) Mod PaymentCycleValue <> 1 And (Not PaymentCycleValue = 1) Then
                            clsCommon.MyMessageBoxShow("Date can only be first day of month or at interval of " & PaymentCycleValue & " Day, Because MCC has payment Cycle of " & PaymentCycleValue & " Day ")
                            dtpFromDate.Value = "01/" & DatePart(DateInterval.Month, clsCommon.GETSERVERDATE()) & "/" & DatePart(DateInterval.Year, clsCommon.GETSERVERDATE())
                            dtpToDate.Value = "01/" & DatePart(DateInterval.Month, clsCommon.GETSERVERDATE()) & "/" & DatePart(DateInterval.Year, clsCommon.GETSERVERDATE())
                            Exit Sub
                        End If
                        dtpToDate.Value = DateAdd(DateInterval.Day, PaymentCycleValue - 1, dtpFromDate.Value)
                        If DatePart(DateInterval.Month, dtpFromDate.Value) <> DatePart(DateInterval.Month, dtpToDate.Value) Then
                            dtpToDate.Value = DateAdd(DateInterval.Month, 1, clsCommon.myCDate("01/" & DatePart(DateInterval.Month, dtpFromDate.Value) & "/" & DatePart(DateInterval.Year, dtpFromDate.Value)))
                            dtpToDate.Value = DateAdd(DateInterval.Day, -1, dtpToDate.Value)

                        End If
                    ElseIf clsCommon.CompairString(PaymentCycleType, "Month") = CompairStringResult.Equal Then
                        If clsCommon.myCdbl(clsCommon.GetPrintDate(dtpFromDate.Value, "dd")) <> 1 Then
                            clsCommon.MyMessageBoxShow("Date can only be first day of month, Because MCC has payment Cycle of Month Type", Me.Text)
                            dtpFromDate.Value = "01/" & DatePart(DateInterval.Month, clsCommon.GETSERVERDATE()) & "/" & DatePart(DateInterval.Year, clsCommon.GETSERVERDATE())
                            dtpToDate.Value = "01/" & DatePart(DateInterval.Month, clsCommon.GETSERVERDATE()) & "/" & DatePart(DateInterval.Year, clsCommon.GETSERVERDATE())
                            Exit Sub
                        End If
                        dtpToDate.Value = DateAdd(DateInterval.Month, PaymentCycleValue, dtpFromDate.Value)
                    ElseIf clsCommon.CompairString(PaymentCycleType, "Year") = CompairStringResult.Equal Then
                        If clsCommon.myCdbl(clsCommon.GetPrintDate(dtpFromDate.Value, "dd")) <> 1 Then
                            clsCommon.MyMessageBoxShow("Date can only be first day of month, Because MCC has payment Cycle of Year Type", Me.Text)
                            dtpFromDate.Value = "01/" & DatePart(DateInterval.Month, clsCommon.GETSERVERDATE()) & "/" & DatePart(DateInterval.Year, clsCommon.GETSERVERDATE())
                            dtpToDate.Value = "01/" & DatePart(DateInterval.Month, clsCommon.GETSERVERDATE()) & "/" & DatePart(DateInterval.Year, clsCommon.GETSERVERDATE())
                            Exit Sub
                        End If
                        dtpToDate.Value = DateAdd(DateInterval.Year, PaymentCycleValue, dtpFromDate.Value)
                    End If
                    ' End If
                End If
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message)
        End Try

    End Sub


    Private Sub dtpFromDate_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles dtpFromDate.Validating
        If Not isLoad Then
            Dim PaymentCycleType As String = ""
            Dim PaymentCycleValue As Integer = 0
            ' If Not isLoad Then
            If clsCommon.myLen(fndLoc.Value) <= 0 Then
                clsCommon.MyMessageBoxShow("Please select the Location first", Me.Text)
                Exit Sub
            End If
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(" select TSPL_MCC_MASTER.Payment_Cycle,TSPL_PAYMENT_CYCLE_MASTER.PC_TYPE,TSPL_PAYMENT_CYCLE_MASTER.PC_VALUE  from TSPL_MCC_MASTER left outer join TSPL_PAYMENT_CYCLE_MASTER on TSPL_PAYMENT_CYCLE_MASTER.PC_CODE=TSPL_MCC_MASTER.Payment_Cycle   where TSPL_MCC_MASTER.MCC_Code =(select Location_Code  from TSPL_LOCATION_MASTER where Loc_Segment_Code='" & fndLoc.Value & "' and Location_Category='MCC' and Rejected_Type='N') ")
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow("No Payment Cycle found on current MCC/Location", Me.Text)
                Exit Sub
            End If
            PaymentCycleType = clsCommon.myCstr(dt.Rows(0)("PC_TYPE"))
            PaymentCycleValue = clsCommon.myCdbl(dt.Rows(0)("PC_VALUE"))
            If clsCommon.CompairString(PaymentCycleType, "Day") = CompairStringResult.Equal Then

                If clsCommon.myCdbl(clsCommon.GetPrintDate(dtpFromDate.Value, "dd")) Mod PaymentCycleValue <> 1 And (Not PaymentCycleValue = 1) Then
                    clsCommon.MyMessageBoxShow("Date can only be first day of month or at interval of " & PaymentCycleValue & " Day, Because MCC has payment Cycle of " & PaymentCycleValue & " Day ")
                    dtpFromDate.Value = "01/" & DatePart(DateInterval.Month, clsCommon.GETSERVERDATE()) & "/" & DatePart(DateInterval.Year, clsCommon.GETSERVERDATE())
                    dtpToDate.Value = "01/" & DatePart(DateInterval.Month, clsCommon.GETSERVERDATE()) & "/" & DatePart(DateInterval.Year, clsCommon.GETSERVERDATE())
                    Exit Sub
                End If
                dtpToDate.Value = DateAdd(DateInterval.Day, PaymentCycleValue - 1, dtpFromDate.Value)
                If DatePart(DateInterval.Month, dtpFromDate.Value) <> DatePart(DateInterval.Month, dtpToDate.Value) Then
                    dtpToDate.Value = DateAdd(DateInterval.Month, 1, clsCommon.myCDate("01/" & DatePart(DateInterval.Month, dtpFromDate.Value) & "/" & DatePart(DateInterval.Year, dtpFromDate.Value)))
                    dtpToDate.Value = DateAdd(DateInterval.Day, -1, dtpToDate.Value)

                End If
                If clsCommon.myCdbl(clsCommon.GetPrintDate(dtpToDate.Value, "dd")) = 30 Then
                    dtpToDate.Value = DateAdd(DateInterval.Month, 1, clsCommon.myCDate("01/" & DatePart(DateInterval.Month, dtpFromDate.Value) & "/" & DatePart(DateInterval.Year, dtpFromDate.Value)))
                    dtpToDate.Value = DateAdd(DateInterval.Day, -1, dtpToDate.Value)

                End If
            ElseIf clsCommon.CompairString(PaymentCycleType, "Month") = CompairStringResult.Equal Then
                If clsCommon.myCdbl(clsCommon.GetPrintDate(dtpFromDate.Value, "dd")) <> 1 Then
                    clsCommon.MyMessageBoxShow("Date can only be first day of month, Because MCC has payment Cycle of Month Type", Me.Text)
                    dtpFromDate.Value = "01/" & DatePart(DateInterval.Month, clsCommon.GETSERVERDATE()) & "/" & DatePart(DateInterval.Year, clsCommon.GETSERVERDATE())
                    dtpToDate.Value = "01/" & DatePart(DateInterval.Month, clsCommon.GETSERVERDATE()) & "/" & DatePart(DateInterval.Year, clsCommon.GETSERVERDATE())
                    Exit Sub
                End If
                dtpToDate.Value = DateAdd(DateInterval.Month, PaymentCycleValue, dtpFromDate.Value)
            ElseIf clsCommon.CompairString(PaymentCycleType, "Year") = CompairStringResult.Equal Then
                If clsCommon.myCdbl(clsCommon.GetPrintDate(dtpFromDate.Value, "dd")) <> 1 Then
                    clsCommon.MyMessageBoxShow("Date can only be first day of month, Because MCC has payment Cycle of Year Type", Me.Text)
                    dtpFromDate.Value = "01/" & DatePart(DateInterval.Month, clsCommon.GETSERVERDATE()) & "/" & DatePart(DateInterval.Year, clsCommon.GETSERVERDATE())
                    dtpToDate.Value = "01/" & DatePart(DateInterval.Month, clsCommon.GETSERVERDATE()) & "/" & DatePart(DateInterval.Year, clsCommon.GETSERVERDATE())
                    Exit Sub
                End If
                dtpToDate.Value = DateAdd(DateInterval.Year, PaymentCycleValue, dtpFromDate.Value)
            End If
            ' End If
        End If

    End Sub
    Public Sub Load_ReportForPaymentProcess()
        '======================preeti gupta ticket no []
        Dim sQuery As String
        Dim dtgv As New DataTable
        Dim companyADD, CompName, CompCode As String

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

        Dim fromDate As String = dtpFromDate.Value
        Dim Todate As String = dtpToDate.Value


        Dim whrcls As String = " where 2=2 "
        whrcls += "  and convert(date,TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_DATE,103)>=convert(date,('" + dtpFromDate.Value + "'),103) and convert(date,TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_DATE,103) <=convert(date,('" + dtpToDate.Value + "'),103) "
        If cbgVSP.CheckedValue.Count > 0 Then ''chkVSPSelect.IsChecked AndAlso
            whrcls += " and TSPL_MILK_PURCHASE_INVOICE_HEAD.VSP_CODE  in (" + clsCommon.GetMulcallString(cbgVSP.CheckedValue) + ")"
        End If
        If clsCommon.myLen(fndLoc.Value) > 0 Then
            whrcls += " and TSPL_LOCATION_MASTER.Loc_Segment_Code     IN (" + fndLoc.Value + ") "
        End If

        Dim whrcls1 As String = " where 2=2 "
        whrcls1 += "  and convert(date,TSPL_PAYMENT_PROCESS_HEAD.From_Date,103)>=convert(date,('" + dtpFromDate.Value + "'),103) and convert(date,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103) <=convert(date,('" + dtpToDate.Value + "'),103) "
        If cbgVSP.CheckedValue.Count > 0 Then ''chkVSPSelect.IsChecked AndAlso
            whrcls1 += " and TSPL_PAYMENT_PROCESS_DETAIL.VSP_CODE  in (" + clsCommon.GetMulcallString(cbgVSP.CheckedValue) + ")"
        End If
        If clsCommon.myLen(fndLoc.Value) > 0 Then
            whrcls1 += " and TSPL_PAYMENT_PROCESS_Head.loc_seg_code    IN (" + fndLoc.Value + ") "
        End If

        sQuery = ""

        'sQuery += " select  '" & fromDate & "'  as fromDate ,'" & Todate & "'  as Todate ,''  as companyADD, '" & CompName & "'  as CompName,'" & CompCode & "'  as CompCode,TSPL_COMPANY_MASTER .Logo_Img   as compLogo1 ,TSPL_COMPANY_MASTER .Logo_Img2 as compLogo2,convert(varchar,DOC_DATE,103) as DOC_DATE,SHIFT,TYPE,SAMPLE_NO,convert(decimal(18,2),NewQty)as NewQty ,convert(decimal(18,1),FAT_PER) as FAT_PER ,convert(decimal(18,1),SNF_PER) as SNF_PER  ,convert(decimal(18,1),CLR)as CLR ,convert(decimal(18,2),(FAT_PER * NewQty)/100 )as TFAT,convert(decimal(18,2),(SNF_PER * NewQty)/100) as TSNF,convert(decimal(18,2),Amount/NewQty) as RATE  ,convert(decimal(18,2),Amount)as Amount ,MCC_CODE,VLC_Code ,VLC_Code_VLC_Uploader,emp,incentive,HEDAmt,AstAMT,DedAmt,coalesce(sale_AMt,0) as sale_AMt,VSP_CODE, ISNULL(Deduction_Debit_Amount,0) as Deduction_Debit_Amount, ISNULL(Deduction_MCC_Sale_Amount,0) as Deduction_MCC_Sale_Amount, ISNULL(Deduction_MCC_Sale_Return_Amount,0) as Deduction_MCC_Sale_Return_Amount, ISNULL(Deduction_Item_Issue_Amount,0) as Deduction_Item_Issue_Amount, ISNULL(Deduction_CREDIT_Amount,0) as Deduction_CREDIT_Amount,ISNULL(Issue_Return_Amount,0) as Issue_Return_Amount,Total_Basic_AMOUNT from( "
        'sQuery += " select addd,DOC_DATE,UOM_Code,Qty as NewQty, Qty,RATE,Net_AMOUNT as Amount ,MCC_CODE+' -'+MCC_NAME+'  QtyMode :'+UOM_Code    as MCC_CODE ,VSP_CODE ,SHIFT,ROUTE_CODE+' -'+Route_Name as ROUTE_CODE  ,Vendor_Name ,MCC_NAME,SAMPLE_NO ,TYPE ,CLR,FAT_PER ,SNF_PER ,VLC_Code_VLC_Uploader+' - '+VLC_Name as VLC_Code_VLC_Uploader, VLC_Code,emp,incentive,HEDAmt,AstAMT,DedAmt,sale_AMt,Deduction_Debit_Amount, Deduction_MCC_Sale_Amount, Deduction_MCC_Sale_Return_Amount, Deduction_Item_Issue_Amount, Deduction_CREDIT_Amount,Issue_Return_Amount,Total_Basic_AMOUNT from "
        sQuery += "select  TSPL_MCC_MASTER.add1 +case when len(TSPL_MCC_MASTER.add2)>0 then ', '+TSPL_MCC_MASTER.add2 else '' end + case when LEN(TSPL_COMPANY_MASTER.City_Code)>0 then ', '+MCC_City.City_Name  else ' ' end + case when len(TSPL_MCC_MASTER.State_Code )>0 then MCC_State.STATE_NAME else '' end  as MCC_address, '" & fromDate & "'  as fromDate ,'" & Todate & "'  as Todate ,'" & companyADD & "'  as companyADD, '" & CompName & "'  as CompName,'" & CompCode & "'  as CompCode,TSPL_COMPANY_MASTER .Logo_Img   as compLogo1 ,TSPL_COMPANY_MASTER .Logo_Img2 as compLogo2,PaymentProcess.Total_EMP_Amount,PaymentProcess.Incentive_Amount ,PaymentProcess.Incentive_EMP_Amount ,PaymentProcess.EMP_Amount ,PaymentProcess.Vsp_Own_System_Amount ,PaymentProcess.Head_Load_Amount ,(PaymentProcess.Payable_Amount) as Payable_Amount,(PaymentProcess.Credit_Note_Amount)as Credit_Note_Amount,(PaymentProcess.Deduction_Amount)*(-1) as Deduction_Amount,(PaymentProcess.Item_Issue_Amount)*(-1) as Item_Issue_Amount,(PaymentProcess.Item_Issue_Return_Amount) as Item_Issue_Return_Amount,(PaymentProcess.MCC_Sale_Amount)*(-1) as MCC_Sale_Amount ,(PaymentProcess.MCC_Sale_Return_Amount) as MCC_Sale_Return_Amount, TSPL_MCC_MASTER.add1 + TSPL_MCC_MASTER.add2 as addd,TSPL_MILK_RECEIPT_DETAIL.UOM_Code,TSPL_MILK_PURCHASE_INVOICE_DETAIL.Qty  ,FAT_PER ,(TSPL_MILK_PURCHASE_INVOICE_DETAIL.FAT_PER*TSPL_MILK_PURCHASE_INVOICE_DETAIL.Qty/100) as FATQTY,SNF_PER,(TSPL_MILK_PURCHASE_INVOICE_DETAIL.SNF_PER *TSPL_MILK_PURCHASE_INVOICE_DETAIL.Qty/100) as SNFQTY "
        sQuery += " ,TSPL_MILK_PURCHASE_INVOICE_DETAIL.RATE as RATE,TSPL_MILK_PURCHASE_INVOICE_DETAIL.AMOUNT as Net_AMOUNT, TSPL_MILK_PURCHASE_INVOICE_HEAD.MCC_CODE , convert(varchar,TSPL_MILK_RECEIPT_HEAD.DOC_DATE,103) as DOC_DATE,TSPL_MILK_PURCHASE_INVOICE_HEAD.VSP_CODE ,TSPL_MILK_SAMPLE_HEAD.SHIFT,"
        sQuery += " TSPL_MILK_PURCHASE_INVOICE_HEAD.ROUTE_CODE ,TSPL_VENDOR_MASTER.Vendor_Name,TSPL_MCC_ROUTE_MASTER .Route_Name  ,TSPL_MCC_MASTER .MCC_NAME ,TSPL_MILK_SAMPLE_DETAIL.TYPE ,TSPL_MILK_SAMPLE_DETAIL.CLR,TSPL_MILK_SAMPLE_DETAIL.SAMPLE_NO ,TSPL_VLC_MASTER_HEAD.VLC_Code,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader,"
        sQuery += " TSPL_VLC_MASTER_HEAD.VLC_Name ,coalesce(TSPL_MILK_PURCHASE_INVOICE_HEAD.TOTAL_PaymentCOMMISSION,0) as [EMP],coalesce(TSPL_MILK_PURCHASE_INVOICE_HEAD.incentive_head,0) as Incentive,coalesce(TSPL_MILK_PURCHASE_INVOICE_HEAD.total_head_load_amount,0) as HEDAmt,coalesce(TSPL_MILK_PURCHASE_INVOICE_HEAD.total_Own_Asset_Amount,0) as AstAMT,coalesce(Total_dEDUCTION_AMOUNT,0) as DedAmt ,TSPL_MILK_PURCHASE_INVOICE_HEAD.Total_Basic_AMOUNT"
        sQuery += "  from TSPL_MILK_PURCHASE_INVOICE_DETAIL  Inner Join TSPL_MILK_PURCHASE_INVOICE_HEAD On TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE =TSPL_MILK_PURCHASE_INVOICE_DETAIL.DOC_CODE  left outer join TSPL_MILK_SRN_HEAD  on TSPL_MILK_SRN_HEAD .DOC_CODE  =TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_CODE"
        sQuery += "   Left Outer Join TSPL_MILK_SAMPLE_HEAD On TSPL_MILK_SAMPLE_HEAD.DOC_CODE =      TSPL_MILK_SRN_HEAD.MILK_SAMPLE_CODE    Left Outer Join TSPL_MILK_SAMPLE_DETAIL On TSPL_MILK_SAMPLE_DETAIL.DOC_CODE      = TSPL_MILK_SAMPLE_HEAD.DOC_CODE And TSPL_MILK_SAMPLE_DETAIL.VLC_DOC_CODE      = TSPL_MILK_SRN_HEAD.VLC_DOC_CODE  "
        sQuery += " left outer join TSPL_MILK_RECEIPT_HEAD on TSPL_MILK_RECEIPT_HEAD.DOC_CODE =TSPL_MILK_SAMPLE_HEAD.MILK_RECEIPT_CODE   left outer join TSPL_MILK_RECEIPT_DETAIL on TSPL_MILK_RECEIPT_DETAIL.DOC_CODE =TSPL_MILK_RECEIPT_HEAD.DOC_CODE and   TSPL_MILK_SRN_HEAD.vlc_doc_Code = TSPL_MILK_RECEIPT_DETAIL.VLC_DOC_CODE Left Outer Join TSPL_VENDOR_MASTER On"
        sQuery += " TSPL_MILK_PURCHASE_INVOICE_HEAD.VSP_CODE =TSPL_VENDOR_MASTER.Vendor_Code And TSPL_VENDOR_MASTER.Form_Type = 'VSP'   Left Outer Join TSPL_MCC_MASTER On TSPL_MILK_RECEIPT_HEAD .MCC_CODE = TSPL_MCC_MASTER.MCC_Code  left join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code =TSPL_MCC_MASTER.MCC_Code Left Outer Join TSPL_MCC_ROUTE_MASTER On TSPL_MILK_RECEIPT_DETAIL.ROUTE_CODE =TSPL_MCC_ROUTE_MASTER.Route_Code  left outer join TSPL_VLC_MASTER_HEAD on"
        sQuery += " TSPL_VLC_MASTER_HEAD.VLC_Code =TSPL_MILK_PURCHASE_INVOICE_DETAIL.VLC_NO  "
        sQuery += " left join TSPL_CITY_MASTER  as MCC_City on MCC_City.city_code=TSPL_MCC_MASTER.City_code "
        sQuery += " left join TSPL_STATE_MASTER as MCC_State on MCC_State.STATE_CODE =TSPL_MCC_MASTER.State_Code "
        sQuery += "  left join  (select VLC_Code, VSP_CODE,sum(Total_EMP_Amount) as Total_EMP_Amount,sum(Incentive_Amount) as Incentive_Amount,sum(Incentive_EMP_Amount) as Incentive_EMP_Amount,sum(EMP_Amount) as EMP_Amount,sum(Vsp_Own_System_Amount) as Vsp_Own_System_Amount,sum(Head_Load_Amount) as Head_Load_Amount,sum(Payable_Amount) as Payable_Amount,sum(Credit_Note_Amount)as Credit_Note_Amount,sum(Deduction_Amount) as Deduction_Amount,sum(Item_Issue_Amount) as Item_Issue_Amount,sum(Item_Issue_Return_Amount) as Item_Issue_Return_Amount,sum(MCC_Sale_Amount) as MCC_Sale_Amount ,sum(MCC_Sale_Return_Amount) as MCC_Sale_Return_Amount from (select TSPL_PAYMENT_PROCESS_DETAIL.Incentive_Amount ,TSPL_PAYMENT_PROCESS_DETAIL.Incentive_EMP_Amount ,TSPL_PAYMENT_PROCESS_DETAIL.EMP_Amount ,TSPL_PAYMENT_PROCESS_DETAIL.Vsp_Own_System_Amount,TSPL_PAYMENT_PROCESS_DETAIL.Total_EMP_Amount ,TSPL_PAYMENT_PROCESS_DETAIL.Head_Load_Amount , TSPL_VLC_MASTER_HEAD.VLC_Code  ,TSPL_PAYMENT_PROCESS_DETAIL.VSP_CODE ,TSPL_PAYMENT_PROCESS_DETAIL.Payable_Amount,TSPL_PAYMENT_PROCESS_DETAIL.Credit_Note_Amount ,TSPL_PAYMENT_PROCESS_DETAIL.Deduction_Amount  ,TSPL_PAYMENT_PROCESS_DETAIL.Item_Issue_Return_Amount ,TSPL_PAYMENT_PROCESS_DETAIL.Item_Issue_Amount,TSPL_PAYMENT_PROCESS_DETAIL.MCC_Sale_Amount ,TSPL_PAYMENT_PROCESS_DETAIL.MCC_Sale_Return_Amount  from TSPL_PAYMENT_PROCESS_DETAIL"
        sQuery += " left join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_HEAD.Doc_No =TSPL_PAYMENT_PROCESS_DETAIL.Doc_No"
        sQuery += " left join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader =TSPL_PAYMENT_PROCESS_DETAIL.VLC_CODE_Uploader " & whrcls1 & ""
        sQuery += " ) as pp group by VSP_CODE,VLC_Code"
        sQuery += " ) as PaymentProcess on "
        sQuery += "  PaymentProcess.vsp_code = TSPL_MILK_PURCHASE_INVOICE_Head.vsp_code And PaymentProcess.VLC_Code = TSPL_MILK_RECEIPT_DETAIL.VLC_Code"
        sQuery += " left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code=TSPL_MILK_PURCHASE_INVOICE_Head.Comp_Code"

        sQuery += "  " & whrcls & " "
        sQuery += "order by vsp_code"

        '================================================= END ALL TYPE DEDUCTION=================================================
        'sQuery += " ) as yy left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_name='" & CompName & "'"
        'sQuery += "  order by convert(date,DOC_DATE,103)"
        Dim dt As New DataTable
        dt = clsDBFuncationality.GetDataTable(sQuery)



        'dtgv = clsDBFuncationality.GetDataTable(sQuery)

        If dt IsNot Nothing And dt.Rows.Count > 0 Then
            gv.DataSource = Nothing
            gv.Rows.Clear()
            gv.Columns.Clear()
            gv.DataSource = dt
            gv.GroupDescriptors.Clear()
            gv.MasterTemplate.SummaryRowsBottom.Clear()
            FormatGrid()
            If btnReferesh = False Then
                Dim frmCRV As New frmCrystalReportViewer()
                frmCRV.funsubreportWithdt(CrystalReportFolder.MilkProcurement, dt, Nothing, "crptMilkPurchaseBillPaymentProcess", "SubMilkPurchaseBill.rpt", "", "Address.rpt")
                frmCRV = Nothing
            End If
        Else
            clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)
        End If

    End Sub

    Private Sub txtMonth_ValueChanged(sender As Object, e As EventArgs) Handles txtMonth.ValueChanged
        Try
            AllowDateChanged = False
            dtpFromDate.MinDate = "01-Jan-0001"
            dtpFromDate.MaxDate = Date.DaysInMonth(txtMonth.Value.Year, txtMonth.Value.Month) & "-" & txtMonth.Value.Month & "-" & txtMonth.Value.Year
            dtpFromDate.MinDate = "01-" & txtMonth.Value.Month & "-" & txtMonth.Value.Year
            dtpToDate.Value = Date.DaysInMonth(txtMonth.Value.Year, txtMonth.Value.Month) & "-" & txtMonth.Value.Month & "-" & txtMonth.Value.Year
            AllowDateChanged = True
        Catch ex As Exception
        End Try
    End Sub

    Private Sub rmPDF_Click(sender As Object, e As EventArgs) Handles rmPDF.Click
        print(EnumExportTo.PDF)
    End Sub
End Class
