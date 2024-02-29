Imports common
Imports System.Data.SqlClient
Imports System.IO

Public Class frmMRPForProduction
    Inherits FrmMainTranScreen

#Region "variables"
    Const colLineno As String = "lineno"
    Const colSelect As String = "Select"
    Const colPlan_Code As String = "colPlan_Code"
    Const colItem_Code As String = "colItem_Code"
    Const colItem_Desc As String = "colItem_Desc"
    Const colItem_Type As String = "colItemType"
    Const colUNIT_CODE As String = "UNIT_CODE"
    Const colPlan_Qty As String = "colPlanQty"
    Const colBom As String = "colBom"
    '=============================================
    '' MRP Detail grid 2nd tab
    Const colSNO As String = "colSNO"
    Const colMainItemCode As String = "colMainItemCode"
    Const colMainItemDesc As String = "colMainItemDesc"
    Const colItemCode As String = "colItemCode"
    Const colItemDesc As String = "colItemDesc"
    Const colUnitCode As String = "colUnitCode"
    Const colItemType As String = "colItemType"
    Const colTotalQtyRequired As String = "colTotalQtyRequired"
    'Const colStockQty As String = "colStockQty"
    'Const colPOQty As String = "colPOQty"
    'Const colSRN_Qty As String = "colSRN_Qty"
    'Const colNetRequireQty As String = "colNetRequireQty"

    '' MRP Detail grid 3nd tab
    Const colSNO3 As String = "colSNO3"
    Const colItemCode3 As String = "colItemCode3"
    Const colItemDesc3 As String = "colItemDesc3"
    Const colUnitCode3 As String = "colUnitCode3"
    Const colItemType3 As String = "colItemType3"
    Const colTotalQtyRequired3 As String = "colTotalQtyRequired3"
    Const colStockQty3 As String = "colStockQty3"
    Const colPOQty3 As String = "colPOQty3"
    Const colSRN_Qty3 As String = "colSRN_Qty3"
    Const colNetRequireQty3 As String = "colNetRequireQty3"
    Const colRequisitionId3 As String = "colRequisitionId3"
    ' Pending PO
    Const colSNO4 As String = "colSNO4"
    Const colPoNo4 As String = "colPoNO4"
    Const colPoDate4 As String = "colPoDate4"
    Const colVendorCode4 As String = "colVendorCode4"
    Const colVendorName4 As String = "colVendorName4"
    Const colLocationCode4 As String = "colLocationCode4"
    Const colLocationName4 As String = "colLocationName4"
    Const colItemCode4 As String = "colItemCode4"
    Const colItemDesc4 As String = "colItemDesc4"
    Const colUnitCode4 As String = "colUnitCode4"
    Const colPoQty4 As String = "colPoQty4"
    Const colGRNQty4 As String = "colGRNQty4"
    Const colMRNQty4 As String = "colMRNQty4"
    Const colSRNQty4 As String = "colSRNQty4"
    Const colPendingPoQty4 As String = "colPendingPoQty4"
  

    ' Pending SRN
    Const colSNO5 As String = "colSNO5"
    Const colSrnNo5 As String = "colPoNO5"
    Const colSrnDate5 As String = "colSrnDate5"
    Const colVendorCode5 As String = "colVendorCode5"
    Const colVendorName5 As String = "colVendorName5"
    Const colLocationCode5 As String = "colLocationCode5"
    Const colLocationName5 As String = "colLocationName5"
    Const colItemCode5 As String = "colItemCode5"
    Const colItemDesc5 As String = "colItemDesc5"
    Const colUnitCode5 As String = "colUnitCode5"
    Const colSRNQty5 As String = "colPoQty5"



    ' colSNO, colMainItemCode, colMainItemDesc , colItemCode , colItemDesc, colUnitCode, colItemType, colTotalQtyRequired, colStockQty, colPOQty , colSRN_Qty, colNetRequireQty
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim isNewEntry As Boolean = True
    Private isCellValueChangedOpen As Boolean = False
    Dim isInsideLoaddata As Boolean = False
    Dim Errorcontrol As New clsErrorControl()
    Dim isPost As Boolean = False
    Dim isFormLoad As Boolean = False
#End Region

    Sub LoadMRPDetailGrid()
        gvMRPDetal.Columns.Clear()
        gvMRPDetal.Rows.Clear()

        Dim stringValue As New GridViewTextBoxColumn
        Dim DoubleValue As New GridViewDecimalColumn
        Dim checkValue As New GridViewCheckBoxColumn

        stringValue = New GridViewTextBoxColumn()
        stringValue.FormatString = ""
        stringValue.HeaderText = "S.No."
        stringValue.Name = colLineno
        stringValue.Width = 70
        stringValue.ReadOnly = True
        stringValue.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvMRPDetal.Columns.Add(stringValue)


        checkValue = New GridViewCheckBoxColumn()
        checkValue.FormatString = ""
        checkValue.HeaderText = ""
        checkValue.Name = colSelect
        checkValue.Width = 100
        checkValue.ReadOnly = False
        checkValue.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvMRPDetal.Columns.Add(checkValue)


        stringValue = New GridViewTextBoxColumn()
        stringValue.FormatString = ""
        stringValue.HeaderText = "Plan Code"
        stringValue.Name = colPlan_Code
        stringValue.Width = 100
        stringValue.ReadOnly = True
        If clsCommon.myLen(TxtitemType.SelectedValue) > 0 Then
            stringValue.IsVisible = False
        Else
            stringValue.IsVisible = True
        End If
        stringValue.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvMRPDetal.Columns.Add(stringValue)

        stringValue = New GridViewTextBoxColumn()
        stringValue.FormatString = ""
        stringValue.HeaderText = "Item Code"
        stringValue.Name = colItem_Code
        stringValue.Width = 100
        If clsCommon.myLen(TxtitemType.SelectedValue) > 0 Then
            stringValue.ReadOnly = False
        Else
            stringValue.ReadOnly = True
        End If

        stringValue.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvMRPDetal.Columns.Add(stringValue)

        stringValue = New GridViewTextBoxColumn()
        stringValue.FormatString = ""
        stringValue.HeaderText = "Item Description"
        stringValue.Name = colItem_Desc
        stringValue.Width = 220
        stringValue.ReadOnly = True
        stringValue.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvMRPDetal.Columns.Add(stringValue)

        stringValue = New GridViewTextBoxColumn()
        stringValue.FormatString = ""
        stringValue.HeaderText = "Item Type"
        stringValue.Name = colItem_Type
        stringValue.Width = 100
        stringValue.ReadOnly = True
        stringValue.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvMRPDetal.Columns.Add(stringValue)

        stringValue = New GridViewTextBoxColumn()
        stringValue.FormatString = ""
        stringValue.HeaderText = "UOM"
        stringValue.Name = colUNIT_CODE
        stringValue.Width = 80
        If clsCommon.myLen(TxtitemType.SelectedValue) > 0 Then
            stringValue.ReadOnly = False
        Else
            stringValue.ReadOnly = True
        End If

        stringValue.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvMRPDetal.Columns.Add(stringValue)

        DoubleValue = New GridViewDecimalColumn()
        DoubleValue.FormatString = ""
        DoubleValue.HeaderText = "Qty"
        DoubleValue.Name = colPlan_Qty
        DoubleValue.Width = 80
        If clsCommon.myLen(TxtitemType.SelectedValue) > 0 Then
            DoubleValue.ReadOnly = False
        Else
            DoubleValue.ReadOnly = True
        End If

        DoubleValue.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvMRPDetal.Columns.Add(DoubleValue)

        stringValue = New GridViewTextBoxColumn()
        stringValue.FormatString = ""
        stringValue.HeaderText = "BOM"
        stringValue.Name = colBom
        stringValue.Width = 150
        stringValue.ReadOnly = False
        gvMRPDetal.Columns.Add(stringValue)

        gvMRPDetal.AllowDeleteRow = True
        gvMRPDetal.AllowAddNewRow = False
        gvMRPDetal.ShowGroupPanel = False
        gvMRPDetal.AllowColumnReorder = True
        gvMRPDetal.AllowRowReorder = False
        gvMRPDetal.EnableSorting = False
        gvMRPDetal.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gvMRPDetal.MasterTemplate.ShowRowHeaderColumn = False
        gvMRPDetal.Rows.AddNew()

        stringValue = Nothing
        DoubleValue = Nothing
        checkValue = Nothing
    End Sub

    Private Sub frmMRPAutoMobile_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnnew.Enabled Then
            funReset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnsave.Enabled Then
            SavingData(False)
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
                frm.strType = "PP_MRP"
                frm.strCode = "PPMrpReversAndCreate"
                frm.ShowDialog()
                If frm.isPasswordCorrect Then
                    btnReverse.Visible = True
                End If
            Else
                clsCommon.MyMessageBoxShow(Me, "You are not authorized to perform this action.", Me.Text, MessageBoxButtons.OK, Telerik.WinControls.RadMessageIcon.Error)
                'MessageBox.Show("You are not authorized to perform this action.", "Unauthorized Access", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        End If
    End Sub

    Sub funClose()
        clsERPFuncationality.closeForm(Me)
    End Sub

    Private Sub frmMRPAutoMobile_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            isFormLoad = True
        '=============================================================================
        Dim coll As Dictionary(Of String, String)
        coll = New Dictionary(Of String, String)()
        coll.Add("MRP_CODE", "VARCHAR(30) NOT NULL PRIMARY KEY ")
        coll.Add("MRP_DESCRIPTION", "VARCHAR(100) NOT NULL ")
        coll.Add("MRP_DATE", "Datetime NOT NULL")
        coll.Add("MRP_FROM", "Datetime NOT NULL")
        coll.Add("MRP_TO", "Datetime NOT NULL")
        coll.Add("MRP_REMARKS", "VARCHAR(MAX) not null ")
        coll.Add("MRP_Location", "varchar(12)  NULL References TSPL_LOCATION_MASTER(Location_Code)")
        coll.Add("Item_Type", "Varchar(100)  NULL")
        coll.Add("Posted", "numeric(2) not null default 0")
        coll.Add("Posting_Date", "datetime null")
        coll.Add("Created_By", "varchar(12) NOT NULL")
        coll.Add("Created_Date", "Datetime NOT NULL")
        coll.Add("Modified_By", "varchar(12) NOT NULL")
        coll.Add("Modified_Date", "Datetime NOT NULL")
        coll.Add("Comp_Code", "varchar(8) NULL REFERENCES TSPL_COMPANY_MASTER(COMP_CODE)")
        coll.Add("Auto_Indent", "integer not null default 0")
        coll.Add("Auto_PO", "integer not null default 0")
        coll.Add("Department_Code", "Varchar(12) Null")
        clsCommonFunctionality.CreateOrAlterTable(True, False, "TSPL_PP_MRP_HEAD", coll, Nothing, False, False)

        '==================== for Item Code =========================================================
        coll = New Dictionary(Of String, String)()
        coll.Add("MRP_CODE", "VARCHAR(30) NOT NULL  REFERENCES TSPL_PP_MRP_HEAD(MRP_CODE)")
        coll.Add("Line_No", "integer null")
        coll.Add("Plan_Code", "Varchar(30) NULL ")
        coll.Add("Item_Code", "Varchar(50) Not NULL References TSPL_ITEM_MASTER(Item_Code)")
        coll.Add("Item_Type", "Varchar(100)  NULL ")
        coll.Add("UNIT_CODE", "varchar(12)  NULL REFERENCES TSPL_UNIT_MASTER(UNIT_CODE) ")
        coll.Add("QTY", "Decimal(18,2) Default 0")
        coll.Add("BOM_CODE", "Varchar(30) NULL References TSPL_PP_BOM_HEAD(BOM_CODE)")
        clsCommonFunctionality.CreateOrAlterTable(True, False, "TSPL_PP_MRP_DETAIL", coll, Nothing, False, False)

        coll = New Dictionary(Of String, String)()
        coll.Add("TR_Code", "Varchar(30) not null PRIMARY KEY")
        coll.Add("MRP_CODE", "VARCHAR(30) NOT NULL  REFERENCES TSPL_PP_MRP_HEAD(MRP_CODE)")
        coll.Add("PLAN_CODE", "Varchar(30) Not NULL References TSPL_PP_PRODUCTION_PLAN_HEAD(PLAN_CODE)")
        clsCommonFunctionality.CreateOrAlterTable(True, False, "TSPL_MRP_PRODUCTION_PLAN", coll, Nothing, False, False)

        coll = New Dictionary(Of String, String)()
        coll.Add("MRP_CODE", "VARCHAR(30) NOT NULL  REFERENCES TSPL_PP_MRP_HEAD(MRP_CODE)")
        coll.Add("Line_No", "integer null")
        coll.Add("Main_ITEM_CODE", "Varchar(50) Not NULL References TSPL_ITEM_MASTER(Item_Code)")
        coll.Add("Item_Code", "Varchar(50) Not NULL References TSPL_ITEM_MASTER(Item_Code)")
        coll.Add("Item_Type", "Varchar(50)  NULL ")
        coll.Add("UNIT_CODE", "varchar(12)  NULL REFERENCES TSPL_UNIT_MASTER(UNIT_CODE) ")
        coll.Add("Total_Qty_Required", "Decimal(18,2) Default 0")
        'coll.Add("Stock_Qty", "Decimal(18,2) Default 0")
        'coll.Add("Pending_PO_Qty", "Decimal(18,2) Default 0")
        'coll.Add("Pending_SRN_Qty", "Decimal(18,2) Default 0")
        'coll.Add("Net_Require_Qty", "Decimal(18,2) Default 0")
        clsCommonFunctionality.CreateOrAlterTable(True, False, "TSPL_PP_MRP_BOM_DETAIL", coll, Nothing, False, False)

        coll = New Dictionary(Of String, String)()
        coll.Add("MRP_CODE", "VARCHAR(30) NOT NULL  REFERENCES TSPL_PP_MRP_HEAD(MRP_CODE)")
        coll.Add("Line_No", "integer null")
        coll.Add("Item_Code", "Varchar(50) Not NULL References TSPL_ITEM_MASTER(Item_Code)")
        coll.Add("Item_Type", "Varchar(50)  NULL ")
        coll.Add("UNIT_CODE", "varchar(12)  NULL REFERENCES TSPL_UNIT_MASTER(UNIT_CODE) ")
        coll.Add("Total_Qty_Required", "Decimal(18,2) Default 0")
        coll.Add("Stock_Qty", "Decimal(18,2) Default 0")
        coll.Add("Pending_PO_Qty", "Decimal(18,2) Default 0")
        coll.Add("Pending_SRN_Qty", "Decimal(18,2) Default 0")
        coll.Add("Net_Require_Qty", "Decimal(18,2) Default 0")
        coll.Add("Requisition_Id", "Varchar(30)  NULL ")
        clsCommonFunctionality.CreateOrAlterTable(True, False, "TSPL_PP_MRP_BOM_Item_DETAIL", coll, Nothing, False, False)

        coll = New Dictionary(Of String, String)()
        coll.Add("MRP_CODE", "VARCHAR(30) NOT NULL  REFERENCES TSPL_PP_MRP_HEAD(MRP_CODE)")
        coll.Add("Line_No", "integer null")
        coll.Add("PurchaseOrder_No", "VARCHAR(30)")
        coll.Add("PurchaseOrder_Date", "datetime")
        coll.Add("Vendor_Code", "VARCHAR(30)")
        coll.Add("Bill_To_Location", "VARCHAR(30)")
        coll.Add("Item_Code", "Varchar(50) Not NULL References TSPL_ITEM_MASTER(Item_Code)")
        coll.Add("Item_Type", "Varchar(50)  NULL ")
        coll.Add("UNIT_CODE", "varchar(12)  NULL REFERENCES TSPL_UNIT_MASTER(UNIT_CODE) ")
            coll.Add("PurchaseOrder_Qty", "Decimal(18,2) Default 0")
            coll.Add("GRN_QTY", "Decimal(18,2) Default 0")
            coll.Add("MRN_QTY", "Decimal(18,2) Default 0")
            coll.Add("SRN_Qty", "Decimal(18,2) Default 0")
            coll.Add("Pending_PO_Qty", "Decimal(18,2) Default 0")
            clsCommonFunctionality.CreateOrAlterTable(True, False, "TSPL_PP_MRP_Pending_PO", coll, Nothing, False, False)

           

        coll = New Dictionary(Of String, String)()
        coll.Add("MRP_CODE", "VARCHAR(30) NOT NULL  REFERENCES TSPL_PP_MRP_HEAD(MRP_CODE)")
        coll.Add("Line_No", "integer null")
        coll.Add("SRN_No", "VARCHAR(30)")
        coll.Add("SRN_DATE", "datetime")
        coll.Add("Vendor_Code", "VARCHAR(30)")
        coll.Add("Bill_To_Location", "VARCHAR(30)")
        coll.Add("Item_Code", "Varchar(50) Not NULL References TSPL_ITEM_MASTER(Item_Code)")
        coll.Add("Item_Type", "Varchar(50)  NULL ")
        coll.Add("UNIT_CODE", "varchar(12)  NULL REFERENCES TSPL_UNIT_MASTER(UNIT_CODE) ")
        coll.Add("SRN_Qty", "Decimal(18,2) Default 0")
        clsCommonFunctionality.CreateOrAlterTable(True, False, "TSPL_PP_MRP_Pending_SRN", coll, Nothing, False, False)

        coll = New Dictionary(Of String, String)()
        coll.Add("MRP_CODE", "VARCHAR(30) NULL ")
        clsCommonFunctionality.CreateOrAlterTable(True, False, "TSPL_REQUISITION_HEAD", coll, Nothing, False, False)
        '==============================================================================

        SetUserMgmtNew()
        ' LoadItemType()
        isNewEntry = True

        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(btnPost, "Press Alt+P for  Post")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D  for Delete ")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnnew, "Press Alt+N Adding New ")
        LoadMRPDetailGrid()
        LoadMRPBalQtyGrid()
        LoadMRPPendingPOGrid()
        LoadMRPBalItemQtyGrid()
        LoadMRPPendingSRNGrid()

        funReset()

        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
            isFormLoad = False
        Catch ex As Exception
            isFormLoad = False
        End Try
    End Sub

    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow("Permission Denied")
            Me.Close()
            Exit Sub
        End If
        btnsave.Visible = MyBase.isModifyFlag
        btnPost.Visible = MyBase.isPostFlag
        btndelete.Visible = MyBase.isDeleteFlag
        'btnReverse.Visible = MyBase.isReverse
        If MyBase.isReverse Then
            btnReverse.Enabled = True
        Else
            btnReverse.Enabled = False
        End If

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
        Dim serverdate As Date = clsCommon.GETSERVERDATE(Nothing)
        txtCode.MyReadOnly = False
        txtCode.Value = Nothing
        dtpMRPDate.Value = serverdate
        txtMRPDescription.Text = ""
        Me.txtDescription.Text = ""
        Me.txtMulProductionPlan.arrValueMember = Nothing
        fndLocation.Value = ""
        lblLocationDesc.Text = ""
        dtpFromDate.Value = serverdate
        dtpToDate.Value = serverdate
        chkStock.Checked = False
        chkPendingPO.Checked = False
        chkItemLevel.Checked = False
        chkPendignQC.Checked = False
        chkAutoIndent.IsChecked = False
        chkAutoPO.IsChecked = False

        UsLock1.Status = ERPTransactionStatus.Pending

        gvMRPDetal.Rows.Clear()
        gvMRPDetal.Rows.AddNew()

        gvBalQty.Rows.Clear()
        gvBalQty.Rows.AddNew()
        gvBalItemDetails.Rows.Clear()
        gvPendingPO.Rows.Clear()
        gvPendingSRN.Rows.Clear()

        UcAttachment1.Form_ID = MyBase.Form_ID
        UcAttachment1.BlankAllControls()

        btnsave.Text = "Save"
        btnGo.Enabled = True
        btnsave.Enabled = False
        btndelete.Enabled = False
        btnPost.Enabled = False

        fndLocation.Enabled = True
        dtpFromDate.Enabled = True
        dtpToDate.Enabled = True
        TxtitemType.Enabled = True
        txtMulProductionPlan.Enabled = True
        dtpFromDate.Enabled = True
        dtpToDate.Enabled = True
        gvMRPDetal.ReadOnly = False
        pnlDepartment.Visible = False
        txtDept.Value = ""
        lblDept.Text = ""
        btnReverse.Visible = False
        LoadItemType()
        txtMRPDescription.Focus()
        txtMRPDescription.Select()
        RadPageView1.SelectedPage = pageGeneral
    End Sub

    Private Sub txtCode__MYNavigator(ByVal sender As Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType)
        Try
            LoadData(txtCode.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Dim obj As New clsProductionMRP()
        Try
            funReset()
            isInsideLoaddata = False
            isNewEntry = True

            obj = clsProductionMRP.GetData(strCode, NavTyep)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.MRP_CODE) > 0) Then
                isNewEntry = False
                isInsideLoaddata = True
                btnsave.Text = "Update"
                If obj.POSTED Then
                    btnsave.Enabled = False
                    btnPost.Enabled = False
                    btndelete.Enabled = False
                    btnGo.Enabled = False
                    UsLock1.Status = ERPTransactionStatus.Approved
                Else
                    btnsave.Enabled = True
                    btndelete.Enabled = True
                    btnPost.Enabled = True
                    btnGo.Enabled = True
                    UsLock1.Status = ERPTransactionStatus.Pending
                End If
                '=================================================
                If clsCommon.myLen(TxtitemType.SelectedValue) > 0 Then
                    txtMulProductionPlan.Enabled = False
                    dtpFromDate.Enabled = False
                    dtpToDate.Enabled = False
                    gvMRPDetal.ReadOnly = False
                    TxtitemType.Enabled = True
                Else
                    txtMulProductionPlan.Enabled = True
                    dtpFromDate.Enabled = True
                    dtpToDate.Enabled = True
                    TxtitemType.Enabled = False
                End If
                '==================================================

                Me.txtCode.Value = obj.MRP_CODE
                Me.dtpMRPDate.Value = obj.MRP_DATE
                Me.dtpFromDate.Value = obj.MRP_FROM
                Me.dtpToDate.Value = obj.MRP_TO
                Me.txtMRPDescription.Text = obj.MRP_DESCRIPTION
                Me.txtDescription.Text = obj.MRP_REMARKS
                Me.fndLocation.Value = obj.MRP_Location
                lblLocationDesc.Text = obj.Location_Desc
                txtDept.Value = obj.Department_Code
                lblDept.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select description from TSPL_GL_SEGMENT_CODE where Seg_No='3' and Segment_code='" + obj.Department_Code + "'", Nothing))
                TxtitemType.SelectedValue = obj.MRP_ITEM_TYPE
                'Me.fndProductionPlan.Value = obj.PROD_PLAN_CODE
                'txtProductionPlanDesc.Text = obj.PROD_PLAN_DESC

                'chkStock.Checked = IIf(obj.Include_Stock = 1, True, False)
                'chkPendignQC.Checked = IIf(obj.Include_Pending_QC = 1, True, False)
                'chkPendingPO.Checked = IIf(obj.Include_Pending_PO = 1, True, False)
                'chkItemLevel.Checked = IIf(obj.Include_Item_Level = 1, True, False)
                chkAutoPO.IsChecked = IIf(obj.Auto_PO = 1, True, False)
                chkAutoIndent.IsChecked = IIf(obj.Auto_Indent = 1, True, False)
                If clsCommon.myLen(clsCommon.myCstr(obj.MRP_ITEM_TYPE)) >> 0 Then
                    TxtitemType.SelectedValue = clsCommon.myCstr(obj.MRP_ITEM_TYPE)
                End If
                'chkConsiderOpenPO.Checked = IIf(obj.Consider_Open_PO = 1, True, False)
                'chkGenAutoSchedule.Checked = IIf(obj.Auto_Schedule_Open_PO = 1, True, False)
                txtMulProductionPlan.arrValueMember = obj.ArrProductionPlanCode
                gvMRPDetal.Rows.Clear()
                gvBalQty.Rows.Clear()
                gvBalItemDetails.Rows.Clear()
                gvPendingPO.Rows.Clear()
                gvPendingSRN.Rows.Clear()

                If obj.ObjListMRPDetail IsNot Nothing And obj.ObjListMRPDetail.Count > 0 Then

                    For Each objMRPDetail As clsMRPProductionDetail In obj.ObjListMRPDetail
                        gvMRPDetal.Rows.AddNew()

                        gvMRPDetal.Rows(gvMRPDetal.Rows.Count - 1).Cells(colLineno).Value = objMRPDetail.Line_No
                        gvMRPDetal.Rows(gvMRPDetal.Rows.Count - 1).Cells(colSelect).Value = True
                        gvMRPDetal.Rows(gvMRPDetal.Rows.Count - 1).Cells(colPlan_Code).Value = objMRPDetail.PLAN_CODE
                        gvMRPDetal.Rows(gvMRPDetal.Rows.Count - 1).Cells(colItem_Code).Value = objMRPDetail.Item_Code
                        gvMRPDetal.Rows(gvMRPDetal.Rows.Count - 1).Cells(colItem_Desc).Value = objMRPDetail.Item_Desc
                        gvMRPDetal.Rows(gvMRPDetal.Rows.Count - 1).Cells(colUNIT_CODE).Value = objMRPDetail.UNIT_CODE
                        gvMRPDetal.Rows(gvMRPDetal.Rows.Count - 1).Cells(colItem_Type).Value = objMRPDetail.Item_Type
                        gvMRPDetal.Rows(gvMRPDetal.Rows.Count - 1).Cells(colPlan_Qty).Value = objMRPDetail.Qty
                        'gvMRPDetal.Rows(gvMRPDetal.Rows.Count - 1).Cells(colmaxqty).Value = objMRPDetail.Max_Qty
                        'gvMRPDetal.Rows(gvMRPDetal.Rows.Count - 1).Cells(colrol).Value = objMRPDetail.ROL_Qty
                        'gvMRPDetal.Rows(gvMRPDetal.Rows.Count - 1).Cells(colRequiredQty).Value = objMRPDetail.Total_Requird_Qty
                        'gvMRPDetal.Rows(gvMRPDetal.Rows.Count - 1).Cells(colstockqty).Value = clsProductionPlanning.GetItemBalance(fndLocation.Value, objMRPDetail.Item_Code, objMRPDetail.RM_UNIT_CODE)
                        If obj.POSTED Then
                            'gvMRPDetal.Rows(gvMRPDetal.Rows.Count - 1).Cells(colstockqty).Value = objMRPDetail.Stock_Qty
                        End If
                        'gvMRPDetal.Rows(gvMRPDetal.Rows.Count - 1).Cells(colPO_Qty).Value = objMRPDetail.PO_Qty
                        'gvMRPDetal.Rows(gvMRPDetal.Rows.Count - 1).Cells(colQC_Qty).Value = objMRPDetail.QC_Qty
                        'gvMRPDetal.Rows(gvMRPDetal.Rows.Count - 1).Cells(colExtraaddqty).Value = objMRPDetail.Extra_Qty
                        'gvMRPDetal.Rows(gvMRPDetal.Rows.Count - 1).Cells(colGrossQty).Value = objMRPDetail.Net_Requird_Qty
                        gvMRPDetal.Rows(gvMRPDetal.Rows.Count - 1).Cells(colBom).Value = objMRPDetail.BOM
                        'gvMRPDetal.Rows(gvMRPDetal.Rows.Count - 1).Cells(colNetRequiredQty).Value = objMRPDetail.Actual_Requird_Qty
                    Next
                End If

                If obj.ObjListMRPBomDetail IsNot Nothing And obj.ObjListMRPBomDetail.Count > 0 Then
                    For Each ObjMRPBomDetail As clsMRPProductionBOMDetail In obj.ObjListMRPBomDetail
                        gvBalQty.Rows.AddNew()
                        gvBalQty.Rows(gvBalQty.Rows.Count - 1).Cells(colSNO).Value = ObjMRPBomDetail.Line_No
                        gvBalQty.Rows(gvBalQty.Rows.Count - 1).Cells(colMainItemCode).Value = ObjMRPBomDetail.Main_ITEM_CODE
                        gvBalQty.Rows(gvBalQty.Rows.Count - 1).Cells(colMainItemDesc).Value = ObjMRPBomDetail.Main_Item_Desc
                        gvBalQty.Rows(gvBalQty.Rows.Count - 1).Cells(colItemCode).Value = ObjMRPBomDetail.Item_Code
                        gvBalQty.Rows(gvBalQty.Rows.Count - 1).Cells(colItemDesc).Value = ObjMRPBomDetail.Item_Desc
                        gvBalQty.Rows(gvBalQty.Rows.Count - 1).Cells(colItemType).Value = ObjMRPBomDetail.Item_Type
                        gvBalQty.Rows(gvBalQty.Rows.Count - 1).Cells(colUnitCode).Value = ObjMRPBomDetail.UNIT_CODE
                        gvBalQty.Rows(gvBalQty.Rows.Count - 1).Cells(colTotalQtyRequired).Value = ObjMRPBomDetail.Total_Qty_Required
                        'gvBalQty.Rows(gvBalQty.Rows.Count - 1).Cells(colStockQty).Value = ObjMRPBomDetail.Stock_Qty
                        'gvBalQty.Rows(gvBalQty.Rows.Count - 1).Cells(colPOQty).Value = ObjMRPBomDetail.Pending_PO_Qty
                        'gvBalQty.Rows(gvBalQty.Rows.Count - 1).Cells(colSRN_Qty).Value = ObjMRPBomDetail.Pending_SRN_Qty
                        'gvBalQty.Rows(gvBalQty.Rows.Count - 1).Cells(colNetRequireQty).Value = ObjMRPBomDetail.Net_Require_Qty
                    Next
                End If

                If obj.ObjListMRPBomItemDetail IsNot Nothing And obj.ObjListMRPBomItemDetail.Count > 0 Then
                    For Each ObjMRPBomItemDetail As clsMRPProductionBOMItemDetail In obj.ObjListMRPBomItemDetail
                        gvBalItemDetails.Rows.AddNew()
                        gvBalItemDetails.Rows(gvBalItemDetails.Rows.Count - 1).Cells(colSNO3).Value = ObjMRPBomItemDetail.Line_No
                        gvBalItemDetails.Rows(gvBalItemDetails.Rows.Count - 1).Cells(colItemCode3).Value = ObjMRPBomItemDetail.Item_Code
                        gvBalItemDetails.Rows(gvBalItemDetails.Rows.Count - 1).Cells(colItemDesc3).Value = ObjMRPBomItemDetail.Item_Desc
                        gvBalItemDetails.Rows(gvBalItemDetails.Rows.Count - 1).Cells(colItemType3).Value = ObjMRPBomItemDetail.Item_Type
                        gvBalItemDetails.Rows(gvBalItemDetails.Rows.Count - 1).Cells(colUnitCode3).Value = ObjMRPBomItemDetail.UNIT_CODE
                        gvBalItemDetails.Rows(gvBalItemDetails.Rows.Count - 1).Cells(colTotalQtyRequired3).Value = ObjMRPBomItemDetail.Total_Qty_Required
                        gvBalItemDetails.Rows(gvBalItemDetails.Rows.Count - 1).Cells(colStockQty3).Value = ObjMRPBomItemDetail.Stock_Qty
                        gvBalItemDetails.Rows(gvBalItemDetails.Rows.Count - 1).Cells(colPOQty3).Value = ObjMRPBomItemDetail.Pending_PO_Qty
                        gvBalItemDetails.Rows(gvBalItemDetails.Rows.Count - 1).Cells(colSRN_Qty3).Value = ObjMRPBomItemDetail.Pending_SRN_Qty
                        gvBalItemDetails.Rows(gvBalItemDetails.Rows.Count - 1).Cells(colNetRequireQty3).Value = ObjMRPBomItemDetail.Net_Require_Qty
                        gvBalItemDetails.Rows(gvBalItemDetails.Rows.Count - 1).Cells(colRequisitionId3).Value = ObjMRPBomItemDetail.Requisition_Id

                    Next
                End If

                If obj.ObjListMRPPendingPO IsNot Nothing And obj.ObjListMRPPendingPO.Count > 0 Then
                    For Each ObjMRPPendingPO As clsMRPProductionPendingPO In obj.ObjListMRPPendingPO
                        gvPendingPO.Rows.AddNew()
                        gvPendingPO.Rows(gvPendingPO.Rows.Count - 1).Cells(colSNO4).Value = ObjMRPPendingPO.Line_No
                        gvPendingPO.Rows(gvPendingPO.Rows.Count - 1).Cells(colPoNo4).Value = ObjMRPPendingPO.PurchaseOrder_No
                        gvPendingPO.Rows(gvPendingPO.Rows.Count - 1).Cells(colPoDate4).Value = ObjMRPPendingPO.PurchaseOrder_Date
                        gvPendingPO.Rows(gvPendingPO.Rows.Count - 1).Cells(colVendorCode4).Value = ObjMRPPendingPO.Vendor_Code
                        gvPendingPO.Rows(gvPendingPO.Rows.Count - 1).Cells(colVendorName4).Value = ObjMRPPendingPO.Vendor_Name
                        gvPendingPO.Rows(gvPendingPO.Rows.Count - 1).Cells(colLocationCode4).Value = ObjMRPPendingPO.Bill_To_Location
                        gvPendingPO.Rows(gvPendingPO.Rows.Count - 1).Cells(colLocationName4).Value = ObjMRPPendingPO.Location_Desc
                        gvPendingPO.Rows(gvPendingPO.Rows.Count - 1).Cells(colItemCode4).Value = ObjMRPPendingPO.Item_Code
                        gvPendingPO.Rows(gvPendingPO.Rows.Count - 1).Cells(colItemDesc4).Value = ObjMRPPendingPO.Item_Desc
                        gvPendingPO.Rows(gvPendingPO.Rows.Count - 1).Cells(colUnitCode4).Value = ObjMRPPendingPO.UNIT_CODE
                        gvPendingPO.Rows(gvPendingPO.Rows.Count - 1).Cells(colPoQty4).Value = ObjMRPPendingPO.PurchaseOrder_Qty
                        ' '  ' GRN_QTY, MRN_QTY,SRN_Qty,Pending_PO_Qty
                        'colGRNQty4,colMRNQty4,colSRNQty4,colPendingPoQty4
                        gvPendingPO.Rows(gvPendingPO.Rows.Count - 1).Cells(colGRNQty4).Value = ObjMRPPendingPO.GRN_QTY
                        gvPendingPO.Rows(gvPendingPO.Rows.Count - 1).Cells(colMRNQty4).Value = ObjMRPPendingPO.MRN_QTY
                        gvPendingPO.Rows(gvPendingPO.Rows.Count - 1).Cells(colSRNQty4).Value = ObjMRPPendingPO.SRN_Qty
                        gvPendingPO.Rows(gvPendingPO.Rows.Count - 1).Cells(colPendingPoQty4).Value = ObjMRPPendingPO.Pending_PO_Qty

                    Next
                End If

                If obj.ObjListMRPPendingSRN IsNot Nothing And obj.ObjListMRPPendingSRN.Count > 0 Then
                    For Each ObjMRPPendingSRN As clsMRPProductionPendingSRN In obj.ObjListMRPPendingSRN
                        gvPendingSRN.Rows.AddNew()
                        gvPendingSRN.Rows(gvPendingSRN.Rows.Count - 1).Cells(colSNO5).Value = ObjMRPPendingSRN.Line_No
                        gvPendingSRN.Rows(gvPendingSRN.Rows.Count - 1).Cells(colSrnDate5).Value = ObjMRPPendingSRN.SRN_No
                        gvPendingSRN.Rows(gvPendingSRN.Rows.Count - 1).Cells(colSrnDate5).Value = ObjMRPPendingSRN.SRN_DATE
                        gvPendingSRN.Rows(gvPendingSRN.Rows.Count - 1).Cells(colVendorCode5).Value = ObjMRPPendingSRN.Vendor_Code
                        gvPendingSRN.Rows(gvPendingSRN.Rows.Count - 1).Cells(colVendorName5).Value = ObjMRPPendingSRN.Vendor_Name
                        gvPendingSRN.Rows(gvPendingSRN.Rows.Count - 1).Cells(colLocationCode5).Value = ObjMRPPendingSRN.Bill_To_Location
                        gvPendingSRN.Rows(gvPendingSRN.Rows.Count - 1).Cells(colLocationName5).Value = ObjMRPPendingSRN.Location_Desc
                        gvPendingSRN.Rows(gvPendingSRN.Rows.Count - 1).Cells(colItemCode5).Value = ObjMRPPendingSRN.Item_Code
                        gvPendingSRN.Rows(gvPendingSRN.Rows.Count - 1).Cells(colItemDesc5).Value = ObjMRPPendingSRN.Item_Desc
                        gvPendingSRN.Rows(gvPendingSRN.Rows.Count - 1).Cells(colUnitCode5).Value = ObjMRPPendingSRN.UNIT_CODE
                        gvPendingSRN.Rows(gvPendingSRN.Rows.Count - 1).Cells(colSRNQty5).Value = ObjMRPPendingSRN.SRN_Qty
                    Next
                End If

            End If


        Catch ex As Exception
            isNewEntry = True
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            obj = Nothing
            isInsideLoaddata = False
        End Try
    End Sub

    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        btnGo.PerformClick()
        SavingData(False)
    End Sub

    Private Sub CalculateNetQty()
        'Try
        '    For Each grow As GridViewRowInfo In gvMRPDetal.Rows
        '        Dim stockqty As Decimal = Nothing
        '        Dim req_qty As Decimal = Nothing
        '        Dim min_qty As Decimal = Nothing
        '        Dim max_qty As Decimal = Nothing
        '        Dim rol_qty As Decimal = Nothing
        '        Dim pending_po As Decimal = Nothing
        '        Dim pending_qc As Decimal = Nothing
        '        Dim extra_add As Decimal = Nothing

        '        stockqty = clsCommon.myCdbl(grow.Cells(colstockqty).Value)
        '        req_qty = clsCommon.myCdbl(grow.Cells(colRequiredQty).Value)
        '        min_qty = clsCommon.myCdbl(grow.Cells(colminqty).Value)
        '        max_qty = clsCommon.myCdbl(grow.Cells(colmaxqty).Value)
        '        rol_qty = clsCommon.myCdbl(grow.Cells(colrol).Value)
        '        pending_po = clsCommon.myCdbl(grow.Cells(colPO_Qty).Value)
        '        pending_qc = clsCommon.myCdbl(grow.Cells(colQC_Qty).Value)
        '        extra_add = clsCommon.myCdbl(grow.Cells(colExtraaddqty).Value)

        '        If chkStock.Checked Then
        '            req_qty -= stockqty
        '        End If
        '        If chkPendingPO.Checked Then
        '            req_qty -= pending_po
        '        End If
        '        If chkPendignQC.Checked Then
        '            req_qty -= pending_qc
        '        End If
        '        If chkItemLevel.Checked Then
        '            'net_qty += max_qty + min_qty + rol_qty
        '        End If

        '        req_qty += extra_add

        '        If req_qty > 0 Then
        '            grow.Cells(colGrossQty).Value = req_qty
        '        End If

        '        stockqty = Nothing
        '        req_qty = Nothing
        '        min_qty = Nothing
        '        max_qty = Nothing
        '        rol_qty = Nothing
        '        pending_po = Nothing
        '        pending_qc = Nothing
        '        extra_add = Nothing
        '    Next
        'Catch ex As Exception
        '    Throw New Exception(ex.Message)
        'End Try
    End Sub

    Public Function Save() As Boolean
        Dim obj As New clsProductionMRP()
        Try
            Dim issaved As Boolean = True

            CalculateNetQty()

            If AllowToSave() Then
                Try
                    obj = New clsProductionMRP()
                    obj.ObjListMRPDetail = New List(Of clsMRPProductionDetail)
                    obj.ObjListMRPBomDetail = New List(Of clsMRPProductionBOMDetail)
                    obj.ObjListMRPBomItemDetail = New List(Of clsMRPProductionBOMItemDetail)
                    obj.ObjListMRPPendingPO = New List(Of clsMRPProductionPendingPO)
                    obj.ObjListMRPPendingSRN = New List(Of clsMRPProductionPendingSRN)

                    obj.MRP_CODE = clsCommon.myCstr(Me.txtCode.Value)
                    obj.MRP_DATE = clsCommon.myCDate(Me.dtpMRPDate.Value)
                    obj.MRP_DESCRIPTION = clsCommon.myCstr(Me.txtMRPDescription.Text)
                    obj.MRP_REMARKS = clsCommon.myCstr(Me.txtDescription.Text)
                    'obj.PROD_PLAN_CODE = clsCommon.myCstr(Me.fndProductionPlan.Value)
                    obj.MRP_Location = clsCommon.myCstr(fndLocation.Value)
                    obj.MRP_FROM = clsCommon.myCDate(Me.dtpFromDate.Value)
                    obj.MRP_TO = clsCommon.myCDate(Me.dtpToDate.Value)
                    obj.MRP_ITEM_TYPE = clsCommon.myCstr(TxtitemType.SelectedValue)

                    'obj.Include_Stock = IIf(chkStock.Checked, 1, 0)
                    'obj.Include_Pending_QC = IIf(chkPendignQC.Checked, 1, 0)
                    'obj.Include_Pending_PO = IIf(chkPendingPO.Checked, 1, 0)
                    'obj.Include_Item_Level = IIf(chkItemLevel.Checked, 1, 0)
                    obj.Auto_PO = IIf(chkAutoPO.IsChecked, 1, 0)
                    obj.Auto_Indent = IIf(chkAutoIndent.IsChecked, 1, 0)
                    If clsCommon.myLen(txtDept.Value) Then
                        obj.Department_Code = txtDept.Value
                    End If
                    'obj.Consider_Open_PO = IIf(chkConsiderOpenPO.Checked, 1, 0)
                    'obj.Auto_Schedule_Open_PO = IIf(chkGenAutoSchedule.Checked, 1, 0)

                    obj.ArrProductionPlanCode = txtMulProductionPlan.arrValueMember
                    '' saving MRP Detail
                    Dim lineNo As Integer = 1
                    Dim objtr As New clsMRPProductionDetail()
                    For Each grow As GridViewRowInfo In gvMRPDetal.Rows
                        objtr = New clsMRPProductionDetail()
                        objtr.MRP_CODE = clsCommon.myCstr(txtCode.Value)
                        objtr.Line_No = lineNo 'clsCommon.myCstr(grow.Cells(colLineno).Value)
                        objtr.PLAN_CODE = clsCommon.myCstr(grow.Cells(colPlan_Code).Value)
                        objtr.Item_Code = clsCommon.myCstr(grow.Cells(colItem_Code).Value)
                        objtr.Item_Type = clsCommon.myCstr(grow.Cells(colItem_Type).Value)
                        objtr.UNIT_CODE = clsCommon.myCstr(grow.Cells(colUNIT_CODE).Value)
                        objtr.Qty = clsCommon.myCdbl(grow.Cells(colPlan_Qty).Value)
                        objtr.BOM = clsCommon.myCstr(grow.Cells(colBom).Value)
                        If clsCommon.myLen(objtr.Item_Code) > 0 AndAlso grow.Cells(colSelect).Value = True Then
                            obj.ObjListMRPDetail.Add(objtr)
                            lineNo += 1
                        End If
                    Next

                    Dim objtrBom As New clsMRPProductionBOMDetail()
                    For Each grow As GridViewRowInfo In gvBalQty.Rows
                        objtrBom = New clsMRPProductionBOMDetail()
                        objtrBom.MRP_CODE = clsCommon.myCstr(txtCode.Value)
                        objtrBom.Line_No = clsCommon.myCstr(grow.Cells(colSNO).Value)
                        objtrBom.Main_ITEM_CODE = clsCommon.myCstr(grow.Cells(colMainItemCode).Value)
                        objtrBom.Item_Code = clsCommon.myCstr(grow.Cells(colItemCode).Value)
                        objtrBom.Item_Type = clsCommon.myCstr(grow.Cells(colItemType).Value)
                        objtrBom.UNIT_CODE = clsCommon.myCstr(grow.Cells(colUnitCode).Value)
                        objtrBom.Total_Qty_Required = clsCommon.myCdbl(grow.Cells(colTotalQtyRequired).Value)
                        'objtrBom.Stock_Qty = clsCommon.myCdbl(grow.Cells(colStockQty).Value)
                        'objtrBom.Pending_PO_Qty = clsCommon.myCdbl(grow.Cells(colPOQty).Value)
                        'objtrBom.Pending_SRN_Qty = clsCommon.myCdbl(grow.Cells(colSRN_Qty).Value)
                        'objtrBom.Net_Require_Qty = clsCommon.myCdbl(grow.Cells(colNetRequireQty).Value)
                        If clsCommon.myLen(objtrBom.Item_Code) > 0 Then
                            obj.ObjListMRPBomDetail.Add(objtrBom)
                        End If
                    Next

                    Dim objtrBomItem As New clsMRPProductionBOMItemDetail()
                    For Each grow As GridViewRowInfo In gvBalItemDetails.Rows
                        objtrBomItem = New clsMRPProductionBOMItemDetail()
                        objtrBomItem.MRP_CODE = clsCommon.myCstr(txtCode.Value)
                        objtrBomItem.Line_No = clsCommon.myCstr(grow.Cells(colSNO3).Value)
                        objtrBomItem.Item_Code = clsCommon.myCstr(grow.Cells(colItemCode3).Value)
                        objtrBomItem.Item_Type = clsCommon.myCstr(grow.Cells(colItemType3).Value)
                        objtrBomItem.UNIT_CODE = clsCommon.myCstr(grow.Cells(colUnitCode3).Value)
                        objtrBomItem.Total_Qty_Required = clsCommon.myCdbl(grow.Cells(colTotalQtyRequired3).Value)
                        objtrBomItem.Stock_Qty = clsCommon.myCdbl(grow.Cells(colStockQty3).Value)
                        objtrBomItem.Pending_PO_Qty = clsCommon.myCdbl(grow.Cells(colPOQty3).Value)
                        objtrBomItem.Pending_SRN_Qty = clsCommon.myCdbl(grow.Cells(colSRN_Qty3).Value)
                        objtrBomItem.Net_Require_Qty = clsCommon.myCdbl(grow.Cells(colNetRequireQty3).Value)
                        If clsCommon.myLen(objtrBomItem.Item_Code) > 0 Then
                            obj.ObjListMRPBomItemDetail.Add(objtrBomItem)
                        End If
                    Next

                    Dim objtrPendingPO As New clsMRPProductionPendingPO()
                    For Each grow As GridViewRowInfo In gvPendingPO.Rows
                        objtrPendingPO = New clsMRPProductionPendingPO()
                        objtrPendingPO.MRP_CODE = clsCommon.myCstr(txtCode.Value)
                        objtrPendingPO.Line_No = clsCommon.myCstr(grow.Cells(colSNO4).Value)
                        objtrPendingPO.PurchaseOrder_No = clsCommon.myCstr(grow.Cells(colPoNo4).Value)
                        objtrPendingPO.PurchaseOrder_Date = clsCommon.myCDate(grow.Cells(colPoDate4).Value)
                        objtrPendingPO.Vendor_Code = clsCommon.myCstr(grow.Cells(colVendorCode4).Value)
                        objtrPendingPO.Vendor_Name = clsCommon.myCstr(grow.Cells(colVendorName4).Value)
                        objtrPendingPO.Bill_To_Location = clsCommon.myCstr(grow.Cells(colLocationCode4).Value)
                        objtrPendingPO.Location_Desc = clsCommon.myCstr(grow.Cells(colLocationName4).Value)
                        objtrPendingPO.Item_Code = clsCommon.myCstr(grow.Cells(colItemCode4).Value)
                        objtrPendingPO.UNIT_CODE = clsCommon.myCstr(grow.Cells(colUnitCode4).Value)
                        objtrPendingPO.PurchaseOrder_Qty = clsCommon.myCdbl(grow.Cells(colPoQty4).Value)
                        objtrPendingPO.GRN_QTY = clsCommon.myCdbl(grow.Cells(colGRNQty4).Value)
                        objtrPendingPO.MRN_QTY = clsCommon.myCdbl(grow.Cells(colMRNQty4).Value)
                        objtrPendingPO.SRN_Qty = clsCommon.myCdbl(grow.Cells(colSRNQty4).Value)
                        objtrPendingPO.Pending_PO_Qty = clsCommon.myCdbl(grow.Cells(colPendingPoQty4).Value)
                        If clsCommon.myLen(objtrPendingPO.Item_Code) > 0 Then
                            obj.ObjListMRPPendingPO.Add(objtrPendingPO)
                        End If
                    Next

                    Dim objtrPendingSRN As New clsMRPProductionPendingSRN()
                    For Each grow As GridViewRowInfo In gvPendingSRN.Rows
                        objtrPendingSRN = New clsMRPProductionPendingSRN()
                        objtrPendingSRN.MRP_CODE = clsCommon.myCstr(txtCode.Value)
                        objtrPendingSRN.Line_No = clsCommon.myCstr(grow.Cells(colSNO5).Value)
                        objtrPendingSRN.SRN_No = clsCommon.myCstr(grow.Cells(colSrnNo5).Value)
                        objtrPendingSRN.SRN_DATE = clsCommon.myCDate(grow.Cells(colSrnDate5).Value)
                        objtrPendingSRN.Vendor_Code = clsCommon.myCstr(grow.Cells(colVendorCode5).Value)
                        objtrPendingSRN.Vendor_Name = clsCommon.myCstr(grow.Cells(colVendorName5).Value)
                        objtrPendingSRN.Bill_To_Location = clsCommon.myCstr(grow.Cells(colLocationCode5).Value)
                        objtrPendingSRN.Location_Desc = clsCommon.myCstr(grow.Cells(colLocationName5).Value)
                        objtrPendingSRN.Item_Code = clsCommon.myCstr(grow.Cells(colItemCode5).Value)
                        objtrPendingSRN.UNIT_CODE = clsCommon.myCstr(grow.Cells(colUnitCode5).Value)
                        objtrPendingSRN.SRN_Qty = clsCommon.myCdbl(grow.Cells(colSRNQty5).Value)
                        If clsCommon.myLen(objtrPendingSRN.Item_Code) > 0 Then
                            obj.ObjListMRPPendingSRN.Add(objtrPendingSRN)
                        End If
                    Next

                    issaved = clsProductionMRP.SaveData(obj, isNewEntry, Me.txtCode.Value)
                    If issaved Then
                        LoadData(obj.MRP_CODE, NavigatorType.Current)
                        Return issaved
                    Else
                        Return issaved
                    End If

                Catch ex As Exception
                    clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
                    Return False
                End Try
            Else
                Return False
            End If

            Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            obj = Nothing
        End Try
    End Function

    Function AllowToSave() As Boolean
        Try
            If btnsave.Text = "Update" Then
                Dim QryStr As String = "select POSTED from TSPL_PP_MRP_HEAD where MRP_Code = '" + txtCode.Value + "' "
                Dim chkpost As String = clsDBFuncationality.getSingleValue(QryStr)
                If chkpost = "1" Then
                    clsCommon.MyMessageBoxShow(Me, "Transection already posted", Me.Text)
                    Return False
                End If
            End If

            If txtMulProductionPlan.arrValueMember IsNot Nothing AndAlso txtMulProductionPlan.arrValueMember.Count > 0 Then
            Else
                If clsCommon.myLen(TxtitemType.SelectedValue) <= 0 Then
                    clsCommon.MyMessageBoxShow(Me, "Select production plan OR Item Type.", Me.Text)
                    Errorcontrol.SetError(txtMulProductionPlan, "Select production plan.")
                    Return False
                End If
                Errorcontrol.ResetError(txtMulProductionPlan)
            End If

            If chkAutoIndent.IsChecked = False AndAlso chkAutoPO.IsChecked = False AndAlso isPost = True Then
                clsCommon.MyMessageBoxShow(Me, "Please Select Auto Indent OR Auto PO", Me.Text)
                Return False
            End If

            Dim icode As String = ""

            For ii As Integer = 0 To gvMRPDetal.Rows.Count - 1
                icode = clsCommon.myCstr(gvMRPDetal.Rows(ii).Cells(colItem_Code).Value)

                If ii = 0 AndAlso clsCommon.myLen(icode) <= 0 Then
                    clsCommon.MyMessageBoxShow(Me, "Fill atleast one row for MRP.", Me.Text)
                    gvMRPDetal.CurrentRow = gvMRPDetal.Rows(ii)
                    Return False
                End If
            Next
            If chkAutoIndent.IsChecked = True Then
                If clsCommon.myLen(txtDept.Value) <= 0 Then
                    clsCommon.MyMessageBoxShow(Me, "Department Code Can not be blank,When Auto Indent Create.", Me.Text)
                    Return False
                End If
            End If

            Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
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
                If (clsProductionMRP.DeleteData(txtCode.Value)) Then
                    saveCancelLog(Reason, "Delete", Nothing)
                    common.clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ", Me.Text)
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
                ' SavingData(True)
                isPost = True
                If (Save()) Then



                    ' If chkAutoPO.IsChecked Or chkConsiderOpenPO.Checked Then ''if auto po checked then first save po
                    'Dim frm As New FrmMRPBasedPO()
                    'frm.chkAutoIndent.IsChecked = chkAutoIndent.IsChecked
                    'frm.chkAutoPO.IsChecked = chkAutoPO.IsChecked
                    'frm.chkConsiderOpenPO.Checked = chkConsiderOpenPO.Checked
                    'frm.chkGenAutoSchedule.Checked = chkGenAutoSchedule.Checked

                    'frm._MRPNO = txtCode.Value
                    'frm.WindowState = FormWindowState.Maximized
                    'frm.ShowDialog()
                    'LoadData(txtCode.Value, NavigatorType.Current)
                    'ElseIf (clsProductionMRP.PostData(txtCode.Value, chkAutoIndent.IsChecked)) Then
                    '    myMessages.post()
                    '    LoadData(txtCode.Value, NavigatorType.Current)
                    'End If
                    If (clsProductionMRP.PostData(txtCode.Value, chkAutoIndent.IsChecked, chkAutoPO.IsChecked)) Then
                        myMessages.post()
                        LoadData(txtCode.Value, NavigatorType.Current)
                    End If
                End If
                isPost = False
            End If
        Catch ex As Exception
            isPost = False
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub SavingData(ByVal ChekBtnPost As Boolean)
        If (Save()) Then
            If ChekBtnPost = False Then
                common.clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
            End If
        End If
    End Sub

    Private Sub txtCode__MYNavigator1(ByVal sender As Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtCode._MYNavigator
        Try
            LoadData(txtCode.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtCode__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtCode._MYValidating
        Try
            Dim str As String = "select count(*) from TSPL_MRP_HEAD where MRP_Code ='" + txtCode.Value + "'  "
            Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
            If no = 0 Then
                txtCode.MyReadOnly = False
            Else
                txtCode.MyReadOnly = True
            End If

            ' If txtCode.MyReadOnly OrElse isButtonClicked Then
            txtCode.Value = clsProductionMRP.GetFinder("", txtCode.Value, isButtonClicked)

            If clsCommon.myLen(txtCode.Value) > 0 Then
                LoadData(txtCode.Value, NavigatorType.Current)
            Else
                funReset()
            End If
            'End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
       
    End Sub

    Private Sub fndLocation__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndLocation._MYValidating
        Try
            Dim WhrCls As String = " Location_Type='Physical'  "
            If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                WhrCls += "  and  Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
            End If
            fndLocation.Value = clsLocation.getFinder(WhrCls, fndLocation.Value, isButtonClicked)
            lblLocationDesc.Text = clsLocation.GetName(fndLocation.Value, Nothing)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub fndProductionPlan__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean)
        'Dim whrcls As String = " TSPL_MF_PRODUCTION_PLAN_HEAD.prod_plan_code not in (select PROD_PLAN_CODE from TSPL_MRP_HEAD where Trans_Id='A_Mobile' and mrp_code<>'" + txtCode.Value + "')"
        'Me.fndProductionPlan.Value = clsProductionPlanning.GetFinder(whrcls, Me.fndProductionPlan.Value, isButtonClicked)

        'chkStock.Checked = False
        'chkPendignQC.Checked = False
        'chkPendingPO.Checked = False
        'chkItemLevel.Checked = False

        'gvMRPDetal.Rows.Clear()
        'gvMRPDetal.Rows.AddNew()
        'Me.txtProductionPlanDesc.Text = Nothing
        'Me.dtpFromDate.Value = Nothing
        'Me.dtpToDate.Value = Nothing
        'If clsCommon.myLen(Me.fndProductionPlan.Value) > 0 Then
        '    Dim objPP As New clsProductionPlanning()
        '    Try
        '        objPP = clsProductionPlanning.GetData(Me.fndProductionPlan.Value, NavigatorType.Current)
        '        If objPP IsNot Nothing Then
        '            Me.txtProductionPlanDesc.Text = objPP.DESCRIPTION
        '            fndLocation.Value = objPP.Location_Code
        '            lblLocationDesc.Text = clsLocation.GetName(objPP.Location_Code, Nothing)
        '            Me.dtpFromDate.Value = objPP.PLAN_FOR_DATE
        '            Me.dtpToDate.Value = objPP.PLAN_TO_DATE


        '            Dim qry As String = "select consm_item_code,sum(isnull(consm_quantity,0)) as consm_quantity,consm_item_unit_code from ("
        '            qry += "select consm_item_code,(case when isnull(prod_quantity,0)>0 then (isnull(CONSM_QUANTITY,0) * isnull(Net_Req_Qty,0)) / PROD_QUANTITY else isnull(CONSM_QUANTITY,0) end) as consm_quantity,consm_item_unit_code from TSPL_MF_BOM_DETAIL "
        '            qry += " left outer join TSPL_MF_BOM_HEAD on TSPL_MF_BOM_HEAD.BOM_CODE=TSPL_MF_BOM_DETAIL.BOM_CODE left outer join TSPL_MF_PROD_PLAN_DETAIL on TSPL_MF_BOM_DETAIL.BOM_CODE=TSPL_MF_PROD_PLAN_DETAIL.BOM_CODE "
        '            qry += " where 1=1 and prod_plan_code='" + fndProductionPlan.Value + "')axa group by consm_item_code,consm_item_unit_code"
        '            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        '            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
        '                For Each dr As DataRow In dt.Rows
        '                    gvMRPDetal.Rows(gvMRPDetal.Rows.Count - 1).Cells(colLineno).Value = gvMRPDetal.Rows.Count
        '                    gvMRPDetal.Rows(gvMRPDetal.Rows.Count - 1).Cells(colItem_Code).Value = clsCommon.myCstr(dr("consm_item_code"))
        '                    gvMRPDetal.Rows(gvMRPDetal.Rows.Count - 1).Cells(colItem_Desc).Value = clsItemMaster.GetItemName(clsCommon.myCstr(dr("consm_item_code")), Nothing)
        '                    gvMRPDetal.Rows(gvMRPDetal.Rows.Count - 1).Cells(colUNIT_CODE).Value = clsCommon.myCstr(dr("consm_item_unit_code"))

        '                    gvMRPDetal.Rows(gvMRPDetal.Rows.Count - 1).Cells(colminqty).Value = clsProductionMRP.GetItemLevelQty("Min", clsCommon.myCstr(dr("consm_item_code")))
        '                    gvMRPDetal.Rows(gvMRPDetal.Rows.Count - 1).Cells(colmaxqty).Value = clsProductionMRP.GetItemLevelQty("Max", clsCommon.myCstr(dr("consm_item_code")))
        '                    gvMRPDetal.Rows(gvMRPDetal.Rows.Count - 1).Cells(colrol).Value = clsProductionMRP.GetItemLevelQty("ROL", clsCommon.myCstr(dr("consm_item_code")))
        '                    gvMRPDetal.Rows(gvMRPDetal.Rows.Count - 1).Cells(colRequiredQty).Value = clsCommon.myCdbl(dr("consm_quantity"))
        '                    gvMRPDetal.Rows(gvMRPDetal.Rows.Count - 1).Cells(colstockqty).Value = clsProductionPlanning.GetItemBalance(fndLocation.Value, clsCommon.myCstr(dr("consm_item_code")), clsCommon.myCstr(dr("consm_item_unit_code")))
        '                    Dim PendingPO_Qty As Double = clsProductionMRP.GetPendingPOQty(clsCommon.myCstr(dr("consm_item_code")))
        '                    If PendingPO_Qty < 0 Then
        '                        PendingPO_Qty = 0
        '                    End If
        '                    gvMRPDetal.Rows(gvMRPDetal.Rows.Count - 1).Cells(colPO_Qty).Value = PendingPO_Qty
        '                    gvMRPDetal.Rows(gvMRPDetal.Rows.Count - 1).Cells(colQC_Qty).Value = 0
        '                    gvMRPDetal.Rows(gvMRPDetal.Rows.Count - 1).Cells(colExtraaddqty).Value = 0
        '                    gvMRPDetal.Rows(gvMRPDetal.Rows.Count - 1).Cells(colGrossQty).Value = 0
        '                    gvMRPDetal.Rows(gvMRPDetal.Rows.Count - 1).Cells(colremarks).Value = Nothing

        '                    gvMRPDetal.Rows.AddNew()

        '                    '=====get child of item is exist in bom===
        '                    Dim bomcode As String = clsMRPProductionPODetail.GetBOMOtherItems(clsCommon.myCstr(dr("consm_item_code")))
        '                    While (clsCommon.myLen(bomcode) > 0)
        '                        Dim item_code As String = ""

        '                        qry = "select consm_item_code,consm_item_unit_code,sum(isnull(consm_quantity,0)) as consm_quantity from ("
        '                        qry += "select consm_item_code,(case when isnull(TSPL_MF_BOM_HEAD.prod_quantity,0)>0 then (isnull(consm_quantity,0) * '" + clsCommon.myCstr(dr("consm_quantity")) + "')/TSPL_MF_BOM_HEAD.prod_quantity else isnull(consm_quantity,0) end) as consm_quantity,consm_item_unit_code from TSPL_MF_BOM_DETAIL left outer join TSPL_MF_BOM_HEAD on TSPL_MF_BOM_HEAD.BOM_CODE=TSPL_MF_BOM_DETAIL.BOM_CODE where 1=1 and "
        '                        qry += " TSPL_MF_BOM_DETAIL.bom_code in (" + bomcode + "))axa group by consm_item_code,consm_item_unit_code"
        '                        Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qry)
        '                        If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
        '                            For Each dr1 As DataRow In dt1.Rows
        '                                gvMRPDetal.Rows(gvMRPDetal.Rows.Count - 1).Cells(colLineno).Value = gvMRPDetal.Rows.Count
        '                                gvMRPDetal.Rows(gvMRPDetal.Rows.Count - 1).Cells(colItem_Code).Value = clsCommon.myCstr(dr1("consm_item_code"))
        '                                gvMRPDetal.Rows(gvMRPDetal.Rows.Count - 1).Cells(colItem_Desc).Value = clsItemMaster.GetItemName(clsCommon.myCstr(dr1("consm_item_code")), Nothing)
        '                                gvMRPDetal.Rows(gvMRPDetal.Rows.Count - 1).Cells(colUNIT_CODE).Value = clsCommon.myCstr(dr1("consm_item_unit_code"))

        '                                gvMRPDetal.Rows(gvMRPDetal.Rows.Count - 1).Cells(colminqty).Value = clsProductionMRP.GetItemLevelQty("Min", clsCommon.myCstr(dr1("consm_item_code")))
        '                                gvMRPDetal.Rows(gvMRPDetal.Rows.Count - 1).Cells(colmaxqty).Value = clsProductionMRP.GetItemLevelQty("Max", clsCommon.myCstr(dr1("consm_item_code")))
        '                                gvMRPDetal.Rows(gvMRPDetal.Rows.Count - 1).Cells(colrol).Value = clsProductionMRP.GetItemLevelQty("ROL", clsCommon.myCstr(dr1("consm_item_code")))
        '                                gvMRPDetal.Rows(gvMRPDetal.Rows.Count - 1).Cells(colRequiredQty).Value = clsCommon.myCdbl(dr1("consm_quantity"))
        '                                gvMRPDetal.Rows(gvMRPDetal.Rows.Count - 1).Cells(colstockqty).Value = clsProductionPlanning.GetItemBalance(fndLocation.Value, clsCommon.myCstr(dr1("consm_item_code")), clsCommon.myCstr(dr1("consm_item_unit_code")))
        '                                PendingPO_Qty = clsProductionMRP.GetPendingPOQty(clsCommon.myCstr(dr1("consm_item_code")))
        '                                If PendingPO_Qty < 0 Then
        '                                    PendingPO_Qty = 0
        '                                End If
        '                                gvMRPDetal.Rows(gvMRPDetal.Rows.Count - 1).Cells(colPO_Qty).Value = PendingPO_Qty 'clsProductionMRP.GetPendingPOQty(clsCommon.myCstr(dr1("consm_item_code")))
        '                                gvMRPDetal.Rows(gvMRPDetal.Rows.Count - 1).Cells(colQC_Qty).Value = 0
        '                                gvMRPDetal.Rows(gvMRPDetal.Rows.Count - 1).Cells(colExtraaddqty).Value = 0
        '                                gvMRPDetal.Rows(gvMRPDetal.Rows.Count - 1).Cells(colGrossQty).Value = 0
        '                                gvMRPDetal.Rows(gvMRPDetal.Rows.Count - 1).Cells(colremarks).Value = Nothing

        '                                gvMRPDetal.Rows.AddNew()

        '                                item_code = item_code + "','" + clsCommon.myCstr(dr1("consm_item_code"))
        '                            Next
        '                        End If

        '                        bomcode = clsMRPProductionPODetail.GetBOMOtherItems(item_code)
        '                    End While
        '                    '=========================end here==============================

        '                Next
        '            End If
        '        End If

        '    Catch ex As Exception
        '        clsCommon.MyMessageBoxShow(ex.Message)
        '    Finally
        '        objPP = Nothing
        '        MergeSimilarItemRows()
        '    End Try
        'End If

    End Sub

    Private Sub MergeSimilarItemRows()
        'Try
        '    Dim item_code As String = ""
        '    Dim qty As Decimal = 0
        '    Dim olditem_code As String = ""
        '    Dim lineno As Integer = 0

        '    For ii As Integer = 0 To gvMRPDetal.Rows.Count - 1
        '        item_code = clsCommon.myCstr(gvMRPDetal.Rows(ii).Cells(colItem_Code).Value)
        '        qty = clsCommon.myCdbl(gvMRPDetal.Rows(ii).Cells(colRequiredQty).Value)

        '        If clsCommon.myLen(item_code) > 0 Then
        '            For jj As Integer = ii + 1 To gvMRPDetal.Rows.Count - 1
        '                olditem_code = clsCommon.myCstr(gvMRPDetal.Rows(jj).Cells(colItem_Code).Value)

        '                If clsCommon.CompairString(item_code, olditem_code) = CompairStringResult.Equal AndAlso clsCommon.CompairString(olditem_code, "OLD") <> CompairStringResult.Equal Then
        '                    qty += clsCommon.myCdbl(gvMRPDetal.Rows(jj).Cells(colRequiredQty).Value)
        '                    gvMRPDetal.Rows(jj).Cells(colItem_Code).Value = "OLD"
        '                End If
        '            Next
        '        End If 'cond.

        '        If clsCommon.CompairString(item_code, "OLD") <> CompairStringResult.Equal Then
        '            lineno += 1
        '            gvMRPDetal.Rows(ii).Cells(colLineno).Value = lineno
        '        End If

        '        gvMRPDetal.Rows(ii).Cells(colRequiredQty).Value = qty
        '    Next

        '    '==============remove old item row
        '    For ii As Integer = gvMRPDetal.Rows.Count - 1 To 0 Step -1
        '        If clsCommon.CompairString(clsCommon.myCstr(gvMRPDetal.Rows(ii).Cells(colItem_Code).Value), "OLD") = CompairStringResult.Equal Then
        '            gvMRPDetal.Rows.RemoveAt(ii)
        '        End If
        '    Next

        'Catch ex As Exception
        '    Throw New Exception(ex.Message)
        'End Try
    End Sub

    Private Sub btnnew_Click1(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnnew.Click
        funReset()
    End Sub

    Private Sub chkStock_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkStock.ToggleStateChanged, chkPendingPO.ToggleStateChanged, chkPendignQC.ToggleStateChanged, chkItemLevel.ToggleStateChanged
        'If Not isInsideLoaddata Then
        '    Dim stockqty As Decimal = Nothing
        '    Dim req_qty As Decimal = Nothing
        '    Dim min_qty As Decimal = Nothing
        '    Dim max_qty As Decimal = Nothing
        '    Dim rol_qty As Decimal = Nothing
        '    Dim pending_po As Decimal = Nothing
        '    Dim pending_qc As Decimal = Nothing
        '    Dim extra_add As Decimal = Nothing


        '    If gvMRPDetal.Columns.Count > 0 Then
        '        For Each grow As GridViewRowInfo In gvMRPDetal.Rows
        '            stockqty = clsCommon.myCdbl(grow.Cells(colstockqty).Value)
        '            req_qty = clsCommon.myCdbl(grow.Cells(colRequiredQty).Value)
        '            min_qty = clsCommon.myCdbl(grow.Cells(colminqty).Value)
        '            max_qty = clsCommon.myCdbl(grow.Cells(colmaxqty).Value)
        '            rol_qty = clsCommon.myCdbl(grow.Cells(colrol).Value)
        '            pending_po = clsCommon.myCdbl(grow.Cells(colPO_Qty).Value)
        '            pending_qc = clsCommon.myCdbl(grow.Cells(colQC_Qty).Value)
        '            extra_add = clsCommon.myCdbl(grow.Cells(colExtraaddqty).Value)

        '            If chkStock.Checked Then
        '                req_qty -= stockqty
        '            End If
        '            If chkPendingPO.Checked Then
        '                req_qty -= pending_po
        '            End If
        '            If chkPendignQC.Checked Then
        '                req_qty -= pending_qc
        '            End If
        '            If chkItemLevel.Checked Then
        '                'net_qty += max_qty + min_qty + rol_qty
        '            End If

        '            req_qty += extra_add

        '            If req_qty > 0 Then
        '                grow.Cells(colGrossQty).Value = req_qty
        '            End If
        '        Next
        '    End If

        '    stockqty = Nothing
        '    req_qty = Nothing
        '    min_qty = Nothing
        '    max_qty = Nothing
        '    rol_qty = Nothing
        '    pending_po = Nothing
        '    pending_qc = Nothing
        '    extra_add = Nothing
        'End If
    End Sub

    Private Sub gvMRPDetal_CellValueChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvMRPDetal.CellValueChanged
        Try
            If Not isInsideLoaddata Then
                If Not isCellValueChangedOpen Then
                    If e.Column Is gvMRPDetal.Columns(colItem_Code) AndAlso clsCommon.myLen(TxtitemType.SelectedValue) Then
                        isCellValueChangedOpen = True
                        OpenICodeList(False)
                        isCellValueChangedOpen = False
                    ElseIf e.Column Is gvMRPDetal.Columns(colBom) Then
                        isCellValueChangedOpen = True
                        OpenBomList(False)
                        isCellValueChangedOpen = False
                    ElseIf e.Column Is gvMRPDetal.Columns(colUNIT_CODE) Then
                        isCellValueChangedOpen = True
                        OpenUomList(False)
                        isCellValueChangedOpen = False
                    ElseIf e.Column Is gvMRPDetal.Columns(colPlan_Qty) Then
                        isCellValueChangedOpen = True
                        'btnsave.Enabled = False
                        'btnPost.Enabled = False
                        isCellValueChangedOpen = False
                    End If
                End If
            End If
        Catch ex As Exception
            isCellValueChangedOpen = False
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
        'Try
        '    If Not isInsideLoaddata Then
        '        If Not isCellValueChangedOpen Then
        '            If e.Column Is gvMRPDetal.Columns(colExtraaddqty) OrElse e.Column Is gvMRPDetal.Columns(colRequiredQty) Then
        '                isCellValueChangedOpen = True
        '                Dim stockqty As Decimal = Nothing
        '                Dim req_qty As Decimal = Nothing
        '                Dim min_qty As Decimal = Nothing
        '                Dim max_qty As Decimal = Nothing
        '                Dim rol_qty As Decimal = Nothing
        '                Dim pending_po As Decimal = Nothing
        '                Dim pending_qc As Decimal = Nothing
        '                Dim extra_add As Decimal = Nothing

        '                stockqty = clsCommon.myCdbl(gvMRPDetal.CurrentRow.Cells(colstockqty).Value)
        '                req_qty = clsCommon.myCdbl(gvMRPDetal.CurrentRow.Cells(colRequiredQty).Value)
        '                min_qty = clsCommon.myCdbl(gvMRPDetal.CurrentRow.Cells(colminqty).Value)
        '                max_qty = clsCommon.myCdbl(gvMRPDetal.CurrentRow.Cells(colmaxqty).Value)
        '                rol_qty = clsCommon.myCdbl(gvMRPDetal.CurrentRow.Cells(colrol).Value)
        '                pending_po = clsCommon.myCdbl(gvMRPDetal.CurrentRow.Cells(colPO_Qty).Value)
        '                pending_qc = clsCommon.myCdbl(gvMRPDetal.CurrentRow.Cells(colQC_Qty).Value)
        '                extra_add = clsCommon.myCdbl(gvMRPDetal.CurrentRow.Cells(colExtraaddqty).Value)

        '                If chkStock.Checked Then
        '                    req_qty -= stockqty
        '                End If
        '                If chkPendingPO.Checked Then
        '                    req_qty -= pending_po
        '                End If
        '                If chkPendignQC.Checked Then
        '                    req_qty -= pending_qc
        '                End If
        '                If chkItemLevel.Checked Then
        '                    'net_qty += max_qty + min_qty + rol_qty
        '                End If

        '                req_qty += extra_add

        '                If req_qty > 0 Then
        '                    gvMRPDetal.CurrentRow.Cells(colGrossQty).Value = req_qty
        '                End If

        '                stockqty = Nothing
        '                req_qty = Nothing
        '                min_qty = Nothing
        '                max_qty = Nothing
        '                rol_qty = Nothing
        '                pending_po = Nothing
        '                pending_qc = Nothing
        '                extra_add = Nothing
        '                isCellValueChangedOpen = False
        '            End If

        '        End If
        '    End If
        'Catch ex As Exception
        '    isCellValueChangedOpen = False
        '    clsCommon.MyMessageBoxShow(ex.Message)
        'End Try
    End Sub


    Private Sub chkConsiderOpenPO_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkConsiderOpenPO.ToggleStateChanged
        If chkConsiderOpenPO.Checked = True Then
            chkGenAutoSchedule.Checked = True
        Else
            chkGenAutoSchedule.Checked = False
        End If
    End Sub
    Sub LoadMRPBalQtyGrid()

        gvBalQty.Rows.Clear()
        gvBalQty.Columns.Clear()

        Dim LineNo As New GridViewTextBoxColumn
        Dim Main_Item_Code As New GridViewTextBoxColumn
        Dim Main_Item_Desc As New GridViewTextBoxColumn
        Dim Item_Code As New GridViewTextBoxColumn
        Dim Item_Desc As New GridViewTextBoxColumn
        Dim RM_UNIT_CODE As New GridViewTextBoxColumn
        Dim Item_Type As New GridViewTextBoxColumn
        ' colItemType,colTotalQtyRequired, colStockQty, colPOQty, colSRN_Qty,colNetRequireQty
        Dim TotalQtyRequired As New GridViewDecimalColumn
        Dim StockQty As New GridViewDecimalColumn
        Dim POQty As New GridViewDecimalColumn
        Dim SRN_Qty As New GridViewDecimalColumn
        Dim NetRequireQty As New GridViewDecimalColumn

        LineNo = New GridViewTextBoxColumn()
        LineNo.FormatString = ""
        LineNo.HeaderText = "S.No."
        LineNo.Name = colSNO
        LineNo.Width = 70
        LineNo.ReadOnly = True
        LineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvBalQty.Columns.Add(LineNo)
        

        Main_Item_Code.FormatString = ""
        Main_Item_Code.HeaderText = "Main Item Code"
        Main_Item_Code.Name = colMainItemCode
        Main_Item_Code.Width = 100
        Main_Item_Code.ReadOnly = True
        Main_Item_Code.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvBalQty.Columns.Add(Main_Item_Code)

        Main_Item_Desc.FormatString = ""
        Main_Item_Desc.HeaderText = "Main Item Desc"
        Main_Item_Desc.Name = colMainItemDesc
        Main_Item_Desc.Width = 100
        Main_Item_Desc.ReadOnly = True
        Main_Item_Desc.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvBalQty.Columns.Add(Main_Item_Desc)

        Item_Code.FormatString = ""
        Item_Code.HeaderText = "Item Code"
        Item_Code.Name = colItemCode
        Item_Code.Width = 100
        Item_Code.ReadOnly = True
        Item_Code.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvBalQty.Columns.Add(Item_Code)

        Item_Desc.FormatString = ""
        Item_Desc.HeaderText = "Item Description"
        Item_Desc.Name = colItemDesc
        Item_Desc.Width = 100
        Item_Desc.ReadOnly = True
        Item_Desc.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvBalQty.Columns.Add(Item_Desc)

        RM_UNIT_CODE.FormatString = ""
        RM_UNIT_CODE.HeaderText = "UOM"
        RM_UNIT_CODE.Name = colUnitCode
        RM_UNIT_CODE.Width = 100
        RM_UNIT_CODE.ReadOnly = True
        RM_UNIT_CODE.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvBalQty.Columns.Add(RM_UNIT_CODE)

        Item_Type.FormatString = ""
        Item_Type.HeaderText = "Item Type"
        Item_Type.Name = colItemType
        Item_Type.Width = 100
        Item_Type.ReadOnly = True
        Item_Type.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvBalQty.Columns.Add(Item_Type)



        TotalQtyRequired.FormatString = ""
        TotalQtyRequired.HeaderText = "Total Qty Required"
        TotalQtyRequired.Name = colTotalQtyRequired
        TotalQtyRequired.Width = 100
        TotalQtyRequired.ReadOnly = True
        TotalQtyRequired.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvBalQty.Columns.Add(TotalQtyRequired)

        'StockQty.FormatString = ""
        'StockQty.HeaderText = "Stock Qty"
        'StockQty.Name = colStockQty
        'StockQty.Width = 120
        'StockQty.ReadOnly = True
        'StockQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        'gvBalQty.Columns.Add(StockQty)

        'POQty.FormatString = ""
        'POQty.HeaderText = "PO Qty"
        'POQty.Name = colPOQty
        'POQty.Width = 120
        'POQty.ReadOnly = True
        'POQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        'gvBalQty.Columns.Add(POQty)

        'SRN_Qty.FormatString = ""
        'SRN_Qty.HeaderText = "SRN Qty"
        'SRN_Qty.Name = colSRN_Qty
        'SRN_Qty.Width = 120
        'SRN_Qty.ReadOnly = True
        'SRN_Qty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        'gvBalQty.Columns.Add(SRN_Qty)

        'NetRequireQty.FormatString = ""
        'NetRequireQty.HeaderText = "Net Require Qty"
        'NetRequireQty.Name = colNetRequireQty
        'NetRequireQty.Width = 120
        'NetRequireQty.ReadOnly = True
        'NetRequireQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        'gvBalQty.Columns.Add(NetRequireQty)
    End Sub

    Sub LoadMRPBalItemQtyGrid()

        gvBalItemDetails.Rows.Clear()
        gvBalItemDetails.Columns.Clear()

        Dim LineNo As New GridViewTextBoxColumn
        Dim Item_Code As New GridViewTextBoxColumn
        Dim Item_Desc As New GridViewTextBoxColumn
        Dim RM_UNIT_CODE As New GridViewTextBoxColumn
        Dim Item_Type As New GridViewTextBoxColumn
        Dim TotalQtyRequired As New GridViewDecimalColumn
        Dim StockQty As New GridViewDecimalColumn
        Dim POQty As New GridViewDecimalColumn
        Dim SRN_Qty As New GridViewDecimalColumn
        Dim NetRequireQty As New GridViewDecimalColumn
        Dim RequisitionId As New GridViewTextBoxColumn

        LineNo = New GridViewTextBoxColumn()
        LineNo.FormatString = ""
        LineNo.HeaderText = "S.No."
        LineNo.Name = colSNO3
        LineNo.Width = 70
        LineNo.ReadOnly = True
        LineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvBalItemDetails.Columns.Add(LineNo)

        Item_Code.FormatString = ""
        Item_Code.HeaderText = "Item Code"
        Item_Code.Name = colItemCode3
        Item_Code.Width = 100
        Item_Code.ReadOnly = True
        Item_Code.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvBalItemDetails.Columns.Add(Item_Code)

        Item_Desc.FormatString = ""
        Item_Desc.HeaderText = "Item Description"
        Item_Desc.Name = colItemDesc3
        Item_Desc.Width = 100
        Item_Desc.ReadOnly = True
        Item_Desc.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvBalItemDetails.Columns.Add(Item_Desc)

        RM_UNIT_CODE.FormatString = ""
        RM_UNIT_CODE.HeaderText = "UOM"
        RM_UNIT_CODE.Name = colUnitCode3
        RM_UNIT_CODE.Width = 100
        RM_UNIT_CODE.ReadOnly = True
        RM_UNIT_CODE.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvBalItemDetails.Columns.Add(RM_UNIT_CODE)

        Item_Type.FormatString = ""
        Item_Type.HeaderText = "Item Type"
        Item_Type.Name = colItemType3
        Item_Type.Width = 100
        Item_Type.ReadOnly = True
        Item_Type.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvBalItemDetails.Columns.Add(Item_Type)



        TotalQtyRequired.FormatString = ""
        TotalQtyRequired.HeaderText = "Total Qty Required"
        TotalQtyRequired.Name = colTotalQtyRequired3
        TotalQtyRequired.Width = 100
        TotalQtyRequired.ReadOnly = True
        TotalQtyRequired.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvBalItemDetails.Columns.Add(TotalQtyRequired)

        StockQty.FormatString = ""
        StockQty.HeaderText = "Stock Qty"
        StockQty.Name = colStockQty3
        StockQty.Width = 120
        StockQty.ReadOnly = True
        StockQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvBalItemDetails.Columns.Add(StockQty)

        POQty.FormatString = ""
        POQty.HeaderText = "Pending PO Qty"
        POQty.Name = colPOQty3
        POQty.Width = 120
        POQty.ReadOnly = True
        POQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvBalItemDetails.Columns.Add(POQty)

        SRN_Qty.FormatString = ""
        SRN_Qty.HeaderText = "Pending SRN Qty"
        SRN_Qty.Name = colSRN_Qty3
        SRN_Qty.Width = 120
        SRN_Qty.ReadOnly = True
        SRN_Qty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvBalItemDetails.Columns.Add(SRN_Qty)

        NetRequireQty.FormatString = ""
        NetRequireQty.HeaderText = "Net Require Qty"
        NetRequireQty.Name = colNetRequireQty3
        NetRequireQty.Width = 120
        NetRequireQty.ReadOnly = True
        NetRequireQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvBalItemDetails.Columns.Add(NetRequireQty)

        RequisitionId.FormatString = ""
        RequisitionId.HeaderText = "Requisition Id"
        RequisitionId.Name = colRequisitionId3
        RequisitionId.Width = 100
        RequisitionId.ReadOnly = True
        RequisitionId.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvBalItemDetails.Columns.Add(RequisitionId)


    End Sub

    ' Pending PO
    Sub LoadMRPPendingPOGrid()

        gvPendingPO.Rows.Clear()
        gvPendingPO.Columns.Clear()

        Dim LineNo As New GridViewTextBoxColumn
        Dim PO_No As New GridViewTextBoxColumn
        Dim PO_Date As New GridViewTextBoxColumn
        Dim Vendor_Code As New GridViewTextBoxColumn
        Dim Vendor_Name As New GridViewTextBoxColumn
        Dim Location_Code As New GridViewTextBoxColumn
        Dim Location_Name As New GridViewTextBoxColumn
        Dim Item_Code As New GridViewTextBoxColumn
        Dim Item_Desc As New GridViewTextBoxColumn
        Dim UNIT_CODE As New GridViewTextBoxColumn
        Dim PO_QTY As New GridViewDecimalColumn
        Dim GRN_QTY As New GridViewDecimalColumn
        Dim MRN_QTY As New GridViewDecimalColumn
        Dim SRN_Qty As New GridViewDecimalColumn
        Dim Pending_PO_Qty As New GridViewDecimalColumn

        LineNo = New GridViewTextBoxColumn()
        LineNo.FormatString = ""
        LineNo.HeaderText = "S.No."
        LineNo.Name = colSNO4
        LineNo.Width = 70
        LineNo.ReadOnly = True
        LineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvPendingPO.Columns.Add(LineNo)

        PO_No.FormatString = ""
        PO_No.HeaderText = "PO No"
        PO_No.Name = colPoNo4
        PO_No.Width = 100
        PO_No.ReadOnly = True
        PO_No.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvPendingPO.Columns.Add(PO_No)

        PO_Date.FormatString = ""
        PO_Date.HeaderText = "PO Date"
        PO_Date.Name = colPoDate4
        PO_Date.Width = 100
        PO_Date.ReadOnly = True
        PO_Date.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvPendingPO.Columns.Add(PO_Date)

        Vendor_Code.FormatString = ""
        Vendor_Code.HeaderText = "Vendor Code"
        Vendor_Code.Name = colVendorCode4
        Vendor_Code.Width = 100
        Vendor_Code.ReadOnly = True
        Vendor_Code.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvPendingPO.Columns.Add(Vendor_Code)

        Vendor_Name.FormatString = ""
        Vendor_Name.HeaderText = "Vendor Name"
        Vendor_Name.Name = colVendorName4
        Vendor_Name.Width = 100
        Vendor_Name.ReadOnly = True
        Vendor_Name.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvPendingPO.Columns.Add(Vendor_Name)

        Location_Code.FormatString = ""
        Location_Code.HeaderText = "Location Code"
        Location_Code.Name = colLocationCode4
        Location_Code.Width = 100
        Location_Code.ReadOnly = True
        Location_Code.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvPendingPO.Columns.Add(Location_Code)

        Location_Name.FormatString = ""
        Location_Name.HeaderText = "Location Name"
        Location_Name.Name = colLocationName4
        Location_Name.Width = 100
        Location_Name.ReadOnly = True
        Location_Name.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvPendingPO.Columns.Add(Location_Name)



        Item_Code.FormatString = ""
        Item_Code.HeaderText = "Item Code"
        Item_Code.Name = colItemCode4
        Item_Code.Width = 100
        Item_Code.ReadOnly = True
        Item_Code.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvPendingPO.Columns.Add(Item_Code)

        Item_Desc.FormatString = ""
        Item_Desc.HeaderText = "Item Description"
        Item_Desc.Name = colItemDesc4
        Item_Desc.Width = 100
        Item_Desc.ReadOnly = True
        Item_Desc.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvPendingPO.Columns.Add(Item_Desc)

        UNIT_CODE.FormatString = ""
        UNIT_CODE.HeaderText = "UOM"
        UNIT_CODE.Name = colUnitCode4
        UNIT_CODE.Width = 100
        UNIT_CODE.ReadOnly = True
        UNIT_CODE.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvPendingPO.Columns.Add(UNIT_CODE)

        'Item_Type.FormatString = ""
        'Item_Type.HeaderText = "Item Type"
        'Item_Type.Name = colItemType3
        'Item_Type.Width = 100
        'Item_Type.ReadOnly = True
        'Item_Type.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        'gvBalItemDetails.Columns.Add(Item_Type)



        PO_QTY.FormatString = ""
        PO_QTY.HeaderText = "PO Qty"
        PO_QTY.Name = colPoQty4
        PO_QTY.Width = 100
        PO_QTY.ReadOnly = True
        PO_QTY.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvPendingPO.Columns.Add(PO_QTY)

        GRN_QTY.FormatString = ""
        GRN_QTY.HeaderText = "GRN Qty"
        GRN_QTY.Name = colGRNQty4
        GRN_QTY.Width = 100
        GRN_QTY.ReadOnly = True
        GRN_QTY.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvPendingPO.Columns.Add(GRN_QTY)

        MRN_QTY.FormatString = ""
        MRN_QTY.HeaderText = "MRN Qty"
        MRN_QTY.Name = colMRNQty4
        MRN_QTY.Width = 100
        MRN_QTY.ReadOnly = True
        MRN_QTY.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvPendingPO.Columns.Add(MRN_QTY)

        SRN_Qty.FormatString = ""
        SRN_Qty.HeaderText = "SRN Qty"
        SRN_Qty.Name = colSRNQty4
        SRN_Qty.Width = 100
        SRN_Qty.ReadOnly = True
        SRN_Qty.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvPendingPO.Columns.Add(SRN_Qty)

        Pending_PO_Qty.FormatString = ""
        Pending_PO_Qty.HeaderText = "Pending PO Qty"
        Pending_PO_Qty.Name = colPendingPoQty4
        Pending_PO_Qty.Width = 100
        Pending_PO_Qty.ReadOnly = True
        Pending_PO_Qty.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvPendingPO.Columns.Add(Pending_PO_Qty)

    End Sub

    ' SRN 

    Sub LoadMRPPendingSRNGrid()

        gvPendingSRN.Rows.Clear()
        gvPendingSRN.Columns.Clear()

        Dim LineNo As New GridViewTextBoxColumn
        Dim SRN_No As New GridViewTextBoxColumn
        Dim SRN_Date As New GridViewTextBoxColumn
        Dim Vendor_Code As New GridViewTextBoxColumn
        Dim Vendor_Name As New GridViewTextBoxColumn
        Dim Location_Code As New GridViewTextBoxColumn
        Dim Location_Name As New GridViewTextBoxColumn
        Dim Item_Code As New GridViewTextBoxColumn
        Dim Item_Desc As New GridViewTextBoxColumn
        Dim UNIT_CODE As New GridViewTextBoxColumn
        Dim SRN_QTY As New GridViewDecimalColumn

        LineNo = New GridViewTextBoxColumn()
        LineNo.FormatString = ""
        LineNo.HeaderText = "S.No."
        LineNo.Name = colSNO5
        LineNo.Width = 70
        LineNo.ReadOnly = True
        LineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvPendingSRN.Columns.Add(LineNo)

        SRN_No.FormatString = ""
        SRN_No.HeaderText = "SRN No"
        SRN_No.Name = colSrnNo5
        SRN_No.Width = 100
        SRN_No.ReadOnly = True
        SRN_No.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvPendingSRN.Columns.Add(SRN_No)

        SRN_Date.FormatString = ""
        SRN_Date.HeaderText = "SRN Date"
        SRN_Date.Name = colSrnDate5
        SRN_Date.Width = 100
        SRN_Date.ReadOnly = True
        SRN_Date.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvPendingSRN.Columns.Add(SRN_Date)

        Vendor_Code.FormatString = ""
        Vendor_Code.HeaderText = "Vendor Code"
        Vendor_Code.Name = colVendorCode5
        Vendor_Code.Width = 100
        Vendor_Code.ReadOnly = True
        Vendor_Code.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvPendingSRN.Columns.Add(Vendor_Code)

        Vendor_Name.FormatString = ""
        Vendor_Name.HeaderText = "Vendor Name"
        Vendor_Name.Name = colVendorName5
        Vendor_Name.Width = 100
        Vendor_Name.ReadOnly = True
        Vendor_Name.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvPendingSRN.Columns.Add(Vendor_Name)

        Location_Code.FormatString = ""
        Location_Code.HeaderText = "Location Code"
        Location_Code.Name = colLocationCode5
        Location_Code.Width = 100
        Location_Code.ReadOnly = True
        Location_Code.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvPendingSRN.Columns.Add(Location_Code)

        Location_Name.FormatString = ""
        Location_Name.HeaderText = "Location Name"
        Location_Name.Name = colLocationName5
        Location_Name.Width = 100
        Location_Name.ReadOnly = True
        Location_Name.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvPendingSRN.Columns.Add(Location_Name)



        Item_Code.FormatString = ""
        Item_Code.HeaderText = "Item Code"
        Item_Code.Name = colItemCode5
        Item_Code.Width = 100
        Item_Code.ReadOnly = True
        Item_Code.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvPendingSRN.Columns.Add(Item_Code)

        Item_Desc.FormatString = ""
        Item_Desc.HeaderText = "Item Description"
        Item_Desc.Name = colItemDesc5
        Item_Desc.Width = 100
        Item_Desc.ReadOnly = True
        Item_Desc.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvPendingSRN.Columns.Add(Item_Desc)

        UNIT_CODE.FormatString = ""
        UNIT_CODE.HeaderText = "UOM"
        UNIT_CODE.Name = colUnitCode5
        UNIT_CODE.Width = 100
        UNIT_CODE.ReadOnly = True
        UNIT_CODE.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvPendingSRN.Columns.Add(UNIT_CODE)

        'Item_Type.FormatString = ""
        'Item_Type.HeaderText = "Item Type"
        'Item_Type.Name = colItemType3
        'Item_Type.Width = 100
        'Item_Type.ReadOnly = True
        'Item_Type.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        'gvBalItemDetails.Columns.Add(Item_Type)



        SRN_QTY.FormatString = ""
        SRN_QTY.HeaderText = "Pending SRN Qty"
        SRN_QTY.Name = colSRNQty5
        SRN_QTY.Width = 100
        SRN_QTY.ReadOnly = True
        SRN_QTY.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvPendingSRN.Columns.Add(SRN_QTY)
    End Sub


    '=====================================================================================================
    Sub LoadItemType()
        TxtitemType.DataSource = getItemTypeQuery() 'clsItemMaster.getItemTypeQuery()
        TxtitemType.DisplayMember = "Name"
        TxtitemType.ValueMember = "Code"
    End Sub

    Public Shared Function getItemTypeQuery() As DataTable
        Dim dt As DataTable = New DataTable()
        Dim qry As String = " SELECT '' AS Code,'Select' as Name union SELECT ITEM_TYPE_CODE AS Code, ITEM_TYPE_NAME  as Name FROM TSPL_ITEM_TYPE_MASTER WHERE TSPL_ITEM_TYPE_MASTER.ITEM_TYPE_CODE  in ('F','S')   "
        dt = clsDBFuncationality.GetDataTable(qry)
        Return dt
    End Function

    Private Sub txtMulProductionPlan__My_Click(sender As Object, e As EventArgs) Handles txtMulProductionPlan._My_Click
        If clsCommon.myLen(fndLocation.Value) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Please Select Location Code First", Me.Text)
            Return
        End If
        isInsideLoaddata = True
        Dim qry As String = " select TSPL_PP_PRODUCTION_PLAN_HEAD.Plan_Code as [Plan Code] , convert (Varchar,TSPL_PP_PRODUCTION_PLAN_HEAD.Plan_Date,103) as [Plan Date] , TSPL_PP_PRODUCTION_PLAN_HEAD .Location_Code as [Location Code], TSPL_LOCATION_MASTER.Location_Desc as [Location Desc] from TSPL_PP_PRODUCTION_PLAN_HEAD left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code = TSPL_PP_PRODUCTION_PLAN_HEAD .Location_Code where  TSPL_PP_PRODUCTION_PLAN_HEAD.Is_Post = 1  "
        qry += " and convert (date,TSPL_PP_PRODUCTION_PLAN_HEAD.Plan_Date,103) > = convert (date,'" + dtpFromDate.Value + "',103)  and convert (date,TSPL_PP_PRODUCTION_PLAN_HEAD.Plan_Date,103) <= convert (date,'" + dtpToDate.Value + "',103) and TSPL_PP_PRODUCTION_PLAN_HEAD .Location_Code = '" + fndLocation.Value + "' and TSPL_PP_PRODUCTION_PLAN_HEAD.Plan_Code not in (select Plan_Code  from TSPL_PP_MRP_DETAIL) and TSPL_PP_PRODUCTION_PLAN_HEAD.Plan_Code not in  (select Plan_Code from TSPL_PP_BATCH_ORDER_HEAD ) "
        txtMulProductionPlan.arrValueMember = clsCommon.ShowMultipleSelectForm("MRP@ProductionPlan", qry, "Plan Code", "Plan Code", txtMulProductionPlan.arrValueMember, txtMulProductionPlan.arrDispalyMember)
        chkPendingPO.Checked = False
        chkItemLevel.Checked = False
        If txtMulProductionPlan.arrValueMember IsNot Nothing AndAlso txtMulProductionPlan.arrValueMember.Count > 0 Then
            qry = " select TSPL_PP_PRODUCTION_PLAN_DETAIL.Plan_Code, TSPL_PP_PRODUCTION_PLAN_DETAIL.Item_Code,TSPL_ITEM_MASTER.Item_Desc , TSPL_PP_PRODUCTION_PLAN_DETAIL.Unit_Code ,TSPL_ITEM_MASTER.Item_Type,TSPL_ITEM_TYPE_MASTER.ITEM_TYPE_NAME, TSPL_PP_PRODUCTION_PLAN_DETAIL.plan_qty from TSPL_PP_PRODUCTION_PLAN_DETAIL left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.ITEM_CODE = TSPL_PP_PRODUCTION_PLAN_DETAIL.ITEM_CODE " & _
                  " left outer join TSPL_ITEM_TYPE_MASTER  on TSPL_ITEM_TYPE_MASTER.ITEM_TYPE_CODE = TSPL_ITEM_MASTER.Item_Type " & _
                  " where TSPL_PP_PRODUCTION_PLAN_DETAIL.Plan_Code in (" + clsCommon.GetMulcallString(txtMulProductionPlan.arrValueMember) + ") "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                gvMRPDetal.DataSource = Nothing
                gvMRPDetal.Rows.Clear()
                gvMRPDetal.Rows.AddNew()
                For Each dr As DataRow In dt.Rows
                    'gv.CurrentRow.Cells(colLineNo).Value

                    gvMRPDetal.Rows(gvMRPDetal.Rows.Count - 1).Cells(colLineno).Value = gvMRPDetal.Rows.Count
                    gvMRPDetal.Rows(gvMRPDetal.Rows.Count - 1).Cells(colSelect).Value = True
                    gvMRPDetal.Rows(gvMRPDetal.Rows.Count - 1).Cells(colPlan_Code).Value = clsCommon.myCstr(dr("Plan_Code"))
                    gvMRPDetal.Rows(gvMRPDetal.Rows.Count - 1).Cells(colItem_Code).Value = clsCommon.myCstr(dr("Item_Code"))
                    gvMRPDetal.Rows(gvMRPDetal.Rows.Count - 1).Cells(colItem_Desc).Value = clsCommon.myCstr(dr("Item_Desc")) 'clsItemMaster.GetItemName(clsCommon.myCstr(dr("consm_item_code")), Nothing)
                    gvMRPDetal.Rows(gvMRPDetal.Rows.Count - 1).Cells(colUNIT_CODE).Value = clsCommon.myCstr(dr("Unit_Code"))
                    gvMRPDetal.Rows(gvMRPDetal.Rows.Count - 1).Cells(colItem_Type).Value = clsCommon.myCstr(dr("ITEM_TYPE_NAME"))
                    gvMRPDetal.Rows(gvMRPDetal.Rows.Count - 1).Cells(colPlan_Qty).Value = clsCommon.myCstr(dr("plan_qty"))
                    gvMRPDetal.Rows(gvMRPDetal.Rows.Count - 1).Cells(colBom).Value = clsMRPProductionPODetail.GetBOMOtherItems(clsCommon.myCstr(dr("Item_Code")))
                    gvMRPDetal.Rows.AddNew()
                Next
            End If
            'gvMRPDetal.ReadOnly = True

            'qry = " select YYYY.*, TSPL_PP_BOM_ITEM_DETAIL.Item_Code, TSPL_ITEM_MASTER.Item_Desc,TSPL_ITEM_MASTER.Item_Type , TSPL_ITEM_TYPE_MASTER.ITEM_TYPE_NAME, TSPL_PP_BOM_ITEM_DETAIL.Unit_Code , TSPL_PP_BOM_ITEM_DETAIL.Quantity , Convert (decimal(18,2) , (TSPL_PP_BOM_ITEM_DETAIL.Quantity / YYYY.BOM_QTY ) * YYYY.Qty) as Reqired_Qty  from (  " & _
            '      " select max (XXXX.BOM_CODE) as BOM_CODE , XXXX.Item_Code as Main_Item_Code , max (XXXX.Item_Desc) as Main_Item_Desc  , sum (XXXX.Final_Qty) as Qty , max ( XXXX.BOM_QTY ) as BOM_QTY , max ( XXXX.BOM_UNIT_CODE ) as BOM_UNIT_CODE from ( " & _
            '      " select Final.* , TSPL_PP_BOM_HEAD.PROD_ITEM_UNIT_CODE as BOM_UNIT_CODE, TSPL_PP_BOM_HEAD.PROD_QUANTITY as BOM_QTY, TSPL_ITEM_UOM_DETAIL_Source_UOM.Conversion_Factor as Source_UOM_Conv_Fact, TSPL_ITEM_UOM_DETAIL_TARGET_UOM.Conversion_Factor as Target_Uom_Conv_Fact, TBL_ITEM_STOCKING_UNIT.Conversion_Factor as Item_Stocking_Unit_Conversion_Factor ," & _
            '      " Convert (decimal (18,2),((Convert (decimal(18,2) , TSPL_ITEM_UOM_DETAIL_Source_UOM.Conversion_Factor ) / Convert (decimal(18,2), TSPL_ITEM_UOM_DETAIL_TARGET_UOM.Conversion_Factor )) * Convert (Decimal(18,2), Final.plan_qty ) ) ) as Final_Qty from ( " & _
            '      " select  TSPL_PP_PRODUCTION_PLAN_DETAIL.Item_Code,TSPL_ITEM_MASTER.Item_Desc , TSPL_PP_PRODUCTION_PLAN_DETAIL.Unit_Code ,TSPL_ITEM_MASTER.Item_Type,TSPL_ITEM_TYPE_MASTER.ITEM_TYPE_NAME, TSPL_PP_PRODUCTION_PLAN_DETAIL.plan_qty , TBL_LATEST_BOM.BOM_CODE from TSPL_PP_PRODUCTION_PLAN_DETAIL left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.ITEM_CODE = TSPL_PP_PRODUCTION_PLAN_DETAIL.ITEM_CODE  left outer join TSPL_ITEM_TYPE_MASTER  on TSPL_ITEM_TYPE_MASTER.ITEM_TYPE_CODE = TSPL_ITEM_MASTER.Item_Type " & _
            '      " left outer join (select PROD_ITEM_CODE,BOM_CODE from (   select convert (nvarchar, DENSE_RANK() OVER (PARTITION BY TSPL_PP_BOM_HEAD.PROD_ITEM_CODE ORDER BY PROD_ITEM_CODE, CONVERT (datetime , Created_Date , 103) , BOM_CODE desc)) as SNO, TSPL_PP_BOM_HEAD.PROD_ITEM_CODE, TSPL_PP_BOM_HEAD.BOM_CODE  from TSPL_PP_BOM_HEAD    )Final where Final.SNO = 1) as TBL_LATEST_BOM on TBL_LATEST_BOM.PROD_ITEM_CODE = TSPL_PP_PRODUCTION_PLAN_DETAIL.Item_Code " & _
            '      " where TSPL_PP_PRODUCTION_PLAN_DETAIL.Plan_Code in (" + clsCommon.GetMulcallString(txtMulProductionPlan.arrValueMember) + ") " & _
            '      " ) Final left outer join TSPL_PP_BOM_HEAD on TSPL_PP_BOM_HEAD.BOM_CODE = Final.BOM_CODE " & _
            '      " left outer join TSPL_ITEM_UOM_DETAIL as TSPL_ITEM_UOM_DETAIL_Source_UOM on TSPL_ITEM_UOM_DETAIL_Source_UOM.Item_Code = Final.Item_Code and  Final.Unit_Code = TSPL_ITEM_UOM_DETAIL_Source_UOM.UOM_Code " & _
            '      " left outer join TSPL_ITEM_UOM_DETAIL as TSPL_ITEM_UOM_DETAIL_TARGET_UOM on TSPL_ITEM_UOM_DETAIL_TARGET_UOM.Item_Code =   Final.Item_Code and TSPL_ITEM_UOM_DETAIL_TARGET_UOM.Uom_code = TSPL_PP_BOM_HEAD.PROD_ITEM_UNIT_CODE " & _
            '      " left outer join  (select Item_Code , UOM_Code , Conversion_Factor  FROM TSPL_ITEM_UOM_DETAIL where Stocking_Unit = 'Y' ) as TBL_ITEM_STOCKING_UNIT on TBL_ITEM_STOCKING_UNIT.Item_Code = Final.Item_Code " & _
            '      " ) XXXX  group by XXXX.Item_Code  " & _
            '      " ) YYYY  left outer join TSPL_PP_BOM_ITEM_DETAIL on TSPL_PP_BOM_ITEM_DETAIL.Bom_code = YYYY.Bom_Code  " & _
            '      " left outer join TSPL_ITEM_MASTER on    TSPL_PP_BOM_ITEM_DETAIL.Item_Code = TSPL_ITEM_MASTER.Item_Code " & _
            '      " left outer join TSPL_ITEM_TYPE_MASTER on TSPL_ITEM_TYPE_MASTER.ITEM_TYPE_CODE = TSPL_ITEM_MASTER.Item_Type "

            'Dim dtBom As DataTable = clsDBFuncationality.GetDataTable(qry)
            'If dtBom IsNot Nothing AndAlso dtBom.Rows.Count > 0 Then
            '    gvBalQty.DataSource = Nothing
            '    gvBalQty.Rows.Clear()
            '    gvBalQty.Rows.AddNew()
            '    For Each dr2 As DataRow In dtBom.Rows
            '        ' colSNO, colMainItemCode, colMainItemDesc , colItemCode , colItemDesc, colUnitCode, colItemType, colTotalQtyRequired, colStockQty, colPOQty , colSRN_Qty, colNetRequireQty
            '        gvBalQty.Rows(gvBalQty.Rows.Count - 1).Cells(colSNO).Value = gvBalQty.Rows.Count
            '        gvBalQty.Rows(gvBalQty.Rows.Count - 1).Cells(colMainItemCode).Value = clsCommon.myCstr(dr2("Main_Item_Code"))
            '        gvBalQty.Rows(gvBalQty.Rows.Count - 1).Cells(colMainItemDesc).Value = clsCommon.myCstr(dr2("Main_Item_Desc"))
            '        gvBalQty.Rows(gvBalQty.Rows.Count - 1).Cells(colItemCode).Value = clsCommon.myCstr(dr2("Item_Code"))
            '        gvBalQty.Rows(gvBalQty.Rows.Count - 1).Cells(colItemDesc).Value = clsCommon.myCstr(dr2("Item_Desc")) 'clsItemMaster.GetItemName(clsCommon.myCstr(dr("consm_item_code")), Nothing)
            '        gvBalQty.Rows(gvBalQty.Rows.Count - 1).Cells(colUnitCode).Value = clsCommon.myCstr(dr2("Unit_Code"))
            '        gvBalQty.Rows(gvBalQty.Rows.Count - 1).Cells(colItemType).Value = clsCommon.myCstr(dr2("ITEM_TYPE_NAME"))
            '        gvBalQty.Rows(gvBalQty.Rows.Count - 1).Cells(colTotalQtyRequired).Value = clsCommon.myCdbl(dr2("Reqired_Qty"))
            '        Dim RequiredQty As Double = clsCommon.myCdbl(dr2("Reqired_Qty"))
            '        Dim StockQty As Double = clsProductionMRP.GetStockQty(clsCommon.myCstr(dr2("Item_Code")), clsCommon.myCstr(dr2("Unit_Code")), fndLocation.Value)
            '        Dim PendingPOQty As Double = clsProductionMRP.GetPendingPOkQty(clsCommon.myCstr(dr2("Item_Code")), clsCommon.myCstr(dr2("Unit_Code")), fndLocation.Value)
            '        Dim PendingSRNQty As Double = clsProductionMRP.GetPendingSRNkQty(clsCommon.myCstr(dr2("Item_Code")), clsCommon.myCstr(dr2("Unit_Code")), fndLocation.Value)
            '        Dim NetRequriedQty As Double = 0
            '        If RequiredQty > (StockQty + PendingPOQty + PendingSRNQty) Then
            '            NetRequriedQty = RequiredQty - (StockQty + PendingPOQty + PendingSRNQty)
            '        End If
            '        gvBalQty.Rows(gvBalQty.Rows.Count - 1).Cells(colStockQty).Value = StockQty
            '        gvBalQty.Rows(gvBalQty.Rows.Count - 1).Cells(colPOQty).Value = PendingPOQty
            '        gvBalQty.Rows(gvBalQty.Rows.Count - 1).Cells(colSRN_Qty).Value = PendingSRNQty
            '        gvBalQty.Rows(gvBalQty.Rows.Count - 1).Cells(colNetRequireQty).Value = NetRequriedQty
            '        gvBalQty.Rows.AddNew()
            '    Next
            'End If
            'gvMRPDetal.ReadOnly = True
            'btnGo.Enabled = False
            'btnsave.Enabled = True
            TxtitemType.Enabled = False
            isInsideLoaddata = False
        End If
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Dim Qry As String = ""
        Dim strBom As String = ""
        Dim strBaseQry As String = ""
        If clsCommon.myLen(fndLocation.Value) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Please Select Location First", Me.Text)
            Return
        End If
        If gvMRPDetal.Rows.Count <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Please Select Item first", Me.Text)
            Return
        End If
        If gvMRPDetal.Rows.Count > 0 Then
            For Each grow As GridViewRowInfo In gvMRPDetal.Rows
                If clsCommon.myLen(clsCommon.myCstr(grow.Cells(colItem_Code).Value)) > 0 AndAlso grow.Cells(colSelect).Value = True Then
                    If clsCommon.myLen(strBom) > 0 Then
                        strBom = strBom + " , " + "'" + clsCommon.myCstr(grow.Cells(colBom).Value) + "'"
                        strBaseQry = strBaseQry + " Union All " + "select '" + clsCommon.myCstr(grow.Cells(colItem_Code).Value) + "' as  Item_Code ,'" + clsCommon.myCstr(grow.Cells(colItem_Desc).Value) + "' as Item_Desc,'" + clsCommon.myCstr(grow.Cells(colUNIT_CODE).Value) + "' as  Unit_Code , '' as  Item_Type, '" + clsCommon.myCstr(grow.Cells(colItem_Type).Value) + "'  Item_Type_Name , '" + clsCommon.myCstr(grow.Cells(colPlan_Qty).Value) + "' as Plan_qty, '" + clsCommon.myCstr(grow.Cells(colBom).Value) + "' BOM_CODE "
                    Else
                        strBom = "'" + clsCommon.myCstr(grow.Cells(colBom).Value) + "'"
                        strBaseQry = "select '" + clsCommon.myCstr(grow.Cells(colItem_Code).Value) + "' as  Item_Code ,'" + clsCommon.myCstr(grow.Cells(colItem_Desc).Value) + "' as Item_Desc,'" + clsCommon.myCstr(grow.Cells(colUNIT_CODE).Value) + "' as  Unit_Code , '' as  Item_Type, '" + clsCommon.myCstr(grow.Cells(colItem_Type).Value) + "'  Item_Type_Name , '" + clsCommon.myCstr(grow.Cells(colPlan_Qty).Value) + "' as Plan_qty, '" + clsCommon.myCstr(grow.Cells(colBom).Value) + "' BOM_CODE "
                    End If
                    Dim strBomExist As String = clsCommon.myCstr(grow.Cells(colBom).Value)
                    If clsCommon.myLen(strBomExist) <= 0 Then
                        clsCommon.MyMessageBoxShow("BOM is not avilable for Item Code [" + clsCommon.myCstr(grow.Cells(colItem_Code).Value) + "]. First Create BOM then select Item.", Me.Text)
                        Return
                    End If
                    Dim strItemQty As Double = clsCommon.myCdbl(grow.Cells(colPlan_Qty).Value)
                    If strItemQty <= 0 Then
                        clsCommon.MyMessageBoxShow("Qty Should be Greater then Zero for Item Code [" + clsCommon.myCstr(grow.Cells(colItem_Code).Value) + "].", Me.Text)
                        Return
                    End If
                End If
            Next
            ' ===================================== Pending PO =====================================================================
            'Qry = " select TSPL_PURCHASE_ORDER_HEAD.Vendor_Code,TSPL_VENDOR_MASTER.Vendor_Name, TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No as [PO No],convert ( varchar, TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_Date,103) as [PO Date], TSPL_PURCHASE_ORDER_Detail.Item_Code [Item Code] ,TSPL_ITEM_MASTER.Item_Desc as [Item Desc], TSPL_PURCHASE_ORDER_HEAD.Bill_To_Location as [Location Code],TSPL_LOCATION_MASTER.Location_Desc as [Location Desc], TSPL_PURCHASE_ORDER_Detail.Unit_Code as [Unit],Convert (decimal (18,2) ,TSPL_PURCHASE_ORDER_Detail.PurchaseOrder_Qty ) as [PO Qty] from TSPL_PURCHASE_ORDER_HEAD " & _
            '      " inner join TSPL_PURCHASE_ORDER_Detail on TSPL_PURCHASE_ORDER_Detail.PurchaseOrder_No =TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No  " & _
            '      " inner join TSPL_PP_BOM_ITEM_DETAIL  on TSPL_PURCHASE_ORDER_Detail.Item_Code = TSPL_PP_BOM_ITEM_DETAIL.ITEM_CODE " & _
            '      " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.ITEM_CODE = TSPL_PURCHASE_ORDER_Detail.Item_Code " & _
            '      " left Outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code = TSPL_PURCHASE_ORDER_HEAD.Bill_To_Location " & _
            '      " left Outer Join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code = TSPL_PURCHASE_ORDER_HEAD.Vendor_Code " & _
            '      " where  TSPL_PURCHASE_ORDER_HEAD.Status =0 and TSPL_PURCHASE_ORDER_HEAD.Bill_To_Location = '" + fndLocation.Value + "' and TSPL_PP_BOM_ITEM_DETAIL.BOM_CODE in (" + strBom + ") "
            'Qry = " select  Final.PurchaseOrder_No,Convert (varchar,Final.PurchaseOrder_Date,103) as PurchaseOrder_Date , Final.Vendor_Code,Final.Vendor_Name,Final.Item_Code,Final.Item_Desc,Final.Bill_To_Location, TSPL_LOCATION_MASTER.Location_Desc,Final.PurchaseOrder_Qty , ( case when Final.SRN_QTY < = Final.Final_Qty_In_Target_UOM  then   Final.Final_Qty_In_Target_UOM  -  Final.SRN_QTY else 0 end) as Pending_PO_Qty  " & _
            '        " from  ( " & _
            '        " select  " & _
            '        " TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No, TSPL_PURCHASE_ORDER_HEAD.Vendor_Code,TSPL_Vendor_Master.Vendor_Name,Convert (varchar,TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_Date,103) as PurchaseOrder_Date , TSPL_PURCHASE_ORDER_Detail.Item_Code,TSPL_ITEM_MASTER.Item_Desc  , TSPL_PURCHASE_ORDER_HEAD.Bill_To_Location, " & _
            '        " TSPL_PURCHASE_ORDER_Detail.Unit_Code,TSPL_PURCHASE_ORDER_Detail.PurchaseOrder_Qty,   Convert (decimal (18,2),isnull (TBL_SRN.SRN_QTY,0) ) as SRN_QTY , " & _
            '        " Convert (decimal (18,2),((Convert (decimal(18,2) , Source_UOM.Conversion_Factor ) / Convert (decimal(18,2), Target_Uom.Conversion_Factor )) * Convert (Decimal(18,2), TSPL_PURCHASE_ORDER_Detail.PurchaseOrder_Qty ) ) )    as Final_Qty_In_Target_UOM    " & _
            '        " from TSPL_PURCHASE_ORDER_HEAD left outer join TSPL_PURCHASE_ORDER_Detail on TSPL_PURCHASE_ORDER_Detail.PurchaseOrder_No =TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No " & _
            '        " left outer join  " & _
            '        " (  " & _
            '        " select XXXX.PO_ID,XXXX.PO_Qty,XXXX.SRN_QTY, XXXX.Item_Code,XXXX.Unit_code from ( " & _
            '        " select TSPL_SRN_DETAIL.PO_ID , Convert (decimal (18,2),((Convert (decimal(18,2) , Source_UOM.Conversion_Factor ) / Convert (decimal(18,2), Target_Uom.Conversion_Factor )) * Convert (Decimal(18,2),  TSPL_SRN_DETAIL.PO_Qty  ) ) ) as PO_Qty " & _
            '        " , Convert (decimal (18,2),((Convert (decimal(18,2) , Source_UOM.Conversion_Factor ) / Convert (decimal(18,2), Target_Uom.Conversion_Factor )) * Convert (Decimal(18,2), TSPL_SRN_DETAIL.SRN_QTY  ) ) ) as SRN_QTY , TSPL_SRN_DETAIL.Item_Code,TSPL_SRN_DETAIL. Unit_Code from TSPL_SRN_DETAIL  " & _
            '        " left outer join TSPL_SRN_HEAD on TSPL_SRN_HEAD.SRN_NO =TSPL_SRN_DETAIL.SRN_No  " & _
            '        " left outer Join tspl_item_uom_detail as Source_UOM on Source_UOM.Item_Code = TSPL_SRN_DETAIL.Item_Code and Source_UOM.Uom_Code = TSPL_SRN_DETAIL.Unit_Code " & _
            '        " left outer Join tspl_item_uom_detail as Target_Uom on Target_Uom.Item_Code = TSPL_SRN_DETAIL.Item_Code and Target_Uom.Uom_Code = 'KG' " & _
            '        " where TSPL_SRN_DETAIL.SRN_QTY > 0 and TSPL_SRN_DETAIL.Item_Code in ( select distinct Item_Code from TSPL_PP_BOM_ITEM_DETAIL where BOM_CODE  in ('BOM/18-19/00004','BOM/18-19/00001','BOM/18-19/00080') )  and TSPL_SRN_HEAD.BILL_TO_LOCATION  = '001' ) XXXX    " & _
            '        " )  as TBL_SRN on TBL_SRN.PO_ID = TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No " & _
            '        " left outer Join tspl_item_uom_detail as Source_UOM on Source_UOM.Item_Code = TSPL_PURCHASE_ORDER_Detail.Item_Code and Source_UOM.Uom_Code = TSPL_PURCHASE_ORDER_Detail.Unit_Code " & _
            '        " left outer Join tspl_item_uom_detail as Target_Uom on Target_Uom.Item_Code = TSPL_PURCHASE_ORDER_Detail.Item_Code and Target_Uom.Uom_Code = 'KG'  " & _
            '        " left outer Join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_PURCHASE_ORDER_Detail.Item_Code " & _
            '        " left outer Join TSPL_Vendor_Master on TSPL_Vendor_Master.Vendor_Code = TSPL_PURCHASE_ORDER_HEAD.Vendor_Code " & _
            '        " where  " & _
            '        " TSPL_PURCHASE_ORDER_HEAD.Status =1   " & _
            '        " and TSPL_PURCHASE_ORDER_HEAD.Close_yn = 'N'   " & _
            '        " and TSPL_PURCHASE_ORDER_Detail.Item_Code in (select distinct Item_Code from TSPL_PP_BOM_ITEM_DETAIL where BOM_CODE  in ('BOM/18-19/00004','BOM/18-19/00001','BOM/18-19/00080')) " & _
            '        " and TSPL_PURCHASE_ORDER_HEAD.Bill_To_Location = '001' " & _
            '        " ) Final left outer join TSPL_LOCATION_MASTER  on TSPL_LOCATION_MASTER.Location_Code = Final.Bill_To_Location  where ( case when Final.SRN_QTY < = Final.Final_Qty_In_Target_UOM  then   Final.Final_Qty_In_Target_UOM  -  Final.SRN_QTY else 0 end) > 0 "
            Qry = " select  Final.PurchaseOrder_No as [PO No],max (Convert (varchar,Final.PurchaseOrder_Date,103)) as [PO Date] , max(Final.Vendor_Code) as Vendor_Code,max(Final.Vendor_Name) as Vendor_Name,max (Final.Item_Code) as [Item Code],max (Final.Item_Desc) as [Item Desc],max(Final.Bill_To_Location ) as [Location Code], max (Final.Unit_Code) as [Unit] ,max (TSPL_LOCATION_MASTER.Location_Desc) as [Location Desc],max (Final.Final_Qty_In_Target_UOM )as [PO Qty] ,sum (isnull (Final.SRN_QTY,0)) as [SRN Qty],sum (isnull(Final.GRN_QTY,0) )as [GRN Qty],sum (isnull(Final.MRN_QTY,0) )as [MRN Qty] , sum (( case when Final.SRN_QTY < = Final.Final_Qty_In_Target_UOM  then   Final.Final_Qty_In_Target_UOM  -  isNull (Final.SRN_QTY,0) else 0 end)) as [Pending PO Qty] " & _
                  " from  ( " & _
                  " select  " & _
                  " TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No, TSPL_PURCHASE_ORDER_HEAD.Vendor_Code,TSPL_Vendor_Master.Vendor_Name,Convert (varchar,TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_Date,103) as PurchaseOrder_Date , TSPL_PURCHASE_ORDER_Detail.Item_Code,TSPL_ITEM_MASTER.Item_Desc  , TSPL_PURCHASE_ORDER_HEAD.Bill_To_Location, " & _
                  " TSPL_PURCHASE_ORDER_Detail.Unit_Code,  " & _
                  " Convert (decimal (18,2),isnull (TBL_SRN.SRN_QTY,0) ) as SRN_QTY  ,TBL_SRN.GRN_QTY,TBL_SRN.MRN_QTY, " & _
                  " Convert (decimal (18,2), isnull (TSPL_PURCHASE_ORDER_Detail.PurchaseOrder_Qty,0))    as Final_Qty_In_Target_UOM    " & _
                  " from TSPL_PURCHASE_ORDER_HEAD left outer join TSPL_PURCHASE_ORDER_Detail on TSPL_PURCHASE_ORDER_Detail.PurchaseOrder_No =TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No " & _
                  " left outer join  " & _
                  " (  " & _
                  " select XXXX.PO_ID,XXXX.PO_Qty,XXXX.SRN_QTY,XXXX.GRN_QTY,XXXX.MRN_QTY, XXXX.Item_Code,XXXX.Unit_code from ( " & _
                  " select TSPL_SRN_DETAIL.PO_ID , Convert (decimal (18,2),isnull(TSPL_SRN_DETAIL.PO_Qty,0) ) as PO_Qty " & _
                  " , Convert (decimal (18,2),isnull (TSPL_SRN_DETAIL.SRN_QTY,0)) as SRN_QTY,Convert (decimal (18,2),isnull (TSPL_SRN_DETAIL.GRN_QTY,0)) as GRN_QTY ,Convert (decimal (18,2),isnull (TSPL_SRN_DETAIL.MRN_Qty,0)) as MRN_QTY , TSPL_SRN_DETAIL.Item_Code,TSPL_SRN_DETAIL. Unit_Code from TSPL_SRN_DETAIL  " & _
                  " left outer join TSPL_SRN_HEAD on TSPL_SRN_HEAD.SRN_NO =TSPL_SRN_DETAIL.SRN_No  " & _
                  " where TSPL_SRN_DETAIL.SRN_QTY > 0 and TSPL_SRN_DETAIL.Item_Code in ( select distinct Item_Code from TSPL_PP_BOM_ITEM_DETAIL where BOM_CODE  in (" + strBom + ") )  and TSPL_SRN_HEAD.BILL_TO_LOCATION  = '" + fndLocation.Value + "' ) XXXX    " & _
                  " )  as TBL_SRN on TBL_SRN.PO_ID = TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No   and TBL_SRN.Item_Code = TSPL_PURCHASE_ORDER_DETAIL.Item_Code " & _
                  " left outer Join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_PURCHASE_ORDER_Detail.Item_Code " & _
                  " left outer Join TSPL_Vendor_Master on TSPL_Vendor_Master.Vendor_Code = TSPL_PURCHASE_ORDER_HEAD.Vendor_Code " & _
                  " where  " & _
                  " TSPL_PURCHASE_ORDER_HEAD.Status =1   " & _
                  " and TSPL_PURCHASE_ORDER_HEAD.Close_yn = 'N'   " & _
                  " and TSPL_PURCHASE_ORDER_Detail.Item_Code in (select distinct Item_Code from TSPL_PP_BOM_ITEM_DETAIL where BOM_CODE  in (" + strBom + ")) " & _
                  " and TSPL_PURCHASE_ORDER_HEAD.Bill_To_Location = '" + fndLocation.Value + "' " & _
                  " ) Final left outer join TSPL_LOCATION_MASTER  on TSPL_LOCATION_MASTER.Location_Code = Final.Bill_To_Location  where ( case when Final.SRN_QTY < = Final.Final_Qty_In_Target_UOM  then   Final.Final_Qty_In_Target_UOM  -  Final.SRN_QTY else 0 end) > 0 group by PurchaseOrder_No,Item_Code "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                gvPendingPO.DataSource = Nothing
                gvPendingPO.Rows.Clear()
                gvPendingPO.Rows.AddNew()
                For Each dr As DataRow In dt.Rows
                    gvPendingPO.Rows(gvPendingPO.Rows.Count - 1).Cells(colSNO4).Value = gvPendingPO.Rows.Count
                    gvPendingPO.Rows(gvPendingPO.Rows.Count - 1).Cells(colPoNo4).Value = clsCommon.myCstr(dr("PO No"))
                    gvPendingPO.Rows(gvPendingPO.Rows.Count - 1).Cells(colPoDate4).Value = clsCommon.myCstr(dr("PO Date"))
                    gvPendingPO.Rows(gvPendingPO.Rows.Count - 1).Cells(colVendorCode4).Value = clsCommon.myCstr(dr("Vendor_Code"))
                    gvPendingPO.Rows(gvPendingPO.Rows.Count - 1).Cells(colVendorName4).Value = clsCommon.myCstr(dr("Vendor_Name"))
                    gvPendingPO.Rows(gvPendingPO.Rows.Count - 1).Cells(colLocationCode4).Value = clsCommon.myCstr(dr("Location Code"))
                    gvPendingPO.Rows(gvPendingPO.Rows.Count - 1).Cells(colLocationName4).Value = clsCommon.myCstr(dr("Location Desc"))
                    gvPendingPO.Rows(gvPendingPO.Rows.Count - 1).Cells(colItemCode4).Value = clsCommon.myCstr(dr("Item Code"))
                    gvPendingPO.Rows(gvPendingPO.Rows.Count - 1).Cells(colItemDesc4).Value = clsCommon.myCstr(dr("Item Desc"))
                    gvPendingPO.Rows(gvPendingPO.Rows.Count - 1).Cells(colUnitCode4).Value = clsCommon.myCstr(dr("Unit"))
                    gvPendingPO.Rows(gvPendingPO.Rows.Count - 1).Cells(colPoQty4).Value = clsCommon.myCdbl(dr("PO Qty"))
                    gvPendingPO.Rows(gvPendingPO.Rows.Count - 1).Cells(colGRNQty4).Value = clsCommon.myCdbl(dr("GRN Qty"))
                    gvPendingPO.Rows(gvPendingPO.Rows.Count - 1).Cells(colMRNQty4).Value = clsCommon.myCdbl(dr("MRN Qty"))
                    gvPendingPO.Rows(gvPendingPO.Rows.Count - 1).Cells(colSRNQty4).Value = clsCommon.myCdbl(dr("SRN Qty"))
                    gvPendingPO.Rows(gvPendingPO.Rows.Count - 1).Cells(colPendingPoQty4).Value = clsCommon.myCdbl(dr("Pending PO Qty"))
                    ' '  ' GRN_QTY, MRN_QTY,SRN_Qty,Pending_PO_Qty
                    'colGRNQty4,colMRNQty4,colSRNQty4,colPendingPoQty4
                    gvPendingPO.Rows.AddNew()
                Next
            Else
                gvPendingPO.DataSource = Nothing
            End If

            ' ===================================== PendingSRN =====================================================================
            'Qry = " select TSPL_SRN_HEAD.Vendor_Code,TSPL_VENDOR_MASTER.Vendor_Name, TSPL_SRN_HEAD.SRN_No as [SRN No],convert ( varchar, TSPL_SRN_HEAD.SRN_DATE,103) as [SRN Date], TSPL_SRN_DETAIL.Item_Code [Item Code] ,TSPL_ITEM_MASTER.Item_Desc as [Item Desc], TSPL_SRN_HEAD.Bill_To_Location as [Location Code],TSPL_LOCATION_MASTER.Location_Desc as [Location Desc], TSPL_SRN_DETAIL.Unit_Code as [Unit],Convert (decimal (18,2) ,TSPL_SRN_DETAIL.SRN_Qty ) as [SRN Qty] from TSPL_SRN_DETAIL " & _
            '      " inner join TSPL_SRN_HEAD on TSPL_SRN_DETAIL.SRN_No =TSPL_SRN_HEAD.SRN_NO  " & _
            '      " inner join  TSPL_PP_BOM_ITEM_DETAIL  on TSPL_SRN_DETAIL.Item_Code = TSPL_PP_BOM_ITEM_DETAIL.ITEM_CODE " & _
            '      " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.ITEM_CODE = TSPL_SRN_DETAIL.Item_Code " & _
            '      " left Outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code = TSPL_SRN_HEAD.Bill_To_Location " & _
            '      " left Outer Join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code = TSPL_SRN_HEAD.Vendor_Code " & _
            '      " where  TSPL_SRN_HEAD.Status =0 and TSPL_SRN_HEAD.Bill_To_Location = '" + fndLocation.Value + "' and TSPL_PP_BOM_ITEM_DETAIL.BOM_CODE in (" + strBom + ") "
            Qry = " select TSPL_SRN_DETAIL.SRN_No as [SRN No],Convert ( varchar,TSPL_SRN_HEAD.SRN_Date,103) as [SRN Date],TSPL_SRN_HEAD.Vendor_Code,TSPL_VENDOR_MASTER.Vendor_Name,TSPL_SRN_HEAD.Bill_To_Location as [Location Code],TSPL_LOCATION_MASTER.Location_Desc as [Location Desc], TSPL_SRN_DETAIL.PO_ID as Purchase_Order,  TSPL_SRN_DETAIL.PO_Qty ,Convert (decimal (18,2),isnull (TSPL_SRN_DETAIL.SRN_QTY ,0) )  as [SRN Qty]  , TSPL_SRN_DETAIL.Item_Code as [Item Code],TSPL_ITEM_MASTER.Item_Desc as [Item Desc], TSPL_SRN_DETAIL.Unit_Code as [Unit] from TSPL_SRN_DETAIL left outer join TSPL_SRN_HEAD on TSPL_SRN_HEAD.SRN_NO =TSPL_SRN_DETAIL.SRN_No  " & _
                  " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_SRN_DETAIL.Item_Code " & _
                  " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code = TSPL_SRN_HEAD.Bill_To_Location " & _
                  " Left Outer Join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code = TSPL_SRN_HEAD.Vendor_Code " & _
                  " where TSPL_SRN_HEAD.Status = 0 and SRN_QTY > 0   and TSPL_SRN_DETAIL.Item_Code in ( select distinct Item_Code from TSPL_PP_BOM_ITEM_DETAIL where BOM_CODE  in (" + strBom + ")) and TSPL_SRN_HEAD.Bill_To_Location = '" + fndLocation.Value + "' "
            Dim dt2 As DataTable = clsDBFuncationality.GetDataTable(Qry)
            If dt2 IsNot Nothing AndAlso dt2.Rows.Count > 0 Then
                gvPendingSRN.DataSource = Nothing
                gvPendingSRN.Rows.Clear()
                gvPendingSRN.Rows.AddNew()
                For Each dr2 As DataRow In dt2.Rows
                    gvPendingSRN.Rows(gvPendingSRN.Rows.Count - 1).Cells(colSNO5).Value = gvPendingPO.Rows.Count
                    gvPendingSRN.Rows(gvPendingSRN.Rows.Count - 1).Cells(colSrnNo5).Value = clsCommon.myCstr(dr2("SRN No"))
                    gvPendingSRN.Rows(gvPendingSRN.Rows.Count - 1).Cells(colSrnDate5).Value = clsCommon.myCstr(dr2("SRN Date"))
                    gvPendingSRN.Rows(gvPendingSRN.Rows.Count - 1).Cells(colVendorCode5).Value = clsCommon.myCstr(dr2("Vendor_Code"))
                    gvPendingSRN.Rows(gvPendingSRN.Rows.Count - 1).Cells(colVendorName5).Value = clsCommon.myCstr(dr2("Vendor_Name"))
                    gvPendingSRN.Rows(gvPendingSRN.Rows.Count - 1).Cells(colLocationCode5).Value = clsCommon.myCstr(dr2("Location Code"))
                    gvPendingSRN.Rows(gvPendingSRN.Rows.Count - 1).Cells(colLocationName5).Value = clsCommon.myCstr(dr2("Location Desc"))
                    gvPendingSRN.Rows(gvPendingSRN.Rows.Count - 1).Cells(colItemCode5).Value = clsCommon.myCstr(dr2("Item Code"))
                    gvPendingSRN.Rows(gvPendingSRN.Rows.Count - 1).Cells(colItemDesc5).Value = clsCommon.myCstr(dr2("Item Desc"))
                    gvPendingSRN.Rows(gvPendingSRN.Rows.Count - 1).Cells(colUnitCode5).Value = clsCommon.myCstr(dr2("Unit"))
                    gvPendingSRN.Rows(gvPendingSRN.Rows.Count - 1).Cells(colSRNQty5).Value = clsCommon.myCstr(dr2("SRN Qty"))
                    gvPendingSRN.Rows.AddNew()
                Next
            Else
                gvPendingSRN.DataSource = Nothing
            End If
            '=============================================MRP DEtail Item Wise==========================================================================
            'If clsCommon.myLen(TxtitemType.SelectedValue) > 0 Then


            Qry = " select YYYY.*, TSPL_PP_BOM_ITEM_DETAIL.Item_Code, TSPL_ITEM_MASTER.Item_Desc,TSPL_ITEM_MASTER.Item_Type , TSPL_ITEM_TYPE_MASTER.ITEM_TYPE_NAME, TSPL_PP_BOM_ITEM_DETAIL.Unit_Code , TSPL_PP_BOM_ITEM_DETAIL.Quantity , Convert (decimal(18,2) , (TSPL_PP_BOM_ITEM_DETAIL.Quantity / YYYY.BOM_QTY ) * YYYY.Qty) as Reqired_Qty  from (  " & _
             " select max (XXXX.BOM_CODE) as BOM_CODE , XXXX.Item_Code as Main_Item_Code , max (XXXX.Item_Desc) as Main_Item_Desc  , sum (XXXX.Final_Qty) as Qty , max ( XXXX.BOM_QTY ) as BOM_QTY , max ( XXXX.BOM_UNIT_CODE ) as BOM_UNIT_CODE from ( " & _
             " select Final.* , TSPL_PP_BOM_HEAD.PROD_ITEM_UNIT_CODE as BOM_UNIT_CODE, TSPL_PP_BOM_HEAD.PROD_QUANTITY as BOM_QTY, TSPL_ITEM_UOM_DETAIL_Source_UOM.Conversion_Factor as Source_UOM_Conv_Fact, TSPL_ITEM_UOM_DETAIL_TARGET_UOM.Conversion_Factor as Target_Uom_Conv_Fact, TBL_ITEM_STOCKING_UNIT.Conversion_Factor as Item_Stocking_Unit_Conversion_Factor ," & _
             " Convert (decimal (18,2),((Convert (decimal(18,2) , TSPL_ITEM_UOM_DETAIL_Source_UOM.Conversion_Factor ) / Convert (decimal(18,2), TSPL_ITEM_UOM_DETAIL_TARGET_UOM.Conversion_Factor )) * Convert (Decimal(18,2), Final.plan_qty ) ) ) as Final_Qty from ( " & _
             "  " + strBaseQry + "  " & _
             "  " & _
             "  " & _
             " ) Final left outer join TSPL_PP_BOM_HEAD on TSPL_PP_BOM_HEAD.BOM_CODE = Final.BOM_CODE " & _
             " left outer join TSPL_ITEM_UOM_DETAIL as TSPL_ITEM_UOM_DETAIL_Source_UOM on TSPL_ITEM_UOM_DETAIL_Source_UOM.Item_Code = Final.Item_Code and  Final.Unit_Code = TSPL_ITEM_UOM_DETAIL_Source_UOM.UOM_Code " & _
             " left outer join TSPL_ITEM_UOM_DETAIL as TSPL_ITEM_UOM_DETAIL_TARGET_UOM on TSPL_ITEM_UOM_DETAIL_TARGET_UOM.Item_Code =   Final.Item_Code and TSPL_ITEM_UOM_DETAIL_TARGET_UOM.Uom_code = TSPL_PP_BOM_HEAD.PROD_ITEM_UNIT_CODE " & _
             " left outer join  (select Item_Code , UOM_Code , Conversion_Factor  FROM TSPL_ITEM_UOM_DETAIL where Stocking_Unit = 'Y' ) as TBL_ITEM_STOCKING_UNIT on TBL_ITEM_STOCKING_UNIT.Item_Code = Final.Item_Code " & _
             " ) XXXX  group by XXXX.Item_Code  " & _
             " ) YYYY  left outer join TSPL_PP_BOM_ITEM_DETAIL on TSPL_PP_BOM_ITEM_DETAIL.Bom_code = YYYY.Bom_Code  " & _
             " left outer join TSPL_ITEM_MASTER on    TSPL_PP_BOM_ITEM_DETAIL.Item_Code = TSPL_ITEM_MASTER.Item_Code " & _
             " left outer join TSPL_ITEM_TYPE_MASTER on TSPL_ITEM_TYPE_MASTER.ITEM_TYPE_CODE = TSPL_ITEM_MASTER.Item_Type "
            'Else
            '    Qry = " select YYYY.*, TSPL_PP_BOM_ITEM_DETAIL.Item_Code, TSPL_ITEM_MASTER.Item_Desc,TSPL_ITEM_MASTER.Item_Type , TSPL_ITEM_TYPE_MASTER.ITEM_TYPE_NAME, TSPL_PP_BOM_ITEM_DETAIL.Unit_Code , TSPL_PP_BOM_ITEM_DETAIL.Quantity , Convert (decimal(18,2) , (TSPL_PP_BOM_ITEM_DETAIL.Quantity / YYYY.BOM_QTY ) * YYYY.Qty) as Reqired_Qty  from (  " & _
            '    " select max (XXXX.BOM_CODE) as BOM_CODE , XXXX.Item_Code as Main_Item_Code , max (XXXX.Item_Desc) as Main_Item_Desc  , sum (XXXX.Final_Qty) as Qty , max ( XXXX.BOM_QTY ) as BOM_QTY , max ( XXXX.BOM_UNIT_CODE ) as BOM_UNIT_CODE from ( " & _
            '    " select Final.* , TSPL_PP_BOM_HEAD.PROD_ITEM_UNIT_CODE as BOM_UNIT_CODE, TSPL_PP_BOM_HEAD.PROD_QUANTITY as BOM_QTY, TSPL_ITEM_UOM_DETAIL_Source_UOM.Conversion_Factor as Source_UOM_Conv_Fact, TSPL_ITEM_UOM_DETAIL_TARGET_UOM.Conversion_Factor as Target_Uom_Conv_Fact, TBL_ITEM_STOCKING_UNIT.Conversion_Factor as Item_Stocking_Unit_Conversion_Factor ," & _
            '    " Convert (decimal (18,2),((Convert (decimal(18,2) , TSPL_ITEM_UOM_DETAIL_Source_UOM.Conversion_Factor ) / Convert (decimal(18,2), TSPL_ITEM_UOM_DETAIL_TARGET_UOM.Conversion_Factor )) * Convert (Decimal(18,2), Final.plan_qty ) ) ) as Final_Qty from ( " & _
            '    " select  TSPL_PP_PRODUCTION_PLAN_DETAIL.Item_Code,TSPL_ITEM_MASTER.Item_Desc , TSPL_PP_PRODUCTION_PLAN_DETAIL.Unit_Code ,TSPL_ITEM_MASTER.Item_Type,TSPL_ITEM_TYPE_MASTER.ITEM_TYPE_NAME, TSPL_PP_PRODUCTION_PLAN_DETAIL.plan_qty , TBL_LATEST_BOM.BOM_CODE from TSPL_PP_PRODUCTION_PLAN_DETAIL left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.ITEM_CODE = TSPL_PP_PRODUCTION_PLAN_DETAIL.ITEM_CODE  left outer join TSPL_ITEM_TYPE_MASTER  on TSPL_ITEM_TYPE_MASTER.ITEM_TYPE_CODE = TSPL_ITEM_MASTER.Item_Type " & _
            '    " left outer join (select PROD_ITEM_CODE,BOM_CODE from (   select convert (nvarchar, DENSE_RANK() OVER (PARTITION BY TSPL_PP_BOM_HEAD.PROD_ITEM_CODE ORDER BY PROD_ITEM_CODE, CONVERT (datetime , Created_Date , 103) , BOM_CODE desc)) as SNO, TSPL_PP_BOM_HEAD.PROD_ITEM_CODE, TSPL_PP_BOM_HEAD.BOM_CODE  from TSPL_PP_BOM_HEAD    )Final where Final.SNO = 1) as TBL_LATEST_BOM on TBL_LATEST_BOM.PROD_ITEM_CODE = TSPL_PP_PRODUCTION_PLAN_DETAIL.Item_Code " & _
            '    " where TSPL_PP_PRODUCTION_PLAN_DETAIL.Plan_Code in (" + clsCommon.GetMulcallString(txtMulProductionPlan.arrValueMember) + ") " & _
            '    " ) Final left outer join TSPL_PP_BOM_HEAD on TSPL_PP_BOM_HEAD.BOM_CODE = Final.BOM_CODE " & _
            '    " left outer join TSPL_ITEM_UOM_DETAIL as TSPL_ITEM_UOM_DETAIL_Source_UOM on TSPL_ITEM_UOM_DETAIL_Source_UOM.Item_Code = Final.Item_Code and  Final.Unit_Code = TSPL_ITEM_UOM_DETAIL_Source_UOM.UOM_Code " & _
            '    " left outer join TSPL_ITEM_UOM_DETAIL as TSPL_ITEM_UOM_DETAIL_TARGET_UOM on TSPL_ITEM_UOM_DETAIL_TARGET_UOM.Item_Code =   Final.Item_Code and TSPL_ITEM_UOM_DETAIL_TARGET_UOM.Uom_code = TSPL_PP_BOM_HEAD.PROD_ITEM_UNIT_CODE " & _
            '    " left outer join  (select Item_Code , UOM_Code , Conversion_Factor  FROM TSPL_ITEM_UOM_DETAIL where Stocking_Unit = 'Y' ) as TBL_ITEM_STOCKING_UNIT on TBL_ITEM_STOCKING_UNIT.Item_Code = Final.Item_Code " & _
            '    " ) XXXX  group by XXXX.Item_Code  " & _
            '    " ) YYYY  left outer join TSPL_PP_BOM_ITEM_DETAIL on TSPL_PP_BOM_ITEM_DETAIL.Bom_code = YYYY.Bom_Code  " & _
            '    " left outer join TSPL_ITEM_MASTER on    TSPL_PP_BOM_ITEM_DETAIL.Item_Code = TSPL_ITEM_MASTER.Item_Code " & _
            '    " left outer join TSPL_ITEM_TYPE_MASTER on TSPL_ITEM_TYPE_MASTER.ITEM_TYPE_CODE = TSPL_ITEM_MASTER.Item_Type "
            'End If


            Dim dtBom As DataTable = clsDBFuncationality.GetDataTable(Qry)
            If dtBom IsNot Nothing AndAlso dtBom.Rows.Count > 0 Then
                gvBalQty.DataSource = Nothing
                gvBalQty.Rows.Clear()
                gvBalQty.Rows.AddNew()
                For Each dr2 As DataRow In dtBom.Rows

                    gvBalQty.Rows(gvBalQty.Rows.Count - 1).Cells(colSNO).Value = gvBalQty.Rows.Count
                    gvBalQty.Rows(gvBalQty.Rows.Count - 1).Cells(colMainItemCode).Value = clsCommon.myCstr(dr2("Main_Item_Code"))
                    gvBalQty.Rows(gvBalQty.Rows.Count - 1).Cells(colMainItemDesc).Value = clsCommon.myCstr(dr2("Main_Item_Desc"))
                    gvBalQty.Rows(gvBalQty.Rows.Count - 1).Cells(colItemCode).Value = clsCommon.myCstr(dr2("Item_Code"))
                    gvBalQty.Rows(gvBalQty.Rows.Count - 1).Cells(colItemDesc).Value = clsCommon.myCstr(dr2("Item_Desc")) 'clsItemMaster.GetItemName(clsCommon.myCstr(dr("consm_item_code")), Nothing)
                    gvBalQty.Rows(gvBalQty.Rows.Count - 1).Cells(colUnitCode).Value = clsCommon.myCstr(dr2("Unit_Code"))
                    gvBalQty.Rows(gvBalQty.Rows.Count - 1).Cells(colItemType).Value = clsCommon.myCstr(dr2("ITEM_TYPE_NAME"))
                    gvBalQty.Rows(gvBalQty.Rows.Count - 1).Cells(colTotalQtyRequired).Value = clsCommon.myCdbl(dr2("Reqired_Qty"))
                    Dim RequiredQty As Double = clsCommon.myCdbl(dr2("Reqired_Qty"))
                    Dim StockQty As Double = clsProductionMRP.GetStockQty(clsCommon.myCstr(dr2("Item_Code")), clsCommon.myCstr(dr2("Unit_Code")), fndLocation.Value)
                    Dim PendingPOQty As Double = clsProductionMRP.GetPendingPOkQty(clsCommon.myCstr(dr2("Item_Code")), clsCommon.myCstr(dr2("Unit_Code")), fndLocation.Value)
                    Dim PendingSRNQty As Double = clsProductionMRP.GetPendingSRNkQty(clsCommon.myCstr(dr2("Item_Code")), clsCommon.myCstr(dr2("Unit_Code")), fndLocation.Value)
                    Dim NetRequriedQty As Double = 0
                    If RequiredQty > (StockQty + PendingPOQty + PendingSRNQty) Then
                        NetRequriedQty = RequiredQty - (StockQty + PendingPOQty + PendingSRNQty)
                    End If
                    'gvBalQty.Rows(gvBalQty.Rows.Count - 1).Cells(colStockQty).Value = StockQty
                    'gvBalQty.Rows(gvBalQty.Rows.Count - 1).Cells(colPOQty).Value = PendingPOQty
                    'gvBalQty.Rows(gvBalQty.Rows.Count - 1).Cells(colSRN_Qty).Value = PendingSRNQty
                    'gvBalQty.Rows(gvBalQty.Rows.Count - 1).Cells(colNetRequireQty).Value = NetRequriedQty
                    gvBalQty.Rows.AddNew()
                Next
            End If

            '==========================Item Details with Stock Qty========================================
            'Qry = " select  KKKK.* ,  Convert (decimal (18,2),((Convert (decimal(18,2) , TSPL_ITEM_UOM_DETAIL_Source_UOM.Conversion_Factor ) / Convert (decimal(18,2), TSPL_ITEM_UOM_DETAIL_TARGET_UOM.Conversion_Factor )) * Convert (Decimal(18,2), KKKK.Reqired_Qty ) ) ) as Firanl_Requeried_Qty from ( " & _
            '      " select SSSS.ITEM_CODE , max( SSSS.Item_Desc) as  Item_Desc  , max (SSSS.Item_Type) as Item_Type ,max (SSSS.Item_TYPE_NAME) as Item_TYPE_NAME , (SSSS.Unit_CODE) as Unit_CODE ,SSSS.BOM_UNIT_CODE, sum(SSSS.QUANTITY) as QUANTITY , sum( SSSS.Reqired_Qty) as Reqired_Qty from ( " & _
            '      " select YYYY.*, TSPL_PP_BOM_ITEM_DETAIL.Item_Code, TSPL_ITEM_MASTER.Item_Desc,TSPL_ITEM_MASTER.Item_Type , TSPL_ITEM_TYPE_MASTER.ITEM_TYPE_NAME, TSPL_PP_BOM_ITEM_DETAIL.Unit_Code , TSPL_PP_BOM_ITEM_DETAIL.Quantity , Convert (decimal(18,2) , (TSPL_PP_BOM_ITEM_DETAIL.Quantity / YYYY.BOM_QTY ) * YYYY.Qty) as Reqired_Qty  from (   select max (XXXX.BOM_CODE) as BOM_CODE , XXXX.Item_Code as Main_Item_Code , max (XXXX.Item_Desc) as Main_Item_Desc  , sum (XXXX.Final_Qty) as Qty , max ( XXXX.BOM_QTY ) as BOM_QTY , max ( XXXX.BOM_UNIT_CODE ) as BOM_UNIT_CODE from (  select Final.* , TSPL_PP_BOM_HEAD.PROD_ITEM_UNIT_CODE as BOM_UNIT_CODE, TSPL_PP_BOM_HEAD.PROD_QUANTITY as BOM_QTY, TSPL_ITEM_UOM_DETAIL_Source_UOM.Conversion_Factor as Source_UOM_Conv_Fact, TSPL_ITEM_UOM_DETAIL_TARGET_UOM.Conversion_Factor as Target_Uom_Conv_Fact, TBL_ITEM_STOCKING_UNIT.Conversion_Factor as Item_Stocking_Unit_Conversion_Factor , Convert (decimal (18,2),((Convert (decimal(18,2) , TSPL_ITEM_UOM_DETAIL_Source_UOM.Conversion_Factor ) / Convert (decimal(18,2), TSPL_ITEM_UOM_DETAIL_TARGET_UOM.Conversion_Factor )) * Convert (Decimal(18,2), Final.plan_qty ) ) ) as Final_Qty from (  "
            'If clsCommon.myLen(TxtitemType.SelectedValue) > 0 Then
            '    Qry = Qry + " " + strBaseQry
            'Else
            '    Qry = Qry + " select  TSPL_PP_PRODUCTION_PLAN_DETAIL.Item_Code,TSPL_ITEM_MASTER.Item_Desc , TSPL_PP_PRODUCTION_PLAN_DETAIL.Unit_Code ,TSPL_ITEM_MASTER.Item_Type,TSPL_ITEM_TYPE_MASTER.ITEM_TYPE_NAME, TSPL_PP_PRODUCTION_PLAN_DETAIL.plan_qty , TBL_LATEST_BOM.BOM_CODE from TSPL_PP_PRODUCTION_PLAN_DETAIL left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.ITEM_CODE = TSPL_PP_PRODUCTION_PLAN_DETAIL.ITEM_CODE  left outer join TSPL_ITEM_TYPE_MASTER  on TSPL_ITEM_TYPE_MASTER.ITEM_TYPE_CODE = TSPL_ITEM_MASTER.Item_Type " & _
            '    " left outer join (select PROD_ITEM_CODE,BOM_CODE from (   select convert (nvarchar, DENSE_RANK() OVER (PARTITION BY TSPL_PP_BOM_HEAD.PROD_ITEM_CODE ORDER BY PROD_ITEM_CODE, CONVERT (datetime , Created_Date , 103) , BOM_CODE desc)) as SNO, TSPL_PP_BOM_HEAD.PROD_ITEM_CODE, TSPL_PP_BOM_HEAD.BOM_CODE  from TSPL_PP_BOM_HEAD    )Final where Final.SNO = 1) as TBL_LATEST_BOM on TBL_LATEST_BOM.PROD_ITEM_CODE = TSPL_PP_PRODUCTION_PLAN_DETAIL.Item_Code " & _
            '    " where TSPL_PP_PRODUCTION_PLAN_DETAIL.Plan_Code in (" + clsCommon.GetMulcallString(txtMulProductionPlan.arrValueMember) + ") "
            'End If
            'Qry = Qry + " ) Final left outer join TSPL_PP_BOM_HEAD on TSPL_PP_BOM_HEAD.BOM_CODE = Final.BOM_CODE  left outer join TSPL_ITEM_UOM_DETAIL as TSPL_ITEM_UOM_DETAIL_Source_UOM on TSPL_ITEM_UOM_DETAIL_Source_UOM.Item_Code = Final.Item_Code and  Final.Unit_Code = TSPL_ITEM_UOM_DETAIL_Source_UOM.UOM_Code  left outer join TSPL_ITEM_UOM_DETAIL as TSPL_ITEM_UOM_DETAIL_TARGET_UOM on TSPL_ITEM_UOM_DETAIL_TARGET_UOM.Item_Code =   Final.Item_Code and TSPL_ITEM_UOM_DETAIL_TARGET_UOM.Uom_code = TSPL_PP_BOM_HEAD.PROD_ITEM_UNIT_CODE  left outer join  (select Item_Code , UOM_Code , Conversion_Factor  FROM TSPL_ITEM_UOM_DETAIL where Stocking_Unit = 'Y' ) as TBL_ITEM_STOCKING_UNIT on TBL_ITEM_STOCKING_UNIT.Item_Code = Final.Item_Code  ) XXXX  group by XXXX.Item_Code   ) YYYY  left outer join TSPL_PP_BOM_ITEM_DETAIL on TSPL_PP_BOM_ITEM_DETAIL.Bom_code = YYYY.Bom_Code   left outer join TSPL_ITEM_MASTER on    TSPL_PP_BOM_ITEM_DETAIL.Item_Code = TSPL_ITEM_MASTER.Item_Code  left outer join TSPL_ITEM_TYPE_MASTER on TSPL_ITEM_TYPE_MASTER.ITEM_TYPE_CODE = TSPL_ITEM_MASTER.Item_Type " & _
            '            " ) SSSS Group by SSSS.ITEM_CODE , SSSS.BOM_UNIT_CODE, SSSS.UNIT_CODE ) KKKK " & _
            '            " left outer join TSPL_ITEM_UOM_DETAIL as TSPL_ITEM_UOM_DETAIL_Source_UOM on TSPL_ITEM_UOM_DETAIL_Source_UOM.Item_Code = KKKK.Item_Code and  KKKK.Unit_Code = TSPL_ITEM_UOM_DETAIL_Source_UOM.UOM_Code  " & _
            '            " left outer join TSPL_ITEM_UOM_DETAIL as TSPL_ITEM_UOM_DETAIL_TARGET_UOM on TSPL_ITEM_UOM_DETAIL_TARGET_UOM.Item_Code =   KKKK.Item_Code and TSPL_ITEM_UOM_DETAIL_TARGET_UOM.Uom_code = KKKK .BOM_UNIT_CODE " & _
            '            "  inner join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = KKKK.Item_Code where TSPL_ITEM_MASTER.Product_Type <> 'MI' "

            Qry = " select FinalMain.ITEM_CODE , max (FinalMain.Item_Desc ) as Item_Desc ,max(ITEM_TYPE_NAME) as ITEM_TYPE_NAME,max ( case when Conversion_Factor is null OR Targer_UoM_Conver_Fact is null then  FinalMain.BOM_UNIT_CODE else FinalMain.Stocking_Unit end) as Unit_Code ,  sum( case when Conversion_Factor is null OR Targer_UoM_Conver_Fact is null then FinalMain.Reqired_Qty else   Convert (decimal (18,2),((Convert (decimal(18,2) , Source_UOM.Conversion_Factor ) / Convert (decimal(18,2), Targer_UoM_Conver_Fact )) * Convert (Decimal(18,2), FinalMain.Reqired_Qty ) ) ) end) as Reqired_Qty  from ( " & _
                 " select  KKKK.* ,  TSPL_ITEM_UOM_DETAIL_Source_UOM.UOM_Code  as Stocking_Unit , TSPL_ITEM_UOM_DETAIL_Source_UOM.Conversion_Factor  as Targer_UoM_Conver_Fact  from ( " & _
                 " select SSSS.ITEM_CODE , ( SSSS.Item_Desc) as  Item_Desc  ,  (SSSS.Item_Type) as Item_Type , (SSSS.Item_TYPE_NAME) as Item_TYPE_NAME , Unit_Code_Bom_Detail_Item as  BOM_UNIT_CODE, (SSSS.QUANTITY) as QUANTITY , ( SSSS.Reqired_Qty) as Reqired_Qty from ( " & _
                 " select YYYY.*, TSPL_PP_BOM_ITEM_DETAIL.Item_Code,TSPL_PP_BOM_ITEM_DETAIL.Unit_Code as Unit_Code_Bom_Detail_Item, TSPL_ITEM_MASTER.Item_Desc,TSPL_ITEM_MASTER.Item_Type , TSPL_ITEM_TYPE_MASTER.ITEM_TYPE_NAME, TSPL_PP_BOM_ITEM_DETAIL.Unit_Code , TSPL_PP_BOM_ITEM_DETAIL.Quantity , Convert (decimal(18,2) , (TSPL_PP_BOM_ITEM_DETAIL.Quantity / YYYY.BOM_QTY ) * YYYY.Qty) as Reqired_Qty  from (   select max (XXXX.BOM_CODE) as BOM_CODE , XXXX.Item_Code as Main_Item_Code , max (XXXX.Item_Desc) as Main_Item_Desc  , sum (XXXX.Final_Qty) as Qty , max ( XXXX.BOM_QTY ) as BOM_QTY , max ( XXXX.BOM_UNIT_CODE ) as BOM_UNIT_CODE from (  select Final.* , TSPL_PP_BOM_HEAD.PROD_ITEM_UNIT_CODE as BOM_UNIT_CODE, TSPL_PP_BOM_HEAD.PROD_QUANTITY as BOM_QTY, TSPL_ITEM_UOM_DETAIL_Source_UOM.Conversion_Factor as Source_UOM_Conv_Fact, TSPL_ITEM_UOM_DETAIL_TARGET_UOM.Conversion_Factor as Target_Uom_Conv_Fact, TBL_ITEM_STOCKING_UNIT.Conversion_Factor as Item_Stocking_Unit_Conversion_Factor , Convert (decimal (18,2),((Convert (decimal(18,2) , TSPL_ITEM_UOM_DETAIL_Source_UOM.Conversion_Factor ) / Convert (decimal(18,2), TSPL_ITEM_UOM_DETAIL_TARGET_UOM.Conversion_Factor )) * Convert (Decimal(18,2), Final.plan_qty ) ) ) as Final_Qty from (  "
            'If clsCommon.myLen(TxtitemType.SelectedValue) > 0 Then
            Qry = Qry + " " + strBaseQry
            'Else
            '    Qry = Qry + " select  TSPL_PP_PRODUCTION_PLAN_DETAIL.Item_Code,TSPL_ITEM_MASTER.Item_Desc , TSPL_PP_PRODUCTION_PLAN_DETAIL.Unit_Code ,TSPL_ITEM_MASTER.Item_Type,TSPL_ITEM_TYPE_MASTER.ITEM_TYPE_NAME, TSPL_PP_PRODUCTION_PLAN_DETAIL.plan_qty , TBL_LATEST_BOM.BOM_CODE from TSPL_PP_PRODUCTION_PLAN_DETAIL left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.ITEM_CODE = TSPL_PP_PRODUCTION_PLAN_DETAIL.ITEM_CODE  left outer join TSPL_ITEM_TYPE_MASTER  on TSPL_ITEM_TYPE_MASTER.ITEM_TYPE_CODE = TSPL_ITEM_MASTER.Item_Type " & _
            '    " left outer join (select PROD_ITEM_CODE,BOM_CODE from (   select convert (nvarchar, DENSE_RANK() OVER (PARTITION BY TSPL_PP_BOM_HEAD.PROD_ITEM_CODE ORDER BY PROD_ITEM_CODE, CONVERT (datetime , Created_Date , 103) , BOM_CODE desc)) as SNO, TSPL_PP_BOM_HEAD.PROD_ITEM_CODE, TSPL_PP_BOM_HEAD.BOM_CODE  from TSPL_PP_BOM_HEAD    )Final where Final.SNO = 1) as TBL_LATEST_BOM on TBL_LATEST_BOM.PROD_ITEM_CODE = TSPL_PP_PRODUCTION_PLAN_DETAIL.Item_Code " & _
            '    " where TSPL_PP_PRODUCTION_PLAN_DETAIL.Plan_Code in (" + clsCommon.GetMulcallString(txtMulProductionPlan.arrValueMember) + ") "
            'End If
            Qry = Qry + " ) Final left outer join TSPL_PP_BOM_HEAD on TSPL_PP_BOM_HEAD.BOM_CODE = Final.BOM_CODE  left outer join TSPL_ITEM_UOM_DETAIL as TSPL_ITEM_UOM_DETAIL_Source_UOM on TSPL_ITEM_UOM_DETAIL_Source_UOM.Item_Code = Final.Item_Code and  Final.Unit_Code = TSPL_ITEM_UOM_DETAIL_Source_UOM.UOM_Code  left outer join TSPL_ITEM_UOM_DETAIL as TSPL_ITEM_UOM_DETAIL_TARGET_UOM on TSPL_ITEM_UOM_DETAIL_TARGET_UOM.Item_Code =   Final.Item_Code and TSPL_ITEM_UOM_DETAIL_TARGET_UOM.Uom_code = TSPL_PP_BOM_HEAD.PROD_ITEM_UNIT_CODE  left outer join  (select Item_Code , UOM_Code , Conversion_Factor  FROM TSPL_ITEM_UOM_DETAIL where Stocking_Unit = 'Y' ) as TBL_ITEM_STOCKING_UNIT on TBL_ITEM_STOCKING_UNIT.Item_Code = Final.Item_Code  ) XXXX  group by XXXX.Item_Code   ) YYYY  left outer join TSPL_PP_BOM_ITEM_DETAIL on TSPL_PP_BOM_ITEM_DETAIL.Bom_code = YYYY.Bom_Code   left outer join TSPL_ITEM_MASTER on    TSPL_PP_BOM_ITEM_DETAIL.Item_Code = TSPL_ITEM_MASTER.Item_Code  left outer join TSPL_ITEM_TYPE_MASTER on TSPL_ITEM_TYPE_MASTER.ITEM_TYPE_CODE = TSPL_ITEM_MASTER.Item_Type " & _
                        " ) SSSS  ) KKKK " & _
                        " left outer join TSPL_ITEM_UOM_DETAIL as TSPL_ITEM_UOM_DETAIL_Source_UOM on TSPL_ITEM_UOM_DETAIL_Source_UOM.Item_Code = KKKK.Item_Code  and TSPL_ITEM_UOM_DETAIL_Source_UOM.Stocking_Unit = 'Y'  " & _
                        "  " & _
                        "  inner join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = KKKK.Item_Code where TSPL_ITEM_MASTER.Product_Type <> 'MI' " & _
                        "  ) FinalMain  left outer join   TSPL_ITEM_UOM_DETAIL  as Source_UOM on Source_UOM.Item_Code = FinalMain.ITEM_CODE  and Source_UOM.UOM_Code = FinalMain.BOM_UNIT_CODE 	group by FinalMain.ITEM_CODE,FinalMain.Stocking_Unit "

            Dim dtBomItem As DataTable = clsDBFuncationality.GetDataTable(Qry)
            If dtBomItem IsNot Nothing AndAlso dtBomItem.Rows.Count > 0 Then
                gvBalItemDetails.DataSource = Nothing
                gvBalItemDetails.Rows.Clear()
                gvBalItemDetails.Rows.AddNew()
                For Each dr3 As DataRow In dtBomItem.Rows

                    gvBalItemDetails.Rows(gvBalItemDetails.Rows.Count - 1).Cells(colSNO3).Value = gvBalItemDetails.Rows.Count
                    gvBalItemDetails.Rows(gvBalItemDetails.Rows.Count - 1).Cells(colItemCode3).Value = clsCommon.myCstr(dr3("Item_Code"))
                    gvBalItemDetails.Rows(gvBalItemDetails.Rows.Count - 1).Cells(colItemDesc3).Value = clsCommon.myCstr(dr3("Item_Desc")) 'clsItemMaster.GetItemName(clsCommon.myCstr(dr("consm_item_code")), Nothing)
                    gvBalItemDetails.Rows(gvBalItemDetails.Rows.Count - 1).Cells(colUnitCode3).Value = clsCommon.myCstr(dr3("Unit_Code"))
                    gvBalItemDetails.Rows(gvBalItemDetails.Rows.Count - 1).Cells(colItemType3).Value = clsCommon.myCstr(dr3("ITEM_TYPE_NAME"))
                    gvBalItemDetails.Rows(gvBalItemDetails.Rows.Count - 1).Cells(colTotalQtyRequired3).Value = clsCommon.myCdbl(dr3("Reqired_Qty"))
                    Dim RequiredQty As Double = clsCommon.myCdbl(dr3("Reqired_Qty"))
                    Dim StockQty As Double = clsProductionMRP.GetStockQty(clsCommon.myCstr(dr3("Item_Code")), clsCommon.myCstr(dr3("Unit_Code")), fndLocation.Value)
                    Dim PendingPOQty As Double = clsProductionMRP.GetPendingPOkQty(clsCommon.myCstr(dr3("Item_Code")), clsCommon.myCstr(dr3("Unit_Code")), fndLocation.Value)
                    Dim PendingSRNQty As Double = clsProductionMRP.GetPendingSRNkQty(clsCommon.myCstr(dr3("Item_Code")), clsCommon.myCstr(dr3("Unit_Code")), fndLocation.Value)
                    Dim NetRequriedQty As Double = 0
                    If RequiredQty > (StockQty + PendingPOQty + PendingSRNQty) Then
                        NetRequriedQty = RequiredQty - (StockQty + PendingPOQty + PendingSRNQty)
                    End If
                    gvBalItemDetails.Rows(gvBalItemDetails.Rows.Count - 1).Cells(colStockQty3).Value = StockQty
                    gvBalItemDetails.Rows(gvBalItemDetails.Rows.Count - 1).Cells(colPOQty3).Value = PendingPOQty
                    gvBalItemDetails.Rows(gvBalItemDetails.Rows.Count - 1).Cells(colSRN_Qty3).Value = PendingSRNQty
                    gvBalItemDetails.Rows(gvBalItemDetails.Rows.Count - 1).Cells(colNetRequireQty3).Value = NetRequriedQty
                    gvBalItemDetails.Rows.AddNew()
                Next
            End If
            '=============================================================================================

            ' btnGo.Enabled = False
            btnsave.Enabled = True
            TxtitemType.Enabled = False
            'gvMRPDetal.ReadOnly = True
        End If

    End Sub

    
    Private Sub TxtitemType_SelectedIndexChanged(sender As Object, e As UI.Data.PositionChangedEventArgs) Handles TxtitemType.SelectedIndexChanged
        If isFormLoad = False Then
            If clsCommon.myLen(TxtitemType.SelectedValue) > 0 Then
                txtMulProductionPlan.Enabled = False
                dtpFromDate.Enabled = False
                dtpToDate.Enabled = False
            Else
                txtMulProductionPlan.Enabled = True
                dtpFromDate.Enabled = True
                dtpToDate.Enabled = True
            End If
            LoadMRPDetailGrid()
        End If
    End Sub

    Sub OpenICodeList(ByVal isButtonClick As Boolean)
        Dim qry = "  select TSPL_ITEM_MASTER.Item_Code as [ItemCode] , TSPL_ITEM_MASTER.Item_Desc as [Item Desc], TSPL_ITEM_MASTER.HSN_Code as [HSN Code] ,TSPL_ITEM_MASTER.Structure_Code as [Structure Code] ,TSPL_ITEM_MASTER.Item_Type, TSPL_ITEM_MASTER.Product_Type as [Product Type]   from TSPL_ITEM_MASTER "
        Dim whrCls As String = " TSPL_ITEM_MASTER.Active=1 and TSPL_ITEM_MASTER.Item_Type = '" + TxtitemType.SelectedValue + "' "
        gvMRPDetal.CurrentRow.Cells(colItem_Code).Value = clsCommon.myCstr(clsCommon.ShowSelectForm("MRP_Production@fndnder", qry, "ItemCode", whrCls, clsCommon.myCstr(gvMRPDetal.CurrentRow.Cells(colItem_Code).Value), "ItemCode", isButtonClick))
        Dim strItemcode As String = clsCommon.myCstr(gvMRPDetal.CurrentRow.Cells(colItem_Code).Value)
        If clsCommon.myLen(strItemcode) > 0 Then
            gvMRPDetal.CurrentRow.Cells(colLineno).Value = gvMRPDetal.Rows.Count
            gvMRPDetal.CurrentRow.Cells(colSelect).Value = True
            gvMRPDetal.CurrentRow.Cells(colItem_Desc).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select TSPL_ITEM_MASTER.Item_Desc  from TSPL_ITEM_MASTER where TSPL_ITEM_MASTER.Item_Code = '" + strItemcode + "'"))
            gvMRPDetal.CurrentRow.Cells(colItem_Type).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select TSPL_ITEM_TYPE_MASTER.ITEM_TYPE_NAME  from TSPL_ITEM_MASTER left outer join TSPL_ITEM_TYPE_MASTER  on TSPL_ITEM_TYPE_MASTER.ITEM_TYPE_CODE = TSPL_ITEM_MASTER.Item_Type   where TSPL_ITEM_MASTER.Item_Code = '" + strItemcode + "'"))
            gvMRPDetal.CurrentRow.Cells(colUNIT_CODE).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select UOM_Code from TSPL_ITEM_UOM_DETAIL where Stocking_Unit = 'Y'  and Item_Code = '" + strItemcode + "' "))
            gvMRPDetal.CurrentRow.Cells(colPlan_Qty).Value = 0
            gvMRPDetal.CurrentRow.Cells(colBom).Value = clsMRPProductionPODetail.GetBOMOtherItems(strItemcode)
            gvMRPDetal.Rows.AddNew()
        Else
            gvMRPDetal.CurrentRow.Cells(colItem_Desc).Value = ""
            gvMRPDetal.CurrentRow.Cells(colItem_Type).Value = ""
            gvMRPDetal.CurrentRow.Cells(colItem_Type).Value = ""
            gvMRPDetal.CurrentRow.Cells(colPlan_Qty).Value = 0
            gvMRPDetal.CurrentRow.Cells(colBom).Value = ""
        End If
        'btnsave.Enabled = False
        'btnPost.Enabled = False
    End Sub

    Sub OpenBomList(ByVal isButtonClick As Boolean)
        If clsCommon.myLen(clsCommon.myCstr(gvMRPDetal.CurrentRow.Cells(colItem_Code).Value)) <= 0 Then
            gvMRPDetal.CurrentRow.Cells(colBom).Value = ""
            Return
        End If
        Dim qry = "  select  TSPL_PP_BOM_HEAD.BOM_CODE , Convert (varchar,BOM_DATE,103)  as BOM_DATE, STATUS , TSPL_PP_BOM_HEAD.PROD_ITEM_CODE , TSPL_PP_BOM_HEAD.PROD_QUANTITY, TSPL_PP_BOM_HEAD.PROD_ITEM_UNIT_CODE, Convert (varchar, TSPL_PP_BOM_HEAD.Valid_FROM_DATE,103) as Valid_FROM_DATE  , Convert (varchar, TSPL_PP_BOM_HEAD.Valid_UPTO_DATE,103) as Valid_UPTO_DATE from TSPL_PP_BOM_HEAD "
        Dim whrCls As String = "  PROD_ITEM_CODE = '" + gvMRPDetal.CurrentRow.Cells(colItem_Code).Value + "' and  TSPL_PP_BOM_HEAD.Is_Post = 1 and TSPL_PP_BOM_HEAD.STATUS = 'Approved' and  Convert (datetime, GETDATE(),103) > = CONVERT (datetime , TSPL_PP_BOM_HEAD.Valid_FROM_DATE , 103)   and  Convert (datetime ,GETDATE(),103) < = CONVERT (datetime , TSPL_PP_BOM_HEAD.Valid_UPTO_DATE , 103)  "
        gvMRPDetal.CurrentRow.Cells(colBom).Value = clsCommon.myCstr(clsCommon.ShowSelectForm("MRP_Production_BOM@fndnder", qry, "BOM_CODE", whrCls, clsCommon.myCstr(gvMRPDetal.CurrentRow.Cells(colItem_Code).Value), "TSPL_PP_BOM_HEAD.Created_Date,TSPL_PP_BOM_HEAD.BOM_CODE desc", isButtonClick))
        'btnsave.Enabled = False
        'btnPost.Enabled = False
    End Sub

    Sub OpenUomList(ByVal isButtonClick As Boolean)
        If clsCommon.myLen(clsCommon.myCstr(gvMRPDetal.CurrentRow.Cells(colItem_Code).Value)) <= 0 Then
            gvMRPDetal.CurrentRow.Cells(colUNIT_CODE).Value = ""
            Return
        End If
        Dim qry = "  select TSPL_ITEM_UOM_DETAIL.UOM_Code as Code,TSPL_UNIT_MASTER.Unit_Desc as [Description] , TSPL_ITEM_UOM_DETAIL.Conversion_Factor as [Conversion Factor] ,TSPL_ITEM_UOM_DETAIL.Stocking_Unit as [Stocking Unit]   From TSPL_ITEM_UOM_DETAIL left outer join TSPL_UNIT_MASTER on TSPL_ITEM_UOM_DETAIL.Uom_Code =TSPL_UNIT_MASTER.Unit_Code  "
        Dim whrCls As String = "  TSPL_ITEM_UOM_DETAIL.Item_Code = '" + clsCommon.myCstr(gvMRPDetal.CurrentRow.Cells(colItem_Code).Value) + "'  "
        gvMRPDetal.CurrentRow.Cells(colUNIT_CODE).Value = clsCommon.myCstr(clsCommon.ShowSelectForm("MRP_Production_UOM@fndnder", qry, "Code", whrCls, clsCommon.myCstr(gvMRPDetal.CurrentRow.Cells(colItem_Code).Value), "", isButtonClick))
        'btnsave.Enabled = False
        'btnPost.Enabled = False
    End Sub


    Private Sub txtDept__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtDept._MYValidating
        Try
            Dim obj As clsDepartment = clsDepartment.Finder(txtDept.Value, isButtonClicked)
            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Code) > 0 Then
                txtDept.Value = obj.Code
                lblDept.Text = obj.Name
            Else
                txtDept.Value = ""
                lblDept.Text = ""
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub chkAutoIndent_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkAutoIndent.ToggleStateChanged
        If chkAutoIndent.IsChecked = True Then
            pnlDepartment.Visible = True
        Else
            pnlDepartment.Visible = False
        End If
    End Sub

    Private Sub btnReverse_Click(sender As Object, e As EventArgs) Handles btnReverse.Click
        Dim trans As SqlTransaction = Nothing
        Try
            If common.clsCommon.MyMessageBoxShow("Reverse and Unpost the Current Document" + Environment.NewLine + "Are you sure", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                Dim Reason As String = ""
                Dim frm As New FrmFreeTxtBox1
                frm.Text = "Remarks for Reverse"
                frm.ShowDialog()
                If clsCommon.myLen(frm.strRmks) <= 0 Then
                    Exit Sub
                Else
                    Reason = frm.strRmks
                End If
                trans = clsDBFuncationality.GetTransactin()
                If clsProductionMRP.ReverseAndUnpost(txtCode.Value, trans) Then
                    saveCancelLog(Reason, "Reverse And Recreate", trans)
                    trans.Commit()
                    common.clsCommon.MyMessageBoxShow(Me, "Successfully Reversed and Recreated", Me.Text)
                    LoadData(txtCode.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            trans.Rollback()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub gvBalItemDetails_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles gvBalItemDetails.CellDoubleClick
        If e.Column Is gvBalItemDetails.Columns(colPOQty3) Then
            RadPageView1.SelectedPage = pagePendingPO
        ElseIf e.Column Is gvBalItemDetails.Columns(colSRN_Qty3) Then
            RadPageView1.SelectedPage = PagePendingSRN
        ElseIf e.Column Is gvBalItemDetails.Columns(colRequisitionId3) Then
            If clsCommon.myLen(clsCommon.myCstr(gvBalItemDetails.CurrentRow.Cells(colRequisitionId3).Value)) > 0 Then
                clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnPurchaseRequistion, clsCommon.myCstr(gvBalItemDetails.CurrentRow.Cells(colRequisitionId3).Value))
            End If
        End If
    End Sub

    Private Sub gvPendingPO_DoubleClick(sender As Object, e As EventArgs) Handles gvPendingPO.DoubleClick
        If clsCommon.myLen(clsCommon.myCstr(gvPendingPO.CurrentRow.Cells(colPoNo4).Value)) > 0 Then
            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnPurchaseOrder, clsCommon.myCstr(gvPendingPO.CurrentRow.Cells(colPoNo4).Value))
        End If
    End Sub

    Private Sub gvPendingSRN_DoubleClick(sender As Object, e As EventArgs) Handles gvPendingSRN.DoubleClick
        If clsCommon.myLen(clsCommon.myCstr(gvPendingSRN.CurrentRow.Cells(colSrnNo5).Value)) > 0 Then
            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnSRN, clsCommon.myCstr(gvPendingSRN.CurrentRow.Cells(colSrnNo5).Value))
        End If
    End Sub

    Private Sub gvMRPDetal_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles gvMRPDetal.CellDoubleClick
        'If e.Column Is gvMRPDetal.Columns(colBom) Then
        '    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmBillOfMaterialDairy, clsCommon.myCstr(gvMRPDetal.CurrentRow.Cells(colBom).Value))
        'End If
    End Sub

End Class