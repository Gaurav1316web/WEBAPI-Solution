Imports common
Public Class frmIBPReport
    Private Sub frmIBPReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            txtFromDate.Value = clsCommon.GETSERVERDATE()
            txtToDate.Value = txtFromDate.Value
            Reset()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Try
            Reset()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub EnabledControls()
        txtFromDate.Enabled = True
        txtToDate.Enabled = True
        txtMultMCC.Enabled = True
        txtMultItem.Enabled = True
    End Sub
    Sub DisabledControls()
        txtFromDate.Enabled = False
        txtToDate.Enabled = False
        txtMultMCC.Enabled = False
        txtMultItem.Enabled = False
    End Sub

    Sub Reset()
        gv.DataSource = Nothing
        gv.Rows.Clear()
        gv.MasterTemplate.Rows.Clear()
        gv.Refresh()
        EnabledControls()
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

    Function ReturnQry(ByVal IsPrint As Boolean) As String
        Dim strFromDate As String = clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt")
        Dim strToDate As String = clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt")

        Dim strQry As String = "Select "
        If IsPrint Then
            strQry += " TSPL_COMPANY_MASTER.Comp_Name,TSPL_COMPANY_MASTER.Add1,TSPL_CITY_MASTER.City_Name,'I.B.P. Report Of FGS For Period : '+'" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + "'+' To '+'" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy") + "' As ReportName,'Run Date : '+'" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy") + "' As RunDate,"
        End If
        strQry += " Item_Desc,OPBal,RecieptProd,RecieptOther,(OPBal+RecieptProd+RecieptOther) As Total_In,Sale,PDOther,(Sale+PDOther)Total_Out,
((OPBal+RecieptProd+RecieptOther)-(Sale+PDOther)) As Balance
from (Select Sku_Seq,Item_Code, 
Max(Item_Desc)Item_Desc,
Sum(CinCFStockQty * Case When Punching_Date< '" + strFromDate + "' And Trans_Type='Transfer' And InOut='I' Then 1 Else 0 End) As OPBal,
Sum(CinCFStockQty * Case When Punching_Date>='" + strFromDate + "' And Punching_Date<='" + strToDate + "' And Trans_Type='Transfer' And InOut='I' And IsProduction=1 Then 1 Else 0 End) As RecieptProd,
Sum(CinCFStockQty * Case When Punching_Date>='" + strFromDate + "' And Punching_Date<='" + strToDate + "' And Trans_Type='Transfer' And InOut='I' And IsProduction=1 Then 0 Else 1 End) As RecieptOther,
Sum(CinCFStockQty * Case When Punching_Date>='" + strFromDate + "' And Punching_Date<='" + strToDate + "' And Trans_Type='MCC-MSALE'  Then 1 Else 0 End) As Sale,
Sum(CinCFStockQty * Case When Punching_Date>='" + strFromDate + "' And Punching_Date<='" + strToDate + "' And Trans_Type='Transfer' And InOut='O' Then 1 Else 0 End) As PDOther
from(
select TSPL_LOCATION_MASTER.Location_Code,TSPL_INVENTORY_MOVEMENT.Punching_Date,TSPL_INVENTORY_MOVEMENT.Trans_Type,TSPL_INVENTORY_MOVEMENT.InOut,
TSPL_ITEM_MASTER.Sku_Seq,TSPL_ITEM_MASTER.Item_Code,TSPL_ITEM_MASTER.Item_Desc,TSPL_ITEM_MASTER.Item_Used_as,TSPL_INVENTORY_MOVEMENT.Stock_Qty,TSPL_INVENTORY_MOVEMENT.Stock_UOM,
TSPL_LOCATION_MASTER.IsProduction,
Case When CinCF.Report_UOM<>1 Then TSPL_INVENTORY_MOVEMENT.Stock_Qty Else Convert(decimal(18,2),((IsNull(TSPL_INVENTORY_MOVEMENT.Stock_Qty,0) / IsNull(CinCF.Conversion_Factor,1)))) End As CinCFStockQty,CinCF.UOM_Code
from TSPL_INVENTORY_MOVEMENT
Left Outer Join TSPL_ITEM_MASTER On TSPL_ITEM_MASTER.Item_Code=TSPL_INVENTORY_MOVEMENT.Item_Code
Left Outer Join TSPL_ITEM_UOM_DETAIL On TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_ITEM_MASTER.Item_Code And TSPL_ITEM_UOM_DETAIL.UOM_Code=TSPL_INVENTORY_MOVEMENT.Stock_UOM
Left Outer Join (Select Item_Code,UOM_Code,Conversion_Factor,IsNull(Report_UOM,0)Report_UOM from TSPL_ITEM_UOM_DETAIL Where Report_UOM=1)CinCF On CinCF.Item_Code=TSPL_ITEM_MASTER.Item_Code 
Left Outer Join TSPL_LOCATION_MASTER On TSPL_LOCATION_MASTER.Location_Code=TSPL_INVENTORY_MOVEMENT.Location_Code
Where  TSPL_INVENTORY_MOVEMENT.Punching_Date<='" + strToDate + "'"
        If txtMultMCC.arrValueMember IsNot Nothing AndAlso txtMultMCC.arrValueMember.Count > 0 Then
            strQry += " And TSPL_LOCATION_MASTER.Location_Code In (" & clsCommon.GetMulcallString(txtMultMCC.arrValueMember) & ")"
        End If
        If txtMultItem.arrValueMember IsNot Nothing AndAlso txtMultItem.arrValueMember.Count > 0 Then
            strQry += " And TSPL_ITEM_MASTER.Item_Code In (" & clsCommon.GetMulcallString(txtMultItem.arrValueMember) & ")"
        End If
        strQry += " )BaseQry Group By Location_Code,Item_Code,Sku_Seq)finalQry 
Left Outer Join TSPL_COMPANY_MASTER On TSPL_COMPANY_MASTER.Comp_Code1='" + objCommonVar.CurrComp_Code1 + "'
Left Outer Join TSPL_CITY_MASTER  On TSPL_CITY_MASTER.City_Code=TSPL_COMPANY_MASTER.City_Code Order By Sku_Seq"
        Return strQry
    End Function

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Try
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(ReturnQry(False))
            gv.DataSource = Nothing
            gv.Rows.Clear()
            gv.Columns.Clear()
            gv.GroupDescriptors.Clear()
            gv.MasterTemplate.SummaryRowsBottom.Clear()
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                gv.DataSource = dt
                gv.AutoExpandGroups = True
                gv.ShowGroupPanel = True
                gv.ShowRowHeaderColumn = False
                gv.AllowAddNewRow = False
                gv.AllowDeleteRow = False
                gv.EnableFiltering = True
                gv.ShowFilteringRow = True
                gv.BestFitColumns()
                SetGridFormat()
                'View()
                gv.BestFitColumns()
                RadPageView1.SelectedPage = RadPageViewPage2
                DisabledControls()
            Else
                Throw New Exception("No Data Found !")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub SetGridFormat()
        gv.AutoExpandGroups = False
        gv.ShowGroupPanel = False
        gv.ShowRowHeaderColumn = False
        gv.AllowAddNewRow = False
        gv.AllowDeleteRow = False
        gv.EnableFiltering = True
        gv.ShowFilteringRow = True
        gv.ReadOnly = True
        For ii As Integer = 0 To gv.Columns.Count - 1
            gv.Columns(ii).ReadOnly = True
            gv.Columns(ii).BestFit()
        Next

        gv.Columns("Item_Desc").HeaderText = "Particular"
        gv.Columns("Item_Desc").IsVisible = True

        gv.Columns("OPBal").HeaderText = "Opening Balance"
        gv.Columns("OPBal").IsVisible = True

        gv.Columns("RecieptProd").HeaderText = "Production"
        gv.Columns("RecieptProd").IsVisible = True

        gv.Columns("RecieptOther").HeaderText = "Other"
        gv.Columns("RecieptOther").IsVisible = True

        gv.Columns("Total_In").HeaderText = "Total"
        gv.Columns("Total_In").IsVisible = True

        gv.Columns("Sale").HeaderText = "Sale"
        gv.Columns("Sale").IsVisible = True

        gv.Columns("PDOther").HeaderText = "P.D. & Other"
        gv.Columns("PDOther").IsVisible = True

        gv.Columns("Total_Out").HeaderText = "Total"
        gv.Columns("Total_Out").IsVisible = True

        gv.Columns("Balance").HeaderText = "Balance"
        gv.Columns("Balance").IsVisible = True


        Dim summaryRowItem As New GridViewSummaryRowItem()
        For ii As Integer = 1 To gv.Columns.Count - 1
            summaryRowItem.Add(New GridViewSummaryItem(gv.Columns(ii).Name, "{0:F2}", GridAggregateFunction.Sum))
        Next
        gv.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        gv.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
    End Sub

    Sub View()
        If gv.Rows.Count > 0 Then
            Dim view As New ColumnGroupsViewDefinition()

            view.ColumnGroups.Add(New GridViewColumnGroup(""))
            view.ColumnGroups(0).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv.Columns("Item_Desc").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv.Columns("OPBal").Name)


            view.ColumnGroups.Add(New GridViewColumnGroup("Receipt"))
            view.ColumnGroups(1).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv.Columns("RecieptProd").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv.Columns("RecieptOther").Name)

            view.ColumnGroups.Add(New GridViewColumnGroup(""))
            view.ColumnGroups(2).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv.Columns("Total_In").Name)
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv.Columns("Sale").Name)
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv.Columns("PDOther").Name)
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv.Columns("Total_Out").Name)
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv.Columns("Balance").Name)

            gv.ViewDefinition = view
        End If
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Try
            Me.Close()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Try
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(ReturnQry(True))
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim frmCRV As New frmCrystalReportViewer()
                frmCRV.funreport(False, CrystalReportFolder.KwalitySalesReport, dt, "rptIBPReport", "IBP Report")
                frmCRV = Nothing
            Else
                Throw New Exception("No Data Found !")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtMultMCC__My_Click(sender As Object, e As EventArgs) Handles txtMultMCC._My_Click
        Try
            Dim qry As String = "select Location_Code as [Code], Location_Desc as [Name] from TSPL_LOCATION_MASTER "
            txtMultMCC.arrValueMember = clsCommon.ShowMultipleSelectForm("MCCMulSelect", qry, "Code", "Name", txtMultMCC.arrValueMember, txtMultMCC.arrDispalyMember)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtMultItem__My_Click(sender As Object, e As EventArgs) Handles txtMultItem._My_Click
        Try
            Dim qry As String = "select Item_Code as [Code], Item_Desc as [Name] from TSPL_ITEM_MASTER Where TSPL_Item_MASTER.Item_Used_as='S' Order By Sku_Seq"
            txtMultItem.arrValueMember = clsCommon.ShowMultipleSelectForm("ItemMulSelect", qry, "Code", "Name", txtMultItem.arrValueMember, txtMultItem.arrDispalyMember)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class