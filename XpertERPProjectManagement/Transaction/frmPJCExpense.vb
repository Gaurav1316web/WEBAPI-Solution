Imports common
Imports System.Data
Imports System.Data.SqlClient

Public Class FrmPJCExpense
    Inherits FrmMainTranScreen
    Private isCellValueChangedOpen As Boolean = False
    Private isNewEntry As Boolean = False
    Private isInsideLoadData As Boolean = False
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim Qry As String
    Const colLineNo As String = "COLLNO"
    Const colExpenseCode As String = "ColExpenseCode"
    Const colExpenseDesc As String = "ColExpenseDesc"
    Const colExpenseType As String = "ColExpenseType"
    Const colIntegrateAP As String = "ColIntegrateAP"
    Const colGLAcct As String = "ColGLAcct"
    Const colBilling As String = "ColBilling"
    Const colCost As String = "COLCOST"
    Const colRemarks As String = "ColRemarks"

    Private Sub FrmPJCExpense_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnSave.Enabled Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btnDelete.Enabled Then
            DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag AndAlso btnPost.Enabled Then
            PostData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Close()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            AddNew()
        End If
    End Sub


    Private Sub FrmPJCExpense_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        AddNew()
        ''For Attachment
        If objCommonVar.IsDemoERP Then
            UcAttachment1.Form_ID = MyBase.Form_ID
            RadPageView1.Pages("Attachments").Item.Visibility = ElementVisibility.Visible
        Else
            RadPageView1.Pages("Attachments").Item.Visibility = ElementVisibility.Collapsed
        End If
        ''End of For Attachment

        '----------For Custom Fields----------
        RadPageView1.Pages("pvpCustomFields").Item.Visibility = MyBase.customFieldTabProperty
        If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
            UcCustomFields1.Report_ID = MyBase.Form_ID
            UcCustomFields1.LoadCustomControls()
        End If
        '---------End of For Custom Fields----
        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
    End Sub
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.FrmPJCExpense)
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow("Permission Denied")
            Me.Close()
            Exit Sub
        End If
        btnsave.Visible = MyBase.isModifyFlag
        btnPost.Visible = MyBase.isPostFlag
        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub
    Sub AddNew()
        txtDate.Value = clsCommon.GETSERVERDATE()
        BlankAllControls()
        LoadBlankGrid()
        isNewEntry = True
        btnSave.Text = "Save"
        btnSave.Enabled = True
        btnPost.Enabled = True
        btnDelete.Enabled = True
        txtDate.Focus()
        gv1.Rows.AddNew()
    End Sub
    Sub BlankAllControls()
        fndEmployee.Value = ""
        lblEmp.Text = ""
        txtDesc.Text = ""
        txtReference.Text = ""
        txtDocNo.Value = ""
        txtDate.Value = clsCommon.GETSERVERDATE()
        fndJob.Value = ""
        lblJob.Text = ""
        fndProject.Value = ""
        lblProject.Text = ""
        txtReference.Text = ""
        fndtask.Value = ""
        lblTask.Text = ""
        UcAttachment1.BlankAllControls()
        ''For Custom Fields
        If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
            UcCustomFields1.BlankAllControls()
        End If
    End Sub

    Sub LoadBlankGrid()
        gv1.Rows.Clear()
        gv1.Columns.Clear()

        Dim repoLineNo As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoLineNo = New GridViewDecimalColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "Line No"
        repoLineNo.Name = colLineNo
        repoLineNo.Width = 50
        repoLineNo.ReadOnly = True
        repoLineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoLineNo)

        Dim repoECode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoECode.FormatString = ""
        repoECode.HeaderText = "Expense Code"
        repoECode.Name = colExpenseCode
        repoECode.HeaderImage = My.Resources.search4
        repoECode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoECode.Width = 100
        repoECode.ReadOnly = False
        gv1.MasterTemplate.Columns.Add(repoECode)

        Dim repoEName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoEName.FormatString = ""
        repoEName.HeaderText = "Expense Description"
        repoEName.Name = colExpenseDesc
        repoEName.Width = 150
        repoEName.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoEName)


        Dim repoEType As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoEType.FormatString = ""
        repoEType.HeaderText = "Expense Type"
        repoEType.Name = colExpenseType
        repoEType.Width = 100
        repoEType.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoEType)

        Dim repoAP As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoAP.FormatString = ""
        repoAP.HeaderText = "Integrate With AP"
        repoAP.Name = colIntegrateAP
        repoAP.Width = 100
        repoAP.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoAP)

        Dim repoAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAmt.FormatString = ""
        repoAmt.HeaderText = "Cost"
        repoAmt.Name = colCost
        repoAmt.Width = 80
        repoAmt.Minimum = 0
        repoAmt.ReadOnly = False
        repoAmt.ShowUpDownButtons = False
        repoAmt.Step = 0
        repoAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoAmt)

        Dim repoBilling As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoBilling.FormatString = ""
        repoBilling.HeaderText = "Billing"
        repoBilling.Name = colBilling
        repoBilling.Width = 100
        repoBilling.ReadOnly = False
        repoBilling.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.MasterTemplate.Columns.Add(repoBilling)

        Dim repoRemarks As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoRemarks = New GridViewTextBoxColumn()
        repoRemarks.FormatString = ""
        repoRemarks.HeaderText = "Remarks"
        repoRemarks.Name = colRemarks
        repoRemarks.Width = 150
        gv1.MasterTemplate.Columns.Add(repoRemarks)

        Dim repoAcct As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoAcct.FormatString = ""
        repoAcct.HeaderText = "GL Account"
        repoAcct.Name = colGLAcct
        repoAcct.Width = 150
        repoAcct.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoAcct)

        clsCustomFieldGrid.LoadBlankGrid(gv1, MyBase.ArrDetailFields)

        gv1.AllowDeleteRow = True
        gv1.AllowAddNewRow = False
        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = False
        gv1.AllowRowReorder = False
        gv1.EnableSorting = False
        gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        gv1.TableElement.TableHeaderHeight = 40
    End Sub

    Private Sub gv1_CellValueChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellValueChanged
        Try
            If (Not isInsideLoadData) Then
                If Not isCellValueChangedOpen Then
                    isCellValueChangedOpen = True
                    If e.Column.FieldName.StartsWith("_CFLD_") Then
                        clsCustomFieldGrid.getFinderForCustomFieldGrid(gv1, e.Column.Name.ToString, MyBase.Form_ID)
                    End If
                    If e.Column Is gv1.Columns(colExpenseCode) OrElse e.Column Is gv1.Columns(colCost) Then
                        If e.Column Is gv1.Columns(colExpenseCode) Then
                            OpenECodeList(False)
                        ElseIf e.Column Is gv1.Columns(colCost) Then
                            UpdateTotal()
                        End If
                    End If
                    

                End If
                isCellValueChangedOpen = False
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Sub UpdateTotal()
        Dim TotCost As Decimal = 0.0
        Dim PaymentCost As Decimal = 0.0
        For Each grow As GridViewRowInfo In gv1.Rows
            If clsCommon.CompairString(grow.Cells(colIntegrateAP).Value, "Y") = CompairStringResult.Equal Then
                PaymentCost = PaymentCost + clsCommon.myCdbl(grow.Cells(colCost).Value)
            End If
            TotCost = TotCost + clsCommon.myCdbl(grow.Cells(colCost).Value)
        Next
        txtTotal.Text = TotCost
        txtPaymentTotal.Text = PaymentCost
    End Sub

    Sub OpenECodeList(ByVal isButtonClick As Boolean)
        Dim qry As String = "select EXPENSE_CODE as Code,DESCRIPTION,EXPENSE_TYPE,INTEGRATE_AP,GLACCOUNT from TSPL_EXPENSE_MASTER "
        Dim ExpCode As String = clsCommon.myCstr(clsCommon.ShowSelectForm("ExpenseCode", qry, "Code", "", gv1.CurrentRow.Cells(colExpenseCode).Value, "", False))
        If clsCommon.myLen(ExpCode) > 0 Then
            qry = "select EXPENSE_CODE as Code,DESCRIPTION,EXPENSE_TYPE,INTEGRATE_AP,GLACCOUNT from TSPL_EXPENSE_MASTER where EXPENSE_CODE='" & ExpCode & "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                gv1.CurrentRow.Cells(colExpenseCode).Value = clsCommon.myCstr(dt.Rows(0)("Code"))
                gv1.CurrentRow.Cells(colExpenseDesc).Value = clsCommon.myCstr(dt.Rows(0)("DESCRIPTION"))
                gv1.CurrentRow.Cells(colExpenseType).Value = clsCommon.myCstr(dt.Rows(0)("EXPENSE_TYPE"))
                gv1.CurrentRow.Cells(colIntegrateAP).Value = clsCommon.myCstr(dt.Rows(0)("INTEGRATE_AP"))
                gv1.CurrentRow.Cells(colGLAcct).Value = clsCommon.myCstr(dt.Rows(0)("GLACCOUNT"))
            Else
                SetBlankOfItemColumns()
            End If
        End If

    End Sub

    Private Sub SetBlankOfItemColumns()
        gv1.CurrentRow.Cells(colExpenseCode).Value = ""
        gv1.CurrentRow.Cells(colExpenseDesc).Value = ""
        gv1.CurrentRow.Cells(colExpenseType).Value = ""
        gv1.CurrentRow.Cells(colIntegrateAP).Value = 0
        gv1.CurrentRow.Cells(colGLAcct).Value = 0
        gv1.CurrentRow.Cells(colCost).Value = 0
        gv1.CurrentRow.Cells(colBilling).Value = 0
        gv1.CurrentRow.Cells(colRemarks).Value = 0
    End Sub

    Private Sub gv1_CurrentColumnChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gv1.CurrentColumnChanged
        If gv1.RowCount > 0 Then
            Dim intCurrRow As Integer = gv1.CurrentRow.Index
            gv1.CurrentRow.Cells(colLineNo).Value = clsCommon.myCdbl(intCurrRow + 1)
            If intCurrRow = gv1.Rows.Count - 1 Then
                gv1.Rows.AddNew()
                gv1.CurrentRow = gv1.Rows(intCurrRow)
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

    Private Sub gv1_UserDeletingRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs) Handles gv1.UserDeletingRow
        If common.clsCommon.MyMessageBoxShow("Delete The Current Row." + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
            e.Cancel = True
            UpdateTotal()
        End If
    End Sub

    Private Sub fndJob__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndJob._MYValidating
        Dim qry As String = "select JOB_CODE as Code,DESCRIPTION,JOB_TYPE from TSPL_PJC_JOB"
        fndJob.Value = clsCommon.ShowSelectForm("Job Code", qry, "Code", "", fndJob.Value, "", isButtonClicked)
        lblJob.Text = clsDBFuncationality.getSingleValue("select DESCRIPTION from TSPL_PJC_JOB where JOB_CODE='" + fndJob.Value + "'")
    End Sub

    Private Sub fndEmployee__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndEmployee._MYValidating
        Dim qry As String = "select EMP_CODE as Code,Emp_Name as Name from TSPL_EMPLOYEE_MASTER"
        fndEmployee.Value = clsCommon.ShowSelectForm("Employee Code", qry, "Code", "", fndEmployee.Value, "", isButtonClicked)
        lblEmp.Text = clsDBFuncationality.getSingleValue("select Emp_Name from TSPL_EMPLOYEE_MASTER where EMP_CODE='" + fndEmployee.Value + "'")
    End Sub

    Private Sub fndtask__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndtask._MYValidating
        Dim qry As String = "select TASK_CODE as Code,DESCRIPTION,TASK_TYPE as Type from TSPL_PJC_TASK"
        fndtask.Value = clsCommon.ShowSelectForm("Task Code", qry, "Code", "", fndtask.Value, "", isButtonClicked)
        lblTask.Text = clsDBFuncationality.getSingleValue("select DESCRIPTION from TSPL_PJC_TASK where TASK_CODE='" + fndtask.Value + "'")
    End Sub

    Private Sub fndProject__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndProject._MYValidating
        Dim qry As String = "select PROJECT_CODE as Code,SPECIFICATION,PROJECT_STATUS as Status from TSPL_PJC_PROJECT"
        fndProject.Value = clsCommon.ShowSelectForm("Project Code", qry, "Code", "", fndProject.Value, "", isButtonClicked)
        lblProject.Text = clsDBFuncationality.getSingleValue("select SPECIFICATION from TSPL_PJC_PROJECT where PROJECT_CODE='" + fndProject.Value + "'")
    End Sub
    Private Sub txtDocNo__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtDocNo._MYNavigator
        Try
            Dim qst As String = "select count(*) from TSPL_PJC_EXPENSE_Header where Document_No='" + txtDocNo.Value + "'"
            Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qst))
            If count = 0 Then
                txtDocNo.MyReadOnly = False
            Else
                txtDocNo.MyReadOnly = True
            End If
            LoadData(txtDocNo.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub txtDocNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtDocNo._MYValidating
        Dim qry As String = "SELECT Document_No AS [DocumentNo], Document_Date as [Date], case when Posted='Y' then 'Yes' else 'No' end as Posted,EMP_CODE as [Employee],Project_Code as [Project] FROM  TSPL_PJC_EXPENSE_Header  "
        txtDocNo.Value = clsCommon.ShowSelectForm("ExpenseDoc", qry, "DocumentNo", "", txtDocNo.Value, "DocumentNo", isButtonClicked)
        LoadData(txtDocNo.Value, NavigatorType.Current)
    End Sub
    Private Function AllowToSave() As Boolean

        If clsCommon.myLen(fndEmployee.Value) <= 0 Then
            fndEmployee.Focus()
            Throw New Exception("Please select Employee")
        End If
        If clsCommon.myLen(fndProject.Value) <= 0 Then
            fndProject.Focus()
            Throw New Exception("Please select Project")
        End If

        Dim Counter As Integer = 0
        For ii As Integer = 0 To gv1.Rows.Count - 1
            Dim strECode As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colExpenseCode).Value)
            Dim dblCost As Double = clsCommon.myCdbl(gv1.Rows(ii).Cells(colCost).Value)
            Counter += 1
            If clsCommon.myLen(strECode) > 0 Then
                If clsCommon.myLen(gv1.Rows(ii).Cells(colCost).Value) <= 0 Then
                    Throw New Exception("Please enter Cost of Expense Code " + strECode + " at Row No " + clsCommon.myCstr(ii + 1))
                End If
            End If
        Next
        If Counter <= 0 Then
            Throw New Exception("Please enter atleast single Expense Code")
        End If
        UcCustomFields1.AllowToSave()
        UcAttachment1.Form_ID = MyBase.Form_ID
        UcAttachment1.AllowToSave()
        Return True
    End Function
    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        SaveData()
    End Sub
    Private Function SaveData() As Boolean
        Try
            If (AllowToSave()) Then

                Dim obj As New clsPJCExpense()

                obj.Document_No = txtDocNo.Value
                obj.Document_Date = txtDate.Value
                obj.Reference = txtReference.Text
                obj.Description = txtDesc.Text
                obj.EMP_CODE = fndEmployee.Value
                obj.PROJECT_CODE = fndProject.Value
                obj.Task_Code = fndtask.Value
                obj.Job_Code = fndJob.Value
                obj.TotalCost = clsCommon.myCdbl(txtTotal.Text)
                obj.TotPaymentCost = clsCommon.myCdbl(txtPaymentTotal.Text)
                ''For Custom Fields
                obj.Form_ID = MyBase.Form_ID
                obj.arrCustomFields = New List(Of clsCustomFieldValues)
                If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
                    UcCustomFields1.GetData(obj.arrCustomFields)
                End If
                ''End of For Custom Fields

                obj.Arr = New List(Of ClsPJCExpenseDetail)()
                Dim isFirstTime As Boolean = True
                For Each grow As GridViewRowInfo In gv1.Rows
                    Dim objTr As New ClsPJCExpenseDetail()
                    objTr.Document_Line_No = clsCommon.myCdbl(grow.Cells(colLineNo).Value)
                    objTr.EXPENSE_CODE = clsCommon.myCstr(grow.Cells(colExpenseCode).Value)
                    objTr.DESCRIPTION = clsCommon.myCstr(grow.Cells(colExpenseDesc).Value)
                    objTr.EXPENSE_TYPE = clsCommon.myCstr(grow.Cells(colExpenseType).Value)
                    objTr.INTEGRATE_AP = clsCommon.myCstr(grow.Cells(colIntegrateAP).Value)
                    objTr.GLACCOUNT = clsCommon.myCstr(grow.Cells(colGLAcct).Value)
                    objTr.Billing = IIf(clsCommon.myCBool(grow.Cells(colBilling).Value), "Y", "N")
                    objTr.Cost = clsCommon.myCdbl(grow.Cells(colCost).Value)
                    objTr.Comments = clsCommon.myCstr(grow.Cells(colRemarks).Value)
                   
                    If isFirstTime Then
                        isFirstTime = False
                    End If


                    If (clsCommon.myLen(objTr.EXPENSE_CODE) > 0) Then
                        obj.Arr.Add(objTr)
                    End If

                Next
                If (obj.Arr Is Nothing OrElse obj.Arr.Count <= 0) Then
                    Throw New Exception("Please Fill at list one Item")
                End If

                Dim isSaved As Boolean = obj.SaveData(obj, isNewEntry)
                UcAttachment1.Form_ID = MyBase.Form_ID
                UcAttachment1.SaveData(obj.Document_No)
                clsCommon.MyMessageBoxShow("Data Saved Successfully", Me.Text)
                LoadData(obj.Document_No, NavigatorType.Current)
                Return isSaved
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Function
    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Try
            btnSave.Enabled = True
            btnPost.Enabled = True
            btnDelete.Enabled = True
            isInsideLoadData = True
            isNewEntry = False
            btnSave.Text = "Update"
            BlankAllControls()
            LoadBlankGrid()


            Dim obj As New clsPJCExpense()
            obj = clsPJCExpense.GetData(strCode, NavTyep, Nothing)

            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Document_No) > 0) Then
                If clsCommon.CompairString(obj.Posted, "Y") = CompairStringResult.Equal Then
                    btnSave.Enabled = False
                    btnPost.Enabled = False
                    btnDelete.Enabled = False
                End If

                txtDocNo.Value = obj.Document_No
                txtDate.Value = obj.Document_Date
                txtReference.Text = obj.Reference
                txtDesc.Text = obj.Description
                fndEmployee.Value = obj.EMP_CODE
                lblEmp.Text = clsDBFuncationality.getSingleValue("select Emp_Name from TSPL_EMPLOYEE_MASTER where EMP_CODE='" + fndEmployee.Value + "'")
                fndProject.Value = obj.PROJECT_CODE
                lblProject.Text = clsDBFuncationality.getSingleValue("select SPECIFICATION from TSPL_PJC_PROJECT where PROJECT_CODE='" + fndProject.Value + "'")
                fndJob.Value = obj.Job_Code
                lblJob.Text = clsDBFuncationality.getSingleValue("select DESCRIPTION from TSPL_PJC_JOB where JOB_CODE='" + fndJob.Value + "'")
                fndtask.Value = obj.Task_Code
                lblTask.Text = clsDBFuncationality.getSingleValue("select DESCRIPTION from TSPL_PJC_TASK where TASK_CODE='" + fndtask.Value + "'")
                txtTotal.Text = obj.TotalCost
                txtPaymentTotal.Text = obj.TotPaymentCost
                If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                    For Each objTr As ClsPJCExpenseDetail In obj.Arr
                        gv1.Rows.AddNew()
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = objTr.Document_Line_No
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colExpenseCode).Value = objTr.EXPENSE_CODE
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colExpenseDesc).Value = objTr.DESCRIPTION
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colExpenseType).Value = objTr.EXPENSE_TYPE
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colIntegrateAP).Value = objTr.INTEGRATE_AP
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colGLAcct).Value = objTr.GLACCOUNT
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colRemarks).Value = objTr.Comments
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colCost).Value = objTr.Cost
                        If objTr.Billing = "Y" Then
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colBilling).Value = True
                        Else
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colBilling).Value = False
                        End If
                    Next

                End If
                ''For Custom Fields
                If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
                    UcCustomFields1.LoadData(obj.Document_No)
                End If
                clsCustomFieldGrid.FillDataInGrid(obj.Document_No, MyBase.Form_ID, gv1)
                UcAttachment1.Form_ID = MyBase.Form_ID
                UcAttachment1.LoadData(obj.Document_No)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        Finally
            isInsideLoadData = False
        End Try
    End Sub

    Private Sub btnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
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
                If (clsPJCExpense.DeleteData(txtDocNo.Value)) Then
                    saveCancelLog(Reason, "Delete", Nothing)
                    common.clsCommon.MyMessageBoxShow("Data Deleted Successfully ")
                    AddNew()
                End If
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub
    Function saveCancelLog(ByVal Reason As String, ByVal Activity_Type As String, Optional ByVal trans As System.Data.SqlClient.SqlTransaction = Nothing) As Boolean
        Dim obj As New clsCancelLog
        obj.Program_Code = Form_ID
        obj.DOCUMENT_NO = clsCommon.myCstr(Me.txtDocNo.Value)
        obj.REASON = Reason
        obj.ACTIVITY_TYPE = Activity_Type
        Return clsCancelLog.SaveData(obj, True, trans)
    End Function

    Private Sub btnPost_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPost.Click
        PostData()
    End Sub
    Private Function PostData()
        Try
            If (myMessages.postConfirm()) Then
                If clsCommon.myLen(txtPaymentTotal.Text) > 0 AndAlso txtPaymentTotal.Text > 0 Then
                    Dim strbankcode As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select top 1  BANK_CODE from TSPL_PJC_SETTINGS"))
                    If clsCommon.myLen(strbankcode) <= 0 Then
                        clsCommon.MyMessageBoxShow("Bank Code not found in PJC Settings")
                        Return False
                    End If
                    Dim strPaymentMode As String = ""
                    If Not String.IsNullOrEmpty(connectSql.RunScalar("select bank_type from tspl_bank_master where bank_code = '" + strbankcode + " '")) Then
                        Dim strbankType = connectSql.RunScalar("select bank_type from tspl_bank_master where bank_code = '" + strbankcode + "'")
                        If strbankType.Trim() = "C" Then
                            strPaymentMode = clsDBFuncationality.getSingleValue("select top 1 Payment_Code as [PaymentMode]  from TSPL_PAYMENT_CODE where PAYMENT_TYPE = 'CASH' ")
                        ElseIf strbankType.Trim() = "P" Then
                            strPaymentMode = clsDBFuncationality.getSingleValue("select top 1 Payment_Code as [PaymentMode]  from TSPL_PAYMENT_CODE where PAYMENT_TYPE = 'Petty Cash'")
                        ElseIf strbankType = "B" Then
                            strPaymentMode = clsDBFuncationality.getSingleValue("select top 1 Payment_Code as [PaymentMode]  from TSPL_PAYMENT_CODE where PAYMENT_TYPE IN ('Cheque', 'Other') ")
                        Else
                            strPaymentMode = clsDBFuncationality.getSingleValue("select top 1 Payment_Code as [PaymentMode]  from TSPL_PAYMENT_CODE where PAYMENT_TYPE = 'Other'")
                        End If
                    End If
                    Dim obj As New clsPaymentHeader()


                    obj.Payment_Date = clsCommon.myCDate(txtDate.Value)
                    obj.Payment_Post_Date = clsCommon.myCDate(txtDate.Value)
                    obj.Bank_Code = clsCommon.myCstr(strbankcode)
                    obj.Payment_Type = "MI"
                    obj.Vendor_Code = ""
                    obj.Vendor_Name = ""
                    obj.Payment_Code = strPaymentMode
                    obj.Cheque_No = ""
                    obj.Cheque_Date = Nothing
                    obj.EMP_CODE = clsCommon.myCstr(fndEmployee.Value)
                    obj.PROJECT_CODE = clsCommon.myCstr(fndProject.Value)
                    obj.Document_No = clsCommon.myCstr(txtDocNo.Value)
                    obj.Payment_Amount = clsCommon.myCdbl(txtPaymentTotal.Text)
                    obj.Total_Applied_Amount = clsCommon.myCdbl(txtPaymentTotal.Text)
                    obj.Remit_To = ""
                    obj.Loadout_No = ""

                    obj.IsChkReverse = "N"
                    obj.CFormRecd = "N"
                    obj.CForm_InvoiceNo = ""
                    Dim EntryDesc = "Expense done for " & txtDocNo.Value & "  Project No " & fndProject.Value & "  for employee " & fndEmployee.Value & " "
                    obj.Entry_Desc = EntryDesc
                    obj.ArrTr = New List(Of clsPaymentDetail)



                    '============================Detail Section==============================


                    Dim strRemarks As String
                    Dim MiscAmt As Decimal = 0.0
                    Dim ESI_Percent As Decimal = 0.0


                    For Each grow As GridViewRowInfo In gv1.Rows
                        If clsCommon.CompairString(grow.Cells(colIntegrateAP).Value, "Y") = CompairStringResult.Equal Then
                            Dim objTr As New clsPaymentDetail()
                            objTr.Payment_Type = "MI"
                            objTr.Account_Code = clsCommon.myCstr(grow.Cells(colGLAcct).Value)
                            objTr.Description = clsDBFuncationality.getSingleValue("select description from tspl_gl_accounts where account_code='" + objTr.Account_Code + "' ")
                            objTr.Net_Balance = clsCommon.myCdbl(grow.Cells(colCost).Value)
                            strRemarks = "Expense done for " & clsCommon.myCstr(grow.Cells(colExpenseCode).Value) & " for   Project No " & fndProject.Value & "  for employee " & fndEmployee.Value & " "
                            objTr.Remarks = strRemarks
                            objTr.ESI_WCT_Percentage = ESI_Percent
                            objTr.EXPENSE_CODE = clsCommon.myCstr(grow.Cells(colExpenseCode).Value)
                            obj.ArrTr.Add(objTr)
                        End If
                    Next

                    '==================Detail Section Ends Here=======================

                    obj.SaveData(obj, True)
                    UcAttachment1.Form_ID = "PYMT-NEW"
                    UcAttachment1.SaveData(obj.Payment_No)
                Else

                    Qry = "Update TSPL_PJC_EXPENSE_HEADER set  Posted='Y' ,Posting_Date='" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy hh:mm tt") & "' WHERE Document_No ='" + txtDocNo.Value + "'"
                    clsDBFuncationality.ExecuteNonQuery(Qry)

                End If
                LoadData(txtDocNo.Value, NavigatorType.Current)
                clsCommon.MyMessageBoxShow("Data Posted Successfully", Me.Text)

            End If
            Return True
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
            Return False
        End Try
    End Function
    Sub SetLength()
        txtDocNo.MyMaxLength = 30
        txtDesc.MaxLength = 200
        txtReference.MaxLength = 200
    End Sub
    Private Sub btnAddNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddNew.Click
        AddNew()
    End Sub

    
End Class
