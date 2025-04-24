Imports XpertERPEngineFine
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmSaveCrystalReportAction
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
        Me.RadLabel4 = New common.Controls.MyLabel()
        Me.btnSave = New Telerik.WinControls.UI.RadButton()
        Me.btnClose = New Telerik.WinControls.UI.RadButton()
        Me.Splitter1 = New System.Windows.Forms.Splitter()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.MyLabel2 = New common.Controls.MyLabel()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.rbtnPdf = New System.Windows.Forms.RadioButton()
        Me.rbtnView = New System.Windows.Forms.RadioButton()
        Me.lblFormId = New common.Controls.MyLabel()
        Me.lblCrystalReportName = New common.Controls.MyLabel()
        Me.MyLabel1 = New common.Controls.MyLabel()
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.lblFormId, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCrystalReportName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadLabel4
        '
        Me.RadLabel4.FieldName = Nothing
        Me.RadLabel4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadLabel4.Location = New System.Drawing.Point(11, 12)
        Me.RadLabel4.Name = "RadLabel4"
        Me.RadLabel4.Size = New System.Drawing.Size(47, 16)
        Me.RadLabel4.TabIndex = 27
        Me.RadLabel4.Text = "Form ID"
        '
        'btnSave
        '
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(103, 8)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(83, 21)
        Me.btnSave.TabIndex = 28
        Me.btnSave.Text = "F5 : Save"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(189, 8)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(83, 21)
        Me.btnClose.TabIndex = 29
        Me.btnClose.Text = "Esc : Cancel"
        '
        'Splitter1
        '
        Me.Splitter1.Location = New System.Drawing.Point(0, 0)
        Me.Splitter1.Name = "Splitter1"
        Me.Splitter1.Size = New System.Drawing.Size(3, 133)
        Me.Splitter1.TabIndex = 31
        Me.Splitter1.TabStop = False
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer1.IsSplitterFixed = True
        Me.SplitContainer1.Location = New System.Drawing.Point(3, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadGroupBox1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblFormId)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblCrystalReportName)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadLabel4)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Size = New System.Drawing.Size(379, 133)
        Me.SplitContainer1.SplitterDistance = 93
        Me.SplitContainer1.TabIndex = 32
        '
        'MyLabel2
        '
        Me.MyLabel2.FieldName = Nothing
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(11, 59)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(66, 16)
        Me.MyLabel2.TabIndex = 1427
        Me.MyLabel2.Text = "Action Type"
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.rbtnPdf)
        Me.RadGroupBox1.Controls.Add(Me.rbtnView)
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(136, 53)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Size = New System.Drawing.Size(230, 30)
        Me.RadGroupBox1.TabIndex = 1426
        '
        'rbtnPdf
        '
        Me.rbtnPdf.AutoSize = True
        Me.rbtnPdf.Location = New System.Drawing.Point(136, 6)
        Me.rbtnPdf.Name = "rbtnPdf"
        Me.rbtnPdf.Size = New System.Drawing.Size(45, 17)
        Me.rbtnPdf.TabIndex = 1
        Me.rbtnPdf.TabStop = True
        Me.rbtnPdf.Text = "PDF"
        Me.rbtnPdf.UseVisualStyleBackColor = True
        '
        'rbtnView
        '
        Me.rbtnView.AutoSize = True
        Me.rbtnView.Checked = True
        Me.rbtnView.Location = New System.Drawing.Point(38, 6)
        Me.rbtnView.Name = "rbtnView"
        Me.rbtnView.Size = New System.Drawing.Size(50, 17)
        Me.rbtnView.TabIndex = 0
        Me.rbtnView.TabStop = True
        Me.rbtnView.Text = "View"
        Me.rbtnView.UseVisualStyleBackColor = True
        '
        'lblFormId
        '
        Me.lblFormId.AutoSize = False
        Me.lblFormId.BorderVisible = True
        Me.lblFormId.FieldName = Nothing
        Me.lblFormId.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFormId.Location = New System.Drawing.Point(137, 12)
        Me.lblFormId.Name = "lblFormId"
        Me.lblFormId.Size = New System.Drawing.Size(230, 16)
        Me.lblFormId.TabIndex = 1425
        '
        'lblCrystalReportName
        '
        Me.lblCrystalReportName.AutoSize = False
        Me.lblCrystalReportName.BorderVisible = True
        Me.lblCrystalReportName.FieldName = Nothing
        Me.lblCrystalReportName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCrystalReportName.Location = New System.Drawing.Point(137, 31)
        Me.lblCrystalReportName.Name = "lblCrystalReportName"
        Me.lblCrystalReportName.Size = New System.Drawing.Size(230, 18)
        Me.lblCrystalReportName.TabIndex = 1424
        '
        'MyLabel1
        '
        Me.MyLabel1.FieldName = Nothing
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(10, 33)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(112, 16)
        Me.MyLabel1.TabIndex = 31
        Me.MyLabel1.Text = "Crystal Report Name"
        '
        'frmSaveCrystalReportAction
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(382, 133)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.Splitter1)
        Me.MaximumSize = New System.Drawing.Size(390, 163)
        Me.MinimumSize = New System.Drawing.Size(390, 163)
        Me.Name = "frmSaveCrystalReportAction"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.RootElement.MaxSize = New System.Drawing.Size(390, 165)
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Save Crystal Report Action Type"
        CType(Me.RadLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.lblFormId, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCrystalReportName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents RadLabel4 As common.Controls.MyLabel
    Friend WithEvents btnSave As RadButton
    Friend WithEvents btnClose As RadButton
    Friend WithEvents Splitter1 As Splitter
    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents MyLabel1 As Controls.MyLabel
    Friend WithEvents lblFormId As Controls.MyLabel
    Friend WithEvents lblCrystalReportName As Controls.MyLabel
    Friend WithEvents RadGroupBox1 As RadGroupBox
    Friend WithEvents rbtnPdf As RadioButton
    Friend WithEvents rbtnView As RadioButton
    Friend WithEvents MyLabel2 As Controls.MyLabel
End Class
