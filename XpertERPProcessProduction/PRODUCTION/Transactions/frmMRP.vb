Imports common
Imports System.Data.SqlClient
Imports System.IO

Public Class frmMRP
    Inherits FrmMainTranScreen
    '' PO Tab Grid columns

    Const colPurchaseOrder_No As String = "colPurchaseOrder_No"
    Const colPOBill_To_Location As String = "colPOBill_To_Location"
    Const colPOVendor_Code As String = "colPOVendor_Code"
    Const colPurchaseOrder_Date As String = "colPurchaseOrder_Date"
    Const colPOVendor_Name As String = "colPOVendor_Name"
    
    '' Pending SRN grid columns
    Const colSRN_No As String = "colSRN_No"
    Const colSRN_Date As String = "colSRN_Date"
    Const colSRNBill_To_Location As String = "colSRNBill_To_Location"
    Const colSRNVendor_Code As String = "colSRNVendor_Code"
    Const colSRNVendor_Name As String = "colSRNVendor_Name"

    '' MRP Detail grid 
    Const colItem_Code As String = "colItem_Code"
    Const colItem_Desc As String = "colItem_Desc"
    Const colRM_UNIT_CODE As String = "colRM_UNIT_CODE"

    Const colOpening_Qty As String = "colOpening_Qty"
    Const colPO_Qty As String = "colPO_Qty"
    Const colSRN_Qty As String = "colSRN_Qty"
    Const colTotal_Avail_Qty As String = "colTotal_Avail_Qty"
    Const colTotal_Requird_Qty As String = "colTotal_Requird_Qty"
    Const colNet_Requird_Qty As String = "colNet_Requird_Qty"
    Const colCost As String = "colCost"
    Const colTotalCost As String = "colTotalCost"

    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim isNewEntry As Boolean = True

    Dim obj As New clsMRP
    'Private ObjList As New List(Of clsMRP)
    Private isCellValueChangedOpen As Boolean = False
    Dim OpenFileDialog1 As New OpenFileDialog

    Sub LoadPOGrid()

        gvPO.Rows.Clear()
        gvPO.Columns.Clear()

        Dim PurchaseOrder_No As New GridViewTextBoxColumn
        Dim Bill_To_Location As New GridViewTextBoxColumn
        Dim Vendor_Code As New GridViewTextBoxColumn
        Dim Vendor_Name As New GridViewTextBoxColumn
        Dim PurchaseOrder_Date As New GridViewDateTimeColumn

        PurchaseOrder_No.FormatString = ""
        PurchaseOrder_No.HeaderText = "PO No"
        PurchaseOrder_No.Name = colPurchaseOrder_No
        PurchaseOrder_No.Width = 100
        PurchaseOrder_No.ReadOnly = True
        PurchaseOrder_No.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvPO.Columns.Add(PurchaseOrder_No)

        Bill_To_Location.FormatString = ""
        Bill_To_Location.HeaderText = "Billing Location"
        Bill_To_Location.Name = colPOBill_To_Location
        Bill_To_Location.Width = 100
        Bill_To_Location.ReadOnly = True
        Bill_To_Location.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvPO.Columns.Add(Bill_To_Location)

        Vendor_Code.FormatString = ""
        Vendor_Code.HeaderText = "Vendor Code"
        Vendor_Code.Name = colPOVendor_Code
        Vendor_Code.Width = 100
        Vendor_Code.ReadOnly = True
        Vendor_Code.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvPO.Columns.Add(Vendor_Code)

        PurchaseOrder_Date.FormatString = ""
        PurchaseOrder_Date.HeaderText = "PO Date"
        PurchaseOrder_Date.Name = colPurchaseOrder_Date
        PurchaseOrder_Date.Width = 120
        PurchaseOrder_Date.ReadOnly = True
        PurchaseOrder_Date.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvPO.Columns.Add(PurchaseOrder_Date)

        Vendor_Name.FormatString = ""
        Vendor_Name.HeaderText = "Vendor Name"
        Vendor_Name.Name = colPOVendor_Name
        Vendor_Name.Width = 120
        Vendor_Name.ReadOnly = True
        Vendor_Name.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvPO.Columns.Add(Vendor_Name)

    End Sub
    Sub LoadSRNGrid()

        gvSRN.Rows.Clear()
        gvSRN.Columns.Clear()

        Dim SRNNo As New GridViewTextBoxColumn
        Dim srnDate As New GridViewDateTimeColumn
        Dim BillingLocation As New GridViewTextBoxColumn
        Dim Vendor_Code As New GridViewTextBoxColumn
        Dim Vendor_Name As New GridViewTextBoxColumn
       
        SRNNo.FormatString = ""
        SRNNo.HeaderText = "SRN No"
        SRNNo.Name = colSRN_No
        SRNNo.Width = 100
        SRNNo.ReadOnly = True
        SRNNo.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvSRN.Columns.Add(SRNNo)

        srnDate.FormatString = ""
        srnDate.HeaderText = "SRN Date"
        srnDate.Name = colSRN_Date
        srnDate.Width = 100
        srnDate.ReadOnly = True
        srnDate.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvSRN.Columns.Add(srnDate)

        BillingLocation.FormatString = ""
        BillingLocation.HeaderText = "Billing Location"
        BillingLocation.Name = colSRNBill_To_Location
        BillingLocation.Width = 100
        BillingLocation.ReadOnly = True
        BillingLocation.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvSRN.Columns.Add(BillingLocation)

        Vendor_Code.FormatString = ""
        Vendor_Code.HeaderText = "Vendor Code"
        Vendor_Code.Name = colSRNVendor_Code
        Vendor_Code.Width = 120
        Vendor_Code.ReadOnly = True
        Vendor_Code.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvSRN.Columns.Add(Vendor_Code)

        Vendor_Name.FormatString = ""
        Vendor_Name.HeaderText = "Vendor Name"
        Vendor_Name.Name = colSRNVendor_Name
        Vendor_Name.Width = 120
        Vendor_Name.ReadOnly = True
        Vendor_Name.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvSRN.Columns.Add(Vendor_Name)

    End Sub
    Sub LoadMRPDetailGrid()

        gvMRPDetal.Rows.Clear()
        gvMRPDetal.Columns.Clear()

        Dim Item_Code As New GridViewTextBoxColumn
        Dim Item_Desc As New GridViewTextBoxColumn
        Dim RM_UNIT_CODE As New GridViewTextBoxColumn

        Dim Opening_Qty As New GridViewDecimalColumn
        Dim PO_Qty As New GridViewDecimalColumn
        Dim SRN_Qty As New GridViewDecimalColumn
        Dim Total_Avail_Qty As New GridViewDecimalColumn
        Dim Total_Requird_Qty As New GridViewDecimalColumn
        Dim Net_Requird_Qty As New GridViewDecimalColumn
        Dim Cost As New GridViewDecimalColumn
        Dim TotalCost As New GridViewDecimalColumn

        Item_Code.FormatString = ""
        Item_Code.HeaderText = "Item Code"
        Item_Code.Name = colItem_Code
        Item_Code.Width = 100
        Item_Code.ReadOnly = True
        Item_Code.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvMRPDetal.Columns.Add(Item_Code)

        Item_Desc.FormatString = ""
        Item_Desc.HeaderText = "Item Description"
        Item_Desc.Name = colItem_Desc
        Item_Desc.Width = 100
        Item_Desc.ReadOnly = True
        Item_Desc.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvMRPDetal.Columns.Add(Item_Desc)

        RM_UNIT_CODE.FormatString = ""
        RM_UNIT_CODE.HeaderText = "UOM"
        RM_UNIT_CODE.Name = colRM_UNIT_CODE
        RM_UNIT_CODE.Width = 100
        RM_UNIT_CODE.ReadOnly = True
        RM_UNIT_CODE.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvMRPDetal.Columns.Add(RM_UNIT_CODE)

        Opening_Qty.FormatString = ""
        Opening_Qty.HeaderText = "Opening Balance"
        Opening_Qty.Name = colOpening_Qty
        Opening_Qty.Width = 100
        Opening_Qty.ReadOnly = True
        Opening_Qty.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvMRPDetal.Columns.Add(Opening_Qty)

        PO_Qty.FormatString = ""
        PO_Qty.HeaderText = "PO Qty"
        PO_Qty.Name = colPO_Qty
        PO_Qty.Width = 120
        PO_Qty.ReadOnly = True
        PO_Qty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvMRPDetal.Columns.Add(PO_Qty)

        SRN_Qty.FormatString = ""
        SRN_Qty.HeaderText = "Pending SRN Qty"
        SRN_Qty.Name = colSRN_Qty
        SRN_Qty.Width = 120
        SRN_Qty.ReadOnly = True
        SRN_Qty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvMRPDetal.Columns.Add(SRN_Qty)

        Total_Avail_Qty.FormatString = ""
        Total_Avail_Qty.HeaderText = "Total Available Qty"
        Total_Avail_Qty.Name = colTotal_Avail_Qty
        Total_Avail_Qty.Width = 120
        Total_Avail_Qty.ReadOnly = True
        Total_Avail_Qty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvMRPDetal.Columns.Add(Total_Avail_Qty)

        Total_Requird_Qty.FormatString = ""
        Total_Requird_Qty.HeaderText = "Total Required Qty"
        Total_Requird_Qty.Name = colTotal_Requird_Qty
        Total_Requird_Qty.Width = 120
        Total_Requird_Qty.ReadOnly = True
        Total_Requird_Qty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvMRPDetal.Columns.Add(Total_Requird_Qty)

        Net_Requird_Qty.FormatString = ""
        Net_Requird_Qty.HeaderText = "Net Required Qty"
        Net_Requird_Qty.Name = colNet_Requird_Qty
        Net_Requird_Qty.Width = 120
        Net_Requird_Qty.ReadOnly = True
        Net_Requird_Qty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvMRPDetal.Columns.Add(Net_Requird_Qty)

        Cost.FormatString = ""
        Cost.HeaderText = "Cost"
        Cost.Name = colCost
        Cost.Width = 120
        Cost.ReadOnly = True
        Cost.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        Cost.IsVisible = False
        gvMRPDetal.Columns.Add(Cost)

        TotalCost.FormatString = ""
        TotalCost.HeaderText = "Total Cost"
        TotalCost.Name = colTotalCost
        TotalCost.Width = 120
        TotalCost.ReadOnly = True
        TotalCost.IsVisible = False
        TotalCost.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvMRPDetal.Columns.Add(TotalCost)
    End Sub

    Private Sub frmMRP_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnnew.Enabled Then
            funReset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnsave.Enabled Then
            Save()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btndelete.Enabled Then
            DeleteData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            funClose()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            funReset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag AndAlso btnPost.Enabled Then
            PostData()
        ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F12 Then
            If MyBase.isReverse Then

                Dim frm As New FrmPWD(Nothing)
                frm.strType = "SIRC"
                frm.strCode = "SIReversAndCreate"
                frm.ShowDialog()
                If frm.isPasswordCorrect Then
                    btnUnpost.Visible = True
                End If
            Else
                clsCommon.MyMessageBoxShow(Me, "You are not authorized to perform this action.", Me.Text, MessageBoxButtons.OK, Telerik.WinControls.RadMessageIcon.Error)
                'MessageBox.Show("You are not authorized to perform this action.", "Unauthorized Access", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        End If
    End Sub
    Sub funClose()
        Me.Close()
    End Sub

    Private Sub frmMRP_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        LoadPOGrid()
        LoadSRNGrid()
        LoadMRPDetailGrid()
        isNewEntry = True
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(btnPost, "Press Alt+P for  Post")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D  for Delete ")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnnew, "Press Alt+N Adding New ")
        ButtonToolTip.SetToolTip(btnRequisition, "Press Alt+R for Print Preview")

        funReset()
        btnUnpost.Visible = False
        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
    End Sub
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmMRP)
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow("Permission Denied")
            Me.Close()
            Exit Sub
        End If
        btnsave.Visible = MyBase.isModifyFlag
        btnPost.Visible = MyBase.isPostFlag
        btndelete.Visible = MyBase.isDeleteFlag
        btnUnpost.Visible = False
        'If MyBase.isReverse Then
        '    btnUnpost.Enabled = True
        'Else
        '    btnUnpost.Enabled = False
        'End If
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        funClose()
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            funReset()
        Catch ex As Exception
        End Try
    End Sub

    Sub funReset()
        isNewEntry = True
        Dim serverdate As Date = clsCommon.GETSERVERDATE
        txtCode.MyReadOnly = False
        txtCode.Value = Nothing
        txtCode.Focus()
        UsLock1.Status = ERPTransactionStatus.Pending
        Me.fndProductionPlan.Value = Nothing
        Me.txtProductionPlanDesc.Text = ""
        dtpMRPDate.Value = serverdate
        dtpFromDate.Value = serverdate

        dtpToDate.Value = serverdate
        lblNoOfDays.Text = 1

        fndItemToProduce.Value = Nothing
        fndBOM.Value = Nothing
        fndLocation.Value = Nothing
        Me.txtBuildQty.Text = 0
        Me.lblUnitName.Text = ""
        Me.lblBOMDesc.Text = ""
        Me.lblLocationDesc.Text = ""
        Me.txtDescription.Text = ""
        gvPO.Rows.Clear()
        gvSRN.Rows.Clear()
        gvMRPDetal.Rows.Clear()

    End Sub

    Private Sub txtCode__MYNavigator(ByVal sender As Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType)
        Try
            LoadData(txtCode.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        funReset()
        txtCode.MyReadOnly = True
        btnsave.Enabled = True
        btndelete.Enabled = True
        obj = clsMRP.GetData(strCode, NavTyep)
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.MRP_CODE) > 0) Then
            isNewEntry = False
            btnsave.Text = "Update"
            If obj.POSTED Then
                btnsave.Enabled = False
                btnPost.Enabled = False
                btndelete.Enabled = False
                UsLock1.Status = ERPTransactionStatus.Approved
            Else
                btnsave.Enabled = True
                btndelete.Enabled = True
                btnPost.Enabled = True
                UsLock1.Status = ERPTransactionStatus.Pending
            End If
            Dim ii As Int16 = 0

            '' general tab
            Me.txtCode.Value = obj.MRP_CODE
            Me.dtpMRPDate.Value = obj.MRP_DATE
            Me.dtpFromDate.Value = obj.MRP_FROM
            Me.dtpToDate.Value = obj.MRP_TO
            Me.lblNoOfDays.Text = obj.MRP_DAYS
            Me.txtMRPDescription.Text = obj.MRP_DESCRIPTION
            Me.txtDescription.Text = obj.MRP_REMARKS
            Me.fndItemToProduce.Value = obj.Item_Code
            Me.fndBOM.Value = obj.BOM_CODE
            Me.txtBuildQty.Text = obj.MRP_QTY
            Me.lblUnitName.Text = obj.MRP_ITEM_UNIT_CODE
            Me.txtPackSize.Text = obj.PACK_SIZE
            Me.fndLocation.Value = obj.MRP_Location
            Me.fndProductionPlan.Value = obj.PROD_PLAN_CODE
            txtProductionPlanDesc.Text = obj.PROD_PLAN_DESC
            '' display salary
            gvPO.Rows.Clear()
            gvMRPDetal.Rows.Clear()
            gvSRN.Rows.Clear()

            If obj.ObjListPO IsNot Nothing And obj.ObjListPO.Count > 0 Then
                For Each objPO As clsMRPPO In obj.ObjListPO
                    gvPO.Rows.AddNew()

                    gvPO.Rows(gvPO.Rows.Count - 1).Cells(colPurchaseOrder_No).Value = objPO.PurchaseOrder_No
                    gvPO.Rows(gvPO.Rows.Count - 1).Cells(colPOBill_To_Location).Value = objPO.Bill_To_Location
                    gvPO.Rows(gvPO.Rows.Count - 1).Cells(colPOVendor_Code).Value = objPO.Vendor_Code
                    gvPO.Rows(gvPO.Rows.Count - 1).Cells(colPurchaseOrder_Date).Value = objPO.PurchaseOrder_Date
                    gvPO.Rows(gvPO.Rows.Count - 1).Cells(colPOVendor_Name).Value = objPO.Vendor_Name
                Next
            End If
            If obj.ObjListMRPSRN IsNot Nothing And obj.ObjListMRPSRN.Count > 0 Then
                For Each objSRN As clsMRPSRN In obj.ObjListMRPSRN
                    gvSRN.Rows.AddNew()

                    gvSRN.Rows(gvSRN.Rows.Count - 1).Cells(colSRN_No).Value = objSRN.SRN_No
                    gvSRN.Rows(gvSRN.Rows.Count - 1).Cells(colSRNBill_To_Location).Value = objSRN.Bill_To_Location
                    gvSRN.Rows(gvSRN.Rows.Count - 1).Cells(colSRNVendor_Code).Value = objSRN.Vendor_Code
                    gvSRN.Rows(gvSRN.Rows.Count - 1).Cells(colSRN_Date).Value = objSRN.SRN_Date
                    gvSRN.Rows(gvSRN.Rows.Count - 1).Cells(colSRNVendor_Name).Value = objSRN.Vendor_Name
                Next
            End If

            '' total variables
            Dim totalOpening As Decimal = 0
            Dim totalRequiredQty As Decimal = 0
            Dim totalPO_Qty As Decimal = 0
            Dim totalSRN_Qty As Decimal = 0
            Dim totalNetReqQty As Decimal = 0
            Dim totalAvailQty As Decimal = 0
            Dim cost As Decimal = 0
            Dim totalCost As Decimal = 0
            If obj.ObjListMRPDetail IsNot Nothing And obj.ObjListMRPDetail.Count > 0 Then
                
                For Each objMRPDetail As clsMRPDetail In obj.ObjListMRPDetail
                    gvMRPDetal.Rows.AddNew()
                    gvMRPDetal.Rows(gvMRPDetal.Rows.Count - 1).Cells(colItem_Code).Value = objMRPDetail.Item_Code
                    gvMRPDetal.Rows(gvMRPDetal.Rows.Count - 1).Cells(colItem_Desc).Value = objMRPDetail.Item_Desc
                    gvMRPDetal.Rows(gvMRPDetal.Rows.Count - 1).Cells(colRM_UNIT_CODE).Value = objMRPDetail.RM_UNIT_CODE

                    gvMRPDetal.Rows(gvMRPDetal.Rows.Count - 1).Cells(colOpening_Qty).Value = objMRPDetail.Opening_Qty
                    gvMRPDetal.Rows(gvMRPDetal.Rows.Count - 1).Cells(colPO_Qty).Value = objMRPDetail.PO_Qty
                    gvMRPDetal.Rows(gvMRPDetal.Rows.Count - 1).Cells(colSRN_Qty).Value = objMRPDetail.SRN_Qty
                    gvMRPDetal.Rows(gvMRPDetal.Rows.Count - 1).Cells(colTotal_Avail_Qty).Value = objMRPDetail.Total_Avail_Qty
                    gvMRPDetal.Rows(gvMRPDetal.Rows.Count - 1).Cells(colTotal_Requird_Qty).Value = objMRPDetail.Total_Requird_Qty
                    gvMRPDetal.Rows(gvMRPDetal.Rows.Count - 1).Cells(colNet_Requird_Qty).Value = objMRPDetail.Net_Requird_Qty
                    gvMRPDetal.Rows(gvMRPDetal.Rows.Count - 1).Cells(colCost).Value = objMRPDetail.COST
                    gvMRPDetal.Rows(gvMRPDetal.Rows.Count - 1).Cells(colTotalCost).Value = objMRPDetail.TOTAL_COST

                    totalRequiredQty = totalRequiredQty + objMRPDetail.Total_Requird_Qty
                    totalOpening = totalOpening + objMRPDetail.Opening_Qty
                    totalPO_Qty = totalPO_Qty + objMRPDetail.PO_Qty
                    totalSRN_Qty = totalSRN_Qty + objMRPDetail.SRN_Qty
                    totalNetReqQty = totalNetReqQty + objMRPDetail.Net_Requird_Qty
                    totalAvailQty = totalAvailQty + gvMRPDetal.Rows(gvMRPDetal.Rows.Count - 1).Cells(colTotal_Avail_Qty).Value
                    cost = cost + gvMRPDetal.Rows(gvMRPDetal.Rows.Count - 1).Cells(colCost).Value
                    totalCost = totalCost + gvMRPDetal.Rows(gvMRPDetal.Rows.Count - 1).Cells(colTotalCost).Value
                Next
                '' total 

                gvMRPDetal.Rows.AddNew()
                gvMRPDetal.Rows(gvMRPDetal.Rows.Count - 1).Cells(colItem_Code).Value = "Total :"

                gvMRPDetal.Rows(gvMRPDetal.Rows.Count - 1).Cells(colTotal_Requird_Qty).Value = totalRequiredQty
                gvMRPDetal.Rows(gvMRPDetal.Rows.Count - 1).Cells(colOpening_Qty).Value = totalOpening
                gvMRPDetal.Rows(gvMRPDetal.Rows.Count - 1).Cells(colPO_Qty).Value = totalPO_Qty
                gvMRPDetal.Rows(gvMRPDetal.Rows.Count - 1).Cells(colSRN_Qty).Value = totalSRN_Qty

                gvMRPDetal.Rows(gvMRPDetal.Rows.Count - 1).Cells(colNet_Requird_Qty).Value = totalNetReqQty
                gvMRPDetal.Rows(gvMRPDetal.Rows.Count - 1).Cells(colTotal_Avail_Qty).Value = totalAvailQty
                gvMRPDetal.Rows(gvMRPDetal.Rows.Count - 1).Cells(colCost).Value = cost
                gvMRPDetal.Rows(gvMRPDetal.Rows.Count - 1).Cells(colTotalCost).Value = totalCost
            End If
        End If

    End Sub

    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        SavingData(False)
    End Sub
    Public Function Save() As Boolean
        If AllowToSave() Then
            Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
            Dim issaved As Boolean
            Try
                Dim obj As New clsMRP
                obj.MRP_CODE = Me.txtCode.Value
                obj.MRP_DESCRIPTION = Me.txtMRPDescription.Text
                obj.MRP_DATE = Me.dtpMRPDate.Value
                obj.MRP_FROM = Me.dtpFromDate.Value
                obj.MRP_TO = Me.dtpToDate.Value
                obj.MRP_REMARKS = Me.txtDescription.Text
                obj.Item_Code = Me.fndItemToProduce.Value
                obj.BOM_CODE = Me.fndBOM.Value
                obj.MRP_Location = Me.fndLocation.Value
                obj.MRP_DAYS = Me.lblNoOfDays.Text
                obj.MRP_QTY = Me.txtBuildQty.Text
                obj.MRP_ITEM_UNIT_CODE = Me.lblUnitName.Text
                obj.PACK_SIZE = clsCommon.myCdbl(Me.txtPackSize.Text)
                obj.PROD_PLAN_CODE = clsCommon.myCstr(Me.fndProductionPlan.Value)

                '' saving po
                Dim objListPO As New List(Of clsMRPPO)
                Dim objPO As clsMRPPO
                For Each row As GridViewRowInfo In gvPO.Rows
                    If clsCommon.myLen(row.Cells(colPurchaseOrder_No).Value) > 0 Then
                        objPO = New clsMRPPO
                        objPO.MRP_CODE = Me.txtCode.Value
                        objPO.PurchaseOrder_No = row.Cells(colPurchaseOrder_No).Value
                        objPO.Bill_To_Location = row.Cells(colPOBill_To_Location).Value
                        objPO.Vendor_Code = row.Cells(colPOVendor_Code).Value
                        objPO.PurchaseOrder_Date = row.Cells(colPurchaseOrder_Date).Value
                        objPO.Vendor_Name = row.Cells(colPOVendor_Name).Value
                        objListPO.Add(objPO)
                    End If
                Next
                '' saving po
                Dim objListSRN As New List(Of clsMRPSRN)
                Dim objSRN As clsMRPSRN
                For Each row As GridViewRowInfo In gvSRN.Rows
                    If clsCommon.myLen(row.Cells(colSRN_No).Value) > 0 Then
                        objSRN = New clsMRPSRN
                        objSRN.MRP_CODE = Me.txtCode.Value
                        objSRN.SRN_No = row.Cells(colSRN_No).Value
                        objSRN.Bill_To_Location = row.Cells(colSRNBill_To_Location).Value
                        objSRN.Vendor_Code = row.Cells(colSRNVendor_Code).Value
                        objSRN.SRN_Date = row.Cells(colSRN_Date).Value
                        objSRN.Vendor_Name = row.Cells(colSRNVendor_Name).Value
                        objListSRN.Add(objSRN)
                    End If
                Next

                '' saving MRP Detail
                Dim objListMrp As New List(Of clsMRPDetail)
                Dim objMRP As clsMRPDetail
                For Each row As GridViewRowInfo In gvMRPDetal.Rows
                    If gvMRPDetal.Rows.IndexOf(row) = gvMRPDetal.Rows.Count - 1 Then
                        Exit For
                    End If
                    'If clsCommon.myLen(row.Cells(colSRN_No).Value) > 0 Then
                    objMRP = New clsMRPDetail
                    objMRP.MRP_CODE = Me.txtCode.Value
                    objMRP.Item_Code = row.Cells(colItem_Code).Value
                    objMRP.Item_Desc = row.Cells(colItem_Desc).Value
                    objMRP.RM_UNIT_CODE = row.Cells(colRM_UNIT_CODE).Value

                    objMRP.Opening_Qty = row.Cells(colOpening_Qty).Value
                    objMRP.PO_Qty = row.Cells(colPO_Qty).Value
                    objMRP.SRN_Qty = row.Cells(colSRN_Qty).Value
                    objMRP.Total_Avail_Qty = row.Cells(colTotal_Avail_Qty).Value
                    objMRP.Total_Requird_Qty = row.Cells(colTotal_Requird_Qty).Value
                    objMRP.Net_Requird_Qty = row.Cells(colNet_Requird_Qty).Value
                    objMRP.COST = row.Cells(colCost).Value
                    objMRP.TOTAL_COST = row.Cells(colTotalCost).Value
                    objListMrp.Add(objMRP)
                    'End If
                Next
                obj.ObjListPO = objListPO
                obj.ObjListMRPSRN = objListSRN
                obj.ObjListMRPDetail = objListMrp
                issaved = clsMRP.SaveData(obj, isNewEntry, trans, Me.txtCode.Value)
                trans.Commit()
                If issaved Then
                    LoadData(obj.MRP_CODE, NavigatorType.Current)
                    'clsCommon.MyMessageBoxShow("Document Saved Successfully.")
                    Return issaved
                Else
                    Return issaved
                End If
            Catch ex As Exception
                If issaved = False Then
                    trans.Rollback()
                End If
                clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
                Return False
            End Try
        Else
            Return False
        End If

        Return True
    End Function
    Sub SavePurchaseRequisitionData(ByVal ChekBtnPost As Boolean)
        Try
            'If (AllowToSave()) Then
            Dim isNewEntry As Boolean = False
            Dim obj As New clsRequistionHead()
            Dim objMRP As clsMRP = clsMRP.GetData(Me.txtCode.Value, NavigatorType.Current)
            If clsCommon.myLen(objMRP.REQUISITION_ID) <= 0 Then
                obj.Requisition_Id = ""
                isNewEntry = True
            Else
                obj.Requisition_Id = objMRP.REQUISITION_ID
                isNewEntry = False
            End If
            obj.Requisition_Date = dtpMRPDate.Value
            obj.Cust_OrderNo = ""

            obj.Ref_No = Me.txtCode.ValidateChildren
            obj.Description = Me.txtDescription.Text
            obj.Remarks = Me.txtDescription.Text
            obj.On_Hold = 0
            obj.Location = Me.fndLocation.Value
            obj.RQ_Detail_Total_Amt = 0 'clsCommon.myCdbl(lblTotRAmt.Text)
            obj.Total_RQ_Amt = 0 'clsCommon.myCdbl(lblTotRAmt.Text)
            obj.Mode_Of_Transport = ""
            obj.Comments = Me.txtDescription.Text
            obj.Is_Internal = "N"
            obj.Item_Type = "O"
            obj.Dept = ""
            obj.Dept_Desc = ""
            obj.Request_By = objCommonVar.CurrentUserCode
            obj.Requisition_Type = ""
            obj.PROJECT_ID = ""

            'Dim dt As DataTable = clsDBFuncationality.GetDataTable("Select Level1, Level2 from TSPL_REQUISITION_APPROVAL")
            'If dt.Rows.Count > 0 Then
            '    If clsCommon.myCdbl(lblTotRAmt.Text) <= clsCommon.myCdbl(dt.Rows(0)("Level1")) Then
            '        obj.Approvel_Level_Required = 1
            '    ElseIf clsCommon.myCdbl(lblTotRAmt.Text) > clsCommon.myCdbl(dt.Rows(0)("Level1")) And clsCommon.myCdbl(lblTotRAmt.Text) <= clsCommon.myCdbl(dt.Rows(0)("Level2")) Then
            '        obj.Approvel_Level_Required = 2
            '    Else
            '        obj.Approvel_Level_Required = 3
            '    End If
            'End If
            obj.Approvel_Level_Required = 1
            obj.ArrTr = New List(Of clsRequistionDetail)
            For Each grow As GridViewRowInfo In gvMRPDetal.Rows
                Dim objTr As New clsRequistionDetail()
                objTr.Item_Code = clsCommon.myCstr(grow.Cells(colItem_Code).Value)
                objTr.Item_Desc = clsCommon.myCstr(grow.Cells(colItem_Desc).Value)
                objTr.Vendor_Code = "" 'clsCommon.myCstr(grow.Cells(colv).Value)
                objTr.Requisition_Qty = clsCommon.myCdbl(grow.Cells(colNet_Requird_Qty).Value)
                objTr.Balance_Qty = clsCommon.myCdbl(grow.Cells(colNet_Requird_Qty).Value)
                objTr.Location = fndLocation.Value 'clsCommon.myCstr(grow.Cells(colLocationCode).Value)
                objTr.Item_Cost = 0 'clsCommon.myCdbl(grow.Cells(col).Value)
                objTr.Unit_Code = clsCommon.myCstr(grow.Cells(colRM_UNIT_CODE).Value)
                objTr.Item_Net_Amt = 0 'clsCommon.myCdbl(grow.Cells(colAmt).Value)
                objTr.Vendor_ItemNo = "" 'clsCommon.myCstr(grow.Cells(colVendorItemNo).Value)
                objTr.Order_No = "" 'clsCommon.myCstr(grow.Cells(colOrderNo).Value)
                objTr.Status = "N"

                objTr.Specification = "" 'clsCommon.myCstr(grow.Cells(colSpecification).Value)
                objTr.Remarks = "" ' clsCommon.myCstr(grow.Cells(colRemarks).Value)

                'objTr.Order_No = clsCommon.myCdbl(grow.Cells(colorderno).Value)
                If (clsCommon.myLen(objTr.Item_Code) > 0) Then
                    obj.ArrTr.Add(objTr)
                End If
            Next


            If (obj.ArrTr Is Nothing OrElse obj.ArrTr.Count <= 0) Then
                common.clsCommon.MyMessageBoxShow("Please Fill at list one Item")
                Return
            End If

            Dim isSaved As Boolean = False
            If (obj.SaveData(obj, isNewEntry)) Then
                isSaved = True
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "REQUISITION_ID", obj.Requisition_Id, True)
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MRP_HEAD", OMInsertOrUpdate.Update, "MRP_CODE='" + objMRP.MRP_CODE + "'")
                If ChekBtnPost = True Then
                    common.clsCommon.MyMessageBoxShow("Data Saved Successfully")
                End If
                'LoadData(obj.Requisition_Id, NavigatorType.Current)

            End If
            'End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Function AllowToSave() As Boolean
        '===================Added by preeti Gupta==============
        If AllowFutureDateTransaction(dtpMRPDate.Value, Nothing) = False Then
            dtpMRPDate.Select()
            Return False
        End If
        '===========================================================
        If btnsave.Text = "Update" Then
            Dim QryStr As String = "select POSTED from TSPL_MRP_HEAD where MRP_Code = '" + txtCode.Value + "' "
            Dim chkpost As String = clsDBFuncationality.getSingleValue(QryStr)
            If chkpost = "1" Then
                clsCommon.MyMessageBoxShow("Transection already posted")
                Return False
            End If
        End If

        If clsCommon.myLen(fndItemToProduce.Value) <= 0 Then
            myMessages.blankValue(Me, "Item To Produce Code", Me.Text)
            fndItemToProduce.Focus()
            Return False
        End If

        If clsCommon.myLen(fndBOM.Value) <= 0 Then
            myMessages.blankValue(Me, "BOM Code", Me.Text)
            fndBOM.Focus()
            Return False
        End If

        If clsCommon.myCdbl(txtBuildQty.Text) <= 0 Then
            myMessages.blankValue(Me, "MRP Quantity", Me.Text)
            txtBuildQty.Focus()
            Return False
        End If
        Return True
    End Function



    Private Sub btndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click
        DeleteData()
    End Sub
    Sub DeleteData()
        If clsCommon.myLen(txtCode.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow("You Cannot Delete Record")
            Exit Sub
        End If
        funDelete()
    End Sub

    Sub funDelete()
        Try
            Dim Reason As String = ""
            If (myMessages.deleteConfirm()) Then
                If clsCancelLog.CheckForReasonOnDelete() Then
                    '' REASON FOR DELETE 
                    Dim frm As New FrmFreeTxtBox1
                    frm.Text = "Remarks for Delete"
                    frm.ShowDialog()
                    If clsCommon.myLen(frm.strRmks) <= 0 Then
                        Exit Sub
                    Else
                        Reason = frm.strRmks
                    End If
                End If
                If (clsMRP.DeleteData(txtCode.Value)) Then
                    saveCancelLog(Reason, "Delete", Nothing)
                    common.clsCommon.MyMessageBoxShow("Data Deleted Successfully ")
                    funReset()
                End If
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try

    End Sub
    Function saveCancelLog(ByVal Reason As String, ByVal Activity_Type As String, Optional ByVal trans As System.Data.SqlClient.SqlTransaction = Nothing) As Boolean
        Dim obj As New clsCancelLog
        obj.Program_Code = Form_ID
        obj.DOCUMENT_NO = clsCommon.myCstr(Me.txtCode.Value)
        obj.REASON = Reason
        obj.ACTIVITY_TYPE = Activity_Type
        Return clsCancelLog.SaveData(obj, True, trans)
    End Function

    Private Sub btnPost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPost.Click
        PostData()
    End Sub

    Sub PostData()
        Try

            If (myMessages.postConfirm()) Then
                SavingData(True)
                If (clsMRP.PostData(txtCode.Value, True)) Then
                    
                    If common.clsCommon.MyMessageBoxShow("Do you want to create Purchase Requisition", "", MessageBoxButtons.YesNo, RadMessageIcon.Question) = DialogResult.Yes Then
                        SavePurchaseRequisitionData(False)
                    End If
                    common.clsCommon.MyMessageBoxShow("Successfully Posted")
                    LoadData(txtCode.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Sub SavingData(ByVal ChekBtnPost As Boolean)
        If (Save()) Then
            If ChekBtnPost = False Then
                common.clsCommon.MyMessageBoxShow("Data Saved Successfully")
            End If
        End If
    End Sub


    Private Sub funPrint()
        'Try
        '    Dim qry As String = " select '" & objCommonVar.CurrentCompanyName & "' as Company_Name, TSPL_FF_SETTLEMENT_HEAD.PROD_ITEM_CODE  as BuildItemCode,CONVERT(VARCHAR,TSPL_FF_SETTLEMENT_HEAD.MO_DATE,103) as BOMDate,CONVERT(VARCHAR,TSPL_FF_SETTLEMENT_HEAD.START_DATE,103) as StartDate,"
        '    qry += " CONVERT(VARCHAR,TSPL_FF_SETTLEMENT_HEAD.END_DATE,103) as EndDate,TSPL_FF_SETTLEMENT_HEAD.STATUS as BomStatus,TSPL_FF_SETTLEMENT_HEAD.PROD_ITEM_UNIT_CODE as BuildUOM,"
        '    qry += " TSPL_FF_SETTLEMENT_HEAD.PROD_QUANTITY as BuildQty, "
        '    qry += " TSPL_FF_SETTLEMENT_HEAD.MIN_BATCH_SIZE as MinBatchSize,TSPL_MF_BOM_DETAIL.LINE_NO as SL_No,TSPL_MF_BOM_DETAIL.CONSM_ITEM_CATEGORY_CODE as ItemCategory,"
        '    qry += " TSPL_MF_BOM_DETAIL.CONSM_ITEM_CODE as ItemCode,TSPL_MF_BOM_DETAIL.ITEM_DESCRIPTION as ItemDesc,TSPL_MF_BOM_DETAIL.CONSM_ITEM_UNIT_CODE as UOM,"
        '    qry += " TSPL_MF_BOM_DETAIL.CONSM_QUANTITY as Quantity,TSPL_MF_BOM_DETAIL.SCRAP_PERCENT as Scrap,TSPL_MF_BOM_DETAIL.WASTAGE_PERCENT as Wastage,"
        '    qry += " TSPL_MF_BOM_DETAIL.REMARKS as Remarks from TSPL_FF_SETTLEMENT_HEAD inner join TSPL_MF_BOM_DETAIL on TSPL_FF_SETTLEMENT_HEAD.MRP_Code=TSPL_MF_BOM_DETAIL.MRP_Code"
        '    qry += " where 2=2"

        '    If txtCode.Value <> "" Then
        '        qry += " and  TSPL_FF_SETTLEMENT_HEAD.MRP_Code='" & txtCode.Value & "' "
        '    End If
        '    Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        '    ProductionReportViewer.funreport(dt, "crptBOMPrint", "Bill Of Material")

        'Catch ex As Exception
        '    common.clsCommon.MyMessageBoxShow(ex.Message)
        'End Try
    End Sub



    Private Sub txtCode__MYNavigator1(ByVal sender As Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtCode._MYNavigator
        Try
            LoadData(txtCode.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub txtCode__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtCode._MYValidating

        Dim str As String = "select count(*) from TSPL_MRP_HEAD where MRP_Code ='" + txtCode.Value + "' "
        Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
        If no = 0 AndAlso isButtonClicked = False Then
            txtCode.MyReadOnly = False
            'txtCode.Value = ""
            '' common.clsCommon.MyMessageBoxShow("Value doesn't exist ")
        Else
            txtCode.MyReadOnly = True
        End If
        If txtCode.MyReadOnly OrElse isButtonClicked Then
            Dim qry As String = "SELECT MRP_CODE AS Code,MRP_DESCRIPTION AS Description,MRP_FROM AS [From],MRP_TO as [To]," & _
                                " ITEM_CODE as [Item Code],MRP_QTY as [MRP Qty], " & _
                                " MRP_ITEM_UNIT_CODE as [Unit Code],MRP_REMARKS as [Remarks],MRP_LOCATION as [Location] FROM TSPL_MRP_HEAD"
            txtCode.Value = clsCommon.ShowSelectForm("TSPL_MRP_HEAD", qry, "Code", "", txtCode.Value, "Code", isButtonClicked)

            If clsCommon.myLen(txtCode.Value) > 0 Then
                LoadData(txtCode.Value, NavigatorType.Current)
            Else
                funReset()
            End If
        End If
    End Sub

    Private Sub chkIncludePO_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkIncludePO.ToggleStateChanged
        If chkIncludePO.Checked Then
            RadPageView1.Pages(1).Hide()
        Else
            RadPageView1.Pages(1).Show()
        End If
    End Sub

    Private Sub chkPendingSRN_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkPendingSRN.ToggleStateChanged
        If chkPendingSRN.Checked Then
            RadPageView1.Pages(2).Hide()
        Else
            RadPageView1.Pages(2).Show()
        End If
    End Sub

    Private Sub dtpFromDate_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpFromDate.ValueChanged
        Me.lblNoOfDays.Text = DateDiff(DateInterval.Day, Me.dtpFromDate.Value, Me.dtpToDate.Value)
    End Sub

    Private Sub dtpToDate_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpToDate.ValueChanged
        Me.lblNoOfDays.Text = DateDiff(DateInterval.Day, Me.dtpFromDate.Value, Me.dtpToDate.Value)
    End Sub

    Private Sub fndItemToProduce__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndItemToProduce._MYValidating
        If clsCommon.myLen(Me.fndProductionPlan.Value) > 0 Then
            Dim dt As DataTable = clsProductionPlanning.GetFinerForPlanItem("TSPL_MF_PRODUCTION_PLAN_HEAD.PROD_PLAN_CODE='" & fndProductionPlan.Value & "'", Me.fndItemToProduce.Value, isButtonClicked)
            If dt.Rows.Count > 0 Then
                Me.fndItemToProduce.Value = dt.Rows(0).Item("Code")
                Me.lblItemDesc.Text = dt.Rows(0).Item("ITEM_DESCRIPTION")
                Me.fndBOM.Value = dt.Rows(0).Item("BOM_CODE")
                lblBOMDesc.Text = dt.Rows(0).Item("Description")
                Me.txtBuildQty.Text = dt.Rows(0).Item("PLAN_QTY")
                Me.lblUnitName.Text = dt.Rows(0).Item("UNIT_CODE")
            End If
        Else
            Dim obj As clsBillOfMaterial = clsBillOfMaterial.FinderForItem(clsCommon.myCstr(fndItemToProduce.Value), "ITEM_TYPE IN ('F')", isButtonClicked)
            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.PROD_ITEM_CODE) > 0 Then
                Me.fndItemToProduce.Value = obj.PROD_ITEM_CODE
                Me.lblItemDesc.Text = obj.ITEM_DESCRIPTION
                Me.lblUnitName.Text = obj.PROD_ITEM_UNIT_CODE
            End If
        End If
    End Sub

    Private Sub btnNext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNext.Click
        CalculateData()
    End Sub
    Sub CalculateData()
        ' Dim qry As String
        If Me.chkIncludePO.Checked Then
            ShowPO()
        Else
            gvPO.Rows.Clear()
        End If
        If Me.chkPendingSRN.Checked Then
            ShowSRN()
        Else
            gvSRN.Rows.Clear()
        End If
        ShowMRPDetail()
    End Sub
    Sub ShowMRPDetail()
        gvMRPDetal.Rows.Clear()
        Dim ObjList As List(Of clsMRPDetail)
        Dim totalOpening As Decimal = 0
        Dim totalRequiredQty As Decimal = 0
        Dim totalPO_Qty As Decimal = 0
        Dim totalSRN_Qty As Decimal = 0
        Dim totalNetReqQty As Decimal = 0
        Dim totalAvailQty As Decimal = 0
        Dim cost As Decimal = 0
        Dim totalCost As Decimal = 0

        ObjList = clsMRPDetail.CalculateMRPDetail(Me.fndBOM.Value, lblUnitName.Text, clsCommon.myCdbl(Me.txtPackSize.Text), clsCommon.myCdbl(Me.txtBuildQty.Text))
        For Each obj As clsMRPDetail In ObjList
            gvMRPDetal.Rows.AddNew()
            gvMRPDetal.Rows(gvMRPDetal.Rows.Count - 1).Cells(colItem_Code).Value = obj.Item_Code
            gvMRPDetal.Rows(gvMRPDetal.Rows.Count - 1).Cells(colItem_Desc).Value = obj.Item_Desc
            gvMRPDetal.Rows(gvMRPDetal.Rows.Count - 1).Cells(colRM_UNIT_CODE).Value = obj.RM_UNIT_CODE

            gvMRPDetal.Rows(gvMRPDetal.Rows.Count - 1).Cells(colTotal_Requird_Qty).Value = obj.Total_Requird_Qty
            gvMRPDetal.Rows(gvMRPDetal.Rows.Count - 1).Cells(colOpening_Qty).Value = obj.Opening_Qty
            gvMRPDetal.Rows(gvMRPDetal.Rows.Count - 1).Cells(colPO_Qty).Value = obj.PO_Qty
            gvMRPDetal.Rows(gvMRPDetal.Rows.Count - 1).Cells(colSRN_Qty).Value = obj.SRN_Qty
            Dim NetReqQty As Decimal
            NetReqQty = IIf((obj.Total_Requird_Qty - obj.Opening_Qty) < 0, 0, (obj.Total_Requird_Qty - obj.Opening_Qty))

            If chkIncludePO.Checked Then
                NetReqQty = IIf((NetReqQty - obj.PO_Qty) < 0, 0, (NetReqQty - obj.PO_Qty))
            End If
            If chkPendingSRN.Checked Then
                NetReqQty = IIf((NetReqQty - obj.SRN_Qty) < 0, 0, (NetReqQty - obj.SRN_Qty))
            End If
            gvMRPDetal.Rows(gvMRPDetal.Rows.Count - 1).Cells(colNet_Requird_Qty).Value = NetReqQty
            gvMRPDetal.Rows(gvMRPDetal.Rows.Count - 1).Cells(colTotal_Avail_Qty).Value = obj.Opening_Qty + gvMRPDetal.Rows(gvMRPDetal.Rows.Count - 1).Cells(colPO_Qty).Value + gvMRPDetal.Rows(gvMRPDetal.Rows.Count - 1).Cells(colSRN_Qty).Value
            gvMRPDetal.Rows(gvMRPDetal.Rows.Count - 1).Cells(colCost).Value = clsInventoryMovement.GetCost(EnumCostingMethod.Averege, obj.Item_Code, Me.fndLocation.Value, NetReqQty, Me.dtpMRPDate.Value, clsCommon.GETSERVERDATE(), False, Nothing)
            gvMRPDetal.Rows(gvMRPDetal.Rows.Count - 1).Cells(colTotalCost).Value = gvMRPDetal.Rows(gvMRPDetal.Rows.Count - 1).Cells(colCost).Value * NetReqQty

            totalRequiredQty = totalRequiredQty + obj.Total_Requird_Qty
            totalOpening = totalOpening + obj.Opening_Qty
            totalPO_Qty = totalPO_Qty + obj.PO_Qty
            totalSRN_Qty = totalSRN_Qty + obj.SRN_Qty
            totalNetReqQty = totalNetReqQty + obj.Net_Requird_Qty
            totalAvailQty = totalAvailQty + gvMRPDetal.Rows(gvMRPDetal.Rows.Count - 1).Cells(colTotal_Avail_Qty).Value
            cost = cost + gvMRPDetal.Rows(gvMRPDetal.Rows.Count - 1).Cells(colCost).Value
            totalCost = totalCost + gvMRPDetal.Rows(gvMRPDetal.Rows.Count - 1).Cells(colTotalCost).Value
        Next
        '' total 

        gvMRPDetal.Rows.AddNew()
        gvMRPDetal.Rows(gvMRPDetal.Rows.Count - 1).Cells(colItem_Code).Value = "Total :"

        gvMRPDetal.Rows(gvMRPDetal.Rows.Count - 1).Cells(colTotal_Requird_Qty).Value = totalRequiredQty
        gvMRPDetal.Rows(gvMRPDetal.Rows.Count - 1).Cells(colOpening_Qty).Value = totalOpening
        gvMRPDetal.Rows(gvMRPDetal.Rows.Count - 1).Cells(colPO_Qty).Value = totalPO_Qty
        gvMRPDetal.Rows(gvMRPDetal.Rows.Count - 1).Cells(colSRN_Qty).Value = totalSRN_Qty
        
        gvMRPDetal.Rows(gvMRPDetal.Rows.Count - 1).Cells(colNet_Requird_Qty).Value = totalNetReqQty
        gvMRPDetal.Rows(gvMRPDetal.Rows.Count - 1).Cells(colTotal_Avail_Qty).Value = totalAvailQty
        gvMRPDetal.Rows(gvMRPDetal.Rows.Count - 1).Cells(colCost).Value = cost
        gvMRPDetal.Rows(gvMRPDetal.Rows.Count - 1).Cells(colTotalCost).Value = totalCost
    End Sub
    Sub ShowPO()
        Dim dtPO As DataTable
        dtPO = clsMRPPO.GetPO(fndItemToProduce.Value, fndBOM.Value)
        gvPO.Rows.Clear()
        For Each dr As DataRow In dtPO.Rows
            gvPO.Rows.AddNew()
            Me.gvPO.Rows(gvPO.Rows.Count - 1).Cells(colPurchaseOrder_No).Value = dr.Item("PurchaseOrder_No")
            Me.gvPO.Rows(gvPO.Rows.Count - 1).Cells(colPurchaseOrder_Date).Value = dr.Item("PurchaseOrder_Date")
            Me.gvPO.Rows(gvPO.Rows.Count - 1).Cells(colPOBill_To_Location).Value = dr.Item("Bill_To_Location")
            Me.gvPO.Rows(gvPO.Rows.Count - 1).Cells(colPOVendor_Code).Value = dr.Item("Vendor_Code")
            Me.gvPO.Rows(gvPO.Rows.Count - 1).Cells(colPOVendor_Name).Value = dr.Item("Vendor_Name")
        Next
    End Sub

    Sub ShowSRN()
        Dim dtSRN As DataTable
        dtSRN = clsMRPSRN.GetSRN(fndItemToProduce.Value, fndBOM.Value)
        gvSRN.Rows.Clear()
        For Each dr As DataRow In dtSRN.Rows
            gvSRN.Rows.AddNew()
            Me.gvSRN.Rows(gvSRN.Rows.Count - 1).Cells(colSRN_No).Value = dr.Item("SRN_No")
            Me.gvSRN.Rows(gvSRN.Rows.Count - 1).Cells(colSRN_Date).Value = dr.Item("SRN_Date")
            Me.gvSRN.Rows(gvSRN.Rows.Count - 1).Cells(colSRNBill_To_Location).Value = dr.Item("Bill_To_Location")
            Me.gvSRN.Rows(gvSRN.Rows.Count - 1).Cells(colSRNVendor_Code).Value = dr.Item("Vendor_Code")
            Me.gvSRN.Rows(gvSRN.Rows.Count - 1).Cells(colSRNVendor_Name).Value = dr.Item("Vendor_Name")
        Next
    End Sub

    
    Private Sub fndBOM__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndBOM._MYValidating
        Dim objBom As clsBillOfMaterial = clsMRP.GetBomCodeFromItemCode(fndItemToProduce.Value)
        If objBom IsNot Nothing Then
            Me.fndBOM.Value = objBom.BOM_CODE
            Me.lblBOMDesc.Text = objBom.DESCRIPTION
        End If
    End Sub

    Private Sub fndLocation__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndLocation._MYValidating
        Try
            Dim qry As String = "select Location_Code as Code,Location_Desc as Name from TSPL_LOCATION_MASTER "
            Dim WhrCls As String = " Location_Type='Physical'  "
            If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                WhrCls += "  and  Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
            End If
            fndLocation.Value = clsCommon.ShowSelectForm("VendorLocFND", qry, "Code", WhrCls, fndLocation.Value, "Code", isButtonClicked)
            lblLocationDesc.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" + fndLocation.Value + "'"))
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub btnRequisition_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRequisition.Click
        SavePurchaseRequisitionData(True)
    End Sub

    Private Sub fndProductionPlan__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndProductionPlan._MYValidating
        Me.fndProductionPlan.Value = clsProductionPlanning.GetFinder("", Me.fndProductionPlan.Value, isButtonClicked)
        If clsCommon.myLen(Me.fndProductionPlan.Value) > 0 Then
            Dim objPP As clsProductionPlanning = clsProductionPlanning.GetData(Me.fndProductionPlan.Value, NavigatorType.Current)
            If Not objPP Is Nothing Then
                Me.txtProductionPlanDesc.Text = objPP.DESCRIPTION
                Me.dtpFromDate.Value = objPP.PLAN_FOR_DATE
                If Not objPP.PLAN_TO_DATE Is Nothing Then
                    Me.dtpToDate.Value = objPP.PLAN_TO_DATE
                End If
                lblNoOfDays.Text = DateDiff(DateInterval.Day, Me.dtpFromDate.Value, Me.dtpToDate.Value) + 1
            End If
        End If
        
    End Sub

    Private Sub btnnew_Click1(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnnew.Click
        funReset()
    End Sub

    Private Sub btnUnpost_Click(sender As Object, e As EventArgs) Handles btnUnpost.Click
        Try
            If clsCommon.myLen(txtCode.Value) > 0 Then
                If clsCommon.MyMessageBoxShow("Unpost the current transaction" + Environment.NewLine + "Are you sure", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = Windows.Forms.DialogResult.Yes Then
                    clsMRP.ReverseAndUnpost(txtCode.Value)
                    clsCommon.MyMessageBoxShow("Tansaction unposted succesffuly", Me.Text)
                    LoadData(txtCode.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub
End Class