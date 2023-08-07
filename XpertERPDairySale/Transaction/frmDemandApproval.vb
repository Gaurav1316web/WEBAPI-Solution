Imports common

Public Class frmDemandApproval
    Private Sub SplitContainer2_Panel1_Paint(sender As Object, e As PaintEventArgs) Handles SplitContainer2.Panel1.Paint

    End Sub

    Private Sub txtRoute__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtRoute._MYValidating
        Try
            Dim qry As String = "Select DISTINCT TSPL_CUSTOMER_MASTER.Route_No as Code,TSPL_CUSTOMER_MASTER.Route_Desc as Description from TSPL_CUSTOMER_MASTER"
            Dim whrcls As String = "" ' "Route_No is not null and TSPL_CUSTOMER_MASTER.Area_Code='" + clsCommon.myCstr() + "'"
            txtRoute.Value = clsCommon.ShowSelectForm("DSRouteFinder", qry, "Code", whrcls, txtRoute.Value, "", isButtonClicked)
            lblRouteDesc.Text = clsCommon.myCstr(clsRouteMaster.GetName(txtRoute.Value, Nothing))

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub frmDemandApproval_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtDate.Value = clsCommon.GETSERVERDATE()
        rbtnMorning.IsChecked = True

    End Sub
    Sub Reset()
        txtZone.Value = ""
        txtRoute.Value = ""
        txtRoute.Enabled = True
        lblZoneDesc.Text = ""
        lblRouteDesc.Text = ""
        rbtnMorning.IsChecked = True
        txtDate.Value = clsCommon.GETSERVERDATE()
        GV1.Rows.Clear()
        GV1.Columns.Clear()
        GV1.DataSource = Nothing
    End Sub

    Private Sub txtZone__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtZone._MYValidating
        Try
            Dim qry As String = "Select DISTINCT TSPL_CUSTOMER_MASTER.Zone_Code as Code,TSPL_ZONE_MASTER.Description as Description from TSPL_CUSTOMER_MASTER left join TSPL_ZONE_MASTER on TSPL_ZONE_MASTER.Zone_Code=TSPL_CUSTOMER_MASTER.Zone_Code"
            Dim whrcls As String = "TSPL_CUSTOMER_MASTER.Zone_Code is not null and  TSPL_CUSTOMER_MASTER.Zone_Code<>''"
            txtZone.Value = clsCommon.ShowSelectForm("DSRouteFinder", qry, "Code", whrcls, txtZone.Value, "", isButtonClicked)
            lblZoneDesc.Text = clsCommon.myCstr(ClsZoneMaster.GetName(txtZone.Value))
            'TxtArea.Enabled = True

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub SetGridFormat()
        GV1.ShowGroupPanel = False
        GV1.ShowRowHeaderColumn = False
        GV1.AllowAddNewRow = False
        GV1.AllowDeleteRow = False
        GV1.EnableFiltering = True
        GV1.ShowFilteringRow = True
        GV1.MasterTemplate.SummaryRowsBottom.Clear()
        For ii As Integer = 0 To GV1.Columns.Count - 2
            GV1.Columns(ii).ReadOnly = True
            GV1.Columns(ii).BestFit()
        Next
        GV1.Columns("Sl_No").HeaderText = "S.N"
        GV1.Columns("Sl_No").IsVisible = True

        GV1.Columns("Cust_Code").HeaderText = "Cust Code"
        GV1.Columns("Cust_Code").IsVisible = True
        GV1.Columns("Item_Code").HeaderText = "Item Code"
        GV1.Columns("Item_Code").IsVisible = True
        GV1.Columns("Item_Name").HeaderText = "Item Name"
        GV1.Columns("Item_Name").IsVisible = True
        GV1.Columns("ShiftType").HeaderText = "Shift "
        GV1.Columns("ShiftType").IsVisible = True
        GV1.Columns("DQty").HeaderText = "Demand Qty"
        GV1.Columns("DQty").IsVisible = False
        GV1.Columns("Qty").HeaderText = "Total Qty"
        GV1.Columns("Qty").IsVisible = True
        GV1.Columns("Qty").FormatString = "{0:n2}"
        GV1.Columns("Item_Rate").HeaderText = "Item Rate"
        GV1.Columns("Item_Rate").IsVisible = True
        GV1.Columns("Item_Rate").FormatString = "{0:n2}"

        Dim summaryRowItemB As New GridViewSummaryRowItem()
        Dim TotalQty As New GridViewSummaryItem("Qty", "{0:n2}", GridAggregateFunction.Sum)
        summaryRowItemB.Add(TotalQty)
        GV1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItemB)



        GV1.AutoSizeRows = True
        GV1.BestFitColumns()
        GV1.MasterTemplate.AutoExpandGroups = True

    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click

    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Try
            '            Dim dt As New DataTable()
            '            Dim strQry As String = "select ROW_NUMBER() Over (Order by TSPL_DEMAND_BOOKING_DETAIL.Cust_Code) As Sl_No,TSPL_DEMAND_BOOKING_DETAIL.Cust_Code,TSPL_DEMAND_BOOKING_DETAIL.Item_Code,TSPL_ITEM_MASTER.Alies_Name as Item_Name,TSPL_DEMAND_BOOKING_DETAIL.ShiftType, TSPL_DEMAND_BOOKING_DETAIL.Qty as DQty, TSPL_DEMAND_BOOKING_DETAIL.Qty,TSPL_DEMAND_BOOKING_DETAIL.Item_Rate"
            '            If rbtnQty.IsChecked Then
            '                strQry += ",0 as Adj_Qty"
            '            ElseIf rbtnPre.IsChecked Then
            '                strQry += ",0 as Adj_Per"
            '            End If
            '            strQry += " From TSPL_DEMAND_BOOKING_MASTER
            'Left Join TSPL_DEMAND_BOOKING_DETAIL on TSPL_DEMAND_BOOKING_DETAIL.Document_No=TSPL_DEMAND_BOOKING_MASTER.Document_No
            'Left Join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_DEMAND_BOOKING_DETAIL.Item_Code
            'where TSPL_DEMAND_BOOKING_MASTER.Created_Date ='" + clsCommon.GetPrintDate(txtDate.Value) + "' and TSPL_DEMAND_BOOKING_MASTER.Route_No='" + clsCommon.myCstr(txtRoute.Value) + "'"
            '            If rbtnMorning.IsChecked Then
            '                strQry += " and TSPL_DEMAND_BOOKING_DETAIL.ShiftType ='Morning'"
            '            ElseIf rbtnEvening.IsChecked Then
            '                strQry += " and TSPL_DEMAND_BOOKING_DETAIL.ShiftType ='Evening'"
            '            End If

            '            dt = clsDBFuncationality.GetDataTable(strQry)
            '            GV1.MasterTemplate.SummaryRowsBottom.Clear()
            '            GV1.DataSource = Nothing
            '            GV1.Rows.Clear()
            '            GV1.Columns.Clear()
            '            GV1.GroupDescriptors.Clear()
            '            GV1.MasterTemplate.SummaryRowsBottom.Clear()
            '            GV1.MasterView.Refresh()


            '            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
            '                clsCommon.MyMessageBoxShow(Me, "Demand Not Found", Me.Text)
            '                Exit Sub
            '            Else
            '                GV1.DataSource = dt

            '                SetGridFormat()

            '            End If


        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Reset()
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click

    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
End Class