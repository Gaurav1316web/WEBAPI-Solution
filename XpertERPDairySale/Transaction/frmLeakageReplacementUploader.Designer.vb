<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmLeakageReplacementUploader
    Inherits XpertERPEngine.FrmMainTranScreen

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
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.RadGroupBox5 = New Telerik.WinControls.UI.RadGroupBox()
        Me.rbtnTaxable = New Telerik.WinControls.UI.RadRadioButton()
        Me.rbtnNonTaxable = New Telerik.WinControls.UI.RadRadioButton()
        Me.txtDate = New common.Controls.MyDateTimePicker()
        Me.RadLabel4 = New common.Controls.MyLabel()
        Me.RadGroupBox3 = New Telerik.WinControls.UI.RadGroupBox()
        Me.rdbAgainstLeakageReplacement = New Telerik.WinControls.UI.RadRadioButton()
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.Gv1 = New Telerik.WinControls.UI.RadGridView()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnSaveAndPost = New Telerik.WinControls.UI.RadButton()
        Me.btnExportInvalid = New Telerik.WinControls.UI.RadButton()
        Me.btnValidate = New Telerik.WinControls.UI.RadButton()
        Me.btnExportFormat = New Telerik.WinControls.UI.RadButton()
        Me.btnSelectSheet = New Telerik.WinControls.UI.RadButton()
        Me.OpenFileDialog = New System.Windows.Forms.OpenFileDialog()
        Me.lblLocation = New common.Controls.MyLabel()
        Me.txtLocation = New common.UserControls.txtFinder()
        Me.RadLabel15 = New common.Controls.MyLabel()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.RadGroupBox5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox5.SuspendLayout()
        CType(Me.rbtnTaxable, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnNonTaxable, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox3.SuspendLayout()
        CType(Me.rdbAgainstLeakageReplacement, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        CType(Me.Gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSaveAndPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnExportInvalid, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnValidate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnExportFormat, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSelectSheet, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel15, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.SplitContainer2)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSaveAndPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnExportInvalid)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnValidate)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnExportFormat)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSelectSheet)
        Me.SplitContainer1.Size = New System.Drawing.Size(711, 420)
        Me.SplitContainer1.SplitterDistance = 383
        Me.SplitContainer1.TabIndex = 0
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblLocation)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtLocation)
        Me.SplitContainer2.Panel1.Controls.Add(Me.RadLabel15)
        Me.SplitContainer2.Panel1.Controls.Add(Me.RadGroupBox5)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtDate)
        Me.SplitContainer2.Panel1.Controls.Add(Me.RadLabel4)
        Me.SplitContainer2.Panel1.Controls.Add(Me.RadGroupBox3)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.RadPageView1)
        Me.SplitContainer2.Size = New System.Drawing.Size(711, 383)
        Me.SplitContainer2.SplitterDistance = 120
        Me.SplitContainer2.TabIndex = 3
        '
        'RadGroupBox5
        '
        Me.RadGroupBox5.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox5.Controls.Add(Me.rbtnTaxable)
        Me.RadGroupBox5.Controls.Add(Me.rbtnNonTaxable)
        Me.RadGroupBox5.HeaderText = "Item Type"
        Me.RadGroupBox5.Location = New System.Drawing.Point(501, 41)
        Me.RadGroupBox5.Name = "RadGroupBox5"
        Me.RadGroupBox5.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox5.Size = New System.Drawing.Size(176, 37)
        Me.RadGroupBox5.TabIndex = 1481
        Me.RadGroupBox5.Text = "Item Type"
        Me.RadGroupBox5.Visible = False
        '
        'rbtnTaxable
        '
        Me.rbtnTaxable.Location = New System.Drawing.Point(92, 15)
        Me.rbtnTaxable.Name = "rbtnTaxable"
        Me.rbtnTaxable.Size = New System.Drawing.Size(58, 18)
        Me.rbtnTaxable.TabIndex = 1
        Me.rbtnTaxable.Text = "Taxable"
        '
        'rbtnNonTaxable
        '
        Me.rbtnNonTaxable.CheckState = System.Windows.Forms.CheckState.Checked
        Me.rbtnNonTaxable.Location = New System.Drawing.Point(3, 15)
        Me.rbtnNonTaxable.Name = "rbtnNonTaxable"
        Me.rbtnNonTaxable.Size = New System.Drawing.Size(83, 18)
        Me.rbtnNonTaxable.TabIndex = 1
        Me.rbtnNonTaxable.Text = "Non Taxable"
        Me.rbtnNonTaxable.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
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
        Me.txtDate.Location = New System.Drawing.Point(94, 39)
        Me.txtDate.MendatroryField = False
        Me.txtDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.MyLinkLable1 = Me.RadLabel4
        Me.txtDate.MyLinkLable2 = Nothing
        Me.txtDate.Name = "txtDate"
        Me.txtDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.ReferenceFieldDesc = Nothing
        Me.txtDate.ReferenceFieldName = Nothing
        Me.txtDate.ReferenceTableName = Nothing
        Me.txtDate.Size = New System.Drawing.Size(125, 18)
        Me.txtDate.TabIndex = 1480
        Me.txtDate.TabStop = False
        Me.txtDate.Text = "13/06/2011 11:29 AM"
        Me.txtDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'RadLabel4
        '
        Me.RadLabel4.FieldName = Nothing
        Me.RadLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel4.Location = New System.Drawing.Point(12, 39)
        Me.RadLabel4.Name = "RadLabel4"
        Me.RadLabel4.Size = New System.Drawing.Size(30, 16)
        Me.RadLabel4.TabIndex = 1479
        Me.RadLabel4.Text = "Date"
        '
        'RadGroupBox3
        '
        Me.RadGroupBox3.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox3.Controls.Add(Me.rdbAgainstLeakageReplacement)
        Me.RadGroupBox3.HeaderText = ""
        Me.RadGroupBox3.Location = New System.Drawing.Point(12, 12)
        Me.RadGroupBox3.Name = "RadGroupBox3"
        Me.RadGroupBox3.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox3.Size = New System.Drawing.Size(665, 23)
        Me.RadGroupBox3.TabIndex = 1
        '
        'rdbAgainstLeakageReplacement
        '
        Me.rdbAgainstLeakageReplacement.CheckState = System.Windows.Forms.CheckState.Checked
        Me.rdbAgainstLeakageReplacement.Location = New System.Drawing.Point(117, 2)
        Me.rdbAgainstLeakageReplacement.Name = "rdbAgainstLeakageReplacement"
        Me.rdbAgainstLeakageReplacement.Size = New System.Drawing.Size(170, 18)
        Me.rdbAgainstLeakageReplacement.TabIndex = 1
        Me.rdbAgainstLeakageReplacement.Text = "Against Leakage Replacement"
        Me.rdbAgainstLeakageReplacement.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(711, 259)
        Me.RadPageView1.TabIndex = 24
        Me.RadPageView1.Text = "Truck Sheet Import"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.AutoScroll = True
        Me.RadPageViewPage1.Controls.Add(Me.Gv1)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(44.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(690, 211)
        Me.RadPageViewPage1.Text = "Items"
        '
        'Gv1
        '
        Me.Gv1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Gv1.Location = New System.Drawing.Point(0, 0)
        Me.Gv1.Name = "Gv1"
        Me.Gv1.Size = New System.Drawing.Size(690, 211)
        Me.Gv1.TabIndex = 2
        Me.Gv1.Text = "RadGridView1"
        Me.Gv1.Visible = False
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(642, 9)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(66, 18)
        Me.btnClose.TabIndex = 13
        Me.btnClose.Text = "Close"
        '
        'btnSaveAndPost
        '
        Me.btnSaveAndPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSaveAndPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSaveAndPost.Location = New System.Drawing.Point(145, 9)
        Me.btnSaveAndPost.Name = "btnSaveAndPost"
        Me.btnSaveAndPost.Size = New System.Drawing.Size(88, 18)
        Me.btnSaveAndPost.TabIndex = 12
        Me.btnSaveAndPost.Text = "Save"
        '
        'btnExportInvalid
        '
        Me.btnExportInvalid.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnExportInvalid.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExportInvalid.Location = New System.Drawing.Point(233, 9)
        Me.btnExportInvalid.Name = "btnExportInvalid"
        Me.btnExportInvalid.Size = New System.Drawing.Size(106, 18)
        Me.btnExportInvalid.TabIndex = 11
        Me.btnExportInvalid.Text = "Export Unvalidated"
        '
        'btnValidate
        '
        Me.btnValidate.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnValidate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnValidate.Location = New System.Drawing.Point(77, 9)
        Me.btnValidate.Name = "btnValidate"
        Me.btnValidate.Size = New System.Drawing.Size(66, 18)
        Me.btnValidate.TabIndex = 10
        Me.btnValidate.Text = "Validate"
        '
        'btnExportFormat
        '
        Me.btnExportFormat.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnExportFormat.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExportFormat.Location = New System.Drawing.Point(341, 9)
        Me.btnExportFormat.Name = "btnExportFormat"
        Me.btnExportFormat.Size = New System.Drawing.Size(83, 18)
        Me.btnExportFormat.TabIndex = 9
        Me.btnExportFormat.Text = "Export Format"
        '
        'btnSelectSheet
        '
        Me.btnSelectSheet.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSelectSheet.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSelectSheet.Location = New System.Drawing.Point(5, 9)
        Me.btnSelectSheet.Name = "btnSelectSheet"
        Me.btnSelectSheet.Size = New System.Drawing.Size(72, 18)
        Me.btnSelectSheet.TabIndex = 8
        Me.btnSelectSheet.Text = "Select Sheet"
        '
        'OpenFileDialog
        '
        Me.OpenFileDialog.FileName = "OpenFileDialog1"
        '
        'lblLocation
        '
        Me.lblLocation.AutoSize = False
        Me.lblLocation.BorderVisible = True
        Me.lblLocation.FieldName = Nothing
        Me.lblLocation.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLocation.Location = New System.Drawing.Point(236, 61)
        Me.lblLocation.Name = "lblLocation"
        Me.lblLocation.Size = New System.Drawing.Size(227, 18)
        Me.lblLocation.TabIndex = 1492
        Me.lblLocation.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblLocation.TextWrap = False
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
        Me.txtLocation.Location = New System.Drawing.Point(93, 60)
        Me.txtLocation.MendatroryField = True
        Me.txtLocation.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLocation.MyLinkLable1 = Me.RadLabel15
        Me.txtLocation.MyLinkLable2 = Nothing
        Me.txtLocation.MyReadOnly = False
        Me.txtLocation.MyShowMasterFormButton = False
        Me.txtLocation.Name = "txtLocation"
        Me.txtLocation.ReferenceFieldDesc = Nothing
        Me.txtLocation.ReferenceFieldName = Nothing
        Me.txtLocation.ReferenceTableName = Nothing
        Me.txtLocation.Size = New System.Drawing.Size(142, 19)
        Me.txtLocation.TabIndex = 1491
        Me.txtLocation.Value = ""
        '
        'RadLabel15
        '
        Me.RadLabel15.FieldName = Nothing
        Me.RadLabel15.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel15.Location = New System.Drawing.Point(10, 63)
        Me.RadLabel15.Name = "RadLabel15"
        Me.RadLabel15.Size = New System.Drawing.Size(49, 16)
        Me.RadLabel15.TabIndex = 1490
        Me.RadLabel15.Text = "Location"
        '
        'frmLeakageReplacementUploader
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(711, 420)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "frmLeakageReplacementUploader"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Leakage ReplacementUploader"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.PerformLayout()
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.RadGroupBox5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox5.ResumeLayout(False)
        Me.RadGroupBox5.PerformLayout()
        CType(Me.rbtnTaxable, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnNonTaxable, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox3.ResumeLayout(False)
        Me.RadGroupBox3.PerformLayout()
        CType(Me.rdbAgainstLeakageReplacement, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        CType(Me.Gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSaveAndPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnExportInvalid, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnValidate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnExportFormat, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSelectSheet, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLocation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel15, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents Gv1 As Telerik.WinControls.UI.RadGridView
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnSaveAndPost As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnExportInvalid As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnValidate As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnExportFormat As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnSelectSheet As Telerik.WinControls.UI.RadButton
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadGroupBox3 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents rdbAgainstLeakageReplacement As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents txtDate As common.Controls.MyDateTimePicker
    Friend WithEvents RadLabel4 As common.Controls.MyLabel
    Friend WithEvents RadPageView1 As RadPageView
    Friend WithEvents RadPageViewPage1 As RadPageViewPage
    Friend WithEvents OpenFileDialog As OpenFileDialog
    Friend WithEvents RadGroupBox5 As RadGroupBox
    Friend WithEvents rbtnTaxable As RadRadioButton
    Friend WithEvents rbtnNonTaxable As RadRadioButton
    Friend WithEvents lblLocation As common.Controls.MyLabel
    Friend WithEvents txtLocation As common.UserControls.txtFinder
    Friend WithEvents RadLabel15 As common.Controls.MyLabel
End Class
