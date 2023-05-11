<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmViewPunchingInvoice
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
        Me.grpbxAdditional = New Telerik.WinControls.UI.RadGroupBox
        Me.lblBalance = New Telerik.WinControls.UI.RadLabel
        Me.lblBalancename = New Telerik.WinControls.UI.RadLabel
        Me.lblPunchedInvoice = New Telerik.WinControls.UI.RadLabel
        Me.RadLabel1 = New Telerik.WinControls.UI.RadLabel
        Me.lblNoOfCashMemo = New common.Controls.MyLabel
        Me.txtNoOfCashMemo = New common.MyNumBox
        Me.fndTransferNo = New common.UserControls.txtNavigator
        Me.lblTransferNo = New common.Controls.MyLabel
        Me.btnreset = New Telerik.WinControls.UI.RadButton
        Me.btnClose = New Telerik.WinControls.UI.RadButton
        Me.btnSave = New Telerik.WinControls.UI.RadButton
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        CType(Me.grpbxAdditional, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpbxAdditional.SuspendLayout()
        CType(Me.lblBalance, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBalancename, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblPunchedInvoice, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblNoOfCashMemo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNoOfCashMemo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTransferNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnreset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'grpbxAdditional
        '
        Me.grpbxAdditional.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.grpbxAdditional.Controls.Add(Me.lblBalance)
        Me.grpbxAdditional.Controls.Add(Me.lblBalancename)
        Me.grpbxAdditional.Controls.Add(Me.lblPunchedInvoice)
        Me.grpbxAdditional.Controls.Add(Me.RadLabel1)
        Me.grpbxAdditional.Controls.Add(Me.lblNoOfCashMemo)
        Me.grpbxAdditional.Controls.Add(Me.txtNoOfCashMemo)
        Me.grpbxAdditional.Controls.Add(Me.fndTransferNo)
        Me.grpbxAdditional.Controls.Add(Me.btnreset)
        Me.grpbxAdditional.Controls.Add(Me.lblTransferNo)
        Me.grpbxAdditional.FooterImageIndex = -1
        Me.grpbxAdditional.FooterImageKey = ""
        Me.grpbxAdditional.HeaderImageIndex = -1
        Me.grpbxAdditional.HeaderImageKey = ""
        Me.grpbxAdditional.HeaderMargin = New System.Windows.Forms.Padding(0)
        Me.grpbxAdditional.HeaderText = ""
        Me.grpbxAdditional.Location = New System.Drawing.Point(18, 3)
        Me.grpbxAdditional.Name = "grpbxAdditional"
        Me.grpbxAdditional.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        '
        '
        '
        Me.grpbxAdditional.RootElement.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.grpbxAdditional.Size = New System.Drawing.Size(401, 133)
        Me.grpbxAdditional.TabIndex = 0
        '
        'lblBalance
        '
        Me.lblBalance.Location = New System.Drawing.Point(130, 103)
        Me.lblBalance.Name = "lblBalance"
        Me.lblBalance.Size = New System.Drawing.Size(12, 18)
        Me.lblBalance.TabIndex = 4
        Me.lblBalance.Text = "0"
        '
        'lblBalancename
        '
        Me.lblBalancename.Location = New System.Drawing.Point(13, 103)
        Me.lblBalancename.Name = "lblBalancename"
        Me.lblBalancename.Size = New System.Drawing.Size(45, 18)
        Me.lblBalancename.TabIndex = 10
        Me.lblBalancename.Text = "Balance"
        '
        'lblPunchedInvoice
        '
        Me.lblPunchedInvoice.Location = New System.Drawing.Point(130, 79)
        Me.lblPunchedInvoice.Name = "lblPunchedInvoice"
        Me.lblPunchedInvoice.Size = New System.Drawing.Size(12, 18)
        Me.lblPunchedInvoice.TabIndex = 3
        Me.lblPunchedInvoice.Text = "0"
        '
        'RadLabel1
        '
        Me.RadLabel1.Location = New System.Drawing.Point(13, 79)
        Me.RadLabel1.Name = "RadLabel1"
        Me.RadLabel1.Size = New System.Drawing.Size(88, 18)
        Me.RadLabel1.TabIndex = 9
        Me.RadLabel1.Text = "Punched Invoice"
        '
        'lblNoOfCashMemo
        '
        Me.lblNoOfCashMemo.Location = New System.Drawing.Point(13, 45)
        Me.lblNoOfCashMemo.Name = "lblNoOfCashMemo"
        Me.lblNoOfCashMemo.Size = New System.Drawing.Size(99, 18)
        Me.lblNoOfCashMemo.TabIndex = 8
        Me.lblNoOfCashMemo.Text = "No Of Cash Memo"
        '
        'txtNoOfCashMemo
        '
        Me.txtNoOfCashMemo.BackColor = System.Drawing.Color.White
        Me.txtNoOfCashMemo.DecimalPlaces = 0
        Me.txtNoOfCashMemo.Location = New System.Drawing.Point(130, 43)
        Me.txtNoOfCashMemo.MendatroryField = False
        Me.txtNoOfCashMemo.MyLinkLable1 = Me.lblNoOfCashMemo
        Me.txtNoOfCashMemo.MyLinkLable2 = Nothing
        Me.txtNoOfCashMemo.Name = "txtNoOfCashMemo"
        Me.txtNoOfCashMemo.Size = New System.Drawing.Size(100, 20)
        Me.txtNoOfCashMemo.TabIndex = 2
        Me.txtNoOfCashMemo.Text = "0"
        Me.txtNoOfCashMemo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtNoOfCashMemo.Value = 0
        '
        'fndTransferNo
        '
        Me.fndTransferNo.Location = New System.Drawing.Point(91, 16)
        Me.fndTransferNo.MendatroryField = True
        Me.fndTransferNo.MyCharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.fndTransferNo.MyLinkLable1 = Me.lblTransferNo
        Me.fndTransferNo.MyLinkLable2 = Nothing
        Me.fndTransferNo.MyMaxLength = 32767
        Me.fndTransferNo.MyReadOnly = False
        Me.fndTransferNo.Name = "fndTransferNo"
        Me.fndTransferNo.Size = New System.Drawing.Size(268, 21)
        Me.fndTransferNo.TabIndex = 0
        Me.fndTransferNo.Value = ""
        '
        'lblTransferNo
        '
        Me.lblTransferNo.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblTransferNo.Location = New System.Drawing.Point(11, 16)
        Me.lblTransferNo.Name = "lblTransferNo"
        Me.lblTransferNo.Size = New System.Drawing.Size(69, 18)
        Me.lblTransferNo.TabIndex = 7
        Me.lblTransferNo.Text = "Transfer No"
        '
        'btnreset
        '
        Me.btnreset.Image = Global.ERP.My.Resources.Resources._new
        Me.btnreset.Location = New System.Drawing.Point(363, 18)
        Me.btnreset.Name = "btnreset"
        Me.btnreset.Size = New System.Drawing.Size(17, 21)
        Me.btnreset.TabIndex = 1
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Location = New System.Drawing.Point(353, 3)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(66, 19)
        Me.btnClose.TabIndex = 6
        Me.btnClose.Text = "Close"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Location = New System.Drawing.Point(18, 3)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(66, 19)
        Me.btnSave.TabIndex = 5
        Me.btnSave.Text = "Save"
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.grpbxAdditional)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClose)
        Me.SplitContainer1.Size = New System.Drawing.Size(435, 179)
        Me.SplitContainer1.SplitterDistance = 141
        Me.SplitContainer1.TabIndex = 1
        '
        'FrmViewPunchingInvoice
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(435, 179)
        Me.Controls.Add(Me.SplitContainer1)
        Me.KeyPreview = True
        Me.Name = "FrmViewPunchingInvoice"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "View Punching Invoice"
        CType(Me.grpbxAdditional, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpbxAdditional.ResumeLayout(False)
        Me.grpbxAdditional.PerformLayout()
        CType(Me.lblBalance, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBalancename, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblPunchedInvoice, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblNoOfCashMemo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNoOfCashMemo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTransferNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnreset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnClose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnSave, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents grpbxAdditional As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents fndTransferNo As common.UserControls.txtNavigator
    Friend WithEvents lblTransferNo As common.Controls.MyLabel
    Friend WithEvents btnreset As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnClose As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnSave As Telerik.WinControls.UI.RadButton
    Friend WithEvents txtNoOfCashMemo As common.MyNumBox
    Friend WithEvents lblNoOfCashMemo As common.Controls.MyLabel
    Friend WithEvents RadLabel1 As Telerik.WinControls.UI.RadLabel
    Friend WithEvents lblBalance As Telerik.WinControls.UI.RadLabel
    Friend WithEvents lblBalancename As Telerik.WinControls.UI.RadLabel
    Friend WithEvents lblPunchedInvoice As Telerik.WinControls.UI.RadLabel
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
End Class

