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
            Dim strQry As String = " Select xy.SNo,xy.DocDate,xy.Milk_Purchase_Invoice_Date,xy.Dcs_Uploader,xy.Dcs_name,xy.Milk_Qty,
                                    xy.FATKg,xy.SNFKg,xy.Milk_Amount,xy.Head_Load_Amount,case when xy.PEAmt=0 then xy.CRAmt  else xy.PEAmt end as PEAmt,xy.OBEAmt,
                                    xy.CRAmt,xy.Reduce_Deduc_Amt,xy.Deduction_Amount,xy.PURCHASEEXPENSE,xy.AMTS,xy.Payable_Amount,xy.Saving_Amount,xy.TotalAmt,xy.CRAmts from
                                    (select max(x.SNo)SNo ,(format(max(Doc_Date), 'dd-MM-yyyy'))DocDate,(format(max(Milk_Purchase_Invoice_Date), 'dd-MM-yyyy'))Milk_Purchase_Invoice_Date ,max(x.Dcs_Uploader)Dcs_Uploader,max(x.Dcs_name)Dcs_name,max(x.Milk_Qty)Milk_Qty
                                    ,max(x.FATKg)FATKg,max(x.SNFKg)SNFKg,max(x.Milk_Amount)Milk_Amount,max(x.Head_Load_Amount)Head_Load_Amount
                                    ,max(isnull(x.CRAmt,0)+ isnull(x.PURCHASEEXPENSE,0)) as PEAmt,max(isnull(x.OBEAmt,0))OBEAmt,max(x.Credit_Note_Amount)CRAmt,max(x.Reduce_Deduc_Amt)Reduce_Deduc_Amt
                                    ,max(x.Deduction_Amount)Deduction_Amount,isnull(max(x.PURCHASEEXPENSE),0)PURCHASEEXPENSE
									,Case When max(isOwnBMC)=1 then sum(x.PURCHASEEXPENSE) else max(isnull(x.Credit_Note_Amount,0)+ isnull(x.PURCHASEEXPENSE,0)) end as AMTS
									,max(x.Payable_Amount)Payable_Amount ,isnull(max(x.Saving_Amount),0)Saving_Amount,max(isnull(x.Payable_Amount,0) + isnull(x.Saving_Amount,0)) as TotalAmt
									,sum(isnull(x.CRAmt,0))CRAmts
                                    from  
                                    (select TSPL_PAYMENT_PROCESS_DETAIL.SNo ,TSPL_PAYMENT_PROCESS_HEAD.Doc_Date,TSPL_PAYMENT_PROCESS_DETAIL.Milk_Purchase_Invoice_Date ,TSPL_PAYMENT_PROCESS_DETAIL.AP_Invoice_Date,TSPL_PAYMENT_PROCESS_DETAIL.AP_Invoice_No,TSPL_PAYMENT_PROCESS_DETAIL.Payee_Joint_Bank_Code
                                    ,TSPL_PAYMENT_PROCESS_DETAIL.Payee_Joint_Bank_Name,TSPL_VLC_MASTER_HEAD.Route_Code as Route,TSPL_MCC_MASTER.MCC_NAME as Bmc_name 
                                    ,TSPL_PAYMENT_PROCESS_DETAIL.VLC_CODE_Uploader  as Dcs_Uploader,TSPL_PAYMENT_PROCESS_DETAIL.VLC_Name as Dcs_name,TSPL_PAYMENT_PROCESS_DETAIL.Milk_Qty 
                                    ,TabFATSNFDetail.FATKg,TabFATSNFDetail.SNFKg,TSPL_PAYMENT_PROCESS_DETAIL.Milk_Amount,TSPL_PAYMENT_PROCESS_DETAIL.Head_Load_Amount
                                    ,TSPL_PAYMENT_PROCESS_DETAIL.Reduce_Deduc_Amt,TSPL_PAYMENT_PROCESS_DETAIL.Deduction_Amount,TSPL_PAYMENT_PROCESS_DETAIL.Credit_Note_Amount 
                                    ,TSPL_PAYMENT_PROCESS_DETAIL.Payable_Amount ,TSPL_PAYMENT_PROCESS_DETAIL.Saving_Amount ,TabDCSdrcr.PURCHASEEXPENSE,TSPL_VLC_MASTER_HEAD.isOwnBMC
									,TabDCSCreditNote.CRAmt as CRAmt,TabDCSCreditNote.OBEAmt as OBEAmt
                                    from   TSPL_PAYMENT_PROCESS_DETAIL
                                    Inner Join TSPL_PAYMENT_PROCESS_HEAD On TSPL_PAYMENT_PROCESS_HEAD.Doc_No =TSPL_PAYMENT_PROCESS_DETAIL.Doc_No
                                      left outer join (select DOC_CODE,cast( sum(FATKg) as decimal(18,3)) as FATKg,cast(case when sum(ACC_Qty)=0 then 0 else sum(FATKg)*100/sum(ACC_Qty) end as decimal(18,2) ) as FATPer ,cast( sum(SNFKg) as decimal(18,3)) as SNFKg,cast(case when sum(ACC_Qty)=0 then 0 else sum(SNFKg)*100/sum(ACC_Qty) end as decimal(18,2) ) as SNFPer
                                      from (select DOC_CODE, ACC_Qty,FAT_PER,SNF_PER,cast(ACC_Qty*FAT_PER/100 as decimal(18,2)) as FATKg, cast(ACC_Qty * SNF_PER / 100 As Decimal(18,2)) As SNFKg  
				                      from TSPL_MILK_PURCHASE_INVOICE_DETAIL )xx group by DOC_CODE 
                                    ) As TabFATSNFDetail On TabFATSNFDetail.DOC_CODE= TSPL_PAYMENT_PROCESS_DETAIL.Milk_Purchase_Invoice_No 
				                    left outer join (select case when DCSDescription='PURCHASE EXP.' THEN Amount ELSE 0 END AS PURCHASEEXPENSE,x.InvoiceNo,x.DCSDescription from(
				                    select TSPL_MILK_PURCHASE_INVOICE_DCS_ADD_DED_DONT_GENERATE_DR_CR_NOTE.InvoiceNo,TSPL_DCS_ADDITION_DEDUCTION.Description As DCSDescription,TSPL_MILK_PURCHASE_INVOICE_DCS_ADD_DED_DONT_GENERATE_DR_CR_NOTE.Amount from TSPL_MILK_PURCHASE_INVOICE_DCS_ADD_DED_DONT_GENERATE_DR_CR_NOTE
				                    left outer join TSPL_DCS_ADDITION_DEDUCTION on TSPL_DCS_ADDITION_DEDUCTION.Code = TSPL_MILK_PURCHASE_INVOICE_DCS_ADD_DED_DONT_GENERATE_DR_CR_NOTE.DCS_Addition_Deduction)x
				                    ) As TabDCSdrcr on TabDCSdrcr.InvoiceNo = TSPL_PAYMENT_PROCESS_DETAIL.Milk_Purchase_Invoice_No 

                                    left outer join (select case when DCSDescription='PURCHASE EXP.' THEN VendorAmt ELSE 0 END AS CRAmt,
                                    case when DCSDescription='OWN BMC EXPANCES' THEN VendorAmt 
                                    WHEN DCSDescription = 'CHILLING CHARGES' THEN VendorAmt ELSE 0 END AS OBEAmt
									,x.Doc_No,x.DCSDescription 
									,x.InvoiceNo from(
				                    select TSPL_PAYMENT_PROCESS_CREDIT_NOTE.Doc_No,TSPL_VENDOR_INVOICE_DETAIL.DCS_Addition_Deduction,TSPL_DCS_ADDITION_DEDUCTION.Description as DCSDescription,TSPL_PAYMENT_PROCESS_CREDIT_NOTE.Amount,TSPL_VENDOR_INVOICE_DETAIL.Amount as VendorAmt,TSPL_VENDOR_INVOICE_HEAD.Main_VSP_Milk_AP_Invoice_No as InvoiceNo from TSPL_PAYMENT_PROCESS_CREDIT_NOTE
					                  left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No = TSPL_PAYMENT_PROCESS_CREDIT_NOTE.AP_Invoice_No
				                    left outer join TSPL_VENDOR_INVOICE_DETAIL on TSPL_VENDOR_INVOICE_DETAIL.Document_No = TSPL_PAYMENT_PROCESS_CREDIT_NOTE.AP_Invoice_No
									left outer join TSPL_DCS_ADDITION_DEDUCTION on TSPL_DCS_ADDITION_DEDUCTION.Code = TSPL_VENDOR_INVOICE_DETAIL.DCS_Addition_Deduction
									)x
				                   ) As TabDCSCreditNote on TabDCSCreditNote.InvoiceNo = TSPL_PAYMENT_PROCESS_DETAIL.AP_Invoice_No 
									
				                    left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_CODE=TSPL_PAYMENT_PROCESS_DETAIL.VSP_Code
				                    left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code=TSPL_PAYMENT_PROCESS_DETAIL.MCC_Code
				                    Where convert(date, TSPL_PAYMENT_PROCESS_DETAIL.Milk_Purchase_Invoice_Date,103)>= '" + clsCommon.GetPrintDate(fromDate.Value) + "'   
                                    and convert(date,TSPL_PAYMENT_PROCESS_DETAIL.Milk_Purchase_Invoice_Date,103)<= '" + clsCommon.GetPrintDate(dtpToDate.Value) + "' 
				                    )x group by x.AP_Invoice_No)xy "

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
            Gv1.Columns(ii).IsVisible = False
        Next

        'Gv1.Columns("Milk_Purchase_Invoice_Date").HeaderText = "Date"
        'Gv1.Columns("Milk_Purchase_Invoice_Date").Width = 200
        'Gv1.Columns("Milk_Purchase_Invoice_Date").IsVisible = True

        Gv1.Columns("Dcs_Uploader").HeaderText = "Dcs Uploader"
        Gv1.Columns("Dcs_Uploader").Width = 200
        Gv1.Columns("Dcs_Uploader").IsVisible = True

        Gv1.Columns("Dcs_name").HeaderText = "Dcs Name"
        Gv1.Columns("Dcs_name").Width = 200
        Gv1.Columns("Dcs_name").IsVisible = True

        Gv1.Columns("Milk_Qty").HeaderText = "Milk Qty"
        Gv1.Columns("Milk_Qty").Width = 200
        Gv1.Columns("Milk_Qty").IsVisible = True

        Gv1.Columns("FATKg").HeaderText = "FATKg"
        Gv1.Columns("FATKg").Width = 200
        Gv1.Columns("FATKg").IsVisible = True

        Gv1.Columns("SNFKg").HeaderText = "SNFKg"
        Gv1.Columns("SNFKg").Width = 200
        Gv1.Columns("SNFKg").IsVisible = True

        Gv1.Columns("Milk_Amount").HeaderText = "Milk Amount"
        Gv1.Columns("Milk_Amount").Width = 200
        Gv1.Columns("Milk_Amount").IsVisible = True

        Gv1.Columns("Head_Load_Amount").HeaderText = "HeadLoad Amount"
        Gv1.Columns("Head_Load_Amount").Width = 200
        Gv1.Columns("Head_Load_Amount").IsVisible = True

        Gv1.Columns("CRAmt").HeaderText = "CR Amount"
        Gv1.Columns("CRAmt").Width = 200
        Gv1.Columns("CRAmt").IsVisible = True

        Gv1.Columns("Reduce_Deduc_Amt").HeaderText = "ReduceDeduc Amt"
        Gv1.Columns("Reduce_Deduc_Amt").Width = 200
        Gv1.Columns("Reduce_Deduc_Amt").IsVisible = True

        Gv1.Columns("Deduction_Amount").HeaderText = "Deduction Amount"
        Gv1.Columns("Deduction_Amount").Width = 200
        Gv1.Columns("Deduction_Amount").IsVisible = True

        Gv1.Columns("PEAmt").HeaderText = "PE.Amount"
        Gv1.Columns("PEAmt").Width = 200
        Gv1.Columns("PEAmt").IsVisible = True

        Gv1.Columns("OBEAmt").HeaderText = "OBE.Amount"
        Gv1.Columns("OBEAmt").Width = 200
        Gv1.Columns("OBEAmt").IsVisible = True

        Gv1.Columns("Payable_Amount").HeaderText = "Payable Amount"
        Gv1.Columns("Payable_Amount").Width = 200
        Gv1.Columns("Payable_Amount").IsVisible = True

        Gv1.Columns("Saving_Amount").HeaderText = "Saving Amount"
        Gv1.Columns("Saving_Amount").Width = 200
        Gv1.Columns("Saving_Amount").IsVisible = True

        Gv1.Columns("TotalAmt").HeaderText = "Total Amount"
        Gv1.Columns("TotalAmt").Width = 200
        Gv1.Columns("TotalAmt").IsVisible = True


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
        Dim item8 As New GridViewSummaryItem("Saving_Amount", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item8)
        Dim item9 As New GridViewSummaryItem("Payable_Amount", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item9)
        Dim item10 As New GridViewSummaryItem("TotalAmt", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item10)
        Dim item11 As New GridViewSummaryItem("PEAmt", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item11)
        Dim item12 As New GridViewSummaryItem("OBEAmt", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item12)

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
