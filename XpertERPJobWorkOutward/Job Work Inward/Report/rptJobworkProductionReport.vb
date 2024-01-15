'Sanjay Ticket No-  BHA/13/11/18-000677 Date  13/Nov/2018
Imports common
Imports System.IO
Imports XpertERPEngine
Imports Telerik.WinControls.UI

Public Class rptJobworkProductionReport
    Inherits FrmMainTranScreen
    Dim strQry As String = ""
    Dim IsFormLoad As Boolean = False
    Dim SummaryData As Boolean = False
    Dim dt As DataTable
    Private Sub rptJobworkProductionReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        IsFormLoad = False
        txtfDate.Value = clsCommon.GETSERVERDATE()
        txtToDate.Value = clsCommon.GETSERVERDATE()
        Reset()
        IsFormLoad = True

    End Sub
    Sub Reset()
        txtfDate.Value = clsCommon.GETSERVERDATE()
        txtToDate.Value = clsCommon.GETSERVERDATE()
        RadPageView1.SelectedPage = RadPageViewPage1
        Gv1.DataSource = Nothing
        btnGo.Enabled = True
        btnBack.Enabled = False
        'btnReset.Enabled = False
        RadSplitButton1.Enabled = False
        txtCustomer.arrValueMember = Nothing
        txtMultLocation.arrValueMember = Nothing
        'LoadDisplayMethod()
        LoadTypes()
        SummaryData = False
    End Sub
    'Sub LoadDisplayMethod()
    '    Dim dt As DataTable = New DataTable()
    '    dt.Columns.Add("Code", GetType(String))
    '    dt.Columns.Add("Name", GetType(String))

    '    Dim dr As DataRow = dt.NewRow()
    '    dr("Code") = "Summary"
    '    dr("Name") = "Summary"
    '    dt.Rows.Add(dr)

    '    dr = dt.NewRow()
    '    dr("Code") = "Detail"
    '    dr("Name") = "Detail"
    '    dt.Rows.Add(dr)

    '    ddlDisplayType.DataSource = dt
    '    ddlDisplayType.ValueMember = "Code"
    '    ddlDisplayType.DisplayMember = "Name"
    '    ddlDisplayType.SelectedValue = "Summary"
    'End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Reset()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        PageSetupReport_ID = clsERPFuncationality.GetReportID(MyBase.Form_ID, ddlReportType.Text)
        TemplateGridview = Gv1
        loaddata()
    End Sub
    Sub FormatGrid()
        Try

            Dim summaryItem As New GridViewSummaryItem()
            'Gv1.TableElement.TableHeaderHeight = 150
            Gv1.MasterTemplate.ShowRowHeaderColumn = True
            For ii As Integer = 0 To Gv1.Columns.Count - 1
                Gv1.Columns(ii).ReadOnly = True
                Gv1.Columns(ii).IsVisible = True
                Gv1.Columns(ii).BestFit()
            Next

            Dim summaryRowItem As New GridViewSummaryRowItem()
            Dim itemQty As New GridViewSummaryItem("Qty", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(itemQty)
            Dim itemFATKG As New GridViewSummaryItem("FAT KG", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(itemFATKG)
            Dim itemSNFKG As New GridViewSummaryItem("SNF KG", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(itemSNFKG)
            '[TS KG]
            Dim itemTSKG As New GridViewSummaryItem("TS KG", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(itemTSKG)
            Dim itemEstimatedQty As New GridViewSummaryItem("Estimated Qty", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(itemEstimatedQty)
            Dim itemActualQty As New GridViewSummaryItem("Actual Qty", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(itemActualQty)
            Dim itemEstimatedFATKG As New GridViewSummaryItem("Estimated FAT KG", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(itemEstimatedFATKG)
            Dim itemEstimatedSNFKG As New GridViewSummaryItem("Estimated SNF KG", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(itemEstimatedSNFKG)

            Gv1.ShowGroupPanel = False
            Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadMenuItem1_Click(sender As Object, e As EventArgs) Handles RadMenuItem1.Click
        Try
            If (Gv1.Rows.Count <= 0) Then
                common.clsCommon.MyMessageBoxShow(Me, "No Data To Export", Me.Text)
                Exit Sub
            End If
            If Gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add("Jobwork Milk Production Report")

                Dim strtemp As String = "Date Range : " + clsCommon.GetPrintDate(txtfDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")
                arrHeader.Add(strtemp)

                If clsCommon.myLen(ddlReportType.Text) > 0 Then
                    arrHeader.Add("Report Type : " + ddlReportType.Text)
                End If

                If txtCustomer.arrValueMember IsNot Nothing AndAlso txtCustomer.arrValueMember.Count > 0 Then
                    arrHeader.Add(" Vendor : " + clsCommon.GetMulcallStringWithComma(txtCustomer.arrDispalyMember))
                End If

                If txtMultLocation.arrValueMember IsNot Nothing AndAlso txtMultLocation.arrValueMember.Count > 0 Then
                    arrHeader.Add(" Location : " + clsCommon.GetMulcallStringWithComma(txtMultLocation.arrDispalyMember))
                End If
                transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
                clsCommon.MyExportToExcelGrid("Jobwork Milk Production Report", Gv1, arrHeader, "Jobwork Milk Production Report")
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    ' Ticket No : ERO/05/06/19-000632 By Prabhakar - add [TS%] and [TS KG] column
    Sub loaddata()
        Try
            Gv1.MasterTemplate.SummaryRowsBottom.Clear()
            Dim dtqry As DataTable = Nothing
            Dim qry As String = ""
            Dim dt As DataTable = Nothing
            Dim WhrCls As String = ""
            WhrCls = " and 2=2 "

            If txtCustomer.arrValueMember IsNot Nothing AndAlso txtCustomer.arrValueMember.Count > 0 Then
                WhrCls += " and TSPL_JWI_ESTIMATION_HEAD.Vendor_Code in (" + clsCommon.GetMulcallString(txtCustomer.arrValueMember) + ") "
            End If

            If txtMultLocation.arrValueMember IsNot Nothing AndAlso txtMultLocation.arrValueMember.Count > 0 Then
                WhrCls += " and TSPL_JWI_ESTIMATION_HEAD.Location_Code in (" + clsCommon.GetMulcallString(txtMultLocation.arrValueMember) + ") "
            End If

            WhrCls += " and convert(date,TSPL_JWI_ESTIMATION_HEAD.Document_Date,103) >= convert(date,('" + txtfDate.Value + "'),103) and convert(date,TSPL_JWI_ESTIMATION_HEAD.Document_Date,103) <= convert(date,('" & txtToDate.Value & "'),103)"

            If clsCommon.CompairString(ddlReportType.Text, "All") = CompairStringResult.Equal Then
                qry = "select convert(varchar,xx.Document_Date,103) as [Date],xx.Vendor_Code as [Vendor Code],TSPL_VENDOR_master.Vendor_Name as [Vendor Name] ,xx.Location_Code as [Location Code] " & _
                ",TSPL_LOCATION_MASTER.Location_Desc as [Location Name],xx.Item_Code as [Item Code],tspl_item_master.Item_Desc as [Item Name],xx.Qty as [Actual Qty],xx.UOM as [UOM] " & _
                ",xx.FAT_KG as [FAT KG],xx.SNF_KG as [SNF KG],isnull (xx.FAT_KG,0) + isnull(xx.SNF_KG,0) as [TS KG],xx.Estimated_Qty as [Estimated Qty],xx.Estimated_FAT_KG as [Estimated FAT KG],xx.Estimated_SNF_KG as [Estimated SNF KG] " & _
               " from (" & _
                " select  TSPL_JWI_ESTIMATION_HEAD.Document_Date,TSPL_JWI_ESTIMATION_HEAD.Vendor_Code,TSPL_JWI_ESTIMATION_HEAD.Location_Code,TSPL_JWI_ESTIMATION_FAT_PRODUCTION.Item_Code " & _
                ",TSPL_JWI_ESTIMATION_FAT_PRODUCTION.Qty,TSPL_JWI_ESTIMATION_FAT_PRODUCTION.UOM,TSPL_JWI_ESTIMATION_FAT_PRODUCTION.FAT_KG,0 as SNF_KG " & _
                ",TSPL_JWI_ESTIMATION_FAT_PRODUCTION.Estimated_Qty,TSPL_JWI_ESTIMATION_FAT_PRODUCTION.Estimated_FAT_KG,0 as Estimated_SNF_KG " & _
                " from TSPL_JWI_ESTIMATION_HEAD " & _
                " left join TSPL_JWI_ESTIMATION_FAT_PRODUCTION on TSPL_JWI_ESTIMATION_FAT_PRODUCTION.Document_No=TSPL_JWI_ESTIMATION_HEAD.Document_No " & _
                " where TSPL_JWI_ESTIMATION_HEAD.Status=1 "
                qry += WhrCls
                qry += " union all " & _
                " select  TSPL_JWI_ESTIMATION_HEAD.Document_Date,TSPL_JWI_ESTIMATION_HEAD.Vendor_Code,TSPL_JWI_ESTIMATION_HEAD.Location_Code,TSPL_JWI_ESTIMATION_SNF_PRODUCTION.Item_Code " & _
                ",TSPL_JWI_ESTIMATION_SNF_PRODUCTION.Qty,TSPL_JWI_ESTIMATION_SNF_PRODUCTION.UOM,0 as FAT_KG,TSPL_JWI_ESTIMATION_SNF_PRODUCTION.SNF_KG " & _
                ",TSPL_JWI_ESTIMATION_SNF_PRODUCTION.Estimated_Qty,0 as Estimated_FAT_KG,TSPL_JWI_ESTIMATION_SNF_PRODUCTION.Estimated_SNF_KG " & _
                " from TSPL_JWI_ESTIMATION_HEAD left join TSPL_JWI_ESTIMATION_SNF_PRODUCTION on TSPL_JWI_ESTIMATION_SNF_PRODUCTION.Document_No=TSPL_JWI_ESTIMATION_HEAD.Document_No " & _
                " where TSPL_JWI_ESTIMATION_HEAD.Status=1 "
                qry += WhrCls
                qry += ")xx " & _
                " left join tspl_item_master on tspl_item_master.Item_Code=xx.Item_Code " & _
                " left join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=xx.Location_Code " & _
                " left join TSPL_VENDOR_master on TSPL_VENDOR_master.Vendor_Code=xx.Vendor_Code where 1=1 "
                qry += " order by xx.Document_Date"
            ElseIf clsCommon.CompairString(ddlReportType.Text, "Vendor Wise") = CompairStringResult.Equal Then
                qry = "select xx.Vendor_Code as [Vendor Code],TSPL_VENDOR_master.Vendor_Name as [Vendor Name] " & _
                 ",xx.Item_Code as [Item Code],tspl_item_master.Item_Desc as [Item Name],sum(xx.Qty) as [Actual Qty],xx.UOM as [UOM] " & _
                 ",sum(xx.FAT_KG) as [FAT KG],sum(xx.SNF_KG) as [SNF KG],isnull(sum(xx.FAT_KG),0)+ isnull(sum(xx.SNF_KG),0) as [TS KG],sum(xx.Estimated_Qty) as [Estimated Qty],sum(xx.Estimated_FAT_KG) as [Estimated FAT KG],sum(xx.Estimated_SNF_KG) as [Estimated SNF KG] " & _
                 " from (select  TSPL_JWI_ESTIMATION_HEAD.Document_Date,TSPL_JWI_ESTIMATION_HEAD.Vendor_Code,TSPL_JWI_ESTIMATION_HEAD.Location_Code,TSPL_JWI_ESTIMATION_FAT_PRODUCTION.Item_Code " & _
                 ",TSPL_JWI_ESTIMATION_FAT_PRODUCTION.Qty,TSPL_JWI_ESTIMATION_FAT_PRODUCTION.UOM,TSPL_JWI_ESTIMATION_FAT_PRODUCTION.FAT_KG,0 as SNF_KG " & _
                 ",TSPL_JWI_ESTIMATION_FAT_PRODUCTION.Estimated_Qty,TSPL_JWI_ESTIMATION_FAT_PRODUCTION.Estimated_FAT_KG,0 as Estimated_SNF_KG " & _
                 " from TSPL_JWI_ESTIMATION_HEAD " & _
                 " left join TSPL_JWI_ESTIMATION_FAT_PRODUCTION on TSPL_JWI_ESTIMATION_FAT_PRODUCTION.Document_No=TSPL_JWI_ESTIMATION_HEAD.Document_No " & _
                " where TSPL_JWI_ESTIMATION_HEAD.Status = 1 "
                qry += WhrCls
                qry += " union all " & _
                " select  TSPL_JWI_ESTIMATION_HEAD.Document_Date,TSPL_JWI_ESTIMATION_HEAD.Vendor_Code,TSPL_JWI_ESTIMATION_HEAD.Location_Code,TSPL_JWI_ESTIMATION_SNF_PRODUCTION.Item_Code " & _
                ",TSPL_JWI_ESTIMATION_SNF_PRODUCTION.Qty,TSPL_JWI_ESTIMATION_SNF_PRODUCTION.UOM,0 as FAT_KG,TSPL_JWI_ESTIMATION_SNF_PRODUCTION.SNF_KG " & _
                ",TSPL_JWI_ESTIMATION_SNF_PRODUCTION.Estimated_Qty,0 as Estimated_FAT_KG,TSPL_JWI_ESTIMATION_SNF_PRODUCTION.Estimated_SNF_KG " & _
                " from TSPL_JWI_ESTIMATION_HEAD left join TSPL_JWI_ESTIMATION_SNF_PRODUCTION on TSPL_JWI_ESTIMATION_SNF_PRODUCTION.Document_No=TSPL_JWI_ESTIMATION_HEAD.Document_No " & _
                " where TSPL_JWI_ESTIMATION_HEAD.Status=1 "
                qry += WhrCls
                qry += " )xx " & _
                " left join tspl_item_master on tspl_item_master.Item_Code=xx.Item_Code " & _
                " left join TSPL_VENDOR_master on TSPL_VENDOR_master.Vendor_Code=xx.Vendor_Code " & _
                " where 1=1 " & _
                " group by xx.Vendor_Code,TSPL_VENDOR_master.Vendor_Name,xx.Item_Code,tspl_item_master.Item_Desc,xx.UOM "
                qry += " order by xx.Vendor_Code"
            ElseIf clsCommon.CompairString(ddlReportType.Text, "Location Wise") = CompairStringResult.Equal Then
                qry = "select xx.Location_Code as [Location Code],TSPL_LOCATION_MASTER.Location_Desc as [Location Name]" & _
                 ",xx.Item_Code as [Item Code],tspl_item_master.Item_Desc as [Item Name],sum(xx.Qty) as [Actual Qty],xx.UOM as [UOM] " & _
                 ",sum(xx.FAT_KG) as [FAT KG],sum(xx.SNF_KG) as [SNF KG],isnull(sum(xx.FAT_KG),0)+isnull(sum(xx.SNF_KG),0) as [TS KG],sum(xx.Estimated_Qty) as [Estimated Qty],sum(xx.Estimated_FAT_KG) as [Estimated FAT KG],sum(xx.Estimated_SNF_KG) as [Estimated SNF KG] " & _
                 " from (select  TSPL_JWI_ESTIMATION_HEAD.Document_Date,TSPL_JWI_ESTIMATION_HEAD.Vendor_Code,TSPL_JWI_ESTIMATION_HEAD.Location_Code,TSPL_JWI_ESTIMATION_FAT_PRODUCTION.Item_Code " & _
                 ",TSPL_JWI_ESTIMATION_FAT_PRODUCTION.Qty,TSPL_JWI_ESTIMATION_FAT_PRODUCTION.UOM,TSPL_JWI_ESTIMATION_FAT_PRODUCTION.FAT_KG,0 as SNF_KG " & _
                 ",TSPL_JWI_ESTIMATION_FAT_PRODUCTION.Estimated_Qty,TSPL_JWI_ESTIMATION_FAT_PRODUCTION.Estimated_FAT_KG,0 as Estimated_SNF_KG " & _
                 " from TSPL_JWI_ESTIMATION_HEAD " & _
                 " left join TSPL_JWI_ESTIMATION_FAT_PRODUCTION on TSPL_JWI_ESTIMATION_FAT_PRODUCTION.Document_No=TSPL_JWI_ESTIMATION_HEAD.Document_No " & _
                " where TSPL_JWI_ESTIMATION_HEAD.Status = 1 "
                qry += WhrCls
                qry += " union all " & _
                " select  TSPL_JWI_ESTIMATION_HEAD.Document_Date,TSPL_JWI_ESTIMATION_HEAD.Vendor_Code,TSPL_JWI_ESTIMATION_HEAD.Location_Code,TSPL_JWI_ESTIMATION_SNF_PRODUCTION.Item_Code " & _
                ",TSPL_JWI_ESTIMATION_SNF_PRODUCTION.Qty,TSPL_JWI_ESTIMATION_SNF_PRODUCTION.UOM,0 as FAT_KG,TSPL_JWI_ESTIMATION_SNF_PRODUCTION.SNF_KG " & _
                ",TSPL_JWI_ESTIMATION_SNF_PRODUCTION.Estimated_Qty,0 as Estimated_FAT_KG,TSPL_JWI_ESTIMATION_SNF_PRODUCTION.Estimated_SNF_KG " & _
                " from TSPL_JWI_ESTIMATION_HEAD left join TSPL_JWI_ESTIMATION_SNF_PRODUCTION on TSPL_JWI_ESTIMATION_SNF_PRODUCTION.Document_No=TSPL_JWI_ESTIMATION_HEAD.Document_No " & _
                " where TSPL_JWI_ESTIMATION_HEAD.Status=1 "
                qry += WhrCls
                qry += " )xx " & _
                " left join tspl_item_master on tspl_item_master.Item_Code=xx.Item_Code " & _
                " left join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=xx.Location_Code " & _
                " where 1=1 " & _
                " group by xx.Location_Code,TSPL_LOCATION_MASTER.Location_Desc,xx.Item_Code,tspl_item_master.Item_Desc,xx.UOM "
                qry += " order by xx.Location_Code"
            ElseIf clsCommon.CompairString(ddlReportType.Text, "Date Wise") = CompairStringResult.Equal Then

                qry = "select convert(varchar,xx.Document_Date,103) as [Date] " & _
                ",xx.Item_Code as [Item Code],tspl_item_master.Item_Desc as [Item Name],sum(xx.Qty) as [Actual Qty],xx.UOM as [UOM] " & _
                ",sum(xx.FAT_KG) as [FAT KG],sum(xx.SNF_KG) as [SNF KG], isnull (sum(xx.FAT_KG),0) + isnull (sum(xx.SNF_KG),0)  as [TS KG],sum(xx.Estimated_Qty) as [Estimated Qty],sum(xx.Estimated_FAT_KG) as [Estimated FAT KG],sum(xx.Estimated_SNF_KG) as [Estimated SNF KG] " & _
                " from (select  TSPL_JWI_ESTIMATION_HEAD.Document_Date,TSPL_JWI_ESTIMATION_HEAD.Vendor_Code,TSPL_JWI_ESTIMATION_HEAD.Location_Code,TSPL_JWI_ESTIMATION_FAT_PRODUCTION.Item_Code " & _
                ",TSPL_JWI_ESTIMATION_FAT_PRODUCTION.Qty,TSPL_JWI_ESTIMATION_FAT_PRODUCTION.UOM,TSPL_JWI_ESTIMATION_FAT_PRODUCTION.FAT_KG,0 as SNF_KG " & _
                ",TSPL_JWI_ESTIMATION_FAT_PRODUCTION.Estimated_Qty,TSPL_JWI_ESTIMATION_FAT_PRODUCTION.Estimated_FAT_KG,0 as Estimated_SNF_KG " & _
                " from TSPL_JWI_ESTIMATION_HEAD " & _
                " left join TSPL_JWI_ESTIMATION_FAT_PRODUCTION on TSPL_JWI_ESTIMATION_FAT_PRODUCTION.Document_No=TSPL_JWI_ESTIMATION_HEAD.Document_No " & _
               " where TSPL_JWI_ESTIMATION_HEAD.Status = 1 "
                qry += WhrCls
                qry += " union all " & _
                " select  TSPL_JWI_ESTIMATION_HEAD.Document_Date,TSPL_JWI_ESTIMATION_HEAD.Vendor_Code,TSPL_JWI_ESTIMATION_HEAD.Location_Code,TSPL_JWI_ESTIMATION_SNF_PRODUCTION.Item_Code " & _
                ",TSPL_JWI_ESTIMATION_SNF_PRODUCTION.Qty,TSPL_JWI_ESTIMATION_SNF_PRODUCTION.UOM,0 as FAT_KG,TSPL_JWI_ESTIMATION_SNF_PRODUCTION.SNF_KG " & _
                ",TSPL_JWI_ESTIMATION_SNF_PRODUCTION.Estimated_Qty,0 as Estimated_FAT_KG,TSPL_JWI_ESTIMATION_SNF_PRODUCTION.Estimated_SNF_KG " & _
                " from TSPL_JWI_ESTIMATION_HEAD left join TSPL_JWI_ESTIMATION_SNF_PRODUCTION on TSPL_JWI_ESTIMATION_SNF_PRODUCTION.Document_No=TSPL_JWI_ESTIMATION_HEAD.Document_No " & _
                " where TSPL_JWI_ESTIMATION_HEAD.Status=1 "
                qry += WhrCls
                qry += " )xx " & _
                " left join tspl_item_master on tspl_item_master.Item_Code=xx.Item_Code " & _
                "  where 1=1 " & _
                " group by xx.Document_Date,xx.Item_Code,tspl_item_master.Item_Desc,xx.UOM "
                qry += " order by xx.Document_Date"
            End If

            dtqry = clsDBFuncationality.GetDataTable(qry)
            If dtqry Is Nothing OrElse dtqry.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub
            End If
            Gv1.DataSource = Nothing
            Gv1.Rows.Clear()
            Gv1.Columns.Clear()
            Gv1.DataSource = dtqry
            SummaryData = True
            FormatGrid()
            RadPageView1.SelectedPage = RadPageViewPage2
            ReStoreGridLayout()
            'btnGo.Enabled = False
            'btnReset.Enabled = True
            RadSplitButton1.Enabled = True
            Gv1.BestFitColumns()
            btnGo.Enabled = True
            btnBack.Enabled = False
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub




    Private Sub txtMultLocation__My_Click(sender As Object, e As EventArgs) Handles txtMultLocation._My_Click
        strQry = "select Location_Code  as [Code],Location_Desc as [Name] from TSPL_LOCATION_MASTER"
        txtMultLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("txtMultLocation", strQry, "Code", "Name", txtMultLocation.arrValueMember, txtMultLocation.arrDispalyMember)
    End Sub


    Private Sub txtCustomer__My_Click(sender As Object, e As EventArgs) Handles txtCustomer._My_Click
        Dim qry As String = " select Vendor_Code as Code,Vendor_name as Name from TSPL_VENDOR_master  WHERE  Status='N'  "
        txtCustomer.arrValueMember = clsCommon.ShowMultipleSelectForm("VenMulSel", qry, "Code", "Name", txtCustomer.arrValueMember, txtCustomer.arrDispalyMember)
    End Sub

    Sub LoadTypes()
        dt = New DataTable
        dt.Columns.Add("Code", GetType(String))
        dt.Rows.Add("All")
        dt.Rows.Add("Vendor Wise")
        dt.Rows.Add("Location Wise")
        dt.Rows.Add("Date Wise")
        ddlReportType.DataSource = dt
        ddlReportType.ValueMember = "Code"
        ddlReportType.DisplayMember = "Code"
        ddlReportType.SelectedValue = "All"
    End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        loaddata()
    End Sub

    Private Sub Gv1_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles Gv1.CellDoubleClick
        DrillDown()
    End Sub

    Private Sub Gv1_KeyDown(sender As Object, e As KeyEventArgs) Handles Gv1.KeyDown
        If e.Control And e.KeyCode = Keys.D Then
            DrillDown()
        End If
    End Sub
    Sub DrillDown()
        Try
            If SummaryData = False Then
                Exit Sub
            End If
            Gv1.MasterTemplate.SummaryRowsBottom.Clear()
            Dim dtqry As DataTable = Nothing
            Dim qry As String = ""
            Dim dt As DataTable = Nothing
            Dim WhrCls As String = " and 2=2 "

            If clsCommon.CompairString(ddlReportType.Text, "All") = CompairStringResult.Equal Then
                WhrCls += " and TSPL_JWI_ESTIMATION_HEAD.Location_Code = '" + clsCommon.myCstr(Gv1.CurrentRow.Cells("Location Code").Value) + "' "
                WhrCls += " and TSPL_JWI_ESTIMATION_HEAD.Vendor_code ='" + clsCommon.myCstr(Gv1.CurrentRow.Cells("Vendor Code").Value) + "' "
                WhrCls += " and Item_code = '" + clsCommon.myCstr(Gv1.CurrentRow.Cells("Item Code").Value) + "' "
                WhrCls += " and convert(date,TSPL_JWI_ESTIMATION_HEAD.Document_Date,103) >= convert(date,('" + clsCommon.myCDate(Gv1.CurrentRow.Cells("Date").Value) + "'),103) and convert(date,TSPL_JWI_ESTIMATION_HEAD.Document_Date,103) <= convert(date,('" & clsCommon.myCDate(Gv1.CurrentRow.Cells("Date").Value) & "'),103)"
            ElseIf clsCommon.CompairString(ddlReportType.Text, "Vendor Wise") = CompairStringResult.Equal Then
                WhrCls += " and TSPL_JWI_ESTIMATION_HEAD.Vendor_Code ='" + clsCommon.myCstr(Gv1.CurrentRow.Cells("Vendor Code").Value) + "' "
                WhrCls += " and Item_code = '" + clsCommon.myCstr(Gv1.CurrentRow.Cells("Item Code").Value) + "' "
                If txtMultLocation.arrValueMember IsNot Nothing AndAlso txtMultLocation.arrValueMember.Count > 0 Then
                    WhrCls += " and TSPL_JWI_ESTIMATION_HEAD.Location_Code in (" + clsCommon.GetMulcallString(txtMultLocation.arrValueMember) + ") "
                End If
                WhrCls += " and convert(date,TSPL_JWI_ESTIMATION_HEAD.Document_Date,103) >= convert(date,('" + txtfDate.Value + "'),103) and convert(date,TSPL_JWI_ESTIMATION_HEAD.Document_Date,103) <= convert(date,('" & txtToDate.Value & "'),103)"

            ElseIf clsCommon.CompairString(ddlReportType.Text, "Location Wise") = CompairStringResult.Equal Then
                WhrCls += " and TSPL_JWI_ESTIMATION_HEAD.Location_Code = '" + clsCommon.myCstr(Gv1.CurrentRow.Cells("Location Code").Value) + "' "
                WhrCls += " and Item_code = '" + clsCommon.myCstr(Gv1.CurrentRow.Cells("Item Code").Value) + "' "
                If txtCustomer.arrValueMember IsNot Nothing AndAlso txtCustomer.arrValueMember.Count > 0 Then
                    WhrCls += " and TSPL_JWI_ESTIMATION_HEAD.Vendor_code in (" + clsCommon.GetMulcallString(txtCustomer.arrValueMember) + ") "
                End If
                WhrCls += " and convert(date,TSPL_JWI_ESTIMATION_HEAD.Document_Date,103) >= convert(date,('" + txtfDate.Value + "'),103) and convert(date,TSPL_JWI_ESTIMATION_HEAD.Document_Date,103) <= convert(date,('" & txtToDate.Value & "'),103)"
            ElseIf clsCommon.CompairString(ddlReportType.Text, "Date Wise") = CompairStringResult.Equal Then
                WhrCls += " and convert(date,TSPL_JWI_ESTIMATION_HEAD.Document_Date,103) >= convert(date,('" + clsCommon.myCDate(Gv1.CurrentRow.Cells("Date").Value) + "'),103) and convert(date,TSPL_JWI_ESTIMATION_HEAD.Document_Date,103) <= convert(date,('" & clsCommon.myCDate(Gv1.CurrentRow.Cells("Date").Value) & "'),103)"
                WhrCls += " and Item_code = '" + clsCommon.myCstr(Gv1.CurrentRow.Cells("Item Code").Value) + "' "
                If txtCustomer.arrValueMember IsNot Nothing AndAlso txtCustomer.arrValueMember.Count > 0 Then
                    WhrCls += " and TSPL_JWI_ESTIMATION_HEAD.Vendor_code in (" + clsCommon.GetMulcallString(txtCustomer.arrValueMember) + ") "
                End If

                If txtMultLocation.arrValueMember IsNot Nothing AndAlso txtMultLocation.arrValueMember.Count > 0 Then
                    WhrCls += " and TSPL_JWI_ESTIMATION_HEAD.Location_Code in (" + clsCommon.GetMulcallString(txtMultLocation.arrValueMember) + ") "
                End If
            End If

            qry = "select xx.document_no as [Doc No],convert(varchar,xx.Document_Date,103) as [Doc Date],xx.Vendor_Code as [Vendor Code],TSPL_VENDOR_master.Vendor_Name as [Vendor Name] ,xx.Location_Code as [Location Code] " & _
               ",TSPL_LOCATION_MASTER.Location_Desc as [Location Name],xx.Item_Code as [Item Code],tspl_item_master.Item_Desc as [Item Name],xx.Qty as [Qty],xx.UOM as [UOM] " & _
               ",xx.FAT_Per as [FAT%],xx.SNF_Per as [SNF%],xx.FAT_KG as [FAT KG],xx.SNF_KG as [SNF KG],xx.Estimated_Qty as [Estimated Qty],xx.Estimated_FAT_KG as [Estimated FAT KG],xx.Estimated_SNF_KG as [Estimated SNF KG] " & _
              " from (" & _
               " select TSPL_JWI_ESTIMATION_HEAD.document_no,TSPL_JWI_ESTIMATION_HEAD.Document_Date,TSPL_JWI_ESTIMATION_HEAD.Vendor_Code,TSPL_JWI_ESTIMATION_HEAD.Location_Code,TSPL_JWI_ESTIMATION_FAT_PRODUCTION.Item_Code " & _
               ",TSPL_JWI_ESTIMATION_FAT_PRODUCTION.Qty,TSPL_JWI_ESTIMATION_FAT_PRODUCTION.UOM,TSPL_JWI_ESTIMATION_FAT_PRODUCTION.FAT_Per,0 as SNF_Per,TSPL_JWI_ESTIMATION_FAT_PRODUCTION.FAT_KG,0 as SNF_KG " & _
               ",TSPL_JWI_ESTIMATION_FAT_PRODUCTION.Estimated_Qty,TSPL_JWI_ESTIMATION_FAT_PRODUCTION.Estimated_FAT_KG,0 as Estimated_SNF_KG " & _
               " from TSPL_JWI_ESTIMATION_HEAD " & _
               " left join TSPL_JWI_ESTIMATION_FAT_PRODUCTION on TSPL_JWI_ESTIMATION_FAT_PRODUCTION.Document_No=TSPL_JWI_ESTIMATION_HEAD.Document_No " & _
               " where TSPL_JWI_ESTIMATION_HEAD.Status=1 "
            qry += WhrCls
            qry += " union all " & _
            " select  TSPL_JWI_ESTIMATION_HEAD.document_no,TSPL_JWI_ESTIMATION_HEAD.Document_Date,TSPL_JWI_ESTIMATION_HEAD.Vendor_Code,TSPL_JWI_ESTIMATION_HEAD.Location_Code,TSPL_JWI_ESTIMATION_SNF_PRODUCTION.Item_Code " & _
            ",TSPL_JWI_ESTIMATION_SNF_PRODUCTION.Qty,TSPL_JWI_ESTIMATION_SNF_PRODUCTION.UOM,0 as FAT_Per,TSPL_JWI_ESTIMATION_SNF_PRODUCTION.SNF_Per,0 as FAT_KG,TSPL_JWI_ESTIMATION_SNF_PRODUCTION.SNF_KG " & _
            ",TSPL_JWI_ESTIMATION_SNF_PRODUCTION.Estimated_Qty,0 as Estimated_FAT_KG,TSPL_JWI_ESTIMATION_SNF_PRODUCTION.Estimated_SNF_KG " & _
            " from TSPL_JWI_ESTIMATION_HEAD left join TSPL_JWI_ESTIMATION_SNF_PRODUCTION on TSPL_JWI_ESTIMATION_SNF_PRODUCTION.Document_No=TSPL_JWI_ESTIMATION_HEAD.Document_No " & _
            " where TSPL_JWI_ESTIMATION_HEAD.Status=1 "
            qry += WhrCls
            qry += ")xx " & _
            " left join tspl_item_master on tspl_item_master.Item_Code=xx.Item_Code " & _
            " left join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=xx.Location_Code " & _
            " left join TSPL_VENDOR_master on TSPL_VENDOR_master.Vendor_Code=xx.Vendor_Code where 1=1 "
            qry += " order by xx.Document_Date,xx.document_no"

            dtqry = clsDBFuncationality.GetDataTable(qry)
            If dtqry Is Nothing OrElse dtqry.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub
            End If
            Gv1.DataSource = Nothing
            Gv1.Rows.Clear()
            Gv1.Columns.Clear()
            Gv1.DataSource = dtqry
            SummaryData = False
            FormatGrid()
            RadPageView1.SelectedPage = RadPageViewPage2

            'btnGo.Enabled = False
            'btnReset.Enabled = True
            RadSplitButton1.Enabled = True
            Gv1.BestFitColumns()
            btnGo.Enabled = False
            btnBack.Enabled = True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(PageSetupReport_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(PageSetupReport_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= Gv1.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To Gv1.Columns.Count - 1 Step ii + 1
                        Gv1.Columns(ii).IsVisible = False
                        Gv1.Columns(ii).VisibleInColumnChooser = True
                    Next
                    Gv1.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub

    Private Sub rmSaveLayout_Click(sender As Object, e As EventArgs) Handles rmSaveLayout.Click
        If clsCommon.myLen(PageSetupReport_ID) > 0 Then
            Gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = PageSetupReport_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            Gv1.SaveLayout(obj.GridLayout)
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            obj.GridColumns = Gv1.ColumnCount
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully", "Information", Me.Text)
            End If


            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
            ''---------------
        End If
    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(PageSetupReport_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", "Information", Me.Text)
    End Sub

    Private Sub RadMenuItem2_Click(sender As Object, e As EventArgs) Handles RadMenuItem2.Click
        Try
            If (Gv1.Rows.Count <= 0) Then
                common.clsCommon.MyMessageBoxShow(Me, "No Data To Export", Me.Text)
                Exit Sub
            End If
            If Gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add("Jobwork Milk Production Report")

                Dim strtemp As String = "Date Range : " + clsCommon.GetPrintDate(txtfDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")
                arrHeader.Add(strtemp)

                If clsCommon.myLen(ddlReportType.Text) > 0 Then
                    arrHeader.Add("Report Type : " + ddlReportType.Text)
                End If

                If txtCustomer.arrValueMember IsNot Nothing AndAlso txtCustomer.arrValueMember.Count > 0 Then
                    arrHeader.Add(" Vendor : " + clsCommon.GetMulcallStringWithComma(txtCustomer.arrDispalyMember))
                End If

                If txtMultLocation.arrValueMember IsNot Nothing AndAlso txtMultLocation.arrValueMember.Count > 0 Then
                    arrHeader.Add(" Location : " + clsCommon.GetMulcallStringWithComma(txtMultLocation.arrDispalyMember))
                End If
                transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
                clsCommon.MyExportToPDF("Jobwork Milk Production Report", Gv1, arrHeader, "Jobwork Milk Production Report", PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class
