Imports common
Imports System.Data.SqlClient
Imports System.IO

Public Class frmBatchOrder
    Inherits FrmMainTranScreen
    'Const colLineNo As String = "LineNo"
    '' columns of plan item details grid
    Const colPPCode As String = "PPCode"
    Const colPlanningDate As String = "PlanningDate"
    Const colPlanForDate As String = "PlanForDate"

    Const colProductionLineCode As String = "ProductionLineCode"
    Const colBOMCode As String = "BOMCode" '' bom code including revision no
    ''Const colRevisionNo As String = "RevisionNo"
    Const colItemCode As String = "ItemCode"
    Const colitemDesc As String = "ItemDesc"
    Const colMinQty As String = "MinQty"
    Const colMaxQty As String = "MaxQty"
    Const colRemarks As String = "Remarks"
    Const colBatchQty As String = "BatchQty"
    Const colStartTime As String = "StartTime"
    Const colEndTime As String = "EndTime"
    Const colHours As String = "Hours"
    Const colSpeed As String = "Speed"
    Const colReason As String = "Reason"
    Const colmfgDate As String = "ManufacturingDate"
    Const coLExpDate As String = "expDate"

    '' columns of Raw Material details grid
    Const colRMPPCode As String = "RMPPCode"
    Const colRMProdLineCode As String = "RMProdLineCode"
    Const colRMBOMCode As String = "RMBOMCode"


    Const colRMItemCode As String = "RMItemCode"
    Const colRMUOM As String = "RMUOM"
    Const colRMQty As String = "RMQty"
    Const colRMActualReq As String = "RMActualReq"
    Const colRMIssue As String = "RMIssue"
    Const colRMReturn As String = "RMReturn"
    Const colRMBreakage As String = "RMBreakage"
    Const colRMRejection As String = "RMRejection"
    Const colRMConsumed As String = "RMConsumed"
    Const colFINAL_PROD_Qty_Stock As String = "colFINAL_PROD_Qty_Stock"
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim isNewEntry As Boolean = True
    'Private Obj As clsmo
    Dim obj As New clsBatchOrder
    Private ObjList As New List(Of clsBatchOrder)
    Private ObjList2 As New List(Of clsBatchOrder)
    Private isCellValueChangedOpen As Boolean = False
    Dim OpenFileDialog1 As New OpenFileDialog

    Dim formtype As String = Nothing

    Public Sub New(ByVal formid As String)
        InitializeComponent()
        formtype = formid
    End Sub
    Sub LoadPPItemGridColumns()
        gvPP.Rows.Clear()
        gvPP.Columns.Clear()

        'Dim LineNo As New GridViewTextBoxColumn
        Dim PPCode As New GridViewTextBoxColumn
        Dim PlanningDate As New GridViewDateTimeColumn
        Dim PlanForDate As New GridViewDateTimeColumn

        Dim ProductionLineCode As New GridViewTextBoxColumn
        Dim BOMCode As New GridViewTextBoxColumn
        Dim ItemCode As New GridViewTextBoxColumn
        Dim itemDesc As New GridViewTextBoxColumn
        Dim MinQty As New GridViewDecimalColumn
        Dim MaxQty As New GridViewDecimalColumn
        Dim Remarks As New GridViewTextBoxColumn

        Dim BatchQty As New GridViewDecimalColumn
        Dim StartTime As New GridViewDateTimeColumn
        Dim EndTime As New GridViewDateTimeColumn
        Dim Hours As New GridViewDecimalColumn
        Dim Speed As New GridViewDecimalColumn
        Dim Reason As New GridViewTextBoxColumn
        Dim mfgDate As New GridViewDateTimeColumn
        Dim expDate As New GridViewDateTimeColumn

        PPCode.FormatString = ""
        PPCode.HeaderText = "Plan Code"
        PPCode.Name = colPPCode
        PPCode.Width = 70
        PPCode.ReadOnly = True
        PPCode.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvPP.Columns.Add(PPCode)

        PlanningDate.FormatString = ""
        PlanningDate.HeaderText = "Planning Date"
        PlanningDate.Name = colPlanningDate
        PlanningDate.Width = 70
        PlanningDate.ReadOnly = True
        PlanningDate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvPP.Columns.Add(PlanningDate)

        PlanForDate.FormatString = ""
        PlanForDate.HeaderText = "Plan For Date"
        PlanForDate.Name = colPlanForDate
        PlanForDate.Width = 70
        PlanForDate.ReadOnly = True
        PlanForDate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvPP.Columns.Add(PlanForDate)

        ProductionLineCode.FormatString = ""
        ProductionLineCode.HeaderText = "Production Line"
        ProductionLineCode.Name = colProductionLineCode
        ProductionLineCode.Width = 100
        ProductionLineCode.ReadOnly = True
        ProductionLineCode.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvPP.Columns.Add(ProductionLineCode)

        BOMCode.FormatString = ""
        BOMCode.HeaderText = "BOM Code"
        BOMCode.Name = colBOMCode
        BOMCode.Width = 100
        BOMCode.ReadOnly = True
        BOMCode.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvPP.Columns.Add(BOMCode)


        ItemCode.FormatString = ""
        ItemCode.HeaderText = "Item Code"
        ItemCode.Name = colItemCode
        ItemCode.Width = 100
        ItemCode.ReadOnly = True
        ItemCode.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvPP.Columns.Add(ItemCode)

        itemDesc.FormatString = ""
        itemDesc.HeaderText = "Item Description"
        itemDesc.Name = colitemDesc
        itemDesc.Width = 50
        itemDesc.ReadOnly = True
        itemDesc.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvPP.Columns.Add(itemDesc)

        MinQty.FormatString = ""
        MinQty.HeaderText = "Min Qty"
        MinQty.Name = colMinQty
        MinQty.Width = 50
        MinQty.ReadOnly = True
        MinQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvPP.Columns.Add(MinQty)

        MaxQty.FormatString = ""
        MaxQty.HeaderText = "Max Qty"
        MaxQty.Name = colMaxQty
        MaxQty.Width = 50
        MaxQty.ReadOnly = True
        MaxQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvPP.Columns.Add(MaxQty)

        Remarks.FormatString = ""
        Remarks.HeaderText = "Remarks"
        Remarks.Name = colRemarks
        Remarks.Width = 50
        Remarks.ReadOnly = True
        Remarks.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvPP.Columns.Add(Remarks)

        BatchQty.FormatString = ""
        BatchQty.HeaderText = "Batch Qty"
        BatchQty.Name = colBatchQty
        BatchQty.Width = 130
        BatchQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvPP.Columns.Add(BatchQty)

        StartTime.Format = DateTimePickerFormat.Time
        StartTime.HeaderText = "Start Time"
        StartTime.Name = colStartTime
        StartTime.CustomFormat = "hh:mm tt"
        StartTime.FormatString = "{0:hh:mm tt}"
        StartTime.Width = 130
        StartTime.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvPP.Columns.Add(StartTime)

        EndTime.Format = DateTimePickerFormat.Time
        EndTime.HeaderText = "End Time"
        EndTime.CustomFormat = "hh:mm tt"
        EndTime.FormatString = "{0:hh:mm tt}"
        EndTime.Name = colEndTime
        EndTime.Width = 130
        EndTime.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvPP.Columns.Add(EndTime)

        Hours.FormatString = ""
        Hours.HeaderText = "Hours"
        Hours.Name = colHours
        Hours.Width = 130
        Hours.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvPP.Columns.Add(Hours)

        Speed.FormatString = ""
        Speed.HeaderText = "Speed"
        Speed.Name = colSpeed
        Speed.Width = 130
        Speed.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvPP.Columns.Add(Speed)

        Reason.FormatString = ""
        Reason.HeaderText = "Reason"
        Reason.Name = colReason
        Reason.Width = 130
        Reason.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvPP.Columns.Add(Reason)

        mfgDate.FormatString = ""
        mfgDate.HeaderText = "MFG Date"
        mfgDate.Name = colmfgDate
        mfgDate.Width = 130
        mfgDate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvPP.Columns.Add(mfgDate)

        expDate.FormatString = ""
        expDate.HeaderText = "Expiry Date"
        expDate.Name = coLExpDate
        expDate.Width = 130
        expDate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvPP.Columns.Add(expDate)

    End Sub

    Sub LoadRMGridColumns()
        gvRM.Rows.Clear()
        gvRM.Columns.Clear()

        Dim RMPPCode As New GridViewTextBoxColumn
        Dim RMProdLineCode As New GridViewTextBoxColumn
        Dim RMBOMCode As New GridViewTextBoxColumn

        Dim RMItemCode As New GridViewTextBoxColumn
        Dim RMUOM As New GridViewTextBoxColumn
        Dim RMQty As New GridViewDecimalColumn
        Dim RMActualReq As New GridViewDecimalColumn
        Dim RMIssue As New GridViewDecimalColumn
        Dim RMReturn As New GridViewDecimalColumn
        Dim RMBreakage As New GridViewDecimalColumn
        Dim RMRejection As New GridViewDecimalColumn
        Dim RMConsumed As New GridViewDecimalColumn


        ''''
        RMPPCode.FormatString = ""
        RMPPCode.HeaderText = "PP Code"
        RMPPCode.Name = colRMPPCode
        RMPPCode.Width = 70
        RMPPCode.ReadOnly = True
        RMPPCode.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvRM.Columns.Add(RMPPCode)

        RMProdLineCode.FormatString = ""
        RMProdLineCode.HeaderText = "Prod. Line Code"
        RMProdLineCode.Name = colRMProdLineCode
        RMProdLineCode.Width = 70
        RMProdLineCode.ReadOnly = True
        RMProdLineCode.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvRM.Columns.Add(RMProdLineCode)

        RMBOMCode.FormatString = ""
        RMBOMCode.HeaderText = "BOM Code"
        RMBOMCode.Name = colRMBOMCode
        RMBOMCode.Width = 70
        RMBOMCode.ReadOnly = True
        RMBOMCode.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvRM.Columns.Add(RMBOMCode)
        ''''
        RMItemCode.FormatString = ""
        RMItemCode.HeaderText = "Item Code"
        RMItemCode.Name = colRMItemCode
        RMItemCode.Width = 70
        RMItemCode.ReadOnly = True
        RMItemCode.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvRM.Columns.Add(RMItemCode)

        RMUOM.FormatString = ""
        RMUOM.HeaderText = "UOM"
        RMUOM.Name = colRMUOM
        RMUOM.Width = 70
        RMUOM.ReadOnly = True
        RMUOM.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvRM.Columns.Add(RMUOM)

        RMQty.FormatString = ""
        RMQty.HeaderText = "Quantity"
        RMQty.Name = colRMQty
        RMQty.Width = 70
        RMQty.ReadOnly = True
        RMQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvRM.Columns.Add(RMQty)

        RMActualReq.FormatString = ""
        RMActualReq.HeaderText = "Actual Required"
        RMActualReq.Name = colRMActualReq
        RMActualReq.Width = 100
        RMActualReq.ReadOnly = True
        RMActualReq.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvRM.Columns.Add(RMActualReq)

        RMIssue.FormatString = ""
        RMIssue.HeaderText = "Issue"
        RMIssue.Name = colRMIssue
        RMIssue.Width = 100
        RMIssue.ReadOnly = True
        RMIssue.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvRM.Columns.Add(RMIssue)


        RMReturn.FormatString = ""
        RMReturn.HeaderText = "Return"
        RMReturn.Name = colRMReturn
        RMReturn.Width = 100
        RMReturn.ReadOnly = True
        RMReturn.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvRM.Columns.Add(RMReturn)

        RMBreakage.FormatString = ""
        RMBreakage.HeaderText = "Breakage"
        RMBreakage.Name = colRMBreakage
        RMBreakage.Width = 100
        RMBreakage.ReadOnly = True
        RMBreakage.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvRM.Columns.Add(RMBreakage)

        RMRejection.FormatString = ""
        RMRejection.HeaderText = "Rejection"
        RMRejection.Name = colRMRejection
        RMRejection.Width = 100
        RMBreakage.ReadOnly = True
        RMRejection.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvRM.Columns.Add(RMRejection)

        RMConsumed.FormatString = ""
        RMConsumed.HeaderText = "Consumed"
        RMConsumed.Name = colRMConsumed
        RMConsumed.Width = 100
        RMConsumed.ReadOnly = True
        RMConsumed.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvRM.Columns.Add(RMConsumed)

        Dim repoFinalProdQtyStock As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoFinalProdQtyStock.FormatString = ""
        repoFinalProdQtyStock.Name = colFINAL_PROD_Qty_Stock
        repoFinalProdQtyStock.Width = 100
        repoFinalProdQtyStock.HeaderText = "Stock Qty"
        repoFinalProdQtyStock.FormatString = "{0:n3}"
        repoFinalProdQtyStock.DecimalPlaces = 3
        repoFinalProdQtyStock.ReadOnly = True
        gvRM.MasterTemplate.Columns.Add(repoFinalProdQtyStock)
    End Sub


    Private Sub frmBatchOrder_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnNew.Enabled Then
            funReset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnsave.Enabled Then
            Save()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btndelete.Enabled Then
            DeleteData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            funClose()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            funReset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso btnPost.Enabled Then
            PostData()
        End If
    End Sub
    Sub funClose()
        Me.Close()
    End Sub

    Private Sub frmBatchOrder_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim coll As Dictionary(Of String, String)
        coll = New Dictionary(Of String, String)()
        coll.Add("Location_Code", "Varchar(12)  NULL REFERENCES TSPL_LOCATION_MASTER(Location_Code)")
        clsCommonFunctionality.CreateOrAlterTable(True, False, "TSPL_MF_BATCH_ORDER", coll, Nothing, False, False)

        '' TSPL_MF_BATCH_PP_DETAIL


        SetUserMgmtNew()
        'LoadPPList()
        LoadPPItemGridColumns()
        LoadRMGridColumns()
        isNewEntry = True
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(btnPost, "Press Alt+P for  Post")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D  for Delete ")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnNew, "Press Alt+N Adding New ")
        '  ButtonToolTip.SetToolTip(btnPrint, "Press Alt+R for Print Preview")
        funReset()
        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
    End Sub
    Sub LoadPPList()
        Dim STRQ As String = ""
        If isNewEntry = True Then
            STRQ += " select DISTINCT  T1.PROD_PLAN_CODE,T1.DESCRIPTION,convert(date,T1.PLANNING_DATE,103) as PLANNING_DATE,T1.PLAN_FOR_DATE "
            STRQ += " FROM TSPL_MF_PRODUCTION_PLAN_HEAD T1 LEFT JOIN  "
            STRQ += " (SELECT T11.BO_CODE,T12.PROD_PLAN_CODE FROM TSPL_MF_BATCH_ORDER T11  "
            STRQ += " INNER JOIN TSPL_MF_BATCH_PP_DETAIL T12 ON T11.BO_CODE=T12.BO_CODE ) AS T2 ON T1.PROD_PLAN_CODE=T2.PROD_PLAN_CODE "
            STRQ += " WHERE T2.BO_CODE IS NULL AND T1.POSTED=1 order by convert(date,T1.PLANNING_DATE,103) desc, T1.PROD_PLAN_CODE desc "
        ElseIf isNewEntry = False And UsLock1.Status = ERPTransactionStatus.Pending Then
            STRQ += " Select * From ( select DISTINCT T1.PROD_PLAN_CODE,T1.DESCRIPTION,convert(date,T1.PLANNING_DATE,103) as PLANNING_DATE,T1.PLAN_FOR_DATE "
            STRQ += " FROM TSPL_MF_PRODUCTION_PLAN_HEAD T1 LEFT JOIN  "
            STRQ += " (SELECT T11.BO_CODE,T12.PROD_PLAN_CODE FROM TSPL_MF_BATCH_ORDER T11  "
            STRQ += " INNER JOIN TSPL_MF_BATCH_PP_DETAIL T12 ON T11.BO_CODE=T12.BO_CODE ) AS T2 ON T1.PROD_PLAN_CODE=T2.PROD_PLAN_CODE "
            STRQ += " WHERE T2.BO_CODE IS NULL AND T1.POSTED=1"
            STRQ += " UNION  "
            STRQ += " select T1.PROD_PLAN_CODE,T1.DESCRIPTION,T1.PLANNING_DATE,T1.PLAN_FOR_DATE "
            STRQ += " FROM TSPL_MF_PRODUCTION_PLAN_HEAD T1 LEFT JOIN  "
            STRQ += " (SELECT T11.BO_CODE,T12.PROD_PLAN_CODE FROM TSPL_MF_BATCH_ORDER T11  "
            STRQ += " INNER JOIN TSPL_MF_BATCH_PP_DETAIL T12 ON T11.BO_CODE=T12.BO_CODE WHERE T11.BO_CODE='" & Me.txtCode.Value & "') AS T2 ON T1.PROD_PLAN_CODE=T2.PROD_PLAN_CODE "
            STRQ += " WHERE T2.BO_CODE IS NOT NULL ) XXXFinal order by convert(date,PLANNING_DATE,103) desc, PROD_PLAN_CODE desc   "
        Else
            STRQ += " select DISTINCT T1.PROD_PLAN_CODE,T1.DESCRIPTION,convert(date, T1.PLANNING_DATE,103) as PLANNING_DATE,T1.PLAN_FOR_DATE "
            STRQ += " FROM TSPL_MF_PRODUCTION_PLAN_HEAD T1 LEFT JOIN  "
            STRQ += " (SELECT T11.BO_CODE,T12.PROD_PLAN_CODE FROM TSPL_MF_BATCH_ORDER T11  "
            STRQ += " INNER JOIN TSPL_MF_BATCH_PP_DETAIL T12 ON T11.BO_CODE=T12.BOM_CODE WHERE T11.BO_CODE='" & Me.txtCode.Value & "') AS T2 ON T1.PROD_PLAN_CODE=T2.PROD_PLAN_CODE "
            STRQ += " WHERE T2.BO_CODE IS NOT NULL order by convert(date,T1.PLANNING_DATE,103) desc, T1.PROD_PLAN_CODE desc "
        End If
       
        Dim dt As DataTable
        dt = clsDBFuncationality.GetDataTable(STRQ)
        cbgPP.DataSource = dt
        cbgPP.ValueMember = "PROD_PLAN_CODE"
        cbgPP.DisplayMember = "DESCRIPTION"


    End Sub
    Private Sub SetUserMgmtNew()
        If formtype = clsUserMgtCode.frmBatchOrderSTD Then
            ''MyBase.SetUserMgmt(clsUserMgtCode.frmBatchOrderSTD)
        ElseIf formtype = clsUserMgtCode.frmBatchOrderPepsi Then
            ''MyBase.SetUserMgmt(clsUserMgtCode.frmBatchOrderPepsi)
        End If
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnsave.Visible = MyBase.isModifyFlag
        btnPost.Visible = MyBase.isPostFlag
        btndelete.Visible = MyBase.isDeleteFlag
        btnPrint.Visible = MyBase.isPrintFlag
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        funClose()
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Try
            funReset()
        Catch ex As Exception
        End Try
    End Sub

    Sub funReset()
        isNewEntry = True
        txtCode.MyReadOnly = False
        txtCode.Value = Nothing
        txtCode.Focus()
        UsLock1.Status = ERPTransactionStatus.Pending
        Me.txtDescription.Text = ""
        txtManualBatchNo.Text = ""
        Me.dtpBODate.Value = Today
        'Me.dtpPlanForDate.Value = Today

        btnsave.Text = "Save"
        btnsave.Enabled = True
        btndelete.Enabled = True
        btnPost.Enabled = True
        Me.gvPP.Rows.Clear()
        gvRM.Rows.Clear()
        fndLocation.Value = ""
        lblLocationDesc.Text = ""
        'Me.gvPP.Rows.AddNew()
        Me.dtpBODate.Value = clsCommon.GETSERVERDATE
        Me.lblCreatedByName.Text = objCommonVar.CurrentUserCode
        LoadPPList()
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
        obj = clsBatchOrder.GetData(strCode, NavTyep)
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.BO_CODE) > 0) Then

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
            LoadPPItemGridColumns()
            txtCode.Value = obj.BO_CODE
            Me.txtDescription.Text = clsCommon.myCstr(obj.DESCRIPTION)
            Me.dtpBODate.Value = obj.BO_DATE
            fndLocation.Value = obj.locationcode
            lblLocationDesc.Text = obj.locationname
            txtManualBatchNo.Text = obj.Manual_Batch_No
            Me.lblCreatedByName.Text = obj.CREATED_BY
            If obj.POSTED = True Then
                Me.lblApprovedByName.Text = obj.APPROVED_BY
            Else
                Me.lblApprovedByName.Text = ""
            End If
            LoadPPList()

            If (clsBatchOrder.ObjList IsNot Nothing AndAlso clsBatchOrder.ObjList.Count > 0) Then
                For Each obj As clsBatchOrder In clsBatchOrder.ObjList
                    gvPP.Rows.AddNew()
                    gvPP.Rows(gvPP.Rows.Count - 1).Cells(colPPCode).Value = obj.PROD_PLAN_CODE
                    gvPP.Rows(gvPP.Rows.Count - 1).Cells(colPlanningDate).Value = obj.PLANNING_DATE
                    gvPP.Rows(gvPP.Rows.Count - 1).Cells(colPlanForDate).Value = obj.PLAN_FOR_DATE
                    gvPP.Rows(gvPP.Rows.Count - 1).Cells(colProductionLineCode).Value = obj.PRODUCTION_LINE_CODE
                    gvPP.Rows(gvPP.Rows.Count - 1).Cells(colBOMCode).Value = obj.BOM_CODE
                    gvPP.Rows(gvPP.Rows.Count - 1).Cells(colBOMCode).Tag = obj.BOM_REVISION_NO
                    gvPP.Rows(gvPP.Rows.Count - 1).Cells(colItemCode).Tag = obj.BOM_CODE
                    gvPP.Rows(gvPP.Rows.Count - 1).Cells(colItemCode).Value = obj.ITEM_CODE
                    gvPP.Rows(gvPP.Rows.Count - 1).Cells(colitemDesc).Value = obj.ITEM_DESCRIPTION

                    gvPP.Rows(gvPP.Rows.Count - 1).Cells(colMinQty).Value = obj.MIN_QUANTITY
                    gvPP.Rows(gvPP.Rows.Count - 1).Cells(colMaxQty).Value = obj.MAX_QUANTITY
                    gvPP.Rows(gvPP.Rows.Count - 1).Cells(colMaxQty).Tag = obj.UNIT_CODE
                    gvPP.Rows(gvPP.Rows.Count - 1).Cells(colRemarks).Value = obj.REMARKS

                    gvPP.Rows(gvPP.Rows.Count - 1).Cells(colBatchQty).Value = obj.BATCH_QTY
                    gvPP.Rows(gvPP.Rows.Count - 1).Cells(colStartTime).Value = obj.START_TIME
                    gvPP.Rows(gvPP.Rows.Count - 1).Cells(colEndTime).Value = obj.END_TIME
                    gvPP.Rows(gvPP.Rows.Count - 1).Cells(colHours).Value = obj.HOURS
                    gvPP.Rows(gvPP.Rows.Count - 1).Cells(colSpeed).Value = obj.SPEED
                    gvPP.Rows(gvPP.Rows.Count - 1).Cells(colReason).Value = obj.REASON
                    gvPP.Rows(gvPP.Rows.Count - 1).Cells(colmfgDate).Value = obj.MF_DATE
                    gvPP.Rows(gvPP.Rows.Count - 1).Cells(coLExpDate).Value = obj.EXP_DATE

                Next
            Else
                gvPP.Rows.AddNew()
            End If
        End If
        fillRM()


    End Sub



    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        SavingData(False)
    End Sub
    Public Function Save() As Boolean
        Try


            If AllowToSave() Then
                Dim obj As New clsBatchOrder
                obj.BO_CODE = Me.txtCode.Value
                obj.DESCRIPTION = Me.txtDescription.Text
                obj.BO_DATE = Me.dtpBODate.Value
                obj.locationcode = fndLocation.Value
                obj.Manual_Batch_No = txtManualBatchNo.Text
                obj.CREATED_BY = Me.lblCreatedBy.Text
                'obj.APPROVED_BY = Me.lblApprovedBy.Text

                Dim obj1 As clsBatchOrder
                ObjList = New List(Of clsBatchOrder)
                For Each grow As GridViewRowInfo In gvPP.Rows
                    If clsCommon.myLen(clsCommon.myCstr(grow.Cells(colItemCode).Tag)) > 0 Then
                        obj1 = New clsBatchOrder()

                        obj1.BO_CODE = txtCode.Value
                        'If clsCommon.myLen(clsCommon.myCstr(grow.Cells(colProductionLineCode).Value)) > 0 Then
                        obj1.PRODUCTION_LINE_CODE = clsCommon.myCstr(grow.Cells(colProductionLineCode).Value)
                        'End If
                        obj1.PROD_PLAN_CODE = clsCommon.myCstr(grow.Cells(colPPCode).Value)
                        obj1.BOM_CODE = clsCommon.myCstr(grow.Cells(colItemCode).Tag)
                        obj1.BOM_REVISION_NO = clsCommon.myCstr(grow.Cells(colBOMCode).Tag)
                        obj1.ITEM_CODE = clsCommon.myCstr(grow.Cells(colItemCode).Value)
                        obj1.ITEM_DESCRIPTION = clsCommon.myCstr(grow.Cells(colitemDesc).Value)
                        obj1.MIN_QUANTITY = clsCommon.myCdbl(grow.Cells(colMinQty).Value)
                        obj1.MAX_QUANTITY = clsCommon.myCdbl(grow.Cells(colMaxQty).Value)
                        obj1.UNIT_CODE = clsCommon.myCstr(grow.Cells(colMaxQty).Tag)
                        obj1.REMARKS = clsCommon.myCdbl(grow.Cells(colRemarks).Value)

                        obj1.BATCH_QTY = clsCommon.myCdbl(grow.Cells(colBatchQty).Value)
                        If clsCommon.myLen(grow.Cells(colStartTime).Value) > 0 Then
                            obj1.START_TIME = clsCommon.myCDate(grow.Cells(colStartTime).Value)
                        Else
                            obj1.START_TIME = Nothing
                        End If

                        If clsCommon.myLen(grow.Cells(colEndTime).Value) > 0 Then
                            obj1.END_TIME = clsCommon.myCDate(grow.Cells(colEndTime).Value)
                        Else
                            obj1.END_TIME = Nothing
                        End If


                        obj1.HOURS = clsCommon.myCdbl(grow.Cells(colHours).Value)
                        obj1.SPEED = clsCommon.myCdbl(grow.Cells(colSpeed).Value)
                        obj1.REASON = clsCommon.myCstr(grow.Cells(colReason).Value)
                        obj1.MF_DATE = clsCommon.myCDate(grow.Cells(colmfgDate).Value)
                        If clsCommon.myLen(grow.Cells(coLExpDate).Value) > 0 Then
                            obj1.EXP_DATE = clsCommon.myCDate(grow.Cells(coLExpDate).Value)
                        Else
                            obj1.EXP_DATE = Nothing
                        End If

                        ObjList.Add(obj1)
                    End If
                Next

                '' saving raw material
                ObjList2 = New List(Of clsBatchOrder)
                For Each grow As GridViewRowInfo In gvRM.Rows
                    If clsCommon.myLen(clsCommon.myCstr(grow.Cells(colRMItemCode).Value)) > 0 Then
                        obj1 = New clsBatchOrder()

                        obj1.BO_CODE = txtCode.Value
                        obj1.PROD_PLAN_CODE = clsCommon.myCstr(grow.Cells(colRMPPCode).Value)
                        obj1.PRODUCTION_LINE_CODE = clsCommon.myCstr(grow.Cells(colRMProdLineCode).Value)
                        obj1.BOM_CODE = clsCommon.myCstr(grow.Cells(colRMBOMCode).Value)

                        obj1.RM_ITEM_CODE = clsCommon.myCstr(grow.Cells(colRMItemCode).Value)
                        obj1.RM_UNIT_CODE = clsCommon.myCstr(grow.Cells(colRMUOM).Value)
                        obj1.RM_QTY = clsCommon.myCstr(grow.Cells(colRMQty).Value)
                        obj1.RM_ACTUAL_REQ_QTY = clsCommon.myCdbl(grow.Cells(colRMActualReq).Tag)
                        obj1.ISSUE_QTY = clsCommon.myCstr(grow.Cells(colRMIssue).Value)
                        obj1.RETURN_QTY = clsCommon.myCstr(grow.Cells(colRMReturn).Value)
                        obj1.BREAKAGE_QTY = clsCommon.myCdbl(grow.Cells(colRMBreakage).Value)
                        obj1.REJ_QTY = clsCommon.myCdbl(grow.Cells(colRMRejection).Value)
                        obj1.CONSM_QTY = clsCommon.myCstr(grow.Cells(colRMConsumed).Value)

                        ObjList2.Add(obj1)
                    End If
                Next

                Dim issaved As Boolean = False
                issaved = obj.SaveData(obj, ObjList, ObjList2, isNewEntry, clsCommon.myCstr(txtCode.Value))

                If issaved = True Then
                    'clsCommon.MyMessageBoxShow("Document Save Successfully.")
                    LoadData(obj.BO_CODE, NavigatorType.Current)
                    Return True
                End If

                'Return False
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)

        End Try
        Return False
    End Function
    Function AllowToSave() As Boolean
        '===================Added by preeti Gupta==============
        If AllowFutureDateTransaction(dtpBODate.Value, Nothing) = False Then
            dtpBODate.Select()
            Return False
        End If
        '===========================================================
        If btnsave.Text = "Update" Then
            Dim QryStr As String = "select POSTED from TSPL_MF_BATCH_ORDER where BO_CODE = '" + txtCode.Value + "' "
            Dim chkpost As String = clsDBFuncationality.getSingleValue(QryStr)
            If chkpost = "1" Then
                clsCommon.MyMessageBoxShow("Transection already posted")
                Return False
            End If
        End If
        If clsCommon.myLen(fndLocation.Value) <= 0 Then
            clsCommon.MyMessageBoxShow("Please select Location First.")
            Return False
        End If
        'If clsCommon.myLen(txtCode.Value) <= 0 Then
        '    myMessages.blankValue("Code")
        '    txtCode.Focus()
        '    Return False
        'End If

        'If clsCommon.myLen(txtCode.Value) <= 0 Then
        '    myMessages.blankValue("Batch Order Code")
        '    txtCode.Focus()
        '    Return False
        'End If

        Dim ii As Int16 = 0
        If Me.gvPP.Rows.Count = 0 Or gvRM.Rows.Count = 0 Then
            clsCommon.MyMessageBoxShow("Row Material Not Found. Please Check End Date of BOM should be greater than or equal to Batch order Date")
            Return False
        End If
        Dim IndustryType As String = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.INDUSTRYTYPE, clsFixedParameterCode.INDUSTRYTYPE, Nothing))
        If clsCommon.CompairString(IndustryType, "A") <> CompairStringResult.Equal Then
            For Each grow As GridViewRowInfo In gvPP.Rows
                If clsCommon.myCdbl(grow.Cells(colBatchQty).Value > clsCommon.myCdbl(grow.Cells(colMaxQty).Value)) Or clsCommon.myCdbl(grow.Cells(colBatchQty).Value < clsCommon.myCdbl(grow.Cells(colMinQty).Value)) Then
                    clsCommon.MyMessageBoxShow("Batch Quantity must be between Minimum and Maximum of Production Plan in Line NO- " & grow.Index + 1 & "!")
                    Return False
                Else
                    ii += 1
                End If

            Next
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
                If (clsBatchOrder.DeleteData(txtCode.Value)) Then
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


    Private Sub txtCode__MYNavigator1(ByVal sender As Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtCode._MYNavigator
        Try
            LoadData(txtCode.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub txtCode__MYValidating1(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtCode._MYValidating

        Dim str As String = "select count(*) from TSPL_MF_BATCH_ORDER where BO_CODE ='" + txtCode.Value + "' "
        Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
        If no = 0 AndAlso isButtonClicked = False Then
            txtCode.MyReadOnly = False
            'txtCode.Value = ""
            '' common.clsCommon.MyMessageBoxShow("Value doesn't exist ")
        Else
            txtCode.MyReadOnly = True
        End If
        If txtCode.MyReadOnly OrElse isButtonClicked Then

            Dim qry As String = "SELECT T1.BO_CODE AS Code,T1.DESCRIPTION,T1.BO_DATE, "
            qry += " T1.MODIFIED_BY AS APPROVED_BY,T1.Created_By,T1.POSTED,T1.POSTING_DATE FROM TSPL_MF_BATCH_ORDER AS T1"

            txtCode.Value = clsCommon.ShowSelectForm("TSPL_MF_BATCH_ORDER", qry, "Code", "", txtCode.Value, " convert(date,BO_DATE,103) desc, Code desc", isButtonClicked)
            If txtCode.Value <> "" Then
                LoadData(txtCode.Value, NavigatorType.Current)
            Else
                funReset()
            End If
        End If


    End Sub

    Private Sub btnPost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPost.Click
        PostData()
    End Sub

    Sub PostData()
        Try
            If (myMessages.postConfirm()) Then
                SavingData(True)
                If (clsBatchOrder.PostData(txtCode.Value, True)) Then
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


    Private Sub MenuItemExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        clsCommon.MyMessageBoxShow("Option under Development !")
        'Dim str As String
        'str = "select DOCUMENT_CODE AS Code, DOCUMENT_NAME as [Document Name], DESCRIPTION AS Description from TSPL_DOCUMENT_MASTER"
        'transportSql.ExporttoExcel(str, Me)

    End Sub

    Private Sub MenuItemClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        funClose()
    End Sub



    Private Sub gvBOM_CellEndEdit(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvPP.CellEndEdit
        If gvPP.CurrentRow Is Nothing Then
            Exit Sub
        End If

        fillRM()

        '    'If gvPP.CurrentRow.Cells(0).Value = "" Then
        '    '    gvPP.CurrentRow.Cells(0).Value = gvPP.RowCount
        '    'End If

        If Not isCellValueChangedOpen Then
            isCellValueChangedOpen = True
            If e.Column Is gvPP.Columns(colStartTime) Or e.Column Is gvPP.Columns(colEndTime) Then
                Try
                    If clsCommon.myLen(gvPP.CurrentRow.Cells(colStartTime).Value) = 0 Or clsCommon.myLen(gvPP.CurrentRow.Cells(colEndTime).Value) = 0 Then
                        gvPP.CurrentRow.Cells(colHours).Value = 0
                    Else
                        gvPP.CurrentRow.Cells(colHours).Value = DateDiff(DateInterval.Hour, gvPP.CurrentRow.Cells(colStartTime).Value, gvPP.CurrentRow.Cells(colEndTime).Value) + 1
                    End If

                Catch ex As Exception

                End Try

            End If


            isCellValueChangedOpen = False
        End If
    End Sub






    Private Sub gvBOM_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles gvPP.KeyDown
        If e.KeyData = Keys.Enter Then
            Me.gvPP.Rows.Add(1)

            gvPP.Rows(gvPP.RowCount - 1).Cells(0).Value = gvPP.RowCount
        End If
        If e.KeyData = Keys.Right And gvPP.CurrentCell.ColumnIndex = gvPP.Columns.Count - 1 Then
            Me.gvPP.Rows.Add(1)
            gvPP.Rows(gvPP.RowCount - 1).Cells(0).Value = gvPP.RowCount
        End If
    End Sub



    Sub fillPPItems(ByVal pp As String)
        Dim strq As String
        Try
            gvPP.ReadOnly = True
            gvRM.ReadOnly = True
            strq = ""
            If isNewEntry = True Then
                strq += "SELECT T1.PROD_PLAN_CODE,T1.PLANNING_DATE,T1.PLAN_FOR_DATE,T2.PRODUCTION_LINE_CODE, "
                strq += "(T2.BOM_CODE + '(' + T2.BOM_REVISION_NO + ')') AS BOM_CODE,T2.BOM_REVISION_NO,T2.BOM_CODE AS BOM_CODE_MAIN,T2.ITEM_CODE,T2.ITEM_DESCRIPTION,T2.MIN_QUANTITY, "
                strq += "T2.MAX_QUANTITY,T2.UNIT_CODE,T2.REMARKS,T3.tech_shelf_life FROM TSPL_MF_PRODUCTION_PLAN_HEAD T1 "
                strq += "INNER JOIN TSPL_MF_PROD_PLAN_DETAIL T2 ON T1.PROD_PLAN_CODE=T2.PROD_PLAN_CODE LEFT JOIN TSPL_ITEM_MASTER T3 ON T2.ITEM_CODE=T3.ITEM_CODE WHERE T1.PROD_PLAN_CODE IN (" & pp & ")"
            ElseIf isNewEntry = False And UsLock1.Status = ERPTransactionStatus.Pending Then
 
                strq += "SELECT T1.PROD_PLAN_CODE,T1.PLANNING_DATE,T1.PLAN_FOR_DATE,T2.PRODUCTION_LINE_CODE, "
                strq += "(T2.BOM_CODE + '(' + T2.BOM_REVISION_NO + ')') AS BOM_CODE,T2.BOM_REVISION_NO,T2.BOM_CODE AS BOM_CODE_MAIN,T2.ITEM_CODE,T2.ITEM_DESCRIPTION,T2.MIN_QUANTITY, "
                strq += "T2.MAX_QUANTITY,T2.UNIT_CODE,T2.REMARKS,T3.tech_shelf_life FROM TSPL_MF_PRODUCTION_PLAN_HEAD T1 "
                strq += "INNER JOIN TSPL_MF_PROD_PLAN_DETAIL T2 ON T1.PROD_PLAN_CODE=T2.PROD_PLAN_CODE LEFT JOIN TSPL_ITEM_MASTER T3 ON T2.ITEM_CODE=T3.ITEM_CODE WHERE T1.PROD_PLAN_CODE IN (" & pp & ")"
            Else
                Exit Sub
            End If
            
            Dim dtPP As DataTable
            dtPP = clsDBFuncationality.GetDataTable(strq)
            Me.gvPP.Rows.Clear()
            Me.gvRM.Rows.Clear()
            Me.gvPP.Tag = pp
            For intLoop As Integer = 0 To dtPP.Rows.Count - 1
                gvPP.Rows.AddNew()

                gvPP.Rows(gvPP.Rows.Count - 1).Cells(colPPCode).Value = dtPP.Rows(intLoop).Item("PROD_PLAN_CODE")
                gvPP.Rows(gvPP.Rows.Count - 1).Cells(colPlanningDate).Value = dtPP.Rows(intLoop).Item("PLANNING_DATE")
                gvPP.Rows(gvPP.Rows.Count - 1).Cells(colPlanForDate).Value = dtPP.Rows(intLoop).Item("PLAN_FOR_DATE")
                gvPP.Rows(gvPP.Rows.Count - 1).Cells(colProductionLineCode).Value = dtPP.Rows(intLoop).Item("PRODUCTION_LINE_CODE")
                gvPP.Rows(gvPP.Rows.Count - 1).Cells(colBOMCode).Value = dtPP.Rows(intLoop).Item("BOM_CODE")
                gvPP.Rows(gvPP.Rows.Count - 1).Cells(colBOMCode).Tag = dtPP.Rows(intLoop).Item("BOM_REVISION_NO")
                gvPP.Rows(gvPP.Rows.Count - 1).Cells(colItemCode).Tag = dtPP.Rows(intLoop).Item("BOM_CODE_MAIN")
                gvPP.Rows(gvPP.Rows.Count - 1).Cells(colItemCode).Value = dtPP.Rows(intLoop).Item("ITEM_CODE")
                gvPP.Rows(gvPP.Rows.Count - 1).Cells(colitemDesc).Value = clsCommon.myCstr(dtPP.Rows(intLoop).Item("ITEM_DESCRIPTION"))
                gvPP.Rows(gvPP.Rows.Count - 1).Cells(colMinQty).Value = clsCommon.myCdbl(dtPP.Rows(intLoop).Item("MIN_QUANTITY"))
                gvPP.Rows(gvPP.Rows.Count - 1).Cells(colMaxQty).Value = clsCommon.myCdbl(dtPP.Rows(intLoop).Item("MAX_QUANTITY"))
                gvPP.Rows(gvPP.Rows.Count - 1).Cells(colMaxQty).Tag = clsCommon.myCstr(dtPP.Rows(intLoop).Item("UNIT_CODE"))
                gvPP.Rows(gvPP.Rows.Count - 1).Cells(colRemarks).Value = clsCommon.myCstr(dtPP.Rows(intLoop).Item("REMARKS"))
                gvPP.Rows(gvPP.Rows.Count - 1).Cells(colmfgDate).Value = Me.dtpBODate.Value
                gvPP.Rows(gvPP.Rows.Count - 1).Cells(coLExpDate).Value = Me.dtpBODate.Value.AddDays(clsCommon.myCdbl(dtPP.Rows(intLoop).Item("tech_shelf_life")))

            Next
            gvPP.ReadOnly = False
            gvRM.ReadOnly = False
        Catch ex As Exception
            gvPP.ReadOnly = False
            gvRM.ReadOnly = False
        End Try

    End Sub

    Sub fillRM()
        Dim strq As String = ""
        Dim strqFinal As String = ""
        Try
            'If isNewEntry = True Then
            For Each row As GridViewRowInfo In gvPP.Rows
                If row.Index = gvPP.Rows.Count - 1 Then
                    strq += "SELECT '" & clsCommon.myCstr(row.Cells(colPPCode).Value) & "' AS PROD_PLAN_CODE,'" & clsCommon.myCstr(row.Cells(colProductionLineCode).Value) & "' AS PRODUCTION_LINE_CODE,'" & clsCommon.myCstr(row.Cells(colItemCode).Tag) & "' AS BOM_CODE,CONSM_ITEM_CODE AS ITEM_CODE,(CONSM_QUANTITY/(case when PROD_QUANTITY=0 then 1 else PROD_QUANTITY end))* " & clsCommon.myCdbl(row.Cells(colBatchQty).Value) & " AS QUANTITY ,CONSM_ITEM_UNIT_CODE AS UNIT_CODE FROM TSPL_MF_BOM_DETAIL left outer join TSPL_MF_BOM_HEAD on TSPL_MF_BOM_HEAD.BOM_CODE=TSPL_MF_BOM_DETAIL.BOM_CODE WHERE PROD_ITEM_CODE = '" & row.Cells(colItemCode).Value & "' and  TSPL_MF_BOM_HEAD.REVISION_NO = (select top 1 REVISION_NO  from TSPL_MF_BOM_HEAD where PROD_ITEM_CODE = '" & row.Cells(colItemCode).Value & "' and ( (TSPL_MF_BOM_HEAD.END_DATE is  null) or ( convert (datetime,TSPL_MF_BOM_HEAD.END_DATE,103) >= convert (datetime,'" + dtpBODate.Value + "',103) )) order by TSPL_MF_BOM_HEAD.REVISION_NO desc )  "
                Else
                    strq += "SELECT '" & clsCommon.myCstr(row.Cells(colPPCode).Value) & "' AS PROD_PLAN_CODE,'" & clsCommon.myCstr(row.Cells(colProductionLineCode).Value) & "' AS PRODUCTION_LINE_CODE,'" & clsCommon.myCstr(row.Cells(colItemCode).Tag) & "' AS BOM_CODE, CONSM_ITEM_CODE AS ITEM_CODE,(CONSM_QUANTITY/(case when PROD_QUANTITY=0 then 1 else PROD_QUANTITY end))* " & clsCommon.myCdbl(row.Cells(colBatchQty).Value) & " AS QUANTITY,CONSM_ITEM_UNIT_CODE AS UNIT_CODE FROM TSPL_MF_BOM_DETAIL left outer join TSPL_MF_BOM_HEAD on TSPL_MF_BOM_HEAD.BOM_CODE=TSPL_MF_BOM_DETAIL.BOM_CODE WHERE PROD_ITEM_CODE = '" & row.Cells(colItemCode).Value & "' and  TSPL_MF_BOM_HEAD.REVISION_NO = (select top 1 REVISION_NO  from TSPL_MF_BOM_HEAD where PROD_ITEM_CODE = '" & row.Cells(colItemCode).Value & "' and ( (TSPL_MF_BOM_HEAD.END_DATE is  null) or ( convert (datetime,TSPL_MF_BOM_HEAD.END_DATE,103) >= convert (datetime,'" + dtpBODate.Value + "',103) )) order by TSPL_MF_BOM_HEAD.REVISION_NO desc )   UNION ALL "
                End If
            Next
            strqFinal = "SELECT T2.BO_CODE,T1.PROD_PLAN_CODE,T1.PRODUCTION_LINE_CODE,T1.BOM_CODE,T1.ITEM_CODE,T1.QUANTITY,T1.UNIT_CODE,T2.REQ_QTY,T2.ISSUE_QTY,T2.RETURN_QTY,T2.RECEIPT_QTY,T2.REJ_QTY,T2.BREAKAGE_QTY FROM ("
            strqFinal += "SELECT PROD_PLAN_CODE,PRODUCTION_LINE_CODE,BOM_CODE,ITEM_CODE,SUM(QUANTITY) AS QUANTITY,UNIT_CODE  FROM " & "(" & strq & ") AS T1 GROUP BY PROD_PLAN_CODE,PRODUCTION_LINE_CODE,BOM_CODE,ITEM_CODE,T1.UNIT_CODE) AS T1 LEFT JOIN( "
            strqFinal += "SELECT T4.BO_CODE,T4.PRODUCTION_LINE_CODE,T4.BOM_CODE,T4.ITEM_CODE,SUM(T4.REQ_QTY) AS REQ_QTY,SUM(T4.ISSUE_QTY) AS ISSUE_QTY,SUM(T4.RETURN_QTY) AS RETURN_QTY, "
            strqFinal += "SUM(T4.RECEIPT_QTY) AS RECEIPT_QTY ,SUM(T4.REJ_QTY) AS REJ_QTY ,SUM(T4.BREAKAGE_QTY) AS BREAKAGE_QTY FROM ( "
            strqFinal += "SELECT T1.BO_CODE,T1.PRODUCTION_LINE_CODE,T1.BOM_CODE,T1.REQ_CODE,T1.ITEM_CODE,T1.REQ_QTY,T2.ISSUE_QTY,T21.RETURN_QTY,T3.RECEIPT_QTY,T3.REJ_QTY,T3.BREAKAGE_QTY  "
            strqFinal += "FROM (  "
            strqFinal += "SELECT T1.BO_CODE,T1.PRODUCTION_LINE_CODE,T1.BOM_CODE,T1.REQ_CODE,T1.ITEM_CODE,(REQ_QTY) AS REQ_QTY FROM TSPL_MF_REQ_DETAIL T1 "
            strqFinal += "INNER JOIN TSPL_MF_REQUISITION T2 ON T1.REQ_CODE=T2.REQ_CODE ) AS T1 "
            strqFinal += "LEFT JOIN (SELECT TT1.PRODUCTION_LINE_CODE,TT1.BOM_CODE, TT1.ITEM_CODE,TT1.REQ_CODE,TT1.ISSUE_CODE,(TT1.ISSUE_QTY) AS ISSUE_QTY FROM TSPL_MF_ISSUE_DETAIL TT1 INNER "
            strqFinal += "JOIN TSPL_MF_ISSUE TT2 ON TT1.ISSUE_CODE=TT2.ISSUE_CODE ) AS T2 ON T1.REQ_CODE=T2.REQ_CODE "
            strqFinal += "LEFT JOIN "
            strqFinal += "(SELECT TT1.RETURN_CODE,TT1.PRODUCTION_LINE_CODE,TT1.BOM_CODE, TT1.ITEM_CODE,TT1.ISSUE_CODE,(TT1.RETURN_QTY) AS RETURN_QTY FROM  "
            strqFinal += "TSPL_MF_RETURN_DETAIL TT1 INNER JOIN TSPL_MF_RETURN TT2 ON TT1.RETURN_CODE=TT2.RETURN_CODE ) AS T21  "
            strqFinal += "ON T2.ISSUE_CODE=T21.ISSUE_CODE "

            strqFinal += "LEFT JOIN  "
            strqFinal += "(SELECT TT1.RECEIPT_CODE,TT1.PROD_PLAN_CODE,TT1.PRODUCTION_LINE_CODE,TT1.BOM_CODE,TT2.BO_CODE,TT1.ITEM_CODE,TT1.RECEIPT_QTY,TT1.REJ_QTY,TT1.BREAKAGE_QTY "
            strqFinal += "FROM TSPL_MF_RECEIPT_DETAIL TT1  "
            strqFinal += "INNER JOIN TSPL_MF_RECEIPT TT2 ON TT1.RECEIPT_CODE=TT2.RECEIPT_CODE) AS T3 ON T1.BO_CODE=T3.BO_CODE AND T1.PRODUCTION_LINE_CODE=T3.PRODUCTION_LINE_CODE AND T1.BOM_CODE=T3.BOM_CODE) AS T4 "
            strqFinal += "WHERE T4.BO_CODE='" & Me.txtCode.Value & "'  GROUP BY T4.BO_CODE,T4.PRODUCTION_LINE_CODE,T4.BOM_CODE,T4.ITEM_CODE ) AS T2 ON T1.ITEM_CODE=T2.ITEM_CODE "
            'Else
            '    strqFinal = "SELECT ITEM_CODE,QTY AS QUANTITY,UNIT_CODE,ACTUAL_REQ_QTY AS REQ_QTY,ISSUE_QTY,RETURN_QTY,REJ_QTY,BREAKAGE_QTY FROM TSPL_MF_BATCH_ORDER_DETAIL WHERE BO_CODE='" & clsCommon.myCstr(Me.txtCode.Value) & "'"
            'End If




            Dim dtRM As DataTable
            dtRM = clsDBFuncationality.GetDataTable(strqFinal)
            Me.gvRM.Rows.Clear()

            For intLoop As Integer = 0 To dtRM.Rows.Count - 1
                gvRM.Rows.AddNew()
                gvRM.Rows(gvRM.Rows.Count - 1).Cells(colRMPPCode).Value = dtRM.Rows(intLoop).Item("PROD_PLAN_CODE")
                gvRM.Rows(gvRM.Rows.Count - 1).Cells(colRMProdLineCode).Value = dtRM.Rows(intLoop).Item("PRODUCTION_LINE_CODE")
                gvRM.Rows(gvRM.Rows.Count - 1).Cells(colRMBOMCode).Value = dtRM.Rows(intLoop).Item("BOM_CODE")

                gvRM.Rows(gvRM.Rows.Count - 1).Cells(colRMItemCode).Value = dtRM.Rows(intLoop).Item("ITEM_CODE")
                gvRM.Rows(gvRM.Rows.Count - 1).Cells(colRMQty).Value = dtRM.Rows(intLoop).Item("QUANTITY")
                gvRM.Rows(gvRM.Rows.Count - 1).Cells(colRMUOM).Value = dtRM.Rows(intLoop).Item("UNIT_CODE")
                gvRM.Rows(gvRM.Rows.Count - 1).Cells(colRMActualReq).Value = clsCommon.myCdbl(dtRM.Rows(intLoop).Item("REQ_QTY"))
                gvRM.Rows(gvRM.Rows.Count - 1).Cells(colRMIssue).Value = clsCommon.myCdbl(dtRM.Rows(intLoop).Item("ISSUE_QTY"))
                gvRM.Rows(gvRM.Rows.Count - 1).Cells(colRMReturn).Value = clsCommon.myCdbl(dtRM.Rows(intLoop).Item("RETURN_QTY"))
                gvRM.Rows(gvRM.Rows.Count - 1).Cells(colRMRejection).Value = clsCommon.myCdbl(dtRM.Rows(intLoop).Item("REJ_QTY"))
                gvRM.Rows(gvRM.Rows.Count - 1).Cells(colRMBreakage).Value = clsCommon.myCdbl(dtRM.Rows(intLoop).Item("BREAKAGE_QTY"))
                gvRM.Rows(gvRM.Rows.Count - 1).Cells(colRMConsumed).Value = clsCommon.myCdbl(dtRM.Rows(intLoop).Item("ISSUE_QTY")) - clsCommon.myCdbl(dtRM.Rows(intLoop).Item("RETURN_QTY")) - clsCommon.myCdbl(dtRM.Rows(intLoop).Item("REJ_QTY")) - clsCommon.myCdbl(dtRM.Rows(intLoop).Item("BREAKAGE_QTY"))

                'gvRM.Rows(gvRM.Rows.Count - 1).Cells(colProductionLineCode).Value = dtRM.Rows(intLoop).Item("PRODUCTION_LINE_CODE")
                'gvRM.Rows(gvRM.Rows.Count - 1).Cells(colBOMCode).Value = dtRM.Rows(intLoop).Item("BOM_CODE")
                'gvRM.Rows(gvRM.Rows.Count - 1).Cells(colItemCode).Value = dtRM.Rows(intLoop).Item("ITEM_CODE")
                'gvRM.Rows(gvRM.Rows.Count - 1).Cells(colitemDesc).Value = clsCommon.myCstr(dtRM.Rows(intLoop).Item("ITEM_DESCRIPTION"))
                'gvRM.Rows(gvRM.Rows.Count - 1).Cells(colMinQty).Value = clsCommon.myCdbl(dtRM.Rows(intLoop).Item("MIN_QUANTITY"))
                'gvRM.Rows(gvRM.Rows.Count - 1).Cells(colMaxQty).Value = clsCommon.myCdbl(dtRM.Rows(intLoop).Item("MAX_QUANTITY"))
                'gvRM.Rows(gvRM.Rows.Count - 1).Cells(colRemarks).Value = clsCommon.myCstr(dtRM.Rows(intLoop).Item("REMARKS"))
                'gvRM.Rows(gvRM.Rows.Count - 1).Cells(colmfgDate).Value = Me.dtpBODate.Value

                Dim Product_Type As String = clsItemMaster.GetItemProductType(clsCommon.myCstr(dtRM.Rows(intLoop).Item("ITEM_CODE")), Nothing)
                Dim BalanceQty As Decimal = 0
                If clsCommon.CompairString(Product_Type, "MI") = CompairStringResult.Equal Then
                    BalanceQty = clsInventoryMovementNew.getBalance(clsCommon.myCstr(dtRM.Rows(intLoop).Item("ITEM_CODE")), clsLocation.GetMainLocationMilk(clsCommon.myCstr(fndLocation.Value), Nothing), clsCommon.myCstr(fndLocation.Value), txtCode.Value, dtpBODate.Value, Nothing, clsCommon.myCstr(dtRM.Rows(intLoop).Item("UNIT_CODE")))
                Else
                    BalanceQty = clsItemLocationDetails.getBalance(clsCommon.myCstr(dtRM.Rows(intLoop).Item("ITEM_CODE")), clsCommon.myCstr(fndLocation.Value), txtCode.Value, dtpBODate.Value, Nothing, clsCommon.myCstr(dtRM.Rows(intLoop).Item("UNIT_CODE")), 0)
                End If
                gvRM.Rows(gvRM.Rows.Count - 1).Cells(colFINAL_PROD_Qty_Stock).Value = BalanceQty

            Next

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    'Private Sub cbgPP_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbgPP.Validated
    '    Try
    '        If Me.gvPP.Tag Is Nothing Then
    '            fillPPItems(clsCommon.myCstr(clsCommon.GetMulcallString(cbgPP.CheckedValue)))
    '            'fillRM()
    '        Else
    '            'If clsCommon.myCstr(clsCommon.GetMulcallString(cbgPP.CheckedValue)) <> Me.gvPP.Tag.ToString Or gvPP.Rows.Count = 0 Then
    '            fillPPItems(clsCommon.myCstr(clsCommon.GetMulcallString(cbgPP.CheckedValue)))
    '            'fillRM()
    '            'End If
    '        End If
    '    Catch ex As Exception

    '    End Try

    'End Sub

    
    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        If txtCode.Value = "" Then
            myMessages.blankValue(Me, "BOM Code", Me.Text)
        Else
            funPrint()
        End If
    End Sub
    Private Sub funPrint()
        Try
            Dim qry As String = "SELECT *,(CAST(T1.STD_SPEED AS NUMERIC(10,2) )/T1.GROSS_QTY) AS STD_RATE_SPEED, CAST((T1.GROSS_QTY/T1.CALC_TIME) AS NUMERIC(10,2)) AS ACT_SPEED, "
            qry += " CAST((T1.GROSS_QTY/T1.CALC_TIME) AS NUMERIC(10,2))/(CASE WHEN T1.STD_SPEED=0 THEN 1 ELSE T1.STD_SPEED END)  AS PER_ACHIEVEMENT FROM "
            qry += " ( SELECT  t1.BO_CODE,CONVERT(VARCHAR,T1.BO_DATE,103) AS BO_DATE ,T1.PROD_PLAN_CODE, (datename(WEEKDAY,PLAN_FOR_DATE) + ' ' + (convert(VARCHAR,T1.PLAN_FOR_DATE,103))) AS  PLANNING_DATE, "
            qry += " T1.PRODUCTION_LINE_CODE,T4.PRODUCTION_LINE_NAME,T1.ITEM_CODE AS  MAIN_ITEM_CODE,T5.Item_Desc AS MAIN_ITEM_DESC,T6.Class_Code AS PACKAGE,"
            qry += " T7.Class_Code AS FLAVOUR, T1.BATCH_QTY AS GROSS_QTY,CONVERT(varchar(5),T1.START_TIME,108) AS START_TIME, CONVERT(varchar(5),T1.END_TIME,108) AS STOP_TIME, "
            qry += " CONVERT(varchar(5),T3.END_TIME,108) AS SCHED_END_TIME,CAST(ABS(DATEDIFF(MINUTE,T1.START_TIME,T1.END_TIME)/60.00)  AS NUMERIC(7,2))AS CALC_TIME,"
            qry += " T3.SPEED AS STD_SPEED,T3.REASON, ('" & objCommonVar.CurrentCompanyName & "')  AS COMPANY_NAME  FROM (SELECT TSPL_MF_BATCH_ORDER.BO_CODE,TSPL_MF_BATCH_ORDER.BO_DATE,"
            qry += " TSPL_MF_BATCH_PP_DETAIL.PRODUCTION_LINE_CODE,TSPL_MF_BATCH_PP_DETAIL.ITEM_CODE,TSPL_MF_BATCH_PP_DETAIL.BATCH_QTY, PP.PROD_PLAN_CODE,PP.PLANNING_DATE,"
            qry += " PP.PLAN_FOR_DATE,TSPL_MF_BATCH_PP_DETAIL.START_TIME,TSPL_MF_BATCH_PP_DETAIL.END_TIME  FROM  TSPL_MF_BATCH_PP_DETAIL   INNER JOIN TSPL_MF_BATCH_ORDER  ON "
            qry += " TSPL_MF_BATCH_ORDER.BO_CODE=TSPL_MF_BATCH_PP_DETAIL.BO_CODE  "
            qry += " INNER JOIN TSPL_MF_PRODUCTION_PLAN_HEAD PP ON  TSPL_MF_BATCH_PP_DETAIL.PROD_PLAN_CODE=PP.PROD_PLAN_CODE) AS T1  LEFT JOIN TSPL_MF_BATCH_PP_DETAIL T3 ON  "
            qry += " T1.BO_CODE=T3.BO_CODE AND T1.PROD_PLAN_CODE=T3.PROD_PLAN_CODE AND T1.PRODUCTION_LINE_CODE=T3.PRODUCTION_LINE_CODE  LEFT JOIN TSPL_MF_PRODUCTION_LINES T4 ON "
            qry += " T1.PRODUCTION_LINE_CODE=T4.PRODUCTION_LINE_CODE LEFT JOIN TSPL_ITEM_MASTER T5 ON T1.ITEM_CODE=T5.Item_Code  LEFT JOIN (SELECT ITEM_CODE,CLASS_CODE,CLASS_DESC"
            qry += " FROM TSPL_ITEM_details WHERE Class_Name='SIZE') T6  ON T1.ITEM_CODE=T6.Item_Code  LEFT JOIN (SELECT ITEM_CODE,CLASS_CODE,CLASS_DESC FROM TSPL_ITEM_details "
            qry += " WHERE Class_Name='FLAVOUR') T7  ON T1.ITEM_CODE=T7.Item_Code ) AS T1"
            qry += " WHERE T1.BO_CODE='" & Me.txtCode.Value & "'"
           


            'If txtCode.Value <> "" Then
            '    qry += " and  TSPL_MF_BOM_HEAD.BOM_CODE='" & txtCode.Value & "' "
            'End If
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            Dim frmCRV As New frmCrystalReportViewer()
            frmCRV.funreport(CrystalReportFolder.PRODUCTION, dt, "crptBatchOrderPrint", "Batch Order Print")
            frmCRV = Nothing
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub gvPP_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gvPP.Click

    End Sub

    'Private Sub cbgPP_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cbgPP.Validating
    '    Try
    '        If Me.gvPP.Tag Is Nothing Then
    '            fillPPItems(clsCommon.myCstr(clsCommon.GetMulcallString(cbgPP.CheckedValue)))
    '            'fillRM()
    '        Else
    '            'If clsCommon.myCstr(clsCommon.GetMulcallString(cbgPP.CheckedValue)) <> Me.gvPP.Tag.ToString Or gvPP.Rows.Count = 0 Then
    '            fillPPItems(clsCommon.myCstr(clsCommon.GetMulcallString(cbgPP.CheckedValue)))
    '            'fillRM()
    '            'End If
    '        End If
    '    Catch ex As Exception

    '    End Try

    'End Sub

    Private Sub btnFillPP_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFillPP.Click
        fillPPItems(clsCommon.myCstr(clsCommon.GetMulcallString(cbgPP.CheckedValue)))
    End Sub
    '===============Added by preeti Gupta Against Ticket No[ADV/24/07/18-000035,ADV/24/07/18-000034]
    Private Sub fndLocation__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndLocation._MYValidating
        Try
            Dim WhrCls As String = " Location_Type='Physical'  "
            If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                WhrCls += "  and  Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
            End If
            fndLocation.Value = clsLocation.getFinder(WhrCls, fndLocation.Value, isButtonClicked)
            lblLocationDesc.Text = clsLocation.GetName(fndLocation.Value, Nothing)

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
End Class