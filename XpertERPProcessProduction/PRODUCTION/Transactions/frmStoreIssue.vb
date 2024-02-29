''update on 30-09-2013 @ Ticket no : BM00000000669
'---------------BM00000003534
Imports common
Imports System.Data.SqlClient
Imports System.IO
Imports XpertERPFixedAssets

Public Class frmStoreIssue
    Inherits FrmMainTranScreen
    Dim formtype As String = Nothing

    Public Sub New(ByVal formid As String)
        InitializeComponent()
        formtype = formid
    End Sub
    Const colLineNo As String = "LNO"
    Const colPLCode As String = "PLCode"
    Const colBOMCode As String = "BOMCode"
    Const colItemCode As String = "ItemCode"
    Const colItemDesc As String = "ItemDesc"
    Const colBatchQty As String = "BatchQty"
    Const colRequisitionQty As String = "RequisitionQty"
    Const colIssueQty As String = "IssueQty"
    Const colUOM As String = "UOM"
    Const colRemarks As String = "Remarks"
    Const colRequisitionNo As String = "RequisitionNo"
    Const colBO As String = "colBO"
    Const colMO As String = "colMO"
    Const colIs_Serialized_Item As String = "SerializedItem"
    Const colIs_Auto_PickSerialized As String = "Auto_Serial_No"
    Const colserialno As String = "SerialNo"

    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim isNewEntry As Boolean = True
    Dim isInsideLoadData As Boolean = False
    Dim obj As New clsProductionIssue
    Private ObjList As New List(Of clsProductionIssueDetail)
    Private isCellValueChangedOpen As Boolean = False
    Dim OpenFileDialog1 As New OpenFileDialog
    Dim MOActive As Boolean = False
    Sub LoadGridColumns()
        gvIssue.DataSource = Nothing
        gvIssue.Rows.Clear()
        gvIssue.Columns.Clear()

        Dim repoLineNo As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoLineNo = New GridViewDecimalColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "Line"
        repoLineNo.Name = colLineNo
        repoLineNo.Width = 30
        repoLineNo.ReadOnly = True
        repoLineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvIssue.MasterTemplate.Columns.Add(repoLineNo)

        Dim repoPLCode As New GridViewTextBoxColumn()
        repoPLCode.FormatString = ""
        repoPLCode.HeaderText = "Production Line Code"
        repoPLCode.Name = colPLCode
        repoPLCode.Width = 100
        repoPLCode.ReadOnly = True
        gvIssue.MasterTemplate.Columns.Add(repoPLCode)

        Dim repoBOMCode As New GridViewTextBoxColumn()
        repoBOMCode.FormatString = ""
        repoBOMCode.HeaderText = "BOM Code"
        repoBOMCode.Name = colBOMCode
        repoBOMCode.Width = 100
        repoBOMCode.ReadOnly = True
        gvIssue.MasterTemplate.Columns.Add(repoBOMCode)

        Dim RequisitionNo As New GridViewTextBoxColumn
        RequisitionNo.FormatString = ""
        RequisitionNo.HeaderText = "Requisition No"
        RequisitionNo.Name = colRequisitionNo
        RequisitionNo.Width = 100
        RequisitionNo.ReadOnly = True
        RequisitionNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvIssue.Columns.Add(RequisitionNo)

        Dim ItemCode As New GridViewTextBoxColumn
        ItemCode.FormatString = ""
        ItemCode.HeaderText = "Item Code"
        ItemCode.Name = colItemCode
        ItemCode.Width = 150
        If chkTrading.Checked = True Then
            ItemCode.ReadOnly = False
            ItemCode.HeaderImage = Global.XpertERPProcessProduction.My.Resources.Resources.search4
            ItemCode.TextImageRelation = TextImageRelation.TextBeforeImage
        Else
            ItemCode.ReadOnly = True
            ItemCode.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        End If
        gvIssue.Columns.Add(ItemCode)

        Dim ItemDesc As New GridViewTextBoxColumn
        ItemDesc.FormatString = ""
        ItemDesc.HeaderText = "Item Description"
        ItemDesc.Name = colItemDesc
        ItemDesc.Width = 100
        ItemDesc.ReadOnly = True
        ItemDesc.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvIssue.Columns.Add(ItemDesc)

        '-----------------------serialized no--------------------------------
        Dim repoIsSerItem As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsSerItem.HeaderText = "Is Serialize Item"
        repoIsSerItem.Name = colIs_Serialized_Item
        repoIsSerItem.ReadOnly = True
        repoIsSerItem.IsVisible = False
        repoIsSerItem.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gvIssue.MasterTemplate.Columns.Add(repoIsSerItem)

        Dim repoIsPickAutoSerNo As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsPickAutoSerNo.HeaderText = "Is Pick Auto Serial"
        repoIsPickAutoSerNo.Name = colIs_Auto_PickSerialized
        repoIsPickAutoSerNo.ReadOnly = True
        repoIsPickAutoSerNo.IsVisible = False
        repoIsPickAutoSerNo.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gvIssue.MasterTemplate.Columns.Add(repoIsPickAutoSerNo) '140

        Dim repoSerNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSerNo.HeaderText = "Serial No."
        repoSerNo.Name = colserialno
        repoSerNo.ReadOnly = True
        repoSerNo.IsVisible = False
        repoSerNo.Width = 80
        repoSerNo.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gvIssue.MasterTemplate.Columns.Add(repoSerNo) '140
        '==============================================================================

        Dim BatchQty As New GridViewDecimalColumn
        BatchQty.FormatString = ""
        BatchQty.HeaderText = "Batch Quantity"
        BatchQty.Name = colBatchQty
        BatchQty.Width = 100
        BatchQty.ReadOnly = True
        BatchQty.FormatString = "{0:n2}"
        BatchQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvIssue.Columns.Add(BatchQty)

        Dim RequisitionQty As New GridViewDecimalColumn
        RequisitionQty.FormatString = ""
        RequisitionQty.HeaderText = "Requisition Quantity"
        RequisitionQty.Name = colRequisitionQty
        RequisitionQty.Width = 100
        RequisitionQty.ReadOnly = True
        RequisitionQty.FormatString = "{0:n2}"
        RequisitionQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvIssue.Columns.Add(RequisitionQty)

        Dim IssueQty As New GridViewDecimalColumn
        IssueQty.FormatString = ""
        IssueQty.HeaderText = "Issue  Quantity"
        IssueQty.Name = colIssueQty
        IssueQty.Width = 100
        'IssueQty.ReadOnly = True
        IssueQty.FormatString = "{0:n2}"
        IssueQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvIssue.Columns.Add(IssueQty)

        Dim UOM As New GridViewTextBoxColumn
        UOM.FormatString = ""
        UOM.HeaderText = "UOM"
        UOM.Name = colUOM
        UOM.Width = 100
        If chkTrading.Checked = True Then
            UOM.ReadOnly = False
        Else
            UOM.ReadOnly = True
        End If

        UOM.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvIssue.Columns.Add(UOM)

        Dim Remarks As New GridViewTextBoxColumn
        Remarks.FormatString = ""
        Remarks.HeaderText = "Remarks"
        Remarks.Name = colRemarks
        Remarks.Width = 100
        Remarks.ReadOnly = True
        Remarks.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvIssue.Columns.Add(Remarks)

        Dim BO As New GridViewTextBoxColumn
        BO.FormatString = ""
        BO.HeaderText = "BO"
        BO.Name = colBO
        BO.Width = 100
        BO.ReadOnly = True
        BO.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        BO.IsVisible = False
        gvIssue.Columns.Add(BO)

        Dim MO As New GridViewTextBoxColumn
        MO.FormatString = ""
        MO.HeaderText = "MO"
        MO.Name = colMO
        MO.Width = 100
        MO.ReadOnly = True
        MO.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        MO.IsVisible = False
        gvIssue.Columns.Add(MO)



        gvIssue.AllowDeleteRow = True
        gvIssue.AllowAddNewRow = False
        gvIssue.ShowGroupPanel = False
        gvIssue.AllowColumnReorder = False
        gvIssue.AllowRowReorder = False
        gvIssue.EnableSorting = False
        gvIssue.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gvIssue.MasterTemplate.ShowRowHeaderColumn = False

    End Sub

    Private Sub frmStoreIssue_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnNew.Enabled Then
            funReset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnsave.Enabled Then
            Save(False)
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btndelete.Enabled Then
            DeleteData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            funClose()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            funReset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag AndAlso btnPost.Enabled Then
            PostData()
        End If
    End Sub

    Sub funClose()
        Me.Close()
    End Sub

    Private Sub frmStoreIssue_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        '' get mo setting
        clsDBFuncationality.ExecuteNonQuery("alter table TSPL_MF_ISSUE_DETAIL alter column BOM_CODE Varchar(30)")
        MOActive = ClsMFSeetings.Get_MO_BO_Setting()
        LoadList()
        funReset()
        LoadGridColumns()
        isNewEntry = True
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(btnPost, "Press Alt+P for  Post")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D  for Delete ")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnNew, "Press Alt+N Adding New ")
        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
    End Sub

    Sub LoadList()
        Dim STRQ As String = ""
        If rdbAgainstReq.Checked Then
            STRQ += " select distinct T1.REQ_CODE as [Code],T1.DESCRIPTION as [Description], convert(varchar(12),T1.REQ_DATE,103) as [Date], T1.REQUESTED_BY as [Requested By],T1.LOCATION_CODE as [Location Code],t1.TR_TYPE AS Type  "
            STRQ += " FROM TSPL_MF_REQUISITION T1 inner join TSPL_MF_REQ_DETAIL T2 on T1.REQ_CODE=T2.REQ_CODE  WHERE T1.POSTED= '1'"
            If MOActive = True Then
                STRQ += " AND T1.TR_TYPE='MO' AND T2.MO_CODE IN (Select TSPL_MF_MANUFACTURING_ORDER.MO_CODE " &
        " from TSPL_MF_MANUFACTURING_ORDER " &
        " LEFT JOIN (select TSPL_MF_RECEIPT.MO_CODE,coalesce(sum(TSPL_MF_RECEIPT_DETAIL.RECEIPT_QTY),0) as Rec_qty  from TSPL_MF_RECEIPT " &
        " inner join TSPL_MF_RECEIPT_DETAIL on TSPL_MF_RECEIPT.RECEIPT_CODE=TSPL_MF_RECEIPT_DETAIL.RECEIPT_CODE " &
        " GROUP BY TSPL_MF_RECEIPT.MO_CODE) AS REC ON TSPL_MF_MANUFACTURING_ORDER.MO_CODE=REC.MO_CODE " &
        " WHERE TSPL_MF_MANUFACTURING_ORDER.POSTED=1 and TSPL_MF_MANUFACTURING_ORDER.MO_STATUS NOT IN ('Closed')) " ' order by [Date]  desc , [Code] desc
            Else
                STRQ += " AND T1.TR_TYPE='BO' AND T2.BO_CODE IN (Select TSPL_MF_BATCH_ORDER.BO_CODE from TSPL_MF_BATCH_ORDER " &
                             " INNER JOIN (select BO_CODE,SUM(BATCH_QTY) AS BATCH_QTY from TSPL_MF_BATCH_PP_DETAIL group by BO_CODE) AS BD ON TSPL_MF_BATCH_ORDER.BO_CODE=BD.BO_CODE " &
                             " LEFT JOIN (select TSPL_MF_RECEIPT.BO_CODE,coalesce(sum(TSPL_MF_RECEIPT_DETAIL.RECEIPT_QTY),0) as Rec_qty  from TSPL_MF_RECEIPT " &
                             " inner join TSPL_MF_RECEIPT_DETAIL on TSPL_MF_RECEIPT.RECEIPT_CODE=TSPL_MF_RECEIPT_DETAIL.RECEIPT_CODE " &
                             " GROUP BY TSPL_MF_RECEIPT.BO_CODE) AS REC ON BD.BO_CODE=REC.BO_CODE " &
                             " WHERE TSPL_MF_BATCH_ORDER.POSTED=1 AND BD.BATCH_QTY>coalesce(REC.Rec_qty,0))  " 'order by [Date]  desc , [Code] desc
            End If
        Else
            If MOActive = True Then
                'STRQ = " select T1.MO_CODE as [Code],T1.DESCRIPTION as [Description], convert(varchar(12),T1.MO_DATE,103) as [Date], T1.APPROVED_BY as [Approved By]," & _
                '       " T1.BOM_CODE as [BOM Code],'MO' AS Type FROM TSPL_MF_MANUFACTURING_ORDER T1 WHERE T1.POSTED= '1'"

                STRQ = " Select TSPL_MF_MANUFACTURING_ORDER.MO_CODE as [Code], DESCRIPTION as [Description],convert(varchar(12),MO_DATE,103) AS [Date], POSTED AS [Is Posted]," &
        " convert(varchar(12),POSTING_DATE,103) AS [Posting Date],APPROVED_BY AS [Approved By],'MO' as Type, " &
        " TSPL_MF_MANUFACTURING_ORDER.QTY_ORDERED_STOCK AS [Ordered Quantity],coalesce(REC.Rec_qty,0) as [Received Quantity] from TSPL_MF_MANUFACTURING_ORDER " &
        " LEFT JOIN (select TSPL_MF_RECEIPT.MO_CODE,coalesce(sum(TSPL_MF_RECEIPT_DETAIL.RECEIPT_QTY),0) as Rec_qty  from TSPL_MF_RECEIPT " &
        " inner join TSPL_MF_RECEIPT_DETAIL on TSPL_MF_RECEIPT.RECEIPT_CODE=TSPL_MF_RECEIPT_DETAIL.RECEIPT_CODE " &
        " GROUP BY TSPL_MF_RECEIPT.MO_CODE) AS REC ON TSPL_MF_MANUFACTURING_ORDER.MO_CODE=REC.MO_CODE " &
        " WHERE TSPL_MF_MANUFACTURING_ORDER.POSTED=1 and TSPL_MF_MANUFACTURING_ORDER.MO_STATUS NOT IN ('Closed') order by MO_DATE desc , TSPL_MF_MANUFACTURING_ORDER.MO_CODE desc "
            Else
                'STRQ = " select T1.BO_CODE as [Code],T1.DESCRIPTION as [Description], convert(varchar(12),T1.BO_DATE,103) as [Date], T1.APPROVED_BY as [Approved By]," & _
                '       " 'BO' AS Type FROM TSPL_MF_BATCH_ORDER T1 WHERE T1.POSTED= '1'"

                STRQ = " Select TSPL_MF_BATCH_ORDER.BO_CODE as [Code], DESCRIPTION as [Description],convert(varchar(12),BO_DATE,103) AS [Date], POSTED AS [Is Posted]," &
                             " convert(varchar(12),POSTING_DATE,103) AS [Posting Date],APPROVED_BY AS [Approved By],'BO' as Type," &
                             " BD.BATCH_QTY as [Batch Quantity],COALESCE(REC.Rec_qty,0) AS [Received Quantity] from TSPL_MF_BATCH_ORDER " &
                             " INNER JOIN (select BO_CODE,SUM(BATCH_QTY) AS BATCH_QTY from TSPL_MF_BATCH_PP_DETAIL group by BO_CODE) AS BD ON TSPL_MF_BATCH_ORDER.BO_CODE=BD.BO_CODE " &
                             " LEFT JOIN (select TSPL_MF_RECEIPT.BO_CODE,coalesce(sum(TSPL_MF_RECEIPT_DETAIL.RECEIPT_QTY),0) as Rec_qty  from TSPL_MF_RECEIPT " &
                             " inner join TSPL_MF_RECEIPT_DETAIL on TSPL_MF_RECEIPT.RECEIPT_CODE=TSPL_MF_RECEIPT_DETAIL.RECEIPT_CODE " &
                             " GROUP BY TSPL_MF_RECEIPT.BO_CODE) AS REC ON BD.BO_CODE=REC.BO_CODE " &
                             " WHERE TSPL_MF_BATCH_ORDER.POSTED=1 AND BD.BATCH_QTY>coalesce(REC.Rec_qty,0) order by BO_DATE desc , TSPL_MF_BATCH_ORDER.BO_CODE desc"
            End If
        End If


        Dim dt As DataTable
        dt = clsDBFuncationality.GetDataTable(STRQ)
        cbgReq.DataSource = dt
        cbgReq.ValueMember = "Code"
        cbgReq.DisplayMember = "Description"
        cbgReq.MyShowHeadrText = True
    End Sub

    Private Sub SetUserMgmtNew()
        If formtype = clsUserMgtCode.frmStoreIssueSTD Then
            'MyBase.SetUserMgmt(clsUserMgtCode.frmStoreIssueSTD)
        ElseIf formtype = clsUserMgtCode.frmStoreIssuePepsi Then
            'MyBase.SetUserMgmt(clsUserMgtCode.frmStoreIssuePepsi)
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
        '' get mo setting
        MOActive = ClsMFSeetings.Get_MO_BO_Setting()

        LoadList()
        txtDescription.Text = ""
        txtComment.Text = ""
        Me.dtpDate.Value = Today
        Me.txtExpireDate.Checked = False
        Me.txtExpireDate.Value = Nothing
        UsLock1.Status = ERPTransactionStatus.Pending
        txtIssuedBy.Value = Nothing
        lblIssuedBy.Text = ""
        txtIssuedTo.Value = Nothing
        lblIssuedTo.Text = ""
        txtLocation.Value = Nothing
        lblLocation.Text = ""
        txtFromLocation.Value = Nothing
        lblFromLocation.Text = ""
        btnsave.Text = "Save"
        btnsave.Enabled = True
        btndelete.Enabled = True
        btnPost.Enabled = True
        gvIssue.Rows.Clear()
        gvIssue.Rows.AddNew()
        chkTrading.Checked = False
        chkTrading.Enabled = True
        'LoadList()
    End Sub

    Private Sub txtCode__MYNavigator(ByVal sender As Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtCode._MYNavigator
        Try
            LoadData(txtCode.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Try
            isInsideLoadData = True

            btnsave.Text = "Update"
            isNewEntry = False
            Dim obj As New clsProductionIssue()
            obj = obj.GetData(strCode, NavTyep)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.ISSUE_CODE) > 0) Then
                funReset()
                isNewEntry = False
                'LoadGridColumns()
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
                chkTrading.Checked = obj.isTrading
                chkTrading.Enabled = False
                If obj.isTrading = True Then
                    RadGroupBox4.Visible = False
                    btnShowItems.Visible = False
                Else
                    RadGroupBox4.Visible = True
                    btnShowItems.Visible = True
                End If
                LoadGridColumns()
                If obj.TR_TYPE = "MO" Then
                    MOActive = True
                Else
                    MOActive = False
                End If

                If obj.isAgainstReq Then
                    Me.rdbAgainstReq.Checked = True
                Else
                    Me.rdbAgainstBOMO.Checked = True
                End If

                CheckedRequsitionNo(obj.ISSUE_CODE)
                txtCode.Value = obj.ISSUE_CODE
                dtpDate.Value = obj.ISSUE_DATE
                UsLock1.Status = obj.POSTED
                txtExpireDate.Checked = IIf(obj.EXP_DATE.Year < 2000, False, True)
                If txtExpireDate.Checked Then
                    txtExpireDate.Value = obj.EXP_DATE
                End If
                txtDescription.Text = obj.DESCRIPTION
                txtLocation.Value = obj.LOCATION_CODE
                lblLocation.Text = obj.LOCATION_NAME
                txtFromLocation.Value = obj.LOCATION_CODE_FROM
                lblFromLocation.Text = obj.LOCATION_FROM_NAME
                txtComment.Text = obj.COMMENTS
                txtIssuedBy.Value = obj.ISSUED_BY
                lblIssuedBy.Text = obj.ISSUED_BY_NAME
                txtIssuedTo.Value = obj.ISSUED_TO
                lblIssuedTo.Text = obj.ISSUED_TO_NAME
                For Each objTr As clsProductionIssueDetail In obj.ObjList
                    gvIssue.Rows.AddNew()
                    gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colLineNo).Value = gvIssue.Rows.Count
                    gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colRequisitionNo).Value = objTr.REQ_CODE
                    gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colPLCode).Value = objTr.PRODUCTION_LINE_CODE
                    gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colBOMCode).Value = objTr.BOM_CODE

                    gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colItemCode).Value = objTr.ITEM_CODE
                    gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colItemDesc).Value = objTr.ITEM_DESCRIPTION
                    gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colBatchQty).Value = objTr.BATCH_QTY
                    gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colRequisitionQty).Value = objTr.REQ_QTY
                    gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssueQty).Value = objTr.ISSUE_QTY
                    gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colUOM).Value = objTr.UNIT_CODE
                    gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colRemarks).Value = objTr.REMARKS
                    gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colBO).Value = objTr.BO_CODE
                    gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colMO).Value = objTr.MO_CODE
                    gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIs_Auto_PickSerialized).Value = clsItemMaster.IsPickAutoSerializeItem(objTr.ITEM_CODE)
                    gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIs_Serialized_Item).Value = clsItemMaster.IsSerializeItem(objTr.ITEM_CODE)
                    gvIssue.Rows(gvIssue.Rows.Count - 1).Tag = objTr.arrSrItem
                Next
                If obj.POSTED = ERPTransactionStatus.Pending Then
                    gvIssue.Rows.AddNew()
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        Finally
            isInsideLoadData = False
        End Try

    End Sub

    'Sub CheckedRequsitionNo(ByVal strCode As String)
    '    Dim dt As DataTable = clsDBFuncationality.GetDataTable("Select distinct REQ_CODE from TSPL_MF_ISSUE_DETAIL where ISSUE_CODE ='" + strCode + "' ")
    '    Dim lst As New ArrayList
    '    For Each dr As DataRow In dt.Rows
    '        lst.Add(clsCommon.myCstr(dr("REQ_CODE")))
    '    Next
    '    cbgReq.CheckedValue = lst
    'End Sub
    Sub CheckedRequsitionNo(ByVal strCode As String)
        Dim qry As String = ""
        'If rdbAgainstReq.Checked Then
        '    qry += " select T1.REQ_CODE as [Code],T1.DESCRIPTION as [Description], convert(varchar(12),T1.REQ_DATE,103) as [Date], T1.REQUESTED_BY as [Requested By],T1.LOCATION_CODE as [Location Code],TR_TYPE AS Type  "
        '    qry += " FROM TSPL_MF_REQUISITION T1 WHERE  T1.REQ_CODE IN ( Select distinct REQ_CODE from TSPL_MF_ISSUE_DETAIL where ISSUE_CODE ='" + strCode + "' )"
        'Else
        '    If MOActive Then
        '    Else

        '    End If
        'End If
        If rdbAgainstReq.Checked Then
            qry = ""
            qry += " select T1.REQ_CODE as [Code],T1.DESCRIPTION as [Description], convert(varchar(12),T1.REQ_DATE,103) as [Date], T1.REQUESTED_BY as [Requested By],T1.LOCATION_CODE as [Location Code],TR_TYPE AS Type  "
            qry += " FROM TSPL_MF_REQUISITION T1 WHERE T1.REQ_CODE IN ( Select distinct REQ_CODE from TSPL_MF_ISSUE_DETAIL where ISSUE_CODE ='" + strCode + "' )"
            If MOActive = True Then
                qry += " AND T1.TR_TYPE='MO'"
            Else
                qry += " AND T1.TR_TYPE='BO'"
            End If
        Else
            qry = ""
            If MOActive = True Then
                qry = " select T1.MO_CODE as [Code],T1.DESCRIPTION as [Description], convert(varchar(12),T1.MO_DATE,103) as [Date], T1.APPROVED_BY as [Approved By]," &
                       " T1.BOM_CODE as [BOM Code],'MO' AS Type FROM TSPL_MF_MANUFACTURING_ORDER T1 WHERE T1.MO_CODE IN ( Select distinct MO_CODE from TSPL_MF_ISSUE_DETAIL where ISSUE_CODE ='" + strCode + "' )"
            Else
                qry = " select T1.BO_CODE as [Code],T1.DESCRIPTION as [Description], convert(varchar(12),T1.BO_DATE,103) as [Date], T1.APPROVED_BY as [Approved By]," &
                       " 'BO' AS Type FROM TSPL_MF_BATCH_ORDER T1 WHERE T1.BO_CODE IN ( Select distinct BO_CODE from TSPL_MF_ISSUE_DETAIL where ISSUE_CODE ='" + strCode + "' )"
            End If
        End If


        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        Dim lst As New ArrayList
        For Each dr As DataRow In dt.Rows
            lst.Add(clsCommon.myCstr(dr("Code")))
        Next
        cbgReq.DataSource = dt
        cbgReq.ValueMember = "Code"
        cbgReq.DisplayMember = "Description"
        cbgReq.MyShowHeadrText = True
        cbgReq.CheckedValue = lst
    End Sub

    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        Save(False)
    End Sub

    Public Sub Save(ByVal ChekBtnPost As Boolean)
        Try
            If (AllowToSave()) Then
                Dim obj As New clsProductionIssue()
                If MOActive = True Then
                    obj.TR_TYPE = "MO"
                Else
                    obj.TR_TYPE = "BO"
                End If
                obj.isAgainstReq = Me.rdbAgainstReq.Checked
                obj.ISSUE_CODE = txtCode.Value
                obj.ISSUE_DATE = dtpDate.Value
                If txtExpireDate.Checked Then
                    obj.EXP_DATE = txtExpireDate.Value
                End If
                obj.DESCRIPTION = txtDescription.Text
                obj.LOCATION_CODE = txtLocation.Value
                obj.LOCATION_CODE_FROM = txtFromLocation.Value
                obj.COMMENTS = txtComment.Text
                obj.ISSUED_BY = txtIssuedBy.Value
                obj.ISSUED_TO = txtIssuedTo.Value
                If chkTrading.Checked = True Then
                    obj.isTrading = True 'chkTrading.Checked
                Else
                    obj.isTrading = False ' chkTrading.Checked
                End If

                obj.ObjList = New List(Of clsProductionIssueDetail)
                For Each grow As GridViewRowInfo In gvIssue.Rows
                    If (clsCommon.myLen(grow.Cells(colItemCode).Value) > 0) Then
                        Dim objTr As New clsProductionIssueDetail()
                        objTr.ISSUE_CODE = clsCommon.myCstr(txtCode.Value)
                        objTr.PRODUCTION_LINE_CODE = clsCommon.myCstr(grow.Cells(colPLCode).Value)
                        objTr.BOM_CODE = clsCommon.myCstr(grow.Cells(colBOMCode).Value)
                        objTr.ITEM_CODE = clsCommon.myCstr(grow.Cells(colItemCode).Value)
                        objTr.ITEM_DESCRIPTION = clsCommon.myCstr(grow.Cells(colItemDesc).Value)
                        objTr.BATCH_QTY = clsCommon.myCdbl(grow.Cells(colBatchQty).Value)
                        objTr.REQ_QTY = clsCommon.myCdbl(grow.Cells(colRequisitionQty).Value)

                        If rdbAgainstReq.Checked Then
                            objTr.REQ_CODE = clsCommon.myCstr(grow.Cells(colRequisitionNo).Value)
                            objTr.MO_CODE = clsCommon.myCstr(grow.Cells(colMO).Value)
                            objTr.BO_CODE = clsCommon.myCstr(grow.Cells(colBO).Value)
                        Else
                            objTr.REQ_CODE = ""
                            objTr.MO_CODE = clsCommon.myCstr(grow.Cells(colMO).Value)
                            objTr.BO_CODE = clsCommon.myCstr(grow.Cells(colBO).Value)
                        End If
                        objTr.ISSUE_QTY = clsCommon.myCdbl(grow.Cells(colIssueQty).Value)
                        objTr.UNIT_CODE = clsCommon.myCstr(grow.Cells(colUOM).Value)
                        objTr.REMARKS = clsCommon.myCstr(grow.Cells(colRemarks).Value)
                        objTr.TR_TYPE = obj.TR_TYPE
                        objTr.Doc_Date = dtpDate.Value
                        objTr.To_Location = txtLocation.Value
                        objTr.From_Location = txtFromLocation.Value
                        objTr.IsPosted = ChekBtnPost
                        objTr.arrSrItem = TryCast(grow.Tag, List(Of clsSerializeInvenotry))

                        obj.ObjList.Add(objTr)
                    End If
                Next
                If (obj.ObjList Is Nothing OrElse obj.ObjList.Count <= 0) Then
                    common.clsCommon.MyMessageBoxShow("Please Fill at list one Item")
                    Return
                End If
                If (obj.SaveData(obj, isNewEntry, Nothing)) Then
                    If ChekBtnPost = False Then
                        common.clsCommon.MyMessageBoxShow("Data Saved Successfully")
                    End If
                    LoadData(obj.ISSUE_CODE, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Function AllowToSave() As Boolean
        '===================Added by preeti Gupta==============
        If AllowFutureDateTransaction(dtpDate.Value, Nothing) = False Then
            dtpDate.Select()
            Return False
        End If
        '===========================================================
        If btnsave.Text = "Update" Then
            Dim QryStr As String = " select POSTED from TSPL_MF_ISSUE where ISSUE_CODE = '" + txtCode.Value + "' "
            Dim chkpost As String = clsDBFuncationality.getSingleValue(QryStr)
            If chkpost = "1" Then
                clsCommon.MyMessageBoxShow("Transection already posted")
                Return False
            End If
        End If
        If clsCommon.myLen(txtLocation.Value) <= 0 Then
            myMessages.blankValue(" To Location")
            txtLocation.Focus()
            Return False
        End If
        If clsCommon.myLen(txtFromLocation.Value) <= 0 Then
            myMessages.blankValue(" From Location")
            txtFromLocation.Focus()
            Return False
        End If
        If clsCommon.CompairString(txtFromLocation.Value, txtLocation.Value) = CompairStringResult.Equal Then
            myMessages.blankValue(" From Location and To Location can not be same.")
            txtFromLocation.Focus()
            Return False
        End If

        If clsCommon.myLen(txtIssuedBy.Value) <= 0 Then
            myMessages.blankValue(" Issued By")
            txtIssuedBy.Focus()
            Return False
        End If
        If clsCommon.myLen(txtIssuedTo.Value) <= 0 Then
            myMessages.blankValue("Issued To")
            txtIssuedTo.Focus()
            Return False
        End If

        Dim ii As Integer = 0
        Dim arrICode As New List(Of String)()
        For Each dr As GridViewRowInfo In gvIssue.Rows

            Dim strICode As String = clsCommon.myCstr(dr.Cells(colItemCode).Value)
            'Dim strIName As String = clsCommon.myCstr(gvIssue.Rows(ii).Cells(colItemDesc).Value)
            If clsCommon.myLen(strICode) > 0 Then
                '======================
                Dim Itemcount As Integer = 0
                For jj As Integer = 0 To gvIssue.Rows.Count - 1
                    Dim strInnerICode As String = clsCommon.myCstr(gvIssue.Rows(jj).Cells(colItemCode).Value)
                    If clsCommon.CompairString(strICode, strInnerICode) = CompairStringResult.Equal AndAlso clsCommon.myLen(strInnerICode) > 0 Then
                        Itemcount += 1
                    End If
                Next
                If Itemcount > 1 AndAlso chkTrading.Checked = True Then
                    clsCommon.MyMessageBoxShow("Item Code (" + strICode + ") Duplicate Found in Grid.")
                    Return False
                End If
                '======================
                If clsCommon.myCdbl(dr.Cells(colIssueQty).Value) > clsCommon.myCdbl(dr.Cells(colRequisitionQty).Value) AndAlso chkTrading.Checked = False Then
                    clsCommon.MyMessageBoxShow("Issue Quantity can not be grater then Requisition Quantity in Row No " + (dr.Index + 1).ToString())
                    Return False
                End If

                Dim availQty As Double = 0
                Dim reqQty As Double = 0

                availQty = clsItemLocationDetails.getBalanceWithUnapproveForRMOther(dr.Cells(colItemCode).Value, Me.txtFromLocation.Value, Me.txtCode.Value, dtpDate.Value, Nothing, dr.Cells(colUOM).Value)
                reqQty = dr.Cells(colIssueQty).Value ''clsCommon.myCdbl(dr.Cells(colItemCode)) * (clsCommon.myCdbl(Me.txtQuantity.Text) / clsCommon.myCdbl(Me.txtBuildQty.Text))
                If reqQty <= 0 AndAlso chkTrading.Checked = True Then
                    Throw New Exception(" Please Enter Issue Qty For Item Code(" & dr.Cells(colItemCode).Value & ") ")
                End If

                If availQty < reqQty Then
                    Throw New Exception("Item Code: " & dr.Cells(colItemCode).Value & " ; Required Qty : " & reqQty & " ; Available Qty : " & availQty & "")
                End If
                If clsCommon.myLen(clsCommon.myCstr(dr.Cells(colUOM).Value)) <= 0 AndAlso chkTrading.Checked = True Then
                    Throw New Exception(" Please Select UOM For Item Code(" & dr.Cells(colItemCode).Value & ") ")
                End If

                Dim is_serialized As String = Nothing
                Dim serialno As String = Nothing
                Dim dblQty As Decimal = Nothing

                is_serialized = clsCommon.myCstr(IIf(dr.Cells(colIs_Serialized_Item).Value = True, "1", "0"))
                serialno = clsCommon.myCstr(dr.Cells(colserialno).Value)
                dblQty = clsCommon.myCdbl(dr.Cells(colIssueQty).Value)

                If clsCommon.myCBool(gvIssue.Rows(ii).Cells(colIs_Serialized_Item).Value) Then
                    Dim arrSerailNo As List(Of clsSerializeInvenotry) = TryCast(gvIssue.Rows(ii).Tag, List(Of clsSerializeInvenotry))
                    If arrSerailNo Is Nothing OrElse dblQty <> arrSerailNo.Count Then
                        common.clsCommon.MyMessageBoxShow("Please provide serial no for item : " + strICode + " . At Line No" + clsCommon.myCstr(ii + 1))
                        Return False
                    End If
                End If


                If Not arrICode.Contains(strICode) Then
                    arrICode.Add(strICode)
                End If
            End If

            ii += 1
        Next


        '-----------------------------serialized check--------------------------------

        '===========================================================
        If ObjList Is Nothing Then
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
                If (clsProductionIssue.DeleteData(txtCode.Value)) Then
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
    Private Sub txtCode__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtCode._MYValidating
        Dim str As String = "select count(*) from TSPL_MF_ISSUE where ISSUE_CODE ='" + txtCode.Value + "' "
        Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
        If no = 0 AndAlso isButtonClicked = False Then
            txtCode.MyReadOnly = False
        Else
            txtCode.MyReadOnly = True
        End If
        If txtCode.MyReadOnly OrElse isButtonClicked Then
            Dim qry As String = "SELECT ISSUE_CODE AS [Code],DESCRIPTION AS [Description],ISSUE_DATE AS [Issue Date],EXP_DATE AS [Exp. Date],ISSUED_BY AS [Issued By],LOCATION_CODE AS [Location Code],COMMENTS AS [Comments],POSTED AS [Posted],POSTING_DATE AS [Posting Date] FROM TSPL_MF_ISSUE "
            txtCode.Value = clsCommon.ShowSelectForm("TSPL_MF_ISSUE", qry, "Code", "", txtCode.Value, "Code", isButtonClicked)
            If txtCode.Value <> "" Then
                LoadData(txtCode.Value, NavigatorType.Current)
            Else
                funReset()
            End If
        End If
    End Sub

    Private Sub txtLocation__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtLocation._MYValidating
        Try
            Dim qry As String = "select Location_Code as Code,Location_Desc as Name from TSPL_LOCATION_MASTER "
            Dim WhrCls As String = " Location_Type='Physical'  "
            If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                WhrCls += "  and  Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
            End If
            txtLocation.Value = clsCommon.ShowSelectForm("VendorLocFND", qry, "Code", WhrCls, txtLocation.Value, "Code", isButtonClicked)
            lblLocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" + txtLocation.Value + "'"))
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub txtIssuedBy__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtIssuedBy._MYValidating
        Try
            Dim OBJEMP As New clsEmployeeMaster
            OBJEMP = clsEmployeeMaster.FinderForEmployee(Me.txtIssuedBy.Value, isButtonClicked)
            If Not OBJEMP Is Nothing Then
                Me.txtIssuedBy.Value = OBJEMP.EMP_CODE
                Me.lblIssuedBy.Text = OBJEMP.Emp_Name
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub btnPost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPost.Click
        PostData()
    End Sub

    Sub PostData()
        Try
            If (myMessages.postConfirm()) Then
                Save(True)
                If (clsProductionIssue.PostData(txtCode.Value, True)) Then
                    common.clsCommon.MyMessageBoxShow("Successfully Posted")
                    LoadData(txtCode.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub gvIssue_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvIssue.CellValueChanged
        Try
            If (Not isInsideLoadData) Then
                If Not isCellValueChangedOpen Then
                    isCellValueChangedOpen = True

                    If (e.Column Is gvIssue.Columns(colItemCode) OrElse e.Column Is gvIssue.Columns(colIssueQty)) AndAlso chkTrading.Checked = False Then
                        If e.Column Is gvIssue.Columns(colItemCode) Then
                            OpenICodeList(False)
                        End If
                        If e.Column Is gvIssue.Columns(colIssueQty) Then
                            If clsCommon.myCdbl(gvIssue.CurrentRow.Cells(colIssueQty).Value) > clsCommon.myCdbl(gvIssue.CurrentRow.Cells(colRequisitionQty).Value) Then
                                clsCommon.MyMessageBoxShow(" Issue Quantity can not be grater then Requisition Quantity.")
                                gvIssue.CurrentRow.Cells(colIssueQty).Value = 0
                            End If
                        End If

                        '-------------------serialized item--------------------------
                        If e.Column Is gvIssue.Columns(colIssueQty) AndAlso Not clsCommon.myCBool(e.Column Is gvIssue.Columns(colIs_Auto_PickSerialized)) Then
                            OpenSerialItem()
                        End If
                        '=========================================================
                    End If
                    If e.Column Is gvIssue.Columns(colItemCode) AndAlso chkTrading.Checked = True Then
                        If e.Column Is gvIssue.Columns(colItemCode) Then
                            OpenICodeList(False)
                        End If
                        'If e.Column Is gvIssue.Columns(colIssueQty) Then
                        '    If clsCommon.myCdbl(gvIssue.CurrentRow.Cells(colIssueQty).Value) > clsCommon.myCdbl(gvIssue.CurrentRow.Cells(colRequisitionQty).Value) Then
                        '        clsCommon.MyMessageBoxShow(" Issue Quantity can not be grater then Requisition Quantity.")
                        '        gvIssue.CurrentRow.Cells(colIssueQty).Value = 0
                        '    End If
                        'End If

                        '-------------------serialized item--------------------------
                        If e.Column Is gvIssue.Columns(colIssueQty) AndAlso Not clsCommon.myCBool(e.Column Is gvIssue.Columns(colIs_Auto_PickSerialized)) Then
                            OpenSerialItem()
                        End If
                    ElseIf e.Column Is gvIssue.Columns(colUOM) AndAlso chkTrading.Checked = True Then
                        OpenUOMList(False)

                    End If

                    isCellValueChangedOpen = False
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        Finally
        End Try
    End Sub

    Sub OpenSerialItem()
        If clsCommon.myCBool(gvIssue.CurrentRow.Cells(colIs_Serialized_Item).Value) Then
            Dim frm As frmSerializeItemOutCheckBox = New frmSerializeItemOutCheckBox()
            frm.strItemCode = clsCommon.myCstr(gvIssue.CurrentRow.Cells(colItemCode).Value)
            frm.strItemName = clsCommon.myCstr(gvIssue.CurrentRow.Cells(colItemDesc).Value)
            frm.strLocationCode = txtFromLocation.Value
            frm.strCurrDocNo = txtCode.Value
            frm.strCurrDocType = "PROD_IS"
            frm.dblqty = clsCommon.myCdbl(gvIssue.CurrentRow.Cells(colIssueQty).Value)
            frm.arr = TryCast(gvIssue.CurrentRow.Tag, List(Of clsSerializeInvenotry))
            frm.ShowDialog()

            If Not frm.isCencelButtonClicked Then
                gvIssue.CurrentRow.Tag = frm.arr
                gvIssue.CurrentRow.Cells(colserialno).Value = clsCommon.myCstr(frm.arr(0).Auto_Sr_No)
            End If
        End If
    End Sub

    Sub OpenICodeList(ByVal isButtonClick As Boolean)
        Dim obj As clsItemMaster = clsItemMaster.FinderForItem(clsCommon.myCstr(gvIssue.CurrentRow.Cells(colItemCode).Value), "cboItemType.SelectedValue", True, isButtonClick, "", "")
        If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Item_Code) > 0 Then
            gvIssue.CurrentRow.Cells(colItemCode).Value = obj.Item_Code
            gvIssue.CurrentRow.Cells(colItemDesc).Value = obj.Item_Desc
            gvIssue.CurrentRow.Cells(colUOM).Value = obj.Unit_Code
            gvIssue.CurrentRow.Cells(colIs_Serialized_Item).Value = clsItemMaster.IsSerializeItem(obj.Item_Code) ' obj.Is_Serial_Item
            gvIssue.CurrentRow.Cells(colIs_Auto_PickSerialized).Value = clsItemMaster.IsPickAutoSerializeItem(obj.Item_Code) ' obj.Is_Pick_Auto_SrNo
        Else
            gvIssue.CurrentRow.Cells(colItemCode).Value = ""
            gvIssue.CurrentRow.Cells(colItemDesc).Value = ""
            gvIssue.CurrentRow.Cells(colUOM).Value = ""
            gvIssue.CurrentRow.Cells(colIs_Serialized_Item).Value = False
            gvIssue.CurrentRow.Cells(colIs_Auto_PickSerialized).Value = False
            gvIssue.CurrentRow.Cells(colserialno).Value = ""
        End If
    End Sub
    'Sub OpenItemListForTrading(ByVal isButtonClick As Boolean)
    '    Dim strICode As String = clsCommon.myCstr(gvIssue.CurrentRow.Cells(colItemCode).Value)
    '    If clsCommon.myLen(strICode) > 0 Then
    '        Dim qry As String = "select UOM_Code as Code,UOM_Description as [Description] from TSPL_ITEM_UOM_DETAIL"
    '        Dim whrCls As String = "Item_Code='" + strICode + "'"
    '        gvIssue.CurrentRow.Cells(colUOM).Value = clsCommon.ShowSelectForm("StanderdStoreIssue@UOMfndnder", qry, "Code", whrCls, clsCommon.myCstr(gvIssue.CurrentRow.Cells(colUOM).Value), "Code", isButtonClick)
    '    Else
    '        gvIssue.CurrentRow.Cells(colUOM).Value = ""
    '    End If
    'End Sub
    Sub OpenUOMList(ByVal isButtonClick As Boolean)
        Dim strICode As String = clsCommon.myCstr(gvIssue.CurrentRow.Cells(colItemCode).Value)
        If clsCommon.myLen(strICode) > 0 Then
            Dim qry As String = "select UOM_Code as Code,UOM_Description as [Description] from TSPL_ITEM_UOM_DETAIL"
            Dim whrCls As String = "Item_Code='" + strICode + "'"
            gvIssue.CurrentRow.Cells(colUOM).Value = clsCommon.ShowSelectForm("StanderdStoreIssue@UOMfndnder", qry, "Code", whrCls, clsCommon.myCstr(gvIssue.CurrentRow.Cells(colUOM).Value), "Code", isButtonClick)
        Else
            gvIssue.CurrentRow.Cells(colUOM).Value = ""
        End If
    End Sub
    Private Sub gvIssue_UserAddedRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowEventArgs) Handles gvIssue.UserAddedRow
        For i As Integer = 0 To gvIssue.Rows.Count - 1
            gvIssue.Rows(0).Cells(0).Value = 1
            If i <> 0 Then
                gvIssue.Rows(i).Cells(colLineNo).Value = i + 1
            End If
        Next
    End Sub

    Private Sub gvIssue_UserDeletingRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs) Handles gvIssue.UserDeletingRow
        If common.clsCommon.MyMessageBoxShow("Delete The Current Row." + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
            e.Cancel = True
        End If
    End Sub

    Private Sub gvIssue_UserDeletedRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowEventArgs) Handles gvIssue.UserDeletedRow
        For ii As Integer = 1 To gvIssue.Rows.Count
            gvIssue.Rows(ii - 1).Cells(colLineNo).Value = ii
        Next
    End Sub

    Private Sub gv1_CurrentColumnChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gvIssue.CurrentColumnChanged
        If gvIssue.RowCount > 0 Then
            Dim intCurrRow As Integer = gvIssue.CurrentRow.Index
            gvIssue.CurrentRow.Cells(colLineNo).Value = clsCommon.myCdbl(intCurrRow + 1)
            If intCurrRow = gvIssue.Rows.Count - 1 Then
                gvIssue.Rows.AddNew()
                gvIssue.CurrentRow = gvIssue.Rows(intCurrRow)
            End If
        End If
    End Sub

    Private Sub btnShowItems_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShowItems.Click
        Dim qry As String = ""
        If clsCommon.myLen(txtIssuedBy.Value) <= 0 Then
            clsCommon.MyMessageBoxShow("Please fill issue by detail first.", Me.Text)
            Return
        End If

        If clsCommon.myLen(txtIssuedTo.Value) <= 0 Then
            clsCommon.MyMessageBoxShow("Please fill issue to detail first.", Me.Text)
            Return
        End If

        If clsCommon.myLen(txtFromLocation.Value) <= 0 Then
            clsCommon.MyMessageBoxShow("Please fill from location detail first.", Me.Text)
            Return
        End If

        If clsCommon.myLen(txtLocation.Value) <= 0 Then
            clsCommon.MyMessageBoxShow("Please fill location detail first.", Me.Text)
            Return
        End If

        If rdbAgainstReq.Checked Then
            qry = " SELECT T1.REQ_CODE,T1.ITEM_CODE,T1.UNIT_CODE,T1.BATCH_QTY,T1.REQ_QTY,T1.ITEM_DESCRIPTION,T1.REMARKS,T1.PRODUCTION_LINE_CODE,T1.BOM_CODE,T1.BO_CODE,T1.MO_CODE FROM TSPL_MF_REQ_DETAIL T1 "
            qry += " WHERE T1.REQ_CODE IN (" + clsCommon.GetMulcallString(cbgReq.CheckedValue) + ")"
        Else
            If MOActive = True Then
                qry = "SELECT TSPL_MF_MANUFACTURING_ORDER.MO_CODE AS REQ_CODE,TSPL_MF_MO_MATERIAL.CONSM_ITEM_CODE as ITEM_CODE,TSPL_MF_MO_MATERIAL.CONSM_ITEM_UNIT_CODE AS UNIT_CODE," & _
                      " TSPL_MF_MO_MATERIAL.CONSM_QUANTITY AS BATCH_QTY,TSPL_MF_MO_MATERIAL.CONSM_QUANTITY AS REQ_QTY,TSPL_MF_MO_MATERIAL.ITEM_DESCRIPTION, " & _
                      " TSPL_MF_MO_MATERIAL.REMARKS,TSPL_MF_MANUFACTURING_ORDER.PRODUCTION_AREA AS PRODUCTION_LINE_CODE,TSPL_MF_MANUFACTURING_ORDER.BOM_CODE,TSPL_MF_MANUFACTURING_ORDER.MO_CODE,'' AS BO_CODE " & _
                      " FROM TSPL_MF_MO_MATERIAL inner join  TSPL_MF_MANUFACTURING_ORDER  on TSPL_MF_MO_MATERIAL.MO_CODE=TSPL_MF_MANUFACTURING_ORDER.MO_CODE " & _
                      " WHERE TSPL_MF_MANUFACTURING_ORDER.MO_CODE IN (" + clsCommon.GetMulcallString(cbgReq.CheckedValue) + ")"
            Else
                qry = " SELECT T1.BO_CODE AS REQ_CODE,T1.ITEM_CODE,T1.UNIT_CODE,T1.QTY AS BATCH_QTY,T1.QTY AS REQ_QTY,T1.PRODUCTION_LINE_CODE,T1.BOM_CODE,T2.Item_Desc AS ITEM_DESCRIPTION,'' AS REMARKS,T1.BO_CODE,'' AS MO_CODE FROM TSPL_MF_BATCH_ORDER_DETAIL T1 " & _
                      " LEFT OUTER JOIN TSPL_ITEM_MASTER T2 ON T2.ITEM_CODE= T1.ITEM_CODE " & _
                      " WHERE T1.BO_CODE IN (" + clsCommon.GetMulcallString(cbgReq.CheckedValue) + ")"
            End If
        End If

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

        gvIssue.DataSource = Nothing
        LoadGridColumns()
        Dim Count As Int16 = 0
        isInsideLoadData = True
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            For Each DR As DataRow In dt.Rows
                gvIssue.Rows.AddNew()
                Count = Count + 1
                gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colLineNo).Value = clsCommon.myCstr(Count)
                gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colRequisitionNo).Value = clsCommon.myCstr(DR("REQ_CODE"))
                gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colPLCode).Value = clsCommon.myCstr(DR("PRODUCTION_LINE_CODE"))
                gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colBOMCode).Value = clsCommon.myCstr(DR("BOM_CODE"))

                gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colItemCode).Value = clsCommon.myCstr(DR("ITEM_CODE"))
                gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colItemDesc).Value = clsCommon.myCstr(DR("ITEM_DESCRIPTION"))
                gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colBatchQty).Value = clsCommon.myCdbl(DR("BATCH_QTY"))
                gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colRequisitionQty).Value = clsCommon.myCstr(DR("REQ_QTY"))
                ' Add 
                gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIssueQty).Value = clsCommon.myCstr(DR("REQ_QTY"))
                gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colUOM).Value = clsCommon.myCstr(DR("UNIT_CODE"))
                gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colRemarks).Value = clsCommon.myCstr(DR("REMARKS"))
                gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colBO).Value = clsCommon.myCstr(DR("BO_CODE"))
                gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colMO).Value = clsCommon.myCstr(DR("MO_CODE"))
                gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIs_Auto_PickSerialized).Value = clsItemMaster.IsPickAutoSerializeItem(clsCommon.myCstr(DR("item_code")))
                gvIssue.Rows(gvIssue.Rows.Count - 1).Cells(colIs_Serialized_Item).Value = clsItemMaster.IsSerializeItem(clsCommon.myCstr(DR("item_code")))
            Next
        Else
            clsCommon.MyMessageBoxShow("No Data Found to display.")
        End If
        ' gv1.Rows.AddNew()
        isInsideLoadData = False
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        If txtCode.Value = "" Then
            myMessages.blankValue("Requisition Number")
        Else
            funPrint()
        End If
    End Sub

    Private Sub funPrint()
        Try
            Dim qry As String = " select TSPL_MF_ISSUE.ISSUE_CODE as [ISSUE_CODE], convert(varchar,TSPL_MF_ISSUE.ISSUE_DATE,103) as [ISSUE_DATE], (case when year(TSPL_MF_ISSUE.EXP_DATE)>1 then convert(varchar,TSPL_MF_ISSUE.EXP_DATE,103) else '' end) as [EXP_DATE],TSPL_MF_ISSUE.DESCRIPTION as [Description], TSPL_MF_ISSUE.COMMENTS as [COMMENTS], "
            'qry += " TSPL_MF_ISSUE.REQUESTED_BY,TSPL_MF_ISSUE.LOCATION_CODE,"
            qry += " TSPL_MF_ISSUE_DETAIL.ITEM_CODE as [ITEM_CODE], TSPL_MF_ISSUE_DETAIL.ITEM_DESCRIPTION as [ITEM_DESCRIPTION],TSPL_MF_ISSUE_DETAIL.BATCH_QTY as [BATCH_QTY], TSPL_MF_ISSUE_DETAIL.REMARKS as [Remarks], TSPL_MF_ISSUE_DETAIL.UNIT_CODE as [UNIT_CODE], TSPL_MF_ISSUE_DETAIL.REQ_QTY as [REQ_QTY], TSPL_MF_ISSUE_DETAIL.ISSUE_QTY as [ISSUE_QTY],TSPL_MF_ISSUE_DETAIL.REQ_CODE as [REQ_CODE],"
            qry += " TSPL_EMPLOYEE_MASTER.Emp_Name as [REQUESTED_USER],TSPL_LOCATION_MASTER.Location_Desc as [Location_Desc], '' as [AuthorizeBy] "
            qry += " from TSPL_MF_ISSUE join TSPL_MF_ISSUE_DETAIL on TSPL_MF_ISSUE.ISSUE_CODE =TSPL_MF_ISSUE_DETAIL.ISSUE_CODE "
            qry += " left outer join TSPL_EMPLOYEE_MASTER on TSPL_EMPLOYEE_MASTER.EMP_CODE =TSPL_MF_ISSUE.ISSUED_BY "
            qry += " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code =TSPL_MF_ISSUE.LOCATION_CODE"
            qry += " where 2=2 "
            If txtCode.Value <> "" Then
                qry += " and  TSPL_MF_ISSUE.ISSUE_CODE = '" + txtCode.Value + "' "
            End If
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            Dim frmCRV As New frmCrystalReportViewer()
            frmCRV.funreport(CrystalReportFolder.PRODUCTION, dt, "ProductionIssue", "Production Issue")
            frmCRV = Nothing
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub txtIssuedTo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtIssuedTo._MYValidating
        Try
            Dim OBJEMP As New clsEmployeeMaster
            OBJEMP = clsEmployeeMaster.FinderForEmployee(Me.txtIssuedTo.Value, isButtonClicked)
            If Not OBJEMP Is Nothing Then
                Me.txtIssuedTo.Value = OBJEMP.EMP_CODE
                Me.lblIssuedTo.Text = OBJEMP.Emp_Name
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub txtFromLocation__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtFromLocation._MYValidating
        Try
            Dim qry As String = "select Location_Code as Code,Location_Desc as Name from TSPL_LOCATION_MASTER "
            Dim WhrCls As String = " Location_Type='Physical'  "
            If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                WhrCls += "  and  Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
            End If
            txtFromLocation.Value = clsCommon.ShowSelectForm("VendorLocFND", qry, "Code", WhrCls, txtFromLocation.Value, "Code", isButtonClicked)
            lblFromLocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" + txtFromLocation.Value + "'"))
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub rdbAgainstReq_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbAgainstReq.CheckedChanged
        Try
            LoadList()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try

    End Sub

    Private Sub gvIssue_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles gvIssue.KeyDown

        If e.KeyCode = Keys.F4 Then
            OpenSerialItem()
        End If
    End Sub

    Private Sub chkTrading_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkTrading.ToggleStateChanged
        If isInsideLoadData = False Then

            If chkTrading.Checked Then
                RadGroupBox4.Visible = False
                btnShowItems.Visible = False
                gvIssue.Rows.Clear()
                gvIssue.Rows.AddNew()
                LoadGridColumns()
                gvIssue.Rows.AddNew()
            Else
                RadGroupBox4.Visible = False
                btnShowItems.Visible = False
                gvIssue.Rows.Clear()
                gvIssue.Rows.AddNew()
                LoadGridColumns()
            End If
        End If
    End Sub

    Private Sub btnShowInventory_Click(sender As Object, e As EventArgs) Handles btnShowInventory.Click
        clsOpenInventory.ShowInventoryDatails(txtCode.Value)
    End Sub
End Class