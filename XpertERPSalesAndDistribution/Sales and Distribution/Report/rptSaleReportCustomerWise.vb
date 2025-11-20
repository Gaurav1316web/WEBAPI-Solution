Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO

Public Class rptSaleReportCustomerWise
    Inherits FrmMainTranScreen

#Region "Variables"

    Dim arrBack As New List(Of String)
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Public arrBatchNo As ArrayList
    Dim arrLoc As String = Nothing
    Dim FORMTYPE As String = Nothing
    Dim dtItem As DataTable = New DataTable
    Dim Baseqry As String = ""
    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.rptItemConsumptionReport)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        'rmExportToExcel.Visible = MyBase.isExport
        btnSplitExport.Visible = MyBase.isExport
    End Sub
#End Region

    Sub Reset()
        Gv1.DataSource = Nothing
        gvDetails.DataSource = Nothing
        EnableDisableCntrl(True)
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub
    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Reset()
    End Sub
    Sub LoadReportType()
        Dim dt As New DataTable
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))
        dt.Rows.Add("", "Select")
        dt.Rows.Add("CIWH", "Customer And Item Wise (Horizontal)")
        dt.Rows.Add("CIWV", "Customer And Item Wise (Vertical)")
        dt.Rows.Add("YMW", "Year and Month Wise")
        dt.Rows.Add("YW", "Year Wise")

        cmbReportType.SelectedIndex = 0
        cmbReportType.DataSource = dt
        cmbReportType.ValueMember = "Code"
        cmbReportType.DisplayMember = "Name"
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Sub EnableDisableCntrl(ByVal val As Boolean)
        RadGroupBox3.Enabled = val
        txtToDate.Enabled = val
        txtFromDate.Enabled = val
        TxtLocation.Enabled = val
        lblBillToLocation.Enabled = val
        cmbReportType.Enabled = val
        RadGroupBoxFiscalYear.Enabled = val
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Load_Sales_Report_CustomerWise()
    End Sub

    Sub Load_Sales_Report_CustomerWise()
        Dim qry As String = ""
        Dim dt As New DataTable()
        Try
            If clsCommon.myLen(TxtLocation.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Plz Select Location.", Me.Text)
                TxtLocation.Focus()
                Exit Sub
            End If

            If cmbReportType.SelectedValue = "YMW" Then
                If clsCommon.myLen(txtFiscalYear.Value) <= 0 Then
                    clsCommon.MyMessageBoxShow(Me, "Plz Select Fiscal Year.", Me.Text)
                    txtFiscalYear.Focus()
                    Exit Sub
                End If
            ElseIf cmbReportType.SelectedValue = "YW" Then
                If txtMulFiscalYear.arrValueMember Is Nothing Then
                    clsCommon.MyMessageBoxShow(Me, "Plz Select atleast one Fiscal Year.", Me.Text)
                    txtMulFiscalYear.Focus()
                    Exit Sub
                End If
            End If

            Baseqry = " SELECT  TSPL_SD_SALE_INVOICE_HEAD.Document_Date,TSPL_SD_SALE_INVOICE_HEAD.Document_Code,DATENAME(MONTH, Document_Date) AS MonthName , MONTH(Document_Date) AS MonthNumber,((ISNULL(TSPL_SD_SALE_INVOICE_DETAIL.Qty,0)*FromUOM.Conversion_Factor)/ToUOM.Conversion_Factor) AS Qty, 
                    ((ISNULL(TSPL_SD_SALE_INVOICE_DETAIL.Qty,0)*FromUOM.Conversion_Factor)/ToUOMBAG.Conversion_Factor) AS Qty_Bag,TSPL_SD_SALE_INVOICE_DETAIL.Item_Net_Amt as Amount,
                    TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location,TSPL_ITEM_MASTER.Item_Code,ToUOM.UOM_Code,TSPL_ITEM_MASTER.Item_Desc,TSPL_SD_SALE_INVOICE_HEAD.Customer_Code,TSPL_CUSTOMER_MASTER.Customer_Name
                    FROM TSPL_SD_SALE_INVOICE_DETAIL 
                    LEFT JOIN TSPL_SD_SALE_INVOICE_HEAD ON TSPL_SD_SALE_INVOICE_HEAD.document_code = TSPL_SD_SALE_INVOICE_DETAIL.document_code
                    LEFT JOIN TSPL_Item_Master ON TSPL_Item_Master.Item_Code = TSPL_SD_SALE_INVOICE_DETAIL.ITEM_CODE
                    LEFT JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_SD_SALE_INVOICE_HEAD.Customer_Code
                    LEFT JOIN TSPL_ITEM_UOM_DETAIL FromUOM ON FromUOM.Item_Code = TSPL_SD_SALE_INVOICE_DETAIL.Item_Code AND FromUOM.UOM_Code = TSPL_SD_SALE_INVOICE_DETAIL.Unit_code
                    LEFT JOIN TSPL_ITEM_UOM_DETAIL ToUOM ON ToUOM.Item_Code = TSPL_SD_SALE_INVOICE_DETAIL.Item_Code AND ToUOM.UOM_Code = 'MT'
                    LEFT JOIN TSPL_ITEM_UOM_DETAIL AS ToUOMBAG ON ToUOMBAG.Item_Code = TSPL_SD_SALE_INVOICE_DETAIL.Item_Code AND ToUOMBAG.UOM_Code = 'BAG'
                    WHERE   TSPL_Item_Master.FG_for_CF_RPT = 1 AND TSPL_SD_SALE_INVOICE_HEAD.Status = 1 AND TSPL_SD_SALE_INVOICE_HEAD.Inter_unit_sale = 0  
                    AND CONVERT(DATE, TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) >= CONVERT(DATE, '" + clsCommon.GetPrintDate(txtFromDate.Value) + "',103)
                    AND CONVERT(DATE, TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) <= CONVERT(DATE, '" + clsCommon.GetPrintDate(txtToDate.Value) + "',103)
                    and TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location = '" & TxtLocation.Value & "' "
            If txtCustomer.arrValueMember IsNot Nothing Then
                Baseqry += " and TSPL_SD_SALE_INVOICE_HEAD.Customer_Code in (" + clsCommon.GetMulcallString(txtCustomer.arrValueMember) + ")  "
            End If
            If TxtCustGrp.arrValueMember IsNot Nothing Then
                Baseqry += " and TSPL_CUSTOMER_MASTER.Cust_Code in (" + clsCommon.GetMulcallString(TxtCustGrp.arrValueMember) + ")  "
            End If

            Baseqry += " UNION ALL
                    SELECT TSPL_SCRAPINVOICE_HEAD.shipment_Date,TSPL_SCRAPINVOICE_HEAD.shipment_No,DATENAME(MONTH, shipment_Date) AS MonthName, MONTH(shipment_Date) AS MonthNumber,((ISNULL(TSPL_SCRAPINVOICE_DETAIL.shipped_Qty,0)*FromUOM.Conversion_Factor)/ToUOM.Conversion_Factor) AS Qty, 
                    ((ISNULL(TSPL_SCRAPINVOICE_DETAIL.shipped_Qty,0)*FromUOM.Conversion_Factor)/ToUOMBAG.Conversion_Factor) AS Qty_Bag,TSPL_SCRAPINVOICE_DETAIL.ItemNetAmt as Amount,
                    TSPL_SCRAPINVOICE_HEAD.Loc_Code AS Bill_To_Location,TSPL_ITEM_MASTER.Item_Code,ToUOM.UOM_Code,TSPL_ITEM_MASTER.Item_Desc,TSPL_SCRAPINVOICE_HEAD.Cust_Code AS Customer_Code,TSPL_CUSTOMER_MASTER.Customer_Name
                    FROM TSPL_SCRAPINVOICE_DETAIL 
                    LEFT JOIN TSPL_SCRAPINVOICE_HEAD ON TSPL_SCRAPINVOICE_HEAD.invoice_No = TSPL_SCRAPINVOICE_DETAIL.invoice_No
                    LEFT JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_SCRAPINVOICE_HEAD.Cust_Code
                    LEFT JOIN TSPL_Item_Master ON TSPL_Item_Master.Item_Code = TSPL_SCRAPINVOICE_DETAIL.Item_Code
                    LEFT JOIN TSPL_ITEM_UOM_DETAIL FromUOM ON FromUOM.Item_Code = TSPL_SCRAPINVOICE_DETAIL.Item_Code AND FromUOM.UOM_Code = TSPL_SCRAPINVOICE_DETAIL.Unit_code
                    LEFT JOIN TSPL_ITEM_UOM_DETAIL AS ToUOM ON ToUOM.Item_Code = TSPL_SCRAPINVOICE_DETAIL.Item_Code AND ToUOM.UOM_Code = 'MT'
                    LEFT JOIN TSPL_ITEM_UOM_DETAIL AS ToUOMBAG ON ToUOMBAG.Item_Code = TSPL_SCRAPINVOICE_DETAIL.Item_Code AND ToUOMBAG.UOM_Code = 'BAG'
                    WHERE  TSPL_Item_Master.FG_for_CF_RPT = 1 AND TSPL_SCRAPINVOICE_HEAD.ispost = 1 AND TSPL_SCRAPINVOICE_HEAD.Inter_unit_sale = 0  
                    AND CONVERT(DATE, TSPL_SCRAPINVOICE_HEAD.shipment_Date,103) >= CONVERT(DATE, '" + clsCommon.GetPrintDate(txtFromDate.Value) + "',103)
                    AND CONVERT(DATE, TSPL_SCRAPINVOICE_HEAD.shipment_Date,103) <= CONVERT(DATE, '" + clsCommon.GetPrintDate(txtToDate.Value) + "',103) 
                    and TSPL_SCRAPINVOICE_HEAD.Loc_Code = '" & TxtLocation.Value & "'
                    "
            If txtCustomer.arrValueMember IsNot Nothing Then
                Baseqry += " and TSPL_SCRAPINVOICE_HEAD.Cust_Code in (" + clsCommon.GetMulcallString(txtCustomer.arrValueMember) + ")  "
            End If
            If TxtCustGrp.arrValueMember IsNot Nothing Then
                Baseqry += " and TSPL_CUSTOMER_MASTER.Cust_Code in (" + clsCommon.GetMulcallString(TxtCustGrp.arrValueMember) + ")  "
            End If

            Dim Itemqry As String = ""
            Itemqry = "select  Item_Code,max(Item_Desc)Item_Desc from  ( " & Baseqry & "  )xx group by Item_Code "
            If TxtItem.arrValueMember IsNot Nothing Then
                Itemqry += " and TSPL_Item_Master.Item_Code in (" + clsCommon.GetMulcallString(TxtItem.arrValueMember) + ")  "
            End If

            dtItem = clsDBFuncationality.GetDataTable(Itemqry)

            Dim IemNameQty As String = Nothing
            'Dim IemNameBag As String = Nothing
            Dim ItemNameAmt As String = Nothing
            Dim ItemCode As String = Nothing
            Dim itemNames2 As String = Nothing
            Dim Total_Qty As String = Nothing
            Dim Total_Amt As String = Nothing
            ' Dim Total_Bag As String = Nothing
            Dim QtyName As String = Nothing
            ' Dim BagName As String = Nothing


            If dtItem.Rows.Count > 0 Then
                For i As Integer = 0 To dtItem.Rows.Count - 1
                    If i = 0 Then
                        IemNameQty += "[" + clsCommon.myCstr(dtItem.Rows(i)("Item_Code")) + "] "
                        'itemNames2 += " IsNull([" + clsCommon.myCstr(dtItem.Rows(i)("Item_Code")) + "],0) As [" + clsCommon.myCstr(dtItem.Rows(i)("Item_Desc")) + "]"
                        ' IemNameBag += "[" + clsCommon.myCstr(dtItem.Rows(i)("Item_Desc")) + "] "
                        ItemNameAmt += "[" + clsCommon.myCstr(dtItem.Rows(i)("Item_Desc")) + "] "
                        itemNames2 += " IsNull([" + clsCommon.myCstr(dtItem.Rows(i)("Item_Code")) + "],0) As [" + clsCommon.myCstr(dtItem.Rows(i)("Item_Desc")) + " Qty]"
                        ' " ,IsNull([" + clsCommon.myCstr(dtItem.Rows(i)("Item_Desc")) + "],0) As [" + clsCommon.myCstr(dtItem.Rows(i)("Item_Desc")) + " Bag]" &
                        itemNames2 += " ,IsNull([" + clsCommon.myCstr(dtItem.Rows(i)("Item_Desc")) + "],0) As [" + clsCommon.myCstr(dtItem.Rows(i)("Item_Desc")) + " Amt]"
                        Total_Qty += " (IsNull([" + clsCommon.myCstr(dtItem.Rows(i)("Item_Code")) + "],0))"
                        Total_Amt += " (IsNull([" + clsCommon.myCstr(dtItem.Rows(i)("Item_Desc")) + "],0))"
                        ' Total_Bag += " (IsNull([" + clsCommon.myCstr(dtItem.Rows(i)("Item_Desc")) + "],0))"
                        QtyName += " Sum([" + clsCommon.myCstr(dtItem.Rows(i)("Item_Desc")) + " Qty]) as [" + clsCommon.myCstr(dtItem.Rows(i)("Item_Desc")) + " Qty]"
                        '" ,Sum([" + clsCommon.myCstr(dtItem.Rows(i)("Item_Desc")) + " Bag]) as [" + clsCommon.myCstr(dtItem.Rows(i)("Item_Desc")) + " Bag]  " &
                        QtyName += " ,Sum([" + clsCommon.myCstr(dtItem.Rows(i)("Item_Desc")) + " Amt]) As [" + clsCommon.myCstr(dtItem.Rows(i)("Item_Desc")) + " Amt] "
                    Else
                        IemNameQty += ", [" + clsCommon.myCstr(dtItem.Rows(i)("Item_Code")) + "] "
                        ' IemNameBag += ", [" + clsCommon.myCstr(dtItem.Rows(i)("Item_Desc")) + "] "
                        ItemNameAmt += ", [" + clsCommon.myCstr(dtItem.Rows(i)("Item_Desc")) + "] "
                        'itemNames2 += ", IsNull([" + clsCommon.myCstr(dtItem.Rows(i)("Item_Code")) + "],0) As [" + clsCommon.myCstr(dtItem.Rows(i)("Item_Desc")) + "]"
                        'itemNames2 += ", IsNull([" + clsCommon.myCstr(dtItem.Rows(i)("Item_Code")) + "],0) As [" + clsCommon.myCstr(dtItem.Rows(i)("Item_Desc")) + " Qty]"
                        itemNames2 += ", IsNull([" + clsCommon.myCstr(dtItem.Rows(i)("Item_Code")) + "],0) As [" + clsCommon.myCstr(dtItem.Rows(i)("Item_Desc")) + " Qty]"
                        '   ", IsNull([" + clsCommon.myCstr(dtItem.Rows(i)("Item_Desc")) + "],0) As [" + clsCommon.myCstr(dtItem.Rows(i)("Item_Desc")) + " Bag]" &
                        itemNames2 += " ,IsNull([" + clsCommon.myCstr(dtItem.Rows(i)("Item_Desc")) + "],0) As [" + clsCommon.myCstr(dtItem.Rows(i)("Item_Desc")) + " Amt]"
                        Total_Qty += " + " + " (IsNull([" + clsCommon.myCstr(dtItem.Rows(i)("Item_Code")) + "],0))"
                        Total_Amt += " + " + " (IsNull([" + clsCommon.myCstr(dtItem.Rows(i)("Item_Desc")) + "],0))"
                        '  Total_Bag += " + " + " (IsNull([" + clsCommon.myCstr(dtItem.Rows(i)("Item_Desc")) + "],0))"
                        QtyName += " ,Sum([" + clsCommon.myCstr(dtItem.Rows(i)("Item_Desc")) + " Qty]) as [" + clsCommon.myCstr(dtItem.Rows(i)("Item_Desc")) + " Qty]"
                        ' " ,Sum([" + clsCommon.myCstr(dtItem.Rows(i)("Item_Desc")) + " Bag]) as [" + clsCommon.myCstr(dtItem.Rows(i)("Item_Desc")) + " Bag] " &
                        QtyName += " ,Sum([" + clsCommon.myCstr(dtItem.Rows(i)("Item_Desc")) + " Amt]) As [" + clsCommon.myCstr(dtItem.Rows(i)("Item_Desc")) + " Amt] "
                    End If
                Next
            End If
            ''-- ===== Sale Invoice =====   -- ===== Scrap Invoice =====

            qry = " WITH BaseData AS ( " & Baseqry & " ), pivoted as ( "
            If cmbReportType.SelectedValue = "CIWH" Then
                qry += " Select Customer_Code as [Customer Code], Customer_Name as [Customer Name], " & itemNames2 & ", " & Total_Qty & " as Total_Qty ," & Total_Amt & " as Total_Amt
                    FROM BaseData
                    PIVOT
                    ( SUM(Qty) FOR Item_Code IN (" & IemNameQty & ") 
                    ) AS P
                    PIVOT
                    ( SUM(Amount) FOR Item_Desc IN (" & ItemNameAmt & ")
                    ) AS P2 )
                    SELECT [Customer Code],max([Customer Name]) as [Customer Name]," & QtyName & " , SUM(Total_Qty) AS Total_Qty,sum(Total_Amt)Total_Amt FROM Pivoted GROUP BY [Customer Code] ORDER BY [Customer Code];"
            ElseIf cmbReportType.SelectedValue = "CIWV" Then
                qry = "  SELECT Customer_Code AS [Customer Code],MAX(Customer_Name) AS [Customer Name], Item_Code AS [Item Code],max(Item_Desc) AS [Item Desc], sum(qty)Qty,max(UOM_Code)[Unit Code],sum(Amount)Amount FROM ( " & Baseqry & "  ) XX GROUP BY CUSTOMER_CODE,Item_Code ORDER BY [Customer Code] "
            ElseIf cmbReportType.SelectedValue = "YMW" Then
                qry += " Select  MonthName as [Month Name],MonthNumber, " & itemNames2 & ", " & Total_Qty & " as Total_Qty , " & Total_Amt & " as Total_Amt
                    FROM BaseData PIVOT ( SUM(Qty) FOR Item_Code IN (" & IemNameQty & ")  ) AS PQty PIVOT ( SUM(Amount) FOR Item_Desc IN (" & ItemNameAmt & ") ) AS PAmt )
                    SELECT [Month Name]," & QtyName & " , SUM(Total_Qty) AS Total_Qty,SUM(Total_Amt) AS Total_Amt FROM Pivoted GROUP BY [Month Name],MonthNumber ORDER BY MonthNumber ;"
            ElseIf cmbReportType.SelectedValue = "YW" Then
                Baseqry = "  select * from ( " & Baseqry & " ) xx  LEFT JOIN (select Fiscal_Code,Fiscal_Name,Start_Date,End_Date from TSPL_Fiscal_Year_Master) as TSPL_Fiscal_Year_Master ON CONVERT(DATE, xx.Document_Date, 103) BETWEEN TSPL_Fiscal_Year_Master.Start_Date AND TSPL_Fiscal_Year_Master.End_Date where 2=2 "
                If txtMulFiscalYear.arrValueMember IsNot Nothing AndAlso txtMulFiscalYear.arrValueMember.Count > 0 Then
                    Baseqry += " and Fiscal_Code in (" & clsCommon.GetMulcallString(txtMulFiscalYear.arrValueMember) & ") "
                    qry = " WITH BaseData AS ( " & Baseqry & " ), pivoted as ( Select  Fiscal_Code,Start_Date,Fiscal_Name, " & itemNames2 & ", " & Total_Qty & " as Total_Qty , " & Total_Amt & " as Total_Amt
                    FROM BaseData PIVOT ( SUM(Qty) FOR Item_Code IN (" & IemNameQty & ")  ) AS PQty PIVOT ( SUM(Amount) FOR Item_Desc IN (" & ItemNameAmt & ") ) AS PAmt )
                    SELECT Fiscal_Code, max(Fiscal_Name)Fiscal_Name," & QtyName & " , SUM(Total_Qty) AS Total_Qty,SUM(Total_Amt) AS Total_Amt FROM Pivoted GROUP BY Fiscal_Code ORDER BY max(Start_Date) ;"
                End If
            End If

            Gv1.DataSource = Nothing
            Gv1.GroupDescriptors.Clear()
            Gv1.SummaryRowsBottom.Clear()
            Dim viewBlank As New TableViewDefinition()
            Gv1.ViewDefinition = viewBlank

            dt = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Gv1.DataSource = dt
                'gv1.Columns("TransType").IsVisible = False
                'gv1.Columns("PROD_ENTRY_CODE").IsVisible = False
                RadPageView1.SelectedPage = RadPageViewPage2
                Gv1.BestFitColumns()
                FormatGrid()
                If Not cmbReportType.SelectedValue = "CIWV" Then
                    View()
                End If
                EnableDisableCntrl(False)
            Else
                clsCommon.MyMessageBoxShow("No data found to display.", "Sales Customer Wise Report")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try

    End Sub
    Sub FormatGrid()
        Gv1.TableElement.TableHeaderHeight = 40
        Gv1.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To Gv1.Columns.Count - 1
            Gv1.Columns(ii).ReadOnly = True
            Gv1.Columns(ii).IsVisible = True
            Gv1.Columns(ii).FormatString = "{0:n2}"
            If Gv1.Columns(ii).Name.Contains("Amt") AndAlso Not Gv1.Columns(ii).Name.Contains("Total_Amt") Then
                Gv1.Columns(ii).HeaderText = "Amt"
            ElseIf Gv1.Columns(ii).Name.Contains("Qty") AndAlso Not Gv1.Columns(ii).Name.Contains("Total_Qty") Then
                Gv1.Columns(ii).HeaderText = "Qty(MT)"
            End If
        Next
        If clsCommon.myLen(Gv1.Columns("Total_Amt")) > 0 Then
            Gv1.Columns("Total_Amt").HeaderText = "Total Amt"
        End If

        If clsCommon.myLen(Gv1.Columns("Total_Qty")) > 0 Then
            Gv1.Columns("Total_Qty").HeaderText = "Total Qty(MT)"
        End If

        If clsCommon.myLen(Gv1.Columns("Fiscal_Code")) > 0 Then
            Gv1.Columns("Fiscal_Code").HeaderText = "Fiscal Code"
            Gv1.Columns("Fiscal_Code").IsVisible = False
        End If

        If clsCommon.myLen(Gv1.Columns("Fiscal_Name")) > 0 Then
            Gv1.Columns("Fiscal_Name").HeaderText = "Fiscal Name"
        End If
        Dim index As Integer = 0
        If cmbReportType.SelectedValue = "CIWH" Then
            index = 2
        ElseIf cmbReportType.SelectedValue = "YMW" Then
            index = 1
        ElseIf cmbReportType.SelectedValue = "YW" Then
            index = 2
        Else
            index = 4
        End If

        Dim summaryRowItem As New GridViewSummaryRowItem()
        For ii As Integer = index To Gv1.Columns.Count - 1
            If Not clsCommon.CompairString(Gv1.Columns(ii).Name, "Unit Code") = CompairStringResult.Equal Then
                summaryRowItem.Add(New GridViewSummaryItem(Gv1.Columns(ii).Name, "{0:F2}", GridAggregateFunction.Sum))
            End If
        Next
        Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        Gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
        Gv1.EnableFiltering = True
        Gv1.ShowGroupPanel = False
        gvDetails.BestFitColumns()
    End Sub

    Sub View()
        Try
            If Gv1.Rows.Count > 0 Then
                Dim view As New ColumnGroupsViewDefinition()
                view.ColumnGroups.Add(New GridViewColumnGroup(""))
                view.ColumnGroups(0).Rows.Add(New GridViewColumnGroupRow())

                If cmbReportType.SelectedValue = "CIWH" Then
                    view.ColumnGroups(0).Rows(0).ColumnNames.Add("Customer Code")
                    view.ColumnGroups(0).Rows(0).ColumnNames.Add("Customer Name")
                ElseIf cmbReportType.SelectedValue = "YMW" Then
                    view.ColumnGroups(0).Rows(0).ColumnNames.Add("Month Name")
                ElseIf cmbReportType.SelectedValue = "YW" Then
                    view.ColumnGroups(0).Rows(0).ColumnNames.Add("Fiscal_Code")
                    view.ColumnGroups(0).Rows(0).ColumnNames.Add("Fiscal_Name")
                End If
                If dtItem.Rows.Count > 0 Then
                    For ii As Integer = 0 To dtItem.Rows.Count - 1
                        view.ColumnGroups.Add(New GridViewColumnGroup(dtItem.Rows(ii)("Item_Desc")))
                        view.ColumnGroups(view.ColumnGroups.Count - 1).Rows.Add(New GridViewColumnGroupRow())
                        For col As Integer = 1 To Gv1.Columns("Total_Qty").Index - 1
                            If clsCommon.CompairString(Gv1.Columns(col).Name.Replace("Qty", ""), dtItem.Rows(ii)("Item_Desc")) = CompairStringResult.Equal OrElse clsCommon.CompairString(Gv1.Columns(col).Name.Replace("Amt", ""), dtItem.Rows(ii)("Item_Desc")) = CompairStringResult.Equal Then
                                view.ColumnGroups(view.ColumnGroups.Count - 1).Rows(0).ColumnNames.Add(Gv1.Columns(col).Name)
                            End If
                        Next
                    Next
                End If

                view.ColumnGroups.Add(New GridViewColumnGroup("Total"))
                view.ColumnGroups(view.ColumnGroups.Count - 1).Rows.Add(New GridViewColumnGroupRow())
                view.ColumnGroups(view.ColumnGroups.Count - 1).Rows(0).ColumnNames.Add(Gv1.Columns("Total_Qty").Name)
                view.ColumnGroups(view.ColumnGroups.Count - 1).Rows(0).ColumnNames.Add(Gv1.Columns("Total_Amt").Name)
                Gv1.ViewDefinition = view
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub
    Private Sub TxtLocation__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles TxtLocation._MYValidating
        Dim qry As String = "select Location_Code as Code,Location_Desc as Name from TSPL_LOCATION_MASTER "
        Dim WhrCls As String = " Location_Type='Physical'  "
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            WhrCls += "  and  Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
        End If


        TxtLocation.Value = clsCommon.ShowSelectForm("VendorMafnd", qry, "Code", WhrCls, TxtLocation.Value, "Code", isButtonClicked)
        lblBillToLocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" + TxtLocation.Value + "'"))

    End Sub

    Private Sub rptSaleReportCustomerWise_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = clsCommon.GETSERVERDATE()
        LoadReportType()
        Reset()
    End Sub

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        Try
            If Gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "Admin") = CompairStringResult.Equal Then
                    arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)

                Else
                    'Dim strLocDesc As String = clsDBFuncationality.getSingleValue("select Location_Desc from tspl_location_master where Location_Code in (" + objCommonVar.strCurrUserLocations + ")")
                    'arrHeader.Add("Location : " + strLocDesc)
                End If

                If TxtLocation.Value IsNot Nothing AndAlso TxtLocation.Value.Count > 0 Then
                    arrHeader.Add(" Location : " + clsCommon.myCstr(lblBillToLocation.Text))
                End If
                arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")

                transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
                clsCommon.MyExportToExcelGrid("Sales Report Customer Wise", Gv1, arrHeader, Me.Text)
                ' transportSql.exportdataChilRows(gv1, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
                common.clsCommon.MyMessageBoxShow("Exported Successfully.", Me.Text)
                'Process.Start(filePath)

            Else
                common.clsCommon.MyMessageBoxShow("No Data Found to Export.", Me.Text)

            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        Try

            If Gv1.Rows.Count > 0 Then

                Dim arrHeader As List(Of String) = New List(Of String)()
                If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "Admin") = CompairStringResult.Equal Then
                    arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                Else
                    'Dim strLocDesc As String = clsDBFuncationality.getSingleValue("select Location_Desc from tspl_location_master where Location_Code in (" + objCommonVar.strCurrUserLocations + ")")
                    'arrHeader.Add("Location : " + strLocDesc)
                End If
                '
                If TxtLocation.Value IsNot Nothing AndAlso TxtLocation.Value.Count > 0 Then
                    arrHeader.Add("Location : " + clsCommon.myCstr(lblBillToLocation.Text))
                End If
                arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")

                transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
                clsCommon.MyExportToPDF("Sales Report Customer Wise", Gv1, arrHeader, "Sales Report Customer Wise", PageSetupReport_ID, objCommonVar.CurrentUserCode)
            Else
                common.clsCommon.MyMessageBoxShow("No Data Found to Export.", Me.Text)
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub rmsaveLayout_Click(sender As Object, e As EventArgs) Handles rmsaveLayout.Click
        If clsCommon.myLen(MyBase.Form_ID) > 0 Then
            Gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = MyBase.Form_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            Gv1.SaveLayout(obj.GridLayout)
            obj.GridColumns = Gv1.ColumnCount
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If obj.SaveData() Then
                clsCommon.MyMessageBoxShow(Me, "Layout saved successfully", Me.Text)
            End If
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        Try
            clsGridLayout.DeleteData(MyBase.Form_ID, objCommonVar.CurrentUserCode)
            common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", "Information", Me.Text)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtCustomer__My_Click(sender As Object, e As EventArgs) Handles txtCustomer._My_Click
        Dim qry As String = " select tspl_customer_master.cust_code as [Code], tspl_customer_master.Customer_Name as [Name] from tspl_customer_master 
                              where 1=1 "
        txtCustomer.arrValueMember = clsCommon.ShowMultipleSelectForm("CustMulSel", qry, "Code", "Name", txtCustomer.arrValueMember, txtCustomer.arrDispalyMember)
        ''richa agarwal 26 June,2019 ERO/24/06/19-000653

    End Sub

    Private Sub TxtCustGrp__My_Click(sender As Object, e As EventArgs) Handles TxtCustGrp._My_Click
        Dim qry As String = " select tspl_customer_master.cust_code as [Code], tspl_customer_master.Customer_Name as [Name],Tspl_customer_master.Cust_Group_Code as [Customer Group] from tspl_customer_master 
                              where 1=1 and Cust_Group_Code='UNION' "
        TxtCustGrp.arrValueMember = clsCommon.ShowMultipleSelectForm("CustMulSel", qry, "Code", "Name", TxtCustGrp.arrValueMember, TxtCustGrp.arrDispalyMember)
        ''richa agarwal 26 June,2019 ERO/24/06/19-000653

    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        PrintReport()
    End Sub

    Sub PrintReport()
        Try
            Dim frmCRV As New frmCrystalReportViewer()
            Dim whrcls As String = ""
            Dim dtPrint As New DataTable
            Dim Qry As String = ""
            Qry = "  ( SELECT  TSPL_LOCATION_MASTER.Location_Desc,TSPL_LOCATION_MASTER.Add1,TSPL_LOCATION_MASTER.Add2,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,((ISNULL(TSPL_SD_SALE_INVOICE_DETAIL.Qty,0)*FromUOM.Conversion_Factor)/ToUOM.Conversion_Factor) AS Qty, 
                    ((ISNULL(TSPL_SD_SALE_INVOICE_DETAIL.Qty,0)*FromUOM.Conversion_Factor)/ToUOMBAG.Conversion_Factor) AS Qty_Bag,TSPL_SD_SALE_INVOICE_DETAIL.Item_Net_Amt as Amount,
                    TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location,TSPL_ITEM_MASTER.Item_Code,TSPL_ITEM_MASTER.Item_Desc,TSPL_SD_SALE_INVOICE_HEAD.Customer_Code,TSPL_CUSTOMER_MASTER.Customer_Name
                    FROM TSPL_SD_SALE_INVOICE_DETAIL 
                    LEFT JOIN TSPL_SD_SALE_INVOICE_HEAD ON TSPL_SD_SALE_INVOICE_HEAD.document_code = TSPL_SD_SALE_INVOICE_DETAIL.document_code
                    LEFT JOIN TSPL_Item_Master ON TSPL_Item_Master.Item_Code = TSPL_SD_SALE_INVOICE_DETAIL.ITEM_CODE
                    LEFT JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_SD_SALE_INVOICE_HEAD.Customer_Code
                    LEFT JOIN TSPL_ITEM_UOM_DETAIL FromUOM ON FromUOM.Item_Code = TSPL_SD_SALE_INVOICE_DETAIL.Item_Code AND FromUOM.UOM_Code = TSPL_SD_SALE_INVOICE_DETAIL.Unit_code
                    LEFT JOIN TSPL_ITEM_UOM_DETAIL ToUOM ON ToUOM.Item_Code = TSPL_SD_SALE_INVOICE_DETAIL.Item_Code AND ToUOM.UOM_Code = 'MT'
                    LEFT JOIN TSPL_ITEM_UOM_DETAIL AS ToUOMBAG ON ToUOMBAG.Item_Code = TSPL_SD_SALE_INVOICE_DETAIL.Item_Code AND ToUOMBAG.UOM_Code = 'BAG'
					left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location
                    WHERE   TSPL_Item_Master.FG_for_CF_RPT = 1 AND TSPL_SD_SALE_INVOICE_HEAD.Status = 1 AND TSPL_SD_SALE_INVOICE_HEAD.Inter_unit_sale = 0  
                    AND CONVERT(DATE, TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) >= CONVERT(DATE, '" + clsCommon.GetPrintDate(txtFromDate.Value) + "',103)
                    AND CONVERT(DATE, TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) <= CONVERT(DATE, '" + clsCommon.GetPrintDate(txtToDate.Value) + "',103)
                    and TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location = '" & TxtLocation.Value & "'"
            If txtCustomer.arrValueMember IsNot Nothing Then
                Qry += " and TSPL_SD_SALE_INVOICE_HEAD.Customer_Code in (" + clsCommon.GetMulcallString(txtCustomer.arrValueMember) + ")  "
            End If
            If TxtCustGrp.arrValueMember IsNot Nothing Then
                Qry += " and TSPL_CUSTOMER_MASTER.Cust_Code in (" + clsCommon.GetMulcallString(TxtCustGrp.arrValueMember) + ")  "
            End If

            Qry += " UNION ALL
  
                    SELECT TSPL_LOCATION_MASTER.Location_Desc,TSPL_LOCATION_MASTER.Add1,TSPL_LOCATION_MASTER.Add2,TSPL_SCRAPINVOICE_HEAD.shipment_Date,((ISNULL(TSPL_SCRAPINVOICE_DETAIL.shipped_Qty,0)*FromUOM.Conversion_Factor)/ToUOM.Conversion_Factor) AS Qty, 
                    ((ISNULL(TSPL_SCRAPINVOICE_DETAIL.shipped_Qty,0)*FromUOM.Conversion_Factor)/ToUOMBAG.Conversion_Factor) AS Qty_Bag,TSPL_SCRAPINVOICE_DETAIL.ItemNetAmt as Amount,
                    TSPL_SCRAPINVOICE_HEAD.Loc_Code AS Bill_To_Location,TSPL_ITEM_MASTER.Item_Code,TSPL_ITEM_MASTER.Item_Desc,TSPL_SCRAPINVOICE_HEAD.Cust_Code AS Customer_Code,TSPL_CUSTOMER_MASTER.Customer_Name
                    FROM TSPL_SCRAPINVOICE_DETAIL 
                    LEFT JOIN TSPL_SCRAPINVOICE_HEAD ON TSPL_SCRAPINVOICE_HEAD.invoice_No = TSPL_SCRAPINVOICE_DETAIL.invoice_No
                    LEFT JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_SCRAPINVOICE_HEAD.Cust_Code
                    LEFT JOIN TSPL_Item_Master ON TSPL_Item_Master.Item_Code = TSPL_SCRAPINVOICE_DETAIL.Item_Code
                    LEFT JOIN TSPL_ITEM_UOM_DETAIL FromUOM ON FromUOM.Item_Code = TSPL_SCRAPINVOICE_DETAIL.Item_Code AND FromUOM.UOM_Code = TSPL_SCRAPINVOICE_DETAIL.Unit_code
                    LEFT JOIN TSPL_ITEM_UOM_DETAIL AS ToUOM ON ToUOM.Item_Code = TSPL_SCRAPINVOICE_DETAIL.Item_Code AND ToUOM.UOM_Code = 'MT'
                    LEFT JOIN TSPL_ITEM_UOM_DETAIL AS ToUOMBAG ON ToUOMBAG.Item_Code = TSPL_SCRAPINVOICE_DETAIL.Item_Code AND ToUOMBAG.UOM_Code = 'BAG'
					left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_SCRAPINVOICE_HEAD.Loc_Code
                    WHERE  TSPL_Item_Master.FG_for_CF_RPT = 1 AND TSPL_SCRAPINVOICE_HEAD.ispost = 1 AND TSPL_SCRAPINVOICE_HEAD.Inter_unit_sale = 0  
                    AND CONVERT(DATE, TSPL_SCRAPINVOICE_HEAD.shipment_Date,103) >= CONVERT(DATE, '" + clsCommon.GetPrintDate(txtFromDate.Value) + "',103)
                    AND CONVERT(DATE, TSPL_SCRAPINVOICE_HEAD.shipment_Date,103) <= CONVERT(DATE, '" + clsCommon.GetPrintDate(txtToDate.Value) + "',103) 
                    and TSPL_SCRAPINVOICE_HEAD.Loc_Code = '" & TxtLocation.Value & "'"
            If txtCustomer.arrValueMember IsNot Nothing Then
                Qry += " and TSPL_SCRAPINVOICE_HEAD.Cust_Code in (" + clsCommon.GetMulcallString(txtCustomer.arrValueMember) + ")  "
            End If
            If TxtCustGrp.arrValueMember IsNot Nothing Then
                Qry += " and TSPL_CUSTOMER_MASTER.Cust_Code in (" + clsCommon.GetMulcallString(TxtCustGrp.arrValueMember) + ")  "
            End If
            Qry += "   )"

            Dim dtItems As DataTable = clsDBFuncationality.GetDataTable("select Item_Code,sum(Qty)Qty,sum(Qty_Bag)Qty_Bag,Max(Item_Desc)Item_Desc,sum(Amount)Amount  from (" + Qry + ") x group by Item_Code")
            Dim FinalQuery As String = " With CTERawData as ( " + Qry + "  )" + Environment.NewLine + Environment.NewLine
            For ii As Integer = 1 To dtItems.Rows.Count Step 5
                If ii > 1 Then
                    FinalQuery += Environment.NewLine + " Union all " + Environment.NewLine
                End If

                FinalQuery += " select ROW_NUMBER() over (order by (Customer_Code)) As SNo,  max(Location_Desc)Location_Desc,max(Add1)Add1,max(Add2)Add2,max(Document_Date) as Document_Date,"

                FinalQuery += "(Customer_Code)Cust_Code,max(Customer_Name)Customer_Name,'" & objCommonVar.CurrentUser & "' as UserName,'RAJASTHAN CO-OPERATIVE DAIRY FEDERATION LIMITED' as HeadName,'" + txtFromDate.Value + "' as FromDate,'" + txtToDate.Value + "' as ToDate "
                For jj As Integer = 1 To 5
                    Dim strJJ As String = clsCommon.myCstr(jj)
                    Dim strICODE As String = ""
                    Dim strIShortDesc As String = ""
                    Dim strIShortDesBag As String = ""
                    Dim strIQty As Double = 0
                    Dim strIQtyBag As Double = 0
                    If (ii + jj - 1) > dtItems.Rows.Count Then
                        strICODE = ""
                        strIShortDesc = ""
                        strIShortDesBag = ""
                        strIQty = 0
                        strIQtyBag = 0
                    Else
                        strICODE = clsCommon.myCstr(dtItems.Rows(ii + jj - 2)("Item_Code"))
                        strIShortDesc = clsCommon.myCstr(dtItems.Rows(ii + jj - 2)("Item_Desc")) & " Qty"
                        strIShortDesBag = clsCommon.myCstr(dtItems.Rows(ii + jj - 2)("Item_Desc")) & " Bag"
                        strIQty = clsCommon.myCdbl(dtItems.Rows(ii + jj - 2)("Qty"))
                        strIQtyBag = clsCommon.myCdbl(dtItems.Rows(ii + jj - 2)("Qty_Bag"))
                    End If
                    FinalQuery += " ,'" + strICODE + "' as Item_" + strJJ + " ,'" + strIShortDesc + "' as ItemQtyDesc_" + strJJ + " ,'" + strIShortDesBag + "' as ItemBagDesc_" + strJJ + ",
sum(CASE WHEN ITEM_CODE= '" + strICODE + "' THEN Qty_Bag ELSE 0 END) AS ItemQtyBag_" + strJJ + ",SUM(CASE WHEN ITEM_CODE= '" + strICODE + "' THEN Qty ELSE 0 END) AS ItemQty_" + strJJ + ", sum(CASE WHEN ITEM_CODE= '" + strICODE + "' THEN Amount ELSE 0 END) AS ItemAmount_" + strJJ + ""

                Next
                FinalQuery += " from (
select xx.*	from CTERawData xx
) x group by Customer_Code"
            Next
            dtPrint = clsDBFuncationality.GetDataTable(FinalQuery)
            If dtPrint.Rows.Count > 0 Then
                frmCRV.funreport(MyBase.Form_ID, CrystalReportFolder.SalesReport, dtPrint, "rptSalesReportCustomerWise", "Sale Report Customer Wise")
            End If

            frmCRV = Nothing
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub TxtItem__My_Click(sender As Object, e As EventArgs) Handles TxtItem._My_Click
        Dim qry As String = " Select TSPL_Item_Master.Item_Code as Code,TSPL_Item_Master.Item_Desc as Name from TSPL_Item_Master 
                              where 1=1 and FG_for_CF_RPT=1 "
        TxtItem.arrValueMember = clsCommon.ShowMultipleSelectForm("CustMulSel", qry, "Code", "Name", TxtItem.arrValueMember, TxtItem.arrDispalyMember)

    End Sub

    Private Sub cmbReportType_SelectedIndexChanged(sender As Object, e As UI.Data.PositionChangedEventArgs) Handles cmbReportType.SelectedIndexChanged
        If cmbReportType.SelectedIndex > 0 Then
            If cmbReportType.SelectedValue = "CIWH" Then
                btnPrint.Visible = True
                RadGroupBoxFiscalYear.Visible = False
                txtMulFiscalYear.Visible = False
                txtFiscalYear.Visible = False
            ElseIf cmbReportType.SelectedValue = "YMW" Then
                btnPrint.Visible = False
                RadGroupBoxFiscalYear.Visible = True
                txtFiscalYear.Visible = True
                txtMulFiscalYear.Visible = False
            ElseIf cmbReportType.SelectedValue = "YW" Then
                btnPrint.Visible = False
                txtFiscalYear.Visible = False
                txtMulFiscalYear.Visible = True
                RadGroupBoxFiscalYear.Visible = True
            Else
                btnPrint.Visible = False
                txtFiscalYear.Visible = False
                txtMulFiscalYear.Visible = False
                RadGroupBoxFiscalYear.Visible = False
            End If
        End If

    End Sub
    Private Sub txtFiscalYear__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtFiscalYear._MYValidating
        Try
            Dim qry As String = "select Fiscal_Code,Fiscal_Name,Start_Date,End_Date from TSPL_FISCAL_YEAR_MASTER "
            txtFiscalYear.Value = clsCommon.ShowSelectForm("rptACMGTlBal", qry, "Fiscal_Code", "", txtFiscalYear.Value, "", isButtonClicked)
            SetFiscalYear()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Sub SetFiscalYear()
        txtFromDate.MinDate = New Date(2001, 4, 1)
        txtFromDate.MaxDate = New Date(3000, 12, 1)
        txtToDate.MinDate = txtFromDate.MinDate
        txtToDate.MaxDate = txtFromDate.MaxDate
        If clsCommon.myLen(txtFiscalYear.Value) > 0 Then
            Dim qry As String = " select Fiscal_Code,Fiscal_Name,Start_Date,End_Date from TSPL_FISCAL_YEAR_MASTER where Fiscal_Code='" + txtFiscalYear.Value + "' "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt Is Nothing OrElse dt.Rows.Count > 0 Then
                txtFromDate.MinDate = clsCommon.myCDate(dt.Rows(0)("Start_Date"))
                txtFromDate.MaxDate = clsCommon.myCDate(dt.Rows(0)("End_Date"))
                txtToDate.MinDate = txtFromDate.MinDate
                txtToDate.MaxDate = txtFromDate.MaxDate

                txtFromDate.Value = txtFromDate.MinDate
                txtToDate.Value = txtFromDate.MaxDate
            End If
        Else
            txtToDate.Value = clsCommon.GETSERVERDATE()
            If txtToDate.Value.Month >= 1 AndAlso txtToDate.Value.Month <= 3 Then
                txtFromDate.Value = New Date(txtToDate.Value.Year - 1, 4, 1)
            Else
                txtFromDate.Value = New Date(txtToDate.Value.Year, 4, 1)
            End If
        End If
    End Sub

    Private Sub txtMulFiscalYear__My_Click(sender As Object, e As EventArgs) Handles txtMulFiscalYear._My_Click
        Try
            Dim qry As String = "select Fiscal_Code as Code ,Fiscal_Name as Name,Start_Date,End_Date from TSPL_Fiscal_Year_Master "
            txtMulFiscalYear.arrValueMember = clsCommon.ShowMultipleSelectForm("MulFisY", qry, "Code", "Name", txtMulFiscalYear.arrValueMember, txtMulFiscalYear.arrDispalyMember)
            qry += " where Fiscal_Code in (" & clsCommon.GetMulcallString(txtMulFiscalYear.arrValueMember) & ") order by Start_Date,End_Date "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            SetFromToDate(dt)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub SetFromToDate(ByVal dt As DataTable)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            txtFromDate.Value = dt.Rows(0)("Start_Date")
            txtToDate.Value = dt.Rows(dt.Rows.Count - 1)("End_Date")
        End If
    End Sub

    Private Sub Gv1_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles Gv1.CellDoubleClick
        Try
            If e.Column Is Gv1.Columns("Customer Code") OrElse e.Column Is Gv1.Columns("Month Name") OrElse e.Column Is Gv1.Columns("Fiscal_Name") Then
                LoadgvDetailsData()
                RadPageView1.SelectedPage = RadPageViewPage3
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub LoadgvDetailsData()
        Dim qry = Baseqry
        Dim whrcls As String = " where 2=2 "
        If cmbReportType.SelectedValue = "CIWH" Then
            whrcls += " and xx.Customer_Code = '" & clsCommon.myCstr(Gv1.CurrentRow.Cells("Customer Code").Value) & "' "
        ElseIf cmbReportType.SelectedValue = "CIWV" Then
            whrcls += " and xx.Customer_Code = '" & clsCommon.myCstr(Gv1.CurrentRow.Cells("Customer Code").Value) & "'  "
        ElseIf cmbReportType.SelectedValue = "YMW" Then
            whrcls += " and xx.MonthName = '" & clsCommon.myCstr(Gv1.CurrentRow.Cells("Month Name").Value) & "' "
        ElseIf cmbReportType.SelectedValue = "YW" Then
            whrcls += " and xx.Fiscal_Code = '" & clsCommon.myCstr(Gv1.CurrentRow.Cells("Fiscal_Code").Value) & "'  "
        End If
        qry = " select Document_Code AS [Document No],Document_Date as [Document Date],Item_Code as [Item Code],Item_Desc as [Item Desc],Qty,Qty_Bag,Amount from ( " & Baseqry & "  ) xx  LEFT JOIN (select Fiscal_Code,Fiscal_Name,Start_Date,End_Date from TSPL_Fiscal_Year_Master) as TSPL_Fiscal_Year_Master ON CONVERT(DATE, xx.Document_Date, 103) BETWEEN TSPL_Fiscal_Year_Master.Start_Date AND TSPL_Fiscal_Year_Master.End_Date " & whrcls & " order by [Document Date] "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        gvDetails.DataSource = Nothing
        gvDetails.SummaryRowsBottom.Clear()
        gvDetails.GroupDescriptors.Clear()
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            gvDetails.DataSource = dt
            gvDetails.BestFitColumns()
            gvDetails.ShowGroupPanel = False
            gvDetails.EnableFiltering = True
            gvDetails.ReadOnly = True
            gvDetails.Columns("Qty").HeaderText = "Qty(MT)"
            gvDetails.Columns("Qty_Bag").HeaderText = "Qty(Bag)"
            gvDetails.Columns("Qty_Bag").FormatString = "{0:n2}"
            gvDetails.Columns("Qty").FormatString = "{0:n2}"

            Dim summaryRowItem As New GridViewSummaryRowItem()
            For ii As Integer = 4 To gvDetails.Columns.Count - 1
                summaryRowItem.Add(New GridViewSummaryItem(gvDetails.Columns(ii).Name, "{0:F2}", GridAggregateFunction.Sum))
            Next
            gvDetails.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
            gvDetails.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
        End If
    End Sub
End Class