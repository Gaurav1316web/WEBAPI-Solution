Imports common
'--------------------------------Ticket No-BM00000000767  Changes by- Shipra Jain -------------------------------------
'' Anubhooti(2-July-2014) Added Export Permission Against BM00000003016 ''''''''

Public Class FrmOrdertracking
    Inherits FrmMainTranScreen
#Region "Functions"
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.ORDNEW)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
        '' Anubhooti(2-July-2014) Added Export Permission Against BM00000003016 ''''''''
        btnExport.Visible = MyBase.isExport
    End Sub
#End Region
    Private Sub FrmOrdertracking_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        fromDate.Value = clsCommon.GETSERVERDATE()
        ToDate.Value = clsCommon.GETSERVERDATE()
        rbtnCategoryAll.IsChecked = True
        rbtncustall.IsChecked = True
        rbtnitemall.IsChecked = True
        rbtnlocall.IsChecked = True

        LoadCategory()
        LoadCustomer()
        LoadLocation()
        LoadItem()
        rdbSummary.IsChecked = True
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
        Dim strquery As String = "select cust_code as [Customer Code], Customer_Name as [Customer Name] from tspl_customer_master where 2=2 and Parent_Customer_YN='N'"
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


    Private Sub btnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnprint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnprint.Click
        Print()
    End Sub

    Sub Print()
        Dim strCustomerInv As String = ""
        Dim strCustomerRet As String = ""
        Dim strLocationInv As String = ""
        Dim strItemInv As String = ""
        Dim strItemRet As String = ""
        Dim strLocationRet As String = ""

        '-----------------------Monika 29/04/2014------------------------------
        If rbtncustslct.IsChecked AndAlso cbgCustomer.CheckedValue.Count = 0 Then
            clsCommon.MyMessageBoxShow("Please Select Atleast One Customer", Me.Text)
            Return
        End If

        If rbtnlocslct.IsChecked AndAlso cbgLocation.CheckedValue.Count = 0 Then
            clsCommon.MyMessageBoxShow("Please Select Atleast One Location", Me.Text)
            Return
        End If

        If rbtnitemslct.IsChecked AndAlso cbgItem.CheckedValue.Count = 0 Then
            clsCommon.MyMessageBoxShow("Please Select Atleast One Item", Me.Text)
            Return
        End If


        If rdbDetail.IsChecked Then
            If rbtnitemslct.IsChecked AndAlso cbgItem.CheckedValue.Count > 0 Then
                strItemInv += " and  aa.Item_Code in (" + clsCommon.GetMulcallString(cbgItem.CheckedValue) + ") "
            Else
                strItemInv = ""
            End If
        End If

        If rbtnlocslct.IsChecked AndAlso cbgLocation.CheckedValue.Count > 0 Then
            strLocationInv += " and  aa.Bill_To_Location in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
        Else
            strLocationInv = ""
        End If
        If rbtncustslct.IsChecked AndAlso cbgCustomer.CheckedValue.Count > 0 Then
            strCustomerInv += " and aa.Customer_Code in (" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ") "
        Else
            strCustomerInv = ""
        End If

        Dim str As String = "select distinct TSPL_SD_SALES_ORDER_HEAD.Salesman_Code,Salesman_Desc,Item_Code, " & _
        "TSPL_SD_SALES_ORDER_HEAD.Document_Code,Document_Date,Parent_Customer_No, Customer_Code,Customer_Name,Bill_To_Location, TSPL_LOCATION_MASTER.Location_Desc," & _
        "Qty as OrderQty,0 as ShipQty,0 as InvQty from TSPL_SD_SALES_ORDER_HEAD left outer join " & _
        "TSPL_SD_SALES_ORDER_DETAIL on TSPL_SD_SALES_ORDER_HEAD.Document_Code=TSPL_SD_SALES_ORDER_DETAIL.Document_Code left outer join " & _
        "TSPL_CUSTOMER_MASTER on TSPL_SD_SALES_ORDER_HEAD.Customer_Code=TSPL_CUSTOMER_MASTER.Cust_Code " & _
       " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_SD_SALES_ORDER_HEAD.Bill_To_Location " & _
"union all " & _
        "select TSPL_SD_SALES_ORDER_HEAD.Salesman_Code,Salesman_Desc,Item_Code,Against_Sales_Order,TSPL_SD_SALES_ORDER_HEAD.Document_Date, " & _
        "Parent_Customer_No,TSPL_SD_SALES_ORDER_HEAD.Customer_Code,Customer_Name,TSPL_SD_SHIPMENT_HEAD.Bill_To_Location, TSPL_LOCATION_MASTER.Location_Desc,0 as OrderQty,Qty as ShipQty, " & _
        "0 as InvQty from TSPL_SD_SHIPMENT_HEAD left outer join TSPL_SD_SHIPMENT_DETAIL on " & _
        "TSPL_SD_SHIPMENT_HEAD.Document_Code=TSPL_SD_SHIPMENT_DETAIL.Document_Code left outer join TSPL_SD_SALES_ORDER_HEAD on " & _
        "TSPL_SD_SHIPMENT_HEAD.Against_Sales_Order=TSPL_SD_SALES_ORDER_HEAD.Document_Code left outer join TSPL_CUSTOMER_MASTER on  " & _
        "TSPL_SD_SALES_ORDER_HEAD.Customer_Code=TSPL_CUSTOMER_MASTER.Cust_Code " & _
               " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_SD_SALES_ORDER_HEAD.Bill_To_Location " & _
"union all " & _
        "select TSPL_SD_SALES_ORDER_HEAD.Salesman_Code,Salesman_Desc,Item_Code,Against_Sales_Order,TSPL_SD_SALES_ORDER_HEAD.Document_Date, " & _
        "Parent_Customer_No,TSPL_SD_SALES_ORDER_HEAD.Customer_Code,Customer_Name,TSPL_SD_SHIPMENT_HEAD.Bill_To_Location, TSPL_LOCATION_MASTER.Location_Desc,0 as OrderQty,0 as ShipQty,Qty as InvQty " & _
        "from TSPL_SD_SALE_INVOICE_HEAD left outer join TSPL_SD_SALE_INVOICE_DETAIL on " & _
        "TSPL_SD_SALE_INVOICE_HEAD.Document_Code=TSPL_SD_SALE_INVOICE_DETAIL.Document_Code left outer join " & _
        "TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Against_Shipment_No=TSPL_SD_SHIPMENT_HEAD.Document_Code left outer join " & _
        "TSPL_SD_SALES_ORDER_HEAD on TSPL_SD_SHIPMENT_HEAD.Against_Sales_Order=TSPL_SD_SALES_ORDER_HEAD.Document_Code left outer join  " & _
        "TSPL_CUSTOMER_MASTER on TSPL_SD_SALES_ORDER_HEAD.Customer_Code=TSPL_CUSTOMER_MASTER.Cust_Code  " & _
                       " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_SD_SALES_ORDER_HEAD.Bill_To_Location "

        '****************************************************************************************
        Dim qry As String
        Dim whrcate As String = ""
        qry = "select distinct Salesman_Code,Salesman_Desc,Item_Code,Document_Code,Document_Date,Parent_Customer_No,Customer_Code,Customer_Name,Bill_To_Location,Location_Desc,OrderQty,ShipQty,InvQty from (select TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code as [MainGroupCode],  TSPL_ITEM_CATEGORY_LEVEL.description as [Main Group],TSPL_ITEM_MASTER_CATEGORY.item_cagetory_values as [GroupCode], TSPL_ITEM_CATEGORY_LEVEL_VALUES.description as [Group Name],aaa.* from (" + str + ")aaa left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.item_code=aaa.item_code LEFT OUTER JOIN TSPL_ITEM_MASTER_CATEGORY ON TSPL_ITEM_MASTER_CATEGORY.item_code=  TSPL_ITEM_MASTER.item_code LEFT OUTER JOIN TSPL_ITEM_CATEGORY_LEVEL ON TSPL_ITEM_CATEGORY_LEVEL.item_category_code= TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code LEFT OUTER JOIN TSPL_ITEM_CATEGORY_LEVEL_VALUES ON TSPL_ITEM_CATEGORY_LEVEL_VALUES.item_category_code= TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code and TSPL_ITEM_CATEGORY_LEVEL_VALUES.code= TSPL_ITEM_MASTER_CATEGORY.item_cagetory_values)ass"

        If rbtnCategorySelect.IsChecked Then
            Dim isFirstTime As Boolean = True
            qry += " where exists (select 1  from TSPL_ITEM_MASTER_CATEGORY where Item_code in (select distinct item_code from (" + str + ")a1) and ( " + Environment.NewLine
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

        '--------------------xml path for categories-------------------
        If clsCommon.myLen(whrcate) > 0 Then
            whrcate = " and " + whrcate
        End If

        str = qry
        qry = "select axa.*,( select distinct (select ','+TSPL_ITEM_CATEGORY_LEVEL.description, ','+TSPL_ITEM_CATEGORY_LEVEL_VALUES.description from TSPL_ITEM_MASTER_CATEGORY left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER_CATEGORY.Item_code=TSPL_ITEM_MASTER.Item_Code left outer join TSPL_ITEM_CATEGORY_LEVEL on TSPL_ITEM_CATEGORY_LEVEL.ITEM_CATEGORY_CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code left outer join TSPL_ITEM_CATEGORY_LEVEL_VALUES on TSPL_ITEM_CATEGORY_LEVEL_VALUES.CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values and TSPL_ITEM_CATEGORY_LEVEL_VALUES.ITEM_CATEGORY_CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code where TSPL_ITEM_MASTER_CATEGORY.Item_code=axa.Item_Code " + whrcate + " for XML path(''))) as Category from (" + qry + ")axa"
        '---------------------------------------------------------------

        If rdbSummary.IsChecked Then
            qry = " (select Document_Code,Document_Date,max(aa.Parent_Customer_No)as ParentCode,MAX(Parent_Master.Customer_Name) as ParentName,Customer_Code,aa.Customer_Name,Bill_To_Location as loc,Location_Desc,aa.Salesman_Code,aa.Salesman_Desc, " & _
            "SUM(OrderQty) as OrderQty,SUM(ShipQty) as  ShipQty , sum(InvQty) as InvQty,SUM(OrderQty)  - SUM(ShipQty)  as OutOrder,SUM(ShipQty) - sum(InvQty) as OutShip " & _
            "from ( " & qry & ") aa  left outer join TSPL_CUSTOMER_MASTER as Parent_Master on Parent_Master.Cust_Code=aa.Parent_Customer_No where convert(date,aa.Document_Date,103) >= '" & clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") & "' and  " & _
            "convert(date,aa.Document_Date,103) <= '" & clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") & "' " & strItemInv & " " & strLocationInv & "  " & strCustomerInv & " " & _
            "group by Document_Code,Document_Date,Customer_Code,aa.Customer_Name,Bill_To_Location,Location_Desc ,aa.Salesman_Code,aa.Salesman_Desc) " 'select Document_Code,Document_Date,Customer_Code,Customer_Name,Bill_To_Location as loc,Location_Desc,Salesman_Code,Salesman_Desc,sum(OrderQty) as OrderQty,sum(shipqty) as ShipQty,sum(invqty) as InvQty,sum(OutOrder) as OutOrder,sum(OutShip) as OutShip from query1 group by

        Else
            qry = "select Document_Code,Document_Date,max(aa.Parent_Customer_No)as ParentCode,MAX(Parent_Master.Customer_Name) as ParentName,Customer_Code,aa.Customer_Name,Bill_To_Location as loc,Location_Desc,aa.Salesman_Code,aa.Salesman_Desc,Item_Code, " & _
           "SUM(OrderQty) as OrderQty,SUM(ShipQty) as  ShipQty , sum(InvQty) as InvQty,SUM(OrderQty)  - SUM(ShipQty)  as OutOrder,SUM(ShipQty) - sum(InvQty) as OutShip,Category " & _
           "from ( " & qry & ") aa  left outer join TSPL_CUSTOMER_MASTER as Parent_Master on Parent_Master.Cust_Code=aa.Parent_Customer_No where convert(date,aa.Document_Date,103) >= '" & clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") & "' and  " & _
           "convert(date,aa.Document_Date,103) <= '" & clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") & "' " & strItemInv & " " & strLocationInv & "  " & strCustomerInv & " " & _
           "group by category,Document_Code,Document_Date,Customer_Code,aa.Customer_Name,Bill_To_Location ,Location_Desc,aa.Salesman_Code,aa.Salesman_Desc,Item_Code"
        End If
        qry += " order by Document_Code "



        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        Gv1.DataSource = Nothing
        Gv1.Columns.Clear()
        Gv1.Rows.Clear()
        Gv1.GroupDescriptors.Clear()
        Gv1.MasterTemplate.SummaryRowsBottom.Clear()
        Gv1.EnableFiltering = True

        If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow("No Data Found to Display", Me.Text)
            Exit Sub
        Else
            Gv1.DataSource = dt
            SetGridFormationOFGV1()
        End If

        Gv1.MasterTemplate.AllowAddNewRow = False
        RadPageView1.SelectedPage = RadPageViewPage2
    End Sub
    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        fromDate.Value = clsCommon.GETSERVERDATE()
        ToDate.Value = clsCommon.GETSERVERDATE()
        rbtnCategoryAll.IsChecked = True
        LoadCategory()
    End Sub

    Sub SetGridFormationOFGV1()
        Gv1.TableElement.TableHeaderHeight = 40
        Gv1.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To Gv1.Columns.Count - 1
            Gv1.Columns(ii).ReadOnly = True
            Gv1.Columns(ii).IsVisible = False
        Next

        If rdbDetail.IsChecked = True Then
            Gv1.Columns("Document_Code").IsVisible = True
            Gv1.Columns("Document_Code").Width = 50
            Gv1.Columns("Document_Code").HeaderText = "Document Code"

            Gv1.Columns("Document_Date").IsVisible = True
            Gv1.Columns("Document_Date").Width = 70
            Gv1.Columns("Document_Date").HeaderText = "Document Date"

            Gv1.Columns("ParentCode").IsVisible = True
            Gv1.Columns("ParentCode").Width = 100
            Gv1.Columns("ParentCode").HeaderText = "Parent Customer Code"

            Gv1.Columns("ParentName").IsVisible = True
            Gv1.Columns("ParentName").Width = 100
            Gv1.Columns("ParentName").HeaderText = "Parent Name"

            Gv1.Columns("Customer_Code").IsVisible = True
            Gv1.Columns("Customer_Code").Width = 100
            Gv1.Columns("Customer_Code").HeaderText = "Customer Code"

            Gv1.Columns("Customer_Name").IsVisible = True
            Gv1.Columns("Customer_Name").Width = 100
            Gv1.Columns("Customer_Name").HeaderText = "Customer Name"

            Gv1.Columns("Salesman_Code").IsVisible = True
            Gv1.Columns("Salesman_Code").Width = 100
            Gv1.Columns("Salesman_Code").HeaderText = "Salesman Code"

            Gv1.Columns("Salesman_Desc").IsVisible = True
            Gv1.Columns("Salesman_Desc").Width = 120
            Gv1.Columns("Salesman_Desc").HeaderText = "Salesman Name"

            Gv1.Columns("loc").IsVisible = True
            Gv1.Columns("loc").Width = 80
            Gv1.Columns("loc").HeaderText = "Location"

            Gv1.Columns("Location_Desc").IsVisible = True
            Gv1.Columns("Location_Desc").Width = 120
            Gv1.Columns("Location_Desc").HeaderText = "Location Desc"

            Gv1.Columns("Item_Code").IsVisible = True
            Gv1.Columns("Item_Code").Width = 80
            Gv1.Columns("Item_Code").HeaderText = "Item Code"


            Gv1.Columns("OrderQty").IsVisible = True
            Gv1.Columns("OrderQty").Width = 80
            Gv1.Columns("OrderQty").HeaderText = "Order Qty"

            Gv1.Columns("ShipQty").IsVisible = True
            Gv1.Columns("ShipQty").Width = 50
            Gv1.Columns("ShipQty").HeaderText = "Shippped Qty"

            Gv1.Columns("InvQty").IsVisible = True
            Gv1.Columns("InvQty").Width = 80
            Gv1.Columns("InvQty").HeaderText = "InvQty"

            Gv1.Columns("OutOrder").IsVisible = True
            Gv1.Columns("OutOrder").Width = 80
            Gv1.Columns("OutOrder").HeaderText = "Order Outstanding"

            Gv1.Columns("OutShip").IsVisible = True
            Gv1.Columns("OutShip").Width = 80
            Gv1.Columns("OutShip").HeaderText = "Shipment Outstanding"

            Try
                Gv1.Columns("Category").IsVisible = True
                Gv1.Columns("Category").Width = 220
                Gv1.Columns("Category").HeaderText = "Item Category"
            Catch ex As Exception
            End Try

        ElseIf rdbSummary.IsChecked Then
            Gv1.Columns("Document_Code").IsVisible = True
            Gv1.Columns("Document_Code").Width = 50
            Gv1.Columns("Document_Code").HeaderText = "Document Code"

            Gv1.Columns("Document_Date").IsVisible = True
            Gv1.Columns("Document_Date").Width = 70
            Gv1.Columns("Document_Date").HeaderText = "Document Date"

            Gv1.Columns("ParentCode").IsVisible = True
            Gv1.Columns("ParentCode").Width = 100
            Gv1.Columns("ParentCode").HeaderText = "Parent Customer Code"

            Gv1.Columns("ParentName").IsVisible = True
            Gv1.Columns("ParentName").Width = 100
            Gv1.Columns("ParentName").HeaderText = "Parent Name"

            Gv1.Columns("Customer_Code").IsVisible = True
            Gv1.Columns("Customer_Code").Width = 100
            Gv1.Columns("Customer_Code").HeaderText = "Customer Code"

            Gv1.Columns("Customer_Name").IsVisible = True
            Gv1.Columns("Customer_Name").Width = 100
            Gv1.Columns("Customer_Name").HeaderText = "Customer Name"

            Gv1.Columns("Salesman_Code").IsVisible = True
            Gv1.Columns("Salesman_Code").Width = 100
            Gv1.Columns("Salesman_Code").HeaderText = "Salesman Code"

            Gv1.Columns("Salesman_Desc").IsVisible = True
            Gv1.Columns("Salesman_Desc").Width = 120
            Gv1.Columns("Salesman_Desc").HeaderText = "Salesman Name"

            Gv1.Columns("loc").IsVisible = True
            Gv1.Columns("loc").Width = 80
            Gv1.Columns("loc").HeaderText = "Location"

            Gv1.Columns("Location_Desc").IsVisible = True
            Gv1.Columns("Location_Desc").Width = 120
            Gv1.Columns("Location_Desc").HeaderText = "Location Desc"

            Gv1.Columns("OrderQty").IsVisible = True
            Gv1.Columns("OrderQty").Width = 80
            Gv1.Columns("OrderQty").HeaderText = "Order Qty"

            Gv1.Columns("ShipQty").IsVisible = True
            Gv1.Columns("ShipQty").Width = 50
            Gv1.Columns("ShipQty").HeaderText = "Shippped Qty"

            Gv1.Columns("InvQty").IsVisible = True
            Gv1.Columns("InvQty").Width = 80
            Gv1.Columns("InvQty").HeaderText = "InvQty"

            Gv1.Columns("OutOrder").IsVisible = True
            Gv1.Columns("OutOrder").Width = 80
            Gv1.Columns("OutOrder").HeaderText = "Order Outstanding"

            Gv1.Columns("OutShip").IsVisible = True
            Gv1.Columns("OutShip").Width = 80
            Gv1.Columns("OutShip").HeaderText = "Shipment Outstanding"

            Try
                Gv1.Columns("Category").IsVisible = True
                Gv1.Columns("Category").Width = 220
                Gv1.Columns("Category").HeaderText = "Item Category"
            Catch ex As Exception
            End Try

        End If

        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim intCount As Integer = 0

        Dim item4 As New GridViewSummaryItem("OrderQty", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item4)
        Dim item5 As New GridViewSummaryItem("ShipQty", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item5)
        Dim item6 As New GridViewSummaryItem("InvQty", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item6)
        Dim item7 As New GridViewSummaryItem("OutOrder", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item7)
        Dim item8 As New GridViewSummaryItem("OutShip", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item8)

        'gv1.GroupDescriptors.Add(New GridGroupByExpression("Item_Code as Item format ""{0}: {1}"" Group By Item_Code"))
        'gv1.GroupDescriptors.Add(New GridGroupByExpression("Item_Desc as Item format ""{0}: {1}"" Group By Item_Desc"))
        'gv1.ShowGroupPanel = False
        'gv1.MasterTemplate.AutoExpandGroups = True

        Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

        RadPageView1.SelectedPage = RadPageViewPage2
    End Sub

    Private Sub ExportToExcel(ByVal exporter As EnumExportTo)
        Try


            Dim arrHeader As List(Of String) = New List(Of String)()
            Dim strtemp As String = "Date Range : " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy")
            arrHeader.Add(strtemp)


            strtemp = ""
            For Each Str As String In cbgLocation.CheckedDisplayMember
                If clsCommon.myLen(strtemp) > 0 Then
                    strtemp += ", "
                End If
                strtemp += Str
            Next
            arrHeader.Add("Location : " + strtemp)

            If rdbDetail.IsChecked Then
                strtemp = ""
                For Each Str As String In cbgItem.CheckedDisplayMember
                    If clsCommon.myLen(strtemp) > 0 Then
                        strtemp += ", "
                    End If
                    strtemp += Str
                Next
                arrHeader.Add("Item : " + strtemp)
            End If
            strtemp = ""
            For Each Str As String In cbgCustomer.CheckedDisplayMember
                If clsCommon.myLen(strtemp) > 0 Then
                    strtemp += ", "
                End If
                strtemp += Str
            Next
            arrHeader.Add("Customer : " + strtemp)



            If exporter = EnumExportTo.Excel Then
                clsCommon.MyExportToExcel("Order Tracking Report ", Gv1, arrHeader, Me.Text)

            Else
                clsCommon.MyExportToPDF("Order Tracking Report ", Gv1, arrHeader, "Order Tracking Report", True)
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub

    Private Sub Export_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Export.Click
        If Gv1.Rows.Count > 0 Then
            ExportToExcel(EnumExportTo.Excel)
        Else
            common.clsCommon.MyMessageBoxShow("No Data Found to Display", Me.Text)
        End If
    End Sub

    Private Sub PDF_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles PDF.Click
        If Gv1.Rows.Count > 0 Then
            ExportToExcel(EnumExportTo.PDF)
        Else
            common.clsCommon.MyMessageBoxShow("No Data Found to Display", Me.Text)
        End If
    End Sub

    Private Sub rbtnCategoryAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtnCategoryAll.ToggleStateChanged, rbtnCategorySelect.ToggleStateChanged
        tvCategory.Enabled = rbtnCategorySelect.IsChecked
    End Sub

    Private Sub rbtncustall_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtncustall.ToggleStateChanged, rbtncustslct.ToggleStateChanged
        cbgCustomer.Enabled = rbtncustslct.IsChecked
    End Sub

    Private Sub rbtnitemall_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtnitemall.ToggleStateChanged, rbtnitemslct.ToggleStateChanged
        cbgItem.Enabled = rbtnitemslct.IsChecked
    End Sub

    Private Sub rbtnlocall_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtnlocall.ToggleStateChanged, rbtnlocslct.ToggleStateChanged
        cbgLocation.Enabled = rbtnlocslct.IsChecked
    End Sub

    Private Sub Gv1_CellDoubleClick(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles Gv1.CellDoubleClick
        If Gv1.Rows.Count > 0 Then
            Dim strDoc
            strDoc = Gv1.CurrentRow.Cells("Document_code").Value
            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmSNSalesOrder, strDoc)
        End If
    End Sub
End Class
