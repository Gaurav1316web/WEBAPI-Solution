Imports common
Imports System.Data.SqlClient
Imports System
Imports XpertERPEngine
Imports System.IO
Imports System.Windows.Forms
Imports Telerik.WinControls.UI

'----------------------Done By Preeti Gupta 29/05/2014-------BM00000002659----------
Public Class FrmSaleHistory
    Inherits FrmMainTranScreen
#Region "Variable"
    Const colItemNo As String = "Item Number"
    Const colItemDec As String = "Item Description"
    Const colQuantitySold As String = "Quantity Sold"
    Const colSalesAmount As String = "Sales Amount"
    Const colCostOfSale As String = "Cost Of sales"
    Const colReturnAmount As String = "Return Amount"
    Const colMarginPercent As String = "Margin Percent"
    Const colCustNo As String = "Customer Number"
    Const colCustName As String = "Customer Name"
    Const colCurrency As String = "Currency"
    Dim ButtonToolTip As ToolTip = New ToolTip()

    Public strFormId As String
    Public strCustId As String = ""
    Public strCustName As String = ""


#End Region
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmSaleHistory)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
            'Me.Close()
            'Exit Function
        End If

    End Sub
    Private Sub btnReferesh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReferesh.Click

        LoadData()
    End Sub
    Sub LoadData()

        Dim qry As String = ""
        Dim customer As String = txtCustomer.Value

        If clsCommon.myLen(txtCustomer.Value) <= 0 Then
            If cboSelectedBy.Text = "Customer" Then
                common.clsCommon.MyMessageBoxShow(Me, "Please select Customer", Me.Text)
                cboSelectedBy.Focus()
                Exit Sub

            Else
                common.clsCommon.MyMessageBoxShow(Me, "Please select item Code", Me.Text)
                cboSelectedBy.Focus()
                Exit Sub
            End If
        End If

        Dim dt As DataTable
        Dim strFromDate As String = clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy")
        Dim strToDate As String = clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy")
        Try
            RadPageView1.SelectedPage = RadPageViewPage1
            If (rrbPosted.IsChecked) = True Then


                If cboSelectedBy.Text = "Customer" Then

                    qry = " Select [Item Number],max([Item Description])as [Item Description] ,"
                    qry += " SUM([Quantity Sold]) as [Quantity Sold] ,SUM ([Sale Amount]) as [Sale Amount],'0' as [Cost of sales],"
                    qry += " SUM([Return qty])as [Return Qty],SUM([Return Amount]) as [Return Amount],(((SUM ([Sale Amount])+'0'+ SUM([Return Amount]))/SUM ([Sale Amount]))*100) as [Margin Percent],(SUM ([Sale Amount])-SUM([Return Amount])) as [Net Amount] from ("
                    qry += " select  TSPL_SD_SALE_INVOICE_DETAIL.Item_Code as [Item Number],Item_Desc as [Item Description],"
                    qry += " TSPL_SD_SALE_INVOICE_DETAIL.Qty as [Quantity Sold] ,TSPL_SD_SALE_INVOICE_DETAIL.Amt_Less_Discount as [Sale Amount],"
                    qry += " '0' as [Cost of sales] ,isnull(TSPL_SD_SALE_RETURN_DETAIL.Qty ,0) as [Return qty],coalesce(TSPL_SD_SALE_RETURN_DETAIL.Amount,0) as [Return Amount] ,'0' as [Margin Percent] "
                    qry += " from TSPL_SD_SALE_INVOICE_DETAIL left outer join TSPL_ITEM_MASTER on TSPL_SD_SALE_INVOICE_DETAIL.Item_Code =TSPL_ITEM_MASTER.Item_Code "
                    qry += " left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code =TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE "
                    qry += " left outer join TSPL_SD_SALE_RETURN_DETAIL  on TSPL_SD_SALE_RETURN_DETAIL.Invoice_Code =TSPL_SD_SALE_INVOICE_HEAD.Document_Code"
                    qry += " and TSPL_SD_SALE_RETURN_DETAIL.Item_Code =TSPL_SD_SALE_INVOICE_detail.Item_Code"
                    qry += " where Customer_Code ='" + customer + "' and TSPL_SD_SALE_INVOICE_HEAD.Status=1"
                    qry += " and cast(Document_Date as date)>='" + strFromDate + "'"
                    qry += " and cast(Document_Date as date)<='" + strToDate + "') "
                    qry += "  XXX Group By [Item Number]"
                ElseIf cboSelectedBy.Text = "Item Code" Then
                    qry = " Select Customer_Code  as [Customer Number],max(Customer_Name) as [Customer Name],"
                    qry += " max(CURRENCY_NAME) as Currency, SUM(Qty) as [Quantity Sold] ,"
                    qry += " sum(Amt_Less_Discount)as [Sales Amount], '0' as [Cost Of Sales],SUM([Return qty])as [Return Qty],sum(ReturnAmount ) as [Return Amount]"
                    qry += " ,(((sum(Amt_Less_Discount)+'0'+sum(ReturnAmount ))/sum(Amt_Less_Discount))*100) as [Margin Percent] ,(sum(Amt_Less_Discount)-sum(ReturnAmount )) as [Net Amount] from ("
                    qry += " select TSPL_SD_SALE_INVOICE_HEAD.Customer_Code,TSPL_CUSTOMER_MASTER.Customer_Name  ,"
                    qry += " TSPL_CURRENCY_MASTER.CURRENCY_NAME , TSPL_SD_SALE_INVOICE_DETAIL.Item_Code, TSPL_SD_SALE_INVOICE_DETAIL.Qty,isnull(TSPL_SD_SALE_RETURN_DETAIL.Qty ,0) as [Return qty],"
                    qry += " coalesce(TSPL_SD_SALE_RETURN_DETAIL.Amount,0) as ReturnAmount,TSPL_SD_SALE_INVOICE_DETAIL.Amt_Less_Discount  from TSPL_SD_SALE_INVOICE_HEAD"
                    qry += " left outer join TSPL_SD_SALE_INVOICE_DETAIL on TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE=TSPL_SD_SALE_INVOICE_HEAD.Document_Code"
                    qry += " left outer join TSPL_SD_SALE_RETURN_DETAIL  on TSPL_SD_SALE_RETURN_DETAIL.Invoice_Code =TSPL_SD_SALE_INVOICE_HEAD.Document_Code"
                    qry += " and TSPL_SD_SALE_RETURN_DETAIL.Item_Code =TSPL_SD_SALE_INVOICE_detail.Item_Code"
                    qry += " left outer join TSPL_CURRENCY_MASTER on TSPL_SD_SALE_INVOICE_HEAD.CURRENCY_CODE =TSPL_CURRENCY_MASTER.CURRENCY_CODE "
                    qry += " left outer join TSPL_CUSTOMER_MASTER on TSPL_SD_SALE_INVOICE_HEAD.Customer_Code =TSPL_CUSTOMER_MASTER.Cust_Code"
                    qry += " WHERE TSPL_SD_SALE_INVOICE_DETAIL.Item_Code='" + customer + "' "
                    qry += " ANd TSPL_SD_SALE_INVOICE_HEAD.Status=1  and cast(Document_Date as date )>= '" + strFromDate + "'"
                    qry += " and cast(Document_Date as date)<='" + strToDate + "' )"

                    qry += " XXX  Group By Customer_Code, Item_Code "

                End If
            ElseIf (rrbAll.IsChecked) = True Then

                If cboSelectedBy.Text = "Customer" Then

                    qry = "Select [Item Number],max([Item Description])as [Item Description] ,"
                    qry += " SUM([Quantity Sold]) as [Quantity Sold] ,SUM ([Sale Amount]) as [Sale Amount],'0' as [Cost of sales],"
                    qry += " SUM([Return qty])as [Return Qty],SUM([Return Amount]) as [Return Amount],(((SUM ([Sale Amount])+'0'+ SUM([Return Amount]))/SUM ([Sale Amount]))*100) as [Margin Percent] ,(SUM ([Sale Amount])-SUM([Return Amount])) as [Net Amount] from ("
                    qry += " select  TSPL_SD_SALE_INVOICE_DETAIL.Item_Code as [Item Number],"
                    qry += " Item_Desc as [Item Description], TSPL_SD_SALE_INVOICE_DETAIL.Qty as [Quantity Sold] ,"
                    qry += " TSPL_SD_SALE_INVOICE_DETAIL.Amt_Less_Discount as [Sale Amount], '0' as [Cost of sales] ,isnull(TSPL_SD_SALE_RETURN_DETAIL.Qty ,0) as [Return qty],"
                    qry += " coalesce(TSPL_SD_SALE_RETURN_DETAIL.Amount,0) as [Return Amount] ,'0' as [Margin Percent] "
                    qry += " from TSPL_SD_SALE_INVOICE_DETAIL left outer join TSPL_ITEM_MASTER on TSPL_SD_SALE_INVOICE_DETAIL.Item_Code =TSPL_ITEM_MASTER.Item_Code"
                    qry += " left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code =TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE"
                    qry += " left outer join TSPL_SD_SALE_RETURN_DETAIL  on TSPL_SD_SALE_RETURN_DETAIL.Invoice_Code =TSPL_SD_SALE_INVOICE_HEAD.Document_Code and"
                    qry += " TSPL_SD_SALE_RETURN_DETAIL.Item_Code =TSPL_SD_SALE_INVOICE_detail.Item_Code"
                    qry += " where Customer_Code ='" + customer + "'  and cast(Document_Date as date)>='" + strFromDate + "'"
                    qry += " and cast(Document_Date as date)<='" + strToDate + "') "
                    qry += "  XXX Group By [Item Number]"
                ElseIf cboSelectedBy.Text = "Item Code" Then
                    qry = " Select Customer_Code  as [Customer Number],max(Customer_Name) as [Customer Name],"
                    qry += " max(CURRENCY_NAME) as Currency, SUM(Qty) as [Quantity Sold] ,"
                    qry += " sum(Amt_Less_Discount)as [Sales Amount], '0' as [Cost Of Sales],SUM([Return qty])as [Return Qty],sum(ReturnAmount ) as [Return Amount]"
                    qry += " ,(((sum(Amt_Less_Discount)+'0'+sum(ReturnAmount ))/sum(Amt_Less_Discount))*100) as [Margin Percent] ,(sum(Amt_Less_Discount)-sum(ReturnAmount )) as [Net Amount] from ("
                    qry += " select TSPL_SD_SALE_INVOICE_HEAD.Customer_Code,TSPL_CUSTOMER_MASTER.Customer_Name  ,"
                    qry += " TSPL_CURRENCY_MASTER.CURRENCY_NAME , TSPL_SD_SALE_INVOICE_DETAIL.Item_Code, TSPL_SD_SALE_INVOICE_DETAIL.Qty,isnull(TSPL_SD_SALE_RETURN_DETAIL.Qty ,0) as [Return qty],"
                    qry += " coalesce(TSPL_SD_SALE_RETURN_DETAIL.Amount,0) as ReturnAmount,TSPL_SD_SALE_INVOICE_DETAIL.Amt_Less_Discount, "
                    qry += " from TSPL_SD_SALE_INVOICE_HEAD left outer join TSPL_SD_SALE_INVOICE_DETAIL on TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE=TSPL_SD_SALE_INVOICE_HEAD.Document_Code"
                    qry += " left outer join TSPL_SD_SALE_RETURN_DETAIL  on TSPL_SD_SALE_RETURN_DETAIL.Invoice_Code =TSPL_SD_SALE_INVOICE_HEAD.Document_Code and TSPL_SD_SALE_RETURN_DETAIL.Item_Code =TSPL_SD_SALE_INVOICE_detail.Item_Code"
                    qry += " left outer join TSPL_CURRENCY_MASTER on TSPL_SD_SALE_INVOICE_HEAD.CURRENCY_CODE =TSPL_CURRENCY_MASTER.CURRENCY_CODE "
                    qry += " left outer join TSPL_CUSTOMER_MASTER on TSPL_SD_SALE_INVOICE_HEAD.Customer_Code =TSPL_CUSTOMER_MASTER.Cust_Code"
                    qry += " WHERE TSPL_SD_SALE_INVOICE_DETAIL.Item_Code='" + customer + "'"
                    qry += " and cast(Document_Date as date)>= '" + strFromDate + "'"
                    qry += " and cast(Document_Date as date)<='" + strToDate + "' )"
                    qry += " XXX  Group By Customer_Code, Item_Code "

                End If
            End If
            dt = clsDBFuncationality.GetDataTable(qry)
            gvCustomer.DataSource = Nothing
            gvCustomer.Columns.Clear()
            gvCustomer.Rows.Clear()
            gvCustomer.GroupDescriptors.Clear()
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub
            End If
            gvCustomer.DataSource = dt
            If cboSelectedBy.Text = "Customer" Then
                FormatGridIsCustomerWise()
            Else
                FormatGridIsItemWise()
            End If
            ReStoreGridLayout()
            RadPageView1.Visible = True
            RadPageViewPage2.Enabled = False

            gvCustomer.Visible = True

            gvCustomer.ReadOnly = True

        Catch ex As Exception
            Throw New Exception(ex.Message)

        End Try
    End Sub

    Sub FormatGridIsCustomerWise()
        gvCustomer.Columns("Item Number").Width = 50
        gvCustomer.Columns("Item Description").Width = 200
        gvCustomer.Columns("Quantity Sold").Width = 50
        gvCustomer.Columns("Sale Amount").Width = 80
        gvCustomer.Columns("Cost of sales").Width = 80
        gvCustomer.Columns("Return Qty").Width = 100
        gvCustomer.Columns("Return Amount").Width = 100
        gvCustomer.Columns("Margin Percent").Width = 80
        gvCustomer.Columns("Net Amount").Width = 100
    End Sub
    Sub FormatGridIsItemWise()
        gvCustomer.Columns("Customer Number").Width = 100
        gvCustomer.Columns("Customer Name").Width = 200
        gvCustomer.Columns("Currency").Width = 100
        gvCustomer.Columns("Quantity Sold").Width = 50
        gvCustomer.Columns("sales Amount").Width = 80
        gvCustomer.Columns("Cost Of Sales").Width = 80
        gvCustomer.Columns("Return Qty").Width = 100
        gvCustomer.Columns("Return Amount").Width = 80
        gvCustomer.Columns("Margin Percent").Width = 80
        gvCustomer.Columns("Net Amount").Width = 100
    End Sub

    'Sub LoadBlankGridForCustomer()
    '    gvCustomer.Rows.Clear()
    '    gvCustomer.Columns.Clear()

    '    Dim repoItemNo As GridViewDecimalColumn = New GridViewDecimalColumn()
    '    repoItemNo = New GridViewDecimalColumn()
    '    repoItemNo.FormatString = ""
    '    repoItemNo.HeaderText = "Item Number"
    '    repoItemNo.Name = colItemNo
    '    repoItemNo.Width = 50
    '    repoItemNo.ReadOnly = True
    '    repoItemNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
    '    gvCustomer.MasterTemplate.Columns.Add(repoItemNo)

    '    Dim repoItemDesc As GridViewComboBoxColumn = New GridViewComboBoxColumn()
    '    repoItemDesc.FormatString = ""
    '    repoItemDesc.HeaderText = "Item Description"
    '    repoItemDesc.Name = colItemDec
    '    repoItemDesc.Width = 200
    '    repoItemDesc.ReadOnly = True
    '    repoItemDesc.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
    '    gvCustomer.MasterTemplate.Columns.Add(repoItemDesc)


    '    Dim repoQuantitySold As GridViewDecimalColumn = New GridViewDecimalColumn()
    '    repoQuantitySold = New GridViewDecimalColumn()
    '    repoQuantitySold.FormatString = ""
    '    repoQuantitySold.HeaderText = "Quantity Sold"
    '    repoQuantitySold.Name = colQuantitySold
    '    repoQuantitySold.Width = 200
    '    repoQuantitySold.ReadOnly = True
    '    repoQuantitySold.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
    '    gvCustomer.MasterTemplate.Columns.Add(repoQuantitySold)

    '    Dim repoSaleAmount As GridViewDecimalColumn = New GridViewDecimalColumn()
    '    repoSaleAmount = New GridViewDecimalColumn()
    '    repoSaleAmount.FormatString = ""
    '    repoSaleAmount.HeaderText = "Sales Amount"
    '    repoSaleAmount.Name = colSalesAmount
    '    repoSaleAmount.Width = 200
    '    repoSaleAmount.ReadOnly = True
    '    repoSaleAmount.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
    '    gvCustomer.MasterTemplate.Columns.Add(repoSaleAmount)


    '    Dim repoCostOfSale As GridViewDecimalColumn = New GridViewDecimalColumn()
    '    repoCostOfSale = New GridViewDecimalColumn()
    '    repoCostOfSale.FormatString = ""
    '    repoCostOfSale.HeaderText = "Cost Of Sales"
    '    repoCostOfSale.Name = colCostOfSale
    '    repoCostOfSale.Width = 200
    '    repoCostOfSale.ReadOnly = True
    '    repoCostOfSale.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
    '    gvCustomer.MasterTemplate.Columns.Add(repoCostOfSale)

    '    Dim repoReturnAmount As GridViewDecimalColumn = New GridViewDecimalColumn()
    '    repoReturnAmount = New GridViewDecimalColumn()
    '    repoReturnAmount.FormatString = ""
    '    repoReturnAmount.HeaderText = "Return Amount"
    '    repoReturnAmount.Name = colReturnAmount
    '    repoReturnAmount.Width = 200
    '    repoReturnAmount.ReadOnly = True
    '    repoReturnAmount.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
    '    gvCustomer.MasterTemplate.Columns.Add(repoReturnAmount)

    '    Dim repoMarginPercent As GridViewDecimalColumn = New GridViewDecimalColumn()
    '    repoMarginPercent = New GridViewDecimalColumn()
    '    repoMarginPercent.FormatString = ""
    '    repoMarginPercent.HeaderText = "Margin Percent"
    '    repoMarginPercent.Name = colMarginPercent
    '    repoMarginPercent.Width = 200
    '    repoMarginPercent.ReadOnly = True
    '    repoMarginPercent.Minimum = 0
    '    repoMarginPercent.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
    '    repoMarginPercent.ReadOnly = True
    '    gvCustomer.MasterTemplate.Columns.Add(repoMarginPercent)
    'End Sub
    'Sub LoadBlankGridForItemWise()
    '    gvitem.Rows.Clear()
    '    gvitem.Columns.Clear()

    '    Dim repoCustNo As GridViewDecimalColumn = New GridViewDecimalColumn()
    '    repoCustNo = New GridViewDecimalColumn()
    '    repoCustNo.FormatString = ""
    '    repoCustNo.HeaderText = "Customer Number"
    '    repoCustNo.Name = colItemNo
    '    repoCustNo.Width = 200
    '    repoCustNo.ReadOnly = True
    '    repoCustNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
    '    gvitem.MasterTemplate.Columns.Add(repoCustNo)

    '    Dim repoCustName As GridViewComboBoxColumn = New GridViewComboBoxColumn()
    '    repoCustName.FormatString = ""
    '    repoCustName.HeaderText = "Item Description"
    '    repoCustName.Name = colCustName
    '    repoCustName.Width = 200
    '    repoCustName.ReadOnly = True
    '    repoCustName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
    '    gvitem.MasterTemplate.Columns.Add(repoCustName)

    '    Dim repoCurrency As GridViewComboBoxColumn = New GridViewComboBoxColumn()
    '    repoCurrency.FormatString = ""
    '    repoCurrency.HeaderText = "Currency "
    '    repoCurrency.Name = colCurrency
    '    repoCurrency.Width = 200
    '    repoCurrency.ReadOnly = True
    '    repoCurrency.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
    '    gvitem.MasterTemplate.Columns.Add(repoCurrency)


    '    Dim repoQuantitySold As GridViewDecimalColumn = New GridViewDecimalColumn()
    '    repoQuantitySold = New GridViewDecimalColumn()
    '    repoQuantitySold.FormatString = ""
    '    repoQuantitySold.HeaderText = "Quantity Sold"
    '    repoQuantitySold.Name = colQuantitySold
    '    repoQuantitySold.Width = 200
    '    repoQuantitySold.ReadOnly = True
    '    repoQuantitySold.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
    '    gvitem.MasterTemplate.Columns.Add(repoQuantitySold)

    '    Dim repoSaleAmount As GridViewDecimalColumn = New GridViewDecimalColumn()
    '    repoSaleAmount = New GridViewDecimalColumn()
    '    repoSaleAmount.FormatString = ""
    '    repoSaleAmount.HeaderText = "Sales Amount"
    '    repoSaleAmount.Name = colSalesAmount
    '    repoSaleAmount.Width = 200
    '    repoSaleAmount.ReadOnly = True
    '    repoSaleAmount.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
    '    gvitem.MasterTemplate.Columns.Add(repoSaleAmount)


    '    Dim repoCostOfSale As GridViewDecimalColumn = New GridViewDecimalColumn()
    '    repoCostOfSale = New GridViewDecimalColumn()
    '    repoCostOfSale.FormatString = ""
    '    repoCostOfSale.HeaderText = "Cost Of Sales"
    '    repoCostOfSale.Name = colCostOfSale
    '    repoCostOfSale.Width = 200
    '    repoCostOfSale.ReadOnly = True
    '    repoCostOfSale.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
    '    gvitem.MasterTemplate.Columns.Add(repoCostOfSale)

    '    Dim repoReturnAmount As GridViewDecimalColumn = New GridViewDecimalColumn()
    '    repoReturnAmount = New GridViewDecimalColumn()
    '    repoReturnAmount.FormatString = ""
    '    repoReturnAmount.HeaderText = "Return Amount"
    '    repoReturnAmount.Name = colReturnAmount
    '    repoReturnAmount.Width = 200
    '    repoReturnAmount.ReadOnly = True
    '    repoReturnAmount.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
    '    gvitem.MasterTemplate.Columns.Add(repoReturnAmount)

    '    Dim repoMarginPercent As GridViewDecimalColumn = New GridViewDecimalColumn()
    '    repoMarginPercent = New GridViewDecimalColumn()
    '    repoMarginPercent.FormatString = ""
    '    repoMarginPercent.HeaderText = "Margin Percent"
    '    repoMarginPercent.Name = colMarginPercent
    '    repoMarginPercent.Width = 200
    '    repoMarginPercent.ReadOnly = True
    '    repoMarginPercent.Minimum = 0
    '    repoMarginPercent.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
    '    repoMarginPercent.ReadOnly = True
    '    gvitem.MasterTemplate.Columns.Add(repoMarginPercent)
    'End Sub

    Private Sub cboSelectedBy_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.Data.PositionChangedEventArgs) Handles cboSelectedBy.SelectedIndexChanged
        If cboSelectedBy.Text = "Item Code" Then
            lblCustomer.Text = "Item Code"
            txtCustomer.Value = ""
            txtCustName.Text = ""
            gvCustomer.Visible = False
            RadPageView1.Visible = False

        Else
            lblCustomer.Text = "Customer"
            txtCustomer.Value = ""
            txtCustName.Text = ""
            gvCustomer.Visible = False
            RadPageView1.Visible = False
        End If
    End Sub

    Private Sub txtItemCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean)

    End Sub



    Private Sub txtCustomer__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtCustomer._MYValidating
        If clsCommon.myLen(cboSelectedBy.Text) <= 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "Please select Selected By", Me.Text)
            cboSelectedBy.Focus()
            Exit Sub
        End If
        If cboSelectedBy.Text = "Customer" Then

            Dim qry As String = "select Cust_Code as [Code],Customer_Name  as [Name],Cust_Category_Code as [Cust Category code] ,Cust_Group_Code as [Cust Group Code],Add1 as [Add1],Add2 as [Add2],Add3 as [Add3],City_Code as [City code],State as [state] ,Country as [Country] ,Phone1 as [phone1] ,Phone2 as [Phone2]  from TSPL_CUSTOMER_MASTER "
            txtCustomer.Value = clsCommon.ShowSelectForm("CustomerFin", qry, "Code", "status='N'", txtCustomer.Value, "Code", isButtonClicked)
            txtCustName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Customer_Name from TSPL_CUSTOMER_MASTER where Cust_Code ='" + txtCustomer.Value + "'"))

        Else

            Dim qry As String = "select Item_Code as Code,Item_Desc,Structure_Code ,Structure_Desc ,Purchase_Class_Code  from TSPL_ITEM_MASTER"
            txtCustomer.Value = clsCommon.ShowSelectForm("ItemFinder", qry, "Code", "", txtCustomer.Value, "Code", isButtonClicked)
            txtCustName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Item_Desc from TSPL_ITEM_MASTER where Item_Code ='" + txtCustomer.Value + "'"))
        End If
    End Sub

    Private Sub FrmSaleHistory_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnReferesh, "Press Alt+R for Referesh ")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+E for Reset ")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")

        cboSelectedBy.Text = "Customer"
        txtCustomer.Value = strCustId
        txtCustName.Text = strCustName

        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = txtToDate.Value.AddMonths(-1)
        gvCustomer.Visible = False
        rrbPosted.IsChecked = True
        RadPageView1.Visible = False
        gvDetails.Visible = False
        rrbPosted.Visible = False
        rrbAll.Visible = False


    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()



    End Sub

    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Reset()
    End Sub
    Sub Reset()

        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = txtToDate.Value.AddMonths(-1)
        gvCustomer.Visible = False
        cboSelectedBy.Text = "Customer"
        txtCustomer.Value = ""
        txtCustName.Text = ""
        rrbPosted.IsChecked = True
        lblCustomer.Text = "Customer"
        RadPageView1.Visible = False

    End Sub

    Private Sub gvCustomer_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gvCustomer.DoubleClick
        Dim customer As String = txtCustomer.Value

        gvDetails.MasterTemplate.SummaryRowsBottom.Clear()
        Dim qry As String = ""
        Dim dt As DataTable
        Dim strFromDate As String = clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy")
        Dim strToDate As String = clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy")
        Dim summaryRowItem As New GridViewSummaryRowItem()

        gvDetails.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        Dim item1 As New GridViewSummaryItem("Qty", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)
        Dim item2 As New GridViewSummaryItem("Amount", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item2)
        Dim item3 As New GridViewSummaryItem("Net Amount", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item3)
        Try
            If (rrbPosted.IsChecked) = True Then
                If cboSelectedBy.Text = "Customer" Then
                    Dim strICode As String = clsCommon.myCstr(gvCustomer.CurrentRow.Cells("Item Number").Value)

                    qry = " select max(Type) as [Type],[Doc No] as [Doc No],max([Location]) as [Location],"
                    qry += " max([Doc date]) as [Doc date], max([Unit Code]) as[Unit Code] ,max([Qty])as [Qty],"
                    qry += " MAX([Amount])as [Amount] from  ((select 'Sale Invoice' as Type,"
                    qry += " TSPL_SD_SALE_INVOICE_HEAD.Document_Code as [Doc No],TSPL_SD_SALE_INVOICE_HEAD.Document_Code as Doc1 ,"
                    qry += " TSPL_LOCATION_MASTER.Location_Desc as [Location],TSPL_SD_SALE_INVOICE_HEAD.Document_Date as [Doc date], "
                    qry += " TSPL_SD_SALE_INVOICE_DETAIL.Unit_code as [Unit Code], TSPL_SD_SALE_INVOICE_DETAIL.Qty as [Qty] , "
                    qry += " TSPL_SD_SALE_INVOICE_DETAIL.Amt_Less_Discount as [Amount]   "
                    qry += " from TSPL_SD_SALE_INVOICE_DETAIL  "
                    qry += " left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE  = TSPL_SD_SALE_INVOICE_HEAD.Document_Code "
                    qry += " left outer join TSPL_SD_SALE_RETURN_DETAIL on TSPL_SD_SALE_INVOICE_HEAD.Document_Code =TSPL_SD_SALE_RETURN_DETAIL.Invoice_Code "
                    qry += " left outer join TSPL_LOCATION_MASTER on TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location =TSPL_LOCATION_MASTER.Location_Code where"
                    qry += " TSPL_SD_SALE_INVOICE_DETAIL.Item_Code ='" + strICode + "' and TSPL_SD_SALE_INVOICE_HEAD.Customer_Code='" + customer + "' and cast(Document_Date as date)>='" + strFromDate + "' "
                    qry += " and cast(Document_Date as date)<='" + strToDate + "'  and TSPL_SD_SALE_INVOICE_HEAD.Status=1)"
                    qry += " union all"
                    qry += "(select 'Sale Return' as Type,TSPL_SD_SALE_RETURN_HEAD.Document_Code as [Doc1],"
                    qry += " TSPL_SD_SALE_RETURN_HEAD.Against_Invoice_No as [Doc No] ,TSPL_LOCATION_MASTER.Location_Desc as [Location],"
                    qry += " TSPL_SD_SALE_RETURN_HEAD.Document_Date as [Doc date] ,TSPL_SD_SALE_RETURN_DETAIL.Unit_code ,"
                    qry += " TSPL_SD_SALE_RETURN_DETAIL .qty*-1 as [Qty] ,TSPL_SD_SALE_RETURN_DETAIL .Amount*-1 as [Amount]"

                    qry += " from TSPL_SD_SALE_RETURN_DETAIL "
                    qry += " left outer join TSPL_SD_SALE_RETURN_HEAD on TSPL_SD_SALE_RETURN_DETAIL.DOCUMENT_CODE =TSPL_SD_SALE_RETURN_HEAD .Document_Code "
                    qry += " left outer join TSPL_SD_SALE_INVOICE_DETAIL  on TSPL_SD_SALE_INVOICE_DETAIL.Document_Code =TSPL_SD_SALE_RETURN_DETAIL.Invoice_Code"
                    qry += " left outer join TSPL_LOCATION_MASTER on TSPL_SD_SALE_RETURN_HEAD.Bill_To_Location =TSPL_LOCATION_MASTER.Location_Code "
                    qry += " where TSPL_SD_SALE_RETURN_DETAIL.Item_Code ='" + strICode + "' and TSPL_SD_SALE_RETURN_HEAD.Customer_Code='" + customer + "'"
                    qry += " and cast(Document_Date as date)>='" + strFromDate + "' and cast(Document_Date as date)<='" + strToDate + "'  ) ) xxx group by [Doc No],[Doc date]"
                    'qry = " select TSPL_SD_SALE_INVOICE_HEAD.Document_Code as [Doc No],TSPL_LOCATION_MASTER.Location_Desc as [Location],TSPL_SD_SALE_INVOICE_HEAD.Document_Date as [Doc date],"
                    'qry += " TSPL_SD_SALE_INVOICE_DETAIL.Unit_code as [Unit Code], TSPL_SD_SALE_INVOICE_DETAIL.Qty as [Invoice Qty] ,"
                    'qry += " TSPL_SD_SALE_INVOICE_DETAIL.Amt_Less_Discount as [Amount] from TSPL_SD_SALE_INVOICE_DETAIL"
                    'qry += " left outer join TSPL_ITEM_MASTER on TSPL_SD_SALE_INVOICE_DETAIL.Item_Code =TSPL_ITEM_MASTER.Item_Code "
                    'qry += " left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code =TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE "
                    'qry += " left outer join TSPL_SD_SALE_RETURN_DETAIL  on TSPL_SD_SALE_RETURN_DETAIL.Invoice_Code =TSPL_SD_SALE_INVOICE_HEAD.Document_Code"
                    'qry += " left outer join TSPL_LOCATION_MASTER on TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location =TSPL_LOCATION_MASTER.Location_Code "
                    'qry += " and TSPL_SD_SALE_RETURN_DETAIL.Item_Code =TSPL_SD_SALE_INVOICE_detail.Item_Code"
                    'qry += " where TSPL_SD_SALE_INVOICE_DETAIL.Item_Code ='" + strICode + "'"
                    'qry += " and TSPL_SD_SALE_INVOICE_HEAD.Customer_Code='" + customer + "'"
                    'qry += " and cast(Document_Date as date)>='" + strFromDate + "'"
                    'qry += " and cast(Document_Date as date)<='" + strToDate + "' "
                    'qry += " and TSPL_SD_SALE_INVOICE_HEAD.Status=1 "

                Else
                    Dim strCustomer As String = clsCommon.myCstr(gvCustomer.CurrentRow.Cells("Customer Number").Value)

                    qry = " select max(Type) as [Type],[Doc No] as [Doc No],max([Location]) as [Location],"
                    qry += " max([Doc date]) as [Doc date], max([Unit Code]) as[Unit Code] ,max([Qty])as [Qty],"
                    qry += " MAX([Amount])as [Amount] from  ((select 'Sale Invoice' as Type,"
                    qry += " TSPL_SD_SALE_INVOICE_HEAD.Document_Code as [Doc No],TSPL_SD_SALE_INVOICE_HEAD.Document_Code as Doc1 ,"
                    qry += " TSPL_LOCATION_MASTER.Location_Desc as [Location],TSPL_SD_SALE_INVOICE_HEAD.Document_Date as [Doc date], "
                    qry += " TSPL_SD_SALE_INVOICE_DETAIL.Unit_code as [Unit Code], TSPL_SD_SALE_INVOICE_DETAIL.Qty as [Qty] , "
                    qry += " TSPL_SD_SALE_INVOICE_DETAIL.Amt_Less_Discount as [Amount] "
                    qry += " from TSPL_SD_SALE_INVOICE_DETAIL  "
                    qry += " left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE  = TSPL_SD_SALE_INVOICE_HEAD.Document_Code "
                    qry += " left outer join TSPL_SD_SALE_RETURN_DETAIL on TSPL_SD_SALE_INVOICE_HEAD.Document_Code =TSPL_SD_SALE_RETURN_DETAIL.Invoice_Code "
                    qry += " left outer join TSPL_LOCATION_MASTER on TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location =TSPL_LOCATION_MASTER.Location_Code where"
                    qry += " TSPL_SD_SALE_INVOICE_DETAIL.Item_Code ='" + customer + "' and TSPL_SD_SALE_INVOICE_HEAD.Customer_Code='" + strCustomer + "' and cast(Document_Date as date)>='" + strFromDate + "' "
                    qry += " and cast(Document_Date as date)<='" + strToDate + "'  and TSPL_SD_SALE_INVOICE_HEAD.Status=1)"
                    qry += " union all"
                    qry += "(select 'Sale Return' as Type,TSPL_SD_SALE_RETURN_HEAD.Document_Code as [Doc1],"
                    qry += " TSPL_SD_SALE_RETURN_HEAD.Against_Invoice_No as [Doc No] ,TSPL_LOCATION_MASTER.Location_Desc as [Location],"
                    qry += " TSPL_SD_SALE_RETURN_HEAD.Document_Date as [Doc date] ,TSPL_SD_SALE_RETURN_DETAIL.Unit_code ,"
                    qry += " TSPL_SD_SALE_RETURN_DETAIL .qty*-1 as [Qty] ,TSPL_SD_SALE_RETURN_DETAIL .Amount*-1 as [Amount]"

                    qry += " from TSPL_SD_SALE_RETURN_DETAIL "
                    qry += " left outer join TSPL_SD_SALE_RETURN_HEAD on TSPL_SD_SALE_RETURN_DETAIL.DOCUMENT_CODE =TSPL_SD_SALE_RETURN_HEAD .Document_Code "
                    qry += " left outer join TSPL_SD_SALE_INVOICE_DETAIL  on TSPL_SD_SALE_INVOICE_DETAIL.Document_Code =TSPL_SD_SALE_RETURN_DETAIL.Invoice_Code"
                    qry += " left outer join TSPL_LOCATION_MASTER on TSPL_SD_SALE_RETURN_HEAD.Bill_To_Location =TSPL_LOCATION_MASTER.Location_Code "
                    qry += " where TSPL_SD_SALE_RETURN_DETAIL.Item_Code ='" + customer + "' and TSPL_SD_SALE_RETURN_HEAD.Customer_Code='" + strCustomer + "'"
                    qry += " and cast(Document_Date as date)>='" + strFromDate + "' and cast(Document_Date as date)<='" + strToDate + "'  ) ) xxx group by [Doc No],[Doc date]"

                End If
                'ElseIf (rrbAll.IsChecked) = True Then
                '    If cboSelectedBy.Text = "Customer" Then
                '        Dim strICode As String = clsCommon.myCstr(gvCustomer.CurrentRow.Cells("Item Number").Value)

                '        qry = " select TSPL_SD_SALE_INVOICE_HEAD.Document_Code as [Doc No],TSPL_LOCATION_MASTER.Location_Desc as [Location],TSPL_SD_SALE_INVOICE_HEAD.Document_Date as [Doc date],"
                '        qry += " TSPL_SD_SALE_INVOICE_DETAIL.Unit_code as [Unit Code], TSPL_SD_SALE_INVOICE_DETAIL.Qty as [Invoice Qty] , "
                '        qry += " TSPL_SD_SALE_INVOICE_DETAIL.Amt_Less_Discount as [Amount] from TSPL_SD_SALE_INVOICE_DETAIL"
                '        qry += " left outer join TSPL_ITEM_MASTER on TSPL_SD_SALE_INVOICE_DETAIL.Item_Code =TSPL_ITEM_MASTER.Item_Code "
                '        qry += " left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code =TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE "
                '        qry += " left outer join TSPL_SD_SALE_RETURN_DETAIL  on TSPL_SD_SALE_RETURN_DETAIL.Invoice_Code =TSPL_SD_SALE_INVOICE_HEAD.Document_Code"
                '        qry += " and TSPL_SD_SALE_RETURN_DETAIL.Item_Code =TSPL_SD_SALE_INVOICE_detail.Item_Code "
                '        qry += " where TSPL_SD_SALE_INVOICE_DETAIL.Item_Code ='" + strICode + "' "
                '        qry += " and TSPL_SD_SALE_INVOICE_HEAD.Customer_Code='" + customer + "' "
                '        qry += " and cast(Document_Date as date)>='" + strFromDate + "' "
                '        qry += " and cast(Document_Date as date)<='" + strToDate + "' "

                '    Else
                '        Dim strCustomer As String = clsCommon.myCstr(gvCustomer.CurrentRow.Cells("Customer Number").Value)

                '        qry = "  select TSPL_SD_SALE_INVOICE_HEAD.Document_Code as [Doc No],TSPL_LOCATION_MASTER.Location_Desc as [Location],TSPL_SD_SALE_INVOICE_HEAD.Document_Date as [Doc date],"
                '        qry += " TSPL_SD_SALE_INVOICE_DETAIL.Unit_code as [Unit Code], TSPL_SD_SALE_INVOICE_DETAIL.Qty as [Invoice Qty] , "
                '        qry += " TSPL_SD_SALE_INVOICE_DETAIL.Amt_Less_Discount as [Amount]   from TSPL_SD_SALE_INVOICE_HEAD"
                '        qry += " left outer join TSPL_SD_SALE_INVOICE_DETAIL on TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE=TSPL_SD_SALE_INVOICE_HEAD.Document_Code"
                '        qry += " left outer join TSPL_SD_SALE_RETURN_DETAIL  on TSPL_SD_SALE_RETURN_DETAIL.Invoice_Code =TSPL_SD_SALE_INVOICE_HEAD.Document_Code "
                '        qry += " and TSPL_SD_SALE_RETURN_DETAIL.Item_Code =TSPL_SD_SALE_INVOICE_detail.Item_Code "
                '        qry += " left outer join TSPL_CURRENCY_MASTER on TSPL_SD_SALE_INVOICE_HEAD.CURRENCY_CODE =TSPL_CURRENCY_MASTER.CURRENCY_CODE "
                '        qry += " left outer join TSPL_CUSTOMER_MASTER on TSPL_SD_SALE_INVOICE_HEAD.Customer_Code =TSPL_CUSTOMER_MASTER.Cust_Code"
                '        qry += " left outer join TSPL_LOCATION_MASTER on TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location =TSPL_LOCATION_MASTER.Location_Code "
                '        qry += " WHERE TSPL_SD_SALE_INVOICE_HEAD.Customer_Code='" + strCustomer + "'"
                '        qry += " and  TSPL_SD_SALE_INVOICE_DETAIL.Item_Code='" + customer + "'"
                '        qry += " and cast(Document_Date as date)>= '" + strFromDate + "' "
                '        qry += " and cast(Document_Date as date)<='" + strToDate + "'  "
                '    End If
            End If


            dt = clsDBFuncationality.GetDataTable(qry)
            gvDetails.DataSource = Nothing
            gvDetails.Columns.Clear()
            gvDetails.Rows.Clear()
            gvDetails.GroupDescriptors.Clear()
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub
            End If
            gvDetails.DataSource = dt

            RadPageView1.Visible = True
            RadPageView1.SelectedPage = RadPageViewPage2
            RadPageViewPage2.Enabled = True
            'If cboSelectedBy.Text = "Customer" Then
            FormatGridCustomerDetails()
            'Else
            'FormatGridItemDetails()
            'End If
            ReStoreGridLayoutDetails()
            gvDetails.ReadOnly = True
            gvDetails.Visible = True



        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try


    End Sub
    Sub FormatGridCustomerDetails()
        gvDetails.Columns("Type").Width = 100
        gvDetails.Columns("Doc No").Width = 100
        gvDetails.Columns("Location").Width = 200
        gvDetails.Columns("Doc date").Width = 100
        gvDetails.Columns("Unit Code").Width = 100
        gvDetails.Columns("Qty").Width = 100
        gvDetails.Columns("Amount").Width = 100


    End Sub


    Private Sub gvDetails_CellDoubleClick(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvDetails.CellDoubleClick
        If clsCommon.CompairString(gvDetails.CurrentRow.Cells("type").Value, "Sale Invoice") = CompairStringResult.Equal Then
            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmSNSaleInvoice, gvDetails.CurrentRow.Cells("Doc No").Value)
        ElseIf clsCommon.CompairString(gvDetails.CurrentRow.Cells("type").Value, "Sale Return") = CompairStringResult.Equal Then
            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.saleReturn, gvDetails.CurrentRow.Cells("Doc No").Value)
        End If

    End Sub
    Private Sub FrmSaleHistory_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.R AndAlso btnReferesh.Enabled Then
            LoadData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.E AndAlso btnReset.Enabled Then
            Reset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Close()

        End If
    End Sub


    Private Sub SaveLayout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveLayoutbtn.Click
        Dim success As Boolean = True
        If clsCommon.myLen(MyBase.Form_ID) > 0 Then

            gvCustomer.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = MyBase.Form_ID & gvCustomer.Name & Microsoft.VisualBasic.Left(cboSelectedBy.Text, 3)
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gvCustomer.SaveLayout(obj.GridLayout)
            obj.GridColumns = gvCustomer.ColumnCount
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If obj.SaveData() Then
                success = True
            End If
            'gvDetails.MasterTemplate.FilterDescriptors.Clear()
            'obj = New clsGridLayout()
            'obj.ReportID = MyBase.Form_ID & gvDetails.Name & Microsoft.VisualBasic.Left(cboSelectedBy.Text, 3)
            'obj.UserID = objCommonVar.CurrentUserCode
            'obj.GridLayout = New MemoryStream()
            'gvDetails.SaveLayout(obj.GridLayout)
            'obj.GridColumns = gvDetails.ColumnCount
            'obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            'success = success And obj.SaveData()
            gvDetails.MasterTemplate.FilterDescriptors.Clear()

            ''richa agarwal regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
            ''---------------

            obj = New clsGridLayout()
            obj.ReportID = MyBase.Form_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gvDetails.SaveLayout(obj.GridLayout)
            obj.GridColumns = gvDetails.ColumnCount
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            success = success And obj.SaveData()
            If success Then
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully", "Information", Me.Text)
            End If

            ''richa agarwal regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
            ''---------------
        End If
    End Sub

    Private Sub DeleteLaayout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteLaayout.Click
        clsGridLayout.DeleteData(MyBase.Form_ID & gvCustomer.Name & Microsoft.VisualBasic.Left(cboSelectedBy.Text, 3), objCommonVar.CurrentUserCode)
        clsGridLayout.DeleteData(MyBase.Form_ID, objCommonVar.CurrentUserCode)
        'clsGridLayout.DeleteData(MyBase.Form_ID, objCommonVar.CurrentUserCode)
        'clsGridLayout.DeleteData(MyBase.Form_ID, objCommonVar.CurrentUserCode)
        'clsGridLayout.DeleteData(MyBase.Form_ID, objCommonVar.CurrentUserCode)

        common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", "Information", Me.Text)
    End Sub

    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(MyBase.Form_ID) > 0 Then

                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(Form_ID & gvCustomer.Name & Microsoft.VisualBasic.Left(cboSelectedBy.Text, 3), "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gvCustomer.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gvCustomer.Columns.Count - 1
                        gvCustomer.Columns(ii).IsVisible = False
                        gvCustomer.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gvCustomer.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)

                End If

            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub
    Private Sub ReStoreGridLayoutDetails()
        Try
            If clsCommon.myLen(MyBase.Form_ID) > 0 Then

                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(Form_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gvDetails.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gvDetails.Columns.Count - 1
                        gvDetails.Columns(ii).IsVisible = False
                        gvDetails.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gvDetails.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)

                End If

            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub
End Class
