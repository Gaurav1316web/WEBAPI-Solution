<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmBulkSaleSettings
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
        Me.FndItemCode = New common.UserControls.txtFinder
        Me.lblItemDesc = New common.Controls.MyLabel
        Me.lblMCCCode = New common.Controls.MyLabel
        Me.ChkAllowItemMRP = New common.Controls.MyCheckBox
        Me.TxtCorrectionFactor = New common.MyNumBox
        Me.MyLabel2 = New common.Controls.MyLabel
        Me.TxtAmountLimitForInvoiceBulkSale = New common.MyNumBox
        Me.MyLabel1 = New common.Controls.MyLabel
        Me.chkCreateDeliveryorderincaseamountincrease = New common.Controls.MyCheckBox
        Me.chkDispatchOutstandingFS = New common.Controls.MyCheckBox
        Me.chkDispatchOutstandingBS = New common.Controls.MyCheckBox
        Me.rdbtnsave = New Telerik.WinControls.UI.RadButton
        Me.rdbtnclose = New Telerik.WinControls.UI.RadButton
        Me.FndItemCodeforBulk = New common.UserControls.txtFinder
        Me.lblItemDescBulk = New common.Controls.MyLabel
        Me.MyLabel4 = New common.Controls.MyLabel
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.lblItemDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblMCCCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ChkAllowItemMRP, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtCorrectionFactor, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtAmountLimitForInvoiceBulkSale, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkCreateDeliveryorderincaseamountincrease, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkDispatchOutstandingFS, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkDispatchOutstandingBS, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbtnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rdbtnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblItemDescBulk, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadGroupBox1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.rdbtnsave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.rdbtnclose)
        Me.SplitContainer1.Size = New System.Drawing.Size(833, 244)
        Me.SplitContainer1.SplitterDistance = 211
        Me.SplitContainer1.TabIndex = 0
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.Controls.Add(Me.FndItemCodeforBulk)
        Me.RadGroupBox1.Controls.Add(Me.lblItemDescBulk)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel4)
        Me.RadGroupBox1.Controls.Add(Me.FndItemCode)
        Me.RadGroupBox1.Controls.Add(Me.lblItemDesc)
        Me.RadGroupBox1.Controls.Add(Me.lblMCCCode)
        Me.RadGroupBox1.Controls.Add(Me.ChkAllowItemMRP)
        Me.RadGroupBox1.Controls.Add(Me.TxtCorrectionFactor)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel2)
        Me.RadGroupBox1.Controls.Add(Me.TxtAmountLimitForInvoiceBulkSale)
        Me.RadGroupBox1.Controls.Add(Me.MyLabel1)
        Me.RadGroupBox1.Controls.Add(Me.chkCreateDeliveryorderincaseamountincrease)
        Me.RadGroupBox1.Controls.Add(Me.chkDispatchOutstandingFS)
        Me.RadGroupBox1.Controls.Add(Me.chkDispatchOutstandingBS)
        Me.RadGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadGroupBox1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadGroupBox1.HeaderText = ""
        Me.RadGroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Padding = New System.Windows.Forms.Padding(10, 20, 10, 10)
        Me.RadGroupBox1.Size = New System.Drawing.Size(833, 211)
        Me.RadGroupBox1.TabIndex = 0
        '
        'FndItemCode
        '
        Me.FndItemCode.Location = New System.Drawing.Point(205, 129)
        Me.FndItemCode.MendatroryField = False
        Me.FndItemCode.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FndItemCode.MyLinkLable1 = Nothing
        Me.FndItemCode.MyLinkLable2 = Nothing
        Me.FndItemCode.MyReadOnly = False
        Me.FndItemCode.MyShowMasterFormButton = False
        Me.FndItemCode.Name = "FndItemCode"
        Me.FndItemCode.Size = New System.Drawing.Size(172, 19)
        Me.FndItemCode.TabIndex = 9
        Me.FndItemCode.Value = ""
        '
        'lblItemDesc
        '
        Me.lblItemDesc.AutoSize = False
        Me.lblItemDesc.BorderVisible = True
        Me.lblItemDesc.Location = New System.Drawing.Point(383, 129)
        Me.lblItemDesc.Name = "lblItemDesc"
        Me.lblItemDesc.Size = New System.Drawing.Size(423, 19)
        Me.lblItemDesc.TabIndex = 10
        Me.lblItemDesc.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblMCCCode
        '
        Me.lblMCCCode.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.lblMCCCode.Location = New System.Drawing.Point(11, 129)
        Me.lblMCCCode.Name = "lblMCCCode"
        Me.lblMCCCode.Size = New System.Drawing.Size(191, 18)
        Me.lblMCCCode.TabIndex = 8
        Me.lblMCCCode.Text = "Default Milk Item For Bulk Sale Trade"
        '
        'ChkAllowItemMRP
        '
        Me.ChkAllowItemMRP.Location = New System.Drawing.Point(11, 70)
        Me.ChkAllowItemMRP.MyLinkLable1 = Nothing
        Me.ChkAllowItemMRP.MyLinkLable2 = Nothing
        Me.ChkAllowItemMRP.Name = "ChkAllowItemMRP"
        Me.ChkAllowItemMRP.Size = New System.Drawing.Size(190, 18)
        Me.ChkAllowItemMRP.TabIndex = 3
        Me.ChkAllowItemMRP.Tag1 = Nothing
        Me.ChkAllowItemMRP.Text = "Allow to enter item MRP manually"
        '
        'TxtCorrectionFactor
        '
        Me.TxtCorrectionFactor.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.TxtCorrectionFactor.DecimalPlaces = 2
        Me.TxtCorrectionFactor.Location = New System.Drawing.Point(205, 108)
        Me.TxtCorrectionFactor.MendatroryField = False
        Me.TxtCorrectionFactor.MyLinkLable1 = Nothing
        Me.TxtCorrectionFactor.MyLinkLable2 = Nothing
        Me.TxtCorrectionFactor.Name = "TxtCorrectionFactor"
        Me.TxtCorrectionFactor.Size = New System.Drawing.Size(172, 20)
        Me.TxtCorrectionFactor.TabIndex = 7
        Me.TxtCorrectionFactor.Text = "0"
        Me.TxtCorrectionFactor.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TxtCorrectionFactor.Value = 0
        '
        'MyLabel2
        '
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel2.Location = New System.Drawing.Point(11, 110)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(133, 16)
        Me.MyLabel2.TabIndex = 6
        Me.MyLabel2.Text = "Default Correction Factor"
        '
        'TxtAmountLimitForInvoiceBulkSale
        '
        Me.TxtAmountLimitForInvoiceBulkSale.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.TxtAmountLimitForInvoiceBulkSale.DecimalPlaces = 2
        Me.TxtAmountLimitForInvoiceBulkSale.Location = New System.Drawing.Point(205, 89)
        Me.TxtAmountLimitForInvoiceBulkSale.MendatroryField = False
        Me.TxtAmountLimitForInvoiceBulkSale.MyLinkLable1 = Nothing
        Me.TxtAmountLimitForInvoiceBulkSale.MyLinkLable2 = Nothing
        Me.TxtAmountLimitForInvoiceBulkSale.Name = "TxtAmountLimitForInvoiceBulkSale"
        Me.TxtAmountLimitForInvoiceBulkSale.Size = New System.Drawing.Size(172, 20)
        Me.TxtAmountLimitForInvoiceBulkSale.TabIndex = 5
        Me.TxtAmountLimitForInvoiceBulkSale.Text = "0"
        Me.TxtAmountLimitForInvoiceBulkSale.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TxtAmountLimitForInvoiceBulkSale.Value = 0
        '
        'MyLabel1
        '
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyLabel1.Location = New System.Drawing.Point(11, 91)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(179, 16)
        Me.MyLabel1.TabIndex = 4
        Me.MyLabel1.Text = "Amount Limit for Invoice Bulk Sale"
        '
        'chkCreateDeliveryorderincaseamountincrease
        '
        Me.chkCreateDeliveryorderincaseamountincrease.Location = New System.Drawing.Point(11, 49)
        Me.chkCreateDeliveryorderincaseamountincrease.MyLinkLable1 = Nothing
        Me.chkCreateDeliveryorderincaseamountincrease.MyLinkLable2 = Nothing
        Me.chkCreateDeliveryorderincaseamountincrease.Name = "chkCreateDeliveryorderincaseamountincrease"
        Me.chkCreateDeliveryorderincaseamountincrease.Size = New System.Drawing.Size(292, 18)
        Me.chkCreateDeliveryorderincaseamountincrease.TabIndex = 2
        Me.chkCreateDeliveryorderincaseamountincrease.Tag1 = Nothing
        Me.chkCreateDeliveryorderincaseamountincrease.Text = "Allow to create delivery order incase amount increases"
        '
        'chkDispatchOutstandingFS
        '
        Me.chkDispatchOutstandingFS.Location = New System.Drawing.Point(11, 28)
        Me.chkDispatchOutstandingFS.MyLinkLable1 = Nothing
        Me.chkDispatchOutstandingFS.MyLinkLable2 = Nothing
        Me.chkDispatchOutstandingFS.Name = "chkDispatchOutstandingFS"
        Me.chkDispatchOutstandingFS.Size = New System.Drawing.Size(316, 18)
        Me.chkDispatchOutstandingFS.TabIndex = 1
        Me.chkDispatchOutstandingFS.Tag1 = Nothing
        Me.chkDispatchOutstandingFS.Text = "Allow delivery customer without checking  outstanding (FS)"
        '
        'chkDispatchOutstandingBS
        '
        Me.chkDispatchOutstandingBS.Location = New System.Drawing.Point(11, 7)
        Me.chkDispatchOutstandingBS.MyLinkLable1 = Nothing
        Me.chkDispatchOutstandingBS.MyLinkLable2 = Nothing
        Me.chkDispatchOutstandingBS.Name = "chkDispatchOutstandingBS"
        Me.chkDispatchOutstandingBS.Size = New System.Drawing.Size(320, 18)
        Me.chkDispatchOutstandingBS.TabIndex = 0
        Me.chkDispatchOutstandingBS.Tag1 = Nothing
        Me.chkDispatchOutstandingBS.Text = "Allow dispatch customer without checking  outstanding (BS)"
        '
        'rdbtnsave
        '
        Me.rdbtnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.rdbtnsave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdbtnsave.Location = New System.Drawing.Point(12, 6)
        Me.rdbtnsave.Name = "rdbtnsave"
        Me.rdbtnsave.Size = New System.Drawing.Size(82, 18)
        Me.rdbtnsave.TabIndex = 0
        Me.rdbtnsave.Text = "Save"
        '
        'rdbtnclose
        '
        Me.rdbtnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.rdbtnclose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdbtnclose.Location = New System.Drawing.Point(738, 6)
        Me.rdbtnclose.Name = "rdbtnclose"
        Me.rdbtnclose.Size = New System.Drawing.Size(83, 18)
        Me.rdbtnclose.TabIndex = 1
        Me.rdbtnclose.Text = "Close"
        '
        'FndItemCodeforBulk
        '
        Me.FndItemCodeforBulk.Location = New System.Drawing.Point(205, 150)
        Me.FndItemCodeforBulk.MendatroryField = False
        Me.FndItemCodeforBulk.MyFont = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FndItemCodeforBulk.MyLinkLable1 = Nothing
        Me.FndItemCodeforBulk.MyLinkLable2 = Nothing
        Me.FndItemCodeforBulk.MyReadOnly = False
        Me.FndItemCodeforBulk.MyShowMasterFormButton = False
        Me.FndItemCodeforBulk.Name = "FndItemCodeforBulk"
        Me.FndItemCodeforBulk.Size = New System.Drawing.Size(172, 19)
        Me.FndItemCodeforBulk.TabIndex = 12
        Me.FndItemCodeforBulk.Value = ""
        '
        'lblItemDescBulk
        '
        Me.lblItemDescBulk.AutoSize = False
        Me.lblItemDescBulk.BorderVisible = True
        Me.lblItemDescBulk.Location = New System.Drawing.Point(383, 150)
        Me.lblItemDescBulk.Name = "lblItemDescBulk"
        Me.lblItemDescBulk.Size = New System.Drawing.Size(423, 19)
        Me.lblItemDescBulk.TabIndex = 13
        Me.lblItemDescBulk.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        '
        'MyLabel4
        '
        Me.MyLabel4.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.MyLabel4.Location = New System.Drawing.Point(11, 150)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(191, 18)
        Me.MyLabel4.TabIndex = 11
        Me.MyLabel4.Text = "Default Milk Item For Bulk Sale Trade"
        '
        'FrmBulkSaleSettings
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(833, 244)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmBulkSaleSettings"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "FrmBulkSaleSettings"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.lblItemDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblMCCCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ChkAllowItemMRP, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtCorrectionFactor, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtAmountLimitForInvoiceBulkSale, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkCreateDeliveryorderincaseamountincrease, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkDispatchOutstandingFS, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkDispatchOutstandingBS, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbtnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rdbtnclose, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblItemDescBulk, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents chkDispatchOutstandingBS As common.Controls.MyCheckBox
    Friend WithEvents rdbtnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents rdbtnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents chkDispatchOutstandingFS As common.Controls.MyCheckBox
    Friend WithEvents chkCreateDeliveryorderincaseamountincrease As common.Controls.MyCheckBox
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents TxtAmountLimitForInvoiceBulkSale As common.MyNumBox
    Friend WithEvents TxtCorrectionFactor As common.MyNumBox
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents ChkAllowItemMRP As common.Controls.MyCheckBox
    Friend WithEvents lblItemDesc As common.Controls.MyLabel
    Friend WithEvents lblMCCCode As common.Controls.MyLabel
    Friend WithEvents FndItemCode As common.UserControls.txtFinder
    Friend WithEvents FndItemCodeforBulk As common.UserControls.txtFinder
    Friend WithEvents lblItemDescBulk As common.Controls.MyLabel
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
End Class

