Imports Microsoft.VisualBasic
Imports XpertERPEngine
Imports System
Imports System.Drawing
Imports System.Windows.Forms
Imports System.Data.SqlClient
Imports Telerik.WinControls
Imports Telerik.WinControls.UI
Imports Telerik.WinControls.Data
Imports Telerik.WinControls.Enumerations
Imports Excel = Microsoft.Office.Interop.Excel
Imports System.Text.RegularExpressions
Imports common
Public Class FrmRECEIPTSAGAINSTSALES_FILLED_
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim userCode, companyCode As String
    Sub LoadReceipt()
        Dim strquery As String = " SELECT DISTINCT TSPL_RECEIPT_DETAIL.Receipt_No AS [Receipt No],convert(varchar,TSPL_RECEIPT_HEADER .Receipt_Date,103) as [Description] FROM TSPL_RECEIPT_DETAIL  left outer join  TSPL_RECEIPT_HEADER on TSPL_RECEIPT_HEADER.Receipt_No=TSPL_RECEIPT_DETAIL.Receipt_No where (Receipt_Date >= Convert(DATE,'" + dtpfrom.Value + "',103) AND Receipt_Date <= CONVERT(DATE,'" + dtpto.Value + "',103))"
        cbgReceipt.DataSource = clsDBFuncationality.GetDataTable(strquery)
        cbgReceipt.ValueMember = "Receipt No"
        cbgReceipt.DisplayMember = "Description"
    End Sub
    Sub LoadCustomer()
        Dim strquery As String = "select cust_code as [Customer Code], Customer_Name as [Customer Name] from tspl_customer_master"
        cbgCustomer.DataSource = clsDBFuncationality.GetDataTable(strquery)
        cbgCustomer.ValueMember = "Customer Code"
        cbgCustomer.DisplayMember = "Customer Name"
    End Sub
    Private Sub chkReceiptAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkReceiptAll.ToggleStateChanged, chkReceiptSelect.ToggleStateChanged
        'cbgReceipt.Enabled = Not chkReceiptAll.IsChecked
        'If chkReceiptSelect.IsChecked Then
        '    chkCustomerAll.IsChecked = chkReceiptSelect.IsChecked
        'End If
        cbgReceipt.Enabled = Not chkReceiptAll.IsChecked
    End Sub
    Private Sub chkCustomerAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkCustomerAll.ToggleStateChanged, chkCustomerSelect.ToggleStateChanged
        'cbgCustomer.Enabled = Not chkCustomerAll.IsChecked
        'If chkCustomerSelect.IsChecked Then
        '    chkReceiptAll.IsChecked = chkCustomerSelect.IsChecked
        'End If
        cbgCustomer.Enabled = Not chkCustomerAll.IsChecked
    End Sub
    Sub LoadVehicle()
        Dim qry As String = "Select Vehicle_Id as [Vehicle Code],Number as [Vehicle No] from TSPL_VEHICLE_MASTER ORDER BY Number"
        cbgVehicle.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgVehicle.ValueMember = "Vehicle Code"
        cbgVehicle.DisplayMember = "Vehicle Code"
    End Sub
    Private Sub dtpfrom_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpfrom.ValueChanged
        LoadReceipt()
    End Sub
    Private Sub dtpto_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpto.ValueChanged
        LoadReceipt()
    End Sub
    Private Sub ChkVehicleAll_ToggleStateChanged(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles ChkVehicleAll.ToggleStateChanged
        cbgVehicle.Enabled = Not ChkVehicleAll.IsChecked
    End Sub

    Private Sub FrmRECEIPTSAGAINSTSALES_FILLED__KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Control AndAlso e.KeyCode = Keys.P Then
            Print(EnumExportTo.refersh)
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag Then
            'SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isPostFlag Then
            'DeleteData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()

        End If
    End Sub

    Private Sub FrmRECEIPTSAGAINSTSALES_FILLED__Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        Reset()
    End Sub
    Sub Reset()
        dtpfrom.Value = clsCommon.GETSERVERDATE().Date
        dtpto.Value = clsCommon.GETSERVERDATE().Date
        chkCustomerAll.IsChecked = True
        chkReceiptAll.IsChecked = True
        ChkVehicleAll.IsChecked = True
        LoadReceipt()
        LoadCustomer()
        LoadVehicle()
        ChkGroup.Checked = False
        ButtonToolTip.SetToolTip(btnprint, "Press Ctrl+P for Print")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        RadPageView1.SelectedPage = RadPageViewPage1
        gv1.DataSource = Nothing
    End Sub
    Private Sub btnprint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnprint.Click
        Print(EnumExportTo.refersh)
    End Sub

    Sub Print(ByVal exporter As EnumExportTo)
        If chkVehicleSelect.IsChecked = True AndAlso cbgVehicle.CheckedValue.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please select atleast one Vechile")
            Return
        End If
        If chkCustomerSelect.IsChecked = True AndAlso cbgCustomer.CheckedValue.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please select atleast one Customer")
            Return
        End If
        If chkReceiptSelect.IsChecked = True AndAlso cbgReceipt.CheckedValue.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please select atleast one Receipt")
            Return
        End If
        Dim fromdate As String = clsCommon.GetPrintDate(dtpfrom.Value, "dd/MM/yyyy")
        Dim todate As String = clsCommon.GetPrintDate(dtpto.Value, "dd/MM/yyyy")

        Try


            Dim strquery As String = " select TSPL_RECEIPT_DETAIL.Receipt_No  as [RECEIPTNO] ,convert(varchar,TSPL_RECEIPT_HEADER .Receipt_Date,103) as [RECP. DATE],CONVERT(VARCHAR,TSPL_RECEIPT_DETAIL.Applied_Amount,103) as [AMOUNT],TSPL_RECEIPT_DETAIL.Document_No  as [INVOICE NO],convert(varchar,TSPL_RECEIPT_DETAIL.Document_Date,103) as [INV.DATE],TSPL_SALE_INVOICE_HEAD.Vehicle_No,TSPL_RECEIPT_HEADER.Salesman_Name as [SALESMAN NAME],TSPL_RECEIPT_HEADER.Customer_Name as [CUSTOMER NAME],isnull (TSPL_SALE_INVOICE_HEAD.Total_Invoice_Amt,0) as [INV. AMOUNT] ,(isnull (TSPL_SALE_INVOICE_HEAD.Total_Invoice_Amt,0)-(TSPL_RECEIPT_DETAIL.Applied_Amount)) as BALANCE "
            strquery += "   from TSPL_RECEIPT_DETAIL "
            strquery += "  left outer join  TSPL_RECEIPT_HEADER on TSPL_RECEIPT_HEADER.Receipt_No=TSPL_RECEIPT_DETAIL.Receipt_No "
            strquery += " left outer join  TSPL_SALE_INVOICE_HEAD on TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No=TSPL_RECEIPT_DETAIL.Document_No "
            strquery += " WHERE   2=2 "
            If clsCommon.myLen(fromdate) > 0 Then
                strquery += " and convert(date,TSPL_RECEIPT_HEADER .Receipt_Date,103) >= convert(date,'" + fromdate + "',103)"
            End If

            If clsCommon.myLen(todate) > 0 Then
                strquery += " and convert(date,TSPL_RECEIPT_HEADER .Receipt_Date,103) <= convert(date,'" + todate + "',103)"
            End If

            If chkReceiptSelect.IsChecked AndAlso cbgReceipt.CheckedValue.Count > 0 Then
                strquery += "and TSPL_RECEIPT_DETAIL.Receipt_No in(" + clsCommon.GetMulcallString(cbgReceipt.CheckedValue) + ")"
            End If
            If chkCustomerSelect.IsChecked AndAlso cbgCustomer.CheckedValue.Count > 0 Then
                strquery += "and TSPL_RECEIPT_HEADER.Cust_Code in (" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ")"
            End If
            If chkVehicleSelect.IsChecked AndAlso cbgVehicle.CheckedValue.Count > 0 Then
                strquery += "and TSPL_SALE_INVOICE_HEAD.Vehicle_Code in (" + clsCommon.GetMulcallString(cbgVehicle.CheckedValue) + ")"
            End If
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(strquery)

            gv1.DataSource = Nothing
            RadPageView1.SelectedPage = RadPageViewPage2
            If dt.Rows.Count > 0 Then
                gv1.DataSource = dt
                gridformat()
                If exporter = 0 Then
                    ExportToExcelGV(exporter)
                ElseIf exporter = 1 Then
                    ExportToExcelGV(exporter)

                End If
            Else
                clsCommon.MyMessageBoxShow("No data found to display")
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message.ToString())
        End Try
        'PrintData(fromdate, todate, chkReceiptSelect.IsChecked, arrreceipt, chkCustomerSelect.IsChecked, arrcustomer, chkVehicleSelect.IsChecked, arrlocation)
    End Sub
    Private Sub ExportToExcelGV(ByVal exporter As EnumExportTo)
        Try
            Dim arrHeader As List(Of String) = New List(Of String)()
            Dim strTemp As String = ""


            arrHeader.Add("From Date : " + clsCommon.GetPrintDate(dtpfrom.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(dtpto.Value, "dd/MM/yyyy") + " ")
  
            If chkCustomerSelect.IsChecked Then
                strTemp = ""
                For Each Str As String In cbgCustomer.CheckedDisplayMember
                    If clsCommon.myLen(strTemp) > 0 Then
                        strTemp += ", "
                    End If
                    strTemp += Str
                Next
                arrHeader.Add("Customer  : " + strTemp)
            End If
            If chkVehicleSelect.IsChecked Then
                strTemp = ""
                For Each Str As String In cbgVehicle.CheckedDisplayMember
                    If clsCommon.myLen(strTemp) > 0 Then
                        strTemp += ", "
                    End If
                    strTemp += Str
                Next
                arrHeader.Add("Vehcile : " + strTemp)
            End If
            If chkReceiptSelect.IsChecked Then
                strTemp = ""
                For Each Str As String In cbgReceipt.CheckedDisplayMember
                    If clsCommon.myLen(strTemp) > 0 Then
                        strTemp += ", "
                    End If
                    strTemp += Str
                Next
                arrHeader.Add("Receipt : " + strTemp)
            End If
            ' clsCommon.MyExportToExcel("Other Party Sale", gv1, arrHeader, "OtherPartySale")

            If exporter = EnumExportTo.PDF Then
            
                clsCommon.MyExportToPDF("Receipts Against Sales (FILLED)", gv1, arrHeader, "ReceiptsAgainstSale(FILLED)", True)
            Else
                clsCommon.MyExportToExcelGrid("Receipts Against Sales (FILLED)", gv1, arrHeader, "ReceiptsAgainstSale(FILLED)")
            End If
        Catch ex As Exception
            clsCommon.ProgressBarHide()
            common.clsCommon.MyMessageBoxShow(ex.Message)
        Finally
            clsCommon.ProgressBarHide()
        End Try
    End Sub
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.receiptFillreport)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        'btnSave.Visible = MyBase.isModifyFlag
        'btnPost.Visible = MyBase.isPostFlag
        'btnDelete.Visible = MyBase.isDeleteFlag
    End Sub
    Public Enum EnumExportTo
        Excel = 0
        PDF = 1
        refersh = 2
    End Enum


  

    Public Sub gridformat()
        Try
            gv1.MasterTemplate.SummaryRowsBottom.Clear()
            gv1.AllowAddNewRow = False
            gv1.TableElement.TableHeaderHeight = 40
            gv1.MasterTemplate.ShowRowHeaderColumn = False
            For ii As Integer = 0 To gv1.Columns.Count - 1
                gv1.Columns(ii).ReadOnly = True
                gv1.Columns(ii).Width = 200
                'gv1.Columns(gv1.Columns.Count - 1).Width = 100
              
            Next
            gv1.Columns("INV. AMOUNT").FormatString = "{0:F2}"
            gv1.Columns("AMOUNT").FormatString = "{0:F2}"
            gv1.Columns("BALANCE").FormatString = "{0:F2}"
            gv1.EnableGrouping = True
            If ChkGroup.Checked Then
                gv1.GroupDescriptors.Add(New GridGroupByExpression("RECEIPTNO as RECEIPTNO format ""{0}: {2}"" Group By RECEIPTNO"))
            End If

            gv1.MasterTemplate.ExpandAllGroups()
            gv1.ShowGroupPanel = False
            gv1.MasterTemplate.AutoExpandGroups = True
            Dim summaryRowItem As New GridViewSummaryRowItem()
            Dim item1 As New GridViewSummaryItem("INV. AMOUNT", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item1)
            Dim item2 As New GridViewSummaryItem("AMOUNT", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item2)
            Dim item3 As New GridViewSummaryItem("BALANCE", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item3)
            gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)



        Catch ex As Exception

        End Try

    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub RadMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem2.Click
        Print(EnumExportTo.PDF)
    End Sub

    Private Sub RadMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem1.Click
        Print(EnumExportTo.Excel)
    End Sub

    Private Sub RadButton3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadButton3.Click
        Reset()
    End Sub
End Class
