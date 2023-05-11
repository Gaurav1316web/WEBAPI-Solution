<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmMTReportContextFormat
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
        Me.RadPageView1 = New Telerik.WinControls.UI.RadPageView
        Me.RDSGSWaiver = New Telerik.WinControls.UI.RadPageViewPage
        Me.MyLabel12 = New common.Controls.MyLabel
        Me.txtSGSWaiverContext = New System.Windows.Forms.RichTextBox
        Me.RDMerchantdeclaration = New Telerik.WinControls.UI.RadPageViewPage
        Me.MyLabel13 = New common.Controls.MyLabel
        Me.TxtMerchantDecContext = New System.Windows.Forms.RichTextBox
        Me.RDMerchantdeclarationF2 = New Telerik.WinControls.UI.RadPageViewPage
        Me.MyLabel2 = New common.Controls.MyLabel
        Me.TxtMerDecFormat2 = New System.Windows.Forms.RichTextBox
        Me.RDLCIssueApp = New Telerik.WinControls.UI.RadPageViewPage
        Me.txtLCIssueApp = New System.Windows.Forms.RichTextBox
        Me.MyLabel1 = New common.Controls.MyLabel
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.btnsave = New Telerik.WinControls.UI.RadButton
        Me.btnclose = New Telerik.WinControls.UI.RadButton
        Me.RDTrustReceipt = New Telerik.WinControls.UI.RadPageViewPage
        Me.RDAcceptanceLetter = New Telerik.WinControls.UI.RadPageViewPage
        Me.MyLabel3 = New common.Controls.MyLabel
        Me.TxtTrustReceipt = New System.Windows.Forms.RichTextBox
        Me.MyLabel4 = New common.Controls.MyLabel
        Me.TxtAcceptanceLetterContext = New System.Windows.Forms.RichTextBox
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView1.SuspendLayout()
        Me.RDSGSWaiver.SuspendLayout()
        CType(Me.MyLabel12, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RDMerchantdeclaration.SuspendLayout()
        CType(Me.MyLabel13, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RDMerchantdeclarationF2.SuspendLayout()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RDLCIssueApp.SuspendLayout()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RDTrustReceipt.SuspendLayout()
        Me.RDAcceptanceLetter.SuspendLayout()
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadPageView1
        '
        Me.RadPageView1.Controls.Add(Me.RDSGSWaiver)
        Me.RadPageView1.Controls.Add(Me.RDMerchantdeclaration)
        Me.RadPageView1.Controls.Add(Me.RDMerchantdeclarationF2)
        Me.RadPageView1.Controls.Add(Me.RDLCIssueApp)
        Me.RadPageView1.Controls.Add(Me.RDTrustReceipt)
        Me.RadPageView1.Controls.Add(Me.RDAcceptanceLetter)
        Me.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadPageView1.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView1.Name = "RadPageView1"
        Me.RadPageView1.SelectedPage = Me.RDTrustReceipt
        Me.RadPageView1.Size = New System.Drawing.Size(1135, 505)
        Me.RadPageView1.TabIndex = 2
        Me.RadPageView1.Text = "RadPageView1"
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripButtons = Telerik.WinControls.UI.StripViewButtons.None
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).ItemAlignment = Telerik.WinControls.UI.StripViewItemAlignment.Near
        CType(Me.RadPageView1.GetChildAt(0), Telerik.WinControls.UI.RadPageViewStripElement).StripAlignment = Telerik.WinControls.UI.StripViewAlignment.Top
        '
        'RDSGSWaiver
        '
        Me.RDSGSWaiver.Controls.Add(Me.MyLabel12)
        Me.RDSGSWaiver.Controls.Add(Me.txtSGSWaiverContext)
        Me.RDSGSWaiver.ItemSize = New System.Drawing.SizeF(111.0!, 26.0!)
        Me.RDSGSWaiver.Location = New System.Drawing.Point(10, 35)
        Me.RDSGSWaiver.Name = "RDSGSWaiver"
        Me.RDSGSWaiver.Size = New System.Drawing.Size(1114, 459)
        Me.RDSGSWaiver.Text = "SGS Waiver Letter"
        '
        'MyLabel12
        '
        Me.MyLabel12.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel12.Location = New System.Drawing.Point(6, 7)
        Me.MyLabel12.Name = "MyLabel12"
        Me.MyLabel12.Size = New System.Drawing.Size(111, 16)
        Me.MyLabel12.TabIndex = 330
        Me.MyLabel12.Text = "SGS Waiver Context"
        '
        'txtSGSWaiverContext
        '
        Me.txtSGSWaiverContext.Location = New System.Drawing.Point(119, 3)
        Me.txtSGSWaiverContext.Name = "txtSGSWaiverContext"
        Me.txtSGSWaiverContext.Size = New System.Drawing.Size(873, 435)
        Me.txtSGSWaiverContext.TabIndex = 329
        Me.txtSGSWaiverContext.Text = ""
        '
        'RDMerchantdeclaration
        '
        Me.RDMerchantdeclaration.Controls.Add(Me.MyLabel13)
        Me.RDMerchantdeclaration.Controls.Add(Me.TxtMerchantDecContext)
        Me.RDMerchantdeclaration.ItemSize = New System.Drawing.SizeF(172.0!, 26.0!)
        Me.RDMerchantdeclaration.Location = New System.Drawing.Point(10, 35)
        Me.RDMerchantdeclaration.Name = "RDMerchantdeclaration"
        Me.RDMerchantdeclaration.Size = New System.Drawing.Size(1114, 459)
        Me.RDMerchantdeclaration.Text = "Merchant Declaration Format 1"
        '
        'MyLabel13
        '
        Me.MyLabel13.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel13.Location = New System.Drawing.Point(6, 8)
        Me.MyLabel13.Name = "MyLabel13"
        Me.MyLabel13.Size = New System.Drawing.Size(156, 16)
        Me.MyLabel13.TabIndex = 334
        Me.MyLabel13.Text = "Merchant Declaration Context"
        '
        'TxtMerchantDecContext
        '
        Me.TxtMerchantDecContext.Location = New System.Drawing.Point(167, 4)
        Me.TxtMerchantDecContext.Name = "TxtMerchantDecContext"
        Me.TxtMerchantDecContext.Size = New System.Drawing.Size(836, 461)
        Me.TxtMerchantDecContext.TabIndex = 333
        Me.TxtMerchantDecContext.Text = ""
        '
        'RDMerchantdeclarationF2
        '
        Me.RDMerchantdeclarationF2.Controls.Add(Me.MyLabel2)
        Me.RDMerchantdeclarationF2.Controls.Add(Me.TxtMerDecFormat2)
        Me.RDMerchantdeclarationF2.ItemSize = New System.Drawing.SizeF(172.0!, 26.0!)
        Me.RDMerchantdeclarationF2.Location = New System.Drawing.Point(10, 35)
        Me.RDMerchantdeclarationF2.Name = "RDMerchantdeclarationF2"
        Me.RDMerchantdeclarationF2.Size = New System.Drawing.Size(1114, 459)
        Me.RDMerchantdeclarationF2.Text = "Merchant Declaration Format 2"
        '
        'MyLabel2
        '
        Me.MyLabel2.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel2.Location = New System.Drawing.Point(11, 3)
        Me.MyLabel2.Name = "MyLabel2"
        Me.MyLabel2.Size = New System.Drawing.Size(156, 16)
        Me.MyLabel2.TabIndex = 336
        Me.MyLabel2.Text = "Merchant Declaration Context"
        '
        'TxtMerDecFormat2
        '
        Me.TxtMerDecFormat2.Location = New System.Drawing.Point(172, -1)
        Me.TxtMerDecFormat2.Name = "TxtMerDecFormat2"
        Me.TxtMerDecFormat2.Size = New System.Drawing.Size(836, 461)
        Me.TxtMerDecFormat2.TabIndex = 335
        Me.TxtMerDecFormat2.Text = ""
        '
        'RDLCIssueApp
        '
        Me.RDLCIssueApp.Controls.Add(Me.txtLCIssueApp)
        Me.RDLCIssueApp.Controls.Add(Me.MyLabel1)
        Me.RDLCIssueApp.ItemSize = New System.Drawing.SizeF(128.0!, 26.0!)
        Me.RDLCIssueApp.Location = New System.Drawing.Point(10, 35)
        Me.RDLCIssueApp.Name = "RDLCIssueApp"
        Me.RDLCIssueApp.Size = New System.Drawing.Size(1114, 459)
        Me.RDLCIssueApp.Text = "LC Issuing Application"
        '
        'txtLCIssueApp
        '
        Me.txtLCIssueApp.Location = New System.Drawing.Point(139, -1)
        Me.txtLCIssueApp.Name = "txtLCIssueApp"
        Me.txtLCIssueApp.Size = New System.Drawing.Size(836, 461)
        Me.txtLCIssueApp.TabIndex = 337
        Me.txtLCIssueApp.Text = ""
        '
        'MyLabel1
        '
        Me.MyLabel1.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel1.Location = New System.Drawing.Point(6, 3)
        Me.MyLabel1.Name = "MyLabel1"
        Me.MyLabel1.Size = New System.Drawing.Size(118, 16)
        Me.MyLabel1.TabIndex = 336
        Me.MyLabel1.Text = "LC Issuing Application"
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.RadPageView1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnsave)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnclose)
        Me.SplitContainer1.Size = New System.Drawing.Size(1135, 539)
        Me.SplitContainer1.SplitterDistance = 505
        Me.SplitContainer1.TabIndex = 3
        '
        'btnsave
        '
        Me.btnsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnsave.Location = New System.Drawing.Point(26, 6)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(73, 20)
        Me.btnsave.TabIndex = 4
        Me.btnsave.Text = "Save"
        '
        'btnclose
        '
        Me.btnclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnclose.Location = New System.Drawing.Point(1035, 5)
        Me.btnclose.Name = "btnclose"
        Me.btnclose.Size = New System.Drawing.Size(73, 20)
        Me.btnclose.TabIndex = 5
        Me.btnclose.Text = "Close"
        '
        'RDTrustReceipt
        '
        Me.RDTrustReceipt.Controls.Add(Me.MyLabel3)
        Me.RDTrustReceipt.Controls.Add(Me.TxtTrustReceipt)
        Me.RDTrustReceipt.ItemSize = New System.Drawing.SizeF(84.0!, 26.0!)
        Me.RDTrustReceipt.Location = New System.Drawing.Point(10, 35)
        Me.RDTrustReceipt.Name = "RDTrustReceipt"
        Me.RDTrustReceipt.Size = New System.Drawing.Size(1114, 459)
        Me.RDTrustReceipt.Text = "Trust Receipt"
        '
        'RDAcceptanceLetter
        '
        Me.RDAcceptanceLetter.Controls.Add(Me.MyLabel4)
        Me.RDAcceptanceLetter.Controls.Add(Me.TxtAcceptanceLetterContext)
        Me.RDAcceptanceLetter.ItemSize = New System.Drawing.SizeF(107.0!, 26.0!)
        Me.RDAcceptanceLetter.Location = New System.Drawing.Point(10, 35)
        Me.RDAcceptanceLetter.Name = "RDAcceptanceLetter"
        Me.RDAcceptanceLetter.Size = New System.Drawing.Size(1114, 459)
        Me.RDAcceptanceLetter.Text = "Acceptance Letter"
        '
        'MyLabel3
        '
        Me.MyLabel3.AutoSize = False
        Me.MyLabel3.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel3.Location = New System.Drawing.Point(6, 6)
        Me.MyLabel3.Name = "MyLabel3"
        Me.MyLabel3.Size = New System.Drawing.Size(107, 38)
        Me.MyLabel3.TabIndex = 332
        Me.MyLabel3.Text = "Trust Receipt Context"
        '
        'TxtTrustReceipt
        '
        Me.TxtTrustReceipt.Location = New System.Drawing.Point(119, 2)
        Me.TxtTrustReceipt.Name = "TxtTrustReceipt"
        Me.TxtTrustReceipt.Size = New System.Drawing.Size(873, 435)
        Me.TxtTrustReceipt.TabIndex = 331
        Me.TxtTrustReceipt.Text = ""
        '
        'MyLabel4
        '
        Me.MyLabel4.AutoSize = False
        Me.MyLabel4.Font = New System.Drawing.Font("Arial", 8.25!)
        Me.MyLabel4.Location = New System.Drawing.Point(7, 5)
        Me.MyLabel4.Name = "MyLabel4"
        Me.MyLabel4.Size = New System.Drawing.Size(107, 35)
        Me.MyLabel4.TabIndex = 332
        Me.MyLabel4.Text = "Acceptance Letter Context"
        '
        'TxtAcceptanceLetterContext
        '
        Me.TxtAcceptanceLetterContext.Location = New System.Drawing.Point(120, 1)
        Me.TxtAcceptanceLetterContext.Name = "TxtAcceptanceLetterContext"
        Me.TxtAcceptanceLetterContext.Size = New System.Drawing.Size(873, 435)
        Me.TxtAcceptanceLetterContext.TabIndex = 331
        Me.TxtAcceptanceLetterContext.Text = ""
        '
        'FrmMTReportContextFormat
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1135, 539)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmMTReportContextFormat"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "MT Report Context Format"
        CType(Me.RadPageView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView1.ResumeLayout(False)
        Me.RDSGSWaiver.ResumeLayout(False)
        Me.RDSGSWaiver.PerformLayout()
        CType(Me.MyLabel12, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RDMerchantdeclaration.ResumeLayout(False)
        Me.RDMerchantdeclaration.PerformLayout()
        CType(Me.MyLabel13, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RDMerchantdeclarationF2.ResumeLayout(False)
        Me.RDMerchantdeclarationF2.PerformLayout()
        CType(Me.MyLabel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RDLCIssueApp.ResumeLayout(False)
        Me.RDLCIssueApp.PerformLayout()
        CType(Me.MyLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnclose, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RDTrustReceipt.ResumeLayout(False)
        Me.RDAcceptanceLetter.ResumeLayout(False)
        CType(Me.MyLabel3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MyLabel4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents RadPageView1 As Telerik.WinControls.UI.RadPageView
    Friend WithEvents RDSGSWaiver As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents MyLabel12 As common.Controls.MyLabel
    Friend WithEvents txtSGSWaiverContext As System.Windows.Forms.RichTextBox
    Friend WithEvents RDMerchantdeclaration As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents MyLabel13 As common.Controls.MyLabel
    Friend WithEvents TxtMerchantDecContext As System.Windows.Forms.RichTextBox
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents btnsave As Telerik.WinControls.UI.RadButton
    Friend WithEvents btnclose As Telerik.WinControls.UI.RadButton
    Friend WithEvents RDLCIssueApp As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents MyLabel1 As common.Controls.MyLabel
    Friend WithEvents txtLCIssueApp As System.Windows.Forms.RichTextBox
    Friend WithEvents RDMerchantdeclarationF2 As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents MyLabel2 As common.Controls.MyLabel
    Friend WithEvents TxtMerDecFormat2 As System.Windows.Forms.RichTextBox
    Friend WithEvents RDTrustReceipt As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents MyLabel3 As common.Controls.MyLabel
    Friend WithEvents TxtTrustReceipt As System.Windows.Forms.RichTextBox
    Friend WithEvents RDAcceptanceLetter As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents MyLabel4 As common.Controls.MyLabel
    Friend WithEvents TxtAcceptanceLetterContext As System.Windows.Forms.RichTextBox
End Class

