<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmOwnBmcExpanse
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
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadButton1 = New Telerik.WinControls.UI.RadButton()
        Me.dtpEndDate = New common.Controls.MyDateTimePicker()
        Me.MyLabel13 = New common.Controls.MyLabel()
        Me.dtStartDate = New common.Controls.MyDateTimePicker()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.gvTs = New common.UserControls.MyRadGridView()
        Me.txtRate = New common.MyNumBox()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.MyLabel4 = New common.Controls.MyLabel()
        Me.txtSNF = New common.MyNumBox()
        Me.txtFat = New common.MyNumBox()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.txtDescription = New common.Controls.MyTextBox()
        Me.lblDescription = New common.Controls.MyLabel()
        Me.chkInactive = New Telerik.WinControls.UI.RadCheckBox()
        Me.UsLock1 = New common.usLock()
        Me.rdbtnreset = New Telerik.WinControls.UI.RadButton()
        Me.txtCode = New common.UserControls.txtNavigator()
        Me.lblAdvanceCode = New common.Controls.MyLabel()
        Me.rdbtnclose = New Telerik.WinControls.UI.RadButton()
        Me.btnPost = New Telerik.WinControls.UI.RadButton()
        Me.btnDelete = New Telerik.WinControls.UI.RadButton()
        Me.btnsave = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpEndDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel13, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtStartDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvTs, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvTs.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSNF, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFat, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkInactive, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbtnreset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblAdvanceCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbtnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadButton1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.dtpEndDate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.dtStartDate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.gvTs)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtRate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel4)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtSNF)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtFat)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel13)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel3)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtDescription)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblDescription)
        Me.SplitContainer1.Panel1.Controls.Add(Me.chkInactive)
        Me.SplitContainer1.Panel1.Controls.Add(Me.UsLock1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.rdbtnreset)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtCode)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblAdvanceCode)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.rdbtnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnDelete)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Size = New System.Drawing.Size(709, 331)
        Me.SplitContainer1.SplitterDistance = 290
        Me.SplitContainer1.TabIndex = 0
        '
        'RadButton1
        '
        Me.RadButton1.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.RadButton1.Location = New System.Drawing.Point(465, 7)
        Me.RadButton1.Name = "RadButton1"
        Me.RadButton1.Size = New System.Drawing.Size(22, 21)
        Me.RadButton1.TabIndex = 1087
        Me.RadButton1.Text = "CC"
        '
        'dtpEndDate
        '
        Me.dtpEndDate.CalculationExpression = Nothing
        Me.dtpEndDate.CustomFormat = "dd/MM/yyyy"
        Me.dtpEndDate.FieldCode = Nothing
        Me.dtpEndDate.FieldDesc = Nothing
        Me.dtpEndDate.FieldMaxLength = 0
        Me.dtpEndDate.FieldName = Nothing
        Me.dtpEndDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpEndDate.isCalculatedField = False
        Me.dtpEndDate.IsSourceFromTable = False
        Me.dtpEndDate.IsSourceFromValueList = False
        Me.dtpEndDate.IsUnique = False
        Me.dtpEndDate.Location = New System.Drawing.Point(229, 50)
        Me.dtpEndDate.MendatroryField = False
        Me.dtpEndDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpEndDate.MyLinkLable1 = Me.MyLabel13
        Me.dtpEndDate.MyLinkLable2 = Nothing
        Me.dtpEndDate.Name = "dtpEndDate"
        Me.dtpEndDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpEndDate.ReferenceFieldDesc = Nothing
        Me.dtpEndDate.ReferenceFieldName = Nothing
        Me.dtpEndDate.ReferenceTableName = Nothing
        Me.dtpEndDate.ShowCheckBox = True
        Me.dtpEndDate.Size = New System.Drawing.Size(91, 18)
        Me.dtpEndDate.TabIndex = 1095
        Me.dtpEndDate.TabStop = False
        Me.dtpEndDate.Text = "13/06/2011"
        Me.dtpEndDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'MyLabel13
        '
        Me.MyLabel13.FieldName = Nothing
        Me.MyLabel13.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel13.Location = New System.Drawing.Point(168, 51)
        Me.MyLabel13.Name = "MyLabel13"
        Me.MyLabel13.Size = New System.Drawing.Size(53, 16)
        Me.MyLabel13.TabIndex = 1094
        Me.MyLabel13.Text = "End Date"
        '
        'dtStartDate
        '
        Me.dtStartDate.CalculationExpression = Nothing
        Me.dtStartDate.CustomFormat = "dd/MM/yyyy"
        Me.dtStartDate.FieldCode = Nothing
        Me.dtStartDate.FieldDesc = Nothing
        Me.dtStartDate.FieldMaxLength = 0
        Me.dtStartDate.FieldName = Nothing
        Me.dtStartDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtStartDate.isCalculatedField = False
        Me.dtStartDate.IsSourceFromTable = False
        Me.dtStartDate.IsSourceFromValueList = False
        Me.dtStartDate.IsUnique = False
        Me.dtStartDate.Location = New System.Drawing.Point(85, 50)
        Me.dtStartDate.MendatroryField = False
        Me.dtStartDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtStartDate.MyLinkLable1 = Me.MyLabel3
        Me.dtStartDate.MyLinkLable2 = Nothing
        Me.dtStartDate.Name = "dtStartDate"
        Me.dtStartDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtStartDate.ReferenceFieldDesc = Nothing
        Me.dtStartDate.ReferenceFieldName = Nothing
        Me.dtStartDate.ReferenceTableName = Nothing
        Me.dtStartDate.Size = New System.Drawing.Size(80, 18)
        Me.dtStartDate.TabIndex = 1093
        Me.dtStartDate.TabStop = False
        Me.dtStartDate.Text = "13/06/2011"
        Me.dtStartDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'MyLabel3
        '
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel3.Location = New System.Drawing.Point(12, 51)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(57, 16)
        Me.MyLabel3.TabIndex = 1092
        Me.MyLabel3.Text = "Start Date"
        '
        'gvTs
        '
        Me.gvTs.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gvTs.Location = New System.Drawing.Point(12, 98)
        '
        '
        '
        Me.gvTs.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gvTs.MasterTemplate.ShowHeaderCellButtons = True
        Me.gvTs.MasterTemplate.ViewDefinition = TableViewDefinition1
        Me.gvTs.MyStopExport = False
        Me.gvTs.Name = "gvTs"
        Me.gvTs.ShowHeaderCellButtons = True
        Me.gvTs.Size = New System.Drawing.Size(685, 189)
        Me.gvTs.TabIndex = 1102
        '
        'txtRate
        '
        Me.txtRate.BackColor = System.Drawing.Color.Transparent
        Me.txtRate.CalculationExpression = Nothing
        Me.txtRate.DecimalPlaces = 1
        Me.txtRate.FieldCode = Nothing
        Me.txtRate.FieldDesc = Nothing
        Me.txtRate.FieldMaxLength = 0
        Me.txtRate.FieldName = Nothing
        Me.txtRate.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.txtRate.isCalculatedField = False
        Me.txtRate.IsSourceFromTable = False
        Me.txtRate.IsSourceFromValueList = False
        Me.txtRate.IsUnique = False
        Me.txtRate.Location = New System.Drawing.Point(362, 69)
        Me.txtRate.MaxLength = 5
        Me.txtRate.MendatroryField = False
        Me.txtRate.MyLinkLable1 = Me.MyLabel2
        Me.txtRate.MyLinkLable2 = Nothing
        Me.txtRate.Name = "txtRate"
        Me.txtRate.ReferenceFieldDesc = Nothing
        Me.txtRate.ReferenceFieldName = Nothing
        Me.txtRate.ReferenceTableName = Nothing
        Me.txtRate.Size = New System.Drawing.Size(91, 21)
        Me.txtRate.TabIndex = 1101
        Me.txtRate.Text = "0"
        Me.txtRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtRate.Value = 0R
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel2.Location = New System.Drawing.Point(168, 71)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(29, 16)
        Me.MyLabel2.TabIndex = 1098
        Me.MyLabel2.Text = "SNF"
        '
        'MyLabel4
        '
        Me.MyLabel4.FieldName = Nothing
        Me.MyLabel4.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel4.Location = New System.Drawing.Point(327, 71)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(36, 16)
        Me.MyLabel4.TabIndex = 1100
        Me.MyLabel4.Text = "RATE"
        '
        'txtSNF
        '
        Me.txtSNF.BackColor = System.Drawing.Color.Transparent
        Me.txtSNF.CalculationExpression = Nothing
        Me.txtSNF.DecimalPlaces = 1
        Me.txtSNF.FieldCode = Nothing
        Me.txtSNF.FieldDesc = Nothing
        Me.txtSNF.FieldMaxLength = 0
        Me.txtSNF.FieldName = Nothing
        Me.txtSNF.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.txtSNF.isCalculatedField = False
        Me.txtSNF.IsSourceFromTable = False
        Me.txtSNF.IsSourceFromValueList = False
        Me.txtSNF.IsUnique = False
        Me.txtSNF.Location = New System.Drawing.Point(229, 69)
        Me.txtSNF.MaxLength = 5
        Me.txtSNF.MendatroryField = False
        Me.txtSNF.MyLinkLable1 = Me.MyLabel2
        Me.txtSNF.MyLinkLable2 = Nothing
        Me.txtSNF.Name = "txtSNF"
        Me.txtSNF.ReferenceFieldDesc = Nothing
        Me.txtSNF.ReferenceFieldName = Nothing
        Me.txtSNF.ReferenceTableName = Nothing
        Me.txtSNF.Size = New System.Drawing.Size(91, 21)
        Me.txtSNF.TabIndex = 1099
        Me.txtSNF.Text = "0"
        Me.txtSNF.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtSNF.Value = 0R
        '
        'txtFat
        '
        Me.txtFat.BackColor = System.Drawing.Color.Transparent
        Me.txtFat.CalculationExpression = Nothing
        Me.txtFat.DecimalPlaces = 1
        Me.txtFat.FieldCode = Nothing
        Me.txtFat.FieldDesc = Nothing
        Me.txtFat.FieldMaxLength = 0
        Me.txtFat.FieldName = Nothing
        Me.txtFat.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.txtFat.isCalculatedField = False
        Me.txtFat.IsSourceFromTable = False
        Me.txtFat.IsSourceFromValueList = False
        Me.txtFat.IsUnique = False
        Me.txtFat.Location = New System.Drawing.Point(85, 69)
        Me.txtFat.MaxLength = 5
        Me.txtFat.MendatroryField = False
        Me.txtFat.MyLinkLable1 = Me.MyLabel1
        Me.txtFat.MyLinkLable2 = Nothing
        Me.txtFat.Name = "txtFat"
        Me.txtFat.ReferenceFieldDesc = Nothing
        Me.txtFat.ReferenceFieldName = Nothing
        Me.txtFat.ReferenceTableName = Nothing
        Me.txtFat.Size = New System.Drawing.Size(80, 21)
        Me.txtFat.TabIndex = 1097
        Me.txtFat.Text = "0"
        Me.txtFat.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtFat.Value = 0R
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel1.Location = New System.Drawing.Point(12, 71)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(28, 16)
        Me.MyLabel1.TabIndex = 1096
        Me.MyLabel1.Text = "FAT"
        '
        'txtDescription
        '
        Me.txtDescription.CalculationExpression = Nothing
        Me.txtDescription.FieldCode = Nothing
        Me.txtDescription.FieldDesc = Nothing
        Me.txtDescription.FieldMaxLength = 0
        Me.txtDescription.FieldName = Nothing
        Me.txtDescription.isCalculatedField = False
        Me.txtDescription.IsSourceFromTable = False
        Me.txtDescription.IsSourceFromValueList = False
        Me.txtDescription.IsUnique = False
        Me.txtDescription.Location = New System.Drawing.Point(85, 29)
        Me.txtDescription.MaxLength = 100
        Me.txtDescription.MendatroryField = False
        Me.txtDescription.MyLinkLable1 = Nothing
        Me.txtDescription.MyLinkLable2 = Nothing
        Me.txtDescription.Name = "txtDescription"
        Me.txtDescription.ReferenceFieldDesc = Nothing
        Me.txtDescription.ReferenceFieldName = Nothing
        Me.txtDescription.ReferenceTableName = Nothing
        Me.txtDescription.Size = New System.Drawing.Size(573, 20)
        Me.txtDescription.TabIndex = 1091
        '
        'lblDescription
        '
        Me.lblDescription.FieldName = Nothing
        Me.lblDescription.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.lblDescription.Location = New System.Drawing.Point(12, 30)
        Me.lblDescription.Name = "lblDescription"
        Me.lblDescription.Size = New System.Drawing.Size(63, 18)
        Me.lblDescription.TabIndex = 1090
        Me.lblDescription.Text = "Description"
        '
        'chkInactive
        '
        Me.chkInactive.Enabled = False
        Me.chkInactive.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkInactive.Location = New System.Drawing.Point(596, 9)
        Me.chkInactive.Name = "chkInactive"
        Me.chkInactive.Size = New System.Drawing.Size(59, 16)
        Me.chkInactive.TabIndex = 1089
        Me.chkInactive.Text = "Inactive"
        '
        'UsLock1
        '
        Me.UsLock1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.UsLock1.Location = New System.Drawing.Point(487, 7)
        Me.UsLock1.MyFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsLock1.Name = "UsLock1"
        Me.UsLock1.Size = New System.Drawing.Size(109, 21)
        Me.UsLock1.Status = common.ERPTransactionStatus.Pending
        Me.UsLock1.TabIndex = 1088
        '
        'rdbtnreset
        '
        Me.rdbtnreset.Image = Global.XpertERPBulkProcurement.My.Resources.Resources._new
        Me.rdbtnreset.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.rdbtnreset.Location = New System.Drawing.Point(443, 7)
        Me.rdbtnreset.Name = "rdbtnreset"
        Me.rdbtnreset.Size = New System.Drawing.Size(22, 21)
        Me.rdbtnreset.TabIndex = 5
        '
        'txtCode
        '
        Me.txtCode.FieldName = Nothing
        Me.txtCode.Location = New System.Drawing.Point(85, 7)
        Me.txtCode.MendatroryField = False
        Me.txtCode.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCode.MyFont = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.txtCode.MyLinkLable1 = Me.lblAdvanceCode
        Me.txtCode.MyLinkLable2 = Nothing
        Me.txtCode.MyMaxLength = 30
        Me.txtCode.MyReadOnly = False
        Me.txtCode.Name = "txtCode"
        Me.txtCode.Size = New System.Drawing.Size(358, 21)
        Me.txtCode.TabIndex = 4
        Me.txtCode.Value = ""
        '
        'lblAdvanceCode
        '
        Me.lblAdvanceCode.FieldName = Nothing
        Me.lblAdvanceCode.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblAdvanceCode.Location = New System.Drawing.Point(12, 8)
        Me.lblAdvanceCode.Name = "lblAdvanceCode"
        Me.lblAdvanceCode.Size = New System.Drawing.Size(33, 18)
        Me.lblAdvanceCode.TabIndex = 3
        Me.lblAdvanceCode.Text = "Code"
        '
        'rdbtnclose
        '
        Me.rdbtnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.rdbtnclose.Location = New System.Drawing.Point(631, 9)
        Me.rdbtnclose.Name = "rdbtnclose"
        Me.rdbtnclose.Size = New System.Drawing.Size(66, 21)
        Me.rdbtnclose.TabIndex = 112
        Me.rdbtnclose.Text = "Close"
        '
        'btnPost
        '
        Me.btnPost.Location = New System.Drawing.Point(153, 9)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(66, 21)
        Me.btnPost.TabIndex = 111
        Me.btnPost.Text = "Post"
        '
        'btnDelete
        '
        Me.btnDelete.Location = New System.Drawing.Point(80, 9)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(66, 21)
        Me.btnDelete.TabIndex = 2
        Me.btnDelete.Text = "Delete"
        '
        'btnsave
        '
        Me.btnsave.Location = New System.Drawing.Point(7, 9)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(66, 21)
        Me.btnsave.TabIndex = 1
        Me.btnsave.Text = "Save"
        '
        'FrmOwnBmcExpanse
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(709, 331)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmOwnBmcExpanse"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "FrmOwnBmcExpanse"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpEndDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel13, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtStartDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvTs.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvTs, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSNF, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFat, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkInactive, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbtnreset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblAdvanceCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbtnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnDelete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents lblAdvanceCode As common.Controls.MyLabel
    Friend WithEvents txtCode As common.UserControls.txtNavigator
    Friend WithEvents rdbtnreset As RadButton
    Friend WithEvents RadButton1 As RadButton
    Friend WithEvents UsLock1 As common.usLock
    Friend WithEvents chkInactive As RadCheckBox
    Friend WithEvents lblDescription As common.Controls.MyLabel
    Friend WithEvents txtDescription As common.Controls.MyTextBox
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents dtStartDate As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel13 As common.Controls.MyLabel
    Friend WithEvents dtpEndDate As common.Controls.MyDateTimePicker
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents txtFat As common.MyNumBox
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents txtRate As common.MyNumBox
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents txtSNF As common.MyNumBox
    Friend WithEvents btnsave As RadButton
    Friend WithEvents btnDelete As RadButton
    Friend WithEvents btnPost As RadButton
    Friend WithEvents rdbtnclose As RadButton
    Friend WithEvents gvTs As common.UserControls.MyRadGridView
End Class
