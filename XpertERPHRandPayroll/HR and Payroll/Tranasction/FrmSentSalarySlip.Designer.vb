<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmSentSalarySlip
    Inherits XpertERPEngine.FrmMainTranScreen

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
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.gbEmployee = New Telerik.WinControls.UI.RadGroupBox()
        Me.cbgEmp = New common.MyCheckBoxGrid()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.chkLocationSelect = New common.Controls.MyRadioButton()
        Me.chkLocationAll = New common.Controls.MyRadioButton()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.rbtnFormat2 = New common.Controls.MyRadioButton()
        Me.rbtnFormat1 = New common.Controls.MyRadioButton()
        Me.RadLabel1 = New common.Controls.MyLabel()
        Me.lblFrompp = New common.Controls.MyLabel()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.txtFromPP1 = New common.UserControls.txtFinder()
        Me.txtLocCode = New common.UserControls.txtMultiSelectFinder()
        Me.lblDivision = New common.Controls.MyLabel()
        Me.lblDepartment = New common.Controls.MyLabel()
        Me.txtDivisionMult = New common.UserControls.txtMultiSelectFinder()
        Me.TxtMultiSelectFinder1 = New common.UserControls.txtMultiSelectFinder()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.btnSendMail = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.gbEmployee, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbEmployee.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.chkLocationSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkLocationAll, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.rbtnFormat2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnFormat1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblFrompp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDivision, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDepartment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSendMail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer1.IsSplitterFixed = True
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.gbEmployee)
        Me.SplitContainer1.Panel1.Controls.Add(Me.GroupBox1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSendMail)
        Me.SplitContainer1.Size = New System.Drawing.Size(1113, 464)
        Me.SplitContainer1.SplitterDistance = 422
        Me.SplitContainer1.TabIndex = 0
        '
        'gbEmployee
        '
        Me.gbEmployee.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.gbEmployee.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.gbEmployee.Controls.Add(Me.cbgEmp)
        Me.gbEmployee.Controls.Add(Me.Panel2)
        Me.gbEmployee.HeaderText = "Employee"
        Me.gbEmployee.Location = New System.Drawing.Point(9, 147)
        Me.gbEmployee.Name = "gbEmployee"
        Me.gbEmployee.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.gbEmployee.Size = New System.Drawing.Size(541, 262)
        Me.gbEmployee.TabIndex = 370
        Me.gbEmployee.Text = "Employee"
        '
        'cbgEmp
        '
        Me.cbgEmp.CheckedValue = Nothing
        Me.cbgEmp.DataSource = Nothing
        Me.cbgEmp.DisplayMember = "Name"
        Me.cbgEmp.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbgEmp.Location = New System.Drawing.Point(10, 40)
        Me.cbgEmp.MyAutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill
        Me.cbgEmp.MyShowHeadrText = False
        Me.cbgEmp.Name = "cbgEmp"
        Me.cbgEmp.Size = New System.Drawing.Size(521, 212)
        Me.cbgEmp.TabIndex = 1
        Me.cbgEmp.TabStop = False
        Me.cbgEmp.ValueMember = "Code"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.chkLocationSelect)
        Me.Panel2.Controls.Add(Me.chkLocationAll)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(10, 20)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(521, 20)
        Me.Panel2.TabIndex = 0
        '
        'chkLocationSelect
        '
        Me.chkLocationSelect.Location = New System.Drawing.Point(291, 1)
        Me.chkLocationSelect.MyLinkLable1 = Nothing
        Me.chkLocationSelect.MyLinkLable2 = Nothing
        Me.chkLocationSelect.Name = "chkLocationSelect"
        Me.chkLocationSelect.Size = New System.Drawing.Size(50, 18)
        Me.chkLocationSelect.TabIndex = 1
        Me.chkLocationSelect.Text = "Select"
        '
        'chkLocationAll
        '
        Me.chkLocationAll.Location = New System.Drawing.Point(114, 1)
        Me.chkLocationAll.MyLinkLable1 = Nothing
        Me.chkLocationAll.MyLinkLable2 = Nothing
        Me.chkLocationAll.Name = "chkLocationAll"
        Me.chkLocationAll.Size = New System.Drawing.Size(33, 18)
        Me.chkLocationAll.TabIndex = 0
        Me.chkLocationAll.Text = "All"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.rbtnFormat2)
        Me.GroupBox1.Controls.Add(Me.rbtnFormat1)
        Me.GroupBox1.Controls.Add(Me.RadLabel1)
        Me.GroupBox1.Controls.Add(Me.lblFrompp)
        Me.GroupBox1.Controls.Add(Me.MyLabel2)
        Me.GroupBox1.Controls.Add(Me.txtFromPP1)
        Me.GroupBox1.Controls.Add(Me.txtLocCode)
        Me.GroupBox1.Controls.Add(Me.lblDivision)
        Me.GroupBox1.Controls.Add(Me.lblDepartment)
        Me.GroupBox1.Controls.Add(Me.txtDivisionMult)
        Me.GroupBox1.Controls.Add(Me.TxtMultiSelectFinder1)
        Me.GroupBox1.Location = New System.Drawing.Point(9, 2)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(541, 139)
        Me.GroupBox1.TabIndex = 369
        Me.GroupBox1.TabStop = False
        '
        'rbtnFormat2
        '
        Me.rbtnFormat2.Location = New System.Drawing.Point(176, 115)
        Me.rbtnFormat2.MyLinkLable1 = Nothing
        Me.rbtnFormat2.MyLinkLable2 = Nothing
        Me.rbtnFormat2.Name = "rbtnFormat2"
        Me.rbtnFormat2.Size = New System.Drawing.Size(65, 18)
        Me.rbtnFormat2.TabIndex = 371
        Me.rbtnFormat2.Text = "Format 2"
        '
        'rbtnFormat1
        '
        Me.rbtnFormat1.Location = New System.Drawing.Point(102, 115)
        Me.rbtnFormat1.MyLinkLable1 = Nothing
        Me.rbtnFormat1.MyLinkLable2 = Nothing
        Me.rbtnFormat1.Name = "rbtnFormat1"
        Me.rbtnFormat1.Size = New System.Drawing.Size(65, 18)
        Me.rbtnFormat1.TabIndex = 370
        Me.rbtnFormat1.Text = "Format 1"
        '
        'RadLabel1
        '
        Me.RadLabel1.FieldName = Nothing
        Me.RadLabel1.Location = New System.Drawing.Point(6, 21)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(92, 18)
        Me.RadLabel1.TabIndex = 366
        Me.RadLabel1.Text = "Select Pay Period"
        '
        'lblFrompp
        '
        Me.lblFrompp.AutoSize = False
        Me.lblFrompp.BorderVisible = True
        Me.lblFrompp.FieldName = Nothing
        Me.lblFrompp.Location = New System.Drawing.Point(330, 21)
        Me.lblFrompp.Name = "lblFrompp"
        Me.lblFrompp.Size = New System.Drawing.Size(201, 19)
        Me.lblFrompp.TabIndex = 368
        Me.lblFrompp.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Location = New System.Drawing.Point(8, 46)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(49, 18)
        Me.MyLabel2.TabIndex = 359
        Me.MyLabel2.Text = "Location"
        '
        'txtFromPP1
        '
        Me.txtFromPP1.CalculationExpression = Nothing
        Me.txtFromPP1.FieldCode = Nothing
        Me.txtFromPP1.FieldDesc = Nothing
        Me.txtFromPP1.FieldMaxLength = 0
        Me.txtFromPP1.FieldName = Nothing
        Me.txtFromPP1.isCalculatedField = False
        Me.txtFromPP1.IsSourceFromTable = False
        Me.txtFromPP1.IsSourceFromValueList = False
        Me.txtFromPP1.IsUnique = False
        Me.txtFromPP1.Location = New System.Drawing.Point(102, 21)
        Me.txtFromPP1.MendatroryField = True
        Me.txtFromPP1.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFromPP1.MyLinkLable1 = Me.RadLabel1
        Me.txtFromPP1.MyLinkLable2 = Me.lblFrompp
        Me.txtFromPP1.MyReadOnly = False
        Me.txtFromPP1.MyShowMasterFormButton = False
        Me.txtFromPP1.Name = "txtFromPP1"
        Me.txtFromPP1.ReferenceFieldDesc = Nothing
        Me.txtFromPP1.ReferenceFieldName = Nothing
        Me.txtFromPP1.ReferenceTableName = Nothing
        Me.txtFromPP1.Size = New System.Drawing.Size(222, 18)
        Me.txtFromPP1.TabIndex = 367
        Me.txtFromPP1.Value = ""
        '
        'txtLocCode
        '
        Me.txtLocCode.arrDispalyMember = Nothing
        Me.txtLocCode.arrValueMember = Nothing
        Me.txtLocCode.Location = New System.Drawing.Point(102, 45)
        Me.txtLocCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLocCode.MyLinkLable1 = Nothing
        Me.txtLocCode.MyLinkLable2 = Nothing
        Me.txtLocCode.MyNullText = "All"
        Me.txtLocCode.Name = "txtLocCode"
        Me.txtLocCode.Size = New System.Drawing.Size(429, 19)
        Me.txtLocCode.TabIndex = 360
        '
        'lblDivision
        '
        Me.lblDivision.FieldName = Nothing
        Me.lblDivision.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblDivision.Location = New System.Drawing.Point(8, 67)
        Me.lblDivision.Name = "lblDivision"
        Me.lblDivision.Size = New System.Drawing.Size(50, 16)
        Me.lblDivision.TabIndex = 361
        Me.lblDivision.Text = "Division"
        '
        'lblDepartment
        '
        Me.lblDepartment.FieldName = Nothing
        Me.lblDepartment.Location = New System.Drawing.Point(8, 89)
        Me.lblDepartment.Name = "lblDepartment"
        Me.lblDepartment.Size = New System.Drawing.Size(66, 18)
        Me.lblDepartment.TabIndex = 365
        Me.lblDepartment.Text = "Department"
        '
        'txtDivisionMult
        '
        Me.txtDivisionMult.arrDispalyMember = Nothing
        Me.txtDivisionMult.arrValueMember = Nothing
        Me.txtDivisionMult.Location = New System.Drawing.Point(102, 69)
        Me.txtDivisionMult.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDivisionMult.MyLinkLable1 = Nothing
        Me.txtDivisionMult.MyLinkLable2 = Nothing
        Me.txtDivisionMult.MyNullText = "All"
        Me.txtDivisionMult.Name = "txtDivisionMult"
        Me.txtDivisionMult.Size = New System.Drawing.Size(429, 19)
        Me.txtDivisionMult.TabIndex = 362
        '
        'TxtMultiSelectFinder1
        '
        Me.TxtMultiSelectFinder1.arrDispalyMember = Nothing
        Me.TxtMultiSelectFinder1.arrValueMember = Nothing
        Me.TxtMultiSelectFinder1.Location = New System.Drawing.Point(102, 91)
        Me.TxtMultiSelectFinder1.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtMultiSelectFinder1.MyLinkLable1 = Nothing
        Me.TxtMultiSelectFinder1.MyLinkLable2 = Nothing
        Me.TxtMultiSelectFinder1.MyNullText = "All"
        Me.TxtMultiSelectFinder1.Name = "TxtMultiSelectFinder1"
        Me.TxtMultiSelectFinder1.Size = New System.Drawing.Size(429, 19)
        Me.TxtMultiSelectFinder1.TabIndex = 364
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(1017, 9)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(84, 22)
        Me.btnclose.TabIndex = 13
        Me.btnclose.Text = "Close"
        '
        'btnSendMail
        '
        Me.btnSendMail.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSendMail.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSendMail.Location = New System.Drawing.Point(12, 9)
        Me.btnSendMail.Name = "btnSendMail"
        Me.btnSendMail.Size = New System.Drawing.Size(71, 22)
        Me.btnSendMail.TabIndex = 12
        Me.btnSendMail.Text = "Send Mail"
        '
        'FrmSentSalarySlip
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1113, 464)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmSentSalarySlip"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "FrmSentSalarySlip"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.gbEmployee, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbEmployee.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.chkLocationSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkLocationAll, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.rbtnFormat2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnFormat1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblFrompp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDivision, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDepartment, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSendMail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents TxtMultiSelectFinder1 As common.UserControls.txtMultiSelectFinder
    Friend WithEvents txtDivisionMult As common.UserControls.txtMultiSelectFinder
    Friend WithEvents lblDivision As common.Controls.MyLabel
    Friend WithEvents txtLocCode As common.UserControls.txtMultiSelectFinder
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents lblDepartment As common.Controls.MyLabel
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents RadLabel1 As common.Controls.MyLabel
    Friend WithEvents lblFrompp As common.Controls.MyLabel
    Friend WithEvents txtFromPP1 As common.UserControls.txtFinder
    Friend WithEvents rbtnFormat2 As common.Controls.MyRadioButton
    Friend WithEvents rbtnFormat1 As common.Controls.MyRadioButton
    Friend WithEvents gbEmployee As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents cbgEmp As common.MyCheckBoxGrid
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents chkLocationSelect As common.Controls.MyRadioButton
    Friend WithEvents chkLocationAll As common.Controls.MyRadioButton
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnSendMail As Telerik.WinControls.UI.RadButton
End Class

