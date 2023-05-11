Imports XpertERPEngine

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmCustomerGroupReport
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
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.lblFromCustomerGroup = New common.Controls.MyLabel()
        Me.lblToCustomerGroup = New common.Controls.MyLabel()
        Me.btnPrint = New Telerik.WinControls.UI.RadButton()
        Me.btnReset = New Telerik.WinControls.UI.RadButton()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.fndToCustomerGroup = New common.UserControls.txtFinder()
        Me.lblToCustomerGroup1 = New common.Controls.MyTextBox()
        Me.fndFromCustomerGroup = New common.UserControls.txtFinder()
        Me.lblFromCustomerGroup1 = New common.Controls.MyTextBox()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.lblFromCustomerGroup, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblToCustomerGroup, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.lblToCustomerGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblFromCustomerGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.RadGroupBox2)
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(13, 11)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(563, 83)
        Me.RadGroupBox1.TabIndex = 1
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.Controls.Add(Me.fndFromCustomerGroup)
        Me.RadGroupBox2.Controls.Add(Me.lblFromCustomerGroup1)
        Me.RadGroupBox2.Controls.Add(Me.fndToCustomerGroup)
        Me.RadGroupBox2.Controls.Add(Me.lblToCustomerGroup1)
        Me.RadGroupBox2.Controls.Add(Me.lblFromCustomerGroup)
        Me.RadGroupBox2.Controls.Add(Me.lblToCustomerGroup)
        Me.RadGroupBox2.HeaderText = ""
        Me.RadGroupBox2.Location = New System.Drawing.Point(13, 11)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(538, 59)
        Me.RadGroupBox2.TabIndex = 46
        '
        'lblFromCustomerGroup
        '
        Me.lblFromCustomerGroup.FieldName = Nothing
        Me.lblFromCustomerGroup.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFromCustomerGroup.Location = New System.Drawing.Point(13, 10)
        Me.lblFromCustomerGroup.Name = "lblFromCustomerGroup"
        Me.lblFromCustomerGroup.Size = New System.Drawing.Size(119, 16)
        Me.lblFromCustomerGroup.TabIndex = 0
        Me.lblFromCustomerGroup.Text = "From Customer Group"
        '
        'lblToCustomerGroup
        '
        Me.lblToCustomerGroup.FieldName = Nothing
        Me.lblToCustomerGroup.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblToCustomerGroup.Location = New System.Drawing.Point(13, 32)
        Me.lblToCustomerGroup.Name = "lblToCustomerGroup"
        Me.lblToCustomerGroup.Size = New System.Drawing.Size(106, 16)
        Me.lblToCustomerGroup.TabIndex = 1
        Me.lblToCustomerGroup.Text = "To Customer Group"
        '
        'btnPrint
        '
        Me.btnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPrint.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrint.Location = New System.Drawing.Point(87, 8)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(68, 18)
        Me.btnPrint.TabIndex = 45
        Me.btnPrint.Text = "Print"
        '
        'btnReset
        '
        Me.btnReset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnReset.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReset.Location = New System.Drawing.Point(13, 8)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(68, 18)
        Me.btnReset.TabIndex = 43
        Me.btnReset.Text = "Reset"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(508, 8)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(68, 18)
        Me.btnClose.TabIndex = 44
        Me.btnClose.Text = "Close"
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadGroupBox1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPrint)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnReset)
        Me.SplitContainer1.Size = New System.Drawing.Size(587, 141)
        Me.SplitContainer1.SplitterDistance = 97
        Me.SplitContainer1.TabIndex = 2
        '
        'fndToCustomerGroup
        '
        Me.fndToCustomerGroup.CalculationExpression = Nothing
        Me.fndToCustomerGroup.FieldCode = Nothing
        Me.fndToCustomerGroup.FieldDesc = Nothing
        Me.fndToCustomerGroup.FieldMaxLength = 0
        Me.fndToCustomerGroup.FieldName = Nothing
        Me.fndToCustomerGroup.isCalculatedField = False
        Me.fndToCustomerGroup.IsSourceFromTable = False
        Me.fndToCustomerGroup.IsSourceFromValueList = False
        Me.fndToCustomerGroup.IsUnique = False
        Me.fndToCustomerGroup.Location = New System.Drawing.Point(147, 32)
        Me.fndToCustomerGroup.MendatroryField = True
        Me.fndToCustomerGroup.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndToCustomerGroup.MyLinkLable1 = Nothing
        Me.fndToCustomerGroup.MyLinkLable2 = Nothing
        Me.fndToCustomerGroup.MyReadOnly = False
        Me.fndToCustomerGroup.MyShowMasterFormButton = False
        Me.fndToCustomerGroup.Name = "fndToCustomerGroup"
        Me.fndToCustomerGroup.ReferenceFieldDesc = Nothing
        Me.fndToCustomerGroup.ReferenceFieldName = Nothing
        Me.fndToCustomerGroup.ReferenceTableName = Nothing
        Me.fndToCustomerGroup.Size = New System.Drawing.Size(112, 19)
        Me.fndToCustomerGroup.TabIndex = 50
        Me.fndToCustomerGroup.Value = ""
        '
        'lblToCustomerGroup1
        '
        Me.lblToCustomerGroup1.CalculationExpression = Nothing
        Me.lblToCustomerGroup1.FieldCode = Nothing
        Me.lblToCustomerGroup1.FieldDesc = Nothing
        Me.lblToCustomerGroup1.FieldMaxLength = 0
        Me.lblToCustomerGroup1.FieldName = Nothing
        Me.lblToCustomerGroup1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblToCustomerGroup1.isCalculatedField = False
        Me.lblToCustomerGroup1.IsSourceFromTable = False
        Me.lblToCustomerGroup1.IsSourceFromValueList = False
        Me.lblToCustomerGroup1.IsUnique = False
        Me.lblToCustomerGroup1.Location = New System.Drawing.Point(265, 32)
        Me.lblToCustomerGroup1.MaxLength = 50
        Me.lblToCustomerGroup1.MendatroryField = True
        Me.lblToCustomerGroup1.MyLinkLable1 = Nothing
        Me.lblToCustomerGroup1.MyLinkLable2 = Nothing
        Me.lblToCustomerGroup1.Name = "lblToCustomerGroup1"
        Me.lblToCustomerGroup1.ReadOnly = True
        Me.lblToCustomerGroup1.ReferenceFieldDesc = Nothing
        Me.lblToCustomerGroup1.ReferenceFieldName = Nothing
        Me.lblToCustomerGroup1.ReferenceTableName = Nothing
        Me.lblToCustomerGroup1.Size = New System.Drawing.Size(261, 18)
        Me.lblToCustomerGroup1.TabIndex = 51
        Me.lblToCustomerGroup1.TabStop = False
        '
        'fndFromCustomerGroup
        '
        Me.fndFromCustomerGroup.CalculationExpression = Nothing
        Me.fndFromCustomerGroup.FieldCode = Nothing
        Me.fndFromCustomerGroup.FieldDesc = Nothing
        Me.fndFromCustomerGroup.FieldMaxLength = 0
        Me.fndFromCustomerGroup.FieldName = Nothing
        Me.fndFromCustomerGroup.isCalculatedField = False
        Me.fndFromCustomerGroup.IsSourceFromTable = False
        Me.fndFromCustomerGroup.IsSourceFromValueList = False
        Me.fndFromCustomerGroup.IsUnique = False
        Me.fndFromCustomerGroup.Location = New System.Drawing.Point(147, 10)
        Me.fndFromCustomerGroup.MendatroryField = True
        Me.fndFromCustomerGroup.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fndFromCustomerGroup.MyLinkLable1 = Nothing
        Me.fndFromCustomerGroup.MyLinkLable2 = Nothing
        Me.fndFromCustomerGroup.MyReadOnly = False
        Me.fndFromCustomerGroup.MyShowMasterFormButton = False
        Me.fndFromCustomerGroup.Name = "fndFromCustomerGroup"
        Me.fndFromCustomerGroup.ReferenceFieldDesc = Nothing
        Me.fndFromCustomerGroup.ReferenceFieldName = Nothing
        Me.fndFromCustomerGroup.ReferenceTableName = Nothing
        Me.fndFromCustomerGroup.Size = New System.Drawing.Size(112, 19)
        Me.fndFromCustomerGroup.TabIndex = 52
        Me.fndFromCustomerGroup.Value = ""
        '
        'lblFromCustomerGroup1
        '
        Me.lblFromCustomerGroup1.CalculationExpression = Nothing
        Me.lblFromCustomerGroup1.FieldCode = Nothing
        Me.lblFromCustomerGroup1.FieldDesc = Nothing
        Me.lblFromCustomerGroup1.FieldMaxLength = 0
        Me.lblFromCustomerGroup1.FieldName = Nothing
        Me.lblFromCustomerGroup1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFromCustomerGroup1.isCalculatedField = False
        Me.lblFromCustomerGroup1.IsSourceFromTable = False
        Me.lblFromCustomerGroup1.IsSourceFromValueList = False
        Me.lblFromCustomerGroup1.IsUnique = False
        Me.lblFromCustomerGroup1.Location = New System.Drawing.Point(265, 10)
        Me.lblFromCustomerGroup1.MaxLength = 50
        Me.lblFromCustomerGroup1.MendatroryField = True
        Me.lblFromCustomerGroup1.MyLinkLable1 = Nothing
        Me.lblFromCustomerGroup1.MyLinkLable2 = Nothing
        Me.lblFromCustomerGroup1.Name = "lblFromCustomerGroup1"
        Me.lblFromCustomerGroup1.ReadOnly = True
        Me.lblFromCustomerGroup1.ReferenceFieldDesc = Nothing
        Me.lblFromCustomerGroup1.ReferenceFieldName = Nothing
        Me.lblFromCustomerGroup1.ReferenceTableName = Nothing
        Me.lblFromCustomerGroup1.Size = New System.Drawing.Size(261, 18)
        Me.lblFromCustomerGroup1.TabIndex = 53
        Me.lblFromCustomerGroup1.TabStop = False
        '
        'FrmCustomerGroupReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(587, 141)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmCustomerGroupReport"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Customer Group Report"
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        Me.RadGroupBox2.PerformLayout()
        CType(Me.lblFromCustomerGroup, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblToCustomerGroup, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.lblToCustomerGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblFromCustomerGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents btnPrint As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblFromCustomerGroup As common.Controls.MyLabel
    Friend WithEvents lblToCustomerGroup As common.Controls.MyLabel
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents fndFromCustomerGroup As common.UserControls.txtFinder
    Friend WithEvents lblFromCustomerGroup1 As common.Controls.MyTextBox
    Friend WithEvents fndToCustomerGroup As common.UserControls.txtFinder
    Friend WithEvents lblToCustomerGroup1 As common.Controls.MyTextBox
End Class

