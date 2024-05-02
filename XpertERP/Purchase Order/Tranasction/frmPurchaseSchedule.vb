''Created By Monika
Imports common
Imports System.Data.SqlClient


Public Class FrmPurchaseSchedule
    Inherits FrmMainTranScreen

#Region "Variables"
    Dim TotalNo_Count_For_Sch_Qty As Integer = 0
    Dim isNewEntry As Boolean = True
    Dim ButtonToolTip As New ToolTip()
    Dim Errorcontrol As New clsErrorControl()
    Dim isCellValueChanged As Boolean = False
    Dim isInsideLoadData As Boolean = False

    Const colLineno As String = "LineNo"
    Const colItemCode As String = "ItemCode"
    Const colItemName As String = "ItemName"
    Const colItemType As String = "ItemType"
    Const colItemUnit As String = "ItemUnit"
    Const colPONo As String = "PONO"
    Const colPODate As String = "PODate"
    Const colPOQty As String = "POQty"
    Const colSchQty As String = "SchQty"
    Const colWKQty1 As String = "WKQty1"
    Const colWKQty2 As String = "WKQty2"
    Const colWKQty3 As String = "WKQty3"
    Const colWKQty4 As String = "WKQty4"
    Const colWKQty5 As String = "WKQty5"
    Const colWKQty6 As String = "WKQty6"
    Const colQty1 As String = "Qty1"
    Const colQty2 As String = "Qty2"
    Const colQty3 As String = "Qty3"
    Const colQty4 As String = "Qty4"
    Const colQty5 As String = "Qty5"
    Const colQty6 As String = "Qty6"
    Const colQty7 As String = "Qty7"
    Const colQty8 As String = "Qty8"
    Const colQty9 As String = "Qty9"
    Const colQty10 As String = "Qty10"
    Const colQty11 As String = "Qty11"
    Const colQty12 As String = "Qty12"
    Const colQty13 As String = "Qty13"
    Const colQty14 As String = "Qty14"
    Const colQty15 As String = "Qty15"
    Const colQty16 As String = "Qty16"
    Const colQty17 As String = "Qty17"
    Const colQty18 As String = "Qty18"
    Const colQty19 As String = "Qty19"
    Const colQty20 As String = "Qty20"
    Const colQty21 As String = "Qty21"
    Const colQty22 As String = "Qty22"
    Const colQty23 As String = "Qty23"
    Const colQty24 As String = "Qty24"
    Const colQty25 As String = "Qty25"
    Const colQty26 As String = "Qty26"
    Const colQty27 As String = "Qty27"
    Const colQty28 As String = "Qty28"
    Const colQty29 As String = "Qty29"
    Const colQty30 As String = "Qty30"
    Const colQty31 As String = "Qty31"
    Const colMonth1 As String = "Month1"
    Const colMonth2 As String = "Month2"
    Const colMonth3 As String = "Month3"
    Const colMonth4 As String = "Month4"
    Const colMonth5 As String = "Month5"
    Const colMonth6 As String = "Month6"
    Const colMonth7 As String = "Month7"
    Const colMonth8 As String = "Month8"
    Const colMonth9 As String = "Month9"
    Const colMonth10 As String = "Month10"
    Const colMonth11 As String = "Month11"
    Const colMonth12 As String = "Month12"
    Const colPers As String = "Pers"
    Const colPersType As String = "PersType"
    Const colRemarks As String = "remarks"

    '============vendor gris================
    Const colVLineno As String = "VLineNo"
    Const colVItemCode As String = "VItemCode"
    Const colVItemName As String = "VItemName"
    Const colVItemType As String = "VItemType"
    Const colVItemUnit As String = "VItemUnit"
    Const colVPONo As String = "VPONO"
    Const colVPODate As String = "VPODate"
    Const colVPOQty As String = "VPOQty"
    Const colVSchQty As String = "VSchQty"
    Const colVWKQty1 As String = "VWKQty1"
    Const colVWKQty2 As String = "VWKQty2"
    Const colVWKQty3 As String = "VWKQty3"
    Const colVWKQty4 As String = "VWKQty4"
    Const colVWKQty5 As String = "VWKQty5"
    Const colVWKQty6 As String = "VWKQty6"
    Const colVQty1 As String = "VQty1"
    Const colVQty2 As String = "VQty2"
    Const colVQty3 As String = "VQty3"
    Const colVQty4 As String = "VQty4"
    Const colVQty5 As String = "VQty5"
    Const colVQty6 As String = "VQty6"
    Const colVQty7 As String = "VQty7"
    Const colVQty8 As String = "VQty8"
    Const colVQty9 As String = "VQty9"
    Const colVQty10 As String = "VQty10"
    Const colVQty11 As String = "VQty11"
    Const colVQty12 As String = "VQty12"
    Const colVQty13 As String = "VQty13"
    Const colVQty14 As String = "VQty14"
    Const colVQty15 As String = "VQty15"
    Const colVQty16 As String = "VQty16"
    Const colVQty17 As String = "VQty17"
    Const colVQty18 As String = "VQty18"
    Const colVQty19 As String = "VQty19"
    Const colVQty20 As String = "VQty20"
    Const colVQty21 As String = "VQty21"
    Const colVQty22 As String = "VQty22"
    Const colVQty23 As String = "VQty23"
    Const colVQty24 As String = "VQty24"
    Const colVQty25 As String = "VQty25"
    Const colVQty26 As String = "VQty26"
    Const colVQty27 As String = "VQty27"
    Const colVQty28 As String = "VQty28"
    Const colVQty29 As String = "VQty29"
    Const colVQty30 As String = "VQty30"
    Const colVQty31 As String = "VQty31"
    Const colVMonth1 As String = "VMonth1"
    Const colVMonth2 As String = "VMonth2"
    Const colVMonth3 As String = "VMonth3"
    Const colVMonth4 As String = "VMonth4"
    Const colVMonth5 As String = "VMonth5"
    Const colVMonth6 As String = "VMonth6"
    Const colVMonth7 As String = "VMonth7"
    Const colVMonth8 As String = "VMonth8"
    Const colVMonth9 As String = "VMonth9"
    Const colVMonth10 As String = "VMonth10"
    Const colVMonth11 As String = "VMonth11"
    Const colVMonth12 As String = "VMonth12"
    Const colVRemarks As String = "Vremarks"
#End Region

    Public Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmPurchaseSchedule)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnPost.Visible = MyBase.isPostFlag
        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub FrmPurchaseSchedule_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag Then
                btnsave.PerformClick()
            ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag Then
                btndelete.PerformClick()
            ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag Then
                btnpost.PerformClick()
            ElseIf e.Alt AndAlso e.KeyCode = Keys.R AndAlso btnunpost.Visible = True AndAlso MyBase.isReverse Then
                btnunpost.PerformClick()
            ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
                Me.Close()
            End If
            If e.Alt AndAlso e.Shift AndAlso e.Control AndAlso e.KeyCode = Keys.F12 AndAlso btnpost.Enabled = False AndAlso btnsave.Enabled = False Then
                Dim frm As New FrmPWD(Nothing)
                frm.strType = "SIRC"
                frm.strCode = "SIReversAndCreate"
                frm.ShowDialog()
                If frm.isPasswordCorrect Then
                    btnunpost.Visible = True
                End If
            End If
        Catch ex As Exception
            isCellValueChanged = False
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub FrmPurchaseSchedule_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        LoadPOType()
        LoadBlankGrid()
        LoadVendorBlankGrid()
        Funreset()

        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for save record.")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D for delete record.")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C for close window.")
        ButtonToolTip.SetToolTip(btnunpost, "Press Alt+R for amend record.")
        ButtonToolTip.SetToolTip(btnpost, "Press Alt+P for post record.")

        btnunpost.Visible = False
        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
    End Sub

    Private Sub Funreset()
        MyLabel1.Text = "Month"
        btnunpost.Visible = False

        txtRev_No.Text = ""
        txtDocNo.Value = ""
        txtDate.Text = clsCommon.GETSERVERDATE(Nothing)
        txtDesc.Text = ""
        txtVendorNo.Value = ""
        lblVendorName.Text = ""
        cboPOType.SelectedValue = ""
        dtpMonth.Text = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(Nothing), "MMM yyyy")
        btnWeek.IsChecked = True
        btnDaily.IsChecked = False
        btnMonthly.IsChecked = False
        txtPONo.Value = ""
        txtPODesc.Text = ""
        GridVisibility()

        gv1.Rows.Clear()
        gv1.Rows.AddNew()

        gv_Vendor.Rows.Clear()
        gv_Vendor.Rows.AddNew()

        btnsave.Text = "Save"
        btnsave.Enabled = True
        btndelete.Enabled = False
        btnpost.Enabled = False
        txtDocNo.MyReadOnly = False
        isInsideLoadData = False
        isNewEntry = True
        txtVendorNo.Enabled = True
        txtPONo.Enabled = True
        cboPOType.Enabled = True
        dtpMonth.Enabled = True
        btnDaily.Enabled = True
        btnMonthly.Enabled = True
        btnWeek.Enabled = True
        UsLock1.Status = ERPTransactionStatus.Open

        UcAttachment1.Form_ID = MyBase.Form_ID
        UcAttachment1.BlankAllControls()

        RadPageView1.SelectedPage = RadPageViewPage1
        txtDocNo.Focus()
        txtDocNo.Select()
    End Sub

    Private Sub LoadBlankGrid()
        gv1.Rows.Clear()
        gv1.Columns.Clear()

        Dim repocombobox As GridViewComboBoxColumn = New GridViewComboBoxColumn()

        Dim repoLineNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "Line No"
        repoLineNo.Name = colLineno
        repoLineNo.Width = 50
        repoLineNo.ReadOnly = True
        repoLineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoLineNo)

        repoLineNo = New GridViewTextBoxColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "Item Code"
        repoLineNo.Name = colItemCode
        repoLineNo.Width = 100
        repoLineNo.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoLineNo)

        repoLineNo = New GridViewTextBoxColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "Description"
        repoLineNo.Name = colItemName
        repoLineNo.Width = 220
        repoLineNo.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoLineNo)

        repoLineNo = New GridViewTextBoxColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "Item Type"
        repoLineNo.Name = colItemType
        repoLineNo.Width = 80
        repoLineNo.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoLineNo)

        repoLineNo = New GridViewTextBoxColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "UOM"
        repoLineNo.Name = colItemUnit
        repoLineNo.Width = 80
        repoLineNo.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoLineNo)

        repoLineNo = New GridViewTextBoxColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "PO Code"
        repoLineNo.Name = colPONo
        repoLineNo.Width = 80
        repoLineNo.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoLineNo)

        repoLineNo = New GridViewTextBoxColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "PO Date"
        repoLineNo.Name = colPODate
        repoLineNo.Width = 80
        repoLineNo.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoLineNo)

        Dim repoqty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoqty.FormatString = ""
        repoqty.HeaderText = "Order Qty"
        repoqty.Name = colPOQty
        repoqty.DecimalPlaces = 2
        repoqty.Width = 80
        repoqty.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoqty)

        repoqty = New GridViewDecimalColumn()
        repoqty.FormatString = ""
        repoqty.HeaderText = "Schedule Qty"
        repoqty.Name = colSchQty
        repoqty.DecimalPlaces = 2
        repoqty.Width = 80
        gv1.MasterTemplate.Columns.Add(repoqty)

        repocombobox = New GridViewComboBoxColumn()
        repocombobox.FormatString = ""
        repocombobox.HeaderText = "Additional Type"
        repocombobox.Name = colPersType
        repocombobox.Width = 70
        repocombobox.DataSource = clsDBFuncationality.GetDataTable("select 'P' as Code,'Percentage' as Name union all select 'V' as Code,'Value' as Name")
        repocombobox.ValueMember = "Code"
        repocombobox.DisplayMember = "Name"
        gv1.MasterTemplate.Columns.Add(repocombobox)

        repoqty = New GridViewDecimalColumn()
        repoqty.FormatString = ""
        repoqty.HeaderText = "Per% / Value"
        repoqty.Name = colPers
        repoqty.DecimalPlaces = 2
        repoqty.Width = 80
        repoqty.WrapText = True
        gv1.MasterTemplate.Columns.Add(repoqty)

        repoqty = New GridViewDecimalColumn()
        repoqty.FormatString = ""
        repoqty.HeaderText = "Week1 Qty"
        repoqty.Name = colWKQty1
        repoqty.DecimalPlaces = 2
        repoqty.Width = 80
        repoqty.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoqty)

        repoqty = New GridViewDecimalColumn()
        repoqty.FormatString = ""
        repoqty.HeaderText = "Week2 Qty"
        repoqty.Name = colWKQty2
        repoqty.DecimalPlaces = 2
        repoqty.Width = 80
        repoqty.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoqty)

        repoqty = New GridViewDecimalColumn()
        repoqty.FormatString = ""
        repoqty.HeaderText = "Week3 Qty"
        repoqty.Name = colWKQty3
        repoqty.DecimalPlaces = 2
        repoqty.Width = 80
        repoqty.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoqty)

        repoqty = New GridViewDecimalColumn()
        repoqty.FormatString = ""
        repoqty.HeaderText = "Week4 Qty"
        repoqty.Name = colWKQty4
        repoqty.DecimalPlaces = 2
        repoqty.Width = 80
        repoqty.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoqty)

        repoqty = New GridViewDecimalColumn()
        repoqty.FormatString = ""
        repoqty.HeaderText = "Week5 Qty"
        repoqty.Name = colWKQty5
        repoqty.DecimalPlaces = 2
        repoqty.Width = 80
        repoqty.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoqty)

        repoqty = New GridViewDecimalColumn()
        repoqty.FormatString = ""
        repoqty.HeaderText = "Week6 Qty"
        repoqty.Name = colWKQty6
        repoqty.DecimalPlaces = 2
        repoqty.Width = 80
        repoqty.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoqty)

        repoqty = New GridViewDecimalColumn()
        repoqty.FormatString = ""
        repoqty.HeaderText = "Day1 Qty"
        repoqty.Name = colQty1
        repoqty.DecimalPlaces = 2
        repoqty.Width = 80
        repoqty.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoqty)

        repoqty = New GridViewDecimalColumn()
        repoqty.FormatString = ""
        repoqty.HeaderText = "Day2 Qty"
        repoqty.Name = colQty2
        repoqty.DecimalPlaces = 2
        repoqty.Width = 80
        repoqty.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoqty)

        repoqty = New GridViewDecimalColumn()
        repoqty.FormatString = ""
        repoqty.HeaderText = "Day3 Qty"
        repoqty.Name = colQty3
        repoqty.DecimalPlaces = 2
        repoqty.Width = 80
        repoqty.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoqty)

        repoqty = New GridViewDecimalColumn()
        repoqty.FormatString = ""
        repoqty.HeaderText = "Day4 Qty"
        repoqty.Name = colQty4
        repoqty.DecimalPlaces = 2
        repoqty.Width = 80
        repoqty.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoqty)

        repoqty = New GridViewDecimalColumn()
        repoqty.FormatString = ""
        repoqty.HeaderText = "Day5 Qty"
        repoqty.Name = colQty5
        repoqty.DecimalPlaces = 2
        repoqty.Width = 80
        repoqty.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoqty)

        repoqty = New GridViewDecimalColumn()
        repoqty.FormatString = ""
        repoqty.HeaderText = "Day6 Qty"
        repoqty.Name = colQty6
        repoqty.DecimalPlaces = 2
        repoqty.Width = 80
        repoqty.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoqty)

        repoqty = New GridViewDecimalColumn()
        repoqty.FormatString = ""
        repoqty.HeaderText = "Day7 Qty"
        repoqty.Name = colQty7
        repoqty.DecimalPlaces = 2
        repoqty.Width = 80
        repoqty.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoqty)

        repoqty = New GridViewDecimalColumn()
        repoqty.FormatString = ""
        repoqty.HeaderText = "Day8 Qty"
        repoqty.Name = colQty8
        repoqty.DecimalPlaces = 2
        repoqty.Width = 80
        repoqty.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoqty)

        repoqty = New GridViewDecimalColumn()
        repoqty.FormatString = ""
        repoqty.HeaderText = "Day9 Qty"
        repoqty.Name = colQty9
        repoqty.DecimalPlaces = 2
        repoqty.Width = 80
        repoqty.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoqty)

        repoqty = New GridViewDecimalColumn()
        repoqty.FormatString = ""
        repoqty.HeaderText = "Day10 Qty"
        repoqty.Name = colQty10
        repoqty.DecimalPlaces = 2
        repoqty.Width = 80
        repoqty.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoqty)

        repoqty = New GridViewDecimalColumn()
        repoqty.FormatString = ""
        repoqty.HeaderText = "Day11 Qty"
        repoqty.Name = colQty11
        repoqty.DecimalPlaces = 2
        repoqty.Width = 80
        repoqty.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoqty)

        repoqty = New GridViewDecimalColumn()
        repoqty.FormatString = ""
        repoqty.HeaderText = "Day12 Qty"
        repoqty.Name = colQty12
        repoqty.DecimalPlaces = 2
        repoqty.Width = 80
        repoqty.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoqty)

        repoqty = New GridViewDecimalColumn()
        repoqty.FormatString = ""
        repoqty.HeaderText = "Day13 Qty"
        repoqty.Name = colQty13
        repoqty.DecimalPlaces = 2
        repoqty.Width = 80
        repoqty.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoqty)

        repoqty = New GridViewDecimalColumn()
        repoqty.FormatString = ""
        repoqty.HeaderText = "Day14 Qty"
        repoqty.Name = colQty14
        repoqty.DecimalPlaces = 2
        repoqty.Width = 80
        repoqty.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoqty)

        repoqty = New GridViewDecimalColumn()
        repoqty.FormatString = ""
        repoqty.HeaderText = "Day15 Qty"
        repoqty.Name = colQty15
        repoqty.DecimalPlaces = 2
        repoqty.Width = 80
        repoqty.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoqty)

        repoqty = New GridViewDecimalColumn()
        repoqty.FormatString = ""
        repoqty.HeaderText = "Day16 Qty"
        repoqty.Name = colQty16
        repoqty.DecimalPlaces = 2
        repoqty.Width = 80
        repoqty.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoqty)

        repoqty = New GridViewDecimalColumn()
        repoqty.FormatString = ""
        repoqty.HeaderText = "Day17 Qty"
        repoqty.Name = colQty17
        repoqty.DecimalPlaces = 2
        repoqty.Width = 80
        repoqty.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoqty)

        repoqty = New GridViewDecimalColumn()
        repoqty.FormatString = ""
        repoqty.HeaderText = "Day18 Qty"
        repoqty.Name = colQty18
        repoqty.DecimalPlaces = 2
        repoqty.Width = 80
        repoqty.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoqty)

        repoqty = New GridViewDecimalColumn()
        repoqty.FormatString = ""
        repoqty.HeaderText = "Day19 Qty"
        repoqty.Name = colQty19
        repoqty.DecimalPlaces = 2
        repoqty.Width = 80
        repoqty.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoqty)

        repoqty = New GridViewDecimalColumn()
        repoqty.FormatString = ""
        repoqty.HeaderText = "Day20 Qty"
        repoqty.Name = colQty20
        repoqty.DecimalPlaces = 2
        repoqty.Width = 80
        repoqty.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoqty)

        repoqty = New GridViewDecimalColumn()
        repoqty.FormatString = ""
        repoqty.HeaderText = "Day21 Qty"
        repoqty.Name = colQty21
        repoqty.DecimalPlaces = 2
        repoqty.Width = 80
        repoqty.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoqty)

        repoqty = New GridViewDecimalColumn()
        repoqty.FormatString = ""
        repoqty.HeaderText = "Day22 Qty"
        repoqty.Name = colQty22
        repoqty.DecimalPlaces = 2
        repoqty.Width = 80
        repoqty.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoqty)

        repoqty = New GridViewDecimalColumn()
        repoqty.FormatString = ""
        repoqty.HeaderText = "Day23 Qty"
        repoqty.Name = colQty23
        repoqty.DecimalPlaces = 2
        repoqty.Width = 80
        repoqty.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoqty)

        repoqty = New GridViewDecimalColumn()
        repoqty.FormatString = ""
        repoqty.HeaderText = "Day24 Qty"
        repoqty.Name = colQty24
        repoqty.DecimalPlaces = 2
        repoqty.Width = 80
        repoqty.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoqty)

        repoqty = New GridViewDecimalColumn()
        repoqty.FormatString = ""
        repoqty.HeaderText = "Day25 Qty"
        repoqty.Name = colQty25
        repoqty.DecimalPlaces = 2
        repoqty.Width = 80
        repoqty.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoqty)

        repoqty = New GridViewDecimalColumn()
        repoqty.FormatString = ""
        repoqty.HeaderText = "Day26 Qty"
        repoqty.Name = colQty26
        repoqty.DecimalPlaces = 2
        repoqty.Width = 80
        repoqty.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoqty)

        repoqty = New GridViewDecimalColumn()
        repoqty.FormatString = ""
        repoqty.HeaderText = "Day27 Qty"
        repoqty.Name = colQty27
        repoqty.DecimalPlaces = 2
        repoqty.Width = 80
        repoqty.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoqty)

        repoqty = New GridViewDecimalColumn()
        repoqty.FormatString = ""
        repoqty.HeaderText = "Day28 Qty"
        repoqty.Name = colQty28
        repoqty.DecimalPlaces = 2
        repoqty.Width = 80
        repoqty.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoqty)

        repoqty = New GridViewDecimalColumn()
        repoqty.FormatString = ""
        repoqty.HeaderText = "Day29 Qty"
        repoqty.Name = colQty29
        repoqty.DecimalPlaces = 2
        repoqty.Width = 80
        repoqty.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoqty)

        repoqty = New GridViewDecimalColumn()
        repoqty.FormatString = ""
        repoqty.HeaderText = "Day30 Qty"
        repoqty.Name = colQty30
        repoqty.DecimalPlaces = 2
        repoqty.Width = 80
        repoqty.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoqty)

        repoqty = New GridViewDecimalColumn()
        repoqty.FormatString = ""
        repoqty.HeaderText = "Day31 Qty"
        repoqty.Name = colQty31
        repoqty.DecimalPlaces = 2
        repoqty.Width = 80
        repoqty.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoqty)

        '===month====
        repoqty = New GridViewDecimalColumn()
        repoqty.FormatString = ""
        repoqty.HeaderText = "Jan(Qty)"
        repoqty.Name = colMonth1
        repoqty.DecimalPlaces = 2
        repoqty.Width = 80
        repoqty.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoqty)

        repoqty = New GridViewDecimalColumn()
        repoqty.FormatString = ""
        repoqty.HeaderText = "Feb(Qty)"
        repoqty.Name = colMonth2
        repoqty.DecimalPlaces = 2
        repoqty.Width = 80
        repoqty.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoqty)

        repoqty = New GridViewDecimalColumn()
        repoqty.FormatString = ""
        repoqty.HeaderText = "March(Qty)"
        repoqty.Name = colMonth3
        repoqty.DecimalPlaces = 2
        repoqty.Width = 80
        repoqty.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoqty)

        repoqty = New GridViewDecimalColumn()
        repoqty.FormatString = ""
        repoqty.HeaderText = "April(Qty)"
        repoqty.Name = colMonth4
        repoqty.DecimalPlaces = 2
        repoqty.Width = 80
        repoqty.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoqty)

        repoqty = New GridViewDecimalColumn()
        repoqty.FormatString = ""
        repoqty.HeaderText = "May(Qty)"
        repoqty.Name = colMonth5
        repoqty.DecimalPlaces = 2
        repoqty.Width = 80
        repoqty.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoqty)

        repoqty = New GridViewDecimalColumn()
        repoqty.FormatString = ""
        repoqty.HeaderText = "June(Qty)"
        repoqty.Name = colMonth6
        repoqty.DecimalPlaces = 2
        repoqty.Width = 80
        repoqty.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoqty)

        repoqty = New GridViewDecimalColumn()
        repoqty.FormatString = ""
        repoqty.HeaderText = "July(Qty)"
        repoqty.Name = colMonth7
        repoqty.DecimalPlaces = 2
        repoqty.Width = 80
        repoqty.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoqty)

        repoqty = New GridViewDecimalColumn()
        repoqty.FormatString = ""
        repoqty.HeaderText = "Aug(Qty)"
        repoqty.Name = colMonth8
        repoqty.DecimalPlaces = 2
        repoqty.Width = 80
        repoqty.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoqty)

        repoqty = New GridViewDecimalColumn()
        repoqty.FormatString = ""
        repoqty.HeaderText = "Sep(Qty)"
        repoqty.Name = colMonth9
        repoqty.DecimalPlaces = 2
        repoqty.Width = 80
        repoqty.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoqty)

        repoqty = New GridViewDecimalColumn()
        repoqty.FormatString = ""
        repoqty.HeaderText = "Oct(Qty)"
        repoqty.Name = colMonth10
        repoqty.DecimalPlaces = 2
        repoqty.Width = 80
        repoqty.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoqty)

        repoqty = New GridViewDecimalColumn()
        repoqty.FormatString = ""
        repoqty.HeaderText = "Nov(Qty)"
        repoqty.Name = colMonth11
        repoqty.DecimalPlaces = 2
        repoqty.Width = 80
        repoqty.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoqty)

        repoqty = New GridViewDecimalColumn()
        repoqty.FormatString = ""
        repoqty.HeaderText = "Dec(Qty)"
        repoqty.Name = colMonth12
        repoqty.DecimalPlaces = 2
        repoqty.Width = 80
        repoqty.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoqty)
        '=============

        repoLineNo = New GridViewTextBoxColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "Remarks"
        repoLineNo.Name = colRemarks
        repoLineNo.Width = 120
        repoLineNo.MaxLength = 100
        gv1.MasterTemplate.Columns.Add(repoLineNo)

        gv1.AllowDeleteRow = True
        gv1.AllowAddNewRow = False
        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = True
        gv1.AllowRowReorder = False
        gv1.EnableSorting = False
        gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        gv1.TableElement.TableHeaderHeight = 40
    End Sub

    Sub LoadPOType()
        cboPOType.DataSource = clsPurchaseOrderHead.LoadPurchaseType()
        cboPOType.ValueMember = "Code"
        cboPOType.DisplayMember = "Name"
    End Sub

    Private Sub LoadVendorBlankGrid()
        gv_Vendor.Rows.Clear()
        gv_Vendor.Columns.Clear()

        Dim repoLineNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "Line No"
        repoLineNo.Name = colVLineno
        repoLineNo.Width = 50
        repoLineNo.ReadOnly = True
        repoLineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv_Vendor.MasterTemplate.Columns.Add(repoLineNo)

        repoLineNo = New GridViewTextBoxColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "Item Code"
        repoLineNo.Name = colVItemCode
        repoLineNo.Width = 100
        repoLineNo.ReadOnly = True
        gv_Vendor.MasterTemplate.Columns.Add(repoLineNo)

        repoLineNo = New GridViewTextBoxColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "Description"
        repoLineNo.Name = colVItemName
        repoLineNo.Width = 220
        repoLineNo.ReadOnly = True
        gv_Vendor.MasterTemplate.Columns.Add(repoLineNo)

        repoLineNo = New GridViewTextBoxColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "Item Type"
        repoLineNo.Name = colVItemType
        repoLineNo.Width = 80
        repoLineNo.ReadOnly = True
        gv_Vendor.MasterTemplate.Columns.Add(repoLineNo)

        repoLineNo = New GridViewTextBoxColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "UOM"
        repoLineNo.Name = colVItemUnit
        repoLineNo.Width = 80
        repoLineNo.ReadOnly = True
        gv_Vendor.MasterTemplate.Columns.Add(repoLineNo)

        repoLineNo = New GridViewTextBoxColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "PO Code"
        repoLineNo.Name = colVPONo
        repoLineNo.Width = 80
        repoLineNo.ReadOnly = True
        gv_Vendor.MasterTemplate.Columns.Add(repoLineNo)

        repoLineNo = New GridViewTextBoxColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "PO Date"
        repoLineNo.Name = colVPODate
        repoLineNo.Width = 80
        repoLineNo.ReadOnly = True
        gv_Vendor.MasterTemplate.Columns.Add(repoLineNo)

        Dim repoqty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoqty.FormatString = ""
        repoqty.HeaderText = "Order Qty"
        repoqty.Name = colVPOQty
        repoqty.DecimalPlaces = 2
        repoqty.Width = 80
        repoqty.ReadOnly = True
        gv_Vendor.MasterTemplate.Columns.Add(repoqty)

        repoqty = New GridViewDecimalColumn()
        repoqty.FormatString = ""
        repoqty.HeaderText = "Schedule Qty"
        repoqty.Name = colVSchQty
        repoqty.DecimalPlaces = 2
        repoqty.Width = 80
        gv_Vendor.MasterTemplate.Columns.Add(repoqty)

        repoqty = New GridViewDecimalColumn()
        repoqty.FormatString = ""
        repoqty.HeaderText = "Week1 Qty"
        repoqty.Name = colVWKQty1
        repoqty.DecimalPlaces = 2
        repoqty.Width = 80
        repoqty.IsVisible = False
        gv_Vendor.MasterTemplate.Columns.Add(repoqty)

        repoqty = New GridViewDecimalColumn()
        repoqty.FormatString = ""
        repoqty.HeaderText = "Week2 Qty"
        repoqty.Name = colVWKQty2
        repoqty.DecimalPlaces = 2
        repoqty.Width = 80
        repoqty.IsVisible = False
        gv_Vendor.MasterTemplate.Columns.Add(repoqty)

        repoqty = New GridViewDecimalColumn()
        repoqty.FormatString = ""
        repoqty.HeaderText = "Week3 Qty"
        repoqty.Name = colVWKQty3
        repoqty.DecimalPlaces = 2
        repoqty.Width = 80
        repoqty.IsVisible = False
        gv_Vendor.MasterTemplate.Columns.Add(repoqty)

        repoqty = New GridViewDecimalColumn()
        repoqty.FormatString = ""
        repoqty.HeaderText = "Week4 Qty"
        repoqty.Name = colVWKQty4
        repoqty.DecimalPlaces = 2
        repoqty.Width = 80
        repoqty.IsVisible = False
        gv_Vendor.MasterTemplate.Columns.Add(repoqty)

        repoqty = New GridViewDecimalColumn()
        repoqty.FormatString = ""
        repoqty.HeaderText = "Week5 Qty"
        repoqty.Name = colVWKQty5
        repoqty.DecimalPlaces = 2
        repoqty.Width = 80
        repoqty.IsVisible = False
        gv_Vendor.MasterTemplate.Columns.Add(repoqty)

        repoqty = New GridViewDecimalColumn()
        repoqty.FormatString = ""
        repoqty.HeaderText = "Week6 Qty"
        repoqty.Name = colVWKQty6
        repoqty.DecimalPlaces = 2
        repoqty.Width = 80
        repoqty.IsVisible = False
        gv_Vendor.MasterTemplate.Columns.Add(repoqty)

        repoqty = New GridViewDecimalColumn()
        repoqty.FormatString = ""
        repoqty.HeaderText = "Day1 Qty"
        repoqty.Name = colVQty1
        repoqty.DecimalPlaces = 2
        repoqty.Width = 80
        repoqty.IsVisible = False
        gv_Vendor.MasterTemplate.Columns.Add(repoqty)

        repoqty = New GridViewDecimalColumn()
        repoqty.FormatString = ""
        repoqty.HeaderText = "Day2 Qty"
        repoqty.Name = colVQty2
        repoqty.DecimalPlaces = 2
        repoqty.Width = 80
        repoqty.IsVisible = False
        gv_Vendor.MasterTemplate.Columns.Add(repoqty)

        repoqty = New GridViewDecimalColumn()
        repoqty.FormatString = ""
        repoqty.HeaderText = "Day3 Qty"
        repoqty.Name = colVQty3
        repoqty.DecimalPlaces = 2
        repoqty.Width = 80
        repoqty.IsVisible = False
        gv_Vendor.MasterTemplate.Columns.Add(repoqty)

        repoqty = New GridViewDecimalColumn()
        repoqty.FormatString = ""
        repoqty.HeaderText = "Day4 Qty"
        repoqty.Name = colVQty4
        repoqty.DecimalPlaces = 2
        repoqty.Width = 80
        repoqty.IsVisible = False
        gv_Vendor.MasterTemplate.Columns.Add(repoqty)

        repoqty = New GridViewDecimalColumn()
        repoqty.FormatString = ""
        repoqty.HeaderText = "Day5 Qty"
        repoqty.Name = colVQty5
        repoqty.DecimalPlaces = 2
        repoqty.Width = 80
        repoqty.IsVisible = False
        gv_Vendor.MasterTemplate.Columns.Add(repoqty)

        repoqty = New GridViewDecimalColumn()
        repoqty.FormatString = ""
        repoqty.HeaderText = "Day6 Qty"
        repoqty.Name = colVQty6
        repoqty.DecimalPlaces = 2
        repoqty.Width = 80
        repoqty.IsVisible = False
        gv_Vendor.MasterTemplate.Columns.Add(repoqty)

        repoqty = New GridViewDecimalColumn()
        repoqty.FormatString = ""
        repoqty.HeaderText = "Day7 Qty"
        repoqty.Name = colVQty7
        repoqty.DecimalPlaces = 2
        repoqty.Width = 80
        repoqty.IsVisible = False
        gv_Vendor.MasterTemplate.Columns.Add(repoqty)

        repoqty = New GridViewDecimalColumn()
        repoqty.FormatString = ""
        repoqty.HeaderText = "Day8 Qty"
        repoqty.Name = colVQty8
        repoqty.DecimalPlaces = 2
        repoqty.Width = 80
        repoqty.IsVisible = False
        gv_Vendor.MasterTemplate.Columns.Add(repoqty)

        repoqty = New GridViewDecimalColumn()
        repoqty.FormatString = ""
        repoqty.HeaderText = "Day9 Qty"
        repoqty.Name = colVQty9
        repoqty.DecimalPlaces = 2
        repoqty.Width = 80
        repoqty.IsVisible = False
        gv_Vendor.MasterTemplate.Columns.Add(repoqty)

        repoqty = New GridViewDecimalColumn()
        repoqty.FormatString = ""
        repoqty.HeaderText = "Day10 Qty"
        repoqty.Name = colVQty10
        repoqty.DecimalPlaces = 2
        repoqty.Width = 80
        repoqty.IsVisible = False
        gv_Vendor.MasterTemplate.Columns.Add(repoqty)

        repoqty = New GridViewDecimalColumn()
        repoqty.FormatString = ""
        repoqty.HeaderText = "Day11 Qty"
        repoqty.Name = colVQty11
        repoqty.DecimalPlaces = 2
        repoqty.Width = 80
        repoqty.IsVisible = False
        gv_Vendor.MasterTemplate.Columns.Add(repoqty)

        repoqty = New GridViewDecimalColumn()
        repoqty.FormatString = ""
        repoqty.HeaderText = "Day12 Qty"
        repoqty.Name = colVQty12
        repoqty.DecimalPlaces = 2
        repoqty.Width = 80
        repoqty.IsVisible = False
        gv_Vendor.MasterTemplate.Columns.Add(repoqty)

        repoqty = New GridViewDecimalColumn()
        repoqty.FormatString = ""
        repoqty.HeaderText = "Day13 Qty"
        repoqty.Name = colVQty13
        repoqty.DecimalPlaces = 2
        repoqty.Width = 80
        repoqty.IsVisible = False
        gv_Vendor.MasterTemplate.Columns.Add(repoqty)

        repoqty = New GridViewDecimalColumn()
        repoqty.FormatString = ""
        repoqty.HeaderText = "Day14 Qty"
        repoqty.Name = colVQty14
        repoqty.DecimalPlaces = 2
        repoqty.Width = 80
        repoqty.IsVisible = False
        gv_Vendor.MasterTemplate.Columns.Add(repoqty)

        repoqty = New GridViewDecimalColumn()
        repoqty.FormatString = ""
        repoqty.HeaderText = "Day15 Qty"
        repoqty.Name = colVQty15
        repoqty.DecimalPlaces = 2
        repoqty.Width = 80
        repoqty.IsVisible = False
        gv_Vendor.MasterTemplate.Columns.Add(repoqty)

        repoqty = New GridViewDecimalColumn()
        repoqty.FormatString = ""
        repoqty.HeaderText = "Day16 Qty"
        repoqty.Name = colVQty16
        repoqty.DecimalPlaces = 2
        repoqty.Width = 80
        repoqty.IsVisible = False
        gv_Vendor.MasterTemplate.Columns.Add(repoqty)

        repoqty = New GridViewDecimalColumn()
        repoqty.FormatString = ""
        repoqty.HeaderText = "Day17 Qty"
        repoqty.Name = colVQty17
        repoqty.DecimalPlaces = 2
        repoqty.Width = 80
        repoqty.IsVisible = False
        gv_Vendor.MasterTemplate.Columns.Add(repoqty)

        repoqty = New GridViewDecimalColumn()
        repoqty.FormatString = ""
        repoqty.HeaderText = "Day18 Qty"
        repoqty.Name = colVQty18
        repoqty.DecimalPlaces = 2
        repoqty.Width = 80
        repoqty.IsVisible = False
        gv_Vendor.MasterTemplate.Columns.Add(repoqty)

        repoqty = New GridViewDecimalColumn()
        repoqty.FormatString = ""
        repoqty.HeaderText = "Day19 Qty"
        repoqty.Name = colVQty19
        repoqty.DecimalPlaces = 2
        repoqty.Width = 80
        repoqty.IsVisible = False
        gv_Vendor.MasterTemplate.Columns.Add(repoqty)

        repoqty = New GridViewDecimalColumn()
        repoqty.FormatString = ""
        repoqty.HeaderText = "Day20 Qty"
        repoqty.Name = colVQty20
        repoqty.DecimalPlaces = 2
        repoqty.Width = 80
        repoqty.IsVisible = False
        gv_Vendor.MasterTemplate.Columns.Add(repoqty)

        repoqty = New GridViewDecimalColumn()
        repoqty.FormatString = ""
        repoqty.HeaderText = "Day21 Qty"
        repoqty.Name = colVQty21
        repoqty.DecimalPlaces = 2
        repoqty.Width = 80
        repoqty.IsVisible = False
        gv_Vendor.MasterTemplate.Columns.Add(repoqty)

        repoqty = New GridViewDecimalColumn()
        repoqty.FormatString = ""
        repoqty.HeaderText = "Day22 Qty"
        repoqty.Name = colVQty22
        repoqty.DecimalPlaces = 2
        repoqty.Width = 80
        repoqty.IsVisible = False
        gv_Vendor.MasterTemplate.Columns.Add(repoqty)

        repoqty = New GridViewDecimalColumn()
        repoqty.FormatString = ""
        repoqty.HeaderText = "Day23 Qty"
        repoqty.Name = colVQty23
        repoqty.DecimalPlaces = 2
        repoqty.Width = 80
        repoqty.IsVisible = False
        gv_Vendor.MasterTemplate.Columns.Add(repoqty)

        repoqty = New GridViewDecimalColumn()
        repoqty.FormatString = ""
        repoqty.HeaderText = "Day24 Qty"
        repoqty.Name = colVQty24
        repoqty.DecimalPlaces = 2
        repoqty.Width = 80
        repoqty.IsVisible = False
        gv_Vendor.MasterTemplate.Columns.Add(repoqty)

        repoqty = New GridViewDecimalColumn()
        repoqty.FormatString = ""
        repoqty.HeaderText = "Day25 Qty"
        repoqty.Name = colVQty25
        repoqty.DecimalPlaces = 2
        repoqty.Width = 80
        repoqty.IsVisible = False
        gv_Vendor.MasterTemplate.Columns.Add(repoqty)

        repoqty = New GridViewDecimalColumn()
        repoqty.FormatString = ""
        repoqty.HeaderText = "Day26 Qty"
        repoqty.Name = colVQty26
        repoqty.DecimalPlaces = 2
        repoqty.Width = 80
        repoqty.IsVisible = False
        gv_Vendor.MasterTemplate.Columns.Add(repoqty)

        repoqty = New GridViewDecimalColumn()
        repoqty.FormatString = ""
        repoqty.HeaderText = "Day27 Qty"
        repoqty.Name = colVQty27
        repoqty.DecimalPlaces = 2
        repoqty.Width = 80
        repoqty.IsVisible = False
        gv_Vendor.MasterTemplate.Columns.Add(repoqty)

        repoqty = New GridViewDecimalColumn()
        repoqty.FormatString = ""
        repoqty.HeaderText = "Day28 Qty"
        repoqty.Name = colVQty28
        repoqty.DecimalPlaces = 2
        repoqty.Width = 80
        repoqty.IsVisible = False
        gv_Vendor.MasterTemplate.Columns.Add(repoqty)

        repoqty = New GridViewDecimalColumn()
        repoqty.FormatString = ""
        repoqty.HeaderText = "Day29 Qty"
        repoqty.Name = colVQty29
        repoqty.DecimalPlaces = 2
        repoqty.Width = 80
        repoqty.IsVisible = False
        gv_Vendor.MasterTemplate.Columns.Add(repoqty)

        repoqty = New GridViewDecimalColumn()
        repoqty.FormatString = ""
        repoqty.HeaderText = "Day30 Qty"
        repoqty.Name = colVQty30
        repoqty.DecimalPlaces = 2
        repoqty.Width = 80
        repoqty.IsVisible = False
        gv_Vendor.MasterTemplate.Columns.Add(repoqty)

        repoqty = New GridViewDecimalColumn()
        repoqty.FormatString = ""
        repoqty.HeaderText = "Day31 Qty"
        repoqty.Name = colVQty31
        repoqty.DecimalPlaces = 2
        repoqty.Width = 80
        repoqty.IsVisible = False
        gv_Vendor.MasterTemplate.Columns.Add(repoqty)

        '===month====
        repoqty = New GridViewDecimalColumn()
        repoqty.FormatString = ""
        repoqty.HeaderText = "Jan(Qty)"
        repoqty.Name = colVMonth1
        repoqty.DecimalPlaces = 2
        repoqty.Width = 80
        repoqty.IsVisible = False
        gv_Vendor.MasterTemplate.Columns.Add(repoqty)

        repoqty = New GridViewDecimalColumn()
        repoqty.FormatString = ""
        repoqty.HeaderText = "Feb(Qty)"
        repoqty.Name = colVMonth2
        repoqty.DecimalPlaces = 2
        repoqty.Width = 80
        repoqty.IsVisible = False
        gv_Vendor.MasterTemplate.Columns.Add(repoqty)

        repoqty = New GridViewDecimalColumn()
        repoqty.FormatString = ""
        repoqty.HeaderText = "March(Qty)"
        repoqty.Name = colVMonth3
        repoqty.DecimalPlaces = 2
        repoqty.Width = 80
        repoqty.IsVisible = False
        gv_Vendor.MasterTemplate.Columns.Add(repoqty)

        repoqty = New GridViewDecimalColumn()
        repoqty.FormatString = ""
        repoqty.HeaderText = "April(Qty)"
        repoqty.Name = colVMonth4
        repoqty.DecimalPlaces = 2
        repoqty.Width = 80
        repoqty.IsVisible = False
        gv_Vendor.MasterTemplate.Columns.Add(repoqty)

        repoqty = New GridViewDecimalColumn()
        repoqty.FormatString = ""
        repoqty.HeaderText = "May(Qty)"
        repoqty.Name = colVMonth5
        repoqty.DecimalPlaces = 2
        repoqty.Width = 80
        repoqty.IsVisible = False
        gv_Vendor.MasterTemplate.Columns.Add(repoqty)

        repoqty = New GridViewDecimalColumn()
        repoqty.FormatString = ""
        repoqty.HeaderText = "June(Qty)"
        repoqty.Name = colVMonth6
        repoqty.DecimalPlaces = 2
        repoqty.Width = 80
        repoqty.IsVisible = False
        gv_Vendor.MasterTemplate.Columns.Add(repoqty)

        repoqty = New GridViewDecimalColumn()
        repoqty.FormatString = ""
        repoqty.HeaderText = "July(Qty)"
        repoqty.Name = colVMonth7
        repoqty.DecimalPlaces = 2
        repoqty.Width = 80
        repoqty.IsVisible = False
        gv_Vendor.MasterTemplate.Columns.Add(repoqty)

        repoqty = New GridViewDecimalColumn()
        repoqty.FormatString = ""
        repoqty.HeaderText = "Aug(Qty)"
        repoqty.Name = colVMonth8
        repoqty.DecimalPlaces = 2
        repoqty.Width = 80
        repoqty.IsVisible = False
        gv_Vendor.MasterTemplate.Columns.Add(repoqty)

        repoqty = New GridViewDecimalColumn()
        repoqty.FormatString = ""
        repoqty.HeaderText = "Sep(Qty)"
        repoqty.Name = colVMonth9
        repoqty.DecimalPlaces = 2
        repoqty.Width = 80
        repoqty.IsVisible = False
        gv_Vendor.MasterTemplate.Columns.Add(repoqty)

        repoqty = New GridViewDecimalColumn()
        repoqty.FormatString = ""
        repoqty.HeaderText = "Oct(Qty)"
        repoqty.Name = colVMonth10
        repoqty.DecimalPlaces = 2
        repoqty.Width = 80
        repoqty.IsVisible = False
        gv_Vendor.MasterTemplate.Columns.Add(repoqty)

        repoqty = New GridViewDecimalColumn()
        repoqty.FormatString = ""
        repoqty.HeaderText = "Nov(Qty)"
        repoqty.Name = colVMonth11
        repoqty.DecimalPlaces = 2
        repoqty.Width = 80
        repoqty.IsVisible = False
        gv_Vendor.MasterTemplate.Columns.Add(repoqty)

        repoqty = New GridViewDecimalColumn()
        repoqty.FormatString = ""
        repoqty.HeaderText = "Dec(Qty)"
        repoqty.Name = colVMonth12
        repoqty.DecimalPlaces = 2
        repoqty.Width = 80
        repoqty.IsVisible = False
        gv_Vendor.MasterTemplate.Columns.Add(repoqty)
        '=============

        repoLineNo = New GridViewTextBoxColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "Remarks"
        repoLineNo.Name = colVRemarks
        repoLineNo.Width = 120
        repoLineNo.MaxLength = 100
        gv_Vendor.MasterTemplate.Columns.Add(repoLineNo)

        gv_Vendor.AllowDeleteRow = True
        gv_Vendor.AllowAddNewRow = False
        gv_Vendor.ShowGroupPanel = False
        gv_Vendor.AllowColumnReorder = True
        gv_Vendor.AllowRowReorder = False
        gv_Vendor.EnableSorting = False
        gv_Vendor.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv_Vendor.MasterTemplate.ShowRowHeaderColumn = False
        gv_Vendor.TableElement.TableHeaderHeight = 40
    End Sub

    Private Sub GridVisibility()
        If gv1.Columns.Count <= 0 Then
            Exit Sub
        End If

        Dim days As Integer = 0
        Dim Week As Integer = 0
        If Not btnMonthly.IsChecked Then
            days = DateTime.DaysInMonth(CInt(clsCommon.myCdbl(clsCommon.GetPrintDate(dtpMonth.Text, "yyyy"))), CInt(clsCommon.myCdbl(clsCommon.GetPrintDate(dtpMonth.Text, "MM"))))
            Dim date1 As String = clsCommon.myCstr(clsCommon.myCstr(days) + "/" + clsCommon.GetPrintDate(dtpMonth.Text, "MMM") + "/" + clsCommon.GetPrintDate(dtpMonth.Text, "yyyy"))
            Week = clsPurchaseSchedule.GetNoOfWeekInMonth(dtpMonth.Text)
        End If

        TotalNo_Count_For_Sch_Qty = 0

        If btnWeek.IsChecked Then
            For ii As Integer = 1 To 6
                gv_Vendor.Columns("VWKQty" + clsCommon.myCstr(ii)).IsVisible = btnWeek.IsChecked
                gv1.Columns("WKQty" + clsCommon.myCstr(ii)).IsVisible = btnWeek.IsChecked
                TotalNo_Count_For_Sch_Qty += 1
                If ii > Week Then
                    TotalNo_Count_For_Sch_Qty -= 1
                    gv_Vendor.Columns("VWKQty" + clsCommon.myCstr(ii)).IsVisible = False
                    gv1.Columns("WKQty" + clsCommon.myCstr(ii)).IsVisible = False
                End If
            Next
            If Not btnDaily.IsChecked Then
                For ii As Integer = 1 To 31
                    gv_Vendor.Columns("VQty" + clsCommon.myCstr(ii)).IsVisible = False
                    gv1.Columns("Qty" + clsCommon.myCstr(ii)).IsVisible = False
                Next
            End If
            If Not btnMonthly.IsChecked Then
                For ii As Integer = 1 To 12
                    gv1.Columns("Month" + clsCommon.myCstr(ii)).IsVisible = False
                    gv_Vendor.Columns("VMonth" + clsCommon.myCstr(ii)).IsVisible = False
                Next
            End If
        End If
        '==========================================
        If btnDaily.IsChecked Then
            For ii As Integer = 1 To 31
                gv_Vendor.Columns("VQty" + clsCommon.myCstr(ii)).IsVisible = btnDaily.IsChecked
                gv1.Columns("Qty" + clsCommon.myCstr(ii)).IsVisible = btnDaily.IsChecked
                TotalNo_Count_For_Sch_Qty += 1
                If ii > days Then
                    TotalNo_Count_For_Sch_Qty -= 1
                    gv_Vendor.Columns("VQty" + clsCommon.myCstr(ii)).IsVisible = False
                    gv1.Columns("Qty" + clsCommon.myCstr(ii)).IsVisible = False
                End If
            Next
            If Not btnWeek.IsChecked Then
                For ii As Integer = 1 To 6
                    gv_Vendor.Columns("VWKQty" + clsCommon.myCstr(ii)).IsVisible = False
                    gv1.Columns("WKQty" + clsCommon.myCstr(ii)).IsVisible = False
                Next
            End If
            If Not btnMonthly.IsChecked Then
                For ii As Integer = 1 To 12
                    gv1.Columns("Month" + clsCommon.myCstr(ii)).IsVisible = False
                    gv_Vendor.Columns("VMonth" + clsCommon.myCstr(ii)).IsVisible = False
                Next
            End If
        End If

        '==================
        If btnMonthly.IsChecked Then
            For ii As Integer = 1 To 12
                TotalNo_Count_For_Sch_Qty += 1
                gv1.Columns("Month" + clsCommon.myCstr(ii)).IsVisible = True
                gv_Vendor.Columns("VMonth" + clsCommon.myCstr(ii)).IsVisible = True
            Next

            If Not btnWeek.IsChecked Then
                For ii As Integer = 1 To 6
                    gv_Vendor.Columns("VWKQty" + clsCommon.myCstr(ii)).IsVisible = False
                    gv1.Columns("WKQty" + clsCommon.myCstr(ii)).IsVisible = False
                Next
            End If
            If Not btnDaily.IsChecked Then
                For ii As Integer = 1 To 31
                    gv_Vendor.Columns("VQty" + clsCommon.myCstr(ii)).IsVisible = False
                    gv1.Columns("Qty" + clsCommon.myCstr(ii)).IsVisible = False
                Next
            End If
        End If
    End Sub

    Private Sub txtDocNo__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtDocNo._MYNavigator
        LoadData(txtDocNo.Value, NavType)
    End Sub

    Private Sub txtDocNo__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtDocNo._MYValidating
        Dim qry As String = "select count(*) from TSPL_PO_SCH_HEAD where Document_Code='" + txtDocNo.Value + "'"
        Dim check As Integer = clsDBFuncationality.getSingleValue(qry)

        If check > 0 Then
            txtDocNo.MyReadOnly = True
        Else
            txtDocNo.MyReadOnly = False
        End If

        If txtDocNo.MyReadOnly OrElse isButtonClicked Then
            txtDocNo.Value = clsPurchaseSchedule.GetFinder("", txtDocNo.Value, isButtonClicked)
            If clsCommon.myLen(txtDocNo.Value) > 0 Then
                LoadData(txtDocNo.Value, NavigatorType.Current)
            Else
                Funreset()
            End If
        End If
    End Sub

    Private Sub LoadData(ByVal strCode As String, ByVal NavType As NavigatorType)
        Dim obj As New clsPurchaseSchedule()
        Try
            obj = clsPurchaseSchedule.GetData(txtDocNo.Value, NavType)
            isInsideLoadData = False
            isNewEntry = True
            btnunpost.Visible = False

            gv_Vendor.Rows.Clear()
            gv_Vendor.Rows.AddNew()
            gv1.Rows.Clear()
            gv1.Rows.AddNew()

            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Document_Code) > 0 Then
                isInsideLoadData = True
                isNewEntry = False


                txtDocNo.Value = obj.Document_Code
                txtDate.Text = obj.Document_Date
                txtDesc.Text = obj.Description
                txtVendorNo.Value = obj.Vendor_Code
                lblVendorName.Text = obj.Vendor_Name
                cboPOType.SelectedValue = obj.PO_Type
                txtPONo.Value = obj.PO_Code
                txtPODesc.Text = obj.PO_Desc
                btnWeek.IsChecked = clsCommon.myCBool(IIf(obj.Schedule_Type = "W", True, False))
                btnDaily.IsChecked = clsCommon.myCBool(IIf(obj.Schedule_Type = "D", True, False))
                btnMonthly.IsChecked = clsCommon.myCBool(IIf(obj.Schedule_Type = "M", True, False))
                dtpMonth.Text = obj.Schedule_Month
                txtRev_No.Text = obj.Revision_No

                GridVisibility()

                If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                    For Each objtr As clsPurchaseScheduleDetail In obj.Arr
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colLineno).Value = objtr.Line_No
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemCode).Value = objtr.Item_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemName).Value = objtr.Item_Name
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemType).Value = objtr.Item_Type
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemUnit).Value = objtr.Unit_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colPONo).Value = objtr.PO_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colPersType).Value = objtr.Pers_Type
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colPers).Value = objtr.Pers_Value
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colPODate).Value = objtr.PO_Date
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colPOQty).Value = clsCommon.myCdbl(clsPurchaseOrderDetail.GetBalancePOQtyBySchedule(objtr.PO_Code, objtr.Item_Code, obj.Document_Code, objtr.Unit_Code))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colSchQty).Value = objtr.Schedule_Qty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colWKQty1).Value = objtr.Week1_Qty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colWKQty2).Value = objtr.Week2_Qty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colWKQty3).Value = objtr.Week3_Qty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colWKQty4).Value = objtr.Week4_Qty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colWKQty5).Value = objtr.Week5_Qty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colWKQty6).Value = objtr.Week6_Qty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colQty1).Value = objtr.Day1_Qty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colQty2).Value = objtr.Day2_Qty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colQty3).Value = objtr.Day3_Qty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colQty4).Value = objtr.Day4_Qty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colQty5).Value = objtr.Day5_Qty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colQty6).Value = objtr.Day6_Qty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colQty7).Value = objtr.Day7_Qty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colQty8).Value = objtr.Day8_Qty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colQty9).Value = objtr.Day9_Qty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colQty10).Value = objtr.Day10_Qty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colQty11).Value = objtr.Day11_Qty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colQty12).Value = objtr.Day12_Qty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colQty13).Value = objtr.Day13_Qty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colQty14).Value = objtr.Day14_Qty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colQty15).Value = objtr.Day15_Qty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colQty16).Value = objtr.Day16_Qty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colQty17).Value = objtr.Day17_Qty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colQty18).Value = objtr.Day18_Qty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colQty19).Value = objtr.Day19_Qty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colQty20).Value = objtr.Day20_Qty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colQty21).Value = objtr.Day21_Qty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colQty22).Value = objtr.Day22_Qty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colQty23).Value = objtr.Day23_Qty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colQty24).Value = objtr.Day24_Qty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colQty25).Value = objtr.Day25_Qty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colQty26).Value = objtr.Day26_Qty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colQty27).Value = objtr.Day27_Qty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colQty28).Value = objtr.Day28_Qty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colQty29).Value = objtr.Day29_Qty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colQty30).Value = objtr.Day30_Qty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colQty31).Value = objtr.Day31_Qty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colMonth1).Value = objtr.Month1_Qty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colMonth2).Value = objtr.Month2_Qty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colMonth3).Value = objtr.Month3_Qty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colMonth4).Value = objtr.Month4_Qty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colMonth5).Value = objtr.Month5_Qty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colMonth6).Value = objtr.Month6_Qty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colMonth7).Value = objtr.Month7_Qty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colMonth8).Value = objtr.Month8_Qty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colMonth9).Value = objtr.Month9_Qty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colMonth10).Value = objtr.Month10_Qty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colMonth11).Value = objtr.Month11_Qty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colMonth12).Value = objtr.Month12_Qty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colRemarks).Value = objtr.Remarks

                        gv1.Rows.AddNew()
                    Next
                End If

                If obj.Arr_Vendor IsNot Nothing AndAlso obj.Arr_Vendor.Count > 0 Then
                    For Each objtr As clsPurchaseScheduleVendorDetail In obj.Arr_Vendor
                        gv_Vendor.Rows(gv_Vendor.Rows.Count - 1).Cells(colVLineno).Value = objtr.Line_No
                        gv_Vendor.Rows(gv_Vendor.Rows.Count - 1).Cells(colVItemCode).Value = objtr.Item_Code
                        gv_Vendor.Rows(gv_Vendor.Rows.Count - 1).Cells(colVItemName).Value = objtr.Item_Name
                        gv_Vendor.Rows(gv_Vendor.Rows.Count - 1).Cells(colVItemType).Value = objtr.Item_Type
                        gv_Vendor.Rows(gv_Vendor.Rows.Count - 1).Cells(colVItemUnit).Value = objtr.Unit_Code
                        gv_Vendor.Rows(gv_Vendor.Rows.Count - 1).Cells(colVPONo).Value = objtr.PO_Code
                        gv_Vendor.Rows(gv_Vendor.Rows.Count - 1).Cells(colVPODate).Value = objtr.PO_Date
                        gv_Vendor.Rows(gv_Vendor.Rows.Count - 1).Cells(colVPOQty).Value = clsCommon.myCdbl(clsPurchaseOrderDetail.GetBalancePOQtyBySchedule(objtr.PO_Code, objtr.Item_Code, obj.Document_Code, objtr.Unit_Code))
                        gv_Vendor.Rows(gv_Vendor.Rows.Count - 1).Cells(colVSchQty).Value = objtr.Schedule_Qty
                        gv_Vendor.Rows(gv_Vendor.Rows.Count - 1).Cells(colVWKQty1).Value = objtr.Week1_Qty
                        gv_Vendor.Rows(gv_Vendor.Rows.Count - 1).Cells(colVWKQty2).Value = objtr.Week2_Qty
                        gv_Vendor.Rows(gv_Vendor.Rows.Count - 1).Cells(colVWKQty3).Value = objtr.Week3_Qty
                        gv_Vendor.Rows(gv_Vendor.Rows.Count - 1).Cells(colVWKQty4).Value = objtr.Week4_Qty
                        gv_Vendor.Rows(gv_Vendor.Rows.Count - 1).Cells(colVWKQty5).Value = objtr.Week5_Qty
                        gv_Vendor.Rows(gv_Vendor.Rows.Count - 1).Cells(colVWKQty6).Value = objtr.Week6_Qty
                        gv_Vendor.Rows(gv_Vendor.Rows.Count - 1).Cells(colVQty1).Value = objtr.Day1_Qty
                        gv_Vendor.Rows(gv_Vendor.Rows.Count - 1).Cells(colVQty2).Value = objtr.Day2_Qty
                        gv_Vendor.Rows(gv_Vendor.Rows.Count - 1).Cells(colVQty3).Value = objtr.Day3_Qty
                        gv_Vendor.Rows(gv_Vendor.Rows.Count - 1).Cells(colVQty4).Value = objtr.Day4_Qty
                        gv_Vendor.Rows(gv_Vendor.Rows.Count - 1).Cells(colVQty5).Value = objtr.Day5_Qty
                        gv_Vendor.Rows(gv_Vendor.Rows.Count - 1).Cells(colVQty6).Value = objtr.Day6_Qty
                        gv_Vendor.Rows(gv_Vendor.Rows.Count - 1).Cells(colVQty7).Value = objtr.Day7_Qty
                        gv_Vendor.Rows(gv_Vendor.Rows.Count - 1).Cells(colVQty8).Value = objtr.Day8_Qty
                        gv_Vendor.Rows(gv_Vendor.Rows.Count - 1).Cells(colVQty9).Value = objtr.Day9_Qty
                        gv_Vendor.Rows(gv_Vendor.Rows.Count - 1).Cells(colVQty10).Value = objtr.Day10_Qty
                        gv_Vendor.Rows(gv_Vendor.Rows.Count - 1).Cells(colVQty11).Value = objtr.Day11_Qty
                        gv_Vendor.Rows(gv_Vendor.Rows.Count - 1).Cells(colVQty12).Value = objtr.Day12_Qty
                        gv_Vendor.Rows(gv_Vendor.Rows.Count - 1).Cells(colVQty13).Value = objtr.Day13_Qty
                        gv_Vendor.Rows(gv_Vendor.Rows.Count - 1).Cells(colVQty14).Value = objtr.Day14_Qty
                        gv_Vendor.Rows(gv_Vendor.Rows.Count - 1).Cells(colVQty15).Value = objtr.Day15_Qty
                        gv_Vendor.Rows(gv_Vendor.Rows.Count - 1).Cells(colVQty16).Value = objtr.Day16_Qty
                        gv_Vendor.Rows(gv_Vendor.Rows.Count - 1).Cells(colVQty17).Value = objtr.Day17_Qty
                        gv_Vendor.Rows(gv_Vendor.Rows.Count - 1).Cells(colVQty18).Value = objtr.Day18_Qty
                        gv_Vendor.Rows(gv_Vendor.Rows.Count - 1).Cells(colVQty19).Value = objtr.Day19_Qty
                        gv_Vendor.Rows(gv_Vendor.Rows.Count - 1).Cells(colVQty20).Value = objtr.Day20_Qty
                        gv_Vendor.Rows(gv_Vendor.Rows.Count - 1).Cells(colVQty21).Value = objtr.Day21_Qty
                        gv_Vendor.Rows(gv_Vendor.Rows.Count - 1).Cells(colVQty22).Value = objtr.Day22_Qty
                        gv_Vendor.Rows(gv_Vendor.Rows.Count - 1).Cells(colVQty23).Value = objtr.Day23_Qty
                        gv_Vendor.Rows(gv_Vendor.Rows.Count - 1).Cells(colVQty24).Value = objtr.Day24_Qty
                        gv_Vendor.Rows(gv_Vendor.Rows.Count - 1).Cells(colVQty25).Value = objtr.Day25_Qty
                        gv_Vendor.Rows(gv_Vendor.Rows.Count - 1).Cells(colVQty26).Value = objtr.Day26_Qty
                        gv_Vendor.Rows(gv_Vendor.Rows.Count - 1).Cells(colVQty27).Value = objtr.Day27_Qty
                        gv_Vendor.Rows(gv_Vendor.Rows.Count - 1).Cells(colVQty28).Value = objtr.Day28_Qty
                        gv_Vendor.Rows(gv_Vendor.Rows.Count - 1).Cells(colVQty29).Value = objtr.Day29_Qty
                        gv_Vendor.Rows(gv_Vendor.Rows.Count - 1).Cells(colVQty30).Value = objtr.Day30_Qty
                        gv_Vendor.Rows(gv_Vendor.Rows.Count - 1).Cells(colVQty31).Value = objtr.Day31_Qty
                        gv_Vendor.Rows(gv_Vendor.Rows.Count - 1).Cells(colVMonth1).Value = objtr.Month1_Qty
                        gv_Vendor.Rows(gv_Vendor.Rows.Count - 1).Cells(colVMonth2).Value = objtr.Month2_Qty
                        gv_Vendor.Rows(gv_Vendor.Rows.Count - 1).Cells(colVMonth3).Value = objtr.Month3_Qty
                        gv_Vendor.Rows(gv_Vendor.Rows.Count - 1).Cells(colVMonth4).Value = objtr.Month4_Qty
                        gv_Vendor.Rows(gv_Vendor.Rows.Count - 1).Cells(colVMonth5).Value = objtr.Month5_Qty
                        gv_Vendor.Rows(gv_Vendor.Rows.Count - 1).Cells(colVMonth6).Value = objtr.Month6_Qty
                        gv_Vendor.Rows(gv_Vendor.Rows.Count - 1).Cells(colVMonth7).Value = objtr.Month7_Qty
                        gv_Vendor.Rows(gv_Vendor.Rows.Count - 1).Cells(colVMonth8).Value = objtr.Month8_Qty
                        gv_Vendor.Rows(gv_Vendor.Rows.Count - 1).Cells(colVMonth9).Value = objtr.Month9_Qty
                        gv_Vendor.Rows(gv_Vendor.Rows.Count - 1).Cells(colVMonth10).Value = objtr.Month10_Qty
                        gv_Vendor.Rows(gv_Vendor.Rows.Count - 1).Cells(colVMonth11).Value = objtr.Month11_Qty
                        gv_Vendor.Rows(gv_Vendor.Rows.Count - 1).Cells(colVMonth12).Value = objtr.Month12_Qty
                        gv_Vendor.Rows(gv_Vendor.Rows.Count - 1).Cells(colVRemarks).Value = objtr.Remarks

                        gv_Vendor.Rows.AddNew()
                    Next
                End If
                '..............................

                btnsave.Text = "Update"
                btnsave.Enabled = True
                btndelete.Enabled = True
                btnpost.Enabled = True
                UsLock1.Status = ERPTransactionStatus.Open

                If obj.Is_Post = 1 Then
                    btnsave.Enabled = False
                    btndelete.Enabled = False
                    btnpost.Enabled = False
                    UsLock1.Status = ERPTransactionStatus.Approved
                End If

                txtVendorNo.Enabled = False
                txtPONo.Enabled = False
                cboPOType.Enabled = False
                dtpMonth.Enabled = False
                btnDaily.Enabled = False
                btnMonthly.Enabled = False
                btnWeek.Enabled = False

                UcAttachment1.LoadData(txtDocNo.Value)
            Else
                Funreset()
            End If
        Catch ex As Exception
            isNewEntry = True
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isInsideLoadData = False
            obj = Nothing
        End Try
    End Sub

    Private Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Funreset()
    End Sub

    Private Sub txtPONo__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtPONo._MYValidating
        Try
            gv1.Rows.Clear()
            gv1.Rows.AddNew()
            gv_Vendor.Rows.Clear()
            gv_Vendor.Rows.AddNew()

            If clsCommon.myLen(txtVendorNo.Value) <= 0 Then
                RadPageView1.SelectedPage = RadPageViewPage1
                txtVendorNo.Focus()
                txtVendorNo.Focus()
                Errorcontrol.SetError(lblVendorName, "Select Vendor")
                Throw New Exception("Select Vendor")
            Else
                Errorcontrol.ResetError(lblVendorName)
            End If

            If clsCommon.myLen(dtpMonth.Text) <= 0 Then
                RadPageView1.SelectedPage = RadPageViewPage1
                dtpMonth.Focus()
                dtpMonth.Focus()
                Errorcontrol.SetError(dtpMonth, "Select Schedule Month")
                Throw New Exception("Select Schedule Month")
            Else
                Errorcontrol.ResetError(dtpMonth)
            End If

            If Not btnDaily.IsChecked AndAlso Not btnWeek.IsChecked AndAlso Not btnMonthly.IsChecked Then
                RadPageView1.SelectedPage = RadPageViewPage1
                Errorcontrol.SetError(btnDaily, "Select Schedule Type(Monthly/Weekly/Daily)")
                Throw New Exception("Select Schedule Type(Monthly/Weekly/Daily)")
            Else
                Errorcontrol.ResetError(btnDaily)
            End If

            Dim days As Integer = 0
            If Not btnMonthly.IsChecked Then
                days = DateTime.DaysInMonth(CInt(clsCommon.myCdbl(clsCommon.GetPrintDate(dtpMonth.Text, "yyyy"))), CInt(clsCommon.myCdbl(clsCommon.GetPrintDate(dtpMonth.Text, "MM"))))
            End If


            Dim frm As New FrmPOSchChild()
            frm._Sch_Doc_No = clsCommon.myCstr(txtDocNo.Value)
            frm._PO_Type = clsCommon.myCstr(cboPOType.SelectedValue)
            If btnMonthly.IsChecked Then
                frm._Sch_Month = dtpMonth.Text
            Else
                frm._Sch_Month = clsCommon.GetPrintDate(clsCommon.myCDate(clsCommon.myCstr(days) + "/" + clsCommon.GetPrintDate(dtpMonth.Text, "MM") + "/" + clsCommon.GetPrintDate(dtpMonth.Text, "yyyy")), "dd/MMM/yyyy")
            End If
            frm._Vendor_Code = clsCommon.myCstr(txtVendorNo.Value)
            If btnDaily.IsChecked Then
                frm._Sch_type = "D"
            ElseIf btnMonthly.IsChecked Then
                frm._Sch_type = "M"
            ElseIf btnWeek.IsChecked Then
                frm._Sch_type = "W"
            End If
            frm.WindowState = FormWindowState.Maximized
            frm.ShowDialog()

            If frm.Arr IsNot Nothing AndAlso frm.Arr.Count > 0 Then
                For Each objtr As clsPurchaseScheduleDetail In frm.Arr
                    If clsCommon.myLen(txtPONo.Value) <= 0 Then
                        txtPONo.Value = objtr.PO_Code
                        txtPODesc.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select description from tspl_purchase_order_head where purchaseorder_no='" + objtr.PO_Code + "'"))
                    End If

                    If clsCommon.CompairString(cboPOType.SelectedValue, "") = CompairStringResult.Equal Then
                        cboPOType.SelectedValue = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select purchaseorder_type from tspl_purchase_order_head where purchaseorder_no='" + objtr.PO_Code + "'"))
                    End If

                    
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colLineno).Value = gv1.Rows.Count
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colItemCode).Value = objtr.Item_Code
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colItemName).Value = clsItemMaster.GetItemName(objtr.Item_Code, Nothing)
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colItemType).Value = clsItemMaster.GetItemType(objtr.Item_Code, Nothing)
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colItemUnit).Value = objtr.Unit_Code
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colPONo).Value = objtr.PO_Code
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colPODate).Value = objtr.PO_Date
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colPOQty).Value = objtr.PO_Qty

                    gv1.Rows.AddNew()

                    '====vendor grid===
                    gv_Vendor.Rows(gv_Vendor.Rows.Count - 1).Cells(colVLineno).Value = gv_Vendor.Rows.Count
                    gv_Vendor.Rows(gv_Vendor.Rows.Count - 1).Cells(colVItemCode).Value = objtr.Item_Code
                    gv_Vendor.Rows(gv_Vendor.Rows.Count - 1).Cells(colVItemName).Value = clsItemMaster.GetItemName(objtr.Item_Code, Nothing)
                    gv_Vendor.Rows(gv_Vendor.Rows.Count - 1).Cells(colVItemType).Value = clsItemMaster.GetItemType(objtr.Item_Code, Nothing)
                    gv_Vendor.Rows(gv_Vendor.Rows.Count - 1).Cells(colVItemUnit).Value = objtr.Unit_Code
                    gv_Vendor.Rows(gv_Vendor.Rows.Count - 1).Cells(colVPONo).Value = objtr.PO_Code
                    gv_Vendor.Rows(gv_Vendor.Rows.Count - 1).Cells(colVPODate).Value = objtr.PO_Date
                    gv_Vendor.Rows(gv_Vendor.Rows.Count - 1).Cells(colVPOQty).Value = objtr.PO_Qty

                    gv_Vendor.Rows.AddNew()
                Next
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtVendorNo__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtVendorNo._MYValidating
        txtVendorNo.Value = clsVendorMaster.getFinder(" TSPL_VENDOR_MASTER.Status='N'", txtVendorNo.Value, isButtonClicked)
        lblVendorName.Text = clsVendorMaster.GetName(txtVendorNo.Value, Nothing)
    End Sub

    Private Sub btnDaily_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles btnDaily.ToggleStateChanged, btnWeek.ToggleStateChanged, btnMonthly.ToggleStateChanged
        If isInsideLoadData = True Then
            Exit Sub
        End If
        If btnMonthly.IsChecked Then
            MyLabel1.Text = "Year"
            'dtpMonth.CustomFormat = "yyyy"
        Else
            MyLabel1.Text = "Month"
            'dtpMonth.CustomFormat = "MMM yyyy"
        End If
        isInsideLoadData = True
        ResetGridQtyColumns()
        GridVisibility()
        isInsideLoadData = False
    End Sub

    Sub ResetGridQtyColumns()
        For ii As Integer = 1 To 31 'daily
            For Each grow As GridViewRowInfo In gv1.Rows
                If clsCommon.myLen(grow.Cells(colItemCode)) > 0 Then
                    grow.Cells(colSchQty).Value = Nothing
                    grow.Cells("Qty" + clsCommon.myCstr(ii)).Value = Nothing
                    gv_Vendor.Rows(grow.Index).Cells(colVSchQty).Value = Nothing
                    gv_Vendor.Rows(grow.Index).Cells("VQty" + clsCommon.myCstr(ii)).Value = Nothing
                End If
            Next
        Next
        For ii As Integer = 1 To 12 'monthly
            For Each grow As GridViewRowInfo In gv1.Rows
                If clsCommon.myLen(grow.Cells(colItemCode)) > 0 Then
                    grow.Cells(colSchQty).Value = Nothing
                    grow.Cells("Month" + clsCommon.myCstr(ii)).Value = Nothing
                    gv_Vendor.Rows(grow.Index).Cells(colVSchQty).Value = Nothing
                    gv_Vendor.Rows(grow.Index).Cells("VMonth" + clsCommon.myCstr(ii)).Value = Nothing
                End If
            Next
        Next
        For ii As Integer = 1 To 6 'weekly
            For Each grow As GridViewRowInfo In gv1.Rows
                If clsCommon.myLen(grow.Cells(colItemCode)) > 0 Then
                    grow.Cells(colSchQty).Value = Nothing
                    grow.Cells("WKQty" + clsCommon.myCstr(ii)).Value = Nothing
                    gv_Vendor.Rows(grow.Index).Cells(colVSchQty).Value = Nothing
                    gv_Vendor.Rows(grow.Index).Cells("VWKQty" + clsCommon.myCstr(ii)).Value = Nothing
                End If
            Next
        Next
    End Sub

    Private Sub dtpMonth_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles dtpMonth.Validating
        GridVisibility()
    End Sub

    Private Sub btnclose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnclose.Click
        clsERPFuncationality.closeForm(Me)
    End Sub

    Private Sub btndelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btndelete.Click
        Try
            If clsCommon.myLen(txtDocNo.Value) <= 0 Then
                RadPageView1.SelectedPage = RadPageViewPage1
                txtDocNo.Focus()
                txtDocNo.Select()
                Errorcontrol.SetError(txtDocNo, "Select document no first.")
                Throw New Exception("Select document no first.")
            Else
                Errorcontrol.ResetError(txtDocNo)
            End If

            If myMessages.deleteConfirm() Then
                If clsPurchaseSchedule.DeleteData(txtDocNo.Value) Then
                    myMessages.delete()
                    Funreset()
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnpost_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnpost.Click
        Try
            If clsCommon.myLen(txtDocNo.Value) <= 0 Then
                RadPageView1.SelectedPage = RadPageViewPage1
                txtDocNo.Focus()
                txtDocNo.Select()
                Errorcontrol.SetError(txtDocNo, "Select document no first.")
                Throw New Exception("Select document no first.")
            Else
                Errorcontrol.ResetError(txtDocNo)
            End If

            If myMessages.postConfirm() Then
                If AllowToSave() Then
                    SaveData(True)
                    If clsPurchaseSchedule.PostData(txtDocNo.Value) Then
                        myMessages.post()
                        LoadData(txtDocNo.Value, NavigatorType.Current)
                    End If
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Function AllowToSave() As Boolean
        Try
            If AllowFutureDateTransaction(txtDate.Value, Nothing) = False Then
                txtDate.Focus()
                Return False
            End If
            If clsCommon.myLen(txtVendorNo.Value) <= 0 Then
                RadPageView1.SelectedPage = RadPageViewPage1
                txtVendorNo.Focus()
                txtVendorNo.Select()
                Errorcontrol.SetError(lblVendorName, "Select Vendor Name")
                Throw New Exception("Select Vendor Name")
            Else
                Errorcontrol.ResetError(lblVendorName)
            End If

            If clsCommon.CompairString(cboPOType.SelectedValue, "") = CompairStringResult.Equal Then
                RadPageView1.SelectedPage = RadPageViewPage1
                cboPOType.Select()
                Errorcontrol.SetError(cboPOType, "Select PO Type")
                Throw New Exception("Select PO Type")
            Else
                Errorcontrol.ResetError(cboPOType)
            End If

            If clsCommon.myLen(dtpMonth.Text) <= 0 Then
                RadPageView1.SelectedPage = RadPageViewPage1
                dtpMonth.Focus()
                dtpMonth.Focus()
                Errorcontrol.SetError(dtpMonth, "Select Schedule Month")
                Throw New Exception("Select Schedule Month")
            Else
                Errorcontrol.ResetError(dtpMonth)
            End If

            If Not btnDaily.IsChecked AndAlso Not btnWeek.IsChecked AndAlso Not btnMonthly.IsChecked Then
                RadPageView1.SelectedPage = RadPageViewPage1
                Errorcontrol.SetError(btnDaily, "Select Schedule Type(Monthly/Weekly/Daily)")
                Throw New Exception("Select Schedule Type(Monthly/Weekly/Daily)")
            Else
                Errorcontrol.ResetError(btnDaily)
            End If

            If clsCommon.myLen(txtPONo.Value) <= 0 Then
                RadPageView1.SelectedPage = RadPageViewPage1
                txtPONo.Focus()
                txtPONo.Select()
                Errorcontrol.SetError(txtPODesc, "Select Purchase Detail")
                Throw New Exception("Select Purchase Detail")
            Else
                Errorcontrol.ResetError(txtPODesc)
            End If

            Dim schedule_qty As Decimal = Nothing
            Dim Icode As String = Nothing
            Dim total_WK_qty As Decimal = Nothing
            Dim total_DD_qty As Decimal = Nothing
            Dim total_MM_Qty As Decimal = Nothing
            Dim orderqty As Decimal = Nothing

            For ii As Integer = 0 To gv1.Rows.Count - 1
                total_DD_qty = 0
                total_MM_Qty = 0
                total_WK_qty = 0

                Icode = clsCommon.myCstr(gv1.Rows(ii).Cells(colItemCode).Value)
                schedule_qty = clsCommon.myCdbl(gv1.Rows(ii).Cells(colSchQty).Value)
                orderqty = clsCommon.myCdbl(gv1.Rows(ii).Cells(colPOQty).Value)

                If clsCommon.myLen(Icode) <= 0 AndAlso ii = 0 Then
                    RadPageView1.SelectedPage = RadPageViewPage1
                    gv1.CurrentRow = gv1.Rows(ii)
                    Throw New Exception("No item found for save of selected PO.")
                End If

                If clsCommon.myLen(Icode) > 0 AndAlso schedule_qty <= 0 Then
                    RadPageView1.SelectedPage = RadPageViewPage1
                    gv1.CurrentRow = gv1.Rows(ii)
                    Throw New Exception("Fill schedule qty for Item: " + clsCommon.myCstr(gv1.Rows(ii).Cells(colItemName).Value) + " and PO No: " + clsCommon.myCstr(gv1.Rows(ii).Cells(colPONo).Value) + " at row no. " + clsCommon.myCstr(ii + 1) + "")
                End If
                If clsPurchaseOrderHead.IsOpenPO(txtPONo.Value) = 0 Then
                    If clsCommon.myLen(Icode) > 0 AndAlso orderqty <> 0 AndAlso schedule_qty > orderqty AndAlso clsCommon.myLen(txtRev_No.Text) <= 0 Then ''when sch is not amended
                        RadPageView1.SelectedPage = RadPageViewPage1
                        gv1.CurrentRow = gv1.Rows(ii)
                        Throw New Exception("Schedule qty for Item: " + clsCommon.myCstr(gv1.Rows(ii).Cells(colItemName).Value) + " and PO No: " + clsCommon.myCstr(gv1.Rows(ii).Cells(colPONo).Value) + " cannot exceed order quantity at row no. " + clsCommon.myCstr(ii + 1) + "")
                    End If
                End If
                If clsCommon.myLen(Icode) > 0 Then
                    For jj As Integer = 1 To 6
                        total_WK_qty = total_WK_qty + clsCommon.myCdbl(gv1.Rows(ii).Cells("WKQty" + clsCommon.myCstr(jj)).Value)
                    Next
                    For jj As Integer = 1 To 31
                        total_DD_qty = total_DD_qty + clsCommon.myCdbl(gv1.Rows(ii).Cells("Qty" + clsCommon.myCstr(jj)).Value)
                    Next
                    For jj As Integer = 1 To 12
                        total_MM_Qty = total_MM_Qty + clsCommon.myCdbl(gv1.Rows(ii).Cells("Month" + clsCommon.myCstr(jj)).Value)
                    Next
                End If

                If schedule_qty <> total_DD_qty + total_WK_qty + total_MM_Qty Then
                    RadPageView1.SelectedPage = RadPageViewPage1
                    gv1.CurrentRow = gv1.Rows(ii)
                    Throw New Exception("Sum of filled qty for Item: " + clsCommon.myCstr(gv1.Rows(ii).Cells(colItemName).Value) + " is not match with schedule qty.(" + clsCommon.myCstr(schedule_qty) + ") at row no. " + clsCommon.myCstr(ii + 1) + "")
                End If
            Next

            '====vendor grid=======
            For ii As Integer = 0 To gv_Vendor.Rows.Count - 1
                total_DD_qty = 0
                total_MM_Qty = 0
                total_WK_qty = 0

                Icode = clsCommon.myCstr(gv_Vendor.Rows(ii).Cells(colVItemCode).Value)
                schedule_qty = clsCommon.myCdbl(gv_Vendor.Rows(ii).Cells(colVSchQty).Value)


                If clsCommon.myLen(Icode) > 0 AndAlso schedule_qty <= 0 Then
                    RadPageView1.SelectedPage = RadPageViewPage2
                    gv_Vendor.CurrentRow = gv_Vendor.Rows(ii)
                    Throw New Exception("Fill schedule qty for Item: " + clsCommon.myCstr(gv_Vendor.Rows(ii).Cells(colVItemName).Value) + " and PO No: " + clsCommon.myCstr(gv_Vendor.Rows(ii).Cells(colVPONo).Value) + " at row no. " + clsCommon.myCstr(ii + 1) + "")
                End If

                If clsCommon.myLen(Icode) > 0 Then
                    For jj As Integer = 1 To 6
                        total_WK_qty = total_WK_qty + clsCommon.myCdbl(gv_Vendor.Rows(ii).Cells("VWKQty" + clsCommon.myCstr(jj)).Value)
                    Next
                    For jj As Integer = 1 To 31
                        total_DD_qty = total_DD_qty + clsCommon.myCdbl(gv_Vendor.Rows(ii).Cells("VQty" + clsCommon.myCstr(jj)).Value)
                    Next
                    For jj As Integer = 1 To 12
                        total_MM_Qty = total_MM_Qty + clsCommon.myCdbl(gv_Vendor.Rows(ii).Cells("VMonth" + clsCommon.myCstr(jj)).Value)
                    Next
                End If

                If schedule_qty <> total_DD_qty + total_WK_qty + total_MM_Qty Then
                    RadPageView1.SelectedPage = RadPageViewPage2
                    gv_Vendor.CurrentRow = gv_Vendor.Rows(ii)
                    Throw New Exception("Sum of filled qty for Item: " + clsCommon.myCstr(gv_Vendor.Rows(ii).Cells(colVItemName).Value) + " is not match with schedule qty.(" + clsCommon.myCstr(schedule_qty) + ") at row no. " + clsCommon.myCstr(ii + 1) + "")
                End If
            Next


            Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Function

    Private Sub SaveData(ByVal isPost As Boolean)
        Dim obj As New clsPurchaseSchedule()
        Try
            obj = New clsPurchaseSchedule()
            obj.Arr = New List(Of clsPurchaseScheduleDetail)
            obj.Arr_Vendor = New List(Of clsPurchaseScheduleVendorDetail)

            obj.Document_Code = clsCommon.myCstr(txtDocNo.Value)
            obj.Document_Date = clsCommon.myCDate(txtDate.Text)
            obj.Description = clsCommon.myCstr(txtDesc.Text).Replace("'", "`")
            obj.Vendor_Code = clsCommon.myCstr(txtVendorNo.Value)
            obj.Revision_No = clsCommon.myCstr(txtRev_No.Text)
            If clsCommon.myLen(dtpMonth.Text) = 4 Then
                obj.Schedule_Month = clsCommon.myCDate("01/01/" + clsCommon.myCstr(dtpMonth.Text))
            Else
                obj.Schedule_Month = clsCommon.myCDate(dtpMonth.Text)
            End If

            If btnDaily.IsChecked Then
                obj.Schedule_Type = "D"
            ElseIf btnWeek.IsChecked Then
                obj.Schedule_Type = "W"
            ElseIf btnMonthly.IsChecked Then
                obj.Schedule_Type = "M"
            End If
            obj.PO_Code = clsCommon.myCstr(txtPONo.Value)
            obj.PO_Type = clsCommon.myCstr(cboPOType.SelectedValue)

            For Each grow As GridViewRowInfo In gv1.Rows
                Dim objtr As New clsPurchaseScheduleDetail()

                objtr.Line_No = clsCommon.myCstr(grow.Cells(colLineno).Value)
                objtr.Item_Code = clsCommon.myCstr(grow.Cells(colItemCode).Value)
                objtr.Unit_Code = clsCommon.myCstr(grow.Cells(colItemUnit).Value)
                objtr.PO_Code = clsCommon.myCstr(grow.Cells(colPONo).Value)
                objtr.PO_Date = clsCommon.myCDate(grow.Cells(colPODate).Value)
                objtr.PO_Qty = clsCommon.myCdbl(grow.Cells(colPOQty).Value)
                objtr.Pers_Type = clsCommon.myCstr(grow.Cells(colPersType).Value)
                objtr.Pers_Value = clsCommon.myCdbl(grow.Cells(colPers).Value)
                objtr.Schedule_Qty = clsCommon.myCdbl(grow.Cells(colSchQty).Value)
                objtr.Week1_Qty = clsCommon.myCdbl(grow.Cells(colWKQty1).Value)
                objtr.Week2_Qty = clsCommon.myCdbl(grow.Cells(colWKQty2).Value)
                objtr.Week3_Qty = clsCommon.myCdbl(grow.Cells(colWKQty3).Value)
                objtr.Week4_Qty = clsCommon.myCdbl(grow.Cells(colWKQty4).Value)
                objtr.Week5_Qty = clsCommon.myCdbl(grow.Cells(colWKQty5).Value)
                objtr.Week6_Qty = clsCommon.myCdbl(grow.Cells(colWKQty6).Value)
                objtr.Day1_Qty = clsCommon.myCdbl(grow.Cells(colQty1).Value)
                objtr.Day2_Qty = clsCommon.myCdbl(grow.Cells(colQty2).Value)
                objtr.Day3_Qty = clsCommon.myCdbl(grow.Cells(colQty3).Value)
                objtr.Day4_Qty = clsCommon.myCdbl(grow.Cells(colQty4).Value)
                objtr.Day5_Qty = clsCommon.myCdbl(grow.Cells(colQty5).Value)
                objtr.Day6_Qty = clsCommon.myCdbl(grow.Cells(colQty6).Value)
                objtr.Day7_Qty = clsCommon.myCdbl(grow.Cells(colQty7).Value)
                objtr.Day8_Qty = clsCommon.myCdbl(grow.Cells(colQty8).Value)
                objtr.Day9_Qty = clsCommon.myCdbl(grow.Cells(colQty9).Value)
                objtr.Day10_Qty = clsCommon.myCdbl(grow.Cells(colQty10).Value)
                objtr.Day11_Qty = clsCommon.myCdbl(grow.Cells(colQty11).Value)
                objtr.Day12_Qty = clsCommon.myCdbl(grow.Cells(colQty12).Value)
                objtr.Day13_Qty = clsCommon.myCdbl(grow.Cells(colQty13).Value)
                objtr.Day14_Qty = clsCommon.myCdbl(grow.Cells(colQty14).Value)
                objtr.Day15_Qty = clsCommon.myCdbl(grow.Cells(colQty15).Value)
                objtr.Day16_Qty = clsCommon.myCdbl(grow.Cells(colQty16).Value)
                objtr.Day17_Qty = clsCommon.myCdbl(grow.Cells(colQty17).Value)
                objtr.Day18_Qty = clsCommon.myCdbl(grow.Cells(colQty18).Value)
                objtr.Day19_Qty = clsCommon.myCdbl(grow.Cells(colQty19).Value)
                objtr.Day20_Qty = clsCommon.myCdbl(grow.Cells(colQty20).Value)
                objtr.Day21_Qty = clsCommon.myCdbl(grow.Cells(colQty21).Value)
                objtr.Day22_Qty = clsCommon.myCdbl(grow.Cells(colQty22).Value)
                objtr.Day23_Qty = clsCommon.myCdbl(grow.Cells(colQty23).Value)
                objtr.Day24_Qty = clsCommon.myCdbl(grow.Cells(colQty24).Value)
                objtr.Day25_Qty = clsCommon.myCdbl(grow.Cells(colQty25).Value)
                objtr.Day26_Qty = clsCommon.myCdbl(grow.Cells(colQty26).Value)
                objtr.Day27_Qty = clsCommon.myCdbl(grow.Cells(colQty27).Value)
                objtr.Day28_Qty = clsCommon.myCdbl(grow.Cells(colQty28).Value)
                objtr.Day29_Qty = clsCommon.myCdbl(grow.Cells(colQty29).Value)
                objtr.Day30_Qty = clsCommon.myCdbl(grow.Cells(colQty30).Value)
                objtr.Day31_Qty = clsCommon.myCdbl(grow.Cells(colQty31).Value)
                objtr.Month1_Qty = clsCommon.myCdbl(grow.Cells(colMonth1).Value)
                objtr.Month2_Qty = clsCommon.myCdbl(grow.Cells(colMonth2).Value)
                objtr.Month3_Qty = clsCommon.myCdbl(grow.Cells(colMonth3).Value)
                objtr.Month4_Qty = clsCommon.myCdbl(grow.Cells(colMonth4).Value)
                objtr.Month5_Qty = clsCommon.myCdbl(grow.Cells(colMonth5).Value)
                objtr.Month6_Qty = clsCommon.myCdbl(grow.Cells(colMonth6).Value)
                objtr.Month7_Qty = clsCommon.myCdbl(grow.Cells(colMonth7).Value)
                objtr.Month8_Qty = clsCommon.myCdbl(grow.Cells(colMonth8).Value)
                objtr.Month9_Qty = clsCommon.myCdbl(grow.Cells(colMonth9).Value)
                objtr.Month10_Qty = clsCommon.myCdbl(grow.Cells(colMonth10).Value)
                objtr.Month11_Qty = clsCommon.myCdbl(grow.Cells(colMonth11).Value)
                objtr.Month12_Qty = clsCommon.myCdbl(grow.Cells(colMonth12).Value)
                objtr.Remarks = clsCommon.myCstr(grow.Cells(colRemarks).Value)

                If clsCommon.myLen(objtr.Item_Code) > 0 Then
                    obj.Arr.Add(objtr)
                End If
            Next

            For Each grow As GridViewRowInfo In gv_Vendor.Rows
                Dim objtr As New clsPurchaseScheduleVendorDetail()

                objtr.Line_No = clsCommon.myCstr(grow.Cells(colVLineno).Value)
                objtr.Item_Code = clsCommon.myCstr(grow.Cells(colVItemCode).Value)
                objtr.Unit_Code = clsCommon.myCstr(grow.Cells(colVItemUnit).Value)
                objtr.PO_Code = clsCommon.myCstr(grow.Cells(colVPONo).Value)
                objtr.PO_Date = clsCommon.myCDate(grow.Cells(colVPODate).Value)
                objtr.PO_Qty = clsCommon.myCdbl(grow.Cells(colVPOQty).Value)
                objtr.Schedule_Qty = clsCommon.myCdbl(grow.Cells(colVSchQty).Value)
                objtr.Week1_Qty = clsCommon.myCdbl(grow.Cells(colVWKQty1).Value)
                objtr.Week2_Qty = clsCommon.myCdbl(grow.Cells(colVWKQty2).Value)
                objtr.Week3_Qty = clsCommon.myCdbl(grow.Cells(colVWKQty3).Value)
                objtr.Week4_Qty = clsCommon.myCdbl(grow.Cells(colVWKQty4).Value)
                objtr.Week5_Qty = clsCommon.myCdbl(grow.Cells(colVWKQty5).Value)
                objtr.Week6_Qty = clsCommon.myCdbl(grow.Cells(colVWKQty6).Value)
                objtr.Day1_Qty = clsCommon.myCdbl(grow.Cells(colVQty1).Value)
                objtr.Day2_Qty = clsCommon.myCdbl(grow.Cells(colVQty2).Value)
                objtr.Day3_Qty = clsCommon.myCdbl(grow.Cells(colVQty3).Value)
                objtr.Day4_Qty = clsCommon.myCdbl(grow.Cells(colVQty4).Value)
                objtr.Day5_Qty = clsCommon.myCdbl(grow.Cells(colVQty5).Value)
                objtr.Day6_Qty = clsCommon.myCdbl(grow.Cells(colVQty6).Value)
                objtr.Day7_Qty = clsCommon.myCdbl(grow.Cells(colVQty7).Value)
                objtr.Day8_Qty = clsCommon.myCdbl(grow.Cells(colVQty8).Value)
                objtr.Day9_Qty = clsCommon.myCdbl(grow.Cells(colVQty9).Value)
                objtr.Day10_Qty = clsCommon.myCdbl(grow.Cells(colVQty10).Value)
                objtr.Day11_Qty = clsCommon.myCdbl(grow.Cells(colVQty11).Value)
                objtr.Day12_Qty = clsCommon.myCdbl(grow.Cells(colVQty12).Value)
                objtr.Day13_Qty = clsCommon.myCdbl(grow.Cells(colVQty13).Value)
                objtr.Day14_Qty = clsCommon.myCdbl(grow.Cells(colVQty14).Value)
                objtr.Day15_Qty = clsCommon.myCdbl(grow.Cells(colVQty15).Value)
                objtr.Day16_Qty = clsCommon.myCdbl(grow.Cells(colVQty16).Value)
                objtr.Day17_Qty = clsCommon.myCdbl(grow.Cells(colVQty17).Value)
                objtr.Day18_Qty = clsCommon.myCdbl(grow.Cells(colVQty18).Value)
                objtr.Day19_Qty = clsCommon.myCdbl(grow.Cells(colVQty19).Value)
                objtr.Day20_Qty = clsCommon.myCdbl(grow.Cells(colVQty20).Value)
                objtr.Day21_Qty = clsCommon.myCdbl(grow.Cells(colVQty21).Value)
                objtr.Day22_Qty = clsCommon.myCdbl(grow.Cells(colVQty22).Value)
                objtr.Day23_Qty = clsCommon.myCdbl(grow.Cells(colVQty23).Value)
                objtr.Day24_Qty = clsCommon.myCdbl(grow.Cells(colVQty24).Value)
                objtr.Day25_Qty = clsCommon.myCdbl(grow.Cells(colVQty25).Value)
                objtr.Day26_Qty = clsCommon.myCdbl(grow.Cells(colVQty26).Value)
                objtr.Day27_Qty = clsCommon.myCdbl(grow.Cells(colVQty27).Value)
                objtr.Day28_Qty = clsCommon.myCdbl(grow.Cells(colVQty28).Value)
                objtr.Day29_Qty = clsCommon.myCdbl(grow.Cells(colVQty29).Value)
                objtr.Day30_Qty = clsCommon.myCdbl(grow.Cells(colVQty30).Value)
                objtr.Day31_Qty = clsCommon.myCdbl(grow.Cells(colVQty31).Value)
                objtr.Month1_Qty = clsCommon.myCdbl(grow.Cells(colVMonth1).Value)
                objtr.Month2_Qty = clsCommon.myCdbl(grow.Cells(colVMonth2).Value)
                objtr.Month3_Qty = clsCommon.myCdbl(grow.Cells(colVMonth3).Value)
                objtr.Month4_Qty = clsCommon.myCdbl(grow.Cells(colVMonth4).Value)
                objtr.Month5_Qty = clsCommon.myCdbl(grow.Cells(colVMonth5).Value)
                objtr.Month6_Qty = clsCommon.myCdbl(grow.Cells(colVMonth6).Value)
                objtr.Month7_Qty = clsCommon.myCdbl(grow.Cells(colVMonth7).Value)
                objtr.Month8_Qty = clsCommon.myCdbl(grow.Cells(colVMonth8).Value)
                objtr.Month9_Qty = clsCommon.myCdbl(grow.Cells(colVMonth9).Value)
                objtr.Month10_Qty = clsCommon.myCdbl(grow.Cells(colVMonth10).Value)
                objtr.Month11_Qty = clsCommon.myCdbl(grow.Cells(colVMonth11).Value)
                objtr.Month12_Qty = clsCommon.myCdbl(grow.Cells(colVMonth12).Value)
                objtr.Remarks = clsCommon.myCstr(grow.Cells(colVRemarks).Value)

                If clsCommon.myLen(objtr.Item_Code) > 0 Then
                    obj.Arr_Vendor.Add(objtr)
                End If
            Next

            If clsPurchaseSchedule.SaveData(obj, isNewEntry) Then
                If Not isPost Then
                    clsCommon.MyMessageBoxShow(Me, "Data saved successfully.", Me.Text)
                End If
                txtDocNo.Value = obj.Document_Code

                UcAttachment1.SaveData(txtDocNo.Value)
                LoadData(txtDocNo.Value, NavigatorType.Current)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            obj = Nothing
        End Try
    End Sub

    Private Sub btnSamePlan_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSamePlan.Click
        gv_Vendor.Rows.Clear()

        For Each grow As GridViewRowInfo In gv1.Rows
            If clsCommon.myLen(grow.Cells(colItemCode).Value) > 0 Then
                gv_Vendor.Rows.AddNew()

                gv_Vendor.Rows(gv_Vendor.Rows.Count - 1).Cells(colVLineno).Value = clsCommon.myCstr(grow.Cells(colLineno).Value)
                gv_Vendor.Rows(gv_Vendor.Rows.Count - 1).Cells(colVItemCode).Value = clsCommon.myCstr(grow.Cells(colItemCode).Value)
                gv_Vendor.Rows(gv_Vendor.Rows.Count - 1).Cells(colVItemName).Value = clsCommon.myCstr(grow.Cells(colItemName).Value)
                gv_Vendor.Rows(gv_Vendor.Rows.Count - 1).Cells(colVItemType).Value = clsCommon.myCstr(grow.Cells(colItemType).Value)
                gv_Vendor.Rows(gv_Vendor.Rows.Count - 1).Cells(colVItemUnit).Value = clsCommon.myCstr(grow.Cells(colItemUnit).Value)
                gv_Vendor.Rows(gv_Vendor.Rows.Count - 1).Cells(colVPONo).Value = clsCommon.myCstr(grow.Cells(colPONo).Value)
                gv_Vendor.Rows(gv_Vendor.Rows.Count - 1).Cells(colVPODate).Value = clsCommon.myCstr(grow.Cells(colPODate).Value)
                gv_Vendor.Rows(gv_Vendor.Rows.Count - 1).Cells(colVRemarks).Value = clsCommon.myCstr(grow.Cells(colRemarks).Value)
                gv_Vendor.Rows(gv_Vendor.Rows.Count - 1).Cells(colVSchQty).Value = clsCommon.myCdbl(grow.Cells(colSchQty).Value)
                gv_Vendor.Rows(gv_Vendor.Rows.Count - 1).Cells(colVPOQty).Value = clsCommon.myCdbl(grow.Cells(colPOQty).Value)

                For ii As Integer = 1 To 6
                    gv_Vendor.Rows(gv_Vendor.Rows.Count - 1).Cells("VWKQty" + clsCommon.myCstr(ii)).Value = clsCommon.myCdbl(grow.Cells("WKQty" + clsCommon.myCstr(ii)).Value)
                Next
                For ii As Integer = 1 To 31
                    gv_Vendor.Rows(gv_Vendor.Rows.Count - 1).Cells("VQty" + clsCommon.myCstr(ii)).Value = clsCommon.myCdbl(grow.Cells("Qty" + clsCommon.myCstr(ii)).Value)
                Next
                For ii As Integer = 1 To 12
                    gv_Vendor.Rows(gv_Vendor.Rows.Count - 1).Cells("VMonth" + clsCommon.myCstr(ii)).Value = clsCommon.myCdbl(grow.Cells("Month" + clsCommon.myCstr(ii)).Value)
                Next
            End If
        Next
    End Sub

    Private Sub btnsave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsave.Click
        If AllowToSave() Then SaveData(False)
    End Sub

    Private Sub btnunpost_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnunpost.Click
        Try
            If clsCommon.myLen(txtDocNo.Value) <= 0 Then
                RadPageView1.SelectedPage = RadPageViewPage1
                txtDocNo.Focus()
                txtDocNo.Select()
                Errorcontrol.SetError(txtDocNo, "Select document no first.")
                Throw New Exception("Select document no first.")
            Else
                Errorcontrol.ResetError(txtDocNo)
            End If

            If clsCommon.MyMessageBoxShow("Do you want to amend this record?", "Attention", MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                If clsPurchaseSchedule.UnPostData(txtDocNo.Value) Then
                    clsCommon.MyMessageBoxShow(Me, "Document unposted successfully.", Me.Text)
                    LoadData(txtDocNo.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub gv1_CellFormatting(sender As Object, e As CellFormattingEventArgs) Handles gv1.CellFormatting
        Try
            If e.Column IsNot Nothing Then
                If clsCommon.myLen(txtRev_No.Text) <= 0 Then
                    gv1.CurrentRow.Cells(colPers).ReadOnly = True
                    gv1.CurrentRow.Cells(colPersType).ReadOnly = True
                Else
                    gv1.CurrentRow.Cells(colPers).ReadOnly = False
                    gv1.CurrentRow.Cells(colPersType).ReadOnly = False
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub gv1_CellValueChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellValueChanged
        Try
            If Not isInsideLoadData Then
                If Not isCellValueChanged Then
                    If e.Column Is gv1.Columns(colSchQty) OrElse e.Column Is gv1.Columns(colPers) OrElse e.Column Is gv1.Columns(colPersType) Then
                        isCellValueChanged = True

                        If TotalNo_Count_For_Sch_Qty > 0 Then
                            Dim mod_value As Decimal = 0
                            Dim qty As Decimal = 0

                            If btnWeek.IsChecked Then

                                If e.Column Is gv1.Columns(colSchQty) Then
                                    Dim Week_of_sch As Integer = clsPurchaseSchedule.GetNoOfWeekInMonth(dtpMonth.Text, True)
                                    TotalNo_Count_For_Sch_Qty = TotalNo_Count_For_Sch_Qty - Week_of_sch + 1
                                    mod_value = clsCommon.myCdbl(gv1.CurrentRow.Cells(colSchQty).Value) Mod TotalNo_Count_For_Sch_Qty
                                    qty = (clsCommon.myCdbl(gv1.CurrentRow.Cells(colSchQty).Value) - mod_value) / TotalNo_Count_For_Sch_Qty

                                    For ii As Integer = 1 To 6
                                        If clsCommon.myCBool(gv1.Columns("WKQty" + clsCommon.myCstr(ii)).IsVisible) = True AndAlso ii >= Week_of_sch Then
                                            gv1.CurrentRow.Cells("WKQty" + clsCommon.myCstr(ii)).Value = qty
                                        Else
                                            gv1.CurrentRow.Cells("WKQty" + clsCommon.myCstr(ii)).Value = Nothing
                                        End If
                                    Next

                                    gv1.CurrentRow.Cells("WKQty" + clsCommon.myCstr(TotalNo_Count_For_Sch_Qty + Week_of_sch - 1)).Value = qty + mod_value  'adjust mod qty in last column
                                    TotalNo_Count_For_Sch_Qty = TotalNo_Count_For_Sch_Qty + Week_of_sch - 1
                                ElseIf e.Column Is gv1.Columns(colPers) OrElse e.Column Is gv1.Columns(colPersType) Then
                                    Dim xvalue As Double = 0
                                    If clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colPersType).Value), "P") = CompairStringResult.Equal Then
                                        xvalue = Math.Round((clsCommon.myCdbl(gv1.CurrentRow.Cells(colSchQty).Value) * clsCommon.myCdbl(gv1.CurrentRow.Cells(colPers).Value)) / 100, 2)
                                    ElseIf clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colPersType).Value), "V") = CompairStringResult.Equal Then
                                        xvalue = clsCommon.myCdbl(gv1.CurrentRow.Cells(colPers).Value)
                                    End If
                                    gv1.CurrentRow.Cells("WKQty" + clsCommon.myCstr(TotalNo_Count_For_Sch_Qty)).Value = clsCommon.myCdbl(gv1.CurrentRow.Cells("WKQty" + clsCommon.myCstr(TotalNo_Count_For_Sch_Qty)).Value) + xvalue
                                    gv1.CurrentRow.Cells(colSchQty).Value = clsCommon.myCdbl(gv1.CurrentRow.Cells(colSchQty).Value) + xvalue
                                End If

                            End If
                            'XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXx
                            If btnDaily.IsChecked Then
                                If e.Column Is gv1.Columns(colSchQty) Then
                                    Dim day_of_sch As Integer = CInt(clsCommon.myCdbl(clsCommon.GetPrintDate(dtpMonth.Text, "dd")))
                                    TotalNo_Count_For_Sch_Qty = TotalNo_Count_For_Sch_Qty - day_of_sch + 1
                                    mod_value = clsCommon.myCdbl(gv1.CurrentRow.Cells(colSchQty).Value) Mod TotalNo_Count_For_Sch_Qty
                                    qty = (clsCommon.myCdbl(gv1.CurrentRow.Cells(colSchQty).Value) - mod_value) / TotalNo_Count_For_Sch_Qty

                                    For ii As Integer = 1 To 31
                                        If clsCommon.myCBool(gv1.Columns("Qty" + clsCommon.myCstr(ii)).IsVisible) = True AndAlso ii >= day_of_sch Then
                                            gv1.CurrentRow.Cells("Qty" + clsCommon.myCstr(ii)).Value = qty
                                        Else
                                            gv1.CurrentRow.Cells("Qty" + clsCommon.myCstr(ii)).Value = Nothing
                                        End If
                                    Next

                                    gv1.CurrentRow.Cells("Qty" + clsCommon.myCstr(TotalNo_Count_For_Sch_Qty + day_of_sch - 1)).Value = qty + mod_value  'adjust mod qty in last column
                                    TotalNo_Count_For_Sch_Qty = TotalNo_Count_For_Sch_Qty + day_of_sch - 1
                                ElseIf e.Column Is gv1.Columns(colPers) OrElse e.Column Is gv1.Columns(colPersType) Then
                                    Dim xvalue As Double = 0
                                    If clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colPersType).Value), "P") = CompairStringResult.Equal Then
                                        xvalue = Math.Round((clsCommon.myCdbl(gv1.CurrentRow.Cells(colSchQty).Value) * clsCommon.myCdbl(gv1.CurrentRow.Cells(colPers).Value)) / 100, 2)
                                    ElseIf clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colPersType).Value), "V") = CompairStringResult.Equal Then
                                        xvalue = clsCommon.myCdbl(gv1.CurrentRow.Cells(colPers).Value)
                                    End If
                                    gv1.CurrentRow.Cells("Qty" + clsCommon.myCstr(TotalNo_Count_For_Sch_Qty)).Value = clsCommon.myCdbl(gv1.CurrentRow.Cells("Qty" + clsCommon.myCstr(TotalNo_Count_For_Sch_Qty)).Value) + xvalue
                                    gv1.CurrentRow.Cells(colSchQty).Value = clsCommon.myCdbl(gv1.CurrentRow.Cells(colSchQty).Value) + xvalue
                                End If

                            End If
                            'XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXx
                            If btnMonthly.IsChecked Then
                                If e.Column Is gv1.Columns(colSchQty) Then
                                    TotalNo_Count_For_Sch_Qty = TotalNo_Count_For_Sch_Qty - clsCommon.myCdbl(clsCommon.GetPrintDate(dtpMonth.Text, "MM")) + 1
                                    mod_value = clsCommon.myCdbl(gv1.CurrentRow.Cells(colSchQty).Value) Mod TotalNo_Count_For_Sch_Qty
                                    qty = (clsCommon.myCdbl(gv1.CurrentRow.Cells(colSchQty).Value) - mod_value) / TotalNo_Count_For_Sch_Qty

                                    For ii As Integer = 1 To 12
                                        If clsCommon.myCBool(gv1.Columns("Month" + clsCommon.myCstr(ii)).IsVisible) = True AndAlso ii >= clsCommon.myCdbl(clsCommon.myCDate(dtpMonth.Text).ToString("MM")) Then
                                            gv1.CurrentRow.Cells("Month" + clsCommon.myCstr(ii)).Value = qty
                                        Else
                                            gv1.CurrentRow.Cells("Month" + clsCommon.myCstr(ii)).Value = Nothing
                                        End If
                                    Next

                                    gv1.CurrentRow.Cells("Month" + clsCommon.myCstr(TotalNo_Count_For_Sch_Qty - 1 + clsCommon.myCdbl(clsCommon.myCDate(dtpMonth.Text).ToString("MM")))).Value = qty + mod_value  'adjust mod qty in last column
                                    TotalNo_Count_For_Sch_Qty = TotalNo_Count_For_Sch_Qty - 1 + clsCommon.myCdbl(clsCommon.myCDate(dtpMonth.Text).ToString("MM"))
                                ElseIf e.Column Is gv1.Columns(colPers) OrElse e.Column Is gv1.Columns(colPersType) Then
                                    Dim xvalue As Double = 0
                                    If clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colPersType).Value), "P") = CompairStringResult.Equal Then
                                        xvalue = Math.Round((clsCommon.myCdbl(gv1.CurrentRow.Cells(colSchQty).Value) * clsCommon.myCdbl(gv1.CurrentRow.Cells(colPers).Value)) / 100, 2)
                                    ElseIf clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colPersType).Value), "V") = CompairStringResult.Equal Then
                                        xvalue = clsCommon.myCdbl(gv1.CurrentRow.Cells(colPers).Value)
                                    End If
                                    gv1.CurrentRow.Cells("Month" + clsCommon.myCstr(TotalNo_Count_For_Sch_Qty)).Value = clsCommon.myCdbl(gv1.CurrentRow.Cells("Month" + clsCommon.myCstr(TotalNo_Count_For_Sch_Qty)).Value) + xvalue
                                    gv1.CurrentRow.Cells(colSchQty).Value = clsCommon.myCdbl(gv1.CurrentRow.Cells(colSchQty).Value) + xvalue
                                End If

                            End If

                            isCellValueChanged = False
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            isCellValueChanged = False
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub gv_Vendor_CellValueChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv_Vendor.CellValueChanged
        Try
            If Not isInsideLoadData Then
                If Not isCellValueChanged Then
                    If e.Column Is gv_Vendor.Columns(colVSchQty) Then
                        isCellValueChanged = True

                        If TotalNo_Count_For_Sch_Qty > 0 Then
                            Dim mod_value As Decimal = 0
                            Dim qty As Decimal = 0

                            If btnWeek.IsChecked Then
                                Dim Week_of_sch As Integer = clsPurchaseSchedule.GetNoOfWeekInMonth(dtpMonth.Text, True)
                                TotalNo_Count_For_Sch_Qty = TotalNo_Count_For_Sch_Qty - Week_of_sch + 1
                                mod_value = clsCommon.myCdbl(gv_Vendor.CurrentRow.Cells(colVSchQty).Value) Mod TotalNo_Count_For_Sch_Qty
                                qty = (clsCommon.myCdbl(gv_Vendor.CurrentRow.Cells(colVSchQty).Value) - mod_value) / TotalNo_Count_For_Sch_Qty

                                For ii As Integer = 1 To 6
                                    If clsCommon.myCBool(gv_Vendor.Columns("VWKQty" + clsCommon.myCstr(ii)).IsVisible) = True AndAlso ii >= Week_of_sch Then
                                        gv_Vendor.CurrentRow.Cells("VWKQty" + clsCommon.myCstr(ii)).Value = qty
                                    End If
                                Next

                                gv_Vendor.CurrentRow.Cells("VWKQty" + clsCommon.myCstr(TotalNo_Count_For_Sch_Qty + Week_of_sch - 1)).Value = qty + mod_value  'adjust mod qty in last column
                                TotalNo_Count_For_Sch_Qty = TotalNo_Count_For_Sch_Qty + Week_of_sch - 1
                            End If
                            'XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXx
                            If btnDaily.IsChecked Then
                                Dim day_of_sch As Integer = CInt(clsCommon.myCdbl(clsCommon.GetPrintDate(dtpMonth.Text, "dd")))
                                TotalNo_Count_For_Sch_Qty = TotalNo_Count_For_Sch_Qty - day_of_sch + 1
                                mod_value = clsCommon.myCdbl(gv_Vendor.CurrentRow.Cells(colVSchQty).Value) Mod TotalNo_Count_For_Sch_Qty
                                qty = (clsCommon.myCdbl(gv_Vendor.CurrentRow.Cells(colVSchQty).Value) - mod_value) / TotalNo_Count_For_Sch_Qty

                                For ii As Integer = 1 To 31
                                    If clsCommon.myCBool(gv_Vendor.Columns("VQty" + clsCommon.myCstr(ii)).IsVisible) = True AndAlso ii >= day_of_sch Then
                                        gv_Vendor.CurrentRow.Cells("VQty" + clsCommon.myCstr(ii)).Value = qty
                                    End If
                                Next

                                gv_Vendor.CurrentRow.Cells("VQty" + clsCommon.myCstr(TotalNo_Count_For_Sch_Qty + day_of_sch - 1)).Value = qty + mod_value  'adjust mod qty in last column
                                TotalNo_Count_For_Sch_Qty = TotalNo_Count_For_Sch_Qty + day_of_sch - 1
                            End If
                            'XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXx
                            If btnMonthly.IsChecked Then
                                TotalNo_Count_For_Sch_Qty = TotalNo_Count_For_Sch_Qty - clsCommon.myCdbl(clsCommon.myCDate(dtpMonth.Text).ToString("MM")) + 1
                                mod_value = clsCommon.myCdbl(gv_Vendor.CurrentRow.Cells(colVSchQty).Value) Mod TotalNo_Count_For_Sch_Qty
                                qty = (clsCommon.myCdbl(gv_Vendor.CurrentRow.Cells(colVSchQty).Value) - mod_value) / TotalNo_Count_For_Sch_Qty

                                For ii As Integer = 1 To 12
                                    If clsCommon.myCBool(gv_Vendor.Columns("VMonth" + clsCommon.myCstr(ii)).IsVisible) = True AndAlso ii >= clsCommon.myCdbl(clsCommon.myCDate(dtpMonth.Text).ToString("MM")) Then
                                        gv_Vendor.CurrentRow.Cells("VMonth" + clsCommon.myCstr(ii)).Value = qty
                                    End If
                                Next

                                gv_Vendor.CurrentRow.Cells("VMonth" + clsCommon.myCstr(TotalNo_Count_For_Sch_Qty - 1 + clsCommon.myCdbl(clsCommon.myCDate(dtpMonth.Text).ToString("MM")))).Value = qty + mod_value  'adjust mod qty in last column
                                TotalNo_Count_For_Sch_Qty = TotalNo_Count_For_Sch_Qty - 1 + clsCommon.myCdbl(clsCommon.myCDate(dtpMonth.Text).ToString("MM"))
                            End If

                            isCellValueChanged = False
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            isCellValueChanged = False
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class
