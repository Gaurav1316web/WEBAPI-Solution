Imports common
Imports System.Data.SqlClient
Imports System.IO

Public Class frmBillOfMaterial
    Inherits FrmMainTranScreen
    Const colLineNo As String = "LineNo"
    Const colItemCategoryCode As String = "ItemCategoryCode"
    Const colItemCode As String = "ItemCode"
    Const colitemDesc As String = "ItemDesc"
    Const colqty As String = "Qty"
    Const colUnitCode As String = "UnitCode"
    Const colscrap_per As String = "Scrap_per"
    Const colwastage_per As String = "Wastage_per"
    Const colRemarks As String = "Remarks"


    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim isNewEntry As Boolean = True
    'Private Obj As clsmo
    Dim obj As New clsBillOfMaterial
    Private ObjList As New List(Of clsBillOfMaterial)
    Private isCellValueChangedOpen As Boolean = False
    Dim OpenFileDialog1 As New OpenFileDialog


    Sub LoadGridColumns()
        gvBOM.Rows.Clear()
        gvBOM.Columns.Clear()

        Dim LineNo As New GridViewTextBoxColumn
        Dim ItemCategoryCode As New GridViewTextBoxColumn
        Dim ItemCode As New GridViewTextBoxColumn
        Dim itemDesc As New GridViewTextBoxColumn
        Dim qty As New GridViewDecimalColumn
        Dim UnitCode As New GridViewTextBoxColumn
        Dim scrap_per As New GridViewDecimalColumn
        Dim wastage_per As New GridViewDecimalColumn
        Dim remarks As New GridViewTextBoxColumn

        LineNo.FormatString = ""
        LineNo.HeaderText = "Line No"
        LineNo.Name = colLineNo
        LineNo.Width = 70
        LineNo.ReadOnly = True
        LineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvBOM.Columns.Add(LineNo)

        ItemCategoryCode.FormatString = ""
        ItemCategoryCode.HeaderText = "Item Category"
        ItemCategoryCode.Name = colItemCategoryCode
        ItemCategoryCode.Width = 100
        'ItemCategoryCode.ReadOnly = True
        ItemCategoryCode.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvBOM.Columns.Add(ItemCategoryCode)

        ItemCode.FormatString = ""
        ItemCode.HeaderText = "Item Code"
        ItemCode.Name = colItemCode
        ItemCode.Width = 100
        'ItemCode.ReadOnly = True
        ItemCode.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvBOM.Columns.Add(ItemCode)

        itemDesc.FormatString = ""
        itemDesc.HeaderText = "Item Description"
        itemDesc.Name = colitemDesc
        itemDesc.Width = 100
        'itemDesc.ReadOnly = True
        itemDesc.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvBOM.Columns.Add(itemDesc)

        qty.FormatString = ""
        qty.HeaderText = "Quantity"
        qty.Name = colqty
        qty.Width = 100
        qty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvBOM.Columns.Add(qty)

        UnitCode.FormatString = ""
        UnitCode.HeaderText = "UOM"
        UnitCode.Name = colUnitCode
        UnitCode.Width = 100
        UnitCode.ReadOnly = True
        UnitCode.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvBOM.Columns.Add(UnitCode)

        scrap_per.FormatString = ""
        scrap_per.HeaderText = "Scrap(%)"
        scrap_per.Name = colscrap_per
        scrap_per.Width = 100
        scrap_per.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvBOM.Columns.Add(scrap_per)

        wastage_per.FormatString = ""
        wastage_per.HeaderText = "Wastage(%)"
        wastage_per.Name = colwastage_per
        wastage_per.Width = 100
        wastage_per.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvBOM.Columns.Add(wastage_per)

        remarks.FormatString = ""
        remarks.HeaderText = "Remarks"
        remarks.Name = colRemarks
        remarks.Width = 130
        remarks.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvBOM.Columns.Add(remarks)




    End Sub


    Private Sub frmBillOfMaterial_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
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
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag AndAlso btnPost.Enabled Then
            PostData()
        End If
    End Sub
    Sub funClose()
        Me.Close()
    End Sub

    Private Sub frmBillOfMaterial_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        LoadGridColumns()
        isNewEntry = True
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(btnPost, "Press Alt+P for  Post")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D  for Delete ")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnNew, "Press Alt+N Adding New ")
        '  ButtonToolTip.SetToolTip(btnPrint, "Press Alt+R for Print Preview")
        Me.dtpBOMDate.Value = clsCommon.GETSERVERDATE
        Me.lblCreatedByName.Text = objCommonVar.CurrentUserCode
        Me.gvBOM.Rows.Clear()
        Me.gvBOM.Rows.AddNew()
    End Sub
    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.frmBillOfMaterialPepsi)
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow("Permission Denied")
            Me.Close()
            Exit Sub
        End If
        btnsave.Visible = MyBase.isModifyFlag
        '--------richa Ticket no. BM00000003014 02/07/2014 to enable/disable import/export option acc. to user mgmt setting -----------
        If btnsave.Visible = True Then
            MenuItemImport.Enabled = True
            MenuItemExport.Enabled = True
        Else
            MenuItemImport.Enabled = False
            MenuItemExport.Enabled = False
        End If
        '--------------------------------------------------
        btnPost.Visible = MyBase.isPostFlag
        btndelete.Visible = MyBase.isDeleteFlag
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
        Me.txtProducedItem.Value = Nothing
        Me.lblMasterItemName.Text = ""
        Me.txtBuildQty.Text = ""
        Me.dtpBOMDate.Value = Today
        Me.dtpStartDate.Value = Today
        Me.dtpEndDate.Value = Today
        Me.cboBOMStatus.SelectedValue = "Open"
        Me.chkDefaultBOM.Checked = False
        Me.txtBuildQty.Text = ""
        Me.txtMinBatchQty.Text = ""
        txtDocPath.Text = ""
        btnsave.Text = "Save"
        btnsave.Enabled = True
        btndelete.Enabled = True
        btnPost.Enabled = True
        Me.gvBOM.Rows.Clear()
        Me.gvBOM.Rows.AddNew()
        Me.dtpBOMDate.Value = clsCommon.GETSERVERDATE
        Me.lblCreatedByName.Text = objCommonVar.CurrentUserCode
    End Sub

    Private Sub txtCode__MYNavigator(ByVal sender As Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType)
        Try
            LoadData(txtCode.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        funReset()
        txtCode.MyReadOnly = True
        btnsave.Enabled = True
        btndelete.Enabled = True
        obj = clsBillOfMaterial.GetData(strCode, NavTyep)
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.BOM_CODE) > 0) Then

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
            LoadGridColumns()
            txtCode.Value = obj.BOM_CODE
            Me.txtDescription.Text = clsCommon.myCstr(obj.DESCRIPTION)
            Me.dtpBOMDate.Value = obj.BOM_DATE
            Me.lblRevisionNo.Text = obj.REVISION_NO
            Me.dtpStartDate.Value = obj.START_DATE
            If clsCommon.myLen(obj.END_DATE) > 0 Then
                Me.dtpEndDate.Value = obj.END_DATE
                Me.dtpEndDate.Checked = True
            Else
                Me.dtpEndDate.Checked = False
            End If

            Me.cboBOMStatus.Text = obj.STATUS
            Me.chkDefaultBOM.Checked = obj.IS_DEFAULT
            Me.txtDocPath.Text = obj.ATTACHED_DOC_PATH
            Me.txtProducedItem.Value = obj.PROD_ITEM_CODE
            Me.txtBuildQty.Text = obj.PROD_QUANTITY
            Me.lblUnitName.Text = obj.PROD_ITEM_UNIT_CODE
            Me.txtMinBatchQty.Text = obj.MIN_BATCH_SIZE
            Me.lblCreatedByName.Text = obj.CREATED_BY
            If obj.POSTED = True Then
                Me.lblApprovedByName.Text = obj.APPROVED_BY
            Else
                Me.lblApprovedByName.Text = ""
            End If

            Me.lblMasterItemName.Text = obj.PROD_ITEM_NAME
            If clsCommon.myLen(obj.ATTACHED_DOC_PATH) > 0 Then
                btnBrowse.Text = "Download"
            Else
                btnBrowse.Text = "Browse"
            End If
            If (clsBillOfMaterial.ObjList IsNot Nothing AndAlso clsBillOfMaterial.ObjList.Count > 0) Then
                For Each obj As clsBillOfMaterial In clsBillOfMaterial.ObjList
                    gvBOM.Rows.AddNew()

                    gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colLineNo).Value = obj.Line_No
                    gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colItemCategoryCode).Value = obj.CONSM_ITEM_CATEGORY_CODE
                    gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colItemCode).Value = obj.CONSM_ITEM_CODE
                    gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colitemDesc).Value = obj.ITEM_DESCRIPTION
                    gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colqty).Value = obj.CONSM_QUANTITY
                    gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colUnitCode).Value = obj.CONSM_ITEM_UNIT_CODE
                    gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colscrap_per).Value = obj.SCRAP_PERCENT
                    gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colwastage_per).Value = obj.WASTAGE_PERCENT
                    gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colRemarks).Value = obj.REMARKS
                Next
            Else
                gvBOM.Rows.AddNew()
            End If
        End If

    End Sub
    Sub Show_BOM_Detail(ByVal strCode As String, ByVal NavTyep As NavigatorType)

        'Dim obj1 As clsMapPayHeadsToSalaStructure
        'obj1 = clsMapPayHeadsToSalaStructure.GetData(strCode, NavTyep)
        'If (obj1 IsNot Nothing AndAlso clsCommon.myLen(obj1.SALARY_STRUCTURE_CODE) > 0) Then

        '    Dim ii As Int16 = 0
        '    LoadGridColumns()

        '    lblMasterItemName.Text = obj1.SALARY_STRUCTURE_NAME
        '    If (obj1.ObjList IsNot Nothing AndAlso obj1.ObjList.Count > 0) Then
        '        For Each obj As clsMapPayHeadsToSalaStructure In obj1.ObjList
        '            gvBOM.Rows.AddNew()
        '            gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colLineNo).Value = obj.LINE_NO
        '            gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colpayHeadCode).Value = obj.PAY_HEAD_CODE
        '            gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colpayHeadName).Value = obj.PAY_HEAD_NAME
        '            gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colPayHeadFormula).Value = obj.PAYHEAD_FORMULA
        '            gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colRateAmount).Value = obj.RATE_AMOUNT
        '            gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colHiddenComponent).Value = obj.IsHiddenComponent
        '        Next
        '    Else
        '        gvBOM.Rows.AddNew()
        '    End If
        'End If

    End Sub


    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        SavingData(False)
    End Sub
    Public Function Save() As Boolean
        If AllowToSave() Then
            Dim obj As New clsBillOfMaterial
            obj.BOM_CODE = Me.txtCode.Value
            obj.DESCRIPTION = Me.txtDescription.Text
            obj.BOM_DATE = Me.dtpBOMDate.Value
            obj.REVISION_NO = Me.lblRevisionNo.Text
            obj.START_DATE = Me.dtpStartDate.Value
            If Me.dtpEndDate.Checked = True Then
                obj.END_DATE = Me.dtpEndDate.Value
            Else
                obj.END_DATE = Nothing
            End If

            obj.STATUS = Me.cboBOMStatus.Text
            obj.IS_DEFAULT = Me.chkDefaultBOM.Checked

            'obj.ATTACHED_DOC = Me.txtDocPath.Text
            obj.ATTACHED_DOC_PATH = Me.txtDocPath.Text
            obj.PROD_ITEM_CODE = Me.txtProducedItem.Value
            obj.PROD_QUANTITY = Me.txtBuildQty.Text
            obj.PROD_ITEM_UNIT_CODE = Me.lblUnitName.Text
            obj.MIN_BATCH_SIZE = clsCommon.myCdbl(Me.txtMinBatchQty.Text)
            obj.CREATED_BY = Me.lblCreatedBy.Text
            'obj.APPROVED_BY = Me.lblApprovedBy.Text

            Dim obj1 As clsBillOfMaterial
            ObjList = New List(Of clsBillOfMaterial)
            For Each grow As GridViewRowInfo In gvBOM.Rows
                If clsCommon.myLen(clsCommon.myCstr(grow.Cells(colLineNo).Value)) > 0 And clsCommon.myLen(clsCommon.myCstr(grow.Cells(colItemCategoryCode).Value)) > 0 Then
                    obj1 = New clsBillOfMaterial()

                    obj1.BOM_CODE = txtCode.Value
                    obj1.Line_No = clsCommon.myCdbl(grow.Cells(colLineNo).Value)
                    obj1.CONSM_ITEM_CATEGORY_CODE = clsCommon.myCstr(grow.Cells(colItemCategoryCode).Value)
                    obj1.CONSM_ITEM_CODE = clsCommon.myCstr(grow.Cells(colItemCode).Value)
                    obj1.ITEM_DESCRIPTION = clsCommon.myCstr(grow.Cells(colitemDesc).Value)
                    obj1.CONSM_QUANTITY = clsCommon.myCdbl(grow.Cells(colqty).Value)
                    obj1.CONSM_ITEM_UNIT_CODE = clsCommon.myCstr(grow.Cells(colUnitCode).Value)
                    obj1.SCRAP_PERCENT = clsCommon.myCdbl(grow.Cells(colscrap_per).Value)
                    obj1.WASTAGE_PERCENT = clsCommon.myCdbl(grow.Cells(colwastage_per).Value)
                    obj1.REMARKS = clsCommon.myCstr(grow.Cells(colRemarks).Value)
                    ObjList.Add(obj1)
                End If
            Next
            Dim issaved As Boolean = False
            issaved = obj.SaveData(obj, ObjList, isNewEntry, clsCommon.myCstr(txtCode.Value))
            If OpenFileDialog1.FileName = "" And issaved = True Then
                'clsCommon.MyMessageBoxShow("Document Save Successfully.")
                LoadData(obj.BOM_CODE, NavigatorType.Current)
                Return True
            End If
            Dim bData As Byte()
            Dim br As BinaryReader = New BinaryReader(System.IO.File.OpenRead(OpenFileDialog1.FileName))
            bData = br.ReadBytes(br.BaseStream.Length)
            obj.ATTACHED_DOC = bData

            Dim str As String
            str = "UPDATE TSPL_MF_BOM_HEAD set ATTACHED_DOC = @BLOBData where BOM_CODE = '" + txtCode.Value + "' "
            Dim cmd As SqlCommand = New SqlCommand(str, clsDBFuncationality.GetConnnection)
            Dim prm As New SqlParameter("@BLOBData", bData)
            cmd.Parameters.Add(prm)
            Dim COUNT As Integer = 0
            COUNT = cmd.ExecuteNonQuery()
            br.Close() ' done by stuti reagrding memory leakage
            If COUNT > 0 AndAlso issaved Then
                'clsCommon.MyMessageBoxShow("Document Save Successfully.")
                LoadData(obj.BOM_CODE, NavigatorType.Current)
                Return True
            End If
            'If issaved Then
            '    LoadData(obj.BOM_CODE, NavigatorType.Current)
            '    Return True
            '    '  common.clsCommon.MyMessageBoxShow("Data Saved Successfully")
            'End If
            'Return False
        End If
        Return False
    End Function
    Function AllowToSave() As Boolean

        If btnsave.Text = "Update" Then
            Dim QryStr As String = "select POSTED from TSPL_MF_BOM_HEAD where BOM_CODE = '" + txtCode.Value + "' "
            Dim chkpost As String = clsDBFuncationality.getSingleValue(QryStr)
            If chkpost = "1" Then
                clsCommon.MyMessageBoxShow("Transection already posted")
                Return False
            End If
        End If

        'If clsCommon.myLen(txtCode.Value) <= 0 Then
        '    myMessages.blankValue("Code")
        '    txtCode.Focus()
        '    Return False
        'End If

        'If clsCommon.myLen(txtCode.Value) <= 0 Then
        '    myMessages.blankValue("Salary Code")
        '    txtCode.Focus()
        '    Return False
        'End If

        Dim ii As Int16 = 0
        For Each grow As GridViewRowInfo In gvBOM.Rows
            If clsCommon.myLen(clsCommon.myCstr(grow.Cells(colItemCategoryCode).Value)) > 0 And clsCommon.myLen(clsCommon.myCstr(grow.Cells(colItemCode).Value)) > 0 Then
                ii += 1
            End If
        Next
        If ii = 0 Then
            clsCommon.MyMessageBoxShow("Please Fill Raw Material Grid.")
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
                If (clsBillOfMaterial.DeleteData(txtCode.Value)) Then
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

    Private Sub txtMasterItem__MYValidating1(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtProducedItem._MYValidating
        Try

            Dim qry As String = "SELECT ITEM_CODE AS CODE,ITEM_DESC AS ITEM_NAME,ITEM_TYPE AS TYPE FROM TSPL_ITEM_MASTER "
            txtProducedItem.Value = clsCommon.ShowSelectForm("TSPL_MF_BOM_HEAD", qry, "Code", "ITEM_TYPE IN ('F','S') ", txtProducedItem.Value, "", isButtonClicked)
            Dim objItm As New clsItemMaster
            '' NO CLASS  FOR ITEM MASTER(FINISHED)
            Dim DT_ITEM As DataTable
            Dim STRQ As String
            STRQ = "SELECT ITEM_DESC,ITEM_TYPE,UNIT_CODE FROM TSPL_ITEM_MASTER WHERE ITEM_CODE='" & txtProducedItem.Value & "'"

            DT_ITEM = clsDBFuncationality.GetDataTable(STRQ)
            If DT_ITEM.Rows.Count > 0 Then
                Me.lblMasterItemName.Text = DT_ITEM.Rows(0).Item("ITEM_DESC")
                Me.lblUnitName.Text = DT_ITEM.Rows(0).Item("UNIT_CODE")
            End If
            '' REVISION NO
            Dim rev_no As String
            STRQ = "SELECT (COUNT(BOM_CODE)) as rev FROM TSPL_MF_BOM_HEAD WHERE PROD_ITEM_CODE='" & txtProducedItem.Value & "' "
            Dim dt_rev As DataTable
            dt_rev = clsDBFuncationality.GetDataTable(STRQ)
            If dt_rev.Rows(0).Item("rev") = 0 Then
                rev_no = txtProducedItem.Value
            Else
                rev_no = txtProducedItem.Value & "/" & (dt_rev.Rows(0).Item("rev") + 1)
            End If

            Me.lblRevisionNo.Text = rev_no
        Catch ex As Exception

        End Try
    End Sub

    Private Sub txtCode__MYNavigator1(ByVal sender As Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtCode._MYNavigator
        Try
            LoadData(txtCode.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtCode__MYValidating1(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtCode._MYValidating

        Dim str As String = "select count(*) from TSPL_MF_BOM_HEAD where BOM_CODE ='" + txtCode.Value + "' "
        Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
        If no = 0 AndAlso isButtonClicked = False Then
            txtCode.MyReadOnly = False
            'txtCode.Value = ""
            '' common.clsCommon.MyMessageBoxShow("Value doesn't exist ")
        Else
            txtCode.MyReadOnly = True
        End If
        If txtCode.MyReadOnly OrElse isButtonClicked Then

            Dim qry As String = "SELECT T1.BOM_CODE AS Code,T1.DESCRIPTION,T1.BOM_DATE,T1.REVISION_NO,T1.START_DATE,T1.END_DATE,T1.STATUS,"
            qry += " T1.IS_DEFAULT,T1.ATTACHED_DOC,T1.ATTACHED_DOC_PATH,T1.PROD_ITEM_CODE,T2.ITEM_DESC AS PROD_ITEM_NAME,T1.PROD_QUANTITY,T1.PROD_ITEM_UNIT_CODE,"
            qry += " T1.MIN_BATCH_SIZE,T1.MODIFIED_BY AS APPROVED_BY,T1.Created_By FROM TSPL_MF_BOM_HEAD  T1 INNER JOIN TSPL_ITEM_MASTER T2  ON T1.PROD_ITEM_CODE=T2.ITEM_CODE "

            txtCode.Value = clsCommon.ShowSelectForm("TSPL_MF_BOM_HEAD", qry, "Code", "", txtCode.Value, "Code", isButtonClicked)
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
            If Me.cboBOMStatus.Text <> "Approved" Then
                common.clsCommon.MyMessageBoxShow("Bom Status must be Approved.")
                Exit Sub
            End If
            If (myMessages.postConfirm()) Then
                SavingData(True)
                If (clsBillOfMaterial.PostData(txtCode.Value, True)) Then
                    common.clsCommon.MyMessageBoxShow("Successfully Posted")
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
                common.clsCommon.MyMessageBoxShow("Data Saved Successfully")
            End If
        End If
    End Sub

   
   

    Private Sub MenuItemClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemClose.Click
        funClose()
    End Sub

   

    Private Sub gvBOM_CellEndEdit(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvBOM.CellEndEdit
        If gvBOM.CurrentRow Is Nothing Then
            Exit Sub
        End If

        If gvBOM.CurrentRow.Cells(0).Value = "" Then
            gvBOM.CurrentRow.Cells(0).Value = gvBOM.RowCount
        End If

        If Not isCellValueChangedOpen Then
            isCellValueChangedOpen = True
            If e.Column Is gvBOM.Columns(colItemCategoryCode) Then
                Dim strq As String = ""
                strq = "select PROD_ITEM_CATEGORY_CODE as Code,DESCRIPTION from TSPL_MF_PRODUCTION_ITEM_CATEGORY "
                gvBOM.CurrentRow.Cells(colItemCategoryCode).Value = clsCommon.ShowSelectForm("cat", strq, "Code", "", clsCommon.myCstr(gvBOM.CurrentRow.Cells(colItemCategoryCode).Value))
            End If

            
            If e.Column Is gvBOM.Columns(colItemCode) Then
                Dim obj As clsBillOfMaterial = clsBillOfMaterial.FinderForItem(clsCommon.myCstr(gvBOM.CurrentRow.Cells(colItemCode).Value), "ITEM_TYPE IN ('R','O') ", False)
                ''and prod_item_category_code='" & gvBOM.CurrentRow.Cells(colItemCategoryCode).Value & "'
                If obj IsNot Nothing AndAlso clsCommon.myLen(obj.PROD_ITEM_CODE) > 0 Then
                    gvBOM.CurrentRow.Cells(colItemCode).Value = obj.PROD_ITEM_CODE
                    gvBOM.CurrentRow.Cells(colitemDesc).Value = obj.ITEM_DESCRIPTION
                    gvBOM.CurrentRow.Cells(colUnitCode).Value = obj.PROD_ITEM_UNIT_CODE

                End If
            End If
            isCellValueChangedOpen = False
        End If
    End Sub


    Private Sub btnBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowse.Click
        If Me.btnBrowse.Text = "Browse" Then
            OpenFileDialog1.ShowDialog()
            txtDocPath.Text = OpenFileDialog1.SafeFileName
            Me.txtDocPath.ReadOnly = False
        Else
            showAttachedDocs(obj)
            Me.txtDocPath.ReadOnly = True
        End If
        
    End Sub
    Sub showAttachedDocs(ByVal obj As clsBillOfMaterial)

        Dim filename As String = ""
        Dim file_path As String = ""
        Dim file_extn As String = ""
        Try



            filename = clsCommon.myCstr(obj.ATTACHED_DOC_PATH).Split("\")(clsCommon.myCstr(obj.ATTACHED_DOC_PATH).Split("\").Length - 1)
            Dim blob As Byte() = obj.ATTACHED_DOC
            file_path = Application.StartupPath
           
            Dim index As Int16 = filename.LastIndexOf(".")
            file_extn = filename.Substring(index)
            filename = filename.Remove(index)
            filename += (clsCommon.myCDate(clsCommon.GETSERVERDATE(), "dd/MM/yy hh:mm:ss")).ToString()
            filename = filename.Replace(" ", "")
            filename = filename.Replace("/", "_")
            filename = filename.Replace(":", "_")
            Dim fs As FileStream = File.Create(file_path + "\\" + filename + file_extn)
            fs.Write(blob, 0, blob.Length)
            fs.Close()
            System.Diagnostics.Process.Start(file_path + "\\" + filename + file_extn)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnNewFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNewFile.Click
        Me.txtDocPath.Text = ""
        Me.btnBrowse.Text = "Browse"
    End Sub

    Private Sub txtProducedItem_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtProducedItem.Load

    End Sub

    

    Private Sub gvBOM_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles gvBOM.KeyDown
        If e.KeyData = Keys.Enter Then
            Me.gvBOM.Rows.Add(1)

            gvBOM.Rows(gvBOM.RowCount - 1).Cells(0).Value = gvBOM.RowCount
        End If
        If e.KeyData = Keys.Right And gvBOM.CurrentCell.ColumnIndex = gvBOM.Columns.Count - 1 Then
            Me.gvBOM.Rows.Add(1)
            gvBOM.Rows(gvBOM.RowCount - 1).Cells(0).Value = gvBOM.RowCount
        End If
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        If txtCode.Value = "" Then
            myMessages.blankValue(Me, "BOM Code", Me.Text)
        Else
            funPrint()
        End If
    End Sub
    Private Sub funPrint()
        Try
            Dim qry As String = " select '" & objCommonVar.CurrentCompanyName & "' as Company_Name, TSPL_MF_BOM_HEAD.PROD_ITEM_CODE  as BuildItemCode,CONVERT(VARCHAR,TSPL_MF_BOM_HEAD.BOM_DATE,103) as BOMDate,CONVERT(VARCHAR,TSPL_MF_BOM_HEAD.START_DATE,103) as StartDate,"
            qry += " CONVERT(VARCHAR,TSPL_MF_BOM_HEAD.END_DATE,103) as EndDate,TSPL_MF_BOM_HEAD.STATUS as BomStatus,TSPL_MF_BOM_HEAD.PROD_ITEM_UNIT_CODE as BuildUOM,"
            qry += " TSPL_MF_BOM_HEAD.PROD_QUANTITY as BuildQty, "
            qry += " TSPL_MF_BOM_HEAD.MIN_BATCH_SIZE as MinBatchSize,TSPL_MF_BOM_DETAIL.LINE_NO as SL_No,TSPL_MF_BOM_DETAIL.CONSM_ITEM_CATEGORY_CODE as ItemCategory,"
            qry += " TSPL_MF_BOM_DETAIL.CONSM_ITEM_CODE as ItemCode,TSPL_MF_BOM_DETAIL.ITEM_DESCRIPTION as ItemDesc,TSPL_MF_BOM_DETAIL.CONSM_ITEM_UNIT_CODE as UOM,"
            qry += " TSPL_MF_BOM_DETAIL.CONSM_QUANTITY as Quantity,TSPL_MF_BOM_DETAIL.SCRAP_PERCENT as Scrap,TSPL_MF_BOM_DETAIL.WASTAGE_PERCENT as Wastage,"
            qry += " TSPL_MF_BOM_DETAIL.REMARKS as Remarks from TSPL_MF_BOM_HEAD inner join TSPL_MF_BOM_DETAIL on TSPL_MF_BOM_HEAD.BOM_CODE=TSPL_MF_BOM_DETAIL.BOM_CODE"
            qry += " where 2=2"

            If txtCode.Value <> "" Then
                qry += " and  TSPL_MF_BOM_HEAD.BOM_CODE='" & txtCode.Value & "' "
            End If
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            Dim frmCRV As New frmCrystalReportViewer()
            frmCRV.funreport(CrystalReportFolder.PRODUCTION, dt, "crptBOMPrint", "Bill Of Material")
            frmCRV = Nothing
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

End Class