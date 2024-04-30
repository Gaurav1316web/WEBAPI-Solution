''''06/12/2012-01:29PM--Updation by --[Pankaj kumar]-- Added New Transactions [Empty Transactions, Sale Return(inter Company)]-----From-Ranjana Sinha
Imports common
Imports System.Data.SqlClient
Public Class FrmLockTransaction1
    Inherits FrmMainTranScreen
    Dim ArrLoc As New ArrayList()
    Dim AllowLockTransactionUserwise As Integer = 0
    Dim DbName As String
    Dim StrQuery As String = String.Empty

    Private Sub FrmLockTransaction1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        AllowLockTransactionUserwise = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowLockTransactionUserwise, clsFixedParameterCode.AllowLockTransactionUserwise, Nothing))
        If AllowLockTransactionUserwise = 1 Then
            btnLockUser.Visible = True
        End If
        LoadCompany()
        chkLocationCode.IsChecked = True
        'LoadBlankGrid()
        'LoadBlankDetail()
        SetUserMgmtNew()
        'dtpFromdate1.Text = clsCommon.GETSERVERDATE
        dtpToDate1.Text = clsCommon.GETSERVERDATE

    End Sub
  
    Public Sub LoadCompany()
        Dim dtCompany As DataTable = clsDBFuncationality.GetDataTable(" Select Comp_Code, Comp_Name  from TSPL_COMPANY_MASTER")
        cmbCompany.DisplayMember = "Comp_Name"
        cmbCompany.ValueMember = "Comp_Code"
        cmbCompany.DataSource = dtCompany
    End Sub

    Sub LoadBlankGrid()
        dgvDetails.Rows.Clear()
        dgvDetails.Columns.Clear()

        Dim Loc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        Loc = New GridViewTextBoxColumn()
        Loc.FormatString = ""
        Loc.HeaderText = "Location"
        Loc.Name = "colLocation"
        Loc.Width = 60
        Loc.ReadOnly = True
        dgvDetails.MasterTemplate.Columns.Add(Loc)

        Dim moduleC As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        moduleC = New GridViewTextBoxColumn()
        moduleC.FormatString = ""
        moduleC.HeaderText = "Module"
        moduleC.Name = "colModule"
        moduleC.Width = 201
        moduleC.ReadOnly = True
        dgvDetails.MasterTemplate.Columns.Add(moduleC)

        Dim Transaction As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        Transaction.FormatString = ""
        Transaction.HeaderText = "Transaction"
        Transaction.Name = "colTransaction"
        Transaction.Width = 301
        Transaction.ReadOnly = True
        dgvDetails.MasterTemplate.Columns.Add(Transaction)

        Dim Lock As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        Lock.FormatString = ""
        Lock.HeaderText = "Lock"
        Lock.Name = "colLock"
        Lock.Width = 71
        Lock.ReadOnly = False
        dgvDetails.MasterTemplate.Columns.Add(Lock)

        Dim startDate As GridViewDateTimeColumn = New GridViewDateTimeColumn()
        startDate = New GridViewDateTimeColumn()
        startDate.CustomFormat = "dd/MM/yyyy"
        startDate.FormatString = "{0:d}"
        startDate.HeaderText = "From"
        startDate.Name = "colFromDate"
        startDate.Width = 151
        startDate.ReadOnly = True
        startDate.IsVisible = False
        dgvDetails.MasterTemplate.Columns.Add(startDate)

        Dim endDate As GridViewDateTimeColumn = New GridViewDateTimeColumn()
        endDate = New GridViewDateTimeColumn()
        endDate.CustomFormat = "dd/MM/yyyy"
        endDate.FormatString = "{0:d}"
        endDate.HeaderText = "To"
        endDate.Name = "colToDate"
        endDate.Width = 151
        'endDate.ReadOnly = True
        dgvDetails.MasterTemplate.Columns.Add(endDate)

        dgvDetails.AllowDeleteRow = False
        dgvDetails.AllowAddNewRow = False
        dgvDetails.ShowGroupPanel = False
        dgvDetails.AllowColumnReorder = False
        dgvDetails.AllowRowReorder = False
        dgvDetails.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        dgvDetails.MasterTemplate.ShowRowHeaderColumn = False
        dgvDetails.ShowFilteringRow = True
        dgvDetails.EnableFiltering = True
    End Sub

    Public Function LockTransactionNameLocationSegwise() As String
        Dim Qry As String = " Select 'Common Services' as [Module],'Bank Transfer' as [Transaction],CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate]  " &
                " UNION ALL " &
                " Select 'Common Services' as [Module],'Reverse Transaction' as [Transaction],CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate]  " &
                " UNION ALL " &
                " Select 'Common Services' as [Module],'Bank Reco' as [Transaction],CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate]  " &
                 " UNION ALL" &
                " Select 'Receivables' as [Module],'Receipt Entry' as [Transaction],CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate]  " &
                " UNION ALL" &
                " Select 'Receivables' as [Module],'AR Invoice Entry' as [Transaction],CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate]  " &
                " UNION ALL" &
                " Select 'Payables' as [Module],'Payment Entry' as [Transaction],CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate]  " &
                " UNION ALL" &
                " Select '" + clsUserMgtCode.ModulePayable + "' as [Module],'" + clsUserMgtCode.mbtnAPInvoiceEntry + "' as [Transaction],CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate]    " &
                " UNION ALL" &
                " Select 'General Ledger' as [Module],'Journal Entry' as [Transaction],CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate]    " &
                " UNION ALL" &
                " Select 'General Ledger' as [Module],'VCGL Entry' as [Transaction],CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate]    " &
                 " UNION ALL" &
                " Select '" + clsUserMgtCode.ModuleTDS + "' as [Module],'" + clsUserMgtCode.mbtnAPInvoiceEntryTDS + "' as [Transaction], CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate]" &
        " UNION ALL" &
                " Select '" + clsUserMgtCode.ModuleMCCMilkProcurement + "' as [Module],'" + clsUserMgtCode.frmMultipleProcDeduction + "' as [Transaction], CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate]"
        Return Qry
    End Function
    Public Function LockTransactionNameLocationwise() As String
        Dim Qry As String = " Select 'Receivables' as [Module],'Adjustment Entry' as [Transaction],CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate]    " &
                " UNION ALL" &
                " Select '" + clsUserMgtCode.ModuleSalesNew + "' as [Module],'Shipment/Sale Invoice' as [Transaction], CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate]  " &
                " UNION ALL" &
                " Select '" + clsUserMgtCode.ModuleSales + "' as [Module],'Sale Return' as [Transaction], CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate]   " &
                " UNION ALL" &
                " Select '" + clsUserMgtCode.ModuleSales + "' as [Module],'Sale Return (Inter Company)' as [Transaction], CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate]   " &
                " UNION ALL" &
                " select 'Material Management' as [Module],'Transfer(Load-In)' as [Transaction], CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate]  " &
                " UNION ALL" &
                " select 'Material Management' as [Module],'Transfer(Load-Out)' as [Transaction], CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate]  " &
                " UNION ALL" &
                " select 'Material Management' as [Module],'Empty Transactions' as [Transaction], CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate]  " &
                " UNION ALL" &
                " Select 'Material Management' as [Module],'Store Adjustment' as [Transaction], CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate]  " &
                " UNION ALL" &
                " Select 'Purchase Order' as [Module],'Purchase Requisition' as [Transaction], CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate]   " &
                " UNION ALL" &
                " Select 'Purchase Order' as [Module],'Purchase Order' as [Transaction], CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate]    " &
                " UNION ALL" &
                " Select 'Purchase Order' as [Module],'Store Receipt Note' as [Transaction], CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate]  " &
                " UNION ALL" &
                " Select 'Purchase Order' as [Module],'Purchase Invoice' as [Transaction], CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate]  " &
                " UNION ALL" &
                " Select 'Purchase Order' as [Module],'Purchase Return' as [Transaction], CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate]  " &
                " UNION ALL" &
                " Select 'Purchase Order' as [Module],'RGP/NRGP' as [Transaction], CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate]  " &
                " UNION ALL" &
                " Select 'Purchase Order' as [Module],'Issue/Return/Transfer' as [Transaction], CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate]  " &
                " UNION ALL" &
                " Select 'Purchase Order' as [Module],'Scrap LoadOut' as [Transaction], CAST(0 as BIT ) as [Lock] , CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate]   " &
                " UNION ALL" &
                " Select '" + clsUserMgtCode.ModuleProductionDairy + "' as [Module],'" + clsUserMgtCode.JobWorkDispatchProduction + "' as [Transaction], CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate]  " &
                " UNION ALL" &
                " Select 'Fresh Sale' as [Module],'Fresh Booking Entry' as [Transaction], CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate]" &
                " UNION ALL" &
                " Select 'Fresh Sale' as [Module],'Fresh Dispatch Multiple' as [Transaction], CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate]" &
                " UNION ALL" &
                " Select 'Fresh Sale' as [Module],'Fresh Crate Received' as [Transaction], CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate]" &
                " UNION ALL" &
                " Select 'Fresh Sale' as [Module],'Fresh Sale Return' as [Transaction], CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate]" &
                " UNION ALL" &
                " Select 'Fresh Sale' as [Module],'Fresh Gatepass Entry' as [Transaction], CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate]" &
                 " UNION ALL" &
                " Select '" + clsUserMgtCode.ModuleBulkSale + "' as [Module],'" + clsUserMgtCode.FrmGateEntrySale + "' as [Transaction], CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate]" &
                " UNION ALL" &
                " Select '" + clsUserMgtCode.ModuleBulkSale + "' as [Module],'" + clsUserMgtCode.FrmSalesOrderBS + "' as [Transaction], CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate]" &
                " UNION ALL" &
                " Select '" + clsUserMgtCode.ModuleBulkSale + "' as [Module],'" + clsUserMgtCode.FrmWeighmentEntry + "' as [Transaction], CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate]" &
                " UNION ALL" &
                " Select '" + clsUserMgtCode.ModuleBulkSale + "' as [Module],'" + clsUserMgtCode.FrmLoadingTanker + "' as [Transaction], CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate]" &
                " UNION ALL" &
                " Select '" + clsUserMgtCode.ModuleBulkSale + "' as [Module],'" + clsUserMgtCode.FrmQualityCheckBulkSale + "' as [Transaction], CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate]" &
                " UNION ALL" &
                " Select '" + clsUserMgtCode.ModuleBulkSale + "' as [Module],'" + clsUserMgtCode.FrmDispatchBulkSale + "' as [Transaction], CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate]" &
                " UNION ALL" &
                " Select '" + clsUserMgtCode.ModuleBulkSale + "' as [Module],'" + clsUserMgtCode.FrmInvoiceBulkSale + "' as [Transaction], CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate]" &
                " UNION ALL" &
                " Select '" + clsUserMgtCode.ModuleBulkSale + "' as [Module],'" + "" + "' as [Transaction], CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate]" &
                " UNION ALL" &
                " Select '" + clsUserMgtCode.ModuleBulkSale + "' as [Module],'" + clsUserMgtCode.FrmTankerOut + "' as [Transaction], CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate]" &
                " UNION ALL" &
                " Select '" + clsUserMgtCode.ModuleBulkSale + "' as [Module],'" + clsUserMgtCode.FrmCreateAutoInvoiceBS + "' as [Transaction], CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate]" &
                " UNION ALL" &
                " Select '" + clsUserMgtCode.ModuleBulkSale + "' as [Module],'" + clsUserMgtCode.FrmBulkDispatchReturnSale + "' as [Transaction], CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate]" &
                " UNION ALL" &
                " Select '" + clsUserMgtCode.ModuleBulkSale + "' as [Module],'" + clsUserMgtCode.FrmBulkSaleReturn + "' as [Transaction], CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate]" &
                " UNION ALL" &
                 " Select '" + clsUserMgtCode.ModuleBulkSale + "' as [Module],'" + clsUserMgtCode.FrmCanSale + "' as [Transaction], CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate]" &
                 " UNION ALL" &
                 " Select '" + clsUserMgtCode.ModuleBulkSale + "' as [Module],'" + clsUserMgtCode.FrmCanSaleUploader + "' as [Transaction], CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate]" &
                " UNION ALL" &
                 " Select '" + clsUserMgtCode.ModuleBulkSale + "' as [Module],'" + clsUserMgtCode.FrmCanReceived + "' as [Transaction], CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate]" &
                " UNION ALL" &
                  " Select '" + clsUserMgtCode.ModuleBulkSale + "' as [Module],'" + "" + "' as [Transaction], CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate]" &
                " UNION ALL" &
                " Select 'Product Sale' as [Module],'Product Booking Entry' as [Transaction], CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate]" &
                " UNION ALL" &
                " Select 'Product Sale' as [Module],'Sale Order' as [Transaction], CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate]" &
                " UNION ALL" &
                " Select 'Product Sale' as [Module],'Product Delivery Order' as [Transaction], CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate]" &
                " UNION ALL" &
                " Select 'Product Sale' as [Module],'Product Dispatch' as [Transaction], CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate]" &
                " UNION ALL" &
                " Select 'Product Sale' as [Module],'Product Invoice' as [Transaction], CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate]" &
                " UNION ALL" &
                " Select 'Product Sale' as [Module],'Product Sale Return' as [Transaction], CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate]" &
                " UNION ALL" &
                " Select '" + clsUserMgtCode.ModuleProductionDairy + "' as [Module],'" + clsUserMgtCode.frmGateEntry + "' as [Transaction], CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate]" &
                " UNION ALL" &
                " Select '" + clsUserMgtCode.ModuleProductionDairy + "' as [Module],'" + clsUserMgtCode.frmWeighment + "' as [Transaction], CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate]" &
                " UNION ALL" &
                " Select '" + clsUserMgtCode.ModuleProductionDairy + "' as [Module],'" + clsUserMgtCode.frmQualityCheck + "' as [Transaction], CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate]" &
                " UNION ALL" &
                " Select '" + clsUserMgtCode.ModuleProductionDairy + "' as [Module],'" + clsUserMgtCode.frmUnloading + "' as [Transaction], CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate]" &
                " UNION ALL" &
                " Select '" + clsUserMgtCode.ModuleProductionDairy + "' as [Module],'" + clsUserMgtCode.frmBulkMilkSRN + "' as [Transaction], CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate]" &
                " UNION ALL" &
                " Select '" + clsUserMgtCode.ModuleProductionDairy + "' as [Module],'" + clsUserMgtCode.frmBulkMilkPurchaseInvoice + "' as [Transaction], CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate]" &
                " UNION ALL" &
                " Select '" + clsUserMgtCode.ModuleProductionDairy + "' as [Module],'" + clsUserMgtCode.frmMilkTransferIn + "' as [Transaction], CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate]" &
                " UNION ALL" &
                 " Select 'Milk Procurement Bulk' as [Module],'Milk Transfer In Return' as [Transaction], CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate]" &
                " UNION ALL" &
                " Select '" + clsUserMgtCode.ModuleMCCMilkProcurement + "' as [Module],'" + clsUserMgtCode.frmProvisionEntry + "' as [Transaction], CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate]" &
                 " UNION ALL" &
                " Select 'Milk Procurement Bulk' as [Module],'Milk Job Work Transfer' as [Transaction], CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate]" &
                " UNION ALL" &
                " Select '" + clsUserMgtCode.ModuleCSASale + "' as [Module],'" + clsUserMgtCode.frmCSADeliveryOrder + "' as [Transaction], CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate]" &
                " UNION ALL" &
                " Select '" + clsUserMgtCode.ModuleCSASale + "' as [Module],'" + clsUserMgtCode.frmCSATransfer + "' as [Transaction], CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate]" &
                " UNION ALL" &
                " Select '" + clsUserMgtCode.ModuleCSASale + "' as [Module],'" + clsUserMgtCode.frmCSASaleInvoice + "' as [Transaction], CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate]" &
                " UNION ALL" &
                " Select '" + clsUserMgtCode.ModuleCSASale + "' as [Module],'CSA ' as [Tra  nsaction], CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate]" &
                " UNION ALL" &
                  " Select '" + clsUserMgtCode.ModuleCSASale + "' as [Module],'" + clsUserMgtCode.frmCSABooking + "' as [Transaction], CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate]" &
                " UNION ALL" &
                " Select '" + clsUserMgtCode.ModuleMCCMilkProcurement + "' as [Module],'" + clsUserMgtCode.frmMilkReceipt + "' as [Transaction], CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate]" &
                " UNION ALL" &
                " Select 'Milk Procurement MCC' as [Module],'Milk SRN' as [Transaction], CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate]" &
                " UNION ALL" &
                " Select 'Milk Procurement MCC' as [Module],'Milk Truck Sheet' as [Transaction], CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate]" &
                " UNION ALL" &
                " Select '" + clsUserMgtCode.ModuleMCCMilkProcurement + "' as [Module],'" + clsUserMgtCode.frmVlcdataUploadar + "' as [Transaction], CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate]" &
                " UNION ALL" &
                " Select '" + clsUserMgtCode.ModuleMCCMilkProcurement + "' as [Module],'" + clsUserMgtCode.frmMilkShiftEndMCC + "' as [Transaction], CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate]" &
                " UNION ALL" &
                " Select '" + clsUserMgtCode.ModuleMCCMilkProcurement + "' as [Module],'" + clsUserMgtCode.frmMCCDispatch + "' as [Transaction], CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate]" &
                " UNION ALL" &
                " Select 'Milk Procurement MCC' as [Module],'Tanker Location Change' as [Transaction], CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate]" &
                " UNION ALL" &
                " Select '" + clsUserMgtCode.ModuleMCCMilkProcurement + "' as [Module],'" + clsUserMgtCode.frmMilkPurchaseInvoice + "' as [Transaction], CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate]" &
                " UNION ALL" &
                " Select '" + clsUserMgtCode.ModuleMCCMilkProcurement + "' as [Module],'" + clsUserMgtCode.frmVSPAssetIssue + "' as [Transaction], CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate]" &
                " UNION ALL" &
                " Select '" + clsUserMgtCode.ModuleMCCMilkProcurement + "' as [Module],'" + clsUserMgtCode.frmMCCMaterial + "' as [Transaction], CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate]" &
                " UNION ALL" &
                " Select '" + clsUserMgtCode.ModuleMCCMilkProcurement + "' as [Module],'" + clsUserMgtCode.frmMCCMaterialSaleReturn + "' as [Transaction], CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate]" &
                " UNION ALL" &
                " Select '" + clsUserMgtCode.ModuleMCCMilkProcurement + "' as [Module],'" + "' as [Transaction], CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate]" &
                " UNION ALL" &
                " Select '" + clsUserMgtCode.ModuleMCCMilkProcurement + "' as [Module],'" + clsUserMgtCode.frmPaymentProcess + "' as [Transaction], CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate]" &
                " UNION ALL" &
                " Select 'Milk Procurement MCC' as [Module],'Milk Recurring Payable Invoice' as [Transaction], CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate]" &
                " UNION ALL" &
                " Select 'Milk Procurement MCC' as [Module],'MCC Milk Transfer Price' as [Transaction], CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate]" &
                " UNION ALL" &
                " Select '" + clsUserMgtCode.ModuleMCCMilkProcurement + "' as [Module],'" + clsUserMgtCode.FrmVLCDataUploaderManual + "' as [Transaction], CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate]" &
                " UNION ALL" &
                " Select 'Standard Production' as [Module],'Manufacturing Order' as [Transaction], CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate]" &
                " UNION ALL" &
                " Select 'Standard Production' as [Module],'Store Issue' as [Transaction], CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate]" &
                " UNION ALL" &
                " Select '" + clsUserMgtCode.ModuleProductionDairy + "' as [Module],'" + clsUserMgtCode.frmProcessProdReturn + "' as [Transaction], CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate]" &
                " UNION ALL" &
                 " Select '" + clsUserMgtCode.ModuleProductionDairy + "' as [Module],'" + clsUserMgtCode.frmSiloMilkTransfer + "' as [Transaction], CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate]" &
                " UNION ALL" &
                 " Select '" + clsUserMgtCode.ModuleProductionDairy + "' as [Module],'" + clsUserMgtCode.frmProductionStoreRequest + "' as [Transaction], CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate]" &
                " UNION ALL" &
                " Select 'Standard Production' as [Module],'Production Receipt' as [Transaction], CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate]" &
                " UNION ALL" &
                " Select '" + clsUserMgtCode.ModuleProductionDairy + "' as [Module],'" + clsUserMgtCode.frmMRPForProduction + "' as [Transaction], CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate]" &
                " UNION ALL" &
                " Select 'Standard Production' as [Module],'Production Serialized Mapping' as [Transaction], CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate]" &
                " UNION ALL" &
                " Select 'Standard Production' as [Module],'Replace Serializing Entry' as [Transaction], CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate]" &
                " UNION ALL" &
                " Select 'Standard Production' as [Module],'MRP(STD)' as [Transaction], CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate]" &
                " UNION ALL" &
                " Select '" + clsUserMgtCode.ModuleProductionDairy + "' as [Module],'" + clsUserMgtCode.frmProductionPlanningDairy + "' as [Transaction], CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate]" &
                " UNION ALL" &
                " Select '" + clsUserMgtCode.ModuleProductionDairy + "' as [Module],'" + clsUserMgtCode.frmBatchOrderDairy + "' as [Transaction], CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate]" &
                " UNION ALL" &
                " Select '" + clsUserMgtCode.ModuleProductionDairy + "' as [Module],'" + clsUserMgtCode.frmProcessProductionIssueEntry + "' as [Transaction], CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate]" &
                " UNION ALL" &
                " Select '" + clsUserMgtCode.ModuleProductionDairy + "' as [Module],'" + clsUserMgtCode.frmProcessProductionStandardization + "' as [Transaction], CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate]" &
                " UNION ALL" &
                " Select '" + clsUserMgtCode.ModuleProductionDairy + "' as [Module],'" + clsUserMgtCode.ProcessProductionStandardizationFinalQC + "' as [Transaction], CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate]" &
                 " UNION ALL" &
                " Select '" + clsUserMgtCode.ModuleProductionDairy + "' as [Module],'" + clsUserMgtCode.frmProcessProductionStageProcess + "' as [Transaction], CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate]" &
                " UNION ALL" &
                 " Select '" + clsUserMgtCode.ModuleProductionDairy + "' as [Module],'" + clsUserMgtCode.DariyProductionUploader + "' as [Transaction], CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate]" &
                " UNION ALL" &
                 " Select '" + clsUserMgtCode.ModuleProductionDairy + "' as [Module],'" + clsUserMgtCode.frmWreckageBooking + "' as [Transaction], CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate]" &
                " UNION ALL" &
                " Select '" + clsUserMgtCode.ModuleProductionDairy + "' as [Module],'" + clsUserMgtCode.frmProductionEntry + "' as [Transaction], CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate]" &
                 " UNION ALL" &
                " Select '" + clsUserMgtCode.ModuleProductionDairy + "' as [Module],'" + clsUserMgtCode.frmAssembDis + "' as [Transaction], CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate]" &
                " UNION ALL" &
                  " Select '" + clsUserMgtCode.ModuleProductionDairy + "' as [Module],'" + clsUserMgtCode.frmProductionEntryFinalQC + "' as [Transaction], CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate]" &
                " UNION ALL" &
                " Select '" + clsUserMgtCode.ModuleFarmerPayment + "' as [Module],'" + clsUserMgtCode.frmMCCMaterialFarmer + "' as [Transaction], CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate]" &
                " UNION ALL" &
                 " Select '" + clsUserMgtCode.ModuleFarmerPayment + "' as [Module],'" + clsUserMgtCode.frmMCCMaterialSaleReturnFarmer + "' as [Transaction], CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate]" &
                " UNION ALL" &
                  " Select '" + clsUserMgtCode.ModuleFarmerPayment + "' as [Module],'" + clsUserMgtCode.frmPaymentProcessFarmer + "' as [Transaction], CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate]" &
                " UNION ALL" &
                " Select '" + clsUserMgtCode.ModuleFarmerPayment + "' as [Module],'" + clsUserMgtCode.frmFarmerPaymentAdjustment + "' as [Transaction], CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate]" &
                " UNION ALL" &
                 " Select '" + clsUserMgtCode.ModuleFarmerPayment + "' as [Module],'" + clsUserMgtCode.frmLockMPCollectionPC + "' as [Transaction], CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate]" &
                " UNION ALL" &
                " Select 'Purchase Order' as [Module],'Gate Received Note' as [Transaction], CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate]  " &
                " UNION ALL" &
                " Select 'Purchase Order' as [Module],'Material Received Note' as [Transaction], CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate]  " &
                " UNION ALL" &
                " Select 'Purchase Order' as [Module],'Store Received Note Return' as [Transaction], CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate]  " &
                " UNION ALL" &
                " Select 'Purchase Order' as [Module],'Store Requisition' as [Transaction], CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate]  " &
                 " UNION ALL" &
                " Select 'Payroll' as [Module],'Weekly Holidays' as [Transaction], CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate]" &
                " UNION ALL" &
                " Select 'Payroll' as [Module],'Allowance Details' as [Transaction], CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate]" &
                " UNION ALL" &
                " Select 'Payroll' as [Module],'Deduction Details' as [Transaction], CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate]" &
                " UNION ALL" &
                " Select 'Payroll' as [Module],'Apply Loan' as [Transaction], CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate]" &
                " UNION ALL" &
                " Select 'Payroll' as [Module],'Loan Generation' as [Transaction], CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate]" &
                " UNION ALL" &
                " Select 'Payroll' as [Module],'Employee Adjustment Voucher' as [Transaction], CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate]" &
                " UNION ALL" &
                " Select 'Payroll' as [Module],'Salary Generation' as [Transaction], CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate]" &
                " UNION ALL" &
                " Select 'Payroll' as [Module],'Employee Transfer' as [Transaction], CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate]" &
                " UNION ALL" &
                " Select 'Payroll' as [Module],'Employee Increment' as [Transaction], CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate]" &
                " UNION ALL" &
                " Select 'Payroll' as [Module],'Allotment Of Leaves' as [Transaction], CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate]" &
                 " UNION ALL " &
                " Select 'Fixed Asset' as [Module],'Acquisition Entry' as [Transaction], CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate]" &
                 " UNION ALL" &
                " Select 'Fixed Asset' as [Module],'Disposal Entry' as [Transaction], CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate]" &
                 " UNION ALL" &
                " Select 'Fixed Asset' as [Module],'Asset Requisition' as [Transaction], CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate]" &
                 " UNION ALL" &
                " Select 'Fixed Asset' as [Module],'Issue Items to Assemble Assset' as [Transaction], CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate]" &
                 " UNION ALL" &
                " Select 'Fixed Asset' as [Module],'Assset Work Expanses' as [Transaction], CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate]" &
                " UNION ALL" &
                " Select '" + clsUserMgtCode.ModulePayable + "' as [Module],'" + clsUserMgtCode.FrmVendorService + "' as [Transaction], CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate]" &
                " UNION ALL" &
                " Select 'Material Management' as [Module],'MM Assemblies/Disassemblies' as [Transaction], CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate] " &
                " UNION ALL " &
                " Select 'JobWork Outward' as [Module],'JobWork SRN' as [Transaction], CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate] " &
                " UNION ALL " &
                " Select 'JobWork Outward' as [Module],'JobWork SRN Return' as [Transaction], CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate] " &
                " UNION ALL" &
                " Select 'JobWork Outward' as [Module],'JobWork Other Transfer' as [Transaction], CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate] " &
                " UNION ALL " &
                " Select 'JobWork Outward' as [Module],'JobWork Other Transfer Return' as [Transaction], CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate] " &
                " UNION ALL " &
                " Select 'JobWork Outward' as [Module],'JobWork Milk Transfer' as [Transaction], CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate]  " &
                " UNION ALL " &
                " Select 'JobWork Outward' as [Module],'JobWork Milk Transfer Return' as [Transaction], CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate] " &
                " UNION ALL " &
                " Select 'JobWork Outward' as [Module],'JobWork Milk Gate Entry' as [Transaction], CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate] " &
                " UNION ALL " &
                " Select 'JobWork Outward' as [Module],'JobWork Milk QualityCheck' as [Transaction], CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate] " &
                " UNION ALL " &
                " Select 'JobWork Outward' as [Module],'JobWork Milk Unloading' as [Transaction], CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate] " &
                " UNION ALL " &
                " Select 'JobWork Outward' as [Module],'JobWork Milk Weighment' as [Transaction], CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate] " &
                 " UNION ALL " &
                " Select 'JobWork Inward' as [Module],'JobWork Billing' as [Transaction], CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate] " &
                 " UNION ALL " &
                " Select 'Milk Jobwork' as [Module],'Milk Jobwork GateEntry' as [Transaction], CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate] " &
                " UNION ALL " &
                " Select 'Milk Jobwork' as [Module],'Milk Jobwork Weighment' as [Transaction], CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate] " &
                " UNION ALL " &
                " Select 'Milk Jobwork' as [Module],'Milk JobWork Quality Check' as [Transaction], CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate] " &
                " UNION ALL " &
                " Select 'Milk Jobwork' as [Module],'Milk JobWork Unloading' as [Transaction], CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate] " &
                " UNION ALL " &
                " Select 'Milk Jobwork' as [Module],'Milk JobWork RGP' as [Transaction], CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate] " &
                " UNION ALL " &
                " Select 'Milk Jobwork' as [Module],'Milk JobWork SRN' as [Transaction], CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate] " &
                " UNION ALL " &
                " Select '" + clsUserMgtCode.ModuleSaleDairy + "' as [Module],'" + clsUserMgtCode.frmbookingdairy + "' as [Transaction], CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate] " &
                " UNION ALL " &
                " Select '" + clsUserMgtCode.ModuleSaleDairy + "' as [Module],'" + clsUserMgtCode.frmDeliveryOrderDairy + "' as [Transaction], CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate] " &
                " UNION ALL " &
                " Select '" + clsUserMgtCode.ModuleSaleDairy + "' as [Module],'" + clsUserMgtCode.frmSaleDispatchDairy + "' as [Transaction], CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate] " &
                " UNION ALL " &
                 " Select '" + clsUserMgtCode.ModuleSaleDairy + "' as [Module],'" + clsUserMgtCode.frmSaleInvoicedairy + "' as [Transaction], CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate] " &
                " UNION ALL " &
                " Select '" + clsUserMgtCode.ModuleSaleDairy + "' as [Module],'" + clsUserMgtCode.frmSaleReturndairy + "' as [Transaction], CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate] " &
                " UNION ALL " &
                 " Select '" + clsUserMgtCode.ModuleSaleDairy + "' as [Module],'" + clsUserMgtCode.frmGatePassDairy + "' as [Transaction], CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate] " &
                " UNION ALL " &
                   " Select '" + clsUserMgtCode.ModuleSaleDairy + "' as [Module],'" + clsUserMgtCode.frmDairyBookingCustomer + "' as [Transaction], CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate] " &
                " UNION ALL " &
                " Select '" + clsUserMgtCode.ModuleSaleDairy + "' as [Module],'" + clsUserMgtCode.frmCrateReceviedDairySale + "' as [Transaction], CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate] " &
                " UNION ALL " &
                 " Select '" + clsUserMgtCode.ModuleSaleDairy + "' as [Module],'" + clsUserMgtCode.frmPerformaInvoiceDairy + "' as [Transaction], CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate] " &
                " UNION ALL " &
                " Select '" + clsUserMgtCode.ModuleSaleDairy + "' as [Module],'" + "" + "' as [Transaction], CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate] " &
                " UNION ALL " &
                  " Select '" + clsUserMgtCode.ModuleSaleDairy + "' as [Module],'" + "" + "' as [Transaction], CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate] " &
                " UNION ALL " &
                " Select 'Dairy Sale' as [Module],'Dairy Invoice' as [Transaction] , CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate] " &
                " UNION ALL " &
                " Select '" + clsUserMgtCode.ModuleSaleDairy + "' as [Module],'" + clsUserMgtCode.frmDairyGatePass + "' as [Transaction] , CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate] " &
                " UNION ALL " &
                " Select '" + clsUserMgtCode.ModuleMCCMilkProcurement + "' as [Module],'" + clsUserMgtCode.frmOpenMCCShift + "' as [Transaction] , CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate] " &
                 " UNION ALL " &
                " Select '" + clsUserMgtCode.ModuleMCCMilkProcurement + "' as [Module],'" + clsUserMgtCode.MilkReject + "' as [Transaction] , CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate] " &
                " UNION ALL " &
                " Select '" + clsUserMgtCode.ModuleMCCMilkProcurement + "' as [Module],'" + clsUserMgtCode.frmMilkSample + "' as [Transaction] , CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate] " &
                 " UNION ALL " &
                " Select '" + clsUserMgtCode.ModuleMCCMilkProcurement + "' as [Module],'" + clsUserMgtCode.DCSMPIncentiveReco + "' as [Transaction] , CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate] " &
                 " UNION ALL " &
                " Select '" + clsUserMgtCode.ModuleMCCMilkProcurement + "' as [Module],'" + clsUserMgtCode.DBTNEFTUploader + "' as [Transaction] , CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate] " &
                " UNION ALL " &
                " Select '" + clsUserMgtCode.ModuleMCCMilkProcurement + "' as [Module],'" + clsUserMgtCode.MilkShiftUploader + "' as [Transaction] , CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate] " &
                " UNION ALL " &
                " Select '" + clsUserMgtCode.ModuleMCCMilkProcurement + "' as [Module],'" + clsUserMgtCode.MilkVSPPayment + "' as [Transaction] , CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate] " &
                " UNION ALL " &
                " Select '" + clsUserMgtCode.ModuleProductionDairy + "' as [Module],'" + clsUserMgtCode.frmCleaning + "' as [Transaction] , CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate] " &
                 " UNION ALL " &
                " Select '" + clsUserMgtCode.ModuleProductionDairy + "' as [Module],'" + clsUserMgtCode.frmGateOut + "' as [Transaction] , CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate] " &
                " UNION ALL " &
                " Select '" + clsUserMgtCode.ModuleProductionDairy + "' as [Module],'" + clsUserMgtCode.frmTankerProvision + "' as [Transaction] , CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate] " &
                " UNION ALL " &
                " Select '" + clsUserMgtCode.ModuleQualityControl + "' as [Module],'" + clsUserMgtCode.frmQualityCheckForSRN + "' as [Transaction] , CAST(0 as BIT ) as [Lock], CONVERT(date, null, 103) as [FromDate], CONVERT(date, null, 103) as [ToDate] "
        Return Qry
    End Function

    Public Sub LoadBlankDetail()
        Try
            ' qry As String = ""
            If chkLocationSegment.IsChecked = True Then
                StrQuery = LockTransactionNameLocationSegwise()                
            Else
                StrQuery = LockTransactionNameLocationwise()             
            End If
            Dim strNew As String = " Select * from (" + StrQuery + ") xxx order by Module, [Transaction]"
            StrQuery = "Select * from (" + StrQuery + ") xxx"

            LoadBlankGrid()
            Dim dtInitialdetail As DataTable = clsDBFuncationality.GetDataTable(strNew)
            'gvTest.DataSource = dtInitialdetail
            'ArrLoc = txtLocationMult.arrValueMember

            If ArrLoc IsNot Nothing Then
                If ArrLoc.Count > 0 Then
                    For Each dr As DataRow In dtInitialdetail.Rows
                        For i As Integer = 0 To ArrLoc.Count - 1
                            dgvDetails.Rows.AddNew()
                            dgvDetails.CurrentRow.Cells("colLocation").Value = clsCommon.myCstr(ArrLoc.Item(i))
                            dgvDetails.CurrentRow.Cells("colModule").Value = dr("Module")
                            dgvDetails.CurrentRow.Cells("colTransaction").Value = dr("Transaction")
                            dgvDetails.CurrentRow.Cells("colLock").Value = False
                        Next
                    Next
                Else

                    For Each dr As DataRow In dtInitialdetail.Rows
                        dgvDetails.Rows.AddNew()
                        dgvDetails.CurrentRow.Cells("colLocation").Value = ""
                        dgvDetails.CurrentRow.Cells("colModule").Value = dr("Module")
                        dgvDetails.CurrentRow.Cells("colTransaction").Value = dr("Transaction")
                        dgvDetails.CurrentRow.Cells("colLock").Value = False

                    Next
                End If
          
            Else
            For Each dr As DataRow In dtInitialdetail.Rows
                dgvDetails.Rows.AddNew()
                dgvDetails.CurrentRow.Cells("colLocation").Value = ""
                dgvDetails.CurrentRow.Cells("colModule").Value = dr("Module")
                dgvDetails.CurrentRow.Cells("colTransaction").Value = dr("Transaction")
                dgvDetails.CurrentRow.Cells("colLock").Value = False

            Next
            End If

            dgvDetails.CurrentRow = dgvDetails.Rows(0)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub
    Public Sub LoadOriginalDetailMultipleLoc()
        Dim qry As String
        If chkLocationSegment.IsChecked = True Then
            qry = "Select Comp_Code as Company, Location_Segment_Code as Location, Module_Name as Module, Trans_Name as [Transaction], Is_Locked as Lock, Start_Date as FromDate, End_Date as ToDate  from TSPL_LOCK_LOCATION_SEGMENT Where Comp_Code='" + cmbCompany.SelectedValue + "' and Location_Segment_Code in (" + clsCommon.GetMulcallString(txtLocationMult.arrValueMember) + ") Order By  Module, Trans_Name"
        Else
            qry = "Select Comp_Code as Company, Location_Code as Location, Module_Name as Module, Trans_Name as [Transaction], Is_Locked as Lock, Start_Date as FromDate, End_Date as ToDate  from TSPL_LOCK_LOCATION Where Comp_Code='" + cmbCompany.SelectedValue + "' and Location_Code in (" + clsCommon.GetMulcallString(txtLocationMult.arrValueMember) + ") Order By  Module, Trans_Name"
        End If
        Try
            Dim dtOriginaldetail As DataTable = clsDBFuncationality.GetDataTable(qry)
            LoadBlankGrid()
            For Each dr As DataRow In dtOriginaldetail.Rows
                dgvDetails.Rows.AddNew()
                dgvDetails.CurrentRow.Cells("colLocation").Value = dr("Location")
                dgvDetails.CurrentRow.Cells("colModule").Value = dr("Module")
                dgvDetails.CurrentRow.Cells("colTransaction").Value = dr("Transaction")
                If clsCommon.myCstr(dr("Lock")) = 1 Then
                    dgvDetails.CurrentRow.Cells("colLock").Value = True
                    dgvDetails.CurrentRow.Cells("colFromDate").Value = clsCommon.GetPrintDate(dr("FromDate"), "dd/MM/yyyy")
                    dgvDetails.CurrentRow.Cells("colToDate").Value = clsCommon.GetPrintDate(dr("ToDate"), "dd/MM/yyyy")
                    dgvDetails.CurrentRow.Cells("colFromDate").ReadOnly = False
                    dgvDetails.CurrentRow.Cells("colToDate").ReadOnly = False
                Else
                    dgvDetails.CurrentRow.Cells("colLock").Value = False
                    dgvDetails.CurrentRow.Cells("colFromDate").ReadOnly = True
                    dgvDetails.CurrentRow.Cells("colToDate").ReadOnly = True
                End If
                Dim strSql As String = ""
                If chkLocationSegment.IsChecked = True Then
                    strSql = "select TSPL_LOCK_LOCATION_SEGMENT_USER.user_code,User_Name,Todate from TSPL_LOCK_LOCATION_SEGMENT_USER left outer join " & _
           "tspL_USER_MASTER on TSPL_LOCK_LOCATION_SEGMENT_USER.User_Code=tspL_USER_MASTER.User_Code " & _
           "where Location_Segment_Code  ='" & clsCommon.myCstr(dgvDetails.CurrentRow.Cells("colLocation").Value) & "'   and Module_Name='" & clsCommon.myCstr(dgvDetails.CurrentRow.Cells("colModule").Value) & "' " & _
           "and Trans_Name='" & dgvDetails.CurrentRow.Cells("colTransaction").Value & "'"

                    Dim dt As DataTable = clsDBFuncationality.GetDataTable(strSql)
                    'Dim arr As New ArrayList
                    Dim Arr As List(Of clsLockTransactionLocationSegmentUserwise) = Nothing
                    Arr = New List(Of clsLockTransactionLocationSegmentUserwise)
                    For Each dr1 As DataRow In dt.Rows
                        Dim obj As clsLockTransactionLocationSegmentUserwise = New clsLockTransactionLocationSegmentUserwise()
                        obj.Status = 1
                        obj.User_Code = dr1("user_code")
                        obj.User_Name = dr1("User_Name")
                        obj.ToDate = dr1("Todate")
                        Arr.Add(obj)
                    Next

                    dgvDetails.CurrentRow.Cells("colTransaction").Tag = Arr
                Else
                    strSql = "select TSPL_LOCK_LOCATION_USER.user_code,User_Name,Todate from TSPL_LOCK_LOCATION_USER left outer join " & _
           "tspL_USER_MASTER on TSPL_LOCK_LOCATION_USER.User_Code=tspL_USER_MASTER.User_Code " & _
           "where Location_Code  ='" & clsCommon.myCstr(dgvDetails.CurrentRow.Cells("colLocation").Value) & "'   and Module_Name='" & clsCommon.myCstr(dgvDetails.CurrentRow.Cells("colModule").Value) & "' " & _
           "and Trans_Name='" & dgvDetails.CurrentRow.Cells("colTransaction").Value & "'"

                    Dim dt As DataTable = clsDBFuncationality.GetDataTable(strSql)
                    'Dim arr As New ArrayList
                    Dim Arr As List(Of clsLockTransactionLocationUserwise) = Nothing
                    Arr = New List(Of clsLockTransactionLocationUserwise)
                    For Each dr1 As DataRow In dt.Rows
                        Dim obj As clsLockTransactionLocationUserwise = New clsLockTransactionLocationUserwise()
                        obj.Status = 1
                        obj.User_Code = dr1("user_code")
                        obj.User_Name = dr1("User_Name")
                        obj.ToDate = dr1("Todate")
                        Arr.Add(obj)
                    Next

                    dgvDetails.CurrentRow.Cells("colTransaction").Tag = Arr
                End If

             
            Next
            'ArrLoc = txtLocationMult.arrValueMember
            Dim str As String = ""
            For i As Integer = 0 To ArrLoc.Count - 1
                If chkLocationSegment.IsChecked = True Then
                    str = "select SS.Module,SS.[Transaction] from (" + StrQuery + ") as SS  " & _
            " except" & _
            " Select  Module_Name as Module, Trans_Name as [Transaction]  from TSPL_LOCK_LOCATION_SEGMENT Where Comp_Code='" + cmbCompany.SelectedValue + "' AND Location_Segment_Code ='" & clsCommon.myCstr(ArrLoc.Item(i)) & "'"
                Else
                    str = "select SS.Module,SS.[Transaction] from (" + StrQuery + ") as SS  " & _
            " except" & _
            " Select  Module_Name as Module, Trans_Name as [Transaction]  from TSPL_LOCK_LOCATION Where Comp_Code='" + cmbCompany.SelectedValue + "' AND Location_Code  ='" & clsCommon.myCstr(ArrLoc.Item(i)) & "'"
                End If
                dtOriginaldetail = clsDBFuncationality.GetDataTable(str)
                If dtOriginaldetail IsNot Nothing AndAlso dtOriginaldetail.Rows.Count > 0 Then
                    For Each dr As DataRow In dtOriginaldetail.Rows
                        dgvDetails.Rows.AddNew()
                        dgvDetails.CurrentRow.Cells("colLocation").Value = clsCommon.myCstr(ArrLoc.Item(i))
                        dgvDetails.CurrentRow.Cells("colModule").Value = dr("Module")
                        dgvDetails.CurrentRow.Cells("colTransaction").Value = dr("Transaction")
                        dgvDetails.CurrentRow.Cells("colLock").Value = False
                        dgvDetails.CurrentRow.Cells("colFromDate").ReadOnly = True
                        dgvDetails.CurrentRow.Cells("colToDate").ReadOnly = True

                    Next
                End If
            Next

            dgvDetails.CurrentRow = dgvDetails.Rows(0)

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Public Sub LoadOriginalDetail()
        Dim qry As String
        If chkLocationSegment.IsChecked = True Then
            qry = "Select Comp_Code as Company, Location_Segment_Code as Location, Module_Name as Module, Trans_Name as [Transaction], Is_Locked as Lock, Start_Date as FromDate, End_Date as ToDate  from TSPL_LOCK_LOCATION_SEGMENT Where Comp_Code='" + cmbCompany.SelectedValue + "' AND Location_Segment_Code ='" & txtlocation.Value & "' Order By  Module, Trans_Name"
        Else
            qry = "Select Comp_Code as Company, Location_Code as Location, Module_Name as Module, Trans_Name as [Transaction], Is_Locked as Lock, Start_Date as FromDate, End_Date as ToDate  from TSPL_LOCK_LOCATION Where Comp_Code='" + cmbCompany.SelectedValue + "' AND Location_Code  ='" & txtlocation.Value & "'  Order By  Module, Trans_Name"
        End If
        Try
            Dim dtOriginaldetail As DataTable = clsDBFuncationality.GetDataTable(qry)
            LoadBlankGrid()
            For Each dr As DataRow In dtOriginaldetail.Rows
                dgvDetails.Rows.AddNew()
                dgvDetails.CurrentRow.Cells("colLocation").Value = dr("Location")
                dgvDetails.CurrentRow.Cells("colModule").Value = dr("Module")
                dgvDetails.CurrentRow.Cells("colTransaction").Value = dr("Transaction")
                If clsCommon.myCstr(dr("Lock")) = 1 Then
                    dgvDetails.CurrentRow.Cells("colLock").Value = True
                    dgvDetails.CurrentRow.Cells("colFromDate").Value = clsCommon.GetPrintDate(dr("FromDate"), "dd/MM/yyyy")
                    dgvDetails.CurrentRow.Cells("colToDate").Value = clsCommon.GetPrintDate(dr("ToDate"), "dd/MM/yyyy")
                    dgvDetails.CurrentRow.Cells("colFromDate").ReadOnly = False
                    dgvDetails.CurrentRow.Cells("colToDate").ReadOnly = False
                Else
                    dgvDetails.CurrentRow.Cells("colLock").Value = False
                    dgvDetails.CurrentRow.Cells("colFromDate").ReadOnly = True
                    dgvDetails.CurrentRow.Cells("colToDate").ReadOnly = True
                End If
                Dim strSql As String = ""
                If chkLocationSegment.IsChecked = True Then
                    strSql = "select TSPL_LOCK_LOCATION_SEGMENT_USER.user_code,User_Name from TSPL_LOCK_LOCATION_SEGMENT_USER left outer join " & _
           "tspL_USER_MASTER on TSPL_LOCK_LOCATION_SEGMENT_USER.User_Code=tspL_USER_MASTER.User_Code " & _
           "where Location_Segment_Code  ='" & txtlocation.Value & "'   and Module_Name='" & clsCommon.myCstr(dgvDetails.CurrentRow.Cells("colModule").Value) & "' " & _
           "and Trans_Name='" & dgvDetails.CurrentRow.Cells("colTransaction").Value & "'"
                Else
                    strSql = "select TSPL_LOCK_LOCATION_USER.user_code,User_Name from TSPL_LOCK_LOCATION_USER left outer join " & _
           "tspL_USER_MASTER on TSPL_LOCK_LOCATION_USER.User_Code=tspL_USER_MASTER.User_Code " & _
           "where Location_Code  ='" & txtlocation.Value & "'   and Module_Name='" & clsCommon.myCstr(dgvDetails.CurrentRow.Cells("colModule").Value) & "' " & _
           "and Trans_Name='" & dgvDetails.CurrentRow.Cells("colTransaction").Value & "'"
                End If
              
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(strSql)
                Dim arr As New ArrayList

                For Each dr1 As DataRow In dt.Rows
                    arr.Add(dr1("user_code"))
                Next
                dgvDetails.CurrentRow.Cells("colTransaction").Tag = arr
            Next

            Dim str As String = ""
            If chkLocationSegment.IsChecked = True Then
                str = "select SS.Module,SS.[Transaction] from (" + StrQuery + ") as SS  " & _
        " except" & _
        " Select  Module_Name as Module, Trans_Name as [Transaction]  from TSPL_LOCK_LOCATION_SEGMENT Where Comp_Code='" + cmbCompany.SelectedValue + "' AND Location_Segment_Code  ='" & txtlocation.Value & "' "
            Else
                str = "select SS.Module,SS.[Transaction] from (" + StrQuery + ") as SS  " & _
        " except" & _
        " Select  Module_Name as Module, Trans_Name as [Transaction]  from TSPL_LOCK_LOCATION Where Comp_Code='" + cmbCompany.SelectedValue + "' AND Location_Code  ='" & txtlocation.Value & "' "
            End If
           
            dtOriginaldetail = clsDBFuncationality.GetDataTable(str)
            If dtOriginaldetail IsNot Nothing AndAlso dtOriginaldetail.Rows.Count > 0 Then
                For Each dr As DataRow In dtOriginaldetail.Rows
                    dgvDetails.Rows.AddNew()
                    dgvDetails.CurrentRow.Cells("colModule").Value = dr("Module")
                    dgvDetails.CurrentRow.Cells("colTransaction").Value = dr("Transaction")
                    dgvDetails.CurrentRow.Cells("colLock").Value = False
                    dgvDetails.CurrentRow.Cells("colFromDate").ReadOnly = True
                    dgvDetails.CurrentRow.Cells("colToDate").ReadOnly = True
                Next
            End If





            dgvDetails.CurrentRow = dgvDetails.Rows(0)

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    

    Private Sub SetUserMgmtNew()
        '' Anubhooti 31-July-2014 BM00000003131
        'MyBase.SetUserMgmt(clsUserMgtCode.lockTransaction)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
            Throw New Exception("Permission Denied")
            'Me.Close()
            'Exit Function
        End If
        btnLock.Visible = MyBase.isPostFlag
    End Sub
    Sub OpenUser()
        If clsCommon.myCBool(dgvDetails.CurrentRow.Cells("colLock").Value) Then

            Dim frm As frmLockLoctionUserwise = New frmLockLoctionUserwise()
            frm.strLocCode = clsCommon.myCstr(dgvDetails.CurrentRow.Cells("colLocation").Value)
            frm.strLocname = clsLocation.GetName(clsCommon.myCstr(dgvDetails.CurrentRow.Cells("colLocation").Value), Nothing)
            frm.strModule = clsCommon.myCstr(dgvDetails.CurrentRow.Cells("colModule").Value)
            frm.strTransName = clsCommon.myCstr(dgvDetails.CurrentRow.Cells("coltransaction").Value)

            If chkLocationSegment.IsChecked = True Then
                frm.blnLocationwsie = False
                frm.arr1 = TryCast(dgvDetails.CurrentRow.Cells("coltransaction").Tag, List(Of clsLockTransactionLocationSegmentUserwise))
                frm.ShowDialog()
                If Not frm.isCencelButtonClicked Then
                    dgvDetails.CurrentRow.Cells("coltransaction").Tag = frm.arr1
                End If
            Else
                frm.blnLocationwsie = True
                frm.arr = TryCast(dgvDetails.CurrentRow.Cells("coltransaction").Tag, List(Of clsLockTransactionLocationUserwise))
                frm.ShowDialog()
                If Not frm.isCencelButtonClicked Then
                    dgvDetails.CurrentRow.Cells("coltransaction").Tag = frm.arr
                End If
            End If
           
        End If
    End Sub
    Private Sub dgvDetails_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles dgvDetails.CellDoubleClick
        Try
            If AllowLockTransactionUserwise = 1 Then
                If clsCommon.myLen(txtLocationMult.arrValueMember) = 0 AndAlso chkAllLoc.Checked = False Then
                    common.clsCommon.MyMessageBoxShow("Please Select location ")
                    txtlocation.Focus()
                    Exit Sub
                Else
                    If e.Column Is dgvDetails.Columns("colTransaction") Then
                        OpenUser()
                    End If
                End If
            End If
            

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

   
    Private Sub dgvDetails_CellValueChanged_1(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles dgvDetails.CellValueChanged
        Try

            If e.Column Is dgvDetails.Columns("colLock") Then
                If dgvDetails.CurrentRow.Cells("colLock").Value = True Then
                    dgvDetails.CurrentRow.Cells("colFromDate").ReadOnly = False
                    dgvDetails.CurrentRow.Cells("colToDate").ReadOnly = False
                Else
                    dgvDetails.CurrentRow.Cells("colFromDate").Value = Nothing
                    dgvDetails.CurrentRow.Cells("colFromDate").ReadOnly = True
                    dgvDetails.CurrentRow.Cells("colToDate").Value = Nothing
                    dgvDetails.CurrentRow.Cells("colToDate").ReadOnly = True
                End If
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnLock_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLock.Click
        If clsCommon.myLen(txtLocationMult.arrValueMember) = 0 AndAlso chkAllLoc.Checked = False Then
            common.clsCommon.MyMessageBoxShow("Please Select location ")
            txtlocation.Focus()
            Exit Sub
        End If
        Dim LIneNo As Integer = 0
        For Each grow As GridViewRowInfo In dgvDetails.Rows
            LIneNo = LIneNo + 1
            If grow.Cells("colLock").Value = True Then
                If clsCommon.myLen(grow.Cells("colFromDate").Value) = 0 AndAlso dtpFromdate1.Text <> "" Then
                    grow.Cells("colFromDate").Value = dtpFromdate1.Text
                End If
                If clsCommon.myLen(grow.Cells("colFromDate").Value) = 0 Then
                    common.clsCommon.MyMessageBoxShow("Select Start Date at Line '" + clsCommon.myCstr(LIneNo) + "'")
                    Exit Sub
                End If
                If clsCommon.myLen(grow.Cells("colToDate").Value) = 0 Then
                    common.clsCommon.MyMessageBoxShow("Select To Date at Line '" + clsCommon.myCstr(LIneNo) + "'")
                    Exit Sub
                End If
                If clsCommon.myLen(grow.Cells("colFromDate").Value) > 0 AndAlso clsCommon.myLen(grow.Cells("colToDate").Value) > 0 Then
                    If grow.Cells("colFromDate").Value > grow.Cells("colToDate").Value Then
                        common.clsCommon.MyMessageBoxShow("Start Date Can Not be Greater Than End Date At Line No " + clsCommon.myCstr(LIneNo) + "")
                        Exit Sub
                    End If
                End If
            End If
        Next

        LockTrans()
    End Sub

    Public Sub LockTrans()
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        'ArrLoc.Clear()
        Dim qry As String
        Try
            '==for multiple location
            Dim LocationFirstTime As Integer = 0
            ' done by priti KDI/19/06/18-000375
            If chkAllLoc.Checked = True Then
                If chkLocationSegment.IsChecked = True Then
                    clsDBFuncationality.ExecuteNonQuery("Delete from TSPL_LOCK_LOCATION_SEGMENT Where Comp_Code='" + cmbCompany.SelectedValue + "'", trans)
                    clsDBFuncationality.ExecuteNonQuery("Delete from TSPL_LOCK_LOCATION_SEGMENT_USER Where Comp_Code='" + cmbCompany.SelectedValue + "'", trans)
                    qry = " Select Segment_code as Code from " + DbName + ".dbo.TSPL_GL_SEGMENT_CODE Where Seg_No=7 "
                Else
                    clsDBFuncationality.ExecuteNonQuery("Delete from TSPL_LOCK_LOCATION Where Comp_Code='" + cmbCompany.SelectedValue + "'", trans)
                    clsDBFuncationality.ExecuteNonQuery("Delete from TSPL_LOCK_LOCATION_USER Where Comp_Code='" + cmbCompany.SelectedValue + "'", trans)
                    qry = " Select Location_Code as Code from " + DbName + ".dbo.TSPL_LOCATION_MASTER Where Location_Type='Physical' "
                End If

                Dim dtLoc As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                ArrLoc.Clear()
                For Each dr As DataRow In dtLoc.Rows
                    ArrLoc.Add(dr("Code"))
                Next

                If chkLocationSegment.IsChecked = True Then
                    For i As Integer = 0 To ArrLoc.Count - 1
                        Dim arr As New List(Of clsLockTransactionLocationSegmentwise)
                        For Each grow As GridViewRowInfo In dgvDetails.Rows
                            Dim obj As New clsLockTransactionLocationSegmentwise
                            obj.Location_Segment_Code = clsCommon.myCstr(ArrLoc(i))
                            obj.Module_Name = clsCommon.myCstr(grow.Cells("colModule").Value)
                            obj.Trans_Name = clsCommon.myCstr(grow.Cells("colTransaction").Value)
                            obj.Is_Locked = clsCommon.myCstr(IIf(grow.Cells("colLock").Value = True, 1, 0))
                            obj.Start_Date = clsCommon.GetPrintDate(grow.Cells("colFromDate").Value, "dd/MMM/yyyy")
                            obj.End_Date = clsCommon.GetPrintDate(grow.Cells("colToDate").Value, "dd/MMM/yyyy")
                            arr.Add(obj)

                            If grow.Cells("colLock").Value Then
                                Dim arr1 As New List(Of clsLockTransactionLocationSegmentUserwise)
                                Dim ArrUser As List(Of clsLockTransactionLocationSegmentUserwise) = Nothing
                                ArrUser = TryCast(grow.Cells("colTransaction").Tag, List(Of clsLockTransactionLocationSegmentUserwise))

                                If ArrUser IsNot Nothing Then
                                    For Each objtr As clsLockTransactionLocationSegmentUserwise In ArrUser
                                        'Dim objtr As New clsLockTransactionLocationSegmentUserwise
                                        objtr.Location_Segment_Code = obj.Location_Segment_Code
                                        objtr.Module_Name = obj.Module_Name
                                        objtr.Trans_Name = obj.Trans_Name

                                        arr1.Add(objtr)
                                    Next
                                    clsLockTransactionLocationSegmentUserwise.SaveData("", "", arr1, trans)
                                End If
                            End If

                        Next
                        clsLockTransactionLocationSegmentwise.SaveData("", "", arr, trans)
                    Next
                Else
                    For i As Integer = 0 To ArrLoc.Count - 1
                        Dim arr As New List(Of clsLockTransactionLocationwise)
                        For Each grow As GridViewRowInfo In dgvDetails.Rows
                            Dim obj As New clsLockTransactionLocationwise
                            obj.Location_Code = clsCommon.myCstr(ArrLoc(i))
                            obj.Module_Name = clsCommon.myCstr(grow.Cells("colModule").Value)
                            obj.Trans_Name = clsCommon.myCstr(grow.Cells("colTransaction").Value)
                            obj.Is_Locked = clsCommon.myCstr(IIf(grow.Cells("colLock").Value = True, 1, 0))
                            obj.Start_Date = clsCommon.GetPrintDate(grow.Cells("colFromDate").Value, "dd/MMM/yyyy")
                            obj.End_Date = clsCommon.GetPrintDate(grow.Cells("colToDate").Value, "dd/MMM/yyyy")
                            arr.Add(obj)
                            Dim arr1 As New List(Of clsLockTransactionLocationUserwise)
                            'Dim ArrUser As ArrayList

                            If grow.Cells("colLock").Value Then
                                Dim ArrUser As List(Of clsLockTransactionLocationUserwise) = Nothing
                                ArrUser = TryCast(grow.Cells("colTransaction").Tag, List(Of clsLockTransactionLocationUserwise))
                                If ArrUser IsNot Nothing Then
                                    For Each objtr As clsLockTransactionLocationUserwise In ArrUser
                                        'Dim objtr As New clsLockTransactionLocationUserwise
                                        objtr.Location_Code = obj.Location_Code
                                        objtr.Module_Name = obj.Module_Name
                                        objtr.Trans_Name = obj.Trans_Name
                                        objtr.User_Code = objtr.User_Code
                                        arr1.Add(objtr)
                                    Next
                                    clsLockTransactionLocationUserwise.SaveData("", "", arr1, trans)
                                End If

                            End If

                        Next
                        clsLockTransactionLocationwise.SaveData("", "", arr, trans)
                    Next
                End If
            Else
                If chkLocationSegment.IsChecked = True Then
                    clsDBFuncationality.ExecuteNonQuery("Delete from TSPL_LOCK_LOCATION_SEGMENT Where Comp_Code='" + cmbCompany.SelectedValue + "' AND Location_Segment_Code in (" + clsCommon.GetMulcallString(txtLocationMult.arrValueMember) + ")", trans)
                    clsDBFuncationality.ExecuteNonQuery("Delete from TSPL_LOCK_LOCATION_SEGMENT_USER Where Comp_Code='" + cmbCompany.SelectedValue + "' AND Location_Segment_Code in (" + clsCommon.GetMulcallString(txtLocationMult.arrValueMember) + ")", trans)
                Else

                    Dim strQ = "Delete from TSPL_LOCK_LOCATION Where Comp_Code='" + cmbCompany.SelectedValue + "' AND Location_Code  in (" + clsCommon.GetMulcallString(txtLocationMult.arrValueMember) + ")"
                    clsDBFuncationality.ExecuteNonQuery(strQ, trans)
                    strQ = "Delete from TSPL_LOCK_LOCATION_USER Where Comp_Code='" + cmbCompany.SelectedValue + "' AND Location_Code  in (" + clsCommon.GetMulcallString(txtLocationMult.arrValueMember) + ")"
                    clsDBFuncationality.ExecuteNonQuery(strQ, trans)
                End If

                If chkLocationSegment.IsChecked = True Then
                    'For i As Integer = 0 To ArrLoc.Count - 1
                    Dim arr As New List(Of clsLockTransactionLocationSegmentwise)
                    For Each grow As GridViewRowInfo In dgvDetails.Rows
                        Dim obj As New clsLockTransactionLocationSegmentwise
                        obj.Location_Segment_Code = clsCommon.myCstr(grow.Cells("colLocation").Value)
                        obj.Module_Name = clsCommon.myCstr(grow.Cells("colModule").Value)
                        obj.Trans_Name = clsCommon.myCstr(grow.Cells("colTransaction").Value)
                        obj.Is_Locked = clsCommon.myCstr(IIf(grow.Cells("colLock").Value = True, 1, 0))
                        obj.Start_Date = clsCommon.GetPrintDate(grow.Cells("colFromDate").Value, "dd/MMM/yyyy")
                        obj.End_Date = clsCommon.GetPrintDate(grow.Cells("colToDate").Value, "dd/MMM/yyyy")
                        arr.Add(obj)

                        If grow.Cells("colLock").Value Then
                            Dim arr1 As New List(Of clsLockTransactionLocationSegmentUserwise)
                            Dim ArrUser As List(Of clsLockTransactionLocationSegmentUserwise) = Nothing
                            ArrUser = TryCast(grow.Cells("colTransaction").Tag, List(Of clsLockTransactionLocationSegmentUserwise))

                            If ArrUser IsNot Nothing Then
                                For Each objtr As clsLockTransactionLocationSegmentUserwise In ArrUser
                                    'Dim objtr As New clsLockTransactionLocationSegmentUserwise
                                    objtr.Location_Segment_Code = obj.Location_Segment_Code
                                    objtr.Module_Name = obj.Module_Name
                                    objtr.Trans_Name = obj.Trans_Name

                                    arr1.Add(objtr)
                                Next
                                clsLockTransactionLocationSegmentUserwise.SaveData("", "", arr1, trans)
                            End If
                        End If

                    Next
                    clsLockTransactionLocationSegmentwise.SaveData("", "", arr, trans)
                    'Next
                Else
                    Dim arr As New List(Of clsLockTransactionLocationwise)
                    'For i As Integer = 0 To ArrLoc.Count - 1
                    For Each grow As GridViewRowInfo In dgvDetails.Rows
                        Dim obj As New clsLockTransactionLocationwise
                        obj.Location_Code = clsCommon.myCstr(grow.Cells("colLocation").Value)
                        obj.Module_Name = clsCommon.myCstr(grow.Cells("colModule").Value)
                        obj.Trans_Name = clsCommon.myCstr(grow.Cells("colTransaction").Value)
                        obj.Is_Locked = clsCommon.myCstr(IIf(grow.Cells("colLock").Value = True, 1, 0))
                        obj.Start_Date = clsCommon.GetPrintDate(grow.Cells("colFromDate").Value, "dd/MMM/yyyy")
                        obj.End_Date = clsCommon.GetPrintDate(grow.Cells("colToDate").Value, "dd/MMM/yyyy")
                        arr.Add(obj)
                        Dim arr1 As New List(Of clsLockTransactionLocationUserwise)
                        'Dim ArrUser As ArrayList

                        If grow.Cells("colLock").Value Then
                            Dim ArrUser As List(Of clsLockTransactionLocationUserwise) = Nothing
                            ArrUser = TryCast(grow.Cells("colTransaction").Tag, List(Of clsLockTransactionLocationUserwise))
                            If ArrUser IsNot Nothing Then
                                For Each objtr As clsLockTransactionLocationUserwise In ArrUser
                                    'Dim objtr As New clsLockTransactionLocationUserwise
                                    objtr.Location_Code = obj.Location_Code
                                    objtr.Module_Name = obj.Module_Name
                                    objtr.Trans_Name = obj.Trans_Name
                                    objtr.User_Code = objtr.User_Code
                                    arr1.Add(objtr)
                                Next
                                clsLockTransactionLocationUserwise.SaveData("", "", arr1, trans)
                            End If

                        End If

                    Next
                    clsLockTransactionLocationwise.SaveData("", "", arr, trans)
                    'Next
                End If

            End If

            trans.Commit()
            common.clsCommon.MyMessageBoxShow("Locked Successfully")

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            trans.Rollback()
        End Try
    End Sub

    
    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub cmbCompany_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.Data.PositionChangedEventArgs) Handles cmbCompany.SelectedIndexChanged
        DbName = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select DataBase_Name from TSPL_COMPANY_MASTER Where Comp_Code='" + cmbCompany.SelectedValue + "'"))
        LoadBlankDetail()
        'txtlocation.Value = ""
        chkAllLoc.Checked = False
    End Sub

    
    Private Sub chkLocationCode_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocationCode.ToggleStateChanged
        ArrLoc.Clear()
        txtLocationMult.arrValueMember = Nothing
        'txtLocationMult.arrDispalyMember = Nothing
        'txtLocationMult.arrValueMember = Nothing
        LoadBlankDetail()
    End Sub

    Private Sub chkLocationSegment_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocationSegment.ToggleStateChanged
        ArrLoc.Clear()
        txtLocationMult.arrValueMember = Nothing
        'txtLocationMult.arrDispalyMember = Nothing
        'txtLocationMult.arrValueMember = Nothing
        LoadBlankDetail()
    End Sub

    Private Sub chkread_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkread.ToggleStateChanged
        If chkread.Checked = True Then

            For i As Integer = 0 To dgvDetails.Rows.Count - 1
                dgvDetails.Rows(i).Cells("colLock").Value = True
            Next
        ElseIf chkread.Checked = False Then

            For i As Integer = 0 To dgvDetails.Rows.Count - 1
                dgvDetails.Rows(i).Cells("colLock").Value = False
            Next

        End If
    End Sub

    Private Sub txtLocationMult__My_Click(sender As Object, e As EventArgs) Handles txtLocationMult._My_Click
        'Dim qry As String = " select Location_Code as Code, Location_Desc as Name from TSPL_LOCATION_MASTER Where Location_Type='Physical'"
        'txtLocationMult.arrValueMember = clsCommon.ShowMultipleSelectForm("MulLoc", qry, "Code", "Name", txtLocationMult.arrValueMember, txtLocationMult.arrDispalyMember)
        Try
            Dim qry As String
            'Dim WhrCls As String
            Dim Count As Integer
            If chkLocationSegment.IsChecked = True Then
                qry = " Select Segment_code as Code, Description as Name  from TSPL_GL_SEGMENT_CODE where Seg_No=7 "
                txtLocationMult.arrValueMember = clsCommon.ShowMultipleSelectForm("MulLoc", qry, "Code", "Name", txtLocationMult.arrValueMember, txtLocationMult.arrDispalyMember)
                ArrLoc = txtLocationMult.arrValueMember
                qry = "Select COUNT(*)  from TSPL_LOCK_LOCATION_SEGMENT Where Comp_Code='" + cmbCompany.SelectedValue + "' AND Location_Segment_Code in (" + clsCommon.GetMulcallString(txtLocationMult.arrValueMember) + ")"
            Else
                qry = " Select Location_Code as Code, Location_Desc as Name from TSPL_LOCATION_MASTER where Location_Type='Physical'"
                txtLocationMult.arrValueMember = clsCommon.ShowMultipleSelectForm("MulLoc", qry, "Code", "Name", txtLocationMult.arrValueMember, txtLocationMult.arrDispalyMember)
                ArrLoc = txtLocationMult.arrValueMember
                qry = "Select COUNT(*)  from TSPL_LOCK_LOCATION Where Comp_Code='" + cmbCompany.SelectedValue + "' AND Location_Code in (" + clsCommon.GetMulcallString(txtLocationMult.arrValueMember) + ")"
            End If
            Count = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
            If Count <= 0 Then
                LoadBlankDetail()
            Else

                LoadOriginalDetailMultipleLoc()
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        If dtpFromdate1.Text <> "" Then

            For i As Integer = 0 To dgvDetails.Rows.Count - 1
                dgvDetails.Rows(i).Cells("colFromDate").Value = dtpFromdate1.Text
            Next
        End If
        If dtpToDate1.Text <> "" Then

            For i As Integer = 0 To dgvDetails.Rows.Count - 1
                dgvDetails.Rows(i).Cells("colToDate").Value = dtpToDate1.Text
            Next

        End If
    End Sub

    Private Sub txtlocation__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtlocation._MYValidating
        Try
            Dim qry As String
            Dim WhrCls As String
            Dim Count As Integer
            If chkLocationSegment.IsChecked = True Then
                qry = " Select Segment_code as Code, Description from TSPL_GL_SEGMENT_CODE  "
                WhrCls = " Seg_No=7"
                txtlocation.Value = clsCommon.ShowSelectForm("LocFinderLockTrans1", qry, "Code", WhrCls, txtlocation.Value, "Code", isButtonClicked)
                qry = "Select COUNT(*)  from TSPL_LOCK_LOCATION_SEGMENT Where Comp_Code='" + cmbCompany.SelectedValue + "' AND Location_Segment_Code='" + txtlocation.Value + "'"
            Else
                qry = " Select Location_Code as Code, Location_Desc as Description from TSPL_LOCATION_MASTER "
                WhrCls = " Location_Type='Physical'"
                txtlocation.Value = clsCommon.ShowSelectForm("LocFinderLockTrans2", qry, "Code", WhrCls, txtlocation.Value, "Code", isButtonClicked)
                qry = "Select COUNT(*)  from TSPL_LOCK_LOCATION Where Comp_Code='" + cmbCompany.SelectedValue + "' AND Location_Code='" + txtlocation.Value + "'"
            End If
            Count = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
            If Count <= 0 Then
                LoadBlankDetail()
            Else
                LoadOriginalDetailMultipleLoc()
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub dgvDetails_Click(sender As Object, e As EventArgs) Handles dgvDetails.Click

    End Sub

    Private Sub btnLockUser_Click(sender As Object, e As EventArgs) Handles btnLockUser.Click
        Try
            clsDBFuncationality.ExecuteNonQuery("Delete from TSPL_LOCK_LOCATION_SEGMENT_USER ")
            clsDBFuncationality.ExecuteNonQuery("Delete from TSPL_LOCK_LOCATION_USER ")
            clsCommon.MyMessageBoxShow("All User Locked successfully")
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub chkAllLoc_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkAllLoc.ToggleStateChanged
        LoadOriginalDetail()
        If chkread.Checked = True Then
            For i As Integer = 0 To dgvDetails.Rows.Count - 1
                dgvDetails.Rows(i).Cells("colLock").Value = True
            Next
        ElseIf chkread.Checked = False Then
            For i As Integer = 0 To dgvDetails.Rows.Count - 1
                dgvDetails.Rows(i).Cells("colLock").Value = False
            Next
        End If
        For Each grow As GridViewRowInfo In dgvDetails.Rows
            grow.Cells("colTransaction").Tag = Nothing
        Next

        If chkAllLoc.Checked Then
            txtLocationMult.Enabled = False
            txtLocationMult.arrValueMember = Nothing
        Else
            txtLocationMult.Enabled = True
        End If
    End Sub
End Class
