Imports common
Imports System.IO
Imports XpertERPEngine
Imports Telerik.WinControls.UI
Imports common.UserControls
Public Class RptDcsPaymentReport
    Inherits FrmMainTranScreen

    Private Sub Reset()
        'txtMultBmc.Enabled=True
        txtMultRoute.arrValueMember = Nothing
        txtMultBmc.arrValueMember = Nothing
        txtMultDCS.arrValueMember = Nothing
        fromDate.Enabled = True
        dtpToDate.Enabled = True
        Gv1.DataSource = Nothing
        Gv1.Rows.Clear()
        Gv1.Columns.Clear()
        RadPageView1.SelectedPage = RadPageViewPage1
        btnGo.Enabled = True
        ControlEnableDisable(True)
    End Sub
    Sub ControlEnableDisable(ByVal isEnable As Boolean)
        fromDate.Enabled = isEnable
        dtpToDate.Enabled = isEnable
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        LoadData
    End Sub

    Public Sub LoadData()
        Try

            Dim dt As New DataTable
            Dim strQry As String = "select 
TSPL_PAYMENT_PROCESS_DETAIL.SNo  ,TSPL_PAYMENT_PROCESS_DETAIL.AP_Invoice_Date,TSPL_PAYMENT_PROCESS_DETAIL.Payee_Joint_Bank_Code
,TSPL_PAYMENT_PROCESS_DETAIL.Payee_Joint_Bank_Name
,TSPL_VLC_MASTER_HEAD.Route_Code as Route
,TSPL_MCC_MASTER.MCC_NAME as Bmc_name 
,TSPL_PAYMENT_PROCESS_DETAIL.VLC_CODE_Uploader  as Dcs_Uploader
,TSPL_PAYMENT_PROCESS_DETAIL.VLC_Name as Dcs_name
,TSPL_PAYMENT_PROCESS_DETAIL.Milk_Qty 
,TabFATSNFDetail.FATKg
,TabFATSNFDetail.SNFKg
,TSPL_PAYMENT_PROCESS_DETAIL.Milk_Amount
,TSPL_PAYMENT_PROCESS_DETAIL.Head_Load_Amount
,TSPL_PAYMENT_PROCESS_DETAIL.Reduce_Deduc_Amt
,TSPL_PAYMENT_PROCESS_DETAIL.Deduction_Amount
,TSPL_PAYMENT_PROCESS_DETAIL.Credit_Note_Amount 
,TSPL_PAYMENT_PROCESS_DETAIL.Payable_Amount 
from   TSPL_PAYMENT_PROCESS_DETAIL 
                  left outer join (select DOC_CODE,cast( sum(FATKg) as decimal(18,3)) as FATKg,cast(case when sum(ACC_Qty)=0 then 0 else sum(FATKg)*100/sum(ACC_Qty) end as decimal(18,2) ) as FATPer ,cast( sum(SNFKg) as decimal(18,3)) as SNFKg,cast(case when sum(ACC_Qty)=0 then 0 else sum(SNFKg)*100/sum(ACC_Qty) end as decimal(18,2) ) as SNFPer 
                  from (select DOC_CODE, ACC_Qty,FAT_PER,SNF_PER,cast(ACC_Qty*FAT_PER/100 as decimal(18,2)) as FATKg, cast(ACC_Qty * SNF_PER / 100 As Decimal(18,2)) As SNFKg from TSPL_MILK_PURCHASE_INVOICE_DETAIL )xx group by DOC_CODE 
                ) As TabFATSNFDetail On TabFATSNFDetail.DOC_CODE= TSPL_PAYMENT_PROCESS_DETAIL.Milk_Purchase_Invoice_No 
				left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_CODE=TSPL_PAYMENT_PROCESS_DETAIL.VSP_Code
				left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code=TSPL_PAYMENT_PROCESS_DETAIL.MCC_Code"
            'where TSPL_PAYMENT_PROCESS_DETAIL.Doc_No ='PP/2324/000002' order by TSPL_PAYMENT_PROCESS_DETAIL.SNo"
            dt = clsDBFuncationality.GetDataTable(strQry)
            Gv1.DataSource = Nothing
            Gv1.Rows.Clear()
            Gv1.Columns.Clear()
            Gv1.GroupDescriptors.Clear()
            Gv1.MasterTemplate.SummaryRowsBottom.Clear()
            Gv1.MasterView.Refresh()

            If dt Is Nothing OrElse dt.Rows.Count > 0 Then
                Gv1.DataSource = dt

                RadPageView1.SelectedPage = RadPageViewPage2

                Gv1.EnableFiltering = True
                FormatGrid()
                ControlEnableDisable(False)
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
            End If

            Gv1.BestFitColumns()

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub


    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        Try
            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(dtpToDate.Value, "dd/MM/yyyy")) + " ")
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptMobileAppMilkCollection & "'"))
            transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
            clsCommon.MyExportToExcelGrid(Me.Text, Gv1, arrHeader, Me.Text)
            common.clsCommon.MyMessageBoxShow(Me, "Exported Successfully.", Me.Text)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)

        End Try
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        Try
            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(dtpToDate.Value, "dd/MM/yyyy")) + " ")
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptMobileAppMilkCollection & "'"))

            transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
            clsCommon.MyExportToPDF(Me.Text, Gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)

        End Try
    End Sub



    Sub FormatGrid()
        Gv1.AutoExpandGroups = False
        Gv1.ShowGroupPanel = False
        Gv1.ShowRowHeaderColumn = False
        Gv1.AllowAddNewRow = False
        Gv1.AllowDeleteRow = False
        Gv1.EnableFiltering = True
        Gv1.ShowFilteringRow = True


        For ii As Integer = 0 To Gv1.Columns.Count - 1
            Gv1.Columns(ii).ReadOnly = True
            Gv1.Columns(ii).BestFit()
        Next
        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim intCount As Integer = 0

        Dim item1 As New GridViewSummaryItem("Milk_Qty", "", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)
        Dim item2 As New GridViewSummaryItem("FATKg", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item2)
        Dim item3 As New GridViewSummaryItem("SNFKg", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item3)
        Dim item4 As New GridViewSummaryItem("Milk_Amount", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item4)
        Dim item5 As New GridViewSummaryItem("Head_Load_Amount", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item5)
        Dim item6 As New GridViewSummaryItem("Reduce_Deduc_Amt", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item6)
        Dim item7 As New GridViewSummaryItem("Deduction_Amount", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item7)
        Dim item8 As New GridViewSummaryItem("Credit_Note_Amount", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item8)
        Dim item9 As New GridViewSummaryItem("Payable_Amount", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item9)

        Gv1.ShowGroupPanel = True
        Gv1.MasterTemplate.AutoExpandGroups = True
        Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        Gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
    End Sub

    Private Sub rptDcsPaymentReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        fromDate.Value = DateTime.Now()
        dtpToDate.Value = DateTime.Now()
    End Sub
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Reset()
    End Sub
    Private Sub txtMultRoute__My_Click(sender As Object, e As EventArgs) Handles txtMultRoute._My_Click
        'txtRoute.arrValueMember = clsCommon.ShowMultipleSelectForm("PCURoute", qry, "Route_Code", "Route_Name", txtRoute.arrValueMember, txtRoute.arrDispalyMember)
        Dim qry As String = " select TSPL_ROUTE_MASTER.Route_No from TSPL_ROUTE_MASTER where 2=2   "
        txtMultRoute.arrValueMember = clsCommon.ShowMultipleSelectForm("VSPMulSelect", qry, "Route_no", "", txtMultRoute.arrValueMember, txtMultRoute.arrDispalyMember)
    End Sub
    Private Sub txtMultBmc__My_Click(sender As Object, e As EventArgs) Handles txtMultBmc._My_Click
        Dim qry As String = " select TSPL_MCC_MASTER.MCC_NAME from TSPL_MCC_MASTER where 2=2  "
        txtMultBmc.arrValueMember = clsCommon.ShowMultipleSelectForm("VSPMulSelect", qry, "MCC_NAME", "", txtMultBmc.arrValueMember, txtMultBmc.arrDispalyMember)
    End Sub

    Private Sub txtMultDCS__My_Click(sender As Object, e As EventArgs) Handles txtMultDCS._My_Click
        Dim qry As String = " select VLC_CODE_Uploader as Code,VLC_Name as Name,TSPL_VENDOR_MASTER.Zone_Code from TSPL_PAYMENT_PROCESS_DETAIL 
                             left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code = TSPL_PAYMENT_PROCESS_DETAIL.VSP_CODE where 2=2 "
        txtMultDCS.arrValueMember = clsCommon.ShowMultipleSelectForm("VSPMulSelect", qry, "Code", "Name", txtMultDCS.arrValueMember, txtMultDCS.arrDispalyMember)
    End Sub
End Class
