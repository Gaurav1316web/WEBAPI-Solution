Imports common
Imports System.Data.SqlClient
Imports System.IO

Public Class frmMRPAutoMobile
    Inherits FrmMainTranScreen

#Region "variables"
    Const colLineno As String = "lineno"
    Const colItem_Code As String = "colItem_Code"
    Const colItem_Desc As String = "colItem_Desc"
    Const colUNIT_CODE As String = "UNIT_CODE"
    Const colminqty As String = "minqty"
    Const colmaxqty As String = "maxqty"
    Const colrol As String = "ROLqty"
    Const colRequiredQty As String = "ReqQty" 'bom
    Const colstockqty As String = "stockqty"
    Const colPO_Qty As String = "colPO_Qty"
    Const colQC_Qty As String = "colQC_Qty"
    Const colGrossQty As String = "colGrossQty"
    Const colNetRequiredQty As String = "colNetRequiredQty"
    Const colExtraaddqty As String = "extraAddqty"
    Const colremarks As String = "remarks"

    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim isNewEntry As Boolean = True
    Private isCellValueChangedOpen As Boolean = False
    Dim isInsideLoaddata As Boolean = False
    Dim Errorcontrol As New clsErrorControl()
#End Region

    Sub LoadMRPDetailGrid()
        gvMRPDetal.Columns.Clear()
        gvMRPDetal.Rows.Clear()

        Dim Item_Code As New GridViewTextBoxColumn
        Dim qty As New GridViewDecimalColumn

        Item_Code = New GridViewTextBoxColumn()
        Item_Code.FormatString = ""
        Item_Code.HeaderText = "S.No."
        Item_Code.Name = colLineno
        Item_Code.Width = 70
        Item_Code.ReadOnly = True
        Item_Code.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvMRPDetal.Columns.Add(Item_Code)

        Item_Code = New GridViewTextBoxColumn()
        Item_Code.FormatString = ""
        Item_Code.HeaderText = "Item Code"
        Item_Code.Name = colItem_Code
        Item_Code.Width = 100
        Item_Code.ReadOnly = True
        Item_Code.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvMRPDetal.Columns.Add(Item_Code)

        Item_Code = New GridViewTextBoxColumn()
        Item_Code.FormatString = ""
        Item_Code.HeaderText = "Item Description"
        Item_Code.Name = colItem_Desc
        Item_Code.Width = 220
        Item_Code.ReadOnly = True
        Item_Code.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvMRPDetal.Columns.Add(Item_Code)

        Item_Code = New GridViewTextBoxColumn()
        Item_Code.FormatString = ""
        Item_Code.HeaderText = "UOM"
        Item_Code.Name = colUNIT_CODE
        Item_Code.Width = 80
        Item_Code.ReadOnly = True
        Item_Code.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvMRPDetal.Columns.Add(Item_Code)

        qty = New GridViewDecimalColumn()
        qty.FormatString = ""
        qty.HeaderText = "Min. Qty"
        qty.Name = colminqty
        qty.Width = 80
        qty.ReadOnly = True
        qty.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvMRPDetal.Columns.Add(qty)

        qty = New GridViewDecimalColumn()
        qty.FormatString = ""
        qty.HeaderText = "Max. Qty"
        qty.Name = colmaxqty
        qty.Width = 80
        qty.ReadOnly = True
        qty.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvMRPDetal.Columns.Add(qty)

        qty = New GridViewDecimalColumn()
        qty.FormatString = ""
        qty.HeaderText = "Reorder Qty"
        qty.Name = colrol
        qty.Width = 80
        qty.ReadOnly = True
        qty.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvMRPDetal.Columns.Add(qty)

        qty = New GridViewDecimalColumn()
        qty.FormatString = ""
        qty.HeaderText = "Required Qty"
        qty.Name = colRequiredQty
        qty.Width = 80
        qty.ReadOnly = False
        qty.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvMRPDetal.Columns.Add(qty)

        qty = New GridViewDecimalColumn()
        qty.FormatString = ""
        qty.HeaderText = "Stock Qty"
        qty.Name = colstockqty
        qty.Width = 80
        qty.ReadOnly = True
        qty.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvMRPDetal.Columns.Add(qty)

        qty = New GridViewDecimalColumn()
        qty.FormatString = ""
        qty.HeaderText = "Pending PO Qty"
        qty.Name = colPO_Qty
        qty.Width = 80
        qty.ReadOnly = True
        qty.WrapText = True
        qty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvMRPDetal.Columns.Add(qty)

        qty = New GridViewDecimalColumn()
        qty.FormatString = ""
        qty.HeaderText = "Pending QC Qty"
        qty.Name = colQC_Qty
        qty.Width = 80
        qty.ReadOnly = True
        qty.WrapText = True
        qty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvMRPDetal.Columns.Add(qty)

        qty = New GridViewDecimalColumn()
        qty.FormatString = ""
        qty.HeaderText = "Extra Qty"
        qty.Name = colExtraaddqty
        qty.Width = 80
        qty.WrapText = True
        qty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvMRPDetal.Columns.Add(qty)

        qty = New GridViewDecimalColumn()
        qty.FormatString = ""
        qty.HeaderText = "Gross Required Qty"
        qty.Name = colGrossQty
        qty.Width = 80
        qty.ReadOnly = True
        qty.WrapText = True
        qty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvMRPDetal.Columns.Add(qty)

        qty = New GridViewDecimalColumn()
        qty.FormatString = ""
        qty.HeaderText = "Net Required Qty"
        qty.Name = colNetRequiredQty
        qty.Width = 80
        qty.ReadOnly = True
        qty.WrapText = True
        qty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvMRPDetal.Columns.Add(qty)

        Item_Code = New GridViewTextBoxColumn()
        Item_Code.FormatString = ""
        Item_Code.HeaderText = "Remarks"
        Item_Code.Name = colremarks
        Item_Code.Width = 100
        Item_Code.MaxLength = 100
        gvMRPDetal.Columns.Add(Item_Code)

        gvMRPDetal.AllowDeleteRow = True
        gvMRPDetal.AllowAddNewRow = False
        gvMRPDetal.ShowGroupPanel = False
        gvMRPDetal.AllowColumnReorder = True
        gvMRPDetal.AllowRowReorder = False
        gvMRPDetal.EnableSorting = False
        gvMRPDetal.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gvMRPDetal.MasterTemplate.ShowRowHeaderColumn = False
        gvMRPDetal.Rows.AddNew()

        Item_Code = Nothing
        qty = Nothing
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
        clsERPFuncationality.closeForm(Me)
    End Sub

    Private Sub frmMRPAutoMobile_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        LoadMRPDetailGrid()
        isNewEntry = True

        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(btnPost, "Press Alt+P for  Post")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D  for Delete ")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnnew, "Press Alt+N Adding New ")

        funReset()
        btnUnpost.Visible = False
        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
    End Sub

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmMRPAutoMobile)
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
        Dim serverdate As Date = clsCommon.GETSERVERDATE(Nothing)
        txtCode.MyReadOnly = False
        txtCode.Value = Nothing
        dtpMRPDate.Value = serverdate
        txtMRPDescription.Text = ""
        Me.txtDescription.Text = ""
        Me.fndProductionPlan.Value = Nothing
        Me.txtProductionPlanDesc.Text = ""
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

        UcAttachment1.Form_ID = MyBase.Form_ID
        UcAttachment1.BlankAllControls()

        btnsave.Text = "Save"
        btnsave.Enabled = True
        btndelete.Enabled = False
        btnPost.Enabled = False

        fndLocation.Enabled = False
        dtpFromDate.Enabled = False
        dtpToDate.Enabled = False

        txtMRPDescription.Focus()
        txtMRPDescription.Select()
    End Sub

    Private Sub txtCode__MYNavigator(ByVal sender As Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType)
        Try
            LoadData(txtCode.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Dim obj As New clsMRPAutoMobile()
        Try
            funReset()
            isInsideLoaddata = False
            isNewEntry = True

            obj = clsMRPAutoMobile.GetData(strCode, NavTyep)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.MRP_CODE) > 0) Then
                isNewEntry = False
                isInsideLoaddata = True
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


                Me.txtCode.Value = obj.MRP_CODE
                Me.dtpMRPDate.Value = obj.MRP_DATE
                Me.dtpFromDate.Value = obj.MRP_FROM
                Me.dtpToDate.Value = obj.MRP_TO
                Me.txtMRPDescription.Text = obj.MRP_DESCRIPTION
                Me.txtDescription.Text = obj.MRP_REMARKS
                Me.fndLocation.Value = obj.MRP_Location
                lblLocationDesc.Text = obj.Location_Desc
                Me.fndProductionPlan.Value = obj.PROD_PLAN_CODE
                txtProductionPlanDesc.Text = obj.PROD_PLAN_DESC

                chkStock.Checked = IIf(obj.Include_Stock = 1, True, False)
                chkPendignQC.Checked = IIf(obj.Include_Pending_QC = 1, True, False)
                chkPendingPO.Checked = IIf(obj.Include_Pending_PO = 1, True, False)
                chkItemLevel.Checked = IIf(obj.Include_Item_Level = 1, True, False)
                chkAutoPO.IsChecked = IIf(obj.Auto_PO = 1, True, False)
                chkAutoIndent.IsChecked = IIf(obj.Auto_Indent = 1, True, False)

                chkConsiderOpenPO.Checked = IIf(obj.Consider_Open_PO = 1, True, False)
                chkGenAutoSchedule.Checked = IIf(obj.Auto_Schedule_Open_PO = 1, True, False)

                gvMRPDetal.Rows.Clear()


                If obj.ObjListMRPDetail IsNot Nothing And obj.ObjListMRPDetail.Count > 0 Then

                    For Each objMRPDetail As clsMRPAutoMobileDetail In obj.ObjListMRPDetail
                        gvMRPDetal.Rows.AddNew()

                        gvMRPDetal.Rows(gvMRPDetal.Rows.Count - 1).Cells(colLineno).Value = objMRPDetail.Line_No
                        gvMRPDetal.Rows(gvMRPDetal.Rows.Count - 1).Cells(colItem_Code).Value = objMRPDetail.Item_Code
                        gvMRPDetal.Rows(gvMRPDetal.Rows.Count - 1).Cells(colItem_Desc).Value = objMRPDetail.Item_Desc
                        gvMRPDetal.Rows(gvMRPDetal.Rows.Count - 1).Cells(colUNIT_CODE).Value = objMRPDetail.RM_UNIT_CODE

                        gvMRPDetal.Rows(gvMRPDetal.Rows.Count - 1).Cells(colminqty).Value = objMRPDetail.Min_Qty
                        gvMRPDetal.Rows(gvMRPDetal.Rows.Count - 1).Cells(colmaxqty).Value = objMRPDetail.Max_Qty
                        gvMRPDetal.Rows(gvMRPDetal.Rows.Count - 1).Cells(colrol).Value = objMRPDetail.ROL_Qty
                        gvMRPDetal.Rows(gvMRPDetal.Rows.Count - 1).Cells(colRequiredQty).Value = objMRPDetail.Total_Requird_Qty
                        gvMRPDetal.Rows(gvMRPDetal.Rows.Count - 1).Cells(colstockqty).Value = clsProductionPlanning.GetItemBalance(fndLocation.Value, objMRPDetail.Item_Code, objMRPDetail.RM_UNIT_CODE)
                        If obj.POSTED Then
                            gvMRPDetal.Rows(gvMRPDetal.Rows.Count - 1).Cells(colstockqty).Value = objMRPDetail.Stock_Qty
                        End If
                        gvMRPDetal.Rows(gvMRPDetal.Rows.Count - 1).Cells(colPO_Qty).Value = objMRPDetail.PO_Qty
                        gvMRPDetal.Rows(gvMRPDetal.Rows.Count - 1).Cells(colQC_Qty).Value = objMRPDetail.QC_Qty
                        gvMRPDetal.Rows(gvMRPDetal.Rows.Count - 1).Cells(colExtraaddqty).Value = objMRPDetail.Extra_Qty
                        gvMRPDetal.Rows(gvMRPDetal.Rows.Count - 1).Cells(colGrossQty).Value = objMRPDetail.Net_Requird_Qty
                        gvMRPDetal.Rows(gvMRPDetal.Rows.Count - 1).Cells(colremarks).Value = objMRPDetail.Remarks
                        gvMRPDetal.Rows(gvMRPDetal.Rows.Count - 1).Cells(colNetRequiredQty).Value = objMRPDetail.Actual_Requird_Qty
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
        SavingData(False)
    End Sub

    Private Sub CalculateNetQty()
        Try
            For Each grow As GridViewRowInfo In gvMRPDetal.Rows
                Dim stockqty As Decimal = Nothing
                Dim req_qty As Decimal = Nothing
                Dim min_qty As Decimal = Nothing
                Dim max_qty As Decimal = Nothing
                Dim rol_qty As Decimal = Nothing
                Dim pending_po As Decimal = Nothing
                Dim pending_qc As Decimal = Nothing
                Dim extra_add As Decimal = Nothing

                stockqty = clsCommon.myCdbl(grow.Cells(colstockqty).Value)
                req_qty = clsCommon.myCdbl(grow.Cells(colRequiredQty).Value)
                min_qty = clsCommon.myCdbl(grow.Cells(colminqty).Value)
                max_qty = clsCommon.myCdbl(grow.Cells(colmaxqty).Value)
                rol_qty = clsCommon.myCdbl(grow.Cells(colrol).Value)
                pending_po = clsCommon.myCdbl(grow.Cells(colPO_Qty).Value)
                pending_qc = clsCommon.myCdbl(grow.Cells(colQC_Qty).Value)
                extra_add = clsCommon.myCdbl(grow.Cells(colExtraaddqty).Value)

                If chkStock.Checked Then
                    req_qty -= stockqty
                End If
                If chkPendingPO.Checked Then
                    req_qty -= pending_po
                End If
                If chkPendignQC.Checked Then
                    req_qty -= pending_qc
                End If
                If chkItemLevel.Checked Then
                    'net_qty += max_qty + min_qty + rol_qty
                End If

                req_qty += extra_add

                If req_qty > 0 Then
                    grow.Cells(colGrossQty).Value = req_qty
                End If

                stockqty = Nothing
                req_qty = Nothing
                min_qty = Nothing
                max_qty = Nothing
                rol_qty = Nothing
                pending_po = Nothing
                pending_qc = Nothing
                extra_add = Nothing
            Next
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Public Function Save() As Boolean
        Dim obj As New clsMRPAutoMobile()
        Try
            Dim issaved As Boolean = True

            CalculateNetQty()

            If AllowToSave() Then
                Try
                    obj = New clsMRPAutoMobile()
                    obj.ObjListMRPDetail = New List(Of clsMRPAutoMobileDetail)

                    obj.MRP_CODE = clsCommon.myCstr(Me.txtCode.Value)
                    obj.MRP_DATE = clsCommon.myCDate(Me.dtpMRPDate.Value)
                    obj.MRP_DESCRIPTION = clsCommon.myCstr(Me.txtMRPDescription.Text)
                    obj.MRP_REMARKS = clsCommon.myCstr(Me.txtDescription.Text)
                    obj.PROD_PLAN_CODE = clsCommon.myCstr(Me.fndProductionPlan.Value)
                    obj.MRP_Location = clsCommon.myCstr(fndLocation.Value)
                    obj.MRP_FROM = clsCommon.myCDate(Me.dtpFromDate.Value)
                    obj.MRP_TO = clsCommon.myCDate(Me.dtpToDate.Value)

                    obj.Include_Stock = IIf(chkStock.Checked, 1, 0)
                    obj.Include_Pending_QC = IIf(chkPendignQC.Checked, 1, 0)
                    obj.Include_Pending_PO = IIf(chkPendingPO.Checked, 1, 0)
                    obj.Include_Item_Level = IIf(chkItemLevel.Checked, 1, 0)
                    obj.Auto_PO = IIf(chkAutoPO.IsChecked, 1, 0)
                    obj.Auto_Indent = IIf(chkAutoIndent.IsChecked, 1, 0)
                    obj.Consider_Open_PO = IIf(chkConsiderOpenPO.Checked, 1, 0)
                    obj.Auto_Schedule_Open_PO = IIf(chkGenAutoSchedule.Checked, 1, 0)

                    '' saving MRP Detail
                    Dim objtr As New clsMRPAutoMobileDetail()

                    For Each grow As GridViewRowInfo In gvMRPDetal.Rows
                        objtr = New clsMRPAutoMobileDetail()

                        objtr.MRP_CODE = clsCommon.myCstr(txtCode.Value)
                        objtr.Line_No = clsCommon.myCstr(grow.Cells(colLineno).Value)
                        objtr.Item_Code = clsCommon.myCstr(grow.Cells(colItem_Code).Value)
                        objtr.RM_UNIT_CODE = clsCommon.myCstr(grow.Cells(colUNIT_CODE).Value)
                        objtr.Min_Qty = clsCommon.myCdbl(grow.Cells(colminqty).Value)
                        objtr.Max_Qty = clsCommon.myCdbl(grow.Cells(colmaxqty).Value)
                        objtr.ROL_Qty = clsCommon.myCdbl(grow.Cells(colrol).Value)
                        objtr.Total_Requird_Qty = clsCommon.myCdbl(grow.Cells(colRequiredQty).Value)
                        objtr.PO_Qty = clsCommon.myCdbl(grow.Cells(colPO_Qty).Value)
                        objtr.QC_Qty = clsCommon.myCdbl(grow.Cells(colQC_Qty).Value)
                        objtr.Extra_Qty = clsCommon.myCdbl(grow.Cells(colExtraaddqty).Value)
                        objtr.Net_Requird_Qty = clsCommon.myCdbl(grow.Cells(colGrossQty).Value)
                        objtr.Stock_Qty = clsCommon.myCdbl(grow.Cells(colstockqty).Value)
                        objtr.Remarks = clsCommon.myCstr(grow.Cells(colremarks).Value)
                        objtr.Actual_Requird_Qty = clsCommon.myCdbl(grow.Cells(colGrossQty).Value)
                        If clsCommon.myLen(objtr.Item_Code) > 0 Then
                            obj.ObjListMRPDetail.Add(objtr)
                        End If
                    Next

                    issaved = clsMRPAutoMobile.SaveData(obj, isNewEntry, Me.txtCode.Value)
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
        Return False
    End Function

    Function AllowToSave() As Boolean
        Try
            If btnsave.Text = "Update" Then
                Dim QryStr As String = "select POSTED from TSPL_MRP_HEAD where MRP_Code = '" + txtCode.Value + "' "
                Dim chkpost As String = clsDBFuncationality.getSingleValue(QryStr)
                If chkpost = "1" Then
                    clsCommon.MyMessageBoxShow(Me, "Transection already posted")
                    Return False
                End If
            End If

            If clsCommon.myLen(fndProductionPlan.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Select production plan.")
                fndProductionPlan.Focus()
                fndProductionPlan.Select()
                Errorcontrol.SetError(txtProductionPlanDesc, "Select production plan.")
                Return False
            Else
                Errorcontrol.ResetError(txtProductionPlanDesc)
            End If

            Dim icode As String = ""

            For ii As Integer = 0 To gvMRPDetal.Rows.Count - 1
                icode = clsCommon.myCstr(gvMRPDetal.Rows(ii).Cells(colItem_Code).Value)

                If ii = 0 AndAlso clsCommon.myLen(icode) <= 0 Then
                    clsCommon.MyMessageBoxShow(Me, "Fill atleast one row for MRP.")
                    gvMRPDetal.CurrentRow = gvMRPDetal.Rows(ii)
                    Return False
                End If
            Next

            Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            Return False
        End Try
    End Function

    Private Sub btndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click
        DeleteData()
    End Sub

    Sub DeleteData()
        If clsCommon.myLen(txtCode.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "You Cannot Delete Record")
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
                If (clsMRPAutoMobile.DeleteData(txtCode.Value)) Then
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
                If chkAutoPO.IsChecked Or chkConsiderOpenPO.Checked Then ''if auto po checked then first save po
                    Dim frm As New FrmMRPBasedPO()
                    frm.chkAutoIndent.IsChecked = chkAutoIndent.IsChecked
                    frm.chkAutoPO.IsChecked = chkAutoPO.IsChecked
                    frm.chkConsiderOpenPO.Checked = chkConsiderOpenPO.Checked
                    frm.chkGenAutoSchedule.Checked = chkGenAutoSchedule.Checked

                    frm._MRPNO = txtCode.Value
                    frm.WindowState = FormWindowState.Maximized
                    frm.ShowDialog()
                    LoadData(txtCode.Value, NavigatorType.Current)
                ElseIf (clsMRPAutoMobile.PostData(txtCode.Value, chkAutoIndent.IsChecked)) Then
                    myMessages.post()
                    LoadData(txtCode.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
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

        Dim str As String = "select count(*) from TSPL_MRP_HEAD where MRP_Code ='" + txtCode.Value + "' and trans_id='A_Mobile' "
        Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
        If no = 0 Then
            txtCode.MyReadOnly = False
        Else
            txtCode.MyReadOnly = True
        End If

        If txtCode.MyReadOnly OrElse isButtonClicked Then
            txtCode.Value = clsMRPAutoMobile.GetFinder("", txtCode.Value, isButtonClicked)

            If clsCommon.myLen(txtCode.Value) > 0 Then
                LoadData(txtCode.Value, NavigatorType.Current)
            Else
                funReset()
            End If
        End If
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

    Private Sub fndProductionPlan__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndProductionPlan._MYValidating
        Dim whrcls As String = " TSPL_MF_PRODUCTION_PLAN_HEAD.prod_plan_code not in (select PROD_PLAN_CODE from TSPL_MRP_HEAD where Trans_Id='A_Mobile' and mrp_code<>'" + txtCode.Value + "')"
        Me.fndProductionPlan.Value = clsProductionPlanning.GetFinder(whrcls, Me.fndProductionPlan.Value, isButtonClicked)

        chkStock.Checked = False
        chkPendignQC.Checked = False
        chkPendingPO.Checked = False
        chkItemLevel.Checked = False

        gvMRPDetal.Rows.Clear()
        gvMRPDetal.Rows.AddNew()
        Me.txtProductionPlanDesc.Text = Nothing
        Me.dtpFromDate.Value = Nothing
        Me.dtpToDate.Value = Nothing
        If clsCommon.myLen(Me.fndProductionPlan.Value) > 0 Then
            Dim objPP As New clsProductionPlanning()
            Try
                objPP = clsProductionPlanning.GetData(Me.fndProductionPlan.Value, NavigatorType.Current)
                If objPP IsNot Nothing Then
                    Me.txtProductionPlanDesc.Text = objPP.DESCRIPTION
                    fndLocation.Value = objPP.Location_Code
                    lblLocationDesc.Text = clsLocation.GetName(objPP.Location_Code, Nothing)
                    Me.dtpFromDate.Value = objPP.PLAN_FOR_DATE
                    If Not objPP.PLAN_TO_DATE Is Nothing Then
                        Me.dtpToDate.Value = objPP.PLAN_TO_DATE
                    End If


                    Dim qry As String = "select consm_item_code,sum(isnull(consm_quantity,0)) as consm_quantity,consm_item_unit_code from ("
                    qry += "select consm_item_code,(case when isnull(prod_quantity,0)>0 then (isnull(CONSM_QUANTITY,0) * isnull(Net_Req_Qty,0)) / PROD_QUANTITY else isnull(CONSM_QUANTITY,0) end) as consm_quantity,consm_item_unit_code from TSPL_MF_BOM_DETAIL "
                    qry += " left outer join TSPL_MF_BOM_HEAD on TSPL_MF_BOM_HEAD.BOM_CODE=TSPL_MF_BOM_DETAIL.BOM_CODE left outer join TSPL_MF_PROD_PLAN_DETAIL on TSPL_MF_BOM_DETAIL.BOM_CODE=TSPL_MF_PROD_PLAN_DETAIL.BOM_CODE "
                    qry += " where 1=1 and prod_plan_code='" + fndProductionPlan.Value + "')axa group by consm_item_code,consm_item_unit_code"
                    Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                        For Each dr As DataRow In dt.Rows
                            gvMRPDetal.Rows(gvMRPDetal.Rows.Count - 1).Cells(colLineno).Value = gvMRPDetal.Rows.Count
                            gvMRPDetal.Rows(gvMRPDetal.Rows.Count - 1).Cells(colItem_Code).Value = clsCommon.myCstr(dr("consm_item_code"))
                            gvMRPDetal.Rows(gvMRPDetal.Rows.Count - 1).Cells(colItem_Desc).Value = clsItemMaster.GetItemName(clsCommon.myCstr(dr("consm_item_code")), Nothing)
                            gvMRPDetal.Rows(gvMRPDetal.Rows.Count - 1).Cells(colUNIT_CODE).Value = clsCommon.myCstr(dr("consm_item_unit_code"))

                            gvMRPDetal.Rows(gvMRPDetal.Rows.Count - 1).Cells(colminqty).Value = clsMRPAutoMobile.GetItemLevelQty("Min", clsCommon.myCstr(dr("consm_item_code")))
                            gvMRPDetal.Rows(gvMRPDetal.Rows.Count - 1).Cells(colmaxqty).Value = clsMRPAutoMobile.GetItemLevelQty("Max", clsCommon.myCstr(dr("consm_item_code")))
                            gvMRPDetal.Rows(gvMRPDetal.Rows.Count - 1).Cells(colrol).Value = clsMRPAutoMobile.GetItemLevelQty("ROL", clsCommon.myCstr(dr("consm_item_code")))
                            gvMRPDetal.Rows(gvMRPDetal.Rows.Count - 1).Cells(colRequiredQty).Value = clsCommon.myCdbl(dr("consm_quantity"))
                            gvMRPDetal.Rows(gvMRPDetal.Rows.Count - 1).Cells(colstockqty).Value = clsProductionPlanning.GetItemBalance(fndLocation.Value, clsCommon.myCstr(dr("consm_item_code")), clsCommon.myCstr(dr("consm_item_unit_code")))
                            Dim PendingPO_Qty As Double = clsMRPAutoMobile.GetPendingPOQty(clsCommon.myCstr(dr("consm_item_code")))
                            If PendingPO_Qty < 0 Then
                                PendingPO_Qty = 0
                            End If
                            gvMRPDetal.Rows(gvMRPDetal.Rows.Count - 1).Cells(colPO_Qty).Value = PendingPO_Qty
                            gvMRPDetal.Rows(gvMRPDetal.Rows.Count - 1).Cells(colQC_Qty).Value = 0
                            gvMRPDetal.Rows(gvMRPDetal.Rows.Count - 1).Cells(colExtraaddqty).Value = 0
                            gvMRPDetal.Rows(gvMRPDetal.Rows.Count - 1).Cells(colGrossQty).Value = 0
                            gvMRPDetal.Rows(gvMRPDetal.Rows.Count - 1).Cells(colremarks).Value = Nothing

                            gvMRPDetal.Rows.AddNew()

                            '=====get child of item is exist in bom===
                            Dim bomcode As String = clsMRPAutoMobilePODetail.GetBOMOtherItems(clsCommon.myCstr(dr("consm_item_code")))
                            While (clsCommon.myLen(bomcode) > 0)
                                Dim item_code As String = ""

                                qry = "select consm_item_code,consm_item_unit_code,sum(isnull(consm_quantity,0)) as consm_quantity from ("
                                qry += "select consm_item_code,(case when isnull(TSPL_MF_BOM_HEAD.prod_quantity,0)>0 then (isnull(consm_quantity,0) * '" + clsCommon.myCstr(dr("consm_quantity")) + "')/TSPL_MF_BOM_HEAD.prod_quantity else isnull(consm_quantity,0) end) as consm_quantity,consm_item_unit_code from TSPL_MF_BOM_DETAIL left outer join TSPL_MF_BOM_HEAD on TSPL_MF_BOM_HEAD.BOM_CODE=TSPL_MF_BOM_DETAIL.BOM_CODE where 1=1 and "
                                qry += " TSPL_MF_BOM_DETAIL.bom_code in (" + bomcode + "))axa group by consm_item_code,consm_item_unit_code"
                                Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qry)
                                If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
                                    For Each dr1 As DataRow In dt1.Rows
                                        gvMRPDetal.Rows(gvMRPDetal.Rows.Count - 1).Cells(colLineno).Value = gvMRPDetal.Rows.Count
                                        gvMRPDetal.Rows(gvMRPDetal.Rows.Count - 1).Cells(colItem_Code).Value = clsCommon.myCstr(dr1("consm_item_code"))
                                        gvMRPDetal.Rows(gvMRPDetal.Rows.Count - 1).Cells(colItem_Desc).Value = clsItemMaster.GetItemName(clsCommon.myCstr(dr1("consm_item_code")), Nothing)
                                        gvMRPDetal.Rows(gvMRPDetal.Rows.Count - 1).Cells(colUNIT_CODE).Value = clsCommon.myCstr(dr1("consm_item_unit_code"))

                                        gvMRPDetal.Rows(gvMRPDetal.Rows.Count - 1).Cells(colminqty).Value = clsMRPAutoMobile.GetItemLevelQty("Min", clsCommon.myCstr(dr1("consm_item_code")))
                                        gvMRPDetal.Rows(gvMRPDetal.Rows.Count - 1).Cells(colmaxqty).Value = clsMRPAutoMobile.GetItemLevelQty("Max", clsCommon.myCstr(dr1("consm_item_code")))
                                        gvMRPDetal.Rows(gvMRPDetal.Rows.Count - 1).Cells(colrol).Value = clsMRPAutoMobile.GetItemLevelQty("ROL", clsCommon.myCstr(dr1("consm_item_code")))
                                        gvMRPDetal.Rows(gvMRPDetal.Rows.Count - 1).Cells(colRequiredQty).Value = clsCommon.myCdbl(dr1("consm_quantity"))
                                        gvMRPDetal.Rows(gvMRPDetal.Rows.Count - 1).Cells(colstockqty).Value = clsProductionPlanning.GetItemBalance(fndLocation.Value, clsCommon.myCstr(dr1("consm_item_code")), clsCommon.myCstr(dr1("consm_item_unit_code")))
                                        PendingPO_Qty = clsMRPAutoMobile.GetPendingPOQty(clsCommon.myCstr(dr1("consm_item_code")))
                                        If PendingPO_Qty < 0 Then
                                            PendingPO_Qty = 0
                                        End If
                                        gvMRPDetal.Rows(gvMRPDetal.Rows.Count - 1).Cells(colPO_Qty).Value = PendingPO_Qty 'clsMRPAutoMobile.GetPendingPOQty(clsCommon.myCstr(dr1("consm_item_code")))
                                        gvMRPDetal.Rows(gvMRPDetal.Rows.Count - 1).Cells(colQC_Qty).Value = 0
                                        gvMRPDetal.Rows(gvMRPDetal.Rows.Count - 1).Cells(colExtraaddqty).Value = 0
                                        gvMRPDetal.Rows(gvMRPDetal.Rows.Count - 1).Cells(colGrossQty).Value = 0
                                        gvMRPDetal.Rows(gvMRPDetal.Rows.Count - 1).Cells(colremarks).Value = Nothing

                                        gvMRPDetal.Rows.AddNew()

                                        item_code = item_code + "','" + clsCommon.myCstr(dr1("consm_item_code"))
                                    Next
                                End If

                                bomcode = clsMRPAutoMobilePODetail.GetBOMOtherItems(item_code)
                            End While
                            '=========================end here==============================

                        Next
                    End If
                End If

            Catch ex As Exception
                clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            Finally
                objPP = Nothing
                MergeSimilarItemRows()
            End Try
        End If

    End Sub

    Private Sub MergeSimilarItemRows()
        Try
            Dim item_code As String = ""
            Dim qty As Decimal = 0
            Dim olditem_code As String = ""
            Dim lineno As Integer = 0

            For ii As Integer = 0 To gvMRPDetal.Rows.Count - 1
                item_code = clsCommon.myCstr(gvMRPDetal.Rows(ii).Cells(colItem_Code).Value)
                qty = clsCommon.myCdbl(gvMRPDetal.Rows(ii).Cells(colRequiredQty).Value)

                If clsCommon.myLen(item_code) > 0 Then
                    For jj As Integer = ii + 1 To gvMRPDetal.Rows.Count - 1
                        olditem_code = clsCommon.myCstr(gvMRPDetal.Rows(jj).Cells(colItem_Code).Value)

                        If clsCommon.CompairString(item_code, olditem_code) = CompairStringResult.Equal AndAlso clsCommon.CompairString(olditem_code, "OLD") <> CompairStringResult.Equal Then
                            qty += clsCommon.myCdbl(gvMRPDetal.Rows(jj).Cells(colRequiredQty).Value)
                            gvMRPDetal.Rows(jj).Cells(colItem_Code).Value = "OLD"
                        End If
                    Next
                End If 'cond.

                If clsCommon.CompairString(item_code, "OLD") <> CompairStringResult.Equal Then
                    lineno += 1
                    gvMRPDetal.Rows(ii).Cells(colLineno).Value = lineno
                End If

                gvMRPDetal.Rows(ii).Cells(colRequiredQty).Value = qty
            Next

            '==============remove old item row
            For ii As Integer = gvMRPDetal.Rows.Count - 1 To 0 Step -1
                If clsCommon.CompairString(clsCommon.myCstr(gvMRPDetal.Rows(ii).Cells(colItem_Code).Value), "OLD") = CompairStringResult.Equal Then
                    gvMRPDetal.Rows.RemoveAt(ii)
                End If
            Next

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub btnnew_Click1(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnnew.Click
        funReset()
    End Sub

    Private Sub chkStock_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkStock.ToggleStateChanged, chkPendingPO.ToggleStateChanged, chkPendignQC.ToggleStateChanged, chkItemLevel.ToggleStateChanged
        If Not isInsideLoaddata Then
            Dim stockqty As Decimal = Nothing
            Dim req_qty As Decimal = Nothing
            Dim min_qty As Decimal = Nothing
            Dim max_qty As Decimal = Nothing
            Dim rol_qty As Decimal = Nothing
            Dim pending_po As Decimal = Nothing
            Dim pending_qc As Decimal = Nothing
            Dim extra_add As Decimal = Nothing


            If gvMRPDetal.Columns.Count > 0 Then
                For Each grow As GridViewRowInfo In gvMRPDetal.Rows
                    stockqty = clsCommon.myCdbl(grow.Cells(colstockqty).Value)
                    req_qty = clsCommon.myCdbl(grow.Cells(colRequiredQty).Value)
                    min_qty = clsCommon.myCdbl(grow.Cells(colminqty).Value)
                    max_qty = clsCommon.myCdbl(grow.Cells(colmaxqty).Value)
                    rol_qty = clsCommon.myCdbl(grow.Cells(colrol).Value)
                    pending_po = clsCommon.myCdbl(grow.Cells(colPO_Qty).Value)
                    pending_qc = clsCommon.myCdbl(grow.Cells(colQC_Qty).Value)
                    extra_add = clsCommon.myCdbl(grow.Cells(colExtraaddqty).Value)

                    If chkStock.Checked Then
                        req_qty -= stockqty
                    End If
                    If chkPendingPO.Checked Then
                        req_qty -= pending_po
                    End If
                    If chkPendignQC.Checked Then
                        req_qty -= pending_qc
                    End If
                    If chkItemLevel.Checked Then
                        'net_qty += max_qty + min_qty + rol_qty
                    End If

                    req_qty += extra_add

                    If req_qty > 0 Then
                        grow.Cells(colGrossQty).Value = req_qty
                    End If
                Next
            End If

            stockqty = Nothing
            req_qty = Nothing
            min_qty = Nothing
            max_qty = Nothing
            rol_qty = Nothing
            pending_po = Nothing
            pending_qc = Nothing
            extra_add = Nothing
        End If
    End Sub

    Private Sub gvMRPDetal_CellValueChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvMRPDetal.CellValueChanged
        Try
            If Not isInsideLoaddata Then
                If Not isCellValueChangedOpen Then
                    If e.Column Is gvMRPDetal.Columns(colExtraaddqty) OrElse e.Column Is gvMRPDetal.Columns(colRequiredQty) Then
                        isCellValueChangedOpen = True
                        Dim stockqty As Decimal = Nothing
                        Dim req_qty As Decimal = Nothing
                        Dim min_qty As Decimal = Nothing
                        Dim max_qty As Decimal = Nothing
                        Dim rol_qty As Decimal = Nothing
                        Dim pending_po As Decimal = Nothing
                        Dim pending_qc As Decimal = Nothing
                        Dim extra_add As Decimal = Nothing

                        stockqty = clsCommon.myCdbl(gvMRPDetal.CurrentRow.Cells(colstockqty).Value)
                        req_qty = clsCommon.myCdbl(gvMRPDetal.CurrentRow.Cells(colRequiredQty).Value)
                        min_qty = clsCommon.myCdbl(gvMRPDetal.CurrentRow.Cells(colminqty).Value)
                        max_qty = clsCommon.myCdbl(gvMRPDetal.CurrentRow.Cells(colmaxqty).Value)
                        rol_qty = clsCommon.myCdbl(gvMRPDetal.CurrentRow.Cells(colrol).Value)
                        pending_po = clsCommon.myCdbl(gvMRPDetal.CurrentRow.Cells(colPO_Qty).Value)
                        pending_qc = clsCommon.myCdbl(gvMRPDetal.CurrentRow.Cells(colQC_Qty).Value)
                        extra_add = clsCommon.myCdbl(gvMRPDetal.CurrentRow.Cells(colExtraaddqty).Value)

                        If chkStock.Checked Then
                            req_qty -= stockqty
                        End If
                        If chkPendingPO.Checked Then
                            req_qty -= pending_po
                        End If
                        If chkPendignQC.Checked Then
                            req_qty -= pending_qc
                        End If
                        If chkItemLevel.Checked Then
                            'net_qty += max_qty + min_qty + rol_qty
                        End If

                        req_qty += extra_add

                        If req_qty > 0 Then
                            gvMRPDetal.CurrentRow.Cells(colGrossQty).Value = req_qty
                        End If

                        stockqty = Nothing
                        req_qty = Nothing
                        min_qty = Nothing
                        max_qty = Nothing
                        rol_qty = Nothing
                        pending_po = Nothing
                        pending_qc = Nothing
                        extra_add = Nothing
                        isCellValueChangedOpen = False
                    End If

                End If
            End If
        Catch ex As Exception
            isCellValueChangedOpen = False
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub chkConsiderOpenPO_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkConsiderOpenPO.ToggleStateChanged
        If chkConsiderOpenPO.Checked = True Then
            chkGenAutoSchedule.Checked = True
        Else
            chkGenAutoSchedule.Checked = False
        End If
    End Sub

    Private Sub btnUnpost_Click(sender As Object, e As EventArgs) Handles btnUnpost.Click
        Try
            If clsCommon.myLen(txtCode.Value) > 0 Then
                If clsCommon.MyMessageBoxShow(Me, "Unpost the current transaction" + Environment.NewLine + "Are you sure", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = Windows.Forms.DialogResult.Yes Then
                    clsMRPAutoMobile.ReverseAndUnpost(txtCode.Value, chkAutoIndent.IsChecked)
                    clsCommon.MyMessageBoxShow(Me, "Tansaction unposted succesffuly", Me.Text)
                    LoadData(txtCode.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class