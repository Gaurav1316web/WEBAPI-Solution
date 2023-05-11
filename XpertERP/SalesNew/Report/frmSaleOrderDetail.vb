'--------------------------------Last modify By - Priti ------------------------------------
'--------------------------------Last modify date - 23/05/2013-------------------------------------
'--------------------------------Last modify Time - 03:55 PM -------------------------------------
'--------------------------------Ticket No-BM00000000580  Changes by- Dipti Waila -------------------------------------
'--------------------------------Ticket No-BM00000000767  Changes by- Shipra Jain -------------------------------------

'' Anubhooti(2-July-2014) Added Export Permission Against BM00000003016 ''''''''
' Puran (13/July/2014) Ticket No- BM00000003399

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
Public Class frmSaleOrderDetail
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim dt As DataTable = Nothing

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmSaleOrderDetail)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        '' Anubhooti(2-July-2014) Added Export Permission Against BM00000003016 ''''''''
        btnExport.Visible = MyBase.isExport
    End Sub

    Private Sub FrmSaleOrderReport_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        SetUserMgmtNew()
        fromDate.Value = clsCommon.GETSERVERDATE()
        ToDate.Value = clsCommon.GETSERVERDATE()
        rbtnCategoryAll.IsChecked = True
        rbtncatall.IsChecked = True
        rbtnitemall.IsChecked = True
        rbtnlocall.IsChecked = True

        LoadCategory()
        LoadCustomer()
        LoadDocType()
        rdbAll.IsChecked = True
        LoadLocation()
        LoadItem()
    End Sub
    Sub LoadCategory()
        Dim qry As String = "select Code,Name,Parent from ("
        qry += " select ITEM_CATEGORY_STRUCT_CODE as Code,DESCRIPTION as Name, null as Parent,0 as Sno from TSPL_ITEM_CATEGORY_STRUCTURE"
        qry += " union all"
        qry += " select TSPL_ITEM_CATEGORY_STRUCT_DETAIL.ITEM_CATEGORY_CODE as Code,TSPL_ITEM_CATEGORY_LEVEL.DESCRIPTION as Name,ITEM_CATEGORY_STRUCT_CODE as Parent,TSPL_ITEM_CATEGORY_STRUCT_DETAIL.CATEGORY_LEVEL as SNo from TSPL_ITEM_CATEGORY_STRUCT_DETAIL"
        qry += " left outer join TSPL_ITEM_CATEGORY_LEVEL on TSPL_ITEM_CATEGORY_LEVEL.ITEM_CATEGORY_CODE=TSPL_ITEM_CATEGORY_STRUCT_DETAIL.ITEM_CATEGORY_CODE"
        qry += " Union all"
        qry += " select CODE,DESCRIPTION as Name,ITEM_CATEGORY_CODE as Parent,100 as SNo from TSPL_ITEM_CATEGORY_LEVEL_VALUES"
        qry += " )xxx order by Sno"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        tvCategory.DataSource = Nothing
        tvCategory.TreeViewElement.AutoSizeItems = True
        tvCategory.ShowLines = True
        tvCategory.ShowRootLines = True
        tvCategory.TreeViewElement.ViewElement.Margin = New Padding(4)
        tvCategory.ShowExpandCollapse = True
        tvCategory.TreeIndent = 15
        tvCategory.FullRowSelect = False
        tvCategory.ShowLines = True
        tvCategory.LineStyle = TreeLineStyle.Dot
        tvCategory.LineColor = Color.FromArgb(110, 153, 210)
        tvCategory.ExpandAnimation = ExpandAnimation.Opacity
        tvCategory.AllowEdit = False
        tvCategory.ShowRootLines = False
        tvCategory.TreeViewElement.AllowAlternatingRowColor = True
        tvCategory.TreeViewElement.AlternatingRowColor = Color.AliceBlue

        tvCategory.TreeViewElement.DrawBorder = True
        tvCategory.ValueMember = "Code"
        tvCategory.DisplayMember = "Name"
        tvCategory.ChildMember = "Code"
        tvCategory.ParentMember = "Parent"
        tvCategory.DataSource = dt
        tvCategory.CheckBoxes = True

        tvCategory.ExpandAll()
    End Sub

    Sub LoadCustomer()
        Dim strquery As String = "select cust_code as [Customer Code], Customer_Name as [Customer Name] from tspl_customer_master where 2=2 "
        cbgCustomer.DataSource = clsDBFuncationality.GetDataTable(strquery)
        cbgCustomer.ValueMember = "Customer Code"
        cbgCustomer.DisplayMember = "Customer Name"
    End Sub
    Sub LoadLocation()
        Dim qry As String = "select Location_Code as Location,Location_Desc as [Location Description] from TSPL_LOCATION_MASTER where Location_Type='Physical' "
        cbgLocation.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgLocation.ValueMember = "Location"
        cbgLocation.DisplayMember = "Location Description"
    End Sub
    Sub LoadItem()
        Dim qry As String = " select item_code ,item_Desc  from TSPL_ITEM_MASTER order by Item_Code "
        cbgItem.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgItem.ValueMember = "item_code"
        cbgItem.DisplayMember = "item_Desc"
    End Sub
    Sub LoadDocType()
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow = dt.NewRow()
        dr("Code") = "Complete"
        dr("Name") = "Complete"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Pending"
        dr("Name") = "Pending"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Partial Shipped"
        dr("Name") = "Partial Shipped"
        dt.Rows.Add(dr)

        'dr = dt.NewRow()
        'dr("Code") = "Sale"
        'dr("Name") = "Sale"
        'dt.Rows.Add(dr)

        ddlStatus.DataSource = dt
        ddlStatus.ValueMember = "Code"
        ddlStatus.DisplayMember = "Name"
    End Sub
    Enum Exporter
        Excel = 0
        PDF = 1
        Print = 2
        Refresh = 3
    End Enum
    Sub LoadData(ByVal IsPrint As Exporter)
        Dim strCustomer As String = ""
        Dim shipcustomer As String = ""
        Dim strStatus As String = ""
        ' Dim strFromdate, strTodate As String
        Dim strLocationInv As String = ""
        Dim strlocship As String = ""
        Dim strItemOrder As String = ""
        Dim strItemShip As String = ""
        Dim strclosed As String = ""
        'gv.DataSource = Nothing
        'gv.Rows.Clear()

        If rbtncatslct.IsChecked AndAlso cbgCustomer.CheckedValue.Count = 0 Then
            clsCommon.MyMessageBoxShow("Please Select Atleast One Customer")
            Return
        End If

        If rbtnlocslct.IsChecked AndAlso cbgLocation.CheckedValue.Count = 0 Then
            clsCommon.MyMessageBoxShow("Please Select Atleast One Location")
            Return
        End If

        If rbtnitemslct.IsChecked AndAlso cbgItem.CheckedValue.Count = 0 Then
            clsCommon.MyMessageBoxShow("Please Select Atleast One Item")
            Return
        End If

        If chkclosed.Checked Then
            strclosed = " and TSPL_SD_SALES_ORDER_HEAD.Close_YN='Y'"
        Else
            strclosed = " and TSPL_SD_SALES_ORDER_HEAD.Close_YN='N'"
        End If

        If rdbAll.IsChecked Then
            strStatus = "All"
        ElseIf rdbpending.IsChecked Then
            strStatus = "Pending"
        ElseIf rdbPartial.IsChecked Then
            strStatus = "Partial Shipped"
        ElseIf rdbComplete.IsChecked Then
            strStatus = "Complete"
        End If
        If IsPrint = Exporter.Print OrElse IsPrint = Exporter.Refresh OrElse IsPrint = Exporter.Excel Then
            If rbtncatslct.IsChecked AndAlso cbgCustomer.CheckedValue.Count > 0 Then
                strCustomer += " and TSPL_SD_SALES_ORDER_HEAD.Customer_Code in (" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ") "
                shipcustomer += " and TSPL_SD_SHIPMENT_HEAD.customer_code in (" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ") "
            Else
                strCustomer = ""
                shipcustomer = ""
            End If

            If rbtnitemslct.IsChecked AndAlso cbgItem.CheckedValue.Count > 0 Then
                strItemOrder += " and  TSPL_SD_SALES_ORDER_DETAIL.Item_Code in (" + clsCommon.GetMulcallString(cbgItem.CheckedValue) + ") "
                strItemShip += " and  TSPL_SD_SHIPMENT_Detail.Item_Code in (" + clsCommon.GetMulcallString(cbgItem.CheckedValue) + ") "
            Else
                strItemShip = ""
                strItemOrder = ""

            End If

            If rbtnlocslct.IsChecked AndAlso cbgLocation.CheckedValue.Count > 0 Then
                strLocationInv += " and  TSPL_SD_SALES_ORDER_HEAD.Bill_To_Location in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
                strlocship += " and TSPL_SD_SHIPMENT_HEAD.Bill_To_Location in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")"
            Else
                strLocationInv = ""
                strlocship = ""
            End If
            Dim str As String = "select '" & fromDate.Value & "' as Fdate,'" & ToDate.Value & "' as Tdate,'" & strStatus & "' as status, " & _
            "Document_Code,Document_Date,max(xx.Parent_Customer_No)as ParentCode,MAX(Parent_Master.Customer_Name) as ParentName,Customer_Code,xx.Customer_Name,Salesman_Name,Bill_To_Location ,Location_Desc,Delivery_date, " & _
            "Item_Code,Item_Desc,sum(OrderQty) as OrderQty,sum(ShippedQty) as ShippedQty,sum(OrderQty-ShippedQty) as Outstanding, " & _
            "Unit_code,max(rate) as rate,sum(Amount) as Amount,max(Disc_Per) as Disc_Per,sum(Disc_Amt) as Disc_Amt, " & _
            "sum(Amt_Less_Discount) as Amt_Less_Discount,sum(Total_Tax_Amt) as Total_Tax_Amt,sum(TotalOrderAmt) as TotalOrderAmt, " & _
            "sum(totalShipAmt) as TotalShippedAmt from " & _
            "(SELECT     TSPL_SD_SALES_ORDER_HEAD.Document_Code, TSPL_SD_SALES_ORDER_HEAD.Document_Date, " & _
                          "TSPL_CUSTOMER_MASTER.Parent_Customer_No,TSPL_SD_SALES_ORDER_HEAD.Customer_Code, TSPL_CUSTOMER_MASTER.Customer_Name, TSPL_SD_SALES_ORDER_HEAD.Salesman_Code, " & _
                          "TSPL_SD_SALES_ORDER_HEAD.Salesman_Name,TSPL_SD_SALES_ORDER_HEAD.Bill_To_Location ,TSPL_LOCATION_MASTER.Location_Desc, TSPL_SD_SALES_ORDER_HEAD.Delivery_date, TSPL_SD_SALES_ORDER_DETAIL.Item_Code, " & _
                         " TSPL_ITEM_MASTER.Item_Desc, TSPL_SD_SALES_ORDER_DETAIL.Qty as OrderQty, 0 as ShippedQty,TSPL_SD_SALES_ORDER_DETAIL.Unit_code, " & _
                         " Item_Cost as rate,Amount,Disc_Per,Disc_Amt,TSPL_SD_SALES_ORDER_DETAIL.Amt_Less_Discount,TSPL_SD_SALES_ORDER_DETAIL.Total_Tax_Amt, " & _
                         " TSPL_SD_SALES_ORDER_DETAIL.Item_Net_Amt as TotalOrderAmt,0 as totalShipAmt FROM TSPL_CUSTOMER_MASTER RIGHT OUTER JOIN " & _
                         " TSPL_SD_SALES_ORDER_HEAD ON TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_SD_SALES_ORDER_HEAD.Customer_Code RIGHT OUTER JOIN " & _
                         " TSPL_ITEM_MASTER RIGHT OUTER JOIN " & _
                         " TSPL_SD_SALES_ORDER_DETAIL ON TSPL_ITEM_MASTER.Item_Code = TSPL_SD_SALES_ORDER_DETAIL.Item_Code ON  " & _
                         " TSPL_SD_SALES_ORDER_HEAD.Document_Code = TSPL_SD_SALES_ORDER_DETAIL.Document_Code " & _
                              "     left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_SD_SALES_ORDER_HEAD.Bill_To_Location " & _
" where convert(date,TSPL_SD_SALES_ORDER_HEAD.Document_Date,103) >= '" & clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") & "' and " & _
                         " convert(date,TSPL_SD_SALES_ORDER_HEAD.Document_Date,103) <= '" & clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") & "'  " & strCustomer & " " & strLocationInv & " " & strItemOrder & " " & strclosed & "" & _
                         " union all " & _
            "SELECT     TSPL_SD_SHIPMENT_HEAD.Against_Sales_Order AS Document_Code, TSPL_SD_SALES_ORDER_HEAD.Document_Date, " & _
                          "TSPL_CUSTOMER_MASTER.Parent_Customer_No,TSPL_SD_SALES_ORDER_HEAD.Customer_Code, TSPL_CUSTOMER_MASTER.Customer_Name, TSPL_SD_SALES_ORDER_HEAD.Salesman_Code, " & _
                          "TSPL_SD_SHIPMENT_HEAD.Salesman_Name,TSPL_SD_SHIPMENT_HEAD.Bill_To_Location ,TSPL_LOCATION_MASTER.Location_Desc, TSPL_SD_SALES_ORDER_HEAD.Delivery_date, TSPL_SD_SHIPMENT_DETAIL.Item_Code, " & _
                         " TSPL_ITEM_MASTER.Item_Desc,0as OrderQty,  TSPL_SD_SHIPMENT_DETAIL.Qty  as ShippedQty,TSPL_SD_SHIPMENT_DETAIL.Unit_code, " & _
                         " Item_Cost as rate,0 as amount,0 as Disc_Per,0 as Disc_Amt,0 as Amt_Less_Discount,0 as Total_Tax_Amt, " & _
                         " 0 as TotalOrderAmt,TSPL_SD_SHIPMENT_DETAIL.Item_Net_Amt as totalShipAmt " & _
                         " FROM TSPL_ITEM_MASTER RIGHT OUTER JOIN " & _
                         " TSPL_SD_SHIPMENT_DETAIL ON TSPL_ITEM_MASTER.Item_Code = TSPL_SD_SHIPMENT_DETAIL.Item_Code RIGHT OUTER JOIN " & _
                         " TSPL_SD_SALES_ORDER_HEAD INNER JOIN " & _
                         " TSPL_SD_SHIPMENT_HEAD ON TSPL_SD_SALES_ORDER_HEAD.Document_Code = TSPL_SD_SHIPMENT_HEAD.Against_Sales_Order ON  " & _
                         " TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE = TSPL_SD_SHIPMENT_HEAD.Document_Code LEFT OUTER JOIN " & _
                         " TSPL_CUSTOMER_MASTER ON TSPL_SD_SALES_ORDER_HEAD.Customer_Code = TSPL_CUSTOMER_MASTER.Cust_Code " & _
          "  left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_SD_SHIPMENT_HEAD.Bill_To_Location " & _
" where convert(date,TSPL_SD_SALES_ORDER_HEAD.Document_Date,103) >= '" & clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") & "' and " & _
                         " convert(date,TSPL_SD_SALES_ORDER_HEAD.Document_Date,103) <= '" & clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") & "'   " & shipcustomer & "   " & strlocship & " " & strItemShip & " " & strclosed & " )  " & _
                         " xx left outer join TSPL_CUSTOMER_MASTER as Parent_Master on Parent_Master.Cust_Code=xx.Parent_Customer_No group by Item_Code,Document_Code,Document_Date,xx.Customer_Name,Customer_Code,Salesman_Name,Bill_To_Location,Location_Desc ,Delivery_date, " & _
                         " Item_Code,Item_Desc,Unit_code "

            If rdbAll.IsChecked Then

            ElseIf rdbpending.IsChecked Then
                str = str & " having SUM(OrderQty) = sum(OrderQty-ShippedQty)"
            ElseIf rdbPartial.IsChecked Then
                str = str & " having SUM(OrderQty) > sum(OrderQty-ShippedQty) and sum(OrderQty-ShippedQty) <> 0"
            ElseIf rdbComplete.IsChecked Then
                str = str & " having sum(OrderQty-ShippedQty)=0"

            End If


            '*********************************************************************************
            Dim qry As String
            qry = "select TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code as [MainGroupCode],  TSPL_ITEM_CATEGORY_LEVEL.description as [Main Group],TSPL_ITEM_MASTER_CATEGORY.item_cagetory_values as [GroupCode], TSPL_ITEM_CATEGORY_LEVEL_VALUES.description as [Group Name],a.* from (" + str + ")a left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.item_code=a.item_code LEFT OUTER JOIN TSPL_ITEM_MASTER_CATEGORY ON TSPL_ITEM_MASTER_CATEGORY.item_code=  TSPL_ITEM_MASTER.item_code LEFT OUTER JOIN TSPL_ITEM_CATEGORY_LEVEL ON TSPL_ITEM_CATEGORY_LEVEL.item_category_code= TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code LEFT OUTER JOIN TSPL_ITEM_CATEGORY_LEVEL_VALUES ON TSPL_ITEM_CATEGORY_LEVEL_VALUES.item_category_code= TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code and TSPL_ITEM_CATEGORY_LEVEL_VALUES.code= TSPL_ITEM_MASTER_CATEGORY.item_cagetory_values"
            Dim whrcate As String = ""
            If rbtnCategorySelect.IsChecked Then
                Dim isFirstTime As Boolean = True
                qry = "select xxa.* from (" + qry + ")xxa where exists (select 1  from TSPL_ITEM_MASTER_CATEGORY where Item_code in (select distinct item_code from (" + str + ")a1) and ( " + Environment.NewLine
                For Each Ctr As RadTreeNode In tvCategory.CheckedNodes
                    If (Ctr.Checked) And Ctr.Parent IsNot Nothing Then
                        If Not isFirstTime Then
                            qry += " or "
                            whrcate += " or "
                        End If
                        qry += " ( maingroupcode='" + clsCommon.myCstr(Ctr.Parent.Value) + "' and groupcode='" + clsCommon.myCstr(Ctr.Value) + "' )" + Environment.NewLine
                        whrcate += " ( TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code='" + clsCommon.myCstr(Ctr.Parent.Value) + "' and TSPL_ITEM_MASTER_CATEGORY.item_cagetory_values='" + clsCommon.myCstr(Ctr.Value) + "' )" + Environment.NewLine
                        isFirstTime = False
                    End If
                Next
                qry += " ))"
                If isFirstTime Then
                    Throw New Exception("Please select at least one Category")
                    Return
                End If
            End If
            '***************************************************************************************

            If clsCommon.myLen(whrcate) > 0 Then
                whrcate = " and " + whrcate
            End If

            qry = "select distinct Fdate,Tdate,Status,Document_Code,Document_Date,asss.ParentCode,ParentName,Customer_Code,Customer_Name,Salesman_Name,Bill_To_Location,Location_Desc,Delivery_Date,Item_Code,Item_Desc,OrderQty,ShippedQty,Outstanding,Unit_Code,Rate,Amount,Disc_per,Disc_Amt,Amt_Less_Discount,Total_Tax_Amt,Totalorderamt,totalshippedamt from (" + qry + ")asss"
            str = qry

            qry = "select axa.*,(select distinct (select ','+TSPL_ITEM_CATEGORY_LEVEL.description, ','+TSPL_ITEM_CATEGORY_LEVEL_VALUES.description from TSPL_ITEM_MASTER_CATEGORY left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER_CATEGORY.Item_code=TSPL_ITEM_MASTER.Item_Code left outer join TSPL_ITEM_CATEGORY_LEVEL on TSPL_ITEM_CATEGORY_LEVEL.ITEM_CATEGORY_CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code left outer join TSPL_ITEM_CATEGORY_LEVEL_VALUES on TSPL_ITEM_CATEGORY_LEVEL_VALUES.CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values and TSPL_ITEM_CATEGORY_LEVEL_VALUES.ITEM_CATEGORY_CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code where TSPL_ITEM_MASTER_CATEGORY.Item_code=axa.Item_Code " + whrcate + " for XML path(''))) as Category from (" + str + ")axa"
            'qry = qry + " order by document_code"

            dt = clsDBFuncationality.GetDataTable(qry)
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("No Record Found")
            ElseIf IsPrint = Exporter.Print Then
                Dim frmCRV As New frmCrystalReportViewer()
                frmCRV.funreport(CrystalReportFolder.NewSalesReports, dt, "crptSaleOrderDetail", "Sale Order Report")
                frmCRV = Nothing
            ElseIf IsPrint = Exporter.Excel Then
                transportSql.ExporttoExcel(qry, Me)
            Else
                gv.DataSource = dt
                SetGridFormation()
                RadPageView1.SelectedPage = RadPageViewPage2
            End If
        ElseIf IsPrint = Exporter.Excel Then
            clsCommon.MyExportToExcel("Sale Order Detail", gv, Nothing, Me.Text)
        Else
            clsCommon.MyExportToPDF(Me.Text, gv, Nothing, Me.Text)
        End If
    End Sub
    Sub SetGridFormation()
        gv.DataSource = Nothing
        gv.Columns.Clear()
        gv.Rows.Clear()
        gv.DataSource = dt
        gv.AllowAddNewRow = False
        gv.AllowDragToGroup = False
        For ii As Integer = 0 To gv.Columns.Count - 1
            gv.Columns(ii).ReadOnly = True
            gv.Columns(ii).IsVisible = False
        Next


        gv.Columns("Fdate").IsVisible = True
        gv.Columns("Fdate").Width = 100
        gv.Columns("Fdate").HeaderText = "From Date"

        gv.Columns("Tdate").IsVisible = True
        gv.Columns("Tdate").Width = 100
        gv.Columns("Tdate").HeaderText = "To date"

        gv.Columns("status").IsVisible = True
        gv.Columns("status").Width = 70
        gv.Columns("status").HeaderText = "Status"

        gv.Columns("Document_Code").IsVisible = True
        gv.Columns("Document_Code").Width = 100
        gv.Columns("Document_Code").HeaderText = "Document Code"

        gv.Columns("Document_Date").IsVisible = True
        gv.Columns("Document_Date").Width = 100
        gv.Columns("Document_Date").HeaderText = "Document Date"

        gv.Columns("ParentCode").IsVisible = True
        gv.Columns("ParentCode").Width = 100
        gv.Columns("ParentCode").HeaderText = "Parent Customer Code"

        gv.Columns("ParentName").IsVisible = True
        gv.Columns("ParentName").Width = 200
        gv.Columns("ParentName").HeaderText = "Parent Name"

        gv.Columns("Customer_Code").IsVisible = True
        gv.Columns("Customer_Code").Width = 100
        gv.Columns("Customer_Code").HeaderText = "Customer Code"

        gv.Columns("Customer_Name").IsVisible = True
        gv.Columns("Customer_Name").Width = 200
        gv.Columns("Customer_Name").HeaderText = "Customer Name"

        gv.Columns("Salesman_Name").IsVisible = True
        gv.Columns("Salesman_Name").Width = 100
        gv.Columns("Salesman_Name").HeaderText = "Salesman Name"

        gv.Columns("Bill_To_Location").IsVisible = True
        gv.Columns("Bill_To_Location").Width = 100
        gv.Columns("Bill_To_Location").HeaderText = "Location Code"

        gv.Columns("Location_Desc").IsVisible = True
        gv.Columns("Location_Desc").Width = 120
        gv.Columns("Location_Desc").HeaderText = "Location Desc"

        gv.Columns("Delivery_date").IsVisible = True
        gv.Columns("Delivery_date").Width = 100
        gv.Columns("Delivery_date").HeaderText = "Delivery Date"

        gv.Columns("Item_Code").IsVisible = True
        gv.Columns("Item_Code").Width = 100
        gv.Columns("Item_Code").HeaderText = "Item Code"

        gv.Columns("Item_Desc").IsVisible = True
        gv.Columns("Item_Desc").Width = 200
        gv.Columns("Item_Desc").HeaderText = "Item Description"

        gv.Columns("OrderQty").IsVisible = True
        gv.Columns("OrderQty").Width = 100
        gv.Columns("OrderQty").HeaderText = "Order Qty"

        gv.Columns("ShippedQty").IsVisible = True
        gv.Columns("ShippedQty").Width = 100
        gv.Columns("ShippedQty").HeaderText = "Shipped Qty"

        gv.Columns("Outstanding").IsVisible = True
        gv.Columns("Outstanding").Width = 100
        gv.Columns("Outstanding").HeaderText = "Outstanding"

        gv.Columns("Unit_code").IsVisible = True
        gv.Columns("Unit_code").Width = 100
        gv.Columns("Unit_code").HeaderText = "Unit Code"

        gv.Columns("rate").IsVisible = True
        gv.Columns("rate").Width = 100
        gv.Columns("rate").HeaderText = "Rate"

        gv.Columns("Amount").IsVisible = True
        gv.Columns("Amount").Width = 100
        gv.Columns("Amount").HeaderText = "Amount"

        gv.Columns("Disc_Per").IsVisible = True
        gv.Columns("Disc_Per").Width = 100
        gv.Columns("Disc_Per").HeaderText = "Discount %"

        gv.Columns("Disc_Amt").IsVisible = True
        gv.Columns("Disc_Amt").Width = 100
        gv.Columns("Disc_Amt").HeaderText = "Discount Amt"

        gv.Columns("Amt_Less_Discount").IsVisible = True
        gv.Columns("Amt_Less_Discount").Width = 100
        gv.Columns("Amt_Less_Discount").HeaderText = "Amt Less Discount"


        gv.Columns("Total_Tax_Amt").IsVisible = True
        gv.Columns("Total_Tax_Amt").Width = 100
        gv.Columns("Total_Tax_Amt").HeaderText = "Total Tax Amt"


        gv.Columns("TotalOrderAmt").IsVisible = True
        gv.Columns("TotalOrderAmt").Width = 100
        gv.Columns("TotalOrderAmt").HeaderText = "Total Order Amt"

        gv.Columns("TotalShippedAmt").IsVisible = True
        gv.Columns("TotalShippedAmt").Width = 100
        gv.Columns("TotalShippedAmt").HeaderText = "Total Shipped Amt"

        Try
            gv.Columns("Category").IsVisible = True
            gv.Columns("Category").Width = 250
            gv.Columns("Category").HeaderText = "Item Category"
        Catch ex As Exception
        End Try

    End Sub
    Private Sub RadButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint1.Click
        LoadData(Exporter.Print)
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles RadButton2.Click
        rdbAll.IsChecked = True
        rbtnCategoryAll.IsChecked = True
        LoadCategory()
    End Sub

    Private Sub btnExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcel.Click
        'If (gv.Rows.Count <= 0) Then
        '    common.clsCommon.MyMessageBoxShow("No Data To Export")
        '    Exit Sub
        'End If
        LoadData(Exporter.Excel)
    End Sub

    Private Sub btnPdf_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPdf.Click
        If (gv.Rows.Count <= 0) Then
            common.clsCommon.MyMessageBoxShow("No Data To Export")
            Exit Sub
        End If
        LoadData(Exporter.PDF)
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        LoadData(Exporter.Refresh)
    End Sub

    Private Sub rbtnCategoryAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtnCategoryAll.ToggleStateChanged, rbtnCategorySelect.ToggleStateChanged
        tvCategory.Enabled = rbtnCategorySelect.IsChecked
    End Sub

    Private Sub rbtncatall_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtncatall.ToggleStateChanged, rbtncatslct.ToggleStateChanged
        cbgCustomer.Enabled = rbtncatslct.IsChecked
    End Sub

    Private Sub rbtnitemall_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtnitemall.ToggleStateChanged, rbtnitemslct.ToggleStateChanged
        cbgItem.Enabled = rbtnitemslct.IsChecked
    End Sub

    Private Sub rbtnlocall_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtnlocall.ToggleStateChanged, rbtnlocslct.ToggleStateChanged
        cbgLocation.Enabled = rbtnlocslct.IsChecked
    End Sub

    Private Sub gv_CellDoubleClick(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv.CellDoubleClick
        If gv.Rows.Count > 0 Then
            Dim strDoc = gv.CurrentRow.Cells("Document_Code").Value
            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmSNSalesOrder, strDoc)
        End If
    End Sub

End Class
