<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmFreshCreditLimit
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
        Me.grpCustomer = New Telerik.WinControls.UI.RadGroupBox()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.MyLabel24 = New common.Controls.MyLabel()
        Me.lblReverseAdvanceSec = New common.Controls.MyLabel()
        Me.MyLabel23 = New common.Controls.MyLabel()
        Me.lblReverseRefund = New common.Controls.MyLabel()
        Me.MyLabel35 = New common.Controls.MyLabel()
        Me.lblTotalOutstansing = New common.Controls.MyLabel()
        Me.MyLabel33 = New common.Controls.MyLabel()
        Me.lblRefund = New common.Controls.MyLabel()
        Me.MyLabel30 = New common.Controls.MyLabel()
        Me.lblLedgerOutstanding = New common.Controls.MyLabel()
        Me.MyLabel26 = New common.Controls.MyLabel()
        Me.lblPendingDO = New common.Controls.MyLabel()
        Me.MyLabel22 = New common.Controls.MyLabel()
        Me.lblAdvanceSecurity = New common.Controls.MyLabel()
        Me.MyLabel21 = New common.Controls.MyLabel()
        Me.lblCreditLimit = New common.Controls.MyLabel()
        Me.rdbtnClose = New Telerik.WinControls.UI.RadButton()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.MyLabel28 = New common.Controls.MyLabel()
        Me.lblShortcloseDO = New common.Controls.MyLabel()
        CType(Me.grpCustomer, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpCustomer.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.MyLabel24, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblReverseAdvanceSec, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel23, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblReverseRefund, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel35, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTotalOutstansing, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel33, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblRefund, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel30, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLedgerOutstanding, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel26, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblPendingDO, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel22, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblAdvanceSecurity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel21, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCreditLimit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbtnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.MyLabel28, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblShortcloseDO, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'grpCustomer
        '
        Me.grpCustomer.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.grpCustomer.Controls.Add(Me.Panel3)
        Me.grpCustomer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpCustomer.HeaderText = ""
        Me.grpCustomer.Location = New System.Drawing.Point(0, 0)
        Me.grpCustomer.Name = "grpCustomer"
        Me.grpCustomer.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.grpCustomer.Size = New System.Drawing.Size(429, 219)
        Me.grpCustomer.TabIndex = 0
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.MyLabel28)
        Me.Panel3.Controls.Add(Me.lblShortcloseDO)
        Me.Panel3.Controls.Add(Me.MyLabel24)
        Me.Panel3.Controls.Add(Me.lblReverseAdvanceSec)
        Me.Panel3.Controls.Add(Me.MyLabel23)
        Me.Panel3.Controls.Add(Me.lblReverseRefund)
        Me.Panel3.Controls.Add(Me.MyLabel35)
        Me.Panel3.Controls.Add(Me.lblTotalOutstansing)
        Me.Panel3.Controls.Add(Me.MyLabel33)
        Me.Panel3.Controls.Add(Me.lblRefund)
        Me.Panel3.Controls.Add(Me.MyLabel30)
        Me.Panel3.Controls.Add(Me.lblLedgerOutstanding)
        Me.Panel3.Controls.Add(Me.MyLabel26)
        Me.Panel3.Controls.Add(Me.lblPendingDO)
        Me.Panel3.Controls.Add(Me.MyLabel22)
        Me.Panel3.Controls.Add(Me.lblAdvanceSecurity)
        Me.Panel3.Controls.Add(Me.MyLabel21)
        Me.Panel3.Controls.Add(Me.lblCreditLimit)
        Me.Panel3.Location = New System.Drawing.Point(3, 3)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(423, 211)
        Me.Panel3.TabIndex = 1415
        '
        'MyLabel24
        '
        Me.MyLabel24.FieldName = Nothing
        Me.MyLabel24.Location = New System.Drawing.Point(3, 42)
        Me.MyLabel24.Name = "MyLabel24"
        Me.MyLabel24.Size = New System.Drawing.Size(118, 18)
        Me.MyLabel24.TabIndex = 74
        Me.MyLabel24.Text = "Revese Adv Security(-)"
        '
        'lblReverseAdvanceSec
        '
        Me.lblReverseAdvanceSec.AutoSize = False
        Me.lblReverseAdvanceSec.BorderVisible = True
        Me.lblReverseAdvanceSec.FieldName = Nothing
        Me.lblReverseAdvanceSec.Location = New System.Drawing.Point(130, 41)
        Me.lblReverseAdvanceSec.Name = "lblReverseAdvanceSec"
        Me.lblReverseAdvanceSec.Size = New System.Drawing.Size(169, 19)
        Me.lblReverseAdvanceSec.TabIndex = 73
        Me.lblReverseAdvanceSec.TextWrap = False
        '
        'MyLabel23
        '
        Me.MyLabel23.FieldName = Nothing
        Me.MyLabel23.Location = New System.Drawing.Point(3, 121)
        Me.MyLabel23.Name = "MyLabel23"
        Me.MyLabel23.Size = New System.Drawing.Size(98, 18)
        Me.MyLabel23.TabIndex = 72
        Me.MyLabel23.Text = "Reverse Refund(+)"
        '
        'lblReverseRefund
        '
        Me.lblReverseRefund.AutoSize = False
        Me.lblReverseRefund.BorderVisible = True
        Me.lblReverseRefund.FieldName = Nothing
        Me.lblReverseRefund.Location = New System.Drawing.Point(130, 120)
        Me.lblReverseRefund.Name = "lblReverseRefund"
        Me.lblReverseRefund.Size = New System.Drawing.Size(169, 19)
        Me.lblReverseRefund.TabIndex = 71
        Me.lblReverseRefund.TextWrap = False
        '
        'MyLabel35
        '
        Me.MyLabel35.FieldName = Nothing
        Me.MyLabel35.Location = New System.Drawing.Point(25, 178)
        Me.MyLabel35.Name = "MyLabel35"
        Me.MyLabel35.Size = New System.Drawing.Size(96, 18)
        Me.MyLabel35.TabIndex = 70
        Me.MyLabel35.Text = "Total Outstanding"
        '
        'lblTotalOutstansing
        '
        Me.lblTotalOutstansing.AutoSize = False
        Me.lblTotalOutstansing.BorderVisible = True
        Me.lblTotalOutstansing.FieldName = Nothing
        Me.lblTotalOutstansing.Location = New System.Drawing.Point(131, 177)
        Me.lblTotalOutstansing.Name = "lblTotalOutstansing"
        Me.lblTotalOutstansing.Size = New System.Drawing.Size(169, 19)
        Me.lblTotalOutstansing.TabIndex = 69
        Me.lblTotalOutstansing.TextWrap = False
        '
        'MyLabel33
        '
        Me.MyLabel33.FieldName = Nothing
        Me.MyLabel33.Location = New System.Drawing.Point(3, 101)
        Me.MyLabel33.Name = "MyLabel33"
        Me.MyLabel33.Size = New System.Drawing.Size(53, 18)
        Me.MyLabel33.TabIndex = 68
        Me.MyLabel33.Text = "Refund(-)"
        '
        'lblRefund
        '
        Me.lblRefund.AutoSize = False
        Me.lblRefund.BorderVisible = True
        Me.lblRefund.FieldName = Nothing
        Me.lblRefund.Location = New System.Drawing.Point(130, 100)
        Me.lblRefund.Name = "lblRefund"
        Me.lblRefund.Size = New System.Drawing.Size(169, 19)
        Me.lblRefund.TabIndex = 67
        Me.lblRefund.TextWrap = False
        '
        'MyLabel30
        '
        Me.MyLabel30.FieldName = Nothing
        Me.MyLabel30.Location = New System.Drawing.Point(3, 83)
        Me.MyLabel30.Name = "MyLabel30"
        Me.MyLabel30.Size = New System.Drawing.Size(115, 18)
        Me.MyLabel30.TabIndex = 66
        Me.MyLabel30.Text = "Ledger Outstansing(-)"
        '
        'lblLedgerOutstanding
        '
        Me.lblLedgerOutstanding.AutoSize = False
        Me.lblLedgerOutstanding.BorderVisible = True
        Me.lblLedgerOutstanding.FieldName = Nothing
        Me.lblLedgerOutstanding.Location = New System.Drawing.Point(130, 82)
        Me.lblLedgerOutstanding.Name = "lblLedgerOutstanding"
        Me.lblLedgerOutstanding.Size = New System.Drawing.Size(169, 19)
        Me.lblLedgerOutstanding.TabIndex = 65
        Me.lblLedgerOutstanding.TextWrap = False
        '
        'MyLabel26
        '
        Me.MyLabel26.FieldName = Nothing
        Me.MyLabel26.Location = New System.Drawing.Point(4, 63)
        Me.MyLabel26.Name = "MyLabel26"
        Me.MyLabel26.Size = New System.Drawing.Size(116, 18)
        Me.MyLabel26.TabIndex = 62
        Me.MyLabel26.Text = "Pending Posted DO(-)"
        '
        'lblPendingDO
        '
        Me.lblPendingDO.AutoSize = False
        Me.lblPendingDO.BorderVisible = True
        Me.lblPendingDO.FieldName = Nothing
        Me.lblPendingDO.Location = New System.Drawing.Point(130, 62)
        Me.lblPendingDO.Name = "lblPendingDO"
        Me.lblPendingDO.Size = New System.Drawing.Size(169, 19)
        Me.lblPendingDO.TabIndex = 61
        Me.lblPendingDO.TextWrap = False
        '
        'MyLabel22
        '
        Me.MyLabel22.FieldName = Nothing
        Me.MyLabel22.Location = New System.Drawing.Point(3, 21)
        Me.MyLabel22.Name = "MyLabel22"
        Me.MyLabel22.Size = New System.Drawing.Size(106, 18)
        Me.MyLabel22.TabIndex = 58
        Me.MyLabel22.Text = "Advance Security(+)"
        '
        'lblAdvanceSecurity
        '
        Me.lblAdvanceSecurity.AutoSize = False
        Me.lblAdvanceSecurity.BorderVisible = True
        Me.lblAdvanceSecurity.FieldName = Nothing
        Me.lblAdvanceSecurity.Location = New System.Drawing.Point(130, 20)
        Me.lblAdvanceSecurity.Name = "lblAdvanceSecurity"
        Me.lblAdvanceSecurity.Size = New System.Drawing.Size(169, 19)
        Me.lblAdvanceSecurity.TabIndex = 57
        Me.lblAdvanceSecurity.TextWrap = False
        '
        'MyLabel21
        '
        Me.MyLabel21.FieldName = Nothing
        Me.MyLabel21.Location = New System.Drawing.Point(3, 3)
        Me.MyLabel21.Name = "MyLabel21"
        Me.MyLabel21.Size = New System.Drawing.Size(78, 18)
        Me.MyLabel21.TabIndex = 56
        Me.MyLabel21.Text = "Credit Limit(+)"
        '
        'lblCreditLimit
        '
        Me.lblCreditLimit.AutoSize = False
        Me.lblCreditLimit.BorderVisible = True
        Me.lblCreditLimit.FieldName = Nothing
        Me.lblCreditLimit.Location = New System.Drawing.Point(130, 2)
        Me.lblCreditLimit.Name = "lblCreditLimit"
        Me.lblCreditLimit.Size = New System.Drawing.Size(169, 19)
        Me.lblCreditLimit.TabIndex = 25
        Me.lblCreditLimit.TextWrap = False
        '
        'rdbtnClose
        '
        Me.rdbtnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.rdbtnClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdbtnClose.Location = New System.Drawing.Point(354, 4)
        Me.rdbtnClose.Name = "rdbtnClose"
        Me.rdbtnClose.Size = New System.Drawing.Size(68, 18)
        Me.rdbtnClose.TabIndex = 2
        Me.rdbtnClose.Text = "Close"
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.grpCustomer)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.rdbtnClose)
        Me.SplitContainer1.Size = New System.Drawing.Size(429, 248)
        Me.SplitContainer1.SplitterDistance = 219
        Me.SplitContainer1.TabIndex = 1
        '
        'MyLabel28
        '
        Me.MyLabel28.FieldName = Nothing
        Me.MyLabel28.Location = New System.Drawing.Point(1, 143)
        Me.MyLabel28.Name = "MyLabel28"
        Me.MyLabel28.Size = New System.Drawing.Size(109, 18)
        Me.MyLabel28.TabIndex = 76
        Me.MyLabel28.Text = "UnPostedDispatch(-)"
        '
        'lblShortcloseDO
        '
        Me.lblShortcloseDO.AutoSize = False
        Me.lblShortcloseDO.BorderVisible = True
        Me.lblShortcloseDO.FieldName = Nothing
        Me.lblShortcloseDO.Location = New System.Drawing.Point(130, 142)
        Me.lblShortcloseDO.Name = "lblShortcloseDO"
        Me.lblShortcloseDO.Size = New System.Drawing.Size(169, 19)
        Me.lblShortcloseDO.TabIndex = 75
        Me.lblShortcloseDO.TextWrap = False
        '
        'frmFreshCreditLimit
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(429, 248)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "frmFreshCreditLimit"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "Credit Limit Details"
        CType(Me.grpCustomer, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpCustomer.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        CType(Me.MyLabel24, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblReverseAdvanceSec, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel23, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblReverseRefund, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel35, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTotalOutstansing, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel33, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblRefund, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel30, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLedgerOutstanding, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel26, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblPendingDO, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel22, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblAdvanceSecurity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel21, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCreditLimit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbtnClose, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.MyLabel28, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblShortcloseDO, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents grpCustomer As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents rdbtnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents MyLabel24 As common.Controls.MyLabel
    Friend WithEvents lblReverseAdvanceSec As common.Controls.MyLabel
    Friend WithEvents MyLabel23 As common.Controls.MyLabel
    Friend WithEvents lblReverseRefund As common.Controls.MyLabel
    Friend WithEvents MyLabel35 As common.Controls.MyLabel
    Friend WithEvents lblTotalOutstansing As common.Controls.MyLabel
    Friend WithEvents MyLabel33 As common.Controls.MyLabel
    Friend WithEvents lblRefund As common.Controls.MyLabel
    Friend WithEvents MyLabel30 As common.Controls.MyLabel
    Friend WithEvents lblLedgerOutstanding As common.Controls.MyLabel
    Friend WithEvents MyLabel26 As common.Controls.MyLabel
    Friend WithEvents lblPendingDO As common.Controls.MyLabel
    Friend WithEvents MyLabel22 As common.Controls.MyLabel
    Friend WithEvents lblAdvanceSecurity As common.Controls.MyLabel
    Friend WithEvents MyLabel21 As common.Controls.MyLabel
    Friend WithEvents lblCreditLimit As common.Controls.MyLabel
    Friend WithEvents MyLabel28 As common.Controls.MyLabel
    Friend WithEvents lblShortcloseDO As common.Controls.MyLabel
End Class

