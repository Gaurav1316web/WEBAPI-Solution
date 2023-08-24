Imports common
Imports System.IO

Public Class rptrlPenaltyRegister
    Inherits FrmMainTranScreen

    '#Region "Variables"
    '    Public PK_Id As Integer
    '    Public DocumentCode As String
    '    Public Against_Tender_Schedule_PK_Id As Integer
    '    Public Penalty_Date As Date
    '    Public Penalty As Decimal
    '#End Region
#Region "Variables"

    Dim blnPageLoad As Boolean = False
    Private isCellValueChangedOpen As Boolean = False
    Private isNewEntry As Boolean = False
    Private isInsideLoadData As Boolean = False
    Private isPageLoadData As Boolean = False
    Const colLineNo As String = "COLLNO"
    Const colICode As String = "COLICODE"
    Const colIName As String = "COLINAME"
    Const colLCode As String = "COLLCODE"
    Const colLName As String = "COLLNAME"
    Const colVCode As String = "COLVCODE"
    Const colVName As String = "COLVNAME"
    Const colQty As String = "COLQTY"
    Const colUnit As String = "COLUNIT"
    Const colRate As String = "COLRATE"
    Const colAmt As String = "COLAMT"
    Const colRemarks As String = "colRemarks"
    Const colComments As String = "colComments"



    Const colScheduleSNo As String = "colScheduleSNo"
    Const colScheduleParentSNo As String = "colScheduleParentSNo"
    Const colScheduleNo As String = "colScheduleNo"
    Const colScheduleFromDate As String = "colScheduleFromDate"
    Const colScheduleToDate As String = "colScheduleToDate"
    Const colScheduleVCode As String = "colScheduleVCode"
    Const colScheduleVName As String = "colScheduleVName"
    Const colScheduleLCode As String = "colScheduleLCode"
    Const colScheduleLName As String = "colScheduleLName"
    Const colScheduleICode As String = "colScheduleICode"
    Const colScheduleIName As String = "colScheduleIName"
    Const colScheduleQtyPer As String = "colScheduleQtyPer"
    Const colScheduleQty As String = "colScheduleQty"
    Const colScheduleShortPer As String = "colScheduleShortPer"
    Const colScheduleShort As String = "colScheduleShort"
    Const colScheduleLateDays As String = "Late_Days"
    Const colScheduleExtensionDays As String = "colScheduleExtensionDays"
    Dim ButtonToolTip As ToolTip = New ToolTip()
#End Region
    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.FrmPendingRequisitionQty)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        radbtnExp.Visible = MyBase.isExport
    End Sub

    Private Sub txtBillToLocation__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtLocation._MYValidating

        Try
            Dim qry As String = "select Location_Code as Code,Location_Desc as Name from TSPL_LOCATION_MASTER "
            Dim WhrCls As String = " Location_Type='Physical'  "
            If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                WhrCls += "  and  Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
            End If
            txtLocation.Value = clsCommon.ShowSelectForm("VMasterFND", qry, "Code", WhrCls, txtLocation.Value, "Code", isButtonClicked)
            lblBillToLocation.Text = clsLocation.GetName(txtLocation.Value, Nothing)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub txtTenderNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtTenderNo._MYValidating

        Try
            If clsCommon.myLen(txtLocation.Value) <= 0 Then

                Throw New Exception("Please select Location")
            End If
            Dim qry As String = "select xx.DocumentCode,max(xx.DocumentDate) as DocumentDate,xx.Location,max(TSPL_LOCATION_MASTER.Location_Desc) as LocationName,xx.Vendor_Code,max(TSPL_VENDOR_MASTER.Vendor_Name) as VendorName,xx.Item_Code,max(TSPL_ITEM_MASTER.Item_Desc) as ItemDesc from (
select DocumentCode,max(DocumentDate) as DocumentDate,Location,Vendor_Code,Item_Code,1 as RI,1 as Chk from (
select TSPL_TENDER_HEADER.DocumentCode,TSPL_TENDER_HEADER.DocumentDate,TSPL_TENDER_DETAIL.Location,TSPL_TENDER_DETAIL.Vendor_Code,TSPL_TENDER_DETAIL.Item_Code from TSPL_TENDER_DETAIL
left outer join TSPL_TENDER_HEADER on TSPL_TENDER_HEADER.DocumentCode=TSPL_TENDER_DETAIL.DocumentCode
where TSPL_TENDER_HEADER.Posted=1  and TSPL_TENDER_DETAIL.Location='" + txtLocation.Value + "'
)x Group by DocumentCode,Location,Vendor_Code,Item_Code
union all
select TSPL_TENDER_PENALTY.Tender_No as DocumentCode,null as  DocumentDate, Location_Code as Location,Vendor_Code,Item_Code,-1 as RI,0 as Chk from TSPL_TENDER_PENALTY 
)xx 
left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=xx.Location
left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=xx.Vendor_Code
left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=xx.Item_Code
Group by xx.DocumentCode,xx.Location,xx.Vendor_Code,xx.Item_Code having sum(xx.RI)>0 and sum(xx.Chk)>0 order by DocumentDate"

            Dim whr As String = "TSPL_TENDER_HEADER.Posted=1 and exists (select 1 from TSPL_TENDER_DETAIL where TSPL_TENDER_DETAIL.DocumentCode=TSPL_TENDER_HEADER.DocumentCode and TSPL_TENDER_DETAIL.Location='" + txtLocation.Value + "')"
            txtTenderNo.Value = clsTenderHead.getFinder(whr, txtTenderNo.Value, isButtonClicked)
            lblTender.Text = clsCommon.GetPrintDate(clsDBFuncationality.getSingleValue("select DocumentDate from TSPL_TENDER_HEADER where DocumentCode = '" + txtTenderNo.Value + "' "))
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            txtTenderNo.Value = ""
        End Try

    End Sub

    Private Sub txtVendorNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtVendorNo._MYValidating

        Try
            If clsCommon.myLen(txtLocation.Value) <= 0 Then
                Throw New Exception("Please select Location")
            End If
            If clsCommon.myLen(txtTenderNo.Value) <= 0 Then
                Throw New Exception("Please select Tender")
            End If
            Dim qry As String = "select Vendor_Code as Code,Vendor_Name as Name,ISNULL(TSPL_VENDOR_MASTER.alies_name,'') As [Alies Name],Terms_Code as [Term Code] ,Terms_Code_Desc as [Term Description] ,Tax_Group as [Tax Group],Tax_Group_Desc as [Tax Group Description] from TSPL_VENDOR_MASTER"
            Dim whr As String = " TSPL_VENDOR_MASTER.Status='N' and exists (select 1 from TSPL_TENDER_DETAIL where TSPL_TENDER_DETAIL.DocumentCode='" + txtTenderNo.Value + "' and TSPL_TENDER_DETAIL.Location='" + txtLocation.Value + "' and TSPL_TENDER_DETAIL.Vendor_Code=TSPL_VENDOR_MASTER.Vendor_Code) "
            txtVendorNo.Value = clsVendorMaster.getFinder(whr, txtVendorNo.Value, isButtonClicked)
            lblVendorName.Text = clsVendorMaster.GetName(txtVendorNo.Value, Nothing)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            txtVendorNo.Value = ""
        End Try

    End Sub

    Private Sub txtItem__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtItem._MYValidating

        Try
            If clsCommon.myLen(txtLocation.Value) <= 0 Then
                Throw New Exception("Please select Location")
            End If
            If clsCommon.myLen(txtTenderNo.Value) <= 0 Then
                Throw New Exception("Please select Tender")
            End If
            If clsCommon.myLen(txtVendorNo.Value) <= 0 Then
                Throw New Exception("Please select Vendor")
            End If
            Dim whr As String = "  exists (select 1 from TSPL_TENDER_DETAIL where TSPL_TENDER_DETAIL.DocumentCode='" + txtTenderNo.Value + "' and TSPL_TENDER_DETAIL.Location='" + txtLocation.Value + "' and TSPL_TENDER_DETAIL.Vendor_Code='" + txtVendorNo.Value + "' and TSPL_TENDER_DETAIL.Item_Code=TSPL_ITEM_MASTER.Item_Code) "
            txtItem.Value = clsItemMaster.getFinder(whr, txtItem.Value, isButtonClicked)
            lblItem.Text = clsItemMaster.GetItemName(txtItem.Value, Nothing)

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            txtItem.Value = ""
        End Try

    End Sub


    Private Sub btnreset_Click(sender As Object, e As EventArgs) Handles btnreset.Click
        Try
            'dtpfromdate.Value = clsCommon.GETSERVERDATE()
            'dtpTodate.Value = clsCommon.GETSERVERDATE()
            RadGroupBox1.Enabled = True
            gv.DataSource = Nothing
            gvSchedule.DataSource = Nothing
            txtLocation.Value = ""
            lblBillToLocation.Text = ""
            txtTenderNo.Value = ""
            lblTender.Text = ""
            txtVendorNo.Value = ""
            lblVendorName.Text = ""
            txtItem.Value = ""
            lblItem.Text = ""
            btnclose.Visible = True
            btnGo.Visible = True
            radbtnExp.Visible = True
            lblOrdered.Text = ""
            lblPending.Text = ""
            lblReceived.Text = ""
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click

        Try
            If clsCommon.myLen(txtLocation.Value) <= 0 Then
                txtLocation.Focus()
                Throw New Exception("Please select " + txtLocation.MyLinkLable1.Text)
            End If
            If clsCommon.myLen(txtTenderNo.Value) <= 0 Then
                txtTenderNo.Focus()
                Throw New Exception("Please select " + txtTenderNo.MyLinkLable1.Text)
            End If
            If clsCommon.myLen(txtVendorNo.Value) <= 0 Then
                txtVendorNo.Focus()
                Throw New Exception("Please select " + txtVendorNo.MyLinkLable1.Text)
            End If
            If clsCommon.myLen(txtItem.Value) <= 0 Then
                txtItem.Focus()
                Throw New Exception("Please select " + txtItem.MyLinkLable1.Text)
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

        PageSetupReport_ID = MyBase.Form_ID
        TemplateGridview = gv
        LoadData()
        SetSchedule()
        CalculateDifference()


    End Sub

    Sub LoadData()
        Try
            Dim qry As String = ""
            Dim whr As String = ""



            qry = " select  TSPL_TENDER_PENALTY.Document_No ,TSPL_PURCHASE_ORDER_HEAD.RefTendorNo AS RAL,TSPL_GRN_DETAIL.Item_Code as 'Item Code',TSPL_GRN_DETAIL.Item_Desc as 'Item Description',TSPL_GRN_HEAD.Vendor_Code as 'Vendor Code' ,TSPL_GRN_HEAD.Vendor_Name AS 'Vendor Name',TSPL_GRN_HEAD.GRN_No as 'GRN No',convert(varchar, TSPL_GRN_HEAD.GRN_Date,103) as 'GRN Date',TSPL_GRN_HEAD.VehicleNo,(case when isnull(TSPL_GRN_HEAD.Status,0) = 1 then 'APPROVED' else 'PENDING' end) as GRNStatus,TSPL_SRN_HEAD.SRN_No as 'SRN No',convert(varchar,TSPL_SRN_HEAD.SRN_Date,103) as  'SRN Date',(Case when isnull(TSPL_SRN_HEAD.Status,0) = 1 then 'APPROVED' else 'PENDING' end) as SRNStatus, TSPL_PO_WEIGHTMENT_HEAD.Weighment_Code as 'Weightment Code',convert(varchar,TSPL_PO_WEIGHTMENT_HEAD.Weighment_Date,103) as 'Weighment Date',TSPL_PO_WEIGHTMENT_DETAIL.Gross_Weight as 'Gross Weight',TSPL_PO_WEIGHTMENT_DETAIL.Tare_Weight as 'Tare Weight',TSPL_PO_WEIGHTMENT_DETAIL.Extra_Weight as 'Extra Weight',TSPL_PO_WEIGHTMENT_DETAIL.Net_Weight as 'Net Weight',(Case when isnull(TSPL_PO_WEIGHTMENT_HEAD.Status,0)= 1 then 'APPROVED' else 'PENDING' end) as WeightmentStatus,isNull(TSPL_SRN_DETAIL.SRN_Qty,0) as 'SRN Qty',TSPL_PO_WEIGHTMENT_DETAIL.UOM
                    ,TSPL_SRN_DEDUCTION_SECURITY.Ded_Amt as SecurityDeductionAmt,TSPL_SRN_DEDUCTION.Ded_Per as QualityDeductionPer,TSPL_SRN_DEDUCTION.Ded_Amt as QualityDeductionAmt,case when isnull(TSPL_SRN_TENDER.Penalty,0)=0 then null else TSPL_SRN_TENDER.Qty end as LatePenaltyQty,case when isnull(TSPL_SRN_TENDER.Penalty,0)=0 then null else TSPL_TENDER_SCHEDULE_PENALTY.Penalty end as LatePenaltyPer,TSPL_SRN_TENDER.Penalty as LatePenaltyAmt,TSPL_PI_DETAIL.PI_No as 'PI No',TSPL_PI_HEAD.PI_Date as 'PI Date'
                        from TSPL_GRN_DETAIL
                    left outer join TSPL_GRN_HEAD on TSPL_GRN_HEAD.GRN_No=TSPL_GRN_DETAIL.GRN_No

                    left outer join TSPL_PURCHASE_ORDER_HEAD on TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No=TSPL_GRN_DETAIL.PO_Id
                    left outer join TSPL_SRN_DETAIL on TSPL_SRN_DETAIL.GRN_ID=TSPL_GRN_HEAD.GRN_No and TSPL_SRN_DETAIL.Item_Code=TSPL_GRN_DETAIL.Item_Code

                    left outer join TSPL_SRN_HEAD on  TSPL_SRN_HEAD.SRN_No = TSPL_SRN_DETAIL.SRN_No
					left outer join TSPL_TENDER_PENALTY_DETAIL on TSPL_TENDER_PENALTY_DETAIL.SRN_No = TSPL_SRN_HEAD.SRN_No

                    left outer join TSPL_TENDER_PENALTY ON TSPL_TENDER_PENALTY.Document_No = TSPL_TENDER_PENALTY_DETAIL.Document_No

                    left outer join TSPL_PI_DETAIL on TSPL_PI_DETAIL.SRN_Id = TSPL_SRN_DETAIL.SRN_No and TSPL_SRN_DETAIL.Item_Code=TSPL_GRN_DETAIL.Item_Code
					Left outer join TSPL_PI_HEAD on TSPL_PI_HEAD.PI_No = TSPL_PI_DETAIL.PI_No

                    
                    left outer join TSPL_PO_WEIGHTMENT_HEAD on TSPL_PO_WEIGHTMENT_HEAD.Against_GRN_No=TSPL_GRN_HEAD.GRN_No
                    left outer join TSPL_PO_WEIGHTMENT_DETAIL on TSPL_PO_WEIGHTMENT_DETAIL.Weighment_Code= TSPL_PO_WEIGHTMENT_HEAD.Weighment_Code and  TSPL_PO_WEIGHTMENT_DETAIL.Item_Code=TSPL_GRN_DETAIL.Item_Code
                    left outer join TSPL_SRN_DEDUCTION_SECURITY on TSPL_SRN_DEDUCTION_SECURITY.SRN_No=TSPL_SRN_HEAD.SRN_No and TSPL_SRN_DEDUCTION_SECURITY.Item_Code=TSPL_SRN_DETAIL.Item_Code
                    left outer join TSPL_SRN_DEDUCTION on TSPL_SRN_DEDUCTION.SRN_No=TSPL_SRN_HEAD.SRN_No and TSPL_SRN_DEDUCTION.Item_Code=TSPL_SRN_DETAIL.Item_Code
                    left outer join TSPL_SRN_TENDER on TSPL_SRN_TENDER.SRN_No=TSPL_SRN_HEAD.SRN_No and TSPL_SRN_TENDER.Item_Code=TSPL_SRN_DETAIL.Item_Code and isnull(TSPL_SRN_TENDER.Penalty,0)>0
                    left outer join TSPL_TENDER_SCHEDULE_PENALTY on  TSPL_TENDER_SCHEDULE_PENALTY.PK_Id=TSPL_SRN_TENDER.Against_Tender_Schedule_Penalty_PK_Id
                    left outer join TSPL_TENDER_HEADER on TSPL_TENDER_HEADER.DocumentCode=TSPL_PURCHASE_ORDER_HEAD.RefTendorNo
                    left outer join TSPL_QC_CHECK_HEAD on TSPL_QC_CHECK_HEAD.Gate_Entry_No=TSPL_GRN_HEAD.GRN_No
                    where TSPL_PURCHASE_ORDER_HEAD.Against_Tender='Y' and TSPL_PURCHASE_ORDER_HEAD.RefTendorNo='" + txtTenderNo.Value + "' and TSPL_QC_CHECK_HEAD.QC_Status<>'Rejected'  and TSPL_GRN_DETAIL.Item_Code='" + txtItem.Value + "' and TSPL_GRN_HEAD.Vendor_Code='" + txtVendorNo.Value + "' and TSPL_GRN_HEAD.Bill_To_Location='" + txtLocation.Value + "' and ISNULL( TSPL_GRN_HEAD.IsCancel,0)=0  
                    Order by CONVERT(date, TSPL_GRN_HEAD.GRN_Date,103),isnull(TSPL_SRN_HEAD.Status,0) desc "
            Dim dtgv As New DataTable
            dtgv = clsDBFuncationality.GetDataTable(qry)
            If dtgv IsNot Nothing And dtgv.Rows.Count > 0 Then
                gv.Visible = True
                RadPageView1.SelectedPage = RadPageViewPage2
                gv.DataSource = dtgv
                gv.ReadOnly = True
                SetGridFormat()
            Else
                common.clsCommon.MyMessageBoxShow("No Data Found")
                gv.DataSource = Nothing
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Sub SetGridFormat()
        gv.ShowGroupPanel = False
        gv.ShowRowHeaderColumn = False
        gv.AllowAddNewRow = False
        gv.AllowDeleteRow = False
        gv.EnableFiltering = True
        gv.ShowFilteringRow = True

        gv.MasterTemplate.SummaryRowsBottom.Clear()

        For ii As Integer = 0 To gv.Columns.Count - 1
            gv.Columns(ii).ReadOnly = True
            gv.Columns(ii).BestFit()
        Next

        Dim summaryRowItem As New GridViewSummaryRowItem()

        Dim item5 As New GridViewSummaryItem("Gross Weight", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item5)
        Dim item4 As New GridViewSummaryItem("Tare Weight", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item4)
        Dim item6 As New GridViewSummaryItem("Net Weight", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item6)
        Dim item7 As New GridViewSummaryItem("QualityDeductionPer", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item7)
        Dim item9 As New GridViewSummaryItem("QualityDeductionAmt", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item9)
        Dim item8 As New GridViewSummaryItem("SRN Qty", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item8)
        Dim item1 As New GridViewSummaryItem("LatePenaltyAmt", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)
        Dim item2 As New GridViewSummaryItem("LatePenaltyQty", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item2)

        gv.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

        gv.AutoSizeRows = False
        gv.BestFitColumns()

    End Sub

    Private Sub ExportGrid(ByVal exporter As EnumExportTo)
        Try
            If gv.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow("No Data Found to Export", Me.Text)
                Exit Sub
            End If

            Dim StrReportName As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptrlPenaltyRegister & "'"))
            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("Name : " & StrReportName)
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)

            If exporter = EnumExportTo.Excel Then
                transportSql.applyExportTemplate(gv, PageSetupReport_ID)
                clsCommon.MyExportToExcelGrid(StrReportName, gv, arrHeader, Me.Text)
            Else
                transportSql.applyExportTemplate(gv, PageSetupReport_ID)
                clsCommon.MyExportToPDF(StrReportName, gv, arrHeader, Me.Text)
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        ExportGrid(EnumExportTo.Excel)
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        ExportGrid(EnumExportTo.PDF)
    End Sub


    Sub SetSchedule()
        Try
            Dim Baseqry As String = ""

            Baseqry = "select TSPL_TENDER_SCHEDULE.PK_Id as 'PK Id',TSPL_TENDER_SCHEDULE.DocumentCode,TSPL_TENDER_SCHEDULE.PSNo,TSPL_TENDER_SCHEDULE.Schedule_No as 'Schedule No',TSPL_TENDER_SCHEDULE.From_Date as 'From Date',TSPL_TENDER_SCHEDULE.To_Date as 'To Date',TSPL_TENDER_SCHEDULE.Vendor_Code as 'Vendor Code',TSPL_TENDER_SCHEDULE.Location_Code as 'Location Code',TSPL_TENDER_SCHEDULE.Item_Code as 'Item Code',TSPL_TENDER_SCHEDULE.Schedule_Qty_Per as 'Schedule Qty Per',TSPL_TENDER_SCHEDULE.Schedule_Qty as 'Schedule Qty',TSPL_TENDER_SCHEDULE.Schedule_Short_Per as 'Schedule Short Per',TSPL_TENDER_SCHEDULE.Schedule_Short as 'Schedule Short',TSPL_TENDER_SCHEDULE.Late_Days as 'Late Days',TSPL_TENDER_SCHEDULE.Extension_Days as 'Extension Days' from TSPL_TENDER_SCHEDULE  where TSPL_TENDER_SCHEDULE.DocumentCode= '" + txtTenderNo.Value + "'    and TSPL_TENDER_SCHEDULE.Location_Code = '" + txtLocation.Value + "'  and TSPL_TENDER_SCHEDULE.Vendor_Code = '" + txtVendorNo.Value + "'   and TSPL_TENDER_SCHEDULE.Item_Code = '" + txtItem.Value + "' order by TSPL_TENDER_SCHEDULE.PK_Id"

            Dim dtgv As New DataTable
            dtgv = clsDBFuncationality.GetDataTable(Baseqry)
            If dtgv IsNot Nothing And dtgv.Rows.Count > 0 Then
                gvSchedule.DataSource = Nothing
                gvSchedule.Rows.Clear()
                gvSchedule.Columns.Clear()
                gvSchedule.DataSource = dtgv
                gvSchedule.BestFitColumns()
                'ReStoreGridLayout()
                gvSchedule.MasterTemplate.SummaryRowsBottom.Clear()
                For Each col As GridViewColumn In gvSchedule.Columns
                    col.Width = 150
                    col.ReadOnly = True
                Next
                Dim summaryRowItem As New GridViewSummaryRowItem()
                Dim item1 As New GridViewSummaryItem("Schedule Qty", "{0:F0}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item1)

                gvSchedule.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

            End If

            If dtgv.Rows.Count <= 0 Then
                gvSchedule.DataSource = Nothing
                gvSchedule.Rows.Clear()
                gvSchedule.Columns.Clear()
                ' clsCommon.MyMessageBoxShow("No Data Found")
                Exit Sub
            End If

        Catch ex As Exception

            clsCommon.MyMessageBoxShow(Me, ex.Message)

        End Try

    End Sub




    Sub CalculateDifference()

        Dim Received As Decimal = 0
        Dim Ordered As Decimal = 0

        For Each row As GridViewRowInfo In gv.Rows
            'If (row.Cells(20).Value) = 0 Then
            'sum1 += 0
            'Else
            ' Assuming the column you want to calculate the sum for in gv is at index 0 '
            Received += Convert.ToDecimal(row.Cells(20).Value)
            'End If
        Next

        For Each row As GridViewRowInfo In gvSchedule.Rows
            ' Assuming the column you want to calculate the sum for in gvschedule is at index 0 '
            Ordered += Convert.ToDecimal(row.Cells(10).Value)
        Next

        Dim Pending As Decimal = 0

        Pending = Ordered - Received
        lblOrdered.Text = clsCommon.myRoundOFF(Ordered, 2)
        lblReceived.Text = clsCommon.myRoundOFF(Received, 2)

        lblPending.Text = clsCommon.myRoundOFF(Pending, 2)
        'Return difference

    End Sub

    Private Sub Printbtn_Click(sender As Object, e As EventArgs) Handles Printbtn.Click
        Try
            Dim strqry As String = "    select  TSPL_TENDER_HEADER.DocumentCode,TSPL_TENDER_HEADER.DocumentDate ,TSPL_PURCHASE_ORDER_HEAD.RefTendorNo AS RAL,TSPL_GRN_DETAIL.Item_Code as 'Item Code',TSPL_GRN_DETAIL.Item_Desc as 'Item Description',TSPL_GRN_HEAD.Vendor_Code as 'Vendor Code' ,TSPL_GRN_HEAD.Vendor_Name AS 'Vendor Name',TSPL_GRN_HEAD.GRN_No as 'GRN No',convert(varchar, TSPL_GRN_HEAD.GRN_Date,103) as 'GRN Date',TSPL_GRN_HEAD.VehicleNo,TSPL_SRN_HEAD.SRN_No as 'SRN No',convert(varchar,TSPL_SRN_HEAD.SRN_Date,103) as  'SRN Date', TSPL_PO_WEIGHTMENT_HEAD.Weighment_Code as 'Weightment Code',convert(varchar,TSPL_PO_WEIGHTMENT_HEAD.Weighment_Date,103) as 'Weighment Date',TSPL_PO_WEIGHTMENT_DETAIL.Gross_Weight as 'Gross Weight',TSPL_PO_WEIGHTMENT_DETAIL.Tare_Weight as 'Tare Weight',TSPL_PO_WEIGHTMENT_DETAIL.Extra_Weight as 'Extra Weight',TSPL_PO_WEIGHTMENT_DETAIL.Net_Weight as 'Net Weight',isNull(TSPL_SRN_DETAIL.SRN_Qty,0) as 'SRN Qty',TSPL_PO_WEIGHTMENT_DETAIL.UOM,TSPL_TENDER_SCHEDULE.schedule_from_date,TSPL_TENDER_SCHEDULE.schedule_to_date,TSPL_TENDER_SCHEDULE.Schedule_No,TSPL_TENDER_SCHEDULE.Schedule_Qty ,TSPL_TENDER_SCHEDULE.Schedule_Qty_Per,TSPL_TENDER_SCHEDULE.Schedule_Short,TSPL_TENDER_SCHEDULE.Schedule_Short_Per,TSPL_TENDER_DETAIL.Qty,TSPL_TENDER_DETAIL.Rate,tspl_location_master.Location_Desc,TSPL_company_master.Logo_Img,TSPL_company_master.Logo_Img2,NoOfSchedule,Schedule_No_Min
                        from TSPL_GRN_DETAIL
                    left outer join TSPL_GRN_HEAD on TSPL_GRN_HEAD.GRN_No=TSPL_GRN_DETAIL.GRN_No
                    left outer join TSPL_PURCHASE_ORDER_HEAD on TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No=TSPL_GRN_DETAIL.PO_Id
                    left outer join TSPL_SRN_DETAIL on TSPL_SRN_DETAIL.GRN_ID=TSPL_GRN_HEAD.GRN_No and TSPL_SRN_DETAIL.Item_Code=TSPL_GRN_DETAIL.Item_Code
                    left outer join TSPL_SRN_HEAD on  TSPL_SRN_HEAD.SRN_No = TSPL_SRN_DETAIL.SRN_No
                    left outer join TSPL_PO_WEIGHTMENT_HEAD on TSPL_PO_WEIGHTMENT_HEAD.Against_GRN_No=TSPL_GRN_HEAD.GRN_No
					left join tspl_location_master on tspl_location_master.location_code=TSPL_GRN_HEAD.Bill_To_Location
                    left outer join TSPL_PO_WEIGHTMENT_DETAIL on TSPL_PO_WEIGHTMENT_DETAIL.Weighment_Code= TSPL_PO_WEIGHTMENT_HEAD.Weighment_Code and  TSPL_PO_WEIGHTMENT_DETAIL.Item_Code=TSPL_GRN_DETAIL.Item_Code
                    left outer join TSPL_SRN_DEDUCTION_SECURITY on TSPL_SRN_DEDUCTION_SECURITY.SRN_No=TSPL_SRN_HEAD.SRN_No and TSPL_SRN_DEDUCTION_SECURITY.Item_Code=TSPL_SRN_DETAIL.Item_Code
                    left outer join TSPL_SRN_DEDUCTION on TSPL_SRN_DEDUCTION.SRN_No=TSPL_SRN_HEAD.SRN_No and TSPL_SRN_DEDUCTION.Item_Code=TSPL_SRN_DETAIL.Item_Code
                    left outer join TSPL_SRN_TENDER on TSPL_SRN_TENDER.SRN_No=TSPL_SRN_HEAD.SRN_No and TSPL_SRN_TENDER.Item_Code=TSPL_SRN_DETAIL.Item_Code and isnull(TSPL_SRN_TENDER.Penalty,0)>0
                     left outer join TSPL_TENDER_HEADER on TSPL_TENDER_HEADER.DocumentCode=TSPL_PURCHASE_ORDER_HEAD.RefTendorNo
					 left join TSPL_company_master on TSPL_company_master.comp_code=TSPL_TENDER_HEADER.comp_code
					 LEFT JOIN (select DOCUMENTCODE,QTY,Rate from tspl_tender_detail where documentcode='" + txtTenderNo.Value + "'and item_code='" + txtItem.Value + "' and vendor_code='" + txtVendorNo.Value + "'and location='" + txtLocation.Value + "')TSPL_TENDER_DETAIL ON TSPL_TENDER_DETAIL.DocumentCode=TSPL_TENDER_HEADER.DocumentCode
					 left outer join  ( select DocumentCode,max(From_Date) as schedule_from_date,max(To_Date) as schedule_to_date,max(Schedule_Qty) as Schedule_Qty,max(Schedule_Qty_Per) as Schedule_Qty_Per,max(Schedule_Short) as Schedule_Short,max(Schedule_Short_Per) as Schedule_Short_Per,max(Schedule_No ) as Schedule_No,count(*) as NoOfSchedule,min(Schedule_No) as Schedule_No_Min from TSPL_TENDER_SCHEDULE where TSPL_TENDER_SCHEDULE.DocumentCode='" + txtTenderNo.Value + "' and 
					 TSPL_TENDER_SCHEDULE.Item_Code='" + txtItem.Value + "'and
					 TSPL_TENDER_SCHEDULE.Vendor_Code='" + txtVendorNo.Value + "' and
					 TSPL_TENDER_SCHEDULE.Location_Code='" + txtLocation.Value + "' GROUP BY DocumentCode) TSPL_TENDER_SCHEDULE on TSPL_TENDER_SCHEDULE.DocumentCode=TSPL_TENDER_HEADER.DocumentCode
                    left outer join TSPL_QC_CHECK_HEAD on TSPL_QC_CHECK_HEAD.Gate_Entry_No=TSPL_GRN_HEAD.GRN_No
                    where TSPL_PURCHASE_ORDER_HEAD.Against_Tender='Y' and TSPL_PURCHASE_ORDER_HEAD.RefTendorNo='" + txtTenderNo.Value + "'and TSPL_QC_CHECK_HEAD.QC_Status<>'Rejected'  and TSPL_GRN_DETAIL.Item_Code='" + txtItem.Value + "' and TSPL_GRN_HEAD.Vendor_Code='" + txtVendorNo.Value + "'and TSPL_GRN_HEAD.Bill_To_Location='" + txtLocation.Value + "' and ISNULL( TSPL_GRN_HEAD.IsCancel,0)=0 
					and TSPL_SRN_HEAD.Status=1
                    Order by CONVERT(date, TSPL_GRN_HEAD.GRN_Date,103) "
            Dim dt As DataTable
            dt = clsDBFuncationality.GetDataTable(strqry)
            If dt.Rows.Count > 0 Then
                Dim frmCRV As New frmCrystalReportViewer()

                frmCRV.funreport(CrystalReportFolder.PurchaseOrder, dt, "ItemRmReport", "ItemWisePrint")
                frmCRV = Nothing
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)

            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message)
        End Try
    End Sub
End Class