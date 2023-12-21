'Developed By - Abhishek Kumar
'Start Date - 26/6/2012
'End Date - 26/6/2012
'' Anubhooti(2-July-2014) Added Export Permission Against BM00000003016 ''''''''
Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports common
' Update BY abhishek as on 29 oct 2012 4:55 pm For Excel
' by vipin for pdf work on 31/01/2013
Public Class FrmItemWiseDispatchLedger3
    Inherits FrmMainTranScreen
    Dim ds As New DataSet()
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim btnReferesh As Boolean = False
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmItemWiseDispatchLedger3)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
        '' Anubhooti(2-July-2014) Added Export Permission Against BM00000003016 ''''''''
        btnExport.Visible = MyBase.isExport

        'btnSave.Visible = MyBase.isModifyFlag
        'btnPost.Visible = MyBase.isPostFlag
        'btnDelete.Visible = MyBase.isDeleteFlag
    End Sub
    Private Sub FrmItemWiseDispatchLedger3_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
       
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnprint, "Press Ctrl+P Print the Report")
        ButtonToolTip.SetToolTip(btnreset, "Press Alt+R Reset the Window")
        Reset()
    End Sub

  
    Public Sub Printdata(ByVal exporter As EnumExportTo)
        Try
            Dim qry As String
            Dim fromdate As String = clsCommon.myCDate(dtpfromdate.Value, "dd/MM/yyyy")
            Dim Todate As String = clsCommon.myCDate(dtpTodate.Value, "dd/MM/yyyy")
            Dim DocNo As ArrayList = cbgDocument.CheckedValue
            Dim ItemArr As ArrayList = cbgItem.CheckedValue
            Dim locationArr As ArrayList = cbgLocation.CheckedValue
            Dim CustomerArr As ArrayList = cbgCustomer.CheckedValue

            Dim Item As String
            Dim location As String
            Dim Doc As String
            Dim Customer As String
            Dim StrItem As String = Nothing
            Dim Strlocation As String = Nothing
            Dim StrDocNo As String = Nothing
            Dim StrCustomer As String = Nothing

            If cbgCustomer.CheckedValue.Count > 0 Then
                Customer = ("'" + clsCommon.GetMulcallString(CustomerArr) + "'")
                StrCustomer = Customer.Replace("'", "")
            End If
            If cbgDocument.CheckedValue.Count > 0 Then
                Doc = "'" + clsCommon.GetMulcallString(DocNo) + "'"
                StrDocNo = Doc.Replace("'", "")
            End If
            If cbgLocation.CheckedValue.Count > 0 Then
                location = "'" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + "'"
                Strlocation = location.Replace("'", "")
            End If
            If cbgItem.CheckedValue.Count > 0 Then
                Item = "'" + clsCommon.GetMulcallString(ItemArr) + "'"
                StrItem = Item.Replace("'", "")
            End If

            qry = "select convert(varchar,tspl_scrapsale_head.shipment_date,103) as [Date],TSPL_SCRAPINVOICE_HEAD .invoice_No  as [Bill No],tspl_scrapsale_head.cust_Code as Party," & _
                " tspl_scrapsale_head.cust_name as [Name of the Party],tspl_scrapsale_detail.Item_Code as Item,tspl_scrapsale_detail.Item_Desc as [Name Of the Item]," & _
                " convert(varchar,tspl_scrapsale_detail.shipped_Qty) +'  '+ tspl_scrapsale_detail.Unit_code as [Packs Pack Type] ,tspl_scrapsale_detail.price as [Rate (Rs.)],tspl_scrapsale_detail.shipped_Qty as [Qty]," & _
                " tspl_scrapsale_detail.shipped_Qty as [Net Wt.],(TSPL_SCRAPSALE_DETAIL  .ItemNetAmt )as [Basic Amount]," & _
                "  (Case when tax1 .Type ='E'then TSPL_SCRAPSALE_DETAIL.TAX1_Amt  else 0 end+Case when tax2.Type ='E'then TSPL_SCRAPSALE_DETAIL.TAX2_Amt  else 0 end+Case when tax3 .Type ='E'then TSPL_SCRAPSALE_DETAIL.TAX3_Amt  else 0 end+Case when tax4 .Type ='E'then TSPL_SCRAPSALE_DETAIL.TAX4_Amt  else 0 end+Case when tax5 .Type ='E'then TSPL_SCRAPSALE_DETAIL.TAX5_Amt  else 0 end+Case when tax6 .Type ='E'then TSPL_SCRAPSALE_DETAIL.TAX6_Amt  else 0 end+Case when tax7 .Type ='E'then TSPL_SCRAPSALE_DETAIL.TAX7_Amt  else 0 end+Case when tax8 .Type ='E'then TSPL_SCRAPSALE_DETAIL.TAX8_Amt  else 0 end+Case when tax9 .Type ='E'then TSPL_SCRAPSALE_DETAIL.TAX9_Amt  else 0 end+Case when tax10 .Type ='E'then TSPL_SCRAPSALE_DETAIL.TAX10_Amt  else 0 end)as Excise," & _
                "  (Case when tax1 .Type ='V'then TSPL_SCRAPSALE_DETAIL.TAX1_Amt  else 0 end+Case when tax2.Type ='V'then TSPL_SCRAPSALE_DETAIL.TAX2_Amt  else 0 end+Case when tax3 .Type ='V'then TSPL_SCRAPSALE_DETAIL.TAX3_Amt  else 0 end+Case when tax4 .Type ='V'then TSPL_SCRAPSALE_DETAIL.TAX4_Amt  else 0 end+Case when tax5 .Type ='V'then TSPL_SCRAPSALE_DETAIL.TAX5_Amt  else 0 end+Case when tax6 .Type ='V'then TSPL_SCRAPSALE_DETAIL.TAX6_Amt  else 0 end+Case when tax7 .Type ='V'then TSPL_SCRAPSALE_DETAIL.TAX7_Amt  else 0 end+Case when tax8 .Type ='V'then TSPL_SCRAPSALE_DETAIL.TAX8_Amt  else 0 end+Case when tax9 .Type ='V'then TSPL_SCRAPSALE_DETAIL.TAX9_Amt  else 0 end+Case when tax10 .Type ='V'then TSPL_SCRAPSALE_DETAIL.TAX10_Amt  else 0 end)as VAT," & _
                " (Case when tax1 .Type ='C'then TSPL_SCRAPSALE_DETAIL.TAX1_Amt  else 0 end+Case when tax2.Type ='C'then TSPL_SCRAPSALE_DETAIL.TAX2_Amt  else 0 end+Case when tax3 .Type ='C'then TSPL_SCRAPSALE_DETAIL.TAX3_Amt  else 0 end+Case when tax4 .Type ='C'then TSPL_SCRAPSALE_DETAIL.TAX4_Amt  else 0 end+Case when tax5 .Type ='C'then TSPL_SCRAPSALE_DETAIL.TAX5_Amt  else 0 end+Case when tax6 .Type ='C'then TSPL_SCRAPSALE_DETAIL.TAX6_Amt  else 0 end+Case when tax7 .Type ='C'then TSPL_SCRAPSALE_DETAIL.TAX7_Amt  else 0 end+Case when tax8 .Type ='C'then TSPL_SCRAPSALE_DETAIL.TAX8_Amt  else 0 end+Case when tax9 .Type ='C'then TSPL_SCRAPSALE_DETAIL.TAX9_Amt  else 0 end+Case when tax10 .Type ='C'then TSPL_SCRAPSALE_DETAIL.TAX10_Amt  else 0 end)as CST," & _
                "  (Case when tax1 .Type ='A'then TSPL_SCRAPSALE_DETAIL.TAX1_Amt  else 0 end+Case when tax2.Type ='A'then TSPL_SCRAPSALE_DETAIL.TAX2_Amt  else 0 end+Case when tax3 .Type ='A'then TSPL_SCRAPSALE_DETAIL.TAX3_Amt  else 0 end+Case when tax4 .Type ='A'then TSPL_SCRAPSALE_DETAIL.TAX4_Amt  else 0 end+Case when tax5 .Type ='A'then TSPL_SCRAPSALE_DETAIL.TAX5_Amt  else 0 end+Case when tax6 .Type ='A'then TSPL_SCRAPSALE_DETAIL.TAX6_Amt  else 0 end+Case when tax7 .Type ='A'then TSPL_SCRAPSALE_DETAIL.TAX7_Amt  else 0 end+Case when tax8 .Type ='A'then TSPL_SCRAPSALE_DETAIL.TAX8_Amt  else 0 end+Case when tax9 .Type ='A'then TSPL_SCRAPSALE_DETAIL.TAX9_Amt  else 0 end+Case when tax10 .Type ='A'then TSPL_SCRAPSALE_DETAIL.TAX10_Amt  else 0 end)as ADDTAX," & _
                "  (Case when tax1 .Type ='O'then TSPL_SCRAPSALE_DETAIL.TAX1_Amt  else 0 end+Case when tax2.Type ='O'then TSPL_SCRAPSALE_DETAIL.TAX2_Amt  else 0 end+Case when tax3 .Type ='O'then TSPL_SCRAPSALE_DETAIL.TAX3_Amt  else 0 end+Case when tax4 .Type ='O'then TSPL_SCRAPSALE_DETAIL.TAX4_Amt  else 0 end+Case when tax5 .Type ='O'then TSPL_SCRAPSALE_DETAIL.TAX5_Amt  else 0 end+Case when tax6 .Type ='O'then TSPL_SCRAPSALE_DETAIL.TAX6_Amt  else 0 end+Case when tax7 .Type ='O'then TSPL_SCRAPSALE_DETAIL.TAX7_Amt  else 0 end+Case when tax8 .Type ='O'then TSPL_SCRAPSALE_DETAIL.TAX8_Amt  else 0 end+Case when tax9 .Type ='O'then TSPL_SCRAPSALE_DETAIL.TAX9_Amt  else 0 end+Case when tax10 .Type ='O'then TSPL_SCRAPSALE_DETAIL.TAX10_Amt  else 0 end)as OTHER," & _
                "  TSPL_SCRAPSALE_DETAIL .TotalAmt as [Amt(Rs.)]  from tspl_scrapsale_detail left outer join tspl_scrapsale_head on tspl_scrapsale_detail.shipment_No = tspl_scrapsale_head.shipment_no inner join TSPL_SCRAPINVOICE_HEAD on TSPL_SCRAPINVOICE_HEAD.shipment_No =TSPL_SCRAPSALE_HEAD .shipment_No  left outer join TSPL_TAX_MASTER as tax1 on tax1.tax_code =TSPL_SCRAPSALE_DETAIL   .tax1 left outer join tspl_tax_master as tax2 on tax2.tax_code = TSPL_SCRAPSALE_DETAIL  .tax2 left outer join tspl_tax_master as tax3 on tax3.Tax_Code=TSPL_SCRAPSALE_DETAIL   .TAX3 left outer join TSPL_TAX_MASTER as tax4 on tax4.Tax_Code= TSPL_SCRAPSALE_DETAIL   .tax4 left outer join TSPL_TAX_MASTER as tax5 on tax5.Tax_Code=TSPL_SCRAPSALE_DETAIL   .tax5 left outer join TSPL_TAX_MASTER as tax6 on tax6.Tax_Code =TSPL_SCRAPSALE_DETAIL   .TAX6 left outer join TSPL_TAX_MASTER as tax7 on tax7.Tax_Code =TSPL_SCRAPSALE_DETAIL   .TAX7 left outer join TSPL_TAX_MASTER as tax8 on tax8.Tax_Code =TSPL_SCRAPSALE_DETAIL   .TAX8 left outer join TSPL_TAX_MASTER as tax9 on tax9.Tax_Code =TSPL_SCRAPSALE_DETAIL   .TAX9 left outer join TSPL_TAX_MASTER as tax10 on tax10.Tax_Code =TSPL_SCRAPSALE_DETAIL   .TAX10 " & _
                "   where Convert(Date,TSPL_SCRAPSALE_HEAD .shipment_Date,103) >=Convert(Date,'" & dtpfromdate.Value & "',103) and Convert(Date,TSPL_SCRAPSALE_HEAD .shipment_Date,103) <=Convert(Date,'" & Todate & "',103) and TSPL_SCRAPSALE_HEAD.Doc_Type='S'"
            'If chkDoc_select.IsChecked = True AndAlso cbgDocument.CheckedValue.Count <= 0 Then
            '    common.clsCommon.MyMessageBoxShow("Please select atleast one Document")
            '    Return
            'ElseIf chkDoc_select.IsChecked = True AndAlso cbgDocument.CheckedValue.Count > 0 Then
            '    qry += " and tspl_scrapsale_head.shipment_no in (" + clsCommon.GetMulcallString(DocNo) + ")"
            'End If
            'If chkLocationSelect.IsChecked = True AndAlso cbgLocation.CheckedValue.Count <= 0 Then
            '    common.clsCommon.MyMessageBoxShow("Please select atleast one Location")
            '    Return
            'ElseIf chkLocationSelect.IsChecked = True AndAlso cbgLocation.CheckedValue.Count > 0 Then
            '    qry += " and tspl_scrapsale_head.Loc_Code in (Select Location_Code  from TSPL_LOCATION_MASTER Where Location_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + "))"
            'End If

            'If itemselect.IsChecked = True AndAlso cbgItem.CheckedValue.Count <= 0 Then
            '    common.clsCommon.MyMessageBoxShow("Please select atleast one Item")
            '    Return
            'ElseIf itemselect.IsChecked = True AndAlso cbgItem.CheckedValue.Count > 0 Then
            '    qry += " and tspl_scrapsale_detail.Item_Code in (" + clsCommon.GetMulcallString(ItemArr) + ")"
            'End If

            'If chkCustSelect.IsChecked = True AndAlso cbgCustomer.CheckedValue.Count <= 0 Then
            '    common.clsCommon.MyMessageBoxShow("Please select atleast one Customer")
            '    Return
            'ElseIf chkCustSelect.IsChecked = True AndAlso cbgCustomer.CheckedValue.Count > 0 Then
            '    qry += " and tspl_scrapsale_head.cust_Code  in (" + clsCommon.GetMulcallString(CustomerArr) + ")"
            'End If
            '====added by shivani
            If clsCommon.myLen(clsCommon.myCstr(ddltype.SelectedValue)) > 0 Then
                If clsCommon.CompairString(clsCommon.myCstr(ddltype.SelectedText), "Cash Sale") = CompairStringResult.Equal Then
                    qry += " and TSPL_SCRAPSALE_HEAD.Is_CashSale='Y' "
                Else
                    qry += " and TSPL_SCRAPSALE_HEAD.Is_Scrap='Y' "
                End If
            End If

            If txtDocNo.arrValueMember IsNot Nothing AndAlso txtDocNo.arrValueMember.Count > 0 Then
                qry += "  and tspl_scrapsale_head.shipment_no in (" + clsCommon.GetMulcallString(txtDocNo.arrValueMember) + ") " + Environment.NewLine
            End If
            If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                qry += " and tspl_scrapsale_head.Loc_Code in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ") " + Environment.NewLine
            End If
            If txtItem.arrValueMember IsNot Nothing AndAlso txtItem.arrValueMember.Count > 0 Then
                qry += "  and tspl_scrapsale_detail.Item_Code in (" + clsCommon.GetMulcallString(txtItem.arrValueMember) + ") " + Environment.NewLine
            End If
            If txtCustomer.arrValueMember IsNot Nothing AndAlso txtCustomer.arrValueMember.Count > 0 Then
                qry += "  and tspl_scrapsale_head.cust_Code  in (" + clsCommon.GetMulcallString(txtCustomer.arrValueMember) + ") " + Environment.NewLine
            End If
            qry += "  order by TSPL_SCRAPSALE_HEAD.shipment_Date "
            '=================
            Dim dtgv As New DataTable
            dtgv = clsDBFuncationality.GetDataTable(qry)
            gv.DataSource = Nothing
            gv.Rows.Clear()
            gv.Columns.Clear()
            If dtgv IsNot Nothing And dtgv.Rows.Count > 0 Then
                gv.DataSource = dtgv
                gv.GroupDescriptors.Clear()
                gv.MasterTemplate.SummaryRowsBottom.Clear()
                gv.EnableFiltering = True
                FormatGrid()
                RadPageView1.SelectedPage = RadPageViewPage2
            Else
                clsCommon.MyMessageBoxShow("No Data Found", Me.Text)
            End If
            If btnReferesh = False Then
                'Dim str As String = "ItemWiseDispatchLedger Report"
                Dim arr As New List(Of String)()
                'arr.Add("ItemWiseDispatchLedger Report")
                arr.Add("  From Date:  " + fromdate + "  To Date: " + Todate + "")
                arr.Add("Company : " + objCommonVar.CurrentCompanyName)
                'If Strlocation <> "" Then
                '    arr.Add(" Location:   " + Strlocation + "")
                'End If
                If txtLocation.arrDispalyMember IsNot Nothing AndAlso txtLocation.arrDispalyMember.Count > 0 Then
                    arr.Add(" Location : " + clsCommon.GetMulcallStringWithComma(txtLocation.arrDispalyMember))
                End If
                If StrItem <> "" Then
                    arr.Add(" Item:  " + StrItem + "")
                End If
                If StrCustomer <> "" Then
                    arr.Add(" Customer:  " + StrCustomer + "")
                End If
                If StrDocNo <> "" Then
                    arr.Add(" Document:   " + StrDocNo + "")
                End If

                ' clsCommon.MyExportToExcel(str, gv, arr, "ItemWiseDispatchLedger Report")
                If exporter = EnumExportTo.Excel Then
                    clsCommon.MyExportToExcelGrid("ItemWiseDispatchLedger Report", gv, arr, Me.Text)
                Else
                    clsCommon.MyExportToPDF("ItemWiseDispatchLedger Report", gv, arr, Me.Text, True)
                End If
                'ExporttoMyExcel(qry, Me)
            End If
            ' RadPageView1.SelectedPage = RadPageViewPage2

        Catch ex As Exception

        End Try
    End Sub
    Sub FormatGrid()
        gv.TableElement.TableHeaderHeight = 25
        gv.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gv.Columns.Count - 1
            gv.Columns(ii).ReadOnly = True
            gv.Columns(ii).IsVisible = False
        Next

        gv.Columns("Date").IsVisible = True
        gv.Columns("Date").Width = 100
        gv.Columns("Date").HeaderText = "Date"
        gv.Columns("Date").FormatString = "{0:d}"

        gv.Columns("Bill No").IsVisible = True
        gv.Columns("Bill No").Width = 100
        gv.Columns("Bill No").HeaderText = "Bill No"


        gv.Columns("Party").IsVisible = True
        gv.Columns("Party").Width = 100
        gv.Columns("Party").HeaderText = "Party"

        gv.Columns("Name of the Party").IsVisible = True
        gv.Columns("Name of the Party").Width = 100
        gv.Columns("Name of the Party").HeaderText = "Name of the Party"


        gv.Columns("Item").IsVisible = True
        gv.Columns("Item").Width = 100
        gv.Columns("Item").HeaderText = "Item"



        gv.Columns("Name Of the Item").IsVisible = True
        gv.Columns("Name Of the Item").Width = 100
        gv.Columns("Name Of the Item").HeaderText = "Name Of the Item"


        gv.Columns("Packs Pack Type").IsVisible = True
        gv.Columns("Packs Pack Type").Width = 100
        gv.Columns("Packs Pack Type").HeaderText = "Packs Pack Type"


        gv.Columns("Rate (Rs.)").IsVisible = True
        gv.Columns("Rate (Rs.)").Width = 100
        gv.Columns("Rate (Rs.)").HeaderText = "Rate (Rs.)"

        gv.Columns("Qty").IsVisible = True
        gv.Columns("Qty").Width = 150
        gv.Columns("Qty").HeaderText = "Qty"


        gv.Columns("Net Wt.").IsVisible = True
        gv.Columns("Net Wt.").Width = 150
        gv.Columns("Net Wt.").HeaderText = "Net Wt."

        gv.Columns("Basic Amount").IsVisible = True
        gv.Columns("Basic Amount").Width = 100
        gv.Columns("Basic Amount").HeaderText = "Basic Amount"

        gv.Columns("Excise").IsVisible = True
        gv.Columns("Excise").Width = 100
        gv.Columns("Excise").HeaderText = "Excise"

        gv.Columns("VAT").IsVisible = True
        gv.Columns("VAT").Width = 100
        gv.Columns("VAT").HeaderText = "VAT"

        gv.Columns("CST").IsVisible = True
        gv.Columns("CST").Width = 100
        gv.Columns("CST").HeaderText = "CST" '

        gv.Columns("ADDTAX").IsVisible = True
        gv.Columns("ADDTAX").Width = 100
        gv.Columns("ADDTAX").HeaderText = "ADDTAX"

        gv.Columns("OTHER").IsVisible = True
        gv.Columns("OTHER").Width = 100
        gv.Columns("OTHER").HeaderText = "OTHER"
        gv.Columns("Amt(Rs.)").IsVisible = True
        gv.Columns("Amt(Rs.)").Width = 100
        gv.Columns("Amt(Rs.)").HeaderText = "Amt(Rs.)"

        
        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim intCount As Integer = 0

        Dim item1 As New GridViewSummaryItem("Amt(Rs.)", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)
        Dim item2 As New GridViewSummaryItem("Excise", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item2)
        Dim item3 As New GridViewSummaryItem("VAT", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item3)
        Dim item4 As New GridViewSummaryItem("CST", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item4)
        Dim item5 As New GridViewSummaryItem("ADDTAX", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item5)
        Dim item6 As New GridViewSummaryItem("OTHER", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item6)
        Dim item7 As New GridViewSummaryItem("Basic Amount", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item7)


        gv.ShowGroupPanel = False
        gv.MasterTemplate.AutoExpandGroups = True
        gv.EnableFiltering = True
        gv.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

    End Sub
    Public Sub FillGridView(ByVal sql As String, ByVal gv As RadGridView)
        Dim bs As New BindingSource()
        ds = connectSql.RunSQLReturnDS(sql)
        bs.DataSource = ds.Tables(0)
        gv.DataSource = bs
    End Sub
    Public Function ExporttoMyExcel(ByVal sql As String, ByVal frm As RadForm) As Boolean
        Dim sfd As SaveFileDialog = New SaveFileDialog()
        Dim Fullpath As String
        Dim path As String
        sfd.FileName = frm.Text

        sfd.Filter = "Excel Workbooks (*.xls;*.xlsx)|*.xls;*.xlsx"
        If sfd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            path = sfd.FileName
            Fullpath = path
        Else
            Return False
        End If



        If Not path.Equals(String.Empty) Then
            Dim gv As New RadGridView()
            Try
                ''''' Dim exporter As New RadGridViewExcelExporter()
                gv.Name = "gTax"
                frm.Controls.Add(gv)
                FillGridView(sql, gv)
                If gv.Rows.Count = 0 Then
                    common.clsCommon.MyMessageBoxShow("There is no data for Show Excel Report.", Me.Text)
                    Return False
                End If
                Dim i As Integer = 0
                For i = 0 To gv.ColumnCount - 1
                    Dim grow As GridViewRowInfo = TryCast(gv.Rows(0), GridViewRowInfo)
                    If TypeOf grow.Cells(i).Value Is DateTime Then
                        Dim datecol As GridViewDateTimeColumn = TryCast(gv.Columns(i), GridViewDateTimeColumn)
                        datecol.ExcelExportType = DisplayFormatType.ShortDate
                    End If
                    If TypeOf grow.Cells(i).Value Is Decimal Then
                        Dim datecol As GridViewDecimalColumn = TryCast(gv.Columns(i), GridViewDecimalColumn)
                        datecol.ExcelExportType = DisplayFormatType.Standard
                    End If
                Next i
                '    exporter.Export(gv, path, frm.Text)

                Dim exporter As New ExportToExcelML(gv)
                AddHandler exporter.ExcelCellFormatting, AddressOf exporter_ExcelCellFormatting
                exporter.ExportHierarchy = True
                ' exporter.ExportVisualSettings = True
                exporter.SheetMaxRows = ExcelMaxRows._65536
                exporter.SheetName = frm.Text
                exporter.RunExport(Fullpath)

                frm.Controls.Remove(gv)
                '' Added By Abhishek For Show Excel Without save.

                Dim xlsApp As Microsoft.Office.Interop.Excel.Application
                Dim xlsWB As Microsoft.Office.Interop.Excel.Workbook
                xlsApp = New Microsoft.Office.Interop.Excel.Application
                xlsApp.Visible = True
                xlsWB = xlsApp.Workbooks.Open(Fullpath)
                'common.clsCommon.MyMessageBoxShow("Excel Report Created!", "Export", MessageBoxButtons.OK)
                Return True
            Catch ex As Exception
                frm.Controls.Remove(gv)
                common.clsCommon.MyMessageBoxShow("No Report Created.", "Export Error", MessageBoxButtons.OK)
                Return False
            End Try
        End If
    End Function
    Private Sub exporter_ExcelCellFormatting(ByVal sender As Object, ByVal e As ExcelML.ExcelCellFormattingEventArgs)
        If e.GridRowInfoType Is GetType(GridViewTableHeaderRowInfo) Then
            e.ExcelStyleElement.FontStyle.Bold = True
            e.ExcelStyleElement.FontStyle.Size = 8
        End If

        'e.ExcelStyleElement.FontStyle.Bold = False
        e.ExcelStyleElement.FontStyle.Size = 8

    End Sub

    Private Sub chkDocAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkdocAll.ToggleStateChanged, chkDoc_select.ToggleStateChanged
        cbgDocument.Enabled = Not chkdocAll.IsChecked
    End Sub
    Private Sub chkLocationAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocationAll.ToggleStateChanged
        cbgLocation.Enabled = False
    End Sub
    Private Sub chkLocationSelect_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocationSelect.ToggleStateChanged
        cbgLocation.Enabled = True
    End Sub

    Private Sub itemall_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles itemall.ToggleStateChanged
        cbgItem.Enabled = Not itemall.IsChecked
    End Sub
    Private Sub chkcustAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkcustAll.ToggleStateChanged
        cbgCustomer.Enabled = Not chkcustAll.IsChecked
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub btnreset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnreset.Click
        Reset()
    End Sub
    Sub Reset()
        dtpfromdate.Value = clsCommon.GETSERVERDATE()
        dtpTodate.Value = clsCommon.GETSERVERDATE()
        LoadDocuemntNo()
        LoadLocation()
        LoadItem()
        LoadCustomer()
        chkdocAll.IsChecked = True
        itemall.IsChecked = True
        chkLocationAll.IsChecked = True
        chkcustAll.IsChecked = True
        txtCustomer.arrValueMember = Nothing
        txtDocNo.arrValueMember = Nothing
        txtLocation.arrValueMember = Nothing
        txtItem.arrValueMember = Nothing
        RadPageView1.SelectedPage = RadPageViewPage1
        gv.DataSource = Nothing
        Get_Type()
    End Sub

    Private Sub btnprint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnprint.Click
        ' Printdata()
    End Sub
    Private Sub dtpToDate_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim fromdate As String = clsCommon.myCDate(dtpfromdate.Value, "dd/MM/yyyy")
        Dim Todate As String = clsCommon.myCDate(dtpTodate.Value, "dd/MM/yyyy")
        Dim qry As String = "Select shipment_no as Code ,shipment_date as [Shipment Date] from TSPL_SCRAPSALE_HEAD where  Convert(Date,TSPL_SCRAPSALE_HEAD .shipment_Date,103) >=Convert(Date,'" & dtpfromdate.Value & "',103) and Convert(Date,TSPL_SCRAPSALE_HEAD .shipment_Date,103) <=Convert(Date,'" & Todate & "',103)"
        cbgDocument.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgDocument.ValueMember = "Code"

    End Sub

    Private Sub dtpfromdate_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim fromdate As String = clsCommon.myCDate(dtpfromdate.Value, "dd/MM/yyyy")
        Dim Todate As String = clsCommon.myCDate(dtpTodate.Value, "dd/MM/yyyy")
        Dim qry As String = "Select shipment_no as Code ,shipment_date as [Shipment Date] from TSPL_SCRAPSALE_HEAD where Convert(Date,TSPL_SCRAPSALE_HEAD .shipment_Date,103) >=Convert(Date,'" & dtpfromdate.Value & "',103) and Convert(Date,TSPL_SCRAPSALE_HEAD .shipment_Date,103) <=Convert(Date,'" & Todate & "',103)"
        cbgDocument.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgDocument.ValueMember = "Code"


    End Sub

    Private Sub FrmItemWiseDispatchLedger3_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt And e.KeyCode = Keys.P Then
            Printdata(EnumExportTo.Excel)
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.R Then
            Reset()
        End If
    End Sub

    'Private Sub btnExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcel.Click
    '    Printdata(EnumExportTo.Excel)
    'End Sub

    'Private Sub btnPDF_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPDF.Click
    '    Printdata(EnumExportTo.PDF)
    'End Sub
  
    Private Sub btnExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcel.Click
        btnReferesh = False
        Printdata(EnumExportTo.Excel)
    End Sub

    Private Sub btnPDF_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPDF.Click
        btnReferesh = False
        Printdata(EnumExportTo.PDF)
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        btnReferesh = True
        Printdata(EnumExportTo.Refresh)
    End Sub
    Sub LoadDocuemntNo()
        cbgDocument.DataSource = Nothing
        Dim fromdate As String = clsCommon.myCDate(dtpfromdate.Value, "dd/MM/yyyy")
        Dim Todate As String = clsCommon.myCDate(dtpTodate.Value, "dd/MM/yyyy")
        Dim qry As String = "Select shipment_no as Code ,shipment_date as [Shipment Date] from TSPL_SCRAPSALE_HEAD where   Convert(Date,TSPL_SCRAPSALE_HEAD .shipment_Date,103) >=Convert(Date,'" & dtpfromdate.Value & "',103) and Convert(Date,TSPL_SCRAPSALE_HEAD .shipment_Date,103) <=Convert(Date,'" & Todate & "',103) AND Doc_Type='S'"
        cbgDocument.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgDocument.ValueMember = "Code"

    End Sub
    Public Sub LoadLocation()
        Dim Qry As String = "select Location_Code as Code, Location_Desc as Description from TSPL_LOCATION_MASTER Where Location_Type='Physical'"
        'Dim qry As String = " select Segment_code as Code, Description  from TSPL_GL_SEGMENT_CODE where Seg_No='7' "
        cbgLocation.DataSource = clsDBFuncationality.GetDataTable(Qry)
        cbgLocation.ValueMember = "Code"
    End Sub

    Sub LoadItem()
        Dim qry As String = "select Item_Code as[Item Code],item_desc as [Description]  from TSPL_ITEM_MASTER "
        cbgItem.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgItem.ValueMember = "Item Code"
        cbgItem.DisplayMember = "Description"
    End Sub
    Public Sub Get_Type()
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow = dt.NewRow()
        dr("Code") = ""
        dr("Name") = "Select"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Scrap Sale"
        dr("Name") = "Scrap Sale"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Cash Sale"
        dr("Name") = "Cash Sale"
        dt.Rows.Add(dr)

        ddltype.DataSource = dt
        ddltype.ValueMember = "Code"
        ddltype.DisplayMember = "Name"
    End Sub
    Sub LoadCustomer()
        Dim qry As String = "select Cust_Code as Code ,Customer_Name as [Customer Name] from TSPL_CUSTOMER_MASTER "
        cbgCustomer.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgCustomer.ValueMember = "Code"
        cbgCustomer.DisplayMember = "Customer Name"
    End Sub
    Private Sub txtDocNo__My_Click(sender As Object, e As EventArgs) Handles txtDocNo._My_Click
        Dim fromdate As String = clsCommon.myCDate(dtpfromdate.Value, "dd/MM/yyyy")
        Dim Todate As String = clsCommon.myCDate(dtpTodate.Value, "dd/MM/yyyy")
        Dim qry As String = "Select shipment_no as Code ,shipment_date as Date from TSPL_SCRAPSALE_HEAD where   Convert(Date,TSPL_SCRAPSALE_HEAD .shipment_Date,103) >=Convert(Date,'" & dtpfromdate.Value & "',103) and Convert(Date,TSPL_SCRAPSALE_HEAD .shipment_Date,103) <=Convert(Date,'" & Todate & "',103) and Doc_Type='S'"
        txtDocNo.arrValueMember = clsCommon.ShowMultipleSelectForm("TransTypeMulSel", qry, "Code", "Date", txtDocNo.arrValueMember, txtDocNo.arrDispalyMember)
    End Sub

    Private Sub txtLocation__My_Click(sender As Object, e As EventArgs) Handles txtLocation._My_Click
        Dim qry As String = " select Location_Code as Code, Location_Desc as Name from TSPL_LOCATION_MASTER Where Location_Type='Physical'"
        txtLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("TransTypeMulSel", qry, "Code", "Name", txtLocation.arrValueMember, txtLocation.arrDispalyMember)
        FrmPendingRequisitionQty.SetDiplayMember(txtLocation, "Location_Desc", "TSPL_LOCATION_MASTER", "Location_Code")
    End Sub

    Private Sub txtCustomer__My_Click(sender As Object, e As EventArgs) Handles txtCustomer._My_Click
        Dim qry As String = "select Cust_Code as Code ,Customer_Name as  Name from TSPL_CUSTOMER_MASTER "
        txtCustomer.arrValueMember = clsCommon.ShowMultipleSelectForm("TransTypeMulSel", qry, "Code", "Name", txtCustomer.arrValueMember, txtCustomer.arrDispalyMember)
    End Sub

    Private Sub txtItem__My_Click(sender As Object, e As EventArgs) Handles txtItem._My_Click
        Dim qry As String = "select Item_Code as Code,item_desc as Name  from TSPL_ITEM_MASTER "
        txtItem.arrValueMember = clsCommon.ShowMultipleSelectForm("TransTypeMulSel", qry, "Code", "Name", txtItem.arrValueMember, txtItem.arrDispalyMember)
    End Sub

    Private Sub btnQuickExport_Click(sender As Object, e As EventArgs) Handles btnQuickExport.Click
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()

            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(dtpfromdate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(dtpTodate.Value, "dd/MM/yyyy")) + " ")
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.frmItemWiseDispatchLedger3 & "'"))
            If txtDocNo.arrDispalyMember IsNot Nothing AndAlso txtDocNo.arrDispalyMember.Count > 0 Then
                arrHeader.Add("Document No : " + clsCommon.GetMulcallStringWithComma(txtDocNo.arrDispalyMember))
            End If

            If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                arrHeader.Add(" Location : " + clsCommon.GetMulcallStringWithComma(txtLocation.arrDispalyMember))
            End If
            If txtCustomer.arrDispalyMember IsNot Nothing AndAlso txtCustomer.arrDispalyMember.Count > 0 Then
                arrHeader.Add(" Customer : " + clsCommon.GetMulcallStringWithComma(txtCustomer.arrDispalyMember))
            End If
            If txtItem.arrDispalyMember IsNot Nothing AndAlso txtItem.arrDispalyMember.Count > 0 Then
                arrHeader.Add(" Item : " + clsCommon.GetMulcallStringWithComma(txtItem.arrDispalyMember))
            End If

            'Dim sfd As SaveFileDialog = New SaveFileDialog()
            'Dim filePath As String
            'sfd.FileName = Me.Text
            'sfd.Filter = "Excel 97-2003 (*.xls) |*.xls;|Excel 2007 (*.xlsx)|*.xlsx;|CSV Files (*.csv) |*.csv"
            'If sfd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            '    filePath = sfd.FileName
            'Else
            '    Exit Sub
            'End If
            'transportSql.exportdataChilRows(gv, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
            'common.clsCommon.MyMessageBoxShow("Exported Successfully.")
            'Process.Start(filePath)
            transportSql.QuickExportToExcel(gv, "", Me.Text, , arrHeader)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class
