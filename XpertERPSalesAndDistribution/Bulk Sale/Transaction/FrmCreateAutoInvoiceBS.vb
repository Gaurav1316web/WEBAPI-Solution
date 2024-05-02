
'--------Created By Richa 18/11/2014 Against Ticket No BM00000003894
Imports System.Data.SqlClient
Imports common
Imports System.IO

Public Class FrmCreateAutoInvoiceBS
    Inherits FrmMainTranScreen
#Region "Variables"
    Dim userCode, companyCode As String
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private isNewEntry As Boolean = False
    Dim isCellValueChangedOpen As Boolean
    Public isInsideLoadData As Boolean = False
    Dim dtAllData As DataTable = Nothing
    Dim dtAllDataDetail As DataTable = Nothing
    Dim dtmain As DataTable = Nothing
    Public Const colSlNo As String = "SLNO"
    Public Const colDispatchNo As String = "colDispatchNo"
    Public Const colDispatchDate As String = "colDispatchDate"
    Public Const colTankerNo As String = "colTankerNo"
    Public Const colItemCode As String = "colItemCode"
    Public Const colItemDesc As String = "colItemDesc"
    Public Const colUnitCode As String = "colUnitCode"
    Public Const colDispatchQty As String = "colDispatchQty"
    Public Const colDispatchFatPer As String = "colDispatchFatPer"
    Public Const colDispatchSnfPer As String = "colDispatchSnfPer"
    Public Const colDispatchRate As String = "colDispatchRate"
    Public Const colDispatchAmount As String = "colDispatchAmount"
    Public Const ColInvoiceQty As String = "ColInvoiceQty"
    Public Const ColInvoiceFatPer As String = "ColInvoiceFatPer"
    Public Const colInvoiceSnfPer As String = "colInvoiceSnfPer"
    Public Const colInvoiceRate As String = "colInvoiceRate"
    Public Const colInvoiceAmount As String = "colInvoiceAmount"
    Public Const colSNFWeightage As String = "colSNFWeightage"
    Public Const colFatWeightage As String = "colFatWeightage"
    Public Const colFatKG As String = "colFATKG"
    Public Const colSNFKG As String = "colSNFKG"
    Public Const colLocationName As String = "colLocationName"
    Public Const colLocation As String = "colLocation"
    Public Const colCustomerName As String = "colCustomerName"
    Public Const colCustomer As String = "colCustomer"
#End Region
    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub FrmCreateAutoInvoiceBS_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown

      
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnsave.Enabled Then
            btnsave_Click(btnsave, e)
            'SaveData()
           
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnclose.Enabled Then
            CloseForm()
        End If
    End Sub

    Sub CloseForm()
        clsERPFuncationality.closeForm(Me)
    End Sub
    Sub Reset()
        fndPriceCode.Value = ""
        btnsave.Text = "Proceed to Create Invoice"
        btnsave.Enabled = True
        UcAttachment1.BlankAllControls()
        isNewEntry = True
        loadBlankItemGrid()
        LoadCustomer()
        LoadLocation()
        ReStoreGridLayout()
        dtpFromdate.Value = clsCommon.GETSERVERDATE()
        dtptodate.Value = clsCommon.GETSERVERDATE()
        TxtInvoiceDate.Value = clsCommon.GETSERVERDATE()
        txtTotalAmt.Value = 0
        txtTotalQty.Value = 0
        isNewEntry = True
        chkCustomerAll.IsChecked = True
        chkLocationAll.IsChecked = True
    End Sub
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.FrmInvoiceBulkSale)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnsave.Visible = MyBase.isModifyFlag
        ' btndelete.Visible = MyBase.isDeleteFlag
        'btnPost.Visible = MyBase.isPostFlag
    End Sub
    Private Sub FrmCreateAutoInvoiceBS_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        Reset()
        UcAttachment1.Form_ID = MyBase.Form_ID
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update Transaction")
        
    End Sub

    Public Sub LoadCustomer()
        cbgCustomer.DataSource = clsDBFuncationality.GetDataTable("Select Cust_Code as Code,Customer_Name as Name from TSPL_CUSTOMER_MASTER ")
        cbgCustomer.ValueMember = "Code"
        cbgCustomer.DisplayMember = "Name"
    End Sub
    Public Sub LoadLocation()
        cbgLocation.DataSource = clsDBFuncationality.GetDataTable("Select Location_Code as Code,Location_Desc as Name from TSPL_LOCATION_MASTER where Location_Type='Physical' and Is_Section='N' and Is_Sub_Location='N'")
        cbgLocation.ValueMember = "Code"
        cbgLocation.DisplayMember = "Name"
    End Sub
    Private Sub btnclose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnclose.Click
        CloseForm()
    End Sub

    Private Sub RdbSavelayout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RdbSavelayout.Click
        If clsCommon.myLen(MyBase.Form_ID) > 0 Then
            gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = MyBase.Form_ID & "gv1"
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv1.SaveLayout(obj.GridLayout)
            obj.GridColumns = gv1.ColumnCount
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If obj.SaveData() Then
                gv1.MasterTemplate.FilterDescriptors.Clear()

                common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
            End If
            ''stuti regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub RdDeleteLayout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RdDeleteLayout.Click
        clsGridLayout.DeleteData(MyBase.Form_ID & "gv1", objCommonVar.CurrentUserCode)
        ReStoreGridLayout()
        common.clsCommon.MyMessageBoxShow("Layout Delete successfully", "Information")
    End Sub
    Sub loadBlankItemGrid()

        gv1.Rows.Clear()
        gv1.Columns.Clear()
        gv1.DataSource = Nothing

        Dim lineNo As New GridViewCheckBoxColumn()
        lineNo.HeaderText = "SL. No."
        lineNo.Name = colSlNo
        lineNo.Width = 60
        lineNo.ReadOnly = False
        lineNo.WrapText = True
        lineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(lineNo)


        Dim DispatchNo As New GridViewTextBoxColumn()
        DispatchNo.FormatString = ""
        DispatchNo.HeaderText = "Dispatch No"
        DispatchNo.Name = colDispatchNo
        DispatchNo.Width = 100
        DispatchNo.ReadOnly = True
        DispatchNo.WrapText = True
        DispatchNo.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(DispatchNo)

        Dim DispatchDate As New GridViewTextBoxColumn()
        DispatchDate.FormatString = ""
        DispatchDate.HeaderText = "Dispatch Date"
        DispatchDate.Name = colDispatchDate
        DispatchDate.Width = 100
        DispatchDate.ReadOnly = True
        DispatchDate.WrapText = True
        DispatchDate.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(DispatchDate)

        Dim Location As New GridViewTextBoxColumn()
        Location.FormatString = ""
        Location.HeaderText = "Location"
        Location.Name = colLocation
        Location.Width = 100
        Location.ReadOnly = True
        Location.WrapText = True
        Location.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(Location)

        Dim LocationName As New GridViewTextBoxColumn()
        LocationName.FormatString = ""
        LocationName.HeaderText = "Location Name"
        LocationName.Name = colLocationName
        LocationName.Width = 100
        LocationName.ReadOnly = True
        LocationName.WrapText = True
        LocationName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(LocationName)

        Dim Customer As New GridViewTextBoxColumn()
        Customer.FormatString = ""
        Customer.HeaderText = "Customer"
        Customer.Name = colCustomer
        Customer.Width = 100
        Customer.ReadOnly = True
        Customer.WrapText = True
        Customer.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(Customer)

        Dim CustomerName As New GridViewTextBoxColumn()
        CustomerName.FormatString = ""
        CustomerName.HeaderText = "Customer Name"
        CustomerName.Name = colCustomerName
        CustomerName.Width = 100
        CustomerName.ReadOnly = True
        CustomerName.WrapText = True
        CustomerName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(CustomerName)


        Dim TankerCode As New GridViewTextBoxColumn()
        TankerCode.FormatString = ""
        TankerCode.HeaderText = "Tanker Code"
        TankerCode.Name = colTankerNo
        TankerCode.Width = 100
        TankerCode.ReadOnly = True
        TankerCode.WrapText = True
        TankerCode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(TankerCode)

        Dim itemCode As New GridViewTextBoxColumn()
        itemCode.FormatString = ""
        itemCode.HeaderText = "Item Code"
        itemCode.Name = colItemCode
        itemCode.Width = 100
        itemCode.ReadOnly = True
        itemCode.WrapText = True
        itemCode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(itemCode)

        Dim itemDesc As New GridViewTextBoxColumn()
        itemDesc.FormatString = ""
        itemDesc.HeaderText = "Item Desc"
        itemDesc.Name = colItemDesc
        itemDesc.Width = 250
        itemDesc.ReadOnly = True
        itemDesc.WrapText = True
        itemDesc.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(itemDesc)

        Dim strUnitCode As New GridViewTextBoxColumn()
        strUnitCode.FormatString = ""
        strUnitCode.HeaderText = "UOM"
        strUnitCode.Name = colUnitCode
        strUnitCode.Width = 70
        strUnitCode.ReadOnly = True
        strUnitCode.WrapText = True
        strUnitCode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(strUnitCode)

        Dim dispatchQty As New GridViewDecimalColumn
        dispatchQty.FormatString = ""
        'dispatchQty.HeaderText = "Dispatch Qty"
        dispatchQty.HeaderText = "Qty"
        dispatchQty.Name = colDispatchQty
        dispatchQty.Width = 100
        dispatchQty.ReadOnly = True
        dispatchQty.WrapText = True
        dispatchQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(dispatchQty)


        Dim dispatchfatper As New GridViewDecimalColumn
        dispatchfatper.FormatString = ""
        'dispatchfatper.HeaderText = "Dispatch FAT %"
        dispatchfatper.HeaderText = "FAT %"
        dispatchfatper.Name = colDispatchFatPer
        dispatchfatper.Width = 75
        dispatchfatper.ReadOnly = True
        dispatchfatper.WrapText = True
        dispatchfatper.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(dispatchfatper)



        Dim dispatchsnfper As New GridViewDecimalColumn
        dispatchsnfper.FormatString = ""
        'dispatchsnfper.HeaderText = "Dispatch SNF %"
        dispatchsnfper.HeaderText = "SNF %"
        dispatchsnfper.Name = colDispatchSnfPer
        dispatchsnfper.Width = 75
        dispatchsnfper.ReadOnly = True
        dispatchsnfper.WrapText = True
        dispatchsnfper.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(dispatchsnfper)

        Dim DispatchFatRate As New GridViewDecimalColumn
        DispatchFatRate.FormatString = ""
        'DispatchFatRate.HeaderText = "Dispatch Rate"
        DispatchFatRate.HeaderText = "Rate"
        DispatchFatRate.Name = colDispatchRate
        DispatchFatRate.Width = 75
        DispatchFatRate.ReadOnly = True
        DispatchFatRate.WrapText = True
        DispatchFatRate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(DispatchFatRate)

        Dim DispatchAmount As New GridViewDecimalColumn
        DispatchAmount.FormatString = ""
        DispatchAmount.HeaderText = "Amount"
        DispatchAmount.Name = colDispatchAmount
        DispatchAmount.Width = 75
        DispatchAmount.ReadOnly = True
        DispatchAmount.WrapText = True
        DispatchAmount.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(DispatchAmount)

        Dim InvoiceQty As New GridViewDecimalColumn
        InvoiceQty.FormatString = ""
        InvoiceQty.HeaderText = "Invoice Qty"
        InvoiceQty.Name = ColInvoiceQty
        InvoiceQty.Width = 75
        InvoiceQty.ReadOnly = True
        InvoiceQty.WrapText = True
        InvoiceQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(InvoiceQty)


        Dim InvoiceFatPer As New GridViewDecimalColumn
        InvoiceFatPer.FormatString = ""
        InvoiceFatPer.HeaderText = "Invoice Fat %"
        InvoiceFatPer.Name = ColInvoiceFatPer
        InvoiceFatPer.Width = 75
        InvoiceFatPer.FormatString = "{0:n2}"
        InvoiceFatPer.ReadOnly = True
        InvoiceFatPer.WrapText = True
        InvoiceFatPer.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(InvoiceFatPer)

        Dim InvoiceSnfPer As New GridViewDecimalColumn
        InvoiceSnfPer.FormatString = ""
        InvoiceSnfPer.HeaderText = "Invoice SNF %"
        InvoiceSnfPer.Name = colInvoiceSnfPer
        InvoiceSnfPer.Width = 75
        InvoiceSnfPer.FormatString = "{0:n2}"
        InvoiceSnfPer.ReadOnly = True
        InvoiceSnfPer.WrapText = True
        InvoiceSnfPer.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(InvoiceSnfPer)

        Dim InvoiceRate As New GridViewDecimalColumn
        InvoiceRate.FormatString = ""
        InvoiceRate.HeaderText = "Invoice Rate"
        InvoiceRate.Name = colInvoiceRate
        InvoiceRate.Width = 75
        InvoiceRate.ReadOnly = True
        InvoiceRate.WrapText = True
        InvoiceRate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(InvoiceRate)

        Dim InvoiceAmount As New GridViewDecimalColumn
        InvoiceAmount.FormatString = ""
        InvoiceAmount.HeaderText = "Invoice Amount"
        InvoiceAmount.Name = colInvoiceAmount
        InvoiceAmount.Width = 75
        InvoiceAmount.ReadOnly = True
        InvoiceAmount.WrapText = True
        InvoiceAmount.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(InvoiceAmount)

        'Dim FatWeightage As New GridViewDecimalColumn
        'FatWeightage.FormatString = ""
        'FatWeightage.HeaderText = "Fat Weightage"
        'FatWeightage.Name = colFatWeightage
        'FatWeightage.Width = 75
        'FatWeightage.ReadOnly = True
        'FatWeightage.WrapText = True
        'FatWeightage.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        'gv1.Columns.Add(FatWeightage)

        'Dim SNFWeightage As New GridViewDecimalColumn
        'SNFWeightage.FormatString = ""
        'SNFWeightage.HeaderText = "SNF Weightage"
        'SNFWeightage.Name = colSNFWeightage
        'SNFWeightage.Width = 75
        'SNFWeightage.ReadOnly = True
        'SNFWeightage.WrapText = True
        'SNFWeightage.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        'gv1.Columns.Add(SNFWeightage)

        'gv1.Rows.AddNew()
        'gv1.Rows(0).Cells(colSlNo).Value = "1"

        ''richa 12/01/2014

        Dim InvoiceFatKg As New GridViewDecimalColumn
        InvoiceFatKg.FormatString = ""
        InvoiceFatKg.DecimalPlaces = 3
        InvoiceFatKg.HeaderText = "Invoice FAT KG"
        InvoiceFatKg.Name = colFatKG
        InvoiceFatKg.Width = 75
        InvoiceFatKg.ReadOnly = True
        InvoiceFatKg.WrapText = True
        InvoiceFatKg.IsVisible = False
        InvoiceFatKg.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(InvoiceFatKg)


        Dim InvoiceSNFKg As New GridViewDecimalColumn
        InvoiceSNFKg.FormatString = ""
        InvoiceSNFKg.DecimalPlaces = 3
        InvoiceSNFKg.HeaderText = "Invoice SNF KG"
        InvoiceSNFKg.Name = colSNFKG
        InvoiceSNFKg.Width = 75
        InvoiceSNFKg.ReadOnly = True
        InvoiceSNFKg.WrapText = True
        InvoiceSNFKg.IsVisible = False
        InvoiceSNFKg.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(InvoiceSNFKg)

        ''----------------

        gv1.AllowAddNewRow = False
        gv1.AllowDeleteRow = False
        gv1.AllowRowReorder = False
        gv1.ShowGroupPanel = False
        gv1.EnableFiltering = False
        gv1.EnableSorting = False
        gv1.EnableGrouping = False
        gv1.AllowColumnReorder = True
        gv1.ShowGroupPanel = False
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        gv1.TableElement.TableHeaderHeight = 40

    End Sub
    Private Sub ReStoreGridLayout()
        Dim obj As clsGridLayout = New clsGridLayout()
        Try
            If clsCommon.myLen(MyBase.Form_ID) > 0 Then
                obj = CType(obj.GetData(Form_ID & "gv1", "", objCommonVar.CurrentUserCode), clsGridLayout)
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
        Finally
            obj = Nothing
        End Try
    End Sub

   
    

    Public Sub LoadDispatchDetail()
        Dim InvoiceAgainst As String = String.Empty
        InvoiceAgainst = "Against Dispatch"

        Dim qry As String = " Select t1.* from (select CAST(1 as bit) as Sel,code,ICode,max(IName) as IName,max(Location) as Location,MAX(TSPL_LOCATION_MASTER.Location_Desc) as LocationName," & _
                            " SUM(Qty* case when RI=1 then 1 else 0 end) as DispatchQty," & _
                            " SUM(Qty* case when RI=-1 then 1 else 0 end) as InvoiceQty," & _
                            " SUM(Unapproved) as UnapprovedQty,SUM((Qty *RI)- Unapproved) as PendingQty ,MAX(Rate) as Rate,max(Document_Date) as Document_Date,max(final.Customer ) as Customer,MAX(TSPL_CUSTOMER_MASTER .Customer_Name ) as CustomerName,MAX(FatPer) As FatPer,MAX(SNFPer) As SNFPer,MAX(Amount) as Amount,Max(Unit_Code) as UnitCode,MAX(Fat_Weightage) as Fat_Weightage,Max(Snf_Weightage) as Snf_Weightage,Max(Fat_KG) as Invoice_Fat_KG,Max(SNF_KG) as Invoice_SNF_KG from ( " + Environment.NewLine & _
                            " Select TSPL_Dispatch_Detail_BulkSale.Document_No as Code,TSPL_Dispatch_BulkSale.Customer_Code as Customer ,TSPL_Dispatch_Detail_BulkSale.Item_Code as ICode,TSPL_ITEM_MASTER.Item_Desc as IName,TSPL_Dispatch_Detail_BulkSale.Qty as Qty,0 as Unapproved,TSPL_Dispatch_BulkSale.Location_Code As Location,1 as RI,TSPL_Dispatch_Detail_BulkSale.FatPer ,TSPL_Dispatch_Detail_BulkSale.SNFPer,TSPL_Dispatch_Detail_BulkSale.NetMilkRate as Rate ,TSPL_Dispatch_Detail_BulkSale.Amount,TSPL_Dispatch_BulkSale.Document_Date ,TSPL_Dispatch_BulkSale.Tanker_Code,TSPL_Dispatch_Detail_BulkSale.Unit_Code,TSPL_BulkSalePrice_MASTER.Fat_Weightage ,TSPL_BulkSalePrice_MASTER.Snf_Weightage,TSPL_Dispatch_Detail_BulkSale.Fat_KG ,TSPL_Dispatch_Detail_BulkSale.SNF_KG   from TSPL_Dispatch_Detail_BulkSale Left Outer Join TSPL_Dispatch_BulkSale on  TSPL_Dispatch_BulkSale.Document_No=TSPL_Dispatch_Detail_BulkSale.Document_No Left Outer Join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_Dispatch_Detail_BulkSale.Item_Code Left outer Join TSPL_BulkSalePrice_MASTER on TSPL_BulkSalePrice_MASTER.Price_Code =TSPL_Dispatch_BulkSale.Price_Code where TSPL_Dispatch_BulkSale.Posted=1"
        If cbgCustomer.CheckedValue.Count > 0 Then
            qry += " and (len(ISNULL(TSPL_Dispatch_BulkSale.Customer_Code,''))=0 or TSPL_Dispatch_BulkSale.Customer_Code in (" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ") )" + Environment.NewLine
        End If
        If cbgLocation.CheckedValue.Count > 0 Then
            qry += " and TSPL_Dispatch_BulkSale.Location_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") " + Environment.NewLine
        End If
        If clsCommon.myLen(fndPriceCode.Value) > 0 Then
            qry += " and TSPL_Dispatch_BulkSale.Price_Code ='" + clsCommon.myCstr(fndPriceCode.Value) + "' " + Environment.NewLine
        End If
        qry += " Union All " + Environment.NewLine & _
                " Select TSPL_INVOICE_DETAIL_BULKSALE.Dispatch_Code as Code,TSPL_INVOICE_MASTER_BULKSALE .Customer_Code as Customer,TSPL_INVOICE_DETAIL_BULKSALE.Item_Code as ICode,TSPL_ITEM_MASTER.Item_Desc as IName,TSPL_INVOICE_DETAIL_BULKSALE.InvoiceQty as Qty,0 as Unapproved ,'' as Location,-1 as RI,0 as Fatper,0 as SNFPer,0 as Rate,0 as Amount,null as Document_Date,'' as Tanker_Code,TSPL_INVOICE_DETAIL_BULKSALE.Unit_Code,0 as Fat_Weightage ,0 as Snf_Weightage,0 as Fat_KG ,0 as SNF_KG from TSPL_INVOICE_DETAIL_BULKSALE left Outer Join TSPL_INVOICE_MASTER_BULKSALE on TSPL_INVOICE_MASTER_BULKSALE.Document_No=TSPL_INVOICE_DETAIL_BULKSALE.Document_No Left Outer Join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_INVOICE_DETAIL_BULKSALE.Item_Code  where TSPL_INVOICE_MASTER_BULKSALE.Posted=1 and len(isnull(TSPL_INVOICE_DETAIL_BULKSALE.Dispatch_Code,''))>0 and TSPL_INVOICE_MASTER_BULKSALE.InvoiceAgainst='" + InvoiceAgainst + "'"

        If cbgCustomer.CheckedValue.Count > 0 Then
            qry += " and TSPL_INVOICE_MASTER_BULKSALE.Customer_Code in (" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ") " + Environment.NewLine
        End If
        If cbgLocation.CheckedValue.Count > 0 Then
            qry += " and TSPL_INVOICE_MASTER_BULKSALE.Location_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") " + Environment.NewLine
        End If

        qry += " Union All " + Environment.NewLine & _
                " Select TSPL_INVOICE_DETAIL_BULKSALE.Dispatch_Code as Code,TSPL_INVOICE_MASTER_BULKSALE .Customer_Code as Customer,TSPL_INVOICE_DETAIL_BULKSALE.Item_Code as ICode,TSPL_ITEM_MASTER.Item_Desc as IName,0 as Qty,TSPL_INVOICE_DETAIL_BULKSALE.InvoiceQty as Unapproved ,'' as Location,-1 as RI,0 as Fatper,0 as SNFPer,0 as Rate,0 as Amount,null as Document_Date,'' as Tanker_Code,TSPL_INVOICE_DETAIL_BULKSALE.Unit_Code,0 as Fat_Weightage ,0 as Snf_Weightage,0 as Fat_KG ,0 as SNF_KG from TSPL_INVOICE_DETAIL_BULKSALE left Outer Join TSPL_INVOICE_MASTER_BULKSALE on TSPL_INVOICE_MASTER_BULKSALE.Document_No=TSPL_INVOICE_DETAIL_BULKSALE.Document_No Left Outer Join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_INVOICE_DETAIL_BULKSALE.Item_Code  where TSPL_INVOICE_MASTER_BULKSALE.Posted=0 and len(isnull(TSPL_INVOICE_DETAIL_BULKSALE.Dispatch_Code,''))>0 and TSPL_INVOICE_MASTER_BULKSALE.InvoiceAgainst='" + InvoiceAgainst + "' "
        If (cbgCustomer.CheckedValue.Count) > 0 Then
            qry += " and TSPL_INVOICE_MASTER_BULKSALE.Customer_Code in (" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ") " + Environment.NewLine
        End If
        If (cbgLocation.CheckedValue.Count) > 0 Then
            qry += " and TSPL_INVOICE_MASTER_BULKSALE.Location_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") " + Environment.NewLine
        End If
        qry += " ) Final left outer join TSPL_CUSTOMER_MASTER  on TSPL_CUSTOMER_MASTER.Cust_Code =final.Customer  left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=Final.Location group by Final.Customer ,Code,ICode having (SUM(Qty *RI)-SUM(Unapproved)) >0 ) as t1 " & _
         " where CONVERT(DATE,t1.Document_Date,103) >= '" & clsCommon.GetPrintDate(dtpFromdate.Value, "dd/MMM/yyyy") & "' AND CONVERT(DATE,t1.Document_Date,103) <= '" & clsCommon.GetPrintDate(dtptodate.Value, "dd/MMM/yyyy") & "' order by t1.Customer,t1.Location,Code,ICode "
        '"  order by MAX(Final.Customer),Max(Final.Location),Code,ICode "
        dtAllDataDetail = clsDBFuncationality.GetDataTable(qry)
        If dtAllDataDetail Is Nothing OrElse dtAllDataDetail.Rows.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow("No data found ")
        Else
            LoadDetailData()
        End If
        InvoiceAgainst = Nothing
        qry = Nothing
    End Sub
    
    Sub LoadDetailData()
        loadBlankItemGrid()
        For Each dr As DataRow In dtAllDataDetail.Rows

            gv1.Rows.AddNew()

            gv1.Rows(gv1.Rows.Count - 1).Cells(colSlNo).Value = True
            gv1.Rows(gv1.Rows.Count - 1).Cells(colDispatchNo).Value = clsCommon.myCstr(dr("code"))
            gv1.Rows(gv1.Rows.Count - 1).Cells(colDispatchDate).Value = clsCommon.myCstr(clsCommon.GetPrintDate(dr("Document_Date"), "dd/MM/yyyy"))
            gv1.Rows(gv1.Rows.Count - 1).Cells(colLocation).Value = clsCommon.myCstr(dr("Location"))
            gv1.Rows(gv1.Rows.Count - 1).Cells(colLocationName).Value = clsCommon.myCstr(dr("LocationName"))
            gv1.Rows(gv1.Rows.Count - 1).Cells(colCustomer).Value = clsCommon.myCstr(dr("Customer"))
            gv1.Rows(gv1.Rows.Count - 1).Cells(colCustomerName).Value = clsCommon.myCstr(dr("CustomerName"))
            gv1.Rows(gv1.Rows.Count - 1).Cells(colItemCode).Value = clsCommon.myCstr(dr("ICode"))
            gv1.Rows(gv1.Rows.Count - 1).Cells(colItemDesc).Value = clsCommon.myCstr(dr("IName"))
            gv1.Rows(gv1.Rows.Count - 1).Cells(colTankerNo).Value = clsDBFuncationality.getSingleValue("Select Tanker_Code  from TSPL_Dispatch_BulkSale where Document_No ='" + clsCommon.myCstr(dr("code")) + "'")
            gv1.Rows(gv1.Rows.Count - 1).Cells(colUnitCode).Value = clsCommon.myCstr(dr("UnitCode"))
            gv1.Rows(gv1.Rows.Count - 1).Cells(colDispatchQty).Value = clsCommon.myCdbl(dr("PendingQty"))
            gv1.Rows(gv1.Rows.Count - 1).Cells(colDispatchFatPer).Value = clsCommon.myCdbl(dr("FatPer"))
            gv1.Rows(gv1.Rows.Count - 1).Cells(colDispatchSnfPer).Value = clsCommon.myCdbl(dr("SNFPer"))
            gv1.Rows(gv1.Rows.Count - 1).Cells(colDispatchRate).Value = clsCommon.myCdbl(dr("Rate"))
            ' gv1.Rows(gv1.Rows.Count - 1).Cells(colDispatchAmount).Value = clsCommon.myCdbl(dr("Rate")) * clsCommon.myCdbl(dr("PendingQty"))
            gv1.Rows(gv1.Rows.Count - 1).Cells(colDispatchAmount).Value = clsCommon.myCdbl(dr("Amount"))
            gv1.Rows(gv1.Rows.Count - 1).Cells(ColInvoiceQty).Value = clsCommon.myCdbl(dr("PendingQty"))
            gv1.Rows(gv1.Rows.Count - 1).Cells(ColInvoiceFatPer).Value = clsCommon.myCdbl(dr("FatPer"))
            gv1.Rows(gv1.Rows.Count - 1).Cells(colInvoiceSnfPer).Value = clsCommon.myCdbl(dr("SNFPer"))
            gv1.Rows(gv1.Rows.Count - 1).Cells(colInvoiceRate).Value = clsCommon.myCdbl(dr("Rate"))
            'gv1.Rows(gv1.Rows.Count - 1).Cells(colInvoiceAmount).Value = clsCommon.myCdbl(dr("Rate")) * clsCommon.myCdbl(dr("PendingQty"))
            gv1.Rows(gv1.Rows.Count - 1).Cells(colInvoiceAmount).Value = clsCommon.myCdbl(dr("Amount"))
            ''richa agarwal 12/01/2014
            gv1.Rows(gv1.Rows.Count - 1).Cells(colFatKG).Value = clsCommon.myCdbl(dr("Invoice_Fat_KG"))
            gv1.Rows(gv1.Rows.Count - 1).Cells(colSNFKG).Value = clsCommon.myCdbl(dr("Invoice_SNF_KG"))
        Next

    End Sub

    Private Sub btnGo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGo.Click
        Try
            If clsCommon.myLen(fndPriceCode.Value) <= 0 Then
                Throw New Exception("Please select price chart")
            End If
            If clsCommon.myCDate(dtpFromdate.Value) > clsCommon.myCDate(dtptodate.Value) Then
                Throw New Exception("From date cannot be greater than To date")
            End If

            LoadDispatchDetail()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub fndPriceCode__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndPriceCode._MYValidating
        'Dim whrcls As String = "Convert(date,TSPL_BulkSalePrice_MASTER.Price_Date ,103)<=CONVERT(date,'" & txtDate.Value & "',103) AND (ISNULL(Convert(date,TSPL_BulkSalePrice_MASTER.ValidTill ,103),'')='' OR Convert(date,TSPL_BulkSalePrice_MASTER.ValidTill,103)>=CONVERT(date,'" & txtDate.Value & "',103))"
        Dim locationcode As String = "  1=1 "
        If (cbgLocation.CheckedValue.Count) > 0 Then
            locationcode += " and TSPL_BulkSalePrice_MASTER.Location_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")"
        End If
        ''richa agarwal 12 Sep, 2016
        If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description from TSPL_FIXED_PARAMETER where code= '" & clsFixedParameterCode.showPostrequiredforBulkSale & "' and Type ='" & clsFixedParameterType.showPostrequiredforBulkSale & "'")), "1") = CompairStringResult.Equal Then
            locationcode += " and TSPL_BulkSalePrice_MASTER.Posted='1' "
        End If

        ' whrcls = locationcode + whrcls
        fndPriceCode.Value = ClsBulkSalePriceChart.getFinder(locationcode, fndPriceCode.Value, isButtonClicked)
    End Sub

  
    Private Function AllowToSave() As Boolean
        Dim count As Integer = 0
        If clsCommon.myLen(fndPriceCode.Value) <= 0 Then
            Throw New Exception("Please select price chart")
        End If

        If clsCommon.myCDate(dtpFromdate.Value) > clsCommon.myCDate(dtptodate.Value) Then
            Throw New Exception("From date cannot be greater than To date")
        End If

        If clsCommon.GetPrintDate(dtptodate.Value, "dd/MM/yyyy") > clsCommon.myCDate(TxtInvoiceDate.Value) Then
            Throw New Exception("Invoice date cannot be less than To date")
        End If

        If gv1.Rows.Count <= 0 Then
            Throw New Exception("There is no dispatch to create invoice")
        End If
        For i As Integer = 0 To gv1.Rows.Count - 1
           
            If clsCommon.myLen(gv1.Rows(i).Cells(colDispatchNo).Value) >= 0 Then
                count = count + 1
            End If
            If count = 0 Then
                Throw New Exception("There is no dispatch to create invoice")
            End If
        Next
       
        Return True
    End Function

    Private Sub btnsave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsave.Click
        Dim LocationCode As String = String.Empty
        Dim CustomerCode As String = String.Empty
        Try
            If AllowToSave() Then
                Dim InvoiceAmount As Double = 0

                Dim CustomerCount As Integer = 0
                Dim count As Integer = 1
                'Dim dt As DataTable = dtAllDataDetail.Clone()
                'dt.Columns.Add("InvoiceSrNo", GetType(String))
                dtmain = clsDBFuncationality.GetDataTable("Select '' as InvoiceSrNo,'' as Customer,'' as Location,'' as DispatchCode,'' as DispatchDate,'' as ItemCode,'' as UnitCode,'' as TankerCode,'' as DispatchQty,'' as DispatchFatPer,'' as DispatchSNFPer,'' as DispatchRate,'' as DispatchAmount,'' as InvoiceQty,'' as InvoiceFatPer,'' as InvoiceSNFPer,'' as InvoiceRate,'' as InvoiceAmount,'' as InvoiceFatKg,'' as InvoiceSNFKg")
                dtmain.Rows.RemoveAt(0)
                For Each grow As GridViewRowInfo In gv1.Rows
                    If clsCommon.myCBool(grow.Cells(colSlNo).Value) Then
                        If clsCommon.CompairString(CustomerCode, clsCommon.myCstr(grow.Cells(colCustomer).Value)) = CompairStringResult.Equal And clsCommon.CompairString(LocationCode, clsCommon.myCstr(grow.Cells(colLocation).Value)) = CompairStringResult.Equal Then
                            InvoiceAmount = InvoiceAmount + clsCommon.myCdbl(grow.Cells(colInvoiceAmount).Value)
                        Else
                            CustomerCount = CustomerCount + 1
                            InvoiceAmount = 0
                            InvoiceAmount = clsCommon.myCdbl(grow.Cells(colInvoiceAmount).Value)
                        End If
                        CustomerCode = clsCommon.myCstr(grow.Cells(colCustomer).Value)
                        LocationCode = clsCommon.myCstr(grow.Cells(colLocation).Value)
                        Dim AmountLimitInvoiceBulkSale As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select description from tspl_fixed_parameter where type='" + clsFixedParameterType.AmountLimitForInvoiceBulkSale + "' and code='" + clsFixedParameterCode.AmountLimitForInvoiceBulkSale + "'"))
                        If AmountLimitInvoiceBulkSale > 0 Then
                            If InvoiceAmount > AmountLimitInvoiceBulkSale Then
                                If clsCommon.myCdbl(grow.Cells(colInvoiceAmount).Value) <= AmountLimitInvoiceBulkSale Then
                                    'count = count + 1
                                    CustomerCount = CustomerCount + 1
                                    dtmain.Rows.Add("" + clsCommon.myCstr(CustomerCount) + "", "" + clsCommon.myCstr(grow.Cells(colCustomer).Value) + "", "" + clsCommon.myCstr(grow.Cells(colLocation).Value) + "", "" + clsCommon.myCstr(grow.Cells(colDispatchNo).Value) + "", "" + clsCommon.myCstr(grow.Cells(colDispatchDate).Value) + "", "" + clsCommon.myCstr(grow.Cells(colItemCode).Value) + "", "" + clsCommon.myCstr(grow.Cells(colUnitCode).Value) + "", "" + clsCommon.myCstr(grow.Cells(colTankerNo).Value) + "", "" + clsCommon.myCstr(grow.Cells(colDispatchQty).Value) + "", " " + clsCommon.myCstr(grow.Cells(colDispatchFatPer).Value) + "", "" + clsCommon.myCstr(grow.Cells(colDispatchSnfPer).Value) + "", "" + clsCommon.myCstr(grow.Cells(colDispatchRate).Value) + "", "" + clsCommon.myCstr(grow.Cells(colDispatchAmount).Value) + "", "" + clsCommon.myCstr(grow.Cells(ColInvoiceQty).Value) + "", "" + clsCommon.myCstr(grow.Cells(ColInvoiceFatPer).Value) + "", "" + clsCommon.myCstr(grow.Cells(colInvoiceSnfPer).Value) + "", "" + clsCommon.myCstr(grow.Cells(colInvoiceRate).Value) + "", "" + clsCommon.myCstr(grow.Cells(colInvoiceAmount).Value) + "", "" + clsCommon.myCstr(grow.Cells(colFatKG).Value) + "", "" + clsCommon.myCstr(grow.Cells(colSNFKG).Value) + "")
                                    InvoiceAmount = clsCommon.myCdbl(grow.Cells(colInvoiceAmount).Value)
                                End If
                            Else
                                dtmain.Rows.Add("" + clsCommon.myCstr(CustomerCount) + "", "" + clsCommon.myCstr(grow.Cells(colCustomer).Value) + "", "" + clsCommon.myCstr(grow.Cells(colLocation).Value) + "", "" + clsCommon.myCstr(grow.Cells(colDispatchNo).Value) + "", "" + clsCommon.myCstr(grow.Cells(colDispatchDate).Value) + "", "" + clsCommon.myCstr(grow.Cells(colItemCode).Value) + "", "" + clsCommon.myCstr(grow.Cells(colUnitCode).Value) + "", "" + clsCommon.myCstr(grow.Cells(colTankerNo).Value) + "", "" + clsCommon.myCstr(grow.Cells(colDispatchQty).Value) + "", " " + clsCommon.myCstr(grow.Cells(colDispatchFatPer).Value) + "", "" + clsCommon.myCstr(grow.Cells(colDispatchSnfPer).Value) + "", "" + clsCommon.myCstr(grow.Cells(colDispatchRate).Value) + "", "" + clsCommon.myCstr(grow.Cells(colDispatchAmount).Value) + "", "" + clsCommon.myCstr(grow.Cells(ColInvoiceQty).Value) + "", "" + clsCommon.myCstr(grow.Cells(ColInvoiceFatPer).Value) + "", "" + clsCommon.myCstr(grow.Cells(colInvoiceSnfPer).Value) + "", "" + clsCommon.myCstr(grow.Cells(colInvoiceRate).Value) + "", "" + clsCommon.myCstr(grow.Cells(colInvoiceAmount).Value) + "", "" + clsCommon.myCstr(grow.Cells(colFatKG).Value) + "", "" + clsCommon.myCstr(grow.Cells(colSNFKG).Value) + "")
                            End If
                        Else
                            dtmain.Rows.Add("" + clsCommon.myCstr(CustomerCount) + "", "" + clsCommon.myCstr(grow.Cells(colCustomer).Value) + "", "" + clsCommon.myCstr(grow.Cells(colLocation).Value) + "", "" + clsCommon.myCstr(grow.Cells(colDispatchNo).Value) + "", "" + clsCommon.myCstr(grow.Cells(colDispatchDate).Value) + "", "" + clsCommon.myCstr(grow.Cells(colItemCode).Value) + "", "" + clsCommon.myCstr(grow.Cells(colUnitCode).Value) + "", "" + clsCommon.myCstr(grow.Cells(colTankerNo).Value) + "", "" + clsCommon.myCstr(grow.Cells(colDispatchQty).Value) + "", " " + clsCommon.myCstr(grow.Cells(colDispatchFatPer).Value) + "", "" + clsCommon.myCstr(grow.Cells(colDispatchSnfPer).Value) + "", "" + clsCommon.myCstr(grow.Cells(colDispatchRate).Value) + "", "" + clsCommon.myCstr(grow.Cells(colDispatchAmount).Value) + "", "" + clsCommon.myCstr(grow.Cells(ColInvoiceQty).Value) + "", "" + clsCommon.myCstr(grow.Cells(ColInvoiceFatPer).Value) + "", "" + clsCommon.myCstr(grow.Cells(colInvoiceSnfPer).Value) + "", "" + clsCommon.myCstr(grow.Cells(colInvoiceRate).Value) + "", "" + clsCommon.myCstr(grow.Cells(colInvoiceAmount).Value) + "", "" + clsCommon.myCstr(grow.Cells(colFatKG).Value) + "", "" + clsCommon.myCstr(grow.Cells(colSNFKG).Value) + "")
                        End If
                    End If
                Next
                If dtmain Is Nothing OrElse dtmain.Rows.Count <= 0 Then
                    Throw New Exception("Please select atleast 1 dispatch")
                End If

                SaveData()
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        Finally
            LocationCode = Nothing
            CustomerCode = Nothing
        End Try
    End Sub
    Sub SaveData()

        'If dtmain Is Nothing OrElse dtmain.Rows.Count <= 0 Then
        '    Exit Sub
        'End If
        Dim strcountno As String = ""
        Dim trans As SqlTransaction = Nothing
        Dim objTr As ClsInvoiceDetailBulkSale = Nothing
        Dim obj As ClsInvoiceBulkSale = Nothing
        Try
            trans = clsDBFuncationality.GetTransactin()

            
            Dim DocuAmount As Double = 0
            Dim intCounter As Integer = 0

            For Each dr As DataRow In dtmain.Rows
                Dim intCurrInvNo As Integer = clsCommon.myCdbl(dr("InvoiceSrNo"))

                If clsCommon.CompairString(strcountno, clsCommon.myCstr(dr("InvoiceSrNo"))) <> CompairStringResult.Equal Then
                    obj = New ClsInvoiceBulkSale()
                    obj.Document_Date = TxtInvoiceDate.Value  ''clsCommon.GETSERVERDATE(trans)
                    obj.Customer_Code = clsCommon.myCstr(dr("Customer"))
                    obj.Location_Code = clsCommon.myCstr(dr("Location"))
                    obj.InvoiceAgainst = "Against Dispatch"
                    obj.fromdate = clsCommon.GETSERVERDATE(trans)
                    obj.todate = clsCommon.GETSERVERDATE(trans)

                    obj.arrInvoiceDetailBulkSale = New List(Of ClsInvoiceDetailBulkSale)
                    objTr = New ClsInvoiceDetailBulkSale()
                    objTr.Dispatch_Code = clsCommon.myCstr(dr("DispatchCode"))
                    objTr.Dispatch_Date = clsCommon.myCstr(dr("DispatchDate"))
                    objTr.Item_Code = clsCommon.myCstr(dr("ItemCode"))
                    objTr.Unit_code = clsCommon.myCstr(dr("UnitCode"))
                    objTr.Tanker_Code = clsCommon.myCstr(dr("TankerCode"))
                    objTr.DispatchQty = clsCommon.myCdbl(dr("DispatchQty"))
                    objTr.DispatchFatPer = clsCommon.myCdbl(dr("DispatchFatPer"))
                    objTr.DispatchSNFPer = clsCommon.myCdbl(dr("DispatchSNFPer"))
                    objTr.DispatchRate = clsCommon.myCdbl(dr("DispatchRate"))
                    objTr.DispatchAmount = clsCommon.myCdbl(dr("DispatchAmount"))
                    objTr.InvoiceQty = clsCommon.myCdbl(dr("InvoiceQty"))
                    objTr.InvoiceFatPer = clsCommon.myCdbl(dr("InvoiceFatPer"))
                    objTr.InvoiceSNFPer = clsCommon.myCdbl(dr("InvoiceSNFPer"))
                    objTr.InvoiceFatKG = clsCommon.myCdbl(dr("InvoiceFatKg"))
                    objTr.InvoiceSNFKG = clsCommon.myCdbl(dr("InvoiceSNFKg"))
                    objTr.InvoiceRate = clsCommon.myCdbl(dr("InvoiceRate"))
                    objTr.InvoiceAmount = clsCommon.myCdbl(dr("InvoiceAmount"))
                    DocuAmount = 0
                    DocuAmount = Math.Round(DocuAmount, 2) + Math.Round(objTr.InvoiceAmount, 2)
                    obj.arrInvoiceDetailBulkSale.Add(objTr)
                Else
                    objTr = New ClsInvoiceDetailBulkSale()
                    objTr.Dispatch_Code = clsCommon.myCstr(dr("DispatchCode"))
                    objTr.Dispatch_Date = clsCommon.myCstr(dr("DispatchDate"))
                    objTr.Item_Code = clsCommon.myCstr(dr("ItemCode"))
                    objTr.Unit_code = clsCommon.myCstr(dr("UnitCode"))
                    objTr.Tanker_Code = clsCommon.myCstr(dr("TankerCode"))
                    objTr.DispatchQty = clsCommon.myCdbl(dr("DispatchQty"))
                    objTr.DispatchFatPer = clsCommon.myCdbl(dr("DispatchFatPer"))
                    objTr.DispatchSNFPer = clsCommon.myCdbl(dr("DispatchSNFPer"))
                    objTr.DispatchRate = clsCommon.myCdbl(dr("DispatchRate"))
                    objTr.DispatchAmount = clsCommon.myCdbl(dr("DispatchAmount"))
                    objTr.InvoiceQty = clsCommon.myCdbl(dr("InvoiceQty"))
                    objTr.InvoiceFatPer = clsCommon.myCdbl(dr("InvoiceFatPer"))
                    objTr.InvoiceSNFPer = clsCommon.myCdbl(dr("InvoiceSNFPer"))
                    objTr.InvoiceRate = clsCommon.myCdbl(dr("InvoiceRate"))
                    objTr.InvoiceAmount = clsCommon.myCdbl(dr("InvoiceAmount"))

                    DocuAmount = Math.Round(DocuAmount, 2) + Math.Round(objTr.InvoiceAmount, 2)
                    obj.arrInvoiceDetailBulkSale.Add(objTr)
                End If
                strcountno = intCurrInvNo
                Dim intNextInvNo As Integer = -1

                If intCounter + 1 < dtmain.Rows.Count Then
                    intNextInvNo = clsCommon.myCdbl(dtmain.Rows(intCounter + 1)("InvoiceSrNo"))
                End If

                If Not (intCurrInvNo = intNextInvNo) Then
                    If Math.Round(clsCommon.myCdbl(DocuAmount), 0) > clsCommon.myCdbl(DocuAmount) Then
                        obj.RoundOffAmount = Math.Round(Math.Round(clsCommon.myCdbl(DocuAmount), 0) - clsCommon.myCdbl(DocuAmount), 2)
                        obj.Total_Amt = Math.Round(clsCommon.myCdbl(DocuAmount), 0)
                    Else
                        obj.RoundOffAmount = Math.Round(Math.Round(clsCommon.myCdbl(DocuAmount)) - clsCommon.myCdbl(DocuAmount), 2)
                        obj.Total_Amt = Math.Round(clsCommon.myCdbl(DocuAmount), 0)
                    End If
                    ClsInvoiceBulkSale.SaveData(obj, True, trans)
                    ClsInvoiceBulkSale.PostData("", "'" + obj.Location_Code + "'", obj.Document_No, trans)
                End If
                intCounter += 1
            Next
            trans.Commit()
            clsCommon.MyMessageBoxShow("Invoice created successfully")
            Reset()
        Catch ex As Exception
            trans.Rollback()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            strcountno = Nothing
            obj = Nothing
            objTr = Nothing
        End Try

    End Sub


    Private Sub chkCustomerAll_ToggleStateChanged(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkCustomerAll.ToggleStateChanged
        cbgCustomer.Enabled = Not chkCustomerAll.IsChecked
    End Sub

 
    Private Sub chkLocationAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocationAll.ToggleStateChanged
        cbgLocation.Enabled = Not chkLocationAll.IsChecked
    End Sub

    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Reset()
    End Sub

    Private Sub gv1_CellValueChanged(sender As Object, e As GridViewCellEventArgs) Handles gv1.CellValueChanged
        updatetotalqtyAndAmount()
    End Sub
    Sub updatetotalqtyAndAmount()
        'For i As Integer = 0 To gv1.Rows.Count - 1

        'Next
        Dim totalQty As Double = 0
        Dim totalAmt As Double = 0
        For Each grow As GridViewRowInfo In gv1.Rows
            If clsCommon.myCBool(grow.Cells(colSlNo).Value) Then
                totalQty = totalQty + clsCommon.myCdbl(grow.Cells(ColInvoiceQty).Value)
                totalAmt = totalAmt + clsCommon.myCdbl(grow.Cells(colInvoiceAmount).Value)
            End If
        Next

        txtTotalQty.Value = totalQty
        txtTotalAmt.Value = totalAmt
    End Sub
End Class
