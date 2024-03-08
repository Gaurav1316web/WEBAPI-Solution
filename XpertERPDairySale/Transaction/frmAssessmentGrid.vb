Imports common
Imports System.IO
Imports System.ComponentModel
Imports Telerik.WinControls.UI.Export

Public Class frmAssessmentGrid
    Public ReportID As String = ""
    Public Type As Integer = 0
    Public IDate As Date
    Dim lvl As Integer
    Private Sub FrmFreeGrid_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ReportID = Me.Name
        If clsCommon.myLen(ReportID) <= 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "Report ID Not found", Me.Text)
            Me.Close()
            Exit Sub
        End If
        lvl = 1
        loadData()
    End Sub

    Dim strShift As String
    Dim strItemType As String
    Dim strItem As String

    Sub loadData()
        Dim BaseQry As String = " select * from (
select TSPL_DEMAND_BOOKING_DETAIL.ShiftType, case when (TSPL_ITEM_MASTER.Is_FreshItem =1  and isnull(TSPL_ITEM_MASTER.CAN,0)=0  and isnull(TSPL_ITEM_MASTER.CRATE,0)=0  and isnull(TSPL_ITEM_MASTER.Is_Milk_Pouch,0)=1) then 'Milk' else (case when (TSPL_ITEM_MASTER.Is_Ambient=1  and isnull(TSPL_ITEM_MASTER.CAN,0)=0  and isnull(TSPL_ITEM_MASTER.CRATE,0)=0) then 'Product' else '' end) end as ItemType,TSPL_DEMAND_BOOKING_MASTER.Route_No,TSPL_ROUTE_MASTER.Route_Desc,TSPL_DEMAND_BOOKING_DETAIL.Item_Code,TSPL_ITEM_MASTER.Item_Desc,TSPL_DEMAND_BOOKING_DETAIL.Qty,TSPL_DEMAND_BOOKING_DETAIL.Unit_code, TSPL_DEMAND_BOOKING_DETAIL.TotalCrates_ItemWise,TSPL_DEMAND_BOOKING_DETAIL.TotalLtr_ItemWise,TSPL_DEMAND_BOOKING_DETAIL.ItemNetAmount
 from TSPL_DEMAND_BOOKING_DETAIL
left outer join TSPL_DEMAND_BOOKING_MASTER on TSPL_DEMAND_BOOKING_MASTER.Document_No=TSPL_DEMAND_BOOKING_DETAIL.Document_No
left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_DEMAND_BOOKING_DETAIL.Item_Code
left outer join TSPL_ROUTE_MASTER on TSPL_ROUTE_MASTER.Route_No=TSPL_DEMAND_BOOKING_MASTER.Route_No
where convert(date, TSPL_DEMAND_BOOKING_MASTER.Document_Date,103)='" + clsCommon.GetPrintDate(IDate, "dd/MMM/yyyy") + "'
)xx where  len(ItemType)>0"
        Dim qry As String = ""
        If lvl = 1 Then
            qry = "  select ShiftType,ItemType
 ,sum(Qty * case when ItemType='Milk' then 0 else 1  end) as Qty
 ,sum(TotalLtr_ItemWise) as [QtyLTR]
 ,sum(TotalCrates_ItemWise) as [QtyCrate]
 ,sum(ItemNetAmount) as Amt from (" + BaseQry + ")xxx group by ItemType,ShiftType  "
        ElseIf lvl = 2 Then
            qry = "select Item_Code,max(Item_Desc) as Item_Desc
,sum(Qty * case when ItemType='Milk' then 0 else 1  end) as Qty
,max(Unit_code ) as Unit_code
,sum(TotalLtr_ItemWise) as [QtyLTR]
 ,sum(TotalCrates_ItemWise) as [QtyCrate]
 ,sum(ItemNetAmount) as Amt from (" + BaseQry + " and ShiftType='" + strShift + "' and ItemType='" + strItemType + "' )xxx group by Item_Code  "
        ElseIf lvl = 3 Then
            qry = "select Route_No,max(Route_Desc) as Route_Desc
,sum(Qty * case when ItemType='Milk' then 0 else 1  end) as Qty
,max(Unit_code ) as Unit_code
,sum(TotalLtr_ItemWise) as [QtyLTR]
 ,sum(TotalCrates_ItemWise) as [QtyCrate]
 ,sum(ItemNetAmount) as Amt from (" + BaseQry + " and ShiftType='" + strShift + "' and ItemType='" + strItemType + "' and Item_Code='" + strItem + "' )xxx group by Route_No"
        End If

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            gv1.DataSource = Nothing
            gv1.DataSource = dt
            For ii As Integer = 0 To gv1.Columns.Count - 1
                gv1.Columns(ii).ReadOnly = True
                gv1.Columns(ii).IsVisible = True
                gv1.Columns(ii).Width = 100
                gv1.Columns(ii).BestFit()
            Next

            ReStoreGridLayout()
            gv1.ShowGroupPanel = False
            gv1.AllowAddNewRow = False
            gv1.ShowGroupPanel = False
            gv1.AllowColumnReorder = False
            gv1.AllowRowReorder = False
            gv1.EnableSorting = False
            gv1.ShowFilteringRow = True
            gv1.EnableFiltering = True
            gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
            gv1.MasterTemplate.ShowRowHeaderColumn = False

            gv1.MasterTemplate.SummaryRowsBottom.Clear()
            If lvl = 1 Then
                gv1.Columns("ShiftType").HeaderText = "Shift"
                gv1.Columns("ItemType").HeaderText = "Item Type"
                gv1.Columns("Qty").HeaderText = "Qty"
                gv1.Columns("QtyLTR").HeaderText = "Ltr"
                gv1.Columns("QtyCrate").HeaderText = "Crate"
                gv1.Columns("Amt").HeaderText = "Amount"

                MyLabel1.Text = "Dated [" + clsCommon.GetPrintDate(IDate, "dd/MM/yyyy") + "]"
            ElseIf lvl = 2 Then
                gv1.Columns("Item_Code").HeaderText = "Item Code"
                gv1.Columns("Item_Desc").HeaderText = "Item"

                gv1.Columns("Qty").HeaderText = "Qty"
                gv1.Columns("Qty").IsVisible = (clsCommon.CompairString(strItemType, "Product") = CompairStringResult.Equal)
                gv1.Columns("Unit_code").HeaderText = "UOM"
                gv1.Columns("Unit_code").IsVisible = (clsCommon.CompairString(strItemType, "Product") = CompairStringResult.Equal)

                gv1.Columns("QtyLTR").HeaderText = "Ltr"
                gv1.Columns("QtyLTR").IsVisible = (clsCommon.CompairString(strItemType, "Milk") = CompairStringResult.Equal)
                gv1.Columns("QtyCrate").HeaderText = "Crate"
                gv1.Columns("QtyCrate").IsVisible = (clsCommon.CompairString(strItemType, "Milk") = CompairStringResult.Equal)
                gv1.Columns("Amt").HeaderText = "Amount"

                MyLabel1.Text = "Dated [" + clsCommon.GetPrintDate(IDate, "dd/MM/yyyy") + "] Shift [" + strShift + "] Item Type [" + strItemType + "]"
            ElseIf lvl = 3 Then
                gv1.Columns("Route_No").HeaderText = "Item Code"
                gv1.Columns("Route_Desc").HeaderText = "Item"

                gv1.Columns("Qty").HeaderText = "Qty"
                gv1.Columns("Qty").IsVisible = (clsCommon.CompairString(strItemType, "Product") = CompairStringResult.Equal)
                gv1.Columns("Unit_code").HeaderText = "UOM"
                gv1.Columns("Unit_code").IsVisible = (clsCommon.CompairString(strItemType, "Product") = CompairStringResult.Equal)

                gv1.Columns("QtyLTR").HeaderText = "Ltr"
                gv1.Columns("QtyLTR").IsVisible = (clsCommon.CompairString(strItemType, "Milk") = CompairStringResult.Equal)
                gv1.Columns("QtyCrate").HeaderText = "Crate"
                gv1.Columns("QtyCrate").IsVisible = (clsCommon.CompairString(strItemType, "Milk") = CompairStringResult.Equal)
                gv1.Columns("Amt").HeaderText = "Amount"

                MyLabel1.Text = "Dated [" + clsCommon.GetPrintDate(IDate, "dd/MM/yyyy") + "] Shift [" + strShift + "] Item Type [" + strItemType + "] Item [" + strItem + "]"
            End If

            Dim summaryRowItem As New GridViewSummaryRowItem()

            Dim item1 As GridViewSummaryItem
            item1 = New GridViewSummaryItem("Qty", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item1)
            item1 = New GridViewSummaryItem("QtyLTR", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item1)
            item1 = New GridViewSummaryItem("QtyCrate", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item1)
            item1 = New GridViewSummaryItem("Amt", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item1)

            gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        End If
    End Sub

    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(ReportID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(ReportID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gv1.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gv1.Columns.Count - 1 Step ii + 1
                        gv1.Columns(ii).IsVisible = False
                        gv1.Columns(ii).VisibleInColumnChooser = True
                    Next

                    gv1.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub

    Private Sub RadButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadButton1.Click
        Me.Close()
    End Sub

    Private Sub RadMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem2.Click
        If clsCommon.myLen(ReportID) > 0 Then
            gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = ReportID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv1.SaveLayout(obj.GridLayout)
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            obj.GridColumns = gv1.ColumnCount
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully",  Me.Text)
            End If
            ''stuti regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If

    End Sub

    Private Sub RadMenuItem3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem3.Click
        clsGridLayout.DeleteData(ReportID, objCommonVar.CurrentUserCode)
    End Sub

    Private Sub Export(ByVal Type As Integer)
        If SaveFileDialog.ShowDialog() <> System.Windows.Forms.DialogResult.OK Then
            Return
        End If
        If SaveFileDialog.FileName.Equals(String.Empty) Then
            RadMessageBox.SetThemeName(gv1.ThemeName)
            ''sfd.Filter = "Excel (*.xls;*.xlsx)|*.xls;*.xlsx"
            common.clsCommon.MyMessageBoxShow(Me, "Please enter a file name.", Me.Text)
            Return
        End If
        Dim fileName As String = Me.SaveFileDialog.FileName

        Try
            Select Case Type

                Case 1 'export to excelML       
                    fileName += ".xls"
                    RunExportToExcelML(fileName)
                Case 2 'export to CSV      
                    fileName += ".csv"
                    RunExportToCSV(fileName)
                    'Case 3 'export to HTML                    
                    '    RunExportToHTML(fileName, openExportFile)
                Case 4 'export to PDF   
                    fileName += ".pdf"
                    RunExportToPDF(fileName)
            End Select

            If common.clsCommon.MyMessageBoxShow(Me, "Open the Exported file", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                Try

                    System.Diagnostics.Process.Start(fileName)
                Catch ex As Exception
                    Dim message As String = String.Format("The file cannot be opened on your system." & Constants.vbLf & "Error message: {0}", ex.Message)
                    common.clsCommon.MyMessageBoxShow(Me, message, "Open File", MessageBoxButtons.OK, RadMessageIcon.Error)
                End Try
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RunExportToExcelML(ByVal fileName As String)
        Dim excelExporter As New ExportToExcelML(gv1)
        excelExporter.SheetName = Me.Text
        excelExporter.SheetMaxRows = ExcelMaxRows._1048576
        excelExporter.SummariesExportOption = SummariesOption.ExportAll
        'excelExporter.ExportVisualSettings = Me.exportVisualSettings
        Try
            excelExporter.RunExport(fileName)


        Catch ex As IOException

            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub RunExportToCSV(ByVal fileName As String)
        Dim csvExporter As New ExportToCSV(gv1)
        csvExporter.SummariesExportOption = SummariesOption.ExportAll
        Try
            csvExporter.RunExport(fileName)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub RunExportToPDF(ByVal fileName As String)
        Dim pdfExporter As New ExportToPDF(gv1)
        pdfExporter.PdfExportSettings.Title = "My PDF Title"
        pdfExporter.PdfExportSettings.PageWidth = 297
        pdfExporter.PdfExportSettings.PageHeight = 210
        pdfExporter.PageTitle = Me.Text
        pdfExporter.FitToPageWidth = True
        Try
            pdfExporter.RunExport(fileName)
            RadMessageBox.SetThemeName(gv1.ThemeName)
        Catch ex As IOException
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub RadMenuItem4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem4.Click
        Export(1)
    End Sub

    Private Sub RadMenuItem5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem5.Click
        Export(4)
    End Sub



    Private Sub gv1_ViewCellFormatting(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CellFormattingEventArgs) Handles gv1.ViewCellFormatting
        'If arrFooter IsNot Nothing AndAlso arrFooter.Count > 0 Then
        '    If TypeOf e.CellElement Is Telerik.WinControls.UI.GridSummaryCellElement Then
        '        e.CellElement.TextAlignment = ContentAlignment.MiddleRight
        '    End If
        'End If
    End Sub



    Private Sub gv1_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles gv1.CellDoubleClick
        Try
            If lvl = 1 Then
                strShift = clsCommon.myCstr(gv1.CurrentRow.Cells("ShiftType").Value)
                strItemType = clsCommon.myCstr(gv1.CurrentRow.Cells("ItemType").Value)
                lvl += 1
                loadData()
            ElseIf lvl = 2 Then
                strItem = clsCommon.myCstr(gv1.CurrentRow.Cells("Item_Code").Value)
                lvl += 1
                loadData()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnopen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnopen.Click
        Try
            If lvl = 3 Then
                strItem = ""
                lvl -= 1
                loadData()
            ElseIf lvl = 2 Then
                strShift = ""
                strItemType = ""
                lvl -= 1
                loadData()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class
