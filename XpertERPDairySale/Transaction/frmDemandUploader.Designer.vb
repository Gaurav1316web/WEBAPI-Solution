<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmDemandUploader
    Inherits FrmMainTranScreen

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim TableViewDefinition1 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.btnGo = New Telerik.WinControls.UI.RadButton()
        Me.lblUOM = New common.Controls.MyLabel()
        Me.rgbEntryUOM = New Telerik.WinControls.UI.RadGroupBox()
        Me.rbtnLTR = New Telerik.WinControls.UI.RadRadioButton()
        Me.rbtnCratePouch = New Telerik.WinControls.UI.RadRadioButton()
        Me.rbtnCrate = New Telerik.WinControls.UI.RadRadioButton()
        Me.lblLocation = New common.Controls.MyLabel()
        Me.lblLocationDesc = New common.Controls.MyTextBox()
        Me.txtLocation = New common.UserControls.txtFinder()
        Me.lblShift = New common.Controls.MyLabel()
        Me.rgbShift = New Telerik.WinControls.UI.RadGroupBox()
        Me.rbtnEvening = New Telerik.WinControls.UI.RadRadioButton()
        Me.rbtnMorning = New Telerik.WinControls.UI.RadRadioButton()
        Me.txtDate = New common.Controls.MyDateTimePicker()
        Me.lblDate = New common.Controls.MyLabel()
        Me.btnExport = New Telerik.WinControls.UI.RadButton()
        Me.btnHistory = New Telerik.WinControls.UI.RadButton()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnSavePost = New Telerik.WinControls.UI.RadButton()
        Me.btnValidate = New Telerik.WinControls.UI.RadButton()
        Me.btnReset = New Telerik.WinControls.UI.RadButton()
        Me.btnUpload = New Telerik.WinControls.UI.RadButton()
        Me.gv1 = New common.UserControls.MyRadGridView()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblUOM, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rgbEntryUOM, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.rgbEntryUOM.SuspendLayout()
        CType(Me.rbtnLTR, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnCratePouch, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnCrate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocationDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblShift, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rgbShift, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.rgbShift.SuspendLayout()
        CType(Me.rbtnEvening, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnMorning, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnExport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnHistory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSavePost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnValidate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnUpload, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadPageView1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnExport)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnHistory)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSavePost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnValidate)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnReset)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnUpload)
        Me.SplitContainer1.Size = New System.Drawing.Size(967, 450)
        Me.SplitContainer1.SplitterDistance = 408
        Me.SplitContainer1.TabIndex = 0
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(967, 408)
        Me.RadPageView1.TabIndex = 1
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.SplitContainer2)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(98.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(946, 360)
        Me.RadPageViewPage1.Text = "Upload Demand"
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.btnGo)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblUOM)
        Me.SplitContainer2.Panel1.Controls.Add(Me.rgbEntryUOM)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblLocation)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblLocationDesc)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtLocation)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblShift)
        Me.SplitContainer2.Panel1.Controls.Add(Me.rgbShift)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtDate)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblDate)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.gv1)
        Me.SplitContainer2.Size = New System.Drawing.Size(946, 360)
        Me.SplitContainer2.SplitterDistance = 63
        Me.SplitContainer2.TabIndex = 0
        '
        'btnGo
        '
        Me.btnGo.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.btnGo.Location = New System.Drawing.Point(431, 32)
        Me.btnGo.Name = "btnGo"
        Me.btnGo.Size = New System.Drawing.Size(129, 24)
        Me.btnGo.TabIndex = 1533
        Me.btnGo.Text = ">>"
        '
        'lblUOM
        '
        Me.lblUOM.FieldName = Nothing
        Me.lblUOM.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblUOM.Location = New System.Drawing.Point(429, 12)
        Me.lblUOM.Name = "lblUOM"
        Me.lblUOM.Size = New System.Drawing.Size(62, 16)
        Me.lblUOM.TabIndex = 1532
        Me.lblUOM.Text = "Entry UOM"
        '
        'rgbEntryUOM
        '
        Me.rgbEntryUOM.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.rgbEntryUOM.Controls.Add(Me.rbtnLTR)
        Me.rgbEntryUOM.Controls.Add(Me.rbtnCratePouch)
        Me.rgbEntryUOM.Controls.Add(Me.rbtnCrate)
        Me.rgbEntryUOM.HeaderText = ""
        Me.rgbEntryUOM.Location = New System.Drawing.Point(496, 7)
        Me.rgbEntryUOM.Name = "rgbEntryUOM"
        Me.rgbEntryUOM.Size = New System.Drawing.Size(197, 24)
        Me.rgbEntryUOM.TabIndex = 1531
        '
        'rbtnLTR
        '
        Me.rbtnLTR.Location = New System.Drawing.Point(149, 4)
        Me.rbtnLTR.Name = "rbtnLTR"
        Me.rbtnLTR.Size = New System.Drawing.Size(38, 18)
        Me.rbtnLTR.TabIndex = 2
        Me.rbtnLTR.TabStop = False
        Me.rbtnLTR.Text = "LTR"
        '
        'rbtnCratePouch
        '
        Me.rbtnCratePouch.Location = New System.Drawing.Point(53, 4)
        Me.rbtnCratePouch.Name = "rbtnCratePouch"
        Me.rbtnCratePouch.Size = New System.Drawing.Size(93, 18)
        Me.rbtnCratePouch.TabIndex = 1
        Me.rbtnCratePouch.TabStop = False
        Me.rbtnCratePouch.Text = "Crate && Pouch"
        '
        'rbtnCrate
        '
        Me.rbtnCrate.CheckState = System.Windows.Forms.CheckState.Checked
        Me.rbtnCrate.Location = New System.Drawing.Point(4, 4)
        Me.rbtnCrate.Name = "rbtnCrate"
        Me.rbtnCrate.Size = New System.Drawing.Size(47, 18)
        Me.rbtnCrate.TabIndex = 0
        Me.rbtnCrate.TabStop = False
        Me.rbtnCrate.Text = "Crate"
        Me.rbtnCrate.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        '
        'lblLocation
        '
        Me.lblLocation.FieldName = Nothing
        Me.lblLocation.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLocation.Location = New System.Drawing.Point(8, 36)
        Me.lblLocation.Name = "lblLocation"
        Me.lblLocation.Size = New System.Drawing.Size(49, 16)
        Me.lblLocation.TabIndex = 1530
        Me.lblLocation.Text = "Location"
        '
        'lblLocationDesc
        '
        Me.lblLocationDesc.CalculationExpression = Nothing
        Me.lblLocationDesc.FieldCode = Nothing
        Me.lblLocationDesc.FieldDesc = Nothing
        Me.lblLocationDesc.FieldMaxLength = 0
        Me.lblLocationDesc.FieldName = Nothing
        Me.lblLocationDesc.isCalculatedField = False
        Me.lblLocationDesc.IsSourceFromTable = False
        Me.lblLocationDesc.IsSourceFromValueList = False
        Me.lblLocationDesc.IsUnique = False
        Me.lblLocationDesc.Location = New System.Drawing.Point(233, 36)
        Me.lblLocationDesc.MaxLength = 150
        Me.lblLocationDesc.MendatroryField = False
        Me.lblLocationDesc.MyLinkLable1 = Nothing
        Me.lblLocationDesc.MyLinkLable2 = Nothing
        Me.lblLocationDesc.Name = "lblLocationDesc"
        Me.lblLocationDesc.ReadOnly = True
        Me.lblLocationDesc.ReferenceFieldDesc = Nothing
        Me.lblLocationDesc.ReferenceFieldName = Nothing
        Me.lblLocationDesc.ReferenceTableName = Nothing
        Me.lblLocationDesc.Size = New System.Drawing.Size(177, 20)
        Me.lblLocationDesc.TabIndex = 1529
        Me.lblLocationDesc.TabStop = False
        '
        'txtLocation
        '
        Me.txtLocation.CalculationExpression = Nothing
        Me.txtLocation.FieldCode = Nothing
        Me.txtLocation.FieldDesc = Nothing
        Me.txtLocation.FieldMaxLength = 0
        Me.txtLocation.FieldName = Nothing
        Me.txtLocation.isCalculatedField = False
        Me.txtLocation.IsSourceFromTable = False
        Me.txtLocation.IsSourceFromValueList = False
        Me.txtLocation.IsUnique = False
        Me.txtLocation.Location = New System.Drawing.Point(63, 34)
        Me.txtLocation.MendatroryField = True
        Me.txtLocation.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLocation.MyLinkLable1 = Nothing
        Me.txtLocation.MyLinkLable2 = Nothing
        Me.txtLocation.MyReadOnly = False
        Me.txtLocation.MyShowMasterFormButton = False
        Me.txtLocation.Name = "txtLocation"
        Me.txtLocation.ReferenceFieldDesc = Nothing
        Me.txtLocation.ReferenceFieldName = Nothing
        Me.txtLocation.ReferenceTableName = Nothing
        Me.txtLocation.Size = New System.Drawing.Size(164, 20)
        Me.txtLocation.TabIndex = 1528
        Me.txtLocation.Value = ""
        '
        'lblShift
        '
        Me.lblShift.FieldName = Nothing
        Me.lblShift.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblShift.Location = New System.Drawing.Point(233, 12)
        Me.lblShift.Name = "lblShift"
        Me.lblShift.Size = New System.Drawing.Size(29, 16)
        Me.lblShift.TabIndex = 1527
        Me.lblShift.Text = "Shift"
        '
        'rgbShift
        '
        Me.rgbShift.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.rgbShift.Controls.Add(Me.rbtnEvening)
        Me.rgbShift.Controls.Add(Me.rbtnMorning)
        Me.rgbShift.HeaderText = ""
        Me.rgbShift.Location = New System.Drawing.Point(269, 7)
        Me.rgbShift.Name = "rgbShift"
        Me.rgbShift.Size = New System.Drawing.Size(141, 24)
        Me.rgbShift.TabIndex = 68
        '
        'rbtnEvening
        '
        Me.rbtnEvening.Location = New System.Drawing.Point(73, 4)
        Me.rbtnEvening.Name = "rbtnEvening"
        Me.rbtnEvening.Size = New System.Drawing.Size(59, 18)
        Me.rbtnEvening.TabIndex = 1
        Me.rbtnEvening.TabStop = False
        Me.rbtnEvening.Text = "Evening"
        '
        'rbtnMorning
        '
        Me.rbtnMorning.CheckState = System.Windows.Forms.CheckState.Checked
        Me.rbtnMorning.Location = New System.Drawing.Point(4, 4)
        Me.rbtnMorning.Name = "rbtnMorning"
        Me.rbtnMorning.Size = New System.Drawing.Size(63, 18)
        Me.rbtnMorning.TabIndex = 0
        Me.rbtnMorning.TabStop = False
        Me.rbtnMorning.Text = "Morning"
        Me.rbtnMorning.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        '
        'txtDate
        '
        Me.txtDate.CalculationExpression = Nothing
        Me.txtDate.CustomFormat = "dd/MM/yyyy hh:mm tt"
        Me.txtDate.FieldCode = Nothing
        Me.txtDate.FieldDesc = Nothing
        Me.txtDate.FieldMaxLength = 0
        Me.txtDate.FieldName = Nothing
        Me.txtDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtDate.isCalculatedField = False
        Me.txtDate.IsSourceFromTable = False
        Me.txtDate.IsSourceFromValueList = False
        Me.txtDate.IsUnique = False
        Me.txtDate.Location = New System.Drawing.Point(63, 9)
        Me.txtDate.MendatroryField = False
        Me.txtDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.MyLinkLable1 = Me.lblDate
        Me.txtDate.MyLinkLable2 = Nothing
        Me.txtDate.Name = "txtDate"
        Me.txtDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.ReferenceFieldDesc = Nothing
        Me.txtDate.ReferenceFieldName = Nothing
        Me.txtDate.ReferenceTableName = Nothing
        Me.txtDate.Size = New System.Drawing.Size(164, 18)
        Me.txtDate.TabIndex = 1526
        Me.txtDate.TabStop = False
        Me.txtDate.Text = "13/06/2011 11:29 AM"
        Me.txtDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'lblDate
        '
        Me.lblDate.FieldName = Nothing
        Me.lblDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDate.Location = New System.Drawing.Point(8, 9)
        Me.lblDate.Name = "lblDate"
        Me.lblDate.Size = New System.Drawing.Size(30, 16)
        Me.lblDate.TabIndex = 1525
        Me.lblDate.Text = "Date"
        '
        'btnExport
        '
        Me.btnExport.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnExport.Location = New System.Drawing.Point(85, 8)
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Size = New System.Drawing.Size(91, 24)
        Me.btnExport.TabIndex = 6
        Me.btnExport.Text = "Export Excel"
        '
        'btnHistory
        '
        Me.btnHistory.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnHistory.Location = New System.Drawing.Point(556, 8)
        Me.btnHistory.Name = "btnHistory"
        Me.btnHistory.Size = New System.Drawing.Size(91, 24)
        Me.btnHistory.TabIndex = 5
        Me.btnHistory.Text = "History"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Location = New System.Drawing.Point(368, 8)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(91, 24)
        Me.btnSave.TabIndex = 4
        Me.btnSave.Text = "Save"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Location = New System.Drawing.Point(877, 7)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(78, 24)
        Me.btnClose.TabIndex = 1
        Me.btnClose.Text = "Close"
        '
        'btnSavePost
        '
        Me.btnSavePost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSavePost.Location = New System.Drawing.Point(461, 8)
        Me.btnSavePost.Name = "btnSavePost"
        Me.btnSavePost.Size = New System.Drawing.Size(91, 24)
        Me.btnSavePost.TabIndex = 3
        Me.btnSavePost.Text = "Save and Post"
        '
        'btnValidate
        '
        Me.btnValidate.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnValidate.Location = New System.Drawing.Point(275, 7)
        Me.btnValidate.Name = "btnValidate"
        Me.btnValidate.Size = New System.Drawing.Size(91, 24)
        Me.btnValidate.TabIndex = 2
        Me.btnValidate.Text = "Validate"
        '
        'btnReset
        '
        Me.btnReset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnReset.Location = New System.Drawing.Point(15, 8)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(64, 24)
        Me.btnReset.TabIndex = 1
        Me.btnReset.Text = "Reset"
        '
        'btnUpload
        '
        Me.btnUpload.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnUpload.Location = New System.Drawing.Point(181, 7)
        Me.btnUpload.Name = "btnUpload"
        Me.btnUpload.Size = New System.Drawing.Size(91, 24)
        Me.btnUpload.TabIndex = 0
        Me.btnUpload.Text = "Upload Excel"
        '
        'gv1
        '
        Me.gv1.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.gv1.Cursor = System.Windows.Forms.Cursors.Default
        Me.gv1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv1.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.gv1.ForeColor = System.Drawing.Color.Black
        Me.gv1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gv1.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gv1.MasterTemplate.AllowAddNewRow = False
        Me.gv1.MasterTemplate.AllowDragToGroup = False
        Me.gv1.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gv1.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv1.MasterTemplate.ViewDefinition = TableViewDefinition1
        Me.gv1.MyExportFilePath = ""
        Me.gv1.MyStopExport = False
        Me.gv1.Name = "gv1"
        Me.gv1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv1.ShowHeaderCellButtons = True
        Me.gv1.Size = New System.Drawing.Size(946, 293)
        Me.gv1.TabIndex = 137
        Me.gv1.VarID = ""
        '
        'frmDemandUploader
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(967, 450)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "frmDemandUploader"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "frmDemandUploader"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.PerformLayout()
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblUOM, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rgbEntryUOM, System.ComponentModel.ISupportInitialize).EndInit()
        Me.rgbEntryUOM.ResumeLayout(False)
        Me.rgbEntryUOM.PerformLayout()
        CType(Me.rbtnLTR, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnCratePouch, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnCrate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocationDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblShift, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rgbShift, System.ComponentModel.ISupportInitialize).EndInit()
        Me.rgbShift.ResumeLayout(False)
        Me.rgbShift.PerformLayout()
        CType(Me.rbtnEvening, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnMorning, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnExport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnHistory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSavePost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnValidate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnUpload, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents btnClose As RadButton
    Friend WithEvents btnSavePost As RadButton
    Friend WithEvents btnValidate As RadButton
    Friend WithEvents btnReset As RadButton
    Friend WithEvents btnUpload As RadButton
    Friend WithEvents SplitContainer2 As SplitContainer
    Friend WithEvents txtDate As common.Controls.MyDateTimePicker
    Friend WithEvents lblDate As common.Controls.MyLabel
    Friend WithEvents rgbShift As RadGroupBox
    Friend WithEvents rbtnEvening As RadRadioButton
    Friend WithEvents rbtnMorning As RadRadioButton
    Friend WithEvents lblShift As common.Controls.MyLabel
    Friend WithEvents lblLocation As common.Controls.MyLabel
    Friend WithEvents lblLocationDesc As common.Controls.MyTextBox
    Friend WithEvents txtLocation As common.UserControls.txtFinder
    Friend WithEvents RadPageView1 As RadPageView
    Friend WithEvents RadPageViewPage1 As RadPageViewPage
    Friend WithEvents btnSave As RadButton
    Friend WithEvents btnHistory As RadButton
    Friend WithEvents lblUOM As common.Controls.MyLabel
    Friend WithEvents rgbEntryUOM As RadGroupBox
    Friend WithEvents rbtnLTR As RadRadioButton
    Friend WithEvents rbtnCratePouch As RadRadioButton
    Friend WithEvents rbtnCrate As RadRadioButton
    Friend WithEvents btnGo As RadButton
    Friend WithEvents btnExport As RadButton
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
End Class
