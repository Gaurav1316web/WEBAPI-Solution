Imports common
Imports System.IO
Public Class frmDemandAdjustment
    Inherits FrmMainTranScreen
#Region "Variables"
    Dim isNewEntry As Boolean = False
    Dim LimitIncreaseDecreseQty As Double = 0
    Dim LimitIncreaseDecresePer As Double = 0
    Dim AmountToCheckCustomerOutstandingForTCSTax As Double = 0
    Dim IncraseOrder As Boolean = False
    Dim DecreaseOrder As Boolean = False
    Dim isChange As Boolean = False
    Private isInsideLoadData As Boolean = False
    Private isCellValueChangedOpen As Boolean = False
    Const ColSNo As String = "ColSNo"
    Const colZoneCode As String = "colZoneCode"
    Const colRouteCode As String = "colRouteCode"
    Const colLocationCode As String = "colLocationCode"
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
    Public Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmBookingProductSale)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If


    End Sub
    Private Sub frmDemandAdjustment_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CreateTable()
        LimitIncreaseDecreseQty = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.DemandIncreaseDecreaseQty, clsFixedParameterCode.DemandIncreaseDecreaseQty, Nothing))
        LimitIncreaseDecresePer = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.DemandIncreaseDecreasePer, clsFixedParameterCode.DemandIncreaseDecreasePer, Nothing))
        AmountToCheckCustomerOutstandingForTCSTax = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AmountToCheckCustomerOutstandingForTCSTax, clsFixedParameterCode.AmountToCheckCustomerOutstandingForTCSTax, Nothing))

        SetUserMgmtNew()
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
        chkChangeItem.Enabled = True
        If isChange Then
            chkChangeItem.Checked = True
            rgbChangeProduct.Visible = True
            rgbChangeProduct.Enabled = True
            rbtnAll.IsChecked = True
            rgbMode.Visible = False
            rgbIDOrder.Visible = False
            rgbIncreaseOrder.Visible = False
            rgbDecreaseOrder.Visible = False
        Else
            chkChangeItem.Checked = False
            rgbChangeProduct.Visible = False
            rgbMode.Visible = True
            rgbIDOrder.Visible = True
            rgbIncreaseOrder.Visible = True
            rgbDecreaseOrder.Visible = True
        End If
        rbtnAutomatic.IsChecked = True
        txtQty.Text = 0
        txtMinCrate.Text = ""


        txtChangeItemCode.Value = ""
        lblChangeitemDesc.Text = ""
        txtPer.Text = 0
        txtFixedQty.Text = 0
        txtChangeQty.Text = 0

        LoadBlankGrid()
        EnableGroup(False)
        btnProceed.Enabled = False
        isNewEntry = True
        IncraseOrder = False
        DecreaseOrder = False
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
        Dim repoLocationCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoLocationCode.FormatString = ""
        repoLocationCode.HeaderText = "Locaiton Code"
        repoLocationCode.Name = colLocationCode
        repoLocationCode.Width = 150
        repoLocationCode.IsVisible = False
        repoLocationCode.ReadOnly = True
        repoLocationCode.TextImageRelation = TextImageRelation.TextBeforeImage
        gv1.MasterTemplate.Columns.Add(repoLocationCode)
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
    Private Sub txtQty_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtMinCrate.KeyPress, txtPer.KeyPress, txtQty.KeyPress
        If Not Char.IsControl(e.KeyChar) AndAlso Not Char.IsDigit(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub
    Private Sub chkChangeItem_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkChangeItem.ToggleStateChanged
        If chkChangeItem.Checked Then
            isChange = True
            AddNew()

            rgbChangeProduct.Visible = True
            rbtnAll.IsChecked = True
            rgbMode.Visible = False
            rgbIDOrder.Visible = False
            rgbIncreaseOrder.Visible = False
            rgbDecreaseOrder.Visible = False
        Else
            isChange = False
            AddNew()

            rgbChangeProduct.Visible = False
            rgbMode.Visible = True
            rgbIDOrder.Visible = True
            rgbIncreaseOrder.Visible = True
            rgbDecreaseOrder.Visible = True
        End If
    End Sub

    Public Sub EnableGroup(ByVal flag As Boolean)

        rgbMode.Enabled = flag
        rgbIDOrder.Enabled = flag
        rgbDecreaseOrder.Enabled = flag
        rgbIncreaseOrder.Enabled = flag
        If isChange Then
            btnSave.Enabled = Not flag
            btnDelete.Enabled = Not flag
            btnGo.Enabled = Not flag

        Else
            btnSave.Enabled = flag
            btnDelete.Enabled = flag
            btnGo.Enabled = Not flag

        End If
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
            lblFixedQty.Visible = False
            txtFixedQty.Visible = False
            txtFixedQty.Text = 0
        Else
            lblPer.Visible = False
            txtPer.Visible = False
            txtQty.Text = 0
            txtPer.Text = 0
            txtFixedQty.Text = 0
            lblFixedQty.Visible = True
            txtFixedQty.Visible = True
            lblFixedQty.Location = New System.Drawing.Point(146, 23)
            txtFixedQty.Location = New System.Drawing.Point(212, 22)
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
                qry = "select Route_No as Code,Route_Desc as Name from TSPL_ROUTE_MASTER where Zone_Code in(" & clsCommon.GetMulcallStringWithComma(txtZoneCode.arrValueMember) & ")"
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
            lblItemDesc.Text = clsDBFuncationality.getSingleValue("select Short_Description  from TSPL_ITEM_MASTER where Item_Code='" & txtItemCode.Value & "'")
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub txtUOM__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtUOM._MYValidating
        Try
            If clsCommon.myLen(txtItemCode.Value) > 0 Then
                Dim qry As String = "select UOM_Code as Code from TSPL_ITEM_UOM_DETAIL"
                txtUOM.Value = clsCommon.ShowSelectForm("fndItemUom", qry, "Code", " Item_Code='" & txtItemCode.Value & "'", txtUOM.Value, "Code", isButtonClicked)
                lblUOMDesc.Text = txtUOM.Value
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
                isInsideLoadData = True
                Dim whrcls As String = ""
                Dim qry As String = ""

                If isChange Then
                    If clsCommon.myLen(txtChangeItemCode.Value) <= 0 OrElse txtRouteCode.arrValueMember Is Nothing Then
                        Throw New Exception("Change Item Code/Route no Not Found")
                    End If
                    qry = "select TSPL_ROUTE_MASTER.Zone_Code,TSPL_DEMAND_BOOKING_MASTER.Route_No,TSPL_DEMAND_BOOKING_MASTER.Location_Code,TSPL_DEMAND_BOOKING_DETAIL.Cust_Code,TSPL_CUSTOMER_MASTER.Customer_Name,'" & txtItemCode.Value & "' as Item_Code,TSPL_DEMAND_BOOKING_DETAIL.Document_No,'" & lblItemDesc.Text & "' as Short_Description,TSPL_DEMAND_BOOKING_DETAIL.Unit_code,0 as Qty," & IIf(rbtnChangebyPer.IsChecked, "Round((TSPL_DEMAND_BOOKING_DETAIL.Qty *" & clsCommon.myCstr(txtChangeQty.Text) & "/100),0)", IIf(rbtnchangeFixQty.IsChecked, "(case when TSPL_DEMAND_BOOKING_DETAIL.Qty>=" & clsCommon.myCstr(txtChangeQty.Text) & " then " & clsCommon.myCstr(txtChangeQty.Text) & " else 0 end)", "TSPL_DEMAND_BOOKING_DETAIL.Qty")) & " As AdjustQty
from TSPL_DEMAND_BOOKING_MASTER 
left join TSPL_DEMAND_BOOKING_DETAIL on TSPL_DEMAND_BOOKING_MASTER.Document_No=TSPL_DEMAND_BOOKING_DETAIL.Document_No
left join TSPL_CUSTOMER_MASTER on TSPL_DEMAND_BOOKING_DETAIL.Cust_Code=TSPL_CUSTOMER_MASTER.Cust_Code
left join TSPL_ROUTE_MASTER on TSPL_DEMAND_BOOKING_MASTER.Route_No=TSPL_ROUTE_MASTER.Route_No
left join TSPL_ITEM_MASTER on TSPL_DEMAND_BOOKING_DETAIL.Item_Code=TSPL_ITEM_MASTER.Item_Code
where TSPL_DEMAND_BOOKING_MASTER.Posted=0 and convert(date,TSPL_DEMAND_BOOKING_MASTER.Document_Date,103)='" & clsCommon.GetPrintDate(txtDemandDate.Value) & "' and TSPL_DEMAND_BOOKING_DETAIL.Item_Code='" & txtChangeItemCode.Value & "' and TSPL_DEMAND_BOOKING_DETAIL.Unit_code='" & txtUOM.Value & "' and TSPL_DEMAND_BOOKING_DETAIL.Qty>='" & txtMinCrate.Text & "' and TSPL_DEMAND_BOOKING_MASTER.ShiftType='" & IIf(rbtnMorning.IsChecked, "Morning", "Evening") & "' "
                    If txtZoneCode.arrValueMember IsNot Nothing Then
                        If txtRouteCode.arrValueMember IsNot Nothing Then
                            qry += " and TSPL_DEMAND_BOOKING_MASTER.Route_No in(" & clsCommon.GetMulcallString(txtRouteCode.arrValueMember) & ")"
                        Else
                            qry += " and TSPL_DEMAND_BOOKING_MASTER.Route_No in(select Route_No from TSPL_ROUTE_MASTER where Zone_Code in(" & clsCommon.GetMulcallString(txtZoneCode.arrValueMember) & ") and Zone_Code is not null)"
                        End If
                    Else
                        If txtRouteCode.arrValueMember IsNot Nothing Then
                            qry += " and TSPL_DEMAND_BOOKING_MASTER.Route_No in(" & clsCommon.GetMulcallString(txtRouteCode.arrValueMember) & ")"
                        End If
                    End If
                    qry += "Union all 
select TSPL_ROUTE_MASTER.Zone_Code,TSPL_DEMAND_BOOKING_MASTER.Route_No,TSPL_DEMAND_BOOKING_MASTER.Location_Code,TSPL_DEMAND_BOOKING_DETAIL.Cust_Code,TSPL_CUSTOMER_MASTER.Customer_Name,TSPL_DEMAND_BOOKING_DETAIL.Item_Code,TSPL_DEMAND_BOOKING_DETAIL.Document_No,TSPL_ITEM_MASTER.Short_Description,TSPL_DEMAND_BOOKING_DETAIL.Unit_code,TSPL_DEMAND_BOOKING_DETAIL.Qty,0 as AdJustQty
from TSPL_DEMAND_BOOKING_MASTER 
left join TSPL_DEMAND_BOOKING_DETAIL on TSPL_DEMAND_BOOKING_MASTER.Document_No=TSPL_DEMAND_BOOKING_DETAIL.Document_No
left join TSPL_CUSTOMER_MASTER on TSPL_DEMAND_BOOKING_DETAIL.Cust_Code=TSPL_CUSTOMER_MASTER.Cust_Code
left join TSPL_ROUTE_MASTER on TSPL_DEMAND_BOOKING_MASTER.Route_No=TSPL_ROUTE_MASTER.Route_No
left join TSPL_ITEM_MASTER on TSPL_DEMAND_BOOKING_DETAIL.Item_Code=TSPL_ITEM_MASTER.Item_Code
where TSPL_DEMAND_BOOKING_MASTER.Posted=0 and convert(date,TSPL_DEMAND_BOOKING_MASTER.Document_Date,103)='" & clsCommon.GetPrintDate(txtDemandDate.Value) & "' and TSPL_DEMAND_BOOKING_DETAIL.Item_Code='" & txtItemCode.Value & "' and TSPL_DEMAND_BOOKING_DETAIL.Unit_code='" & txtUOM.Value & "' and TSPL_DEMAND_BOOKING_MASTER.ShiftType='" & IIf(rbtnMorning.IsChecked, "Morning", "Evening") & "'"
                    If txtZoneCode.arrValueMember IsNot Nothing Then
                        If txtRouteCode.arrValueMember IsNot Nothing Then
                            qry += " and TSPL_DEMAND_BOOKING_MASTER.Route_No in(" & clsCommon.GetMulcallString(txtRouteCode.arrValueMember) & ")"
                        Else
                            qry += " and TSPL_DEMAND_BOOKING_MASTER.Route_No in(select Route_No from TSPL_ROUTE_MASTER where Zone_Code in(" & clsCommon.GetMulcallString(txtZoneCode.arrValueMember) & ") and Zone_Code is not null)"
                        End If
                    Else
                        If txtRouteCode.arrValueMember IsNot Nothing Then
                            qry += " and TSPL_DEMAND_BOOKING_MASTER.Route_No in(" & clsCommon.GetMulcallString(txtRouteCode.arrValueMember) & ")"
                        End If
                    End If
                    qry = "select MAX(XX.Zone_Code)as Zone_Code,XX.Route_No as Route_No,MAX(XX.Location_Code) as Location_Code,
                    XX.Cust_Code,MAX(XX.Customer_Name)as Customer_Name,
                    '" & txtItemCode.Value & "' as Item_Code,max(XX.Document_No) as TR_Code ,'" & lblItemDesc.Text & "' as Short_Description ,
                    max(XX.Unit_code) as Unit_Code,sum(XX.Qty) as Qty,SUM(XX.AdjustQty) as AdjustQty ,(sum(XX.Qty)+SUM(XX.AdjustQty)) as FinalQty  from (" & qry & ") XX  group by XX.Cust_Code,XX.Route_No "
                    If rbtnChangebyPer.IsChecked OrElse rbtnchangeFixQty.IsChecked Then
                        qry += " union all "
                        qry += " select MAX(ChangeItem.Zone_Code)as Zone_Code,ChangeItem.Route_No as Route_No,MAX(ChangeItem.Location_Code) as Location_Code,
                    ChangeItem.Cust_Code,MAX(ChangeItem.Customer_Name)as Customer_Name,
                    '" & txtChangeItemCode.Value & "' as Item_Code,max(ChangeItem.Document_No) as TR_Code ,'" & lblChangeitemDesc.Text & "' as Short_Description ,
                    max(ChangeItem.Unit_code) as Unit_Code,sum(ChangeItem.Qty) as Qty,SUM(ChangeItem.AdjustQty) as AdjustQty ,(sum(ChangeItem.Qty)+SUM(ChangeItem.AdjustQty)) as FinalQty  
					from (select TSPL_ROUTE_MASTER.Zone_Code,TSPL_DEMAND_BOOKING_MASTER.Route_No,TSPL_DEMAND_BOOKING_MASTER.Location_Code,TSPL_DEMAND_BOOKING_DETAIL.Cust_Code,TSPL_CUSTOMER_MASTER.Customer_Name,'" & txtChangeItemCode.Value & "' as Item_Code,TSPL_DEMAND_BOOKING_DETAIL.Document_No,'" & lblChangeitemDesc.Text & "' as Short_Description,TSPL_DEMAND_BOOKING_DETAIL.Unit_code,TSPL_DEMAND_BOOKING_DETAIL.Qty as Qty," & IIf(rbtnChangebyPer.IsChecked, "-Round((TSPL_DEMAND_BOOKING_DETAIL.Qty *" & clsCommon.myCstr(txtChangeQty.Text) & "/100),0)", "- (case when TSPL_DEMAND_BOOKING_DETAIL.Qty>=" & clsCommon.myCstr(txtChangeQty.Text) & " then " & clsCommon.myCstr(txtChangeQty.Text) & " else 0 end)") & " as AdjustQty
from TSPL_DEMAND_BOOKING_MASTER 
left join TSPL_DEMAND_BOOKING_DETAIL on TSPL_DEMAND_BOOKING_MASTER.Document_No=TSPL_DEMAND_BOOKING_DETAIL.Document_No
left join TSPL_CUSTOMER_MASTER on TSPL_DEMAND_BOOKING_DETAIL.Cust_Code=TSPL_CUSTOMER_MASTER.Cust_Code
left join TSPL_ROUTE_MASTER on TSPL_DEMAND_BOOKING_MASTER.Route_No=TSPL_ROUTE_MASTER.Route_No
left join TSPL_ITEM_MASTER on TSPL_DEMAND_BOOKING_DETAIL.Item_Code=TSPL_ITEM_MASTER.Item_Code
where TSPL_DEMAND_BOOKING_MASTER.Posted=0 and convert(date,TSPL_DEMAND_BOOKING_MASTER.Document_Date,103)='" & clsCommon.GetPrintDate(txtDemandDate.Value) & "' and TSPL_DEMAND_BOOKING_DETAIL.Item_Code='" & txtChangeItemCode.Value & "' and TSPL_DEMAND_BOOKING_DETAIL.Unit_code='" & txtUOM.Value & "' and TSPL_DEMAND_BOOKING_DETAIL.Qty>='" & txtMinCrate.Text & "' and TSPL_DEMAND_BOOKING_MASTER.ShiftType='" & IIf(rbtnMorning.IsChecked, "Morning", "Evening") & "'"
                        If txtZoneCode.arrValueMember IsNot Nothing Then
                            If txtRouteCode.arrValueMember IsNot Nothing Then
                                qry += " and TSPL_DEMAND_BOOKING_MASTER.Route_No in(" & clsCommon.GetMulcallString(txtRouteCode.arrValueMember) & ")"
                            Else
                                qry += " and TSPL_DEMAND_BOOKING_MASTER.Route_No in(select Route_No from TSPL_ROUTE_MASTER where Zone_Code in(" & clsCommon.GetMulcallString(txtZoneCode.arrValueMember) & ") and Zone_Code is not null)"
                            End If
                        Else
                            If txtRouteCode.arrValueMember IsNot Nothing Then
                                qry += " and TSPL_DEMAND_BOOKING_MASTER.Route_No in(" & clsCommon.GetMulcallString(txtRouteCode.arrValueMember) & ")"
                            End If
                        End If
                        qry += " ) ChangeItem  group by ChangeItem.Cust_Code,ChangeItem.Route_No "
                    End If
                Else
                    qry = "select TSPL_ROUTE_MASTER.Zone_Code,TSPL_DEMAND_BOOKING_MASTER.Route_No,TSPL_DEMAND_BOOKING_MASTER.Location_Code,TSPL_DEMAND_BOOKING_DETAIL.Cust_Code,TSPL_CUSTOMER_MASTER.Customer_Name,TSPL_DEMAND_BOOKING_DETAIL.Item_Code,TSPL_DEMAND_BOOKING_DETAIL.TR_Code,TSPL_ITEM_MASTER.Short_Description,TSPL_DEMAND_BOOKING_DETAIL.Unit_code,TSPL_DEMAND_BOOKING_DETAIL.Qty,0 as AdJustQty
from TSPL_DEMAND_BOOKING_MASTER 
left join TSPL_DEMAND_BOOKING_DETAIL on TSPL_DEMAND_BOOKING_MASTER.Document_No=TSPL_DEMAND_BOOKING_DETAIL.Document_No
left join TSPL_CUSTOMER_MASTER on TSPL_DEMAND_BOOKING_DETAIL.Cust_Code=TSPL_CUSTOMER_MASTER.Cust_Code
left join TSPL_ROUTE_MASTER on TSPL_DEMAND_BOOKING_MASTER.Route_No=TSPL_ROUTE_MASTER.Route_No
left join TSPL_ITEM_MASTER on TSPL_DEMAND_BOOKING_DETAIL.Item_Code=TSPL_ITEM_MASTER.Item_Code
where TSPL_DEMAND_BOOKING_MASTER.Posted=0 and convert(date,TSPL_DEMAND_BOOKING_MASTER.Document_Date,103)='" & clsCommon.GetPrintDate(txtDemandDate.Value) & "' and TSPL_DEMAND_BOOKING_DETAIL.Item_Code='" & txtItemCode.Value & "' and TSPL_DEMAND_BOOKING_DETAIL.Unit_code='" & txtUOM.Value & "' and TSPL_DEMAND_BOOKING_DETAIL.Qty>='" & txtMinCrate.Text & "' "
                    If txtZoneCode.arrValueMember IsNot Nothing Then
                        If txtRouteCode.arrValueMember IsNot Nothing Then
                            whrcls += " and TSPL_DEMAND_BOOKING_MASTER.Route_No in(" & clsCommon.GetMulcallString(txtRouteCode.arrValueMember) & ")"
                        Else
                            whrcls += " and TSPL_DEMAND_BOOKING_MASTER.Route_No in(select Route_No from TSPL_ROUTE_MASTER where Zone_Code in(" & clsCommon.GetMulcallString(txtZoneCode.arrValueMember) & ") and Zone_Code is not null)"
                        End If
                    Else
                        If txtRouteCode.arrValueMember IsNot Nothing Then
                            whrcls += " and TSPL_DEMAND_BOOKING_MASTER.Route_No in(" & clsCommon.GetMulcallString(txtRouteCode.arrValueMember) & ")"
                        End If
                    End If
                    If rbtnMorning.IsChecked Then
                        whrcls += " and TSPL_DEMAND_BOOKING_DETAIL.ShiftType='Morning' "
                    Else
                        whrcls += " and TSPL_DEMAND_BOOKING_DETAIL.ShiftType='Evening' "
                    End If
                    qry += whrcls
                    qry += " order by TSPL_DEMAND_BOOKING_DETAIL.Qty desc "
                End If

                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    Dim introw As Integer = 0
                    For Each dr As DataRow In dt.Rows
                        gv1.Rows(introw).Cells(ColSNo).Value = introw + 1
                        gv1.Rows(introw).Cells(colZoneCode).Value = clsCommon.myCstr(dr("Zone_Code"))
                        gv1.Rows(introw).Cells(colRouteCode).Value = clsCommon.myCstr(dr("Route_No"))
                        gv1.Rows(introw).Cells(colLocationCode).Value = clsCommon.myCstr(dr("Location_Code"))
                        gv1.Rows(introw).Cells(colBoothCode).Value = clsCommon.myCstr(dr("Cust_Code"))
                        gv1.Rows(introw).Cells(colBoothName).Value = clsCommon.myCstr(dr("Customer_Name"))
                        gv1.Rows(introw).Cells(colItemcode).Value = clsCommon.myCstr(dr("Item_Code"))
                        gv1.Rows(introw).Cells(colTRCode).Value = clsCommon.myCstr(dr("TR_Code"))
                        gv1.Rows(introw).Cells(colItemDesc).Value = clsCommon.myCstr(dr("Short_Description"))
                        gv1.Rows(introw).Cells(colUOM).Value = clsCommon.myCstr(dr("Unit_code"))
                        gv1.Rows(introw).Cells(colDemandQty).Value = clsCommon.myCdbl(dr("Qty"))
                        gv1.Rows(introw).Cells(colAdjustedQty).Value = clsCommon.myCdbl(dr("AdJustQty"))
                        If isChange Then
                            gv1.Rows(introw).Cells(colFinalQty).Value = clsCommon.myCdbl(dr("FinalQty"))
                        Else
                            gv1.Rows(introw).Cells(colFinalQty).Value = clsCommon.myCdbl(gv1.Rows(introw).Cells(colDemandQty).Value) - clsCommon.myCdbl(gv1.Rows(introw).Cells(colAdjustedQty).Value)
                        End If
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
                    If isChange Then
                        EnableGroup(False)
                        rgbChangeProduct.Enabled = False
                    Else
                        EnableGroup(True)
                    End If
                    RadPageView1.SelectedPage = RadPageViewPage2
                Else
                    Throw New Exception("Data Not Found!")
                End If
            Else
                Throw New Exception(" Invalid Item Code/UOM/Minimum Crate Qty!")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isInsideLoadData = False
        End Try
    End Sub
    Private Sub btnIncrease1_Click(sender As Object, e As EventArgs) Handles btnIncrease1.Click
        Dim dbltotalqty As Integer = 0
        Dim dblperqty As Integer = 0
        Dim dblbalqty As Integer = 0
        Dim dblorderqty As Integer = 0
        'Dim totalDQty As Double = 0
        'Dim totalAQty As Double = 0
        'Dim totalFQty As Double = 0
        Try
            isInsideLoadData = True
            IncraseOrder = True
            DecreaseOrder = False
            For Each grow As GridViewRowInfo In gv1.Rows
                grow.Cells(colAdjustedQty).Value = 0
                grow.Cells(colFinalQty).Value = clsCommon.myCdbl(grow.Cells(colDemandQty).Value) + 0
            Next
            If rbtnAutomatic.IsChecked Then
                If clsCommon.myCdbl(txtPer.Text) <= LimitIncreaseDecresePer Then
                    For Each grow As GridViewRowInfo In gv1.Rows
                        If clsCommon.myCdbl(txtQty.Text) > 0 AndAlso clsCommon.myLen(grow.Cells(colRouteCode).Value) > 0 Then
                            'If clsCommon.myLen(grow.Cells(colRouteCode).Value) > 0 Then
                            dblperqty = clsCommon.myCdbl(grow.Cells(colDemandQty).Value) * (clsCommon.myCdbl(txtPer.Text) / 100)
                                dblorderqty = clsCommon.myCdbl(grow.Cells(colDemandQty).Value) + dblperqty
                            If dbltotalqty = 0 AndAlso clsCommon.myCdbl(txtQty.Text) < dblperqty Then
                                'If clsCommon.myCdbl(txtQty.Text) < dblperqty Then
                                dblbalqty = clsCommon.myCdbl(txtQty.Text)
                                If dblbalqty > 0 Then
                                    grow.Cells(colAdjustedQty).Value = dblbalqty
                                    grow.Cells(colFinalQty).Value = clsCommon.myCdbl(grow.Cells(colDemandQty).Value) + dblbalqty
                                End If
                                Exit For
                                'End If
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
                            'End If
                        End If
                    Next
                    RadPageView1.SelectedPage = RadPageViewPage2
                Else
                    Throw New Exception(" Increase/Decrease percentage not more then Limit ( " & clsCommon.myCstr(LimitIncreaseDecresePer) & " )")
                End If
            ElseIf rbtnFix.IsChecked Then
                If clsCommon.myCdbl(txtQty.Text) <= LimitIncreaseDecreseQty Then
                    For Each grow As GridViewRowInfo In gv1.Rows
                        If clsCommon.myCdbl(txtQty.Text) > 0 AndAlso clsCommon.myLen(grow.Cells(colRouteCode).Value) > 0 Then
                            'If clsCommon.myLen(grow.Cells(colRouteCode).Value) > 0 Then
                            If clsCommon.myCdbl(txtFixedQty.Text) >= (dbltotalqty + clsCommon.myCdbl(txtQty.Text)) Then
                                dblorderqty = clsCommon.myCdbl(grow.Cells(colDemandQty).Value) + clsCommon.myCdbl(txtQty.Text)
                                grow.Cells(colAdjustedQty).Value = clsCommon.myCdbl(txtQty.Text)
                                grow.Cells(colFinalQty).Value = dblorderqty
                                dbltotalqty += clsCommon.myCdbl(txtQty.Text)
                            Else
                                dblbalqty = clsCommon.myCdbl(txtFixedQty.Text) - dbltotalqty
                                If clsCommon.myCdbl(txtQty.Text) >= dblbalqty Then
                                    dblorderqty = clsCommon.myCdbl(grow.Cells(colDemandQty).Value) + clsCommon.myCdbl(dblbalqty)
                                    grow.Cells(colAdjustedQty).Value = clsCommon.myCdbl(dblbalqty)
                                    grow.Cells(colFinalQty).Value = dblorderqty
                                End If
                                Exit For
                            End If
                            'End If
                        End If
                    Next
                    RadPageView1.SelectedPage = RadPageViewPage2
                Else
                    Throw New Exception(" Increase/Decrease Qty not more then Limit ( " & clsCommon.myCstr(LimitIncreaseDecreseQty) & " )")
                End If
            End If
            updateAll()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isInsideLoadData = False
        End Try
    End Sub
    Public Sub updateAll()
        Try
            Dim totalDQty As Double = 0
            Dim totalAQty As Double = 0
            Dim totalFQty As Double = 0
            isInsideLoadData = True
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
            Throw New Exception(ex.Message)
        Finally
            isInsideLoadData = False
        End Try

    End Sub
    Private Sub btnDecrease1_Click(sender As Object, e As EventArgs) Handles btnDecrease1.Click
        Dim dbltotalqty As Integer = 0
        Dim dblperqty As Integer = 0
        Dim dblbalqty As Integer = 0
        Dim dblorderqty As Integer = 0
        'Dim totalDQty As Double = 0
        'Dim totalAQty As Double = 0
        'Dim totalFQty As Double = 0
        Try
            isInsideLoadData = True
            DecreaseOrder = True
            IncraseOrder = False
            For Each grow As GridViewRowInfo In gv1.Rows
                grow.Cells(colAdjustedQty).Value = 0
                grow.Cells(colFinalQty).Value = clsCommon.myCdbl(grow.Cells(colDemandQty).Value) + 0
            Next
            If rbtnAutomatic.IsChecked Then
                If clsCommon.myCdbl(txtPer.Text) <= LimitIncreaseDecresePer Then
                    For Each grow As GridViewRowInfo In gv1.Rows
                        If clsCommon.myCdbl(txtQty.Text) > 0 AndAlso clsCommon.myLen(grow.Cells(colRouteCode).Value) > 0 Then
                            'If clsCommon.myLen(grow.Cells(colRouteCode).Value) > 0 Then
                            dblperqty = clsCommon.myCdbl(grow.Cells(colDemandQty).Value) * (clsCommon.myCdbl(txtPer.Text) / 100)
                            dblorderqty = clsCommon.myCdbl(grow.Cells(colDemandQty).Value) - dblperqty
                            If dbltotalqty = 0 AndAlso clsCommon.myCdbl(txtQty.Text) < dblperqty Then
                                'If clsCommon.myCdbl(txtQty.Text) < dblperqty Then
                                dblbalqty = clsCommon.myCdbl(txtQty.Text)
                                If dblbalqty > 0 Then
                                    grow.Cells(colAdjustedQty).Value = dblbalqty
                                    If (clsCommon.myCdbl(grow.Cells(colDemandQty).Value) - dblbalqty) > 0 Then
                                        grow.Cells(colFinalQty).Value = clsCommon.myCdbl(grow.Cells(colDemandQty).Value) - dblbalqty

                                    End If
                                End If
                                Exit For
                                'End If
                            End If
                            If (clsCommon.myCdbl(txtQty.Text)) <= dbltotalqty Then
                                dblbalqty = dbltotalqty - clsCommon.myCdbl(txtQty.Text)
                                If dblbalqty > 0 Then
                                    grow.Cells(colAdjustedQty).Value = dblbalqty
                                    If (clsCommon.myCdbl(grow.Cells(colDemandQty).Value) - dblbalqty) > 0 Then
                                        grow.Cells(colFinalQty).Value = clsCommon.myCdbl(grow.Cells(colDemandQty).Value) - dblbalqty

                                    End If
                                End If
                                Exit For
                            Else
                                If (clsCommon.myCdbl(txtQty.Text)) <= (dbltotalqty + dblperqty) Then
                                    dblbalqty = clsCommon.myCdbl(txtQty.Text) - dbltotalqty
                                    If dblbalqty > 0 Then
                                        grow.Cells(colAdjustedQty).Value = dblbalqty
                                        If (clsCommon.myCdbl(grow.Cells(colDemandQty).Value) - dblbalqty) > 0 Then
                                            grow.Cells(colFinalQty).Value = clsCommon.myCdbl(grow.Cells(colDemandQty).Value) - dblbalqty

                                        End If
                                    End If
                                    Exit For
                                End If
                                grow.Cells(colAdjustedQty).Value = dblperqty
                                grow.Cells(colFinalQty).Value = dblorderqty
                                dbltotalqty += dblperqty
                            End If
                            'If (clsCommon.myCdbl(txtQty.Text)) <= dbltotalqty Then
                            '    dblbalqty = dbltotalqty - clsCommon.myCdbl(txtQty.Text)
                            '    If dblbalqty > 0 Then
                            '        grow.Cells(colAdjustedQty).Value = dblbalqty
                            '        grow.Cells(colFinalQty).Value = clsCommon.myCdbl(grow.Cells(colDemandQty).Value) - dblbalqty
                            '    End If
                            '    Exit For
                            'Else
                            '    dbltotalqty += dblperqty
                            '    grow.Cells(colAdjustedQty).Value = dblperqty
                            '    grow.Cells(colFinalQty).Value = dblorderqty
                            'End If
                            'End If
                        End If
                    Next
                    RadPageView1.SelectedPage = RadPageViewPage2
                Else
                    Throw New Exception(" Increase/Decrease percentage not more then Limit ( " + clsCommon.myCstr(LimitIncreaseDecresePer) + " )")
                End If
            ElseIf rbtnFix.IsChecked Then
                If clsCommon.myCdbl(txtQty.Text) <= LimitIncreaseDecreseQty Then
                    For Each grow As GridViewRowInfo In gv1.Rows
                        If clsCommon.myCdbl(txtQty.Text) > 0 AndAlso clsCommon.myLen(grow.Cells(colRouteCode).Value) > 0 Then
                            'If clsCommon.myLen(grow.Cells(colRouteCode).Value) > 0 Then
                            If clsCommon.myCdbl(txtFixedQty.Text) >= (dbltotalqty + clsCommon.myCdbl(txtQty.Text)) Then
                                dblorderqty = clsCommon.myCdbl(grow.Cells(colDemandQty).Value) - clsCommon.myCdbl(txtQty.Text)
                                grow.Cells(colAdjustedQty).Value = clsCommon.myCdbl(txtQty.Text)
                                grow.Cells(colFinalQty).Value = dblorderqty
                                If dblorderqty < 0 Then
                                    grow.Cells(colFinalQty).Value = 0
                                    grow.Cells(colAdjustedQty).Value = 0
                                End If
                                dbltotalqty += clsCommon.myCdbl(txtQty.Text)
                            Else
                                dblbalqty = clsCommon.myCdbl(txtFixedQty.Text) - dbltotalqty
                                If clsCommon.myCdbl(txtQty.Text) >= dblbalqty Then
                                    dblorderqty = clsCommon.myCdbl(grow.Cells(colDemandQty).Value) - clsCommon.myCdbl(dblbalqty)
                                    grow.Cells(colAdjustedQty).Value = clsCommon.myCdbl(dblbalqty)
                                    If dblorderqty > 0 Then
                                        grow.Cells(colFinalQty).Value = dblorderqty

                                    End If
                                End If
                                Exit For
                            End If
                            'End If
                        End If
                    Next
                    RadPageView1.SelectedPage = RadPageViewPage2
                Else
                    Throw New Exception(" Increase/Decrease Qty not more then Limit ( " & clsCommon.myCstr(LimitIncreaseDecreseQty) & " )")
                End If
            End If
            updateAll()
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
            lblChangeitemDesc.Text = clsDBFuncationality.getSingleValue("select Short_Description  from TSPL_ITEM_MASTER where Item_Code='" & txtChangeItemCode.Value & "'")
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    '    Private Sub btnChange_Click(sender As Object, e As EventArgs)
    '        Dim totalDQty As Double = 0
    '        Dim totalAQty As Double = 0
    '        Dim totalFQty As Double = 0
    '        Try
    '            isInsideLoadData = True
    '            Dim whrcls As String = " where 2=2 and TSPL_DEMAND_BOOKING_MASTER.Posted=0 "
    '            Dim qry As String = ""
    '            Dim mainQry As String = " select TSPL_ROUTE_MASTER.Zone_Code,TSPL_DEMAND_BOOKING_MASTER.Route_No,TSPL_DEMAND_BOOKING_MASTER.Location_Code,TSPL_DEMAND_BOOKING_DETAIL.Cust_Code,TSPL_CUSTOMER_MASTER.Customer_Name,TSPL_DEMAND_BOOKING_DETAIL.Item_Code,TSPL_DEMAND_BOOKING_DETAIL.TR_Code,TSPL_ITEM_MASTER.Short_Description,TSPL_DEMAND_BOOKING_DETAIL.Unit_code,TSPL_DEMAND_BOOKING_DETAIL.Qty
    'from TSPL_DEMAND_BOOKING_MASTER 
    'left join TSPL_DEMAND_BOOKING_DETAIL on TSPL_DEMAND_BOOKING_MASTER.Document_No=TSPL_DEMAND_BOOKING_DETAIL.Document_No
    'left join TSPL_CUSTOMER_MASTER on TSPL_DEMAND_BOOKING_DETAIL.Cust_Code=TSPL_CUSTOMER_MASTER.Cust_Code
    'left join TSPL_ROUTE_MASTER on TSPL_DEMAND_BOOKING_MASTER.Route_No=TSPL_ROUTE_MASTER.Route_No
    'left join TSPL_ITEM_MASTER on TSPL_DEMAND_BOOKING_DETAIL.Item_Code=TSPL_ITEM_MASTER.Item_Code"
    '            whrcls += "  and convert(date,TSPL_DEMAND_BOOKING_MASTER.Document_Date,103)='" + clsCommon.GetPrintDate(txtDemandDate.Value) + "'  and TSPL_DEMAND_BOOKING_DETAIL.Unit_code='" + txtUOM.Value + "' and TSPL_DEMAND_BOOKING_DETAIL.Qty>='" + txtMinCrate.Text + "' "
    '            If txtZoneCode.arrValueMember IsNot Nothing Then
    '                If txtRouteCode.arrValueMember IsNot Nothing Then
    '                    whrcls += " and TSPL_DEMAND_BOOKING_MASTER.Route_No in('" + clsCommon.GetMulcallStringWithComma(txtRouteCode.arrValueMember) + "')"
    '                Else
    '                    whrcls += " and TSPL_DEMAND_BOOKING_MASTER.Route_No in(select Route_No from TSPL_ROUTE_MASTER where Zone_Code in(" + clsCommon.GetMulcallStringWithComma(txtZoneCode.arrValueMember) + ") and Zone_Code is not null)"
    '                End If
    '            Else
    '                If txtRouteCode.arrValueMember IsNot Nothing Then
    '                    whrcls += " and TSPL_DEMAND_BOOKING_MASTER.Route_No in('" + clsCommon.GetMulcallStringWithComma(txtRouteCode.arrValueMember) + "')"
    '                End If
    '            End If
    '            If rbtnMorning.IsChecked Then
    '                whrcls += " and TSPL_DEMAND_BOOKING_DETAIL.ShiftType='Morning' "
    '            Else
    '                whrcls += " and TSPL_DEMAND_BOOKING_DETAIL.ShiftType='Evening' "
    '            End If
    '            whrcls += " and TSPL_DEMAND_BOOKING_DETAIL.Cust_Code in(select TSPL_DEMAND_BOOKING_DETAIL.Cust_Code from TSPL_DEMAND_BOOKING_MASTER
    'left join TSPL_DEMAND_BOOKING_DETAIL on TSPL_DEMAND_BOOKING_MASTER.Document_No=TSPL_DEMAND_BOOKING_DETAIL.Document_No " + whrcls + " and TSPL_DEMAND_BOOKING_DETAIL.Item_Code ='" + txtItemCode.Value + "' ) and TSPL_DEMAND_BOOKING_DETAIL.Item_Code in('" + txtItemCode.Value + "','" + txtChangeItemCode.Value + "')"
    '            mainQry += whrcls
    '            qry = " select XX.Zone_Code,xx.Route_No,xx.Cust_Code,xx.Customer_Name,xx.Item_Code,xx.TR_Code,xx.Short_Description,xx.Unit_code,xx.Qty,
    'case when xx.Item_Code='" + txtItemCode.Value + "' then CAST(" + clsCommon.myCstr(txtDeductQty.Text) + " AS int) else (case when xx.Item_Code='" + txtChangeItemCode.Value + "' then CAST(" + clsCommon.myCstr(txtAddQty.Text) + " AS int) else 0 end ) end as AdjustQty,
    'case when xx.Item_Code='" + txtItemCode.Value + "' then (xx.Qty - CAST(" + clsCommon.myCstr(txtDeductQty.Text) + " AS int)) else (case when xx.Item_Code='" + txtChangeItemCode.Value + "' then (xx.Qty+ CAST(" + clsCommon.myCstr(txtAddQty.Text) + " AS int)) else 0 end ) end as FinalQty
    'from(" + mainQry + " )XX  order by xx.Qty desc"
    '            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
    '            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
    '                Dim introw As Integer = 0
    '                For Each dr As DataRow In dt.Rows
    '                    gv1.Rows(introw).Cells(ColSNo).Value = introw + 1
    '                    gv1.Rows(introw).Cells(colZoneCode).Value = clsCommon.myCstr(dr("Zone_Code"))
    '                    gv1.Rows(introw).Cells(colRouteCode).Value = clsCommon.myCstr(dr("Route_No"))
    '                    gv1.Rows(introw).Cells(colLocationCode).Value = clsCommon.myCstr(dr("Location_Code"))
    '                    gv1.Rows(introw).Cells(colBoothCode).Value = clsCommon.myCstr(dr("Cust_Code"))
    '                    gv1.Rows(introw).Cells(colBoothName).Value = clsCommon.myCstr(dr("Customer_Name"))
    '                    gv1.Rows(introw).Cells(colItemcode).Value = clsCommon.myCstr(dr("Item_Code"))
    '                    gv1.Rows(introw).Cells(colTRCode).Value = clsCommon.myCstr(dr("TR_Code"))
    '                    gv1.Rows(introw).Cells(colItemDesc).Value = clsCommon.myCstr(dr("Short_Description"))
    '                    gv1.Rows(introw).Cells(colUOM).Value = clsCommon.myCstr(dr("Unit_code"))
    '                    gv1.Rows(introw).Cells(colDemandQty).Value = clsCommon.myCdbl(dr("Qty"))
    '                    gv1.Rows(introw).Cells(colAdjustedQty).Value = clsCommon.myCdbl(dr("AdjustQty"))
    '                    gv1.Rows(introw).Cells(colFinalQty).Value = clsCommon.myCdbl(dr("FinalQty"))
    '                    totalDQty += clsCommon.myCdbl(gv1.Rows(introw).Cells(colDemandQty).Value)
    '                    totalAQty += clsCommon.myCdbl(gv1.Rows(introw).Cells(colAdjustedQty).Value)
    '                    totalFQty += clsCommon.myCdbl(gv1.Rows(introw).Cells(colFinalQty).Value)
    '                    gv1.Rows.AddNew()
    '                    introw += 1
    '                Next
    '                gv1.Rows(introw).Cells(colUOM).Value = "Total"
    '                gv1.Rows(introw).Cells(colDemandQty).Value = totalDQty
    '                gv1.Rows(introw).Cells(colAdjustedQty).Value = totalAQty
    '                gv1.Rows(introw).Cells(colFinalQty).Value = totalFQty
    '                EnableGroup(True)
    '                RadPageView1.SelectedPage = RadPageViewPage2
    '            Else
    '                Throw New Exception("Data Not Found!")
    '            End If
    '        Catch ex As Exception
    '            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
    '        Finally
    '            isInsideLoadData = False
    '        End Try
    '    End Sub
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
        coll.Add("FixedQty", "decimal(18,2) NULL")
        coll.Add("Deduct_Qty", "decimal(18,0) NULL")
        coll.Add("Change_Item_Code", "Varchar(50) NULL References TSPL_ITEM_MASTER(Item_Code)")
        coll.Add("Add_Qty", "decimal(18,0) NULL")
        coll.Add("Status", "int NULL")
        coll.Add("Modified_By", "varchar(20) NOT NULL")
        coll.Add("Modified_Date", "Datetime NOT NULL")
        coll.Add("Created_By", "varchar(20)  NULL ")
        coll.Add("Created_Date", "Datetime  NULL")
        coll.Add("Change_product_Type", "int  NULL")
        coll.Add("Change_Qty", "int  NULL")
        clsCommonFunctionality.CreateOrAlterTable(True, False, "TSPL_DEMAND_ADJUSTMENT_HEAD", coll, Nothing, True, False, "", "Document_Code", "", True)
        coll = New Dictionary(Of String, String)()
        coll.Add("Document_Code", "VARCHAR(30) NOT NULL REFERENCES TSPL_DEMAND_ADJUSTMENT_HEAD(Document_Code) ")
        coll.Add("TR_Code", "varchar(30) Not NULL")
        coll.Add("Zone_Code", "varchar(30) Not NULL")
        coll.Add("Route_Code", "varchar(12)  NOT NULL REFERENCES TSPL_ROUTE_MASTER (Route_No)")
        coll.Add("Location_Code", "VARCHAR(50) NULL")
        coll.Add("Booth_Code", "varchar(12) NULL references TSPL_CUSTOMER_MASTER(Cust_Code)")
        coll.Add("Item_Code", "Varchar(50) NULL References TSPL_ITEM_MASTER(Item_Code)")
        coll.Add("Unit_Code", "varchar(12) null REFERENCES TSPL_UNIT_MASTER (Unit_Code)")
        coll.Add("Demand_Qty", "Decimal(18,0) Not NULL")
        coll.Add("Adjust_Qty", "Decimal(18,0) Not NULL")
        coll.Add("Final_Qty", "Decimal(18,0) Not NULL")
        coll.Add("TotalCrates_ItemWise", "Decimal(18,2) NULL")
        coll.Add("TotalLtr_ItemWise", "Decimal(18,2) NULL")
        coll.Add("Item_Rate", "Decimal(18,6) NULL")
        coll.Add("ItemNetAmount", "Decimal(18,2) NULL")
        coll.Add("TAX_Group", "varchar(30) NULL")
        coll.Add("TAX1", "varchar(30) NULL")
        coll.Add("TAX1_Rate", "Decimal(18,2) NULL")
        coll.Add("TAX1_Amt", "Decimal(18,2) NULL")
        coll.Add("TAX1_Base_Amt", "Decimal(18,2) NULL")
        coll.Add("TAX2", "varchar(30) NULL")
        coll.Add("TAX2_Rate", "Decimal(18,2) NULL")
        coll.Add("TAX2_Amt", "Decimal(18,2) NULL")
        coll.Add("TAX2_Base_Amt", "Decimal(18,2) NULL")
        coll.Add("TAX3", "varchar(30) NULL")
        coll.Add("TAX3_Rate", "Decimal(18,2) NULL")
        coll.Add("TAX3_Amt", "Decimal(18,2) NULL")
        coll.Add("TAX3_Base_Amt", "Decimal(18,2) NULL")
        coll.Add("TAX4", "varchar(30) NULL")
        coll.Add("TAX4_Rate", "Decimal(18,2) NULL")
        coll.Add("TAX4_Amt", "Decimal(18,2) NULL")
        coll.Add("TAX4_Base_Amt", "Decimal(18,2) NULL")
        coll.Add("TAX5", "varchar(30) NULL")
        coll.Add("TAX5_Rate", "Decimal(18,2) NULL")
        coll.Add("TAX5_Amt", "Decimal(18,2) NULL")
        coll.Add("TAX5_Base_Amt", "Decimal(18,2) NULL")
        coll.Add("TAX6", "varchar(30) NULL")
        coll.Add("TAX6_Rate", "Decimal(18,2) NULL")
        coll.Add("TAX6_Amt", "Decimal(18,2) NULL")
        coll.Add("TAX6_Base_Amt", "Decimal(18,2) NULL")
        coll.Add("TAX7", "varchar(30) NULL")
        coll.Add("TAX7_Rate", "Decimal(18,2) NULL")
        coll.Add("TAX7_Amt", "Decimal(18,2) NULL")
        coll.Add("TAX7_Base_Amt", "Decimal(18,2) NULL")
        coll.Add("TAX8", "varchar(30) NULL")
        coll.Add("TAX8_Rate", "Decimal(18,2) NULL")
        coll.Add("TAX8_Amt", "Decimal(18,2) NULL")
        coll.Add("TAX8_Base_Amt", "Decimal(18,2) NULL")
        coll.Add("TAX9", "varchar(30) NULL")
        coll.Add("TAX9_Rate", "Decimal(18,2) NULL")
        coll.Add("TAX9_Amt", "Decimal(18,2) NULL")
        coll.Add("TAX9_Base_Amt", "Decimal(18,2) NULL")
        coll.Add("TAX10", "varchar(30) NULL")
        coll.Add("TAX10_Rate", "Decimal(18,2) NULL")
        coll.Add("TAX10_Amt", "Decimal(18,2) NULL")
        coll.Add("TAX10_Base_Amt", "Decimal(18,2) NULL")

        clsCommonFunctionality.CreateOrAlterTable(True, False, "TSPL_DEMAND_ADJUSTMENT_DETAIL", coll, "", True, False, "TSPL_DEMAND_ADJUSTMENT_HEAD", "Document_Code", "", True)

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
            SaveData()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Public Sub SaveData()
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
                If rbtnAutomatic.IsChecked Then
                    obj.Percentage = clsCommon.myCdbl(txtPer.Text)
                ElseIf rbtnFix.IsChecked Then
                    obj.FixedQty = clsCommon.myCdbl(txtFixedQty.Text)
                End If
                obj.Deduct_Qty = 0
                obj.Add_Qty = 0
                If clsCommon.myLen(txtChangeItemCode.Value) > 0 AndAlso chkChangeItem.Checked Then
                    obj.Change_Item_Code = txtChangeItemCode.Value
                    If rbtnAll.IsChecked Then
                        obj.Change_product_Type = 0
                    ElseIf rbtnChangebyPer.IsChecked Then
                        obj.Change_product_Type = 1
                        obj.Change_Qty = txtChangeQty.Text
                    ElseIf rbtnchangeFixQty.IsChecked Then
                        obj.Change_product_Type = 2
                        obj.Change_Qty = txtChangeQty.Text

                    End If
                End If

                obj.Arr = New List(Of clsDemandAdjustmentDetail)
                For Each grow As GridViewRowInfo In gv1.Rows
                    If clsCommon.myLen(grow.Cells(colRouteCode).Value) > 0 Then
                        Dim objTr As New clsDemandAdjustmentDetail()
                        objTr.TR_Code = clsCommon.myCstr(grow.Cells(colTRCode).Value)
                        objTr.Zone_Code = clsCommon.myCstr(grow.Cells(colZoneCode).Value)
                        objTr.Route_Code = clsCommon.myCstr(grow.Cells(colRouteCode).Value)
                        objTr.Location_Code = clsCommon.myCstr(grow.Cells(colLocationCode).Value)
                        objTr.Booth_Code = clsCommon.myCstr(grow.Cells(colBoothCode).Value)
                        objTr.Item_Code = clsCommon.myCstr(grow.Cells(colItemcode).Value)
                        objTr.Unit_Code = clsCommon.myCstr(grow.Cells(colUOM).Value)
                        objTr.Demand_Qty = clsCommon.myCdbl(grow.Cells(colDemandQty).Value)
                        objTr.Adjust_Qty = clsCommon.myCdbl(grow.Cells(colAdjustedQty).Value)
                        objTr.Final_Qty = clsCommon.myCdbl(grow.Cells(colFinalQty).Value)
                        If clsCommon.CompairString(objTr.Unit_Code, "Crate") = CompairStringResult.Equal Then
                            objTr.TotalCrates_ItemWise = clsCommon.myCdbl(grow.Cells(colFinalQty).Value)
                        Else
                            Dim dblTotalCrateRowWise As Double = 0
                            Dim ItemCrateType As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select IS_CrateType  from TSPL_ITEM_MASTER Where Item_Code  ='" & clsCommon.myCstr(objTr.Item_Code) & "'"))
                            If ItemCrateType = 1 Then
                                'Dim IsStockingUnit As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Stocking_Unit from TSPL_ITEM_UOM_DETAIL Left Outer Join tspl_unit_master on tspl_unit_master.Unit_Code = TSPL_ITEM_UOM_DETAIL.UOM_Code Where TSPL_ITEM_UOM_DETAIL.Item_Code ='" & clsCommon.myCstr(objTr.Item_Code) & "' and TSPL_ITEM_UOM_DETAIL.UOM_Code  ='" & clsCommon.myCstr(objTr.Unit_Code) & "'"))
                                Dim CrateConvFactor As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL Left Outer Join tspl_unit_master on tspl_unit_master.Unit_Code = TSPL_ITEM_UOM_DETAIL.UOM_Code Where TSPL_ITEM_UOM_DETAIL.Item_Code ='" & clsCommon.myCstr(objTr.Item_Code) & "' and tspl_unit_master.Crate_Type ='Y' "))
                                Dim ItemConvFactor As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL Left Outer Join tspl_unit_master on tspl_unit_master.Unit_Code = TSPL_ITEM_UOM_DETAIL.UOM_Code Where TSPL_ITEM_UOM_DETAIL.Item_Code ='" & clsCommon.myCstr(objTr.Item_Code) & "' and TSPL_ITEM_UOM_DETAIL.UOM_Code ='" & clsCommon.myCstr(objTr.Unit_Code) & "' "))
                                If clsCommon.CompairString(objCommonVar.CurrComp_Code1, "JPR") <> CompairStringResult.Equal AndAlso CrateConvFactor > 0 AndAlso ItemConvFactor > 0 Then
                                    'If CrateConvFactor > 0 AndAlso ItemConvFactor > 0 Then
                                    Dim DispatchQty As Double = clsCommon.myCdbl(objTr.Final_Qty) * ItemConvFactor

                                    If DispatchQty > (CrateConvFactor / 2) Then
                                        dblTotalCrateRowWise = Math.Ceiling(DispatchQty / CrateConvFactor)
                                    Else
                                        dblTotalCrateRowWise = 0
                                    End If

                                    'End If
                                End If
                                objTr.TotalCrates_ItemWise = clsCommon.myCdbl(dblTotalCrateRowWise)
                            End If

                        End If
                        ''to convert into litre
                        Dim dblTotalLitreRowWise As Double = 0
                        Dim CrateConvFactor_Ltr As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL Left Outer Join tspl_unit_master on tspl_unit_master.Unit_Code = TSPL_ITEM_UOM_DETAIL.UOM_Code Where TSPL_ITEM_UOM_DETAIL.Item_Code ='" & clsCommon.myCstr(objTr.Item_Code) & "' and TSPL_ITEM_UOM_DETAIL.UOM_Code='Ltr' "))
                        Dim ItemConvFactor_Ltr As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL Left Outer Join tspl_unit_master on tspl_unit_master.Unit_Code = TSPL_ITEM_UOM_DETAIL.UOM_Code Where TSPL_ITEM_UOM_DETAIL.Item_Code ='" & clsCommon.myCstr(objTr.Item_Code) & "' and TSPL_ITEM_UOM_DETAIL.UOM_Code ='" & clsCommon.myCstr(objTr.Unit_Code) & "' "))
                        If CrateConvFactor_Ltr > 0 AndAlso ItemConvFactor_Ltr > 0 Then
                            Dim DispatchQty As Double = clsCommon.myCdbl(objTr.Final_Qty) * ItemConvFactor_Ltr
                            dblTotalLitreRowWise = (DispatchQty / CrateConvFactor_Ltr)
                        End If
                        objTr.TotalLtr_ItemWise = clsCommon.myCdbl(dblTotalLitreRowWise)
                        Dim objItemRates As New clsDemandAdjustmentDetail()
                        objItemRates = getItemRate(objTr.Booth_Code, objTr.Item_Code, objTr.Unit_Code, objTr.Location_Code, txtDemandDate.Value, objTr.Final_Qty)
                        objTr.Item_Rate = objItemRates.Item_Rate
                        objTr.ItemNetAmount = objItemRates.ItemNetAmount
                        objTr.TAX_Group = objItemRates.TAX_Group
                        objTr.TAX1 = objItemRates.TAX1
                        objTr.TAX1_Rate = objItemRates.TAX1_Rate
                        objTr.TAX1_Amt = objItemRates.TAX1_Amt
                        objTr.TAX1_Base_Amt = objItemRates.TAX1_Base_Amt
                        objTr.TAX2 = objItemRates.TAX2
                        objTr.TAX2_Rate = objItemRates.TAX2_Rate
                        objTr.TAX2_Amt = objItemRates.TAX2_Amt
                        objTr.TAX2_Base_Amt = objItemRates.TAX2_Base_Amt
                        objTr.TAX3 = objItemRates.TAX3
                        objTr.TAX3_Rate = objItemRates.TAX3_Rate
                        objTr.TAX3_Amt = objItemRates.TAX3_Amt
                        objTr.TAX3_Base_Amt = objItemRates.TAX3_Base_Amt
                        objTr.TAX4 = objItemRates.TAX4
                        objTr.TAX4_Rate = objItemRates.TAX4_Rate
                        objTr.TAX4_Amt = objItemRates.TAX4_Amt
                        objTr.TAX4_Base_Amt = objItemRates.TAX4_Base_Amt
                        objTr.TAX5 = objItemRates.TAX5
                        objTr.TAX5_Rate = objItemRates.TAX5_Rate
                        objTr.TAX5_Amt = objItemRates.TAX5_Amt
                        objTr.TAX5_Base_Amt = objItemRates.TAX5_Base_Amt
                        objTr.TAX6 = objItemRates.TAX6
                        objTr.TAX6_Rate = objItemRates.TAX6_Rate
                        objTr.TAX6_Amt = objItemRates.TAX6_Amt
                        objTr.TAX6_Base_Amt = objItemRates.TAX6_Base_Amt
                        objTr.TAX7 = objItemRates.TAX7
                        objTr.TAX7_Rate = objItemRates.TAX7_Rate
                        objTr.TAX7_Amt = objItemRates.TAX7_Amt
                        objTr.TAX7_Base_Amt = objItemRates.TAX7_Base_Amt
                        objTr.TAX8 = objItemRates.TAX8
                        objTr.TAX8_Rate = objItemRates.TAX8_Rate
                        objTr.TAX8_Amt = objItemRates.TAX8_Amt
                        objTr.TAX8_Base_Amt = objItemRates.TAX8_Base_Amt
                        objTr.TAX9 = objItemRates.TAX9
                        objTr.TAX9_Rate = objItemRates.TAX9_Rate
                        objTr.TAX9_Amt = objItemRates.TAX9_Amt
                        objTr.TAX9_Base_Amt = objItemRates.TAX9_Base_Amt
                        objTr.TAX10 = objItemRates.TAX10
                        objTr.TAX10_Rate = objItemRates.TAX10_Rate
                        objTr.TAX10_Amt = objItemRates.TAX10_Amt
                        objTr.TAX10_Base_Amt = objItemRates.TAX10_Base_Amt

                        ''---------end of litre conversion
                        obj.Arr.Add(objTr)
                    End If
                Next
                If (obj.SaveData(obj, isNewEntry)) Then
                        clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                        LoadData(obj.Document_Code, NavigatorType.Current)
                    End If
                End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
    Public Function getItemRate(ByVal strCustCode As String, ByVal strItemCode As String, ByVal strUnitCode As String, ByVal strLocation As String, ByVal strdate As DateTime, ByVal itemQty As Double) As clsDemandAdjustmentDetail
        'Dim dblRate As Double = 0
        Dim strPriceCode As String = ""
        Dim obj As clsDemandAdjustmentDetail = New clsDemandAdjustmentDetail()
        Dim qry As String = "select tspl_customer_master.price_CodeNon from TSPL_CUSTOMER_MASTER where Cust_Code='" & clsCommon.myCstr(strCustCode) & "'"
        strPriceCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
        If clsCommon.myLen(strPriceCode) <= 0 Then
            Throw New Exception("price_CodeNon not found for Customer " & clsCommon.myCstr(strCustCode) & "")
        End If

        qry = " Select Is_With_Tax, RowNo, Item_Price_ID, XXXE.Item_Code, UOM, Start_Date, Item_Basic_Price,Item_Basic_Net,Price_Code,Item_Selling_Price, XXXE.Tax_group,XXXE.TAX1_Rate, " &
    " XXXE.TAX2_Rate,XXXE.TAX3_Rate,XXXE.TAX4_Rate,XXXE.TAX5_Rate, " &
    "  XXXE.TAX6_Rate,XXXE.TAX7_Rate,XXXE.TAX8_Rate,XXXE.TAX9_Rate, " &
    " XXXE.TAX10_Rate,XXXE.TAX1 ,XXXE.TAX2,XXXE.TAX3, " &
    " XXXE.TAX4,XXXE.TAX5,XXXE.TAX6,XXXE.TAX7, " &
    " XXXE.TAX8,XXXE.TAX9,XXXE.TAX10,XXXE.TAX1_Amt, " &
    " XXXE.TAX2_Amt,XXXE.TAX3_Amt,XXXE.TAX4_Amt, XXXE.TAX5_Amt,XXXE.TAX6_Amt,XXXE.TAX7_Amt,XXXE.TAX8_Amt,XXXE.TAX9_Amt,XXXE.TAX10_Amt,XXXE.Against_Plan_TR_Code  from ( " &
    "Select ROW_NUMBER() OVER (Partition By TSPL_ITEM_PRICE_MASTER.Item_Code ORDER BY TSPL_ITEM_PRICE_MASTER.Item_Code,  " &
    "Start_Date Desc) as RowNo,Is_With_Tax, Item_Price_ID, TSPL_ITEM_PRICE_MASTER.Item_Code, UOM, Start_Date,  " &
    "Item_Basic_Price,Item_Basic_Net,Price_Code,Item_Selling_Price, TSPL_ITEM_PRICE_MASTER.Tax_group,TSPL_ITEM_PRICE_MASTER.TAX1_Rate,  " &
    "TSPL_ITEM_PRICE_MASTER.TAX2_Rate,TSPL_ITEM_PRICE_MASTER.TAX3_Rate,TSPL_ITEM_PRICE_MASTER.TAX4_Rate,TSPL_ITEM_PRICE_MASTER.TAX5_Rate,  " &
    " TSPL_ITEM_PRICE_MASTER.TAX6_Rate, TSPL_ITEM_PRICE_MASTER.TAX7_Rate, TSPL_ITEM_PRICE_MASTER.TAX8_Rate, TSPL_ITEM_PRICE_MASTER.TAX9_Rate, " &
    " TSPL_ITEM_PRICE_MASTER.TAX10_Rate, TSPL_ITEM_PRICE_MASTER.TAX1, TSPL_ITEM_PRICE_MASTER.TAX2, TSPL_ITEM_PRICE_MASTER.TAX3, " &
    " TSPL_ITEM_PRICE_MASTER.TAX4, TSPL_ITEM_PRICE_MASTER.TAX5, TSPL_ITEM_PRICE_MASTER.TAX6, TSPL_ITEM_PRICE_MASTER.TAX7, " &
    " TSPL_ITEM_PRICE_MASTER.TAX8,TSPL_ITEM_PRICE_MASTER.TAX9,TSPL_ITEM_PRICE_MASTER.TAX10,TSPL_ITEM_PRICE_MASTER.TAX1_Amt , TSPL_ITEM_PRICE_MASTER.TAX2_Amt ,TSPL_ITEM_PRICE_MASTER.TAX3_Amt ,TSPL_ITEM_PRICE_MASTER.TAX4_Amt,TSPL_ITEM_PRICE_MASTER.TAX5_Amt,TSPL_ITEM_PRICE_MASTER.TAX6_Amt,TSPL_ITEM_PRICE_MASTER.TAX7_Amt,   TSPL_ITEM_PRICE_MASTER.TAX8_Amt,TSPL_ITEM_PRICE_MASTER.TAX9_Amt,TSPL_ITEM_PRICE_MASTER.TAX10_Amt,TSPL_ITEM_PRICE_MASTER.Against_Plan_TR_Code from TSPL_ITEM_PRICE_MASTER  left  outer join  " &
    "TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_PRICE_MASTER.Item_Code=TSPL_ITEM_UOM_DETAIL.Item_Code and  " &
    "TSPL_ITEM_PRICE_MASTER.UOM=TSPL_ITEM_UOM_DETAIL.UOM_Code   where  Start_Date<='" & clsCommon.GetPrintDate(strdate, "dd/MMM/yyyy") & "'  and (End_Date >= '" & clsCommon.GetPrintDate(strdate, "dd/MMM/yyyy") & "'  or End_date is null)  and  " &
    "TSPL_ITEM_PRICE_MASTER.Price_Code='" & strPriceCode & "' and UOM='" & strUnitCode & "' and TSPL_ITEM_PRICE_MASTER.item_code='" & strItemCode & "' AND Location_Code='" & clsCommon.myCstr(strLocation) & "'  " &
    ") XXXE WHERE RowNo=1  "

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            obj.Item_Rate = clsCommon.myCdbl(dt.Rows(0).Item("Item_Basic_Price"))
            obj.ItemNetAmount = itemQty * obj.Item_Rate
            obj.TAX_Group = clsCommon.myCstr(dt.Rows(0).Item("TAX_Group"))
            obj.TAX1 = clsCommon.myCstr(dt.Rows(0).Item("TAX1"))
            If clsCommon.CompairString(obj.TAX1, "TCS") = CompairStringResult.Equal Then
                obj.TAX1_Rate = CalculateTCS(clsCommon.myCstr(strCustCode))
            Else
                obj.TAX1_Rate = clsCommon.myCdbl(dt.Rows(0).Item("TAX1_Rate"))
            End If
            obj.TAX1_Amt = Math.Round(obj.ItemNetAmount * (obj.TAX1_Rate / 100), 2)
            obj.TAX1_Base_Amt = obj.ItemNetAmount
            obj.TAX2 = clsCommon.myCstr(dt.Rows(0).Item("Tax2"))
            If clsCommon.CompairString(obj.TAX2, "TCS") = CompairStringResult.Equal Then
                obj.TAX2_Rate = CalculateTCS(clsCommon.myCstr(strCustCode))
            Else
                obj.TAX2_Rate = clsCommon.myCdbl(dt.Rows(0).Item("Tax2_Rate"))
            End If
            obj.TAX2_Amt = Math.Round(obj.ItemNetAmount * (obj.TAX2_Rate / 100), 2)
            obj.TAX2_Base_Amt = obj.ItemNetAmount
            obj.TAX3 = clsCommon.myCstr(dt.Rows(0).Item("Tax3"))
            If clsCommon.CompairString(obj.TAX3, "TCS") = CompairStringResult.Equal Then
                obj.TAX3_Rate = CalculateTCS(clsCommon.myCstr(strCustCode))
            Else
                obj.TAX3_Rate = clsCommon.myCdbl(dt.Rows(0).Item("Tax3_Rate"))
            End If
            obj.TAX3_Amt = Math.Round(obj.ItemNetAmount * (obj.TAX3_Rate / 100), 2)
            obj.TAX3_Base_Amt = obj.ItemNetAmount
            obj.TAX4 = clsCommon.myCstr(dt.Rows(0).Item("Tax4"))
            If clsCommon.CompairString(obj.TAX4, "TCS") = CompairStringResult.Equal Then
                obj.TAX4_Rate = CalculateTCS(clsCommon.myCstr(strCustCode))
            Else
                obj.TAX4_Rate = clsCommon.myCdbl(dt.Rows(0).Item("Tax4_Rate"))
            End If
            obj.TAX4_Amt = Math.Round(obj.ItemNetAmount * (obj.TAX4_Rate / 100), 2)
            obj.TAX4_Base_Amt = obj.ItemNetAmount
            obj.TAX5 = clsCommon.myCstr(dt.Rows(0).Item("Tax5"))
            If clsCommon.CompairString(obj.TAX5, "TCS") = CompairStringResult.Equal Then
                obj.TAX5_Rate = CalculateTCS(clsCommon.myCstr(strCustCode))
            Else
                obj.TAX5_Rate = clsCommon.myCdbl(dt.Rows(0).Item("Tax5_Rate"))
            End If
            obj.TAX5_Amt = Math.Round(obj.ItemNetAmount * (obj.TAX5_Rate / 100), 2)
            obj.TAX5_Base_Amt = obj.ItemNetAmount
            obj.TAX6 = clsCommon.myCstr(dt.Rows(0).Item("Tax6"))
            If clsCommon.CompairString(obj.TAX6, "TCS") = CompairStringResult.Equal Then
                obj.TAX6_Rate = CalculateTCS(clsCommon.myCstr(strCustCode))
            Else
                obj.TAX6_Rate = clsCommon.myCdbl(dt.Rows(0).Item("Tax6_Rate"))
            End If
            obj.TAX6_Amt = Math.Round(obj.ItemNetAmount * (obj.TAX6_Rate / 100), 2)
            obj.TAX6_Base_Amt = obj.ItemNetAmount
            obj.TAX7 = clsCommon.myCstr(dt.Rows(0).Item("Tax7"))
            If clsCommon.CompairString(obj.TAX7, "TCS") = CompairStringResult.Equal Then
                obj.TAX7_Rate = CalculateTCS(clsCommon.myCstr(strCustCode))
            Else
                obj.TAX7_Rate = clsCommon.myCdbl(dt.Rows(0).Item("Tax7_Rate"))
            End If
            obj.TAX7_Amt = Math.Round(obj.ItemNetAmount * (obj.TAX7_Rate / 100), 2)
            obj.TAX7_Base_Amt = obj.ItemNetAmount
            obj.TAX8 = clsCommon.myCstr(dt.Rows(0).Item("Tax8"))
            If clsCommon.CompairString(obj.TAX8, "TCS") = CompairStringResult.Equal Then
                obj.TAX8_Rate = CalculateTCS(clsCommon.myCstr(strCustCode))
            Else
                obj.TAX8_Rate = clsCommon.myCdbl(dt.Rows(0).Item("Tax8_Rate"))
            End If
            obj.TAX8_Amt = Math.Round(obj.ItemNetAmount * (obj.TAX8_Rate / 100), 2)
            obj.TAX8_Base_Amt = obj.ItemNetAmount
            obj.TAX9 = clsCommon.myCstr(dt.Rows(0).Item("Tax9"))
            If clsCommon.CompairString(obj.TAX9, "TCS") = CompairStringResult.Equal Then
                obj.TAX9_Rate = CalculateTCS(clsCommon.myCstr(strCustCode))
            Else
                obj.TAX9_Rate = clsCommon.myCdbl(dt.Rows(0).Item("Tax9_Rate"))
            End If
            obj.TAX9_Amt = Math.Round(obj.ItemNetAmount * (obj.TAX9_Rate / 100), 2)
            obj.TAX9_Base_Amt = obj.ItemNetAmount
            obj.TAX10 = clsCommon.myCstr(dt.Rows(0).Item("Tax10"))
            If clsCommon.CompairString(obj.TAX10, "TCS") = CompairStringResult.Equal Then
                obj.TAX10_Rate = CalculateTCS(clsCommon.myCstr(strCustCode))
            Else
                obj.TAX10_Rate = clsCommon.myCdbl(dt.Rows(0).Item("Tax10_Rate"))
            End If
            obj.TAX10_Amt = Math.Round(obj.ItemNetAmount * (obj.TAX10_Rate / 100), 2)
            obj.TAX10_Base_Amt = obj.ItemNetAmount
        End If
        Return obj
    End Function
    Function CalculateTCS(ByVal CustCode As String) As Double
        Dim TCSBaseAmount As Double = 0
        Dim TCSTaxRate As Double = 0

        Dim balanceAmt As Double = 0
        Dim OPInvoice_Sale_Amt As Double = 0
        Dim CurrFinYR As String = String.Empty
        Dim FinancialYear As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT CASE WHEN DatePart(Month, '" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") & "') >= 4 THEN DatePart(Year, '" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") & "') & 1 ELSE DatePart(Year, '" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") & "') END AS Fiscal_Year"))
        Dim strStartDate As Date = "01/Apr/" & clsCommon.myCstr(clsCommon.myCdbl(FinancialYear - 1))
        Dim strEndDate As Date = "31/Mar/" & FinancialYear
        CurrFinYR = clsCommon.myCstr(clsCommon.myCdbl(FinancialYear - 1)) & "-" & FinancialYear
        TCSBaseAmount = 0
        Dim strqry As String = "select sum(ItemNetAmount) from TSPL_DEMAND_BOOKING_MASTER left join TSPL_DEMAND_BOOKING_DETAIL on TSPL_DEMAND_BOOKING_MASTER.Document_No=TSPL_DEMAND_BOOKING_DETAIL.Document_No where TSPL_DEMAND_BOOKING_DETAIL.Cust_Code='" & clsCommon.myCstr(CustCode) & "' and convert(date, TSPL_DEMAND_BOOKING_MASTER.Document_Date,103)>='" & clsCommon.GetPrintDate(strStartDate) & "' and convert(date, TSPL_DEMAND_BOOKING_MASTER.Document_Date,103)<='" & clsCommon.GetPrintDate(strEndDate) & "' "
        balanceAmt = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(strqry))
        OPInvoice_Sale_Amt = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Sale_amt from TSPL_OP_INVOICE_FOR_TCS where Customer_Code='" & clsCommon.myCstr(CustCode) & "' and Financial_Year_Code='" & clsCommon.myCstr(CurrFinYR) & "'"))
        TCSBaseAmount = OPInvoice_Sale_Amt + balanceAmt
        If AmountToCheckCustomerOutstandingForTCSTax > 0 Then
            If TCSBaseAmount > AmountToCheckCustomerOutstandingForTCSTax Then

                If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.TCSRateforCustomerWithPanNo, clsFixedParameterCode.TCSRateforCustomerWithPanNo, Nothing)) = 1 Then
                    Dim Is_ITR_Filled_And_TCSAmountGreater50K As Boolean = IIf(clsCommon.myCstr(clsDBFuncationality.getSingleValue(" SELECT CASE WHEN ISNULL(IsTCSGreaterthan50K,0)=1 AND ISNULL(IsITRfilledinLast2Years,0)=1 THEN 1 ELSE 0 END FROM TSPL_CUSTOMER_MASTER WHERE Cust_Code='" & clsCommon.myCstr(CustCode) & "'")) = 1, True, False)
                    If Is_ITR_Filled_And_TCSAmountGreater50K Then
                        TCSTaxRate = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.TCSRateforCustomerWithPanNo, clsFixedParameterCode.TCSRateforCustomerWithPanNo, Nothing))
                    Else
                        TCSTaxRate = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.TCSRateforCustomerWithoutPanNo, clsFixedParameterCode.TCSRateforCustomerWithoutPanNo, Nothing))
                    End If
                Else
                    Dim panno As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(pan,'')+isnull(Additional3 ,'') as PanNoAdhar from tspl_customer_master where cust_code='" & clsCommon.myCstr(CustCode) & "'"))
                    If clsCommon.myLen(panno) > 0 Then
                        TCSTaxRate = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.TCSRateforCustomerWithPanNo, clsFixedParameterCode.TCSRateforCustomerWithPanNo, Nothing))
                    Else
                        TCSTaxRate = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.TCSRateforCustomerWithoutPanNo, clsFixedParameterCode.TCSRateforCustomerWithoutPanNo, Nothing))
                    End If
                End If
            Else
                TCSTaxRate = 0
            End If
        Else
            TCSTaxRate = 0
        End If
        Return TCSTaxRate
    End Function

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Try
            Dim obj As New clsDemandAdjustment
            'Dim intRow As Integer
            obj = clsDemandAdjustment.GetData(strCode, NavTyep)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Document_Code) > 0) Then
                AddNew()
                If isChange Then
                    EnableGroup(False)
                    btnGo.Enabled = False
                    chkChangeItem.Enabled = False
                    rgbChangeProduct.Enabled = False
                Else
                    EnableGroup(True)

                End If
                txtDate.Enabled = False
                txtDemandDate.Enabled = False
                If obj.Status = 1 Then
                    btnSave.Enabled = False
                    btnDelete.Enabled = False
                    btnProceed.Enabled = False
                    chkChangeItem.Enabled = False
                    rgbChangeProduct.Enabled = False

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
                    Dim substrings As String() = obj.Zone_Code.Split(delimiter)
                    Dim zoneList As New ArrayList()
                    For Each substring As String In substrings
                        zoneList.Add(substring)
                    Next
                    txtZoneCode.arrValueMember = zoneList
                End If
                If obj.Route_Code IsNot Nothing Then
                    Dim delimiter As Char = ","c
                    Dim substrings As String() = obj.Route_Code.Split(delimiter)
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
                lblItemDesc.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Short_Description  from TSPL_ITEM_MASTER where Item_Code='" & obj.Item_Code & "'"))
                txtUOM.Value = obj.Unit_Code
                lblUOMDesc.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select UOM_Description from TSPL_ITEM_UOM_DETAIL where UOM_Code='" & obj.Unit_Code & "' and Item_Code='" & obj.Item_Code & "'"))
                chkChangeItem.Checked = obj.Is_Change_Product
                txtMinCrate.Text = obj.Minimum_Qty
                rbtnAutomatic.IsChecked = obj.Is_Automatic
                rbtnFix.IsChecked = obj.Is_FixQty
                txtQty.Text = obj.Increase_Decrease_Qty
                If rbtnAutomatic.IsChecked Then
                    txtPer.Text = obj.Percentage
                ElseIf rbtnFix.IsChecked Then
                    txtFixedQty.Text = obj.FixedQty
                End If
                If chkChangeItem.Checked Then
                    txtChangeItemCode.Value = obj.Change_Item_Code
                    lblChangeitemDesc.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Short_Description  from TSPL_ITEM_MASTER where Item_Code='" & obj.Change_Item_Code & "'"))
                    If obj.Change_product_Type = 0 Then
                        rbtnAll.IsChecked = True
                    ElseIf obj.Change_product_Type = 1 Then
                        rbtnChangebyPer.IsChecked = True
                        txtChangeQty.Text = obj.Change_Qty
                    ElseIf obj.Change_product_Type = 2 Then
                        rbtnchangeFixQty.IsChecked = True
                        txtChangeQty.Text = obj.Change_Qty
                    End If
                End If

                'txtAddQty.Text = 0
                If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                    Dim dblrows As Integer = 0
                    Dim dblDtotal As Integer = 0
                    Dim dblAtotal As Integer = 0
                    Dim dblFtotal As Integer = 0
                    For Each objTr As clsDemandAdjustmentDetail In obj.Arr
                        gv1.Rows(dblrows).Cells(ColSNo).Value = dblrows + 1
                        gv1.Rows(dblrows).Cells(colZoneCode).Value = objTr.Zone_Code
                        gv1.Rows(dblrows).Cells(colTRCode).Value = objTr.TR_Code
                        gv1.Rows(dblrows).Cells(colLocationCode).Value = objTr.Location_Code
                        gv1.Rows(dblrows).Cells(colRouteCode).Value = objTr.Route_Code
                        gv1.Rows(dblrows).Cells(colBoothCode).Value = objTr.Booth_Code
                        gv1.Rows(dblrows).Cells(colBoothName).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Customer_Name  from TSPL_CUSTOMER_MASTER where CUST_CODE='" & objTr.Booth_Code & "'"))
                        gv1.Rows(dblrows).Cells(colItemcode).Value = objTr.Item_Code
                        gv1.Rows(dblrows).Cells(colItemDesc).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Short_Description  from TSPL_ITEM_MASTER where Item_Code='" & objTr.Item_Code & "'"))
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
            Dim qry As String = "select count(*) from TSPL_DEMAND_ADJUSTMENT_HEAD where Document_Code='" & txtDocNo.Value & "' "
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
            Dim whrClas As String = ""
            If isChange Then
                whrClas = " TSPL_DEMAND_ADJUSTMENT_HEAD.Is_Change_Product=1"
            Else
                whrClas = " TSPL_DEMAND_ADJUSTMENT_HEAD.Is_Change_Product=0"

            End If
            Reset()
            LoadData(clsCommon.ShowSelectForm("FSBook1DocNo", qry, "DocumentCode", whrClas, txtDocNo.Value, "Document_date DESC", isButtonClicked, " TSPL_DEMAND_ADJUSTMENT_HEAD.Document_date "), NavigatorType.Current)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub btnProceed_Click(sender As Object, e As EventArgs) Handles btnProceed.Click
        Try
            If common.clsCommon.MyMessageBoxShow("Do you want to Proceed this record?", "", MessageBoxButtons.YesNo, RadMessageIcon.Question) = DialogResult.Yes Then
                If clsCommon.myLen(txtDocNo.Value) > 0 Then
                    If clsDemandAdjustment.ProceedDemand(txtDocNo.Value, isChange) Then
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
            If clsCommon.myLen(txtDocNo.Value) > 0 AndAlso (myMessages.deleteConfirm()) AndAlso clsDemandAdjustment.DeleteData(txtDocNo.Value) Then
                'If (myMessages.deleteConfirm()) Then
                'If clsDemandAdjustment.DeleteData(txtDocNo.Value) Then
                clsCommon.MyMessageBoxShow(Me, "Delete Successfully", Me.Text)
                        AddNew()
                '    End If
                'End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub gv1_CellValueChanged(sender As Object, e As GridViewCellEventArgs) Handles gv1.CellValueChanged
        Try
            If (Not isInsideLoadData) Then
                If Not isCellValueChangedOpen Then
                    isCellValueChangedOpen = True
                    If e.Column Is gv1.Columns(colAdjustedQty) Then
                        If IncraseOrder Then
                            If clsCommon.myCdbl(gv1.CurrentRow.Cells(colAdjustedQty).Value) > 0 Then
                                Dim dblorderQty As Double = clsCommon.myCdbl(gv1.CurrentRow.Cells(colDemandQty).Value) + clsCommon.myCdbl(gv1.CurrentRow.Cells(colAdjustedQty).Value)
                                gv1.CurrentRow.Cells(colFinalQty).Value = dblorderQty
                            End If
                        ElseIf DecreaseOrder Then
                            If clsCommon.myCdbl(gv1.CurrentRow.Cells(colAdjustedQty).Value) > 0 Then
                                Dim dblorderQty As Double = clsCommon.myCdbl(gv1.CurrentRow.Cells(colDemandQty).Value) + clsCommon.myCdbl(gv1.CurrentRow.Cells(colAdjustedQty).Value)
                                gv1.CurrentRow.Cells(colFinalQty).Value = dblorderQty
                            End If
                        Else
                            gv1.CurrentRow.Cells(colAdjustedQty).Value = 0
                        End If

                        updateAll()
                    End If
                End If
                isCellValueChangedOpen = False
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isCellValueChangedOpen = False
        End Try
    End Sub

    Private Sub btnHistory_Click(sender As Object, e As EventArgs) Handles btnHistory.Click
        Try
            If clsCommon.myLen(txtDocNo.Value) <= 0 Then
                clsCommon.MyMessageBoxShow("Select Document No")
                Exit Sub
            End If
            clsERPFuncationalityOLD.ShowTransHistoryData(txtDocNo.Value, "Document_Code", "TSPL_DEMAND_ADJUSTMENT_HEAD", "TSPL_TTSPL_DEMAND_ADJUSTMENT_DETAIL")
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    'Private Sub frmDemandAdjustment_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
    '    If e.Alt AndAlso e.KeyCode = Keys.C Then
    '        Dim frm As New FrmPWD(Nothing)
    '        frm.strType = clsFixedParameterType.SIR
    '        frm.strCode = clsFixedParameterCode.SIReversAndCreate
    '        frm.ShowDialog()
    '        If frm.isPasswordCorrect Then
    '            isChange = True
    '            AddNew()
    '        End If
    '    End If
    'End Sub

    Private Sub rbtnAll_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles rbtnAll.ToggleStateChanged
        If rbtnAll.IsChecked Then
            rbtnAll.IsChecked = True
            rbtnChangebyPer.IsChecked = False
            rbtnchangeFixQty.IsChecked = False
            lblEnterQty.Visible = False
            txtChangeQty.Text = 0
            txtChangeQty.Visible = False
        End If
    End Sub

    Private Sub rbtnChangebyPer_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles rbtnChangebyPer.ToggleStateChanged
        If rbtnChangebyPer.IsChecked Then
            rbtnAll.IsChecked = False
            rbtnChangebyPer.IsChecked = True
            rbtnchangeFixQty.IsChecked = False
            lblEnterQty.Visible = True
            lblEnterQty.Text = "Enter Qty in Percentage"
            txtChangeQty.Text = 0
            txtChangeQty.Visible = True
        End If
    End Sub

    Private Sub rbtnchangeFixQty_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles rbtnchangeFixQty.ToggleStateChanged
        If rbtnchangeFixQty.IsChecked Then
            rbtnAll.IsChecked = False
            rbtnChangebyPer.IsChecked = False
            rbtnchangeFixQty.IsChecked = True
            lblEnterQty.Visible = True
            lblEnterQty.Text = "Enter Fixed Qty"
            txtChangeQty.Text = 0
            txtChangeQty.Visible = True
        End If
    End Sub
End Class
