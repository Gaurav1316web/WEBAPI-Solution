Imports Microsoft.VisualBasic
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
Imports XpertERPEngine
Imports System.Threading
Imports Telerik.WinControls.UI.Export
Imports Telerik.WinControls.UI.Export.ExcelML
Public Class RptSaleAccountSummary
    Inherits FrmMainTranScreen
    Dim l1User, l2User, l3User, l4User, l5User As String
    Const colName As String = "Name"
    Const colCode As String = "Code"
    Dim userCode, companyCode, sql, strQuery As String
    Dim dr As DataTable
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim strPost As String
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.SaleAccountSummary)
        'If Not (MyBase.isReadFlag) Then
        '    RadMessageBox.Show("Permission Denied")
        '    Me.Close()
        '    Exit Sub
        'End If
        'btnSave.Visible = MyBase.isModifyFlag
        'btnPost.Visible = MyBase.isPostFlag
        'btnDelete.Visible = MyBase.isDeleteFlag
    End Sub
    Public Sub New(ByVal user As String, ByVal company As String)
        InitializeComponent()
        userCode = user
        companyCode = company
        sql = "SELECT  User_Type,Level1_Code, Level2_Code, Level3_Code, Level4_Code FROM TSPL_USER_MASTER WHERE User_Code='" + userCode + "'"
        'dr = clsDBFuncationality.GetDataTable(sql)
        Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(sql)
        If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
            For Each dr As DataRow In dt1.Rows
                l1User = dr(0).ToString()
                l2User = dr(1).ToString()
                l3User = dr(2).ToString()
                l4User = dr(3).ToString()
                l5User = dr(4).ToString()
            Next
        End If
    End Sub

    Private Sub RptSaleAccountSummary_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.P Then
            'Print()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag Then
            'SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isPostFlag Then
            'DeleteData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            Reset()
        End If
    End Sub

    Private Sub RptSaleAccountSummary_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        dtpstart.Value = clsCommon.GETSERVERDATE()
        dtpend.Value = clsCommon.GETSERVERDATE()
        dtpStarttime.Value = DateTime.Now
        dtpendtime.Value = DateTime.Now
        rdbSummary.IsChecked = True

        'grpSelect.Visible = True
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
        'rbtnCompanyAll.IsChecked = True
        rbtnCompanySelect.IsChecked = True
        Dim arrDBName As New ArrayList()
        arrDBName.Add(objCommonVar.CurrDatabase)
        cbgCompany.CheckedValue = arrDBName

        rdbSku.IsChecked = True

        'grpSelect.Visible = True
        'grpSku.Visible = True
        ddlType.Text = "Quantity"
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnreset, "Press Alt+N for reset")
        RadPageView1.SelectedPage = RadPageViewPage1
        rdbAll.IsChecked = True
    End Sub
    Sub Loadlocation()
        '  Dim qry As String = "select Location_Code,Location_Desc from TSPL_LOCATION_MASTER where Location_Type <> 'Logical'  order by Location_Code"
        'Dim qry As String = " select Segment_code as Code, Description  from TSPL_GL_SEGMENT_CODE where Seg_No='7' "
        'cbglocation.DataSource = clsDBFuncationality.GetDataTable(qry)
        'cbglocation.ValueMember = "Code"
        'cbglocation.DisplayMember = "Description"
        cbglocation.DataSource = clsLocation.GetLocationSegments()
        cbglocation.ValueMember = "Code"
        cbglocation.DisplayMember = "Name"
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
    Sub reset()
        dtpstart.Value = clsCommon.GETSERVERDATE()
        dtpend.Value = clsCommon.GETSERVERDATE()
        dtpStarttime.Value = DateTime.Now
        dtpendtime.Value = DateTime.Now
        rdbSummary.IsChecked = True
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
        ddlType.Text = "Quantity"
        'grpSelect.Visible = True
        LoadCompany()
        rbtnCompanyAll.IsChecked = True
        rdbSku.IsChecked = True
        rdbAll.IsChecked = True
        rdbIterComp.Checked = True
    End Sub
    Sub print()
        Try
            Dim strInterPost As String
            If rdbAll.IsChecked = True Then
                strInterPost = ""
                strPost = ""
            Else
                strInterPost = " and Is_Post=1 "
                strPost = " and Is_Post='Y' "
            End If

            'GV1.EnableFiltering = True
            Dim dt As DataTable = Nothing
            If chklocSelect.IsChecked = True AndAlso cbglocation.CheckedValue.Count <= 0 Then
                RadMessageBox.Show("Please select at least one Location or select ALL")
                Return
            ElseIf chkChkSelect.IsChecked = True AndAlso cbgCustomer.CheckedValue.Count <= 0 Then
                RadMessageBox.Show("Please select at least one Customer or select ALL")
                Return
            ElseIf chkItemSelect1.IsChecked = True AndAlso cbgItem1.CheckedValue.Count <= 0 Then
                RadMessageBox.Show("Please select at least one Item or select ALL")
                Return
            ElseIf chkClassSelect.IsChecked = True AndAlso chkCustomerClass.CheckedValue.Count <= 0 Then
                RadMessageBox.Show("Please select at least one Customer Type or select ALL")
                Return
            ElseIf chkGroupSelect.IsChecked = True AndAlso cbgCustGroup.CheckedValue.Count <= 0 Then
                RadMessageBox.Show("Please select at least one Customer Group or select ALL")
                Return
            ElseIf rbtnCompanySelect.IsChecked = True AndAlso cbgCompany.CheckedValue.Count <= 0 Then
                RadMessageBox.Show("Please select at least one Company or select ALL")
                Return
            End If
            Dim strSql, strInterComp, strCustAll, strLocAll, strItemAll, strClassAll, strCustGroup, strSQL1Group, strReportTitle, strOrderColumn, strOrderBy As String

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


            If rdbSku.IsChecked = True Then
                strSQL1Group = clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER.Item_Code"
                strReportTitle = "Cash Discount Summary Sku wise"
                strOrderColumn = "TSPL_ITEM_MASTER.Sku_Seq"
                strOrderBy = "Order By TSPL_ITEM_MASTER.Sku_Seq"
            ElseIf rdbPack.IsChecked = True Then
                strSQL1Group = clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS.Class_Desc"
                strReportTitle = "Cash Discount Summary Pack wise"
                strOrderColumn = "TSPL_ITEM_MASTER.Pack_Seq"
                strOrderBy = "Order By TSPL_ITEM_MASTER.Pack_Seq"
            ElseIf rdbFlavour.IsChecked = True Then
                strSQL1Group = "TSPL_ITEM_DETAILS_1.Class_Desc"
                strReportTitle = "Cash Discount Summary Flavour wise"
                strOrderColumn = "TSPL_ITEM_MASTER.Flavour_Seq"
                strOrderBy = "Order By TSPL_ITEM_MASTER.Flavour_Seq"
            End If
            Dim strLoca As String = ""
            Dim strClass As String = ""
            If chklocSelect.IsChecked Then
                For Each Str As String In cbglocation.CheckedDisplayMember
                    If clsCommon.myLen(strLoca) > 0 Then
                        strLoca += ", "
                    End If
                    strLoca += Str
                Next
            End If

            If chkClassSelect.IsChecked Then
                For Each Str As String In chkCustomerClass.CheckedDisplayMember
                    If clsCommon.myLen(strClass) > 0 Then
                        strClass += ", "
                    End If
                    strClass += Str
                Next
            End If
            Dim strPivot As String
            If rdbSku.IsChecked = True Then
                strPivot = "TSPL_SALE_INVOICE_DETAIL.Item_Code"
            ElseIf rdbPack.IsChecked = True Then
                strPivot = "TSPL_ITEM_DETAILS.Class_Desc"
            ElseIf rdbFlavour.IsChecked = True Then
                strPivot = "TSPL_ITEM_DETAILS_1.Class_Desc"
            End If



            Dim str1 As String = "SELECT case when Unit_code='FC' then " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.MRP_Amt / TSPL_ITEM_UOM_DETAIL_1.Conversion_Factor else " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.MRP_Amt end AS MRPBottle," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Comp_Code AS Company, " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Desc AS Location, " & _
                      "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Desc AS [Customer Group], " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Desc AS [Customer Type], " & _
                      "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Customer_Name AS Customer, " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No AS Invoice, " & _
                      "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date AS [Invoice Date], " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Route_No AS [Route No],  " & _
                      "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Route_Desc AS [Route Desc], " + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.EMP_CODE AS [Salesman Code], " & _
                      "" + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.Emp_Name AS [Salesman Name], " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Vehicle_No AS Vehicle, " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Code AS Item, " & _
                      "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.MRP_Amt * " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor AS MRP, " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Price_Amount1 AS [Trade Margin], " & _
                      "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Price_Amount2 AS [Party Discount], " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Price_Amount3 AS [Extra Discount], " & _
                      "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Price_Amount4 AS [Dist Margin], " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Price_Amount5 AS [Agency Margin], " & _
                      "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Price_Amount6 AS [TPT and Others], " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Price_Amount7 AS [Scheme Disc], " & _
                      "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Price_Amount8 AS PC1, " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Price_Amount9 AS PC2, " & _
                      "((MRP_Amt * " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor) - (Price_Amount1)) as [Trade Price], " & _
                      "case when scheme_item='N' then " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Invoice_Qty/" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor else 0 end AS [Sale Qty], " & _
                      "case when scheme_item='Y' then " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Invoice_Qty/" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor else 0 end AS [FOC Qty], " & _
                      "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Invoice_Qty/" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor AS [Gross Qty], " & _
                      "case when scheme_item='N' then ((MRP_Amt * " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor) - (Price_Amount1 + Price_Amount2 + Price_Amount3 + Price_Amount4 + Price_Amount5 + Price_Amount6 + Price_Amount7 + Price_Amount8 + Price_Amount9)) * ( TSPL_SALE_INVOICE_DETAIL.Invoice_Qty/" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor) else 0 end as [Sale Amount], " & _
                      "case when scheme_item='Y' then ((MRP_Amt * " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor) - (Price_Amount1 + Price_Amount2 + Price_Amount3 + Price_Amount4 + Price_Amount5 + Price_Amount6 + Price_Amount7 + Price_Amount8 + Price_Amount9)) * ( TSPL_SALE_INVOICE_DETAIL.Invoice_Qty/" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor) else 0 end as [FOC Amount], " & _
                      "((MRP_Amt * " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor) - (Price_Amount1 + Price_Amount2 + Price_Amount3 + Price_Amount4 + Price_Amount5 + Price_Amount6 + Price_Amount7 + Price_Amount8 + Price_Amount9)) * ( TSPL_SALE_INVOICE_DETAIL.Invoice_Qty/" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor) as [Gross Amount] " & _
                      "FROM  " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL LEFT OUTER JOIN " & _
                      "" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL ON " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Code = " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Item_Code AND " & _
                      "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Unit_code = " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.UOM_Code RIGHT OUTER JOIN " & _
                      "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD ON " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No = " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No LEFT OUTER JOIN " & _
                      "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_GROUP_MASTER RIGHT OUTER JOIN " & _
                      "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_TYPE_MASTER RIGHT OUTER JOIN " & _
                      "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER ON " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Code = " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Type_Code ON  " & _
                      "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code = " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Group_Code ON " & _
                      "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Cust_Code = " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Code LEFT OUTER JOIN " & _
                      "" + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER ON " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Salesman_Code = " + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.EMP_CODE LEFT OUTER JOIN " & _
                      "" + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER ON " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Location = " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Code  " & _
                      "left outer join " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL as TSPL_ITEM_UOM_DETAIL_1 on  " & _
                      "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAIL_1.Item_Code " & _
                      "WHERE  TSPL_ITEM_UOM_DETAIL_1.UOM_Code='FB' and convert(date," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103) >= convert(date, '" & dtpstart.Value & "',103) AND " & _
                      "convert(date," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103) <= convert(date, '" & dtpend.Value & "',103) " & strPost & " "

            Dim str2 As String = "SELECT case when Unit_code='FC' then " + clsCommon.ReplicateDBString + "TSPL_SALE_Return_DETAIL.MRP_Amt / TSPL_ITEM_UOM_DETAIL_1.Conversion_Factor else " + clsCommon.ReplicateDBString + "TSPL_SALE_Return_DETAIL.MRP_Amt end AS MRPBottle, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Comp_Code AS Company, " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Desc AS Location, " & _
                     "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Desc AS [Customer Group], " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Desc AS [Customer Type], " & _
                     "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Customer_Name AS Customer, " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Sale_Return_No AS Invoice, " & _
                     "" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Invoice_Date AS [Invoice Date], " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Route_No AS [Route No],  " & _
                     "" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Route_Desc AS [Route Desc], " + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.EMP_CODE AS [Salesman Code], " & _
                     "" + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.Emp_Name AS [Salesman Name], " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Vehicle_No AS Vehicle, " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Item_Code AS Item, " & _
                     "" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.MRP_Amt * " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor AS MRP, " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Price_Amount1 AS [Trade Margin], " & _
                     "" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Price_Amount2 AS [Party Discount], " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Price_Amount3 AS [Extra Discount], " & _
                     "" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Price_Amount4 AS [Dist Margin], " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Price_Amount5 AS [Agency Margin], " & _
                     "" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Price_Amount6 AS [TPT and Others], " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Price_Amount7 AS [Scheme Disc], " & _
                     "" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Price_Amount8 AS PC1, " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Price_Amount9 AS PC2, " & _
                     "((MRP_Amt * " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor) - (Price_Amount1)) as [Trade Price], " & _
                     "case when scheme_item='N' then - (" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Return_Qty/" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor) else 0 end AS [Sale Qty], " & _
                     "case when scheme_item='Y' then - (" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Return_Qty/" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor) else 0 end AS [FOC Qty], " & _
                     "- (" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Return_Qty/" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor) AS [Gross Qty], " & _
                     "case when scheme_item='N' then -((MRP_Amt * " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor) - (Price_Amount1 + Price_Amount2 + Price_Amount3 + Price_Amount4 + Price_Amount5 + Price_Amount6 + Price_Amount7 + Price_Amount8 + Price_Amount9)) * ( TSPL_SALE_RETURN_DETAIL.Return_Qty/" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor) else 0 end as [Sale Amount], " & _
                     "case when scheme_item='Y' then -((MRP_Amt * " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor) - (Price_Amount1 + Price_Amount2 + Price_Amount3 + Price_Amount4 + Price_Amount5 + Price_Amount6 + Price_Amount7 + Price_Amount8 + Price_Amount9)) * ( TSPL_SALE_RETURN_DETAIL.Return_Qty/" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor) else 0 end as [FOC Amount], " & _
                     "-((MRP_Amt * " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor) - (Price_Amount1 + Price_Amount2 + Price_Amount3 + Price_Amount4 + Price_Amount5 + Price_Amount6 + Price_Amount7 + Price_Amount8 + Price_Amount9)) * ( TSPL_SALE_RETURN_DETAIL.Return_Qty/" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor) as [Gross Amount] " & _
                     "FROM  " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL LEFT OUTER JOIN " & _
                     "" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL ON " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Item_Code = " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Item_Code AND " & _
                     "" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Unit_code = " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.UOM_Code RIGHT OUTER JOIN " & _
                     "" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD ON " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Sale_Return_No = " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Sale_Return_No LEFT OUTER JOIN " & _
                     "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_GROUP_MASTER RIGHT OUTER JOIN " & _
                     "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_TYPE_MASTER RIGHT OUTER JOIN " & _
                     "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER ON " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Code = " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Type_Code ON  " & _
                     "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code = " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Group_Code ON " & _
                     "" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Cust_Code = " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Code LEFT OUTER JOIN " & _
                     "" + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER ON " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Salesman_Code = " + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.EMP_CODE LEFT OUTER JOIN " & _
                     "" + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER ON " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Location = " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Code  " & _
                     "left outer join " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL as TSPL_ITEM_UOM_DETAIL_1 on  " & _
                     "" + clsCommon.ReplicateDBString + "TSPL_SALE_Return_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAIL_1.Item_Code " & _
                     "WHERE  TSPL_ITEM_UOM_DETAIL_1.UOM_Code='FB' and convert(date," + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Invoice_Date,103) >= convert(date, '" & dtpstart.Value & "',103) AND " & _
                     "convert(date," + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Invoice_Date,103) <= convert(date, '" & dtpend.Value & "',103) " & strPost & " "


            strInterComp = "SELECT case when Unit_code='FC' then " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.MRP_Amt / TSPL_ITEM_UOM_DETAIL_1.Conversion_Factor else " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.MRP_Amt end AS MRPBottle, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_HEAD.Comp_Code AS Company, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Desc AS Location, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Desc AS [Customer Group], " & _
            "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Desc AS [Customer Type], " & _
            "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Customer_Name AS Customer, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_HEAD.Document_No AS Invoice, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_HEAD.Document_Date AS [Invoice Date], " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_HEAD.Route_No AS [Route No], " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_HEAD.Route_Desc AS [Route Desc], " & _
            "" + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.EMP_CODE AS [Salesman Code], " & _
            "" + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.Emp_Name AS [Salesman Name], " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_HEAD.Vehicle_No AS Vehicle, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.Item_Code AS Item, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.MRP_Amt * " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor AS MRP, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.Price_Amount1 AS [Trade Margin], " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.Price_Amount2 AS [Party Discount], " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.Price_Amount3 AS [Extra Discount], " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.Price_Amount4 AS [Dist Margin], " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.Price_Amount5 AS [Agency Margin], " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.Price_Amount6 AS [TPT and Others], " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.Price_Amount7 AS [Scheme Disc], " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.Price_Amount8 AS PC1, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.Price_Amount9 AS PC2, " & _
            "((MRP_Amt * " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor) - (Price_Amount1)) as [Trade Price], " & _
            "case when scheme_item='N' then - " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.Qty/" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor else 0 end AS [Sale Qty], " & _
            "case when scheme_item='Y' then - " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.Qty/" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor else 0 end AS [FOC Qty], " & _
            "- " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.Qty/" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor AS [Gross Qty], " & _
            "case when scheme_item='N' then - ((MRP_Amt * " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor) - (Price_Amount1 + Price_Amount2 + Price_Amount3 + Price_Amount4 + Price_Amount5 + Price_Amount6 + Price_Amount7 + Price_Amount8 + Price_Amount9)) * ( TSPL_SALE_RETURN_INTER_DETAIL.Qty/" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor) else 0 end as [Sale Amount], " & _
            "case when scheme_item='Y' then - ((MRP_Amt * " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor) - (Price_Amount1 + Price_Amount2 + Price_Amount3 + Price_Amount4 + Price_Amount5 + Price_Amount6 + Price_Amount7 + Price_Amount8 + Price_Amount9)) * ( TSPL_SALE_RETURN_INTER_DETAIL.Qty/" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor) else 0 end as [FOC Amount], " & _
            "- ((MRP_Amt * " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor) - (Price_Amount1 + Price_Amount2 + Price_Amount3 + Price_Amount4 + Price_Amount5 + Price_Amount6 + Price_Amount7 + Price_Amount8 + Price_Amount9)) * ( TSPL_SALE_RETURN_INTER_DETAIL.Qty/" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor) as [Gross Amount] " & _
            "FROM " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL LEFT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL ON " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.Item_Code = " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Item_Code " & _
            "AND " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.Unit_code = " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.UOM_Code " & _
            "RIGHT OUTER JOIN " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_HEAD ON  " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.Document_No = " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_HEAD.Document_No " & _
            "LEFT OUTER JOIN " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_GROUP_MASTER RIGHT OUTER JOIN " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_TYPE_MASTER " & _
            "RIGHT OUTER JOIN " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER ON " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Code = " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Type_Code ON " & _
            "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code = " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Group_Code ON  " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_HEAD.Cust_Code = " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Code LEFT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER ON " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_HEAD.Salesman_Code = " + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.EMP_CODE " & _
            "LEFT OUTER JOIN " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER ON " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_HEAD.Location = " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Code " & _
            "left outer join " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL as TSPL_ITEM_UOM_DETAIL_1 on  " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAIL_1.Item_Code " & _
            "WHERE  TSPL_ITEM_UOM_DETAIL_1.UOM_Code='FB' and convert(date," + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_HEAD.Document_Date,103) >= convert(date, '" & dtpstart.Value & "',103) AND  " & _
            "convert(date," + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_HEAD.Document_Date,103) <= convert(date, '" & dtpend.Value & "',103)  " & strInterPost & " "

            If strLocAll = "N" Then
                str1 += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_head.Location in (Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbglocation.CheckedValue) + ")) "
                str2 += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Location in (Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbglocation.CheckedValue) + ")) "
                strInterComp += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_HEAD.Location in (Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbglocation.CheckedValue) + ")) "
            End If
            If strCustAll = "N" Then
                str1 += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_head.Cust_Code in (" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ") "
                str2 += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Cust_Code in (" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ") "
                strInterComp += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_HEAD.Cust_Code in (" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ") "

            End If
            If strClassAll = "N" Then
                str1 += " and " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Customer_Class in (" + clsCommon.GetMulcallString(chkCustomerClass.CheckedValue) + ") "
                str2 += " and " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Customer_Class in (" + clsCommon.GetMulcallString(chkCustomerClass.CheckedValue) + ") "
                strInterComp += " and " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Customer_Class in (" + clsCommon.GetMulcallString(chkCustomerClass.CheckedValue) + ") "

            End If
            If strItemAll = "N" Then
                str1 += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Code in (" + clsCommon.GetMulcallString(cbgItem1.CheckedValue) + ") "
                str2 += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Item_Code in (" + clsCommon.GetMulcallString(cbgItem1.CheckedValue) + ") "
                strInterComp += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.Item_Code in (" + clsCommon.GetMulcallString(cbgItem1.CheckedValue) + ") "

            End If
            If strCustGroup = "N" Then
                str1 += " and " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Group_Code in (" + clsCommon.GetMulcallString(cbgCustGroup.CheckedValue) + ") "
                str2 += " and " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Group_Code in (" + clsCommon.GetMulcallString(cbgCustGroup.CheckedValue) + ") "
                strInterComp += " and " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Group_Code in (" + clsCommon.GetMulcallString(cbgCustGroup.CheckedValue) + ") "
            End If

            If rdbIterComp.Checked Then
                strSql = str1 & " Union All " & str2 & " Union All " & strInterComp
            Else
                strSql = str1 & " Union All " & str2
            End If

            Dim ArrDBName As ArrayList = Nothing
            If rbtnCompanyAll.IsChecked Then
                ArrDBName = cbgCompany.AllValue
            Else
                ArrDBName = cbgCompany.CheckedValue
            End If
            strQuery = clsCommon.GetQueryWithAllSelectedDataBase(strSql, ArrDBName, False)
            strQuery = "select TSPL_COMPANY_MASTER.Logo_Img,TSPL_COMPANY_MASTER.Logo_Img2, TSPL_COMPANY_MASTER.Tin_No, TSPL_COMPANY_MASTER.CST_LST, TSPL_COMPANY_MASTER.Ecc_No,TSPL_COMPANY_MASTER.Comp_Name,  (Case When ISNULL(TSPL_COMPANY_MASTER.Add1,'')='' Then  '' Else TSPL_COMPANY_MASTER.Add1 + case When ISNULL(TSPL_COMPANY_MASTER.Add1,'')='' Then  '' Else ', '+ TSPL_COMPANY_MASTER.Add2 + Case When ISNULL(TSPL_COMPANY_MASTER.Add3,'')='' Then  '' Else TSPL_COMPANY_MASTER.Add3 End End End) AS CompAddress,Company,Location,[Customer Group],[Customer Type],Customer,Invoice,[Invoice Date],Item, [Route No],[Route Desc],[Salesman Code],[Salesman Name],Vehicle,convert(decimal(18,2),MRP) as MRP,convert(decimal(18,2),MRPBottle) as MRPBottle,[Trade Margin],MRP- [Trade Margin] as TP,[Dist Margin],MRP- [Trade Margin] - [Dist Margin] as DP,[Sale Qty], (MRP- [Trade Margin]) * [Sale Qty] as TradeAmt,(MRP- [Trade Margin] - [Dist Margin]) * [Sale Qty] as DistAmt FROM " & _
                   " (" & strQuery & ") aa"

            'dt = clsDBFuncationality.GetDataTable(strQuery)
            'GV1.DataSource = Nothing
            Dim frmcrystal As New frmCrystalReportViewer()
            frmcrystal.funreport(CrystalReportFolder.SalesReport, clsDBFuncationality.GetDataTable(strQuery), "crptSaleAccountSummary", "Sales Account Summary")
            ''GV1.Columns.Clear()
            ''GV1.Rows.Clear()
            'GV1.GroupDescriptors.Clear()
            'GV1.MasterTemplate.SummaryRowsBottom.Clear()

            'If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
            '    RadMessageBox.Show("No Data Found to Display", Me.Text)
            '    Exit Sub
            'Else
            '    GV1.DataSource = dt
            '    SetGridFormationOFGV1()
            'End If

        Catch ex As Exception
            RadMessageBox.Show(ex.Message, Me.Text)
        End Try
    End Sub


    'Sub SetGridFormationOFGV1()
    '    Dim strItemCode, head2 As String

    '    GV1.TableElement.TableHeaderHeight = 40
    '    GV1.MasterTemplate.ShowRowHeaderColumn = False
    '    For ii As Integer = 0 To GV1.Columns.Count - 1
    '        GV1.Columns(ii).ReadOnly = True
    '        GV1.Columns(ii).IsVisible = False
    '    Next

    '    If rdbSummary.IsChecked = True Then
    '        GV1.Columns("company").IsVisible = True
    '        GV1.Columns("company").Width = 50
    '        GV1.Columns("company").HeaderText = "company"

    '        GV1.Columns("Location").IsVisible = True
    '        GV1.Columns("Location").Width = 70
    '        GV1.Columns("Location").HeaderText = "Location"
    '        'GV1.Columns("Location").BestFit()

    '        GV1.Columns("Customer Group").IsVisible = True
    '        GV1.Columns("Customer Group").Width = 100
    '        GV1.Columns("Customer Group").HeaderText = "Customer"
    '        ''GV1.Columns("Customer Group").BestFit()

    '        GV1.Columns("Customer Type").IsVisible = True
    '        GV1.Columns("Customer Type").Width = 100
    '        GV1.Columns("Customer Type").HeaderText = "Customer Type"
    '        ''GV1.Columns("Customer Type").BestFit()

    '        GV1.Columns("Customer").IsVisible = True
    '        GV1.Columns("Customer").Width = 80
    '        GV1.Columns("Customer").HeaderText = "Customer"
    '        ''GV1.Columns("Customer").BestFit()

    '        GV1.Columns("Invoice").IsVisible = True
    '        GV1.Columns("Invoice").Width = 80
    '        GV1.Columns("Invoice").HeaderText = "Doc No"
    '        ''GV1.Columns("Invoice").BestFit()

    '        GV1.Columns("Invoice Date").IsVisible = True
    '        GV1.Columns("Invoice Date").Width = 80
    '        GV1.Columns("Invoice Date").HeaderText = "Invoice Date"
    '        ''GV1.Columns("Invoice Date").BestFit()

    '        GV1.Columns("Route No").IsVisible = True
    '        GV1.Columns("Route No").Width = 80
    '        GV1.Columns("Route No").HeaderText = "Route No"
    '        'GV1.Columns("Route No").BestFit()

    '        GV1.Columns("Route Desc").IsVisible = True
    '        GV1.Columns("Route Desc").Width = 80
    '        GV1.Columns("Route Desc").HeaderText = "Route Desc"
    '        'GV1.Columns("Route Desc").BestFit()

    '        GV1.Columns("Salesman Code").IsVisible = True
    '        GV1.Columns("Salesman Code").Width = 80
    '        GV1.Columns("Salesman Code").HeaderText = "Salesman Code"
    '        'GV1.Columns("Salesman Code").BestFit()

    '        GV1.Columns("Salesman Name").IsVisible = True
    '        GV1.Columns("Salesman Name").Width = 80
    '        GV1.Columns("Salesman Name").HeaderText = "Salesman Name"
    '        'GV1.Columns("Salesman Name").BestFit()

    '        GV1.Columns("Vehicle").IsVisible = True
    '        GV1.Columns("Vehicle").Width = 80
    '        GV1.Columns("Vehicle").HeaderText = "Vehicle"
    '        'GV1.Columns("Vehicle").BestFit()

    '        GV1.Columns("Item").IsVisible = True
    '        GV1.Columns("Item").Width = 80
    '        GV1.Columns("Item").HeaderText = "Item"
    '        'GV1.Columns("Item").BestFit()

    '        GV1.Columns("MRP").IsVisible = True
    '        GV1.Columns("MRP").Width = 80
    '        GV1.Columns("MRP").HeaderText = "MRPCase"

    '        GV1.Columns("MRPBottle").IsVisible = True
    '        GV1.Columns("MRPBottle").Width = 80
    '        GV1.Columns("MRPBottle").HeaderText = "MRPBottle"
    '        'GV1.Columns("MRP").BestFit()

    '        GV1.Columns("Trade Margin").IsVisible = True
    '        GV1.Columns("Trade Margin").Width = 80
    '        GV1.Columns("Trade Margin").HeaderText = "Trade Margin"
    '        'GV1.Columns("Trade Margin").BestFit()

    '        GV1.Columns("Party Discount").IsVisible = True
    '        GV1.Columns("Party Discount").Width = 80
    '        GV1.Columns("Party Discount").HeaderText = "Party Discount"
    '        'GV1.Columns("Party Discount").BestFit()

    '        GV1.Columns("Extra Discount").IsVisible = True
    '        GV1.Columns("Extra Discount").Width = 80
    '        GV1.Columns("Extra Discount").HeaderText = "Extra Discount"
    '        'GV1.Columns("Extra Discount").BestFit()

    '        GV1.Columns("Dist Margin").IsVisible = True
    '        GV1.Columns("Dist Margin").Width = 80
    '        GV1.Columns("Dist Margin").HeaderText = "Dist Margin"
    '        'GV1.Columns("Dist Margin").BestFit()

    '        GV1.Columns("Agency Margin").IsVisible = True
    '        GV1.Columns("Agency Margin").Width = 80
    '        GV1.Columns("Agency Margin").HeaderText = "Agency Margin"
    '        'GV1.Columns("Agency Margin").BestFit()

    '        GV1.Columns("TPT and Others").IsVisible = True
    '        GV1.Columns("TPT and Others").Width = 80
    '        GV1.Columns("TPT and Others").HeaderText = "TPT and Others"
    '        'GV1.Columns("TPT and Others").BestFit()

    '        GV1.Columns("Scheme Disc").IsVisible = True
    '        GV1.Columns("Scheme Disc").Width = 80
    '        GV1.Columns("Scheme Disc").HeaderText = "Scheme Disc"
    '        'GV1.Columns("Scheme Disc").BestFit()

    '        GV1.Columns("PC1").IsVisible = True
    '        GV1.Columns("PC1").Width = 80
    '        GV1.Columns("PC1").HeaderText = "PC1"
    '        'GV1.Columns("PC1").BestFit()

    '        GV1.Columns("PC2").IsVisible = True
    '        GV1.Columns("PC2").Width = 80
    '        GV1.Columns("PC2").HeaderText = "PC2"
    '        'GV1.Columns("PC2").BestFit()

    '        GV1.Columns("Trade Price").IsVisible = True
    '        GV1.Columns("Trade Price").Width = 80
    '        GV1.Columns("Trade Price").HeaderText = "Trade Price"
    '        'GV1.Columns("Trade Price").BestFit()

    '        GV1.Columns("Sale Qty").IsVisible = True
    '        GV1.Columns("Sale Qty").Width = 80
    '        GV1.Columns("Sale Qty").HeaderText = "Sale Qty"
    '        'GV1.Columns("Sale Qty").BestFit()

    '        GV1.Columns("FOC Qty").IsVisible = True
    '        GV1.Columns("FOC Qty").Width = 80
    '        GV1.Columns("FOC Qty").HeaderText = "FOC Qty"
    '        'GV1.Columns("Sale Qty").BestFit()

    '        GV1.Columns("Gross Qty").IsVisible = True
    '        GV1.Columns("Gross Qty").Width = 80
    '        GV1.Columns("Gross Qty").HeaderText = "Gross Qty"
    '        'GV1.Columns("Sale Qty").BestFit()

    '        GV1.Columns("Sale Amount").IsVisible = True
    '        GV1.Columns("Sale Amount").Width = 80
    '        GV1.Columns("Sale Amount").HeaderText = "Sale Amount"

    '        GV1.Columns("FOC Amount").IsVisible = True
    '        GV1.Columns("FOC Amount").Width = 80
    '        GV1.Columns("FOC Amount").HeaderText = "FOC Amount"

    '        GV1.Columns("Gross Amount").IsVisible = True
    '        GV1.Columns("Gross Amount").Width = 80
    '        GV1.Columns("Gross Amount").HeaderText = "Gross Amount"

    '        GV1.Columns("TrademarginAmt").IsVisible = True
    '        GV1.Columns("TrademarginAmt").Width = 80
    '        GV1.Columns("TrademarginAmt").HeaderText = "Trade Margin Amount"

    '        GV1.Columns("DistmarginAmt").IsVisible = True
    '        GV1.Columns("DistmarginAmt").Width = 80
    '        GV1.Columns("DistmarginAmt").HeaderText = "Distributed Amount"
    '        'GV1.Columns("Amount").BestFit()


    '    End If

    '    Dim summaryRowItem As New GridViewSummaryRowItem()
    '    Dim intCount As Integer = 0

    '    'Dim item1 As New GridViewSummaryItem("Trade Margin", "{0:F2}", GridAggregateFunction.Sum)
    '    'summaryRowItem.Add(item1)
    '    'Dim item2 As New GridViewSummaryItem("Party Discount", "{0:F2}", GridAggregateFunction.Sum)
    '    'summaryRowItem.Add(item2)
    '    'Dim item3 As New GridViewSummaryItem("Extra Discount", "{0:F2}", GridAggregateFunction.Sum)
    '    'summaryRowItem.Add(item3)
    '    'Dim item4 As New GridViewSummaryItem("Dist Margin", "{0:F2}", GridAggregateFunction.Sum)
    '    'summaryRowItem.Add(item4)
    '    'Dim item5 As New GridViewSummaryItem("Agency Margin", "{0:F2}", GridAggregateFunction.Sum)
    '    'summaryRowItem.Add(item5)

    '    Dim item8 As New GridViewSummaryItem("FOC Qty", "{0:F2}", GridAggregateFunction.Sum)
    '    summaryRowItem.Add(item8)
    '    Dim item9 As New GridViewSummaryItem("Gross Qty", "{0:F2}", GridAggregateFunction.Sum)
    '    summaryRowItem.Add(item9)
    '    Dim item10 As New GridViewSummaryItem("Sale Qty", "{0:F2}", GridAggregateFunction.Sum)
    '    summaryRowItem.Add(item10)
    '    Dim item11 As New GridViewSummaryItem("Sale Amount", "{0:F2}", GridAggregateFunction.Sum)
    '    summaryRowItem.Add(item11)
    '    Dim item6 As New GridViewSummaryItem("FOC Amount", "{0:F2}", GridAggregateFunction.Sum)
    '    summaryRowItem.Add(item6)
    '    Dim item7 As New GridViewSummaryItem("Gross Amount", "{0:F2}", GridAggregateFunction.Sum)
    '    summaryRowItem.Add(item7)
    '    Dim item1 As New GridViewSummaryItem("TrademarginAmt", "{0:F2}", GridAggregateFunction.Sum)
    '    summaryRowItem.Add(item1)
    '    Dim item2 As New GridViewSummaryItem("DistmarginAmt", "{0:F2}", GridAggregateFunction.Sum)
    '    summaryRowItem.Add(item2)

    '    GV1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

    '    RadPageView1.SelectedPage = RadPageViewPage2
    'End Sub

    Private Sub ExportToExcel(ByVal exporter As EnumExportTo)
        Try


            Dim arrHeader As List(Of String) = New List(Of String)()
            Dim strtemp As String = "Date Range : " + clsCommon.GetPrintDate(dtpstart.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(dtpend.Value, "dd/MM/yyyy")
            arrHeader.Add(strtemp)

            If chkGroupSelect.IsChecked Then
                strtemp = ""
                For Each Str As String In cbgCustGroup.CheckedDisplayMember
                    If clsCommon.myLen(strtemp) > 0 Then
                        strtemp += ", "
                    End If
                    strtemp += Str
                Next
                arrHeader.Add("Customer Group : " + strtemp)
            End If
            If chkClassSelect.IsChecked Then
                strtemp = ""
                For Each Str As String In chkCustomerClass.CheckedDisplayMember
                    If clsCommon.myLen(strtemp) > 0 Then
                        strtemp += ", "
                    End If
                    strtemp += Str
                Next
                arrHeader.Add("Customer Type : " + strtemp)
            End If

            If chklocSelect.IsChecked Then
                strtemp = ""
                For Each Str As String In cbglocation.CheckedDisplayMember
                    If clsCommon.myLen(strtemp) > 0 Then
                        strtemp += ", "
                    End If
                    strtemp += Str
                Next
                arrHeader.Add("Location Segment : " + strtemp)
            End If



            ' clsCommon.MyExportToExcel("Sale Account Breakage " + IIf(rdbDetail.IsChecked, " ( Detail ) ", " ( Summary ) "), GV1, arrHeader, Me.Text)
            'If exporter = EnumExportTo.Excel Then
            '    clsCommon.MyExportToExcel("Sale Account Breakage " + IIf(rdbDetail.IsChecked, " ( Detail ) ", " ( Summary ) "), GV1, arrHeader, Me.Text)

            'Else
            '    clsCommon.MyExportToPDF("Sale Account Breakage " + IIf(rdbDetail.IsChecked, " ( Detail ) ", " ( Summary ) "), GV1, arrHeader, "Sale Account Breakage ", True)
            'End If

            ''Dim strReportTitle As String
            ''strReportTitle = "Sale Account Breakage"
            ''Dim saveDialog1 As New SaveFileDialog()
            ''saveDialog1.FileName = strReportTitle
            ''saveDialog1.Filter = "Excel Workbooks (*.xls;*.xlsx)|*.xls;*.xlsx"
            ''Dim Fullpath As String
            ''Dim path = Application.StartupPath
            ''Fullpath = path + "\" + saveDialog1.FileName
            ''Dim i As Integer = 0
            ''For i = 0 To GV1.ColumnCount - 1
            ''    Dim grow As GridViewRowInfo = TryCast(GV1.Rows(0), GridViewRowInfo)
            ''    If TypeOf grow.Cells(i).Value Is DateTime Then
            ''        Dim datecol As GridViewDateTimeColumn = TryCast(GV1.Columns(i), GridViewDateTimeColumn)
            ''        datecol.ExcelExportType = DisplayFormatType.ShortDate
            ''    End If
            ''Next i
            ''Dim exporter As New ExportToExcelML(GV1)
            ''exporter.SummariesExportOption = SummariesOption.ExportAll
            ' ''If rdbSummary.IsChecked = True Then
            ''exporter.ExportVisualSettings = True
            ' ''End If
            ''exporter.ExportHierarchy = True
            ''exporter.HiddenColumnOption = HiddenOption.DoNotExport
            ''exporter.SheetMaxRows = ExcelMaxRows._1048576
            ''AddHandler exporter.ExcelCellFormatting, AddressOf exporter_ExcelCellFormatting
            ''AddHandler exporter.ExcelTableCreated, AddressOf exporter_ExcelTableCreated
            ''exporter.SheetName = strReportTitle
            ''exporter.RunExport(Fullpath)
            ''Me.Controls.Remove(GV1)
            ''Dim xlsApp As Microsoft.Office.Interop.Excel.ApplicationClass
            ''Dim xlsWB As Microsoft.Office.Interop.Excel.WorkbookClass
            ''xlsApp = New Microsoft.Office.Interop.Excel.ApplicationClass
            ''xlsApp.Visible = True
            ''xlsWB = xlsApp.Workbooks.Open(Fullpath)
        Catch ex As Exception
            RadMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub

    Private Sub RunExportToExcelML(ByVal fileName As Object)
        'Try
        '    Dim exporter As New ExportToExcelML(GV1)
        '    exporter.SummariesExportOption = SummariesOption.ExportAll
        '    'If rdbSummary.IsChecked = True Then
        '    '    exporter.ExportVisualSetting = True
        '    'End If
        '    exporter.ExportHierarchy = True
        '    exporter.HiddenColumnOption = HiddenOption.DoNotExport
        '    exporter.SheetMaxRows = ExcelMaxRows._1048576
        '    AddHandler exporter.ExcelCellFormatting, AddressOf exporter_ExcelCellFormatting
        '    AddHandler exporter.ExcelTableCreated, AddressOf exporter_ExcelTableCreated
        '    exporter.RunExport(fileName.ToString())
        '    Dim text As String = "Export finished successfully!"
        '    Dim xlApp As Excel.Application
        '    xlApp = New Excel.ApplicationClass
        '    Process.Start(fileName.ToString())

        '    RadMessageBox.Show(text)
        'Catch ex As Exception
        '    RadMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        'End Try
    End Sub


    '''' <summary>        
    '''' '''' using ExcelCellFormatting event for updating progress bar and applying custom format in excel file        
    '''' '''' </summary>        

    Private Sub exporter_ExcelCellFormatting(ByVal sender As Object, ByVal e As ExcelCellFormattingEventArgs)
        If e.GridRowInfoType Is GetType(GridViewDataRowInfo) Then
        End If
    End Sub

    '''' <summary>        
    '''' '''' using ExcelTableCreated event for adding custom header row        
    '''' '''' </summary>        

    Private Sub exporter_ExcelTableCreated(ByVal sender As Object, ByVal e As ExcelTableCreatedEventArgs)
        ''Dim strReportTitle, strConverted, strOrderby, head2, strSummary, strQty As String

        ''If rdbSku.IsChecked = True Then
        ''    strReportTitle = "Sku wise"
        ''ElseIf rdbPack.IsChecked = True Then
        ''    strReportTitle = "pack wise"
        ''ElseIf rdbFlavour.IsChecked = True Then
        ''    strReportTitle = "Flavour wise"

        ''End If

        ''If e.SheetIndex = 0 Then 'add header row only for the first excel sheet                

        ''    Dim style1 As SingleStyleElement = (CType(sender, ExportToExcelML)).AddCustomExcelRow(e.ExcelTableElement, 20, "Sale Account Breakage ")
        ''    Dim style3 As SingleStyleElement = (CType(sender, ExportToExcelML)).AddCustomExcelRow(e.ExcelTableElement, 20, "Start Date : = " + clsCommon.GetPrintDate(dtpstart.Value, "dd/MM/yyyy"))
        ''    Dim style4 As SingleStyleElement = (CType(sender, ExportToExcelML)).AddCustomExcelRow(e.ExcelTableElement, 20, "End Date : = " + clsCommon.GetPrintDate(dtpend.Value, "dd/MM/yyyy"))

        ''    If chklocSelect.IsChecked Then
        ''        Dim strLoca As String = ""
        ''        For Each Str As String In cbglocation.CheckedDisplayMember
        ''            If clsCommon.myLen(strLoca) > 0 Then
        ''                strLoca += ", "
        ''            End If
        ''            strLoca += Str
        ''        Next
        ''        Dim style6 As SingleStyleElement = (CType(sender, ExportToExcelML)).AddCustomExcelRow(e.ExcelTableElement, 20, "Location : " + strLoca)
        ''        Dim style7 As SingleStyleElement = (CType(sender, ExportToExcelML)).AddCustomExcelRow(e.ExcelTableElement, 20, "")

        ''    End If

        ''End If
    End Sub


    Private Sub chkGroupAll_ToggleStateChanged(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkGroupAll.ToggleStateChanged
        cbgCustGroup.Enabled = Not chkGroupAll.IsChecked
    End Sub
    Private Sub chkCustAll_ToggleStateChanged(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkCustAll.ToggleStateChanged
        cbgCustomer.Enabled = Not chkCustAll.IsChecked
    End Sub

    Private Sub chkClassAll_ToggleStateChanged(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkClassAll.ToggleStateChanged
        chkCustomerClass.Enabled = Not chkClassAll.IsChecked
    End Sub

    Private Sub rbtnCompanyAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtnCompanyAll.ToggleStateChanged
        cbgCompany.Enabled = Not rbtnCompanyAll.IsChecked
    End Sub
    Private Sub btnclose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub btnreset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnreset.Click
        reset()
        'GV1.DataSource = Nothing
        'GV1.Columns.Clear()
        'GV1.Rows.Clear()

        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

    Private Sub btnExportToExcel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExportToExcel.Click
        'If GV1.Rows.Count > 0 Then
        '    ExportToExcel()
        'Else
        '    RadMessageBox.Show("No Data Found to Display", Me.Text)
        'End If
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        print()
    End Sub


    Private Sub chklocAll_ToggleStateChanged1(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chklocAll.ToggleStateChanged
        cbglocation.Enabled = Not chklocAll.IsChecked
    End Sub

    Private Sub chkCustAll_ToggleStateChanged1(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkCustAll.ToggleStateChanged
        cbgCustomer.Enabled = Not chkCustAll.IsChecked

    End Sub

    Private Sub chkClassAll_ToggleStateChanged1(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkClassAll.ToggleStateChanged
        chkCustomerClass.Enabled = Not chkClassAll.IsChecked

    End Sub

    Private Sub chkGroupAll_ToggleStateChanged1(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkGroupAll.ToggleStateChanged
        cbgCustGroup.Enabled = Not chkGroupAll.IsChecked
    End Sub

    Private Sub rbtnCompanyAll_ToggleStateChanged1(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtnCompanyAll.ToggleStateChanged
        cbgCompany.Enabled = Not rbtnCompanyAll.IsChecked

    End Sub

    Private Sub chkItemAll1_ToggleStateChanged(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkItemAll1.ToggleStateChanged
        cbgItem1.Enabled = Not chkItemAll1.IsChecked
    End Sub





    'Private Sub RadMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem2.Click
    '    If GV1.Rows.Count > 0 Then
    '        ExportToExcel(EnumExportTo.PDF)
    '    Else
    '        RadMessageBox.Show("No Data Found to Display", Me.Text)
    '    End If
    'End Sub

    'Private Sub RadMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem1.Click
    '    If GV1.Rows.Count > 0 Then
    '        ExportToExcel(EnumExportTo.Excel)
    '    Else
    '        RadMessageBox.Show("No Data Found to Display", Me.Text)
    '    End If
    'End Sub
End Class
