Imports common
Imports System.Globalization
Imports System.Data.SqlClient
Imports System.IO
Imports System.Net
Imports XpertERPEngine
Imports System.Text.RegularExpressions

Public Class rptAccountStatementReport
    Inherits FrmMainTranScreen

    Private Sub rptAccountStatementReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = clsCommon.GETSERVERDATE()
        RadPageView1.SelectedPage = RadPageViewPage1
        Reset()
        CreateTab()
    End Sub

    Sub CreateTab()
        Try
            Dim coll As Dictionary(Of String, String)
            coll = New Dictionary(Of String, String)
            coll.Add("Cust_Code", "varchar(12) null references TSPL_Customer_MASTER(Cust_Code)")
            coll.Add("Opening_Amount", "decimal (18,2) NULL")
            clsCommonFunctionality.CreateOrAlterTable(True, False, "TSPL_Opening_Table", coll, Nothing, Nothing)

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        LoadData(False)
    End Sub
    Sub Reset()
        gv1.DataSource = Nothing
        RadPageView1.SelectedPage = RadPageViewPage1
        EnableDisableControl(True)
    End Sub

    Private Sub EnableDisableControl(ByVal val As Boolean)
        RadGroupBox1.Enabled = val
    End Sub

    Private Sub LoadData(ByVal isPrint As Boolean)
        Try
            Dim qry As String = "  Select Comp_Name,Add1,Add2,Add3,FromDate,ToDate,Cust_Code,Customer_Name,[DR/CR],OP,Money_Receipt,Milk_Amount,Product_Amount,Refund_Amount,Credit_Amount,Debit_Amount,CASE WHEN Closing < 0 then -(Closing) else Closing end as Closing,[DR.CR] from (Select Comp_Name,Add1,Add2,Add3,FromDate,ToDate,Cust_Code,Customer_Name,CASE WHEN OP < 0 THEN 'DR' ELSE 'CR' END AS [DR/CR],CASE WHEN OP < 0 then -(OP) else OP end as OP,Money_Receipt,Milk_Amount,Product_Amount,Refund_Amount,Credit_Amount,Debit_Amount,((OP+Money_Receipt+Refund_Amount)-(Milk_Amount+Product_Amount)) as Closing, case when ((OP+Money_Receipt+Refund_Amount)-(Milk_Amount+Product_Amount)) < 0 then 'DR' ELSE 'CR' end as [DR.CR] from ( Select max(TSPL_COMPANY_MASTER.Comp_Name)Comp_Name,max(TSPL_COMPANY_MASTER.Add1)Add1,max(TSPL_COMPANY_MASTER.Add2)Add2,
max(TSPL_COMPANY_MASTER.Add3)Add3,'" & clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") & "' as FromDate, '" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy") & "' as ToDate,XX.Cust_Code,max(xx.Customer_Name)Customer_Name,
case when '01/Apr/2026' = Convert( Date,'" + clsCommon.GetPrintDate((txtFromDate.Value), "dd/MMM/yyyy ") + "',103) then max(OT.Opening_Amount) else 
max(OT.Opening_Amount)+sum((Amount) * (case when   Convert( Date, Document_Date,103) >= Convert(Date,'01/Apr/2026 12:00:00 AM',103) and  Convert( Date, Document_Date,103) < Convert( Date,'" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "',103)  then 1 else 0 end) * (case when RI=1 or RI=4 or RI=5 OR RI=7 then 1 else -1 end)) end as OP 
,sum(Amount * (case when  Convert( Date, Document_Date,103) >=Convert(Date,'" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "',103) and Document_Date<=Convert(Date,'" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "',103) then 1 else 0 end) * (case when (RI=1) then 1 else 0 end)) as Money_Receipt
,sum(Amount * (case when  Convert( Date, Document_Date,103)>=Convert(Date,'" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "',103) and Convert( Date, Document_Date,103)<=Convert(Date,'" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "',103) then 1 else 0 end) * (case when (RI=2) then 1 else 0 end)) as Milk_Amount
,sum(Amount * (case when  Convert( Date, Document_Date,103)>=Convert(Date,'" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "',103) and Convert( Date, Document_Date,103)<=Convert(Date,'" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "',103) then 1 else 0 end) * (case when (RI=3) then 1 else 0 end)) as Product_Amount
,sum(Amount * (case when  Convert( Date, Document_Date,103)>=Convert(Date,'" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "',103) and Convert( Date, Document_Date,103)<=Convert(Date,'" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "',103) then 1 else 0 end) * (case when (RI=4) then 1 else 0 end)) as Refund_Amount
,sum(Amount * (case when  Convert( Date, Document_Date,103)>=Convert(Date,'" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "',103) and Convert( Date, Document_Date,103)<=Convert(Date,'" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "',103) then 1 else 0 end) * (case when (RI=5) then 1 else 0 end)) as Credit_Amount
,sum(Amount * (case when  Convert( Date, Document_Date,103)>=Convert(Date,'" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "',103) and Convert( Date, Document_Date,103)<=Convert(Date,'" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "',103) then 1 else 0 end) * (case when (RI=6) then 1 else 0 end)) as Debit_Amount
 
from 
(Select Cust_Code,Customer_Name,Receipt_Amount as Amount,Convert(date,TSPL_RECEIPT_HEADER.Receipt_Date,103) as Document_Date,1 as RI from TSPL_RECEIPT_HEADER
union all
Select Customer_Code as Cust_Code,Customer_Name,Total_Amt as Amount,convert(date,TSPL_SD_SHIPMENT_HEAD.Document_Date,103) as Document_Date,2 as RI from TSPL_SD_SHIPMENT_HEAD
left outer join TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SHIPMENT_HEAD.Customer_Code
where Is_Taxable=0 and TSPL_SD_SHIPMENT_HEAD.Status=1
union all
Select Customer_Code as Cust_Code,Customer_Name,Total_Amt as Amount,convert(date,TSPL_SD_SHIPMENT_HEAD.Document_Date,103) as Document_Date,3 as RI from TSPL_SD_SHIPMENT_HEAD
left outer join TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SHIPMENT_HEAD.Customer_Code
where Is_Taxable=1 and TSPL_SD_SHIPMENT_HEAD.Status=1
union all
Select Customer_Code as Cust_Code,Customer_Name,Total_Amt as Amount,convert(date,TSPL_SD_SALE_RETURN_HEAD.Document_Date,103),4 as RI from TSPL_SD_SALE_RETURN_HEAD
left outer join TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SALE_RETURN_HEAD.Customer_Code
union all
Select Customer_Code,Customer_Name,Document_Total as Amount,convert(date,TSPL_Customer_Invoice_Head.Document_Date,103) as Document_Date,5 as RI from TSPL_Customer_Invoice_Head
WHERE Document_Type='C'
union all
Select Customer_Code,Customer_Name,Document_Total as Amount,convert(date,TSPL_Customer_Invoice_Head.Document_Date,103) as Document_Date,6 as RI from TSPL_Customer_Invoice_Head
WHERE Document_Type='D'
) XX 
left outer join TSPL_COMPANY_MASTER ON 2=2 
LEFT JOIN   TSPL_Opening_Table OT ON OT.Cust_Code = XX.Cust_Code 
"
            If txtCustomer.arrValueMember IsNot Nothing Then
                qry += " where XX.Cust_Code In (" & clsCommon.GetMulcallString(txtCustomer.arrValueMember) & ")"
            End If
            'where XX.Cust_Code In('D1')

            qry += " Group by XX.Cust_Code )YY )ZZ "

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            gv1.DataSource = Nothing
            gv1.Rows.Clear()
            gv1.Columns.Clear()
            gv1.GroupDescriptors.Clear()
            gv1.MasterView.Refresh()
            If dt.Rows.Count > 0 Then
                gv1.DataSource = dt
                gv1.GroupDescriptors.Clear()
                gv1.EnableFiltering = True
                gv1.MasterTemplate.SummaryRowsBottom.Clear()
                EnableDisableControl(False)
                'View()
                SetGridFormation(isPrint)
                gv1.MasterTemplate.AutoExpandGroups = True
                RadPageView1.SelectedPage = RadPageViewPage2
                gv1.BestFitColumns()
                If isPrint Then
                    Dim frmCRV As New frmCrystalReportViewer()
                    frmCRV.funreport(Form_ID, CrystalReportFolder.SalesReport, dt, "rptAccountStatement", "Account Statement Report")
                    frmCRV = Nothing
                End If
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
        '        Try
        '            Dim whrcls As String = ""
        '            txtFromDate.Value = "01/" & DatePart(DateInterval.Month, txtFromDate.Value) & "/" & DatePart(DateInterval.Year, txtFromDate.Value)
        '            Dim dtDate As New DataTable()
        '            whrcls = "  and TSPL_MILK_SRN_HEAD.Posted=1  and convert(date,TSPL_MILK_SRN_HEAD.Doc_Date,103) >= Convert(date,'" & clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") & "',103)  and convert(date,TSPL_MILK_SRN_HEAD.Doc_Date,103) <= Convert(date,'" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") & "',103)  "

        '            Dim qry As String = "select "
        '            If isPrint Then
        '                qry += "  Comp_Name, Logo_Img,'" & clsCommon.GetPrintDate(txtFromDate.Value, "MMM-yyyy") & "' as Month,'" & objCommonVar.CurrentUser & "' as UserName, "
        '            End If
        '            qry += " Source,Zone_Code,Zone_Name,Sweet_Qty,Sweet_FATKG,Sweet_SNFKG,Sour_Qty,Sour_FATKG,Sour_SNFKG,Curd_Qty,Total_Qty,Total_FAT_KG,Total_SNF_KG,(Total_Qty/" & txtToDate.Value.Day & ") as Avg_Qty,convert(decimal(18,2),((Total_FAT_KG*100)/ (Sweet_Qty+Sour_Qty))) as Avg_FAT,convert(decimal(18,2),((Total_SNF_KG*100)/ (Sweet_Qty+Sour_Qty))) as Avg_SNF from ( Select Source,Zone_Code,max(Zone_Name)Zone_Name,sum(case when QBD = 'SWEET' then Qty else 0 end) as Sweet_Qty,sum(case when QBD = 'SWEET' then FAT_KG else 0 end) as Sweet_FATKG,sum(case when QBD = 'SWEET' then SNF_KG else 0 end) as Sweet_SNFKG,sum(case when QBD = 'SOUR' then Qty else 0 end) as Sour_Qty,sum(case when QBD = 'SOUR' then FAT_KG else 0 end) as Sour_FATKG,
        'sum(case when QBD = 'SOUR' then SNF_KG else 0 end) as Sour_SNFKG,
        'sum(case when QBD = 'CURD' then Qty else 0 end) as Curd_Qty,sum(Qty)as Total_Qty,sum(FAT_KG)as Total_FAT_KG,SUM(SNF_KG)as Total_SNF_KG ,SeqNo from (
        'select TSPL_VENDOR_MASTER.Zone_Code,TSPL_ZONE_MASTER.Description as Zone_Name,xx.* from (
        'select Against_Uploader_TR_No,Against_Shift_Uploader_TR_No,TSPL_MILK_SRN_HEAD.DOC_DATE,TSPL_MILK_SRN_HEAD.VLC_Code,(case when TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.TR_No is not null then isnull (TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Reject_Type,'SWEET') else (case when TSPL_MILK_SHIFT_UPLOADER_DETAIL.TR_No is not null then isnull (TSPL_MILK_SHIFT_UPLOADER_DETAIL.Reject_Type,'SWEET') end) end) as QBD ,Qty,FAT_KG,SNF_KG, (case when TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.TR_No is not null then 'MILK RECEIVED AT MCC' ELSE  (case when TSPL_MILK_SHIFT_UPLOADER_DETAIL.TR_No is not null then 'MILK RECEIVED THROUGH BMC' end) end) AS  Source,  (case when TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.TR_No is not null then 1 ELSE  (case when TSPL_MILK_SHIFT_UPLOADER_DETAIL.TR_No is not null then 2 end) end)  AS SeqNo
        'from TSPL_MILK_SRN_HEAD
        'left join TSPL_MILK_SRN_DETAIL on TSPL_MILK_SRN_DETAIL.DOC_CODE=TSPL_MILK_SRN_HEAD.DOC_CODE
        'left join TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL on TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.TR_No=TSPL_MILK_SRN_HEAD.Against_Uploader_TR_No  
        'left join TSPL_MILK_SHIFT_UPLOADER_DETAIL on TSPL_MILK_SHIFT_UPLOADER_DETAIL.TR_No=TSPL_MILK_SRN_HEAD.Against_Shift_Uploader_TR_No  
        'where 2=2 " & whrcls & "
        '-----TOTAL MILK RECEIVED DURING MONTH
        'union all
        'select  Against_Uploader_TR_No, Against_Shift_Uploader_TR_No,TSPL_MILK_SRN_HEAD.DOC_DATE,TSPL_MILK_SRN_HEAD.VLC_Code,(case when TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.TR_No is not null then isnull (TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Reject_Type,'SWEET') else (case when TSPL_MILK_SHIFT_UPLOADER_DETAIL.TR_No is not null then isnull (TSPL_MILK_SHIFT_UPLOADER_DETAIL.Reject_Type,'SWEET') end) end) as QBD,Qty, FAT_KG, SNF_KG,'TOTAL MILK RECEIVED DURING MONTH' AS  Source, 3 AS SeqNo 
        ' from TSPL_MILK_SRN_HEAD
        'left join TSPL_MILK_SRN_DETAIL on TSPL_MILK_SRN_DETAIL.DOC_CODE=TSPL_MILK_SRN_HEAD.DOC_CODE
        'left join TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL on TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.TR_No=TSPL_MILK_SRN_HEAD.Against_Uploader_TR_No  
        'left join TSPL_MILK_SHIFT_UPLOADER_DETAIL on TSPL_MILK_SHIFT_UPLOADER_DETAIL.TR_No=TSPL_MILK_SRN_HEAD.Against_Shift_Uploader_TR_No  
        'where 2=2 " & whrcls & "
        ') xx 
        'left join TSPL_VLC_MASTER_HEAD ON TSPL_VLC_MASTER_HEAD.VLC_CODE = xx.VLC_Code
        'LEFT JOIN TSPL_VENDOR_MASTER ON TSPL_VENDOR_MASTER.VENDOR_CODE = TSPL_VLC_MASTER_HEAD.VSP_CODE 
        'LEFT JOIN TSPL_ZONE_MASTER ON TSPL_ZONE_MASTER.Zone_Code = TSPL_VENDOR_MASTER.ZONE_CODE
        ') xxx "
        '            If txtCustomer.arrValueMember IsNot Nothing Then
        '                qry += " where xxx.Zone_Code in (" & clsCommon.GetMulcallString(txtCustomer.arrValueMember) & ")"
        '            End If
        '            qry += " group by Zone_Code,Source,SeqNo  )xxxx LEFT JOIN TSPL_COMPANY_MASTER ON 1=1 order by SeqNo,Zone_Code "

        '            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        '            gv1.DataSource = Nothing
        '            gv1.Rows.Clear()
        '            gv1.Columns.Clear()
        '            gv1.GroupDescriptors.Clear()
        '            gv1.MasterView.Refresh()
        '            If dt.Rows.Count > 0 Then
        '                gv1.DataSource = dt
        '                gv1.GroupDescriptors.Clear()
        '                gv1.EnableFiltering = True
        '                gv1.MasterTemplate.SummaryRowsBottom.Clear()
        '                EnableDisableControl(False)
        '                'View()
        '                SetGridFormation(isPrint)
        '                gv1.MasterTemplate.AutoExpandGroups = True
        '                RadPageView1.SelectedPage = RadPageViewPage2
        '                gv1.BestFitColumns()
        '                If isPrint Then
        '                    Dim frmCRV As New frmCrystalReportViewer()
        '                    frmCRV.funreport(Form_ID, CrystalReportFolder.MilkProcurement, dt, "rptMilkProcurement", "Milk Procurement Report")
        '                    frmCRV = Nothing
        '                End If
        '            Else
        '                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
        '                Exit Sub
        '            End If
        '        Catch ex As Exception
        '            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)

        '        End Try
    End Sub
    Sub View()
        Try
            If gv1.Rows.Count > 0 Then
                Dim view As New ColumnGroupsViewDefinition()
                view.ColumnGroups.Add(New GridViewColumnGroup("AREA"))
                view.ColumnGroups(0).Rows.Add(New GridViewColumnGroupRow())
                view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv1.Columns("Zone_Name").Name)

                view.ColumnGroups.Add(New GridViewColumnGroup("SWEET"))
                view.ColumnGroups(view.ColumnGroups.Count - 1).Rows.Add(New GridViewColumnGroupRow())
                view.ColumnGroups(view.ColumnGroups.Count - 1).Rows(0).ColumnNames.Add(gv1.Columns("Sweet_Qty").Name)
                view.ColumnGroups(view.ColumnGroups.Count - 1).Rows(0).ColumnNames.Add(gv1.Columns("Sweet_FATKG").Name)
                view.ColumnGroups(view.ColumnGroups.Count - 1).Rows(0).ColumnNames.Add(gv1.Columns("Sweet_SNFKG").Name)

                view.ColumnGroups.Add(New GridViewColumnGroup("SOUR"))
                view.ColumnGroups(view.ColumnGroups.Count - 1).Rows.Add(New GridViewColumnGroupRow())

                view.ColumnGroups(view.ColumnGroups.Count - 1).Rows(0).ColumnNames.Add(gv1.Columns("Sour_Qty").Name)
                view.ColumnGroups(view.ColumnGroups.Count - 1).Rows(0).ColumnNames.Add(gv1.Columns("Sour_FATKG").Name)
                view.ColumnGroups(view.ColumnGroups.Count - 1).Rows(0).ColumnNames.Add(gv1.Columns("Sour_SNFKG").Name)

                view.ColumnGroups.Add(New GridViewColumnGroup("CURD"))
                view.ColumnGroups(view.ColumnGroups.Count - 1).Rows.Add(New GridViewColumnGroupRow())
                view.ColumnGroups(view.ColumnGroups.Count - 1).Rows(0).ColumnNames.Add(gv1.Columns("Curd_Qty").Name)




                gv1.ViewDefinition = view

            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub
    Sub SetGridFormation(ByVal isPrint As Boolean)
        '  gv1.TableElement.TableHeaderHeight = 40
        gv1.MasterTemplate.ShowRowHeaderColumn = True
        gv1.EnableFiltering = True
        gv1.ShowRowHeaderColumn = True
        gv1.ShowGroupPanel = True
        For ii As Integer = 0 To gv1.Columns.Count - 1
            gv1.Columns(ii).ReadOnly = True
            gv1.Columns(ii).IsVisible = True
            gv1.Columns(ii).FormatString = "{0:n2}"
        Next
        'If isPrint Then
        '    gv1.Columns("Comp_Name").IsVisible = False
        '    gv1.Columns("Logo_Img").IsVisible = False
        '    gv1.Columns("Month").IsVisible = False
        '    gv1.Columns("UserName").IsVisible = False
        'End If
        gv1.Columns("Cust_Code").IsVisible = False
        gv1.Columns("Cust_Code").VisibleInColumnChooser = True
        gv1.Columns("FromDate").IsVisible = False
        gv1.Columns("FromDate").VisibleInColumnChooser = True
        gv1.Columns("ToDate").IsVisible = False
        gv1.Columns("ToDate").VisibleInColumnChooser = True
        gv1.Columns("Comp_Name").IsVisible = False
        gv1.Columns("Comp_Name").VisibleInColumnChooser = True
        gv1.Columns("Add1").IsVisible = False
        gv1.Columns("Add1").VisibleInColumnChooser = True
        gv1.Columns("Add2").IsVisible = False
        gv1.Columns("Add2").VisibleInColumnChooser = True
        gv1.Columns("Add3").IsVisible = False
        gv1.Columns("Add3").VisibleInColumnChooser = True
        gv1.Columns("Credit_Amount").IsVisible = False
        gv1.Columns("Credit_Amount").VisibleInColumnChooser = True
        gv1.Columns("Debit_Amount").IsVisible = False
        gv1.Columns("Debit_Amount").VisibleInColumnChooser = True

        gv1.Columns("Customer_Name").HeaderText = "Customer Name"
        gv1.Columns("OP").HeaderText = "Opening Balance"
        gv1.Columns("OP").FormatString = "{0:n2}"
        gv1.Columns("Money_Receipt").HeaderText = "Money Receipt"
        gv1.Columns("Money_Receipt").FormatString = "{0:n2}"
        gv1.Columns("Milk_Amount").HeaderText = "Milk Amount"
        gv1.Columns("Milk_Amount").FormatString = "{0:n2}"
        gv1.Columns("Product_Amount").HeaderText = "Product Amount"
        gv1.Columns("Product_Amount").FormatString = "{0:n2}"
        gv1.Columns("Refund_Amount").HeaderText = "Refund Amount"
        gv1.Columns("Refund_Amount").FormatString = "{0:n2}"
        gv1.Columns("Closing").HeaderText = "Closing Balance"
        gv1.Columns("Closing").FormatString = "{0:n2}"


        'Dim summaryRowItem As New GridViewSummaryRowItem()
        'For ii As Integer = IIf(isPrint, 7, 3) To gv1.Columns.Count - 1
        '    If gv1.Columns(ii).Name.Contains("FAT") OrElse gv1.Columns(ii).Name.Contains("SNF") Then
        '        If clsCommon.CompairString(gv1.Columns(ii).Name, "Avg_FAT") = CompairStringResult.Equal Then
        '            Dim summaryItem1 As New GridViewSummaryItem()
        '            summaryItem1.FormatString = "{0:F3}"
        '            summaryItem1.Name = "Avg_FAT"
        '            summaryItem1.AggregateExpression = "sum(Total_FAT_KG)*100/sum(Sweet_Qty+Sour_Qty)"
        '            summaryRowItem.Add(summaryItem1)
        '        ElseIf clsCommon.CompairString(gv1.Columns(ii).Name, "Avg_SNF") = CompairStringResult.Equal Then
        '            Dim summaryItem2 As New GridViewSummaryItem()
        '            summaryItem2.FormatString = "{0:F3}"
        '            summaryItem2.Name = "Avg_SNF"
        '            summaryItem2.AggregateExpression = "(sum(Total_SNF_KG)/sum(Sweet_Qty+Sour_Qty))*100"
        '            summaryRowItem.Add(summaryItem2)
        '        Else
        '            summaryRowItem.Add(New GridViewSummaryItem(gv1.Columns(ii).Name, "{0:F3}", GridAggregateFunction.Sum))
        '        End If
        '    Else
        '        summaryRowItem.Add(New GridViewSummaryItem(gv1.Columns(ii).Name, "{0:F2}", GridAggregateFunction.Sum))
        '    End If
        'Next
        'gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

        'Dim Itemdescriptor As New GroupDescriptor()
        'Itemdescriptor.GroupNames.Add("Source", System.ComponentModel.ListSortDirection.Ascending)
        'gv1.GroupDescriptors.Add(Itemdescriptor)
        'gv1.MasterTemplate.AutoExpandGroups = True
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Reset()
    End Sub

    Private Sub btnExcel_Click(sender As Object, e As EventArgs) Handles btnExcel.Click
        Try
            If gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Print Date (" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd-MMM-yyyy hh:mm:ss tt") + ")")
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.AccountStatementReport & "'"))
                arrHeader.Add("Month: " & clsCommon.GetPrintDate(txtFromDate.Value, "MMM-yyyy"))
                'If txtCustomer.arrValueMember IsNot Nothing Then
                '    arrHeader.Add("Route : " & clsCommon.GetMulcallString(txtCustomer.arrValueMember) & "  Route Name :" & clsCommon.GetMulcallString(txtCustomer.arrDispalyMember) & "")
                'End If
                clsCommon.MyExportToExcelGrid(Me.Text, gv1, arrHeader, Me.Text)
            Else
                clsCommon.MyMessageBoxShow(Me, "No data found to export.", Me.Text)

            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnPDF_Click(sender As Object, e As EventArgs) Handles btnPDF.Click
        Try

            If gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()

                clsCommon.MyExportToPDF(Me.Text, gv1, arrHeader, Me.Text)
                arrHeader.Add("Month: " & clsCommon.GetPrintDate(txtFromDate.Value, "MMM-yyyy"))
                'If txtCustomer.arrValueMember IsNot Nothing Then
                '    arrHeader.Add("Party Name : " & clsCommon.GetMulcallString(txtCustomer.arrValueMember) & "  Route Name :" & clsCommon.GetMulcallString(txtCustomer.arrDispalyMember) & "")
                'End If
            Else
                clsCommon.MyMessageBoxShow(Me, "No data found To export", Me.Text)
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtFromDate_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs)
        txtToDate.Value = New DateTime(txtFromDate.Value.Year, txtFromDate.Value.Month, DateTime.DaysInMonth(txtFromDate.Value.Year, txtFromDate.Value.Month))
    End Sub
    Private Sub txtCustomer__My_Click(sender As Object, e As EventArgs) Handles txtCustomer._My_Click
        Try
            Dim qry As String = "select Cust_Code as Code,Customer_Name as Name from tspl_customer_master  "

            txtCustomer.arrValueMember = clsCommon.ShowMultipleSelectForm("CustFilter", qry, "Code", "Name", txtCustomer.arrValueMember, txtCustomer.arrDispalyMember)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        LoadData(True)
    End Sub
End Class