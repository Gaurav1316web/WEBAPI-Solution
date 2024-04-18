Imports XpertERPEngine

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmEmployeeRegister
    Inherits FrmMainTranScreen

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim TableViewDefinition1 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Me.gv1 = New common.UserControls.MyRadGridView()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.lblDepartment = New common.Controls.MyLabel()
        Me.lblDesignation = New common.Controls.MyLabel()
        Me.txtDepartment = New common.UserControls.txtFinder()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.TxtDesignation = New common.UserControls.txtFinder()
        Me.lbldes = New common.Controls.MyLabel()
        Me.lblLocationName = New common.Controls.MyLabel()
        Me.fndLocation = New common.UserControls.txtFinder()
        Me.txtEmp = New common.UserControls.txtMultiSelectFinder()
        Me.CboStatus = New common.Controls.MyComboBox()
        Me.lblLocationCode = New common.Controls.MyLabel()
        Me.lblEmp = New common.Controls.MyLabel()
        Me.lblStatus = New common.Controls.MyLabel()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem3 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItemSave = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItemDelete = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnReset = New Telerik.WinControls.UI.RadButton()
        Me.btnRefresh = New Telerik.WinControls.UI.RadButton()
        Me.RadSplitButton1 = New Telerik.WinControls.UI.RadSplitButton()
        Me.rmPFWithdrawnForm = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmPFEligibiltyRegister = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnExport = New Telerik.WinControls.UI.RadSplitButton()
        Me.btnExcel = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnPDF = New Telerik.WinControls.UI.RadMenuItem()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.lblDepartment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDesignation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbldes, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocationName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CboStatus, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocationCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblEmp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblStatus, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnRefresh, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadSplitButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnExport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'gv1
        '
        Me.gv1.AutoScroll = True
        Me.gv1.BackColor = System.Drawing.Color.FromArgb(CType(CType(191, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.gv1.Cursor = System.Windows.Forms.Cursors.Default
        Me.gv1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv1.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.gv1.ForeColor = System.Drawing.Color.Black
        Me.gv1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gv1.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gv1.MasterTemplate.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        Me.gv1.MasterTemplate.AllowAddNewRow = False
        Me.gv1.MasterTemplate.AllowDeleteRow = False
        Me.gv1.MasterTemplate.AllowEditRow = False
        Me.gv1.MasterTemplate.EnableFiltering = True
        Me.gv1.MasterTemplate.EnableGrouping = False
        Me.gv1.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gv1.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv1.MasterTemplate.ViewDefinition = TableViewDefinition1
        Me.gv1.MyStopExport = False
        Me.gv1.Name = "gv1"
        Me.gv1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv1.ShowHeaderCellButtons = True
        Me.gv1.Size = New System.Drawing.Size(1021, 414)
        Me.gv1.TabIndex = 146
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.ImageScalingSize = New System.Drawing.Size(68, 14)
        Me.btnClose.Location = New System.Drawing.Point(914, 3)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(113, 21)
        Me.btnClose.TabIndex = 2
        Me.btnClose.Text = "Close"
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadPageView1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadMenu1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnReset)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnRefresh)
        Me.SplitContainer1.Panel2.Controls.Add(Me.RadSplitButton1)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnExport)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Size = New System.Drawing.Size(1042, 516)
        Me.SplitContainer1.SplitterDistance = 482
        Me.SplitContainer1.TabIndex = 0
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 20)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage2
        Me.RadPageView1.Size = New System.Drawing.Size(1042, 462)
        Me.RadPageView1.TabIndex = 215
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.lblDepartment)
        Me.RadPageViewPage1.Controls.Add(Me.lblDesignation)
        Me.RadPageViewPage1.Controls.Add(Me.txtDepartment)
        Me.RadPageViewPage1.Controls.Add(Me.MyLabel2)
        Me.RadPageViewPage1.Controls.Add(Me.TxtDesignation)
        Me.RadPageViewPage1.Controls.Add(Me.lbldes)
        Me.RadPageViewPage1.Controls.Add(Me.lblLocationName)
        Me.RadPageViewPage1.Controls.Add(Me.fndLocation)
        Me.RadPageViewPage1.Controls.Add(Me.txtEmp)
        Me.RadPageViewPage1.Controls.Add(Me.CboStatus)
        Me.RadPageViewPage1.Controls.Add(Me.lblLocationCode)
        Me.RadPageViewPage1.Controls.Add(Me.lblEmp)
        Me.RadPageViewPage1.Controls.Add(Me.lblStatus)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(41.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(1021, 414)
        Me.RadPageViewPage1.Text = "Filter"
        '
        'lblDepartment
        '
        Me.lblDepartment.AutoSize = False
        Me.lblDepartment.BorderVisible = True
        Me.lblDepartment.FieldName = Nothing
        Me.lblDepartment.Location = New System.Drawing.Point(321, 74)
        Me.lblDepartment.Name = "lblDepartment"
        Me.lblDepartment.Size = New System.Drawing.Size(239, 19)
        Me.lblDepartment.TabIndex = 401
        '
        'lblDesignation
        '
        Me.lblDesignation.AutoSize = False
        Me.lblDesignation.BorderVisible = True
        Me.lblDesignation.FieldName = Nothing
        Me.lblDesignation.Location = New System.Drawing.Point(321, 47)
        Me.lblDesignation.Name = "lblDesignation"
        Me.lblDesignation.Size = New System.Drawing.Size(239, 19)
        Me.lblDesignation.TabIndex = 400
        '
        'txtDepartment
        '
        Me.txtDepartment.CalculationExpression = Nothing
        Me.txtDepartment.FieldCode = Nothing
        Me.txtDepartment.FieldDesc = Nothing
        Me.txtDepartment.FieldMaxLength = 0
        Me.txtDepartment.FieldName = Nothing
        Me.txtDepartment.isCalculatedField = False
        Me.txtDepartment.IsSourceFromTable = False
        Me.txtDepartment.IsSourceFromValueList = False
        Me.txtDepartment.IsUnique = False
        Me.txtDepartment.Location = New System.Drawing.Point(100, 74)
        Me.txtDepartment.MendatroryField = True
        Me.txtDepartment.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDepartment.MyLinkLable1 = Me.MyLabel2
        Me.txtDepartment.MyLinkLable2 = Nothing
        Me.txtDepartment.MyReadOnly = False
        Me.txtDepartment.MyShowMasterFormButton = False
        Me.txtDepartment.Name = "txtDepartment"
        Me.txtDepartment.ReferenceFieldDesc = Nothing
        Me.txtDepartment.ReferenceFieldName = Nothing
        Me.txtDepartment.ReferenceTableName = Nothing
        Me.txtDepartment.Size = New System.Drawing.Size(215, 19)
        Me.txtDepartment.TabIndex = 399
        Me.txtDepartment.Value = ""
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(13, 74)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(65, 16)
        Me.MyLabel2.TabIndex = 398
        Me.MyLabel2.Text = "Department"
        '
        'TxtDesignation
        '
        Me.TxtDesignation.CalculationExpression = Nothing
        Me.TxtDesignation.FieldCode = Nothing
        Me.TxtDesignation.FieldDesc = Nothing
        Me.TxtDesignation.FieldMaxLength = 0
        Me.TxtDesignation.FieldName = Nothing
        Me.TxtDesignation.isCalculatedField = False
        Me.TxtDesignation.IsSourceFromTable = False
        Me.TxtDesignation.IsSourceFromValueList = False
        Me.TxtDesignation.IsUnique = False
        Me.TxtDesignation.Location = New System.Drawing.Point(100, 47)
        Me.TxtDesignation.MendatroryField = False
        Me.TxtDesignation.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtDesignation.MyLinkLable1 = Me.lbldes
        Me.TxtDesignation.MyLinkLable2 = Nothing
        Me.TxtDesignation.MyReadOnly = False
        Me.TxtDesignation.MyShowMasterFormButton = False
        Me.TxtDesignation.Name = "TxtDesignation"
        Me.TxtDesignation.ReferenceFieldDesc = Nothing
        Me.TxtDesignation.ReferenceFieldName = Nothing
        Me.TxtDesignation.ReferenceTableName = Nothing
        Me.TxtDesignation.Size = New System.Drawing.Size(215, 19)
        Me.TxtDesignation.TabIndex = 397
        Me.TxtDesignation.Value = ""
        '
        'lbldes
        '
        Me.lbldes.FieldName = Nothing
        Me.lbldes.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbldes.Location = New System.Drawing.Point(13, 47)
        Me.lbldes.Name = "lbldes"
        Me.lbldes.Size = New System.Drawing.Size(66, 16)
        Me.lbldes.TabIndex = 396
        Me.lbldes.Text = "Designation"
        '
        'lblLocationName
        '
        Me.lblLocationName.AutoSize = False
        Me.lblLocationName.BorderVisible = True
        Me.lblLocationName.FieldName = Nothing
        Me.lblLocationName.Location = New System.Drawing.Point(321, 19)
        Me.lblLocationName.Name = "lblLocationName"
        Me.lblLocationName.Size = New System.Drawing.Size(239, 19)
        Me.lblLocationName.TabIndex = 395
        '
        'fndLocation
        '
        Me.fndLocation.CalculationExpression = Nothing
        Me.fndLocation.FieldCode = Nothing
        Me.fndLocation.FieldDesc = Nothing
        Me.fndLocation.FieldMaxLength = 0
        Me.fndLocation.FieldName = Nothing
        Me.fndLocation.isCalculatedField = False
        Me.fndLocation.IsSourceFromTable = False
        Me.fndLocation.IsSourceFromValueList = False
        Me.fndLocation.IsUnique = False
        Me.fndLocation.Location = New System.Drawing.Point(100, 19)
        Me.fndLocation.MendatroryField = True
        Me.fndLocation.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndLocation.MyLinkLable1 = Nothing
        Me.fndLocation.MyLinkLable2 = Nothing
        Me.fndLocation.MyReadOnly = False
        Me.fndLocation.MyShowMasterFormButton = False
        Me.fndLocation.Name = "fndLocation"
        Me.fndLocation.ReferenceFieldDesc = Nothing
        Me.fndLocation.ReferenceFieldName = Nothing
        Me.fndLocation.ReferenceTableName = Nothing
        Me.fndLocation.Size = New System.Drawing.Size(215, 19)
        Me.fndLocation.TabIndex = 394
        Me.fndLocation.Value = ""
        '
        'txtEmp
        '
        Me.txtEmp.arrDispalyMember = Nothing
        Me.txtEmp.arrValueMember = Nothing
        Me.txtEmp.Location = New System.Drawing.Point(100, 132)
        Me.txtEmp.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEmp.MyLinkLable1 = Nothing
        Me.txtEmp.MyLinkLable2 = Nothing
        Me.txtEmp.MyNullText = "Please select..."
        Me.txtEmp.Name = "txtEmp"
        Me.txtEmp.Size = New System.Drawing.Size(460, 19)
        Me.txtEmp.TabIndex = 393
        '
        'CboStatus
        '
        Me.CboStatus.CalculationExpression = Nothing
        Me.CboStatus.DropDownAnimationEnabled = True
        Me.CboStatus.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.CboStatus.FieldCode = Nothing
        Me.CboStatus.FieldDesc = Nothing
        Me.CboStatus.FieldMaxLength = 0
        Me.CboStatus.FieldName = Nothing
        Me.CboStatus.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CboStatus.isCalculatedField = False
        Me.CboStatus.IsSourceFromTable = False
        Me.CboStatus.IsSourceFromValueList = False
        Me.CboStatus.IsUnique = False
        Me.CboStatus.Location = New System.Drawing.Point(100, 105)
        Me.CboStatus.MendatroryField = False
        Me.CboStatus.MyLinkLable1 = Nothing
        Me.CboStatus.MyLinkLable2 = Nothing
        Me.CboStatus.Name = "CboStatus"
        Me.CboStatus.ReferenceFieldDesc = Nothing
        Me.CboStatus.ReferenceFieldName = Nothing
        Me.CboStatus.ReferenceTableName = Nothing
        Me.CboStatus.Size = New System.Drawing.Size(215, 18)
        Me.CboStatus.TabIndex = 392
        '
        'lblLocationCode
        '
        Me.lblLocationCode.FieldName = Nothing
        Me.lblLocationCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLocationCode.Location = New System.Drawing.Point(13, 19)
        Me.lblLocationCode.Name = "lblLocationCode"
        Me.lblLocationCode.Size = New System.Drawing.Size(79, 16)
        Me.lblLocationCode.TabIndex = 391
        Me.lblLocationCode.Text = "Location Code"
        '
        'lblEmp
        '
        Me.lblEmp.FieldName = Nothing
        Me.lblEmp.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEmp.Location = New System.Drawing.Point(13, 131)
        Me.lblEmp.Name = "lblEmp"
        Me.lblEmp.Size = New System.Drawing.Size(60, 18)
        Me.lblEmp.TabIndex = 390
        Me.lblEmp.Text = "Employees"
        '
        'lblStatus
        '
        Me.lblStatus.FieldName = Nothing
        Me.lblStatus.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStatus.Location = New System.Drawing.Point(13, 103)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(37, 18)
        Me.lblStatus.TabIndex = 388
        Me.lblStatus.Text = "Status"
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.gv1)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(50.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(1021, 414)
        Me.RadPageViewPage2.Text = "Report"
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem3})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(1042, 20)
        Me.RadMenu1.TabIndex = 214
        '
        'RadMenuItem3
        '
        Me.RadMenuItem3.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItemSave, Me.RadMenuItemDelete})
        Me.RadMenuItem3.Name = "RadMenuItem3"
        Me.RadMenuItem3.Text = "Setting"
        '
        'RadMenuItemSave
        '
        Me.RadMenuItemSave.Name = "RadMenuItemSave"
        Me.RadMenuItemSave.Text = "Save Layout"
        '
        'RadMenuItemDelete
        '
        Me.RadMenuItemDelete.Name = "RadMenuItemDelete"
        Me.RadMenuItemDelete.Text = "Delete Layout"
        '
        'btnReset
        '
        Me.btnReset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnReset.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReset.ImageScalingSize = New System.Drawing.Size(68, 14)
        Me.btnReset.Location = New System.Drawing.Point(101, 6)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(90, 21)
        Me.btnReset.TabIndex = 334
        Me.btnReset.Text = "Reset"
        '
        'btnRefresh
        '
        Me.btnRefresh.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnRefresh.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRefresh.ImageScalingSize = New System.Drawing.Size(68, 14)
        Me.btnRefresh.Location = New System.Drawing.Point(12, 6)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(83, 21)
        Me.btnRefresh.TabIndex = 333
        Me.btnRefresh.Text = ">>"
        '
        'RadSplitButton1
        '
        Me.RadSplitButton1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.RadSplitButton1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rmPFWithdrawnForm, Me.rmPFEligibiltyRegister})
        Me.RadSplitButton1.Location = New System.Drawing.Point(292, 6)
        Me.RadSplitButton1.Name = "RadSplitButton1"
        Me.RadSplitButton1.Size = New System.Drawing.Size(95, 21)
        Me.RadSplitButton1.TabIndex = 332
        Me.RadSplitButton1.Text = "PF Statement"
        '
        'rmPFWithdrawnForm
        '
        Me.rmPFWithdrawnForm.Name = "rmPFWithdrawnForm"
        Me.rmPFWithdrawnForm.Text = "PF Withdrawn Form"
        '
        'rmPFEligibiltyRegister
        '
        Me.rmPFEligibiltyRegister.Name = "rmPFEligibiltyRegister"
        Me.rmPFEligibiltyRegister.Text = "PF Eligibility Register"
        '
        'btnExport
        '
        Me.btnExport.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnExport.Items.AddRange(New Telerik.WinControls.RadItem() {Me.btnExcel, Me.btnPDF})
        Me.btnExport.Location = New System.Drawing.Point(194, 6)
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Size = New System.Drawing.Size(95, 21)
        Me.btnExport.TabIndex = 331
        Me.btnExport.Text = "Export"
        '
        'btnExcel
        '
        Me.btnExcel.Image = Global.XpertERPHRandPayroll.My.Resources.Resources.MSE
        Me.btnExcel.Name = "btnExcel"
        Me.btnExcel.Text = "Excel"
        '
        'btnPDF
        '
        Me.btnPDF.Image = Global.XpertERPHRandPayroll.My.Resources.Resources.pdf
        Me.btnPDF.Name = "btnPDF"
        Me.btnPDF.Text = "PDF"
        '
        'frmEmployeeRegister
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1042, 516)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "frmEmployeeRegister"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Employee Register"
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage1.PerformLayout()
        CType(Me.lblDepartment, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDesignation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbldes, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocationName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CboStatus, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocationCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblEmp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblStatus, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnRefresh, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadSplitButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnExport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem3 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItemSave As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItemDelete As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnExport As Telerik.WinControls.UI.RadSplitButton
    Friend WithEvents btnExcel As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnPDF As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadSplitButton1 As Telerik.WinControls.UI.RadSplitButton
    Friend WithEvents rmPFWithdrawnForm As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmPFEligibiltyRegister As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents btnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnRefresh As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblEmp As common.Controls.MyLabel
    Friend WithEvents lblStatus As common.Controls.MyLabel
    Friend WithEvents lblLocationCode As common.Controls.MyLabel
    Friend WithEvents CboStatus As common.Controls.MyComboBox
    Friend WithEvents txtEmp As common.UserControls.txtMultiSelectFinder
    Friend WithEvents fndLocation As common.UserControls.txtFinder
    Friend WithEvents lblLocationName As common.Controls.MyLabel
    Friend WithEvents lbldes As common.Controls.MyLabel
    Friend WithEvents TxtDesignation As common.UserControls.txtFinder
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents txtDepartment As common.UserControls.txtFinder
    Friend WithEvents lblDepartment As common.Controls.MyLabel
    Friend WithEvents lblDesignation As common.Controls.MyLabel
End Class

