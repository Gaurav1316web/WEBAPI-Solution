''19/09/2013---Created by --[Pradeep Sharma]-- Ticket no : BM00000000484
'updated by pradeep BM00000000717
Imports common
Imports Microsoft.Office.Interop

Public Class frmProductionRequisition
    Inherits FrmMainTranScreen
#Region "Variables"

    Private isCellValueChangedOpen As Boolean = False
    Private isNewEntry As Boolean = False
    Private isInsideLoadData As Boolean = False

    Const colLineNo As String = "LNO"
    Const colBatchNo As String = "BatchNo"
    Const colPLCode As String = "PLCode"
    Const colBOMCode As String = "BOMCode"

    Const colICode As String = "ICODE"
    Const colIName As String = "INAME"
    Const colBatchQty As String = "BatchQty"
    Const colReqQty As String = "ReqQty"
    Const colUnit As String = "Unit"
    Const colRemarks As String = "REMARKS"

    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim repoBalQty As GridViewDecimalColumn

    Public strDocumentNo As String = ""
    Public MOActive As Boolean = False
#End Region

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmProductionRequisition)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnPost.Visible = MyBase.isPostFlag
        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub FrmAPInvoiceEntry_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction")
        ButtonToolTip.SetToolTip(btnPost, "Press Alt+P Post Trasnaction")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnAddNew, "Press Alt+N Adding New Trasnaction")
        '' get mo setting
        MOActive = ClsMFSeetings.Get_MO_BO_Setting()
        If MOActive = True Then
            Me.RadGroupBox1.Text = "MO NO."
        Else
            Me.RadGroupBox1.Text = "BO NO."
        End If
        RadPageView1.SelectedPage = RadPageViewPage1
        LoadBlankGrid()
        AddNew()
        SetLength()
        If MOActive = True Then
            LoadMONo()
        Else
            LoadBatchNo()
        End If

        If clsCommon.myLen(strDocumentNo) > 0 Then
            LoadData(strDocumentNo, NavigatorType.Current)
        End If
        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
    End Sub

    Sub SetLength()
        txtReqNo.MyMaxLength = 30
        txtDesc.MaxLength = 100
        txtComment.MaxLength = 500
    End Sub

    Sub BlankAllControls()
        txtReqNo.Value = ""
        txtDesc.Text = ""
        txtLocation.Value = ""
        lblLocation.Text = ""
        txtComment.Text = ""
        txtDate.Value = clsCommon.myCDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy")
        txtExpireDate.Value = txtDate.Value
        txtRequestBy.Value = Nothing
        lblRequestedBy.Text = ""
    End Sub

    Sub LoadBlankGrid()

        gv1.Rows.Clear()
        gv1.Columns.Clear()

        Dim repoLineNo As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoLineNo = New GridViewDecimalColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "Line"
        repoLineNo.Name = colLineNo
        repoLineNo.Width = 30
        repoLineNo.ReadOnly = True
        repoLineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoLineNo)

        Dim repoBatchNo As New GridViewTextBoxColumn()
        repoBatchNo.FormatString = ""
        If MOActive = True Then
            repoBatchNo.HeaderText = "MO No"
        Else
            repoBatchNo.HeaderText = "Batch No"
        End If

        repoBatchNo.Name = colBatchNo
        repoBatchNo.Width = 100
        repoBatchNo.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoBatchNo)

        Dim repoPLCode As New GridViewTextBoxColumn()
        repoPLCode.FormatString = ""
        repoPLCode.HeaderText = "Production Line Code"
        repoPLCode.Name = colPLCode
        repoPLCode.Width = 100
        repoPLCode.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoPLCode)

        Dim repoBOMCode As New GridViewTextBoxColumn()
        repoBOMCode.FormatString = ""
        repoBOMCode.HeaderText = "BOM Code"
        repoBOMCode.Name = colBOMCode
        repoBOMCode.Width = 100
        repoBOMCode.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoBOMCode)

        Dim repoICode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoICode.FormatString = ""
        repoICode.HeaderText = "Item Code"
        repoICode.Name = colICode
        repoICode.ReadOnly = True
        'repoICode.HeaderImage = Global.ERP.My.Resources.Resources.search4
        'repoICode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoICode.Width = 100
        gv1.MasterTemplate.Columns.Add(repoICode)

        Dim repoIName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoIName.FormatString = ""
        repoIName.HeaderText = "Description"
        repoIName.Name = colIName
        repoIName.Width = 150
        repoIName.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoIName)

        repoBalQty = New GridViewDecimalColumn()
        repoBalQty.FormatString = ""
        repoBalQty.WrapText = True
        If MOActive = True Then
            repoBalQty.HeaderText = "MO Quantity"
        Else
            repoBalQty.HeaderText = "Batch Quantity"
        End If

        repoBalQty.Name = colBatchQty
        repoBalQty.Width = 100
        repoBalQty.Minimum = 0
        repoBalQty.ReadOnly = True
        repoBalQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoBalQty)

        Dim repoQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoQty = New GridViewDecimalColumn()
        repoQty.FormatString = ""
        repoQty.HeaderText = "Req. Quantity"
        repoQty.Name = colReqQty
        repoQty.Width = 100
        repoQty.Minimum = 0
        repoQty.ReadOnly = False
        repoQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoQty)

        Dim repoUnit As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoUnit.FormatString = ""
        repoUnit.HeaderText = "UOM"
        repoUnit.Name = colUnit
        repoUnit.ReadOnly = True
        repoUnit.Width = 100
        gv1.MasterTemplate.Columns.Add(repoUnit)

        Dim repoRemarks As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoRemarks = New GridViewTextBoxColumn()
        repoRemarks.FormatString = ""
        repoRemarks.HeaderText = "Remarks"
        repoRemarks.Name = colRemarks
        repoRemarks.Width = 150
        repoRemarks.ReadOnly = False
        gv1.MasterTemplate.Columns.Add(repoRemarks)


        gv1.AllowDeleteRow = True
        gv1.AllowAddNewRow = False
        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = False
        gv1.AllowRowReorder = False
        gv1.EnableSorting = False
        gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv1.MasterTemplate.ShowRowHeaderColumn = False
    End Sub

    Sub LoadBatchNo()
        Dim qry1 As String = " Select TSPL_MF_BATCH_ORDER.BO_CODE as [Code], DESCRIPTION as [Description],convert(varchar(12),BO_DATE,103) AS [Date], POSTED AS [Is Posted]," & _
                             " convert(varchar(12),POSTING_DATE,103) AS [Posting Date],APPROVED_BY AS [Approved By],'BO' as Type," & _
                             " BD.BATCH_QTY as [Batch Quantity],COALESCE(REC.Rec_qty,0) AS [Received Quantity] from TSPL_MF_BATCH_ORDER " & _
                             " INNER JOIN (select BO_CODE,SUM(BATCH_QTY) AS BATCH_QTY from TSPL_MF_BATCH_PP_DETAIL group by BO_CODE) AS BD ON TSPL_MF_BATCH_ORDER.BO_CODE=BD.BO_CODE " & _
                             " LEFT JOIN (select TSPL_MF_RECEIPT.BO_CODE,coalesce(sum(TSPL_MF_RECEIPT_DETAIL.RECEIPT_QTY),0) as Rec_qty  from TSPL_MF_RECEIPT " & _
                             " inner join TSPL_MF_RECEIPT_DETAIL on TSPL_MF_RECEIPT.RECEIPT_CODE=TSPL_MF_RECEIPT_DETAIL.RECEIPT_CODE " & _
                             " GROUP BY TSPL_MF_RECEIPT.BO_CODE) AS REC ON BD.BO_CODE=REC.BO_CODE " & _
                             " WHERE TSPL_MF_BATCH_ORDER.POSTED=1 AND BD.BATCH_QTY>coalesce(REC.Rec_qty,0)"

        cbgBatchNo.DataSource = clsDBFuncationality.GetDataTable(qry1)
        cbgBatchNo.ValueMember = "Code"
        cbgBatchNo.DisplayMember = "Description"
        cbgBatchNo.MyShowHeadrText = True
    End Sub

    Sub LoadMONo()
        Dim qry1 As String = " Select TSPL_MF_MANUFACTURING_ORDER.MO_CODE as [Code], DESCRIPTION as [Description],convert(varchar(12),MO_DATE,103) AS [Date], POSTED AS [Is Posted]," & _
        " convert(varchar(12),POSTING_DATE,103) AS [Posting Date],APPROVED_BY AS [Approved By],'MO' as Type, " & _
        " TSPL_MF_MANUFACTURING_ORDER.QTY_ORDERED_STOCK AS [Ordered Quantity],coalesce(REC.Rec_qty,0) as [Received Quantity] from TSPL_MF_MANUFACTURING_ORDER " & _
        " LEFT JOIN (select TSPL_MF_RECEIPT.MO_CODE,coalesce(sum(TSPL_MF_RECEIPT_DETAIL.RECEIPT_QTY),0) as Rec_qty  from TSPL_MF_RECEIPT " & _
        " inner join TSPL_MF_RECEIPT_DETAIL on TSPL_MF_RECEIPT.RECEIPT_CODE=TSPL_MF_RECEIPT_DETAIL.RECEIPT_CODE " & _
        " GROUP BY TSPL_MF_RECEIPT.MO_CODE) AS REC ON TSPL_MF_MANUFACTURING_ORDER.MO_CODE=REC.MO_CODE " & _
        " WHERE TSPL_MF_MANUFACTURING_ORDER.POSTED=1 and TSPL_MF_MANUFACTURING_ORDER.MO_STATUS NOT IN ('Closed')"

        cbgBatchNo.DataSource = clsDBFuncationality.GetDataTable(qry1)
        cbgBatchNo.ValueMember = "Code"
        cbgBatchNo.DisplayMember = "Description"
        cbgBatchNo.MyShowHeadrText = True
    End Sub

    Private Sub gv1_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellValueChanged
        Try
            If (Not isInsideLoadData) Then
                If Not isCellValueChangedOpen Then
                    isCellValueChangedOpen = True
                    If e.Column Is gv1.Columns(colICode) OrElse e.Column Is gv1.Columns(colReqQty) Then
                        If e.Column Is gv1.Columns(colICode) Then
                            OpenICodeList(False)
                        End If
                        If e.Column Is gv1.Columns(colReqQty) Then
                            If clsCommon.myCdbl(gv1.CurrentRow.Cells(colReqQty).Value) > clsCommon.myCdbl(gv1.CurrentRow.Cells(colBatchQty).Value) Then
                                clsCommon.MyMessageBoxShow("Requisition Quantity can not be grater then Batch Quantity.")
                                gv1.CurrentRow.Cells(colReqQty).Value = 0
                            End If
                        End If
                        setGridFocus()
                    End If
                    isCellValueChangedOpen = False
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        Finally
        End Try
    End Sub

    Sub OpenICodeList(ByVal isButtonClick As Boolean)
        Dim obj As clsItemMaster = clsItemMaster.FinderForItem(clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value), "cboItemType.SelectedValue", True, isButtonClick, "", "")
        If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Item_Code) > 0 Then
            gv1.CurrentRow.Cells(colICode).Value = obj.Item_Code
            gv1.CurrentRow.Cells(colIName).Value = obj.Item_Desc
            gv1.CurrentRow.Cells(colUnit).Value = obj.Unit_Code
        Else
            gv1.CurrentRow.Cells(colICode).Value = ""
            gv1.CurrentRow.Cells(colIName).Value = ""
            gv1.CurrentRow.Cells(colUnit).Value = ""
        End If
    End Sub

    Private Sub setGridFocus()
        Dim intCurrRow As Integer = gv1.CurrentRow.Index
        If clsCommon.myLen(gv1.CurrentRow.Cells(colICode).Value) > 0 Then
            If gv1.CurrentColumn Is gv1.Columns(colICode) Then
                gv1.CurrentRow = gv1.Rows(intCurrRow)
                gv1.CurrentColumn = gv1.Columns(colReqQty)
            ElseIf gv1.CurrentColumn Is gv1.Columns(colRemarks) Then
                gv1.CurrentRow = gv1.Rows(intCurrRow + 1)
            End If
        End If
    End Sub

    Private Sub gv1_UserAddedRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowEventArgs) Handles gv1.UserAddedRow
        For i As Integer = 0 To gv1.Rows.Count - 1
            gv1.Rows(0).Cells(0).Value = 1
            If i <> 0 Then
                gv1.Rows(i).Cells(colLineNo).Value = i + 1
            End If
        Next
    End Sub

    Private Sub btnAddNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddNew.Click
        AddNew()
    End Sub

    Sub AddNew()
        '' get mo setting
        MOActive = ClsMFSeetings.Get_MO_BO_Setting()
        If MOActive = True Then
            Me.RadGroupBox1.Text = "MO NO."
        Else
            Me.RadGroupBox1.Text = "BO NO."
        End If
        BlankAllControls()
        LoadBlankGrid()
        LoadBatchNo()
        gv1.Rows.AddNew()
        isNewEntry = True
        btnSave.Text = "Save"
        btnSave.Enabled = True
        btnPost.Enabled = True
        btnDelete.Enabled = True
        UsLock1.Status = ERPTransactionStatus.Pending
        txtDate.Focus()
        txtLocation.Enabled = True
    End Sub

    Function AllowToSave() As Boolean
        Try
            '===================Added by preeti Gupta==============
            If AllowFutureDateTransaction(txtDate.Value, Nothing) = False Then
                txtDate.Select()
                Return False
            End If
            '===========================================================
            If btnSave.Text = "Update" Then
                Dim strchk As String = " select POSTED from TSPL_MF_REQUISITION where REQ_CODE='" + txtReqNo.Value + "' and ISUSED = 1 "
                Dim chkpost As String = clsDBFuncationality.getSingleValue(strchk)
                If chkpost = "1" Then
                    clsCommon.MyMessageBoxShow("Transection already posted")
                    Return False
                End If
            End If

            If clsCommon.myLen(txtLocation.Value) <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please select Location.")
                txtLocation.Focus()
                Return False
            End If
            If clsCommon.myLen(txtRequestBy.Value) <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please select Request By.")
                txtRequestBy.Focus()
                Return False
            End If

            Dim arrICode As New List(Of String)()
            For ii As Integer = 0 To gv1.Rows.Count - 1
                Dim strICode As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colICode).Value)
                Dim strIName As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colIName).Value)
                If clsCommon.myLen(strICode) > 0 Then
                    'For jj As Integer = 0 To gv1.Rows.Count - 1
                    '    If (ii = jj) Then
                    '        Continue For
                    '    End If
                    '    If (clsCommon.CompairString(strICode, clsCommon.myCstr(gv1.Rows(jj).Cells(colICode).Value)) = CompairStringResult.Equal) Then
                    '        common.clsCommon.MyMessageBoxShow("Already selected Item " + strICode.Trim() + "( " + strIName.Trim() + " ) At Line No: " + clsCommon.myCstr(clsCommon.myCdbl(ii + 1)) + " and  " + clsCommon.myCstr(clsCommon.myCdbl(jj + 1)) + "")
                    '        Return False
                    '    End If
                    'Next
                    If clsCommon.myCdbl(gv1.Rows(ii).Cells(colReqQty).Value) > clsCommon.myCdbl(gv1.Rows(ii).Cells(colBatchQty).Value) Then
                        clsCommon.MyMessageBoxShow("Requisition Quantity can not be grater then Batch Quantity in Row No " + (ii + 1).ToString())
                        Return False
                    End If
                    If Not arrICode.Contains(strICode) Then
                        arrICode.Add(strICode)
                    End If
                End If
            Next

            If arrICode.Count < 1 Then
                common.clsCommon.MyMessageBoxShow("Please Choose Batch Items. ")
                txtLocation.Focus()
                Return False
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
            Return False
        End Try
        Return True
    End Function

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        SaveData(False)
    End Sub

    Sub SaveData(ByVal ChekBtnPost As Boolean)
        Try
            If (AllowToSave()) Then
                Dim obj As New clsProductionRequisition()
                obj.REQ_CODE = txtReqNo.Value
                obj.REQ_DATE = txtDate.Value
                If txtExpireDate.Checked Then
                    obj.EXP_DATE = txtExpireDate.Value
                End If
                obj.DESCRIPTION = txtDesc.Text
                obj.LOCATION_CODE = txtLocation.Value
                obj.COMMENTS = txtComment.Text
                obj.REQUESTED_BY = txtRequestBy.Value
                If MOActive = True Then
                    obj.TR_TYPE = "MO"
                Else
                    obj.TR_TYPE = "BO"
                End If

                obj.ObjList = New List(Of clsProductionRequisitionDetail)
                For Each grow As GridViewRowInfo In gv1.Rows
                    If (clsCommon.myLen(grow.Cells(colICode).Value) > 0) Then
                        Dim objTr As New clsProductionRequisitionDetail()

                        objTr.TR_TYPE = obj.TR_TYPE
                        If obj.TR_TYPE = "BO" Then
                            objTr.BO_CODE = clsCommon.myCstr(grow.Cells(colBatchNo).Value)
                            objTr.MO_CODE = ""
                        Else
                            objTr.BO_CODE = ""
                            objTr.MO_CODE = clsCommon.myCstr(grow.Cells(colBatchNo).Value)
                        End If

                        objTr.ITEM_CODE = clsCommon.myCstr(grow.Cells(colICode).Value)
                        objTr.PRODUCTION_LINE_CODE = clsCommon.myCstr(grow.Cells(colPLCode).Value)
                        objTr.BOM_CODE = clsCommon.myCstr(grow.Cells(colBOMCode).Value)
                        objTr.ITEM_DESCRIPTION = clsCommon.myCstr(grow.Cells(colIName).Value)
                        objTr.BATCH_QTY = clsCommon.myCdbl(grow.Cells(colBatchQty).Value)
                        objTr.REQ_QTY = clsCommon.myCdbl(grow.Cells(colReqQty).Value)
                        objTr.UNIT_CODE = clsCommon.myCstr(grow.Cells(colUnit).Value)
                        objTr.REMARKS = clsCommon.myCstr(grow.Cells(colRemarks).Value)


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
                    LoadData(obj.REQ_CODE, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Sub LoadData(ByVal strDocumentNo As String, ByVal navType As common.NavigatorType)
        Try
            isInsideLoadData = True
            isNewEntry = False
            Dim obj As New clsProductionRequisition()
            obj = obj.GetData(strDocumentNo, navType)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.REQ_CODE) > 0) Then
                If obj.TR_TYPE = "MO" Then
                    MOActive = True
                Else
                    MOActive = False
                End If

                BlankAllControls()
                LoadBlankGrid()
                If obj.POSTED Then
                    btnSave.Enabled = True
                    btnPost.Enabled = True
                    btnDelete.Enabled = False
                    UsLock1.Status = ERPTransactionStatus.Approved
                    btnSave.Text = "Update"
                    If obj.ISUSED Then
                        btnSave.Enabled = False
                        btnPost.Enabled = False
                    End If
                Else
                    btnSave.Enabled = True
                    btnDelete.Enabled = True
                    btnPost.Enabled = True
                    UsLock1.Status = ERPTransactionStatus.Pending
                    btnSave.Text = "Update"
                End If
                CheckedBatchNo(obj.REQ_CODE, obj.TR_TYPE)
                txtReqNo.Value = obj.REQ_CODE
                txtDate.Value = obj.REQ_DATE
                UsLock1.Status = obj.POSTED
                txtExpireDate.Checked = IIf(obj.EXP_DATE.Year < 2000, False, True)
                If txtExpireDate.Checked Then
                    txtExpireDate.Value = obj.EXP_DATE
                End If
                txtDesc.Text = obj.DESCRIPTION
                txtLocation.Value = obj.LOCATION_CODE
                lblLocation.Text = obj.LOCATION_NAME
                txtComment.Text = obj.COMMENTS
                txtRequestBy.Value = obj.REQUESTED_BY
                lblRequestedBy.Text = obj.REQUESTED_BY_NAME
                For Each objTr As clsProductionRequisitionDetail In obj.ObjList
                    gv1.Rows.AddNew()
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = gv1.Rows.Count

                    If MOActive = True Then
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colBatchNo).Value = objTr.MO_CODE
                    Else
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colBatchNo).Value = objTr.BO_CODE
                    End If
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value = objTr.ITEM_CODE
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colPLCode).Value = objTr.PRODUCTION_LINE_CODE
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colBOMCode).Value = objTr.BOM_CODE
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colIName).Value = objTr.ITEM_DESCRIPTION
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colBatchQty).Value = objTr.BATCH_QTY
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colReqQty).Value = objTr.REQ_QTY
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colUnit).Value = objTr.UNIT_CODE
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colRemarks).Value = objTr.REMARKS
                Next
                If obj.POSTED = ERPTransactionStatus.Pending Then
                    ' gv1.Rows.AddNew()
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        Finally
            isInsideLoadData = False
        End Try
    End Sub

    Sub CheckedBatchNo(ByVal strCode As String, ByVal TR_TYPE As String)
        Dim qry As String = ""
        If TR_TYPE = "BO" Then
            qry = " Select BO_CODE as [Code], DESCRIPTION as [Description],convert(varchar(12),BO_DATE,103) AS [Date], POSTED AS [Is Posted], " & _
            " convert(varchar(12),POSTING_DATE,103) AS [Posting Date],APPROVED_BY AS [Approved By],'BO' as Type from TSPL_MF_BATCH_ORDER " & _
            " WHERE BO_CODE IN (Select distinct BO_CODE from TSPL_MF_REQ_DETAIL where REQ_CODE ='" + strCode + "' ) "
        Else
            qry = " Select MO_CODE as [Code], DESCRIPTION as [Description],convert(varchar(12),MO_DATE,103) AS [Date], POSTED AS [Is Posted], " & _
            " convert(varchar(12),POSTING_DATE,103) AS [Posting Date],APPROVED_BY AS [Approved By],'MO' as Type from TSPL_MF_MANUFACTURING_ORDER " & _
            " WHERE MO_CODE IN (Select distinct MO_CODE from TSPL_MF_REQ_DETAIL where REQ_CODE ='" + strCode + "' ) "
        End If
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        Dim lst As New ArrayList
        For Each dr As DataRow In dt.Rows
            lst.Add(clsCommon.myCstr(dr("Code")))
        Next
        cbgBatchNo.DataSource = dt
        cbgBatchNo.ValueMember = "Code"
        cbgBatchNo.DisplayMember = "Description"
        cbgBatchNo.MyShowHeadrText = True
        cbgBatchNo.CheckedValue = lst
    End Sub

    Private Sub RadButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        CloseForm()
    End Sub

    Sub CloseForm()
        Me.Close()
    End Sub

    Private Sub btnPost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPost.Click
        PostData()
    End Sub

    Sub PostData()
        Try
            If (myMessages.postConfirm()) Then
                If (clsProductionRequisition.PostData(txtReqNo.Value, True)) Then
                    common.clsCommon.MyMessageBoxShow("Data Posted Successfully ")
                    LoadData(txtReqNo.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        DeleteData()
    End Sub

    Sub DeleteData()
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
                If (clsProductionRequisition.DeleteData(txtReqNo.Value)) Then
                    saveCancelLog(Reason, "Delete", Nothing)
                    common.clsCommon.MyMessageBoxShow("Data Deleted Successfully ")
                    AddNew()
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Function saveCancelLog(ByVal Reason As String, ByVal Activity_Type As String, Optional ByVal trans As System.Data.SqlClient.SqlTransaction = Nothing) As Boolean
        Dim obj As New clsCancelLog
        obj.Program_Code = Form_ID
        obj.DOCUMENT_NO = clsCommon.myCstr(Me.txtReqNo.Value)
        obj.REASON = Reason
        obj.ACTIVITY_TYPE = Activity_Type
        Return clsCancelLog.SaveData(obj, True, trans)
    End Function

    Private Sub txtDocNo__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtReqNo._MYNavigator
        Try
            Dim qst As String = " select count(*) from TSPL_MF_REQUISITION "
            Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qst))
            If count = 0 Then
                txtReqNo.MyReadOnly = False
            Else
                txtReqNo.MyReadOnly = True
            End If
            LoadData(txtReqNo.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub txtDocNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtReqNo._MYValidating

        Dim str As String = "select count(*) from TSPL_MF_REQUISITION where REQ_CODE ='" + txtReqNo.Value + "' "
        Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
        'clsDBFuncationality.connectionString
        If no = 0 AndAlso isButtonClicked = False Then
            txtReqNo.MyReadOnly = False
        Else
            txtReqNo.MyReadOnly = True
        End If
        If txtReqNo.MyReadOnly OrElse isButtonClicked Then

            Dim qry As String = "select REQ_CODE as [Code],REQ_DATE as [Date], EXP_DATE as [Exp. Date],DESCRIPTION as [Description], (case when POSTED = '0' then 'Pending' else 'Approved' end ) as [Posted],REQUESTED_BY as [Requested By],COMMENTS as [Comments],LOCATION_CODE as [Location Code] from TSPL_MF_REQUISITION"
            txtReqNo.Value = clsCommon.ShowSelectForm("TSPL_MF_REQUISITION", qry, "Code", "", txtReqNo.Value, "Code", isButtonClicked)
            If txtReqNo.Value <> "" Then
                LoadData(txtReqNo.Value, NavigatorType.Current)
            Else
                AddNew()
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

    Private Sub frmProductionRequisition_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.F2 AndAlso gv1.CurrentCell IsNot Nothing Then
            isCellValueChangedOpen = True
            If gv1.CurrentColumn Is gv1.Columns(colICode) Then
                gv1.CurrentColumn = gv1.Columns(colIName)
                OpenICodeList(True)
                gv1.CurrentColumn = gv1.Columns(colICode)
            End If
            setGridFocus()
            isCellValueChangedOpen = False
        ElseIf e.Alt AndAlso e.KeyCode = Keys.N Then
            AddNew()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnSave.Enabled Then
            SaveData(False)
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag AndAlso btnPost.Enabled Then
            PostData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btnDelete.Enabled Then
            DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnClose.Enabled Then
            CloseForm()
        End If
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        If txtReqNo.Value = "" Then
            myMessages.blankValue(Me, "Requisition Number", Me.Text)
        Else
            funPrint()
        End If
    End Sub

    Private Sub funPrint()
        Try
            Dim qry As String = " select TSPL_MF_REQUISITION.REQ_CODE as [REQ_CODE], convert(varchar,TSPL_MF_REQUISITION.REQ_DATE,103) as [REQ_DATE], (case when year(TSPL_MF_REQUISITION.EXP_DATE)>1 then convert(varchar,TSPL_MF_REQUISITION.EXP_DATE,103) else '' end) as [EXP_DATE],TSPL_MF_REQUISITION.Description as [Description], TSPL_MF_REQUISITION.COMMENTS as [COMMENTS], "
            'qry += " TSPL_MF_REQUISITION.REQUESTED_BY,TSPL_MF_REQUISITION.LOCATION_CODE,"
            qry += " TSPL_MF_REQ_DETAIL.ITEM_CODE as [ITEM_CODE], TSPL_MF_REQ_DETAIL.ITEM_DESCRIPTION as [ITEM_DESCRIPTION],TSPL_MF_REQ_DETAIL.BATCH_QTY as [BATCH_QTY], TSPL_MF_REQ_DETAIL.REMARKS as [Remarks], TSPL_MF_REQ_DETAIL.UNIT_CODE as [UNIT_CODE], TSPL_MF_REQ_DETAIL.REQ_QTY as [REQ_QTY], TSPL_MF_REQ_DETAIL.BO_CODE as [BO_CODE],"
            qry += " TSPL_EMPLOYEE_MASTER.Emp_Name as [REQUESTED_USER],TSPL_LOCATION_MASTER.Location_Desc as [Location_Desc], '' as [AuthorizeBy] "
            qry += " from TSPL_MF_REQUISITION join TSPL_MF_REQ_DETAIL on TSPL_MF_REQUISITION.REQ_CODE =TSPL_MF_REQ_DETAIL.REQ_CODE "
            qry += " left outer join TSPL_EMPLOYEE_MASTER on TSPL_EMPLOYEE_MASTER.EMP_CODE =TSPL_MF_REQUISITION.REQUESTED_BY "
            qry += " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code =TSPL_MF_REQUISITION.LOCATION_CODE"
            qry += " where 2=2 "
            If txtReqNo.Value <> "" Then
                qry += " and  TSPL_MF_REQUISITION.REQ_CODE = '" + txtReqNo.Value + "' "
            End If
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            Dim frmCRV As New frmCrystalReportViewer()
            frmCRV.funreport(CrystalReportFolder.PRODUCTION, dt, "ProductionRequisition", "Production Requisition")
            frmCRV = Nothing
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub gv1_UserDeletingRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs) Handles gv1.UserDeletingRow
        If common.clsCommon.MyMessageBoxShow("Delete The Current Row." + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
            e.Cancel = True
        End If
    End Sub

    Private Sub gv1_UserDeletedRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowEventArgs) Handles gv1.UserDeletedRow
        For ii As Integer = 1 To gv1.Rows.Count
            gv1.Rows(ii - 1).Cells(colLineNo).Value = ii
        Next
    End Sub

    Private Sub gv1_CurrentColumnChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gv1.CurrentColumnChanged
        If gv1.RowCount > 0 Then
            'Dim intCurrRow As Integer = gv1.CurrentRow.Index
            'gv1.CurrentRow.Cells(colLineNo).Value = clsCommon.myCdbl(intCurrRow + 1)
            'If intCurrRow = gv1.Rows.Count - 1 Then
            '    gv1.Rows.AddNew()
            '    gv1.CurrentRow = gv1.Rows(intCurrRow)
            'End If
        End If
    End Sub

    'Public Sub SendMail()
    '    Try
    '        If Not objCommonVar.IsMailSend Then
    '            Exit Sub
    '        End If
    '        Dim ArrReceivers As New List(Of String)
    '        Dim ArrUsers As New List(Of String)
    '        Dim no As Integer = 0
    '        Dim qry As String = "select TSPL_REQUISITION_HEAD.Requisition_Id ,TSPL_REQUISITION_HEAD.Requisition_Date ,TSPL_REQUISITION_HEAD.Expire_Date ,TSPL_REQUISITION_HEAD.Require_Date ,TSPL_REQUISITION_HEAD.Ref_No ,TSPL_REQUISITION_HEAD.Description,TSPL_REQUISITION_HEAD.Remarks,TSPL_REQUISITION_HEAD.Request_By ,TSPL_REQUISITION_DETAIL.Item_Code ,TSPL_REQUISITION_DETAIL.Item_Desc ,TSPL_REQUISITION_DETAIL.Unit_Code ,TSPL_REQUISITION_DETAIL.Requisition_Qty,(select SUM(Item_Qty)from TSPL_ITEM_LOCATION_DETAILS where Item_Code=TSPL_REQUISITION_DETAIL.Item_Code)as AvaiQty  ,TSPL_VENDOR_MASTER.Vendor_Name,TSPL_REQUISITION_HEAD.Comments ,TSPL_COMPANY_MASTER.Comp_Name ,TSPL_COMPANY_MASTER.Logo_Img ,TSPL_COMPANY_MASTER.Logo_Img2,user1.User_Name as CreatedBy,TSPL_REQUISITION_HEAD.Approvel_Level_Required"
    '        qry += " , case when TSPL_REQUISITION_HEAD.Status =1 then  user2.User_Name else '' end  as AuthorizeBy ,"
    '        qry += " TSPL_REQUISITION_HEAD.Request_By,TSPL_REQUISITION_HEAD.Require_Date,TSPL_REQUISITION_HEAD.Dept_Desc,TSPL_REQUISITION_HEAD.Location ,TSPL_COMPANY_MASTER.Add1  from TSPL_REQUISITION_HEAD join TSPL_REQUISITION_DETAIL on TSPL_REQUISITION_HEAD.Requisition_Id =TSPL_REQUISITION_DETAIL.Requisition_Id left outer join TSPL_COMPANY_MASTER on  TSPL_REQUISITION_HEAD.Comp_Code = TSPL_COMPANY_MASTER.Comp_Code left outer join TSPL_VENDOR_MASTER on TSPL_REQUISITION_DETAIL.Vendor_Code =TSPL_VENDOR_MASTER.Vendor_Code left outer join TSPL_USER_MASTER as user1 on TSPL_REQUISITION_HEAD.Created_By=user1.User_Code left outer join TSPL_USER_MASTER as user2 on TSPL_REQUISITION_HEAD.Modify_By=user2.User_Code   where(2 = 2)"
    '        qry += " and  TSPL_REQUISITION_HEAD.Requisition_Id='" + txtReqNo.Value + "'"
    '        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
    '        Dim ReqNo As String = dt.Rows(0)("Requisition_Id").ToString
    '        Dim Reqdate As String = dt.Rows(0)("Requisition_Date").ToString
    '        For i As Integer = 0 To dt.Rows.Count - 1
    '            If dt.Rows(i)("vendor_name").ToString() <> "" Then
    '                no = no + 1
    '            End If
    '        Next
    '        If dt.Rows(0)("Approvel_Level_Required").ToString <> "" Then
    '            Dim level As String = dt.Rows(0)("Approvel_Level_Required").ToString
    '            Dim Query As String = "select E_mail,user_code from TSPL_USER_MASTER  where level <=" + level + " "
    '            Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(Query)
    '            For Each dr As DataRow In dt1.Rows
    '                If clsCommon.myLen(dr("E_mail")) > 0 Then
    '                    ArrReceivers.Add(clsCommon.myCstr(dr("E_mail")))
    '                    ArrUsers.Add(clsCommon.myCstr(dr("user_code")))
    '                End If
    '            Next
    '        End If
    '        Dim strreportPath As String = ""
    '        Dim frmCRV As New frmCrystalReportViewer()
    '        strreportPath = frmCRV.funreport1(CrystalReportFolder.PurchaseOrder, dt, "PurchaseRequisition", "Purchase Requisition")
    '        frmCRV = Nothing
    '        If ArrReceivers.Count > 0 Then
    '            sendEMailThroughOUTLOOK(strreportPath, ArrReceivers, ArrUsers, ReqNo, Reqdate)
    '            clsCommon.MyMessageBoxShow("Mail has been sent succcessfully.")
    '        Else
    '            Throw New Exception("No Recipients found to mail.")
    '        End If
    '    Catch ex As Exception
    '        clsCommon.MyMessageBoxShow(ex.Message)
    '    End Try
    'End Sub
    'Public Sub sendEMailThroughOUTLOOK(ByVal strPath As String, ByVal arrReceivers As List(Of String), ByVal arrUsers As List(Of String), ByVal ReqNo As String, ByVal ReqDate As String)
    '    Try
    '        If Process.GetProcessesByName("OutLook").Length < 1 Then
    '            Process.Start("OutLook.exe")
    '        End If
    '        Dim oApp As New Outlook.Application()
    '        Dim oMsg As Outlook.MailItem = DirectCast(oApp.CreateItem(Outlook.OlItemType.olMailItem), Outlook.MailItem)
    '        Dim sDisplayName As [String] = "MyAttachment"
    '        oMsg.Subject = "Approval required for Requisition No:" + ReqNo + " dated :" + clsCommon.GetPrintDate(ReqDate, "dd/MMM/yyyy") + ""

    '        oMsg.Body = "Please find the attached Requisition No:" + ReqNo + " dated :" + clsCommon.GetPrintDate(ReqDate, "dd/MMM/yyyy") + " for your kind approval." & vbCrLf & "" & vbCrLf & "Best Regards" & vbCrLf & "" + objCommonVar.CurrentUser + ""
    '        Dim iPosition As Integer = CInt(oMsg.Body.Length) + 1
    '        Dim iAttachType As Integer = CInt(Outlook.OlAttachmentType.olByValue)

    '        If clsCommon.myLen(strPath) > 0 Then
    '            Dim oAttach As Outlook.Attachment = oMsg.Attachments.Add(strPath, iAttachType, iPosition, sDisplayName)
    '        End If
    '        For ii As Integer = 0 To arrReceivers.Count - 1
    '            oMsg.Recipients.Add(arrReceivers(ii))
    '        Next
    '        oMsg.Send()
    '        oMsg = Nothing
    '        oApp = Nothing
    '    Catch ex As Exception
    '        Throw New Exception(ex.Message)
    '    End Try
    'End Sub

    Private Sub txtRequestBy__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtRequestBy._MYValidating
        Try
            Dim OBJEMP As New clsEmployeeMaster
            OBJEMP = clsEmployeeMaster.FinderForEmployee(Me.txtRequestBy.Value, isButtonClicked)
            If Not OBJEMP Is Nothing Then
                Me.txtRequestBy.Value = OBJEMP.EMP_CODE
                Me.lblRequestedBy.Text = OBJEMP.Emp_Name
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub btnShowItems_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShowItems.Click
        Dim qry As String = ""
        If MOActive Then
            qry = " SELECT T1.MO_CODE as BO_CODE,T1.CONSM_ITEM_CODE as ITEM_CODE,T1.CONSM_ITEM_UNIT_CODE as UNIT_CODE,T1.CONSM_QUANTITY AS QTY, " & _
                " TSPL_MF_MANUFACTURING_ORDER.PRODUCTION_AREA AS PRODUCTION_LINE_CODE,TSPL_MF_MANUFACTURING_ORDER.BOM_CODE,T2.Item_Desc  " & _
                " from TSPL_MF_MANUFACTURING_ORDER left join TSPL_MF_MO_MATERIAL T1 on TSPL_MF_MANUFACTURING_ORDER.MO_CODE=T1.MO_CODE  " & _
                " LEFT OUTER JOIN TSPL_ITEM_MASTER T2 ON T2.ITEM_CODE= T1.CONSM_ITEM_CODE  " & _
                " WHERE TSPL_MF_MANUFACTURING_ORDER.MO_CODE IN (" + clsCommon.GetMulcallString(cbgBatchNo.CheckedValue) + ")"
        Else
            qry = " SELECT T1.BO_CODE,T1.ITEM_CODE,T1.UNIT_CODE,T1.QTY,T1.PRODUCTION_LINE_CODE,T1.BOM_CODE,T2.Item_Desc FROM TSPL_MF_BATCH_ORDER_DETAIL T1 "
            qry += " LEFT OUTER JOIN TSPL_ITEM_MASTER T2 ON T2.ITEM_CODE= T1.ITEM_CODE "
            qry += " WHERE T1.BO_CODE IN (" + clsCommon.GetMulcallString(cbgBatchNo.CheckedValue) + ")"
        End If
        
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

        gv1.DataSource = Nothing
        LoadBlankGrid()
        Dim Count As Int16 = 0
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            For Each DR As DataRow In dt.Rows
                gv1.Rows.AddNew()
                Count = Count + 1
                gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = clsCommon.myCstr(Count)
                gv1.Rows(gv1.Rows.Count - 1).Cells(colBatchNo).Value = clsCommon.myCstr(DR("BO_CODE"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value = clsCommon.myCstr(DR("ITEM_CODE"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colPLCode).Value = clsCommon.myCstr(DR("PRODUCTION_LINE_CODE"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colBOMCode).Value = clsCommon.myCstr(DR("BOM_CODE"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colIName).Value = clsCommon.myCstr(DR("Item_Desc"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colBatchQty).Value = clsCommon.myCdbl(DR("QTY"))
                'gv1.Rows(gv1.Rows.Count - 1).Cells(colReqQty).Value = clsCommon.myCstr(DR("ITEM_CODE"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colUnit).Value = clsCommon.myCstr(DR("UNIT_CODE"))
                'gv1.Rows(gv1.Rows.Count - 1).Cells(colRemarks).Value = clsCommon.myCstr(DR("ITEM_CODE"))
            Next
        Else
            clsCommon.MyMessageBoxShow("No Data Found to display.")
        End If
        ' gv1.Rows.AddNew()
    End Sub
End Class
