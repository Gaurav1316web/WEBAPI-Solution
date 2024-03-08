
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common
Imports System.IO

Public Class FrmShipmentReport
    Inherits FrmMainTranScreen

    Dim ButtonToolTip As ToolTip = New ToolTip()

    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        RadSplitButton1.Visible = MyBase.isExport
    End Sub

    Private Sub FrmSecurityLevel_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnGenrate, "Press Alt+R Refresh ")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N ")
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = txtToDate.Value.AddMonths(-1)
        RadPageView1.SelectedPage = RadPageViewPage1
        Gv1.ReadOnly = True
    End Sub


    Sub Print(ByVal IsPrint As Exporter)
        Try
            If clsCommon.GetDateWithStartTime(txtFromDate.Value) > clsCommon.GetDateWithStartTime(txtToDate.Value) Then
                Throw New Exception("From date can not be greater then to Date")
            End If
            ''BHA/07/09/18-000522 by balwinder on 14/05/2019
            Dim strQry As String = "select *,(SIAmount-ShipmentAmount+AdjAmount) as Diff from (" + Environment.NewLine + _
            "select max(Trans_Type) as Trans_Type, max(case when RI=1 then ShipmentNO else null end) as ShipmentNO, max(case when RI=1 then Source_Doc_Date else null end) as ShipmentDate, max(case when RI=1 then Account_code else null end) as ShipmentAccount_code,sum(case when RI=1 then Amount else 0 end) as ShipmentAmount, SI_No, max(case when RI=2 then Source_Doc_Date else null end) as SIDate, max(case when RI=2 then Account_code else null end) as SIAccount_code,sum(case when RI=2 then Amount else 0 end) as SIAmount,sum(case when RI=3 then Amount else 0 end) as AdjAmount from (" + Environment.NewLine + _
            "select TAB_PI.Trans_Type ,TAB_PI.SI_No, TSPL_JOURNAL_MASTER.Source_Doc_No as ShipmentNO,TSPL_JOURNAL_MASTER.Source_Doc_Date,TSPL_JOURNAL_DETAILS.Account_code,ABS(TSPL_JOURNAL_DETAILS.Amount) as Amount,1 as RI,1 as Chk" + Environment.NewLine + _
            "from TSPL_JOURNAL_DETAILS" + Environment.NewLine + _
            "left outer join TSPL_JOURNAL_MASTER on TSPL_JOURNAL_MASTER.Voucher_No= TSPL_JOURNAL_DETAILS.Voucher_No" + Environment.NewLine + _
            "left outer join (select TSPL_SD_SHIPMENT_HEAD.Document_Code as ShipmentNo,TSPL_SD_SALE_INVOICE_HEAD.Document_Code as SI_No,TSPL_SD_SHIPMENT_HEAD.Trans_Type from TSPL_SD_SHIPMENT_HEAD left outer join  TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Against_Shipment_No=TSPL_SD_SHIPMENT_HEAD.Document_Code group by TSPL_SD_SHIPMENT_HEAD.Document_Code,TSPL_SD_SALE_INVOICE_HEAD.Document_Code,TSPL_SD_SHIPMENT_HEAD.Trans_Type  ) as TAB_PI on TAB_PI.ShipmentNo=TSPL_JOURNAL_MASTER.Source_Doc_No" + Environment.NewLine + _
            "where  TSPL_JOURNAL_MASTER.Source_Code='SD-SH' and TSPL_JOURNAL_DETAILS.Reco_Control_Account='H' and TSPL_JOURNAL_MASTER.Source_Doc_Date>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' and TSPL_JOURNAL_MASTER.Source_Doc_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' " + Environment.NewLine
            If txtCustomer.arrValueMember IsNot Nothing AndAlso txtCustomer.arrValueMember.Count > 0 Then
                strQry += " and TSPL_JOURNAL_MASTER.Source_Type='C' and TSPL_JOURNAL_MASTER.CustVend_Code in (" + clsCommon.GetMulcallString(txtCustomer.arrValueMember) + ")" + Environment.NewLine
            End If

            strQry += "union all" + Environment.NewLine + _
            "select '' as Trans_Type, TSPL_Customer_Invoice_Head.Against_Sale_No as SI_No,'' as ShipmentNO,TSPL_JOURNAL_MASTER.Source_Doc_Date ,TSPL_JOURNAL_DETAILS.Account_code,ABS(TSPL_JOURNAL_DETAILS.Amount) as Amount,2 as RI,0 as Chk from TSPL_JOURNAL_DETAILS" + Environment.NewLine + _
            "Inner join TSPL_JOURNAL_MASTER on TSPL_JOURNAL_MASTER.Voucher_No=TSPL_JOURNAL_DETAILS.Voucher_No" + Environment.NewLine + _
            "inner join TSPL_Customer_Invoice_Head on TSPL_Customer_Invoice_Head.Document_No=TSPL_JOURNAL_MASTER.Source_Doc_No" + Environment.NewLine + _
            "where TSPL_JOURNAL_MASTER.Source_Code='AR-IN' and TSPL_JOURNAL_DETAILS.Reco_Control_Account='H' and TSPL_Customer_Invoice_Head.Document_Type='I' and len(isnull( TSPL_Customer_Invoice_Head.Against_Sale_No,''))>0   " + Environment.NewLine
            If txtCustomer.arrValueMember IsNot Nothing AndAlso txtCustomer.arrValueMember.Count > 0 Then
                strQry += " and TSPL_JOURNAL_MASTER.Source_Type='C' and TSPL_JOURNAL_MASTER.CustVend_Code in (" + clsCommon.GetMulcallString(txtCustomer.arrValueMember) + ")" + Environment.NewLine
            End If
            strQry += ")xx group by SI_No having sum(Chk)>0 " + Environment.NewLine + _
            ")xxx where 2=2 order by ShipmentDate "


            Dim dt As DataTable = clsDBFuncationality.GetDataTable(strQry)
            Gv1.DataSource = Nothing
            Gv1.Rows.Clear()
            Gv1.Columns.Clear()
            Gv1.MasterTemplate.SummaryRowsBottom.Clear()
            Gv1.DataSource = dt
            Gv1.GroupDescriptors.Clear()
            Gv1.MasterTemplate.BestFitColumns()

            Gv1.EnableFiltering = True
            RadPageView1.SelectedPage = RadPageViewPage2

            Gv1.ReadOnly = True
            btnGenrate.Enabled = True
            SetGridLayout()
            EnableDisableControl(False)
            ReStoreGridLayout()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Sub SetGridLayout()

        Gv1.TableElement.TableHeaderHeight = 20
        Gv1.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To Gv1.Columns.Count - 1
            Gv1.Columns(ii).ReadOnly = True
            Gv1.Columns(ii).IsVisible = True
        Next

        Gv1.Columns("Trans_Type").IsVisible = True
        Gv1.Columns("Trans_Type").Width = 100
        Gv1.Columns("Trans_Type").HeaderText = "Transaction"

        Gv1.Columns("ShipmentNO").IsVisible = True
        Gv1.Columns("ShipmentNO").Width = 200
        Gv1.Columns("ShipmentNO").HeaderText = "Shipment No"

        Gv1.Columns("ShipmentDate").IsVisible = False
        Gv1.Columns("ShipmentDate").Width = 100
        Gv1.Columns("ShipmentDate").HeaderText = "Shipment Date"

        Gv1.Columns("ShipmentAccount_code").IsVisible = True
        Gv1.Columns("ShipmentAccount_code").Width = 100
        Gv1.Columns("ShipmentAccount_code").HeaderText = "Shipment Account No"

        Gv1.Columns("ShipmentAmount").IsVisible = True
        Gv1.Columns("ShipmentAmount").Width = 100
        Gv1.Columns("ShipmentAmount").HeaderText = "Shipment Amount"

        Gv1.Columns("SI_No").IsVisible = True
        Gv1.Columns("SI_No").Width = 100
        Gv1.Columns("SI_No").HeaderText = "Sale Invoice No"

        Gv1.Columns("SIDate").IsVisible = False
        Gv1.Columns("SIDate").Width = 100
        Gv1.Columns("SIDate").HeaderText = "Sale Invoice Date"

        Gv1.Columns("SIAccount_code").IsVisible = True
        Gv1.Columns("SIAccount_code").Width = 100
        Gv1.Columns("SIAccount_code").HeaderText = "Sale Invoice Account"

        Gv1.Columns("SIAmount").IsVisible = True
        Gv1.Columns("SIAmount").Width = 100
        Gv1.Columns("SIAmount").HeaderText = "Sale Invoice Amount"

        Gv1.Columns("AdjAmount").IsVisible = True
        Gv1.Columns("AdjAmount").Width = 100
        Gv1.Columns("AdjAmount").HeaderText = "Adjustment Amount"

        Gv1.Columns("Diff").IsVisible = True
        Gv1.Columns("Diff").Width = 100
        Gv1.Columns("Diff").HeaderText = "Difference Amount"

        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim item2 As New GridViewSummaryItem("ShipmentAmount", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item2)

        Dim item3 As New GridViewSummaryItem("SIAmount", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item3)

        Dim item4 As New GridViewSummaryItem("AdjAmount", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item4)

        Dim item5 As New GridViewSummaryItem("Diff", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item5)
        Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
    End Sub

    

    Private Sub FrmSecurityLevel_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt And e.KeyCode = Keys.R Then
            Print(Exporter.Refresh)
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.N Then
            Reset()
        End If
    End Sub

    Enum Exporter
        Excel = 0
        PDF = 1
        Print = 2
        Refresh = 3
    End Enum

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGenrate.Click
        Try
            PageSetupReport_ID = MyBase.Form_ID
            Print(Exporter.Refresh)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        EnableDisableControl(True)
    End Sub

    Sub EnableDisableControl(ByVal val As Boolean)
        txtFromDate.Enabled = val
        txtToDate.Enabled = val
        txtCustomer.Enabled = val
    End Sub

    Private Sub ExportExcel_PDF(ByVal IsPrint As Exporter)
        Try
            Dim arrHeader As List(Of String) = New List(Of String)()
            Dim strTemp As String = ""
            arrHeader.Add("From Date : From " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy") + " ")
            arrHeader.Add("Company : " + objCommonVar.CurrentCompanyName)

            If txtCustomer.arrDispalyMember Is Nothing OrElse txtCustomer.arrDispalyMember.Count > 0 Then
                Dim stCustomerName As String = ""
                For Each StrName As String In txtCustomer.arrDispalyMember
                    If clsCommon.myLen(stCustomerName) > 0 Then
                        stCustomerName += ", "
                    End If
                    stCustomerName += StrName
                Next
                arrHeader.Add(("Customer : " + stCustomerName + " "))
            End If
            transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
            If IsPrint = Exporter.Excel Then
                clsCommon.MyExportToExcelGrid("Shipment Clearing Report", Gv1, arrHeader, Me.Text)
            ElseIf IsPrint = Exporter.PDF Then
                clsCommon.MyExportToPDF("Shipment Clearing Report", Gv1, arrHeader, "Shipment Clearing Report", PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub btnQuickExport_Click(sender As Object, e As EventArgs)
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()

            arrHeader.Add("Date Range: From " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy"))
            arrHeader.Add("Company : " + objCommonVar.CurrentCompanyName)
            transportSql.QuickExportToExcel(Gv1, "", Me.Text, , arrHeader)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(MyBase.Form_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(Form_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
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

    Private Sub rmSaveLayout_Click(sender As Object, e As EventArgs) Handles rmSaveLayout.Click
        If clsCommon.myLen(MyBase.Form_ID) > 0 Then
            Gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = MyBase.Form_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            Gv1.SaveLayout(obj.GridLayout)
            obj.GridColumns = Gv1.ColumnCount
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully",  Me.Text)
            End If
            
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If

    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(MyBase.Form_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", "Information", Me.Text)
    End Sub

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        Try
            If (Gv1.Rows.Count <= 0) Then
                Throw New Exception("No Data found To Export")
            End If
            ExportExcel_PDF(Exporter.Excel)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        Try
            If (Gv1.Rows.Count <= 0) Then
                Throw New Exception("No Data found To Export")
            End If
            ExportExcel_PDF(Exporter.PDF)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    ' Ticket No : BHA/21/01/19-000787 By Prabhakar
    Private Sub Gv1_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles Gv1.CellDoubleClick
        Try
            If e.Column Is Gv1.Columns("ARinvoiceno") Then
                clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnARInvoiceEntry, clsCommon.myCstr(Gv1.CurrentRow.Cells("ARinvoiceno").Value))
                ' Fresh Sale 
            ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("TransType").Value), "FS") = CompairStringResult.Equal AndAlso e.Column Is Gv1.Columns("invoiceno") Then
                clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmInvoiceFreshSale, clsCommon.myCstr(Gv1.CurrentRow.Cells("invoiceno").Value))
            ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("TransType").Value), "FS") = CompairStringResult.Equal AndAlso e.Column Is Gv1.Columns("shipmentno") Then
                clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmSaleDispatchDairy, clsCommon.myCstr(Gv1.CurrentRow.Cells("shipmentno").Value))
                ' Product Sale 
            ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("TransType").Value), "PS") = CompairStringResult.Equal AndAlso e.Column Is Gv1.Columns("invoiceno") Then
                clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmSaleInvoiceProductSale, clsCommon.myCstr(Gv1.CurrentRow.Cells("invoiceno").Value))
            ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("TransType").Value), "PS") = CompairStringResult.Equal AndAlso e.Column Is Gv1.Columns("shipmentno") Then
                clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmSaleDispatchDairy, clsCommon.myCstr(Gv1.CurrentRow.Cells("shipmentno").Value))
                ' MCC
            ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("TransType").Value), "MCC") = CompairStringResult.Equal AndAlso e.Column Is Gv1.Columns("invoiceno") Then
                clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmMCCMaterial, clsCommon.myCstr(Gv1.CurrentRow.Cells("shipmentno").Value))
            ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("TransType").Value), "MCC") = CompairStringResult.Equal AndAlso e.Column Is Gv1.Columns("shipmentno") Then
                clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmMCCMaterial, clsCommon.myCstr(Gv1.CurrentRow.Cells("shipmentno").Value))
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtCustomer__My_Click(sender As Object, e As EventArgs) Handles txtCustomer._My_Click
        Dim qry As String = "select Cust_Code as [code],Customer_Name as [Name] from TSPL_CUSTOMER_MASTER"
        txtCustomer.arrValueMember = clsCommon.ShowMultipleSelectForm("Cust@Shcle", qry, "Code", "Name", txtCustomer.arrValueMember, txtCustomer.arrDispalyMember)
    End Sub
End Class