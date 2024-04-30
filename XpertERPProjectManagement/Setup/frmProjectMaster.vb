Imports common
Imports System

Public Class FrmProjectMaster
    Inherits FrmMainTranScreen

#Region "Variables"
    Dim isInsideLoadData As Boolean = False
    Dim isCellValueChangedJob As Boolean = False
    Dim isCellValueChangedTask As Boolean = False

    Dim isNewEntry As Boolean = False
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private isCellValueChangedOpen As Boolean = False
    Private IsFormLoad As Boolean = False

    Const colJobSNo As String = "colJobSNo"
    Const colJobCode As String = "colJobCode"
    Const colJobDesc As String = "colJobDesc"
    Const colJobType As String = "colJobType"
    Const colJobAccountMethod As String = "colJobAccountMethod"
    Const colJobBillingType As String = "colJobBillingType"
    Const colJobCloseToBill As String = "colJobCloseToBill"
    Const colJobCloseToCost As String = "colJobCloseToCost"

    Const colJobStartDate As String = "colJobStartDate"
    Const colJobEndDate As String = "colJobEndDate"
    Const colJobStatus As String = "colJobStatus"
    Const colJobStatusDate As String = "colJobStatusDate"

    Const colTaskSNo As String = "colTaskSNo"
    Const colTaskCode As String = "colTaskTaskCode"
    Const colTaskDesc As String = "colTaskTaskDesc"
    Const colTaskBillingType As String = "colTaskBillingType"
    Const colTaskQty As String = "colTaskQty"
    Const colTaskCost As String = "colTaskCost"
    Const colTaskAmt As String = "colTaskAmt"
    Const colTaskCostPlus As String = "colTaskCostPlus"
    Const colTaskBillingAmt As String = "colTaskBillingAmt"
    Const colTaskType As String = "colTaskType"
    Const colTaskVendorCode As String = "colTaskVendorCode"
    Const colTaskVendorDescription As String = "colTaskVendorDescription"
    Const colTaskStartDate As String = "colTaskStartDate"
    Const colTaskEndDate As String = "colTaskEndDate"
    Const colTaskStatus As String = "colTaskStatus"
    Const colTaskStatusDate As String = "colTaskStatusDate"
#End Region

    Private Sub FrmProjectMaster_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnSave.Enabled Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btnDelete.Enabled Then
            DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Close()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            AddNew()
        End If
    End Sub

    Private Sub FrmProjectMaster_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        SetUserMgmtNew()

        txtCustomer.MendatroryField = True

        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnAddNew, "Press Alt+N Adding New Trasnaction")

        RadPageView1.SelectedPage = RadPageViewPage1
        LoadBlankGridJob()
        LoadBlankGridTask()
        IsFormLoad = True
        LoadStatus()
        LoadProjectType()
        LoadAccountMethod()
        AddNew()
        SetLength()
        IsFormLoad = False

        ''For Custom Fields
        RadPageView1.Pages("pvpCustomFields").Item.Visibility = MyBase.customFieldTabProperty
        If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
            UcCustomFields1.Report_ID = MyBase.Form_ID
            UcCustomFields1.LoadCustomControls()
        End If

        ''For Attachment
        RadPageView1.Pages("Attachments").Item.Visibility = ElementVisibility.Collapsed
        If objCommonVar.IsDemoERP Then
            UcAttachment1.Form_ID = MyBase.Form_ID
            RadPageView1.Pages("Attachments").Item.Visibility = ElementVisibility.Visible
        End If
        ''End of For Attachment

    End Sub

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.ProjectMaster)
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow("Permission Denied")
            Me.Close()
            Exit Sub
        End If
        btnSave.Visible = MyBase.isModifyFlag
        'btnPost.Visible = MyBase.isPostFlag
        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    Sub LoadBlankGridJob()
        gvJob.Rows.Clear()
        gvJob.Columns.Clear()

        Dim repoSNo As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoSNo.FormatString = ""
        repoSNo.HeaderText = "SNo"
        repoSNo.Name = colJobSNo
        repoSNo.Width = 30
        repoSNo.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        repoSNo.ReadOnly = True
        gvJob.MasterTemplate.Columns.Add(repoSNo)

        Dim repoRowType As GridViewComboBoxColumn = New GridViewComboBoxColumn()
        repoRowType.FormatString = ""
        repoRowType.HeaderText = "Billing Type"
        repoRowType.Name = colJobBillingType
        repoRowType.Width = 100
        repoRowType.ReadOnly = False
        repoRowType.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        repoRowType.DataSource = GetBillingType()
        repoRowType.ValueMember = "Code"
        repoRowType.DisplayMember = "Code"
        gvJob.MasterTemplate.Columns.Add(repoRowType)


        Dim repoJobCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoJobCode.FormatString = ""
        repoJobCode.HeaderText = "Job Code"
        repoJobCode.Name = colJobCode
        repoJobCode.HeaderImage = My.Resources.search4
        repoJobCode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoJobCode.Width = 100
        repoJobCode.ReadOnly = False
        gvJob.MasterTemplate.Columns.Add(repoJobCode)

        Dim repoJobDesc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoJobDesc.FormatString = ""
        repoJobDesc.HeaderText = "Job Description"
        repoJobDesc.Name = colJobDesc
        repoJobDesc.Width = 150
        repoJobDesc.ReadOnly = True
        gvJob.MasterTemplate.Columns.Add(repoJobDesc)

        Dim repoJobType As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoJobType.FormatString = ""
        repoJobType.HeaderText = "Job Type"
        repoJobType.Name = colJobType
        repoJobType.Width = 100
        repoJobType.ReadOnly = True
        gvJob.MasterTemplate.Columns.Add(repoJobType)

        Dim repoJobAccount As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoJobAccount.FormatString = ""
        repoJobAccount.HeaderText = "Account Method"
        repoJobAccount.Name = colJobAccountMethod
        repoJobAccount.Width = 100
        repoJobAccount.ReadOnly = True
        gvJob.MasterTemplate.Columns.Add(repoJobAccount)

        
        Dim repoCloseToBill As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoCloseToBill.HeaderText = "Close to Bill"
        repoCloseToBill.Width = 100
        repoCloseToBill.Name = colJobCloseToBill
        repoCloseToBill.ReadOnly = False
        repoCloseToBill.IsVisible = True
        repoCloseToBill.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gvJob.MasterTemplate.Columns.Add(repoCloseToBill)


        Dim repoCloseToCost As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoCloseToCost.HeaderText = "Close to Cost"
        repoCloseToCost.Width = 100
        repoCloseToCost.Name = colJobCloseToCost
        repoCloseToCost.ReadOnly = False
        repoCloseToCost.IsVisible = True
        repoCloseToCost.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gvJob.MasterTemplate.Columns.Add(repoCloseToCost)

        Dim repoStartDate As GridViewDateTimeColumn = New GridViewDateTimeColumn()
        repoStartDate.Format = DateTimePickerFormat.Custom
        repoStartDate.CustomFormat = "dd-MM-yyyy"
        repoStartDate.HeaderText = "Start Date"
        repoStartDate.FormatString = "{0:d}"
        repoStartDate.Name = colJobStartDate
        repoStartDate.WrapText = True
        repoStartDate.ReadOnly = False
        repoStartDate.Width = 80
        gvJob.MasterTemplate.Columns.Add(repoStartDate)

        Dim repoEndDate As GridViewDateTimeColumn = New GridViewDateTimeColumn()
        repoEndDate.Format = DateTimePickerFormat.Custom
        repoEndDate.CustomFormat = "dd-MM-yyyy"
        repoEndDate.HeaderText = "End Date"
        repoEndDate.FormatString = "{0:d}"
        repoEndDate.Name = colJobEndDate
        repoEndDate.WrapText = True
        repoEndDate.ReadOnly = False
        repoEndDate.Width = 100
        gvJob.MasterTemplate.Columns.Add(repoEndDate)

        Dim repoStatus As GridViewComboBoxColumn = New GridViewComboBoxColumn()
        repoStatus.FormatString = ""
        repoStatus.HeaderText = "Status"
        repoStatus.Name = colJobStatus
        repoStatus.Width = 50
        repoStatus.ReadOnly = False
        repoStatus.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        repoStatus.DataSource = GetStatus()
        repoStatus.ValueMember = "Code"
        repoStatus.DisplayMember = "Code"
        gvJob.MasterTemplate.Columns.Add(repoStatus)

        Dim repoStatusDate As GridViewDateTimeColumn = New GridViewDateTimeColumn()
        repoStatusDate.Format = DateTimePickerFormat.Custom
        repoStatusDate.CustomFormat = "dd-MM-yyyy"
        repoStatusDate.HeaderText = "Status Date"
        repoStatusDate.FormatString = "{0:d}"
        repoStatusDate.Name = colJobStatusDate
        repoStatusDate.WrapText = True
        repoStatusDate.ReadOnly = False
        repoStatusDate.Width = 80
        gvJob.MasterTemplate.Columns.Add(repoStatusDate)

        gvJob.AllowAddNewRow = False
        gvJob.ShowGroupPanel = False
        gvJob.AllowColumnReorder = True
        gvJob.AllowRowReorder = False
        gvJob.EnableSorting = False
        gvJob.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gvJob.MasterTemplate.ShowRowHeaderColumn = False
        gvJob.TableElement.TableHeaderHeight = 20
    End Sub

    Public Shared Function GetStatus() As DataTable
        Dim dt As New DataTable()
        dt.Columns.Add("Code", GetType(String))
        Dim dr As DataRow = dt.NewRow()
        dr("Code") = ""
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Open"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "WIP"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Complete"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Hold"
        dt.Rows.Add(dr)

        Return dt
    End Function

    Sub LoadBlankGridTask()
        gvTask.Rows.Clear()
        gvTask.Columns.Clear()

        Dim repoSNo As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoSNo.FormatString = ""
        repoSNo.HeaderText = "SNo"
        repoSNo.Name = colTaskSNo
        repoSNo.Width = 30
        repoSNo.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        repoSNo.ReadOnly = True
        gvTask.MasterTemplate.Columns.Add(repoSNo)

        Dim repoTaskCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTaskCode.FormatString = ""
        repoTaskCode.HeaderText = "Task Code"
        repoTaskCode.Name = colTaskCode
        repoTaskCode.HeaderImage = My.Resources.search4
        repoTaskCode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoTaskCode.Width = 100
        repoTaskCode.ReadOnly = False
        gvTask.MasterTemplate.Columns.Add(repoTaskCode)

        Dim repoTaskDesc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTaskDesc.FormatString = ""
        repoTaskDesc.HeaderText = "Task Description"
        repoTaskDesc.Name = colTaskDesc
        repoTaskDesc.Width = 150
        repoTaskDesc.ReadOnly = True
        gvTask.MasterTemplate.Columns.Add(repoTaskDesc)

        
        Dim repoBillingType As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoBillingType.FormatString = ""
        repoBillingType.HeaderText = "Billing Type"
        repoBillingType.Name = colTaskBillingType
        repoBillingType.Width = 80
        repoBillingType.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoBillingType.ReadOnly = True
        gvTask.MasterTemplate.Columns.Add(repoBillingType)

        Dim repoTaskQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaskQty.DecimalPlaces = 0
        repoTaskQty.FormatString = "{0:n0}"
        repoTaskQty.HeaderText = "Qty"
        repoTaskQty.Name = colTaskQty
        repoTaskQty.Width = 80
        repoTaskQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoTaskQty.ReadOnly = False
        gvTask.MasterTemplate.Columns.Add(repoTaskQty)

        Dim repoTaskCost As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaskCost.DecimalPlaces = 2
        repoTaskCost.FormatString = "{0:n2}"
        repoTaskCost.HeaderText = "Cost"
        repoTaskCost.Name = colTaskCost
        repoTaskCost.Width = 80
        repoTaskCost.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoTaskCost.ReadOnly = False
        gvTask.MasterTemplate.Columns.Add(repoTaskCost)

        Dim repoTaskAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaskAmt.DecimalPlaces = 2
        repoTaskAmt.FormatString = "{0:n2}"
        repoTaskAmt.HeaderText = "Amount"
        repoTaskAmt.Name = colTaskAmt
        repoTaskAmt.Width = 80
        repoTaskAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoTaskAmt.ReadOnly = True
        gvTask.MasterTemplate.Columns.Add(repoTaskAmt)

        Dim repoCostPlus As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoCostPlus.DecimalPlaces = 2
        repoCostPlus.FormatString = "{0:n2}"
        repoCostPlus.HeaderText = "Cost Plus"
        repoCostPlus.Name = colTaskCostPlus
        repoCostPlus.Width = 80
        repoCostPlus.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoCostPlus.ReadOnly = False
        gvTask.MasterTemplate.Columns.Add(repoCostPlus)

        Dim repoCostBillingAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoCostBillingAmt.DecimalPlaces = 2
        repoCostBillingAmt.FormatString = "{0:n2}"
        repoCostBillingAmt.HeaderText = "Billing Amount"
        repoCostBillingAmt.Name = colTaskBillingAmt
        repoCostBillingAmt.Width = 80
        repoCostBillingAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoCostBillingAmt.ReadOnly = False
        gvTask.MasterTemplate.Columns.Add(repoCostBillingAmt)

        Dim repoTaskTypeExternal As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoTaskTypeExternal.HeaderText = "External Task "
        repoTaskTypeExternal.Name = colTaskType
        repoTaskTypeExternal.ReadOnly = False
        repoTaskTypeExternal.IsVisible = True
        repoTaskTypeExternal.Width = 80
        repoTaskTypeExternal.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gvTask.MasterTemplate.Columns.Add(repoTaskTypeExternal)

        Dim repoVendorCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoVendorCode.FormatString = ""
        repoVendorCode.HeaderText = "Vendor Code"
        repoVendorCode.Name = colTaskVendorCode
        repoVendorCode.HeaderImage = My.Resources.search4
        repoVendorCode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoVendorCode.Width = 100
        repoVendorCode.ReadOnly = False
        gvTask.MasterTemplate.Columns.Add(repoVendorCode)

        Dim repoVendorDesc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoVendorDesc.FormatString = ""
        repoVendorDesc.HeaderText = "Vendor"
        repoVendorDesc.Name = colTaskVendorDescription
        repoVendorDesc.Width = 150
        repoVendorDesc.ReadOnly = True
        gvTask.MasterTemplate.Columns.Add(repoVendorDesc)

        Dim repoStartDate As GridViewDateTimeColumn = New GridViewDateTimeColumn()
        repoStartDate.Format = DateTimePickerFormat.Custom
        repoStartDate.CustomFormat = "dd-MM-yyyy"
        repoStartDate.HeaderText = "Start Date"
        repoStartDate.FormatString = "{0:d}"
        repoStartDate.Name = colTaskStartDate
        repoStartDate.WrapText = True
        repoStartDate.ReadOnly = False
        repoStartDate.Width = 80
        gvTask.MasterTemplate.Columns.Add(repoStartDate)

        Dim repoEndDate As GridViewDateTimeColumn = New GridViewDateTimeColumn()
        repoEndDate.Format = DateTimePickerFormat.Custom
        repoEndDate.CustomFormat = "dd-MM-yyyy"
        repoEndDate.HeaderText = "End Date"
        repoEndDate.FormatString = "{0:d}"
        repoEndDate.Name = colTaskEndDate
        repoEndDate.WrapText = True
        repoEndDate.ReadOnly = False
        repoEndDate.Width = 100
        gvTask.MasterTemplate.Columns.Add(repoEndDate)

        Dim repoStatus As GridViewComboBoxColumn = New GridViewComboBoxColumn()
        repoStatus.FormatString = ""
        repoStatus.HeaderText = "Status"
        repoStatus.Name = colTaskStatus
        repoStatus.Width = 50
        repoStatus.ReadOnly = False
        repoStatus.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        repoStatus.DataSource = GetStatus()
        repoStatus.ValueMember = "Code"
        repoStatus.DisplayMember = "Code"
        gvTask.MasterTemplate.Columns.Add(repoStatus)

        Dim repoStatusDate As GridViewDateTimeColumn = New GridViewDateTimeColumn()
        repoStatusDate.Format = DateTimePickerFormat.Custom
        repoStatusDate.CustomFormat = "dd-MM-yyyy"
        repoStatusDate.HeaderText = "Status Date"
        repoStatusDate.FormatString = "{0:d}"
        repoStatusDate.Name = colTaskStatusDate
        repoStatusDate.WrapText = True
        repoStatusDate.ReadOnly = False
        repoStatusDate.Width = 80
        gvTask.MasterTemplate.Columns.Add(repoStatusDate)

        gvTask.AllowAddNewRow = False
        gvTask.ShowGroupPanel = False
        gvTask.AllowColumnReorder = True
        gvTask.AllowRowReorder = False
        gvTask.EnableSorting = False
        gvTask.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gvTask.MasterTemplate.ShowRowHeaderColumn = False
        gvTask.TableElement.TableHeaderHeight = 20
    End Sub

    Sub LoadStatus()
        cboStatus.DataSource = clsProjectStatus.GetProjectStatus()
        cboStatus.ValueMember = "Code"
        cboStatus.DisplayMember = "Name"
    End Sub

    Sub LoadProjectType()
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))
        Dim dr As DataRow = dt.NewRow()
        dr("Code") = "Time And Material"
        dr("Name") = "Time And Material"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Fixed Price"
        dr("Name") = "Fixed Price"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Cost Plus"
        dr("Name") = "Cost Plus"
        dt.Rows.Add(dr)

        cboProjectType.DataSource = dt
        cboProjectType.ValueMember = "Code"
        cboProjectType.DisplayMember = "Name"
    End Sub

    Sub LoadAccountMethod()
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))
        Dim dr As DataRow = dt.NewRow()
        dr("Code") = "Complete Project"
        dr("Name") = "Complete Project"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Total Cost Percenatage Complete"
        dr("Name") = "Total Cost Percenatage Complete"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Labour Hour Percenatage Complete"
        dr("Name") = "Labour Hour Percenatage Complete"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Billings and Costs"
        dr("Name") = "Billings and Costs"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Category Percenatage Complete"
        dr("Name") = "Category Percenatage Complete"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Accural Basis"
        dr("Name") = "Accural Basis"
        dt.Rows.Add(dr)

        cboAccountMethod.DataSource = dt
        cboAccountMethod.ValueMember = "Code"
        cboAccountMethod.DisplayMember = "Name"
    End Sub

    Private Function GetBillingType() As DataTable
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow = dt.NewRow()
        dr("Code") = "Billable"

        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Non billable"

        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "No charge "

        dt.Rows.Add(dr)

        Return dt
    End Function

    Sub AddNew()
        BlankAllControls()
         
        isNewEntry = True
        btnSave.Text = "Save"
        btnSave.Enabled = True
        txtDocNo.MyReadOnly = False
        btnDelete.Enabled = True
        txtDocNo.Focus()
        gvJob.Rows.AddNew()
        gvTask.Rows.AddNew()
    End Sub

    Sub SetLength()
        'txtDocNo.MyMaxLength = 30
        'txtDesc.MaxLength = 200
        'txtRemarks.MaxLength = 200
        'txtComment.MaxLength = 200
        'cboModeOfTransport.MaxLength = 12
        'cboPOType.MaxLength = 1
        'cboItemType.MaxLength = 1
    End Sub


    Private Function AllowToSave() As Boolean
        gvJob_CurrentRowChanging(Nothing, Nothing)
        UpdateAllTotals()
        If clsCommon.myLen(txtDesc.Text) <= 0 Then
            txtDesc.Focus()
            Throw New Exception("Please enter description of the project")
        End If

        Return True
    End Function

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        SaveData()
    End Sub
    Sub SaveData()
        Try
            If (AllowToSave()) Then
                Dim obj As New clsProjectMaster()
                obj.PROJECT_CODE = txtDocNo.Value
                obj.SPECIFICATION = txtDesc.Text
                obj.PROJECT_STATUS = clsCommon.myCstr(cboStatus.SelectedValue)
                obj.Project_Manager = txtProjectManager.Value
                obj.Cust_Code = txtCustomer.Value
                obj.Sale_Order_No = txtSalesOrder.Value
                obj.Project_Type = clsCommon.myCstr(cboProjectType.SelectedValue)
                obj.Project_Type_Value = txtValue.Value
                obj.Account_Method = clsCommon.myCstr(cboAccountMethod.SelectedValue)
                If txtApproveDate.Checked Then
                    obj.Approve_Date = txtApproveDate.Value
                End If
                If txtReleasedDate.Checked Then
                    obj.Open_Date = txtReleasedDate.Value
                End If
                If txtCompleteDate.Checked Then
                    obj.Completed_Date = txtCompleteDate.Value
                End If
                obj.Comment = txtComment.Text
                obj.Total_Cost = clsCommon.myCdbl(lblCost.Text)
                obj.Total_Billing = clsCommon.myCdbl(lblBilling.Text)
                obj.Total_Profit = clsCommon.myCdbl(lblProfit.Text)
                obj.arrJob = New List(Of clsProjectJobMaster)
                For ii As Integer = 0 To gvJob.RowCount - 1
                    If clsCommon.myLen(gvJob.Rows(ii).Cells(colJobCode).Value) > 0 Then
                        Dim objtr As New clsProjectJobMaster()
                        objtr.SNo = clsCommon.myCdbl(gvJob.Rows(ii).Cells(colJobSNo).Value)
                        objtr.Job_Code = clsCommon.myCstr(gvJob.Rows(ii).Cells(colJobCode).Value)
                        objtr.Job_Type = clsCommon.myCstr(gvJob.Rows(ii).Cells(colJobType).Value)
                        objtr.Accounting_Method = clsCommon.myCstr(gvJob.Rows(ii).Cells(colJobAccountMethod).Value)
                        objtr.Billing_Type = clsCommon.myCstr(gvJob.Rows(ii).Cells(colJobBillingType).Value)
                        objtr.Close_To_Bill = clsCommon.myCBool(gvJob.Rows(ii).Cells(colJobCloseToBill).Value)
                        objtr.Close_To_Cost = clsCommon.myCBool(gvJob.Rows(ii).Cells(colJobCloseToCost).Value)


                        If clsCommon.myLen(gvJob.Rows(ii).Cells(colJobStartDate).Value) > 0 Then
                            objtr.Start_Date = clsCommon.myCDate(gvJob.Rows(ii).Cells(colJobStartDate).Value)
                        End If
                        If clsCommon.myLen(gvJob.Rows(ii).Cells(colJobEndDate).Value) > 0 Then
                            objtr.End_Date = clsCommon.myCDate(gvJob.Rows(ii).Cells(colJobEndDate).Value)
                        End If
                        objtr.Status = clsCommon.myCstr(gvJob.Rows(ii).Cells(colJobStatus).Value)
                        If clsCommon.myLen(gvJob.Rows(ii).Cells(colJobStatusDate).Value) > 0 Then
                            objtr.Status_Date = clsCommon.myCDate(gvJob.Rows(ii).Cells(colJobStatusDate).Value)
                        End If





                        objtr.arrTask = TryCast(gvJob.Rows(ii).Tag, List(Of clsProjectTaskMaster))
                        If (objtr.arrTask Is Nothing OrElse objtr.arrTask.Count <= 0) Then
                            Throw New Exception("Please Fill at list one task for Job" + objtr.Job_Code)
                        End If
                        obj.arrJob.Add(objtr)
                    End If
                Next
                If (obj.arrJob Is Nothing OrElse obj.arrJob.Count <= 0) Then
                    Throw New Exception("Please Fill at list one Job")
                End If

                ''For Custom Fields
                obj.Form_ID = MyBase.Form_ID
                'obj.arrCustomFields = New List(Of clsCustomFieldValues)
                'If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
                '    UcCustomFields1.GetData(obj.arrCustomFields)
                'End If
                'If MyBase.ArrDetailFields IsNot Nothing AndAlso MyBase.ArrDetailFields.Count > 0 Then
                '    clsCustomFieldGrid.GetData(obj.arrCustomFields, gvJob, MyBase.ArrDetailFields, colICode)
                'End If
                ''End of For Custom Fields
                Dim isSaved As Boolean = obj.SaveData(obj, isNewEntry)
                UcAttachment1.SaveData(obj.PROJECT_CODE)


                'Return isSaved
                clsCommon.MyMessageBoxShow("Data Saved Successfully", Me.Text)
                LoadData(obj.PROJECT_CODE, NavigatorType.Current)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub txtProjectManager__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtProjectManager._MYValidating
        Dim qry As String = "select EMP_CODE as Code,Emp_Name as Name from TSPL_EMPLOYEE_MASTER"
        txtProjectManager.Value = clsCommon.ShowSelectForm("PMProjMana", qry, "Code", "", txtProjectManager.Value, "Code", isButtonClicked)
        lblProjectManager.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Emp_Name from TSPL_EMPLOYEE_MASTER where EMP_CODE='" + txtProjectManager.Value + "'"))
    End Sub

    Private Sub txtCustomer__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtCustomer._MYValidating
        Dim qry As String = "select Cust_Code as Code,Customer_Name as Name from TSPL_CUSTOMER_MASTER"
        txtCustomer.Value = clsCommon.ShowSelectForm("PMCustMas", qry, "Code", "", txtCustomer.Value, "Code", isButtonClicked)
        lblCustomer.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Customer_Name from TSPL_CUSTOMER_MASTER where Cust_Code='" + txtCustomer.Value + "'"))
    End Sub

    Private Sub txtSalesOrder__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtSalesOrder._MYValidating
        Dim qry As String = "select Document_Code as Code,Document_Date as [Document Date],Customer_Code as [Customer Code], TSPL_CUSTOMER_MASTER.Customer_Name as Customer from TSPL_SD_SALES_ORDER_HEAD left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SALES_ORDER_HEAD.Customer_Code"
        Dim whrclas As String = "TSPL_SD_SALES_ORDER_HEAD.Status='1' "
        If clsCommon.myLen(txtCustomer.Value) > 0 Then
            whrclas += " and TSPL_SD_SALES_ORDER_HEAD.Customer_Code='" + txtCustomer.Value + "'"
        End If

        txtSalesOrder.Value = clsCommon.ShowSelectForm("PMCustOrder", qry, "Code", "", txtCustomer.Value, "Code", isButtonClicked)
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry + " where Document_Code='" + txtSalesOrder.Value + "'")
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            txtCustomer.Value = clsCommon.myCstr(dt.Rows(0)("Customer Code"))
            lblCustomer.Text = clsCommon.myCstr(dt.Rows(0)("Customer"))
        End If
    End Sub

    Sub BlankAllControls()
        txtDocNo.Value = ""
        txtDesc.Text = ""
        cboStatus.SelectedValue = "Estimated"
        txtProjectManager.Value = ""
        lblProjectManager.Text = ""
        txtCustomer.Value = ""
        lblCustomer.Text = ""
        txtSalesOrder.Value = ""
        cboProjectType.SelectedValue = "Time And Material"
        cboAccountMethod.SelectedValue = "Complete Project"
        txtComment.Text = ""
        txtPlanBy.Text = ""
        txtApproveBy.Text = ""

        txtApproveDate.Value = clsCommon.GETSERVERDATE()
        txtApproveDate.Checked = True
        txtApproveDateActual.Value = txtApproveDate.Value
        txtApproveDateActual.Checked = False

        lblReleasedBy.Text = ""
        txtReleasedDate.Value = txtApproveDate.Value
        txtReleasedDate.Checked = True
        txtReleasedDateActual.Value = txtApproveDate.Value
        txtReleasedDateActual.Checked = False

        lblCompleteBy.Text = ""
        txtCompleteDate.Value = txtApproveDate.Value
        txtCompleteDate.Checked = True
        txtCompleteDateActual.Value = txtApproveDate.Value
        txtCompleteDateActual.Checked = False
        txtComment.Text = ""
        LoadBlankGridJob()
        LoadBlankGridTask()

        lblCost.Text = ""
        lblCostActual.Text = ""

        lblBilling.Text = ""
        lblBillingActual.Text = ""

        lblProfit.Text = ""
        lblProfitActual.Text = ""


        lblCostActual.Text = ""
        lblBillingActual.Text = ""
        lblProfitActual.Text = ""
        ''For Custom Fields
        If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
            UcCustomFields1.BlankAllControls()
        End If
        ''End of For Custom Fields
        UcAttachment1.BlankAllControls()
        TreeView.DataSource = Nothing
    End Sub

    Private Sub btnAddNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddNew.Click
        AddNew()
    End Sub

    Private Sub gvJob_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvJob.CellValueChanged
        Try
            If (Not isInsideLoadData) Then
                If Not isCellValueChangedJob Then
                    isCellValueChangedJob = True
                    If e.Column Is gvJob.Columns(colJobCode) Then
                        Dim qry As String = "select JOB_CODE as CODE ,DESCRIPTION from TSPL_PJC_JOB"
                        Dim whrCls As String = ""
                        gvJob.CurrentRow.Cells(colJobCode).Value = clsCommon.ShowSelectForm("ProjeJobFind", qry, "Code", whrCls, clsCommon.myCstr(gvJob.CurrentRow.Cells(colJobCode).Value), "Code", False)
                        gvJob.CurrentRow.Cells(colJobDesc).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select DESCRIPTION from TSPL_PJC_JOB where JOB_CODE ='" + clsCommon.myCstr(gvJob.CurrentRow.Cells(colJobCode).Value) + "'"))
                        gvJob.CurrentRow.Cells(colJobType).Value = clsCommon.myCstr(cboProjectType.SelectedValue)
                        gvJob.CurrentRow.Cells(colJobAccountMethod).Value = clsCommon.myCstr(cboAccountMethod.SelectedValue)
                    End If
                    UpdateAllTotals()
                    isCellValueChangedJob = False
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub gvJob_CurrentColumnChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gvJob.CurrentColumnChanged
        If gvJob.RowCount > 0 Then
            Dim intCurrRow As Integer = gvJob.CurrentRow.Index
            gvJob.CurrentRow.Cells(colJobSNo).Value = clsCommon.myCdbl(intCurrRow + 1)
            If intCurrRow = gvJob.Rows.Count - 1 Then
                gvJob.Rows.AddNew()
                gvJob.CurrentRow = gvJob.Rows(intCurrRow)
            End If
        End If
    End Sub

    Private Sub gvTask_CellFormatting(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.CellFormattingEventArgs) Handles gvTask.CellFormatting
        Try
            If e.Column Is gvTask.Columns(colTaskVendorCode) Then
                e.Row.Cells(colTaskVendorCode).ReadOnly = Not clsCommon.myCBool(e.Row.Cells(colTaskType).Value)
            ElseIf e.Column Is gvTask.Columns(colTaskCostPlus) Then
                e.Row.Cells(colTaskCostPlus).ReadOnly = Not (clsCommon.CompairString(clsCommon.myCstr(cboProjectType.SelectedValue), "Cost Plus") = CompairStringResult.Equal)
            ElseIf e.Column Is gvTask.Columns(colTaskBillingAmt) Then
                e.Row.Cells(colTaskBillingAmt).ReadOnly = (clsCommon.CompairString(clsCommon.myCstr(cboProjectType.SelectedValue), "Cost Plus") = CompairStringResult.Equal)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub gvTask_CellValueChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvTask.CellValueChanged
        Try
            If (Not isInsideLoadData) Then
                If Not isCellValueChangedTask Then
                    isCellValueChangedTask = True
                    If e.Column Is gvTask.Columns(colTaskCode) OrElse e.Column Is gvTask.Columns(colTaskVendorCode) OrElse e.Column Is gvTask.Columns(colTaskQty) OrElse e.Column Is gvTask.Columns(colTaskCost) OrElse e.Column Is gvTask.Columns(colTaskCostPlus) OrElse e.Column Is gvTask.Columns(colTaskBillingAmt) Then
                        If e.Column Is gvTask.Columns(colTaskCode) Then
                            Dim qry As String = "select TASK_CODE as Code,DESCRIPTION  from TSPL_PJC_TASK"
                            Dim whrCls As String = ""
                            gvTask.CurrentRow.Cells(colTaskCode).Value = clsCommon.ShowSelectForm("ProjeTaskFind", qry, "Code", whrCls, clsCommon.myCstr(gvTask.CurrentRow.Cells(colTaskCode).Value), "Code", False)
                            gvTask.CurrentRow.Cells(colTaskDesc).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select DESCRIPTION  from TSPL_PJC_TASK where TASK_CODE='" + clsCommon.myCstr(gvTask.CurrentRow.Cells(colTaskCode).Value) + "'"))
                            gvTask.CurrentRow.Cells(colTaskBillingType).Value = clsCommon.myCstr(gvJob.CurrentRow.Cells(colJobBillingType).Value)
                            gvTask.CurrentRow.Cells(colTaskQty).Value = 1
                            If clsCommon.CompairString(clsCommon.myCstr(cboProjectType.SelectedValue), "Cost Plus") = CompairStringResult.Equal Then
                                gvTask.CurrentRow.Cells(colTaskCostPlus).Value = txtValue.Value
                            Else
                                gvTask.CurrentRow.Cells(colTaskBillingAmt).Value = txtValue.Value
                            End If

                        ElseIf e.Column Is gvTask.Columns(colTaskVendorCode) Then
                            Dim qry As String = "select Vendor_Code as Code,Vendor_Name as Name from TSPL_VENDOR_MASTER"
                            Dim whrCls As String = ""
                            gvTask.CurrentRow.Cells(colTaskVendorCode).Value = clsCommon.ShowSelectForm("ProjeVendorFind", qry, "Code", whrCls, clsCommon.myCstr(gvTask.CurrentRow.Cells(colTaskVendorCode).Value), "Code", False)
                            gvTask.CurrentRow.Cells(colTaskVendorDescription).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  Vendor_Name  from TSPL_VENDOR_MASTER where Vendor_Code='" + clsCommon.myCstr(gvTask.CurrentRow.Cells(colTaskVendorCode).Value) + "'"))

                        End If
                        UpdateAllTotals()
                    End If
                    isCellValueChangedTask = False
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub gvTask_CurrentColumnChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gvTask.CurrentColumnChanged
        If gvTask.RowCount > 0 Then
            Dim intCurrRow As Integer = gvTask.CurrentRow.Index
            gvTask.CurrentRow.Cells(colTaskSNo).Value = clsCommon.myCdbl(intCurrRow + 1)
            If intCurrRow = gvTask.Rows.Count - 1 Then
                gvTask.Rows.AddNew()
                gvTask.CurrentRow = gvTask.Rows(intCurrRow)
            End If
        End If
    End Sub

    Sub UpdateAllTotals()
        Try
            Dim dblCost As Double = 0
            Dim dblBillableAmt As Double = 0
            For ii As Integer = 0 To gvJob.Rows.Count - 1
                If ii = gvJob.CurrentRow.Index Then
                    For jj As Integer = 0 To gvTask.Rows.Count - 1
                        If clsCommon.myLen(gvTask.Rows(jj).Cells(colTaskCode).Value) > 0 Then
                            gvTask.Rows(jj).Cells(colTaskAmt).Value = clsCommon.myCdbl(gvTask.Rows(jj).Cells(colTaskQty).Value) * clsCommon.myCdbl(gvTask.Rows(jj).Cells(colTaskCost).Value)
                            If clsCommon.CompairString(clsCommon.myCstr(cboProjectType.SelectedValue), "Cost Plus") = CompairStringResult.Equal Then
                                gvTask.Rows(jj).Cells(colTaskBillingAmt).Value = gvTask.Rows(jj).Cells(colTaskAmt).Value * (100 + gvTask.Rows(jj).Cells(colTaskCostPlus).Value) / 100
                            Else
                                gvTask.Rows(jj).Cells(colTaskCostPlus).Value = 0
                            End If
                            dblCost += clsCommon.myCdbl(gvTask.Rows(jj).Cells(colTaskAmt).Value)
                            dblBillableAmt += clsCommon.myCdbl(gvTask.Rows(jj).Cells(colTaskBillingAmt).Value)
                        End If
                    Next
                Else
                    Dim Arr As List(Of clsProjectTaskMaster) = TryCast(gvJob.Rows(ii).Tag, List(Of clsProjectTaskMaster))
                    If Arr IsNot Nothing AndAlso Arr.Count > 0 Then
                        For jj As Integer = 0 To Arr.Count - 1
                            Arr(jj).Amount = Arr(jj).Qty * Arr(jj).Cost
                            If clsCommon.CompairString(clsCommon.myCstr(cboProjectType.SelectedValue), "Cost Plus") = CompairStringResult.Equal Then
                                Arr(jj).Billing_Amt = Arr(jj).Amount * (100 + Arr(jj).Cost_Plus) / 100
                            Else
                                Arr(jj).Cost_Plus = 0
                            End If
                            dblCost += Arr(jj).Amount
                            dblBillableAmt += Arr(jj).Billing_Amt
                        Next
                    End If
                End If
            Next
            lblCost.Text = clsCommon.myFormat(dblCost)
            lblBilling.Text = clsCommon.myFormat(dblBillableAmt)
            lblProfit.Text = clsCommon.myFormat(dblBillableAmt - dblCost)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub gvJob_CurrentRowChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.CurrentRowChangedEventArgs) Handles gvJob.CurrentRowChanged
        Try
            If Not isInsideLoadData Then
                LoadBlankGridTask()
                Dim Arr As List(Of clsProjectTaskMaster) = TryCast(e.CurrentRow.Tag, List(Of clsProjectTaskMaster))
                Dim counter As Integer = 1
                If Arr IsNot Nothing AndAlso Arr.Count > 0 Then
                    For Each obj As clsProjectTaskMaster In Arr
                        gvTask.Rows.AddNew()
                        gvTask.Rows(gvTask.RowCount - 1).Cells(colTaskSNo).Value = counter
                        gvTask.Rows(gvTask.RowCount - 1).Cells(colTaskCode).Value = obj.Task_Code
                        gvTask.Rows(gvTask.RowCount - 1).Cells(colTaskDesc).Value = obj.Task_Description
                        gvTask.Rows(gvTask.RowCount - 1).Cells(colTaskBillingType).Value = obj.Billing_Type
                        gvTask.Rows(gvTask.RowCount - 1).Cells(colTaskQty).Value = obj.Qty
                        gvTask.Rows(gvTask.RowCount - 1).Cells(colTaskCost).Value = obj.Cost
                        gvTask.Rows(gvTask.RowCount - 1).Cells(colTaskAmt).Value = obj.Amount
                        gvTask.Rows(gvTask.RowCount - 1).Cells(colTaskCostPlus).Value = obj.Cost_Plus
                        gvTask.Rows(gvTask.RowCount - 1).Cells(colTaskBillingAmt).Value = obj.Billing_Amt
                        gvTask.Rows(gvTask.RowCount - 1).Cells(colTaskType).Value = obj.Is_Task_Type_External
                        gvTask.Rows(gvTask.RowCount - 1).Cells(colTaskVendorCode).Value = obj.Vendor_Code
                        gvTask.Rows(gvTask.RowCount - 1).Cells(colTaskVendorDescription).Value = obj.Vendor_Desc


                        If obj.Start_Date IsNot Nothing Then
                            gvTask.Rows(gvTask.RowCount - 1).Cells(colTaskStartDate).Value = obj.Start_Date
                        End If
                        If obj.End_Date IsNot Nothing Then
                            gvTask.Rows(gvTask.RowCount - 1).Cells(colTaskEndDate).Value = obj.End_Date
                        End If
                        gvTask.Rows(gvTask.RowCount - 1).Cells(colTaskStatus).Value = obj.Status
                        If obj.Status_Date IsNot Nothing Then
                            gvTask.Rows(gvTask.RowCount - 1).Cells(colTaskStatusDate).Value = obj.Status_Date
                        End If
                        counter += 1
                    Next
                End If
                gvTask.Rows.AddNew()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)

        End Try
    End Sub

    Private Sub gvJob_CurrentRowChanging(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.CurrentRowChangingEventArgs) Handles gvJob.CurrentRowChanging
        Try

            If gvJob.CurrentRow Is Nothing OrElse gvJob.CurrentRow.Index < 0 Then
                Exit Sub
            End If

            If Not isInsideLoadData Then
                Dim arr As New List(Of clsProjectTaskMaster)
                'If arr IsNot Nothing AndAlso arr.Count > 0 Then
                For ii As Integer = 0 To gvTask.Rows.Count - 1
                    If clsCommon.myLen(gvTask.Rows(ii).Cells(colTaskCode).Value) > 0 Then
                        Dim obj As New clsProjectTaskMaster()
                        obj.SNo = clsCommon.myCdbl(gvTask.Rows(ii).Cells(colTaskSNo).Value)
                        obj.Task_Code = clsCommon.myCstr(gvTask.Rows(ii).Cells(colTaskCode).Value)
                        obj.Task_Description = clsCommon.myCstr(gvTask.Rows(ii).Cells(colTaskDesc).Value)
                        obj.Billing_Type = clsCommon.myCstr(gvTask.Rows(ii).Cells(colTaskBillingType).Value)
                        obj.Qty = clsCommon.myCdbl(gvTask.Rows(ii).Cells(colTaskQty).Value)
                        obj.Cost = clsCommon.myCdbl(gvTask.Rows(ii).Cells(colTaskCost).Value)
                        obj.Amount = clsCommon.myCdbl(gvTask.Rows(ii).Cells(colTaskAmt).Value)
                        obj.Cost_Plus = clsCommon.myCdbl(gvTask.Rows(ii).Cells(colTaskCostPlus).Value)
                        obj.Billing_Amt = clsCommon.myCdbl(gvTask.Rows(ii).Cells(colTaskBillingAmt).Value)
                        obj.Is_Task_Type_External = clsCommon.myCBool(gvTask.Rows(ii).Cells(colTaskType).Value)
                        If obj.Is_Task_Type_External Then
                            obj.Vendor_Code = clsCommon.myCstr(gvTask.Rows(ii).Cells(colTaskVendorCode).Value)
                            obj.Vendor_Desc = clsCommon.myCstr(gvTask.Rows(ii).Cells(colTaskVendorDescription).Value)
                        End If

                        If clsCommon.myLen(gvTask.Rows(ii).Cells(colTaskStartDate).Value) > 0 Then
                            obj.Start_Date = clsCommon.myCDate(gvTask.Rows(ii).Cells(colTaskStartDate).Value)
                        End If
                        If clsCommon.myLen(gvTask.Rows(ii).Cells(colTaskEndDate).Value) > 0 Then
                            obj.End_Date = clsCommon.myCDate(gvTask.Rows(ii).Cells(colTaskEndDate).Value)
                        End If
                        obj.Status = clsCommon.myCstr(gvTask.Rows(ii).Cells(colTaskStatus).Value)
                        If clsCommon.myLen(gvTask.Rows(ii).Cells(colTaskStatusDate).Value) > 0 Then
                            obj.Status_Date = clsCommon.myCDate(gvTask.Rows(ii).Cells(colTaskStatusDate).Value)
                        End If

                        arr.Add(obj)
                    End If
                Next
               

                gvJob.CurrentRow.Tag = arr
                'End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Try
            isInsideLoadData = True
            isNewEntry = False
            btnSave.Text = "Update"
            BlankAllControls()
            Dim obj As New clsProjectMaster()
            obj = clsProjectMaster.GetData(strCode, NavTyep)

            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.PROJECT_CODE) > 0) Then
                txtDocNo.Value = obj.PROJECT_CODE
                txtDesc.Text = obj.SPECIFICATION
                cboStatus.SelectedValue = obj.PROJECT_STATUS
                txtProjectManager.Value = obj.Project_Manager
                lblProjectManager.Text = obj.Project_Manager_Name
                txtCustomer.Value = obj.Cust_Code
                lblCustomer.Text = obj.Customer_Name
                txtSalesOrder.Value = obj.Sale_Order_No
                cboProjectType.SelectedValue = obj.Project_Type
                txtValue.Value = obj.Project_Type_Value
                cboAccountMethod.SelectedValue = obj.Account_Method

                txtPlanBy.Text = obj.Create_Name
                txtPlanDate.Value = obj.Created_Date

                txtApproveBy.Text = obj.Approve_Name
                txtApproveDate.Checked = obj.Approve_Date.HasValue
                If obj.Approve_Date.HasValue Then
                    txtApproveDate.Value = obj.Approve_Date
                End If
                txtApproveDateActual.Checked = obj.Approve_Date_Actual.HasValue
                If obj.Approve_Date_Actual.HasValue Then
                    txtApproveDateActual.Value = obj.Approve_Date_Actual
                End If

                lblReleasedBy.Text = obj.Open_Name
                txtReleasedDate.Checked = obj.Open_Date.HasValue
                If obj.Open_Date.HasValue Then
                    txtReleasedDate.Value = obj.Open_Date
                End If
                txtReleasedDateActual.Checked = obj.Open_Date_Actual.HasValue
                If obj.Open_Date_Actual.HasValue Then
                    txtReleasedDateActual.Value = obj.Open_Date_Actual
                End If

                lblCompleteBy.Text = obj.Completed_Name
                txtCompleteDate.Checked = obj.Completed_Date.HasValue
                If obj.Completed_Date.HasValue Then
                    txtCompleteDate.Value = obj.Completed_Date
                End If
                txtCompleteDateActual.Checked = obj.Completed_Date_Actual.HasValue
                If obj.Completed_Date_Actual.HasValue Then
                    txtCompleteDateActual.Value = obj.Completed_Date_Actual
                End If
                txtComment.Text = obj.Comment
                lblCost.Text = clsCommon.myFormat(obj.Total_Cost)
                lblBilling.Text = clsCommon.myFormat(obj.Total_Billing)
                lblProfit.Text = clsCommon.myFormat(obj.Total_Profit)

                lblCostActual.Text = clsCommon.myFormat(obj.Actual_Cost)
                lblBillingActual.Text = clsCommon.myFormat(obj.Actual_Billing)
                lblProfitActual.Text = clsCommon.myFormat(obj.Actual_Profit)

                If obj.arrJob IsNot Nothing AndAlso obj.arrJob.Count > 0 Then
                    For Each objTr As clsProjectJobMaster In obj.arrJob
                        gvJob.Rows.AddNew()
                        gvJob.Rows(gvJob.Rows.Count - 1).Cells(colJobSNo).Value = objTr.SNo
                        gvJob.Rows(gvJob.Rows.Count - 1).Cells(colJobCode).Value = objTr.Job_Code
                        gvJob.Rows(gvJob.Rows.Count - 1).Cells(colJobDesc).Value = objTr.Job_Desc
                        gvJob.Rows(gvJob.Rows.Count - 1).Cells(colJobType).Value = objTr.Job_Type
                        gvJob.Rows(gvJob.Rows.Count - 1).Cells(colJobAccountMethod).Value = objTr.Accounting_Method
                        gvJob.Rows(gvJob.Rows.Count - 1).Cells(colJobBillingType).Value = objTr.Billing_Type
                        gvJob.Rows(gvJob.Rows.Count - 1).Cells(colJobCloseToBill).Value = objTr.Close_To_Bill
                        gvJob.Rows(gvJob.Rows.Count - 1).Cells(colJobCloseToCost).Value = objTr.Close_To_Cost


                        If objTr.Start_Date IsNot Nothing Then
                            gvJob.Rows(gvJob.RowCount - 1).Cells(colJobStartDate).Value = objTr.Start_Date
                        End If
                        If objTr.End_Date IsNot Nothing Then
                            gvJob.Rows(gvJob.RowCount - 1).Cells(colJobEndDate).Value = objTr.End_Date
                        End If
                        gvJob.Rows(gvJob.RowCount - 1).Cells(colJobStatus).Value = objTr.Status
                        If objTr.Status_Date IsNot Nothing Then
                            gvJob.Rows(gvJob.RowCount - 1).Cells(colJobStatusDate).Value = objTr.Status_Date
                        End If

                        gvJob.Rows(gvJob.Rows.Count - 1).Tag = objTr.arrTask
                    Next
                    gvJob.Rows.AddNew()
                End If
                ''For Custom Fields
                If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
                    UcCustomFields1.LoadData(obj.PROJECT_CODE)
                End If
                'clsCustomFieldGrid.FillDataInGrid(obj.PROJECT_CODE, MyBase.Form_ID, gvJob)
                ''End of For Custom Fields
                UcAttachment1.LoadData(obj.PROJECT_CODE)
                LoadTreeView()
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isInsideLoadData = False
        End Try
    End Sub

    Sub LoadTreeView()
        Dim qry As String = "select * from (" & _
        " select TSPL_PJC_PROJECT.PROJECT_CODE as Code,TSPL_PJC_PROJECT.SPECIFICATION as Name,null as ParentCode,0 as SNo" & _
        " from TSPL_PJC_PROJECT " & _
        " where TSPL_PJC_PROJECT.PROJECT_CODE='" + txtDocNo.Value + "'" & _
        " union all" & _
        " select TSPL_PJC_PROJECT_JOB.JOB_CODE as Code,TSPL_PJC_JOB.DESCRIPTION as Name,TSPL_PJC_PROJECT_JOB.PROJECT_CODE as ParentCode ,TSPL_PJC_PROJECT_JOB.S_No as SNo" & _
        " from TSPL_PJC_PROJECT_JOB " & _
        " left outer join TSPL_PJC_JOB on TSPL_PJC_JOB.JOB_CODE=TSPL_PJC_PROJECT_JOB.JOB_CODE" & _
        " left outer join TSPL_PJC_PROJECT on TSPL_PJC_PROJECT.PROJECT_CODE=TSPL_PJC_PROJECT_JOB.PROJECT_CODE" & _
        " where TSPL_PJC_PROJECT.PROJECT_CODE='" + txtDocNo.Value + "'" & _
        " union all " & _
       " select TSPL_PJC_PROJECT_TASK.Task_Code as Code,TSPL_PJC_TASK.DESCRIPTION as Name,TSPL_PJC_PROJECT_JOB.JOB_CODE as ParentCode,TSPL_PJC_PROJECT_TASK.SNo " & _
        " from TSPL_PJC_PROJECT_TASK " & _
        " left outer join TSPL_PJC_TASK on TSPL_PJC_TASK.TASK_CODE=TSPL_PJC_PROJECT_TASK.Task_Code  " & _
        " left outer join TSPL_PJC_PROJECT_JOB on TSPL_PJC_PROJECT_JOB.Job_ID=TSPL_PJC_PROJECT_TASK.Job_ID" & _
        " left outer join TSPL_PJC_PROJECT on TSPL_PJC_PROJECT.PROJECT_CODE=TSPL_PJC_PROJECT_JOB.PROJECT_CODE" & _
        " where TSPL_PJC_PROJECT.PROJECT_CODE='" + txtDocNo.Value + "'" & _
        " )xxx order by SNo"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

        TreeView.DataSource = Nothing
        TreeView.TreeViewElement.AutoSizeItems = True
        TreeView.ShowLines = True
        TreeView.ShowRootLines = True
        TreeView.TreeViewElement.ViewElement.Margin = New Padding(4)
        TreeView.ShowExpandCollapse = True
        TreeView.TreeIndent = 15
        TreeView.FullRowSelect = False
        TreeView.ShowLines = True
        TreeView.LineStyle = TreeLineStyle.Dot
        TreeView.LineColor = Color.FromArgb(110, 153, 210)
        TreeView.ExpandAnimation = ExpandAnimation.Opacity
        TreeView.AllowEdit = False
        TreeView.ShowRootLines = False
        TreeView.TreeViewElement.AllowAlternatingRowColor = True
        TreeView.TreeViewElement.AlternatingRowColor = Color.AliceBlue
        'TreeView.TreeViewElement.AngleTransform = 270
        'TreeView.TreeViewElement.RightToLeft = True
        TreeView.TreeViewElement.DrawBorder = True
        TreeView.ValueMember = "Code"
        TreeView.DisplayMember = "Name"
        TreeView.ChildMember = "Code"
        TreeView.ParentMember = "ParentCode"
        TreeView.DataSource = dt
        'TreeView.ExpandAll()
    End Sub

    Private Sub txtDocNo__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtDocNo._MYNavigator
        LoadData(txtDocNo.Value, NavType)
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub txtDocNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtDocNo._MYValidating
        Try
            Dim qst As String = "select count(*) from TSPL_PJC_PROJECT where PROJECT_CODE='" + txtDocNo.Value + "'"
            Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qst))
            If count = 0 Then
                txtDocNo.MyReadOnly = False
            Else
                txtDocNo.MyReadOnly = True
            End If
            If txtDocNo.MyReadOnly OrElse isButtonClicked Then
                Dim qry As String = "select PROJECT_CODE as Code,SPECIFICATION as Description,PROJECT_STATUS as Status from TSPL_PJC_PROJECT"
                Dim whrClas As String = ""
                txtDocNo.Value = clsCommon.ShowSelectForm("PjcProMas", qry, "Code", whrClas, txtDocNo.Value, "Code", isButtonClicked)
                LoadData(txtDocNo.Value, NavigatorType.Current)
                'LoadData(clsCommon.ShowSelectForm("PjcProMas", qry, "Code", whrClas, txtDocNo.Value, "Code", isButtonClicked), NavigatorType.Current)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
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
                If (clsProjectMaster.DeleteData(txtDocNo.Value)) Then
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

    Private Sub txtDocNo_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDocNo.Load

    End Sub
End Class
