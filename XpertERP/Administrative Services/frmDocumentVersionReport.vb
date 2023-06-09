Imports common
Imports System.Data.SqlClient

Public Class FrmDocumentVersionReport
    Inherits FrmMainTranScreen
    Dim trnsLstCustomer As New List(Of String)
    Dim strCustomerCode As String = Nothing
    Dim dt1 As DataTable = New DataTable()
    Dim Isrefreshed As Boolean = False
    Dim IsSelected As Boolean = False
    Dim qry As String
    Dim dt As DataTable
    Dim count As Integer = 0
    Dim strNoOfRecord As String
    Dim trnsLst As New List(Of String)
    Dim arrUser As New ArrayList()
    Dim arrSelectedUser As New ArrayList()
    Dim strDocNo As String = Nothing
    Dim countPostedDoc As Integer = 0
    Public IsPostBack As Boolean = False
    Dim DtError As DataTable
    Dim dr As DataRow
    Public fromdate As DateTime
    Public Todate As DateTime
    Public ModuleName As String = ""
    Public Transaction As String = ""
    Public IsOpenPsted As Boolean
    Dim ButtonToolTip As New ToolTip()
    Dim isInsideLoad As Boolean = False
    Dim dtAuthen As DataTable
    Dim StrQuery As String = Nothing
    Dim ChkAllowBulkPosting As Double
    Dim arrLoc As String = Nothing
    Dim IsInsideLoadData As Boolean = True
    Dim ShowDairySaleModuleOnBulkPosting As Integer
    Dim whrLoc As String = ""
    Dim whrCreatedUser As String = ""
    Dim whrModifiedUser As String = ""

    Private Sub FrmDocumentVersionReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ShowDairySaleModuleOnBulkPosting = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ShowDairySaleModuleOnBulkPosting, clsFixedParameterCode.ShowDairySaleModuleOnBulkPosting, Nothing))
        dtpFromDate.Value = clsCommon.GETSERVERDATE()
        dtpToDate.Value = clsCommon.GETSERVERDATE()
        LoadModuleType()
        LaodModuleCommonServices()
        If clsCommon.myLen(ModuleName) > 0 Then
            cboModule.SelectedValue = ModuleName
            cboTransaction.SelectedValue = Transaction
            dtpFromDate.Value = fromdate
            dtpToDate.Value = Todate
            ShowData()
        End If
        ChkMilkType.Visible = False

    End Sub

    Public Sub LoadModuleType()
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        dr = dt.NewRow()
        dr("Code") = "Common Services"
        dr("Name") = "Common Services"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Receivables"
        dr("Name") = "Receivables"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Payables"
        dr("Name") = "Payables"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Tax Deducted At Source"
        dr("Name") = "Tax Deducted At Source"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "General Ledger"
        dr("Name") = "General Ledger"
        dt.Rows.Add(dr)


        dr = dt.NewRow()
        dr("Code") = "Product Sale"
        dr("Name") = "Product Sale"
        dt.Rows.Add(dr)


        If ShowDairySaleModuleOnBulkPosting = 1 Then
            dr = dt.NewRow()
            dr("Code") = "Dairy Sale"
            dr("Name") = "Dairy Sale"
            dt.Rows.Add(dr)
        Else
            dr = dt.NewRow()
            dr("Code") = "Fresh Sale"
            dr("Name") = "Fresh Sale"
            dt.Rows.Add(dr)
        End If

        dr = dt.NewRow()
        dr("Code") = "Material Management"
        dr("Name") = "Material Management"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Purchase Order"
        dr("Name") = "Purchase Order"
        dt.Rows.Add(dr)


        dr = dt.NewRow()
        dr("Code") = "HR and Payroll"
        dr("Name") = "HR and Payroll"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Production"
        dr("Name") = "Production"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Fixed Assets"
        dr("Name") = "Fixed Assets"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Bulk Sale"
        dr("Name") = "Bulk Sale"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "MCC Procurement"
        dr("Name") = "MCC Procurement"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Milk Procurement Bulk"
        dr("Name") = "Milk Procurement Bulk"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Farmer Payment"
        dr("Name") = "Farmer Payment"
        dt.Rows.Add(dr)

        cboModule.DataSource = dt
        cboModule.DisplayMember = "Name"
        cboModule.ValueMember = "Code"

    End Sub

    'Sub LaodModuleCommonServices()
    '    Dim dt1 As DataTable = New DataTable()
    '    dt1.Columns.Add("Code", GetType(String))
    '    dt1.Columns.Add("Name", GetType(String))

    '    dr = dt1.NewRow()
    '    dr("Code") = "Bank Transfer"
    '    dr("Name") = "Bank Transfer"
    '    dt1.Rows.Add(dr)


    '    dr = dt1.NewRow()
    '    dr("Code") = "Reverse Transaction"
    '    dr("Name") = "Reverse Transaction"
    '    dt1.Rows.Add(dr)

    '    cboTransaction.DataSource = dt1
    '    cboTransaction.DisplayMember = "Name"
    '    cboTransaction.ValueMember = "Code"
    'End Sub

    Sub LoadModuleFarmerPayment()

        Dim dt1 As DataTable = New DataTable()
        dt1.Columns.Add("Code", GetType(String))
        dt1.Columns.Add("Name", GetType(String))


        dr = dt1.NewRow()
        dr("Code") = "MCC Material Sale Farmer"
        dr("Name") = "MCC Material Sale Farmer"
        dt1.Rows.Add(dr)

        dr = dt1.NewRow()
        dr("Code") = "MCC Material Sale Farmer Return"
        dr("Name") = "MCC Material Sale Farmer Return"
        dt1.Rows.Add(dr)

        dr = dt1.NewRow()
        dr("Code") = "Farmer Payment Adjustment"
        dr("Name") = "Farmer Payment Adjustment"
        dt1.Rows.Add(dr)




        cboTransaction.DataSource = dt1
        cboTransaction.DisplayMember = "Name"
        cboTransaction.ValueMember = "Code"

    End Sub

    Sub LoadModulePO()

        Dim dt1 As DataTable = New DataTable()
        dt1.Columns.Add("Code", GetType(String))
        dt1.Columns.Add("Name", GetType(String))


        dr = dt1.NewRow()
        dr("Code") = "Purchase Requisition"
        dr("Name") = "Purchase Requisition"
        dt1.Rows.Add(dr)

        dr = dt1.NewRow()
        dr("Code") = "Purchase Order"
        dr("Name") = "Purchase Order"
        dt1.Rows.Add(dr)

        dr = dt1.NewRow()
        dr("Code") = "Gate Receipt Note"
        dr("Name") = "Gate Receipt Note"
        dt1.Rows.Add(dr)

        dr = dt1.NewRow()
        dr("Code") = "Material Receipt Note"
        dr("Name") = "Material Receipt Note"
        dt1.Rows.Add(dr)

        dr = dt1.NewRow()
        dr("Code") = "Store Receipt Note"
        dr("Name") = "Store Receipt Note"
        dt1.Rows.Add(dr)

        dr = dt1.NewRow()
        dr("Code") = "Purchase Invoice"
        dr("Name") = "Purchase Invoice"
        dt1.Rows.Add(dr)
        ''''Added By ---Pankaj Kumar Chaudhary ----on 12/10/2011
        dr = dt1.NewRow()
        dr("Code") = "Purchase Return"
        dr("Name") = "Purchase Return"
        dt1.Rows.Add(dr)

        dr = dt1.NewRow()
        dr("Code") = "RGP/NRGP"
        dr("Name") = "RGP/NRGP"
        dt1.Rows.Add(dr)

        dr = dt1.NewRow()
        dr("Code") = "Issue/Return/Transfer"
        dr("Name") = "Issue/Return/Transfer"
        dt1.Rows.Add(dr)

        dr = dt1.NewRow()
        dr("Code") = "Scrap LoadOut"
        dr("Name") = "Scrap LoadOut"
        dt1.Rows.Add(dr)

        dr = dt1.NewRow()
        dr("Code") = "Scrap Invoice"
        dr("Name") = "Scrap Invoice"
        dt1.Rows.Add(dr)


        cboTransaction.DataSource = dt1
        cboTransaction.DisplayMember = "Name"
        cboTransaction.ValueMember = "Code"

    End Sub
    ''
    ''Loads The Transactions of Module(Material Management) In Combo Box (Transaction)
    ''
    Sub LaodModuleMaterialMgmt()
        Dim dt1 As DataTable = New DataTable()
        dt1.Columns.Add("Code", GetType(String))
        dt1.Columns.Add("Name", GetType(String))


        dr = dt1.NewRow()
        dr("Code") = "Transfer(Load-In)"
        dr("Name") = "Transfer(Load-In)"
        dt1.Rows.Add(dr)

        dr = dt1.NewRow()
        dr("Code") = "Transfer(Load-Out)"
        dr("Name") = "Transfer(Load-Out)"
        dt1.Rows.Add(dr)


        'dr = dt1.NewRow()
        'dr("Code") = "Adjustment Entry"
        'dr("Name") = "Adjustment Entry"
        'dt1.Rows.Add(dr)

        dr = dt1.NewRow()
        dr("Code") = "Empty Transactions"
        dr("Name") = "Empty Transactions"
        dt1.Rows.Add(dr)

        dr = dt1.NewRow()
        dr("Code") = "Production Entry"
        dr("Name") = "Production Entry"
        dt1.Rows.Add(dr)

        dr = dt1.NewRow()
        dr("Code") = "Store Adjustment"
        dr("Name") = "Store Adjustment"
        dt1.Rows.Add(dr)

        cboTransaction.DataSource = dt1
        cboTransaction.DisplayMember = "Name"
        cboTransaction.ValueMember = "Code"
    End Sub

    ''
    ''Loads The Transactions of Module(Sales And Distribution) In Combo Box (Transaction)
    ''

    Sub LaodModuleSalesNDistribution()
        Dim dt1 As DataTable = New DataTable()
        dt1.Columns.Add("Code", GetType(String))
        dt1.Columns.Add("Name", GetType(String))

        dr = dt1.NewRow()
        dr("Code") = "Sale Order"
        dr("Name") = "Sale Order"
        dt1.Rows.Add(dr)

        dr = dt1.NewRow()
        dr("Code") = "Shipment"
        dr("Name") = "Shipment"
        dt1.Rows.Add(dr)

        dr = dt1.NewRow()
        dr("Code") = "Sale Invoice"
        dr("Name") = "Sale Invoice"
        dt1.Rows.Add(dr)

        dr = dt1.NewRow()
        dr("Code") = "Sale Return"
        dr("Name") = "Sale Return"
        dt1.Rows.Add(dr)

        dr = dt1.NewRow()
        dr("Code") = "Sale Return (Inter Company)"
        dr("Name") = "Sale Return (Inter Company)"
        dt1.Rows.Add(dr)

        'dr = dt1.NewRow()
        'dr("Code") = "Quick Settlement"
        'dr("Name") = "Quick Settlement"
        'dt1.Rows.Add(dr)

        cboTransaction.DataSource = dt1
        cboTransaction.DisplayMember = "Name"
        cboTransaction.ValueMember = "Code"
    End Sub
    Sub LaodModuleProductSale()
        Dim dt1 As DataTable = New DataTable()
        dt1.Columns.Add("Code", GetType(String))
        dt1.Columns.Add("Name", GetType(String))

        dr = dt1.NewRow()
        dr("Code") = "Shipment/Invoice"
        dr("Name") = "Shipment/Invoice"
        dt1.Rows.Add(dr)

        cboTransaction.DataSource = dt1
        cboTransaction.DisplayMember = "Name"
        cboTransaction.ValueMember = "Code"
    End Sub

    Sub LaodModuleFreshSale()
        Dim dt1 As DataTable = New DataTable()
        dt1.Columns.Add("Code", GetType(String))
        dt1.Columns.Add("Name", GetType(String))

        dr = dt1.NewRow()
        dr("Code") = "Shipment/Invoice"
        dr("Name") = "Shipment/Invoice"
        dt1.Rows.Add(dr)

        cboTransaction.DataSource = dt1
        cboTransaction.DisplayMember = "Name"
        cboTransaction.ValueMember = "Code"
    End Sub

    Sub LaodModuleDairySale()
        Dim dt1 As DataTable = New DataTable()
        dt1.Columns.Add("Code", GetType(String))
        dt1.Columns.Add("Name", GetType(String))

        dr = dt1.NewRow()
        dr("Code") = "Booking/DO"
        dr("Name") = "Booking/DO"
        dt1.Rows.Add(dr)

        dr = dt1.NewRow()
        dr("Code") = "Shipment/Invoice"
        dr("Name") = "Shipment/Invoice"
        dt1.Rows.Add(dr)

        cboTransaction.DataSource = dt1
        cboTransaction.DisplayMember = "Name"
        cboTransaction.ValueMember = "Code"
    End Sub
    ''
    ''Loads The Transaction of Module(Genertal Ledger) In Combo Box (Transaction)
    ''

    Sub LaodModuleGeneralLedger()
        Dim dt1 As DataTable = New DataTable()
        dt1.Columns.Add("Code", GetType(String))
        dt1.Columns.Add("Name", GetType(String))

        dr = dt1.NewRow()
        dr("Code") = "Journal Entry"
        dr("Name") = "Journal Entry"
        dt1.Rows.Add(dr)

        dr = dt1.NewRow()
        dr("Code") = "VCGL Entry"
        dr("Name") = "VCGL Entry"
        dt1.Rows.Add(dr)

        cboTransaction.DataSource = dt1
        cboTransaction.DisplayMember = "Name"
        cboTransaction.ValueMember = "Code"
    End Sub

    ''
    ''Loads The Transaction of Module(Tax Deducted At Source) In Combo Box (Transaction)
    ''

    Sub LaodModuleTaxDeductedAtSource()
        Dim dt1 As DataTable = New DataTable()
        dt1.Columns.Add("Code", GetType(String))
        dt1.Columns.Add("Name", GetType(String))

        dr = dt1.NewRow()
        'dr("Code") = "Create Remittance"
        'dr("Name") = "Create Remittance"
        'dt1.Rows.Add(dr)

        'dr = dt1.NewRow()
        dr("Code") = "Remittance Entry"
        dr("Name") = "Remittance Entry"
        dt1.Rows.Add(dr)

        cboTransaction.DataSource = dt1
        cboTransaction.DisplayMember = "Name"
        cboTransaction.ValueMember = "Code"
    End Sub

    ''
    ''Loads The Transaction of Module(Payables) In Combo Box (Transaction)
    ''

    Sub LaodModulePayables()
        Dim dt1 As DataTable = New DataTable()
        dt1.Columns.Add("Code", GetType(String))
        dt1.Columns.Add("Name", GetType(String))


        dr = dt1.NewRow()
        dr("Code") = "Payment Entry"
        dr("Name") = "Payment Entry"
        dt1.Rows.Add(dr)


        dr = dt1.NewRow()
        dr("Code") = "AP Invoice Entry"
        dr("Name") = "AP Invoice Entry"
        dt1.Rows.Add(dr)

        cboTransaction.DataSource = dt1
        cboTransaction.DisplayMember = "Name"
        cboTransaction.ValueMember = "Code"
    End Sub



    Sub LaodModuleReceivables()
        Dim dt1 As DataTable = New DataTable()
        dt1.Columns.Add("Code", GetType(String))
        dt1.Columns.Add("Name", GetType(String))

        dr = dt1.NewRow()
        dr("Code") = "Receipt Entry"
        dr("Name") = "Receipt Entry"
        dt1.Rows.Add(dr)

        dr = dt1.NewRow()
        dr("Code") = "Adjustment Entry"
        dr("Name") = "Adjustment Entry"
        dt1.Rows.Add(dr)

        dr = dt1.NewRow()
        dr("Code") = "AR Invoice Entry"
        dr("Name") = "AR Invoice Entry"
        dt1.Rows.Add(dr)

        cboTransaction.DataSource = dt1
        cboTransaction.DisplayMember = "Name"
        cboTransaction.ValueMember = "Code"
    End Sub



    Sub LaodModuleCommonServices()
        Dim dt1 As DataTable = New DataTable()
        dt1.Columns.Add("Code", GetType(String))
        dt1.Columns.Add("Name", GetType(String))

        dr = dt1.NewRow()
        dr("Code") = "Bank Transfer"
        dr("Name") = "Bank Transfer"
        dt1.Rows.Add(dr)


        dr = dt1.NewRow()
        dr("Code") = "Reverse Transaction"
        dr("Name") = "Reverse Transaction"
        dt1.Rows.Add(dr)

        cboTransaction.DataSource = dt1
        cboTransaction.DisplayMember = "Name"
        cboTransaction.ValueMember = "Code"
    End Sub

    Sub LaodModuleHRAndPayroll()
        Dim dt1 As DataTable = New DataTable()
        dt1.Columns.Add("Code", GetType(String))
        dt1.Columns.Add("Name", GetType(String))

        dr = dt1.NewRow()
        dr("Code") = "Employee Salary"
        dr("Name") = "Employee Salary"
        dt1.Rows.Add(dr)

        dr = dt1.NewRow()
        dr("Code") = "Hourly Attendance"
        dr("Name") = "Hourly Attendance"
        dt1.Rows.Add(dr)

        dr = dt1.NewRow()
        dr("Code") = "Daily Attendance"
        dr("Name") = "Daily Attendance"
        dt1.Rows.Add(dr)

        dr = dt1.NewRow()
        dr("Code") = "Monthly Attendance"
        dr("Name") = "Monthly Attendance"
        dt1.Rows.Add(dr)

        dr = dt1.NewRow()
        dr("Code") = "Allowance"
        dr("Name") = "Allowance"
        dt1.Rows.Add(dr)

        dr = dt1.NewRow()
        dr("Code") = "Deduction"
        dr("Name") = "Deduction"
        dt1.Rows.Add(dr)

        dr = dt1.NewRow()
        dr("Code") = "Bonus"
        dr("Name") = "Bonus"
        dt1.Rows.Add(dr)

        dr = dt1.NewRow()
        dr("Code") = "Salary Adjustment"
        dr("Name") = "Salary Adjustment"
        dt1.Rows.Add(dr)

        dr = dt1.NewRow()
        dr("Code") = "Reimbursement"
        dr("Name") = "Reimbursement"
        dt1.Rows.Add(dr)

        dr = dt1.NewRow()
        dr("Code") = "Loan Application"
        dr("Name") = "Loan Application"
        dt1.Rows.Add(dr)

        dr = dt1.NewRow()
        dr("Code") = "Loan Adjustment"
        dr("Name") = "Loan Adjustment"
        dt1.Rows.Add(dr)

        dr = dt1.NewRow()
        dr("Code") = "Leave Application"
        dr("Name") = "Leave Application"
        dt1.Rows.Add(dr)

        dr = dt1.NewRow()
        dr("Code") = "Leave Adjustment"
        dr("Name") = "Leave Adjustment"
        dt1.Rows.Add(dr)

        dr = dt1.NewRow()
        dr("Code") = "Employee Increment"
        dr("Name") = "Employee Increment"
        dt1.Rows.Add(dr)

        cboTransaction.DataSource = dt1
        cboTransaction.DisplayMember = "Name"
        cboTransaction.ValueMember = "Code"
    End Sub

    Sub LaodModuleProduction()
        Dim dt1 As DataTable = New DataTable()
        dt1.Columns.Add("Code", GetType(String))
        dt1.Columns.Add("Name", GetType(String))

        dr = dt1.NewRow()
        dr("Code") = "Batch Order"
        dr("Name") = "Batch Order"
        dt1.Rows.Add(dr)

        dr = dt1.NewRow()
        dr("Code") = "Bill Of Material"
        dr("Name") = "Bill Of Material"
        dt1.Rows.Add(dr)

        dr = dt1.NewRow()
        dr("Code") = "Manufacturing Order"
        dr("Name") = "Manufacturing Order"
        dt1.Rows.Add(dr)

        dr = dt1.NewRow()
        dr("Code") = "Production Planning"
        dr("Name") = "Production Planning"
        dt1.Rows.Add(dr)

        dr = dt1.NewRow()
        dr("Code") = "Production Receipt"
        dr("Name") = "Production Receipt"
        dt1.Rows.Add(dr)

        dr = dt1.NewRow()
        dr("Code") = "Production Requisition"
        dr("Name") = "Production Requisition"
        dt1.Rows.Add(dr)

        dr = dt1.NewRow()
        dr("Code") = "Production Return"
        dr("Name") = "Production Return"
        dt1.Rows.Add(dr)

        dr = dt1.NewRow()
        dr("Code") = "Store Issue"
        dr("Name") = "Store Issue"
        dt1.Rows.Add(dr)

        cboTransaction.DataSource = dt1
        cboTransaction.DisplayMember = "Name"
        cboTransaction.ValueMember = "Code"
    End Sub

    Sub LoadMccProcurement()
        Dim dt1 As DataTable = New DataTable()
        dt1.Columns.Add("Code", GetType(String))
        dt1.Columns.Add("Name", GetType(String))

        dr = dt1.NewRow()
        dr("Code") = "VSP Item Issue"
        dr("Name") = "VSP Item Issue"
        dt1.Rows.Add(dr)

        dr = dt1.NewRow()
        dr("Code") = "VSP Asset Issue"
        dr("Name") = "VSP Asset Issue"
        dt1.Rows.Add(dr)

        dr = dt1.NewRow()
        dr("Code") = "MCC Material Sale"
        dr("Name") = "MCC Material Sale"
        dt1.Rows.Add(dr)

        dr = dt1.NewRow()
        dr("Code") = "MCC Material Sale Return"
        dr("Name") = "MCC Material Sale Return"
        dt1.Rows.Add(dr)

        dr = dt1.NewRow()
        dr("Code") = "Milk Shift Uploader"
        dr("Name") = "Milk Shift Uploader"
        dt1.Rows.Add(dr)

        cboTransaction.DataSource = dt1
        cboTransaction.DisplayMember = "Name"
        cboTransaction.ValueMember = "Code"
    End Sub

    Sub LoadMilkProcurementBulk()
        Dim dt1 As DataTable = New DataTable()
        dt1.Columns.Add("Code", GetType(String))
        dt1.Columns.Add("Name", GetType(String))

        dr = dt1.NewRow()
        dr("Code") = "Purchase Invoice"
        dr("Name") = "Purchase Invoice"
        dt1.Rows.Add(dr)

        cboTransaction.DataSource = dt1
        cboTransaction.DisplayMember = "Name"
        cboTransaction.ValueMember = "Code"
    End Sub

    Sub LaodModuleBulkSale()
        Dim dt1 As DataTable = New DataTable()
        dt1.Columns.Add("Code", GetType(String))
        dt1.Columns.Add("Name", GetType(String))

        dr = dt1.NewRow()
        dr("Code") = "Bulk Dispatch"
        dr("Name") = "Bulk Dispatch"
        dt1.Rows.Add(dr)

        cboTransaction.DataSource = dt1
        cboTransaction.DisplayMember = "Name"
        cboTransaction.ValueMember = "Code"
    End Sub

    Private Sub cboModule_SelectedIndexChanged(sender As Object, e As UI.Data.PositionChangedEventArgs) Handles cboModule.SelectedIndexChanged
        LoadTrnsListOfSelectedModeule()
        gv1.DataSource = Nothing
    End Sub

    Public Sub LoadTrnsListOfSelectedModeule()
        If clsCommon.CompairString(clsCommon.myCstr(cboModule.SelectedValue), "Purchase Order") = CompairStringResult.Equal Then
            cboTransaction.DataSource = Nothing
            LoadModulePO()
            'btnPost.Enabled = False
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboModule.SelectedValue), "Material Management") = CompairStringResult.Equal Then
            cboTransaction.DataSource = Nothing
            LaodModuleMaterialMgmt()
            'btnPost.Enabled = False
            'ElseIf clsCommon.CompairString(clsCommon.myCstr(cboModule.SelectedValue), "Sales And Distribution") = CompairStringResult.Equal Then
            '    cboTransaction.DataSource = Nothing
            '    LaodModuleSalesNDistribution()
            '    'btnPost.Enabled = False
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboModule.SelectedValue), "General Ledger") = CompairStringResult.Equal Then
            cboTransaction.DataSource = Nothing
            LaodModuleGeneralLedger()
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboModule.SelectedValue), "Product Sale") = CompairStringResult.Equal Then
            cboTransaction.DataSource = Nothing
            LaodModuleProductSale()
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboModule.SelectedValue), "Fresh Sale") = CompairStringResult.Equal Then
            cboTransaction.DataSource = Nothing
            LaodModuleFreshSale()
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboModule.SelectedValue), "Dairy Sale") = CompairStringResult.Equal Then
            cboTransaction.DataSource = Nothing
            LaodModuleDairySale()
            'btnPost.Enabled = False
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboModule.SelectedValue), "Tax Deducted At Source") = CompairStringResult.Equal Then
            cboTransaction.DataSource = Nothing
            LaodModuleTaxDeductedAtSource()
            'btnPost.Enabled = False
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboModule.SelectedValue), "Payables") = CompairStringResult.Equal Then
            cboTransaction.DataSource = Nothing
            LaodModulePayables()
            'btnPost.Enabled = False
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboModule.SelectedValue), "Receivables") = CompairStringResult.Equal Then
            cboTransaction.DataSource = Nothing
            LaodModuleReceivables()
            'btnPost.Enabled = False
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboModule.SelectedValue), "Common Services") = CompairStringResult.Equal Then
            cboTransaction.DataSource = Nothing
            LaodModuleCommonServices()
            'btnPost.Enabled = False
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboModule.SelectedValue), "HR and Payroll") = CompairStringResult.Equal Then
            cboTransaction.DataSource = Nothing
            LaodModuleHRAndPayroll()
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboModule.SelectedValue), "Production") = CompairStringResult.Equal Then
            cboTransaction.DataSource = Nothing
            LaodModuleProduction()
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboModule.SelectedValue), "Bulk Sale") = CompairStringResult.Equal Then
            cboTransaction.DataSource = Nothing
            LaodModuleBulkSale()
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboModule.SelectedValue), "MCC Procurement") = CompairStringResult.Equal Then
            cboTransaction.DataSource = Nothing
            LoadMccProcurement()
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboModule.SelectedValue), "Milk Procurement Bulk") = CompairStringResult.Equal Then
            cboTransaction.DataSource = Nothing
            LoadMilkProcurementBulk()
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboModule.SelectedValue), "Farmer Payment") = CompairStringResult.Equal Then
            cboTransaction.DataSource = Nothing
            LoadModuleFarmerPayment()
        End If

    End Sub

    Sub LoadBlankGrid()
        gv1.Rows.Clear()
        gv1.Columns.Clear()
        gv1.AllowAddNewRow = False

    End Sub

    Sub ShowData()
        Try


            If clsCommon.CompairString(clsCommon.myCstr(cboModule.SelectedValue), "Purchase Order") = CompairStringResult.Equal Then
                gv1.DataSource = Nothing
                FillPurchaseOrder()
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboModule.SelectedValue), "Product Sale") = CompairStringResult.Equal Then
                gv1.DataSource = Nothing
                FillSalesNDistribution()
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboModule.SelectedValue), "Fresh Sale") = CompairStringResult.Equal Then
                gv1.DataSource = Nothing
                FillFreshSale()
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboModule.SelectedValue), "Dairy Sale") = CompairStringResult.Equal Then
                gv1.DataSource = Nothing
                FillDairySale()
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboModule.SelectedValue), "Material Management") = CompairStringResult.Equal Then
                gv1.DataSource = Nothing
                FillMaterialManagement()
                'ElseIf clsCommon.CompairString(clsCommon.myCstr(cboModule.SelectedValue), "Sales And Distribution") = CompairStringResult.Equal Then
                '    gv1.DataSource = Nothing
                '    FillSalesNDistribution()
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboModule.SelectedValue), "General Ledger") = CompairStringResult.Equal Then
                gv1.DataSource = Nothing
                FillGeneralLedger()
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboModule.SelectedValue), "Tax Deducted At Source") = CompairStringResult.Equal Then
                gv1.DataSource = Nothing
                FillTaxDdctdAtSource()
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboModule.SelectedValue), "Payables") = CompairStringResult.Equal Then
                gv1.DataSource = Nothing
                FillPayables()
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboModule.SelectedValue), "Receivables") = CompairStringResult.Equal Then
                gv1.DataSource = Nothing
                FillReceivables()
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboModule.SelectedValue), "Common Services") = CompairStringResult.Equal Then
                gv1.DataSource = Nothing
                FillCommonServices()
                If gv1.DataSource IsNot Nothing Then
                    gv1.MasterTemplate.Columns("Description").IsVisible = False
                End If
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboModule.SelectedValue), "HR and Payroll") = CompairStringResult.Equal Then
                gv1.DataSource = Nothing
                FillHRAndPayroll()
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboModule.SelectedValue), "Production") = CompairStringResult.Equal Then
                gv1.DataSource = Nothing
                FillProduction()
                ''richa agarwal 12/05/2015 against ticket no.BM00000006520
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboModule.SelectedValue), "Bulk Sale") = CompairStringResult.Equal Then
                gv1.DataSource = Nothing
                FillBulkSale()
                'gv1.MasterTemplate.Columns("Description").IsVisible = False
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboModule.SelectedValue), "MCC Procurement") = CompairStringResult.Equal Then
                gv1.DataSource = Nothing
                FillMccProcurement()
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboModule.SelectedValue), "Milk Procurement Bulk") = CompairStringResult.Equal Then
                gv1.DataSource = Nothing
                FillMilkProcurementBulk()
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboModule.SelectedValue), "Farmer Payment") = CompairStringResult.Equal Then
                gv1.DataSource = Nothing
                FillMCCMaterialSaleFarmer()
            End If

            If dt Is Nothing OrElse dt.Rows.Count = 0 Then
                gv1.DataSource = Nothing

                common.clsCommon.MyMessageBoxShow("There Is No  Data Between The Dates '" + dtpFromDate.Value.Date + "' And '" + dtpToDate.Value.Date + "' ", Me.Text)

                Return
            End If
            IsPostBack = False

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Public Sub Load_Authorisation(ByVal ProgramName As String)

        StrQuery = "select max(TSPL_GROUP_PROGRAM_MAPPING.Authorized_Flag) as Authorized_Flag from TSPL_GROUP_PROGRAM_MAPPING " & _
         " inner join TSPL_Program_Master on TSPL_Program_Master.Program_Code=TSPL_GROUP_PROGRAM_MAPPING.Program_Code " & _
          " where TSPL_Program_Master.Program_Code='" + ProgramName + "' and " & _
          "TSPL_GROUP_PROGRAM_MAPPING.Group_Code in (select Group_Code  from TSPL_USER_GROUP_MAPPING where User_Code ='" + objCommonVar.CurrentUserCode + "') and TSPL_GROUP_PROGRAM_MAPPING.Authorized_Flag=1 "
        dtAuthen = clsDBFuncationality.GetDataTable(StrQuery)

    End Sub

    Sub FillPurchaseOrder()

        If clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), "Purchase Requisition") = CompairStringResult.Equal Then
            Load_Authorisation(clsUserMgtCode.mbtnPurchaseRequistion)
            If dtAuthen.Rows.Count > 0 Then
                If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then
                    gv1.DataSource = Nothing
                    If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                        whrLoc = " and TSPL_REQUISITION_HEAD.Location in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ") "
                    Else
                        whrLoc = ""
                    End If
                    If txtCreatedBy.arrValueMember IsNot Nothing AndAlso txtCreatedBy.arrValueMember.Count > 0 Then
                        whrCreatedUser = " and TSPL_REQUISITION_HEAD.Created_By in (" + clsCommon.GetMulcallString(txtCreatedBy.arrValueMember) + ") "
                    Else
                        whrCreatedUser = ""
                    End If
                    If txtModifyedBy.arrValueMember IsNot Nothing AndAlso txtModifyedBy.arrValueMember.Count > 0 Then
                        whrModifiedUser = " and TSPL_REQUISITION_HEAD.Modify_By in (" + clsCommon.GetMulcallString(txtCreatedBy.arrValueMember) + ") "
                    Else
                        whrModifiedUser = ""
                    End If

                    qry = "  select XXXFinal.Document_No , max( XXXFinal.Document_Date ) as Document_Date , max( XXXFinal.Exe_Version_For_Create_Date) as Exe_Version_On_Document_Create,max( TBL_Department.Created_By_Deparment ) as Created_By_Deparment ,max(XXXFinal.Created_By) as Created_By ,max( XXXFinal.Created_Date ) as Created_Date, max( XXXFinal.Exe_Version_For_Modify_Date) as Exe_Version_On_Document_Modify , max(XXXFinal.Modify_By ) as Modify_By , max(XXXFinal.Modify_Date) as Modify_Date  from  " &
                          " ( " &
                          "  select XXXCreatDateVersion.Document_No , XXXCreatDateVersion.Document_Date , XXXCreatDateVersion.Exe_Version_For_Create_Date, XXXCreatDateVersion.Created_By , XXXCreatDateVersion.Created_Date , XXXCreatDateVersion.Exe_Version_For_Modify_Date,XXXCreatDateVersion.Modify_By,XXXCreatDateVersion.Modify_Date from  ( " &
                          "  select FirstTable.Document_No  ,FirstTable.Document_Date ,SecondTable.Version_No as Exe_Version_For_Create_Date ,FirstTable.Created_By as Created_By , FirstTable.Created_Date  as Created_Date , '' as Exe_Version_For_Modify_Date, FirstTable.Modify_By  , FirstTable.Modify_Date as  Modify_Date  from  ( " &
                          "  Select 1 as SNO, TSPL_REQUISITION_HEAD.Requisition_Id as  Document_No ,TSPL_REQUISITION_HEAD.Requisition_Date as Document_Date,TSPL_REQUISITION_HEAD.Created_By , DATEDIFF(dd, 0, convert(date,TSPL_REQUISITION_HEAD.Created_Date,103)) + case when TSPL_REQUISITION_HEAD.Requisition_Date is not null then  convert(datetime, CAST(TSPL_REQUISITION_HEAD.Requisition_Date AS time) ) else '' end as Created_Date ,TSPL_REQUISITION_HEAD.Modify_By, DATEDIFF(dd, 0, convert(date,TSPL_REQUISITION_HEAD.Modify_Date,103)) + case when TSPL_REQUISITION_HEAD.Posting_Date is not null then  convert(datetime, CAST(TSPL_REQUISITION_HEAD.Posting_Date AS time) ) else '' end as  Modify_Date   from TSPL_REQUISITION_HEAD  " &
                          "  where convert(date,TSPL_REQUISITION_HEAD.Created_Date,103) >= convert(date,'" + dtpFromDate.Value + "',103) and convert(date,TSPL_REQUISITION_HEAD.Created_Date,103) <= convert(date,'" + dtpToDate.Value + "',103)  " + whrLoc + " " + whrCreatedUser + " " + whrModifiedUser + "  ) as FirstTable " &
                          "  left outer join " &
                          " (select 1 as  SNO, a.Version_No , a.Date as From_Date ,(SELECT min(b.date)  FROM TSPL_Exe_Deployment b WHERE b.Date > a.Date   ) as To_Date  from TSPL_Exe_Deployment a ) SecondTable on SecondTable.SNO = FirstTable.SNO  " &
                          "  where convert (datetime ,FirstTable.Created_Date,103) between convert(datetime,SecondTable.From_Date) and convert(datetime,SecondTable.To_Date)   " &
                          " ) XXXCreatDateVersion " &
                          "  Union All  " &
                          " select XXXModifyDateVersion.Document_No , XXXModifyDateVersion.Document_Date , XXXModifyDateVersion.Exe_Version_For_Create_Date, XXXModifyDateVersion.Created_By , XXXModifyDateVersion.Created_Date , XXXModifyDateVersion.Exe_Version_For_Modify_Date,XXXModifyDateVersion.Modify_By,XXXModifyDateVersion.Modify_Date from (  " &
                          " select FirstTable.Document_No  ,FirstTable.Document_Date ,'' as Exe_Version_For_Create_Date ,FirstTable.Created_By as Created_By , FirstTable.Created_Date  as Created_Date , SecondTable.Version_No  as Exe_Version_For_Modify_Date, FirstTable.Modify_By  , FirstTable.Modify_Date as  Modify_Date  from  ( " &
                          " Select 1 as SNO, TSPL_REQUISITION_HEAD.Requisition_Id as  Document_No ,TSPL_REQUISITION_HEAD.Requisition_Date as Document_Date,TSPL_REQUISITION_HEAD.Created_By , DATEDIFF(dd, 0, convert(date,TSPL_REQUISITION_HEAD.Created_Date,103)) + case when TSPL_REQUISITION_HEAD.Requisition_Date is not null then  convert(datetime, CAST(TSPL_REQUISITION_HEAD.Requisition_Date AS time) ) else '' end as Created_Date ,TSPL_REQUISITION_HEAD.Modify_By, DATEDIFF(dd, 0, convert(date,TSPL_REQUISITION_HEAD.Modify_Date,103)) + case when TSPL_REQUISITION_HEAD.Posting_Date is not null then  convert(datetime, CAST(TSPL_REQUISITION_HEAD.Posting_Date AS time) ) else '' end as  Modify_Date   from TSPL_REQUISITION_HEAD  " &
                          " where convert(date,TSPL_REQUISITION_HEAD.Modify_Date,103) >= convert(date,'" + dtpFromDate.Value + "',103) and convert(date,TSPL_REQUISITION_HEAD.Modify_Date,103) <= convert(date,'" + dtpToDate.Value + "',103)  " + whrLoc + " " + whrCreatedUser + " " + whrModifiedUser + " ) as FirstTable " &
                          " left outer join  " &
                          " (select 1 as  SNO, a.Version_No , a.Date as From_Date ,(SELECT min(b.date)  FROM TSPL_Exe_Deployment b WHERE b.Date > a.Date   ) as To_Date  from TSPL_Exe_Deployment a ) SecondTable on SecondTable.SNO = FirstTable.SNO " &
                          "  where convert (datetime ,FirstTable.Modify_Date,103) between convert(datetime,SecondTable.From_Date) and convert(datetime,SecondTable.To_Date)  " &
                          "   ) XXXModifyDateVersion ) XXXFinal  " &
                          " left outer join TSPL_User_Master on TSPL_User_Master.User_Code = XXXFinal.Created_By " &
                          " left outer join  (select Segment_code , Description as Created_By_Deparment  from TSPL_GL_SEGMENT_CODE where  Seg_No=3 ) TBL_Department on TBL_Department.Segment_code =TSPL_User_Master.Segment_code " &
                          " group by XXXFinal.Document_No " &
                          " order by convert (datetime,max(XXXFinal.Document_Date)) ,XXXFinal.Document_No "

                    ' ' By Document Date Start 
                    'qry = " select FirstTable.Document_No  ,FirstTable.Document_Date ,SecondTable.Version_No as Exe_Version ,FirstTable.Created_By as Created_By ,convert(varchar, FirstTable.Created_Date,103)  as Created_Date , FirstTable.Modify_By  ,convert(varchar, FirstTable.Modify_Date,103) as  Modify_Date  from " & _
                    '      " (Select 1 as SNO, TSPL_REQUISITION_HEAD.Requisition_Id as  Document_No ,TSPL_REQUISITION_HEAD.Requisition_Date as Document_Date,TSPL_REQUISITION_HEAD.Created_By ,TSPL_REQUISITION_HEAD.Created_Date,TSPL_REQUISITION_HEAD.Modify_By,TSPL_REQUISITION_HEAD.Modify_Date   from TSPL_REQUISITION_HEAD " & _
                    '      "	where convert(date,TSPL_REQUISITION_HEAD.Requisition_Date,103) >= convert(date,'" + dtpFromDate.Value + "',103) and convert(date,TSPL_REQUISITION_HEAD.Requisition_Date,103) <= convert(date,'" + dtpToDate.Value + "',103)    ) as FirstTable left outer join  " & _
                    '      " (select 1 as  SNO, a.Version_No , a.Date as From_Date ,(SELECT min(b.date)  FROM TSPL_Exe_Deployment b WHERE b.Date > a.Date   ) as To_Date  from TSPL_Exe_Deployment a ) SecondTable on SecondTable.SNO = FirstTable.SNO " & _
                    '      " where convert (datetime ,FirstTable.Document_Date) between convert(datetime,SecondTable.From_Date) and convert(datetime,SecondTable.To_Date) " & _
                    '      " order by convert (datetime,FirstTable.Document_Date) ,FirstTable.Document_No "
                    ' ' By Document Date End

                    'qry = "SELECT CAST(TSPL_REQUISITION_HEAD.Status as BIT ) as Status, TSPL_REQUISITION_HEAD.Requisition_Id as [Document Id], TSPL_REQUISITION_HEAD.Requisition_Date as [Document Date],isnull(TSPL_REQUISITION_HEAD.Total_RQ_Amt,0) as [Amount], TSPL_REQUISITION_HEAD.Expire_Date as [Exipe Date], TSPL_REQUISITION_HEAD.Require_Date  as [Require Date], TSPL_REQUISITION_HEAD.Description as Description, CAST(TSPL_REQUISITION_HEAD.On_Hold as bit) as Hold,TSPL_REQUISITION_HEAD.Created_By AS 'Created By' FROM TSPL_REQUISITION_HEAD WHERE  convert(date,TSPL_REQUISITION_HEAD.Requisition_Date,103) >= convert(date,'" + dtpFromDate.Value + "',103) and convert(date,TSPL_REQUISITION_HEAD.Requisition_Date,103) <= convert(date,'" + dtpToDate.Value + "',103) "
                    'qry += " ORDER BY TSPL_REQUISITION_HEAD.Requisition_Date , TSPL_REQUISITION_HEAD.Requisition_Id  "
                End If
            End If
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), "Purchase Order") = CompairStringResult.Equal Then
            Load_Authorisation(clsUserMgtCode.mbtnPurchaseOrder)
            If dtAuthen.Rows.Count > 0 Then
                If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then
                    gv1.DataSource = Nothing

                    If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                        whrLoc = " and TSPL_PURCHASE_ORDER_HEAD.Bill_To_Location in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ") "
                    Else
                        whrLoc = ""
                    End If
                    If txtCreatedBy.arrValueMember IsNot Nothing AndAlso txtCreatedBy.arrValueMember.Count > 0 Then
                        whrCreatedUser = " and TSPL_PURCHASE_ORDER_HEAD.Created_By in (" + clsCommon.GetMulcallString(txtCreatedBy.arrValueMember) + ") "
                    Else
                        whrCreatedUser = ""
                    End If
                    If txtModifyedBy.arrValueMember IsNot Nothing AndAlso txtModifyedBy.arrValueMember.Count > 0 Then
                        whrModifiedUser = " and TSPL_PURCHASE_ORDER_HEAD.Modify_By in (" + clsCommon.GetMulcallString(txtCreatedBy.arrValueMember) + ") "
                    Else
                        whrModifiedUser = ""
                    End If

                    qry = "  select XXXFinal.Document_No , max( XXXFinal.Document_Date ) as Document_Date , max( XXXFinal.Exe_Version_For_Create_Date) as Exe_Version_On_Document_Create,max( TBL_Department.Created_By_Deparment ) as Created_By_Deparment, max(XXXFinal.Created_By) as Created_By ,max( XXXFinal.Created_Date ) as Created_Date, max( XXXFinal.Exe_Version_For_Modify_Date) as Exe_Version_On_Document_Modify , max(XXXFinal.Modify_By ) as Modify_By , max(XXXFinal.Modify_Date) as Modify_Date  from  " &
                          " ( " &
                          "  select XXXCreatDateVersion.Document_No , XXXCreatDateVersion.Document_Date , XXXCreatDateVersion.Exe_Version_For_Create_Date, XXXCreatDateVersion.Created_By , XXXCreatDateVersion.Created_Date , XXXCreatDateVersion.Exe_Version_For_Modify_Date,XXXCreatDateVersion.Modify_By,XXXCreatDateVersion.Modify_Date from  ( " &
                          "  select FirstTable.Document_No  ,FirstTable.Document_Date ,SecondTable.Version_No as Exe_Version_For_Create_Date ,FirstTable.Created_By as Created_By , FirstTable.Created_Date  as Created_Date , '' as Exe_Version_For_Modify_Date, FirstTable.Modify_By  , FirstTable.Modify_Date as  Modify_Date  from  ( " &
                          "  Select 1 as SNO, TSPL_PURCHASE_ORDER_HEAD  .PurchaseOrder_No as  Document_No ,TSPL_PURCHASE_ORDER_HEAD  .PurchaseOrder_Date as Document_Date,TSPL_PURCHASE_ORDER_HEAD  .Created_By , DATEDIFF(dd, 0, convert(date,TSPL_PURCHASE_ORDER_HEAD  .Created_Date,103)) + case when TSPL_PURCHASE_ORDER_HEAD  .PurchaseOrder_Date is not null then  convert(datetime, CAST(TSPL_PURCHASE_ORDER_HEAD  .PurchaseOrder_Date AS time) ) else '' end as Created_Date ,TSPL_PURCHASE_ORDER_HEAD  .Modify_By, DATEDIFF(dd, 0, convert(date,TSPL_PURCHASE_ORDER_HEAD  .Modify_Date,103)) + case when TSPL_PURCHASE_ORDER_HEAD  .Posting_Date is not null then  convert(datetime, CAST(TSPL_PURCHASE_ORDER_HEAD  .Posting_Date AS time) ) else '' end as  Modify_Date   from TSPL_PURCHASE_ORDER_HEAD    " &
                          "  where convert(date,TSPL_PURCHASE_ORDER_HEAD  .Created_Date,103) >= convert(date,'" + dtpFromDate.Value + "',103) and convert(date,TSPL_PURCHASE_ORDER_HEAD  .Created_Date,103) <= convert(date,'" + dtpToDate.Value + "',103)  " + whrLoc + " " + whrCreatedUser + " " + whrModifiedUser + " ) as FirstTable " &
                          "  left outer join " &
                          " (select 1 as  SNO, a.Version_No , a.Date as From_Date ,(SELECT min(b.date)  FROM TSPL_Exe_Deployment b WHERE b.Date > a.Date   ) as To_Date  from TSPL_Exe_Deployment a ) SecondTable on SecondTable.SNO = FirstTable.SNO  " &
                          "  where convert (datetime ,FirstTable.Created_Date,103) between convert(datetime,SecondTable.From_Date) and convert(datetime,SecondTable.To_Date)   " &
                          " ) XXXCreatDateVersion " &
                          "  Union All  " &
                          " select XXXModifyDateVersion.Document_No , XXXModifyDateVersion.Document_Date , XXXModifyDateVersion.Exe_Version_For_Create_Date, XXXModifyDateVersion.Created_By , XXXModifyDateVersion.Created_Date , XXXModifyDateVersion.Exe_Version_For_Modify_Date,XXXModifyDateVersion.Modify_By,XXXModifyDateVersion.Modify_Date from (  " &
                          " select FirstTable.Document_No  ,FirstTable.Document_Date ,'' as Exe_Version_For_Create_Date ,FirstTable.Created_By as Created_By , FirstTable.Created_Date  as Created_Date , SecondTable.Version_No  as Exe_Version_For_Modify_Date, FirstTable.Modify_By  , FirstTable.Modify_Date as  Modify_Date  from  ( " &
                          " Select 1 as SNO, TSPL_PURCHASE_ORDER_HEAD  .PurchaseOrder_No as  Document_No ,TSPL_PURCHASE_ORDER_HEAD  .PurchaseOrder_Date as Document_Date,TSPL_PURCHASE_ORDER_HEAD  .Created_By , DATEDIFF(dd, 0, convert(date,TSPL_PURCHASE_ORDER_HEAD  .Created_Date,103)) + case when TSPL_PURCHASE_ORDER_HEAD  .PurchaseOrder_Date is not null then  convert(datetime, CAST(TSPL_PURCHASE_ORDER_HEAD  .PurchaseOrder_Date AS time) ) else '' end as Created_Date ,TSPL_PURCHASE_ORDER_HEAD  .Modify_By, DATEDIFF(dd, 0, convert(date,TSPL_PURCHASE_ORDER_HEAD  .Modify_Date,103)) + case when TSPL_PURCHASE_ORDER_HEAD  .Posting_Date is not null then  convert(datetime, CAST(TSPL_PURCHASE_ORDER_HEAD  .Posting_Date AS time) ) else '' end as  Modify_Date   from TSPL_PURCHASE_ORDER_HEAD    " &
                          " where convert(date,TSPL_PURCHASE_ORDER_HEAD  .Modify_Date,103) >= convert(date,'" + dtpFromDate.Value + "',103) and convert(date,TSPL_PURCHASE_ORDER_HEAD  .Modify_Date,103) <= convert(date,'" + dtpToDate.Value + "',103) " + whrLoc + " " + whrCreatedUser + " " + whrModifiedUser + "   ) as FirstTable " &
                          " left outer join  " &
                          " (select 1 as  SNO, a.Version_No , a.Date as From_Date ,(SELECT min(b.date)  FROM TSPL_Exe_Deployment b WHERE b.Date > a.Date   ) as To_Date  from TSPL_Exe_Deployment a ) SecondTable on SecondTable.SNO = FirstTable.SNO " &
                          "  where convert (datetime ,FirstTable.Modify_Date,103) between convert(datetime,SecondTable.From_Date) and convert(datetime,SecondTable.To_Date)  " &
                          "   ) XXXModifyDateVersion ) XXXFinal " &
                          " left outer join TSPL_User_Master on TSPL_User_Master.User_Code = XXXFinal.Created_By " &
                          " left outer join  (select Segment_code , Description as Created_By_Deparment  from TSPL_GL_SEGMENT_CODE where  Seg_No=3 ) TBL_Department on TBL_Department.Segment_code =TSPL_User_Master.Segment_code " &
                          " group by XXXFinal.Document_No " &
                          " order by convert (datetime,max(XXXFinal.Document_Date)) ,XXXFinal.Document_No "

                    'qry = " select FirstTable.Document_No  ,FirstTable.Document_Date ,SecondTable.Version_No as Exe_Version ,FirstTable.Created_By as Created_By ,convert(varchar,FirstTable.Created_Date,103)  as Created_Date , FirstTable.Modify_By  ,convert(varchar, FirstTable.Modify_Date,103) as Modify_Date   from " & _
                    '      " (Select 1 as SNO, TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No as  Document_No ,TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_Date as Document_Date,TSPL_PURCHASE_ORDER_HEAD.Created_By ,TSPL_PURCHASE_ORDER_HEAD.Created_Date,TSPL_PURCHASE_ORDER_HEAD.Modify_By,TSPL_PURCHASE_ORDER_HEAD.Modify_Date   from TSPL_PURCHASE_ORDER_HEAD " & _
                    '      "	where convert(date,TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_Date,103) >= convert(date,'" + dtpFromDate.Value + "',103) and convert(date,TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_Date,103) <= convert(date,'" + dtpToDate.Value + "',103)    ) as FirstTable left outer join  " & _
                    '      " (select 1 as  SNO, a.Version_No , a.Date as From_Date ,(SELECT min(b.date)  FROM TSPL_Exe_Deployment b WHERE b.Date > a.Date   ) as To_Date  from TSPL_Exe_Deployment a ) SecondTable on SecondTable.SNO = FirstTable.SNO " & _
                    '      " where convert (datetime ,FirstTable.Document_Date) between convert(datetime,SecondTable.From_Date) and convert(datetime,SecondTable.To_Date) " & _
                    '      " order by convert (datetime,FirstTable.Document_Date) ,FirstTable.Document_No "

                    'qry = "  select FirstTable.PurchaseOrder_No as Document_No  ,FirstTable.PurchaseOrder_Date as Document_Date ,SecondTable.Version_No as Exe_Version ,FirstTable.Created_By as Created_By ,FirstTable.Created_Date  as Created_Date , FirstTable.Modify_By as Modified_By ,FirstTable.Modify_Date  as Modified_Date  from " & _
                    '      "  (Select 1 as SNO, TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No ,TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_Date,TSPL_PURCHASE_ORDER_HEAD.Created_By ,TSPL_PURCHASE_ORDER_HEAD.Created_Date,Modify_By,Modify_Date   from TSPL_PURCHASE_ORDER_HEAD where convert(date,TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_Date,103) >= convert(date,'" + dtpFromDate.Value + "',103) and convert(date,TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_Date,103) <= convert(date,'" + dtpToDate.Value + "',103)     ) as FirstTable left outer join " & _
                    '      " (select 1 as  SNO, a.Version_No , a.Date as From_Date ,(SELECT min(b.date)  FROM TSPL_Exe_Deployment b WHERE b.Date > a.Date   ) as To_Date  from TSPL_Exe_Deployment a ) 			   SecondTable on SecondTable.SNO = FirstTable.SNO " & _
                    '      " where convert (datetime ,FirstTable.PurchaseOrder_Date) between convert(datetime,SecondTable.From_Date) and convert(datetime,SecondTable.To_Date) "


                End If
            End If
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), "Gate Receipt Note") = CompairStringResult.Equal Then
            Load_Authorisation(clsUserMgtCode.mbtnGRN)
            If dtAuthen.Rows.Count > 0 Then
                If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then
                    gv1.DataSource = Nothing
                    If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                        whrLoc = " and TSPL_GRN_HEAD.Bill_To_Location in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ") "
                    Else
                        whrLoc = ""
                    End If
                    If txtCreatedBy.arrValueMember IsNot Nothing AndAlso txtCreatedBy.arrValueMember.Count > 0 Then
                        whrCreatedUser = " and TSPL_GRN_HEAD.Created_By in (" + clsCommon.GetMulcallString(txtCreatedBy.arrValueMember) + ") "
                    Else
                        whrCreatedUser = ""
                    End If
                    If txtModifyedBy.arrValueMember IsNot Nothing AndAlso txtModifyedBy.arrValueMember.Count > 0 Then
                        whrModifiedUser = " and TSPL_GRN_HEAD.Modify_By in (" + clsCommon.GetMulcallString(txtCreatedBy.arrValueMember) + ") "
                    Else
                        whrModifiedUser = ""
                    End If

                    qry = "  select XXXFinal.Document_No , max( XXXFinal.Document_Date ) as Document_Date , max( XXXFinal.Exe_Version_For_Create_Date) as Exe_Version_On_Document_Create,max( TBL_Department.Created_By_Deparment ) as Created_By_Deparment, max(XXXFinal.Created_By) as Created_By ,max( XXXFinal.Created_Date ) as Created_Date, max( XXXFinal.Exe_Version_For_Modify_Date) as Exe_Version_On_Document_Modify , max(XXXFinal.Modify_By ) as Modify_By , max(XXXFinal.Modify_Date) as Modify_Date  from  " &
                          " ( " &
                          "  select XXXCreatDateVersion.Document_No , XXXCreatDateVersion.Document_Date , XXXCreatDateVersion.Exe_Version_For_Create_Date, XXXCreatDateVersion.Created_By , XXXCreatDateVersion.Created_Date , XXXCreatDateVersion.Exe_Version_For_Modify_Date,XXXCreatDateVersion.Modify_By,XXXCreatDateVersion.Modify_Date from  ( " &
                          "  select FirstTable.Document_No  ,FirstTable.Document_Date ,SecondTable.Version_No as Exe_Version_For_Create_Date ,FirstTable.Created_By as Created_By , FirstTable.Created_Date  as Created_Date , '' as Exe_Version_For_Modify_Date, FirstTable.Modify_By  , FirstTable.Modify_Date as  Modify_Date  from  ( " &
                          "  Select 1 as SNO, TSPL_GRN_HEAD  .GRN_NO as  Document_No ,TSPL_GRN_HEAD  .GRN_Date as Document_Date,TSPL_GRN_HEAD  .Created_By , DATEDIFF(dd, 0, convert(date,TSPL_GRN_HEAD  .Created_Date,103)) + case when TSPL_GRN_HEAD  .GRN_Date is not null then  convert(datetime, CAST(TSPL_GRN_HEAD  .GRN_Date AS time) ) else '' end as Created_Date ,TSPL_GRN_HEAD  .Modify_By, DATEDIFF(dd, 0, convert(date,TSPL_GRN_HEAD  .Modify_Date,103)) + case when TSPL_GRN_HEAD  .Posting_Date is not null then  convert(datetime, CAST(TSPL_GRN_HEAD  .Posting_Date AS time) ) else '' end as  Modify_Date   from TSPL_GRN_HEAD    " &
                          "  where convert(date,TSPL_GRN_HEAD  .Created_Date,103) >= convert(date,'" + dtpFromDate.Value + "',103) and convert(date,TSPL_GRN_HEAD  .Created_Date,103) <= convert(date,'" + dtpToDate.Value + "',103)  " + whrLoc + " " + whrCreatedUser + " " + whrModifiedUser + "  ) as FirstTable " &
                          "  left outer join " &
                          " (select 1 as  SNO, a.Version_No , a.Date as From_Date ,(SELECT min(b.date)  FROM TSPL_Exe_Deployment b WHERE b.Date > a.Date   ) as To_Date  from TSPL_Exe_Deployment a ) SecondTable on SecondTable.SNO = FirstTable.SNO  " &
                          "  where convert (datetime ,FirstTable.Created_Date,103) between convert(datetime,SecondTable.From_Date) and convert(datetime,SecondTable.To_Date)   " &
                          " ) XXXCreatDateVersion " &
                          "  Union All  " &
                          " select XXXModifyDateVersion.Document_No , XXXModifyDateVersion.Document_Date , XXXModifyDateVersion.Exe_Version_For_Create_Date, XXXModifyDateVersion.Created_By , XXXModifyDateVersion.Created_Date , XXXModifyDateVersion.Exe_Version_For_Modify_Date,XXXModifyDateVersion.Modify_By,XXXModifyDateVersion.Modify_Date from (  " &
                          " select FirstTable.Document_No  ,FirstTable.Document_Date ,'' as Exe_Version_For_Create_Date ,FirstTable.Created_By as Created_By , FirstTable.Created_Date  as Created_Date , SecondTable.Version_No  as Exe_Version_For_Modify_Date, FirstTable.Modify_By  , FirstTable.Modify_Date as  Modify_Date  from  ( " &
                          " Select 1 as SNO, TSPL_GRN_HEAD  .GRN_NO as  Document_No ,TSPL_GRN_HEAD  .GRN_Date as Document_Date,TSPL_GRN_HEAD  .Created_By , DATEDIFF(dd, 0, convert(date,TSPL_GRN_HEAD  .Created_Date,103)) + case when TSPL_GRN_HEAD  .GRN_Date is not null then  convert(datetime, CAST(TSPL_GRN_HEAD  .GRN_Date AS time) ) else '' end as Created_Date ,TSPL_GRN_HEAD  .Modify_By, DATEDIFF(dd, 0, convert(date,TSPL_GRN_HEAD  .Modify_Date,103)) + case when TSPL_GRN_HEAD  .Posting_Date is not null then  convert(datetime, CAST(TSPL_GRN_HEAD  .Posting_Date AS time) ) else '' end as  Modify_Date   from TSPL_GRN_HEAD    " &
                          " where convert(date,TSPL_GRN_HEAD  .Modify_Date,103) >= convert(date,'" + dtpFromDate.Value + "',103) and convert(date,TSPL_GRN_HEAD  .Modify_Date,103) <= convert(date,'" + dtpToDate.Value + "',103) " + whrLoc + " " + whrCreatedUser + " " + whrModifiedUser + "     ) as FirstTable " &
                          " left outer join  " &
                          " (select 1 as  SNO, a.Version_No , a.Date as From_Date ,(SELECT min(b.date)  FROM TSPL_Exe_Deployment b WHERE b.Date > a.Date   ) as To_Date  from TSPL_Exe_Deployment a ) SecondTable on SecondTable.SNO = FirstTable.SNO " &
                          "  where convert (datetime ,FirstTable.Modify_Date,103) between convert(datetime,SecondTable.From_Date) and convert(datetime,SecondTable.To_Date)  " &
                          "   ) XXXModifyDateVersion ) XXXFinal  " &
                          "  left outer join TSPL_User_Master on TSPL_User_Master.User_Code = XXXFinal.Created_By " &
                          "  left outer join  (select Segment_code , Description as Created_By_Deparment  from TSPL_GL_SEGMENT_CODE where  Seg_No=3 ) TBL_Department on TBL_Department.Segment_code =TSPL_User_Master.Segment_code " &
                          " group by XXXFinal.Document_No " &
                          " order by convert (datetime,max(XXXFinal.Document_Date)) ,XXXFinal.Document_No "

                    'qry = " select FirstTable.Document_No  ,FirstTable.Document_Date ,SecondTable.Version_No as Exe_Version ,FirstTable.Created_By as Created_By ,convert(varchar, FirstTable.Created_Date,103)   as Created_Date , FirstTable.Modify_By  ,convert (varchar,FirstTable.Modify_Date,103) as Modify_Date   from " & _
                    '      " (Select 1 as SNO, TSPL_GRN_HEAD.GRN_No as  Document_No ,TSPL_GRN_HEAD.GRN_Date as Document_Date,TSPL_GRN_HEAD.Created_By ,TSPL_GRN_HEAD.Created_Date,TSPL_GRN_HEAD.Modify_By,TSPL_GRN_HEAD.Modify_Date   from TSPL_GRN_HEAD " & _
                    '      "	where convert(date,TSPL_GRN_HEAD.GRN_Date,103) >= convert(date,'" + dtpFromDate.Value + "',103) and convert(date,TSPL_GRN_HEAD.GRN_Date,103) <= convert(date,'" + dtpToDate.Value + "',103)    ) as FirstTable left outer join  " & _
                    '      " (select 1 as  SNO, a.Version_No , a.Date as From_Date ,(SELECT min(b.date)  FROM TSPL_Exe_Deployment b WHERE b.Date > a.Date   ) as To_Date  from TSPL_Exe_Deployment a ) SecondTable on SecondTable.SNO = FirstTable.SNO " & _
                    '      " where convert (datetime ,FirstTable.Document_Date) between convert(datetime,SecondTable.From_Date) and convert(datetime,SecondTable.To_Date) " & _
                    '      " order by convert (datetime,FirstTable.Document_Date) ,FirstTable.Document_No "


                    'qry = "SELECT CAST(TSPL_GRN_HEAD.Status as BIT ) as Status, TSPL_GRN_HEAD.GRN_No as [Document Id], TSPL_GRN_HEAD.GRN_Date as [Document Date],isnull(TSPL_GRN_HEAD.GRN_Total_Amt,0)  as [Amount], TSPL_GRN_HEAD.Vendor_Code as [Vendor Code], TSPL_GRN_HEAD.Vendor_Name as [Vendor Name], TSPL_GRN_HEAD.Description as Description, CAST(TSPL_GRN_HEAD.On_Hold as bit) as Hold,TSPL_GRN_HEAD.Created_By as 'Created By' FROM TSPL_GRN_HEAD   where  convert(date,TSPL_GRN_HEAD.GRN_Date,103) >= convert(date,'" + dtpFromDate.Value + "',103) and convert(date,TSPL_GRN_HEAD.GRN_Date,103) <= convert(date,'" + dtpToDate.Value + "',103) "
                    'qry += " ORDER BY TSPL_GRN_HEAD.GRN_Date , TSPL_GRN_HEAD.GRN_No "
                End If
            End If
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), "Material Receipt Note") = CompairStringResult.Equal Then
            Load_Authorisation(clsUserMgtCode.mbtnMRN)
            If dtAuthen.Rows.Count > 0 Then
                If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then
                    gv1.DataSource = Nothing

                    If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                        whrLoc = " and TSPL_MRN_HEAD.Bill_To_Location in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ") "
                    Else
                        whrLoc = ""
                    End If
                    If txtCreatedBy.arrValueMember IsNot Nothing AndAlso txtCreatedBy.arrValueMember.Count > 0 Then
                        whrCreatedUser = " and TSPL_MRN_HEAD.Created_By in (" + clsCommon.GetMulcallString(txtCreatedBy.arrValueMember) + ") "
                    Else
                        whrCreatedUser = ""
                    End If
                    If txtModifyedBy.arrValueMember IsNot Nothing AndAlso txtModifyedBy.arrValueMember.Count > 0 Then
                        whrModifiedUser = " and TSPL_MRN_HEAD.Modify_By in (" + clsCommon.GetMulcallString(txtCreatedBy.arrValueMember) + ") "
                    Else
                        whrModifiedUser = ""
                    End If

                    qry = "  select XXXFinal.Document_No , max( XXXFinal.Document_Date ) as Document_Date , max( XXXFinal.Exe_Version_For_Create_Date) as Exe_Version_On_Document_Create,max( TBL_Department.Created_By_Deparment ) as Created_By_Deparment ,max(XXXFinal.Created_By) as Created_By ,max( XXXFinal.Created_Date ) as Created_Date, max( XXXFinal.Exe_Version_For_Modify_Date) as Exe_Version_On_Document_Modify , max(XXXFinal.Modify_By ) as Modify_By , max(XXXFinal.Modify_Date) as Modify_Date  from  " &
                          " ( " &
                          "  select XXXCreatDateVersion.Document_No , XXXCreatDateVersion.Document_Date , XXXCreatDateVersion.Exe_Version_For_Create_Date, XXXCreatDateVersion.Created_By , XXXCreatDateVersion.Created_Date , XXXCreatDateVersion.Exe_Version_For_Modify_Date,XXXCreatDateVersion.Modify_By,XXXCreatDateVersion.Modify_Date from  ( " &
                          "  select FirstTable.Document_No  ,FirstTable.Document_Date ,SecondTable.Version_No as Exe_Version_For_Create_Date ,FirstTable.Created_By as Created_By , FirstTable.Created_Date  as Created_Date , '' as Exe_Version_For_Modify_Date, FirstTable.Modify_By  , FirstTable.Modify_Date as  Modify_Date  from  ( " &
                          "  Select 1 as SNO, TSPL_MRN_HEAD  .MRN_No as  Document_No ,TSPL_MRN_HEAD  .MRN_Date as Document_Date,TSPL_MRN_HEAD  .Created_By , DATEDIFF(dd, 0, convert(date,TSPL_MRN_HEAD  .Created_Date,103)) + case when TSPL_MRN_HEAD  .MRN_Date is not null then  convert(datetime, CAST(TSPL_MRN_HEAD  .MRN_Date AS time) ) else '' end as Created_Date ,TSPL_MRN_HEAD  .Modify_By, DATEDIFF(dd, 0, convert(date,TSPL_MRN_HEAD  .Modify_Date,103)) + case when TSPL_MRN_HEAD  .Posting_Date is not null then  convert(datetime, CAST(TSPL_MRN_HEAD  .Posting_Date AS time) ) else '' end as  Modify_Date   from TSPL_MRN_HEAD    " &
                          "  where convert(date,TSPL_MRN_HEAD  .Created_Date,103) >= convert(date,'" + dtpFromDate.Value + "',103) and convert(date,TSPL_MRN_HEAD  .Created_Date,103) <= convert(date,'" + dtpToDate.Value + "',103) " + whrLoc + " " + whrCreatedUser + " " + whrModifiedUser + "        ) as FirstTable " &
                          "  left outer join " &
                          " (select 1 as  SNO, a.Version_No , a.Date as From_Date ,(SELECT min(b.date)  FROM TSPL_Exe_Deployment b WHERE b.Date > a.Date   ) as To_Date  from TSPL_Exe_Deployment a ) SecondTable on SecondTable.SNO = FirstTable.SNO  " &
                          "  where convert (datetime ,FirstTable.Created_Date,103) between convert(datetime,SecondTable.From_Date) and convert(datetime,SecondTable.To_Date)   " &
                          " ) XXXCreatDateVersion " &
                          "  Union All  " &
                          " select XXXModifyDateVersion.Document_No , XXXModifyDateVersion.Document_Date , XXXModifyDateVersion.Exe_Version_For_Create_Date, XXXModifyDateVersion.Created_By , XXXModifyDateVersion.Created_Date , XXXModifyDateVersion.Exe_Version_For_Modify_Date,XXXModifyDateVersion.Modify_By,XXXModifyDateVersion.Modify_Date from (  " &
                          " select FirstTable.Document_No  ,FirstTable.Document_Date ,'' as Exe_Version_For_Create_Date ,FirstTable.Created_By as Created_By , FirstTable.Created_Date  as Created_Date , SecondTable.Version_No  as Exe_Version_For_Modify_Date, FirstTable.Modify_By  , FirstTable.Modify_Date as  Modify_Date  from  ( " &
                          " Select 1 as SNO, TSPL_MRN_HEAD  .MRN_No as  Document_No ,TSPL_MRN_HEAD  .MRN_Date as Document_Date,TSPL_MRN_HEAD  .Created_By , DATEDIFF(dd, 0, convert(date,TSPL_MRN_HEAD  .Created_Date,103)) + case when TSPL_MRN_HEAD  .MRN_Date is not null then  convert(datetime, CAST(TSPL_MRN_HEAD  .MRN_Date AS time) ) else '' end as Created_Date ,TSPL_MRN_HEAD  .Modify_By, DATEDIFF(dd, 0, convert(date,TSPL_MRN_HEAD  .Modify_Date,103)) + case when TSPL_MRN_HEAD  .Posting_Date is not null then  convert(datetime, CAST(TSPL_MRN_HEAD  .Posting_Date AS time) ) else '' end as  Modify_Date   from TSPL_MRN_HEAD    " &
                          " where convert(date,TSPL_MRN_HEAD  .Modify_Date,103) >= convert(date,'" + dtpFromDate.Value + "',103) and convert(date,TSPL_MRN_HEAD  .Modify_Date,103) <= convert(date,'" + dtpToDate.Value + "',103)  " + whrLoc + " " + whrCreatedUser + " " + whrModifiedUser + "       ) as FirstTable " &
                          " left outer join  " &
                          " (select 1 as  SNO, a.Version_No , a.Date as From_Date ,(SELECT min(b.date)  FROM TSPL_Exe_Deployment b WHERE b.Date > a.Date   ) as To_Date  from TSPL_Exe_Deployment a ) SecondTable on SecondTable.SNO = FirstTable.SNO " &
                          "  where convert (datetime ,FirstTable.Modify_Date,103) between convert(datetime,SecondTable.From_Date) and convert(datetime,SecondTable.To_Date)  " &
                          "   ) XXXModifyDateVersion ) XXXFinal " &
                          " left outer join TSPL_User_Master on TSPL_User_Master.User_Code = XXXFinal.Created_By " &
                          " left outer join  (select Segment_code , Description as Created_By_Deparment  from TSPL_GL_SEGMENT_CODE where  Seg_No=3 ) TBL_Department on TBL_Department.Segment_code =TSPL_User_Master.Segment_code " &
                          " group by XXXFinal.Document_No " &
                          " order by convert (datetime,max(XXXFinal.Document_Date)) ,XXXFinal.Document_No "

                    'qry = " select FirstTable.Document_No  ,FirstTable.Document_Date ,SecondTable.Version_No as Exe_Version ,FirstTable.Created_By as Created_By ,convert(varchar, FirstTable.Created_Date,103)   as Created_Date , FirstTable.Modify_By  ,convert (varchar,FirstTable.Modify_Date,103) as Modify_Date   from " & _
                    '     " (Select 1 as SNO, TSPL_MRN_HEAD.MRN_No as  Document_No ,TSPL_MRN_HEAD.MRN_Date as Document_Date,TSPL_MRN_HEAD.Created_By ,TSPL_MRN_HEAD.Created_Date,TSPL_MRN_HEAD.Modify_By,TSPL_MRN_HEAD.Modify_Date   from TSPL_MRN_HEAD " & _
                    '     "	where convert(date,TSPL_MRN_HEAD.MRN_Date,103) >= convert(date,'" + dtpFromDate.Value + "',103) and convert(date,TSPL_MRN_HEAD.MRN_Date,103) <= convert(date,'" + dtpToDate.Value + "',103)    ) as FirstTable left outer join  " & _
                    '     " (select 1 as  SNO, a.Version_No , a.Date as From_Date ,(SELECT min(b.date)  FROM TSPL_Exe_Deployment b WHERE b.Date > a.Date   ) as To_Date  from TSPL_Exe_Deployment a ) SecondTable on SecondTable.SNO = FirstTable.SNO " & _
                    '     " where convert (datetime ,FirstTable.Document_Date) between convert(datetime,SecondTable.From_Date) and convert(datetime,SecondTable.To_Date) " & _
                    '     " order by convert (datetime,FirstTable.Document_Date) ,FirstTable.Document_No "

                    'qry = "SELECT CAST(TSPL_MRN_HEAD.Status as BIT ) as Status, TSPL_MRN_HEAD.MRN_No as [Document Id], TSPL_MRN_HEAD.MRN_Date as [Document Date],isnull(TSPL_MRN_HEAD.MRN_Total_Amt,0) as [Amount], TSPL_MRN_HEAD.Vendor_Code as [Vendor Code], TSPL_MRN_HEAD.Vendor_Name as [Vendor Name], TSPL_MRN_HEAD.Description, CAST(TSPL_MRN_HEAD.On_Hold as bit) as Hold,TSPL_MRN_HEAD.Created_By as 'Created By' FROM TSPL_MRN_HEAD   where  convert(date,TSPL_MRN_HEAD.MRN_Date,103) >= convert(date,'" + dtpFromDate.Value + "',103) and convert(date,TSPL_MRN_HEAD.MRN_Date,103) <= convert(date,'" + dtpToDate.Value + "',103) "
                    'qry += " ORDER BY TSPL_MRN_HEAD.MRN_Date , TSPL_MRN_HEAD.MRN_No "
                End If
            End If
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), "Store Receipt Note") = CompairStringResult.Equal Then
            Load_Authorisation(clsUserMgtCode.mbtnSRN)
            If dtAuthen.Rows.Count > 0 Then
                If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then
                    gv1.DataSource = Nothing
                    If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                        whrLoc = " and TSPL_MRN_HEAD.Bill_To_Location in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ") "
                    Else
                        whrLoc = ""
                    End If
                    If txtCreatedBy.arrValueMember IsNot Nothing AndAlso txtCreatedBy.arrValueMember.Count > 0 Then
                        whrCreatedUser = " and TSPL_MRN_HEAD.Created_By in (" + clsCommon.GetMulcallString(txtCreatedBy.arrValueMember) + ") "
                    Else
                        whrCreatedUser = ""
                    End If
                    If txtModifyedBy.arrValueMember IsNot Nothing AndAlso txtModifyedBy.arrValueMember.Count > 0 Then
                        whrModifiedUser = " and TSPL_MRN_HEAD.Modify_By in (" + clsCommon.GetMulcallString(txtCreatedBy.arrValueMember) + ") "
                    Else
                        whrModifiedUser = ""
                    End If
                    qry = "  select XXXFinal.Document_No , max( XXXFinal.Document_Date ) as Document_Date , max( XXXFinal.Exe_Version_For_Create_Date) as Exe_Version_On_Document_Create,max( TBL_Department.Created_By_Deparment ) as Created_By_Deparment ,max(XXXFinal.Created_By) as Created_By ,max( XXXFinal.Created_Date ) as Created_Date, max( XXXFinal.Exe_Version_For_Modify_Date) as Exe_Version_On_Document_Modify , max(XXXFinal.Modify_By ) as Modify_By , max(XXXFinal.Modify_Date) as Modify_Date  from  " &
                          " ( " &
                          "  select XXXCreatDateVersion.Document_No , XXXCreatDateVersion.Document_Date , XXXCreatDateVersion.Exe_Version_For_Create_Date, XXXCreatDateVersion.Created_By , XXXCreatDateVersion.Created_Date , XXXCreatDateVersion.Exe_Version_For_Modify_Date,XXXCreatDateVersion.Modify_By,XXXCreatDateVersion.Modify_Date from  ( " &
                          "  select FirstTable.Document_No  ,FirstTable.Document_Date ,SecondTable.Version_No as Exe_Version_For_Create_Date ,FirstTable.Created_By as Created_By , FirstTable.Created_Date  as Created_Date , '' as Exe_Version_For_Modify_Date, FirstTable.Modify_By  , FirstTable.Modify_Date as  Modify_Date  from  ( " &
                          "  Select 1 as SNO, TSPL_SRN_HEAD  .SRN_No as  Document_No ,TSPL_SRN_HEAD  .SRN_Date as Document_Date,TSPL_SRN_HEAD  .Created_By , DATEDIFF(dd, 0, convert(date,TSPL_SRN_HEAD  .Created_Date,103)) + case when TSPL_SRN_HEAD  .SRN_Date is not null then  convert(datetime, CAST(TSPL_SRN_HEAD  .SRN_Date AS time) ) else '' end as Created_Date ,TSPL_SRN_HEAD  .Modify_By, DATEDIFF(dd, 0, convert(date,TSPL_SRN_HEAD  .Modify_Date,103)) + case when TSPL_SRN_HEAD  .Posting_Date is not null then  convert(datetime, CAST(TSPL_SRN_HEAD  .Posting_Date AS time) ) else '' end as  Modify_Date   from TSPL_SRN_HEAD    " &
                          "  where convert(date,TSPL_SRN_HEAD  .Created_Date,103) >= convert(date,'" + dtpFromDate.Value + "',103) and convert(date,TSPL_SRN_HEAD  .Created_Date,103) <= convert(date,'" + dtpToDate.Value + "',103)  " + whrLoc + " " + whrCreatedUser + " " + whrModifiedUser + "     ) as FirstTable " &
                          "  left outer join " &
                          " (select 1 as  SNO, a.Version_No , a.Date as From_Date ,(SELECT min(b.date)  FROM TSPL_Exe_Deployment b WHERE b.Date > a.Date   ) as To_Date  from TSPL_Exe_Deployment a ) SecondTable on SecondTable.SNO = FirstTable.SNO  " &
                          "  where convert (datetime ,FirstTable.Created_Date,103) between convert(datetime,SecondTable.From_Date) and convert(datetime,SecondTable.To_Date)   " &
                          " ) XXXCreatDateVersion " &
                          "  Union All  " &
                          " select XXXModifyDateVersion.Document_No , XXXModifyDateVersion.Document_Date , XXXModifyDateVersion.Exe_Version_For_Create_Date, XXXModifyDateVersion.Created_By , XXXModifyDateVersion.Created_Date , XXXModifyDateVersion.Exe_Version_For_Modify_Date,XXXModifyDateVersion.Modify_By,XXXModifyDateVersion.Modify_Date from (  " &
                          " select FirstTable.Document_No  ,FirstTable.Document_Date ,'' as Exe_Version_For_Create_Date ,FirstTable.Created_By as Created_By , FirstTable.Created_Date  as Created_Date , SecondTable.Version_No  as Exe_Version_For_Modify_Date, FirstTable.Modify_By  , FirstTable.Modify_Date as  Modify_Date  from  ( " &
                          " Select 1 as SNO, TSPL_SRN_HEAD  .SRN_No as  Document_No ,TSPL_SRN_HEAD  .SRN_Date as Document_Date,TSPL_SRN_HEAD  .Created_By , DATEDIFF(dd, 0, convert(date,TSPL_SRN_HEAD  .Created_Date,103)) + case when TSPL_SRN_HEAD  .SRN_Date is not null then  convert(datetime, CAST(TSPL_SRN_HEAD  .SRN_Date AS time) ) else '' end as Created_Date ,TSPL_SRN_HEAD  .Modify_By, DATEDIFF(dd, 0, convert(date,TSPL_SRN_HEAD  .Modify_Date,103)) + case when TSPL_SRN_HEAD  .Posting_Date is not null then  convert(datetime, CAST(TSPL_SRN_HEAD  .Posting_Date AS time) ) else '' end as  Modify_Date   from TSPL_SRN_HEAD    " &
                          " where convert(date,TSPL_SRN_HEAD  .Modify_Date,103) >= convert(date,'" + dtpFromDate.Value + "',103) and convert(date,TSPL_SRN_HEAD  .Modify_Date,103) <= convert(date,'" + dtpToDate.Value + "',103)   " + whrLoc + " " + whrCreatedUser + " " + whrModifiedUser + "    ) as FirstTable " &
                          " left outer join  " &
                          " (select 1 as  SNO, a.Version_No , a.Date as From_Date ,(SELECT min(b.date)  FROM TSPL_Exe_Deployment b WHERE b.Date > a.Date   ) as To_Date  from TSPL_Exe_Deployment a ) SecondTable on SecondTable.SNO = FirstTable.SNO " &
                          "  where convert (datetime ,FirstTable.Modify_Date,103) between convert(datetime,SecondTable.From_Date) and convert(datetime,SecondTable.To_Date)  " &
                          "   ) XXXModifyDateVersion ) XXXFinal " &
                          " left outer join TSPL_User_Master on TSPL_User_Master.User_Code = XXXFinal.Created_By " &
                          " left outer join  (select Segment_code , Description as Created_By_Deparment  from TSPL_GL_SEGMENT_CODE where  Seg_No=3 ) TBL_Department on TBL_Department.Segment_code =TSPL_User_Master.Segment_code " &
                          " group by XXXFinal.Document_No " &
                          " order by convert (datetime,max(XXXFinal.Document_Date)) ,XXXFinal.Document_No "

                    'qry = " select FirstTable.Document_No  ,FirstTable.Document_Date ,SecondTable.Version_No as Exe_Version ,FirstTable.Created_By as Created_By ,convert(varchar, FirstTable.Created_Date,103)   as Created_Date , FirstTable.Modify_By  ,convert (varchar,FirstTable.Modify_Date,103) as Modify_Date   from " & _
                    '    " (Select 1 as SNO, TSPL_SRN_HEAD.SRN_No as  Document_No ,TSPL_SRN_HEAD.SRN_Date as Document_Date,TSPL_SRN_HEAD.Created_By ,TSPL_SRN_HEAD.Created_Date,TSPL_SRN_HEAD.Modify_By,TSPL_SRN_HEAD.Modify_Date   from TSPL_SRN_HEAD " & _
                    '    "	where convert(date,TSPL_SRN_HEAD.SRN_Date ,103) >= convert(date,'" + dtpFromDate.Value + "',103) and convert(date,TSPL_SRN_HEAD.SRN_Date ,103) <= convert(date,'" + dtpToDate.Value + "',103)    ) as FirstTable left outer join  " & _
                    '    " (select 1 as  SNO, a.Version_No , a.Date as From_Date ,(SELECT min(b.date)  FROM TSPL_Exe_Deployment b WHERE b.Date > a.Date   ) as To_Date  from TSPL_Exe_Deployment a ) SecondTable on SecondTable.SNO = FirstTable.SNO " & _
                    '    " where convert (datetime ,FirstTable.Document_Date) between convert(datetime,SecondTable.From_Date) and convert(datetime,SecondTable.To_Date) " & _
                    '    " order by convert (datetime,FirstTable.Document_Date) ,FirstTable.Document_No "

                    'qry = "SELECT CAST(TSPL_SRN_HEAD.Status as BIT ) as Status, TSPL_SRN_HEAD.SRN_No as [Document Id], TSPL_SRN_HEAD.SRN_Date as [Document Date],isnull(TSPL_SRN_HEAD.SRN_Total_Amt,0) as [Amount], TSPL_SRN_HEAD.Vendor_Code as [Vendor Code], TSPL_SRN_HEAD.Vendor_Name as [Vendor Name], TSPL_SRN_HEAD.Description as Description, CAST(TSPL_SRN_HEAD.On_Hold as bit) as Hold,TSPL_SRN_HEAD.Created_By as 'Created BY' FROM TSPL_SRN_HEAD   where  convert(date,TSPL_SRN_HEAD.SRN_Date,103) >= convert(date,'" + dtpFromDate.Value + "',103) and convert(date,TSPL_SRN_HEAD.SRN_Date,103) <= convert(date,'" + dtpToDate.Value + "',103) "
                    'qry += " ORDER BY TSPL_SRN_HEAD.SRN_Date , TSPL_SRN_HEAD.SRN_No "
                End If
            End If
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), "Purchase Invoice") = CompairStringResult.Equal Then
            Load_Authorisation(clsUserMgtCode.mbtnPurchaseInvoice)
            If dtAuthen.Rows.Count > 0 Then
                If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then
                    gv1.DataSource = Nothing
                    If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                        whrLoc = " and TSPL_PI_HEAD.Bill_To_Location in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ") "
                    Else
                        whrLoc = ""
                    End If
                    If txtCreatedBy.arrValueMember IsNot Nothing AndAlso txtCreatedBy.arrValueMember.Count > 0 Then
                        whrCreatedUser = " and TSPL_PI_HEAD.Created_By in (" + clsCommon.GetMulcallString(txtCreatedBy.arrValueMember) + ") "
                    Else
                        whrCreatedUser = ""
                    End If
                    If txtModifyedBy.arrValueMember IsNot Nothing AndAlso txtModifyedBy.arrValueMember.Count > 0 Then
                        whrModifiedUser = " and TSPL_PI_HEAD.Modify_By in (" + clsCommon.GetMulcallString(txtCreatedBy.arrValueMember) + ") "
                    Else
                        whrModifiedUser = ""
                    End If
                    qry = "  select XXXFinal.Document_No , max( XXXFinal.Document_Date ) as Document_Date , max( XXXFinal.Exe_Version_For_Create_Date) as Exe_Version_On_Document_Create, max(XXXFinal.Created_By) as Created_By ,max( TBL_Department.Created_By_Deparment ) as Created_By_Deparment,max( XXXFinal.Created_Date ) as Created_Date, max( XXXFinal.Exe_Version_For_Modify_Date) as Exe_Version_On_Document_Modify , max(XXXFinal.Modify_By ) as Modify_By , max(XXXFinal.Modify_Date) as Modify_Date  from  " &
                          " ( " &
                          "  select XXXCreatDateVersion.Document_No , XXXCreatDateVersion.Document_Date , XXXCreatDateVersion.Exe_Version_For_Create_Date, XXXCreatDateVersion.Created_By , XXXCreatDateVersion.Created_Date , XXXCreatDateVersion.Exe_Version_For_Modify_Date,XXXCreatDateVersion.Modify_By,XXXCreatDateVersion.Modify_Date from  ( " &
                          "  select FirstTable.Document_No  ,FirstTable.Document_Date ,SecondTable.Version_No as Exe_Version_For_Create_Date ,FirstTable.Created_By as Created_By , FirstTable.Created_Date  as Created_Date , '' as Exe_Version_For_Modify_Date, FirstTable.Modify_By  , FirstTable.Modify_Date as  Modify_Date  from  ( " &
                          "  Select 1 as SNO, TSPL_PI_HEAD  .PI_No as  Document_No ,TSPL_PI_HEAD  .PI_Date as Document_Date,TSPL_PI_HEAD  .Created_By , DATEDIFF(dd, 0, convert(date,TSPL_PI_HEAD  .Created_Date,103)) + case when TSPL_PI_HEAD  .PI_Date is not null then  convert(datetime, CAST(convert(datetime, TSPL_PI_HEAD  .PI_Date) AS time) ) else '' end as Created_Date ,TSPL_PI_HEAD  .Modify_By, DATEDIFF(dd, 0, convert(date,TSPL_PI_HEAD  .Modify_Date,103)) + case when TSPL_PI_HEAD  .Posting_Date is not null then  convert(datetime, CAST( convert(datetime,TSPL_PI_HEAD  .Posting_Date) AS time) ) else '' end as  Modify_Date   from TSPL_PI_HEAD    " &
                          "  where convert(date,TSPL_PI_HEAD  .Created_Date,103) >= convert(date,'" + dtpFromDate.Value + "',103) and convert(date,TSPL_PI_HEAD  .Created_Date,103) <= convert(date,'" + dtpToDate.Value + "',103)  " + whrLoc + " " + whrCreatedUser + " " + whrModifiedUser + "    ) as FirstTable " &
                          "  left outer join " &
                          " (select 1 as  SNO, a.Version_No , a.Date as From_Date ,(SELECT min(b.date)  FROM TSPL_Exe_Deployment b WHERE b.Date > a.Date   ) as To_Date  from TSPL_Exe_Deployment a ) SecondTable on SecondTable.SNO = FirstTable.SNO  " &
                          "  where convert (datetime ,FirstTable.Created_Date,103) between convert(datetime,SecondTable.From_Date) and convert(datetime,SecondTable.To_Date)   " &
                          " ) XXXCreatDateVersion " &
                          "  Union All  " &
                          " select XXXModifyDateVersion.Document_No , XXXModifyDateVersion.Document_Date , XXXModifyDateVersion.Exe_Version_For_Create_Date, XXXModifyDateVersion.Created_By , XXXModifyDateVersion.Created_Date , XXXModifyDateVersion.Exe_Version_For_Modify_Date,XXXModifyDateVersion.Modify_By,XXXModifyDateVersion.Modify_Date from (  " &
                          " select FirstTable.Document_No  ,FirstTable.Document_Date ,'' as Exe_Version_For_Create_Date ,FirstTable.Created_By as Created_By , FirstTable.Created_Date  as Created_Date , SecondTable.Version_No  as Exe_Version_For_Modify_Date, FirstTable.Modify_By  , FirstTable.Modify_Date as  Modify_Date  from  ( " &
                          " Select 1 as SNO, TSPL_PI_HEAD  .PI_No as  Document_No ,TSPL_PI_HEAD  .PI_Date as Document_Date,TSPL_PI_HEAD  .Created_By , DATEDIFF(dd, 0, convert(date,TSPL_PI_HEAD  .Created_Date,103)) + case when TSPL_PI_HEAD  .PI_Date is not null then  convert(datetime, CAST( convert(datetime,TSPL_PI_HEAD  .PI_Date) AS time) ) else '' end as Created_Date ,TSPL_PI_HEAD  .Modify_By, DATEDIFF(dd, 0, convert(date,TSPL_PI_HEAD  .Modify_Date,103)) + case when TSPL_PI_HEAD  .Posting_Date is not null then  convert(datetime, CAST(convert(datetime,TSPL_PI_HEAD  .Posting_Date) AS time) ) else '' end as  Modify_Date   from TSPL_PI_HEAD    " &
                          " where convert(date,TSPL_PI_HEAD  .Modify_Date,103) >= convert(date,'" + dtpFromDate.Value + "',103) and convert(date,TSPL_PI_HEAD  .Modify_Date,103) <= convert(date,'" + dtpToDate.Value + "',103)  " + whrLoc + " " + whrCreatedUser + " " + whrModifiedUser + "    ) as FirstTable " &
                          " left outer join  " &
                          " (select 1 as  SNO, a.Version_No , a.Date as From_Date ,(SELECT min(b.date)  FROM TSPL_Exe_Deployment b WHERE b.Date > a.Date   ) as To_Date  from TSPL_Exe_Deployment a ) SecondTable on SecondTable.SNO = FirstTable.SNO " &
                          "  where convert (datetime ,FirstTable.Modify_Date,103) between convert(datetime,SecondTable.From_Date) and convert(datetime,SecondTable.To_Date)  " &
                          "   ) XXXModifyDateVersion ) XXXFinal  " &
                          " left outer join TSPL_User_Master on TSPL_User_Master.User_Code = XXXFinal.Created_By " &
                          " left outer join  (select Segment_code , Description as Created_By_Deparment  from TSPL_GL_SEGMENT_CODE where  Seg_No=3 ) TBL_Department on TBL_Department.Segment_code =TSPL_User_Master.Segment_code " &
                          "group by XXXFinal.Document_No " &
                          " order by convert (datetime,max(XXXFinal.Document_Date)) ,XXXFinal.Document_No "

                    '  qry = " select FirstTable.Document_No  ,FirstTable.Document_Date ,SecondTable.Version_No as Exe_Version ,FirstTable.Created_By as Created_By ,convert(varchar, FirstTable.Created_Date,103)   as Created_Date , FirstTable.Modify_By  ,convert (varchar,FirstTable.Modify_Date,103) as Modify_Date   from " & _
                    '" (Select 1 as SNO, TSPL_PI_HEAD.PI_No as  Document_No ,TSPL_PI_HEAD.PI_Date as Document_Date,TSPL_PI_HEAD.Created_By ,TSPL_PI_HEAD.Created_Date,TSPL_PI_HEAD.Modify_By,TSPL_PI_HEAD.Modify_Date   from TSPL_PI_HEAD " & _
                    '"	where convert(date,TSPL_PI_HEAD.PI_Date ,103) >= convert(date,'" + dtpFromDate.Value + "',103) and convert(date,TSPL_PI_HEAD.PI_Date ,103) <= convert(date,'" + dtpToDate.Value + "',103)    ) as FirstTable left outer join  " & _
                    '" (select 1 as  SNO, a.Version_No , a.Date as From_Date ,(SELECT min(b.date)  FROM TSPL_Exe_Deployment b WHERE b.Date > a.Date   ) as To_Date  from TSPL_Exe_Deployment a ) SecondTable on SecondTable.SNO = FirstTable.SNO " & _
                    '" where convert (datetime ,FirstTable.Document_Date) between convert(datetime,SecondTable.From_Date) and convert(datetime,SecondTable.To_Date) " & _
                    '" order by convert (datetime,FirstTable.Document_Date) ,FirstTable.Document_No "


                    'qry = "SELECT CAST(TSPL_PI_HEAD.Status as BIT ) as Status, TSPL_PI_HEAD.PI_No as [Document Id], TSPL_PI_HEAD.PI_Date as [Document Date],isnull(TSPL_PI_HEAD.PI_Total_Amt,0) as [Amount], TSPL_PI_HEAD.Vendor_Code as [Vendor Code], TSPL_PI_HEAD.Vendor_Name as [Vendor Name], TSPL_PI_HEAD.Description as Description, CAST(TSPL_PI_HEAD.On_Hold as bit) as Hold,TSPL_PI_HEAD.Created_By as 'Created By' FROM TSPL_PI_HEAD   where  convert(date,TSPL_PI_HEAD.PI_Date,103) >= convert(date,'" + dtpFromDate.Value + "',103) and convert(date,TSPL_PI_HEAD.PI_Date,103) <= convert(date,'" + dtpToDate.Value + "',103) "
                    'qry += " ORDER BY TSPL_PI_HEAD.PI_Date , TSPL_PI_HEAD.PI_No "
                End If
            End If
            ''Added on 12/10/2011
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), "Purchase Return") = CompairStringResult.Equal Then
            Load_Authorisation(clsUserMgtCode.mbtnPurchaseReturn)
            If dtAuthen.Rows.Count > 0 Then
                If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then
                    gv1.DataSource = Nothing

                    qry = GetQueryByScreen("TSPL_PR_HEAD", "PR_No", "PR_Date", "Created_By", "Created_Date", "Modify_By", "Modify_Date", "Posting_Date", "", "Bill_to_Location")

                    '  qry = " select FirstTable.Document_No  ,FirstTable.Document_Date ,SecondTable.Version_No as Exe_Version ,FirstTable.Created_By as Created_By ,convert(varchar, FirstTable.Created_Date,103)   as Created_Date , FirstTable.Modify_By  ,convert (varchar,FirstTable.Modify_Date,103) as Modify_Date   from " & _
                    '" (Select 1 as SNO, TSPL_PR_HEAD.PR_No as  Document_No ,TSPL_PR_HEAD.PR_Date as Document_Date,TSPL_PR_HEAD.Created_By ,TSPL_PR_HEAD.Created_Date,TSPL_PR_HEAD.Modify_By,TSPL_PR_HEAD.Modify_Date   from TSPL_PR_HEAD " & _
                    '"	where convert(date,TSPL_PR_HEAD.PR_Date ,103) >= convert(date,'" + dtpFromDate.Value + "',103) and convert(date,TSPL_PR_HEAD.PR_Date ,103) <= convert(date,'" + dtpToDate.Value + "',103)    ) as FirstTable left outer join  " & _
                    '" (select 1 as  SNO, a.Version_No , a.Date as From_Date ,(SELECT min(b.date)  FROM TSPL_Exe_Deployment b WHERE b.Date > a.Date   ) as To_Date  from TSPL_Exe_Deployment a ) SecondTable on SecondTable.SNO = FirstTable.SNO " & _
                    '" where convert (datetime ,FirstTable.Document_Date) between convert(datetime,SecondTable.From_Date) and convert(datetime,SecondTable.To_Date) " & _
                    '" order by convert (datetime,FirstTable.Document_Date) ,FirstTable.Document_No "

                    'qry = "SELECT CAST(TSPL_PR_HEAD.Status as BIT ) as Status, TSPL_PR_HEAD.PR_No as [Document Id], TSPL_PR_HEAD.PR_Date as [Document Date],isnull(TSPL_PR_HEAD.PR_Total_Amt,0) as [Amount], TSPL_PR_HEAD.Vendor_Code as [Vendor Code], TSPL_PR_HEAD.Vendor_Name as [Vendor Name], TSPL_PR_HEAD.Description as Description, CAST(TSPL_PR_HEAD.On_Hold as bit) as Hold,TSPL_PR_HEAD.Created_By as 'Created By' FROM TSPL_PR_HEAD   where  convert(date,TSPL_PR_HEAD.PR_Date,103) >= convert(date,'" + dtpFromDate.Value + "',103) and convert(date,TSPL_PR_HEAD.PR_Date,103) <= convert(date,'" + dtpToDate.Value + "',103)  "
                    'qry += " ORDER BY TSPL_PR_HEAD.PR_Date , TSPL_PR_HEAD.PR_No "
                End If
            End If
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), "RGP/NRGP") = CompairStringResult.Equal Then
            Load_Authorisation(clsUserMgtCode.mbtnRGP_NRGP_Rpt)
            If dtAuthen.Rows.Count > 0 Then
                If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then
                    gv1.DataSource = Nothing

                    qry = GetQueryByScreen("TSPL_RGP_HEAD", "RGP_No", "RGP_Date", "Created_By", "Created_Date", "Modify_By", "Modify_Date", "Posting_Date", "", "Location")

                    '  qry = " select FirstTable.Document_No  ,FirstTable.Document_Date ,SecondTable.Version_No as Exe_Version ,FirstTable.Created_By as Created_By ,convert(varchar, FirstTable.Created_Date,103)   as Created_Date , FirstTable.Modify_By  ,convert (varchar,FirstTable.Modify_Date,103) as Modify_Date   from " & _
                    '" (Select 1 as SNO, TSPL_RGP_HEAD.RGP_No as  Document_No ,TSPL_RGP_HEAD.RGP_Date as Document_Date,TSPL_RGP_HEAD.Created_By ,TSPL_RGP_HEAD.Created_Date,TSPL_RGP_HEAD.Modify_By,TSPL_RGP_HEAD.Modify_Date   from TSPL_RGP_HEAD " & _
                    '"	where convert(date,TSPL_RGP_HEAD.RGP_Date ,103) >= convert(date,'" + dtpFromDate.Value + "',103) and convert(date,TSPL_RGP_HEAD.RGP_Date ,103) <= convert(date,'" + dtpToDate.Value + "',103)    ) as FirstTable left outer join  " & _
                    '" (select 1 as  SNO, a.Version_No , a.Date as From_Date ,(SELECT min(b.date)  FROM TSPL_Exe_Deployment b WHERE b.Date > a.Date   ) as To_Date  from TSPL_Exe_Deployment a ) SecondTable on SecondTable.SNO = FirstTable.SNO " & _
                    '" where convert (datetime ,FirstTable.Document_Date) between convert(datetime,SecondTable.From_Date) and convert(datetime,SecondTable.To_Date) " & _
                    '" order by convert (datetime,FirstTable.Document_Date) ,FirstTable.Document_No "

                    'qry = "SELECT CAST(TSPL_RGP_HEAD.Status as BIT ) as Status, TSPL_RGP_HEAD.RGP_No as [Document Id], TSPL_RGP_HEAD.RGP_Date as [Document Date],isnull(TSPL_RGP_HEAD.Document_Amount,0) as [Amount], TSPL_RGP_HEAD.Vendor_Code as [Vendor Code], TSPL_RGP_HEAD.Vendor_Name as [Vendor Name], TSPL_RGP_HEAD.Remarks as Description, CAST(TSPL_RGP_HEAD.On_Hold as bit) as Hold,TSPL_RGP_HEAD.Created_By as 'Created BY' FROM TSPL_RGP_HEAD   where  convert(date,TSPL_RGP_HEAD.RGP_Date,103) >= convert(date,'" + dtpFromDate.Value + "',103) and convert(date,TSPL_RGP_HEAD.RGP_Date,103) <= convert(date,'" + dtpToDate.Value + "',103) "
                    'qry += " ORDER BY TSPL_RGP_HEAD.RGP_Date , TSPL_RGP_HEAD.RGP_No "
                End If
            End If

        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), "Issue/Return/Transfer") = CompairStringResult.Equal Then
            Load_Authorisation(clsUserMgtCode.mbtnIssueReturn)
            If dtAuthen.Rows.Count > 0 Then
                If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then
                    gv1.DataSource = Nothing

                    qry = GetQueryByScreen("TSPL_IssueReturn_HEAD", "Doc_No", "Doc_Date", "Created_By", "Created_Date", "Modify_By", "Modify_Date", "Posting_Date", "", "From_Location")
                    '  qry = " select FirstTable.Document_No  ,FirstTable.Document_Date ,SecondTable.Version_No as Exe_Version ,FirstTable.Created_By as Created_By ,convert(varchar, FirstTable.Created_Date,103)   as Created_Date , FirstTable.Modify_By  ,convert (varchar,FirstTable.Modify_Date,103) as Modify_Date   from " & _
                    '" (Select 1 as SNO, TSPL_IssueReturn_HEAD.Doc_No as  Document_No ,TSPL_IssueReturn_HEAD.Doc_Date as Document_Date,TSPL_IssueReturn_HEAD.Created_By ,TSPL_IssueReturn_HEAD.Created_Date,TSPL_IssueReturn_HEAD.Modify_By,TSPL_IssueReturn_HEAD.Modify_Date   from TSPL_IssueReturn_HEAD " & _
                    '"	where convert(date,TSPL_IssueReturn_HEAD.Doc_Date ,103) >= convert(date,'" + dtpFromDate.Value + "',103) and convert(date,TSPL_IssueReturn_HEAD.Doc_Date ,103) <= convert(date,'" + dtpToDate.Value + "',103)    ) as FirstTable left outer join  " & _
                    '" (select 1 as  SNO, a.Version_No , a.Date as From_Date ,(SELECT min(b.date)  FROM TSPL_Exe_Deployment b WHERE b.Date > a.Date   ) as To_Date  from TSPL_Exe_Deployment a ) SecondTable on SecondTable.SNO = FirstTable.SNO " & _
                    '" where convert (datetime ,FirstTable.Document_Date) between convert(datetime,SecondTable.From_Date) and convert(datetime,SecondTable.To_Date) " & _
                    '" order by convert (datetime,FirstTable.Document_Date) ,FirstTable.Document_No "

                    'qry = "SELECT CAST(TSPL_IssueReturn_HEAD.Status as BIT ) as Status, TSPL_IssueReturn_HEAD.Doc_No as [Document Id], TSPL_IssueReturn_HEAD.Doc_Date as [Document Date], TSPL_IssueReturn_HEAD.Doc_Type as [Document Type],isnull(TSPL_IssueReturn_HEAD.Doc_Amt,0) as [Amount], TSPL_IssueReturn_HEAD.From_Location as [From Location],TSPL_IssueReturn_HEAD.To_Location as [To Location], TSPL_IssueReturn_HEAD.Remarks as Description, CAST(TSPL_IssueReturn_HEAD.On_Hold as bit) as Hold,TSPL_IssueReturn_HEAD.Created_By as 'Created By' FROM TSPL_IssueReturn_HEAD   where  convert(date,TSPL_IssueReturn_HEAD.Doc_Date,103) >= convert(date,'" + dtpFromDate.Value + "',103) and convert(date,TSPL_IssueReturn_HEAD.Doc_Date,103) <= convert(date,'" + dtpToDate.Value + "',103) "
                    'qry += " ORDER BY TSPL_IssueReturn_HEAD.Doc_Date , TSPL_IssueReturn_HEAD.Doc_No "
                End If
            End If
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), "Scrap LoadOut") = CompairStringResult.Equal Then
            Load_Authorisation(clsUserMgtCode.ScrapSale)
            If dtAuthen.Rows.Count > 0 Then
                If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then
                    gv1.DataSource = Nothing

                    qry = GetQueryByScreen("TSPL_SCRAPSALE_HEAD", "shipment_No", "shipment_Date", "Created_By", "Created_Date", "Modify_By", "Modify_Date", "Posting_Date", "", "Loc_Code")
                    '  qry = " select FirstTable.Document_No  ,FirstTable.Document_Date ,SecondTable.Version_No as Exe_Version ,FirstTable.Created_By as Created_By ,convert(varchar, FirstTable.Created_Date,103)   as Created_Date , FirstTable.Modify_By  ,convert (varchar,FirstTable.Modify_Date,103) as Modify_Date   from " & _
                    '" (Select 1 as SNO, TSPL_SCRAPSALE_HEAD.shipment_No as  Document_No ,TSPL_SCRAPSALE_HEAD.shipment_Date as Document_Date,TSPL_SCRAPSALE_HEAD.Created_By ,TSPL_SCRAPSALE_HEAD.Created_Date,TSPL_SCRAPSALE_HEAD.Modify_By,TSPL_SCRAPSALE_HEAD.Modify_Date   from TSPL_SCRAPSALE_HEAD " & _
                    '"	where convert(date,TSPL_SCRAPSALE_HEAD.shipment_Date ,103) >= convert(date,'" + dtpFromDate.Value + "',103) and convert(date,TSPL_SCRAPSALE_HEAD.shipment_Date ,103) <= convert(date,'" + dtpToDate.Value + "',103)    ) as FirstTable left outer join  " & _
                    '" (select 1 as  SNO, a.Version_No , a.Date as From_Date ,(SELECT min(b.date)  FROM TSPL_Exe_Deployment b WHERE b.Date > a.Date   ) as To_Date  from TSPL_Exe_Deployment a ) SecondTable on SecondTable.SNO = FirstTable.SNO " & _
                    '" where convert (datetime ,FirstTable.Document_Date) between convert(datetime,SecondTable.From_Date) and convert(datetime,SecondTable.To_Date) " & _
                    '" order by convert (datetime,FirstTable.Document_Date) ,FirstTable.Document_No "

                    'qry = "SELECT CAST((case when TSPL_SCRAPSALE_HEAD.ispost =1 then 1 else 0 end)as BIT) as Status, TSPL_SCRAPSALE_HEAD.shipment_No as [Document Id], TSPL_SCRAPSALE_HEAD.shipment_Date as [Document Date],isnull(TSPL_SCRAPSALE_HEAD.Doc_Amt,0) as [Amount],TSPL_SCRAPSALE_HEAD.cust_Code as [Customer Code],TSPL_SCRAPSALE_HEAD.cust_Name as [Customer Name], TSPL_SCRAPSALE_HEAD.Description as [Description],TSPL_SCRAPSALE_HEAD.Created_By as 'Created By' FROM TSPL_SCRAPSALE_HEAD WHERE  convert(date,TSPL_SCRAPSALE_HEAD.shipment_Date,103) >= convert(date,'" + dtpFromDate.Value + "',103) and convert(date,TSPL_SCRAPSALE_HEAD.shipment_Date ,103) <= convert(date,'" + dtpToDate.Value + "',103)"
                    'qry += "ORDER BY TSPL_SCRAPSALE_HEAD.shipment_Date , TSPL_SCRAPSALE_HEAD.shipment_No "
                End If
            End If
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), "Scrap Invoice") = CompairStringResult.Equal Then
            Load_Authorisation(clsUserMgtCode.ScrapSale)
            If dtAuthen.Rows.Count > 0 Then
                If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then
                    gv1.DataSource = Nothing

                    qry = GetQueryByScreen("TSPL_SCRAPINVOICE_HEAD", "invoice_No", "shipment_Date", "Created_By", "Created_Date", "Modify_By", "Modify_Date", "Posting_Date", "", "Loc_Code")
                    '  qry = " select FirstTable.Document_No  ,FirstTable.Document_Date ,SecondTable.Version_No as Exe_Version ,FirstTable.Created_By as Created_By ,convert(varchar, FirstTable.Created_Date,103)   as Created_Date , FirstTable.Modify_By  ,convert (varchar,FirstTable.Modify_Date,103) as Modify_Date   from " & _
                    '" (Select 1 as SNO, TSPL_SCRAPINVOICE_HEAD.invoice_No as  Document_No ,TSPL_SCRAPINVOICE_HEAD.shipment_Date as Document_Date,TSPL_SCRAPINVOICE_HEAD.Created_By ,TSPL_SCRAPINVOICE_HEAD.Created_Date,TSPL_SCRAPINVOICE_HEAD.Modify_By,TSPL_SCRAPINVOICE_HEAD.Modify_Date   from TSPL_SCRAPINVOICE_HEAD " & _
                    '"	where convert(date,TSPL_SCRAPINVOICE_HEAD.shipment_Date ,103) >= convert(date,'" + dtpFromDate.Value + "',103) and convert(date,TSPL_SCRAPINVOICE_HEAD.shipment_Date ,103) <= convert(date,'" + dtpToDate.Value + "',103)    ) as FirstTable left outer join  " & _
                    '" (select 1 as  SNO, a.Version_No , a.Date as From_Date ,(SELECT min(b.date)  FROM TSPL_Exe_Deployment b WHERE b.Date > a.Date   ) as To_Date  from TSPL_Exe_Deployment a ) SecondTable on SecondTable.SNO = FirstTable.SNO " & _
                    '" where convert (datetime ,FirstTable.Document_Date) between convert(datetime,SecondTable.From_Date) and convert(datetime,SecondTable.To_Date) " & _
                    '" order by convert (datetime,FirstTable.Document_Date) ,FirstTable.Document_No "


                    'qry = "SELECT CAST((case when TSPL_SCRAPINVOICE_HEAD.ispost =1 then 1 else 0 end)as BIT) as Status, TSPL_SCRAPINVOICE_HEAD.invoice_No as [Document Id], TSPL_SCRAPINVOICE_HEAD.shipment_Date as [Document Date],isnull(TSPL_SCRAPINVOICE_HEAD.Doc_Amt,0) as [Amount],TSPL_SCRAPINVOICE_HEAD.cust_Code as [Customer Code],TSPL_SCRAPINVOICE_HEAD.cust_Name as [Customer Name], TSPL_SCRAPINVOICE_HEAD.Description as [Description],TSPL_SCRAPINVOICE_HEAD.Created_By as 'Created By' FROM TSPL_SCRAPINVOICE_HEAD WHERE  convert(date,TSPL_SCRAPINVOICE_HEAD.shipment_Date,103) >= convert(date,'" + dtpFromDate.Value + "',103) and convert(date,TSPL_SCRAPINVOICE_HEAD.shipment_Date ,103) <= convert(date,'" + dtpToDate.Value + "',103)"
                    'qry += "ORDER BY TSPL_SCRAPINVOICE_HEAD.shipment_Date , TSPL_SCRAPINVOICE_HEAD.invoice_No "
                End If
            End If
        End If
        If clsCommon.CompairString(clsCommon.myCstr(qry), Nothing) <> CompairStringResult.Equal Then

            dt = clsDBFuncationality.GetDataTable(qry)
            gv1.DataSource = dt

            SetGridFormationOFgv()



        End If
        '-------------------------------------Code Ends Here--------------------------------------------

    End Sub

    ''-------------------------------------------------------------------
    '' Function For Filling--------Module(Material Management)-----------
    ''-------------------------------------------------------------------

    Sub FillMaterialManagement()

        If clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), "Transfer(Load-In)") = CompairStringResult.Equal Then
            Load_Authorisation(clsUserMgtCode.Transfer)
            If dtAuthen.Rows.Count > 0 Then
                If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then

                    qry = GetQueryByScreen("TSPL_TRANSFER_HEAD", "Transfer_No", "Transfer_Date", "Created_By", "Created_Date", "Modify_By", "Modify_Date", "Posting_Date", "And TSPL_TRANSFER_HEAD.Transfer_Type='LI' and (TSPL_TRANSFER_HEAD.Post = 'N' OR TSPL_TRANSFER_HEAD.Post IS NULL) and TSPL_TRANSFER_HEAD.is_Auto_Created_Trans=0 and  TSPL_TRANSFER_HEAD.Trans_Type<>'Route'   ")

                    '  qry = " select FirstTable.Document_No  ,FirstTable.Document_Date ,SecondTable.Version_No as Exe_Version ,FirstTable.Created_By as Created_By ,convert(varchar, FirstTable.Created_Date,103)   as Created_Date , FirstTable.Modify_By  ,convert (varchar,FirstTable.Modify_Date,103) as Modify_Date   from " & _
                    '" (Select 1 as SNO, TSPL_TRANSFER_HEAD.Transfer_No as  Document_No ,TSPL_TRANSFER_HEAD.Transfer_Date as Document_Date,TSPL_TRANSFER_HEAD.Created_By ,TSPL_TRANSFER_HEAD.Created_Date,TSPL_TRANSFER_HEAD.Modify_By,TSPL_TRANSFER_HEAD.Modify_Date   from TSPL_TRANSFER_HEAD " & _
                    '"	where convert(date,TSPL_TRANSFER_HEAD.Transfer_Date ,103) >= convert(date,'" + dtpFromDate.Value + "',103) and convert(date,TSPL_TRANSFER_HEAD.Transfer_Date ,103) <= convert(date,'" + dtpToDate.Value + "',103) And TSPL_TRANSFER_HEAD.Transfer_Type='LI' and (TSPL_TRANSFER_HEAD.Post = 'N' OR TSPL_TRANSFER_HEAD.Post IS NULL) and TSPL_TRANSFER_HEAD.is_Auto_Created_Trans=0 and  TSPL_TRANSFER_HEAD.Trans_Type<>'Route'   ) as FirstTable left outer join  " & _
                    '" (select 1 as  SNO, a.Version_No , a.Date as From_Date ,(SELECT min(b.date)  FROM TSPL_Exe_Deployment b WHERE b.Date > a.Date   ) as To_Date  from TSPL_Exe_Deployment a ) SecondTable on SecondTable.SNO = FirstTable.SNO " & _
                    '" where convert (datetime ,FirstTable.Document_Date) between convert(datetime,SecondTable.From_Date) and convert(datetime,SecondTable.To_Date) " & _
                    '" order by convert (datetime,FirstTable.Document_Date) ,FirstTable.Document_No "


                    'qry = "SELECT  CAST((case when TSPL_TRANSFER_HEAD.Post ='Y' then 1 else 0 end)as BIT) as Status, TSPL_TRANSFER_HEAD.Transfer_No  as [Document Id], TSPL_TRANSFER_HEAD.Transfer_Date as [Document Date], isnull(TSPL_TRANSFER_HEAD.Total_Transfer_Amount,0) as Amount, TSPL_TRANSFER_HEAD.Transfer_Type as [Transfer Type], TSPL_TRANSFER_HEAD.Load_Out_No, TSPL_TRANSFER_HEAD.Reference as [Reference], TSPL_TRANSFER_HEAD.Description as [Description],TSPL_TRANSFER_HEAD.Created_By as 'Created By', TSPL_TRANSFER_HEAD.FromLoc_Desc AS [To Location], TSPL_TRANSFER_HEAD.ToLoc_Desc as [From Location] FROM TSPL_TRANSFER_HEAD WHERE  convert(date,TSPL_TRANSFER_HEAD.Transfer_Date,103) >= convert(date,'" + dtpFromDate.Value + "',103) and convert(date,TSPL_TRANSFER_HEAD.Transfer_Date,103) <= convert(date,'" + dtpToDate.Value + "',103) And TSPL_TRANSFER_HEAD.Transfer_Type='LI' and (TSPL_TRANSFER_HEAD.Post = 'N' OR TSPL_TRANSFER_HEAD.Post IS NULL) and TSPL_TRANSFER_HEAD.is_Auto_Created_Trans=0"
                    'qry += " and  TSPL_TRANSFER_HEAD.Trans_Type<>'Route'"
                    'qry += " ORDER BY TSPL_TRANSFER_HEAD.Transfer_Date , TSPL_TRANSFER_HEAD.Transfer_No "


                End If
            End If
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), "Transfer(Load-Out)") = CompairStringResult.Equal Then
            Load_Authorisation(clsUserMgtCode.Transfer)
            If dtAuthen.Rows.Count > 0 Then
                If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then

                    qry = GetQueryByScreen("TSPL_TRANSFER_HEAD", "Transfer_No", "Transfer_Date", "Created_By", "Created_Date", "Modify_By", "Modify_Date", "Posting_Date", " And TSPL_TRANSFER_HEAD.Transfer_Type='LO' and (TSPL_TRANSFER_HEAD.Post = 'N' OR TSPL_TRANSFER_HEAD.Post IS NULL )and TSPL_TRANSFER_HEAD.is_Auto_Created_Trans=0   ")
                    '  qry = " select FirstTable.Document_No  ,FirstTable.Document_Date ,SecondTable.Version_No as Exe_Version ,FirstTable.Created_By as Created_By ,convert(varchar, FirstTable.Created_Date,103)   as Created_Date , FirstTable.Modify_By  ,convert (varchar,FirstTable.Modify_Date,103) as Modify_Date   from " & _
                    '" (Select 1 as SNO, TSPL_TRANSFER_HEAD.Transfer_No as  Document_No ,TSPL_TRANSFER_HEAD.Transfer_Date as Document_Date,TSPL_TRANSFER_HEAD.Created_By ,TSPL_TRANSFER_HEAD.Created_Date,TSPL_TRANSFER_HEAD.Modify_By,TSPL_TRANSFER_HEAD.Modify_Date   from TSPL_TRANSFER_HEAD " & _
                    '"	where convert(date,TSPL_TRANSFER_HEAD.Transfer_Date ,103) >= convert(date,'" + dtpFromDate.Value + "',103) and convert(date,TSPL_TRANSFER_HEAD.Transfer_Date ,103) <= convert(date,'" + dtpToDate.Value + "',103)  And TSPL_TRANSFER_HEAD.Transfer_Type='LO' and (TSPL_TRANSFER_HEAD.Post = 'N' OR TSPL_TRANSFER_HEAD.Post IS NULL )and TSPL_TRANSFER_HEAD.is_Auto_Created_Trans=0   ) as FirstTable left outer join  " & _
                    '" (select 1 as  SNO, a.Version_No , a.Date as From_Date ,(SELECT min(b.date)  FROM TSPL_Exe_Deployment b WHERE b.Date > a.Date   ) as To_Date  from TSPL_Exe_Deployment a ) SecondTable on SecondTable.SNO = FirstTable.SNO " & _
                    '" where convert (datetime ,FirstTable.Document_Date) between convert(datetime,SecondTable.From_Date) and convert(datetime,SecondTable.To_Date) " & _
                    '" order by convert (datetime,FirstTable.Document_Date) ,FirstTable.Document_No "

                    'qry = "SELECT CAST((case when TSPL_TRANSFER_HEAD.Post ='Y' then 1 else 0 end)as BIT) as Status, TSPL_TRANSFER_HEAD.Transfer_No  as [Document Id], TSPL_TRANSFER_HEAD.Transfer_Date as [Document Date], isnull(TSPL_TRANSFER_HEAD.Total_Transfer_Amount,0) as Amount, TSPL_TRANSFER_HEAD.Transfer_Type as [Transfer Type], TSPL_TRANSFER_HEAD.Reference as [Reference], TSPL_TRANSFER_HEAD.Description as [Description],TSPL_TRANSFER_HEAD.Created_By as 'Created By',TSPL_TRANSFER_HEAD.FromLoc_Desc AS [From Location], TSPL_TRANSFER_HEAD.ToLoc_Desc as [To Location] FROM TSPL_TRANSFER_HEAD WHERE  convert(date,TSPL_TRANSFER_HEAD.Transfer_Date,103) >= convert(date,'" + dtpFromDate.Value + "',103) and convert(date,TSPL_TRANSFER_HEAD.Transfer_Date,103) <= convert(date,'" + dtpToDate.Value + "',103) And TSPL_TRANSFER_HEAD.Transfer_Type='LO' and (TSPL_TRANSFER_HEAD.Post = 'N' OR TSPL_TRANSFER_HEAD.Post IS NULL )and TSPL_TRANSFER_HEAD.is_Auto_Created_Trans=0"


                End If
            End If
        Else

            '   qry = "Select CAST((case when Max(Status) ='Y' then 1 else 0 end)as BIT) as Status, MAX([Document Id]) as [Document Id], MAX([Document Date]) as [Document Date], SUM(Amount) as Amount, MAX(ItemType) as [Item Type], MAX([Referenced Document]) as [Referenced Document], MAX([Transfer No]) as [Transfer No], MAX(Description) as Description from (SELECT TSPL_ADJUSTMENT_HEADER.Posted as Status, TSPL_ADJUSTMENT_HEADER.Adjustment_No as [Document Id], TSPL_ADJUSTMENT_HEADER.Adjustment_Date as [Document Date], isnull(Item_Cost,0) as Amount, TSPL_ADJUSTMENT_HEADER.ItemType as ItemType, TSPL_ADJUSTMENT_HEADER.Reference_Document as [Referenced Document], " & _
            '   " (case when  TSPL_ADJUSTMENT_HEADER.Reference_Document ='Sale Invoice' then TSPL_SHIPMENT_MASTER.Transfer_No else  case when TSPL_ADJUSTMENT_HEADER.Reference_Document ='Load Out/Transfer' then  TSPL_ADJUSTMENT_HEADER.Document_No end end ) as [Transfer No], TSPL_ADJUSTMENT_HEADER.Description as Description,TSPL_ADJUSTMENT_HEADER.Created_By as 'Created By'   FROM TSPL_ADJUSTMENT_HEADER Left Outer Join TSPL_ADJUSTMENT_DETAIL on TSPL_ADJUSTMENT_DETAIL.Adjustment_No=TSPL_ADJUSTMENT_HEADER.Adjustment_No " & _
            '" LEFT OUTER JOIN TSPL_SALE_INVOICE_HEAD on TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No=TSPL_ADJUSTMENT_HEADER.Document_No  left outer join TSPL_SHIPMENT_MASTER on TSPL_SHIPMENT_MASTER .Shipment_No =TSPL_SALE_INVOICE_HEAD.Shipment_No  WHERE  convert(date,TSPL_ADJUSTMENT_HEADER.Adjustment_Date,103) >= convert(date,'" + dtpFromDate.Value + "',103) and convert(date,TSPL_ADJUSTMENT_HEADER.Adjustment_Date,103) <= convert(date,'" + dtpToDate.Value + "',103) "

            If clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), "Empty Transactions") = CompairStringResult.Equal Then
                Load_Authorisation(clsUserMgtCode.mbtnEmptyTrans)
                If dtAuthen.Rows.Count > 0 Then
                    If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then

                        qry = GetQueryByScreen("TSPL_ADJUSTMENT_HEADER", "Adjustment_No", "Adjustment_Date", "Created_By", "Created_Date", "Modify_By", "Modify_Date", "Posting_Date", " AND (TSPL_ADJUSTMENT_HEADER.ItemType='E' AND (ISNULL(Reference_Document, '')='' OR TSPL_SALE_INVOICE_HEAD.Shipment_Type='Sale'))   ")
                        '      qry = " select FirstTable.Document_No  ,FirstTable.Document_Date ,SecondTable.Version_No as Exe_Version ,FirstTable.Created_By as Created_By ,convert(varchar, FirstTable.Created_Date,103)   as Created_Date , FirstTable.Modify_By  ,convert (varchar,FirstTable.Modify_Date,103) as Modify_Date   from " & _
                        '" (Select 1 as SNO, TSPL_ADJUSTMENT_HEADER.Transfer_No as  Document_No ,TSPL_ADJUSTMENT_HEADER.Adjustment_Date as Document_Date,TSPL_ADJUSTMENT_HEADER.Created_By ,TSPL_ADJUSTMENT_HEADER.Created_Date,TSPL_ADJUSTMENT_HEADER.Modify_By,TSPL_ADJUSTMENT_HEADER.Modify_Date   from TSPL_ADJUSTMENT_HEADER " & _
                        '"	where convert(date,TSPL_ADJUSTMENT_HEADER.Adjustment_Date ,103) >= convert(date,'" + dtpFromDate.Value + "',103) and convert(date,TSPL_ADJUSTMENT_HEADER.Adjustment_Date ,103) <= convert(date,'" + dtpToDate.Value + "',103)   AND (TSPL_ADJUSTMENT_HEADER.ItemType='E' AND (ISNULL(Reference_Document, '')='' OR TSPL_SALE_INVOICE_HEAD.Shipment_Type='Sale'))  ) as FirstTable left outer join  " & _
                        '" (select 1 as  SNO, a.Version_No , a.Date as From_Date ,(SELECT min(b.date)  FROM TSPL_Exe_Deployment b WHERE b.Date > a.Date   ) as To_Date  from TSPL_Exe_Deployment a ) SecondTable on SecondTable.SNO = FirstTable.SNO " & _
                        '" where convert (datetime ,FirstTable.Document_Date) between convert(datetime,SecondTable.From_Date) and convert(datetime,SecondTable.To_Date) " & _
                        '" order by convert (datetime,FirstTable.Document_Date) ,FirstTable.Document_No "

                        'qry += " AND (TSPL_ADJUSTMENT_HEADER.ItemType='E' AND (ISNULL(Reference_Document, '')='' OR TSPL_SALE_INVOICE_HEAD.Shipment_Type='Sale')) "
                    End If
                End If
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), "Production Entry") = CompairStringResult.Equal Then
                Load_Authorisation(clsUserMgtCode.mbtnEmptyTrans)
                If dtAuthen.Rows.Count > 0 Then
                    If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then
                        qry = GetQueryByScreen("TSPL_ADJUSTMENT_HEADER", "Adjustment_No", "Adjustment_Date", "Created_By", "Created_Date", "Modify_By", "Modify_Date", "Posting_Date", " AND TSPL_ADJUSTMENT_HEADER.ItemType IN ('FT', 'FM')    ")

                        '      qry = " select FirstTable.Document_No  ,FirstTable.Document_Date ,SecondTable.Version_No as Exe_Version ,FirstTable.Created_By as Created_By ,convert(varchar, FirstTable.Created_Date,103)   as Created_Date , FirstTable.Modify_By  ,convert (varchar,FirstTable.Modify_Date,103) as Modify_Date   from " & _
                        '" (Select 1 as SNO, TSPL_ADJUSTMENT_HEADER.Transfer_No as  Document_No ,TSPL_ADJUSTMENT_HEADER.Adjustment_Date as Document_Date,TSPL_ADJUSTMENT_HEADER.Created_By ,TSPL_ADJUSTMENT_HEADER.Created_Date,TSPL_ADJUSTMENT_HEADER.Modify_By,TSPL_ADJUSTMENT_HEADER.Modify_Date   from TSPL_ADJUSTMENT_HEADER " & _
                        '"	where convert(date,TSPL_ADJUSTMENT_HEADER.Adjustment_Date ,103) >= convert(date,'" + dtpFromDate.Value + "',103) and convert(date,TSPL_ADJUSTMENT_HEADER.Adjustment_Date ,103) <= convert(date,'" + dtpToDate.Value + "',103) AND TSPL_ADJUSTMENT_HEADER.ItemType IN ('FT', 'FM')   ) as FirstTable left outer join  " & _
                        '" (select 1 as  SNO, a.Version_No , a.Date as From_Date ,(SELECT min(b.date)  FROM TSPL_Exe_Deployment b WHERE b.Date > a.Date   ) as To_Date  from TSPL_Exe_Deployment a ) SecondTable on SecondTable.SNO = FirstTable.SNO " & _
                        '" where convert (datetime ,FirstTable.Document_Date) between convert(datetime,SecondTable.From_Date) and convert(datetime,SecondTable.To_Date) " & _
                        '" order by convert (datetime,FirstTable.Document_Date) ,FirstTable.Document_No "

                        ' qry += " AND TSPL_ADJUSTMENT_HEADER.ItemType IN ('FT', 'FM')"
                    End If
                End If

            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), "Store Adjustment") = CompairStringResult.Equal AndAlso ChkMilkType.Checked = True Then
                Load_Authorisation(clsUserMgtCode.mbtnStoreAdjustment)
                If dtAuthen.Rows.Count > 0 Then
                    If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then
                        qry = GetQueryByScreen("TSPL_ADJUSTMENT_HEADER", "Adjustment_No", "Adjustment_Date", "Created_By", "Created_Date", "Modify_By", "Modify_Date", "Posting_Date", " AND TSPL_ADJUSTMENT_HEADER.IsMilkType='1'       ")

                        '      qry = " select FirstTable.Document_No  ,FirstTable.Document_Date ,SecondTable.Version_No as Exe_Version ,FirstTable.Created_By as Created_By ,convert(varchar, FirstTable.Created_Date,103)   as Created_Date , FirstTable.Modify_By  ,convert (varchar,FirstTable.Modify_Date,103) as Modify_Date   from " & _
                        '" (Select 1 as SNO, TSPL_ADJUSTMENT_HEADER.Transfer_No as  Document_No ,TSPL_ADJUSTMENT_HEADER.Adjustment_Date as Document_Date,TSPL_ADJUSTMENT_HEADER.Created_By ,TSPL_ADJUSTMENT_HEADER.Created_Date,TSPL_ADJUSTMENT_HEADER.Modify_By,TSPL_ADJUSTMENT_HEADER.Modify_Date   from TSPL_ADJUSTMENT_HEADER " & _
                        '"	where convert(date,TSPL_ADJUSTMENT_HEADER.Adjustment_Date ,103) >= convert(date,'" + dtpFromDate.Value + "',103) and convert(date,TSPL_ADJUSTMENT_HEADER.Adjustment_Date ,103) <= convert(date,'" + dtpToDate.Value + "',103) AND TSPL_ADJUSTMENT_HEADER.IsMilkType='1'   ) as FirstTable left outer join  " & _
                        '" (select 1 as  SNO, a.Version_No , a.Date as From_Date ,(SELECT min(b.date)  FROM TSPL_Exe_Deployment b WHERE b.Date > a.Date   ) as To_Date  from TSPL_Exe_Deployment a ) SecondTable on SecondTable.SNO = FirstTable.SNO " & _
                        '" where convert (datetime ,FirstTable.Document_Date) between convert(datetime,SecondTable.From_Date) and convert(datetime,SecondTable.To_Date) " & _
                        '" order by convert (datetime,FirstTable.Document_Date) ,FirstTable.Document_No "

                        'qry += " AND TSPL_ADJUSTMENT_HEADER.IsMilkType='1'"
                    Else
                        qry = GetQueryByScreen("TSPL_ADJUSTMENT_HEADER", "Adjustment_No", "Adjustment_Date", "Created_By", "Created_Date", "Modify_By", "Modify_Date", "Posting_Date", " AND TSPL_ADJUSTMENT_HEADER.IsMilkType='0'     ")

                        '      qry = " select FirstTable.Document_No  ,FirstTable.Document_Date ,SecondTable.Version_No as Exe_Version ,FirstTable.Created_By as Created_By ,convert(varchar, FirstTable.Created_Date,103)   as Created_Date , FirstTable.Modify_By  ,convert (varchar,FirstTable.Modify_Date,103) as Modify_Date   from " & _
                        '" (Select 1 as SNO, TSPL_ADJUSTMENT_HEADER.Transfer_No as  Document_No ,TSPL_ADJUSTMENT_HEADER.Adjustment_Date as Document_Date,TSPL_ADJUSTMENT_HEADER.Created_By ,TSPL_ADJUSTMENT_HEADER.Created_Date,TSPL_ADJUSTMENT_HEADER.Modify_By,TSPL_ADJUSTMENT_HEADER.Modify_Date   from TSPL_ADJUSTMENT_HEADER " & _
                        '"	where convert(date,TSPL_ADJUSTMENT_HEADER.Adjustment_Date ,103) >= convert(date,'" + dtpFromDate.Value + "',103) and convert(date,TSPL_ADJUSTMENT_HEADER.Adjustment_Date ,103) <= convert(date,'" + dtpToDate.Value + "',103) AND TSPL_ADJUSTMENT_HEADER.IsMilkType='0'   ) as FirstTable left outer join  " & _
                        '" (select 1 as  SNO, a.Version_No , a.Date as From_Date ,(SELECT min(b.date)  FROM TSPL_Exe_Deployment b WHERE b.Date > a.Date   ) as To_Date  from TSPL_Exe_Deployment a ) SecondTable on SecondTable.SNO = FirstTable.SNO " & _
                        '" where convert (datetime ,FirstTable.Document_Date) between convert(datetime,SecondTable.From_Date) and convert(datetime,SecondTable.To_Date) " & _
                        '" order by convert (datetime,FirstTable.Document_Date) ,FirstTable.Document_No "
                        'qry += " AND TSPL_ADJUSTMENT_HEADER.IsMilkType='0'"

                    End If
                End If
            End If



            'qry += " ) AAA Group By [Document Id] "
            'qry += " order by [Document Date], [Document Id] "
            ''-------------------------------------------Code  Ends Here-----------------------------------------------
        End If
        '    End If
        'End If

        If clsCommon.CompairString(clsCommon.myCstr(qry), Nothing) <> CompairStringResult.Equal Then

            dt = clsDBFuncationality.GetDataTable(qry)
            gv1.DataSource = Nothing
            gv1.DataSource = dt
            gv1.MasterTemplate.SummaryRowsBottom.Clear()
            'gv1Format()
            SetGridFormationOFgv()

        End If
        '-------------------------------------Code Ends Here--------------------------------------------
    End Sub

    ''-------------------------------------------------------------------
    '' Function For Filling--------Module(Sales & Distribution)----------
    ''-------------------------------------------------------------------

    Sub FillSalesNDistribution()

        If clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), "Sale Order") = CompairStringResult.Equal Then
            Load_Authorisation(clsUserMgtCode.frmSalesOrderProductSale)
            If dtAuthen.Rows.Count > 0 Then
                If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then
                    gv1.DataSource = Nothing

                    qry = GetQueryByScreen("TSPL_SALES_ORDER_HEAD", "Order_No", "Order_Date", "Created_By", "Created_Date", "Modify_By", "Modify_Date", "Post_Date", "  ", "Location")

                    '  qry = " select FirstTable.Document_No  ,FirstTable.Document_Date ,SecondTable.Version_No as Exe_Version ,FirstTable.Created_By as Created_By ,convert(varchar, FirstTable.Created_Date,103)   as Created_Date , FirstTable.Modify_By  ,convert (varchar,FirstTable.Modify_Date,103) as Modify_Date   from " & _
                    '" (Select 1 as SNO, TSPL_SALES_ORDER_HEAD.Order_No as  Document_No ,TSPL_SALES_ORDER_HEAD.Order_Date as Document_Date,TSPL_SALES_ORDER_HEAD.Created_By ,TSPL_SALES_ORDER_HEAD.Created_Date,TSPL_SALES_ORDER_HEAD.Modify_By,TSPL_SALES_ORDER_HEAD.Modify_Date   from TSPL_SALES_ORDER_HEAD " & _
                    '"	where convert(date,TSPL_SALES_ORDER_HEAD.Order_Date ,103) >= convert(date,'" + dtpFromDate.Value + "',103) and convert(date,TSPL_SALES_ORDER_HEAD.Order_Date ,103) <= convert(date,'" + dtpToDate.Value + "',103)    ) as FirstTable left outer join  " & _
                    '" (select 1 as  SNO, a.Version_No , a.Date as From_Date ,(SELECT min(b.date)  FROM TSPL_Exe_Deployment b WHERE b.Date > a.Date   ) as To_Date  from TSPL_Exe_Deployment a ) SecondTable on SecondTable.SNO = FirstTable.SNO " & _
                    '" where convert (datetime ,FirstTable.Document_Date) between convert(datetime,SecondTable.From_Date) and convert(datetime,SecondTable.To_Date) " & _
                    '" order by convert (datetime,FirstTable.Document_Date) ,FirstTable.Document_No "

                    'qry = "SELECT CAST((case when TSPL_SALES_ORDER_HEAD.Is_Post =1 then 1 else 0 end)as BIT) as Status, TSPL_SALES_ORDER_HEAD.Order_No as [Document Id], TSPL_SALES_ORDER_HEAD.Order_Date as [Document Date],isnull(TSPL_SALES_ORDER_HEAD.Total_Order_Amt,0) as [Amount], TSPL_SALES_ORDER_HEAD.Cust_Code as [Customer Code], TSPL_SALES_ORDER_HEAD.Cust_Name as [Customer Name], TSPL_SALES_ORDER_HEAD.Description as [Description], CAST((case when TSPL_SALES_ORDER_HEAD.On_Hold  =1 then 1 else 0 end)as BIT) as Hold,TSPL_SALES_ORDER_HEAD.Created_By as 'Created By' FROM TSPL_SALES_ORDER_HEAD WHERE  convert(date,TSPL_SALES_ORDER_HEAD.Order_Date,103) >= convert(date,'" + dtpFromDate.Value + "',103) and convert(date,TSPL_SALES_ORDER_HEAD.Order_Date,103) <= convert(date,'" + dtpToDate.Value + "',103) "
                    'qry += "ORDER BY TSPL_SALES_ORDER_HEAD.Order_Date , TSPL_SALES_ORDER_HEAD.Order_No "
                End If
            End If
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), "Shipment/Invoice") = CompairStringResult.Equal Then
            gv1.DataSource = Nothing
            Dim strTransType As String = ""
            If clsCommon.CompairString(clsCommon.myCstr(cboModule.SelectedValue), "Product Sale") = CompairStringResult.Equal Then
                Load_Authorisation(clsUserMgtCode.frmShipmentProductSale)
                strTransType = "PS"
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboModule.SelectedValue), "Fresh Sale") = CompairStringResult.Equal Then
                Load_Authorisation(clsUserMgtCode.FrmDispatchFreshSale)
                strTransType = "FS"
            End If
            If dtAuthen.Rows.Count > 0 Then
                If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then
                    qry = GetQueryByScreen("TSPL_SD_SHIPMENT_HEAD", "Document_Code", "Document_Date", "Created_By", "Created_Date", "Modify_By", "Modify_Date", "Posting_Date", " and TSPL_SD_SHIPMENT_HEAD.Trans_Type='" & strTransType & "'    ")

                    '  qry = " select FirstTable.Document_No  ,FirstTable.Document_Date ,SecondTable.Version_No as Exe_Version ,FirstTable.Created_By as Created_By ,convert(varchar, FirstTable.Created_Date,103)   as Created_Date , FirstTable.Modify_By  ,convert (varchar,FirstTable.Modify_Date,103) as Modify_Date   from " & _
                    '" (Select 1 as SNO, TSPL_SD_SHIPMENT_HEAD.Document_Code as  Document_No ,TSPL_SD_SHIPMENT_HEAD.Document_Date as Document_Date,TSPL_SD_SHIPMENT_HEAD.Created_By ,TSPL_SD_SHIPMENT_HEAD.Created_Date,TSPL_SD_SHIPMENT_HEAD.Modify_By,TSPL_SD_SHIPMENT_HEAD.Modify_Date   from TSPL_SD_SHIPMENT_HEAD " & _
                    '"	where convert(date,TSPL_SD_SHIPMENT_HEAD.Document_Date ,103) >= convert(date,'" + dtpFromDate.Value + "',103) and convert(date,TSPL_SD_SHIPMENT_HEAD.Document_Date ,103) <= convert(date,'" + dtpToDate.Value + "',103)   and TSPL_SD_SHIPMENT_HEAD.Trans_Type='" & strTransType & "'   ) as FirstTable left outer join  " & _
                    '" (select 1 as  SNO, a.Version_No , a.Date as From_Date ,(SELECT min(b.date)  FROM TSPL_Exe_Deployment b WHERE b.Date > a.Date   ) as To_Date  from TSPL_Exe_Deployment a ) SecondTable on SecondTable.SNO = FirstTable.SNO " & _
                    '" where convert (datetime ,FirstTable.Document_Date) between convert(datetime,SecondTable.From_Date) and convert(datetime,SecondTable.To_Date) " & _
                    '" order by convert (datetime,FirstTable.Document_Date) ,FirstTable.Document_No "


                    'qry = "SELECT  CAST(TSPL_SD_SHIPMENT_HEAD.Status as BIT) as Status, TSPL_SD_SHIPMENT_HEAD.Document_Code as [Document Id], TSPL_SD_SHIPMENT_HEAD.Document_Date as [Document Date], TSPL_SD_SALE_INVOICE_HEAD.Document_Code as [Sale Invoice], isnull(TSPL_SD_SHIPMENT_HEAD.Total_Amt,0) as [Amount], TSPL_SD_SHIPMENT_HEAD.Bill_To_Location As Location , TSPL_LOCATION_MASTER.Location_Desc as [Location Desc], TSPL_SD_SHIPMENT_HEAD.Customer_Code as [Customer Code], TSPL_CUSTOMER_MASTER.Customer_Name as [Customer Name], TSPL_SD_SHIPMENT_HEAD.Description as [Description], CAST(TSPL_SD_SHIPMENT_HEAD.On_Hold as BIT) as Hold,TSPL_SD_SHIPMENT_HEAD.Created_By as 'Created By' FROM TSPL_SD_SHIPMENT_HEAD" & _
                    '    " Left Outer Join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Against_Shipment_No=TSPL_SD_SHIPMENT_HEAD.Document_Code" & _
                    '    " Left Outer Join TSPL_LOCATION_MASTER on TSPL_SD_SHIPMENT_HEAD.Bill_To_Location=TSPL_LOCATION_MASTER.Location_Code" & _
                    '    " LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SHIPMENT_HEAD.Customer_Code WHERE convert(date,TSPL_SD_SHIPMENT_HEAD.Document_Date,103) >= convert(date,'" + dtpFromDate.Value + "',103) and convert(date,TSPL_SD_SHIPMENT_HEAD.Document_Date,103) <= convert(date,'" + dtpToDate.Value + "',103)"

                    'qry += " and TSPL_SD_SHIPMENT_HEAD.Trans_Type='" & strTransType & "'"
                    'qry += "ORDER BY TSPL_SD_SHIPMENT_HEAD.Document_Date , TSPL_SD_SHIPMENT_HEAD.Document_Code"

                End If
            End If
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), "Sale Invoice") = CompairStringResult.Equal Then
            Load_Authorisation(clsUserMgtCode.frmSaleInvoiceProductSale)
            If dtAuthen.Rows.Count > 0 Then
                If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then
                    gv1.DataSource = Nothing

                    qry = GetQueryByScreen("TSPL_SD_SALE_INVOICE_HEAD", "Document_Code", "Document_Date", "Created_By", "Created_Date", "Modify_By", "Modify_Date", "Posting_Date", " ")

                    '  qry = " select FirstTable.Document_No  ,FirstTable.Document_Date ,SecondTable.Version_No as Exe_Version ,FirstTable.Created_By as Created_By ,convert(varchar, FirstTable.Created_Date,103)   as Created_Date , FirstTable.Modify_By  ,convert (varchar,FirstTable.Modify_Date,103) as Modify_Date   from " & _
                    '" (Select 1 as SNO, TSPL_SD_SALE_INVOICE_HEAD.Document_Code as  Document_No ,TSPL_SD_SALE_INVOICE_HEAD.Document_Date as Document_Date,TSPL_SD_SALE_INVOICE_HEAD.Created_By ,TSPL_SD_SALE_INVOICE_HEAD.Created_Date,TSPL_SD_SALE_INVOICE_HEAD.Modify_By,TSPL_SD_SALE_INVOICE_HEAD.Modify_Date   from TSPL_SD_SALE_INVOICE_HEAD " & _
                    '"	where convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date ,103) >= convert(date,'" + dtpFromDate.Value + "',103) and convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date ,103) <= convert(date,'" + dtpToDate.Value + "',103)    ) as FirstTable left outer join  " & _
                    '" (select 1 as  SNO, a.Version_No , a.Date as From_Date ,(SELECT min(b.date)  FROM TSPL_Exe_Deployment b WHERE b.Date > a.Date   ) as To_Date  from TSPL_Exe_Deployment a ) SecondTable on SecondTable.SNO = FirstTable.SNO " & _
                    '" where convert (datetime ,FirstTable.Document_Date) between convert(datetime,SecondTable.From_Date) and convert(datetime,SecondTable.To_Date) " & _
                    '" order by convert (datetime,FirstTable.Document_Date) ,FirstTable.Document_No "


                    'qry = "SELECT CAST(TSPL_SD_SALE_INVOICE_HEAD.Status as BIT) as Status, TSPL_SD_SALE_INVOICE_HEAD.Document_Code as [Document Id], TSPL_SD_SALE_INVOICE_HEAD.Document_Date as [Document Date],isnull(TSPL_SD_SALE_INVOICE_HEAD.Total_Amt,0) as [Amount], TSPL_SD_SALE_INVOICE_HEAD.Against_Shipment_No as [Shipment No], TSPL_SD_SALE_INVOICE_HEAD.Customer_Code as [Customer Code], TSPL_CUSTOMER_MASTER.Customer_Name as [Customer Name], TSPL_SD_SALE_INVOICE_HEAD.Description as [Description], CAST(TSPL_SD_SALE_INVOICE_HEAD.On_Hold as BIT) as Hold,TSPL_SD_SALE_INVOICE_HEAD.Created_By as 'Created By'" & _
                    '" FROM TSPL_SD_SALE_INVOICE_HEAD" & _
                    '" LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SALE_INVOICE_HEAD.Customer_Code" & _
                    '" WHERE  convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) >= convert(date,'" + dtpFromDate.Value + "',103) and convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) <= convert(date,'" + dtpToDate.Value + "',103)"



                    'qry += "ORDER BY TSPL_SD_SALE_INVOICE_HEAD.Document_Date, TSPL_SD_SALE_INVOICE_HEAD.Document_Code "
                End If
            End If
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), "Sale Return") = CompairStringResult.Equal Then
            Load_Authorisation(clsUserMgtCode.frmSaleReturnProductSale)
            If dtAuthen.Rows.Count > 0 Then
                If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then
                    gv1.DataSource = Nothing

                    qry = GetQueryByScreen("TSPL_SD_SALE_RETURN_HEAD", "Document_Code", "Document_Date", "Created_By", "Created_Date", "Modify_By", "Modify_Date", "Posting_Date", " ")

                    '  qry = " select FirstTable.Document_No  ,FirstTable.Document_Date ,SecondTable.Version_No as Exe_Version ,FirstTable.Created_By as Created_By ,convert(varchar, FirstTable.Created_Date,103)   as Created_Date , FirstTable.Modify_By  ,convert (varchar,FirstTable.Modify_Date,103) as Modify_Date   from " & _
                    '" (Select 1 as SNO, TSPL_SD_SALE_RETURN_HEAD.Document_Code as  Document_No ,TSPL_SD_SALE_RETURN_HEAD.Document_Date as Document_Date,TSPL_SD_SALE_RETURN_HEAD.Created_By ,TSPL_SD_SALE_RETURN_HEAD.Created_Date,TSPL_SD_SALE_RETURN_HEAD.Modify_By,TSPL_SD_SALE_RETURN_HEAD.Modify_Date   from TSPL_SD_SALE_RETURN_HEAD " & _
                    '"	where convert(date,TSPL_SD_SALE_RETURN_HEAD.Document_Date ,103) >= convert(date,'" + dtpFromDate.Value + "',103) and convert(date,TSPL_SD_SALE_RETURN_HEAD.Document_Date ,103) <= convert(date,'" + dtpToDate.Value + "',103)    ) as FirstTable left outer join  " & _
                    '" (select 1 as  SNO, a.Version_No , a.Date as From_Date ,(SELECT min(b.date)  FROM TSPL_Exe_Deployment b WHERE b.Date > a.Date   ) as To_Date  from TSPL_Exe_Deployment a ) SecondTable on SecondTable.SNO = FirstTable.SNO " & _
                    '" where convert (datetime ,FirstTable.Document_Date) between convert(datetime,SecondTable.From_Date) and convert(datetime,SecondTable.To_Date) " & _
                    '" order by convert (datetime,FirstTable.Document_Date) ,FirstTable.Document_No "

                    'qry = "SELECT CAST(TSPL_SD_SALE_RETURN_HEAD.Status as BIT) as Status, TSPL_SD_SALE_RETURN_HEAD.Document_Code as [Document Id], TSPL_SD_SALE_RETURN_HEAD.Document_Date as [Document Date],isnull(TSPL_SD_SALE_RETURN_HEAD.Total_Amt,0) as [Amount], TSPL_SD_SALE_RETURN_HEAD.Against_Invoice_No As Invoice_No, TSPL_SD_SALE_RETURN_HEAD.Customer_Code as [Customer Code], TSPL_CUSTOMER_MASTER.Customer_Name as [Customer Name],TSPL_SD_SALE_RETURN_HEAD.Description as [Description],TSPL_SD_SALE_RETURN_HEAD.Created_By as 'Created By' FROM TSPL_SD_SALE_RETURN_HEAD LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SALE_RETURN_HEAD.Customer_Code WHERE  convert(date,TSPL_SD_SALE_RETURN_HEAD.Document_Date,103) >= convert(date,'" + dtpFromDate.Value + "',103) and convert(date,TSPL_SD_SALE_RETURN_HEAD.Document_Date ,103) <= convert(date,'" + dtpToDate.Value + "',103)"

                    'qry += "ORDER BY TSPL_SALE_RETURN_HEAD.Sale_Return_Date , TSPL_SALE_RETURN_HEAD.Sale_Return_No "
                End If
            End If
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), "Sale Return (Inter Company)") = CompairStringResult.Equal Then
            Load_Authorisation("Sale Return (Inter Company)")
            If dtAuthen.Rows.Count > 0 Then
                If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then
                    gv1.DataSource = Nothing

                    qry = GetQueryByScreen("TSPL_SALE_RETURN_INTER_HEAD", "Document_No", "Document_Date", "Created_By", "Created_Date", "Modify_By", "Modify_Date", "Post_Date", " ")

                    '  qry = " select FirstTable.Document_No  ,FirstTable.Document_Date ,SecondTable.Version_No as Exe_Version ,FirstTable.Created_By as Created_By ,convert(varchar, FirstTable.Created_Date,103)   as Created_Date , FirstTable.Modify_By  ,convert (varchar,FirstTable.Modify_Date,103) as Modify_Date   from " & _
                    '" (Select 1 as SNO, TSPL_SALE_RETURN_INTER_HEAD.Document_Code as  Document_No ,TSPL_SALE_RETURN_INTER_HEAD.Document_Date as Document_Date,TSPL_SALE_RETURN_INTER_HEAD.Created_By ,TSPL_SALE_RETURN_INTER_HEAD.Created_Date,TSPL_SALE_RETURN_INTER_HEAD.Modify_By,TSPL_SALE_RETURN_INTER_HEAD.Modify_Date   from TSPL_SALE_RETURN_INTER_HEAD " & _
                    '"	where convert(date,TSPL_SALE_RETURN_INTER_HEAD.Document_Date ,103) >= convert(date,'" + dtpFromDate.Value + "',103) and convert(date,TSPL_SALE_RETURN_INTER_HEAD.Document_Date ,103) <= convert(date,'" + dtpToDate.Value + "',103)    ) as FirstTable left outer join  " & _
                    '" (select 1 as  SNO, a.Version_No , a.Date as From_Date ,(SELECT min(b.date)  FROM TSPL_Exe_Deployment b WHERE b.Date > a.Date   ) as To_Date  from TSPL_Exe_Deployment a ) SecondTable on SecondTable.SNO = FirstTable.SNO " & _
                    '" where convert (datetime ,FirstTable.Document_Date) between convert(datetime,SecondTable.From_Date) and convert(datetime,SecondTable.To_Date) " & _
                    '" order by convert (datetime,FirstTable.Document_Date) ,FirstTable.Document_No "

                    'qry = "SELECT CAST((case when TSPL_SALE_RETURN_INTER_HEAD.Is_Post =1 then 1 else 0 end)as BIT) as Status, TSPL_SALE_RETURN_INTER_HEAD.Document_No  as [Document Id], TSPL_SALE_RETURN_INTER_HEAD.Document_Date as [Document Date],isnull(TSPL_SALE_RETURN_INTER_HEAD.Total_Order_Amt,0) as [Amount], TSPL_SALE_RETURN_INTER_HEAD.Cust_Code as [Customer Code], TSPL_CUSTOMER_MASTER.Customer_Name as [Customer Name],TSPL_SALE_RETURN_INTER_HEAD.Description as [Description],TSPL_SALE_RETURN_INTER_HEAD.Created_By as 'Created By' FROM TSPL_SALE_RETURN_INTER_HEAD Left Outer Join TSPL_CUSTOMER_MASTER ON TSPL_SALE_RETURN_INTER_HEAD.Cust_Code=TSPL_CUSTOMER_MASTER.Cust_Code Left Outer  Join TSPL_LOCATION_MASTER ON TSPL_SALE_RETURN_INTER_HEAD.Location=TSPL_LOCATION_MASTER.Location_Code WHERE  convert(date,TSPL_SALE_RETURN_INTER_HEAD.Document_Date ,103) >= convert(date,'" + dtpFromDate.Value + "',103) and convert(date,TSPL_SALE_RETURN_INTER_HEAD.Document_Date  ,103) <= convert(date,'" + dtpToDate.Value + "',103) "

                    'qry += " ORDER BY TSPL_SALE_RETURN_INTER_HEAD.Document_Date , TSPL_SALE_RETURN_INTER_HEAD.Document_No "
                End If
            End If
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), "Quick Settlement") = CompairStringResult.Equal Then
            Load_Authorisation("Quick Settlement")
            If dtAuthen.Rows.Count > 0 Then
                If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then
                    gv1.DataSource = Nothing

                    qry = GetQueryByScreen("tspl_QuickSettleMent", "Quick_SettleMent_Id", "Quick_Settlement_Date", "Created_By", "Created_Date", "Modify_By", "Modify_Date", "Modify_Date", " ")

                    '  qry = " select FirstTable.Document_No  ,FirstTable.Document_Date ,SecondTable.Version_No as Exe_Version ,FirstTable.Created_By as Created_By ,convert(varchar, FirstTable.Created_Date,103)   as Created_Date , FirstTable.Modify_By  ,convert (varchar,FirstTable.Modify_Date,103) as Modify_Date   from " & _
                    '" (Select 1 as SNO, tspl_QuickSettleMent.Quick_SettleMent_Id as  Document_No ,tspl_QuickSettleMent.Quick_Settlement_Date as Document_Date,tspl_QuickSettleMent.Created_By ,tspl_QuickSettleMent.Created_Date,tspl_QuickSettleMent.Modify_By,tspl_QuickSettleMent.Modify_Date   from tspl_QuickSettleMent " & _
                    '"	where convert(date,tspl_QuickSettleMent.Quick_Settlement_Date ,103) >= convert(date,'" + dtpFromDate.Value + "',103) and convert(date,tspl_QuickSettleMent.Quick_Settlement_Date ,103) <= convert(date,'" + dtpToDate.Value + "',103)    ) as FirstTable left outer join  " & _
                    '" (select 1 as  SNO, a.Version_No , a.Date as From_Date ,(SELECT min(b.date)  FROM TSPL_Exe_Deployment b WHERE b.Date > a.Date   ) as To_Date  from TSPL_Exe_Deployment a ) SecondTable on SecondTable.SNO = FirstTable.SNO " & _
                    '" where convert (datetime ,FirstTable.Document_Date) between convert(datetime,SecondTable.From_Date) and convert(datetime,SecondTable.To_Date) " & _
                    '" order by convert (datetime,FirstTable.Document_Date) ,FirstTable.Document_No "

                    'qry = "SELECT Distinct CAST((case when tspl_QuickSettleMent.Post ='Y' then 1 else 0 end)as BIT) as Status, tspl_QuickSettleMent.Quick_SettleMent_Id as [Document Id], tspl_QuickSettleMent.Quick_Settlement_Date as [Document Date],isnull(tspl_QuickSettleMent.Transfer_Amount,0) as [Amount],tspl_QuickSettleMent.Transfer_Number, tspl_QuickSettleMent.Comments as [Description],tspl_QuickSettleMent.Created_By as 'Created By' FROM tspl_QuickSettleMent WHERE  convert(date,tspl_QuickSettleMent.Quick_Settlement_Date,103) >= convert(date,'" + dtpFromDate.Value + "',103) and convert(date,tspl_QuickSettleMent.Quick_Settlement_Date ,103) <= convert(date,'" + dtpToDate.Value + "',103) "
                    'qry += "ORDER BY tspl_QuickSettleMent.Quick_Settlement_Date , tspl_QuickSettleMent.Quick_SettleMent_Id "

                End If
            End If
        End If
        If clsCommon.CompairString(clsCommon.myCstr(qry), Nothing) <> CompairStringResult.Equal Then

            dt = clsDBFuncationality.GetDataTable(qry)
            gv1.DataSource = dt
            gv1.MasterTemplate.SummaryRowsBottom.Clear()
            ' gv1Format()
            SetGridFormationOFgv()

        End If
        '-------------------------------------Code Ends Here--------------------------------------------
    End Sub

    ''-------------------------------------------------------------------
    '' Function For Filling --------Module(General Ledger)---------------
    ''-------------------------------------------------------------------

    Sub FillGeneralLedger()

        If clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), "Journal Entry") = CompairStringResult.Equal Then
            Load_Authorisation(clsUserMgtCode.journalEntry)
            If dtAuthen.Rows.Count > 0 Then
                If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then
                    gv1.DataSource = Nothing

                    qry = GetQueryByScreen("TSPL_JOURNAL_MASTER", "Voucher_No", "Voucher_Date", "Created_By", "Created_Date", "Modify_By", "Modify_Date", "Posting_Date", " ")

                    '  qry = " select FirstTable.Document_No  ,FirstTable.Document_Date ,SecondTable.Version_No as Exe_Version ,FirstTable.Created_By as Created_By ,convert(varchar, FirstTable.Created_Date,103)   as Created_Date , FirstTable.Modify_By  ,convert (varchar,FirstTable.Modify_Date,103) as Modify_Date   from " & _
                    '" (Select 1 as SNO, TSPL_JOURNAL_MASTER.Voucher_No as  Document_No ,TSPL_JOURNAL_MASTER.Voucher_Date as Document_Date,TSPL_JOURNAL_MASTER.Created_By ,TSPL_JOURNAL_MASTER.Created_Date,TSPL_JOURNAL_MASTER.Modify_By,TSPL_JOURNAL_MASTER.Modify_Date   from TSPL_JOURNAL_MASTER " & _
                    '"	where convert(date,TSPL_JOURNAL_MASTER.Voucher_Date ,103) >= convert(date,'" + dtpFromDate.Value + "',103) and convert(date,TSPL_JOURNAL_MASTER.Voucher_Date ,103) <= convert(date,'" + dtpToDate.Value + "',103)    ) as FirstTable left outer join  " & _
                    '" (select 1 as  SNO, a.Version_No , a.Date as From_Date ,(SELECT min(b.date)  FROM TSPL_Exe_Deployment b WHERE b.Date > a.Date   ) as To_Date  from TSPL_Exe_Deployment a ) SecondTable on SecondTable.SNO = FirstTable.SNO " & _
                    '" where convert (datetime ,FirstTable.Document_Date) between convert(datetime,SecondTable.From_Date) and convert(datetime,SecondTable.To_Date) " & _
                    '" order by convert (datetime,FirstTable.Document_Date) ,FirstTable.Document_No "


                    'qry = "SELECT CAST((case when TSPL_JOURNAL_MASTER.Authorized ='A' then 1 else 0 end)as BIT) as Status, TSPL_JOURNAL_MASTER.Voucher_No as [Document Id], TSPL_JOURNAL_MASTER.Voucher_Date as [Document Date],isnull(TSPL_JOURNAL_MASTER.Total_Debit_Amt,0) as [Amount], TSPL_JOURNAL_MASTER.Posting_Date as [Posting Date],TSPL_JOURNAL_MASTER.Source_Doc_No as [Source Document Code], TSPL_JOURNAL_MASTER.Source_Code as [Source Code],TSPL_JOURNAL_MASTER.CustVend_Code as [Customer/Vendor Code],TSPL_JOURNAL_MASTER.CustVend_Name as [Customer/Vendor Name], TSPL_JOURNAL_MASTER.Voucher_Desc as [Description],TSPL_JOURNAL_MASTER.Created_By as 'Created By' FROM TSPL_JOURNAL_MASTER WHERE  convert(date,TSPL_JOURNAL_MASTER.Voucher_Date,103) >= convert(date,'" + dtpFromDate.Value + "',103) and convert(date,TSPL_JOURNAL_MASTER.Voucher_Date,103) <= convert(date,'" + dtpToDate.Value + "',103) "


                    'qry += " ORDER BY TSPL_JOURNAL_MASTER.Voucher_Date , TSPL_JOURNAL_MASTER.Voucher_No "
                End If
            End If
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), "VCGL Entry") = CompairStringResult.Equal Then
            Load_Authorisation(clsUserMgtCode.mbtnVCGLEntry)
            If dtAuthen.Rows.Count > 0 Then
                If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then
                    gv1.DataSource = Nothing

                    qry = GetQueryByScreen("TSPL_VCGL_Head", "Document_No", "Created_Date", "Created_By", "Created_Date", "Modify_By", "Modify_Date", "Modify_Date", " ") ' Time working for modify date and create date 

                    '  qry = " select FirstTable.Document_No  ,FirstTable.Document_Date ,SecondTable.Version_No as Exe_Version ,FirstTable.Created_By as Created_By ,convert(varchar, FirstTable.Created_Date,103)   as Created_Date , FirstTable.Modify_By  ,convert (varchar,FirstTable.Modify_Date,103) as Modify_Date   from " & _
                    '" (Select 1 as SNO, TSPL_VCGL_Head.Document_No as  Document_No ,TSPL_VCGL_Head.Document_Date as Document_Date,TSPL_VCGL_Head.Created_By ,TSPL_VCGL_Head.Created_Date,TSPL_VCGL_Head.Modify_By,TSPL_VCGL_Head.Modify_Date   from TSPL_VCGL_Head " & _
                    '"	where convert(date,TSPL_VCGL_Head.Document_Date ,103) >= convert(date,'" + dtpFromDate.Value + "',103) and convert(date,TSPL_VCGL_Head.Document_Date ,103) <= convert(date,'" + dtpToDate.Value + "',103)    ) as FirstTable left outer join  " & _
                    '" (select 1 as  SNO, a.Version_No , a.Date as From_Date ,(SELECT min(b.date)  FROM TSPL_Exe_Deployment b WHERE b.Date > a.Date   ) as To_Date  from TSPL_Exe_Deployment a ) SecondTable on SecondTable.SNO = FirstTable.SNO " & _
                    '" where convert (datetime ,FirstTable.Document_Date) between convert(datetime,SecondTable.From_Date) and convert(datetime,SecondTable.To_Date) " & _
                    '" order by convert (datetime,FirstTable.Document_Date) ,FirstTable.Document_No "

                    'qry = "SELECT CAST((TSPL_VCGL_Head.Status)as BIT) as Status, TSPL_VCGL_Head.Document_No as [Document Id], CONVERT(Varchar,TSPL_VCGL_Head.Document_Date, 103) as [Document Date], CONVERT(Varchar,TSPL_VCGL_Head.Posting_Date, 103) as [Posting Date],TSPL_VCGL_Head.VC_Code as [VC Code], TSPL_VCGL_Head.VC_Name as [VC Name],isnull(TSPL_VCGL_Head.Tot_Dr_Amount,0) as [Debit Amount],isnull(TSPL_VCGL_Head.Tot_Cr_Amount,0) as [Credit Amount], isnull(TSPL_VCGL_Head.Amount,0) as [Balance], TSPL_VCGL_Head.Remarks as [Description],TSPL_VCGL_Head.Created_By as 'Created By' FROM TSPL_VCGL_Head WHERE  convert(date,TSPL_VCGL_Head.Document_Date ,103) >= convert(date,'" + dtpFromDate.Value + "',103) and convert(date,TSPL_VCGL_Head.Document_Date,103) <= convert(date,'" + dtpToDate.Value + "',103) "

                    'qry += " ORDER BY TSPL_VCGL_Head.Document_Date  , TSPL_VCGL_Head.Document_No "
                End If
            End If
        End If
        If clsCommon.CompairString(clsCommon.myCstr(qry), Nothing) <> CompairStringResult.Equal Then

            dt = clsDBFuncationality.GetDataTable(qry)
            gv1.DataSource = dt
            gv1.MasterTemplate.SummaryRowsBottom.Clear()
            ' gv1Format()
            SetGridFormationOFgv()


        End If
        '-------------------------------------Code Ends Here--------------------------------------------
    End Sub

    ''-------------------------------------------------------------------
    '' Function For Filling --------Module(Tax Deducted At Source)-------
    ''-------------------------------------------------------------------

    Sub FillTaxDdctdAtSource()

        If clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), "Remittance Entry") = CompairStringResult.Equal Then
            Load_Authorisation(clsUserMgtCode.remittanceentry)
            If dtAuthen.Rows.Count > 0 Then
                If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then
                    gv1.DataSource = Nothing

                    qry = GetQueryByScreen("TSPL_REMITTANCE_ENTRY", "Remittance_Code", "Remittance_Date", "Created_By", "Created_Date", "Modify_By", "Modify_Date", "Modify_Date", " ")


                    '  qry = " select FirstTable.Document_No  ,FirstTable.Document_Date ,SecondTable.Version_No as Exe_Version ,FirstTable.Created_By as Created_By ,convert(varchar, FirstTable.Created_Date,103)   as Created_Date , FirstTable.Modify_By  ,convert (varchar,FirstTable.Modify_Date,103) as Modify_Date   from " & _
                    '" (Select 1 as SNO, TSPL_REMITTANCE_ENTRY.Remittance_Code as  Document_No ,TSPL_REMITTANCE_ENTRY.Remittance_Date as Document_Date,TSPL_REMITTANCE_ENTRY.Created_By ,TSPL_REMITTANCE_ENTRY.Created_Date,TSPL_REMITTANCE_ENTRY.Modify_By,TSPL_REMITTANCE_ENTRY.Modify_Date   from TSPL_REMITTANCE_ENTRY " & _
                    '"	where convert(date,TSPL_REMITTANCE_ENTRY.Remittance_Date ,103) >= convert(date,'" + dtpFromDate.Value + "',103) and convert(date,TSPL_REMITTANCE_ENTRY.Remittance_Date ,103) <= convert(date,'" + dtpToDate.Value + "',103)    ) as FirstTable left outer join  " & _
                    '" (select 1 as  SNO, a.Version_No , a.Date as From_Date ,(SELECT min(b.date)  FROM TSPL_Exe_Deployment b WHERE b.Date > a.Date   ) as To_Date  from TSPL_Exe_Deployment a ) SecondTable on SecondTable.SNO = FirstTable.SNO " & _
                    '" where convert (datetime ,FirstTable.Document_Date) between convert(datetime,SecondTable.From_Date) and convert(datetime,SecondTable.To_Date) " & _
                    '" order by convert (datetime,FirstTable.Document_Date) ,FirstTable.Document_No "

                    'qry = "SELECT CAST((case when TSPL_REMITTANCE_ENTRY.Posted ='Y' then 1 else 0 end)as BIT) as Status, TSPL_REMITTANCE_ENTRY.Remittance_Code as [Document Id], TSPL_REMITTANCE_ENTRY.Remittance_Date as [Document Date],isnull(TSPL_REMITTANCE_ENTRY.Amt_To_Remit,0) as [Amount], TSPL_REMITTANCE_ENTRY.Bank_Code as [Bank Code],TSPL_REMITTANCE_ENTRY.Payment_Code as [Payment Code],TSPL_REMITTANCE_ENTRY.Cheque_No as [Cheque No], TSPL_REMITTANCE_ENTRY.Description  as [Description],TSPL_REMITTANCE_ENTRY.Created_By as 'Created By' FROM TSPL_REMITTANCE_ENTRY WHERE  convert(date,TSPL_REMITTANCE_ENTRY.Remittance_Date,103) >= convert(date,'" + dtpFromDate.Value + "',103) and convert(date,TSPL_REMITTANCE_ENTRY.Remittance_Date ,103) <= convert(date,'" + dtpToDate.Value + "',103) "

                    'qry += " ORDER BY TSPL_REMITTANCE_ENTRY.Remittance_Date , TSPL_REMITTANCE_ENTRY.Remittance_Code "
                End If
            End If
        End If
        If clsCommon.CompairString(clsCommon.myCstr(qry), Nothing) <> CompairStringResult.Equal Then
            dt = clsDBFuncationality.GetDataTable(qry)
            gv1.DataSource = dt
            gv1.MasterTemplate.SummaryRowsBottom.Clear()
            'gv1Format()
            SetGridFormationOFgv()


        End If
        '-------------------------------------Code Ends Here--------------------------------------------
    End Sub

    ''-------------------------------------------------------------------
    '' Function For Filling--------Module(Payables)----------------------
    ''-------------------------------------------------------------------

    Sub FillPayables()

        If clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), "Payment Entry") = CompairStringResult.Equal Then
            Load_Authorisation(clsUserMgtCode.PaymentEntryNew)
            If dtAuthen.Rows.Count > 0 Then
                If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then
                    gv1.DataSource = Nothing

                    qry = GetQueryByScreen("TSPL_PAYMENT_HEADER", "Payment_No", "Payment_Date", "Created_By", "Created_Date", "Modify_By", "Modify_Date", "Modify_Date", " ")

                    '  qry = " select FirstTable.Document_No  ,FirstTable.Document_Date ,SecondTable.Version_No as Exe_Version ,FirstTable.Created_By as Created_By ,convert(varchar, FirstTable.Created_Date,103)   as Created_Date , FirstTable.Modify_By  ,convert (varchar,FirstTable.Modify_Date,103) as Modify_Date   from " & _
                    '" (Select 1 as SNO, TSPL_PAYMENT_HEADER.Payment_No as  Document_No ,TSPL_PAYMENT_HEADER.Payment_Date as Document_Date,TSPL_PAYMENT_HEADER.Created_By ,TSPL_PAYMENT_HEADER.Created_Date,TSPL_PAYMENT_HEADER.Modify_By,TSPL_PAYMENT_HEADER.Modify_Date   from TSPL_PAYMENT_HEADER " & _
                    '"	where convert(date,TSPL_PAYMENT_HEADER.Payment_Date ,103) >= convert(date,'" + dtpFromDate.Value + "',103) and convert(date,TSPL_PAYMENT_HEADER.Payment_Date ,103) <= convert(date,'" + dtpToDate.Value + "',103)    ) as FirstTable left outer join  " & _
                    '" (select 1 as  SNO, a.Version_No , a.Date as From_Date ,(SELECT min(b.date)  FROM TSPL_Exe_Deployment b WHERE b.Date > a.Date   ) as To_Date  from TSPL_Exe_Deployment a ) SecondTable on SecondTable.SNO = FirstTable.SNO " & _
                    '" where convert (datetime ,FirstTable.Document_Date) between convert(datetime,SecondTable.From_Date) and convert(datetime,SecondTable.To_Date) " & _
                    '" order by convert (datetime,FirstTable.Document_Date) ,FirstTable.Document_No "

                    'qry = "SELECT CAST((case when TSPL_PAYMENT_HEADER.Posted ='P' then 1 else 0 end)as BIT) as Status, TSPL_PAYMENT_HEADER.Payment_No as [Document Id], TSPL_PAYMENT_HEADER.Payment_Date as [Document Date], isnull(TSPL_PAYMENT_HEADER.Payment_Amount,0) as [Amount], TSPL_PAYMENT_HEADER.Vendor_Code as [Vendor Code],TSPL_PAYMENT_HEADER.Vendor_Name as [Vendor Name], TSPL_PAYMENT_HEADER.Bank_Code as [Bank Code], right(TSPL_BANK_MASTER.BANKACC,3) as Location, TSPL_PAYMENT_HEADER.Payment_Code as [Payment Code],TSPL_PAYMENT_HEADER.Payment_Type as [Payment Type],TSPL_PAYMENT_HEADER.Cheque_No as [Cheque No], TSPL_PAYMENT_HEADER.Narration as [Description],TSPL_PAYMENT_HEADER.Created_By as 'Created BY' FROM TSPL_PAYMENT_HEADER Left Outer Join TSPL_BANK_MASTER on TSPL_PAYMENT_HEADER.Bank_Code=TSPL_BANK_MASTER.BANK_CODE WHERE  convert(date,TSPL_PAYMENT_HEADER.Payment_Date,103) >= convert(date,'" + dtpFromDate.Value + "',103) and convert(date,TSPL_PAYMENT_HEADER.Payment_Date ,103) <= convert(date,'" + dtpToDate.Value + "',103) "
                    'qry += " ORDER BY TSPL_PAYMENT_HEADER.Payment_Date , TSPL_PAYMENT_HEADER.Payment_No "

                End If
            End If
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), "AP Invoice Entry") = CompairStringResult.Equal Then
            Load_Authorisation(clsUserMgtCode.mbtnAPInvoiceEntry)
            If dtAuthen.Rows.Count > 0 Then
                If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then
                    gv1.DataSource = Nothing

                    qry = GetQueryByScreen("TSPL_VENDOR_INVOICE_HEAD", "Document_No", "invoice_entry_date", "Created_By", "Created_Date", "Modify_By", "Modify_Date", "Posting_Date", " ")

                    '  qry = " select FirstTable.Document_No  ,FirstTable.Document_Date ,SecondTable.Version_No as Exe_Version ,FirstTable.Created_By as Created_By ,convert(varchar, FirstTable.Created_Date,103)   as Created_Date , FirstTable.Modify_By  ,convert (varchar,FirstTable.Modify_Date,103) as Modify_Date   from " & _
                    '" (Select 1 as SNO, TSPL_VENDOR_INVOICE_HEAD.Document_No as  Document_No ,TSPL_VENDOR_INVOICE_HEAD.invoice_entry_date as Document_Date,TSPL_VENDOR_INVOICE_HEAD.Created_By ,TSPL_VENDOR_INVOICE_HEAD.Created_Date,TSPL_VENDOR_INVOICE_HEAD.Modify_By,TSPL_VENDOR_INVOICE_HEAD.Modify_Date   from TSPL_VENDOR_INVOICE_HEAD " & _
                    '"	where convert(date,TSPL_VENDOR_INVOICE_HEAD.invoice_entry_date ,103) >= convert(date,'" + dtpFromDate.Value + "',103) and convert(date,TSPL_VENDOR_INVOICE_HEAD.invoice_entry_date ,103) <= convert(date,'" + dtpToDate.Value + "',103)    ) as FirstTable left outer join  " & _
                    '" (select 1 as  SNO, a.Version_No , a.Date as From_Date ,(SELECT min(b.date)  FROM TSPL_Exe_Deployment b WHERE b.Date > a.Date   ) as To_Date  from TSPL_Exe_Deployment a ) SecondTable on SecondTable.SNO = FirstTable.SNO " & _
                    '" where convert (datetime ,FirstTable.Document_Date) between convert(datetime,SecondTable.From_Date) and convert(datetime,SecondTable.To_Date) " & _
                    '" order by convert (datetime,FirstTable.Document_Date) ,FirstTable.Document_No "

                    'qry = "SELECT CAST((case when ISNULL(TSPL_VENDOR_INVOICE_HEAD.Posting_Date, '')= '' then 0 else 1 end)as BIT) as Status, TSPL_VENDOR_INVOICE_HEAD.Document_No as [Document Id], TSPL_VENDOR_INVOICE_HEAD.invoice_entry_date as [Document Date],isnull(TSPL_VENDOR_INVOICE_HEAD.Document_Total,0) as [Amount], TSPL_VENDOR_INVOICE_HEAD.Vendor_Code as [Vendor Code],TSPL_VENDOR_INVOICE_HEAD.Vendor_Name as [Vendor Name], TSPL_VENDOR_INVOICE_HEAD.RefDocNo, TSPL_VENDOR_INVOICE_HEAD.Description as " & _
                    '      "[Description], CAST((case when TSPL_VENDOR_INVOICE_HEAD.On_Hold ='Y' then 1 else 0 end)as BIT) as Hold,TSPL_VENDOR_INVOICE_HEAD.Created_By as 'Created BY' FROM TSPL_VENDOR_INVOICE_HEAD WHERE  convert(date,TSPL_VENDOR_INVOICE_HEAD.invoice_entry_date,103) >= convert(date,'" + dtpFromDate.Value + "',103) and convert(date,TSPL_VENDOR_INVOICE_HEAD.invoice_entry_date ,103) <= convert(date,'" + dtpToDate.Value + "',103) "


                    'qry += " ORDER BY TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_Date , TSPL_VENDOR_INVOICE_HEAD.Document_No "

                End If
            End If
        End If
        If clsCommon.CompairString(clsCommon.myCstr(qry), Nothing) <> CompairStringResult.Equal Then

            dt = clsDBFuncationality.GetDataTable(qry)
            gv1.DataSource = dt
            gv1.MasterTemplate.SummaryRowsBottom.Clear()
            'gv1Format()
            SetGridFormationOFgv()
            'gv1.MasterTemplate.Columns("Description").IsVisible = False
            'Me.gv1.Columns("RefDocNo").IsVisible = False

        End If
        '-------------------------------------Code Ends Here--------------------------------------------
    End Sub

    ''-------------------------------------------------------------------
    '' Function For Filling --------Module(Receivable)-------------------
    ''-------------------------------------------------------------------

    Sub FillReceivables()

        If clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), "Receipt Entry") = CompairStringResult.Equal Then
            Load_Authorisation(clsUserMgtCode.ReceiptEntry)
            If dtAuthen.Rows.Count > 0 Then
                If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then
                    gv1.DataSource = Nothing

                    qry = GetQueryByScreen("TSPL_RECEIPT_HEADER", "Receipt_NO", "Receipt_Date", "Created_By", "Created_Date", "Modify_By", "Modify_Date", "Receipt_Post_Date", " ")

                    '  qry = " select FirstTable.Document_No  ,FirstTable.Document_Date ,SecondTable.Version_No as Exe_Version ,FirstTable.Created_By as Created_By ,convert(varchar, FirstTable.Created_Date,103)   as Created_Date , FirstTable.Modify_By  ,convert (varchar,FirstTable.Modify_Date,103) as Modify_Date   from " & _
                    '" (Select 1 as SNO, TSPL_RECEIPT_HEADER.Document_No as  Document_No ,TSPL_RECEIPT_HEADER.Receipt_Date as Document_Date,TSPL_RECEIPT_HEADER.Created_By ,TSPL_RECEIPT_HEADER.Created_Date,TSPL_RECEIPT_HEADER.Modify_By,TSPL_RECEIPT_HEADER.Modify_Date   from TSPL_RECEIPT_HEADER " & _
                    '"	where convert(date,TSPL_RECEIPT_HEADER.Receipt_Date ,103) >= convert(date,'" + dtpFromDate.Value + "',103) and convert(date,TSPL_RECEIPT_HEADER.Receipt_Date ,103) <= convert(date,'" + dtpToDate.Value + "',103)    ) as FirstTable left outer join  " & _
                    '" (select 1 as  SNO, a.Version_No , a.Date as From_Date ,(SELECT min(b.date)  FROM TSPL_Exe_Deployment b WHERE b.Date > a.Date   ) as To_Date  from TSPL_Exe_Deployment a ) SecondTable on SecondTable.SNO = FirstTable.SNO " & _
                    '" where convert (datetime ,FirstTable.Document_Date) between convert(datetime,SecondTable.From_Date) and convert(datetime,SecondTable.To_Date) " & _
                    '" order by convert (datetime,FirstTable.Document_Date) ,FirstTable.Document_No "

                    'qry = "SELECT CAST((case when TSPL_RECEIPT_HEADER.Posted ='Y' then 1 else 0 end)as BIT) as Status, TSPL_RECEIPT_HEADER.Receipt_No as [Document Id], TSPL_RECEIPT_HEADER.Receipt_Date as [Document Date], isnull(TSPL_RECEIPT_HEADER.Receipt_Amount,0) as [Amount], TSPL_RECEIPT_HEADER.Cust_Code as [Customer Code],TSPL_RECEIPT_HEADER.Customer_Name as [Customer Name], TSPL_BANK_MASTER.BANK_CODE as [Bank Code], right(TSPL_BANK_MASTER.BANKACC,3) as Location, TSPL_RECEIPT_HEADER.Payment_Code as [Payment Code],TSPL_RECEIPT_HEADER.Cheque_No as [Cheque No],TSPL_RECEIPT_HEADER.Cheque_Date as [Cheque Date], TSPL_RECEIPT_HEADER.Entry_Desc as [Description],TSPL_RECEIPT_HEADER.Created_By as 'Created By' FROM TSPL_RECEIPT_HEADER Left Outer Join TSPL_BANK_MASTER on TSPL_RECEIPT_HEADER.Bank_Code=TSPL_BANK_MASTER.BANK_CODE WHERE  convert(date,TSPL_RECEIPT_HEADER.Receipt_Date,103) >= convert(date,'" + dtpFromDate.Value + "',103) and convert(date,TSPL_RECEIPT_HEADER.Receipt_Date  ,103) <= convert(date,'" + dtpToDate.Value + "',103) "

                    'qry += " ORDER BY TSPL_RECEIPT_HEADER.Receipt_Date , TSPL_RECEIPT_HEADER.Receipt_No "
                End If

            End If

        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), "Adjustment Entry") = CompairStringResult.Equal Then
            Load_Authorisation(clsUserMgtCode.ReceiptAdjustmentEntry)
            If dtAuthen.Rows.Count > 0 Then
                If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then
                    gv1.DataSource = Nothing

                    qry = GetQueryByScreen("TSPL_Receipt_Adjustment_Header", "Adjustment_No", "Adjustment_Date", "Created_By", "Created_Date", "Modify_By", "Modify_Date", "Post_Date", " ")

                    '  qry = " select FirstTable.Document_No  ,FirstTable.Document_Date ,SecondTable.Version_No as Exe_Version ,FirstTable.Created_By as Created_By ,convert(varchar, FirstTable.Created_Date,103)   as Created_Date , FirstTable.Modify_By  ,convert (varchar,FirstTable.Modify_Date,103) as Modify_Date   from " & _
                    '" (Select 1 as SNO, TSPL_Receipt_Adjustment_Header.Adjustment_No as  Document_No ,TSPL_Receipt_Adjustment_Header.Adjustment_Date as Document_Date,TSPL_Receipt_Adjustment_Header.Created_By ,TSPL_Receipt_Adjustment_Header.Created_Date,TSPL_Receipt_Adjustment_Header.Modify_By,TSPL_Receipt_Adjustment_Header.Modify_Date   from TSPL_Receipt_Adjustment_Header " & _
                    '"	where convert(date,TSPL_Receipt_Adjustment_Header.Adjustment_Date ,103) >= convert(date,'" + dtpFromDate.Value + "',103) and convert(date,TSPL_Receipt_Adjustment_Header.Adjustment_Date ,103) <= convert(date,'" + dtpToDate.Value + "',103)    ) as FirstTable left outer join  " & _
                    '" (select 1 as  SNO, a.Version_No , a.Date as From_Date ,(SELECT min(b.date)  FROM TSPL_Exe_Deployment b WHERE b.Date > a.Date   ) as To_Date  from TSPL_Exe_Deployment a ) SecondTable on SecondTable.SNO = FirstTable.SNO " & _
                    '" where convert (datetime ,FirstTable.Document_Date) between convert(datetime,SecondTable.From_Date) and convert(datetime,SecondTable.To_Date) " & _
                    '" order by convert (datetime,FirstTable.Document_Date) ,FirstTable.Document_No "

                    'qry = "SELECT CAST((case when TSPL_Receipt_Adjustment_Header.Is_Post='Y' then 1 else 0 end)as BIT) as Status, TSPL_Receipt_Adjustment_Header.Adjustment_No as [Document Id], TSPL_Receipt_Adjustment_Header.Adjustment_Date as [Document Date], isnull(TSPL_Receipt_Adjustment_Header.Adjustment_Amount,0) as [Amount], TSPL_SALE_INVOICE_HEAD.Location , TSPL_Receipt_Adjustment_Header.Customer_No as [Customer Code],TSPL_CUSTOMER_MASTER.Customer_Name as [Customer Name], TSPL_Receipt_Adjustment_Header.Description  as [Description],TSPL_Receipt_Adjustment_Header.Created_By as 'Created By' FROM TSPL_Receipt_Adjustment_Header Left Outer Join TSPL_CUSTOMER_MASTER on TSPL_Receipt_Adjustment_Header.Customer_No=TSPL_CUSTOMER_MASTER.Cust_Code Left Outer Join TSPL_SALE_INVOICE_HEAD ON TSPL_Receipt_Adjustment_Header.Doc_No=TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No WHERE  convert(date,TSPL_Receipt_Adjustment_Header.Adjustment_Date ,103) >= convert(date,'" + dtpFromDate.Value + "',103) and convert(date,TSPL_Receipt_Adjustment_Header.Adjustment_Date   ,103) <= convert(date,'" + dtpToDate.Value + "',103) "


                    'qry += " ORDER BY TSPL_Receipt_Adjustment_Header.Adjustment_Date , TSPL_Receipt_Adjustment_Header.Adjustment_No "
                End If
            End If
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), "AR Invoice Entry") = CompairStringResult.Equal Then
            Load_Authorisation(clsUserMgtCode.mbtnARInvoiceEntry)
            If dtAuthen.Rows.Count > 0 Then
                If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then
                    gv1.DataSource = Nothing

                    qry = GetQueryByScreen("TSPL_Customer_Invoice_Head", "Document_No", "Document_Date", "Created_By", "Created_Date", "Modify_By", "Modify_Date", "Posting_Date", " ")

                    '  qry = " select FirstTable.Document_No  ,FirstTable.Document_Date ,SecondTable.Version_No as Exe_Version ,FirstTable.Created_By as Created_By ,convert(varchar, FirstTable.Created_Date,103)   as Created_Date , FirstTable.Modify_By  ,convert (varchar,FirstTable.Modify_Date,103) as Modify_Date   from " & _
                    '" (Select 1 as SNO, TSPL_Customer_Invoice_Head.Document_No as  Document_No ,TSPL_Customer_Invoice_Head.Document_Date as Document_Date,TSPL_Customer_Invoice_Head.Created_By ,TSPL_Customer_Invoice_Head.Created_Date,TSPL_Customer_Invoice_Head.Modify_By,TSPL_Customer_Invoice_Head.Modify_Date   from TSPL_Customer_Invoice_Head " & _
                    '"	where convert(date,TSPL_Customer_Invoice_Head.Document_Date ,103) >= convert(date,'" + dtpFromDate.Value + "',103) and convert(date,TSPL_Customer_Invoice_Head.Document_Date ,103) <= convert(date,'" + dtpToDate.Value + "',103)    ) as FirstTable left outer join  " & _
                    '" (select 1 as  SNO, a.Version_No , a.Date as From_Date ,(SELECT min(b.date)  FROM TSPL_Exe_Deployment b WHERE b.Date > a.Date   ) as To_Date  from TSPL_Exe_Deployment a ) SecondTable on SecondTable.SNO = FirstTable.SNO " & _
                    '" where convert (datetime ,FirstTable.Document_Date) between convert(datetime,SecondTable.From_Date) and convert(datetime,SecondTable.To_Date) " & _
                    '" order by convert (datetime,FirstTable.Document_Date) ,FirstTable.Document_No "

                    'qry = "SELECT CAST((case when TSPL_Customer_Invoice_Head.status =1 then 1 else 0 end)as BIT) as Status, TSPL_Customer_Invoice_Head.Document_No  as [Document Id],TSPL_Customer_Invoice_Head.Document_Date as [Document Date],isnull(TSPL_Customer_Invoice_Head.Document_Total,0) as [Amount], TSPL_Customer_Invoice_Head.Customer_code as [Customer Code],TSPL_Customer_Invoice_Head.Customer_Name  as [Customer Name], TSPL_Customer_Invoice_Head.Description  as [Description],TSPL_Customer_Invoice_Head.Created_By as 'Created By' FROM TSPL_Customer_Invoice_Head WHERE  convert(date,TSPL_Customer_Invoice_Head.Document_Date ,103) >= convert(date,'" + dtpFromDate.Value + "',103) and convert(date,TSPL_Customer_Invoice_Head.Document_Date   ,103) <= convert(date,'" + dtpToDate.Value + "',103) "
                    'qry += " ORDER BY TSPL_Customer_Invoice_Head.Document_Date , TSPL_Customer_Invoice_Head.Document_No "


                End If
            End If
        End If

        If clsCommon.CompairString(clsCommon.myCstr(qry), Nothing) <> CompairStringResult.Equal Then

            dt = clsDBFuncationality.GetDataTable(qry)
            gv1.DataSource = dt
            gv1.MasterTemplate.SummaryRowsBottom.Clear()
            ' gv1Format()

            SetGridFormationOFgv()

        End If
        '-------------------------------------Code Ends Here--------------------------------------------
    End Sub

    ''-------------------------------------------------------------------
    '' Function For Filling --------Module(Common Services)--------------
    ''-------------------------------------------------------------------

    Sub FillCommonServices()
        If clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), "Bank Transfer") = CompairStringResult.Equal Then
            Load_Authorisation(clsUserMgtCode.bankTransfer)
            If dtAuthen.Rows.Count > 0 Then
                If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then
                    gv1.DataSource = Nothing

                    ' qry = GetQueryByScreen("TSPL_BANK_TRANSFER", "Transfer_No", "Transfer_Date", "Created_By", "Created_Date", "Modify_By", "Modify_Date", "Modify_Date", " ")

                    '  qry = " select FirstTable.Document_No  ,FirstTable.Document_Date ,SecondTable.Version_No as Exe_Version ,FirstTable.Created_By as Created_By ,convert(varchar, FirstTable.Created_Date,103)   as Created_Date , FirstTable.Modify_By  ,convert (varchar,FirstTable.Modify_Date,103) as Modify_Date   from " & _
                    '" (Select 1 as SNO, TSPL_BANK_TRANSFER.Transfer_No as  Document_No ,TSPL_BANK_TRANSFER.Transfer_Date as Document_Date,TSPL_BANK_TRANSFER.Created_By ,TSPL_BANK_TRANSFER.Created_Date,TSPL_BANK_TRANSFER.Modify_By,TSPL_BANK_TRANSFER.Modify_Date   from TSPL_BANK_TRANSFER " & _
                    '"	where convert(date,TSPL_BANK_TRANSFER.Transfer_Date ,103) >= convert(date,'" + dtpFromDate.Value + "',103) and convert(date,TSPL_BANK_TRANSFER.Transfer_Date ,103) <= convert(date,'" + dtpToDate.Value + "',103)    ) as FirstTable left outer join  " & _
                    '" (select 1 as  SNO, a.Version_No , a.Date as From_Date ,(SELECT min(b.date)  FROM TSPL_Exe_Deployment b WHERE b.Date > a.Date   ) as To_Date  from TSPL_Exe_Deployment a ) SecondTable on SecondTable.SNO = FirstTable.SNO " & _
                    '" where convert (datetime ,FirstTable.Document_Date) between convert(datetime,SecondTable.From_Date) and convert(datetime,SecondTable.To_Date) " & _
                    '" order by convert (datetime,FirstTable.Document_Date) ,FirstTable.Document_No "

                    'qry = "SELECT CAST((case when TSPL_BANK_TRANSFER.Post ='P' then 1 else 0 end)as BIT) as Status, TSPL_BANK_TRANSFER.Transfer_No as [Document Id], TSPL_BANK_TRANSFER.Transfer_Date as [Document Date],isnull(TSPL_BANK_TRANSFER.Transfer_Amount,0) as [Amount], TSPL_BANK_TRANSFER.From_Bank_Code as [From-Bank Code], TSPL_BANK_TRANSFER.From_Bank_Name as [From-Bank Name], TSPL_BANK_TRANSFER.From_Bank_Acc_No as [From-Bank A/C No], TSPL_BANK_TRANSFER.To_Bank_Code as [To-Bank Code], TSPL_BANK_TRANSFER.To_Bank_Name as [To-Bank Name], TSPL_BANK_TRANSFER.From_Bank_Acc_No as [From-Bank A/C No], TSPL_BANK_TRANSFER.Description as [Description],TSPL_BANK_TRANSFER.Created_By as 'Created By' FROM TSPL_BANK_TRANSFER WHERE  convert(date,TSPL_BANK_TRANSFER.Transfer_Date ,103) >= convert(date,'" + dtpFromDate.Value + "',103) and convert(date,TSPL_BANK_TRANSFER.Transfer_Date ,103) <= convert(date,'" + dtpToDate.Value + "',103) "


                    'qry += " ORDER BY TSPL_BANK_TRANSFER.Transfer_Date, TSPL_BANK_TRANSFER.Transfer_No "

                End If
            End If

        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), "Reverse Transaction") = CompairStringResult.Equal Then
            Load_Authorisation(clsUserMgtCode.reverseTransaction)
            If dtAuthen.Rows.Count > 0 Then
                If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then
                    gv1.DataSource = Nothing

                    qry = GetQueryByScreen("TSPL_BANK_REVERSE", "Reverse_Code", "Reversal_Date", "Created_By", "Created_Date", "Modify_By", "Modify_Date", "Modify_Date", " ")

                    '  qry = " select FirstTable.Document_No  ,FirstTable.Document_Date ,SecondTable.Version_No as Exe_Version ,FirstTable.Created_By as Created_By ,convert(varchar, FirstTable.Created_Date,103)   as Created_Date , FirstTable.Modify_By  ,convert (varchar,FirstTable.Modify_Date,103) as Modify_Date   from " & _
                    '" (Select 1 as SNO, TSPL_BANK_REVERSE.Reverse_Code as  Document_No ,TSPL_BANK_REVERSE.Reversal_Date as Document_Date,TSPL_BANK_REVERSE.Created_By ,TSPL_BANK_REVERSE.Created_Date,TSPL_BANK_REVERSE.Modify_By,TSPL_BANK_REVERSE.Modify_Date   from TSPL_BANK_REVERSE " & _
                    '"	where convert(date,TSPL_BANK_REVERSE.Reversal_Date ,103) >= convert(date,'" + dtpFromDate.Value + "',103) and convert(date,TSPL_BANK_REVERSE.Reversal_Date ,103) <= convert(date,'" + dtpToDate.Value + "',103)    ) as FirstTable left outer join  " & _
                    '" (select 1 as  SNO, a.Version_No , a.Date as From_Date ,(SELECT min(b.date)  FROM TSPL_Exe_Deployment b WHERE b.Date > a.Date   ) as To_Date  from TSPL_Exe_Deployment a ) SecondTable on SecondTable.SNO = FirstTable.SNO " & _
                    '" where convert (datetime ,FirstTable.Document_Date) between convert(datetime,SecondTable.From_Date) and convert(datetime,SecondTable.To_Date) " & _
                    '" order by convert (datetime,FirstTable.Document_Date) ,FirstTable.Document_No "

                    'qry = "SELECT CAST((case when TSPL_BANK_REVERSE.Post ='P' then 1 else 0 end)as BIT) as Status, TSPL_BANK_REVERSE.Reverse_Code as [Document Id], TSPL_BANK_REVERSE.Reversal_Date as [Document Date],isnull(TSPL_BANK_REVERSE.Amount,0) as [Amount], TSPL_BANK_REVERSE.Bank_Code as [Bank Code], TSPL_BANK_REVERSE.Back_Acc_No as [Back A/C No], TSPL_BANK_REVERSE.Vendor_Code as [Vendor Code], TSPL_BANK_REVERSE.Vendor_Name as [Vendor Name], TSPL_BANK_REVERSE.Cust_Code as [Customer Code], TSPL_BANK_REVERSE.Cust_Name as [Customer Name], TSPL_BANK_REVERSE.Cheque_No as [Cheque No],'' as [Description],TSPL_BANK_REVERSE.Created_By as 'Created BY' FROM TSPL_BANK_REVERSE WHERE  convert(date,TSPL_BANK_REVERSE.Reversal_Date ,103) >= convert(date,'" + dtpFromDate.Value + "',103) and convert(date,TSPL_BANK_REVERSE.Reversal_Date,103) <= convert(date,'" + dtpToDate.Value + "',103) "


                    'qry += " ORDER BY TSPL_BANK_REVERSE.Reversal_Date, TSPL_BANK_REVERSE.Reverse_Code "

                End If
            End If
        End If
        If clsCommon.CompairString(clsCommon.myCstr(qry), Nothing) <> CompairStringResult.Equal Then

            dt = clsDBFuncationality.GetDataTable(qry)
            gv1.DataSource = dt
            gv1.MasterTemplate.SummaryRowsBottom.Clear()
            'gv1Format()
            SetGridFormationOFgv()

        End If
        '-------------------------------------Code Ends Here--------------------------------------------
    End Sub

    Sub FillHRAndPayroll()
        '' fill Employee Salary
        If clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), "Employee Salary") = CompairStringResult.Equal Then
            Load_Authorisation(clsUserMgtCode.frmEmpSalary)
            If dtAuthen.Rows.Count > 0 Then
                If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then
                    gv1.DataSource = Nothing

                    qry = GetQueryByScreen("TSPL_EMPLOYEE_SALARY", "EMP_SAL_CODE", "APPLICABLE_FROM", "Created_By", "Created_Date", "Modified_By", "Modified_Date", "Posting_Date", " ")

                    '  qry = " select FirstTable.Document_No  ,FirstTable.Document_Date ,SecondTable.Version_No as Exe_Version ,FirstTable.Created_By as Created_By ,convert(varchar, FirstTable.Created_Date,103)   as Created_Date , FirstTable.Modify_By  ,convert (varchar,FirstTable.Modify_Date,103) as Modify_Date   from " & _
                    '" (Select 1 as SNO, TSPL_EMPLOYEE_SALARY.EMP_SAL_CODE as  Document_No ,TSPL_EMPLOYEE_SALARY.APPLICABLE_FROM as Document_Date,TSPL_EMPLOYEE_SALARY.Created_By ,TSPL_EMPLOYEE_SALARY.Created_Date,TSPL_EMPLOYEE_SALARY.Modify_By,TSPL_EMPLOYEE_SALARY.Modify_Date   from TSPL_EMPLOYEE_SALARY " & _
                    '"	where convert(date,TSPL_EMPLOYEE_SALARY.APPLICABLE_FROM ,103) >= convert(date,'" + dtpFromDate.Value + "',103) and convert(date,TSPL_EMPLOYEE_SALARY.APPLICABLE_FROM ,103) <= convert(date,'" + dtpToDate.Value + "',103)    ) as FirstTable left outer join  " & _
                    '" (select 1 as  SNO, a.Version_No , a.Date as From_Date ,(SELECT min(b.date)  FROM TSPL_Exe_Deployment b WHERE b.Date > a.Date   ) as To_Date  from TSPL_Exe_Deployment a ) SecondTable on SecondTable.SNO = FirstTable.SNO " & _
                    '" where convert (datetime ,FirstTable.Document_Date) between convert(datetime,SecondTable.From_Date) and convert(datetime,SecondTable.To_Date) " & _
                    '" order by convert (datetime,FirstTable.Document_Date) ,FirstTable.Document_No "

                    'qry = " SELECT POSTED as Status,EMP_SAL_CODE as [Document Id],APPLICABLE_FROM as [Document Date],'' AS [Description],TSPL_EMPLOYEE_SALARY.EMP_CODE as [Employee Code]," & _
                    '      " TSPL_EMPLOYEE_MASTER.Emp_Name AS [Employee Name],REVISION_NO as [Revision No],SALARY_STRUCTURE_CODE as [Salary Structure], " & _
                    '      " TSPL_EMPLOYEE_SALARY.Created_By as [Created By],TSPL_EMPLOYEE_SALARY.Created_Date as [Created Date] " & _
                    '      " FROM TSPL_EMPLOYEE_SALARY left join TSPL_EMPLOYEE_MASTER ON TSPL_EMPLOYEE_SALARY.EMP_CODE=TSPL_EMPLOYEE_MASTER.EMP_CODE " & _
                    '      " WHERE  convert(date,TSPL_EMPLOYEE_SALARY.APPLICABLE_FROM ,103) >= convert(date,'" + dtpFromDate.Value + "',103) and convert(date,TSPL_EMPLOYEE_SALARY.APPLICABLE_FROM,103) <= convert(date,'" + dtpToDate.Value + "',103)"


                    'qry += " ORDER BY TSPL_EMPLOYEE_SALARY.APPLICABLE_FROM, TSPL_EMPLOYEE_SALARY.EMP_SAL_CODE "

                End If
            End If

            '' fill Employee Hourly Attendance
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), "Hourly Attendance") = CompairStringResult.Equal Then
            Load_Authorisation("Hourly Attendance")
            If dtAuthen.Rows.Count > 0 Then
                If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then
                    gv1.DataSource = Nothing

                    qry = GetQueryByScreen("TSPL_HOURLY_ATTENDANCE", "DLA_CODE", "Created_Date", "Created_By", "Created_Date", "Modified_By", "Modified_Date", "Posting_Date", " ")

                    '  qry = " select FirstTable.Document_No  ,FirstTable.Document_Date ,SecondTable.Version_No as Exe_Version ,FirstTable.Created_By as Created_By ,convert(varchar, FirstTable.Created_Date,103)   as Created_Date , FirstTable.Modify_By  ,convert (varchar,FirstTable.Modify_Date,103) as Modify_Date   from " & _
                    '" (Select 1 as SNO, TSPL_HOURLY_ATTENDANCE.EMP_SAL_CODE as  Document_No ,TSPL_HOURLY_ATTENDANCE.Created_Date as Document_Date,TSPL_HOURLY_ATTENDANCE.Created_By ,TSPL_HOURLY_ATTENDANCE.Created_Date,TSPL_HOURLY_ATTENDANCE.Modify_By,TSPL_HOURLY_ATTENDANCE.Modify_Date   from TSPL_HOURLY_ATTENDANCE " & _
                    '"	where convert(date,TSPL_HOURLY_ATTENDANCE.Created_Date ,103) >= convert(date,'" + dtpFromDate.Value + "',103) and convert(date,TSPL_HOURLY_ATTENDANCE.Created_Date ,103) <= convert(date,'" + dtpToDate.Value + "',103)    ) as FirstTable left outer join  " & _
                    '" (select 1 as  SNO, a.Version_No , a.Date as From_Date ,(SELECT min(b.date)  FROM TSPL_Exe_Deployment b WHERE b.Date > a.Date   ) as To_Date  from TSPL_Exe_Deployment a ) SecondTable on SecondTable.SNO = FirstTable.SNO " & _
                    '" where convert (datetime ,FirstTable.Document_Date) between convert(datetime,SecondTable.From_Date) and convert(datetime,SecondTable.To_Date) " & _
                    '" order by convert (datetime,FirstTable.Document_Date) ,FirstTable.Document_No "

                    'qry = " SELECT POSTED as Status,DLA_CODE as [Document Id],'' AS [Description],PAY_PERIOD_CODE as [Pay Period Code],REGISTER_TYPE as [Register Type]," & _
                    '      " TSPL_HOURLY_ATTENDANCE.Created_Date as [Document Date],ENTEREDBY_EMP_CODE as [Entered By],TSPL_HOURLY_ATTENDANCE.Created_By as [Created By],TSPL_HOURLY_ATTENDANCE.Created_Date as [Created Date] FROM TSPL_HOURLY_ATTENDANCE  " & _
                    '      " WHERE  convert(date,TSPL_HOURLY_ATTENDANCE.Created_Date ,103) >= convert(date,'" + dtpFromDate.Value + "',103) " & _
                    '      " and convert(date,TSPL_HOURLY_ATTENDANCE.Created_Date,103) <= convert(date,'" + dtpToDate.Value + "',103) "


                    'qry += " ORDER BY TSPL_HOURLY_ATTENDANCE.Created_Date, TSPL_HOURLY_ATTENDANCE.DLA_CODE "

                End If
            End If

            '' fill Employee Daily Attendance
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), "Daily Attendance") = CompairStringResult.Equal Then
            Load_Authorisation("Daily Attendance")
            If dtAuthen.Rows.Count > 0 Then
                If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then
                    gv1.DataSource = Nothing
                    qry = GetQueryByScreen("TSPL_DAILY_ATTENDANCE", "DLA_CODE", "Created_Date", "Created_By", "Created_Date", "Modified_By", "Modified_Date", "Posting_Date", " ")


                    '  qry = " select FirstTable.Document_No  ,FirstTable.Document_Date ,SecondTable.Version_No as Exe_Version ,FirstTable.Created_By as Created_By ,convert(varchar, FirstTable.Created_Date,103)   as Created_Date , FirstTable.Modify_By  ,convert (varchar,FirstTable.Modify_Date,103) as Modify_Date   from " & _
                    '" (Select 1 as SNO, TSPL_DAILY_ATTENDANCE.EMP_SAL_CODE as  Document_No ,TSPL_DAILY_ATTENDANCE.Created_Date as Document_Date,TSPL_DAILY_ATTENDANCE.Created_By ,TSPL_DAILY_ATTENDANCE.Created_Date,TSPL_DAILY_ATTENDANCE.Modify_By,TSPL_DAILY_ATTENDANCE.Modify_Date   from TSPL_DAILY_ATTENDANCE " & _
                    '"	where convert(date,TSPL_DAILY_ATTENDANCE.Created_Date ,103) >= convert(date,'" + dtpFromDate.Value + "',103) and convert(date,TSPL_DAILY_ATTENDANCE.Created_Date ,103) <= convert(date,'" + dtpToDate.Value + "',103)    ) as FirstTable left outer join  " & _
                    '" (select 1 as  SNO, a.Version_No , a.Date as From_Date ,(SELECT min(b.date)  FROM TSPL_Exe_Deployment b WHERE b.Date > a.Date   ) as To_Date  from TSPL_Exe_Deployment a ) SecondTable on SecondTable.SNO = FirstTable.SNO " & _
                    '" where convert (datetime ,FirstTable.Document_Date) between convert(datetime,SecondTable.From_Date) and convert(datetime,SecondTable.To_Date) " & _
                    '" order by convert (datetime,FirstTable.Document_Date) ,FirstTable.Document_No "


                    'qry = " SELECT POSTED as Status,DLA_CODE as [Document Id],'' AS [Description],PAY_PERIOD_CODE as [Pay Period Code],REGISTER_TYPE as [Register Type]," & _
                    '      " TSPL_DAILY_ATTENDANCE.Created_Date as [Document Date],ENTEREDBY_EMP_CODE as [Entered By],TSPL_DAILY_ATTENDANCE.Created_By as [Created By],TSPL_DAILY_ATTENDANCE.Created_Date as [Created Date] FROM TSPL_DAILY_ATTENDANCE  " & _
                    '      " WHERE  convert(date,TSPL_DAILY_ATTENDANCE.Created_Date ,103) >= convert(date,'" + dtpFromDate.Value + "',103) " & _
                    '      " and convert(date,TSPL_DAILY_ATTENDANCE.Created_Date,103) <= convert(date,'" + dtpToDate.Value + "',103) "



                    'qry += " ORDER BY TSPL_DAILY_ATTENDANCE.Created_Date, TSPL_DAILY_ATTENDANCE.DLA_CODE "

                End If
            End If

            '' fill Employee Monthly Attendance
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), "Monthly Attendance") = CompairStringResult.Equal Then
            Load_Authorisation("Monthly Attendance")
            If dtAuthen.Rows.Count > 0 Then
                If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then
                    gv1.DataSource = Nothing
                    qry = GetQueryByScreen("TSPL_MONTHLY_ATTENDANCE", "MTA_CODE", "Created_Date", "Created_By", "Created_Date", "Modified_By", "Modified_Date", "Posting_Date", " ")

                    '  qry = " select FirstTable.Document_No  ,FirstTable.Document_Date ,SecondTable.Version_No as Exe_Version ,FirstTable.Created_By as Created_By ,convert(varchar, FirstTable.Created_Date,103)   as Created_Date , FirstTable.Modify_By  ,convert (varchar,FirstTable.Modify_Date,103) as Modify_Date   from " & _
                    '" (Select 1 as SNO, TSPL_MONTHLY_ATTENDANCE.EMP_SAL_CODE as  Document_No ,TSPL_MONTHLY_ATTENDANCE.Created_Date as Document_Date,TSPL_MONTHLY_ATTENDANCE.Created_By ,TSPL_MONTHLY_ATTENDANCE.Created_Date,TSPL_MONTHLY_ATTENDANCE.Modify_By,TSPL_MONTHLY_ATTENDANCE.Modify_Date   from TSPL_MONTHLY_ATTENDANCE " & _
                    '"	where convert(date,TSPL_MONTHLY_ATTENDANCE.Created_Date ,103) >= convert(date,'" + dtpFromDate.Value + "',103) and convert(date,TSPL_MONTHLY_ATTENDANCE.Created_Date ,103) <= convert(date,'" + dtpToDate.Value + "',103)    ) as FirstTable left outer join  " & _
                    '" (select 1 as  SNO, a.Version_No , a.Date as From_Date ,(SELECT min(b.date)  FROM TSPL_Exe_Deployment b WHERE b.Date > a.Date   ) as To_Date  from TSPL_Exe_Deployment a ) SecondTable on SecondTable.SNO = FirstTable.SNO " & _
                    '" where convert (datetime ,FirstTable.Document_Date) between convert(datetime,SecondTable.From_Date) and convert(datetime,SecondTable.To_Date) " & _
                    '" order by convert (datetime,FirstTable.Document_Date) ,FirstTable.Document_No "

                    'qry = " SELECT POSTED as Status,MTA_CODE as [Document Id],'' AS [Description],PAY_PERIOD_CODE as [Pay Period Code],REGISTER_TYPE as [Register Type]," & _
                    '      " TSPL_MONTHLY_ATTENDANCE.Created_Date as [Document Date],ENTEREDBY_EMP_CODE as [Entered By],TSPL_MONTHLY_ATTENDANCE.Created_By as [Created By],TSPL_MONTHLY_ATTENDANCE.Created_Date as [Created Date] FROM TSPL_MONTHLY_ATTENDANCE  " & _
                    '      " WHERE  convert(date,TSPL_MONTHLY_ATTENDANCE.Created_Date ,103) >= convert(date,'" + dtpFromDate.Value + "',103) " & _
                    '      " and convert(date,TSPL_MONTHLY_ATTENDANCE.Created_Date,103) <= convert(date,'" + dtpToDate.Value + "',103) "



                    'qry += " ORDER BY TSPL_MONTHLY_ATTENDANCE.Created_Date, TSPL_MONTHLY_ATTENDANCE.MTA_CODE "

                End If
            End If

            '' fill Employee Allowance
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), "Allowance") = CompairStringResult.Equal Then
            Load_Authorisation("Allowance")
            If dtAuthen.Rows.Count > 0 Then
                If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then
                    gv1.DataSource = Nothing
                    qry = GetQueryByScreen("TSPL_ALLOWANCE", "ALLOWANCE_CODE", "ALLOWANCE_DATE", "Created_By", "Created_Date", "Modified_By", "Modified_Date", "Posting_Date", " ")


                    '  qry = " select FirstTable.Document_No  ,FirstTable.Document_Date ,SecondTable.Version_No as Exe_Version ,FirstTable.Created_By as Created_By ,convert(varchar, FirstTable.Created_Date,103)   as Created_Date , FirstTable.Modify_By  ,convert (varchar,FirstTable.Modify_Date,103) as Modify_Date   from " & _
                    '" (Select 1 as SNO, TSPL_ALLOWANCE.ALLOWANCE_CODE as  Document_No ,TSPL_ALLOWANCE.ALLOWANCE_DATE as Document_Date,TSPL_ALLOWANCE.Created_By ,TSPL_ALLOWANCE.Created_Date,TSPL_ALLOWANCE.Modify_By,TSPL_ALLOWANCE.Modify_Date   from TSPL_ALLOWANCE " & _
                    '"	where convert(date,TSPL_ALLOWANCE.ALLOWANCE_DATE ,103) >= convert(date,'" + dtpFromDate.Value + "',103) and convert(date,TSPL_ALLOWANCE.ALLOWANCE_DATE ,103) <= convert(date,'" + dtpToDate.Value + "',103)    ) as FirstTable left outer join  " & _
                    '" (select 1 as  SNO, a.Version_No , a.Date as From_Date ,(SELECT min(b.date)  FROM TSPL_Exe_Deployment b WHERE b.Date > a.Date   ) as To_Date  from TSPL_Exe_Deployment a ) SecondTable on SecondTable.SNO = FirstTable.SNO " & _
                    '" where convert (datetime ,FirstTable.Document_Date) between convert(datetime,SecondTable.From_Date) and convert(datetime,SecondTable.To_Date) " & _
                    '" order by convert (datetime,FirstTable.Document_Date) ,FirstTable.Document_No "

                    'qry = " SELECT TSPL_ALLOWANCE.POSTED as Status,TSPL_ALLOWANCE.ALLOWANCE_CODE as [Document Id],TSPL_ALLOWANCE.PAY_PERIOD_CODE as [Pay Period Code], " & _
                    '      " TSPL_ALLOWANCE.ALLOWANCE_DATE as [Document Date],TSPL_ALLOWANCE.EMP_CODE as [Employee Code],TSPL_ALLOWANCE.ALLOWANCE_REMARKS as [Description],  " & _
                    '      " SUM(isnull(TSPL_ALLOWANCE_DETAIL.Allowance_Amount,0)) as Amount,TSPL_ALLOWANCE.Created_By as [Created By],TSPL_ALLOWANCE.Created_Date as [Created Date] FROM TSPL_ALLOWANCE LEFT OUTER JOIN TSPL_ALLOWANCE_DETAIL ON TSPL_ALLOWANCE.ALLOWANCE_CODE=TSPL_ALLOWANCE_DETAIL.ALLOWANCE_CODE " & _
                    '      " WHERE  convert(date,TSPL_ALLOWANCE.Created_Date ,103) >= convert(date,'" + dtpFromDate.Value + "',103) " & _
                    '      " and convert(date,TSPL_ALLOWANCE.Created_Date,103) <= convert(date,'" + dtpToDate.Value + "',103) "


                    'qry += " GROUP BY TSPL_ALLOWANCE.ALLOWANCE_CODE,TSPL_ALLOWANCE.POSTED,TSPL_ALLOWANCE.PAY_PERIOD_CODE,TSPL_ALLOWANCE.ALLOWANCE_DATE, TSPL_ALLOWANCE.EMP_CODE,TSPL_ALLOWANCE.ALLOWANCE_REMARKS,TSPL_ALLOWANCE.Created_By,TSPL_ALLOWANCE.Created_Date"
                    'qry += " ORDER BY TSPL_ALLOWANCE.ALLOWANCE_DATE, TSPL_ALLOWANCE.ALLOWANCE_CODE "
                End If
            End If

            '' fill Employee Deduction
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), "Deduction") = CompairStringResult.Equal Then
            Load_Authorisation("Deduction")
            If dtAuthen.Rows.Count > 0 Then
                If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then
                    gv1.DataSource = Nothing

                    qry = GetQueryByScreen("TSPL_DEDUCTION", "DEDUCTION_CODE", "DEDUCTION_DATE", "Created_By", "Created_Date", "Modified_By", "Modified_Date", "Posting_Date", " ")

                    'qry = " select FirstTable.Document_No  ,FirstTable.Document_Date ,SecondTable.Version_No as Exe_Version ,FirstTable.Created_By as Created_By ,convert(varchar, FirstTable.Created_Date,103)   as Created_Date , FirstTable.Modify_By  ,convert (varchar,FirstTable.Modify_Date,103) as Modify_Date   from " & _
                    ' " (Select 1 as SNO, TSPL_DEDUCTION.BO_CODE as  Document_No ,TSPL_DEDUCTION.DEDUCTION_DATE  as Document_Date,TSPL_DEDUCTION.Created_By ,TSPL_DEDUCTION.Created_Date,TSPL_DEDUCTION.Modify_By,TSPL_DEDUCTION.Modify_Date   from TSPL_DEDUCTION " & _
                    ' "	where convert(date,TSPL_DEDUCTION.DEDUCTION_DATE  ,103) >= convert(date,'" + dtpFromDate.Value + "',103) and convert(date,TSPL_DEDUCTION.DEDUCTION_DATE  ,103) <= convert(date,'" + dtpToDate.Value + "',103)    ) as FirstTable left outer join  " & _
                    ' " (select 1 as  SNO, a.Version_No , a.Date as From_Date ,(SELECT min(b.date)  FROM TSPL_Exe_Deployment b WHERE b.Date > a.Date   ) as To_Date  from TSPL_Exe_Deployment a ) SecondTable on SecondTable.SNO = FirstTable.SNO " & _
                    ' " where convert (datetime ,FirstTable.Document_Date) between convert(datetime,SecondTable.From_Date) and convert(datetime,SecondTable.To_Date) " & _
                    ' " order by convert (datetime,FirstTable.Document_Date) ,FirstTable.Document_No "



                    'qry = " SELECT TSPL_DEDUCTION.POSTED as Status,TSPL_DEDUCTION.DEDUCTION_CODE as [Document Id],TSPL_DEDUCTION.PAY_PERIOD_CODE as [Pay Period Code], " & _
                    '      " TSPL_DEDUCTION.DEDUCTION_DATE as [Document Date],TSPL_DEDUCTION.EMP_CODE as [Employee Code],TSPL_DEDUCTION.DEDUCTION_REMARKS as [Description], " & _
                    '      " SUM(isnull(TSPL_DEDUCTION_DETAIL.DEDUCTION_AMOUNT ,0)) as Amount,TSPL_DEDUCTION.Created_By as [Created By],TSPL_DEDUCTION.Created_Date as [Created Date] FROM TSPL_DEDUCTION LEFT OUTER JOIN TSPL_DEDUCTION_DETAIL ON TSPL_DEDUCTION.DEDUCTION_CODE=TSPL_DEDUCTION_DETAIL.DEDUCTION_CODE " & _
                    '      " WHERE  convert(date,TSPL_DEDUCTION.Created_Date ,103) >= convert(date,'" + dtpFromDate.Value + "',103) " & _
                    '      " and convert(date,TSPL_DEDUCTION.Created_Date,103) <= convert(date,'" + dtpToDate.Value + "',103) "



                    'qry += " GROUP BY TSPL_DEDUCTION.DEDUCTION_CODE,TSPL_DEDUCTION.POSTED,TSPL_DEDUCTION.PAY_PERIOD_CODE,TSPL_DEDUCTION.DEDUCTION_DATE, TSPL_DEDUCTION.EMP_CODE,TSPL_DEDUCTION.DEDUCTION_REMARKS,TSPL_DEDUCTION.Created_By,TSPL_DEDUCTION.Created_Date"
                    'qry += " ORDER BY TSPL_DEDUCTION.DEDUCTION_DATE, TSPL_DEDUCTION.DEDUCTION_CODE "
                End If
            End If

            '' fill Employee Bonus
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), "Bonus") = CompairStringResult.Equal Then
            Load_Authorisation("Bonus")
            If dtAuthen.Rows.Count > 0 Then
                If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then
                    gv1.DataSource = Nothing
                    qry = GetQueryByScreen("TSPL_EMPLOYEE_BONUS", "EMP_BONUS_CODE", "Created_Date", "Created_By", "Created_Date", "Modified_By", "Modified_Date", "Posting_Date", " ")

                    'qry = " select FirstTable.Document_No  ,FirstTable.Document_Date ,SecondTable.Version_No as Exe_Version ,FirstTable.Created_By as Created_By ,convert(varchar, FirstTable.Created_Date,103)   as Created_Date , FirstTable.Modify_By  ,convert (varchar,FirstTable.Modify_Date,103) as Modify_Date   from " & _
                    '  " (Select 1 as SNO, TSPL_EMPLOYEE_BONUS.EMP_BONUS_CODE as  Document_No ,TSPL_EMPLOYEE_BONUS.Created_Date  as Document_Date,TSPL_EMPLOYEE_BONUS.Created_By ,TSPL_EMPLOYEE_BONUS.Created_Date,TSPL_EMPLOYEE_BONUS.Modify_By,TSPL_EMPLOYEE_BONUS.Modify_Date   from TSPL_EMPLOYEE_BONUS " & _
                    '  "	where convert(date,TSPL_EMPLOYEE_BONUS.Created_Date  ,103) >= convert(date,'" + dtpFromDate.Value + "',103) and convert(date,TSPL_EMPLOYEE_BONUS.Created_Date  ,103) <= convert(date,'" + dtpToDate.Value + "',103)    ) as FirstTable left outer join  " & _
                    '  " (select 1 as  SNO, a.Version_No , a.Date as From_Date ,(SELECT min(b.date)  FROM TSPL_Exe_Deployment b WHERE b.Date > a.Date   ) as To_Date  from TSPL_Exe_Deployment a ) SecondTable on SecondTable.SNO = FirstTable.SNO " & _
                    '  " where convert (datetime ,FirstTable.Document_Date) between convert(datetime,SecondTable.From_Date) and convert(datetime,SecondTable.To_Date) " & _
                    '  " order by convert (datetime,FirstTable.Document_Date) ,FirstTable.Document_No "

                    'qry = " SELECT TSPL_EMPLOYEE_BONUS.POSTED as Status,TSPL_EMPLOYEE_BONUS.EMP_BONUS_CODE as [Document Id],TSPL_EMPLOYEE_BONUS.FROM_PAY_PERIOD_CODE as [From Pay Period],TSPL_EMPLOYEE_BONUS.TO_PAY_PERIOD_CODE as [To Pay Period], " & _
                    '      " TSPL_EMPLOYEE_BONUS.PAYABLE_PAY_PERIOD_CODE as [Payable Pay Period],TSPL_EMPLOYEE_BONUS.Created_Date as [Document Date],TSPL_EMPLOYEE_BONUS.DESCRIPTION as [Description], SUM(isnull(TSPL_BONUS_GENERATION_DETAIL.ACTUAL_AMOUNT  ,0)) as Amount," & _
                    '      " TSPL_EMPLOYEE_BONUS.Created_By as [Created By],TSPL_EMPLOYEE_BONUS.Created_Date as [Created Date] FROM TSPL_EMPLOYEE_BONUS LEFT OUTER JOIN TSPL_BONUS_GENERATION_DETAIL ON TSPL_EMPLOYEE_BONUS.EMP_BONUS_CODE=TSPL_BONUS_GENERATION_DETAIL.EMP_BONUS_CODE" & _
                    '      " WHERE  convert(date,TSPL_EMPLOYEE_BONUS.Created_Date ,103) >= convert(date,'" + dtpFromDate.Value + "',103) " & _
                    '      " and convert(date,TSPL_EMPLOYEE_BONUS.Created_Date,103) <= convert(date,'" + dtpToDate.Value + "',103) "


                    'qry += " GROUP BY TSPL_EMPLOYEE_BONUS.EMP_BONUS_CODE,TSPL_EMPLOYEE_BONUS.POSTED,TSPL_EMPLOYEE_BONUS.FROM_PAY_PERIOD_CODE,TSPL_EMPLOYEE_BONUS.TO_PAY_PERIOD_CODE, TSPL_EMPLOYEE_BONUS.PAYABLE_PAY_PERIOD_CODE,TSPL_EMPLOYEE_BONUS.Created_Date,TSPL_EMPLOYEE_BONUS.DESCRIPTION,TSPL_EMPLOYEE_BONUS.Created_By"
                    'qry += " ORDER BY TSPL_EMPLOYEE_BONUS.Created_Date, TSPL_EMPLOYEE_BONUS.EMP_BONUS_CODE "
                End If
            End If

            '' fill Employee Salary Adjustment
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), "Salary Adjustment") = CompairStringResult.Equal Then

            Load_Authorisation("Salary Adjustment")
            If dtAuthen.Rows.Count > 0 Then
                If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then
                    gv1.DataSource = Nothing

                    qry = GetQueryByScreen("TSPL_ADJUSTMENT_VOUCHER", "ADJUSTMENT_CODE", "ADJUSTMENT_DATE", "Created_By", "Created_Date", "Modified_By", "Modified_Date", "Posting_Date", " ")


                    'qry = " SELECT TSPL_ADJUSTMENT_VOUCHER.POSTED as Status,TSPL_ADJUSTMENT_VOUCHER.ADJUSTMENT_CODE as [Document Id],TSPL_ADJUSTMENT_VOUCHER.PAY_PERIOD_CODE as [Pay Period Code], " & _
                    '      " TSPL_ADJUSTMENT_VOUCHER.ADJUSTMENT_DATE as [Document Date],TSPL_ADJUSTMENT_VOUCHER.EMP_CODE as [Employee Code], " & _
                    '      " TSPL_ADJUSTMENT_VOUCHER.ADJUSTMENT_REMARK as [Description], SUM(isnull(TSPL_EMPADJUSTMENT_DETAIL.ADJUSTMENT_PLUS ,0)+isnull(TSPL_EMPADJUSTMENT_DETAIL.ADJUSTMENT_MINUS ,0)) as Amount," & _
                    '      " TSPL_ADJUSTMENT_VOUCHER.Created_By as [Created By],TSPL_ADJUSTMENT_VOUCHER.Created_Date as [Created Date] FROM TSPL_ADJUSTMENT_VOUCHER LEFT OUTER JOIN TSPL_EMPADJUSTMENT_DETAIL ON TSPL_ADJUSTMENT_VOUCHER.ADJUSTMENT_CODE=TSPL_EMPADJUSTMENT_DETAIL.ADJUSTMENT_CODE" & _
                    '      " WHERE  convert(date,TSPL_ADJUSTMENT_VOUCHER.ADJUSTMENT_DATE ,103) >= convert(date,'" + dtpFromDate.Value + "',103) " & _
                    '      " and convert(date,TSPL_ADJUSTMENT_VOUCHER.ADJUSTMENT_DATE,103) <= convert(date,'" + dtpToDate.Value + "',103) "



                    'qry += " GROUP BY TSPL_ADJUSTMENT_VOUCHER.ADJUSTMENT_CODE,TSPL_ADJUSTMENT_VOUCHER.POSTED,TSPL_ADJUSTMENT_VOUCHER.PAY_PERIOD_CODE, TSPL_ADJUSTMENT_VOUCHER.ADJUSTMENT_DATE, tSPL_ADJUSTMENT_VOUCHER.EMP_CODE, TSPL_ADJUSTMENT_VOUCHER.ADJUSTMENT_REMARK, TSPL_ADJUSTMENT_VOUCHER.Created_By, TSPL_ADJUSTMENT_VOUCHER.Created_Date"
                    'qry += " ORDER BY TSPL_ADJUSTMENT_VOUCHER.ADJUSTMENT_DATE, TSPL_ADJUSTMENT_VOUCHER.ADJUSTMENT_CODE "
                End If
            End If

            '' fill Employee Reimbursement
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), "Reimbursement") = CompairStringResult.Equal Then
            Load_Authorisation("Reimbursement")
            If dtAuthen.Rows.Count > 0 Then
                If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then
                    gv1.DataSource = Nothing
                    qry = GetQueryByScreen("TSPL_EMP_REIMBURSEMENT", "REIMBURSEMENT_CODE", "REIMBURSEMENT_DATE", "Created_By", "Created_Date", "Modified_By", "Modified_Date", "Posting_Date", " ")


                    'qry = " SELECT POSTED as Status,REIMBURSEMENT_CODE as [Document Id],PAY_PERIOD_CODE as [Pay Period Code], " & _
                    '      " TSPL_EMP_REIMBURSEMENT.REIMBURSEMENT_DATE as [Document Date],TSPL_EMP_REIMBURSEMENT.EMP_CODE as [Employee Code], " & _
                    '      " isnull(TSPL_EMP_REIMBURSEMENT.REIMBURSEMENT_AMOUNT,0) as [Amount],TSPL_EMP_REIMBURSEMENT.REIMBURSEMENT_REMARK as [Description], " & _
                    '      " TSPL_EMP_REIMBURSEMENT.Created_By as [Created By],TSPL_EMP_REIMBURSEMENT.Created_Date as [Created Date] FROM TSPL_EMP_REIMBURSEMENT " & _
                    '      " WHERE  convert(date,TSPL_EMP_REIMBURSEMENT.REIMBURSEMENT_DATE ,103) >= convert(date,'" + dtpFromDate.Value + "',103) " & _
                    '      " and convert(date,TSPL_EMP_REIMBURSEMENT.REIMBURSEMENT_DATE,103) <= convert(date,'" + dtpToDate.Value + "',103) "



                    'qry += " ORDER BY TSPL_EMP_REIMBURSEMENT.REIMBURSEMENT_DATE, TSPL_EMP_REIMBURSEMENT.REIMBURSEMENT_CODE "

                End If
            End If
            '' fill Employee Loan Application
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), "Loan Application") = CompairStringResult.Equal Then
            Load_Authorisation("Loan Application")
            If dtAuthen.Rows.Count > 0 Then
                If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then
                    gv1.DataSource = Nothing

                    qry = GetQueryByScreen("TSPL_LOAN_APPLICATION", "LOAN_CODE", "LOAN_DATE", "Created_By", "Created_Date", "Modified_By", "Modified_Date", "Posting_Date", " ")

                    'qry = " SELECT POSTED as Status,LOAN_CODE as [Document Id],TSPL_LOAN_APPLICATION.LOAN_DATE as [Document Date],TSPL_LOAN_APPLICATION.EMP_CODE as [Employee Code], " & _
                    '      " TSPL_LOAN_APPLICATION.LOAN_TYPE as [Loan Type],isnull(TSPL_LOAN_APPLICATION.LOAN_AMOUNT,0) as [Amount], " & _
                    '      " TSPL_LOAN_APPLICATION.PAYMENT_STARTDATE as [Payment Start Date],TSPL_LOAN_APPLICATION.NO_OF_EMI as [No of EMI], " & _
                    '      " TSPL_LOAN_APPLICATION.INTEREST_APPLIED as [Interest Applied],TSPL_LOAN_APPLICATION.INTEREST_TYPE as [Interest Type], " & _
                    '      " TSPL_LOAN_APPLICATION.INTEREST_RATE as [Interest Rate],isnull(TSPL_LOAN_APPLICATION.INTEREST_AMOUNT,0) as [Interest Amount]," & _
                    '      " isnull(TSPL_LOAN_APPLICATION.TOTALPAYABLE_AMOUNT,0) as [Total Payable Amount],TSPL_LOAN_APPLICATION.LOAN_DESCRIPTION as [Description]," & _
                    '      " TSPL_LOAN_APPLICATION.Created_By as [Created By],TSPL_LOAN_APPLICATION.Created_Date as [Created Date] FROM TSPL_LOAN_APPLICATION " & _
                    '      " WHERE  convert(date,TSPL_LOAN_APPLICATION.LOAN_DATE ,103) >= convert(date,'" + dtpFromDate.Value + "',103) " & _
                    '      " and convert(date,TSPL_LOAN_APPLICATION.LOAN_DATE,103) <= convert(date,'" + dtpToDate.Value + "',103) "



                    'qry += " ORDER BY TSPL_LOAN_APPLICATION.LOAN_DATE, TSPL_LOAN_APPLICATION.LOAN_CODE "

                End If
            End If

            '' fill Employee Loan Adjustment
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), "Loan Adjustment") = CompairStringResult.Equal Then

            Load_Authorisation("Loan Adjustment")
            If dtAuthen.Rows.Count > 0 Then
                If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then
                    gv1.DataSource = Nothing
                    qry = GetQueryByScreen("TSPL_LOAN_ADJUSTMENT", "LOANADJUSTMENT_CODE", "ADJUSTMENT_DATE", "Created_By", "Created_Date", "Modified_By", "Modified_Date", "Posting_Date", " ")

                    'qry = " SELECT POSTED as Status,LOANADJUSTMENT_CODE as [Document Id],TSPL_LOAN_ADJUSTMENT.ADJUSTMENT_DATE as [Document Date], " & _
                    '      " TSPL_LOAN_ADJUSTMENT.EMP_CODE as [Employee Code],TSPL_LOAN_ADJUSTMENT.PAY_PERIOD_CODE as [Pay Period], " & _
                    '      " isnull(TSPL_LOAN_ADJUSTMENT.ADJUSTMENT_PLUS,0) as [Adjustment Plus],isnull(TSPL_LOAN_ADJUSTMENT.ADJUSTMENT_MINUS,0) as [Adjustment Minus], " & _
                    '      " TSPL_LOAN_ADJUSTMENT.ADJUSTMENT_REASON as [Description], " & _
                    '      " TSPL_LOAN_ADJUSTMENT.Created_By as [Created By],TSPL_LOAN_ADJUSTMENT.Created_Date as [Created Date] FROM TSPL_LOAN_ADJUSTMENT " & _
                    '      " WHERE  convert(date,TSPL_LOAN_ADJUSTMENT.ADJUSTMENT_DATE ,103) >= convert(date,'" + dtpFromDate.Value + "',103) " & _
                    '      " and convert(date,TSPL_LOAN_ADJUSTMENT.ADJUSTMENT_DATE,103) <= convert(date,'" + dtpToDate.Value + "',103) "


                    'qry += " ORDER BY TSPL_LOAN_ADJUSTMENT.ADJUSTMENT_DATE, TSPL_LOAN_ADJUSTMENT.LOANADJUSTMENT_CODE "

                End If
            End If

            '' fill Employee Leave Application
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), "Leave Application") = CompairStringResult.Equal Then
            Load_Authorisation("Leave Application")
            If dtAuthen.Rows.Count > 0 Then
                If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then
                    gv1.DataSource = Nothing

                    qry = GetQueryByScreen("TSPL_LEAVE_APPLICATION", "LVAPPLICATION_CODE", "APPLICATION_DATE", "Created_By", "Created_Date", "Modified_By", "Modified_Date", "Posting_Date", " ")


                    'qry = " SELECT POSTED as Status,LVAPPLICATION_CODE as [Document Id],TSPL_LEAVE_APPLICATION.APPLICATION_DATE as [Document Date], " & _
                    '      " TSPL_LEAVE_APPLICATION.EMP_CODE as [Employee Code],TSPL_LEAVE_APPLICATION.PAY_PERIOD_CODE as [Pay Period], " & _
                    '      " TSPL_LEAVE_APPLICATION.LEAVE_CODE as [Leave Code],TSPL_LEAVE_APPLICATION.FROM_DATE as [From Date],TSPL_LEAVE_APPLICATION.TO_DATE as [To Date], " & _
                    '      " TSPL_LEAVE_APPLICATION.TOTAL_DAYS as [Total Days],TSPL_LEAVE_APPLICATION.LEAVE_REASON as [Description], " & _
                    '      " TSPL_LEAVE_APPLICATION.Created_By as [Created By],TSPL_LEAVE_APPLICATION.Created_Date as [Created Date] FROM TSPL_LEAVE_APPLICATION  " & _
                    '      " WHERE  convert(date,TSPL_LEAVE_APPLICATION.APPLICATION_DATE ,103) >= convert(date,'" + dtpFromDate.Value + "',103) " & _
                    '      " and convert(date,TSPL_LEAVE_APPLICATION.APPLICATION_DATE,103) <= convert(date,'" + dtpToDate.Value + "',103) "


                    'qry += " ORDER BY TSPL_LEAVE_APPLICATION.APPLICATION_DATE, TSPL_LEAVE_APPLICATION.LVAPPLICATION_CODE "

                End If
            End If

            '' fill Employee Leave Adjustment
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), "Leave Adjustment") = CompairStringResult.Equal Then
            Load_Authorisation("Leave Adjustment")
            If dtAuthen.Rows.Count > 0 Then
                If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then
                    gv1.DataSource = Nothing

                    qry = GetQueryByScreen("TSPL_LEAVE_ADJUSTMENT", "LVADJUSTMENT_CODE", "ADJUSTMENT_DATE", "Created_By", "Created_Date", "Modified_By", "Modified_Date", "Posting_Date", " ")


                    'qry = " SELECT POSTED as Status,LVADJUSTMENT_CODE as [Document Id],TSPL_LEAVE_ADJUSTMENT.ADJUSTMENT_DATE as [Document Date], " & _
                    '      " TSPL_LEAVE_ADJUSTMENT.EMP_CODE as [Employee Code],TSPL_LEAVE_ADJUSTMENT.PAY_PERIOD_CODE as [Pay Period], " & _
                    '      " TSPL_LEAVE_ADJUSTMENT.LEAVE_CODE as [Leave Code],TSPL_LEAVE_ADJUSTMENT.ADJUST_ALLOTED as [Adjustment in Alloted], " & _
                    '      " TSPL_LEAVE_ADJUSTMENT.ADJUST_AVAILED as [Adjustment in Availed],TSPL_LEAVE_ADJUSTMENT.LEAVE_REASON as [Description], " & _
                    '      " TSPL_LEAVE_ADJUSTMENT.Created_By as [Created By],TSPL_LEAVE_ADJUSTMENT.Created_Date as [Created Date] FROM TSPL_LEAVE_ADJUSTMENT " & _
                    '      " WHERE  convert(date,TSPL_LEAVE_ADJUSTMENT.ADJUSTMENT_DATE ,103) >= convert(date,'" + dtpFromDate.Value + "',103) " & _
                    '      " and convert(date,TSPL_LEAVE_ADJUSTMENT.ADJUSTMENT_DATE,103) <= convert(date,'" + dtpToDate.Value + "',103) "


                    'qry += " ORDER BY TSPL_LEAVE_ADJUSTMENT.ADJUSTMENT_DATE, TSPL_LEAVE_ADJUSTMENT.LVADJUSTMENT_CODE "
                End If
            End If
            '==Shivani Tyagi===against[BM00000007827]
            '' fill Employee Increment
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), "Employee Increment") = CompairStringResult.Equal Then
            Load_Authorisation("Employee Increment")
            If dtAuthen.Rows.Count > 0 Then
                If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then
                    gv1.DataSource = Nothing

                    qry = GetQueryByScreen("TSPL_EMPLOYEE_INCREMENT_HEAD", "INCREMENT_CODE", "INCREMENT_DATE", "Created_By", "Created_Date", "Modified_By", "Modified_Date", "Posting_Date", " ")

                    'qry = "select TSPL_EMPLOYEE_INCREMENT_HEAD.POSTED as status ,TSPL_EMPLOYEE_INCREMENT_HEAD.INCREMENT_CODE as [Document Id] ,convert(varchar,TSPL_EMPLOYEE_INCREMENT_HEAD.INCREMENT_DATE,103) as [Document Date],'' as [Description],TSPL_EMPLOYEE_INCREMENT_HEAD.EMP_CODE as [Employee Code],TSPL_EMPLOYEE_MASTER.Emp_Name as [Employee Name] ,TSPL_EMPLOYEE_INCREMENT_HEAD.REVISION_NO as [Revision No],TSPL_EMPLOYEE_INCREMENT_HEAD.SALARY_STRUCTURE_CODE as [Salary Structure],SUM(convert(decimal,isnull(TSPL_EMPLOYEE_INCREMENT_DETAIL.IncrementAmt,0))) AS Amount,TSPL_EMPLOYEE_INCREMENT_HEAD.Created_By as [Created By] ,convert(varchar,TSPL_EMPLOYEE_INCREMENT_HEAD.Created_Date,103) as [Created Date]  from TSPL_EMPLOYEE_INCREMENT_HEAD left join tspl_employee_master on tspl_employee_master.EMP_CODE =TSPL_EMPLOYEE_INCREMENT_HEAD.EMP_CODE LEFT OUTER JOIN TSPL_EMPLOYEE_INCREMENT_DETAIL ON TSPL_EMPLOYEE_INCREMENT_HEAD.INCREMENT_CODE=TSPL_EMPLOYEE_INCREMENT_DETAIL.INCREMENT_CODE  " & _
                    '      " WHERE  convert(date,TSPL_EMPLOYEE_INCREMENT_HEAD.INCREMENT_DATE ,103) >= convert(date,'" + dtpFromDate.Value + "',103) and convert(date,TSPL_EMPLOYEE_INCREMENT_HEAD.INCREMENT_DATE,103) <= convert(date,'" + dtpToDate.Value + "',103)"


                    'qry += " GROUP BY TSPL_EMPLOYEE_INCREMENT_HEAD.INCREMENT_CODE,TSPL_EMPLOYEE_INCREMENT_HEAD.POSTED,TSPL_EMPLOYEE_INCREMENT_HEAD.INCREMENT_DATE, TSPL_EMPLOYEE_INCREMENT_HEAD.EMP_CODE,TSPL_EMPLOYEE_MASTER.Emp_Name,TSPL_EMPLOYEE_INCREMENT_HEAD.REVISION_NO,TSPL_EMPLOYEE_INCREMENT_HEAD.SALARY_STRUCTURE_CODE,TSPL_EMPLOYEE_INCREMENT_HEAD.Created_By,TSPL_EMPLOYEE_INCREMENT_HEAD.Created_Date"
                    'qry += " ORDER BY TSPL_EMPLOYEE_INCREMENT_HEAD.INCREMENT_DATE, TSPL_EMPLOYEE_INCREMENT_HEAD.INCREMENT_CODE "
                End If
            End If
        End If

        If clsCommon.CompairString(clsCommon.myCstr(qry), Nothing) <> CompairStringResult.Equal Then

            dt = clsDBFuncationality.GetDataTable(qry)
            gv1.DataSource = dt
            gv1.MasterTemplate.SummaryRowsBottom.Clear()
            'gv1Format()
            SetGridFormationOFgv()


        End If
        '-------------------------------------Code Ends Here--------------------------------------------
    End Sub

    Sub FillProduction()
        '' fill Employee Salary
        If clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), "Batch Order") = CompairStringResult.Equal Then
            Load_Authorisation(clsUserMgtCode.frmBatchOrderSTD)
            If dtAuthen.Rows.Count > 0 Then
                If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then
                    gv1.DataSource = Nothing
                    qry = GetQueryByScreen("TSPL_MF_BATCH_ORDER", "BO_CODE", "BO_Date", "Created_By", "Created_Date", "Modified_By", "Modified_Date", "Posting_Date", " ")

                    'qry = " select FirstTable.Document_No  ,FirstTable.Document_Date ,SecondTable.Version_No as Exe_Version ,FirstTable.Created_By as Created_By ,convert(varchar, FirstTable.Created_Date,103)   as Created_Date , FirstTable.Modify_By  ,convert (varchar,FirstTable.Modify_Date,103) as Modify_Date   from " & _
                    '  " (Select 1 as SNO, TSPL_MF_BATCH_ORDER.BO_CODE as  Document_No ,TSPL_MF_BATCH_ORDER.BO_Date  as Document_Date,TSPL_MF_BATCH_ORDER.Created_By ,TSPL_MF_BATCH_ORDER.Created_Date,TSPL_MF_BATCH_ORDER.Modify_By,TSPL_MF_BATCH_ORDER.Modify_Date   from TSPL_MF_BATCH_ORDER " & _
                    '  "	where convert(date,TSPL_MF_BATCH_ORDER.BO_Date  ,103) >= convert(date,'" + dtpFromDate.Value + "',103) and convert(date,TSPL_MF_BATCH_ORDER.BO_Date  ,103) <= convert(date,'" + dtpToDate.Value + "',103)    ) as FirstTable left outer join  " & _
                    '  " (select 1 as  SNO, a.Version_No , a.Date as From_Date ,(SELECT min(b.date)  FROM TSPL_Exe_Deployment b WHERE b.Date > a.Date   ) as To_Date  from TSPL_Exe_Deployment a ) SecondTable on SecondTable.SNO = FirstTable.SNO " & _
                    '  " where convert (datetime ,FirstTable.Document_Date) between convert(datetime,SecondTable.From_Date) and convert(datetime,SecondTable.To_Date) " & _
                    '  " order by convert (datetime,FirstTable.Document_Date) ,FirstTable.Document_No "




                    'qry = " Select POSTED as Status,BO_CODE as[Document Id],BO_Date as [Document Date], Description," & _
                    '       "APPROVED_BY [Approved By],Created_By as [Created By],Created_Date as [Created Date] " & _
                    '       "from TSPL_MF_BATCH_ORDER" & _
                    '      " WHERE  convert(date,BO_Date ,103) >= convert(date,'" + dtpFromDate.Value + "',103) and convert(date,BO_Date,103) <= convert(date,'" + dtpToDate.Value + "',103)"


                    'qry += " ORDER BY TSPL_MF_BATCH_ORDER.BO_Date, TSPL_MF_BATCH_ORDER.BO_CODE "

                End If
            End If
            '' fill Employee Hourly Attendance
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), "Bill Of Material") = CompairStringResult.Equal Then
            Load_Authorisation(clsUserMgtCode.frmBillOfMaterialCosting)
            If dtAuthen.Rows.Count > 0 Then
                If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then
                    gv1.DataSource = Nothing

                    qry = GetQueryByScreen("TSPL_MF_BOM_HEAD", "BOM_CODE", "BOM_Date", "Created_By", "Created_Date", "Modified_By", "Modified_Date", "Posting_Date", " ")


                    'qry = " select FirstTable.Document_No  ,FirstTable.Document_Date ,SecondTable.Version_No as Exe_Version ,FirstTable.Created_By as Created_By ,convert(varchar, FirstTable.Created_Date,103)   as Created_Date , FirstTable.Modify_By  ,convert (varchar,FirstTable.Modify_Date,103) as Modify_Date   from " & _
                    ' " (Select 1 as SNO, TSPL_MF_BOM_HEAD.BOM_CODE as  Document_No ,TSPL_MF_BOM_HEAD.BOM_Date  as Document_Date,TSPL_MF_BOM_HEAD.Created_By ,TSPL_MF_BOM_HEAD.Created_Date,TSPL_MF_BOM_HEAD.Modify_By,TSPL_MF_BOM_HEAD.Modify_Date   from TSPL_MF_BOM_HEAD " & _
                    ' "	where convert(date,TSPL_MF_BOM_HEAD.BOM_Date  ,103) >= convert(date,'" + dtpFromDate.Value + "',103) and convert(date,TSPL_MF_BOM_HEAD.BOM_Date  ,103) <= convert(date,'" + dtpToDate.Value + "',103)    ) as FirstTable left outer join  " & _
                    ' " (select 1 as  SNO, a.Version_No , a.Date as From_Date ,(SELECT min(b.date)  FROM TSPL_Exe_Deployment b WHERE b.Date > a.Date   ) as To_Date  from TSPL_Exe_Deployment a ) SecondTable on SecondTable.SNO = FirstTable.SNO " & _
                    ' " where convert (datetime ,FirstTable.Document_Date) between convert(datetime,SecondTable.From_Date) and convert(datetime,SecondTable.To_Date) " & _
                    ' " order by convert (datetime,FirstTable.Document_Date) ,FirstTable.Document_No "

                    'qry = " select POSTED as Status,BOM_CODE as[Document Id],BOM_Date as [Document Date],'' as Description," & _
                    '      " REVISION_NO [Revision No],PROD_ITEM_CODE as [Item Code],Prod_quantity as [Quatinty]," & _
                    '      " prod_item_unit_code as [Item Unit Code]," & _
                    '      " Created_By as [Created By],Created_Date as [Created Date]" & _
                    '      " from TSPL_MF_BOM_HEAD " & _
                    '      " WHERE  convert(date,TSPL_MF_BOM_HEAD.BOM_Date ,103) >= convert(date,'" + dtpFromDate.Value + "',103) " & _
                    '      " and convert(date,TSPL_MF_BOM_HEAD.BOM_Date,103) <= convert(date,'" + dtpToDate.Value + "',103) "


                    'qry += " ORDER BY TSPL_MF_BOM_HEAD.Created_Date, TSPL_MF_BOM_HEAD.BOM_CODE "

                End If
            End If

            '' fill Employee Daily Attendance
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), "Manufacturing Order") = CompairStringResult.Equal Then
            Load_Authorisation(clsUserMgtCode.frmManufacturingOrder)
            If dtAuthen.Rows.Count > 0 Then
                If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then
                    gv1.DataSource = Nothing

                    qry = GetQueryByScreen("TSPL_MF_MANUFACTURING_ORDER", "MO_CODE", "MO_DATE", "Created_By", "Created_Date", "Modified_By", "Modified_Date", "Posting_Date", " ")

                    '  qry = " select FirstTable.Document_No  ,FirstTable.Document_Date ,SecondTable.Version_No as Exe_Version ,FirstTable.Created_By as Created_By ,convert(varchar, FirstTable.Created_Date,103)   as Created_Date , FirstTable.Modify_By  ,convert (varchar,FirstTable.Modify_Date,103) as Modify_Date   from " & _
                    '" (Select 1 as SNO, TSPL_MF_MANUFACTURING_ORDER.MO_CODE as  Document_No ,TSPL_MF_MANUFACTURING_ORDER.MO_DATE  as Document_Date,TSPL_MF_MANUFACTURING_ORDER.Created_By ,TSPL_MF_MANUFACTURING_ORDER.Created_Date,TSPL_MF_MANUFACTURING_ORDER.Modify_By,TSPL_MF_MANUFACTURING_ORDER.Modify_Date   from TSPL_MF_MANUFACTURING_ORDER " & _
                    '"	where convert(date,TSPL_MF_MANUFACTURING_ORDER.MO_DATE  ,103) >= convert(date,'" + dtpFromDate.Value + "',103) and convert(date,TSPL_MF_MANUFACTURING_ORDER.MO_DATE  ,103) <= convert(date,'" + dtpToDate.Value + "',103)    ) as FirstTable left outer join  " & _
                    '" (select 1 as  SNO, a.Version_No , a.Date as From_Date ,(SELECT min(b.date)  FROM TSPL_Exe_Deployment b WHERE b.Date > a.Date   ) as To_Date  from TSPL_Exe_Deployment a ) SecondTable on SecondTable.SNO = FirstTable.SNO " & _
                    '" where convert (datetime ,FirstTable.Document_Date) between convert(datetime,SecondTable.From_Date) and convert(datetime,SecondTable.To_Date) " & _
                    '" order by convert (datetime,FirstTable.Document_Date) ,FirstTable.Document_No "

                    'qry = "  select POSTED as Status,MO_CODE as[Document Id],MO_DATE as [Document Date],'' as Description," & _
                    '      " ITEM_CODE as [Item Code],QTY_ORDERED as [Qty Ordered],UNIT_CODE as [Unit Code], " & _
                    '      " QTY_ORDERED_STOCK as [Qty Ordered Stock],UNIT_CODE_STOCK as [Unit Code Stock], " & _
                    '      " Description,BOM_CODE as [Batch of Material Code],MO_DUE_DATE as [MO Due Date], " & _
                    '      " PRODUCTION_AREA as [Production Area],PLANNER Planner,IN_CHARGE as [In Charge], " & _
                    '      " Created_By as [Created By],Created_Date as [Created Date]  " & _
                    '      " from TSPL_MF_MANUFACTURING_ORDER " & _
                    '      " WHERE  convert(date,TSPL_MF_MANUFACTURING_ORDER.MO_DATE ,103) >= convert(date,'" + dtpFromDate.Value + "',103) " & _
                    '      " and convert(date,TSPL_MF_MANUFACTURING_ORDER.MO_DATE,103) <= convert(date,'" + dtpToDate.Value + "',103) "


                    'qry += " ORDER BY TSPL_MF_MANUFACTURING_ORDER.Created_Date, TSPL_MF_MANUFACTURING_ORDER.MO_CODE "

                End If
            End If

            '' fill Employee Monthly Attendance
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), "Production Planning") = CompairStringResult.Equal Then
            Load_Authorisation(clsUserMgtCode.frmProductionPlanningSTD)
            If dtAuthen.Rows.Count > 0 Then
                If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then
                    gv1.DataSource = Nothing

                    qry = GetQueryByScreen("TSPL_MF_PRODUCTION_PLAN_HEAD", "PROD_PLAN_CODE", "PLAN_FOR_DATE", "Created_By", "Created_Date", "Modified_By", "Modified_Date", "Posting_Date", " ")


                    'qry = " select FirstTable.Document_No  ,FirstTable.Document_Date ,SecondTable.Version_No as Exe_Version ,FirstTable.Created_By as Created_By ,convert(varchar, FirstTable.Created_Date,103)   as Created_Date , FirstTable.Modify_By  ,convert (varchar,FirstTable.Modify_Date,103) as Modify_Date   from " & _
                    ' " (Select 1 as SNO, TSPL_MF_PRODUCTION_PLAN_HEAD.PROD_PLAN_CODE as  Document_No ,TSPL_MF_PRODUCTION_PLAN_HEAD.PLAN_FOR_DATE  as Document_Date,TSPL_MF_PRODUCTION_PLAN_HEAD.Created_By ,TSPL_MF_PRODUCTION_PLAN_HEAD.Created_Date,TSPL_MF_PRODUCTION_PLAN_HEAD.Modify_By,TSPL_MF_PRODUCTION_PLAN_HEAD.Modify_Date   from TSPL_MF_PRODUCTION_PLAN_HEAD " & _
                    ' "	where convert(date,TSPL_MF_PRODUCTION_PLAN_HEAD.PLAN_FOR_DATE  ,103) >= convert(date,'" + dtpFromDate.Value + "',103) and convert(date,TSPL_MF_PRODUCTION_PLAN_HEAD.PLAN_FOR_DATE  ,103) <= convert(date,'" + dtpToDate.Value + "',103)    ) as FirstTable left outer join  " & _
                    ' " (select 1 as  SNO, a.Version_No , a.Date as From_Date ,(SELECT min(b.date)  FROM TSPL_Exe_Deployment b WHERE b.Date > a.Date   ) as To_Date  from TSPL_Exe_Deployment a ) SecondTable on SecondTable.SNO = FirstTable.SNO " & _
                    ' " where convert (datetime ,FirstTable.Document_Date) between convert(datetime,SecondTable.From_Date) and convert(datetime,SecondTable.To_Date) " & _
                    ' " order by convert (datetime,FirstTable.Document_Date) ,FirstTable.Document_No "




                    'qry = " select POSTED as Status,PROD_PLAN_CODE as[Document Id],PLAN_FOR_DATE as [Document Date]," & _
                    '      " DESCRIPTION as [Description],COMMENTS as [Comments]," & _
                    '      " Created_By as [Created By],Created_Date as [Created Date] " & _
                    '      " from TSPL_MF_PRODUCTION_PLAN_HEAD " & _
                    '      " WHERE  convert(date,TSPL_MF_PRODUCTION_PLAN_HEAD.Created_Date ,103) >= convert(date,'" + dtpFromDate.Value + "',103) " & _
                    '      " and convert(date,TSPL_MF_PRODUCTION_PLAN_HEAD.Created_Date,103) <= convert(date,'" + dtpToDate.Value + "',103) "


                    'qry += " ORDER BY TSPL_MF_PRODUCTION_PLAN_HEAD.Created_Date, TSPL_MF_PRODUCTION_PLAN_HEAD.PROD_PLAN_CODE "

                End If
            End If

            '' fill Employee Allowance
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), "Production Receipt") = CompairStringResult.Equal Then
            Load_Authorisation(clsUserMgtCode.frmProductionReceiptSTD)
            If dtAuthen.Rows.Count > 0 Then
                If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then
                    gv1.DataSource = Nothing

                    qry = GetQueryByScreen("TSPL_MF_RECEIPT", "RECEIPT_CODE", "RECEIPT_DATE", "Created_By", "Created_Date", "Modified_By", "Modified_Date", "Posting_Date", " ")

                    '  qry = " select FirstTable.Document_No  ,FirstTable.Document_Date ,SecondTable.Version_No as Exe_Version ,FirstTable.Created_By as Created_By ,convert(varchar, FirstTable.Created_Date,103)   as Created_Date , FirstTable.Modify_By  ,convert (varchar,FirstTable.Modify_Date,103) as Modify_Date   from " & _
                    '" (Select 1 as SNO, TSPL_MF_RECEIPT.RECEIPT_CODE as  Document_No ,TSPL_MF_RECEIPT.RECEIPT_DATE  as Document_Date,TSPL_MF_RECEIPT.Created_By ,TSPL_MF_RECEIPT.Created_Date,TSPL_MF_RECEIPT.Modify_By,TSPL_MF_RECEIPT.Modify_Date   from TSPL_MF_RECEIPT " & _
                    '"	where convert(date,TSPL_MF_RECEIPT.RECEIPT_DATE  ,103) >= convert(date,'" + dtpFromDate.Value + "',103) and convert(date,TSPL_MF_RECEIPT.RECEIPT_DATE  ,103) <= convert(date,'" + dtpToDate.Value + "',103)    ) as FirstTable left outer join  " & _
                    '" (select 1 as  SNO, a.Version_No , a.Date as From_Date ,(SELECT min(b.date)  FROM TSPL_Exe_Deployment b WHERE b.Date > a.Date   ) as To_Date  from TSPL_Exe_Deployment a ) SecondTable on SecondTable.SNO = FirstTable.SNO " & _
                    '" where convert (datetime ,FirstTable.Document_Date) between convert(datetime,SecondTable.From_Date) and convert(datetime,SecondTable.To_Date) " & _
                    '" order by convert (datetime,FirstTable.Document_Date) ,FirstTable.Document_No "

                    'qry = " select POSTED as Status,RECEIPT_CODE as[Document Id],BATCH_DATE [Document Date]," & _
                    '      " DESCRIPTION as [Description],BO_CODE [BO Code],RECEIPT_DATE [Receipt Date], " & _
                    '      " Received_by [Received By],LOCATION_CODE [Location Code],COMMENTS as [Comments]," & _
                    '      " Created_By as [Created By],Created_Date as [Created Date] " & _
                    '      " from TSPL_MF_RECEIPT " & _
                    '      " WHERE  convert(date,TSPL_MF_RECEIPT.Created_Date ,103) >= convert(date,'" + dtpFromDate.Value + "',103) " & _
                    '      " and convert(date,TSPL_MF_RECEIPT.Created_Date,103) <= convert(date,'" + dtpToDate.Value + "',103) "


                    'qry += " ORDER BY TSPL_MF_RECEIPT.RECEIPT_DATE, TSPL_MF_RECEIPT.RECEIPT_CODE "

                End If
            End If

            '' fill Employee Deduction
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), "Production Requisition") = CompairStringResult.Equal Then
            Load_Authorisation(clsUserMgtCode.frmProductionRequisition)
            If dtAuthen.Rows.Count > 0 Then
                If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then
                    gv1.DataSource = Nothing

                    qry = GetQueryByScreen("TSPL_MF_REQUISITION", "REQ_CODE", "REQ_DATE", "Created_By", "Created_Date", "Modified_By", "Modified_Date", "Posting_Date", " ")

                    'qry = " select FirstTable.Document_No  ,FirstTable.Document_Date ,SecondTable.Version_No as Exe_Version ,FirstTable.Created_By as Created_By ,convert(varchar, FirstTable.Created_Date,103)   as Created_Date , FirstTable.Modify_By  ,convert (varchar,FirstTable.Modify_Date,103) as Modify_Date   from " & _
                    ' " (Select 1 as SNO, TSPL_MF_REQUISITION.REQ_CODE as  Document_No ,TSPL_MF_REQUISITION.REQ_DATE  as Document_Date,TSPL_MF_REQUISITION.Created_By ,TSPL_MF_REQUISITION.Created_Date,TSPL_MF_REQUISITION.Modify_By,TSPL_MF_REQUISITION.Modify_Date   from TSPL_MF_REQUISITION " & _
                    ' "	where convert(date,TSPL_MF_REQUISITION.REQ_DATE  ,103) >= convert(date,'" + dtpFromDate.Value + "',103) and convert(date,TSPL_MF_REQUISITION.REQ_DATE  ,103) <= convert(date,'" + dtpToDate.Value + "',103)    ) as FirstTable left outer join  " & _
                    ' " (select 1 as  SNO, a.Version_No , a.Date as From_Date ,(SELECT min(b.date)  FROM TSPL_Exe_Deployment b WHERE b.Date > a.Date   ) as To_Date  from TSPL_Exe_Deployment a ) SecondTable on SecondTable.SNO = FirstTable.SNO " & _
                    ' " where convert (datetime ,FirstTable.Document_Date) between convert(datetime,SecondTable.From_Date) and convert(datetime,SecondTable.To_Date) " & _
                    ' " order by convert (datetime,FirstTable.Document_Date) ,FirstTable.Document_No "



                    'qry = "  select POSTED as Status,REQ_CODE as[Document Id],REQ_DATE as [Document Date] ," & _
                    '      " EXP_Date as [Expire Date],REQUESTED_BY as [Requested By]," & _
                    '      " DESCRIPTION as [Description],LOCATION_CODE [Location Code],COMMENTS as [Comments]," & _
                    '      " Created_By as [Created By],Created_Date as [Created Date] " & _
                    '      " from TSPL_MF_REQUISITION " & _
                    '      " WHERE  convert(date,TSPL_MF_REQUISITION.Created_Date ,103) >= convert(date,'" + dtpFromDate.Value + "',103) " & _
                    '      " and convert(date,TSPL_MF_REQUISITION.Created_Date,103) <= convert(date,'" + dtpToDate.Value + "',103) "


                    'qry += " ORDER BY TSPL_MF_REQUISITION.REQ_DATE, TSPL_MF_REQUISITION.REQ_CODE "

                End If
            End If

            '' fill Employee Bonus
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), "Production Return") = CompairStringResult.Equal Then
            Load_Authorisation(clsUserMgtCode.frmProductionReturnSTD)
            If dtAuthen.Rows.Count > 0 Then
                If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then
                    gv1.DataSource = Nothing

                    qry = GetQueryByScreen("TSPL_MF_RETURN", "RETURN_CODE", "RETURN_DATE", "Created_By", "Created_Date", "Modified_By", "Modified_Date", "Posting_Date", " ")

                    'qry = " select FirstTable.Document_No  ,FirstTable.Document_Date ,SecondTable.Version_No as Exe_Version ,FirstTable.Created_By as Created_By ,convert(varchar, FirstTable.Created_Date,103)   as Created_Date , FirstTable.Modify_By  ,convert (varchar,FirstTable.Modify_Date,103) as Modify_Date   from " & _
                    '  " (Select 1 as SNO, TSPL_MF_RETURN.RETURN_CODE as  Document_No ,TSPL_MF_RETURN.RETURN_DATE  as Document_Date,TSPL_MF_RETURN.Created_By ,TSPL_MF_RETURN.Created_Date,TSPL_MF_RETURN.Modify_By,TSPL_MF_RETURN.Modify_Date   from TSPL_MF_RETURN " & _
                    '  "	where convert(date,TSPL_MF_RETURN.RETURN_DATE  ,103) >= convert(date,'" + dtpFromDate.Value + "',103) and convert(date,TSPL_MF_RETURN.RETURN_DATE  ,103) <= convert(date,'" + dtpToDate.Value + "',103)    ) as FirstTable left outer join  " & _
                    '  " (select 1 as  SNO, a.Version_No , a.Date as From_Date ,(SELECT min(b.date)  FROM TSPL_Exe_Deployment b WHERE b.Date > a.Date   ) as To_Date  from TSPL_Exe_Deployment a ) SecondTable on SecondTable.SNO = FirstTable.SNO " & _
                    '  " where convert (datetime ,FirstTable.Document_Date) between convert(datetime,SecondTable.From_Date) and convert(datetime,SecondTable.To_Date) " & _
                    '  " order by convert (datetime,FirstTable.Document_Date) ,FirstTable.Document_No "



                    'qry = " select POSTED as Status,RETURN_CODE as[Document Id],RETURN_DATE as [Document Date] ," & _
                    '      " EXP_Date as [Expire Date],RETURNED_BY as [Returned By],RETURNED_TO as [Returned To]," & _
                    '      " DESCRIPTION as [Description],LOCATION_CODE [Location Code],COMMENTS as [Comments]," & _
                    '      " Created_By as [Created By],Created_Date as [Created Date] " & _
                    '      " from TSPL_MF_RETURN " & _
                    '      " WHERE  convert(date,TSPL_MF_RETURN.RETURN_DATE ,103) >= convert(date,'" + dtpFromDate.Value + "',103) " & _
                    '      " and convert(date,TSPL_MF_RETURN.RETURN_DATE,103) <= convert(date,'" + dtpToDate.Value + "',103) "


                    'qry += " ORDER BY TSPL_MF_RETURN.RETURN_DATE, TSPL_MF_RETURN.RETURN_CODE "
                End If
            End If

            '' fill Employee Salary Adjustment
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), "Store Issue") = CompairStringResult.Equal Then
            Load_Authorisation(clsUserMgtCode.frmStoreIssueSTD)
            If dtAuthen.Rows.Count > 0 Then
                If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then
                    gv1.DataSource = Nothing

                    qry = GetQueryByScreen("TSPL_MF_ISSUE", "ISSUE_CODE", "ISSUE_DATE", "Created_By", "Created_Date", "Modified_By", "Modified_Date", "Posting_Date", " ")

                    'qry = " select FirstTable.Document_No  ,FirstTable.Document_Date ,SecondTable.Version_No as Exe_Version ,FirstTable.Created_By as Created_By ,convert(varchar, FirstTable.Created_Date,103)   as Created_Date , FirstTable.Modify_By  ,convert (varchar,FirstTable.Modify_Date,103) as Modify_Date   from " & _
                    '  " (Select 1 as SNO, TSPL_MF_ISSUE.ISSUE_CODE as  Document_No ,TSPL_MF_ISSUE.ISSUE_DATE  as Document_Date,TSPL_MF_ISSUE.Created_By ,TSPL_MF_ISSUE.Created_Date,TSPL_MF_ISSUE.Modify_By,TSPL_MF_ISSUE.Modify_Date   from TSPL_MF_ISSUE " & _
                    '  "	where convert(date,TSPL_MF_ISSUE.ISSUE_DATE  ,103) >= convert(date,'" + dtpFromDate.Value + "',103) and convert(date,TSPL_MF_ISSUE.ISSUE_DATE  ,103) <= convert(date,'" + dtpToDate.Value + "',103)    ) as FirstTable left outer join  " & _
                    '  " (select 1 as  SNO, a.Version_No , a.Date as From_Date ,(SELECT min(b.date)  FROM TSPL_Exe_Deployment b WHERE b.Date > a.Date   ) as To_Date  from TSPL_Exe_Deployment a ) SecondTable on SecondTable.SNO = FirstTable.SNO " & _
                    '  " where convert (datetime ,FirstTable.Document_Date) between convert(datetime,SecondTable.From_Date) and convert(datetime,SecondTable.To_Date) " & _
                    '  " order by convert (datetime,FirstTable.Document_Date) ,FirstTable.Document_No "



                    'qry = " select POSTED as Status,ISSUE_CODE as[Document Id],ISSUE_DATE as [Document Date] ," & _
                    '      " EXP_Date as [Expire Date],ISSUED_BY as [Issue By],ISSUED_TO as [Issue To]," & _
                    '      " DESCRIPTION as [Description],LOCATION_CODE [Location Code],COMMENTS as [Comments]," & _
                    '      " Created_By as [Created By],Created_Date as [Created Date] " & _
                    '      " from TSPL_MF_ISSUE" & _
                    '      " WHERE  convert(date,TSPL_MF_ISSUE.ISSUE_DATE ,103) >= convert(date,'" + dtpFromDate.Value + "',103) " & _
                    '      " and convert(date,TSPL_MF_ISSUE.ISSUE_DATE,103) <= convert(date,'" + dtpToDate.Value + "',103) "


                    'qry += " ORDER BY TSPL_MF_ISSUE.ISSUE_DATE, TSPL_MF_ISSUE.ISSUE_CODE "

                End If
            End If
        End If
        If clsCommon.CompairString(clsCommon.myCstr(qry), Nothing) <> CompairStringResult.Equal Then

            dt = clsDBFuncationality.GetDataTable(qry)
            gv1.DataSource = dt
            gv1.MasterTemplate.SummaryRowsBottom.Clear()
            ' gv1Format()
            SetGridFormationOFgv()


        End If
        '-------------------------------------Code Ends Here--------------------------------------------
    End Sub
    '===========Sanjeet(27/12/2016)============================
    Sub FillMccProcurement()
        If clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), "VSP Item Issue") = CompairStringResult.Equal Then
            Load_Authorisation(clsUserMgtCode.frmVSPItemIssue)
            If dtAuthen.Rows.Count > 0 Then
                If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then
                    gv1.DataSource = Nothing

                    qry = GetQueryByScreen("TSPL_VSPAsset_HEAD", "DOC_NO", "Doc_Date", "Created_By", "Created_Date", "Modify_By", "Modify_Date", "Posting_Date", " ", "From_Location")

                    '  qry = " select FirstTable.Document_No  ,FirstTable.Document_Date ,SecondTable.Version_No as Exe_Version ,FirstTable.Created_By as Created_By ,convert(varchar, FirstTable.Created_Date,103)   as Created_Date , FirstTable.Modify_By  ,convert (varchar,FirstTable.Modify_Date,103) as Modify_Date   from " & _
                    '" (Select 1 as SNO, TSPL_VSPItem_HEAD.DOC_NO as  Document_No ,TSPL_VSPItem_HEAD.DOC_DATE  as Document_Date,TSPL_VSPItem_HEAD.Created_By ,TSPL_VSPItem_HEAD.Created_Date,TSPL_VSPItem_HEAD.Modify_By,TSPL_VSPItem_HEAD.Modify_Date   from TSPL_VSPItem_HEAD " & _
                    '"	where convert(date,TSPL_VSPItem_HEAD.DOC_DATE  ,103) >= convert(date,'" + dtpFromDate.Value + "',103) and convert(date,TSPL_VSPItem_HEAD.DOC_DATE  ,103) <= convert(date,'" + dtpToDate.Value + "',103)    ) as FirstTable left outer join  " & _
                    '" (select 1 as  SNO, a.Version_No , a.Date as From_Date ,(SELECT min(b.date)  FROM TSPL_Exe_Deployment b WHERE b.Date > a.Date   ) as To_Date  from TSPL_Exe_Deployment a ) SecondTable on SecondTable.SNO = FirstTable.SNO " & _
                    '" where convert (datetime ,FirstTable.Document_Date) between convert(datetime,SecondTable.From_Date) and convert(datetime,SecondTable.To_Date) " & _
                    '" order by convert (datetime,FirstTable.Document_Date) ,FirstTable.Document_No "

                    'qry = "select CAST(Status AS BIT) as Status,DOC_NO as [Document Id],DOC_DATE as [Document Date],To_Location as [Location Code],Remarks as [Description],Comment ,On_Hold AS Hold, Created_By as [Created By],Created_Date as [Created Date]  from TSPL_VSPItem_HEAD " & _
                    '    " where  convert(date,DOC_DATE,103)>= convert(date,'" + dtpFromDate.Value + "',103) " & _
                    '"and convert(date,DOC_DATE,103) <=convert(date,'" + dtpToDate.Value + "',103) "

                    'qry += " ORDER BY TSPL_VSPItem_HEAD.DOC_DATE, TSPL_VSPItem_HEAD.DOC_NO "

                End If
            End If
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), "VSP Asset Issue") = CompairStringResult.Equal Then
            Load_Authorisation(clsUserMgtCode.frmVSPAssetIssue)
            If dtAuthen.Rows.Count > 0 Then
                If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then
                    gv1.DataSource = Nothing

                    qry = GetQueryByScreen("TSPL_VSPAsset_HEAD", "DOC_NO", "Doc_Date", "Created_By", "Created_Date", "Modify_By", "Modify_Date", "Posting_Date", " ", "From_Location")

                    '  qry = " select FirstTable.Document_No  ,FirstTable.Document_Date ,SecondTable.Version_No as Exe_Version ,FirstTable.Created_By as Created_By ,convert(varchar, FirstTable.Created_Date,103)   as Created_Date , FirstTable.Modify_By  ,convert (varchar,FirstTable.Modify_Date,103) as Modify_Date   from " & _
                    '" (Select 1 as SNO, TSPL_VSPAsset_HEAD.DOC_NO as  Document_No ,TSPL_VSPAsset_HEAD.DOC_DATE  as Document_Date,TSPL_VSPAsset_HEAD.Created_By ,TSPL_VSPAsset_HEAD.Created_Date,TSPL_VSPAsset_HEAD.Modify_By,TSPL_VSPAsset_HEAD.Modify_Date   from TSPL_VSPAsset_HEAD " & _
                    '"	where convert(date,TSPL_VSPAsset_HEAD.DOC_DATE  ,103) >= convert(date,'" + dtpFromDate.Value + "',103) and convert(date,TSPL_VSPAsset_HEAD.DOC_DATE  ,103) <= convert(date,'" + dtpToDate.Value + "',103)    ) as FirstTable left outer join  " & _
                    '" (select 1 as  SNO, a.Version_No , a.Date as From_Date ,(SELECT min(b.date)  FROM TSPL_Exe_Deployment b WHERE b.Date > a.Date   ) as To_Date  from TSPL_Exe_Deployment a ) SecondTable on SecondTable.SNO = FirstTable.SNO " & _
                    '" where convert (datetime ,FirstTable.Document_Date) between convert(datetime,SecondTable.From_Date) and convert(datetime,SecondTable.To_Date) " & _
                    '" order by convert (datetime,FirstTable.Document_Date) ,FirstTable.Document_No "


                    'qry = "select CAST(Status AS BIT) as Status,DOC_NO as [Document Id],DOC_DATE as [Document Date],To_Location as [Location Code],Remarks as [Description],Comment ,On_Hold AS Hold, Created_By as [Created By],Created_Date as [Created Date]  from TSPL_VSPAsset_HEAD " & _
                    '" where  convert(date,DOC_DATE,103) >= convert(date,'" + dtpFromDate.Value + "',103) " & _
                    '"and convert(date,DOC_DATE,103) <=convert(date,'" + dtpToDate.Value + "',103) "

                    'qry += " ORDER BY TSPL_VSPAsset_HEAD.DOC_DATE, TSPL_VSPAsset_HEAD.DOC_NO "

                End If
            End If
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), "MCC Material Sale") = CompairStringResult.Equal Then
            Load_Authorisation(clsUserMgtCode.frmMCCMaterial)
            If dtAuthen.Rows.Count > 0 Then
                If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then
                    gv1.DataSource = Nothing

                    qry = GetQueryByScreen("TSPL_SD_SHIPMENT_HEAD", "Document_Code", "Document_Date", "Created_By", "Created_Date", "Modify_By", "Modify_Date", "Posting_Date", " ", "Bill_To_Location")

                    ' qry = " select FirstTable.Document_No  ,FirstTable.Document_Date ,SecondTable.Version_No as Exe_Version ,FirstTable.Created_By as Created_By ,convert(varchar, FirstTable.Created_Date,103)   as Created_Date , FirstTable.Modify_By  ,convert (varchar,FirstTable.Modify_Date,103) as Modify_Date   from " & _
                    '" (Select 1 as SNO, TSPL_SD_SHIPMENT_HEAD.Document_Code as  Document_No ,TSPL_SD_SHIPMENT_HEAD.Document_Date  as Document_Date,TSPL_SD_SHIPMENT_HEAD.Created_By ,TSPL_SD_SHIPMENT_HEAD.Created_Date,TSPL_SD_SHIPMENT_HEAD.Modify_By,TSPL_SD_SHIPMENT_HEAD.Modify_Date   from TSPL_SD_SHIPMENT_HEAD " & _
                    '"	where convert(date,TSPL_SD_SHIPMENT_HEAD.Document_Date  ,103) >= convert(date,'" + dtpFromDate.Value + "',103) and convert(date,TSPL_SD_SHIPMENT_HEAD.Document_Date  ,103) <= convert(date,'" + dtpToDate.Value + "',103)    ) as FirstTable left outer join  " & _
                    '" (select 1 as  SNO, a.Version_No , a.Date as From_Date ,(SELECT min(b.date)  FROM TSPL_Exe_Deployment b WHERE b.Date > a.Date   ) as To_Date  from TSPL_Exe_Deployment a ) SecondTable on SecondTable.SNO = FirstTable.SNO " & _
                    '" where convert (datetime ,FirstTable.Document_Date) between convert(datetime,SecondTable.From_Date) and convert(datetime,SecondTable.To_Date) " & _
                    '" order by convert (datetime,FirstTable.Document_Date) ,FirstTable.Document_No "


                    'qry = "select CAST(Status AS BIT) AS Status,Document_Code as [Document Id],Document_Date as [Document Date],DESCRIPTION as [Description], " & _
                    '"Bill_To_Location as [Location Code],Created_By as [Created By],Created_Date as [Created Date]  from TSPL_SD_SHIPMENT_HEAD " & _
                    '"where Trans_Type='MCC' and convert(date,Document_Date,103) >= convert(date,'" + dtpFromDate.Value + "',103) " & _
                    '"and convert(date,Document_Date,103) <=convert(date,'" + dtpToDate.Value + "',103) "


                    'qry += " ORDER BY TSPL_SD_SHIPMENT_HEAD.Document_Date, TSPL_SD_SHIPMENT_HEAD.Document_Code "

                End If
            End If
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), "MCC Material Sale Return") = CompairStringResult.Equal Then
            Load_Authorisation(clsUserMgtCode.frmMCCMaterialSaleReturn)
            If dtAuthen.Rows.Count > 0 Then
                If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then
                    gv1.DataSource = Nothing
                    qry = GetQueryByScreen("TSPL_SD_SALE_RETURN_HEAD", "Document_Code", "Created_Date", "Created_By", "Created_Date", "Modify_By", "Modify_Date", "Posting_Date", " ", "Bill_To_Location")

                    '  qry = " select FirstTable.Document_No  ,FirstTable.Document_Date ,SecondTable.Version_No as Exe_Version ,FirstTable.Created_By as Created_By ,convert(varchar, FirstTable.Created_Date,103)   as Created_Date , FirstTable.Modify_By  ,convert (varchar,FirstTable.Modify_Date,103) as Modify_Date   from " & _
                    '" (Select 1 as SNO, TSPL_SD_SALE_RETURN_HEAD.Document_Code as  Document_No ,TSPL_SD_SALE_RETURN_HEAD.Document_Date  as Document_Date,TSPL_SD_SALE_RETURN_HEAD.Created_By ,TSPL_SD_SALE_RETURN_HEAD.Created_Date,TSPL_SD_SALE_RETURN_HEAD.Modify_By,TSPL_SD_SALE_RETURN_HEAD.Modify_Date   from TSPL_SD_SALE_RETURN_HEAD " & _
                    '"	where convert(date,TSPL_SD_SALE_RETURN_HEAD.Document_Date  ,103) >= convert(date,'" + dtpFromDate.Value + "',103) and convert(date,TSPL_SD_SALE_RETURN_HEAD.Document_Date  ,103) <= convert(date,'" + dtpToDate.Value + "',103) and  TSPL_SD_SALE_RETURN_HEAD.Trans_Type='MCC'   ) as FirstTable left outer join  " & _
                    '" (select 1 as  SNO, a.Version_No , a.Date as From_Date ,(SELECT min(b.date)  FROM TSPL_Exe_Deployment b WHERE b.Date > a.Date   ) as To_Date  from TSPL_Exe_Deployment a ) SecondTable on SecondTable.SNO = FirstTable.SNO " & _
                    '" where convert (datetime ,FirstTable.Document_Date) between convert(datetime,SecondTable.From_Date) and convert(datetime,SecondTable.To_Date) " & _
                    '" order by convert (datetime,FirstTable.Document_Date) ,FirstTable.Document_No "

                    'qry = "select CAST(Status AS BIT) AS Status,Document_Code as [Document Id],Document_Date as [Document Date],DESCRIPTION as [Description]," & _
                    '        "Bill_To_Location as [Location Code],Created_By as [Created By],Created_Date as [Created Date]  from TSPL_SD_SALE_RETURN_HEAD " & _
                    '        "where Trans_Type='MCC' and convert(date,Document_Date,103)>= convert(date,'" + dtpFromDate.Value + "',103) " & _
                    '" and convert(date,Document_Date,103) <=convert(date,'" + dtpToDate.Value + "',103) "


                    'qry += " ORDER BY TSPL_SD_SALE_RETURN_HEAD.Document_Date, TSPL_SD_SALE_RETURN_HEAD.Document_Code "

                End If
            End If
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), "Milk Shift Uploader") = CompairStringResult.Equal Then
            Load_Authorisation(clsUserMgtCode.MilkShiftUploader)
            If dtAuthen.Rows.Count > 0 Then
                If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then
                    gv1.DataSource = Nothing
                    qry = GetQueryByScreen("TSPL_MILK_SHIFT_UPLOADER_HEAD", "document_no", "Shift_Date", "Created_By", "Created_Date", "Modified_By", "Modified_Date", "Posted_Date", " ", "MCC_Code")

                    '  qry = " select FirstTable.Document_No  ,FirstTable.Document_Date ,SecondTable.Version_No as Exe_Version ,FirstTable.Created_By as Created_By ,convert(varchar, FirstTable.Created_Date,103)   as Created_Date , FirstTable.Modify_By  ,convert (varchar,FirstTable.Modify_Date,103) as Modify_Date   from " & _
                    '" (Select 1 as SNO, TSPL_SD_SALE_RETURN_HEAD.Document_Code as  Document_No ,TSPL_SD_SALE_RETURN_HEAD.Document_Date  as Document_Date,TSPL_SD_SALE_RETURN_HEAD.Created_By ,TSPL_SD_SALE_RETURN_HEAD.Created_Date,TSPL_SD_SALE_RETURN_HEAD.Modify_By,TSPL_SD_SALE_RETURN_HEAD.Modify_Date   from TSPL_SD_SALE_RETURN_HEAD " & _
                    '"	where convert(date,TSPL_SD_SALE_RETURN_HEAD.Document_Date  ,103) >= convert(date,'" + dtpFromDate.Value + "',103) and convert(date,TSPL_SD_SALE_RETURN_HEAD.Document_Date  ,103) <= convert(date,'" + dtpToDate.Value + "',103) and  TSPL_SD_SALE_RETURN_HEAD.Trans_Type='MCC'   ) as FirstTable left outer join  " & _
                    '" (select 1 as  SNO, a.Version_No , a.Date as From_Date ,(SELECT min(b.date)  FROM TSPL_Exe_Deployment b WHERE b.Date > a.Date   ) as To_Date  from TSPL_Exe_Deployment a ) SecondTable on SecondTable.SNO = FirstTable.SNO " & _
                    '" where convert (datetime ,FirstTable.Document_Date) between convert(datetime,SecondTable.From_Date) and convert(datetime,SecondTable.To_Date) " & _
                    '" order by convert (datetime,FirstTable.Document_Date) ,FirstTable.Document_No "

                    'qry = "select CAST(Status AS BIT) AS Status,Document_Code as [Document Id],Document_Date as [Document Date],DESCRIPTION as [Description]," & _
                    '        "Bill_To_Location as [Location Code],Created_By as [Created By],Created_Date as [Created Date]  from TSPL_SD_SALE_RETURN_HEAD " & _
                    '        "where Trans_Type='MCC' and convert(date,Document_Date,103)>= convert(date,'" + dtpFromDate.Value + "',103) " & _
                    '" and convert(date,Document_Date,103) <=convert(date,'" + dtpToDate.Value + "',103) "


                    'qry += " ORDER BY TSPL_SD_SALE_RETURN_HEAD.Document_Date, TSPL_SD_SALE_RETURN_HEAD.Document_Code "

                End If
            End If
        End If
        If clsCommon.CompairString(clsCommon.myCstr(qry), Nothing) <> CompairStringResult.Equal Then

            dt = clsDBFuncationality.GetDataTable(qry)
            gv1.DataSource = dt
            gv1.MasterTemplate.SummaryRowsBottom.Clear()
            'gv1Format()
            SetGridFormationOFgv()

        End If
    End Sub

    Sub FillMilkProcurementBulk()
        If clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), "Purchase Invoice") = CompairStringResult.Equal Then
            Load_Authorisation(clsUserMgtCode.frmBulkMilkPurchaseInvoice)
            If dtAuthen.Rows.Count > 0 Then
                If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then
                    gv1.DataSource = Nothing

                    qry = GetQueryByScreen("TSPL_BULK_MILK_PURCHASE_INVOICE_head", "DOC_NO", "Created_Date", "Created_By", "Created_Date", "Modified_By", "Modified_Date", "Posting_Date", " ")

                    '  qry = " select FirstTable.Document_No  ,FirstTable.Document_Date ,SecondTable.Version_No as Exe_Version ,FirstTable.Created_By as Created_By ,convert(varchar, FirstTable.Created_Date,103)   as Created_Date , FirstTable.Modify_By  ,convert (varchar,FirstTable.Modify_Date,103) as Modify_Date   from " & _
                    '" (Select 1 as SNO, TSPL_BULK_MILK_PURCHASE_INVOICE_head.DOC_NO as  Document_No ,TSPL_BULK_MILK_PURCHASE_INVOICE_head.DOC_DATE as Document_Date,TSPL_BULK_MILK_PURCHASE_INVOICE_head.Created_By ,TSPL_BULK_MILK_PURCHASE_INVOICE_head.Created_Date,TSPL_BULK_MILK_PURCHASE_INVOICE_head.Modify_By,TSPL_BULK_MILK_PURCHASE_INVOICE_head.Modify_Date   from TSPL_BULK_MILK_PURCHASE_INVOICE_head " & _
                    '"	where convert(date,TSPL_BULK_MILK_PURCHASE_INVOICE_head.DOC_DATE ,103) >= convert(date,'" + dtpFromDate.Value + "',103) and convert(date,TSPL_BULK_MILK_PURCHASE_INVOICE_head.DOC_DATE ,103) <= convert(date,'" + dtpToDate.Value + "',103)    ) as FirstTable left outer join  " & _
                    '" (select 1 as  SNO, a.Version_No , a.Date as From_Date ,(SELECT min(b.date)  FROM TSPL_Exe_Deployment b WHERE b.Date > a.Date   ) as To_Date  from TSPL_Exe_Deployment a ) SecondTable on SecondTable.SNO = FirstTable.SNO " & _
                    '" where convert (datetime ,FirstTable.Document_Date) between convert(datetime,SecondTable.From_Date) and convert(datetime,SecondTable.To_Date) " & _
                    '" order by convert (datetime,FirstTable.Document_Date) ,FirstTable.Document_No "

                    'qry = "select CAST(isPosted AS BIT) as Status,DOC_NO as [Document Id],DOC_DATE as [Document Date],isnull(Total_AMT,0) as [Amount], '' as [Description], Vendor_Code as [Vendor Code],loc_code as [Location Code] , Created_By as [Created By],Created_Date as [Created Date]  from TSPL_BULK_MILK_PURCHASE_INVOICE_head " & _
                    '    " where  convert(date,DOC_DATE,103)>= convert(date,'" + dtpFromDate.Value + "',103) " & _
                    '" and convert(date,DOC_DATE,103) <=convert(date,'" + dtpToDate.Value + "',103) "

                    'qry += " ORDER BY TSPL_BULK_MILK_PURCHASE_INVOICE_head.DOC_DATE, TSPL_BULK_MILK_PURCHASE_INVOICE_head.DOC_NO "
                End If
            End If
        End If
        If clsCommon.CompairString(clsCommon.myCstr(qry), Nothing) <> CompairStringResult.Equal Then

            dt = clsDBFuncationality.GetDataTable(qry)
            gv1.DataSource = dt
            gv1.MasterTemplate.SummaryRowsBottom.Clear()
            'gv1Format()
            SetGridFormationOFgv()

        End If
    End Sub
    '============================================

    ''richa agarwal 12/05/2015 against ticket no.BM00000006520
    Sub FillBulkSale()
        If clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), "Bulk Dispatch") = CompairStringResult.Equal Then
            Load_Authorisation(clsUserMgtCode.FrmDispatchBulkSale)
            If dtAuthen.Rows.Count > 0 Then
                If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then
                    gv1.DataSource = Nothing

                    qry = GetQueryByScreen("TSPL_Dispatch_BulkSale", "Document_No", "Document_Date", "Created_By", "Created_Date", "Modified_By", "Modified_Date", "Posting_Date", " ")

                    '  qry = " select FirstTable.Document_No  ,FirstTable.Document_Date ,SecondTable.Version_No as Exe_Version ,FirstTable.Created_By as Created_By ,convert(varchar, FirstTable.Created_Date,103)   as Created_Date , FirstTable.Modify_By  ,convert (varchar,FirstTable.Modify_Date,103) as Modify_Date   from " & _
                    '" (Select 1 as SNO, TSPL_Dispatch_BulkSale.Document_No as  Document_No ,TSPL_Dispatch_BulkSale.Document_Date as Document_Date,TSPL_Dispatch_BulkSale.Created_By ,TSPL_Dispatch_BulkSale.Created_Date,TSPL_Dispatch_BulkSale.Modify_By,TSPL_Dispatch_BulkSale.Modify_Date   from TSPL_Dispatch_BulkSale " & _
                    '"	where convert(date,TSPL_Dispatch_BulkSale.Document_Date ,103) >= convert(date,'" + dtpFromDate.Value + "',103) and convert(date,TSPL_Dispatch_BulkSale.Document_Date ,103) <= convert(date,'" + dtpToDate.Value + "',103)    ) as FirstTable left outer join  " & _
                    '" (select 1 as  SNO, a.Version_No , a.Date as From_Date ,(SELECT min(b.date)  FROM TSPL_Exe_Deployment b WHERE b.Date > a.Date   ) as To_Date  from TSPL_Exe_Deployment a ) SecondTable on SecondTable.SNO = FirstTable.SNO " & _
                    '" where convert (datetime ,FirstTable.Document_Date) between convert(datetime,SecondTable.From_Date) and convert(datetime,SecondTable.To_Date) " & _
                    '" order by convert (datetime,FirstTable.Document_Date) ,FirstTable.Document_No "

                    'qry = " Select CAST((TSPL_Dispatch_BulkSale.Posted)as BIT) as Status,TSPL_Dispatch_BulkSale.Document_No as[Document Id],convert(varchar,TSPL_Dispatch_BulkSale.Document_Date,103) as [Document Date]," & _
                    '" isnull(TSPL_Dispatch_BulkSale.Total_Amt,0) as [Amount],TSPL_Dispatch_BulkSale.QC_Code as [QC No],TSPL_CUSTOMER_MASTER.Customer_Name as [Customer],TSPL_Dispatch_BulkSale.Tare_Weight as [Tare Weight], " & _
                    '" TSPL_Dispatch_BulkSale.Gross_Weight as [Gross Weight],TSPL_Dispatch_BulkSale.Net_Weight as [Net Weight] ,TSPL_Dispatch_BulkSale.Created_By as [Created By], " & _
                    '" convert(varchar,TSPL_Dispatch_BulkSale.Created_Date,103) as [Created Date],'' as Description from TSPL_Dispatch_BulkSale " & _
                    '" Left Outer Join TSPL_CUSTOMER_MASTER on TSPL_Dispatch_BulkSale.Customer_Code =TSPL_CUSTOMER_MASTER.Cust_Code " & _
                    '" WHERE  convert(date,TSPL_Dispatch_BulkSale.Document_Date ,103) >= convert(date,'" + dtpFromDate.Value + "',103) and convert(date,TSPL_Dispatch_BulkSale.Document_Date,103) <= convert(date,'" + dtpToDate.Value + "',103) "


                    'qry += " ORDER BY TSPL_Dispatch_BulkSale.Document_Date, TSPL_Dispatch_BulkSale.Document_No "

                End If
            End If
        End If
        If clsCommon.CompairString(clsCommon.myCstr(qry), Nothing) <> CompairStringResult.Equal Then

            dt = clsDBFuncationality.GetDataTable(qry)
            gv1.DataSource = dt
            gv1.MasterTemplate.SummaryRowsBottom.Clear()
            ' gv1Format()
            SetGridFormationOFgv()

        End If
    End Sub
    Sub FillFreshSale()
        If clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), "Shipment/Invoice") = CompairStringResult.Equal Then
            Load_Authorisation(clsUserMgtCode.FrmDispatchFreshSale)
            If dtAuthen.Rows.Count > 0 Then
                If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then
                    gv1.DataSource = Nothing

                    qry = GetQueryByScreen("TSPL_SD_SALE_INVOICE_HEAD", "Document_Code", "Document_Date", "Created_By", "Created_Date", "Modify_By", "Modify_Date", "Posting_Date", " ")

                    '  qry = " select FirstTable.Document_No  ,FirstTable.Document_Date ,SecondTable.Version_No as Exe_Version ,FirstTable.Created_By as Created_By ,convert(varchar, FirstTable.Created_Date,103)   as Created_Date , FirstTable.Modify_By  ,convert (varchar,FirstTable.Modify_Date,103) as Modify_Date   from " & _
                    '" (Select 1 as SNO, TSPL_SD_SALE_INVOICE_HEAD.Against_Shipment_No as  Document_No ,TSPL_SD_SALE_INVOICE_HEAD.Document_Date as Document_Date,TSPL_SD_SALE_INVOICE_HEAD.Created_By ,TSPL_SD_SALE_INVOICE_HEAD.Created_Date,TSPL_SD_SALE_INVOICE_HEAD.Modify_By,TSPL_SD_SALE_INVOICE_HEAD.Modify_Date   from TSPL_SD_SALE_INVOICE_HEAD " & _
                    '"	where convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date ,103) >= convert(date,'" + dtpFromDate.Value + "',103) and convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date ,103) <= convert(date,'" + dtpToDate.Value + "',103) and TSPL_SD_SALE_INVOICE_HEAD.Trans_Type='FS'   ) as FirstTable left outer join  " & _
                    '" (select 1 as  SNO, a.Version_No , a.Date as From_Date ,(SELECT min(b.date)  FROM TSPL_Exe_Deployment b WHERE b.Date > a.Date   ) as To_Date  from TSPL_Exe_Deployment a ) SecondTable on SecondTable.SNO = FirstTable.SNO " & _
                    '" where convert (datetime ,FirstTable.Document_Date) between convert(datetime,SecondTable.From_Date) and convert(datetime,SecondTable.To_Date) " & _
                    '" order by convert (datetime,FirstTable.Document_Date) ,FirstTable.Document_No "

                    'qry = "Select CAST((TSPL_SD_SALE_INVOICE_HEAD.Status)as BIT) as Status,Against_Shipment_No as [Document Id],TSPL_SD_SALE_INVOICE_HEAD.Document_Code as InvoiceNo, " & _
                    '    "convert(varchar,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) as [Document Date], isnull(TSPL_SD_SALE_INVOICE_HEAD.Total_Amt,0) as [Amount], " & _
                    '    "'' as [QC No],TSPL_CUSTOMER_MASTER.Customer_Name as [Customer],TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location as Location, " & _
                    '    "TSPL_SD_SALE_INVOICE_HEAD.Created_By as [Created By],  convert(varchar,TSPL_SD_SALE_INVOICE_HEAD.Created_Date,103) as [Created Date] ,TSPL_SD_SALE_INVOICE_HEAD.description" & _
                    '    " from TSPL_SD_SALE_INVOICE_HEAD  Left Outer Join TSPL_CUSTOMER_MASTER on " & _
                    '    "TSPL_SD_SALE_INVOICE_HEAD.Customer_Code =TSPL_CUSTOMER_MASTER.Cust_Code  " & _
                    '    "Left Outer Join TSPL_LOCATION_MASTER on TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location =TSPL_LOCATION_MASTER.Location_Code " & _
                    '" WHERE  convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date ,103) >= convert(date,'" + dtpFromDate.Value + "',103) and " & _
                    '"convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) <= convert(date,'" + dtpToDate.Value + "',103) "



                    'qry += " and TSPL_SD_SALE_INVOICE_HEAD.Trans_Type='FS' "
                    'qry += " ORDER BY TSPL_SD_SALE_INVOICE_HEAD.Document_Date, TSPL_SD_SALE_INVOICE_HEAD.Document_Code "

                End If
            End If
        End If
        If clsCommon.CompairString(clsCommon.myCstr(qry), Nothing) <> CompairStringResult.Equal Then

            dt = clsDBFuncationality.GetDataTable(qry)
            gv1.DataSource = dt
            gv1.MasterTemplate.SummaryRowsBottom.Clear()
            ' gv1Format()
            SetGridFormationOFgv()

        End If
    End Sub

    Sub FillDairySale()
        If clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), "Shipment/Invoice") = CompairStringResult.Equal Then
            Load_Authorisation(clsUserMgtCode.frmSaleDispatchDairy)
            If dtAuthen.Rows.Count > 0 Then
                If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then
                    gv1.DataSource = Nothing

                    qry = GetQueryByScreen("TSPL_BOOKING_MATSER", "Document_No", "Document_Date", "Created_By", "Created_Date", "Modified_By", "Modified_Date", "Modified_Date", " ", "Location_Code")

                    '  qry = " select FirstTable.Document_No  ,FirstTable.Document_Date ,SecondTable.Version_No as Exe_Version ,FirstTable.Created_By as Created_By ,convert(varchar, FirstTable.Created_Date,103)   as Created_Date , FirstTable.Modify_By  ,convert (varchar,FirstTable.Modify_Date,103) as Modify_Date   from " & _
                    '" (Select 1 as SNO, TSPL_SD_SHIPMENT_HEAD.Document_Code as  Document_No ,TSPL_SD_SHIPMENT_HEAD.Document_Date as Document_Date,TSPL_SD_SHIPMENT_HEAD.Created_By ,TSPL_SD_SHIPMENT_HEAD.Created_Date,TSPL_SD_SHIPMENT_HEAD.Modify_By,TSPL_SD_SHIPMENT_HEAD.Modify_Date   from TSPL_SD_SHIPMENT_HEAD " & _
                    '"	where convert(date,TSPL_SD_SHIPMENT_HEAD.Document_Date ,103) >= convert(date,'" + dtpFromDate.Value + "',103) and convert(date,TSPL_SD_SHIPMENT_HEAD.Document_Date ,103) <= convert(date,'" + dtpToDate.Value + "',103)    ) as FirstTable left outer join  " & _
                    '" (select 1 as  SNO, a.Version_No , a.Date as From_Date ,(SELECT min(b.date)  FROM TSPL_Exe_Deployment b WHERE b.Date > a.Date   ) as To_Date  from TSPL_Exe_Deployment a ) SecondTable on SecondTable.SNO = FirstTable.SNO " & _
                    '" where convert (datetime ,FirstTable.Document_Date) between convert(datetime,SecondTable.From_Date) and convert(datetime,SecondTable.To_Date) " & _
                    '" order by convert (datetime,FirstTable.Document_Date) ,FirstTable.Document_No "

                    'qry = "Select CAST((TSPL_SD_SHIPMENT_HEAD.Status)as BIT) as Status,Document_Code as [Document Id], " & _
                    '    "(select isnull((Select distinct '['+TSPL_SD_SALE_INVOICE_HEAD.Document_Code+']  ' from TSPL_SD_SHIPMENT_HEAD a left outer join TSPL_SD_SALE_INVOICE_HEAD on a.Document_Code=TSPL_SD_SALE_INVOICE_HEAD.Against_Shipment_No where  a.Document_Code= TSPL_SD_SHIPMENT_HEAD.Document_Code  for xml path('')),'') ) as InvoiceNo, " & _
                    '    "convert(varchar,TSPL_SD_SHIPMENT_HEAD.Document_Date,103) as [Document Date], isnull(TSPL_SD_SHIPMENT_HEAD.Total_Amt,0) as [Amount], " & _
                    '    "'' as [QC No],TSPL_CUSTOMER_MASTER.Customer_Name as [Customer],TSPL_SD_SHIPMENT_HEAD.Bill_To_Location as Location,  " & _
                    '    "TSPL_SD_SHIPMENT_HEAD.Created_By as [Created By],  convert(varchar,TSPL_SD_SHIPMENT_HEAD.Created_Date,103) as [Created Date] ,   " & _
                    '    "TSPL_SD_SHIPMENT_HEAD.description from TSPL_SD_SHIPMENT_HEAD  Left Outer Join TSPL_CUSTOMER_MASTER on  " & _
                    '    "TSPL_SD_SHIPMENT_HEAD.Customer_Code =TSPL_CUSTOMER_MASTER.Cust_Code  Left Outer Join TSPL_LOCATION_MASTER on  " & _
                    '    "TSPL_SD_SHIPMENT_HEAD.Bill_To_Location =TSPL_LOCATION_MASTER.Location_Code " & _
                    '" WHERE  convert(date,TSPL_SD_SHIPMENT_HEAD.Document_Date ,103) >= convert(date,'" + dtpFromDate.Value + "',103) and " & _
                    '"convert(date,TSPL_SD_SHIPMENT_HEAD.Document_Date,103) <= convert(date,'" + dtpToDate.Value + "',103) "





                    'qry += " ORDER BY TSPL_SD_SHIPMENT_HEAD.Document_Date, TSPL_SD_SHIPMENT_HEAD.Document_Code "

                End If
            End If

        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), "Booking/DO") = CompairStringResult.Equal Then
            Load_Authorisation(clsUserMgtCode.frmbookingdairy)
            If dtAuthen.Rows.Count > 0 Then
                If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then
                    gv1.DataSource = Nothing

                    qry = GetQueryByScreen("TSPL_BOOKING_MATSER", "Document_No", "Document_Date", "Created_By", "Created_Date", "Modified_By", "Modified_Date", "Modified_Date", " ", "Location_Code")

                    '  qry = " select FirstTable.Document_No  ,FirstTable.Document_Date ,SecondTable.Version_No as Exe_Version ,FirstTable.Created_By as Created_By ,convert(varchar, FirstTable.Created_Date,103)   as Created_Date , FirstTable.Modify_By  ,convert (varchar,FirstTable.Modify_Date,103) as Modify_Date   from " & _
                    '" (Select 1 as SNO, TSPL_BOOKING_MATSER.Document_Code as  Document_No ,TSPL_BOOKING_MATSER.Document_Date as Document_Date,TSPL_BOOKING_MATSER.Created_By ,TSPL_BOOKING_MATSER.Created_Date,TSPL_BOOKING_MATSER.Modify_By,TSPL_BOOKING_MATSER.Modify_Date   from TSPL_BOOKING_MATSER " & _
                    '"	where convert(date,TSPL_BOOKING_MATSER.Document_Date ,103) >= convert(date,'" + dtpFromDate.Value + "',103) and convert(date,TSPL_BOOKING_MATSER.Document_Date ,103) <= convert(date,'" + dtpToDate.Value + "',103)    ) as FirstTable left outer join  " & _
                    '" (select 1 as  SNO, a.Version_No , a.Date as From_Date ,(SELECT min(b.date)  FROM TSPL_Exe_Deployment b WHERE b.Date > a.Date   ) as To_Date  from TSPL_Exe_Deployment a ) SecondTable on SecondTable.SNO = FirstTable.SNO " & _
                    '" where convert (datetime ,FirstTable.Document_Date) between convert(datetime,SecondTable.From_Date) and convert(datetime,SecondTable.To_Date) " & _
                    '" order by convert (datetime,FirstTable.Document_Date) ,FirstTable.Document_No "


                    'qry = "select CAST((case when TSPL_BOOKING_DETAIL.Booking_Status=4 then 1 else 0 end)as BIT) as Status " & _
                    '    " ,TSPL_BOOKING_DETAIL.Document_No as [Document Id] " & _
                    '",TSPL_BOOKING_DETAIL.DocumentAmount as [Amount],TSPL_CUSTOMER_MASTER.Cust_Code as [Customer Code],TSPL_CUSTOMER_MASTER.Customer_Name as [Customer Name] " & _
                    '",TSPL_LOCATION_MASTER.Location_Desc as [Location], TSPL_BOOKING_MATSER.Created_By as [Created By] " & _
                    '", convert(varchar,TSPL_BOOKING_MATSER.Created_Date,103) as [Created Date] " & _
                    '", '' as [Description] " & _
                    '"from TSPL_BOOKING_MATSER inner join TSPL_BOOKING_DETAIL on TSPL_BOOKING_DETAIL.Document_No=TSPL_BOOKING_MATSER.Document_No " & _
                    '"Left Outer Join TSPL_CUSTOMER_MASTER on TSPL_BOOKING_DETAIL.Cust_Code =TSPL_CUSTOMER_MASTER.Cust_Code   " & _
                    '"Left Outer Join TSPL_LOCATION_MASTER on TSPL_BOOKING_DETAIL.Loc_Code =TSPL_LOCATION_MASTER.Location_Code  " & _
                    '" WHERE FOC_Item=0 and convert(date,TSPL_BOOKING_MATSER.Document_Date ,103) >= convert(date,'" + dtpFromDate.Value + "',103) and " & _
                    '"convert(date,TSPL_BOOKING_MATSER.Document_Date,103) <= convert(date,'" + dtpToDate.Value + "',103) "


                    'qry += " group by Booking_Status,TSPL_BOOKING_DETAIL.Document_No,DocumentAmount,TSPL_CUSTOMER_MASTER.Cust_Code,Customer_Name,Location_Desc,TSPL_BOOKING_MATSER.Created_By,TSPL_BOOKING_MATSER.Created_Date"
                    'qry += " ORDER BY [Created Date], [Document Id] "
                End If
            End If

        End If
        If clsCommon.CompairString(clsCommon.myCstr(qry), Nothing) <> CompairStringResult.Equal Then

            dt = clsDBFuncationality.GetDataTable(qry)
            gv1.DataSource = dt
            gv1.MasterTemplate.SummaryRowsBottom.Clear()
            'gv1Format()
            SetGridFormationOFgv()

        End If
    End Sub


    Sub FillMCCMaterialSaleFarmer()
        If clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), "MCC Material Sale Farmer") = CompairStringResult.Equal Then
            Load_Authorisation(clsUserMgtCode.frmMCCMaterialFarmer)
            If dtAuthen.Rows.Count > 0 Then
                If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then
                    gv1.DataSource = Nothing

                    qry = GetQueryByScreen("TSPL_MCC_Sale_Farmer_Head", "Document_Code", "Document_Date", "Created_By", "Created_Date", "Modify_By", "Modify_Date", "Posting_Date", " ")

                    '  qry = " select FirstTable.Document_No  ,FirstTable.Document_Date ,SecondTable.Version_No as Exe_Version ,FirstTable.Created_By as Created_By ,convert(varchar, FirstTable.Created_Date,103)   as Created_Date , FirstTable.Modify_By  ,convert (varchar,FirstTable.Modify_Date,103) as Modify_Date   from " & _
                    '" (Select 1 as SNO, TSPL_MCC_Sale_Farmer_Head.Document_Code as  Document_No ,TSPL_MCC_Sale_Farmer_Head.Document_Date as Document_Date,TSPL_MCC_Sale_Farmer_Head.Created_By ,TSPL_MCC_Sale_Farmer_Head.Created_Date,TSPL_MCC_Sale_Farmer_Head.Modify_By,TSPL_MCC_Sale_Farmer_Head.Modify_Date   from TSPL_MCC_Sale_Farmer_Head " & _
                    '"	where convert(date,TSPL_MCC_Sale_Farmer_Head.Document_Date ,103) >= convert(date,'" + dtpFromDate.Value + "',103) and convert(date,TSPL_MCC_Sale_Farmer_Head.Document_Date ,103) <= convert(date,'" + dtpToDate.Value + "',103)    ) as FirstTable left outer join  " & _
                    '" (select 1 as  SNO, a.Version_No , a.Date as From_Date ,(SELECT min(b.date)  FROM TSPL_Exe_Deployment b WHERE b.Date > a.Date   ) as To_Date  from TSPL_Exe_Deployment a ) SecondTable on SecondTable.SNO = FirstTable.SNO " & _
                    '" where convert (datetime ,FirstTable.Document_Date) between convert(datetime,SecondTable.From_Date) and convert(datetime,SecondTable.To_Date) " & _
                    '" order by convert (datetime,FirstTable.Document_Date) ,FirstTable.Document_No "

                    'qry = " Select CAST((TSPL_MCC_Sale_Farmer_Head.Status)as BIT) as Status,TSPL_MCC_Sale_Farmer_Head.Document_Code as[Document Id],convert(varchar,TSPL_MCC_Sale_Farmer_Head.Document_Date,103) as [Document Date]," & _
                    '" isnull(TSPL_MCC_Sale_Farmer_Head.Total_Amt,0) as [Amount],TSPL_LOCATION_MASTER.Location_Desc as [Location], " & _
                    '" TSPL_MCC_Sale_Farmer_Head.Created_By as [Created By], " & _
                    '" convert(varchar,TSPL_MCC_Sale_Farmer_Head.Created_Date,103) as [Created Date],TSPL_MCC_Sale_Farmer_Head.Description from TSPL_MCC_Sale_Farmer_Head " & _
                    '" left join TSPL_LOCATION_MASTER  on TSPL_MCC_Sale_Farmer_Head.bill_to_location=TSPL_LOCATION_MASTER.Location_Code  " & _
                    '" WHERE  convert(date,TSPL_MCC_Sale_Farmer_Head.Document_Date ,103) >= convert(date,'" + dtpFromDate.Value + "',103) and convert(date,TSPL_MCC_Sale_Farmer_Head.Document_Date,103) <= convert(date,'" + dtpToDate.Value + "',103) "


                    'qry += " ORDER BY TSPL_MCC_Sale_Farmer_Head.Document_Date, TSPL_MCC_Sale_Farmer_Head.Document_Code "

                End If
            End If
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), "MCC Material Sale Farmer Return") = CompairStringResult.Equal Then
            Load_Authorisation(clsUserMgtCode.frmMCCMaterialFarmer)
            If dtAuthen.Rows.Count > 0 Then
                If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then
                    gv1.DataSource = Nothing
                    qry = GetQueryByScreen("TSPL_MCC_SALE_RETURN_HEAD_FARMER", "Document_Code", "Document_Date", "Created_By", "Created_Date", "Modify_By", "Modify_Date", "Posting_Date", " ")

                    '  qry = " select FirstTable.Document_No  ,FirstTable.Document_Date ,SecondTable.Version_No as Exe_Version ,FirstTable.Created_By as Created_By ,convert(varchar, FirstTable.Created_Date,103)   as Created_Date , FirstTable.Modify_By  ,convert (varchar,FirstTable.Modify_Date,103) as Modify_Date   from " & _
                    '" (Select 1 as SNO, TSPL_MCC_SALE_RETURN_HEAD_FARMER.Document_Code as  Document_No ,TSPL_MCC_SALE_RETURN_HEAD_FARMER.Document_Date as Document_Date,TSPL_MCC_SALE_RETURN_HEAD_FARMER.Created_By ,TSPL_MCC_SALE_RETURN_HEAD_FARMER.Created_Date,TSPL_MCC_SALE_RETURN_HEAD_FARMER.Modify_By,TSPL_MCC_SALE_RETURN_HEAD_FARMER.Modify_Date   from TSPL_MCC_SALE_RETURN_HEAD_FARMER " & _
                    '"	where convert(date,TSPL_MCC_SALE_RETURN_HEAD_FARMER.Document_Date ,103) >= convert(date,'" + dtpFromDate.Value + "',103) and convert(date,TSPL_MCC_SALE_RETURN_HEAD_FARMER.Document_Date ,103) <= convert(date,'" + dtpToDate.Value + "',103)    ) as FirstTable left outer join  " & _
                    '" (select 1 as  SNO, a.Version_No , a.Date as From_Date ,(SELECT min(b.date)  FROM TSPL_Exe_Deployment b WHERE b.Date > a.Date   ) as To_Date  from TSPL_Exe_Deployment a ) SecondTable on SecondTable.SNO = FirstTable.SNO " & _
                    '" where convert (datetime ,FirstTable.Document_Date) between convert(datetime,SecondTable.From_Date) and convert(datetime,SecondTable.To_Date) " & _
                    '" order by convert (datetime,FirstTable.Document_Date) ,FirstTable.Document_No "

                    'qry = " Select CAST((TSPL_MCC_SALE_RETURN_HEAD_FARMER.Status)as BIT) as Status,TSPL_MCC_SALE_RETURN_HEAD_FARMER.Document_Code as[Document Id],convert(varchar,TSPL_MCC_SALE_RETURN_HEAD_FARMER.Document_Date,103) as [Document Date]," & _
                    '" isnull(TSPL_MCC_SALE_RETURN_HEAD_FARMER.Total_Amt,0) as [Amount],TSPL_LOCATION_MASTER.Location_Desc as [Location], " & _
                    '" TSPL_MCC_SALE_RETURN_HEAD_FARMER.Created_By as [Created By], " & _
                    '" convert(varchar,TSPL_MCC_SALE_RETURN_HEAD_FARMER.Created_Date,103) as [Created Date],TSPL_MCC_SALE_RETURN_HEAD_FARMER.Description from TSPL_MCC_SALE_RETURN_HEAD_FARMER " & _
                    '" left join TSPL_LOCATION_MASTER  on TSPL_MCC_SALE_RETURN_HEAD_FARMER.bill_to_location=TSPL_LOCATION_MASTER.Location_Code  " & _
                    '" WHERE  convert(date,TSPL_MCC_SALE_RETURN_HEAD_FARMER.Document_Date ,103) >= convert(date,'" + dtpFromDate.Value + "',103) and convert(date,TSPL_MCC_SALE_RETURN_HEAD_FARMER.Document_Date,103) <= convert(date,'" + dtpToDate.Value + "',103) "


                    'qry += " ORDER BY TSPL_MCC_SALE_RETURN_HEAD_FARMER.Document_Date, TSPL_MCC_SALE_RETURN_HEAD_FARMER.Document_Code "

                End If
            End If
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), "Farmer Payment Adjustment") = CompairStringResult.Equal Then
            Load_Authorisation(clsUserMgtCode.frmMCCMaterialFarmer)
            If dtAuthen.Rows.Count > 0 Then
                If clsCommon.CompairString(clsCommon.myCstr(dtAuthen.Rows(0)("Authorized_Flag")), "1") = CompairStringResult.Equal Then
                    gv1.DataSource = Nothing

                    qry = GetQueryByScreen("TSPL_MP_Pay_Adj_Head", "Adjustment_No", "Adjustment_Date", "Created_By", "Created_Date", "Modify_By", "Modify_Date", "Modify_Date", " ")

                    '  qry = " select FirstTable.Document_No  ,FirstTable.Document_Date ,SecondTable.Version_No as Exe_Version ,FirstTable.Created_By as Created_By ,convert(varchar, FirstTable.Created_Date,103)   as Created_Date , FirstTable.Modify_By  ,convert (varchar,FirstTable.Modify_Date,103) as Modify_Date   from " & _
                    '" (Select 1 as SNO, TSPL_MP_Pay_Adj_Head.invoice_No as  Document_No ,TSPL_MP_Pay_Adj_Head.Adjustment_Date as Document_Date,TSPL_MP_Pay_Adj_Head.Created_By ,TSPL_MP_Pay_Adj_Head.Created_Date,TSPL_MP_Pay_Adj_Head.Modify_By,TSPL_MP_Pay_Adj_Head.Modify_Date   from TSPL_MP_Pay_Adj_Head " & _
                    '"	where convert(date,TSPL_MP_Pay_Adj_Head.Adjustment_Date ,103) >= convert(date,'" + dtpFromDate.Value + "',103) and convert(date,TSPL_MP_Pay_Adj_Head.Adjustment_Date ,103) <= convert(date,'" + dtpToDate.Value + "',103)    ) as FirstTable left outer join  " & _
                    '" (select 1 as  SNO, a.Version_No , a.Date as From_Date ,(SELECT min(b.date)  FROM TSPL_Exe_Deployment b WHERE b.Date > a.Date   ) as To_Date  from TSPL_Exe_Deployment a ) SecondTable on SecondTable.SNO = FirstTable.SNO " & _
                    '" where convert (datetime ,FirstTable.Document_Date) between convert(datetime,SecondTable.From_Date) and convert(datetime,SecondTable.To_Date) " & _
                    '" order by convert (datetime,FirstTable.Document_Date) ,FirstTable.Document_No "


                    'qry = " Select CAST((TSPL_MP_Pay_Adj_Head.Is_Post)as BIT) as Status,TSPL_MP_Pay_Adj_Head.Adjustment_No as[Document Id],convert(varchar,TSPL_MP_Pay_Adj_Head.Adjustment_Date,103) as [Document Date]," & _
                    '" isnull(TSPL_MP_Pay_Adj_Head.Doc_Amount ,0) as [Amount],TSPL_LOCATION_MASTER.Location_Desc as [Location], " & _
                    '" TSPL_MP_Pay_Adj_Head.Created_By as [Created By], " & _
                    '" convert(varchar,TSPL_MP_Pay_Adj_Head.Created_Date,103) as [Created Date],TSPL_MP_Pay_Adj_Head.Description from TSPL_MP_Pay_Adj_Head " & _
                    '" left join TSPL_LOCATION_MASTER  on TSPL_MP_Pay_Adj_Head.MCC_Code=TSPL_LOCATION_MASTER.Location_Code  " & _
                    '" WHERE  convert(date,TSPL_MP_Pay_Adj_Head.Adjustment_Date ,103) >= convert(date,'" + dtpFromDate.Value + "',103) and convert(date,TSPL_MP_Pay_Adj_Head.Adjustment_Date,103) <= convert(date,'" + dtpToDate.Value + "',103) "

                    'qry += " ORDER BY TSPL_MP_Pay_Adj_Head.Adjustment_No, TSPL_MP_Pay_Adj_Head.Adjustment_Date "

                End If
            End If
        End If


        If clsCommon.CompairString(clsCommon.myCstr(qry), Nothing) <> CompairStringResult.Equal Then

            dt = clsDBFuncationality.GetDataTable(qry)
            gv1.DataSource = dt
            gv1.MasterTemplate.SummaryRowsBottom.Clear()
            'gv1Format()
            SetGridFormationOFgv()

        End If
    End Sub


    Private Sub btnShow_Click(sender As Object, e As EventArgs) Handles btnShow.Click
        If dtpFromDate.Value > dtpToDate.Value Then
            common.clsCommon.MyMessageBoxShow("'From date' Cann't Be Greater Than 'To Date'")
        Else
            qry = Nothing
            ShowData()
        End If
    End Sub

    Sub SetGridFormationOFgv()
        gv1.TableElement.TableHeaderHeight = 40
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gv1.Columns.Count - 1
            gv1.Columns(ii).ReadOnly = True
            gv1.Columns(ii).IsVisible = False
        Next
        gv1.Columns("Document_No").IsVisible = True
        gv1.Columns("Document_No").Width = 200
        gv1.Columns("Document_No").HeaderText = "Document No"

        gv1.Columns("Document_Date").IsVisible = True
        gv1.Columns("Document_Date").Width = 200
        gv1.Columns("Document_Date").HeaderText = "Document Date"

        gv1.Columns("Exe_Version_On_Document_Create").IsVisible = True
        gv1.Columns("Exe_Version_On_Document_Create").Width = 200
        gv1.Columns("Exe_Version_On_Document_Create").HeaderText = "Exe Version(Document Create)"

        gv1.Columns("Created_By_Deparment").IsVisible = True
        gv1.Columns("Created_By_Deparment").Width = 150
        gv1.Columns("Created_By_Deparment").HeaderText = "Department Name"

        gv1.Columns("Created_By").IsVisible = True
        gv1.Columns("Created_By").Width = 150
        gv1.Columns("Created_By").HeaderText = "Created By"

        gv1.Columns("Created_Date").IsVisible = True
        gv1.Columns("Created_Date").Width = 150
        gv1.Columns("Created_Date").HeaderText = "Created Date"

        gv1.Columns("Exe_Version_On_Document_Modify").IsVisible = True
        gv1.Columns("Exe_Version_On_Document_Modify").Width = 200
        gv1.Columns("Exe_Version_On_Document_Modify").HeaderText = "Exe Version(Document Modify)"

        gv1.Columns("Modify_By").IsVisible = True
        gv1.Columns("Modify_By").Width = 150
        gv1.Columns("Modify_By").HeaderText = "Modified By"

        gv1.Columns("Modify_Date").IsVisible = True
        gv1.Columns("Modify_Date").Width = 150
        gv1.Columns("Modify_Date").HeaderText = "Modified Date"
        gv1.EnableGrouping = False

    End Sub

    Private Sub cboTransaction_SelectedIndexChanged(sender As Object, e As UI.Data.PositionChangedEventArgs) Handles cboTransaction.SelectedIndexChanged
        gv1.DataSource = Nothing
    End Sub

    Private Sub RadButton1_Click(sender As Object, e As EventArgs) Handles RadButton1.Click
        Me.Close()
    End Sub

    Public Function GetQueryByScreen(ByVal strTableName As String, ByVal strDocumnetNo As String, ByVal strDocumentDate As String, ByVal strCreateBy As String, ByVal strCreateDate As String, ByVal strModifyBy As String, ByVal strModifyDate As String, ByVal strPostingDate As String, ByVal strWhere As String, Optional ByVal strLocation As String = "") As String
        If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 AndAlso clsCommon.myLen(strLocation) > 0 Then
            whrLoc = " and " + strTableName + "." + strLocation + " in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ") "
        Else
            whrLoc = ""
        End If
        If txtCreatedBy.arrValueMember IsNot Nothing AndAlso txtCreatedBy.arrValueMember.Count > 0 AndAlso clsCommon.myLen(strCreateBy) > 0 Then
            whrCreatedUser = " and " + strTableName + "." + strCreateBy + " in (" + clsCommon.GetMulcallString(txtCreatedBy.arrValueMember) + ") "
        Else
            whrCreatedUser = ""
        End If
        If txtModifyedBy.arrValueMember IsNot Nothing AndAlso txtModifyedBy.arrValueMember.Count > 0 AndAlso clsCommon.myLen(strModifyBy) > 0 Then
            whrModifiedUser = " and " + strTableName + "." + strModifyBy + " in (" + clsCommon.GetMulcallString(txtCreatedBy.arrValueMember) + ") "
        Else
            whrModifiedUser = ""
        End If

        Dim qry As String = "  select XXXFinal.Document_No , max( XXXFinal.Document_Date ) as Document_Date , max( XXXFinal.Exe_Version_For_Create_Date) as Exe_Version_On_Document_Create,max( TBL_Department.Created_By_Deparment ) as Created_By_Deparment ,max(XXXFinal.Created_By) as Created_By ,max( XXXFinal.Created_Date ) as Created_Date, max( XXXFinal.Exe_Version_For_Modify_Date) as Exe_Version_On_Document_Modify , max(XXXFinal.Modify_By ) as Modify_By , max(XXXFinal.Modify_Date) as Modify_Date  from  " &
                          " ( " &
                          "  select XXXCreatDateVersion.Document_No , XXXCreatDateVersion.Document_Date , XXXCreatDateVersion.Exe_Version_For_Create_Date, XXXCreatDateVersion.Created_By , XXXCreatDateVersion.Created_Date , XXXCreatDateVersion.Exe_Version_For_Modify_Date,XXXCreatDateVersion.Modify_By,XXXCreatDateVersion.Modify_Date from  ( " &
                          "  select FirstTable.Document_No  ,FirstTable.Document_Date ,SecondTable.Version_No as Exe_Version_For_Create_Date ,FirstTable.Created_By as Created_By , FirstTable.Created_Date  as Created_Date , '' as Exe_Version_For_Modify_Date, FirstTable.Modify_By  , FirstTable.Modify_Date as  Modify_Date  from  ( " &
                          "  Select 1 as SNO, " + strTableName + "  ." + strDocumnetNo + " as  Document_No , convert(datetime, " + strTableName + "  ." + strDocumentDate + ",103) as Document_Date," + strTableName + "  ." + strCreateBy + " as Created_By , DATEDIFF(dd, 0, convert(date," + strTableName + "  ." + strCreateDate + ",103)) + case when " + strTableName + "  ." + strDocumentDate + " is not null then  convert(datetime, CAST( convert (datetime," + strTableName + "  ." + strDocumentDate + ",103) AS time) ) else '' end as Created_Date ," + strTableName + "  ." + strModifyBy + " as Modify_By, DATEDIFF(dd, 0, convert(date," + strTableName + "  ." + strModifyDate + ",103)) + case when " + strTableName + "  ." + strPostingDate + " is not null then  convert(datetime, CAST(convert(datetime," + strTableName + "  ." + strPostingDate + ",103) AS time) ) else '' end as  Modify_Date   from " + strTableName + "    " &
                          "  where convert(date," + strTableName + "  ." + strCreateDate + ",103) >= convert(date,'" + dtpFromDate.Value + "',103) and convert(date," + strTableName + "  ." + strCreateDate + ",103) <= convert(date,'" + dtpToDate.Value + "',103)   " + strWhere + "  " + whrLoc + " " + whrCreatedUser + " " + whrModifiedUser + "   ) as FirstTable " &
                          "  left outer join " &
                          " (select 1 as  SNO, a.Version_No , a.Date as From_Date ,(SELECT min(b.date)  FROM TSPL_Exe_Deployment b WHERE b.Date > a.Date   ) as To_Date  from TSPL_Exe_Deployment a ) SecondTable on SecondTable.SNO = FirstTable.SNO  " &
                          "  where convert (datetime ,FirstTable.Created_Date,103) between convert(datetime,SecondTable.From_Date) and convert(datetime,SecondTable.To_Date)   " &
                          " ) XXXCreatDateVersion " &
                          "  Union All  " &
                          " select XXXModifyDateVersion.Document_No , XXXModifyDateVersion.Document_Date , XXXModifyDateVersion.Exe_Version_For_Create_Date, XXXModifyDateVersion.Created_By , XXXModifyDateVersion.Created_Date , XXXModifyDateVersion.Exe_Version_For_Modify_Date,XXXModifyDateVersion.Modify_By,XXXModifyDateVersion.Modify_Date from (  " &
                          " select FirstTable.Document_No  ,FirstTable.Document_Date ,'' as Exe_Version_For_Create_Date ,FirstTable.Created_By as Created_By , FirstTable.Created_Date  as Created_Date , SecondTable.Version_No  as Exe_Version_For_Modify_Date, FirstTable.Modify_By  , FirstTable.Modify_Date as  Modify_Date  from  ( " &
                          " Select 1 as SNO, " + strTableName + "  ." + strDocumnetNo + " as  Document_No ,convert(datetime, " + strTableName + "  ." + strDocumentDate + ",103) as Document_Date," + strTableName + "  ." + strCreateBy + "  as Created_By , DATEDIFF(dd, 0, convert(date," + strTableName + "  ." + strCreateDate + ",103)) + case when " + strTableName + "  ." + strDocumentDate + " is not null then  convert(datetime, CAST( convert(datetime," + strTableName + "  ." + strDocumentDate + ",103) AS time) ) else '' end as Created_Date ," + strTableName + "  ." + strModifyBy + " as Modify_By, DATEDIFF(dd, 0, convert(date," + strTableName + "  ." + strModifyDate + ",103)) + case when " + strTableName + "  ." + strPostingDate + " is not null then  convert(datetime, CAST(convert(datetime," + strTableName + "  ." + strPostingDate + ",103) AS time) ) else '' end as  Modify_Date   from " + strTableName + "    " &
                          " where convert(date," + strTableName + "  ." + strModifyDate + ",103) >= convert(date,'" + dtpFromDate.Value + "',103) and convert(date," + strTableName + "  ." + strModifyDate + ",103) <= convert(date,'" + dtpToDate.Value + "',103)   " + strWhere + "   " + whrLoc + " " + whrCreatedUser + " " + whrModifiedUser + "    ) as FirstTable " &
                          " left outer join  " &
                          " (select 1 as  SNO, a.Version_No , a.Date as From_Date ,(SELECT min(b.date)  FROM TSPL_Exe_Deployment b WHERE b.Date > a.Date   ) as To_Date  from TSPL_Exe_Deployment a ) SecondTable on SecondTable.SNO = FirstTable.SNO " &
                          "  where convert (datetime ,FirstTable.Modify_Date,103) between convert(datetime,SecondTable.From_Date) and convert(datetime,SecondTable.To_Date)  " &
                          "   ) XXXModifyDateVersion ) XXXFinal " &
                          " left outer join TSPL_User_Master on TSPL_User_Master.User_Code = XXXFinal.Created_By " &
                          " left outer join  (select Segment_code , Description as Created_By_Deparment  from TSPL_GL_SEGMENT_CODE where  Seg_No=3 ) TBL_Department on TBL_Department.Segment_code =TSPL_User_Master.Segment_code " &
                          " group by XXXFinal.Document_No " &
                          " order by convert (datetime,max(XXXFinal.Document_Date)) ,XXXFinal.Document_No "
        Return qry
    End Function

    Private Sub txtLocation__My_Click(sender As Object, e As EventArgs) Handles txtLocation._My_Click
        Dim qry As String = " Select Location_Code as Code, Location_Desc as Name from TSPL_LOCATION_MASTER Where Location_Type='Physical' "
        txtLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("Location@DocVersion@MulitSection", qry, "Code", "Name", txtLocation.arrValueMember, txtLocation.arrDispalyMember)
    End Sub

    Private Sub txtCreatedBy__My_Click(sender As Object, e As EventArgs) Handles txtCreatedBy._My_Click
        Dim qry As String = " Select User_Code as Code, User_Name as Name from TSPL_USER_MASTER "
        txtCreatedBy.arrValueMember = clsCommon.ShowMultipleSelectForm("CreatedBy@DocVersion@MulitSection", qry, "Code", "Name", txtCreatedBy.arrValueMember, txtCreatedBy.arrDispalyMember)
    End Sub

    Private Sub txtModifyedBy__My_Click(sender As Object, e As EventArgs) Handles txtModifyedBy._My_Click
        Dim qry As String = " Select User_Code as Code, User_Name as Name from TSPL_USER_MASTER "
        txtModifyedBy.arrValueMember = clsCommon.ShowMultipleSelectForm("ModifyedBy@DocVersion@MulitSection", qry, "Code", "Name", txtModifyedBy.arrValueMember, txtModifyedBy.arrDispalyMember)
    End Sub
End Class
