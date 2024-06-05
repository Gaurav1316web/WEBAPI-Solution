Imports common
Imports System.IO
Public Class frmDemandAdjustment
    Inherits FrmMainTranScreen
#Region "Variables"
    Dim isNewEntry As Boolean = False
    Dim LimitIncreaseDecreseQty As Double = 0
    Dim LimitIncreaseDecresePer As Double = 0
    Private isInsideLoadData As Boolean = False
    Private isCellValueChangedOpen As Boolean = False

    Const ColSNo As String = "ColSNo"
    Const colZoneCode As String = "colZoneCode"
    Const colRouteCode As String = "colRouteCode"
    Const colBoothCode As String = "colBoothCode"
    Const colBoothName As String = "colBoothName"
    Const colItemcode As String = "colItemcode"
    Const colTRCode As String = "colTRCode"
    Const colItemDesc As String = "colItemDesc"
    Const colUOM As String = "colUOM"
    Const colDemandQty As String = "colDemandQty"
    Const colAdjustedQty As String = "colAdjustedQty"
    Const colFinalQty As String = "colFinalQty"

#End Region
    Private Sub frmDemandAdjustment_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CreateTable()
        LimitIncreaseDecreseQty = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.DemandIncreaseDecreaseQty, clsFixedParameterCode.DemandIncreaseDecreaseQty, Nothing))
        LimitIncreaseDecresePer = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.DemandIncreaseDecreasePer, clsFixedParameterCode.DemandIncreaseDecreasePer, Nothing))
        AddNew()
    End Sub
    Private Sub btnAddNew_Click(sender As Object, e As EventArgs) Handles btnAddNew.Click
        AddNew()
    End Sub
    Public Sub AddNew()
        txtDocNo.Value = ""
        txtDocNo.MyReadOnly = False
        txtDate.Value = clsCommon.GETSERVERDATE()
        txtDemandDate.Value = txtDate.Value
        txtDate.Enabled = True
        txtDemandDate.Enabled = True
        txtZoneCode.arrValueMember = Nothing
        txtRouteCode.arrValueMember = Nothing
        txtItemCode.Value = ""
        lblItemDesc.Text = ""
        txtUOM.Value = ""
        lblUOMDesc.Text = ""
        rbtnMorning.IsChecked = True
        chkChangeItem.Checked = False
        chkChangeItem.Enabled = True
        rbtnAutomatic.IsChecked = True
        txtQty.Text = 0
        txtMinCrate.Text = ""
        txtDeductQty.Text = 0
        txtAddQty.Text = 0
        txtChangeItemCode.Value = ""
        lblChangeitemDesc.Text = ""
        txtPer.Text = 0
        ControlGroup(True)
        LoadBlankGrid()
        EnableGroup(False)
        btnProceed.Enabled = False
        isNewEntry = True
    End Sub
    Sub LoadBlankGrid()
        gv1.Rows.Clear()
        gv1.Columns.Clear()
        Dim repoNumBox As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.FormatString = ""
        repoNumBox.HeaderText = "SNo"
        repoNumBox.Name = ColSNo
        repoNumBox.Width = 40
        repoNumBox.ReadOnly = True
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoNumBox)
        Dim repoZone As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoZone.FormatString = ""
        repoZone.HeaderText = "Zone"
        repoZone.Name = colZoneCode
        repoZone.IsVisible = True
        repoZone.ReadOnly = True
        repoZone.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoZone)
        Dim repoRoute As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoRoute.FormatString = ""
        repoRoute.HeaderText = "Route"
        repoRoute.Name = colRouteCode
        repoRoute.IsVisible = True
        repoRoute.ReadOnly = True
        repoRoute.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoRoute)
        Dim repoBoothCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoBoothCode.FormatString = ""
        repoBoothCode.HeaderText = "Booth Code"
        repoBoothCode.Name = colBoothCode
        repoBoothCode.Width = 150
        repoBoothCode.IsVisible = True
        repoBoothCode.ReadOnly = True
        repoBoothCode.TextImageRelation = TextImageRelation.TextBeforeImage
        gv1.MasterTemplate.Columns.Add(repoBoothCode)
        Dim repoBoothName = New GridViewTextBoxColumn()
        repoBoothName.FormatString = ""
        repoBoothName.HeaderText = "Booth Name"
        repoBoothName.Name = colBoothName
        repoBoothName.Width = 200
        repoBoothName.IsVisible = True
        repoBoothName.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoBoothName)
        Dim repoItemCode = New GridViewTextBoxColumn()
        repoItemCode.FormatString = ""
        repoItemCode.HeaderText = "Item Code"
        repoItemCode.Name = colItemcode
        repoItemCode.Width = 120
        repoItemCode.IsVisible = False
        repoItemCode.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoItemCode)
        Dim repoTRCode = New GridViewTextBoxColumn()
        repoTRCode.FormatString = ""
        repoTRCode.HeaderText = "Item TR Code"
        repoTRCode.Name = colTRCode
        repoTRCode.Width = 120
        repoTRCode.IsVisible = False
        repoTRCode.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoTRCode)
        Dim repoItemDesc = New GridViewTextBoxColumn()
        repoItemDesc.FormatString = ""
        repoItemDesc.HeaderText = "Item Short Desc"
        repoItemDesc.Name = colItemDesc
        repoItemDesc.Width = 120
        repoItemDesc.IsVisible = True
        repoItemDesc.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoItemDesc)
        Dim repoItemUOM = New GridViewTextBoxColumn()
        repoItemUOM.FormatString = ""
        repoItemUOM.HeaderText = "UOM"
        repoItemUOM.Name = colUOM
        repoItemUOM.Width = 120
        repoItemUOM.IsVisible = True
        repoItemUOM.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoItemUOM)
        Dim repoDemandQty = New GridViewDecimalColumn()
        repoDemandQty.FormatString = ""
        repoDemandQty.HeaderText = "Demand Qty"
        repoDemandQty.Name = colDemandQty
        repoDemandQty.Width = 120
        repoDemandQty.Minimum = 0
        repoDemandQty.ShowUpDownButtons = False
        repoDemandQty.Step = 0
        repoDemandQty.DecimalPlaces = 4
        repoDemandQty.IsVisible = True
        repoDemandQty.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoDemandQty)
        Dim repoAdjustedQty = New GridViewDecimalColumn()
        repoAdjustedQty.FormatString = ""
        repoAdjustedQty.HeaderText = "Adjusted Qty"
        repoAdjustedQty.Name = colAdjustedQty
        repoAdjustedQty.Width = 120
        repoAdjustedQty.Minimum = 0
        repoAdjustedQty.ShowUpDownButtons = False
        repoAdjustedQty.Step = 0
        repoAdjustedQty.DecimalPlaces = 4
        repoAdjustedQty.IsVisible = True
        gv1.MasterTemplate.Columns.Add(repoAdjustedQty)
        Dim repoFinalQty = New GridViewDecimalColumn()
        repoFinalQty.FormatString = ""
        repoFinalQty.HeaderText = "Final Order"
        repoFinalQty.Name = colFinalQty
        repoFinalQty.Width = 120
        repoFinalQty.Minimum = 0
        repoFinalQty.ShowUpDownButtons = False
        repoFinalQty.Step = 0
        repoFinalQty.DecimalPlaces = 4
        repoFinalQty.IsVisible = True
        gv1.MasterTemplate.Columns.Add(repoFinalQty)
        gv1.Enabled = True
        gv1.AllowAddNewRow = False
        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = False
        gv1.AllowRowReorder = False
        gv1.EnableSorting = False
        gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        gv1.TableElement.TableHeaderHeight = 40
        gv1.AllowDeleteRow = True
        gv1.Rows.AddNew()
        gv1.BestFitColumns()
    End Sub
    Private Sub txtQty_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtAddQty.KeyPress, txtDeductQty.KeyPress, txtAddQty.KeyPress, txtMinCrate.KeyPress, txtPer.KeyPress, txtQty.KeyPress
        If Not Char.IsControl(e.KeyChar) AndAlso Not Char.IsDigit(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub
    Private Sub chkChangeItem_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkChangeItem.ToggleStateChanged
        If chkChangeItem.Checked Then
            rgbChangeItem.Location = New System.Drawing.Point(2, 121)
            ControlGroup(False)
        Else
            ControlGroup(True)
        End If
    End Sub
    Public Sub ControlGroup(ByVal flag As Boolean)
        rgbChangeItem.Visible = Not flag
        rgbMode.Visible = flag
        rgbIDOrder.Visible = flag
        rgbDecreaseOrder.Visible = flag
        rgbIncreaseOrder.Visible = flag
        If flag Then
            ControlIsnotChangeItem(flag)
        End If
    End Sub
    Public Sub EnableGroup(ByVal flag As Boolean)
        rgbChangeItem.Enabled = flag
        rgbMode.Enabled = flag
        rgbIDOrder.Enabled = flag
        rgbDecreaseOrder.Enabled = flag
        rgbIncreaseOrder.Enabled = flag
        btnSave.Enabled = flag
        btnDelete.Enabled = flag
        btnGo.Enabled = Not flag
        rgbShiftType.Enabled = Not flag
        txtZoneCode.Enabled = Not flag
        txtRouteCode.Enabled = Not flag
        txtItemCode.Enabled = Not flag
        txtUOM.Enabled = Not flag
        txtUOM.Enabled = Not flag
        txtMinCrate.Enabled = Not flag
        txtDemandDate.Enabled = Not flag
        txtDate.Enabled = Not flag


    End Sub
    Public Sub ControlIsnotChangeItem(ByVal flag As Boolean)
        rgbIDOrder.Visible = flag
        rgbDecreaseOrder.Visible = flag
        rgbIncreaseOrder.Visible = flag
    End Sub
    Private Sub rbtnAutomatic_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles rbtnAutomatic.ToggleStateChanged
        ControlIsnotChangeItem(True)
        If rbtnAutomatic.IsChecked Then
            lblPer.Visible = True
            txtPer.Visible = True
            txtQty.Text = 0
            txtPer.Text = 0
        Else
            lblPer.Visible = False
            txtPer.Visible = False
            txtQty.Text = 0
            txtPer.Text = 0
        End If
    End Sub
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
    Private Sub RadPageViewPage1_Paint(sender As Object, e As PaintEventArgs) Handles RadPageViewPage1.Paint
    End Sub
    Private Sub txtZoneCode__My_Click(sender As Object, e As EventArgs) Handles txtZoneCode._My_Click
        Try
            Dim qry As String = "select distinct TSPL_ROUTE_MASTER.Zone_code as Code,TSPL_ZONE_MASTER.Description  from TSPL_ROUTE_MASTER left join TSPL_ZONE_MASTER on TSPL_ROUTE_MASTER.Zone_code=TSPL_ZONE_MASTER.Zone_Code where TSPL_ROUTE_MASTER.Zone_code is not null "
            txtZoneCode.arrValueMember = clsCommon.ShowMultipleSelectForm("ZoneCodesearch", qry, "Code", "Code", txtZoneCode.arrValueMember, txtZoneCode.arrDispalyMember)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub txtRouteCode__My_Click(sender As Object, e As EventArgs) Handles txtRouteCode._My_Click
        Dim qry As String = String.Empty
        Try
            If txtZoneCode.arrValueMember IsNot Nothing Then
                qry = "select Route_No as Code,Route_Desc as Name from TSPL_ROUTE_MASTER where Zone_Code in(" + clsCommon.GetMulcallStringWithComma(txtZoneCode.arrValueMember) + ")"
            Else
                qry = "select Route_No as Code,Route_Desc as Name from TSPL_ROUTE_MASTER where Zone_Code is not null "
            End If
            txtRouteCode.arrValueMember = clsCommon.ShowMultipleSelectForm("fndZoneCode", qry, "Code", "Code", txtRouteCode.arrValueMember, txtRouteCode.arrDispalyMember)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub txtItemCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtItemCode._MYValidating
        Try
            Dim qry As String = " select Item_code as Code,Item_Desc,Short_Description  from TSPL_ITEM_MASTER "
            Dim whrcls As String = " Item_Type='F'"
            txtItemCode.Value = clsCommon.ShowSelectForm("fndItemscode", qry, "Code", whrcls, txtItemCode.Value, "Code", isButtonClicked)
            lblItemDesc.Text = clsDBFuncationality.getSingleValue("select Short_Description  from TSPL_ITEM_MASTER where Item_Code='" + txtItemCode.Value + "'")
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub txtUOM__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtUOM._MYValidating
        Try
            If clsCommon.myLen(txtItemCode.Value) > 0 Then
                Dim qry As String = "select UOM_Code as Code from TSPL_ITEM_UOM_DETAIL"
                txtUOM.Value = clsCommon.ShowSelectForm("fndItemUom", qry, "Code", " Item_Code='" + txtItemCode.Value + "'", txtUOM.Value, "Code", isButtonClicked)
                lblUOMDesc.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select UOM_Description from TSPL_ITEM_UOM_DETAIL where UOM_Code='" + txtUOM.Value + "' and Item_Code='" + txtItemCode.Value + "'"))
            Else
                Throw New Exception("Please Select Item Code")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Try
            Dim totalDQty As Double = 0
            Dim totalAQty As Double = 0
            Dim totalFQty As Double = 0
            If clsCommon.myLen(txtItemCode.Value) > 0 AndAlso clsCommon.myLen(txtUOM.Value) > 0 AndAlso clsCommon.myLen(txtMinCrate.Text) > 0 Then
                LoadBlankGrid()
                Dim whrcls As String = ""
                Dim qry As String = "select TSPL_ROUTE_MASTER.Zone_Code,TSPL_DEMAND_BOOKING_MASTER.Route_No,TSPL_DEMAND_BOOKING_DETAIL.Cust_Code,TSPL_CUSTOMER_MASTER.Customer_Name,TSPL_DEMAND_BOOKING_DETAIL.Item_Code,TSPL_DEMAND_BOOKING_DETAIL.TR_Code,TSPL_ITEM_MASTER.Short_Description,TSPL_DEMAND_BOOKING_DETAIL.Unit_code,TSPL_DEMAND_BOOKING_DETAIL.Qty
from TSPL_DEMAND_BOOKING_MASTER 
left join TSPL_DEMAND_BOOKING_DETAIL on TSPL_DEMAND_BOOKING_MASTER.Document_No=TSPL_DEMAND_BOOKING_DETAIL.Document_No
left join TSPL_CUSTOMER_MASTER on TSPL_DEMAND_BOOKING_DETAIL.Cust_Code=TSPL_CUSTOMER_MASTER.Cust_Code
left join TSPL_ROUTE_MASTER on TSPL_DEMAND_BOOKING_MASTER.Route_No=TSPL_ROUTE_MASTER.Route_No
left join TSPL_ITEM_MASTER on TSPL_DEMAND_BOOKING_DETAIL.Item_Code=TSPL_ITEM_MASTER.Item_Code
where TSPL_DEMAND_BOOKING_MASTER.Posted=0 and convert(date,TSPL_DEMAND_BOOKING_MASTER.Document_Date,103)='" + clsCommon.GetPrintDate(txtDemandDate.Value) + "' and TSPL_DEMAND_BOOKING_DETAIL.Item_Code='" + txtItemCode.Value + "' and TSPL_DEMAND_BOOKING_DETAIL.Unit_code='" + txtUOM.Value + "' and TSPL_DEMAND_BOOKING_DETAIL.Qty>='" + txtMinCrate.Text + "' "
                If txtZoneCode.arrValueMember IsNot Nothing Then
                    If txtRouteCode.arrValueMember IsNot Nothing Then
                        whrcls += " and TSPL_DEMAND_BOOKING_MASTER.Route_No in('" + clsCommon.GetMulcallStringWithComma(txtRouteCode.arrValueMember) + "')"
                    Else
                        whrcls += " and TSPL_DEMAND_BOOKING_MASTER.Route_No in(select Route_No from TSPL_ROUTE_MASTER where Zone_Code in(" + clsCommon.GetMulcallStringWithComma(txtZoneCode.arrValueMember) + ") and Zone_Code is not null)"
                    End If
                Else
                    If txtRouteCode.arrValueMember IsNot Nothing Then
                        whrcls += " and TSPL_DEMAND_BOOKING_MASTER.Route_No in('" + clsCommon.GetMulcallStringWithComma(txtRouteCode.arrValueMember) + "')"
                    End If
                End If
                If rbtnMorning.IsChecked Then
                    whrcls += " and TSPL_DEMAND_BOOKING_DETAIL.ShiftType='Morning' "
                Else
                    whrcls += " and TSPL_DEMAND_BOOKING_DETAIL.ShiftType='Evening' "
                End If
                qry += whrcls
                qry += " order by TSPL_DEMAND_BOOKING_DETAIL.Qty desc "
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    Dim introw As Integer = 0
                    For Each dr As DataRow In dt.Rows
                        gv1.Rows(introw).Cells(ColSNo).Value = introw + 1
                        gv1.Rows(introw).Cells(colZoneCode).Value = clsCommon.myCstr(dr("Zone_Code"))
                        gv1.Rows(introw).Cells(colRouteCode).Value = clsCommon.myCstr(dr("Route_No"))
                        gv1.Rows(introw).Cells(colBoothCode).Value = clsCommon.myCstr(dr("Cust_Code"))
                        gv1.Rows(introw).Cells(colBoothName).Value = clsCommon.myCstr(dr("Customer_Name"))
                        gv1.Rows(introw).Cells(colItemcode).Value = clsCommon.myCstr(dr("Item_Code"))
                        gv1.Rows(introw).Cells(colTRCode).Value = clsCommon.myCstr(dr("TR_Code"))
                        gv1.Rows(introw).Cells(colItemDesc).Value = clsCommon.myCstr(dr("Short_Description"))
                        gv1.Rows(introw).Cells(colUOM).Value = clsCommon.myCstr(dr("Unit_code"))
                        gv1.Rows(introw).Cells(colDemandQty).Value = clsCommon.myCdbl(dr("Qty"))
                        gv1.Rows(introw).Cells(colAdjustedQty).Value = 0
                        gv1.Rows(introw).Cells(colFinalQty).Value = clsCommon.myCdbl(gv1.Rows(introw).Cells(colDemandQty).Value) - clsCommon.myCdbl(gv1.Rows(introw).Cells(colAdjustedQty).Value)
                        totalDQty += clsCommon.myCdbl(gv1.Rows(introw).Cells(colDemandQty).Value)
                        totalAQty += clsCommon.myCdbl(gv1.Rows(introw).Cells(colAdjustedQty).Value)
                        totalFQty += clsCommon.myCdbl(gv1.Rows(introw).Cells(colFinalQty).Value)
                        gv1.Rows.AddNew()
                        introw += 1
                    Next
                    gv1.Rows(introw).Cells(colUOM).Value = "Total"
                    gv1.Rows(introw).Cells(colDemandQty).Value = totalDQty
                    gv1.Rows(introw).Cells(colAdjustedQty).Value = totalAQty
                    gv1.Rows(introw).Cells(colFinalQty).Value = totalFQty
                    EnableGroup(True)
                    RadPageView1.SelectedPage = RadPageViewPage2
                Else
                    Throw New Exception("Data Not Found!")
                End If
            Else
                Throw New Exception(" Invalid Item Code/UOM/Minimum Crate Qty!")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub btnIncrease1_Click(sender As Object, e As EventArgs) Handles btnIncrease1.Click
        Dim dbltotalqty As Integer = 0
        Dim dblperqty As Integer = 0
        Dim dblbalqty As Integer = 0
        Dim dblorderqty As Integer = 0
        Dim totalDQty As Double = 0
        Dim totalAQty As Double = 0
        Dim totalFQty As Double = 0
        Try
            isInsideLoadData = True

            For Each grow As GridViewRowInfo In gv1.Rows
                grow.Cells(colAdjustedQty).Value = 0
                grow.Cells(colFinalQty).Value = clsCommon.myCdbl(grow.Cells(colDemandQty).Value) + 0
            Next
            If rbtnAutomatic.IsChecked Then
                If clsCommon.myCdbl(txtPer.Text) <= LimitIncreaseDecresePer Then
                    For Each grow As GridViewRowInfo In gv1.Rows
                        If clsCommon.myCdbl(txtQty.Text) > 0 Then
                            If clsCommon.myLen(grow.Cells(colRouteCode).Value) > 0 Then
                                dblperqty = clsCommon.myCdbl(grow.Cells(colDemandQty).Value) * (clsCommon.myCdbl(txtPer.Text) / 100)
                                dblorderqty = clsCommon.myCdbl(grow.Cells(colDemandQty).Value) + dblperqty
                                If dbltotalqty = 0 Then
                                    If clsCommon.myCdbl(txtQty.Text) < dblperqty Then
                                        dblbalqty = clsCommon.myCdbl(txtQty.Text)
                                        If dblbalqty > 0 Then
                                            grow.Cells(colAdjustedQty).Value = dblbalqty
                                            grow.Cells(colFinalQty).Value = clsCommon.myCdbl(grow.Cells(colDemandQty).Value) + dblbalqty
                                        End If
                                        Exit For
                                    End If
                                End If
                                If (clsCommon.myCdbl(txtQty.Text)) <= dbltotalqty Then
                                    dblbalqty = dbltotalqty - clsCommon.myCdbl(txtQty.Text)
                                    If dblbalqty > 0 Then
                                        grow.Cells(colAdjustedQty).Value = dblbalqty
                                        grow.Cells(colFinalQty).Value = clsCommon.myCdbl(grow.Cells(colDemandQty).Value) + dblbalqty
                                    End If
                                    Exit For
                                Else
                                    If (clsCommon.myCdbl(txtQty.Text)) <= (dbltotalqty + dblperqty) Then
                                        dblbalqty = clsCommon.myCdbl(txtQty.Text) - dbltotalqty
                                        If dblbalqty > 0 Then
                                            grow.Cells(colAdjustedQty).Value = dblbalqty
                                            grow.Cells(colFinalQty).Value = clsCommon.myCdbl(grow.Cells(colDemandQty).Value) + dblbalqty
                                        End If
                                        Exit For
                                    End If
                                    grow.Cells(colAdjustedQty).Value = dblperqty
                                    grow.Cells(colFinalQty).Value = dblorderqty
                                    dbltotalqty += dblperqty

                                End If
                            End If
                        End If
                    Next
                    RadPageView1.SelectedPage = RadPageViewPage2
                Else
                    Throw New Exception(" Increase/Decrease percentage not more then Limit ( " + clsCommon.myCstr(LimitIncreaseDecresePer) + " )")
                    End If
                ElseIf rbtnFix.IsChecked Then
                If clsCommon.myCdbl(txtQty.Text) <= LimitIncreaseDecreseQty Then
                    For Each grow As GridViewRowInfo In gv1.Rows
                        If clsCommon.myCdbl(txtQty.Text) > 0 Then
                            If clsCommon.myLen(grow.Cells(colRouteCode).Value) > 0 Then
                                dblorderqty = clsCommon.myCdbl(grow.Cells(colDemandQty).Value) + clsCommon.myCdbl(txtQty.Text)
                                grow.Cells(colAdjustedQty).Value = clsCommon.myCdbl(txtQty.Text)
                                grow.Cells(colFinalQty).Value = dblorderqty
                            End If
                        End If
                    Next
                    RadPageView1.SelectedPage = RadPageViewPage2
                Else
                    Throw New Exception(" Increase/Decrease Qty not more then Limit ( " + clsCommon.myCstr(LimitIncreaseDecreseQty) + " )")
                End If

            End If
                For Each grow As GridViewRowInfo In gv1.Rows
                    If clsCommon.myLen(grow.Cells(colRouteCode).Value) > 0 Then
                        totalDQty += clsCommon.myCdbl(grow.Cells(colDemandQty).Value)
                        totalAQty += clsCommon.myCdbl(grow.Cells(colAdjustedQty).Value)
                        totalFQty += clsCommon.myCdbl(grow.Cells(colFinalQty).Value)
                    Else
                        grow.Cells(colUOM).Value = "Total"
                        grow.Cells(colDemandQty).Value = totalDQty
                        grow.Cells(colAdjustedQty).Value = totalAQty
                        grow.Cells(colFinalQty).Value = totalFQty
                    End If
                Next

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isInsideLoadData = False

        End Try
    End Sub
    Private Sub btnDecrease1_Click(sender As Object, e As EventArgs) Handles btnDecrease1.Click
        Dim dbltotalqty As Integer = 0
        Dim dblperqty As Integer = 0
        Dim dblbalqty As Integer = 0
        Dim dblorderqty As Integer = 0
        Dim totalDQty As Double = 0
        Dim totalAQty As Double = 0
        Dim totalFQty As Double = 0
        Try
            isInsideLoadData = True


            For Each grow As GridViewRowInfo In gv1.Rows
                    grow.Cells(colAdjustedQty).Value = 0
                    grow.Cells(colFinalQty).Value = clsCommon.myCdbl(grow.Cells(colDemandQty).Value) + 0
                Next
            If rbtnAutomatic.IsChecked Then
                If clsCommon.myCdbl(txtPer.Text) <= LimitIncreaseDecresePer Then
                    For Each grow As GridViewRowInfo In gv1.Rows
                        If clsCommon.myCdbl(txtQty.Text) > 0 Then
                            If clsCommon.myLen(grow.Cells(colRouteCode).Value) > 0 Then
                                dblperqty = clsCommon.myCdbl(grow.Cells(colDemandQty).Value) * (clsCommon.myCdbl(txtPer.Text) / 100)
                                dblorderqty = clsCommon.myCdbl(grow.Cells(colDemandQty).Value) - dblperqty
                                If dbltotalqty = 0 Then
                                    If clsCommon.myCdbl(txtQty.Text) < dblperqty Then
                                        dblbalqty = clsCommon.myCdbl(txtQty.Text)
                                        If dblbalqty > 0 Then
                                            grow.Cells(colAdjustedQty).Value = dblbalqty
                                            grow.Cells(colFinalQty).Value = clsCommon.myCdbl(grow.Cells(colDemandQty).Value) - dblbalqty
                                        End If
                                        Exit For
                                    End If
                                End If
                                If (clsCommon.myCdbl(txtQty.Text)) <= dbltotalqty Then
                                    dblbalqty = dbltotalqty - clsCommon.myCdbl(txtQty.Text)
                                    If dblbalqty > 0 Then
                                        grow.Cells(colAdjustedQty).Value = dblbalqty
                                        grow.Cells(colFinalQty).Value = clsCommon.myCdbl(grow.Cells(colDemandQty).Value) - dblbalqty
                                    End If
                                    Exit For
                                Else
                                    dbltotalqty += dblperqty
                                    grow.Cells(colAdjustedQty).Value = dblperqty
                                    grow.Cells(colFinalQty).Value = dblorderqty
                                End If
                            End If
                        End If
                    Next
                    RadPageView1.SelectedPage = RadPageViewPage2
                Else
                    Throw New Exception(" Increase/Decrease percentage not more then Limit ( " + clsCommon.myCstr(LimitIncreaseDecresePer) + " )")
                End If
            ElseIf rbtnFix.IsChecked Then
                If clsCommon.myCdbl(txtQty.Text) <= LimitIncreaseDecreseQty Then
                    For Each grow As GridViewRowInfo In gv1.Rows
                        If clsCommon.myCdbl(txtQty.Text) > 0 Then
                            If clsCommon.myLen(grow.Cells(colRouteCode).Value) > 0 Then
                                dblorderqty = clsCommon.myCdbl(grow.Cells(colDemandQty).Value) - clsCommon.myCdbl(txtQty.Text)
                                grow.Cells(colAdjustedQty).Value = clsCommon.myCdbl(txtQty.Text)
                                grow.Cells(colFinalQty).Value = dblorderqty
                            End If
                        End If
                    Next
                    RadPageView1.SelectedPage = RadPageViewPage2
                Else
                    Throw New Exception(" Increase/Decrease Qty not more then Limit ( " + clsCommon.myCstr(LimitIncreaseDecreseQty) + " )")
                End If


            End If
                For Each grow As GridViewRowInfo In gv1.Rows
                    If clsCommon.myLen(grow.Cells(colRouteCode).Value) > 0 Then
                        totalDQty += clsCommon.myCdbl(grow.Cells(colDemandQty).Value)
                        totalAQty += clsCommon.myCdbl(grow.Cells(colAdjustedQty).Value)
                        totalFQty += clsCommon.myCdbl(grow.Cells(colFinalQty).Value)
                    Else
                        grow.Cells(colUOM).Value = "Total"
                        grow.Cells(colDemandQty).Value = totalDQty
                        grow.Cells(colAdjustedQty).Value = totalAQty
                        grow.Cells(colFinalQty).Value = totalFQty
                    End If
                Next

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isInsideLoadData = False

        End Try
    End Sub
    Private Sub txtChangeItemCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtChangeItemCode._MYValidating
        Try
            Dim qry As String = " select Item_code as Code,Item_Desc,Short_Description  from TSPL_ITEM_MASTER "
            Dim whrcls As String = " Item_Type='F'"
            txtChangeItemCode.Value = clsCommon.ShowSelectForm("fndItemscode@changeItem", qry, "Code", whrcls, txtChangeItemCode.Value, "Code", isButtonClicked)
            lblChangeitemDesc.Text = clsDBFuncationality.getSingleValue("select Short_Description  from TSPL_ITEM_MASTER where Item_Code='" + txtChangeItemCode.Value + "'")
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub btnChange_Click(sender As Object, e As EventArgs) Handles btnChange.Click
        Dim totalDQty As Double = 0
        Dim totalAQty As Double = 0
        Dim totalFQty As Double = 0
        Try
            isInsideLoadData = True

            Dim whrcls As String = " where 2=2 and TSPL_DEMAND_BOOKING_MASTER.Posted=0 "
            Dim qry As String = ""
            Dim mainQry As String = " select TSPL_ROUTE_MASTER.Zone_Code,TSPL_DEMAND_BOOKING_MASTER.Route_No,TSPL_DEMAND_BOOKING_DETAIL.Cust_Code,TSPL_CUSTOMER_MASTER.Customer_Name,TSPL_DEMAND_BOOKING_DETAIL.Item_Code,TSPL_DEMAND_BOOKING_DETAIL.TR_Code,TSPL_ITEM_MASTER.Short_Description,TSPL_DEMAND_BOOKING_DETAIL.Unit_code,TSPL_DEMAND_BOOKING_DETAIL.Qty
from TSPL_DEMAND_BOOKING_MASTER 
left join TSPL_DEMAND_BOOKING_DETAIL on TSPL_DEMAND_BOOKING_MASTER.Document_No=TSPL_DEMAND_BOOKING_DETAIL.Document_No
left join TSPL_CUSTOMER_MASTER on TSPL_DEMAND_BOOKING_DETAIL.Cust_Code=TSPL_CUSTOMER_MASTER.Cust_Code
left join TSPL_ROUTE_MASTER on TSPL_DEMAND_BOOKING_MASTER.Route_No=TSPL_ROUTE_MASTER.Route_No
left join TSPL_ITEM_MASTER on TSPL_DEMAND_BOOKING_DETAIL.Item_Code=TSPL_ITEM_MASTER.Item_Code"
            whrcls += "  and convert(date,TSPL_DEMAND_BOOKING_MASTER.Document_Date,103)='" + clsCommon.GetPrintDate(txtDemandDate.Value) + "'  and TSPL_DEMAND_BOOKING_DETAIL.Unit_code='" + txtUOM.Value + "' and TSPL_DEMAND_BOOKING_DETAIL.Qty>='" + txtMinCrate.Text + "' "
            If txtZoneCode.arrValueMember IsNot Nothing Then
                If txtRouteCode.arrValueMember IsNot Nothing Then
                    whrcls += " and TSPL_DEMAND_BOOKING_MASTER.Route_No in('" + clsCommon.GetMulcallStringWithComma(txtRouteCode.arrValueMember) + "')"
                Else
                    whrcls += " and TSPL_DEMAND_BOOKING_MASTER.Route_No in(select Route_No from TSPL_ROUTE_MASTER where Zone_Code in(" + clsCommon.GetMulcallStringWithComma(txtZoneCode.arrValueMember) + ") and Zone_Code is not null)"
                End If
            Else
                If txtRouteCode.arrValueMember IsNot Nothing Then
                    whrcls += " and TSPL_DEMAND_BOOKING_MASTER.Route_No in('" + clsCommon.GetMulcallStringWithComma(txtRouteCode.arrValueMember) + "')"
                End If
            End If
            If rbtnMorning.IsChecked Then
                whrcls += " and TSPL_DEMAND_BOOKING_DETAIL.ShiftType='Morning' "
            Else
                whrcls += " and TSPL_DEMAND_BOOKING_DETAIL.ShiftType='Evening' "
            End If
            whrcls += " and TSPL_DEMAND_BOOKING_DETAIL.Cust_Code in(select TSPL_DEMAND_BOOKING_DETAIL.Cust_Code from TSPL_DEMAND_BOOKING_MASTER
left join TSPL_DEMAND_BOOKING_DETAIL on TSPL_DEMAND_BOOKING_MASTER.Document_No=TSPL_DEMAND_BOOKING_DETAIL.Document_No " + whrcls + " and TSPL_DEMAND_BOOKING_DETAIL.Item_Code ='" + txtItemCode.Value + "' ) and TSPL_DEMAND_BOOKING_DETAIL.Item_Code in('" + txtItemCode.Value + "','" + txtChangeItemCode.Value + "')"
            mainQry += whrcls
            qry = " select XX.Zone_Code,xx.Route_No,xx.Cust_Code,xx.Customer_Name,xx.Item_Code,xx.TR_Code,xx.Short_Description,xx.Unit_code,xx.Qty,
case when xx.Item_Code='" + txtItemCode.Value + "' then CAST(" + clsCommon.myCstr(txtDeductQty.Text) + " AS int) else (case when xx.Item_Code='" + txtChangeItemCode.Value + "' then CAST(" + clsCommon.myCstr(txtAddQty.Text) + " AS int) else 0 end ) end as AdjustQty,
case when xx.Item_Code='" + txtItemCode.Value + "' then (xx.Qty - CAST(" + clsCommon.myCstr(txtDeductQty.Text) + " AS int)) else (case when xx.Item_Code='" + txtChangeItemCode.Value + "' then (xx.Qty+ CAST(" + clsCommon.myCstr(txtAddQty.Text) + " AS int)) else 0 end ) end as FinalQty
from(" + mainQry + " )XX  order by xx.Qty desc"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim introw As Integer = 0
                For Each dr As DataRow In dt.Rows
                    gv1.Rows(introw).Cells(ColSNo).Value = introw + 1
                    gv1.Rows(introw).Cells(colZoneCode).Value = clsCommon.myCstr(dr("Zone_Code"))
                    gv1.Rows(introw).Cells(colRouteCode).Value = clsCommon.myCstr(dr("Route_No"))
                    gv1.Rows(introw).Cells(colBoothCode).Value = clsCommon.myCstr(dr("Cust_Code"))
                    gv1.Rows(introw).Cells(colBoothName).Value = clsCommon.myCstr(dr("Customer_Name"))
                    gv1.Rows(introw).Cells(colItemcode).Value = clsCommon.myCstr(dr("Item_Code"))
                    gv1.Rows(introw).Cells(colTRCode).Value = clsCommon.myCstr(dr("TR_Code"))
                    gv1.Rows(introw).Cells(colItemDesc).Value = clsCommon.myCstr(dr("Short_Description"))
                    gv1.Rows(introw).Cells(colUOM).Value = clsCommon.myCstr(dr("Unit_code"))
                    gv1.Rows(introw).Cells(colDemandQty).Value = clsCommon.myCdbl(dr("Qty"))
                    gv1.Rows(introw).Cells(colAdjustedQty).Value = clsCommon.myCdbl(dr("AdjustQty"))
                    gv1.Rows(introw).Cells(colFinalQty).Value = clsCommon.myCdbl(dr("FinalQty"))
                    totalDQty += clsCommon.myCdbl(gv1.Rows(introw).Cells(colDemandQty).Value)
                    totalAQty += clsCommon.myCdbl(gv1.Rows(introw).Cells(colAdjustedQty).Value)
                    totalFQty += clsCommon.myCdbl(gv1.Rows(introw).Cells(colFinalQty).Value)
                    gv1.Rows.AddNew()
                    introw += 1
                Next
                gv1.Rows(introw).Cells(colUOM).Value = "Total"
                gv1.Rows(introw).Cells(colDemandQty).Value = totalDQty
                gv1.Rows(introw).Cells(colAdjustedQty).Value = totalAQty
                gv1.Rows(introw).Cells(colFinalQty).Value = totalFQty
                EnableGroup(True)
                RadPageView1.SelectedPage = RadPageViewPage2
            Else
                Throw New Exception("Data Not Found!")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isInsideLoadData = False

        End Try
    End Sub
    Private Sub rmiImport_Click(sender As Object, e As EventArgs) Handles rmiImport.Click
        clsCommon.MyMessageBoxShow(Me, "Import features will coming soon...", Me.Text)
    End Sub
    Private Sub rmiExport_Click(sender As Object, e As EventArgs) Handles rmiExport.Click
        clsCommon.MyMessageBoxShow(Me, "Export features will coming soon...", Me.Text)
    End Sub
    Sub CreateTable()
        Dim coll As Dictionary(Of String, String)
        coll = New Dictionary(Of String, String)()
        coll.Add("Document_Code", "VARCHAR(30) NOT NULL PRIMARY KEY")
        coll.Add("Document_Date", "Datetime NOT NULL")
        coll.Add("Demand_Date", "Datetime NOT NULL")
        coll.Add("Zone_Code", "VARCHAR(50) NULL")
        coll.Add("Route_Code", "VARCHAR(50) NULL")
        coll.Add("Item_Code", "Varchar(50) NULL References TSPL_ITEM_MASTER(Item_Code)")
        coll.Add("Unit_Code", "varchar(12) NOT NULL")
        coll.Add("Shift_Type", "VARCHAR(10) NOT NULL")
        coll.Add("Is_Change_Product", "int NOT NULL")
        coll.Add("Minimum_Qty", "VARCHAR(200) NOT NULL")
        coll.Add("Is_Automatic", "VARCHAR(200) NOT NULL")
        coll.Add("Is_FixQty", "varchar(20) NOT NULL")
        coll.Add("Increase_Decrease_Qty", "decimal(18,0) NULL")
        coll.Add("Percentage", "decimal(18,2) NULL")
        coll.Add("Deduct_Qty", "decimal(18,0) NULL")
        coll.Add("Change_Item_Code", "Varchar(50) NULL References TSPL_ITEM_MASTER(Item_Code)")
        coll.Add("Add_Qty", "decimal(18,0) NULL")
        coll.Add("Status", "int NULL")
        coll.Add("Modified_By", "varchar(20) NOT NULL")
        coll.Add("Modified_Date", "Datetime NOT NULL")
        coll.Add("Created_By", "varchar(20)  NULL ")
        coll.Add("Created_Date", "Datetime  NULL")
        clsCommonFunctionality.CreateOrAlterTable(True, False, "TSPL_DEMAND_ADJUSTMENT_HEAD", coll, Nothing, True, False, "", "Document_Code", "")
        coll = New Dictionary(Of String, String)()
        coll.Add("Document_Code", "VARCHAR(30) NOT NULL REFERENCES TSPL_DEMAND_ADJUSTMENT_HEAD(Document_Code) ")
        coll.Add("TR_Code", "varchar(30) Not NULL")
        coll.Add("Zone_Code", "varchar(30) Not NULL")
        coll.Add("Route_Code", "varchar(12)  NOT NULL REFERENCES TSPL_ROUTE_MASTER (Route_No)")
        coll.Add("Booth_Code", "varchar(12) NULL references TSPL_CUSTOMER_MASTER(Cust_Code)")
        coll.Add("Item_Code", "Varchar(50) NULL References TSPL_ITEM_MASTER(Item_Code)")
        coll.Add("Unit_Code", "varchar(12) null REFERENCES TSPL_UNIT_MASTER (Unit_Code)")
        coll.Add("Demand_Qty", "Decimal(18,0) Not NULL")
        coll.Add("Adjust_Qty", "Decimal(18,0) Not NULL")
        coll.Add("Final_Qty", "Decimal(18,0) Not NULL")
        clsCommonFunctionality.CreateOrAlterTable(True, False, "TSPL_DEMAND_ADJUSTMENT_DETAIL", coll, "", True, False, "TSPL_DEMAND_ADJUSTMENT_HEAD", "Document_Code", "")
    End Sub
    Function AllowToSave() As Boolean
        Try
            If clsCommon.myLen(txtItemCode.Value) <= 0 Then
                Throw New Exception("Please select Item Code!")
            End If
            If clsCommon.myLen(txtUOM.Value) <= 0 Then
                Throw New Exception("Please select UOM!")
            End If
            If clsCommon.myLen(txtMinCrate.Text) <= 0 Then
                Throw New Exception("Please Enter Minimum Qty!")
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            If (AllowToSave()) Then
                Dim obj As New clsDemandAdjustment()
                obj.Document_Date = clsCommon.myCDate(txtDate.Value)
                obj.Demand_Date = clsCommon.myCDate(txtDemandDate.Value)
                If txtZoneCode.arrValueMember IsNot Nothing AndAlso txtZoneCode.arrDispalyMember IsNot Nothing Then
                    obj.Zone_Code = clsCommon.GetMulcallStringWithComma(txtZoneCode.arrDispalyMember)
                Else
                    obj.Zone_Code = Nothing
                End If
                If txtRouteCode.arrValueMember IsNot Nothing AndAlso txtRouteCode.arrDispalyMember IsNot Nothing Then
                    obj.Route_Code = clsCommon.GetMulcallStringWithComma(txtRouteCode.arrDispalyMember)
                End If
                If rbtnMorning.IsChecked Then
                    obj.Shift_Type = "Morning"
                ElseIf rbtnEvening.IsChecked Then
                    obj.Shift_Type = "Evening"
                End If
                obj.Document_Code = txtDocNo.Value
                obj.Item_Code = txtItemCode.Value
                obj.Unit_Code = txtUOM.Value
                obj.Minimum_Qty = txtMinCrate.Text
                obj.Is_Change_Product = chkChangeItem.Checked
                obj.Is_Automatic = rbtnAutomatic.IsChecked
                obj.Is_FixQty = rbtnFix.IsChecked
                obj.Increase_Decrease_Qty = clsCommon.myCdbl(txtQty.Text)
                obj.Percentage = clsCommon.myCdbl(txtPer.Text)
                obj.Deduct_Qty = clsCommon.myCdbl(txtDeductQty.Text)
                obj.Add_Qty = clsCommon.myCdbl(txtAddQty.Text)
                If clsCommon.myLen(txtChangeItemCode.Value) > 0 Then
                    obj.Change_Item_Code = txtChangeItemCode.Value
                End If
                obj.Arr = New List(Of clsDemandAdjustmentDetail)
                For Each grow As GridViewRowInfo In gv1.Rows
                    If clsCommon.myLen(grow.Cells(colRouteCode).Value) > 0 Then
                        Dim objTr As New clsDemandAdjustmentDetail()
                        objTr.TR_Code = clsCommon.myCstr(grow.Cells(colTRCode).Value)
                        objTr.Zone_Code = clsCommon.myCstr(grow.Cells(colZoneCode).Value)
                        objTr.Route_Code = clsCommon.myCstr(grow.Cells(colRouteCode).Value)
                        objTr.Booth_Code = clsCommon.myCstr(grow.Cells(colBoothCode).Value)
                        objTr.Item_Code = clsCommon.myCstr(grow.Cells(colItemcode).Value)
                        objTr.Unit_Code = clsCommon.myCstr(grow.Cells(colUOM).Value)
                        objTr.Demand_Qty = clsCommon.myCdbl(grow.Cells(colDemandQty).Value)
                        objTr.Adjust_Qty = clsCommon.myCdbl(grow.Cells(colAdjustedQty).Value)
                        objTr.Final_Qty = clsCommon.myCdbl(grow.Cells(colFinalQty).Value)

                        obj.Arr.Add(objTr)
                    End If
                Next
                If (obj.SaveData(obj, isNewEntry)) = True Then
                    clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                    LoadData(obj.Document_Code, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Try


            Dim obj As New clsDemandAdjustment
            'Dim intRow As Integer
            obj = clsDemandAdjustment.GetData(strCode, NavTyep)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Document_Code) > 0) Then
                AddNew()
                EnableGroup(True)
                txtDate.Enabled = False
                txtDemandDate.Enabled = False
                If obj.Status = 1 Then
                    btnSave.Enabled = False
                    btnDelete.Enabled = False
                    btnProceed.Enabled = False
                Else
                    btnSave.Enabled = True
                    btnDelete.Enabled = True
                    btnProceed.Enabled = True
                End If
                isInsideLoadData = True

                isNewEntry = False
                chkChangeItem.Enabled = False
                txtDocNo.Value = obj.Document_Code
                txtDate.Value = obj.Document_Date
                txtDemandDate.Value = obj.Demand_Date
                If obj.Zone_Code IsNot Nothing Then
                    Dim delimiter As Char = ","c
                    Dim substrings() As String = obj.Zone_Code.Split(delimiter)
                    Dim zoneList As New ArrayList()
                    For Each substring As String In substrings
                        zoneList.Add(substring)
                    Next
                    txtZoneCode.arrValueMember = zoneList
                End If
                If obj.Route_Code IsNot Nothing Then
                    Dim delimiter As Char = ","c
                    Dim substrings() As String = obj.Route_Code.Split(delimiter)
                    Dim routeList As New ArrayList()
                    For Each substring As String In substrings
                        routeList.Add(substring)
                    Next
                    txtRouteCode.arrValueMember = routeList
                End If
                If clsCommon.CompairString(obj.Shift_Type, "Morning") = CompairStringResult.Equal Then
                    rbtnMorning.IsChecked = True
                Else
                    rbtnEvening.IsChecked = True
                End If
                txtItemCode.Value = obj.Item_Code
                lblItemDesc.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Short_Description  from TSPL_ITEM_MASTER where Item_Code='" + obj.Item_Code + "'"))
                txtUOM.Value = obj.Unit_Code
                lblUOMDesc.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select UOM_Description from TSPL_ITEM_UOM_DETAIL where UOM_Code='" + obj.Unit_Code + "' and Item_Code='" + obj.Item_Code + "'"))
                chkChangeItem.Checked = obj.Is_Change_Product
                txtMinCrate.Text = obj.Minimum_Qty
                rbtnAutomatic.IsChecked = obj.Is_Automatic
                rbtnFix.IsChecked = obj.Is_FixQty
                txtQty.Text = obj.Increase_Decrease_Qty
                txtPer.Text = obj.Percentage
                txtDeductQty.Text = obj.Deduct_Qty
                txtChangeItemCode.Value = obj.Change_Item_Code
                lblChangeitemDesc.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Short_Description  from TSPL_ITEM_MASTER where Item_Code='" + obj.Change_Item_Code + "'"))
                txtAddQty.Text = obj.Add_Qty
                If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                    Dim dblrows As Integer = 0
                    Dim dblDtotal As Integer = 0
                    Dim dblAtotal As Integer = 0
                    Dim dblFtotal As Integer = 0
                    For Each objTr As clsDemandAdjustmentDetail In obj.Arr
                        gv1.Rows(dblrows).Cells(ColSNo).Value = dblrows + 1
                        gv1.Rows(dblrows).Cells(colZoneCode).Value = objTr.Zone_Code
                        gv1.Rows(dblrows).Cells(colTRCode).Value = objTr.TR_Code
                        gv1.Rows(dblrows).Cells(colRouteCode).Value = objTr.Route_Code
                        gv1.Rows(dblrows).Cells(colBoothCode).Value = objTr.Booth_Code
                        gv1.Rows(dblrows).Cells(colBoothName).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Customer_Name  from TSPL_CUSTOMER_MASTER where CUST_CODE='" + objTr.Booth_Code + "'"))
                        gv1.Rows(dblrows).Cells(colItemcode).Value = objTr.Item_Code
                        gv1.Rows(dblrows).Cells(colItemDesc).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Short_Description  from TSPL_ITEM_MASTER where Item_Code='" + objTr.Item_Code + "'"))
                        gv1.Rows(dblrows).Cells(colUOM).Value = objTr.Unit_Code
                        gv1.Rows(dblrows).Cells(colDemandQty).Value = objTr.Demand_Qty
                        gv1.Rows(dblrows).Cells(colAdjustedQty).Value = objTr.Adjust_Qty
                        gv1.Rows(dblrows).Cells(colFinalQty).Value = objTr.Final_Qty
                        dblDtotal += objTr.Demand_Qty
                        dblAtotal += objTr.Adjust_Qty
                        dblFtotal += objTr.Final_Qty
                        gv1.Rows.AddNew()
                        dblrows += 1
                    Next
                    gv1.Rows(dblrows).Cells(colUOM).Value = "Total"
                    gv1.Rows(dblrows).Cells(colDemandQty).Value = dblDtotal
                    gv1.Rows(dblrows).Cells(colAdjustedQty).Value = dblAtotal
                    gv1.Rows(dblrows).Cells(colFinalQty).Value = dblFtotal
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isInsideLoadData = False

        End Try

    End Sub
    Private Sub txtDocNo__MYNavigator(sender As Object, e As EventArgs, NavType As NavigatorType) Handles txtDocNo._MYNavigator
        Try
            Dim qry As String = "select count(*) from TSPL_DEMAND_ADJUSTMENT_HEAD where Document_Code='" + txtDocNo.Value + "' "
            Dim check As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
            If check > 0 Then
                txtDocNo.MyReadOnly = True
            ElseIf check <= 0 Then
                txtDocNo.MyReadOnly = False
            End If
            LoadData(txtDocNo.Value, NavType)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub txtDocNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtDocNo._MYValidating
        Try
            Dim qry As String = "select TSPL_DEMAND_ADJUSTMENT_HEAD.Document_Code as DocumentCode,convert(varchar(12),TSPL_DEMAND_ADJUSTMENT_HEAD.Document_date,103) as DocumentDate,TSPL_DEMAND_ADJUSTMENT_HEAD.Shift_Type from TSPL_DEMAND_ADJUSTMENT_HEAD "
            'Dim whrClas As String = " TSPL_DEMAND_BOOKING_MASTER.comp_code='" + objCommonVar.CurrentCompanyCode + "' "
            Reset()
            LoadData(clsCommon.ShowSelectForm("FSBook1DocNo", qry, "DocumentCode", "", txtDocNo.Value, "Document_date DESC", isButtonClicked, " TSPL_DEMAND_ADJUSTMENT_HEAD.Document_date "), NavigatorType.Current)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub btnProceed_Click(sender As Object, e As EventArgs) Handles btnProceed.Click
        Try
            If common.clsCommon.MyMessageBoxShow("Do you want to Proceed this record?", "", MessageBoxButtons.YesNo, RadMessageIcon.Question) = DialogResult.Yes Then


                If clsCommon.myLen(txtDocNo.Value) > 0 Then
                    If clsDemandAdjustment.ProceedDemand(txtDocNo.Value) Then
                        clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                        LoadData(txtDocNo.Value, NavigatorType.Current)

                    End If
                Else
                    Throw New Exception("Document not found!")
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Try
            If clsCommon.myLen(txtDocNo.Value) > 0 Then
                If (myMessages.deleteConfirm()) Then
                    If clsDemandAdjustment.DeleteData(txtDocNo.Value) Then
                        clsCommon.MyMessageBoxShow(Me, "Delete Successfully", Me.Text)
                        AddNew()
                    End If
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    'Private Sub gv1_CellValueChanged(sender As Object, e As GridViewCellEventArgs) Handles gv1.CellValueChanged
    '    Try
    '        If (Not isInsideLoadData) Then
    '            If Not isCellValueChangedOpen Then
    '                isCellValueChangedOpen = True
    '                If e.Column Is gv1.Columns(colAdjustedQty) Then

    '                End If

    '            End If
    '            isCellValueChangedOpen = False

    '        End If

    '    Catch ex As Exception
    '        clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
    '    Finally
    '        isCellValueChangedOpen = False

    '    End Try
    'End Sub
    'Private Sub UpdateAllTotals()

    'End Sub
End Class
