<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmDemandHistory
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
        Dim RadListDataItem4 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem5 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem6 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim TableViewDefinition2 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem2 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem4 = New Telerik.WinControls.UI.RadMenuItem()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.btnGo = New Telerik.WinControls.UI.RadButton()
        Me.cmbShift = New common.Controls.MyComboBox()
        Me.lblShift = New common.Controls.MyLabel()
        Me.lblDate = New common.Controls.MyLabel()
        Me.lblBooth = New common.Controls.MyLabel()
        Me.txtBoothDesc = New common.Controls.MyTextBox()
        Me.txtBooth = New common.UserControls.txtFinder()
        Me.txtDate = New common.Controls.MyDateTimePicker()
        Me.gv1 = New common.UserControls.MyRadGridView()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.btnExport = New Telerik.WinControls.UI.RadSplitButton()
        Me.rmiExcel = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmiPDF = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnreset = New Telerik.WinControls.UI.RadButton()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbShift, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblShift, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBooth, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtBoothDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnExport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnreset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(800, 20)
        Me.RadMenu1.TabIndex = 1
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem2, Me.RadMenuItem4})
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "Settings"
        Me.RadMenuItem1.UseCompatibleTextRendering = False
        '
        'RadMenuItem2
        '
        Me.RadMenuItem2.Name = "RadMenuItem2"
        Me.RadMenuItem2.Text = "Save Layout"
        Me.RadMenuItem2.UseCompatibleTextRendering = False
        '
        'RadMenuItem4
        '
        Me.RadMenuItem4.Name = "RadMenuItem4"
        Me.RadMenuItem4.Text = "Delete Layout"
        Me.RadMenuItem4.UseCompatibleTextRendering = False
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.SplitContainer2)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnExport)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnreset)
        Me.SplitContainer1.Size = New System.Drawing.Size(800, 430)
        Me.SplitContainer1.SplitterDistance = 390
        Me.SplitContainer1.TabIndex = 2
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
        Me.SplitContainer2.Panel1.Controls.Add(Me.cmbShift)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblShift)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblDate)
        Me.SplitContainer2.Panel1.Controls.Add(Me.lblBooth)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtBoothDesc)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtBooth)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtDate)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.gv1)
        Me.SplitContainer2.Size = New System.Drawing.Size(800, 390)
        Me.SplitContainer2.SplitterDistance = 129
        Me.SplitContainer2.TabIndex = 0
        '
        'btnGo
        '
        Me.btnGo.Location = New System.Drawing.Point(449, 39)
        Me.btnGo.Name = "btnGo"
        Me.btnGo.Size = New System.Drawing.Size(88, 19)
        Me.btnGo.TabIndex = 1519
        Me.btnGo.Text = ">>>"
        '
        'cmbShift
        '
        Me.cmbShift.AutoCompleteDisplayMember = Nothing
        Me.cmbShift.AutoCompleteValueMember = Nothing
        Me.cmbShift.CalculationExpression = Nothing
        Me.cmbShift.DropDownAnimationEnabled = True
        Me.cmbShift.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList
        Me.cmbShift.FieldCode = Nothing
        Me.cmbShift.FieldDesc = Nothing
        Me.cmbShift.FieldMaxLength = 0
        Me.cmbShift.FieldName = Nothing
        Me.cmbShift.isCalculatedField = False
        Me.cmbShift.IsSourceFromTable = False
        Me.cmbShift.IsSourceFromValueList = False
        Me.cmbShift.IsUnique = False
        RadListDataItem4.Text = "Morning"
        RadListDataItem5.Text = "Evening"
        RadListDataItem6.Text = "Both"
        Me.cmbShift.Items.Add(RadListDataItem4)
        Me.cmbShift.Items.Add(RadListDataItem5)
        Me.cmbShift.Items.Add(RadListDataItem6)
        Me.cmbShift.Location = New System.Drawing.Point(298, 8)
        Me.cmbShift.MendatroryField = False
        Me.cmbShift.MyLinkLable1 = Nothing
        Me.cmbShift.MyLinkLable2 = Nothing
        Me.cmbShift.Name = "cmbShift"
        Me.cmbShift.ReferenceFieldDesc = Nothing
        Me.cmbShift.ReferenceFieldName = Nothing
        Me.cmbShift.ReferenceTableName = Nothing
        Me.cmbShift.Size = New System.Drawing.Size(99, 20)
        Me.cmbShift.TabIndex = 1517
        '
        'lblShift
        '
        Me.lblShift.FieldName = Nothing
        Me.lblShift.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblShift.Location = New System.Drawing.Point(226, 10)
        Me.lblShift.Name = "lblShift"
        Me.lblShift.Size = New System.Drawing.Size(57, 16)
        Me.lblShift.TabIndex = 1518
        Me.lblShift.Text = "Shift Type"
        '
        'lblDate
        '
        Me.lblDate.FieldName = Nothing
        Me.lblDate.Location = New System.Drawing.Point(21, 7)
        Me.lblDate.Name = "lblDate"
        Me.lblDate.Size = New System.Drawing.Size(30, 18)
        Me.lblDate.TabIndex = 55
        Me.lblDate.Text = "Date"
        '
        'lblBooth
        '
        Me.lblBooth.FieldName = Nothing
        Me.lblBooth.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBooth.Location = New System.Drawing.Point(21, 38)
        Me.lblBooth.Name = "lblBooth"
        Me.lblBooth.Size = New System.Drawing.Size(36, 16)
        Me.lblBooth.TabIndex = 57
        Me.lblBooth.Text = "Booth"
        '
        'txtBoothDesc
        '
        Me.txtBoothDesc.CalculationExpression = Nothing
        Me.txtBoothDesc.FieldCode = Nothing
        Me.txtBoothDesc.FieldDesc = Nothing
        Me.txtBoothDesc.FieldMaxLength = 0
        Me.txtBoothDesc.FieldName = Nothing
        Me.txtBoothDesc.isCalculatedField = False
        Me.txtBoothDesc.IsSourceFromTable = False
        Me.txtBoothDesc.IsSourceFromValueList = False
        Me.txtBoothDesc.IsUnique = False
        Me.txtBoothDesc.Location = New System.Drawing.Point(225, 38)
        Me.txtBoothDesc.MaxLength = 150
        Me.txtBoothDesc.MendatroryField = False
        Me.txtBoothDesc.MyLinkLable1 = Nothing
        Me.txtBoothDesc.MyLinkLable2 = Nothing
        Me.txtBoothDesc.Name = "txtBoothDesc"
        Me.txtBoothDesc.ReadOnly = True
        Me.txtBoothDesc.ReferenceFieldDesc = Nothing
        Me.txtBoothDesc.ReferenceFieldName = Nothing
        Me.txtBoothDesc.ReferenceTableName = Nothing
        Me.txtBoothDesc.Size = New System.Drawing.Size(202, 20)
        Me.txtBoothDesc.TabIndex = 56
        Me.txtBoothDesc.TabStop = False
        '
        'txtBooth
        '
        Me.txtBooth.CalculationExpression = Nothing
        Me.txtBooth.FieldCode = Nothing
        Me.txtBooth.FieldDesc = Nothing
        Me.txtBooth.FieldMaxLength = 0
        Me.txtBooth.FieldName = Nothing
        Me.txtBooth.isCalculatedField = False
        Me.txtBooth.IsSourceFromTable = False
        Me.txtBooth.IsSourceFromValueList = False
        Me.txtBooth.IsUnique = False
        Me.txtBooth.Location = New System.Drawing.Point(89, 38)
        Me.txtBooth.MendatroryField = True
        Me.txtBooth.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBooth.MyLinkLable1 = Nothing
        Me.txtBooth.MyLinkLable2 = Nothing
        Me.txtBooth.MyReadOnly = False
        Me.txtBooth.MyShowMasterFormButton = False
        Me.txtBooth.Name = "txtBooth"
        Me.txtBooth.ReferenceFieldDesc = Nothing
        Me.txtBooth.ReferenceFieldName = Nothing
        Me.txtBooth.ReferenceTableName = Nothing
        Me.txtBooth.Size = New System.Drawing.Size(128, 19)
        Me.txtBooth.TabIndex = 54
        Me.txtBooth.Value = ""
        '
        'txtDate
        '
        Me.txtDate.CalculationExpression = Nothing
        Me.txtDate.CustomFormat = "dd-MM-yyyy"
        Me.txtDate.FieldCode = Nothing
        Me.txtDate.FieldDesc = Nothing
        Me.txtDate.FieldMaxLength = 0
        Me.txtDate.FieldName = Nothing
        Me.txtDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtDate.isCalculatedField = False
        Me.txtDate.IsSourceFromTable = False
        Me.txtDate.IsSourceFromValueList = False
        Me.txtDate.IsUnique = False
        Me.txtDate.Location = New System.Drawing.Point(88, 7)
        Me.txtDate.MendatroryField = False
        Me.txtDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.MyLinkLable1 = Nothing
        Me.txtDate.MyLinkLable2 = Nothing
        Me.txtDate.Name = "txtDate"
        Me.txtDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.ReferenceFieldDesc = Nothing
        Me.txtDate.ReferenceFieldName = Nothing
        Me.txtDate.ReferenceTableName = Nothing
        Me.txtDate.Size = New System.Drawing.Size(127, 20)
        Me.txtDate.TabIndex = 53
        Me.txtDate.TabStop = False
        Me.txtDate.Text = "14-09-2011"
        Me.txtDate.Value = New Date(2011, 9, 14, 0, 0, 0, 0)
        '
        'gv1
        '
        Me.gv1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv1.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gv1.MasterTemplate.AllowAddNewRow = False
        Me.gv1.MasterTemplate.EnableFiltering = True
        Me.gv1.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gv1.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv1.MasterTemplate.ViewDefinition = TableViewDefinition2
        Me.gv1.Name = "gv1"
        Me.gv1.ShowHeaderCellButtons = True
        Me.gv1.Size = New System.Drawing.Size(800, 257)
        Me.gv1.TabIndex = 1
        Me.gv1.TabStop = False
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Location = New System.Drawing.Point(718, 9)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(70, 19)
        Me.btnclose.TabIndex = 86
        Me.btnclose.Text = "Close"
        '
        'btnExport
        '
        Me.btnExport.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnExport.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rmiExcel, Me.rmiPDF})
        Me.btnExport.Location = New System.Drawing.Point(97, 9)
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Size = New System.Drawing.Size(79, 19)
        Me.btnExport.TabIndex = 85
        Me.btnExport.Text = "Export"
        '
        'rmiExcel
        '
        Me.rmiExcel.Name = "rmiExcel"
        Me.rmiExcel.Text = "Excel"
        Me.rmiExcel.UseCompatibleTextRendering = False
        '
        'rmiPDF
        '
        Me.rmiPDF.Name = "rmiPDF"
        Me.rmiPDF.Text = "PDF"
        Me.rmiPDF.UseCompatibleTextRendering = False
        '
        'btnreset
        '
        Me.btnreset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnreset.Location = New System.Drawing.Point(21, 9)
        Me.btnreset.Name = "btnreset"
        Me.btnreset.Size = New System.Drawing.Size(70, 19)
        Me.btnreset.TabIndex = 84
        Me.btnreset.Text = "Reset"
        '
        'frmDemandHistory
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.RadMenu1)
        Me.Name = "frmDemandHistory"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Demand History"
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.PerformLayout()
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.btnGo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbShift, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblShift, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBooth, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtBoothDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnExport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnreset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents RadMenu1 As RadMenu
    Friend WithEvents RadMenuItem1 As RadMenuItem
    Friend WithEvents RadMenuItem2 As RadMenuItem
    Friend WithEvents RadMenuItem4 As RadMenuItem
    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents SplitContainer2 As SplitContainer
    Friend WithEvents btnExport As RadSplitButton
    Friend WithEvents rmiExcel As RadMenuItem
    Friend WithEvents rmiPDF As RadMenuItem
    Friend WithEvents btnreset As RadButton
    Friend WithEvents btnclose As RadButton
    Friend WithEvents lblDate As common.Controls.MyLabel
    Friend WithEvents lblBooth As common.Controls.MyLabel
    Friend WithEvents txtBoothDesc As common.Controls.MyTextBox
    Friend WithEvents txtBooth As common.UserControls.txtFinder
    Friend WithEvents txtDate As common.Controls.MyDateTimePicker
    Friend WithEvents cmbShift As common.Controls.MyComboBox
    Friend WithEvents lblShift As common.Controls.MyLabel
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents btnGo As RadButton
End Class
