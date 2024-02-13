Imports common
Imports System.Globalization
Imports System.Data.SqlClient
Imports System.IO
Imports System.Text.RegularExpressions
Imports System.Net.Mail
Imports System.Net.WebClient
Imports System.Net
Imports XpertERPEngine

Public Class rptBulkSaleAcknowledgementUploaderReport
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim StrPermission As String

    Private Sub rptBulkSaleAcknowledgementUploaderReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        StrPermission = clsERPFuncationality.UserWiseAvailableLocationCode()
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnGo, "Press Alt+R Refresh ")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N Adding New")
        RadPageView1.SelectedPage = RadPageViewPage1
        txtToDate.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE, "dd/MM/yyyy")
        txtFromDate.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE, "dd/MM/yyyy")
        Reset()
    End Sub

    Private Sub txtCustomer__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtCustomer._MYValidating
        Dim qry As String = "select TSPL_CUSTOMER_MASTER.Cust_Code as Code,Customer_Name as Name from TSPL_CUSTOMER_MASTER"
        txtCustomer.Value = clsCommon.ShowSelectForm("BulkAckUploaderReport", qry, "Code", "", txtCustomer.Value, "Code", isButtonClicked)
    End Sub



    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        LoadData()
    End Sub
    Sub Reset()
        gv1.DataSource = Nothing
        RadPageView1.SelectedPage = RadPageViewPage1
        txtCustomer.Value = ""
        EnableDisableControl(True)
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = clsCommon.GETSERVERDATE()
    End Sub

    Private Sub EnableDisableControl(ByVal val As Boolean)
        RadGroupBox1.Enabled = val

    End Sub

    Private Sub LoadData()
        Try
            Dim dt As DataTable = New DataTable()
            Dim whrcls As String = ""
            If clsCommon.myLen(txtCustomer.Value) > 0 Then
                whrcls = " And Customer_Code = '" & txtCustomer.Value & "' "
            End If
            whrcls = " and convert(date,TSPL_Dispatch_BulkSale.document_date , 103 ) >= convert(date,'" & txtFromDate.Value & "',103) and  convert(date,TSPL_Dispatch_BulkSale.document_date , 103 ) <= convert(date,'" & txtToDate.Value & "',103) "
            Dim qry As String = ""
            qry = "select ROW_NUMBER() over(order by (TSPL_Dispatch_BulkSale.Document_Date)) as 'SNO.', TSPL_Dispatch_BulkSale.Customer_Code as Customer,TSPL_Dispatch_BulkSale.Tanker_Code as [Tanker No] ,convert(varchar,TSPL_Dispatch_BulkSale.Document_Date,103) as 'Dispatch Date',TSPL_Dispatch_BulkSale.Document_No as 'Dispatch No' , isnull(TSPL_Dispatch_Detail_BulkSale.Qty,0) as 'Dispatch Qty',isnull(convert(decimal(18,2),TSPL_Dispatch_Detail_BulkSale.FatPer),0) as 'Dispatch FatPer',
            isnull(convert(decimal(18,2),TSPL_Dispatch_Detail_BulkSale.SNFPer),0) as 'Dispatch SNFPer' ,isnull(convert(decimal(18,2),TSPL_Dispatch_Detail_BulkSale.Fat_KG),0) AS 'Dispatch FATKG',isnull(convert(decimal(18,2),TSPL_Dispatch_Detail_BulkSale.FatRate),0) AS 'Dispatch FAT Rate', isnull(convert(decimal(18,2),TSPL_Dispatch_Detail_BulkSale.SNF_KG),0) AS 'Dispatch SNFKG',isnull(convert(decimal(18,2),TSPL_Dispatch_Detail_BulkSale.SNFRate),0) AS 'Dispatch SNF Rate',
            isnull(convert(decimal(18,2),TSPL_Dispatch_Detail_BulkSale.Amount),0) AS 'Dispatch Amount' , isnull(TSPL_BULK_SALE_ACKNOWLEDGEMENT.Qty,0) as 'ACK Qty',isnull(convert(decimal(18,2),TSPL_BULK_SALE_ACKNOWLEDGEMENT.FAT),0) AS  'ACK FatPer', isnull(convert(decimal(18,2),TSPL_BULK_SALE_ACKNOWLEDGEMENT.SNF),0) AS 'ACK SNFPer' , isnull(convert(decimal(18,2),TSPL_BULK_SALE_ACKNOWLEDGEMENT.FAT_KG),0) AS 'ACK FATKG', isnull(convert(decimal(18,2),TSPL_BULK_SALE_ACKNOWLEDGEMENT.FAT_Rate),0) AS 'ACK FAT Rate', isnull(convert(decimal(18,2),TSPL_BULK_SALE_ACKNOWLEDGEMENT.SNF_KG),0) AS 'ACK SNFKG', isnull(convert(decimal(18,2),TSPL_BULK_SALE_ACKNOWLEDGEMENT.SNF_Rate),0) AS 'ACK SNF Rate' , isnull(convert(decimal(18,2),TSPL_BULK_SALE_ACKNOWLEDGEMENT.Amount),0) AS 'ACK Amount' , isnull(convert(decimal(18,2),TSPL_BULK_SALE_ACKNOWLEDGEMENT.Diff_Amount),0) AS 'ACK Diff Amount' ,TSPL_BULK_SALE_ACKNOWLEDGEMENT.Remarks AS Remarks  from TSPL_Dispatch_Detail_BulkSale  
            Left Outer Join TSPL_Dispatch_BulkSale On TSPL_Dispatch_BulkSale.Document_No=TSPL_Dispatch_Detail_BulkSale.Document_No  left outer join TSPL_BULK_SALE_ACKNOWLEDGEMENT on TSPL_BULK_SALE_ACKNOWLEDGEMENT.Bulk_Dispatch_Document = TSPL_Dispatch_Detail_BulkSale.Document_No Where 1=1 " & whrcls & " order by TSPL_Dispatch_BulkSale.Document_Date,TSPL_Dispatch_BulkSale.Document_No "
            dt = clsDBFuncationality.GetDataTable(qry)
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
                SetGridFormation()
                gv1.MasterTemplate.AutoExpandGroups = True
                RadPageView1.SelectedPage = RadPageViewPage2
                gv1.BestFitColumns()
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub

            End If


        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub SetGridFormation()
        gv1.TableElement.TableHeaderHeight = 40
        gv1.MasterTemplate.ShowRowHeaderColumn = True
        gv1.EnableFiltering = True
        gv1.ShowRowHeaderColumn = True
        For ii As Integer = 0 To gv1.Columns.Count - 1
            gv1.Columns(ii).ReadOnly = True
            gv1.Columns(ii).IsVisible = True
            gv1.Columns(ii).Width = 150
        Next
        gv1.Columns("SNO.").Width = 40
        gv1.Columns("Dispatch Date").Width = 100
        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim ACKQty As New GridViewSummaryItem("ACK Qty", "", GridAggregateFunction.Sum)
        summaryRowItem.Add(ACKQty)
        Dim ACKAmount As New GridViewSummaryItem("ACK Amount", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(ACKAmount)
        Dim ACKDiffAmount As New GridViewSummaryItem("ACK Diff Amount", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(ACKDiffAmount)
        Dim DispatchQty As New GridViewSummaryItem("Dispatch Qty", "", GridAggregateFunction.Sum)
        summaryRowItem.Add(DispatchQty)
        Dim DispatchAmount As New GridViewSummaryItem("Dispatch Amount", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(DispatchAmount)

        Dim DispatchFatKG As New GridViewSummaryItem("Dispatch FATKG", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(DispatchFatKG)
        Dim DispatchSNFKG As New GridViewSummaryItem("Dispatch SNFKG", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(DispatchSNFKG)

        'Dim DispatchFatPer As New GridViewSummaryItem("Dispatch FatPer", "{0:F2}", "sum(Dispatch FATKG)*100/sum(Dispatch Qty)")
        'summaryRowItem.Add(DispatchFatPer)
        'Dim DispatchSNFPer As New GridViewSummaryItem("Dispatch SNFPer", "{0:F2}", "sum(Dispatch SNFKG)*100/sum(Dispatch Qty)")
        'summaryRowItem.Add(DispatchSNFPer)

        Dim ACKFatKG As New GridViewSummaryItem("ACK FATKG", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(ACKFatKG)
        Dim ACKSNFKG As New GridViewSummaryItem("ACK SNFKG", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(ACKSNFKG)

        'Dim ACKFatPer As New GridViewSummaryItem("ACK FatPer", "{0:F2}", "sum(ACK FATKG)*100/sum(ACK Qty)")
        'summaryRowItem.Add(ACKFatPer)
        'Dim ACKSNFPer As New GridViewSummaryItem("ACK SNFPer", "{0:F2}", "sum(ACK SNFKG)*100/sum(ACK Qty)")
        'summaryRowItem.Add(ACKSNFPer)

        gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Reset()
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        Try

            If gv1.Rows.Count > 0 Then
                Dim style As New GridPrintStyle()
                style.PrintGrouping = True
                style.HeaderCellBackColor = Color.White
                style.GroupRowBackColor = Color.White
                style.SummaryCellBackColor = Color.White
                style.PrintSummaries = True
                gv1.PrintStyle = style

                Dim doc As New clsMyPrintDocument()

                doc.Margins.Top = 50
                doc.Margins.Bottom = 50
                doc.Margins.Left = 50
                doc.Margins.Right = 50
                doc.HeaderHeight = 90
                doc.Landscape = True
                doc.AssociatedObject = gv1

                doc.DocumentName = objCommonVar.CurrentCompanyName
                doc.MiddleHeader = clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.BulkSaleAcknowledgementUploaderReport & "'")
                doc.LeftHeader = "Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy") + Environment.NewLine & "Company : " & objCommonVar.CurrentCompanyName + Environment.NewLine + Environment.NewLine + "Customer: " + txtCustomer.Value
                doc.RightHeader = "Print Date(" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd-MMM-yyyy hh:mm : ss tt") + ")"
                doc.HeaderFont = New Font("Segoe UI", 10, FontStyle.Bold)

                doc.AssociatedObject = gv1

                doc.RightFooter = "Page [Page #] Of [Total Pages]"

                Dim dialog As New RadPrintPreviewDialog
                dialog.Document = doc
                dialog.ToolMenu.Visible = True
                dialog.Show()

                doc.Print()
            Else
                clsCommon.MyMessageBoxShow(Me, "No data found To export", Me.Text)
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        Try
            If gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Print Date (" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd-MMM-yyyy hh:mm:ss tt") + ")")
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptDailyQtyReport & "'"))
                arrHeader.Add("Date Range : " & clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + "  To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy"))

                If clsCommon.myLen(txtCustomer.Value) > 0 Then
                    arrHeader.Add("Customer : " & txtCustomer.Value)
                End If

                transportSql.QuickExportToExcel(gv1, "", Me.Text, , arrHeader)
            Else
                Throw New Exception("No data found to export.")

            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
End Class