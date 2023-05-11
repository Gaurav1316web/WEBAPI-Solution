'--------------------------------Last modify By - Dipti ------------------------------------
'--------------------------------Last modify date - 29/01/2013-------------------------------------
'---------At the form load only that company will keep checked by whom user get logged in------
'By vipin for PDF Work 
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

Public Class frmCashDiscountNew
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.CashDiscountReport)
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow("Permission Denied")
            Me.Close()
            Exit Sub
        End If
        'btnSave.Visible = MyBase.isModifyFlag
        'btnPost.Visible = MyBase.isPostFlag
        'btnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub FrmSaleAccountBreakOrCashDisc_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        SetUserMgmtNew()
        txtFromDate.Value = clsCommon.GETSERVERDATE()
        txtTodate.Value = clsCommon.GETSERVERDATE()
        rbtnPosted.IsChecked = True
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

        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

    Sub Loadlocation()
        'Dim qry As String = " select Segment_code as Code, Description  from TSPL_GL_SEGMENT_CODE where Seg_No='7' "
        'cbglocation.DataSource = clsDBFuncationality.GetDataTable(qry)

        cbglocation.DataSource = clsLocation.GetLocationSegments()
        cbglocation.ValueMember = "Code"
        cbglocation.DisplayMember = "Name"

    End Sub

    Sub LoadCustomer()
        Dim qry As String = "select Cust_Code as [Customer Code],Customer_Name as [Customer Name],Customer_Class as [Customer Class] from TSPL_CUSTOMER_MASTER"
        cbgCustomer.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgCustomer.ValueMember = "Customer Code"
        cbgCustomer.DisplayMember = "Customer Name"
    End Sub

    Sub LoadCustomerGroup()
        Dim qry As String = "select Cust_Group_Code as [Customer Group Code],Cust_Group_Desc as [Description]from TSPL_CUSTOMER_GROUP_MASTER"
        cbgCustGroup.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgCustGroup.ValueMember = "Customer Group Code"
        cbgCustGroup.DisplayMember = "Description"
    End Sub

    Sub LoadItem()
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
        cbgCompany.DisplayMember = "Company Name"
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Close()
    End Sub

    Sub LoadCustomerClass()
        Dim dt As DataTable = clsDBFuncationality.GetDataTable("select Cust_Type_Code,Cust_Type_Desc from TSPL_CUSTOMER_TYPE_MASTER")
        chkCustomerClass.DataSource = dt
        chkCustomerClass.ValueMember = "Cust_Type_Code"
        chkCustomerClass.DisplayMember = "Cust_Type_Desc"
    End Sub

    Private Sub btnreset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnreset.Click
        reset()
    End Sub

    Sub reset()
        txtFromDate.Value = clsCommon.GETSERVERDATE()
        txtTodate.Value = clsCommon.GETSERVERDATE()
        rbtnPosted.IsChecked = True
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

    Private Sub chkGroupAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkGroupAll.ToggleStateChanged, chkGroupSelect.ToggleStateChanged
        cbgCustGroup.Enabled = chkGroupSelect.IsChecked
    End Sub

    Private Sub chkItemAll1_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkItemAll1.ToggleStateChanged, chkItemSelect1.ToggleStateChanged
        cbgItem1.Enabled = chkItemSelect1.IsChecked
    End Sub

    Private Sub btnclose_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Try
            gv1.EnableFiltering = True
            LoadData()
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Sub LoadData()
        If chkGroupSelect.IsChecked AndAlso cbgCustGroup.CheckedValue.Count <= 0 Then
            Throw New Exception("Please select at least one customer Group")

        End If
        If chkChkSelect.IsChecked AndAlso cbgCustomer.CheckedValue.Count <= 0 Then
            Throw New Exception("Please select at least one customer")
        End If
        If chkClassSelect.IsChecked AndAlso chkCustomerClass.CheckedValue.Count <= 0 Then
            Throw New Exception("Please select at least one customer Class")
        End If
        If chkItemSelect1.IsChecked AndAlso cbgItem1.CheckedValue.Count <= 0 Then
            Throw New Exception("Please select at least one item")

        End If
        If chklocSelect.IsChecked AndAlso cbglocation.CheckedValue.Count <= 0 Then
            Throw New Exception("Please select at least one location")

        End If
        If rbtnCompanySelect.IsChecked AndAlso cbgCompany.CheckedValue.Count <= 0 Then
            Throw New Exception("Please select at least one company")

        End If
        Dim qry As String = " select " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Comp_Code," + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER.Comp_Name," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Location," + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Desc," + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Group_Code," + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Desc," + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Type_Code," + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Desc," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Cust_Code," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Cust_Name ," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No,convert(varchar(10)," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103) as Sale_Invoice_Date," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Route_No," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Route_Desc," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Salesman_Code ," + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.Emp_Name as Salesman_Name," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Vehicle_Code," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Vehicle_No," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Code," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Desc, "
        qry += " convert(decimal	(18,2), round(" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.MRP_Amt * " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor,0)) as  MRP,"
        qry += " convert(decimal(18,2), round( " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Invoice_Qty/ " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor,2)) as Invoice_Qty,"
        qry += " convert(decimal(18,2), round((" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Cust_Discount*" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Invoice_Qty),2)) as CashDiscount,"
        qry += " " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Unit_code," + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor," + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER.Sku_Seq "
        qry += " from " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL"
        qry += " left outer join " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD on " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No=" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No"
        qry += " left outer join " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER on " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Code=" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Location"
        qry += " left outer join " + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER on " + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER.Comp_Code=" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Comp_Code"
        qry += " left outer join " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER on " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Code=" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Cust_Code"
        qry += " left outer join " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_GROUP_MASTER on " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code=" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Group_Code"
        qry += " left outer join " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_TYPE_MASTER on " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Code=" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Type_Code"
        qry += " left outer join " + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER on " + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.EMP_CODE=" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Salesman_Code"
        qry += " left outer join " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL on " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Item_Code=" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Code and " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.UOM_Code=" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Unit_code"
        qry += " left outer join " + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER on " + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER.Item_Code=" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Code" + Environment.NewLine
        qry += " where 2=2 and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Cust_Discount>0  and ( " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Scheme_Item <> 'Y') and  " & _
                 "( Discount_Code = '') and  (TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Code <> 'F' and TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Code <> 'S') "
        If rbtnPosted.IsChecked = True Then
            qry += " and Is_Post='Y' "
        End If
        qry += "  and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date>='" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "' and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date<='" + clsCommon.GetPrintDate(txtTodate.Value, "dd/MMM/yyyy") + "'"
        If chkGroupSelect.IsChecked AndAlso cbgCustGroup.CheckedValue.Count > 0 Then
            qry += " and " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Group_Code in (" + clsCommon.GetMulcallString(cbgCustGroup.CheckedValue) + ")"
        End If
        If chkChkSelect.IsChecked AndAlso cbgCustomer.CheckedValue.Count > 0 Then
            qry += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Cust_Code in (" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ")"
        End If
        If chkClassSelect.IsChecked AndAlso chkCustomerClass.CheckedValue.Count > 0 Then
            qry += " and " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Type_Code in (" + clsCommon.GetMulcallString(chkCustomerClass.CheckedValue) + ")"
        End If
        If chkItemSelect1.IsChecked AndAlso cbgItem1.CheckedValue.Count > 0 Then
            qry += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Code in (" + clsCommon.GetMulcallString(cbgItem1.CheckedValue) + ")"
        End If
        If chklocSelect.IsChecked AndAlso cbglocation.CheckedValue.Count > 0 Then
            qry += " and " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbglocation.CheckedValue) + ")"
        End If

        Dim FinalQury As String = ""
        Dim dt As DataTable = Nothing
        If rdbDetail.IsChecked Then
            FinalQury = "select xxxx.Comp_Code,max(xxxx.Comp_Name) as Comp_Name,xxxx.Location,max(xxxx.Location_Desc) as Location_Desc,xxxx.Cust_Group_Code,max(xxxx.Cust_Group_Desc) as Cust_Group_Desc,xxxx.Cust_Type_Code,max(xxxx.Cust_Type_Desc) as Cust_Type_Desc,xxxx.Cust_Code,max(xxxx.Cust_Name ) as Cust_Name ,xxxx.Sale_Invoice_No,max(xxxx.Sale_Invoice_Date) as Sale_Invoice_Date,xxxx.Route_No,max(xxxx.Route_Desc) as Route_Desc,xxxx.Salesman_Code ,max(xxxx.Salesman_Name) as Salesman_Name,xxxx.Vehicle_Code,max(xxxx.Vehicle_No) as Vehicle_No,xxxx.Item_Code,max(xxxx.Item_Desc) as Item_Desc, xxxx.MRP,sum(xxxx.Invoice_Qty) as Invoice_Qty,"
            FinalQury += " CONVERT(decimal(18,2),ROUND( sum(xxxx.CashDiscount) ,2))as CashDiscount,"
            FinalQury += " CONVERT(decimal(18,2), round(sum(xxxx.CashDiscount)/sum(xxxx.Invoice_Qty),2)) as DiscountPerCase"
            FinalQury += " from(" + qry + " )xxxx "
            FinalQury += " group by Comp_Code,Location,Cust_Group_Code,Cust_Type_Code,Cust_Code,Sale_Invoice_No,Route_No,Salesman_Code,Vehicle_Code,Item_Code,MRP"
            FinalQury = clsCommon.GetQueryWithAllSelectedDataBase(FinalQury, GetSelectedDatabase(), True)
            dt = clsDBFuncationality.GetDataTable(FinalQury)
        Else
            Dim strMainColumns As String = ""
            Dim strQtyColumns As String = ""
            Dim strAmtColumns As String = ""
            Dim strTotalQty As String = ""
            Dim strTotalAmt As String = ""
            Dim strTotal As String = ""
            qry = clsCommon.GetQueryWithAllSelectedDataBase(qry, GetSelectedDatabase(), False)

            Dim qryForPivotColumns As String = " select Item_Code   from (" + qry + ")xxxx group by  Item_Code order by max(sku_Seq)"
            Dim dtForPivot As DataTable = clsDBFuncationality.GetDataTable(qryForPivotColumns)
            If dtForPivot Is Nothing OrElse dtForPivot.Rows.Count <= 0 Then
                Throw New Exception("No Data found")
            End If
            Dim isFirstTime As Boolean = True
            For Each dr As DataRow In dtForPivot.Rows
                Dim strCurrQtyCol As String = "[" + clsCommon.myCstr(dr("Item_Code")) + " - Qty]"
                Dim strCurrAmtCol As String = "[" + clsCommon.myCstr(dr("Item_Code")) + " - Amt]"
                If Not isFirstTime Then
                    strQtyColumns += ","
                    strAmtColumns += ","
                    strTotalQty += "+"
                    strTotalAmt += "+"
                End If
                strQtyColumns += strCurrQtyCol
                strAmtColumns += strCurrAmtCol
                strTotalQty += "isnull(sum( " + strCurrQtyCol + "),0)"
                strTotalAmt += "isnull(sum( " + strCurrAmtCol + "),0) "


                strMainColumns += ",isnull(sum( " + strCurrQtyCol + "),0) as " + strCurrQtyCol + ",isnull(sum( " + strCurrAmtCol + "),0) as " + strCurrAmtCol + ""

                isFirstTime = False
            Next
            strTotal = " (" + strTotalQty + ") as TotalQty, (" + strTotalAmt + ") as TotalAmt "
            qry = "  select comp_code,MAX(Comp_Name) as Comp_Name,Location ,MAX(Location_Desc) as Location_Desc,Cust_Code,MAX(Cust_Name) as Cust_Name,Item_Code + ' - Qty' as Item_Code_Qty,Item_Code + ' - Amt' as Item_Code_Amt,SUM(Invoice_Qty) as Invoice_Qty,SUM(CashDiscount) as CashDiscount  from (" + qry + " )xxxx group by comp_code,Location,Cust_Code,Item_Code"
            FinalQury = "  select comp_code,MAX(Comp_Name) as Comp_Name,Location,MAX(Location_Desc) as Location_Desc,Cust_Code,MAX(Cust_Name) as Cust_Name, " + strTotal + " " + strMainColumns + "  from (" + Environment.NewLine
            FinalQury += qry
            FinalQury += " )xxxxx" + Environment.NewLine
            FinalQury += " pivot (sum(Invoice_Qty) for item_Code_Qty in (" + strQtyColumns + ") ) as PQ" + Environment.NewLine
            FinalQury += " pivot (sum(CashDiscount) for item_Code_Amt in (" + strAmtColumns + ") ) as PA" + Environment.NewLine
            FinalQury += " group by comp_code,Location,Cust_Code"
            dt = clsDBFuncationality.GetDataTable(FinalQury)
        End If


        gv1.DataSource = Nothing
        gv1.Columns.Clear()
        gv1.Rows.Clear()
        gv1.GroupDescriptors.Clear()
        gv1.MasterTemplate.SummaryRowsBottom.Clear()

        If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
            Throw New Exception("No Data Found to Display")
            Exit Sub
        End If
        gv1.DataSource = dt
        gv1.MasterTemplate.AllowAddNewRow = False
        SetGridFormationOFGV1()
        RadPageView1.SelectedPage = RadPageViewPage2
    End Sub

    Private Function GetSelectedDatabase() As ArrayList
        If rbtnCompanyAll.IsChecked Then
            Return cbgCompany.AllValue
        Else
            Return cbgCompany.CheckedValue
        End If
    End Function

    Sub SetGridFormationOFGV1()
        gv1.ShowGroupPanel = False
        gv1.TableElement.TableHeaderHeight = 40
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gv1.Columns.Count - 1
            gv1.Columns(ii).ReadOnly = True
            If rdbDetail.IsChecked Then
                gv1.Columns(ii).IsVisible = False
            Else
                gv1.Columns(ii).Width = 100
            End If
        Next
        If rdbDetail.IsChecked Then
            gv1.Columns("Comp_Code").HeaderText = "Company Code"

            gv1.Columns("Comp_Name").IsVisible = True
            gv1.Columns("Comp_Name").Width = 100
            gv1.Columns("Comp_Name").HeaderText = "Company"

            gv1.Columns("Location").HeaderText = "Location Code"

            gv1.Columns("Location_Desc").IsVisible = True
            gv1.Columns("Location_Desc").Width = 100
            gv1.Columns("Location_Desc").HeaderText = "Location"

            gv1.Columns("Cust_Group_Code").HeaderText = "Customer Group Code"

            gv1.Columns("Cust_Group_Desc").IsVisible = True
            gv1.Columns("Cust_Group_Desc").Width = 100
            gv1.Columns("Cust_Group_Desc").HeaderText = "Customer"

            gv1.Columns("Cust_Type_Code").HeaderText = "Customer Class Code"

            gv1.Columns("Cust_Type_Desc").IsVisible = True
            gv1.Columns("Cust_Type_Desc").Width = 100
            gv1.Columns("Cust_Type_Desc").HeaderText = "Customer Class"

            gv1.Columns("Cust_Code").HeaderText = "Customer Code"

            gv1.Columns("Cust_Name").IsVisible = True
            gv1.Columns("Cust_Name").Width = 100
            gv1.Columns("Cust_Name").HeaderText = "Customer"

            gv1.Columns("Sale_Invoice_No").IsVisible = True
            gv1.Columns("Sale_Invoice_No").Width = 100
            gv1.Columns("Sale_Invoice_No").HeaderText = "Invoice No"

            gv1.Columns("Sale_Invoice_Date").IsVisible = True
            gv1.Columns("Sale_Invoice_Date").Width = 100
            gv1.Columns("Sale_Invoice_Date").HeaderText = "Date"

            gv1.Columns("Route_No").IsVisible = True
            gv1.Columns("Route_No").Width = 100
            gv1.Columns("Route_No").HeaderText = "Route No"

            gv1.Columns("Route_Desc").IsVisible = True
            gv1.Columns("Route_Desc").Width = 100
            gv1.Columns("Route_Desc").HeaderText = "Route"

            gv1.Columns("Salesman_Code").HeaderText = "Salesman Code"

            gv1.Columns("Salesman_Name").IsVisible = True
            gv1.Columns("Salesman_Name").Width = 100
            gv1.Columns("Salesman_Name").HeaderText = "Salesman"

            gv1.Columns("Vehicle_Code").HeaderText = "Vehicle Code"

            gv1.Columns("Vehicle_No").IsVisible = True
            gv1.Columns("Vehicle_No").Width = 100
            gv1.Columns("Vehicle_No").HeaderText = "Vehicle No"

            gv1.Columns("Item_Code").HeaderText = "Item Code"

            gv1.Columns("Item_Desc").IsVisible = True
            gv1.Columns("Item_Desc").Width = 100
            gv1.Columns("Item_Desc").HeaderText = "Item"

            gv1.Columns("MRP").IsVisible = True
            gv1.Columns("MRP").Width = 100
            gv1.Columns("MRP").HeaderText = "MRP"

            gv1.Columns("Invoice_Qty").IsVisible = True
            gv1.Columns("Invoice_Qty").Width = 100
            gv1.Columns("Invoice_Qty").HeaderText = "Invoice Qty"

            gv1.Columns("CashDiscount").IsVisible = True
            gv1.Columns("CashDiscount").Width = 100
            gv1.Columns("CashDiscount").HeaderText = "Cash Discount"

            gv1.Columns("DiscountPerCase").IsVisible = True
            gv1.Columns("DiscountPerCase").Width = 100
            gv1.Columns("DiscountPerCase").HeaderText = "Discount Per Case"

            Dim summaryRowItem As New GridViewSummaryRowItem()
            Dim item1 As New GridViewSummaryItem("Invoice_Qty", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item1)

            Dim item2 As New GridViewSummaryItem("CashDiscount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item2)

            gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        Else
            gv1.Columns("Comp_Code").HeaderText = "Company Code"
            gv1.Columns("Comp_Code").IsVisible = False


            gv1.Columns("Comp_Name").IsVisible = True
            gv1.Columns("Comp_Name").Width = 150
            gv1.Columns("Comp_Name").HeaderText = "Company"
            gv1.Columns("Comp_Name").IsPinned = True

            gv1.Columns("Location").HeaderText = "Location Code"
            gv1.Columns("Location").IsVisible = False

            gv1.Columns("Location_Desc").IsVisible = True
            gv1.Columns("Location_Desc").Width = 150
            gv1.Columns("Location_Desc").HeaderText = "Location"
            gv1.Columns("Location_Desc").IsPinned = True

            gv1.Columns("Cust_Code").HeaderText = "Customer Code"
            gv1.Columns("Cust_Code").IsVisible = False

            gv1.Columns("Cust_Name").IsVisible = True
            gv1.Columns("Cust_Name").Width = 150
            gv1.Columns("Cust_Name").HeaderText = "Customer"
            gv1.Columns("Cust_Name").IsPinned = True

            gv1.Columns("TotalQty").IsVisible = True
            gv1.Columns("TotalQty").Width = 101
            gv1.Columns("TotalQty").HeaderText = "Total Qty"
            gv1.Columns("TotalQty").IsPinned = True

            gv1.Columns("TotalAmt").IsVisible = True
            gv1.Columns("TotalAmt").Width = 101
            gv1.Columns("TotalAmt").HeaderText = "Total Amt"
            gv1.Columns("TotalAmt").IsPinned = True
            Dim summaryRowItem As New GridViewSummaryRowItem()
            For ii As Integer = 0 To gv1.Columns.Count - 1
                Dim strColName As String = clsCommon.myCstr(gv1.Columns(ii).Name)
                Dim strSubString As String = strColName.Substring(clsCommon.myLen(strColName) - 3, 3)
                If clsCommon.CompairString("Qty", strSubString) = CompairStringResult.Equal OrElse clsCommon.CompairString("Amt", strSubString) = CompairStringResult.Equal Then
                    Dim item1 As New GridViewSummaryItem(strColName, "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(item1)
                End If
            Next
            gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        End If
    End Sub

    Private Sub btnExportToExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExportToExcel.Click
        'Try
        '    LoadData()
        '    Dim arrHeader As List(Of String) = New List(Of String)()
        '    Dim strtemp As String = "Date Range : " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtTodate.Value, "dd/MM/yyyy")
        '    arrHeader.Add(strtemp)
        '    If chkGroupSelect.IsChecked Then
        '        strtemp = ""
        '        For Each Str As String In cbgCustGroup.CheckedDisplayMember
        '            If clsCommon.myLen(strtemp) > 0 Then
        '                strtemp += ", "
        '            End If
        '            strtemp += Str
        '        Next
        '        arrHeader.Add("Customer Group : " + strtemp)
        '    End If
        '    If chkClassSelect.IsChecked Then
        '        strtemp = ""
        '        For Each Str As String In chkCustomerClass.CheckedDisplayMember
        '            If clsCommon.myLen(strtemp) > 0 Then
        '                strtemp += ", "
        '            End If
        '            strtemp += Str
        '        Next
        '        arrHeader.Add("Customer Class : " + strtemp)
        '    End If
        '    If chkChkSelect.IsChecked Then
        '        strtemp = ""
        '        For Each Str As String In cbgCustomer.CheckedDisplayMember
        '            If clsCommon.myLen(strtemp) > 0 Then
        '                strtemp += ", "
        '            End If
        '            strtemp += Str
        '        Next
        '        arrHeader.Add("Customer : " + strtemp)
        '    End If

        '    If chklocSelect.IsChecked Then
        '        strtemp = ""
        '        For Each Str As String In cbglocation.CheckedDisplayMember
        '            If clsCommon.myLen(strtemp) > 0 Then
        '                strtemp += ", "
        '            End If
        '            strtemp += Str
        '        Next
        '        arrHeader.Add("Location Segment : " + strtemp)
        '    End If

        '    If chkItemSelect1.IsChecked Then
        '        strtemp = ""
        '        For Each Str As String In cbgItem1.CheckedDisplayMember
        '            If clsCommon.myLen(strtemp) > 0 Then
        '                strtemp += ", "
        '            End If
        '            strtemp += Str
        '        Next
        '        arrHeader.Add("Item : " + strtemp)
        '    End If
        '    If rbtnCompanySelect.IsChecked Then
        '        strtemp = ""
        '        For Each Str As String In cbgCompany.CheckedDisplayMember
        '            If clsCommon.myLen(strtemp) > 0 Then
        '                strtemp += ", "
        '            End If
        '            strtemp += Str
        '        Next
        '        arrHeader.Add("Company : " + strtemp)
        '    End If
        '    clsCommon.MyExportToExcel("Cash Discount" + IIf(rdbDetail.IsChecked, " ( Detail ) ", " ( Summary ) "), gv1, arrHeader, Me.Text)
        'Catch ex As Exception
        '    common.clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        'End Try
    End Sub


    Public Sub printdata(ByVal exporter As EnumExportTo)
        Try
            LoadData()
            Dim arrHeader As List(Of String) = New List(Of String)()
            Dim strtemp As String = "Date Range : " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtTodate.Value, "dd/MM/yyyy")
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
                arrHeader.Add("Customer Class : " + strtemp)
            End If
            If chkChkSelect.IsChecked Then
                strtemp = ""
                For Each Str As String In cbgCustomer.CheckedDisplayMember
                    If clsCommon.myLen(strtemp) > 0 Then
                        strtemp += ", "
                    End If
                    strtemp += Str
                Next
                arrHeader.Add("Customer : " + strtemp)
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

            If chkItemSelect1.IsChecked Then
                strtemp = ""
                For Each Str As String In cbgItem1.CheckedDisplayMember
                    If clsCommon.myLen(strtemp) > 0 Then
                        strtemp += ", "
                    End If
                    strtemp += Str
                Next
                arrHeader.Add("Item : " + strtemp)
            End If
            If rbtnCompanySelect.IsChecked Then
                strtemp = ""
                For Each Str As String In cbgCompany.CheckedDisplayMember
                    If clsCommon.myLen(strtemp) > 0 Then
                        strtemp += ", "
                    End If
                    strtemp += Str
                Next
                arrHeader.Add("Company : " + strtemp)
            End If
            ' clsCommon.MyExportToExcel("Cash Discount" + IIf(rdbDetail.IsChecked, " ( Detail ) ", " ( Summary ) "), gv1, arrHeader, Me.Text)


            If exporter = EnumExportTo.Excel Then
                clsCommon.MyExportToExcel("Cash Discount" + IIf(rdbDetail.IsChecked, " ( Detail ) ", " ( Summary ) "), gv1, arrHeader, Me.Text)
            Else
                clsCommon.MyExportToPDF("Cash Discount" + IIf(rdbDetail.IsChecked, " ( Detail ) ", " ( Summary ) "), gv1, arrHeader, "Cash Discount Report", True)
            End If


        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcel.Click
        printdata(EnumExportTo.Excel)
    End Sub

    Private Sub btnPDF_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPDF.Click
        printdata(EnumExportTo.PDF)
    End Sub

End Class
