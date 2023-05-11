<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmRequisitionApproval
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
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox
        Me.rbtnOff = New Telerik.WinControls.UI.RadRadioButton
        Me.rbtnLBL5 = New Telerik.WinControls.UI.RadRadioButton
        Me.rbtnLBL4 = New Telerik.WinControls.UI.RadRadioButton
        Me.rbtnLBL3 = New Telerik.WinControls.UI.RadRadioButton
        Me.rbtnLBL2 = New Telerik.WinControls.UI.RadRadioButton
        Me.rbtnLBL1 = New Telerik.WinControls.UI.RadRadioButton
        Me.txtLevel3 = New common.MyNumBox
        Me.MyLabel2 = New common.Controls.MyLabel
        Me.txtLevel2 = New common.MyNumBox
        Me.MyLabel1 = New common.Controls.MyLabel
        Me.txtLevel1 = New common.MyNumBox
        Me.lblAmount = New common.Controls.MyLabel
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox
        Me.btnReset = New Telerik.WinControls.UI.RadButton
        Me.btnSave = New Telerik.WinControls.UI.RadButton
        Me.btnClose = New Telerik.WinControls.UI.RadButton
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.rbtnOff, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnLBL5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnLBL4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnLBL3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnLBL2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rbtnLBL1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtLevel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtLevel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtLevel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblAmount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.IsSplitterFixed = True
        Me.SplitContainer1.Location = New System.Drawing.Point(2, 5)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadGroupBox1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.RadGroupBox2)
        Me.SplitContainer1.Size = New System.Drawing.Size(464, 134)
        Me.SplitContainer1.SplitterDistance = 81
        Me.SplitContainer1.TabIndex = 0
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.BackColor = System.Drawing.Color.Transparent
        Me.RadGroupBox1.Controls.Add(Me.rbtnOff)
        Me.RadGroupBox1.Controls.Add(Me.rbtnLBL5)
        Me.RadGroupBox1.Controls.Add(Me.rbtnLBL4)
        Me.RadGroupBox1.Controls.Add(Me.rbtnLBL3)
        Me.RadGroupBox1.Controls.Add(Me.rbtnLBL2)
        Me.RadGroupBox1.Controls.Add(Me.rbtnLBL1)
        Me.RadGroupBox1.Controls.Add(Me.txtLevel3)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel2)
        Me.RadGroupBox1.Controls.Add(Me.txtLevel2)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel1)
        Me.RadGroupBox1.Controls.Add(Me.txtLevel1)
        Me.RadGroupBox1.Controls.Add(Me.lblAmount)
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(5, 6)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(452, 76)
        Me.RadGroupBox1.TabIndex = 0
        '
        'rbtnOff
        '
        Me.rbtnOff.Location = New System.Drawing.Point(268, 7)
        Me.rbtnOff.Name = "rbtnOff"
        Me.rbtnOff.Size = New System.Drawing.Size(36, 18)
        Me.rbtnOff.TabIndex = 1
        Me.rbtnOff.Text = "Off"
        '
        'rbtnLBL5
        '
        Me.rbtnLBL5.Location = New System.Drawing.Point(374, 51)
        Me.rbtnLBL5.Name = "rbtnLBL5"
        Me.rbtnLBL5.Size = New System.Drawing.Size(55, 18)
        Me.rbtnLBL5.TabIndex = 8
        Me.rbtnLBL5.Text = "Level 3"
        '
        'rbtnLBL4
        '
        Me.rbtnLBL4.Location = New System.Drawing.Point(374, 29)
        Me.rbtnLBL4.Name = "rbtnLBL4"
        Me.rbtnLBL4.Size = New System.Drawing.Size(55, 18)
        Me.rbtnLBL4.TabIndex = 5
        Me.rbtnLBL4.Text = "Level 2"
        '
        'rbtnLBL3
        '
        Me.rbtnLBL3.Location = New System.Drawing.Point(374, 7)
        Me.rbtnLBL3.Name = "rbtnLBL3"
        Me.rbtnLBL3.Size = New System.Drawing.Size(55, 18)
        Me.rbtnLBL3.TabIndex = 2
        Me.rbtnLBL3.Text = "Level 1"
        '
        'rbtnLBL2
        '
        Me.rbtnLBL2.Location = New System.Drawing.Point(268, 51)
        Me.rbtnLBL2.Name = "rbtnLBL2"
        Me.rbtnLBL2.Size = New System.Drawing.Size(58, 18)
        Me.rbtnLBL2.TabIndex = 7
        Me.rbtnLBL2.Text = "Finance"
        '
        'rbtnLBL1
        '
        Me.rbtnLBL1.Location = New System.Drawing.Point(268, 28)
        Me.rbtnLBL1.Name = "rbtnLBL1"
        Me.rbtnLBL1.Size = New System.Drawing.Size(71, 18)
        Me.rbtnLBL1.TabIndex = 4
        Me.rbtnLBL1.Text = "Budgetary"
        '
        'txtLevel3
        '
        Me.txtLevel3.BackColor = System.Drawing.Color.White
        Me.txtLevel3.DecimalPlaces = 2
        Me.txtLevel3.Location = New System.Drawing.Point(88, 51)
        Me.txtLevel3.MendatroryField = False
        Me.txtLevel3.MyLinkLable1 = Nothing
        Me.txtLevel3.MyLinkLable2 = Nothing
        Me.txtLevel3.Name = "txtLevel3"
        Me.txtLevel3.Size = New System.Drawing.Size(154, 20)
        Me.txtLevel3.TabIndex = 6
        Me.txtLevel3.Text = "0"
        Me.txtLevel3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtLevel3.Value = 0
        '
        'MyLabel2
        '
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(9, 53)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(52, 16)
        Me.MyLabel2.TabIndex = 9
        Me.MyLabel2.Text = "Level 3 >"
        '
        'txtLevel2
        '
        Me.txtLevel2.BackColor = System.Drawing.Color.White
        Me.txtLevel2.DecimalPlaces = 2
        Me.txtLevel2.Location = New System.Drawing.Point(88, 29)
        Me.txtLevel2.MendatroryField = False
        Me.txtLevel2.MyLinkLable1 = Nothing
        Me.txtLevel2.MyLinkLable2 = Nothing
        Me.txtLevel2.Name = "txtLevel2"
        Me.txtLevel2.Size = New System.Drawing.Size(154, 20)
        Me.txtLevel2.TabIndex = 3
        Me.txtLevel2.Text = "0"
        Me.txtLevel2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtLevel2.Value = 0
        '
        'MyLabel1
        '
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(9, 31)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(59, 16)
        Me.MyLabel1.TabIndex = 10
        Me.MyLabel1.Text = "Level 2 <="
        '
        'txtLevel1
        '
        Me.txtLevel1.BackColor = System.Drawing.Color.White
        Me.txtLevel1.DecimalPlaces = 2
        Me.txtLevel1.Location = New System.Drawing.Point(88, 7)
        Me.txtLevel1.MendatroryField = False
        Me.txtLevel1.MyLinkLable1 = Nothing
        Me.txtLevel1.MyLinkLable2 = Nothing
        Me.txtLevel1.Name = "txtLevel1"
        Me.txtLevel1.Size = New System.Drawing.Size(154, 20)
        Me.txtLevel1.TabIndex = 0
        Me.txtLevel1.Text = "0"
        Me.txtLevel1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtLevel1.Value = 0
        '
        'lblAmount
        '
        Me.lblAmount.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAmount.Location = New System.Drawing.Point(9, 9)
        Me.lblAmount.Name = "lblAmount"
        Me.lblAmount.Size = New System.Drawing.Size(62, 16)
        Me.lblAmount.TabIndex = 11
        Me.lblAmount.Text = "Level 1  <="
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.BackColor = System.Drawing.Color.Transparent
        Me.RadGroupBox2.Controls.Add(Me.btnReset)
        Me.RadGroupBox2.Controls.Add(Me.btnSave)
        Me.RadGroupBox2.Controls.Add(Me.btnClose)
        Me.RadGroupBox2.HeaderText = ""
        Me.RadGroupBox2.Location = New System.Drawing.Point(5, 3)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox2.Size = New System.Drawing.Size(452, 26)
        Me.RadGroupBox2.TabIndex = 0
        '
        'btnReset
        '
        Me.btnReset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnReset.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReset.Location = New System.Drawing.Point(81, 4)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(68, 18)
        Me.btnReset.TabIndex = 1
        Me.btnReset.Text = "Reset"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(5, 4)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(68, 18)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "Save"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(378, 4)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(68, 18)
        Me.btnClose.TabIndex = 2
        Me.btnClose.Text = "Close"
        '
        'FrmRequisitionApproval
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(468, 140)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmRequisitionApproval"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Requisition Approval"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.rbtnOff, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnLBL5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnLBL4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnLBL3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnLBL2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rbtnLBL1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtLevel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtLevel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtLevel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblAmount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        CType(Me.btnReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtLevel3 As common.MyNumBox
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents txtLevel2 As common.MyNumBox
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents txtLevel1 As common.MyNumBox
    Friend WithEvents lblAmount As common.Controls.MyLabel
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents btnReset As Telerik.WinControls.UI.RadButton
    Friend WithEvents rbtnLBL5 As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rbtnLBL4 As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rbtnLBL3 As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rbtnLBL2 As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rbtnLBL1 As Telerik.WinControls.UI.RadRadioButton
    Friend WithEvents rbtnOff As Telerik.WinControls.UI.RadRadioButton
End Class

