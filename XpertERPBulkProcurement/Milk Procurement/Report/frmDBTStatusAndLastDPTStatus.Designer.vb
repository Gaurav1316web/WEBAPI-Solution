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
        Dim TableViewDefinition1 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.MyLabel1 = New common.Controls.MyLabel()
        Me.txtFinYr = New common.UserControls.txtFinder()
        Me.gv1 = New common.UserControls.MyRadGridView()
        Me.rdbLastDBTStatus = New System.Windows.Forms.RadioButton()
        Me.rdbDBTStatus = New System.Windows.Forms.RadioButton()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem2 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem4 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RMIALL = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadButton4 = New Telerik.WinControls.UI.RadButton()
        Me.RadButton3 = New Telerik.WinControls.UI.RadButton()
        Me.RadButton1 = New Telerik.WinControls.UI.RadButton()
        Me.RadButton2 = New Telerik.WinControls.UI.RadButton()
        Me.btnreset = New Telerik.WinControls.UI.RadButton()
        Me.btnReport = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadButton2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnreset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReport, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtFinYr)
        Me.SplitContainer1.Panel1.Controls.Add(Me.gv1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.rdbLastDBTStatus)
        Me.SplitContainer1.Panel1.Controls.Add(Me.rdbDBTStatus)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadMenu1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.RadButton4)
        Me.SplitContainer1.Panel2.Controls.Add(Me.RadButton3)
        Me.SplitContainer1.Panel2.Controls.Add(Me.RadButton1)
        Me.SplitContainer1.Panel2.Controls.Add(Me.RadButton2)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnreset)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnReport)
        Me.SplitContainer1.Size = New System.Drawing.Size(800, 450)
        Me.SplitContainer1.SplitterDistance = 407
        Me.SplitContainer1.TabIndex = 0
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Location = New System.Drawing.Point(261, 27)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(75, 18)
        Me.MyLabel1.TabIndex = 323
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
        Me.txtFinYr.Location = New System.Drawing.Point(342, 26)
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
        Me.txtFinYr.TabIndex = 322
        Me.txtFinYr.Value = ""
        '
        'gv1
        '
        Me.gv1.Location = New System.Drawing.Point(12, 67)
        '
        '
        '
        Me.gv1.MasterTemplate.AllowAddNewRow = False
        Me.gv1.MasterTemplate.EnableFiltering = True
        Me.gv1.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect
        Me.gv1.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv1.MasterTemplate.ViewDefinition = TableViewDefinition1
        Me.gv1.MyStopExport = False
        Me.gv1.Name = "gv1"
        Me.gv1.ShowHeaderCellButtons = True
        Me.gv1.Size = New System.Drawing.Size(767, 324)
        Me.gv1.TabIndex = 321
        Me.gv1.TabStop = False
        '
        'rdbLastDBTStatus
        '
        Me.rdbLastDBTStatus.AutoSize = True
        Me.rdbLastDBTStatus.Location = New System.Drawing.Point(12, 26)
        Me.rdbLastDBTStatus.Name = "rdbLastDBTStatus"
        Me.rdbLastDBTStatus.Size = New System.Drawing.Size(103, 17)
        Me.rdbLastDBTStatus.TabIndex = 320
        Me.rdbLastDBTStatus.TabStop = True
        Me.rdbLastDBTStatus.Text = "Last DBT Status"
        Me.rdbLastDBTStatus.UseVisualStyleBackColor = True
        '
        'rdbDBTStatus
        '
        Me.rdbDBTStatus.AutoSize = True
        Me.rdbDBTStatus.Location = New System.Drawing.Point(121, 26)
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
        Me.RadMenu1.Size = New System.Drawing.Size(800, 20)
        Me.RadMenu1.TabIndex = 76
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
        'RadButton4
        '
        Me.RadButton4.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.RadButton4.Location = New System.Drawing.Point(88, 8)
        Me.RadButton4.Name = "RadButton4"
        Me.RadButton4.Size = New System.Drawing.Size(70, 19)
        Me.RadButton4.TabIndex = 10
        Me.RadButton4.Text = "Reset"
        '
        'RadButton3
        '
        Me.RadButton3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.RadButton3.Location = New System.Drawing.Point(12, 8)
        Me.RadButton3.Name = "RadButton3"
        Me.RadButton3.Size = New System.Drawing.Size(70, 19)
        Me.RadButton3.TabIndex = 9
        Me.RadButton3.Text = ">>>"
        '
        'RadButton1
        '
        Me.RadButton1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.RadButton1.Location = New System.Drawing.Point(88, -403)
        Me.RadButton1.Name = "RadButton1"
        Me.RadButton1.Size = New System.Drawing.Size(70, 19)
        Me.RadButton1.TabIndex = 7
        Me.RadButton1.Text = "Reset"
        '
        'RadButton2
        '
        Me.RadButton2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.RadButton2.Location = New System.Drawing.Point(12, -403)
        Me.RadButton2.Name = "RadButton2"
        Me.RadButton2.Size = New System.Drawing.Size(70, 19)
        Me.RadButton2.TabIndex = 8
        Me.RadButton2.Text = ">>>"
        '
        'btnreset
        '
        Me.btnreset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnreset.Location = New System.Drawing.Point(84, -799)
        Me.btnreset.Name = "btnreset"
        Me.btnreset.Size = New System.Drawing.Size(70, 19)
        Me.btnreset.TabIndex = 5
        Me.btnreset.Text = "Reset"
        '
        'btnReport
        '
        Me.btnReport.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnReport.Location = New System.Drawing.Point(8, -799)
        Me.btnReport.Name = "btnReport"
        Me.btnReport.Size = New System.Drawing.Size(70, 19)
        Me.btnReport.TabIndex = 6
        Me.btnReport.Text = ">>>"
        '
        'frmDBTStatusAndLastDPTStatus
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "frmDBTStatusAndLastDPTStatus"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "frmDBTStatusAndLastDPTStatus"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadButton2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnreset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents btnreset As RadButton
    Friend WithEvents btnReport As RadButton
    Friend WithEvents RadButton1 As RadButton
    Friend WithEvents RadButton2 As RadButton
    Friend WithEvents RadMenu1 As RadMenu
    Friend WithEvents RadMenuItem1 As RadMenuItem
    Friend WithEvents RadMenuItem2 As RadMenuItem
    Friend WithEvents RadMenuItem4 As RadMenuItem
    Friend WithEvents RMIALL As RadMenuItem
    Friend WithEvents rdbLastDBTStatus As RadioButton
    Friend WithEvents rdbDBTStatus As RadioButton
    Friend WithEvents RadButton3 As RadButton
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents RadButton4 As RadButton
    Friend WithEvents txtFinYr As common.UserControls.txtFinder
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
End Class
