<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmDBTStatusAndLastDPTStatus
    'Inherits System.Windows.Forms.Form
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
        Dim TableViewDefinition16 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Dim TableViewDefinition15 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Dim TableViewDefinition14 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Dim TableViewDefinition13 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.rbtnTranasctionAll = New System.Windows.Forms.RadioButton()
        Me.rbtnTransactionPosted = New System.Windows.Forms.RadioButton()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.txtFinYr = New common.UserControls.txtFinder()
        Me.rdbDBTStatus = New System.Windows.Forms.RadioButton()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem2 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem4 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RMIALL = New Telerik.WinControls.UI.RadMenuItem()
        Me.gv1 = New common.UserControls.MyRadGridView()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.btnExport = New Telerik.WinControls.UI.RadSplitButton()
        Me.rmiExcel = New Telerik.WinControls.UI.RadMenuItem()
        Me.rmiPDF = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadButton4 = New Telerik.WinControls.UI.RadButton()
        Me.RadButton3 = New Telerik.WinControls.UI.RadButton()
        Me.RadButton1 = New Telerik.WinControls.UI.RadButton()
        Me.RadButton2 = New Telerik.WinControls.UI.RadButton()
        Me.btnreset = New Telerik.WinControls.UI.RadButton()
        Me.btnReport = New Telerik.WinControls.UI.RadButton()
        Me.rdbLastDBTStatus = New System.Windows.Forms.RadioButton()
        Me.gv = New common.UserControls.MyRadGridView()
        Me.MyRadGridView1 = New common.UserControls.MyRadGridView()
        Me.MyRadGridView2 = New common.UserControls.MyRadGridView()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gv1.SuspendLayout()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnExport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnreset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyRadGridView1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gv.SuspendLayout()
        CType(Me.MyRadGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyRadGridView1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MyRadGridView1.SuspendLayout()
        CType(Me.MyRadGridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyRadGridView2.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.SplitContainer2)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnExport)
        Me.SplitContainer1.Panel2.Controls.Add(Me.RadButton4)
        Me.SplitContainer1.Panel2.Controls.Add(Me.RadButton3)
        Me.SplitContainer1.Panel2.Controls.Add(Me.RadButton1)
        Me.SplitContainer1.Panel2.Controls.Add(Me.RadButton2)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnreset)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnReport)
        Me.SplitContainer1.Size = New System.Drawing.Size(940, 471)
        Me.SplitContainer1.SplitterDistance = 435
        Me.SplitContainer1.TabIndex = 0
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
        Me.SplitContainer2.Panel1.Controls.Add(Me.rdbLastDBTStatus)
        Me.SplitContainer2.Panel1.Controls.Add(Me.Label2)
        Me.SplitContainer2.Panel1.Controls.Add(Me.GroupBox1)
        Me.SplitContainer2.Panel1.Controls.Add(Me.MyLabel1)
        Me.SplitContainer2.Panel1.Controls.Add(Me.txtFinYr)
        Me.SplitContainer2.Panel1.Controls.Add(Me.rdbDBTStatus)
        Me.SplitContainer2.Panel1.Controls.Add(Me.RadMenu1)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.gv1)
        Me.SplitContainer2.Size = New System.Drawing.Size(940, 435)
        Me.SplitContainer2.SplitterDistance = 102
        Me.SplitContainer2.TabIndex = 0
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Verdana", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(373, 84)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(125, 18)
        Me.Label2.TabIndex = 325
        Me.Label2.Text = "Report Name"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.rbtnTranasctionAll)
        Me.GroupBox1.Controls.Add(Me.rbtnTransactionPosted)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 49)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(916, 32)
        Me.GroupBox1.TabIndex = 324
        Me.GroupBox1.TabStop = False
        '
        'rbtnTranasctionAll
        '
        Me.rbtnTranasctionAll.AutoSize = True
        Me.rbtnTranasctionAll.Location = New System.Drawing.Point(170, 11)
        Me.rbtnTranasctionAll.Name = "rbtnTranasctionAll"
        Me.rbtnTranasctionAll.Size = New System.Drawing.Size(100, 17)
        Me.rbtnTranasctionAll.TabIndex = 12
        Me.rbtnTranasctionAll.Text = "All Transaction"
        Me.rbtnTranasctionAll.UseVisualStyleBackColor = True
        '
        'rbtnTransactionPosted
        '
        Me.rbtnTransactionPosted.AutoSize = True
        Me.rbtnTransactionPosted.Checked = True
        Me.rbtnTransactionPosted.Location = New System.Drawing.Point(6, 12)
        Me.rbtnTransactionPosted.Name = "rbtnTransactionPosted"
        Me.rbtnTransactionPosted.Size = New System.Drawing.Size(122, 17)
        Me.rbtnTransactionPosted.TabIndex = 13
        Me.rbtnTransactionPosted.TabStop = True
        Me.rbtnTransactionPosted.Text = "Posted Transaction"
        Me.rbtnTransactionPosted.UseVisualStyleBackColor = True
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Location = New System.Drawing.Point(94, 26)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(75, 18)
        Me.MyLabel1.TabIndex = 321
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
        Me.txtFinYr.Location = New System.Drawing.Point(174, 25)
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
        Me.txtFinYr.TabIndex = 320
        Me.txtFinYr.Value = ""
        '
        'rdbDBTStatus
        '
        Me.rdbDBTStatus.AutoSize = True
        Me.rdbDBTStatus.Location = New System.Drawing.Point(18, 26)
        Me.rdbDBTStatus.Name = "rdbDBTStatus"
        Me.rdbDBTStatus.Size = New System.Drawing.Size(80, 17)
        Me.rdbDBTStatus.TabIndex = 319
        Me.rdbDBTStatus.TabStop = True
        Me.rdbDBTStatus.Text = "DBT Status"
        Me.rdbDBTStatus.UseVisualStyleBackColor = True
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem1})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Size = New System.Drawing.Size(940, 20)
        Me.RadMenu1.TabIndex = 4
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RadMenuItem2, Me.RadMenuItem4, Me.RMIALL})
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
        'RMIALL
        '
        Me.RMIALL.Name = "RMIALL"
        Me.RMIALL.Text = "ALL"
        Me.RMIALL.UseCompatibleTextRendering = False
        '
        'gv1
        '
        Me.gv1.Controls.Add(Me.gv)
        Me.gv1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv1.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.gv1.MasterTemplate.AllowAddNewRow = False
        Me.gv1.MasterTemplate.EnableFiltering = True
        Me.gv1.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gv1.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv1.MasterTemplate.ViewDefinition = TableViewDefinition16
        Me.gv1.MyStopExport = False
        Me.gv1.Name = "gv1"
        Me.gv1.ShowHeaderCellButtons = True
        Me.gv1.Size = New System.Drawing.Size(940, 329)
        Me.gv1.TabIndex = 1
        Me.gv1.TabStop = False
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Location = New System.Drawing.Point(865, 9)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(70, 19)
        Me.btnclose.TabIndex = 85
        Me.btnclose.Text = "Close"
        '
        'btnExport
        '
        Me.btnExport.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnExport.Items.AddRange(New Telerik.WinControls.RadItem() {Me.rmiExcel, Me.rmiPDF})
        Me.btnExport.Location = New System.Drawing.Point(164, 9)
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Size = New System.Drawing.Size(70, 19)
        Me.btnExport.TabIndex = 84
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
        'RadButton4
        '
        Me.RadButton4.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.RadButton4.Location = New System.Drawing.Point(88, 9)
        Me.RadButton4.Name = "RadButton4"
        Me.RadButton4.Size = New System.Drawing.Size(70, 19)
        Me.RadButton4.TabIndex = 10
        Me.RadButton4.Text = "Reset"
        '
        'RadButton3
        '
        Me.RadButton3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.RadButton3.Location = New System.Drawing.Point(12, 9)
        Me.RadButton3.Name = "RadButton3"
        Me.RadButton3.Size = New System.Drawing.Size(70, 19)
        Me.RadButton3.TabIndex = 9
        Me.RadButton3.Text = ">>>"
        '
        'RadButton1
        '
        Me.RadButton1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.RadButton1.Location = New System.Drawing.Point(88, -405)
        Me.RadButton1.Name = "RadButton1"
        Me.RadButton1.Size = New System.Drawing.Size(70, 19)
        Me.RadButton1.TabIndex = 7
        Me.RadButton1.Text = "Reset"
        '
        'RadButton2
        '
        Me.RadButton2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.RadButton2.Location = New System.Drawing.Point(12, -405)
        Me.RadButton2.Name = "RadButton2"
        Me.RadButton2.Size = New System.Drawing.Size(70, 19)
        Me.RadButton2.TabIndex = 8
        Me.RadButton2.Text = ">>>"
        '
        'btnreset
        '
        Me.btnreset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnreset.Location = New System.Drawing.Point(84, -801)
        Me.btnreset.Name = "btnreset"
        Me.btnreset.Size = New System.Drawing.Size(70, 19)
        Me.btnreset.TabIndex = 5
        Me.btnreset.Text = "Reset"
        '
        'btnReport
        '
        Me.btnReport.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnReport.Location = New System.Drawing.Point(8, -801)
        Me.btnReport.Name = "btnReport"
        Me.btnReport.Size = New System.Drawing.Size(70, 19)
        Me.btnReport.TabIndex = 6
        Me.btnReport.Text = ">>>"
        '
        'rdbLastDBTStatus
        '
        Me.rdbLastDBTStatus.AutoSize = True
        Me.rdbLastDBTStatus.Location = New System.Drawing.Point(299, 27)
        Me.rdbLastDBTStatus.Name = "rdbLastDBTStatus"
        Me.rdbLastDBTStatus.Size = New System.Drawing.Size(103, 17)
        Me.rdbLastDBTStatus.TabIndex = 326
        Me.rdbLastDBTStatus.TabStop = True
        Me.rdbLastDBTStatus.Text = "Last DBT Status"
        Me.rdbLastDBTStatus.UseVisualStyleBackColor = True
        '
        'gv
        '
        Me.gv.Controls.Add(Me.MyRadGridView1)
        Me.gv.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gv.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.MyRadGridView1.MasterTemplate.AllowAddNewRow = False
        Me.MyRadGridView1.MasterTemplate.EnableFiltering = True
        Me.MyRadGridView1.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.MyRadGridView1.MasterTemplate.ShowHeaderCellButtons = True
        Me.MyRadGridView1.MasterTemplate.ViewDefinition = TableViewDefinition15
        Me.gv.MyStopExport = False
        Me.gv.Name = "gv"
        Me.gv.ShowHeaderCellButtons = True
        Me.gv.Size = New System.Drawing.Size(940, 329)
        Me.gv.TabIndex = 1
        Me.gv.TabStop = False
        '
        'MyRadGridView1
        '
        Me.MyRadGridView1.Controls.Add(Me.MyRadGridView2)
        Me.MyRadGridView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MyRadGridView1.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.MyRadGridView1.MasterTemplate.AllowAddNewRow = False
        Me.MyRadGridView1.MasterTemplate.EnableFiltering = True
        Me.MyRadGridView1.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.MyRadGridView1.MasterTemplate.ShowHeaderCellButtons = True
        Me.MyRadGridView1.MasterTemplate.ViewDefinition = TableViewDefinition14
        Me.MyRadGridView1.MyStopExport = False
        Me.MyRadGridView1.Name = "MyRadGridView1"
        Me.MyRadGridView1.ShowHeaderCellButtons = True
        Me.MyRadGridView1.Size = New System.Drawing.Size(940, 329)
        Me.MyRadGridView1.TabIndex = 1
        Me.MyRadGridView1.TabStop = False
        '
        'MyRadGridView2
        '
        Me.MyRadGridView2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MyRadGridView2.Location = New System.Drawing.Point(0, 0)
        '
        '
        '
        Me.MyRadGridView2.MasterTemplate.AllowAddNewRow = False
        Me.MyRadGridView2.MasterTemplate.EnableFiltering = True
        Me.MyRadGridView2.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.MyRadGridView2.MasterTemplate.ShowHeaderCellButtons = True
        Me.MyRadGridView2.MasterTemplate.ViewDefinition = TableViewDefinition13
        Me.MyRadGridView2.MyStopExport = False
        Me.MyRadGridView2.Name = "MyRadGridView2"
        Me.MyRadGridView2.ShowHeaderCellButtons = True
        Me.MyRadGridView2.Size = New System.Drawing.Size(940, 329)
        Me.MyRadGridView2.TabIndex = 1
        Me.MyRadGridView2.TabStop = False
        '
        'frmDBTStatusAndLastDPTStatus
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(940, 471)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "frmDBTStatusAndLastDPTStatus"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "frmDBTStatusAndLastDPTStatus"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.PerformLayout()
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gv1.ResumeLayout(False)
        Me.gv1.PerformLayout()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnExport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnreset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyRadGridView1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gv.ResumeLayout(False)
        Me.gv.PerformLayout()
        CType(Me.MyRadGridView1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyRadGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MyRadGridView1.ResumeLayout(False)
        Me.MyRadGridView1.PerformLayout()
        CType(Me.MyRadGridView2.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyRadGridView2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents btnreset As RadButton
    Friend WithEvents btnReport As RadButton
    Friend WithEvents RadButton1 As RadButton
    Friend WithEvents RadButton2 As RadButton
    Friend WithEvents RadButton3 As RadButton
    Friend WithEvents RadButton4 As RadButton
    Friend WithEvents btnExport As RadSplitButton
    Friend WithEvents rmiExcel As RadMenuItem
    Friend WithEvents rmiPDF As RadMenuItem
    Friend WithEvents btnclose As RadButton
    Friend WithEvents SplitContainer2 As SplitContainer
    Friend WithEvents RadMenu1 As RadMenu
    Friend WithEvents RadMenuItem1 As RadMenuItem
    Friend WithEvents RadMenuItem2 As RadMenuItem
    Friend WithEvents RadMenuItem4 As RadMenuItem
    Friend WithEvents RMIALL As RadMenuItem
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents txtFinYr As common.UserControls.txtFinder
    Friend WithEvents rdbDBTStatus As RadioButton
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents rbtnTranasctionAll As RadioButton
    Friend WithEvents rbtnTransactionPosted As RadioButton
    Friend WithEvents Label2 As Label
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents rdbLastDBTStatus As RadioButton
    Friend WithEvents gv As common.UserControls.MyRadGridView
    Friend WithEvents MyRadGridView1 As common.UserControls.MyRadGridView
    Friend WithEvents MyRadGridView2 As common.UserControls.MyRadGridView
End Class
