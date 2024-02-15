'--------Created By Richa 07/08/2014 Against Ticket No BM00000003249,BM00000004068
Imports common
Public Class frmPendingDispatch

#Region "Variables"
    Public customerCode As String = Nothing
    Public customerName As String = Nothing
    Public Locationcode As String = Nothing
    Public LocationName As String = Nothing
    Public strCurrCode As String = Nothing
    Public fromdate As Date?
    Public todate As Date?
    Public InvoiceAgainst As String = Nothing
    Public ArrReturn As List(Of clsDispatchDetailBulkSale) = Nothing

    Dim dtAllData As DataTable = Nothing
    Dim IsInsideLoadData As Boolean = False

    Const colDSelect As String = "SELECT"
    Const colDCode As String = "CODE"
    Const colDICode As String = "ICODE"
    Const colDIName As String = "INAME"    
    Const colDRate As String = "RATE"
    Const colDOrderQty As String = "ORDERQTY"
    Const colDApprovedQty As String = "APPROVEDQTY"
    Const colDUnApprovedQty As String = "UNAPPROVEDQTY"
    Const colDDamageQty As String = "DAMAGEQTY"
    Const colDPendingQty As String = "PENDINGQTY"
    Const colDFatWeightage As String = "FatWeightage"
    Const colDSNFWeightage As String = "SNFWeightage"
    Const colDAmount As String = "AMOUNT"
    Const colDFatPer As String = "FATPER"
    Const colDSNFPer As String = "SNFPER"
    Const colDCLR As String = "CLR"
    Const colDFatKG As String = "FATKG"
    Const colDSNFKG As String = "SNFKG"
    Const colDUnitCode As String = "colDUnitCode"


    Const colHSelect As String = "SELECT"
    Const colHCode As String = "CODE"
    Const colHDate As String = "DATE"
    Const colHCustomerCode As String = "CUSTOMER"
    Const colHCustomerName As String = "CUSTOMERNAME"
#End Region

    Private Sub FrmPendingRequistion_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If clsCommon.myLen(customerCode) > 0 Then
            Me.Text = Me.Text + " For " + customerName
        End If
        If clsCommon.myLen(Locationcode) > 0 Then
            Me.Text = Me.Text + " For " + LocationName
        End If

        Dim qry As String = "select CAST(0 as bit) as Sel,code,ICode,max(IName) as IName,max(Location) as Location,MAX(TSPL_LOCATION_MASTER.Location_Desc) as LocationName," &
                            " SUM(Qty* case when RI=1 then 1 else 0 end) as DispatchQty," &
                            " SUM(Qty* case when RI=-1 then 1 else 0 end) as InvoiceQty," &
                            " SUM(Unapproved) as UnapprovedQty,SUM((Qty *RI)- Unapproved) as PendingQty ,MAX(Rate) as Rate,max(Document_Date) as Document_Date,max(final.Customer ) as Customer,MAX(TSPL_CUSTOMER_MASTER .Customer_Name ) as CustomerName,MAX(FAT) As FatPer,MAX(SNF) As SNFPer,max(CLR) as CLR,MAX(Amount) as Amount,Max(Unit_Code) as UnitCode,MAX(Fat_Weightage) as Fat_Weightage,Max(Snf_Weightage) as Snf_Weightage,Max(Fat_KG) as Invoice_Fat_KG,Max(SNF_KG) as Invoice_SNF_KG from ( " + Environment.NewLine &
                            " Select TSPL_Dispatch_Detail_BulkSale.Document_No as Code,TSPL_Dispatch_BulkSale.Customer_Code as Customer ,TSPL_Dispatch_Detail_BulkSale.Item_Code as ICode,TSPL_ITEM_MASTER.Item_Desc as IName,TSPL_BULK_SALE_ACKNOWLEDGEMENT.Qty as Qty,0 as Unapproved,TSPL_Dispatch_BulkSale.Location_Code As Location,1 as RI,TSPL_BULK_SALE_ACKNOWLEDGEMENT.FAT ,TSPL_BULK_SALE_ACKNOWLEDGEMENT.SNF,TSPL_Dispatch_Detail_BulkSale.CLR ,TSPL_Dispatch_Detail_BulkSale.NetMilkRate as Rate ,TSPL_BULK_SALE_ACKNOWLEDGEMENT.Amount,TSPL_Dispatch_BulkSale.Document_Date ,TSPL_Dispatch_BulkSale.Tanker_Code,TSPL_Dispatch_Detail_BulkSale.Unit_Code,TSPL_BulkSalePrice_MASTER.Fat_Weightage ,TSPL_BulkSalePrice_MASTER.Snf_Weightage,TSPL_BULK_SALE_ACKNOWLEDGEMENT.Fat_KG ,TSPL_BULK_SALE_ACKNOWLEDGEMENT.SNF_KG   from TSPL_BULK_SALE_ACKNOWLEDGEMENT Left Outer Join TSPL_Dispatch_BulkSale on  TSPL_Dispatch_BulkSale.Document_No=TSPL_BULK_SALE_ACKNOWLEDGEMENT.Bulk_Dispatch_Document left outer join TSPL_Dispatch_Detail_BulkSale on TSPL_Dispatch_Detail_BulkSale.Document_No = TSPL_Dispatch_BulkSale.Document_No
                            Left Outer Join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_Dispatch_Detail_BulkSale.Item_Code Left outer Join TSPL_BulkSalePrice_MASTER on TSPL_BulkSalePrice_MASTER.Price_Code =TSPL_Dispatch_BulkSale.Price_Code where TSPL_Dispatch_BulkSale.Posted=1" + Environment.NewLine &
                            " and convert(date,TSPL_Dispatch_BulkSale.Document_Date,103)>=convert(date,('" + fromdate + "'),103) and convert(date,TSPL_Dispatch_BulkSale.Document_Date,103) <=convert(date,('" + todate + "'),103) " &
                         " and TSPL_Dispatch_BulkSale.Document_No not in (Select isnull(DispatchNo ,'') from TSPL_SALE_RETURN_MASTER_BULKSALE where Against ='Bulk Dispatch') "
        If clsCommon.myLen(customerCode) > 0 Then
            qry += " and (len(ISNULL(TSPL_Dispatch_BulkSale.Customer_Code,''))=0 or TSPL_Dispatch_BulkSale.Customer_Code='" + customerCode + "')" + Environment.NewLine
        End If
        If clsCommon.myLen(Locationcode) > 0 Then
            qry += " and TSPL_Dispatch_BulkSale.Location_Code='" + Locationcode + "' " + Environment.NewLine
        End If
        qry += " Union All " + Environment.NewLine & _
                " Select TSPL_INVOICE_DETAIL_BULKSALE.Dispatch_Code as Code,TSPL_INVOICE_MASTER_BULKSALE .Customer_Code as Customer,TSPL_INVOICE_DETAIL_BULKSALE.Item_Code as ICode,TSPL_ITEM_MASTER.Item_Desc as IName,TSPL_INVOICE_DETAIL_BULKSALE.InvoiceQty as Qty,0 as Unapproved ,'' as Location,-1 as RI,0 as Fatper,0 as SNFPer,0 as CLR,0 as Rate,0 as Amount,null as Document_Date,'' as Tanker_Code,TSPL_INVOICE_DETAIL_BULKSALE.Unit_Code,0 as Fat_Weightage ,0 as Snf_Weightage,0 as Fat_KG ,0 as SNF_KG from TSPL_INVOICE_DETAIL_BULKSALE left Outer Join TSPL_INVOICE_MASTER_BULKSALE on TSPL_INVOICE_MASTER_BULKSALE.Document_No=TSPL_INVOICE_DETAIL_BULKSALE.Document_No Left Outer Join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_INVOICE_DETAIL_BULKSALE.Item_Code  where TSPL_INVOICE_MASTER_BULKSALE.Posted=1 and len(isnull(TSPL_INVOICE_DETAIL_BULKSALE.Dispatch_Code,''))>0 and TSPL_INVOICE_MASTER_BULKSALE.InvoiceAgainst='" + InvoiceAgainst + "'"

        ' ''richa changes on 03/09/2014
        'If clsCommon.myLen(customerCode) > 0 Then
        '    qry += " and TSPL_INVOICE_MASTER_BULKSALE.Customer_Code='" + customerCode + "' " + Environment.NewLine
        'End If
        'If clsCommon.myLen(Locationcode) > 0 Then
        '    qry += " and TSPL_INVOICE_MASTER_BULKSALE.Location_Code='" + Locationcode + "' " + Environment.NewLine
        'End If
        ' ''------------
        'qry += " Union All " + Environment.NewLine & _
        '        " Select TSPL_INVOICE_DETAIL_BULKSALE.Dispatch_Code as Code,TSPL_INVOICE_MASTER_BULKSALE .Customer_Code as Customer,TSPL_INVOICE_DETAIL_BULKSALE.Item_Code as ICode,TSPL_ITEM_MASTER.Item_Desc as IName,0 as Qty,TSPL_INVOICE_DETAIL_BULKSALE.InvoiceQty as Unapproved ,'' as Location,-1 as RI,0 as Fatper,0 as SNFPer,0 as Rate,0 as Amount,null as Document_Date,'' as Tanker_Code,TSPL_INVOICE_DETAIL_BULKSALE.Unit_Code,0 as Fat_Weightage ,0 as Snf_Weightage,0 as Fat_KG ,0 as SNF_KG from TSPL_INVOICE_DETAIL_BULKSALE left Outer Join TSPL_INVOICE_MASTER_BULKSALE on TSPL_INVOICE_MASTER_BULKSALE.Document_No=TSPL_INVOICE_DETAIL_BULKSALE.Document_No Left Outer Join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_INVOICE_DETAIL_BULKSALE.Item_Code  where TSPL_INVOICE_MASTER_BULKSALE.Posted=0 and len(isnull(TSPL_INVOICE_DETAIL_BULKSALE.Dispatch_Code,''))>0 and TSPL_INVOICE_MASTER_BULKSALE.InvoiceAgainst='" + InvoiceAgainst + "' and TSPL_INVOICE_DETAIL_BULKSALE.Dispatch_Code not in ('" + strCurrCode + "') "
        ' ''richa changes on 03/09/2014
        'If clsCommon.myLen(customerCode) > 0 Then
        '    qry += " and TSPL_INVOICE_MASTER_BULKSALE.Customer_Code='" + customerCode + "' " + Environment.NewLine
        'End If
        'If clsCommon.myLen(Locationcode) > 0 Then
        '    qry += " and TSPL_INVOICE_MASTER_BULKSALE.Location_Code='" + Locationcode + "' " + Environment.NewLine
        'End If
        ' ''------------
        qry += " ) Final left outer join TSPL_CUSTOMER_MASTER  on TSPL_CUSTOMER_MASTER.Cust_Code =final.Customer  left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=Final.Location " & _
        "  where not exists (Select TSPL_INVOICE_DETAIL_BULKSALE.Dispatch_Code from TSPL_INVOICE_DETAIL_BULKSALE inner join TSPL_INVOICE_MASTER_BULKSALE on TSPL_INVOICE_MASTER_BULKSALE.Document_No =TSPL_INVOICE_DETAIL_BULKSALE.Document_No and TSPL_INVOICE_DETAIL_BULKSALE.Dispatch_Code =Final.Code where TSPL_INVOICE_MASTER_BULKSALE.InvoiceAgainst='" + InvoiceAgainst + "' and TSPL_INVOICE_DETAIL_BULKSALE.Dispatch_Code not in ('" + strCurrCode + "') ) " & _
        " group by Code,ICode having (SUM(Qty *RI)-SUM(Unapproved)) >0 order by Code,ICode "
        ''        
        dtAllData = clsDBFuncationality.GetDataTable(qry)
        If dtAllData Is Nothing OrElse dtAllData.Rows.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow("No item found for customer " + customerName + " at particular location ")
            Me.Close()
        End If
        LoadHeadData()
        LoadBlankGridDetail()
    End Sub

    Sub LoadHeadData()
        IsInsideLoadData = True
        LoadBlankHeadGrid()
        Dim arr As New List(Of String)
        For Each dr As DataRow In dtAllData.Rows
            Dim strCode As String = clsCommon.myCstr(dr("code"))
            If Not arr.Contains(strCode) Then
                arr.Add(strCode)
                gvHead.Rows.AddNew()
                gvHead.Rows(gvHead.RowCount - 1).Cells(colHSelect).Value = False
                gvHead.Rows(gvHead.RowCount - 1).Cells(colHCode).Value = strCode
                gvHead.Rows(gvHead.RowCount - 1).Cells(colHDate).Value = clsCommon.myCstr(dr("Document_Date"))
                gvHead.Rows(gvHead.RowCount - 1).Cells(colHCustomerCode).Value = clsCommon.myCstr(dr("Customer"))
                gvHead.Rows(gvHead.RowCount - 1).Cells(colHCustomerName).Value = clsCommon.myCstr(dr("CustomerName"))
            End If
        Next
        IsInsideLoadData = False
    End Sub

    Sub LoadBlankHeadGrid()
        gvHead.Rows.Clear()
        gvHead.Columns.Clear()

        Dim repoSelect As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoSelect.HeaderText = " "
        repoSelect.Name = colHSelect
        repoSelect.ReadOnly = False
        repoSelect.Width = 25
        repoSelect.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gvHead.MasterTemplate.Columns.Add(repoSelect)


        Dim repoCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCode.FormatString = ""
        repoCode.HeaderText = "Dispatch No"
        repoCode.Name = colHCode
        repoCode.Width = 170
        repoCode.ReadOnly = True
        gvHead.MasterTemplate.Columns.Add(repoCode)

        Dim repoDate As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoDate.FormatString = ""
        repoDate.HeaderText = "Date"
        repoDate.Name = colHDate
        repoDate.Width = 70
        repoDate.ReadOnly = True
        gvHead.MasterTemplate.Columns.Add(repoDate)

        Dim repocustomer As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repocustomer.FormatString = ""
        repocustomer.HeaderText = "Customer"
        repocustomer.Name = colHCustomerCode
        repocustomer.Width = 170
        repocustomer.ReadOnly = True
        gvHead.MasterTemplate.Columns.Add(repocustomer)

        Dim repoCustomerName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCustomerName.FormatString = ""
        repoCustomerName.HeaderText = "Customer Name"
        repoCustomerName.Name = colHCustomerName
        repoCustomerName.Width = 170
        repoCustomerName.ReadOnly = True
        gvHead.MasterTemplate.Columns.Add(repoCustomerName)

        gvHead.ShowFilteringRow = True
        gvHead.EnableFiltering = True
        gvHead.AllowDeleteRow = False
        gvHead.AllowAddNewRow = False
        gvHead.ShowGroupPanel = False
        gvHead.AllowColumnReorder = False
        gvHead.AllowRowReorder = False
        gvHead.EnableSorting = False
        gvHead.EnableAlternatingRowColor = True
        gvHead.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gvHead.MasterTemplate.ShowRowHeaderColumn = False
        gvHead.TableElement.TableHeaderHeight = 40
    End Sub

    Sub LoadBlankGridDetail()
        gv1.Rows.Clear()
        gv1.Columns.Clear()

        Dim repoSelect As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoSelect.HeaderText = " "
        repoSelect.Name = colDSelect
        repoSelect.ReadOnly = False
        repoSelect.Width = 25
        repoSelect.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoSelect)


        Dim repoCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCode.FormatString = ""
        repoCode.HeaderText = "Dispatch No"
        repoCode.Name = colDCode
        repoCode.Width = 180
        repoCode.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoCode)

        Dim repoICode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoICode.FormatString = ""
        repoICode.HeaderText = "Item Code"
        repoICode.Name = colDICode
        repoICode.Width = 100
        repoICode.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoICode)

        Dim repoIName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoIName.FormatString = ""
        repoIName.HeaderText = "Item"
        repoIName.Name = colDIName
        repoIName.Width = 180
        repoIName.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoIName)

        Dim repoUnit As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoUnit.FormatString = ""
        repoUnit.HeaderText = "UOM"
        repoUnit.Name = colDUnitCode
        repoUnit.Width = 180
        repoUnit.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoUnit)

        Dim repoFatPer As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoFatPer.FormatString = ""
        repoFatPer.HeaderText = "Fat Per"
        repoFatPer.Name = colDFatPer
        repoFatPer.ReadOnly = True
        repoFatPer.IsVisible = True
        repoFatPer.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoFatPer)

        Dim repoSNFPer As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoSNFPer.FormatString = ""
        repoSNFPer.HeaderText = "SNF Per"
        repoSNFPer.Name = colDSNFPer
        repoSNFPer.ReadOnly = True
        repoSNFPer.IsVisible = True
        repoSNFPer.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoSNFPer)

        Dim repoCLR As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoCLR.FormatString = "{0:n2}"
        repoCLR.HeaderText = "CLR"
        repoCLR.Name = colDCLR
        repoCLR.ReadOnly = True
        repoCLR.IsVisible = True
        repoCLR.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoCLR)

        Dim repoRate As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoRate.FormatString = ""
        repoRate.HeaderText = "Rate"
        repoRate.Name = colDRate
        repoRate.ReadOnly = True
        repoRate.IsVisible = True
        repoRate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoRate)

        Dim repoAmount As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAmount.FormatString = ""
        repoAmount.HeaderText = "Amount"
        repoAmount.Name = colDAmount
        repoAmount.ReadOnly = True
        repoAmount.IsVisible = True
        repoAmount.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoAmount)

        Dim repoOrderQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoOrderQty.FormatString = ""
        repoOrderQty.HeaderText = "Dispatch Qty"
        repoOrderQty.Name = colDOrderQty
        repoOrderQty.ReadOnly = True
        repoOrderQty.Width = 80
        repoOrderQty.WrapText = True
        repoOrderQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoOrderQty)

        Dim repoAppQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAppQty.FormatString = ""
        repoAppQty.HeaderText = "Invoice Qty"
        repoAppQty.Name = colDApprovedQty
        repoAppQty.ReadOnly = True
        repoAppQty.Width = 100
        repoAppQty.WrapText = True
        repoAppQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoAppQty)

        Dim repoUnAppQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoUnAppQty.FormatString = ""
        repoUnAppQty.HeaderText = "Unapproved Qty"
        repoUnAppQty.Name = colDUnApprovedQty
        repoUnAppQty.ReadOnly = True
        repoUnAppQty.Width = 80
        repoUnAppQty.WrapText = True
        repoUnAppQty.IsVisible = False
        repoUnAppQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoUnAppQty)


        Dim repoPendingQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoPendingQty = New GridViewDecimalColumn()
        repoPendingQty.FormatString = ""
        repoPendingQty.HeaderText = "Pending"
        repoPendingQty.Name = colDPendingQty
        repoPendingQty.ReadOnly = True
        repoPendingQty.Width = 80
        repoPendingQty.WrapText = True
        repoPendingQty.IsVisible = False
        repoPendingQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoPendingQty)

        Dim repoFatWeightage As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoFatWeightage = New GridViewDecimalColumn()
        repoFatWeightage.FormatString = ""
        repoFatWeightage.HeaderText = "Fat Weightage"
        repoFatWeightage.Name = colDFatWeightage
        repoFatWeightage.ReadOnly = True
        repoFatWeightage.Width = 80
        repoFatWeightage.WrapText = True
        repoFatWeightage.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoFatWeightage)

        Dim repoSnfWeightage As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoSnfWeightage = New GridViewDecimalColumn()
        repoSnfWeightage.FormatString = ""
        repoSnfWeightage.HeaderText = "SNF Weightage"
        repoSnfWeightage.Name = colDSNFWeightage
        repoSnfWeightage.ReadOnly = True
        repoSnfWeightage.Width = 80
        repoSnfWeightage.WrapText = True
        repoSnfWeightage.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoSnfWeightage)

        ''richa agarwal 12/01/2014
        Dim repoFatKG As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoFatKG = New GridViewDecimalColumn()
        repoFatKG.FormatString = "{0:n3}"
        repoFatKG.HeaderText = "Fat KG"
        repoFatKG.Name = colDFatKG
        repoFatKG.ReadOnly = True
        repoFatKG.Width = 80
        repoFatKG.WrapText = True
        repoFatKG.IsVisible = False
        repoFatKG.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoFatKG)


        Dim repoSNFKG As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoSNFKG = New GridViewDecimalColumn()
        repoSNFKG.FormatString = "{0:n3}"
        repoSNFKG.HeaderText = "SNF KG"
        repoSNFKG.Name = colDSNFKG
        repoSNFKG.ReadOnly = True
        repoSNFKG.Width = 80
        repoSNFKG.WrapText = True
        repoSNFKG.IsVisible = False
        repoSNFKG.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoSNFKG)

        ''-------------------------

        gv1.AllowAddNewRow = False
        gv1.AllowDeleteRow = False
        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = True
        gv1.AllowRowReorder = True
        gv1.EnableSorting = False
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        gv1.MasterTemplate.ShowColumnHeaders = True
        gv1.EnableAlternatingRowColor = True
        gv1.EnableFiltering = True
        gv1.ShowFilteringRow = True
        gv1.TableElement.TableHeaderHeight = 40
    End Sub

    ''Sub setGridPropery()
    ''    gv1.AllowAddNewRow = False
    ''    gv1.ShowGroupPanel = False
    ''    gv1.AllowColumnReorder = True
    ''    gv1.AllowRowReorder = True
    ''    gv1.EnableSorting = False
    ''    gv1.MasterTemplate.ShowRowHeaderColumn = False
    ''    gv1.MasterTemplate.ShowColumnHeaders = True
    ''    ''gv1.MasterTemplate.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill
    ''    gv1.EnableAlternatingRowColor = True
    ''    gv1.EnableFiltering = True
    ''    gv1.ShowFilteringRow = True
    ''End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        btnCancelPressed()
    End Sub

    Private Sub btnOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOk.Click
        btnOKPressed()
    End Sub

    Sub btnCancelPressed()
        Me.Close()
    End Sub

    Sub btnOKPressed()
        If Not isAllowed() Then
            Exit Sub
        End If

        ArrReturn = New List(Of clsDispatchDetailBulkSale)
        Dim obj As clsDispatchDetailBulkSale = Nothing
        For ii As Integer = 0 To gv1.RowCount - 1
            If (clsCommon.myCBool(gv1.Rows(ii).Cells(colDSelect).Value)) Then
                obj = New clsDispatchDetailBulkSale()
                obj.Document_No = clsCommon.myCstr(gv1.Rows(ii).Cells(colDCode).Value)
                obj.Item_Code = clsCommon.myCstr(gv1.Rows(ii).Cells(colDICode).Value)
                'obj.Qty = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDOrderQty).Value)
                obj.Unit_code = clsCommon.myCstr(gv1.Rows(ii).Cells(colDUnitCode).Value)
                obj.Qty = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDPendingQty).Value)
                obj.FatPer = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDFatPer).Value)
                obj.SNFPer = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDSNFPer).Value)
                obj.CLR = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDCLR).Value)
                obj.Rate = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDRate).Value)
                obj.Amount = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDAmount).Value)
                obj.Fat_KG = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDFatKG).Value)
                obj.SNF_KG = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDSNFKG).Value)
                If (obj.Qty > 0) Then
                    ArrReturn.Add(obj)
                End If
            End If
        Next

        If ArrReturn.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please select at least one non zero Pending PO item")
        Else
            Me.Close()
        End If
    End Sub

    Private Function isAllowed() As Boolean
        Dim arrVendor As New List(Of String)
        For ii As Integer = 0 To gvHead.RowCount - 1
            If clsCommon.myCBool(gvHead.Rows(ii).Cells(colHSelect).Value) Then
                Dim strCode As String = clsCommon.myCstr(gvHead.Rows(ii).Cells(colHCode).Value)
                For jj As Integer = 0 To gv1.RowCount - 1
                    If clsCommon.myCBool(gv1.Rows(jj).Cells(colDSelect).Value) AndAlso clsCommon.CompairString(strCode, clsCommon.myCstr(gv1.Rows(jj).Cells(colDCode).Value)) = CompairStringResult.Equal Then
                        Dim strVendorCode As String = clsCommon.myCstr(gvHead.Rows(ii).Cells(colHCustomerCode).Value)
                        If Not arrVendor.Contains(strVendorCode) Then
                            arrVendor.Add(strVendorCode)
                            If arrVendor.Count > 1 Then
                                clsCommon.MyMessageBoxShow("Items for more than one customer not acceptable ")
                                Return False
                            End If
                            customerCode = strVendorCode
                            customerName = clsCommon.myCstr(gvHead.Rows(ii).Cells(colHCustomerName).Value)
                        End If
                    End If
                Next
            End If
        Next
        Return True
    End Function

    Private Sub FrmPendingRequistion_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.F5 Then
            btnOKPressed()
        ElseIf e.KeyCode = Keys.Escape Then
            btnCancelPressed()
        End If
    End Sub

    Private Sub gv1_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gv1.DoubleClick
        If gv1.CurrentCell Is gv1.Columns(colDCode) Then
            Dim strPONO As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colDCode).Value)
            Dim SelectStatus As Boolean = clsCommon.myCBool(gv1.CurrentRow.Cells(colDSelect).Value)
            For ii As Integer = 0 To gv1.Rows.Count - 1
                If clsCommon.CompairString(strPONO, clsCommon.myCstr(gv1.Rows(ii).Cells(colDCode).Value)) = CompairStringResult.Equal Then
                    gv1.Rows(ii).Cells(colDSelect).Value = Not SelectStatus
                End If
            Next
        End If
    End Sub

    Private Sub gvHead_ValueChanging(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.ValueChangingEventArgs) Handles gvHead.ValueChanging
        ''richa agarwal 09/04/2015 against ticket no BM00000006114
        Try
            If Not IsInsideLoadData AndAlso gvHead.CurrentRow.Index >= 0 Then
                Dim strCode As String = clsCommon.myCstr(gvHead.CurrentRow.Cells(colHCode).Value)
                If clsCommon.myLen(strCode) > 0 Then

                    LoadDetailData(e.NewValue, strCode)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)

        End Try
        'If Not IsInsideLoadData Then
        '    If gvHead.CurrentColumn Is gvHead.Columns(colHSelect) Then
        '        Dim strVendorCode As String = clsCommon.myCstr(gvHead.CurrentRow.Cells(colHVendorCode).Value)
        '        Dim strVendorName As String = clsCommon.myCstr(gvHead.CurrentRow.Cells(colHVendorName).Value)
        '        If clsCommon.myLen(VendorCode) <= 0 Then
        '            VendorCode = strVendorCode
        '            VendorName = strVendorName
        '        End If

        '        If clsCommon.CompairString(strVendorCode, VendorCode) = CompairStringResult.Equal Then
        '            Dim strCode As String = clsCommon.myCstr(gvHead.CurrentRow.Cells(colHCode).Value)
        '            If clsCommon.myLen(strCode) > 0 Then
        '                LoadDetailData(e.NewValue, strCode)
        '            End If
        '        Else
        '            common.clsCommon.MyMessageBoxShow("PO's Vendor should be `" + VendorName)
        '            e.Cancel = True
        '        End If
        '    End If
        'End If
    End Sub

    Sub LoadDetailData(ByVal NewVal As Boolean, ByVal strCode As String)
        ' Try

        If NewVal Then
            For Each dr As DataRow In dtAllData.Rows
                If clsCommon.CompairString(strCode, clsCommon.myCstr(dr("Code"))) = CompairStringResult.Equal Then
                    For i As Integer = 1 To gv1.Rows.Count
                        If clsCommon.myCdbl(gv1.Rows(0).Cells(colDFatWeightage).Value) <> clsCommon.myCdbl(dr("Fat_Weightage")) Then
                            '' gvHead.CurrentRow.Cells(colDSelect).Value = False
                            Throw New Exception("Fat Weightage should be same for all selected dispatch")
                        End If
                        If clsCommon.myCdbl(gv1.Rows(0).Cells(colDSNFWeightage).Value) <> clsCommon.myCdbl(dr("Snf_Weightage")) Then
                            'gvHead.CurrentRow.Cells(colDSelect).Value = False
                            Throw New Exception("SNF Weightage should be same for all selected dispatch")
                        End If
                    Next

                    gv1.Rows.AddNew()


                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDSelect).Value = True
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDCode).Value = clsCommon.myCstr(dr("code"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDICode).Value = clsCommon.myCstr(dr("ICode"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDIName).Value = clsCommon.myCstr(dr("IName"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDUnitCode).Value = clsCommon.myCstr(dr("UnitCode"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDOrderQty).Value = clsCommon.myCdbl(dr("DispatchQty"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDApprovedQty).Value = clsCommon.myCdbl(dr("InvoiceQty"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDUnApprovedQty).Value = clsCommon.myCdbl(dr("UnapprovedQty"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDPendingQty).Value = clsCommon.myCdbl(dr("PendingQty"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDRate).Value = clsCommon.myCdbl(dr("Rate"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDAmount).Value = clsCommon.myCdbl(dr("Amount"))
                    'gv1.Rows(gv1.Rows.Count - 1).Cells(colDAmount).Value = clsCommon.myCdbl(dr("Rate")) * clsCommon.myCdbl(dr("PendingQty"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDFatPer).Value = clsCommon.myCdbl(dr("FatPer"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDSNFPer).Value = clsCommon.myCdbl(dr("SNFPer"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDCLR).Value = clsCommon.myCdbl(dr("CLR"))
                    ''richa 18/09/2014
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDFatWeightage).Value = clsCommon.myCdbl(dr("Fat_Weightage"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDSNFWeightage).Value = clsCommon.myCdbl(dr("Snf_Weightage"))

                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDFatKG).Value = clsCommon.myCdbl(dr("Invoice_Fat_KG"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDSNFKG).Value = clsCommon.myCdbl(dr("Invoice_SNF_KG"))

                End If
            Next
        Else
            For ii As Integer = gv1.Rows.Count - 1 To 0 Step -1
                If clsCommon.CompairString(strCode, clsCommon.myCstr(gv1.Rows(ii).Cells(colDCode).Value)) = CompairStringResult.Equal Then
                    gv1.Rows.RemoveAt(ii)
                End If
            Next
        End If

        'Catch ex As Exception
        '    clsCommon.MyMessageBoxShow(ex.Message)
        '    gvHead.CurrentRow.Cells(colDSelect).Value = False
        'End Try
    End Sub

End Class

