''----08/06/2012--updation by shipra------on location's filter locations are being displayed from Segment Table

'--------------------------------Last modify By - Priti ------------------------------------
'--------------------------------Last modify date - 07/09/2012-------------------------------------
'--------------------------------Last modify Time - 11:00 AM -------------------------------------
'BM00000000724,BM00000000765
Imports common
Imports XpertERPEngine
Imports Telerik.WinControls.UI
Imports Telerik.WinControls.Data
Imports Telerik.Data
Imports Telerik.WinControls.Enumerations
Imports Telerik.WinControls
Public Class FrmExciseSummaryReport
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim ExportToExcel As Boolean = False

    Private Sub FrmExciseSummaryReport_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.P Then
            print(True)
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag Then
            'SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isPostFlag Then
            'DeleteData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()
        End If
    End Sub

    Private Sub FrmExciseSummaryReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        dtStart.Value = System.DateTime.Now()
        dtEnd.Value = System.DateTime.Now()
        LoadLocation()
        chkLocAll.IsChecked = True
        If Not clsCommon.CompairString(objCommonVar.CurrentUserCode, "ADMIN") = CompairStringResult.Equal Then
            If funSetUserAccess() = False Then Exit Sub
        End If
        btnExport.Visible = False
        'ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction")
        'ButtonToolTip.SetToolTip(btnPost, "Press Alt+P Post Trasnaction")
        'ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        'ButtonToolTip.SetToolTip(btnReset, "Press Alt+N for reset")
        ButtonToolTip.SetToolTip(btnPrint, "Press Alt+P for Print ")
    End Sub

    Private Function funSetUserAccess() As Boolean
        Try

            Dim strRights As String = ""
            Dim strTemp() As String
            Dim strProgCode =
            strRights = enuUserRights.enuRead & "," & enuUserRights.enuModify & "," & enuUserRights.enuDelete & "," & enuUserRights.enuAuthorised
            strRights = modUserMgt.funGetPermissions(strRights, strProgCode)
            strTemp = Split(strRights, ",")
            If strTemp(0) = "0" Then
                MsgBox("Permission Denied", MsgBoxStyle.Critical, Me.Text)
                funSetUserAccess = False
                blnRead = False
                Me.Close()
                Exit Function
            Else
                blnRead = True
            End If
            funSetUserAccess = True
        Catch er As Exception
            clsCommon.MyMessageBoxShow(er.Message)
            Return False
        End Try
        Return True
    End Function

    Private Sub btnReferesh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReferesh.Click
        print(False)
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        print(True)
    End Sub
    Sub print(ByVal IsPrint As Boolean)
        Dim strReportName As String
        Dim startDate As String = clsCommon.GetPrintDate(dtStart.Value, "dd-MMM-yyyy hh:mm tt")
        Dim EndDate As String = clsCommon.GetPrintDate(dtEnd.Value, "dd-MMM-yyyy hh:mm tt")
        Dim startTime As String = dtStart.Value.ToString("hh:mm tt")
        Dim EndTime As String = dtEnd.Value.ToString("hh:mm tt")

        If chkLocSelect.IsChecked Then
            If cbgLocation.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please Select Atleast One Location Code.")
                Return
            End If
        End If
        Dim LocFilter As String = ""
        If cbgLocation.CheckedValue.Count > 0 Then
            LocFilter = clsCommon.GetMulcallString(cbgLocation.CheckedValue)
            LocFilter = LocFilter.Replace("'", "")
        End If
        '---------------------------------------------Finished Goods------------------------------------
        Dim strQryFG As String = "select  '" + startDate + "' AS StartDate, '" + EndDate + "' AS [End Date], '" + startTime + "' AS [Start Time], '" + EndTime + "' AS [End Time], '' as LocFilter, Sale_Invoice_No, Sale_Invoice_Date, TSPL_ITEM_MASTER.Item_Code, ItemCode, TSPL_ITEM_MASTER.Item_Desc, Qty, Total_Assessable_Amt, TAX1_Rate, TAX1_Amt, TAX2_Rate, TAX2_Amt, TAX3_Rate, TAX3_Amt, (TAX1_Amt+TAX2_Amt+TAX3_Amt) as totalTax, Date_Time_Removal, MRP_Amt,  Location, TSPL_CHAPTER_HEAD.Description as Cheapter_Heads from (  "
        strQryFG += " SELECT Sale_Invoice_No, MAX(Sale_Invoice_Date) as Sale_Invoice_Date, Item_Code, Item_Code+' ( '+CONVERT(varchar,MRP_Amt)+' )' as ItemCode, MAX(Item_Desc) as Item_Desc, SUM(Qty) as Qty, SUM(Qty*Item_Assessable_Rate) as Total_Assessable_Amt, MAX(TAX1_Rate) as TAX1_Rate, SUM([TAX1_Amt]) as [TAX1_Amt], MAX(TAX2_Rate) as TAX2_Rate,  SUM(TAX2_Amt) as TAX2_Amt, MAX(TAX3_Rate) as TAX3_Rate, SUM(TAX3_Amt) as TAX3_Amt, MAX(Date_Time_Removal) as Date_Time_Removal, MRP_Amt, MAX(Location) as Location from ( "
        strQryFG += " SELECT TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No, TSPL_SALE_INVOICE_HEAD.Date_Time_Removal as  Sale_Invoice_Date, TSPL_SALE_INVOICE_DETAIL.Item_Code, TSPL_SALE_INVOICE_DETAIL.Item_Desc, "
        strQryFG += " CASE WHEN TSPL_SALE_INVOICE_DETAIL.Unit_code = 'FB' THEN  ROUND((TSPL_SALE_INVOICE_DETAIL.Invoice_Qty / TSPL_ITEM_UOM_DETAIL.Conversion_Factor),0) ELSE TSPL_SALE_INVOICE_DETAIL.Invoice_Qty END AS Qty,  "
        strQryFG += " CASE WHEN TSPL_SALE_INVOICE_DETAIL.Unit_code = 'FB' THEN  Item_Assessable_Rate*Conversion_Factor Else Item_Assessable_Rate END AS Item_Assessable_Rate, "
        strQryFG += " TSPL_SALE_INVOICE_DETAIL.TAX1_Rate,  TSPL_SALE_INVOICE_DETAIL.TAX1_Amt*TSPL_SALE_INVOICE_DETAIL.Invoice_Qty as [TAX1_Amt], "
        strQryFG += " TSPL_SALE_INVOICE_DETAIL.TAX2_Rate,   TSPL_SALE_INVOICE_DETAIL.TAX2_Amt*TSPL_SALE_INVOICE_DETAIL.Invoice_Qty as [TAX2_Amt], "
        strQryFG += " TSPL_SALE_INVOICE_DETAIL.TAX3_Rate,  TSPL_SALE_INVOICE_DETAIL.TAX3_Amt*TSPL_SALE_INVOICE_DETAIL.Invoice_Qty as [TAX3_Amt], "
        strQryFG += " TSPL_SALE_INVOICE_HEAD.Date_Time_Removal, "
        strQryFG += " CONVERT(Decimal(18,0),CASE WHEN TSPL_SALE_INVOICE_DETAIL.Unit_code='FC' Then (TSPL_SALE_INVOICE_DETAIL.MRP_Amt/TSPL_ITEM_UOM_DETAIL.Conversion_Factor) Else TSPL_SALE_INVOICE_DETAIL.MRP_Amt END) as MRP_Amt, "
        strQryFG += " TSPL_SALE_INVOICE_HEAD.Location"
        strQryFG += " FROM TSPL_SALE_INVOICE_HEAD "
        strQryFG += " Left Outer JOIN  TSPL_SALE_INVOICE_DETAIL ON TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No = TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No"
        strQryFG += " Left Outer JOIN  TSPL_LOCATION_MASTER ON TSPL_SALE_INVOICE_HEAD.Location = TSPL_LOCATION_MASTER.Location_Code "
        strQryFG += " Left Outer Join TSPL_ITEM_UOM_DETAIL ON TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_SALE_INVOICE_DETAIL.Item_Code where TSPL_SALE_INVOICE_HEAD.Is_Post ='Y' AND TSPL_LOCATION_MASTER.Excisable='T' AND TSPL_ITEM_UOM_DETAIL.UOM_Code='FB' "
        strQryFG += " ) XXX  GROUP BY Sale_Invoice_No, Item_Code, MRP_Amt    "
        strQryFG += " union ALL"
        strQryFG += " SELECT Transfer_No, MAX(EntryDateTime) as EntryDateTime, Item_Code, Item_Code+' ( '+CONVERT(varchar,MRP_Amt)+' )' as ItemCode, MAX(Item_Desc) as Item_Desc, SUM(Item_Qty) as Qty, SUM(Total_Assessable_Amt) as Total_Assessable_Amt, MAX(TAX1_Rate) as TAX1_Rate, SUM(TAX1_Amt) as TAX1_Amt, MAX(TAX2_Rate) as TAX2_Rate, SUM(TAX2_Amt) as TAX2_Amt, MAX(TAX3_Rate) as TAX3_Rate,  SUM(TAX3_Amt) as TAX3_Amt, MAX(Date_Time_Removal) as Date_Time_Removal, MAX(MRP_Amt) as MRP_Amt, MAX(From_Location) as location  FROM ( "
        strQryFG += " SELECT TSPL_TRANSFER_HEAD.Transfer_No ,TSPL_TRANSFER_HEAD.EntryDateTime , TSPL_TRANSFER_DETAIL.Item_Code ,TSPL_TRANSFER_DETAIL.Item_Desc ,"
        strQryFG += " TSPL_TRANSFER_DETAIL.Item_Qty , TSPL_TRANSFER_DETAIL.Item_Qty*TSPL_TRANSFER_DETAIL.Assessable_Amt as Total_Assessable_Amt, TSPL_TRANSFER_DETAIL.TAX1_Rate ,TSPL_TRANSFER_DETAIL.TAX1_Amt  ,"
        strQryFG += " TSPL_TRANSFER_DETAIL.TAX2_Rate,TSPL_TRANSFER_DETAIL.TAX2_Amt, "
        strQryFG += " TSPL_TRANSFER_DETAIL.TAX3_Rate,TSPL_TRANSFER_DETAIL.TAX3_Amt, "
        strQryFG += " '' AS Date_Time_Removal , COnvert(Decimal(18,0),MRP_In_Bottle) as MRP_Amt, TSPL_TRANSFER_HEAD.From_Location    "
        strQryFG += " from TSPL_TRANSFER_HEAD "
        strQryFG += " Left Outer Join TSPL_TRANSFER_DETAIL on TSPL_TRANSFER_HEAD.Transfer_No=TSPL_TRANSFER_DETAIL.Transfer_No  where TSPL_TRANSFER_HEAD.Post='Y' and TSPL_TRANSFER_HEAD.Item_Type ='Full'"
        strQryFG += " ) YYY GROUP BY Transfer_No, Item_Code, MRP_Amt"
        strQryFG += " ) as final Left OUTER JOIN TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code =final.Item_Code LEFT OUTER JOIN TSPL_CHAPTER_HEAD on TSPL_ITEM_MASTER.Cheapter_Heads=TSPL_CHAPTER_HEAD.Chapter_Head_Code where (Sale_Invoice_Date >= '" + startDate + "') AND (Sale_Invoice_Date <= '" + EndDate + "') "
        If chkLocSelect.IsChecked Then
            strQryFG += " and final.Location in   (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")  "
        Else
            strQryFG += " and final.Location in   (" + clsCommon.GetMulcallString(cbgLocation.AllValue) + ")  "
        End If
        '------------------------------------------------------------------------------------------------

        '--------------------------------------------Others Goods----------------------------------------
        Dim strQryOthers As String = " select  DISTINCT '" + startDate + "' AS StartDate, '" + EndDate + "' AS [End Date], '" + startTime + "' AS [Start Time], '" + EndTime + "' AS [End Time], '" + LocFilter + "' as LocFilter, *, '' as Cheapter_Heads from (" & _
                      " SELECT DISTINCT   H.invoice_No as Sale_Invoice_No,H.posting_Date as Sale_Invoice_Date , D.Item_Code, Item_Code+' ( '+CONVERT(varchar,price)+' )' as ItemCode, D.Item_Desc, D.invoice_Qty as [Qty] , D.TotalAmt as Total_Assessable_Amt, D.TAX1_Rate, D.TAX1_Amt, D.TAX2_Rate, D.TAX2_Amt, D.TAX3_Rate, D.TAX3_Amt, (D.TAX1_Amt+D.TAX2_Amt+D.TAX3_Amt) as totalTax, '' AS Date_Time_Removal, price AS MRP_Amt,H.Loc_Code as Location FROM TSPL_SCRAPINVOICE_HEAD AS H INNER JOIN  TSPL_SCRAPINVOICE_DETAIL AS D ON H.invoice_No = D.invoice_No where H.ispost  =1 and 'E'=( select Type  from TSPL_TAX_MASTER where Tax_Code =h.TAX1  )" & _
                      " ) as final where (Sale_Invoice_Date >= '" + startDate + "') AND (Sale_Invoice_Date <= '" + EndDate + "')  "
        If chkLocSelect.IsChecked Then
            strQryOthers += " and final.Location in   (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")  "
            'Else
            '    strQryOthers += " and final.Location in   (" + clsCommon.GetMulcallString(cbgLocation.AllValue) + ")  "
        End If
        '-------------------------------------------------------------------------------------------------
        If rbtnSummary.IsChecked And chkAllItems.Checked Then
            strQryFG = strQryFG + " UNION ALL " + strQryOthers
        End If
        If rdbothers.IsChecked Then
            strQryFG = strQryOthers
        End If

        If rbtnSummary.IsChecked = True Then
            strReportName = "crptExciseSummary"
        Else
            strReportName = "crptExciseDetails"
        End If


        Try
            Dim dt As DataTable
            If rbtnSummary.IsChecked = True Then
                If chkCHapterWise.Checked Then
                    strQryFG = "SELECT '" + startDate + "' AS StartDate, '" + EndDate + "' AS EndDate, '" + startTime + "' AS [Start Time], '" + EndTime + "' AS [End Time], '" + clsCommon.GETSERVERDATE() + "' AS RunDate, Cheapter_Heads, SUM(Qty) as Qty, SUM(Total_Assessable_Amt) as Total_Assessable_Amt, SUM(TAX1_Amt) as BED, SUM(TAX2_Amt) as ECess, SUM(TAX3_Amt) as HCess, (SUM(TAX2_Amt)+SUM(TAX3_Amt)) [TotalCess], (SUM(TAX1_Amt)+SUM(TAX2_Amt)+SUM(TAX3_Amt)) as TotalTax from ( " + strQryFG + " ) SUMMARY Group By Cheapter_Heads"
                Else
                    strQryFG = "SELECT '" + startDate + "' AS StartDate, '" + EndDate + "' AS EndDate, '" + startTime + "' AS [Start Time], '" + EndTime + "' AS [End Time], '" + clsCommon.GETSERVERDATE() + "' AS RunDate, Sale_Invoice_No, MAX(Sale_Invoice_Date) AS Sale_Invoice_Date, SUM(Qty) as Qty, SUM(Total_Assessable_Amt) as Total_Assessable_Amt, SUM(TAX1_Amt) as BED, SUM(TAX2_Amt) as ECess, SUM(TAX3_Amt) as HCess, (SUM(TAX2_Amt)+SUM(TAX3_Amt)) [TotalCess], (SUM(TAX1_Amt)+SUM(TAX2_Amt)+SUM(TAX3_Amt)) as TotalTax from ( " + strQryFG + " ) SUMMARY Group By Sale_Invoice_No"
                End If
            End If

            If rbtnDetail.IsChecked Then
                Dim dtItem As DataTable
                Dim ItemQry As String
                If chkImrpWise.Checked Then
                    If rdbothers.IsChecked Then
                        dtItem = clsDBFuncationality.GetDataTable("Select Distinct '['+Item_Code+' ( '+CONVERT(Varchar,CONVERT(Decimal(18,2),price))+' )]' as ItemCode from TSPL_SCRAPINVOICE_DETAIL WHERE ISNULL(Item_Code,'')<>''")
                    Else
                        dtItem = clsDBFuncationality.GetDataTable("Select Distinct '['+Item_Code+' ( '+CONVERT(Varchar,CONVERT(Decimal(18,0),mrp))+' )]' as ItemCode, Item_Code from (Select Distinct Item_Code, ISNULL(case When XXX.UOM='FC' THEN (mrp/(Select Conversion_Factor from TSPL_ITEM_UOM_DETAIL WHERE TSPL_ITEM_UOM_DETAIL.Item_Code=XXX.Item_Code AND TSPL_ITEM_UOM_DETAIL.UOM_Code='FB')) else mrp End,0) as mrp from (Select Item_Code, MRP_Amt as mrp, Unit_code as Uom  from TSPL_SALE_INVOICE_DETAIL UNION Select Item_Code, MRP, Uom   from TSPL_TRANSFER_DETAIL ) XXX ) YYY Order by Item_Code")
                    End If

                    Dim ItemString1 As String = ""
                    Dim ItemString2 As String = ""
                    Dim strTotal As String = ""
                    Dim Count As Integer = 0
                    For Each dr As DataRow In dtItem.Rows
                        Dim ItemCode As String = clsCommon.myCstr(dr("ItemCode"))
                        If Count > 0 Then
                            ItemString1 += "," + ItemCode
                            ItemString2 += "," + "ISNULL(" + ItemCode + ",0) AS " + ItemCode
                            strTotal += "+" + "ISNULL(" + ItemCode + ",0)"
                        Else
                            ItemString1 += ItemCode
                            ItemString2 += "ISNULL(" + ItemCode + ",0) AS " + ItemCode
                            strTotal += "ISNULL(" + ItemCode + ",0)"
                        End If
                        Count += 1
                    Next
                    ItemQry = "Select Sale_Invoice_No, Sale_Invoice_Date," + ItemString2 + ", (" + strTotal + ") as Total From (SELECT Sale_Invoice_No, Sale_Invoice_Date, ItemCode, totalTax from (" + strQryFG + " ) YYY ) ZZZ Pivot (SUM(totalTax) FOR ItemCode IN (" + ItemString1 + ")) AS pvt"
                    dtItem = clsDBFuncationality.GetDataTable(ItemQry)
                    gv1.DataSource = Nothing
                    gv1.DataSource = dtItem
                    Exit Sub
                ElseIf chkIQtywise.Checked Then

                End If
            End If
            dt = clsDBFuncationality.GetDataTable(strQryFG)
            gv1.DataSource = Nothing
            gv1.DataSource = dt

            FormatGrid()
            If IsPrint Then
                If rbtnSummary.IsChecked = True Then
                    Dim frmcrystal As New frmCrystalReportViewer()
                    frmcrystal.funreport(CrystalReportFolder.SalesReport, dt, strReportName, "Excise Report")
                Else
                    Dim frmcrystal As New frmCrystalReportViewer()
                    frmcrystal.funreport(CrystalReportFolder.SalesReport, dt, strReportName, "Excise Report")
                End If
            End If
            RadPageView1.SelectedPage = RadPageViewPage2
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub FormatGrid()
        gv1.MasterTemplate.SummaryRowsBottom.Clear()
        For ii As Integer = 0 To gv1.Columns.Count - 1
            gv1.Columns(ii).ReadOnly = True
            gv1.Columns(ii).IsVisible = False
        Next


        If rbtnSummary.IsChecked Then
            If chkCHapterWise.Checked Then
                gv1.Columns("Cheapter_Heads").IsVisible = True
                gv1.Columns("Cheapter_Heads").HeaderText = "CHAPTER HEAD"
                gv1.Columns("Cheapter_Heads").Width = 100
            Else
                gv1.Columns("Sale_Invoice_No").IsVisible = True
                gv1.Columns("Sale_Invoice_No").HeaderText = "INVOICE NO"
                gv1.Columns("Sale_Invoice_No").Width = 100
            End If
            gv1.Columns("Qty").IsVisible = True
            gv1.Columns("Qty").HeaderText = "QUANTITY"
            gv1.Columns("Qty").Width = 80

            gv1.Columns("BED").IsVisible = True
            gv1.Columns("BED").HeaderText = "B.E.D."
            gv1.Columns("BED").Width = 110

            gv1.Columns("ECess").IsVisible = True
            gv1.Columns("ECess").HeaderText = "EDN CESSS"
            gv1.Columns("ECess").Width = 110

            gv1.Columns("HCess").IsVisible = True
            gv1.Columns("HCess").HeaderText = "SH.ED CESS"
            gv1.Columns("HCess").Width = 110

            gv1.Columns("TotalCess").IsVisible = True
            gv1.Columns("TotalCess").HeaderText = "TOTAL CESS"
            gv1.Columns("TotalCess").Width = 150

            gv1.Columns("TotalTax").IsVisible = True
            gv1.Columns("TotalTax").HeaderText = "G.TOTAL"
            gv1.Columns("TotalTax").Width = 170

            Dim summaryRowItem As New GridViewSummaryRowItem()
            Dim SumQty As New GridViewSummaryItem("Qty", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(SumQty)
            Dim SumBed As New GridViewSummaryItem("BED", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(SumBed)
            Dim SumEcess As New GridViewSummaryItem("ECess", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(SumEcess)
            Dim SumHCess As New GridViewSummaryItem("HCess", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(SumHCess)
            Dim SumTotalCess As New GridViewSummaryItem("TotalCess", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(SumTotalCess)
            Dim SumTotalTax As New GridViewSummaryItem("TotalTax", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(SumTotalTax)
            gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        Else
            gv1.Columns("Sale_Invoice_No").IsVisible = True
            gv1.Columns("Sale_Invoice_No").HeaderText = "Invoice No"
            gv1.Columns("Sale_Invoice_No").Width = 100

            gv1.Columns("Sale_Invoice_Date").IsVisible = True
            gv1.Columns("Sale_Invoice_Date").HeaderText = "Invoice Date"
            gv1.Columns("Sale_Invoice_Date").Width = 150

            gv1.Columns("Item_Desc").IsVisible = True
            gv1.Columns("Item_Desc").HeaderText = "Item"
            gv1.Columns("Item_Desc").Width = 200

            gv1.Columns("MRP_Amt").IsVisible = True
            gv1.Columns("MRP_Amt").HeaderText = "MRP"
            gv1.Columns("MRP_Amt").Width = 80

            gv1.Columns("Qty").IsVisible = True
            gv1.Columns("Qty").HeaderText = "QUANTITY"
            gv1.Columns("Qty").Width = 80

            gv1.Columns("TAX1_Amt").IsVisible = True
            gv1.Columns("TAX1_Amt").HeaderText = "B.E.D."
            gv1.Columns("TAX1_Amt").Width = 100

            gv1.Columns("TAX2_Amt").IsVisible = True
            gv1.Columns("TAX2_Amt").HeaderText = "EDN CESSS"
            gv1.Columns("TAX2_Amt").Width = 100

            gv1.Columns("TAX3_Amt").IsVisible = True
            gv1.Columns("TAX3_Amt").HeaderText = "SH.ED CESS"
            gv1.Columns("TAX3_Amt").Width = 100

            Dim summaryRowItem As New GridViewSummaryRowItem()
            Dim SumQty As New GridViewSummaryItem("Qty", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(SumQty)
            Dim SumAmount As New GridViewSummaryItem("Total_Assessable_Amt", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(SumAmount)
            Dim SumBed As New GridViewSummaryItem("TAX1_Amt", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(SumBed)
            Dim SumEcess As New GridViewSummaryItem("TAX2_Amt", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(SumEcess)
            Dim SumHCess As New GridViewSummaryItem("TAX3_Amt", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(SumHCess)
            gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        End If
    End Sub

    Sub LoadLocation()
        Dim qry As String = " select Location_Code,Location_Desc from TSPL_LOCATION_MASTER where Location_Type='Physical' and Excisable='T' "
        'Dim qry As String = " select Segment_code as Code, Description  from TSPL_GL_SEGMENT_CODE where Seg_No='7' "
        cbgLocation.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgLocation.ValueMember = "Location_Code"
        cbgLocation.DisplayMember = "Location_Desc"
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub chkLocAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocAll.ToggleStateChanged
        cbgLocation.Enabled = Not chkLocAll.IsChecked
    End Sub


    Private Sub btnExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExport.Click
        Try
            ExportToExcel = True
            print(False)
            ExportToExcel = False
            If gv1.DataSource Is Nothing OrElse gv1.Rows.Count <= 0 Then
                Throw New Exception("No Data found to Export")
            End If
            ExportToExcelGV(EnumExportTo.Excel)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub ExportToExcelGV(ByVal exporter As EnumExportTo)
        Try
            Dim arrHeader As List(Of String) = New List(Of String)()
            Dim CompName As String = clsDBFuncationality.getSingleValue("Select Comp_Name from TSPL_COMPANY_MASTER Where Comp_Code='" + objCommonVar.CurrentCompanyCode + "'")
            arrHeader.Add(CompName)
            If chkCHapterWise.Checked Then
                arrHeader.Add("EXCISE DUTY CALCULATION  - CHAPTERWISE " + clsCommon.GETSERVERDATE() + " ")
            Else
                arrHeader.Add("EXCISE DUTY CALCULATION  " + clsCommon.GETSERVERDATE() + " ")
            End If
            arrHeader.Add("From Date : " + clsCommon.GetPrintDate(dtStart.Value, "dd/MMM/yyyy") + " To " + clsCommon.GetPrintDate(dtEnd.Value, "dd/MMM/yyyy") + " ")
            arrHeader.Add("From Time : " + clsCommon.GetPrintDate(dtStart.Value, "hh:mm tt") + " To " + clsCommon.GetPrintDate(dtEnd.Value, "hh:mm tt") + " ")


            clsCommon.MyExportToExcel("Excise Summary", gv1, arrHeader, Me.Text)

        Catch ex As Exception
            clsCommon.ProgressBarHide()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            clsCommon.ProgressBarHide()
        End Try
    End Sub

    Private Sub rbtnDetail_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtnDetail.ToggleStateChanged
        ShowButtons()
        If rbtnDetail.IsChecked Then
            chkIQtywise.Checked = False
            chkAllItems.Enabled = False
            chkCHapterWise.Enabled = False
            chkIQtywise.Enabled = True
            chkImrpWise.Enabled = True
        End If
    End Sub

    Private Sub rbtnSummary_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtnSummary.ToggleStateChanged
        ShowButtons()
        If rbtnSummary.IsChecked Then
            chkImrpWise.Enabled = False
            chkIQtywise.Enabled = False
            chkAllItems.Checked = True
            chkCHapterWise.Checked = False
            chkAllItems.Enabled = True
            chkCHapterWise.Enabled = True
        End If
    End Sub

    Private Sub chkImrpWise_ToggleStateChanged_1(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkImrpWise.ToggleStateChanged
        ShowButtons()
        If chkImrpWise.Checked Then
            chkIQtywise.Checked = False
            btnPrint.Visible = False
        End If
    End Sub

    Private Sub chkIQtywise_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkIQtywise.ToggleStateChanged
        ShowButtons()
        If chkIQtywise.Checked Then
            chkImrpWise.Checked = False
            btnPrint.Visible = False
        End If
    End Sub

    Private Sub chkDocWise_ToggleStateChanged_1(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkAllItems.ToggleStateChanged, chkAllItems.ToggleStateChanged
        If chkAllItems.Checked Then
            chkCHapterWise.Checked = False
        End If
    End Sub

    Private Sub chkCHapterWise_ToggleStateChanged_1(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkCHapterWise.ToggleStateChanged
        ShowButtons()
        If chkCHapterWise.Checked Then
            chkAllItems.Checked = False
            btnPrint.Visible = False
        End If
    End Sub

    Private Sub ShowButtons()
        btnPrint.Visible = True
        btnExport.Visible = True
    End Sub
End Class
