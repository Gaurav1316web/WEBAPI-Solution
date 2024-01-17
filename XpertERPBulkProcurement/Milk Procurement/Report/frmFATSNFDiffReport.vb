Imports common
Imports System.ComponentModel
Imports System.IO

Public Class frmFATSNFDiffReport
    Inherits FrmMainTranScreen

    Dim dt As DataTable = Nothing
    Dim arr As New Dictionary(Of Integer, DataRow)
    Dim SettCalculateFATSNFLossByCycleWise As Boolean = False

    'Dim ApplyZoneWiseVSP As Boolean = False
    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnExp.Visible = MyBase.isExport
    End Sub
    Private Sub rptMilkBillProcurementSummary_Load(sender As Object, e As EventArgs) Handles Me.Load
        SettCalculateFATSNFLossByCycleWise = (clsCommon.myCDecimal(clsFixedParameter.GetData(clsFixedParameterType.CalculateFATSNFLossCycleWise, clsFixedParameterCode.CalculateFATSNFLossCycleWise, Nothing)) = 1)
        Reset()
        If SettCalculateFATSNFLossByCycleWise Then
            rbtnMCCWise.IsChecked = True
            GroupBox1.Visible = False
        End If
    End Sub

    Sub Reset()
        txtFromDate.Value = clsCommon.GETSERVERDATE()
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtToDate.ReadOnly = False
        btnGo.Enabled = True
        Gv1.DataSource = Nothing
        Gv1.Rows.Clear()
        Gv1.Columns.Clear()
        Gv1.MasterTemplate.SummaryRowsBottom.Clear()
        dt = Nothing
        arr = New Dictionary(Of Integer, DataRow)
        txtMCC.arrDispalyMember = Nothing
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub
    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Print(False)
    End Sub
    Sub Print(ByVal isPrint As Boolean)
        Try
            Gv1.MasterTemplate.SummaryRowsBottom.Clear()
            Gv1.DataSource = Nothing
            Gv1.Rows.Clear()
            Gv1.Columns.Clear()
            Gv1.GroupDescriptors.Clear()
            Gv1.MasterTemplate.SummaryRowsBottom.Clear()
            Gv1.MasterView.Refresh()


            PageSetupReport_ID = clsCommon.myCstr(MyBase.Form_ID)
            If rbtnMCCWise.IsChecked = True Then
                PageSetupReport_ID = PageSetupReport_ID + "_M"
            ElseIf rbtnDetails.IsChecked = True Then
                PageSetupReport_ID = PageSetupReport_ID + "_D"
            End If

            Dim BaseQry As String = clsMilkCollectionDCS.GetBaseQueryFATSNFGainLoss(txtFromDate.Value, txtToDate.Value, txtMCC.arrValueMember)
            Dim Qry As String = ""
            If rbtnDetails.IsChecked Then
                Qry = BaseQry + " order by Document_Date, MCC_NAME "
            ElseIf rbtnMCCWise.IsChecked Then
                Qry = "select MCC_Code,max(MCC_NAME) as MCC_NAME,sum(MCCQty) as MCCQty,convert(decimal(18,2),sum(MCCFATKG))  as MCCFATKG, CONVERT(decimal(18,2),sum(MCCSNFKG)) as MCCSNFKG ,sum(DCSQty) as DCSQty, CONVERT(decimal(18,2),sum(DCSFATKG))  as DCSFATKG,CONVERT(decimal(18,2),sum(DCSSNFKG)) as DCSSNFKG,CONVERT(decimal(18,2),sum(DiffFATKG)) as DiffFATKG, CONVERT(decimal(18,2), sum(DiffSNFKG)) as DiffSNFKG,sum(FatAmt) as FatAmt,sum(SNFAmt) as SNFAmt,sum(Amt) as Amt,max(FindCode) as FindCode from ( " + BaseQry + ")XX group by MCC_Code  "
                If SettCalculateFATSNFLossByCycleWise Then
                    Qry = "select xxx.MCC_Code,xxx.MCC_NAME,xxx.MCCQty,xxx.MCCFATKG,xxx.MCCSNFKG,xxx.DCSQty,xxx.DCSFATKG,xxx.DCSSNFKG,xxx.DiffFATKG,xxx.DiffSNFKG
,cast((case when xxx.DiffFATKG<0 then TSPL_OWN_BMC_GAIN_LOSS_RATE.Loss_FAT_Rate else TSPL_OWN_BMC_GAIN_LOSS_RATE.Gain_FAT_Rate end)*xxx.DiffFATKG as decimal(18,2)) as FatAmt 
,cast((case when xxx.DiffSNFKG<0 then TSPL_OWN_BMC_GAIN_LOSS_RATE.Loss_SNF_Rate else TSPL_OWN_BMC_GAIN_LOSS_RATE.Gain_SNF_Rate end)*xxx.DiffSNFKG as decimal(18,2)) as SNFAmt 
,cast((((case when xxx.DiffFATKG<0 then TSPL_OWN_BMC_GAIN_LOSS_RATE.Loss_FAT_Rate else TSPL_OWN_BMC_GAIN_LOSS_RATE.Gain_FAT_Rate end)*xxx.DiffFATKG) +((case when xxx.DiffSNFKG<0 then TSPL_OWN_BMC_GAIN_LOSS_RATE.Loss_SNF_Rate else TSPL_OWN_BMC_GAIN_LOSS_RATE.Gain_SNF_Rate end)*xxx.DiffSNFKG))as decimal(18,2)) as Amt
from (" + Qry + ")xxx left outer join TSPL_OWN_BMC_GAIN_LOSS_RATE on TSPL_OWN_BMC_GAIN_LOSS_RATE.Code=xxx.FindCode"
                End If
                Qry += " order by MCC_NAME"
            Else
                Throw New Exception("Wrong Method")
            End If
            dt = Nothing
            dt = clsDBFuncationality.GetDataTable(Qry)
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                Throw New Exception("No Data Found to Display")
            End If

            Gv1.DataSource = dt
            RadPageView1.SelectedPage = RadPageViewPage2
            SetGridFormat(Gv1)
            EnableDisaableControls(False)
            ReStoreGridLayout()

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub ReStoreGridLayout()
        Try
            '  If rbtnNEFT.IsChecked Then
            If clsCommon.myLen(PageSetupReport_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(PageSetupReport_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= Gv1.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To Gv1.Columns.Count - 1 Step ii + 1
                        Gv1.Columns(ii).IsVisible = False
                        Gv1.Columns(ii).VisibleInColumnChooser = True
                    Next
                    Gv1.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub

    Sub SetGridFormat(ByRef Gv1 As RadGridView)
        Gv1.ShowGroupPanel = True
        Gv1.ShowRowHeaderColumn = False
        Gv1.AllowAddNewRow = False
        Gv1.AllowDeleteRow = False
        Gv1.EnableFiltering = True
        Gv1.ShowFilteringRow = True
        Gv1.MasterTemplate.SummaryRowsBottom.Clear()

        For ii As Integer = 0 To Gv1.Columns.Count - 1
            Gv1.Columns(ii).IsVisible = True
            Gv1.Columns(ii).ReadOnly = True
            Gv1.Columns(ii).BestFit()
        Next
        If rbtnDetails.IsChecked Then
            Gv1.Columns("MCC_Code").HeaderText = "BMC Code"
            Gv1.Columns("MCC_Code").IsVisible = False

            Gv1.Columns("MCC_NAME").HeaderText = "BMC"
            Gv1.Columns("Document_Date").HeaderText = "Date"
            Gv1.Columns("MCCQty").HeaderText = "BMC Qty"
            Gv1.Columns("MCCFATKG").HeaderText = "BMC FAT KG"
            Gv1.Columns("MCCSNFKG").HeaderText = "BMC SNF KG"
            Gv1.Columns("DCSQty").HeaderText = "DCS Qty"
            Gv1.Columns("DCSFATKG").HeaderText = "DCS FAT KG"
            Gv1.Columns("DCSSNFKG").HeaderText = "DCS SNF KG"
            Gv1.Columns("DiffFATKG").HeaderText = "Diff FAT KG"
            Gv1.Columns("DiffSNFKG").HeaderText = "Diff SNF KG"
            Gv1.Columns("FatAmt").HeaderText = "FAT Amt"
            Gv1.Columns("SNFAmt").HeaderText = "SNF Amt"
            Gv1.Columns("Amt").HeaderText = "Amount"
        ElseIf rbtnMCCWise.IsChecked Then
            Gv1.Columns("MCC_Code").HeaderText = "BMC Code"
            Gv1.Columns("MCC_Code").IsVisible = False
            Gv1.Columns("MCC_NAME").HeaderText = "BMC"
            Gv1.Columns("MCCQty").HeaderText = "BMC Qty"
            Gv1.Columns("MCCFATKG").HeaderText = "BMC FAT KG"
            Gv1.Columns("MCCSNFKG").HeaderText = "BMC SNF KG"
            Gv1.Columns("DCSQty").HeaderText = "DCS Qty"
            Gv1.Columns("DCSFATKG").HeaderText = "DCS FAT KG"
            Gv1.Columns("DCSSNFKG").HeaderText = "DCS SNF KG"
            Gv1.Columns("DiffFATKG").HeaderText = "Diff FAT KG"
            Gv1.Columns("DiffSNFKG").HeaderText = "Diff SNF KG"
            Gv1.Columns("FatAmt").HeaderText = "FAT Amt"
            Gv1.Columns("SNFAmt").HeaderText = "SNF Amt"
            Gv1.Columns("Amt").HeaderText = "Amount"
        End If
        Gv1.AutoSizeRows = False
        Gv1.BestFitColumns()
        Gv1.MasterTemplate.AutoExpandGroups = True
    End Sub
    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        EnableDisaableControls(True)
    End Sub
    Sub EnableDisaableControls(ByVal flag As Boolean)
        txtMCC.Enabled = flag
        txtFromDate.Enabled = flag
        txtToDate.Enabled = flag
        'ddlType.Enabled = flag
        GroupBox1.Enabled = flag
        'txtBlock.Enabled = flag
        'If clsCommon.myLen(objCommonVar.strCurrUserZones) > 0 Then
        '    txtZone.Enabled = False
        'End If
        'txtRevenueVillage.Enabled = flag
        'txtGrampanchayat.Enabled = flag
        'txtPanchayatSamiti.Enabled = flag
        'txtVidhanSabha.Enabled = flag
        'txtDistrict.Enabled = flag
    End Sub
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
    Private Sub ExportGrid(ByVal exporter As EnumExportTo)
        Try
            If Gv1.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow("No Data Found to Export", Me.Text)
                Exit Sub
            End If
            Dim strHeading As String = "FAT SNF Differnece Report"

            Dim arrHeader As List(Of String) = New List(Of String)()
            If txtMCC.arrValueMember IsNot Nothing AndAlso txtMCC.arrValueMember.Count > 0 Then
                arrHeader.Add("Mcc : " + clsCommon.GetMulcallStringWithComma(txtMCC.arrValueMember))
            End If
            arrHeader.Add("Date Range from : " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy"))



            If exporter = EnumExportTo.Excel Then
                transportSql.QuickExportToExcel(Gv1, "", Me.Text, , arrHeader)
            Else
                clsCommon.MyExportToPDF(strHeading, Gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK)
        End Try
    End Sub

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        ExportGrid(EnumExportTo.Excel)
    End Sub
    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        ExportGrid(EnumExportTo.PDF)
    End Sub
    Private Sub RmiExcelGrid_Click(sender As Object, e As EventArgs) Handles rmiExcelGrid.Click
        Try
            If Gv1.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow("No Data Found to Export", Me.Text)
                Exit Sub
            End If

            Dim strHeading As String = clsCommon.myCstr("FAT SNF Differnece Report")

            Dim arrHeader As List(Of String) = New List(Of String)()
            If txtMCC.arrValueMember IsNot Nothing AndAlso txtMCC.arrValueMember.Count > 0 Then
                arrHeader.Add("Mcc : " + clsCommon.GetMulcallStringWithComma(txtMCC.arrValueMember))
            End If

            arrHeader.Add("Date Range from : " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy"))


            clsCommon.MyExportToExcelGrid(strHeading, Gv1, arrHeader, Me.Text, True)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK, Telerik.WinControls.RadMessageIcon.Error)
        End Try
    End Sub
    Private Sub TxtMultiSelectFinder1__My_Click(sender As Object, e As EventArgs) Handles txtMCC._My_Click
        Dim qry As String = "select MCC_Code as [MCC Code],MCC_NAME as [MCC Name],TSPL_MCC_MASTER.plant_code as [Plant Code],tspl_location_master.location_desc as [Plant Name] from TSPL_MCC_MASTER left join tspl_location_master on tspl_location_master.location_code=TSPL_MCC_MASTER.plant_code"
        txtMCC.arrValueMember = clsCommon.ShowMultipleSelectForm("MBPSMCC4", qry, "MCC Code", "MCC Name", txtMCC.arrValueMember, txtMCC.arrDispalyMember)
    End Sub




    Private Sub rmsaveLayout_Click(sender As Object, e As EventArgs) Handles rmsaveLayout.Click
        Try
            If clsCommon.myLen(PageSetupReport_ID) > 0 Then
                Gv1.MasterTemplate.FilterDescriptors.Clear()
                Dim obj As New clsGridLayout()
                obj.ReportID = PageSetupReport_ID
                obj.UserID = objCommonVar.CurrentUserCode
                obj.GridLayout = New MemoryStream()
                Gv1.SaveLayout(obj.GridLayout)
                obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                obj.GridColumns = Gv1.ColumnCount
                If obj.SaveData() Then
                    common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
                End If
                obj.GridLayout.Close()
                obj.GridLayout.Dispose()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        Try
            clsGridLayout.DeleteData(PageSetupReport_ID, objCommonVar.CurrentUserCode)
            common.clsCommon.MyMessageBoxShow("Layout Delete successfully", "Information")
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadButton1_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Print(True)
    End Sub

    Private Sub txtFromDate_Validating(sender As Object, e As CancelEventArgs) Handles txtFromDate.Validating
        SetToDate()
    End Sub

    Sub SetToDate()
        If SettCalculateFATSNFLossByCycleWise Then
            Dim PaymentCycleType As String = ""
            Dim PaymentCycleValue As Integer = 0
            Dim strMCC As String = ""
            If txtMCC.arrValueMember IsNot Nothing AndAlso txtMCC.arrValueMember.Count > 0 Then
                strMCC = txtMCC.arrValueMember(0)
            Else
                strMCC = clsDBFuncationality.getSingleValue("select top 1 MCC_Code from TSPL_MCC_MASTER where In_active=0")
            End If
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(" select TSPL_MCC_MASTER.Payment_Cycle,TSPL_PAYMENT_CYCLE_MASTER.PC_TYPE,TSPL_PAYMENT_CYCLE_MASTER.PC_VALUE  from TSPL_MCC_MASTER left outer join TSPL_PAYMENT_CYCLE_MASTER on TSPL_PAYMENT_CYCLE_MASTER.PC_CODE=TSPL_MCC_MASTER.Payment_Cycle   where TSPL_MCC_MASTER.MCC_Code ='" + strMCC + "' ")
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "No Payment Cycle found on current MCC/Location", Me.Text)
                Exit Sub
            End If
            PaymentCycleType = clsCommon.myCstr(dt.Rows(0)("PC_TYPE"))
            PaymentCycleValue = clsCommon.myCdbl(dt.Rows(0)("PC_VALUE"))
            Dim dtCurr As DateTime = clsCommon.GETSERVERDATE()
            If clsCommon.CompairString(PaymentCycleType, "Day") = CompairStringResult.Equal Then
                If txtFromDate.Value.Day Mod PaymentCycleValue <> 1 And (Not PaymentCycleValue = 1) Then
                    clsCommon.MyMessageBoxShow(Me, "Date can only be first day of month or at interval of " & PaymentCycleValue & " Day, Because MCC has payment Cycle of " & PaymentCycleValue & " Day ")
                    txtFromDate.Value = New Date(dtCurr.Year, dtCurr.Month, 1)
                    txtToDate.Value = txtFromDate.Value
                    Exit Sub
                End If
                txtToDate.Value = txtFromDate.Value.AddDays(PaymentCycleValue - 1)

                If txtFromDate.Value.Month <> txtToDate.Value.Month Then
                    txtToDate.Value = New Date(txtFromDate.Value.Year, txtFromDate.Value.Month, 1).AddMonths(1).AddDays(-1)
                End If
                Dim dtNxtPay As DateTime = txtToDate.Value.AddDays(Math.Ceiling(PaymentCycleValue / 2.0))
                If txtFromDate.Value.Month <> dtNxtPay.Month Then
                    txtToDate.Value = New Date(txtFromDate.Value.Year, txtFromDate.Value.Month, 1).AddMonths(1).AddDays(-1)
                End If
            ElseIf clsCommon.CompairString(PaymentCycleType, "Month") = CompairStringResult.Equal Then
                If clsCommon.myCdbl(clsCommon.GetPrintDate(txtFromDate.Value, "dd")) <> 1 Then
                    clsCommon.MyMessageBoxShow(Me, "Date can only be first day of month, Because MCC has payment Cycle of Month Type", Me.Text)
                    txtFromDate.Value = "01/" & DatePart(DateInterval.Month, dtCurr) & "/" & DatePart(DateInterval.Year, dtCurr)
                    txtToDate.Value = "01/" & DatePart(DateInterval.Month, dtCurr) & "/" & DatePart(DateInterval.Year, dtCurr)
                    Exit Sub
                End If
                txtToDate.Value = DateAdd(DateInterval.Month, PaymentCycleValue, txtFromDate.Value)
            ElseIf clsCommon.CompairString(PaymentCycleType, "Year") = CompairStringResult.Equal Then
                If clsCommon.myCdbl(clsCommon.GetPrintDate(txtFromDate.Value, "dd")) <> 1 Then
                    clsCommon.MyMessageBoxShow(Me, "Date can only be first day of month, Because MCC has payment Cycle of Year Type", Me.Text)
                    txtFromDate.Value = "01/" & DatePart(DateInterval.Month, dtCurr) & "/" & DatePart(DateInterval.Year, dtCurr)
                    txtToDate.Value = "01/" & DatePart(DateInterval.Month, dtCurr) & "/" & DatePart(DateInterval.Year, dtCurr)
                    Exit Sub
                End If
                txtToDate.Value = DateAdd(DateInterval.Year, PaymentCycleValue, txtFromDate.Value)
            End If
            ' End If
        End If
    End Sub
End Class
