Imports common
Imports System.Data.SqlClient
Imports System.IO

Public Class frmProductionPlanningDemo
    Inherits FrmMainTranScreen
    Const colLineNo As String = "LineNo"
    'Const colProductionLineCode As String = "ProductionLineCode"
    Const colBOMCode As String = "BOMCode"
    Const colRevisionNo As String = "RevisionNo"
    Const colItemCode As String = "ItemCode"
    Const colitemDesc As String = "ItemDesc"
    'Const colMinQty As String = "MinQty"
    'Const colMaxQty As String = "MaxQty"
    Const colPlanQty As String = "MaxQty"
    Const colUnitCode As String = "UnitCode"
    Const colRemarks As String = "Remarks"


    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim isNewEntry As Boolean = True
    'Private Obj As clsmo
    Dim obj As New clsProductionPlanning
    Private ObjList As New List(Of clsProductionPlanning)
    Private isCellValueChangedOpen As Boolean = False
    Dim OpenFileDialog1 As New OpenFileDialog


    Sub LoadGridColumns()
        gvPP.Rows.Clear()
        gvPP.Columns.Clear()

        Dim LineNo As New GridViewTextBoxColumn
        Dim ProductionLineCode As New GridViewTextBoxColumn
        Dim BOMCode As New GridViewTextBoxColumn
        Dim RevisionNo As New GridViewTextBoxColumn
        Dim ItemCode As New GridViewTextBoxColumn
        Dim itemDesc As New GridViewTextBoxColumn
        'Dim MinQty As New GridViewDecimalColumn
        'Dim MaxQty As New GridViewDecimalColumn
        Dim PlanQty As New GridViewDecimalColumn
        Dim UnitCode As New GridViewTextBoxColumn
        Dim Remarks As New GridViewTextBoxColumn

        LineNo.FormatString = ""
        LineNo.HeaderText = "Line No"
        LineNo.Name = colLineNo
        LineNo.Width = 70
        LineNo.ReadOnly = True
        LineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvPP.Columns.Add(LineNo)

        'ProductionLineCode.FormatString = ""
        'ProductionLineCode.HeaderText = "Production Line"
        'ProductionLineCode.Name = colProductionLineCode
        'ProductionLineCode.Width = 100
        ''ProductionLineCode.ReadOnly = True
        'ProductionLineCode.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        'gvPP.Columns.Add(ProductionLineCode)

        BOMCode.FormatString = ""
        BOMCode.HeaderText = "BOM Code"
        BOMCode.Name = colBOMCode
        BOMCode.Width = 100
        'BOMCode.ReadOnly = True
        BOMCode.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvPP.Columns.Add(BOMCode)

        RevisionNo.FormatString = ""
        RevisionNo.HeaderText = "Revision No"
        RevisionNo.Name = colRevisionNo
        RevisionNo.Width = 100
        RevisionNo.ReadOnly = True
        RevisionNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvPP.Columns.Add(RevisionNo)

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
        itemDesc.Width = 100
        itemDesc.ReadOnly = True
        itemDesc.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvPP.Columns.Add(itemDesc)

        'MinQty.FormatString = ""
        'MinQty.HeaderText = "Min Qty"
        'MinQty.Name = colMinQty
        'MinQty.Width = 100
        'MinQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        'gvPP.Columns.Add(MinQty)

        'MaxQty.FormatString = ""
        'MaxQty.HeaderText = "Max Qty"
        'MaxQty.Name = colMaxQty
        'MaxQty.Width = 100
        'MaxQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        'gvPP.Columns.Add(MaxQty)

        PlanQty.FormatString = ""
        PlanQty.HeaderText = "Plan Qty"
        PlanQty.Name = colPlanQty
        PlanQty.Width = 100
        PlanQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvPP.Columns.Add(PlanQty)

        UnitCode.FormatString = ""
        UnitCode.HeaderText = "Unit Code"
        UnitCode.Name = colUnitCode
        UnitCode.Width = 60
        UnitCode.ReadOnly = True
        UnitCode.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvPP.Columns.Add(UnitCode)

        Remarks.FormatString = ""
        Remarks.HeaderText = "Remarks"
        Remarks.Name = colRemarks
        Remarks.Width = 130
        Remarks.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvPP.Columns.Add(Remarks)




    End Sub


    Private Sub frmProductionPlanningDemo_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
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

    Private Sub frmProductionPlanningDemo_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        LoadGridColumns()
        isNewEntry = True
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(btnPost, "Press Alt+P for  Post")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D  for Delete ")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnNew, "Press Alt+N Adding New ")
        '  ButtonToolTip.SetToolTip(btnPrint, "Press Alt+R for Print Preview")
        funReset()
        'Me.dtpBOMDate.Value = clsCommon.GETSERVERDATE
        'Me.lblCreatedByName.Text = objCommonVar.CurrentUserCode
        'Me.gvPP.Rows.Clear()
        'Me.gvPP.Rows.AddNew()
        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
    End Sub
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmDemoProductionPlanning)
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

        Dim serverDate As Date
        serverDate = clsCommon.GETSERVERDATE

        Me.dtpBOMDate.Value = serverDate
        Me.dtpPlanFromDate.Value = serverDate
        Me.dtpToDate.Value = serverDate
        Me.txtComments.Text = ""
        btnsave.Text = "Save"
        btnsave.Enabled = True
        btndelete.Enabled = True
        btnPost.Enabled = True
        Me.gvPP.Rows.Clear()
        Me.gvPP.Rows.AddNew()

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
        obj = clsProductionPlanning.GetData(strCode, NavTyep)
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.PROD_PLAN_CODE) > 0) Then

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
            txtCode.Value = obj.PROD_PLAN_CODE
            Me.txtDescription.Text = clsCommon.myCstr(obj.DESCRIPTION)
            Me.txtComments.Text = clsCommon.myCstr(obj.COMMENTS)
            Me.dtpBOMDate.Value = obj.PLANNING_DATE
            Me.dtpPlanFromDate.Value = obj.PLAN_FOR_DATE
            If Not obj.PLAN_TO_DATE Is Nothing Then
                Me.dtpToDate.Value = obj.PLAN_TO_DATE
            End If

            Me.lblCreatedByName.Text = obj.CREATED_BY
            Me.txtPlannedBy.Value = obj.PLANNED_BY
            Me.lblPlannedByName.Text = obj.PLANNED_BY_NAME

            If obj.POSTED = True Then
                Me.lblApprovedByName.Text = obj.APPROVED_BY
            Else
                Me.lblApprovedByName.Text = ""
            End If


            If (clsProductionPlanning.ObjList IsNot Nothing AndAlso clsProductionPlanning.ObjList.Count > 0) Then
                For Each obj As clsProductionPlanning In clsProductionPlanning.ObjList
                    gvPP.Rows.AddNew()

                    gvPP.Rows(gvPP.Rows.Count - 1).Cells(colLineNo).Value = obj.Line_No
                    'gvPP.Rows(gvPP.Rows.Count - 1).Cells(colProductionLineCode).Value = obj.PRODUCTION_LINE_CODE
                    gvPP.Rows(gvPP.Rows.Count - 1).Cells(colBOMCode).Value = obj.BOM_CODE
                    gvPP.Rows(gvPP.Rows.Count - 1).Cells(colRevisionNo).Value = obj.BOM_REVISION_NO
                    gvPP.Rows(gvPP.Rows.Count - 1).Cells(colItemCode).Value = obj.ITEM_CODE
                    gvPP.Rows(gvPP.Rows.Count - 1).Cells(colitemDesc).Value = obj.ITEM_DESCRIPTION
                    'gvPP.Rows(gvPP.Rows.Count - 1).Cells(colMinQty).Value = obj.MIN_QUANTITY
                    'gvPP.Rows(gvPP.Rows.Count - 1).Cells(colMaxQty).Value = obj.MAX_QUANTITY
                    gvPP.Rows(gvPP.Rows.Count - 1).Cells(colPlanQty).Value = obj.PLAN_QTY
                    gvPP.Rows(gvPP.Rows.Count - 1).Cells(colUnitCode).Value = obj.UNIT_CODE
                    gvPP.Rows(gvPP.Rows.Count - 1).Cells(colRemarks).Value = obj.REMARKS
                Next
            Else
                gvPP.Rows.AddNew()
            End If
        End If

    End Sub



    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        SavingData(False)
    End Sub
    Public Function Save() As Boolean
        If AllowToSave() Then
            Dim obj As New clsProductionPlanning
            obj.PROD_PLAN_CODE = Me.txtCode.Value
            obj.DESCRIPTION = Me.txtDescription.Text
            obj.COMMENTS = Me.txtComments.Text
            obj.PLANNING_DATE = Me.dtpBOMDate.Value
            obj.PLAN_FOR_DATE = Me.dtpPlanFromDate.Value
            obj.PLAN_TO_DATE = Me.dtpToDate.Value
            obj.CREATED_BY = Me.lblCreatedBy.Text
            obj.PLANNED_BY = clsCommon.myCstr(Me.txtPlannedBy.Value)

            Dim obj1 As clsProductionPlanning
            ObjList = New List(Of clsProductionPlanning)
            For Each grow As GridViewRowInfo In gvPP.Rows
                If clsCommon.myLen(clsCommon.myCstr(grow.Cells(colLineNo).Value)) > 0 And clsCommon.myLen(clsCommon.myCstr(grow.Cells(colItemCode).Value)) > 0 Then
                    obj1 = New clsProductionPlanning()

                    obj1.PROD_PLAN_CODE = txtCode.Value
                    obj1.Line_No = clsCommon.myCdbl(grow.Cells(colLineNo).Value)
                    'obj1.PRODUCTION_LINE_CODE = clsCommon.myCstr(grow.Cells(colProductionLineCode).Value)
                    obj1.BOM_CODE = clsCommon.myCstr(grow.Cells(colBOMCode).Value)
                    obj1.BOM_REVISION_NO = clsCommon.myCstr(grow.Cells(colRevisionNo).Value)
                    obj1.ITEM_CODE = clsCommon.myCstr(grow.Cells(colItemCode).Value)
                    obj1.ITEM_DESCRIPTION = clsCommon.myCstr(grow.Cells(colitemDesc).Value)
                    obj1.MIN_QUANTITY = clsCommon.myCdbl(grow.Cells(colPlanQty).Value)
                    obj1.MAX_QUANTITY = clsCommon.myCdbl(grow.Cells(colPlanQty).Value)
                    obj1.PLAN_QTY = clsCommon.myCdbl(grow.Cells(colPlanQty).Value)
                    obj1.UNIT_CODE = clsCommon.myCstr(grow.Cells(colUnitCode).Value)
                    obj1.REMARKS = clsCommon.myCstr(grow.Cells(colRemarks).Value)
                    ObjList.Add(obj1)
                End If
            Next
            Dim issaved As Boolean = False
            issaved = obj.SaveData(obj, ObjList, isNewEntry, clsCommon.myCstr(txtCode.Value))
            If issaved = True Then
                'clsCommon.MyMessageBoxShow("Document Save Successfully.")
                LoadData(obj.PROD_PLAN_CODE, NavigatorType.Current)
                Return True
            End If

            'Return False
        End If
        Return False
    End Function
    Function AllowToSave() As Boolean

        If btnsave.Text = "Update" Then
            Dim QryStr As String = "select POSTED from TSPL_MF_PRODUCTION_PLAN_HEAD where PROD_PLAN_CODE = '" + txtCode.Value + "' "
            Dim chkpost As String = clsDBFuncationality.getSingleValue(QryStr)
            If chkpost = "1" Then
                clsCommon.MyMessageBoxShow(Me, "Transection already posted", Me.Text)
                Return False
            End If
        End If

        If dtpPlanFromDate.Value > Me.dtpToDate.Value Then
            myMessages.blankValue(Me, "To Date must be greate than or equal to From Date", Me.Text)
            dtpToDate.Focus()
            Return False
        End If

        If clsCommon.myLen(txtPlannedBy.Value) <= 0 Then
            myMessages.blankValue(Me, "Planned By", Me.Text)
            txtCode.Focus()
            Return False
        End If

        Dim ii As Int16 = 0
        For Each grow As GridViewRowInfo In gvPP.Rows
            If clsCommon.myLen(clsCommon.myCstr(grow.Cells(colItemCode).Value)) > 0 Then
                ii += 1

                ObjList.Add(obj)
            End If

        Next
        If ObjList Is Nothing OrElse ObjList.Count <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "List is Empty.", Me.Text)
            Return False
        End If
        Return True
    End Function



    Private Sub btndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click
        DeleteData()
    End Sub
    Sub DeleteData()
        If clsCommon.myLen(txtCode.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "You Cannot Delete Record", Me.Text)
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
                If (clsProductionPlanning.DeleteData(txtCode.Value)) Then
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


    Private Sub txtCode__MYNavigator1(ByVal sender As Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtCode._MYNavigator
        Try
            LoadData(txtCode.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtCode__MYValidating1(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtCode._MYValidating

        Dim str As String = "select count(*) from TSPL_MF_PRODUCTION_PLAN_HEAD where PROD_PLAN_CODE ='" + txtCode.Value + "' "
        Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
        If no = 0 AndAlso isButtonClicked = False Then
            txtCode.MyReadOnly = False
            'txtCode.Value = ""
            '' common.clsCommon.MyMessageBoxShow("Value doesn't exist ")
        Else
            txtCode.MyReadOnly = True
        End If
        If txtCode.MyReadOnly OrElse isButtonClicked Then

            Dim qry As String = "SELECT T1.PROD_PLAN_CODE AS Code,T1.DESCRIPTION as Description,T1.COMMENTS as Comments,T1.PLANNING_DATE as [Planning Date],T1.PLANNED_BY as [Planned By],T2.EMP_NAME AS [Planned By Name],T1.PLAN_FOR_DATE as [From Date],T1.PLAN_TO_DATE AS [To Date],"
            qry += " T1.MODIFIED_BY AS APPROVED_BY,T1.Created_By,T1.POSTED,T1.POSTING_DATE FROM TSPL_MF_PRODUCTION_PLAN_HEAD  T1 INNER JOIN TSPL_EMPLOYEE_MASTER T2  ON T1.PLANNED_BY=T2.EMP_CODE "

            txtCode.Value = clsCommon.ShowSelectForm("TSPL_MF_PRODUCTION_PLAN_HEAD", qry, "Code", "", txtCode.Value, "Code", isButtonClicked)
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
                If (clsProductionPlanning.PostData(txtCode.Value, True)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Successfully Posted", Me.Text)
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




    Private Sub MenuItemClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        funClose()
    End Sub



    Private Sub gvBOM_CellEndEdit(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvPP.CellEndEdit
        If gvPP.CurrentRow Is Nothing Then
            Exit Sub
        End If

        If gvPP.CurrentRow.Cells(0).Value = "" Then
            gvPP.CurrentRow.Cells(0).Value = gvPP.RowCount
        End If

        If Not isCellValueChangedOpen Then
            isCellValueChangedOpen = True
            'If e.Column Is gvPP.Columns(colProductionLineCode) Then
            '    Dim strq As String = ""
            '    strq = "select PRODUCTION_LINE_CODE as Code,PRODUCTION_LINE_NAME AS NAME,DESCRIPTION from TSPL_MF_PRODUCTION_LINES "
            '    gvPP.CurrentRow.Cells(colProductionLineCode).Value = clsCommon.ShowSelectForm("cat", strq, "Code", "", clsCommon.myCstr(gvPP.CurrentRow.Cells(colProductionLineCode).Value))
            'End If


            If e.Column Is gvPP.Columns(colBOMCode) Then
                Dim obj As clsBillOfMaterial = clsBillOfMaterial.FinderForBOM(clsCommon.myCstr(gvPP.CurrentRow.Cells(colBOMCode).Value), False)
                If obj IsNot Nothing AndAlso clsCommon.myLen(obj.BOM_CODE) > 0 Then
                    gvPP.CurrentRow.Cells(colBOMCode).Value = obj.BOM_CODE
                    gvPP.CurrentRow.Cells(colRevisionNo).Value = obj.REVISION_NO
                    gvPP.CurrentRow.Cells(colItemCode).Value = obj.PROD_ITEM_CODE
                    gvPP.CurrentRow.Cells(colitemDesc).Value = obj.ITEM_DESCRIPTION
                    gvPP.CurrentRow.Cells(colUnitCode).Value = obj.PROD_ITEM_UNIT_CODE
                Else
                    gvPP.CurrentRow.Cells(colBOMCode).Value = ""
                    gvPP.CurrentRow.Cells(colRevisionNo).Value = ""
                    gvPP.CurrentRow.Cells(colItemCode).Value = ""
                    gvPP.CurrentRow.Cells(colitemDesc).Value = ""
                    gvPP.CurrentRow.Cells(colUnitCode).Value = ""
                End If
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

    Private Sub txtPlannedBy__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtPlannedBy._MYValidating
        Try
            Dim OBJEMP As New clsEmployeeMaster
            OBJEMP = clsEmployeeMaster.FinderForEmployee(Me.txtPlannedBy.Value, isButtonClicked)
            If Not OBJEMP Is Nothing Then
                Me.txtPlannedBy.Value = OBJEMP.EMP_CODE
                Me.lblPlannedByName.Text = clsEmployeeMaster.GetData(Me.txtPlannedBy.Value, NavigatorType.Current).Emp_Name
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        If txtCode.Value = "" Then
            myMessages.blankValue(Me, "Production Plan Code", Me.Text)
        Else
            funPrint()
        End If
    End Sub
    Sub funPrint()
        Try
            Dim qry As String = ""
            qry += " SELECT  T2.PROD_PLAN_CODE,convert(VARCHAR,T2.PLANNING_DATE,103) AS PLANNING_DATE,  T1.PRODUCTION_LINE_CODE,T4.PRODUCTION_LINE_NAME,T1.ITEM_CODE AS "
            qry += " MAIN_ITEM_CODE,T5.Item_Desc AS MAIN_ITEM_DESC,T6.Class_Code AS PACKAGE,T7.Class_Code AS FLAVOUR, "
            qry += " T3.MIN_QUANTITY AS MIN_QTY,T3.MAX_QUANTITY AS MAX_QTY,CONVERT(varchar(5),T3.START_TIME,108) AS START_TIME,T3.SPEED , "
            qry += " (CONVERT(VARCHAR,T2.PLANNING_DATE ,103) + ' ' + CONVERT(varchar(5),T3.END_TIME,108)) AS STOP_TIME,T3.REASON, ('" & objCommonVar.CurrentCompanyName & "')  AS COMPANY_NAME  FROM TSPL_MF_PROD_PLAN_DETAIL T1 INNER JOIN "
            qry += " TSPL_MF_PRODUCTION_PLAN_HEAD T2 ON T1.PROD_PLAN_CODE=T2.PROD_PLAN_CODE  "
            qry += " LEFT JOIN TSPL_MF_BATCH_PP_DETAIL T3 ON T2.PROD_PLAN_CODE=T3.PROD_PLAN_CODE "
            qry += " LEFT JOIN TSPL_MF_PRODUCTION_LINES T4 ON T1.PRODUCTION_LINE_CODE=T4.PRODUCTION_LINE_CODE "
            qry += " LEFT JOIN TSPL_ITEM_MASTER T5 ON T1.ITEM_CODE=T5.Item_Code "
            qry += " LEFT JOIN (SELECT ITEM_CODE,CLASS_CODE,CLASS_DESC FROM TSPL_ITEM_details WHERE Class_Name='SIZE') T6 ON T1.ITEM_CODE=T6.Item_Code "
            qry += " LEFT JOIN (SELECT ITEM_CODE,CLASS_CODE,CLASS_DESC FROM TSPL_ITEM_details WHERE Class_Name='FLAVOUR') T7 ON T1.ITEM_CODE=T7.Item_Code "
            qry += " WHERE 2=2"
            If txtCode.Value <> "" Then
                qry += " AND  T2.PROD_PLAN_CODE='" & clsCommon.myCstr(txtCode.Value) & "' "
            End If
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            Dim frmCRV As New frmCrystalReportViewer()
            frmCRV.funreport(CrystalReportFolder.PRODUCTION, dt, "crptProductionPlan", "Production Plan")
            frmCRV = Nothing
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class