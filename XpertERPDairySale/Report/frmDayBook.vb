Imports System.IO
Imports common

Public Class frmDayBook
#Region "Variables"
    Const colDate As String = "ColDate"
    Const colParticulars As String = "colParticulars"
    Const colVchType As String = "colVchType"
    Const colVchNo As String = "colVchNo"
    Const colDebitAmt As String = "colDebitAmt"
    Const colCreditAmt As String = "colCreditAmt"

#End Region
    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Reset()
    End Sub
    Sub Reset()
        LoadBlankGrid()
        txtfDate.Value = clsCommon.GETSERVERDATE()
        txtToDate.Value = txtfDate.Value
        gvData.DataSource = Nothing
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub
    Sub LoadBlankGrid()
        gvData.Rows.Clear()
        gvData.Columns.Clear()
        Dim repoDate As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoDate.FormatString = ""
        repoDate.HeaderText = "Date"
        repoDate.Name = colDate
        repoDate.Width = 100
        repoDate.HeaderTextAlignment = ContentAlignment.MiddleLeft
        repoDate.TextAlignment = ContentAlignment.MiddleLeft
        repoDate.IsVisible = True
        repoDate.ReadOnly = True
        gvData.MasterTemplate.Columns.Add(repoDate)
        Dim repoPart As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoPart.FormatString = ""
        repoPart.HeaderText = "Particulars"
        repoPart.Name = colParticulars
        repoPart.Width = 250
        repoPart.HeaderTextAlignment = ContentAlignment.MiddleLeft
        repoPart.TextAlignment = ContentAlignment.MiddleLeft
        repoPart.IsVisible = True
        repoPart.ReadOnly = True
        gvData.MasterTemplate.Columns.Add(repoPart)
        Dim repoVchType As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoVchType.FormatString = ""
        repoVchType.HeaderText = "Vch Type"
        repoVchType.Name = colVchType
        repoVchType.Width = 250
        repoVchType.HeaderTextAlignment = ContentAlignment.MiddleLeft
        repoVchType.TextAlignment = ContentAlignment.MiddleLeft
        repoVchType.IsVisible = True
        repoVchType.ReadOnly = True
        gvData.MasterTemplate.Columns.Add(repoVchType)
        Dim repoVchNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoVchNo.FormatString = ""
        repoVchNo.HeaderText = "Vch No"
        repoVchNo.Name = colVchNo
        repoVchNo.Width = 250
        repoVchNo.HeaderTextAlignment = ContentAlignment.MiddleLeft
        repoVchNo.TextAlignment = ContentAlignment.MiddleLeft
        repoVchNo.IsVisible = True
        repoVchNo.ReadOnly = True
        gvData.MasterTemplate.Columns.Add(repoVchNo)
        Dim repoDebitAmt As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoDebitAmt.FormatString = ""
        repoDebitAmt.HeaderText = "Debit Amount"
        repoDebitAmt.Name = colDebitAmt
        repoDebitAmt.Width = 100
        repoDebitAmt.HeaderTextAlignment = ContentAlignment.MiddleRight
        repoDebitAmt.TextAlignment = ContentAlignment.MiddleRight
        repoDebitAmt.IsVisible = True
        repoDebitAmt.ReadOnly = True
        gvData.MasterTemplate.Columns.Add(repoDebitAmt)
        Dim repoCreditAmt As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCreditAmt.FormatString = ""
        repoCreditAmt.HeaderText = "Credit Amount"
        repoCreditAmt.Name = colCreditAmt
        repoCreditAmt.Width = 100
        repoCreditAmt.HeaderTextAlignment = ContentAlignment.MiddleRight
        repoCreditAmt.TextAlignment = ContentAlignment.MiddleRight
        repoCreditAmt.IsVisible = True
        repoCreditAmt.ReadOnly = True
        gvData.MasterTemplate.Columns.Add(repoCreditAmt)
        gvData.AllowDeleteRow = True
        gvData.AllowAddNewRow = False
        gvData.ShowGroupPanel = False
        gvData.AllowColumnReorder = False
        gvData.AllowRowReorder = False
        gvData.EnableSorting = False
        gvData.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gvData.MasterTemplate.ShowRowHeaderColumn = False
        gvData.TableElement.TableHeaderHeight = 40
    End Sub
    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Try
            LoadBlankGrid()

            Dim lstObj As List(Of clsDayBookHead) = clsDayBookHead.GetData(txtfDate.Value)
            Dim rowcount As Integer = 0
            Dim rowItem As Integer = 0
            Dim rowtax As Integer = 1
            Dim totalItemAmt As Integer = 0
            If lstObj IsNot Nothing AndAlso lstObj.Count > 0 Then
                For i As Integer = 0 To lstObj.Count - 1
                    gvData.Rows.AddNew()
                    Dim row1 As GridViewRowInfo = gvData.Rows(rowItem)
                    For Each cell As GridViewCellInfo In row1.Cells
                        cell.Style.Font = New Font("Arial", 10, FontStyle.Bold)
                    Next
                    gvData.Rows(rowItem).Cells(colDate).Value = clsCommon.GetPrintDate(lstObj(i).Document_Date, "dd/MM/yyyy")
                    gvData.Rows(rowItem).Cells(colParticulars).Value = clsCommon.myCstr(lstObj(i).Cust_Name)
                    gvData.Rows(rowItem).Cells(colVchType).Value = clsCommon.myCstr(lstObj(i).TaxGroup)
                    gvData.Rows(rowItem).Cells(colVchNo).Value = clsCommon.myCstr(lstObj(i).InvoiceNo)
                    gvData.Rows(rowItem).Cells(colDebitAmt).Value = clsCommon.myCstr(lstObj(i).DebitAmt)
                    gvData.Rows.AddNew()
                    rowItem += 1
                    Dim row2 As GridViewRowInfo = gvData.Rows(rowItem)
                    For Each cell As GridViewCellInfo In row2.Cells
                        cell.Style.Font = New Font("Arial", 10, FontStyle.Bold)
                    Next
                    gvData.Rows(rowItem).Cells(colParticulars).Value = "SALES GST"
                    gvData.Rows(rowItem).Cells(colCreditAmt).Value = clsCommon.myCstr(lstObj(i).CreditAmt)
                    For Each items As clsDayBookItems In lstObj(i).Arr
                        gvData.Rows.AddNew()
                        rowItem += 1
                        gvData.Rows(rowItem).Cells(colParticulars).Value = clsCommon.myCstr(items.Item_Name)
                        gvData.Rows(rowItem).Cells(colVchType).Value = clsCommon.myCstr(items.Qty) + " " + clsCommon.myCstr(items.UOM) + "  " + clsCommon.myCstr(items.Item_Rate) + " " + clsCommon.myCstr(items.UOM)
                        gvData.Rows(rowItem).Cells(colVchNo).Value = clsCommon.myCstr(items.Amount)
                        totalItemAmt += items.Amount
                    Next
                    If lstObj(i).ArrTax IsNot Nothing AndAlso lstObj(i).ArrTax.Count > 0 Then
                        For Each items As clsDayBookTaxDetail In lstObj(i).ArrTax
                            gvData.Rows.AddNew()
                            rowItem += 1
                            Dim row As GridViewRowInfo = gvData.Rows(rowItem)
                            For Each cell As GridViewCellInfo In row.Cells
                                cell.Style.Font = New Font("Arial", 10, FontStyle.Bold)
                            Next
                            gvData.Rows(rowItem).Cells(colParticulars).Value = clsCommon.myCstr(items.Tax_Name) + " " + clsCommon.myCstr(items.Tax_Rate)

                            gvData.Rows(rowItem).Cells(colCreditAmt).Value = clsCommon.myCstr(items.Tax_Amt)

                        Next
                    End If
                    rowItem += 1
                Next
            End If
            'For Each row As GridViewRowInfo In gvData.Rows
            '    For Each cell As GridViewCellInfo In row.Cells
            '        cell.Style.BorderColor = Color.Transparent
            '        cell.Style.BorderWidth = 0

            '    Next
            'Next

            RadPageView1.SelectedPage = RadPageViewPage2

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub frmDayBook_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Reset()
            lbltoDate.Visible = False
            txtToDate.Visible = False
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub PDFWithImage()
        Dim doc As New CustomPrintDocumentHeaderWithLogo()
        Try
            doc.Margins.Top = 50
            doc.Margins.Bottom = 50
            doc.Margins.Left = 50
            doc.Margins.Right = 50
            doc.HeaderHeight = 150
            doc.AssociatedObject = gvData
            Dim strHeader As String = Me.Text.Replace("/", "")

            doc.LeftHeader = "[Logo]"
            Dim qry As String = "select logo_Img from TSPL_COMPANY_MASTER where Comp_Code='" + objCommonVar.CurrentCompanyCode + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                If (clsCommon.myLen(dt.Rows(0)("logo_Img")) > 0) Then
                    Dim data As Byte() = DirectCast(dt.Rows(0)("logo_Img"), Byte())
                    Dim ms As New MemoryStream(data)
                    doc.Logo = Image.FromStream(ms)
                End If
            End If

            doc.MiddleMiddleText = objCommonVar.CurrentCompanyName
            doc.MiddleMiddleFont = New Font("Arial", 10, FontStyle.Bold)
            doc.MiddleMiddleText = "Day Book"
            doc.MiddleMiddleFont = New Font("Arial", 10, FontStyle.Bold)
            doc.MiddleMiddleText = "Date : " + clsCommon.GetPrintDate(txtfDate.Value, "dd/MM/yyyy")
            doc.MiddleMiddleFont = New Font("Arial", 8, FontStyle.Bold)
            'doc.LeftLowerText = "Date : " + clsCommon.GetPrintDate(txtfDate.Value, "dd/MM/yyyy") '+ " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy")
            'doc.LeftLowerFont = New Font("Arial", 8, FontStyle.Bold)
            'doc.MiddleLowerText = strHeader
            'doc.MiddleLowerFont = New Font("Arial", 10, FontStyle.Bold)
            'doc.RightLowerText = "Run Date: " + clsCommon.myCstr(clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy hh:mm:ss tt"))
            'doc.RightLowerFont = New Font("Arial", 8, FontStyle.Bold)
            doc.LeftFooter = "Print Date & Time: " + clsCommon.myCstr(clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy hh:mm:ss tt"))
            doc.RightFooter = "Page [Page #] of [Total Pages]"

            Dim dialog As New RadPrintPreviewDialog
            dialog.Document = doc
            dialog.ToolMenu.Visible = True
            dialog.ShowDialog()

        Catch ex As Exception
            doc = Nothing
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub PrintPDF()
        Try
            If gvData.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                'arrHeader.Add("Name : Day Book")
                'arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add(("For: " + clsCommon.GetPrintDate(txtfDate.Value, "dd/MM/yyyy")))
                '+ " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")

                'If TxtMultiRoute.arrValueMember IsNot Nothing AndAlso TxtMultiRoute.arrValueMember.Count > 0 Then
                '    arrHeader.Add("Route No : " + clsCommon.GetMulcallString(TxtMultiRoute.arrValueMember))
                'End If
                'If TxtMultiBMC.arrValueMember IsNot Nothing AndAlso TxtMultiBMC.arrValueMember.Count > 0 Then
                '    arrHeader.Add("BMC : " + clsCommon.GetMulcallString(TxtMultiBMC.arrValueMember))
                'End If

                transportSql.applyExportTemplate(gvData, PageSetupReport_ID)
                clsCommon.MyExportToPDF("Day Book", gvData, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
            Else
                common.clsCommon.MyMessageBoxShow(Me, "No Data Found to Export.", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        'PDFWithImage()
        PrintPDF()
    End Sub

    Private Sub gvData_CellFormatting(sender As Object, e As CellFormattingEventArgs) Handles gvData.CellFormatting
        'If e.Column.Name = colDate Then
        '    e.CellElement.Font = New Font("Arial", 10, FontStyle.Bold)
        'End If
        'If e.Column.Name = colDebitAmt Then
        '    e.CellElement.Font = New Font("Arial", 10, FontStyle.Bold)
        'End If
        'If e.Column.Name = colCreditAmt Then
        '    e.CellElement.Font = New Font("Arial", 10, FontStyle.Bold)
        'End If
        e.CellElement.DrawBorder = False
        e.CellElement.RowElement.DrawBorder = False

    End Sub

    Private Sub gvData_PrintCellFormatting(sender As Object, e As PrintCellFormattingEventArgs) Handles gvData.PrintCellFormatting
        Try

            e.PrintCell.DrawBorder = False
            ' e.p.DrawBorder = False
            If (Not (clsCommon.CompairString(clsCommon.myCstr(e.Row.Cells(colDate).Value), "") = CompairStringResult.Equal)) OrElse clsCommon.myCstr(e.Row.Cells(colParticulars).Value).Contains("SALES GST") OrElse clsCommon.myCstr(e.Row.Cells(colParticulars).Value).Contains("CGST") OrElse clsCommon.myCstr(e.Row.Cells(colParticulars).Value).Contains("SGST") OrElse clsCommon.myCstr(e.Row.Cells(colParticulars).Value).Contains("IGST") OrElse clsCommon.myCstr(e.Row.Cells(colParticulars).Value).Contains("KKF") OrElse clsCommon.myCstr(e.Row.Cells(colParticulars).Value).Contains("Tax") OrElse (Not (clsCommon.CompairString(clsCommon.myCstr(e.Row.Cells(colDebitAmt).Value), "") = CompairStringResult.Equal)) OrElse (Not (clsCommon.CompairString(clsCommon.myCstr(e.Row.Cells(colCreditAmt).Value), "") = CompairStringResult.Equal)) Then

                'OrElse clsCommon.myCstr(e.Row.Cells("DCS").Value).Contains("Total Dispatch for Tanker :") OrElse clsCommon.myCstr(e.Row.Cells("DCS").Value).Contains("Receipt at dock for :") OrElse clsCommon.myCstr(e.Row.Cells("DCS").Value).Contains("Total Collection for Date :") OrElse clsCommon.myCstr(e.Row.Cells("DCS").Value).Contains("Total Dispatch for Date :") OrElse clsCommon.myCstr(e.Row.Cells("DCS").Value).Contains("Receipt at Dock for Date :") OrElse clsCommon.myCstr(e.Row.Cells("DCS").Value).Contains("Total Disp-Collection :") OrElse clsCommon.myCstr(e.Row.Cells("DCS").Value).Contains("Loss in Transit :") OrElse clsCommon.myCstr(e.Row.Cells("DCS").Value).Contains("Overall Loss :") Then
                'e.PrintCell.Font = New Font("Arial", 8, FontStyle.Bold)
                e.PrintCell.Font = New Font("Arial", 8.25, FontStyle.Bold)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class