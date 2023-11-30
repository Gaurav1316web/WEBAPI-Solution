Imports common
Imports System.IO
Public Class frmRptTankerDispatchWithDeduction
    Inherits FrmMainTranScreen
#Region "Variables"
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim dt As DataTable = Nothing
#End Region

    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.rptTankerDispatchWidthDeduction)
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
            'RefreshData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()
        End If
    End Sub

    Private Sub FrmRptSales_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        RadPageView1.SelectedPage = RadPageViewPage1
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N Adding New Trasnaction")
        funreset()
        LoadReportType()
        cbReportType.SelectedIndex = 0
        txtToDate.Value = clsCommon.GETSERVERDATE
        txtFromDate.Value = txtToDate.Value.AddMonths(-1)
    End Sub

    Sub LoadReportType()
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Rows.Add("Detail")
        dt.Rows.Add("Summary")
        cbReportType.DataSource = dt
        cbReportType.ValueMember = "Code"
        cbReportType.DisplayMember = "Code"
    End Sub
    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        funreset()
    End Sub

    Sub funreset()
        gv1.GroupDescriptors.Clear()
        gv1.MasterTemplate.SummaryRowsBottom.Clear()
        dt = Nothing
        gv1.DataSource = Nothing
        gv1.Columns.Clear()
        gv1.Rows.Clear()
        EnableDisableControls(True)
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        gv1.EnableFiltering = True
        PageSetupReport_ID = clsERPFuncationality.GetReportID(MyBase.Form_ID, cbReportType.Text)
        TemplateGridview = gv1
        RefreshData()
    End Sub

    Public Sub RefreshData()
        Try
            If clsCommon.GetDateWithStartTime(txtFromDate.Value) > clsCommon.GetDateWithStartTime(txtToDate.Value) Then
                Throw New Exception("From date can't be greater than to date")
            End If
            gv1.GroupDescriptors.Clear()
            gv1.MasterTemplate.SummaryRowsBottom.Clear()
            clsCommon.ProgressBarShow()
            clsCommon.ProgressBarUpdate("Loading Data.Please Wait...")
            Dim FinalQry As String = ""
            Dim FromDate As String = clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt")
            Dim ToDate As String = clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt")

            ''changed by richa agarwal 15 Dec, 2016 show dbit tpe deduction with -ve and credit type transactions with +ve
            ' Dim BaseQry As String = "select   TSPL_AP_INVOICE_SECONDARY_TRANSPORTER_DEDUTION_DETAIL.DC_Challan_No,TSPL_AP_INVOICE_SECONDARY_TRANSPORTER_DEDUTION_DETAIL.Deduction_Code ,((case when TSPL_VENDOR_INVOICE_HEAD.Document_Type='C' then -1 else 1 end) * TSPL_AP_INVOICE_SECONDARY_TRANSPORTER_DEDUTION_DETAIL.Amount) as Amount ,TSPL_DEDUCTION_MASTER.Description as DedName " + Environment.NewLine + _
            Dim BaseQry As String = "select   TSPL_AP_INVOICE_SECONDARY_TRANSPORTER_DEDUTION_DETAIL.DC_Challan_No,TSPL_AP_INVOICE_SECONDARY_TRANSPORTER_DEDUTION_DETAIL.Deduction_Code ,((case when TSPL_VENDOR_INVOICE_HEAD.Document_Type='C' then 1 else -1 end) * TSPL_AP_INVOICE_SECONDARY_TRANSPORTER_DEDUTION_DETAIL.Amount) as Amount ,TSPL_DEDUCTION_MASTER.Description as DedName " + Environment.NewLine + _
            " from TSPL_AP_INVOICE_SECONDARY_TRANSPORTER_DEDUTION_DETAIL " + Environment.NewLine + _
            " left outer join TSPL_MCC_DISPATCH_CHALLAN on TSPL_MCC_DISPATCH_CHALLAN.Chalan_NO=TSPL_AP_INVOICE_SECONDARY_TRANSPORTER_DEDUTION_DETAIL.DC_Challan_No " + Environment.NewLine + _
            " left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.document_no=TSPL_AP_INVOICE_SECONDARY_TRANSPORTER_DEDUTION_DETAIL.AP_Invoice_No " + Environment.NewLine + _
            " left outer join TSPL_DEDUCTION_MASTER on TSPL_DEDUCTION_MASTER.Code=TSPL_AP_INVOICE_SECONDARY_TRANSPORTER_DEDUTION_DETAIL.Deduction_Code" + Environment.NewLine + _
            " where TSPL_MCC_DISPATCH_CHALLAN.isPosted = 1 And TSPL_VENDOR_INVOICE_HEAD.Posting_Date Is Not null " + Environment.NewLine + _
            " and TSPL_MCC_DISPATCH_CHALLAN.Dispatch_Date>='" + FromDate + "' and TSPL_MCC_DISPATCH_CHALLAN.Dispatch_Date<='" + ToDate + "'"
            If txtTankerNo.arrValueMember IsNot Nothing AndAlso txtTankerNo.arrValueMember.Count > 0 Then
                BaseQry += " and TSPL_MCC_DISPATCH_CHALLAN.Tanker_No in (" + clsCommon.GetMulcallString(txtTankerNo.arrValueMember) + ")" + Environment.NewLine
            End If
            If txtMCC.arrValueMember IsNot Nothing AndAlso txtMCC.arrValueMember.Count > 0 Then
                BaseQry += " and TSPL_MCC_DISPATCH_CHALLAN.MCC_Code in (" + clsCommon.GetMulcallString(txtMCC.arrValueMember) + ") " + Environment.NewLine
            End If
            If ToMccORPlant.arrValueMember IsNot Nothing AndAlso ToMccORPlant.arrValueMember.Count > 0 Then
                BaseQry += " and TSPL_MCC_DISPATCH_CHALLAN.Mcc_Or_Plant_Code in (" + clsCommon.GetMulcallString(ToMccORPlant.arrValueMember) + ") " + Environment.NewLine
            End If

            If txtVendorNo.arrValueMember IsNot Nothing AndAlso txtVendorNo.arrValueMember.Count > 0 Then
                BaseQry += " and TSPL_VENDOR_INVOICE_HEAD.Vendor_Code in (" + clsCommon.GetMulcallString(txtVendorNo.arrValueMember) + ") " + Environment.NewLine
            End If


            FinalQry = "select Deduction_Code,max(DedName) as DedName from (" + BaseQry + " )xx group by Deduction_Code"
            Dim arrDed As Dictionary(Of String, String) = Nothing
            Dim dtColumn As DataTable = clsDBFuncationality.GetDataTable(FinalQry)
            Dim strColumn As String = ""
            Dim strColumnWithNull As String = ""
            Dim strColRange As String = ""
            If dtColumn IsNot Nothing AndAlso dtColumn.Rows.Count > 0 Then
                arrDed = New Dictionary(Of String, String)
                Dim isFirstTime As Boolean = True
                For Each dr As DataRow In dtColumn.Rows
                    If Not isFirstTime Then
                        strColumn += ","
                        strColRange += ","
                    End If
                    strColumn += "[" + clsCommon.myCstr(dr("Deduction_Code")) + "]"
                    strColumnWithNull += " + IsNull([" + clsCommon.myCstr(dr("Deduction_Code")) + "],0)"
                    arrDed.Add(clsCommon.myCstr(dr("Deduction_Code")), clsCommon.myCstr(dr("DedName")))
                    strColRange += ("SUM(" + "IsNull([" + clsCommon.myCstr(dr("Deduction_Code")) + "],0)" + ") AS " + "[" + clsCommon.myCstr(dr("Deduction_Code")) + "]")
                    isFirstTime = False
                Next
            End If

            FinalQry = "With a as(select TSPL_MCC_Dispatch_Challan.Chalan_NO,Convert(varchar,TSPL_MCC_Dispatch_Challan.Dispatch_Date,103) As Dispatch_Date_view ,  TSPL_MCC_Dispatch_Challan.Dispatch_Date as Dispatch_Date,  " + Environment.NewLine + _
            " Convert(varchar,Tspl_Gate_Entry_Details.Date_And_Time,103) As ReceivingDate, TSPL_MCC_Dispatch_Challan.MCC_Code" + Environment.NewLine + _
            " , TSPL_LOCATION_MASTER_MCC.Location_Desc As [Mcc Name], TSPL_MCC_Dispatch_Challan.Mcc_Or_Plant_Code As [Plant Code], TSPL_LOCATION_MASTER_Plant.Location_Desc As [Plant Name], " + Environment.NewLine + _
            " TSPL_MCC_Dispatch_Challan.Tanker_No,IsNull(TSPL_MCC_Dispatch_Challan.Net_Qty, 0) As [Dispatch Qty], IsNull(TSPL_Weighment_Detail.Net_Weight, 0) As [Receiving Qty]," + Environment.NewLine + _
            " TSPL_TANKER_MASTER.Tanker_Transporter_Code as [Transporter Code],TSPL_MCC_Dispatch_Challan.Tanker_Transporter_Name As [Transporter Name],Distance.Distance,TSPL_VENDOR_MASTER.Account_No ,TSPL_VENDOR_MASTER.IFSC_Code ,TSPL_MCC_Dispatch_Challan.Payment_Type,TSPL_MCC_Dispatch_Challan.Payment_Rate as Payment_Rate,IsNull(TSPL_MCC_Dispatch_Challan.Payment_Amount,0) As [Freight Amount]" + Environment.NewLine
            If clsCommon.myLen(strColumn) > 0 Then
                FinalQry += ", " + strColumn + "" + Environment.NewLine
            End If

            FinalQry += " ,(IsNull(TSPL_MCC_Dispatch_Challan.Payment_Amount,0)" + strColumnWithNull + ") as BillAmt " + Environment.NewLine + _
            " from TSPL_MCC_Dispatch_Challan" + Environment.NewLine
            If clsCommon.myLen(strColumn) > 0 Then
                FinalQry += " left outer join  ( select * from (" + Environment.NewLine + _
                " select  DC_Challan_No,Deduction_Code,sum(Amount) as Amount  " + Environment.NewLine + _
                " from (" + BaseQry + ")x group by DC_Challan_No,Deduction_Code" + Environment.NewLine + _
                " ) src" + Environment.NewLine + _
                " Pivot" + Environment.NewLine + _
                " (sum(amount) for Deduction_Code in (" + strColumn + ")) piv) xxx on xxx.DC_Challan_No=TSPL_MCC_Dispatch_Challan.Chalan_NO" + Environment.NewLine
            End If
            FinalQry += " Left outer Join TSPL_LOCATION_MASTER As TSPL_LOCATION_MASTER_MCC On TSPL_LOCATION_MASTER_MCC.Location_Code = TSPL_MCC_Dispatch_Challan.MCC_Code " + Environment.NewLine + _
            " Left outer Join TSPL_LOCATION_MASTER As TSPL_LOCATION_MASTER_Plant On TSPL_LOCATION_MASTER_Plant.Location_Code = TSPL_MCC_Dispatch_Challan.Mcc_Or_Plant_Code " + Environment.NewLine + _
            " Left outer Join Tspl_Gate_Entry_Details On Tspl_Gate_Entry_Details.Challan_No = TSPL_MCC_Dispatch_Challan.Chalan_NO " + Environment.NewLine + _
            " Left Join TSPL_Weighment_Detail On TSPL_Weighment_Detail.Challan_No = TSPL_MCC_Dispatch_Challan.Chalan_NO " + Environment.NewLine + _
            " Left outer Join TSPL_TANKER_MASTER On TSPL_TANKER_MASTER.Tanker_No = TSPL_MCC_Dispatch_Challan.Tanker_No  " + Environment.NewLine + _
            " left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code =TSPL_TANKER_MASTER.Tanker_Transporter_Code " + Environment.NewLine + _
            " left join (select  tspl_location_distance_master.From_Location_code ,tspl_location_distance_master.to_Location_Code ,max(tspl_location_distance_master.Distance ) as Distance from tspl_location_distance_master group by  tspl_location_distance_master.From_Location_code ,tspl_location_distance_master.to_Location_Code) as Distance on Distance.From_Location_code=TSPL_MCC_Dispatch_Challan.MCC_Code and TSPL_MCC_Dispatch_Challan.Mcc_Or_Plant_Code=Distance.to_Location_Code " + Environment.NewLine + _
            " where TSPL_MCC_DISPATCH_CHALLAN.isPosted = 1  " + Environment.NewLine + _
            " and TSPL_MCC_DISPATCH_CHALLAN.Dispatch_Date>='" + FromDate + "' and TSPL_MCC_DISPATCH_CHALLAN.Dispatch_Date<='" + ToDate + "'"
            If txtTankerNo.arrValueMember IsNot Nothing AndAlso txtTankerNo.arrValueMember.Count > 0 Then
                FinalQry += " and TSPL_MCC_DISPATCH_CHALLAN.Tanker_No in (" + clsCommon.GetMulcallString(txtTankerNo.arrValueMember) + ")" + Environment.NewLine
            End If
            If txtMCC.arrValueMember IsNot Nothing AndAlso txtMCC.arrValueMember.Count > 0 Then
                FinalQry += " and TSPL_MCC_DISPATCH_CHALLAN.MCC_Code in (" + clsCommon.GetMulcallString(txtMCC.arrValueMember) + ") " + Environment.NewLine
            End If
            If ToMccORPlant.arrValueMember IsNot Nothing AndAlso ToMccORPlant.arrValueMember.Count > 0 Then
                FinalQry += " and TSPL_MCC_DISPATCH_CHALLAN.Mcc_Or_Plant_Code in (" + clsCommon.GetMulcallString(ToMccORPlant.arrValueMember) + ") " + Environment.NewLine
            End If
            If txtVendorNo.arrValueMember IsNot Nothing AndAlso txtVendorNo.arrValueMember.Count > 0 Then
                FinalQry += " and TSPL_TANKER_MASTER.Tanker_Transporter_Code in (" + clsCommon.GetMulcallString(txtVendorNo.arrValueMember) + ") " + Environment.NewLine
            End If
            FinalQry += ") "
            FinalQry += "  SELECT "
            If clsCommon.CompairString(clsCommon.myCstr(cbReportType.SelectedItem.Value), "Detail") = CompairStringResult.Equal Then
                FinalQry += " * "
            Else
                '  FinalQry += "  [Transporter Code],[Transporter Name],[Dispatch Qty]=round(sum([Dispatch Qty]),2),[Receiving Qty]=round(sum([Receiving Qty]),2),Distance=sum(Distance),[Freight Amount]=round(sum([Freight Amount]),2)" + (IIf((clsCommon.myLen(strColumn) > 0), ("," + strColRange), "")) + ",BillAmt=round(sum(BillAmt ),2)  "
                FinalQry += " [From Date]='" + clsCommon.GetPrintDate(FromDate, "dd/MM/yyyy") + "', [To Date]='" + clsCommon.GetPrintDate(ToDate, "dd/MM/yyyy") + "', [Transporter Code],[Transporter Name],MCC_Code,[Mcc Name],[Dispatch Qty]=round(sum([Dispatch Qty]),2),[Receiving Qty]=round(sum([Receiving Qty]),2),Distance=sum(Distance),[Account_No],IFSC_Code,[Freight Amount]=round(sum([Freight Amount]),2) " + (IIf((clsCommon.myLen(strColumn) > 0), ("," + strColRange), "")) + ",BillAmt=round(sum(BillAmt ),2)  "
            End If
            FinalQry += "FROM A "
            If clsCommon.CompairString(clsCommon.myCstr(cbReportType.SelectedItem.Value), "Summary") = CompairStringResult.Equal Then
                FinalQry += " group by  [Transporter Code],[Transporter Name],MCC_Code,[Mcc Name],[Account_No],IFSC_Code " + (IIf((clsCommon.myLen(strColumn) > 0), ("," + strColumn), ""))
            End If




            dt = clsDBFuncationality.GetDataTable(FinalQry)
            clsCommon.ProgressBarHide()
            SetGridFormation(arrDed)

        Catch ex As Exception
            clsCommon.ProgressBarHide()
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub SetGridFormation(ByVal arrColumn As Dictionary(Of String, String))
        gv1.DataSource = Nothing
        gv1.Columns.Clear()
        gv1.Rows.Clear()
        gv1.DataSource = dt
        RadPageView1.SelectedPage = RadPageViewPage2
        For ii As Integer = 0 To gv1.Columns.Count - 1
            gv1.Columns(ii).ReadOnly = True
            gv1.Columns(ii).IsVisible = False
        Next
        If clsCommon.CompairString(clsCommon.myCstr(cbReportType.SelectedItem.Value), "Detail") = CompairStringResult.Equal Then
            gv1.Columns("Chalan_NO").IsVisible = True
            gv1.Columns("Chalan_NO").Width = 150
            gv1.Columns("Chalan_NO").HeaderText = "Dispatch No"

            gv1.Columns("Dispatch_Date").IsVisible = False
            gv1.Columns("Dispatch_Date").Width = 100
            gv1.Columns("Dispatch_Date").HeaderText = "DispatchDate"

            gv1.Columns("Dispatch_Date_view").IsVisible = True
            gv1.Columns("Dispatch_Date_view").Width = 100
            gv1.Columns("Dispatch_Date_view").HeaderText = "Dispatch Date"

            gv1.Columns("ReceivingDate").IsVisible = True
            gv1.Columns("ReceivingDate").Width = 100
            gv1.Columns("ReceivingDate").HeaderText = "Receiving Date"

            gv1.Columns("Plant Code").IsVisible = True
            gv1.Columns("Plant Code").Width = 100
            gv1.Columns("Plant Code").HeaderText = "Plant Code"

            gv1.Columns("Plant Name").IsVisible = True
            gv1.Columns("Plant Name").Width = 200
            gv1.Columns("Plant Name").HeaderText = "Plant"

            gv1.Columns("Tanker_No").IsVisible = True
            gv1.Columns("Tanker_No").Width = 100
            gv1.Columns("Tanker_No").HeaderText = "Tanker No"

            gv1.Columns("Payment_Rate").IsVisible = True
            gv1.Columns("Payment_Rate").Width = 100
            gv1.Columns("Payment_Rate").HeaderText = "Payment Rate"


            gv1.Columns("Payment_Type").IsVisible = True
            gv1.Columns("Payment_Type").Width = 100
            gv1.Columns("Payment_Type").HeaderText = "Payment Type"
        Else
            gv1.Columns("From Date").IsVisible = True
            gv1.Columns("From Date").Width = 100
            gv1.Columns("From Date").HeaderText = "From Date"

            gv1.Columns("To Date").IsVisible = True
            gv1.Columns("To Date").Width = 100
            gv1.Columns("To Date").HeaderText = "To Date"
        End If

        gv1.Columns("MCC_Code").IsVisible = True
        gv1.Columns("MCC_Code").Width = 100
        gv1.Columns("MCC_Code").HeaderText = "MCC Code"

        gv1.Columns("Mcc Name").IsVisible = True
        gv1.Columns("Mcc Name").Width = 200
        gv1.Columns("Mcc Name").HeaderText = "Mcc"

       


        gv1.Columns("Account_No").IsVisible = True
        gv1.Columns("Account_No").Width = 100
        gv1.Columns("Account_No").HeaderText = "Account No"

        gv1.Columns("IFSC_Code").IsVisible = True
        gv1.Columns("IFSC_Code").Width = 100
        gv1.Columns("IFSC_Code").HeaderText = "IFSC Code"

        gv1.Columns("Transporter Code").IsVisible = True
        gv1.Columns("Transporter Code").Width = 100
        gv1.Columns("Transporter Code").HeaderText = "Transporter Code"

        gv1.Columns("Transporter Name").IsVisible = True
        gv1.Columns("Transporter Name").Width = 100
        gv1.Columns("Transporter Name").HeaderText = "Transporter Name"

        gv1.Columns("Dispatch Qty").IsVisible = True
        gv1.Columns("Dispatch Qty").Width = 100
        gv1.Columns("Dispatch Qty").HeaderText = "Dispatch Qty"

        gv1.Columns("Receiving Qty").IsVisible = True
        gv1.Columns("Receiving Qty").Width = 100
        gv1.Columns("Receiving Qty").HeaderText = "Receiving Qty"

        gv1.Columns("Distance").IsVisible = True
        gv1.Columns("Distance").Width = 100
        gv1.Columns("Distance").HeaderText = "Distance"

      

        gv1.Columns("Freight Amount").IsVisible = True
        gv1.Columns("Freight Amount").Width = 100
        gv1.Columns("Freight Amount").HeaderText = "Freight Amount"

        gv1.Columns("BillAmt").IsVisible = True
        gv1.Columns("BillAmt").Width = 100
        gv1.Columns("BillAmt").HeaderText = "Bill Amt"



        gv1.MasterTemplate.ExpandAllGroups()
        gv1.ShowGroupPanel = False
        gv1.MasterTemplate.AutoExpandGroups = True
        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim item1 As New GridViewSummaryItem("Freight Amount", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)
        item1 = New GridViewSummaryItem("BillAmt", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)
        If arrColumn IsNot Nothing AndAlso arrColumn.Count > 0 Then
            For Each str As String In arrColumn.Keys
                gv1.Columns(str).IsVisible = True
                gv1.Columns(str).Width = 100
                gv1.Columns(str).HeaderText = arrColumn(str)

                item1 = New GridViewSummaryItem(str, "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item1)
            Next
        End If
        gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        EnableDisableControls(False)
    End Sub

    Private Sub EnableDisableControls(ByVal Val As Boolean)
        txtFromDate.Enabled = Val
        txtToDate.Enabled = Val
        txtMCC.Enabled = Val
        txtTankerNo.Enabled = Val
        ToMccORPlant.Enabled = Val
        txtVendorNo.Enabled = Val
        cbReportType.Enabled = Val
    End Sub


    Private Sub gv1_ViewCellFormatting(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CellFormattingEventArgs) Handles gv1.ViewCellFormatting
        If TypeOf e.CellElement Is Telerik.WinControls.UI.GridSummaryCellElement Then
            e.CellElement.TextAlignment = ContentAlignment.MiddleRight
        End If
    End Sub



    Private Sub txtMCC__My_Click(sender As Object, e As EventArgs) Handles txtMCC._My_Click
        Try
            Dim qry As String = "select MCC_CODE,MCC_NAME from TSPL_MCC_MASTER"
            txtMCC.arrValueMember = clsCommon.ShowMultipleSelectForm("tdwdmcc", qry, "MCC_CODE", "MCC_NAME", txtMCC.arrValueMember, txtMCC.arrDispalyMember)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtTankerNo__My_Click(sender As Object, e As EventArgs) Handles txtTankerNo._My_Click
        Try
            Dim qry As String = "select Tanker_No,Tanker_Transporter_Code,Description from tspl_Tanker_master"
            txtTankerNo.arrValueMember = clsCommon.ShowMultipleSelectForm("tdwdtanker", qry, "Tanker_No", "", txtTankerNo.arrValueMember, Nothing)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub


    Private Sub ToMccORPlant__My_Click(sender As Object, e As EventArgs) Handles ToMccORPlant._My_Click
        Try
            Dim qry As String = "select Location_Code as Code,Location_Desc as Name from TSPL_LOCATION_MASTER where (Location_Category='MCC' or Type='PLANT')"
            ToMccORPlant.arrValueMember = clsCommon.ShowMultipleSelectForm("tdwdmcc", qry, "Code", "Name", ToMccORPlant.arrValueMember, ToMccORPlant.arrDispalyMember)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtVendorNo__My_Click(sender As Object, e As EventArgs) Handles txtVendorNo._My_Click
        Try
            Dim qry As String = "Select Vendor_Code as Code,Vendor_Name as Name from TSPL_VENDOR_MASTER where isnull(Form_Type,'') ='TTM' and Status='N' "
            txtVendorNo.arrValueMember = clsCommon.ShowMultipleSelectForm("tdwdvendor", qry, "Code", "", txtVendorNo.arrValueMember, Nothing)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub rmSaveLayout_Click(sender As Object, e As EventArgs) Handles rmSaveLayout.Click
        If clsCommon.myLen(PageSetupReport_ID) > 0 Then
            gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = PageSetupReport_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv1.SaveLayout(obj.GridLayout)
            obj.GridColumns = gv1.ColumnCount
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully", "Information", Me.Text)
            End If
            ''stuti regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(PageSetupReport_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", "Information", Me.Text)
    End Sub

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        Print(EnumExportTo.Excel)
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        Print(EnumExportTo.PDF)
    End Sub

    Private Sub Print(ByVal exporter As EnumExportTo)
        Try
            If gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy"))
                If txtMCC.arrValueMember IsNot Nothing AndAlso txtMCC.arrValueMember.Count > 0 Then
                    arrHeader.Add("From MCC : " + clsCommon.GetMulcallStringWithComma(txtMCC.arrValueMember))
                End If
                If ToMccORPlant.arrValueMember IsNot Nothing AndAlso ToMccORPlant.arrValueMember.Count > 0 Then
                    arrHeader.Add("To Plant/MCC : " + clsCommon.GetMulcallStringWithComma(ToMccORPlant.arrValueMember))
                End If
                If txtTankerNo.arrValueMember IsNot Nothing AndAlso txtTankerNo.arrValueMember.Count > 0 Then
                    arrHeader.Add("Tanker No : " + clsCommon.GetMulcallStringWithComma(txtTankerNo.arrValueMember))
                End If
                If txtVendorNo.arrValueMember IsNot Nothing AndAlso txtVendorNo.arrValueMember.Count > 0 Then
                    arrHeader.Add("Vendor No : " + clsCommon.GetMulcallStringWithComma(txtVendorNo.arrValueMember))
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
                    transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
                    transportSql.QuickExportToExcel(gv1, "", Me.Text, , arrHeader)
                    'transportSql.exportdataChilRows(gv1, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader) 'frm.Text)
                    ' '''''''''''''''---------------------
                    'common.clsCommon.MyMessageBoxShow("Exported Successfully.")
                    'Process.Start(filePath)
                Else
                    transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
                    clsCommon.MyExportToPDF(Me.Text, gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
                End If
            Else
                common.clsCommon.MyMessageBoxShow(Me, "No Data Found to Export.", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

End Class
