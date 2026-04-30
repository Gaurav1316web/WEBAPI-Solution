Imports common
Imports System.IO
Public Class rptPartWiseItemReport
    Inherits FrmMainTranScreen

    Private Sub rptPartWiseItemReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            txtfDate.Value = clsCommon.GETSERVERDATE()
            txtToDate.Value = txtfDate.Value
            txtMultiCustomer.arrValueMember = Nothing
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtMultiCustomer__My_Click(sender As Object, e As EventArgs) Handles txtMultiCustomer._My_Click
        Dim qry As String = " select cust_code as [Code], Customer_Name as [Name] from tspl_customer_master "
        txtMultiCustomer.arrValueMember = clsCommon.ShowMultipleSelectForm("CustMulSel", qry, "Code", "Name", txtMultiCustomer.arrValueMember, txtMultiCustomer.arrDispalyMember)
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Reset()
    End Sub
    Private Sub reset()
        txtMultiCustomer.arrValueMember = Nothing
        gvData.DataSource = Nothing
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        PageSetupReport_ID = MyBase.Form_ID
        TemplateGridview = gvData
        LoadData()
    End Sub
    Public Sub LOADDATA()
        Dim strtxtfDate As String = clsCommon.GetPrintDate(txtfDate.Value, "dd/MMM/yyyy")
        Dim strToDate As String = clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy")
        Dim qry As String = ""
        Dim dt As DataTable = Nothing
        Dim SumItemCode As String = Nothing
        Dim SumItemCode1 As String = Nothing
        Dim ItemCode As String = Nothing
        Dim ItemDesc As String = Nothing
        Dim itemqry As String = "  Select distinct TSPL_SD_SALE_INVOICE_DETAIL.Item_Code,TSPL_ITEM_MASTER.Item_Desc,Sku_Seq
                                        from TSPL_SD_SALE_INVOICE_DETAIL 
                                        Left outer join  TSPL_SD_SALE_INVOICE_HEAD ON TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE = TSPL_SD_SALE_INVOICE_HEAD.Document_Code
		                                Left outer join  TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code = TSPL_SD_SALE_INVOICE_DETAIL.Item_Code
		 	                            where 2=2  and CONVERT(DATE,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) >= convert(date,'" & clsCommon.GetPrintDate(txtfDate.Value, "dd/MMM/yyyy") & "',103)
                                        AND CONVERT(DATE,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) <=convert(date,'" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") & "',103) "
        If txtMultiCustomer.arrValueMember IsNot Nothing AndAlso txtMultiCustomer.arrValueMember.Count > 0 Then
            itemqry += "  and TSPL_SD_SALE_INVOICE_HEAD.Customer_Code in (" + clsCommon.GetMulcallString(txtMultiCustomer.arrValueMember) + ")"
        End If
        itemqry += " Order By Sku_Seq"
        Dim dtitem As DataTable = clsDBFuncationality.GetDataTable(itemqry)
        If dtitem.Rows.Count > 0 Then
            For i As Integer = 0 To dtitem.Rows.Count - 1
                If i = 0 Then
                    SumItemCode1 += " Sum(IsNull([" + clsCommon.myCstr(dtitem.Rows(i)("Item_Code")) + "],0)) "

                    SumItemCode += ", Sum(IsNull([" + clsCommon.myCstr(dtitem.Rows(i)("Item_Code")) + "],0)) As [" + clsCommon.myCstr(dtitem.Rows(i)("Item_Desc")) + "]"

                    'SumItemCode += " Sum(IsNull([" + clsCommon.myCstr(dtitem.Rows(i)("Item_Code")) + "],0)) As [" + clsCommon.myCstr(dtitem.Rows(i)("Item_Desc")) + " Qty (" + clsCommon.myCstr(dtitem.Rows(i)("UOM_Code")) + ")],Sum(IsNull([" + clsCommon.myCstr(dtitem.Rows(i)("Item_Desc")) + "],0)) As [" + clsCommon.myCstr(dtitem.Rows(i)("Item_Desc")) + " Amt]"
                    ItemCode += "[" + clsCommon.myCstr(dtitem.Rows(i)("Item_Code")) + "] "
                    ItemDesc += "[" + clsCommon.myCstr(dtitem.Rows(i)("Item_Desc")) + "]"
                Else
                    SumItemCode1 += "+" + " Sum(IsNull([" + clsCommon.myCstr(dtitem.Rows(i)("Item_Code")) + "],0))"
                    SumItemCode += ", Sum(IsNull([" + clsCommon.myCstr(dtitem.Rows(i)("Item_Code")) + "],0)) As [" + clsCommon.myCstr(dtitem.Rows(i)("Item_Desc")) + "]"

                    'SumItemCode += ", Sum(IsNull([" + clsCommon.myCstr(dtitem.Rows(i)("Item_Code")) + "],0)) As [" + clsCommon.myCstr(dtitem.Rows(i)("Item_Desc")) + " Qty (" + clsCommon.myCstr(dtitem.Rows(i)("UOM_Code")) + ")],Sum(IsNull([" + clsCommon.myCstr(dtitem.Rows(i)("Item_Desc")) + "],0)) As [" + clsCommon.myCstr(dtitem.Rows(i)("Item_Desc")) + " Amt]"
                    ItemCode += ", [" + clsCommon.myCstr(dtitem.Rows(i)("Item_Code")) + "] "
                    ItemDesc += ", [" + clsCommon.myCstr(dtitem.Rows(i)("Item_Desc")) + "]"
                End If
            Next
        End If
        Dim whrcls As String = Nothing
        If dtitem.Rows.Count > 0 Then
            qry = " Select convert(Varchar,Document_date,103) as [Document Date],Customer_Code AS [Party Code],Customer_Name as [Party Name]
" & SumItemCode & " ," & SumItemCode1 & " AS Total_Amount from 
(select TSPL_ITEM_MASTER.item_desc, TSPL_SD_SALE_INVOICE_HEAD.Customer_Code,TSPL_CUSTOMER_MASTER.Customer_Name,TSPL_SD_SALE_INVOICE_HEAD.Document_date,(TSPL_ITEM_MASTER.Item_Desc)Item_Code1,TSPL_SD_SALE_INVOICE_DETAIL.item_code, TSPL_SD_SALE_INVOICE_DETAIL.amt_less_discount, --
TSPL_SD_SALE_INVOICE_DETAIL.document_code,TSPL_SD_SALE_INVOICE_DETAIL.qty
from TSPL_SD_SALE_INVOICE_DETAIL
left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.document_code=TSPL_SD_SALE_INVOICE_DETAIL.document_code
LEFT JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_SD_SALE_INVOICE_HEAD.Customer_Code
LEFT JOIN TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code = TSPL_SD_SALE_INVOICE_DETAIL.Item_Code
LEFT JOIN TSPL_ITEM_UOM_DETAIL ON TSPL_ITEM_UOM_DETAIL.Item_Code = TSPL_ITEM_MASTER.Item_Code AND TSPL_ITEM_UOM_DETAIL.Report_UOM = 1 
where 2=2 AND CONVERT(DATE,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) >= convert(date,'" & clsCommon.GetPrintDate(txtfDate.Value, "dd/MMM/yyyy") & "',103)
  AND CONVERT(DATE,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) <=convert(date,'" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") & "',103) "
            If txtMultiCustomer.arrValueMember IsNot Nothing AndAlso txtMultiCustomer.arrValueMember.Count > 0 Then
                qry += "  and TSPL_CUSTOMER_MASTER.Cust_Code in (" + clsCommon.GetMulcallString(txtMultiCustomer.arrValueMember) + ")"
            End If
        End If
        qry += ") XX  PIVOT (SUM(Qty)  For Item_Code In (" & ItemCode & ") ) As pivot_Code   GROUP BY  convert(Varchar,Document_date,103),Customer_Code,Customer_Name"

        dt = clsDBFuncationality.GetDataTable(qry)
        gvData.GroupDescriptors.Clear()
        gvData.MasterTemplate.SummaryRowsBottom.Clear()
        gvData.DataSource = Nothing
        If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
            Exit Sub
        Else
            RadPageView1.SelectedPage = RadPageViewPage2
            gvData.GroupDescriptors.Clear()
            gvData.MasterTemplate.SummaryRowsBottom.Clear()
            gvData.DataSource = dt
            gvData.AutoExpandGroups = True
            gvData.ShowGroupPanel = True
            gvData.ShowRowHeaderColumn = False
            gvData.AllowAddNewRow = False
            gvData.AllowDeleteRow = False
            gvData.EnableFiltering = True
            gvData.ShowFilteringRow = True
            SetGridFormationOFGV1Collection()

            ' SetGridFormat()
            'SetGridFormationOFGV1()
            gvData.BestFitColumns()
        End If
    End Sub
    Sub SetGridFormationOFGV1Collection()
        gvData.TableElement.TableHeaderHeight = 40
        gvData.MasterTemplate.ShowRowHeaderColumn = False
        Dim summaryRowItem As New GridViewSummaryRowItem()
        For ii As Integer = 0 To gvData.Columns.Count - 1
            gvData.Columns(ii).ReadOnly = True
            gvData.Columns(ii).IsVisible = True
            gvData.Columns("Total_Amount").IsVisible = True
            gvData.Columns("Total_Amount").HeaderText = "Total Amount"
        Next
        Dim summaryRowItemB As New GridViewSummaryRowItem()
        Dim Total_Amount As New GridViewSummaryItem("Total_Amount", "{0:n2}", GridAggregateFunction.Sum)
        summaryRowItemB.Add(Total_Amount)

        Dim index As Integer = 2
        For ii As Integer = index To gvData.Columns.Count - 1
            summaryRowItem.Add(New GridViewSummaryItem(gvData.Columns(ii).Name, "{0:F2}", GridAggregateFunction.Sum))
        Next
        gvData.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        gvData.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom

        'gvData.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
        gvData.AutoSizeRows = True
        gvData.BestFitColumns()
        gvData.MasterTemplate.AutoExpandGroups = True
    End Sub

    Private Sub SplitContainer1_Panel2_Paint(sender As Object, e As PaintEventArgs) Handles SplitContainer1.Panel2.Paint

    End Sub

    Private Sub rmenuExport_Click(sender As Object, e As EventArgs) Handles rmenuExport.Click
        Export(EnumExportTo.Excel)
    End Sub
    Private Sub Export(ByVal exporter As EnumExportTo)
        Try
            If gvData.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptPartWiseItemReport & "'"))
                If txtMultiCustomer.arrValueMember IsNot Nothing AndAlso txtMultiCustomer.arrValueMember.Count > 0 Then
                    arrHeader.Add(" Customer : " + clsCommon.GetMulcallStringWithComma(txtMultiCustomer.arrDispalyMember))
                End If

                transportSql.QuickExportToExcel(gvData, "", Me.Text, , arrHeader)
            Else
                clsCommon.MyMessageBoxShow(Me, "No data found to export", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub rmenuPDF_Click(sender As Object, e As EventArgs) Handles rmenuPDF.Click
        If gvData.Rows.Count > 0 Then
            ExporttoExcel(EnumExportTo.PDF)
        Else
            RadMessageBox.Show("No Data Found to Display", Me.Text)
        End If
    End Sub
    Private Sub ExportToExcel(ByVal exporter As EnumExportTo)
        Try
            Dim arrHeader As List(Of String) = New List(Of String)()
            Dim strtemp As String = "Date Range : " + clsCommon.GetPrintDate(txtfDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")
            arrHeader.Add(strtemp)
            arrHeader.Add("Company : " + objCommonVar.CurrentCompanyName)
            If txtMultiCustomer.arrValueMember IsNot Nothing AndAlso txtMultiCustomer.arrValueMember.Count > 0 Then
                arrHeader.Add(" Customer : " + clsCommon.GetMulcallStringWithComma(txtMultiCustomer.arrDispalyMember))
            End If
            If exporter = EnumExportTo.Excel Then
                clsCommon.MyExportToExcelGrid("Party wise Item Report", gvData, arrHeader, Me.Text)
            Else
                clsCommon.MyExportToPDF("Party wise Item Report", gvData, arrHeader, "Party wise Item Report", PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub
End Class