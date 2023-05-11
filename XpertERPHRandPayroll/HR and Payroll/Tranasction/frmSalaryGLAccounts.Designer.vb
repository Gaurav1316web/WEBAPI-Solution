Imports XpertERPEngine

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSalaryGLAccounts
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
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.lblSalaryPayableAccount = New common.Controls.MyLabel()
        Me.MyLabel3 = New common.Controls.MyLabel()
        Me.txtESIPayableAcc = New common.Controls.MyTextBox()
        Me.txtSalaryPayableAccount = New common.Controls.MyTextBox()
        Me.lblToBank = New common.Controls.MyLabel()
        Me.txtToBanksAmt = New common.Controls.MyTextBox()
        Me.lblSalariesPayable = New common.Controls.MyLabel()
        Me.txtSalariesPayableAmt = New common.Controls.MyTextBox()
        Me.gv1 = New common.UserControls.MyRadGridView()
        Me.btnPost = New Telerik.WinControls.UI.RadButton()
        Me.btnclose = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.lblSalaryPayableAccount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtESIPayableAcc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSalaryPayableAccount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblToBank, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtToBanksAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblSalariesPayable, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSalariesPayableAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblSalaryPayableAccount)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MyLabel3)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtESIPayableAcc)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtSalaryPayableAccount)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblToBank)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtToBanksAmt)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblSalariesPayable)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtSalariesPayableAmt)
        Me.SplitContainer1.Panel1.Controls.Add(Me.gv1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPost)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Size = New System.Drawing.Size(775, 473)
        Me.SplitContainer1.SplitterDistance = 435
        Me.SplitContainer1.TabIndex = 0
        '
        'lblSalaryPayableAccount
        '
        Me.lblSalaryPayableAccount.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblSalaryPayableAccount.FieldName = Nothing
        Me.lblSalaryPayableAccount.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSalaryPayableAccount.Location = New System.Drawing.Point(10, 411)
        Me.lblSalaryPayableAccount.Name = "lblSalaryPayableAccount"
        Me.lblSalaryPayableAccount.Size = New System.Drawing.Size(126, 16)
        Me.lblSalaryPayableAccount.TabIndex = 243
        Me.lblSalaryPayableAccount.Text = "Salary Payable Account"
        '
        'MyLabel3
        '
        Me.MyLabel3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.MyLabel3.FieldName = Nothing
        Me.MyLabel3.Location = New System.Drawing.Point(455, 411)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(91, 18)
        Me.MyLabel3.TabIndex = 242
        Me.MyLabel3.Text = "Bank GL Account"
        '
        'txtESIPayableAcc
        '
        Me.txtESIPayableAcc.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtESIPayableAcc.CalculationExpression = Nothing
        Me.txtESIPayableAcc.FieldCode = Nothing
        Me.txtESIPayableAcc.FieldDesc = Nothing
        Me.txtESIPayableAcc.FieldMaxLength = 0
        Me.txtESIPayableAcc.FieldName = Nothing
        Me.txtESIPayableAcc.isCalculatedField = False
        Me.txtESIPayableAcc.IsSourceFromTable = False
        Me.txtESIPayableAcc.IsSourceFromValueList = False
        Me.txtESIPayableAcc.IsUnique = False
        Me.txtESIPayableAcc.Location = New System.Drawing.Point(552, 409)
        Me.txtESIPayableAcc.MaxLength = 55
        Me.txtESIPayableAcc.MendatroryField = False
        Me.txtESIPayableAcc.MyLinkLable1 = Nothing
        Me.txtESIPayableAcc.MyLinkLable2 = Nothing
        Me.txtESIPayableAcc.Name = "txtESIPayableAcc"
        Me.txtESIPayableAcc.ReadOnly = True
        Me.txtESIPayableAcc.ReferenceFieldDesc = Nothing
        Me.txtESIPayableAcc.ReferenceFieldName = Nothing
        Me.txtESIPayableAcc.ReferenceTableName = Nothing
        Me.txtESIPayableAcc.Size = New System.Drawing.Size(220, 20)
        Me.txtESIPayableAcc.TabIndex = 241
        '
        'txtSalaryPayableAccount
        '
        Me.txtSalaryPayableAccount.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtSalaryPayableAccount.CalculationExpression = Nothing
        Me.txtSalaryPayableAccount.FieldCode = Nothing
        Me.txtSalaryPayableAccount.FieldDesc = Nothing
        Me.txtSalaryPayableAccount.FieldMaxLength = 0
        Me.txtSalaryPayableAccount.FieldName = Nothing
        Me.txtSalaryPayableAccount.isCalculatedField = False
        Me.txtSalaryPayableAccount.IsSourceFromTable = False
        Me.txtSalaryPayableAccount.IsSourceFromValueList = False
        Me.txtSalaryPayableAccount.IsUnique = False
        Me.txtSalaryPayableAccount.Location = New System.Drawing.Point(141, 408)
        Me.txtSalaryPayableAccount.MaxLength = 50
        Me.txtSalaryPayableAccount.MendatroryField = False
        Me.txtSalaryPayableAccount.MyLinkLable1 = Nothing
        Me.txtSalaryPayableAccount.MyLinkLable2 = Nothing
        Me.txtSalaryPayableAccount.Name = "txtSalaryPayableAccount"
        Me.txtSalaryPayableAccount.ReadOnly = True
        Me.txtSalaryPayableAccount.ReferenceFieldDesc = Nothing
        Me.txtSalaryPayableAccount.ReferenceFieldName = Nothing
        Me.txtSalaryPayableAccount.ReferenceTableName = Nothing
        Me.txtSalaryPayableAccount.Size = New System.Drawing.Size(219, 20)
        Me.txtSalaryPayableAccount.TabIndex = 240
        '
        'lblToBank
        '
        Me.lblToBank.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblToBank.FieldName = Nothing
        Me.lblToBank.Location = New System.Drawing.Point(455, 388)
        Me.lblToBank.Name = "lblToBank"
        Me.lblToBank.Size = New System.Drawing.Size(51, 18)
        Me.lblToBank.TabIndex = 239
        Me.lblToBank.Text = "To Banks"
        '
        'txtToBanksAmt
        '
        Me.txtToBanksAmt.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtToBanksAmt.CalculationExpression = Nothing
        Me.txtToBanksAmt.FieldCode = Nothing
        Me.txtToBanksAmt.FieldDesc = Nothing
        Me.txtToBanksAmt.FieldMaxLength = 0
        Me.txtToBanksAmt.FieldName = Nothing
        Me.txtToBanksAmt.isCalculatedField = False
        Me.txtToBanksAmt.IsSourceFromTable = False
        Me.txtToBanksAmt.IsSourceFromValueList = False
        Me.txtToBanksAmt.IsUnique = False
        Me.txtToBanksAmt.Location = New System.Drawing.Point(552, 386)
        Me.txtToBanksAmt.MaxLength = 55
        Me.txtToBanksAmt.MendatroryField = False
        Me.txtToBanksAmt.MyLinkLable1 = Me.lblSalariesPayable
        Me.txtToBanksAmt.MyLinkLable2 = Nothing
        Me.txtToBanksAmt.Name = "txtToBanksAmt"
        Me.txtToBanksAmt.ReadOnly = True
        Me.txtToBanksAmt.ReferenceFieldDesc = Nothing
        Me.txtToBanksAmt.ReferenceFieldName = Nothing
        Me.txtToBanksAmt.ReferenceTableName = Nothing
        Me.txtToBanksAmt.Size = New System.Drawing.Size(220, 20)
        Me.txtToBanksAmt.TabIndex = 238
        '
        'lblSalariesPayable
        '
        Me.lblSalariesPayable.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblSalariesPayable.FieldName = Nothing
        Me.lblSalariesPayable.Location = New System.Drawing.Point(10, 388)
        Me.lblSalariesPayable.Name = "lblSalariesPayable"
        Me.lblSalariesPayable.Size = New System.Drawing.Size(86, 18)
        Me.lblSalariesPayable.TabIndex = 236
        Me.lblSalariesPayable.Text = "Salaries Payable"
        '
        'txtSalariesPayableAmt
        '
        Me.txtSalariesPayableAmt.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtSalariesPayableAmt.CalculationExpression = Nothing
        Me.txtSalariesPayableAmt.FieldCode = Nothing
        Me.txtSalariesPayableAmt.FieldDesc = Nothing
        Me.txtSalariesPayableAmt.FieldMaxLength = 0
        Me.txtSalariesPayableAmt.FieldName = Nothing
        Me.txtSalariesPayableAmt.isCalculatedField = False
        Me.txtSalariesPayableAmt.IsSourceFromTable = False
        Me.txtSalariesPayableAmt.IsSourceFromValueList = False
        Me.txtSalariesPayableAmt.IsUnique = False
        Me.txtSalariesPayableAmt.Location = New System.Drawing.Point(141, 386)
        Me.txtSalariesPayableAmt.MaxLength = 55
        Me.txtSalariesPayableAmt.MendatroryField = False
        Me.txtSalariesPayableAmt.MyLinkLable1 = Me.lblSalariesPayable
        Me.txtSalariesPayableAmt.MyLinkLable2 = Nothing
        Me.txtSalariesPayableAmt.Name = "txtSalariesPayableAmt"
        Me.txtSalariesPayableAmt.ReadOnly = True
        Me.txtSalariesPayableAmt.ReferenceFieldDesc = Nothing
        Me.txtSalariesPayableAmt.ReferenceFieldName = Nothing
        Me.txtSalariesPayableAmt.ReferenceTableName = Nothing
        Me.txtSalariesPayableAmt.Size = New System.Drawing.Size(219, 20)
        Me.txtSalariesPayableAmt.TabIndex = 237
        '
        'gv1
        '
        Me.gv1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gv1.BackColor = System.Drawing.Color.FromArgb(CType(CType(191, Byte), Integer), CType(CType(219, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.gv1.Cursor = System.Windows.Forms.Cursors.Default
        Me.gv1.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.gv1.ForeColor = System.Drawing.Color.Black
        Me.gv1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.gv1.Location = New System.Drawing.Point(3, 3)
        '
        'gv1
        '
        Me.gv1.MasterTemplate.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        Me.gv1.MasterTemplate.AllowAddNewRow = False
        Me.gv1.MasterTemplate.AllowEditRow = False
        Me.gv1.MasterTemplate.AutoGenerateColumns = False
        Me.gv1.MasterTemplate.EnableGrouping = False
        Me.gv1.MasterTemplate.ShowHeaderCellButtons = True
        Me.gv1.Name = "gv1"
        Me.gv1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gv1.ShowHeaderCellButtons = True
        Me.gv1.Size = New System.Drawing.Size(769, 377)
        Me.gv1.TabIndex = 6
        Me.gv1.Text = "RadGridView4"
        '
        'btnPost
        '
        Me.btnPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPost.Location = New System.Drawing.Point(10, 8)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(66, 18)
        Me.btnPost.TabIndex = 4
        Me.btnPost.Text = "Proceed"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnclose.Location = New System.Drawing.Point(691, 8)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(66, 18)
        Me.btnclose.TabIndex = 5
        Me.btnclose.Text = "Close"
        '
        'frmSalaryGLAccounts
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(775, 473)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "frmSalaryGLAccounts"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Salary Accounts"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.lblSalaryPayableAccount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtESIPayableAcc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSalaryPayableAccount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblToBank, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtToBanksAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblSalariesPayable, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSalariesPayableAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gv1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnPost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents gv1 As common.UserControls.MyRadGridView
    Friend WithEvents btnPost As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents lblToBank As common.Controls.MyLabel
    Friend WithEvents txtToBanksAmt As common.Controls.MyTextBox
    Friend WithEvents lblSalariesPayable As common.Controls.MyLabel
    Friend WithEvents txtSalariesPayableAmt As common.Controls.MyTextBox
    Friend WithEvents txtSalaryPayableAccount As common.Controls.MyTextBox
    Friend WithEvents txtESIPayableAcc As common.Controls.MyTextBox
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents lblSalaryPayableAccount As common.Controls.MyLabel
End Class
