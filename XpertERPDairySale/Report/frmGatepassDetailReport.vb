Imports System.IO
Imports common
Public Class frmGatepassDetailReport
    Inherits FrmMainTranScreen
    Dim EnableProductSaleForJPR As Boolean = False

    Private Sub frmGatepassDetailReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            EnableProductSaleForJPR = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.EnableProductSaleForJPR, clsFixedParameterCode.EnableProductSaleForJPR, Nothing)) = 1, True, False)
            fromDate.Value = clsCommon.GETSERVERDATE()
            ToDate.Value = clsCommon.GETSERVERDATE()
            rbtnGatepassDate.Checked = True
            rbtnBothShift.Checked = True
            rbtnBoth.Checked = True
            If EnableProductSaleForJPR Then
                rbtnIceCream.Visible = True
                rbtnBoth.Visible = False
            Else
                rbtnIceCream.Visible = False
                rbtnBoth.Visible = True
            End If
            RadPageView1.SelectedPage = RadPageViewPage1
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Try
            fromDate.EnableCodedUITests = True
            ToDate.Enabled = True
            txtMultRoute.Enabled = True
            RadGroupBox1.Enabled = True
            RadGroupBox2.Enabled = True
            RadGroupBox3.Enabled = True
            RadGroupBox4.Enabled = True
            btnGo.Enabled = True
            BlankGrid()
            rbtnMilk.Checked = True
            RadPageView1.SelectedPage = RadPageViewPage1
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtMultRoute__My_Click(sender As Object, e As EventArgs) Handles txtMultRoute._My_Click
        Try
            Dim Qry As String = "Select TSPL_ROUTE_MASTER.Route_No As [Route Code],TSPL_ROUTE_MASTER.Route_Desc As [Description] from TSPL_ROUTE_MASTER
                                Left Outer Join (Select Route_No from TSPL_DAIRYSALE_GATEPASS_MASTER Group By Route_No)TSPL_DAIRYSALE_GATEPASS_MASTER On TSPL_DAIRYSALE_GATEPASS_MASTER.Route_No=TSPL_ROUTE_MASTER.Route_No"
            txtMultRoute.arrValueMember = clsCommon.ShowMultipleSelectForm("@Route", Qry, "Route Code", "Description", txtMultRoute.arrValueMember, txtMultRoute.arrDispalyMember)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Try
            If clsCommon.myCDate(fromDate.Value, "dd/MM/yyyy") > clsCommon.myCDate(ToDate.Value, "dd/MM/yyyy") Then
                Throw New Exception("From Date can't be less than To Date !")
            End If
            LoadData()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub LoadData()
        Dim ItemCode As String = Nothing
        Dim ItemName As String = Nothing
        Dim tItem As String = Nothing
        Dim ItemType As String = ""
        If EnableProductSaleForJPR Then
            ItemType = " MainFinal.[Item Type] ,"
        End If

        Dim Qry As String = "select TSPL_DAIRYSALE_GATEPASS_MASTER.GPCode As [Gatepass No],case when TSPL_DAIRYSALE_GATEPASS_MASTER.item_type = 'M' then 'Milk' when TSPL_DAIRYSALE_GATEPASS_MASTER.item_type = 'P' then 'Product' when TSPL_DAIRYSALE_GATEPASS_MASTER.item_type = 'I' then 'Ice Cream' end  as [Item Type] ,Format(TSPL_DAIRYSALE_GATEPASS_MASTER.GPDate,'dd/MM/yyyy hh:mm tt') as [Gatepass Date],Format(TSPL_DAIRYSALE_GATEPASS_MASTER.Supply_Date,'dd/MM/yyyy') as [Supply Date],"
        Qry += "TSPL_DAIRYSALE_GATEPASS_MASTER.ShiftType As [Shift Type],TSPL_DAIRYSALE_GATEPASS_MASTER.Route_No As [Route No],TSPL_ROUTE_MASTER.Route_Desc As [Route Desc],
TSPL_DAIRYSALE_GATEPASS_MASTER.DistributorName As [Distributor Name],TSPL_DAIRYSALE_GATEPASS_MASTER.Vehicle_Number As [Vehicle Number],TSPL_DAIRYSALE_GATEPASS_MASTER.Driver_Name As [Driver Name],TSPL_DAIRYSALE_GATEPASS_MASTER.Driver_ContactNo As [Driver Contact No.],TSPL_DAIRYSALE_GATEPASS_MASTER.Loading_Slip As [Loading Slip],TSPL_DAIRYSALE_GATEPASS_MASTER.Remarks,TSPL_DAIRYSALE_GATEPASS_MASTER.Comments,TSPL_DAIRYSALE_GATEPASS_MASTER.Trip_No As [Trip No],TSPL_DAIRYSALE_GATEPASS_MASTER.TotalCrate As [Total Crate],
TSPL_DAIRYSALE_GATEPASS_DETAIL.Item_Code As [Item Code],TSPL_ITEM_MASTER.Short_Description As [Short Description],TSPL_DAIRYSALE_GATEPASS_DETAIL.Unit_Code As [Unit Code],
TSPL_DAIRYSALE_GATEPASS_DETAIL.Qty,
(TSPL_DAIRYSALE_GATEPASS_DETAIL.Qty*ItemConvinUOM.Conversion_Factor/ItemConvReportUOM.Conversion_Factor) as [QtyAccReportUOM],ItemConvReportUOM.UOM_Code As [ReportUOM]
from TSPL_DAIRYSALE_GATEPASS_MASTER 
left join TSPL_DAIRYSALE_GATEPASS_DETAIL on TSPL_DAIRYSALE_GATEPASS_DETAIL.GPCode=TSPL_DAIRYSALE_GATEPASS_MASTER.GPCode
left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code= TSPL_DAIRYSALE_GATEPASS_DETAIL.Item_Code
left join TSPL_ITEM_UOM_DETAIL as ItemConvReportUOM on TSPL_DAIRYSALE_GATEPASS_DETAIL.Item_Code = ItemConvReportUOM.Item_Code and ItemConvReportUOM.Report_UOM=1
left join TSPL_ITEM_UOM_DETAIL as ItemConvinUOM on TSPL_DAIRYSALE_GATEPASS_DETAIL.Item_Code = ItemConvinUOM.Item_Code and TSPL_DAIRYSALE_GATEPASS_DETAIL.Unit_Code = ItemConvinUOM.UOM_Code 
left join TSPL_ROUTE_MASTER on TSPL_ROUTE_MASTER.Route_No=TSPL_DAIRYSALE_GATEPASS_MASTER.Route_No "

        If rbtnGatepassDate.Checked Then
            Qry += " Where convert(date,TSPL_DAIRYSALE_GATEPASS_MASTER.GPDate,103)>='" + clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") + "' And convert(date,TSPL_DAIRYSALE_GATEPASS_MASTER.GPDate,103)<='" + clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") + "' "
        Else
            Qry += " Where convert(date,TSPL_DAIRYSALE_GATEPASS_MASTER.Supply_Date,103)>='" + clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") + "' And convert(date,TSPL_DAIRYSALE_GATEPASS_MASTER.Supply_Date,103)<='" + clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") + "' "
        End If

        If rbtnMorning.Checked Then
            Qry += " and TSPL_DAIRYSALE_GATEPASS_MASTER.ShiftType='Morning' "
        ElseIf rbtnEvening.Checked Then
            Qry += " and TSPL_DAIRYSALE_GATEPASS_MASTER.ShiftType='Evening' "
        End If

        If EnableProductSaleForJPR Then
            If rbtnMilk.Checked Then
                Qry += " and TSPL_DAIRYSALE_GATEPASS_MASTER.item_type='M' "
            ElseIf rbtnProduct.Checked Then
                Qry += " and TSPL_DAIRYSALE_GATEPASS_MASTER.item_type='P' "
            ElseIf rbtnIceCream.Checked Then
                Qry += " and TSPL_DAIRYSALE_GATEPASS_MASTER.item_type='I' "
            End If
        Else
            If rbtnMilk.Checked Then
                Qry += " and TSPL_ITEM_MASTER.Is_FreshItem=1 "
            ElseIf rbtnProduct.Checked Then
                Qry += " and TSPL_ITEM_MASTER.Is_Ambient=1 "
            End If
        End If


        If txtMultRoute.arrValueMember IsNot Nothing AndAlso txtMultRoute.arrValueMember.Count > 0 Then
            Qry += " and TSPL_DAIRYSALE_GATEPASS_MASTER.Route_No In (" & clsCommon.GetMulcallString(txtMultRoute.arrValueMember) & ") "
        End If
        Qry += " and ItemConvReportUOM.Report_UOM=1"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry + " Order By IsNull(TSPL_ITEM_MASTER.Sku_Seq,0) ASC")

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            For Each rows In dt.Rows
                If clsCommon.myLen(ItemCode) > 0 Then
                    If Not ItemCode.Contains(clsCommon.myCstr(rows("Item Code"))) Then
                        'ItemCode += "," + clsCommon.myCstr(rows("Item Code"))
                        'ItemName += "," + " Case When Max(xxxfinal.[Item Code])='" + clsCommon.myCstr(rows("Item Code")) + "' Then Sum(xxxfinal.QtyinLtr) Else 0 End As [" + clsCommon.myCstr(rows("Short Description")) + "]"
                        ItemCode += "," + clsCommon.myCstr(rows("Item Code"))
                        tItem += ",Sum(MainFinal.[" + clsCommon.myCstr(rows("Short Description")) + "]) As [" + clsCommon.myCstr(rows("Short Description")) + "(" + clsCommon.myCstr(rows("ReportUOM")) + ")]"
                        ItemName += "," + " Case When Max(xxxfinal.[Item Code])='" + clsCommon.myCstr(rows("Item Code")) + "' Then Sum(xxxfinal.QtyAccReportUOM) Else 0 End As [" + clsCommon.myCstr(rows("Short Description")) + "]"
                    End If
                Else
                    'ItemCode = clsCommon.myCstr(rows("Item Code"))
                    'ItemName = ",Case When Max(xxxfinal.[Item Code])='" + clsCommon.myCstr(rows("Item Code")) + "' Then Sum(xxxfinal.QtyinLtr) Else 0 End As [" + clsCommon.myCstr(rows("Short Description")) + "]"
                    ItemCode = "," + clsCommon.myCstr(rows("Item Code"))
                    tItem = ",Sum(MainFinal.[" + clsCommon.myCstr(rows("Short Description")) + "]) As [" + clsCommon.myCstr(rows("Short Description")) + "(" + clsCommon.myCstr(rows("ReportUOM")) + ")]"
                    ItemName = "," + " Case When Max(xxxfinal.[Item Code])='" + clsCommon.myCstr(rows("Item Code")) + "' Then Sum(xxxfinal.QtyAccReportUOM) Else 0 End As [" + clsCommon.myCstr(rows("Short Description")) + "]"
                End If
            Next
        Else
            Throw New Exception("Data Not Found !")
        End If

        dt = Nothing
        Dim finalQry As String = "Select xxxfinal.[Gatepass No],xxxfinal.[Item Type],xxxfinal.[Gatepass Date],xxxfinal.[Supply Date],xxxfinal.[Shift Type],xxxfinal.[Route No],Max(xxxfinal.[Route Desc])[Route Desc],Max([Distributor Name])[Distributor Name],Max(xxxfinal.[Vehicle Number])[Vehicle Number],Max(xxxfinal.[Driver Name])[Driver Name],Max(xxxfinal.[Driver Contact No.])[Driver Contact No.],Max(xxxfinal.[Loading Slip])[Loading Slip],Max(xxxfinal.Remarks)[Remarks],Max(xxxfinal.Comments)[Comments],Max(xxxfinal.[Trip No])[Trip No],Max(xxxfinal.[Total Crate])[Total Crate] "
        finalQry += "" + ItemName + ""
        finalQry += " from (" + Qry + ") As xxxfinal  Group By xxxfinal.[Gatepass No],xxxfinal.[Item Type],xxxfinal.[Gatepass Date],xxxfinal.[Supply Date],xxxfinal.[Shift Type],xxxfinal.[Route No],xxxfinal.[Item Code]"

        Qry = "Select MainFinal.[Gatepass No]," & ItemType & " MainFinal.[Gatepass Date],MainFinal.[Supply Date],"
        Qry += "MainFinal.[Shift Type],MainFinal.[Route No],Max(MainFinal.[Route Desc])[Route Desc],Max(MainFinal.[Distributor Name])[Distributor Name],Max(MainFinal.[Vehicle Number])[Vehicle Number],Max(MainFinal.[Driver Name])[Driver Name],Max(MainFinal.[Driver Contact No.])[Driver Contact No.],Max(MainFinal.[Loading Slip])[Loading Slip],Max(MainFinal.Remarks)[Remarks],Max(MainFinal.Comments)[Comments],Max(MainFinal.[Trip No])[Trip No],Max(MainFinal.[Total Crate])[Total Crate] "
        Qry += "" + tItem + ""
        Qry += "from (" + finalQry + ")As MainFinal Group By MainFinal.[Gatepass No]," & ItemType & "MainFinal.[Gatepass Date],MainFinal.[Supply Date],MainFinal.[Shift Type],MainFinal.[Route No],MainFinal.[Vehicle Number],MainFinal.[Loading Slip]"
        dt = clsDBFuncationality.GetDataTable(Qry)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            BlankGrid()
            Gv1.DataSource = dt
            RadPageView1.SelectedPage = RadPageViewPage2
            Gv1.EnableFiltering = True
            Gv1.ReadOnly = True
            ControlEnableDisable()
            Gv1.BestFitColumns()
            FormatGrid()
            ReStoreGridLayout()
        Else
            Throw New Exception("Data Not Found !")
        End If
    End Sub

    Sub FormatGrid()
        Gv1.TableElement.TableHeaderHeight = 40
        Gv1.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To Gv1.Columns.Count - 1
            Gv1.Columns(ii).ReadOnly = True
            Gv1.Columns(ii).IsVisible = True
            'Gv1.Columns("Remarks").VisibleInColumnChooser = True
        Next
        Gv1.Columns("Route Desc").IsVisible = False
        Gv1.Columns("Route Desc").VisibleInColumnChooser = True

        Gv1.Columns("Driver Name").IsVisible = False
        Gv1.Columns("Driver Name").VisibleInColumnChooser = True

        Gv1.Columns("Driver Contact No.").IsVisible = False
        Gv1.Columns("Driver Contact No.").VisibleInColumnChooser = True

        Gv1.Columns("Comments").IsVisible = False
        Gv1.Columns("Comments").VisibleInColumnChooser = True

        Gv1.Columns("Trip No").IsVisible = False
        Gv1.Columns("Trip No").VisibleInColumnChooser = True

        Gv1.Columns("Total Crate").IsVisible = False
        Gv1.Columns("Total Crate").VisibleInColumnChooser = True

        Gv1.AllowAddNewRow = False
        Gv1.ShowGroupPanel = True
        Gv1.BestFitColumns()
    End Sub
    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(Me.Form_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(Me.Form_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= Gv1.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To Gv1.Columns.Count - 1 Step ii + 1
                        Gv1.Columns(ii).IsVisible = False
                    Next
                    Gv1.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
            If Gv1 IsNot Nothing AndAlso Gv1.Rows.Count > 0 Then
                Gv1.MasterTemplate.SummaryRowsBottom.Clear()
                Dim summaryRowItem As New GridViewSummaryRowItem()
                Dim chkIndex As Integer = 0
                For i As Integer = 10 To Gv1.Columns.Count - 1
                    If (Gv1.Columns(i).Name).Contains("Slip") Then
                        chkIndex = i
                    ElseIf (Gv1.Columns(i).Name).Contains("Remarks") Then
                        chkIndex = i
                    End If
                    If chkIndex > 0 AndAlso i > chkIndex Then
                        Dim item As New GridViewSummaryItem(Gv1.Columns(i).Name, "{0:F2}", GridAggregateFunction.Sum)
                        summaryRowItem.Add(item)
                    End If
                Next
                Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
                Gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub

    Sub BlankGrid()
        Gv1.DataSource = Nothing
        Gv1.Rows.Clear()
        Gv1.Columns.Clear()
        Gv1.GroupDescriptors.Clear()
        Gv1.MasterTemplate.SummaryRowsBottom.Clear()
        Gv1.MasterView.Refresh()
    End Sub
    Sub ControlEnableDisable()
        txtMultRoute.Enabled = False
        RadGroupBox1.Enabled = False
        RadGroupBox2.Enabled = False
        RadGroupBox3.Enabled = False
        RadGroupBox4.Enabled = False
        btnGo.Enabled = False
    End Sub

    Private Sub RadMenuItem2_Click(sender As Object, e As EventArgs) Handles RadMenuItem2.Click
        Try
            If Gv1 IsNot Nothing AndAlso Gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Name : Gatepass Detail Report")
                arrHeader.Add("From Date : " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " to " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy"))
                Dim StrHeading As String = objCommonVar.CurrentCompanyName + Environment.NewLine + "Gatepass Detail Report From Date " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " to " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy")
                applyExportTemplate(Gv1, PageSetupReport_ID)
                clsCommon.MyExportToExcelGrid(objCommonVar.CurrentCompanyName, Gv1, arrHeader, "Gatepass Detail Report")
                clsCommon.MyMessageBoxShow(Me, "Exported Successfully.", Me.Text)
                'clsCommon.MyOldExportToPDF(objCommonVar.CurrentCompanyName, Gv1, arrHeader, "DispathLedgerReport")
            Else
                Throw New Exception("Data Not Found To Export !")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub rmSaveLayout_Click(sender As Object, e As EventArgs) Handles rmSaveLayout.Click
        Try
            If clsCommon.myLen(Me.Form_ID) > 0 Then
                Dim obj As New clsGridLayout()
                Gv1.MasterTemplate.FilterDescriptors.Clear()
                obj = New clsGridLayout()
                obj.ReportID = Me.Form_ID
                obj.UserID = objCommonVar.CurrentUserCode
                obj.GridLayout = New MemoryStream()
                Gv1.SaveLayout(obj.GridLayout)
                obj.GridColumns = Gv1.ColumnCount
                obj.UserID = objCommonVar.CurrentUserCode
                obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                If obj.SaveData() Then
                    clsCommon.MyMessageBoxShow(Me, "Layout saved successfully", Me.Text)
                End If
                obj.GridLayout.Close()
                obj.GridLayout.Dispose()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        Try
            If clsCommon.myLen(PageSetupReport_ID) > 0 Then
                clsGridLayout.DeleteData(PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class