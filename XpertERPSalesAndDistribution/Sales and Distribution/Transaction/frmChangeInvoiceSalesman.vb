'----Created By--Pankaj  Kuamr 12/12/12
'--14/12/2012-1:15PM--Updation By--Pankaj Kumar----Change SalesMan in Sale Invoice, Shipment, Empty Transaction Against Transfer------Fwd By---Ranjana Mam
'--14/12/2012-1:15PM--Updation By--Pankaj Kumar----Change sale invoice as credit Invoice if Customer is Credit Customer------Fwd By---Ranjana Mam
'--17/12/2012-6:25PM--Updation By--Pankaj Kumar----Added Type Empty------Fwd By---Ranjana Mam
'26/12/2012-5:45PM-Updation By--Pankaj Kumar--Change Salesman in Sale return Also---fwd By--Ranjana Mam
Imports common
Imports XpertERPEngine
Imports System.Data.SqlClient
Imports Telerik.WinControls.UI

Public Class FrmChangeInvoiceSalesman
    Inherits FrmMainTranScreen

    Private Sub FrmChangeInvoiceSalesman_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        gvInvoice.AllowAddNewRow = False
        LoadLocation()
        LoadCustomer()
        Reset()
    End Sub

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.ChangeInvoiceSalesman)

        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnChange.Visible = MyBase.isModifyFlag
    End Sub

    Private Sub Reset()
        ddlType.Text = "Sale"
        txtFromDate.Value = clsCommon.GETSERVERDATE()
        txtToDate.Value = clsCommon.GETSERVERDATE()
        chkLocAll.IsChecked = True
        chkCustomerAll.IsChecked = True
        gvInvoice.DataSource = Nothing
    End Sub

    Sub LoadLocation()
        Dim strquery As String = "SELECT Location_Code AS [Code], Location_Desc AS [Description] FROM TSPL_LOCATION_MASTER where Location_Type='Physical' "
        cbgLocation.DataSource = clsDBFuncationality.GetDataTable(strquery)
        cbgLocation.ValueMember = "Code"
        cbgLocation.DisplayMember = "Name"
    End Sub

    Sub LoadCustomer()
        Dim strquery As String = "Select Cust_Code as Code, Customer_Name as Name from TSPL_CUSTOMER_MASTER"
        cbgCustomer.DataSource = clsDBFuncationality.GetDataTable(strquery)
        cbgCustomer.ValueMember = "Code"
        cbgCustomer.DisplayMember = "Name"
    End Sub

    Private Sub chkLocAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocAll.ToggleStateChanged
        cbgLocation.Enabled = False
    End Sub

    Private Sub chkLocSelect_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocSelect.ToggleStateChanged
        cbgLocation.Enabled = True
    End Sub

    Private Sub chkCustomerAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkCustomerAll.ToggleStateChanged
        cbgCustomer.Enabled = False
    End Sub

    Private Sub chkCustomerSelect_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkCustomerSelect.ToggleStateChanged
        cbgCustomer.Enabled = True
    End Sub

    Private Sub btnShow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShow.Click
        ShowData()
    End Sub

    Public Sub ShowData()
        If chkLocSelect.IsChecked = True AndAlso cbgLocation.CheckedValue.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please Select Atleast Single Location Or Select All")
            Exit Sub
        ElseIf chkCustomerSelect.IsChecked = True AndAlso cbgCustomer.CheckedValue.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please Select Atleast Single Customer Or Select All")
            Exit Sub
        End If

        Dim Type As String
        If ddlType.Text = "Transfer" Then
            Type = " AND TSPL_SALE_INVOICE_HEAD.Shipment_Type='Transfer' "
        Else
            Type = " AND TSPL_SALE_INVOICE_HEAD.Shipment_Type='Sale' "
        End If
        Dim qry As String = ""
        Try
            If ddlType.Text <> "Empty" Then
                qry = "Select TSPL_SALE_INVOICE_HEAD.Shipment_No as [Shipment No], TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No as [Invoice No], "
                qry += " Convert(Varchar,TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date, 103) as Date, TSPL_SALE_INVOICE_HEAD.Location, "
                qry += " TSPL_SALE_INVOICE_HEAD.Cust_Code as [Customer Code], TSPL_CUSTOMER_MASTER.Customer_Name as [Customer Name], "
                qry += " Total_Invoice_Amt as [Amount], TSPL_SALE_INVOICE_HEAD.Salesman_Code as [Current Salesman Code], "
                qry += " TSPL_GL_SEGMENT_CODE.Description as [Current Salesman Name], '' as [New Salesman Code], '' as [New Salesman Name], "
                qry += " case when TSPL_SALE_INVOICE_HEAD.Credit_Invoice='Y' Then Cast(1 AS Bit) Else Case When TSPL_CUSTOMER_MASTER.Credit_Customer='Y' Then CAST(0 AS Bit) Else CAST(0 as Bit) End End as [Is Credit], "
                qry += " case when TSPL_SALE_INVOICE_HEAD.Credit_Invoice='Y' Then 'Lock' Else Case When TSPL_CUSTOMER_MASTER.Credit_Customer='Y' Then 'Unlock' Else 'Lock' End End as [IsLock] "
                qry += " from TSPL_SALE_INVOICE_HEAD "
                qry += " Left Outer Join TSPL_GL_SEGMENT_CODE on TSPL_GL_SEGMENT_CODE.Segment_code=TSPL_SALE_INVOICE_HEAD.Salesman_Code "
                qry += " Left Outer Join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SALE_INVOICE_HEAD.Cust_Code  "
                qry += " Where TSPL_GL_SEGMENT_CODE.Seg_No=4 " + Type + "  AND TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date >=CONVERT(Date, '" + txtFromDate.Value + "', 103) "
                qry += " AND TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date <=CONVERT(Date, '" + txtToDate.Value + "', 103) "
                If chkLocSelect.IsChecked = True Then
                    qry += " AND TSPL_SALE_INVOICE_HEAD.Location in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
                End If
                If chkCustomerSelect.IsChecked = True Then
                    qry += " AND TSPL_SALE_INVOICE_HEAD.Cust_Code in (" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ") "
                End If
                qry += " Order By TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date, Sale_Invoice_No"
            Else
                qry = "Select TSPL_ADJUSTMENT_HEADER.Adjustment_No as [Adjustment No], TSPL_ADJUSTMENT_HEADER.Adjustment_No as [Invoice No],  "
                qry += " Convert(Varchar,TSPL_ADJUSTMENT_HEADER.Adjustment_Date, 103) as Date, TSPL_ADJUSTMENT_HEADER.Loc_Code as Location,  "
                qry += " TSPL_ADJUSTMENT_HEADER.Customer_CODE as [Customer Code], TSPL_ADJUSTMENT_HEADER.Customer_Name as [Customer Name], "
                qry += " (Select SUM(TSPL_ADJUSTMENT_DETAIL.Item_Cost) from TSPL_ADJUSTMENT_DETAIL Where TSPL_ADJUSTMENT_DETAIL.Adjustment_No=TSPL_ADJUSTMENT_HEADER.Adjustment_No) as [Amount],  "
                qry += " TSPL_ADJUSTMENT_HEADER.EMP_CODE as [Current Salesman Code], TSPL_EMPLOYEE_MASTER.EMP_NAME as [Current Salesman Name], "
                qry += " '' as [New Salesman Code], '' as [New Salesman Name]  "
                qry += " from TSPL_ADJUSTMENT_HEADER Left Outer Join TSPL_EMPLOYEE_MASTER ON TSPL_EMPLOYEE_MASTER.EMP_CODE=TSPL_ADJUSTMENT_HEADER.EMP_CODE "
                qry += " Where TSPL_ADJUSTMENT_HEADER.ItemType='E' "
                qry += " AND ISNULL(Reference_Document, '')='' AND ISNULL(Customer_CODE, '')<>'' "
                qry += " AND Convert(DAte,TSPL_ADJUSTMENT_HEADER.Adjustment_Date, 103) >=CONVERT(Date, '" + txtFromDate.Value + "', 103)  "
                qry += " AND Convert(Date, TSPL_ADJUSTMENT_HEADER.Adjustment_Date, 103) <=CONVERT(Date, '" + txtToDate.Value + "', 103)"
                If chkLocSelect.IsChecked = True Then
                    qry += " AND TSPL_ADJUSTMENT_HEADER.Loc_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
                End If
                If chkCustomerSelect.IsChecked = True Then
                    qry += " AND TSPL_ADJUSTMENT_HEADER.Customer_CODE in (" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ") "
                End If
                qry += "  Order By TSPL_ADJUSTMENT_HEADER.Adjustment_Date, TSPL_ADJUSTMENT_HEADER.Adjustment_No "
            End If

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            gvInvoice.DataSource = Nothing
            If dt.Rows.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("No Data Found")
            Else
                gvInvoice.DataSource = dt
                FormatGrid()
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub FormatGrid()
        If ddlType.Text <> "Empty" Then
            gvInvoice.Columns("Shipment No").ReadOnly = True
            gvInvoice.Columns("Shipment No").Width = 121

            gvInvoice.Columns("Invoice No").ReadOnly = True
            gvInvoice.Columns("Invoice No").Width = 131
        Else
            gvInvoice.Columns("Adjustment No").ReadOnly = True
            gvInvoice.Columns("Adjustment No").Width = 131
        End If


        gvInvoice.Columns("Date").ReadOnly = True
        gvInvoice.Columns("Date").Width = 81

        gvInvoice.Columns("Location").ReadOnly = True
        gvInvoice.Columns("Location").Width = 71

        gvInvoice.Columns("Customer Code").ReadOnly = True
        gvInvoice.Columns("Customer Code").Width = 101

        gvInvoice.Columns("Customer Name").ReadOnly = True
        gvInvoice.Columns("Customer Name").Width = 201

        gvInvoice.Columns("Amount").ReadOnly = True
        gvInvoice.Columns("Amount").Width = 101

        gvInvoice.Columns("Current Salesman Code").ReadOnly = True
        gvInvoice.Columns("Current Salesman Code").Width = 101

        gvInvoice.Columns("Current Salesman Name").ReadOnly = True
        gvInvoice.Columns("Current Salesman Name").Width = 201

        If ddlType.Text = "Transfer" Then
            gvInvoice.Columns("New Salesman Code").IsVisible = False
            gvInvoice.Columns("New Salesman Name").IsVisible = False
        Else
            gvInvoice.Columns("New Salesman Code").ReadOnly = False
            gvInvoice.Columns("New Salesman Code").Width = 101

            gvInvoice.Columns("New Salesman Name").ReadOnly = True
            gvInvoice.Columns("New Salesman Name").Width = 201
        End If

        If ddlType.Text <> "Empty" Then
            gvInvoice.Columns("Is Credit").Width = 81

            For Each grow As GridViewRowInfo In gvInvoice.Rows
                If grow.Cells("IsLock").Value = "Unlock" Then
                    grow.Cells("Is Credit").ReadOnly = False
                Else
                    grow.Cells("Is Credit").ReadOnly = True
                End If
                'Dim Qry As String = ""
                'If clsDBFuncationality.getSingleValue("Select Credit_Invoice from TSPL_SALE_INVOICE_HEAD  Where Sale_Invoice_No='" + clsCommon.myCstr(grow.Cells("Invoice No").Value) + "'") = "Y" Then
                '    grow.Cells("Is Credit").Value = True
                '    grow.Cells("Is Credit").ReadOnly = True
                'ElseIf clsDBFuncationality.getSingleValue("Select Credit_Customer  from TSPL_CUSTOMER_MASTER Where Cust_Code='" + clsCommon.myCstr(grow.Cells("Customer Code").Value) + "'") = "Y" Then
                '    grow.Cells("Is Credit").Value = False
                '    grow.Cells("Is Credit").ReadOnly = False
                'Else
                '    grow.Cells("Is Credit").Value = False
                '    grow.Cells("Is Credit").ReadOnly = True
                'End If
            Next
            gvInvoice.Columns("IsLock").IsVisible = False
        End If
    End Sub

    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Reset()
    End Sub

    Private Sub btnChange_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnChange.Click
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If ddlType.Text = "Empty" Then
                For i As Integer = 0 To gvInvoice.Rows.Count - 1
                    If clsCommon.myCstr(gvInvoice.Rows(i).Cells("New Salesman Code").Value) <> "" Then
                        Dim SalesmanCode As String = clsCommon.myCstr(gvInvoice.Rows(i).Cells("New Salesman Code").Value)
                        Dim SalesmanName As String = clsCommon.myCstr(gvInvoice.Rows(i).Cells("New Salesman Name").Value)
                        Dim AdjustmentNo As String = clsCommon.myCstr(gvInvoice.Rows(i).Cells("Invoice No").Value)
                        Dim qry As String = "Update TSPL_ADJUSTMENT_HEADER set EMP_CODE='" + SalesmanCode + "', EMP_NAME='" + SalesmanName + "' Where ItemType='E' AND Adjustment_No='" + AdjustmentNo + "'"
                        clsDBFuncationality.ExecuteNonQuery(qry, trans)
                    End If
                Next
            Else
                For i As Integer = 0 To gvInvoice.Rows.Count - 1
                    If clsCommon.myCstr(gvInvoice.Rows(i).Cells("New Salesman Code").Value) <> "" Then
                        Dim SalesmanCode As String = clsCommon.myCstr(gvInvoice.Rows(i).Cells("New Salesman Code").Value)
                        Dim SalesmanName As String = clsCommon.myCstr(gvInvoice.Rows(i).Cells("New Salesman Name").Value)
                        Dim SaleInvoiceNo As String = clsCommon.myCstr(gvInvoice.Rows(i).Cells("Invoice No").Value)
                        Dim Shipmentno As String = clsCommon.myCstr(gvInvoice.Rows(i).Cells("Shipment No").Value)
                        clsERPFuncationality.ChangeSalesman(trans, SaleInvoiceNo, Shipmentno, SalesmanCode, SalesmanName)
                    End If
                    If clsCommon.myCstr(gvInvoice.Rows(i).Cells("Is Credit").Value) = True Then
                        Dim SaleInvoiceNo As String = clsCommon.myCstr(gvInvoice.Rows(i).Cells("Invoice No").Value)
                        ChangeInvoiceAsCredit(trans, SaleInvoiceNo)
                    End If
                Next
            End If
            common.clsCommon.MyMessageBoxShow("Changed Succcessfully")
            trans.Commit()
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            trans.Rollback()
        End Try
    End Sub

    Public Sub ChangeInvoiceAsCredit(ByVal trans As SqlTransaction, ByVal saleInvoiceNo As String)
        Try
            Dim creditInvoice As String = "Update TSPL_SALE_INVOICE_HEAD set Credit_Invoice='Y' Where Sale_Invoice_No ='" + saleInvoiceNo + "'"
            clsDBFuncationality.ExecuteNonQuery(creditInvoice, trans)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub gvInvoice_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvInvoice.CellValueChanged
        If e.Column.Name = "New Salesman Code" Then
            OpenSalesmanList(False)
        End If
    End Sub

    Public Sub OpenSalesmanList(ByVal IsButtonClicked As Boolean)
        Dim qry As String = "Select EMP_CODE as Code, Emp_Name as Name  from TSPL_EMPLOYEE_MASTER "
        gvInvoice.CurrentRow.Cells("New Salesman Code").Value = clsCommon.ShowSelectForm("SalesmanChange", qry, "Code", "Emp_type='Salesman'", clsCommon.myCstr(gvInvoice.CurrentRow.Cells("New Salesman Code").Value), "Code", IsButtonClicked)
        gvInvoice.CurrentRow.Cells("New Salesman Name").Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Description as Name from TSPL_GL_SEGMENT_CODE Where Seg_No=4 AND Segment_code='" + clsCommon.myCstr(gvInvoice.CurrentRow.Cells("New Salesman Code").Value) + "'"))
    End Sub
End Class
