Imports Microsoft.VisualBasic
Imports System
Imports System.Text
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

Public Class FrmOverallDiscountReport
    Inherits FrmMainTranScreen

    Dim l1User, l2User, l3User, l4User, l5User As String
    Const colName As String = "Name"
    Const colCode As String = "Code"
    Dim userCode, companyCode, sql, strQuery As String
    Dim dr As DataTable

    Public Sub New(ByVal user As String, ByVal company As String)
        InitializeComponent()
        userCode = user
        companyCode = company
        sql = "SELECT  User_Type,Level1_Code, Level2_Code, Level3_Code, Level4_Code FROM TSPL_USER_MASTER WHERE User_Code='" + userCode + "'"
        dr = clsDBFuncationality.GetDataTable(sql)
        For Each row As DataRow In dr.Rows
            l1User = row(0).ToString()
            l2User = row(1).ToString()
            l3User = row(2).ToString()
            l4User = row(3).ToString()
            l5User = row(4).ToString()
        Next
    End Sub




    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt("OVER-ALL-DC")
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow("Permission Denied")
            Me.Close()
        End If
        '      btnSave.Visible = MyBase.isModifyFlag
        '       btnAuth.Visible = MyBase.isPostFlag
        '        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub FrmOverallDiscountReport_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.P Then
            Print()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag Then
            'SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isPostFlag Then
            'DeleteData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()
        End If
    End Sub



    Private Sub FrmOverallDiscountReport_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        SetUserMgmtNew()
        dtpstart.Value = clsCommon.GETSERVERDATE()
        dtpend.Value = clsCommon.GETSERVERDATE()
        dtpStarttime.Value = DateTime.Now
        dtpendtime.Value = DateTime.Now

        chklocAll.IsChecked = True
        chkCustAll.IsChecked = True
        chkClassAll.IsChecked = True
        chkItemAll1.IsChecked = True
        chkGroupAll.IsChecked = True
        LoadItem()
        LoadCustomer()
        LoadCustomerClass()
        Loadlocation()
        LoadCustomerGroup()
        LoadCompany()
        rbtnCompanyAll.IsChecked = True
        rdbItemwise.IsChecked = True

    End Sub
    Sub Loadlocation()
        Dim qry As String = "select Location_Code,Location_Desc from TSPL_LOCATION_MASTER where Location_Type <> 'Logical'  order by Location_Code"
        cbglocation.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbglocation.ValueMember = "Location_Code"
        cbglocation.DisplayMember = "Location_Desc"
    End Sub

    Sub LoadCustomer()
        Dim qry As String = "select Cust_Code as [Customer Code],Customer_Name as [Customer Name],Customer_Class as [Customer Class] from TSPL_CUSTOMER_MASTER"
        cbgCustomer.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgCustomer.ValueMember = "Customer Code"
        cbgCustomer.DisplayMember = "Customer Code"
    End Sub

    Sub LoadCustomerGroup()
        Dim qry As String = "select Cust_Group_Code as [Customer Group Code],Cust_Group_Desc as [Description]from TSPL_CUSTOMER_GROUP_MASTER"
        cbgCustGroup.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgCustGroup.ValueMember = "Customer Group Code"
        cbgCustGroup.DisplayMember = "Customer Group Code"
    End Sub
    Sub LoadItem()
        ''Dim qry As String = "select distinct Item_Code as [Item Code],Scheme_Applicable as [Scheme Applicable] from TSPL_SALE_INVOICE_DETAIL"
        Dim qry As String = "select Item_Code as [Item Code],Item_Desc as [Item Description] from TSPL_ITEM_MASTER"
        cbgItem1.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgItem1.ValueMember = "Item Code"
        cbgItem1.DisplayMember = "Item Description"
    End Sub
    Private Sub chklocAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chklocAll.ToggleStateChanged
        cbglocation.Enabled = Not chklocAll.IsChecked
    End Sub
    Sub LoadCompany()
        Dim qry As String = "SELECT Comp_Code as [Company Code],Comp_Name as [Company Name],DataBase_Name from TSPL_COMPANY_MASTER where len(isnull(DataBase_Name,''))>0"
        Dim ArrHideColumn As New List(Of String)
        ArrHideColumn.Add("DataBase_Name")
        cbgCompany.ArrHideColumns = ArrHideColumn
        cbgCompany.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgCompany.ValueMember = "DataBase_Name"
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub
    Sub LoadCustomerClass()
        Dim qry As String = "select Cust_Type_Code as [Code],Cust_Type_Desc as [Name] from TSPL_CUSTOMER_type_master"
        chkCustomerClass.DataSource = clsDBFuncationality.GetDataTable(qry)
        chkCustomerClass.ValueMember = "Code"
        chkCustomerClass.DisplayMember = "Name"
    End Sub
    Function LoadClass() As DataTable

        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow = dt.NewRow()
        'dr("Code") = ""
        'dr("Name") = "Select"
        'dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "A"
        dr("Name") = "AGENCY"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "D"
        dr("Name") = "DIRECT ROUTE"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "F"
        dr("Name") = "FRANCHISEE"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "M"
        dr("Name") = "MODERN TRADE"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "P"
        dr("Name") = "PRE-SALE"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "S"
        dr("Name") = "SUPER DISTRIBUTOR"
        dt.Rows.Add(dr)


        Return dt
    End Function

    Private Sub btnreset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnreset.Click
        dtpstart.Value = clsCommon.GETSERVERDATE()
        dtpend.Value = clsCommon.GETSERVERDATE()
        dtpStarttime.Value = DateTime.Now
        dtpendtime.Value = DateTime.Now

        chklocAll.IsChecked = True
        chkCustAll.IsChecked = True
        chkClassAll.IsChecked = True
        chkItemAll1.IsChecked = True
        chkGroupAll.IsChecked = True
        LoadItem()
        LoadCustomer()
        LoadCustomerClass()
        Loadlocation()
        LoadCustomerGroup()
        LoadCompany()
        rbtnCompanyAll.IsChecked = True
        rdbItemwise.IsChecked = True
    End Sub

    Private Sub chkCustAll_ToggleStateChanged(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkCustAll.ToggleStateChanged
        cbgCustomer.Enabled = Not chkCustAll.IsChecked
    End Sub

    Private Sub chkClassAll_ToggleStateChanged(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkClassAll.ToggleStateChanged
        chkCustomerClass.Enabled = Not chkClassAll.IsChecked
    End Sub

    Private Sub rbtnCompanyAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtnCompanyAll.ToggleStateChanged, rbtnCompanySelect.ToggleStateChanged
        cbgCompany.Enabled = Not rbtnCompanyAll.IsChecked
    End Sub

    Private Sub chkItemAll1_ToggleStateChanged1(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkItemAll1.ToggleStateChanged
        cbgItem1.Enabled = Not chkItemAll1.IsChecked
    End Sub

    Private Sub chkGroupAll_ToggleStateChanged(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkGroupAll.ToggleStateChanged
        cbgCustGroup.Enabled = Not chkGroupAll.IsChecked
    End Sub

    Private Sub btnprint_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnprint.Click
       

    End Sub
    Sub print()
        If chklocSelect.IsChecked = True AndAlso cbglocation.CheckedValue.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please select at least one Location or select ALL")
            Return
        ElseIf chkChkSelect.IsChecked = True AndAlso cbgCustomer.CheckedValue.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please select at least one Customer or select ALL")
            Return
        ElseIf chkItemSelect1.IsChecked = True AndAlso cbgItem1.CheckedValue.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please select at least one Item or select ALL")
            Return
        ElseIf chkClassSelect.IsChecked = True AndAlso chkCustomerClass.CheckedValue.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please select at least one Customer Type or select ALL")
            Return
        ElseIf chkGroupSelect.IsChecked = True AndAlso cbgCustGroup.CheckedValue.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please select at least one Customer Group or select ALL")
            Return
        ElseIf rbtnCompanySelect.IsChecked = True AndAlso cbgCompany.CheckedValue.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please select at least one Company or select ALL")
            Return
        End If
        Dim strCustAll, strLocAll, strItemAll, strClassAll, strCustGroup As String
        Dim strTotCashDisc, strSql, strSql1, strSql2, strSql3, strSql4, Un1, Un2, Un3 As String

        If rdbItemwise.IsChecked = True Then
            strTotCashDisc = " " + clsCommon.ReplicateDBString + "TSPL_INV_PRICE_COM_DETAIL.Price_Comp2_Amt+ " & _
                " " + clsCommon.ReplicateDBString + "TSPL_INV_PRICE_COM_DETAIL.Price_Comp3_Amt+  " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Cust_Discount as Price_Comp4_Amt+ " & _
                " " + clsCommon.ReplicateDBString + "TSPL_INV_PRICE_COM_DETAIL.Price_Comp6_Amt+ " & _
                " " + clsCommon.ReplicateDBString + "TSPL_INV_PRICE_COM_DETAIL.Price_Comp7_Amt"
        Else
            strTotCashDisc = " " + clsCommon.ReplicateDBString + "TSPL_INV_PRICE_COM_DETAIL.Price_Comp2_Amt+ " & _
                " " + clsCommon.ReplicateDBString + "TSPL_INV_PRICE_COM_DETAIL.Price_Comp3_Amt +  " + clsCommon.ReplicateDBString + "TSPL_INV_PRICE_COM_DETAIL.Price_Comp6_Amt+ " & _
                " " + clsCommon.ReplicateDBString + "TSPL_INV_PRICE_COM_DETAIL.Price_Comp7_Amt"
        End If
        strSql1 = "SELECT  a.Sale_Invoice_No, " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,  " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Route_No, " & _
                " " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Vehicle_No, a.Item_Code as SkuGrp,a.Invoice_Qty/Conversion_Factor AS CashDisc_qty, " & _
                "convert(decimal(18,2)," & strTotCashDisc & ") as TotCashDisc,0 as TotTradeDisc,0 as TradeDisc, " & _
                "ISNULL( " + clsCommon.ReplicateDBString + "TSPL_INV_PRICE_COM_DETAIL.MRP, 0) - ISNULL( " + clsCommon.ReplicateDBString + "TSPL_INV_PRICE_COM_DETAIL.Price_Comp1_Amt, 0) AS Ret_price, " & _
                " " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Remarks,  " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Customer_Class, " & _
                " " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Type_Code,  " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Desc, " & _
                " " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Group_Code,  " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Desc, " & _
                " " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Location, " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Desc, " & _
                " " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Code,  " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Customer_Name, " & _
                "a.Unit_code,'" & dtpstart.Value & "' AS Fdate, '" & dtpend.Value & "' AS ToDate, " & _
                " " + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER.Sku_Seq as OrderColumn, " + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER.Item_Code as Group1 , " & _
                "TSPL_COMPANY_MASTER.Comp_Code,TSPL_COMPANY_MASTER.Comp_Name, 0 as Disttarget, " & _
                "0 as NonDisttarget,( " + clsCommon.ReplicateDBString + "TSPL_INV_PRICE_COM_DETAIL.Price_Comp4_Amt +  " + clsCommon.ReplicateDBString + "TSPL_INV_PRICE_COM_DETAIL.Price_Comp5_Amt) * a.Invoice_Qty/Conversion_Factor as DisMargin,Conversion_Factor as Conv  FROM " & _
                "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL AS a INNER JOIN " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD ON " & _
                "a.Sale_Invoice_No =  " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No INNER JOIN " & _
                " " + clsCommon.ReplicateDBString + "TSPL_INV_PRICE_COM_DETAIL ON a.Sale_Invoice_No =  " + clsCommon.ReplicateDBString + "TSPL_INV_PRICE_COM_DETAIL.Invoice_No AND " & _
                "a.Item_Code =  " + clsCommon.ReplicateDBString + "TSPL_INV_PRICE_COM_DETAIL.Item_Code AND " & _
                "a.MRP_Amt =  " + clsCommon.ReplicateDBString + "TSPL_INV_PRICE_COM_DETAIL.MRP AND a.Scheme_Item = TSPL_INV_PRICE_COM_DETAIL.Scheme_Item AND " & _
                "a.Promo_Scheme_Item =  " + clsCommon.ReplicateDBString + "TSPL_INV_PRICE_COM_DETAIL.Promo_Scheme_Item AND " & _
                "a.Sampling_Item =  " + clsCommon.ReplicateDBString + "TSPL_INV_PRICE_COM_DETAIL.Sampling_Item INNER JOIN " & _
                " " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER ON  " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Cust_Code =  " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Code INNER JOIN " & _
                " " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_GROUP_MASTER ON  " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Group_Code =  " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code INNER JOIN " & _
                " " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_TYPE_MASTER ON  " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Customer_Class =  " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Code INNER JOIN " & _
                " " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER ON  " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Location = TSPL_LOCATION_MASTER.Location_Code LEFT OUTER JOIN " & _
                " " + clsCommon.ReplicateDBString + "TSPL_Sampling_Master ON  " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Scheme_Sample_Code = TSPL_Sampling_Master.Sampling_Code INNER JOIN " & _
                " " + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER ON  " + clsCommon.ReplicateDBString + "TSPL_INV_PRICE_COM_DETAIL.Item_Code = TSPL_ITEM_MASTER.Item_Code INNER JOIN " & _
                " " + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS ON  " + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER.Item_Code =  " + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS.Item_Code INNER JOIN " & _
                " " + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS AS TSPL_ITEM_DETAILS_1 ON  " + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER.Item_Code = TSPL_ITEM_DETAILS_1.Item_Code INNER JOIN " & _
                " " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL ON a.Item_Code =  " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Item_Code AND a.Unit_code =  " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.UOM_Code inner join " & _
                " " + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER on  " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Comp_Code= " + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER.Comp_Code WHERE " & _
                "(a.Promo_Scheme_Item = 'N' and a.Sampling_Item = 'N' and a.Scheme_Item = 'N') and " & _
                "convert(date, " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103) >= convert(date, '" & dtpstart.Value & "',103) AND " & _
                "convert(date, " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103) <= convert(date, '" & dtpend.Value & "',103) and " & _
                " " + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS.Class_Name='size' and TSPL_ITEM_DETAILS_1.Class_Name='flavour'"

        Un1 = "Union All "

        strSql2 = " SELECT  a.Sale_Invoice_No, " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date, " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Route_No, " & _
             "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Vehicle_No, a.Item_Code as SkuGrp, " & _
             "0 AS CashDisc_qty,0 as TotCashDisc,(" + clsCommon.ReplicateDBString + "TSPL_INV_PRICE_COM_DETAIL.Price_Comp2_Amt+" + clsCommon.ReplicateDBString + "TSPL_INV_PRICE_COM_DETAIL.Price_Comp3_Amt+ " + clsCommon.ReplicateDBString + "TSPL_INV_PRICE_COM_DETAIL.Price_Comp4_Amt+ " & _
             "" + clsCommon.ReplicateDBString + "TSPL_INV_PRICE_COM_DETAIL.Price_Comp5_Amt+ " + clsCommon.ReplicateDBString + "TSPL_INV_PRICE_COM_DETAIL.Price_Comp6_Amt+ " & _
             "" + clsCommon.ReplicateDBString + "TSPL_INV_PRICE_COM_DETAIL.Price_Comp7_Amt) as TotTradeDisc,a.Invoice_Qty as TradeDisc, " & _
             "ISNULL(" + clsCommon.ReplicateDBString + "TSPL_INV_PRICE_COM_DETAIL.MRP, 0) - ISNULL(" + clsCommon.ReplicateDBString + "TSPL_INV_PRICE_COM_DETAIL.Price_Comp1_Amt, 0) AS Ret_price, " & _
             "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Remarks, " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Customer_Class," + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Type_Code, " & _
             "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Desc, " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Group_Code, " & _
             "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Desc, " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Location, " & _
             "" + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Desc, " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Code, " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Customer_Name, " & _
             "a.Unit_code,'" & dtpstart.Value & "' AS Fdate, '" & dtpend.Value & "' AS ToDate," + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER.Sku_Seq as OrderColumn, " & _
             "" + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER.Item_Code as Group1 ," + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER.Comp_Code," + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER.Comp_Name, " & _
             "0 as Disttarget, 0 as NonDisttarget,0 as DisMargin,Conversion_Factor as Conv FROM " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL AS a INNER JOIN " & _
             "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD ON a.Sale_Invoice_No = " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No INNER JOIN " & _
             "" + clsCommon.ReplicateDBString + "TSPL_INV_PRICE_COM_DETAIL ON a.Sale_Invoice_No = " + clsCommon.ReplicateDBString + "TSPL_INV_PRICE_COM_DETAIL.Invoice_No AND " & _
             "a.Item_Code = " + clsCommon.ReplicateDBString + "TSPL_INV_PRICE_COM_DETAIL.Item_Code AND a.MRP_Amt = TSPL_INV_PRICE_COM_DETAIL.MRP AND " & _
             "a.Scheme_Item = " + clsCommon.ReplicateDBString + "TSPL_INV_PRICE_COM_DETAIL.Scheme_Item AND " & _
             "a.Promo_Scheme_Item = " + clsCommon.ReplicateDBString + "TSPL_INV_PRICE_COM_DETAIL.Promo_Scheme_Item AND " & _
             "a.Sampling_Item = " + clsCommon.ReplicateDBString + "TSPL_INV_PRICE_COM_DETAIL.Sampling_Item INNER JOIN " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER ON " & _
             "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Cust_Code = " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Code INNER JOIN " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_GROUP_MASTER ON " & _
             "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Group_Code = " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code INNER JOIN " & _
             "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_TYPE_MASTER ON " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Customer_Class = " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Code INNER JOIN " & _
             "" + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER ON " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Location = " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Code LEFT OUTER JOIN " & _
             "" + clsCommon.ReplicateDBString + "TSPL_Sampling_Master ON " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Scheme_Sample_Code = " + clsCommon.ReplicateDBString + "TSPL_Sampling_Master.Sampling_Code INNER JOIN " & _
             "" + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER ON " + clsCommon.ReplicateDBString + "TSPL_INV_PRICE_COM_DETAIL.Item_Code = " + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER.Item_Code INNER JOIN " & _
             "" + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS ON " + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER.Item_Code =" + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS.Item_Code INNER JOIN " & _
             "" + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS AS TSPL_ITEM_DETAILS_1 ON " + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER.Item_Code = TSPL_ITEM_DETAILS_1.Item_Code INNER JOIN " & _
             "" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL ON a.Item_Code = " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Item_Code AND " & _
             "a.Unit_code =" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.UOM_Code inner join " + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER on " & _
             "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Comp_Code=" + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER.Comp_Code WHERE " & _
             "(a.Promo_Scheme_Item = 'Y' or a.Sampling_Item = 'Y' or a.Scheme_Item = 'Y') and " & _
             "convert(date," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103) >= convert(date, '" & dtpstart.Value & "',103) AND " & _
             "convert(date," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103) <= convert(date, '" & dtpend.Value & "',103) and " & _
             "" + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS.Class_Name='size' and TSPL_ITEM_DETAILS_1.Class_Name='flavour'"

        Un2 = "Union All "

        strSql3 = "SELECT " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No, " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date, " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Route_No, " & _
                      "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Vehicle_No, '' AS SkuGrp, 0 AS CashDisc_qty, 0 AS TotCashDisc, 0 AS TotTradeDisc, 0 AS TradeDisc, 0 AS Ret_price, " & _
                      "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Remarks, " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Customer_Class, " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Type_Code, " & _
                      "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Desc, " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Group_Code, " & _
                      "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Desc, " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Location, " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Desc, " & _
                      "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Code, " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Customer_Name, '' AS Unit_code, '" & dtpstart.Value & "' AS Fdate, " & _
                      "'" & dtpend.Value & "' AS ToDate, 0 AS OrderColumn, '' AS Group1, TSPL_COMPANY_MASTER.Comp_Code, " & _
                      "" + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER.Comp_Name," + clsCommon.ReplicateDBString + " TSPL_Receipt_Adjustment_Detail.Amount as Disttarget,0 as  NonDisttarget,0 as DisMargin,0 as Conv " & _
                      "FROM " + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER INNER JOIN " & _
                      "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD INNER JOIN " & _
                      "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER ON " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Cust_Code = " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Code INNER JOIN " & _
                      "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_GROUP_MASTER ON " & _
                      "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Group_Code = " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code INNER JOIN " & _
                      "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_TYPE_MASTER ON " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Customer_Class = " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Code INNER JOIN " & _
                      "" + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER ON " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Location = " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Code ON " & _
                      "" + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER.Comp_Code = " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Comp_Code INNER JOIN " & _
                      "" + clsCommon.ReplicateDBString + "TSPL_Receipt_Adjustment_Header ON " + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER.Comp_Code = " + clsCommon.ReplicateDBString + "TSPL_Receipt_Adjustment_Header.Comp_Code AND " & _
                      "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No = " + clsCommon.ReplicateDBString + "TSPL_Receipt_Adjustment_Header.Doc_No INNER JOIN " & _
                      "" + clsCommon.ReplicateDBString + "TSPL_Receipt_Adjustment_Detail ON " & _
                      "" + clsCommon.ReplicateDBString + "TSPL_Receipt_Adjustment_Header.Adjustment_No = TSPL_Receipt_Adjustment_Detail.Adjustment_No INNER JOIN " & _
                      "" + clsCommon.ReplicateDBString + "TSPL_Discount_Master ON " + clsCommon.ReplicateDBString + "TSPL_Receipt_Adjustment_Detail.Discount_Code = " + clsCommon.ReplicateDBString + "TSPL_Discount_Master.Code WHERE  " + clsCommon.ReplicateDBString + " TSPL_Discount_Master.Target_Based='Y' and " & _
                     "convert(date," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103) >= convert(date, '" & dtpstart.Value & "',103) AND " & _
                     "convert(date," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103) <= convert(date, '" & dtpend.Value & "',103)"

        Un3 = "Union All "

        strSql4 = "SELECT " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No, " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date, " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Route_No, " & _
                      "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Vehicle_No, '' AS SkuGrp, 0 AS CashDisc_qty, 0 AS TotCashDisc, 0 AS TotTradeDisc, 0 AS TradeDisc, 0 AS Ret_price, " & _
                      "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Remarks, " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Customer_Class, " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Type_Code, " & _
                      "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Desc, " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Group_Code, " & _
                      "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Desc, " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Location, " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Desc, " & _
                      "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Code, " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Customer_Name, '' AS Unit_code, '" & dtpstart.Value & "' AS Fdate, " & _
                      "'" & dtpend.Value & "' AS ToDate, 0 AS OrderColumn, '' AS Group1, TSPL_COMPANY_MASTER.Comp_Code, " & _
                      "" + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER.Comp_Name,0 as Disttarget," + clsCommon.ReplicateDBString + " TSPL_Receipt_Adjustment_Detail.Amount as  NonDisttarget,0 as DisMargin,0 as Conv " & _
                      "FROM " + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER INNER JOIN " & _
                      "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD INNER JOIN " & _
                      "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER ON " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Cust_Code = " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Code INNER JOIN " & _
                      "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_GROUP_MASTER ON " & _
                      "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Group_Code = " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code INNER JOIN " & _
                      "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_TYPE_MASTER ON " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Customer_Class = " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Code INNER JOIN " & _
                      "" + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER ON " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Location = " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Code ON " & _
                      "" + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER.Comp_Code = " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Comp_Code INNER JOIN " & _
                      "" + clsCommon.ReplicateDBString + "TSPL_Receipt_Adjustment_Header ON " + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER.Comp_Code = " + clsCommon.ReplicateDBString + "TSPL_Receipt_Adjustment_Header.Comp_Code AND " & _
                      "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No = " + clsCommon.ReplicateDBString + "TSPL_Receipt_Adjustment_Header.Doc_No INNER JOIN " & _
                      "" + clsCommon.ReplicateDBString + "TSPL_Receipt_Adjustment_Detail ON " & _
                      "" + clsCommon.ReplicateDBString + "TSPL_Receipt_Adjustment_Header.Adjustment_No = TSPL_Receipt_Adjustment_Detail.Adjustment_No INNER JOIN " & _
                      "" + clsCommon.ReplicateDBString + "TSPL_Discount_Master ON " + clsCommon.ReplicateDBString + "TSPL_Receipt_Adjustment_Detail.Discount_Code = " + clsCommon.ReplicateDBString + "TSPL_Discount_Master.Code WHERE  " + clsCommon.ReplicateDBString + " TSPL_Discount_Master.Target_Based='N' and " & _
                     "convert(date," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103) >= convert(date, '" & dtpstart.Value & "',103) AND " & _
                     "convert(date," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103) <= convert(date, '" & dtpend.Value & "',103)"
        If chklocAll.IsChecked = True Then
            strLocAll = "Y"
        Else
            strLocAll = "N"
        End If
        If chkItemAll1.IsChecked = True Then
            strItemAll = "Y"
        Else
            strItemAll = "N"
        End If
        If chkClassAll.IsChecked = True Then
            strClassAll = "Y"
        Else
            strClassAll = "N"
        End If
        If chkCustAll.IsChecked = True Then
            strCustAll = "Y"
        Else
            strCustAll = "N"
        End If

        If chkGroupAll.IsChecked = True Then
            strCustGroup = "Y"
        Else
            strCustGroup = "N"
        End If




        If strLocAll = "N" Then
            strSql1 += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_head.Location in (" + clsCommon.GetMulcallString(cbglocation.CheckedValue) + ") "
            strSql2 += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_head.Location in (" + clsCommon.GetMulcallString(cbglocation.CheckedValue) + ") "
            strSql3 += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_head.Location in (" + clsCommon.GetMulcallString(cbglocation.CheckedValue) + ") "
            strSql4 += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_head.Location in (" + clsCommon.GetMulcallString(cbglocation.CheckedValue) + ") "
        End If
        If strCustAll = "N" Then
            strSql1 += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_head.Cust_Code in (" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ") "
            strSql2 += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_head.Cust_Code in (" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ") "
            strSql3 += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_head.Cust_Code in (" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ") "
            strSql4 += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_head.Cust_Code in (" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ") "
        End If
        If strClassAll = "N" Then
            strSql1 += " and " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Customer_Class in (" + clsCommon.GetMulcallString(chkCustomerClass.CheckedValue) + ") "
            strSql2 += " and " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Customer_Class in (" + clsCommon.GetMulcallString(chkCustomerClass.CheckedValue) + ") "
            strSql3 += " and " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Customer_Class in (" + clsCommon.GetMulcallString(chkCustomerClass.CheckedValue) + ") "
            strSql4 += " and " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Customer_Class in (" + clsCommon.GetMulcallString(chkCustomerClass.CheckedValue) + ") "
        End If
        'If strItemAll = "N" Then
        '    strSql1 += " and " + clsCommon.ReplicateDBString + "TSPL_INV_PRICE_COM_DETAIL.Item_Code in (" + clsCommon.GetMulcallString(cbgItem1.CheckedValue) + ") "
        '    strSql2 += " and " + clsCommon.ReplicateDBString + "TSPL_INV_PRICE_COM_DETAIL.Item_Code in (" + clsCommon.GetMulcallString(cbgItem1.CheckedValue) + ") "
        '    strSql3 += " and " + clsCommon.ReplicateDBString + "TSPL_INV_PRICE_COM_DETAIL.Item_Code in (" + clsCommon.GetMulcallString(cbgItem1.CheckedValue) + ") "
        '    strSql4 += " and " + clsCommon.ReplicateDBString + "TSPL_INV_PRICE_COM_DETAIL.Item_Code in (" + clsCommon.GetMulcallString(cbgItem1.CheckedValue) + ") "
        'End If
        If strCustGroup = "N" Then
            strSql1 += " and " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Group_Code in (" + clsCommon.GetMulcallString(cbgCustGroup.CheckedValue) + ") "
            strSql2 += " and " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Group_Code in (" + clsCommon.GetMulcallString(cbgCustGroup.CheckedValue) + ") "
            strSql3 += " and " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Group_Code in (" + clsCommon.GetMulcallString(cbgCustGroup.CheckedValue) + ") "
            strSql4 += " and " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Group_Code in (" + clsCommon.GetMulcallString(cbgCustGroup.CheckedValue) + ") "
        End If

        Dim ArrDBName As ArrayList = Nothing
        If rbtnCompanyAll.IsChecked Then
            ArrDBName = cbgCompany.AllValue
        Else
            ArrDBName = cbgCompany.CheckedValue
        End If



        If rdbItemwise.IsChecked = True Then
            strSql = strSql1 & Un1 & strSql2
            strQuery = clsCommon.GetQueryWithAllSelectedDataBase(strSql, ArrDBName, False)
            frmCrystalReportViewer.funreport(CrystalReportFolder.SalesReport, clsDBFuncationality.GetDataTable(strQuery), "crptOverallDiscountItemwise", "OverAll Discount report Item wise")
        Else
            strSql = strSql1 & Un1 & strSql2 & Un2 & strSql3 & Un3 & strSql4
            strQuery = clsCommon.GetQueryWithAllSelectedDataBase(strSql, ArrDBName, False)
            frmCrystalReportViewer.funreport(CrystalReportFolder.SalesReport, clsDBFuncationality.GetDataTable(strQuery), "crptOverallDiscountInvoicewise", "OverAll Discount report Invoice wise")
        End If
    End Sub
    Function funTempTableCreation() As Boolean
        Dim dt As New DataTable
        Dim strQry, Code, Desc, DescAmount As String

        strQry = "if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[TSPL_TEMP_DISCOUNT]') " & _
                  " and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [dbo].[TSPL_TEMP_DISCOUNT]"
        clsDBFuncationality.ExecuteNonQuery(strQry)

        strQry = "CREATE TABLE TSPL_TEMP_DISCOUNT (Item_Code VARCHAR(50),MRP_Bottle decimal(18,2) DEFAULT 0 , " & _
        "MRP_case decimal(18,2) DEFAULT 0 ,TP decimal(18,2) DEFAULT 0 ,BS_Scheme decimal(18,2) DEFAULT 0 , " & _
        "Sale decimal(18,2) DEFAULT 0 ,Trade_Disc_Amt decimal(18,2) DEFAULT 0 ,Cash_Disc_qty decimal(18,2) DEFAULT 0 , " & _
        "Cash_Disc_Amt decimal(18,2) DEFAULT 0 ,Acct_qty decimal(18,2) DEFAULT 0 ,Acct_Amt decimal(18,2) DEFAULT 0 ) "
        clsDBFuncationality.ExecuteNonQuery(strQry)

        ''  To create  discount column
        Dim coll As New Dictionary(Of String, String)()
        strQry = "select distinct Discount_Code,[Description] as Descr from TSPL_SALE_INVOICE_DETAIL inner join TSPL_Discount_Master on TSPL_SALE_INVOICE_DETAIL.Discount_Code=TSPL_Discount_Master.Code where Discount_Code <> '' or Discount_Code=null"
        dt = clsDBFuncationality.GetDataTable(strQry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            For Each dr As DataRow In dt.Rows
                Code = clsCommon.myCstr(dr("Discount_Code"))
                Desc = clsCommon.myCstr(dr("Descr"))
                '' ''Desc = Desc.Replace(" ", "")
                Desc = Replace(Desc, " ", "_")
                DescAmount = Desc + "_Amount"
                '' '' ''coll.Add("" & Desc & "", "varchar(50) Null")
                coll.Add("" & Desc & "", "Decimal(18,2) default 0")
                coll.Add("" & DescAmount & "", "Decimal(18,2) default 0")
                clsCommonFunctionality.CreateOrAlterTable("TSPL_TEMP_DISCOUNT", coll)
            Next
        End If


        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()

        Dim strItemCode, strInvoiceNo, strSchemeCode, DiscCode, DiscDesc, strItemCodeFOC As String
        Dim DecMRPBottle, DecMRPCase, ConvRate, DecTP, decSaleQty, decFOCamt, oldFOCamt As Decimal
        Dim DecCustDisc, DecCustDiscAmt, DecAcctQty, DecAcctAmt, DecDiscQty, DecDiscAmt As Decimal

        ''  To delete existing record


        'strQry = "Delete from TSPL_TEMP_DISCOUNT"
        'clsDBFuncationality.ExecuteNonQuery(strQry, trans)

        ''  To insert Item code,MRPcase,MRPbottle
        'strQry = "Select Distinct TSPL_SALE_INVOICE_DETAIL.Item_code,MRP_Amt * Conversion_Factor as MRPCase from TSPL_SALE_INVOICE_DETAIL inner join " & _
        '"TSPL_ITEM_UOM_DETAIL on TSPL_SALE_INVOICE_DETAIL.item_code=tspl_item_uom_detail.item_code and TSPL_SALE_INVOICE_DETAIL.Unit_code=TSPL_ITEM_UOM_DETAIL.UOM_Code WHERE (Scheme_Applicable='Y' or  (Scheme_Code_Qty <> '' and Scheme_Item='N'))"

        strQry = "Select Distinct TSPL_SALE_INVOICE_DETAIL.Item_code,MRP_Amt * Conversion_Factor as MRPCase,((MRP_Amt * Conversion_Factor) -Price_Amount1) as TP from TSPL_SALE_INVOICE_DETAIL inner join " & _
       "TSPL_ITEM_UOM_DETAIL on TSPL_SALE_INVOICE_DETAIL.item_code=tspl_item_uom_detail.item_code and TSPL_SALE_INVOICE_DETAIL.Unit_code=TSPL_ITEM_UOM_DETAIL.UOM_Code "
        dt = clsDBFuncationality.GetDataTable(strQry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            For Each dr As DataRow In dt.Rows
                strItemCode = clsCommon.myCstr(dr("Item_code"))
                DecMRPCase = clsCommon.myCdbl(dr("MRPCase"))
                DecTP = clsCommon.myCdbl(dr("TP"))

                strQry = " Select Conversion_Factor FROM TSPL_ITEM_UOM_DETAIL WHERE " & _
                "Item_Code = '" & strItemCode & "' AND UOM_Code = 'fb'"
                dt = clsDBFuncationality.GetDataTable(strQry, trans)
                If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                    ConvRate = clsCommon.myCdbl(dt.Rows(0)("Conversion_Factor"))
                End If
                DecMRPBottle = DecMRPCase / ConvRate
                strQry = "Insert into TSPL_TEMP_DISCOUNT (Item_Code,MRP_Bottle,MRP_case,TP ) values ('" & strItemCode & "'," & DecMRPBottle & "," & DecMRPCase & "," & DecTP & ")"
                clsDBFuncationality.ExecuteNonQuery(strQry, trans)
            Next
        End If

        ''  To insert TP or Retailer price
        'strQry = "Select ((MRP_Amt * Conversion_Factor) -Price_Amount1) as Price_Amount1,TSPL_SALE_INVOICE_DETAIL.Item_code,MRP_Amt * Conversion_Factor as MRPCase  from TSPL_SALE_INVOICE_DETAIL " & _
        '"inner join TSPL_ITEM_UOM_DETAIL on TSPL_SALE_INVOICE_DETAIL.item_code=tspl_item_uom_detail.item_code " & _
        '"and TSPL_SALE_INVOICE_DETAIL.Unit_code=TSPL_ITEM_UOM_DETAIL.UOM_Code where (Scheme_Applicable='Y' or  (Scheme_Code_Qty <> '' and Scheme_Item='N'))"

        'strQry = "Select distinct  ((MRP_Amt * Conversion_Factor) -Price_Amount1) as Price_Amount1,MRP_Amt * Conversion_Factor as MRPCase  from TSPL_SALE_INVOICE_DETAIL inner join TSPL_ITEM_UOM_DETAIL on TSPL_SALE_INVOICE_DETAIL.item_code=tspl_item_uom_detail.item_code and TSPL_SALE_INVOICE_DETAIL.Unit_code=TSPL_ITEM_UOM_DETAIL.UOM_Code " & _
        '         "where TSPL_SALE_INVOICE_DETAIL.Item_Code='7N300bfc' and MRP_Amt=MRP_Amt * Conversion_Factor"
        'dt = clsDBFuncationality.GetDataTable(strQry, trans)
        'If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
        '    For Each dr As DataRow In dt.Rows
        '        DecTP = 0
        '        strItemCode = clsCommon.myCstr(dr("Item_code"))
        '        DecTP = clsCommon.myCdbl(dr("Price_Amount1"))
        '        DecMRPCase = clsCommon.myCdbl(dr("MRPCase"))

        '        strQry = "Update TSPL_TEMP_DISCOUNT  set TP='" & DecTP & "' where Item_Code='" & strItemCode & "' and MRP_case=" & DecMRPCase & ""
        '        clsDBFuncationality.ExecuteNonQuery(strQry, trans)
        '    Next
        'End If


        ''  To insert Main Item qty
        strQry = "Select ((MRP_Amt * Conversion_Factor) -Price_Amount1) as TP,TSPL_SALE_INVOICE_DETAIL.Item_code,MRP_Amt * Conversion_Factor as MRPCase,sum(Invoice_Qty) as Invoice_Qty  from TSPL_SALE_INVOICE_DETAIL " & _
        "inner join TSPL_ITEM_UOM_DETAIL on TSPL_SALE_INVOICE_DETAIL.item_code=tspl_item_uom_detail.item_code " & _
        "and TSPL_SALE_INVOICE_DETAIL.Unit_code=TSPL_ITEM_UOM_DETAIL.UOM_Code where (Scheme_Applicable='Y' or  (Scheme_Code_Qty <> '' and Scheme_Item='N'))  " & _
        "and (Discount_Code = '' or Discount_Code=null) group by TSPL_SALE_INVOICE_DETAIL.Item_Code,((MRP_Amt * Conversion_Factor) -Price_Amount1) ,(MRP_Amt * Conversion_Factor) "
        dt = clsDBFuncationality.GetDataTable(strQry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            For Each dr As DataRow In dt.Rows
                DecTP = 0
                strItemCode = clsCommon.myCstr(dr("Item_code"))
                DecTP = clsCommon.myCdbl(dr("TP"))
                DecMRPCase = clsCommon.myCdbl(dr("MRPCase"))
                decSaleQty = clsCommon.myCdbl(dr("Invoice_Qty"))

                strQry = "Update TSPL_TEMP_DISCOUNT  set Sale='" & decSaleQty & "' where Item_Code='" & strItemCode & "' and MRP_case=" & DecMRPCase & " and TP=" & DecTP & ""
                clsDBFuncationality.ExecuteNonQuery(strQry, trans)
            Next
        End If


        ''  To insert FOC amount
        oldFOCamt = 0
        strQry = "Select Sale_Invoice_No,TSPL_SALE_INVOICE_DETAIL.Item_Code,MRP_Amt * Conversion_Factor as MRPCase,Scheme_Code_Qty,((MRP_Amt * Conversion_Factor) -Price_Amount1) as TP  from TSPL_SALE_INVOICE_DETAIL inner join TSPL_ITEM_UOM_DETAIL on TSPL_SALE_INVOICE_DETAIL.item_code=tspl_item_uom_detail.item_code and TSPL_SALE_INVOICE_DETAIL.Unit_code=TSPL_ITEM_UOM_DETAIL.UOM_Code where (Scheme_Applicable='Y' or  (Scheme_Code_Qty <> '' and Scheme_Item='N'))  and (Discount_Code = '' or Discount_Code=null)"
        dt = clsDBFuncationality.GetDataTable(strQry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            For Each dr As DataRow In dt.Rows
                strInvoiceNo = clsCommon.myCstr(dr("Sale_Invoice_No"))
                strItemCode = clsCommon.myCstr(dr("Item_code"))
                DecMRPCase = clsCommon.myCdbl(dr("MRPCase"))
                strSchemeCode = clsCommon.myCstr(dr("Scheme_Code_Qty"))
                DecTP = clsCommon.myCdbl(dr("TP"))

                If strSchemeCode <> "MS1" Then
                    strQry = "Select (Invoice_Qty * Basic_Rate) as FOCAMt,TSPL_SALE_INVOICE_DETAIL.Item_Code  from TSPL_SALE_INVOICE_DETAIL inner join TSPL_ITEM_UOM_DETAIL on TSPL_SALE_INVOICE_DETAIL.item_code=tspl_item_uom_detail.item_code and TSPL_SALE_INVOICE_DETAIL.Unit_code=TSPL_ITEM_UOM_DETAIL.UOM_Code " & _
                    "where Scheme_Code_Qty='" & strSchemeCode & "' and Scheme_Item='Y' and Sale_Invoice_No='" & strInvoiceNo & "' and (Discount_Code = '' or Discount_Code=null)"

                    dt = clsDBFuncationality.GetDataTable(strQry, trans)
                    If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                        strItemCodeFOC = clsCommon.myCstr(dr("Item_Code"))
                        decFOCamt = clsCommon.myCdbl(dt.Rows(0)("FOCAMt"))
                    End If

                    strQry = "Select Trade_Disc_Amt  from TSPL_TEMP_DISCOUNT where Item_Code='" & strItemCode & "' and MRP_case=" & DecMRPCase & " and TP=" & DecTP & ""
                    dt = clsDBFuncationality.GetDataTable(strQry, trans)
                    If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                        oldFOCamt = clsCommon.myCdbl(dt.Rows(0)("Trade_Disc_Amt"))
                        decFOCamt = decFOCamt + oldFOCamt

                        strQry = "Update TSPL_TEMP_DISCOUNT  set Trade_Disc_Amt='" & decFOCamt & "' where Item_Code='" & strItemCode & "' and MRP_case=" & DecMRPCase & " and TP=" & DecTP & ""
                        clsDBFuncationality.ExecuteNonQuery(strQry, trans)
                    End If

                Else

                    strQry = "Select sum(Invoice_Qty * Basic_Rate) as FOCamt  from TSPL_SALE_INVOICE_DETAIL inner join TSPL_ITEM_UOM_DETAIL on TSPL_SALE_INVOICE_DETAIL.item_code=tspl_item_uom_detail.item_code and TSPL_SALE_INVOICE_DETAIL.Unit_code=TSPL_ITEM_UOM_DETAIL.UOM_Code " & _
                    " where Main_Item='" & strItemCode & "'  and Scheme_Item='Y' and Sale_Invoice_No='" & strInvoiceNo & "' and (Discount_Code = '' or Discount_Code=null)"
                    dt = clsDBFuncationality.GetDataTable(strQry, trans)
                    If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                        decFOCamt = clsCommon.myCdbl(dt.Rows(0)("FOCAMt"))
                    End If

                    strQry = "Select Trade_Disc_Amt  from TSPL_TEMP_DISCOUNT where Item_Code='" & strItemCode & "' and MRP_case=" & DecMRPCase & " and TP=" & DecTP & ""
                    dt = clsDBFuncationality.GetDataTable(strQry, trans)
                    If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                        oldFOCamt = clsCommon.myCdbl(dt.Rows(0)("Trade_Disc_Amt"))

                        decFOCamt = decFOCamt + oldFOCamt

                        strQry = "Update TSPL_TEMP_DISCOUNT  set Trade_Disc_Amt='" & decFOCamt & "' where Item_Code='" & strItemCode & "' and MRP_case=" & DecMRPCase & " and TP=" & DecTP & ""
                        clsDBFuncationality.ExecuteNonQuery(strQry, trans)
                    End If

                End If
            Next
        End If

        ''  To insert Cust Discount qty
        strQry = "select TSPL_SALE_INVOICE_DETAIL.Item_Code,(MRP_Amt * Conversion_Factor) as MRPCase ,((MRP_Amt * Conversion_Factor) -Price_Amount1) as TP,sum(Invoice_Qty) as Cust_DiscQty from TSPL_SALE_INVOICE_DETAIL inner join TSPL_ITEM_UOM_DETAIL on TSPL_SALE_INVOICE_DETAIL.item_code=tspl_item_uom_detail.item_code and TSPL_SALE_INVOICE_DETAIL.Unit_code=TSPL_ITEM_UOM_DETAIL.UOM_Code " & _
        "where Cust_Discount <> 0 group by TSPL_SALE_INVOICE_DETAIL.Item_Code,MRP_Amt * Conversion_Factor,((MRP_Amt * Conversion_Factor) -Price_Amount1)"
        dt = clsDBFuncationality.GetDataTable(strQry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            For Each dr As DataRow In dt.Rows
                strItemCode = clsCommon.myCstr(dr("Item_code"))
                DecTP = clsCommon.myCdbl(dr("TP"))
                DecMRPCase = clsCommon.myCdbl(dr("MRPCase"))
                DecCustDisc = clsCommon.myCdbl(dr("Cust_DiscQty"))

                strQry = "Update TSPL_TEMP_DISCOUNT  set Cash_Disc_qty='" & DecCustDisc & "' where Item_Code='" & strItemCode & "' and MRP_case=" & DecMRPCase & " and TP=" & DecTP & ""
                clsDBFuncationality.ExecuteNonQuery(strQry, trans)
            Next
        End If

        ''  To insert Cust Discount Amount
        strQry = "select TSPL_SALE_INVOICE_DETAIL.Item_Code,(MRP_Amt * Conversion_Factor) as MRPCase,((MRP_Amt * Conversion_Factor) -Price_Amount1) as TP,sum(Invoice_Qty * Cust_Discount) as Cust_DiscAmt from TSPL_SALE_INVOICE_DETAIL inner join TSPL_ITEM_UOM_DETAIL on TSPL_SALE_INVOICE_DETAIL.item_code=tspl_item_uom_detail.item_code and TSPL_SALE_INVOICE_DETAIL.Unit_code=TSPL_ITEM_UOM_DETAIL.UOM_Code " & _
        "where Cust_Discount <> 0 group by TSPL_SALE_INVOICE_DETAIL.Item_Code,MRP_Amt * Conversion_Factor,((MRP_Amt * Conversion_Factor) -Price_Amount1) "
        dt = clsDBFuncationality.GetDataTable(strQry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            For Each dr As DataRow In dt.Rows
                strItemCode = clsCommon.myCstr(dr("Item_code"))
                DecTP = clsCommon.myCdbl(dr("TP"))
                DecMRPCase = clsCommon.myCdbl(dr("MRPCase"))
                DecCustDiscAmt = clsCommon.myCdbl(dr("Cust_DiscAmt"))

                strQry = "Update TSPL_TEMP_DISCOUNT  set Cash_Disc_Amt='" & DecCustDiscAmt & "' where Item_Code='" & strItemCode & "' and MRP_case=" & DecMRPCase & " and TP=" & DecTP & ""
                clsDBFuncationality.ExecuteNonQuery(strQry, trans)
            Next
        End If

        ''  To insert Account MT Qty
        strQry = "select TSPL_SALE_INVOICE_DETAIL.Item_Code,(MRP_Amt * Conversion_Factor) as MRPCase,((MRP_Amt * Conversion_Factor) -Price_Amount1) as TP," & _
               "sum(isnull(Price_Amount2,0) + isnull(Price_Amount3,0) + isnull(Price_Amount6,0) + isnull(Price_Amount7,0)) as AcctQty from TSPL_SALE_INVOICE_DETAIL inner join TSPL_ITEM_UOM_DETAIL on TSPL_SALE_INVOICE_DETAIL.item_code=tspl_item_uom_detail.item_code and TSPL_SALE_INVOICE_DETAIL.Unit_code=TSPL_ITEM_UOM_DETAIL.UOM_Code " & _
               " where Cust_Discount = 0 and Price_Amount4=0 and Price_Amount5=0 group by TSPL_SALE_INVOICE_DETAIL.Item_Code,MRP_Amt * Conversion_Factor,((MRP_Amt * Conversion_Factor) -Price_Amount1)"
        dt = clsDBFuncationality.GetDataTable(strQry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            For Each dr As DataRow In dt.Rows
                strItemCode = clsCommon.myCstr(dr("Item_code"))
                DecTP = clsCommon.myCdbl(dr("TP"))
                DecMRPCase = clsCommon.myCdbl(dr("MRPCase"))
                DecAcctQty = clsCommon.myCdbl(dr("AcctQty"))

                strQry = "Update TSPL_TEMP_DISCOUNT  set Acct_qty='" & DecAcctQty & "' where Item_Code='" & strItemCode & "' and MRP_case=" & DecMRPCase & " and TP=" & DecTP & ""
                clsDBFuncationality.ExecuteNonQuery(strQry, trans)
            Next
        End If


        ''  To insert Account MT Amount
        strQry = "select TSPL_SALE_INVOICE_DETAIL.Item_Code,(MRP_Amt * Conversion_Factor) as MRPCase,((MRP_Amt * Conversion_Factor) -Price_Amount1) as TP," & _
               "sum((isnull(Price_Amount2,0) + isnull(Price_Amount3,0) + isnull(Price_Amount6,0) + isnull(Price_Amount7,0)) * Basic_Rate) as Acctamt from TSPL_SALE_INVOICE_DETAIL inner join TSPL_ITEM_UOM_DETAIL on TSPL_SALE_INVOICE_DETAIL.item_code=tspl_item_uom_detail.item_code and TSPL_SALE_INVOICE_DETAIL.Unit_code=TSPL_ITEM_UOM_DETAIL.UOM_Code " & _
               " where Cust_Discount = 0 and Price_Amount4=0 and Price_Amount5=0 group by TSPL_SALE_INVOICE_DETAIL.Item_Code,MRP_Amt * Conversion_Factor,((MRP_Amt * Conversion_Factor) -Price_Amount1)"
        dt = clsDBFuncationality.GetDataTable(strQry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            For Each dr As DataRow In dt.Rows
                strItemCode = clsCommon.myCstr(dr("Item_code"))
                DecTP = clsCommon.myCdbl(dr("TP"))
                DecMRPCase = clsCommon.myCdbl(dr("MRPCase"))
                DecAcctAmt = clsCommon.myCdbl(dr("Acctamt"))

                strQry = "Update TSPL_TEMP_DISCOUNT  set Acct_Amt='" & DecAcctAmt & "' where Item_Code='" & strItemCode & "' and MRP_case=" & DecMRPCase & " and TP=" & DecTP & ""
                clsDBFuncationality.ExecuteNonQuery(strQry, trans)
            Next
        End If

        ''  To insert Discount Qty
        strQry = "select Discount_Code,Description,TSPL_SALE_INVOICE_DETAIL.Item_Code,(MRP_Amt * Conversion_Factor) as MRPCase,((MRP_Amt * Conversion_Factor) -Price_Amount1) as TP," & _
               "sum(Invoice_Qty ) AS DiscQty from TSPL_SALE_INVOICE_DETAIL inner join TSPL_ITEM_UOM_DETAIL on TSPL_SALE_INVOICE_DETAIL.item_code=tspl_item_uom_detail.item_code and TSPL_SALE_INVOICE_DETAIL.Unit_code=TSPL_ITEM_UOM_DETAIL.UOM_Code inner join TSPL_Discount_Master on TSPL_SALE_INVOICE_DETAIL.Discount_Code=TSPL_Discount_Master.Code " & _
               "where Discount_Code <> '' or  Discount_Code <> NULL  group by Discount_Code,Description,TSPL_SALE_INVOICE_DETAIL.Item_Code,(MRP_Amt * Conversion_Factor) ,((MRP_Amt * Conversion_Factor) -Price_Amount1)  "
        dt = clsDBFuncationality.GetDataTable(strQry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            For Each dr As DataRow In dt.Rows
                DiscCode = clsCommon.myCstr(dr("Discount_Code"))
                DiscDesc = clsCommon.myCstr(dr("Description"))
                strItemCode = clsCommon.myCstr(dr("Item_code"))
                DecTP = clsCommon.myCdbl(dr("TP"))
                DecMRPCase = clsCommon.myCdbl(dr("MRPCase"))
                DecDiscQty = clsCommon.myCdbl(dr("DiscQty"))

                Desc = Replace(DiscDesc, " ", "_")
                strQry = "Update TSPL_TEMP_DISCOUNT  set " & Desc & " ='" & DecDiscQty & "' where Item_Code='" & strItemCode & "' and MRP_case=" & DecMRPCase & " and TP=" & DecTP & ""
                clsDBFuncationality.ExecuteNonQuery(strQry, trans)
            Next
        End If

        ''  To insert Discount Qty
        strQry = "select Discount_Code,Description,TSPL_SALE_INVOICE_DETAIL.Item_Code,(MRP_Amt * Conversion_Factor) as MRPCase,((MRP_Amt * Conversion_Factor) -Price_Amount1) as TP," & _
               "sum(Invoice_Qty * Basic_rate ) AS DiscAmt from TSPL_SALE_INVOICE_DETAIL inner join TSPL_ITEM_UOM_DETAIL on TSPL_SALE_INVOICE_DETAIL.item_code=tspl_item_uom_detail.item_code and TSPL_SALE_INVOICE_DETAIL.Unit_code=TSPL_ITEM_UOM_DETAIL.UOM_Code inner join TSPL_Discount_Master on TSPL_SALE_INVOICE_DETAIL.Discount_Code=TSPL_Discount_Master.Code " & _
               "where Discount_Code <> '' or  Discount_Code <> NULL  group by Discount_Code,Description,TSPL_SALE_INVOICE_DETAIL.Item_Code,(MRP_Amt * Conversion_Factor) ,((MRP_Amt * Conversion_Factor) -Price_Amount1)  "
        dt = clsDBFuncationality.GetDataTable(strQry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            For Each dr As DataRow In dt.Rows
                DiscCode = clsCommon.myCstr(dr("Discount_Code"))
                DiscDesc = clsCommon.myCstr(dr("Description"))
                strItemCode = clsCommon.myCstr(dr("Item_code"))
                DecTP = clsCommon.myCdbl(dr("TP"))
                DecMRPCase = clsCommon.myCdbl(dr("MRPCase"))
                DecDiscAmt = clsCommon.myCdbl(dr("DiscAmt"))

                Desc = Replace(DiscDesc, " ", "_")
                Desc = Desc + "_Amount"
                strQry = "Update TSPL_TEMP_DISCOUNT  set " & Desc & " ='" & DecDiscAmt & "' where Item_Code='" & strItemCode & "' and MRP_case=" & DecMRPCase & " and TP=" & DecTP & ""
                clsDBFuncationality.ExecuteNonQuery(strQry, trans)
            Next
        End If
        trans.Commit()
        MsgBox("successfully")
        Return True
    End Function

    Private Sub RadButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadButton1.Click
        funTempTableCreation()
        Dim StrBase As String = "select COLUMN_NAME ,'['+REPLACE(COLUMN_NAME,'_',' ')+']'  from INFORMATION_SCHEMA.COLUMNS  where table_name='TSPL_TEMP_DISCOUNT'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(StrBase)
        Dim strCollection As String = ""
        Dim str As String = ""
        For Each dr As DataRow In dt.Rows
            str = dr(0).ToString()
            If str = "BS_Scheme" Then
                strCollection += "(TSPL_TEMP_DISCOUNT.Trade_Disc_Amt/TSPL_TEMP_DISCOUNT.Trade_Disc_Amt/(TSPL_TEMP_DISCOUNT.TP/24))"
            Else
                strCollection += "TSPL_TEMP_DISCOUNT." + dr(0).ToString()
            End If
            strCollection += (" As ")
            strCollection += dr(1).ToString() + ","
        Next
        Dim strCmd As String = " SELECT " + strCollection + "  TSPL_ITEM_DETAILS.Class_Code " & _
                              "  FROM  TSPL_TEMP_DISCOUNT INNER JOIN TSPL_ITEM_DETAILS ON TSPL_TEMP_DISCOUNT.Item_Code = TSPL_ITEM_DETAILS.Item_Code"
        Dim whrCls As String = " and (TSPL_ITEM_DETAILS.Class_Name = 'size')"
        transportSql.ExporttoExcel(strCmd, whrCls, Me)

    End Sub
End Class
