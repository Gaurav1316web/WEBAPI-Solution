<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmERPStatusTrackingReport
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
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.gv1 = New common.UserControls.MyRadGridView()
        Me.btnPrint = New Telerik.WinControls.UI.RadButton()
        Me.btnExport = New Telerik.WinControls.UI.RadSplitButton()
        Me.rmiExcel = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmiPDF = New Telerik.WinControls.UI.RadMenuItem()
        Me.btnreset = New Telerik.WinControls.UI.RadButton()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.btnReport = New Telerik.WinControls.UI.RadButton()
        Me.rdbMilkProcurement = New System.Windows.Forms.RadioButton()
        Me.rdbERPStatusMilkUnion = New System.Windows.Forms.RadioButton()
        Me.SplitContainer3 = New System.Windows.Forms.SplitContainer()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.txtFinYr = New common.UserControls.txtFinder()
        Me.rdbDBTStatus = New System.Windows.Forms.RadioButton()
        Me.txtDate = New common.Controls.MyDateTimePicker()
        Me.RadLabel3 = New common.Controls.MyLabel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label1 = New common.Controls.MyLabel()
        Me.chkDBT = New Telerik.WinControls.UI.RadCheckBox()
        Me.rbtnTranasctionAll = New System.Windows.Forms.RadioButton()
        Me.rbtnTransactionPosted = New System.Windows.Forms.RadioButton()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem2 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem4 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RMIALL = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem3 = New Telerik.WinControls.UI.RadMenuItem()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnExport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnreset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReport, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer3.Panel1.SuspendLayout()
        Me.SplitContainer3.Panel2.SuspendLayout()
        Me.SplitContainer3.SuspendLayout()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.Label1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkDBT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.IsSplitterFixed = True
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 58)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.gv1)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.btnPrint)
        Me.SplitContainer2.Panel2.Controls.Add(Me.btnExport)
        Me.SplitContainer2.Panel2.Controls.Add(Me.btnreset)
        Me.SplitContainer2.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer2.Panel2.Controls.Add(Me.btnReport)
        Me.SplitContainer2.Size = New System.Drawing.Size(703, 284)
        Me.SplitContainer2.SplitterDistance = 254
        Me.SplitContainer2.TabIndex = 1
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
        Me.gv1.MasterTemplate.ViewDefinition = TableViewDefinition1
        Me.gv1.Name = "gv1"
        Me.gv1.ShowHeaderCellButtons = True
        Me.gv1.Size = New System.Drawing.Size(703, 254)
        Me.gv1.TabIndex = 0
        Me.gv1.TabStop = False
        '
        'btnPrint
        '
        Me.btnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPrint.Location = New System.Drawing.Point(240, 3)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(70, 19)
        Me.btnPrint.TabIndex = 84
        Me.btnPrint.Text = "Print"
        '
        'btnExport
        '
        Me.btnExport.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnExport.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rmiExcel, Me.rmiPDF})
        Me.btnExport.Location = New System.Drawing.Point(155, 3)
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Size = New System.Drawing.Size(79, 19)
        Me.btnExport.TabIndex = 83
        Me.btnExport.Text = "Export"
        '
        'rmiExcel
        '
        Me.rmiExcel.Name = "rmiExcel"
        Me.rmiExcel.Text = "Excel"
        '
        'rmiPDF
        '
        Me.rmiPDF.Name = "rmiPDF"
        Me.rmiPDF.Text = "PDF"
        '
        'btnreset
        '
        Me.btnreset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnreset.Location = New System.Drawing.Point(79, 3)
        Me.btnreset.Name = "btnreset"
        Me.btnreset.Size = New System.Drawing.Size(70, 19)
        Me.btnreset.TabIndex = 0
        Me.btnreset.Text = "Reset"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Location = New System.Drawing.Point(628, 3)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(70, 19)
        Me.btnclose.TabIndex = 1
        Me.btnclose.Text = "Close"
        '
        'btnReport
        '
        Me.btnReport.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnReport.Location = New System.Drawing.Point(3, 3)
        Me.btnReport.Name = "btnReport"
        Me.btnReport.Size = New System.Drawing.Size(70, 19)
        Me.btnReport.TabIndex = 4
        Me.btnReport.Text = ">>>"
        '
        'rdbMilkProcurement
        '
        Me.rdbMilkProcurement.AutoSize = True
        Me.rdbMilkProcurement.Location = New System.Drawing.Point(164, 5)
        Me.rdbMilkProcurement.Name = "rdbMilkProcurement"
        Me.rdbMilkProcurement.Size = New System.Drawing.Size(115, 17)
        Me.rdbMilkProcurement.TabIndex = 9
        Me.rdbMilkProcurement.TabStop = True
        Me.rdbMilkProcurement.Text = "Milk Procurement"
        Me.rdbMilkProcurement.UseVisualStyleBackColor = True
        '
        'rdbERPStatusMilkUnion
        '
        Me.rdbERPStatusMilkUnion.AutoSize = True
        Me.rdbERPStatusMilkUnion.Checked = True
        Me.rdbERPStatusMilkUnion.Location = New System.Drawing.Point(3, 5)
        Me.rdbERPStatusMilkUnion.Name = "rdbERPStatusMilkUnion"
        Me.rdbERPStatusMilkUnion.Size = New System.Drawing.Size(158, 17)
        Me.rdbERPStatusMilkUnion.TabIndex = 8
        Me.rdbERPStatusMilkUnion.TabStop = True
        Me.rdbERPStatusMilkUnion.Text = "ERP Status At Milk Unions"
        Me.rdbERPStatusMilkUnion.UseVisualStyleBackColor = True
        '
        'SplitContainer3
        '
        Me.SplitContainer3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer3.IsSplitterFixed = True
        Me.SplitContainer3.Location = New System.Drawing.Point(0, 20)
        Me.SplitContainer3.Name = "SplitContainer3"
        Me.SplitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer3.Panel1
        '
        Me.SplitContainer3.Panel1.Controls.Add(Me.MyLabel1)
        Me.SplitContainer3.Panel1.Controls.Add(Me.txtFinYr)
        Me.SplitContainer3.Panel1.Controls.Add(Me.rdbDBTStatus)
        Me.SplitContainer3.Panel1.Controls.Add(Me.rdbERPStatusMilkUnion)
        Me.SplitContainer3.Panel1.Controls.Add(Me.txtDate)
        Me.SplitContainer3.Panel1.Controls.Add(Me.rdbMilkProcurement)
        Me.SplitContainer3.Panel1.Controls.Add(Me.RadLabel3)
        '
        'SplitContainer3.Panel2
        '
        Me.SplitContainer3.Panel2.Controls.Add(Me.SplitContainer2)
        Me.SplitContainer3.Panel2.Controls.Add(Me.Panel1)
        Me.SplitContainer3.Size = New System.Drawing.Size(703, 371)
        Me.SplitContainer3.SplitterDistance = 25
        Me.SplitContainer3.TabIndex = 1
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Location = New System.Drawing.Point(500, 5)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(75, 18)
        Me.MyLabel1.TabIndex = 317
        Me.MyLabel1.Text = "Financial Year"
        '
        'txtFinYr
        '
        Me.txtFinYr.CalculationExpression = Nothing
        Me.txtFinYr.FieldCode = Nothing
        Me.txtFinYr.FieldDesc = Nothing
        Me.txtFinYr.FieldMaxLength = 0
        Me.txtFinYr.FieldName = Nothing
        Me.txtFinYr.isCalculatedField = False
        Me.txtFinYr.IsSourceFromTable = False
        Me.txtFinYr.IsSourceFromValueList = False
        Me.txtFinYr.IsUnique = False
        Me.txtFinYr.Location = New System.Drawing.Point(574, 3)
        Me.txtFinYr.MendatroryField = False
        Me.txtFinYr.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFinYr.MyLinkLable1 = Nothing
        Me.txtFinYr.MyLinkLable2 = Nothing
        Me.txtFinYr.MyReadOnly = False
        Me.txtFinYr.MyShowMasterFormButton = False
        Me.txtFinYr.Name = "txtFinYr"
        Me.txtFinYr.ReferenceFieldDesc = Nothing
        Me.txtFinYr.ReferenceFieldName = Nothing
        Me.txtFinYr.ReferenceTableName = Nothing
        Me.txtFinYr.Size = New System.Drawing.Size(119, 19)
        Me.txtFinYr.TabIndex = 316
        Me.txtFinYr.Value = ""
        '
        'rdbDBTStatus
        '
        Me.rdbDBTStatus.AutoSize = True
        Me.rdbDBTStatus.Location = New System.Drawing.Point(414, 5)
        Me.rdbDBTStatus.Name = "rdbDBTStatus"
        Me.rdbDBTStatus.Size = New System.Drawing.Size(80, 17)
        Me.rdbDBTStatus.TabIndex = 11
        Me.rdbDBTStatus.TabStop = True
        Me.rdbDBTStatus.Text = "DBT Status"
        Me.rdbDBTStatus.UseVisualStyleBackColor = True
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
        Me.txtDate.Location = New System.Drawing.Point(326, 3)
        Me.txtDate.MendatroryField = False
        Me.txtDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.MyLinkLable1 = Nothing
        Me.txtDate.MyLinkLable2 = Nothing
        Me.txtDate.Name = "txtDate"
        Me.txtDate.NullDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.txtDate.ReferenceFieldDesc = Nothing
        Me.txtDate.ReferenceFieldName = Nothing
        Me.txtDate.ReferenceTableName = Nothing
        Me.txtDate.Size = New System.Drawing.Size(82, 20)
        Me.txtDate.TabIndex = 1
        Me.txtDate.TabStop = False
        Me.txtDate.Text = "17-12-2011"
        Me.txtDate.Value = New Date(2011, 12, 17, 0, 0, 0, 0)
        '
        'RadLabel3
        '
        Me.RadLabel3.FieldName = Nothing
        Me.RadLabel3.Location = New System.Drawing.Point(292, 4)
        Me.RadLabel3.Name = "RadLabel3"
        Me.RadLabel3.Size = New System.Drawing.Size(30, 18)
        Me.RadLabel3.TabIndex = 10
        Me.RadLabel3.Text = "Date"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.chkDBT)
        Me.Panel1.Controls.Add(Me.rbtnTranasctionAll)
        Me.Panel1.Controls.Add(Me.rbtnTransactionPosted)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(703, 58)
        Me.Panel1.TabIndex = 11
        '
        'Label1
        '
        Me.Label1.AutoSize = False
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label1.FieldName = Nothing
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Bold)
        Me.Label1.Location = New System.Drawing.Point(0, 35)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(703, 23)
        Me.Label1.TabIndex = 12
        Me.Label1.Text = "Report Name"
        Me.Label1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        '
        'chkDBT
        '
        Me.chkDBT.Location = New System.Drawing.Point(292, 3)
        Me.chkDBT.Name = "chkDBT"
        Me.chkDBT.Size = New System.Drawing.Size(74, 18)
        Me.chkDBT.TabIndex = 0
        Me.chkDBT.Text = "DBTReport"
        '
        'rbtnTranasctionAll
        '
        Me.rbtnTranasctionAll.AutoSize = True
        Me.rbtnTranasctionAll.Location = New System.Drawing.Point(167, 2)
        Me.rbtnTranasctionAll.Name = "rbtnTranasctionAll"
        Me.rbtnTranasctionAll.Size = New System.Drawing.Size(100, 17)
        Me.rbtnTranasctionAll.TabIndex = 10
        Me.rbtnTranasctionAll.Text = "All Transaction"
        Me.rbtnTranasctionAll.UseVisualStyleBackColor = True
        '
        'rbtnTransactionPosted
        '
        Me.rbtnTransactionPosted.AutoSize = True
        Me.rbtnTransactionPosted.Checked = True
        Me.rbtnTransactionPosted.Location = New System.Drawing.Point(3, 3)
        Me.rbtnTransactionPosted.Name = "rbtnTransactionPosted"
        Me.rbtnTransactionPosted.Size = New System.Drawing.Size(122, 17)
        Me.rbtnTransactionPosted.TabIndex = 11
        Me.rbtnTransactionPosted.TabStop = True
        Me.rbtnTransactionPosted.Text = "Posted Transaction"
        Me.rbtnTransactionPosted.UseVisualStyleBackColor = True
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(703, 20)
        Me.RadMenu1.TabIndex = 0
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem2, Me.RadMenuItem4, Me.RMIALL})
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "Settings"
        '
        'RadMenuItem2
        '
        Me.RadMenuItem2.Name = "RadMenuItem2"
        Me.RadMenuItem2.Text = "Save Layout"
        '
        'RadMenuItem4
        '
        Me.RadMenuItem4.Name = "RadMenuItem4"
        Me.RadMenuItem4.Text = "Delete Layout"
        '
        'RMIALL
        '
        Me.RMIALL.Name = "RMIALL"
        Me.RMIALL.Text = "ALL"
        '
        'RadMenuItem3
        '
        Me.RadMenuItem3.Name = "RadMenuItem3"
        Me.RadMenuItem3.Text = ""
        '
        'FrmERPStatusTrackingReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(703, 391)
        Me.Controls.Add(Me.SplitContainer3)
        Me.Controls.Add(Me.RadMenu1)
        Me.Name = "FrmERPStatusTrackingReport"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "ERP Status Tracking Report"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnExport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnreset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReport, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer3.Panel1.ResumeLayout(False)
        Me.SplitContainer3.Panel1.PerformLayout()
        Me.SplitContainer3.Panel2.ResumeLayout(False)
        Me.SplitContainer3.ResumeLayout(False)
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.Label1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkDBT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnreset As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnReport As Telerik.WinControls.UI.RadButton
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents SplitContainer3 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem2 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem4 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents btnExport As Telerik.WinControls.UI.RadSplitButton
    Friend WithEvents rmiExcel As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents rmiPDF As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents chkDBT As RadCheckBox
    Friend WithEvents rdbMilkProcurement As RadioButton
    Friend WithEvents rdbERPStatusMilkUnion As RadioButton
    Friend WithEvents txtDate As common.Controls.MyDateTimePicker
    Friend WithEvents RadLabel3 As common.Controls.MyLabel
    Friend WithEvents RMIALL As RadMenuItem
    Friend WithEvents btnPrint As RadButton
    Friend WithEvents RadMenuItem3 As RadMenuItem
    Friend WithEvents rbtnTranasctionAll As RadioButton
    Friend WithEvents rbtnTransactionPosted As RadioButton
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Label1 As common.Controls.MyLabel
    Friend WithEvents rdbDBTStatus As RadioButton
    Friend WithEvents txtFinYr As common.UserControls.txtFinder
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
End Class

