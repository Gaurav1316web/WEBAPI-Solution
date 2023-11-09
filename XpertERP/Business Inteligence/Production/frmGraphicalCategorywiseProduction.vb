' Created BY Panch Raj as on 10 oct 2013 @ Ticket No-[BM00000000530]-
Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports common
Public Class frmGraphicalCategorywiseProduction
    Inherits FrmMainTranScreen
    Dim qry As String
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim dtMst As DataTable
    Dim dtPP As DataTable
    Dim dtRM As DataTable
    Dim coveredAngle As Double = 0
    Dim isInsideLoadData As Boolean = False


    Private Sub frmGraphicalCategorywiseProduction_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt And e.KeyCode = Keys.P Then
            'PrintData1()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.R Then
            Reset()
        End If
    End Sub

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmGraphicalCategorywiseProduction)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
    End Sub

    Private Sub frmGraphicalCategorywiseProduction_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        'chkItemAll.IsChecked = True
        'chkLocationAll.IsChecked = True
        isInsideLoadData = True
        LoadFigures()
        LoadChartType()
        LoadSkinType()
        LoadOrientation()
       
        cboType.SelectedValue = Charting.ChartSeriesType.Bar
        cboSkin.SelectedValue = "Gradient"
        cboOrientation.SelectedValue = "Vertical"

        dtpFromdate1.Value = clsCommon.GETSERVERDATE().AddMonths(-1)
        dtpToDate.Value = clsCommon.GETSERVERDATE()
        'ItemLoad()
        'LoadLocation()

        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        chkAutoScroll.Checked = True
        'ButtonToolTip.SetToolTip(btnPrint, "Press Ctrl+P Print the Report")
        'ButtonToolTip.SetToolTip(btnreset, "Press Alt+R Reset the Window")
        PrintData()
        isInsideLoadData = False
    End Sub
    Sub LoadSkinType()
        cboSkin.DataSource = FrmBIMonthWiseSale.GetChartSkin()
        cboSkin.ValueMember = "Code"
        cboSkin.DisplayMember = "Code"
    End Sub

    Sub LoadChartType()
        cboType.DataSource = FrmBIMonthWiseSale.GetChartType()
        cboType.ValueMember = "Code"
        cboType.DisplayMember = "Name"
    End Sub
    Sub LoadOrientation()
        cboOrientation.DataSource = FrmBIMonthWiseSale.GetChartOrientation()
        cboOrientation.ValueMember = "Code"
        cboOrientation.DisplayMember = "Code"
    End Sub

    Private Sub btnExcelChart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcelChart.Click
        Try
            dtMst = clsProductionReceipt.GetCategorywiseProduction(dtpFromdate1.Value, dtpToDate.Value, cmbFigure.SelectedValue).Copy()
            SnDUtility.GenerateExcelChart(dtMst, cboType.SelectedValue, "", "Category", "Produced Qty", "Rejected Qty", "Break Qty")
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub PrintData()
        Try
            isInsideLoadData = True
            dtMst = clsProductionReceipt.GetCategorywiseProduction(dtpFromdate1.Value, dtpToDate.Value, cmbFigure.SelectedValue).Copy()

            RadChart1.ChartTitle.TextBlock.Text = ""

            '' NEW CODE
            If cboType.SelectedValue = Charting.ChartSeriesType.Pie Then
                'RadChart1.DataManager.LabelsColumn = "DateMonth"
            Else
                'RadChart1.DataManager.LabelsColumn = "Amount"
                If clsCommon.CompairString(clsCommon.myCstr(cboOrientation.SelectedValue), "Horizontal") = CompairStringResult.Equal Then
                    RadChart1.SeriesOrientation = Telerik.Charting.ChartSeriesOrientation.Horizontal
                Else
                    RadChart1.SeriesOrientation = Telerik.Charting.ChartSeriesOrientation.Vertical
                End If
            End If
            RadChart1.Height = Me.SplitContainer1.Panel2.Height - 5
            RadChart1.Width = Me.SplitContainer1.Panel2.Width - 5
            Me.RadChart1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)

            If chkAutoScroll.Checked Then
                If cboType.SelectedValue = Charting.ChartSeriesType.Bar Then
                    If clsCommon.CompairString(clsCommon.myCstr(cboOrientation.SelectedValue), "Horizontal") = CompairStringResult.Equal Then
                        RadChart1.Width = Me.SplitContainer1.Panel2.Width - 5
                        RadChart1.Height = (150 * dtMst.Rows.Count) '' IIf(dt.Rows.Count <= 5, 250, 0) + (40 * dt.Rows.Count)
                        Me.RadChart1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top) Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)

                    Else
                        RadChart1.Width = (150 * dtMst.Rows.Count) '' IIf(dt.Rows.Count <= 5, 250, 0) + (40 * dt.Rows.Count)
                        RadChart1.Height = Me.SplitContainer1.Panel2.Height - 5
                        Me.RadChart1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) Or System.Windows.Forms.AnchorStyles.Left)), System.Windows.Forms.AnchorStyles)
                    End If
                End If
            End If




            RadChart1.AutoTextWrap = True

            RadChart1.IntelligentLabelsEnabled = True
            RadChart1.Series(0).Appearance.LegendDisplayMode = Telerik.Charting.ChartSeriesLegendDisplayMode.ItemLabels
            RadChart1.Series.Clear()
            RadChart1.DataSource = Nothing
            RadChart1.PlotArea.XAxis.DataLabelsColumn = "Category"
            RadChart1.DataSource = dtMst '' clsDBFuncationality.GetDataTable(qry)
            RadChart1.Skin = clsCommon.myCstr(cboSkin.SelectedValue)
            RadChart1.DefaultType = cboType.SelectedValue
            
            RadChart1.DataBind()
            RadChart1.Update()

            RadChart1.Series(0).DataLabelsColumn = "Produced Qty"
            RadChart1.Series(1).DataLabelsColumn = "Rejected Qty"
            RadChart1.Series(2).DataLabelsColumn = "Break Qty"
            isInsideLoadData = False
            '' END OF NEW CODE 

            gv.DataSource = Nothing
            gv.DataSource = dtMst
            gv.MasterTemplate.BestFitColumns()
            isInsideLoadData = False
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        PrintData()
    End Sub

    Sub PrintData1(ByVal exporter As EnumExportTo)
        If gv.Rows.Count > 0 Then
            Dim fromdate As String = clsCommon.myCDate(dtpFromdate1.Value, "dd/MM/yyyy")
            Dim Todate As String = clsCommon.myCDate(dtpToDate.Value, "dd/MM/yyyy")
            Dim str As String = "Categorywise Production Report"

            Dim arr As New List(Of String)()
            arr.Add("Categorywise Production Report")
            arr.Add("  From Date:  " + fromdate + "  To Date: " + Todate + "   ")

            If exporter = EnumExportTo.Excel Then
                clsCommon.MyExportToExcel(str, gv, arr, "Categorywise Production Report")
            Else
                clsCommon.MyExportToPDF(str, gv, arr, "Categorywise Production Report", False)
            End If
        Else
            common.clsCommon.MyMessageBoxShow(Me, "No Record Found to print.", Me.Text)
        End If
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnreset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Reset()
    End Sub
    Sub Reset()
        'chkItemAll.IsChecked = True
        'chkLocationAll.IsChecked = True
        dtpFromdate1.Value = clsCommon.GETSERVERDATE().AddDays(-30)
        dtpToDate.Value = clsCommon.GETSERVERDATE()
        gv.DataSource = Nothing
        gv.Rows.Clear()
        gv.Columns.Clear()
        'ItemLoad()
        'LoadLocation()
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

    Private Sub btnExcel_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs)
        PrintData1(EnumExportTo.Excel)
    End Sub

    Private Sub btnPDF_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs)
        PrintData1(EnumExportTo.PDF)
    End Sub

    Private Sub RadChart1_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles RadChart1.MouseDoubleClick
        'MsgBox(e.X & ";" & e.Y)
        Dim dtSrc As DataTable
        dtSrc = Me.RadChart1.DataSource
        Dim totalItems As Integer
        totalItems = dtSrc.Rows.Count
        Dim plotAreaX As Integer = 0
        'totalItems = RadChart1.Chart.PlotArea.XAxis.Items.Count

        totalItems = dtSrc.Rows.Count

        Dim perItemSize As Integer = 0
        perItemSize = (RadChart1.Size.Width - 106 - 50) / totalItems
        Dim found As Boolean = False
        Dim itemIndex As Integer
        If RadChart1.DefaultType = Telerik.Charting.ChartSeriesType.Bar Then
            itemIndex = Math.Ceiling(((e.X - 50) / perItemSize)) - 1
        ElseIf RadChart1.DefaultType = Telerik.Charting.ChartSeriesType.Pie Then
            Dim fstPoint As PointF
            Dim polyPoint() As PointF = Nothing


            coveredAngle = 0
            For intloop As Integer = 0 To dtSrc.Rows.Count - 1
                If intloop = 0 Then
                    fstPoint = New Point(585, 280)
                Else
                    fstPoint = polyPoint(10)
                End If
                polyPoint = PolygonPoints(fstPoint, dtSrc, New Point(445, 280), 140, intloop)

                'Exit Sub
                If PointInPolygon(polyPoint, e.X, e.Y) = True Then

                    found = True
                    itemIndex = intloop
                    Exit For
                End If
            Next
            'PictureBox1.BringToFront()

            'PictureBox1.Image = Nothing
            'DrawpolyGon(polyPoint, "")
            If found = False Then

                Exit Sub
            End If

        End If

        If itemIndex < 1 Then
            itemIndex = 0
        End If

        Dim dtdtl As DataTable
        'dtdtl = clsBatchOrder.GetBatchOrderStatusDetail(dtpFromdate1.Value, dtpToDate.Value, dtSrc.Rows(itemIndex).Item("Batch Order Status")).Copy()
        dtdtl = clsProductionReceipt.GetCategorywiseProductionDetail(dtpFromdate1.Value, dtpToDate.Value, dtSrc.Rows(itemIndex).Item("Category")).Copy()
        gv.DataSource = Nothing
        gv.DataSource = dtdtl
        gv.MasterTemplate.BestFitColumns()
        RadPageView1.SelectedPage = RadPageViewPage2

    End Sub
    Function PolygonPoints(ByVal fstPoint As PointF, ByVal dtsrc As DataTable, ByVal originPoint As Point, ByVal radCircle As Integer, ByVal dtItemIndex As Integer) As System.Drawing.PointF()
        Dim totalCount As Integer = 0
        For intloop As Integer = 0 To dtsrc.Rows.Count - 1
            totalCount = totalCount + dtsrc.Rows(intloop).Item(1)
        Next
        Dim angle As Double
        Dim polPoint(10) As PointF
        'If currY < originPoint.Y Then
        '    angle = -(360 * (dtsrc.Rows(dtItemIndex).Item(1) / totalCount) * 3.14) / 180
        'Else
        '    angle = (360 * (dtsrc.Rows(dtItemIndex).Item(1) / totalCount) * 3.14) / 180
        'End If
        angle = (360 * (dtsrc.Rows(dtItemIndex).Item(1) / totalCount) * 3.14) / 180
        polPoint(0) = originPoint
        polPoint(1) = fstPoint
        Dim tempAngle As Double = 0
        tempAngle = coveredAngle
        For intloop As Integer = 2 To 10

            tempAngle = tempAngle + angle / 10
            '(10 - (intloop - 1))
            'polPoint(intloop) = New Point(originPoint.X + radCircle * Math.Cos(angle / (10 - (intloop - 1))), IIf(fstPoint.Y >= originPoint.Y, (originPoint.Y + radCircle * Math.Sin(angle / (10 - (intloop - 1)))), (originPoint.Y - radCircle * Math.Sin(angle / (10 - (intloop - 1))))))
            polPoint(intloop) = New Point(originPoint.X + radCircle * Math.Cos(tempAngle), (originPoint.Y + radCircle * Math.Sin(tempAngle)))

        Next
        coveredAngle = coveredAngle + angle
        Return polPoint
    End Function
    Public Function PointInPolygon(ByVal points() As PointF, ByVal X As Single, ByVal Y As Single) As Boolean
        ' Get the angle between the point and the
        ' first and last vertices.
        Dim max_point As Integer = points.Length - 1
        Dim total_angle As Single = GetAngle( _
            points(max_point).X, points(max_point).Y, _
            X, Y, _
            points(0).X, points(0).Y)

        ' Add the angles from the point
        ' to each other pair of vertices.
        For i As Integer = 0 To max_point - 1
            total_angle += GetAngle( _
                points(i).X, points(i).Y, _
                X, Y, _
                points(i + 1).X, points(i + 1).Y)
        Next i

        ' The total angle should be 2 * PI or -2 * PI if
        ' the point is in the polygon and close to zero
        ' if the point is outside the polygon.

        Return Math.Abs(total_angle) > 0.000001
    End Function
    Sub DrawpolyGon(ByVal PolPoints() As PointF, ByVal str As String)


        If Not PolPoints Is Nothing Then

            'objGraphics.DrawPolygon(Pens.Red, PolPoints)
            'objGraphics.FillPolygon(Brushes.Pink, PolPoints)

            Dim fm As New FontFamily("verdana")
            Dim font As System.Drawing.Font = New Font(fm, 10)
            Dim fs As New StringFormat
            fs.LineAlignment = StringAlignment.Center
            fs.Alignment = StringAlignment.Center
            'objGraphics.DrawString(str, font, Brushes.Black, PolPoints(0).X, PolPoints(0).Y)
            'PictureBox1.Visible = True
        End If

    End Sub
    Public Function GetAngle(ByVal Ax As Single, ByVal Ay As Single, ByVal Bx As Single, ByVal By As Single, ByVal Cx As Single, ByVal Cy As Single) As Single
        Dim dot_product As Single
        Dim cross_product As Single

        ' Get the dot product and cross product.
        dot_product = DotProduct(Ax, Ay, Bx, By, Cx, Cy)
        cross_product = CrossProductLength(Ax, Ay, Bx, By, Cx, Cy)

        ' Calculate the angle.
        Return Math.Atan2(cross_product, dot_product)
    End Function
    Private Function DotProduct( _
       ByVal Ax As Single, ByVal Ay As Single, _
       ByVal Bx As Single, ByVal By As Single, _
       ByVal Cx As Single, ByVal Cy As Single _
     ) As Single
        ' Get the vectors' coordinates.
        Dim BAx As Single = Ax - Bx
        Dim BAy As Single = Ay - By
        Dim BCx As Single = Cx - Bx
        Dim BCy As Single = Cy - By

        ' Calculate the dot product.
        Return BAx * BCx + BAy * BCy
    End Function
    Public Function CrossProductLength( _
       ByVal Ax As Single, ByVal Ay As Single, _
       ByVal Bx As Single, ByVal By As Single, _
       ByVal Cx As Single, ByVal Cy As Single _
     ) As Single
        ' Get the vectors' coordinates.
        Dim BAx As Single = Ax - Bx

        Dim BAy As Single = Ay - By

        Dim BCx As Single = Cx - Bx
        Dim BCy As Single = Cy - By

        ' Calculate the Z coordinate of the cross product.
        Return BAx * BCy - BAy * BCx
    End Function

    Private Sub RadButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadButton2.Click
        PrintData()
    End Sub

    Private Sub cboType_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.Data.PositionChangedEventArgs) Handles cboType.SelectedIndexChanged
        Try
            If Not isInsideLoadData Then
                PrintData()
            End If


        Catch ex As Exception

        End Try

    End Sub

    Private Sub cboSkin_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.Data.PositionChangedEventArgs) Handles cboSkin.SelectedIndexChanged
        Try
            If Not isInsideLoadData Then
                RadChart1.Skin = clsCommon.myCstr(cboSkin.SelectedValue)
                RadChart1.Update()
            End If
        Catch ex As Exception

        End Try

    End Sub
    Private Sub chkAutoScroll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkAutoScroll.ToggleStateChanged
        PrintData()
    End Sub

    Private Sub cmbFigure_SelectedValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbFigure.SelectedValueChanged
        If Not isInsideLoadData Then
            PrintData()
        End If
    End Sub

    Private Sub LoadFigures()
        cmbFigure.DataSource = FrmBankBookClosing.LoadFigures()
        cmbFigure.DisplayMember = "Code"
        cmbFigure.ValueMember = "Value"
    End Sub
End Class
