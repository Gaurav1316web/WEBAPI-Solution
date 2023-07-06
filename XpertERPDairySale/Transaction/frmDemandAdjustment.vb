Imports common
Imports System.IO
Public Class frmDemandAdjustment
    Inherits FrmMainTranScreen
    Dim gv2 As New RadGridView()
    Private Sub btnSearch_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub rdbnBoth_ToggleStateChanged(sender As Object, args As StateChangedEventArgs)

    End Sub

    Private Sub rbtn_Ambient_ToggleStateChanged(sender As Object, args As StateChangedEventArgs)

    End Sub

    Private Sub rbtn_Fresh_ToggleStateChanged(sender As Object, args As StateChangedEventArgs)

    End Sub

    Private Sub chkIndividualCustomer_ToggleStateChanged(sender As Object, args As StateChangedEventArgs)

    End Sub

    Private Sub txtCustomerNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean)

    End Sub

    Private Sub txtVehicleNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean)

    End Sub

    Private Sub btnUpdateCrateAndAmt_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub TxtCity__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean)

    End Sub

    Private Sub txtRouteNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean)

    End Sub

    Private Sub rbtnMorningEveningBoth_ToggleStateChanged(sender As Object, args As StateChangedEventArgs)

    End Sub

    Private Sub rbtnEvening_ToggleStateChanged(sender As Object, args As StateChangedEventArgs)

    End Sub

    Private Sub rbtnMorning_ToggleStateChanged(sender As Object, args As StateChangedEventArgs)

    End Sub

    Private Sub txtLocation__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean)

    End Sub

    Private Sub btnAddNew_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub txtDocNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean)

    End Sub

    Private Sub txtDocNo__MYNavigator(sender As Object, e As EventArgs, NavType As NavigatorType)

    End Sub

    Private Sub rmi_TS_PDF_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub rmi_TS_Excel_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Btn_TSCancel_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Btn_GPCancel_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Btn_Gatepass_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Btn_TruckSheet_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub btnAssessment_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub btnPost_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub btnreverse_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub btnSaveLayout_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub btnLayout_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub SplitContainer2_Panel1_Paint(sender As Object, e As PaintEventArgs) Handles SplitContainer2.Panel1.Paint

    End Sub

    Private Sub txtRoute__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtRoute._MYValidating
        Try
            Dim qry As String = "Select DISTINCT TSPL_CUSTOMER_MASTER.Route_No as Code,TSPL_CUSTOMER_MASTER.Route_Desc as Description from TSPL_CUSTOMER_MASTER"
            Dim whrcls As String = "Route_No is not null and TSPL_CUSTOMER_MASTER.Area_Code='" + clsCommon.myCstr(TxtArea.Value) + "'"
            txtRoute.Value = clsCommon.ShowSelectForm("DSRouteFinder", qry, "Code", whrcls, txtRoute.Value, "", isButtonClicked)
            lblRouteDesc.Text = clsCommon.myCstr(clsRouteMaster.GetName(txtRoute.Value, Nothing))

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub frmDemandAdjustment_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        txtDate.Value = clsCommon.GETSERVERDATE()
        rbtnMorning.IsChecked = True
        rbtnQty.IsChecked = True
    End Sub

    Sub Reset()
        txtZone.Value = ""
        TxtArea.Enabled=False
        TxtArea.Value = ""
        txtRoute.Value = ""
        txtRoute.Enabled = False
        lblZoneDesc.Text = ""
        lblAreaDesc.Text = ""
        lblRouteDesc.Text = ""
        rbtnMorning.IsChecked = True
        rbtnQty.IsChecked = True
        txtDate.Value = clsCommon.GETSERVERDATE()

        GV1.Rows.Clear()
        GV1.Columns.Clear()
        GV1.DataSource = Nothing

    End Sub

    Private Sub btnClose_Click_1(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Reset()
    End Sub

    Private Sub txtZone__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtZone._MYValidating
        Try
            Dim qry As String = "Select DISTINCT TSPL_CUSTOMER_MASTER.Zone_Code as Code,TSPL_ZONE_MASTER.Description as Description from TSPL_CUSTOMER_MASTER left join TSPL_ZONE_MASTER on TSPL_ZONE_MASTER.Zone_Code=TSPL_CUSTOMER_MASTER.Zone_Code"
            Dim whrcls As String = "TSPL_CUSTOMER_MASTER.Zone_Code is not null and  TSPL_CUSTOMER_MASTER.Zone_Code<>''"
            txtZone.Value = clsCommon.ShowSelectForm("DSRouteFinder", qry, "Code", whrcls, txtZone.Value, "", isButtonClicked)
            lblZoneDesc.Text = clsCommon.myCstr(ClsZoneMaster.GetName(txtZone.Value))
            TxtArea.Enabled = True

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub TxtArea__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles TxtArea._MYValidating
        Try
            Dim qry As String = "Select DISTINCT TSPL_CUSTOMER_MASTER.Area_Code as Code,TSPL_AREA_MASTER.Name as Description from TSPL_CUSTOMER_MASTER left join TSPL_AREA_MASTER on TSPL_AREA_MASTER.Code=TSPL_CUSTOMER_MASTER.Area_Code"
            Dim whrcls As String = "TSPL_CUSTOMER_MASTER.Area_Code is not null and TSPL_CUSTOMER_MASTER.Zone_Code='" + clsCommon.myCstr(txtZone.Value) + "'"
            TxtArea.Value = clsCommon.ShowSelectForm("DSRouteFinder", qry, "Code", whrcls, TxtArea.Value, "", isButtonClicked)
            lblAreaDesc.Text = clsCommon.myCstr(clsAreaMaster.GetName(TxtArea.Value))
            txtRoute.Enabled = True

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
        If rbtnQty.IsChecked Then
            GV1.Columns("Adj_Qty").HeaderText = "Adjust By Qty"
            GV1.Columns("Adj_Qty").IsVisible = True

        ElseIf rbtnPre.IsChecked Then
            GV1.Columns("Adj_Per").HeaderText = "Adjust By %"
            GV1.Columns("Adj_Per").IsVisible = True
        End If
        Dim summaryRowItemB As New GridViewSummaryRowItem()
        Dim TotalQty As New GridViewSummaryItem("Qty", "{0:n2}", GridAggregateFunction.Sum)
        summaryRowItemB.Add(TotalQty)
        GV1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItemB)



        GV1.AutoSizeRows = True
        GV1.BestFitColumns()
        GV1.MasterTemplate.AutoExpandGroups = True

    End Sub

    Private Sub btnGo_Click_1(sender As Object, e As EventArgs) Handles btnGo.Click
        Try
            Dim dt As New DataTable()
            Dim strQry As String = "select ROW_NUMBER() Over (Order by TSPL_DEMAND_BOOKING_DETAIL.Cust_Code) As Sl_No,TSPL_DEMAND_BOOKING_DETAIL.Cust_Code,TSPL_DEMAND_BOOKING_DETAIL.Item_Code,TSPL_ITEM_MASTER.Alies_Name as Item_Name,TSPL_DEMAND_BOOKING_DETAIL.ShiftType, TSPL_DEMAND_BOOKING_DETAIL.Qty as DQty, TSPL_DEMAND_BOOKING_DETAIL.Qty,TSPL_DEMAND_BOOKING_DETAIL.Item_Rate"
            If rbtnQty.IsChecked Then
                strQry += ",0 as Adj_Qty"
            ElseIf rbtnPre.IsChecked Then
                strQry += ",0 as Adj_Per"
            End If
            strQry += " From TSPL_DEMAND_BOOKING_MASTER
Left Join TSPL_DEMAND_BOOKING_DETAIL on TSPL_DEMAND_BOOKING_DETAIL.Document_No=TSPL_DEMAND_BOOKING_MASTER.Document_No
Left Join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_DEMAND_BOOKING_DETAIL.Item_Code
where TSPL_DEMAND_BOOKING_MASTER.Created_Date ='" + clsCommon.GetPrintDate(txtDate.Value) + "' and TSPL_DEMAND_BOOKING_MASTER.Route_No='" + clsCommon.myCstr(txtRoute.Value) + "'"
            If rbtnMorning.IsChecked Then
                strQry += " and TSPL_DEMAND_BOOKING_DETAIL.ShiftType ='Morning'"
            ElseIf rbtnEvening.IsChecked Then
                strQry += " and TSPL_DEMAND_BOOKING_DETAIL.ShiftType ='Evening'"
            End If

            dt = clsDBFuncationality.GetDataTable(strQry)
            GV1.MasterTemplate.SummaryRowsBottom.Clear()
            GV1.DataSource = Nothing
            GV1.Rows.Clear()
            GV1.Columns.Clear()
            GV1.GroupDescriptors.Clear()
            GV1.MasterTemplate.SummaryRowsBottom.Clear()
            GV1.MasterView.Refresh()


            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Demand Not Found", Me.Text)
                Exit Sub
            Else
                GV1.DataSource = dt

                SetGridFormat()

            End If


        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub GV1_CellValueChanged(sender As Object, e As GridViewCellEventArgs) Handles GV1.CellValueChanged

    End Sub

    Private Sub GV1_CellEndEdit(sender As Object, e As GridViewCellEventArgs) Handles GV1.CellEndEdit
        Try
            Dim dis As Double = 0
            Dim qty As Double = 0
            Dim temp As Double = 0
            If rbtnQty.IsChecked Then
                dis = GV1.CurrentCell.Value()
                If dis >= 0 Then
                    qty = GV1.CurrentRow.Cells(5).Value
                    temp = qty - dis
                    If temp >= 0 Then
                        GV1.CurrentRow.Cells(6).Value = temp
                    Else
                        GV1.CurrentCell.Value = 0
                        Throw New Exception(" Adjust Qty is greater then Total Qty")
                    End If
                Else
                    GV1.CurrentCell.Value = 0
                    Throw New Exception("Invalid Adjust Qty!")
                End If



            ElseIf rbtnPre.IsChecked Then
                dis = GV1.CurrentCell.Value()
                If dis >= 0 Then
                    qty = GV1.CurrentRow.Cells(5).Value()

                    temp = qty - ((qty * dis) / 100)

                    If temp >= 0 Then
                        GV1.CurrentRow.Cells(6).Value = temp
                    Else
                        GV1.CurrentCell.Value = 0
                        Throw New Exception("Invalid Adjust Percentage!")
                    End If
                Else
                    GV1.CurrentCell.Value = 0
                    Throw New Exception("Invalid Adjust Percentage!")
                End If


            End If


        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)

        End Try
    End Sub
End Class
