Imports common
Imports System.Globalization
Imports System.Data.SqlClient
Imports System
Imports System.IO
Imports Telerik.WinControls.UI.Export

Public Class rptSchemeSaleReport
    Inherits FrmMainTranScreen

#Region "Variable"
    Dim AreaWiseBilling As Boolean = False
    Dim StrPermission As String
#End Region

    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        LoadData()
    End Sub

    Private Sub LoadData()
        Try
            Dim qry As String = ""
            Dim qry1 As String = ""
            Dim whrcls As String = ""
            Dim strShift As String = ""
            Dim whrclsShift As String = ""
            Dim itemNames1 As String = Nothing
            Dim SchitemNames1 As String = Nothing
            Dim ItemName7 As String = Nothing
            Dim SchItemName7 As String = Nothing
            Dim ItemName89 As String = Nothing
            Dim SchItemName89 As String = Nothing

            If txtDistributor.arrValueMember IsNot Nothing Then
                whrcls += "  TSPL_SD_SALE_INVOICE_HEAD.Customer_Code In (" + clsCommon.GetMulcallString(txtDistributor.arrValueMember) + ")"
            End If

            qry = "SELECT max(TSPL_ITEM_MASTER.Item_Code)Item_Code,
                max(TSPL_ITEM_MASTER.Short_Description)Short_Description,max(TSPL_ITEM_MASTER.Sku_Seq)Sku_Seq
            FROM TSPL_SD_SALE_INVOICE_DETAIL 
			left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code  left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code = TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE             left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SALE_INVOICE_HEAD.Customer_Code
            where   TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Item='N' "
            If txtDistributor.arrValueMember IsNot Nothing Then
                qry += "  and TSPL_SD_SALE_INVOICE_HEAD.Customer_Code In (" + clsCommon.GetMulcallString(txtDistributor.arrValueMember) + ")"
            End If
            If rdbMilk.Checked Then
                qry += " and TSPL_ITEM_MASTER.Is_FreshItem = 1  "
            ElseIf rdbProduct.Checked Then
                qry += " and TSPL_ITEM_MASTER.Is_Ambient = 1 "
            End If

            qry += " and convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) >=Convert(date,'" & txtFromDate.Value & "',103) 
            and convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) <= Convert(date,'" & txtToDate.Value & "',103) 
			group by TSPL_ITEM_MASTER.Item_Code"

            qry1 = "SELECT max(TSPL_ITEM_MASTER.Item_Code)Item_Code,
                max(TSPL_ITEM_MASTER.Short_Description)Short_Description,max(TSPL_ITEM_MASTER.Sku_Seq)Sku_Seq
            FROM TSPL_SD_SALE_INVOICE_DETAIL 
			left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code  left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code = TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE             left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SALE_INVOICE_HEAD.Customer_Code
            where   TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Item='Y' "
            If txtDistributor.arrValueMember IsNot Nothing Then
                qry1 += "  and TSPL_SD_SALE_INVOICE_HEAD.Customer_Code In (" + clsCommon.GetMulcallString(txtDistributor.arrValueMember) + ")"
            End If
            If rdbMilk.Checked Then
                qry1 += " and TSPL_ITEM_MASTER.Is_FreshItem = 1  "
            ElseIf rdbProduct.Checked Then
                qry1 += " and TSPL_ITEM_MASTER.Is_Ambient = 1 "
            End If
            qry1 += " and convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) >=Convert(date,'" & txtFromDate.Value & "',103) 
            and convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) <= Convert(date,'" & txtToDate.Value & "',103) 
			group by TSPL_ITEM_MASTER.Item_Code"

            Dim dtitemName As DataTable = clsDBFuncationality.GetDataTable(qry)
            Dim dtSchemeItem As DataTable = clsDBFuncationality.GetDataTable(qry1)
            If dtitemName.Rows.Count > 0 Then
                For i As Integer = 0 To dtitemName.Rows.Count - 1
                    If i = 0 Then
                        itemNames1 += "[" + clsCommon.myCstr(dtitemName.Rows(i)("Item_Code")) + "] "
                        ItemName7 += "Sum(IsNull([" + clsCommon.myCstr(dtitemName.Rows(i)("Item_Code")) + "],0)) As [" + clsCommon.myCstr(dtitemName.Rows(i)("Short_Description")) + "]"
                        ItemName89 += " Sum(ISNULL([" + clsCommon.myCstr(dtitemName.Rows(i)("Item_Code")) + "],0)"
                    Else
                        itemNames1 += ", [" + clsCommon.myCstr(dtitemName.Rows(i)("Item_Code")) + "] "
                        ItemName7 += ", Sum(IsNull([" + clsCommon.myCstr(dtitemName.Rows(i)("Item_Code")) + "],0)) As [" + clsCommon.myCstr(dtitemName.Rows(i)("Short_Description")) + "]"
                        ItemName89 += "+" + " ISNULL([" + clsCommon.myCstr(dtitemName.Rows(i)("Item_Code")) + "],0)"
                    End If
                Next
            Else
                gv1.DataSource = Nothing
                clsCommon.MyMessageBoxShow(Me, "No data found to display", Me.Text)
                gv1.Rows.Clear()
                gv1.Columns.Clear()
                gv1.DataSource = Nothing
                Exit Sub
            End If

            If dtSchemeItem.Rows.Count > 0 Then
                For i As Integer = 0 To dtSchemeItem.Rows.Count - 1
                    If i = 0 Then
                        SchitemNames1 += "[" + clsCommon.myCstr(dtSchemeItem.Rows(i)("Short_Description")) + "] "
                        SchItemName7 += "Sum(IsNull([" + clsCommon.myCstr(dtSchemeItem.Rows(i)("Short_Description")) + "],0)) As [" + clsCommon.myCstr(dtSchemeItem.Rows(i)("Short_Description")) + "]"
                        SchItemName89 += " Sum(ISNULL([" + clsCommon.myCstr(dtSchemeItem.Rows(i)("Short_Description")) + "],0)"
                    Else
                        SchitemNames1 += ", [" + clsCommon.myCstr(dtSchemeItem.Rows(i)("Short_Description")) + "] "
                        SchItemName7 += ", Sum(IsNull([" + clsCommon.myCstr(dtSchemeItem.Rows(i)("Short_Description")) + "],0)) As [" + clsCommon.myCstr(dtSchemeItem.Rows(i)("Short_Description")) + "]"
                        SchItemName89 += "+" + " ISNULL([" + clsCommon.myCstr(dtSchemeItem.Rows(i)("Short_Description")) + "],0)"
                    End If
                Next
            Else
                gv1.DataSource = Nothing
                clsCommon.MyMessageBoxShow(Me, "No data found to display", Me.Text)
                gv1.Rows.Clear()
                gv1.Columns.Clear()
                gv1.DataSource = Nothing
                Exit Sub
            End If

            Dim BaseQry As String = ""
            BaseQry = " Select max(Document_Code)Document_Code, CONVERT(varchar(10), Document_Date, 103) AS Document_Date,max(Customer_Code)Customer_Code,
" & ItemName7 & "," & ItemName89 & ") AS [Total Qty]," & SchItemName7 & "," & SchItemName89 & ") AS [Total Scheme Qty]

from (Select max(Document_Code)Document_Code,Document_Date,max(Customer_Code)Customer_Code,(Item_Code)Item_Code,max(Item_Code1)Item_Code1,max(Item_Desc1)Item_Desc1,
sum(Qty)Qty,sum(Qty1)Qty1 from  (Select TSPL_SD_SALE_INVOICE_HEAD.Document_Code,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,TSPL_SD_SALE_INVOICE_HEAD.Customer_Code,
TSPL_ITEM_MASTER.Item_Code as Item_Code,case when Scheme_Item='Y' then TSPL_ITEM_MASTER.Item_Desc end as Item_Code1,case when Scheme_Item='Y' then TSPL_ITEM_MASTER.item_desc end as Item_Desc1,
case when Scheme_Item='Y' then TSPL_SD_SALE_INVOICE_DETAIL.Qty end as Qty1,
TSPL_SD_SALE_INVOICE_DETAIL.Qty
 from TSPL_SD_SALE_INVOICE_DETAIL
left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code=TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE
left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code
left outer join TSPL_ITEM_UOM_DETAIL ON tspl_item_uom_detail.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code and tspl_item_uom_detail.UOM_Code= TSPL_SD_SALE_INVOICE_DETAIL.Unit_code
Left OUTER Join TSPL_CUSTOMER_MASTER On TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_SD_SALE_INVOICE_HEAD.Customer_Code
Left outer join TSPL_ZONE_MASTER on TSPL_ZONE_MASTER.zone_code = TSPL_CUSTOMER_MASTER.zone_code 
		 Left outer join TSPL_ROUTE_MASTER on TSPL_ROUTE_MASTER.Route_No = TSPL_SD_SALE_INVOICE_HEAD.Route_No
LEFT JOIN  ( select item_code,uom_code,conversion_factor,UOM_Description from  TSPL_ITEM_UOM_DETAIL where Report_UOM = 1 ) as  Report_UOM ON TSPL_SD_SALE_INVOICE_DETAIL.Item_Code = Report_UOM.item_code 
where convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) >= CONVERT(DATE,'" & txtFromDate.Value & "', 103) 
and   convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) <= CONVERT(DATE, '" & txtToDate.Value & "', 103)"

            If txtDistributor.arrValueMember IsNot Nothing Then
                BaseQry += "  and TSPL_SD_SALE_INVOICE_HEAD.Customer_Code In (" + clsCommon.GetMulcallString(txtDistributor.arrValueMember) + ")"
            End If

            BaseQry += " )xx group by document_date,Item_Code)	xxy

PIVOT (SUM(qty)  For item_code In (" & itemNames1 & ") ) As pivot_fresh
PIVOT (SUM(qty1)  For item_code1 In (" & SchitemNames1 & ") ) As pivot_Scheme group by  CONVERT(varchar(10), Document_Date, 103) order by CONVERT(varchar(10), Document_Date, 103) asc "

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(BaseQry)
            gv1.DataSource = Nothing
            gv1.Rows.Clear()
            gv1.Columns.Clear()
            gv1.GroupDescriptors.Clear()
            gv1.MasterView.Refresh()
            gv1.GroupDescriptors.Clear()
            gv1.EnableFiltering = True
            gv1.MasterTemplate.SummaryRowsBottom.Clear()
            If dt.Rows.Count > 0 Then
                gv1.DataSource = dt
                gv1.BestFitColumns()
                'View()
                SetGridFormation()
                'ReStoreGridLayout()
                gv1.MasterTemplate.AutoExpandGroups = True
                RadPageView1.SelectedPage = RadPageViewPage2
                gv1.BestFitColumns()
                EnableDisableControls(False)
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub

            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub SetGridFormation()
        gv1.TableElement.TableHeaderHeight = 40
        gv1.MasterTemplate.ShowRowHeaderColumn = True
        For ii As Integer = 0 To gv1.Columns.Count - 1
            gv1.Columns(ii).ReadOnly = True
            gv1.Columns(ii).IsVisible = True
            'gv1.Columns(ii).FormatString = "{0:n2}"
        Next
        gv1.Columns("Document_Code").HeaderText = "Document"
        gv1.Columns("Document_Code").IsVisible = False
        gv1.Columns("Customer_Code").HeaderText = "Customer Code"
        gv1.Columns("Customer_Code").IsVisible = False
        gv1.Columns("Document_Date").HeaderText = "Document Date"
        'gv1.Columns("Document_Date").DefaultCellStyle.Format = "dd/MM/yyyy"


        Dim summaryRowItem As New GridViewSummaryRowItem()
        For ii As Integer = 3 To gv1.Columns.Count - 1
            'gv1.Columns(ii).FormatString = "{0:n2}"
            summaryRowItem.Add(New GridViewSummaryItem(gv1.Columns(ii).Name, "{0:F2}", GridAggregateFunction.Sum))
        Next
        gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
    End Sub

    Private Sub rptSchemeSaleReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = clsCommon.GETSERVERDATE()
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        funreset()
    End Sub

    Sub funreset()
        EnableDisableControls(True)
        gv1.DataSource = Nothing
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub
    Private Sub EnableDisableControls(ByVal val As Boolean)
        RadGroupBox1.Enabled = val
    End Sub

    Private Sub RadMenuItem4_Click(sender As Object, e As EventArgs) Handles RadMenuItem4.Click
        Try
            If gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")
                'arrHeader.Add(objCommonVar.CurrentCompanyName)
                clsCommon.MyExportToExcelGrid("", gv1, arrHeader, Me.Text)
                'transportSql.exportdata(gv1, "", Me.Text, False, arrHeader, False, False, True)
            Else
                clsCommon.MyMessageBoxShow(Me, "No data found to export", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadMenuItem5_Click(sender As Object, e As EventArgs) Handles RadMenuItem5.Click
        Try
            If gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                'arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                'arrHeader.Add(objCommonVar.CurrentCompanyName)
                'arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")
                clsCommon.MyExportToPDF(Me.Text, gv1, arrHeader, Me.Text)
            Else
                clsCommon.MyMessageBoxShow(Me, "No data found to export", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub txtDistributor__My_Click(sender As Object, e As EventArgs) Handles txtDistributor._My_Click
        Try
            Dim qry As String = " Select Cust_Code As Code , Customer_Name As Name  from TSPL_CUSTOMER_MASTER  "
            txtDistributor.arrValueMember = clsCommon.ShowMultipleSelectForm("LedgerRoute", qry, "Code", "Name", txtDistributor.arrValueMember, txtDistributor.arrDispalyMember)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class