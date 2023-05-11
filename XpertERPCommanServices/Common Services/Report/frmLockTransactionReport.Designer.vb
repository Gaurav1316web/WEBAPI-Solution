<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmLockTransactionReport
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
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView()
        Me.RadPageViewPage1 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.chkUser = New common.Controls.MyCheckBox()
        Me.chkLocationCode = New Telerik.WinControls.UI.RadRadioButton()
        Me.chkLocationSegment = New Telerik.WinControls.UI.RadRadioButton()
        Me.txtLocationMult = New common.UserControls.txtMultiSelectFinder()
        Me.RadLabel4 = New Telerik.WinControls.UI.RadLabel()
        Me.RadLabel5 = New common.Controls.MyLabel()
        Me.RadLabel6 = New common.Controls.MyLabel()
        Me.dtpToLockDate = New common.Controls.MyDateTimePicker()
        Me.dtpFromLockDate = New common.Controls.MyDateTimePicker()
        Me.RadPageViewPage2 = New Telerik.WinControls.UI.RadPageViewPage()
        Me.gv = New common.UserControls.MyRadGridView()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.btnReset = New Telerik.WinControls.UI.RadButton()
        Me.btnExport = New Telerik.WinControls.UI.RadSplitButton()
        Me.btnExportExcel = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnExportPDF = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnRefresh = New Telerik.WinControls.UI.RadButton()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem3 = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmSaveLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmDeleteLayout = New Telerik.WinControls.UI.RadMenuItem()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RadPageViewPage1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.chkUser, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkLocationCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkLocationSegment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpToLockDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtpFromLockDate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageViewPage2.SuspendLayout()
        CType(Me.gv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnExport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnRefresh, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 20)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadPageView1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnReset)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnExport)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnRefresh)
        Me.SplitContainer1.Size = New System.Drawing.Size(786, 514)
        Me.SplitContainer1.SplitterDistance = 485
        Me.SplitContainer1.TabIndex = 0
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage1)
        Me.RadPageView1.Controls.Add(Me.RadPageViewPage2)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RadPageViewPage1
        Me.RadPageView1.Size = New System.Drawing.Size(786, 485)
        Me.RadPageView1.TabIndex = 0
        Me.RadPageView1.Text = "RadPageView1"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        '
        'RadPageViewPage1
        '
        Me.RadPageViewPage1.Controls.Add(Me.Panel1)
        Me.RadPageViewPage1.Controls.Add(Me.txtLocationMult)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel4)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel5)
        Me.RadPageViewPage1.Controls.Add(Me.RadLabel6)
        Me.RadPageViewPage1.Controls.Add(Me.dtpToLockDate)
        Me.RadPageViewPage1.Controls.Add(Me.dtpFromLockDate)
        Me.RadPageViewPage1.ItemSize = New System.Drawing.SizeF(41.0!, 28.0!)
        Me.RadPageViewPage1.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage1.Name = "RadPageViewPage1"
        Me.RadPageViewPage1.Size = New System.Drawing.Size(765, 437)
        Me.RadPageViewPage1.Text = "Filter"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.chkUser)
        Me.Panel1.Controls.Add(Me.chkLocationCode)
        Me.Panel1.Controls.Add(Me.chkLocationSegment)
        Me.Panel1.Location = New System.Drawing.Point(19, 3)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(461, 29)
        Me.Panel1.TabIndex = 351
        '
        'chkUser
        '
        Me.chkUser.Location = New System.Drawing.Point(251, 6)
        Me.chkUser.MyLinkLable1 = Nothing
        Me.chkUser.MyLinkLable2 = Nothing
        Me.chkUser.Name = "chkUser"
        Me.chkUser.Size = New System.Drawing.Size(58, 18)
        Me.chkUser.TabIndex = 351
        Me.chkUser.Tag1 = Nothing
        Me.chkUser.Text = "By User"
        '
        'chkLocationCode
        '
        Me.chkLocationCode.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkLocationCode.Location = New System.Drawing.Point(3, 6)
        Me.chkLocationCode.Name = "chkLocationCode"
        Me.chkLocationCode.Size = New System.Drawing.Size(92, 18)
        Me.chkLocationCode.TabIndex = 349
        Me.chkLocationCode.Text = "Location Code"
        Me.chkLocationCode.ToggleState = Telerik.WinControls.Enumerations.ToggleState.[On]
        '
        'chkLocationSegment
        '
        Me.chkLocationSegment.Location = New System.Drawing.Point(119, 6)
        Me.chkLocationSegment.Name = "chkLocationSegment"
        Me.chkLocationSegment.Size = New System.Drawing.Size(110, 18)
        Me.chkLocationSegment.TabIndex = 350
        Me.chkLocationSegment.TabStop = False
        Me.chkLocationSegment.Text = "Location Segment"
        '
        'txtLocationMult
        '
        Me.txtLocationMult.arrDispalyMember = Nothing
        Me.txtLocationMult.arrValueMember = Nothing
        Me.txtLocationMult.Location = New System.Drawing.Point(134, 87)
        Me.txtLocationMult.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLocationMult.MyLinkLable1 = Nothing
        Me.txtLocationMult.MyLinkLable2 = Nothing
        Me.txtLocationMult.MyNullText = "All"
        Me.txtLocationMult.Name = "txtLocationMult"
        Me.txtLocationMult.Size = New System.Drawing.Size(347, 19)
        Me.txtLocationMult.TabIndex = 348
        '
        'RadLabel4
        '
        Me.RadLabel4.Location = New System.Drawing.Point(17, 86)
        Me.RadLabel4.Name = "RadLabel4"
        Me.RadLabel4.Size = New System.Drawing.Size(49, 18)
        Me.RadLabel4.TabIndex = 347
        Me.RadLabel4.Text = "Location"
        '
        'RadLabel5
        '
        Me.RadLabel5.FieldName = Nothing
        Me.RadLabel5.Location = New System.Drawing.Point(17, 62)
        Me.RadLabel5.Name = "RadLabel5"
        Me.RadLabel5.Size = New System.Drawing.Size(84, 18)
        Me.RadLabel5.TabIndex = 346
        Me.RadLabel5.Text = "To Locked Date"
        '
        'RadLabel6
        '
        Me.RadLabel6.FieldName = Nothing
        Me.RadLabel6.Location = New System.Drawing.Point(17, 38)
        Me.RadLabel6.Name = "RadLabel6"
        Me.RadLabel6.Size = New System.Drawing.Size(91, 18)
        Me.RadLabel6.TabIndex = 345
        Me.RadLabel6.Text = "From Lock Date  "
        '
        'dtpToLockDate
        '
        Me.dtpToLockDate.CalculationExpression = Nothing
        Me.dtpToLockDate.CustomFormat = "dd/MM/yyyy"
        Me.dtpToLockDate.FieldCode = Nothing
        Me.dtpToLockDate.FieldDesc = Nothing
        Me.dtpToLockDate.FieldMaxLength = 0
        Me.dtpToLockDate.FieldName = Nothing
        Me.dtpToLockDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpToLockDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpToLockDate.isCalculatedField = False
        Me.dtpToLockDate.IsSourceFromTable = False
        Me.dtpToLockDate.IsSourceFromValueList = False
        Me.dtpToLockDate.IsUnique = False
        Me.dtpToLockDate.Location = New System.Drawing.Point(134, 62)
        Me.dtpToLockDate.MendatroryField = False
        Me.dtpToLockDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpToLockDate.MyLinkLable1 = Nothing
        Me.dtpToLockDate.MyLinkLable2 = Nothing
        Me.dtpToLockDate.Name = "dtpToLockDate"
        Me.dtpToLockDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpToLockDate.ReferenceFieldDesc = Nothing
        Me.dtpToLockDate.ReferenceFieldName = Nothing
        Me.dtpToLockDate.ReferenceTableName = Nothing
        Me.dtpToLockDate.ShowCheckBox = True
        Me.dtpToLockDate.Size = New System.Drawing.Size(105, 18)
        Me.dtpToLockDate.TabIndex = 344
        Me.dtpToLockDate.TabStop = False
        Me.dtpToLockDate.Text = "13/06/2011"
        Me.dtpToLockDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'dtpFromLockDate
        '
        Me.dtpFromLockDate.CalculationExpression = Nothing
        Me.dtpFromLockDate.CustomFormat = "dd/MM/yyyy"
        Me.dtpFromLockDate.FieldCode = Nothing
        Me.dtpFromLockDate.FieldDesc = Nothing
        Me.dtpFromLockDate.FieldMaxLength = 0
        Me.dtpFromLockDate.FieldName = Nothing
        Me.dtpFromLockDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpFromLockDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFromLockDate.isCalculatedField = False
        Me.dtpFromLockDate.IsSourceFromTable = False
        Me.dtpFromLockDate.IsSourceFromValueList = False
        Me.dtpFromLockDate.IsUnique = False
        Me.dtpFromLockDate.Location = New System.Drawing.Point(134, 38)
        Me.dtpFromLockDate.MendatroryField = False
        Me.dtpFromLockDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpFromLockDate.MyLinkLable1 = Nothing
        Me.dtpFromLockDate.MyLinkLable2 = Nothing
        Me.dtpFromLockDate.Name = "dtpFromLockDate"
        Me.dtpFromLockDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpFromLockDate.ReferenceFieldDesc = Nothing
        Me.dtpFromLockDate.ReferenceFieldName = Nothing
        Me.dtpFromLockDate.ReferenceTableName = Nothing
        Me.dtpFromLockDate.ShowCheckBox = True
        Me.dtpFromLockDate.Size = New System.Drawing.Size(105, 18)
        Me.dtpFromLockDate.TabIndex = 343
        Me.dtpFromLockDate.TabStop = False
        Me.dtpFromLockDate.Text = "13/06/2011"
        Me.dtpFromLockDate.Value = New Date(2011, 6, 13, 11, 29, 49, 421)
        '
        'RadPageViewPage2
        '
        Me.RadPageViewPage2.Controls.Add(Me.gv)
        Me.RadPageViewPage2.ItemSize = New System.Drawing.SizeF(50.0!, 28.0!)
        Me.RadPageViewPage2.Location = New System.Drawing.Point(10, 37)
        Me.RadPageViewPage2.Name = "RadPageViewPage2"
        Me.RadPageViewPage2.Size = New System.Drawing.Size(765, 457)
        Me.RadPageViewPage2.Text = "Report"
        '
        'gv
        '
        Me.gv.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.gv.Cursor = System.Windows.Forms.Cursors.Default
        Me.gv.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.gv.ForeColor = System.Drawing.Color.Black
        Me.gv.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gv.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gv.MasterTemplate.AllowAddNewRow = False
        Me.gv.MasterTemplate.EnableFiltering = True
        Me.gv.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv.Name = "gv"
        Me.gv.ReadOnly = True
        Me.gv.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv.ShowHeaderCellButtons = True
        Me.gv.Size = New System.Drawing.Size(765, 457)
        Me.gv.TabIndex = 7
        Me.gv.Text = "RadGridView1"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Location = New System.Drawing.Point(716, 0)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(67, 24)
        Me.btnClose.TabIndex = 19
        Me.btnClose.Text = "Close"
        '
        'btnReset
        '
        Me.btnReset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnReset.Location = New System.Drawing.Point(79, 0)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(95, 24)
        Me.btnReset.TabIndex = 17
        Me.btnReset.Text = "Reset"
        '
        'btnExport
        '
        Me.btnExport.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnExport.Items.AddRange(New Telerik.WinControls.RadItem() {Me.btnExportExcel, Me.btnExportPDF})
        Me.btnExport.Location = New System.Drawing.Point(174, 0)
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Size = New System.Drawing.Size(95, 24)
        Me.btnExport.TabIndex = 16
        Me.btnExport.Text = "Export"
        '
        'btnExportExcel
        '
        Me.btnExportExcel.AccessibleDescription = "Excel"
        Me.btnExportExcel.AccessibleName = "Excel"
        Me.btnExportExcel.Name = "btnExportExcel"
        Me.btnExportExcel.Text = "Excel"
        '
        'btnExportPDF
        '
        Me.btnExportPDF.AccessibleDescription = "PDF"
        Me.btnExportPDF.AccessibleName = "PDF"
        Me.btnExportPDF.Name = "btnExportPDF"
        Me.btnExportPDF.Text = "PDF"
        '
        'btnRefresh
        '
        Me.btnRefresh.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnRefresh.Location = New System.Drawing.Point(1, 0)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(78, 24)
        Me.btnRefresh.TabIndex = 15
        Me.btnRefresh.Text = ">>>"
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem3})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(786, 20)
        Me.RadMenu1.TabIndex = 24
        Me.RadMenu1.Text = "RadMenu1"
        '
        'RadMenuItem3
        '
        Me.RadMenuItem3.AccessibleDescription = "Setting"
        Me.RadMenuItem3.AccessibleName = "Setting"
        Me.RadMenuItem3.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rmSaveLayout, Me.rmDeleteLayout})
        Me.RadMenuItem3.Name = "RadMenuItem3"
        Me.RadMenuItem3.Text = "Setting"
        '
        'rmSaveLayout
        '
        Me.rmSaveLayout.AccessibleDescription = "Save Layout"
        Me.rmSaveLayout.AccessibleName = "Save Layout"
        Me.rmSaveLayout.Name = "rmSaveLayout"
        Me.rmSaveLayout.Text = "Save Layout"
        '
        'rmDeleteLayout
        '
        Me.rmDeleteLayout.AccessibleDescription = "Delete Layout"
        Me.rmDeleteLayout.AccessibleName = "Delete Layout"
        Me.rmDeleteLayout.Name = "rmDeleteLayout"
        Me.rmDeleteLayout.Text = "Delete Layout"
        '
        'FrmLockTransactionReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(786, 534)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Name = "FrmLockTransactionReport"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Lock Transaction Report"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RadPageViewPage1.ResumeLayout(False)
        Me.RadPageViewPage1.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.chkUser, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkLocationCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkLocationSegment, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpToLockDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtpFromLockDate, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageViewPage2.ResumeLayout(False)
        CType(Me.gv.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnExport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnRefresh, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RadPageViewPage1 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents RadPageViewPage2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents dtpToLockDate As common.Controls.MyDateTimePicker
    Friend WithEvents dtpFromLockDate As common.Controls.MyDateTimePicker
    Friend WithEvents RadLabel6 As common.Controls.MyLabel
    Friend WithEvents RadLabel5 As common.Controls.MyLabel
    Friend WithEvents txtLocationMult As common.UserControls.txtMultiSelectFinder
    Friend WithEvents RadLabel4 As Telerik.WinControls.UI.RadLabel
    Friend WithEvents btnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnExport As Telerik.WinControls.UI.RadSplitButton
    Friend WithEvents btnRefresh As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnExportExcel As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents gv As common.UserControls.MyRadGridView
    Friend WithEvents chkLocationSegment As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents chkLocationCode As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents chkUser As common.Controls.MyCheckBox
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadMenuItem3 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnExportPDF As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmSaveLayout As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmDeleteLayout As Telerik.WinControls.UI.RadMenuItem
End Class

