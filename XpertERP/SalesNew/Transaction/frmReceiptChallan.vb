'--Created By --[Panch Raj]--Aaagainst Ticket No-[BM00000002087]
' Anubhooti Added Location Finder Against BM00000003006------------
Imports common
Imports System.Data.SqlClient
Imports System.IO

Public Class frmReceiptChallan
    Inherits FrmMainTranScreen
    'Dim Qry As String
    'Dim dt As DataTable
    Dim ButtonToolTip As ToolTip = New ToolTip()
    'Dim ArrDb As New List(Of String)
    Const colInvoiceNo As String = "colInvoiceNo"
    Const colInvoiceDate As String = "colInvoiceDate"
    Const colBillToLocation As String = "colBillToLocation"
    Const colBillToLocationDesc As String = "colBillToLocationDesc"
    Const colInvoiceAmount As String = "colInvoiceAmount"
    Const colCustCode As String = "colCustCode"
    Const colCustName As String = "colCustName"
    Const colSalesmanCode As String = "colSalesmanCode"
    Const colSalesmanName As String = "colSalesmanName"

    Const colVehicleIn As String = "colVehicleIn"
    Const colReceiptIn As String = "colReceiptIn"
    Const colRemarks As String = "colRemarks"
    Const colComments As String = "colComments"
    Const colTransferHO As String = "colTransferHO"

    '' Anubhooti 30-Sep-2014 BM00000003711
    Const colGRNo As String = "colGRNo"
    Const colGRDate As String = "colGRDate"
    ''
    Dim ReportID As String = "Daily Receipt"
    Dim IsInsideLoadData As Boolean = True

    Private Sub frmReceiptChallan_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.N AndAlso MyBase.isPostFlag Then
            Reset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnClose.Enabled Then
            Me.Close()
        End If
    End Sub

    Private Sub frmReceiptChallan_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        Reset()
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        'ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnGo, "Press Alt+N Adding New Trasnaction")
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnGo, "Press Alt+N Reset Trasnaction")
         
    End Sub

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmSNReceiptChallan)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
           
        End If
        btnSave.Visible = MyBase.isModifyFlag
        'btnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    Sub LoadBlankGrid()
        gv1.DataSource = Nothing
        gv1.Rows.Clear()
        gv1.Columns.Clear()

        Dim InvoiceNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        InvoiceNo.FormatString = ""
        InvoiceNo.HeaderText = "Invoice No"
        InvoiceNo.Name = colInvoiceNo
        InvoiceNo.Width = 100
        InvoiceNo.ReadOnly = True
        InvoiceNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(InvoiceNo)

        Dim InvoiceDate As GridViewDateTimeColumn = New GridViewDateTimeColumn()
        InvoiceDate.FormatString = ""
        InvoiceDate.Format = DateTimePickerFormat.Custom
        InvoiceDate.CustomFormat = "dd/MMM/yyyy"
        InvoiceDate.HeaderText = "Invoice Date"
        InvoiceDate.FormatString = "{0:d}"
        InvoiceDate.Name = colInvoiceDate
        InvoiceDate.Width = 120
        InvoiceDate.ReadOnly = True
        InvoiceDate.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.MasterTemplate.Columns.Add(InvoiceDate)

        Dim BillToLocation As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        BillToLocation.FormatString = ""
        BillToLocation.HeaderText = "Location Code"
        BillToLocation.Name = colBillToLocation
        BillToLocation.Width = 100
        BillToLocation.ReadOnly = True
        BillToLocation.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.MasterTemplate.Columns.Add(BillToLocation)

        Dim BillToLocationDesc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        BillToLocationDesc.FormatString = ""
        BillToLocationDesc.HeaderText = "Location Description"
        BillToLocationDesc.Name = colBillToLocationDesc
        BillToLocationDesc.Width = 120
        BillToLocationDesc.ReadOnly = True
        BillToLocationDesc.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.MasterTemplate.Columns.Add(BillToLocationDesc)

        Dim InvoiceAmount As GridViewDecimalColumn = New GridViewDecimalColumn()
        InvoiceAmount.FormatString = ""
        InvoiceAmount.HeaderText = "Document Amount"
        InvoiceAmount.Name = colInvoiceAmount
        InvoiceAmount.Width = 100
        InvoiceAmount.ReadOnly = True
        InvoiceAmount.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.MasterTemplate.Columns.Add(InvoiceAmount)

        Dim CustCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        CustCode.FormatString = ""
        CustCode.HeaderText = "Customer Code"
        CustCode.Name = colCustCode
        CustCode.Width = 100
        CustCode.ReadOnly = True
        CustCode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.MasterTemplate.Columns.Add(CustCode)

        Dim CustName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        CustName.FormatString = ""
        CustName.HeaderText = "Customer Name"
        CustName.Name = colCustName
        CustName.Width = 100
        CustName.ReadOnly = True
        CustName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.MasterTemplate.Columns.Add(CustName)


        Dim SalesmanCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        SalesmanCode.FormatString = ""
        SalesmanCode.HeaderText = "Salesman Code"
        SalesmanCode.Name = colSalesmanCode
        SalesmanCode.Width = 100
        SalesmanCode.ReadOnly = True
        SalesmanCode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.MasterTemplate.Columns.Add(SalesmanCode)

        Dim SalesmanName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        SalesmanName.FormatString = ""
        SalesmanName.HeaderText = "Salesman Name"
        SalesmanName.Name = colSalesmanName
        SalesmanName.Width = 100
        SalesmanName.ReadOnly = True
        SalesmanName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.MasterTemplate.Columns.Add(SalesmanName)

        Dim VehicleIn As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        VehicleIn.FormatString = ""
        VehicleIn.HeaderText = "Vehicle In"
        VehicleIn.Name = colVehicleIn
        VehicleIn.Width = 70
        VehicleIn.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(VehicleIn)

        Dim ReceiptIn As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        ReceiptIn.FormatString = ""
        ReceiptIn.HeaderText = "Receipt In"
        ReceiptIn.Name = colReceiptIn
        ReceiptIn.Width = 70
        ReceiptIn.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(ReceiptIn)

        Dim TransferHo As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        TransferHo.FormatString = ""
        TransferHo.HeaderText = "Transfer To HO"
        TransferHo.Name = colTransferHO
        TransferHo.Width = 70
        TransferHo.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(TransferHo)

        Dim Remarks As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        Remarks.FormatString = ""
        Remarks.HeaderText = "Remarks"
        Remarks.Name = colRemarks
        Remarks.Width = 100
        Remarks.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.MasterTemplate.Columns.Add(Remarks)

        Dim Comments As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        Comments.FormatString = ""
        Comments.HeaderText = "Comments"
        Comments.Name = colComments
        Comments.Width = 100
        Comments.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.MasterTemplate.Columns.Add(Comments)

        '' Anubhooti 30-Sep-2014 BM00000003711 (Add GRNo ,GRDate Manually)
        Dim GRNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        GRNo.FormatString = ""
        GRNo.HeaderText = "GR No"
        GRNo.Name = colGRNo
        GRNo.Width = 100
        GRNo.MaxLength = 20
        'GRNo.ReadOnly = True
        GRNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(GRNo)

        Dim GRDate As GridViewDateTimeColumn = New GridViewDateTimeColumn()
        GRDate.FormatString = ""
        GRDate.Format = DateTimePickerFormat.Custom
        GRDate.CustomFormat = "dd/MMM/yyyy"
        GRDate.HeaderText = "GR Date"
        GRDate.FormatString = "{0:d}"
        GRDate.Name = colGRDate
        GRDate.Width = 120
        'GRDate.ReadOnly = True
        GRDate.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.MasterTemplate.Columns.Add(GRDate)
        ''

        gv1.EnableFiltering = True
        gv1.AllowDeleteRow = False
        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = False
        gv1.AllowRowReorder = False
        gv1.EnableSorting = False
        gv1.AllowAddNewRow = False
        gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv1.MasterTemplate.ShowRowHeaderColumn = False
    End Sub

    Sub PrintLoadBlankGrid()
        gv1.DataSource = Nothing
        gv1.Rows.Clear()
        gv1.Columns.Clear()


        Dim InvoiceNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        InvoiceNo.FormatString = ""
        InvoiceNo.HeaderText = "Invoice No"
        InvoiceNo.Name = colInvoiceNo
        InvoiceNo.Width = 100
        InvoiceNo.ReadOnly = True
        InvoiceNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(InvoiceNo)

        Dim InvoiceDate As GridViewDateTimeColumn = New GridViewDateTimeColumn()
        InvoiceDate.FormatString = ""
        InvoiceDate.Format = DateTimePickerFormat.Custom
        InvoiceDate.CustomFormat = "dd/MMM/yyyy"
        InvoiceDate.HeaderText = "Invoice Date"
        InvoiceDate.FormatString = "{0:d}"
        InvoiceDate.Name = colInvoiceDate
        InvoiceDate.Width = 120
        InvoiceDate.ReadOnly = True
        InvoiceDate.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.MasterTemplate.Columns.Add(InvoiceDate)

        Dim BillToLocation As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        BillToLocation.FormatString = ""
        BillToLocation.HeaderText = "Location Code"
        BillToLocation.Name = colBillToLocation
        BillToLocation.Width = 100
        BillToLocation.ReadOnly = True
        BillToLocation.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.MasterTemplate.Columns.Add(BillToLocation)

        Dim BillToLocationDesc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        BillToLocationDesc.FormatString = ""
        BillToLocationDesc.HeaderText = "Location Description"
        BillToLocationDesc.Name = colBillToLocationDesc
        BillToLocationDesc.Width = 120
        BillToLocationDesc.ReadOnly = True
        BillToLocationDesc.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.MasterTemplate.Columns.Add(BillToLocationDesc)

        Dim InvoiceAmount As GridViewDecimalColumn = New GridViewDecimalColumn()
        InvoiceAmount.FormatString = ""
        InvoiceAmount.HeaderText = "Document Amount"
        InvoiceAmount.Name = colInvoiceAmount
        InvoiceAmount.Width = 100
        InvoiceAmount.ReadOnly = True
        InvoiceAmount.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.MasterTemplate.Columns.Add(InvoiceAmount)

        Dim CustCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        CustCode.FormatString = ""
        CustCode.HeaderText = "Customer Code"
        CustCode.Name = colCustCode
        CustCode.Width = 100
        CustCode.ReadOnly = True
        CustCode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.MasterTemplate.Columns.Add(CustCode)

        Dim CustName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        CustName.FormatString = ""
        CustName.HeaderText = "Customer Name"
        CustName.Name = colCustName
        CustName.Width = 100
        CustName.ReadOnly = True
        CustName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.MasterTemplate.Columns.Add(CustName)


        Dim SalesmanCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        SalesmanCode.FormatString = ""
        SalesmanCode.HeaderText = "Salesman Code"
        SalesmanCode.Name = colSalesmanCode
        SalesmanCode.Width = 100
        SalesmanCode.ReadOnly = True
        SalesmanCode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.MasterTemplate.Columns.Add(SalesmanCode)

        Dim SalesmanName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        SalesmanName.FormatString = ""
        SalesmanName.HeaderText = "Salesman Name"
        SalesmanName.Name = colSalesmanName
        SalesmanName.Width = 100
        SalesmanName.ReadOnly = True
        SalesmanName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.MasterTemplate.Columns.Add(SalesmanName)

        Dim VehicleIn As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        VehicleIn.FormatString = ""
        VehicleIn.HeaderText = "Vehicle In"
        VehicleIn.Name = colVehicleIn
        VehicleIn.Width = 70
        VehicleIn.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.MasterTemplate.Columns.Add(VehicleIn)

        Dim ReceiptIn As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        ReceiptIn.FormatString = ""
        ReceiptIn.HeaderText = "Receipt In"
        ReceiptIn.Name = colReceiptIn
        ReceiptIn.Width = 70
        ReceiptIn.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.MasterTemplate.Columns.Add(ReceiptIn)

        Dim TransferHo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        TransferHo.FormatString = ""
        TransferHo.HeaderText = "Transfer To HO"
        TransferHo.Name = colTransferHO
        TransferHo.Width = 70
        TransferHo.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.MasterTemplate.Columns.Add(TransferHo)


        Dim Remarks As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        Remarks.FormatString = ""
        Remarks.HeaderText = "Remarks"
        Remarks.Name = colRemarks
        Remarks.Width = 100
        Remarks.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.MasterTemplate.Columns.Add(Remarks)

        Dim Comments As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        Comments.FormatString = ""
        Comments.HeaderText = "Comments"
        Comments.Name = colComments
        Comments.Width = 100
        Comments.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.MasterTemplate.Columns.Add(Comments)

       
        gv1.EnableFiltering = True
        gv1.AllowDeleteRow = False
        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = False
        gv1.AllowRowReorder = False
        gv1.EnableSorting = False
        gv1.AllowAddNewRow = False
        gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv1.MasterTemplate.ShowRowHeaderColumn = False
    End Sub
    Private Sub Reset()
        Dim serverdate As Date = clsCommon.GETSERVERDATE
        Me.dtpFrom.Value = serverdate
        Me.dtpTo.Value = serverdate
        Me.fndVehicleCode.Value = Nothing
        Me.fndCustCode.Value = Nothing

        LoadBlankGrid()
    End Sub


    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            If SaveData() Then
                RadMessageBox.Show("Data Saved Successfully")
                Reset()
            End If
        Catch ex As Exception
            RadMessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Function SaveData() As Boolean
        Try
            If AllowToSave() Then
                Dim Arr As New List(Of clsReceiptChallan)
                For Each grow As GridViewRowInfo In gv1.Rows
                    If clsCommon.myCBool(grow.Cells(colVehicleIn).Value) = True OrElse clsCommon.myCBool(grow.Cells(colReceiptIn).Value) = True OrElse clsCommon.myCBool(grow.Cells(colTransferHO).Value) = True Then
                        Dim objTr As New clsReceiptChallan()
                        objTr.SALE_INVOICE_NO = clsCommon.myCstr(grow.Cells(colInvoiceNo).Value)
                        objTr.VEHICLE_IN = IIf(clsCommon.myCBool(grow.Cells(colVehicleIn).Value) = True, "Y", "N")
                        objTr.RECEIPT_IN = IIf(clsCommon.myCBool(grow.Cells(colReceiptIn).Value) = True, "Y", "N")
                        objTr.TRANSFER_HO = IIf(clsCommon.myCBool(grow.Cells(colTransferHO).Value) = True, "Y", "N")
                        objTr.REMARKS = clsCommon.myCstr(grow.Cells(colRemarks).Value)
                        objTr.COMMENTS = clsCommon.myCstr(grow.Cells(colComments).Value)
                        '' Anubhooti 30-Sep-2014
                        objTr.GRNo = clsCommon.myCstr(grow.Cells(colGRNo).Value)
                        objTr.GRDate = clsCommon.myCstr(grow.Cells(colGRDate).Value)
                        ''
                        Arr.Add(objTr)
                    End If
                Next
                If clsReceiptChallan.SaveData(Arr) Then
                    Return True
                End If
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Private Function AllowToSave() As Boolean
        Return True
    End Function


    Private Sub LoadDetails()
        Try
            IsInsideLoadData = True
            LoadBlankGrid()
            Dim qry As String
            Dim dt As DataTable
            '-------richa 13/08/2014 Ticket No. BM00000003242---------
            Dim strwherecls As String = ""
            Dim strwhrcondition As String = ""
            strwherecls = Xtra.CustomerPermission()
            '-----------------------------------------------------
            qry = "select Document_Code as [Invoice No],convert(date,Document_Date,105) as [Invoice Date],Bill_To_Location as [Bill To Location],TSPL_LOCATION_MASTER.Location_Desc as [Location Description],TSPL_SD_SALE_INVOICE_HEAD.Total_Amt as [Total Amount]," & _
                  " Customer_Code as [Customer Code],TSPL_CUSTOMER_MASTER.Customer_Name as [Customer Name],TSPL_SD_SALE_INVOICE_HEAD.Salesman_Code as [Salesman Code], " & _
                  " TSPL_SD_SALE_INVOICE_HEAD.Salesman_Name as [Salesman Name],TSPL_SD_SALE_INVOICE_HEAD.VEHICLE_IN as [Vehicle In], " & _
                  " TSPL_SD_SALE_INVOICE_HEAD.RECEIPT_IN  as [Receipt In],TSPL_SD_SALE_INVOICE_HEAD.TRANSFER_HO as [Transfer To HO],'' as Remarks,'' as Comments from TSPL_SD_SALE_INVOICE_HEAD inner join TSPL_CUSTOMER_MASTER " & _
                  " on TSPL_SD_SALE_INVOICE_HEAD.Customer_Code=TSPL_CUSTOMER_MASTER.Cust_Code " & _
                  " left join TSPL_LOCATION_MASTER on TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location=TSPL_LOCATION_MASTER.Location_Code " & _
                  " where (TSPL_SD_SALE_INVOICE_HEAD.VEHICLE_IN !='Y' or TSPL_SD_SALE_INVOICE_HEAD.RECEIPT_IN !='Y' or TSPL_SD_SALE_INVOICE_HEAD.TRANSFER_HO !='Y') " & _
                  " and TSPL_SD_SALE_INVOICE_HEAD.Document_Date between '" & clsCommon.GetPrintDate(Me.dtpFrom.Value, "dd/MMM/yyyy") & "' and '" & clsCommon.GetPrintDate(Me.dtpTo.Value.AddDays(1), "dd/MMM/yyyy") & "'"
            If clsCommon.myLen(fndVehicleCode.Value) > 0 Then
                qry = qry & " and TSPL_SD_SALE_INVOICE_HEAD.VehicleNo='" & clsCommon.myCstr(fndVehicleCode.Value) & "'"
            End If

            If clsCommon.myLen(fndCustCode.Value) > 0 Then
                qry = qry & " and TSPL_SD_SALE_INVOICE_HEAD.Customer_Code='" & clsCommon.myCstr(fndCustCode.Value) & "'"
            Else
                '-------richa 13/08/2014 Ticket No. BM00000003242---------
                If clsCommon.myLen(strwherecls) > 0 Then
                    qry = qry & " and TSPL_SD_SALE_INVOICE_HEAD.Customer_Code in (" + strwherecls + ")"
                End If
                '-----------------------------------------------------
            End If

            ''Anubhooti 30-June-2014
            If clsCommon.myLen(fndLocation.Value) > 0 Then
                qry = qry & " and TSPL_LOCATION_MASTER.Location_Code='" & clsCommon.myCstr(fndLocation.Value) & "'"
            End If

            qry += " order by Document_Date"

            dt = clsDBFuncationality.GetDataTable(qry)
            Dim ii As Integer = 0
            For Each dr As DataRow In dt.Rows
                gv1.Rows.AddNew()
                ii += 1
                gv1.CurrentRow.Cells(colInvoiceNo).Value = dr.Item("Invoice No")
                gv1.CurrentRow.Cells(colInvoiceDate).Value = dr.Item("Invoice Date")
                gv1.CurrentRow.Cells(colBillToLocation).Value = clsCommon.myCstr(dr.Item("Bill To Location"))
                gv1.CurrentRow.Cells(colBillToLocationDesc).Value = clsCommon.myCstr(dr.Item("Location Description"))
                gv1.CurrentRow.Cells(colCustCode).Value = clsCommon.myCstr(dr.Item("Customer Code"))
                gv1.CurrentRow.Cells(colCustName).Value = clsCommon.myCstr(dr.Item("Customer Name"))
                gv1.CurrentRow.Cells(colSalesmanCode).Value = clsCommon.myCstr(dr.Item("Salesman Code"))
                gv1.CurrentRow.Cells(colSalesmanName).Value = clsCommon.myCstr(dr.Item("Salesman Name"))
                gv1.CurrentRow.Cells(colInvoiceAmount).Value = clsCommon.myCdbl(dr.Item("Total Amount"))
                gv1.CurrentRow.Cells(colVehicleIn).Value = IIf(dr.Item("Vehicle In") = "Y", True, False)
                gv1.CurrentRow.Cells(colReceiptIn).Value = IIf(dr.Item("Receipt In") = "Y", True, False)
                gv1.CurrentRow.Cells(colTransferHO).Value = IIf(dr.Item("Transfer To HO") = "Y", True, False)
            Next
            ReStoreGridLayout()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            IsInsideLoadData = False
        End Try
    End Sub
    Private Sub PrintLoadDetails()
        Try
            IsInsideLoadData = True
            PrintLoadBlankGrid()
            Dim qry As String
            Dim dt1 As DataTable
            qry = "select Document_Code as [Invoice No],convert(date,Document_Date,105) as [Invoice Date],Bill_To_Location as [Bill To Location],TSPL_LOCATION_MASTER.Location_Desc as [Location Description],TSPL_SD_SALE_INVOICE_HEAD.Total_Amt as [Total Amount]," & _
                  " Customer_Code as [Customer Code],TSPL_CUSTOMER_MASTER.Customer_Name as [Customer Name],TSPL_SD_SALE_INVOICE_HEAD.Salesman_Code as [Salesman Code], " & _
                  " TSPL_SD_SALE_INVOICE_HEAD.Salesman_Name as [Salesman Name],TSPL_SD_SALE_INVOICE_HEAD.VEHICLE_IN as [Vehicle In], " & _
                  " TSPL_SD_SALE_INVOICE_HEAD.RECEIPT_IN  as [Receipt In],TSPL_SD_SALE_INVOICE_HEAD.TRANSFER_HO as [Transfer To HO],TSPL_RECEIPT_CHALLAN.Remarks,TSPL_RECEIPT_CHALLAN.Comments from TSPL_SD_SALE_INVOICE_HEAD inner join TSPL_CUSTOMER_MASTER " & _
                  " on TSPL_SD_SALE_INVOICE_HEAD.Customer_Code=TSPL_CUSTOMER_MASTER.Cust_Code " & _
                  " left join TSPL_LOCATION_MASTER on TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location=TSPL_LOCATION_MASTER.Location_Code " & _
                  " left outer join TSPL_RECEIPT_CHALLAN on TSPL_SD_SALE_INVOICE_HEAD.Document_Code=TSPL_RECEIPT_CHALLAN.SALE_INVOICE_NO " & _
                  " where (TSPL_SD_SALE_INVOICE_HEAD.VEHICLE_IN ='Y' or TSPL_SD_SALE_INVOICE_HEAD.RECEIPT_IN ='Y' or TSPL_SD_SALE_INVOICE_HEAD.TRANSFER_HO='Y') " & _
                  " and TSPL_SD_SALE_INVOICE_HEAD.Document_Date between '" & clsCommon.GetPrintDate(Me.dtpFrom.Value, "dd/MMM/yyyy") & "' and '" & clsCommon.GetPrintDate(Me.dtpTo.Value.AddDays(1), "dd/MMM/yyyy") & "'"
            If clsCommon.myLen(fndVehicleCode.Value) > 0 Then
                qry = qry & " and TSPL_SD_SALE_INVOICE_HEAD.VehicleNo='" & clsCommon.myCstr(fndVehicleCode.Value) & "'"
            End If

            If clsCommon.myLen(fndCustCode.Value) > 0 Then
                qry = qry & " and TSPL_SD_SALE_INVOICE_HEAD.Customer_Code='" & clsCommon.myCstr(fndCustCode.Value) & "'"
            End If
            ''Anubhooti 30-June-2014
            If clsCommon.myLen(fndLocation.Value) > 0 Then
                qry = qry & " and TSPL_LOCATION_MASTER.Location_Code='" & clsCommon.myCstr(fndLocation.Value) & "'"
            End If

            dt1 = clsDBFuncationality.GetDataTable(qry)
            gv1.DataSource = dt1

            'ReStoreGridLayout()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            IsInsideLoadData = False
        End Try
    End Sub

    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(ReportID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(ReportID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gv1.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gv1.Columns.Count - 1 Step ii + 1
                        gv1.Columns(ii).IsVisible = False
                        gv1.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gv1.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub
    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGo.Click
        'Reset()
        LoadDetails()

    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub fndCustCode__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndCustCode._MYValidating
        '-------richa 13/08/2014 Ticket No. BM00000003242---------
        Dim strwherecls As String = ""
        Dim strwhrcondition As String = ""
        strwherecls = Xtra.CustomerPermission()
        '-----------------------------------------------------
        Dim qry As String = "select Cust_Code as Code,Customer_Name as Name,TSPL_CUSTOMER_MASTER.add1 +case when len(TSPL_CUSTOMER_MASTER.add2)>0 then ', '+TSPL_CUSTOMER_MASTER.add2 else '' end +case when LEN(isnull(TSPL_CUSTOMER_MASTER.Add3,''))>0 then ', '+isnull(TSPL_CUSTOMER_MASTER.Add3,'') else ' ' end + case when LEN(TSPL_CITY_MASTER.City_Name)>0 then ', '+TSPL_CITY_MASTER.City_Name else ' ' end + case when len(TSPL_CUSTOMER_MASTER.State )>0 then TSPL_CUSTOMER_MASTER.State else '' end  as Address,TSPL_CUSTOMER_MASTER.Terms_Code as [Term Code] , TSPL_TERMS_MASTER.Terms_Desc as [Term Description] ,Tax_Group as [Tax Group],Tax_Group_Desc as [Tax Group Description],Salesman_Code as [Salesman Code],Salesman_Desc as Salesman "
        qry += " from TSPL_CUSTOMER_MASTER "
        qry += " left outer join TSPL_CITY_MASTER on TSPL_CITY_MASTER.City_Code=TSPL_CUSTOMER_MASTER.City_Code"
        qry += " left outer join TSPL_TDS_STATE_MASTER on TSPL_TDS_STATE_MASTER.State_Code=TSPL_CUSTOMER_MASTER.State"
        qry += " left outer join TSPL_TERMS_MASTER on TSPL_TERMS_MASTER.Terms_Code=TSPL_CUSTOMER_MASTER.Terms_Code"
        qry += " left outer join TSPL_TAX_GROUP_MASTER on TSPL_TAX_GROUP_MASTER.Tax_Group_Code=TSPL_CUSTOMER_MASTER.Tax_Group and TSPL_TAX_GROUP_MASTER.Tax_Group_Type='S'"
        '-------richa 13/08/2014 Ticket No. BM00000003242---------
        If clsCommon.myLen(strwherecls) > 0 Then
            strwhrcondition = " TSPL_CUSTOMER_MASTER.Cust_Code in (" + strwherecls + ")"
        End If
        '-----------------------------------------------------
        fndCustCode.Value = clsCommon.ShowSelectForm("SNSOVendorFndr", qry, "Code", strwhrcondition, fndCustCode.Value, "Code", isButtonClicked)
        If clsCommon.myLen(fndCustCode.Value) > 0 Then
            lblCustName.Text = clsCustomerMasterNew.GetData(fndCustCode.Value, Nothing).Customer_Name
        End If

    End Sub

    Private Sub RadMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem2.Click
        If clsCommon.myLen(ReportID) > 0 Then
            gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = ReportID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv1.SaveLayout(obj.GridLayout)
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            obj.GridColumns = gv1.ColumnCount
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
            End If
            ''stuti regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub RadMenuItem3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem3.Click
        clsGridLayout.DeleteData(ReportID, objCommonVar.CurrentUserCode)
    End Sub

    Private Sub Export_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Export.Click
        
        ExportToExcel(Exporter.Excel)
        If (gv1.Rows.Count <= 0) Then
            common.clsCommon.MyMessageBoxShow("No Data To Export")
            Exit Sub
        End If
    End Sub

    Private Sub PDF_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles PDF.Click
       
        ExportToExcel(Exporter.PDF)
        If (gv1.Rows.Count <= 0) Then
            common.clsCommon.MyMessageBoxShow("No Data To Export")
            Exit Sub
        End If
    End Sub
    Enum Exporter
        Excel = 0
        PDF = 1
        Print = 2
        Refresh = 3
    End Enum
    Sub ExportToExcel(ByVal IsPrint As Exporter)
        PrintLoadDetails()
        Dim arrHeader As List(Of String) = New List(Of String)()
        Dim strTemp As String = ""
        arrHeader.Add("From Date : " + clsCommon.GetPrintDate(dtpFrom.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(dtpTo.Value, "dd/MM/yyyy") + " ")
        arrHeader.Add("Customer : " + fndCustCode.Value)


        If IsPrint = Exporter.Excel Then
            clsCommon.MyExportToExcelGrid("Daily Receipt", gv1, arrHeader, Me.Text)
        ElseIf IsPrint = Exporter.PDF Then
            clsCommon.MyExportToPDF("Daily Receipt", gv1, arrHeader, Me.Text, True)
        End If
    End Sub

    Private Sub fndLocation__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndLocation._MYValidating
        Dim qry As String = ""
        qry = " Select Location_Code As Code ,Location_Desc From TSPL_Location_Master "
        fndLocation.Value = clsCommon.ShowSelectForm("LocationFndr", qry, "Code", "", fndLocation.Value, "Code", isButtonClicked)
        LblLocationDesc.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Location_Desc From TSPL_Location_Master Where Location_Code ='" & fndLocation.Value & "'"))
    End Sub
End Class
